Imports DataAccess

Public Class xtraSuratRujukBalik
    Private Sub xtra_BeforePrint(ByVal sender As System.Object, ByVal e As System.Drawing.Printing.PrintEventArgs) Handles MyBase.BeforePrint
        lblLabel_2.Text = sCompany
        deDate.Text = Now.ToString("dd/MM/yyyy")
        Jam.Text = Now.ToString("HH:mm:ss") & " Wib"

        Dim oSetKoneksi As New Brigging.clsSetKoneksi
        Dim imageData = oSetKoneksi.GetDataAktive

        If imageData IsNot Nothing Then
            Dim sCast = ByteArrayToImage(imageData.GAMBAR.ToArray())

            'Dim img As Image = CType(ic.ConvertFrom(sCast), Image)
            picGAMBAR.Image = sCast
        End If
    End Sub
    Private Function ByteArrayToImage(ByVal byteArrayIn() As Byte) As Image
        Using ms As New System.IO.MemoryStream(byteArrayIn)
            Dim returnImage = Image.FromStream(ms)
            Return returnImage
        End Using
    End Function
    Private Sub xtraTrackingPasien_PrintProgress(sender As System.Object, e As DevExpress.XtraPrinting.PrintProgressEventArgs) Handles MyBase.PrintProgress
        If e.PrintAction = Printing.PrintAction.PrintToFile Or e.PrintAction = Printing.PrintAction.PrintToPrinter Then
            sCetakSEP = True
        End If
    End Sub

End Class