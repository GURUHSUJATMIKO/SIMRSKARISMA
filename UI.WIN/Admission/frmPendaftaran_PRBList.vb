Imports DataAccess
Imports UI.WIN.MAIN.My.Resources
Imports System.Linq
Imports Newtonsoft.Json.Linq

Public Class frmPendaftaran_PRBList
    Implements ILanguage

    Private oFormMode As FORM_MODE = FORM_MODE.FORM_MODE_VIEW
    Private oPendaftaran_PRB As New Admission.clsPendaftaran_PRB

#Region "Function"
    Private Sub Me_Load() Handles Me.Load
        Me.Text = Pendaftaran_PRB.TITLE

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
                      Where x.MODUL = "PRB" _
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
    Private Sub fn_LoadLanguage() Implements ILanguage.fn_LoadLanguage
        Try
            Me.Text = Pendaftaran_PRB.TITLE

            fn_LoadLanguageMaster()
            fn_LoadLanguageDetail()
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadLanguageMaster()
        Try
            Me.Text = Pendaftaran_PRB.TITLE

            grv.Columns("KDPRB").Caption = Pendaftaran_PRB.KDPRB
            grv.Columns("KDPENDAFTARAN").Caption = Pendaftaran.KDPENDAFTARAN
            grv.Columns("KDCUSTOMER").Caption = Customer.KDCUSTOMER
            grv.Columns("PASIEN").Caption = Customer.NAME_DISPLAY
            grv.Columns("KDDOCTOR").Caption = Doctor.NAME_DISPLAY
            grv.Columns("KETERANGAN").Caption = Pendaftaran_PRB.KETERANGAN
            grv.Columns("SARAN").Caption = Pendaftaran_PRB.SARAN
            grv.Columns("KDUSER").Caption = User.KDUSER

            ColumnChooserToolStripMenuItem.Text = Caption.ColumnChooser
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Public Sub fn_LoadLanguageDetail()
        Try
            grv1.Columns("KDOBAT").Caption = Pendaftaran_PRB.DETAIL_KDOBAT
            grv1.Columns("NAMAOBAT").Caption = Pendaftaran_PRB.DETAIL_NAMAOBAT
            grv1.Columns("SIGNA1").Caption = Pendaftaran_PRB.DETAIL_SIGNA1
            grv1.Columns("SIGNA2").Caption = Pendaftaran_PRB.DETAIL_SIGNA2
            grv1.Columns("JUMLAH").Caption = Pendaftaran_PRB.DETAIL_JUMLAH
        Catch oErr As Exception
            'MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadData()
        Try
            Dim ds = From x In oPendaftaran_PRB.GetData
                     Select x.KDPRB, x.KDPENDAFTARAN, x.S_PENDAFTARAN_H.KDCUSTOMER, PASIEN = x.S_PENDAFTARAN_H.M_CUSTOMER.NAME_DISPLAY, KDDOCTOR = x.M_DOCTOR.NAME_DISPLAY, x.KETERANGAN, x.SARAN, x.KDUSER, Details = x.S_PENDAFTARAN_PRB_Ds

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

        grv.Columns("KDPRB").Visible = False
        grv.Columns("KDPRB").OptionsColumn.ShowInCustomizationForm = False
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

        grv1.Columns("S_PENDAFTARAN_PRB").Visible = False
        grv1.Columns("DATECREATED").Visible = False
        grv1.Columns("DATEUPDATED").Visible = False
        grv1.Columns("KDPRB").Visible = False
        grv1.Columns("KDPRB").OptionsColumn.ShowInCustomizationForm = False
        grv1.Columns("KDPENDAFTARAN").Visible = False
        grv1.Columns("KDPENDAFTARAN").OptionsColumn.ShowInCustomizationForm = False
        grv1.Columns("SEQ").Visible = False
        grv1.Columns("SEQ").OptionsColumn.ShowInCustomizationForm = False
    End Sub
    Private Sub ColumnChooserToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles ColumnChooserToolStripMenuItem.Click
        grv.ShowCustomization()
    End Sub
    Private Function fn_DeleteData(ByVal sKDKUNJUNGAN_POLI As String) As Boolean
        Try
            oPendaftaran_PRB.DeleteData(sKDKUNJUNGAN_POLI)
            fn_DeleteData = True
        Catch oErr As Exception
            fn_DeleteData = False
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Function
    Private Sub grv_MasterRowExpanded(sender As System.Object, e As DevExpress.XtraGrid.Views.Grid.CustomMasterRowEventArgs) Handles grv.MasterRowExpanded
        grv1 = TryCast(grv.GetDetailView(e.RowHandle, e.RelationIndex), DevExpress.XtraGrid.Views.Grid.GridView)

        fn_LoadFormatDataDetail()
        fn_LoadLanguageDetail()
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
        If grv.GetFocusedRowCellValue("KDPRB") Is Nothing Then
            Exit Sub
        End If

        Dim frmPendaftaran_PRB As New frmPendaftaran_PRB
        Try
            frmPendaftaran_PRB.LoadMe(FORM_MODE.FORM_MODE_VIEW, grv.GetFocusedRowCellValue("KDPRB"))
            frmPendaftaran_PRB.ShowDialog(Me)
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub picAdd_Click() Handles picAdd.Click
        Dim frmPendaftaran_PRB As New frmPendaftaran_PRB
        Try
            frmPendaftaran_PRB.LoadMe(FORM_MODE.FORM_MODE_ADD)
            frmPendaftaran_PRB.ShowDialog(Me)
            fn_LoadSecurity()
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        Finally
            If Not frmPendaftaran_PRB Is Nothing Then frmPendaftaran_PRB.Dispose()
            frmPendaftaran_PRB = Nothing

            Dim rowHandle As Integer = grv.LocateByValue(rowHandle, grv.Columns("MEMO"), sCode)
            If rowHandle > 0 Then grv.FocusedRowHandle = rowHandle

            If sStatusSave = "NEW" Then
                sStatusSave = "NONE"
                picAdd_Click()
            End If
        End Try
    End Sub
    Private Sub picUpdate_Click() Handles picUpdate.Click
        If grv.GetFocusedRowCellValue("KDPRB") Is Nothing Then
            Exit Sub
        End If
        If grv.GetFocusedRowCellValue("MEMO") = "POLI 1" Then
            MsgBox("Tidak Dapat di Edit", MsgBoxStyle.Exclamation, Me.Text)
            Exit Sub
        End If

        Dim frmPendaftaran_PRB As New frmPendaftaran_PRB
        Try
            frmPendaftaran_PRB.LoadMe(FORM_MODE.FORM_MODE_EDIT, grv.GetFocusedRowCellValue("KDPRB"))
            frmPendaftaran_PRB.ShowDialog(Me)
            fn_LoadSecurity()
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        Finally
            If Not frmPendaftaran_PRB Is Nothing Then frmPendaftaran_PRB.Dispose()
            frmPendaftaran_PRB = Nothing

            Dim rowHandle As Integer = grv.LocateByValue(rowHandle, grv.Columns("MEMO"), sCode)
            If rowHandle > 0 Then grv.FocusedRowHandle = rowHandle

            If sStatusSave = "NEW" Then
                sStatusSave = "NONE"
                picAdd_Click()
            End If
        End Try
    End Sub
    Private Function fn_HapusPRB() As Boolean
        Try
            If grv.GetFocusedRowCellValue("NoSEP") = String.Empty Then
                fn_HapusPRB = True
                Exit Function
            End If

            Dim oSetKoneksi As New Brigging.clsSetKoneksi
            Dim dsDataSetKoneksi = oSetKoneksi.GetData().FirstOrDefault(Function(x) x.ISACTIVE = True And x.NAME_DISPLAY = "VCLAIM")
            Dim uTime As Integer = 0

            If dsDataSetKoneksi IsNot Nothing Then
                Dim jsonRequest As String = String.Empty

                jsonRequest = "{" & """request"": {" & """t_prb"": {" & """noSrb"": """ & grv.GetFocusedRowCellValue("KDPRB") & """," & """noSep"": """ & grv.GetFocusedRowCellValue("NoSEP") & """," & """user"": """ & sUserID & """" & "}}}"

                uTime = (DateTime.UtcNow - New DateTime(1970, 1, 1, 0, 0, 0)).TotalSeconds
                Dim dsSetKoneksi = oSetKoneksi.HapusPRB(dsDataSetKoneksi.ALAMATWEB, dsDataSetKoneksi.CONSID, dsDataSetKoneksi.SECREATKEY, dsDataSetKoneksi.USER_KEY, uTime, jsonRequest)

                If dsSetKoneksi <> "" Then
                    Dim allData = JObject.Parse(dsSetKoneksi)

                    Dim CodeResponse As String = String.Empty
                    Dim messageResponse As String = String.Empty

                    CodeResponse = IIf(IsDBNull(allData.Item("metaData").Item("code")) = True, "", allData.Item("metaData").Item("code"))
                    messageResponse = allData("metaData")("message").ToString

                    If CodeResponse = "200" Then
                        fn_HapusPRB = True
                    Else
                        fn_HapusPRB = False
                        MsgBox(CodeResponse & " - " & messageResponse, MsgBoxStyle.Exclamation, Me.Text)
                    End If
                Else
                    fn_HapusPRB = False
                    MsgBox("Delete PRB Data Gagal/Kosong", MsgBoxStyle.Exclamation, Me.Text)
                End If
            Else
                fn_HapusPRB = False
                MsgBox("Koneksi Tidak ditemukan", MsgBoxStyle.Exclamation, Me.Text)
            End If
        Catch oErr As Exception
            fn_HapusPRB = False
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Function
    Private Sub picDelete_Click() Handles picDelete.Click
        If grv.GetFocusedRowCellValue("KDPRB") Is Nothing Then
            Exit Sub
        End If
        If grv.GetFocusedRowCellValue("MEMO") = "POLI 1" Then
            MsgBox("Tidak Dapat di Delete", MsgBoxStyle.Exclamation, Me.Text)
            Exit Sub
        End If

        If MsgBox(Statement.DeleteQuestion, MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, Me.Text) = MsgBoxResult.No Then Exit Sub

        If fn_HapusPRB() = True Then
            If fn_DeleteData(grv.GetFocusedRowCellValue("KDPRB")) = False Then
                MsgBox(Statement.DeleteFail, MsgBoxStyle.Exclamation, Me.Text)
                Exit Sub
            End If
            MsgBox(Statement.DeleteSuccess, MsgBoxStyle.Information, Me.Text)
            fn_LoadSecurity()
        End If
    End Sub
    Private Sub picPrint_Click() Handles picPrint.Click
        'Try
        '    If grv.GetFocusedRowCellValue("KDPRB") = String.Empty Then Exit Sub

        '    Dim rpt As New xtraPendaftaran_PRB

        '    rpt.ShowPrintMarginsWarning = False
        '    rpt.Watermark.Text = sWATERMARK
        '    Dim ds = oPendaftaran_PRB.GetData(grv.GetFocusedRowCellValue("KDPRB"))
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