Public Class xtraReportEMedrekRI_13
    Private Sub xtra_BeforePrint(ByVal sender As System.Object, ByVal e As System.Drawing.Printing.PrintEventArgs) Handles MyBase.BeforePrint
        txtNAMA.Text = NAMA
        txtJENISKELAMIN.Text = JENISKELAMIN
        txtTANGGALLAHIR.Text = TANGGALLAHIR

        'Try
        '    Dim NewCopy As String = sAlamatTandaTanganDokter & sKDUSER_TTD & "_TTD" & ".png"
        '    'Dim NewCopyCap As String = sAlamatTandaTanganDokter & sKDUSER_TTD & "_CAP" & ".png"

        '    If sKDUSER_TTD <> "" Then
        '        If FileIO.FileSystem.FileExists(NewCopy) Then
        '            XrPictureBox1.Image = GetImageFromURL(NewCopy)
        '        End If

        '        'If FileIO.FileSystem.FileExists(NewCopyCap) Then
        '        '    XrPictureBox4.Image = GetImageFromURL(NewCopyCap)
        '        'End If
        '    End If
        'Catch ex As Exception

        'End Try
    End Sub
    Private Sub picDokter_BeforePrint(sender As Object, e As Printing.PrintEventArgs)

    End Sub
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