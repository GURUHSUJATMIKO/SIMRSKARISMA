Imports DataAccess

Public Class xtraDigital_CPPT_01_RawatInapNew
    Public Property DetailReport As Object

    Private Sub xtra_BeforePrint(ByVal sender As System.Object, ByVal e As System.Drawing.Printing.PrintEventArgs) Handles MyBase.BeforePrint
        Try
            lblUMUR.Text = sUmurPasienDiCPPT
        Catch ex As Exception

        End Try
    End Sub
    Private Sub Detail_BeforePrint(ByVal sender As Object, ByVal e As System.Drawing.Printing.PrintEventArgs) Handles Detail.BeforePrint
        'Try
        '    If GetCurrentColumnValue("KDUSER") <> "" Then
        '        If GetCurrentColumnValue("PROFESI") = "DOKTER" Then
        '            'Dim NewCopydOKTER As String = "\\192.168.2.223\Users\SIMRS\δupload berkas rmδ\Cap\" & GetCurrentColumnValue("KDUSER") & "_TTD" & ".png"
        '            Dim NewCopydOKTER As String = sAlamatTandaTanganDokter & GetCurrentColumnValue("KDUSER") & "_TTD" & ".png"
        '            If FileIO.FileSystem.FileExists(NewCopydOKTER) Then
        '                XrPictureBox3.Image = GetImageFromURL(NewCopydOKTER)
        '            End If
        '        Else
        '            'PERAWAT
        '            Dim oPerawat As New Reference.clsUnit
        '            Dim dsUnit = oPerawat.GetDataByName(GetCurrentColumnValue("KDUSER"))
        '            If dsUnit IsNot Nothing Then
        '                Dim NewCopyPERAWAT As String = sAlamatTandaTanganPerawat & dsUnit.KDUNIT & ".png"

        '                If FileIO.FileSystem.FileExists(NewCopyPERAWAT) Then
        '                    XrPictureBox3.Image = GetImageFromURL(NewCopyPERAWAT)
        '                End If
        '            End If
        '        End If
        '    End If

        '    If GetCurrentColumnValue("NIK") <> "" Then
        '        Dim NewCopydOKTERDPJPVERIF As String = sAlamatTandaTanganDokter & GetCurrentColumnValue("NIK") & "_TTD" & ".png"

        '        If FileIO.FileSystem.FileExists(NewCopydOKTERDPJPVERIF) Then
        '            XrPictureBox4.Image = GetImageFromURL(NewCopydOKTERDPJPVERIF)
        '        End If
        '    End If
        'Catch oErr As Exception
        '    MsgBox("Cek data Tanda Tangan Dokter: " & GetCurrentColumnValue("KDUSER") & "_TTD" & ".png" & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        'End Try

        If GetCurrentColumnValue("NIK") <> "" Then
            lblQRVerifikasi.Visible = True
        Else
            lblQRVerifikasi.Visible = False
        End If
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