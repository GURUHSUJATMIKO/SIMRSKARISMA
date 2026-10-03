Imports DataAccess
Imports UI.WIN.MAIN.My.Resources
Imports System.Linq
Imports System.Data.SqlClient

Public Class frmDIGITAL_IGD_01_AWALList
    Private oFormMode As FORM_MODE = FORM_MODE.FORM_MODE_VIEW
    Private oDIGITAL_IGD_01_AWAL As New Transaksi.clsDIGITAL_IGD_01_AWAL

#Region "Function"
    Private Sub Me_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.Text = "Reload Data IGD - List"

        deDATEFrom.DateTime = Now
        deDATETo.DateTime = Now

        fn_LoadSecurity()

    End Sub
    Private Sub Me_FormClosed(sender As System.Object, e As System.Windows.Forms.FormClosedEventArgs) Handles MyBase.FormClosed

    End Sub
    Private Sub fn_LoadSecurity()
        Try
            Dim oOtority As New Setting.clsOtority
            Dim oUser As New Setting.clsUser

            Dim ds = (From x In oOtority.GetDataDetail
                      Join y In oUser.GetData
                      On x.KDOTORITY Equals y.KDOTORITY
                      Where x.MODUL = "MEDREK_RJ" _
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
            SQL &= "TANGGAL = A.DATE "
            SQL &= ",A.KDAWALASESMENIGD "
            'SQL &= ",A.KDPENDAFTARAN "
            SQL &= ",BED = A.BAD "
            SQL &= ",A.NAMAPASIEN "
            SQL &= ",A.TRIAGE_1 "
            SQL &= ",A.TRIAGE_2 "
            SQL &= ",A.TRIAGE_3 "
            SQL &= ",A.TRIAGE_4 "
            SQL &= ",A.KDUSER "
            SQL &= "FROM "
            SQL &= "S_DIGITAL_IGD_01_AWAL A "
            SQL &= "WHERE CONVERT(VARCHAR(8), A.DATE, 112) >= '" & deDATEFrom.DateTime.ToString("yyyyMMdd") & "' AND CONVERT(VARCHAR(8), A.DATE, 112) <= '" & deDATETo.DateTime.ToString("yyyyMMdd") & "' "
            SQL &= "ORDER BY A.DATE DESC "

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

        grv.Columns("TRIAGE_1").VisibleIndex = -1
        grv.Columns("TRIAGE_2").VisibleIndex = -1
        grv.Columns("TRIAGE_3").VisibleIndex = -1
        grv.Columns("TRIAGE_4").VisibleIndex = -1

        grv.BestFitColumns()
    End Sub
    Private Sub MasterColumnChooserToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles MasterColumnChooserToolStripMenuItem.Click
        grv.ShowCustomization()
    End Sub
    Private Sub grv_FocusedRowChanged(ByVal sender As System.Object, ByVal e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs)
        If grv.GetFocusedRowCellValue("KDAWALASESMENIGD") Is Nothing Then
            fn_LoadSecurity()
            Exit Sub
        End If
    End Sub
    Private Sub grv_RowStyle(sender As System.Object, e As DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs) Handles grv.RowStyle
        If grv.IsFilterRow(e.RowHandle) Then Exit Sub
        If CBool(grv.GetRowCellValue(e.RowHandle, "TRIAGE_1")) = True Then
            e.Appearance.BackColor = Color.Red
        ElseIf CBool(grv.GetRowCellValue(e.RowHandle, "TRIAGE_2")) = True Then
            e.Appearance.BackColor = Color.Yellow
        ElseIf CBool(grv.GetRowCellValue(e.RowHandle, "TRIAGE_3")) = True Then
            e.Appearance.BackColor = Color.Green
        ElseIf CBool(grv.GetRowCellValue(e.RowHandle, "TRIAGE_4")) = True Then
            e.Appearance.BackColor = Color.Black
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
        If grv.GetFocusedRowCellValue("KDAWALASESMENIGD") Is Nothing Then
            Exit Sub
        End If

        Dim frmDIGITAL_IGD_01_AWAL As New frmDIGITAL_IGD_01_AWAL
        Try
            frmDIGITAL_IGD_01_AWAL.LoadMe(FORM_MODE.FORM_MODE_VIEW, grv.GetFocusedRowCellValue("KDAWALASESMENIGD"))
            frmDIGITAL_IGD_01_AWAL.ShowDialog(Me)
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub picAdd_Click() Handles picAdd.Click
        Dim frmDIGITAL_IGD_01_AWAL As New frmDIGITAL_IGD_01_AWAL
        Try
            frmDIGITAL_IGD_01_AWAL.LoadMe(FORM_MODE.FORM_MODE_ADD)
            frmDIGITAL_IGD_01_AWAL.ShowDialog(Me)

            If sStatusSave <> "NEW" Then
                fn_LoadSecurity()
            End If

        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        Finally
            If Not frmDIGITAL_IGD_01_AWAL Is Nothing Then frmDIGITAL_IGD_01_AWAL.Dispose()
            frmDIGITAL_IGD_01_AWAL = Nothing

            Dim rowHandle As Integer = grv.LocateByValue(rowHandle, grv.Columns("KDAWALASESMENIGD"), sCode)
            If rowHandle > 0 Then grv.FocusedRowHandle = rowHandle

            If sStatusSave = "NEW" Then
                sStatusSave = "NONE"
                picAdd_Click()
            End If
        End Try
    End Sub
    Private Sub picUpdate_Click() Handles picUpdate.Click
        If grv.GetFocusedRowCellValue("KDAWALASESMENIGD") Is Nothing Then
            Exit Sub
        End If
        If grv.GetFocusedRowCellValue("KDPENDAFTARAN") <> "" Then
            MsgBox("Tidak dapat dirubah sudah digabung", MsgBoxStyle.Information, Me.Text)
            Exit Sub
        End If

        Dim frmDIGITAL_IGD_01_AWAL As New frmDIGITAL_IGD_01_AWAL
        Try
            frmDIGITAL_IGD_01_AWAL.LoadMe(FORM_MODE.FORM_MODE_EDIT, grv.GetFocusedRowCellValue("KDAWALASESMENIGD"))
            frmDIGITAL_IGD_01_AWAL.ShowDialog(Me)
            fn_LoadSecurity()
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        Finally
            If Not frmDIGITAL_IGD_01_AWAL Is Nothing Then frmDIGITAL_IGD_01_AWAL.Dispose()
            frmDIGITAL_IGD_01_AWAL = Nothing

            Dim rowHandle As Integer = grv.LocateByValue(rowHandle, grv.Columns("KDAWALASESMENIGD"), sCode)
            If rowHandle > 0 Then grv.FocusedRowHandle = rowHandle

            If sStatusSave = "NEW" Then
                sStatusSave = "NONE"
                picAdd_Click()
            End If
        End Try
    End Sub
    Private Sub picDelete_Click() Handles picDelete.Click
        If grv.GetFocusedRowCellValue("KDAWALASESMENIGD") Is Nothing Then
            Exit Sub
        End If

        If grv.GetFocusedRowCellValue("KDPENDAFTARAN") <> "" Then
            MsgBox("Tidak dapat dirubah sudah digabung", MsgBoxStyle.Information, Me.Text)
            Exit Sub
        End If

        Dim KDDIGITAL_IGD_01_AWAL As String = grv.GetFocusedRowCellValue("KDAWALASESMENIGD")

        If MsgBox(Statement.DeleteQuestion, MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, Me.Text) = MsgBoxResult.No Then Exit Sub

        Dim frmPesanDelete As New frmPesanDelete
        frmPesanDelete.ShowDialog(Me)

        If sDeletePesan <> "" Then
            Dim oDelete As New Setting.clsDelete

            oDelete.InsertData("DIGITAL_IGD_01_AWAL", "DELETE", grv.GetFocusedRowCellValue("KDAWALASESMENIGD") & "Tanggal " & Now & " Oleh " & sUserID & " Alasan " & sDeletePesan, KDDIGITAL_IGD_01_AWAL)

            oDIGITAL_IGD_01_AWAL.DeleteData(grv.GetFocusedRowCellValue("KDAWALASESMENIGD"))
            fn_LoadSecurity()

            MsgBox(Statement.DeleteSuccess, MsgBoxStyle.Information, Me.Text)

        End If
    End Sub
    Private Sub picPrint_Click() Handles picPrint.Click
        'Try
        '    If grv.GetFocusedRowCellValue("KDAWALASESMENIGD") = String.Empty Then Exit Sub
        '    Dim ds = oDIGITAL_IGD_01_AWAL.GetData(grv.GetFocusedRowCellValue("KDAWALASESMENIGD"))

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
        '        oDIGITAL_IGD_01_AWAL.UpdateCetak(ds.KDDIGITAL_IGD_01_AWAL)
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