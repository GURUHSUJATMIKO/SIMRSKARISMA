Public Class xtraOpname
    Private Sub xtraOpname_BeforePrint(sender As Object, e As System.Drawing.Printing.PrintEventArgs) Handles Me.BeforePrint
        lCOMPANY.Text = sCompany
        lADDRESS.Text = sAddress
        lPHONE.Text = sPhone
        lNPWP.Text = sNPWP
    End Sub
End Class