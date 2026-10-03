Imports DataAccess

Public Class xtraRingkasanKeluarRawatInap
    Private Sub xtra_BeforePrint(ByVal sender As System.Object, ByVal e As System.Drawing.Printing.PrintEventArgs) Handles MyBase.BeforePrint
        Try
            Dim sKODEPASIEN As String = String.Empty

            Dim value1 As Object = GetCurrentColumnValue("KDCUSTOMER")
            If value1 IsNot Nothing Then
                sKODEPASIEN = value1.ToString()

                DownloadIamge1 = "http://203.210.87.29/img/SIGNATURE/" & sKODEPASIEN & "/" & sKODEPASIEN & ".png"
                Dim img As Image = GetImageFromURL(DownloadIamge1)

                XrPictureBox2.Image = GetImageFromURL(DownloadIamge1)
            End If
        Catch ex As Exception
            XrPictureBox2.Visible = False
        End Try

        'If GetCurrentColumnValue("KDUSER") <> "" Then
        '    Dim oStaff As New Reference.clsDoctor

        '    Dim dsDoctor = oStaff.GetDataByKDUSER(GetCurrentColumnValue("KDUSER"))

        '    If dsDoctor IsNot Nothing Then
        '        If dsDoctor.KDCUSTOMER <> "" Then
        '            Try
        '                XrPictureBox3.Image = GetImageFromURL(AlamatDownloadIamge1 & dsDoctor.KDCUSTOMER & "/" & dsDoctor.KDCUSTOMER & ".png")
        '            Catch ex As Exception

        '            End Try
        '        End If
        '        Try
        '            Dim sCast1 = ByteArrayToImage(dsDoctor.ATTACHMENT.ToArray())
        '            XrPictureBox5.Image = sCast1
        '        Catch ex As Exception

        '        End Try
        '    End If
        'End If
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