Imports QRCoder
Imports DataAccess

Public Class xtraReportEMedrekRI_40_New
    Private oResumeRawatJalan As New Digital.clsResumeRawatJalan

    Private Sub picPetugas_BeforePrint(sender As Object, e As Printing.PrintEventArgs) Handles picPetugas.BeforePrint
        Try
            Dim sUserTandatangan As String = String.Empty
            Dim UserTandatangan As Object = Me.GetCurrentColumnValue("KDUSER")

            Dim gen As New QRCodeGenerator
            Dim data = gen.CreateQrCode(UserTandatangan, QRCodeGenerator.ECCLevel.Q)
            Dim code As New QRCode(data)
            picPetugas.Image = code.GetGraphic(6)
        Catch ex As Exception
            picPetugas.Visible = False
        End Try
    End Sub


    Private Sub pic1_BeforePrint(sender As Object, e As Printing.PrintEventArgs) Handles pic1.BeforePrint
        Dim dsResumeRJ = oResumeRawatJalan.GetDataCodeResumeRJ(sKDRESUMERAWATJALAN)

        If dsResumeRJ IsNot Nothing Then
            If sTextLBP = "" Then
                Dim oSignature As New Identitas.clsSet_Signature

                Dim dsSignature = oSignature.GetData(dsResumeRJ.KDKUNJUNGAN & "LBP")
                If dsSignature IsNot Nothing Then
                    If dsSignature.ALAMAT_URL <> "" Then

                        Dim DownloadIamge1 As String = "http://172.165.115.222:86/" & dsSignature.ALAMAT_URL
                        Dim img As Image = GetImageFromURL(DownloadIamge1)
                        pic1.Image = GetImageFromURL(DownloadIamge1)
                    End If
                End If
            Else
                Dim gen As New QRCodeGenerator
                Dim data = gen.CreateQrCode(dsResumeRJ.R_IDENTITAS_PASIEN.KARTUBPJS, QRCodeGenerator.ECCLevel.Q)
                Dim code As New QRCode(data)
                pic1.Image = code.GetGraphic(6)
            End If
        End If
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