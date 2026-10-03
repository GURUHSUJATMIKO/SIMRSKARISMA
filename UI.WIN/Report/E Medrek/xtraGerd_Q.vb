Imports DataAccess

Public Class xtraGerd_Q
    Private Sub xtra_BeforePrint(ByVal sender As System.Object, ByVal e As System.Drawing.Printing.PrintEventArgs) Handles MyBase.BeforePrint
        If DownloadIamge1 <> "" Then
            Try
                Dim img As Image = GetImageFromURL(DownloadIamge1)

                'XrPictureBox2.Image = GetImageFromURL(DownloadIamge1)
            Catch ex As Exception

                'XrPictureBox2.Visible = False

            End Try
        Else
            'XrPictureBox2.Visible = False
        End If

        'Dim oStaff As New Reference.clsDoctor

        'Dim dsDoctor = oStaff.GetData(sUserIDTandaTangan)

        'If dsDoctor IsNot Nothing Then
        '    If dsDoctor.KDCUSTOMER <> "" Then
        '        Try
        '            XrPictureBox3.Image = GetImageFromURL(AlamatDownloadIamge1 & dsDoctor.KDCUSTOMER & "/" & dsDoctor.KDCUSTOMER & ".png")
        '        Catch ex As Exception

        '        End Try
        '    End If
        '    Try
        '        Dim sCast1 = ByteArrayToImage(dsDoctor.ATTACHMENT.ToArray())

        '        'Dim img As Image = CType(ic.ConvertFrom(sCast), Image)
        '        XrPictureBox5.Image = sCast1

        '    Catch ex As Exception

        '    End Try
        'End If

        Try
            XrLabel2.Text = sWAKTU
            Dim NewCopy As String = sAlamatTandaTanganDokter & sKDUSER_TTD & "_TTD" & ".png"
            'Dim NewCopyCap As String = sAlamatTandaTanganDokter & sKDUSER_TTD & "_CAP" & ".png"

            If sKDUSER_TTD <> "" Then
                If FileIO.FileSystem.FileExists(NewCopy) Then
                    XrPictureBox2.Image = GetImageFromURL(NewCopy)
                End If

                'If FileIO.FileSystem.FileExists(NewCopyCap) Then
                '    XrPictureBox4.Image = GetImageFromURL(NewCopyCap)
                'End If
            End If
        Catch ex As Exception

        End Try
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