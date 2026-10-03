Imports DataAccess
Imports UI.WIN.MAIN.My.Resources
Imports System.Linq

Public Class frmSetKoneksi
#Region "Declaration"
    Private oFormMode As FORM_MODE = FORM_MODE.FORM_MODE_VIEW
    Private sNoId As String
    Private isLoad As Boolean = False
    Private oSetKoneksi As New Brigging.clsSetKoneksi
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
            Me.Text = SetKoneksi.TITLE

            lNAME_DISPLAY.Text = SetKoneksi.NAME_DISPLAY & " *"
            lPPKPELAYANAN.Text = SetKoneksi.PPKPELAYANAN & " *"
            lCONSID.Text = SetKoneksi.CONSID & " *"
            lSCREATKEY.Text = SetKoneksi.SCREATKEY & " *"
            lALAMATWEB.Text = SetKoneksi.ALMATWEB & " *"
            lKODERS.Text = "Folder Suara" & " *"
            lPASSWORDRS.Text = SetKoneksi.PASSWORDRS & " *"
            lFOLDER.Text = SetKoneksi.FOLDER
            lREMARKS.Text = SetKoneksi.REMARKS
            lKONEKSI_SIMRS.Text = "Koneksi SIMRS"
            chkISACTIVE.Text = SetKoneksi.ISACTIVE

            btnSaveNew.Caption = Caption.FormSaveNew
            btnSaveClose.Caption = Caption.FormSaveClose
            btnClose.Caption = Caption.FormClose
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub Form_Closing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        sCode = cboNAME_DISPLAY.Text.Trim.ToUpper
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

        cboNAME_DISPLAY.Properties.ReadOnly = Status
        txtPPKPELAYANAN.Properties.ReadOnly = Status
        txtCONSID.Properties.ReadOnly = Status
        txtSCREATKEY.Properties.ReadOnly = Status
        txtALAMATWEB.Properties.ReadOnly = Status
        txtKODERS.Properties.ReadOnly = Status
        txtPASSWORDRS.Properties.ReadOnly = Status
        txtFOLDER.Properties.ReadOnly = Status
        txtREMARKS.Properties.ReadOnly = Status
        chkISACTIVE.Properties.ReadOnly = Status
        txtKONEKSI_SIMRS.Properties.ReadOnly = Status
        txtALAMAT_ECLAIM.Properties.ReadOnly = Status
        txtGENERATE_ECLAIM.Properties.ReadOnly = Status
        txtTUSLAH.Properties.ReadOnly = Status
        txtUSER_KEY.Properties.ReadOnly = Status
    End Sub
    Private Sub fn_EmptyMe()
        cboNAME_DISPLAY.SelectedIndex = 0
        txtPPKPELAYANAN.ResetText()
        txtCONSID.ResetText()
        txtSCREATKEY.ResetText()
        txtALAMATWEB.ResetText()
        txtKODERS.ResetText()
        txtPASSWORDRS.ResetText()
        txtFOLDER.ResetText()
        txtREMARKS.ResetText()
        chkISACTIVE.Checked = True
        txtKONEKSI_SIMRS.ResetText()
        txtALAMAT_ECLAIM.ResetText()
        txtGENERATE_ECLAIM.ResetText()
        txtUSER_KEY.ResetText()
    End Sub
    Private Sub fn_LoadData()
        Try
            Dim ds = oSetKoneksi.GetData(sNoId)

            With ds
                cboNAME_DISPLAY.Text = .NAME_DISPLAY
                txtPPKPELAYANAN.Text = .PPKPELAYANAN
                txtCONSID.Text = .CONSID
                txtSCREATKEY.Text = .SECREATKEY
                txtALAMATWEB.Text = .ALAMATWEB
                txtKODERS.Text = .KODERS
                txtPASSWORDRS.Text = .PASSWORDRS
                txtFOLDER.Text = .FOLDER
                txtREMARKS.Text = .REMARKS
                chkISACTIVE.Checked = .ISACTIVE
                txtKONEKSI_SIMRS.Text = .KONEKSI_SIMRS
                txtALAMAT_ECLAIM.Text = .ALAMAT_ECLAIM
                txtGENERATE_ECLAIM.Text = .GENERATE_ECLAIM
                txtTUSLAH.Text = .TUSLAHFARMASI
                txtUSER_KEY.Text = .USER_KEY
                'IMAGE
                Try
                    Dim img = (From x In oSetKoneksi.GetData
                               Where x.KDKONEKSI = sNoId
                               Select x.GAMBAR).Single

                    picGAMBAR.Image = ByteArrayToImage(img.ToArray())
                Catch oErr As Exception
                    'MsgBox("Load Image : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try

            End With
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Function ByteArrayToImage(ByVal byteArrayIn() As Byte) As Image
        Using ms As New System.IO.MemoryStream(byteArrayIn)
            Dim returnImage = Image.FromStream(ms)
            Return returnImage
        End Using
    End Function
    Private Function ImageToByteArray(ByVal ImageIn As System.Drawing.Image) As Byte()

        Using ms As New System.IO.MemoryStream
            ImageIn.Save(ms, System.Drawing.Imaging.ImageFormat.Gif)
            Return ms.ToArray
        End Using
    End Function
    Private Function fn_Validate() As Boolean
        Try
            fn_Validate = True
            If cboNAME_DISPLAY.Text = String.Empty Then
                cboNAME_DISPLAY.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                cboNAME_DISPLAY.ErrorText = Statement.ErrorRequired

                cboNAME_DISPLAY.Focus()
                fn_Validate = False
                Exit Function
            End If
            If txtPPKPELAYANAN.Text = String.Empty Then
                txtPPKPELAYANAN.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                txtPPKPELAYANAN.ErrorText = Statement.ErrorRequired

                txtPPKPELAYANAN.Focus()
                fn_Validate = False
                Exit Function
            End If
            If txtCONSID.Text = String.Empty Then
                txtCONSID.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                txtCONSID.ErrorText = Statement.ErrorRequired

                txtCONSID.Focus()
                fn_Validate = False
                Exit Function
            End If
            If txtSCREATKEY.Text = String.Empty Then
                txtSCREATKEY.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                txtSCREATKEY.ErrorText = Statement.ErrorRequired

                txtSCREATKEY.Focus()
                fn_Validate = False
                Exit Function
            End If
            If txtALAMATWEB.Text = String.Empty Then
                txtALAMATWEB.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                txtALAMATWEB.ErrorText = Statement.ErrorRequired

                txtALAMATWEB.Focus()
                fn_Validate = False
                Exit Function
            End If
            If txtKODERS.Text = String.Empty Then
                txtKODERS.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                txtKODERS.ErrorText = Statement.ErrorRequired

                txtKODERS.Focus()
                fn_Validate = False
                Exit Function
            End If
            If txtPASSWORDRS.Text = String.Empty Then
                txtPASSWORDRS.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                txtPASSWORDRS.ErrorText = Statement.ErrorRequired

                txtPASSWORDRS.Focus()
                fn_Validate = False
                Exit Function
            End If
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Function
    Private Function fn_Save() As Boolean
        Try
            ' ***** HEADER *****
            Dim ds = oSetKoneksi.GetStructureHeader
            With ds
                Try
                    .DATECREATED = oSetKoneksi.GetData(sNoId).DATECREATED
                Catch oErr As Exception
                    .DATECREATED = Now
                End Try
                .DATEUPDATED = Now

                .KDKONEKSI = sNoId
                .NAME_DISPLAY = cboNAME_DISPLAY.Text
                .PPKPELAYANAN = txtPPKPELAYANAN.Text
                .CONSID = txtCONSID.Text
                .SECREATKEY = txtSCREATKEY.Text
                .ALAMATWEB = txtALAMATWEB.Text
                .KODERS = txtKODERS.Text
                .PASSWORDRS = txtPASSWORDRS.Text
                .FOLDER = txtFOLDER.Text
                .REMARKS = txtREMARKS.Text
                .ISACTIVE = chkISACTIVE.Checked
                .CONSID_APLICARE = ""
                .SECREATKEY_APLICARE = ""
                .ALAMATWEB_APLICARE = ""
                .ALAMAT_ECLAIM = txtALAMAT_ECLAIM.Text
                .GENERATE_ECLAIM = txtGENERATE_ECLAIM.Text
                .KONEKSI_SIMRS = txtKONEKSI_SIMRS.Text
                .TUSLAHFARMASI = CDec(txtTUSLAH.Text)
                .USER_KEY = txtUSER_KEY.Text

                'IMAGE
                Try
                    Dim data As Byte() = System.IO.File.ReadAllBytes(picGAMBAR.ImageLocation)
                    .GAMBAR = data
                Catch oErr As Exception
                    If oFormMode = FORM_MODE.FORM_MODE_EDIT Then
                        Try
                            Dim img = (From x In oSetKoneksi.GetData
                                       Where x.KDKONEKSI = sNoId
                                       Select x.GAMBAR).Single

                            .GAMBAR = img
                        Catch ex1 As Exception

                        End Try
                    End If
                End Try
            End With

            If oFormMode = FORM_MODE.FORM_MODE_ADD Then
                Try
                    fn_Save = oSetKoneksi.InsertData(ds)
                Catch oErr As Exception
                    MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            ElseIf oFormMode = FORM_MODE.FORM_MODE_EDIT Then
                Try
                    fn_Save = oSetKoneksi.UpdateData(ds)
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
    Private Sub picGAMBAR_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles picGAMBAR.Click
        If OpenFileDialog1.ShowDialog = Windows.Forms.DialogResult.OK Then
            picGAMBAR.Load(OpenFileDialog1.FileName)
            picGAMBAR.Update()
        End If
    End Sub
#End Region
End Class