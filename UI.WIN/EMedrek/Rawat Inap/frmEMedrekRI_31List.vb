Imports DataAccess
Imports UI.WIN.MAIN.My.Resources
Imports System.Linq

Public Class frmEMedrekRI_31List
    Private oFormMode As FORM_MODE = FORM_MODE.FORM_MODE_VIEW
    Private oS_DIGITAL_RI_31 As New Transaksi.clsDigital_SBAR
    Private sKDCUSTOMER As String = String.Empty
    Private sKDREG As String = String.Empty
    Private sNAMAPASIEN As String = String.Empty
    Private sTANGGALLAHIR As DateTime = Now
    Private sTANGGALDATANG As DateTime = Now
    Private sTUJUAN As String = String.Empty
    Private sKDDOCTOR As String = String.Empty
    Private sJENISKELAMIN As String = String.Empty

#Region "Function"
    Public Sub fn_LoadMe(ByVal KDREG As String, ByVal JENISKELAMIN As String, ByVal KDDOCTOR As String, ByVal KDCUSTOMER As String, ByVal NAMAPASIEN As String, ByVal TANGGALLAHIR As DateTime, ByVal TUJUAN As String, ByVal TANGGALDATANG As DateTime)
        sKDCUSTOMER = KDCUSTOMER
        sKDREG = KDREG
        sNAMAPASIEN = NAMAPASIEN
        sTANGGALLAHIR = TANGGALLAHIR
        sTANGGALDATANG = TANGGALDATANG
        sTUJUAN = TUJUAN
        sKDDOCTOR = KDDOCTOR
        sJENISKELAMIN = JENISKELAMIN
    End Sub
    Private Sub Me_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
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
            Dim ds = From x In oS_DIGITAL_RI_31.GetDataByRM(sKDCUSTOMER)
                     Select x.KDPINDAHAN, x.KDREG, TANGGAL = x.DATE, DARIRUANGAN = x.DEPARTMENT2_NAME_DISPLAY, KERUANGAN = x.DEPARTMENT_NAME_DISPLAY, x.KDUSER

            grd.DataSource = ds.ToList

            fn_LoadFormatData()
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
                grv.Columns(iLoop).DisplayFormat.FormatString = "{0:dd/MM/yyyy}"
            End If
        Next
    End Sub
    Private Function fn_DeleteData(ByVal sKDS_DIGITAL_RI_31 As String) As Boolean
        Try
            oS_DIGITAL_RI_31.DeleteData(sKDS_DIGITAL_RI_31)

            fn_DeleteData = True
        Catch oErr As Exception
            fn_DeleteData = False
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Function
    Private Sub grv_RowStyle(sender As System.Object, e As DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs) Handles grv.RowStyle
        'If grv.IsFilterRow(e.RowHandle) Then Exit Sub
        'If CBool(grv.GetRowCellValue(e.RowHandle, "ISDELETE")) = True Then
        '    e.Appearance.BackColor = Color.LightGray
        'Else
        '    e.Appearance.BackColor = Color.LightGreen
        'End If
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
        If grv.GetFocusedRowCellValue("KDPINDAHAN") Is Nothing Then
            Exit Sub
        End If

        Dim frmEMedrekRI_31 As New frmEMedrekRI_31
        Try
            frmEMedrekRI_31.LoadMe(FORM_MODE.FORM_MODE_VIEW, "", sKDREG, sKDDOCTOR, sKDCUSTOMER, sNAMAPASIEN, sTANGGALLAHIR, sTUJUAN, sTANGGALDATANG, grv.GetFocusedRowCellValue("KDPINDAHAN"))
            frmEMedrekRI_31.ShowDialog(Me)
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub picAdd_Click() Handles picAdd.Click
        Dim frmEMedrekRI_31 As New frmEMedrekRI_31
        Try
            frmEMedrekRI_31.LoadMe(FORM_MODE.FORM_MODE_ADD, "", sKDREG, sKDDOCTOR, sKDCUSTOMER, sNAMAPASIEN, sTANGGALLAHIR, sTUJUAN, sTANGGALDATANG, "")
            frmEMedrekRI_31.ShowDialog(Me)
            fn_LoadSecurity()
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        Finally
            If Not frmEMedrekRI_31 Is Nothing Then frmEMedrekRI_31.Dispose()
            frmEMedrekRI_31 = Nothing

            Dim rowHandle As Integer = grv.LocateByValue(rowHandle, grv.Columns("KDPINDAHAN"), sCode)
            If rowHandle > 0 Then grv.FocusedRowHandle = rowHandle

            If sStatusSave = "NEW" Then
                sStatusSave = "NONE"
                picAdd_Click()
            End If
        End Try
    End Sub
    Private Sub picUpdate_Click() Handles picUpdate.Click
        If grv.GetFocusedRowCellValue("KDPINDAHAN") Is Nothing Then
            Exit Sub
        End If
        Dim frmEMedrekRI_31 As New frmEMedrekRI_31
        Try
            frmEMedrekRI_31.LoadMe(FORM_MODE.FORM_MODE_EDIT, "", sKDREG, sKDDOCTOR, sKDCUSTOMER, sNAMAPASIEN, sTANGGALLAHIR, sTUJUAN, sTANGGALDATANG, grv.GetFocusedRowCellValue("KDPINDAHAN"))
            frmEMedrekRI_31.ShowDialog(Me)
            fn_LoadSecurity()
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        Finally
            If Not frmEMedrekRI_31 Is Nothing Then frmEMedrekRI_31.Dispose()
            frmEMedrekRI_31 = Nothing

            Dim rowHandle As Integer = grv.LocateByValue(rowHandle, grv.Columns("KDPINDAHAN"), sCode)
            If rowHandle > 0 Then grv.FocusedRowHandle = rowHandle

            If sStatusSave = "NEW" Then
                sStatusSave = "NONE"
                picAdd_Click()
            End If
        End Try
    End Sub
    Private Sub picDelete_Click() Handles picDelete.Click
        If grv.GetFocusedRowCellValue("KDPINDAHAN") Is Nothing Then
            Exit Sub
        End If
        If MsgBox(Statement.DeleteQuestion, MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, Me.Text) = MsgBoxResult.No Then Exit Sub
        If fn_DeleteData(grv.GetFocusedRowCellValue("KDPINDAHAN")) = False Then
            MsgBox(Statement.DeleteFail, MsgBoxStyle.Exclamation, Me.Text)
            Exit Sub
        End If
        MsgBox(Statement.DeleteSuccess, MsgBoxStyle.Information, Me.Text)
        fn_LoadSecurity()
    End Sub
    Private Sub picPrint_Click() Handles picPrint.Click
        Try
            If grv.GetFocusedRowCellValue("KDPINDAHAN") = String.Empty Then Exit Sub

            Dim rpt As New xtraReportEMedrekRI_31

            rpt.ShowPrintMarginsWarning = False
            rpt.Watermark.Text = sWATERMARK
            Dim ds = oS_DIGITAL_RI_31.GetData(grv.GetFocusedRowCellValue("KDPINDAHAN"))
            rpt.BindingSource1.DataSource = ds

            NAMA = sNAMAPASIEN
            TANGGALLAHIR = sTANGGALDATANG.ToString("dd-MM-yyyy")
            JENISKELAMIN = sJENISKELAMIN

            Dim printTool As New DevExpress.XtraReports.UI.ReportPrintTool(rpt)
            printTool.ShowPreviewDialog(DevExpress.LookAndFeel.UserLookAndFeel.Default)
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub picRefresh_Click() Handles picRefresh.Click
        fn_LoadSecurity()
    End Sub
    Private Sub CopyHandOverToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CopyHandOverToolStripMenuItem.Click
        If grv.GetFocusedRowCellValue("KDPINDAHAN") = String.Empty Then Exit Sub

        Dim frmEMedrekRI_31 As New frmEMedrekRI_31
        Try
            frmEMedrekRI_31.LoadMe(FORM_MODE.FORM_MODE_ADD, grv.GetFocusedRowCellValue("KDPINDAHAN"), sKDREG, sKDDOCTOR, sKDCUSTOMER, sNAMAPASIEN, sTANGGALLAHIR, sTUJUAN, sTANGGALDATANG, "")
            frmEMedrekRI_31.ShowDialog(Me)
            fn_LoadSecurity()
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        Finally
            If Not frmEMedrekRI_31 Is Nothing Then frmEMedrekRI_31.Dispose()
            frmEMedrekRI_31 = Nothing

            Dim rowHandle As Integer = grv.LocateByValue(rowHandle, grv.Columns("KDPINDAHAN"), sCode)
            If rowHandle > 0 Then grv.FocusedRowHandle = rowHandle

            If sStatusSave = "NEW" Then
                sStatusSave = "NONE"
                picAdd_Click()
            End If
        End Try
    End Sub
#End Region
End Class