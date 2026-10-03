Imports DataAccess
Imports UI.WIN.MAIN.My.Resources
Imports System.Linq
Imports Newtonsoft.Json.Linq

Public Class frmKabupaten
#Region "Declaration"
    Private oFormMode As FORM_MODE = FORM_MODE.FORM_MODE_VIEW
    Private sNoId As String
    Private isLoad As Boolean = False
    Private oKABUPATEN As New Reference.clsKabupaten
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
            Me.Text = Kabupaten.TITLE

            lPROPINSI.Text = Kabupaten.KDPROPINSI & " *"
            lMEMO.Text = Kabupaten.MEMO & " *"
            chkISACTIVE.Text = Kabupaten.ISACTIVE
            chkISDEFAULT.Text = Kabupaten.ISDEFAULT
            lVCALIM_KODE.Text = Kabupaten.VCLAIM_KODE & " *"

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
        fn_LoadKDPROPINSI()

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

        grdKDPROPINSI.Properties.ReadOnly = Status
        txtMEMO.Properties.ReadOnly = Status
        chkISACTIVE.Properties.ReadOnly = Status
        chkISDEFAULT.Properties.ReadOnly = Status
        txtVCLAIM_KODE.Properties.ReadOnly = True
    End Sub
    Private Sub fn_EmptyMe()
        grdKDPROPINSI.ResetText()
        txtMEMO.ResetText()

        chkISACTIVE.Checked = True
        chkISDEFAULT.Checked = False
        txtVCLAIM_KODE.ResetText()
        txtMEMO.ResetText()
    End Sub
    Private Sub fn_LoadData()
        Try
            Dim ds = oKABUPATEN.GetData(sNoId)

            With ds
                grdKDPROPINSI.Text = .KDPROPINSI
                txtMEMO.Text = .MEMO
                chkISACTIVE.Checked = .ISACTIVE
                chkISDEFAULT.Checked = .ISDEFAULT
                txtVCLAIM_KODE.Text = .VCLAIM_KODE
            End With
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Function fn_Validate() As Boolean
        Try
            fn_Validate = True
            If grdKDPROPINSI.Text = String.Empty Then
                grdKDPROPINSI.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                grdKDPROPINSI.ErrorText = Statement.ErrorRequired

                grdKDPROPINSI.Focus()
                fn_Validate = False
                Exit Function
            End If
            If txtVCLAIM_KODE.Text = String.Empty Then
                txtVCLAIM_KODE.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                txtVCLAIM_KODE.ErrorText = Statement.ErrorRequired

                txtVCLAIM_KODE.Focus()
                fn_Validate = False
                Exit Function
            End If
            If txtMEMO.Text = String.Empty Then
                txtMEMO.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                txtMEMO.ErrorText = Statement.ErrorRequired

                txtMEMO.Focus()
                fn_Validate = False
                Exit Function
            End If
            If oFormMode = FORM_MODE.FORM_MODE_ADD Then
                If oKABUPATEN.IsExist(txtMEMO.Text.ToUpper.Trim) = True Then
                    txtMEMO.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                    txtMEMO.ErrorText = Statement.ErrorRegistered

                    txtMEMO.Focus()
                    fn_Validate = False
                    Exit Function
                End If
            Else
                If txtMEMO.Text.Trim.ToUpper <> oKABUPATEN.GetData(sNoId).MEMO Then
                    If oKABUPATEN.IsExist(txtMEMO.Text.ToUpper.Trim) = True Then
                        txtMEMO.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                        txtMEMO.ErrorText = Statement.ErrorRegistered

                        txtMEMO.Focus()
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
            Dim ds = oKABUPATEN.GetStructureHeader
            With ds
                Try
                    .DATECREATED = oKABUPATEN.GetData(sNoId).DATECREATED
                Catch oErr As Exception
                    .DATECREATED = Now
                End Try
                .DATEUPDATED = Now

                .KDKABUPATEN = sNoId
                .KDPROPINSI = IIf(String.IsNullOrEmpty(grdKDPROPINSI.EditValue), String.Empty, grdKDPROPINSI.EditValue)
                .MEMO = txtMEMO.Text.Trim.ToUpper
                .ISACTIVE = chkISACTIVE.Checked
                .ISDEFAULT = chkISDEFAULT.Checked
                .VCLAIM_KODE = txtVCLAIM_KODE.Text.Trim.ToUpper
            End With

            If oFormMode = FORM_MODE.FORM_MODE_ADD Then
                Try
                    fn_Save = oKABUPATEN.InsertData(ds)
                Catch oErr As Exception
                    MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            ElseIf oFormMode = FORM_MODE.FORM_MODE_EDIT Then
                Try
                    fn_Save = oKABUPATEN.UpdateData(ds)
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
#End Region
#Region "Lookup / Event"
    Private Sub fn_LoadKDPROPINSI()
        Dim oPRONISI As New Reference.clsPropinsi
        Try
            grdKDPROPINSI.Properties.DataSource = oPRONISI.GetData.Where(Function(x) x.ISACTIVE = True And x.VCLAIM_KODE <> "").ToList()
            grdKDPROPINSI.Properties.ValueMember = "KDPROPINSI"
            grdKDPROPINSI.Properties.DisplayMember = "MEMO"
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadKabupaten(ByVal KodePropinsi As String)
        Try
            Dim oSetKoneksi As New Brigging.clsSetKoneksi
            Dim dsDataSetKoneksi = oSetKoneksi.GetData().FirstOrDefault(Function(x) x.ISACTIVE = True And x.NAME_DISPLAY = "VCLAIM")
            Dim uTime As Integer = 0

            If dsDataSetKoneksi IsNot Nothing Then
                uTime = (DateTime.UtcNow - New DateTime(1970, 1, 1, 0, 0, 0)).TotalSeconds
                Dim dsSetKoneksi = oSetKoneksi.GetDataVClaimReferensiKabupaten(dsDataSetKoneksi.ALAMATWEB, dsDataSetKoneksi.CONSID, dsDataSetKoneksi.SECREATKEY, dsDataSetKoneksi.USER_KEY, uTime, KodePropinsi)

                If dsSetKoneksi <> "" Then
                    Try
                        Dim allData = JObject.Parse(dsSetKoneksi)

                        Dim table As DataTable

                        table = New DataTable("M_TABEL")
                        table.Columns.Add("kode")
                        table.Columns.Add("nama")

                        Dim dsData = oSetKoneksi.Decrypt(allData("response"), dsDataSetKoneksi.CONSID & dsDataSetKoneksi.SECREATKEY & uTime)
                        Dim ds = JObject.Parse(dsData)

                        For Each item In ds("list")
                            table.Rows.Add(New String() {item("kode"), item("nama")})
                        Next

                        grdKABUPATEN.Properties.DataSource = table
                        grdKABUPATEN.Properties.ValueMember = "kode"
                        grdKABUPATEN.Properties.DisplayMember = "nama"

                        grdKABUPATEN.ShowPopup()

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
    End Sub
    Private Sub grdKDPROPINSI_KeyPress(sender As Object, e As KeyPressEventArgs) Handles grdKDPROPINSI.KeyPress
        If grdKDPROPINSI.Text = String.Empty Then Exit Sub
        Dim oPropinsi As New Reference.clsPropinsi
        If Asc(e.KeyChar) = 13 Then
            fn_LoadKabupaten(oPropinsi.GetData(grdKDPROPINSI.EditValue).VCLAIM_KODE)
        End If
    End Sub
    Private Sub grdKABUPATEN_KeyPress(sender As Object, e As KeyPressEventArgs) Handles grdKABUPATEN.KeyPress
        If grdKABUPATEN.Text = String.Empty Then Exit Sub
        If Asc(e.KeyChar) = 13 Then
            txtVCLAIM_KODE.Text = grdKABUPATEN.EditValue
            txtMEMO.Text = grdKABUPATEN.Text
        End If
    End Sub
    Private Sub SimpleButton1_Click(sender As Object, e As EventArgs) Handles SimpleButton1.Click
        Dim frmPropinsi As New frmPropinsi
        Try
            frmPropinsi.LoadMe(FORM_MODE.FORM_MODE_ADD)
            frmPropinsi.ShowDialog(Me)

            fn_LoadKDPROPINSI()
            grdKDPROPINSI.Text = sCode
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
#End Region
End Class