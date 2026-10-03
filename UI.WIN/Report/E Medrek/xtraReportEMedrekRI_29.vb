Public Class xtraReportEMedrekRI_29
    Private Sub xtra_BeforePrint(ByVal sender As System.Object, ByVal e As System.Drawing.Printing.PrintEventArgs) Handles MyBase.BeforePrint
        Try
            'Dim gen As New QRCodeGenerator
            'Dim data = gen.CreateQrCode(sTandaTanganUser, QRCodeGenerator.ECCLevel.Q)
            'Dim code As New QRCode(data)
            'picPetugas.Image = code.GetGraphic(6)

            lblNama.Text = NAMA
            lblTanggalLahir.Text = TANGGALLAHIR
        Catch ex As Exception

        End Try

        'Try
        '    Dim value1 As Object = GetCurrentColumnValue("KDUSER")
        '    'XrLabel2.Text = sWAKTU
        '    Dim NewCopy As String = sAlamatTandaTanganDokter & value1.ToString() & "_TTD" & ".png"
        '    'Dim NewCopyCap As String = sAlamatTandaTanganDokter & sKDUSER_TTD & "_CAP" & ".png"

        '    If sKDUSER_TTD <> "" Then
        '        If FileIO.FileSystem.FileExists(NewCopy) Then
        '            XrPictureBox2.Image = GetImageFromURL(NewCopy)
        '        End If

        '        'If FileIO.FileSystem.FileExists(NewCopyCap) Then
        '        '    XrPictureBox4.Image = GetImageFromURL(NewCopyCap)
        '        'End If
        '    End If
        'Catch ex As Exception

        'End Try
    End Sub
    Private Function ByteArrayToImage(ByVal byteArrayIn() As Byte) As Image
        Using ms As New System.IO.MemoryStream(byteArrayIn)
            Dim returnImage = Image.FromStream(ms)
            Return returnImage
        End Using
    End Function
    Private Function GetImageFromURL(ByVal url As String) As Image
        Dim retVal As Image = Nothing

        If Not String.IsNullOrWhiteSpace(url) Then
            Dim req As System.Net.WebRequest = System.Net.WebRequest.Create(url.Trim)

            Using request As System.Net.WebResponse = req.GetResponse
                Using stream As System.IO.Stream = request.GetResponseStream
                    retVal = New Bitmap(System.Drawing.Image.FromStream(stream))
                End Using
            End Using
        End If

        Return retVal

    End Function
End Class