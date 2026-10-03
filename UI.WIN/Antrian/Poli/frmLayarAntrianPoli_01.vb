Imports System.Data.SqlClient
Imports DataAccess

Public Class frmLayarAntrianPoli_01
    Private oSet_Panggilan As New Antrian.clsSET_PANGGIL_ANTRIAN
    Private sAlamatFolder As String = sAlamatSuara
    Private sKDDOCTOR_1 As Integer = 0
    Private sKDDOCTOR_2 As Integer = 0

#Region "Function"
    Private Sub frmLayarAntrian_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        fn_LoadData(sLAYARPOLI)

        fn_LoadDataDokter1(sKDDOCTOR_1)
        fn_LoadDataDokter2(sKDDOCTOR_2)

        TimerPanggil.Start()
        TimerLoadDalam.Start()

    End Sub
    Private Sub fn_LoadData(ByVal LAYARPOLI As String)
        Try
            Dim oConn As New SqlConnection
            Dim oComm As New SqlCommand
            Dim da As SqlDataAdapter
            Dim ds As New DataSet
            Dim SQL As String
            Dim sConn As String = sConnOld

            oConn = New SqlConnection(sConn)
            If oConn.State = ConnectionState.Closed Then
                oConn.Open()
            End If

            SQL = "SELECT "
            SQL &= "* "
            SQL &= "FROM "
            SQL &= "Z_MASTER_ATRIANPOLI A "
            SQL &= "INNER JOIN M_DOCTOR B "
            SQL &= "ON A.KDDEPARTMENT = B.KDDOCTOR "
            SQL &= "WHERE A.LAYAR = '" & LAYARPOLI & "' "
            SQL &= "AND A.ISACTIVE = 1 "

            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "Z_MASTER_ATRIANPOLI")


            If oConn.State = ConnectionState.Open Then
                oConn.Close()
            End If
            '
            For iLoop As Integer = 0 To ds.Tables("Z_MASTER_ATRIANPOLI").Rows.Count - 1
                With ds.Tables("Z_MASTER_ATRIANPOLI")
                    If .Rows(iLoop)("KETERANGAN") = "1" Then
                        sKDDOCTOR_1 = .Rows(iLoop)("KDDEPARTMENT")
                        'sKODEPOLI_1 = .Rows(iLoop)("KDPOLIBPJS")
                        btnPOLI1.Text = "DOKTER " & .Rows(iLoop)("NAME_DISPLAY")
                        LabelControl1.Text = .Rows(iLoop)("FREE_TEXT")
                    ElseIf .Rows(iLoop)("KETERANGAN") = "2" Then
                        sKDDOCTOR_2 = .Rows(iLoop)("KDDEPARTMENT")
                        'sKODEPOLI_2 = .Rows(iLoop)("KDPOLIBPJS")
                        btnPOLI2.Text = "DOKTER " & .Rows(iLoop)("NAME_DISPLAY")
                        LabelControl2.Text = .Rows(iLoop)("FREE_TEXT")
                    End If
                End With
            Next

        Catch oErr As Exception
            TimerLoadDalam.Stop()
            MsgBox("Load List Pasien Data : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadDataDokter1(ByVal KDDOCTOR As Integer)
        Try
            Dim oConn As New SqlConnection
            Dim oComm As New SqlCommand
            Dim da As SqlDataAdapter
            Dim ds As New DataSet
            Dim SQL As String
            Dim sConn As String = sConnOld

            oConn = New SqlConnection(sConn)
            If oConn.State = ConnectionState.Closed Then
                oConn.Open()
            End If

            SQL = "SELECT "
            SQL &= "* "
            SQL &= "FROM ( "
            SQL &= "SELECT "
            SQL &= "ANTRIANDOKTER = C.KODEANTRIAN + FORMAT(B.ANTRIANDOKTER, '000')  "
            SQL &= ",PASIEN = D.NAME_DISPLAY "
            SQL &= ",KETERANGAN = (SELECT CASE B.ISRESUME WHEN 1 THEN 'SELESAI DOKTER' ELSE (SELECT CASE B.ISPERAWAT WHEN 0 THEN 'ANTRIAN PERAWAT' ELSE 'ANTRIAN DOKTER' END) END) "
            SQL &= ""
            SQL &= "FROM "
            SQL &= "S_PENDAFTARAN_H B "
            SQL &= "INNER JOIN M_DOCTOR C "
            SQL &= "ON B.KDDOCTOR = C.KDDOCTOR "
            SQL &= "INNER JOIN M_CUSTOMER D "
            SQL &= "ON B.KDCUSTOMER = D.KDCUSTOMER "
            SQL &= "WHERE CONVERT(VARCHAR(8), B.DATE, 112) = '" & Now.ToString("yyyyMMdd") & "' "
            SQL &= "AND B.STATUSDAFTAR <> 3 "
            If KDDOCTOR = "2131" Then
                SQL &= "AND CONVERT(NVARCHAR(50), B.KONSUL) = '1052' "
                SQL &= "AND B.CATEGORY = 1 "
            Else
                SQL &= "AND CONVERT(NVARCHAR(50), B.KONSUL) <> '1052' "
                SQL &= "AND B.CATEGORY = 1 "
                SQL &= "AND B.KDDOCTOR = " & KDDOCTOR & " "
            End If

            SQL &= ") Z "
            SQL &= "ORDER BY Z.KETERANGAN, Z.ANTRIANDOKTER ASC "

            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "S_LISTPASIEN_POLI1")

            grdDalam.DataSource = ds.Tables("S_LISTPASIEN_POLI1")
            grdDalam.ForceInitialize()

            If oConn.State = ConnectionState.Open Then
                oConn.Close()
            End If

        Catch oErr As Exception
            TimerLoadDalam.Stop()
            MsgBox("Load List Pasien Data : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadDataDokter2(ByVal KDDOCTOR As Integer)
        Try
            Dim oConn As New SqlConnection
            Dim oComm As New SqlCommand
            Dim da As SqlDataAdapter
            Dim ds As New DataSet
            Dim SQL As String
            Dim sConn As String = sConnOld

            oConn = New SqlConnection(sConn)
            If oConn.State = ConnectionState.Closed Then
                oConn.Open()
            End If

            SQL = "SELECT "
            SQL &= "* "
            SQL &= "FROM ( "
            SQL &= "SELECT "
            SQL &= "ANTRIANDOKTER = C.KODEANTRIAN + FORMAT(B.ANTRIANDOKTER, '000')  "
            SQL &= ",PASIEN = D.NAME_DISPLAY "
            SQL &= ",KETERANGAN = (SELECT CASE B.ISRESUME WHEN 1 THEN 'SELESAI DOKTER' ELSE (SELECT CASE B.ISPERAWAT WHEN 0 THEN 'ANTRIAN PERAWAT' ELSE 'ANTRIAN DOKTER' END) END) "
            SQL &= "FROM "
            SQL &= "S_PENDAFTARAN_H B "
            SQL &= "INNER JOIN M_DOCTOR C "
            SQL &= "ON B.KDDOCTOR = C.KDDOCTOR "
            SQL &= "INNER JOIN M_CUSTOMER D "
            SQL &= "ON B.KDCUSTOMER = D.KDCUSTOMER "
            SQL &= "WHERE CONVERT(VARCHAR(8), B.DATE, 112) = '" & Now.ToString("yyyyMMdd") & "' "
            SQL &= "AND B.STATUSDAFTAR <> 3 "
            If KDDOCTOR = "2131" Then
                SQL &= "AND CONVERT(NVARCHAR(50), B.KONSUL) = '1052' "
                SQL &= "AND B.CATEGORY = 1 "
            Else
                SQL &= "AND CONVERT(NVARCHAR(50), B.KONSUL) <> '1052' "
                SQL &= "AND B.CATEGORY = 1 "
                SQL &= "AND B.KDDOCTOR = " & KDDOCTOR & " "
            End If

            SQL &= ") Z "
            SQL &= "ORDER BY Z.KETERANGAN, Z.ANTRIANDOKTER ASC "

            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "S_LISTPASIEN_POLI2")

            grdKulit.DataSource = ds.Tables("S_LISTPASIEN_POLI2")
            grdKulit.ForceInitialize()

            If oConn.State = ConnectionState.Open Then
                oConn.Close()
            End If

        Catch oErr As Exception
            TimerLoadDalam.Stop()
            MsgBox("Load List Pasien Data : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub grvDalam_RowStyle(sender As System.Object, e As DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs) Handles grvDalam.RowStyle
        If grvDalam.IsFilterRow(e.RowHandle) Then Exit Sub
        If grvDalam.GetRowCellValue(e.RowHandle, "KETERANGAN") = "SELESAI DOKTER" Then
            e.Appearance.BackColor = Color.LawnGreen
        Else
            If grvDalam.GetRowCellValue(e.RowHandle, "KETERANGAN") = "ANTRIAN DOKTER" Then
                e.Appearance.BackColor = Color.Orange
            End If
        End If
    End Sub
    Private Sub grvKulit_RowStyle(sender As System.Object, e As DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs) Handles grvKulit.RowStyle
        If grvKulit.IsFilterRow(e.RowHandle) Then Exit Sub
        If grvKulit.GetRowCellValue(e.RowHandle, "KETERANGAN") = "SELESAI DOKTER" Then
            e.Appearance.BackColor = Color.LawnGreen
        Else
            If grvKulit.GetRowCellValue(e.RowHandle, "KETERANGAN") = "ANTRIAN DOKTER" Then
                e.Appearance.BackColor = Color.Orange
            End If
        End If
    End Sub
    Private Sub fn_LoadLoket(ByVal KODE_DOKTER1 As String, ByVal KODE_DOKTER2 As String)
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
            SQL &= "AND KODE_DOKTER = '" & KODE_DOKTER1 & "' "
            SQL &= "OR "
            SQL &= "ISPANGGIL = 0 "
            SQL &= "AND KODE_DOKTER = '" & KODE_DOKTER2 & "' "

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
                    If .Rows(iLoop)("DESCRIPTION") = "" Then
                        Panggil(.Rows(iLoop)("NOMORANTRIAN"), .Rows(iLoop)("KODE_POLI"), .Rows(iLoop)("KODE_DOKTER"), False)
                    Else
                        Panggil(.Rows(iLoop)("NOMORANTRIAN"), .Rows(iLoop)("KODE_POLI"), .Rows(iLoop)("KODE_DOKTER"), True)
                    End If

                    fn_UpdateIsPanggil(.Rows(iLoop)("KODE_POLI"), .Rows(iLoop)("KODE_DOKTER"))
                End With
            Next

        Catch oErr As Exception
            TimerPanggil.Stop()
            MsgBox("Load Antrian Data : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub Panggil(ByVal nilai As Long, ByVal KODE_POLI As String, ByVal KODE_DOKTER As String, ByVal ULANG As Boolean)
        My.Computer.Audio.Play(sAlamatFolder & "opening.wav", AudioPlayMode.WaitToComplete)

        If ULANG = True Then
            My.Computer.Audio.Play(sAlamatFolder & "Ulang.wav", AudioPlayMode.WaitToComplete)
        End If

        My.Computer.Audio.Play(sAlamatFolder & "Antrian Nomor.wav", AudioPlayMode.WaitToComplete)

        'Try
        '    My.Computer.Audio.Play(sAlamatFolder & "kepoli.wav", AudioPlayMode.WaitToComplete)
        '    My.Computer.Audio.Play(sAlamatFolder & KODE_POLI & ".wav", AudioPlayMode.WaitToComplete)
        'Catch ex As Exception

        'End Try

        Terbilang(nilai)

        Try
            My.Computer.Audio.Play(sAlamatFolder & "kedokter.wav", AudioPlayMode.WaitToComplete)
            My.Computer.Audio.Play(sAlamatFolder & KODE_DOKTER & ".wav", AudioPlayMode.WaitToComplete)
        Catch ex As Exception

        End Try
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
    Private Sub fn_UpdateIsPanggil(ByVal sKODE_POLI As String, ByVal sKODE_DOKTER As String)
        Dim dsSet_Panggilan = oSet_Panggilan.GetData(sKODE_POLI, sKODE_DOKTER)
        If dsSet_Panggilan IsNot Nothing Then
            oSet_Panggilan.UpdateIsPanggil(sKODE_POLI, sKODE_DOKTER)
        End If
    End Sub
    Private Sub SimpleButton2_Click(sender As Object, e As EventArgs)
        Me.Close()
    End Sub
    Private Sub TimerPanggil_Tick(sender As Object, e As EventArgs) Handles TimerPanggil.Tick
        fn_LoadLoket(sKDDOCTOR_1, sKDDOCTOR_2)
    End Sub
    Private Sub TimerLoadDalam_Tick(sender As Object, e As EventArgs) Handles TimerLoadDalam.Tick
        fn_LoadDataDokter1(sKDDOCTOR_1)
        fn_LoadDataDokter2(sKDDOCTOR_2)
    End Sub
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles btnPOLI1.Click
        Me.Close()
    End Sub
#End Region
End Class