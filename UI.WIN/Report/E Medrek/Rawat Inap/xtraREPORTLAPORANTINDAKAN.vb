Imports System.Drawing.Printing
'Imports QRCoder
Imports DataAccess


Public Class xtraREPORTLAPORANTINDAKAN
    'Private oSignatureURL As New TandaTangan.clsTandaTangan
    'Dim sTTDUserSKRumkit As String

    Private Sub xtraReportLAPORANOPERASI_BeforePrint(sender As Object, e As PrintEventArgs) Handles MyBase.BeforePrint
        txtNAMA.Text = NAMA & " (" & JENISKELAMIN & ")"
        'txtJENISKELAMIN.Text = JENISKELAMIN
        txtTANGGALLAHIR.Text = TANGGALLAHIR

        lblUsia.Text = USIA

        Dim sJENISANESTESI As Boolean = False
        Dim sPEMERIKSAANPA As Boolean = False
        Dim sPEMERIKSAANCAIRAN As Boolean = False
        Dim sPEMERIKSAANIMPLAN As Boolean = False

        Dim value1 As Object = GetCurrentColumnValue("CheckEdit7")
        Dim value2 As Object = GetCurrentColumnValue("CheckEdit8")
        Dim value3 As Object = GetCurrentColumnValue("CheckEdit9")
        Dim value4 As Object = GetCurrentColumnValue("CheckEdit10")
        Dim value5 As Object = GetCurrentColumnValue("CheckEdit17")
        Dim value6 As Object = GetCurrentColumnValue("CheckEdit18")
        Dim value7 As Object = GetCurrentColumnValue("CheckEdit19")
        Dim value8 As Object = GetCurrentColumnValue("CheckEdit20")
        Dim value9 As Object = GetCurrentColumnValue("CheckEdit1")
        Dim value10 As Object = GetCurrentColumnValue("CheckEdit2")

        If value1 IsNot Nothing Then
            sJENISANESTESI = CBool(value1.ToString())
        End If
        If value2 IsNot Nothing Then
            sJENISANESTESI = CBool(value2.ToString())
        End If
        If value3 IsNot Nothing Then
            sJENISANESTESI = CBool(value3.ToString())
        End If
        If value4 IsNot Nothing Then
            sJENISANESTESI = CBool(value4.ToString())
        End If
        If value5 IsNot Nothing Then
            sPEMERIKSAANPA = CBool(value5.ToString())
        End If
        If value6 IsNot Nothing Then
            sPEMERIKSAANPA = CBool(value6.ToString())
        End If
        If value7 IsNot Nothing Then
            sPEMERIKSAANCAIRAN = CBool(value7.ToString())
        End If
        If value8 IsNot Nothing Then
            sPEMERIKSAANCAIRAN = CBool(value8.ToString())
        End If
        If value9 IsNot Nothing Then
            sPEMERIKSAANIMPLAN = CBool(value9.ToString())
        End If
        If value10 IsNot Nothing Then
            sPEMERIKSAANIMPLAN = CBool(value10.ToString())
        End If

        If sJENISANESTESI = False Then
            lblJenisOperasi_1.Visible = False
            lblJenisOperasi_2.Visible = False
            lblJenisOperasi_3.Visible = False
            lblJenisOperasi_4.Visible = False
        End If
        If sPEMERIKSAANPA = False Then
            lblPemeriksaanPA_1.Visible = False
            lblPemeriksaanPA_2.Visible = False
        End If
        If sPEMERIKSAANCAIRAN = False Then
            lblPemeriksaanCairan_1.Visible = False
            lblPemeriksaanCairan_2.Visible = False
        End If
        If sPEMERIKSAANIMPLAN = False Then
            lblPemeriksaanImpan_1.Visible = False
            lblPemeriksaanImpan_2.Visible = False
        End If

        Dim valueWaktu As Object = GetCurrentColumnValue("TextEdit13")
        If valueWaktu IsNot Nothing Then
            If CDate(valueWaktu.ToString()).ToString("HH:mm") = "00:00" Then
                lblJamMulaiAnestesi.Text = "-"
            Else
                lblJamMulaiAnestesi.Text = CDate(valueWaktu.ToString()).ToString("HH:mm")
            End If
        End If
    End Sub
End Class