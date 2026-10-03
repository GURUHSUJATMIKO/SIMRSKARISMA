Imports DataAccess
Imports UI.WIN.MAIN.My.Resources
Imports System.Linq
Imports Newtonsoft.Json.Linq
Imports System.Data.SqlClient

Public Class frmCustomer
#Region "Declaration"
    Private oFormMode As FORM_MODE = FORM_MODE.FORM_MODE_VIEW
    Private sNoId As String
    Private isLoad As Boolean = False
    Private oCustomer As New Reference.clsCustomer
    Private sNopasien As String = String.Empty
    Private sCari As Boolean = False
#End Region
#Region "Function"
    Public Sub fn_LoadRM(ByVal Nopasien As String, ByVal CariPasien As Boolean)
        sNopasien = Nopasien.PadLeft(9, "0")
        sCari = CariPasien
    End Sub
    Public Sub LoadMe(ByVal FormMode As Integer, Optional ByVal NoId As String = "")
        oFormMode = FormMode
        sNoId = NoId
    End Sub
    Private Sub Form_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        fn_ChangeFormState()
        fn_LoadLanguage()
        isLoad = True

        If sRMBPJS <> "" Then
            If oFormMode = FORM_MODE.FORM_MODE_ADD Then
                txtKDCUSTOMER.Text = sRMBPJS
            End If
        End If
    End Sub
    Public Sub fn_LoadLanguage()
        Try
            Me.Text = Customer.TITLE

            lRM.Text = Customer.KDCUSTOMER
            lKDDAFTAR_L1.Text = sDaftar_L1
            lKDDAFTAR_L2.Text = sDaftar_L2
            lKDDAFTAR_L3.Text = sDaftar_L3
            lKDDAFTAR_L4.Text = sDaftar_L4
            lKDDAFTAR_L5.Text = sDaftar_L5
            lKDDAFTAR_L6.Text = sDaftar_L6
            lKTP.Text = Customer.KTP
            lKARTUBPJS.Text = Customer.KARTUBPJS
            lNAME_DISPLAY.Text = Customer.NAME_DISPLAY
            lJALAN.Text = Customer.JALAN
            lKDKELURAHAN.Text = Customer.KDKELURAHAN
            lKECAMATAN.Text = Customer.KECAMATAN
            lPROPINSI.Text = "Propinsi"
            lKOTA.Text = Customer.KOTA
            lKODEPOS.Text = Customer.KODEPPOS
            lNOMORTELEPON.Text = Customer.NOTELEPON
            lTEMPATLAHIR.Text = Customer.TEMPATLAHIR
            lTANGGALLAHIR.Text = Customer.TANGGALLAHIR
            lKDJENISKELAMIN.Text = Customer.KDJENISKELAMIN
            lSTATUSKAWIN.Text = Customer.STATUSKAWIN
            lAGAMA.Text = Customer.KDAGAMA
            lNEGARA.Text = Customer.NEGARA
            lSUKU.Text = Customer.KDSUKU
            lPENDIDIKAN.Text = Customer.PENDIDIKAN
            lEMAIL.Text = Customer.EMAIL
            lNOTELEPON_WA.Text = Customer.NOTELEPON_WA
            lGOLONGANDARAH.Text = Customer.KDGOLONGANDARAH
            chkSTATUSHIDUP.Text = Customer.KDSTATUSHIDUP
            chkISACTIVE.Text = Customer.ISACTIVE

            btnSaveNew.Caption = Caption.FormSaveNew
            btnSaveClose.Caption = Caption.FormSaveClose
            btnClose.Caption = Caption.FormClose
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub Form_Closing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        sCode = txtKDCUSTOMER.Text.Trim.ToUpper
    End Sub
    Private Sub fn_ChangeFormState()
        fn_LoadStatusKawin()
        fn_LoadPendidikan()
        fn_LoadPropinsi()
        fn_LoadDaftar1()
        fn_LoadDaftar2()
        fn_LoadDaftar3()
        fn_LoadDaftar4()
        fn_LoadDaftar5()
        fn_LoadDaftar6()
        fn_LoadKDAGAMA()
        fn_LoadKDSUKU()
        fn_LoadKDHUBUNGAN()

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
            txtKDCUSTOMER.Properties.ReadOnly = False
        Else
            txtKDCUSTOMER.Properties.ReadOnly = True
        End If

        grdKDDAFTAR_L1.Properties.ReadOnly = Status
        grdKDDAFTAR_L2.Properties.ReadOnly = Status
        grdKDDAFTAR_L3.Properties.ReadOnly = Status
        grdKDDAFTAR_L4.Properties.ReadOnly = Status
        grdKDDAFTAR_L5.Properties.ReadOnly = Status
        grdKDDAFTAR_L6.Properties.ReadOnly = Status
        txtKTP.Properties.ReadOnly = Status
        txtKARTUBPJS.Properties.ReadOnly = Status
        txtNAME_DISPLAY.Properties.ReadOnly = Status
        txtJALAN.Properties.ReadOnly = Status
        grdKDKELURAHAN.Properties.ReadOnly = Status
        grdKDPROPINSI.Properties.ReadOnly = Status
        grdKDKOTA.Properties.ReadOnly = Status
        grdKDKECAMATAN.Properties.ReadOnly = Status
        txtKODEPOS.Properties.ReadOnly = Status
        txtNOMORTELEPON.Properties.ReadOnly = Status
        txtTEMPATLAHIR.Properties.ReadOnly = Status
        deTANGGLLAHIR.Properties.ReadOnly = Status
        cboKDJENISKELAMIN.Properties.ReadOnly = Status
        cboSTATUSKAWIN.Properties.ReadOnly = Status
        grdKDAGAMA.Properties.ReadOnly = Status
        cboNEGARA.Properties.ReadOnly = Status
        grdKDSUKU.Properties.ReadOnly = Status
        cboPENDIDIKAN.Properties.ReadOnly = Status
        txtEMAIL.Properties.ReadOnly = Status
        txtNOTELEPON_WA.Properties.ReadOnly = Status
        cboKDGOLONGANDARAH.Properties.ReadOnly = Status
        chkSTATUSHIDUP.Properties.ReadOnly = Status
        chkISACTIVE.Properties.ReadOnly = Status
        txtALAMATPASIEN_SIMRS.Properties.ReadOnly = Status
        txtNAMAPENANGGUNGJAWAB.Properties.ReadOnly = Status
        grdKDHUBUNGAN.Properties.ReadOnly = Status
        txtALAMATPENANGGUNGJAWAB.Properties.ReadOnly = Status
        txtNOMORTELEPONPENANGGUNGJAWAB.Properties.ReadOnly = Status

    End Sub
    Private Sub fn_EmptyMe()
        If sNopasien = String.Empty Then
            txtKDCUSTOMER.Text = "<---AUTO--->"
        Else
            txtKDCUSTOMER.Text = sNopasien
        End If
        grdKDDAFTAR_L2.ResetText()
        txtKTP.ResetText()
        txtKARTUBPJS.ResetText()
        txtNAME_DISPLAY.ResetText()
        txtJALAN.ResetText()
        grdKDKELURAHAN.ResetText()
        grdKDPROPINSI.ResetText()
        grdKDKOTA.ResetText()
        grdKDKECAMATAN.ResetText()
        txtKODEPOS.ResetText()
        txtNOMORTELEPON.ResetText()
        txtTEMPATLAHIR.ResetText()
        deTANGGLLAHIR.ResetText()
        cboKDJENISKELAMIN.ResetText()
        cboSTATUSKAWIN.SelectedIndex = 0
        grdKDAGAMA.Text = oCustomer.AgamaDefault
        grdKDDAFTAR_L3.ResetText()
        grdKDDAFTAR_L4.ResetText()
        grdKDDAFTAR_L5.ResetText()
        cboNEGARA.SelectedIndex = 0
        grdKDSUKU.Text = oCustomer.SukuDefault
        cboPENDIDIKAN.SelectedIndex = 0
        txtEMAIL.ResetText()
        txtNOTELEPON_WA.ResetText()
        cboKDGOLONGANDARAH.Text = "-"
        chkSTATUSHIDUP.Checked = True
        chkISACTIVE.Checked = True
        txtALAMATPASIEN_SIMRS.ResetText()
        txtNAMAPENANGGUNGJAWAB.ResetText()
        grdKDHUBUNGAN.ResetText()
        txtALAMATPENANGGUNGJAWAB.ResetText()
        txtNOMORTELEPONPENANGGUNGJAWAB.ResetText()

        Dim oPendaftaran As New Admission.clsPendaftaran
        grdKDDAFTAR_L1.Text = oPendaftaran.Daftar_L1_Default
        grdKDDAFTAR_L2.Text = oPendaftaran.Daftar_L2_Default
        grdKDDAFTAR_L3.Text = oPendaftaran.Daftar_L3_Default
        grdKDDAFTAR_L4.Text = oPendaftaran.Daftar_L4_Default
        grdKDDAFTAR_L5.Text = oPendaftaran.Daftar_L5_Default
        grdKDDAFTAR_L6.Text = oPendaftaran.Daftar_L6_Default

        Dim oPropinsi As New Reference.clsPropinsi
        grdKDPROPINSI.Text = oPropinsi.Propinsi_Default()
        fn_LoadKota(grdKDPROPINSI.EditValue)

        If sCari = True Then
            Try
                Dim oConn_1 As New SqlConnection
                Dim oComm_1 As New SqlCommand
                Dim da_1 As SqlDataAdapter
                Dim ds_1 As New DataSet
                Dim SQL_1 As String

                oConn_1 = New SqlConnection(sConnOld)

                If oConn_1.State = ConnectionState.Closed Then
                    oConn_1.Open()
                End If

                SQL_1 = "SELECT "
                SQL_1 &= "* "
                SQL_1 &= "FROM M_CUSTOMER A "
                SQL_1 &= "WHERE A.KDCUSTOMER = '" & sNopasien & "' "

                oComm_1.Connection = oConn_1
                oComm_1.CommandText = SQL_1
                oComm_1.CommandTimeout = 120
                oComm_1.CommandType = CommandType.Text

                da_1 = New SqlDataAdapter(oComm_1)
                da_1.Fill(ds_1, "M_CUSTOMER")

                For iLoop As Integer = 0 To ds_1.Tables("M_CUSTOMER").Rows.Count - 1
                    Dim dsRekap As New DataAccess.R_HISTORY_PENDAFATRAN
                    With ds_1.Tables("M_CUSTOMER")
                        txtKTP.Text = .Rows(iLoop)("NIK")
                        txtKARTUBPJS.Text = fn_LoadKartu(sNopasien)
                        txtNAME_DISPLAY.Text = .Rows(iLoop)("NAME_DISPLAY")
                        txtTEMPATLAHIR.Text = .Rows(iLoop)("TEMPATLAHIR")
                        deTANGGLLAHIR.DateTime = .Rows(iLoop)("TANGGALLAHIR")
                        grdKDAGAMA.Text = .Rows(iLoop)("AGAMA")
                        If .Rows(iLoop)("JK") = 1 Then
                            cboKDJENISKELAMIN.SelectedIndex = 0
                        Else
                            cboKDJENISKELAMIN.SelectedIndex = 1
                        End If

                        cboSTATUSKAWIN.SelectedIndex = .Rows(iLoop)("STATUS") - 1
                        cboKDGOLONGANDARAH.SelectedIndex = .Rows(iLoop)("GOLDARAH") - 1
                        If .Rows(iLoop)("PENDIDIKAN") = 0 Then
                            cboPENDIDIKAN.SelectedIndex = 0
                        Else
                            cboPENDIDIKAN.SelectedIndex = .Rows(iLoop)("PENDIDIKAN") - 1
                        End If
                        grdKDSUKU.Text = .Rows(iLoop)("KDETNIS")
                        txtNOMORTELEPON.Text = .Rows(iLoop)("MOBILE")
                        If .Rows(iLoop)("KEWARGANEGARAAN") = "INDONESIA" Then
                            cboNEGARA.SelectedIndex = 0
                        Else
                            cboNEGARA.SelectedIndex = 1
                        End If

                        fn_LoadAlamat(txtKDCUSTOMER.Text)
                    End With
                Next

                If oConn_1.State = ConnectionState.Open Then
                    oConn_1.Close()
                End If

                grdKDHUBUNGAN.Text = "HB_0000000001"

            Catch oErr As Exception
                MsgBox("Preview Data : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            End Try
        End If
    End Sub
    Private Function fn_LoadKartu(ByVal KDCUSTOMER As String) As String
        Try
            fn_LoadKartu = ""

            Dim oConn_1 As New SqlConnection
            Dim oComm_1 As New SqlCommand
            Dim da_1 As SqlDataAdapter
            Dim ds_1 As New DataSet
            Dim SQL_1 As String

            oConn_1 = New SqlConnection(sConnOld)

            If oConn_1.State = ConnectionState.Closed Then
                oConn_1.Open()
            End If

            SQL_1 = "SELECT "
            SQL_1 &= "* "
            SQL_1 &= "FROM M_CUSTOMER_DEBTOR A "
            SQL_1 &= "WHERE A.KDCUSTOMER = '" & sNopasien & "' "
            SQL_1 &= "AND A.KDDEBTOR = 1 "

            oComm_1.Connection = oConn_1
            oComm_1.CommandText = SQL_1
            oComm_1.CommandTimeout = 120
            oComm_1.CommandType = CommandType.Text

            da_1 = New SqlDataAdapter(oComm_1)
            da_1.Fill(ds_1, "M_CUSTOMER_DEBTOR")

            For iLoop As Integer = 0 To ds_1.Tables("M_CUSTOMER_DEBTOR").Rows.Count - 1
                Dim dsRekap As New DataAccess.R_HISTORY_PENDAFATRAN
                With ds_1.Tables("M_CUSTOMER_DEBTOR")
                    fn_LoadKartu = .Rows(iLoop)("NOKARTU")
                End With
            Next

            If oConn_1.State = ConnectionState.Open Then
                oConn_1.Close()
            End If

        Catch oErr As Exception
            fn_LoadKartu = ""
            MsgBox("Preview Data : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Function
    Private Sub fn_LoadAlamat(ByVal KDCUSTOMER As String)
        Try

            Dim oConn_1 As New SqlConnection
            Dim oComm_1 As New SqlCommand
            Dim da_1 As SqlDataAdapter
            Dim ds_1 As New DataSet
            Dim SQL_1 As String

            oConn_1 = New SqlConnection(sConnOld)

            If oConn_1.State = ConnectionState.Closed Then
                oConn_1.Open()
            End If

            SQL_1 = "SELECT "
            SQL_1 &= "TOP 1 * "
            SQL_1 &= "FROM M_CUSTOMER_ADDRESS A "
            SQL_1 &= "WHERE A.KDCUSTOMER = '" & sNopasien & "' "
            SQL_1 &= "ORDER BY SEQ DESC "

            oComm_1.Connection = oConn_1
            oComm_1.CommandText = SQL_1
            oComm_1.CommandTimeout = 120
            oComm_1.CommandType = CommandType.Text

            da_1 = New SqlDataAdapter(oComm_1)
            da_1.Fill(ds_1, "M_CUSTOMER_ADDRESS")

            For iLoop As Integer = 0 To ds_1.Tables("M_CUSTOMER_ADDRESS").Rows.Count - 1
                Dim dsRekap As New DataAccess.R_HISTORY_PENDAFATRAN
                With ds_1.Tables("M_CUSTOMER_ADDRESS")
                    txtJALAN.Text = .Rows(iLoop)("ADDRESS_STREET")

                    Dim oPropinsis As New Reference.clsPropinsi
                    Dim dsProvinsi = oPropinsis.GetData(.Rows(iLoop)("KODEPROV"))

                    If dsProvinsi IsNot Nothing Then
                        fn_LoadPropinsi()
                        grdKDPROPINSI.Text = dsProvinsi.KDPROPINSI
                    End If

                    fn_LoadKota(grdKDPROPINSI.EditValue)

                    Dim oKota As New Reference.clsKabupaten
                    Dim dsKabupaten = oKota.GetData(.Rows(iLoop)("KODEKOTA"))

                    If dsKabupaten IsNot Nothing Then
                        grdKDKOTA.Text = dsKabupaten.KDKABUPATEN
                    End If

                    fn_LoadKecamatan(grdKDKOTA.EditValue)

                    Dim oKecamatan As New Reference.clsKecamatan
                    Dim dsKecamatan = oKecamatan.GetData(.Rows(iLoop)("KODEKEC"))

                    If dsKecamatan IsNot Nothing Then
                        grdKDKECAMATAN.Text = dsKecamatan.KDKECAMATAN
                    End If

                    fn_LoadKelurahan(grdKDKECAMATAN.EditValue)

                    Dim oKelurahan As New Reference.clsKelurahan
                    Dim dsKelurahan = oKelurahan.GetData(.Rows(iLoop)("KODEDESA"))

                    If dsKelurahan IsNot Nothing Then
                        grdKDKELURAHAN.Text = dsKelurahan.KDKELURAHAN
                        txtKODEPOS.Text = dsKelurahan.KODEPOS
                    End If

                    txtALAMATPASIEN_SIMRS.Text = txtJALAN.Text.ToString.Trim.ToUpper & " " & grdKDPROPINSI.Text & " " & grdKDKOTA.Text & " " & grdKDKECAMATAN.Text
                End With
            Next

            If oConn_1.State = ConnectionState.Open Then
                oConn_1.Close()
            End If

        Catch oErr As Exception
            MsgBox("Preview Data : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadData()
        Try
            Dim ds = oCustomer.GetData(sNoId)
            Dim KdPropinsi As String = String.Empty
            Dim KdKabupaten As String = String.Empty
            Dim KdKecamatan As String = String.Empty

            With ds
                txtKDCUSTOMER.Text = .KDCUSTOMER
                grdKDDAFTAR_L1.Text = .KDDAFTAR_L1
                grdKDDAFTAR_L2.Text = .KDDAFTAR_L2
                grdKDDAFTAR_L3.Text = .KDDAFTAR_L3
                grdKDDAFTAR_L4.Text = .KDDAFTAR_L4
                grdKDDAFTAR_L5.Text = .KDDAFTAR_L5
                grdKDDAFTAR_L6.Text = .KDDAFTAR_L6
                txtKTP.Text = .KTP
                txtKARTUBPJS.Text = .KARTUBPJS
                txtNAME_DISPLAY.Text = .NAME_DISPLAY
                txtJALAN.Text = .JALAN
                grdKDKELURAHAN.Text = .KDKELURAHAN

                If IsDBNull(.KDKELURAHAN) Then

                Else
                    If .KELURAHAN IsNot Nothing Then
                        Dim oKelurahans As New Reference.clsKelurahan
                        Dim dsKelurahan = oKelurahans.GetData(.KDKELURAHAN)

                        If dsKelurahan IsNot Nothing Then
                            fn_LoadKelurahan(dsKelurahan.KDKECAMATAN)
                            KdKecamatan = dsKelurahan.KDKECAMATAN
                            grdKDKELURAHAN.Text = oKelurahans.GetData(.KDKELURAHAN).KDKELURAHAN
                        End If
                    End If

                End If

                If IsDBNull(.KECAMATAN) Then

                Else
                    If .KECAMATAN IsNot Nothing Then
                        Dim oKecamatans As New Reference.clsKecamatan
                        Dim dsKecamatan = oKecamatans.GetData(KdKecamatan)

                        If dsKecamatan IsNot Nothing Then
                            fn_LoadKecamatan(dsKecamatan.KDKABUPATEN)
                            KdKabupaten = dsKecamatan.KDKABUPATEN
                            grdKDKECAMATAN.Text = oKecamatans.GetData(KdKecamatan).KDKECAMATAN
                        End If
                    End If

                End If

                If IsDBNull(.KOTA) Then

                Else
                    If .KOTA IsNot Nothing Then
                        Dim oKotas As New Reference.clsKabupaten
                        Dim dsKota = oKotas.GetData(KdKabupaten)

                        If dsKota IsNot Nothing Then
                            fn_LoadKota(dsKota.KDPROPINSI)
                            KdPropinsi = dsKota.KDPROPINSI
                            grdKDKOTA.Text = oKotas.GetData(KdKabupaten).KDKABUPATEN
                        End If
                    End If

                End If

                If IsDBNull(.PROPINSI) Then

                Else
                    If .PROPINSI IsNot Nothing Then
                        Dim oPropinsis As New Reference.clsPropinsi
                        Dim dsProvinsi = oPropinsis.GetData(KdPropinsi)

                        If dsProvinsi IsNot Nothing Then
                            fn_LoadPropinsi()
                            grdKDPROPINSI.Text = oPropinsis.GetData(KdPropinsi).KDPROPINSI
                        End If
                    End If

                End If
                'txtKECAMATAN.Text = .KECAMATAN
                'txtKOTA.Text = .KOTA
                txtKODEPOS.Text = .KODEPOS
                txtNOMORTELEPON.Text = .NOTELEPON
                txtTEMPATLAHIR.Text = .TEMPATLAHIR
                deTANGGLLAHIR.DateTime = .TANGGALLAHIR
                cboKDJENISKELAMIN.SelectedIndex = .KDJENISKELAMIN
                cboSTATUSKAWIN.SelectedIndex = .STATUSKAWAIN
                grdKDAGAMA.Text = .KDAGAMA
                cboNEGARA.Text = .NEGARA
                grdKDSUKU.Text = .KDSUKU
                cboPENDIDIKAN.SelectedIndex = .PENDIDIKAN
                txtEMAIL.Text = .EMAIL
                txtNOTELEPON_WA.Text = .NOTELEPON_WA
                cboKDGOLONGANDARAH.SelectedIndex = .KDGOLONGANDARAH
                chkSTATUSHIDUP.Checked = .KDSTATUSHIDUP
                chkISACTIVE.Checked = .ISACTIVE
                txtALAMATPASIEN_SIMRS.Text = .ALAMATPASIEN_SIMRS
                txtNAMAPENANGGUNGJAWAB.Text = .NAMAPENANGUNGJAWAB
                grdKDHUBUNGAN.Text = .KDHUBUNGAN
                txtALAMATPENANGGUNGJAWAB.Text = .ALAMATNAMAPENANGUNGJAWAB
                txtNOMORTELEPONPENANGGUNGJAWAB.Text = .NOMORTELEPONPENANGGUNGJAWAB
            End With
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Function fn_Validate() As Boolean
        Try
            fn_Validate = True
            If txtKTP.Text = String.Empty Then
                txtKTP.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                txtKTP.ErrorText = Statement.ErrorRequired

                txtKTP.Focus()
                fn_Validate = False
                Exit Function
            End If
            If txtNAME_DISPLAY.Text = String.Empty Then
                txtNAME_DISPLAY.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                txtNAME_DISPLAY.ErrorText = Statement.ErrorRequired

                txtNAME_DISPLAY.Focus()
                fn_Validate = False
                Exit Function
            End If
            If grdKDAGAMA.Text = String.Empty Then
                grdKDAGAMA.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                grdKDAGAMA.ErrorText = Statement.ErrorRequired

                grdKDAGAMA.Focus()
                fn_Validate = False
                Exit Function
            End If
            If grdKDSUKU.Text = String.Empty Then
                grdKDSUKU.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                grdKDSUKU.ErrorText = Statement.ErrorRequired

                grdKDSUKU.Focus()
                fn_Validate = False
                Exit Function
            End If
            If cboKDJENISKELAMIN.Text = String.Empty Then
                cboKDJENISKELAMIN.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                cboKDJENISKELAMIN.ErrorText = Statement.ErrorRequired

                cboKDJENISKELAMIN.Focus()
                fn_Validate = False
                Exit Function
            End If
            If txtTEMPATLAHIR.Text = String.Empty Then
                txtTEMPATLAHIR.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                txtTEMPATLAHIR.ErrorText = Statement.ErrorRequired

                txtTEMPATLAHIR.Focus()
                fn_Validate = False
                Exit Function
            End If
            If cboNEGARA.Text = String.Empty Then
                cboNEGARA.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                cboNEGARA.ErrorText = Statement.ErrorRequired

                cboNEGARA.Focus()
                fn_Validate = False
                Exit Function
            End If
            If txtNOMORTELEPON.Text = String.Empty Then
                txtNOMORTELEPON.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                txtNOMORTELEPON.ErrorText = Statement.ErrorRequired

                txtNOMORTELEPON.Focus()
                fn_Validate = False
                Exit Function
            End If
            If grdKDDAFTAR_L1.Text = String.Empty Then
                grdKDDAFTAR_L1.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                grdKDDAFTAR_L1.ErrorText = Statement.ErrorRequired

                grdKDDAFTAR_L1.Focus()
                fn_Validate = False
                Exit Function
            End If
            If grdKDDAFTAR_L2.Text = String.Empty Then
                grdKDDAFTAR_L2.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                grdKDDAFTAR_L2.ErrorText = Statement.ErrorRequired

                grdKDDAFTAR_L2.Focus()
                fn_Validate = False
                Exit Function
            End If
            If grdKDDAFTAR_L3.Text = String.Empty Then
                grdKDDAFTAR_L3.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                grdKDDAFTAR_L3.ErrorText = Statement.ErrorRequired

                grdKDDAFTAR_L3.Focus()
                fn_Validate = False
                Exit Function
            End If
            If grdKDDAFTAR_L4.Text = String.Empty Then
                grdKDDAFTAR_L4.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                grdKDDAFTAR_L4.ErrorText = Statement.ErrorRequired

                grdKDDAFTAR_L4.Focus()
                fn_Validate = False
                Exit Function
            End If
            If grdKDDAFTAR_L5.Text = String.Empty Then
                grdKDDAFTAR_L5.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                grdKDDAFTAR_L5.ErrorText = Statement.ErrorRequired

                grdKDDAFTAR_L5.Focus()
                fn_Validate = False
                Exit Function
            End If
            If grdKDDAFTAR_L6.Text = String.Empty Then
                grdKDDAFTAR_L6.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                grdKDDAFTAR_L6.ErrorText = Statement.ErrorRequired

                grdKDDAFTAR_L6.Focus()
                fn_Validate = False
                Exit Function
            End If
            If grdKDKELURAHAN.Text = String.Empty Then
                grdKDKELURAHAN.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                grdKDKELURAHAN.ErrorText = Statement.ErrorRequired

                grdKDKELURAHAN.Focus()
                fn_Validate = False
                Exit Function
            End If
            If grdKDHUBUNGAN.Text = String.Empty Then
                grdKDHUBUNGAN.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                grdKDHUBUNGAN.ErrorText = Statement.ErrorRequired

                grdKDHUBUNGAN.Focus()
                fn_Validate = False
                Exit Function
            End If
            If txtKTP.Text <> "-" Then
                If oFormMode = FORM_MODE.FORM_MODE_ADD Then
                    If oCustomer.IsExist(txtKTP.Text.ToUpper.Trim) = True Then
                        txtKTP.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                        txtKTP.ErrorText = Statement.ErrorRegistered

                        txtKTP.Focus()
                        fn_Validate = False
                        Exit Function
                    End If
                Else
                    If txtKTP.Text.Trim.ToUpper <> oCustomer.GetData(sNoId).KTP Then
                        If oCustomer.IsExist(txtKTP.Text.ToUpper.Trim) = True Then
                            txtKTP.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                            txtKTP.ErrorText = Statement.ErrorRegistered

                            txtKTP.Focus()
                            fn_Validate = False
                            Exit Function
                        End If
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
            Dim ds = oCustomer.GetStructureHeader
            With ds
                Try
                    .DATECREATED = oCustomer.GetData(sNoId).DATECREATED
                Catch oErr As Exception
                    .DATECREATED = Now
                End Try
                .DATEUPDATED = Now

                .KDCUSTOMER = IIf(txtKDCUSTOMER.Text = "<---AUTO--->", sNoId, txtKDCUSTOMER.Text.ToString.Trim.ToUpper)
                .KDDAFTAR_L1 = grdKDDAFTAR_L1.EditValue
                .KDDAFTAR_L2 = grdKDDAFTAR_L2.EditValue
                .KDDAFTAR_L3 = grdKDDAFTAR_L3.EditValue
                .KDDAFTAR_L4 = grdKDDAFTAR_L4.EditValue
                .KDDAFTAR_L5 = grdKDDAFTAR_L5.EditValue
                .KDDAFTAR_L6 = grdKDDAFTAR_L6.EditValue
                .KTP = txtKTP.Text.ToString.Trim.ToUpper
                .KARTUBPJS = txtKARTUBPJS.Text.ToString.Trim.ToUpper
                .NAME_DISPLAY = txtNAME_DISPLAY.Text.ToString.Trim.ToUpper
                .JALAN = txtJALAN.Text.ToString.Trim.ToUpper
                .PROPINSI = grdKDPROPINSI.Text.ToString.Trim.ToUpper
                .KDKELURAHAN = grdKDKELURAHAN.EditValue
                .KELURAHAN = grdKDKELURAHAN.Text
                .KECAMATAN = grdKDKECAMATAN.Text.ToString.Trim.ToUpper
                .KOTA = grdKDKOTA.Text.ToString.Trim.ToUpper
                .KODEPOS = txtKODEPOS.Text.ToString.Trim.ToUpper
                .NOTELEPON = txtNOMORTELEPON.Text.ToString.Trim.ToUpper
                .TEMPATLAHIR = txtTEMPATLAHIR.Text.ToString.Trim.ToUpper
                .TANGGALLAHIR = deTANGGLLAHIR.DateTime
                .KDJENISKELAMIN = cboKDJENISKELAMIN.SelectedIndex
                .STATUSKAWAIN = cboSTATUSKAWIN.SelectedIndex
                .KDAGAMA = grdKDAGAMA.EditValue
                .LAIN_LAIN = ""
                .NEGARA = cboNEGARA.Text.ToString.Trim.ToUpper
                .KDSUKU = grdKDSUKU.EditValue
                .PENDIDIKAN = cboPENDIDIKAN.SelectedIndex
                .EMAIL = txtEMAIL.Text
                .NOTELEPON_WA = txtNOTELEPON_WA.Text
                .KDGOLONGANDARAH = cboKDGOLONGANDARAH.SelectedIndex
                .KDSTATUSHIDUP = chkSTATUSHIDUP.Checked
                .ISACTIVE = chkISACTIVE.Checked
                .KDUSER = sUserID
                .ALAMATPASIEN_SIMRS = txtALAMATPASIEN_SIMRS.Text
                .NAMAPENANGUNGJAWAB = txtNAMAPENANGGUNGJAWAB.Text
                .KDHUBUNGAN = grdKDHUBUNGAN.Text
                .ALAMATNAMAPENANGUNGJAWAB = txtALAMATPENANGGUNGJAWAB.Text
                .NOMORTELEPONPENANGGUNGJAWAB = txtNOMORTELEPONPENANGGUNGJAWAB.Text
            End With

            If oFormMode = FORM_MODE.FORM_MODE_ADD Then
                Try
                    txtKDCUSTOMER.Text = oCustomer.InsertData(ds)

                    If txtKDCUSTOMER.Text = "" Then
                        fn_Save = False
                    Else
                        fn_Save = True
                    End If

                Catch oErr As Exception
                    MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            ElseIf oFormMode = FORM_MODE.FORM_MODE_EDIT Then
                Try
                    fn_Save = oCustomer.UpdateData(ds)
                Catch oErr As Exception
                    MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            End If
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            fn_Save = False
        End Try
    End Function
    Private Function InsertJenisPeserta(ByVal sKDJENISPESERTA As String, ByVal sMEMO As String) As Boolean
        Try
            Dim oJenisPeserta As New Reference.clsDaftar_L1

            InsertJenisPeserta = True

            If oJenisPeserta.IsExistKode(sKDJENISPESERTA) Then
                grdKDDAFTAR_L1.EditValue = sKDJENISPESERTA
            Else
                Dim ds = oJenisPeserta.GetStructureHeader
                With ds
                    .DATECREATED = Now
                    .DATEUPDATED = Now
                    .KDDAFTAR_L1 = sKDJENISPESERTA
                    .MEMO = sMEMO
                    .ISDEFAULT = IIf(oJenisPeserta.CheckDefault(0, False) = False, True, False)
                    .ISACTIVE = True
                End With

                InsertJenisPeserta = oJenisPeserta.InsertData(ds)

                fn_LoadDaftar1()
                grdKDDAFTAR_L1.EditValue = sKDJENISPESERTA
            End If

        Catch ex As Exception
            InsertJenisPeserta = False
            MsgBox(ex.ToString)
        End Try
    End Function
#End Region
#Region "Grid Method"

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
    Private Sub btnAddSuku_Click(sender As Object, e As EventArgs) Handles btnAddSuku.Click
        frmSuku.LoadMe(FORM_MODE.FORM_MODE_ADD)
        frmSuku.ShowDialog(Me)
        fn_LoadKDSUKU()

        Dim oSuku As New Reference.clsSuku

        If sCode = String.Empty Then Exit Sub

        grdKDSUKU.Text = oSuku.GetData.FirstOrDefault(Function(x) x.MEMO = sCode).KDSUKU

    End Sub
    Private Sub btnAgama_Click(sender As Object, e As EventArgs)
        frmAgama.LoadMe(FORM_MODE.FORM_MODE_ADD)
        frmAgama.ShowDialog(Me)
        fn_LoadKDAGAMA()

        Dim oAgama As New Reference.clsAgama

        If sCode = String.Empty Then Exit Sub

        grdKDAGAMA.Text = oAgama.GetData.FirstOrDefault(Function(x) x.MEMO = sCode).KDAGAMA

    End Sub
#End Region
#Region "Lookup / Event"
    Private Sub fn_LoadStatusKawin()
        cboSTATUSKAWIN.Properties.Items.Add("LAJANG")
        cboSTATUSKAWIN.Properties.Items.Add("MENIKAH")
        cboSTATUSKAWIN.Properties.Items.Add("DUDA")
        cboSTATUSKAWIN.Properties.Items.Add("JANDA")
        cboSTATUSKAWIN.Properties.Items.Add("-")
    End Sub
    Private Sub fn_LoadPendidikan()
        cboPENDIDIKAN.Properties.Items.Add("-")
        cboPENDIDIKAN.Properties.Items.Add("TIDAK SEKOLAH")
        cboPENDIDIKAN.Properties.Items.Add("SD")
        cboPENDIDIKAN.Properties.Items.Add("SLTP")
        cboPENDIDIKAN.Properties.Items.Add("SLTA")
        cboPENDIDIKAN.Properties.Items.Add("DIPLOMA")
        cboPENDIDIKAN.Properties.Items.Add("SARJANA")
        cboPENDIDIKAN.Properties.Items.Add("MAGISTER")
        cboPENDIDIKAN.Properties.Items.Add("DOKTOR")
    End Sub
    Private Sub fn_LoadPropinsi()
        Dim oPropinsi As New Reference.clsPropinsi
        Try
            grdKDPROPINSI.Properties.DataSource = oPropinsi.GetData.Where(Function(x) x.ISACTIVE = True).ToList()
            grdKDPROPINSI.Properties.ValueMember = "KDPROPINSI"
            grdKDPROPINSI.Properties.DisplayMember = "MEMO"

        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadKota(ByVal KDPROPINSI As String)
        Dim oKota As New Reference.clsKabupaten
        Try
            grdKDKOTA.Properties.DataSource = oKota.GetDataPropinsi(KDPROPINSI).ToList()
            grdKDKOTA.Properties.ValueMember = "KDKABUPATEN"
            grdKDKOTA.Properties.DisplayMember = "MEMO"

        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadKecamatan(ByVal KDKABUPATEN As String)
        Dim oKecamatan As New Reference.clsKecamatan
        Try
            grdKDKECAMATAN.Properties.DataSource = oKecamatan.GetDataPropinsiKabupaten(KDKABUPATEN).ToList()
            grdKDKECAMATAN.Properties.ValueMember = "KDKECAMATAN"
            grdKDKECAMATAN.Properties.DisplayMember = "MEMO"

        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadKelurahan(ByVal sKDKECAMATAN As String)
        Dim oKelurahan As New Reference.clsKelurahan
        Try
            'Dim dsKelurahan = From x In oKelurahan.GetData.Where(Function(x) x.ISACTIVE = True)
            '                  Select x.KDKELURAHAN, x.MEMO, PROPINSI = x.M_KECAMATAN.M_KABUPATEN.M_PROPINSI.MEMO, KABUPATEN = x.M_KECAMATAN.M_KABUPATEN.MEMO, KECAMATAN = x.M_KECAMATAN.MEMO

            'grdKDKELURAHAN.Properties.DataSource = dsKelurahan.ToList()
            'grdKDKELURAHAN.Properties.ValueMember = "KDKELURAHAN"
            'grdKDKELURAHAN.Properties.DisplayMember = "MEMO"

            grdKDKELURAHAN.Properties.DataSource = oKelurahan.GetDataPropinsiKabupatenKecamatan(sKDKECAMATAN).ToList()
            grdKDKELURAHAN.Properties.ValueMember = "KDKELURAHAN"
            grdKDKELURAHAN.Properties.DisplayMember = "MEMO"
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadDaftar1()
        Dim oDAFTAR_L1 As New Reference.clsDaftar_L1
        Try
            grdKDDAFTAR_L1.Properties.DataSource = oDAFTAR_L1.GetData.Where(Function(x) x.ISACTIVE = True).ToList()
            grdKDDAFTAR_L1.Properties.ValueMember = "KDDAFTAR_L1"
            grdKDDAFTAR_L1.Properties.DisplayMember = "MEMO"

        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadDaftar2()
        Dim oDAFTAR_L2 As New Reference.clsDaftar_L2
        Try
            grdKDDAFTAR_L2.Properties.DataSource = oDAFTAR_L2.GetData.Where(Function(x) x.ISACTIVE = True).ToList()
            grdKDDAFTAR_L2.Properties.ValueMember = "KDDAFTAR_L2"
            grdKDDAFTAR_L2.Properties.DisplayMember = "MEMO"

        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadDaftar3()
        Dim oDAFTAR_L3 As New Reference.clsDaftar_L3
        Try
            grdKDDAFTAR_L3.Properties.DataSource = oDAFTAR_L3.GetData.Where(Function(x) x.ISACTIVE = True).ToList()
            grdKDDAFTAR_L3.Properties.ValueMember = "KDDAFTAR_L3"
            grdKDDAFTAR_L3.Properties.DisplayMember = "MEMO"

        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadDaftar4()
        Dim oDAFTAR_L4 As New Reference.clsDaftar_L4
        Try
            grdKDDAFTAR_L4.Properties.DataSource = oDAFTAR_L4.GetData.Where(Function(x) x.ISACTIVE = True).ToList()
            grdKDDAFTAR_L4.Properties.ValueMember = "KDDAFTAR_L4"
            grdKDDAFTAR_L4.Properties.DisplayMember = "MEMO"

        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadDaftar5()
        Dim oDAFTAR_L5 As New Reference.clsDaftar_L5
        Try
            grdKDDAFTAR_L5.Properties.DataSource = oDAFTAR_L5.GetData.Where(Function(x) x.ISACTIVE = True).ToList()
            grdKDDAFTAR_L5.Properties.ValueMember = "KDDAFTAR_L5"
            grdKDDAFTAR_L5.Properties.DisplayMember = "MEMO"

        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadDaftar6()
        Dim oDAFTAR_L6 As New Reference.clsDaftar_L6
        Try
            grdKDDAFTAR_L6.Properties.DataSource = oDAFTAR_L6.GetData.Where(Function(x) x.ISACTIVE = True).ToList()
            grdKDDAFTAR_L6.Properties.ValueMember = "KDDAFTAR_L6"
            grdKDDAFTAR_L6.Properties.DisplayMember = "MEMO"

        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadKDAGAMA()
        Dim oAgama As New Reference.clsAgama
        Try
            grdKDAGAMA.Properties.DataSource = oAgama.GetData.Where(Function(x) x.ISACTIVE = True).ToList()
            grdKDAGAMA.Properties.ValueMember = "KDAGAMA"
            grdKDAGAMA.Properties.DisplayMember = "MEMO"
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub grdKDAGAMA_KeyDown(sender As System.Object, e As System.Windows.Forms.KeyEventArgs) Handles grdKDAGAMA.KeyDown
        If e.KeyCode = Keys.Delete Then
            grdKDAGAMA.ResetText()
        End If
    End Sub
    Private Sub fn_LoadKDSUKU()
        Dim oSuku As New Reference.clsSuku
        Try
            grdKDSUKU.Properties.DataSource = oSuku.GetData.Where(Function(x) x.ISACTIVE = True).ToList()
            grdKDSUKU.Properties.ValueMember = "KDSUKU"
            grdKDSUKU.Properties.DisplayMember = "MEMO"
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub grdKDSUKU_KeyDown(sender As System.Object, e As System.Windows.Forms.KeyEventArgs) Handles grdKDSUKU.KeyDown
        If e.KeyCode = Keys.Delete Then
            grdKDSUKU.ResetText()
        End If
    End Sub
    Private Sub fn_LoadKDHUBUNGAN()
        Dim oHubungan As New Reference.clsHubungan
        Try
            grdKDHUBUNGAN.Properties.DataSource = oHubungan.GetData.Where(Function(x) x.ISACTIVE = True).ToList()
            grdKDHUBUNGAN.Properties.ValueMember = "KDHUBUNGAN"
            grdKDHUBUNGAN.Properties.DisplayMember = "MEMO"
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub txtKARTUBPJS_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtKARTUBPJS.KeyPress
        If Asc(e.KeyChar) = 13 Then
            If txtKARTUBPJS.Text = String.Empty Then
                Exit Sub
            End If

            If txtKARTUBPJS.Text.Count = 13 Then
                Try
                    Dim oSetKoneksi As New Brigging.clsSetKoneksi
                    Dim dsDataSetKoneksi = oSetKoneksi.GetData().FirstOrDefault(Function(x) x.ISACTIVE = True And x.NAME_DISPLAY = "VCLAIM")
                    Dim uTime As Integer = 0

                    If dsDataSetKoneksi IsNot Nothing Then
                        uTime = (DateTime.UtcNow - New DateTime(1970, 1, 1, 0, 0, 0)).TotalSeconds
                        Dim dsSetKoneksi = oSetKoneksi.GetDataVClaimPesertaNoKartuBPJS(dsDataSetKoneksi.ALAMATWEB, dsDataSetKoneksi.CONSID, dsDataSetKoneksi.SECREATKEY, dsDataSetKoneksi.USER_KEY, uTime, txtKARTUBPJS.Text, Now.ToString("yyyy-MM-dd"))

                        If dsSetKoneksi <> "" Then
                            Try
                                Dim allData = JObject.Parse(dsSetKoneksi)

                                Dim DataDecrypt = JObject.Parse(oSetKoneksi.Decrypt(allData("response"), dsDataSetKoneksi.CONSID & dsDataSetKoneksi.SECREATKEY & uTime))

                                MsgBox("Jenis Peserta : " & DataDecrypt("peserta")("jenisPeserta")("keterangan").ToString() _
                                                  & vbCrLf _
                                                  & "No RM : " & DataDecrypt("peserta")("mr")("noMR").ToString() _
                                                  & vbCrLf _
                                                  & "No Telepon : " & DataDecrypt("peserta")("mr")("noTelepon").ToString() _
                                                  & vbCrLf _
                                                  & "Nama : " & DataDecrypt("peserta")("nama").ToString() _
                                                  & vbCrLf _
                                                  & "Tgl Cetak Kartu : " & DataDecrypt("peserta")("tglCetakKartu").ToString() _
                                                  & vbCrLf _
                                                  & "Umur Sekarang : " & DataDecrypt("peserta")("umur")("umurSekarang").ToString() _
                                                  & vbCrLf _
                                                  & "Tgl TAT : " & DataDecrypt("peserta")("tglTAT").ToString() _
                                                  & vbCrLf _
                                                  & "Tgl TMT : " & DataDecrypt("peserta")("tglTMT").ToString() _
                                                  & vbCrLf _
                                                  & "Status Peserta : " & DataDecrypt("peserta")("statusPeserta")("keterangan").ToString() _
                                                  & vbCrLf _
                                                  & vbCrLf _
                                                  & "Jenis Pasien : " & IIf(DataDecrypt("peserta")("mr")("noMR").ToString.Trim.ToUpper() = "", "PASIEN BARU", "PASIEN LAMA") _
                                                  , MsgBoxStyle.Information, Me.Text)

                                If DataDecrypt("peserta")("mr")("noMR").ToString.Trim.ToUpper() <> "" Then
                                    txtKDCUSTOMER.Text = DataDecrypt("peserta")("mr")("noMR").ToString.Trim.ToUpper()
                                End If


                                Dim jenisPeserta As String = DataDecrypt("peserta")("jenisPeserta")("keterangan").ToString()
                                Dim kodejenisPeserta As String = DataDecrypt("peserta")("jenisPeserta")("kode").ToString()

                                If jenisPeserta <> "" Then
                                    InsertJenisPeserta(kodejenisPeserta, jenisPeserta)
                                End If

                                txtNAME_DISPLAY.Text = DataDecrypt("peserta")("nama").ToString.Trim.ToUpper()
                                txtNOMORTELEPON.Text = DataDecrypt("peserta")("mr")("noTelepon").ToString.Trim.ToUpper()
                                txtKTP.Text = DataDecrypt("peserta")("nik").ToString()
                                deTANGGLLAHIR.DateTime = DataDecrypt("peserta")("tglLahir").ToString()
                                cboKDJENISKELAMIN.SelectedIndex = IIf(DataDecrypt("peserta")("sex").ToString() = "L", 0, 1)

                            Catch oErr As Exception
                                MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message & vbCrLf & vbCrLf & "Result : " & vbCrLf & dsSetKoneksi, MsgBoxStyle.Exclamation, Me.Text)
                            End Try
                        End If
                    Else
                        MsgBox("Koneksi Tidak ditemukan", MsgBoxStyle.Exclamation, Me.Text)
                    End If

                    'txtVCLAIM_KDDOCTOR.ResetText()

                Catch oErr As Exception
                    MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            ElseIf txtKARTUBPJS.Text.Count > 13 Then
                MsgBox(Statement.ErrorStatement & "No Kartu BPJS > 13 Digit", MsgBoxStyle.Exclamation, Me.Text)
            Else
                MsgBox(Statement.ErrorStatement & "Kartu BPJS < 13 Digit", MsgBoxStyle.Exclamation, Me.Text)
            End If
        End If
    End Sub
    Private Sub txtKTP_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtKTP.KeyPress
        If Asc(e.KeyChar) = 13 Then
            If txtKTP.Text = String.Empty Then
                Exit Sub
            End If

            If txtKTP.Text.Count = 16 Then
                Try
                    Dim oSetKoneksi As New Brigging.clsSetKoneksi
                    Dim dsDataSetKoneksi = oSetKoneksi.GetData().FirstOrDefault(Function(x) x.ISACTIVE = True And x.NAME_DISPLAY = "VCLAIM")
                    Dim uTime As Integer = 0

                    If dsDataSetKoneksi IsNot Nothing Then
                        uTime = (DateTime.UtcNow - New DateTime(1970, 1, 1, 0, 0, 0)).TotalSeconds
                        Dim dsSetKoneksi = oSetKoneksi.GetDataVClaimPesertaNIK(dsDataSetKoneksi.ALAMATWEB, dsDataSetKoneksi.CONSID, dsDataSetKoneksi.SECREATKEY, dsDataSetKoneksi.USER_KEY, uTime, txtKTP.Text, Now.ToString("yyyy-MM-dd"))

                        If dsSetKoneksi <> "" Then
                            Try
                                Dim allData = JObject.Parse(dsSetKoneksi)

                                Dim DataDecrypt = JObject.Parse(oSetKoneksi.Decrypt(allData("response"), dsDataSetKoneksi.CONSID & dsDataSetKoneksi.SECREATKEY & uTime))

                                MsgBox("Jenis Peserta : " & DataDecrypt("peserta")("jenisPeserta")("keterangan").ToString() _
                                                  & vbCrLf _
                                                  & "No RM : " & DataDecrypt("peserta")("mr")("noMR").ToString() _
                                                  & vbCrLf _
                                                  & "No Telepon : " & DataDecrypt("peserta")("mr")("noTelepon").ToString() _
                                                  & vbCrLf _
                                                  & "Nama : " & DataDecrypt("peserta")("nama").ToString() _
                                                  & vbCrLf _
                                                  & "Tgl Cetak Kartu : " & DataDecrypt("peserta")("tglCetakKartu").ToString() _
                                                  & vbCrLf _
                                                  & "Umur Sekarang : " & DataDecrypt("peserta")("umur")("umurSekarang").ToString() _
                                                  & vbCrLf _
                                                  & "Tgl TAT : " & DataDecrypt("peserta")("tglTAT").ToString() _
                                                  & vbCrLf _
                                                  & "Tgl TMT : " & DataDecrypt("peserta")("tglTMT").ToString() _
                                                  & vbCrLf _
                                                  & "Status Peserta : " & DataDecrypt("peserta")("statusPeserta")("keterangan").ToString() _
                                                  & vbCrLf _
                                                  & vbCrLf _
                                                  & "Jenis Pasien : " & IIf(DataDecrypt("peserta")("mr")("noMR").ToString.Trim.ToUpper() = "", "PASIEN BARU", "PASIEN LAMA") _
                                                  , MsgBoxStyle.Information, Me.Text)

                                If DataDecrypt("peserta")("mr")("noMR").ToString.Trim.ToUpper() <> "" Then
                                    txtKDCUSTOMER.Text = DataDecrypt("peserta")("mr")("noMR").ToString.Trim.ToUpper()
                                End If

                                Dim jenisPeserta As String = DataDecrypt("peserta")("jenisPeserta")("keterangan").ToString()
                                Dim kodejenisPeserta As String = DataDecrypt("peserta")("jenisPeserta")("kode").ToString()

                                If jenisPeserta <> "" Then
                                    InsertJenisPeserta(kodejenisPeserta, jenisPeserta)
                                End If

                                txtNAME_DISPLAY.Text = DataDecrypt("peserta")("nama").ToString.Trim.ToUpper()
                                txtNOMORTELEPON.Text = DataDecrypt("peserta")("mr")("noTelepon").ToString.Trim.ToUpper()
                                txtKARTUBPJS.Text = DataDecrypt("peserta")("noKartu").ToString()
                                deTANGGLLAHIR.DateTime = DataDecrypt("peserta")("tglLahir").ToString()
                                cboKDJENISKELAMIN.SelectedIndex = IIf(DataDecrypt("peserta")("sex").ToString() = "L", 0, 1)

                            Catch oErr As Exception
                                MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message & vbCrLf & vbCrLf & "Result : " & vbCrLf & dsSetKoneksi, MsgBoxStyle.Exclamation, Me.Text)
                            End Try
                        End If
                    Else
                        MsgBox("Koneksi Tidak ditemukan", MsgBoxStyle.Exclamation, Me.Text)
                    End If

                    'txtVCLAIM_KDDOCTOR.ResetText()

                Catch oErr As Exception
                    MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            ElseIf txtKARTUBPJS.Text.Count > 16 Then
                MsgBox(Statement.ErrorStatement & "No NIK > 16 Digit", MsgBoxStyle.Exclamation, Me.Text)
            Else
                MsgBox(Statement.ErrorStatement & "No NIK < 16 Digit", MsgBoxStyle.Exclamation, Me.Text)
            End If
        End If
    End Sub
    'Private Sub btnKelurahan_Click(sender As Object, e As EventArgs)
    '    Dim frmKelurahan As New frmKelurahan
    '    Try
    '        frmKelurahan.LoadMe(FORM_MODE.FORM_MODE_ADD)
    '        frmKelurahan.ShowDialog(Me)

    '        fn_LoadKelurahan()
    '        grdKDKELURAHAN.Text = sCode
    '    Catch oErr As Exception
    '        MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
    '    End Try
    'End Sub
    Private Sub txtKDCUSTOMER_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtKDCUSTOMER.KeyPress
        If Asc(e.KeyChar) = 13 Then
            If oFormMode = FORM_MODE.FORM_MODE_ADD Then
                Dim dsCustomer = oCustomer.GetData(txtKDCUSTOMER.Text.ToString)
                If dsCustomer IsNot Nothing Then
                    MsgBox("Pasien Sudah ada di database", MsgBoxStyle.Exclamation, Me.Text)
                    Me.Close()
                End If
            End If
        End If
    End Sub
    Private Sub grdKDKELURAHAN_KeyPress(sender As Object, e As KeyPressEventArgs) Handles grdKDKELURAHAN.KeyPress
        If Asc(e.KeyChar) = 13 Then
            Dim oKelurahan As New Reference.clsKelurahan
            Dim dsKelurahan = oKelurahan.GetData(grdKDKELURAHAN.EditValue)
            If dsKelurahan IsNot Nothing Then
                'grdKDKELURAHAN.EditValue = dsKelurahan.KDKELURAHAN
                'txtPROPINSI.Text = dsKelurahan.M_KECAMATAN.M_KABUPATEN.M_PROPINSI.MEMO
                'txtKOTA.Text = dsKelurahan.M_KECAMATAN.M_KABUPATEN.MEMO
                'txtKECAMATAN.Text = dsKelurahan.M_KECAMATAN.MEMO
                txtKODEPOS.Text = dsKelurahan.KODEPOS
                'If txtALAMATPASIEN_SIMRS.Text = "" Then
                '    txtALAMATPASIEN_SIMRS.Text = txtJALAN.Text.ToString.Trim.ToUpper & " " & txtPROPINSI.Text & " " & txtKOTA.Text & " " & txtKECAMATAN.Text
                'End If
            End If
        End If
    End Sub
    Private Sub grdKDPROPINSI_EditValueChanged(sender As Object, e As EventArgs) Handles grdKDPROPINSI.EditValueChanged
        If isLoad = True Then
            fn_LoadKota(grdKDPROPINSI.EditValue)
        End If
    End Sub
    Private Sub grdKDKOTA_EditValueChanged(sender As Object, e As EventArgs) Handles grdKDKOTA.EditValueChanged
        If isLoad = True Then
            fn_LoadKecamatan(grdKDKOTA.EditValue)
        End If
    End Sub
    Private Sub grdKDKECAMATAN_EditValueChanged(sender As Object, e As EventArgs) Handles grdKDKECAMATAN.EditValueChanged
        If isLoad = True Then
            fn_LoadKelurahan(grdKDKECAMATAN.EditValue)
            txtALAMATPASIEN_SIMRS.Text = txtJALAN.Text.ToString.Trim.ToUpper & " " & grdKDPROPINSI.Text & " " & grdKDKOTA.Text & " " & grdKDKECAMATAN.Text
        End If
    End Sub
    Private Sub grdKDKELURAHAN_EditValueChanged(sender As Object, e As EventArgs) Handles grdKDKELURAHAN.EditValueChanged
        If isLoad = True Then
            Dim oKelurahan As New Reference.clsKelurahan
            Dim dsKelurahan = oKelurahan.GetData(grdKDKELURAHAN.EditValue)
            If dsKelurahan IsNot Nothing Then
                txtKODEPOS.Text = dsKelurahan.KODEPOS
            End If
        End If
    End Sub
#End Region
End Class