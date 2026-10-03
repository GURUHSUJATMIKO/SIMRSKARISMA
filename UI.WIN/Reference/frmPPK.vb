Imports DataAccess
Imports UI.WIN.MAIN.My.Resources
Imports System.Linq
Imports Newtonsoft.Json.Linq

Public Class frmPPK
#Region "Declaration"
    Private oFormMode As FORM_MODE = FORM_MODE.FORM_MODE_VIEW
    Private sNoId As String
    Private isLoad As Boolean = False
    Private oPPK As New Reference.clsPPK
#End Region
#Region "Function"
    Public Sub LoadMe(ByVal FormMode As Integer, Optional ByVal NoId As String = "")
        oFormMode = FormMode
        sNoId = NoId
    End Sub
    Private Sub Form_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        fn_ChangeFormState()
        fn_LoadLanguage()
        isLoad = True
    End Sub
    Public Sub fn_LoadLanguage()
        Try
            Me.Text = PPK.TITLE

            lJENISFASKES.Text = PPK.JENISFASKES
            lMEMO.Text = PPK.MEMO & " *"
            lKDFASKES.Text = PPK.KDFASKES & " *"
            chkISACTIVE.Text = PPK.ISACTIVE
            chkISDEFAULT.Text = PPK.ISDEFAULT

            btnSaveNew.Caption = Caption.FormSaveNew
            btnSaveClose.Caption = Caption.FormSaveClose
            btnClose.Caption = Caption.FormClose
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub Form_Closing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        sCode = txtMEMO.Text.Trim.ToUpper
    End Sub
    Private Sub fn_ChangeFormState()
        Select Case oFormMode
            Case FORM_MODE.FORM_MODE_VIEW
                fn_ViewMode(True)
                fn_LoadData()
            Case FORM_MODE.FORM_MODE_ADD
                fn_ViewMode(False)
                fn_EmptyMe()
            Case FORM_MODE.FORM_MODE_EDIT
                fn_ViewMode(False)
                fn_LoadData()
            Case Else
                fn_ViewMode(True)
        End Select
    End Sub
    Private Sub fn_ViewMode(ByVal Status As Boolean)
        btnSaveNew.Enabled = Not Status
        btnSaveClose.Enabled = Not Status

        If oFormMode = FORM_MODE.FORM_MODE_ADD Then
            txtKDFASKES.Properties.ReadOnly = False
        Else
            txtKDFASKES.Properties.ReadOnly = True
        End If

        cboASALRUJUKAN.Properties.ReadOnly = Status
        txtMEMO.Properties.ReadOnly = Status
        chkISACTIVE.Properties.ReadOnly = Status
        chkISDEFAULT.Properties.ReadOnly = Status
    End Sub
    Private Sub fn_EmptyMe()
        txtMEMO.ResetText()
        txtKDFASKES.ResetText()
        chkISACTIVE.Checked = True
        chkISDEFAULT.Checked = False
    End Sub
    Private Sub fn_LoadData()
        Try
            Dim ds = oPPK.GetData(sNoId)

            With ds
                cboASALRUJUKAN.SelectedIndex = .JENISFASKES
                txtKDFASKES.Text = .KODEFASKES
                txtMEMO.Text = .MEMO
                chkISACTIVE.Checked = .ISACTIVE
                chkISDEFAULT.Checked = .ISDEFAULT
            End With
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Function fn_Validate() As Boolean
        Try
            fn_Validate = True
            If txtMEMO.Text = String.Empty Then
                txtMEMO.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                txtMEMO.ErrorText = Statement.ErrorRequired

                txtMEMO.Focus()
                fn_Validate = False
                Exit Function
            End If
            If txtKDFASKES.Text = String.Empty Then
                txtKDFASKES.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                txtKDFASKES.ErrorText = Statement.ErrorRequired

                txtKDFASKES.Focus()
                fn_Validate = False
                Exit Function
            End If
            If oFormMode = FORM_MODE.FORM_MODE_ADD Then
                If oPPK.IsExist(txtKDFASKES.Text) = True Then
                    txtMEMO.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                    txtMEMO.ErrorText = Statement.ErrorRegistered

                    txtMEMO.Focus()
                    fn_Validate = False
                    Exit Function
                End If
            Else
                If txtKDFASKES.Text <> oPPK.GetData(sNoId).KODEFASKES Then
                    If oPPK.IsExist(txtKDFASKES.Text) = True Then
                        txtKDFASKES.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                        txtKDFASKES.ErrorText = Statement.ErrorRegistered

                        txtKDFASKES.Focus()
                        fn_Validate = False
                        Exit Function
                    End If
                End If
            End If
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Function
    Private Function fn_Save() As Boolean
        Try
            ' ***** HEADER *****
            Dim ds = oPPK.GetStructureHeader
            With ds
                Try
                    .DATECREATED = oPPK.GetData(sNoId).DATECREATED
                Catch oErr As Exception
                    .DATECREATED = Now
                End Try
                .DATEUPDATED = Now

                .JENISFASKES = cboASALRUJUKAN.SelectedIndex
                .KDPPK = sNoId
                .KODEFASKES = txtKDFASKES.Text
                .MEMO = txtMEMO.Text
                .ISACTIVE = chkISACTIVE.Checked
                .ISDEFAULT = chkISDEFAULT.Checked
            End With

            If oFormMode = FORM_MODE.FORM_MODE_ADD Then
                Try
                    fn_Save = oPPK.InsertData(ds)
                Catch oErr As Exception
                    MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            ElseIf oFormMode = FORM_MODE.FORM_MODE_EDIT Then
                Try
                    fn_Save = oPPK.UpdateData(ds)
                Catch oErr As Exception
                    MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            End If
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            fn_Save = False
        End Try
    End Function
