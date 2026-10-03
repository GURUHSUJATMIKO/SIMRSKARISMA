Imports DataAccess
Imports UI.WIN.MAIN.My.Resources
Imports System.Linq
Imports System.Data.SqlClient

Public Class frmCashInList
    Implements ILanguage

    Private oFormMode As FORM_MODE = FORM_MODE.FORM_MODE_VIEW
    Private oCashIn As New Finance.clsCashIn

#Region "Function"
    Private Sub Me_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.Text = CashIn.TITLE
        deDATEFrom.DateTime = Now
        deDATETo.DateTime = Now
        fn_LoadSecurity()

        Try
            grv.RestoreLayoutFromRegistry("HKEY_CURRENT_USER\Software\SIMRS\SW\" & sUserID & "\" & Me.Text)
        Catch ex As Exception

        End Try
    End Sub
    Private Sub Me_FormClosed(sender As System.Object, e As System.Windows.Forms.FormClosedEventArgs) Handles MyBase.FormClosed
        Try
            grv.SaveLayoutToRegistry("HKEY_CURRENT_USER\Software\SIMRS\SW\" & sUserID & "\" & Me.Text)
        Catch ex As Exception

        End Try
    End Sub
    Private Sub fn_LoadSecurity()
        Try
            Dim oOtority As New Setting.clsOtority
            Dim oUser As New Setting.clsUser

            Dim ds = (From x In oOtority.GetDataDetail _
                     Join y In oUser.GetData _
                     On x.KDOTORITY Equals y.KDOTORITY _
                     Where x.MODUL = "CASHIN" _
                     And y.KDUSER = sUserID _
                     Select x.ISADD, x.ISDELETE, x.ISUPDATE, x.ISPRINT, x.ISVIEW).FirstOrDefault

            Try
                picAdd.Enabled = ds.ISADD
                picDelete.Enabled = ds.ISDELETE
                picUpdate.Enabled = ds.ISUPDATE
                picPrint.Enabled = ds.ISPRINT
                picRefresh.Enabled = ds.ISVIEW

                If ds.ISVIEW = True Then
                    fn_LoadData()
                    fn_LoadLanguage()
                End If
            Catch oErr As Exception
                MsgBox(Statement.SecurityNotInstalled, MsgBoxStyle.Exclamation, Me.Text)

                picAdd.Enabled = False
                picDelete.Enabled = False
                picUpdate.Enabled = False
                picPrint.Enabled = False
                picRefresh.Enabled = False
            End Try
        Catch oErr As Exception
            MsgBox(Statement.SecurityNotInstalled, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Public Sub fn_LoadLanguage() Implements ILanguage.fn_LoadLanguage
        Try
            Me.Text = CashIn.TITLE

            fn_LoadLanguageMaster()
            fn_LoadLanguageDetail()
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Public Sub fn_LoadLanguageMaster()
        Try
            grv.Columns("KDCASHIN").Caption = CashIn.KDCASHIN
            'grv.Columns("KDPENDAFTARAN").Caption = CashIn.TANGGAL
            grv.Columns("PENJAMIN").Caption = CashIn.KDPENJAMIN
            'grv.Columns("REKAMMEDIS").Caption = CashIn.REKAMMEDIS
            'grv.Columns("NAMA").Caption = CashIn.SUBTOTAL
            grv.Columns("KDUSER").Caption = Caption.User
        Catch oErr As Exception

        End Try
    End Sub
    Public Sub fn_LoadLanguageDetail()
        Try
            'grv1.Columns("NOINVOICE").Caption = CashIn.DETAIL_NOINVOICE
            'grv1.Columns("AMOUNTPAYMENT").Caption = CashIn.DETAIL_AMOUNTPAYMENT
            'grv1.Columns("REMARKS").Caption = CashIn.DETAIL_REMARKS
        Catch oErr As Exception

        End Try
    End Sub
    Private Sub fn_LoadData()
        Try
            grv.Columns.Clear()
            grd.DataSource = Nothing

            Dim oConn As New SqlConnection
            Dim oComm As New SqlCommand
            Dim da As SqlDataAdapter
            Dim ds As New DataSet
            Dim SQL As String

            Dim sConn As String = Decrypt(My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Software\SIMRS\SW\", "Database", "").ToString())

            oConn = New SqlConnection(sConn)
            If oConn.State = ConnectionState.Closed Then
                oConn.Open()
            End If

            'SQL = "SELECT "
            'SQL &= "A.KDCASHIN "
            'SQL &= ",B.KDPENDAFTARAN "
            'SQL &= ",PENJAMIN = D.MEMO "
            'SQL &= ",TUJUAN = ISNULL((SELECT (SELECT BB.NAME_DISPLAY FROM M_DEPARTMENT BB WHERE AA.KDDEPARTMENT = BB.KDDEPARTMENT) FROM S_PENDAFTARAN_KUNJUNGANPOLI AA WHERE A.KDKUNJUNGAN = AA.KDKUNJUNGAN_POLI), (SELECT (SELECT BB.NAME_DISPLAY FROM M_RUANGRAWAT BB WHERE AA.KDRUANGRAWAT = BB.KDRUANGRAWAT) FROM S_PENDAFTARAN_KUNJUNGANRUANGAN AA WHERE A.KDKUNJUNGAN = AA.KDKUNJUNGAN_RUANGAN)) "
            'SQL &= ",DOKTER = ISNULL((SELECT (SELECT BB.NAME_DISPLAY FROM M_DOCTOR BB WHERE AA.KDDOCTOR = BB.KDDOCTOR) FROM S_PENDAFTARAN_KUNJUNGANPOLI AA WHERE A.KDKUNJUNGAN = AA.KDKUNJUNGAN_POLI), (SELECT (SELECT BB.NAME_DISPLAY FROM M_DOCTOR BB WHERE AA.KDDOCTOR = BB.KDDOCTOR) FROM S_PENDAFTARAN_KUNJUNGANRUANGAN AA WHERE A.KDKUNJUNGAN = AA.KDKUNJUNGAN_RUANGAN)) "
            'SQL &= ",REKAMMEDIS = B.KDCUSTOMER "
            'SQL &= ",NAMA = C.NAME_DISPLAY "
            'SQL &= ",A.KDUSER "
            'SQL &= "FROM "
            'SQL &= "F_CASHIN_H A "
            'SQL &= "INNER JOIN S_PENDAFTARAN_H B "
            'SQL &= "ON A.KDPENDAFTARAN = B.KDPENDAFTARAN "
            'SQL &= "INNER JOIN M_CUSTOMER C "
            'SQL &= "ON B.KDCUSTOMER = C.KDCUSTOMER "
            'SQL &= "INNER JOIN M_PENJAMIN D "
            'SQL &= "ON A.KDPENJAMIN = D.KDPENJAMIN "
            'SQL &= "WHERE CONVERT(VARCHAR(8), A.DATE, 112) >= '" & deDATEFrom.DateTime.ToString("yyyyMMdd") & "' AND CONVERT(VARCHAR(8), A.DATE, 112) <= '" & deDATETo.DateTime.ToString("yyyyMMdd") & "' "

            SQL = "SELECT "
            SQL &= "A.KDCASHIN "
            SQL &= ",PENJAMIN = B.MEMO "
            'SQL &= ",B.KDPENDAFTARAN "
            'SQL &= ",PENJAMIN = D.MEMO "
            'SQL &= ",TUJUAN = ISNULL((SELECT (SELECT BB.NAME_DISPLAY FROM M_DEPARTMENT BB WHERE AA.KDDEPARTMENT = BB.KDDEPARTMENT) FROM S_PENDAFTARAN_KUNJUNGANPOLI AA WHERE A.KDKUNJUNGAN = AA.KDKUNJUNGAN_POLI), (SELECT (SELECT BB.NAME_DISPLAY FROM M_RUANGRAWAT BB WHERE AA.KDRUANGRAWAT = BB.KDRUANGRAWAT) FROM S_PENDAFTARAN_KUNJUNGANRUANGAN AA WHERE A.KDKUNJUNGAN = AA.KDKUNJUNGAN_RUANGAN)) "
            'SQL &= ",DOKTER = ISNULL((SELECT (SELECT BB.NAME_DISPLAY FROM M_DOCTOR BB WHERE AA.KDDOCTOR = BB.KDDOCTOR) FROM S_PENDAFTARAN_KUNJUNGANPOLI AA WHERE A.KDKUNJUNGAN = AA.KDKUNJUNGAN_POLI), (SELECT (SELECT BB.NAME_DISPLAY FROM M_DOCTOR BB WHERE AA.KDDOCTOR = BB.KDDOCTOR) FROM S_PENDAFTARAN_KUNJUNGANRUANGAN AA WHERE A.KDKUNJUNGAN = AA.KDKUNJUNGAN_RUANGAN)) "
            'SQL &= ",REKAMMEDIS = B.KDCUSTOMER "
            'SQL &= ",NAMA = C.NAME_DISPLAY "
            SQL &= ",A.KDUSER "
            SQL &= "FROM "
            SQL &= "F_CASHIN_H A "
            SQL &= "INNER JOIN M_PENJAMIN B "
            SQL &= "ON A.KDPENJAMIN = B.KDPENJAMIN "
            SQL &= "WHERE CONVERT(VARCHAR(8), A.DATE, 112) >= '" & deDATEFrom.DateTime.ToString("yyyyMMdd") & "' AND CONVERT(VARCHAR(8), A.DATE, 112) <= '" & deDATETo.DateTime.ToString("yyyyMMdd") & "' "


            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "ALL")

            grd.MainView = grv
            grd.DataSource = ds.Tables("ALL")
            grd.ForceInitialize()

            fn_LoadFormatData()

            If oConn.State = ConnectionState.Open Then
                oConn.Close()
            End If
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub grv_MasterRowExpanded(sender As System.Object, e As DevExpress.XtraGrid.Views.Grid.CustomMasterRowEventArgs) Handles grv.MasterRowExpanded
        grv1 = TryCast(grv.GetDetailView(e.RowHandle, e.RelationIndex), DevExpress.XtraGrid.Views.Grid.GridView)

        fn_LoadFormatDataDetail()
        fn_LoadLanguageDetail()
    End Sub
    Private Sub fn_LoadFormatData()
        For iLoop As Integer = 0 To grv.Columns.Count - 1
            If grv.Columns(iLoop).ColumnType.Name = "Decimal" Then
                grv.Columns(iLoop).DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                grv.Columns(iLoop).DisplayFormat.FormatString = "{0:n2}"
                grv.Columns(iLoop).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
            ElseIf grv.Columns(iLoop).ColumnType.Name = "DateTime" Then
                grv.Columns(iLoop).DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
                grv.Columns(iLoop).DisplayFormat.FormatString = "{0:dd/MM/yyyy}"
            End If
        Next
    End Sub
    Private Sub fn_LoadFormatDataDetail()
        For iLoop As Integer = 0 To grv1.Columns.Count - 1
            If grv1.Columns(iLoop).ColumnType.Name = "Decimal" Then
                grv1.Columns(iLoop).DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                grv1.Columns(iLoop).DisplayFormat.FormatString = "{0:n2}"
                grv1.Columns(iLoop).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
            ElseIf grv1.Columns(iLoop).ColumnType.Name = "DateTime" Then
                grv1.Columns(iLoop).DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
                grv1.Columns(iLoop).DisplayFormat.FormatString = "{0:dd/MM/yyyy}"
            End If
        Next

        grv1.Columns("DATECREATED").Visible = False
        grv1.Columns("DATEUPDATED").Visible = False
        grv1.Columns("SEQ").Visible = False
        grv1.Columns("KDCASHIN").Visible = False
        grv1.Columns("F_CASHIN_H").Visible = False

        grv1.Columns("DATECREATED").OptionsColumn.ShowInCustomizationForm = False
        grv1.Columns("DATEUPDATED").OptionsColumn.ShowInCustomizationForm = False
        grv1.Columns("SEQ").OptionsColumn.ShowInCustomizationForm = False
        grv1.Columns("KDCASHIN").OptionsColumn.ShowInCustomizationForm = False
        grv1.Columns("F_CASHIN_H").OptionsColumn.ShowInCustomizationForm = False
    End Sub
    Private Sub MasterColumnChooserToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles MasterColumnChooserToolStripMenuItem.Click
        grv.ShowCustomization()
    End Sub
    Private Sub DetailColumnChooserToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles DetailColumnChooserToolStripMenuItem.Click
        grv1.ShowCustomization()
    End Sub
    Private Function fn_DeleteData(ByVal sKDCASHIN As String) As Boolean
        Try
            oCashIn.DeleteData(sKDCASHIN)

            fn_DeleteData = True
        Catch oErr As Exception
            fn_DeleteData = False
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Function
    Private Sub grv_RowStyle(sender As System.Object, e As DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs) Handles grv.RowStyle
        If grv.IsFilterRow(e.RowHandle) Then Exit Sub
        If CBool(grv.GetRowCellValue(e.RowHandle, "ISDELETE")) = True Then
            e.Appearance.BackColor = Color.LightGray
        Else
            e.Appearance.BackColor = Color.LightGreen
        End If
    End Sub
#End Region
#Region "Command Button"
    Private Sub frmMember_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        Select Case e.KeyCode
            Case Keys.A
                If e.Alt = True And picAdd.Enabled = True Then
                    picAdd_Click()
                End If
            Case Keys.E
                If e.Alt = True And picUpdate.Enabled = True Then
                    picUpdate_Click()
                End If
            Case Keys.D
                If e.Alt = True And picDelete.Enabled = True Then
                    picDelete_Click()
                End If
            Case Keys.P
                If e.Alt = True And picPrint.Enabled = True Then
                    picPrint_Click()
                End If
            Case Keys.R
                If e.Alt = True And picRefresh.Enabled = True Then
                    picRefresh_Click()
                End If
        End Select
    End Sub
    Private Sub grv_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs)
        If grv.GetFocusedRowCellValue("KDCASHIN") Is Nothing Then
            Exit Sub
        End If

        Dim frmCashIn As New frmCashIn
        Try
            frmCashIn.LoadMe(FORM_MODE.FORM_MODE_VIEW, grv.GetFocusedRowCellValue("KDCASHIN"))
            frmCashIn.ShowDialog(Me)
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub picAdd_Click() Handles picAdd.Click
        Dim frmCashIn As New frmCashIn
        Try
            frmCashIn.LoadMe(FORM_MODE.FORM_MODE_ADD)
            frmCashIn.ShowDialog(Me)
            fn_LoadSecurity()
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        Finally
            If Not frmCashIn Is Nothing Then frmCashIn.Dispose()
            frmCashIn = Nothing

            Dim rowHandle As Integer = grv.LocateByValue(rowHandle, grv.Columns("KDCASHIN"), sCode)
            If rowHandle > 0 Then grv.FocusedRowHandle = rowHandle

            If sStatusSave = "NEW" Then
                sStatusSave = "NONE"
                picAdd_Click()
            End If
        End Try
    End Sub
    Private Sub picUpdate_Click() Handles picUpdate.Click
        If grv.GetFocusedRowCellValue("KDCASHIN") Is Nothing Then
            Exit Sub
        End If
        Dim frmCashIn As New frmCashIn
        Try
            frmCashIn.LoadMe(FORM_MODE.FORM_MODE_EDIT, grv.GetFocusedRowCellValue("KDCASHIN"))
            frmCashIn.ShowDialog(Me)
            fn_LoadSecurity()
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        Finally
            If Not frmCashIn Is Nothing Then frmCashIn.Dispose()
            frmCashIn = Nothing

            Dim rowHandle As Integer = grv.LocateByValue(rowHandle, grv.Columns("KDCASHIN"), sCode)
            If rowHandle > 0 Then grv.FocusedRowHandle = rowHandle

            If sStatusSave = "NEW" Then
                sStatusSave = "NONE"
                picAdd_Click()
            End If
        End Try
    End Sub
    Private Sub picDelete_Click() Handles picDelete.Click
        If grv.GetFocusedRowCellValue("KDCASHIN") Is Nothing Then
            Exit Sub
        End If

        If MsgBox(Statement.DeleteQuestion, MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, Me.Text) = MsgBoxResult.No Then Exit Sub

        Dim frmPesanDelete As New frmPesanDelete
        frmPesanDelete.ShowDialog(Me)

        Dim KDCASHIN As String = grv.GetFocusedRowCellValue("KDCASHIN")

        If sDeletePesan <> "" Then
            Dim oDelete As New Setting.clsDelete

            oDelete.InsertData("PEMBAYARAN", "DELETE", grv.GetFocusedRowCellValue("KDCASHIN") & "Tanggal " & Now & " Oleh " & sUserID & " Alasan " & sDeletePesan, KDCASHIN)

            oCashIn.DeleteData(grv.GetFocusedRowCellValue("KDCASHIN"))
            fn_LoadSecurity()

            MsgBox(Statement.DeleteSuccess, MsgBoxStyle.Information, Me.Text)

        End If

        'If grv.GetFocusedRowCellValue("KDCASHIN") Is Nothing Then
        '    Exit Sub
        'End If
        'If MsgBox(Statement.DeleteQuestion, MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, Me.Text) = MsgBoxResult.No Then Exit Sub
        'If fn_DeleteData(grv.GetFocusedRowCellValue("KDCASHIN")) = False Then
        '    MsgBox(Statement.DeleteFail, MsgBoxStyle.Exclamation, Me.Text)
        '    Exit Sub
        'End If
        'MsgBox(Statement.DeleteSuccess, MsgBoxStyle.Information, Me.Text)
        'fn_LoadSecurity()
    End Sub
    Private Sub picPrint_Click() Handles picPrint.Click
        'Try
        '    If grv.GetFocusedRowCellValue("KDCASHIN") = String.Empty Then Exit Sub

        '    Dim rpt As New xtraCashIn

        '    rpt.ShowPrintMarginsWarning = False
        '    rpt.Watermark.Text = sWATERMARK
        '    Dim ds = oCashIn.GetData(grv.GetFocusedRowCellValue("KDCASHIN"))
        '    rpt.bindingSource.DataSource = ds
        '    Dim printTool As New DevExpress.XtraReports.UI.ReportPrintTool(rpt)
        '    printTool.ShowPreviewDialog(DevExpress.LookAndFeel.UserLookAndFeel.Default)
        'Catch oErr As Exception
        '    MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        'End Try
    End Sub
    Private Sub picRefresh_Click() Handles picRefresh.Click
        fn_LoadSecurity()
    End Sub
#End Region
End Class