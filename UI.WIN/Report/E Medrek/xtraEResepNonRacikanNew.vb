Imports DataAccess

Public Class xtraEResepNonRacikanNew
    Private Sub xtra_BeforePrint(ByVal sender As System.Object, ByVal e As System.Drawing.Printing.PrintEventArgs) Handles MyBase.BeforePrint
        'lblMenjadi_tex.Text = lblMenjadi
        'lblTertulis_tex.Text = lblTertulis

        If DownloadIamge1 <> "" Then
            Try
                Dim img As Image = GetImageFromURL(DownloadIamge1)

                XrPictureBox2.Image = GetImageFromURL(DownloadIamge1)
            Catch ex As Exception

                XrPictureBox2.Visible = False
            End Try
        Else
            XrPictureBox2.Visible = False
        End If

        Dim oStaff As New Reference.clsDoctor

        Dim dsDoctor = oStaff.GetData(sUserIDTandaTangan)

        If dsDoctor IsNot Nothing Then
            If dsDoctor.KDCUSTOMER <> "" Then
                Try
                    XrPictureBox3.Image = GetImageFromURL(AlamatDownloadIamge1 & dsDoctor.KDCUSTOMER & "/" & dsDoctor.KDCUSTOMER & ".png")
                Catch ex As Exception

                End Try
            End If

            Try
                Dim sCast1 = ByteArrayToImage(dsDoctor.ATTACHMENT.ToArray())

                'Dim img As Image = CType(ic.ConvertFrom(sCast), Image)
                XrPictureBox5.Image = sCast1

            Catch ex As Exception

            End Try
        End If

        'XrRichText1.Font = New Font(XrRichText1.Font.Name(), 6, FontStyle.Regular)
        'XrRichText1.Font = New Font("Times New Roman", 6, FontStyle.Regular)
        'XrRichText1.SelectionFont = New Font("Tahoma", 12, FontStyle.Bold)
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