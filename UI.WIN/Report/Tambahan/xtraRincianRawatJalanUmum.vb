Public Class xtraRincianRawatJalanUmum
    Private Sub xtra_BeforePrint(ByVal sender As System.Object, ByVal e As System.Drawing.Printing.PrintEventArgs) Handles MyBase.BeforePrint
        'Dim sTest = StrConv(sUserID, VbStrConv.ProperCase)

        ''lTANGGAL.Text =  "Printed on " & Now.ToString("dd/MM/yyy HH:mm") & " by " & sTest

        'lTANGGAL.Text =  "Printed on "
        'lBy.Text = " by " & sTest

        'If sKetPerusahaan = "PERUSAHAAN" Or sKetPerusahaan = "MCU" Then
        '    lPerusahaan.Text = sKetPerusahaan & " \ " & sPerusahaan
        'Else
        '    lPerusahaan.Text = sKetPerusahaan
        'End If

    End Sub
End Class