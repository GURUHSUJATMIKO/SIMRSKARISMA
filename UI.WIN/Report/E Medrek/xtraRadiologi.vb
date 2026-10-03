Public Class xtraRadiologi
    Private Sub xtra_BeforePrint(ByVal sender As System.Object, ByVal e As System.Drawing.Printing.PrintEventArgs) Handles MyBase.BeforePrint
        Dim sTest = StrConv(sUserID, VbStrConv.ProperCase)

        'lCetak.Text =  "Di print pada tanggal " & Now.ToString("dd/MM/yyyy HH:mm")

        lblRM.Text = sKDCUSTOMER_RAD
        lblNAMA.Text = sNAME_DISPLAY_RAD
        lblTANGGAL.Text = sDATE_RAD
        lblDEPARTMENT.Text = sDEPARTMENT_RAD
        lblREPORTDATE.Text = sREPORTDATE_RAD
        lblEXAMDESC.Text = sEXAMPDESC_RAD
        lblDOCTOR.Text = sDOKTER_RAD
        lblDIAGNOSA.Text = sDIAGNOSA_RAD
        lblDESCRIPTION.Text = IIf(sDESCRIPTION_RAD = "", sHasilLoadRadiologi, sDESCRIPTION_RAD)
        lblDOKTER.Text = sDOKTER_RAD2
        lblEXAMDESC.Text = sPEMERIKSAAN_RAD
    End Sub
End Class