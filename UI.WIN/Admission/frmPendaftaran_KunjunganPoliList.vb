Imports DataAccess
Imports UI.WIN.MAIN.My.Resources
Imports System.Linq
Imports Newtonsoft.Json.Linq

Public Class frmPendaftaran_KunjunganPoliList
    Implements ILanguage

    Private oFormMode As FORM_MODE = FORM_MODE.FORM_MODE_VIEW
    Private oPendaftaran_KunjunganPoli As New Admission.clsPendaftaran_KunjunganPoli

#Region "Function"
    Private Sub Me_Load() Handles Me.Load
        Me.Text = Pendaftaran_KunjunganPoli.TITLE
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
                      Where x.MODUL = "KUNJUNGAN_POLI" _
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
            Me.Text = Pendaftaran_KunjunganPoli.TITLE

            grv.Columns("KDKUNJUNGAN_POLI").Caption = Pendaftaran_KunjunganPoli.KDKUNJUNGAN_POLI
            grv.Columns("KDPENDAFTARAN").Caption = Pendaftaran.KDPENDAFTARAN
            grv.Columns("KDCUSTOMER").Caption = Customer.KDCUSTOMER
            grv.Columns("PASIEN").Caption = Customer.NAME_DISPLAY
            grv.Columns("NAME_DISPLAY").Caption = Department.NAME_DISPLAY
            grv.Columns("DATE_MASUK").Caption = Pendaftaran_KunjunganPoli.DATE_MASUK
            'grv.Columns("DATE_KELUAR").Caption = Pendaftaran_KunjunganPoli.DATE_KELUAR
            grv.Columns("MEMO").Caption = Pendaftaran_KunjunganPoli.MEMO
            grv.Columns("ISCHEKED").Caption = Pendaftaran_KunjunganPoli.ISCHEKED
            grv.Columns("KDUSER").Caption = User.KDUSER
            grv.Columns("PENJAMIN").Caption = Penjamin.MEMO
            grv.Columns("DOKTER").Caption = Pendaftaran.KDDOCTOR

            ColumnChooserToolStripMenuItem.Text = Caption.ColumnChooser
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadData()
        Try
            Dim ds = From x In oPendaftaran_KunjunganPoli.GetData(deDATEFrom.DateTime, deDATETo.DateTime)
                     Select x.KDKUNJUNGAN_POLI, x.KDPENDAFTARAN, PENJAMIN = x.M_PENJAMIN.MEMO, x.S_PENDAFTARAN_H.KDCUSTOMER, PASIEN = x.S_PENDAFTARAN_H.M_CUSTOMER.NAME_DISPLAY, DOKTER = x.M_DOCTOR.NAME_DISPLAY, x.M_DEPARTMENT.NAME_DISPLAY, x.DATE_MASUK, x.MEMO, x.ISCHEKED, x.KDUSER

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

        'grv.Columns("KDKUNJUNGAN_POLI").Visible = False
        'grv.Columns("KDKUNJUNGAN_POLI").OptionsColumn.ShowInCustomizationForm = False
    End Sub
    Private Sub ColumnChooserToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles ColumnChooserToolStripMenuItem.Click
        grv.ShowCustomization()
    End Sub
    Private Function fn_DeleteData(ByVal sKDKUNJUNGAN_POLI As String) As Boolean
        Try
            oPendaftaran_KunjunganPoli.DeleteData(sKDKUNJUNGAN_POLI)
            fn_DeleteData = True
        Catch oErr As Exception
            fn_DeleteData = False
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Function
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
        If grv.GetFocusedRowCellValue("KDKUNJUNGAN_POLI") Is Nothing Then
            Exit Sub
        End If

        Dim frmPendaftaran_KunjunganPoli As New frmPendaftaran_KunjunganPoli
        Try
            frmPendaftaran_KunjunganPoli.LoadMe(FORM_MODE.FORM_MODE_VIEW, grv.GetFocusedRowCellValue("KDKUNJUNGAN_POLI"))
            frmPendaftaran_KunjunganPoli.ShowDialog(Me)
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub picAdd_Click() Handles picAdd.Click
        Dim frmPendaftaran_KunjunganPoli As New frmPendaftaran_KunjunganPoli
        Try
            frmPendaftaran_KunjunganPoli.LoadMe(FORM_MODE.FORM_MODE_ADD)
            frmPendaftaran_KunjunganPoli.ShowDialog(Me)
            fn_LoadSecurity()
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        Finally
            If Not frmPendaftaran_KunjunganPoli Is Nothing Then frmPendaftaran_KunjunganPoli.Dispose()
            frmPendaftaran_KunjunganPoli = Nothing

            Dim rowHandle As Integer = grv.LocateByValue(rowHandle, grv.Columns("MEMO"), sCode)
            If rowHandle > 0 Then grv.FocusedRowHandle = rowHandle

            If sStatusSave = "NEW" Then
                sStatusSave = "NONE"
                picAdd_Click()
            End If
        End Try
    End Sub
    Private Sub picUpdate_Click() Handles picUpdate.Click
        If grv.GetFocusedRowCellValue("KDKUNJUNGAN_POLI") Is Nothing Then
            Exit Sub
        End If
        If grv.GetFocusedRowCellValue("MEMO") = "POLI 1" Then
            MsgBox("Tidak Dapat di Edit", MsgBoxStyle.Exclamation, Me.Text)
            Exit Sub
        End If

        Dim frmPendaftaran_KunjunganPoli As New frmPendaftaran_KunjunganPoli
        Try
            frmPendaftaran_KunjunganPoli.LoadMe(FORM_MODE.FORM_MODE_EDIT, grv.GetFocusedRowCellValue("KDKUNJUNGAN_POLI"))
            frmPendaftaran_KunjunganPoli.ShowDialog(Me)
            fn_LoadSecurity()
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        Finally
            If Not frmPendaftaran_KunjunganPoli Is Nothing Then frmPendaftaran_KunjunganPoli.Dispose()
            frmPendaftaran_KunjunganPoli = Nothing

            Dim rowHandle As Integer = grv.LocateByValue(rowHandle, grv.Columns("MEMO"), sCode)
            If rowHandle > 0 Then grv.FocusedRowHandle = rowHandle

            If sStatusSave = "NEW" Then
                sStatusSave = "NONE"
                picAdd_Click()
            End If
        End Try
    End Sub
    Private Sub picDelete_Click() Handles picDelete.Click
        If grv.GetFocusedRowCellValue("KDKUNJUNGAN_POLI") Is Nothing Then
            Exit Sub
        End If
        If grv.GetFocusedRowCellValue("MEMO") = "POLI 1" Then
            MsgBox("Tidak Dapat di Delete", MsgBoxStyle.Exclamation, Me.Text)
            Exit Sub
        End If

        If MsgBox(Statement.DeleteQuestion, MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, Me.Text) = MsgBoxResult.No Then Exit Sub

        If fn_DeleteData(grv.GetFocusedRowCellValue("KDKUNJUNGAN_POLI")) = False Then
            MsgBox(Statement.DeleteFail, MsgBoxStyle.Exclamation, Me.Text)
            Exit Sub
        End If
        MsgBox(Statement.DeleteSuccess, MsgBoxStyle.Information, Me.Text)
        fn_LoadSecurity()

    End Sub
    Private Sub picPrint_Click() Handles picPrint.Click
        'Try
        '    If grv.GetFocusedRowCellValue("KDKUNJUNGAN_POLI") = String.Empty Then Exit Sub

        '    Dim rpt As New xtraPendaftaran_KunjunganPoli

        '    rpt.ShowPrintMarginsWarning = False
        '    rpt.Watermark.Text = sWATERMARK
        '    Dim ds = oPendaftaran_KunjunganPoli.GetData(grv.GetFocusedRowCellValue("KDKUNJUNGAN_POLI"))
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