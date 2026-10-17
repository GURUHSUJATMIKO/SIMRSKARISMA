Imports DataAccess

Public Class xtraMedicalCheckUp
    Private Sub xtra_BeforePrint(ByVal sender As System.Object, ByVal e As System.Drawing.Printing.PrintEventArgs) Handles MyBase.BeforePrint
        Try
            XrPictureBox2.Image = GetImageFromURL(GetCurrentColumnValue("MemoEdit37").ToString())
        Catch ex As Exception

        End Try

        Try

            Dim value1 As Object = GetCurrentColumnValue("KDMEDICALCHECKUP")
            Dim value2 As Object = GetCurrentColumnValue("DATE")

            If value1 IsNot Nothing Then
                Dim oKoding As New Digital.clsDigital_MedicalChekUp
                Dim ds = oKoding.GetData(value1.ToString())
                If ds IsNot Nothing Then
                    Dim NewCopy As String = sAlamatTandaTanganDokter & ds.KDDOCTOR & "_TTD" & ".png"
                    Dim NewCopy_Cap As String = sAlamatTandaTanganDokter & ds.KDDOCTOR & "_CAP" & ".png"

                    Dim oStaff As New Reference.clsDoctor

                    If ds.KDDOCTOR <> "" Then
                        If FileIO.FileSystem.FileExists(NewCopy) Then
                            XrPictureBox3.Image = GetImageFromURL(NewCopy)
                        Else
                            Dim dsDoctor = oStaff.GetData(ds.KDDOCTOR)

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
                            Dim dsDoctor = oStaff.GetData(ds.KDDOCTOR)

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

                End If
            End If
            If value2 IsNot Nothing Then
                lblTANGGAL.Text = "Bandung Barat, " & CDate(value2.ToString()).ToString("dd-MM-yyyy")
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