#End Region
#Region "Command Button"
    Private Sub frmItem_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        Select Case e.KeyCode
            Case Keys.F12
                btnClose_Click()
            Case Keys.F2
                If btnSaveNew.Enabled = True Then
                    btnSaveNew_Click()
                End If
            Case Keys.F3
                If btnSaveClose.Enabled = True Then
                    btnSaveClose_Click()
                End If
        End Select
    End Sub
    Private Sub btnSaveNew_Click() Handles btnSaveNew.ItemClick
        If fn_Validate() = False Then Exit Sub
        If MsgBox(Statement.SaveQuestion, MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, Me.Text) = MsgBoxResult.No Then Exit Sub
        If fn_Save() = False Then
            MsgBox(Statement.SaveFail, MsgBoxStyle.Exclamation, Me.Text)
        Else
            MsgBox(Statement.SaveSuccess, MsgBoxStyle.Information, Me.Text)
            sStatusSave = "NEW"
            Me.Close()
        End If
    End Sub
    Private Sub btnSaveClose_Click() Handles btnSaveClose.ItemClick
        If fn_Validate() = False Then Exit Sub
        If MsgBox(Statement.SaveQuestion, MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, Me.Text) = MsgBoxResult.No Then Exit Sub
        If fn_Save() = False Then
            MsgBox(Statement.SaveFail, MsgBoxStyle.Exclamation, Me.Text)
        Else
            MsgBox(Statement.SaveSuccess, MsgBoxStyle.Information, Me.Text)
            Me.Close()
        End If
    End Sub
    Private Sub btnClose_Click() Handles btnClose.ItemClick
        Me.Close()
    End Sub
    Private Sub txtCARI_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtCARI.KeyPress
        If Asc(e.KeyChar) = 13 Then
            Try
                If txtCARI.Text = String.Empty Then
                    Exit Sub
                End If

                Dim oSetKoneksi As New Brigging.clsSetKoneksi
                Dim dsDataSetKoneksi = oSetKoneksi.GetData().FirstOrDefault(Function(x) x.ISACTIVE = True And x.NAME_DISPLAY = "VCLAIM")
                Dim uTime As Integer = 0

                If dsDataSetKoneksi IsNot Nothing Then
                    uTime = (DateTime.UtcNow - New DateTime(1970, 1, 1, 0, 0, 0)).TotalSeconds
                    Dim dsSetKoneksi = oSetKoneksi.GetDataVClaimReferensiFasilitasKesehatan(dsDataSetKoneksi.ALAMATWEB, dsDataSetKoneksi.CONSID, dsDataSetKoneksi.SECREATKEY, dsDataSetKoneksi.USER_KEY, uTime, txtCARI.Text, cboASALRUJUKAN.SelectedIndex + 1)

                    If dsSetKoneksi <> "" Then
                        Try
                            Dim allData = JObject.Parse(dsSetKoneksi)

                            Dim table As DataTable

                            table = New DataTable("M_PPK")
                            table.Columns.Add("kode")
                            table.Columns.Add("nama")

                            Dim dsData = oSetKoneksi.Decrypt(allData("response"), dsDataSetKoneksi.CONSID & dsDataSetKoneksi.SECREATKEY & uTime)
                            Dim ds = JObject.Parse(dsData)

                            For Each item In ds("faskes")
                                table.Rows.Add(New String() {item("kode"), item("nama")})
                            Next

                            grdCARI.Properties.DataSource = table
                            grdCARI.Properties.ValueMember = "kode"
                            grdCARI.Properties.DisplayMember = "nama"

                            grdCARI.ShowPopup()

                            txtCARI.ResetText()

                        Catch oErr As Exception
                            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message & vbCrLf & vbCrLf & "Result : " & vbCrLf & dsSetKoneksi, MsgBoxStyle.Exclamation, Me.Text)
                        End Try
                    End If
                Else
                    MsgBox("Koneksi Tidak ditemukan", MsgBoxStyle.Exclamation, Me.Text)
                End If
            Catch oErr As Exception
                MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            End Try
        End If
    End Sub
    Private Sub grdCARI_KeyPress(sender As Object, e As KeyPressEventArgs) Handles grdCARI.KeyPress
        If Asc(e.KeyChar) = 13 Then
            If grdCARI.Text = String.Empty Then Exit Sub
            txtKDFASKES.Text = grdCARI.EditValue
            txtMEMO.Text = grdCARI.Text
        End If
    End Sub
#End Region
#Region "Lookup / Event"

#End Region
End Class