Public Class xtraLabelGelang_New_04
    Private Sub xtra_BeforePrint(ByVal sender As System.Object, ByVal e As System.Drawing.Printing.PrintEventArgs) Handles MyBase.BeforePrint
        'lJK.Text = sJK
        txtRM1.Text = sRekamMedis_1
        txtRM2.Text = sRekamMedis_2
        txtRM3.Text = sRekamMedis_3
        txtRM4.Text = sRekamMedis_4
        txtRM5.Text = sRekamMedis_5
        txtRM6.Text = sRekamMedis_6
    End Sub
End Class