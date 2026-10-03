Imports DataAccess
Imports UI.WIN.MAIN.My.Resources
Imports System.Linq
Imports Newtonsoft.Json.Linq

Public Class frmLPK
#Region "Declaration"
    Private oFormMode As FORM_MODE = FORM_MODE.FORM_MODE_VIEW
    Private sNoId As String
    Private isLoad As Boolean = False
    Private oLPK As New Admission.clsLPK
    Private PopUP As Boolean = False
    Private REQUEST As String = ""
    Private RESPONSE As String = ""

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
            Me.Text = LPK.TITLE

            lKDLPK.Text = LPK.KDLPK
            lKDPENDAFATRAN.Text = LPK.KDPENDAFTARAN
            lKDRUANGRAWAT.Text = LPK.KDRUANGRAWAT
            lNOMORSEP.Text = LPK.NOMORSEP
            lDATE_MASUK.Text = LPK.DATE_MASUK
            lDATE_KELUAR.Text = LPK.DATE_KELUAR
            lJAMINAN.Text = LPK.JAMINAN
            lKDDEPARTMENT.Text = LPK.KDDEPARTMENT
            lKDKELASRAWAT.Text = LPK.KDKELASRAWAT
            lKDSPESIALISTIK.Text = LPK.KDSPESIALISTIK
            lKDCARAKELUAR.Text = LPK.KDCARAKELUAR
            lTINDAKLANJUT.Text = LPK.TINDAKLANJUT
            lKDPPK.Text = LPK.KDPPK
            lDATE_KONTROL.Text = LPK.DATE_KONTROL
            lKDDEPARTMENT_KONTROL.Text = LPK.KDDEPARTMENT_KONTROL
            lKDDOCTOR.Text = LPK.KDDOCTOR
            lKDPASCAPULANG.Text = LPK.KDPASCAPULANG

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
        fn_LoadKDRUANGRAWAT()
        fn_LoadKDPPK()
        fn_LoadKDDOCTOR()
        fn_LoadKDCARAKELUAR()
        fn_LoadKDKELASRAWAT()
        fn_LoadKDSUBSPESIALISTIK()
        fn_LoadKDPASCAPULANG()
        fn_LoadKDDIAGNOSA()
        fn_LoadKDPROSEDUR()

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
        txtNOMORSEP.Properties.ReadOnly = True
        deDATE_MASUK.Properties.ReadOnly = True
        deDATE_KELUAR.Properties.ReadOnly = Status
        cboJAMINAN.Properties.ReadOnly = Status
        grdKDDEPARTMENT.Properties.ReadOnly = Status
        grdKDKELASRAWAT.Properties.ReadOnly = Status
        grdKDSPESIALISTIK.Properties.ReadOnly = Status
        grdKDCARAKELUAR.Properties.ReadOnly = Status
        grdKDPASCAPULANG.Properties.ReadOnly = Status
        cboTINDAKLANJUT.Properties.ReadOnly = Status
        grdKDPPK.Properties.ReadOnly = Status
        deDATE_KONTROL.Properties.ReadOnly = Status
        grdKDDEPARTMENT_KONTROL.Properties.ReadOnly = Status
        grdKDDOCTOR.Properties.ReadOnly = Status
        grdKDRUANGRAWAT.Properties.ReadOnly = Status
    End Sub
    Private Sub fn_EmptyMe()
        txtCODE.Text = "<--- AUTO --->"

        grdKDPENDAFTARAN.ResetText()
        txtNOMORSEP.ResetText()
        deDATE_MASUK.DateTime = Now
        deDATE_KELUAR.DateTime = Now
        deDATE_KONTROL.DateTime = Now
    End Sub
    Private Sub fn_LoadData()
        Try
            Dim ds = oLPK.GetData(sNoId)

            With ds
                txtCODE.Text = sNoId
                cboCARI.SelectedIndex = 2
                fn_LoadKDPENDAFTARAN(.KDPENDAFTARAN)

                grdKDPENDAFTARAN.Text = .KDPENDAFTARAN
                txtNOMORSEP.Text = .NOMORSEP
                deDATE_MASUK.DateTime = .DATE_MASUK
                deDATE_KELUAR.DateTime = .DATE_KELUAR
                cboJAMINAN.SelectedIndex = .JAMINAN
                grdKDDEPARTMENT.Text = .KDDEPARTMENT_POLI
                grdKDKELASRAWAT.Text = .KDKELASRAWAT
                grdKDSPESIALISTIK.Text = .KDSPESIALISTIK
                grdKDCARAKELUAR.Text = .KDCARAKELUAR
                grdKDPASCAPULANG.Text = .KDPASCAPULANG
                cboTINDAKLANJUT.SelectedIndex = .TINDAKLANJUT
                grdKDPPK.Text = .KDPPK
                deDATE_KONTROL.DateTime = .DATE_KONTROL
                grdKDDEPARTMENT_KONTROL.Text = .KDDEPARTMENT_KONTROL
                grdKDDOCTOR.Text = .KDDOCTOR
                grdKDRUANGRAWAT.Text = .KDDEPARTMENT_RUANGAN

                BindingSource1.DataSource = oLPK.GetDataDetail1.Where(Function(x) x.KDLPK = sNoId).OrderBy(Function(x) x.SEQ).ToList()
                grdDetail_Diagnosa.DataSource = BindingSource1

                BindingSource2.DataSource = oLPK.GetDataDetail2.Where(Function(x) x.KDLPK = sNoId).OrderBy(Function(x) x.SEQ).ToList()
                grdDetail_Prosedur.DataSource = BindingSource2

            End With
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Function fn_Validate() As Boolean
        Try
            fn_Validate = True
            If grdKDPENDAFTARAN.Text = String.Empty Then
                grdKDPENDAFTARAN.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                grdKDPENDAFTARAN.ErrorText = Statement.ErrorRequired

                grdKDPENDAFTARAN.Focus()
                fn_Validate = False
                Exit Function
            End If

            If cboJAMINAN.Text = String.Empty Then
                cboJAMINAN.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                cboJAMINAN.ErrorText = Statement.ErrorRequired

                cboJAMINAN.Focus()
                fn_Validate = False
                Exit Function
            End If
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
            If grdKDDOCTOR.Text = String.Empty Then
                grdKDDOCTOR.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                grdKDDOCTOR.ErrorText = Statement.ErrorRequired

                grdKDDOCTOR.Focus()
                fn_Validate = False
                Exit Function
            End If
            If grdKDDEPARTMENT_KONTROL.Text = String.Empty Then
                grdKDDEPARTMENT_KONTROL.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                grdKDDEPARTMENT_KONTROL.ErrorText = Statement.ErrorRequired

                grdKDDEPARTMENT_KONTROL.Focus()
                fn_Validate = False
                Exit Function
            End If

            grvDetail_Diagnosa.UpdateCurrentRow()

            If grvDetail_Diagnosa.RowCount < 2 Then
                MsgBox(Statement.ErrorDetail, MsgBoxStyle.Exclamation, Me.Text)
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
            Dim sNoLPK As String = String.Empty

            Dim ds = oLPK.GetStructureHeader
            With ds
                Try
                    .DATECREATED = oLPK.GetData(sNoId).DATECREATED
                Catch oErr As Exception
                    .DATECREATED = Now
                End Try
                .DATEUPDATED = Now

                .KDLPK = IIf(sNoLPK = String.Empty, sNoId, sNoLPK)
                .KDPENDAFTARAN = grdKDPENDAFTARAN.EditValue
                .NOMORSEP = txtNOMORSEP.Text.Trim.ToUpper
                .DATE_MASUK = deDATE_MASUK.DateTime
                .DATE_KELUAR = deDATE_KELUAR.DateTime
                .JAMINAN = cboJAMINAN.SelectedIndex
                .KDDEPARTMENT_POLI = grdKDDEPARTMENT.EditValue
                .KDDEPARTMENT_RUANGAN = grdKDRUANGRAWAT.EditValue
                .KDKELASRAWAT = grdKDKELASRAWAT.EditValue
                .KDSPESIALISTIK = grdKDSPESIALISTIK.EditValue
                .KDCARAKELUAR = grdKDCARAKELUAR.EditValue
                .KDPASCAPULANG = grdKDPASCAPULANG.EditValue
                .TINDAKLANJUT = cboTINDAKLANJUT.SelectedIndex
                .KDPPK = grdKDPPK.EditValue
                .DATE_KONTROL = deDATE_KONTROL.DateTime
                .KDDEPARTMENT_KONTROL = grdKDDEPARTMENT_KONTROL.EditValue
                .KDDOCTOR = grdKDDOCTOR.EditValue
                .KDUSER = sUserID
                .REQUEST = REQUEST
                .RESPONSE = RESPONSE
            End With

            Dim arrDetail1 = oLPK.GetStructureDetail1List
            For i As Integer = 0 To grvDetail_Diagnosa.RowCount - 2
                Dim dsDetail = oLPK.GetStructureDetail1
                With dsDetail
                    .SEQ = i
                    .KDLPK = ds.KDLPK
                    .KDDIAGNOSA = grvDetail_Diagnosa.GetRowCellValue(i, colKDDIAGNOSA)
                    .REMARKS = "-"
                End With
                arrDetail1.Add(dsDetail)
            Next
            ' ***** PROSEDUR *****
            Dim arrDetail2 = oLPK.GetStructureDetail2List
            For i As Integer = 0 To grvDetail_Prosedur.RowCount - 2
                Dim dsDetail = oLPK.GetStructureDetail2
                With dsDetail
                    .SEQ = i
                    .KDLPK = ds.KDLPK
                    .KDPROSEDUR = grvDetail_Prosedur.GetRowCellValue(i, colKDPROSEDUR)
                    .REMARKS = "-"
                End With
                arrDetail2.Add(dsDetail)
            Next
            If oFormMode = FORM_MODE.FORM_MODE_ADD Then
                Try
                    txtCODE.Text = oLPK.InsertData(ds, arrDetail1, arrDetail2)
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
                    fn_Save = oLPK.UpdateData(ds, arrDetail1, arrDetail2)
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
    Private Sub CetakRegister(ByVal KDLPK As String)
        'Dim rpt As New xtraLPK

        'Dim ds = oLPK.GetData(KDLPK)
        'rpt.bindingSource.DataSource = ds
        'Dim printTool As New DevExpress.XtraReports.UI.ReportPrintTool(rpt)
        'printTool.PrintDialog()
    End Sub
    Private Function fn_CreateLPK() As String
        Try
            Dim oSetKoneksi As New Brigging.clsSetKoneksi
            Dim oDepartment As New Reference.clsDepartment
            Dim oDoctor As New Reference.clsDoctor
            Dim oPPK As New Reference.clsPPK
            Dim jsonRequest As String = String.Empty
            Dim NoLPK As String = String.Empty

            Dim oLPK As New Reference.clsPPK
            Dim oPoli As New Reference.clsDepartment

            Dim list1 As New List(Of String)
            Dim list2 As New List(Of String)

            For i As Integer = 0 To grvDetail_Diagnosa.RowCount - 2
                list1.Add(" { " & """kode"": """ & grvDetail_Diagnosa.GetRowCellValue(i, colKDDIAGNOSA) & """," & """level"": """ & i + 1 & """ } ")
            Next

            For i As Integer = 0 To grvDetail_Prosedur.RowCount - 2
                list2.Add(" { " & """kode"": """ & grvDetail_Prosedur.GetRowCellValue(i, colKDPROSEDUR) & """ } ")
            Next

            jsonRequest = " { "
            jsonRequest &= """request"" :  { "
            jsonRequest &= """t_LPK"": { "
            jsonRequest &= """noSep"": """ & txtNOMORSEP.Text.Trim.ToUpper & """, "
            jsonRequest &= """tglMasuk"": """ & deDATE_MASUK.DateTime.ToString("yyyy-MM-dd") & """, "
            jsonRequest &= """tglKeluar"": """ & deDATE_KELUAR.DateTime.ToString("yyyy-MM-dd") & """, "
            jsonRequest &= """jaminan"": """ & cboJAMINAN.SelectedIndex + 1 & """, "
            jsonRequest &= """poli"": { "
            jsonRequest &= """poli"": """ & grdKDDEPARTMENT.EditValue & """ "
            jsonRequest &= "}, "
            jsonRequest &= """perawatan"": { "
            jsonRequest &= """ruangRawat"": """ & grdKDDEPARTMENT.EditValue & """, "
            jsonRequest &= """kelasRawat"": """ & grdKDKELASRAWAT.EditValue & """, "
            jsonRequest &= """spesialistik"": """ & grdKDSPESIALISTIK.EditValue & """, "
            jsonRequest &= """caraKeluar"": """ & grdKDCARAKELUAR.EditValue & """, "
            jsonRequest &= """kondisiPulang"": """ & grdKDPASCAPULANG.EditValue & """ "
            jsonRequest &= "}, "
            jsonRequest &= """diagnosa"": "
            jsonRequest &= "[ "
            jsonRequest &= String.Join(",", list1.ToArray)
            jsonRequest &= "], "
            jsonRequest &= """procedure"": "
            jsonRequest &= "[ "
            jsonRequest &= String.Join(",", list2.ToArray)
            jsonRequest &= "], "
            jsonRequest &= """rencanaTL"": { "
            jsonRequest &= """tindakLanjut"": """ & IIf(cboTINDAKLANJUT.SelectedIndex = 0, 1, IIf(cboTINDAKLANJUT.SelectedIndex = 1, 2, IIf(cboTINDAKLANJUT.SelectedIndex = 2, 3, 4))) & """, "
            jsonRequest &= """dirujukKe"": { "
            jsonRequest &= """kodePPK"": """ & oPPK.GetData(grdKDPPK.EditValue).KODEFASKES & """ "
            jsonRequest &= "}, "
            jsonRequest &= """kontrolKembali"": { "
            jsonRequest &= """tglKontrol"": """ & deDATE_KONTROL.DateTime.ToString("yyyy-MM-dd") & """, "
            jsonRequest &= """poli"": """ & oDepartment.GetData(grdKDDEPARTMENT.EditValue).KDDEPARTMENT_BPJS & """ "
            jsonRequest &= "} "
            jsonRequest &= "}, "
            jsonRequest &= """DPJP"": """ & oDoctor.GetData(grdKDDOCTOR.EditValue).VCLAIM_KDDPJP & """, "
            jsonRequest &= """user"": """ & sUserID & """ "
            jsonRequest &= "} "
            jsonRequest &= "} "
            jsonRequest &= "}  "

            Dim dsDataSetKoneksi = oSetKoneksi.GetData().FirstOrDefault(Function(x) x.ISACTIVE = True And x.NAME_DISPLAY = "VCLAIM")
            Dim uTime As Integer = 0

            If dsDataSetKoneksi IsNot Nothing Then
                uTime = (DateTime.UtcNow - New DateTime(1970, 1, 1, 0, 0, 0)).TotalSeconds
                Dim dsSetKoneksi = oSetKoneksi.InsertLPK(dsDataSetKoneksi.ALAMATWEB, dsDataSetKoneksi.CONSID, dsDataSetKoneksi.SECREATKEY, dsDataSetKoneksi.USER_KEY, uTime, jsonRequest)

                If dsSetKoneksi <> "" Then
                    Dim allData = JObject.Parse(dsSetKoneksi)

                    Dim CodeResponse As String = String.Empty
                    Dim messageResponse As String = String.Empty

                    CodeResponse = IIf(IsDBNull(allData.Item("metaData").Item("code")) = True, "", allData.Item("metaData").Item("code"))
                    messageResponse = allData("metaData")("message").ToString

                    If CodeResponse = "200" Then
                        Dim DataDecrypt = JObject.Parse(oSetKoneksi.Decrypt(allData("response"), dsDataSetKoneksi.CONSID & dsDataSetKoneksi.SECREATKEY & uTime))

                        REQUEST = jsonRequest
                        RESPONSE = dsSetKoneksi
                        fn_CreateLPK = DataDecrypt
                    Else
                        fn_CreateLPK = ""
                        MsgBox(CodeResponse & " - " & messageResponse, MsgBoxStyle.Exclamation, Me.Text)
                        Exit Function
                    End If
                Else
                    fn_CreateLPK = ""
                    MsgBox("Insert LPK Data Gagal/Kosong", MsgBoxStyle.Exclamation, Me.Text)
                End If
            Else
                fn_CreateLPK = ""
                MsgBox("Koneksi Tidak ditemukan", MsgBoxStyle.Exclamation, Me.Text)
            End If

        Catch oErr As Exception
            fn_CreateLPK = ""
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Function
    Private Function fn_UpdateLPK() As Boolean
        Try
            Dim oSetKoneksi As New Brigging.clsSetKoneksi
            Dim oDepartment As New Reference.clsDepartment
            Dim oDoctor As New Reference.clsDoctor
            Dim oPPK As New Reference.clsPPK
            Dim jsonRequest As String = String.Empty
            Dim NoLPK As String = String.Empty

            Dim oLPK As New Reference.clsPPK
            Dim oPoli As New Reference.clsDepartment

            Dim list1 As New List(Of String)
            Dim list2 As New List(Of String)

            For i As Integer = 0 To grvDetail_Diagnosa.RowCount - 2
                list1.Add(" { " & """kode"": """ & grvDetail_Diagnosa.GetRowCellValue(i, colKDDIAGNOSA) & """," & """level"": """ & i + 1 & """ } ")
            Next

            For i As Integer = 0 To grvDetail_Prosedur.RowCount - 2
                list2.Add(" { " & """kode"": """ & grvDetail_Prosedur.GetRowCellValue(i, colKDPROSEDUR) & """ } ")
            Next


            jsonRequest = " { "
            jsonRequest &= """request"" :  { "
            jsonRequest &= """t_LPK"": { "
            jsonRequest &= """noSep"": """ & txtNOMORSEP.Text.Trim.ToUpper & """, "
            jsonRequest &= """tglMasuk"": """ & deDATE_MASUK.DateTime.ToString("yyyy-MM-dd") & """, "
            jsonRequest &= """tglKeluar"": """ & deDATE_KELUAR.DateTime.ToString("yyyy-MM-dd") & """, "
            jsonRequest &= """jaminan"": """ & cboJAMINAN.SelectedIndex + 1 & """, "
            jsonRequest &= """poli"": { "
            jsonRequest &= """poli"": """ & grdKDDEPARTMENT.EditValue & """ "
            jsonRequest &= "}, "
            jsonRequest &= """perawatan"": { "
            jsonRequest &= """ruangRawat"": """ & grdKDDEPARTMENT.EditValue & """, "
            jsonRequest &= """kelasRawat"": """ & grdKDKELASRAWAT.EditValue & """, "
            jsonRequest &= """spesialistik"": """ & grdKDSPESIALISTIK.EditValue & """, "
            jsonRequest &= """caraKeluar"": """ & grdKDCARAKELUAR.EditValue & """, "
            jsonRequest &= """kondisiPulang"": """ & grdKDPASCAPULANG.EditValue & """ "
            jsonRequest &= "}, "
            jsonRequest &= """diagnosa"": "
            jsonRequest &= "[ "
            jsonRequest &= String.Join(",", list1.ToArray)
            jsonRequest &= "], "
            jsonRequest &= """procedure"": "
            jsonRequest &= "[ "
            jsonRequest &= String.Join(",", list2.ToArray)
            jsonRequest &= "], "
            jsonRequest &= """rencanaTL"": { "
            jsonRequest &= """tindakLanjut"": """ & IIf(cboTINDAKLANJUT.SelectedIndex = 0, 1, IIf(cboTINDAKLANJUT.SelectedIndex = 1, 2, IIf(cboTINDAKLANJUT.SelectedIndex = 2, 3, 4))) & """, "
            jsonRequest &= """dirujukKe"": { "
            jsonRequest &= """kodePPK"": """ & oPPK.GetData(grdKDPPK.EditValue).KODEFASKES & """ "
            jsonRequest &= "}, "
            jsonRequest &= """kontrolKembali"": { "
            jsonRequest &= """tglKontrol"": """ & deDATE_KONTROL.DateTime.ToString("yyyy-MM-dd") & """, "
            jsonRequest &= """poli"": """ & oDepartment.GetData(grdKDDEPARTMENT.EditValue).KDDEPARTMENT_BPJS & """ "
            jsonRequest &= "} "
            jsonRequest &= "}, "
            jsonRequest &= """DPJP"": """ & oDoctor.GetData(grdKDDOCTOR.EditValue).VCLAIM_KDDPJP & """, "
            jsonRequest &= """user"": """ & sUserID & """ "
            jsonRequest &= "} "
            jsonRequest &= "} "
            jsonRequest &= "}  "

            Dim dsDataSetKoneksi = oSetKoneksi.GetData().FirstOrDefault(Function(x) x.ISACTIVE = True And x.NAME_DISPLAY = "VCLAIM")
            Dim uTime As Integer = 0

            If dsDataSetKoneksi IsNot Nothing Then
                uTime = (DateTime.UtcNow - New DateTime(1970, 1, 1, 0, 0, 0)).TotalSeconds
                Dim dsSetKoneksi = oSetKoneksi.UpdateLPK(dsDataSetKoneksi.ALAMATWEB, dsDataSetKoneksi.CONSID, dsDataSetKoneksi.SECREATKEY, dsDataSetKoneksi.USER_KEY, uTime, jsonRequest)

                If dsSetKoneksi <> "" Then
                    Dim allData = JObject.Parse(dsSetKoneksi)

                    Dim CodeResponse As String = String.Empty
                    Dim messageResponse As String = String.Empty

                    CodeResponse = IIf(IsDBNull(allData.Item("metaData").Item("code")) = True, "", allData.Item("metaData").Item("code"))
                    messageResponse = allData("metaData")("message").ToString

                    If CodeResponse = "200" Then
                        REQUEST = jsonRequest
                        RESPONSE = dsSetKoneksi
                        fn_UpdateLPK = True
                    Else
                        fn_UpdateLPK = False
                        MsgBox(CodeResponse & " - " & messageResponse, MsgBoxStyle.Exclamation, Me.Text)
                        Exit Function
                    End If
                Else
                    fn_UpdateLPK = False
                    MsgBox("Insert LPK Data Gagal/Kosong", MsgBoxStyle.Exclamation, Me.Text)
                End If
            Else
                fn_UpdateLPK = False
                MsgBox("Koneksi Tidak ditemukan", MsgBoxStyle.Exclamation, Me.Text)
            End If
        Catch oErr As Exception
            fn_UpdateLPK = False
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Function
    Private Sub DeleteToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DeleteToolStripMenuItem.Click
        If oFormMode = FORM_MODE.FORM_MODE_VIEW Then Exit Sub
        grvDetail_Diagnosa.DeleteSelectedRows()
    End Sub
    Private Sub ToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem1.Click
        If oFormMode = FORM_MODE.FORM_MODE_VIEW Then Exit Sub
        grvDetail_Prosedur.DeleteSelectedRows()
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

            grdKDDEPARTMENT_KONTROL.Properties.DataSource = oDEPARTMENT.GetData.Where(Function(x) x.ISACTIVE = True).ToList()
            grdKDDEPARTMENT_KONTROL.Properties.ValueMember = "KDDEPARTMENT"
            grdKDDEPARTMENT_KONTROL.Properties.DisplayMember = "NAME_DISPLAY"

        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadKDRUANGRAWAT()
        Dim oRUANGRAWAT As New Reference.clsRuangRawat
        Try
            grdKDRUANGRAWAT.Properties.DataSource = oRUANGRAWAT.GetData.Where(Function(x) x.ISACTIVE = True).ToList()
            grdKDRUANGRAWAT.Properties.ValueMember = "KDRUANGRAWAT"
            grdKDRUANGRAWAT.Properties.DisplayMember = "NAME_DISPLAY"

        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadKDDOCTOR()
        Dim oDOCTOR As New Reference.clsDoctor
        Try
            grdKDDOCTOR.Properties.DataSource = oDOCTOR.GetData.Where(Function(x) x.ISACTIVE = True).ToList()
            grdKDDOCTOR.Properties.ValueMember = "KDDOCTOR"
            grdKDDOCTOR.Properties.DisplayMember = "NAME_DISPLAY"

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
    Private Sub fn_LoadKDKELASRAWAT()
        Dim oKELAS As New Reference.clsKelasRawat

        Try
            grdKDKELASRAWAT.Properties.DataSource = oKELAS.GetData.Where(Function(x) x.ISACTIVE = True).ToList()
            grdKDKELASRAWAT.Properties.ValueMember = "KDKELASRAWAT"
            grdKDKELASRAWAT.Properties.DisplayMember = "MEMO"

        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadKDSUBSPESIALISTIK()
        Dim oSUBSPESIALISTIK As New Reference.clsSpesialistik

        Try
            grdKDSPESIALISTIK.Properties.DataSource = oSUBSPESIALISTIK.GetData.Where(Function(x) x.ISACTIVE = True).ToList()
            grdKDSPESIALISTIK.Properties.ValueMember = "KDSPESIALISTIK"
            grdKDSPESIALISTIK.Properties.DisplayMember = "MEMO"

        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadKDCARAKELUAR()
        Dim oCARAPULANG As New Reference.clsCaraKeluar

        Try
            grdKDCARAKELUAR.Properties.DataSource = oCARAPULANG.GetData.Where(Function(x) x.ISACTIVE = True).ToList()
            grdKDCARAKELUAR.Properties.ValueMember = "KDCARAKELUAR"
            grdKDCARAKELUAR.Properties.DisplayMember = "MEMO"

        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadKDPASCAPULANG()
        Dim oPASCAPULANG As New Reference.clsPascaPulang

        Try
            grdKDPASCAPULANG.Properties.DataSource = oPASCAPULANG.GetData.Where(Function(x) x.ISACTIVE = True).ToList()
            grdKDPASCAPULANG.Properties.ValueMember = "KDPASCAPULANG"
            grdKDPASCAPULANG.Properties.DisplayMember = "MEMO"

        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadKDDIAGNOSA()
        Dim oDIAGNOSA As New Reference.clsDiagnosa

        Try
            grdKDDIAGNOSA.DataSource = oDIAGNOSA.GetData.Where(Function(x) x.ISACTIVE = True).ToList()
            grdKDDIAGNOSA.ValueMember = "KDDIAGNOSA"
            grdKDDIAGNOSA.DisplayMember = "MEMO"

        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadKDPROSEDUR()
        Dim oPROSEDUR As New Reference.clsProsedur

        Try
            grdKDPROSEDUR.DataSource = oPROSEDUR.GetData.Where(Function(x) x.ISACTIVE = True).ToList()
            grdKDPROSEDUR.ValueMember = "KDPROSEDUR"
            grdKDPROSEDUR.DisplayMember = "MEMO"

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
                deDATE_MASUK.DateTime = dsPendaftaran.DATE
                txtNOMORSEP.Text = dsPendaftaran.NOMORSEP
                grdKDDEPARTMENT.Text = dsPendaftaran.KDDEPARTMENT
            End If
        End If
    End Sub
#End Region
End Class