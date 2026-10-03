Imports DataAccess
Imports UI.WIN.MAIN.My.Resources
Imports System.Linq
Imports Newtonsoft.Json.Linq
Imports System.Data.SqlClient

Public Class frmUpdate_Tanggal_Pulang
#Region "Declaration"
    Private oFormMode As FORM_MODE = FORM_MODE.FORM_MODE_VIEW
    Private sNoId As String
    Private isLoad As Boolean = False
    Private oUpdate_Tanggal_Pulang As New Admission.clsUpdate_Tanggal_Pulang
    Private REQUEST As String = String.Empty
    Private RESPONSE As String = String.Empty
    Private sPopUP As Boolean = False
    Private sAutomatis As Boolean = False
#End Region
#Region "Function"
    Public Sub LoadMeOtomatis(ByVal Automatis As Boolean, ByVal KDPENDAFTARAN As String)
        sAutomatis = Automatis
        fn_LoadKDKUNJUNGANAUTO(KDPENDAFTARAN)
        Kode()
    End Sub
    Public Sub LoadMe(ByVal FormMode As Integer, Optional ByVal NoId As String = "")
        oFormMode = FormMode
        sNoId = NoId
    End Sub
    Private Sub Form_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        fn_ChangeFormState()
        fn_LoadLanguage()
        isLoad = True
        'txtCARI.Text = sREKAMMEDIS
    End Sub
    Public Sub fn_LoadLanguage()
        Try
            Me.Text = Update_Tanggal_Pulang.TITLE

            lNAMAPASIEN.Text = Customer.TITLE
            lKDDEPARTMENT.Text = Department.TITLE
            lKDDOCTOR.Text = Doctor.TITLE

            lNOMORSEP.Text = Update_Tanggal_Pulang.NOMORSEP & " *"
            lDATE.Text = Update_Tanggal_Pulang.TANGGAL
            lKDPENDAFTARAN.Text = Update_Tanggal_Pulang.KDPENDAFTARAN
            lCARAPULANG.Text = Update_Tanggal_Pulang.CARAPULANG

            lNOSURATMENINGGAL.Text = Update_Tanggal_Pulang.NOSURATMENINGGAL
            lDATE_MENINGGAL.Text = Update_Tanggal_Pulang.DATE_MENINGGAL
            lNOLPMANUAL.Text = Update_Tanggal_Pulang.NOLPMANUAL

            btnSaveNew.Caption = Caption.FormSaveNew
            btnSaveClose.Caption = Caption.FormSaveClose
            btnClose.Caption = Caption.FormClose
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub Form_Closing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        sCode = grdKDPENDAFTARAN.Text.Trim.ToUpper
    End Sub
    Private Sub fn_ChangeFormState()
        fn_LoadKDDOCTOR()
        fn_LoadKDDEPARTMENT()

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

        txtNOMORSEP.Properties.ReadOnly = Status
        deDATE.Properties.ReadOnly = Status
        grdKDPENDAFTARAN.Properties.ReadOnly = Status
        cboCARAPULANG.Properties.ReadOnly = Status
        txtNOSURATMENINGGAL.Properties.ReadOnly = Status
        deDATE_MENINGGAL.Properties.ReadOnly = Status
        deDATENOLPMANUAL.Properties.ReadOnly = Status
    End Sub
    Private Sub fn_EmptyMe()
        deDATE.DateTime = Now
        cboCARAPULANG.SelectedIndex = 1
        If sAutomatis = False Then
            txtNOMORSEP.ResetText()
            grdKDPENDAFTARAN.ResetText()
        End If

    End Sub
    Private Sub fn_LoadData()
        Try
            Dim ds = oUpdate_Tanggal_Pulang.GetData(sNoId)

            With ds
                txtNOMORSEP.Text = .NOMORSEP
                deDATE.DateTime = .DATE
                fn_LoadKDKUNJUNGAN(.KDPENDAFTARAN, 3)
                grdKDPENDAFTARAN.Text = .KDPENDAFTARAN

                Dim oKunjungan As New Admission.clsPendaftaran
                Dim dsKunjungan = oKunjungan.GetData(grdKDPENDAFTARAN.EditValue)
                If dsKunjungan IsNot Nothing Then
                    txtNAMAPASIEN.Text = dsKunjungan.M_CUSTOMER.NAME_DISPLAY & " / " & dsKunjungan.KDCUSTOMER
                    grdKDDEPARTMENT_H.Text = dsKunjungan.KDDEPARTMENT
                    grdKDDOCTOR_H.Text = dsKunjungan.KDDOCTOR
                Else
                    txtNAMAPASIEN.ResetText()
                    grdKDDEPARTMENT_H.ResetText()
                    grdKDDOCTOR_H.ResetText()
                End If

                cboCARAPULANG.SelectedIndex = .CARAPULANG
                txtNOSURATMENINGGAL.Text = .NOSURATMENINGGAL
                deDATE_MENINGGAL.DateTime = .DATE_MENINGGAL
                deDATENOLPMANUAL.DateTime = .NOLPMANUAL
            End With
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Function fn_Validate() As Boolean
        Try
            fn_Validate = True
            'If txtNOMORSEP.Text = String.Empty Then
            '    txtNOMORSEP.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
            '    txtNOMORSEP.ErrorText = Statement.ErrorRequired

            '    txtNOMORSEP.Focus()
            '    fn_Validate = False
            '    Exit Function
            'End If
            If grdKDPENDAFTARAN.Text = String.Empty Then
                grdKDPENDAFTARAN.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                grdKDPENDAFTARAN.ErrorText = Statement.ErrorRequired

                grdKDPENDAFTARAN.Focus()
                fn_Validate = False
                Exit Function
            End If

            If cboCARAPULANG.Text ="Meninggal" Then
                If txtNOSURATMENINGGAL.Text = String.Empty Then
                    txtNOSURATMENINGGAL.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                    txtNOSURATMENINGGAL.ErrorText = Statement.ErrorRequired

                    txtNOSURATMENINGGAL.Focus()
                    fn_Validate = False
                    Exit Function
                End If
            End If
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Function
    Private Function fn_UpdateTanggalPulang() As Boolean
        Try
            If txtNOMORSEP.Text = "" Then
                fn_UpdateTanggalPulang = False
                Exit Function
            End If

            Dim jsonRequest As String = String.Empty
            Dim noSep As String = String.Empty
            Dim tglPulang As String = String.Empty
            Dim user As String = String.Empty

            noSep = txtNOMORSEP.Text.Trim.ToUpper
            tglPulang = deDATE.DateTime.ToString("yyyy-MM-dd HH:mm:ss")
            user = sUserID

            jsonRequest = "{ "
            jsonRequest &= """request"" :  { "
            jsonRequest &= """t_sep"": { "
            jsonRequest &= """noSep"": """ & noSep & ""","
            jsonRequest &= """tglPulang"": """ & tglPulang & ""","
            jsonRequest &= """user"": """ & user & """"
            jsonRequest &= "} "
            jsonRequest &= "} "
            jsonRequest &= "} "

            Dim oSetKoneksi As New Brigging.clsSetKoneksi
            Dim dsDataSetKoneksi = oSetKoneksi.GetData().FirstOrDefault(Function(x) x.ISACTIVE = True And x.NAME_DISPLAY = "VCLAIM")
            Dim uTime As Integer = 0

            If dsDataSetKoneksi IsNot Nothing Then
                uTime = (DateTime.UtcNow - New DateTime(1970, 1, 1, 0, 0, 0)).TotalSeconds
                Dim dsSetKoneksi = oSetKoneksi.UpdateTanggalPulang(dsDataSetKoneksi.ALAMATWEB, dsDataSetKoneksi.CONSID, dsDataSetKoneksi.SECREATKEY, dsDataSetKoneksi.USER_KEY, uTime, jsonRequest)

                If dsSetKoneksi <> "" Then
                    Dim allData = JObject.Parse(dsSetKoneksi)

                    Dim CodeResponse As String = String.Empty
                    Dim messageResponse As String = String.Empty

                    CodeResponse = IIf(IsDBNull(allData.Item("metaData").Item("code")) = True, "", allData.Item("metaData").Item("code"))
                    messageResponse = allData("metaData")("message").ToString

                    If CodeResponse = "200" Then
                        REQUEST = jsonRequest
                        RESPONSE = dsSetKoneksi
                        fn_UpdateTanggalPulang = True
                    Else
                        fn_UpdateTanggalPulang = False
                        MsgBox(CodeResponse & " - " & messageResponse, MsgBoxStyle.Exclamation, Me.Text)
                        Exit Function
                    End If
                Else
                    fn_UpdateTanggalPulang = False
                    MsgBox("Update SEP Data Gagal/Kosong", MsgBoxStyle.Exclamation, Me.Text)
                End If
            Else
                fn_UpdateTanggalPulang = False
                MsgBox("Koneksi Tidak ditemukan", MsgBoxStyle.Exclamation, Me.Text)
            End If
        Catch oErr As Exception
            fn_UpdateTanggalPulang = False
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Function
    Private Function fn_UpdateTanggalPulangv2() As Boolean
        Try
            If txtNOMORSEP.Text = "" Then
                fn_UpdateTanggalPulangv2 = False
                Exit Function
            End If

            Dim jsonRequest As String = String.Empty
            Dim noSep As String = String.Empty
            Dim tglPulang As String = String.Empty
            Dim user As String = String.Empty

            noSep = txtNOMORSEP.Text.Trim.ToUpper
            tglPulang = deDATE.DateTime.ToString("yyyy-MM-dd")
            user = sUserID

            jsonRequest = "{ "
            jsonRequest &= """request"" :  { "
            jsonRequest &= """t_sep"": { "
            jsonRequest &= """noSep"": """ & noSep & ""","
            jsonRequest &= """statusPulang"": """ & cboCARAPULANG.SelectedIndex + 1 & ""","
            jsonRequest &= """noSuratMeninggal"": """ & txtNOSURATMENINGGAL.Text & ""","
            jsonRequest &= """tglMeninggal"": """ & IIf(cboCARAPULANG.Text = "Meninggal", deDATE_MENINGGAL.DateTime.ToString("yyyy-MM-dd"), "") & ""","
            jsonRequest &= """tglPulang"": """ & tglPulang & ""","
            jsonRequest &= """noLPManual"": """ & IIf(cboCARAPULANG.Text = "Meninggal", deDATENOLPMANUAL.DateTime.ToString("yyyy-MM-dd"), "") & ""","
            jsonRequest &= """user"": """ & user & """"
            jsonRequest &= "} "
            jsonRequest &= "} "
            jsonRequest &= "} "

            Dim oSetKoneksi As New Brigging.clsSetKoneksi
            Dim dsDataSetKoneksi = oSetKoneksi.GetData().FirstOrDefault(Function(x) x.ISACTIVE = True And x.NAME_DISPLAY = "VCLAIM")
            Dim uTime As Integer = 0

            If dsDataSetKoneksi IsNot Nothing Then
                uTime = (DateTime.UtcNow - New DateTime(1970, 1, 1, 0, 0, 0)).TotalSeconds
                Dim dsSetKoneksi = oSetKoneksi.UpdateTanggalPulang(dsDataSetKoneksi.ALAMATWEB, dsDataSetKoneksi.CONSID, dsDataSetKoneksi.SECREATKEY, dsDataSetKoneksi.USER_KEY, uTime, jsonRequest)

                If dsSetKoneksi <> "" Then
                    Dim allData = JObject.Parse(dsSetKoneksi)

                    Dim CodeResponse As String = String.Empty
                    Dim messageResponse As String = String.Empty

                    CodeResponse = IIf(IsDBNull(allData.Item("metaData").Item("code")) = True, "", allData.Item("metaData").Item("code"))
                    messageResponse = allData("metaData")("message").ToString

                    If CodeResponse = "200" Then
                        REQUEST = jsonRequest
                        RESPONSE = dsSetKoneksi
                        fn_UpdateTanggalPulangv2 = True
                    Else
                        fn_UpdateTanggalPulangv2 = False
                        MsgBox(CodeResponse & " - " & messageResponse, MsgBoxStyle.Exclamation, Me.Text)
                        Exit Function
                    End If
                Else
                    fn_UpdateTanggalPulangv2 = False
                    MsgBox("Update SEP Data Gagal/Kosong", MsgBoxStyle.Exclamation, Me.Text)
                End If
            Else
                fn_UpdateTanggalPulangv2 = False
                MsgBox("Koneksi Tidak ditemukan", MsgBoxStyle.Exclamation, Me.Text)
            End If
        Catch oErr As Exception
            fn_UpdateTanggalPulangv2 = False
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Function
    Private Function fn_Save() As Boolean
        Try
            ' ***** HEADER *****
            Dim ds = oUpdate_Tanggal_Pulang.GetStructureHeader
            With ds
                Try
                    .DATECREATED = oUpdate_Tanggal_Pulang.GetData(sNoId).DATECREATED
                Catch oErr As Exception
                    .DATECREATED = Now
                End Try
                .DATEUPDATED = Now
                .DATE = deDATE.DateTime
                .NOMORSEP = txtNOMORSEP.Text.Trim.ToUpper
                .KDUSER = sUserID
                .REQUEST = REQUEST
                .RESPON = RESPONSE
                .KDPENDAFTARAN = grdKDPENDAFTARAN.EditValue
                .CARAPULANG = cboCARAPULANG.SelectedIndex
                .NOSURATMENINGGAL = txtNOSURATMENINGGAL.Text
                .DATE_MENINGGAL = deDATE_MENINGGAL.DateTime
                .NOLPMANUAL = deDATENOLPMANUAL.DateTime
            End With

            If oFormMode = FORM_MODE.FORM_MODE_ADD Then
                Try
                    Dim sKDPENDAFATARN As String = String.Empty
                    fn_Save = oUpdate_Tanggal_Pulang.InsertData(ds, sKDPENDAFATARN)
                Catch oErr As Exception
                    MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            ElseIf oFormMode = FORM_MODE.FORM_MODE_EDIT Then
                Try
                    fn_Save = oUpdate_Tanggal_Pulang.UpdateData(ds)
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
    Private Sub btnUpdate_Click() Handles btnUpdate.ItemClick
        If txtNOMORSEP.Text = String.Empty Then
            MsgBox("Nomor SEP kosong", MsgBoxStyle.Information, Me.Text)
            Exit Sub
        End If
        If MsgBox("Apakah akan Update tanpa nommor register?", MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, Me.Text) = MsgBoxResult.No Then Exit Sub
        If fn_UpdateTanggalPulang() = True Then
            MsgBox("Berhasil Update Tanggal Pulang", MsgBoxStyle.Information, Me.Text)
        End If
    End Sub
    Private Sub btnSaveNew_Click() Handles btnSaveNew.ItemClick
        If fn_Validate() = False Then Exit Sub
        If MsgBox(Statement.SaveQuestion, MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, Me.Text) = MsgBoxResult.No Then Exit Sub

        REQUEST = String.Empty
        RESPONSE = String.Empty

        If fn_UpdateTanggalPulang() = True Then
            If fn_Save() = False Then
                MsgBox(Statement.SaveFail, MsgBoxStyle.Exclamation, Me.Text)
            Else
                MsgBox(Statement.SaveSuccess, MsgBoxStyle.Information, Me.Text)
                sStatusSave = "NEW"
                Me.Close()
            End If
        End If

    End Sub
    Private Sub btnSaveClose_Click() Handles btnSaveClose.ItemClick
        If fn_Validate() = False Then Exit Sub
        If MsgBox(Statement.SaveQuestion, MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, Me.Text) = MsgBoxResult.No Then Exit Sub

        REQUEST = String.Empty
        RESPONSE = String.Empty

        If fn_UpdateTanggalPulang() = True Then
            If fn_Save() = False Then
                MsgBox(Statement.SaveFail, MsgBoxStyle.Exclamation, Me.Text)
            Else
                MsgBox(Statement.SaveSuccess, MsgBoxStyle.Information, Me.Text)
                Me.Close()
            End If
        End If
    End Sub
    Private Sub btnClose_Click() Handles btnClose.ItemClick
        Me.Close()
    End Sub

#End Region
#Region "Lookup / Event"
    Private Sub fn_LoadKDKUNJUNGAN(ByVal sParameter As String, ByVal sCari As Integer)
        Try
            Dim oConn As New SqlConnection
            Dim oComm As New SqlCommand
            Dim da As SqlDataAdapter
            Dim ds As New DataSet
            Dim SQL As String

            Dim sConn As String = Decrypt(My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Software\SIMRS\SW\", "Database", "").ToString())

            oConn = New SqlConnection(sConn)
            If oConn.State = ConnectionState.Closed Then
                oConn.Open()
            End If

            SQL = "SELECT "
            SQL &= "A.KDPENDAFTARAN "
            SQL &= ",D.NAME_DISPLAY "
            SQL &= ",A.DATE "
            SQL &= ",TUJUAN = B.NAME_DISPLAY  "
            SQL &= ",DPJP = C.NAME_DISPLAY "
            SQL &= ",PULANG = ISNULL((SELECT CONVERT(BIT, 1) FROM T_UPDATE_TANGGAL_PULANG WHERE A.KDPENDAFTARAN = KDPENDAFTARAN AND CARAPULANG <> 5), ISNULL((SELECT CONVERT(BIT, 1) FROM T_UPDATE_TANGGAL_PULANG WHERE A.KDPENDAFTARAN_AWAL = KDPENDAFTARAN AND CARAPULANG <> 5), CONVERT(BIT, 0)))  "
            SQL &= ",RANAP = (SELECT CASE WHEN KDPENDAFTARAN_AWAL <> '' THEN CONVERT(BIT, 1) ELSE CONVERT(BIT, 0) END) "
            SQL &= "FROM "
            SQL &= "S_PENDAFTARAN_H A "
            SQL &= "INNER JOIN M_DEPARTMENT B "
            SQL &= "ON A.KDDEPARTMENT = B.KDDEPARTMENT "
            SQL &= "INNER JOIN M_DOCTOR C "
            SQL &= "ON A.KDDOCTOR = C.KDDOCTOR "
            SQL &= "INNER JOIN M_CUSTOMER D "
            SQL &= "ON A.KDCUSTOMER = D.KDCUSTOMER "
            If sCari = 0 Then
                SQL &= "WHERE A.KDCUSTOMER = '" & sParameter & "' "
            ElseIf sCari = 1 Then
                SQL &= "WHERE D.NAME_DISPLAY LIKE '%" & sParameter & "%' "
            Else
                SQL &= "WHERE A.KDPENDAFTARAN LIKE '%" & sParameter & "%' "
            End If
            SQL &= "AND A.CATEGORY = 1 "
            SQL &= "ORDER BY A.DATE DESC "

            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "ALL")

            grdKDPENDAFTARAN.Properties.DataSource = ds.Tables("ALL")
            grdKDPENDAFTARAN.Properties.ValueMember = "KDPENDAFTARAN"
            grdKDPENDAFTARAN.Properties.DisplayMember = "KDPENDAFTARAN"

            If sPopUP = True Then
                grdKDPENDAFTARAN.ShowPopup()
            End If

            If oConn.State = ConnectionState.Open Then
                oConn.Close()
            End If
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadKDKUNJUNGANAUTO(ByVal sParameter As String)
        Try
            Dim oConn As New SqlConnection
            Dim oComm As New SqlCommand
            Dim da As SqlDataAdapter
            Dim ds As New DataSet
            Dim SQL As String

            Dim sConn As String = Decrypt(My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Software\SIMRS\SW\", "Database", "").ToString())

            oConn = New SqlConnection(sConn)
            If oConn.State = ConnectionState.Closed Then
                oConn.Open()
            End If

            SQL = "SELECT "
            SQL &= "A.KDPENDAFTARAN "
            SQL &= ",D.NAME_DISPLAY "
            SQL &= ",A.DATE "
            SQL &= ",TUJUAN = B.NAME_DISPLAY  "
            SQL &= ",DPJP = C.NAME_DISPLAY "
            SQL &= ",PULANG = ISNULL((SELECT CONVERT(BIT, 1) FROM T_UPDATE_TANGGAL_PULANG WHERE A.KDPENDAFTARAN = KDPENDAFTARAN AND CARAPULANG <> 5), ISNULL((SELECT CONVERT(BIT, 1) FROM T_UPDATE_TANGGAL_PULANG WHERE A.KDPENDAFTARAN_AWAL = KDPENDAFTARAN AND CARAPULANG <> 5), CONVERT(BIT, 0)))  "
            SQL &= ",RANAP = (SELECT CASE WHEN KDPENDAFTARAN_AWAL <> '' THEN CONVERT(BIT, 1) ELSE CONVERT(BIT, 0) END) "
            SQL &= "FROM "
            SQL &= "S_PENDAFTARAN_H A "
            SQL &= "INNER JOIN M_DEPARTMENT B "
            SQL &= "ON A.KDDEPARTMENT = B.KDDEPARTMENT "
            SQL &= "INNER JOIN M_DOCTOR C "
            SQL &= "ON A.KDDOCTOR = C.KDDOCTOR "
            SQL &= "INNER JOIN M_CUSTOMER D "
            SQL &= "ON A.KDCUSTOMER = D.KDCUSTOMER "
            SQL &= "WHERE A.KDPENDAFTARAN = '" & sParameter & "' "
            SQL &= "AND A.CATEGORY = 1 "
            SQL &= "ORDER BY A.DATE DESC "

            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "ALL")

            grdKDPENDAFTARAN.Properties.DataSource = ds.Tables("ALL")
            grdKDPENDAFTARAN.Properties.ValueMember = "KDPENDAFTARAN"
            grdKDPENDAFTARAN.Properties.DisplayMember = "KDPENDAFTARAN"

            grdKDPENDAFTARAN.Text = sParameter

            If oConn.State = ConnectionState.Open Then
                oConn.Close()
            End If
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub txtCARI_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtCARI.KeyPress
        If Asc(e.KeyChar) = 13 Then
            sPopUP = True
            If txtCARI.Text = String.Empty Then Exit Sub
            fn_LoadKDKUNJUNGAN(IIf(cboCARI.SelectedIndex = 0, txtCARI.Text.ToString.Trim, txtCARI.Text.ToString.Trim), cboCARI.SelectedIndex)
            txtCARI.ResetText()
        End If
    End Sub
    Private Sub Kode()
        Dim oTanggalPulang As New Admission.clsUpdate_Tanggal_Pulang
        Dim ds = oTanggalPulang.GetDatabyKD(grdKDPENDAFTARAN.EditValue)

        If ds IsNot Nothing Then
            txtNAMAPASIEN.ResetText()
            grdKDDEPARTMENT_H.ResetText()
            grdKDDOCTOR_H.ResetText()
            MsgBox("Pasien Atas Nama : " & ds.S_PENDAFTARAN_H.M_CUSTOMER.NAME_DISPLAY & "Sudah Ada Transaksi Pulang", MsgBoxStyle.Exclamation, Me.Text)
        Else
            Dim oKunjungan As New Admission.clsPendaftaran
            Dim dsKunjungan = oKunjungan.GetData(grdKDPENDAFTARAN.EditValue)
            If dsKunjungan IsNot Nothing Then
                txtNAMAPASIEN.Text = dsKunjungan.M_CUSTOMER.NAME_DISPLAY & " / " & dsKunjungan.KDCUSTOMER
                grdKDDEPARTMENT_H.Text = dsKunjungan.KDDEPARTMENT
                grdKDDOCTOR_H.Text = dsKunjungan.KDDOCTOR
                txtNOMORSEP.Text = dsKunjungan.NOMORSEP
            Else
                txtNAMAPASIEN.ResetText()
                grdKDDEPARTMENT_H.ResetText()
                grdKDDOCTOR_H.ResetText()
                txtNOMORSEP.ResetText()
            End If
        End If
    End Sub
    Private Sub grdKDPENDAFTARAN_KeyPress(sender As Object, e As KeyPressEventArgs) Handles grdKDPENDAFTARAN.KeyPress
        If Asc(e.KeyChar) = 13 Then
            Kode()
        End If
    End Sub
    Private Sub fn_LoadKDDOCTOR()
        Dim oDoctor As New Reference.clsDoctor
        Try
            Dim ds = oDoctor.GetData.Where(Function(x) x.ISACTIVE = True).ToList()

            grdKDDOCTOR_H.Properties.DataSource = ds
            grdKDDOCTOR_H.Properties.ValueMember = "KDDOCTOR"
            grdKDDOCTOR_H.Properties.DisplayMember = "NAME_DISPLAY"

        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadKDDEPARTMENT()
        Dim oDepartment As New Reference.clsDepartment
        Try
            Dim ds = oDepartment.GetData.Where(Function(x) x.ISACTIVE = True).ToList()

            grdKDDEPARTMENT_H.Properties.DataSource = ds
            grdKDDEPARTMENT_H.Properties.ValueMember = "KDDEPARTMENT"
            grdKDDEPARTMENT_H.Properties.DisplayMember = "NAME_DISPLAY"

        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub cboCARAPULANG_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboCARAPULANG.SelectedIndexChanged
        If isLoad = True Then
            If cboCARAPULANG.Text = "Meninggal" Then
                lNOSURATMENINGGAL.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                lDATE_MENINGGAL.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                lNOLPMANUAL.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            Else
                txtNOSURATMENINGGAL.ResetText()
                lNOSURATMENINGGAL.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                lDATE_MENINGGAL.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                lNOLPMANUAL.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            End If
        End If
    End Sub
#End Region
End Class