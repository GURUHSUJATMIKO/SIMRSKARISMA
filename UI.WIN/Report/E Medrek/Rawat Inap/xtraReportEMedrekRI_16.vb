'Imports QRCoder
Public Class xtraReportEMedrekRI_16
    Private Sub xtra_BeforePrint(ByVal sender As System.Object, ByVal e As System.Drawing.Printing.PrintEventArgs) Handles MyBase.BeforePrint
        txtNAMA.Text = NAMA
        txtJENISKELAMIN.Text = JENISKELAMIN
        txtTANGGALLAHIR.Text = TANGGALLAHIR
    End Sub
    Private Sub picDPJP_BeforePrint(sender As Object, e As Printing.PrintEventArgs) Handles picDPJP.BeforePrint
        'Try
        '    If sTandaTanganDokter <> "" Then
        '        Dim gen As New QRCodeGenerator
        '        Dim data = gen.CreateQrCode(sTandaTanganDokter, QRCodeGenerator.ECCLevel.Q)
        '        Dim code As New QRCode(data)
        '        picDPJP.Image = code.GetGraphic(6)
        '    End If
        'Catch ex As Exception

        'End Try
    End Sub
    Private Sub picPeralihan_BeforePrint(sender As Object, e As Printing.PrintEventArgs) Handles picPeralihan.BeforePrint
        'Try
        '    If sTandaTanganDokterPeralihan <> "" Then
        '        Dim gen As New QRCodeGenerator
        '        Dim data = gen.CreateQrCode(sTandaTanganDokterPeralihan, QRCodeGenerator.ECCLevel.Q)
        '        Dim code As New QRCode(data)
        '        picPeralihan.Image = code.GetGraphic(6)
        '    End If
        'Catch ex As Exception

        'End Try
    End Sub
    Private Sub picDokter2_BeforePrint(sender As Object, e As Printing.PrintEventArgs) Handles picDokter2.BeforePrint
        'Try
        '    If sTandaTanganDokter2 <> "" Then
        '        Dim gen As New QRCodeGenerator
        '        Dim data = gen.CreateQrCode(sTandaTanganDokter2, QRCodeGenerator.ECCLevel.Q)
        '        Dim code As New QRCode(data)
        '        picDokter2.Image = code.GetGraphic(6)
        '    End If
        'Catch ex As Exception

        'End Try
    End Sub
End Class