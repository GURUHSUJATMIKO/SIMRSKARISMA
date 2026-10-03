Public Class xtraReportEMedrekRI_31
    Private Sub xtra_BeforePrint(ByVal sender As System.Object, ByVal e As System.Drawing.Printing.PrintEventArgs) Handles MyBase.BeforePrint
        txtNAMA.Text = NAMA
        txtJENISKELAMIN.Text = JENISKELAMIN
        txtTANGGALLAHIR.Text = TANGGALLAHIR
    End Sub
End Class