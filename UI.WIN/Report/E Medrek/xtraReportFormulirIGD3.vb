Public Class xtraReportFormulirIGD3
    Private Sub xtra_BeforePrint(ByVal sender As System.Object, ByVal e As System.Drawing.Printing.PrintEventArgs) Handles MyBase.BeforePrint
        lblNAMA.Text = NAMA
        lblRM.Text = RM
        lblTanggalLahir.Text = TANGGALLAHIR
    End Sub
End Class