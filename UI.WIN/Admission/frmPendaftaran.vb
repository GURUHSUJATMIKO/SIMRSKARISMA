Imports DataAccess
Imports UI.WIN.MAIN.My.Resources
Imports System.Linq
Imports System.Data.SqlClient
Imports Newtonsoft.Json.Linq

Public Class frmPendaftaran
#Region "Declaration"
    Private oFormMode As FORM_MODE = FORM_MODE.FORM_MODE_VIEW
    Private sNoId As String
    Private isLoad As Boolean = False
    Private oPendaftaran As New Admission.clsPendaftaran
    Private PPKPELAYANAN As String = String.Empty
    Private oSetKoneksi As New Brigging.clsSetKoneksi

#End Region
#Region "Function"
    Public Sub LoadMe(ByVal FormMode As Integer, Optional ByVal NoId As String = "")
        oFormMode = FormMode
        sNoId = NoId
        Dim dsKoneksiBriging = oPendaftaran.GetDataKoneksi("VCLAIM")
        If dsKoneksiBriging IsNot Nothing Then
            PPKPELAYANAN = dsKoneksiBriging.PPKPELAYANAN
        End If

    End Sub
    Private Sub Form_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        lFLAGPROCEDURE.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        lKDKUNJUNGAN.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never

        fn_ChangeFormState()
        fn_LoadLanguage()
        isLoad = True
    End Sub
    Public Sub fn_LoadLanguage()
        Try
            Me.Text = Pendaftaran.TITLE

            lNAMAPASIEN.Text = Pendaftaran.KDCUSTOMER_NAMAPASIEN
            lJENISKELAMIN.Text = Customer.KDJENISKELAMIN
            lPERUSAHAAN.Text = Pendaftaran.KDPERUSAHAAN
            lKDPENDAFATRAN.Text = Pendaftaran.KDPENDAFTARAN & " *"
            lKDPENDAFTARAN_AWAL.Text = Pendaftaran.KDPENDAFTARAN_AWAL & " *"
            lNOMORSEP.Text = Pendaftaran.NOMORSEP
            lKDPENJAMIN.Text = Pendaftaran.KDPENJAMIN & " *"
            lDAFTAR_L1.Text = sDaftar_L1
            lDAFTAR_L2.Text = sDaftar_L2
            lDAFTAR_L3.Text = sDaftar_L3
            'lDAFTAR_L4.Text = sDaftar_L4
            'lDAFTAR_L5.Text = sDaftar_L5
            lDAFTAR_L6.Text = sDaftar_L6
            lKARTUBPJS.Text = Pendaftaran.KARTUBPJS
            lDATE.Text = Pendaftaran.TANGGAL & " *"
            lCATEGORY.Text = Pendaftaran.CATEGORY & " *"
            lKDKELASRAWAT.Text = Pendaftaran.KDKELASRAWAT & " *"
            lKDCUSTOMER.Text = Pendaftaran.KDCUSTOMER & " *"
            lASALRUJUKAN.Text = Pendaftaran.ASALRUJUKAN & " *"
            lDATE_RUJUKAN.Text = Pendaftaran.DATE_RUJUKAN & " *"
            lNOMORRUJUKAN.Text = Pendaftaran.NOMORRUJUKAN & " *"
            lKDPPK.Text = Pendaftaran.KDPPK & " *"
            lCATATAN.Text = Pendaftaran.CATATAN & " *"
            lKDDIAGNOSA.Text = Pendaftaran.KDDIAGNOSA & " *"
            lKDDEPARTMENT.Text = Pendaftaran.KDDEPARTMENT & " *"
            chkLakaLantas.Text = Pendaftaran.JAMINAN_ISLAKLANTAS
            chkPenjamin1.Text = Pendaftaran.JAMINAN_PENJAMIN_PENJAMIN1
            chkPenjamin2.Text = Pendaftaran.JAMINAN_PENJAMIN_PENJAMIN2
            chkPenjamin3.Text = Pendaftaran.JAMINAN_PENJAMIN_PENJAMIN3
            chkPenjamin4.Text = Pendaftaran.JAMINAN_PENJAMIN_PENJAMIN4
            lTAB3_TGLKEJADIAN.Text = Pendaftaran.JAMINAN_PENJAMIN_TGLKEJADIAN
            lTAB3_KETERANGAN.Text = Pendaftaran.JAMINAN_PENJAMIN_KETERANGAN
            chkISSUPLESI.Text = Pendaftaran.JAMINAN_PENJAMIN_SUPLESI_ISSUPLESI
            lTAB3_NOSEPSUPLESI.Text = Pendaftaran.JAMINAN_PENJAMIN_SUPLESI_NOSEPSUPLESI
            lTAB3_PROPINSI.Text = Pendaftaran.JAMINAN_PENJAMIN_SUPLESI_LOKASILAKA_KDPROPINSI
            lTAB3_KABUPATEN.Text = Pendaftaran.JAMINAN_PENJAMIN_SUPLESI_LOKASILAKA_KABUPATEN
            lTAB3_KECAMATAN.Text = Pendaftaran.JAMINAN_PENJAMIN_SUPLESI_LOKASILAKA_KECAMATAN
            lNOMORSKDP.Text = Pendaftaran.NOMORSKDP
            lKDDOCTOR.Text = Pendaftaran.KDDOCTOR & " *"
            lNOMORTELEPON.Text = Pendaftaran.NOMORTELEPON & " *"

            lKDKELASRAWAT_NAIKKELAS.Text = Pendaftaran.KDKELASRAWAT_NAIKKELAS
            lPEMBIAYAAN.Text = Pendaftaran.PEMBIAYAAN
            lPENANGGUNGJAWAB.Text = Pendaftaran.PENANGGUNGJAWAB
            lTUJUANKUNJUNGAN.Text = Pendaftaran.TUJUANKUNJUNGAN
            lFLAGPROCEDURE.Text = Pendaftaran.FLAGPROCEDURE
            lKDKUNJUNGAN.Text = Pendaftaran.KDPENUNJANG
            lASSEMENTPEL.Text = Pendaftaran.ASESMENTPEL
            lKDDOCTOR_PELAYANAN.Text = Pendaftaran.KDDOCTOR_PELAYANAN

            btnSaveNew.Caption = Caption.FormSaveNew
            btnSaveClose.Caption = Caption.FormSaveClose
            btnClose.Caption = Caption.FormClose

            tab1.Text = Pendaftaran.TAB_1
            tab2.Text = Pendaftaran.TAB_2
            tab3.Text = Pendaftaran.TAB_3

        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub Form_Closing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        sCode = txtKDPENDAFTARAN.Text.Trim.ToUpper
    End Sub
    Private Sub fn_ChangeFormState()
        fn_LoadPerusahaan()
        fn_LoadDepartment()
        fn_LoadHubungan()
        fn_LoadRuangan()
        fn_LoadPenjamin()
        fn_LoadDaftar1()
        fn_LoadDaftar2()
        fn_LoadDaftar3()
        'fn_LoadDaftar4()
        'fn_LoadDaftar5()
        fn_LoadDaftar6()
        fn_LoadDiganosa()
        fn_LoadFaskes()
        fn_LoadkelasRawat()

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
        btnAddCustomer.Enabled = Not Status
        'btnCariRI.Enabled = Not Status
        btnCreateSEP.Enabled = Not Status
        btnSuratKontrol.Enabled = Not Status
        btnCariPasien.Enabled = Not Status

        txtKDPENDAFTARAN_AWAL.Properties.ReadOnly = True
        chkLama.Properties.ReadOnly = Status
        txtNOMORSEP.Properties.ReadOnly = Status
        grdKDPENJAMIN.Properties.ReadOnly = Status
        grdKDPERUSAHAAN.Properties.ReadOnly = Status
        grdKDDAFTAR_L1.Properties.ReadOnly = Status
        grdKDDAFTAR_L2.Properties.ReadOnly = Status
        grdKDDAFTAR_L3.Properties.ReadOnly = Status
        'grdKDDAFTAR_L4.Properties.ReadOnly = Status
        'grdKDDAFTAR_L5.Properties.ReadOnly = Status
        grdKDDAFTAR_L6.Properties.ReadOnly = Status
        'txtNAMAKELUARGA.Properties.ReadOnly = Status
        txtKARTUBPJS.Properties.ReadOnly = Status
        deDATE.Properties.ReadOnly = Status
        If oFormMode = FORM_MODE.FORM_MODE_ADD Then
            txtKDPENDAFTARAN.Properties.ReadOnly = False
            rbCATEGORY.Properties.ReadOnly = False
        Else
            txtKDPENDAFTARAN.Properties.ReadOnly = True
            rbCATEGORY.Properties.ReadOnly = True
        End If
        grdKDKELASRAWAT.Properties.ReadOnly = Status
        txtKDCUSTOMER.Properties.ReadOnly = Status
        cboASALRUJUKAN.Properties.ReadOnly = Status
        deDATE_RUJUKAN.Properties.ReadOnly = Status
        txtNOMORRUJUKAN.Properties.ReadOnly = Status
        grdKDPPK.Properties.ReadOnly = Status
        txtCATATAN.Properties.ReadOnly = Status
        grdKDDIAGNOSA.Properties.ReadOnly = Status
        grdKDDEPARTMENT.Properties.ReadOnly = Status
        chkISEKSEKUTIF.Properties.ReadOnly = Status
        chkCOB.Properties.ReadOnly = Status
        chkISKATARAK.Properties.ReadOnly = Status
        chkLakaLantas.Properties.ReadOnly = Status
        chkPenjamin1.Properties.ReadOnly = Status
        chkPenjamin2.Properties.ReadOnly = Status
        chkPenjamin3.Properties.ReadOnly = Status
        chkPenjamin4.Properties.ReadOnly = Status
        chkISSUPLESI.Properties.ReadOnly = Status
        txtPENJAMIN_SUPLESI_NOSEPSUPLESI.Properties.ReadOnly = Status
        txtJAMINAN_PENJAMIN_KETERANGAN.Properties.ReadOnly = Status
        txtSUPLESI_PROPINSI.Properties.ReadOnly = Status
        txtSUPLESI_KABUPATEN.Properties.ReadOnly = Status
        txtSUPLESI_KECAMATAN.Properties.ReadOnly = Status
        deDATE_PENJAMIN_TGLKEJADIAN.Properties.ReadOnly = Status
        txtNOMORSKDP.Properties.ReadOnly = Status
        grdKDDOCTOR.Properties.ReadOnly = Status
        txtNOMORTELEPON.Properties.ReadOnly = Status
        chkIsOfline.Properties.ReadOnly = Status
        txtNAMAPENANGGUNGJAWAB.Properties.ReadOnly = Status
        grdKDHUBUNGAN.Properties.ReadOnly = Status
        txtALAMATPENANGGUNGJAWAB.Properties.ReadOnly = Status
        txtNOMORTELEPONPENANGGUNGJAWAB.Properties.ReadOnly = Status
        grdKDKELASRAWAT_NAIKKELAS.Properties.ReadOnly = Status
        txtPEMBIAYAAN.Properties.ReadOnly = Status
        txtPENANGGUNGJAWAB.Properties.ReadOnly = Status
        txtTUJUANKUNJUNGAN.Properties.ReadOnly = Status
        txtFLAGPROCEDURE.Properties.ReadOnly = Status
        txtKDPENUNJANG.Properties.ReadOnly = Status
        txtASSEMENTPEL.Properties.ReadOnly = Status
        grdKDDOCTOR_PELAYANAN.Properties.ReadOnly = Status
        chkISJAGA.Properties.ReadOnly = Status
        grdTempatTidur.Properties.ReadOnly = Status

    End Sub
    Private Sub fn_EmptyMe()
        chkLama.Checked = True
        txtNAMAPASIEN.ResetText()
        txtKDPENDAFTARAN_AWAL.ResetText()
        txtKDJENISKELAMIN.ResetText()
        txtKDPENDAFTARAN.Text = "<--- AUTO --->"
        txtNOMORSEP.Text = "<--- AUTO --->"
        deDATE.DateTime = Now
        txtKDCUSTOMER.ResetText()
        txtKARTUBPJS.ResetText()
        grdKDPERUSAHAAN.Text = oPendaftaran.Perusahaan_Default
        grdKDPENJAMIN.Text = oPendaftaran.Penjamin_Default
        grdKDDAFTAR_L1.ResetText()
        'grdKDDAFTAR_L2.Text = oPendaftaran.Daftar_L2_Default
        'grdKDDAFTAR_L3.Text = oPendaftaran.Daftar_L3_Default
        'grdKDDAFTAR_L4.Text = oPendaftaran.Daftar_L4_Default
        'grdKDDAFTAR_L5.Text = oPendaftaran.Daftar_L5_Default
        grdKDDAFTAR_L6.Text = oPendaftaran.Daftar_L6_Default
        rbCATEGORY_SelectedIndexChanged()
        chkISEKSEKUTIF.Checked = False
        chkISKATARAK.Checked = False
        grdKDDEPARTMENT.ResetText()
        grdKDDOCTOR.ResetText()
        deDATE_RUJUKAN.DateTime = Now
        txtNOMORRUJUKAN.ResetText()
        txtNOMORSKDP.ResetText()
        grdKDDIAGNOSA.ResetText()
        txtNOMORTELEPON.Text = "000000000000"
        txtCATATAN.Text = "-"
        grdKDPPK.Text = oPendaftaran.Daftar_PPK_Default
        cboASALRUJUKAN.SelectedIndex = 0
        grdKDKELASRAWAT.Text = oPendaftaran.Daftar_KELASRAWAT_Default
        chkCOB.Checked = False
        chkLakaLantas.Checked = False
        chkPenjamin1.Checked = False
        chkPenjamin2.Checked = False
        chkPenjamin3.Checked = False
        chkPenjamin4.Checked = False
        deDATE_PENJAMIN_TGLKEJADIAN.DateTime = Now
        txtJAMINAN_PENJAMIN_KETERANGAN.ResetText()
        chkISSUPLESI.Checked = False
        txtPENJAMIN_SUPLESI_NOSEPSUPLESI.ResetText()
        txtSUPLESI_PROPINSI.ResetText()
        txtSUPLESI_KABUPATEN.ResetText()
        txtSUPLESI_KECAMATAN.ResetText()
        txtNAMAPASIEN.ResetText()
        txtKDJENISKELAMIN.ResetText()
        txtNAMAPENANGGUNGJAWAB.Text = "-"
        grdKDHUBUNGAN.Text = oPendaftaran.Daftar_HUBUNGAN_Default
        txtALAMATPENANGGUNGJAWAB.Text = "-"
        txtNOMORTELEPONPENANGGUNGJAWAB.Text = "-"
        If grdKDPENJAMIN.Text.Contains("BPJS KESEHATAN") Then
            chkIsOfline.Checked = False
        End If
        grdKDKELASRAWAT_NAIKKELAS.Text = oPendaftaran.Daftar_KELASRAWAT_Default
        txtPEMBIAYAAN.ResetText()
        txtPENANGGUNGJAWAB.ResetText()
        txtTUJUANKUNJUNGAN.ResetText()
        txtFLAGPROCEDURE.ResetText()
        txtKDPENUNJANG.ResetText()
        txtASSEMENTPEL.ResetText()
        grdKDDOCTOR_PELAYANAN.ResetText()
        chkISJAGA.Checked = False
        grdTempatTidur.ResetText()
        chkRESISTERAWAL.Checked = False
        chkMultiRecord.Checked = True

        txtJALAN.ResetText()
        txtPROPINSI.ResetText()
        txtKECAMATAN.ResetText()
        txtKOTA.ResetText()
        txtKELURAHAN.ResetText()
        txtKODEPOS.ResetText()
    End Sub
    Private Sub fn_LoadData()
        Try
            ' ***** HEADER *****
            Dim ds = oPendaftaran.GetData(sNoId)
            Dim dsPenanggungJawab = oPendaftaran.GetDataPenanggungJawabByPendaftaran(sNoId)

            With ds
                txtKDPENDAFTARAN.Text = sNoId
                txtKDPENDAFTARAN_AWAL.Text = .KDPENDAFTARAN_AWAL
                txtNOMORSEP.Text = .NOMORSEP
                grdKDPENJAMIN.Text = .KDPENJAMIN
                grdKDPERUSAHAAN.Text = .KDPERUSAHAAN
                grdKDDAFTAR_L1.Text = .KDDAFTAR_L1
                grdKDDAFTAR_L2.Text = .KDDAFTAR_L2
                grdKDDAFTAR_L3.Text = .KDDAFTAR_L3
                'grdKDDAFTAR_L4.Text = .KDDAFTAR_L4
                'grdKDDAFTAR_L5.Text = .KDDAFTAR_L5
                grdKDDAFTAR_L6.Text = .KDDAFTAR_L6
                'txtNAMAKELUARGA.Text = .NAMAKELUARGA
                txtKARTUBPJS.Text = .KARTUBPJS
                deDATE.DateTime = .DATE
                rbCATEGORY.SelectedIndex = .CATEGORY
                rbCATEGORY_SelectedIndexChanged()
                grdKDKELASRAWAT.Text = .KDKELASRAWAT
                txtKDCUSTOMER.Text = .KDCUSTOMER
                cboASALRUJUKAN.SelectedIndex = .ASALRUJUKAN
                deDATE_RUJUKAN.DateTime = .DATE_RUJUKAN
                txtNOMORRUJUKAN.Text = .NOMORRUJUKAN
                grdKDPPK.Text = .KDPPK
                txtCATATAN.Text = .CATATAN
                grdKDDIAGNOSA.Text = .KDDIAGNOSA
                grdKDDEPARTMENT.Text = .KDDEPARTMENT
                chkISEKSEKUTIF.Checked = .ISEKSEKUTIF
                chkCOB.Checked = .ISCOB
                chkISKATARAK.Checked = .ISKATARAK
                chkLakaLantas.Checked = .JAMINAN_ISLAKALANTAS
                chkPenjamin1.Checked = .JAMINAN_PENJAMIN_PENJAMIN1
                chkPenjamin2.Checked = .JAMINAN_PENJAMIN_PENJAMIN2
                chkPenjamin3.Checked = .JAMINAN_PENJAMIN_PENJAMIN3
                chkPenjamin4.Checked = .JAMINAN_PENJAMIN_PENJAMIN4
                deDATE_PENJAMIN_TGLKEJADIAN.DateTime = Now
                txtJAMINAN_PENJAMIN_KETERANGAN.Text = .JAMINAN_PENJAMIN_KETERANGAN
                chkISSUPLESI.Checked = .JAMINAN_PENJAMIN_SUPLESI_ISSUPLESI
                txtPENJAMIN_SUPLESI_NOSEPSUPLESI.Text = .JAMINAN_PENJAMIN_SUPLESI_NOSEPSUPLESI
                txtSUPLESI_PROPINSI.Text = .JAMINAN_PENJAMIN_SUPLESI_LOKASILAKA_KDPROPONSI
                txtSUPLESI_KABUPATEN.Text = .JAMINAN_PENJAMIN_SUPLESI_LOKASILAKA_KDKABUPATEN
                txtSUPLESI_KECAMATAN.Text = .JAMINAN_PENJAMIN_SUPLESI_LOKASILAKA_KDKECAMATAN
                txtNOMORSKDP.Text = .NOMORSKDP
                fn_LoadDoctor(grdKDDEPARTMENT.EditValue)
                grdKDDOCTOR.Text = .KDDOCTOR
                txtNOMORTELEPON.Text = .NOMORTELEPON
                chkIsOfline.Checked = .ISOFFLINE
                chkLama.Checked = .ISPASIENLAMA

                grdKDKELASRAWAT_NAIKKELAS.Text = .KDKELASRAWAT_NAIKKELAS
                txtPEMBIAYAAN.Text = .PEMBIAYAAN
                txtPENANGGUNGJAWAB.Text = .PENANGGUNGJAWAB
                txtTUJUANKUNJUNGAN.Text = .TUJUANKUNJUNGAN
                txtFLAGPROCEDURE.Text = .FLAGPROCEDURE
                txtKDPENUNJANG.Text = .KDPENUNJANG
                txtASSEMENTPEL.Text = .ASESMENTPEL
                grdKDDOCTOR_PELAYANAN.Text = .KDDOCTOR_PELAYANAN

                fn_LoadCustomer(.KDCUSTOMER)

                With dsPenanggungJawab
                    txtNAMAPENANGGUNGJAWAB.Text = dsPenanggungJawab.NAMA
                    grdKDHUBUNGAN.Text = dsPenanggungJawab.KDHUBUNGAN
                    txtALAMATPENANGGUNGJAWAB.Text = dsPenanggungJawab.ALAMAT
                    txtNOMORTELEPONPENANGGUNGJAWAB.Text = dsPenanggungJawab.NOMORTELEPON
                End With

                If .CATEGORY = 1 Then
                    Dim oKunjungan_Ruangan As New Admission.clsPendaftaran_KunjunganRuangan
                    Dim dsKunjungan_DataRuangan = oKunjungan_Ruangan.GetDataRuangan1(txtKDPENDAFTARAN.Text)
                    If dsKunjungan_DataRuangan IsNot Nothing Then
                        grdKDRUANGRAWAT.EditValue = dsKunjungan_DataRuangan.KDRUANGRAWAT
                        Dim oRuangRawat As New Reference.clsRuangRawat
                        fn_LoadTempatTidur(dsKunjungan_DataRuangan.KDRUANGRAWAT)
                        grdTempatTidur.EditValue = oRuangRawat.GetDataDetail(dsKunjungan_DataRuangan.KDRUANGRAWAT, dsKunjungan_DataRuangan.SEQ).SEQ
                    End If
                End If

                tabControl.SelectedTabPage = tab3
                tabControl.SelectedTabPage = tab2
                tabControl.SelectedTabPage = tab1

                If txtKDCUSTOMER.Text <> "" Then
                    fn_LoadHistoryPasien(.KDCUSTOMER)
                End If
                chkISJAGA.Checked = .ISJAGA

            End With

        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub btnCEK_Click(sender As Object, e As EventArgs) Handles btnCEK.Click
        Try
            If txtKDCUSTOMER.Text <> "" Then
                picAttacment.Image = GetImageFromURL(AlamatDownloadIamge1 & txtKDCUSTOMER.Text & "/" & txtKDCUSTOMER.Text & ".png")
            End If
        Catch ex As Exception
            fn_SaveDevice(txtKDCUSTOMER.Text)
        End Try
    End Sub
    Private Function GetImageFromURL(ByVal url As String) As Image
        Dim retVal As Image = Nothing

        If Not String.IsNullOrWhiteSpace(url) Then
            Dim req As System.Net.WebRequest = System.Net.WebRequest.Create(url.Trim)

            Using request As System.Net.WebResponse = req.GetResponse
                Using stream As System.IO.Stream = request.GetResponseStream
                    retVal = New Bitmap(System.Drawing.Image.FromStream(stream))
                End Using
            End Using
        End If

        Return retVal

    End Function
    Private Function fn_SaveDevice(ByVal kdcustomer As String) As Boolean
        Try
            Dim listPendaftaran As New List(Of DataAccess.R_HISTORY_PENDAFATRAN)

            Dim oConn_1 As New SqlConnection
            Dim oComm_1 As New SqlCommand
            Dim da_1 As SqlDataAdapter
            Dim ds_1 As New DataSet
            Dim SQL_1 As String

            oConn_1 = New SqlConnection(sConnOld)

            If oConn_1.State = ConnectionState.Closed Then
                oConn_1.Open()
            End If

            SQL_1 = "DELETE FROM  "
            SQL_1 &= "SET_SIGNATURE_CEK "
            SQL_1 &= "WHERE KDCUSTOMER = '" & kdcustomer & "' "

            oComm_1.Connection = oConn_1
            oComm_1.CommandText = SQL_1
            oComm_1.CommandTimeout = 120
            oComm_1.CommandType = CommandType.Text

            da_1 = New SqlDataAdapter(oComm_1)
            da_1.Fill(ds_1, "DELETE")

            SQL_1 = "SELECT  "
            SQL_1 &= "* "
            SQL_1 &= "FROM "
            SQL_1 &= "SET_SIGNATURE_CEK "
            SQL_1 &= "WHERE DEVICE = '" & cboDevice.Text & "' "

            oComm_1.Connection = oConn_1
            oComm_1.CommandText = SQL_1
            oComm_1.CommandTimeout = 120
            oComm_1.CommandType = CommandType.Text

            da_1 = New SqlDataAdapter(oComm_1)
            da_1.Fill(ds_1, "GETDATA")

            Dim Tes As Boolean = False

            For iLoop As Integer = 0 To ds_1.Tables("GETDATA").Rows.Count - 1
                Tes = True

                SQL_1 = "UPDATE  "
                SQL_1 &= "SET_SIGNATURE_CEK "
                SQL_1 &= "SET "
                SQL_1 &= "KDCUSTOMER = '" & kdcustomer & "' "
                SQL_1 &= ",DESCRIPTION = '" & txtNAMAPASIEN.Text & "' "
                SQL_1 &= ",DEVICE = '" & cboDevice.Text & "' "
                SQL_1 &= ",ISREAD = 0 "
                SQL_1 &= "WHERE KDCUSTOMER = '" & kdcustomer & "' "

                oComm_1.Connection = oConn_1
                oComm_1.CommandText = SQL_1
                oComm_1.CommandTimeout = 120
                oComm_1.CommandType = CommandType.Text

                da_1 = New SqlDataAdapter(oComm_1)
                da_1.Fill(ds_1, "INSERT")
            Next

            If Tes = False Then
                SQL_1 = "INSERT INTO  "
                SQL_1 &= "SET_SIGNATURE_CEK "
                SQL_1 &= "( "
                SQL_1 &= "KDCUSTOMER "
                SQL_1 &= ",DESCRIPTION "
                SQL_1 &= ",DEVICE "
                SQL_1 &= ",ISREAD "
                SQL_1 &= ") "
                SQL_1 &= "VALUES "
                SQL_1 &= "( "
                SQL_1 &= "'" & kdcustomer & "' "
                SQL_1 &= ",'" & txtNAMAPASIEN.Text & "' "
                SQL_1 &= ",'" & cboDevice.Text & "' "
                SQL_1 &= ",0 "
                SQL_1 &= ") "

                oComm_1.Connection = oConn_1
                oComm_1.CommandText = SQL_1
                oComm_1.CommandTimeout = 120
                oComm_1.CommandType = CommandType.Text

                da_1 = New SqlDataAdapter(oComm_1)
                da_1.Fill(ds_1, "INSERT")
            End If

            If oConn_1.State = ConnectionState.Open Then
                oConn_1.Close()
            End If

        Catch oErr As Exception
            MsgBox("Preview Data : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try


        'Try
        '    Dim oSet_Signature_Cek As New Setting.clsSet_Signature_Cek
        '    ' ***** HEADER *****
        '    Dim ds = oSet_Signature_Cek.GetStructureHeader
        '    With ds
        '        .KDCUSTOMER = txtNORM.Text
        '        .DESCRIPTION = sUserID
        '        .ISREAD = False
        '        .DEVICE = cboDevice.Text
        '    End With

        '    Dim dsCekRM = oSet_Signature_Cek.GetDataSignatureRM(txtNORM.Text)

        '    If dsCekRM IsNot Nothing Then
        '        oSet_Signature_Cek.DeleteData(dsCekRM.KDCUSTOMER)
        '    End If

        '    Dim dsCek = oSet_Signature_Cek.GetDataSignatureDevice(cboDevice.Text)

        '    If dsCek IsNot Nothing Then
        '        fn_SaveDevice = oSet_Signature_Cek.UpdateData(ds)
        '    Else
        '        fn_SaveDevice = oSet_Signature_Cek.InsertData(ds)
        '    End If

        '    sDeviceDefault = cboDevice.Text

        'Catch oErr As Exception
        '    MsgBox("Simpan Data : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        '    fn_SaveDevice = False
        'End Try
    End Function
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

            If grdKDKELASRAWAT.Text = String.Empty Then
                grdKDKELASRAWAT.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                grdKDKELASRAWAT.ErrorText = Statement.ErrorRequired

                grdKDKELASRAWAT.Focus()
                fn_Validate = False
                Exit Function
            End If
            If grdKDPENJAMIN.Text = String.Empty Then
                grdKDPENJAMIN.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                grdKDPENJAMIN.ErrorText = Statement.ErrorRequired

                grdKDPENJAMIN.Focus()
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
            'If grdKDDAFTAR_L4.Text = String.Empty Then
            '    grdKDDAFTAR_L4.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
            '    grdKDDAFTAR_L4.ErrorText = Statement.ErrorRequired

            '    grdKDDAFTAR_L4.Focus()
            '    fn_Validate = False
            '    Exit Function
            'End If
            'If grdKDDAFTAR_L5.Text = String.Empty Then
            '    grdKDDAFTAR_L5.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
            '    grdKDDAFTAR_L5.ErrorText = Statement.ErrorRequired

            '    grdKDDAFTAR_L5.Focus()
            '    fn_Validate = False
            '    Exit Function
            'End If
            If grdKDDAFTAR_L6.Text = String.Empty Then
                grdKDDAFTAR_L6.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                grdKDDAFTAR_L6.ErrorText = Statement.ErrorRequired

                grdKDDAFTAR_L6.Focus()
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

            If txtKDCUSTOMER.Text = String.Empty Then
                txtKDCUSTOMER.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                txtKDCUSTOMER.ErrorText = Statement.ErrorRequired

                txtKDCUSTOMER.Focus()
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
            If grdKDDOCTOR.Text = String.Empty Then
                grdKDDOCTOR.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                grdKDDOCTOR.ErrorText = Statement.ErrorRequired

                grdKDDOCTOR.Focus()
                fn_Validate = False
                Exit Function
            End If
            If grdKDDOCTOR_PELAYANAN.Text = String.Empty Then
                grdKDDOCTOR_PELAYANAN.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                grdKDDOCTOR_PELAYANAN.ErrorText = Statement.ErrorRequired

                grdKDDOCTOR_PELAYANAN.Focus()
                fn_Validate = False
                Exit Function
            End If
            If grdKDKELASRAWAT_NAIKKELAS.Text = String.Empty Then
                grdKDKELASRAWAT_NAIKKELAS.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                grdKDKELASRAWAT_NAIKKELAS.ErrorText = Statement.ErrorRequired

                grdKDKELASRAWAT_NAIKKELAS.Focus()
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

            If grdKDPERUSAHAAN.Text = String.Empty Then
                grdKDPERUSAHAAN.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                grdKDPERUSAHAAN.ErrorText = Statement.ErrorRequired

                grdKDPERUSAHAAN.Focus()
                fn_Validate = False
                Exit Function
            End If

            'If txtKDPENDAFTARAN_AWAL.Text = String.Empty And rbCATEGORY.SelectedIndex = 1 Then
            '    If cboASALRUJUKAN.SelectedIndex = 0 Then
            '        txtKDPENDAFTARAN_AWAL.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
            '        txtKDPENDAFTARAN_AWAL.ErrorText = Statement.ErrorRequired

            '        txtKDPENDAFTARAN_AWAL.Focus()
            '        fn_Validate = False
            '        Exit Function
            '    End If
            'End If

            Dim oDepartment As New Reference.clsDepartment

            If chkISEKSEKUTIF.Checked = True Then
                Dim dsDepartment = oDepartment.GetData(grdKDDEPARTMENT.EditValue)
                If dsDepartment IsNot Nothing Then
                    If dsDepartment.ISEKSEKUTIF = False Then
                        MsgBox("Bukan Eksekutif")
                        fn_Validate = False
                        Exit Function
                    End If
                End If
            End If

            If txtCATATAN.Text = String.Empty Then
                txtCATATAN.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                txtCATATAN.ErrorText = Statement.ErrorRequired

                txtCATATAN.Focus()
                fn_Validate = False
                Exit Function
            End If

            If chkISKATARAK.Checked = True Then
                Dim dsDepartment = oDepartment.GetData(grdKDDEPARTMENT.EditValue)
                If dsDepartment IsNot Nothing Then
                    If dsDepartment.ISKATARAK = False Then
                        MsgBox("Tidak bisa buka Katarak")
                        fn_Validate = False
                        Exit Function
                    End If
                End If
            End If

            If chkLakaLantas.Checked = True Then
                If txtJAMINAN_PENJAMIN_KETERANGAN.Text = String.Empty Then
                    txtJAMINAN_PENJAMIN_KETERANGAN.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                    txtJAMINAN_PENJAMIN_KETERANGAN.ErrorText = Statement.ErrorRequired

                    txtJAMINAN_PENJAMIN_KETERANGAN.Focus()
                    fn_Validate = False
                    Exit Function
                End If

                If chkISSUPLESI.Checked = True Then
                    If txtPENJAMIN_SUPLESI_NOSEPSUPLESI.Text = String.Empty Then
                        txtPENJAMIN_SUPLESI_NOSEPSUPLESI.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                        txtPENJAMIN_SUPLESI_NOSEPSUPLESI.ErrorText = Statement.ErrorRequired

                        txtPENJAMIN_SUPLESI_NOSEPSUPLESI.Focus()
                        fn_Validate = False
                        Exit Function
                    End If
                    If txtSUPLESI_PROPINSI.Text = String.Empty Then
                        txtSUPLESI_PROPINSI.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                        txtSUPLESI_PROPINSI.ErrorText = Statement.ErrorRequired

                        txtSUPLESI_PROPINSI.Focus()
                        fn_Validate = False
                        Exit Function
                    End If
                    If txtSUPLESI_KABUPATEN.Text = String.Empty Then
                        txtSUPLESI_KABUPATEN.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                        txtSUPLESI_KABUPATEN.ErrorText = Statement.ErrorRequired

                        txtSUPLESI_KABUPATEN.Focus()
                        fn_Validate = False
                        Exit Function
                    End If
                    If txtSUPLESI_KECAMATAN.Text = String.Empty Then
                        txtSUPLESI_KECAMATAN.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                        txtSUPLESI_KECAMATAN.ErrorText = Statement.ErrorRequired

                        txtSUPLESI_KECAMATAN.Focus()
                        fn_Validate = False
                        Exit Function
                    End If
                End If
            End If

            If txtNOMORTELEPON.Text.Count <= 7 Then
                txtNOMORTELEPON.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                txtNOMORTELEPON.ErrorText = Statement.ErrorRequired
                MsgBox("Nomor Telepon Minimal 7 digit", MsgBoxStyle.Exclamation, Me.Text)
                txtNOMORTELEPON.Focus()
                fn_Validate = False
                Exit Function
            End If

            'Dim dsSIP

            'Daftar Poli sama
            If oFormMode = FORM_MODE.FORM_MODE_ADD Then
                If rbCATEGORY.SelectedIndex = 0 Then
                    If grdKDPENJAMIN.Text = "BPJS KESEHATAN" Then
                        If rbCATEGORY.SelectedIndex = 0 Then
                            Dim oPOLI As New Reference.clsDepartment
                            If grdKDDEPARTMENT.EditValue <> "IGD" Then
                                Dim dsKunjunganSama = oPendaftaran.GetDataByRMUnitDate(txtKDCUSTOMER.Text, grdKDDEPARTMENT.EditValue, deDATE.DateTime)
                                If dsKunjunganSama IsNot Nothing Then
                                    MsgBox("Pasien hari ini sudah berkunjung ke Poli yg sama dengan No Pendaftaran " & dsKunjunganSama.KDPENDAFTARAN, MsgBoxStyle.Exclamation, Me.Text)
                                    fn_Validate = False
                                    Exit Function
                                End If
                                If chkIsOfline.Checked = False Then
                                    If txtNOMORRUJUKAN.Text = String.Empty Then
                                        txtNOMORRUJUKAN.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                                        txtNOMORRUJUKAN.ErrorText = Statement.ErrorRequired

                                        txtNOMORRUJUKAN.Focus()
                                        fn_Validate = False
                                        Exit Function
                                    End If
                                End If

                            End If
                        End If
                    Else
                        Dim oPOLI As New Reference.clsDepartment
                        If grdKDDEPARTMENT.EditValue <> "IGD" Then
                            Dim dsKunjunganSama = oPendaftaran.GetDataByRMUnitDate(txtKDCUSTOMER.Text, grdKDDEPARTMENT.EditValue, deDATE.DateTime)
                            If dsKunjunganSama IsNot Nothing Then
                                If MsgBox("Pasien hari ini sudah berkunjung ke Poli yg sama dengan No Pendaftaran " & dsKunjunganSama.KDPENDAFTARAN & " Apakah Akan lanjut Transaksi ?", MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, Me.Text) = MsgBoxResult.No Then
                                    fn_Validate = False
                                    Exit Function
                                End If

                            End If
                        End If
                    End If

                    If grdKDPENJAMIN.Text = "BPJS KESEHATAN" Then
                        If rbCATEGORY.SelectedIndex = 0 Then
                            Dim dsPoliTerakhir = oPendaftaran.GetDataByRMKunjunganTerkahir(txtKDCUSTOMER.Text)

                            If dsPoliTerakhir IsNot Nothing Then
                                Dim oKunjungan = DateDiff(DateInterval.Day, dsPoliTerakhir.DATE, deDATE.DateTime) + 1

                                If oKunjungan <= 7 Then
                                    If MsgBox("Apakah akan dilanjutkan, Tujuan Terkahir : " & dsPoliTerakhir.M_DEPARTMENT.NAME_DISPLAY & " Tanggal " & dsPoliTerakhir.DATE.ToString("dd-MM-yyyy"), MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, "Kunjungan Kurang dari 7 hari : " & oKunjungan) = MsgBoxResult.No Then
                                        fn_Validate = False
                                        Exit Function
                                    End If
                                End If

                            End If
                        End If
                    End If
                End If
            End If

            If rbCATEGORY.SelectedIndex = 1 Then
                If oFormMode = FORM_MODE.FORM_MODE_ADD Then
                    Dim dsRuangan = oPendaftaran.GetDataByRMDateRawatInap(txtKDCUSTOMER.Text, deDATE.DateTime)

                    If dsRuangan IsNot Nothing Then
                        If MsgBox("Rekam Medis : " & dsRuangan.KDCUSTOMER & " Sudah mendapatkan register rawat inap dengan tanggal yang sama nomor pendaftaran : " & dsRuangan.KDPENDAFTARAN & " Apakah akan melanjutkan?", MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, Me.Text) = MsgBoxResult.No Then
                            fn_Validate = False
                            Exit Function
                        End If

                    End If
                End If

                If grdKDRUANGRAWAT.Text = String.Empty Then
                    tabControl.SelectedTabPage = tab2
                    grdKDRUANGRAWAT.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                    grdKDRUANGRAWAT.ErrorText = Statement.ErrorRequired

                    grdKDRUANGRAWAT.Focus()
                    fn_Validate = False
                    Exit Function
                End If

                If grdTempatTidur.Text = String.Empty Then
                    tabControl.SelectedTabPage = tab2
                    grdTempatTidur.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                    grdTempatTidur.ErrorText = Statement.ErrorRequired
                    MsgBox("Tempat Tidur Masih Kosong", MsgBoxStyle.Information, Me.Text)

                    grdTempatTidur.Focus()
                    fn_Validate = False
                    Exit Function
                End If

                If txtNAMAPENANGGUNGJAWAB.Text = String.Empty Then
                    tabControl.SelectedTabPage = tab2
                    txtNAMAPENANGGUNGJAWAB.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                    txtNAMAPENANGGUNGJAWAB.ErrorText = Statement.ErrorRequired

                    txtNAMAPENANGGUNGJAWAB.Focus()
                    fn_Validate = False
                    Exit Function
                End If
                If txtALAMATPENANGGUNGJAWAB.Text = String.Empty Then
                    txtALAMATPENANGGUNGJAWAB.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                    txtALAMATPENANGGUNGJAWAB.ErrorText = Statement.ErrorRequired

                    txtALAMATPENANGGUNGJAWAB.Focus()
                    fn_Validate = False
                    Exit Function
                End If
                If txtNOMORTELEPONPENANGGUNGJAWAB.Text = String.Empty Then
                    txtNOMORTELEPONPENANGGUNGJAWAB.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                    txtNOMORTELEPONPENANGGUNGJAWAB.ErrorText = Statement.ErrorRequired

                    txtNOMORTELEPONPENANGGUNGJAWAB.Focus()
                    fn_Validate = False
                    Exit Function
                End If

                'Dim oKelasAplicares As New Reference.clsKelasAplicare

                'Dim dsKelasAplicare = oKelasAplicares.GetDataDetail_UOM(grdKDUPDATE_APLICARE.EditValue)
                'If dsKelasAplicare IsNot Nothing Then
                '    If txtKDJENISKELAMIN.Text = "P" Then
                '        If dsKelasAplicare.TERSEDIA_PEREMPUAN <= 0 Then
                '            MsgBox(Statement.ErrorStatement & " Ruangan Penuh Untuk Pasien Perempuan", MsgBoxStyle.Information, Me.Text)

                '            grdKDUPDATE_APLICARE.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                '            grdKDUPDATE_APLICARE.ErrorText = Statement.ErrorRequired

                '            grdKDUPDATE_APLICARE.Focus()
                '            fn_Validate = False
                '            Exit Function

                '        End If

                '    Else
                '        If dsKelasAplicare.TERSEDIA_LAKI <= 0 Then
                '            MsgBox(Statement.ErrorStatement & " Ruangan Penuh Untuk Pasien Laki-laki", MsgBoxStyle.Information, Me.Text)

                '            grdKDUPDATE_APLICARE.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                '            grdKDUPDATE_APLICARE.ErrorText = Statement.ErrorRequired

                '            grdKDUPDATE_APLICARE.Focus()
                '            fn_Validate = False
                '            Exit Function

                '        End If
                '    End If
                'End If

            End If
            'If chkIsOfline.Checked = False Then
            '    If txtNOMORSKDP.Text = "" Then
            '        Dim sLASTNUMBER As Integer = 0
            '        Dim sMODUL As String = "SKD"
            '        Dim oCounter As New Setting.clsCounter
            '        sLASTNUMBER = oCounter.GetLastNumber(sMODUL)
            '        If sLASTNUMBER = 0 Then
            '            Try
            '                oCounter.InsertData(sMODUL, Now)
            '                sLASTNUMBER = oCounter.GetLastNumber(sMODUL, Now)
            '            Catch ex As Exception
            '                sLASTNUMBER = 0
            '            End Try
            '        End If

            '        oCounter.UpdateData(sMODUL, sLASTNUMBER + 1)

            '        Dim NOMORSKDP As String = String.Empty
            '        NOMORSKDP = sLASTNUMBER + 1
            '        txtNOMORSKDP.Text = NOMORSKDP
            '    End If
            'End If
        Catch oErr As Exception
            fn_Validate = False
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Function
    Private Function fn_jsonRequestInsertSEPV1() As String
        Try
            Dim jsonRequest As String = String.Empty
            Dim oFaskes As New Reference.clsPPK
            Dim oPoli As New Reference.clsDepartment
            Dim oDoctor As New Reference.clsDoctor
            Dim jaminanPenjamin As String = String.Empty
            Dim oDepartment As New Reference.clsDepartment

            If chkPenjamin1.Checked = True And chkPenjamin2.Checked = False And chkPenjamin3.Checked = False And chkPenjamin4.Checked = False Then
                jaminanPenjamin = "1"
            ElseIf chkPenjamin1.Checked = True And chkPenjamin2.Checked = True And chkPenjamin3.Checked = False And chkPenjamin4.Checked = False Then
                jaminanPenjamin = "1,2"
            ElseIf chkPenjamin1.Checked = True And chkPenjamin2.Checked = True And chkPenjamin3.Checked = True And chkPenjamin4.Checked = False Then
                jaminanPenjamin = "1,2,3"
            ElseIf chkPenjamin1.Checked = True And chkPenjamin2.Checked = True And chkPenjamin3.Checked = True And chkPenjamin4.Checked = True Then
                jaminanPenjamin = "1,2,3,4"
            ElseIf chkPenjamin1.Checked = True And chkPenjamin2.Checked = False And chkPenjamin3.Checked = True And chkPenjamin4.Checked = False Then
                jaminanPenjamin = "1,3"
            ElseIf chkPenjamin1.Checked = True And chkPenjamin2.Checked = False And chkPenjamin3.Checked = False And chkPenjamin4.Checked = True Then
                jaminanPenjamin = "1,4"
            ElseIf chkPenjamin1.Checked = False And chkPenjamin2.Checked = True And chkPenjamin3.Checked = False And chkPenjamin4.Checked = False Then
                jaminanPenjamin = "2"
            ElseIf chkPenjamin1.Checked = False And chkPenjamin2.Checked = True And chkPenjamin3.Checked = True And chkPenjamin4.Checked = False Then
                jaminanPenjamin = "2,3"
            ElseIf chkPenjamin1.Checked = False And chkPenjamin2.Checked = True And chkPenjamin3.Checked = True And chkPenjamin4.Checked = True Then
                jaminanPenjamin = "2,3,4"
            ElseIf chkPenjamin1.Checked = False And chkPenjamin2.Checked = True And chkPenjamin3.Checked = False And chkPenjamin4.Checked = True Then
                jaminanPenjamin = "2,4"
            ElseIf chkPenjamin1.Checked = False And chkPenjamin2.Checked = False And chkPenjamin3.Checked = True And chkPenjamin4.Checked = False Then
                jaminanPenjamin = "3"
            ElseIf chkPenjamin1.Checked = False And chkPenjamin2.Checked = False And chkPenjamin3.Checked = True And chkPenjamin4.Checked = True Then
                jaminanPenjamin = "3,4"
            ElseIf chkPenjamin1.Checked = False And chkPenjamin2.Checked = False And chkPenjamin3.Checked = False And chkPenjamin4.Checked = True Then
                jaminanPenjamin = "4"
            End If

            jsonRequest = " { "
            jsonRequest &= """request"" :  { "
            jsonRequest &= """t_sep"": { "
            jsonRequest &= """noKartu"": """ & txtKARTUBPJS.Text.Trim.ToUpper & ""","
            jsonRequest &= """tglSep"": """ & deDATE.DateTime.ToString("yyyy-MM-dd") & """, "
            jsonRequest &= """ppkPelayanan"": """ & PPKPELAYANAN & """, "
            jsonRequest &= """jnsPelayanan"": """ & IIf(rbCATEGORY.SelectedIndex = 0, 2, 1) & """, "
            jsonRequest &= """klsRawat"": """ & IIf(rbCATEGORY.SelectedIndex = 0, "3", grdKDKELASRAWAT.EditValue) & """, "
            jsonRequest &= """noMR"": """ & txtKDCUSTOMER.Text.ToString & """, "
            jsonRequest &= """rujukan"": { "
            jsonRequest &= """asalRujukan"": """ & cboASALRUJUKAN.SelectedIndex + 1 & """, "
            jsonRequest &= """tglRujukan"": """ & deDATE_RUJUKAN.DateTime.ToString("yyyy-MM-dd") & """, "
            jsonRequest &= """noRujukan"": """ & txtNOMORRUJUKAN.Text.ToString.Trim.ToUpper & """, "
            jsonRequest &= """ppkRujukan"": """ & oFaskes.GetData(grdKDPPK.EditValue).KODEFASKES & """ "
            jsonRequest &= "}, "
            jsonRequest &= """catatan"": """ & txtCATATAN.Text.ToString.Trim.ToUpper & """, "
            jsonRequest &= """diagAwal"": """ & grdKDDIAGNOSA.EditValue & """, "
            jsonRequest &= """poli"": { "
            jsonRequest &= """tujuan"": """ & oDepartment.GetData(grdKDDEPARTMENT.EditValue).KDDEPARTMENT_BPJS & """, "
            jsonRequest &= """eksekutif"": """ & IIf(chkISEKSEKUTIF.Checked = False, 0, 1) & """ "
            jsonRequest &= "}, "
            jsonRequest &= """cob"": { "
            jsonRequest &= """cob"": """ & IIf(chkCOB.Checked = False, 0, 1) & """ "
            jsonRequest &= "}, "
            jsonRequest &= """katarak"": { "
            jsonRequest &= """katarak"": """ & IIf(chkISKATARAK.Checked = False, 0, 1) & """ "
            jsonRequest &= "}, "
            jsonRequest &= """jaminan"": { "
            If chkLakaLantas.Checked = False Then
                jsonRequest &= """lakaLantas"": """ & 0 & """, "
                jsonRequest &= """penjamin"": { "
                jsonRequest &= """penjamin"": """ & "" & """, "
                jsonRequest &= """tglKejadian"": """ & "" & """, "
                jsonRequest &= """keterangan"": """ & "" & """, "
                jsonRequest &= """suplesi"": { "
                jsonRequest &= """suplesi"": """ & 0 & """, "
                jsonRequest &= """noSepSuplesi"": """ & "" & """, "
                jsonRequest &= """lokasiLaka"": { "
                jsonRequest &= """kdPropinsi"": """ & "" & """, "
                jsonRequest &= """kdKabupaten"": """ & "" & """, "
                jsonRequest &= """kdKecamatan"": """ & "" & """ "
            Else
                jsonRequest &= """lakaLantas"": """ & 1 & """, "
                jsonRequest &= """penjamin"": { "
                jsonRequest &= """penjamin"": """ & jaminanPenjamin & """, "
                jsonRequest &= """tglKejadian"": """ & deDATE_PENJAMIN_TGLKEJADIAN.DateTime.ToString("yyyy-MM-dd") & """, "
                jsonRequest &= """keterangan"": """ & txtJAMINAN_PENJAMIN_KETERANGAN.Text.ToString.Trim.ToUpper & """, "
                jsonRequest &= """suplesi"": { "
                jsonRequest &= """suplesi"": """ & IIf(chkISSUPLESI.Checked = False, 0, 1) & """, "
                jsonRequest &= """noSepSuplesi"": """ & txtPENJAMIN_SUPLESI_NOSEPSUPLESI.Text.ToString.Trim.ToUpper & """, "
                jsonRequest &= """lokasiLaka"": { "
                jsonRequest &= """kdPropinsi"": """ & txtSUPLESI_PROPINSI.Text & """, "
                jsonRequest &= """kdKabupaten"": """ & txtSUPLESI_KABUPATEN.Text & """, "
                jsonRequest &= """kdKecamatan"": """ & txtSUPLESI_KECAMATAN.Text & """ "
            End If
            jsonRequest &= "} "
            jsonRequest &= "} "
            jsonRequest &= "} "
            jsonRequest &= "}, "
            jsonRequest &= """skdp"": { "
            jsonRequest &= """noSurat"": """ & IIf(txtNOMORSKDP.Text = String.Empty, "", Microsoft.VisualBasic.Right(txtNOMORSKDP.Text, 6)) & """, "
            jsonRequest &= """kodeDPJP"": """ & oDoctor.GetData(grdKDDOCTOR.EditValue).VCLAIM_KDDPJP & """ "
            jsonRequest &= "}, "
            jsonRequest &= """noTelp"": """ & txtNOMORTELEPON.Text.ToString.Trim.ToUpper & """, "
            jsonRequest &= """user"": """ & sUserID & """ "
            jsonRequest &= "} "
            jsonRequest &= "} "
            jsonRequest &= "}  "

            fn_jsonRequestInsertSEPV1 = jsonRequest
        Catch oErr As Exception
            fn_jsonRequestInsertSEPV1 = ""
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Function
    Private Function fn_jsonRequestUpdateSEPV1() As String
        Try
            Dim jsonRequest As String = String.Empty
            Dim oFaskes As New Reference.clsPPK
            Dim oPoli As New Reference.clsDepartment
            Dim oDoctor As New Reference.clsDoctor
            Dim jaminanPenjamin As String = String.Empty

            If chkPenjamin1.Checked = True And chkPenjamin2.Checked = False And chkPenjamin3.Checked = False And chkPenjamin4.Checked = False Then
                jaminanPenjamin = "1"
            ElseIf chkPenjamin1.Checked = True And chkPenjamin2.Checked = True And chkPenjamin3.Checked = False And chkPenjamin4.Checked = False Then
                jaminanPenjamin = "1,2"
            ElseIf chkPenjamin1.Checked = True And chkPenjamin2.Checked = True And chkPenjamin3.Checked = True And chkPenjamin4.Checked = False Then
                jaminanPenjamin = "1,2,3"
            ElseIf chkPenjamin1.Checked = True And chkPenjamin2.Checked = True And chkPenjamin3.Checked = True And chkPenjamin4.Checked = True Then
                jaminanPenjamin = "1,2,3,4"
            ElseIf chkPenjamin1.Checked = True And chkPenjamin2.Checked = False And chkPenjamin3.Checked = True And chkPenjamin4.Checked = False Then
                jaminanPenjamin = "1,3"
            ElseIf chkPenjamin1.Checked = True And chkPenjamin2.Checked = False And chkPenjamin3.Checked = False And chkPenjamin4.Checked = True Then
                jaminanPenjamin = "1,4"
            ElseIf chkPenjamin1.Checked = False And chkPenjamin2.Checked = True And chkPenjamin3.Checked = False And chkPenjamin4.Checked = False Then
                jaminanPenjamin = "2"
            ElseIf chkPenjamin1.Checked = False And chkPenjamin2.Checked = True And chkPenjamin3.Checked = True And chkPenjamin4.Checked = False Then
                jaminanPenjamin = "2,3"
            ElseIf chkPenjamin1.Checked = False And chkPenjamin2.Checked = True And chkPenjamin3.Checked = True And chkPenjamin4.Checked = True Then
                jaminanPenjamin = "2,3,4"
            ElseIf chkPenjamin1.Checked = False And chkPenjamin2.Checked = True And chkPenjamin3.Checked = False And chkPenjamin4.Checked = True Then
                jaminanPenjamin = "2,4"
            ElseIf chkPenjamin1.Checked = False And chkPenjamin2.Checked = False And chkPenjamin3.Checked = True And chkPenjamin4.Checked = False Then
                jaminanPenjamin = "3"
            ElseIf chkPenjamin1.Checked = False And chkPenjamin2.Checked = False And chkPenjamin3.Checked = True And chkPenjamin4.Checked = True Then
                jaminanPenjamin = "3,4"
            ElseIf chkPenjamin1.Checked = False And chkPenjamin2.Checked = False And chkPenjamin3.Checked = False And chkPenjamin4.Checked = True Then
                jaminanPenjamin = "4"
            End If

            jsonRequest = " { "
            jsonRequest &= """request"": { "
            jsonRequest &= """t_sep"": { "
            jsonRequest &= """noSep"": """ & txtNOMORSEP.Text & """, "
            jsonRequest &= """klsRawat"": """ & IIf(rbCATEGORY.SelectedIndex = 0, "3", grdKDKELASRAWAT.EditValue) & """, "
            jsonRequest &= """noMR"": """ & txtKDCUSTOMER.Text.ToString & """, "
            jsonRequest &= """rujukan"":{ "
            jsonRequest &= """asalRujukan"":""" & cboASALRUJUKAN.SelectedIndex + 1 & """, "
            jsonRequest &= """tglRujukan"":""" & deDATE_RUJUKAN.DateTime.ToString("yyyy-MM-dd") & """, "
            jsonRequest &= """noRujukan"":""" & txtNOMORRUJUKAN.Text.ToString.Trim.ToUpper & """, "
            jsonRequest &= """ppkRujukan"":""" & oFaskes.GetData(grdKDPPK.EditValue).KODEFASKES & """ "
            jsonRequest &= "}, "
            jsonRequest &= """catatan"": """ & txtCATATAN.Text.ToString.Trim.ToUpper & """, "
            jsonRequest &= """diagAwal"": """ & grdKDDIAGNOSA.EditValue & """, "
            jsonRequest &= """poli"" :  { "
            jsonRequest &= """eksekutif"": """ & IIf(chkISEKSEKUTIF.Checked = False, 0, 1) & """ "
            jsonRequest &= "}, "
            jsonRequest &= """cob"": { "
            jsonRequest &= """cob"": """ & IIf(chkCOB.Checked = False, 0, 1) & """ "
            jsonRequest &= "}, "
            jsonRequest &= """katarak"":{ "
            jsonRequest &= """katarak"":""" & IIf(chkISKATARAK.Checked = False, 0, 1) & """ "
            jsonRequest &= "}, "
            jsonRequest &= """skdp"":{ "
            jsonRequest &= """noSurat"":""" & IIf(txtNOMORSKDP.Text = String.Empty, "", Microsoft.VisualBasic.Right(txtNOMORSKDP.Text, 6)) & """, "
            jsonRequest &= """kodeDPJP"":""" & oDoctor.GetData(grdKDDOCTOR.EditValue).VCLAIM_KDDPJP & """ "
            jsonRequest &= "}, "
            jsonRequest &= """jaminan"": { "
            If chkLakaLantas.Checked = False Then
                jsonRequest &= """lakaLantas"": """ & 0 & """, "
                jsonRequest &= """penjamin"": { "
                jsonRequest &= """penjamin"": """ & "" & """, "
                jsonRequest &= """tglKejadian"": """ & "" & """, "
                jsonRequest &= """keterangan"": """ & "" & """, "
                jsonRequest &= """suplesi"": { "
                jsonRequest &= """suplesi"": """ & 0 & """, "
                jsonRequest &= """noSepSuplesi"": """ & "" & """, "
                jsonRequest &= """lokasiLaka"": { "
                jsonRequest &= """kdPropinsi"": """ & "" & """, "
                jsonRequest &= """kdKabupaten"": """ & "" & """, "
                jsonRequest &= """kdKecamatan"": """ & "" & """ "
            Else
                jsonRequest &= """lakaLantas"": """ & 1 & """, "
                jsonRequest &= """penjamin"": { "
                jsonRequest &= """penjamin"": """ & jaminanPenjamin & """, "
                jsonRequest &= """tglKejadian"": """ & deDATE_PENJAMIN_TGLKEJADIAN.DateTime.ToString("yyyy-MM-dd") & """, "
                jsonRequest &= """keterangan"": """ & txtJAMINAN_PENJAMIN_KETERANGAN.Text.ToString.Trim.ToUpper & """, "
                jsonRequest &= """suplesi"": { "
                jsonRequest &= """suplesi"": """ & IIf(chkISSUPLESI.Checked = False, 0, 1) & """, "
                jsonRequest &= """noSepSuplesi"": """ & txtPENJAMIN_SUPLESI_NOSEPSUPLESI.Text.ToString.Trim.ToUpper & """, "
                jsonRequest &= """lokasiLaka"": { "

                Dim oPropinsi As New Reference.clsPropinsi
                Dim oKabupaten As New Reference.clsKabupaten
                Dim oKecamatan As New Reference.clsKecamatan

                jsonRequest &= """kdPropinsi"": """ & txtSUPLESI_PROPINSI.Text & """, "
                jsonRequest &= """kdKabupaten"": """ & txtSUPLESI_KABUPATEN.Text & """, "
                jsonRequest &= """kdKecamatan"": """ & txtSUPLESI_KECAMATAN.Text & """ "
            End If

            jsonRequest &= "} "
            jsonRequest &= "} "
            jsonRequest &= "} "
            jsonRequest &= "}, "
            jsonRequest &= """noTelp"": """ & txtNOMORTELEPON.Text.ToString.Trim.ToUpper & """, "
            jsonRequest &= """user"": """ & sUserID & """ "
            jsonRequest &= "} "
            jsonRequest &= "} "
            jsonRequest &= "} "

            fn_jsonRequestUpdateSEPV1 = jsonRequest
        Catch oErr As Exception
            fn_jsonRequestUpdateSEPV1 = ""
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Function
    Private Function fn_jsonRequestInsertSEPV2() As String
        Try
            Dim jsonRequest As String = String.Empty
            Dim oFaskes As New Reference.clsPPK
            Dim oPoli As New Reference.clsDepartment
            Dim oDoctor As New Reference.clsDoctor
            Dim oDepartment As New Reference.clsDepartment

            jsonRequest = " { "
            jsonRequest &= """request"" :  { "
            jsonRequest &= """t_sep"": { "
            jsonRequest &= """noKartu"": """ & txtKARTUBPJS.Text.Trim.ToUpper & ""","
            jsonRequest &= """tglSep"": """ & deDATE.DateTime.ToString("yyyy-MM-dd") & """, "
            jsonRequest &= """ppkPelayanan"": """ & oSetKoneksi.GetData().Where(Function(x) x.ISACTIVE = True And x.NAME_DISPLAY = "VCLAIM").FirstOrDefault.PPKPELAYANAN.ToString.Trim.ToUpper & """, "
            jsonRequest &= """jnsPelayanan"": """ & IIf(rbCATEGORY.SelectedIndex = 0, 2, 1) & """, "
            jsonRequest &= """klsRawat"": { "
            jsonRequest &= """klsRawatHak"": """ & IIf(rbCATEGORY.SelectedIndex = 0, "3", grdKDKELASRAWAT.EditValue) & """, "
            jsonRequest &= """klsRawatNaik"": """ & IIf(grdKDKELASRAWAT.Text = grdKDKELASRAWAT_NAIKKELAS.Text, "", grdKDKELASRAWAT_NAIKKELAS.EditValue) & """, "
            jsonRequest &= """pembiayaan"": """ & IIf(grdKDKELASRAWAT.Text = grdKDKELASRAWAT_NAIKKELAS.Text, "", txtPEMBIAYAAN.SelectedIndex + 1) & """, "
            jsonRequest &= """penanggungJawab"": """ & IIf(grdKDKELASRAWAT.Text = grdKDKELASRAWAT_NAIKKELAS.Text, "", txtPENANGGUNGJAWAB.Text) & """ "
            jsonRequest &= "}, "
            jsonRequest &= """noMR"": """ & txtKDCUSTOMER.Text.ToString & """, "
            jsonRequest &= """rujukan"": { "
            jsonRequest &= """asalRujukan"": """ & cboASALRUJUKAN.SelectedIndex + 1 & """, "
            jsonRequest &= """tglRujukan"": """ & deDATE_RUJUKAN.DateTime.ToString("yyyy-MM-dd") & """, "
            jsonRequest &= """noRujukan"": """ & txtNOMORRUJUKAN.Text.ToString.Trim.ToUpper & """, "
            jsonRequest &= """ppkRujukan"": """ & oFaskes.GetData(grdKDPPK.EditValue).KODEFASKES & """ "
            jsonRequest &= "}, "
            jsonRequest &= """catatan"": """ & txtCATATAN.Text.ToString.Trim.ToUpper & """, "
            jsonRequest &= """diagAwal"": """ & grdKDDIAGNOSA.EditValue & """, "
            jsonRequest &= """poli"": { "
            jsonRequest &= """tujuan"": """ & IIf(rbCATEGORY.SelectedIndex = 0, oDepartment.GetData(grdKDDEPARTMENT.EditValue).KDDEPARTMENT_BPJS, "") & """, "
            jsonRequest &= """eksekutif"": """ & IIf(chkISEKSEKUTIF.Checked = False, 0, 1) & """ "
            jsonRequest &= "}, "
            jsonRequest &= """cob"": { "
            jsonRequest &= """cob"": """ & IIf(chkCOB.Checked = False, 0, 1) & """ "
            jsonRequest &= "}, "
            jsonRequest &= """katarak"": { "
            jsonRequest &= """katarak"": """ & IIf(chkISKATARAK.Checked = False, 0, 1) & """ "
            jsonRequest &= "}, "
            jsonRequest &= """jaminan"": { "
            If chkLakaLantas.Checked = False Then
                jsonRequest &= """lakaLantas"": """ & 0 & """, "
                jsonRequest &= """penjamin"": { "
                jsonRequest &= """tglKejadian"": """ & "" & """, "
                jsonRequest &= """keterangan"": """ & "" & """, "
                jsonRequest &= """suplesi"": { "
                jsonRequest &= """suplesi"": """ & 0 & """, "
                jsonRequest &= """noSepSuplesi"": """ & "" & """, "
                jsonRequest &= """lokasiLaka"": { "
                jsonRequest &= """kdPropinsi"": """ & "" & """, "
                jsonRequest &= """kdKabupaten"": """ & "" & """, "
                jsonRequest &= """kdKecamatan"": """ & "" & """ "
            Else
                jsonRequest &= """lakaLantas"": """ & 1 & """, "
                jsonRequest &= """penjamin"": { "
                jsonRequest &= """tglKejadian"": """ & deDATE_PENJAMIN_TGLKEJADIAN.DateTime.ToString("yyyy-MM-dd") & """, "
                jsonRequest &= """keterangan"": """ & txtJAMINAN_PENJAMIN_KETERANGAN.Text.ToString.Trim.ToUpper & """, "
                jsonRequest &= """suplesi"": { "
                jsonRequest &= """suplesi"": """ & IIf(chkISSUPLESI.Checked = False, 0, 1) & """, "
                jsonRequest &= """noSepSuplesi"": """ & txtPENJAMIN_SUPLESI_NOSEPSUPLESI.Text.ToString.Trim.ToUpper & """, "
                jsonRequest &= """lokasiLaka"": { "
                jsonRequest &= """kdPropinsi"": """ & txtSUPLESI_PROPINSI.Text & """, "
                jsonRequest &= """kdKabupaten"": """ & txtSUPLESI_KABUPATEN.Text & """, "
                jsonRequest &= """kdKecamatan"": """ & txtSUPLESI_KECAMATAN.Text & """ "
            End If
            jsonRequest &= "} "
            jsonRequest &= "} "
            jsonRequest &= "} "
            jsonRequest &= "}, "
            jsonRequest &= """tujuanKunj"": """ & IIf(txtTUJUANKUNJUNGAN.Text = "", "", txtTUJUANKUNJUNGAN.SelectedIndex) & """, "
            jsonRequest &= """flagProcedure"": """ & IIf(txtFLAGPROCEDURE.Text = "", "", txtFLAGPROCEDURE.SelectedIndex) & """, "
            jsonRequest &= """kdPenunjang"": """ & IIf(txtKDPENUNJANG.Text = "", "", txtKDPENUNJANG.SelectedIndex + 1) & """, "
            jsonRequest &= """assesmentPel"": """ & IIf(txtASSEMENTPEL.Text = "", "", txtASSEMENTPEL.SelectedIndex + 1) & """, "
            jsonRequest &= """skdp"": { "
            jsonRequest &= """noSurat"": """ & txtNOMORSKDP.Text & """, "
            jsonRequest &= """kodeDPJP"": """ & oDoctor.GetData(grdKDDOCTOR.EditValue).VCLAIM_KDDPJP & """ "
            jsonRequest &= "}, "
            jsonRequest &= """dpjpLayan"": """ & IIf(rbCATEGORY.SelectedIndex = 0, IIf(rbCATEGORY.SelectedIndex = 0, oDoctor.GetData(grdKDDOCTOR_PELAYANAN.EditValue).VCLAIM_KDDPJP, ""), "") & """, "
            jsonRequest &= """noTelp"": """ & txtNOMORTELEPON.Text.ToString.Trim.ToUpper & """, "
            jsonRequest &= """user"": """ & sUserID & """ "
            jsonRequest &= "} "
            jsonRequest &= "} "
            jsonRequest &= "}  "
            fn_jsonRequestInsertSEPV2 = jsonRequest
        Catch oErr As Exception
            fn_jsonRequestInsertSEPV2 = ""
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Function
    Private Function fn_jsonRequestUpdateSEPV2() As String
        Try
            Dim jsonRequest As String = String.Empty
            Dim oFaskes As New Reference.clsPPK
            Dim oPoli As New Reference.clsDepartment
            Dim oDoctor As New Reference.clsDoctor
            Dim oDepartment As New Reference.clsDepartment

            jsonRequest = " { "
            jsonRequest &= """request"": { "
            jsonRequest &= """t_sep"": { "
            jsonRequest &= """noSep"": """ & txtNOMORSEP.Text & """, "
            jsonRequest &= """klsRawat"": { "
            jsonRequest &= """klsRawatHak"": """ & IIf(rbCATEGORY.SelectedIndex = 0, "3", grdKDKELASRAWAT.EditValue) & """, "
            jsonRequest &= """klsRawatNaik"": """ & IIf(grdKDKELASRAWAT.Text = grdKDKELASRAWAT_NAIKKELAS.Text, "", grdKDKELASRAWAT_NAIKKELAS.EditValue) & """, "
            jsonRequest &= """pembiayaan"": """ & IIf(grdKDKELASRAWAT.Text = grdKDKELASRAWAT_NAIKKELAS.Text, "", txtPEMBIAYAAN.SelectedIndex + 1) & """, "
            jsonRequest &= """penanggungJawab"": """ & IIf(grdKDKELASRAWAT.Text = grdKDKELASRAWAT_NAIKKELAS.Text, "", txtPENANGGUNGJAWAB.Text) & """ "
            jsonRequest &= " }, "
            jsonRequest &= """noMR"": """ & txtKDCUSTOMER.Text.ToString & """, "
            jsonRequest &= """catatan"": """ & txtCATATAN.Text.ToString.Trim.ToUpper & """, "
            jsonRequest &= """diagAwal"": """ & grdKDDIAGNOSA.EditValue & """, "
            jsonRequest &= """poli"" :  { "
            jsonRequest &= """tujuan"": """ & IIf(grdKDDEPARTMENT.Text = "DOTS", "PAR", IIf(grdKDDEPARTMENT.Text = "TUMBUH KEMBANG ANAK", "ANA", oDepartment.GetData(grdKDDEPARTMENT.EditValue).KDDEPARTMENT_BPJS)) & """, "
            jsonRequest &= """eksekutif"": """ & IIf(chkISEKSEKUTIF.Checked = False, 0, 1) & """ "
            jsonRequest &= "}, "
            jsonRequest &= """cob"": { "
            jsonRequest &= """cob"": """ & IIf(chkCOB.Checked = False, 0, 1) & """ "
            jsonRequest &= "}, "
            jsonRequest &= """katarak"":{ "
            jsonRequest &= """katarak"":""" & IIf(chkISKATARAK.Checked = False, 0, 1) & """ "
            jsonRequest &= "}, "
            jsonRequest &= """jaminan"": { "
            If chkLakaLantas.Checked = False Then
                jsonRequest &= """lakaLantas"": """ & 0 & """, "
                jsonRequest &= """penjamin"": { "
                jsonRequest &= """tglKejadian"": """ & "" & """, "
                jsonRequest &= """keterangan"": """ & "" & """, "
                jsonRequest &= """suplesi"": { "
                jsonRequest &= """suplesi"": """ & 0 & """, "
                jsonRequest &= """noSepSuplesi"": """ & "" & """, "
                jsonRequest &= """lokasiLaka"": { "
                jsonRequest &= """kdPropinsi"": """ & "" & """, "
                jsonRequest &= """kdKabupaten"": """ & "" & """, "
                jsonRequest &= """kdKecamatan"": """ & "" & """ "
            Else
                jsonRequest &= """lakaLantas"": """ & 1 & """, "
                jsonRequest &= """penjamin"": { "
                jsonRequest &= """tglKejadian"": """ & deDATE_PENJAMIN_TGLKEJADIAN.DateTime.ToString("yyyy-MM-dd") & """, "
                jsonRequest &= """keterangan"": """ & txtJAMINAN_PENJAMIN_KETERANGAN.Text.ToString.Trim.ToUpper & """, "
                jsonRequest &= """suplesi"": { "
                jsonRequest &= """suplesi"": """ & IIf(chkISSUPLESI.Checked = False, 0, 1) & """, "
                jsonRequest &= """noSepSuplesi"": """ & txtPENJAMIN_SUPLESI_NOSEPSUPLESI.Text.ToString.Trim.ToUpper & """, "
                jsonRequest &= """lokasiLaka"": { "
                jsonRequest &= """kdPropinsi"": """ & txtSUPLESI_PROPINSI.Text & """, "
                jsonRequest &= """kdKabupaten"": """ & txtSUPLESI_KABUPATEN.Text & """, "
                jsonRequest &= """kdKecamatan"": """ & txtSUPLESI_KECAMATAN.Text & """ "
            End If
            jsonRequest &= "} "
            jsonRequest &= "} "
            jsonRequest &= "} "
            jsonRequest &= "}, "
            jsonRequest &= """dpjpLayan"": """ & IIf(rbCATEGORY.SelectedIndex = 0, IIf(rbCATEGORY.SelectedIndex = 0, oDoctor.GetData(grdKDDOCTOR_PELAYANAN.EditValue).VCLAIM_KDDPJP, ""), "") & """, "
            jsonRequest &= """noTelp"": """ & txtNOMORTELEPON.Text.ToString.Trim.ToUpper & """, "
            jsonRequest &= """user"": """ & sUserID & """ "
            jsonRequest &= "} "
            jsonRequest &= "} "
            jsonRequest &= "} "

            fn_jsonRequestUpdateSEPV2 = jsonRequest
        Catch oErr As Exception
            fn_jsonRequestUpdateSEPV2 = ""
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Function
    Private Function fn_CreateSEP(ByVal Requset As String, ByVal uTime As Integer) As String
        Try
            Dim dsDataSetKoneksi = oSetKoneksi.GetData().FirstOrDefault(Function(x) x.ISACTIVE = True And x.NAME_DISPLAY = "VCLAIM")

            If dsDataSetKoneksi IsNot Nothing Then

                Dim dsSetKoneksi = oSetKoneksi.InsertSEP(dsDataSetKoneksi.ALAMATWEB, dsDataSetKoneksi.CONSID, dsDataSetKoneksi.SECREATKEY, dsDataSetKoneksi.USER_KEY, uTime, Requset)

                If dsSetKoneksi <> "" Then
                    Dim allData = JObject.Parse(dsSetKoneksi)

                    Dim CodeResponse As String = String.Empty
                    Dim messageResponse As String = String.Empty

                    CodeResponse = IIf(IsDBNull(allData.Item("metaData").Item("code")) = True, "", allData.Item("metaData").Item("code"))
                    messageResponse = allData("metaData")("message").ToString

                    If CodeResponse = "200" Then
                        fn_CreateSEP = allData("response")
                    Else
                        fn_CreateSEP = ""
                        Dim frmPesertaBPJS As New frmPesertaBPJS
                        frmPesertaBPJS.LoadMe(txtKDCUSTOMER.Text, CodeResponse & " - " & messageResponse)
                        frmPesertaBPJS.ShowDialog(Me)
                    End If
                Else
                    fn_CreateSEP = ""
                    MsgBox("Insert SEP Data Gagal/Kosong", MsgBoxStyle.Exclamation, Me.Text)
                End If
            Else
                fn_CreateSEP = ""
                MsgBox("Koneksi Tidak ditemukan", MsgBoxStyle.Exclamation, Me.Text)
            End If
        Catch oErr As Exception
            fn_CreateSEP = ""
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Function
    Private Function fn_UpdateSEP(ByVal Requset As String, ByVal uTime As Integer) As String
        Try
            If chkIsOfline.Checked = True Then
                MsgBox("Ofline di ceklis, silahkan buka ceklis untuk melanjutkan", MsgBoxStyle.Exclamation, Me.Text)
                fn_UpdateSEP = ""
                Exit Function
            End If

            Dim dsDataSetKoneksi = oSetKoneksi.GetData().FirstOrDefault(Function(x) x.ISACTIVE = True And x.NAME_DISPLAY = "VCLAIM")

            If dsDataSetKoneksi IsNot Nothing Then
                uTime = (DateTime.UtcNow - New DateTime(1970, 1, 1, 0, 0, 0)).TotalSeconds
                Dim dsSetKoneksi = oSetKoneksi.UpdateSEP(dsDataSetKoneksi.ALAMATWEB, dsDataSetKoneksi.CONSID, dsDataSetKoneksi.SECREATKEY, dsDataSetKoneksi.USER_KEY, uTime, Requset)

                If dsSetKoneksi <> "" Then
                    Dim allData = JObject.Parse(dsSetKoneksi)

                    Dim CodeResponse As String = String.Empty
                    Dim messageResponse As String = String.Empty

                    CodeResponse = IIf(IsDBNull(allData.Item("metaData").Item("code")) = True, "", allData.Item("metaData").Item("code"))
                    messageResponse = allData("metaData")("message").ToString

                    If CodeResponse = "200" Then
                        fn_UpdateSEP = allData("response")
                    Else
                        fn_UpdateSEP = ""
                        MsgBox(CodeResponse & " - " & messageResponse, MsgBoxStyle.Exclamation, Me.Text)
                    End If
                Else
                    fn_UpdateSEP = ""
                    MsgBox("Update SEP Data Gagal/Kosong", MsgBoxStyle.Exclamation, Me.Text)
                End If
            Else
                fn_UpdateSEP = ""
                MsgBox("Koneksi Tidak ditemukan", MsgBoxStyle.Exclamation, Me.Text)
            End If
        Catch oErr As Exception
            fn_UpdateSEP = ""
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Function
    Private Function fn_CariSEP(ByVal NOMORSEP As String, ByVal Pesan As Boolean) As Boolean
        Try
            If NOMORSEP = String.Empty Then
                fn_CariSEP = False
                Exit Function
            End If

            Dim dsDataSetKoneksi = oSetKoneksi.GetData().FirstOrDefault(Function(x) x.ISACTIVE = True And x.NAME_DISPLAY = "VCLAIM")
            Dim uTime As Integer = 0

            If dsDataSetKoneksi IsNot Nothing Then
                uTime = (DateTime.UtcNow - New DateTime(1970, 1, 1, 0, 0, 0)).TotalSeconds
                Dim dsSetKoneksi = oSetKoneksi.CariSEP(dsDataSetKoneksi.ALAMATWEB, dsDataSetKoneksi.CONSID, dsDataSetKoneksi.SECREATKEY, dsDataSetKoneksi.USER_KEY, uTime, NOMORSEP)

                If dsSetKoneksi <> "" Then
                    Dim allData = JObject.Parse(dsSetKoneksi)

                    Dim CodeResponse As String = String.Empty
                    Dim messageResponse As String = String.Empty

                    CodeResponse = IIf(IsDBNull(allData.Item("metaData").Item("code")) = True, "", allData.Item("metaData").Item("code"))
                    messageResponse = allData("metaData")("message").ToString

                    If CodeResponse = "200" Then
                        fn_CariSEP = True

                        If Pesan = True Then
                            Dim DataDecrypt = JObject.Parse(oSetKoneksi.Decrypt(allData("response"), dsDataSetKoneksi.CONSID & dsDataSetKoneksi.SECREATKEY & uTime))
                            MsgBox(DataDecrypt, MsgBoxStyle.Exclamation, Me.Text)
                        End If

                    Else
                        fn_CariSEP = False
                        If Pesan = True Then
                            MsgBox(CodeResponse & " - " & messageResponse, MsgBoxStyle.Exclamation, Me.Text)
                        End If
                    End If
                Else
                    fn_CariSEP = False
                    If Pesan = True Then
                        MsgBox("Insert SEP Data Gagal", MsgBoxStyle.Exclamation, Me.Text)
                    End If
                End If
            Else
                fn_CariSEP = False
                If Pesan = True Then
                    MsgBox("Cari SEP Data Gagal/Kosong", MsgBoxStyle.Exclamation, Me.Text)
                End If
            End If
        Catch oErr As Exception
            fn_CariSEP = False
            If Pesan = True Then
                fn_CariSEP = Statement.ErrorStatement & vbCrLf & oErr.Message
            End If
        End Try
    End Function
    Private Function fn_CreateSEPv2(ByVal Request As String, ByVal uTime As Integer) As String
        Try
            Dim dsDataSetKoneksi = oSetKoneksi.GetData().FirstOrDefault(Function(x) x.ISACTIVE = True And x.NAME_DISPLAY = "VCLAIM")

            If dsDataSetKoneksi IsNot Nothing Then
                Dim dsSetKoneksi = oSetKoneksi.InsertSEPv2(dsDataSetKoneksi.ALAMATWEB, dsDataSetKoneksi.CONSID, dsDataSetKoneksi.SECREATKEY, dsDataSetKoneksi.USER_KEY, uTime, Request)

                If dsSetKoneksi <> "" Then
                    Dim allData = JObject.Parse(dsSetKoneksi)

                    Dim CodeResponse As String = String.Empty
                    Dim messageResponse As String = String.Empty

                    CodeResponse = IIf(IsDBNull(allData.Item("metaData").Item("code")) = True, "", allData.Item("metaData").Item("code"))
                    messageResponse = allData("metaData")("message").ToString

                    If CodeResponse = "200" Then
                        fn_CreateSEPv2 = allData("response")
                    Else
                        fn_CreateSEPv2 = ""
                        MsgBox(CodeResponse & " - " & messageResponse, MsgBoxStyle.Exclamation, Me.Text)
                        Exit Function
                    End If
                Else
                    fn_CreateSEPv2 = ""
                    MsgBox("Insert SEP Data Gagal/Kosong", MsgBoxStyle.Exclamation, Me.Text)
                End If
            Else
                fn_CreateSEPv2 = ""
                MsgBox("Koneksi Tidak ditemukan", MsgBoxStyle.Exclamation, Me.Text)
            End If
        Catch oErr As Exception
            fn_CreateSEPv2 = ""
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Function
    Private Function fn_UpdateSEPv2(ByVal Request As String, ByVal uTime As Integer) As String
        Try
            If chkIsOfline.Checked = True Then
                MsgBox("Ofline di ceklis, silahkan buka ceklis untuk melanjutkan", MsgBoxStyle.Exclamation, Me.Text)
                fn_UpdateSEPv2 = ""
                Exit Function
            End If

            Dim dsDataSetKoneksi = oSetKoneksi.GetData().FirstOrDefault(Function(x) x.ISACTIVE = True And x.NAME_DISPLAY = "VCLAIM")

            If dsDataSetKoneksi IsNot Nothing Then
                Dim dsSetKoneksi = oSetKoneksi.UpdateSEPv2(dsDataSetKoneksi.ALAMATWEB, dsDataSetKoneksi.CONSID, dsDataSetKoneksi.SECREATKEY, dsDataSetKoneksi.USER_KEY, uTime, Request)

                If dsSetKoneksi <> "" Then
                    Dim allData = JObject.Parse(dsSetKoneksi)

                    Dim CodeResponse As String = String.Empty
                    Dim messageResponse As String = String.Empty

                    CodeResponse = IIf(IsDBNull(allData.Item("metaData").Item("code")) = True, "", allData.Item("metaData").Item("code"))
                    messageResponse = allData("metaData")("message").ToString

                    If CodeResponse = "200" Then
                        fn_UpdateSEPv2 = allData("response")
                    Else
                        fn_UpdateSEPv2 = ""
                        MsgBox(CodeResponse & " - " & messageResponse, MsgBoxStyle.Exclamation, Me.Text)
                        Exit Function
                    End If
                Else
                    fn_UpdateSEPv2 = ""
                    MsgBox("Update SEP Data Gagal/Kosong", MsgBoxStyle.Exclamation, Me.Text)
                End If
            Else
                fn_UpdateSEPv2 = ""
                MsgBox("Koneksi Tidak ditemukan", MsgBoxStyle.Exclamation, Me.Text)
            End If
        Catch oErr As Exception
            fn_UpdateSEPv2 = ""
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Function
    Private Function fn_Save() As Boolean
        Try
            Dim jsonRequest As String = String.Empty
            Dim jsonResponse As String = String.Empty
            Dim sINFORMASIPRB As String = String.Empty

            If sAktiveVersi2 = False Then
                ' ***** SAVE SEP V1

                If chkIsOfline.Checked = False Then
                    Dim uTime As Integer = (DateTime.UtcNow - New DateTime(1970, 1, 1, 0, 0, 0)).TotalSeconds

                    If oFormMode = FORM_MODE.FORM_MODE_ADD Then
                        jsonRequest = fn_jsonRequestInsertSEPV1()

                        If jsonRequest <> "" Then
                            jsonResponse = fn_CreateSEP(jsonRequest, uTime)
                            If jsonResponse <> "" Then
                                Dim dsDataSetKoneksi = oSetKoneksi.GetData().FirstOrDefault(Function(x) x.ISACTIVE = True And x.NAME_DISPLAY = "VCLAIM")
                                Dim DataDecrypt = JObject.Parse(oSetKoneksi.Decrypt(jsonResponse, dsDataSetKoneksi.CONSID & dsDataSetKoneksi.SECREATKEY & uTime))
                                txtNOMORSEP.Text = DataDecrypt.Item("sep")("noSep").ToString()
                                sINFORMASIPRB = DataDecrypt.Item("sep")("informasi")("prolanisPRB").ToString()
                            Else
                                fn_Save = False
                                Exit Function
                            End If
                        Else
                            fn_Save = False
                            Exit Function
                        End If
                    Else
                        jsonRequest = fn_jsonRequestUpdateSEPV1()
                        If jsonRequest <> "" Then
                            jsonResponse = fn_UpdateSEP(jsonRequest, uTime)
                            If jsonResponse <> "" Then
                                'Dim dsDataSetKoneksi = oSetKoneksi.GetData().FirstOrDefault(Function(x) x.ISACTIVE = True And x.NAME_DISPLAY = "VCLAIM")
                                'Dim DataDecrypt = JObject.Parse(oSetKoneksi.Decrypt(jsonResponse, dsDataSetKoneksi.CONSID & dsDataSetKoneksi.SECREATKEY & uTime))
                                'txtNOMORSEP.Text = DataDecrypt.Item("sep")("noSep").ToString()
                                'txtINFORMASIPRB.Text = DataDecrypt.Item("sep")("informasi")("prolanisPRB").ToString()
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
                    sINFORMASIPRB = String.Empty
                    If txtNOMORSEP.Text = "<--- AUTO --->" Then
                        txtNOMORSEP.ResetText()
                    End If
                End If
            Else
                ' ***** SAVE SEP V2

                If chkIsOfline.Checked = False Then
                    Dim uTime As Integer = (DateTime.UtcNow - New DateTime(1970, 1, 1, 0, 0, 0)).TotalSeconds

                    If oFormMode = FORM_MODE.FORM_MODE_ADD Then
                        jsonRequest = fn_jsonRequestInsertSEPV2()

                        If jsonRequest <> "" Then
                            jsonResponse = fn_CreateSEPv2(jsonRequest, uTime)
                            If jsonResponse <> "" Then
                                Dim dsDataSetKoneksi = oSetKoneksi.GetData().FirstOrDefault(Function(x) x.ISACTIVE = True And x.NAME_DISPLAY = "VCLAIM")
                                Dim DataDecrypt = JObject.Parse(oSetKoneksi.Decrypt(jsonResponse, dsDataSetKoneksi.CONSID & dsDataSetKoneksi.SECREATKEY & uTime))
                                txtNOMORSEP.Text = DataDecrypt.Item("sep")("noSep").ToString()
                                sINFORMASIPRB = DataDecrypt.Item("sep")("informasi")("prolanisPRB").ToString()
                            Else
                                fn_Save = False
                                Exit Function
                            End If
                        Else
                            fn_Save = False
                            Exit Function
                        End If
                    Else
                        jsonRequest = fn_jsonRequestUpdateSEPV2()
                        If jsonRequest <> "" Then
                            jsonResponse = fn_UpdateSEPv2(jsonRequest, uTime)
                            If jsonResponse <> "" Then
                                'Dim dsDataSetKoneksi = oSetKoneksi.GetData().FirstOrDefault(Function(x) x.ISACTIVE = True And x.NAME_DISPLAY = "VCLAIM")
                                'Dim DataDecrypt = JObject.Parse(oSetKoneksi.Decrypt(jsonResponse, dsDataSetKoneksi.CONSID & dsDataSetKoneksi.SECREATKEY & uTime))
                                'txtNOMORSEP.Text = DataDecrypt.Item("sep")("noSep").ToString()
                                'txtINFORMASIPRB.Text = DataDecrypt.Item("sep")("informasi")("prolanisPRB").ToString()
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
                    sINFORMASIPRB = String.Empty
                    If txtNOMORSEP.Text = "<--- AUTO --->" Then
                        txtNOMORSEP.ResetText()
                    End If
                End If
            End If

            ' ****************************************************

            ' ***** HEADER *****
            Dim oKunjungan_Poli As New Admission.clsPendaftaran_KunjunganPoli
            Dim oKunjungan_Ruangan As New Admission.clsPendaftaran_KunjunganRuangan

            Dim ds = oPendaftaran.GetStructureHeader
            With ds
                Try
                    .DATECREATED = oPendaftaran.GetData(sNoId).DATECREATED
                Catch oErr As Exception
                    .DATECREATED = Now
                End Try
                .DATEUPDATED = Now
                .KDPENDAFTARAN = sNoId
                .KDPERUSAHAAN = grdKDPERUSAHAAN.EditValue
                .KDPENDAFTARAN_AWAL = txtKDPENDAFTARAN_AWAL.Text
                If txtNOMORSEP.Text = "<--- AUTO --->" Then
                    txtNOMORSEP.Text = ""
                End If
                .NOMORSEP = txtNOMORSEP.Text
                .KDPENJAMIN = grdKDPENJAMIN.EditValue
                .KDDAFTAR_L1 = grdKDDAFTAR_L1.EditValue
                .KDDAFTAR_L2 = grdKDDAFTAR_L2.EditValue
                .KDDAFTAR_L3 = grdKDDAFTAR_L3.EditValue
                .KDDAFTAR_L4 = oPendaftaran.Daftar_L4_Default
                .KDDAFTAR_L5 = oPendaftaran.Daftar_L5_Default
                .KDDAFTAR_L6 = grdKDDAFTAR_L6.EditValue
                Try
                    .STATUSDAFTAR = oPendaftaran.GetData(sNoId).STATUSDAFTAR
                Catch oErr As Exception
                    .STATUSDAFTAR = 0
                End Try
                .NAMAKELUARGA = ""
                .KARTUBPJS = txtKARTUBPJS.Text.ToString.Trim.ToUpper
                .DATE = deDATE.DateTime
                .PPKPELAYANAN = PPKPELAYANAN
                .CATEGORY = rbCATEGORY.SelectedIndex
                .KDKELASRAWAT = grdKDKELASRAWAT.EditValue
                .KDCUSTOMER = txtKDCUSTOMER.Text.ToString.Trim.ToUpper
                .ASALRUJUKAN = cboASALRUJUKAN.SelectedIndex
                .DATE_RUJUKAN = deDATE_RUJUKAN.DateTime
                .NOMORRUJUKAN = txtNOMORRUJUKAN.Text.ToString.Trim.ToUpper
                .KDPPK = grdKDPPK.EditValue
                .CATATAN = txtCATATAN.Text.ToString.Trim.ToUpper
                .KDDIAGNOSA = grdKDDIAGNOSA.EditValue
                .KDDEPARTMENT = grdKDDEPARTMENT.EditValue
                .ISEKSEKUTIF = chkISEKSEKUTIF.Checked
                .ISCOB = chkCOB.Checked
                .ISKATARAK = chkISKATARAK.Checked
                If chkLakaLantas.Checked = False Then
                    .JAMINAN_ISLAKALANTAS = False
                    .JAMINAN_PENJAMIN_PENJAMIN1 = False
                    .JAMINAN_PENJAMIN_PENJAMIN2 = False
                    .JAMINAN_PENJAMIN_PENJAMIN3 = False
                    .JAMINAN_PENJAMIN_PENJAMIN4 = False
                    .JAMINAN_PENJAMIN_TGLKEJADIAN = Now
                    .JAMINAN_PENJAMIN_KETERANGAN = ""
                    .JAMINAN_PENJAMIN_SUPLESI_ISSUPLESI = False
                    .JAMINAN_PENJAMIN_SUPLESI_NOSEPSUPLESI = ""
                    .JAMINAN_PENJAMIN_SUPLESI_LOKASILAKA_KDPROPONSI = ""
                    .JAMINAN_PENJAMIN_SUPLESI_LOKASILAKA_KDKABUPATEN = ""
                    .JAMINAN_PENJAMIN_SUPLESI_LOKASILAKA_KDKECAMATAN = ""
                Else
                    .JAMINAN_ISLAKALANTAS = chkLakaLantas.Checked
                    .JAMINAN_PENJAMIN_PENJAMIN1 = chkPenjamin1.Checked
                    .JAMINAN_PENJAMIN_PENJAMIN2 = chkPenjamin2.Checked
                    .JAMINAN_PENJAMIN_PENJAMIN3 = chkPenjamin3.Checked
                    .JAMINAN_PENJAMIN_PENJAMIN4 = chkPenjamin4.Checked
                    .JAMINAN_PENJAMIN_TGLKEJADIAN = deDATE_PENJAMIN_TGLKEJADIAN.DateTime
                    .JAMINAN_PENJAMIN_KETERANGAN = txtJAMINAN_PENJAMIN_KETERANGAN.Text.Trim
                    .JAMINAN_PENJAMIN_SUPLESI_ISSUPLESI = chkISSUPLESI.Checked
                    .JAMINAN_PENJAMIN_SUPLESI_NOSEPSUPLESI = txtPENJAMIN_SUPLESI_NOSEPSUPLESI.Text.ToString.Trim.ToUpper
                    .JAMINAN_PENJAMIN_SUPLESI_LOKASILAKA_KDPROPONSI = txtSUPLESI_PROPINSI.Text
                    .JAMINAN_PENJAMIN_SUPLESI_LOKASILAKA_KDKABUPATEN = txtSUPLESI_KABUPATEN.Text
                    .JAMINAN_PENJAMIN_SUPLESI_LOKASILAKA_KDKECAMATAN = txtSUPLESI_KECAMATAN.Text
                End If
                .NOMORSKDP = txtNOMORSKDP.Text
                .KDDOCTOR = grdKDDOCTOR.EditValue
                .NOMORTELEPON = txtNOMORTELEPON.Text.ToString.Trim.ToUpper
                .KDUSER = sUserID
                .ISOFFLINE = chkIsOfline.Checked
                .REQUEST = jsonRequest
                .RESPON = jsonResponse
                .INFORMASIPRB = sINFORMASIPRB
                Try
                    .CETAK = oPendaftaran.GetData(sNoId).CETAK
                Catch ex As Exception
                    .CETAK = 1
                End Try
                .KDUPDATE_APLICARE = ""
                .EMAIL = ""
                .JALAN = txtJALAN.Text
                .PROPINSI = txtPROPINSI.Text
                .KOTA = txtKOTA.Text
                .KECAMATAN = txtKECAMATAN.Text
                .KELURAHAN = txtKELURAHAN.Text
                .KODEPOS = txtKODEPOS.Text
                .ISPASIENLAMA = chkLama.Checked
                .KDKELASRAWAT_NAIKKELAS = grdKDKELASRAWAT_NAIKKELAS.EditValue
                .PEMBIAYAAN = txtPEMBIAYAAN.Text
                .PENANGGUNGJAWAB = txtPENANGGUNGJAWAB.Text
                .TUJUANKUNJUNGAN = txtTUJUANKUNJUNGAN.Text
                .FLAGPROCEDURE = txtFLAGPROCEDURE.Text
                .KDPENUNJANG = txtKDPENUNJANG.Text
                .ASESMENTPEL = txtASSEMENTPEL.Text
                .KDDOCTOR_PELAYANAN = grdKDDOCTOR_PELAYANAN.EditValue
                .ISJAGA = chkISJAGA.Checked
                .USIA = oPendaftaran.GetUmurPasien(deDATE.DateTime, oPendaftaran.GetDataTanggalLahir(txtKDCUSTOMER.Text).TANGGALLAHIR)
                Try
                    .NOMORANTRIAN = oPendaftaran.GetData(sNoId).NOMORANTRIAN
                    .NOMORLABEL = oPendaftaran.GetData(sNoId).NOMORLABEL
                Catch ex As Exception
                    .NOMORANTRIAN = ""
                    .NOMORLABEL = ""
                End Try

            End With

            '***** Penanggung Jawab *****
            Dim dsPenanggunjawab = oPendaftaran.GetStructureHeader_PenanggungJawab
            With dsPenanggunjawab
                Try
                    .DATECREATED = oPendaftaran.GetData(sNoId).DATECREATED
                Catch oErr As Exception
                    .DATECREATED = Now
                End Try
                .DATEUPDATED = Now
                .KDPENDAFTARAN = ds.KDPENDAFTARAN
                .NAMA = txtNAMAPENANGGUNGJAWAB.Text.ToString.Trim.ToUpper
                .KDHUBUNGAN = grdKDHUBUNGAN.EditValue
                .ALAMAT = txtALAMATPENANGGUNGJAWAB.Text.ToString.ToString.ToUpper
                .NOMORTELEPON = txtNOMORTELEPONPENANGGUNGJAWAB.Text.ToString.Trim.ToUpper
                .KDCUSTOMER = txtKDCUSTOMER.Text
            End With

            If oFormMode = FORM_MODE.FORM_MODE_ADD Then
                Try
                    If txtKDPENDAFTARAN.Text = "<--- AUTO --->" Then
                        Dim oCounter As New Setting.clsCounter
                        Dim sMODUL As String = IIf(rbCATEGORY.SelectedIndex = 0, "RJ", "RI")
                        Dim sLASTNUMBER As Integer = oCounter.GetLastNumber(sMODUL, deDATE.DateTime)
                        If sLASTNUMBER = 0 Then
                            Try
                                oCounter.InsertData(sMODUL, deDATE.DateTime)
                                sLASTNUMBER = oCounter.GetLastNumber(sMODUL, deDATE.DateTime)
                            Catch ex As Exception
                                sLASTNUMBER = 0
                            End Try
                        End If

                        txtKDPENDAFTARAN.Text = AutoNumberSIMRS(sMODUL, sLASTNUMBER + 1, deDATE.DateTime)
                        oCounter.UpdateData(sMODUL, sLASTNUMBER + 1, Month(deDATE.DateTime), Year(deDATE.DateTime))
                    End If

                    fn_Save = oPendaftaran.InsertData(ds, dsPenanggunjawab, txtKDPENDAFTARAN.Text.ToString)

                    If rbCATEGORY.SelectedIndex = 0 Then
                        Dim dsKunjungan_Poli = oKunjungan_Poli.GetStructureHeader
                        With dsKunjungan_Poli
                            .DATECREATED = Now
                            .DATEUPDATED = Now
                            .KDKUNJUNGAN_POLI = ""
                            .KDPENDAFTARAN = txtKDPENDAFTARAN.Text
                            .KDDEPARTMENT = grdKDDEPARTMENT.EditValue
                            .DATE_MASUK = deDATE.DateTime
                            .DATE_KELUAR = deDATE.DateTime
                            .ISCHEKED = False
                            .MEMO = "POLI 1"
                            .KDUSER = sUserID
                            .KDPENJAMIN = grdKDPENJAMIN.EditValue
                            .KDDOCTOR = grdKDDOCTOR.EditValue
                            .KDPERUSAHAAN = grdKDPERUSAHAAN.EditValue
                        End With
                        Dim KDKUNJUNGAN As String = oKunjungan_Poli.InsertData(dsKunjungan_Poli, oPendaftaran.GetDataMemoDepartment(grdKDDEPARTMENT.EditValue).KDDEPARTMENT_BPJS & oPendaftaran.GetDataMemoDoctor(grdKDDOCTOR.EditValue).MEMO & IIf(chkISJAGA.Checked = False, "P", "S" & If(grdKDPENJAMIN.Text = "BPJS KESEHATAN", "B", "A")))
                        If fn_Save_simrsRJ(KDKUNJUNGAN) = False Then
                            MsgBox("Data Belum Masuk Ke SIMRS Lama", MsgBoxStyle.Exclamation, Me.Text)
                        End If
                        picPrintGelang_Farmasi(txtKDPENDAFTARAN.Text)
                        fn_SaveTracking(0)
                    Else
                        Dim dsKunjungan_Ruangan = oKunjungan_Ruangan.GetStructureHeader
                        With dsKunjungan_Ruangan
                            .DATECREATED = Now
                            .DATEUPDATED = Now
                            .KDKUNJUNGAN_RUANGAN = ""
                            .KDPENDAFTARAN = txtKDPENDAFTARAN.Text
                            .KDRUANGRAWAT = grdKDRUANGRAWAT.EditValue
                            .SEQ = grdTempatTidur.EditValue
                            .DATE_MASUK = deDATE.DateTime
                            .DATE_KELUAR = deDATE.DateTime
                            .ISCHEKED = False
                            .MEMO = "RUANGAN 1"
                            .KDUSER = sUserID
                            .KDPENJAMIN = grdKDPENJAMIN.EditValue
                            .KDDOCTOR = grdKDDOCTOR.EditValue
                            .KDPERUSAHAAN = grdKDPERUSAHAAN.EditValue
                        End With

                        If fn_Save_simrsRI(oKunjungan_Ruangan.InsertData(dsKunjungan_Ruangan, oKunjungan_Ruangan.GetData(grdKDRUANGRAWAT.EditValue).MEMO)) = False Then
                            MsgBox("Data Belum Masuk Ke SIMRS Lama", MsgBoxStyle.Exclamation, Me.Text)
                        End If

                        fn_SaveTracking(1)
                    End If

                    picPrintGelang_Antrian(txtKDPENDAFTARAN.Text)
                    picPrintGelang1(txtKDPENDAFTARAN.Text)
                    CetakRegister(txtKDPENDAFTARAN.Text)

                Catch oErr As Exception
                    MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            ElseIf oFormMode = FORM_MODE.FORM_MODE_EDIT Then
                Try
                    fn_Save = oPendaftaran.UpdateData(ds, dsPenanggunjawab)

                    If rbCATEGORY.SelectedIndex = 0 Then
                        Dim dsKunjungan_Poli = oKunjungan_Poli.GetStructureHeader

                        Dim dsKunjungan_DataPoli = oKunjungan_Poli.GetDataPoli1(txtKDPENDAFTARAN.Text)
                        If dsKunjungan_DataPoli IsNot Nothing Then

                            With dsKunjungan_Poli
                                .DATECREATED = dsKunjungan_DataPoli.DATECREATED
                                .DATEUPDATED = Now
                                .KDKUNJUNGAN_POLI = dsKunjungan_DataPoli.KDKUNJUNGAN_POLI
                                .KDPENDAFTARAN = txtKDPENDAFTARAN.Text
                                .KDDEPARTMENT = grdKDDEPARTMENT.EditValue
                                .KDDOCTOR = grdKDDOCTOR.EditValue
                                .DATE_MASUK = deDATE.DateTime
                                .DATE_KELUAR = deDATE.DateTime
                                .ISCHEKED = dsKunjungan_DataPoli.ISCHEKED
                                .MEMO = dsKunjungan_DataPoli.MEMO
                                .KDUSER = sUserID
                                .KDPENJAMIN = grdKDPENJAMIN.EditValue
                                .KDPERUSAHAAN = grdKDPERUSAHAAN.EditValue
                            End With
                            oKunjungan_Poli.UpdateData(dsKunjungan_Poli)

                            If fn_Save_simrsRJ(dsKunjungan_DataPoli.KDKUNJUNGAN_POLI) = False Then
                                MsgBox("Data Belum Masuk Ke SIMRS Lama", MsgBoxStyle.Exclamation, Me.Text)
                            End If
                        End If
                    Else
                        Dim dsKunjungan_Ruangan = oKunjungan_Ruangan.GetStructureHeader

                        Dim dsKunjungan_DataRuangan = oKunjungan_Ruangan.GetDataRuangan1(txtKDPENDAFTARAN.Text)
                        If dsKunjungan_DataRuangan IsNot Nothing Then

                            With dsKunjungan_Ruangan
                                .DATECREATED = dsKunjungan_DataRuangan.DATECREATED
                                .DATEUPDATED = Now
                                .KDKUNJUNGAN_RUANGAN = dsKunjungan_DataRuangan.KDKUNJUNGAN_RUANGAN
                                .KDPENDAFTARAN = txtKDPENDAFTARAN.Text
                                .KDRUANGRAWAT = grdKDRUANGRAWAT.EditValue
                                .KDDOCTOR = grdKDDOCTOR.EditValue
                                .DATE_MASUK = dsKunjungan_DataRuangan.DATE_MASUK
                                .DATE_KELUAR = dsKunjungan_DataRuangan.DATE_KELUAR
                                .ISCHEKED = dsKunjungan_DataRuangan.ISCHEKED
                                .MEMO = dsKunjungan_DataRuangan.MEMO
                                .KDUSER = sUserID
                                .KDPENJAMIN = grdKDPENJAMIN.EditValue
                                .SEQ = grdTempatTidur.EditValue
                                .KDPERUSAHAAN = grdKDPERUSAHAAN.EditValue
                            End With
                            oKunjungan_Ruangan.UpdateData(dsKunjungan_Ruangan)

                            If fn_Save_simrsRI(dsKunjungan_DataRuangan.KDKUNJUNGAN_RUANGAN) = False Then
                                MsgBox("Data Belum Masuk Ke SIMRS Lama", MsgBoxStyle.Exclamation, Me.Text)
                            End If

                        End If
                    End If

                Catch oErr As Exception
                    MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            End If

            oPendaftaran.UpdateCustomer(txtKDCUSTOMER.Text, txtNOMORTELEPON.Text, grdKDDAFTAR_L1.EditValue, grdKDDAFTAR_L2.EditValue, grdKDDAFTAR_L3.EditValue, grdKDDAFTAR_L6.EditValue, txtNAMAPENANGGUNGJAWAB.Text, grdKDHUBUNGAN.EditValue, txtALAMATPENANGGUNGJAWAB.Text, txtNOMORTELEPONPENANGGUNGJAWAB.Text)

        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            fn_Save = False
        End Try
    End Function
    Private Function fn_SaveTracking(ByVal Parameter As Integer) As Boolean
        Try
            ' ***** HEADER *****
            Dim oTracking As New Admission.clsTracking
            Dim oDepartment As New Reference.clsDepartment
            Dim oRuangan As New Reference.clsRuangRawat

            Dim ds = oTracking.GetStructureHeader
            With ds
                Try
                    .DATECREATED = oPendaftaran.GetData(sNoId).DATECREATED
                Catch oErr As Exception
                    .DATECREATED = Now
                End Try
                .DATEUPDATED = Now
                .KDTRACKING = sNoId
                .CATEGORY = Parameter
                .STATUS = "BELUM KIRIM"
                .KDPENDAFTARAN = txtKDPENDAFTARAN.Text
                .KDCUSTOMER = txtKDCUSTOMER.Text
                .JENIS_PASIEN = IIf(chkLama.Checked = False, "B", "L")
                .TUJUAN_SEKARANG = If(Parameter = 0, oDepartment.GetData(grdKDDEPARTMENT.EditValue).NAME_DISPLAY, oRuangan.GetData(grdKDRUANGRAWAT.EditValue).NAME_DISPLAY)
                Dim dsTracking = oTracking.GetDataOrderByDesc(txtKDCUSTOMER.Text)
                If dsTracking IsNot Nothing Then
                    .TUJUAN_SEBELUM = dsTracking.TUJUAN_SEKARANG
                    .DATE_SEBELUM = dsTracking.DATE_KIRIM
                Else
                    .TUJUAN_SEBELUM = If(Parameter = 0, oDepartment.GetData(grdKDDEPARTMENT.EditValue).NAME_DISPLAY, oRuangan.GetData(grdKDRUANGRAWAT.EditValue).NAME_DISPLAY)
                    .DATE_SEBELUM = Now
                End If
                .DATE_KIRIM = Now
                .DATE_KEMBALI = Now
                .ISCHEKED = False
                .MEMO = ""
                .KDUSER = sUserID
            End With

            If oFormMode = FORM_MODE.FORM_MODE_ADD Then
                fn_SaveTracking = oTracking.InsertData(ds)
            End If

        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            fn_SaveTracking = False
        End Try
    End Function
    Public Function AutoNumberSIMRS(ByVal sKDCOUNTER As String, ByVal sLASTNUMBER As Integer, ByVal sDATE As DateTime) As String
        Dim sMonth As String = String.Empty

        Select Case Month(sDATE)
            Case 1
                sMonth = "A"
            Case 2
                sMonth = "B"
            Case 3
                sMonth = "C"
            Case 4
                sMonth = "D"
            Case 5
                sMonth = "E"
            Case 6
                sMonth = "F"
            Case 7
                sMonth = "G"
            Case 8
                sMonth = "H"
            Case 9
                sMonth = "I"
            Case 10
                sMonth = "J"
            Case 11
                sMonth = "K"
            Case 12
                sMonth = "L"
        End Select

        If sLASTNUMBER < 10 Then
            AutoNumberSIMRS = sKDCOUNTER & Year(sDATE) & sMonth & "00000" & sLASTNUMBER.ToString
        ElseIf sLASTNUMBER < 100 Then
            AutoNumberSIMRS = sKDCOUNTER & Year(sDATE) & sMonth & "0000" & sLASTNUMBER.ToString
        ElseIf sLASTNUMBER < 1000 Then
            AutoNumberSIMRS = sKDCOUNTER & Year(sDATE) & sMonth & "000" & sLASTNUMBER.ToString
        ElseIf sLASTNUMBER < 10000 Then
            AutoNumberSIMRS = sKDCOUNTER & Year(sDATE) & sMonth & "00" & sLASTNUMBER.ToString
        ElseIf sLASTNUMBER < 100000 Then
            AutoNumberSIMRS = sKDCOUNTER & Year(sDATE) & sMonth & "0" & sLASTNUMBER.ToString
        Else
            AutoNumberSIMRS = sKDCOUNTER & Year(sDATE) & sMonth & sLASTNUMBER.ToString
        End If
    End Function
    Private Sub picPrintGelang_Antrian(ByVal sKDREG As String)
        Try
            Dim rpt As New xtraLabelGelang_New_01

            Dim oPendaftaran As New Admission.clsPendaftaran

            Dim dsPendaftaran = oPendaftaran.GetData(sKDREG)

            rpt.bindingSource.DataSource = dsPendaftaran

            Dim printTool As New DevExpress.XtraReports.UI.ReportPrintTool(rpt)

            Try
                printTool.PrinterSettings.PrinterName = "GELANG"
                printTool.PrinterSettings.Copies = 3
                printTool.PrintDialog()

            Catch ex As Exception
                printTool.PrintDialog()
            End Try

        Catch oErr As Exception
            MsgBox("Print Data : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub picPrintGelang_Farmasi(ByVal sKDREG As String)
        Try
            'Dim rpt As New xtraLabelGelang

            Dim rpt As New xtraLabelGelang_New_02

            Dim oPendaftaran As New Admission.clsPendaftaran

            Dim dsPendaftaran = oPendaftaran.GetData(sKDREG)

            rpt.bindingSource.DataSource = dsPendaftaran

            Dim printTool As New DevExpress.XtraReports.UI.ReportPrintTool(rpt)

            'printTool.ShowPreviewDialog(DevExpress.LookAndFeel.UserLookAndFeel.Default)
            Try
                printTool.PrinterSettings.PrinterName = "GELANG"
                printTool.PrinterSettings.Copies = 1
                printTool.PrintDialog()

            Catch ex As Exception
                printTool.PrintDialog()
            End Try

        Catch oErr As Exception
            MsgBox("Print Data : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub picPrintGelang1(ByVal sKDREG As String)
        Try
            Dim rpt As New xtraLabelGelang

            'Dim rpt As New xtraLabelGelang_New_01

            Dim oPendaftaran As New Admission.clsPendaftaran

            Dim dsPendaftaran = oPendaftaran.GetData(sKDREG)

            rpt.bindingSource.DataSource = dsPendaftaran

            Dim printTool As New DevExpress.XtraReports.UI.ReportPrintTool(rpt)

            'printTool.ShowPreviewDialog(DevExpress.LookAndFeel.UserLookAndFeel.Default)
            Try
                printTool.PrinterSettings.PrinterName = "GELANG"
                printTool.PrinterSettings.Copies = 1
                printTool.PrintDialog()

            Catch ex As Exception
                printTool.PrintDialog()
            End Try

        Catch oErr As Exception
            MsgBox("Print Data : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Function fn_Save_simrsRJ(ByVal KDKUNJUNGAN_POLI As String) As Boolean
        Try
            Dim oPendaftaranKunjungan As New Admission.clsPendaftaran_KunjunganPoli

            Dim dsKunjungan = oPendaftaranKunjungan.GetData(KDKUNJUNGAN_POLI)

            If dsKunjungan IsNot Nothing Then
                Dim oConn As New SqlConnection
                Dim oComm As New SqlCommand
                Dim da As SqlDataAdapter
                Dim ds As New DataSet
                Dim SQL As String
                Dim sConn As String = sConnOld
                oConn = New SqlConnection(sConn)

                If oConn.State = ConnectionState.Closed Then
                    oConn.Open()
                End If

                SQL = "SELECT *  "
                SQL &= "FROM S_PENDAFTARAN_H "
                SQL &= "WHERE "
                SQL &= "KDREG = '" & dsKunjungan.KDPENDAFTARAN & "' "

                oComm.Connection = oConn
                oComm.CommandText = SQL
                oComm.CommandTimeout = 120
                oComm.CommandType = CommandType.Text

                da = New SqlDataAdapter(oComm)
                da.Fill(ds, "GETDATA_PENDAFTARAN")

                If ds.Tables("GETDATA_PENDAFTARAN").Rows.Count > 0 Then
                    fn_Save_simrsRJ = True
                    Exit Function
                End If
                SQL = "INSERT INTO "
                SQL &= "S_PENDAFTARAN_H "
                SQL &= "( "
                SQL &= "KDREG "
                SQL &= ",DATECREATED "
                SQL &= ",DATEUPDATED "
                SQL &= ",DATE "
                SQL &= ",KDDOCTOR "
                SQL &= ",PRIORITAS "
                SQL &= ",PENDAFTARANVIA "
                SQL &= ",CATATAN "
                SQL &= ",KDDEBTOR "
                SQL &= ",JENISASURANSI "
                SQL &= ",KARTUBPJS "
                SQL &= ",KDGROUPCUSTOMER "
                SQL &= ",NIK "
                SQL &= ",NAMA_PJ "
                SQL &= ",NIK_PJ "
                SQL &= ",ASALRUJUKAN "
                SQL &= ",NOMORRUJUKAN "
                SQL &= ",CATEGORY "
                SQL &= ",NOIDUSER "
                SQL &= ",RUJUKAN "
                SQL &= ",KDDEPARTMENT "
                SQL &= ",ADDRESS_STREET "
                SQL &= ",PHONE "
                SQL &= ",NAMA_WALI "
                SQL &= ",HUBUNGAN "
                SQL &= ",ADDRESS_WALI "
                SQL &= ",PHONE_WALI "
                SQL &= ",DOKTER_RUJUKAN "
                SQL &= ",DIKIRIM_MELALUI "
                SQL &= ",KDDIAGNOSA "
                SQL &= ",TANGGALMASUK "
                SQL &= ",KELASRAWAT "
                SQL &= ",KDMROOM "
                SQL &= ",JENISDIET "
                SQL &= ",KETERANGANDIET "
                SQL &= ",UANGMUKA "
                SQL &= ",KETRANAP "
                SQL &= ",BERASALDARI "
                SQL &= ",KETHUBUNGAN "
                SQL &= ",KDCUSTOMER "
                SQL &= ",ISDOKUMENRM "
                SQL &= ",STATUSDAFTAR "
                SQL &= ",GRANDTOTAL "
                SQL &= ",PAYAMOUNT "
                SQL &= ",ISUPDATEPULANG "
                SQL &= ",DATEPULANG "
                SQL &= ",USIA "
                SQL &= ",WAKTUDOKUMENRM "
                SQL &= ",ISPROLANIS "
                SQL &= ",KDSKDP "
                SQL &= ",KDKELASRAWAT "
                SQL &= ",NOMORSEP "
                SQL &= ",KDPESERTA "
                SQL &= ",KDASALRUJUKAN "
                SQL &= ",KDPPK "
                SQL &= ",DATERUJUKAN "
                SQL &= ",ISEKSEKUTIF "
                SQL &= ",ISKATARAK "
                SQL &= ",ISCOB "
                SQL &= ",ISPENJAMINKKL "
                SQL &= ",ISJASARAHARJA "
                SQL &= ",ISKETENAGAKERJAAN "
                SQL &= ",ISASABRI "
                SQL &= ",ISSUPLESI "
                SQL &= ",ISTASPEN "
                SQL &= ",TANGGALKEJADIAN "
                SQL &= ",KODEPROV "
                SQL &= ",KODEKOTA "
                SQL &= ",KODEKEC "
                SQL &= ",KETERANGANKKL "
                SQL &= ",REQUEST "
                SQL &= ",RESPONSE "
                SQL &= ",KDCOB "
                SQL &= ",NOSUPLESI "
                SQL &= ",TYPE "
                SQL &= ",STATUSBPJS "
                SQL &= ",ISOFFLINE "
                SQL &= ",INFORMASIBPJS "
                SQL &= ",CETAK "
                SQL &= ",KONSUL "
                SQL &= ",DOKTERKONSUL "
                SQL &= ",KDREGAWAL "
                SQL &= ",ISNAIKRANAP "
                SQL &= ",NOMORANTRIAN "
                SQL &= ") "
                SQL &= "VALUES "
                SQL &= "( "
                SQL &= "'" & dsKunjungan.KDPENDAFTARAN & "' "
                SQL &= ",'" & dsKunjungan.DATECREATED.ToString("yyyy-MM-dd HH:mm:ss") & "' "
                SQL &= ",'" & dsKunjungan.DATEUPDATED.ToString("yyyy-MM-dd HH:mm:ss") & "' "
                SQL &= ",'" & dsKunjungan.DATE_MASUK.ToString("yyyy-MM-dd HH:mm:ss") & "' "
                SQL &= ",'" & dsKunjungan.KDDOCTOR & "' "
                SQL &= ",'" & dsKunjungan.S_PENDAFTARAN_H.KDDAFTAR_L6 & "' "
                SQL &= ",'" & dsKunjungan.S_PENDAFTARAN_H.KDDAFTAR_L4 & "' "
                SQL &= ",'" & dsKunjungan.S_PENDAFTARAN_H.CATATAN & "' "
                SQL &= ",'" & dsKunjungan.KDPENJAMIN & "' "
                SQL &= ",'" & dsKunjungan.S_PENDAFTARAN_H.M_PERUSAHAAN.MEMO & "' "
                SQL &= ",'" & dsKunjungan.S_PENDAFTARAN_H.KARTUBPJS & "' "
                SQL &= ",'" & dsKunjungan.S_PENDAFTARAN_H.KDPERUSAHAAN & "' "
                SQL &= ",'" & dsKunjungan.S_PENDAFTARAN_H.M_CUSTOMER.KTP & "' "
                SQL &= ",'" & dsKunjungan.S_PENDAFTARAN_H.PENANGGUNGJAWAB & "' "
                SQL &= ",'' "
                SQL &= ",'" & dsKunjungan.S_PENDAFTARAN_H.M_PPK.MEMO & "' "
                SQL &= ",'" & dsKunjungan.S_PENDAFTARAN_H.NOMORRUJUKAN & "' "
                SQL &= ",'1' "
                SQL &= ",'" & dsKunjungan.S_PENDAFTARAN_H.KDUSER & "' "
                SQL &= ",'" & dsKunjungan.S_PENDAFTARAN_H.KDDAFTAR_L2 & "' "
                SQL &= ",'" & dsKunjungan.KDDEPARTMENT & "' "
                SQL &= ",'" & dsKunjungan.S_PENDAFTARAN_H.JALAN & "' "
                SQL &= ",'" & dsKunjungan.S_PENDAFTARAN_H.NOMORTELEPON & "' "
                SQL &= ",'' "
                SQL &= ",'' "
                SQL &= ",'' "
                SQL &= ",'' "
                SQL &= ",'' "
                SQL &= ",'' "
                SQL &= ",'" & dsKunjungan.S_PENDAFTARAN_H.KDDIAGNOSA & "' "
                SQL &= ",'" & dsKunjungan.DATE_MASUK.ToString("yyyy-MM-dd HH:mm:ss") & "' "
                SQL &= ",'' "
                SQL &= ",'1' "
                SQL &= ",'' "
                SQL &= ",'' "
                SQL &= ",0 "
                SQL &= ",'' "
                SQL &= ",'1' "
                SQL &= ",'' "
                SQL &= ",'" & dsKunjungan.S_PENDAFTARAN_H.KDCUSTOMER & "' "
                SQL &= ",'0' "
                SQL &= ",'1' "
                SQL &= ",0 "
                SQL &= ",0 "
                SQL &= ",'0' "
                SQL &= ",'" & dsKunjungan.DATE_KELUAR.ToString("yyyy-MM-dd HH:mm:ss") & "' "
                SQL &= ",'" & dsKunjungan.S_PENDAFTARAN_H.USIA & "' "
                SQL &= ",'' "
                SQL &= ",'0' "
                SQL &= ",'" & dsKunjungan.S_PENDAFTARAN_H.NOMORSKDP & "' "
                SQL &= ",'" & dsKunjungan.S_PENDAFTARAN_H.KDKELASRAWAT & "' "
                SQL &= ",'" & dsKunjungan.S_PENDAFTARAN_H.NOMORSEP & "' "
                SQL &= ",'-' "
                SQL &= ",'1' "
                SQL &= ",'" & dsKunjungan.S_PENDAFTARAN_H.M_PPK.KODEFASKES & "' "
                SQL &= ",'" & dsKunjungan.S_PENDAFTARAN_H.DATE_RUJUKAN.ToString("yyyy-MM-dd HH:mm:ss") & "' "
                SQL &= ",'" & dsKunjungan.S_PENDAFTARAN_H.ISEKSEKUTIF & "' "
                SQL &= ",'" & dsKunjungan.S_PENDAFTARAN_H.ISKATARAK & "' "
                SQL &= ",'" & dsKunjungan.S_PENDAFTARAN_H.ISCOB & "' "
                SQL &= ",'0' "
                SQL &= ",'0' "
                SQL &= ",'0' "
                SQL &= ",'0' "
                SQL &= ",'0' "
                SQL &= ",'0' "
                SQL &= ",'" & dsKunjungan.DATE_MASUK.ToString("yyyy-MM-dd HH:mm:ss") & "' "
                SQL &= ",'0000' "
                SQL &= ",'0000' "
                SQL &= ",'0000' "
                SQL &= ",'' "
                SQL &= ",'" & dsKunjungan.S_PENDAFTARAN_H.REQUEST & "' "
                SQL &= ",'" & dsKunjungan.S_PENDAFTARAN_H.RESPON & "' "
                SQL &= ",'' "
                SQL &= ",'" & dsKunjungan.S_PENDAFTARAN_H.JAMINAN_PENJAMIN_SUPLESI_NOSEPSUPLESI & "' "
                SQL &= ",'' "
                SQL &= ",'' "
                SQL &= ",'" & dsKunjungan.S_PENDAFTARAN_H.ISOFFLINE & "' "
                SQL &= ",'" & dsKunjungan.S_PENDAFTARAN_H.INFORMASIPRB & "' "
                SQL &= ",'1' "
                SQL &= ",'2' "
                SQL &= ",'9' "
                SQL &= ",'" & dsKunjungan.S_PENDAFTARAN_H.KDPENDAFTARAN_AWAL & "' "
                SQL &= ",'0' "
                SQL &= ",'" & dsKunjungan.KDKUNJUNGAN_POLI & "' "
                SQL &= ") "

                oComm.Connection = oConn
                oComm.CommandText = SQL
                oComm.CommandTimeout = 120
                oComm.CommandType = CommandType.Text

                da = New SqlDataAdapter(oComm)
                da.Fill(ds, "INSERTSET_PENDAFTARAN")

                fn_Save_simrsRJ = True

                If oConn.State = ConnectionState.Open Then
                    oConn.Close()
                End If
            Else
                fn_Save_simrsRJ = False
            End If
        Catch oErr As Exception
            fn_Save_simrsRJ = False
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Function
    Private Function fn_Save_simrsRI(ByVal KDKUNJUNGAN_RUANGAN As String) As Boolean
        Try
            Dim oPendaftaranKunjungan As New Admission.clsPendaftaran_KunjunganRuangan

            Dim dsKunjungan = oPendaftaranKunjungan.GetData(KDKUNJUNGAN_RUANGAN)

            If dsKunjungan IsNot Nothing Then
                Dim oConn As New SqlConnection
                Dim oComm As New SqlCommand
                Dim da As SqlDataAdapter
                Dim ds As New DataSet
                Dim SQL As String
                Dim sConn As String = sConnOld
                oConn = New SqlConnection(sConn)

                If oConn.State = ConnectionState.Closed Then
                    oConn.Open()
                End If

                SQL = "SELECT *  "
                SQL &= "FROM S_PENDAFTARAN_H "
                SQL &= "WHERE "
                SQL &= "KDREG = '" & dsKunjungan.KDPENDAFTARAN & "' "

                oComm.Connection = oConn
                oComm.CommandText = SQL
                oComm.CommandTimeout = 120
                oComm.CommandType = CommandType.Text

                da = New SqlDataAdapter(oComm)
                da.Fill(ds, "GETDATA_PENDAFTARAN")

                If ds.Tables("GETDATA_PENDAFTARAN").Rows.Count > 0 Then
                    fn_Save_simrsRI = True
                    Exit Function
                End If
                SQL = "INSERT INTO "
                SQL &= "S_PENDAFTARAN_H "
                SQL &= "( "
                SQL &= "KDREG "
                SQL &= ",DATECREATED "
                SQL &= ",DATEUPDATED "
                SQL &= ",DATE "
                SQL &= ",KDDOCTOR "
                SQL &= ",PRIORITAS "
                SQL &= ",PENDAFTARANVIA "
                SQL &= ",CATATAN "
                SQL &= ",KDDEBTOR "
                SQL &= ",JENISASURANSI "
                SQL &= ",KARTUBPJS "
                SQL &= ",KDGROUPCUSTOMER "
                SQL &= ",NIK "
                SQL &= ",NAMA_PJ "
                SQL &= ",NIK_PJ "
                SQL &= ",ASALRUJUKAN "
                SQL &= ",NOMORRUJUKAN "
                SQL &= ",CATEGORY "
                SQL &= ",NOIDUSER "
                SQL &= ",RUJUKAN "
                SQL &= ",KDDEPARTMENT "
                SQL &= ",ADDRESS_STREET "
                SQL &= ",PHONE "
                SQL &= ",NAMA_WALI "
                SQL &= ",HUBUNGAN "
                SQL &= ",ADDRESS_WALI "
                SQL &= ",PHONE_WALI "
                SQL &= ",DOKTER_RUJUKAN "
                SQL &= ",DIKIRIM_MELALUI "
                SQL &= ",KDDIAGNOSA "
                SQL &= ",TANGGALMASUK "
                SQL &= ",KELASRAWAT "
                SQL &= ",KDMROOM "
                SQL &= ",JENISDIET "
                SQL &= ",KETERANGANDIET "
                SQL &= ",UANGMUKA "
                SQL &= ",KETRANAP "
                SQL &= ",BERASALDARI "
                SQL &= ",KETHUBUNGAN "
                SQL &= ",KDCUSTOMER "
                SQL &= ",ISDOKUMENRM "
                SQL &= ",STATUSDAFTAR "
                SQL &= ",GRANDTOTAL "
                SQL &= ",PAYAMOUNT "
                SQL &= ",ISUPDATEPULANG "
                SQL &= ",DATEPULANG "
                SQL &= ",USIA "
                SQL &= ",WAKTUDOKUMENRM "
                SQL &= ",ISPROLANIS "
                SQL &= ",KDSKDP "
                SQL &= ",KDKELASRAWAT "
                SQL &= ",NOMORSEP "
                SQL &= ",KDPESERTA "
                SQL &= ",KDASALRUJUKAN "
                SQL &= ",KDPPK "
                SQL &= ",DATERUJUKAN "
                SQL &= ",ISEKSEKUTIF "
                SQL &= ",ISKATARAK "
                SQL &= ",ISCOB "
                SQL &= ",ISPENJAMINKKL "
                SQL &= ",ISJASARAHARJA "
                SQL &= ",ISKETENAGAKERJAAN "
                SQL &= ",ISASABRI "
                SQL &= ",ISSUPLESI "
                SQL &= ",ISTASPEN "
                SQL &= ",TANGGALKEJADIAN "
                SQL &= ",KODEPROV "
                SQL &= ",KODEKOTA "
                SQL &= ",KODEKEC "
                SQL &= ",KETERANGANKKL "
                SQL &= ",REQUEST "
                SQL &= ",RESPONSE "
                SQL &= ",KDCOB "
                SQL &= ",NOSUPLESI "
                SQL &= ",TYPE "
                SQL &= ",STATUSBPJS "
                SQL &= ",ISOFFLINE "
                SQL &= ",INFORMASIBPJS "
                SQL &= ",CETAK "
                SQL &= ",KONSUL "
                SQL &= ",DOKTERKONSUL "
                SQL &= ",KDREGAWAL "
                SQL &= ",ISNAIKRANAP "
                SQL &= ",NOMORANTRIAN "
                SQL &= ") "
                SQL &= "VALUES "
                SQL &= "( "
                SQL &= "'" & dsKunjungan.KDPENDAFTARAN & "' "
                SQL &= ",'" & dsKunjungan.DATECREATED.ToString("yyyy-MM-dd HH:mm:ss") & "' "
                SQL &= ",'" & dsKunjungan.DATEUPDATED.ToString("yyyy-MM-dd HH:mm:ss") & "' "
                SQL &= ",'" & dsKunjungan.DATE_MASUK.ToString("yyyy-MM-dd HH:mm:ss") & "' "
                SQL &= ",'" & dsKunjungan.KDDOCTOR & "' "
                SQL &= ",'" & dsKunjungan.S_PENDAFTARAN_H.KDDAFTAR_L6 & "' "
                SQL &= ",'" & dsKunjungan.S_PENDAFTARAN_H.KDDAFTAR_L4 & "' "
                SQL &= ",'" & dsKunjungan.S_PENDAFTARAN_H.CATATAN & "' "
                SQL &= ",'" & dsKunjungan.KDPENJAMIN & "' "
                SQL &= ",'" & dsKunjungan.S_PENDAFTARAN_H.M_PERUSAHAAN.MEMO & "' "
                SQL &= ",'" & dsKunjungan.S_PENDAFTARAN_H.KARTUBPJS & "' "
                SQL &= ",'" & dsKunjungan.S_PENDAFTARAN_H.KDPERUSAHAAN & "' "
                SQL &= ",'" & dsKunjungan.S_PENDAFTARAN_H.M_CUSTOMER.KTP & "' "
                SQL &= ",'" & dsKunjungan.S_PENDAFTARAN_H.PENANGGUNGJAWAB & "' "
                SQL &= ",'' "
                SQL &= ",'" & dsKunjungan.S_PENDAFTARAN_H.M_PPK.MEMO & "' "
                SQL &= ",'" & dsKunjungan.S_PENDAFTARAN_H.NOMORRUJUKAN & "' "
                SQL &= ",'1' "
                SQL &= ",'" & dsKunjungan.S_PENDAFTARAN_H.KDUSER & "' "
                SQL &= ",'" & dsKunjungan.S_PENDAFTARAN_H.KDDAFTAR_L2 & "' "
                SQL &= ",'" & dsKunjungan.S_PENDAFTARAN_H.KDDEPARTMENT & "' "
                SQL &= ",'" & dsKunjungan.S_PENDAFTARAN_H.JALAN & "' "
                SQL &= ",'" & dsKunjungan.S_PENDAFTARAN_H.NOMORTELEPON & "' "
                SQL &= ",'' "
                SQL &= ",'' "
                SQL &= ",'' "
                SQL &= ",'' "
                SQL &= ",'' "
                SQL &= ",'' "
                SQL &= ",'" & dsKunjungan.S_PENDAFTARAN_H.KDDIAGNOSA & "' "
                SQL &= ",'" & dsKunjungan.DATE_MASUK.ToString("yyyy-MM-dd HH:mm:ss") & "' "
                SQL &= ",'' "
                SQL &= ",'1' "
                SQL &= ",'' "
                SQL &= ",'' "
                SQL &= ",0 "
                SQL &= ",'' "
                SQL &= ",'1' "
                SQL &= ",'' "
                SQL &= ",'" & dsKunjungan.S_PENDAFTARAN_H.KDCUSTOMER & "' "
                SQL &= ",'0' "
                SQL &= ",'1' "
                SQL &= ",0 "
                SQL &= ",0 "
                SQL &= ",'0' "
                SQL &= ",'" & dsKunjungan.DATE_KELUAR.ToString("yyyy-MM-dd HH:mm:ss") & "' "
                SQL &= ",'" & dsKunjungan.S_PENDAFTARAN_H.USIA & "' "
                SQL &= ",'' "
                SQL &= ",'0' "
                SQL &= ",'" & dsKunjungan.S_PENDAFTARAN_H.NOMORSKDP & "' "
                SQL &= ",'" & dsKunjungan.S_PENDAFTARAN_H.KDKELASRAWAT & "' "
                SQL &= ",'" & dsKunjungan.S_PENDAFTARAN_H.NOMORSEP & "' "
                SQL &= ",'-' "
                SQL &= ",'1' "
                SQL &= ",'" & dsKunjungan.S_PENDAFTARAN_H.M_PPK.KODEFASKES & "' "
                SQL &= ",'" & dsKunjungan.S_PENDAFTARAN_H.DATE_RUJUKAN.ToString("yyyy-MM-dd HH:mm:ss") & "' "
                SQL &= ",'" & dsKunjungan.S_PENDAFTARAN_H.ISEKSEKUTIF & "' "
                SQL &= ",'" & dsKunjungan.S_PENDAFTARAN_H.ISKATARAK & "' "
                SQL &= ",'" & dsKunjungan.S_PENDAFTARAN_H.ISCOB & "' "
                SQL &= ",'0' "
                SQL &= ",'0' "
                SQL &= ",'0' "
                SQL &= ",'0' "
                SQL &= ",'0' "
                SQL &= ",'0' "
                SQL &= ",'" & dsKunjungan.DATE_MASUK.ToString("yyyy-MM-dd HH:mm:ss") & "' "
                SQL &= ",'0000' "
                SQL &= ",'0000' "
                SQL &= ",'0000' "
                SQL &= ",'' "
                SQL &= ",'" & dsKunjungan.S_PENDAFTARAN_H.REQUEST & "' "
                SQL &= ",'" & dsKunjungan.S_PENDAFTARAN_H.RESPON & "' "
                SQL &= ",'' "
                SQL &= ",'" & dsKunjungan.S_PENDAFTARAN_H.JAMINAN_PENJAMIN_SUPLESI_NOSEPSUPLESI & "' "
                SQL &= ",'' "
                SQL &= ",'' "
                SQL &= ",'" & dsKunjungan.S_PENDAFTARAN_H.ISOFFLINE & "' "
                SQL &= ",'" & dsKunjungan.S_PENDAFTARAN_H.INFORMASIPRB & "' "
                SQL &= ",'1' "
                SQL &= ",'2' "
                SQL &= ",'9' "
                SQL &= ",'" & dsKunjungan.S_PENDAFTARAN_H.KDPENDAFTARAN_AWAL & "' "
                SQL &= ",'0' "
                SQL &= ",'" & dsKunjungan.KDKUNJUNGAN_RUANGAN & "' "
                SQL &= ") "

                oComm.Connection = oConn
                oComm.CommandText = SQL
                oComm.CommandTimeout = 120
                oComm.CommandType = CommandType.Text

                da = New SqlDataAdapter(oComm)
                da.Fill(ds, "INSERTSET_PENDAFTARAN")

                fn_Save_simrsRI = True

                If oConn.State = ConnectionState.Open Then
                    oConn.Close()
                End If
            Else
                fn_Save_simrsRI = False
            End If
        Catch oErr As Exception
            fn_Save_simrsRI = False
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Function
#End Region
#Region "Command Button"
    Private Sub frmPendaftaran_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
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
            Case Keys.F5
                If btnAddCustomer.Enabled = True Then
                    btnAddCustomer_Click()
                End If
            Case Keys.F6
                If btnSuratKontrol.Enabled = True Then
                    btnSuratKontrol_Click()
                End If
            Case Keys.F8
                If btnCreateSEP.Enabled = True Then
                    btnCreateSEP_Click()
                End If
            Case Keys.F9
                If btnCariPasien.Enabled = True Then
                    btnCariPasien_Click()
                End If
        End Select
    End Sub
    Private Sub btnListFinger_Click() Handles btnListFinger.ItemClick
        Try
            Dim dsDataSetKoneksi = oSetKoneksi.GetData().FirstOrDefault(Function(x) x.ISACTIVE = True And x.NAME_DISPLAY = "VCLAIM")
            Dim uTime As Integer = 0

            If dsDataSetKoneksi IsNot Nothing Then
                uTime = (DateTime.UtcNow - New DateTime(1970, 1, 1, 0, 0, 0)).TotalSeconds
                Dim dsSetKoneksi = oSetKoneksi.GetListFingerPrint(dsDataSetKoneksi.ALAMATWEB, dsDataSetKoneksi.CONSID, dsDataSetKoneksi.SECREATKEY, dsDataSetKoneksi.USER_KEY, uTime, Now.ToString("yyyy-MM-dd"))

                If dsSetKoneksi <> "" Then
                    Dim allData = JObject.Parse(dsSetKoneksi)

                    Dim CodeResponse As String = String.Empty
                    Dim messageResponse As String = String.Empty

                    CodeResponse = IIf(IsDBNull(allData.Item("metaData").Item("code")) = True, "", allData.Item("metaData").Item("code"))
                    messageResponse = allData("metaData")("message").ToString

                    If CodeResponse = "200" Then
                        Dim DataDecrypt = JObject.Parse(oSetKoneksi.Decrypt(allData("response"), dsDataSetKoneksi.CONSID & dsDataSetKoneksi.SECREATKEY & uTime))
                        MsgBox(CodeResponse & " - " & DataDecrypt("list").ToString(), MsgBoxStyle.Exclamation, Me.Text)
                    Else
                        MsgBox(CodeResponse & " - " & messageResponse, MsgBoxStyle.Exclamation, Me.Text)
                    End If
                Else
                    MsgBox("Kosong Koneksi", MsgBoxStyle.Exclamation, Me.Text)
                End If
            Else
                MsgBox("Cari Finger Print Data Gagal/Kosong", MsgBoxStyle.Exclamation, Me.Text)
            End If
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub btnCariPasien_Click() Handles btnCariPasien.ItemClick
        Try
            frmBrowsePasien.ShowDialog(Me)
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        Finally
            If Not frmBrowsePasien Is Nothing Then frmBrowsePasien.Dispose()
            frmBrowsePasien = Nothing
        End Try

        If sCode <> "" Then
            fn_LoadCustomer(sCode)
        End If
    End Sub
    Private Sub btnCreateSEP_Click() Handles btnCreateSEP.ItemClick
        If oFormMode = FORM_MODE.FORM_MODE_EDIT Then
            Dim dsDaftar = oPendaftaran.GetData(txtKDPENDAFTARAN.Text.ToString)
            If dsDaftar IsNot Nothing Then
                If sAktiveVersi2 = False Then
                    Try
                        Dim uTime As Integer = (DateTime.UtcNow - New DateTime(1970, 1, 1, 0, 0, 0)).TotalSeconds
                        Dim jsonRequest As String = String.Empty
                        Dim jsonResponse As String = String.Empty

                        jsonRequest = fn_jsonRequestInsertSEPV1()

                        If jsonRequest <> "" Then
                            jsonResponse = fn_CreateSEP(jsonRequest, uTime)
                            If jsonResponse <> "" Then
                                Dim dsDataSetKoneksi = oSetKoneksi.GetData().FirstOrDefault(Function(x) x.ISACTIVE = True And x.NAME_DISPLAY = "VCLAIM")
                                Dim DataDecrypt = JObject.Parse(oSetKoneksi.Decrypt(jsonResponse, dsDataSetKoneksi.CONSID & dsDataSetKoneksi.SECREATKEY & uTime))
                                txtNOMORSEP.Text = DataDecrypt.Item("sep")("noSep").ToString()
                                oPendaftaran.UpdateSEP(dsDaftar.KDPENDAFTARAN, txtNOMORSEP.Text, jsonRequest, jsonResponse, sUserID, txtNOMORSKDP.Text, DataDecrypt.Item("sep")("informasi")("prolanisPRB").ToString())
                            End If
                        End If
                    Catch oErr As Exception
                        MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
                    End Try
                Else
                    Try
                        Dim uTime As Integer = (DateTime.UtcNow - New DateTime(1970, 1, 1, 0, 0, 0)).TotalSeconds
                        Dim jsonRequest As String = String.Empty
                        Dim jsonResponse As String = String.Empty
                        Dim sINFORMASIPRB As String = String.Empty

                        jsonRequest = fn_jsonRequestInsertSEPV2()

                        If jsonRequest <> "" Then
                            jsonResponse = fn_CreateSEPv2(jsonRequest, uTime)
                            If jsonResponse <> "" Then
                                Dim dsDataSetKoneksi = oSetKoneksi.GetData().FirstOrDefault(Function(x) x.ISACTIVE = True And x.NAME_DISPLAY = "VCLAIM")
                                Dim DataDecrypt = JObject.Parse(oSetKoneksi.Decrypt(jsonResponse, dsDataSetKoneksi.CONSID & dsDataSetKoneksi.SECREATKEY & uTime))
                                txtNOMORSEP.Text = DataDecrypt.Item("sep")("noSep").ToString()
                                oPendaftaran.UpdateSEP(dsDaftar.KDPENDAFTARAN, txtNOMORSEP.Text, jsonRequest, jsonResponse, sUserID, txtNOMORSKDP.Text, DataDecrypt.Item("sep")("informasi")("prolanisPRB").ToString())
                            End If
                        End If
                    Catch oErr As Exception
                        MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
                    End Try
                End If
            End If
        Else
            MsgBox("Hanya dilakukan pada saat edit pendaftaran", MsgBoxStyle.Exclamation, Me.Text)
        End If
    End Sub
    Private Sub btnSuratKontrol_Click() Handles btnSuratKontrol.ItemClick
        Dim frmSKD As New frmSKD
        Try
            frmSKD.fn_LoadNoPendaftaranPolidanDokter(grdKDDEPARTMENT.EditValue, grdKDDOCTOR.EditValue, txtKDCUSTOMER.Text)
            frmSKD.LoadMe(FORM_MODE.FORM_MODE_ADD)
            frmSKD.ShowDialog(Me)

            txtNOMORSKDP.Text = sCode
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        Finally
            If Not frmSKD Is Nothing Then frmSKD.Dispose()
            frmSKD = Nothing
        End Try
    End Sub
    Private Sub btnSaveNew_Click() Handles btnSaveNew.ItemClick
        If fn_Validate() = False Then Exit Sub
        If MsgBox(Statement.SaveQuestion, MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, Me.Text) = MsgBoxResult.No Then Exit Sub

        If fn_Save() = False Then
            MsgBox(Statement.SaveFail, MsgBoxStyle.Exclamation, Me.Text)
            If oFormMode = FORM_MODE.FORM_MODE_ADD Then
                txtKDPENDAFTARAN.Text = "<--- AUTO --->"
            End If
        Else
            'Dim oSKDS As New Admission.clsSKD
            'Dim dsSKDS = oSKDS.GetData(txtNOMORSKDP.Text.ToString.Trim.ToUpper)
            'If dsSKDS IsNot Nothing Then
            '    oSKDS.UpdateDataFix(dsSKDS.KDSKD, True)
            'End If

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
            If oFormMode = FORM_MODE.FORM_MODE_ADD Then
                txtKDPENDAFTARAN.Text = "<--- AUTO --->"
            End If
        Else
            'Dim oSKDS As New Admission.clsSKD
            'Dim dsSKDS = oSKDS.GetData(txtNOMORSKDP.Text.ToString.Trim.ToUpper)
            'If dsSKDS IsNot Nothing Then
            '    oSKDS.UpdateDataFix(dsSKDS.KDSKD, True)
            'End If

            MsgBox(Statement.SaveSuccess, MsgBoxStyle.Information, Me.Text)
            Me.Close()
        End If
    End Sub
    Private Sub btnClose_Click() Handles btnClose.ItemClick
        Me.Close()
    End Sub
    Private Sub btnAddCustomer_Click() Handles btnAddCustomer.ItemClick
        If fn_ValidatePencarian() = False Then Exit Sub

        Dim oCustomer As New Reference.clsCustomer
        Dim dsCustomer = oCustomer.GetData(txtKDCUSTOMER.Text)
        If dsCustomer IsNot Nothing Then
            Dim frmCustomer As New frmCustomer
            Try
                frmCustomer.LoadMe(FORM_MODE.FORM_MODE_EDIT, dsCustomer.KDCUSTOMER)
                frmCustomer.ShowDialog(Me)
            Catch oErr As Exception
                MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            End Try
        Else
            Dim frmCustomer As New frmCustomer
            Try
                frmCustomer.fn_LoadRM(txtKDCUSTOMER.Text, False)
                frmCustomer.LoadMe(FORM_MODE.FORM_MODE_ADD)
                frmCustomer.ShowDialog(Me)
            Catch oErr As Exception
                MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            End Try
            If sCode <> "" Then
                If sCode <> "<--- AUTO --->" Then
                    fn_LoadCustomer(sCode)
                    If chkIsOfline.Checked = False Then
                        Dim oPOLI As New Reference.clsDepartment
                        If grdKDDEPARTMENT.EditValue <> "IGD" Then
                            fn_LoadKartuBPJSSatuRecord(txtKARTUBPJS.Text)
                        End If
                    End If
                Else
                    txtKDCUSTOMER.ResetText()
                End If
            Else
                txtKDCUSTOMER.ResetText()
            End If
        End If
    End Sub
    Private Sub CetakRegister(ByVal KDPENDAFTARAN As String)
        Try
            sCetakSEP = False

            Dim ds = oPendaftaran.GetData(KDPENDAFTARAN)

            If ds IsNot Nothing Then
                If fn_CariSEP(ds.NOMORSEP, False) = True Then
                    Dim rpt As New xtraSEP
                    rpt.bindingSource.DataSource = ds
                    Dim printTool As New DevExpress.XtraReports.UI.ReportPrintTool(rpt)
                    printTool.ShowPreviewDialog(DevExpress.LookAndFeel.UserLookAndFeel.Default)
                Else
                    Dim rpt As New xtraUmum
                    rpt.bindingSource.DataSource = ds
                    Dim printTool As New DevExpress.XtraReports.UI.ReportPrintTool(rpt)
                    printTool.ShowPreviewDialog(DevExpress.LookAndFeel.UserLookAndFeel.Default)
                End If

                If sCetakSEP = True Then
                    oPendaftaran.UpdateCetak(KDPENDAFTARAN)
                End If
            End If
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
#End Region
#Region "Lookup"
    Private Sub fn_LoadPenjamin()
        Dim oPenjamin As New Reference.clsPENJAMIN
        Try
            grdKDPENJAMIN.Properties.DataSource = oPenjamin.GetData.Where(Function(x) x.ISACTIVE = True).ToList()
            grdKDPENJAMIN.Properties.ValueMember = "KDPENJAMIN"
            grdKDPENJAMIN.Properties.DisplayMember = "MEMO"
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
    'Private Sub fn_LoadDaftar4()
    '    Dim oDAFTAR_L4 As New Reference.clsDaftar_L4
    '    Try
    '        grdKDDAFTAR_L4.Properties.DataSource = oDAFTAR_L4.GetData.Where(Function(x) x.ISACTIVE = True).ToList()
    '        grdKDDAFTAR_L4.Properties.ValueMember = "KDDAFTAR_L4"
    '        grdKDDAFTAR_L4.Properties.DisplayMember = "MEMO"

    '    Catch oErr As Exception
    '        MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
    '    End Try
    'End Sub
    'Private Sub fn_LoadDaftar5()
    '    Dim oDAFTAR_L5 As New Reference.clsDaftar_L5
    '    Try
    '        grdKDDAFTAR_L5.Properties.DataSource = oDAFTAR_L5.GetData.Where(Function(x) x.ISACTIVE = True).ToList()
    '        grdKDDAFTAR_L5.Properties.ValueMember = "KDDAFTAR_L5"
    '        grdKDDAFTAR_L5.Properties.DisplayMember = "MEMO"

    '    Catch oErr As Exception
    '        MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
    '    End Try
    'End Sub
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
    Private Sub fn_LoadDepartment()
        Dim oDepartment As New Reference.clsDepartment
        Try
            grdKDDEPARTMENT.Properties.DataSource = oDepartment.GetData.Where(Function(x) x.ISACTIVE = True And x.KDDEPARTMENT_BPJS <> "").ToList()
            grdKDDEPARTMENT.Properties.ValueMember = "KDDEPARTMENT"
            grdKDDEPARTMENT.Properties.DisplayMember = "NAME_DISPLAY"

        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadPerusahaan()
        Dim oPerusahaan As New Reference.clsPerusahaan
        Try
            grdKDPERUSAHAAN.Properties.DataSource = oPerusahaan.GetData.Where(Function(x) x.ISACTIVE = True).ToList()
            grdKDPERUSAHAAN.Properties.ValueMember = "KDPERUSAHAAN"
            grdKDPERUSAHAAN.Properties.DisplayMember = "NAME_DISPLAY"

        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadHubungan()
        Dim oHubungan As New Reference.clsHubungan
        Try
            grdKDHUBUNGAN.Properties.DataSource = oHubungan.GetData.Where(Function(x) x.ISACTIVE = True).ToList()
            grdKDHUBUNGAN.Properties.ValueMember = "KDHUBUNGAN"
            grdKDHUBUNGAN.Properties.DisplayMember = "MEMO"

        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadRuangan()
        Dim oRuangRawat As New Reference.clsRuangRawat
        Try
            grdKDRUANGRAWAT.Properties.DataSource = oRuangRawat.GetData.Where(Function(x) x.ISACTIVE = True).ToList()
            grdKDRUANGRAWAT.Properties.ValueMember = "KDRUANGRAWAT"
            grdKDRUANGRAWAT.Properties.DisplayMember = "NAME_DISPLAY"
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadTempatTidur(ByVal Parameter As String)
        Dim oRuangRawat As New Reference.clsRuangRawat
        Try
            grdTempatTidur.Properties.DataSource = oRuangRawat.GetDataDetail(Parameter).Where(Function(x) x.ISACTIVE = True).ToList()
            grdTempatTidur.Properties.ValueMember = "SEQ"
            grdTempatTidur.Properties.DisplayMember = "MEMO"
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadDoctor(ByVal KDDEPARTMENT As String)
        If grdKDDEPARTMENT.Text = "" Then Exit Sub
        Dim oDoctor As New Reference.clsDoctor

        If rbCATEGORY.SelectedIndex = 0 Then
            Try
                If grdKDPENJAMIN.Text = "BPJS KESEHATAN" Then
                    Dim dsDoctor = From x In oDoctor.GetData
                                   Where x.ISACTIVE = True And x.KDDEPARTMENT = grdKDDEPARTMENT.EditValue And x.VCLAIM_KDDPJP <> ""
                                   Select x.KDDOCTOR, x.NAME_DISPLAY

                    grdKDDOCTOR.Properties.DataSource = dsDoctor.ToList()
                    grdKDDOCTOR.Properties.ValueMember = "KDDOCTOR"
                    grdKDDOCTOR.Properties.DisplayMember = "NAME_DISPLAY"

                    grdKDDOCTOR_PELAYANAN.Properties.DataSource = dsDoctor.ToList()
                    grdKDDOCTOR_PELAYANAN.Properties.ValueMember = "KDDOCTOR"
                    grdKDDOCTOR_PELAYANAN.Properties.DisplayMember = "NAME_DISPLAY"
                Else
                    Dim dsDoctor = From x In oDoctor.GetData
                                   Where x.ISACTIVE = True And x.KDDEPARTMENT = grdKDDEPARTMENT.EditValue
                                   Select x.KDDOCTOR, x.NAME_DISPLAY

                    grdKDDOCTOR.Properties.DataSource = dsDoctor.ToList()
                    grdKDDOCTOR.Properties.ValueMember = "KDDOCTOR"
                    grdKDDOCTOR.Properties.DisplayMember = "NAME_DISPLAY"

                    grdKDDOCTOR_PELAYANAN.Properties.DataSource = dsDoctor.ToList()
                    grdKDDOCTOR_PELAYANAN.Properties.ValueMember = "KDDOCTOR"
                    grdKDDOCTOR_PELAYANAN.Properties.DisplayMember = "NAME_DISPLAY"
                End If

            Catch oErr As Exception
                MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            End Try
        Else
            Try
                grdKDDOCTOR.Properties.DataSource = oDoctor.GetData().Where(Function(x) x.ISACTIVE = True).ToList()
                grdKDDOCTOR.Properties.ValueMember = "KDDOCTOR"
                grdKDDOCTOR.Properties.DisplayMember = "NAME_DISPLAY"

                grdKDDOCTOR_PELAYANAN.Properties.DataSource = oDoctor.GetData().Where(Function(x) x.ISACTIVE = True).ToList()
                grdKDDOCTOR_PELAYANAN.Properties.ValueMember = "KDDOCTOR"
                grdKDDOCTOR_PELAYANAN.Properties.DisplayMember = "NAME_DISPLAY"
            Catch oErr As Exception
                MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            End Try
        End If

        'If rbCATEGORY.SelectedIndex = 0 Then
        '    Try
        '        Dim dsDoctor = From x In oDoctor.GetData
        '                       Where x.ISACTIVE = True And x.KDDEPARTMENT = grdKDDEPARTMENT.EditValue
        '                       Select x.KDDOCTOR, x.NAME_DISPLAY

        '        grdKDDOCTOR.Properties.DataSource = dsDoctor.ToList()
        '        grdKDDOCTOR.Properties.ValueMember = "KDDOCTOR"
        '        grdKDDOCTOR.Properties.DisplayMember = "NAME_DISPLAY"

        '    Catch oErr As Exception
        '        MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        '    End Try
        'Else
        '    Try
        '        grdKDDOCTOR.Properties.DataSource = oDoctor.GetData.Where(Function(x) x.ISACTIVE = True).ToList()
        '        grdKDDOCTOR.Properties.ValueMember = "KDDOCTOR"
        '        grdKDDOCTOR.Properties.DisplayMember = "NAME_DISPLAY"

        '    Catch oErr As Exception
        '        MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        '    End Try
        'End If
    End Sub
    Private Sub fn_LoadDiganosa()
        Dim oDiagnosa As New Reference.clsDiagnosa
        Try
            grdKDDIAGNOSA.Properties.DataSource = oDiagnosa.GetData.Where(Function(x) x.ISACTIVE = True).ToList()
            grdKDDIAGNOSA.Properties.ValueMember = "KDDIAGNOSA"
            grdKDDIAGNOSA.Properties.DisplayMember = "MEMO"

        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadFaskes()
        Dim oPPK As New Reference.clsPPK
        Try
            grdKDPPK.Properties.DataSource = oPPK.GetData.Where(Function(x) x.ISACTIVE = True).ToList()
            grdKDPPK.Properties.ValueMember = "KDPPK"
            grdKDPPK.Properties.DisplayMember = "MEMO"
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadkelasRawat()
        Dim oKELAS As New Reference.clsKelasRawat
        Try
            grdKDKELASRAWAT.Properties.DataSource = oKELAS.GetData.Where(Function(x) x.ISACTIVE = True).ToList()
            grdKDKELASRAWAT.Properties.ValueMember = "KDKELASRAWAT"
            grdKDKELASRAWAT.Properties.DisplayMember = "MEMO"

            grdKDKELASRAWAT_NAIKKELAS.Properties.DataSource = oKELAS.GetData.Where(Function(x) x.ISACTIVE = True).ToList()
            grdKDKELASRAWAT_NAIKKELAS.Properties.ValueMember = "KDKELASRAWAT"
            grdKDKELASRAWAT_NAIKKELAS.Properties.DisplayMember = "MEMO"
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadHistoryPasien(ByVal sKDCUSTOMER As String)
        Try
            Dim listPendaftaran As New List(Of DataAccess.R_HISTORY_PENDAFATRAN)

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
            SQL_1 &= "NoPendaftaran = A.KDREG "
            SQL_1 &= ",TanggalMasuk = A.DATE "
            SQL_1 &= ",NoSuratKontrol = '' "
            SQL_1 &= ",TanggalRujukan = A.DATERUJUKAN "
            SQL_1 &= ",NoRujukan = A.NOMORRUJUKAN "
            SQL_1 &= ",NoSEP = A.NOMORSEP "
            SQL_1 &= ",Tujuan = B.NAME_DISPLAY "
            SQL_1 &= "FROM S_PENDAFTARAN_H A "
            SQL_1 &= "INNER JOIN M_DEPARTMENT B "
            SQL_1 &= "ON A.KDDEPARTMENT = B.KDDEPARTMENT "
            SQL_1 &= "WHERE A.KDCUSTOMER ='" & sKDCUSTOMER & "' "
            'SQL_1 &= "ORDER BY A.DATE DESC "

            oComm_1.Connection = oConn_1
            oComm_1.CommandText = SQL_1
            oComm_1.CommandTimeout = 120
            oComm_1.CommandType = CommandType.Text

            da_1 = New SqlDataAdapter(oComm_1)
            da_1.Fill(ds_1, "S_PENDAFTARAN_1")

            For iLoop As Integer = 0 To ds_1.Tables("S_PENDAFTARAN_1").Rows.Count - 1
                Dim dsRekap As New DataAccess.R_HISTORY_PENDAFATRAN
                With ds_1.Tables("S_PENDAFTARAN_1")
                    dsRekap.NoPendaftaran = .Rows(iLoop)("NoPendaftaran")
                    dsRekap.TanggalMasuk = .Rows(iLoop)("TanggalMasuk")
                    dsRekap.TanggalRujukan = .Rows(iLoop)("TanggalRujukan")
                    dsRekap.NoRujukan = .Rows(iLoop)("NoRujukan")
                    dsRekap.NoSEP = .Rows(iLoop)("NoSEP")
                    dsRekap.Tujuan = .Rows(iLoop)("Tujuan")

                    listPendaftaran.Add(dsRekap)
                End With
            Next

            If oConn_1.State = ConnectionState.Open Then
                oConn_1.Close()
            End If

            Dim oConn_2 As New SqlConnection
            Dim oComm_2 As New SqlCommand
            Dim da_2 As SqlDataAdapter
            Dim ds_2 As New DataSet
            Dim SQL_2 As String
            Dim sConn_2 As String = Decrypt(My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Software\SIMRS\SW\", "Database", "").ToString())

            oConn_2 = New SqlConnection(sConn_2)

            If oConn_2.State = ConnectionState.Closed Then
                oConn_2.Open()
            End If

            SQL_2 = "SELECT "
            SQL_2 &= "* "
            SQL_2 &= "FROM "
            SQL_2 &= "( "
            SQL_2 &= "SELECT "
            SQL_2 &= "NoPendaftaran = A.KDKUNJUNGAN_POLI "
            SQL_2 &= ",TanggalMasuk = A.DATE_MASUK "
            SQL_2 &= ",NoSuratKontrol = ISNULL((SELECT KDSKD FROM S_PENDAFTARAN_SKD WHERE A.KDPENDAFTARAN = KDPENDAFTARAN), '') "
            SQL_2 &= ",TanggalRujukan = B.DATE_RUJUKAN "
            SQL_2 &= ",NoRujukan = B.NOMORRUJUKAN "
            SQL_2 &= ",NoSEP = B.NOMORSEP "
            SQL_2 &= ",Tujuan = C.NAME_DISPLAY "
            SQL_2 &= "FROM S_PENDAFTARAN_KUNJUNGANPOLI A "
            SQL_2 &= "INNER JOIN S_PENDAFTARAN_H B "
            SQL_2 &= "ON A.KDPENDAFTARAN = B.KDPENDAFTARAN "
            SQL_2 &= "INNER JOIN M_DEPARTMENT C "
            SQL_2 &= "ON A.KDDEPARTMENT = C.KDDEPARTMENT "
            SQL_2 &= "WHERE B.KDCUSTOMER ='" & sKDCUSTOMER & "' "

            SQL_2 &= "UNION "

            SQL_2 &= "SELECT "
            SQL_2 &= "NoPendaftaran = A.KDKUNJUNGAN_RUANGAN "
            SQL_2 &= ",TanggalMasuk = A.DATE_MASUK "
            SQL_2 &= ",NoSuratKontrol = ISNULL((SELECT KDSKD FROM S_PENDAFTARAN_SKD WHERE A.KDPENDAFTARAN = KDPENDAFTARAN), '') "
            SQL_2 &= ",TanggalRujukan = B.DATE_RUJUKAN "
            SQL_2 &= ",NoRujukan = B.NOMORRUJUKAN "
            SQL_2 &= ",NoSEP = B.NOMORSEP "
            SQL_2 &= ",Tujuan = C.NAME_DISPLAY "
            SQL_2 &= "FROM S_PENDAFTARAN_KUNJUNGANRUANGAN A "
            SQL_2 &= "INNER JOIN S_PENDAFTARAN_H B "
            SQL_2 &= "ON A.KDPENDAFTARAN = B.KDPENDAFTARAN "
            SQL_2 &= "INNER JOIN M_RUANGRAWAT C "
            SQL_2 &= "ON A.KDRUANGRAWAT = C.KDRUANGRAWAT "
            SQL_2 &= "WHERE B.KDCUSTOMER ='" & sKDCUSTOMER & "' "

            SQL_2 &= ") Z "
            'SQL_2 &= "ORDER BY "
            'SQL_2 &= "Z.TanggalMasuk "


            oComm_2.Connection = oConn_2
            oComm_2.CommandText = SQL_2
            oComm_2.CommandTimeout = 120
            oComm_2.CommandType = CommandType.Text

            da_2 = New SqlDataAdapter(oComm_2)
            da_2.Fill(ds_2, "S_PENDAFTARAN_2")

            For iLoop As Integer = 0 To ds_2.Tables("S_PENDAFTARAN_2").Rows.Count - 1
                Dim dsRekap As New DataAccess.R_HISTORY_PENDAFATRAN
                With ds_2.Tables("S_PENDAFTARAN_2")
                    dsRekap.NoPendaftaran = .Rows(iLoop)("NoPendaftaran")
                    dsRekap.TanggalMasuk = .Rows(iLoop)("TanggalMasuk")
                    dsRekap.TanggalRujukan = .Rows(iLoop)("TanggalRujukan")
                    dsRekap.NoRujukan = .Rows(iLoop)("NoRujukan")
                    dsRekap.NoSEP = .Rows(iLoop)("NoSEP")
                    dsRekap.Tujuan = .Rows(iLoop)("Tujuan")

                    listPendaftaran.Add(dsRekap)
                End With
            Next

            If oConn_2.State = ConnectionState.Open Then
                oConn_2.Close()
            End If

            grdHistoryPasien.DataSource = listPendaftaran.OrderBy(Function(x) x.TanggalMasuk)
            grdHistoryPasien.ForceInitialize()

            fn_LoadFormatDataAll()

        Catch oErr As Exception
            MsgBox("Preview Data : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadFormatDataAll()
        For iLoop As Integer = 0 To grvHistoryPasien.Columns.Count - 1
            If grvHistoryPasien.Columns(iLoop).ColumnType.Name = "Decimal" Then
                grvHistoryPasien.Columns(iLoop).DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                grvHistoryPasien.Columns(iLoop).DisplayFormat.FormatString = "{0:n2}"
                grvHistoryPasien.Columns(iLoop).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far

                grvHistoryPasien.Columns(iLoop).SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                grvHistoryPasien.Columns(iLoop).SummaryItem.DisplayFormat = "{0:n2}"
            ElseIf grvHistoryPasien.Columns(iLoop).ColumnType.Name = "DateTime" Then
                grvHistoryPasien.Columns(iLoop).DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
                grvHistoryPasien.Columns(iLoop).DisplayFormat.FormatString = "{0:dd/MM/yyyy}"
            End If
        Next
    End Sub
    Public Function AutoNumber(ByVal sKDCOUNTER As String, ByVal sLASTNUMBER As Integer, ByVal sDATE As DateTime) As String
        Dim sMonth As String = String.Empty

        Select Case Month(sDATE)
            Case 1
                sMonth = "A"
            Case 2
                sMonth = "B"
            Case 3
                sMonth = "C"
            Case 4
                sMonth = "D"
            Case 5
                sMonth = "E"
            Case 6
                sMonth = "F"
            Case 7
                sMonth = "G"
            Case 8
                sMonth = "H"
            Case 9
                sMonth = "I"
            Case 10
                sMonth = "J"
            Case 11
                sMonth = "K"
            Case 12
                sMonth = "L"
        End Select

        If sLASTNUMBER < 10 Then
            AutoNumber = sKDCOUNTER & Year(sDATE) & sMonth & "00000" & sLASTNUMBER.ToString
        ElseIf sLASTNUMBER < 100 Then
            AutoNumber = sKDCOUNTER & Year(sDATE) & sMonth & "0000" & sLASTNUMBER.ToString
        ElseIf sLASTNUMBER < 1000 Then
            AutoNumber = sKDCOUNTER & Year(sDATE) & sMonth & "000" & sLASTNUMBER.ToString
        ElseIf sLASTNUMBER < 10000 Then
            AutoNumber = sKDCOUNTER & Year(sDATE) & sMonth & "00" & sLASTNUMBER.ToString
        ElseIf sLASTNUMBER < 100000 Then
            AutoNumber = sKDCOUNTER & Year(sDATE) & sMonth & "0" & sLASTNUMBER.ToString
        Else
            AutoNumber = sKDCOUNTER & Year(sDATE) & sMonth & sLASTNUMBER.ToString
        End If
    End Function
#End Region
#Region "Event"
    Private Sub rbCATEGORY_SelectedIndexChanged() Handles rbCATEGORY.SelectedIndexChanged
        grdKDDOCTOR.Properties.DataSource = Nothing
        If rbCATEGORY.SelectedIndex = 0 Then
            lRUANGAN.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            lKDPENDAFTARAN_AWAL.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            lCARIKDREG_AWAL.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            lGABUNG.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            'lCARIPERTAMA.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            lCARIRUJUKAN.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            chkRESISTERAWAL.Checked = False
        Else
            lRUANGAN.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            lKDPENDAFTARAN_AWAL.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            lCARIKDREG_AWAL.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            lGABUNG.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            'lCARIPERTAMA.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            lCARIRUJUKAN.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            chkRESISTERAWAL.Checked = True
        End If
    End Sub
    Private Sub grdKDDEPARTMENT_KeyPress(sender As Object, e As KeyPressEventArgs) Handles grdKDDEPARTMENT.KeyPress
        If Asc(e.KeyChar) = 13 Then
            fn_LoadDoctor(grdKDDEPARTMENT.EditValue)

            Dim oPOLI As New Reference.clsDepartment
            Dim dsPoli = oPOLI.GetData(grdKDDEPARTMENT.EditValue)

            If dsPoli IsNot Nothing Then
                If dsPoli.KDDEPARTMENT = "IGD" Then
                    cboASALRUJUKAN.SelectedIndex = 1
                    Dim oFaskes As New Reference.clsPPK

                    Dim dsFaskes = oFaskes.GetDataKodeFaskes(oSetKoneksi.GetData().Where(Function(x) x.ISACTIVE = True And x.NAME_DISPLAY = "VCLAIM").FirstOrDefault.PPKPELAYANAN.ToString.Trim.ToUpper)
                    If dsFaskes IsNot Nothing Then
                        grdKDPPK.Text = dsFaskes.KDPPK
                    End If
                End If
            End If

            grdKDDOCTOR.Focus()

        End If
    End Sub
    Private Sub grdKDDOCTOR_KeyPress(sender As Object, e As KeyPressEventArgs) Handles grdKDDOCTOR.KeyPress
        If Asc(e.KeyChar) = 13 Then
            grdKDDOCTOR_PELAYANAN.Text = grdKDDOCTOR.EditValue
            If grdKDPENJAMIN.Text = "BPJS KESEHATAN" Then
                'If grdKDDIAGNOSA.Text = String.Empty Then
                '    grdKDDIAGNOSA.ShowPopup()
                'End If
            End If
        End If
    End Sub
    Private Sub chkISEKSEKUTIF_Click(sender As Object, e As EventArgs) Handles chkISEKSEKUTIF.Click
        If grdKDDEPARTMENT.Text = String.Empty Then
            MsgBox("Bukan Eksekutif", MsgBoxStyle.Exclamation, Me.Text)
            chkISEKSEKUTIF.Checked = False
        End If
        Dim oDepartment As New Reference.clsDepartment
        Dim dsDepartment = oDepartment.GetData(grdKDDEPARTMENT.EditValue)
        If dsDepartment IsNot Nothing Then
            If dsDepartment.ISEKSEKUTIF = False Then
                MsgBox("Bukan Eksekutif", MsgBoxStyle.Exclamation, Me.Text)
                chkISEKSEKUTIF.Checked = False
            End If
        Else
            chkISEKSEKUTIF.Checked = False
        End If
    End Sub
    Private Sub chkISKATARAK_Click(sender As Object, e As EventArgs) Handles chkISKATARAK.Click
        If grdKDDEPARTMENT.Text = String.Empty Then
            MsgBox("Tidak bisa buka Katarak", MsgBoxStyle.Exclamation, Me.Text)
            chkISEKSEKUTIF.Checked = False
        End If
        Dim oDepartment As New Reference.clsDepartment
        Dim dsDepartment = oDepartment.GetData(grdKDDEPARTMENT.EditValue)
        If dsDepartment IsNot Nothing Then
            If dsDepartment.ISKATARAK = False Then
                MsgBox("Tidak bisa buka Katarak", MsgBoxStyle.Exclamation, Me.Text)
                chkISKATARAK.Checked = False
            End If
        Else
            chkISKATARAK.Checked = False
        End If
    End Sub
    Private Sub txtNOMORSEP_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtNOMORSEP.KeyPress
        If Asc(e.KeyChar) = 13 Then
            If txtNOMORSEP.Text.Count = 19 Then
                fn_CariSEP(txtNOMORSEP.Text.ToString.Trim.ToUpper, True)
                'MsgBox(fn_CariSEP(txtNOMORSEP.Text.ToString.Trim.ToUpper), MsgBoxStyle.Information, Me.Text)
            ElseIf txtNOMORSEP.Text.Count > 19 Then
                MsgBox("Validasi : " & vbCrLf & "No SEP lebih dari 19 Digit", MsgBoxStyle.Exclamation, Me.Text)
            Else
                MsgBox("Validasi : " & vbCrLf & "No SEP kurang dari 19 Digit", MsgBoxStyle.Exclamation, Me.Text)
            End If
        End If
    End Sub
    Private Sub grdHistoryPasien_DoubleClick(sender As Object, e As EventArgs) Handles grdHistoryPasien.DoubleClick
        If grvHistoryPasien.GetFocusedRowCellValue("NomorPendaftaran") Is Nothing Then
            Exit Sub
        End If

        Dim frmPENDAFTARAN As New frmPendaftaran
        Try
            frmPENDAFTARAN.LoadMe(FORM_MODE.FORM_MODE_VIEW, grvHistoryPasien.GetFocusedRowCellValue("NomorPendaftaran"))
            frmPENDAFTARAN.ShowDialog(Me)
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
#End Region
#Region "Insert Reference"
    Private Function InsertDiagnosa(ByVal sKDDIAGNOSA As String, ByVal sMEMO As String) As Boolean
        Try
            Dim oDiagnosa As New Reference.clsDiagnosa

            InsertDiagnosa = True

            If oDiagnosa.IsExist(sKDDIAGNOSA) Then
                grdKDDIAGNOSA.EditValue = sKDDIAGNOSA
            Else
                Dim ds = oDiagnosa.GetStructureHeader
                With ds
                    .DATECREATED = Now
                    .DATEUPDATED = Now
                    .KDDIAGNOSA = sKDDIAGNOSA
                    .MEMO = sMEMO.ToString.Trim.ToUpper
                    .ISDEFAULT = IIf(oDiagnosa.CheckDefault(0, False) = False, True, False)
                    .ISACTIVE = True
                End With

                InsertDiagnosa = oDiagnosa.InsertData(ds)

                fn_LoadDiganosa()
                grdKDDIAGNOSA.EditValue = sKDDIAGNOSA
            End If

        Catch ex As Exception
            InsertDiagnosa = False
            MsgBox(ex.ToString)
        End Try
    End Function
    Private Function InsertPPK(ByVal sKODEFASKES As String, ByVal sMEMO As String) As Boolean
        Try
            Dim oPPK As New Reference.clsPPK

            InsertPPK = True

            If oPPK.IsExistKodeFaskes(sKODEFASKES) Then
                grdKDPPK.EditValue = oPPK.GetDataKodeFaskes(sKODEFASKES).KDPPK
            Else
                Dim ds = oPPK.GetStructureHeader
                With ds
                    .DATECREATED = Now
                    .DATEUPDATED = Now
                    .KDPPK = ""
                    .JENISFASKES = cboASALRUJUKAN.SelectedIndex
                    .KODEFASKES = sKODEFASKES
                    .MEMO = sMEMO
                    .ISDEFAULT = IIf(oPPK.CheckDefault(0, False) = False, True, False)
                    .ISACTIVE = True
                End With

                InsertPPK = oPPK.InsertData(ds)

                fn_LoadFaskes()
                grdKDPPK.EditValue = oPPK.GetDataKodeFaskes(sKODEFASKES).KDPPK
            End If

        Catch ex As Exception
            InsertPPK = False
            MsgBox(ex.ToString)
        End Try
    End Function
    Private Function InsertJenisPeserta(ByVal sKDJENISPESERTA As String, ByVal sMEMO As String) As Boolean
        Try
            Dim oJenisPeserta As New Reference.clsDaftar_L1

            InsertJenisPeserta = True

            If oJenisPeserta.IsExistKode(sKDJENISPESERTA) Then
                grdKDDAFTAR_L1.Text = sKDJENISPESERTA
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
                grdKDDAFTAR_L1.Text = sKDJENISPESERTA
            End If

        Catch ex As Exception
            InsertJenisPeserta = False
            MsgBox(ex.ToString)
        End Try
    End Function
    Private Function InsertCOB(ByVal sKDCOB As String, ByVal sMEMO As String, ByVal sTGLTAT As DateTime, ByVal sTGLTMT As DateTime) As Boolean
        Try
            Dim oCOB As New Reference.clsCOB

            InsertCOB = True

            If oCOB.IsExist(sKDCOB) Then
                'grdKDCOB.EditValue = oCOB.GetData(sKDCOB).KDCOB
            Else
                Dim ds = oCOB.GetStructureHeader
                With ds
                    .DATECREATED = Now
                    .DATEUPDATED = Now
                    .KDCOB = sKDCOB
                    .MEMO = sMEMO
                    .ISDEFAULT = IIf(oCOB.CheckDefault(0, False) = False, True, False)
                    .ISACTIVE = True
                    .TGLTAT = sTGLTAT
                    .TGLTMT = sTGLTMT
                End With

                InsertCOB = oCOB.InsertData(ds)

            End If

        Catch ex As Exception
            InsertCOB = False
            MsgBox(ex.ToString)
        End Try
    End Function
#End Region
#Region "Function / Sub Search"
    Private Function fn_ValidatePencarian() As Boolean
        Try
            fn_ValidatePencarian = True
            If grdKDPENJAMIN.Text = String.Empty Then
                grdKDPENJAMIN.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                grdKDPENJAMIN.ErrorText = Statement.ErrorRequired

                grdKDPENJAMIN.Focus()
                fn_ValidatePencarian = False
                Exit Function
            End If
        Catch oErr As Exception
            fn_ValidatePencarian = False
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Function
    Private Sub fn_LoadCustomer(ByVal Parameter As String)
        Try
            Dim oCustomer As New Reference.clsCustomer

            Dim dsCustomer = oCustomer.GetData(Parameter)
            If dsCustomer IsNot Nothing Then
                txtNAMAPASIEN.Text = dsCustomer.NAME_DISPLAY
                If dsCustomer.KDJENISKELAMIN = 0 Then
                    txtKDJENISKELAMIN.Text = "L"
                Else
                    txtKDJENISKELAMIN.Text = "P"
                End If

                txtJALAN.Text = dsCustomer.JALAN
                txtPROPINSI.Text = dsCustomer.PROPINSI
                txtKOTA.Text = dsCustomer.KOTA
                txtKECAMATAN.Text = dsCustomer.KECAMATAN
                txtKELURAHAN.Text = dsCustomer.KELURAHAN
                txtKODEPOS.Text = dsCustomer.KODEPOS
                txtKARTUBPJS.Text = dsCustomer.KARTUBPJS
                txtKDCUSTOMER.Text = dsCustomer.KDCUSTOMER
                txtNOMORTELEPON.Text = dsCustomer.NOTELEPON
                grdKDDAFTAR_L1.Text = dsCustomer.KDDAFTAR_L1
                grdKDDAFTAR_L2.Text = dsCustomer.KDDAFTAR_L2
                grdKDDAFTAR_L3.Text = dsCustomer.KDDAFTAR_L3
                'grdKDDAFTAR_L4.Text = dsCustomer.KDDAFTAR_L4
                'grdKDDAFTAR_L5.Text = dsCustomer.KDDAFTAR_L5
                grdKDDAFTAR_L6.Text = dsCustomer.KDDAFTAR_L6

                txtNAMAPENANGGUNGJAWAB.Text = dsCustomer.NAMAPENANGUNGJAWAB
                grdKDHUBUNGAN.Text = dsCustomer.KDHUBUNGAN
                txtALAMATPENANGGUNGJAWAB.Text = dsCustomer.ALAMATNAMAPENANGUNGJAWAB
                txtNOMORTELEPONPENANGGUNGJAWAB.Text = dsCustomer.NOMORTELEPONPENANGGUNGJAWAB

                fn_LoadHistoryPasien(dsCustomer.KDCUSTOMER)

                Try
                    If txtKDCUSTOMER.Text <> "" Then
                        picAttacment.Image = GetImageFromURL(AlamatDownloadIamge1 & txtKDCUSTOMER.Text & "/" & txtKDCUSTOMER.Text & ".png")
                    End If
                Catch ex As Exception

                End Try
            Else
                MsgBox("Nomor Rekam Medis Tidak Ada di database " & Parameter, MsgBoxStyle.Exclamation, Me.Text)
                fn_EmptyMe()
                txtKDCUSTOMER.Text = Parameter

                Dim frmCustomer As New frmCustomer
                Try
                    frmCustomer.fn_LoadRM(txtKDCUSTOMER.Text, True)
                    frmCustomer.LoadMe(FORM_MODE.FORM_MODE_ADD)
                    frmCustomer.ShowDialog(Me)
                Catch oErr As Exception
                    MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
                If sCode <> "" And sCode <> "<---AUTO--->" Then
                    If sCode <> "<--- AUTO --->" Then
                        fn_LoadCustomer(sCode)
                        If chkIsOfline.Checked = False Then
                            Dim oPOLI As New Reference.clsDepartment
                            If grdKDDEPARTMENT.EditValue <> "IGD" Then
                                fn_LoadKartuBPJSSatuRecord(txtKARTUBPJS.Text)
                            End If
                        End If
                    Else
                        txtKDCUSTOMER.ResetText()
                    End If
                Else
                    txtKDCUSTOMER.ResetText()
                End If
            End If
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadNomorSKD(ByVal Parameter As String)
        Try
            Dim oSuratKontrol As New Admission.clsSKD
            Dim dsSuratKontrol = oSuratKontrol.GetData(Parameter)

            If dsSuratKontrol IsNot Nothing Then
                grdKDPENJAMIN.Text = dsSuratKontrol.S_PENDAFTARAN_H.KDPENJAMIN
                fn_LoadCustomer(dsSuratKontrol.S_PENDAFTARAN_H.KDCUSTOMER)
                cboASALRUJUKAN.SelectedIndex = dsSuratKontrol.S_PENDAFTARAN_H.ASALRUJUKAN
                grdKDPPK.Text = dsSuratKontrol.S_PENDAFTARAN_H.KDPPK
                grdKDDEPARTMENT.Text = dsSuratKontrol.KDDEPARTMENT
                fn_LoadDoctor(grdKDDEPARTMENT.EditValue)
                grdKDDOCTOR.Text = dsSuratKontrol.KDDOCTOR
                grdKDDOCTOR_PELAYANAN.Text = dsSuratKontrol.KDDOCTOR
                txtNOMORRUJUKAN.Text = dsSuratKontrol.NOMORRUJUKAN
                txtKARTUBPJS.Text = dsSuratKontrol.S_PENDAFTARAN_H.KARTUBPJS
                deDATE_RUJUKAN.DateTime = dsSuratKontrol.S_PENDAFTARAN_H.DATE_RUJUKAN
                txtCATATAN.Text = dsSuratKontrol.TINDAKLANJUT
                grdKDDIAGNOSA.Text = dsSuratKontrol.KDDIAGNOSA
                grdKDKELASRAWAT.Text = dsSuratKontrol.S_PENDAFTARAN_H.KDKELASRAWAT
                grdKDKELASRAWAT_NAIKKELAS.Text = dsSuratKontrol.S_PENDAFTARAN_H.KDKELASRAWAT
                'txtTUJUANKUNJUNGAN.SelectedIndex = dsSuratKontrol.S_PENDAFTARAN_H.TUJUANKUNJUNGAN
                'txtFLAGPROCEDURE.SelectedIndex = dsSuratKontrol.S_PENDAFTARAN_H.FLAGPROCEDURE
                'txtKDPENUNJANG.SelectedIndex = dsSuratKontrol.S_PENDAFTARAN_H.KDPENUNJANG
                'txtASSEMENTPEL.SelectedIndex = dsSuratKontrol.S_PENDAFTARAN_H.ASESMENTPEL
            Else
                MsgBox("Data Tidak ditemukan dengan Nomor " & Parameter, MsgBoxStyle.Exclamation, Me.Text)
                deDATE_RUJUKAN.DateTime = Now
                txtNOMORRUJUKAN.ResetText()
                grdKDDIAGNOSA.ResetText()
                grdKDDEPARTMENT.ResetText()
                grdKDDOCTOR.ResetText()
                grdKDPPK.Reset()
                grdKDDOCTOR.Properties.DataSource = Nothing
                txtNOMORSKDP.ResetText()
            End If
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadPendaftaranRekamMedisList(ByVal sParameter As String)
        Try
            If sParameter = String.Empty Then Exit Sub

            grvCARIKDPENDAFTARAN_AWAL.Columns.Clear()
            grdCARIKDREGAWAL.Properties.DataSource = Nothing
            grvCARIKDPENDAFTARAN_AWAL.OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways

            Dim dsCustomerList = From x In oPendaftaran.GetDataByRekamMedisRawatJalan(sParameter)
                                 Select Kode = x.KDPENDAFTARAN, TanggalDaftar = x.DATE, NoRekamMedis = x.KDCUSTOMER, NoKTP = x.M_CUSTOMER.KTP, NoKartuBPJS = x.KARTUBPJS, Pasien = x.M_CUSTOMER.NAME_DISPLAY, TanggalLahir = x.M_CUSTOMER.TANGGALLAHIR.ToString("dd-MM-yyyy")

            grdCARIKDREGAWAL.Properties.DataSource = dsCustomerList.ToList()
            grdCARIKDREGAWAL.Properties.ValueMember = "Kode"
            grdCARIKDREGAWAL.Properties.DisplayMember = "Kode"

            fn_LoadFormatDataKdregAwal()

            grdCARIKDREGAWAL.ShowPopup()
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadFormatDataKdregAwal()
        For iLoop As Integer = 0 To grvCARIKDPENDAFTARAN_AWAL.Columns.Count - 1
            If grvCARIKDPENDAFTARAN_AWAL.Columns(iLoop).ColumnType.Name = "Decimal" Then
                grvCARIKDPENDAFTARAN_AWAL.Columns(iLoop).DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                grvCARIKDPENDAFTARAN_AWAL.Columns(iLoop).DisplayFormat.FormatString = "{0:n2}"
                grvCARIKDPENDAFTARAN_AWAL.Columns(iLoop).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far

                grvCARIKDPENDAFTARAN_AWAL.Columns(iLoop).SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                grvCARIKDPENDAFTARAN_AWAL.Columns(iLoop).SummaryItem.DisplayFormat = "{0:n2}"
            ElseIf grvCARIKDPENDAFTARAN_AWAL.Columns(iLoop).ColumnType.Name = "DateTime" Then
                grvCARIKDPENDAFTARAN_AWAL.Columns(iLoop).DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
                grvCARIKDPENDAFTARAN_AWAL.Columns(iLoop).DisplayFormat.FormatString = "{0:dd/MM/yyyy}"
            End If
        Next
    End Sub
    Private Sub fn_LoadKartuBPJSSatuRecord(ByVal sKARTUBPS As String)
        Try
            If txtKARTUBPJS.Text = "" Then Exit Sub

            If txtKARTUBPJS.Text.Count <> 13 Then
                txtKARTUBPJS.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                txtKARTUBPJS.ErrorText = Statement.ErrorRequired
                MsgBox(Statement.ErrorStatement & " No Kartu BPJS Tidak sama dengan 13 Digit", MsgBoxStyle.Exclamation, Me.Text)

                txtKARTUBPJS.Focus()
                Exit Sub
            End If

            Dim dsDataSetKoneksi = oSetKoneksi.GetData().FirstOrDefault(Function(x) x.ISACTIVE = True And x.NAME_DISPLAY = "VCLAIM")
            Dim uTime As Integer = 0

            If dsDataSetKoneksi IsNot Nothing Then
                uTime = (DateTime.UtcNow - New DateTime(1970, 1, 1, 0, 0, 0)).TotalSeconds
                Dim dsSetKoneksi = oSetKoneksi.CariRujukanKartuSatuRecord(dsDataSetKoneksi.ALAMATWEB, dsDataSetKoneksi.CONSID, dsDataSetKoneksi.SECREATKEY, dsDataSetKoneksi.USER_KEY, uTime, sKARTUBPS, cboASALRUJUKAN.SelectedIndex)

                If dsSetKoneksi <> "" Then
                    Try
                        Dim allData = JObject.Parse(dsSetKoneksi)

                        Dim CodeResponse As String = String.Empty
                        Dim messageResponse As String = String.Empty

                        CodeResponse = IIf(IsDBNull(allData.Item("metaData").Item("code")) = True, "", allData.Item("metaData").Item("code"))
                        messageResponse = allData("metaData")("message").ToString

                        If CodeResponse = "200" Then
                            Dim DataDecrypt = JObject.Parse(oSetKoneksi.Decrypt(allData("response"), dsDataSetKoneksi.CONSID & dsDataSetKoneksi.SECREATKEY & uTime))
                            Dim oDepartment As New Reference.clsDepartment
                            Dim dsDepartment = oDepartment.GetDatakodebpjs(DataDecrypt("rujukan")("poliRujukan")("kode").ToString())

                            If dsDepartment IsNot Nothing Then
                                grdKDDEPARTMENT.Text = dsDepartment.KDDEPARTMENT
                                fn_LoadDoctor(dsDepartment.KDDEPARTMENT)
                                grdKDDOCTOR.Focus()
                            Else
                                MsgBox("Kode Poli Tidak ditemukan di Database", MsgBoxStyle.Exclamation, Me.Text)
                            End If
                            txtNOMORRUJUKAN.Text = DataDecrypt("rujukan")("noKunjungan").ToString()
                            txtKARTUBPJS.Text = DataDecrypt("rujukan")("peserta")("noKartu").ToString()
                            deDATE_RUJUKAN.DateTime = DataDecrypt("rujukan")("tglKunjungan").ToString()

                            Dim hari As Integer = DateDiff(DateInterval.Day, deDATE_RUJUKAN.DateTime, Today())
                            If hari > 90 Then
                                txtNOMORRUJUKAN.ResetText()
                                MsgBox("Surat Rujukan Nomor : " & txtNOMORRUJUKAN.Text & " masa berlaku Habis, Maksimal 3(tiga) bulan dari tanggal rujukan Silahkan ke Faskes Perujuk untuk Perbaharui Rujukan", MsgBoxStyle.OkOnly, "Perhatian !!!")
                            End If

                            txtCATATAN.Text = DataDecrypt("rujukan")("keluhan").ToString()
                            InsertDiagnosa(DataDecrypt("rujukan")("diagnosa")("kode").ToString(), DataDecrypt("rujukan")("diagnosa")("kode").ToString() & " - " & DataDecrypt("rujukan")("diagnosa")("nama").ToString())
                            If DataDecrypt("rujukan")("peserta")("mr")("noMR").ToString() <> "" Then
                                fn_LoadCustomer(DataDecrypt("rujukan")("peserta")("mr")("noMR").ToString())
                            End If
                            If DataDecrypt("rujukan")("peserta")("jenisPeserta")("keterangan").ToString() <> "" Then
                                InsertJenisPeserta(DataDecrypt("rujukan")("peserta")("jenisPeserta")("kode").ToString(), DataDecrypt("rujukan")("peserta")("jenisPeserta")("keterangan").ToString())
                            End If
                            If DataDecrypt("rujukan")("peserta")("cob")("nmAsuransi").ToString() <> "" Then
                                InsertCOB(DataDecrypt("rujukan")("peserta")("cob")("noAsuransi").ToString(), DataDecrypt("rujukan")("peserta")("cob")("nmAsuransi").ToString(), DataDecrypt("rujukan")("peserta")("cob")("tglTAT").ToString(), DataDecrypt("rujukan")("peserta")("cob")("tglTMT").ToString())
                            End If
                            If DataDecrypt("rujukan")("provPerujuk")("nama").ToString() <> "" Then
                                InsertPPK(DataDecrypt("rujukan")("provPerujuk")("kode").ToString(), DataDecrypt("rujukan")("provPerujuk")("nama").ToString())
                            End If

                            grdKDKELASRAWAT.Text = DataDecrypt("rujukan")("peserta")("hakKelas")("kode").ToString()
                            grdKDKELASRAWAT_NAIKKELAS.Text = DataDecrypt("rujukan")("peserta")("hakKelas")("kode").ToString()

                            Dim Message As String = "Jenis Peserta : " & DataDecrypt("rujukan")("peserta")("jenisPeserta")("keterangan").ToString() _
                                              & vbCrLf _
                                              & "No RM di BPJS : " & DataDecrypt("rujukan")("peserta")("mr")("noMR").ToString() _
                                              & vbCrLf _
                                              & "NIK : " & DataDecrypt("rujukan")("peserta")("nik").ToString() _
                                              & vbCrLf _
                                              & "No Kartu : " & DataDecrypt("rujukan")("peserta")("noKartu").ToString() _
                                              & vbCrLf _
                                              & "No Telepon : " & DataDecrypt("rujukan")("peserta")("mr")("noTelepon").ToString() _
                                              & vbCrLf _
                                              & "Nama : " & DataDecrypt("rujukan")("peserta")("nama").ToString() _
                                              & vbCrLf _
                                              & "Tgl Cetak Kartu : " & DataDecrypt("rujukan")("peserta")("tglCetakKartu").ToString() _
                                              & vbCrLf _
                                              & "Umur Sekarang : " & DataDecrypt("rujukan")("peserta")("umur")("umurSekarang").ToString() _
                                              & vbCrLf _
                                              & "Tgl TAT : " & DataDecrypt("rujukan")("peserta")("tglTAT").ToString() _
                                              & vbCrLf _
                                              & "Tgl TMT : " & DataDecrypt("rujukan")("peserta")("tglTMT").ToString() _
                                              & vbCrLf _
                                              & "Hak Kelas : " & DataDecrypt("rujukan")("peserta")("hakKelas")("keterangan").ToString() _
                                              & vbCrLf _
                                              & vbCrLf _
                                              & "Status Peserta : " & DataDecrypt("rujukan")("peserta")("statusPeserta")("keterangan").ToString()

                            Dim frmPesertaBPJS As New frmPesertaBPJS
                            frmPesertaBPJS.LoadMe(DataDecrypt("rujukan")("peserta")("mr")("noMR").ToString(), Message)
                            frmPesertaBPJS.ShowDialog(Me)
                        Else
                            MsgBox(CodeResponse & " - " & messageResponse, MsgBoxStyle.Exclamation, Me.Text)
                        End If
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
    Private Sub fn_LoadKartuBPJSMultiRecord(ByVal sKARTUBPS As String)
        Try
            If txtKARTUBPJS.Text = "" Then Exit Sub

            If txtKARTUBPJS.Text.Count <> 13 Then
                txtKARTUBPJS.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                txtKARTUBPJS.ErrorText = Statement.ErrorRequired
                MsgBox(Statement.ErrorStatement & " No Kartu BPJS Tidak sama dengan 13 Digit", MsgBoxStyle.Exclamation, Me.Text)

                txtKARTUBPJS.Focus()
                Exit Sub
            End If

            Dim dsDataSetKoneksi = oSetKoneksi.GetData().FirstOrDefault(Function(x) x.ISACTIVE = True And x.NAME_DISPLAY = "VCLAIM")
            Dim uTime As Integer = 0

            If dsDataSetKoneksi IsNot Nothing Then
                uTime = (DateTime.UtcNow - New DateTime(1970, 1, 1, 0, 0, 0)).TotalSeconds
                Dim dsSetKoneksi = oSetKoneksi.CariRujukanKartuMultiRecord(dsDataSetKoneksi.ALAMATWEB, dsDataSetKoneksi.CONSID, dsDataSetKoneksi.SECREATKEY, dsDataSetKoneksi.USER_KEY, uTime, sKARTUBPS, cboASALRUJUKAN.SelectedIndex)

                If dsSetKoneksi <> "" Then
                    Try
                        Dim allData = JObject.Parse(dsSetKoneksi)

                        Dim CodeResponse As String = String.Empty
                        Dim messageResponse As String = String.Empty

                        CodeResponse = IIf(IsDBNull(allData.Item("metaData").Item("code")) = True, "", allData.Item("metaData").Item("code"))
                        messageResponse = allData("metaData")("message").ToString

                        If CodeResponse = "200" Then
                            Dim DataDecrypt = JObject.Parse(oSetKoneksi.Decrypt(allData("response"), dsDataSetKoneksi.CONSID & dsDataSetKoneksi.SECREATKEY & uTime))

                            Dim table As DataTable

                            table = New DataTable("M_RUJUKAN")

                            table.Columns.Add("noKunjungan")
                            table.Columns.Add("tglKunjungan")
                            table.Columns.Add("noKartu")
                            table.Columns.Add("nama")
                            table.Columns.Add("provPerujuk")
                            table.Columns.Add("poliRujukan")

                            For Each item In DataDecrypt("rujukan")
                                table.Rows.Add(New String() {item("noKunjungan"), item("tglKunjungan"), item("peserta")("noKartu"), item("peserta")("nama"), item("provPerujuk")("nama"), item("poliRujukan")("nama")})
                            Next

                            grdCARIRUJUKAN.Properties.DataSource = table

                            grdCARIRUJUKAN.Properties.ValueMember = "noKunjungan"
                            grdCARIRUJUKAN.Properties.DisplayMember = "noKunjungan"

                            fn_LoadFormatDataRujukan()

                            grdCARIRUJUKAN.ShowPopup()
                        Else
                            grvCARIRUJUKAN.Columns.Clear()
                            grdCARIRUJUKAN.Properties.DataSource = Nothing
                            grvCARIRUJUKAN.OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways

                            MsgBox(CodeResponse & " - " & messageResponse, MsgBoxStyle.Exclamation, Me.Text)
                        End If
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
    Private Sub fn_LoadFormatDataRujukan()
        For iLoop As Integer = 0 To grvCARIRUJUKAN.Columns.Count - 1
            If grvCARIRUJUKAN.Columns(iLoop).ColumnType.Name = "Decimal" Then
                grvCARIRUJUKAN.Columns(iLoop).DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                grvCARIRUJUKAN.Columns(iLoop).DisplayFormat.FormatString = "{0:n2}"
                grvCARIRUJUKAN.Columns(iLoop).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far

                grvCARIRUJUKAN.Columns(iLoop).SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                grvCARIRUJUKAN.Columns(iLoop).SummaryItem.DisplayFormat = "{0:n2}"
            ElseIf grvCARIRUJUKAN.Columns(iLoop).ColumnType.Name = "DateTime" Then
                grvCARIRUJUKAN.Columns(iLoop).DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
                grvCARIRUJUKAN.Columns(iLoop).DisplayFormat.FormatString = "{0:dd/MM/yyyy}"
            End If
        Next
    End Sub
    Private Sub fn_LoadNomorRujukan(ByVal sNOMORRUJUKAN As String)
        Try
            Dim dsDataSetKoneksi = oSetKoneksi.GetData().FirstOrDefault(Function(x) x.ISACTIVE = True And x.NAME_DISPLAY = "VCLAIM")
            Dim uTime As Integer = 0

            If dsDataSetKoneksi IsNot Nothing Then
                uTime = (DateTime.UtcNow - New DateTime(1970, 1, 1, 0, 0, 0)).TotalSeconds
                Dim dsSetKoneksi = oSetKoneksi.CariRujukan(dsDataSetKoneksi.ALAMATWEB, dsDataSetKoneksi.CONSID, dsDataSetKoneksi.SECREATKEY, dsDataSetKoneksi.USER_KEY, uTime, sNOMORRUJUKAN, cboASALRUJUKAN.SelectedIndex)

                If dsSetKoneksi <> "" Then
                    Try
                        Dim allData = JObject.Parse(dsSetKoneksi)
                        Dim CodeResponse As String = String.Empty
                        Dim messageResponse As String = String.Empty

                        CodeResponse = IIf(IsDBNull(allData.Item("metaData").Item("code")) = True, "", allData.Item("metaData").Item("code"))
                        messageResponse = allData("metaData")("message").ToString

                        If CodeResponse = "200" Then
                            Dim DataDecrypt = JObject.Parse(oSetKoneksi.Decrypt(allData("response"), dsDataSetKoneksi.CONSID & dsDataSetKoneksi.SECREATKEY & uTime))
                            Dim oDepartment As New Reference.clsDepartment
                            Dim dsDepartment = oDepartment.GetDatakodebpjs(DataDecrypt("rujukan")("poliRujukan")("kode").ToString())

                            If dsDepartment IsNot Nothing Then
                                grdKDDEPARTMENT.Text = dsDepartment.KDDEPARTMENT
                                fn_LoadDoctor(dsDepartment.KDDEPARTMENT)
                                grdKDDOCTOR.Focus()
                            Else
                                MsgBox("Kode Poli Tidak ditemukan di Database", MsgBoxStyle.Exclamation, Me.Text)
                            End If
                            txtNOMORRUJUKAN.Text = DataDecrypt("rujukan")("noKunjungan").ToString()
                            txtKARTUBPJS.Text = DataDecrypt("rujukan")("peserta")("noKartu").ToString()
                            deDATE_RUJUKAN.DateTime = DataDecrypt("rujukan")("tglKunjungan").ToString()

                            Dim hari As Integer = DateDiff(DateInterval.Day, deDATE_RUJUKAN.DateTime, Today())
                            If hari > 90 Then
                                txtNOMORRUJUKAN.ResetText()
                                MsgBox("Surat Rujukan Nomor : " & txtNOMORRUJUKAN.Text & " masa berlaku Habis, Maksimal 3(tiga) bulan dari tanggal rujukan Silahkan ke Faskes Perujuk untuk Perbaharui Rujukan", MsgBoxStyle.OkOnly, "Perhatian !!!")
                            End If

                            txtCATATAN.Text = DataDecrypt("rujukan")("keluhan").ToString()
                            InsertDiagnosa(DataDecrypt("rujukan")("diagnosa")("kode").ToString(), DataDecrypt("rujukan")("diagnosa")("kode").ToString() & " - " & DataDecrypt("rujukan")("diagnosa")("nama").ToString())
                            If DataDecrypt("rujukan")("peserta")("mr")("noMR").ToString() <> "" Then
                                fn_LoadCustomer(DataDecrypt("rujukan")("peserta")("mr")("noMR").ToString())
                            End If
                            If DataDecrypt("rujukan")("peserta")("jenisPeserta")("keterangan").ToString() <> "" Then
                                InsertJenisPeserta(DataDecrypt("rujukan")("peserta")("jenisPeserta")("kode").ToString(), DataDecrypt("rujukan")("peserta")("jenisPeserta")("keterangan").ToString())
                            End If
                            If DataDecrypt("rujukan")("peserta")("cob")("nmAsuransi").ToString() <> "" Then
                                InsertCOB(DataDecrypt("rujukan")("peserta")("cob")("noAsuransi").ToString(), DataDecrypt("rujukan")("peserta")("cob")("nmAsuransi").ToString(), DataDecrypt("rujukan")("peserta")("cob")("tglTAT").ToString(), DataDecrypt("rujukan")("peserta")("cob")("tglTMT").ToString())
                            End If
                            If DataDecrypt("rujukan")("provPerujuk")("nama").ToString() <> "" Then
                                InsertPPK(DataDecrypt("rujukan")("provPerujuk")("kode").ToString(), DataDecrypt("rujukan")("provPerujuk")("nama").ToString())
                            End If

                            grdKDKELASRAWAT.Text = DataDecrypt("rujukan")("peserta")("hakKelas")("kode").ToString()
                            grdKDKELASRAWAT_NAIKKELAS.Text = DataDecrypt("rujukan")("peserta")("hakKelas")("kode").ToString()

                            Dim Message As String = "Jenis Peserta : " & DataDecrypt("rujukan")("peserta")("jenisPeserta")("keterangan").ToString() _
                                              & vbCrLf _
                                              & "No RM di BPJS : " & DataDecrypt("rujukan")("peserta")("mr")("noMR").ToString() _
                                              & vbCrLf _
                                              & "NIK : " & DataDecrypt("rujukan")("peserta")("nik").ToString() _
                                              & vbCrLf _
                                              & "No Kartu : " & DataDecrypt("rujukan")("peserta")("noKartu").ToString() _
                                              & vbCrLf _
                                              & "No Telepon : " & DataDecrypt("rujukan")("peserta")("mr")("noTelepon").ToString() _
                                              & vbCrLf _
                                              & "Nama : " & DataDecrypt("rujukan")("peserta")("nama").ToString() _
                                              & vbCrLf _
                                              & "Tgl Cetak Kartu : " & DataDecrypt("rujukan")("peserta")("tglCetakKartu").ToString() _
                                              & vbCrLf _
                                              & "Umur Sekarang : " & DataDecrypt("rujukan")("peserta")("umur")("umurSekarang").ToString() _
                                              & vbCrLf _
                                              & "Tgl TAT : " & DataDecrypt("rujukan")("peserta")("tglTAT").ToString() _
                                              & vbCrLf _
                                              & "Tgl TMT : " & DataDecrypt("rujukan")("peserta")("tglTMT").ToString() _
                                              & vbCrLf _
                                              & "Hak Kelas : " & DataDecrypt("rujukan")("peserta")("hakKelas")("keterangan").ToString() _
                                              & vbCrLf _
                                              & vbCrLf _
                                              & "Status Peserta : " & DataDecrypt("rujukan")("peserta")("statusPeserta")("keterangan").ToString()

                            Dim frmPesertaBPJS As New frmPesertaBPJS
                            frmPesertaBPJS.LoadMe(DataDecrypt("rujukan")("peserta")("mr")("noMR").ToString(), Message)
                            frmPesertaBPJS.ShowDialog(Me)
                        Else
                            deDATE_RUJUKAN.DateTime = Now
                            txtNOMORRUJUKAN.ResetText()
                            grdKDDIAGNOSA.ResetText()
                            grdKDDEPARTMENT.ResetText()
                            grdKDDOCTOR.ResetText()
                            grdKDPPK.Reset()
                            grdKDDOCTOR.Properties.DataSource = Nothing
                            MsgBox(CodeResponse & " - " & messageResponse, MsgBoxStyle.Exclamation, Me.Text)
                        End If
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
    Private Sub fn_SuplesiNokartuV1(ByVal sNoKartuPeserta As String)
        Try
            Dim dsDataSetKoneksi = oSetKoneksi.GetData().FirstOrDefault(Function(x) x.ISACTIVE = True And x.NAME_DISPLAY = "VCLAIM")
            Dim uTime As Integer = 0

            If dsDataSetKoneksi IsNot Nothing Then
                uTime = (DateTime.UtcNow - New DateTime(1970, 1, 1, 0, 0, 0)).TotalSeconds
                Dim dsSetKoneksi = oSetKoneksi.GetDataSuplesiJasaRaharja(dsDataSetKoneksi.ALAMATWEB, dsDataSetKoneksi.CONSID, dsDataSetKoneksi.SECREATKEY, dsDataSetKoneksi.USER_KEY, uTime, sNoKartuPeserta, deDATE.DateTime.ToString("yyyy-MM-dd"))

                If dsSetKoneksi <> "" Then
                    Try
                        Dim allData = JObject.Parse(dsSetKoneksi)

                        Dim CodeResponse As String = String.Empty
                        Dim messageResponse As String = String.Empty

                        CodeResponse = IIf(IsDBNull(allData.Item("metaData").Item("code")) = True, "", allData.Item("metaData").Item("code"))
                        messageResponse = allData("metaData")("message").ToString

                        If CodeResponse = "200" Then
                            Dim DataDecrypt = JObject.Parse(oSetKoneksi.Decrypt(allData("response"), dsDataSetKoneksi.CONSID & dsDataSetKoneksi.SECREATKEY & uTime))

                            Dim table As DataTable

                            table = New DataTable("M_SUPLESI")

                            table.Columns.Add("noRegister")
                            table.Columns.Add("noSep")
                            table.Columns.Add("noSepAwal")
                            table.Columns.Add("noSuratJaminan")
                            table.Columns.Add("tglKejadian")
                            table.Columns.Add("tglSep")

                            For Each item In DataDecrypt("response")("jaminan")
                                table.Rows.Add(New String() {item("noRegister"), item("noSep"), item("noSepAwal"), item("noSuratJaminan"), item("tglKejadian"), item("tglSep")})
                            Next

                            grdSUPLESI.Properties.DataSource = table

                            grdSUPLESI.Properties.ValueMember = "noSep"
                            grdSUPLESI.Properties.DisplayMember = "noSep"

                            grdSUPLESI.ShowPopup()
                        Else
                            Dim table As DataTable
                            table = New DataTable("M_SUPLESI")

                            table.Columns.Add("noRegister")
                            table.Columns.Add("noSep")
                            table.Columns.Add("noSepAwal")
                            table.Columns.Add("noSuratJaminan")
                            table.Columns.Add("tglKejadian")
                            table.Columns.Add("tglSep")

                            grdSUPLESI.Properties.DataSource = table

                            MsgBox(CodeResponse & " - " & messageResponse, MsgBoxStyle.Exclamation, Me.Text)
                        End If
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
    Private Sub fn_DataIndukKecelakaan(ByVal sNoKartuPeserta As String)
        Try
            Dim dsDataSetKoneksi = oSetKoneksi.GetData().FirstOrDefault(Function(x) x.ISACTIVE = True And x.NAME_DISPLAY = "VCLAIM")
            Dim uTime As Integer = 0

            If dsDataSetKoneksi IsNot Nothing Then
                uTime = (DateTime.UtcNow - New DateTime(1970, 1, 1, 0, 0, 0)).TotalSeconds
                Dim dsSetKoneksi = oSetKoneksi.GetDataDataIndukKecelakaan(dsDataSetKoneksi.ALAMATWEB, dsDataSetKoneksi.CONSID, dsDataSetKoneksi.SECREATKEY, dsDataSetKoneksi.USER_KEY, uTime, sNoKartuPeserta)

                If dsSetKoneksi <> "" Then
                    Try
                        Dim allData = JObject.Parse(dsSetKoneksi)

                        Dim CodeResponse As String = String.Empty
                        Dim messageResponse As String = String.Empty

                        CodeResponse = IIf(IsDBNull(allData.Item("metaData").Item("code")) = True, "", allData.Item("metaData").Item("code"))
                        messageResponse = allData("metaData")("message").ToString

                        If CodeResponse = "200" Then
                            Dim DataDecrypt = JObject.Parse(oSetKoneksi.Decrypt(allData("response"), dsDataSetKoneksi.CONSID & dsDataSetKoneksi.SECREATKEY & uTime))

                            Dim table As DataTable

                            table = New DataTable("M_SUPLESI")

                            table.Columns.Add("noSep")
                            table.Columns.Add("tglKejadian")
                            table.Columns.Add("ppkPelSEP")
                            table.Columns.Add("kdProp")
                            table.Columns.Add("kdKab")
                            table.Columns.Add("kdKec")
                            table.Columns.Add("ketKejadian")
                            table.Columns.Add("noSEPSuplesi")

                            Dim oPropinsi As New Reference.clsPropinsi
                            Dim oKabupaten As New Reference.clsKabupaten
                            Dim oKecamatan As New Reference.clsKecamatan
                            Dim Propinsi As String = String.Empty
                            Dim Kabupaten As String = String.Empty
                            Dim Kecamatan As String = String.Empty

                            For Each item In DataDecrypt("list")
                                Dim dsPropinsi = oPropinsi.GetDatakodebpjs(item("kdProp"))
                                If dsPropinsi IsNot Nothing Then
                                    Propinsi = dsPropinsi.MEMO
                                Else
                                    Propinsi = item("kdProp")
                                End If
                                Dim dsKabupaten = oKabupaten.GetDatakodebpjs(item("kdKab"))
                                If dsKabupaten IsNot Nothing Then
                                    Kabupaten = dsKabupaten.MEMO
                                Else
                                    Kabupaten = item("kdKab")
                                End If
                                Dim dsKecamatan = oKecamatan.GetDatakodebpjs(item("kdKec"))
                                If dsKecamatan IsNot Nothing Then
                                    Kecamatan = dsKecamatan.MEMO
                                Else
                                    Kecamatan = item("kdKec")
                                End If

                                table.Rows.Add(New String() {item("noSep"), item("tglKejadian"), item("ppkPelSEP"), item("kdProp"), Propinsi, item("kdKab"), Kabupaten, item("kdKec"), Kecamatan, item("ketKejadian"), item("noSEPSuplesi")})
                            Next

                            grdSUPLESI.Properties.DataSource = table

                            grdSUPLESI.Properties.ValueMember = "noSEPSuplesi"
                            grdSUPLESI.Properties.DisplayMember = "noSEPSuplesi"

                            grdSUPLESI.ShowPopup()
                        Else
                            Dim table As DataTable
                            table = New DataTable("M_SUPLESI")

                            table.Columns.Add("noRegister")
                            table.Columns.Add("noSep")
                            table.Columns.Add("noSepAwal")
                            table.Columns.Add("noSuratJaminan")
                            table.Columns.Add("tglKejadian")
                            table.Columns.Add("tglSep")

                            grdSUPLESI.Properties.DataSource = table

                            MsgBox(CodeResponse & " - " & messageResponse, MsgBoxStyle.Exclamation, Me.Text)
                        End If
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
#End Region
#Region "Event Search"
    Private Sub txtKDCUSTOMER_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtKDCUSTOMER.KeyPress
        If Asc(e.KeyChar) = 13 Then
            If fn_ValidatePencarian() = True Then
                If chkRESISTERAWAL.Checked = False Then
                    fn_LoadCustomer(txtKDCUSTOMER.Text)
                    If chkIsOfline.Checked = False Then
                        Dim oPOLI As New Reference.clsDepartment
                        If grdKDDEPARTMENT.EditValue <> "IGD" Then
                            fn_LoadKartuBPJSSatuRecord(txtKARTUBPJS.Text)
                        End If
                    End If
                Else
                    fn_LoadPendaftaranRekamMedisList(txtKDCUSTOMER.Text)
                End If
            End If
        End If
    End Sub
    Private Sub grdCARIKDREGAWAL_KeyPress(sender As Object, e As KeyPressEventArgs) Handles grdCARIKDREGAWAL.KeyPress
        If Asc(e.KeyChar) = 13 Then
            If fn_ValidatePencarian() = True Then
                Dim dsPendaftaran = oPendaftaran.GetData(grdCARIKDREGAWAL.EditValue)
                If dsPendaftaran IsNot Nothing Then
                    txtKDPENDAFTARAN_AWAL.Text = dsPendaftaran.KDPENDAFTARAN
                    fn_LoadCustomer(dsPendaftaran.KDCUSTOMER)
                    If txtKDCUSTOMER.Text <> "" Then
                        fn_LoadHistoryPasien(txtKDCUSTOMER.Text)
                    End If
                    grdKDDEPARTMENT.Text = dsPendaftaran.KDDEPARTMENT
                    grdKDDIAGNOSA.Text = dsPendaftaran.KDDIAGNOSA
                    txtKDCUSTOMER.Text = dsPendaftaran.KDCUSTOMER
                    txtKARTUBPJS.Text = dsPendaftaran.KARTUBPJS
                    txtNOMORRUJUKAN.Text = dsPendaftaran.NOMORSEP
                    txtNOMORTELEPON.Text = dsPendaftaran.NOMORTELEPON
                    grdKDKELASRAWAT.Text = dsPendaftaran.KDKELASRAWAT
                    grdKDKELASRAWAT_NAIKKELAS.Text = dsPendaftaran.KDKELASRAWAT_NAIKKELAS
                    grdKDDAFTAR_L1.Text = dsPendaftaran.KDDAFTAR_L1
                    grdKDDAFTAR_L2.Text = dsPendaftaran.KDDAFTAR_L2
                    grdKDDAFTAR_L3.Text = dsPendaftaran.KDDAFTAR_L3
                    'grdKDDAFTAR_L4.Text = dsPendaftaran.KDDAFTAR_L4
                    'grdKDDAFTAR_L5.Text = dsPendaftaran.KDDAFTAR_L5
                    grdKDDAFTAR_L6.Text = dsPendaftaran.KDDAFTAR_L6
                    grdKDPPK.Text = dsPendaftaran.KDPPK
                    fn_LoadDoctor(grdKDDEPARTMENT.EditValue)
                    txtPEMBIAYAAN.Text = dsPendaftaran.PEMBIAYAAN
                    txtPENANGGUNGJAWAB.Text = dsPendaftaran.PENANGGUNGJAWAB
                    txtTUJUANKUNJUNGAN.Text = dsPendaftaran.TUJUANKUNJUNGAN
                    txtFLAGPROCEDURE.Text = dsPendaftaran.FLAGPROCEDURE
                    txtKDPENUNJANG.Text = dsPendaftaran.KDPENUNJANG
                    txtASSEMENTPEL.Text = dsPendaftaran.ASESMENTPEL
                    grdKDDOCTOR_PELAYANAN.Text = dsPendaftaran.KDDOCTOR_PELAYANAN
                    fn_LoadHistoryPasien(dsPendaftaran.KDCUSTOMER)
                End If
            End If
        End If
    End Sub
    Private Sub txtKARTUBPJS_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtKARTUBPJS.KeyPress
        If Asc(e.KeyChar) = 13 Then
            If fn_ValidatePencarian() = True Then
                If chkMultiRecord.Checked = False Then
                    fn_LoadKartuBPJSSatuRecord(txtKARTUBPJS.Text)
                Else
                    fn_LoadKartuBPJSMultiRecord(txtKARTUBPJS.Text)
                End If
            End If
        End If
    End Sub
    Private Sub grdCARIRUJUKAN_KeyPress(sender As Object, e As KeyPressEventArgs) Handles grdCARIRUJUKAN.KeyPress
        If Asc(e.KeyChar) = 13 Then
            If fn_ValidatePencarian() = True Then
                txtNOMORRUJUKAN.Text = grdCARIRUJUKAN.EditValue
                fn_LoadNomorRujukan(txtNOMORRUJUKAN.Text)
            End If
        End If
    End Sub
    Private Sub txtNOMORRUJUKAN_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtNOMORRUJUKAN.KeyPress
        If Asc(e.KeyChar) = 13 Then
            If fn_ValidatePencarian() = True Then
                fn_LoadNomorRujukan(txtNOMORRUJUKAN.Text)
            End If
        End If
    End Sub
    Private Sub txtCARISUPLESI_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtCARISUPLESI.KeyPress
        If Asc(e.KeyChar) = 13 Then
            If fn_ValidatePencarian() = True Then
                If txtCARISUPLESI.Text <> "" Then
                    fn_DataIndukKecelakaan(txtCARISUPLESI.Text.Trim.ToUpper)
                End If
            End If
        End If
    End Sub
    Private Sub grdSUPLESI_KeyPress(sender As Object, e As KeyPressEventArgs) Handles grdSUPLESI.KeyPress
        If Asc(e.KeyChar) = 13 Then
            txtPENJAMIN_SUPLESI_NOSEPSUPLESI.Text = grdSUPLESI.EditValue
            txtSUPLESI_PROPINSI.Text = grvSuplesi.GetFocusedRowCellValue("kdProp")
            txtSUPLESI_KABUPATEN.Text = grvSuplesi.GetFocusedRowCellValue("kdKab")
            txtSUPLESI_KECAMATAN.Text = grvSuplesi.GetFocusedRowCellValue("kdKec")
        End If
    End Sub
    Private Sub txtPENJAMIN_SUPLESI_NOSEPSUPLESI_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtPENJAMIN_SUPLESI_NOSEPSUPLESI.KeyPress
        If Asc(e.KeyChar) = 13 Then
            If fn_ValidatePencarian() = True Then
                fn_SuplesiNokartuV1(txtPENJAMIN_SUPLESI_NOSEPSUPLESI.Text)
            End If
        End If
    End Sub
    Private Sub txtNOMORSKDP_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtNOMORSKDP.KeyPress
        If Asc(e.KeyChar) = 13 Then
            If fn_ValidatePencarian() = True Then
                fn_LoadNomorSKD(txtNOMORSKDP.Text)
            End If
        End If
    End Sub
    Private Sub txtPEMBIAYAAN_SelectedIndexChanged(sender As Object, e As EventArgs) Handles txtPEMBIAYAAN.SelectedIndexChanged
        If isLoad = True Then
            If txtPEMBIAYAAN.SelectedIndex = 0 Then
                txtPENANGGUNGJAWAB.Text = "Pribadi"
            End If
        End If
    End Sub
    Private Sub grdKDKELASRAWAT_EditValueChanged(sender As Object, e As EventArgs) Handles grdKDKELASRAWAT.EditValueChanged
        If grdKDKELASRAWAT_NAIKKELAS.Text = "" Then
            grdKDKELASRAWAT_NAIKKELAS.Text = grdKDKELASRAWAT.EditValue
        End If
    End Sub
    Private Sub btnFinger_Click(sender As Object, e As EventArgs) Handles btnFinger.Click
        If txtKARTUBPJS.Text = "" Then Exit Sub

        If txtKARTUBPJS.Text.Count <> 13 Then
            txtKARTUBPJS.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
            txtKARTUBPJS.ErrorText = Statement.ErrorRequired
            MsgBox(Statement.ErrorStatement & " No Kartu BPJS Tidak sama dengan 13 Digit", MsgBoxStyle.Exclamation, Me.Text)
            Exit Sub
        End If

        fn_GetFingerPrint(txtKARTUBPJS.Text, Now.ToString("yyyy-MM-dd"))
    End Sub
    Private Function fn_GetFingerPrint(ByVal Kartu As String, ByVal TglPel As String) As Boolean
        Try
            Dim dsDataSetKoneksi = oSetKoneksi.GetData().FirstOrDefault(Function(x) x.ISACTIVE = True And x.NAME_DISPLAY = "VCLAIM")
            Dim uTime As Integer = 0

            If dsDataSetKoneksi IsNot Nothing Then
                uTime = (DateTime.UtcNow - New DateTime(1970, 1, 1, 0, 0, 0)).TotalSeconds
                Dim dsSetKoneksi = oSetKoneksi.GetFingerPrint(dsDataSetKoneksi.ALAMATWEB, dsDataSetKoneksi.CONSID, dsDataSetKoneksi.SECREATKEY, dsDataSetKoneksi.USER_KEY, uTime, Kartu, TglPel)

                If dsSetKoneksi <> "" Then
                    Dim allData = JObject.Parse(dsSetKoneksi)

                    Dim CodeResponse As String = String.Empty
                    Dim messageResponse As String = String.Empty

                    CodeResponse = IIf(IsDBNull(allData.Item("metaData").Item("code")) = True, "", allData.Item("metaData").Item("code"))
                    messageResponse = allData("metaData")("message").ToString

                    If CodeResponse = "200" Then
                        fn_GetFingerPrint = True
                        Dim DataDecrypt = JObject.Parse(oSetKoneksi.Decrypt(allData("response"), dsDataSetKoneksi.CONSID & dsDataSetKoneksi.SECREATKEY & uTime))

                        MsgBox(CodeResponse & " - " & DataDecrypt("status").ToString(), MsgBoxStyle.Exclamation, Me.Text)

                    Else
                        fn_GetFingerPrint = False
                        MsgBox(CodeResponse & " - " & messageResponse, MsgBoxStyle.Exclamation, Me.Text)
                    End If
                Else
                    fn_GetFingerPrint = False
                    MsgBox("Kosong Koneksi", MsgBoxStyle.Exclamation, Me.Text)
                End If
            Else
                fn_GetFingerPrint = False
                MsgBox("Cari Finger Print Data Gagal/Kosong", MsgBoxStyle.Exclamation, Me.Text)
            End If
        Catch oErr As Exception
            fn_GetFingerPrint = False
            fn_GetFingerPrint = Statement.ErrorStatement & vbCrLf & oErr.Message
        End Try
    End Function
    Private Sub btnDataSEPInternal_Click(sender As Object, e As EventArgs) Handles btnDataSEPInternal.Click
        Try
            If txtSEPINTERNAL.Text = "" Then Exit Sub

            Dim dsDataSetKoneksi = oSetKoneksi.GetData().FirstOrDefault(Function(x) x.ISACTIVE = True And x.NAME_DISPLAY = "VCLAIM")
            Dim uTime As Integer = 0

            If dsDataSetKoneksi IsNot Nothing Then
                uTime = (DateTime.UtcNow - New DateTime(1970, 1, 1, 0, 0, 0)).TotalSeconds
                Dim dsSetKoneksi = oSetKoneksi.DataSEPInternal(dsDataSetKoneksi.ALAMATWEB, dsDataSetKoneksi.CONSID, dsDataSetKoneksi.SECREATKEY, dsDataSetKoneksi.USER_KEY, uTime, txtSEPINTERNAL.Text)

                If dsSetKoneksi <> "" Then
                    Dim allData = JObject.Parse(dsSetKoneksi)

                    Dim CodeResponse As String = String.Empty
                    Dim messageResponse As String = String.Empty

                    CodeResponse = IIf(IsDBNull(allData.Item("metaData").Item("code")) = True, "", allData.Item("metaData").Item("code"))
                    messageResponse = allData("metaData")("message").ToString

                    If CodeResponse = "200" Then
                        Dim DataDecrypt = JObject.Parse(oSetKoneksi.Decrypt(allData("response"), dsDataSetKoneksi.CONSID & dsDataSetKoneksi.SECREATKEY & uTime))

                        Dim table As DataTable

                        table = New DataTable("M_INTERNAL")

                        table.Columns.Add("tujuanrujuk")
                        table.Columns.Add("nmtujuanrujuk")
                        table.Columns.Add("nmpoliasal")
                        table.Columns.Add("tglrujukinternal")
                        table.Columns.Add("nosep")
                        table.Columns.Add("kdpolituj")
                        table.Columns.Add("nmdokter")
                        table.Columns.Add("nosurat")

                        For Each item In DataDecrypt("list")
                            table.Rows.Add(New String() {item("tujuanrujuk"), item("nmtujuanrujuk"), item("nmpoliasal"), item("tglrujukinternal"), item("nosep"), item("kdpolituj"), item("nmdokter"), item("nosurat")})
                        Next

                        grdNOMORSEURATSEP.Properties.DataSource = table
                        grdNOMORSEURATSEP.Properties.ValueMember = "nosurat"
                        grdNOMORSEURATSEP.Properties.DisplayMember = "nosurat"

                        grdNOMORSEURATSEP.ShowPopup()
                    Else
                        MsgBox(CodeResponse & " - " & messageResponse, MsgBoxStyle.Exclamation, Me.Text)
                    End If
                Else
                    MsgBox("Kosong Koneksi", MsgBoxStyle.Exclamation, Me.Text)
                End If
            Else
                MsgBox("Cari Finger Print Data Gagal/Kosong", MsgBoxStyle.Exclamation, Me.Text)
            End If
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub grdNOMORSEURATSEP_KeyPress(sender As Object, e As KeyPressEventArgs) Handles grdNOMORSEURATSEP.KeyPress
        If txtKARTUBPJS.Text.Count <> 13 Then
            txtTGLRUJUKANINTERNAL.Text = grvNOMORSEURATSEP.GetFocusedRowCellValue("tglrujukinternal")
            txtKODEPOLIINTERNAL.Text = grvNOMORSEURATSEP.GetFocusedRowCellValue("kdpolituj")
        End If
    End Sub
    Private Sub btnHapusSEPInternal_Click(sender As Object, e As EventArgs) Handles btnHapusSEPInternal.Click
        Dim Pesan As String = String.Empty
        Try
            If grdNOMORSEURATSEP.Text = "" Then Exit Sub

            Dim dsDataSetKoneksi = oSetKoneksi.GetData().FirstOrDefault(Function(x) x.ISACTIVE = True And x.NAME_DISPLAY = "VCLAIM")
            Dim uTime As Integer = 0

            Dim jsonRequest As String = String.Empty

            jsonRequest = "{" & """request"": {" & """t_sep"": {" & """noSep"": """ & txtSEPINTERNAL.Text & """," & """noSurat"": """ & grdNOMORSEURATSEP.EditValue & """," & """tglRujukanInternal"": """ & txtTGLRUJUKANINTERNAL.Text & """," & """kdPoliTuj"": """ & txtKODEPOLIINTERNAL.Text & """," & """user"": """ & sUserID & """" & "}}}"

            If dsDataSetKoneksi IsNot Nothing Then
                uTime = (DateTime.UtcNow - New DateTime(1970, 1, 1, 0, 0, 0)).TotalSeconds
                Dim dsSetKoneksi = oSetKoneksi.HapusSEPInternal(dsDataSetKoneksi.ALAMATWEB, dsDataSetKoneksi.CONSID, dsDataSetKoneksi.SECREATKEY, dsDataSetKoneksi.USER_KEY, uTime, jsonRequest)

                If dsSetKoneksi <> "" Then
                    Dim allData = JObject.Parse(dsSetKoneksi)

                    Dim CodeResponse As String = String.Empty
                    Dim messageResponse As String = String.Empty

                    CodeResponse = IIf(IsDBNull(allData.Item("metaData").Item("code")) = True, "", allData.Item("metaData").Item("code"))
                    messageResponse = allData("metaData")("message").ToString

                    If CodeResponse = "200" Then
                        Pesan = allData("response")
                        Dim DataDecrypt = JObject.Parse(oSetKoneksi.Decrypt(allData("response"), dsDataSetKoneksi.CONSID & dsDataSetKoneksi.SECREATKEY & uTime))
                        MsgBox(CodeResponse & DataDecrypt.ToString(), MsgBoxStyle.Exclamation, Me.Text)
                    Else
                        MsgBox(CodeResponse & " - " & messageResponse, MsgBoxStyle.Exclamation, Me.Text)
                    End If
                Else
                    MsgBox("Kosong Koneksi", MsgBoxStyle.Exclamation, Me.Text)
                End If
            Else
                MsgBox("Cari Finger Print Data Gagal/Kosong", MsgBoxStyle.Exclamation, Me.Text)
            End If
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message & vbCrLf & Pesan, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub btnHapusSEP_Click(sender As Object, e As EventArgs) Handles btnHapusSEP.Click
        If txtSEPINTERNAL.Text = "" Then Exit Sub

        If MsgBox(Statement.DeleteQuestion, MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, Me.Text) = MsgBoxResult.No Then Exit Sub

        Dim frmPesanDelete As New frmPesanDelete
        frmPesanDelete.ShowDialog(Me)

        If sDeletePesan <> "" Then
            Dim oDelete As New Setting.clsDelete

            oDelete.InsertData("PENDAFTARAN", "DELETE", txtSEPINTERNAL.Text & "Tanggal " & Now & " Oleh " & sUserID & " Alasan " & sDeletePesan, "")

            If fn_CariSEP(txtSEPINTERNAL.Text.ToString.Trim.ToUpper) = "ADA" Then
                Try
                    Dim oSetKoneksi As New Brigging.clsSetKoneksi
                    Dim dsDataSetKoneksi = oSetKoneksi.GetData().FirstOrDefault(Function(x) x.ISACTIVE = True And x.NAME_DISPLAY = "VCLAIM")
                    Dim uTime As Integer = 0

                    If dsDataSetKoneksi IsNot Nothing Then
                        Dim jsonRequest As String = String.Empty

                        jsonRequest = "{" & """request"": {" & """t_sep"": {" & """noSep"": """ & txtSEPINTERNAL.Text.ToString.Trim.ToUpper & """," & """user"": """ & sUserID & """" & "}}}"

                        uTime = (DateTime.UtcNow - New DateTime(1970, 1, 1, 0, 0, 0)).TotalSeconds
                        Dim dsSetKoneksi = oSetKoneksi.HapusSEP(dsDataSetKoneksi.ALAMATWEB, dsDataSetKoneksi.CONSID, dsDataSetKoneksi.SECREATKEY, dsDataSetKoneksi.USER_KEY, uTime, jsonRequest)

                        If dsSetKoneksi <> "" Then
                            Dim allData = JObject.Parse(dsSetKoneksi)

                            Dim CodeResponse As String = String.Empty
                            Dim messageResponse As String = String.Empty

                            CodeResponse = IIf(IsDBNull(allData.Item("metaData").Item("code")) = True, "", allData.Item("metaData").Item("code"))
                            messageResponse = allData("metaData")("message").ToString

                            If CodeResponse = "200" Then
                                'Dim DataDecrypt = oSetKoneksi.Decrypt(allData("response"), dsDataSetKoneksi.CONSID & dsDataSetKoneksi.SECREATKEY & uTime)
                                MsgBox(CodeResponse & " - " & messageResponse, MsgBoxStyle.Exclamation, Me.Text)
                            Else
                                MsgBox(CodeResponse & " - " & messageResponse, MsgBoxStyle.Exclamation, Me.Text)
                            End If
                        Else
                            MsgBox("Delete SEP Data Gagal/Kosong", MsgBoxStyle.Exclamation, Me.Text)
                        End If
                    Else
                        MsgBox("Koneksi Tidak ditemukan", MsgBoxStyle.Exclamation, Me.Text)
                    End If
                Catch oErr As Exception
                    MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            End If

            MsgBox(Statement.DeleteSuccess, MsgBoxStyle.Information, Me.Text)

        End If
    End Sub
    Private Function fn_CariSEP(ByVal NoSEP As String) As String
        Try
            Dim oSetKoneksi As New Brigging.clsSetKoneksi
            Dim dsDataSetKoneksi = oSetKoneksi.GetData().FirstOrDefault(Function(x) x.ISACTIVE = True And x.NAME_DISPLAY = "VCLAIM")
            Dim uTime As Integer = 0

            If dsDataSetKoneksi IsNot Nothing Then
                uTime = (DateTime.UtcNow - New DateTime(1970, 1, 1, 0, 0, 0)).TotalSeconds
                Dim dsSetKoneksi = oSetKoneksi.CariSEP(dsDataSetKoneksi.ALAMATWEB, dsDataSetKoneksi.CONSID, dsDataSetKoneksi.SECREATKEY, dsDataSetKoneksi.USER_KEY, uTime, NoSEP)

                If dsSetKoneksi <> "" Then
                    Dim allData = JObject.Parse(dsSetKoneksi)

                    Dim CodeResponse As String = String.Empty
                    Dim messageResponse As String = String.Empty

                    CodeResponse = IIf(IsDBNull(allData.Item("metaData").Item("code")) = True, "", allData.Item("metaData").Item("code"))
                    messageResponse = allData("metaData")("message").ToString

                    If CodeResponse = "200" Then
                        fn_CariSEP = "ADA"
                        'Dim DataDecrypt = JObject.Parse(oSetKoneksi.Decrypt(allData("response"), dsDataSetKoneksi.CONSID & dsDataSetKoneksi.SECREATKEY & uTime))
                    Else
                        fn_CariSEP = "KOSONG"
                    End If
                Else
                    fn_CariSEP = "KOSONG"
                End If
            Else
                fn_CariSEP = "KOSONG"
            End If
        Catch oErr As Exception
            fn_CariSEP = "KOSONG"
        End Try
    End Function
    Private Sub grdKDPENJAMIN_EditValueChanged(sender As Object, e As EventArgs) Handles grdKDPENJAMIN.EditValueChanged
        If grdKDPENJAMIN.Text = "BPJS KESEHATAN" Then
            chkIsOfline.Checked = False
        Else
            chkIsOfline.Checked = True
        End If
    End Sub
    Private Sub txtTUJUANKUNJUNGAN_SelectedIndexChanged(sender As Object, e As EventArgs) Handles txtTUJUANKUNJUNGAN.SelectedIndexChanged
        If txtTUJUANKUNJUNGAN.SelectedIndex = 0 Then
            txtFLAGPROCEDURE.ResetText()
            txtKDPENUNJANG.ResetText()
            lFLAGPROCEDURE.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            lKDKUNJUNGAN.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        Else
            txtFLAGPROCEDURE.ResetText()
            txtKDPENUNJANG.ResetText()
            lFLAGPROCEDURE.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            lKDKUNJUNGAN.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        End If
    End Sub
    Private Sub txtTUJUANKUNJUNGAN_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtTUJUANKUNJUNGAN.KeyPress
        If Asc(e.KeyChar) = 13 Then
            txtTUJUANKUNJUNGAN.ResetText()
        End If
    End Sub
    Private Sub txtASSEMENTPEL_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtASSEMENTPEL.KeyPress
        If Asc(e.KeyChar) = 13 Then
            txtASSEMENTPEL.ResetText()
        End If
    End Sub
    Private Sub txtFLAGPROCEDURE_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtFLAGPROCEDURE.KeyPress
        If Asc(e.KeyChar) = 13 Then
            txtFLAGPROCEDURE.ResetText()
        End If
    End Sub
    Private Sub txtKDPENUNJANG_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtKDPENUNJANG.KeyPress
        If Asc(e.KeyChar) = 13 Then
            txtKDPENUNJANG.ResetText()
        End If
    End Sub
    Private Sub SimpleButton2_Click(sender As Object, e As EventArgs) Handles SimpleButton2.Click

    End Sub
#End Region
End Class