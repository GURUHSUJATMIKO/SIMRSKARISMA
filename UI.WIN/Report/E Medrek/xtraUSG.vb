Public Class xtraUSG
    Private Sub xtra_BeforePrint(ByVal sender As System.Object, ByVal e As System.Drawing.Printing.PrintEventArgs) Handles MyBase.BeforePrint
        Dim sTest = StrConv(sUserID, VbStrConv.ProperCase)

        'lCetak.Text =  "Di print pada tanggal " & Now.ToString("dd/MM/yyyy HH:mm")
        'lblNOUSG.Text = sNO_USG
        'lblNAMA.Text = sNAMA_USG
        'lblUMUR.Text = sUMUR_USG
        'lblDATE.Text = sDATE_USG
        'lblKDTRARIF.Text = sKDTARIF_USG
        'lblGEJALA.Text = sGEJALA_USG
        'lblDOKTER.Text = sDOKTER_USG
        'lblDESCRIPTION.Text = sDESCRIPTION_USG
        'lblDOK.Text = sDOKTER_RAD2

        lblNOUSG.Text = sNOMORUSG_RAD
        lblUMUR.Text = sUMUR_RAD
        lblKDTRARIF.Text = sPEMERIKSAAN_RAD
        lblGEJALA.Text = sDIAGNOSA_RAD
        'lblRM.Text = sKDCUSTOMER_RAD
        lblNAMA.Text = sNAME_DISPLAY_RAD
        lblDATE.Text = sDATE_RAD
        'lblDEPARTMENT.Text = sDEPARTMENT_RAD
        'lblREPORTDATE.Text = sREPORTDATE_RAD
        'lblEXAMDESC.Text = sEXAMPDESC_RAD
        lblDOKTER.Text = sDOKTER_RAD
        'lblDIAGNOSA.Text = sDIAGNOSA_RAD
        lblDESCRIPTION.Text = IIf(sDESCRIPTION_RAD = "", sHasilLoadRadiologi, sDESCRIPTION_RAD)
        lblDOK.Text = sDOKTER_RAD2
    End Sub
End Class