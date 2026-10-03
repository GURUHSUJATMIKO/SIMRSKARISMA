Public Class xtraRincianRawatJalan
    Private Sub xtra_BeforePrint(ByVal sender As System.Object, ByVal e As System.Drawing.Printing.PrintEventArgs) Handles MyBase.BeforePrint
        If sCetakUserByMerge = False Then
            Dim sTest = StrConv(sUserID, VbStrConv.ProperCase)

            lTANGGAL.Text = "Printed on "
            lBy.Text = " by " & sTest
        Else
            lBy.Text = " by " & sUSERCASHIER
        End If

    End Sub
End Class