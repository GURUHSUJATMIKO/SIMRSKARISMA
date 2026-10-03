'Imports QRCoder

Public Class xtraReportEMedrekRJ_36
    Private Sub xtra_BeforePrint(ByVal sender As System.Object, ByVal e As System.Drawing.Printing.PrintEventArgs) Handles MyBase.BeforePrint
        txtNAMA.Text = NAMA
        txtJENISKELAMIN.Text = JENISKELAMIN
        txtTANGGALLAHIR.Text = TANGGALLAHIR
        'If sAttacment_1 IsNot Nothing Then
        '    Dim sCast = ByteArrayToImage(sAttacment_1.ToArray())

        '    'Dim img As Image = CType(ic.ConvertFrom(sCast), Image)
        '    lPicture2.Image = sCast
        'End If
    End Sub

    Private Function ByteArrayToImage(ByVal byteArrayIn() As Byte) As Image
        Using ms As New System.IO.MemoryStream(byteArrayIn)
            Dim returnImage = Image.FromStream(ms)
            Return returnImage
        End Using
    End Function

    Private Sub lPicture2_BeforePrint(sender As Object, e As Printing.PrintEventArgs) Handles lPicture2.BeforePrint
        'If sAttacment_1 IsNot Nothing Then
        '    Dim sCast = ByteArrayToImage(sAttacment_1.ToArray())

        '    'Dim img As Image = CType(ic.ConvertFrom(sCast), Image)
        '    lPicture2.Image = sCast
        'End If
    End Sub

    Private Sub picDokter_BeforePrint(sender As Object, e As Printing.PrintEventArgs) Handles picDokter.BeforePrint
        'Try
        '    Dim gen As New QRCodeGenerator
        '    Dim data = gen.CreateQrCode(sTandaTanganUser, QRCodeGenerator.ECCLevel.Q)
        '    Dim code As New QRCode(data)
        '    picDokter.Image = code.GetGraphic(6)
        'Catch ex As Exception
        '    picDokter.Visible = False
        'End Try
    End Sub
End Class