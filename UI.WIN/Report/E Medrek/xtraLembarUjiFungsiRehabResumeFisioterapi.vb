Imports DataAccess

Public Class xtraLembarUjiFungsiRehabResumeFisioterapi
    Private Sub xtraCashIn_BeforePrint(sender As Object, e As System.Drawing.Printing.PrintEventArgs) Handles Me.BeforePrint
        Try
            'Dim value1 As Object = GetCurrentColumnValue("KDPENDAFTARAN")
            Dim value2 As Object = GetCurrentColumnValue("DATE")
            Dim valueUser As Object = GetCurrentColumnValue("KDUSER")

            If sKDDOCTOR_PENDAFTARAN <> "" Then
                Dim oStaff As New Reference.clsDoctor

                Dim NewCopy As String = sAlamatTandaTanganDokter & sKDDOCTOR_PENDAFTARAN & "_TTD" & ".png"
                Dim NewCopy_Cap As String = sAlamatTandaTanganDokter & sKDDOCTOR_PENDAFTARAN & "_CAP" & ".png"

                If FileIO.FileSystem.FileExists(NewCopy) Then
                    XrPictureBox3.Image = GetImageFromURL(NewCopy)
                Else
                    Dim dsDoctor = oStaff.GetData(sKDDOCTOR_PENDAFTARAN)

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
                    Dim dsDoctor = oStaff.GetData(sKDDOCTOR_PENDAFTARAN)

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
            If value2 IsNot Nothing Then
                lblTANGGAL1.Text = "Bandung Barat, " & CDate(value2.ToString()).ToString("dd-MM-yyyy")
            End If

            If valueUser IsNot Nothing Then
                Dim oUnit As New Reference.clsUnit
                Dim dsUnit = oUnit.GetDataByKdUser(valueUser.ToString())
                If dsUnit IsNot Nothing Then
                    Dim NewCopy_Cap As String = sAlamatTandaTanganPerawat & dsUnit.KDUNIT & ".png"

                    If dsUnit.KDCUSTOMER <> "" Then
                        Dim NewCopy As String = sAlamatTandaTanganPerawat & dsUnit.KDUNIT & "_TTDPERAWAT" & ".png"

                        If FileIO.FileSystem.FileExists(NewCopy) Then
                            XrPictureBox1.Image = GetImageFromURL(NewCopy)
                        Else
                            Try
                                Dim SaveImage As New Bitmap(GetImageFromURL(AlamatDownloadIamge1 & dsUnit.KDCUSTOMER & "/" & dsUnit.KDCUSTOMER & ".png"))
                                SaveImage.Save(NewCopy, Imaging.ImageFormat.Png)
                                SaveImage.Dispose()

                                XrPictureBox1.Image = GetImageFromURL(NewCopy)
                            Catch ex As Exception
                                XrPictureBox1.Image = Nothing
                            End Try
                        End If
                    End If

                    If FileIO.FileSystem.FileExists(NewCopy_Cap) Then
                        XrPictureBox4.Image = GetImageFromURL(NewCopy_Cap)
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