'Imports iPOS.GLB.Globals
'Imports iPOS.DA
Imports System.Linq

Imports System.Data
Imports System.Data.SqlClient
Imports System.IO

Public Class frmLayarAntrianPenyakitDalam
    'Private oSet_Panggilan As New Setting.clsSet_Panggilan
    Private sAlamatFolder As String = "D:\Suara\"

#Region "Function"
    Private Sub frmLayarAntrian_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Timer1.Start()
        Timer2.Start()
    End Sub
    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        fn_LoadLoket()
    End Sub
    Private Sub Timer2_Tick(sender As Object, e As EventArgs) Handles Timer2.Tick
        fn_Tanggal()
        fn_LoadLoketTampil()
    End Sub
    Private Sub fn_LoadLoketTampil()
        Try
            Dim oConn As New SqlConnection
            Dim oComm As New SqlCommand
            Dim da As SqlDataAdapter
            Dim ds As New DataSet
            Dim SQL As String
            Dim sConn As String = Decrypt(My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Software\SIMRS\SW\", "Database", "").ToString())

            oConn = New SqlConnection(sConn)
            If oConn.State = ConnectionState.Closed Then
                oConn.Open()
            End If

            SQL = "SELECT "
            SQL &= "* "
            SQL &= "FROM "
            SQL &= "SET_PANGGIL_ANTRIAN "
            SQL &= "WHERE "

            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "SET_LOKET")

            If oConn.State = ConnectionState.Open Then
                oConn.Close()
            End If

            For iLoop As Integer = 0 To ds.Tables("SET_LOKET").Rows.Count - 1
                With ds.Tables("SET_LOKET")
                    If .Rows(iLoop)("KODE") = "A" Then
                        lblNAMAPOLI.Text = lbl1.Text
                        lbl16.Text = .Rows(iLoop)("KODE") & .Rows(iLoop)("NOMORANTRIAN").ToString.PadLeft(3, "0")
                    End If
                    If .Rows(iLoop)("KODE") = "B" Then
                        lblNAMAPOLI.Text = lbl2.Text
                        lbl17.Text = .Rows(iLoop)("KODE") & .Rows(iLoop)("NOMORANTRIAN").ToString.PadLeft(3, "0")
                    End If
                    If .Rows(iLoop)("KODE") = "C" Then
                        lblNAMAPOLI.Text = lbl3.Text
                        lbl18.Text = .Rows(iLoop)("KODE") & .Rows(iLoop)("NOMORANTRIAN").ToString.PadLeft(3, "0")
                    End If
                    If .Rows(iLoop)("KODE") = "D" Then
                        lblNAMAPOLI.Text = lbl4.Text
                        lbl19.Text = .Rows(iLoop)("KODE") & .Rows(iLoop)("NOMORANTRIAN").ToString.PadLeft(3, "0")
                    End If
                    If .Rows(iLoop)("KODE") = "E" Then
                        lblNAMAPOLI.Text = lbl5.Text
                        lbl20.Text = .Rows(iLoop)("KODE") & .Rows(iLoop)("NOMORANTRIAN").ToString.PadLeft(3, "0")
                    End If
                    If .Rows(iLoop)("KODE") = "F" Then
                        lblNAMAPOLI.Text = lbl6.Text
                        lbl21.Text = .Rows(iLoop)("KODE") & .Rows(iLoop)("NOMORANTRIAN").ToString.PadLeft(3, "0")
                    End If
                    If .Rows(iLoop)("KODE") = "G" Then
                        lblNAMAPOLI.Text = lbl7.Text
                        lbl22.Text = .Rows(iLoop)("KODE") & .Rows(iLoop)("NOMORANTRIAN").ToString.PadLeft(3, "0")
                    End If
                    If .Rows(iLoop)("KODE") = "H" Then
                        lblNAMAPOLI.Text = lbl8.Text
                        lbl23.Text = .Rows(iLoop)("KODE") & .Rows(iLoop)("NOMORANTRIAN").ToString.PadLeft(3, "0")
                    End If
                    If .Rows(iLoop)("KODE") = "I" Then
                        lblNAMAPOLI.Text = lbl9.Text
                        lbl24.Text = .Rows(iLoop)("KODE") & .Rows(iLoop)("NOMORANTRIAN").ToString.PadLeft(3, "0")
                    End If
                    If .Rows(iLoop)("KODE") = "J" Then
                        lblNAMAPOLI.Text = lbl10.Text
                        lbl25.Text = .Rows(iLoop)("KODE") & .Rows(iLoop)("NOMORANTRIAN").ToString.PadLeft(3, "0")
                    End If
                    If .Rows(iLoop)("KODE") = "K" Then
                        lblNAMAPOLI.Text = lbl11.Text
                        lbl26.Text = .Rows(iLoop)("KODE") & .Rows(iLoop)("NOMORANTRIAN").ToString.PadLeft(3, "0")
                    End If
                    If .Rows(iLoop)("KODE") = "L" Then
                        lblNAMAPOLI.Text = lbl12.Text
                        lbl27.Text = .Rows(iLoop)("KODE") & .Rows(iLoop)("NOMORANTRIAN").ToString.PadLeft(3, "0")
                    End If
                    If .Rows(iLoop)("KODE") = "M" Then
                        lblNAMAPOLI.Text = lbl13.Text
                        lbl28.Text = .Rows(iLoop)("KODE") & .Rows(iLoop)("NOMORANTRIAN").ToString.PadLeft(3, "0")
                    End If
                    If .Rows(iLoop)("KODE") = "N" Then
                        lblNAMAPOLI.Text = lbl4.Text
                        lbl29.Text = .Rows(iLoop)("KODE") & .Rows(iLoop)("NOMORANTRIAN").ToString.PadLeft(3, "0")
                    End If
                    If .Rows(iLoop)("KODE") = "O" Then
                        lblNAMAPOLI.Text = lbl5.Text
                        lbl30.Text = .Rows(iLoop)("KODE") & .Rows(iLoop)("NOMORANTRIAN").ToString.PadLeft(3, "0")
                    End If
                    If .Rows(iLoop)("KODE") = "P" Then
                        lblNAMAPOLI.Text = lbl6.Text
                        lbl31.Text = .Rows(iLoop)("KODE") & .Rows(iLoop)("NOMORANTRIAN").ToString.PadLeft(3, "0")
                    End If
                End With
            Next

        Catch oErr As Exception
            Timer1.Stop()
            'Timer2.Stop()
            MsgBox("Load Antrian Data : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadLoket()
        Try
            Dim oConn As New SqlConnection
            Dim oComm As New SqlCommand
            Dim da As SqlDataAdapter
            Dim ds As New DataSet
            Dim SQL As String
            Dim sConn As String = Decrypt(My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Software\SIMRS\SW\", "Database", "").ToString())

            oConn = New SqlConnection(sConn)
            If oConn.State = ConnectionState.Closed Then
                oConn.Open()
            End If

            SQL = "SELECT "
            SQL &= "* "
            SQL &= "FROM "
            SQL &= "SET_PANGGIL_ANTRIAN "
            SQL &= "WHERE "
            SQL &= "ISPANGGIL = 0 "

            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "SET_LOKET")

            If oConn.State = ConnectionState.Open Then
                oConn.Close()
            End If

            For iLoop As Integer = 0 To ds.Tables("SET_LOKET").Rows.Count - 1
                With ds.Tables("SET_LOKET")
                    fn_UpdateIsPanggil(.Rows(iLoop)("KODE"))
                    lblNOANTRIAN.Text = .Rows(iLoop)("KODE") & .Rows(iLoop)("NOMORANTRIAN").ToString.PadLeft(3, "0")
                    Panggil(.Rows(iLoop)("NOMORANTRIAN"), .Rows(iLoop)("KODE"))
                End With
            Next

        Catch oErr As Exception
            'Timer1.Stop()
            Timer2.Stop()
            MsgBox("Load Antrian Data : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub Panggil(ByVal nilai As Long, ByVal KODE As String)
        My.Computer.Audio.Play(sAlamatFolder & "opening.wav", AudioPlayMode.WaitToComplete)
        My.Computer.Audio.Play(sAlamatFolder & "Antrian Nomor.wav", AudioPlayMode.WaitToComplete)

        My.Computer.Audio.Play(sAlamatFolder & KODE & ".wav", AudioPlayMode.WaitToComplete)

        Terbilang(nilai)

        'My.Computer.Audio.Play(sAlamatFolder & "diloket.wav", AudioPlayMode.WaitToComplete)
        'My.Computer.Audio.Play(sAlamatFolder & LOKET & ".wav", AudioPlayMode.WaitToComplete)
    End Sub
    Private Sub Terbilang(ByVal i As Integer)
        Select Case i
            Case 1 To 20
                My.Computer.Audio.Play(sAlamatFolder & i & ".wav", AudioPlayMode.WaitToComplete)
            Case 21 To 99
                If i = 20 Or i = 30 Or i = 40 Or i = 50 Or i = 60 Or i = 70 Or i = 80 Or i = 90 Then
                    My.Computer.Audio.Play(sAlamatFolder & i & ".wav", AudioPlayMode.WaitToComplete)
                Else
                    My.Computer.Audio.Play(sAlamatFolder & Int(i / 10) & "0m" & ".wav", AudioPlayMode.WaitToComplete)
                    My.Computer.Audio.Play(sAlamatFolder & i Mod 10 & ".wav", AudioPlayMode.WaitToComplete)
                End If
            Case 100 To 999
                If i = 100 Or i = 200 Or i = 300 Or i = 400 Or i = 500 Or i = 600 Or i = 700 Or i = 800 Or i = 900 Then
                    My.Computer.Audio.Play(sAlamatFolder & i & ".wav", AudioPlayMode.WaitToComplete)
                Else
                    My.Computer.Audio.Play(sAlamatFolder & Int(i / 100) & "00m" & ".wav", AudioPlayMode.WaitToComplete)

                    Dim Puluhan = i Mod 100

                    If Puluhan <= 20 Then
                        My.Computer.Audio.Play(sAlamatFolder & Puluhan & ".wav", AudioPlayMode.WaitToComplete)
                    Else
                        If Puluhan = 20 Or Puluhan = 30 Or Puluhan = 40 Or Puluhan = 50 Or Puluhan = 60 Or Puluhan = 70 Or Puluhan = 80 Or Puluhan = 90 Then
                            My.Computer.Audio.Play(sAlamatFolder & Puluhan & "m" & ".wav", AudioPlayMode.WaitToComplete)
                        Else
                            My.Computer.Audio.Play(sAlamatFolder & Int(Puluhan / 10) & "0m" & ".wav", AudioPlayMode.WaitToComplete)
                            My.Computer.Audio.Play(sAlamatFolder & Puluhan Mod 10 & ".wav", AudioPlayMode.WaitToComplete)
                        End If
                    End If
                End If
        End Select
    End Sub
    Private Sub fn_UpdateIsPanggil(ByVal Loket As String)
        'Dim dsSet_Panggilan = oSet_Panggilan.GetData(Loket)
        'If dsSet_Panggilan IsNot Nothing Then
        '    oSet_Panggilan.UpdateIsPanggil(Loket)
        'End If
    End Sub
    Private Sub fn_Tanggal()
        lblWaktu.Text = fn_Timer() & ", " & Now.ToString("dd-MM-yyyy")
        'lblJAM.Text = Now.ToString("HH:mm:ss")
    End Sub
    Private Function fn_Timer() As String
        Select Case Now.ToString("dddd")
            Case "Sunday"
                fn_Timer = "MINGGU"
            Case "Monday"
                fn_Timer = "SENIN"
            Case "Tuesday"
                fn_Timer = "SELASA"
            Case "Wednesday"
                fn_Timer = "RABU"
            Case "Thursday"
                fn_Timer = "KAMIS"
            Case "Friday"
                fn_Timer = "JUMAT"
            Case "Saturday"
                fn_Timer = "SABTU"
            Case Else
                fn_Timer = "-"
        End Select
    End Function
    Private Sub SimpleButton1_Click(sender As Object, e As EventArgs) Handles SimpleButton1.Click
        Me.Close()
    End Sub
#End Region
End Class