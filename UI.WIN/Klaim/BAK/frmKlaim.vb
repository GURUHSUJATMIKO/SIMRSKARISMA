Imports DataAccess
Imports UI.WIN.MAIN.My.Resources
Imports System.Linq
Imports System.Security.Cryptography
Imports System.Text
Imports Newtonsoft.Json.Linq

Public Class frmKlaim
#Region "Declaration"
    Private oFormMode As FORM_MODE = FORM_MODE.FORM_MODE_VIEW
    Private sNoId As String
    Private isLoad As Boolean = False
    Private oKlaim As New EClaim.clsKlaim
    Private oSetKoneksi As New Brigging.clsSetKoneksi
    Private oPendaftaran As New Admission.clsPendaftaran
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
            Me.Text = Klaim.TITLE

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
        fn_LoadCARAPULANG()
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
        btnProsedur.Enabled = Not Status

        cboCaraBayar.Properties.ReadOnly = Status
        txtNoPeserta.Properties.ReadOnly = Status
        txtNoSEP.Properties.ReadOnly = Status
        grdCOB.Properties.ReadOnly = Status
        If oFormMode = FORM_MODE.FORM_MODE_ADD Then
            rbCATEGORY.Properties.ReadOnly = False
        Else
            rbCATEGORY.Properties.ReadOnly = True
        End If
        chkKelasEksekutif.Properties.ReadOnly = Status
        chkNaikTurunKelas.Properties.ReadOnly = Status
        chkAdaRawatKelas.Properties.ReadOnly = Status
        deDATEMASUK.Properties.ReadOnly = Status
        deDATEPULANG.Properties.ReadOnly = Status
        rbKelasPelayanan.Properties.ReadOnly = Status
        txtRawatIntensif_hari.Properties.ReadOnly = Status
        txtLOS.Properties.ReadOnly = Status
        txtADLScore_SubAcute.Properties.ReadOnly = Status
        txtADLScore_Chronic.Properties.ReadOnly = Status
        rbKelasHak.Properties.ReadOnly = Status
        txtUmur.Properties.ReadOnly = Status
        txtLama.Properties.ReadOnly = Status
        txtVentilator.Properties.ReadOnly = Status
        txtBeratBadan.Properties.ReadOnly = Status
        grdCaraPulang.Properties.ReadOnly = Status
        'txtJenisTarif.Properties.ReadOnly = Status
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
    End Sub
    Private Sub fn_EmptyMe()
        txtCODE.Text = "<--- AUTO --->"
        chkKelasEksekutif.Checked = False
        chkNaikTurunKelas.Checked = False

        deDATEMASUK.DateTime = Now
        deDATEPULANG.DateTime = Now
    End Sub
    Private Sub fn_LoadData()
        Try
            Dim ds = oKlaim.GetData(sNoId)

            With ds
                txtCODE.Text = sNoId
                txtRegister.Text = .KDPENDAFTARAN
                cboCaraBayar.SelectedIndex = .CARABAYAR
                txtNoPeserta.Text = .NOPESERTA
                txtNoSEP.Text = .NOSEP
                grdCOB.Text = .KDCOB
                rbCATEGORY.SelectedIndex = .CATEGORY
                chkKelasEksekutif.Checked = .ISKELASEKSEKUTIF
                chkNaikTurunKelas.Checked = .ISNAIKTURUNKELAS
                chkAdaRawatKelas.Checked = .ISADARAWATINTENSIF
                deDATEMASUK.DateTime = .DATE_MASUK
                deDATEPULANG.DateTime = .DATE_KELUAR
                rbKelasPelayanan.SelectedIndex = .KELASPELAYANAN
                txtRawatIntensif_hari.Text = .RAWATINTENSIF_HARI
                txtLOS.Text = .LOS
                txtADLScore_SubAcute.Text = .ADLSCORE_SUBACUTE
                txtADLScore_Chronic.Text = .ADLSCORE_CHRONIC
                grdDPJP.Text = .DPJP
                rbKelasHak.SelectedIndex = .HAKKELAS
                txtUmur.Text = .UMUR
                txtLama.Text = .LAMA
                txtVentilator.Text = .VENTILATOR
                txtBeratBadan.Text = .BERATBADAN
                grdCaraPulang.Text = .KDCARAKELUAR
                txtJenisTarif.Text = .JENISTARIF
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
            End With
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Function fn_Validate() As Boolean
        Try
            fn_Validate = True
            If txtRegister.Text = String.Empty Then
                txtRegister.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                txtRegister.ErrorText = Statement.ErrorRequired

                txtRegister.Focus()
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
            Dim ds = oKlaim.GetStructureHeader
            With ds
                Try
                    .DATECREATED = oKlaim.GetData(sNoId).DATECREATED
                Catch oErr As Exception
                    .DATECREATED = Now
                End Try
                .DATEUPDATED = Now

                .KDPENDAFTARAN = sNoId
                .KDPENDAFTARAN = txtRegister.Text
                .CARABAYAR = cboCaraBayar.SelectedIndex
                .NOPESERTA = txtNoPeserta.Text
                .NOSEP = txtNoSEP.Text
                .KDCOB = grdCOB.EditValue
                .CATEGORY = rbCATEGORY.SelectedIndex
                .ISKELASEKSEKUTIF = chkKelasEksekutif.Checked
                .ISNAIKTURUNKELAS = chkNaikTurunKelas.Checked
                .ISADARAWATINTENSIF = chkAdaRawatKelas.Checked
                .DATE_MASUK = deDATEMASUK.DateTime
                .DATE_KELUAR = deDATEPULANG.DateTime
                .KELASPELAYANAN = rbKelasPelayanan.SelectedIndex
                .RAWATINTENSIF_HARI = txtRawatIntensif_hari.Text
                .LOS = txtLOS.Text
                .ADLSCORE_SUBACUTE = txtADLScore_SubAcute.Text
                .ADLSCORE_CHRONIC = txtADLScore_Chronic.Text
                .DPJP = grdDPJP.EditValue
                .HAKKELAS = rbKelasHak.SelectedIndex
                .UMUR = txtUmur.Text
                .LAMA = txtLama.Text
                .VENTILATOR = txtVentilator.Text
                .BERATBADAN = txtBeratBadan.Text
                .KDCARAKELUAR = grdCaraPulang.EditValue
                .JENISTARIF = txtJenisTarif.Text
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

            Try
                fn_SaveKlaim()
            Catch ex As Exception
                Exit Function
            End Try

        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            fn_Save = False
        End Try
    End Function
    Private Function fn_SaveKlaim() As Boolean
        Dim dsPendaftaran = oPendaftaran.GetData(txtRegister.Text)
        Dim Request As String = ""
        If dsPendaftaran IsNot Nothing Then
            Dim dsMembuatKlaimBaru = oSetKoneksi.fn_Membuatklaimbaru("VCLAIM", "{" & """metadata"": {" & """method"": " & """new_claim""    }," & """data"": {" & """nomor_kartu"": """ & dsPendaftaran.KARTUBPJS & """, " & """nomor_sep"": """ & txtNoSEP.Text & """, " & """nomor_rm"": """ & txtNoPeserta.Text & """, " & """nama_pasien"": """ & dsPendaftaran.M_CUSTOMER.NAME_DISPLAY & """, " & """tgl_lahir"": """ & dsPendaftaran.M_CUSTOMER.TANGGALLAHIR.ToString("yyyy-MM-dd HH:mm:ss") & """, " & """gender"": """ & IIf(dsPendaftaran.M_CUSTOMER.KDJENISKELAMIN = 0, "1", "2") & """   } } ")

            Dim jsonDecode = JObject.Parse(dsMembuatKlaimBaru)
            Dim code = jsonDecode("metadata")("code").ToString
            Dim message = jsonDecode("metadata")("message").ToString
            If code = "200" Then
                'Dim ds
            ElseIf code = "400" Then
                Dim UpdateDataPasien = JObject.Parse(oSetKoneksi.fn_UpdateDataPasien("VCLAIM", "{" & """metadata"": {" & """method"": " & """update_patient"", " & """nomor_rm"": """ & txtNoPeserta.Text & """    },    " & """data"": { " & """nomor_kartu"": """ & dsPendaftaran.KARTUBPJS & """, " & """nomor_rm"": """ & txtNoPeserta.Text & """, " & """nama_pasien & "": """ & dsPendaftaran.M_CUSTOMER.NAME_DISPLAY & """," & """tgl_lahir"": """ & dsPendaftaran.M_CUSTOMER.TANGGALLAHIR.ToString("yyyy-MM-dd HH:mm:ss") & """, " & """gender"": """ & IIf(dsPendaftaran.M_CUSTOMER.KDJENISKELAMIN = 0, "1", "2") & """    } } "))
                If UpdateDataPasien("metadata")("code").ToString = "200" Then
                    'Dim dsMengisiUpdateDataKlaim = JObject.Parse(oSetKoneksi.fn_Membuatklaimbaru("VCLAIM", "{" & """metadata"": { " & """method"": " & """set_claim_data"",    " & """nomor_sep"": """ & txtNoSEP.Text & """  },  " & """data"": {    " & """nomor_sep"": """ & txtNoSEP.Text & """,    " & """nomor_kartu"": """ & dsPendaftaran.KARTUBPJS & """,    " & """tgl_masuk"": """ & deDATEMASUK.DateTime.ToString("yyyy-MM-dd HH:mm:ss") & """,    " & """tgl_pulang"": """ & deDATEPULANG.DateTime.ToString("yyyy-MM-dd HH:mm:ss") & """,    " & """jenis_rawat"": """ & IIf(rbCATEGORY.SelectedIndex = 0, "1", "2") & """,    " & """kelas_rawat"": """ & IIf(rbKelasHak.SelectedIndex = 0, "1",IIf(rbKelasHak.SelectedIndex=1,"2","3")) & """,    " & """adl_sub_acute"": """ & txtADLScore_SubAcute .Text & """,    " & """adl_chronic"": """ & txtadl_choronic.Text & """,    " & """icu_indikator"": """ & txticu_indikator.Text & """,    " & """icu_los"": """ & txticu_los.Text & """,    " & """ventilator_hour"": """ & txtventilator_hour.Text & """,    " & """upgrade_class_ind"": """ & upgrade_class_ind & """,    " & """upgrade_class_class"": """ & upgrade_class_class & """,    " & """upgrade_class_los"": """ & txtUpgradeClassLos.Text & """,    " & """add_payment_pct"": """ & txtaddpaymentpct.Text & """,    " & """birth_weight"": """ & beralahir & """,    " & """discharge_status"": """ & caraPulang & """,    " & """diagnosa"": """ & KodeDiagnosa & """,    " & """procedure"": """ & KodeProsedur & """,    " & """tarif_rs"": {      " & """prosedur_non_bedah"": """ & CInt(txtPNonBedah.Text) & """,      " & """prosedur_bedah"": """ & CInt(txtPBedah.Text) & """,      " & """konsultasi"": """ & CInt(txtKonsultasi.Text) & """,      " & """tenaga_ahli"": """ & CInt(txtTenagaAhli.Text) & """,      " & """keperawatan"": """ & CInt(txtKeperawatan.Text) & """,      " & """penunjang"": """ & CInt(txtPenunjang.Text) & """,      " & """radiologi"": """ & CInt(txtRadiologi.Text) & """,      " & """laboratorium"": """ & CInt(txtLab.Text) & """,      " & """pelayanan_darah"": """ & CInt(txtPelayananDarah.Text) & """,      " & """rehabilitasi"": """ & CInt(txtRehabilitasi.Text) & """,      " & """kamar"": """ & CInt(txtKamar.Text) & """,      " & """rawat_intensif"": """ & CInt(txtRawatIntensif.Text) & """,   " & """obat"": """ & CInt(txtObat.Text) & """,   " & """obat_kronis"": """ & CInt(txtObatKronis.Text) & """, " & """obat_kemoterapi"": """ & CInt(txtObatKemoTerapi.Text) & """,      " & """alkes"": """ & CInt(txtAlkes.Text) & """,      " & """bmhp"": """ & CInt(txtBMHP.Text) & """,      " & """sewa_alat"": """ & CInt(txtSewaAlat.Text) & """    },    " & """tarif_poli_eks"": """ & CInt(txtTarifPoliEksekutif.Text) & """,    " & """nama_dokter"": """ & grdDOCTOR.Text & """,    " & """kode_tarif"": """ & KodeTarif & """,    " & """payor_id"": """ & txtpayor_id.Text & """,    " & """payor_cd"": """ & txtJaminan.Text & """,    " & """cob_cd"": """ & txtCOB.Text & """,    " & """coder_nik"": """ & sCoder_nik & """  } } "))
                Else
                    MsgBox(Statement.ErrorStatement & vbCrLf & UpdateDataPasien("metadata")("code").ToString & " - " & UpdateDataPasien("metadata")("message").ToString, MsgBoxStyle.Exclamation, Me.Text)
                End If
            Else
                MsgBox(Statement.ErrorStatement & vbCrLf & dsMembuatKlaimBaru, MsgBoxStyle.Exclamation, Me.Text)
            End If

        End If
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
    Private Sub fn_LoadCARAPULANG()
        Dim oCARAPULANG As New Reference.clsCaraKeluar
        Try
            grdCaraPulang.Properties.DataSource = oCARAPULANG.GetData.Where(Function(x) x.ISACTIVE = True).ToList()
            grdCaraPulang.Properties.ValueMember = "KDCARAKELUAR"
            grdCaraPulang.Properties.DisplayMember = "MEMO"
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub txtCARI_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtCARI.KeyPress
        If Asc(e.KeyChar) = 13 Then
            fn_LoadKDPENDAFTARAN(txtCARI.Text)
            txtCARI.ResetText()
        End If
    End Sub
    Private Sub fn_LoadKDPENDAFTARAN(ByVal Parameter As String)
        Dim oDEPARTMENT As New Reference.clsDepartment
        Try
            Dim oPendaftaran As New Admission.clsPendaftaran

            Dim dsPendaftaran = (From x In oPendaftaran.GetData()
                                 Where IIf(cboCARI.SelectedIndex = 0, x.KDCUSTOMER = Parameter, IIf(cboCARI.SelectedIndex = 1, x.M_CUSTOMER.NAME_DISPLAY = Parameter, x.KDPENDAFTARAN = Parameter))
                                 Select x.KDPENDAFTARAN, x.CATEGORY, x.KDCUSTOMER, x.M_CUSTOMER.NAME_DISPLAY, TUJUAN = x.M_DEPARTMENT.NAME_DISPLAY, DOKTER = x.M_DOCTOR.NAME_DISPLAY, x.DATE).OrderByDescending(Function(x) x.DATE)

            grdKDPENDAFTARAN.Properties.DataSource = dsPendaftaran.Where(Function(x) x.CATEGORY = rbCATEGORY.SelectedIndex).ToList()
            grdKDPENDAFTARAN.Properties.ValueMember = "KDPENDAFTARAN"
            grdKDPENDAFTARAN.Properties.DisplayMember = "KDPENDAFTARAN"

            grdKDPENDAFTARAN.ShowPopup()
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub grdKDPENDAFTARAN_KeyPress(sender As Object, e As KeyPressEventArgs) Handles grdKDPENDAFTARAN.KeyPress
        If Asc(e.KeyChar) = 13 Then
            Dim oPendaftaran As New Admission.clsPendaftaran
            Dim dsPendaftaran = oPendaftaran.GetData(grdKDPENDAFTARAN.EditValue)
            If dsPendaftaran IsNot Nothing Then
                deDATEMASUK.DateTime = dsPendaftaran.DATE
                rbCATEGORY.SelectedIndex = dsPendaftaran.CATEGORY
                txtRegister.Text = dsPendaftaran.KDPENDAFTARAN
                If dsPendaftaran.KDKELASRAWAT = "3" Then
                    rbKelasHak.SelectedIndex = 0
                ElseIf dsPendaftaran.KDKELASRAWAT = "4" Then
                    rbKelasHak.SelectedIndex = 1
                Else
                    rbKelasHak.SelectedIndex = 2
                End If

                txtNoPeserta.Text = dsPendaftaran.KDCUSTOMER
                txtNoSEP.Text = dsPendaftaran.NOMORSEP
            End If
        End If
    End Sub
    Private Sub rbCATEGORY_SelectedIndexChanged(sender As Object, e As EventArgs) Handles rbCATEGORY.SelectedIndexChanged
        'fn_HiddenCategory()
    End Sub
    Private Sub txtNoSEP_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtNoSEP.KeyPress
        If Asc(e.KeyChar) = 13 Then
            If txtNoSEP.Text.Count = 19 Then
                MsgBox(fn_IntegrasiSEPdenganInacbg(txtNoSEP.Text.ToString.Trim.ToUpper), MsgBoxStyle.Information, Me.Text)
            ElseIf txtNoSEP.Text.Count > 19 Then
                MsgBox("Validasi : " & vbCrLf & "No SEP lebih dari 19 Digit", MsgBoxStyle.Exclamation, Me.Text)
            Else
                MsgBox("Validasi : " & vbCrLf & "No SEP kurang dari 19 Digit", MsgBoxStyle.Exclamation, Me.Text)
            End If
        End If
    End Sub
    Private Function fn_IntegrasiSEPdenganInacbg(ByVal NOMORSEP As String) As String
        Try
            If NOMORSEP = String.Empty Then
                fn_IntegrasiSEPdenganInacbg = ""
                Exit Function
            End If

            Dim oSetKoneksi As New Brigging.clsSetKoneksi
            Dim dsSetKoneksi = oSetKoneksi.IntegrasiSEPdenganInacbg("VCLAIM", NOMORSEP)

            Dim allData = JObject.Parse(dsSetKoneksi)
            Dim CodeResponse As String = String.Empty
            Dim messageResponse As String = String.Empty

            CodeResponse = IIf(IsDBNull(allData.Item("metaData").Item("code")) = True, "", allData.Item("metaData").Item("code"))
            messageResponse = allData("metaData")("message").ToString

            If CodeResponse = "200" Then
                fn_IntegrasiSEPdenganInacbg = dsSetKoneksi
            Else
                fn_IntegrasiSEPdenganInacbg = dsSetKoneksi
            End If

        Catch oErr As Exception
            fn_IntegrasiSEPdenganInacbg = Statement.ErrorStatement & vbCrLf & oErr.Message
            'MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Function
#End Region
End Class