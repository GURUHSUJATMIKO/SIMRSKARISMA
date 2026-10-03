Imports DataAccess
Imports UI.WIN.MAIN.My.Resources
Imports System.Linq
Imports Newtonsoft.Json.Linq

Public Class frmPendaftaran_PRB
#Region "Declaration"
    Private oFormMode As FORM_MODE = FORM_MODE.FORM_MODE_VIEW
    Private sNoId As String
    Private isLoad As Boolean = False
    Private oPendaftaran_PRB As New Admission.clsPendaftaran_PRB
    Private REQUEST As String = String.Empty
    Private RESPONSE As String = String.Empty
    Private sNomorsep As String = String.Empty
    Private sNoKartu As String = String.Empty
#End Region
#Region "Function"
    Public Sub fn_LoadIdentias(ByVal KDPENDAFTRAN As String, ByVal NOMORSEP As String, ByVal KARTUBPJS As String)
        sNomorsep = NOMORSEP
        sNoKartu = KARTUBPJS
        txtKDPENDAFTARAN.Text = KDPENDAFTRAN
    End Sub
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
            Me.Text = Pendaftaran_PRB.TITLE

            lKDPendaftaran_PRB.Text = Pendaftaran_PRB.KDPRB & " *"
            lKDPENDAFTARAN.Text = Pendaftaran.KDPENDAFTARAN & " *"
            lEMAIL.Text = Pendaftaran_PRB.EMAIL & " *"
            lKDDIAGNOSA_PRB.Text = Pendaftaran_PRB.KDDIAGNOSA_PRB & " *"
            lKDDOCTOR.Text = Pendaftaran_PRB.KDDOCTOR & " *"
            lKETERANGAN.Text = Pendaftaran_PRB.KETERANGAN & " *"
            lSARAN.Text = Pendaftaran_PRB.SARAN & " *"

            grvDetail.Columns("KDOBAT").Caption = Pendaftaran_PRB.DETAIL_KDOBAT
            grvDetail.Columns("NAMAOBAT").Caption = Pendaftaran_PRB.DETAIL_NAMAOBAT
            grvDetail.Columns("SIGNA1").Caption = Pendaftaran_PRB.DETAIL_SIGNA1
            grvDetail.Columns("SIGNA2").Caption = Pendaftaran_PRB.DETAIL_SIGNA2
            grvDetail.Columns("JUMLAH").Caption = Pendaftaran_PRB.DETAIL_NAMAOBAT

            btnSaveNew.Caption = Caption.FormSaveNew
            btnSaveClose.Caption = Caption.FormSaveClose
            btnClose.Caption = Caption.FormClose
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub Form_Closing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        sCode = txtKDPendaftaran_PRB.Text.Trim.ToUpper
    End Sub
    Private Sub fn_ChangeFormState()
        fn_LoadDiagnosaPRB()
        fn_LoadDoctor()

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

        txtEMAIL.Properties.ReadOnly = Status
        grdKDDIAGNOSA_PRB.Properties.ReadOnly = Status
        grdKDDOCTOR.Properties.ReadOnly = Status
        txtKETERANGAN.Properties.ReadOnly = Status
        txtSARAN.Properties.ReadOnly = Status
        grvDetail.OptionsBehavior.ReadOnly = Status
    End Sub
    Private Sub fn_EmptyMe()
        txtKDPendaftaran_PRB.Text = "<--- AUTO --->"
        txtEMAIL.ResetText()
        grdKDDIAGNOSA_PRB.ResetText()
        grdKDDOCTOR.ResetText()
        txtKETERANGAN.ResetText()
        txtSARAN.ResetText()
    End Sub
    Private Sub fn_LoadData()
        Try
            ' ***** HEADER *****
            Dim ds = oPendaftaran_PRB.GetData(sNoId)

            With ds
                txtKDPendaftaran_PRB.Text = .KDPRB
                txtKDPENDAFTARAN.Text = .KDPENDAFTARAN
                txtEMAIL.Text = .EMAIL
                grdKDDIAGNOSA_PRB.Text = .KDDIAGNOSA_PRB
                grdKDDOCTOR.Text = .KDDOCTOR
                txtKETERANGAN.Text = .KETERANGAN
                txtSARAN.Text = .SARAN

                bindingSource.DataSource = oPendaftaran_PRB.GetDataDetail.Where(Function(x) x.KDPRB = sNoId).OrderBy(Function(x) x.SEQ).ToList()
                grdDetail.DataSource = bindingSource
            End With
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Function fn_Validate() As Boolean
        Try
            fn_Validate = True

            If txtKDPENDAFTARAN.Text = String.Empty Then
                txtKDPENDAFTARAN.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                txtKDPENDAFTARAN.ErrorText = Statement.ErrorRequired

                txtKDPENDAFTARAN.Focus()
                fn_Validate = False
                Exit Function
            End If
            If grdKDDIAGNOSA_PRB.Text = String.Empty Then
                grdKDDIAGNOSA_PRB.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                grdKDDIAGNOSA_PRB.ErrorText = Statement.ErrorRequired

                grdKDDIAGNOSA_PRB.Focus()
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
            If txtKETERANGAN.Text = String.Empty Then
                txtKETERANGAN.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                txtKETERANGAN.ErrorText = Statement.ErrorRequired

                txtKETERANGAN.Focus()
                fn_Validate = False
                Exit Function
            End If
            If txtSARAN.Text = String.Empty Then
                txtSARAN.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                txtSARAN.ErrorText = Statement.ErrorRequired

                txtSARAN.Focus()
                fn_Validate = False
                Exit Function
            End If

            'grvDetail.UpdateCurrentRow()

            'If grvDetail.RowCount < 2 Then
            '    MsgBox(Statement.ErrorDetail, MsgBoxStyle.Exclamation, Me.Text)
            '    fn_Validate = False
            '    Exit Function
            'End If
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Function
    Private Function fn_Save() As Boolean
        Try

            ' ***** HEADER *****
            Dim ds = oPendaftaran_PRB.GetStructureHeader
            With ds
                Try
                    .DATECREATED = oPendaftaran_PRB.GetData(sNoId).DATECREATED
                Catch oErr As Exception
                    .DATECREATED = Now
                End Try
                .DATEUPDATED = Now

                .KDPRB = txtKDPendaftaran_PRB.Text
                .KDPENDAFTARAN = txtKDPENDAFTARAN.Text
                .EMAIL = txtEMAIL.Text
                .KDDIAGNOSA_PRB = grdKDDIAGNOSA_PRB.EditValue
                .KDDOCTOR = grdKDDOCTOR.EditValue
                .KETERANGAN = txtKETERANGAN.Text
                .SARAN = txtSARAN.Text
                .REQUEST = REQUEST
                .RESPONSE = RESPONSE
                .KDUSER = sUserID
            End With

            ' ***** DETIL *****
            Dim arrDetail = oPendaftaran_PRB.GetStructureDetailList
            For i As Integer = 0 To grvDetail.RowCount - 2
                Dim dsDetail = oPendaftaran_PRB.GetStructureDetail
                With dsDetail
                    Try
                        .DATECREATED = oPendaftaran_PRB.GetData(sNoId).DATECREATED
                    Catch oErr As Exception
                        .DATECREATED = Now
                    End Try
                    .DATEUPDATED = Now

                    .SEQ = i
                    .KDPRB = ds.KDPRB
                    .KDPENDAFTARAN = txtKDPENDAFTARAN.Text
                    .KDOBAT = grvDetail.GetRowCellValue(i, colKDOBAT)
                    .NAMAOBAT = grvDetail.GetRowCellValue(i, colNAMAOBAT)
                    .SIGNA1 = grvDetail.GetRowCellValue(i, colSIGNA1)
                    .SIGNA2 = grvDetail.GetRowCellValue(i, colSIGNA2)
                    .JUMLAH = grvDetail.GetRowCellValue(i, colJUMLAH)
                End With
                arrDetail.Add(dsDetail)
            Next

            If oFormMode = FORM_MODE.FORM_MODE_ADD Then
                Try
                    'txtKDPendaftaran_PRB.Text = fn_InsertPRB(sNomorsep, sNoKartu, "Cimareme")
                    'If txtKDPendaftaran_PRB.Text = "" Then
                    '    fn_Save = False
                    'Else
                    '    fn_Save = oPendaftaran_PRB.InsertData(ds, arrDetail)
                    'End If
                    fn_Save = oPendaftaran_PRB.InsertData(ds, arrDetail)
                Catch oErr As Exception
                    MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            ElseIf oFormMode = FORM_MODE.FORM_MODE_EDIT Then
                Try
                    'If fn_UpdatePRB(sNomorsep, sNoKartu, "Cimareme") = False Then
                    '    fn_Save = False
                    'Else
                    '    fn_Save = oPendaftaran_PRB.UpdateData(ds, arrDetail)
                    'End If
                    fn_Save = oPendaftaran_PRB.UpdateData(ds, arrDetail)
                Catch oErr As Exception
                    MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            End If
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            fn_Save = False
        End Try
    End Function
    Private Function fn_InsertPRB(ByVal Nomorsep As String, ByVal NoKartu As String, ByVal Alamat As String) As String
        Try
            REQUEST = ""
            RESPONSE = ""

            Dim oSetKoneksi As New Brigging.clsSetKoneksi
            Dim jsonRequest As String = String.Empty
            Dim oDoctor As New Reference.clsDoctor
            Dim list As New List(Of String)

            For i As Integer = 0 To grvDetail.RowCount - 2
                list.Add(" { " & """kode"": """ & grvDetail.GetRowCellValue(i, colKDOBAT) & """," & """nama"": """ & grvDetail.GetRowCellValue(i, colNAMAOBAT) & """ } ")
            Next

            jsonRequest = " { "
            jsonRequest &= """request"" :  { "
            jsonRequest &= """t_prb"": { "
            jsonRequest &= """noSep"": """ & Nomorsep & ""","
            jsonRequest &= """noKartu"": """ & NoKartu & """, "
            jsonRequest &= """alamat"": """ & Alamat & """, "
            jsonRequest &= """email"": """ & txtEMAIL.Text & """, "
            jsonRequest &= """programPRB"": """ & grdKDDIAGNOSA_PRB.EditValue & """, "
            jsonRequest &= """kodeDPJP"": """ & oDoctor.GetData(grdKDDOCTOR.EditValue).VCLAIM_KDDPJP & """, "
            jsonRequest &= """keterangan"": """ & txtKETERANGAN.Text & """, "
            jsonRequest &= """saran"": """ & txtSARAN.Text & """, "
            jsonRequest &= """user"": """ & sUserID & """, "
            jsonRequest &= """obat"": "
            jsonRequest &= "[ "

            jsonRequest &= String.Join(",", list.ToArray)

            jsonRequest &= "] "
            jsonRequest &= "} "
            jsonRequest &= "} "
            jsonRequest &= "}  "

            Dim uTime As Integer = 0

            If sConsidVclaim <> "" Then
                uTime = (DateTime.UtcNow - New DateTime(1970, 1, 1, 0, 0, 0)).TotalSeconds
                Dim dsSetKoneksi = oSetKoneksi.InsertPRB(sUrlVclaim, sConsidVclaim, sSecreateKeyVclaim, sUserKeyVclaim, uTime, jsonRequest)

                If dsSetKoneksi <> "" Then
                    Dim allData = JObject.Parse(dsSetKoneksi)

                    Dim CodeResponse As String = String.Empty
                    Dim messageResponse As String = String.Empty

                    CodeResponse = IIf(IsDBNull(allData.Item("metaData").Item("code")) = True, "", allData.Item("metaData").Item("code"))
                    messageResponse = allData("metaData")("message").ToString

                    If CodeResponse = "200" Then
                        Dim DataDecrypt = JObject.Parse(oSetKoneksi.Decrypt(allData("response"), sConsidVclaim & sSecreateKeyVclaim & uTime))

                        REQUEST = jsonRequest
                        RESPONSE = dsSetKoneksi
                        fn_InsertPRB = DataDecrypt.Item("noSRB").ToString()
                    Else
                        fn_InsertPRB = ""
                        MsgBox(CodeResponse & " - " & messageResponse, MsgBoxStyle.Exclamation, Me.Text)
                        Exit Function
                    End If
                Else
                    fn_InsertPRB = ""
                    MsgBox("Insert PRB", MsgBoxStyle.Exclamation, Me.Text)
                End If
            Else
                fn_InsertPRB = ""
                MsgBox("Koneksi Tidak ditemukan", MsgBoxStyle.Exclamation, Me.Text)
            End If
        Catch oErr As Exception
            fn_InsertPRB = ""
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Function
    Private Function fn_UpdatePRB(ByVal Nomorsep As String, ByVal NoKartu As String, ByVal Alamat As String) As Boolean
        Try
            REQUEST = ""
            RESPONSE = ""

            Dim oSetKoneksi As New Brigging.clsSetKoneksi
            Dim jsonRequest As String = String.Empty
            Dim oDoctor As New Reference.clsDoctor

            Dim list As New List(Of String)

            For i As Integer = 0 To grvDetail.RowCount - 2
                list.Add(" { " & """kode"": """ & grvDetail.GetRowCellValue(i, colKDOBAT) & """," & """nama"": """ & grvDetail.GetRowCellValue(i, colNAMAOBAT) & """ } ")
            Next

            jsonRequest = " { "
            jsonRequest &= """request"" :  { "
            jsonRequest &= """t_prb"": { "
            jsonRequest &= """noSrb"": """ & txtKDPendaftaran_PRB.Text & """, "
            jsonRequest &= """noSep"": """ & Nomorsep & ""","
            jsonRequest &= """alamat"": """ & Alamat & """, "
            jsonRequest &= """email"": """ & txtEMAIL.Text & """, "
            jsonRequest &= """kodeDPJP"": """ & oDoctor.GetData(grdKDDOCTOR.EditValue).VCLAIM_KDDPJP & """, "
            jsonRequest &= """keterangan"": """ & txtKETERANGAN.Text & """, "
            jsonRequest &= """saran"": """ & txtSARAN.Text & """, "
            jsonRequest &= """user"": """ & sUserID & """, "
            jsonRequest &= """obat"": "
            jsonRequest &= "[ "

            jsonRequest &= String.Join(",", list.ToArray)

            jsonRequest &= "] "
            jsonRequest &= "} "
            jsonRequest &= "} "
            jsonRequest &= "}  "

            Dim uTime As Integer = 0

            If sConsidVclaim <> "" Then
                uTime = (DateTime.UtcNow - New DateTime(1970, 1, 1, 0, 0, 0)).TotalSeconds
                Dim dsSetKoneksi = oSetKoneksi.UpdatePRB(sUrlVclaim, sConsidVclaim, sSecreateKeyVclaim, sUserKeyVclaim, uTime, jsonRequest)

                If dsSetKoneksi <> "" Then
                    Dim allData = JObject.Parse(dsSetKoneksi)

                    Dim CodeResponse As String = String.Empty
                    Dim messageResponse As String = String.Empty

                    CodeResponse = IIf(IsDBNull(allData.Item("metaData").Item("code")) = True, "", allData.Item("metaData").Item("code"))
                    messageResponse = allData("metaData")("message").ToString

                    If CodeResponse = "200" Then
                        REQUEST = jsonRequest
                        RESPONSE = dsSetKoneksi
                        fn_UpdatePRB = True
                    Else
                        fn_UpdatePRB = False
                        MsgBox(CodeResponse & " - " & messageResponse, MsgBoxStyle.Exclamation, Me.Text)
                        Exit Function
                    End If
                Else
                    fn_UpdatePRB = False
                    MsgBox("Insert PRB", MsgBoxStyle.Exclamation, Me.Text)
                End If
            Else
                fn_UpdatePRB = False
                MsgBox("Koneksi Tidak ditemukan", MsgBoxStyle.Exclamation, Me.Text)
            End If
        Catch oErr As Exception
            fn_UpdatePRB = ""
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Function
#End Region
#Region "Grid Method"
    Private Sub DeleteToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DeleteToolStripMenuItem.Click
        If oFormMode = FORM_MODE.FORM_MODE_VIEW Then Exit Sub
        grvDetail.DeleteSelectedRows()
    End Sub
#End Region
#Region "Command Button"
    Private Sub frmPendaftaran_PRB_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
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
    Private Sub fn_LoadDiagnosaPRB()
        Dim oDiagnosaPRB As New Reference.clsDiagnosa_PRB
        Try
            grdKDDIAGNOSA_PRB.Properties.DataSource = oDiagnosaPRB.GetData.Where(Function(x) x.ISACTIVE = True).ToList()
            grdKDDIAGNOSA_PRB.Properties.ValueMember = "KDDIAGNOSA_PRB"
            grdKDDIAGNOSA_PRB.Properties.DisplayMember = "MEMO"
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub grdKDWAREHOUSE_KeyDown(sender As System.Object, e As System.Windows.Forms.KeyEventArgs) Handles grdKDDIAGNOSA_PRB.KeyDown
        If e.KeyCode = Keys.Delete Then
            grdKDDIAGNOSA_PRB.ResetText()
        End If
    End Sub
    Private Sub fn_LoadDoctor()
        Dim oDoctor As New Reference.clsDoctor
        Try
            grdKDDOCTOR.Properties.DataSource = oDoctor.GetData.Where(Function(x) x.ISACTIVE = True).ToList()
            grdKDDOCTOR.Properties.ValueMember = "KDDOCTOR"
            grdKDDOCTOR.Properties.DisplayMember = "NAME_DISPLAY"
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub grdKDDOCTOR_KeyDown(sender As System.Object, e As System.Windows.Forms.KeyEventArgs) Handles grdKDDOCTOR.KeyDown
        If e.KeyCode = Keys.Delete Then
            grdKDDOCTOR.ResetText()
        End If
    End Sub
    'Private Sub txtCARI_KeyPress(sender As Object, e As KeyPressEventArgs)
    '    If Asc(e.KeyChar) = 13 Then
    '        fn_LoadPendaftaranRekamMedisList(txtCARI.Text)
    '    End If
    'End Sub
    Private Sub fn_LoadPendaftaranRekamMedisList(ByVal sParameter As String)
        'Try
        '    If sParameter = String.Empty Then Exit Sub
        '    Dim oPendaftaran As New Admission.clsPendaftaran
        '    Dim dsCustomerList = From x In oPendaftaran.GetDataByRekamMedisRawatJalan(sParameter)
        '                         Select Kode = x.KDPENDAFTARAN, NoSEP = x.NOMORSEP, NoKartuBPJS = x.KARTUBPJS, Pasien = x.M_CUSTOMER.NAME_DISPLAY, TanggalLahir = x.M_CUSTOMER.TANGGALLAHIR.ToString("dd-MM-yyyy"), Alamat = x.JALAN & " " & x.KELURAHAN & " " & x.KECAMATAN & " " & x.KELURAHAN

        '    grdCARIKDREGAWAL.Properties.DataSource = dsCustomerList.ToList()
        '    grdCARIKDREGAWAL.Properties.ValueMember = "Kode"
        '    grdCARIKDREGAWAL.Properties.DisplayMember = "Kode"

        '    grdCARIKDREGAWAL.ShowPopup()
        'Catch oErr As Exception
        '    MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        'End Try
    End Sub
    'Private Sub grdCARIKDREGAWAL_KeyPress(sender As Object, e As KeyPressEventArgs)
    '    txtKDPENDAFTARAN.Text = grdCARIKDREGAWAL.EditValue
    'End Sub
    Private Sub txtNamaObat_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtNamaObat.KeyPress
        If Asc(e.KeyChar) = 13 Then
            Try
                Dim oSetKoneksi As New Brigging.clsSetKoneksi
                Dim uTime As Integer = 0

                If sConsidVclaim <> "" Then
                    uTime = (DateTime.UtcNow - New DateTime(1970, 1, 1, 0, 0, 0)).TotalSeconds
                    Dim dsSetKoneksi = oSetKoneksi.GetDataVClaimReferensiObatGenerikProgramPRB(sUrlVclaim, sConsidVclaim, sSecreateKeyVclaim, sUserKeyVclaim, uTime, txtNamaObat.Text)

                    If dsSetKoneksi <> "" Then
                        Try
                            Dim allData = JObject.Parse(dsSetKoneksi)

                            Dim table As DataTable

                            table = New DataTable("M_KELAS")
                            table.Columns.Add("kode")
                            table.Columns.Add("nama")

                            Dim dsData = oSetKoneksi.Decrypt(allData("response"), sConsidVclaim & sSecreateKeyVclaim & uTime)
                            Dim dsDiagnosa = JObject.Parse(dsData)

                            For Each item In dsDiagnosa("list")
                                table.Rows.Add(New String() {item("kode"), item("nama")})
                            Next

                            grdCARI.Properties.DataSource = table
                            grdCARI.Properties.ValueMember = "kode"
                            grdCARI.Properties.DisplayMember = "nama"

                            grdCARI.ShowPopup()

                            'txtCARI.ResetText()

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
            grvDetail.Focus()
            grvDetail.AddNewRow()
            grvDetail.SetFocusedRowCellValue(colKDOBAT, grdCARI.EditValue)
            grvDetail.SetFocusedRowCellValue(colNAMAOBAT, grdCARI.Text)
            grvDetail.SetFocusedRowCellValue(colSIGNA1, 1)
            grvDetail.SetFocusedRowCellValue(colSIGNA2, 1)
            grvDetail.SetFocusedRowCellValue(colJUMLAH, 1)
            grvDetail.UpdateCurrentRow()
        End If
    End Sub
#End Region
End Class