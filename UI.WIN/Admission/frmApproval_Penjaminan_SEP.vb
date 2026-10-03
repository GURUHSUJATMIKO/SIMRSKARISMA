Imports DataAccess
Imports UI.WIN.MAIN.My.Resources
Imports System.Linq
Imports Newtonsoft.Json.Linq

Public Class frmApproval_Penjaminan_SEP
#Region "Declaration"
    Private oFormMode As FORM_MODE = FORM_MODE.FORM_MODE_VIEW
    Private sNoId As String
    Private isLoad As Boolean = False
    Private oApproval_Penjaminan_SEP As New Admission.clsApproval_Penjaminan_SEP
    Private PopUP As Boolean = False
    Private REQUEST As String = String.Empty
    Private RESPONSE As String = String.Empty

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
            Me.Text = Approval_Penjaminan_SEP.TITLE

            lKDAPPROVAL.Text = Approval_Penjaminan_SEP.KDAPPROVAL
            lKDCUSTOMER.Text = Approval_Penjaminan_SEP.KDCUSTOMER & " *"
            lDATE.Text = Approval_Penjaminan_SEP.TANGGAL
            lCATEGORY.Text = Approval_Penjaminan_SEP.CATEGORY
            lJENISRAWAT.Text = Approval_Penjaminan_SEP.JENISRAWAT
            lJENISPENGAJUAN.Text = Approval_Penjaminan_SEP.JENISPENGAJUAN
            lDESCRIPTION.Text = Approval_Penjaminan_SEP.DESCRIPTION & " *"

            btnSaveNew.Caption = Caption.FormSaveNew
            btnSaveClose.Caption = Caption.FormSaveClose
            btnClose.Caption = Caption.FormClose
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub Form_Closing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        sCode = txtCODE.Text.Trim.ToUpper
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

        grdKDCUSTOMER.Properties.ReadOnly = Status
        deDATE.Properties.ReadOnly = Status
        rbCATEGORY.Properties.ReadOnly = Status
        rbJENISRAWAT.Properties.ReadOnly = Status
        txtDESCRIPTION.Properties.ReadOnly = Status
        cboJENISPENGAJUAN.Properties.ReadOnly = Status
    End Sub
    Private Sub fn_EmptyMe()
        txtCODE.Text = "<--- AUTO --->"

        grdKDCUSTOMER.ResetText()
        deDATE.DateTime = Now
        txtDESCRIPTION.ResetText()
    End Sub
    Private Sub fn_LoadData()
        Try
            Dim ds = oApproval_Penjaminan_SEP.GetData(sNoId)

            With ds
                txtCODE.Text = sNoId
                rbCATEGORY.SelectedIndex = .CATEGORY
                rbJENISRAWAT.SelectedIndex = .JENISRAWAT

                fn_LoadKDCUSTOMER(.KDCUSTOMER)
                grdKDCUSTOMER.Text = .KDCUSTOMER

                txtDESCRIPTION.Text = .DESCRIPTION
                deDATE.DateTime = .DATE
                cboJENISPENGAJUAN.SelectedIndex = .JENISPENGAJUAN
            End With
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Function fn_Validate() As Boolean
        Try
            fn_Validate = True
            If txtDESCRIPTION.Text = String.Empty Then
                txtDESCRIPTION.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                txtDESCRIPTION.ErrorText = Statement.ErrorRequired

                txtDESCRIPTION.Focus()
                fn_Validate = False
                Exit Function
            End If
            If grdKDCUSTOMER.Text = String.Empty Then
                grdKDCUSTOMER.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                grdKDCUSTOMER.ErrorText = Statement.ErrorRequired

                grdKDCUSTOMER.Focus()
                fn_Validate = False
                Exit Function
            End If
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Function
    Private Function fn_SavePengajuan() As Boolean
        Try
            Dim jsonRequest As String = String.Empty
            Dim noKartu As String = String.Empty
            Dim tglSep As String = String.Empty
            Dim jnsPelayanan As String = String.Empty
            Dim keterangan As String = String.Empty
            Dim user As String = String.Empty
            Dim oCustomer As New Reference.clsCustomer

            noKartu = oCustomer.GetData(grdKDCUSTOMER.EditValue).KARTUBPJS
            tglSep = deDATE.DateTime.ToString("yyyy-MM-dd")
            jnsPelayanan = rbJENISRAWAT.SelectedIndex + 1
            keterangan = txtDESCRIPTION.Text.ToString.Trim
            user = sUserID

            jsonRequest = "{ "
            jsonRequest &= """request"" :  { "
            jsonRequest &= """t_sep"": { "
            jsonRequest &= """noKartu"": """ & noKartu & ""","
            jsonRequest &= """tglSep"": """ & tglSep & ""","
            jsonRequest &= """jnsPelayanan"": """ & jnsPelayanan & ""","
            jsonRequest &= """jnsPengajuan"": """ & cboJENISPENGAJUAN.SelectedIndex + 1 & ""","
            jsonRequest &= """keterangan"": """ & keterangan & ""","
            jsonRequest &= """user"": """ & user & """"
            jsonRequest &= "} "
            jsonRequest &= "} "
            jsonRequest &= "} "

            Dim oSetKoneksi As New Brigging.clsSetKoneksi
            Dim dsDataSetKoneksi = oSetKoneksi.GetData().FirstOrDefault(Function(x) x.ISACTIVE = True And x.NAME_DISPLAY = "VCLAIM")
            Dim uTime As Integer = 0

            If dsDataSetKoneksi IsNot Nothing Then
                uTime = (DateTime.UtcNow - New DateTime(1970, 1, 1, 0, 0, 0)).TotalSeconds
                Dim dsSetKoneksi = oSetKoneksi.Approval_Pengajuan(dsDataSetKoneksi.ALAMATWEB, dsDataSetKoneksi.CONSID, dsDataSetKoneksi.SECREATKEY, dsDataSetKoneksi.USER_KEY, uTime, jsonRequest)

                If dsSetKoneksi <> "" Then
                    Dim allData = JObject.Parse(dsSetKoneksi)

                    Dim CodeResponse As String = String.Empty
                    Dim messageResponse As String = String.Empty

                    CodeResponse = IIf(IsDBNull(allData.Item("metaData").Item("code")) = True, "", allData.Item("metaData").Item("code"))
                    messageResponse = allData("metaData")("message").ToString

                    If CodeResponse = "200" Then
                        REQUEST = jsonRequest
                        RESPONSE = dsSetKoneksi
                        fn_SavePengajuan = True
                    Else
                        fn_SavePengajuan = False
                        MsgBox(CodeResponse & " - " & messageResponse, MsgBoxStyle.Exclamation, Me.Text)
                        Exit Function
                    End If
                Else
                    fn_SavePengajuan = False
                    MsgBox("Update SEP Data Gagal/Kosong", MsgBoxStyle.Exclamation, Me.Text)
                End If
            Else
                fn_SavePengajuan = False
                MsgBox("Koneksi Tidak ditemukan", MsgBoxStyle.Exclamation, Me.Text)
            End If

        Catch oErr As Exception
            fn_SavePengajuan = False
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Function
    Private Function fn_SaveApprovalPengajuan() As Boolean
        Try
            Dim jsonRequest As String = String.Empty
            Dim noKartu As String = String.Empty
            Dim tglSep As String = String.Empty
            Dim jnsPelayanan As String = String.Empty
            Dim keterangan As String = String.Empty
            Dim user As String = String.Empty
            Dim oCustomer As New Reference.clsCustomer

            noKartu = oCustomer.GetData(grdKDCUSTOMER.EditValue).KARTUBPJS
            tglSep = deDATE.DateTime.ToString("yyyy-MM-dd")
            jnsPelayanan = rbJENISRAWAT.SelectedIndex + 1
            keterangan = txtDESCRIPTION.Text.ToString.Trim
            user = sUserID

            jsonRequest = "{ "
            jsonRequest &= """request"" :  { "
            jsonRequest &= """t_sep"": { "
            jsonRequest &= """noKartu"": """ & noKartu & ""","
            jsonRequest &= """tglSep"": """ & tglSep & ""","
            jsonRequest &= """jnsPelayanan"": """ & jnsPelayanan & ""","
            jsonRequest &= """keterangan"": """ & keterangan & ""","
            jsonRequest &= """user"": """ & user & """"
            jsonRequest &= "} "
            jsonRequest &= "} "
            jsonRequest &= "} "

            Dim oSetKoneksi As New Brigging.clsSetKoneksi
            Dim dsDataSetKoneksi = oSetKoneksi.GetData().FirstOrDefault(Function(x) x.ISACTIVE = True And x.NAME_DISPLAY = "VCLAIM")
            Dim uTime As Integer = 0

            If dsDataSetKoneksi IsNot Nothing Then
                uTime = (DateTime.UtcNow - New DateTime(1970, 1, 1, 0, 0, 0)).TotalSeconds
                Dim dsSetKoneksi = oSetKoneksi.Approval_ApprovalPengajuanSEP(dsDataSetKoneksi.ALAMATWEB, dsDataSetKoneksi.CONSID, dsDataSetKoneksi.SECREATKEY, dsDataSetKoneksi.USER_KEY, uTime, jsonRequest)

                If dsSetKoneksi <> "" Then
                    Dim allData = JObject.Parse(dsSetKoneksi)

                    Dim CodeResponse As String = String.Empty
                    Dim messageResponse As String = String.Empty

                    CodeResponse = IIf(IsDBNull(allData.Item("metaData").Item("code")) = True, "", allData.Item("metaData").Item("code"))
                    messageResponse = allData("metaData")("message").ToString

                    If CodeResponse = "200" Then
                        REQUEST = jsonRequest
                        RESPONSE = dsSetKoneksi
                        fn_SaveApprovalPengajuan = True
                    Else
                        fn_SaveApprovalPengajuan = False
                        MsgBox(CodeResponse & " - " & messageResponse, MsgBoxStyle.Exclamation, Me.Text)
                        Exit Function
                    End If
                Else
                    fn_SaveApprovalPengajuan = False
                    MsgBox("Update SEP Data Gagal/Kosong", MsgBoxStyle.Exclamation, Me.Text)
                End If
            Else
                fn_SaveApprovalPengajuan = False
                MsgBox("Koneksi Tidak ditemukan", MsgBoxStyle.Exclamation, Me.Text)
            End If

        Catch oErr As Exception
            fn_SaveApprovalPengajuan = False
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Function
    Private Function fn_Save() As Boolean
        Try
            ' ***** HEADER *****
            Dim ds = oApproval_Penjaminan_SEP.GetStructureHeader
            With ds
                Try
                    .DATECREATED = oApproval_Penjaminan_SEP.GetData(sNoId).DATECREATED
                Catch oErr As Exception
                    .DATECREATED = Now
                End Try
                .DATEUPDATED = Now
                .DATE = deDATE.DateTime
                .KDAPPROVAL = sNoId
                .CATEGORY = rbCATEGORY.SelectedIndex
                .JENISRAWAT = rbJENISRAWAT.SelectedIndex
                .JENISPENGAJUAN = cboJENISPENGAJUAN.SelectedIndex
                .KDCUSTOMER = grdKDCUSTOMER.EditValue
                .DESCRIPTION = txtDESCRIPTION.Text.Trim.ToUpper
                .KDUSER = sUserID
                .REQUEST = REQUEST
                .RESPON = RESPONSE

            End With

            If oFormMode = FORM_MODE.FORM_MODE_ADD Then
                Try
                    fn_Save = oApproval_Penjaminan_SEP.InsertData(ds)
                Catch oErr As Exception
                    MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            ElseIf oFormMode = FORM_MODE.FORM_MODE_EDIT Then
                Try
                    fn_Save = oApproval_Penjaminan_SEP.UpdateData(ds)
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

        REQUEST = String.Empty
        RESPONSE = String.Empty

        If rbCATEGORY.SelectedIndex = 0 Then
            If fn_SavePengajuan() = True Then
                If fn_Save() = False Then
                    MsgBox(Statement.SaveFail, MsgBoxStyle.Exclamation, Me.Text)
                Else
                    MsgBox(Statement.SaveSuccess, MsgBoxStyle.Information, Me.Text)
                    sStatusSave = "NEW"
                    Me.Close()
                End If
            End If
        Else
            If fn_SaveApprovalPengajuan() = True Then
                If fn_Save() = False Then
                    MsgBox(Statement.SaveFail, MsgBoxStyle.Exclamation, Me.Text)
                Else
                    MsgBox(Statement.SaveSuccess, MsgBoxStyle.Information, Me.Text)
                    sStatusSave = "NEW"
                    Me.Close()
                End If
            End If
        End If

    End Sub
    Private Sub btnSaveClose_Click() Handles btnSaveClose.ItemClick
        If fn_Validate() = False Then Exit Sub
        If MsgBox(Statement.SaveQuestion, MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, Me.Text) = MsgBoxResult.No Then Exit Sub

        REQUEST = String.Empty
        RESPONSE = String.Empty

        If rbCATEGORY.SelectedIndex = 0 Then
            If fn_SavePengajuan() = True Then
                If fn_Save() = False Then
                    MsgBox(Statement.SaveFail, MsgBoxStyle.Exclamation, Me.Text)
                Else
                    MsgBox(Statement.SaveSuccess, MsgBoxStyle.Information, Me.Text)
                    Me.Close()
                End If
            End If
        Else
            If fn_SaveApprovalPengajuan() = True Then
                If fn_Save() = False Then
                    MsgBox(Statement.SaveFail, MsgBoxStyle.Exclamation, Me.Text)
                Else
                    MsgBox(Statement.SaveSuccess, MsgBoxStyle.Information, Me.Text)
                    Me.Close()
                End If
            End If
        End If
    End Sub
    Private Sub btnClose_Click() Handles btnClose.ItemClick
        Me.Close()
    End Sub
#End Region
#Region "Lookup / Event"
    Private Sub txtCARI_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtCARI.KeyPress
        If Asc(e.KeyChar) = 13 Then
            PopUP = True
            fn_LoadKDCUSTOMER(txtCARI.Text)
            txtCARI.ResetText()
        End If
    End Sub
    Private Sub fn_LoadKDCUSTOMER(ByVal Parameter As String)
        Dim oCUSTOMER As New Reference.clsCustomer
        Try
            grdKDCUSTOMER.Properties.DataSource = oCUSTOMER.GetDataCustomerByApproval(Parameter, cboCARI.SelectedIndex).ToList()
            grdKDCUSTOMER.Properties.ValueMember = "KDCUSTOMER"
            grdKDCUSTOMER.Properties.DisplayMember = "NAME_DISPLAY"

            If PopUP = True Then
                grdKDCUSTOMER.ShowPopup()
            End If


        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub rbCATEGORY_SelectedIndexChanged(sender As Object, e As EventArgs) Handles rbCATEGORY.SelectedIndexChanged
        If isLoad = True Then
            If rbCATEGORY.SelectedIndex = 0 Then
                lJENISPENGAJUAN.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            Else
                lJENISPENGAJUAN.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            End If
        End If
    End Sub
#End Region
End Class