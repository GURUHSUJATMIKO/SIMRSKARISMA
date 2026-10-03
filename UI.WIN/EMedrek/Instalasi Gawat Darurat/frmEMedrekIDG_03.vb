Imports DataAccess
Imports UI.WIN.MAIN.My.Resources
Imports System.Linq
Imports System.Data.SqlClient

Public Class frmEMedrekIDG_03
#Region "Declaration"
    Private oFormMode As FORM_MODE = FORM_MODE.FORM_MODE_VIEW
    Private isLoad As Boolean = False
    Private oS_DIGITAL_IGD_03 As New Transaksi.clsDigital_IGD_03
    Private down As Boolean = False

#End Region
#Region "Function"
    Public Sub LoadMe(ByVal KDREG As String, ByVal KDCUSTOMER As String, ByVal NAMAPASIEN As String, ByVal USIA As String, ByVal TGL As String, ByVal DOKTER As String)
        txtNoRegister.Text = KDREG
        txtNoPasien.Text = KDCUSTOMER
        txtNamaPasien.Text = NAMAPASIEN
        txtUmur.Text = USIA
        txtTanggalDaftar.Text = TGL
        txtTujuan.Text = "IGD"
        txtDokter.Text = DOKTER
    End Sub
    Private Sub Form_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        fn_ChangeFormState()
        isLoad = True
    End Sub
    Private Sub Form_Closing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        Dispose()
        sCode = txtNoRegister.Text.Trim.ToUpper
    End Sub
    Private Overloads Sub Dispose()
        MyBase.Dispose()
        Me.Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
    Private Sub fn_ChangeFormState()
        Dim ds = oS_DIGITAL_IGD_03.GetData(txtNoRegister.Text)

        If ds Is Nothing Then
            oFormMode = FORM_MODE.FORM_MODE_ADD
        Else
            oFormMode = FORM_MODE.FORM_MODE_EDIT
        End If

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
        chkKENDARAAN_1.Properties.ReadOnly = Status
        chkKENDARAAN_2.Properties.ReadOnly = Status
        chkKENDARAAN_3.Properties.ReadOnly = Status
        txtKENDARAAN_KETERANGAN.Properties.ReadOnly = Status
        chkASALPASIEN_1.Properties.ReadOnly = Status
        chkASALPASIEN_2.Properties.ReadOnly = Status
        txtASALPASIEN_KETERANGAN.Properties.ReadOnly = Status
        chkWAWANCARA_1.Properties.ReadOnly = Status
        chkWAWANCARA_2.Properties.ReadOnly = Status
        txtWAWANCARA_KETERANGAN.Properties.ReadOnly = Status
        chkALERGI_1.Properties.ReadOnly = Status
        chkALERGI_2.Properties.ReadOnly = Status
        txtALERGI_KETERANGAN.Properties.ReadOnly = Status
        chkTRIAGE_1.Properties.ReadOnly = Status
        chkTRIAGE_2.Properties.ReadOnly = Status
        chkTRIAGE_3.Properties.ReadOnly = Status
        chkTRIAGE_4.Properties.ReadOnly = Status
        txtAIRWAY.Properties.ReadOnly = Status
        txtBREATHING.Properties.ReadOnly = Status
        txtCISRCULATION.Properties.ReadOnly = Status
        txtKELUHANUTAMA.Properties.ReadOnly = Status
        txtRIWAYATMENSTRUASI_TEXT_1.Properties.ReadOnly = Status
        txtRIWAYATMENSTRUASI_TEXT_2.Properties.ReadOnly = Status
        txtRIWAYATMENSTRUASI_TEXT_3.Properties.ReadOnly = Status
        txtRIWAYATMENSTRUASI_TEXT_4.Properties.ReadOnly = Status
        txtRIWAYATMENSTRUASI_TEXT_5.Properties.ReadOnly = Status
        chkRIWAYATMENSTRUASI_6_1.Properties.ReadOnly = Status
        chkRIWAYATMENSTRUASI_6_2.Properties.ReadOnly = Status
        txtRIWAYATMENSTRUASI_TEXT_7.Properties.ReadOnly = Status
        chkRIWAYATMENSTRUASI_8_1.Properties.ReadOnly = Status
        chkRIWAYATMENSTRUASI_8_2.Properties.ReadOnly = Status
        txtPERGERAKANJANIN.Properties.ReadOnly = Status
        txtPERKAWINANKE_LAMANYA_TEXT.Properties.ReadOnly = Status
        txtUMURLAKILAKI.Properties.ReadOnly = Status
        txtUMURPEREMPUAN.Properties.ReadOnly = Status
        chkRIWAYATKONTRASEPSI_1.Properties.ReadOnly = Status
        chkRIWAYATKONTRASEPSI_2.Properties.ReadOnly = Status
        chkRIWAYATKONTRASEPSI_3.Properties.ReadOnly = Status
        chkRIWAYATKONTRASEPSI_4.Properties.ReadOnly = Status
        chkRIWAYATKONTRASEPSI_5.Properties.ReadOnly = Status
        chkRIWAYATPENYAKITSEKARANG_1.Properties.ReadOnly = Status
        chkRIWAYATPENYAKITSEKARANG_2.Properties.ReadOnly = Status
        chkRIWAYATPENYAKITSEKARANG_3.Properties.ReadOnly = Status
        chkRIWAYATPENYAKITSEKARANG_4.Properties.ReadOnly = Status
        chkRIWAYATPENYAKITSEKARANG_5.Properties.ReadOnly = Status
        chkRIWAYATPENYAKITSEKARANG_6.Properties.ReadOnly = Status
        chkRIWAYATPENYAKITDAHULU_1.Properties.ReadOnly = Status
        chkRIWAYATPENYAKITDAHULU_2.Properties.ReadOnly = Status
        chkRIWAYATPENYAKITDAHULU_3.Properties.ReadOnly = Status
        chkRIWAYATPENYAKITDAHULU_4.Properties.ReadOnly = Status
        chkRIWAYATPENYAKITDAHULU_5.Properties.ReadOnly = Status
        chkRIWAYATPENYAKITKELUARGA_1.Properties.ReadOnly = Status
        chkRIWAYATPENYAKITKELUARGA_2.Properties.ReadOnly = Status
        chkRIWAYATPENYAKITKELUARGA_3.Properties.ReadOnly = Status
        chkRIWAYATPENYAKITKELUARGA_4.Properties.ReadOnly = Status
        chkRIWAYATPENYAKITKELUARGA_5.Properties.ReadOnly = Status
        chkRIWAYATGYNECOLOG_1.Properties.ReadOnly = Status
        chkRIWAYATGYNECOLOG_2.Properties.ReadOnly = Status
        chkRIWAYATGYNECOLOG_3.Properties.ReadOnly = Status
        chkRIWAYATGYNECOLOG_4.Properties.ReadOnly = Status
        chkRIWAYATGYNECOLOG_5.Properties.ReadOnly = Status
        chkRIWAYATGYNECOLOG_6.Properties.ReadOnly = Status
        chkRIWAYATGYNECOLOG_7.Properties.ReadOnly = Status
        chkRIWAYATGYNECOLOG_8.Properties.ReadOnly = Status
        chkRIWAYATGYNECOLOG_9.Properties.ReadOnly = Status
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
        txtKEADAANUMUM.Properties.ReadOnly = Status
        txtKESADARAN_TEXT_1.Properties.ReadOnly = Status
        txtKESADARAN_TEXT_2.Properties.ReadOnly = Status
        txtKESADARAN_TEXT_3.Properties.ReadOnly = Status
        txtKESADARAN_TEXT_4.Properties.ReadOnly = Status
        txtKESADARAN_TEXT_5.Properties.ReadOnly = Status
        chkKESADARAN_1.Properties.ReadOnly = Status
        chkKESADARAN_2.Properties.ReadOnly = Status
        chkKESADARAN_3.Properties.ReadOnly = Status
        chkKESADARAN_4.Properties.ReadOnly = Status
        chkKEADAANEMOSIONAL_1.Properties.ReadOnly = Status
        chkKEADAANEMOSIONAL_2.Properties.ReadOnly = Status
        txtKEADAANEMOSIONAL_TEXT_1.Properties.ReadOnly = Status
        txtKEADAANEMOSIONAL_TEXT_2.Properties.ReadOnly = Status
        txtKEADAANEMOSIONAL_TEXT_3.Properties.ReadOnly = Status
        chkPEMERIKSAANFISIKMUKA_1.Properties.ReadOnly = Status
        chkPEMERIKSAANFISIKMUKA_2.Properties.ReadOnly = Status
        chkPEMERIKSAANFISIKMATA_1.Properties.ReadOnly = Status
        chkPEMERIKSAANFISIKMATA_2.Properties.ReadOnly = Status
        chkPEMERIKSAANFISIKMATA_3.Properties.ReadOnly = Status
        chkPEMERIKSAANFISIKMATA_4.Properties.ReadOnly = Status
        chkPEMERIKSAANFISIKLEHER_1.Properties.ReadOnly = Status
        chkPEMERIKSAANFISIKLEHER_2.Properties.ReadOnly = Status
        chkPEMERIKSAANFISIKLEHER_3.Properties.ReadOnly = Status
        chkPEMERIKSAANFISIKLEHER_4.Properties.ReadOnly = Status
        chkPEMERIKSAANFISIKLEHER_5.Properties.ReadOnly = Status
        chkPEMERIKSAANFISIKLEHER_6.Properties.ReadOnly = Status
        chkPEMERIKSAANFISIKDADA_1.Properties.ReadOnly = Status
        chkPEMERIKSAANFISIKDADA_2.Properties.ReadOnly = Status
        chkPEMERIKSAANFISIKDADA_3.Properties.ReadOnly = Status
        chkPEMERIKSAANFISIKDADA_4.Properties.ReadOnly = Status
        chkPEMERIKSAANFISIKDADA_5.Properties.ReadOnly = Status
        chkPEMERIKSAANFISIKDADA_6.Properties.ReadOnly = Status
        chkPEMERIKSAANFISIKDADA_7.Properties.ReadOnly = Status
        chkPEMERIKSAANFISIKDADA_8.Properties.ReadOnly = Status
        chkPEMERIKSAANFISIKDADA_9.Properties.ReadOnly = Status
        chkPEMERIKSAANFISIKDADA_10.Properties.ReadOnly = Status
        chkPEMERIKSAANFISIK_OEDEM_1.Properties.ReadOnly = Status
        chkPEMERIKSAANFISIK_OEDEM_2.Properties.ReadOnly = Status
        chkPEMERIKSAANFISIK_VARICES_1.Properties.ReadOnly = Status
        chkPEMERIKSAANFISIK_VARICES_2.Properties.ReadOnly = Status
        chkPEMERIKSAANFISIK_KEKUATANOTOT_1.Properties.ReadOnly = Status
        chkPEMERIKSAANFISIK_KEKUATANOTOT_2.Properties.ReadOnly = Status
        chkPEMERIKSAANFISIK_REFLEX_1.Properties.ReadOnly = Status
        chkPEMERIKSAANFISIK_REFLEX_2.Properties.ReadOnly = Status
        chkPEMERIKSAANFISIK_KEMERAHAN_1.Properties.ReadOnly = Status
        chkPEMERIKSAANFISIK_KEMERAHAN_2.Properties.ReadOnly = Status
        chkPEMERIKSAANFISIK_BEKASLUKAOPERASI_1.Properties.ReadOnly = Status
        chkPEMERIKSAANFISIK_BEKASLUKAOPERASI_2.Properties.ReadOnly = Status
        txtPEMERIKSAANFISIK_TFU_TEXT.Properties.ReadOnly = Status
        txtPEMERIKSAANFISIK_LEOPOLD_1_TEXT.Properties.ReadOnly = Status
        txtPEMERIKSAANFISIK_LEOPOLD_2_TEXT.Properties.ReadOnly = Status
        txtPEMERIKSAANFISIK_LEOPOLD_3_TEXT.Properties.ReadOnly = Status
        txtPEMERIKSAANFISIK_LEOPOLD_4_TEXT.Properties.ReadOnly = Status
        txtPEMERIKSAANFISIK_LEOPOLD_5_TEXT.Properties.ReadOnly = Status
        txtPEMERIKSAANFISIK_LEOPOLD_6_TEXT.Properties.ReadOnly = Status
        txtPEMERIKSAANFISIK_LEOPOLD_7_TEXT.Properties.ReadOnly = Status
        txtPEMERIKSAANFISIK_LEOPOLD_8_TEXT.Properties.ReadOnly = Status
        txtPEMERIKSAANFISIK_TBBJ_TEXT.Properties.ReadOnly = Status
        chkPEMERIKSAANFISIK_INTESITAS_1.Properties.ReadOnly = Status
        chkPEMERIKSAANFISIK_INTESITAS_2.Properties.ReadOnly = Status
        chkGINECOLOGY_1.Properties.ReadOnly = Status
        chkGYNECOLOGI_1_1.Properties.ReadOnly = Status
        chkGINECOLOGY_2.Properties.ReadOnly = Status
        'chkGINECOLOGY_3.Properties.ReadOnly = Status
        'chkGINECOLOGY_4.Properties.ReadOnly = Status
        'chkGINECOLOGY_5.Properties.ReadOnly = Status
        chkGINECOLOGY_6.Properties.ReadOnly = Status
        chkGINECOLOGY_7.Properties.ReadOnly = Status
        chkGINECOLOGY_8.Properties.ReadOnly = Status
        chkGINECOLOGY_9.Properties.ReadOnly = Status
        txtGINECOLOGY_10_TEXT.Properties.ReadOnly = Status
        txtNIFAS_1_TEXT.Properties.ReadOnly = Status
        chkNIFAS_2.Properties.ReadOnly = Status
        chkNIFAS_3.Properties.ReadOnly = Status
        chkNIFAS_4.Properties.ReadOnly = Status
        chkNIFAS_5.Properties.ReadOnly = Status
        chkNIFAS_6.Properties.ReadOnly = Status
        chkNIFAS_7.Properties.ReadOnly = Status
        chkNIFAS_8.Properties.ReadOnly = Status
        chkNIFAS_9.Properties.ReadOnly = Status
        chkNIFAS_10.Properties.ReadOnly = Status
        txtNIFAS_11_TEXT.Properties.ReadOnly = Status
        txtNIFAS_12_TEXT.Properties.ReadOnly = Status
        txtPEMERIKSAANDALAM_1_TEXT.Properties.ReadOnly = Status
        txtPEMERIKSAANDALAM_2_TEXT.Properties.ReadOnly = Status
        txtPEMERIKSAANDALAM_3_TEXT.Properties.ReadOnly = Status
        txtPEMERIKSAANDALAM_4_TEXT.Properties.ReadOnly = Status
        txtPEMERIKSAANDALAM_5_TEXT.Properties.ReadOnly = Status
        txtPEMERIKSAANDALAM_6_TEXT.Properties.ReadOnly = Status
        'chkPEMERIKSAANDALAM_6_1.Properties.ReadOnly = Status
        'chkPEMERIKSAANDALAM_6_2.Properties.ReadOnly = Status
        txtPEMERIKSAANPENUNJANG_1_TEXT.Properties.ReadOnly = Status
        txtPEMERIKSAANPENUNJANG_2_TEXT.Properties.ReadOnly = Status
        txtPEMERIKSAANPENUNJANG_3_TEXT.Properties.ReadOnly = Status
        txtPEMERIKSAANPENUNJANG_4_TEXT.Properties.ReadOnly = Status
        txtPEMERIKSAANPENUNJANG_5_TEXT.Properties.ReadOnly = Status
        txtPEMERIKSAANPENUNJANG_6_TEXT.Properties.ReadOnly = Status
        txtANALISADATA_TEXT.Properties.ReadOnly = Status
        txtPENATALAKSANAAN_TEXT.Properties.ReadOnly = Status
        chkRIWAYATIMUNISASI_1.Properties.ReadOnly = Status
        chkRIWAYATIMUNISASI_2.Properties.ReadOnly = Status
        chkRIWAYATPERKAWINAN_1.Properties.ReadOnly = Status
        chkRIWAYATPERKAWINAN_2.Properties.ReadOnly = Status
        txtMENGETAHUI.Properties.ReadOnly = Status
        'grvDetail.OptionsBehavior.ReadOnly = Status


        TextEdit1.Properties.ReadOnly = Status
        TextEdit2.Properties.ReadOnly = Status
        TextEdit3.Properties.ReadOnly = Status
        '.TextEdit4 = ""
        '.TextEdit5 = ""
        chkSOSIAL_01_1.Properties.ReadOnly = Status
        chkSOSIAL_01_2.Properties.ReadOnly = Status
        chkSOSIAL_02_1.Properties.ReadOnly = Status
        chkSOSIAL_02_2.Properties.ReadOnly = Status
        chkSOSIAL_02_3.Properties.ReadOnly = Status
        chkSOSIAL_02_4.Properties.ReadOnly = Status

        'grvDetail.OptionsBehavior.ReadOnly = Status

        txtSOSIAL_03_1_TEXT.Properties.ReadOnly = Status
        txtSOSIAL_03_2_TEXT.Properties.ReadOnly = Status
        txtSOSIAL_03_3_TEXT.Properties.ReadOnly = Status
        txtSTATUS_SPIRITUAL_01_TEXT.Properties.ReadOnly = Status
        txtSTATUS_SPIRITUAL_02_TEXT.Properties.ReadOnly = Status
    End Sub
    Private Sub fn_EmptyMe()
        'txtCODE.Text = "<--- AUTO --->"
        deDATE.DateTime = Now
        chkKENDARAAN_1.Checked = False
        chkKENDARAAN_2.Checked = False
        chkKENDARAAN_3.Checked = False
        txtKENDARAAN_KETERANGAN.ResetText()
        chkASALPASIEN_1.Checked = False
        chkASALPASIEN_2.Checked = False
        txtASALPASIEN_KETERANGAN.ResetText()
        chkWAWANCARA_1.Checked = False
        chkWAWANCARA_2.Checked = False
        txtWAWANCARA_KETERANGAN.ResetText()
        chkALERGI_1.Checked = False
        chkALERGI_2.Checked = False
        txtALERGI_KETERANGAN.ResetText()
        chkTRIAGE_1.Checked = False
        chkTRIAGE_2.Checked = False
        chkTRIAGE_3.Checked = False
        chkTRIAGE_4.Checked = False
        txtAIRWAY.ResetText()
        txtBREATHING.ResetText()
        txtCISRCULATION.ResetText()
        txtKELUHANUTAMA.ResetText()
        txtRIWAYATMENSTRUASI_TEXT_1.ResetText()
        txtRIWAYATMENSTRUASI_TEXT_2.ResetText()
        txtRIWAYATMENSTRUASI_TEXT_3.ResetText()
        txtRIWAYATMENSTRUASI_TEXT_4.ResetText()
        txtRIWAYATMENSTRUASI_TEXT_5.ResetText()
        chkRIWAYATMENSTRUASI_6_1.Checked = False
        chkRIWAYATMENSTRUASI_6_2.Checked = False
        txtRIWAYATMENSTRUASI_TEXT_7.ResetText()
        chkRIWAYATMENSTRUASI_8_1.Checked = False
        chkRIWAYATMENSTRUASI_8_2.Checked = False
        txtPERGERAKANJANIN.ResetText()
        txtPERKAWINANKE_LAMANYA_TEXT.ResetText()
        txtUMURLAKILAKI.ResetText()
        txtUMURPEREMPUAN.ResetText()
        chkRIWAYATKONTRASEPSI_1.Checked = False
        chkRIWAYATKONTRASEPSI_2.Checked = False
        chkRIWAYATKONTRASEPSI_3.Checked = False
        chkRIWAYATKONTRASEPSI_4.Checked = False
        chkRIWAYATKONTRASEPSI_5.Checked = False
        chkRIWAYATPENYAKITSEKARANG_1.Checked = False
        chkRIWAYATPENYAKITSEKARANG_2.Checked = False
        chkRIWAYATPENYAKITSEKARANG_3.Checked = False
        chkRIWAYATPENYAKITSEKARANG_4.Checked = False
        chkRIWAYATPENYAKITSEKARANG_5.Checked = False
        chkRIWAYATPENYAKITSEKARANG_6.Checked = False
        chkRIWAYATPENYAKITDAHULU_1.Checked = False
        chkRIWAYATPENYAKITDAHULU_2.Checked = False
        chkRIWAYATPENYAKITDAHULU_3.Checked = False
        chkRIWAYATPENYAKITDAHULU_4.Checked = False
        chkRIWAYATPENYAKITDAHULU_5.Checked = False
        chkRIWAYATPENYAKITKELUARGA_1.Checked = False
        chkRIWAYATPENYAKITKELUARGA_2.Checked = False
        chkRIWAYATPENYAKITKELUARGA_3.Checked = False
        chkRIWAYATPENYAKITKELUARGA_4.Checked = False
        chkRIWAYATPENYAKITKELUARGA_5.Checked = False
        chkRIWAYATGYNECOLOG_1.Checked = False
        chkRIWAYATGYNECOLOG_2.Checked = False
        chkRIWAYATGYNECOLOG_3.Checked = False
        chkRIWAYATGYNECOLOG_4.Checked = False
        chkRIWAYATGYNECOLOG_5.Checked = False
        chkRIWAYATGYNECOLOG_6.Checked = False
        chkRIWAYATGYNECOLOG_7.Checked = False
        chkRIWAYATGYNECOLOG_8.Checked = False
        chkRIWAYATGYNECOLOG_9.Checked = False
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
        txtKEADAANUMUM.ResetText()
        txtKESADARAN_TEXT_1.ResetText()
        txtKESADARAN_TEXT_2.ResetText()
        txtKESADARAN_TEXT_3.ResetText()
        txtKESADARAN_TEXT_4.ResetText()
        txtKESADARAN_TEXT_5.ResetText()
        chkKESADARAN_1.Checked = False
        chkKESADARAN_2.Checked = False
        chkKESADARAN_3.Checked = False
        chkKESADARAN_4.Checked = False
        chkKEADAANEMOSIONAL_1.Checked = False
        chkKEADAANEMOSIONAL_2.Checked = False
        txtKEADAANEMOSIONAL_TEXT_1.ResetText()
        txtKEADAANEMOSIONAL_TEXT_2.ResetText()
        txtKEADAANEMOSIONAL_TEXT_3.ResetText()
        chkPEMERIKSAANFISIKMUKA_1.Checked = False
        chkPEMERIKSAANFISIKMUKA_2.Checked = False
        chkPEMERIKSAANFISIKMATA_1.Checked = False
        chkPEMERIKSAANFISIKMATA_2.Checked = False
        chkPEMERIKSAANFISIKMATA_3.Checked = False
        chkPEMERIKSAANFISIKMATA_4.Checked = False
        chkPEMERIKSAANFISIKLEHER_1.Checked = False
        chkPEMERIKSAANFISIKLEHER_2.Checked = False
        chkPEMERIKSAANFISIKLEHER_3.Checked = False
        chkPEMERIKSAANFISIKLEHER_4.Checked = False
        chkPEMERIKSAANFISIKLEHER_5.Checked = False
        chkPEMERIKSAANFISIKLEHER_6.Checked = False
        chkPEMERIKSAANFISIKDADA_1.Checked = False
        chkPEMERIKSAANFISIKDADA_2.Checked = False
        chkPEMERIKSAANFISIKDADA_3.Checked = False
        chkPEMERIKSAANFISIKDADA_4.Checked = False
        chkPEMERIKSAANFISIKDADA_5.Checked = False
        chkPEMERIKSAANFISIKDADA_6.Checked = False
        chkPEMERIKSAANFISIKDADA_7.Checked = False
        chkPEMERIKSAANFISIKDADA_8.Checked = False
        chkPEMERIKSAANFISIKDADA_9.Checked = False
        chkPEMERIKSAANFISIKDADA_10.Checked = False
        chkPEMERIKSAANFISIK_OEDEM_1.Checked = False
        chkPEMERIKSAANFISIK_OEDEM_2.Checked = False
        chkPEMERIKSAANFISIK_VARICES_1.Checked = False
        chkPEMERIKSAANFISIK_VARICES_2.Checked = False
        chkPEMERIKSAANFISIK_KEKUATANOTOT_1.Checked = False
        chkPEMERIKSAANFISIK_KEKUATANOTOT_2.Checked = False
        chkPEMERIKSAANFISIK_REFLEX_1.Checked = False
        chkPEMERIKSAANFISIK_REFLEX_2.Checked = False
        chkPEMERIKSAANFISIK_KEMERAHAN_1.Checked = False
        chkPEMERIKSAANFISIK_KEMERAHAN_2.Checked = False
        chkPEMERIKSAANFISIK_BEKASLUKAOPERASI_1.Checked = False
        chkPEMERIKSAANFISIK_BEKASLUKAOPERASI_2.Checked = False
        txtPEMERIKSAANFISIK_TFU_TEXT.ResetText()
        txtPEMERIKSAANFISIK_LEOPOLD_1_TEXT.ResetText()
        txtPEMERIKSAANFISIK_LEOPOLD_2_TEXT.ResetText()
        txtPEMERIKSAANFISIK_LEOPOLD_3_TEXT.ResetText()
        txtPEMERIKSAANFISIK_LEOPOLD_4_TEXT.ResetText()
        txtPEMERIKSAANFISIK_LEOPOLD_5_TEXT.ResetText()
        txtPEMERIKSAANFISIK_LEOPOLD_6_TEXT.ResetText()
        txtPEMERIKSAANFISIK_LEOPOLD_7_TEXT.ResetText()
        txtPEMERIKSAANFISIK_LEOPOLD_8_TEXT.ResetText()
        txtPEMERIKSAANFISIK_TBBJ_TEXT.ResetText()
        chkPEMERIKSAANFISIK_INTESITAS_1.Checked = False
        chkPEMERIKSAANFISIK_INTESITAS_2.Checked = False
        chkGINECOLOGY_1.Checked = False
        chkGYNECOLOGI_1_1.Checked = False
        chkGINECOLOGY_2.Checked = False
        chkGINECOLOGY_3.Checked = False
        chkGINECOLOGY_4.Checked = False
        chkGINECOLOGY_5.Checked = False
        chkGINECOLOGY_6.Checked = False
        chkGINECOLOGY_7.Checked = False
        chkGINECOLOGY_8.Checked = False
        chkGINECOLOGY_9.Checked = False
        txtGINECOLOGY_10_TEXT.ResetText()
        txtNIFAS_1_TEXT.ResetText()
        chkNIFAS_2.Checked = False
        chkNIFAS_3.Checked = False
        chkNIFAS_4.Checked = False
        chkNIFAS_5.Checked = False
        chkNIFAS_6.Checked = False
        chkNIFAS_7.Checked = False
        chkNIFAS_8.Checked = False
        chkNIFAS_9.Checked = False
        chkNIFAS_10.Checked = False
        txtNIFAS_11_TEXT.ResetText()
        txtNIFAS_12_TEXT.ResetText()
        txtPEMERIKSAANDALAM_1_TEXT.ResetText()
        txtPEMERIKSAANDALAM_2_TEXT.ResetText()
        txtPEMERIKSAANDALAM_3_TEXT.ResetText()
        txtPEMERIKSAANDALAM_4_TEXT.ResetText()
        txtPEMERIKSAANDALAM_5_TEXT.ResetText()
        txtPEMERIKSAANDALAM_6_TEXT.ResetText()
        'chkPEMERIKSAANDALAM_6_1.Checked = False
        'chkPEMERIKSAANDALAM_6_2.Checked = False
        txtPEMERIKSAANPENUNJANG_1_TEXT.ResetText()
        txtPEMERIKSAANPENUNJANG_2_TEXT.ResetText()
        txtPEMERIKSAANPENUNJANG_3_TEXT.ResetText()
        txtPEMERIKSAANPENUNJANG_4_TEXT.ResetText()
        txtPEMERIKSAANPENUNJANG_5_TEXT.ResetText()
        txtPEMERIKSAANPENUNJANG_6_TEXT.ResetText()
        txtANALISADATA_TEXT.ResetText()
        txtPENATALAKSANAAN_TEXT.ResetText()
        chkRIWAYATIMUNISASI_1.Checked = False
        chkRIWAYATIMUNISASI_2.Checked = False
        chkRIWAYATPERKAWINAN_1.Checked = False
        chkRIWAYATPERKAWINAN_2.Checked = False
        txtMENGETAHUI.ResetText()
        txtRELOAD.ResetText()


        TextEdit1.ResetText()
        TextEdit2.ResetText()
        TextEdit3.ResetText()
        '.TextEdit4 = ""
        '.TextEdit5 = ""
        chkSOSIAL_01_1.Checked = False
        chkSOSIAL_01_2.Checked = False
        chkSOSIAL_02_1.Checked = False
        chkSOSIAL_02_2.Checked = False
        chkSOSIAL_02_3.Checked = False
        chkSOSIAL_02_4.Checked = False

        txtSOSIAL_03_1_TEXT.ResetText()
        txtSOSIAL_03_2_TEXT.ResetText()
        txtSOSIAL_03_3_TEXT.ResetText()
        txtSTATUS_SPIRITUAL_01_TEXT.ResetText()
        txtSTATUS_SPIRITUAL_02_TEXT.ResetText()
    End Sub
    Private Sub fn_LoadData()
        Try
            ' ***** HEADER *****
            Dim ds = oS_DIGITAL_IGD_03.GetData(txtNoRegister.Text)

            With ds
                deDATE.DateTime = .DATE
                chkKENDARAAN_1.Checked = .KENDARAAN_1
                chkKENDARAAN_2.Checked = .KENDARAAN_2
                chkKENDARAAN_3.Checked = .KENDARAAN_3
                txtKENDARAAN_KETERANGAN.Text = .KENDARAAN_KETERANGAN
                chkASALPASIEN_1.Checked = .ASALPASIEN_1
                chkASALPASIEN_2.Checked = .ASALPASIEN_2
                txtASALPASIEN_KETERANGAN.Text = .ASALPASIEN_KETERANGAN
                chkWAWANCARA_1.Checked = .WAWANCARA_1
                chkWAWANCARA_2.Checked = .WAWANCARA_2
                txtWAWANCARA_KETERANGAN.Text = .WAWANCARA_KETERANGAN
                chkALERGI_1.Checked = .ALERGI_1
                chkALERGI_2.Checked = .ALERGI_2
                txtALERGI_KETERANGAN.Text = .ALERGI_KETERANGAN
                chkTRIAGE_1.Checked = .TRIAGE_1
                chkTRIAGE_2.Checked = .TRIAGE_2
                chkTRIAGE_3.Checked = .TRIAGE_3
                chkTRIAGE_4.Checked = .TRIAGE_4
                txtAIRWAY.Text = .TRIAGE_AIRWAY
                txtBREATHING.Text = .TRIAGE_BREATHING
                txtCISRCULATION.Text = .TRIAGE_CIRCULATION
                txtKELUHANUTAMA.Text = .KELUHANUTAMA
                txtRIWAYATMENSTRUASI_TEXT_1.Text = .RIWAYATMENSTRUASI_TEXT_1
                txtRIWAYATMENSTRUASI_TEXT_2.Text = .RIWAYATMENSTRUASI_TEXT_2
                txtRIWAYATMENSTRUASI_TEXT_3.Text = .RIWAYATMENSTRUASI_TEXT_3
                txtRIWAYATMENSTRUASI_TEXT_4.Text = .RIWAYATMENSTRUASI_TEXT_4
                txtRIWAYATMENSTRUASI_TEXT_5.Text = .RIWAYATMENSTRUASI_TEXT_5
                chkRIWAYATMENSTRUASI_6_1.Checked = .RIWAYATMENSTRUASI_6_1
                chkRIWAYATMENSTRUASI_6_2.Checked = .RIWAYATMENSTRUASI_6_2
                txtRIWAYATMENSTRUASI_TEXT_7.Text = .RIWAYATMENSTRUASI_TEXT_7
                chkRIWAYATMENSTRUASI_8_1.Checked = .RIWAYATMENSTRUASI_8_1
                chkRIWAYATMENSTRUASI_8_2.Checked = .RIWAYATMENSTRUASI_8_2
                txtPERGERAKANJANIN.Text = .PERGERAKANJANIN
                txtPERKAWINANKE_TEXT.Text = .PERKAWINANKE_TEXT
                txtPERKAWINANKE_LAMANYA_TEXT.Text = .PERKAWINANKE_LAMANYA_TEXT
                txtUMURLAKILAKI.Text = .UMURLAKILAKI
                txtUMURPEREMPUAN.Text = .UMURPEREMPUAN
                chkRIWAYATKONTRASEPSI_1.Checked = .RIWAYATKONTRASEPSI_1
                chkRIWAYATKONTRASEPSI_2.Checked = .RIWAYATKONTRASEPSI_2
                chkRIWAYATKONTRASEPSI_3.Checked = .RIWAYATKONTRASEPSI_3
                chkRIWAYATKONTRASEPSI_4.Checked = .RIWAYATKONTRASEPSI_4
                chkRIWAYATKONTRASEPSI_5.Checked = .RIWAYATKONTRASEPSI_5
                chkRIWAYATPENYAKITSEKARANG_1.Checked = .RIWAYATPENYAKITSEKARANG_1
                chkRIWAYATPENYAKITSEKARANG_2.Checked = .RIWAYATPENYAKITSEKARANG_2
                chkRIWAYATPENYAKITSEKARANG_3.Checked = .RIWAYATPENYAKITSEKARANG_3
                chkRIWAYATPENYAKITSEKARANG_4.Checked = .RIWAYATPENYAKITSEKARANG_4
                chkRIWAYATPENYAKITSEKARANG_5.Checked = .RIWAYATPENYAKITSEKARANG_5
                chkRIWAYATPENYAKITSEKARANG_6.Checked = .RIWAYATPENYAKITSEKARANG_6
                chkRIWAYATPENYAKITDAHULU_1.Checked = .RIWAYATPENYAKITDAHULU_1
                chkRIWAYATPENYAKITDAHULU_2.Checked = .RIWAYATPENYAKITDAHULU_2
                chkRIWAYATPENYAKITDAHULU_3.Checked = .RIWAYATPENYAKITDAHULU_3
                chkRIWAYATPENYAKITDAHULU_4.Checked = .RIWAYATPENYAKITDAHULU_4
                chkRIWAYATPENYAKITDAHULU_5.Checked = .RIWAYATPENYAKITDAHULU_5
                chkRIWAYATPENYAKITKELUARGA_1.Checked = .RIWAYATPENYAKITKELUARGA_1
                chkRIWAYATPENYAKITKELUARGA_2.Checked = .RIWAYATPENYAKITKELUARGA_2
                chkRIWAYATPENYAKITKELUARGA_3.Checked = .RIWAYATPENYAKITKELUARGA_3
                chkRIWAYATPENYAKITKELUARGA_4.Checked = .RIWAYATPENYAKITKELUARGA_4
                chkRIWAYATPENYAKITKELUARGA_5.Checked = .RIWAYATPENYAKITKELUARGA_5
                chkRIWAYATGYNECOLOG_1.Checked = .RIWAYATGYNECOLOG_1
                chkRIWAYATGYNECOLOG_2.Checked = .RIWAYATGYNECOLOG_2
                chkRIWAYATGYNECOLOG_3.Checked = .RIWAYATGYNECOLOG_3
                chkRIWAYATGYNECOLOG_4.Checked = .RIWAYATGYNECOLOG_4
                chkRIWAYATGYNECOLOG_5.Checked = .RIWAYATGYNECOLOG_5
                chkRIWAYATGYNECOLOG_6.Checked = .RIWAYATGYNECOLOG_6
                chkRIWAYATGYNECOLOG_7.Checked = .RIWAYATGYNECOLOG_7
                chkRIWAYATGYNECOLOG_8.Checked = .RIWAYATGYNECOLOG_8
                chkRIWAYATGYNECOLOG_9.Checked = .RIWAYATGYNECOLOG_9
                chkSKALANYERI_00.Checked = .SKALANEYRI_00
                chkSKALANYERI_01.Checked = .SKALANEYRI_01
                chkSKALANYERI_02.Checked = .SKALANEYRI_02
                chkSKALANYERI_03.Checked = .SKALANEYRI_03
                chkSKALANYERI_04.Checked = .SKALANEYRI_04
                chkSKALANYERI_05.Checked = .SKALANEYRI_05
                chkSKALANYERI_06.Checked = .SKALANEYRI_06
                chkSKALANYERI_07.Checked = .SKALANEYRI_07
                chkSKALANYERI_08.Checked = .SKALANEYRI_08
                chkSKALANYERI_09.Checked = .SKALANEYRI_09
                chkSKALANYERI_10.Checked = .SKALANEYRI_10
                txtKESADARAN_TEXT_1.Text = .KESADARAN_TEXT_1
                txtKESADARAN_TEXT_2.Text = .KESADARAN_TEXT_2
                txtKESADARAN_TEXT_3.Text = .KESADARAN_TEXT_3
                txtKESADARAN_TEXT_4.Text = .KESADARAN_TEXT_4
                txtKESADARAN_TEXT_5.Text = .KESADARAN_TEXT_5
                txtKEADAANUMUM.Text = .KEADAANUMUM
                chkKESADARAN_1.Checked = .KESADARAN_1
                chkKESADARAN_2.Checked = .KESADARAN_2
                chkKESADARAN_3.Checked = .KESADARAN_3
                chkKESADARAN_4.Checked = .KESADARAN_4
                chkKEADAANEMOSIONAL_1.Checked = .KEADAANEMOSIONAL_1
                chkKEADAANEMOSIONAL_2.Checked = .KEADAANEMOSIONAL_2
                txtKEADAANEMOSIONAL_TEXT_1.Text = .KEADAANEMOSIONAL_TEXT_1
                txtKEADAANEMOSIONAL_TEXT_2.Text = .KEADAANEMOSIONAL_TEXT_2
                txtKEADAANEMOSIONAL_TEXT_3.Text = .KEADAANEMOSIONAL_TEXT_3
                chkPEMERIKSAANFISIKMUKA_1.Checked = .PEMERIKSAANFISIK_MUKA_1
                chkPEMERIKSAANFISIKMUKA_2.Checked = .PEMERIKSAANFISIK_MUKA_2
                chkPEMERIKSAANFISIKMATA_1.Checked = .PEMERIKSAANFISIK_MATA_1
                chkPEMERIKSAANFISIKMATA_2.Checked = .PEMERIKSAANFISIK_MATA_2
                chkPEMERIKSAANFISIKMATA_3.Checked = .PEMERIKSAANFISIK_MATA_3
                chkPEMERIKSAANFISIKMATA_4.Checked = .PEMERIKSAANFISIK_MATA_4
                chkPEMERIKSAANFISIKLEHER_1.Checked = .PEMERIKSAANFISIK_LEHER_1
                chkPEMERIKSAANFISIKLEHER_2.Checked = .PEMERIKSAANFISIK_LEHER_2
                chkPEMERIKSAANFISIKLEHER_3.Checked = .PEMERIKSAANFISIK_LEHER_3
                chkPEMERIKSAANFISIKLEHER_4.Checked = .PEMERIKSAANFISIK_LEHER_4
                chkPEMERIKSAANFISIKLEHER_5.Checked = .PEMERIKSAANFISIK_LEHER_5
                chkPEMERIKSAANFISIKLEHER_6.Checked = .PEMERIKSAANFISIK_LEHER_6
                chkPEMERIKSAANFISIKDADA_1.Checked = .PEMERIKSAANFISIK_DADA_1
                chkPEMERIKSAANFISIKDADA_2.Checked = .PEMERIKSAANFISIK_DADA_2
                chkPEMERIKSAANFISIKDADA_3.Checked = .PEMERIKSAANFISIK_DADA_3
                chkPEMERIKSAANFISIKDADA_4.Checked = .PEMERIKSAANFISIK_DADA_4
                chkPEMERIKSAANFISIKDADA_5.Checked = .PEMERIKSAANFISIK_DADA_5
                chkPEMERIKSAANFISIKDADA_6.Checked = .PEMERIKSAANFISIK_DADA_6
                chkPEMERIKSAANFISIKDADA_7.Checked = .PEMERIKSAANFISIK_DADA_7
                chkPEMERIKSAANFISIKDADA_8.Checked = .PEMERIKSAANFISIK_DADA_8
                chkPEMERIKSAANFISIKDADA_9.Checked = .PEMERIKSAANFISIK_DADA_9
                chkPEMERIKSAANFISIKDADA_10.Checked = .PEMERIKSAANFISIK_DADA_10
                chkPEMERIKSAANFISIK_OEDEM_1.Checked = .PEMERIKSAANFISIK_OEDEM_1
                chkPEMERIKSAANFISIK_OEDEM_2.Checked = .PEMERIKSAANFISIK_OEDEM_2
                chkPEMERIKSAANFISIK_VARICES_1.Checked = .PEMERIKSAANFISIK_VARICECS_1
                chkPEMERIKSAANFISIK_VARICES_2.Checked = .PEMERIKSAANFISIK_VARICECS_2
                chkPEMERIKSAANFISIK_KEKUATANOTOT_1.Checked = .PEMERIKSAANFISIK_KEKUATANOTOT_1
                chkPEMERIKSAANFISIK_KEKUATANOTOT_2.Checked = .PEMERIKSAANFISIK_KEKUATANOTOT_2
                chkPEMERIKSAANFISIK_REFLEX_1.Checked = .PEMERIKSAANFISIK_REFLEX_1
                chkPEMERIKSAANFISIK_REFLEX_2.Checked = .PEMERIKSAANFISIK_REFLEX_2
                chkPEMERIKSAANFISIK_KEMERAHAN_1.Checked = .PEMERIKSAANFISIK_KEMERAHAN_1
                chkPEMERIKSAANFISIK_KEMERAHAN_2.Checked = .PEMERIKSAANFISIK_KEMERAHAN_2
                chkPEMERIKSAANFISIK_BEKASLUKAOPERASI_1.Checked = .PEMERIKSAANFISIK_BEKASLUKAOPERASI_1
                chkPEMERIKSAANFISIK_BEKASLUKAOPERASI_2.Checked = .PEMERIKSAANFISIK_BEKASLUKAOPERASI_2
                txtPEMERIKSAANFISIK_TFU_TEXT.Text = .PEMERIKSAANFISIK_TFU_TEXT
                txtPEMERIKSAANFISIK_LEOPOLD_1_TEXT.Text = .PEMERIKSAANFISIK_LEOPOLD_1_TEXT
                txtPEMERIKSAANFISIK_LEOPOLD_2_TEXT.Text = .PEMERIKSAANFISIK_LEOPOLD_2_TEXT
                txtPEMERIKSAANFISIK_LEOPOLD_3_TEXT.Text = .PEMERIKSAANFISIK_LEOPOLD_3_TEXT
                txtPEMERIKSAANFISIK_LEOPOLD_4_TEXT.Text = .PEMERIKSAANFISIK_LEOPOLD_4_TEXT
                txtPEMERIKSAANFISIK_LEOPOLD_5_TEXT.Text = .PEMERIKSAANFISIK_LEOPOLD_5_TEXT
                txtPEMERIKSAANFISIK_LEOPOLD_6_TEXT.Text = .PEMERIKSAANFISIK_LEOPOLD_6_TEXT
                txtPEMERIKSAANFISIK_LEOPOLD_7_TEXT.Text = .PEMERIKSAANFISIK_LEOPOLD_7_TEXT
                txtPEMERIKSAANFISIK_LEOPOLD_8_TEXT.Text = .PEMERIKSAANFISIK_LEOPOLD_8_TEXT
                txtPEMERIKSAANFISIK_TBBJ_TEXT.Text = .PEMERIKSAANFISIK_TBBJ_TEXT
                chkPEMERIKSAANFISIK_INTESITAS_1.Checked = .PEMERIKSAANFISIK_INTERSITAS_1
                chkPEMERIKSAANFISIK_INTESITAS_2.Checked = .PEMERIKSAANFISIK_INTERSITAS_2
                chkGINECOLOGY_1.Checked = .GYNECOLOGI_1
                chkGYNECOLOGI_1_1.Checked = .GYNECOLOGI_1_1
                chkGINECOLOGY_2.Checked = .GYNECOLOGI_2
                chkGINECOLOGY_3.Checked = .GYNECOLOGI_3
                chkGINECOLOGY_4.Checked = .GYNECOLOGI_4
                chkGINECOLOGY_5.Checked = .GYNECOLOGI_5
                chkGINECOLOGY_6.Checked = .GYNECOLOGI_6
                chkGINECOLOGY_7.Checked = .GYNECOLOGI_7
                chkGINECOLOGY_8.Checked = .GYNECOLOGI_8
                chkGINECOLOGY_9.Checked = .GYNECOLOGI_9
                txtGINECOLOGY_10_TEXT.Text = .GYNECOLOGI_10_TEXT
                txtNIFAS_1_TEXT.Text = .NIFAS_1_TEXT
                chkNIFAS_2.Checked = .NIFAS_2
                chkNIFAS_3.Checked = .NIFAS_3
                chkNIFAS_4.Checked = .NIFAS_4
                chkNIFAS_5.Checked = .NIFAS_5
                chkNIFAS_6.Checked = .NIFAS_6
                chkNIFAS_7.Checked = .NIFAS_7
                chkNIFAS_8.Checked = .NIFAS_8
                chkNIFAS_9.Checked = .NIFAS_9
                chkNIFAS_10.Checked = .NIFAS_10
                txtNIFAS_11_TEXT.Text = .NIFAS_11_TEXT
                txtNIFAS_12_TEXT.Text = .NIFAS_12_TEXT
                txtPEMERIKSAANDALAM_1_TEXT.Text = .PEMERIKSAANDALAM_1_TEXT
                txtPEMERIKSAANDALAM_2_TEXT.Text = .PEMERIKSAANDALAM_2_TEXT
                txtPEMERIKSAANDALAM_3_TEXT.Text = .PEMERIKSAANDALAM_3_TEXT
                txtPEMERIKSAANDALAM_4_TEXT.Text = .PEMERIKSAANDALAM_4_TEXT
                txtPEMERIKSAANDALAM_5_TEXT.Text = .PEMERIKSAANDALAM_5_TEXT
                txtPEMERIKSAANDALAM_6_TEXT.Text = .PEMERIKSAANDALAM_6_TEXT
                'chkPEMERIKSAANDALAM_6_1.Checked = .PEMERIKSAANDALAM_6_1
                'chkPEMERIKSAANDALAM_6_2.Checked = .PEMERIKSAANDALAM_6_2
                txtPEMERIKSAANPENUNJANG_1_TEXT.Text = .PEMERIKSAANPENUNJANG_1_TEXT
                txtPEMERIKSAANPENUNJANG_2_TEXT.Text = .PEMERIKSAANPENUNJANG_2_TEXT
                txtPEMERIKSAANPENUNJANG_3_TEXT.Text = .PEMERIKSAANPENUNJANG_3_TEXT
                txtPEMERIKSAANPENUNJANG_4_TEXT.Text = .PEMERIKSAANPENUNJANG_4_TEXT
                txtPEMERIKSAANPENUNJANG_5_TEXT.Text = .PEMERIKSAANPENUNJANG_5_TEXT
                txtPEMERIKSAANPENUNJANG_6_TEXT.Text = .PEMERIKSAANPENUNJANG_6_TEXT
                txtANALISADATA_TEXT.Text = .ANALISADATA_TEXT
                txtPENATALAKSANAAN_TEXT.Text = .PENATALKSANAAN_TEXT
                chkRIWAYATIMUNISASI_1.Checked = .RIWAYATIMUNISASI_1
                chkRIWAYATIMUNISASI_2.Checked = .RIWAYATIMUNISASI_2
                chkRIWAYATPERKAWINAN_1.Checked = .RIWAYATIPERKAWINAN_1
                chkRIWAYATPERKAWINAN_2.Checked = .RIWAYATIPERKAWINAN_2
                txtRELOAD.Text = .KDUSER_SIGNATURE
                'Try
                '    Dim img = (From x In oS_DIGITAL_IGD_03.GetData
                '               Where x.KDREG = txtNoRegister.Text
                '               Select x.ATTACHMENT_2).Single

                '    picGAMBAR.Image = ByteArrayToImage(img.ToArray())
                'Catch oErr As Exception
                '    'MsgBox("Load Image : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
                'End Try

                TextEdit1.Text = .TextEdit1
                TextEdit2.Text = .TextEdit2
                TextEdit3.Text = .TextEdit3
                '.TextEdit4 = ""
                '.TextEdit5 = ""
                chkSOSIAL_01_1.Checked = .chkSOSIAL_01_1
                chkSOSIAL_01_2.Checked = .chkSOSIAL_01_2
                chkSOSIAL_02_1.Checked = .chkSOSIAL_02_1
                chkSOSIAL_02_2.Checked = .chkSOSIAL_02_2
                chkSOSIAL_02_3.Checked = .chkSOSIAL_02_3
                chkSOSIAL_02_4.Checked = .chkSOSIAL_02_4


                txtSOSIAL_03_1_TEXT.Text = .txtSOSIAL_03_1_TEXT
                txtSOSIAL_03_2_TEXT.Text = .txtSOSIAL_03_2_TEXT
                txtSOSIAL_03_3_TEXT.Text = .txtSOSIAL_03_3_TEXT
                txtSTATUS_SPIRITUAL_01_TEXT.Text = .txtSTATUS_SPIRITUAL_01_TEXT
                txtSTATUS_SPIRITUAL_02_TEXT.Text = .txtSTATUS_SPIRITUAL_02_TEXT

                txtMENGETAHUI.Text = .MENGETAHUI
                BindingSource.DataSource = oS_DIGITAL_IGD_03.GetDataDetail.Where(Function(x) x.KDPENDAFTARAN = txtNoRegister.Text).OrderBy(Function(x) x.SEQ).ToList()
                grdDetail.DataSource = BindingSource
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
            If txtNoRegister.Text = String.Empty Then
                MsgBox("Dibutuhkan Register", MsgBoxStyle.Exclamation, Me.Text)
                txtNoRegister.Focus()
                fn_Validate = False
                Exit Function
            End If
        Catch oErr As Exception
            MsgBox("Validate Data : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Function
    Private Function fn_Save() As Boolean
        Try
            ' ***** HEADER *****
            Dim ds = oS_DIGITAL_IGD_03.GetStructureHeader
            With ds
                .KDPENDAFTARAN = txtNoRegister.Text
                .DATE = deDATE.DateTime

                Try
                    .DATECREATED = oS_DIGITAL_IGD_03.GetData(txtNoRegister.Text).DATECREATED
                Catch ex As Exception
                    .DATECREATED = Now
                End Try
                .DATEUPDATED = Now
                .KENDARAAN_1 = chkKENDARAAN_1.Checked
                .KENDARAAN_2 = chkKENDARAAN_2.Checked
                .KENDARAAN_3 = chkKENDARAAN_3.Checked
                .KENDARAAN_KETERANGAN = txtKENDARAAN_KETERANGAN.Text
                .ASALPASIEN_1 = chkASALPASIEN_1.Checked
                .ASALPASIEN_2 = chkASALPASIEN_2.Checked
                .ASALPASIEN_KETERANGAN = txtASALPASIEN_KETERANGAN.Text
                .WAWANCARA_1 = chkWAWANCARA_1.Checked
                .WAWANCARA_2 = chkWAWANCARA_2.Checked
                .WAWANCARA_KETERANGAN = txtWAWANCARA_KETERANGAN.Text
                .ALERGI_1 = chkALERGI_1.Checked
                .ALERGI_2 = chkALERGI_2.Checked
                .ALERGI_KETERANGAN = txtALERGI_KETERANGAN.Text
                .TRIAGE_1 = chkTRIAGE_1.Checked
                .TRIAGE_2 = chkTRIAGE_2.Checked
                .TRIAGE_3 = chkTRIAGE_3.Checked
                .TRIAGE_4 = chkTRIAGE_4.Checked
                .TRIAGE_AIRWAY = txtAIRWAY.Text
                .TRIAGE_BREATHING = txtBREATHING.Text
                .TRIAGE_CIRCULATION = txtCISRCULATION.Text
                .KELUHANUTAMA = txtKELUHANUTAMA.Text
                .RIWAYATMENSTRUASI_TEXT_1 = txtRIWAYATMENSTRUASI_TEXT_1.Text
                .RIWAYATMENSTRUASI_TEXT_2 = txtRIWAYATMENSTRUASI_TEXT_2.Text
                .RIWAYATMENSTRUASI_TEXT_3 = txtRIWAYATMENSTRUASI_TEXT_3.Text
                .RIWAYATMENSTRUASI_TEXT_4 = txtRIWAYATMENSTRUASI_TEXT_4.Text
                .RIWAYATMENSTRUASI_TEXT_5 = txtRIWAYATMENSTRUASI_TEXT_5.Text
                .RIWAYATMENSTRUASI_6_1 = chkRIWAYATMENSTRUASI_6_1.Checked
                .RIWAYATMENSTRUASI_6_2 = chkRIWAYATMENSTRUASI_6_2.Checked
                .RIWAYATMENSTRUASI_TEXT_7 = txtRIWAYATMENSTRUASI_TEXT_7.Text
                .RIWAYATMENSTRUASI_8_1 = chkRIWAYATMENSTRUASI_8_1.Checked
                .RIWAYATMENSTRUASI_8_2 = chkRIWAYATMENSTRUASI_8_2.Checked
                .PERGERAKANJANIN = txtPERGERAKANJANIN.Text
                .PERKAWINANKE_TEXT = txtPERKAWINANKE_TEXT.Text
                .PERKAWINANKE_LAMANYA_TEXT = txtPERKAWINANKE_LAMANYA_TEXT.Text
                .UMURLAKILAKI = txtUMURLAKILAKI.Text
                .UMURPEREMPUAN = txtUMURPEREMPUAN.Text
                .RIWAYATKONTRASEPSI_1 = chkRIWAYATKONTRASEPSI_1.Checked
                .RIWAYATKONTRASEPSI_2 = chkRIWAYATKONTRASEPSI_2.Checked
                .RIWAYATKONTRASEPSI_3 = chkRIWAYATKONTRASEPSI_3.Checked
                .RIWAYATKONTRASEPSI_4 = chkRIWAYATKONTRASEPSI_4.Checked
                .RIWAYATKONTRASEPSI_5 = chkRIWAYATKONTRASEPSI_5.Checked
                .RIWAYATPENYAKITSEKARANG_1 = chkRIWAYATPENYAKITSEKARANG_1.Checked
                .RIWAYATPENYAKITSEKARANG_2 = chkRIWAYATPENYAKITSEKARANG_2.Checked
                .RIWAYATPENYAKITSEKARANG_3 = chkRIWAYATPENYAKITSEKARANG_3.Checked
                .RIWAYATPENYAKITSEKARANG_4 = chkRIWAYATPENYAKITSEKARANG_4.Checked
                .RIWAYATPENYAKITSEKARANG_5 = chkRIWAYATPENYAKITSEKARANG_5.Checked
                .RIWAYATPENYAKITSEKARANG_6 = chkRIWAYATPENYAKITSEKARANG_6.Checked
                .RIWAYATPENYAKITDAHULU_1 = chkRIWAYATPENYAKITDAHULU_1.Checked
                .RIWAYATPENYAKITDAHULU_2 = chkRIWAYATPENYAKITDAHULU_2.Checked
                .RIWAYATPENYAKITDAHULU_3 = chkRIWAYATPENYAKITDAHULU_3.Checked
                .RIWAYATPENYAKITDAHULU_4 = chkRIWAYATPENYAKITDAHULU_4.Checked
                .RIWAYATPENYAKITDAHULU_5 = chkRIWAYATPENYAKITDAHULU_5.Checked
                .RIWAYATPENYAKITKELUARGA_1 = chkRIWAYATPENYAKITKELUARGA_1.Checked
                .RIWAYATPENYAKITKELUARGA_2 = chkRIWAYATPENYAKITKELUARGA_2.Checked
                .RIWAYATPENYAKITKELUARGA_3 = chkRIWAYATPENYAKITKELUARGA_3.Checked
                .RIWAYATPENYAKITKELUARGA_4 = chkRIWAYATPENYAKITKELUARGA_4.Checked
                .RIWAYATPENYAKITKELUARGA_5 = chkRIWAYATPENYAKITKELUARGA_5.Checked
                .RIWAYATGYNECOLOG_1 = chkRIWAYATGYNECOLOG_1.Checked
                .RIWAYATGYNECOLOG_2 = chkRIWAYATGYNECOLOG_2.Checked
                .RIWAYATGYNECOLOG_3 = chkRIWAYATGYNECOLOG_3.Checked
                .RIWAYATGYNECOLOG_4 = chkRIWAYATGYNECOLOG_4.Checked
                .RIWAYATGYNECOLOG_5 = chkRIWAYATGYNECOLOG_5.Checked
                .RIWAYATGYNECOLOG_6 = chkRIWAYATGYNECOLOG_6.Checked
                .RIWAYATGYNECOLOG_7 = chkRIWAYATGYNECOLOG_7.Checked
                .RIWAYATGYNECOLOG_8 = chkRIWAYATGYNECOLOG_8.Checked
                .RIWAYATGYNECOLOG_9 = chkRIWAYATGYNECOLOG_9.Checked
                .SKALANEYRI_00 = chkSKALANYERI_00.Checked
                .SKALANEYRI_01 = chkSKALANYERI_01.Checked
                .SKALANEYRI_02 = chkSKALANYERI_02.Checked
                .SKALANEYRI_03 = chkSKALANYERI_03.Checked
                .SKALANEYRI_04 = chkSKALANYERI_04.Checked
                .SKALANEYRI_05 = chkSKALANYERI_05.Checked
                .SKALANEYRI_06 = chkSKALANYERI_06.Checked
                .SKALANEYRI_07 = chkSKALANYERI_07.Checked
                .SKALANEYRI_08 = chkSKALANYERI_08.Checked
                .SKALANEYRI_09 = chkSKALANYERI_09.Checked
                .SKALANEYRI_10 = chkSKALANYERI_10.Checked
                .KEADAANUMUM = txtKEADAANUMUM.Text
                .KESADARAN_TEXT_1 = txtKESADARAN_TEXT_1.Text
                .KESADARAN_TEXT_2 = txtKESADARAN_TEXT_2.Text
                .KESADARAN_TEXT_3 = txtKESADARAN_TEXT_3.Text
                .KESADARAN_TEXT_4 = txtKESADARAN_TEXT_4.Text
                .KESADARAN_TEXT_5 = txtKESADARAN_TEXT_5.Text
                .KESADARAN_1 = chkKESADARAN_1.Checked
                .KESADARAN_2 = chkKESADARAN_2.Checked
                .KESADARAN_3 = chkKESADARAN_3.Checked
                .KESADARAN_4 = chkKESADARAN_4.Checked
                .KEADAANEMOSIONAL_1 = chkKEADAANEMOSIONAL_1.Checked
                .KEADAANEMOSIONAL_2 = chkKEADAANEMOSIONAL_2.Checked
                .KEADAANEMOSIONAL_TEXT_1 = txtKEADAANEMOSIONAL_TEXT_1.Text
                .KEADAANEMOSIONAL_TEXT_2 = txtKEADAANEMOSIONAL_TEXT_2.Text
                .KEADAANEMOSIONAL_TEXT_3 = txtKEADAANEMOSIONAL_TEXT_3.Text
                .PEMERIKSAANFISIK_MUKA_1 = chkPEMERIKSAANFISIKMUKA_1.Checked
                .PEMERIKSAANFISIK_MUKA_2 = chkPEMERIKSAANFISIKMUKA_2.Checked
                .PEMERIKSAANFISIK_MATA_1 = chkPEMERIKSAANFISIKMATA_1.Checked
                .PEMERIKSAANFISIK_MATA_2 = chkPEMERIKSAANFISIKMATA_2.Checked
                .PEMERIKSAANFISIK_MATA_3 = chkPEMERIKSAANFISIKMATA_3.Checked
                .PEMERIKSAANFISIK_MATA_4 = chkPEMERIKSAANFISIKMATA_4.Checked
                .PEMERIKSAANFISIK_LEHER_1 = chkPEMERIKSAANFISIKLEHER_1.Checked
                .PEMERIKSAANFISIK_LEHER_2 = chkPEMERIKSAANFISIKLEHER_2.Checked
                .PEMERIKSAANFISIK_LEHER_3 = chkPEMERIKSAANFISIKLEHER_3.Checked
                .PEMERIKSAANFISIK_LEHER_4 = chkPEMERIKSAANFISIKLEHER_4.Checked
                .PEMERIKSAANFISIK_LEHER_5 = chkPEMERIKSAANFISIKLEHER_5.Checked
                .PEMERIKSAANFISIK_LEHER_6 = chkPEMERIKSAANFISIKLEHER_6.Checked
                .PEMERIKSAANFISIK_DADA_1 = chkPEMERIKSAANFISIKDADA_1.Checked
                .PEMERIKSAANFISIK_DADA_2 = chkPEMERIKSAANFISIKDADA_2.Checked
                .PEMERIKSAANFISIK_DADA_3 = chkPEMERIKSAANFISIKDADA_3.Checked
                .PEMERIKSAANFISIK_DADA_4 = chkPEMERIKSAANFISIKDADA_4.Checked
                .PEMERIKSAANFISIK_DADA_5 = chkPEMERIKSAANFISIKDADA_5.Checked
                .PEMERIKSAANFISIK_DADA_6 = chkPEMERIKSAANFISIKDADA_6.Checked
                .PEMERIKSAANFISIK_DADA_7 = chkPEMERIKSAANFISIKDADA_7.Checked
                .PEMERIKSAANFISIK_DADA_8 = chkPEMERIKSAANFISIKDADA_8.Checked
                .PEMERIKSAANFISIK_DADA_9 = chkPEMERIKSAANFISIKDADA_9.Checked
                .PEMERIKSAANFISIK_DADA_10 = chkPEMERIKSAANFISIKDADA_10.Checked
                .PEMERIKSAANFISIK_OEDEM_1 = chkPEMERIKSAANFISIK_OEDEM_1.Checked
                .PEMERIKSAANFISIK_OEDEM_2 = chkPEMERIKSAANFISIK_OEDEM_2.Checked
                .PEMERIKSAANFISIK_VARICECS_1 = chkPEMERIKSAANFISIK_VARICES_1.Checked
                .PEMERIKSAANFISIK_VARICECS_2 = chkPEMERIKSAANFISIK_VARICES_2.Checked
                .PEMERIKSAANFISIK_KEKUATANOTOT_1 = chkPEMERIKSAANFISIK_KEKUATANOTOT_1.Checked
                .PEMERIKSAANFISIK_KEKUATANOTOT_2 = chkPEMERIKSAANFISIK_KEKUATANOTOT_2.Checked
                .PEMERIKSAANFISIK_REFLEX_1 = chkPEMERIKSAANFISIK_REFLEX_1.Checked
                .PEMERIKSAANFISIK_REFLEX_2 = chkPEMERIKSAANFISIK_REFLEX_2.Checked
                .PEMERIKSAANFISIK_KEMERAHAN_1 = chkPEMERIKSAANFISIK_KEMERAHAN_1.Checked
                .PEMERIKSAANFISIK_KEMERAHAN_2 = chkPEMERIKSAANFISIK_KEMERAHAN_2.Checked
                .PEMERIKSAANFISIK_BEKASLUKAOPERASI_1 = chkPEMERIKSAANFISIK_BEKASLUKAOPERASI_1.Checked
                .PEMERIKSAANFISIK_BEKASLUKAOPERASI_2 = chkPEMERIKSAANFISIK_BEKASLUKAOPERASI_2.Checked
                .PEMERIKSAANFISIK_TFU_TEXT = txtPEMERIKSAANFISIK_TFU_TEXT.Text
                .PEMERIKSAANFISIK_LEOPOLD_1_TEXT = txtPEMERIKSAANFISIK_LEOPOLD_1_TEXT.Text
                .PEMERIKSAANFISIK_LEOPOLD_2_TEXT = txtPEMERIKSAANFISIK_LEOPOLD_2_TEXT.Text
                .PEMERIKSAANFISIK_LEOPOLD_3_TEXT = txtPEMERIKSAANFISIK_LEOPOLD_3_TEXT.Text
                .PEMERIKSAANFISIK_LEOPOLD_4_TEXT = txtPEMERIKSAANFISIK_LEOPOLD_4_TEXT.Text
                .PEMERIKSAANFISIK_LEOPOLD_5_TEXT = txtPEMERIKSAANFISIK_LEOPOLD_5_TEXT.Text
                .PEMERIKSAANFISIK_LEOPOLD_6_TEXT = txtPEMERIKSAANFISIK_LEOPOLD_6_TEXT.Text
                .PEMERIKSAANFISIK_LEOPOLD_7_TEXT = txtPEMERIKSAANFISIK_LEOPOLD_7_TEXT.Text
                .PEMERIKSAANFISIK_LEOPOLD_8_TEXT = txtPEMERIKSAANFISIK_LEOPOLD_8_TEXT.Text
                .PEMERIKSAANFISIK_TBBJ_TEXT = txtPEMERIKSAANFISIK_TBBJ_TEXT.Text
                .PEMERIKSAANFISIK_INTERSITAS_1 = chkPEMERIKSAANFISIK_INTESITAS_1.Checked
                .PEMERIKSAANFISIK_INTERSITAS_2 = chkPEMERIKSAANFISIK_INTESITAS_2.Checked
                .GYNECOLOGI_1 = chkGINECOLOGY_1.Checked
                .GYNECOLOGI_1_1 = chkGYNECOLOGI_1_1.Checked
                .GYNECOLOGI_2 = chkGINECOLOGY_2.Checked
                .GYNECOLOGI_3 = chkGINECOLOGY_3.Checked
                .GYNECOLOGI_4 = chkGINECOLOGY_4.Checked
                .GYNECOLOGI_5 = chkGINECOLOGY_5.Checked
                .GYNECOLOGI_6 = chkGINECOLOGY_6.Checked
                .GYNECOLOGI_7 = chkGINECOLOGY_7.Checked
                .GYNECOLOGI_8 = chkGINECOLOGY_8.Checked
                .GYNECOLOGI_9 = chkGINECOLOGY_9.Checked
                .GYNECOLOGI_10_TEXT = txtGINECOLOGY_10_TEXT.Text
                .NIFAS_1_TEXT = txtNIFAS_1_TEXT.Text
                .NIFAS_2 = chkNIFAS_2.Checked
                .NIFAS_3 = chkNIFAS_3.Checked
                .NIFAS_4 = chkNIFAS_4.Checked
                .NIFAS_5 = chkNIFAS_5.Checked
                .NIFAS_6 = chkNIFAS_6.Checked
                .NIFAS_7 = chkNIFAS_7.Checked
                .NIFAS_8 = chkNIFAS_8.Checked
                .NIFAS_9 = chkNIFAS_9.Checked
                .NIFAS_10 = chkNIFAS_10.Checked
                .NIFAS_11_TEXT = txtNIFAS_11_TEXT.Text
                .NIFAS_12_TEXT = txtNIFAS_12_TEXT.Text
                .PEMERIKSAANDALAM_1_TEXT = txtPEMERIKSAANDALAM_1_TEXT.Text
                .PEMERIKSAANDALAM_2_TEXT = txtPEMERIKSAANDALAM_2_TEXT.Text
                .PEMERIKSAANDALAM_3_TEXT = txtPEMERIKSAANDALAM_3_TEXT.Text
                .PEMERIKSAANDALAM_4_TEXT = txtPEMERIKSAANDALAM_4_TEXT.Text
                .PEMERIKSAANDALAM_5_TEXT = txtPEMERIKSAANDALAM_5_TEXT.Text
                .PEMERIKSAANDALAM_6_TEXT = txtPEMERIKSAANDALAM_6_TEXT.Text
                .PEMERIKSAANDALAM_6_1 = False
                .PEMERIKSAANDALAM_6_2 = False
                .PEMERIKSAANPENUNJANG_1_TEXT = txtPEMERIKSAANPENUNJANG_1_TEXT.Text
                .PEMERIKSAANPENUNJANG_2_TEXT = txtPEMERIKSAANPENUNJANG_2_TEXT.Text
                .PEMERIKSAANPENUNJANG_3_TEXT = txtPEMERIKSAANPENUNJANG_3_TEXT.Text
                .PEMERIKSAANPENUNJANG_4_TEXT = txtPEMERIKSAANPENUNJANG_4_TEXT.Text
                .PEMERIKSAANPENUNJANG_5_TEXT = txtPEMERIKSAANPENUNJANG_5_TEXT.Text
                .PEMERIKSAANPENUNJANG_6_TEXT = txtPEMERIKSAANPENUNJANG_6_TEXT.Text
                .ANALISADATA_TEXT = txtANALISADATA_TEXT.Text
                .PENATALKSANAAN_TEXT = txtPENATALAKSANAAN_TEXT.Text
                .RIWAYATIMUNISASI_1 = chkRIWAYATIMUNISASI_1.Checked
                .RIWAYATIMUNISASI_2 = chkRIWAYATIMUNISASI_2.Checked
                .RIWAYATIPERKAWINAN_1 = chkRIWAYATPERKAWINAN_1.Checked
                .RIWAYATIPERKAWINAN_2 = chkRIWAYATPERKAWINAN_2.Checked
                'Try
                '    Dim ms As New IO.MemoryStream()
                '    picGAMBAR2.Image.Save(ms, picGAMBAR2.Image.RawFormat)

                '    Dim data As Byte() = ms.GetBuffer()

                '    .ATTACHMENT_2 = data
                'Catch oErr As Exception

                'End Try

                .MENGKAJI = ""
                .MENGETAHUI = txtMENGETAHUI.Text
                Try
                    .CETAK = oS_DIGITAL_IGD_03.GetData(txtNoRegister.Text).CETAK
                Catch ex As Exception
                    .CETAK = 0
                End Try
                .KDUSER = sUserID
                .KDUSER_SIGNATURE = txtRELOAD.Text

                .TextEdit1 = TextEdit1.Text
                .TextEdit2 = TextEdit2.Text
                .TextEdit3 = TextEdit3.Text
                .TextEdit4 = ""
                .TextEdit5 = ""
                .chkSOSIAL_01_1 = chkSOSIAL_01_1.Checked
                .chkSOSIAL_01_2 = chkSOSIAL_01_2.Checked
                .chkSOSIAL_02_1 = chkSOSIAL_02_1.Checked
                .chkSOSIAL_02_2 = chkSOSIAL_02_2.Checked
                .chkSOSIAL_02_3 = chkSOSIAL_02_3.Checked
                .chkSOSIAL_02_4 = chkSOSIAL_02_4.Checked

                .txtSOSIAL_03_1_TEXT = txtSOSIAL_03_1_TEXT.Text
                .txtSOSIAL_03_2_TEXT = txtSOSIAL_03_2_TEXT.Text
                .txtSOSIAL_03_3_TEXT = txtSOSIAL_03_3_TEXT.Text
                .txtSTATUS_SPIRITUAL_01_TEXT = txtSTATUS_SPIRITUAL_01_TEXT.Text
                .txtSTATUS_SPIRITUAL_02_TEXT = txtSTATUS_SPIRITUAL_02_TEXT.Text
            End With

            ' ***** DETIL *****
            Dim arrDetail = oS_DIGITAL_IGD_03.GetStructureDetailList
            For i As Integer = 0 To grvDetail.RowCount - 2
                Dim dsDetail = oS_DIGITAL_IGD_03.GetStructureDetail
                With dsDetail
                    .SEQ = i
                    .KDPENDAFTARAN = ds.KDPENDAFTARAN
                    .DATE = grvDetail.GetRowCellValue(i, colDATE)
                    .TEMPATPERSALINAN = grvDetail.GetRowCellValue(i, colTEMPATPERSALINAN)
                    .UMURKEHAMILAN = grvDetail.GetRowCellValue(i, colUMURKEHAMILAN)
                    .JENISPERSALINAN = grvDetail.GetRowCellValue(i, colJENISPERSALINAN)
                    .PENOLONG = grvDetail.GetRowCellValue(i, colPENOLONG)
                    .ANAK_JK = grvDetail.GetRowCellValue(i, colANAK_JK)
                    .ANAK_BB = grvDetail.GetRowCellValue(i, colANAK_BB)
                    .ANAK_PB = grvDetail.GetRowCellValue(i, colANAK_PB)
                    .ASIEKSKLUSIF = grvDetail.GetRowCellValue(i, colASIEKSKLUSIF)
                    .KET = grvDetail.GetRowCellValue(i, colKET)
                End With
                arrDetail.Add(dsDetail)
            Next

            If oFormMode = FORM_MODE.FORM_MODE_ADD Then
                Try
                    fn_Save = oS_DIGITAL_IGD_03.InsertData(ds, arrDetail)
                Catch ex As Exception
                    MsgBox("Simpan Data : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            ElseIf oFormMode = FORM_MODE.FORM_MODE_EDIT Then
                Try
                    fn_Save = oS_DIGITAL_IGD_03.UpdateData(ds, arrDetail)
                Catch ex As Exception
                    MsgBox("Simpan Data : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            End If

            If fn_Save = True Then
                If txtRELOAD.Text <> "" Then
                    frmEMedrekIDG_01.fn_LoadUpdateBed(ds.KDPENDAFTARAN, txtRELOAD.Text)
                End If
            End If
        Catch oErr As Exception
            MsgBox("Simpan Data : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            fn_Save = False
        Finally
            sStatusSaveVonekN = fn_Save
        End Try
    End Function
#End Region
#Region "Command Button"
    Private Sub frmItem_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        Select Case e.KeyCode
            Case Keys.F12
                btnClose_Click()
            Case Keys.F3
                If btnSaveClose.Enabled = True Then
                    btnSaveClose_Click()
                End If
            Case Keys.PageUp
	            fn_ScrollPage(True)
            Case Keys.PageDown
	            fn_ScrollPage(False)
        End Select
    End Sub
    Private Sub btnSaveClose_Click() Handles btnSaveClose.ItemClick
        If fn_Validate() = False Then Exit Sub
        If MsgBox("Save " & txtNoRegister.Text.Trim.ToUpper & "?", MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, Me.Text) = MsgBoxResult.No Then Exit Sub
        If fn_Save() = False Then
            MsgBox("Save gagal! Tolong kontak admin anda!", MsgBoxStyle.Exclamation, Me.Text)
        Else
            MsgBox("Save " & txtNoRegister.Text.Trim.ToUpper & " success!", MsgBoxStyle.Information, Me.Text)
            Me.Close()
        End If
    End Sub
    Private Sub btnClose_Click() Handles btnClose.ItemClick
        Me.Close()
    End Sub
    Private Sub btnTambah_Click(sender As Object, e As EventArgs) Handles btnTambah.Click
        Dim frmPopUpRiwayatKehamilan As New frmPopUpRiwayatKehamilan
        frmPopUpRiwayatKehamilan.fn_LoadMe("", "", "", "", "", "", "", "", "", "")
        frmPopUpRiwayatKehamilan.ShowDialog()

        If sFind1 <> String.Empty Then
            grvDetail.Focus()
            grvDetail.AddNewRow()
            grvDetail.SetFocusedRowCellValue(colDATE, sFind10)
            grvDetail.SetFocusedRowCellValue(colTEMPATPERSALINAN, sFind1)
            grvDetail.SetFocusedRowCellValue(colUMURKEHAMILAN, sFind2)
            grvDetail.SetFocusedRowCellValue(colJENISPERSALINAN, sFind3)
            grvDetail.SetFocusedRowCellValue(colPENOLONG, sFind4)
            grvDetail.SetFocusedRowCellValue(colANAK_JK, sFind5)
            grvDetail.SetFocusedRowCellValue(colANAK_BB, sFind6)
            grvDetail.SetFocusedRowCellValue(colANAK_PB, sFind7)
            grvDetail.SetFocusedRowCellValue(colASIEKSKLUSIF, sFind8)
            grvDetail.SetFocusedRowCellValue(colKET, sFind9)

            grvDetail.UpdateCurrentRow()
        End If

        sFind10 = String.Empty
        sFind1 = String.Empty
        sFind2 = String.Empty
        sFind3 = String.Empty
        sFind4 = String.Empty
        sFind5 = String.Empty
        sFind6 = String.Empty
        sFind7 = String.Empty
        sFind8 = String.Empty
        sFind9 = String.Empty

        grdDetail.Focus()
    End Sub

    Private Sub EditToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles EditToolStripMenuItem.Click
        Dim frmPopUpRiwayatKehamilan As New frmPopUpRiwayatKehamilan
        frmPopUpRiwayatKehamilan.fn_LoadMe(grvDetail.GetFocusedRowCellValue(colDATE), grvDetail.GetFocusedRowCellValue(colTEMPATPERSALINAN), grvDetail.GetFocusedRowCellValue(colUMURKEHAMILAN), grvDetail.GetFocusedRowCellValue(colJENISPERSALINAN), grvDetail.GetFocusedRowCellValue(colPENOLONG), grvDetail.GetFocusedRowCellValue(colANAK_JK), grvDetail.GetFocusedRowCellValue(colANAK_BB), grvDetail.GetFocusedRowCellValue(colANAK_PB), grvDetail.GetFocusedRowCellValue(colASIEKSKLUSIF), grvDetail.GetFocusedRowCellValue(colKET))
        frmPopUpRiwayatKehamilan.ShowDialog()

        If sFind1 <> String.Empty Then
            grvDetail.SetFocusedRowCellValue(colDATE, sFind10)
            grvDetail.SetFocusedRowCellValue(colTEMPATPERSALINAN, sFind1)
            grvDetail.SetFocusedRowCellValue(colUMURKEHAMILAN, sFind2)
            grvDetail.SetFocusedRowCellValue(colJENISPERSALINAN, sFind3)
            grvDetail.SetFocusedRowCellValue(colPENOLONG, sFind4)
            grvDetail.SetFocusedRowCellValue(colANAK_JK, sFind5)
            grvDetail.SetFocusedRowCellValue(colANAK_BB, sFind6)
            grvDetail.SetFocusedRowCellValue(colANAK_PB, sFind7)
            grvDetail.SetFocusedRowCellValue(colASIEKSKLUSIF, sFind8)
            grvDetail.SetFocusedRowCellValue(colKET, sFind9)

            grvDetail.UpdateCurrentRow()
        End If

        sFind10 = String.Empty
        sFind1 = String.Empty
        sFind2 = String.Empty
        sFind3 = String.Empty
        sFind4 = String.Empty
        sFind5 = String.Empty
        sFind6 = String.Empty
        sFind7 = String.Empty
        sFind8 = String.Empty
        sFind9 = String.Empty

        grdDetail.Focus()
    End Sub

    Private Sub DeleteToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DeleteToolStripMenuItem.Click
        grvDetail.DeleteSelectedRows()
    End Sub

    Private Sub chkGINECOLOGY_2_CheckedChanged(sender As Object, e As EventArgs) Handles chkGINECOLOGY_2.CheckedChanged
        If chkGINECOLOGY_2.Checked = True Then
            chkGINECOLOGY_3.ReadOnly = False
            chkGINECOLOGY_4.ReadOnly = False
            chkGINECOLOGY_5.ReadOnly = False
        Else
            chkGINECOLOGY_3.ReadOnly = True
            chkGINECOLOGY_4.ReadOnly = True
            chkGINECOLOGY_5.ReadOnly = True
            chkGINECOLOGY_3.Checked = False
            chkGINECOLOGY_4.Checked = False
            chkGINECOLOGY_5.Checked = False
        End If
    End Sub
    Private Sub chkGINECOLOGY_3_CheckedChanged(sender As Object, e As EventArgs) Handles chkGINECOLOGY_3.Click, chkGINECOLOGY_4.Click, chkGINECOLOGY_5.Click
        chkGINECOLOGY_3.Checked = False
        chkGINECOLOGY_4.Checked = False
        chkGINECOLOGY_5.Checked = False
    End Sub
    Private Sub chkGINECOLOGY_6_CheckedChanged(sender As Object, e As EventArgs) Handles chkGINECOLOGY_6.Click, chkGINECOLOGY_7.Click
        chkGINECOLOGY_6.Checked = False
        chkGINECOLOGY_7.Checked = False
    End Sub
#End Region
#Region "Lookup / Event"

#End Region
#Region "Handles"
    Private Sub chkKENDARAAN_Click(sender As Object, e As EventArgs) Handles chkKENDARAAN_1.Click, chkKENDARAAN_2.Click, chkKENDARAAN_3.Click
        chkKENDARAAN_1.Checked = False
        chkKENDARAAN_2.Checked = False
        chkKENDARAAN_3.Checked = False
    End Sub
    Private Sub chkASALPASIEN_Click(sender As Object, e As EventArgs) Handles chkASALPASIEN_1.Click, chkASALPASIEN_2.Click
        chkASALPASIEN_1.Checked = False
        chkASALPASIEN_2.Checked = False
    End Sub
    Private Sub chkWAWANCARA_Click(sender As Object, e As EventArgs) Handles chkWAWANCARA_1.Click, chkWAWANCARA_2.Click
        chkWAWANCARA_1.Checked = False
        chkWAWANCARA_2.Checked = False
    End Sub
    Private Sub chkALERGI_Click(sender As Object, e As EventArgs) Handles chkALERGI_1.Click, chkALERGI_2.Click
        chkALERGI_1.Checked = False
        chkALERGI_2.Checked = False
    End Sub
    Private Sub chkRIWAYATMENSTRUASI_8_Click(sender As Object, e As EventArgs) Handles chkRIWAYATMENSTRUASI_8_1.Click, chkRIWAYATMENSTRUASI_8_2.Click
        chkRIWAYATMENSTRUASI_8_1.Checked = False
        chkRIWAYATMENSTRUASI_8_2.Checked = False
    End Sub
    Private Sub chkRIWAYATIMUNISASI_Click(sender As Object, e As EventArgs) Handles chkRIWAYATIMUNISASI_1.Click, chkRIWAYATIMUNISASI_2.Click
        chkRIWAYATIMUNISASI_1.Checked = False
        chkRIWAYATIMUNISASI_2.Checked = False
    End Sub
    Private Sub chkRIWAYATPERKAWINAN_Click(sender As Object, e As EventArgs) Handles chkRIWAYATPERKAWINAN_1.Click, chkRIWAYATPERKAWINAN_2.Click
        chkRIWAYATPERKAWINAN_1.Checked = False
        chkRIWAYATPERKAWINAN_2.Checked = False
    End Sub
    Private Sub chkSKALANYERI_Click(sender As Object, e As EventArgs) Handles chkSKALANYERI_00.Click, chkSKALANYERI_01.Click, chkSKALANYERI_03.Click, chkSKALANYERI_04.Click, chkSKALANYERI_05.Click, chkSKALANYERI_06.Click, chkSKALANYERI_07.Click, chkSKALANYERI_08.Click, chkSKALANYERI_08.Click, chkSKALANYERI_10.Click
        chkSKALANYERI_00.Checked = False
        chkSKALANYERI_02.Checked = False
        chkSKALANYERI_03.Checked = False
        chkSKALANYERI_04.Checked = False
        chkSKALANYERI_05.Checked = False
        chkSKALANYERI_06.Checked = False
        chkSKALANYERI_07.Checked = False
        chkSKALANYERI_08.Checked = False
        chkSKALANYERI_09.Checked = False
        chkSKALANYERI_10.Checked = False
    End Sub
    Private Sub chkPEMERIKSAANFISIKMUKA_Click(sender As Object, e As EventArgs) Handles chkPEMERIKSAANFISIKMUKA_1.Click, chkPEMERIKSAANFISIKMUKA_1.Click
        chkPEMERIKSAANFISIKMUKA_1.Checked = False
        chkPEMERIKSAANFISIKMUKA_2.Checked = False
    End Sub
    Private Sub chkPEMERIKSAANFISIKMATA_1_Click(sender As Object, e As EventArgs) Handles chkPEMERIKSAANFISIKMATA_1.Click, chkPEMERIKSAANFISIKMATA_2.Click
        chkPEMERIKSAANFISIKMATA_1.Checked = False
        chkPEMERIKSAANFISIKMATA_2.Checked = False
    End Sub
    Private Sub chkPEMERIKSAANFISIKMATA_2_Click(sender As Object, e As EventArgs) Handles chkPEMERIKSAANFISIKMATA_3.Click, chkPEMERIKSAANFISIKMATA_4.Click
        chkPEMERIKSAANFISIKMATA_3.Checked = False
        chkPEMERIKSAANFISIKMATA_4.Checked = False
    End Sub
    Private Sub chkPEMERIKSAANFISIKLEHER_1_Click(sender As Object, e As EventArgs) Handles chkPEMERIKSAANFISIKLEHER_1.Click, chkPEMERIKSAANFISIKLEHER_2.Click
        chkPEMERIKSAANFISIKLEHER_1.Checked = False
        chkPEMERIKSAANFISIKLEHER_2.Checked = False
    End Sub
    Private Sub chkPEMERIKSAANFISIKLEHER_2_Click(sender As Object, e As EventArgs) Handles chkPEMERIKSAANFISIKLEHER_3.Click, chkPEMERIKSAANFISIKLEHER_4.Click
        chkPEMERIKSAANFISIKLEHER_3.Checked = False
        chkPEMERIKSAANFISIKLEHER_4.Checked = False
    End Sub
    Private Sub chkPEMERIKSAANFISIKLEHER_3_Click(sender As Object, e As EventArgs) Handles chkPEMERIKSAANFISIKLEHER_5.Click, chkPEMERIKSAANFISIKLEHER_6.Click
        chkPEMERIKSAANFISIKLEHER_5.Checked = False
        chkPEMERIKSAANFISIKLEHER_6.Checked = False
    End Sub
    Private Sub chkPEMERIKSAANFISIKDADA_1_Click(sender As Object, e As EventArgs) Handles chkPEMERIKSAANFISIKDADA_1.Click, chkPEMERIKSAANFISIKDADA_2.Click
        chkPEMERIKSAANFISIKDADA_1.Checked = False
        chkPEMERIKSAANFISIKDADA_2.Checked = False
    End Sub
    Private Sub chkPEMERIKSAANFISIKDADA_2_Click(sender As Object, e As EventArgs) Handles chkPEMERIKSAANFISIKDADA_3.Click, chkPEMERIKSAANFISIKDADA_4.Click
        chkPEMERIKSAANFISIKDADA_3.Checked = False
        chkPEMERIKSAANFISIKDADA_4.Checked = False
    End Sub
    Private Sub chkPEMERIKSAANFISIKDADA_3_Click(sender As Object, e As EventArgs) Handles chkPEMERIKSAANFISIKDADA_5.Click, chkPEMERIKSAANFISIKDADA_6.Click
        chkPEMERIKSAANFISIKDADA_5.Checked = False
        chkPEMERIKSAANFISIKDADA_6.Checked = False
    End Sub
    Private Sub chkPEMERIKSAANFISIKDADA_4_Click(sender As Object, e As EventArgs) Handles chkPEMERIKSAANFISIKDADA_7.Click, chkPEMERIKSAANFISIKDADA_8.Click
        chkPEMERIKSAANFISIKDADA_7.Checked = False
        chkPEMERIKSAANFISIKDADA_8.Checked = False
    End Sub
    Private Sub chkPEMERIKSAANFISIKDADA_5_Click(sender As Object, e As EventArgs) Handles chkPEMERIKSAANFISIKDADA_9.Click, chkPEMERIKSAANFISIKDADA_10.Click
        chkPEMERIKSAANFISIKDADA_9.Checked = False
        chkPEMERIKSAANFISIKDADA_10.Checked = False
    End Sub
    Private Sub chkPEMERIKSAANFISIK_OEDEM_Click(sender As Object, e As EventArgs) Handles chkPEMERIKSAANFISIK_OEDEM_1.Click, chkPEMERIKSAANFISIK_OEDEM_2.Click
        chkPEMERIKSAANFISIK_OEDEM_1.Checked = False
        chkPEMERIKSAANFISIK_OEDEM_2.Checked = False
    End Sub
    Private Sub chkPEMERIKSAANFISIK_VARICES_Click(sender As Object, e As EventArgs) Handles chkPEMERIKSAANFISIK_VARICES_1.Click, chkPEMERIKSAANFISIK_VARICES_2.Click
        chkPEMERIKSAANFISIK_VARICES_1.Checked = False
        chkPEMERIKSAANFISIK_VARICES_2.Checked = False
    End Sub
    Private Sub chkPEMERIKSAANFISIK_KEKUATANOTOT_Click(sender As Object, e As EventArgs) Handles chkPEMERIKSAANFISIK_KEKUATANOTOT_1.Click, chkPEMERIKSAANFISIK_KEKUATANOTOT_2.Click
        chkPEMERIKSAANFISIK_KEKUATANOTOT_1.Checked = False
        chkPEMERIKSAANFISIK_KEKUATANOTOT_2.Checked = False
    End Sub
    Private Sub chkPEMERIKSAANFISIK_REFLEX_Click(sender As Object, e As EventArgs) Handles chkPEMERIKSAANFISIK_REFLEX_1.Click, chkPEMERIKSAANFISIK_REFLEX_2.Click
        chkPEMERIKSAANFISIK_REFLEX_1.Checked = False
        chkPEMERIKSAANFISIK_REFLEX_2.Checked = False
    End Sub
    Private Sub chkPEMERIKSAANFISIK_KEMERAHAN_Click(sender As Object, e As EventArgs) Handles chkPEMERIKSAANFISIK_KEMERAHAN_1.Click, chkPEMERIKSAANFISIK_KEMERAHAN_2.Click
        chkPEMERIKSAANFISIK_KEMERAHAN_1.Checked = False
        chkPEMERIKSAANFISIK_KEMERAHAN_2.Checked = False
    End Sub
    Private Sub chkPEMERIKSAANFISIK_BEKASLUKAOPERASI_Click(sender As Object, e As EventArgs) Handles chkPEMERIKSAANFISIK_BEKASLUKAOPERASI_1.Click, chkPEMERIKSAANFISIK_BEKASLUKAOPERASI_2.Click
        chkPEMERIKSAANFISIK_BEKASLUKAOPERASI_1.Checked = False
        chkPEMERIKSAANFISIK_BEKASLUKAOPERASI_2.Checked = False
    End Sub

    Private Sub frmEMedrekIDG_03_MouseWheel(sender As Object, e As MouseEventArgs) Handles MyBase.MouseWheel
	    If e.Delta > 0 Then
		    'up
		    fn_ScrollPage(True)
	    Else
		    'down
		    fn_ScrollPage(False)
	    End If
    End Sub

    Private Sub fn_ScrollPage(ByVal isUp As Boolean)
	    Dim myView As Point = Me.Panel1.AutoScrollPosition
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

	    Me.Panel1.AutoScrollPosition = myView
    End Sub

    Private Sub SimpleButton2_Click(sender As Object, e As EventArgs) Handles SimpleButton2.Click
        fn_LoadDataReloadIGD()
        grdReloadIGD.ShowPopup()
    End Sub
    Private Sub fn_LoadDataReloadIGD()
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

            Dim Tanggal As DateTime = deDATE.DateTime.AddDays(-1)

            SQL = "SELECT "
            SQL &= "A.KDAWALASESMENIGD "
            SQL &= ",A.NAMAPASIEN "
            SQL &= ",A.KDUSER "
            SQL &= "FROM "
            SQL &= "S_DIGITAL_IGD_01_AWAL A "
            SQL &= "WHERE CONVERT(VARCHAR(8), A.DATE, 112) >= '" & Tanggal.ToString("yyyyMMdd") & "' "
            'SQL &= "AND A.KDPENDAFTARAN = '' "
            SQL &= "ORDER BY A.DATE DESC "

            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "ALL")

            grdReloadIGD.Properties.DataSource = ds.Tables("ALL")
            grdReloadIGD.Properties.ValueMember = "KDAWALASESMENIGD"
            grdReloadIGD.Properties.DisplayMember = "NAMAPASIEN"

            If oConn.State = ConnectionState.Open Then
                oConn.Close()
            End If
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub grdReloadIGD_EditValueChanged(sender As Object, e As EventArgs) Handles grdReloadIGD.EditValueChanged
        If grdReloadIGD.Text <> "" Then
            Try
                txtRELOAD.Text = grdReloadIGD.EditValue

                Dim oDIGITAL_IGD_01_AWAL As New Transaksi.clsDIGITAL_IGD_01_AWAL
                ' ***** HEADER *****
                Dim ds = oDIGITAL_IGD_01_AWAL.GetData(grdReloadIGD.EditValue)

                With ds
                    'txtPETUGAS_TRIAGE.Text = .KDUSER
                    deDATE.DateTime = .DATE
                    'txtNamaPasien.Text = .NAMAPASIEN
                    txtKELUHANUTAMA.Text = .RIWAYAT
                    'txtRIWAYAT_ALERGI.Text = .ALERGI
                    txtKEADAANUMUM.Text = .ANAMNESIS
                    'cboTINGKATKESADARAN.Text = .TINGKATKESADARAN
                    'txtGCS.Text = .GCS
                    'cboE.Text = .GCS_E
                    'cboM.Text = .GCS_M
                    'cboV.Text = .GCS_V
                    txtKEADAANUMUM.Text = .KEADAANUMUM
                    'txtKESADARAN_TEXT_5.Text = .BERATBADAN
                    txtKESADARAN_TEXT_5.Text = .TINGGIBADAN
                    txtKESADARAN_TEXT_1.Text = .BP
                    txtKESADARAN_TEXT_2.Text = .HR
                    txtKESADARAN_TEXT_3.Text = .RR
                    txtKESADARAN_TEXT_4.Text = .T
                    'txtSPO2.Text = .SP02

                    chkTRIAGE_1.Checked = .TRIAGE_1
                    chkTRIAGE_2.Checked = .TRIAGE_2
                    chkTRIAGE_3.Checked = .TRIAGE_3
                    chkTRIAGE_4.Checked = .TRIAGE_4

                    'txtSAATPULANG_GCS.Text = .GCS
                    'cboSAATPULANG_E.Text = .GCS_E
                    'cboSAATPULANG_M.Text = .GCS_M
                    'cboSAATPULANG_V.Text = .GCS_V

                    'cboSAATPULANG_TINGKATKESDARAN.Text = .KEADAANUMUM
                    'txtSAATPULANG_BP.Text = .BP
                    'txtSAATPULANG_HR.Text = .HR
                    'txtSAATPULANG_T.Text = .T
                    'txtSAATPULANG_RR.Text = .RR
                    'txtSAATPULANG_SPO2.Text = .SP02

                    txtAIRWAY.Text = .TRIAGE_AIRWAY
                    txtBREATHING.Text = .TRIAGE_BREATHING
                    txtCISRCULATION.Text = .TRIAGE_CIRCULATION

                    'BindingSourceTindakanPoli.DataSource = oDIGITAL_IGD_01_AWAL.GetDataDetail(.KDAWALASESMENIGD).OrderBy(Function(x) x.SEQ).ToList()
                    'grdTindakanPoli.DataSource = BindingSourceTindakanPoli

                    'BindingSourcePenunjang.DataSource = oDIGITAL_IGD_01_AWAL.GetDataDetail_P(.KDAWALASESMENIGD).OrderBy(Function(x) x.SEQ).ToList()
                    'grdTindakan.DataSource = BindingSourcePenunjang
                End With
            Catch oErr As Exception
                MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            End Try
        End If
    End Sub
#End Region
End Class