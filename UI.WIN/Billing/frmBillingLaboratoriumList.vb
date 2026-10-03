Imports DataAccess
Imports UI.WIN.MAIN.My.Resources
Imports System.Linq
Imports System.Data.SqlClient

Public Class frmBillingLaboratoriumList
    Implements ILanguage

    Private oFormMode As FORM_MODE = FORM_MODE.FORM_MODE_VIEW
    Private oBillingPoli As New Billing.clsBillingPoli
    Private oBillingRuangan As New Billing.clsBillingRuangan

#Region "Function"
    Private Sub Me_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.Text = BillingLaboratorium.TITLE

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

            Dim ds = (From x In oOtority.GetDataDetail
                      Join y In oUser.GetData
                      On x.KDOTORITY Equals y.KDOTORITY
                      Where x.MODUL = "BILLINGLABORATORIUM" _
                      And y.KDUSER = sUserID
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

                    Try
                        grv.RestoreLayoutFromRegistry("HKEY_CURRENT_USER\Software\SIMRS\SW\" & sUserID & "\" & Me.Text)
                    Catch ex As Exception

                    End Try
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
            Me.Text = BillingLaboratorium.TITLE

            grv.Columns("TANGGAL").Caption = BillingLaboratorium.TANGGAL
            grv.Columns("KDBILLING").Caption = BillingLaboratorium.KDBILLING_POLI
            grv.Columns("KDPENDAFTARAN").Caption = BillingLaboratorium.KDPENDAFTARAN
            grv.Columns("KDKUNJUNGAN").Caption = BillingLaboratorium.KDKUNJUNGAN_POLI
            grv.Columns("TUJUAN").Caption = BillingLaboratorium.TUJUAN
            grv.Columns("PENJAMIN").Caption = BillingLaboratorium.PENJAMIN
            grv.Columns("REKAMMEDIS").Caption = BillingLaboratorium.REKAMMEDIS
            grv.Columns("NAMA").Caption = BillingLaboratorium.NAMA
            grv.Columns("KDUSER").Caption = BillingLaboratorium.KDUSER

            fn_LoadLanguageMaster()
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Public Sub fn_LoadLanguageMaster()
        Try
            'grv.Columns("KDBILLING").Caption = BillingPoli.KDBillingPoli
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

            SQL = "SELECT "
            SQL &= "A.CATEGORY "
            SQL &= ",TANGGAL = A.DATE "
            SQL &= ",KDBILLING = A.KDBILLING_POLI "
            SQL &= ",C.KDPENDAFTARAN "
            SQL &= ",KDKUNJUNGAN = A.KDKUNJUNGAN_POLI "
            SQL &= ",TUJUAN = F.NAME_DISPLAY "
            SQL &= ",PENJAMIN = E.MEMO "
            SQL &= ",REKAMMEDIS = C.KDCUSTOMER "
            SQL &= ",NAMA = D.NAME_DISPLAY "
            SQL &= ",A.KDUSER "
            SQL &= "FROM "
            SQL &= "S_BILLING_POLI_H A "
            SQL &= "INNER JOIN S_PENDAFTARAN_KUNJUNGANPOLI B "
            SQL &= "ON A.KDKUNJUNGAN_POLI = B.KDKUNJUNGAN_POLI "
            SQL &= "INNER JOIN S_PENDAFTARAN_H C "
            SQL &= "ON B.KDPENDAFTARAN = C.KDPENDAFTARAN "
            SQL &= "INNER JOIN M_CUSTOMER D "
            SQL &= "ON C.KDCUSTOMER = D.KDCUSTOMER "
            SQL &= "INNER JOIN M_PENJAMIN E "
            SQL &= "ON B.KDPENJAMIN = E.KDPENJAMIN "
            SQL &= "INNER JOIN M_DEPARTMENT F "
            SQL &= "ON B.KDDEPARTMENT = F.KDDEPARTMENT "
            SQL &= "WHERE CONVERT(VARCHAR(8), A.DATE, 112) >= '" & deDATEFrom.DateTime.ToString("yyyyMMdd") & "' AND CONVERT(VARCHAR(8), A.DATE, 112) <= '" & deDATETo.DateTime.ToString("yyyyMMdd") & "' "
            SQL &= "AND A.CATEGORY = 2 "

            SQL &= "UNION "

            SQL &= "SELECT "
            SQL &= "A.CATEGORY "
            SQL &= ",TANGGAL = A.DATE "
            SQL &= ",KDBILLING = A.KDBILLING_RUANGAN "
            SQL &= ",C.KDPENDAFTARAN "
            SQL &= ",KDKUNJUNGAN = A.KDKUNJUNGAN_RUANGAN "
            SQL &= ",TUJUAN = F.NAME_DISPLAY "
            SQL &= ",PENJAMIN = E.MEMO "
            SQL &= ",REKAMMEDIS = C.KDCUSTOMER "
            SQL &= ",NAMA = D.NAME_DISPLAY "
            SQL &= ",A.KDUSER "
            SQL &= "FROM "
            SQL &= "S_BILLING_Ruangan_H A "
            SQL &= "INNER JOIN S_PENDAFTARAN_KUNJUNGANRUANGAN B "
            SQL &= "ON A.KDKUNJUNGAN_Ruangan = B.KDKUNJUNGAN_RUANGAN "
            SQL &= "INNER JOIN S_PENDAFTARAN_H C "
            SQL &= "ON B.KDPENDAFTARAN = C.KDPENDAFTARAN "
            SQL &= "INNER JOIN M_CUSTOMER D "
            SQL &= "ON C.KDCUSTOMER = D.KDCUSTOMER "
            SQL &= "INNER JOIN M_PENJAMIN E "
            SQL &= "ON B.KDPENJAMIN = E.KDPENJAMIN "
            SQL &= "INNER JOIN M_RUANGRAWAT F "
            SQL &= "ON B.KDRUANGRAWAT = F.KDRUANGRAWAT "
            SQL &= "WHERE CONVERT(VARCHAR(8), A.DATE, 112) >= '" & deDATEFrom.DateTime.ToString("yyyyMMdd") & "' AND CONVERT(VARCHAR(8), A.DATE, 112) <= '" & deDATETo.DateTime.ToString("yyyyMMdd") & "' "
            SQL &= "AND A.CATEGORY = 3 "

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
    Private Sub fn_LoadFormatData()
        For iLoop As Integer = 0 To grv.Columns.Count - 1
            If grv.Columns(iLoop).ColumnType.Name = "Decimal" Then
                grv.Columns(iLoop).DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                grv.Columns(iLoop).DisplayFormat.FormatString = "{0:n2}"
                grv.Columns(iLoop).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
            ElseIf grv.Columns(iLoop).ColumnType.Name = "DateTime" Then
                grv.Columns(iLoop).DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
                grv.Columns(iLoop).DisplayFormat.FormatString = "{0:dd/MM/yyyy HH:mm:ss}"
            End If
        Next
        grv.BestFitColumns()

        grv.Columns("CATEGORY").VisibleIndex = -1
    End Sub
    Private Sub MasterColumnChooserToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles MasterColumnChooserToolStripMenuItem.Click
        grv.ShowCustomization()
    End Sub
    Private Sub grv_FocusedRowChanged(ByVal sender As System.Object, ByVal e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs)
        If grv.GetFocusedRowCellValue("KDBILLING") Is Nothing Then
            fn_LoadSecurity()
            Exit Sub
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
    Private Sub grv_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles grv.DoubleClick
        If grv.GetFocusedRowCellValue("KDBILLING") Is Nothing Then
            Exit Sub
        End If

        Dim frmBillingLaboratorium As New frmBillingLaboratorium
        Try
            frmBillingLaboratorium.LoadMe(FORM_MODE.FORM_MODE_VIEW, grv.GetFocusedRowCellValue("CATEGORY"), grv.GetFocusedRowCellValue("KDBILLING"))
            frmBillingLaboratorium.ShowDialog(Me)
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub picAdd_Click() Handles picAdd.Click
        Dim frmBillingLaboratorium As New frmBillingLaboratorium
        Try
            frmBillingLaboratorium.LoadMe(FORM_MODE.FORM_MODE_ADD, 0)
            frmBillingLaboratorium.ShowDialog(Me)

            If sStatusSave <> "NEW" Then
                fn_LoadSecurity()
            End If

        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        Finally
            If Not frmBillingLaboratorium Is Nothing Then frmBillingLaboratorium.Dispose()
            frmBillingLaboratorium = Nothing

            Dim rowHandle As Integer = grv.LocateByValue(rowHandle, grv.Columns("KDBILLING"), sCode)
            If rowHandle > 0 Then grv.FocusedRowHandle = rowHandle

            If sStatusSave = "NEW" Then
                sStatusSave = "NONE"
                picAdd_Click()
            End If
        End Try
    End Sub
    Private Sub picUpdate_Click() Handles picUpdate.Click
        If grv.GetFocusedRowCellValue("KDBILLING") Is Nothing Then
            Exit Sub
        End If
        Dim frmBillingLaboratorium As New frmBillingLaboratorium
        Try
            frmBillingLaboratorium.LoadMe(FORM_MODE.FORM_MODE_EDIT, grv.GetFocusedRowCellValue("CATEGORY"), grv.GetFocusedRowCellValue("KDBILLING"))
            frmBillingLaboratorium.ShowDialog(Me)
            fn_LoadSecurity()
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        Finally
            If Not frmBillingLaboratorium Is Nothing Then frmBillingLaboratorium.Dispose()
            frmBillingLaboratorium = Nothing

            Dim rowHandle As Integer = grv.LocateByValue(rowHandle, grv.Columns("KDBILLING"), sCode)
            If rowHandle > 0 Then grv.FocusedRowHandle = rowHandle

            If sStatusSave = "NEW" Then
                sStatusSave = "NONE"
                picAdd_Click()
            End If
        End Try
    End Sub
    Private Sub picDelete_Click() Handles picDelete.Click
        If grv.GetFocusedRowCellValue("KDBILLING") Is Nothing Then
            Exit Sub
        End If

        If grv.GetFocusedRowCellValue("CATEGORY") = 2 Then
            Dim KDBillingPoli As String = grv.GetFocusedRowCellValue("KDBILLING")

            If MsgBox(Statement.DeleteQuestion, MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, Me.Text) = MsgBoxResult.No Then Exit Sub

            Dim frmPesanDelete As New frmPesanDelete
            frmPesanDelete.ShowDialog(Me)

            If sDelete = True Then
                oBillingPoli.DeleteData(grv.GetFocusedRowCellValue("KDBILLING"))
                fn_LoadSecurity()

                Dim oDelete As New Setting.clsDelete

                oDelete.InsertData("BillingPoli", "DELETE", grv.GetFocusedRowCellValue("NoSEP") & "Tanggal " & Now & " Oleh " & sUserID, KDBillingPoli)

                MsgBox(Statement.DeleteSuccess, MsgBoxStyle.Information, Me.Text)

            End If
        ElseIf grv.GetFocusedRowCellValue("CATEGORY") = 3 Then
            Dim KDBillingPoli As String = grv.GetFocusedRowCellValue("KDBILLING")

            If MsgBox(Statement.DeleteQuestion, MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, Me.Text) = MsgBoxResult.No Then Exit Sub

            Dim frmPesanDelete As New frmPesanDelete
            frmPesanDelete.ShowDialog(Me)

            If sDelete = True Then
                oBillingRuangan.DeleteData(grv.GetFocusedRowCellValue("KDBILLING"))
                fn_LoadSecurity()

                Dim oDelete As New Setting.clsDelete

                oDelete.InsertData("BillingPoli", "DELETE", grv.GetFocusedRowCellValue("NoSEP") & "Tanggal " & Now & " Oleh " & sUserID, KDBillingPoli)

                MsgBox(Statement.DeleteSuccess, MsgBoxStyle.Information, Me.Text)

            End If
        End If
    End Sub
    Private Sub picPrint_Click() Handles picPrint.Click
        'Try
        '    If grv.GetFocusedRowCellValue("KDBILLING") = String.Empty Then Exit Sub
        '    Dim ds = oBillingPoli.GetData(grv.GetFocusedRowCellValue("KDBILLING"))

        '    sCetakSEP = False

        '    If fn_CariSEP() = "ADA" Then
        '        Dim rpt As New xtraSEP
        '        rpt.bindingSource.DataSource = ds
        '        Dim printTool As New DevExpress.XtraReports.UI.ReportPrintTool(rpt)
        '        printTool.ShowPreviewDialog(DevExpress.LookAndFeel.UserLookAndFeel.Default)
        '    Else
        '        Dim rpt As New xtraUmum
        '        rpt.bindingSource.DataSource = ds
        '        Dim printTool As New DevExpress.XtraReports.UI.ReportPrintTool(rpt)
        '        printTool.ShowPreviewDialog(DevExpress.LookAndFeel.UserLookAndFeel.Default)
        '    End If

        '    If sCetakSEP = True Then
        '        oBillingPoli.UpdateCetak(ds.KDBillingPoli)
        '    End If
        'Catch oErr As Exception
        '    MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        'End Try
    End Sub
    Private Sub picRefresh_Click() Handles picRefresh.Click
        fn_LoadSecurity()
    End Sub
#End Region
End Class