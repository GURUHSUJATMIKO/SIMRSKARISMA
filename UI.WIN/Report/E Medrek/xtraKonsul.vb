Public Class xtraKonsul
    Private Sub xtra_BeforePrint(ByVal sender As System.Object, ByVal e As System.Drawing.Printing.PrintEventArgs) Handles MyBase.BeforePrint
        txtNama.Text = sNAMA_DIKONSUL
        txtRM.Text = sRM_DIKONSUL
        txtRuangan.Text = sRUANG_DIKONSUL
        txtDokterDari.Text = sDOKTERDARI_DIKONSUL
        txtDokterKepada.Text = sDOKTERKEPADA_DIKONSUL
        txtKonsul.Text = sISIKONSUL_DIKONSUL
        txtJawab.Text = sJAWABKONSUL_DIKONSUL
    End Sub
End Class