Imports DataAccess
Imports UI.WIN.MAIN.My.Resources
Imports System.Linq
Imports Newtonsoft.Json.Linq
Imports System.Data
Imports System.Data.SqlClient

Public Class frmKlaimRawatJalan
#Region "Declaration"
    Private oFormMode As FORM_MODE = FORM_MODE.FORM_MODE_VIEW
    Private sNoId As String
    Private isLoad As Boolean = False
    Private oKlaim As New EClaim.clsKlaim
    Private oSetKoneksi As New Brigging.clsSetKoneksi
    Private oPendaftaran As New Admission.clsPendaftaran
    Private sKDPENDAFTARAN As String = String.Empty
    Private sConnAvisena As String = String.Empty
#End Region
#Region "Function"
    Public Sub LoadMe(ByVal FormMode As Integer, Optional ByVal NoId As String = "")
        oFormMode = FormMode
        sNoId = NoId
        Dim dsKoneksiBriging = oPendaftaran.GetDataKoneksi("VCLAIM")
        If dsKoneksiBriging IsNot Nothing Then
            sConnAvisena = dsKoneksiBriging.KONEKSI_SIMRS
        End If
    End Sub
    Private Sub Form_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        fn_ChangeFormState()
        fn_LoadLanguage()
        isLoad = True
        txtCARI.Focus()
    End Sub
    Public Sub fn_LoadLanguage()
        Try
            Me.Text = "Grouper Rawat Jalan"

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
        fn_LoadDPJP()
        fn_LoadCOB()
        fn_LoadCARAKELUAR()
        fn_LoadKODETARIF()
        fn_LoadKDICD_X()
        fn_LoadKDICD_IX()

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
        btnICD.Enabled = Not Status
        btnUpdatePasien.Enabled = Not Status
        btnProsedur.Enabled = Not Status
        cboCaraBayar.Properties.ReadOnly = Status
        txtNoPeserta.Properties.ReadOnly = Status
        txtNoSEP.Properties.ReadOnly = Status
        grdCOB.Properties.ReadOnly = Status
        chkKelasEksekutif.Properties.ReadOnly = Status
        chkNaikKelas.Properties.ReadOnly = Status
        chkAdaRawat.Properties.ReadOnly = Status
        deDATEMASUK.Properties.ReadOnly = Status
        deDATEPULANG.Properties.ReadOnly = Status
        rbKELASPELAYANAN.ReadOnly = Status
        txtLAMA.Properties.ReadOnly = Status
        txtLOS.Properties.ReadOnly = Status
        txtADLScore_SubAcute.Properties.ReadOnly = True
        txtADLScore_Chronic.Properties.ReadOnly = True
        rbKELASHAK.Properties.ReadOnly = Status
        txtUmur.Properties.ReadOnly = Status
        txtBeratBadan.Properties.ReadOnly = Status
        grdCaraKeluar.Properties.ReadOnly = Status
        grdJenisTarif.Properties.ReadOnly = Status
        txttarifRumahSakit.Properties.ReadOnly = Status
        txtTarifEksekutif.Properties.ReadOnly = Status
        txtProsedurNonBedah.Properties.ReadOnly = Status
        txtTenagaAhli.Properties.ReadOnly = Status
        txtRadiologi.Properties.ReadOnly = Status
        txtRehabilitasi.Properties.ReadOnly = Status
        txtObat.Properties.ReadOnly = Status
        txtAlkes.Properties.ReadOnly = Status
        txtProsedurBedah.Properties.ReadOnly = Status
        txtKeperawatan.Properties.ReadOnly = Status
        txtLaboratorium.Properties.ReadOnly = Status
        txtKamarAkomodasi.Properties.ReadOnly = Status
        txtObatKronis.Properties.ReadOnly = Status
        txtBMHP.Properties.ReadOnly = Status
        txtKonsultasi.Properties.ReadOnly = Status
        txtPenunjang.Properties.ReadOnly = Status
        txtPelayananDarah.Properties.ReadOnly = Status
        txtRawatIntensif.Properties.ReadOnly = Status
        txtObatKemoTerapi.Properties.ReadOnly = Status
        txtSewaAlat.Properties.ReadOnly = Status
        txtICD_X.Properties.ReadOnly = Status
        txtICCD_IX.Properties.ReadOnly = Status
        txtRAWATINTENSIF_HARI.Properties.ReadOnly = Status
        txtVENTILATOR.Properties.ReadOnly = Status
        If oFormMode = FORM_MODE.FORM_MODE_ADD Then
            txtCARI.Properties.ReadOnly = False
            grdKDCASHIN.Properties.ReadOnly = False
            rbCategory.Properties.ReadOnly = False
        Else
            txtCARI.Properties.ReadOnly = True
            grdKDCASHIN.Properties.ReadOnly = True
            rbCategory.Properties.ReadOnly = True
        End If
        rbCATEGORY_SelectedIndexChanged()
    End Sub
    Private Sub fn_EmptyMe()
        txtCODE.Text = "<--- AUTO --->"
        chkKelasEksekutif.Checked = False
        cboCaraBayar.SelectedIndex = 0
        txtNoPeserta.ResetText()
        txtNoSEP.ResetText()
        grdJenisTarif.Text = oKlaim.KodeTarifKlaim_Default
        grdCOB.Text = oKlaim.COB_Default
        chkKelasEksekutif.Checked = False
        chkNaikKelas.Checked = False
        chkAdaRawat.Checked = False
        deDATEMASUK.DateTime = Now
        deDATEPULANG.DateTime = Now
        rbKELASPELAYANAN.SelectedIndex = 0
        txtLAMA.Text = 0
        txtLOS.Text = 1
        txtADLScore_SubAcute.Text = "-"
        txtADLScore_Chronic.Text = "-"
        txtHAKKELAS.Text = "-"
        rbKELASHAK.SelectedIndex = 0
        txtUmur.ResetText()
        txtBeratBadan.ResetText()
        grdCaraKeluar.Text = oKlaim.CaraKeluar_Default
        txttarifRumahSakit.ResetText()
        txtTarifEksekutif.ResetText()
        txtProsedurNonBedah.ResetText()
        txtTenagaAhli.ResetText()
        txtRadiologi.ResetText()
        txtRehabilitasi.ResetText()
        txtObat.ResetText()
        txtAlkes.ResetText()
        txtProsedurBedah.ResetText()
        txtKeperawatan.ResetText()
        txtLaboratorium.ResetText()
        txtKamarAkomodasi.ResetText()
        txtObatKronis.ResetText()
        txtBMHP.ResetText()
        txtKonsultasi.ResetText()
        txtPenunjang.ResetText()
        txtPelayananDarah.ResetText()
        txtRawatIntensif.ResetText()
        txtObatKemoTerapi.ResetText()
        txtSewaAlat.ResetText()
        txtICD_X.ResetText()
        txtICCD_IX.ResetText()
        txtRAWATINTENSIF_HARI.Text = 0
        txtVENTILATOR.Text = 0
    End Sub
    Private Sub fn_LoadData()
        Try
            Dim ds = oKlaim.GetData(sNoId)

            With ds
                txtCODE.Text = .KDPENDAFTARAN
                cboCaraBayar.SelectedIndex = .CARABAYAR
                txtNoPeserta.Text = .NOPESERTA
                txtNoSEP.Text = .NOSEP
                grdCOB.Text = .KDCOB
                rbCategory.SelectedIndex = .CATEGORY
                chkKelasEksekutif.Checked = .ISKELASEKSEKUTIF
                chkNaikKelas.Checked = .ISNAIKTURUNKELAS
                chkAdaRawat.Checked = .ISADARAWATINTENSIF
                deDATEMASUK.DateTime = .DATE_MASUK
                deDATEPULANG.DateTime = .DATE_KELUAR
                rbKELASPELAYANAN.SelectedIndex = .KELASPELAYANAN
                txtLAMA.Text = .LAMA
                txtLOS.Text = .LOS
                txtADLScore_SubAcute.Text = .ADLSCORE_SUBACUTE
                txtADLScore_Chronic.Text = .ADLSCORE_CHRONIC
                grdDPJP.Text = .DPJP
                txtHAKKELAS.Text = "-"
                rbKELASHAK.SelectedIndex = .HAKKELAS
                txtUmur.Text = .UMUR
                txtBeratBadan.Text = .BERATBADAN
                grdCaraKeluar.Text = .KDCARAKELUAR
                grdJenisTarif.Text = .JENISTARIF
                txttarifRumahSakit.Text = .TARIFRUMAHSAKIT
                txtTarifEksekutif.Text = .TARIFEKSEKUTIF
                txtProsedurNonBedah.Text = .PROSEDURNONBEDAH
                txtTenagaAhli.Text = .TENAGAAHLI
                txtRadiologi.Text = .RADIOLOGI
                txtRehabilitasi.Text = .REHABILITASI
                txtObat.Text = .OBAT
                txtAlkes.Text = .ALKES
                txtProsedurBedah.Text = .PROSEDURBEDAH
                txtKeperawatan.Text = .KEPERAWATAN
                txtLaboratorium.Text = .LABORATORIUM
                txtKamarAkomodasi.Text = .KAMARAKOMODASI
                txtObatKronis.Text = .OBATKRONIS
                txtBMHP.Text = .BMHP
                txtKonsultasi.Text = .KONSULTASI
                txtPenunjang.Text = .PENUNJANG
                txtPelayananDarah.Text = .PELAYANANDARAH
                txtRawatIntensif.Text = .RAWATINTENSIF
                txtObatKemoTerapi.Text = .OBATKEMOTERAPI
                txtSewaAlat.Text = .SEWAALAT
                txtICD_X.Text = .ICD_10
                txtICCD_IX.Text = .ICD_9
                txtRAWATINTENSIF_HARI.Text = .RAWATINTENSIF_HARI
                txtVENTILATOR.Text = .VENTILATOR
                fn_LoadData(.KDPENDAFTARAN)
            End With
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Function fn_AuotDaftar() As Boolean
        Try
            fn_AuotDaftar = True

            If grdDPJP.Text = String.Empty Then
                grdDPJP.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                grdDPJP.ErrorText = Statement.ErrorRequired

                grdDPJP.Focus()
                fn_AuotDaftar = False
                Exit Function
            End If

            If oFormMode = FORM_MODE.FORM_MODE_ADD Then
                If grdKDCASHIN.Text = String.Empty Then
                    grdKDCASHIN.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                    grdKDCASHIN.ErrorText = Statement.ErrorRequired

                    grdKDCASHIN.Focus()
                    fn_AuotDaftar = False
                    Exit Function
                End If

                Dim oCustomer As New Reference.clsCustomer
                Dim dsCustomer = oCustomer.GetData(grvKDCASHIN.GetFocusedRowCellValue("KDCUSTOMER"))
                If dsCustomer Is Nothing Then
                    MsgBox("Pasien Belum ada", MsgBoxStyle.Exclamation, Me.Text)

                    Dim frmCustomer As New frmCustomer
                    Try
                        frmCustomer.fn_LoadRM(grvKDCASHIN.GetFocusedRowCellValue("KDCUSTOMER"))
                        frmCustomer.LoadMe(FORM_MODE.FORM_MODE_ADD)
                        frmCustomer.ShowDialog(Me)
                    Catch oErr As Exception
                        MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
                    End Try

                    fn_AuotDaftar = False
                    Exit Function
                End If

                Dim oDepartment As New Reference.clsDepartment
                Dim dsDepartment = oDepartment.GetDataByNO_KLINIK(grvKDCASHIN.GetFocusedRowCellValue("Kode_Klinik"))
                If dsDepartment Is Nothing Then
                    MsgBox("Kode Polikinik belum ada", MsgBoxStyle.Exclamation, Me.Text)
                    fn_AuotDaftar = False
                    Exit Function
                End If
            End If

            Dim dsPendaftaran = oPendaftaran.GetData(txtCODE.Text)
            If dsPendaftaran Is Nothing Then
                If txtNoSEP.Text = String.Empty Then
                    txtNoSEP.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                    txtNoSEP.ErrorText = Statement.ErrorRequired

                    txtNoSEP.Focus()
                    fn_AuotDaftar = False
                    Exit Function
                End If
                If txtNoSEP.Text.Count <> 19 Then
                    MsgBox("Nomor SEP Tidak sama 19 digit", MsgBoxStyle.Exclamation, Me.Text)

                    txtNoSEP.Focus()
                    fn_AuotDaftar = False
                    Exit Function
                End If

                If fn_SavePendaftaran() = False Then
                    fn_AuotDaftar = False
                    Exit Function
                End If
            Else
                txtNoSEP.Text = dsPendaftaran.NOMORSEP
            End If
        Catch oErr As Exception
            fn_AuotDaftar = False
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Function
    Private Function fn_Validate() As Boolean
        Try
            fn_Validate = True

            If fn_AuotDaftar() = False Then
                fn_Validate = False
                Exit Function
            End If

            If txtCODE.Text = String.Empty Then
                txtCODE.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                txtCODE.ErrorText = Statement.ErrorRequired

                txtCODE.Focus()
                fn_Validate = False
                Exit Function
            End If

            If grdDPJP.Text = String.Empty Then
                grdDPJP.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                grdDPJP.ErrorText = Statement.ErrorRequired

                grdDPJP.Focus()
                fn_Validate = False
                Exit Function
            End If
            If txtNoPeserta.Text = String.Empty Then
                txtNoPeserta.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                txtNoPeserta.ErrorText = Statement.ErrorRequired

                txtNoPeserta.Focus()
                fn_Validate = False
                Exit Function
            End If

            If oFormMode = FORM_MODE.FORM_MODE_ADD Then
                Dim dsRegister = oKlaim.GetData(txtCODE.Text)
                If dsRegister IsNot Nothing Then
                    MsgBox("No Pendaftaran " & dsRegister.KDPENDAFTARAN & " Sudah di Grouper", MsgBoxStyle.Exclamation, Me.Text)
                    fn_Validate = False
                    Exit Function
                End If
            End If

        Catch oErr As Exception
            fn_Validate = False
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Function
    Private Function fn_Save() As Boolean
        Try
            ' ***** ECLAIM *****
            If fn_Membuatklaimbaru() = True Then
                If fn_MengisiUpdateDataKlaim() = False Then
                    fn_Save = False
                    Exit Function
                End If
            Else
                fn_Save = False
                Exit Function
            End If

            ' ***** HEADER *****
            Dim ds = oKlaim.GetStructureHeader
            With ds
                Try
                    .DATECREATED = oKlaim.GetData(txtCODE.Text).DATECREATED
                Catch oErr As Exception
                    .DATECREATED = Now
                End Try
                .DATEUPDATED = Now

                .KDPENDAFTARAN = txtCODE.Text
                .CARABAYAR = cboCaraBayar.SelectedIndex
                .NOPESERTA = txtNoPeserta.Text
                .NOSEP = txtNoSEP.Text
                .KDCOB = grdCOB.EditValue
                .CATEGORY = rbCategory.SelectedIndex
                .ISKELASEKSEKUTIF = chkKelasEksekutif.Checked
                .ISNAIKTURUNKELAS = chkNaikKelas.Checked
                .ISADARAWATINTENSIF = chkAdaRawat.Checked
                .DATE_MASUK = deDATEMASUK.DateTime
                .DATE_KELUAR = deDATEPULANG.DateTime
                .KELASPELAYANAN = rbKELASPELAYANAN.SelectedIndex
                .RAWATINTENSIF_HARI = CInt(txtRAWATINTENSIF_HARI.Text)
                .LOS = CInt(txtLOS.Text)
                .ADLSCORE_SUBACUTE = txtADLScore_SubAcute.Text
                .ADLSCORE_CHRONIC = txtADLScore_Chronic.Text
                .DPJP = grdDPJP.EditValue
                .HAKKELAS = rbKELASHAK.SelectedIndex
                .UMUR = txtUmur.Text
                .LAMA = txtLAMA.Text
                .VENTILATOR = CInt(txtVENTILATOR.Text)
                .BERATBADAN = CInt(txtBeratBadan.Text)
                .KDCARAKELUAR = grdCaraKeluar.EditValue
                .JENISTARIF = grdJenisTarif.EditValue
                .TARIFRUMAHSAKIT = txttarifRumahSakit.Text
                .TARIFEKSEKUTIF = txtTarifEksekutif.Text
                .PROSEDURNONBEDAH = txtProsedurNonBedah.Text
                .TENAGAAHLI = txtTenagaAhli.Text
                .RADIOLOGI = txtRadiologi.Text
                .REHABILITASI = txtRehabilitasi.Text
                .OBAT = txtObat.Text
                .ALKES = txtAlkes.Text
                .PROSEDURBEDAH = txtProsedurBedah.Text
                .KEPERAWATAN = txtKeperawatan.Text
                .LABORATORIUM = txtLaboratorium.Text
                .KAMARAKOMODASI = txtKamarAkomodasi.Text
                .OBATKRONIS = txtObatKronis.Text
                .BMHP = txtBMHP.Text
                .KONSULTASI = txtKonsultasi.Text
                .PENUNJANG = txtPenunjang.Text
                .PELAYANANDARAH = txtPelayananDarah.Text
                .RAWATINTENSIF = txtRawatIntensif.Text
                .OBATKEMOTERAPI = txtObatKemoTerapi.Text
                .SEWAALAT = txtSewaAlat.Text
                .ICD_10 = txtICD_X.Text
                .ICD_9 = txtICCD_IX.Text
                .MEMO = ""
                .KDUSER = sUserID
            End With

            If oFormMode = FORM_MODE.FORM_MODE_ADD Then
                Try
                    fn_Save = oKlaim.InsertData(ds)
                Catch oErr As Exception
                    MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            ElseIf oFormMode = FORM_MODE.FORM_MODE_EDIT Then
                Try
                    fn_Save = oKlaim.UpdateData(ds)
                Catch oErr As Exception
                    MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            End If

        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            fn_Save = False
        End Try
    End Function
    Private Function fn_SavePendaftaran() As Boolean
        Try
            ' ***** HEADER *****
            Dim oDepartment As New Reference.clsDepartment

            Dim ds = oPendaftaran.GetStructureHeader
            With ds
                .DATECREATED = Now
                .DATEUPDATED = Now
                .KDPENDAFTARAN = txtCODE.Text
                .KDPENDAFTARAN_AWAL = ""
                .NOMORSEP = txtNoSEP.Text
                .KDPENJAMIN = "PENJAMIN_0000000001"
                .KDDAFTAR_L1 = oPendaftaran.Daftar_L1_Default
                .KDDAFTAR_L2 = oPendaftaran.Daftar_L2_Default
                .KDDAFTAR_L3 = oPendaftaran.Daftar_L3_Default
                .KDDAFTAR_L4 = oPendaftaran.Daftar_L4_Default
                .KDDAFTAR_L5 = oPendaftaran.Daftar_L5_Default
                .KDDAFTAR_L6 = oPendaftaran.Daftar_L6_Default
                .STATUSDAFTAR = 0
                .NAMAKELUARGA = ""
                .KARTUBPJS = txtNoPeserta.Text.ToString.Trim.ToUpper
                .DATE = grvKDCASHIN.GetFocusedRowCellValue("TANGGAL")
                .PPKPELAYANAN = "0093R010"
                .CATEGORY = rbCategory.SelectedIndex
                .KDKELASRAWAT = oPendaftaran.Daftar_KELASRAWAT_Default
                .KDCUSTOMER = grvKDCASHIN.GetFocusedRowCellValue("KDCUSTOMER")
                .ASALRUJUKAN = 0
                .DATE_RUJUKAN = grvKDCASHIN.GetFocusedRowCellValue("TANGGAL")
                .NOMORRUJUKAN = ""
                .KDPPK = oPendaftaran.Daftar_PPK_Default
                .CATATAN = ""
                .KDDIAGNOSA = oPendaftaran.Daftar_DIAGNOSA_Default
                .KDDEPARTMENT = oDepartment.GetDataByNO_KLINIK(grvKDCASHIN.GetFocusedRowCellValue("Kode_Klinik")).KDDEPARTMENT
                .ISEKSEKUTIF = False
                .ISCOB = oPendaftaran.Daftar_COB_Default
                .ISKATARAK = False
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
                .NOMORSKDP = ""
                .KDDOCTOR = grdDPJP.EditValue
                .NOMORTELEPON = ""
                .KDUSER = sUserID
                .ISOFFLINE = True
                .REQUEST = "Auto"
                .RESPON = ""
                .INFORMASIPRB = ""
                .CETAK = 1
                .KDUPDATE_APLICARE = ""
                .EMAIL = ""
                .JALAN = ""
                .PROPINSI = ""
                .KOTA = ""
                .KECAMATAN = ""
                .KELURAHAN = ""
                .KODEPOS = ""
                .ISPASIENLAMA = IIf(grvKDCASHIN.GetFocusedRowCellValue("Lama") = "1", True, False)
            End With

            fn_SavePendaftaran = oPendaftaran.InsertData(ds, Nothing, txtCODE.Text.ToString)

        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            fn_SavePendaftaran = False
        End Try
    End Function
    Private Function fn_Membuatklaimbaru() As Boolean
        Try
            Dim dsPendaftaran = oPendaftaran.GetData(txtCODE.Text)

            Dim dsMembuatKlaimBaru = oSetKoneksi.fn_Membuatklaimbaru("VCLAIM", "{" & """metadata"": {" & """method"": " & """new_claim""    }," & """data"": {" & """nomor_kartu"": """ & txtNoPeserta.Text & """, " & """nomor_sep"": """ & txtNoSEP.Text & """, " & """nomor_rm"": """ & dsPendaftaran.KDCUSTOMER & """, " & """nama_pasien"": """ & dsPendaftaran.M_CUSTOMER.NAME_DISPLAY & """, " & """tgl_lahir"": """ & dsPendaftaran.M_CUSTOMER.TANGGALLAHIR.ToString("yyyy-MM-dd HH:mm:ss") & """, " & """gender"": """ & IIf(dsPendaftaran.M_CUSTOMER.KDJENISKELAMIN = 0, "1", "2") & """   } } ")

            If dsMembuatKlaimBaru <> "" Then
                Dim jsonDecode = JObject.Parse(dsMembuatKlaimBaru)
                Dim code = jsonDecode("metadata")("code").ToString
                Dim message = jsonDecode("metadata")("message").ToString

                If code = "200" Then
                    fn_Membuatklaimbaru = True
                ElseIf code = "400" Then
                    fn_Membuatklaimbaru = True
                    If oFormMode = FORM_MODE.FORM_MODE_ADD Then
                        MsgBox(Statement.ErrorStatement & vbCrLf & code & " - " & message, MsgBoxStyle.Information, Me.Text)
                    End If
                Else
                    fn_Membuatklaimbaru = False
                    MsgBox(Statement.ErrorStatement & vbCrLf & dsMembuatKlaimBaru, MsgBoxStyle.Exclamation, Me.Text)
                End If
            Else
                MsgBox("Koneksi Tidak ditemukan", MsgBoxStyle.Exclamation, Me.Text)
                fn_Membuatklaimbaru = False
            End If

        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            fn_Membuatklaimbaru = False
        End Try
    End Function
    Private Function fn_MengisiUpdateDataKlaim() As Boolean
        Try
            Dim dsPendaftaran = oPendaftaran.GetData(txtCODE.Text)
            Dim oSetUser As New Setting.clsUser

            Dim dsMembuatKlaimBaru = oSetKoneksi.fn_MengisiUpdateDataKlaim("VCLAIM", "{" & """metadata"": { " & """method"": " & """set_claim_data"",    " & """nomor_sep"": """ & txtNoSEP.Text & """  },  " & """data"": {    " & """nomor_sep"": """ & txtNoSEP.Text & """,    " & """nomor_kartu"": """ & txtNoPeserta.Text & """,    " & """tgl_masuk"": """ & deDATEMASUK.DateTime.ToString("yyyy-MM-dd HH:mm:ss") & """,    " & """tgl_pulang"": """ & deDATEPULANG.DateTime.ToString("yyyy-MM-dd HH:mm:ss") & """,    " & """jenis_rawat"": """ & "2" & """,    " & """kelas_rawat"": """ & IIf(chkKelasEksekutif.Checked = False, "3", "1") & """,    " & """adl_sub_acute"": """ & txtADLScore_SubAcute.Text & """,    " & """adl_chronic"": """ & txtADLScore_Chronic.Text & """,    " & """icu_indikator"": """ & "0" & """,    " & """icu_los"": """ & "0" & """,    " & """ventilator_hour"": """ & "0" & """,    " & """upgrade_class_ind"": """ & "0" & """,    " & """upgrade_class_class"": """ & "" & """,    " & """upgrade_class_los"": """ & 0 & """,    " & """add_payment_pct"": """ & 0 & """,    " & """birth_weight"": """ & txtBeratBadan.Text & """,    " & """discharge_status"": """ & grdCaraKeluar.EditValue & """,    " & """diagnosa"": """ & txtICD_X.Text & """,    " & """procedure"": """ & txtICCD_IX.Text & """,    " & """tarif_rs"": {      " & """prosedur_non_bedah"": """ & CInt(txtProsedurNonBedah.Text) & """,      " & """prosedur_bedah"": """ & CInt(txtProsedurBedah.Text) & """,      " & """konsultasi"": """ & CInt(txtKonsultasi.Text) & """,      " & """tenaga_ahli"": """ & CInt(txtTenagaAhli.Text) & """,      " & """keperawatan"": """ & CInt(txtKeperawatan.Text) & """,      " & """penunjang"": """ & CInt(txtPenunjang.Text) & """,      " & """radiologi"": """ & CInt(txtRadiologi.Text) & """,      " & """laboratorium"": """ & CInt(txtLaboratorium.Text) & """,      " & """pelayanan_darah"": """ & CInt(txtPelayananDarah.Text) & """,      " & """rehabilitasi"": """ & CInt(txtRehabilitasi.Text) & """,      " & """kamar"": """ & CInt(txtKamarAkomodasi.Text) & """,      " & """rawat_intensif"": """ & CInt(txtRawatIntensif.Text) & """,   " & """obat"": """ & CInt(txtObat.Text) & """,   " & """obat_kronis"": """ & CInt(txtObatKronis.Text) & """, " & """obat_kemoterapi"": """ & CInt(txtObatKemoTerapi.Text) & """,      " & """alkes"": """ & CInt(txtAlkes.Text) & """,      " & """bmhp"": """ & CInt(txtBMHP.Text) & """,      " & """sewa_alat"": """ & CInt(txtSewaAlat.Text) & """    },    " & """tarif_poli_eks"": """ & CInt(txtTarifEksekutif.Text) & """,    " & """nama_dokter"": """ & grdDPJP.Text & """,    " & """kode_tarif"": """ & grdJenisTarif.EditValue & """,    " & """payor_id"": """ & cboCaraBayar.Text & """,    " & """payor_cd"": """ & IIf(cboCaraBayar.Text = "JKN", "3", IIf(cboCaraBayar.Text = "JAMKESDA", "5", IIf(cboCaraBayar.Text = "JAMKESOS", "6", "1"))) & """,    " & """cob_cd"": """ & IIf(grdCOB.Text = "-", "#", grdCOB.EditValue) & """,    " & """coder_nik"": """ & oSetUser.GetData(sUserID).NIK & """  } } ")

            Dim jsonDecode = JObject.Parse(dsMembuatKlaimBaru)
            Dim code = jsonDecode("metadata")("code").ToString
            Dim message = jsonDecode("metadata")("message").ToString

            If code = "200" Then
                fn_MengisiUpdateDataKlaim = True
            ElseIf code = "400" Then
                fn_MengisiUpdateDataKlaim = False
                MsgBox(Statement.ErrorStatement & vbCrLf & code & " - " & message, MsgBoxStyle.Information, Me.Text)
            Else
                fn_MengisiUpdateDataKlaim = False
                MsgBox(Statement.ErrorStatement & vbCrLf & dsMembuatKlaimBaru, MsgBoxStyle.Exclamation, Me.Text)
            End If
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            fn_MengisiUpdateDataKlaim = False
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
            Case Keys.F5
                If btnAddCustomer.Enabled = True Then
                    btnAddCustomer_Click()
                End If
            Case Keys.F6
                If btnUpdatePasien.Enabled = True Then
                    btnUpdatePasien_Click()
                End If
        End Select
    End Sub
    Private Sub btnAddCustomer_Click() Handles btnAddCustomer.ItemClick
        Dim oCustomer As New Reference.clsCustomer
        Dim dsCustomer = oCustomer.GetData(grvKDCASHIN.GetFocusedRowCellValue("KDCUSTOMER"))
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
                frmCustomer.fn_LoadRM(grvKDCASHIN.GetFocusedRowCellValue("KDCUSTOMER"))
                frmCustomer.LoadMe(FORM_MODE.FORM_MODE_ADD)
                frmCustomer.ShowDialog(Me)
            Catch oErr As Exception
                MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            End Try
        End If

        If sCode = "" Then

        ElseIf sCode = "<---AUTO--->" Then

        Else
            txtCARI.Text = sCode
        End If

    End Sub
    Private Sub btnUpdatePasien_Click() Handles btnUpdatePasien.ItemClick
        Try
            Dim dsPendaftaran = oPendaftaran.GetData(txtCODE.Text)
            Dim Request As String = ""

            Dim dsMembuatKlaimBaru = oSetKoneksi.fn_UpdateDataPasien("VCLAIM", "{" & """metadata"": {" & """method"": " & """update_patient""    }," & """data"": {" & """nomor_kartu"": """ & txtNoPeserta.Text & """, " & """nomor_rm"": """ & dsPendaftaran.KDCUSTOMER & """, " & """nama_pasien"": """ & dsPendaftaran.M_CUSTOMER.NAME_DISPLAY & """, " & """tgl_lahir"": """ & dsPendaftaran.M_CUSTOMER.TANGGALLAHIR.ToString("yyyy-MM-dd HH:mm:ss") & """, " & """gender"": """ & IIf(dsPendaftaran.M_CUSTOMER.KDJENISKELAMIN = 0, "1", "2") & """   } } ")

            Dim jsonDecode = JObject.Parse(dsMembuatKlaimBaru)
            Dim code = jsonDecode("metadata")("code").ToString
            Dim message = jsonDecode("metadata")("message").ToString

            If code = "200" Then
                MsgBox(Statement.ErrorStatement & vbCrLf & code & " - " & message, MsgBoxStyle.Information, Me.Text)
            ElseIf code = "400" Then
                MsgBox(Statement.ErrorStatement & vbCrLf & code & " - " & message, MsgBoxStyle.Information, Me.Text)
            Else
                MsgBox(Statement.ErrorStatement & vbCrLf & dsMembuatKlaimBaru, MsgBoxStyle.Exclamation, Me.Text)
            End If
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
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
    Private Sub rbCATEGORY_SelectedIndexChanged() Handles rbCategory.SelectedIndexChanged
        If rbCategory.SelectedIndex = 0 Then
            lKELASEKSEKUTIF.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            lNAIKKELAS.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            lADARAWAT.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            lKELASHAKRJ.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            lKELASHAKRJ_LBL.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            lKELASHAKRI.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            If chkKelasEksekutif.Checked = False Then
                lTARIFEKSEKUTIF.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                txtTarifEksekutif.Text = "0"
            Else
                lTARIFEKSEKUTIF.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            End If
            lKELASPELAYANAN.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            LKELASPELAYANAN_RB.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            lLAMA_LBL.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            lLAMA.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            lRAWATINTENSIF_HARI.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            lRAWATINTENSIF_HARI_TEXT.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            lVENTILATOR.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            lVENTILATOR_TEXT.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            chkNaikKelas.Checked = False
            chkAdaRawat.Checked = False
            rbKELASHAK.SelectedIndex = 0
            rbKELASPELAYANAN.SelectedIndex = 0
            txtLAMA.Text = 0
            txtRAWATINTENSIF_HARI.Text = 0
            txtVENTILATOR.Text = 0
        Else
            lKELASEKSEKUTIF.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            lNAIKKELAS.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            lADARAWAT.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            lKELASHAKRJ.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            lKELASHAKRJ_LBL.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            lKELASHAKRI.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            lTARIFEKSEKUTIF.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            If chkNaikKelas.Checked = False Then
                lLAMA_LBL.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                lLAMA.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                lKELASPELAYANAN.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                LKELASPELAYANAN_RB.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                txtLAMA.Text = "0"
                rbKELASPELAYANAN.SelectedIndex = 0
            Else
                lLAMA_LBL.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                lLAMA.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                lKELASPELAYANAN.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                LKELASPELAYANAN_RB.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            End If
            If chkAdaRawat.Checked = False Then
                lRAWATINTENSIF_HARI.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                lRAWATINTENSIF_HARI_TEXT.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                lVENTILATOR.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                lVENTILATOR_TEXT.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                txtRAWATINTENSIF_HARI.Text = 0
                txtVENTILATOR.Text = 0
            Else
                lRAWATINTENSIF_HARI.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                lRAWATINTENSIF_HARI_TEXT.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                lVENTILATOR.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                lVENTILATOR_TEXT.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            End If
            txtTarifEksekutif.Text = "0"
            chkKelasEksekutif.Checked = False
        End If
    End Sub
    Private Sub chkKelasEksekutif_CheckedChanged(sender As Object, e As EventArgs) Handles chkKelasEksekutif.CheckedChanged
        If chkKelasEksekutif.Checked = False Then
            lTARIFEKSEKUTIF.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            txtTarifEksekutif.Text = "0"
        Else
            lTARIFEKSEKUTIF.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            txtTarifEksekutif.Text = "0"
        End If
    End Sub
    Private Sub chkNaikKelas_CheckedChanged(sender As Object, e As EventArgs) Handles chkNaikKelas.CheckedChanged
        If chkNaikKelas.Checked = False Then
            lLAMA_LBL.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            lLAMA.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            lKELASPELAYANAN.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            LKELASPELAYANAN_RB.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            txtLAMA.Text = "0"
            rbKELASPELAYANAN.SelectedIndex = 0
        Else
            lLAMA_LBL.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            lLAMA.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            lKELASPELAYANAN.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            LKELASPELAYANAN_RB.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            txtLAMA.Text = "0"
            rbKELASPELAYANAN.SelectedIndex = 0
        End If
    End Sub
    Private Sub chkAdaRawat_CheckedChanged(sender As Object, e As EventArgs) Handles chkAdaRawat.CheckedChanged
        If chkAdaRawat.Checked = False Then
            lRAWATINTENSIF_HARI.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            lRAWATINTENSIF_HARI_TEXT.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            lVENTILATOR.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            lVENTILATOR_TEXT.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            txtRAWATINTENSIF_HARI.Text = 0
            txtVENTILATOR.Text = 0
        Else
            lRAWATINTENSIF_HARI.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            lRAWATINTENSIF_HARI_TEXT.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            lVENTILATOR.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            lVENTILATOR_TEXT.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            txtRAWATINTENSIF_HARI.Text = 0
            txtVENTILATOR.Text = 0
        End If
    End Sub
    Private Sub fn_LoadKDICD_X()
        Dim oICD_X As New Reference.clsDiagnosa
        Try
            grdICD_X.Properties.DataSource = oICD_X.GetData.Where(Function(x) x.ISACTIVE = True).ToList()
            grdICD_X.Properties.ValueMember = "KDDIAGNOSA"
            grdICD_X.Properties.DisplayMember = "MEMO"
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadKDICD_IX()
        Dim oICD_IX As New Reference.clsProsedur
        Try
            grdICD_IX.Properties.DataSource = oICD_IX.GetData.Where(Function(x) x.ISACTIVE = True).ToList()
            grdICD_IX.Properties.ValueMember = "KDPROSEDUR"
            grdICD_IX.Properties.DisplayMember = "MEMO"
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadKODETARIF()
        Dim oKodeTarif As New Reference.clsKodeTarif_Klaim
        Try
            grdJenisTarif.Properties.DataSource = oKodeTarif.GetData.Where(Function(x) x.ISACTIVE = True).ToList()
            grdJenisTarif.Properties.ValueMember = "KODETARIF_KLAIM"
            grdJenisTarif.Properties.DisplayMember = "MEMO"
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadDPJP()
        Dim oDPJP As New Reference.clsDoctor
        Try
            grdDPJP.Properties.DataSource = oDPJP.GetData.Where(Function(x) x.ISACTIVE = True).ToList()
            grdDPJP.Properties.ValueMember = "KDDOCTOR"
            grdDPJP.Properties.DisplayMember = "NAME_DISPLAY"
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadCOB()
        Dim oCOB As New Reference.clsCOB
        Try
            grdCOB.Properties.DataSource = oCOB.GetData.Where(Function(x) x.ISACTIVE = True).ToList()
            grdCOB.Properties.ValueMember = "KDCOB"
            grdCOB.Properties.DisplayMember = "MEMO"
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadCARAKELUAR()
        Dim oCARAPULANG As New Reference.clsCaraKeluar
        Try
            grdCaraKeluar.Properties.DataSource = oCARAPULANG.GetData.Where(Function(x) x.ISACTIVE = True).ToList()
            grdCaraKeluar.Properties.ValueMember = "KDCARAKELUAR"
            grdCaraKeluar.Properties.DisplayMember = "MEMO"
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub txtCARI_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtCARI.KeyPress
        If Asc(e.KeyChar) = 13 Then
            If txtCARI.Text <> "" Then
                Try
                    Dim oDepartment As New Reference.clsDepartment
                    Dim oDoctor As New Reference.clsDoctor
                    Dim oConn As New SqlConnection
                    Dim oComm As New SqlCommand
                    Dim da As SqlDataAdapter
                    Dim ds As New DataSet
                    Dim SQL As String

                    Dim sConn As String = sConnAvisena

                    oConn = New SqlConnection(sConn)

                    If oConn.State = ConnectionState.Closed Then
                        oConn.Open()
                    End If

                    SQL = "SELECT "
                    SQL &= "KDPENDAFTARAN = A.No_Reg "
                    SQL &= ",TANGGAL = CONVERT(datetime, A.Tgl_Reg) "
                    SQL &= ",KDCUSTOMER = A.No_Medrec "
                    SQL &= ",PASIEN = C.Nama "
                    SQL &= ",TUJUAN = B.NAMA_KLINIK "
                    SQL &= ",A.NoID_Petugas "
                    SQL &= ",C.Tgl_Lahir "
                    SQL &= ",No_BPJS = ISNULL((C.No_BPJS), '') "
                    SQL &= ",NIK = ISNULL((C.NIK), '') "
                    SQL &= ",A.Lama "
                    SQL &= ",A.Kode_Klinik "
                    SQL &= "FROM REGISTRASI A "
                    SQL &= "INNER JOIN KLINIK B "
                    SQL &= "ON A.Kode_Klinik = B.NO_KLINIK "
                    SQL &= "INNER JOIN PASIEN C "
                    SQL &= "ON A.No_Medrec = C.No_Medrec "
                    If cboCARI.SelectedIndex = 0 Then
                        SQL &= "WHERE A.No_Reg = '" & txtCARI.Text & "' "
                    Else
                        SQL &= "WHERE A.No_Medrec = '" & txtCARI.Text & "' "
                    End If

                    oComm.Connection = oConn
                    oComm.CommandText = SQL
                    oComm.CommandTimeout = 120
                    oComm.CommandType = CommandType.Text

                    da = New SqlDataAdapter(oComm)
                    da.Fill(ds, "REGISTRASI")

                    grdKDCASHIN.Properties.DataSource = ds.Tables("REGISTRASI")
                    grdKDCASHIN.Properties.ValueMember = "KDPENDAFTARAN"
                    grdKDCASHIN.Properties.DisplayMember = "PASIEN"
                    grdKDCASHIN.ForceInitialize()

                    If oConn.State = ConnectionState.Open Then
                        oConn.Close()
                    End If

                    grdKDCASHIN.ShowPopup()

                Catch oErr As Exception
                    MsgBox("Load Data" & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            End If
        End If
    End Sub
    Private Sub grdKDCASHIN_KeyPress(sender As Object, e As KeyPressEventArgs) Handles grdKDCASHIN.KeyPress
        If Asc(e.KeyChar) = 13 Then
            txtCARI.ResetText()
            txtCODE.Text = grdKDCASHIN.EditValue
            txtNoPeserta.Text = grvKDCASHIN.GetFocusedRowCellValue("No_BPJS")
            deDATEMASUK.DateTime = grvKDCASHIN.GetFocusedRowCellValue("TANGGAL")
            deDATEPULANG.DateTime = grvKDCASHIN.GetFocusedRowCellValue("TANGGAL")

            Dim oDoctor As New Reference.clsDoctor
            Dim dsDoctor = oDoctor.GetDataByNO_IDPETUGAS(grvKDCASHIN.GetFocusedRowCellValue("NoID_Petugas"))
            If dsDoctor IsNot Nothing Then
                grdDPJP.Text = dsDoctor.KDDOCTOR
            End If
            txtUmur.Text = oPendaftaran.GetUmurPasien(grvKDCASHIN.GetFocusedRowCellValue("TANGGAL"), grvKDCASHIN.GetFocusedRowCellValue("Tgl_Lahir"))
            fn_LoadData(grdKDCASHIN.EditValue)
        End If
    End Sub
    Private Sub fn_LoadData(ByVal Paramater As String)
        Try
            Dim oConn As New SqlConnection
            Dim oComm As New SqlCommand
            Dim da As SqlDataAdapter
            Dim ds As New DataSet
            Dim SQL As String

            Dim sConn As String = sConnAvisena

            oConn = New SqlConnection(sConn)

            If oConn.State = ConnectionState.Closed Then
                oConn.Open()
            End If

            SQL = "SELECT "
            SQL &= "A.No_Reg "
            SQL &= ",A.Tgl_Reg "
            SQL &= ",A.No_Medrec "
            SQL &= "FROM REGISTRASI A "
            SQL &= "WHERE A.No_Reg = '" & Paramater & "' "

            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "S_SI_H")

            SQL = "SELECT "
            SQL &= "Transaksi = 'Rawat Jalan' "
            SQL &= ",TR.No_Reg "
            SQL &= ",Tgl_Tindakan = CONVERT(datetime, TR.Tgl_Tindakan) "
            SQL &= ",TM.NamaTindakan "
            SQL &= ",TotalTarifMargin = CONVERT(decimal(12,2), TR.TotalTarifMargin) "
            SQL &= ",KB.Kategori "
            SQL &= "FROM "
            SQL &= "TR_TINDAKAN_MEDIS TR "
            SQL &= "INNER JOIN TINDAKAN_MEDIS TM ON TR.ID_Tindakan = TM.NoID "
            SQL &= "INNER JOIN Kat_Billing KB ON TM.No_Billing = KB.No "
            SQL &= "WHERE "
            SQL &= "TR.No_Reg = '" & Paramater & "' "

            SQL &= "UNION "

            SQL &= "SELECT "
            SQL &= "Transaksi = 'Penunjang' "
            SQL &= ",TR.No_Reg "
            SQL &= ",Tgl_Tindakan = CONVERT(datetime, TR.Tgl_Tindakan) "
            SQL &= ",TM.NamaTindakan "
            SQL &= ",TotalTarifMargin = CONVERT(decimal(12,2), TR.TotalTarifMargin) "
            SQL &= ",KB.Kategori "
            SQL &= "FROM "
            SQL &= "TR_TINDAKAN_LAB TR "
            SQL &= "INNER JOIN TINDAKAN_MEDIS TM ON TR.ID_Tindakan = TM.NoID "
            SQL &= "INNER JOIN Kat_Billing KB ON TM.No_Billing = KB.No "
            SQL &= "WHERE "
            SQL &= "TR.No_Reg = '" & Paramater & "' "

            SQL &= "UNION "

            SQL &= "SELECT "
            SQL &= "Transaksi = 'Densus' "
            SQL &= ",TR.No_Reg "
            SQL &= ",Tgl_Tindakan = CONVERT(datetime, TR.Tgl_Tindakan) "
            SQL &= ",TM.NamaTindakan "
            SQL &= ",TotalTarifMargin = CONVERT(decimal(12,2), TR.TotalTarifMargin) "
            SQL &= ",KB.Kategori "
            SQL &= "FROM "
            SQL &= "TR_TINDAKAN_MEDIS_DENSUS TR "
            SQL &= "INNER JOIN TINDAKAN_MEDIS TM ON TR.ID_Tindakan = TM.NoID "
            SQL &= "INNER JOIN Kat_Billing KB ON TM.No_Billing = KB.No "
            SQL &= "WHERE "
            SQL &= "TR.No_Reg = '" & Paramater & "' "

            SQL &= "UNION "

            SQL &= "SELECT "
            SQL &= "Transaksi = 'xFarmasi' "
            SQL &= ",OJ.No_Reg "
            SQL &= ",Tgl_Tindakan = CONVERT(datetime, TR.Tanggal) "
            SQL &= ",NamaTindakan = O.Nama_Obat "
            SQL &= ",TotalTarifMargin = CONVERT(decimal(12,2), OJ.JUMLAH) "
            SQL &= ",Kategori = 'Obat' "
            SQL &= "FROM "
            SQL &= "TRANSAKSI_APOTIK TR "
            SQL &= "INNER JOIN ORD_JUAL_CORPORATE OJ ON TR.NO_TRANS = OJ.No_Trans "
            SQL &= "INNER JOIN OBAT O ON TR.Kode_Barang = O.Kode_Obat "
            SQL &= "WHERE "
            SQL &= "OJ.No_Reg = '" & Paramater & "' "

            SQL &= "UNION "

            SQL &= "SELECT "
            SQL &= "Transaksi = 'xFarmasi' "
            SQL &= ",OJ.No_Reg "
            SQL &= ",Tgl_Tindakan = CONVERT(datetime, TR.Tanggal) "
            SQL &= ",NamaTindakan = O.Nama_Obat "
            SQL &= ",TotalTarifMargin = CONVERT(decimal(12,2), OJ.JUMLAH) "
            SQL &= ",Kategori = 'Alkes' "
            SQL &= "FROM "
            SQL &= "TRANSAKSI_APOTIK TR "
            SQL &= "INNER JOIN ORD_JUAL_ALAT OJ ON TR.NO_TRANS = OJ.No_Trans "
            SQL &= "INNER JOIN OBAT O ON TR.Kode_Barang = O.Kode_Obat "
            SQL &= "WHERE "
            SQL &= "OJ.No_Reg = '" & Paramater & "' "

            SQL &= "UNION "

            SQL &= "SELECT "
            SQL &= "Transaksi = 'xFarmasi' "
            SQL &= ",OJ.No_Reg "
            SQL &= ",Tgl_Tindakan = CONVERT(datetime, TR.Tanggal) "
            SQL &= ",NamaTindakan = O.Nama_Obat "
            SQL &= ",TotalTarifMargin = CONVERT(decimal(12,2), OJ.JUMLAH) "
            SQL &= ",Kategori = 'Obat PRB' "
            SQL &= "FROM "
            SQL &= "TRANSAKSI_APOTIK TR "
            SQL &= "INNER JOIN ORD_JUAL_PRB OJ ON TR.NO_TRANS = OJ.No_Trans "
            SQL &= "INNER JOIN OBAT O ON TR.Kode_Barang = O.Kode_Obat "
            SQL &= "WHERE "
            SQL &= "OJ.No_Reg = '" & Paramater & "' "

            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "S_SI_D")

            Dim keyColumn As DataColumn = ds.Tables("S_SI_H").Columns("No_Reg")
            Dim foreignKeyColumn As DataColumn = ds.Tables("S_SI_D").Columns("No_Reg")
            ds.Relations.Add("FK_RELATION", keyColumn, foreignKeyColumn)

            grd.MainView = grv
            grd.DataSource = ds.Tables("S_SI_H")
            grd.ForceInitialize()

            grd.LevelTree.Nodes.Add("FK_RELATION", grv1)
            grv1.ViewCaption = "Details"

            grv1.PopulateColumns(ds.Tables("S_SI_D"))
            grv1.Columns("No_Reg").VisibleIndex = -1

            If oConn.State = ConnectionState.Open Then
                oConn.Close()
            End If

            fn_LoadFormatData()
            grv.BestFitColumns()

            Dim sProsedurNonBedah As Decimal = 0
            Dim sProsedurBedah As Decimal = 0
            Dim sKonsultasi As Decimal = 0
            Dim sTenagaAhli As Decimal = 0
            Dim sKeperawatan As Decimal = 0
            Dim sPenunjang As Decimal = 0
            Dim sRadiologi As Decimal = 0
            Dim sLaboratorium As Decimal = 0
            Dim sPelayananDarah As Decimal = 0
            Dim sRehabilitasi As Decimal = 0
            Dim sKamarAkomodasi As Decimal = 0
            Dim sRawatIntensif As Decimal = 0
            Dim sObat As Decimal = 0
            Dim sBMHP As Decimal = 0
            Dim sAlatMedis As Decimal = 0
            Dim sAlkes As Decimal = 0
            Dim sObatPRB As Decimal = 0

            For iLoop As Integer = 0 To ds.Tables("S_SI_D").Rows.Count - 1
                With ds.Tables("S_SI_D")
                    If .Rows(iLoop)("Kategori") = "Kosong" Then
                        MsgBox("Tindakan " & .Rows(iLoop)("NamaTindakan") & " Belum Masuk Kategori", MsgBoxStyle.Exclamation, Me.Text)
                    End If
                    If .Rows(iLoop)("Kategori") = "Prosedur Non Bedah" Then
                        sProsedurNonBedah += .Rows(iLoop)("TotalTarifMargin")
                    End If
                    If .Rows(iLoop)("Kategori") = "Prosedur Bedah" Then
                        sProsedurBedah += .Rows(iLoop)("TotalTarifMargin")
                    End If
                    If .Rows(iLoop)("Kategori") = "Konsultasi" Then
                        sKonsultasi += .Rows(iLoop)("TotalTarifMargin")
                    End If
                    If .Rows(iLoop)("Kategori") = "Tenaga Ahli" Then
                        sTenagaAhli += .Rows(iLoop)("TotalTarifMargin")
                    End If
                    If .Rows(iLoop)("Kategori") = "Keperawatan" Then
                        sKeperawatan += .Rows(iLoop)("TotalTarifMargin")
                    End If
                    If .Rows(iLoop)("Kategori") = "Penunjang" Then
                        sPenunjang += .Rows(iLoop)("TotalTarifMargin")
                    End If
                    If .Rows(iLoop)("Kategori") = "Radiologi" Then
                        sRadiologi += .Rows(iLoop)("TotalTarifMargin")
                    End If
                    If .Rows(iLoop)("Kategori") = "Laboratorium" Then
                        sLaboratorium += .Rows(iLoop)("TotalTarifMargin")
                    End If
                    If .Rows(iLoop)("Kategori") = "Pelayanan Darah" Then
                        sPelayananDarah += .Rows(iLoop)("TotalTarifMargin")
                    End If
                    If .Rows(iLoop)("Kategori") = "Rehabilitasi" Then
                        sRehabilitasi += .Rows(iLoop)("TotalTarifMargin")
                    End If
                    If .Rows(iLoop)("Kategori") = "Kamar / Akomodasi" Then
                        sKamarAkomodasi += .Rows(iLoop)("TotalTarifMargin")
                    End If
                    If .Rows(iLoop)("Kategori") = "Rawat Intensif" Then
                        sRawatIntensif += .Rows(iLoop)("TotalTarifMargin")
                    End If
                    If .Rows(iLoop)("Kategori") = "Obat" Then
                        sObat += .Rows(iLoop)("TotalTarifMargin")
                    End If
                    If .Rows(iLoop)("Kategori") = "BMHP" Then
                        sBMHP += .Rows(iLoop)("TotalTarifMargin")
                    End If
                    If .Rows(iLoop)("Kategori") = "Alat Medis" Then
                        sAlatMedis += .Rows(iLoop)("TotalTarifMargin")
                    End If
                    If .Rows(iLoop)("Kategori") = "Alkes" Then
                        sAlkes += .Rows(iLoop)("TotalTarifMargin")
                    End If
                    If .Rows(iLoop)("Kategori") = "Obat PRB" Then
                        sObatPRB += .Rows(iLoop)("TotalTarifMargin")
                    End If
                End With
            Next

            txtProsedurNonBedah.Text = sProsedurNonBedah
            txtProsedurBedah.Text = sProsedurBedah
            txtKonsultasi.Text = sKonsultasi
            txtTenagaAhli.Text = sTenagaAhli
            txtKeperawatan.Text = sKeperawatan
            txtPenunjang.Text = sPenunjang
            txtRadiologi.Text = sRadiologi
            txtLaboratorium.Text = sLaboratorium
            txtPelayananDarah.Text = sPelayananDarah
            txtRehabilitasi.Text = sRehabilitasi
            txtKamarAkomodasi.Text = sKamarAkomodasi
            txtRawatIntensif.Text = sRawatIntensif
            txtObat.Text = sObat
            txtBMHP.Text = sBMHP
            txtSewaAlat.Text = sAlatMedis
            txtAlkes.Text = sAlkes
            txtObatKronis.Text = sObatPRB
        Catch oErr As Exception
            MsgBox("Load Data" & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadFormatData()
        For iLoop As Integer = 0 To grv.Columns.Count - 1
            If grv.Columns(iLoop).ColumnType.Name = "Decimal" Then
                grv.Columns(iLoop).DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                grv.Columns(iLoop).DisplayFormat.FormatString = "{0:n2}"
                grv.Columns(iLoop).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
                grv.Columns(iLoop).SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                grv.Columns(iLoop).SummaryItem.DisplayFormat = "{0:n2}"
            ElseIf grv.Columns(iLoop).ColumnType.Name = "DateTime" Then
                grv.Columns(iLoop).DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
                grv.Columns(iLoop).DisplayFormat.FormatString = "{0:dd/MM/yyyy}"
            End If
        Next

        For iLoop As Integer = 0 To grv1.Columns.Count - 1
            If grv1.Columns(iLoop).ColumnType.Name = "Decimal" Then
                grv1.Columns(iLoop).DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                grv1.Columns(iLoop).DisplayFormat.FormatString = "{0:n2}"
                grv1.Columns(iLoop).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
                grv1.Columns(iLoop).SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                grv1.Columns(iLoop).SummaryItem.DisplayFormat = "{0:n2}"
            ElseIf grv1.Columns(iLoop).ColumnType.Name = "DateTime" Then
                grv1.Columns(iLoop).DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
                grv1.Columns(iLoop).DisplayFormat.FormatString = "{0:dd/MM/yyyy}"
            End If
        Next

        grv1.Columns("No_Reg").VisibleIndex = -1

        grv1.Columns("Transaksi").Group()
        grv1.ExpandAllGroups()

        grv1.Columns("TotalTarifMargin").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        grv1.Columns("TotalTarifMargin").DisplayFormat.FormatString = "{0:n0}"
        grv1.Columns("TotalTarifMargin").AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        grv1.Columns("TotalTarifMargin").SummaryItem.FieldName = "TotalTarifMargin"
        grv1.Columns("TotalTarifMargin").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
        grv1.Columns("TotalTarifMargin").SummaryItem.DisplayFormat = "{0:n0}"

    End Sub
    Private Sub OnValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtProsedurNonBedah.EditValueChanged, txtTenagaAhli.EditValueChanged, txtRadiologi.EditValueChanged, txtRehabilitasi.EditValueChanged _
                                      , txtObat.EditValueChanged, txtAlkes.EditValueChanged, txtProsedurBedah.EditValueChanged, txtKeperawatan.EditValueChanged, txtLaboratorium.EditValueChanged _
                                      , txtKamarAkomodasi.EditValueChanged, txtObatKronis.EditValueChanged, txtBMHP.EditValueChanged, txtKonsultasi.EditValueChanged _
                                      , txtPenunjang.EditValueChanged, txtPelayananDarah.EditValueChanged, txtRawatIntensif.EditValueChanged, txtObatKemoTerapi.EditValueChanged, txtSewaAlat.EditValueChanged
        If isLoad Then
            Calculate()
        End If
    End Sub
    Private Sub Calculate()
        Dim sSubTotal = 0

        sSubTotal = CDec(txtProsedurNonBedah.Text) + CDec(txtTenagaAhli.Text) + CDec(txtRadiologi.Text) + CDec(txtRehabilitasi.Text) _
                                  + CDec(txtObat.Text) + CDec(txtAlkes.Text) + CDec(txtProsedurBedah.Text) + CDec(txtKeperawatan.Text) + CDec(txtLaboratorium.Text) _
                                  + CDec(txtKamarAkomodasi.Text) + CDec(txtObatKronis.Text) + CDec(txtBMHP.Text) + CDec(txtKonsultasi.Text) _
                                  + CDec(txtPenunjang.Text) + CDec(txtPelayananDarah.Text) + CDec(txtRawatIntensif.Text) + CDec(txtObatKemoTerapi.Text) + CDec(txtSewaAlat.Text)

        txttarifRumahSakit.Text = sSubTotal

    End Sub
    Private Sub grdICD_X_KeyPress(sender As Object, e As KeyPressEventArgs) Handles grdICD_X.KeyPress
        If Asc(e.KeyChar) = 13 Then
            If grdICD_X.Text <> "" Then
                If txtICD_X.Text = "" Then
                    txtICD_X.Text = grdICD_X.EditValue
                Else
                    txtICD_X.Text = txtICD_X.Text & "#" & grdICD_X.EditValue
                End If
            End If
        End If
    End Sub
    Private Sub grdICD_IX_KeyPress(sender As Object, e As KeyPressEventArgs) Handles grdICD_IX.KeyPress
        If Asc(e.KeyChar) = 13 Then
            If grdICD_IX.Text <> "" Then
                If txtICCD_IX.Text = "" Then
                    txtICCD_IX.Text = grdICD_IX.EditValue
                Else
                    txtICCD_IX.Text = txtICCD_IX.Text & "#" & grdICD_IX.EditValue
                End If
            End If
        End If
    End Sub

#End Region
End Class