Imports DataAccess
Imports UI.WIN.MAIN.My.Resources
Imports System.Linq

Public Class frmPendaftaran_KunjunganRuanganList
    Implements ILanguage

    Private oFormMode As FORM_MODE = FORM_MODE.FORM_MODE_VIEW
    Private oPendaftaran_KunjunganRuangan As New Admission.clsPendaftaran_KunjunganRuangan

#Region "Function"
    Private Sub Me_Load() Handles Me.Load
        Me.Text = Pendaftaran_KunjunganRuangan.TITLE

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
                      Where x.MODUL = "KUNJUNGAN_RUANGAN" _
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
            Me.Text = Pendaftaran_KunjunganRuangan.TITLE

            grv.Columns("KDKUNJUNGAN_RUANGAN").Caption = Pendaftaran_KunjunganRuangan.KDKUNJUNGAN_POLI
            grv.Columns("KDPENDAFTARAN").Caption = Pendaftaran.KDPENDAFTARAN
            grv.Columns("KDCUSTOMER").Caption = Customer.KDCUSTOMER
            grv.Columns("PASIEN").Caption = Customer.NAME_DISPLAY
            grv.Columns("NAME_DISPLAY").Caption = Department.NAME_DISPLAY
            grv.Columns("DATE_MASUK").Caption = Pendaftaran_KunjunganRuangan.DATE_MASUK
            grv.Columns("DATE_KELUAR").Caption = Pendaftaran_KunjunganRuangan.DATE_KELUAR
            grv.Columns("MEMO").Caption = Pendaftaran_KunjunganRuangan.MEMO
            grv.Columns("ISCHEKED").Caption = Pendaftaran_KunjunganRuangan.ISCHEKED
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
            Dim ds = From x In oPendaftaran_KunjunganRuangan.GetData(deDATEFrom.DateTime, deDATETo.DateTime)
                     Select x.KDKUNJUNGAN_RUANGAN, x.SEQ, x.KDPENDAFTARAN, x.S_PENDAFTARAN_H.KDCUSTOMER, PENJAMIN = x.M_PENJAMIN.MEMO, PASIEN = x.S_PENDAFTARAN_H.M_CUSTOMER.NAME_DISPLAY, DOKTER = x.M_DOCTOR.NAME_DISPLAY, x.M_RUANGRAWAT.NAME_DISPLAY, x.DATE_MASUK, x.DATE_KELUAR, x.MEMO, x.ISCHEKED, x.KDUSER

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

        grv.Columns("KDKUNJUNGAN_RUANGAN").Visible = False
        grv.Columns("KDKUNJUNGAN_RUANGAN").OptionsColumn.ShowInCustomizationForm = False
        grv.Columns("SEQ").Visible = False
        grv.Columns("SEQ").OptionsColumn.ShowInCustomizationForm = False
    End Sub
    Private Sub ColumnChooserToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles ColumnChooserToolStripMenuItem.Click
        grv.ShowCustomization()
    End Sub
    Private Function fn_DeleteData(ByVal sKDKUNJUNGAN_RUANGAN As String) As Boolean
        Try
            oPendaftaran_KunjunganRuangan.DeleteData(sKDKUNJUNGAN_RUANGAN)
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
        If grv.GetFocusedRowCellValue("KDKUNJUNGAN_RUANGAN") Is Nothing Then
            Exit Sub
        End If

        Dim frmPendaftaran_KunjunganRuangan As New frmPendaftaran_KunjunganRuangan
        Try
            frmPendaftaran_KunjunganRuangan.LoadMe(FORM_MODE.FORM_MODE_VIEW, grv.GetFocusedRowCellValue("KDKUNJUNGAN_RUANGAN"))
            frmPendaftaran_KunjunganRuangan.ShowDialog(Me)
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub picAdd_Click() Handles picAdd.Click
        Dim frmPendaftaran_KunjunganRuangan As New frmPendaftaran_KunjunganRuangan
        Try
            frmPendaftaran_KunjunganRuangan.LoadMe(FORM_MODE.FORM_MODE_ADD)
            frmPendaftaran_KunjunganRuangan.ShowDialog(Me)
            fn_LoadSecurity()
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        Finally
            If Not frmPendaftaran_KunjunganRuangan Is Nothing Then frmPendaftaran_KunjunganRuangan.Dispose()
            frmPendaftaran_KunjunganRuangan = Nothing

            Dim rowHandle As Integer = grv.LocateByValue(rowHandle, grv.Columns("MEMO"), sCode)
            If rowHandle > 0 Then grv.FocusedRowHandle = rowHandle

            If sStatusSave = "NEW" Then
                sStatusSave = "NONE"
                picAdd_Click()
            End If
        End Try
    End Sub
    Private Sub picUpdate_Click() Handles picUpdate.Click
        If grv.GetFocusedRowCellValue("KDKUNJUNGAN_RUANGAN") Is Nothing Then
            Exit Sub
        End If
        If grv.GetFocusedRowCellValue("MEMO") = "RUANGAN 1" Then
            MsgBox("Tidak Dapat di Edit", MsgBoxStyle.Exclamation, Me.Text)
            Exit Sub
        End If

        Dim frmPendaftaran_KunjunganRuangan As New frmPendaftaran_KunjunganRuangan
        Try
            frmPendaftaran_KunjunganRuangan.LoadMe(FORM_MODE.FORM_MODE_EDIT, grv.GetFocusedRowCellValue("KDKUNJUNGAN_RUANGAN"))
            frmPendaftaran_KunjunganRuangan.ShowDialog(Me)
            fn_LoadSecurity()
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        Finally
            If Not frmPendaftaran_KunjunganRuangan Is Nothing Then frmPendaftaran_KunjunganRuangan.Dispose()
            frmPendaftaran_KunjunganRuangan = Nothing

            Dim rowHandle As Integer = grv.LocateByValue(rowHandle, grv.Columns("MEMO"), sCode)
            If rowHandle > 0 Then grv.FocusedRowHandle = rowHandle

            If sStatusSave = "NEW" Then
                sStatusSave = "NONE"
                picAdd_Click()
            End If
        End Try
    End Sub
    Private Sub picDelete_Click() Handles picDelete.Click
        If grv.GetFocusedRowCellValue("KDKUNJUNGAN_RUANGAN") Is Nothing Then
            Exit Sub
        End If
        If grv.GetFocusedRowCellValue("MEMO") = "RUANGAN 1" Then
            MsgBox("Tidak Dapat di Delete", MsgBoxStyle.Exclamation, Me.Text)
            Exit Sub
        End If
        If MsgBox(Statement.DeleteQuestion, MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, Me.Text) = MsgBoxResult.No Then Exit Sub

        If fn_DeleteData(grv.GetFocusedRowCellValue("KDKUNJUNGAN_RUANGAN")) = False Then
            MsgBox(Statement.DeleteFail, MsgBoxStyle.Exclamation, Me.Text)
            Exit Sub
        End If
        MsgBox(Statement.DeleteSuccess, MsgBoxStyle.Information, Me.Text)
        fn_LoadSecurity()

    End Sub
    Private Sub picKeluar_Click() Handles picKeluar.Click
        If grv.GetFocusedRowCellValue("KDKUNJUNGAN_RUANGAN") Is Nothing Then
            Exit Sub
        End If

        If grv.GetFocusedRowCellValue("ISCHEKED") = True Then
            If MsgBox("Apakah Yakin Pasien Masuk Ruangan kembali, jika tekan Yes data tidak dapat dirubah lagi", MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, Me.Text) = MsgBoxResult.No Then Exit Sub

            If oPendaftaran_KunjunganRuangan.UpdateDataIsCheked(grv.GetFocusedRowCellValue("KDKUNJUNGAN_RUANGAN"), False) = False Then
                MsgBox(Statement.DeleteFail, MsgBoxStyle.Exclamation, Me.Text)
                Exit Sub
            End If

            oPendaftaran_KunjunganRuangan.UpdateDataIsTerisi(grv.GetFocusedRowCellValue("KDKUNJUNGAN_RUANGAN"), grv.GetFocusedRowCellValue("SEQ"), True)
            MsgBox(Statement.DeleteSuccess, MsgBoxStyle.Information, Me.Text)
            fn_LoadSecurity()
        Else
            If MsgBox("Apakah Yakin Pasien Sudah Keluar, jika tekan Yes data tidak dapat dirubah lagi", MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, Me.Text) = MsgBoxResult.No Then Exit Sub

            If oPendaftaran_KunjunganRuangan.UpdateDataIsCheked(grv.GetFocusedRowCellValue("KDKUNJUNGAN_RUANGAN"), True) = False Then
                MsgBox(Statement.DeleteFail, MsgBoxStyle.Exclamation, Me.Text)
                Exit Sub
            End If

            oPendaftaran_KunjunganRuangan.UpdateDataIsTerisi(grv.GetFocusedRowCellValue("KDKUNJUNGAN_RUANGAN"), grv.GetFocusedRowCellValue("SEQ"), False)
            MsgBox(Statement.DeleteSuccess, MsgBoxStyle.Information, Me.Text)
            fn_LoadSecurity()
        End If
    End Sub
    Private Sub picPrint_Click() Handles picPrint.Click
        'Try
        '    If grv.GetFocusedRowCellValue("KDKUNJUNGAN_RUANGAN") = String.Empty Then Exit Sub

        '    Dim rpt As New xtraPendaftaran_KunjunganRuangan

        '    rpt.ShowPrintMarginsWarning = False
        '    rpt.Watermark.Text = sWATERMARK
        '    Dim ds = oPendaftaran_KunjunganRuangan.GetData(grv.GetFocusedRowCellValue("KDKUNJUNGAN_RUANGAN"))
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
#Region "Lookup"
    'Private Sub fn_LoadRuangan()
    '    Dim oRuangRawat As New Reference.clsRuangRawat
    '    Try
    '        grdKDRUANGRAWAT.Properties.DataSource = oRuangRawat.GetData.Where(Function(x) x.ISACTIVE = True).ToList()
    '        grdKDRUANGRAWAT.Properties.ValueMember = "KDRUANGRAWAT"
    '        grdKDRUANGRAWAT.Properties.DisplayMember = "NAME_DISPLAY"

    '    Catch oErr As Exception
    '        MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
    '    End Try
    'End Sub
#End Region
End Class