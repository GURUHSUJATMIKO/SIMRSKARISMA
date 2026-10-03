Imports System.Drawing.Printing
'Imports QRCoder
Imports DataAccess


Public Class xtraReportLAPORANOPERASI
    'Private oSignatureURL As New TandaTangan.clsTandaTangan
    'Dim sTTDUserSKRumkit As String

    Private Sub xtraReportLAPORANOPERASI_BeforePrint(sender As Object, e As PrintEventArgs) Handles MyBase.BeforePrint
        txtNAMA.Text = NAMA & " (" & JENISKELAMIN & ")"
        'txtJENISKELAMIN.Text = JENISKELAMIN
        txtTANGGALLAHIR.Text = TANGGALLAHIR

        lblUsia.Text = USIA
        'Try
        '    sTTDUserSKRumkit = oSignatureURL.fn_TandaTanganByKodeKunjungan("5",GetCurrentColumnValue("KDKUNJUNGAN").ToString())
        'Catch ex As Exception

        'End Try
        '

        Dim value1 As Object = GetCurrentColumnValue("TextEdit13")
        If value1 IsNot Nothing Then
            If CDate(value1.ToString()).ToString("HH:mm") = "00:00" Then
                lblJamMulaiAnestesi.Text = "-"
            Else
                lblJamMulaiAnestesi.Text = CDate(value1.ToString()).ToString("HH:mm")
            End If
        End If

    End Sub

    Private Sub BarcodeImplan_BeforePrint(sender As Object, e As PrintEventArgs)
        'Try
        '    Dim bm As New Bitmap(sBarcodeImplan)
        '    BarcodeImplan.Image = bm

        'Catch ex As Exception
        '    BarcodeImplan.Visible = False
        'End Try
    End Sub

    Private Sub picUser_BeforePrint(sender As Object, e As Printing.PrintEventArgs)


        'Try
        '    Dim gen As New QRCodeGenerator
        '    Dim data = gen.CreateQrCode(sTTDUserSKRumkit, QRCodeGenerator.ECCLevel.Q)
        '    Dim code As New QRCode(data)
        '    picUser.Image = code.GetGraphic(6)


        'Catch ex As Exception
        'End Try


    End Sub

    Private Sub picDokter_BeforePrint(sender As Object, e As PrintEventArgs)
        'Try
        '    Dim gen As New QRCodeGenerator
        '    Dim data = gen.CreateQrCode(sTTDUserSKRumkit, QRCodeGenerator.ECCLevel.Q)
        '    Dim code As New QRCode(data)
        '    picDokter.Image = code.GetGraphic(6)
        'Catch ex As Exception

        'End Try
    End Sub


End Class