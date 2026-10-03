Imports System.Linq
Imports DevExpress.XtraReports.UI
Imports DataAccess

Public Class xtraReportAskepRawatJalan
    'Dim oDiagnosa As New EMedrek.clsS_DIGITAL_ASKEP_RAWATJALAN
    Private Sub xtra_BeforePrint(ByVal sender As System.Object, ByVal e As System.Drawing.Printing.PrintEventArgs) Handles MyBase.BeforePrint
        'If sPictureLogo IsNot Nothing Then
        '    XrPictureBox11.Image = sPictureLogo
        'End If
    End Sub
    Private Sub GroupHeader1_BeforePrint(sender As Object, e As Printing.PrintEventArgs) Handles GroupHeader1.BeforePrint
        Dim sNOIDASKEP As String
        Dim sKdItemDiagnosa As String
        Dim cek As String

        Try
            sNOIDASKEP = Report.GetCurrentColumnValue("KDASESMEN")
            sKdItemDiagnosa = DetailReport.GetCurrentColumnValue("KDITEMDIAGNOSAPERAWAT")
            cek = DetailReport.GetCurrentColumnValue("CEK1")
            If cek = False Then
                e.Cancel = True
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub cellDiagnosa_BeforePrint(sender As Object, e As Printing.PrintEventArgs) Handles cellDiagnosa.BeforePrint
        'Dim sNOIDASKEP As String
        'Dim sKdItemDiagnosa As String

        'Try
        '    sNOIDASKEP = Report.GetCurrentColumnValue("KDASESMEN")
        '    sKdItemDiagnosa = DetailReport.GetCurrentColumnValue("KDITEMDIAGNOSAPERAWAT")
        '    Dim listHasil As New List(Of String)
        '    Dim listHasil2 As New List(Of String)
        '    Dim dsDiagnosa = oDiagnosa.GetData(sNOIDASKEP)
        '    Dim strdiagnosa As String = ""
        '    Dim count As String = ""

        '    If dsDiagnosa IsNot Nothing Then
        '        For Each xloop In oDiagnosa.GetDataDetail(sNOIDASKEP).Where(Function(x) (x.DIAGNOSA_KEPERAWATAN.Contains(":") Or x.CEK1 = True) And x.KDITEMDIAGNOSAPERAWAT = sKdItemDiagnosa)
        '            If xloop.DIAGNOSA_KEPERAWATAN.Contains("b.d") Or xloop.DIAGNOSA_KEPERAWATAN.Contains("dibuktikan dengan") Then
        '                listHasil.Add(xloop.DIAGNOSA_KEPERAWATAN)
        '            ElseIf xloop.DIAGNOSA_KEPERAWATAN.Contains(":") Then
        '                listHasil.Add(vbCrLf & xloop.DIAGNOSA_KEPERAWATAN)
        '            Else
        '                listHasil.Add(xloop.DIAGNOSA_KEPERAWATAN)
        '            End If
        '        Next

        '        strdiagnosa = strdiagnosa & String.Join(vbCrLf, listHasil.ToArray)
        '        cellDiagnosa.Text = strdiagnosa
        '    End If
        'Catch ex As Exception

        'End Try
    End Sub

    Private Sub cellLuaran_BeforePrint(sender As Object, e As Printing.PrintEventArgs) Handles cellLuaran.BeforePrint
        'Dim sNOIDASKEP As String
        'Dim sKdItemDiagnosa As String

        'Try
        '    sNOIDASKEP = Report.GetCurrentColumnValue("KDASESMEN")
        '    sKdItemDiagnosa = DetailReport.GetCurrentColumnValue("KDITEMDIAGNOSAPERAWAT")
        '    Dim listHasil As New List(Of String)
        '    Dim listHasil2 As New List(Of String)
        '    Dim dsDiagnosa = oDiagnosa.GetData(sNOIDASKEP)
        '    Dim strdiagnosa As String = ""
        '    Dim count As String = ""

        '    If dsDiagnosa IsNot Nothing Then
        '        For Each xloop In oDiagnosa.GetDataDetail(sNOIDASKEP).Where(Function(x) (x.LUARAN.Contains("Setelah dilakukan perawatan dalam waktu") And x.LUARAN.Contains(":") Or x.CEK2 = True) And x.KDITEMDIAGNOSAPERAWAT = sKdItemDiagnosa)
        '            If xloop.LUARAN.Contains("Setelah dilakukan perawatan dalam waktu") Then
        '                listHasil.Add(xloop.LUARAN)
        '            ElseIf xloop.LUARAN.Contains(":") Then
        '                listHasil.Add(vbCrLf & xloop.LUARAN)
        '            Else
        '                listHasil.Add(xloop.LUARAN)
        '            End If
        '        Next

        '        strdiagnosa = strdiagnosa & String.Join(vbCrLf, listHasil.ToArray)
        '        cellLuaran.Text = strdiagnosa
        '    End If
        'Catch ex As Exception

        'End Try
    End Sub

    Private Sub celIntervensi_BeforePrint(sender As Object, e As Printing.PrintEventArgs) Handles celIntervensi.BeforePrint
        'Dim sNOIDASKEP As String
        'Dim sKdItemDiagnosa As String

        'Try
        '    sNOIDASKEP = Report.GetCurrentColumnValue("KDASESMEN")
        '    sKdItemDiagnosa = DetailReport.GetCurrentColumnValue("KDITEMDIAGNOSAPERAWAT")
        '    Dim listHasil As New List(Of String)
        '    Dim listHasil2 As New List(Of String)
        '    Dim dsDiagnosa = oDiagnosa.GetData(sNOIDASKEP)
        '    Dim strdiagnosa As String = ""
        '    Dim count As String = ""

        '    If dsDiagnosa IsNot Nothing Then
        '        For Each xloop In oDiagnosa.GetDataDetail(sNOIDASKEP).Where(Function(x) (x.INTERVENSI.Contains(":") Or x.CEK3 = True) And x.KDITEMDIAGNOSAPERAWAT = sKdItemDiagnosa)
        '            listHasil.Add(xloop.INTERVENSI)
        '        Next

        '        strdiagnosa = strdiagnosa & String.Join(vbCrLf, listHasil.ToArray)
        '        celIntervensi.Text = strdiagnosa
        '    End If
        'Catch ex As Exception

        'End Try
    End Sub

    Private Sub SubBand1_BeforePrint(sender As Object, e As Printing.PrintEventArgs) Handles SubBand1.BeforePrint
        'dewasa
        Dim cekmorse0 As Boolean = Report.GetCurrentColumnValue("SKALANYERI_00")
        Dim cekmorse1 As Boolean = Report.GetCurrentColumnValue("SKALANYERI_01")
        Dim cekmorse2 As Boolean = Report.GetCurrentColumnValue("SKALANYERI_02")
        Dim cekmorse3 As Boolean = Report.GetCurrentColumnValue("SKALANYERI_03")
        Dim cekmorse4 As Boolean = Report.GetCurrentColumnValue("SKALANYERI_04")
        Dim cekmorse5 As Boolean = Report.GetCurrentColumnValue("SKALANYERI_05")
        Dim cekmorse6 As Boolean = Report.GetCurrentColumnValue("SKALANYERI_06")
        Dim cekmorse7 As Boolean = Report.GetCurrentColumnValue("SKALANYERI_07")
        Dim cekmorse8 As Boolean = Report.GetCurrentColumnValue("SKALANYERI_08")
        Dim cekmorse9 As Boolean = Report.GetCurrentColumnValue("SKALANYERI_09")
        Dim cekmorse10 As Boolean = Report.GetCurrentColumnValue("SKALANYERI_10")

        If cekmorse0 = false And 
            cekmorse1 = false And
            cekmorse2 = false And
            cekmorse3 = false And
            cekmorse4 = false And
            cekmorse5 = false And
            cekmorse6 = false And
            cekmorse7 = false And
            cekmorse8 = false And
            cekmorse9 = false And
            cekmorse10 = false Then
            SubBand1.Visible = False
        Else
            SubBand1.Visible = True
        End If
    End Sub

    Private Sub SubBand2_BeforePrint(sender As Object, e As Printing.PrintEventArgs) Handles SubBand2.BeforePrint
        'anak
        Dim cekmorse1 As Boolean = Report.GetCurrentColumnValue("FLACC_1")
        Dim cekmorse2 As Boolean = Report.GetCurrentColumnValue("FLACC_2")
        Dim cekmorse3 As Boolean = Report.GetCurrentColumnValue("FLACC_3")
        Dim cekmorse4 As Boolean = Report.GetCurrentColumnValue("FLACC_4")
        Dim cekmorse5 As Boolean = Report.GetCurrentColumnValue("FLACC_5")
        Dim cekmorse6 As Boolean = Report.GetCurrentColumnValue("FLACC_6")

        If cekmorse1 = false And
            cekmorse2 = false And
            cekmorse3 = false And
            cekmorse4 = false And
            cekmorse5 = false And
            cekmorse6 = false Then
            SubBand2.Visible = False
        Else
            SubBand2.Visible = True
        End If
    End Sub


End Class