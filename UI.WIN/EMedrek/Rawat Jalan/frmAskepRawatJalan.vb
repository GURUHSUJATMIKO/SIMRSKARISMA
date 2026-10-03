Imports DataAccess
Imports UI.WIN.MAIN.My.Resources
Imports System.Linq
Imports System.Data.SqlClient
Imports DevExpress.XtraSplashScreen

Public Class frmAskepRawatJalan
#Region "Declaration"
    Private oFormMode As FORM_MODE = FORM_MODE.FORM_MODE_VIEW
    Private isLoad As Boolean = False
    Private oDigital As New Digital.clsDigital_AskepRawatJalan
    Private oDiagnosaPerawat As New Digital.clsDiagnosaPerawat
    Private down As Boolean = False
    Private sNoid As String
    Private sRM As String
    Private sNAMA As String
    Private sJENISKELAMIN As String
    Private sTANGGALLAHIR As DateTime
    Private sUSERPERAWAT_x As String
#End Region
#Region "Function"
    Public Sub LoadMe(ByVal FormMode As Integer, ByVal USERPERAWAT As String, ByVal RM As String, ByVal NAMA As String, ByVal JK As String, ByVal TANGGALLAHIR As DateTime, ByVal NoId As String)
        oFormMode = FormMode

        sNoid = NoId
        sRM = RM
        sNAMA = NAMA
        sJENISKELAMIN = JK
        sTANGGALLAHIR = TANGGALLAHIR
        sUSERPERAWAT_x = USERPERAWAT
    End Sub
    Private Sub Form_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.Text = ""
        fn_ChangeFormState()
        isLoad = True
    End Sub
    Private Sub Form_Closing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        Dispose()
    End Sub
    Private Overloads Sub Dispose()
        MyBase.Dispose()
        Me.Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
    Private Sub fn_ChangeFormState()
        fn_LoadKDITEMDIAGNOSAPERAWAT()
        fn_LoadHistoryAsesmen()

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

        deDATE.Properties.ReadOnly = Status

        chkALERGI_TIDAK.Properties.ReadOnly = Status
        chkALERGI_YA.Properties.ReadOnly = Status
        txtALERGI_YA_TEXT.Properties.ReadOnly = Status
        chkAUTO.Properties.ReadOnly = Status
        chkALLO.Properties.ReadOnly = Status
        txtALLO_TEXT.Properties.ReadOnly = Status
        txtKELUHAN_UTAMA.Properties.ReadOnly = Status
        txtKESADARAN_UMUM.Properties.ReadOnly = Status
        txtKEADAAN.Properties.ReadOnly = Status
        txtTANDA_VITAL_TD.Properties.ReadOnly = Status
        txtTANDA_VITAL_NADI.Properties.ReadOnly = Status
        txtTANDA_VITAL_SUHU.Properties.ReadOnly = Status
        txtTANDA_VITAL_R.Properties.ReadOnly = Status
        chkTANDA_VITAL_REGULER.Properties.ReadOnly = Status
        chkTANDA_VITAL_IREGULER.Properties.ReadOnly = Status
        txtTANDA_VITAL_SPO2.Properties.ReadOnly = Status
        txtTANDA_VITAL_BB.Properties.ReadOnly = Status
        txtTANDA_VITAL_TB.Properties.ReadOnly = Status
        txtTANDA_VITAL_LD.Properties.ReadOnly = Status
        txtTANDA_VITAL_LK.Properties.ReadOnly = Status
        chkSTATUS_PSIKOLOGIS_01.Properties.ReadOnly = Status
        chkSTATUS_PSIKOLOGIS_02.Properties.ReadOnly = Status
        chkSTATUS_PSIKOLOGIS_03.Properties.ReadOnly = Status
        chkSTATUS_PSIKOLOGIS_04.Properties.ReadOnly = Status
        chkSTATUS_PSIKOLOGIS_05.Properties.ReadOnly = Status
        chkSTATUS_PSIKOLOGIS_06.Properties.ReadOnly = Status
        txtSTATUS_PSIKOLOGIS_06_TEXT.Properties.ReadOnly = Status

        chkSTATUS_MENTAL_01.Properties.ReadOnly = Status
        chkSTATUS_MENTAL_02.Properties.ReadOnly = Status
        txtSTATUS_MENTAL_02_TEXT.Properties.ReadOnly = Status
        chkSTATUS_MENTAL_03.Properties.ReadOnly = Status
        txtSTATUS_MENTAL_03_TEXT.Properties.ReadOnly = Status

        chkSOSIAL_01_1.Properties.ReadOnly = Status
        chkSOSIAL_01_2.Properties.ReadOnly = Status
        chkSOSIAL_02_1.Properties.ReadOnly = Status
        chkSOSIAL_02_2.Properties.ReadOnly = Status
        chkSOSIAL_02_3.Properties.ReadOnly = Status
        chkSOSIAL_02_4.Properties.ReadOnly = Status
        txtSOSIAL_02_4_TEXT.Properties.ReadOnly = Status

        txtSOSIAL_03_1_TEXT.Properties.ReadOnly = Status
        txtSOSIAL_03_2_TEXT.Properties.ReadOnly = Status
        txtSOSIAL_03_3_TEXT.Properties.ReadOnly = Status

        txtSTATUS_SPIRITUAL_01_TEXT.Properties.ReadOnly = Status
        txtSTATUS_SPIRITUAL_02_TEXT.Properties.ReadOnly = Status

        chkPERSYARAFAN_01.Properties.ReadOnly = Status
        chkPERSYARAFAN_02.Properties.ReadOnly = Status
        chkPERSYARAFAN_03.Properties.ReadOnly = Status
        chkPERSYARAFAN_04.Properties.ReadOnly = Status
        chkPERSYARAFAN_05.Properties.ReadOnly = Status
        txtPERSYARAFAN_05_TEXT.Properties.ReadOnly = Status

        chkPERNAPASAN_01.Properties.ReadOnly = Status
        chkPERNAPASAN_02.Properties.ReadOnly = Status
        chkPERNAPASAN_03.Properties.ReadOnly = Status
        chkPERNAPASAN_04.Properties.ReadOnly = Status
        txtPERNAPASAN_04_TEXT.Properties.ReadOnly = Status

        chkPENCERNAAN_01.Properties.ReadOnly = Status
        chkPENCERNAAN_02.Properties.ReadOnly = Status
        chkPENCERNAAN_03.Properties.ReadOnly = Status
        chkPENCERNAAN_04.Properties.ReadOnly = Status
        chkPENCERNAAN_05.Properties.ReadOnly = Status
        txtPENCERNAAN_05_TEXT.Properties.ReadOnly = Status

        chkENDROKIN_01.Properties.ReadOnly = Status
        chkENDROKIN_02.Properties.ReadOnly = Status
        chkENDROKIN_03.Properties.ReadOnly = Status
        chkENDROKIN_04.Properties.ReadOnly = Status
        chkENDROKIN_05.Properties.ReadOnly = Status
        txtENDROKIN_05_TEXT.Properties.ReadOnly = Status

        chkCARDIOVASKULER_01.Properties.ReadOnly = Status
        chkCARDIOVASKULER_02.Properties.ReadOnly = Status
        chkCARDIOVASKULER_03.Properties.ReadOnly = Status
        chkCARDIOVASKULER_04.Properties.ReadOnly = Status
        txtCARDIOVASKULER_04_TEXT.Properties.ReadOnly = Status

        chkABDOMEN_01.Properties.ReadOnly = Status
        chkABDOMEN_02.Properties.ReadOnly = Status
        chkABDOMEN_03.Properties.ReadOnly = Status
        chkABDOMEN_04.Properties.ReadOnly = Status
        chkABDOMEN_05.Properties.ReadOnly = Status
        txtABDOMEN_05_TEXT.Properties.ReadOnly = Status
        chkABDOMEN_06.Properties.ReadOnly = Status
        txtABDOMEN_06_TEXT.Properties.ReadOnly = Status
        chkABDOMEN_07.Properties.ReadOnly = Status
        txtABDOMEN_07_TEXT.Properties.ReadOnly = Status
        chkABDOMEN_08.Properties.ReadOnly = Status
        txtABDOMEN_08_TEXT.Properties.ReadOnly = Status
        chkABDOMEN_09.Properties.ReadOnly = Status
        txtABDOMEN_09_TEXT.Properties.ReadOnly = Status

        chkREPRODUKSI_01.Properties.ReadOnly = Status
        chkREPRODUKSI_01_1.Properties.ReadOnly = Status
        chkREPRODUKSI_01_2.Properties.ReadOnly = Status
        chkREPRODUKSI_02.Properties.ReadOnly = Status
        chkREPRODUKSI_02_1.Properties.ReadOnly = Status
        txtREPRODUKSI_02_1_TEXT.Properties.ReadOnly = Status
        chkREPRODUKSI_02_2.Properties.ReadOnly = Status
        chkREPRODUKSI_03.Properties.ReadOnly = Status
        chkREPRODUKSI_03_1.Properties.ReadOnly = Status
        txtREPRODUKSI_03_1_TEXT.Properties.ReadOnly = Status
        chkREPRODUKSI_03_2.Properties.ReadOnly = Status
        chkREPRODUKSI_04.Properties.ReadOnly = Status
        txtREPRODUKSI_04_TEXT.Properties.ReadOnly = Status
        chkREPRODUKSI_05.Properties.ReadOnly = Status
        txtREPRODUKSI_05_TEXT.Properties.ReadOnly = Status
        chkREPRODUKSI_06.Properties.ReadOnly = Status
        txtREPRODUKSI_06_TEXT.Properties.ReadOnly = Status
        chkREPRODUKSI_07.Properties.ReadOnly = Status
        txtREPRODUKSI_07_TEXT.Properties.ReadOnly = Status
        chkREPRODUKSI_08.Properties.ReadOnly = Status
        txtREPRODUKSI_08_TEXT.Properties.ReadOnly = Status

        chkKULIT_01.Properties.ReadOnly = Status
        chkKULIT_02.Properties.ReadOnly = Status
        txtKULIT_02_TEXT.Properties.ReadOnly = Status
        chkKULIT_03.Properties.ReadOnly = Status
        txtKULIT_03_TEXT.Properties.ReadOnly = Status
        chkKULIT_04.Properties.ReadOnly = Status
        chkKULIT_05.Properties.ReadOnly = Status
        chkKULIT_06.Properties.ReadOnly = Status
        txtKULIT_06_TEXT.Properties.ReadOnly = Status

        chkURINARIA_01.Properties.ReadOnly = Status
        chkURINARIA_02.Properties.ReadOnly = Status
        txtURINARIA_02_TEXT.Properties.ReadOnly = Status
        chkURINARIA_03.Properties.ReadOnly = Status
        txtURINARIA_03_TEXT.Properties.ReadOnly = Status
        chkURINARIA_04.Properties.ReadOnly = Status
        txtURINARIA_04_TEXT.Properties.ReadOnly = Status

        chkKEADAAN_EMOSIONAL_01.Properties.ReadOnly = Status
        chkKEADAAN_EMOSIONAL_02.Properties.ReadOnly = Status
        chkKEADAAN_EMOSIONAL_03.Properties.ReadOnly = Status
        chkKEADAAN_EMOSIONAL_04.Properties.ReadOnly = Status
        chkKEADAAN_EMOSIONAL_05.Properties.ReadOnly = Status
        txtKEADAAN_EMOSIONAL_05_TEXT.Properties.ReadOnly = Status

        chkMATA_01.Properties.ReadOnly = Status
        chkMATA_02.Properties.ReadOnly = Status
        chkMATA_03.Properties.ReadOnly = Status
        chkMATA_04.Properties.ReadOnly = Status
        txtMATA_04_TEXT.Properties.ReadOnly = Status

        chkOTOT_SENDI_TULANG_01.Properties.ReadOnly = Status
        chkOTOT_SENDI_TULANG_02.Properties.ReadOnly = Status
        chkOTOT_SENDI_TULANG_03.Properties.ReadOnly = Status
        chkOTOT_SENDI_TULANG_04.Properties.ReadOnly = Status
        txtOTOT_SENDI_TULANG_04_TEXT.Properties.ReadOnly = Status

        chkMULUT_01.Properties.ReadOnly = Status
        chkMULUT_02.Properties.ReadOnly = Status
        txtMULUT_02_TEXT.Properties.ReadOnly = Status

        chkHIDUNG_MUKA_01.Properties.ReadOnly = Status
        chkHIDUNG_MUKA_02.Properties.ReadOnly = Status
        txtHIDUNG_MUKA_02_TEXT.Properties.ReadOnly = Status

        chkTELINGA_01.Properties.ReadOnly = Status
        chkTELINGA_02.Properties.ReadOnly = Status
        txtTELINGA_02_TEXT.Properties.ReadOnly = Status

        chkTENGGOROKAN_01.Properties.ReadOnly = Status
        chkTENGGOROKAN_02.Properties.ReadOnly = Status
        txtTENGGOROKAN_02_TEXT.Properties.ReadOnly = Status

        chkSKRINING_GIZI_01_1.Properties.ReadOnly = Status
        chkSKRINING_GIZI_01_2.Properties.ReadOnly = Status
        chkSKRINING_GIZI_02_1.Properties.ReadOnly = Status
        chkSKRINING_GIZI_02_2.Properties.ReadOnly = Status
        chkSKRINING_GIZI_03_1.Properties.ReadOnly = Status
        chkSKRINING_GIZI_03_2.Properties.ReadOnly = Status

        chkSKRINING_RESIKO_JATUH_01_1.Properties.ReadOnly = Status
        chkSKRINING_RESIKO_JATUH_01_2.Properties.ReadOnly = Status
        chkSKRINING_RESIKO_JATUH_02_1.Properties.ReadOnly = Status
        chkSKRINING_RESIKO_JATUH_02_2.Properties.ReadOnly = Status

        chkRESIKO_JATUH_01.Properties.ReadOnly = Status
        chkRESIKO_JATUH_02.Properties.ReadOnly = Status
        chkRESIKO_JATUH_03.Properties.ReadOnly = Status

        chkTINDAKAN_01_YA.Properties.ReadOnly = Status
        chkTINDAKAN_02_YA.Properties.ReadOnly = Status
        chkTINDAKAN_03_YA.Properties.ReadOnly = Status
        chkTINDAKAN_04_YA.Properties.ReadOnly = Status
        chkTINDAKAN_01_TIDAK.Properties.ReadOnly = Status
        chkTINDAKAN_02_TIDAK.Properties.ReadOnly = Status
        chkTINDAKAN_03_TIDAK.Properties.ReadOnly = Status
        chkTINDAKAN_04_TIDAK.Properties.ReadOnly = Status

        grdPerawat1.Properties.ReadOnly = Status
        grdPerawat2.Properties.ReadOnly = Status
        grdPerawat3.Properties.ReadOnly = Status
        grdPerawat4.Properties.ReadOnly = Status

        txtPASIENKELUARGA1.ReadOnly = Status
        txtPASIENKELUARGA2.ReadOnly = Status
        txtPASIENKELUARGA3.ReadOnly = Status
        txtPASIENKELUARGA4.ReadOnly = Status

        chkSKALANYERI_00.Properties.ReadOnly = Status
        chkSKALANYERI_01.Properties.ReadOnly = Status
        chkSKALANYERI_02.Properties.ReadOnly = Status
        chkSKALANYERI_03.Properties.ReadOnly = Status
        chkSKALANYERI_04.Properties.ReadOnly = Status
        chkSKALANYERI_05.Properties.ReadOnly = Status
        chkSKALANYERI_06.Properties.ReadOnly = Status
        chkSKALANYERI_07.Properties.ReadOnly = Status
        chkSKALANYERI_08.Properties.ReadOnly = Status
        chkSKALANYERI_09.Properties.ReadOnly = Status
        chkSKALANYERI_10.Properties.ReadOnly = Status

        chkSKALANEYRI_KRONIS_01.Properties.ReadOnly = Status
        txtSKALANEYRI_LOKASI_TEXT.Properties.ReadOnly = Status
        txtSKALANEYRI_ISTIRAHAT_TEXT.Properties.ReadOnly = Status
        txtSKALANEYRI_AKTIVITAS_TEXT.Properties.ReadOnly = Status
        chkSKALANEYRI_AKUT_01.Properties.ReadOnly = Status
        txtSKALANEYRI_AKUT_LOKASI_TEXT.Properties.ReadOnly = Status
        txtSKALANEYRI_AKUT_ISTIRAHAT_TEXT.Properties.ReadOnly = Status
        txtSKALANEYRI_AKUT_AKTIVITAS_TEXT.Properties.ReadOnly = Status

        chkKEBUTUHAN_PENDIDIKAN_DAN_PENGAJARAN_01.Properties.ReadOnly = Status
        chkKEBUTUHAN_PENDIDIKAN_DAN_PENGAJARAN_02.Properties.ReadOnly = Status
        chkKEBUTUHAN_PENDIDIKAN_DAN_PENGAJARAN_03.Properties.ReadOnly = Status
        chkKEBUTUHAN_PENDIDIKAN_DAN_PENGAJARAN_04.Properties.ReadOnly = Status
        txtKEBUTUHAN_PENDIDIKAN_DAN_PENGAJARAN_04_TEXT.Properties.ReadOnly = Status

        chkBUTUHPENDIDIKAN_TIDAK.Properties.ReadOnly = Status
        chkBUTUHPENDIDIKAN_YA.Properties.ReadOnly = Status

        chkKEBUTUHAN_EDUKASI_01.Properties.ReadOnly = Status
        chkKEBUTUHAN_EDUKASI_02.Properties.ReadOnly = Status
        chkKEBUTUHAN_EDUKASI_03.Properties.ReadOnly = Status
        chkKEBUTUHAN_EDUKASI_04.Properties.ReadOnly = Status
        txtKEBUTUHAN_EDUKASI_04_TEXT.Properties.ReadOnly = Status

        chkALERGI_TIDAK2.Properties.ReadOnly = Status
        chkALERGI_YA2.Properties.ReadOnly = Status
        txtALERGI_TEXT2.Properties.ReadOnly = Status

        txtRIWAYAT_PENYAKIT.Properties.ReadOnly = Status
        txtRIWAYAT_IMUNISASI.Properties.ReadOnly = Status

        txtPEMERIKSAAN_PENUNJANG_01.Properties.ReadOnly = Status
        chkPEMERIKSAAN_PENUNJANG_02.Properties.ReadOnly = Status
        chkPEMERIKSAAN_PENUNJANG_03.Properties.ReadOnly = Status
        chkPEMERIKSAAN_PENUNJANG_04.Properties.ReadOnly = Status
        txtPEMERIKSAAN_PENUNJANG_04_TEXT.Properties.ReadOnly = Status

        grdKDITEMDIAGNOSAPERAWAT.Properties.ReadOnly = Status

        chkHASIL_PENANGANAN_01.Properties.ReadOnly = Status
        chkHASIL_PENANGANAN_02.Properties.ReadOnly = Status
        chkHASIL_PENANGANAN_03.Properties.ReadOnly = Status
        chkHASIL_PENANGANAN_04.Properties.ReadOnly = Status
        chkHASIL_PENANGANAN_05.Properties.ReadOnly = Status
        txtHASIL_PENANGANAN_05_TEXT.Properties.ReadOnly = Status
        txtPERENCANAAN_PASIEN_PULANG_01.Properties.ReadOnly = Status
        chkPERENCANAAN_PASIEN_PULANG_02_1.Properties.ReadOnly = Status
        chkPERENCANAAN_PASIEN_PULANG_02_2.Properties.ReadOnly = Status
        chkPERENCANAAN_PASIEN_PULANG_02_3.Properties.ReadOnly = Status
        chkPERENCANAAN_PASIEN_PULANG_02_4.Properties.ReadOnly = Status
        txtPERENCANAAN_PASIEN_PULANG_02_4_TEXT.Properties.ReadOnly = Status
        chkPERENCANAAN_PASIEN_PULANG_03_1.Properties.ReadOnly = Status
        chkPERENCANAAN_PASIEN_PULANG_03_2.Properties.ReadOnly = Status
        chkPERENCANAAN_PASIEN_PULANG_03_3.Properties.ReadOnly = Status
        chkPERENCANAAN_PASIEN_PULANG_03_4.Properties.ReadOnly = Status
        chkPERENCANAAN_PASIEN_PULANG_03_5.Properties.ReadOnly = Status
        chkPERENCANAAN_PASIEN_PULANG_03_6.Properties.ReadOnly = Status
        chkPERENCANAAN_PASIEN_PULANG_03_6_1.Properties.ReadOnly = Status
        chkPERENCANAAN_PASIEN_PULANG_03_6_2.Properties.ReadOnly = Status
        chkPERENCANAAN_PASIEN_PULANG_03_7.Properties.ReadOnly = Status
        chkPERENCANAAN_PASIEN_PULANG_03_8.Properties.ReadOnly = Status
        txtPERENCANAAN_PASIEN_PULANG_03_8_TEXT.Properties.ReadOnly = Status

        txtNamaPasienKeluarga.Properties.ReadOnly = Status

        chkFLACC_1.Properties.ReadOnly = Status
        chkFLACC_2.Properties.ReadOnly = Status
        chkFLACC_3.Properties.ReadOnly = Status
        chkFLACC_4.Properties.ReadOnly = Status
        chkFLACC_5.Properties.ReadOnly = Status
        chkFLACC_6.Properties.ReadOnly = Status

        txtSKORFLACC_1.Properties.ReadOnly = Status
        txtSKORFLACC_2.Properties.ReadOnly = Status
        txtSKORFLACC_3.Properties.ReadOnly = Status
        txtSKORFLACC_4.Properties.ReadOnly = Status
        txtSKORFLACC_5.Properties.ReadOnly = Status

        chkUMUR_01.Properties.ReadOnly = Status
        chkUMUR_02.Properties.ReadOnly = Status
        chkUMUR_03.Properties.ReadOnly = Status
        chkUMUR_04.Properties.ReadOnly = Status
        chkUMUR_05.Properties.ReadOnly = Status
        chkUMUR_06.Properties.ReadOnly = Status
        chkUMUR_07.Properties.ReadOnly = Status
        chkUMUR_08.Properties.ReadOnly = Status
        chkUMUR_09.Properties.ReadOnly = Status
        chkUMUR_10.Properties.ReadOnly = Status

        chkSOSIAL_01.Properties.ReadOnly = Status
        chkSOSIAL_02.Properties.ReadOnly = Status
        chkSOSIAL_03.Properties.ReadOnly = Status
        chkSOSIAL_04.Properties.ReadOnly = Status
        chkSOSIAL_05.Properties.ReadOnly = Status
        chkSOSIAL_06.Properties.ReadOnly = Status
        chkSOSIAL_07.Properties.ReadOnly = Status
        chkSOSIAL_08.Properties.ReadOnly = Status
        chkSOSIAL_09.Properties.ReadOnly = Status
        chkSOSIAL_10.Properties.ReadOnly = Status

        chkMOTORIK_HALUS_01.Properties.ReadOnly = Status
        chkMOTORIK_HALUS_02.Properties.ReadOnly = Status
        chkMOTORIK_HALUS_03.Properties.ReadOnly = Status
        chkMOTORIK_HALUS_04.Properties.ReadOnly = Status
        chkMOTORIK_HALUS_05.Properties.ReadOnly = Status
        chkMOTORIK_HALUS_06.Properties.ReadOnly = Status
        chkMOTORIK_HALUS_07.Properties.ReadOnly = Status
        chkMOTORIK_HALUS_08.Properties.ReadOnly = Status
        chkMOTORIK_HALUS_09.Properties.ReadOnly = Status

        chkMOTORIK_KASAR_01.Properties.ReadOnly = Status
        chkMOTORIK_KASAR_02.Properties.ReadOnly = Status
        chkMOTORIK_KASAR_03.Properties.ReadOnly = Status
        chkMOTORIK_KASAR_04.Properties.ReadOnly = Status
        chkMOTORIK_KASAR_05.Properties.ReadOnly = Status
        chkMOTORIK_KASAR_06.Properties.ReadOnly = Status
        chkMOTORIK_KASAR_07.Properties.ReadOnly = Status
        chkMOTORIK_KASAR_08.Properties.ReadOnly = Status
        chkMOTORIK_KASAR_09.Properties.ReadOnly = Status

        chkBAHASA_01.Properties.ReadOnly = Status
        chkBAHASA_02.Properties.ReadOnly = Status
        chkBAHASA_03.Properties.ReadOnly = Status
        chkBAHASA_04.Properties.ReadOnly = Status
        chkBAHASA_05.Properties.ReadOnly = Status
        chkBAHASA_06.Properties.ReadOnly = Status
        chkBAHASA_07.Properties.ReadOnly = Status
        chkBAHASA_08.Properties.ReadOnly = Status
        chkBAHASA_09.Properties.ReadOnly = Status

        txtKDUSERPERAWAT.Properties.ReadOnly = Status
    End Sub
    Private Sub fn_EmptyMe()
        'txtCODE.Text = "<--- AUTO --->"

        txtKDUSERPERAWAT.Text =sUSERPERAWAT_x 
        deDATE.DateTime = Now
        chkALERGI_TIDAK.Checked = False
        chkALERGI_YA.Checked = False
        txtALERGI_YA_TEXT.ResetText()
        chkAUTO.Checked = False
        chkALLO.Checked = False
        txtALLO_TEXT.ResetText()
        txtKELUHAN_UTAMA.ResetText()
        txtKESADARAN_UMUM.ResetText()
        txtKEADAAN.ResetText()
        txtTANDA_VITAL_TD.ResetText()
        txtTANDA_VITAL_NADI.ResetText()
        txtTANDA_VITAL_SUHU.ResetText()
        txtTANDA_VITAL_R.ResetText()
        chkTANDA_VITAL_REGULER.Checked = False
        chkTANDA_VITAL_IREGULER.Checked = False
        txtTANDA_VITAL_SPO2.ResetText()
        txtTANDA_VITAL_BB.ResetText()
        txtTANDA_VITAL_TB.ResetText()
        txtTANDA_VITAL_LD.ResetText()
        txtTANDA_VITAL_LK.ResetText()
        chkSTATUS_PSIKOLOGIS_01.Checked = False
        chkSTATUS_PSIKOLOGIS_02.Checked = False
        chkSTATUS_PSIKOLOGIS_03.Checked = False
        chkSTATUS_PSIKOLOGIS_04.Checked = False
        chkSTATUS_PSIKOLOGIS_05.Checked = False
        chkSTATUS_PSIKOLOGIS_06.Checked = False
        txtSTATUS_PSIKOLOGIS_06_TEXT.ResetText()

        chkSTATUS_MENTAL_01.Checked = False
        chkSTATUS_MENTAL_02.Checked = False
        txtSTATUS_MENTAL_02_TEXT.ResetText()
        chkSTATUS_MENTAL_03.Checked = False
        txtSTATUS_MENTAL_03_TEXT.ResetText()

        chkSOSIAL_01_1.Checked = False
        chkSOSIAL_01_2.Checked = False
        chkSOSIAL_02_1.Checked = False
        chkSOSIAL_02_2.Checked = False
        chkSOSIAL_02_3.Checked = False
        chkSOSIAL_02_4.Checked = False
        txtSOSIAL_02_4_TEXT.ResetText()

        txtSOSIAL_03_1_TEXT.ResetText()
        txtSOSIAL_03_2_TEXT.ResetText()
        txtSOSIAL_03_3_TEXT.ResetText()

        txtSTATUS_SPIRITUAL_01_TEXT.ResetText()
        txtSTATUS_SPIRITUAL_02_TEXT.ResetText()

        chkPERSYARAFAN_01.Checked = False
        chkPERSYARAFAN_02.Checked = False
        chkPERSYARAFAN_03.Checked = False
        chkPERSYARAFAN_04.Checked = False
        chkPERSYARAFAN_05.Checked = False
        txtPERSYARAFAN_05_TEXT.ResetText()

        chkPERNAPASAN_01.Checked = False
        chkPERNAPASAN_02.Checked = False
        chkPERNAPASAN_03.Checked = False
        chkPERNAPASAN_04.Checked = False
        txtPERNAPASAN_04_TEXT.ResetText()

        chkPENCERNAAN_01.Checked = False
        chkPENCERNAAN_02.Checked = False
        chkPENCERNAAN_03.Checked = False
        chkPENCERNAAN_04.Checked = False
        chkPENCERNAAN_05.Checked = False
        txtPENCERNAAN_05_TEXT.ResetText()

        chkENDROKIN_01.Checked = False
        chkENDROKIN_02.Checked = False
        chkENDROKIN_03.Checked = False
        chkENDROKIN_04.Checked = False
        chkENDROKIN_05.Checked = False
        txtENDROKIN_05_TEXT.ResetText()

        chkCARDIOVASKULER_01.Checked = False
        chkCARDIOVASKULER_02.Checked = False
        chkCARDIOVASKULER_03.Checked = False
        chkCARDIOVASKULER_04.Checked = False
        txtCARDIOVASKULER_04_TEXT.ResetText()

        chkABDOMEN_01.Checked = False
        chkABDOMEN_02.Checked = False
        chkABDOMEN_03.Checked = False
        chkABDOMEN_04.Checked = False
        chkABDOMEN_05.Checked = False
        txtABDOMEN_05_TEXT.ResetText()
        chkABDOMEN_06.Checked = False
        txtABDOMEN_06_TEXT.ResetText()
        chkABDOMEN_07.Checked = False
        txtABDOMEN_07_TEXT.ResetText()
        chkABDOMEN_08.Checked = False
        txtABDOMEN_08_TEXT.ResetText()
        chkABDOMEN_09.Checked = False
        txtABDOMEN_09_TEXT.ResetText()

        chkREPRODUKSI_01.Checked = False
        chkREPRODUKSI_01_1.Checked = False
        chkREPRODUKSI_01_2.Checked = False
        chkREPRODUKSI_02.Checked = False
        chkREPRODUKSI_02_1.Checked = False
        txtREPRODUKSI_02_1_TEXT.ResetText()
        chkREPRODUKSI_02_2.Checked = False
        chkREPRODUKSI_03.Checked = False
        chkREPRODUKSI_03_1.Checked = False
        txtREPRODUKSI_03_1_TEXT.ResetText()
        chkREPRODUKSI_03_2.Checked = False
        chkREPRODUKSI_04.Checked = False
        txtREPRODUKSI_04_TEXT.ResetText()
        chkREPRODUKSI_05.Checked = False
        txtREPRODUKSI_05_TEXT.ResetText()
        chkREPRODUKSI_06.Checked = False
        txtREPRODUKSI_06_TEXT.ResetText()
        chkREPRODUKSI_07.Checked = False
        txtREPRODUKSI_07_TEXT.ResetText()
        chkREPRODUKSI_08.Checked = False
        txtREPRODUKSI_08_TEXT.ResetText()

        chkKULIT_01.Checked = False
        chkKULIT_02.Checked = False
        txtKULIT_02_TEXT.ResetText()
        chkKULIT_03.Checked = False
        txtKULIT_03_TEXT.ResetText()
        chkKULIT_04.Checked = False
        chkKULIT_05.Checked = False
        chkKULIT_06.Checked = False
        txtKULIT_06_TEXT.ResetText()

        chkURINARIA_01.Checked = False
        chkURINARIA_02.Checked = False
        txtURINARIA_02_TEXT.ResetText()
        chkURINARIA_03.Checked = False
        txtURINARIA_03_TEXT.ResetText()
        chkURINARIA_04.Checked = False
        txtURINARIA_04_TEXT.ResetText()

        chkKEADAAN_EMOSIONAL_01.Checked = False
        chkKEADAAN_EMOSIONAL_02.Checked = False
        chkKEADAAN_EMOSIONAL_03.Checked = False
        chkKEADAAN_EMOSIONAL_04.Checked = False
        chkKEADAAN_EMOSIONAL_05.Checked = False
        txtKEADAAN_EMOSIONAL_05_TEXT.ResetText()

        chkMATA_01.Checked = False
        chkMATA_02.Checked = False
        chkMATA_03.Checked = False
        chkMATA_04.Checked = False
        txtMATA_04_TEXT.ResetText()

        chkOTOT_SENDI_TULANG_01.Checked = False
        chkOTOT_SENDI_TULANG_02.Checked = False
        chkOTOT_SENDI_TULANG_03.Checked = False
        chkOTOT_SENDI_TULANG_04.Checked = False
        txtOTOT_SENDI_TULANG_04_TEXT.ResetText()

        chkMULUT_01.Checked = False
        chkMULUT_02.Checked = False
        txtMULUT_02_TEXT.ResetText()

        chkHIDUNG_MUKA_01.Checked = False
        chkHIDUNG_MUKA_02.Checked = False
        txtHIDUNG_MUKA_02_TEXT.ResetText()

        chkTELINGA_01.Checked = False
        chkTELINGA_02.Checked = False
        txtTELINGA_02_TEXT.ResetText()

        chkTENGGOROKAN_01.Checked = False
        chkTENGGOROKAN_02.Checked = False
        txtTENGGOROKAN_02_TEXT.ResetText()

        chkSKRINING_GIZI_01_1.Checked = False
        chkSKRINING_GIZI_01_2.Checked = False
        chkSKRINING_GIZI_02_1.Checked = False
        chkSKRINING_GIZI_02_2.Checked = False
        chkSKRINING_GIZI_03_1.Checked = False
        chkSKRINING_GIZI_03_2.Checked = False

        chkSKRINING_RESIKO_JATUH_01_1.Checked = False
        chkSKRINING_RESIKO_JATUH_01_2.Checked = False
        chkSKRINING_RESIKO_JATUH_02_1.Checked = False
        chkSKRINING_RESIKO_JATUH_02_2.Checked = False

        chkRESIKO_JATUH_01.Checked = False
        chkRESIKO_JATUH_02.Checked = False
        chkRESIKO_JATUH_03.Checked = False

        chkTINDAKAN_01_YA.Checked = False
        chkTINDAKAN_02_YA.Checked = False
        chkTINDAKAN_03_YA.Checked = False
        chkTINDAKAN_04_YA.Checked = False
        chkTINDAKAN_01_TIDAK.Checked = False
        chkTINDAKAN_02_TIDAK.Checked = False
        chkTINDAKAN_03_TIDAK.Checked = False
        chkTINDAKAN_04_TIDAK.Checked = False

        grdPerawat1.ResetText()
        grdPerawat2.ResetText()
        grdPerawat3.ResetText()
        grdPerawat4.ResetText()

        txtPASIENKELUARGA1.ResetText()
        txtPASIENKELUARGA2.ResetText()
        txtPASIENKELUARGA3.ResetText()
        txtPASIENKELUARGA4.ResetText()

        chkSKALANYERI_00.Checked = False
        chkSKALANYERI_01.Checked = False
        chkSKALANYERI_02.Checked = False
        chkSKALANYERI_03.Checked = False
        chkSKALANYERI_04.Checked = False
        chkSKALANYERI_05.Checked = False
        chkSKALANYERI_06.Checked = False
        chkSKALANYERI_07.Checked = False
        chkSKALANYERI_08.Checked = False
        chkSKALANYERI_09.Checked = False
        chkSKALANYERI_10.Checked = False

        chkSKALANEYRI_KRONIS_01.Checked = False
        txtSKALANEYRI_LOKASI_TEXT.ResetText()
        txtSKALANEYRI_ISTIRAHAT_TEXT.ResetText()
        txtSKALANEYRI_AKTIVITAS_TEXT.ResetText()
        chkSKALANEYRI_AKUT_01.Checked = False
        txtSKALANEYRI_AKUT_LOKASI_TEXT.ResetText()
        txtSKALANEYRI_AKUT_ISTIRAHAT_TEXT.ResetText()
        txtSKALANEYRI_AKUT_AKTIVITAS_TEXT.ResetText()

        chkKEBUTUHAN_PENDIDIKAN_DAN_PENGAJARAN_01.Checked = False
        chkKEBUTUHAN_PENDIDIKAN_DAN_PENGAJARAN_02.Checked = False
        chkKEBUTUHAN_PENDIDIKAN_DAN_PENGAJARAN_03.Checked = False
        chkKEBUTUHAN_PENDIDIKAN_DAN_PENGAJARAN_04.Checked = False
        txtKEBUTUHAN_PENDIDIKAN_DAN_PENGAJARAN_04_TEXT.ResetText()

        chkBUTUHPENDIDIKAN_TIDAK.Checked = False
        chkBUTUHPENDIDIKAN_YA.Checked = False

        chkKEBUTUHAN_EDUKASI_01.Checked = False
        chkKEBUTUHAN_EDUKASI_02.Checked = False
        chkKEBUTUHAN_EDUKASI_03.Checked = False
        chkKEBUTUHAN_EDUKASI_04.Checked = False
        txtKEBUTUHAN_EDUKASI_04_TEXT.ResetText()

        chkALERGI_TIDAK2.Checked = False
        chkALERGI_YA2.Checked = False
        txtALERGI_TEXT2.ResetText()

        txtRIWAYAT_PENYAKIT.ResetText()
        txtRIWAYAT_IMUNISASI.ResetText()

        txtPEMERIKSAAN_PENUNJANG_01.ResetText()
        chkPEMERIKSAAN_PENUNJANG_02.Checked = False
        chkPEMERIKSAAN_PENUNJANG_03.Checked = False
        chkPEMERIKSAAN_PENUNJANG_04.Checked = False
        txtPEMERIKSAAN_PENUNJANG_04_TEXT.ResetText()

        grdKDITEMDIAGNOSAPERAWAT.ResetText()

        chkHASIL_PENANGANAN_01.Checked = False
        chkHASIL_PENANGANAN_02.Checked = False
        chkHASIL_PENANGANAN_03.Checked = False
        chkHASIL_PENANGANAN_04.Checked = False
        chkHASIL_PENANGANAN_05.Checked = False
        txtHASIL_PENANGANAN_05_TEXT.ResetText()
        txtPERENCANAAN_PASIEN_PULANG_01.ResetText()
        chkPERENCANAAN_PASIEN_PULANG_02_1.Checked = False
        chkPERENCANAAN_PASIEN_PULANG_02_2.Checked = False
        chkPERENCANAAN_PASIEN_PULANG_02_3.Checked = False
        chkPERENCANAAN_PASIEN_PULANG_02_4.Checked = False
        txtPERENCANAAN_PASIEN_PULANG_02_4_TEXT.ResetText()
        chkPERENCANAAN_PASIEN_PULANG_03_1.Checked = False
        chkPERENCANAAN_PASIEN_PULANG_03_2.Checked = False
        chkPERENCANAAN_PASIEN_PULANG_03_3.Checked = False
        chkPERENCANAAN_PASIEN_PULANG_03_4.Checked = False
        chkPERENCANAAN_PASIEN_PULANG_03_5.Checked = False
        chkPERENCANAAN_PASIEN_PULANG_03_6.Checked = False
        chkPERENCANAAN_PASIEN_PULANG_03_6_1.Checked = False
        chkPERENCANAAN_PASIEN_PULANG_03_6_2.Checked = False
        chkPERENCANAAN_PASIEN_PULANG_03_7.Checked = False
        chkPERENCANAAN_PASIEN_PULANG_03_8.Checked = False
        txtPERENCANAAN_PASIEN_PULANG_03_8_TEXT.ResetText()

        txtNamaPasienKeluarga.ResetText()

        chkFLACC_1.Checked = False
        chkFLACC_2.Checked = False
        chkFLACC_3.Checked = False
        chkFLACC_4.Checked = False
        chkFLACC_5.Checked = False
        chkFLACC_6.Checked = False

        txtSKORFLACC_1.ResetText()
        txtSKORFLACC_2.ResetText()
        txtSKORFLACC_3.ResetText()
        txtSKORFLACC_4.ResetText()
        txtSKORFLACC_5.ResetText()

        chkSOSIAL_01.Checked = False
        chkSOSIAL_02.Checked = False
        chkSOSIAL_03.Checked = False
        chkSOSIAL_04.Checked = False
        chkSOSIAL_05.Checked = False
        chkSOSIAL_06.Checked = False
        chkSOSIAL_07.Checked = False
        chkSOSIAL_08.Checked = False
        chkSOSIAL_09.Checked = False
        chkSOSIAL_10.Checked = False

        chkMOTORIK_HALUS_01.Checked = False
        chkMOTORIK_HALUS_02.Checked = False
        chkMOTORIK_HALUS_03.Checked = False
        chkMOTORIK_HALUS_04.Checked = False
        chkMOTORIK_HALUS_05.Checked = False
        chkMOTORIK_HALUS_06.Checked = False
        chkMOTORIK_HALUS_07.Checked = False
        chkMOTORIK_HALUS_08.Checked = False
        chkMOTORIK_HALUS_09.Checked = False

        chkMOTORIK_KASAR_01.Checked = False
        chkMOTORIK_KASAR_02.Checked = False
        chkMOTORIK_KASAR_03.Checked = False
        chkMOTORIK_KASAR_04.Checked = False
        chkMOTORIK_KASAR_05.Checked = False
        chkMOTORIK_KASAR_06.Checked = False
        chkMOTORIK_KASAR_07.Checked = False
        chkMOTORIK_KASAR_08.Checked = False
        chkMOTORIK_KASAR_09.Checked = False

        chkBAHASA_01.Checked = False
        chkBAHASA_02.Checked = False
        chkBAHASA_03.Checked = False
        chkBAHASA_04.Checked = False
        chkBAHASA_05.Checked = False
        chkBAHASA_06.Checked = False
        chkBAHASA_07.Checked = False
        chkBAHASA_08.Checked = False
        chkBAHASA_09.Checked = False

        chkUMUR_01.Checked = False
        chkUMUR_02.Checked = False
        chkUMUR_03.Checked = False
        chkUMUR_04.Checked = False
        chkUMUR_05.Checked = False
        chkUMUR_06.Checked = False
        chkUMUR_07.Checked = False
        chkUMUR_08.Checked = False
        chkUMUR_09.Checked = False
        chkUMUR_10.Checked = False


    End Sub
    Private Sub fn_LoadData()
        Try
            ' ***** HEADER *****
            Dim ds = oDigital.GetData(sNoid)

            With ds

                chkALERGI_TIDAK.Checked = .ALERGI_TIDAK
                chkALERGI_YA.Checked = .ALERGI_YA
                txtALERGI_YA_TEXT.Text = .ALERGI_YA_TEXT
                chkAUTO.Checked = .AUTO
                chkALLO.Checked = .ALLO
                txtALLO_TEXT.Text = .ALLO_TEXT
                txtKELUHAN_UTAMA.Text = .KELUHAN_UTAMA
                txtKESADARAN_UMUM.Text = .KESADARAN_UMUM
                txtKEADAAN.Text = .KEADAAN
                txtTANDA_VITAL_TD.Text = .TANDA_VITAL_TD
                txtTANDA_VITAL_NADI.Text = .TANDA_VITAL_NADI
                txtTANDA_VITAL_SUHU.Text = .TANDA_VITAL_SUHU
                txtTANDA_VITAL_R.Text = .TANDA_VITAL_R
                chkTANDA_VITAL_REGULER.Checked = .TANDA_VITAL_REGULER
                chkTANDA_VITAL_IREGULER.Checked = .TANDA_VITAL_IREGULER
                txtTANDA_VITAL_SPO2.Text = .TANDA_VITAL_SPO2
                txtTANDA_VITAL_BB.Text = .TANDA_VITAL_BB
                txtTANDA_VITAL_TB.Text = .TANDA_VITAL_TB
                txtTANDA_VITAL_LD.Text = .TANDA_VITAL_LD
                txtTANDA_VITAL_LK.Text = .TANDA_VITAL_LK
                chkSTATUS_PSIKOLOGIS_01.Checked = .STATUS_PSIKOLOGIS_01
                chkSTATUS_PSIKOLOGIS_02.Checked = .STATUS_PSIKOLOGIS_02
                chkSTATUS_PSIKOLOGIS_03.Checked = .STATUS_PSIKOLOGIS_03
                chkSTATUS_PSIKOLOGIS_04.Checked = .STATUS_PSIKOLOGIS_04
                chkSTATUS_PSIKOLOGIS_05.Checked = .STATUS_PSIKOLOGIS_05
                chkSTATUS_PSIKOLOGIS_06.Checked = .STATUS_PSIKOLOGIS_06
                txtSTATUS_PSIKOLOGIS_06_TEXT.Text = .STATUS_PSIKOLOGIS_06_TEXT

                chkSTATUS_MENTAL_01.Checked = .STATUS_MENTAL_01
                chkSTATUS_MENTAL_02.Checked = .STATUS_MENTAL_02
                txtSTATUS_MENTAL_02_TEXT.Text = .STATUS_MENTAL_02_TEXT
                chkSTATUS_MENTAL_03.Checked = .STATUS_MENTAL_03
                txtSTATUS_MENTAL_03_TEXT.Text = .STATUS_MENTAL_03_TEXT

                chkSOSIAL_01_1.Checked = .SOSIAL_01_1
                chkSOSIAL_01_2.Checked = .SOSIAL_01_2
                chkSOSIAL_02_1.Checked = .SOSIAL_02_1
                chkSOSIAL_02_2.Checked = .SOSIAL_02_2
                chkSOSIAL_02_3.Checked = .SOSIAL_02_3
                chkSOSIAL_02_4.Checked = .SOSIAL_02_4
                txtSOSIAL_02_4_TEXT.Text = .SOSIAL_02_4_TEXT

                txtSOSIAL_03_1_TEXT.Text = .SOSIAL_03_1_TEXT
                txtSOSIAL_03_2_TEXT.Text = .SOSIAL_03_2_TEXT
                txtSOSIAL_03_3_TEXT.Text = .SOSIAL_03_3_TEXT

                txtSTATUS_SPIRITUAL_01_TEXT.Text = .STATUS_SPIRITUAL_01_TEXT
                txtSTATUS_SPIRITUAL_02_TEXT.Text = .STATUS_SPIRITUAL_02_TEXT

                chkPERSYARAFAN_01.Checked = .PERSYARAFAN_01
                chkPERSYARAFAN_02.Checked = .PERSYARAFAN_02
                chkPERSYARAFAN_03.Checked = .PERSYARAFAN_03
                chkPERSYARAFAN_04.Checked = .PERSYARAFAN_04
                chkPERSYARAFAN_05.Checked = .PERSYARAFAN_05
                txtPERSYARAFAN_05_TEXT.Text = .PERSYARAFAN_05_TEXT

                chkPERNAPASAN_01.Checked = .PERNAPASAN_01
                chkPERNAPASAN_02.Checked = .PERNAPASAN_02
                chkPERNAPASAN_03.Checked = .PERNAPASAN_03
                chkPERNAPASAN_04.Checked = .PERNAPASAN_04
                txtPERNAPASAN_04_TEXT.Text = .PERNAPASAN_04_TEXT

                chkPENCERNAAN_01.Checked = .PENCERNAAN_01
                chkPENCERNAAN_02.Checked = .PENCERNAAN_02
                chkPENCERNAAN_03.Checked = .PENCERNAAN_03
                chkPENCERNAAN_04.Checked = .PENCERNAAN_04
                chkPENCERNAAN_05.Checked = .PENCERNAAN_05
                txtPENCERNAAN_05_TEXT.Text = .PENCERNAAN_05_TEXT

                chkENDROKIN_01.Checked = .ENDROKIN_01
                chkENDROKIN_02.Checked = .ENDROKIN_02
                chkENDROKIN_03.Checked = .ENDROKIN_03
                chkENDROKIN_04.Checked = .ENDROKIN_04
                chkENDROKIN_05.Checked = .ENDROKIN_05
                txtENDROKIN_05_TEXT.Text = .ENDROKIN_05_TEXT

                chkCARDIOVASKULER_01.Checked = .CARDIOVASKULER_01
                chkCARDIOVASKULER_02.Checked = .CARDIOVASKULER_02
                chkCARDIOVASKULER_03.Checked = .CARDIOVASKULER_03
                chkCARDIOVASKULER_04.Checked = .CARDIOVASKULER_04
                txtCARDIOVASKULER_04_TEXT.Text = .CARDIOVASKULER_04_TEXT

                chkABDOMEN_01.Checked = .ABDOMEN_01
                chkABDOMEN_02.Checked = .ABDOMEN_02
                chkABDOMEN_03.Checked = .ABDOMEN_03
                chkABDOMEN_04.Checked = .ABDOMEN_04
                chkABDOMEN_05.Checked = .ABDOMEN_05
                txtABDOMEN_05_TEXT.Text = .ABDOMEN_05_TEXT
                chkABDOMEN_06.Checked = .ABDOMEN_06
                txtABDOMEN_06_TEXT.Text = .ABDOMEN_06_TEXT
                chkABDOMEN_07.Checked = .ABDOMEN_07
                txtABDOMEN_07_TEXT.Text = .ABDOMEN_07_TEXT
                chkABDOMEN_08.Checked = .ABDOMEN_08
                txtABDOMEN_08_TEXT.Text = .ABDOMEN_08_TEXT
                chkABDOMEN_09.Checked = .ABDOMEN_09
                txtABDOMEN_09_TEXT.Text = .ABDOMEN_09_TEXT

                chkREPRODUKSI_01.Checked = .REPRODUKSI_01
                chkREPRODUKSI_01_1.Checked = .REPRODUKSI_01_1
                chkREPRODUKSI_01_2.Checked = .REPRODUKSI_01_2
                chkREPRODUKSI_02.Checked = .REPRODUKSI_02
                chkREPRODUKSI_02_1.Checked = .REPRODUKSI_02_1
                txtREPRODUKSI_02_1_TEXT.Text = .REPRODUKSI_02_1_TEXT
                chkREPRODUKSI_02_2.Checked = .REPRODUKSI_02_2
                chkREPRODUKSI_03.Checked = .REPRODUKSI_03
                chkREPRODUKSI_03_1.Checked = .REPRODUKSI_03_1
                txtREPRODUKSI_03_1_TEXT.Text = .REPRODUKSI_03_1_TEXT
                chkREPRODUKSI_03_2.Checked = .REPRODUKSI_03_2
                chkREPRODUKSI_04.Checked = .REPRODUKSI_04
                txtREPRODUKSI_04_TEXT.Text = .REPRODUKSI_04_TEXT
                chkREPRODUKSI_05.Checked = .REPRODUKSI_05
                txtREPRODUKSI_05_TEXT.Text = .REPRODUKSI_05_TEXT
                chkREPRODUKSI_06.Checked = .REPRODUKSI_06
                txtREPRODUKSI_06_TEXT.Text = .REPRODUKSI_06_TEXT
                chkREPRODUKSI_07.Checked = .REPRODUKSI_07
                txtREPRODUKSI_07_TEXT.Text = .REPRODUKSI_07_TEXT
                chkREPRODUKSI_08.Checked = .REPRODUKSI_08
                txtREPRODUKSI_08_TEXT.Text = .REPRODUKSI_08_TEXT

                chkKULIT_01.Checked = .KULIT_01
                chkKULIT_02.Checked = .KULIT_02
                txtKULIT_02_TEXT.Text = .KULIT_02_TEXT
                chkKULIT_03.Checked = .KULIT_03
                txtKULIT_03_TEXT.Text = .KULIT_03_TEXT
                chkKULIT_04.Checked = .KULIT_04
                chkKULIT_05.Checked = .KULIT_05
                chkKULIT_06.Checked = .KULIT_06
                txtKULIT_06_TEXT.Text = .KULIT_06_TEXT

                chkURINARIA_01.Checked = .URINARIA_01
                chkURINARIA_02.Checked = .URINARIA_02
                txtURINARIA_02_TEXT.Text = .URINARIA_02_TEXT
                chkURINARIA_03.Checked = .URINARIA_03
                txtURINARIA_03_TEXT.Text = .URINARIA_03_TEXT
                chkURINARIA_04.Checked = .URINARIA_04
                txtURINARIA_04_TEXT.Text = .URINARIA_04_TEXT

                chkKEADAAN_EMOSIONAL_01.Checked = .KEADAAN_EMOSIONAL_01
                chkKEADAAN_EMOSIONAL_02.Checked = .KEADAAN_EMOSIONAL_02
                chkKEADAAN_EMOSIONAL_03.Checked = .KEADAAN_EMOSIONAL_03
                chkKEADAAN_EMOSIONAL_04.Checked = .KEADAAN_EMOSIONAL_04
                chkKEADAAN_EMOSIONAL_05.Checked = .KEADAAN_EMOSIONAL_05
                txtKEADAAN_EMOSIONAL_05_TEXT.Text = .KEADAAN_EMOSIONAL_05_TEXT

                chkMATA_01.Checked = .MATA_01
                chkMATA_02.Checked = .MATA_02
                chkMATA_03.Checked = .MATA_03
                chkMATA_04.Checked = .MATA_04
                txtMATA_04_TEXT.Text = .MATA_04_TEXT

                chkOTOT_SENDI_TULANG_01.Checked = .OTOT_SENDI_TULANG_01
                chkOTOT_SENDI_TULANG_02.Checked = .OTOT_SENDI_TULANG_02
                chkOTOT_SENDI_TULANG_03.Checked = .OTOT_SENDI_TULANG_03
                chkOTOT_SENDI_TULANG_04.Checked = .OTOT_SENDI_TULANG_04
                txtOTOT_SENDI_TULANG_04_TEXT.Text = .OTOT_SENDI_TULANG_04_TEXT

                chkMULUT_01.Checked = .MULUT_01
                chkMULUT_02.Checked = .MULUT_02
                txtMULUT_02_TEXT.Text = .MULUT_02_TEXT

                chkHIDUNG_MUKA_01.Checked = .HIDUNG_MUKA_01
                chkHIDUNG_MUKA_02.Checked = .HIDUNG_MUKA_02
                txtHIDUNG_MUKA_02_TEXT.Text = .HIDUNG_MUKA_02_TEXT

                chkTELINGA_01.Checked = .TELINGA_01
                chkTELINGA_02.Checked = .TELINGA_02
                txtTELINGA_02_TEXT.Text = .TELINGA_02_TEXT

                chkTENGGOROKAN_01.Checked = .TENGGOROKAN_01
                chkTENGGOROKAN_02.Checked = .TENGGOROKAN_02
                txtTENGGOROKAN_02_TEXT.Text = .TENGGOROKAN_02_TEXT

                chkSKRINING_GIZI_01_1.Checked = .SKRINING_GIZI_01_1
                chkSKRINING_GIZI_01_2.Checked = .SKRINING_GIZI_01_2
                chkSKRINING_GIZI_02_1.Checked = .SKRINING_GIZI_02_1
                chkSKRINING_GIZI_02_2.Checked = .SKRINING_GIZI_02_2
                chkSKRINING_GIZI_03_1.Checked = .SKRINING_GIZI_03_1
                chkSKRINING_GIZI_03_2.Checked = .SKRINING_GIZI_03_2

                chkSKRINING_RESIKO_JATUH_01_1.Checked = .SKRINING_RESIKO_JATUH_01_1
                chkSKRINING_RESIKO_JATUH_01_2.Checked = .SKRINING_RESIKO_JATUH_01_2
                chkSKRINING_RESIKO_JATUH_02_1.Checked = .SKRINING_RESIKO_JATUH_02_1
                chkSKRINING_RESIKO_JATUH_02_2.Checked = .SKRINING_RESIKO_JATUH_02_2

                chkRESIKO_JATUH_01.Checked = .RESIKO_JATUH_01
                chkRESIKO_JATUH_02.Checked = .RESIKO_JATUH_02
                chkRESIKO_JATUH_03.Checked = .RESIKO_JATUH_03

                chkTINDAKAN_01_YA.Checked = .TINDAKAN_01_YA
                chkTINDAKAN_02_YA.Checked = .TINDAKAN_02_YA
                chkTINDAKAN_03_YA.Checked = .TINDAKAN_03_YA
                chkTINDAKAN_04_YA.Checked = .TINDAKAN_04_YA
                chkTINDAKAN_01_TIDAK.Checked = .TINDAKAN_01_TIDAK
                chkTINDAKAN_02_TIDAK.Checked = .TINDAKAN_02_TIDAK
                chkTINDAKAN_03_TIDAK.Checked = .TINDAKAN_03_TIDAK
                chkTINDAKAN_04_TIDAK.Checked = .TINDAKAN_04_TIDAK

                grdPerawat1.EditValue = .KDSTAFFPERAWAT1
                grdPerawat2.EditValue = .KDSTAFFPERAWAT2
                grdPerawat3.EditValue = .KDSTAFFPERAWAT3
                grdPerawat4.EditValue = .KDSTAFFPERAWAT4

                txtPASIENKELUARGA1.Text = .PASIENKELUARGA1
                txtPASIENKELUARGA2.Text = .PASIENKELUARGA2
                txtPASIENKELUARGA3.Text = .PASIENKELUARGA3
                txtPASIENKELUARGA4.Text = .PASIENKELUARGA4

                chkSKALANYERI_00.Checked = .SKALANYERI_00
                chkSKALANYERI_01.Checked = .SKALANYERI_01
                chkSKALANYERI_02.Checked = .SKALANYERI_02
                chkSKALANYERI_03.Checked = .SKALANYERI_03
                chkSKALANYERI_04.Checked = .SKALANYERI_04
                chkSKALANYERI_05.Checked = .SKALANYERI_05
                chkSKALANYERI_06.Checked = .SKALANYERI_06
                chkSKALANYERI_07.Checked = .SKALANYERI_07
                chkSKALANYERI_08.Checked = .SKALANYERI_08
                chkSKALANYERI_09.Checked = .SKALANYERI_09
                chkSKALANYERI_10.Checked = .SKALANYERI_10

                chkSKALANEYRI_KRONIS_01.Checked = .SKALANEYRI_KRONIS_01
                txtSKALANEYRI_LOKASI_TEXT.Text = .SKALANEYRI_LOKASI_TEXT
                txtSKALANEYRI_ISTIRAHAT_TEXT.Text = .SKALANEYRI_ISTIRAHAT_TEXT
                txtSKALANEYRI_AKTIVITAS_TEXT.Text = .SKALANEYRI_AKTIVITAS_TEXT
                chkSKALANEYRI_AKUT_01.Checked = .SKALANEYRI_AKUT_01
                txtSKALANEYRI_AKUT_LOKASI_TEXT.Text = .SKALANEYRI_AKUT_LOKASI_TEXT
                txtSKALANEYRI_AKUT_ISTIRAHAT_TEXT.Text = .SKALANEYRI_AKUT_ISTIRAHAT_TEXT
                txtSKALANEYRI_AKUT_AKTIVITAS_TEXT.Text = .SKALANEYRI_AKUT_AKTIVITAS_TEXT

                chkKEBUTUHAN_PENDIDIKAN_DAN_PENGAJARAN_01.Checked = .KEBUTUHAN_PENDIDIKAN_DAN_PENGAJARAN_01
                chkKEBUTUHAN_PENDIDIKAN_DAN_PENGAJARAN_02.Checked = .KEBUTUHAN_PENDIDIKAN_DAN_PENGAJARAN_02
                chkKEBUTUHAN_PENDIDIKAN_DAN_PENGAJARAN_03.Checked = .KEBUTUHAN_PENDIDIKAN_DAN_PENGAJARAN_03
                chkKEBUTUHAN_PENDIDIKAN_DAN_PENGAJARAN_04.Checked = .KEBUTUHAN_PENDIDIKAN_DAN_PENGAJARAN_04
                txtKEBUTUHAN_PENDIDIKAN_DAN_PENGAJARAN_04_TEXT.Text = .KEBUTUHAN_PENDIDIKAN_DAN_PENGAJARAN_04_TEXT

                chkBUTUHPENDIDIKAN_TIDAK.Checked = .BUTUHPENDIDIKAN_TIDAK
                chkBUTUHPENDIDIKAN_YA.Checked = .BUTUHPENDIDIKAN_YA

                chkKEBUTUHAN_EDUKASI_01.Checked = .KEBUTUHAN_EDUKASI_01
                chkKEBUTUHAN_EDUKASI_02.Checked = .KEBUTUHAN_EDUKASI_02
                chkKEBUTUHAN_EDUKASI_03.Checked = .KEBUTUHAN_EDUKASI_03
                chkKEBUTUHAN_EDUKASI_04.Checked = .KEBUTUHAN_EDUKASI_04
                txtKEBUTUHAN_EDUKASI_04_TEXT.Text = .KEBUTUHAN_EDUKASI_04_TEXT

                chkALERGI_TIDAK2.Checked = .ALERGI_TIDAK2
                chkALERGI_YA2.Checked = .ALERGI_YA2
                txtALERGI_TEXT2.Text = .ALERGI_TEXT2

                txtRIWAYAT_PENYAKIT.Text = .RIWAYAT_PENYAKIT
                txtRIWAYAT_IMUNISASI.Text = .RIWAYAT_IMUNISASI

                txtPEMERIKSAAN_PENUNJANG_01.Text = .PEMERIKSAAN_PENUNJANG_01
                chkPEMERIKSAAN_PENUNJANG_02.Checked = .PEMERIKSAAN_PENUNJANG_02
                chkPEMERIKSAAN_PENUNJANG_03.Checked = .PEMERIKSAAN_PENUNJANG_03
                chkPEMERIKSAAN_PENUNJANG_04.Checked = .PEMERIKSAAN_PENUNJANG_04
                txtPEMERIKSAAN_PENUNJANG_04_TEXT.Text = .PEMERIKSAAN_PENUNJANG_04_TEXT

                chkHASIL_PENANGANAN_01.Checked = .HASIL_PENANGANAN_01
                chkHASIL_PENANGANAN_02.Checked = .HASIL_PENANGANAN_02
                chkHASIL_PENANGANAN_03.Checked = .HASIL_PENANGANAN_03
                chkHASIL_PENANGANAN_04.Checked = .HASIL_PENANGANAN_04
                chkHASIL_PENANGANAN_05.Checked = .HASIL_PENANGANAN_05
                txtHASIL_PENANGANAN_05_TEXT.Text = .HASIL_PENANGANAN_05_TEXT
                txtPERENCANAAN_PASIEN_PULANG_01.Text = .PERENCANAAN_PASIEN_PULANG_01
                chkPERENCANAAN_PASIEN_PULANG_02_1.Checked = .PERENCANAAN_PASIEN_PULANG_02_1
                chkPERENCANAAN_PASIEN_PULANG_02_2.Checked = .PERENCANAAN_PASIEN_PULANG_02_2
                chkPERENCANAAN_PASIEN_PULANG_02_3.Checked = .PERENCANAAN_PASIEN_PULANG_02_3
                chkPERENCANAAN_PASIEN_PULANG_02_4.Checked = .PERENCANAAN_PASIEN_PULANG_02_4
                txtPERENCANAAN_PASIEN_PULANG_02_4_TEXT.Text = .PERENCANAAN_PASIEN_PULANG_02_4_TEXT
                chkPERENCANAAN_PASIEN_PULANG_03_1.Checked = .PERENCANAAN_PASIEN_PULANG_03_1
                chkPERENCANAAN_PASIEN_PULANG_03_2.Checked = .PERENCANAAN_PASIEN_PULANG_03_2
                chkPERENCANAAN_PASIEN_PULANG_03_3.Checked = .PERENCANAAN_PASIEN_PULANG_03_3
                chkPERENCANAAN_PASIEN_PULANG_03_4.Checked = .PERENCANAAN_PASIEN_PULANG_03_4
                chkPERENCANAAN_PASIEN_PULANG_03_5.Checked = .PERENCANAAN_PASIEN_PULANG_03_5
                chkPERENCANAAN_PASIEN_PULANG_03_6.Checked = .PERENCANAAN_PASIEN_PULANG_03_6
                chkPERENCANAAN_PASIEN_PULANG_03_6_1.Checked = .PERENCANAAN_PASIEN_PULANG_03_6_1
                chkPERENCANAAN_PASIEN_PULANG_03_6_2.Checked = .PERENCANAAN_PASIEN_PULANG_03_6_2
                chkPERENCANAAN_PASIEN_PULANG_03_7.Checked = .PERENCANAAN_PASIEN_PULANG_03_7
                chkPERENCANAAN_PASIEN_PULANG_03_8.Checked = .PERENCANAAN_PASIEN_PULANG_03_8
                txtPERENCANAAN_PASIEN_PULANG_03_8_TEXT.Text = .PERENCANAAN_PASIEN_PULANG_03_8_TEXT

                txtNamaPasienKeluarga.Text = .PASIEN_KELUARGA

                chkFLACC_1.Checked = .FLACC_1
                chkFLACC_2.Checked = .FLACC_2
                chkFLACC_3.Checked = .FLACC_3
                chkFLACC_4.Checked = .FLACC_4
                chkFLACC_5.Checked = .FLACC_5
                chkFLACC_6.Checked = .FLACC_6

                txtSKORFLACC_1.Text = .SKORFLACC_1
                txtSKORFLACC_2.Text = .SKORFLACC_2
                txtSKORFLACC_3.Text = .SKORFLACC_3
                txtSKORFLACC_4.Text = .SKORFLACC_4
                txtSKORFLACC_5.Text = .SKORFLACC_5

                chkSOSIAL_01.Checked = .SOSIAL_01
                chkSOSIAL_02.Checked = .SOSIAL_02
                chkSOSIAL_03.Checked = .SOSIAL_03
                chkSOSIAL_04.Checked = .SOSIAL_04
                chkSOSIAL_05.Checked = .SOSIAL_05
                chkSOSIAL_06.Checked = .SOSIAL_06
                chkSOSIAL_07.Checked = .SOSIAL_07
                chkSOSIAL_08.Checked = .SOSIAL_08
                chkSOSIAL_09.Checked = .SOSIAL_09
                chkSOSIAL_10.Checked = .SOSIAL_10

                chkMOTORIK_HALUS_01.Checked = .MOTORIK_HALUS_01
                chkMOTORIK_HALUS_02.Checked = .MOTORIK_HALUS_02
                chkMOTORIK_HALUS_03.Checked = .MOTORIK_HALUS_03
                chkMOTORIK_HALUS_04.Checked = .MOTORIK_HALUS_04
                chkMOTORIK_HALUS_05.Checked = .MOTORIK_HALUS_05
                chkMOTORIK_HALUS_06.Checked = .MOTORIK_HALUS_06
                chkMOTORIK_HALUS_07.Checked = .MOTORIK_HALUS_07
                chkMOTORIK_HALUS_08.Checked = .MOTORIK_HALUS_08
                chkMOTORIK_HALUS_09.Checked = .MOTORIK_HALUS_09

                chkMOTORIK_KASAR_01.Checked = .MOTORIK_KASAR_01
                chkMOTORIK_KASAR_02.Checked = .MOTORIK_KASAR_02
                chkMOTORIK_KASAR_03.Checked = .MOTORIK_KASAR_03
                chkMOTORIK_KASAR_04.Checked = .MOTORIK_KASAR_04
                chkMOTORIK_KASAR_05.Checked = .MOTORIK_KASAR_05
                chkMOTORIK_KASAR_06.Checked = .MOTORIK_KASAR_06
                chkMOTORIK_KASAR_07.Checked = .MOTORIK_KASAR_07
                chkMOTORIK_KASAR_08.Checked = .MOTORIK_KASAR_08
                chkMOTORIK_KASAR_09.Checked = .MOTORIK_KASAR_09

                chkBAHASA_01.Checked = .BAHASA_01
                chkBAHASA_02.Checked = .BAHASA_02
                chkBAHASA_03.Checked = .BAHASA_03
                chkBAHASA_04.Checked = .BAHASA_04
                chkBAHASA_05.Checked = .BAHASA_05
                chkBAHASA_06.Checked = .BAHASA_06
                chkBAHASA_07.Checked = .BAHASA_07
                chkBAHASA_08.Checked = .BAHASA_08
                chkBAHASA_09.Checked = .BAHASA_09

                chkUMUR_01.Checked = .UMUR_01
                chkUMUR_02.Checked = .UMUR_02
                chkUMUR_03.Checked = .UMUR_03
                chkUMUR_04.Checked = .UMUR_04
                chkUMUR_05.Checked = .UMUR_05
                chkUMUR_06.Checked = .UMUR_06
                chkUMUR_07.Checked = .UMUR_07
                chkUMUR_08.Checked = .UMUR_08
                chkUMUR_09.Checked = .UMUR_09
                chkUMUR_10.Checked = .UMUR_10

                BindingSource1.DataSource = oDigital.GetDataDetail(sNoid).OrderBy(Function(x) x.SEQ).ToList()
                grdDetail.DataSource = BindingSource1

                txtKDUSERPERAWAT.Text = .KDSIGNATURE

                grvDetail.OptionsSelection.MultiSelect = True
            End With
        Catch oErr As Exception
            MsgBox("Load List Data : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Function ByteArrayToImage(ByVal byteArrayIn() As Byte) As Image
        Using ms As New System.IO.MemoryStream(byteArrayIn)
            Dim returnImage = Image.FromStream(ms)
            Return returnImage
        End Using
    End Function
    Private Function fn_Validate() As Boolean
        Try
            fn_Validate = True
            If sNoid = String.Empty Then
                MsgBox("Dibutuhkan Register", MsgBoxStyle.Exclamation, Me.Text)
                deDATE.Focus()
                fn_Validate = False
                Exit Function
            End If

            'If grdPerawat.Text = String.Empty Then
            '    MsgBox("Dibutuhkan Perawat", MsgBoxStyle.Exclamation, Me.Text)
            '    grdPerawat.Focus()
            '    fn_Validate = False
            '    Exit Function
            'End If

        Catch oErr As Exception
            MsgBox("Validate Data : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Function
    Private Function fn_Save() As Boolean
        Try
            ' ***** HEADER *****
            Dim ds = oDigital.GetStructureHeader
            With ds
                .KDPENDAFTARAN = sNoid
                .KDCUSTOMER = sRM
                .NAMAPASIEN = sNAMA
                .JENISKELAMIN = sJENISKELAMIN
                .TANGGALLAHIR = sTANGGALLAHIR
                .DATE = Now
                .DATECREATED = Now
                Try
                    .DATEUPDATED = oDigital.GetData(sNoid).DATEUPDATED
                Catch ex As Exception
                    .DATEUPDATED = Now
                End Try

                .DATE = deDATE.DateTime

                .ALERGI_TIDAK = chkALERGI_TIDAK.Checked
                .ALERGI_YA = chkALERGI_YA.Checked
                .ALERGI_YA_TEXT = txtALERGI_YA_TEXT.Text
                .AUTO = chkAUTO.Checked
                .ALLO = chkALLO.Checked
                .ALLO_TEXT = txtALLO_TEXT.Text
                .KELUHAN_UTAMA = txtKELUHAN_UTAMA.Text
                .KESADARAN_UMUM = txtKESADARAN_UMUM.Text
                .KEADAAN = txtKEADAAN.Text
                .TANDA_VITAL_TD = txtTANDA_VITAL_TD.Text
                .TANDA_VITAL_NADI = txtTANDA_VITAL_NADI.Text
                .TANDA_VITAL_SUHU = txtTANDA_VITAL_SUHU.Text
                .TANDA_VITAL_R = txtTANDA_VITAL_R.Text
                .TANDA_VITAL_REGULER = chkTANDA_VITAL_REGULER.Checked
                .TANDA_VITAL_IREGULER = chkTANDA_VITAL_IREGULER.Checked
                .TANDA_VITAL_SPO2 = txtTANDA_VITAL_SPO2.Text
                .TANDA_VITAL_BB = txtTANDA_VITAL_BB.Text
                .TANDA_VITAL_TB = txtTANDA_VITAL_TB.Text
                .TANDA_VITAL_LD = txtTANDA_VITAL_LD.Text
                .TANDA_VITAL_LK = txtTANDA_VITAL_LK.Text
                .STATUS_PSIKOLOGIS_01 = chkSTATUS_PSIKOLOGIS_01.Checked
                .STATUS_PSIKOLOGIS_02 = chkSTATUS_PSIKOLOGIS_02.Checked
                .STATUS_PSIKOLOGIS_03 = chkSTATUS_PSIKOLOGIS_03.Checked
                .STATUS_PSIKOLOGIS_04 = chkSTATUS_PSIKOLOGIS_04.Checked
                .STATUS_PSIKOLOGIS_05 = chkSTATUS_PSIKOLOGIS_05.Checked
                .STATUS_PSIKOLOGIS_06 = chkSTATUS_PSIKOLOGIS_06.Checked
                .STATUS_PSIKOLOGIS_06_TEXT = txtSTATUS_PSIKOLOGIS_06_TEXT.Text

                .STATUS_MENTAL_01 = chkSTATUS_MENTAL_01.Checked
                .STATUS_MENTAL_02 = chkSTATUS_MENTAL_02.Checked
                .STATUS_MENTAL_02_TEXT = txtSTATUS_MENTAL_02_TEXT.Text
                .STATUS_MENTAL_03 = chkSTATUS_MENTAL_03.Checked
                .STATUS_MENTAL_03_TEXT = txtSTATUS_MENTAL_03_TEXT.Text

                .SOSIAL_01_1 = chkSOSIAL_01_1.Checked
                .SOSIAL_01_2 = chkSOSIAL_01_2.Checked
                .SOSIAL_02_1 = chkSOSIAL_02_1.Checked
                .SOSIAL_02_2 = chkSOSIAL_02_2.Checked
                .SOSIAL_02_3 = chkSOSIAL_02_3.Checked
                .SOSIAL_02_4 = chkSOSIAL_02_4.Checked
                .SOSIAL_02_4_TEXT = txtSOSIAL_02_4_TEXT.Text

                .SOSIAL_03_1_TEXT = txtSOSIAL_03_1_TEXT.Text
                .SOSIAL_03_2_TEXT = txtSOSIAL_03_2_TEXT.Text
                .SOSIAL_03_3_TEXT = txtSOSIAL_03_3_TEXT.Text

                .STATUS_SPIRITUAL_01_TEXT = txtSTATUS_SPIRITUAL_01_TEXT.Text
                .STATUS_SPIRITUAL_02_TEXT = txtSTATUS_SPIRITUAL_02_TEXT.Text

                .PERSYARAFAN_01 = chkPERSYARAFAN_01.Checked
                .PERSYARAFAN_02 = chkPERSYARAFAN_02.Checked
                .PERSYARAFAN_03 = chkPERSYARAFAN_03.Checked
                .PERSYARAFAN_04 = chkPERSYARAFAN_04.Checked
                .PERSYARAFAN_05 = chkPERSYARAFAN_05.Checked
                .PERSYARAFAN_05_TEXT = txtPERSYARAFAN_05_TEXT.Text

                .PERNAPASAN_01 = chkPERNAPASAN_01.Checked
                .PERNAPASAN_02 = chkPERNAPASAN_02.Checked
                .PERNAPASAN_03 = chkPERNAPASAN_03.Checked
                .PERNAPASAN_04 = chkPERNAPASAN_04.Checked
                .PERNAPASAN_04_TEXT = txtPERNAPASAN_04_TEXT.Text

                .PENCERNAAN_01 = chkPENCERNAAN_01.Checked
                .PENCERNAAN_02 = chkPENCERNAAN_02.Checked
                .PENCERNAAN_03 = chkPENCERNAAN_03.Checked
                .PENCERNAAN_04 = chkPENCERNAAN_04.Checked
                .PENCERNAAN_05 = chkPENCERNAAN_05.Checked
                .PENCERNAAN_05_TEXT = txtPENCERNAAN_05_TEXT.Text

                .ENDROKIN_01 = chkENDROKIN_01.Checked
                .ENDROKIN_02 = chkENDROKIN_02.Checked
                .ENDROKIN_03 = chkENDROKIN_03.Checked
                .ENDROKIN_04 = chkENDROKIN_04.Checked
                .ENDROKIN_05 = chkENDROKIN_05.Checked
                .ENDROKIN_05_TEXT = txtENDROKIN_05_TEXT.Text

                .CARDIOVASKULER_01 = chkCARDIOVASKULER_01.Checked
                .CARDIOVASKULER_02 = chkCARDIOVASKULER_02.Checked
                .CARDIOVASKULER_03 = chkCARDIOVASKULER_03.Checked
                .CARDIOVASKULER_04 = chkCARDIOVASKULER_04.Checked
                .CARDIOVASKULER_04_TEXT = txtCARDIOVASKULER_04_TEXT.Text

                .ABDOMEN_01 = chkABDOMEN_01.Checked
                .ABDOMEN_02 = chkABDOMEN_02.Checked
                .ABDOMEN_03 = chkABDOMEN_03.Checked
                .ABDOMEN_04 = chkABDOMEN_04.Checked
                .ABDOMEN_05 = chkABDOMEN_05.Checked
                .ABDOMEN_05_TEXT = txtABDOMEN_05_TEXT.Text
                .ABDOMEN_06 = chkABDOMEN_06.Checked
                .ABDOMEN_06_TEXT = txtABDOMEN_06_TEXT.Text
                .ABDOMEN_07 = chkABDOMEN_07.Checked
                .ABDOMEN_07_TEXT = txtABDOMEN_07_TEXT.Text
                .ABDOMEN_08 = chkABDOMEN_08.Checked
                .ABDOMEN_08_TEXT = txtABDOMEN_08_TEXT.Text
                .ABDOMEN_09 = chkABDOMEN_09.Checked
                .ABDOMEN_09_TEXT = txtABDOMEN_09_TEXT.Text

                .REPRODUKSI_01 = chkREPRODUKSI_01.Checked
                .REPRODUKSI_01_1 = chkREPRODUKSI_01_1.Checked
                .REPRODUKSI_01_2 = chkREPRODUKSI_01_2.Checked
                .REPRODUKSI_02 = chkREPRODUKSI_02.Checked
                .REPRODUKSI_02_1 = chkREPRODUKSI_02_1.Checked
                .REPRODUKSI_02_1_TEXT = txtREPRODUKSI_02_1_TEXT.Text
                .REPRODUKSI_02_2 = chkREPRODUKSI_02_2.Checked
                .REPRODUKSI_03 = chkREPRODUKSI_03.Checked
                .REPRODUKSI_03_1 = chkREPRODUKSI_03_1.Checked
                .REPRODUKSI_03_1_TEXT = txtREPRODUKSI_03_1_TEXT.Text
                .REPRODUKSI_03_2 = chkREPRODUKSI_03_2.Checked
                .REPRODUKSI_04 = chkREPRODUKSI_04.Checked
                .REPRODUKSI_04_TEXT = txtREPRODUKSI_04_TEXT.Text
                .REPRODUKSI_05 = chkREPRODUKSI_05.Checked
                .REPRODUKSI_05_TEXT = txtREPRODUKSI_05_TEXT.Text
                .REPRODUKSI_06 = chkREPRODUKSI_06.Checked
                .REPRODUKSI_06_TEXT = txtREPRODUKSI_06_TEXT.Text
                .REPRODUKSI_07 = chkREPRODUKSI_07.Checked
                .REPRODUKSI_07_TEXT = txtREPRODUKSI_07_TEXT.Text
                .REPRODUKSI_08 = chkREPRODUKSI_08.Checked
                .REPRODUKSI_08_TEXT = txtREPRODUKSI_08_TEXT.Text

                .KULIT_01 = chkKULIT_01.Checked
                .KULIT_02 = chkKULIT_02.Checked
                .KULIT_02_TEXT = txtKULIT_02_TEXT.Text
                .KULIT_03 = chkKULIT_03.Checked
                .KULIT_03_TEXT = txtKULIT_03_TEXT.Text
                .KULIT_04 = chkKULIT_04.Checked
                .KULIT_05 = chkKULIT_05.Checked
                .KULIT_06 = chkKULIT_06.Checked
                .KULIT_06_TEXT = txtKULIT_06_TEXT.Text

                .URINARIA_01 = chkURINARIA_01.Checked
                .URINARIA_02 = chkURINARIA_02.Checked
                .URINARIA_02_TEXT = txtURINARIA_02_TEXT.Text
                .URINARIA_03 = chkURINARIA_03.Checked
                .URINARIA_03_TEXT = txtURINARIA_03_TEXT.Text
                .URINARIA_04 = chkURINARIA_04.Checked
                .URINARIA_04_TEXT = txtURINARIA_04_TEXT.Text

                .KEADAAN_EMOSIONAL_01 = chkKEADAAN_EMOSIONAL_01.Checked
                .KEADAAN_EMOSIONAL_02 = chkKEADAAN_EMOSIONAL_02.Checked
                .KEADAAN_EMOSIONAL_03 = chkKEADAAN_EMOSIONAL_03.Checked
                .KEADAAN_EMOSIONAL_04 = chkKEADAAN_EMOSIONAL_04.Checked
                .KEADAAN_EMOSIONAL_05 = chkKEADAAN_EMOSIONAL_05.Checked
                .KEADAAN_EMOSIONAL_05_TEXT = txtKEADAAN_EMOSIONAL_05_TEXT.Text

                .MATA_01 = chkMATA_01.Checked
                .MATA_02 = chkMATA_02.Checked
                .MATA_03 = chkMATA_03.Checked
                .MATA_04 = chkMATA_04.Checked
                .MATA_04_TEXT = txtMATA_04_TEXT.Text

                .OTOT_SENDI_TULANG_01 = chkOTOT_SENDI_TULANG_01.Checked
                .OTOT_SENDI_TULANG_02 = chkOTOT_SENDI_TULANG_02.Checked
                .OTOT_SENDI_TULANG_03 = chkOTOT_SENDI_TULANG_03.Checked
                .OTOT_SENDI_TULANG_04 = chkOTOT_SENDI_TULANG_04.Checked
                .OTOT_SENDI_TULANG_04_TEXT = txtOTOT_SENDI_TULANG_04_TEXT.Text

                .MULUT_01 = chkMULUT_01.Checked
                .MULUT_02 = chkMULUT_02.Checked
                .MULUT_02_TEXT = txtMULUT_02_TEXT.Text

                .HIDUNG_MUKA_01 = chkHIDUNG_MUKA_01.Checked
                .HIDUNG_MUKA_02 = chkHIDUNG_MUKA_02.Checked
                .HIDUNG_MUKA_02_TEXT = txtHIDUNG_MUKA_02_TEXT.Text

                .TELINGA_01 = chkTELINGA_01.Checked
                .TELINGA_02 = chkTELINGA_02.Checked
                .TELINGA_02_TEXT = txtTELINGA_02_TEXT.Text

                .TENGGOROKAN_01 = chkTENGGOROKAN_01.Checked
                .TENGGOROKAN_02 = chkTENGGOROKAN_02.Checked
                .TENGGOROKAN_02_TEXT = txtTENGGOROKAN_02_TEXT.Text

                .SKRINING_GIZI_01_1 = chkSKRINING_GIZI_01_1.Checked
                .SKRINING_GIZI_01_2 = chkSKRINING_GIZI_01_2.Checked
                .SKRINING_GIZI_02_1 = chkSKRINING_GIZI_02_1.Checked
                .SKRINING_GIZI_02_2 = chkSKRINING_GIZI_02_2.Checked
                .SKRINING_GIZI_03_1 = chkSKRINING_GIZI_03_1.Checked
                .SKRINING_GIZI_03_2 = chkSKRINING_GIZI_03_2.Checked

                .SKRINING_RESIKO_JATUH_01_1 = chkSKRINING_RESIKO_JATUH_01_1.Checked
                .SKRINING_RESIKO_JATUH_01_2 = chkSKRINING_RESIKO_JATUH_01_2.Checked
                .SKRINING_RESIKO_JATUH_02_1 = chkSKRINING_RESIKO_JATUH_02_1.Checked
                .SKRINING_RESIKO_JATUH_02_2 = chkSKRINING_RESIKO_JATUH_02_2.Checked

                .RESIKO_JATUH_01 = chkRESIKO_JATUH_01.Checked
                .RESIKO_JATUH_02 = chkRESIKO_JATUH_02.Checked
                .RESIKO_JATUH_03 = chkRESIKO_JATUH_03.Checked

                .TINDAKAN_01_YA = chkTINDAKAN_01_YA.Checked
                .TINDAKAN_02_YA = chkTINDAKAN_02_YA.Checked
                .TINDAKAN_03_YA = chkTINDAKAN_03_YA.Checked
                .TINDAKAN_04_YA = chkTINDAKAN_04_YA.Checked
                .TINDAKAN_01_TIDAK = chkTINDAKAN_01_TIDAK.Checked
                .TINDAKAN_02_TIDAK = chkTINDAKAN_02_TIDAK.Checked
                .TINDAKAN_03_TIDAK = chkTINDAKAN_03_TIDAK.Checked
                .TINDAKAN_04_TIDAK = chkTINDAKAN_04_TIDAK.Checked

                .KDSTAFFPERAWAT1 = If(grdPerawat1.EditValue Is Nothing, "", grdPerawat1.EditValue)
                .NAMAPERAWAT1 = grdPerawat1.Text
                .KDSTAFFPERAWAT2 = If(grdPerawat2.EditValue Is Nothing, "", grdPerawat2.EditValue)
                .NAMAPERAWAT2 = grdPerawat2.Text
                .KDSTAFFPERAWAT3 = If(grdPerawat3.EditValue Is Nothing, "", grdPerawat3.EditValue)
                .NAMAPERAWAT3 = grdPerawat3.Text
                .KDSTAFFPERAWAT4 = If(grdPerawat4.EditValue Is Nothing, "", grdPerawat4.EditValue)
                .NAMAPERAWAT4 = grdPerawat4.Text

                .PASIENKELUARGA1 = txtPASIENKELUARGA1.Text
                .PASIENKELUARGA2 = txtPASIENKELUARGA2.Text
                .PASIENKELUARGA3 = txtPASIENKELUARGA3.Text
                .PASIENKELUARGA4 = txtPASIENKELUARGA4.Text

                .SKALANYERI_00 = chkSKALANYERI_00.Checked
                .SKALANYERI_01 = chkSKALANYERI_01.Checked
                .SKALANYERI_02 = chkSKALANYERI_02.Checked
                .SKALANYERI_03 = chkSKALANYERI_03.Checked
                .SKALANYERI_04 = chkSKALANYERI_04.Checked
                .SKALANYERI_05 = chkSKALANYERI_05.Checked
                .SKALANYERI_06 = chkSKALANYERI_06.Checked
                .SKALANYERI_07 = chkSKALANYERI_07.Checked
                .SKALANYERI_08 = chkSKALANYERI_08.Checked
                .SKALANYERI_09 = chkSKALANYERI_09.Checked
                .SKALANYERI_10 = chkSKALANYERI_10.Checked

                .SKALANEYRI_KRONIS_01 = chkSKALANEYRI_KRONIS_01.Checked
                .SKALANEYRI_LOKASI_TEXT = txtSKALANEYRI_LOKASI_TEXT.Text
                .SKALANEYRI_ISTIRAHAT_TEXT = txtSKALANEYRI_ISTIRAHAT_TEXT.Text
                .SKALANEYRI_AKTIVITAS_TEXT = txtSKALANEYRI_AKTIVITAS_TEXT.Text
                .SKALANEYRI_AKUT_01 = chkSKALANEYRI_AKUT_01.Checked
                .SKALANEYRI_AKUT_LOKASI_TEXT = txtSKALANEYRI_AKUT_LOKASI_TEXT.Text
                .SKALANEYRI_AKUT_ISTIRAHAT_TEXT = txtSKALANEYRI_AKUT_ISTIRAHAT_TEXT.Text
                .SKALANEYRI_AKUT_AKTIVITAS_TEXT = txtSKALANEYRI_AKUT_AKTIVITAS_TEXT.Text

                .KEBUTUHAN_PENDIDIKAN_DAN_PENGAJARAN_01 = chkKEBUTUHAN_PENDIDIKAN_DAN_PENGAJARAN_01.Checked
                .KEBUTUHAN_PENDIDIKAN_DAN_PENGAJARAN_02 = chkKEBUTUHAN_PENDIDIKAN_DAN_PENGAJARAN_02.Checked
                .KEBUTUHAN_PENDIDIKAN_DAN_PENGAJARAN_03 = chkKEBUTUHAN_PENDIDIKAN_DAN_PENGAJARAN_03.Checked
                .KEBUTUHAN_PENDIDIKAN_DAN_PENGAJARAN_04 = chkKEBUTUHAN_PENDIDIKAN_DAN_PENGAJARAN_04.Checked
                .KEBUTUHAN_PENDIDIKAN_DAN_PENGAJARAN_04_TEXT = txtKEBUTUHAN_PENDIDIKAN_DAN_PENGAJARAN_04_TEXT.Text

                .BUTUHPENDIDIKAN_TIDAK = chkBUTUHPENDIDIKAN_TIDAK.Checked
                .BUTUHPENDIDIKAN_YA = chkBUTUHPENDIDIKAN_YA.Checked

                .KEBUTUHAN_EDUKASI_01 = chkKEBUTUHAN_EDUKASI_01.Checked
                .KEBUTUHAN_EDUKASI_02 = chkKEBUTUHAN_EDUKASI_02.Checked
                .KEBUTUHAN_EDUKASI_03 = chkKEBUTUHAN_EDUKASI_03.Checked
                .KEBUTUHAN_EDUKASI_04 = chkKEBUTUHAN_EDUKASI_04.Checked
                .KEBUTUHAN_EDUKASI_04_TEXT = txtKEBUTUHAN_EDUKASI_04_TEXT.Text

                .ALERGI_TIDAK2 = chkALERGI_TIDAK2.Checked
                .ALERGI_YA2 = chkALERGI_YA2.Checked
                .ALERGI_TEXT2 = txtALERGI_TEXT2.Text

                .RIWAYAT_PENYAKIT = txtRIWAYAT_PENYAKIT.Text
                .RIWAYAT_IMUNISASI = txtRIWAYAT_IMUNISASI.Text

                .PEMERIKSAAN_PENUNJANG_01 = txtPEMERIKSAAN_PENUNJANG_01.Text
                .PEMERIKSAAN_PENUNJANG_02 = chkPEMERIKSAAN_PENUNJANG_02.Checked
                .PEMERIKSAAN_PENUNJANG_03 = chkPEMERIKSAAN_PENUNJANG_03.Checked
                .PEMERIKSAAN_PENUNJANG_04 = chkPEMERIKSAAN_PENUNJANG_04.Checked
                .PEMERIKSAAN_PENUNJANG_04_TEXT = txtPEMERIKSAAN_PENUNJANG_04_TEXT.Text

                .HASIL_PENANGANAN_01 = chkHASIL_PENANGANAN_01.Checked
                .HASIL_PENANGANAN_02 = chkHASIL_PENANGANAN_02.Checked
                .HASIL_PENANGANAN_03 = chkHASIL_PENANGANAN_03.Checked
                .HASIL_PENANGANAN_04 = chkHASIL_PENANGANAN_04.Checked
                .HASIL_PENANGANAN_05 = chkHASIL_PENANGANAN_05.Checked
                .HASIL_PENANGANAN_05_TEXT = txtHASIL_PENANGANAN_05_TEXT.Text
                .PERENCANAAN_PASIEN_PULANG_01 = txtPERENCANAAN_PASIEN_PULANG_01.Text
                .PERENCANAAN_PASIEN_PULANG_02_1 = chkPERENCANAAN_PASIEN_PULANG_02_1.Checked
                .PERENCANAAN_PASIEN_PULANG_02_2 = chkPERENCANAAN_PASIEN_PULANG_02_2.Checked
                .PERENCANAAN_PASIEN_PULANG_02_3 = chkPERENCANAAN_PASIEN_PULANG_02_3.Checked
                .PERENCANAAN_PASIEN_PULANG_02_4 = chkPERENCANAAN_PASIEN_PULANG_02_4.Checked
                .PERENCANAAN_PASIEN_PULANG_02_4_TEXT = txtPERENCANAAN_PASIEN_PULANG_02_4_TEXT.Text
                .PERENCANAAN_PASIEN_PULANG_03_1 = chkPERENCANAAN_PASIEN_PULANG_03_1.Checked
                .PERENCANAAN_PASIEN_PULANG_03_2 = chkPERENCANAAN_PASIEN_PULANG_03_2.Checked
                .PERENCANAAN_PASIEN_PULANG_03_3 = chkPERENCANAAN_PASIEN_PULANG_03_3.Checked
                .PERENCANAAN_PASIEN_PULANG_03_4 = chkPERENCANAAN_PASIEN_PULANG_03_4.Checked
                .PERENCANAAN_PASIEN_PULANG_03_5 = chkPERENCANAAN_PASIEN_PULANG_03_5.Checked
                .PERENCANAAN_PASIEN_PULANG_03_6 = chkPERENCANAAN_PASIEN_PULANG_03_6.Checked
                .PERENCANAAN_PASIEN_PULANG_03_6_1 = chkPERENCANAAN_PASIEN_PULANG_03_6_1.Checked
                .PERENCANAAN_PASIEN_PULANG_03_6_2 = chkPERENCANAAN_PASIEN_PULANG_03_6_2.Checked
                .PERENCANAAN_PASIEN_PULANG_03_7 = chkPERENCANAAN_PASIEN_PULANG_03_7.Checked
                .PERENCANAAN_PASIEN_PULANG_03_8 = chkPERENCANAAN_PASIEN_PULANG_03_8.Checked
                .PERENCANAAN_PASIEN_PULANG_03_8_TEXT = txtPERENCANAAN_PASIEN_PULANG_03_8_TEXT.Text

                .PASIEN_KELUARGA = txtNamaPasienKeluarga.Text

                'tambahan
                .FLACC_1 = chkFLACC_1.Checked
                .FLACC_2 = chkFLACC_2.Checked
                .FLACC_3 = chkFLACC_3.Checked
                .FLACC_4 = chkFLACC_4.Checked
                .FLACC_5 = chkFLACC_5.Checked
                .FLACC_6 = chkFLACC_6.Checked

                .SKORFLACC_1 = txtSKORFLACC_1.Text
                .SKORFLACC_2 = txtSKORFLACC_2.Text
                .SKORFLACC_3 = txtSKORFLACC_3.Text
                .SKORFLACC_4 = txtSKORFLACC_4.Text
                .SKORFLACC_5 = txtSKORFLACC_5.Text

                .SOSIAL_01 = chkSOSIAL_01.Checked
                .SOSIAL_02 = chkSOSIAL_02.Checked
                .SOSIAL_03 = chkSOSIAL_03.Checked
                .SOSIAL_04 = chkSOSIAL_04.Checked
                .SOSIAL_05 = chkSOSIAL_05.Checked
                .SOSIAL_06 = chkSOSIAL_06.Checked
                .SOSIAL_07 = chkSOSIAL_07.Checked
                .SOSIAL_08 = chkSOSIAL_08.Checked
                .SOSIAL_09 = chkSOSIAL_09.Checked
                .SOSIAL_10 = chkSOSIAL_10.Checked

                .MOTORIK_HALUS_01 = chkMOTORIK_HALUS_01.Checked
                .MOTORIK_HALUS_02 = chkMOTORIK_HALUS_02.Checked
                .MOTORIK_HALUS_03 = chkMOTORIK_HALUS_03.Checked
                .MOTORIK_HALUS_04 = chkMOTORIK_HALUS_04.Checked
                .MOTORIK_HALUS_05 = chkMOTORIK_HALUS_05.Checked
                .MOTORIK_HALUS_06 = chkMOTORIK_HALUS_06.Checked
                .MOTORIK_HALUS_07 = chkMOTORIK_HALUS_07.Checked
                .MOTORIK_HALUS_08 = chkMOTORIK_HALUS_08.Checked
                .MOTORIK_HALUS_09 = chkMOTORIK_HALUS_09.Checked

                .MOTORIK_KASAR_01 = chkMOTORIK_KASAR_01.Checked
                .MOTORIK_KASAR_02 = chkMOTORIK_KASAR_02.Checked
                .MOTORIK_KASAR_03 = chkMOTORIK_KASAR_03.Checked
                .MOTORIK_KASAR_04 = chkMOTORIK_KASAR_04.Checked
                .MOTORIK_KASAR_05 = chkMOTORIK_KASAR_05.Checked
                .MOTORIK_KASAR_06 = chkMOTORIK_KASAR_06.Checked
                .MOTORIK_KASAR_07 = chkMOTORIK_KASAR_07.Checked
                .MOTORIK_KASAR_08 = chkMOTORIK_KASAR_08.Checked
                .MOTORIK_KASAR_09 = chkMOTORIK_KASAR_09.Checked

                .BAHASA_01 = chkBAHASA_01.Checked
                .BAHASA_02 = chkBAHASA_02.Checked
                .BAHASA_03 = chkBAHASA_03.Checked
                .BAHASA_04 = chkBAHASA_04.Checked
                .BAHASA_05 = chkBAHASA_05.Checked
                .BAHASA_06 = chkBAHASA_06.Checked
                .BAHASA_07 = chkBAHASA_07.Checked
                .BAHASA_08 = chkBAHASA_08.Checked
                .BAHASA_09 = chkBAHASA_09.Checked

                .UMUR_01 = chkUMUR_01.Checked
                .UMUR_02 = chkUMUR_02.Checked
                .UMUR_03 = chkUMUR_03.Checked
                .UMUR_04 = chkUMUR_04.Checked
                .UMUR_05 = chkUMUR_05.Checked
                .UMUR_06 = chkUMUR_06.Checked
                .UMUR_07 = chkUMUR_07.Checked
                .UMUR_08 = chkUMUR_08.Checked
                .UMUR_09 = chkUMUR_09.Checked
                .UMUR_10 = chkUMUR_10.Checked

                Try
                    .CETAK = oDigital.GetData(sNoid).CETAK
                Catch ex As Exception
                    .CETAK = 0
                End Try

                .KDUSER = txtKDUSERPERAWAT.Text
                .KDSIGNATURE = sUserID
            End With

            ' ***** DETIL *****
            Dim arrDetail = oDigital.GetStructureDetailList
            For i As Integer = 0 To grvDetail.RowCount - 2
                Dim dsDetail = oDigital.GetStructureDetail
                With dsDetail
                    .SEQ = i
                    .KDPENDAFTARAN = ds.KDPENDAFTARAN
                    .CEK1 = grvDetail.GetRowCellValue(i, colCeklis1)
                    '.KDITEMDIAGNOSAPERAWAT = grvDetail.GetRowCellValue(i, colKDITEMDIAGNOSAPERAWAT)
                    .KDITEMDIAGNOSAPERAWAT = IIf(grvDetail.GetRowCellValue(i, colKDITEMDIAGNOSAPERAWAT) Is Nothing, "-", grvDetail.GetRowCellValue(i, colKDITEMDIAGNOSAPERAWAT))
                    .DIAGNOSA_KEPERAWATAN = IIf(grvDetail.GetRowCellValue(i, colDiagnosaKeperawatan) Is Nothing, "", grvDetail.GetRowCellValue(i, colDiagnosaKeperawatan))
                    .CEK2 = grvDetail.GetRowCellValue(i, colCeklis2)
                    .LUARAN = IIf(grvDetail.GetRowCellValue(i, colLuaran) Is Nothing, "", grvDetail.GetRowCellValue(i, colLuaran))
                    .CEK3 = grvDetail.GetRowCellValue(i, colCeklis3)
                    .INTERVENSI = IIf(grvDetail.GetRowCellValue(i, colIntervensi) Is Nothing, "", grvDetail.GetRowCellValue(i, colIntervensi))
                    .KD_PARAF = ""
                    .NAMA_PARAF = IIf(grvDetail.GetRowCellValue(i, colKDParaf) Is Nothing, "", grvDetail.GetRowCellValue(i, colKDParaf))
                End With
                arrDetail.Add(dsDetail)
            Next

            If oFormMode = FORM_MODE.FORM_MODE_ADD Then
                Try
                    fn_Save = oDigital.InsertData(ds, arrDetail)
                Catch ex As Exception
                    MsgBox("Simpan Data :  " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            ElseIf oFormMode = FORM_MODE.FORM_MODE_EDIT Then
                Try
                    fn_Save = oDigital.UpdateData(ds, arrDetail)
                Catch ex As Exception
                    MsgBox("Simpan Data : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            End If
        Catch oErr As Exception
            MsgBox("Simpan Data : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            fn_Save = False
        End Try
    End Function
#End Region
#Region "Command Button"
    Private Sub frmItem_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        Select Case e.KeyCode
            'Case Keys.F12
            '    btnClose_Click()
            'Case Keys.F3
            '    If btnSaveClose.Enabled = True Then
            '        btnSaveClose_Click()
            '    End If
            Case Keys.PageUp
                fn_ScrollPage(True)
            Case Keys.PageDown
                fn_ScrollPage(False)
        End Select
    End Sub
    Private Sub btnSaveClose_Click() Handles btnSaveClose.ItemClick
        If fn_Validate() = False Then Exit Sub
        If MsgBox("Save " & sNoid.Trim.ToUpper & "?", MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, Me.Text) = MsgBoxResult.No Then Exit Sub
        If fn_Save() = False Then
            MsgBox("Save gagal! Tolong kontak admin anda!", MsgBoxStyle.Exclamation, Me.Text)
        Else
            oFormMode = FORM_MODE.FORM_MODE_EDIT
            MsgBox("Save " & sNoid.Trim.ToUpper & " success!", MsgBoxStyle.Information, Me.Text)
            'Me.Close()
        End If
    End Sub
    Private Sub btnClose_Click() Handles btnClose.ItemClick
        Me.Close()
    End Sub
#End Region
#Region "Lookup / Event"
    Private Sub fn_LoadKDITEMDIAGNOSAPERAWAT()
        Try
            Dim ds = From x In oDiagnosaPerawat.GetDataItemAll()
                     Select x.DESCRIPTION, x.KDITEMDIAGNOSAPERAWAT, x.KATEGORI, x.ISACTIVE

            grdKDITEMDIAGNOSAPERAWAT.Properties.DataSource = ds.Where(Function(x) x.ISACTIVE = True).ToList()
            grdKDITEMDIAGNOSAPERAWAT.Properties.ValueMember = "KDITEMDIAGNOSAPERAWAT"
            grdKDITEMDIAGNOSAPERAWAT.Properties.DisplayMember = "DESCRIPTION"

            grvKDITEMDIAGNOSAPERAWAT.BestFitColumns()

        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
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

    Dim sSEQ As Integer = 0
    Dim sCounter As Integer = 0
    Private Sub btnPilihDiagnosa_Click(sender As Object, e As EventArgs) Handles btnPilihDiagnosa.Click
        If grdKDITEMDIAGNOSAPERAWAT.Text = String.Empty Then
            Exit Sub
        End If

        'getkategori
        Dim sKategori As String = grvKDITEMDIAGNOSAPERAWAT.GetFocusedRowCellValue("KATEGORI")
        Dim desc1 As String = String.Empty
        Dim desc2 As String = String.Empty

        If sKategori = "AKTUAL" Then
            desc1 = " b.d :"
            desc2 = "D.d :"
        ElseIf sKategori = "RISIKO" Then
            desc1 = " dibuktikan dengan :"
            desc2 = "Faktor risiko :"
        ElseIf sKategori = "PROMKES" Then
            desc1 = " dibuktikan dengan :"
            desc2 = "Tanda dan gejala :"
        End If

        isLoad = False

        Dim ds = oDigital.GetDataDetailDiagnosa(sNoid, grdKDITEMDIAGNOSAPERAWAT.EditValue)

        If ds IsNot Nothing Then
            MsgBox("Diagnosa Sudah Ada dengan nomor Kode " & ds.KDITEMDIAGNOSAPERAWAT, MsgBoxStyle.Exclamation, Me.Text)
        Else
            grvDetail.OptionsSelection.MultiSelect = True

            sSEQ = grvDetail.RowCount() - 1

            'nama diagnosa keperawatan
            grvDetail.Focus()
            grvDetail.AddNewRow()
            grvDetail.SetFocusedRowCellValue(colSEQ, sSEQ)
            grvDetail.SetFocusedRowCellValue(colKDITEMDIAGNOSAPERAWAT, grdKDITEMDIAGNOSAPERAWAT.EditValue)
            grvDetail.SetFocusedRowCellValue(colCeklis1, False)
            grvDetail.SetFocusedRowCellValue(colDiagnosaKeperawatan, grdKDITEMDIAGNOSAPERAWAT.Text & " " & grdKDITEMDIAGNOSAPERAWAT.EditValue & desc1)
            grvDetail.SetFocusedRowCellValue(colCeklis2, False)
            grvDetail.SetFocusedRowCellValue(colLuaran, "Setelah dilakukan perawatan dalam waktu ...  menit")
            grvDetail.SetFocusedRowCellValue(colCeklis3, False)
            grvDetail.SetFocusedRowCellValue(colIntervensi, "")
            grvDetail.SetFocusedRowCellValue(colKDParaf, "")
            sSEQ = sSEQ + 1


            'berhubungan dengan
            For Each xloop In oDigital.GetDataItemDetil(grdKDITEMDIAGNOSAPERAWAT.EditValue)
                If xloop.KDITEMDIAGNOSAPERAWAT <> "" And xloop.KDITEMDIAGNOSAPERAWAT <> "...." Then
                    If xloop.BERHUBUNGANDENGAN <> "" Or xloop.KRITERIA <> "" Or xloop.INTERVENSI <> "" Then
                        grvDetail.Focus()
                        grvDetail.AddNewRow()

                        grvDetail.SetFocusedRowCellValue(colSEQ, sSEQ)
                        grvDetail.SetFocusedRowCellValue(colKDITEMDIAGNOSAPERAWAT, xloop.KDITEMDIAGNOSAPERAWAT)
                        grvDetail.SetFocusedRowCellValue(colCeklis1, xloop.ISCHEKED)
                        grvDetail.SetFocusedRowCellValue(colDiagnosaKeperawatan, xloop.BERHUBUNGANDENGAN)
                        grvDetail.SetFocusedRowCellValue(colCeklis2, xloop.ISCHEKED)
                        grvDetail.SetFocusedRowCellValue(colLuaran, xloop.TUJUAN)
                        grvDetail.SetFocusedRowCellValue(colCeklis3, xloop.ISCHEKED)
                        grvDetail.SetFocusedRowCellValue(colIntervensi, xloop.INTERVENSI)
                        grvDetail.SetFocusedRowCellValue(colKDParaf, "")

                        sSEQ = sSEQ + 1
                    End If
                End If
            Next

            'ditandai dengan
            Dim countGrid As Integer = 0
            For i As Integer = 0 To grvDetail.RowCount() - 1
                If grvDetail.GetRowCellValue(i, colDiagnosaKeperawatan) = "" And grvDetail.GetRowCellValue(i, colKDITEMDIAGNOSAPERAWAT) = grdKDITEMDIAGNOSAPERAWAT.EditValue Then
                    countGrid = i
                    Exit For
                End If
                countGrid = countGrid + 1
            Next

            countGrid = countGrid + 1
            grvDetail.SetRowCellValue(countGrid, colDiagnosaKeperawatan, desc2)
            countGrid = countGrid + 1

            For Each yloop In oDigital.GetDataItemDetil(grdKDITEMDIAGNOSAPERAWAT.EditValue)
                If grvDetail.GetRowCellValue(countGrid, colDiagnosaKeperawatan) = "" And grvDetail.GetRowCellValue(countGrid, colKDITEMDIAGNOSAPERAWAT) = grdKDITEMDIAGNOSAPERAWAT.EditValue Then
                    grvDetail.SetRowCellValue(countGrid, colDiagnosaKeperawatan, yloop.DITANDAIDENGAN)
                End If
                countGrid = countGrid + 1
            Next

            ''tujuan
            'Dim countGrid2 As Integer = 0
            'For i As Integer = 0 To grvDetail.RowCount()-1
            '    If grvDetail.GetRowCellValue(i, colLuaran) = "" And grvDetail.GetRowCellValue(i, colKDITEMDIAGNOSAPERAWAT) = grdKDITEMDIAGNOSAPERAWAT.EditValue Then
            '        countGrid2 = i
            '        Exit For
            '    End If
            '    countGrid2 = countGrid2 + 1
            'Next

            'countGrid2 = countGrid2 + 1
            'grvDetail.SetRowCellValue(countGrid2,colLuaran, "Kriteria :")
            'countGrid2 = countGrid2 + 1

            'For Each yloop In oDigital.GetDataItemDetil(grdKDITEMDIAGNOSAPERAWAT.EditValue)
            '    If grvDetail.GetRowCellValue(countGrid2,colLuaran)="" And grvDetail.GetRowCellValue(countGrid2, colKDITEMDIAGNOSAPERAWAT) = grdKDITEMDIAGNOSAPERAWAT.EditValue Then
            '        grvDetail.SetRowCellValue(countGrid2,colLuaran, yloop.KRITERIA)
            '    End If
            '    countGrid2 = countGrid2 + 1
            'Next

            'kriteria
            Dim countGrid3 As Integer = 0
            For i As Integer = 0 To grvDetail.RowCount() - 1
                If grvDetail.GetRowCellValue(i, colLuaran) = "" And grvDetail.GetRowCellValue(i, colKDITEMDIAGNOSAPERAWAT) = grdKDITEMDIAGNOSAPERAWAT.EditValue Then
                    countGrid3 = i
                    Exit For
                End If
                countGrid3 = countGrid3 + 1
            Next

            countGrid3 = countGrid3 + 1
            grvDetail.SetRowCellValue(countGrid3, colLuaran, "Kriteria hasil :")
            countGrid3 = countGrid3 + 1

            For Each yloop In oDigital.GetDataItemDetil(grdKDITEMDIAGNOSAPERAWAT.EditValue)
                If grvDetail.GetRowCellValue(countGrid3, colLuaran) = "" And grvDetail.GetRowCellValue(countGrid3, colKDITEMDIAGNOSAPERAWAT) = grdKDITEMDIAGNOSAPERAWAT.EditValue Then
                    grvDetail.SetRowCellValue(countGrid3, colLuaran, yloop.KRITERIA)
                End If
                countGrid3 = countGrid3 + 1
            Next


            'batas
            grvDetail.Focus()
            grvDetail.AddNewRow()
            grvDetail.SetFocusedRowCellValue(colSEQ, sSEQ)
            grvDetail.SetFocusedRowCellValue(colKDITEMDIAGNOSAPERAWAT, grdKDITEMDIAGNOSAPERAWAT.EditValue)
            grvDetail.SetFocusedRowCellValue(colCeklis1, False)
            grvDetail.SetFocusedRowCellValue(colDiagnosaKeperawatan, "")
            grvDetail.SetFocusedRowCellValue(colCeklis2, False)
            grvDetail.SetFocusedRowCellValue(colLuaran, "")
            grvDetail.SetFocusedRowCellValue(colCeklis3, False)
            grvDetail.SetFocusedRowCellValue(colIntervensi, "")
            grvDetail.SetFocusedRowCellValue(colKDParaf, "")
            sSEQ = sSEQ + 1

        End If

        'grvDetail.BestFitColumns()
        isLoad = True
    End Sub

    Private Sub DeleteToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DeleteToolStripMenuItem.Click
        If oFormMode = FORM_MODE.FORM_MODE_VIEW Then Exit Sub
        grvDetail.DeleteSelectedRows()
    End Sub
    Private Sub fn_LoadHistoryAsesmen()
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
            SQL &= "TANGGAL = A.DATE  "
            SQL &= ",KDASESMEN = A.KDPENDAFTARAN "
            SQL &= ",A.KELUHAN_UTAMA "
            SQL &= ",A.KDUSER "
            SQL &= "FROM  "
            SQL &= "S_DIGITAL_ASKEP_RAWATJALAN A "
            SQL &= "WHERE A.KDCUSTOMER = '" & sRM & "'  "

            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "R_ASKEPRAJAL")

            grd_RiwayatAskep.MainView = grv_RiwayatAskep
            grd_RiwayatAskep.DataSource = ds.Tables("R_ASKEPRAJAL")
            grd_RiwayatAskep.ForceInitialize()

            grv_RiwayatAskep.BestFitColumns()
            grv_RiwayatAskep.Columns("KDASESMEN").Visible = False

            If oConn.State = ConnectionState.Open Then
                oConn.Close()
            End If

        Catch oErr As Exception
            MsgBox("Information : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub CopyAsesmenToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CopyAsesmenToolStripMenuItem.Click
        If grv_RiwayatAskep.GetFocusedRowCellValue("KDASESMEN") Is Nothing Then
            Exit Sub
        End If

        SplashScreenManager.ShowForm(Me, GetType(frmLoadingPanel), True, True, False)

        SplashScreenManager.Default.SetWaitFormCaption("Processing data.....")

        Try
            ' ***** HEADER *****
            Dim ds = oDigital.GetData(grv_RiwayatAskep.GetFocusedRowCellValue("KDASESMEN"))

            With ds

                chkALERGI_TIDAK.Checked = .ALERGI_TIDAK
                chkALERGI_YA.Checked = .ALERGI_YA
                txtALERGI_YA_TEXT.Text = .ALERGI_YA_TEXT
                chkAUTO.Checked = .AUTO
                chkALLO.Checked = .ALLO
                txtALLO_TEXT.Text = .ALLO_TEXT
                txtKELUHAN_UTAMA.Text = .KELUHAN_UTAMA
                txtKESADARAN_UMUM.Text = .KESADARAN_UMUM
                txtKEADAAN.Text = .KEADAAN
                txtTANDA_VITAL_TD.Text = .TANDA_VITAL_TD
                txtTANDA_VITAL_NADI.Text = .TANDA_VITAL_NADI
                txtTANDA_VITAL_SUHU.Text = .TANDA_VITAL_SUHU
                txtTANDA_VITAL_R.Text = .TANDA_VITAL_R
                chkTANDA_VITAL_REGULER.Checked = .TANDA_VITAL_REGULER
                chkTANDA_VITAL_IREGULER.Checked = .TANDA_VITAL_IREGULER
                txtTANDA_VITAL_SPO2.Text = .TANDA_VITAL_SPO2
                txtTANDA_VITAL_BB.Text = .TANDA_VITAL_BB
                txtTANDA_VITAL_TB.Text = .TANDA_VITAL_TB
                txtTANDA_VITAL_LD.Text = .TANDA_VITAL_LD
                txtTANDA_VITAL_LK.Text = .TANDA_VITAL_LK
                chkSTATUS_PSIKOLOGIS_01.Checked = .STATUS_PSIKOLOGIS_01
                chkSTATUS_PSIKOLOGIS_02.Checked = .STATUS_PSIKOLOGIS_02
                chkSTATUS_PSIKOLOGIS_03.Checked = .STATUS_PSIKOLOGIS_03
                chkSTATUS_PSIKOLOGIS_04.Checked = .STATUS_PSIKOLOGIS_04
                chkSTATUS_PSIKOLOGIS_05.Checked = .STATUS_PSIKOLOGIS_05
                chkSTATUS_PSIKOLOGIS_06.Checked = .STATUS_PSIKOLOGIS_06
                txtSTATUS_PSIKOLOGIS_06_TEXT.Text = .STATUS_PSIKOLOGIS_06_TEXT

                chkSTATUS_MENTAL_01.Checked = .STATUS_MENTAL_01
                chkSTATUS_MENTAL_02.Checked = .STATUS_MENTAL_02
                txtSTATUS_MENTAL_02_TEXT.Text = .STATUS_MENTAL_02_TEXT
                chkSTATUS_MENTAL_03.Checked = .STATUS_MENTAL_03
                txtSTATUS_MENTAL_03_TEXT.Text = .STATUS_MENTAL_03_TEXT

                chkSOSIAL_01_1.Checked = .SOSIAL_01_1
                chkSOSIAL_01_2.Checked = .SOSIAL_01_2
                chkSOSIAL_02_1.Checked = .SOSIAL_02_1
                chkSOSIAL_02_2.Checked = .SOSIAL_02_2
                chkSOSIAL_02_3.Checked = .SOSIAL_02_3
                chkSOSIAL_02_4.Checked = .SOSIAL_02_4
                txtSOSIAL_02_4_TEXT.Text = .SOSIAL_02_4_TEXT

                txtSOSIAL_03_1_TEXT.Text = .SOSIAL_03_1_TEXT
                txtSOSIAL_03_2_TEXT.Text = .SOSIAL_03_2_TEXT
                txtSOSIAL_03_3_TEXT.Text = .SOSIAL_03_3_TEXT

                txtSTATUS_SPIRITUAL_01_TEXT.Text = .STATUS_SPIRITUAL_01_TEXT
                txtSTATUS_SPIRITUAL_02_TEXT.Text = .STATUS_SPIRITUAL_02_TEXT

                chkPERSYARAFAN_01.Checked = .PERSYARAFAN_01
                chkPERSYARAFAN_02.Checked = .PERSYARAFAN_02
                chkPERSYARAFAN_03.Checked = .PERSYARAFAN_03
                chkPERSYARAFAN_04.Checked = .PERSYARAFAN_04
                chkPERSYARAFAN_05.Checked = .PERSYARAFAN_05
                txtPERSYARAFAN_05_TEXT.Text = .PERSYARAFAN_05_TEXT

                chkPERNAPASAN_01.Checked = .PERNAPASAN_01
                chkPERNAPASAN_02.Checked = .PERNAPASAN_02
                chkPERNAPASAN_03.Checked = .PERNAPASAN_03
                chkPERNAPASAN_04.Checked = .PERNAPASAN_04
                txtPERNAPASAN_04_TEXT.Text = .PERNAPASAN_04_TEXT

                chkPENCERNAAN_01.Checked = .PENCERNAAN_01
                chkPENCERNAAN_02.Checked = .PENCERNAAN_02
                chkPENCERNAAN_03.Checked = .PENCERNAAN_03
                chkPENCERNAAN_04.Checked = .PENCERNAAN_04
                chkPENCERNAAN_05.Checked = .PENCERNAAN_05
                txtPENCERNAAN_05_TEXT.Text = .PENCERNAAN_05_TEXT

                chkENDROKIN_01.Checked = .ENDROKIN_01
                chkENDROKIN_02.Checked = .ENDROKIN_02
                chkENDROKIN_03.Checked = .ENDROKIN_03
                chkENDROKIN_04.Checked = .ENDROKIN_04
                chkENDROKIN_05.Checked = .ENDROKIN_05
                txtENDROKIN_05_TEXT.Text = .ENDROKIN_05_TEXT

                chkCARDIOVASKULER_01.Checked = .CARDIOVASKULER_01
                chkCARDIOVASKULER_02.Checked = .CARDIOVASKULER_02
                chkCARDIOVASKULER_03.Checked = .CARDIOVASKULER_03
                chkCARDIOVASKULER_04.Checked = .CARDIOVASKULER_04
                txtCARDIOVASKULER_04_TEXT.Text = .CARDIOVASKULER_04_TEXT

                chkABDOMEN_01.Checked = .ABDOMEN_01
                chkABDOMEN_02.Checked = .ABDOMEN_02
                chkABDOMEN_03.Checked = .ABDOMEN_03
                chkABDOMEN_04.Checked = .ABDOMEN_04
                chkABDOMEN_05.Checked = .ABDOMEN_05
                txtABDOMEN_05_TEXT.Text = .ABDOMEN_05_TEXT
                chkABDOMEN_06.Checked = .ABDOMEN_06
                txtABDOMEN_06_TEXT.Text = .ABDOMEN_06_TEXT
                chkABDOMEN_07.Checked = .ABDOMEN_07
                txtABDOMEN_07_TEXT.Text = .ABDOMEN_07_TEXT
                chkABDOMEN_08.Checked = .ABDOMEN_08
                txtABDOMEN_08_TEXT.Text = .ABDOMEN_08_TEXT
                chkABDOMEN_09.Checked = .ABDOMEN_09
                txtABDOMEN_09_TEXT.Text = .ABDOMEN_09_TEXT

                chkREPRODUKSI_01.Checked = .REPRODUKSI_01
                chkREPRODUKSI_01_1.Checked = .REPRODUKSI_01_1
                chkREPRODUKSI_01_2.Checked = .REPRODUKSI_01_2
                chkREPRODUKSI_02.Checked = .REPRODUKSI_02
                chkREPRODUKSI_02_1.Checked = .REPRODUKSI_02_1
                txtREPRODUKSI_02_1_TEXT.Text = .REPRODUKSI_02_1_TEXT
                chkREPRODUKSI_02_2.Checked = .REPRODUKSI_02_2
                chkREPRODUKSI_03.Checked = .REPRODUKSI_03
                chkREPRODUKSI_03_1.Checked = .REPRODUKSI_03_1
                txtREPRODUKSI_03_1_TEXT.Text = .REPRODUKSI_03_1_TEXT
                chkREPRODUKSI_03_2.Checked = .REPRODUKSI_03_2
                chkREPRODUKSI_04.Checked = .REPRODUKSI_04
                txtREPRODUKSI_04_TEXT.Text = .REPRODUKSI_04_TEXT
                chkREPRODUKSI_05.Checked = .REPRODUKSI_05
                txtREPRODUKSI_05_TEXT.Text = .REPRODUKSI_05_TEXT
                chkREPRODUKSI_06.Checked = .REPRODUKSI_06
                txtREPRODUKSI_06_TEXT.Text = .REPRODUKSI_06_TEXT
                chkREPRODUKSI_07.Checked = .REPRODUKSI_07
                txtREPRODUKSI_07_TEXT.Text = .REPRODUKSI_07_TEXT
                chkREPRODUKSI_08.Checked = .REPRODUKSI_08
                txtREPRODUKSI_08_TEXT.Text = .REPRODUKSI_08_TEXT

                chkKULIT_01.Checked = .KULIT_01
                chkKULIT_02.Checked = .KULIT_02
                txtKULIT_02_TEXT.Text = .KULIT_02_TEXT
                chkKULIT_03.Checked = .KULIT_03
                txtKULIT_03_TEXT.Text = .KULIT_03_TEXT
                chkKULIT_04.Checked = .KULIT_04
                chkKULIT_05.Checked = .KULIT_05
                chkKULIT_06.Checked = .KULIT_06
                txtKULIT_06_TEXT.Text = .KULIT_06_TEXT

                chkURINARIA_01.Checked = .URINARIA_01
                chkURINARIA_02.Checked = .URINARIA_02
                txtURINARIA_02_TEXT.Text = .URINARIA_02_TEXT
                chkURINARIA_03.Checked = .URINARIA_03
                txtURINARIA_03_TEXT.Text = .URINARIA_03_TEXT
                chkURINARIA_04.Checked = .URINARIA_04
                txtURINARIA_04_TEXT.Text = .URINARIA_04_TEXT

                chkKEADAAN_EMOSIONAL_01.Checked = .KEADAAN_EMOSIONAL_01
                chkKEADAAN_EMOSIONAL_02.Checked = .KEADAAN_EMOSIONAL_02
                chkKEADAAN_EMOSIONAL_03.Checked = .KEADAAN_EMOSIONAL_03
                chkKEADAAN_EMOSIONAL_04.Checked = .KEADAAN_EMOSIONAL_04
                chkKEADAAN_EMOSIONAL_05.Checked = .KEADAAN_EMOSIONAL_05
                txtKEADAAN_EMOSIONAL_05_TEXT.Text = .KEADAAN_EMOSIONAL_05_TEXT

                chkMATA_01.Checked = .MATA_01
                chkMATA_02.Checked = .MATA_02
                chkMATA_03.Checked = .MATA_03
                chkMATA_04.Checked = .MATA_04
                txtMATA_04_TEXT.Text = .MATA_04_TEXT

                chkOTOT_SENDI_TULANG_01.Checked = .OTOT_SENDI_TULANG_01
                chkOTOT_SENDI_TULANG_02.Checked = .OTOT_SENDI_TULANG_02
                chkOTOT_SENDI_TULANG_03.Checked = .OTOT_SENDI_TULANG_03
                chkOTOT_SENDI_TULANG_04.Checked = .OTOT_SENDI_TULANG_04
                txtOTOT_SENDI_TULANG_04_TEXT.Text = .OTOT_SENDI_TULANG_04_TEXT

                chkMULUT_01.Checked = .MULUT_01
                chkMULUT_02.Checked = .MULUT_02
                txtMULUT_02_TEXT.Text = .MULUT_02_TEXT

                chkHIDUNG_MUKA_01.Checked = .HIDUNG_MUKA_01
                chkHIDUNG_MUKA_02.Checked = .HIDUNG_MUKA_02
                txtHIDUNG_MUKA_02_TEXT.Text = .HIDUNG_MUKA_02_TEXT

                chkTELINGA_01.Checked = .TELINGA_01
                chkTELINGA_02.Checked = .TELINGA_02
                txtTELINGA_02_TEXT.Text = .TELINGA_02_TEXT

                chkTENGGOROKAN_01.Checked = .TENGGOROKAN_01
                chkTENGGOROKAN_02.Checked = .TENGGOROKAN_02
                txtTENGGOROKAN_02_TEXT.Text = .TENGGOROKAN_02_TEXT

                chkSKRINING_GIZI_01_1.Checked = .SKRINING_GIZI_01_1
                chkSKRINING_GIZI_01_2.Checked = .SKRINING_GIZI_01_2
                chkSKRINING_GIZI_02_1.Checked = .SKRINING_GIZI_02_1
                chkSKRINING_GIZI_02_2.Checked = .SKRINING_GIZI_02_2
                chkSKRINING_GIZI_03_1.Checked = .SKRINING_GIZI_03_1
                chkSKRINING_GIZI_03_2.Checked = .SKRINING_GIZI_03_2

                chkSKRINING_RESIKO_JATUH_01_1.Checked = .SKRINING_RESIKO_JATUH_01_1
                chkSKRINING_RESIKO_JATUH_01_2.Checked = .SKRINING_RESIKO_JATUH_01_2
                chkSKRINING_RESIKO_JATUH_02_1.Checked = .SKRINING_RESIKO_JATUH_02_1
                chkSKRINING_RESIKO_JATUH_02_2.Checked = .SKRINING_RESIKO_JATUH_02_2

                chkRESIKO_JATUH_01.Checked = .RESIKO_JATUH_01
                chkRESIKO_JATUH_02.Checked = .RESIKO_JATUH_02
                chkRESIKO_JATUH_03.Checked = .RESIKO_JATUH_03

                chkTINDAKAN_01_YA.Checked = .TINDAKAN_01_YA
                chkTINDAKAN_02_YA.Checked = .TINDAKAN_02_YA
                chkTINDAKAN_03_YA.Checked = .TINDAKAN_03_YA
                chkTINDAKAN_04_YA.Checked = .TINDAKAN_04_YA
                chkTINDAKAN_01_TIDAK.Checked = .TINDAKAN_01_TIDAK
                chkTINDAKAN_02_TIDAK.Checked = .TINDAKAN_02_TIDAK
                chkTINDAKAN_03_TIDAK.Checked = .TINDAKAN_03_TIDAK
                chkTINDAKAN_04_TIDAK.Checked = .TINDAKAN_04_TIDAK

                grdPerawat1.EditValue = .KDSTAFFPERAWAT1
                grdPerawat2.EditValue = .KDSTAFFPERAWAT2
                grdPerawat3.EditValue = .KDSTAFFPERAWAT3
                grdPerawat4.EditValue = .KDSTAFFPERAWAT4

                txtPASIENKELUARGA1.Text = .PASIENKELUARGA1
                txtPASIENKELUARGA2.Text = .PASIENKELUARGA2
                txtPASIENKELUARGA3.Text = .PASIENKELUARGA3
                txtPASIENKELUARGA4.Text = .PASIENKELUARGA4

                chkSKALANYERI_00.Checked = .SKALANYERI_00
                chkSKALANYERI_01.Checked = .SKALANYERI_01
                chkSKALANYERI_02.Checked = .SKALANYERI_02
                chkSKALANYERI_03.Checked = .SKALANYERI_03
                chkSKALANYERI_04.Checked = .SKALANYERI_04
                chkSKALANYERI_05.Checked = .SKALANYERI_05
                chkSKALANYERI_06.Checked = .SKALANYERI_06
                chkSKALANYERI_07.Checked = .SKALANYERI_07
                chkSKALANYERI_08.Checked = .SKALANYERI_08
                chkSKALANYERI_09.Checked = .SKALANYERI_09
                chkSKALANYERI_10.Checked = .SKALANYERI_10

                chkSKALANEYRI_KRONIS_01.Checked = .SKALANEYRI_KRONIS_01
                txtSKALANEYRI_LOKASI_TEXT.Text = .SKALANEYRI_LOKASI_TEXT
                txtSKALANEYRI_ISTIRAHAT_TEXT.Text = .SKALANEYRI_ISTIRAHAT_TEXT
                txtSKALANEYRI_AKTIVITAS_TEXT.Text = .SKALANEYRI_AKTIVITAS_TEXT
                chkSKALANEYRI_AKUT_01.Checked = .SKALANEYRI_AKUT_01
                txtSKALANEYRI_AKUT_LOKASI_TEXT.Text = .SKALANEYRI_AKUT_LOKASI_TEXT
                txtSKALANEYRI_AKUT_ISTIRAHAT_TEXT.Text = .SKALANEYRI_AKUT_ISTIRAHAT_TEXT
                txtSKALANEYRI_AKUT_AKTIVITAS_TEXT.Text = .SKALANEYRI_AKUT_AKTIVITAS_TEXT

                chkKEBUTUHAN_PENDIDIKAN_DAN_PENGAJARAN_01.Checked = .KEBUTUHAN_PENDIDIKAN_DAN_PENGAJARAN_01
                chkKEBUTUHAN_PENDIDIKAN_DAN_PENGAJARAN_02.Checked = .KEBUTUHAN_PENDIDIKAN_DAN_PENGAJARAN_02
                chkKEBUTUHAN_PENDIDIKAN_DAN_PENGAJARAN_03.Checked = .KEBUTUHAN_PENDIDIKAN_DAN_PENGAJARAN_03
                chkKEBUTUHAN_PENDIDIKAN_DAN_PENGAJARAN_04.Checked = .KEBUTUHAN_PENDIDIKAN_DAN_PENGAJARAN_04
                txtKEBUTUHAN_PENDIDIKAN_DAN_PENGAJARAN_04_TEXT.Text = .KEBUTUHAN_PENDIDIKAN_DAN_PENGAJARAN_04_TEXT

                chkBUTUHPENDIDIKAN_TIDAK.Checked = .BUTUHPENDIDIKAN_TIDAK
                chkBUTUHPENDIDIKAN_YA.Checked = .BUTUHPENDIDIKAN_YA

                chkKEBUTUHAN_EDUKASI_01.Checked = .KEBUTUHAN_EDUKASI_01
                chkKEBUTUHAN_EDUKASI_02.Checked = .KEBUTUHAN_EDUKASI_02
                chkKEBUTUHAN_EDUKASI_03.Checked = .KEBUTUHAN_EDUKASI_03
                chkKEBUTUHAN_EDUKASI_04.Checked = .KEBUTUHAN_EDUKASI_04
                txtKEBUTUHAN_EDUKASI_04_TEXT.Text = .KEBUTUHAN_EDUKASI_04_TEXT

                chkALERGI_TIDAK2.Checked = .ALERGI_TIDAK2
                chkALERGI_YA2.Checked = .ALERGI_YA2
                txtALERGI_TEXT2.Text = .ALERGI_TEXT2

                txtRIWAYAT_PENYAKIT.Text = .RIWAYAT_PENYAKIT
                txtRIWAYAT_IMUNISASI.Text = .RIWAYAT_IMUNISASI

                txtPEMERIKSAAN_PENUNJANG_01.Text = .PEMERIKSAAN_PENUNJANG_01
                chkPEMERIKSAAN_PENUNJANG_02.Checked = .PEMERIKSAAN_PENUNJANG_02
                chkPEMERIKSAAN_PENUNJANG_03.Checked = .PEMERIKSAAN_PENUNJANG_03
                chkPEMERIKSAAN_PENUNJANG_04.Checked = .PEMERIKSAAN_PENUNJANG_04
                txtPEMERIKSAAN_PENUNJANG_04_TEXT.Text = .PEMERIKSAAN_PENUNJANG_04_TEXT

                chkHASIL_PENANGANAN_01.Checked = .HASIL_PENANGANAN_01
                chkHASIL_PENANGANAN_02.Checked = .HASIL_PENANGANAN_02
                chkHASIL_PENANGANAN_03.Checked = .HASIL_PENANGANAN_03
                chkHASIL_PENANGANAN_04.Checked = .HASIL_PENANGANAN_04
                chkHASIL_PENANGANAN_05.Checked = .HASIL_PENANGANAN_05
                txtHASIL_PENANGANAN_05_TEXT.Text = .HASIL_PENANGANAN_05_TEXT
                txtPERENCANAAN_PASIEN_PULANG_01.Text = .PERENCANAAN_PASIEN_PULANG_01
                chkPERENCANAAN_PASIEN_PULANG_02_1.Checked = .PERENCANAAN_PASIEN_PULANG_02_1
                chkPERENCANAAN_PASIEN_PULANG_02_2.Checked = .PERENCANAAN_PASIEN_PULANG_02_2
                chkPERENCANAAN_PASIEN_PULANG_02_3.Checked = .PERENCANAAN_PASIEN_PULANG_02_3
                chkPERENCANAAN_PASIEN_PULANG_02_4.Checked = .PERENCANAAN_PASIEN_PULANG_02_4
                txtPERENCANAAN_PASIEN_PULANG_02_4_TEXT.Text = .PERENCANAAN_PASIEN_PULANG_02_4_TEXT
                chkPERENCANAAN_PASIEN_PULANG_03_1.Checked = .PERENCANAAN_PASIEN_PULANG_03_1
                chkPERENCANAAN_PASIEN_PULANG_03_2.Checked = .PERENCANAAN_PASIEN_PULANG_03_2
                chkPERENCANAAN_PASIEN_PULANG_03_3.Checked = .PERENCANAAN_PASIEN_PULANG_03_3
                chkPERENCANAAN_PASIEN_PULANG_03_4.Checked = .PERENCANAAN_PASIEN_PULANG_03_4
                chkPERENCANAAN_PASIEN_PULANG_03_5.Checked = .PERENCANAAN_PASIEN_PULANG_03_5
                chkPERENCANAAN_PASIEN_PULANG_03_6.Checked = .PERENCANAAN_PASIEN_PULANG_03_6
                chkPERENCANAAN_PASIEN_PULANG_03_6_1.Checked = .PERENCANAAN_PASIEN_PULANG_03_6_1
                chkPERENCANAAN_PASIEN_PULANG_03_6_2.Checked = .PERENCANAAN_PASIEN_PULANG_03_6_2
                chkPERENCANAAN_PASIEN_PULANG_03_7.Checked = .PERENCANAAN_PASIEN_PULANG_03_7
                chkPERENCANAAN_PASIEN_PULANG_03_8.Checked = .PERENCANAAN_PASIEN_PULANG_03_8
                txtPERENCANAAN_PASIEN_PULANG_03_8_TEXT.Text = .PERENCANAAN_PASIEN_PULANG_03_8_TEXT

                txtNamaPasienKeluarga.Text = .PASIEN_KELUARGA

                chkFLACC_1.Checked = .FLACC_1
                chkFLACC_2.Checked = .FLACC_2
                chkFLACC_3.Checked = .FLACC_3
                chkFLACC_4.Checked = .FLACC_4
                chkFLACC_5.Checked = .FLACC_5
                chkFLACC_6.Checked = .FLACC_6

                txtSKORFLACC_1.Text = .SKORFLACC_1
                txtSKORFLACC_2.Text = .SKORFLACC_2
                txtSKORFLACC_3.Text = .SKORFLACC_3
                txtSKORFLACC_4.Text = .SKORFLACC_4
                txtSKORFLACC_5.Text = .SKORFLACC_5

                chkSOSIAL_01.Checked = .SOSIAL_01
                chkSOSIAL_02.Checked = .SOSIAL_02
                chkSOSIAL_03.Checked = .SOSIAL_03
                chkSOSIAL_04.Checked = .SOSIAL_04
                chkSOSIAL_05.Checked = .SOSIAL_05
                chkSOSIAL_06.Checked = .SOSIAL_06
                chkSOSIAL_07.Checked = .SOSIAL_07
                chkSOSIAL_08.Checked = .SOSIAL_08
                chkSOSIAL_09.Checked = .SOSIAL_09
                chkSOSIAL_10.Checked = .SOSIAL_10

                chkMOTORIK_HALUS_01.Checked = .MOTORIK_HALUS_01
                chkMOTORIK_HALUS_02.Checked = .MOTORIK_HALUS_02
                chkMOTORIK_HALUS_03.Checked = .MOTORIK_HALUS_03
                chkMOTORIK_HALUS_04.Checked = .MOTORIK_HALUS_04
                chkMOTORIK_HALUS_05.Checked = .MOTORIK_HALUS_05
                chkMOTORIK_HALUS_06.Checked = .MOTORIK_HALUS_06
                chkMOTORIK_HALUS_07.Checked = .MOTORIK_HALUS_07
                chkMOTORIK_HALUS_08.Checked = .MOTORIK_HALUS_08
                chkMOTORIK_HALUS_09.Checked = .MOTORIK_HALUS_09

                chkMOTORIK_KASAR_01.Checked = .MOTORIK_KASAR_01
                chkMOTORIK_KASAR_02.Checked = .MOTORIK_KASAR_02
                chkMOTORIK_KASAR_03.Checked = .MOTORIK_KASAR_03
                chkMOTORIK_KASAR_04.Checked = .MOTORIK_KASAR_04
                chkMOTORIK_KASAR_05.Checked = .MOTORIK_KASAR_05
                chkMOTORIK_KASAR_06.Checked = .MOTORIK_KASAR_06
                chkMOTORIK_KASAR_07.Checked = .MOTORIK_KASAR_07
                chkMOTORIK_KASAR_08.Checked = .MOTORIK_KASAR_08
                chkMOTORIK_KASAR_09.Checked = .MOTORIK_KASAR_09

                chkBAHASA_01.Checked = .BAHASA_01
                chkBAHASA_02.Checked = .BAHASA_02
                chkBAHASA_03.Checked = .BAHASA_03
                chkBAHASA_04.Checked = .BAHASA_04
                chkBAHASA_05.Checked = .BAHASA_05
                chkBAHASA_06.Checked = .BAHASA_06
                chkBAHASA_07.Checked = .BAHASA_07
                chkBAHASA_08.Checked = .BAHASA_08
                chkBAHASA_09.Checked = .BAHASA_09

                chkUMUR_01.Checked = .UMUR_01
                chkUMUR_02.Checked = .UMUR_02
                chkUMUR_03.Checked = .UMUR_03
                chkUMUR_04.Checked = .UMUR_04
                chkUMUR_05.Checked = .UMUR_05
                chkUMUR_06.Checked = .UMUR_06
                chkUMUR_07.Checked = .UMUR_07
                chkUMUR_08.Checked = .UMUR_08
                chkUMUR_09.Checked = .UMUR_09
                chkUMUR_10.Checked = .UMUR_10

                BindingSource1.DataSource = oDigital.GetDataDetail(ds.KDPENDAFTARAN)
                grdDetail.DataSource = BindingSource1

                txtKDUSERPERAWAT.Text = sUSERPERAWAT_x

                grvDetail.OptionsSelection.MultiSelect = True
            End With
            SplashScreenManager.CloseForm(False)

        Catch oErr As Exception
            SplashScreenManager.CloseForm(False)
            MsgBox("Load List Data : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try

    End Sub

    Private Sub frmAskepRawatJalan_MouseWheel(sender As Object, e As MouseEventArgs) Handles MyBase.MouseWheel
        If e.Delta > 0 Then
            'up
            fn_ScrollPage(True)
        Else
            'down
            fn_ScrollPage(False)
        End If
    End Sub

    Private Sub fn_ScrollPage(ByVal isUp As Boolean)
        Dim myView As Point = Me.Panel3.AutoScrollPosition
        Dim scrollchange As Integer = 50

        If isUp Then
            'up
            myView.X = -myView.X
            myView.y = -scrollchange - myView.Y
        Else
            'down
            myView.X = -myView.X
            myView.y = scrollchange - myView.Y
        End If

        Me.Panel3.AutoScrollPosition = myView
    End Sub
#End Region
End Class