Imports System.Data.SqlClient

Public Class frmDashboard_
    Private Sub Me_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'Timer1.Start()
        lblWaktu.Text = Now.ToString("dd-MM-yyyy HH:mm:ss")
    End Sub
    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        lblWaktu.Text = Now.ToString("dd-MM-yyyy HH:mm:ss")
    End Sub
    Private Sub XtraTabControl1_SelectedPageChanged() Handles XtraTabControl1.SelectedPageChanged
        If XtraTabControl1.SelectedTabPageIndex = 1 Then
            fn_LoadDataPendaftaran()
            fn_LoadDataPendaftaranPoli()
            fn_LoadDataTopTen()
            fn_LoadDataRuangan
            lblWaktu.Text = Now.ToString("dd-MM-yyyy HH:mm:ss")
        End If
    End Sub
    Private Sub fn_LoadDataPendaftaran()
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

            SQL = "SELECT
                    BULAN = 
                    CASE X.BULAN 
                    WHEN 1 THEN 'Januari' 
                    WHEN 2 THEN 'Februari'
                    WHEN 3 THEN 'Maret'
                    WHEN 4 THEN 'April'
                    WHEN 5 THEN 'Mei'
                    WHEN 6 THEN 'Juni'
                    WHEN 7 THEN 'Juli'
                    WHEN 8 THEN 'Agustus'
                    WHEN 9 THEN 'September'
                    WHEN 10 THEN 'Oktober'
                    WHEN 11 THEN 'Nopember'
                    ELSE
                    'Desember'
                    END
                    ,KUNJUNGAN = COUNT(X.KDREG)
                    FROM
                    (
                    SELECT 
                    BULAN = MONTH(B.DATE)
                    ,B.KDREG
                    FROM 
                    S_PENDAFTARAN_H B 
                    WHERE YEAR(B.DATE) = '" & Now.ToString("yyyy") & "' 
                    AND B.CATEGORY = 1 AND B.STATUSDAFTAR <> 3
                    ) X
                    GROUP BY
                    X.BULAN "

            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "S_PENDAFTARAN_H_KUNJUNGAN")

            ChartTahun.Series("Kunjungan").Points.Clear()

            For iLoop As Integer = 0 To ds.Tables("S_PENDAFTARAN_H_KUNJUNGAN").Rows.Count - 1
                With ds.Tables("S_PENDAFTARAN_H_KUNJUNGAN")
                    ChartTahun.Series("Kunjungan").Points.AddXY(.Rows(iLoop)("BULAN"), .Rows(iLoop)("KUNJUNGAN"))
                End With
            Next

            ChartTahun.Titles("Title1").Text = "Grafik Kunjungan Rawat Jalan Tahun " & Now.ToString("yyyy")

            If oConn.State = ConnectionState.Open Then
                oConn.Close()
            End If
        Catch oErr As Exception
            MsgBox("Load List Pasien Data : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadDataPendaftaranPoli()
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

            SQL = "SELECT
                    X.POLI
                    ,KUNJUNGAN = COUNT(X.KDREG)
                    FROM
                    (
                    SELECT 
                    BULAN = MONTH(B.DATE)
                    ,B.KDREG
                    ,POLI = C.KDPOLIBPJS
                    FROM 
                    S_PENDAFTARAN_H B 
                    INNER JOIN M_DEPARTMENT C
                    ON B.KDDEPARTMENT = C.KDDEPARTMENT
                    WHERE YEAR(B.DATE) ='" & Now.ToString("yyyy") & "' 
                    AND B.CATEGORY = 1 AND B.STATUSDAFTAR <> 3
                    ) X
                    GROUP BY
                    X.POLI "

            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "S_PENDAFTARAN_H_POLI")

            ChartPoli.Series("Poli").Points.Clear()

            For iLoop As Integer = 0 To ds.Tables("S_PENDAFTARAN_H_POLI").Rows.Count - 1
                With ds.Tables("S_PENDAFTARAN_H_POLI")
                    ChartPoli.Series("Poli").Points.AddXY(.Rows(iLoop)("POLI"), .Rows(iLoop)("KUNJUNGAN"))
                End With
            Next

            ChartPoli.Titles("Title1").Text = "Grafik Kunjungan Poli Tahun " & Now.ToString("yyyy")

            If oConn.State = ConnectionState.Open Then
                oConn.Close()
            End If
        Catch oErr As Exception
            MsgBox("Load List Pasien Data : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadDataTopTen()
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
            SQL &= "TOP 10 * "
            SQL &= "FROM ( "
            SQL &= "SELECT "
            SQL &= "B.KDDIAGNOSA "
            SQL &= ",TOTAL = COUNT(B.KDDIAGNOSA) "
            SQL &= "FROM S_KODING_H A "
            SQL &= "INNER JOIN S_KODING_DX B "
            SQL &= "ON A.KDKODING = B.KDKODING "
            SQL &= "INNER JOIN S_PENDAFTARAN_H D "
            SQL &= "ON A.KDREG = D.KDREG "
            SQL &= "WHERE MONTH(D.DATE) ='" & CInt(Now.ToString("MM")) & "'  "
            SQL &= "AND D.CATEGORY = 1 AND D.STATUSDAFTAR <> 3 "
            SQL &= "GROUP BY "
            SQL &= "B.KDDIAGNOSA "
            SQL &= ") X "
            SQL &= "ORDER BY "
            SQL &= "X.TOTAL DESC "

            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)

            da.Fill(ds, "S_KODING_H")

            ChartDiagnosa.Series("ICDX").Points.Clear()
            'ChartDiagnosa.Series("ICDIX").Points.Clear()

            For iLoop As Integer = 0 To ds.Tables("S_KODING_H").Rows.Count - 1
                With ds.Tables("S_KODING_H")
                    ChartDiagnosa.Series("ICDX").Points.AddXY(.Rows(iLoop)("KDDIAGNOSA"), .Rows(iLoop)("TOTAL"))
                End With
            Next

            ChartDiagnosa.Titles("Title1").Text = "Grafik 10 Diagnosa Teratas Bulan " & Now.ToString("MMMM")

            If oConn.State = ConnectionState.Open Then
                oConn.Close()
            End If
        Catch oErr As Exception
            MsgBox("Load List Pasien Data : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadDataRuangan()
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
            SQL &= "XX.Terisi,TOTAL = COUNT(XX.KDMROOM) FROM ( "
            SQL &= "SELECT "
            SQL &= "A.KDMROOM "
            'SQL &= ",NamaKamar = A.LOKASI + '-' + A.NAME_DISPLAY "
            'SQL &= ",NomorBed = C.SEQ "
            'SQL &= ",TanggalMasuk = ISNULL((SELECT FORMAT(DATE, 'dd/MM/yyyy HH:mm:ss') FROM S_PENDAFTARAN_H WHERE C.KDREG = KDREG), '') "
            'SQL &= ",TANGGALMASUK_ = ISNULL((SELECT DATE FROM S_PENDAFTARAN_H WHERE C.KDREG = KDREG), '') "
            'SQL &= ",ISPULANG = ISNULL((SELECT ISUPDATEPULANG FROM S_PENDAFTARAN_H WHERE C.KDREG = KDREG), '') "
            'SQL &= ",DATEPULANG = ISNULL((SELECT DATEPULANG FROM S_PENDAFTARAN_H WHERE C.KDREG = KDREG), '') "
            'SQL &= ",KelasRawat = B.NAME_DISPLAY "
            'SQL &= ",NoTransaksi = C.KDREG "
            'SQL &= ",NoTransaksi_Poli = ISNULL((SELECT KDREGAWAL FROM S_PENDAFTARAN_H WHERE C.KDREG = KDREG), '') "
            'SQL &= ",NoTransaksi_PoliNama = ISNULL((SELECT BB.NAME_DISPLAY FROM S_PENDAFTARAN_H AA INNER JOIN M_DEPARTMENT BB ON AA.KDDEPARTMENT = BB.KDDEPARTMENT WHERE C.KDREG = AA.KDREG), '') "
            'SQL &= ",NoRM = ISNULL((SELECT KDCUSTOMER FROM S_PENDAFTARAN_H WHERE C.KDREG = KDREG), '') "
            'SQL &= ",KARTUBPJS = ISNULL((SELECT KARTUBPJS FROM S_PENDAFTARAN_H WHERE C.KDREG = KDREG), '') "
            'SQL &= ",NOMORSEP = ISNULL((SELECT NOMORSEP FROM S_PENDAFTARAN_H WHERE C.KDREG = KDREG), '') "
            'SQL &= ",KDKELASRAWAT = ISNULL((SELECT BB.DESCRIPTION FROM S_PENDAFTARAN_H AA INNER JOIN M_KELASRAWAT BB ON AA.KDKELASRAWAT = BB.KDKELASRAWAT WHERE C.KDREG = AA.KDREG ), '') "
            'SQL &= ",NamaPasien = ISNULL((SELECT BB.NAME_DISPLAY FROM S_PENDAFTARAN_H AA INNER JOIN M_CUSTOMER BB ON AA.KDCUSTOMER = BB.KDCUSTOMER WHERE C.KDREG = AA.KDREG), '') "
            'SQL &= ",JenisKelamin = ISNULL((SELECT CASE BB.JK WHEN '1' THEN 'Laki-laki' ELSE 'Perempuan' END FROM S_PENDAFTARAN_H AA INNER JOIN M_CUSTOMER BB ON AA.KDCUSTOMER = BB.KDCUSTOMER WHERE C.KDREG = AA.KDREG), '') "
            'SQL &= ",TanggalLahir = ISNULL((SELECT BB.TANGGALLAHIR FROM S_PENDAFTARAN_H AA INNER JOIN M_CUSTOMER BB ON AA.KDCUSTOMER = BB.KDCUSTOMER WHERE C.KDREG = AA.KDREG), '') "
            'SQL &= ",Penjamin = ISNULL((SELECT BB.NAME_DISPLAY FROM S_PENDAFTARAN_H AA INNER JOIN M_DEBTOR BB ON AA.KDDEBTOR = BB.KDDEBTOR WHERE C.KDREG = AA.KDREG), '') "
            'SQL &= ",KDDOCTOR_DPJPUTAMA = ISNULL((SELECT AA.KDDOCTOR FROM S_PENDAFTARAN_H AA WHERE C.KDREG = AA.KDREG), '') "
            SQL &= ",Tersedia = C.JENIS_TERSEDIA "
            SQL &= ",Terisi =  CASE C.ISTERISI WHEN 'RENCANA PULANG' THEN 'RENCANAPULANG' ELSE C.ISTERISI END "
            SQL &= ",Keterangan = C.DESCRIPTION "
            'SQL &= ",KDCPPT = ISNULL((SELECT TOP 1 KDCPPT FROM I_TRACKING_CPPT WHERE TANGGAL = '" & Now.ToString("yyyyMMdd") & "' AND C.KDREG = KDREG AND KDDOCTOR = '" & grdDPJPUtama.EditValue & "'), '') "
            'SQL &= ",KDCPPT_PERAWAT = ISNULL((SELECT TOP 1 KDCPPT FROM I_TRACKING_CPPT WHERE TANGGAL = '" & Now.ToString("yyyyMMdd") & "' AND C.KDREG = KDREG AND DESCRIPTION = 'PERAWAT'), '') "
            'SQL &= ",KDCPPT_DOKTERAKHIR = ISNULL((SELECT TOP 1 KDCPPT FROM I_TRACKING_CPPT WHERE C.KDREG = KDREG AND KDDOCTOR = '" & grdDPJPUtama.EditValue & "' ORDER BY KDCPPT DESC), '') "
            'SQL &= ",KDCPPT_PERAWATAKHIR = ISNULL((SELECT TOP 1 KDCPPT FROM I_TRACKING_CPPT WHERE C.KDREG = KDREG AND DESCRIPTION = 'PERAWAT' ORDER BY KDCPPT DESC), '') "
            'SQL &= ",KETERANGAN_TINDAKLANJUT = ISNULL((SELECT KETERANGAN_TINDAKLANJUT FROM S_PENDAFTARAN_H WHERE C.KDREG = KDREG), '') "
            SQL &= "FROM "
            SQL &= "M_MEDICAL_ROOM A "
            SQL &= "INNER JOIN M_TIPE_KAMAR B "
            SQL &= "ON A.KDKAMAR = B.KDKAMAR "
            SQL &= "INNER JOIN M_MEDICAL_ROOM_DETIL C "
            SQL &= "ON A.KDMROOM = C.KDMROOM "
            SQL &= "WHERE A.ISACTIVE = 1 "
            SQL &= ") XX GROUP BY XX.Terisi "

            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)

            da.Fill(ds, "M_MEDICALROOM")

            Dim TotalRuangan As Integer = 0

            For iLoop As Integer = 0 To ds.Tables("M_MEDICALROOM").Rows.Count - 1
                With ds.Tables("M_MEDICALROOM")
                    TotalRuangan += 1
                End With
            Next

            'Label2.Text = "TOTAL BED " & TotalRuangan

            'ChartBed.Series("BOOKING").Points.Clear()
            'ChartBed.Series("KOSONG").Points.Clear()
            'ChartBed.Series("RENCANAPULANG").Points.Clear()
            ChartBed.Series("TERISI").Points.Clear()

            For iLoop As Integer = 0 To ds.Tables("M_MEDICALROOM").Rows.Count - 1
                With ds.Tables("M_MEDICALROOM")
                    'ChartBed.Series("BOOKING").Points.AddXY(.Rows(iLoop)("Terisi"), .Rows(iLoop)("TOTAL"))
                    'ChartBed.Series("KOSONG").Points.AddXY(.Rows(iLoop)("Terisi"), .Rows(iLoop)("TOTAL"))
                    'ChartBed.Series("RENCANAPULANG").Points.AddXY(.Rows(iLoop)("Terisi"), .Rows(iLoop)("TOTAL"))
                    ChartBed.Series("TERISI").Points.AddXY(.Rows(iLoop)("Terisi"), .Rows(iLoop)("TOTAL"))
                End With
            Next

            If oConn.State = ConnectionState.Open Then
                oConn.Close()
            End If
        Catch ex As Exception
            MsgBox("Preview Data : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub lblWaktu_Click(sender As Object, e As EventArgs) Handles lblWaktu.Click
        fn_LoadDataPendaftaran()
        fn_LoadDataPendaftaranPoli()
        fn_LoadDataTopTen()
        fn_LoadDataRuangan
        lblWaktu.Text = Now.ToString("dd-MM-yyyy HH:mm:ss")
    End Sub
End Class