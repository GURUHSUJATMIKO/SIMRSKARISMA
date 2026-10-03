Imports DataAccess
Imports UI.WIN.MAIN.My.Resources
Imports System.Linq
Imports Newtonsoft.Json.Linq

Public Class frmRujukan
#Region "Declaration"
    Private oFormMode As FORM_MODE = FORM_MODE.FORM_MODE_VIEW
    Private sNoId As String
    Private isLoad As Boolean = False
    Private oRujukan As New Admission.clsRujukan
    Private PopUP As Boolean = False

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
            Me.Text = Rujukan.TITLE

            lKDRUJUKAN.Text = Rujukan.KDRUJUKAN
            lKDPENDAFATRAN.Text = Rujukan.KDPENDAFTARAN & " *"
            lDATE.Text = Rujukan.TANGGAL
            lRENCANATGLKUNJUNGAN.Text = "Tgl Rencana Kunjungan"
            lKDPPK.Text = Rujukan.KDPPK
            lCATATAN.Text = Rujukan.CATATAN
            lKDDEPARTMENT.Text = Rujukan.KDDEPARTMENT
            lKDDIAGNOSA.Text = Rujukan.KDDIAGNOSA
            lTIPERUJUKAN.Text = Rujukan.TIPERUJUKAN
            lNOMORSEP.Text = Rujukan.NOMORSEP
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
        fn_LoadKDDEPARTMENT()
        fn_LoadKDDIAGNOSA()
        fn_LoadKDPPK()

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

        grdKDPENDAFTARAN.Properties.ReadOnly = Status
        deDATE.Properties.ReadOnly = Status
        deDATERENCANAKUNJUNGAN.Properties.ReadOnly = Status
        txtNOMORSEP.Properties.ReadOnly = Status
        grdKDPPK.Properties.ReadOnly = Status
        grdKDDEPARTMENT.Properties.ReadOnly = Status
        grdKDPPK.Properties.ReadOnly = Status
        grdKDDIAGNOSA.Properties.ReadOnly = Status
        cboTIPERUJUKAN.Properties.ReadOnly = Status
        txtCATATAN.Properties.ReadOnly = Status
    End Sub
    Private Sub fn_EmptyMe()
        txtCODE.Text = "<--- AUTO --->"

        grdKDPENDAFTARAN.ResetText()
        deDATE.DateTime = Now
        deDATERENCANAKUNJUNGAN.DateTime = Now
        txtNOMORSEP.ResetText()
        grdKDDEPARTMENT.ResetText()
        grdKDPPK.ResetText()
        grdKDDIAGNOSA.ResetText()
        txtCATATAN.ResetText()

    End Sub
    Private Sub fn_LoadData()
        Try
            Dim ds = oRujukan.GetData(sNoId)

            With ds
                txtCODE.Text = sNoId
                cboCARI.SelectedIndex = 2
                fn_LoadKDPENDAFTARAN(.KDPENDAFTARAN)
                grdKDPENDAFTARAN.Text = .KDPENDAFTARAN
                txtNOMORSEP.Text = .NOMORSEP
                deDATE.DateTime = .DATE
                deDATERENCANAKUNJUNGAN.DateTime = .DATE_RENCANA
                grdKDPPK.Text = .KDPPK
                txtCATATAN.Text = .CATATAN
                grdKDDIAGNOSA.Text = .KDDIAGNOSA
                cboTIPERUJUKAN.SelectedIndex = .TIPERUJUKAN
                grdKDDEPARTMENT.Text = .KDDEPARTMENT

            End With
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Function fn_Validate() As Boolean
        Try
            fn_Validate = True
            If txtCATATAN.Text = String.Empty Then
                txtCATATAN.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                txtCATATAN.ErrorText = Statement.ErrorRequired

                txtCATATAN.Focus()
                fn_Validate = False
                Exit Function
            End If
            If grdKDPENDAFTARAN.Text = String.Empty Then
                grdKDPENDAFTARAN.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                grdKDPENDAFTARAN.ErrorText = Statement.ErrorRequired

                grdKDPENDAFTARAN.Focus()
                fn_Validate = False
                Exit Function
            End If

            'If txtNOMORSEP.Text = String.Empty Then
            '    txtNOMORSEP.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
            '    txtNOMORSEP.ErrorText = Statement.ErrorRequired

            '    txtNOMORSEP.Focus()
            '    fn_Validate = False
            '    Exit Function
            'End If
            If grdKDDEPARTMENT.Text = String.Empty Then
                grdKDDEPARTMENT.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                grdKDDEPARTMENT.ErrorText = Statement.ErrorRequired

                grdKDDEPARTMENT.Focus()
                fn_Validate = False
                Exit Function
            End If
            If grdKDPPK.Text = String.Empty Then
                grdKDPPK.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                grdKDPPK.ErrorText = Statement.ErrorRequired

                grdKDPPK.Focus()
                fn_Validate = False
                Exit Function
            End If
            If grdKDDIAGNOSA.Text = String.Empty Then
                grdKDDIAGNOSA.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                grdKDDIAGNOSA.ErrorText = Statement.ErrorRequired

                grdKDDIAGNOSA.Focus()
                fn_Validate = False
                Exit Function
            End If

        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Function
    Private Function fn_Save() As Boolean
        Try
            Dim jsonRequest As String = String.Empty
            Dim jsonResponse As String = String.Empty
            Dim oSetKoneksi As New Brigging.clsSetKoneksi

            If txtNOMORSEP.Text <> "" Then
                If sAktiveVersi2 = False Then
                    Dim uTime As Integer = (DateTime.UtcNow - New DateTime(1970, 1, 1, 0, 0, 0)).TotalSeconds

                    If oFormMode = FORM_MODE.FORM_MODE_ADD Then
                        jsonRequest = fn_RequestInsertRujukan()

                        If jsonRequest <> "" Then
                            jsonResponse = fn_CreateRujukan(jsonRequest, uTime)
                            If jsonResponse <> "" Then
                                Dim dsDataSetKoneksi = oSetKoneksi.GetData().FirstOrDefault(Function(x) x.ISACTIVE = True And x.NAME_DISPLAY = "VCLAIM")
                                Dim DataDecrypt = JObject.Parse(oSetKoneksi.Decrypt(jsonResponse, dsDataSetKoneksi.CONSID & dsDataSetKoneksi.SECREATKEY & uTime))
                                txtCODE.Text = DataDecrypt.Item("rujukan")("noRujukan").ToString()
                            Else
                                fn_Save = False
                                Exit Function
                            End If
                        Else
                            fn_Save = False
                            Exit Function
                        End If
                    Else
                        jsonRequest = fn_RequestUpdateRujukan()
                        If jsonRequest <> "" Then
                            jsonResponse = fn_UpdateRujukan(jsonRequest, uTime)
                            If jsonResponse <> "" Then

                            Else
                                fn_Save = False
                                Exit Function
                            End If
                        Else
                            fn_Save = False
                            Exit Function
                        End If
                    End If

                Else
                    Dim uTime As Integer = (DateTime.UtcNow - New DateTime(1970, 1, 1, 0, 0, 0)).TotalSeconds

                    If oFormMode = FORM_MODE.FORM_MODE_ADD Then
                        jsonRequest = fn_RequestInsertRujukanv2()

                        If jsonRequest <> "" Then
                            jsonResponse = fn_CreateRujukanV2(jsonRequest, uTime)
                            If jsonResponse <> "" Then
                                Dim dsDataSetKoneksi = oSetKoneksi.GetData().FirstOrDefault(Function(x) x.ISACTIVE = True And x.NAME_DISPLAY = "VCLAIM")
                                Dim DataDecrypt = JObject.Parse(oSetKoneksi.Decrypt(jsonResponse, dsDataSetKoneksi.CONSID & dsDataSetKoneksi.SECREATKEY & uTime))
                                txtCODE.Text = DataDecrypt.Item("rujukan")("noRujukan").ToString()
                            Else
                                fn_Save = False
                                Exit Function
                            End If
                        Else
                            fn_Save = False
                            Exit Function
                        End If
                    Else
                        jsonRequest = fn_RequestUpdateRujukanV2()
                        If jsonRequest <> "" Then
                            jsonResponse = fn_UpdateRujukanV2(jsonRequest, uTime)
                            If jsonResponse <> "" Then

                            Else
                                fn_Save = False
                                Exit Function
                            End If
                        Else
                            fn_Save = False
                            Exit Function
                        End If
                    End If
                End If
            Else
                txtCODE.ResetText()
            End If

            ' ***** HEADER *****

            Dim ds = oRujukan.GetStructureHeader
            With ds
                Try
                    .DATECREATED = oRujukan.GetData(sNoId).DATECREATED
                Catch oErr As Exception
                    .DATECREATED = Now
                End Try
                .DATEUPDATED = Now
                .KDRUJUKAN = txtCODE.Text
                .KDPENDAFTARAN = grdKDPENDAFTARAN.EditValue
                .NOMORSEP = txtNOMORSEP.Text.Trim.ToUpper
                .DATE = deDATE.DateTime
                .DATE_RENCANA = deDATERENCANAKUNJUNGAN.DateTime
                .KDPPK = grdKDPPK.EditValue
                .KDDEPARTMENT = grdKDDEPARTMENT.EditValue
                .KDPPK = grdKDPPK.EditValue
                .KDDIAGNOSA = grdKDDIAGNOSA.EditValue
                .CATATAN = txtCATATAN.Text.Trim.ToUpper
                .ISDELETE = False
                .KDUSER = sUserID
                .REQUEST = jsonRequest
                .RESPONSE = jsonResponse
            End With

            If oFormMode = FORM_MODE.FORM_MODE_ADD Then
                Try
                    txtCODE.Text = oRujukan.InsertData(ds)
                    If txtCODE.Text = "" Then
                        fn_Save = False
                    Else
                        fn_Save = True
                        CetakRegister(txtCODE.Text)
                    End If

                Catch oErr As Exception
                    MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            ElseIf oFormMode = FORM_MODE.FORM_MODE_EDIT Then
                Try
                    fn_Save = oRujukan.UpdateData(ds)
                    CetakRegister(txtCODE.Text)
                Catch oErr As Exception
                    MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            End If
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            fn_Save = False
        End Try
    End Function
    Private Sub CetakRegister(ByVal KDRUJUKAN As String)
        'Dim rpt As New xtraRujukan

        'Dim ds = oRujukan.GetData(KDRUJUKAN)
        'rpt.bindingSource.DataSource = ds
        'Dim printTool As New DevExpress.XtraReports.UI.ReportPrintTool(rpt)
        'printTool.PrintDialog()
    End Sub
    Private Function fn_RequestInsertRujukan()
        Try
            Dim oSetKoneksi As New Brigging.clsSetKoneksi
            Dim oDaftar As New Admission.clsPendaftaran
            Dim oDepartment As New Reference.clsDepartment
            Dim jsonRequest As String = String.Empty
            Dim oRujukan As New Reference.clsPPK

            jsonRequest = " { "
            jsonRequest &= """request"" :  { "
            jsonRequest &= """t_rujukan"": { "
            jsonRequest &= """noSep"": """ & txtNOMORSEP.Text.Trim.ToUpper & """, "
            jsonRequest &= """tglRujukan"": """ & deDATE.DateTime.ToString("yyyy-MM-dd") & """, "
            jsonRequest &= """ppkDirujuk"": """ & oRujukan.GetData(grdKDPPK.EditValue).KODEFASKES & """, "
            jsonRequest &= """jnsPelayanan"": """ & IIf(oDaftar.GetData(grdKDPENDAFTARAN.EditValue).CATEGORY = 0, 1, 2) & """, "
            jsonRequest &= """catatan"": """ & txtCATATAN.Text.Trim.ToUpper & """, "
            jsonRequest &= """diagRujukan"": """ & grdKDDIAGNOSA.EditValue & """, "
            jsonRequest &= """tipeRujukan"": """ & cboTIPERUJUKAN.SelectedIndex & """, "
            jsonRequest &= """poliRujukan"": """ & oDepartment.GetData(grdKDDEPARTMENT.EditValue).KDDEPARTMENT_BPJS & """, "
            jsonRequest &= """user"": """ & sUserID & """ "
            jsonRequest &= "} "
            jsonRequest &= "} "
            jsonRequest &= "}  "

            fn_RequestInsertRujukan = jsonRequest

        Catch oErr As Exception
            fn_RequestInsertRujukan = ""
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Function
    Private Function fn_RequestInsertRujukanv2()
        Try
            Dim oSetKoneksi As New Brigging.clsSetKoneksi
            Dim oDaftar As New Admission.clsPendaftaran
            Dim oDepartment As New Reference.clsDepartment
            Dim jsonRequest As String = String.Empty
            Dim oRujukan As New Reference.clsPPK

            jsonRequest = " { "
            jsonRequest &= """request"" :  { "
            jsonRequest &= """t_rujukan"": { "
            jsonRequest &= """noSep"": """ & txtNOMORSEP.Text.Trim.ToUpper & """, "
            jsonRequest &= """tglRujukan"": """ & deDATE.DateTime.ToString("yyyy-MM-dd") & """, "
            jsonRequest &= """tglRencanaKunjungan"": """ & deDATERENCANAKUNJUNGAN.DateTime.ToString("yyyy-MM-dd") & """, "
            jsonRequest &= """ppkDirujuk"": """ & oRujukan.GetData(grdKDPPK.EditValue).KODEFASKES & """, "
            jsonRequest &= """jnsPelayanan"": """ & IIf(oDaftar.GetData(grdKDPENDAFTARAN.EditValue).CATEGORY = 0, 1, 2) & """, "
            jsonRequest &= """catatan"": """ & txtCATATAN.Text.Trim.ToUpper & """, "
            jsonRequest &= """diagRujukan"": """ & grdKDDIAGNOSA.EditValue & """, "
            jsonRequest &= """tipeRujukan"": """ & cboTIPERUJUKAN.SelectedIndex & """, "
            jsonRequest &= """poliRujukan"": """ & oDepartment.GetData(grdKDDEPARTMENT.EditValue).KDDEPARTMENT_BPJS & """, "
            jsonRequest &= """user"": """ & sUserID & """ "
            jsonRequest &= "} "
            jsonRequest &= "} "
            jsonRequest &= "}  "

            fn_RequestInsertRujukanv2 = jsonRequest

        Catch oErr As Exception
            fn_RequestInsertRujukanv2 = ""
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Function
    Private Function fn_CreateRujukan(ByVal jsonRequest As String, ByVal uTime As Integer) As String
        Try
            Dim oSetKoneksi As New Brigging.clsSetKoneksi

            Dim dsDataSetKoneksi = oSetKoneksi.GetData().FirstOrDefault(Function(x) x.ISACTIVE = True And x.NAME_DISPLAY = "VCLAIM")

            If dsDataSetKoneksi IsNot Nothing Then
                Dim dsSetKoneksi = oSetKoneksi.InsertRujukan(dsDataSetKoneksi.ALAMATWEB, dsDataSetKoneksi.CONSID, dsDataSetKoneksi.SECREATKEY, dsDataSetKoneksi.USER_KEY, uTime, jsonRequest)

                If dsSetKoneksi <> "" Then
                    Dim allData = JObject.Parse(dsSetKoneksi)

                    Dim CodeResponse As String = String.Empty
                    Dim messageResponse As String = String.Empty

                    CodeResponse = IIf(IsDBNull(allData.Item("metaData").Item("code")) = True, "", allData.Item("metaData").Item("code"))
                    messageResponse = allData("metaData")("message").ToString

                    If CodeResponse = "200" Then
                        fn_CreateRujukan = allData("response")
                    Else
                        fn_CreateRujukan = ""
                        MsgBox(CodeResponse & " - " & messageResponse, MsgBoxStyle.Exclamation, Me.Text)
                    End If
                Else
                    fn_CreateRujukan = ""
                    MsgBox("Koneksi Rujukan Kosong", MsgBoxStyle.Exclamation, Me.Text)
                End If
            Else
                fn_CreateRujukan = ""
                MsgBox("Koneksi Tidak ditemukan", MsgBoxStyle.Exclamation, Me.Text)
            End If
        Catch oErr As Exception
            fn_CreateRujukan = ""
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Function
    Private Function fn_CreateRujukanV2(ByVal jsonRequest As String, ByVal uTime As Integer) As String
        Try
            Dim oSetKoneksi As New Brigging.clsSetKoneksi

            Dim dsDataSetKoneksi = oSetKoneksi.GetData().FirstOrDefault(Function(x) x.ISACTIVE = True And x.NAME_DISPLAY = "VCLAIM")

            If dsDataSetKoneksi IsNot Nothing Then
                Dim dsSetKoneksi = oSetKoneksi.InsertRujukanV2(dsDataSetKoneksi.ALAMATWEB, dsDataSetKoneksi.CONSID, dsDataSetKoneksi.SECREATKEY, dsDataSetKoneksi.USER_KEY, uTime, jsonRequest)

                If dsSetKoneksi <> "" Then
                    Dim allData = JObject.Parse(dsSetKoneksi)

                    Dim CodeResponse As String = String.Empty
                    Dim messageResponse As String = String.Empty

                    CodeResponse = IIf(IsDBNull(allData.Item("metaData").Item("code")) = True, "", allData.Item("metaData").Item("code"))
                    messageResponse = allData("metaData")("message").ToString

                    If CodeResponse = "200" Then
                        fn_CreateRujukanV2 = allData("response")
                    Else
                        fn_CreateRujukanV2 = ""
                        MsgBox(CodeResponse & " - " & messageResponse, MsgBoxStyle.Exclamation, Me.Text)
                    End If
                Else
                    fn_CreateRujukanV2 = ""
                    MsgBox("Koneksi Rujukan Kosong", MsgBoxStyle.Exclamation, Me.Text)
                End If
            Else
                fn_CreateRujukanV2 = ""
                MsgBox("Koneksi Tidak ditemukan", MsgBoxStyle.Exclamation, Me.Text)
            End If
        Catch oErr As Exception
            fn_CreateRujukanV2 = ""
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Function
    Private Function fn_RequestUpdateRujukan()
        Try
            Dim oSetKoneksi As New Brigging.clsSetKoneksi
            Dim oDaftar As New Admission.clsPendaftaran
            Dim oDepartment As New Reference.clsDepartment
            Dim jsonRequest As String = String.Empty
            Dim oRujukan As New Reference.clsPPK

            jsonRequest = " { "
            jsonRequest &= """request"" :  { "
            jsonRequest &= """t_rujukan"": { "
            jsonRequest &= """noRujukan"": """ & txtCODE.Text.Trim.ToUpper & """, "
            jsonRequest &= """ppkDirujuk"": """ & oRujukan.GetData(grdKDPPK.EditValue).KODEFASKES & """, "
            jsonRequest &= """tipe"": """ & cboTIPERUJUKAN.SelectedIndex & """, "
            jsonRequest &= """jnsPelayanan"": """ & IIf(oDaftar.GetData(grdKDPENDAFTARAN.EditValue).CATEGORY = 0, 1, 2) & """, "
            jsonRequest &= """catatan"": """ & txtCATATAN.Text.Trim.ToUpper & """, "
            jsonRequest &= """diagRujukan"": """ & grdKDDIAGNOSA.EditValue & """, "
            jsonRequest &= """tipeRujukan"": """ & cboTIPERUJUKAN.SelectedIndex & """, "
            jsonRequest &= """poliRujukan"": """ & oDepartment.GetData(grdKDDEPARTMENT.EditValue).KDDEPARTMENT_BPJS & """, "
            jsonRequest &= """user"": """ & sUserID & """ "
            jsonRequest &= "} "
            jsonRequest &= "} "
            jsonRequest &= "}  "

            fn_RequestUpdateRujukan = jsonRequest

        Catch oErr As Exception
            fn_RequestUpdateRujukan = ""
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Function
    Private Function fn_RequestUpdateRujukanV2()
        Try
            Dim oSetKoneksi As New Brigging.clsSetKoneksi
            Dim oDaftar As New Admission.clsPendaftaran
            Dim oDepartment As New Reference.clsDepartment
            Dim jsonRequest As String = String.Empty
            Dim oRujukan As New Reference.clsPPK

            jsonRequest = " { "
            jsonRequest &= """request"" :  { "
            jsonRequest &= """t_rujukan"": { "
            jsonRequest &= """noRujukan"": """ & txtCODE.Text.Trim.ToUpper & """, "
            jsonRequest &= """tglRujukan"": """ & deDATE.DateTime.ToString("yyyy-MM-dd") & """, "
            jsonRequest &= """tglRencanaKunjungan"": """ & deDATERENCANAKUNJUNGAN.DateTime.ToString("yyyy-MM-dd") & """, "
            jsonRequest &= """ppkDirujuk"": """ & oRujukan.GetData(grdKDPPK.EditValue).KODEFASKES & """, "
            jsonRequest &= """jnsPelayanan"": """ & IIf(oDaftar.GetData(grdKDPENDAFTARAN.EditValue).CATEGORY = 0, 1, 2) & """, "
            jsonRequest &= """catatan"": """ & txtCATATAN.Text.Trim.ToUpper & """, "
            jsonRequest &= """diagRujukan"": """ & grdKDDIAGNOSA.EditValue & """, "
            'jsonRequest &= """tipe"": """ & cboTIPERUJUKAN.SelectedIndex & """, "
            jsonRequest &= """tipeRujukan"": """ & cboTIPERUJUKAN.SelectedIndex & """, "
            jsonRequest &= """poliRujukan"": """ & oDepartment.GetData(grdKDDEPARTMENT.EditValue).KDDEPARTMENT_BPJS & """, "
            jsonRequest &= """user"": """ & sUserID & """ "
            jsonRequest &= "} "
            jsonRequest &= "} "
            jsonRequest &= "}  "

            fn_RequestUpdateRujukanV2 = jsonRequest

        Catch oErr As Exception
            fn_RequestUpdateRujukanV2 = ""
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Function
    Private Function fn_UpdateRujukan(ByVal jsonRequest As String, ByVal uTime As Integer) As String
        Try
            Dim oSetKoneksi As New Brigging.clsSetKoneksi
            Dim dsDataSetKoneksi = oSetKoneksi.GetData().FirstOrDefault(Function(x) x.ISACTIVE = True And x.NAME_DISPLAY = "VCLAIM")

            If dsDataSetKoneksi IsNot Nothing Then
                Dim dsSetKoneksi = oSetKoneksi.UpdateRujukan(dsDataSetKoneksi.ALAMATWEB, dsDataSetKoneksi.CONSID, dsDataSetKoneksi.SECREATKEY, dsDataSetKoneksi.USER_KEY, uTime, jsonRequest)

                If dsSetKoneksi <> "" Then
                    Dim allData = JObject.Parse(dsSetKoneksi)

                    Dim CodeResponse As String = String.Empty
                    Dim messageResponse As String = String.Empty

                    CodeResponse = IIf(IsDBNull(allData.Item("metaData").Item("code")) = True, "", allData.Item("metaData").Item("code"))
                    messageResponse = allData("metaData")("message").ToString

                    If CodeResponse = "200" Then
                        fn_UpdateRujukan = allData("response")
                    Else
                        fn_UpdateRujukan = ""
                        MsgBox(CodeResponse & " - " & messageResponse, MsgBoxStyle.Exclamation, Me.Text)
                        Exit Function
                    End If
                Else
                    fn_UpdateRujukan = ""
                    MsgBox("Koneksi Rujukan Kosong", MsgBoxStyle.Exclamation, Me.Text)
                End If
            Else
                fn_UpdateRujukan = ""
                MsgBox("Koneksi Tidak ditemukan", MsgBoxStyle.Exclamation, Me.Text)
            End If
        Catch oErr As Exception
            fn_UpdateRujukan = ""
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Function
    Private Function fn_UpdateRujukanV2(ByVal jsonRequest As String, ByVal uTime As Integer) As String
        Try
            Dim oSetKoneksi As New Brigging.clsSetKoneksi
            Dim dsDataSetKoneksi = oSetKoneksi.GetData().FirstOrDefault(Function(x) x.ISACTIVE = True And x.NAME_DISPLAY = "VCLAIM")

            If dsDataSetKoneksi IsNot Nothing Then
                Dim dsSetKoneksi = oSetKoneksi.UpdateRujukanV2(dsDataSetKoneksi.ALAMATWEB, dsDataSetKoneksi.CONSID, dsDataSetKoneksi.SECREATKEY, dsDataSetKoneksi.USER_KEY, uTime, jsonRequest)

                If dsSetKoneksi <> "" Then
                    Dim allData = JObject.Parse(dsSetKoneksi)

                    Dim CodeResponse As String = String.Empty
                    Dim messageResponse As String = String.Empty

                    CodeResponse = IIf(IsDBNull(allData.Item("metaData").Item("code")) = True, "", allData.Item("metaData").Item("code"))
                    messageResponse = allData("metaData")("message").ToString

                    If CodeResponse = "200" Then
                        fn_UpdateRujukanV2 = allData("response")
                    Else
                        fn_UpdateRujukanV2 = ""
                        MsgBox(CodeResponse & " - " & messageResponse, MsgBoxStyle.Exclamation, Me.Text)
                        Exit Function
                    End If
                Else
                    fn_UpdateRujukanV2 = ""
                    MsgBox("Koneksi Rujukan Kosong", MsgBoxStyle.Exclamation, Me.Text)
                End If
            Else
                fn_UpdateRujukanV2 = ""
                MsgBox("Koneksi Tidak ditemukan", MsgBoxStyle.Exclamation, Me.Text)
            End If
        Catch oErr As Exception
            fn_UpdateRujukanV2 = ""
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Function
    Private Sub btnFaskes_Click(sender As Object, e As EventArgs) Handles btnFaskes.Click
        frmPPK.LoadMe(FORM_MODE.FORM_MODE_ADD)
        frmPPK.ShowDialog(Me)
        fn_LoadKDPPK()

        Dim oPPK As New Reference.clsPPK

        grdKDPPK.Text = oPPK.GetData.FirstOrDefault(Function(x) x.MEMO = sCode).KDPPK

    End Sub
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
    Private Sub fn_LoadKDDEPARTMENT()
        Dim oDEPARTMENT As New Reference.clsDepartment
        Try
            grdKDDEPARTMENT.Properties.DataSource = oDEPARTMENT.GetData.Where(Function(x) x.ISACTIVE = True).ToList()
            grdKDDEPARTMENT.Properties.ValueMember = "KDDEPARTMENT"
            grdKDDEPARTMENT.Properties.DisplayMember = "NAME_DISPLAY"

        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadKDDIAGNOSA()
        Dim oDIAGNOSA As New Reference.clsDiagnosa

        Try
            grdKDDIAGNOSA.Properties.DataSource = oDIAGNOSA.GetData.Where(Function(x) x.ISACTIVE = True).ToList()
            grdKDDIAGNOSA.Properties.ValueMember = "KDDIAGNOSA"
            grdKDDIAGNOSA.Properties.DisplayMember = "MEMO"

        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadKDPPK()
        Dim oPPK As New Reference.clsPPK

        Try
            grdKDPPK.Properties.DataSource = oPPK.GetData.Where(Function(x) x.ISACTIVE = True).ToList()
            grdKDPPK.Properties.ValueMember = "KDPPK"
            grdKDPPK.Properties.DisplayMember = "MEMO"

        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub txtCARI_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtCARI.KeyPress
        If Asc(e.KeyChar) = 13 Then
            PopUP = True
            fn_LoadKDPENDAFTARAN(txtCARI.Text)
            txtCARI.ResetText()
        End If
    End Sub
    Private Sub fn_LoadKDPENDAFTARAN(ByVal Parameter As String)
        Dim oDEPARTMENT As New Reference.clsDepartment
        Try
            Dim oPendaftaran As New Admission.clsPendaftaran

            Dim dsPendaftaran = (From x In oPendaftaran.GetDataBySKD(Parameter, cboCARI.SelectedIndex)
                                 Select x.KDPENDAFTARAN, x.CATEGORY, x.KDCUSTOMER, x.M_CUSTOMER.NAME_DISPLAY, TUJUAN = x.M_DEPARTMENT.NAME_DISPLAY, DOKTER = x.M_DOCTOR.NAME_DISPLAY, x.DATE).OrderByDescending(Function(x) x.DATE)

            grdKDPENDAFTARAN.Properties.DataSource = dsPendaftaran.ToList()
            grdKDPENDAFTARAN.Properties.ValueMember = "KDPENDAFTARAN"
            grdKDPENDAFTARAN.Properties.DisplayMember = "KDPENDAFTARAN"

            If PopUP = True Then
                grdKDPENDAFTARAN.ShowPopup()
            End If

        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub grdKDPENDAFTARAN_KeyPress(sender As Object, e As KeyPressEventArgs) Handles grdKDPENDAFTARAN.KeyPress
        If Asc(e.KeyChar) = 13 Then
            Dim oPendaftaran As New Admission.clsPendaftaran
            Dim dsPendaftaran = oPendaftaran.GetData(grdKDPENDAFTARAN.EditValue)
            If dsPendaftaran IsNot Nothing Then
                deDATE.DateTime = dsPendaftaran.DATE
                txtNOMORSEP.Text = dsPendaftaran.NOMORSEP
                grdKDDIAGNOSA.Text = dsPendaftaran.KDDIAGNOSA
            End If
        End If
    End Sub
#End Region
End Class