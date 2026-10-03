Imports DataAccess
Imports UI.WIN.MAIN.My.Resources
Imports System.Linq
Imports Newtonsoft.Json.Linq

Public Class frmRujukanList
    Implements ILanguage

    Private oFormMode As FORM_MODE = FORM_MODE.FORM_MODE_VIEW
    Private oRujukan As New Admission.clsRujukan

#Region "Function"
    Private Sub Me_Load() Handles Me.Load
        Me.Text = Rujukan.TITLE

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
                      Where x.MODUL = "RUJUKAN" _
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
            Me.Text = Rujukan.TITLE

            grv.Columns("KDRUJUKAN").Caption = Rujukan.KDRUJUKAN
            grv.Columns("KDPENDAFTARAN").Caption = Rujukan.KDPENDAFTARAN
            grv.Columns("NOMORSEP").Caption = Rujukan.NOMORSEP
            grv.Columns("TUJUAN").Caption = Rujukan.KDDEPARTMENT
            grv.Columns("DATE").Caption = Rujukan.TANGGAL
            grv.Columns("DIAGNOSA").Caption = Rujukan.KDDIAGNOSA
            grv.Columns("TIPERUJUKAN").Caption = Rujukan.TIPERUJUKAN
            grv.Columns("CATATAN").Caption = Rujukan.CATATAN
            grv.Columns("KDPPK").Caption = Rujukan.KDPPK

            ColumnChooserToolStripMenuItem.Text = Caption.ColumnChooser
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadData()
        Try
            Dim ds = From x In oRujukan.GetData
                     Select x.KDRUJUKAN, x.NOMORSEP, x.KDPENDAFTARAN, x.DATE, TUJUAN = x.M_DEPARTMENT.NAME_DISPLAY, KDPPK = x.M_PPK.MEMO, DIAGNOSA = x.M_DIAGNOSA.MEMO, x.TIPERUJUKAN, x.CATATAN

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

        grv.Columns("KDRUJUKAN").Visible = False
        grv.Columns("KDRUJUKAN").OptionsColumn.ShowInCustomizationForm = False
    End Sub
    Private Sub ColumnChooserToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles ColumnChooserToolStripMenuItem.Click
        'grv.ShowCustomization()
        If grv.GetFocusedRowCellValue("KDRUJUKAN") Is Nothing Then
            Exit Sub
        End If
        Dim frmRujukanKhusus As New frmRujukanKhusus
        Try
            frmRujukanKhusus.LoadMe(FORM_MODE.FORM_MODE_ADD, grv.GetFocusedRowCellValue("KDRUJUKAN"))
            frmRujukanKhusus.ShowDialog(Me)
            fn_LoadSecurity()
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        Finally
            If Not frmRujukanKhusus Is Nothing Then frmRujukanKhusus.Dispose()
            frmRujukanKhusus = Nothing

        End Try
    End Sub
    Private Function fn_DeleteData(ByVal RujukanRujukan As String) As Boolean
        Try
            Dim frmPesanDelete As New frmPesanDelete
            frmPesanDelete.ShowDialog(Me)

            If sDeletePesan <> "" Then
                Dim oDelete As New Setting.clsDelete

                oDelete.InsertData("RUJUKAN", "DELETE", grv.GetFocusedRowCellValue("NoSEP") & "Tanggal " & Now & " Oleh " & sUserID & " Alasan " & sDeletePesan, RujukanRujukan)

                If fn_DeleteRujukan(grv.GetFocusedRowCellValue("KDRUJUKAN")) = True Then
                    oRujukan.DeleteData(RujukanRujukan)
                    fn_DeleteData = True
                End If
            End If

        Catch oErr As Exception
            fn_DeleteData = False
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Function
    Private Function fn_DeleteRujukan(ByVal NOMORRUJUKAN As String) As Boolean
        Try
            If NOMORRUJUKAN = String.Empty Then
                fn_DeleteRujukan = True
                Exit Function
            End If

            Dim oSetKoneksi As New Brigging.clsSetKoneksi
            Dim dsDataSetKoneksi = oSetKoneksi.GetData().FirstOrDefault(Function(x) x.ISACTIVE = True And x.NAME_DISPLAY = "VCLAIM")
            Dim uTime As Integer = 0

            If dsDataSetKoneksi IsNot Nothing Then
                Dim jsonRequest As String = String.Empty

                jsonRequest = "{" & """request"": {" & """t_rujukan"": {" & """noRujukan"": """ & grv.GetFocusedRowCellValue("KDRUJUKAN") & """," & """user"": """ & sUserID & """" & "}}}"

                uTime = (DateTime.UtcNow - New DateTime(1970, 1, 1, 0, 0, 0)).TotalSeconds
                Dim dsSetKoneksi = oSetKoneksi.DeleteRujukan(dsDataSetKoneksi.ALAMATWEB, dsDataSetKoneksi.CONSID, dsDataSetKoneksi.SECREATKEY, dsDataSetKoneksi.USER_KEY, uTime, jsonRequest)

                If dsSetKoneksi <> "" Then
                    Dim allData = JObject.Parse(dsSetKoneksi)

                    Dim CodeResponse As String = String.Empty
                    Dim messageResponse As String = String.Empty

                    CodeResponse = IIf(IsDBNull(allData.Item("metaData").Item("code")) = True, "", allData.Item("metaData").Item("code"))
                    messageResponse = allData("metaData")("message").ToString

                    If CodeResponse = "200" Then
                        fn_DeleteRujukan = True
                    Else
                        fn_DeleteRujukan = False
                        MsgBox(CodeResponse & " - " & messageResponse, MsgBoxStyle.Exclamation, Me.Text)
                    End If
                Else
                    fn_DeleteRujukan = False
                    MsgBox("Delete SEP Data Gagal/Kosong", MsgBoxStyle.Exclamation, Me.Text)
                End If
            Else
                fn_DeleteRujukan = False
                MsgBox("Koneksi Tidak ditemukan", MsgBoxStyle.Exclamation, Me.Text)
            End If
        Catch oErr As Exception
            fn_DeleteRujukan = False
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
        If grv.GetFocusedRowCellValue("KDRUJUKAN") Is Nothing Then
            Exit Sub
        End If

        Dim frmRujukan As New frmRujukan
        Try
            frmRujukan.LoadMe(FORM_MODE.FORM_MODE_VIEW, grv.GetFocusedRowCellValue("KDRUJUKAN"))
            frmRujukan.ShowDialog(Me)
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub picAdd_Click() Handles picAdd.Click
        Dim frmRujukan As New frmRujukan
        Try
            frmRujukan.LoadMe(FORM_MODE.FORM_MODE_ADD)
            frmRujukan.ShowDialog(Me)
            fn_LoadSecurity()
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        Finally
            If Not frmRujukan Is Nothing Then frmRujukan.Dispose()
            frmRujukan = Nothing

            Dim rowHandle As Integer = grv.LocateByValue(rowHandle, grv.Columns("MEMO"), sCode)
            If rowHandle > 0 Then grv.FocusedRowHandle = rowHandle

            If sStatusSave = "NEW" Then
                sStatusSave = "NONE"
                picAdd_Click()
            End If
        End Try
    End Sub
    Private Sub picUpdate_Click() Handles picUpdate.Click
        If grv.GetFocusedRowCellValue("KDRUJUKAN") Is Nothing Then
            Exit Sub
        End If
        Dim frmRujukan As New frmRujukan
        Try
            frmRujukan.LoadMe(FORM_MODE.FORM_MODE_EDIT, grv.GetFocusedRowCellValue("KDRUJUKAN"))
            frmRujukan.ShowDialog(Me)
            fn_LoadSecurity()
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        Finally
            If Not frmRujukan Is Nothing Then frmRujukan.Dispose()
            frmRujukan = Nothing

            Dim rowHandle As Integer = grv.LocateByValue(rowHandle, grv.Columns("MEMO"), sCode)
            If rowHandle > 0 Then grv.FocusedRowHandle = rowHandle

            If sStatusSave = "NEW" Then
                sStatusSave = "NONE"
                picAdd_Click()
            End If
        End Try
    End Sub
    Private Sub picDelete_Click() Handles picDelete.Click
        If grv.GetFocusedRowCellValue("KDRUJUKAN") Is Nothing Then
            Exit Sub
        End If
        If MsgBox(Statement.DeleteQuestion, MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, Me.Text) = MsgBoxResult.No Then Exit Sub

        If fn_DeleteData(grv.GetFocusedRowCellValue("KDRUJUKAN")) = False Then
            MsgBox(Statement.DeleteFail, MsgBoxStyle.Exclamation, Me.Text)
            Exit Sub
        End If
        MsgBox(Statement.DeleteSuccess, MsgBoxStyle.Information, Me.Text)
        fn_LoadSecurity()

    End Sub
    Private Sub picPrint_Click() Handles picPrint.Click
        Try
            If grv.GetFocusedRowCellValue("KDRUJUKAN") = String.Empty Then Exit Sub

            Dim rpt As New xtraRujukan

            rpt.ShowPrintMarginsWarning = False
            rpt.Watermark.Text = sWATERMARK
            Dim ds = oRujukan.GetData(grv.GetFocusedRowCellValue("KDRUJUKAN"))
            rpt.bindingSource.DataSource = ds
            Dim printTool As New DevExpress.XtraReports.UI.ReportPrintTool(rpt)
            printTool.ShowPreviewDialog(DevExpress.LookAndFeel.UserLookAndFeel.Default)
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub picRefresh_Click() Handles picRefresh.Click
        fn_LoadSecurity()
    End Sub
    Private Sub EditRujukanKhususToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles EditRujukanKhususToolStripMenuItem.Click

    End Sub
    Private Sub DeleteRujukanKhususToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DeleteRujukanKhususToolStripMenuItem.Click

    End Sub
#End Region
End Class