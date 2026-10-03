Imports DataAccess
Imports UI.WIN.MAIN.My.Resources
Imports System.Linq
Imports System.Data.SqlClient

Public Class frmAsesmenAwalKeperawatanNeonatal
#Region "Declaration"
    Private oFormMode As FORM_MODE = FORM_MODE.FORM_MODE_VIEW
    Private isLoad As Boolean = False
    Private oDigital As New Digital.clsS_DIGITAL_ASKEP_NEONATAL
    Private sNoid As String = String.Empty

#End Region
#Region "Function"
    Public Sub LoadMe(ByVal FormMode As Integer, ByVal KDREG As String, ByVal KDCUSTOMER As String, ByVal NAMAPASIEN As String, ByVal JENISKELAMIN As String, ByVal KDUSER_PERAWAT As String, ByVal DPJP As String, Optional ByVal NoId As String = "")
        oFormMode = FormMode
        sNoid = NoId

        txtNoPasien.Text = KDCUSTOMER
        txtNamaPasien.Text = NAMAPASIEN
        txtJenisKelamin.Text = JENISKELAMIN
        txtNoRegister.Text = KDREG
        txtPERAWAT.Text = KDUSER_PERAWAT
        'txtDPJP.Text = DPJP
    End Sub
    Private Sub Form_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'Me.Text = ResumeRawatInap.TITLE
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
        btnDiagnosa.Enabled = Not Status

        chkISAUTO.Properties.ReadOnly = Status
        chkISALLO.Properties.ReadOnly = Status
        txtNAMA.Properties.ReadOnly = Status
        txtTGLMASUKRS.Properties.ReadOnly = Status
        txtWAKTUPEMERIKSAAN.Properties.ReadOnly = Status
        txtNAMAIBU.Properties.ReadOnly = Status
        txtTGLLAHIRIBU.Properties.ReadOnly = Status
        txtBANGSA.Properties.ReadOnly = Status
        chkISDIRAWAT_YA.Properties.ReadOnly = Status
        chkISDIRAWAT_TIDAK.Properties.ReadOnly = Status
        chkISKAWIN_YA.Properties.ReadOnly = Status
        chkISKAWIN_TIDAK.Properties.ReadOnly = Status
        txtAGAMAIBU.Properties.ReadOnly = Status
        txtNORMIBU.Properties.ReadOnly = Status
        txtRUANG.Properties.ReadOnly = Status
        chkISDOKTER.Properties.ReadOnly = Status
        chkBIDAN.Properties.ReadOnly = Status
        'grdDetail.Properties.ReadOnly = Status
        txtTERDAHULU.Properties.ReadOnly = Status
        txtSELAMAKEHAMILAN.Properties.ReadOnly = Status
        chkISALERI_YA.Properties.ReadOnly = Status
        chkISALERI_TIDAK.Properties.ReadOnly = Status
        txtALERGI.Properties.ReadOnly = Status
        txtGOLDAR.Properties.ReadOnly = Status
        txtABO.Properties.ReadOnly = Status
        txtRH.Properties.ReadOnly = Status
        chkSPONTAN.Properties.ReadOnly = Status
        chkINDUKSIDGCARA.Properties.ReadOnly = Status
        chkSC.Properties.ReadOnly = Status
        chkVIE.Properties.ReadOnly = Status
        chkFORSEP.Properties.ReadOnly = Status
        chkINDIKASI.Properties.ReadOnly = Status
        txtINDIKASI_TEXT.Properties.ReadOnly = Status
        chkANALGESIA.Properties.ReadOnly = Status
        chkANASTESIA.Properties.ReadOnly = Status
        chkANTIBIOTIK.Properties.ReadOnly = Status
        chkLAINLAIN.Properties.ReadOnly = Status
        txtLAINLAIN_TEXT.Properties.ReadOnly = Status
        txtKALAI.Properties.ReadOnly = Status
        txtKALAII.Properties.ReadOnly = Status
        txtPECAHKETUBAN.Properties.ReadOnly = Status
        txtSUHUIBU.Properties.ReadOnly = Status
        txtGPA.Properties.ReadOnly = Status
        chkSPOG.Properties.ReadOnly = Status
        chkPEMERIKSAANBIDAN.Properties.ReadOnly = Status
        chkTIDAKADAPEMERIKSAAN.Properties.ReadOnly = Status
        chkPEMERIKSAANTERATUR.Properties.ReadOnly = Status
        chkPEMERIKSAANTIDAKTERATUR.Properties.ReadOnly = Status
        chkISOIMMUNISASI.Properties.ReadOnly = Status
        chkTOXAEMIA.Properties.ReadOnly = Status
        chkHYDROAMINON.Properties.ReadOnly = Status
        chkPENDARAHAN.Properties.ReadOnly = Status
        chkDIABETES.Properties.ReadOnly = Status
        chkINFEKSITRURO.Properties.ReadOnly = Status
        chkLAIN2.Properties.ReadOnly = Status
        txtLAIN2_TEXT.Properties.ReadOnly = Status
        chkFD_ADA.Properties.ReadOnly = Status
        chkFD_TIDAKADA.Properties.ReadOnly = Status
        chkFD_TIDAKDIKETAHUI.Properties.ReadOnly = Status
        chkMECONIUM.Properties.ReadOnly = Status
        chkFD_YA.Properties.ReadOnly = Status
        chkFD_TIDAK.Properties.ReadOnly = Status
        chkFD_YA2.Properties.ReadOnly = Status
        chkFD_TIDAK2.Properties.ReadOnly = Status
        dePARTUS.Properties.ReadOnly = Status
        txtPARTUS_LETAK.Properties.ReadOnly = Status
        chkPARTUS_SPONTAN.Properties.ReadOnly = Status
        chkPARTUS_OPERATIF.Properties.ReadOnly = Status
        txtPLACENTABERAT.Properties.ReadOnly = Status
        txtPLACENTAKEADAAN.Properties.ReadOnly = Status
        chkTALIPUSAT_YA.Properties.ReadOnly = Status
        chkTALIPUSAT_TIDAK.Properties.ReadOnly = Status
        chkTALIPUSAT_3.Properties.ReadOnly = Status
        chkTALIPUSAT_2.Properties.ReadOnly = Status
        chkTALIPUSAT_TIDAKADA.Properties.ReadOnly = Status
        txtTALIPUSAT_KELAINAN.Properties.ReadOnly = Status
        txtTD.Properties.ReadOnly = Status
        txtNADI.Properties.ReadOnly = Status
        txtP.Properties.ReadOnly = Status
        txtSUHU.Properties.ReadOnly = Status
        chkIRAMA_REGULAR.Properties.ReadOnly = Status
        chkIRAMA_IRREGULAR.Properties.ReadOnly = Status
        chkRETRAKSI_TIDAKADA.Properties.ReadOnly = Status
        chkRETRAKSI_ADA.Properties.ReadOnly = Status
        chkBENTUK_NORMAL.Properties.ReadOnly = Status
        chkBENTUK_TIDAKNORMAL.Properties.ReadOnly = Status
        txtBENTUK_TIDAKNORMAL_TEXT.Properties.ReadOnly = Status
        chkPOLANAFAS_NORMAL.Properties.ReadOnly = Status
        chkPOLANAFAS_TIDAKNORMAL.Properties.ReadOnly = Status
        txtPOLANAFAS_TIDAKNORMAL_TEXT.Properties.ReadOnly = Status
        chkSUARANAFAS_NORMAL.Properties.ReadOnly = Status
        chkSUARANAFAS_TIDAKNORMAL.Properties.ReadOnly = Status
        txtSUARANAFAS_TIDAKNORMAL_TEXT.Properties.ReadOnly = Status
        chkPCH_TIDAKADA.Properties.ReadOnly = Status
        chkPCH_ADA.Properties.ReadOnly = Status
        chkSIANOSIS_TIDAKADA.Properties.ReadOnly = Status
        chkSIANOSIS_ADA.Properties.ReadOnly = Status
        chkALATBANTU_SPONTAN.Properties.ReadOnly = Status
        chkKANUL.Properties.ReadOnly = Status
        chkRBMASK.Properties.ReadOnly = Status
        chkNRBMASK.Properties.ReadOnly = Status
        txtO2.Properties.ReadOnly = Status
        chkVENTILATOR.Properties.ReadOnly = Status
        txtVENTILATOR_SETTING.Properties.ReadOnly = Status
        chkSIANOSIS2_TIDAKADA.Properties.ReadOnly = Status
        chkSIANOSIS2_ADA.Properties.ReadOnly = Status
        chkPUCAT_TIDAKADA.Properties.ReadOnly = Status
        chkPUCAT_ADA.Properties.ReadOnly = Status
        chkINTENSITAS_KUAT.Properties.ReadOnly = Status
        chkINTENSITAS_LEMAH.Properties.ReadOnly = Status
        chkIRAMANADI_REGULER.Properties.ReadOnly = Status
        chkIRAMANADI_IRREGULER.Properties.ReadOnly = Status
        chkEDEMA_TIDAKADA.Properties.ReadOnly = Status
        chkEDEMA_ADA.Properties.ReadOnly = Status
        chkAKRAL_HANGAT.Properties.ReadOnly = Status
        chkAKRAL_DINGIN.Properties.ReadOnly = Status
        chkCRT_KURANG.Properties.ReadOnly = Status
        chkCRT_LEBIH.Properties.ReadOnly = Status
        chkCLUBBINGFINGER_TIDAKADA.Properties.ReadOnly = Status
        chkCLUBBINGFINGER_ADA.Properties.ReadOnly = Status
        chkKESADARAN_ALERT.Properties.ReadOnly = Status
        chkKESADARAN_LETARGIS.Properties.ReadOnly = Status
        chkKESADARAN_SEDASI.Properties.ReadOnly = Status
        chkGANGGUAN_TIDAKADA.Properties.ReadOnly = Status
        chkGANGGUAN_ADA.Properties.ReadOnly = Status
        txtGANGGUAN_ADA_TEXT.Properties.ReadOnly = Status
        chkMULUT_MUKOSALEMBAB.Properties.ReadOnly = Status
        chkMULUT_MUKOSAKERING.Properties.ReadOnly = Status
        chkMULUT_LABIO.Properties.ReadOnly = Status
        chkMULUT_PENDARAHANGUSI.Properties.ReadOnly = Status
        chkMULUT_LAINLAIN.Properties.ReadOnly = Status
        chkMUNTAH_YA.Properties.ReadOnly = Status
        chkMUNTAH_TIDAK.Properties.ReadOnly = Status
        chkMUAL_YA.Properties.ReadOnly = Status
        chkMUAL_TIDAK.Properties.ReadOnly = Status
        txtPERISTALTIK_USUS.Properties.ReadOnly = Status
        txtLINGKARPERUT.Properties.ReadOnly = Status
        chkPENGELUARAN_ANUS.Properties.ReadOnly = Status
        chkSTOMA.Properties.ReadOnly = Status
        txtSTOMA_TEXT.Properties.ReadOnly = Status
        txtFREKUENSI.Properties.ReadOnly = Status
        txtKONSISTENSI.Properties.ReadOnly = Status
        chkFESES_NORMAL.Properties.ReadOnly = Status
        chkFESES_CAIR.Properties.ReadOnly = Status
        chkFESES_HIJAU.Properties.ReadOnly = Status
        chkFESES_DEMPUL.Properties.ReadOnly = Status
        chkFESES_BERDARAH.Properties.ReadOnly = Status
        chkFESES_LAINLAIN.Properties.ReadOnly = Status
        txtFESES_LAINLAIN_TEXT.Properties.ReadOnly = Status
        chkURIN_SPONTAN.Properties.ReadOnly = Status
        chkURIN_KATETER.Properties.ReadOnly = Status
        chkURIN_CYSTOSTOMY.Properties.ReadOnly = Status
        chkURIN_KELAINAN_TIDAKADA.Properties.ReadOnly = Status
        chkURIN_KELAINAN_ADA.Properties.ReadOnly = Status
        txtURIN_KELAINAN.Properties.ReadOnly = Status
        txtDIARESES.Properties.ReadOnly = Status
        chkWARNAKULIT_NORMAL.Properties.ReadOnly = Status
        chkWARNAKULIT_PUCAT.Properties.ReadOnly = Status
        chkWARNAKULIT_KUNING.Properties.ReadOnly = Status
        chkWARNAKULIT_MOTTED.Properties.ReadOnly = Status
        chkKELAINANKULIT_TIDAKADA.Properties.ReadOnly = Status
        chkKELAINANKULIT_ADA.Properties.ReadOnly = Status
        chkMUSKULOSKELETAL_TIDAKADA.Properties.ReadOnly = Status
        chkMUSKULOSKELETAL_ADA.Properties.ReadOnly = Status
        txtMUSKULOSKELETAL.Properties.ReadOnly = Status
        chkGERAKAN_BEBAS.Properties.ReadOnly = Status
        chkGERAKAN_TERBATAS.Properties.ReadOnly = Status
        chkGENITALIA_NORMAL.Properties.ReadOnly = Status
        chkGENITALIA_KELAINAN.Properties.ReadOnly = Status
        txtGENITALIA.Properties.ReadOnly = Status
        cboSKOR1.Properties.ReadOnly = Status
        cboSKOR2.Properties.ReadOnly = Status
        cboSKOR3.Properties.ReadOnly = Status
        cboSKOR4.Properties.ReadOnly = Status
        cboSKOR5.Properties.ReadOnly = Status
        cboSKOR6.Properties.ReadOnly = Status
        txtTOTALSKOR.Properties.ReadOnly = Status
        deTGLDITEMUKAN1.Properties.ReadOnly = Status
        deTGLDITEMUKAN2.Properties.ReadOnly = Status
        deTGLDITEMUKAN3.Properties.ReadOnly = Status
        deTGLDITEMUKAN4.Properties.ReadOnly = Status
        deTGLDITEMUKAN5.Properties.ReadOnly = Status
        deTGLDITEMUKAN6.Properties.ReadOnly = Status
        deTGLDITEMUKAN7.Properties.ReadOnly = Status
        deTGLDITEMUKAN8.Properties.ReadOnly = Status
        deTGLDITEMUKAN9.Properties.ReadOnly = Status
        deTGLDITEMUKAN10.Properties.ReadOnly = Status
        deTGLDITEMUKAN11.Properties.ReadOnly = Status
        deTGLDITEMUKAN12.Properties.ReadOnly = Status
        deTGLDITEMUKAN13.Properties.ReadOnly = Status
        deTGLDITEMUKAN14.Properties.ReadOnly = Status
        deTGLDITEMUKAN15.Properties.ReadOnly = Status
        txtLAINLAIN.Properties.ReadOnly = Status
        deTGLTERATASI1.Properties.ReadOnly = Status
        deTGLTERATASI2.Properties.ReadOnly = Status
        deTGLTERATASI3.Properties.ReadOnly = Status
        deTGLTERATASI4.Properties.ReadOnly = Status
        deTGLTERATASI5.Properties.ReadOnly = Status
        deTGLTERATASI6.Properties.ReadOnly = Status
        deTGLTERATASI7.Properties.ReadOnly = Status
        deTGLTERATASI8.Properties.ReadOnly = Status
        deTGLTERATASI9.Properties.ReadOnly = Status
        deTGLTERATASI10.Properties.ReadOnly = Status
        deTGLTERATASI11.Properties.ReadOnly = Status
        deTGLTERATASI12.Properties.ReadOnly = Status
        deTGLTERATASI13.Properties.ReadOnly = Status
        deTGLTERATASI14.Properties.ReadOnly = Status
        deTGLTERATASI15.Properties.ReadOnly = Status
        chkMK_YA1.Properties.ReadOnly = Status
        chkMK_YA2.Properties.ReadOnly = Status
        chkMK_YA3.Properties.ReadOnly = Status
        chkMK_YA4.Properties.ReadOnly = Status
        chkMK_YA5.Properties.ReadOnly = Status
        chkMK_YA6.Properties.ReadOnly = Status
        chkMK_YA7.Properties.ReadOnly = Status
        chkMK_YA8.Properties.ReadOnly = Status
        chkMK_TIDAK1.Properties.ReadOnly = Status
        chkMK_TIDAK2.Properties.ReadOnly = Status
        chkMK_TIDAK3.Properties.ReadOnly = Status
        chkMK_TIDAK4.Properties.ReadOnly = Status
        chkMK_TIDAK5.Properties.ReadOnly = Status
        chkMK_TIDAK6.Properties.ReadOnly = Status
        chkMK_TIDAK7.Properties.ReadOnly = Status
        chkMK_TIDAK8.Properties.ReadOnly = Status
        txtKET1.Properties.ReadOnly = Status
        txtKET2.Properties.ReadOnly = Status
        txtKET3.Properties.ReadOnly = Status
        txtKET4.Properties.ReadOnly = Status
        txtKET5.Properties.ReadOnly = Status
        txtKET6.Properties.ReadOnly = Status
        txtKET7.Properties.ReadOnly = Status
        txtKET8.Properties.ReadOnly = Status
        txtJAM.Properties.ReadOnly = Status
    End Sub
    Private Sub fn_EmptyMe()
        chkISAUTO.Checked = False
        chkISALLO.Checked = False
        txtNAMA.ResetText()
        txtTGLMASUKRS.ResetText()
        txtWAKTUPEMERIKSAAN.ResetText()
        txtNAMAIBU.ResetText()
        txtTGLLAHIRIBU.ResetText()
        txtBANGSA.ResetText()
        chkISDIRAWAT_YA.Checked = False
        chkISDIRAWAT_TIDAK.Checked = False
        chkISKAWIN_YA.Checked = False
        chkISKAWIN_TIDAK.Checked = False
        txtAGAMAIBU.ResetText()
        txtNORMIBU.ResetText()
        txtRUANG.ResetText()
        chkISDOKTER.Checked = False
        chkBIDAN.Checked = False
        txtTERDAHULU.ResetText()
        txtSELAMAKEHAMILAN.ResetText()
        chkISALERI_YA.Checked = False
        chkISALERI_TIDAK.Checked = False
        txtALERGI.ResetText()
        txtGOLDAR.ResetText()
        txtABO.ResetText()
        txtRH.ResetText()
        chkSPONTAN.Checked = False
        chkINDUKSIDGCARA.Checked = False
        chkSC.Checked = False
        chkVIE.Checked = False
        chkFORSEP.Checked = False
        chkINDIKASI.Checked = False
        txtINDIKASI_TEXT.ResetText()
        chkANALGESIA.Checked = False
        chkANASTESIA.Checked = False
        chkANTIBIOTIK.Checked = False
        chkLAINLAIN.Checked = False
        txtLAINLAIN_TEXT.ResetText()
        txtKALAI.ResetText()
        txtKALAII.ResetText()
        txtPECAHKETUBAN.ResetText()
        txtSUHUIBU.ResetText()
        txtGPA.ResetText()
        chkSPOG.Checked = False
        chkPEMERIKSAANBIDAN.Checked = False
        chkTIDAKADAPEMERIKSAAN.Checked = False
        chkPEMERIKSAANTERATUR.Checked = False
        chkPEMERIKSAANTIDAKTERATUR.Checked = False
        chkISOIMMUNISASI.Checked = False
        chkTOXAEMIA.Checked = False
        chkHYDROAMINON.Checked = False
        chkPENDARAHAN.Checked = False
        chkDIABETES.Checked = False
        chkINFEKSITRURO.Checked = False
        chkLAIN2.Checked = False
        txtLAIN2_TEXT.ResetText()
        chkFD_ADA.Checked = False
        chkFD_TIDAKADA.Checked = False
        chkFD_TIDAKDIKETAHUI.Checked = False
        chkMECONIUM.Checked = False
        chkFD_YA.Checked = False
        chkFD_TIDAK.Checked = False
        chkFD_YA2.Checked = False
        chkFD_TIDAK2.Checked = False
        dePARTUS.ResetText()
        txtPARTUS_LETAK.ResetText()
        chkPARTUS_SPONTAN.Checked = False
        chkPARTUS_OPERATIF.Checked = False
        txtPLACENTABERAT.ResetText()
        txtPLACENTAKEADAAN.ResetText()
        chkTALIPUSAT_YA.Checked = False
        chkTALIPUSAT_TIDAK.Checked = False
        chkTALIPUSAT_3.Checked = False
        chkTALIPUSAT_2.Checked = False
        chkTALIPUSAT_TIDAKADA.Checked = False
        txtTALIPUSAT_KELAINAN.ResetText()
        txtTD.ResetText()
        txtNADI.ResetText()
        txtP.ResetText()
        txtSUHU.ResetText()
        chkIRAMA_REGULAR.Checked = False
        chkIRAMA_IRREGULAR.Checked = False
        chkRETRAKSI_TIDAKADA.Checked = False
        chkRETRAKSI_ADA.Checked = False
        chkBENTUK_NORMAL.Checked = False
        chkBENTUK_TIDAKNORMAL.Checked = False
        txtBENTUK_TIDAKNORMAL_TEXT.ResetText()
        chkPOLANAFAS_NORMAL.Checked = False
        chkPOLANAFAS_TIDAKNORMAL.Checked = False
        txtPOLANAFAS_TIDAKNORMAL_TEXT.ResetText()
        chkSUARANAFAS_NORMAL.Checked = False
        chkSUARANAFAS_TIDAKNORMAL.Checked = False
        txtSUARANAFAS_TIDAKNORMAL_TEXT.ResetText()
        chkPCH_TIDAKADA.Checked = False
        chkPCH_ADA.Checked = False
        chkSIANOSIS_TIDAKADA.Checked = False
        chkSIANOSIS_ADA.Checked = False
        chkALATBANTU_SPONTAN.Checked = False
        chkKANUL.Checked = False
        chkRBMASK.Checked = False
        chkNRBMASK.Checked = False
        txtO2.ResetText()
        chkVENTILATOR.Checked = False
        txtVENTILATOR_SETTING.ResetText()
        chkSIANOSIS2_TIDAKADA.Checked = False
        chkSIANOSIS2_ADA.Checked = False
        chkPUCAT_TIDAKADA.Checked = False
        chkPUCAT_ADA.Checked = False
        chkINTENSITAS_KUAT.Checked = False
        chkINTENSITAS_LEMAH.Checked = False
        chkIRAMANADI_REGULER.Checked = False
        chkIRAMANADI_IRREGULER.Checked = False
        chkEDEMA_TIDAKADA.Checked = False
        chkEDEMA_ADA.Checked = False
        chkAKRAL_HANGAT.Checked = False
        chkAKRAL_DINGIN.Checked = False
        chkCRT_KURANG.Checked = False
        chkCRT_LEBIH.Checked = False
        chkCLUBBINGFINGER_TIDAKADA.Checked = False
        chkCLUBBINGFINGER_ADA.Checked = False
        chkKESADARAN_ALERT.Checked = False
        chkKESADARAN_LETARGIS.Checked = False
        chkKESADARAN_SEDASI.Checked = False
        chkGANGGUAN_TIDAKADA.Checked = False
        chkGANGGUAN_ADA.Checked = False
        txtGANGGUAN_ADA_TEXT.ResetText()
        chkMULUT_MUKOSALEMBAB.Checked = False
        chkMULUT_MUKOSAKERING.Checked = False
        chkMULUT_LABIO.Checked = False
        chkMULUT_PENDARAHANGUSI.Checked = False
        chkMULUT_LAINLAIN.Checked = False
        chkMUNTAH_YA.Checked = False
        chkMUNTAH_TIDAK.Checked = False
        chkMUAL_YA.Checked = False
        chkMUAL_TIDAK.Checked = False
        txtPERISTALTIK_USUS.ResetText()
        txtLINGKARPERUT.ResetText()
        chkPENGELUARAN_ANUS.Checked = False
        chkSTOMA.Checked = False
        txtSTOMA_TEXT.ResetText()
        txtFREKUENSI.ResetText()
        txtKONSISTENSI.ResetText()
        chkFESES_NORMAL.Checked = False
        chkFESES_CAIR.Checked = False
        chkFESES_HIJAU.Checked = False
        chkFESES_DEMPUL.Checked = False
        chkFESES_BERDARAH.Checked = False
        chkFESES_LAINLAIN.Checked = False
        txtFESES_LAINLAIN_TEXT.ResetText()
        chkURIN_SPONTAN.Checked = False
        chkURIN_KATETER.Checked = False
        chkURIN_CYSTOSTOMY.Checked = False
        chkURIN_KELAINAN_TIDAKADA.Checked = False
        chkURIN_KELAINAN_ADA.Checked = False
        txtURIN_KELAINAN.ResetText()
        txtDIARESES.ResetText()
        chkWARNAKULIT_NORMAL.Checked = False
        chkWARNAKULIT_PUCAT.Checked = False
        chkWARNAKULIT_KUNING.Checked = False
        chkWARNAKULIT_MOTTED.Checked = False
        chkKELAINANKULIT_TIDAKADA.Checked = False
        chkKELAINANKULIT_ADA.Checked = False
        chkMUSKULOSKELETAL_TIDAKADA.Checked = False
        chkMUSKULOSKELETAL_ADA.Checked = False
        txtMUSKULOSKELETAL.ResetText()
        chkGERAKAN_BEBAS.Checked = False
        chkGERAKAN_TERBATAS.Checked = False
        chkGENITALIA_NORMAL.Checked = False
        chkGENITALIA_KELAINAN.Checked = False
        txtGENITALIA.ResetText()
        cboSKOR1.ResetText()
        cboSKOR2.ResetText()
        cboSKOR3.ResetText()
        cboSKOR4.ResetText()
        cboSKOR5.ResetText()
        cboSKOR6.ResetText()
        txtTOTALSKOR.ResetText()
        deTGLDITEMUKAN1.ResetText()
        deTGLDITEMUKAN2.ResetText()
        deTGLDITEMUKAN3.ResetText()
        deTGLDITEMUKAN4.ResetText()
        deTGLDITEMUKAN5.ResetText()
        deTGLDITEMUKAN6.ResetText()
        deTGLDITEMUKAN7.ResetText()
        deTGLDITEMUKAN8.ResetText()
        deTGLDITEMUKAN9.ResetText()
        deTGLDITEMUKAN10.ResetText()
        deTGLDITEMUKAN11.ResetText()
        deTGLDITEMUKAN12.ResetText()
        deTGLDITEMUKAN13.ResetText()
        deTGLDITEMUKAN14.ResetText()
        deTGLDITEMUKAN15.ResetText()
        txtLAINLAIN.ResetText()
        deTGLTERATASI1.ResetText()
        deTGLTERATASI2.ResetText()
        deTGLTERATASI3.ResetText()
        deTGLTERATASI4.ResetText()
        deTGLTERATASI5.ResetText()
        deTGLTERATASI6.ResetText()
        deTGLTERATASI7.ResetText()
        deTGLTERATASI8.ResetText()
        deTGLTERATASI9.ResetText()
        deTGLTERATASI10.ResetText()
        deTGLTERATASI11.ResetText()
        deTGLTERATASI12.ResetText()
        deTGLTERATASI13.ResetText()
        deTGLTERATASI14.ResetText()
        deTGLTERATASI15.ResetText()
        chkMK_YA1.Checked = False
        chkMK_YA2.Checked = False
        chkMK_YA3.Checked = False
        chkMK_YA4.Checked = False
        chkMK_YA5.Checked = False
        chkMK_YA6.Checked = False
        chkMK_YA7.Checked = False
        chkMK_YA8.Checked = False
        chkMK_TIDAK1.Checked = False
        chkMK_TIDAK2.Checked = False
        chkMK_TIDAK3.Checked = False
        chkMK_TIDAK4.Checked = False
        chkMK_TIDAK5.Checked = False
        chkMK_TIDAK6.Checked = False
        chkMK_TIDAK7.Checked = False
        chkMK_TIDAK8.Checked = False
        txtKET1.ResetText()
        txtKET2.ResetText()
        txtKET3.ResetText()
        txtKET4.ResetText()
        txtKET5.ResetText()
        txtKET6.ResetText()
        txtKET7.ResetText()
        txtKET8.ResetText()
        txtJAM.ResetText()
    End Sub
    Private Sub fn_LoadData()
        Try
            ' ***** HEADER *****
            Dim ds = oDigital.GetData(sNoid)

            With ds
                
                chkISAUTO.Checked = .ISAUTO
                chkISALLO.Checked = .ISALLO
                txtNAMA.Text = .NAMA
                txtTGLMASUKRS.Text = .TGLMASUKRS
                txtWAKTUPEMERIKSAAN.Text = .WAKTUPEMERIKSAAN
                txtNAMAIBU.Text = .NAMAIBU
                txtTGLLAHIRIBU.Text = .TGLLAHIRIBU
                txtBANGSA.Text = .BANGSA
                chkISDIRAWAT_YA.Checked = .ISDIRAWAT_YA
                chkISDIRAWAT_TIDAK.Checked = .ISDIRAWAT_TIDAK
                chkISKAWIN_YA.Checked = .ISKAWIN_YA
                chkISKAWIN_TIDAK.Checked = .ISKAWIN_TIDAK
                txtAGAMAIBU.Text = .AGAMAIBU
                txtNORMIBU.Text = .NORMIBU
                txtRUANG.Text = .RUANG
                chkISDOKTER.Checked = .ISDOKTER
                chkBIDAN.Checked = .ISBIDAN

                txtTERDAHULU.Text = .TERDAHULU
                txtSELAMAKEHAMILAN.Text = .SELAMAKEHAMILAN
                chkISALERI_YA.Checked = .ISALERI_YA
                chkISALERI_TIDAK.Checked = .ISALERI_TIDAK
                txtALERGI.Text = .ALERGI
                txtGOLDAR.Text = .GOLDAR
                txtABO.Text = .ABO
                txtRH.Text = .RH
                chkSPONTAN.Checked = .SPONTAN
                chkINDUKSIDGCARA.Checked = .INDUKSIDGCARA
                chkSC.Checked = .SC
                chkVIE.Checked = .VIE
                chkFORSEP.Checked = .FORSEP
                chkINDIKASI.Checked = .INDIKASI
                txtINDIKASI_TEXT.Text = .INDIKASI_TEXT
                chkANALGESIA.Checked = .ANALGESIA
                chkANASTESIA.Checked = .ANASTESIA
                chkANTIBIOTIK.Checked = .ANTIBIOTIK
                chkLAINLAIN.Checked = .LAINLAIN
                txtLAINLAIN_TEXT.Text = .LAINLAIN_TEXT
                txtKALAI.Text = .KALAI
                txtKALAII.Text = .KALAII
                txtPECAHKETUBAN.Text = .PECAHKETUBAN
                txtSUHUIBU.Text = .SUHUIBU
                txtGPA.Text = .GPA
                chkSPOG.Checked = .SPOG
                chkPEMERIKSAANBIDAN.Checked = .PEMERIKSAANBIDAN
                chkTIDAKADAPEMERIKSAAN.Checked = .TIDAKADAPEMERIKSAAN
                chkPEMERIKSAANTERATUR.Checked = .PEMERIKSAANTERATUR
                chkPEMERIKSAANTIDAKTERATUR.Checked = .PEMERIKSAANTIDAKTERATUR
                chkISOIMMUNISASI.Checked = .ISOIMMUNISASI
                chkTOXAEMIA.Checked = .TOXAEMIA
                chkHYDROAMINON.Checked = .HYDROAMINON
                chkPENDARAHAN.Checked = .PENDARAHAN
                chkDIABETES.Checked = .DIABETES
                chkINFEKSITRURO.Checked = .INFEKSITRURO
                chkLAIN2.Checked = .LAIN2
                txtLAIN2_TEXT.Text = .LAIN2_TEXT
                chkFD_ADA.Checked = .FD_ADA
                chkFD_TIDAKADA.Checked = .FD_TIDAKADA
                chkFD_TIDAKDIKETAHUI.Checked = .FD_TIDAKDIKETAHUI
                chkMECONIUM.Checked = .MECONIUM
                chkFD_YA.Checked = .FD_YA
                chkFD_TIDAK.Checked = .FD_TIDAK
                chkFD_YA2.Checked = .FD_YA2
                chkFD_TIDAK2.Checked = .FD_TIDAK2
                dePARTUS.Text = .PARTUS
                txtPARTUS_LETAK.Text = .PARTUS_LETAK
                chkPARTUS_SPONTAN.Checked = .PARTUS_SPONTAN
                chkPARTUS_OPERATIF.Checked = .PARTUS_OPERATIF
                txtPLACENTABERAT.Text = .PLACENTABERAT
                txtPLACENTAKEADAAN.Text = .PLACENTAKEADAAN
                chkTALIPUSAT_YA.Checked = .TALIPUSAT_YA
                chkTALIPUSAT_TIDAK.Checked = .TALIPUSAT_TIDAK
                chkTALIPUSAT_3.Checked = .TALIPUSAT_3
                chkTALIPUSAT_2.Checked = .TALIPUSAT_2
                chkTALIPUSAT_TIDAKADA.Checked = .TALIPUSAT_TIDAKADA
                txtTALIPUSAT_KELAINAN.Text = .TALIPUSAT_KELAINAN
                txtTD.Text = .TD
                txtNADI.Text = .NADI
                txtP.Text = .P
                txtSUHU.Text = .SUHU
                chkIRAMA_REGULAR.Checked = .IRAMA_REGULAR
                chkIRAMA_IRREGULAR.Checked = .IRAMA_IRREGULAR
                chkRETRAKSI_TIDAKADA.Checked = .RETRAKSI_TIDAKADA
                chkRETRAKSI_ADA.Checked = .RETRAKSI_ADA
                chkBENTUK_NORMAL.Checked = .BENTUK_NORMAL
                chkBENTUK_TIDAKNORMAL.Checked = .BENTUK_TIDAKNORMAL
                txtBENTUK_TIDAKNORMAL_TEXT.Text = .BENTUK_TIDAKNORMAL_TEXT
                chkPOLANAFAS_NORMAL.Checked = .POLANAFAS_NORMAL
                chkPOLANAFAS_TIDAKNORMAL.Checked = .POLANAFAS_TIDAKNORMAL
                txtPOLANAFAS_TIDAKNORMAL_TEXT.Text = .POLANAFAS_TIDAKNORMAL_TEXT
                chkSUARANAFAS_NORMAL.Checked = .SUARANAFAS_NORMAL
                chkSUARANAFAS_TIDAKNORMAL.Checked = .SUARANAFAS_TIDAKNORMAL
                txtSUARANAFAS_TIDAKNORMAL_TEXT.Text = .SUARANAFAS_TIDAKNORMAL_TEXT
                chkPCH_TIDAKADA.Checked = .PCH_TIDAKADA
                chkPCH_ADA.Checked = .PCH_ADA
                chkSIANOSIS_TIDAKADA.Checked = .SIANOSIS_TIDAKADA
                chkSIANOSIS_ADA.Checked = .SIANOSIS_ADA
                chkALATBANTU_SPONTAN.Checked = .ALATBANTU_SPONTAN
                chkKANUL.Checked = .KANUL
                chkRBMASK.Checked = .RBMASK
                chkNRBMASK.Checked = .NRBMASK
                txtO2.Text = .O2
                chkVENTILATOR.Checked = .VENTILATOR
                txtVENTILATOR_SETTING.Text = .VENTILATOR_SETTING
                chkSIANOSIS2_TIDAKADA.Checked = .SIANOSIS2_TIDAKADA
                chkSIANOSIS2_ADA.Checked = .SIANOSIS2_ADA
                chkPUCAT_TIDAKADA.Checked = .PUCAT_TIDAKADA
                chkPUCAT_ADA.Checked = .PUCAT_ADA
                chkINTENSITAS_KUAT.Checked = .INTENSITAS_KUAT
                chkINTENSITAS_LEMAH.Checked = .INTENSITAS_LEMAH
                chkIRAMANADI_REGULER.Checked = .IRAMANADI_REGULER
                chkIRAMANADI_IRREGULER.Checked = .IRAMANADI_IRREGULER
                chkEDEMA_TIDAKADA.Checked = .EDEMA_TIDAKADA
                chkEDEMA_ADA.Checked = .EDEMA_ADA
                chkAKRAL_HANGAT.Checked = .AKRAL_HANGAT
                chkAKRAL_DINGIN.Checked = .AKRAL_DINGIN
                chkCRT_KURANG.Checked = .CRT_KURANG
                chkCRT_LEBIH.Checked = .CRT_LEBIH
                chkCLUBBINGFINGER_TIDAKADA.Checked = .CLUBBINGFINGER_TIDAKADA
                chkCLUBBINGFINGER_ADA.Checked = .CLUBBINGFINGER_ADA
                chkKESADARAN_ALERT.Checked = .KESADARAN_ALERT
                chkKESADARAN_LETARGIS.Checked = .KESADARAN_LETARGIS
                chkKESADARAN_SEDASI.Checked = .KESADARAN_SEDASI
                chkGANGGUAN_TIDAKADA.Checked = .GANGGUAN_TIDAKADA
                chkGANGGUAN_ADA.Checked = .GANGGUAN_ADA
                txtGANGGUAN_ADA_TEXT.Text = .GANGGUAN_ADA_TEXT
                chkMULUT_MUKOSALEMBAB.Checked = .MULUT_MUKOSALEMBAB
                chkMULUT_MUKOSAKERING.Checked = .MULUT_MUKOSAKERING
                chkMULUT_LABIO.Checked = .MULUT_LABIO
                chkMULUT_PENDARAHANGUSI.Checked = .MULUT_PENDARAHANGUSI
                chkMULUT_LAINLAIN.Checked = .MULUT_LAINLAIN
                chkMUNTAH_YA.Checked = .MUNTAH_YA
                chkMUNTAH_TIDAK.Checked = .MUNTAH_TIDAK
                chkMUAL_YA.Checked = .MUAL_YA
                chkMUAL_TIDAK.Checked = .MUAL_TIDAK
                txtPERISTALTIK_USUS.Text = .PERISTALTIK_USUS
                txtLINGKARPERUT.Text = .LINGKAR_PERUT
                chkPENGELUARAN_ANUS.Checked = .PENGELUARAN_ANUS
                chkSTOMA.Checked = .STOMA
                txtSTOMA_TEXT.Text = .STOMA_TEXT
                txtFREKUENSI.Text = .FREKUENSI
                txtKONSISTENSI.Text = .KONSISTENSI
                chkFESES_NORMAL.Checked = .FESES_NORMAL
                chkFESES_CAIR.Checked = .FESES_CAIR
                chkFESES_HIJAU.Checked = .FESES_HIJAU
                chkFESES_DEMPUL.Checked = .FESES_DEMPUL
                chkFESES_BERDARAH.Checked = .FESES_BERDARAH
                chkFESES_LAINLAIN.Checked = .FESES_LAINLAIN
                txtFESES_LAINLAIN_TEXT.Text = .FESES_LAINLAIN_TEXT
                chkURIN_SPONTAN.Checked = .URIN_SPONTAN
                chkURIN_KATETER.Checked = .URIN_KATETER
                chkURIN_CYSTOSTOMY.Checked = .URIN_CYSTOSTOMY
                chkURIN_KELAINAN_TIDAKADA.Checked = .URIN_KELAINAN_TIDAKADA
                chkURIN_KELAINAN_ADA.Checked = .URIN_KELAINAN_ADA
                txtURIN_KELAINAN.Text = .URIN_KELAINAN
                txtDIARESES.Text = .DIARESES
                chkWARNAKULIT_NORMAL.Checked = .WARNAKULIT_NORMAL
                chkWARNAKULIT_PUCAT.Checked = .WARNAKULIT_PUCAT
                chkWARNAKULIT_KUNING.Checked = .WARNAKULIT_KUNING
                chkWARNAKULIT_MOTTED.Checked = .WARNAKULIT_MOTTED
                chkKELAINANKULIT_TIDAKADA.Checked = .KELAINANKULIT_TIDAKADA
                chkKELAINANKULIT_ADA.Checked = .KELAINANKULIT_ADA
                chkMUSKULOSKELETAL_TIDAKADA.Checked = .MUSKULOSKELETAL_TIDAKADA
                chkMUSKULOSKELETAL_ADA.Checked = .MUSKULOSKELETAL_ADA
                txtMUSKULOSKELETAL.Text = .MUSKULOSKELETAL
                chkGERAKAN_BEBAS.Checked = .GERAKAN_BEBAS
                chkGERAKAN_TERBATAS.Checked = .GERAKAN_TERBATAS
                chkGENITALIA_NORMAL.Checked = .GENITALIA_NORMAL
                chkGENITALIA_KELAINAN.Checked = .GENITALIA_KELAINAN
                txtGENITALIA.Text = .GENITALIA
                cboSKOR1.Text = .SKOR1
                cboSKOR2.Text = .SKOR2
                cboSKOR3.Text = .SKOR3
                cboSKOR4.Text = .SKOR4
                cboSKOR5.Text = .SKOR5
                cboSKOR6.Text = .SKOR6
                txtTOTALSKOR.Text = .TOTALSKOR
                deTGLDITEMUKAN1.Text = .TGLDITEMUKAN1
                deTGLDITEMUKAN2.Text = .TGLDITEMUKAN2
                deTGLDITEMUKAN3.Text = .TGLDITEMUKAN3
                deTGLDITEMUKAN4.Text = .TGLDITEMUKAN4
                deTGLDITEMUKAN5.Text = .TGLDITEMUKAN5
                deTGLDITEMUKAN6.Text = .TGLDITEMUKAN6
                deTGLDITEMUKAN7.Text = .TGLDITEMUKAN7
                deTGLDITEMUKAN8.Text = .TGLDITEMUKAN8
                deTGLDITEMUKAN9.Text = .TGLDITEMUKAN9
                deTGLDITEMUKAN10.Text = .TGLDITEMUKAN10
                deTGLDITEMUKAN11.Text = .TGLDITEMUKAN11
                deTGLDITEMUKAN12.Text = .TGLDITEMUKAN12
                deTGLDITEMUKAN13.Text = .TGLDITEMUKAN13
                deTGLDITEMUKAN14.Text = .TGLDITEMUKAN14
                deTGLDITEMUKAN15.Text = .TGLDITEMUKAN15
                txtLAINLAIN.Text = .LAINLAIN2
                deTGLTERATASI1.Text = .TGLTERATASI1
                deTGLTERATASI2.Text = .TGLTERATASI2
                deTGLTERATASI3.Text = .TGLTERATASI3
                deTGLTERATASI4.Text = .TGLTERATASI4
                deTGLTERATASI5.Text = .TGLTERATASI5
                deTGLTERATASI6.Text = .TGLTERATASI6
                deTGLTERATASI7.Text = .TGLTERATASI7
                deTGLTERATASI8.Text = .TGLTERATASI8
                deTGLTERATASI9.Text = .TGLTERATASI9
                deTGLTERATASI10.Text = .TGLTERATASI10
                deTGLTERATASI11.Text = .TGLTERATASI11
                deTGLTERATASI12.Text = .TGLTERATASI12
                deTGLTERATASI13.Text = .TGLTERATASI13
                deTGLTERATASI14.Text = .TGLTERATASI14
                deTGLTERATASI15.Text = .TGLTERATASI15
                chkMK_YA1.Checked = .MK_YA1
                chkMK_YA2.Checked = .MK_YA2
                chkMK_YA3.Checked = .MK_YA3
                chkMK_YA4.Checked = .MK_YA4
                chkMK_YA5.Checked = .MK_YA5
                chkMK_YA6.Checked = .MK_YA6
                chkMK_YA7.Checked = .MK_YA7
                chkMK_YA8.Checked = .MK_YA8
                chkMK_TIDAK1.Checked = .MK_TIDAK1
                chkMK_TIDAK2.Checked = .MK_TIDAK2
                chkMK_TIDAK3.Checked = .MK_TIDAK3
                chkMK_TIDAK4.Checked = .MK_TIDAK4
                chkMK_TIDAK5.Checked = .MK_TIDAK5
                chkMK_TIDAK6.Checked = .MK_TIDAK6
                chkMK_TIDAK7.Checked = .MK_TIDAK7
                chkMK_TIDAK8.Checked = .MK_TIDAK8
                txtKET1.Text = .KET1
                txtKET2.Text = .KET2
                txtKET3.Text = .KET3
                txtKET4.Text = .KET4
                txtKET5.Text = .KET5
                txtKET6.Text = .KET6
                txtKET7.Text = .KET7
                txtKET8.Text = .KET8
                txtJAM.Text = .JAM

                BindingSource1.DataSource = oDigital.GetDataDetail.Where(Function(x) x.KDASESMEN = sNoid).OrderBy(Function(x) x.SEQ).ToList()
                grdDetail.DataSource = BindingSource1

                grdDokter.EditValue = .KDSTAFFDOKTER
                txtPERAWAT.Text = .KDUSER

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
    Private Function fn_Save() As Boolean
        Try
            ' ***** HEADER *****
            Dim ds = oDigital.GetStructureHeader
            With ds
                .KDASESMEN = sNoid
                .KDPENDAFTARAN = txtNoRegister.Text
                .KDCUSTOMER = txtNoPasien.Text

                Try
                    .DATE = oDigital.GetData(sNoid).DATE
                    .DATECREATED = oDigital.GetData(sNoid).DATECREATED
                Catch ex As Exception
                    .DATE = Now
                    .DATECREATED = Now
                End Try
                .DATEUPDATED = Now
                .ISAUTO = chkISAUTO.Checked
                .ISALLO = chkISALLO.Checked
                .NAMA = txtNAMA.Text
                .TGLMASUKRS = txtTGLMASUKRS.Text
                .WAKTUPEMERIKSAAN = txtWAKTUPEMERIKSAAN.Text
                .NAMAIBU = txtNAMAIBU.Text
                .TGLLAHIRIBU = txtTGLLAHIRIBU.Text
                .BANGSA = txtBANGSA.Text
                .ISDIRAWAT_YA = chkISDIRAWAT_YA.Checked
                .ISDIRAWAT_TIDAK = chkISDIRAWAT_TIDAK.Checked
                .ISKAWIN_YA = chkISKAWIN_YA.Checked
                .ISKAWIN_TIDAK = chkISKAWIN_TIDAK.Checked
                .AGAMAIBU = txtAGAMAIBU.Text
                .NORMIBU = txtNORMIBU.Text
                .RUANG = txtRUANG.Text
                .ISDOKTER = chkISDOKTER.Checked
                .ISBIDAN = chkBIDAN.Checked
                .TERDAHULU = txtTERDAHULU.Text
                .SELAMAKEHAMILAN = txtSELAMAKEHAMILAN.Text
                .ISALERI_YA = chkISALERI_YA.Checked
                .ISALERI_TIDAK = chkISALERI_TIDAK.Checked
                .ALERGI = txtALERGI.Text
                .GOLDAR = txtGOLDAR.Text
                .ABO = txtABO.Text
                .RH = txtRH.Text
                .SPONTAN = chkSPONTAN.Checked
                .INDUKSIDGCARA = chkINDUKSIDGCARA.Checked
                .SC = chkSC.Checked
                .VIE = chkVIE.Checked
                .FORSEP = chkFORSEP.Checked
                .INDIKASI = chkINDIKASI.Checked
                .INDIKASI_TEXT = txtINDIKASI_TEXT.Text
                .ANALGESIA = chkANALGESIA.Checked
                .ANASTESIA = chkANASTESIA.Checked
                .ANTIBIOTIK = chkANTIBIOTIK.Checked
                .LAINLAIN = chkLAINLAIN.Checked
                .LAINLAIN_TEXT = txtLAINLAIN_TEXT.Text
                .KALAI = txtKALAI.Text
                .KALAII = txtKALAII.Text
                .PECAHKETUBAN = txtPECAHKETUBAN.Text
                .SUHUIBU = txtSUHUIBU.Text
                .GPA = txtGPA.Text
                .SPOG = chkSPOG.Checked
                .PEMERIKSAANBIDAN = chkPEMERIKSAANBIDAN.Checked
                .TIDAKADAPEMERIKSAAN = chkTIDAKADAPEMERIKSAAN.Checked
                .PEMERIKSAANTERATUR = chkPEMERIKSAANTERATUR.Checked
                .PEMERIKSAANTIDAKTERATUR = chkPEMERIKSAANTIDAKTERATUR.Checked
                .ISOIMMUNISASI = chkISOIMMUNISASI.Checked
                .TOXAEMIA = chkTOXAEMIA.Checked
                .HYDROAMINON = chkHYDROAMINON.Checked
                .PENDARAHAN = chkPENDARAHAN.Checked
                .DIABETES = chkDIABETES.Checked
                .INFEKSITRURO = chkINFEKSITRURO.Checked
                .LAIN2 = chkLAIN2.Checked
                .LAIN2_TEXT = txtLAIN2_TEXT.Text
                .FD_ADA = chkFD_ADA.Checked
                .FD_TIDAKADA = chkFD_TIDAKADA.Checked
                .FD_TIDAKDIKETAHUI = chkFD_TIDAKDIKETAHUI.Checked
                .MECONIUM = chkMECONIUM.Checked
                .FD_YA = chkFD_YA.Checked
                .FD_TIDAK = chkFD_TIDAK.Checked
                .FD_YA2 = chkFD_YA2.Checked
                .FD_TIDAK2 = chkFD_TIDAK2.Checked
                .PARTUS = dePARTUS.Text
                .PARTUS_LETAK = txtPARTUS_LETAK.Text
                .PARTUS_SPONTAN = chkPARTUS_SPONTAN.Checked
                .PARTUS_OPERATIF = chkPARTUS_OPERATIF.Checked
                .PLACENTABERAT = txtPLACENTABERAT.Text
                .PLACENTAKEADAAN = txtPLACENTAKEADAAN.Text
                .TALIPUSAT_YA = chkTALIPUSAT_YA.Checked
                .TALIPUSAT_TIDAK = chkTALIPUSAT_TIDAK.Checked
                .TALIPUSAT_3 = chkTALIPUSAT_3.Checked
                .TALIPUSAT_2 = chkTALIPUSAT_2.Checked
                .TALIPUSAT_TIDAKADA = chkTALIPUSAT_TIDAKADA.Checked
                .TALIPUSAT_KELAINAN = txtTALIPUSAT_KELAINAN.Text
                .TD = txtTD.Text
                .NADI = txtNADI.Text
                .P = txtP.Text
                .SUHU = txtSUHU.Text
                .IRAMA_REGULAR = chkIRAMA_REGULAR.Checked
                .IRAMA_IRREGULAR = chkIRAMA_IRREGULAR.Checked
                .RETRAKSI_TIDAKADA = chkRETRAKSI_TIDAKADA.Checked
                .RETRAKSI_ADA = chkRETRAKSI_ADA.Checked
                .BENTUK_NORMAL = chkBENTUK_NORMAL.Checked
                .BENTUK_TIDAKNORMAL = chkBENTUK_TIDAKNORMAL.Checked
                .BENTUK_TIDAKNORMAL_TEXT = txtBENTUK_TIDAKNORMAL_TEXT.Text
                .POLANAFAS_NORMAL = chkPOLANAFAS_NORMAL.Checked
                .POLANAFAS_TIDAKNORMAL = chkPOLANAFAS_TIDAKNORMAL.Checked
                .POLANAFAS_TIDAKNORMAL_TEXT = txtPOLANAFAS_TIDAKNORMAL_TEXT.Text
                .SUARANAFAS_NORMAL = chkSUARANAFAS_NORMAL.Checked
                .SUARANAFAS_TIDAKNORMAL = chkSUARANAFAS_TIDAKNORMAL.Checked
                .SUARANAFAS_TIDAKNORMAL_TEXT = txtSUARANAFAS_TIDAKNORMAL_TEXT.Text
                .PCH_TIDAKADA = chkPCH_TIDAKADA.Checked
                .PCH_ADA = chkPCH_ADA.Checked
                .SIANOSIS_TIDAKADA = chkSIANOSIS_TIDAKADA.Checked
                .SIANOSIS_ADA = chkSIANOSIS_ADA.Checked
                .ALATBANTU_SPONTAN = chkALATBANTU_SPONTAN.Checked
                .KANUL = chkKANUL.Checked
                .RBMASK = chkRBMASK.Checked
                .NRBMASK = chkNRBMASK.Checked
                .O2 = txtO2.Text
                .VENTILATOR = chkVENTILATOR.Checked
                .VENTILATOR_SETTING = txtVENTILATOR_SETTING.Text
                .SIANOSIS2_TIDAKADA = chkSIANOSIS2_TIDAKADA.Checked
                .SIANOSIS2_ADA = chkSIANOSIS2_ADA.Checked
                .PUCAT_TIDAKADA = chkPUCAT_TIDAKADA.Checked
                .PUCAT_ADA = chkPUCAT_ADA.Checked
                .INTENSITAS_KUAT = chkINTENSITAS_KUAT.Checked
                .INTENSITAS_LEMAH = chkINTENSITAS_LEMAH.Checked
                .IRAMANADI_REGULER = chkIRAMANADI_REGULER.Checked
                .IRAMANADI_IRREGULER = chkIRAMANADI_IRREGULER.Checked
                .EDEMA_TIDAKADA = chkEDEMA_TIDAKADA.Checked
                .EDEMA_ADA = chkEDEMA_ADA.Checked
                .AKRAL_HANGAT = chkAKRAL_HANGAT.Checked
                .AKRAL_DINGIN = chkAKRAL_DINGIN.Checked
                .CRT_KURANG = chkCRT_KURANG.Checked
                .CRT_LEBIH = chkCRT_LEBIH.Checked
                .CLUBBINGFINGER_TIDAKADA = chkCLUBBINGFINGER_TIDAKADA.Checked
                .CLUBBINGFINGER_ADA = chkCLUBBINGFINGER_ADA.Checked
                .KESADARAN_ALERT = chkKESADARAN_ALERT.Checked
                .KESADARAN_LETARGIS = chkKESADARAN_LETARGIS.Checked
                .KESADARAN_SEDASI = chkKESADARAN_SEDASI.Checked
                .GANGGUAN_TIDAKADA = chkGANGGUAN_TIDAKADA.Checked
                .GANGGUAN_ADA = chkGANGGUAN_ADA.Checked
                .GANGGUAN_ADA_TEXT = txtGANGGUAN_ADA_TEXT.Text
                .MULUT_MUKOSALEMBAB = chkMULUT_MUKOSALEMBAB.Checked
                .MULUT_MUKOSAKERING = chkMULUT_MUKOSAKERING.Checked
                .MULUT_LABIO = chkMULUT_LABIO.Checked
                .MULUT_PENDARAHANGUSI = chkMULUT_PENDARAHANGUSI.Checked
                .MULUT_LAINLAIN = chkMULUT_LAINLAIN.Checked
                .MUNTAH_YA = chkMUNTAH_YA.Checked
                .MUNTAH_TIDAK = chkMUNTAH_TIDAK.Checked
                .MUAL_YA = chkMUAL_YA.Checked
                .MUAL_TIDAK = chkMUAL_TIDAK.Checked
                .PERISTALTIK_USUS = txtPERISTALTIK_USUS.Text
                .LINGKAR_PERUT = txtLINGKARPERUT.Text
                .PENGELUARAN_ANUS = chkPENGELUARAN_ANUS.Checked
                .STOMA = chkSTOMA.Checked
                .STOMA_TEXT = txtSTOMA_TEXT.Text
                .FREKUENSI = txtFREKUENSI.Text
                .KONSISTENSI = txtKONSISTENSI.Text
                .FESES_NORMAL = chkFESES_NORMAL.Checked
                .FESES_CAIR = chkFESES_CAIR.Checked
                .FESES_HIJAU = chkFESES_HIJAU.Checked
                .FESES_DEMPUL = chkFESES_DEMPUL.Checked
                .FESES_BERDARAH = chkFESES_BERDARAH.Checked
                .FESES_LAINLAIN = chkFESES_LAINLAIN.Checked
                .FESES_LAINLAIN_TEXT = txtFESES_LAINLAIN_TEXT.Text
                .URIN_SPONTAN = chkURIN_SPONTAN.Checked
                .URIN_KATETER = chkURIN_KATETER.Checked
                .URIN_CYSTOSTOMY = chkURIN_CYSTOSTOMY.Checked
                .URIN_KELAINAN_TIDAKADA = chkURIN_KELAINAN_TIDAKADA.Checked
                .URIN_KELAINAN_ADA = chkURIN_KELAINAN_ADA.Checked
                .URIN_KELAINAN = txtURIN_KELAINAN.Text
                .DIARESES = txtDIARESES.Text
                .WARNAKULIT_NORMAL = chkWARNAKULIT_NORMAL.Checked
                .WARNAKULIT_PUCAT = chkWARNAKULIT_PUCAT.Checked
                .WARNAKULIT_KUNING = chkWARNAKULIT_KUNING.Checked
                .WARNAKULIT_MOTTED = chkWARNAKULIT_MOTTED.Checked
                .KELAINANKULIT_TIDAKADA = chkKELAINANKULIT_TIDAKADA.Checked
                .KELAINANKULIT_ADA = chkKELAINANKULIT_ADA.Checked
                .MUSKULOSKELETAL_TIDAKADA = chkMUSKULOSKELETAL_TIDAKADA.Checked
                .MUSKULOSKELETAL_ADA = chkMUSKULOSKELETAL_ADA.Checked
                .MUSKULOSKELETAL = txtMUSKULOSKELETAL.Text
                .GERAKAN_BEBAS = chkGERAKAN_BEBAS.Checked
                .GERAKAN_TERBATAS = chkGERAKAN_TERBATAS.Checked
                .GENITALIA_NORMAL = chkGENITALIA_NORMAL.Checked
                .GENITALIA_KELAINAN = chkGENITALIA_KELAINAN.Checked
                .GENITALIA = txtGENITALIA.Text
                .SKOR1 = cboSKOR1.Text
                .SKOR2 = cboSKOR2.Text
                .SKOR3 = cboSKOR3.Text
                .SKOR4 = cboSKOR4.Text
                .SKOR5 = cboSKOR5.Text
                .SKOR6 = cboSKOR6.Text
                .TOTALSKOR = txtTOTALSKOR.Text
                .TGLDITEMUKAN1 = deTGLDITEMUKAN1.Text
                .TGLDITEMUKAN2 = deTGLDITEMUKAN2.Text
                .TGLDITEMUKAN3 = deTGLDITEMUKAN3.Text
                .TGLDITEMUKAN4 = deTGLDITEMUKAN4.Text
                .TGLDITEMUKAN5 = deTGLDITEMUKAN5.Text
                .TGLDITEMUKAN6 = deTGLDITEMUKAN6.Text
                .TGLDITEMUKAN7 = deTGLDITEMUKAN7.Text
                .TGLDITEMUKAN8 = deTGLDITEMUKAN8.Text
                .TGLDITEMUKAN9 = deTGLDITEMUKAN9.Text
                .TGLDITEMUKAN10 = deTGLDITEMUKAN10.Text
                .TGLDITEMUKAN11 = deTGLDITEMUKAN11.Text
                .TGLDITEMUKAN12 = deTGLDITEMUKAN12.Text
                .TGLDITEMUKAN13 = deTGLDITEMUKAN13.Text
                .TGLDITEMUKAN14 = deTGLDITEMUKAN14.Text
                .TGLDITEMUKAN15 = deTGLDITEMUKAN15.Text
                .LAINLAIN2 = txtLAINLAIN.Text
                .TGLTERATASI1 = deTGLTERATASI1.Text
                .TGLTERATASI2 = deTGLTERATASI2.Text
                .TGLTERATASI3 = deTGLTERATASI3.Text
                .TGLTERATASI4 = deTGLTERATASI4.Text
                .TGLTERATASI5 = deTGLTERATASI5.Text
                .TGLTERATASI6 = deTGLTERATASI6.Text
                .TGLTERATASI7 = deTGLTERATASI7.Text
                .TGLTERATASI8 = deTGLTERATASI8.Text
                .TGLTERATASI9 = deTGLTERATASI9.Text
                .TGLTERATASI10 = deTGLTERATASI10.Text
                .TGLTERATASI11 = deTGLTERATASI11.Text
                .TGLTERATASI12 = deTGLTERATASI12.Text
                .TGLTERATASI13 = deTGLTERATASI13.Text
                .TGLTERATASI14 = deTGLTERATASI14.Text
                .TGLTERATASI15 = deTGLTERATASI15.Text
                .MK_YA1 = chkMK_YA1.Checked
                .MK_YA2 = chkMK_YA2.Checked
                .MK_YA3 = chkMK_YA3.Checked
                .MK_YA4 = chkMK_YA4.Checked
                .MK_YA5 = chkMK_YA5.Checked
                .MK_YA6 = chkMK_YA6.Checked
                .MK_YA7 = chkMK_YA7.Checked
                .MK_YA8 = chkMK_YA8.Checked
                .MK_TIDAK1 = chkMK_TIDAK1.Checked
                .MK_TIDAK2 = chkMK_TIDAK2.Checked
                .MK_TIDAK3 = chkMK_TIDAK3.Checked
                .MK_TIDAK4 = chkMK_TIDAK4.Checked
                .MK_TIDAK5 = chkMK_TIDAK5.Checked
                .MK_TIDAK6 = chkMK_TIDAK6.Checked
                .MK_TIDAK7 = chkMK_TIDAK7.Checked
                .MK_TIDAK8 = chkMK_TIDAK8.Checked
                .KET1 = txtKET1.Text
                .KET2 = txtKET2.Text
                .KET3 = txtKET3.Text
                .KET4 = txtKET4.Text
                .KET5 = txtKET5.Text
                .KET6 = txtKET6.Text
                .KET7 = txtKET7.Text
                .KET8 = txtKET8.Text
                .JAM = txtJAM.Text
                .KDSTAFFDOKTER = IIf(grdDokter.Text = "", "", grdDokter.EditValue)
                .NAMADOKTER = IIf(grdDokter.Text = "", "", grdDokter.Text)
                .KDSTAFFPERAWAT = ""
                .NAMAPERAWAT = txtPERAWAT.Text
                Try
                    .CETAK = oDigital.GetData(sNoid).CETAK
                Catch ex As Exception
                    .CETAK = 0
                End Try
                .KDUSER = txtPERAWAT.Text
                .KDSIGNATURE = sUserID
                .ISDELETE = False
            End With

            ' ***** DETIL *****
            Dim arrDetail = oDigital.GetStructureDetailList
            For i As Integer = 0 To grvDetail.RowCount - 2
                Dim dsDetail = oDigital.GetStructureDetail
                With dsDetail
                    .SEQ = i
                    .KDASESMEN = ds.KDASESMEN
                    .TGLTAHUNKELAHIRAN = grvDetail.GetRowCellValue(i, colTGLKELAHIRAN)
                    .SEX = grvDetail.GetRowCellValue(i, colSEX)
                    .BERATBADANLAHIR = grvDetail.GetRowCellValue(i, colBBL)
                    .KEADAANBAYI = grvDetail.GetRowCellValue(i, colKEADAANBAYI)
                    .KOMPLIKASI = grvDetail.GetRowCellValue(i, colKOMPLIKASI)
                    .PENYAKITWAKTUHAMIL = grvDetail.GetRowCellValue(i, colPENYAKITWAKTUHAMIL)
                    .JENISPERSALINAN = grvDetail.GetRowCellValue(i, colJENISPERSALINAN)
                    .LAINLAIN = grvDetail.GetRowCellValue(i, colLAIN)
                End With
                arrDetail.Add(dsDetail)
            Next

            If oFormMode = FORM_MODE.FORM_MODE_ADD Then
                Try
                    fn_Save = oDigital.InsertData(ds, arrDetail)
                Catch ex As Exception
                    MsgBox("Simpan Data : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            ElseIf oFormMode = FORM_MODE.FORM_MODE_EDIT Then
                Try
                    fn_Save = oDigital.UpdateData(sNoid, ds, arrDetail)
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
        'If fn_Validate() = False Then Exit Sub
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


#End Region
#Region "Lookup / Event"
    Private Sub fn_LoadDoctor()
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
            SQL &= "KDSTAFF = A.KDDOCTOR  "
            SQL &= ",NAME_DISPLAY = A.NAME_DISPLAY "
            SQL &= "FROM "
            SQL &= "M_DOCTOR A "
            SQL &= "WHERE "
            SQL &= "A.ISACTIVE = 1 "

            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "STAFF")

            grdDokter.Properties.DataSource = ds.Tables("STAFF")
            grdDokter.Properties.ValueMember = "KDSTAFF"
            grdDokter.Properties.DisplayMember = "NAME_DISPLAY"

            'grdPerawat.Properties.DataSource = ds.Tables("STAFF")
            'grdPerawat.Properties.ValueMember = "KDSTAFF"
            'grdPerawat.Properties.DisplayMember = "NAME_DISPLAY"

            If oConn.State = ConnectionState.Open Then
                oConn.Close()
            End If
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub

    Private Sub DeleteToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DeleteToolStripMenuItem.Click
        If oFormMode = FORM_MODE.FORM_MODE_VIEW Then Exit Sub
        grvDetail.DeleteSelectedRows()
    End Sub

    Private Sub frmAsesmenAwalKeperawatanNeonatal_MouseWheel(sender As Object, e As MouseEventArgs) Handles MyBase.MouseWheel
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