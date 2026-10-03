Public Class xtraReportEMedrekRI_40
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
    Private Sub GroupFooter1_BeforePrint(sender As Object, e As Printing.PrintEventArgs) Handles GroupFooter1.BeforePrint
        Dim cek416 As Boolean = Report.GetCurrentColumnValue("ASESMEN_416")
        Dim cek417 As Boolean = Report.GetCurrentColumnValue("ASESMEN_417")
        Dim cek418 As Boolean = Report.GetCurrentColumnValue("ASESMEN_418")
        Dim cek419 As Boolean = Report.GetCurrentColumnValue("ASESMEN_419")
        Dim cek420 As Boolean = Report.GetCurrentColumnValue("ASESMEN_420")
        Dim cek421 As Boolean = Report.GetCurrentColumnValue("ASESMEN_421")
        Dim cek422 As Boolean = Report.GetCurrentColumnValue("ASESMEN_422")
        Dim cek423 As Boolean = Report.GetCurrentColumnValue("ASESMEN_423")
        Dim cek424 As Boolean = Report.GetCurrentColumnValue("ASESMEN_424")
        Dim cek425 As Boolean = Report.GetCurrentColumnValue("ASESMEN_425")
        Dim cek426 As Boolean = Report.GetCurrentColumnValue("ASESMEN_426")
        Dim cek427 As Boolean = Report.GetCurrentColumnValue("ASESMEN_427")
        Dim cek428 As Boolean = Report.GetCurrentColumnValue("ASESMEN_428")
        Dim cek429 As Boolean = Report.GetCurrentColumnValue("ASESMEN_429")
        Dim cek430 As Boolean = Report.GetCurrentColumnValue("ASESMEN_430")
        Dim cek431 As Boolean = Report.GetCurrentColumnValue("ASESMEN_431")
        Dim cek432 As Boolean = Report.GetCurrentColumnValue("ASESMEN_432")
        Dim cek433 As Boolean = Report.GetCurrentColumnValue("ASESMEN_433")
        Dim cek434 As Boolean = Report.GetCurrentColumnValue("ASESMEN_434")
        Dim cek435 As Boolean = Report.GetCurrentColumnValue("ASESMEN_435")
        Dim cek436 As Boolean = Report.GetCurrentColumnValue("ASESMEN_436")
        Dim cek437 As Boolean = Report.GetCurrentColumnValue("ASESMEN_437")
        Dim cek438 As Boolean = Report.GetCurrentColumnValue("ASESMEN_438")
        Dim cek439 As String = Report.GetCurrentColumnValue("ASESMEN_439")


        If cek416 = False And
           cek417 = False And
           cek418 = False And
           cek419 = False And
           cek420 = False And
           cek421 = False And
           cek422 = False And
           cek423 = False And
           cek424 = False And
           cek425 = False And
           cek426 = False And
           cek427 = False And
           cek428 = False And
           cek429 = False And
           cek430 = False And
           cek431 = False And
           cek432 = False And
           cek433 = False And
           cek434 = False And
           cek435 = False And
           cek436 = False And
           cek437 = False And
           cek438 = False And
           cek439 = "" Then
            GroupFooter1.Visible = False
        Else
            GroupFooter1.Visible = True
        End If
    End Sub

    Private Sub GroupFooter2_BeforePrint(sender As Object, e As Printing.PrintEventArgs) Handles GroupFooter2.BeforePrint
        Dim cek1 As Boolean = Report.GetCurrentColumnValue("MASALAHKEP_1")
        Dim cek2 As Boolean = Report.GetCurrentColumnValue("MASALAHKEP_2")
        Dim cek3 As Boolean = Report.GetCurrentColumnValue("MASALAHKEP_3")
        Dim cek4 As Boolean = Report.GetCurrentColumnValue("MASALAHKEP_4")
        Dim cek5 As Boolean = Report.GetCurrentColumnValue("MASALAHKEP_5")
        Dim cek6 As Boolean = Report.GetCurrentColumnValue("MASALAHKEP_6")
        Dim cek7 As Boolean = Report.GetCurrentColumnValue("MASALAHKEP_7")
        Dim cek8 As Boolean = Report.GetCurrentColumnValue("MASALAHKEP_8")
        Dim cek9 As Boolean = Report.GetCurrentColumnValue("MASALAHKEP_9")
        Dim cek10 As Boolean = Report.GetCurrentColumnValue("MASALAHKEP_10")
        Dim cek11 As Boolean = Report.GetCurrentColumnValue("MASALAHKEP_11")
        Dim cek12 As Boolean = Report.GetCurrentColumnValue("MASALAHKEP_12")
        Dim cek13 As Boolean = Report.GetCurrentColumnValue("MASALAHKEP_13")
        Dim cek14 As Boolean = Report.GetCurrentColumnValue("MASALAHKEP_14")
        Dim cek15 As Boolean = Report.GetCurrentColumnValue("MASALAHKEP_15")
        Dim cek16 As Boolean = Report.GetCurrentColumnValue("MASALAHKEP_16")
        Dim cek17 As Boolean = Report.GetCurrentColumnValue("MASALAHKEP_17")
        Dim cek18 As Boolean = Report.GetCurrentColumnValue("MASALAHKEP_18")
        Dim cek19 As Boolean = Report.GetCurrentColumnValue("MASALAHKEP_19")
        Dim cek20 As Boolean = Report.GetCurrentColumnValue("MASALAHKEP_20")
        Dim cek21 As Boolean = Report.GetCurrentColumnValue("MASALAHKEP_21")
        Dim cek22 As Boolean = Report.GetCurrentColumnValue("MASALAHKEP_22")
        Dim cek23 As Boolean = Report.GetCurrentColumnValue("MASALAHKEP_23")
        Dim cek24 As Boolean = Report.GetCurrentColumnValue("MASALAHKEP_24")
        Dim cek25 As Boolean = Report.GetCurrentColumnValue("MASALAHKEP_25")
        Dim cek26 As Boolean = Report.GetCurrentColumnValue("MASALAHKEP_26")
        Dim cek27 As Boolean = Report.GetCurrentColumnValue("MASALAHKEP_27")
        Dim cek28 As Boolean = Report.GetCurrentColumnValue("MASALAHKEP_28")
        Dim cek29 As Boolean = Report.GetCurrentColumnValue("MASALAHKEP_29")
        Dim cek30 As Boolean = Report.GetCurrentColumnValue("MASALAHKEP_30")
        Dim cek31 As Boolean = Report.GetCurrentColumnValue("MASALAHKEP_31")
        Dim cek32 As Boolean = Report.GetCurrentColumnValue("MASALAHKEP_32")
        Dim cek33 As Boolean = Report.GetCurrentColumnValue("MASALAHKEP_33")
        Dim cek34 As Boolean = Report.GetCurrentColumnValue("MASALAHKEP_34")
        Dim cek35 As Boolean = Report.GetCurrentColumnValue("MASALAHKEP_35")
        Dim cek36 As Boolean = Report.GetCurrentColumnValue("MASALAHKEP_36")
        Dim cek37 As Boolean = Report.GetCurrentColumnValue("MASALAHKEP_37")
        Dim cek38 As Boolean = Report.GetCurrentColumnValue("MASALAHKEP_38")
        Dim cek39 As Boolean = Report.GetCurrentColumnValue("MASALAHKEP_39")
        Dim cek40 As Boolean = Report.GetCurrentColumnValue("MASALAHKEP_40")
        Dim cek41 As Boolean = Report.GetCurrentColumnValue("MASALAHKEP_41")
        Dim cek42 As Boolean = Report.GetCurrentColumnValue("MASALAHKEP_42")
        Dim cek43 As Boolean = Report.GetCurrentColumnValue("MASALAHKEP_43")
        Dim cek44 As Boolean = Report.GetCurrentColumnValue("MASALAHKEP_44")
        Dim cektext As String = Report.GetCurrentColumnValue("MASALAHKEP_44_TEXT")

        If cek1 = False And
            cek2 = False And
            cek3 = False And
            cek4 = False And
            cek5 = False And
            cek6 = False And
            cek7 = False And
            cek8 = False And
            cek9 = False And
            cek10 = False And
            cek11 = False And
            cek12 = False And
            cek13 = False And
            cek14 = False And
            cek15 = False And
            cek16 = False And
            cek17 = False And
            cek18 = False And
            cek19 = False And
            cek20 = False And
            cek21 = False And
            cek22 = False And
            cek23 = False And
            cek24 = False And
            cek25 = False And
            cek26 = False And
            cek27 = False And
            cek28 = False And
            cek29 = False And
            cek30 = False And
            cek31 = False And
            cek32 = False And
            cek33 = False And
            cek34 = False And
            cek35 = False And
            cek36 = False And
            cek37 = False And
            cek38 = False And
            cek39 = False And
            cek40 = False And
            cek41 = False And
            cek42 = False And
            cek43 = False And
            cek44 = False And
            cektext = "" Then
            GroupFooter2.Visible = False
        Else
            GroupFooter2.Visible = True
        End If
    End Sub
End Class