Public Class xtraRincianRawatJalanBPJS
    Private Sub xtra_BeforePrint(ByVal sender As System.Object, ByVal e As System.Drawing.Printing.PrintEventArgs) Handles MyBase.BeforePrint
        If sCetakUserByMerge = False Then
            Dim sTest = StrConv(sUserID, VbStrConv.ProperCase)

            lTANGGAL.Text = "Printed on "
            lBy.Text = " by " & sTest
        Else
            lBy.Text = " by " & sUSERCASHIER
        End If

        lOBATKRONIS.Text = FormatNumber(sTotalProlanis, 0)
        lTOTALPAKET.Text = FormatNumber(sTotalPaket, 0)
        lTOTALRS.Text = FormatNumber(sGrandtotalRS, 0)

    End Sub
End Class