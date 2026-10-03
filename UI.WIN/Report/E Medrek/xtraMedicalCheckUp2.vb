Imports DataAccess

Public Class xtraMedicalCheckUp2
    Private Sub xtra_BeforePrint(ByVal sender As System.Object, ByVal e As System.Drawing.Printing.PrintEventArgs) Handles MyBase.BeforePrint
        Try
            'If DownloadIamge1 <> "" Then
            '    Try
            '        Dim img As Image = GetImageFromURL(DownloadIamge1)

            '        XrPictureBox2.Image = GetImageFromURL(DownloadIamge1)
            '    Catch ex As Exception

            '        XrPictureBox2.Visible = False

            '    End Try
            'Else
            '    XrPictureBox2.Visible = False
            'End If

            Dim NewCopy As String = sAlamatTandaTanganDokter & GetCurrentColumnValue("KDDOCTOR") & "_TTD" & ".png"
            Dim NewCopy_Cap As String = sAlamatTandaTanganDokter & GetCurrentColumnValue("KDDOCTOR") & "_CAP" & ".png"

            Dim oStaff As New Reference.clsDoctor

            If GetCurrentColumnValue("KDDOCTOR") <> "" Then
                If FileIO.FileSystem.FileExists(NewCopy) Then
                    XrPictureBox3.Image = GetImageFromURL(NewCopy)
                Else
                    Dim dsDoctor = oStaff.GetData(GetCurrentColumnValue("KDDOCTOR"))

                    If dsDoctor IsNot Nothing Then
                        If dsDoctor.KDCUSTOMER <> "" Then
                            Try
                                Dim SaveImage As New Bitmap(GetImageFromURL(AlamatDownloadIamge1 & dsDoctor.KDCUSTOMER & "/" & dsDoctor.KDCUSTOMER & ".png"))
                                SaveImage.Save(NewCopy, Imaging.ImageFormat.Png)
                                SaveImage.Dispose()

                                XrPictureBox3.Image = GetImageFromURL(NewCopy)
                            Catch ex As Exception
                                XrPictureBox3.Image = GetImageFromURL(AlamatDownloadIamge1 & dsDoctor.KDCUSTOMER & "/" & dsDoctor.KDCUSTOMER & ".png")
                            End Try

                        End If
                    End If
                End If

                If FileIO.FileSystem.FileExists(NewCopy_Cap) Then
                    XrPictureBox5.Image = GetImageFromURL(NewCopy_Cap)
                Else
                    Dim dsDoctor = oStaff.GetData(GetCurrentColumnValue("KDDOCTOR"))

                    If dsDoctor IsNot Nothing Then
                        Try
                            Dim SaveImage As New Bitmap(ByteArrayToImage(dsDoctor.ATTACHMENT.ToArray()))
                            SaveImage.Save(NewCopy_Cap, Imaging.ImageFormat.Png)
                            SaveImage.Dispose()

                            Dim sCast1 = ByteArrayToImage(dsDoctor.ATTACHMENT.ToArray())
                            XrPictureBox5.Image = GetImageFromURL(NewCopy_Cap)
                        Catch ex As Exception

                        End Try
                    End If
                End If

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