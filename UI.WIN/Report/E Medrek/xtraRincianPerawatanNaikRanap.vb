Public Class xtraRincianPerawatanNaikRanap
    Private Sub xtra_BeforePrint(ByVal sender As System.Object, ByVal e As System.Drawing.Printing.PrintEventArgs) Handles MyBase.BeforePrint
        If sCetakUserByMerge = False Then
            Dim sTest = StrConv(sUserID, VbStrConv.ProperCase)

            lTANGGAL.Text = Now.ToString("dd/MM/yyy HH:mm")

            lCetak.Text = "Printed on "
            lBy.Text = " by " & sTest

        Else
            lBy.Text = " by " & sUSERCASHIER
        End If

        'If sKetPerusahaan = "PERUSAHAAN" Or sKetPerusahaan = "MCU" Or sKetPerusahaan = "KEMENTERIAN KESEHATAN" Then
        '    lPerusahaan.Text = sKetPerusahaan & " \ " & sPerusahaan
        'Else
        '    lPerusahaan.Text = sKetPerusahaan
        'End If

    End Sub
End Class