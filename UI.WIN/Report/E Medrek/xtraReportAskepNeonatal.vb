Imports QRCoder

Public Class xtraReportAskepNeonatal
    Private Sub picPerawat_BeforePrint(sender As Object, e As Printing.PrintEventArgs) Handles picPerawat.BeforePrint
        Try
            If sTandaTanganPerawat <> "" Then
                Dim gen As New QRCodeGenerator
                Dim data = gen.CreateQrCode(sTandaTanganPerawat, QRCodeGenerator.ECCLevel.Q)
                Dim code As New QRCode(data)
                picPerawat.Image = code.GetGraphic(6)
            End If

        Catch ex As Exception

        End Try
    End Sub

    Private Sub picDokter_BeforePrint(sender As Object, e As Printing.PrintEventArgs) Handles picDokter.BeforePrint
        Try
            If sTandaTanganDokter <> "" Then
                Dim gen As New QRCodeGenerator
                Dim data = gen.CreateQrCode(sTandaTanganDokter, QRCodeGenerator.ECCLevel.Q)
                Dim code As New QRCode(data)
                picDokter.Image = code.GetGraphic(6)
            End If

        Catch ex As Exception

        End Try
    End Sub
End Class