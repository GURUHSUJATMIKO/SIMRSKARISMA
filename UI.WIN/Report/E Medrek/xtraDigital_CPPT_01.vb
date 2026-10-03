Imports DataAccess

Public Class xtraDigital_CPPT_01
    Public Property DetailReport As Object
    Private Sub xtra_BeforePrint(ByVal sender As System.Object, ByVal e As System.Drawing.Printing.PrintEventArgs) Handles MyBase.BeforePrint
        Try
            'If AlamatGamabarCPPTRJ <> "" Then
            '    XrPictureBox3.Image = GetImageFromURL(AlamatGamabarCPPTRJ)
            'End If
        Catch ex As Exception

        End Try

    End Sub
    Private Sub Detail_BeforePrint(ByVal sender As Object, ByVal e As System.Drawing.Printing.PrintEventArgs) Handles Detail.BeforePrint
        Dim sProfesi As String = String.Empty
        'Dim sHasil_S As String = String.Empty
        'Dim sHasil_O As String = String.Empty
        'Dim sHasil_A As String = String.Empty
        'Dim sHasil_P As String = String.Empty
        'Dim sHasilINTRUSKI As String = String.Empty

        'Dim sUserTandatangan As String = String.Empty
        'Dim UserTandatangan As Object = GetCurrentColumnValue("KDUSER")

        'If UserTandatangan IsNot Nothing Then
        '    sUserTandatangan = UserTandatangan.ToString()

        '    Try
        '        Dim gen As New QRCodeGenerator
        '        Dim data = gen.CreateQrCode(sTandaTanganUser & sUserTandatangan, QRCodeGenerator.ECCLevel.Q)
        '        Dim code As New QRCode(data)
        '        picPetugas_1.Image = code.GetGraphic(6)
        '    Catch ex As Exception
        '        picPetugas_1.Visible = False
        '    End Try
        'End If

        'Dim NewCopy As String = "\\192.168.2.223\Users\SIMRS\δupload berkas rmδ\Cap\" & GetCurrentColumnValue("KDUSER") & "_TTD" & ".png"
        'XrPictureBox2

        Try
            Dim oReqAkhirPemeriksaan As New Transaksi.clsReqAkhirPemeriksaan
            Dim dsAlamatImage = oReqAkhirPemeriksaan.GetDataByPendaftaarn(GetCurrentColumnValue("KDPENDAFTARAN").ToString())

            If dsAlamatImage IsNot Nothing
                If dsAlamatImage.ALAMATGAMBAR <> ""
                    XrPictureBox2.Image = GetImageFromURL(dsAlamatImage.ALAMATGAMBAR)
                End If
            End If

        Catch ex As Exception

        End Try
        Try
            Dim NewCopy As String = sAlamatTandaTanganDokter & GetCurrentColumnValue("KDUSER") & "_TTD" & ".png"
            Dim NewCopyCap As String = sAlamatTandaTanganDokter & GetCurrentColumnValue("KDUSER") & "_CAP" & ".png"

            If GetCurrentColumnValue("KDUSER") <> "" Then
                If FileIO.FileSystem.FileExists(NewCopy) Then
                    XrPictureBox3.Image = GetImageFromURL(NewCopy)
                Else
                    'Dim oStaff As New Reference.clsDoctor

                    'Dim dsDoctor = oStaff.GetDataByKDUSER(GetCurrentColumnValue("KDUSER"))

                    'If dsDoctor IsNot Nothing Then
                    '    If dsDoctor.KDCUSTOMER <> "" Then
                    '        Try
                    '            Dim SaveImage As New Bitmap(GetImageFromURL(AlamatDownloadIamge1 & dsDoctor.KDCUSTOMER & "/" & dsDoctor.KDCUSTOMER & ".png"))
                    '            SaveImage.Save(NewCopy, Imaging.ImageFormat.Png)
                    '            SaveImage.Dispose()

                    '            XrPictureBox3.Image = GetImageFromURL(NewCopy)
                    '        Catch ex As Exception
                    '            XrPictureBox3.Image = GetImageFromURL(AlamatDownloadIamge1 & dsDoctor.KDCUSTOMER & "/" & dsDoctor.KDCUSTOMER & ".png")
                    '        End Try
                    '    End If
                    'End If
                End If

                If FileIO.FileSystem.FileExists(NewCopyCap) Then
                    XrPictureBox4.Image = GetImageFromURL(NewCopyCap)
                End If
            End If
        Catch ex As Exception

        End Try


        'Dim value1 As Object = DetailReport.GetCurrentColumnValue("PROFESI")

        'If value1 IsNot Nothing Then
        '    sProfesi = value1.ToString()
        'End If

        'If sProfesi = "DOKTER" Then
        '    XrTableCell58.ForeColor = Color.Black
        'Else
        '    XrTableCell58.ForeColor = Color.Blue
        'End If

        'Me.PictureEdit1.Image = Picture.ByteArrayToImage(CellValue1)

        'Dim value1 As Object = ("KDUSER")
        'Dim value2 As Object = DetailReport.GetCurrentColumnValue("INTRUKSI")
        'Dim valueS As Object = DetailReport.GetCurrentColumnValue("SOAP_S")
        'Dim valueO As Object = DetailReport.GetCurrentColumnValue("SOAP_O")
        'Dim valueA As Object = DetailReport.GetCurrentColumnValue("SOAP_A")
        'Dim valueP As Object = DetailReport.GetCurrentColumnValue("SOAP_P")

        'If value1 IsNot Nothing Then
        '    sProfesi = value1.ToString()

        '    If value2 IsNot Nothing Then
        '        sHasilINTRUSKI = value2.ToString()
        '    Else
        '        sHasilINTRUSKI = ""
        '    End If

        '    If valueS IsNot Nothing Then
        '        sHasil_S = valueS.ToString()
        '    Else
        '        sHasil_S = ""
        '    End If

        '    If valueO IsNot Nothing Then
        '        sHasil_O = valueO.ToString()
        '    Else
        '        sHasil_O = ""
        '    End If

        '    If valueA IsNot Nothing Then
        '        sHasil_A = valueA.ToString()
        '    Else
        '        sHasil_A = ""
        '    End If

        '    If valueP IsNot Nothing Then
        '        sHasil_P = valueP.ToString()
        '    Else
        '        sHasil_P = ""
        '    End If

        '    If sProfesi = "DOKTER" Then
        '        XrRichText_PROFESI.Text = sProfesi
        '        XrRichText_PROFESI.ForeColor = Color.Black

        '        XrRichText_INTRUKSI.Text = "Hasil Penunjang " & vbCrLf & sHasilINTRUSKI
        '        XrRichText_INTRUKSI.ForeColor = Color.Black

        '        XrRichText_S.Text = "S : " & vbCrLf & sHasil_S
        '        XrRichText_S.ForeColor = Color.Black

        '        XrRichText_O.Text = "O : " & vbCrLf & sHasil_O
        '        XrRichText_O.ForeColor = Color.Black

        '        XrRichText_A.Text = "A : " & vbCrLf & sHasil_A
        '        XrRichText_A.ForeColor = Color.Black

        '        XrRichText_P.Text = "P : " & vbCrLf & sHasil_P
        '        XrRichText_P.ForeColor = Color.Black
        '    ElseIf sProfesi = "GIZI" Then
        '        XrRichText_PROFESI.Text = sProfesi
        '        XrRichText_PROFESI.ForeColor = Color.Blue

        '        XrRichText_INTRUKSI.Text = "Hasil Penunjang " & sHasilINTRUSKI
        '        XrRichText_INTRUKSI.ForeColor = Color.Blue

        '        XrRichText_S.Text = "A " & vbCrLf & sHasil_S
        '        XrRichText_S.ForeColor = Color.Blue

        '        XrRichText_O.Text = "D " & vbCrLf & sHasil_O
        '        XrRichText_O.ForeColor = Color.Blue

        '        XrRichText_A.Text = "i " & vbCrLf & sHasil_A
        '        XrRichText_A.ForeColor = Color.Blue

        '        XrRichText_P.Text = "ME " & vbCrLf & sHasil_P
        '        XrRichText_P.ForeColor = Color.Blue
        '    Else
        '        XrRichText_PROFESI.Text = sProfesi
        '        XrRichText_PROFESI.ForeColor = Color.Blue

        '        XrRichText_INTRUKSI.Text = "Hasil Penunjang " & sHasilINTRUSKI
        '        XrRichText_INTRUKSI.ForeColor = Color.Blue

        '        XrRichText_S.Text = "S " & vbCrLf & sHasil_S
        '        XrRichText_S.ForeColor = Color.Blue

        '        XrRichText_O.Text = "O " & vbCrLf & sHasil_O
        '        XrRichText_O.ForeColor = Color.Blue

        '        XrRichText_A.Text = "A " & vbCrLf & sHasil_A
        '        XrRichText_A.ForeColor = Color.Blue

        '        XrRichText_P.Text = "P " & vbCrLf & sHasil_P
        '        XrRichText_P.ForeColor = Color.Blue
        '    End If
        'End If

        'Dim sUserTandatangan As String = String.Empty
        'Dim UserTandatangan As Object = DetailReport.GetCurrentColumnValue("KDUSER")

        'If UserTandatangan IsNot Nothing Then
        '    sUserTandatangan = UserTandatangan.ToString()

        '    Try
        '        Dim gen As New QRCodeGenerator
        '        Dim data = gen.CreateQrCode(sTandaTanganUser & sUserTandatangan, QRCodeGenerator.ECCLevel.Q)
        '        Dim code As New QRCode(data)
        '        picPetugas_1.Image = code.GetGraphic(6)
        '    Catch ex As Exception
        '        picPetugas_1.Visible = False
        '    End Try
        'End If

        'Dim sUserTandatangan_2 As String = String.Empty
        'Dim UserTandatangan_2 As Object = DetailReport.GetCurrentColumnValue("KDUSER_VERIFKASI")

        'If UserTandatangan_2 IsNot Nothing Then
        '    sUserTandatangan_2 = UserTandatangan_2.ToString()

        '    Try
        '        Dim gen As New QRCodeGenerator
        '        Dim data = gen.CreateQrCode(sTandaTanganUser & sUserTandatangan_2, QRCodeGenerator.ECCLevel.Q)
        '        Dim code As New QRCode(data)
        '        picPetugas_1.Image = code.GetGraphic(6)
        '    Catch ex As Exception
        '        picPetugas_1.Visible = False
        '    End Try
        'End If

        'Try
        '    Dim gen As New QRCodeGenerator
        '    Dim data = gen.CreateQrCode(sTandaTanganUser_2, QRCodeGenerator.ECCLevel.Q)
        '    Dim code As New QRCode(data)
        '    picPetugas_2.Image = code.GetGraphic(6)
        'Catch ex As Exception
        '    picPetugas_2.Visible = False
        'End Try

        'Dim oStaff As New Reference.clsDoctor

        'Dim dsDoctor = oStaff.GetData(sUserIDTandaTangan)

        'If dsDoctor IsNot Nothing Then
        '    If dsDoctor.KDCUSTOMER <> "" Then
        '        Try
        '            XrPictureBox3.Image = GetImageFromURL(AlamatDownloadIamge1 & dsDoctor.KDCUSTOMER & "/" & dsDoctor.KDCUSTOMER & ".png")
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