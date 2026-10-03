Imports DataAccess
Imports System.Linq
Imports System.Data.SqlClient
Imports Newtonsoft.Json.Linq
Imports System.IO
Imports iTextSharp.text
Imports iTextSharp.text.pdf
Imports DevExpress.XtraSplashScreen
'Imports System.Drawing
'Imports System.Web.UI.WebControls
Imports System.Globalization

Public Class frmMedrekRawatNew2JalanList
    Private isLoad As Boolean = False
    Private isLoad_Splash As Boolean = False
    Private sisDOkter As Boolean = False
    Private oFormMode As FORM_MODE = FORM_MODE.FORM_MODE_VIEW
    Private sNoidSimpan As String = String.Empty
    Private oSetKoneksi As New Brigging.clsSetKoneksi
    Private oOrderPenunjang As New Order.clsOrderPenunjang
    Private sALAMATPASIEN As String = String.Empty
    Private sTEMPATLAHIR As String = String.Empty
    Private sKDDEPARTMENT As String = String.Empty

#Region "Function"
    Public Sub fn_LoadMe(ByVal isDOKTER As Boolean)
        sisDOkter = isDOKTER
        fn_LoadDEPARTMENT()
        Dim oDoctor As New Reference.clsDoctor
        Dim dsDepartment = oDoctor.GetDataByIDUser(sUserID)
        If dsDepartment IsNot Nothing Then
            grdKDDEPARTMENT.EditValue = dsDepartment.KDDEPARTMENT
            fn_LoadDokter(dsDepartment.KDDOCTOR)
        End If
    End Sub
    Private Sub Me_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        deDATE.DateTime = Now
        deTANGGALDATANG.DateTime = Now
        deDATETANGGALLAHIR.DateTime = Now
        fn_LoadSecurity()
        lGROUPFORM.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        lLISTANTRIAN.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        'lLISTANTRIANCHEK.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        isLoad_Splash = True
    End Sub
    Private Sub Form_Closing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        Dispose()
    End Sub
    Private Overloads Sub Dispose()
        MyBase.Dispose()
        Me.Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
    Private Sub fn_LoadSecurity()
        Try
            Dim oOtority As New Setting.clsOtority
            Dim oUser As New Setting.clsUser

            Dim ds = (From x In oOtority.GetDataDetail
                      Join y In oUser.GetData
                      On x.KDOTORITY Equals y.KDOTORITY
                      Where x.MODUL = "MEDREK_RJ" _
                      And y.KDUSER = sUserID
                      Select x.ISADD, x.ISDELETE, x.ISUPDATE, x.ISPRINT, x.ISVIEW).FirstOrDefault

            Try
                If ds.ISVIEW = True Then
                    fn_LoadView()
                    If grdKDDEPARTMENT.Text <> "" And grdDPJP.Text <> "" Then
                        fn_LoadData(grdKDDEPARTMENT.EditValue, grdDPJP.EditValue)
                    End If

                    If grdDPJP.Text <> "" Then
                        Dim oDoctor As New Reference.clsDoctor
                        Dim dsDoctor = oDoctor.GetData(grdDPJP.EditValue)
                        If dsDoctor IsNot Nothing Then
                            lblSIP.Text = dsDoctor.SIP
                            'fn_LoadJadwalDokter(dsDoctor.KDDOCTOR)
                        End If
                    End If
                End If
            Catch ex As Exception
                MsgBox("Keamanan belum dipasang, tolong kontak admin anda!", MsgBoxStyle.Exclamation, Me.Text)
                picRefreshListPasien.Enabled = False
            End Try
        Catch ex As Exception
            MsgBox("Load Security : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadJadwalDokter(ByVal Kodedokter As String)
        Try
            Dim hari = ""
            Select Case deDATE.DateTime.ToString("dddd")
                Case "Sunday"
                    hari = "MINGGU"
                Case "Monday"
                    hari = "SENIN"
                Case "Tuesday"
                    hari = "SELASA"
                Case "Wednesday"
                    hari = "RABU"
                Case "Thursday"
                    hari = "KAMIS"
                Case "Friday"
                    hari = "JUMAT"
                Case "Saturday"
                    hari = "SABTU"
                Case Else
                    hari = "-"
            End Select

            Dim oDoctor As New Reference.clsDoctor
            Dim dsDoctorJadwal = oDoctor.GetDataJadwalDokter(Kodedokter, hari)
            If dsDoctorJadwal IsNot Nothing Then
                lblJadwalSIP.Text = dsDoctorJadwal.JAMDARI.ToString("HH:mm") & "-" & dsDoctorJadwal.JAMSAMPAI.ToString("HH:mm")
            Else
                lblJadwalSIP.Text = "-"
            End If
        Catch ex As Exception
            MsgBox("Jadwal Dokter : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadView()
        Dim oDepartment As New Reference.clsDepartment
        Dim dsDepartment1 = oDepartment.GetData(grdKDDEPARTMENT.EditValue)

        If dsDepartment1 IsNot Nothing Then
            If dsDepartment1.KDDEPARTMENT_BPJS = "IGD" Then

                AddToolStripMenuItem.Visible = False
                LaporanOperasiToolStripMenuItem.Visible = False
                LaporanTindakanToolStripMenuItem.Visible = False
                PemeriksaanAwalToolStripMenuItem.Visible = False
                tab1.PageVisible = False
                tab2.PageVisible = False

                If sisDOkter = False Then
                    AsesmenAwalPerawatIGDToolStripMenuItem.Visible = True
                    AsesmenMedisIGDToolStripMenuItem.Visible = False
                    tab4.Visible = False
                Else
                    AsesmenAwalPerawatIGDToolStripMenuItem.Visible = False
                    AsesmenMedisIGDToolStripMenuItem.Visible = True
                    tab4.Visible = True
                End If
            Else
                AsesmenAwalPerawatIGDToolStripMenuItem.Visible = False
                AsesmenMedisIGDToolStripMenuItem.Visible = False
                tab4.Visible = False

                If sisDOkter = False Then
                    LaporanOperasiToolStripMenuItem.Visible = False
                    LaporanTindakanToolStripMenuItem.Visible = False
                    AddToolStripMenuItem.Visible = False
                    PemeriksaanAwalToolStripMenuItem.Visible = True
                    tab1.PageVisible = False
                    tab2.PageVisible = False
                Else
                    LaporanOperasiToolStripMenuItem.Visible = True
                    LaporanTindakanToolStripMenuItem.Visible = True
                    AddToolStripMenuItem.Visible = True
                    PemeriksaanAwalToolStripMenuItem.Visible = False
                    tab1.PageVisible = True
                    tab2.PageVisible = True
                End If
            End If
        End If
    End Sub
    Private Sub fn_EmptyMe()
        'picGAMBAR2.Image = CType(My.Resources.ResourceManager.GetObject("gambar"), Image)
        If grdKDDEPARTMENT.EditValue = "1058" Then
            If txtOBJEKTIF_JENISKELAMIN.Text = "Laki-laki" Then
                picGAMBAR2.Image = My.Resources.ResourceManager.GetObject("gambarwajah")
            Else
                picGAMBAR2.Image = My.Resources.ResourceManager.GetObject("gambarwajahlakilaki")
            End If
        Else
            picGAMBAR2.Image = My.Resources.ResourceManager.GetObject("gambar")
        End If

        sPicture = Nothing

        grvHystori.OptionsSelection.MultiSelect = True
        grvHystori.SelectAll()
        grvHystori.DeleteSelectedRows()
        grvHystori.OptionsSelection.MultiSelect = False

        grvUpload.OptionsSelection.MultiSelect = True
        grvUpload.SelectAll()
        grvUpload.DeleteSelectedRows()
        grvUpload.OptionsSelection.MultiSelect = False

        PdfViewerCPPTRawatJalan.CloseDocument()
        PdfViewerResume.CloseDocument()
        PdfViewer_IGD.CloseDocument()
        PdfViewerPerawatIGD.CloseDocument()
        PdfViewerCPPTRawatInap.CloseDocument()
        PdfViewerAssemenAwalMedisRawatInap.CloseDocument()
        PdfViewerLaporanOperasi.CloseDocument()
        PdfViewerTransferInternal.CloseDocument()
        PdfViewerSuratKeterangan.CloseDocument()
        PdfViewerFormulirMPP.CloseDocument()
        PdfViewerSkinAnalysis.CloseDocument()
        PdfViewerMCU.CloseDocument()

        txtKDPENDAFTARAN.ResetText()
        txtKDCUSTOMER.ResetText()
        deDATETANGGALLAHIR.DateTime = Now
        deTANGGALDATANG.DateTime = Now
        txtNAMAPASIEN.ResetText()
        txtOBJEKTIF_JENISKELAMIN.ResetText()
        txtDIAGNOOSA.ResetText()
        txtOBJEKTIF_UMUR.ResetText()
        txtPenjamin.ResetText()
        txtKARTUBPJS.ResetText()
        txtKODEBOOKING.ResetText()
        txtNOMORSEP.ResetText()
        txtOBJEKTIF_KESADARAN.ResetText()
        txtOBJEKTIF_IMT.ResetText()
        txtOBJEKTIF_BBIDEAL.ResetText()
        txtOBJEKTIF_BBDITURUNKAN.ResetText()
        txtGoalOfTreatment.ResetText()
        txtEdukasi.ResetText()
        txtFrekuensi.ResetText()

        txtTINDAKLANJUT.ResetText()
        txtGRANDTOTAL.Text = "0"

        grvDetailResep.OptionsSelection.MultiSelect = True
        grvDetailResep.SelectAll()
        grvDetailResep.DeleteSelectedRows()
        grvDetailResep.OptionsSelection.MultiSelect = False

        grvOBATRACIKAN.OptionsSelection.MultiSelect = True
        grvOBATRACIKAN.SelectAll()
        grvOBATRACIKAN.DeleteSelectedRows()
        grvOBATRACIKAN.OptionsSelection.MultiSelect = False

        grvTindakan.OptionsSelection.MultiSelect = True
        grvTindakan.SelectAll()
        grvTindakan.DeleteSelectedRows()
        grvTindakan.OptionsSelection.MultiSelect = False

        grvTindakanPoli.OptionsSelection.MultiSelect = True
        grvTindakanPoli.SelectAll()
        grvTindakanPoli.DeleteSelectedRows()
        grvTindakanPoli.OptionsSelection.MultiSelect = False

        grvDiagnosaPenyerta.OptionsSelection.MultiSelect = True
        grvDiagnosaPenyerta.SelectAll()
        grvDiagnosaPenyerta.DeleteSelectedRows()
        grvDiagnosaPenyerta.OptionsSelection.MultiSelect = False

        txtDIAGNOOSA.ResetText()
        txtDESKRIPSI.ResetText()
    End Sub
    Private Sub fn_LoadDataList()
        Try
            grvListantrian.OptionsSelection.MultiSelect = True
            grvListantrian.SelectAll()
            grvListantrian.DeleteSelectedRows()
            grvListantrian.OptionsSelection.MultiSelect = False

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
            SQL &= "Cek = B.ISRESUME "
            SQL &= ",AntrianDokter = (SELECT CASE B.ISRESUME WHEN 0 THEN (SELECT CASE B.ANTRIANDOKTER WHEN 0 THEN 'X' ELSE 'A' + RIGHT('000' + CONVERT(NVARCHAR(50), B.ANTRIANDOKTER), 3) END) ELSE 'Y' + RIGHT('000' + CONVERT(NVARCHAR(50), B.ANTRIANDOKTER), 3) END) "
            SQL &= ",Pasien = D.NAME_DISPLAY + ' ' + IIf(D.FRONT_TITLE IS NULL, '', D.FRONT_TITLE + ' ') + D.BACK_TITLE "
            SQL &= ",Keterangan = B.KETERANGAN_TINDAKLANJUT "
            SQL &= ",B.ISPERAWAT "
            SQL &= ",KeteranganPenunjangHariIni = ISNULL((SELECT KDREG FROM S_PENDAFTARAN_PENUNJANGHARIINISELESAI WHERE B.KDREG = KDREG), '') "
            SQL &= "FROM "
            SQL &= "S_PENDAFTARAN_H B "
            SQL &= "INNER JOIN M_DOCTOR C "
            SQL &= "ON B.KDDOCTOR = C.KDDOCTOR "
            SQL &= "INNER JOIN M_CUSTOMER D "
            SQL &= "ON B.KDCUSTOMER = D.KDCUSTOMER "
            SQL &= "INNER JOIN M_DIAGNOSAICD10 E "
            SQL &= "ON B.KDDIAGNOSA = E.KDDIAGNOSA "
            SQL &= "INNER JOIN M_DEBTOR F "
            SQL &= "ON B.KDDEBTOR = F.KDDEBTOR "
            SQL &= "WHERE CONVERT(VARCHAR(8), B.DATE, 112) = '" & deDATE.DateTime.ToString("yyyyMMdd") & "' "
            If grdKDDEPARTMENT.Text = "FISIOTERAPI" Then
                'FISIOTERAFI
                SQL &= "AND B.STATUSDAFTAR <> '3' "
                SQL &= "AND B.CATEGORY = 1 "
                SQL &= "AND CONVERT(NVARCHAR(50), B.KONSUL) = '1052' "
            Else
                SQL &= "AND CONVERT(NVARCHAR(50), B.KDDEPARTMENT) = '" & grdKDDEPARTMENT.EditValue & "' "
                SQL &= "AND CONVERT(NVARCHAR(50), B.KDDOCTOR) = '" & grdDPJP.EditValue & "' "
                SQL &= "AND B.STATUSDAFTAR <> '3' "
                SQL &= "AND B.CATEGORY = 1 "
                SQL &= "AND B.KONSUL <> '1052' "
            End If
            'SQL &= "AND B.KDDEPARTMENT = " & grdKDDEPARTMENT.EditValue & " "
            'SQL &= "AND B.KDDOCTOR = " & grdDPJP.EditValue & " "
            'SQL &= "AND B.STATUSDAFTAR <> '3' "
            'SQL &= "AND B.CATEGORY = 1 "
            SQL &= ") Z "
            SQL &= "ORDER BY Z.ANTRIANDOKTER ASC "

            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "S_LISTPASIEN_ANTRIAN")

            grdListantrian.DataSource = ds.Tables("S_LISTPASIEN_ANTRIAN")
            grdListantrian.ForceInitialize()

            If oConn.State = ConnectionState.Open Then
                oConn.Close()
            End If

            'fn_LoadFormatData()

            'grvListantrian.Columns("Cek").VisibleIndex = -1
            grvListantrian.Columns("Keterangan").VisibleIndex = -1
            grvListantrian.Columns("ISPERAWAT").VisibleIndex = -1
            grvListantrian.Columns("KeteranganPenunjangHariIni").VisibleIndex = -1
            grvListantrian.BestFitColumns()

        Catch oErr As Exception
            MsgBox("Load List Pasien Data : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadData(ByVal KDDEPARTMENT As String, ByVal KDDOCTOR As String)
        Try
            If isLoad_Splash = False Then Exit Sub

            SplashScreenManager.ShowForm(Me, GetType(frmLoadingPanel), True, True, False)

            SplashScreenManager.Default.SetWaitFormCaption("Processing Load Data.....")

            grvListPasien.OptionsSelection.MultiSelect = True
            grvListPasien.SelectAll()
            grvListPasien.DeleteSelectedRows()
            grvListPasien.OptionsSelection.MultiSelect = False

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
            SQL &= "NoRegister = B.KDREG "
            SQL &= ",Antrian = (SELECT CASE B.ISRESUME WHEN 0 THEN (SELECT CASE B.ANTRIANDOKTER WHEN 0 THEN 'X' ELSE 'A' + RIGHT('000' + CONVERT(NVARCHAR(50), B.ANTRIANDOKTER), 3) END) ELSE 'Y' + RIGHT('000' + CONVERT(NVARCHAR(50), B.ANTRIANDOKTER), 3) END) "
            'SQL &= ",AntrianDokter = B.ANTRIANDOKTER "
            SQL &= ",KodeBooking = B.NOMORANTRIAN "
            SQL &= ",Pasien = D.NAME_DISPLAY + ' ' + IIf(D.FRONT_TITLE IS NULL, '', D.FRONT_TITLE + ' ') + D.BACK_TITLE "
            SQL &= ",Dokter = C.NAME_DISPLAY "
            SQL &= ",SIP = C.SIP "
            SQL &= ",B.KDCUSTOMER "
            SQL &= ",B.USIA "
            SQL &= ",D.TANGGALLAHIR "
            SQL &= ",DIAGNOSA = E.KDDIAGNOSA + ' - ' + E.DESCRIPTION "
            SQL &= ",B.DATE "
            SQL &= ",WaktuPeriksa = ISNULL((SELECT FORMAT(WAKTU, 'HH:mm:ss') FROM Z_ANTRIAN_POLI WHERE B.NOMORANTRIAN = KODEBOOKING AND TAKSID = 4) ,'')  "
            SQL &= ",WaktuSelisih = ISNULL((SELECT  DATEDIFF(MINUTE, B.DATE, WAKTU) FROM Z_ANTRIAN_POLI WHERE B.NOMORANTRIAN = KODEBOOKING AND TAKSID = 4) ,'') "
            'SQL &= ",WaktuSelesai = ISNULL((SELECT FORMAT(WAKTU, 'HH:mm:ss') FROM Z_ANTRIAN_POLI WHERE B.NOMORANTRIAN = KODEBOOKING AND TAKSID = 5) ,'')  "
            SQL &= ",PENJAMIN = F.NAME_DISPLAY "
            SQL &= ",B.NOMORSEP "
            SQL &= ",B.KARTUBPJS "
            SQL &= ",Cek = B.ISRESUME "
            SQL &= ",C.KDDOCTOR "
            SQL &= ",D.JK "
            SQL &= ",B.ISPERAWAT "
            SQL &= ",Keterangan = B.KETERANGAN_TINDAKLANJUT "
            SQL &= ",KeteranganPenunjangHariIni = ISNULL((SELECT KDREG FROM S_PENDAFTARAN_PENUNJANGHARIINISELESAI WHERE B.KDREG = KDREG), '') "
            SQL &= ",B.ADDRESS_STREET "
            SQL &= ",D.TEMPATLAHIR "
            SQL &= ",B.KDDEPARTMENT "
            SQL &= "FROM "
            SQL &= "S_PENDAFTARAN_H B "
            SQL &= "INNER JOIN M_DOCTOR C "
            SQL &= "ON B.KDDOCTOR = C.KDDOCTOR "
            SQL &= "INNER JOIN M_CUSTOMER D "
            SQL &= "ON B.KDCUSTOMER = D.KDCUSTOMER "
            SQL &= "INNER JOIN M_DIAGNOSAICD10 E "
            SQL &= "ON B.KDDIAGNOSA = E.KDDIAGNOSA "
            SQL &= "INNER JOIN M_DEBTOR F "
            SQL &= "ON B.KDDEBTOR = F.KDDEBTOR "
            SQL &= "WHERE CONVERT(VARCHAR(8), B.DATE, 112) = '" & deDATE.DateTime.ToString("yyyyMMdd") & "' "
            If grdKDDEPARTMENT.Text = "FISIOTERAPI" Then
                'FISIOTERAFI
                SQL &= "AND B.STATUSDAFTAR <> '3' "
                SQL &= "AND B.CATEGORY = 1 "
                SQL &= "AND CONVERT(NVARCHAR(50), B.KONSUL) = '1052' "
            Else
                SQL &= "AND CONVERT(NVARCHAR(50), B.KDDEPARTMENT) = '" & KDDEPARTMENT & "' "
                SQL &= "AND CONVERT(NVARCHAR(50), B.KDDOCTOR) = '" & KDDOCTOR & "' "
                SQL &= "AND B.STATUSDAFTAR <> '3' "
                SQL &= "AND B.CATEGORY = 1 "
                SQL &= "AND B.KONSUL <> '1052' "
            End If

            SQL &= ") Z "
            SQL &= "ORDER BY Z.Antrian, Z.NoRegister ASC "

            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "S_LISTPASIEN")

            grdListPasien.DataSource = ds.Tables("S_LISTPASIEN")
            grdListPasien.ForceInitialize()

            If oConn.State = ConnectionState.Open Then
                oConn.Close()
            End If

            fn_LoadFormatData()

            'grvListPasien.BestFitColumns()
            Rata()

            SplashScreenManager.CloseForm(False)
        Catch oErr As Exception
            SplashScreenManager.CloseForm(False)
            MsgBox("Load List Pasien Data : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadFormatData()
        For iLoop As Integer = 0 To grvListPasien.Columns.Count - 1
            If grvListPasien.Columns(iLoop).ColumnType.Name = "Decimal" Then
                grvListPasien.Columns(iLoop).DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                grvListPasien.Columns(iLoop).DisplayFormat.FormatString = "{0:n2}"
                grvListPasien.Columns(iLoop).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
                grvListPasien.GroupSummary.Add(DevExpress.Data.SummaryItemType.Sum, grvListPasien.Columns(iLoop).FieldName, grvListPasien.Columns(iLoop),
                                     "{0:n2}")
                grvListPasien.Columns(iLoop).SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                grvListPasien.Columns(iLoop).SummaryItem.DisplayFormat = "{0:n2}"
            ElseIf grvListPasien.Columns(iLoop).ColumnType.Name = "DateTime" Then
                grvListPasien.Columns(iLoop).DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
                grvListPasien.Columns(iLoop).DisplayFormat.FormatString = "{0:HH:mm:ss}"
            End If
        Next

        'grvListPasien.Columns("Antrian").Caption = "Antrian"
        grvListPasien.Columns("PENJAMIN").Caption = "Penjamin"
        grvListPasien.Columns("DATE").Caption = "Jam Pendaftaran"
        grvListPasien.Columns("KodeBooking").VisibleIndex = -1
        grvListPasien.Columns("NoRegister").VisibleIndex = -1
        grvListPasien.Columns("Dokter").VisibleIndex = -1
        grvListPasien.Columns("SIP").VisibleIndex = -1
        grvListPasien.Columns("KDCUSTOMER").VisibleIndex = -1
        grvListPasien.Columns("USIA").VisibleIndex = -1
        grvListPasien.Columns("TANGGALLAHIR").VisibleIndex = -1
        grvListPasien.Columns("DIAGNOSA").VisibleIndex = -1
        grvListPasien.Columns("NOMORSEP").VisibleIndex = -1
        grvListPasien.Columns("KARTUBPJS").VisibleIndex = -1
        grvListPasien.Columns("Cek").VisibleIndex = -1
        grvListPasien.Columns("KDDOCTOR").VisibleIndex = -1
        grvListPasien.Columns("JK").VisibleIndex = -1
        grvListPasien.Columns("ISPERAWAT").VisibleIndex = -1
        grvListPasien.Columns("WaktuSelisih").VisibleIndex = -1
        grvListPasien.Columns("ADDRESS_STREET").VisibleIndex = -1
        grvListPasien.Columns("TEMPATLAHIR").VisibleIndex = -1
        grvListPasien.Columns("KeteranganPenunjangHariIni").VisibleIndex = -1
        grvListPasien.Columns("KDDEPARTMENT").VisibleIndex = -1
    End Sub
    Private Sub Rata()
        Try
            Dim totalMenit As Integer = 0
            Dim jumlahBaris As Integer = 0

            For i As Integer = 0 To grvListPasien.RowCount - 1
                If grvListPasien.GetRowCellValue(i, "WaktuPeriksa") <> "" Then
                    'jumlahBaris += 1
                    'totalMenit += grvListPasien.GetRowCellValue(i, "WaktuSelisih")

                    totalMenit += Convert.ToInt32(grvListPasien.GetRowCellValue(i, "WaktuSelisih"))
                    jumlahBaris += 1

                End If
            Next

            Dim rataRata As Double = totalMenit / jumlahBaris
            Dim jam As Integer = Math.Floor(rataRata / 60)
            Dim menit As Integer = CInt(Math.Round(rataRata Mod 60))

            txtLayananRataRata.Text = $"{jam} jam {menit} menit"

            If rataRata >= 50 Then
                'txtLayananRataRata.ForeColor = Color.Yellow
                txtLayananRataRata.BackColor = Color.Red
            Else
                'txtLayananRataRata.ForeColor = Color.Yellow
                txtLayananRataRata.BackColor = Color.LawnGreen
            End If
            'Console.WriteLine($"Rata-rata waktu tunggu: {jam} jam {menit} menit")

            'Dim desimalJam As Double = sJumlahWaktuMenit / sJumlahPasien

            'Dim jam As Integer = Math.Floor(desimalJam)
            'Dim menit As Integer = CInt(Math.Round((desimalJam - jam) * 60))

            'txtLayananRataRata.Text = $"{jam} jam {menit} menit"

        Catch ex As Exception

        End Try
    End Sub
    Private Sub fn_LoadHystori(ByVal nopasien As String)
        Try
            grvHystori.OptionsSelection.MultiSelect = True
            grvHystori.SelectAll()
            grvHystori.DeleteSelectedRows()
            grvHystori.OptionsSelection.MultiSelect = False

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

            SQL = "SELECT * FROM ( "
            SQL &= "SELECT "
            SQL &= "Kategori = CONVERT(VARCHAR(8), A.DATE, 103) + ', ' + A.TUJUAN + ', ' + A.DOKTER "
            SQL &= ",Tipe ='DIAGNOSA UTAMA' "
            SQL &= ",NoRegister = A.KDPENDAFTARAN "
            SQL &= ",NoTransaksi = A.KDREQRECIPE "
            SQL &= ",Deskripsi = A.DIAGNOSA "
            SQL &= ",PILIH = CONVERT(BIT, 0) "
            SQL &= ",KDITEM = '' "
            SQL &= ",KDUOM = '' "
            SQL &= "FROM "
            SQL &= "S_REQ_RECIPE_H A "
            SQL &= "WHERE A.KDCUSTOMER = '" & nopasien & "' "

            SQL &= "UNION "

            SQL &= "SELECT "
            SQL &= "Kategori = CONVERT(VARCHAR(8), A.DATE, 103) + ', ' + A.TUJUAN + ', ' + A.DOKTER "
            SQL &= ",Tipe ='XRESEP' "
            SQL &= ",NoRegister = A.KDPENDAFTARAN "
            SQL &= ",NoTransaksi = A.KDREQRECIPE "
            SQL &= ",Deskripsi = B.NAMAOBAT + ', ' + B.SATUAN + ', ' + CONVERT(NVARCHAR(5), B.QTY) + ', ' + B.REMARKS_DOKTER + ', ' + B.SIGNA + ', ' + B.CARAPAKAI "
            SQL &= ",PILIH = CONVERT(BIT, 0) "
            SQL &= ",B.KDITEM "
            SQL &= ",B.KDUOM "
            SQL &= "FROM "
            SQL &= "S_REQ_RECIPE_H A "
            SQL &= "INNER JOIN S_REQ_RECIPE_D B "
            SQL &= "ON A.KDREQRECIPE = B.KDREQRECIPE "
            SQL &= "WHERE A.KDCUSTOMER = '" & nopasien & "' "
            SQL &= ") Z "
            SQL &= "ORDER BY Z.Kategori DESC "

            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "S_HISTORY")

            BindingSource_all.DataSource = ds.Tables("S_HISTORY")

            grdHystori.DataSource = BindingSource_all
            grdHystori.ForceInitialize()

            If oConn.State = ConnectionState.Open Then
                oConn.Close()
            End If

            fn_SetFormatHystori()

            grvHystori.BestFitColumns()

        Catch oErr As Exception
            MsgBox("Load History Data : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_SetFormatHystori()
        For iLoop As Integer = 0 To grvHystori.Columns.Count - 1
            If grvHystori.Columns(iLoop).ColumnType.Name = "Decimal" Then
                grvHystori.Columns(iLoop).DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                grvHystori.Columns(iLoop).DisplayFormat.FormatString = "{0:n2}"
                grvHystori.Columns(iLoop).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
            ElseIf grvHystori.Columns(iLoop).ColumnType.Name = "DateTime" Then
                grvHystori.Columns(iLoop).DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
                grvHystori.Columns(iLoop).DisplayFormat.FormatString = "{0:dd/MM/yyyy HH:mm:ss}"
            End If
        Next

        grvHystori.Columns("NoRegister").VisibleIndex = -1
        grvHystori.Columns("NoTransaksi").VisibleIndex = -1
        grvHystori.Columns("KDITEM").VisibleIndex = -1
        grvHystori.Columns("KDUOM").VisibleIndex = -1
        grvHystori.Columns("Kategori").Group()
        grvHystori.Columns("Tipe").Group()
        grvHystori.ExpandAllGroups()
    End Sub
    Private Sub fn_LoadDataUpload(ByVal RM As String)
        Try
            grvUpload.OptionsSelection.MultiSelect = True
            grvUpload.SelectAll()
            grvUpload.DeleteSelectedRows()
            grvUpload.OptionsSelection.MultiSelect = False

            Dim oBrigging As New Brigging.clsSetKoneksi

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

            SQL = "SELECT * "
            SQL &= "FROM ( "
            SQL &= "SELECT "
            'SQL &= "Kategori = CONVERT(VARCHAR(8), B.DATE, 103) + ', ' + E.NAME_DISPLAY + ', ' + F.NAME_DISPLAY "
            SQL &= "Tanggal = B.DATE "
            SQL &= ",NamaUpload = D.DESCRIPTION "
            SQL &= ",Catatan = C.MEMO "
            SQL &= ",AlamatUpload = C.ALAMATAKHIR_PDF "
            SQL &= ",Type = C.TYPE "
            SQL &= ",Kodeupload = C.KDPDFTRANSAKSI "
            SQL &= "FROM "
            SQL &= "S_PDF_H A "
            SQL &= "INNER JOIN S_PENDAFTARAN_H B "
            SQL &= "ON A.KDREG = B.KDREG "
            SQL &= "INNER JOIN S_PDF_D C "
            SQL &= "ON A.KDPDFTRANSAKSI = C.KDPDFTRANSAKSI "
            SQL &= "INNER JOIN M_PDF D "
            SQL &= "ON C.KDPDF = D.KDPDF "
            SQL &= "INNER JOIN M_DEPARTMENT E "
            SQL &= "ON B.KDDEPARTMENT = E.KDDEPARTMENT "
            SQL &= "INNER JOIN M_DOCTOR F "
            SQL &= "ON B.KDDOCTOR = F.KDDOCTOR "
            'SQL &= "WHERE B.KDREG = '" & lblRegister.Text & "' "
            SQL &= "WHERE B.KDCUSTOMER = '" & RM & "' "
            SQL &= "AND D.DESCRIPTION <> 'ADMINISTRASI' "

            SQL &= "UNION "

            SQL &= "SELECT "
            'SQL &= "Kategori = CONVERT(VARCHAR(8), B.DATE, 103) + ', ' + E.NAME_DISPLAY + ', ' + F.NAME_DISPLAY "
            SQL &= "Tanggal = B.DATE "
            SQL &= ",NamaUpload = 'RONTGEN' "
            SQL &= ",Catatan = A.EXAMDESC "
            SQL &= ",AlamatUpload = A.EXAMDESC "
            SQL &= ",Type = 'RONTGEN' "
            SQL &= ",Kodeupload = CONVERT(NVARCHAR(50), A.KDRAD) "
            SQL &= "FROM "
            SQL &= "S_RADIOLOGI_H A "
            SQL &= "INNER JOIN S_PENDAFTARAN_H B "
            SQL &= "ON A.KDREG = B.KDREG "
            SQL &= "INNER JOIN M_DEPARTMENT E "
            SQL &= "ON B.KDDEPARTMENT = E.KDDEPARTMENT "
            SQL &= "INNER JOIN M_DOCTOR F "
            SQL &= "ON B.KDDOCTOR = F.KDDOCTOR "
            SQL &= "WHERE B.KDCUSTOMER = '" & RM & "' "
            'SQL &= "WHERE "
            'SQL &= "A.KDREG = '" & lblRegister.Text & "' "

            SQL &= "UNION "

            SQL &= "SELECT "
            'SQL &= "Kategori = CONVERT(VARCHAR(8), B.DATE, 103) + ', ' + E.NAME_DISPLAY + ', ' + F.NAME_DISPLAY "
            SQL &= "Tanggal = B.DATE "
            SQL &= ",NamaUpload = 'USG' "
            SQL &= ",Catatan = A.KDTARIF "
            SQL &= ",AlamatUpload = A.GEJALA "
            SQL &= ",Type = 'USG' "
            SQL &= ",Kodeupload =  CONVERT(NVARCHAR(50), A.KDUSG)  "
            SQL &= "FROM "
            SQL &= "S_USG_H A "
            SQL &= "INNER JOIN S_PENDAFTARAN_H B "
            SQL &= "ON A.KDREG = B.KDREG "
            SQL &= "INNER JOIN M_DEPARTMENT E "
            SQL &= "ON B.KDDEPARTMENT = E.KDDEPARTMENT "
            SQL &= "INNER JOIN M_DOCTOR F "
            SQL &= "ON B.KDDOCTOR = F.KDDOCTOR "
            'SQL &= "WHERE "
            'SQL &= "A.KDREG = '" & lblRegister.Text & "' "
            SQL &= "WHERE B.KDCUSTOMER = '" & RM & "' "

            SQL &= "UNION "

            SQL &= "SELECT "
            'SQL &= "Kategori = CONVERT(VARCHAR(8), B.DATE, 103) + ', ' + E.NAME_DISPLAY + ', ' + F.NAME_DISPLAY "
            SQL &= "Tanggal = B.DATE "
            SQL &= ",NamaUpload = 'LABORATORIUM' "
            SQL &= ",Catatan = H.NAMA_TARIF "
            SQL &= ",AlamatUpload = '" & sAlamatSimpanPDFLaboratorium & "' "
            SQL &= ",Type = 'browser' "
            SQL &= ",Kodeupload = A.KDTRANSAKSILAB "
            SQL &= "FROM "
            SQL &= "S_TRANSAKSILAB_H A "
            SQL &= "INNER JOIN S_PENDAFTARAN_H B "
            SQL &= "ON A.KDREG = B.KDREG "
            SQL &= "INNER JOIN M_DEPARTMENT E "
            SQL &= "ON B.KDDEPARTMENT = E.KDDEPARTMENT "
            SQL &= "INNER JOIN M_DOCTOR F "
            SQL &= "ON B.KDDOCTOR = F.KDDOCTOR "
            SQL &= "INNER JOIN S_TRANSAKSILAB_D G "
            SQL &= "ON A.KDTRANSAKSILAB = G.KDTRANSAKSILAB "
            SQL &= "INNER JOIN M_TARIF_NEW AS H "
            SQL &= "ON G.KDTARIF = H.KDTARIF "
            SQL &= "WHERE B.KDCUSTOMER = '" & RM & "' "
            'SQL &= "WHERE B.KDREG = '" & lblRegister.Text & "' "


            SQL &= "UNION "

            SQL &= "SELECT "
            'SQL &= "Kategori = CONVERT(VARCHAR(8), B.DATE, 103) + ', ' + E.NAME_DISPLAY + ', ' + F.NAME_DISPLAY "
            SQL &= "Tanggal = D.DATE "
            SQL &= ",NamaUpload = 'RADIOLOGI' "
            SQL &= ",Catatan = B.NAMA_TARIF "
            SQL &= ",AlamatUpload = C.NORADIOLOGI "
            SQL &= ",Type = 'APIRADIOLOGI' "
            SQL &= ",Kodeupload = A.KDTRANSAKSI "
            SQL &= "FROM "
            SQL &= "S_TRANSAKSI_RJ A "
            SQL &= "INNER JOIN M_TARIF_NEW AS B "
            SQL &= "ON A.KDTARIF = B.KDTARIF "
            SQL &= "INNER JOIN S_TRANSAKSI_H AS C "
            SQL &= "ON A.KDTRANSAKSI = C.KDTRANSAKSI "
            SQL &= "INNER JOIN S_PENDAFTARAN_H D "
            SQL &= "ON C.KDREG = D.KDREG "
            SQL &= "WHERE D.KDCUSTOMER = '" & RM & "' "

            SQL &= "UNION "

            SQL &= "SELECT "
            'SQL &= "Kategori = CONVERT(VARCHAR(8), B.DATE, 103) + ', ' + E.NAME_DISPLAY + ', ' + F.NAME_DISPLAY "
            SQL &= "Tanggal = D.DATE "
            SQL &= ",NamaUpload = 'EKG-ECHO (RJ)' "
            SQL &= ",Catatan = B.NAMA_TARIF "
            SQL &= ",AlamatUpload = E.NORADIOLOGI "
            SQL &= ",Type = 'APIRADIOLOGI' "
            SQL &= ",Kodeupload = A.KDTRANSAKSIRJ "
            SQL &= "FROM "
            SQL &= "S_TRANSAKSIRJ_D A "
            SQL &= "INNER JOIN M_TARIF_NEW AS B "
            SQL &= "ON A.KDTARIF = B.KDTARIF "
            SQL &= "INNER JOIN S_TRANSAKSIRJ_H AS C "
            SQL &= "ON A.KDTRANSAKSIRJ = C.KDTRANSAKSIRJ "
            SQL &= "INNER JOIN S_PENDAFTARAN_H D "
            SQL &= "ON C.KDREG = D.KDREG "
            SQL &= "INNER JOIN S_TRANSAKSI_EKG_ECHO E "
            SQL &= "ON C.KDTRANSAKSIRJ = E.KDTRANSAKSIRI "
            SQL &= "WHERE D.KDCUSTOMER = '" & RM & "' "

            SQL &= "UNION "

            SQL &= "SELECT "
            'SQL &= "Kategori = CONVERT(VARCHAR(8), B.DATE, 103) + ', ' + E.NAME_DISPLAY + ', ' + F.NAME_DISPLAY "
            SQL &= "Tanggal = D.DATE "
            SQL &= ",NamaUpload = 'EKG-ECHO (RI)' "
            SQL &= ",Catatan = B.NAMA_TARIF "
            SQL &= ",AlamatUpload = E.NORADIOLOGI "
            SQL &= ",Type = 'APIRADIOLOGI' "
            SQL &= ",Kodeupload = A.KDTRANSAKSIRI "
            SQL &= "FROM "
            SQL &= "S_TRANSAKSIRI_D A "
            SQL &= "INNER JOIN M_TARIF_NEW AS B "
            SQL &= "ON A.KDTARIF = B.KDTARIF "
            SQL &= "INNER JOIN S_TRANSAKSIRI_H AS C "
            SQL &= "ON A.KDTRANSAKSIRI = C.KDTRANSAKSIRI "
            SQL &= "INNER JOIN S_PENDAFTARAN_H D "
            SQL &= "ON C.KDREG = D.KDREG "
            SQL &= "INNER JOIN S_TRANSAKSI_EKG_ECHO E "
            SQL &= "ON C.KDTRANSAKSIRI = E.KDTRANSAKSIRI "
            SQL &= "WHERE D.KDCUSTOMER = '" & RM & "' "

            SQL &= ") Z "
            SQL &= "ORDER BY "
            SQL &= "Z.Tanggal DESC "


            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "S_upload")

            grdUpload.DataSource = ds.Tables("S_upload")
            grdUpload.ForceInitialize()

            If oConn.State = ConnectionState.Open Then
                oConn.Close()
            End If

            fn_LoadFormatDataUpload()

            grvUpload.Columns("NamaUpload").Caption = "Jenis Pemeriksaan"
            grvUpload.Columns("AlamatUpload").VisibleIndex = -1
            grvUpload.Columns("Type").VisibleIndex = -1
            grvUpload.Columns("Kodeupload").VisibleIndex = -1

            grvUpload.BestFitColumns()

        Catch oErr As Exception
            MsgBox("Load List Pasien Data : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadFormatDataUpload()
        For iLoop As Integer = 0 To grvUpload.Columns.Count - 1
            If grvUpload.Columns(iLoop).ColumnType.Name = "Decimal" Then
                grvUpload.Columns(iLoop).DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                grvUpload.Columns(iLoop).DisplayFormat.FormatString = "{0:n2}"
                grvUpload.Columns(iLoop).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
            ElseIf grvUpload.Columns(iLoop).ColumnType.Name = "DateTime" Then
                grvUpload.Columns(iLoop).DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
                grvUpload.Columns(iLoop).DisplayFormat.FormatString = "{0:dd/MM/yyyy}"
            End If
        Next
    End Sub
    Private Sub fn_DokterBersama(ByVal kdcustomer As String)
        Try
            grvDokterBersama.OptionsSelection.MultiSelect = True
            grvDokterBersama.SelectAll()
            grvDokterBersama.DeleteSelectedRows()
            grvDokterBersama.OptionsSelection.MultiSelect = False

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
            SQL &= "A.KDREG "
            SQL &= ",DARIDOKTER = C.NAME_DISPLAY "
            SQL &= ",KEPADADOKTER = B.NAME_DISPLAY "
            SQL &= ",KETERANGAN = 'DPJP KE ' + CONVERT(NVARCHAR(2), A.SEQ)  "
            SQL &= ",A.ISIKONSUL "
            SQL &= ",A.JAWABKONSUL "
            SQL &= ",A.SEQ "
            SQL &= ",KDDOCTOR_DARI "
            SQL &= ",KDDOCTOR_KEPADA "
            SQL &= ",TGLKONSUL = A.TANGGAL_ISIKONSUL "
            SQL &= ",A.ISIKONSUL "
            SQL &= ",TGLJAWAB = A.TANGGAL_JAWABKONSUL "
            SQL &= ",A.JAWABKONSUL "
            SQL &= "FROM "
            SQL &= "I_TRACKING_KDDOCTOR A "
            SQL &= "INNER JOIN M_DOCTOR B "
            SQL &= "ON A.KDDOCTOR_KEPADA = B.KDDOCTOR "
            SQL &= "INNER JOIN M_DOCTOR C "
            SQL &= "ON A.KDDOCTOR_DARI = C.KDDOCTOR "
            SQL &= "INNER JOIN S_PENDAFTARAN_H D "
            SQL &= "ON A.KDREG = D.KDREG  "
            SQL &= "WHERE "
            SQL &= "D.KDCUSTOMER = '" & kdcustomer & "' "

            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "I_TRACKING_KDDOCTOR")

            grdDokterBersama.DataSource = ds.Tables("I_TRACKING_KDDOCTOR")
            grdDokterBersama.ForceInitialize()

            If oConn.State = ConnectionState.Open Then
                oConn.Close()
            End If

            grvDokterBersama.BestFitColumns()

        Catch oErr As Exception
            MsgBox("Load Tracking Dokter : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadDocument(ByVal KDREG As String, ByVal RM As String)
        If isLoad_Splash = False Then Exit Sub

        PdfViewerCPPTRawatJalan.CloseDocument()
        PdfViewerResume.CloseDocument()
        PdfViewer_IGD.CloseDocument()
        PdfViewerPerawatIGD.CloseDocument()
        PdfViewerCPPTRawatInap.CloseDocument()
        PdfViewerAssemenAwalMedisRawatInap.CloseDocument()
        PdfViewerTransferInternal.CloseDocument()
        PdfViewerSuratKeterangan.CloseDocument()
        PdfViewerFormulirMPP.CloseDocument()
        PdfViewerSkinAnalysis.CloseDocument
        PdfViewerMCU.CloseDocument()

        If XtraTabControl1.SelectedTabPageIndex = 0 Then
            SplashScreenManager.ShowForm(Me, GetType(frmLoadingPanel), True, True, False)

            SplashScreenManager.Default.SetWaitFormCaption("Processing data CPPT Rawat Jalan.....")

            If sCPPT_QRRJ = False Then
                Try
                    Dim oReqCPPT As New Transaksi.clsReqCPPT

                    Dim FolderSimpan = "C:/SIMRS/CPPT"

                    If Not Directory.Exists(FolderSimpan) Then
                        Directory.CreateDirectory(FolderSimpan)
                    Else
                        DeleteDirectory(FolderSimpan)
                        Directory.CreateDirectory(FolderSimpan)
                    End If

                    Dim dataList As New List(Of Byte())
                    Dim dsLoad() As Byte
                    Dim ds = oReqCPPT.GetDataByRMList(RM)
                    Dim rpt As New xtraDigital_CPPT_01

                    rpt.ShowPrintMarginsWarning = False
                    rpt.Watermark.Text = sWATERMARK

                    rpt.bindingSource.DataSource = ds
                    rpt.ExportToPdf(FolderSimpan & "HASIL" & ".pdf")
                    dataList.Add(File.ReadAllBytes(FolderSimpan & "HASIL" & ".pdf"))

                    If ds.Count > 0 Then
                        dsLoad = oSetKoneksi.MergeFilesByte(dataList)
                        Dim stream As New MemoryStream(dsLoad)
                        PdfViewerCPPTRawatJalan.LoadDocument(stream)
                    Else
                        PdfViewerCPPTRawatJalan.CloseDocument()
                    End If

                    'PdfViewerCPPTRawatJalan.LoadDocument("C:/SIMRS/CPPT/" & lblRegister.Text & ".pdf")

                    'Dim printTool As New DevExpress.XtraReports.UI.ReportPrintTool(rpt)
                    'printTool.ShowPreviewDialog(DevExpress.LookAndFeel.UserLookAndFeel.Default)
                Catch oErr As Exception
                    SplashScreenManager.CloseForm(False)
                    MsgBox("Cetak CPPT" & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            Else
                Try
                    Dim oReqCPPT As New Transaksi.clsReqCPPT

                    Dim FolderSimpan = "C:/SIMRS/CPPT"

                    If Not Directory.Exists(FolderSimpan) Then
                        Directory.CreateDirectory(FolderSimpan)
                    Else
                        DeleteDirectory(FolderSimpan)
                        Directory.CreateDirectory(FolderSimpan)
                    End If

                    Dim dataList As New List(Of Byte())
                    Dim dsLoad() As Byte
                    Dim ds = oReqCPPT.GetDataByRMList(RM)
                    Dim rpt As New xtraDigital_CPPT_01_QR

                    rpt.ShowPrintMarginsWarning = False
                    rpt.Watermark.Text = sWATERMARK

                    rpt.bindingSource.DataSource = ds
                    rpt.ExportToPdf(FolderSimpan & "HASIL" & ".pdf")
                    dataList.Add(File.ReadAllBytes(FolderSimpan & "HASIL" & ".pdf"))

                    If ds.Count > 0 Then
                        dsLoad = oSetKoneksi.MergeFilesByte(dataList)
                        Dim stream As New MemoryStream(dsLoad)
                        PdfViewerCPPTRawatJalan.LoadDocument(stream)
                    Else
                        PdfViewerCPPTRawatJalan.CloseDocument()
                    End If

                    'PdfViewerCPPTRawatJalan.LoadDocument("C:/SIMRS/CPPT/" & lblRegister.Text & ".pdf")

                    'Dim printTool As New DevExpress.XtraReports.UI.ReportPrintTool(rpt)
                    'printTool.ShowPreviewDialog(DevExpress.LookAndFeel.UserLookAndFeel.Default)
                Catch oErr As Exception
                    SplashScreenManager.CloseForm(False)
                    MsgBox("Cetak CPPT" & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            End If

            SplashScreenManager.CloseForm(False)
        ElseIf XtraTabControl1.SelectedTabPageIndex = 1 Then
            SplashScreenManager.ShowForm(Me, GetType(frmLoadingPanel), True, True, False)

            SplashScreenManager.Default.SetWaitFormCaption("Processing data CPPT Rawat Inap.....")

            Try
                Dim oReqCPPT As New Transaksi.clsReqCPPT

                Dim FolderSimpan = "C:/SIMRS/CPPT"

                If Not Directory.Exists(FolderSimpan) Then
                    Directory.CreateDirectory(FolderSimpan)
                Else
                    DeleteDirectory(FolderSimpan)
                    Directory.CreateDirectory(FolderSimpan)
                End If

                Dim dataList As New List(Of Byte())
                Dim dsLoad() As Byte
                Dim ds = oReqCPPT.GetDataByRMRIList(RM)
                Dim rpt As New xtraDigital_CPPT_01_RawatInap

                rpt.ShowPrintMarginsWarning = False
                rpt.Watermark.Text = sWATERMARK

                rpt.bindingSource.DataSource = ds
                rpt.ExportToPdf(FolderSimpan & "HASILRANAP" & ".pdf")
                dataList.Add(File.ReadAllBytes(FolderSimpan & "HASILRANAP" & ".pdf"))

                If ds.Count > 0 Then
                    dsLoad = oSetKoneksi.MergeFilesByte(dataList)
                    Dim stream As New MemoryStream(dsLoad)
                    PdfViewerCPPTRawatInap.LoadDocument(stream)
                Else
                    PdfViewerCPPTRawatInap.CloseDocument()
                End If

            Catch oErr As Exception
                MsgBox("Cetak CPPT" & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            End Try

            SplashScreenManager.CloseForm(False)
        ElseIf XtraTabControl1.SelectedTabPageIndex = 2 Then
            fn_LoadHystori(RM)
        ElseIf XtraTabControl1.SelectedTabPageIndex = 3 Then
            SplashScreenManager.ShowForm(Me, GetType(frmLoadingPanel), True, True, False)

            SplashScreenManager.Default.SetWaitFormCaption("Processing data Resume Rawat Jalan.....")

            Dim oKoding As New Admission.clsKoding
            Dim dsReq = oKoding.GetDataByKDPENDAFTARAN(KDREG)
            If dsReq IsNot Nothing Then
                fn_PrintStrukResumeRawatJalan(dsReq.KDKODING, RM, True, txtKDPENDAFTARAN.Text)
            End If

            SplashScreenManager.CloseForm(False)
        ElseIf XtraTabControl1.SelectedTabPageIndex = 4 Then
            fn_LoadDataUpload(RM)
        ElseIf XtraTabControl1.SelectedTabPageIndex = 5 Then
            SplashScreenManager.ShowForm(Me, GetType(frmLoadingPanel), True, True, False)

            SplashScreenManager.Default.SetWaitFormCaption("Processing data Asesemen Awal Medis IGD.....")
            fn_PrintStrukAsesemenMedisIGD(RM)

            SplashScreenManager.CloseForm(False)
        ElseIf XtraTabControl1.SelectedTabPageIndex = 6 Then
            SplashScreenManager.ShowForm(Me, GetType(frmLoadingPanel), True, True, False)

            SplashScreenManager.Default.SetWaitFormCaption("Processing data Asesemen Awal Perawatan IGD.....")
            fn_PrintStrukAsesemenPerawatIGD(RM)

            SplashScreenManager.CloseForm(False)
        ElseIf XtraTabControl1.SelectedTabPageIndex = 7 Then
            Try
                Dim FolderSimpan = "C:/SIMRS/CPPT"

                If Not Directory.Exists(FolderSimpan) Then
                    Directory.CreateDirectory(FolderSimpan)
                Else
                    DeleteDirectory(FolderSimpan)
                    Directory.CreateDirectory(FolderSimpan)
                End If

                SplashScreenManager.ShowForm(Me, GetType(frmLoadingPanel), True, True, False)

                SplashScreenManager.Default.SetWaitFormCaption("Processing data Tabel Diagnosis GERD-Q.....")

                Dim oIGD As New Inventory.clsDigital_IGD_01_GERD

                Dim dataList As New List(Of Byte())
                Dim dsLoad() As Byte

                For Each xloop In oIGD.GetDataByRM(txtKDCUSTOMER.Text)
                    Dim ds = oIGD.GetData(xloop.KDGERD)
                    If ds IsNot Nothing Then
                        Dim rpt As New xtraGerd_Q

                        rpt.ShowPrintMarginsWarning = False
                        rpt.Watermark.Text = sWATERMARK

                        rpt.bindingSource.DataSource = ds
                        rpt.ExportToPdf(FolderSimpan & ds.KDGERD & ".pdf")
                        dataList.Add(File.ReadAllBytes(FolderSimpan & ds.KDGERD & ".pdf"))
                    End If
                Next

                If dataList.Count > 0 Then
                    dsLoad = oSetKoneksi.MergeFilesByte(dataList)
                    Dim stream As New MemoryStream(dsLoad)
                    pdfGERD.LoadDocument(stream)
                End If

                SplashScreenManager.CloseForm(False)
            Catch oErr As Exception
                SplashScreenManager.CloseForm(False)
                MsgBox("Cetak Gerd Q IGD" & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            End Try
        ElseIf XtraTabControl1.SelectedTabPageIndex = 8 Then
            SplashScreenManager.ShowForm(Me, GetType(frmLoadingPanel), True, True, False)

            SplashScreenManager.Default.SetWaitFormCaption("Processing Konsultasi.....")
            fn_DokterBersama(RM)

            SplashScreenManager.CloseForm(False)
        ElseIf XtraTabControl1.SelectedTabPageIndex = 9 Then
            SplashScreenManager.ShowForm(Me, GetType(frmLoadingPanel), True, True, False)

            SplashScreenManager.Default.SetWaitFormCaption("Processing data Assemen Awal Medis Rawat Inap.....")

            Try
                Dim oDigital_DischargePlanning As New Transaksi.clsDigital_DischargePlanning

                Dim FolderSimpan = "C:/SIMRS/ASESMENAWALMEDISRI"

                If Not Directory.Exists(FolderSimpan) Then
                    Directory.CreateDirectory(FolderSimpan)
                Else
                    DeleteDirectory(FolderSimpan)
                    Directory.CreateDirectory(FolderSimpan)
                End If

                Dim dataList As New List(Of Byte())
                Dim dsLoad() As Byte

                For Each xloop In oDigital_DischargePlanning.GetDataByRM(RM)
                    Dim ds = oDigital_DischargePlanning.GetData(xloop.KDREG)
                    If ds IsNot Nothing Then
                        NAMA = txtNAMAPASIEN.Text
                        JENISKELAMIN = ""
                        TANGGALLAHIR = deDATETANGGALLAHIR.DateTime.ToString("dd-MM-yyyy")
                        sKDUSER_TTD = ds.KDUSER

                        Dim rpt As New xtraReportEMedrekRI_13

                        rpt.ShowPrintMarginsWarning = False
                        rpt.Watermark.Text = sWATERMARK

                        rpt.BindingSource.DataSource = ds
                        rpt.ExportToPdf(FolderSimpan & ds.KDREG & ".pdf")
                        dataList.Add(File.ReadAllBytes(FolderSimpan & ds.KDREG & ".pdf"))
                    End If
                Next

                If dataList.Count > 0 Then
                    dsLoad = oSetKoneksi.MergeFilesByte(dataList)
                    Dim stream As New MemoryStream(dsLoad)
                    PdfViewerAssemenAwalMedisRawatInap.LoadDocument(stream)
                Else
                    PdfViewerAssemenAwalMedisRawatInap.CloseDocument()
                End If
            Catch oErr As Exception
                MsgBox("Cetak CPPT" & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            End Try

            SplashScreenManager.CloseForm(False)
        ElseIf XtraTabControl1.SelectedTabPageIndex = 10 Then
            Try
                grvLaporanOperasi.OptionsSelection.MultiSelect = True
                grvLaporanOperasi.SelectAll()
                grvLaporanOperasi.DeleteSelectedRows()
                grvLaporanOperasi.OptionsSelection.MultiSelect = False

                If txtKDCUSTOMER.Text = "" Then Exit Sub

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
                SQL &= "FROM ( "
                SQL &= "SELECT "
                SQL &= "KETERANGAN = 'LAPORAN OPERASI' "
                SQL &= ",KODE = A.KDLAPORANOPERASI "
                SQL &= ",TANGGAL = A.DATE "
                SQL &= ",A.JENIS_OPERASI "
                SQL &= ",[DOKTER OPERATOR] = A.DOKTER_NAMEDISPLAY "
                'SQL &= ",[DOKTER ANESTESI] = A.TextEdit4 "
                'SQL &= ",PEMBUATLAPORAN = A.KDUSER "
                SQL &= ",JENISTINDAKAN = A.PROSEDUR2 "
                SQL &= "FROM "
                SQL &= "S_DIGITAL_OK_LAPORANOPERASI A "
                SQL &= "WHERE A.KDCUSTOMER = '" & txtKDCUSTOMER.Text & "' "
                SQL &= "AND A.ISDELETE = '0' "

                SQL &= "UNION "

                SQL &= "SELECT "
                SQL &= "KETERANGAN = 'LAPORAN TINDAKAN' "
                SQL &= ",KODE = A.KDLAPORANTINDAKAN "
                SQL &= ",TANGGAL = A.DATE "
                SQL &= ",A.JENIS_OPERASI "
                SQL &= ",[DOKTER OPERATOR] = A.DOKTER_NAMEDISPLAY "
                'SQL &= ",[DOKTER ANESTESI] = '-' "
                'SQL &= ",PEMBUATLAPORAN = A.KDUSER "
                SQL &= ",JENISTINDAKAN = A.PROSEDUR2 "
                SQL &= "FROM "
                SQL &= "S_DIGITAL_OK_LAPORANTINDAKAN A "
                SQL &= "WHERE A.KDCUSTOMER = '" & txtKDCUSTOMER.Text & "' "
                SQL &= "AND A.ISDELETE = '0' "
                SQL &= ") X "
                SQL &= "ORDER BY X.TANGGAL DESC "

                oComm.Connection = oConn
                oComm.CommandText = SQL
                oComm.CommandTimeout = 120
                oComm.CommandType = CommandType.Text

                da = New SqlDataAdapter(oComm)
                da.Fill(ds, "S_DIGITAL_OK_LAPORANOPERASI")

                grdLaporanOperasi.DataSource = ds.Tables("S_DIGITAL_OK_LAPORANOPERASI")
                grdLaporanOperasi.ForceInitialize()

                If oConn.State = ConnectionState.Open Then
                    oConn.Close()
                End If

                grvLaporanOperasi.Columns("KODE").Visible = False

                For iLoop As Integer = 0 To grvLaporanOperasi.Columns.Count - 1
                    If grvLaporanOperasi.Columns(iLoop).ColumnType.Name = "Decimal" Then
                        grvLaporanOperasi.Columns(iLoop).DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                        grvLaporanOperasi.Columns(iLoop).DisplayFormat.FormatString = "{0:n2}"
                        grvLaporanOperasi.Columns(iLoop).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
                    ElseIf grvLaporanOperasi.Columns(iLoop).ColumnType.Name = "DateTime" Then
                        grvLaporanOperasi.Columns(iLoop).DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
                        grvLaporanOperasi.Columns(iLoop).DisplayFormat.FormatString = "{0:dd/MM/yyyy HH:mm}"
                    End If
                Next

            Catch oErr As Exception
                MsgBox("Load Gerd Q : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            End Try
        ElseIf XtraTabControl1.SelectedTabPageIndex = 11 Then
            Try
                SplashScreenManager.ShowForm(Me, GetType(frmLoadingPanel), True, True, False)

                SplashScreenManager.Default.SetWaitFormCaption("Processing data Transfer Internal.....")

                Dim oDigital As New Transaksi.clsTransferInternal

                Dim dataList As New List(Of Byte())
                Dim dsLoad() As Byte

                Dim FolderSimpan = "C:/TRANSFERINTERNAL/"

                If Not Directory.Exists(FolderSimpan) Then
                    Directory.CreateDirectory(FolderSimpan)
                Else
                    DeleteDirectory(FolderSimpan)
                    Directory.CreateDirectory(FolderSimpan)
                End If

                For Each xloop In oDigital.GetDataByRM(txtKDCUSTOMER.Text)
                    Dim ds = oDigital.GetData(xloop.KDTRANSFERINTERNAL)
                    If ds IsNot Nothing Then
                        Dim rpt As New xtraTransferInternal
                        rpt.ShowPrintMarginsWarning = False
                        rpt.Watermark.Text = sWATERMARK
                        rpt.bindingSource.DataSource = ds
                        rpt.ExportToPdf(FolderSimpan & ds.KDTRANSFERINTERNAL & ".pdf")
                        dataList.Add(File.ReadAllBytes(FolderSimpan & ds.KDTRANSFERINTERNAL & ".pdf"))
                    End If
                Next

                If dataList.Count > 0 Then
                    dsLoad = MergeFilesByte(dataList)
                    Dim stream As New MemoryStream(dsLoad)
                    PdfViewerTransferInternal.LoadDocument(stream)
                End If

                SplashScreenManager.CloseForm(False)

            Catch oErr As Exception
                SplashScreenManager.CloseForm(False)
                MsgBox("Cetak Transfer Internal" & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            End Try
        ElseIf XtraTabControl1.SelectedTabPageIndex = 12 Then
            Try
                SplashScreenManager.ShowForm(Me, GetType(frmLoadingPanel), True, True, False)

                SplashScreenManager.Default.SetWaitFormCaption("Processing data Surat Keterangan.....")

                Dim oDigital As New Digital.clsSuratKeteranganSakit
                Dim oDigital2 As New Digital.clsSuratKeteranganSehat

                Dim dataList As New List(Of Byte())
                Dim dsLoad() As Byte

                Dim FolderSimpan = "C:/SURATKETERANGAN/"

                If Not Directory.Exists(FolderSimpan) Then
                    Directory.CreateDirectory(FolderSimpan)
                Else
                    DeleteDirectory(FolderSimpan)
                    Directory.CreateDirectory(FolderSimpan)
                End If

                For Each xloop In oDigital.GetDataByRM(txtKDCUSTOMER.Text)
                    Dim ds = oDigital.GetData(xloop.KDREG)
                    If ds IsNot Nothing Then
                        sKDUSER_TTD = ds.KDUSER
                        Dim rpt As New xtraSuratKeteranganSakit
                        rpt.ShowPrintMarginsWarning = False
                        rpt.Watermark.Text = sWATERMARK
                        rpt.bindingSource.DataSource = ds
                        rpt.ExportToPdf(FolderSimpan & ds.KDREG & ".pdf")
                        dataList.Add(File.ReadAllBytes(FolderSimpan & ds.KDREG & ".pdf"))
                    End If
                Next

                For Each xloop In oDigital2.GetDataByRM(txtKDCUSTOMER.Text)
                    Dim ds = oDigital2.GetData(xloop.NOMORSURAT)
                    If ds IsNot Nothing Then
                        sKDUSER_TTD = ds.KDUSER
                        Dim rpt As New xtraSuratKeteranganSehat
                        rpt.ShowPrintMarginsWarning = False
                        rpt.Watermark.Text = sWATERMARK
                        rpt.bindingSource.DataSource = ds

                        Dim Wktu As String = Now.ToString("ddMMyyyyHHmmss")

                        rpt.ExportToPdf(FolderSimpan & Wktu & ds.KDREG & ".pdf")
                        dataList.Add(File.ReadAllBytes(FolderSimpan & Wktu & ds.KDREG & ".pdf"))
                    End If
                Next

                If dataList.Count > 0 Then
                    dsLoad = MergeFilesByte(dataList)
                    Dim stream As New MemoryStream(dsLoad)
                    PdfViewerSuratKeterangan.LoadDocument(stream)
                End If

                SplashScreenManager.CloseForm(False)

            Catch oErr As Exception
                SplashScreenManager.CloseForm(False)
                MsgBox("Cetak Surat Keterangan" & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            End Try
        ElseIf XtraTabControl1.SelectedTabPageIndex = 13 Then
            Try
                SplashScreenManager.ShowForm(Me, GetType(frmLoadingPanel), True, True, False)

                SplashScreenManager.Default.SetWaitFormCaption("Processing data Formulir MPP.....")

                Dim oDigital As New Digital.clsDigital_MPPA
                Dim oDigital2 As New Digital.clsDigital_MPPB

                Dim dataList As New List(Of Byte())
                Dim dsLoad() As Byte

                Dim FolderSimpan = "C:/MPP/"

                If Not Directory.Exists(FolderSimpan) Then
                    Directory.CreateDirectory(FolderSimpan)
                Else
                    DeleteDirectory(FolderSimpan)
                    Directory.CreateDirectory(FolderSimpan)
                End If

                sUSIADIASESMENIGD = deDATETANGGALLAHIR.DateTime.ToString("dd-MM-yyyy") & " (" & HitungUmur(deDATETANGGALLAHIR.DateTime, deDATE.DateTime) & ")"

                For Each xloop In oDigital.GetDataByRMList(txtKDCUSTOMER.Text)
                    Dim ds = oDigital.GetData(xloop.KDMPPA)
                    If ds IsNot Nothing Then
                        sKDUSER_TTD = ds.KDUSER

                        Dim rpt As New xtraDigital_MPPA
                        rpt.ShowPrintMarginsWarning = False
                        rpt.Watermark.Text = sWATERMARK
                        rpt.bindingSource.DataSource = ds
                        rpt.ExportToPdf(FolderSimpan & ds.KDMPPA & ".pdf")
                        dataList.Add(File.ReadAllBytes(FolderSimpan & ds.KDMPPA & ".pdf"))
                    End If
                Next

                For Each xloop In oDigital.GetDataByRMList2(txtKDCUSTOMER.Text)
                    Dim ds = oDigital2.GetData(xloop.KDMPPB)
                    If ds IsNot Nothing Then
                        sKDUSER_TTD = ds.KDUSER
                        Dim rpt As New xtraDigital_MPPB
                        rpt.ShowPrintMarginsWarning = False
                        rpt.Watermark.Text = sWATERMARK
                        rpt.bindingSource.DataSource = ds
                        rpt.ExportToPdf(FolderSimpan & ds.KDMPPB & ".pdf")
                        dataList.Add(File.ReadAllBytes(FolderSimpan & ds.KDMPPB & ".pdf"))
                    End If
                Next

                If dataList.Count > 0 Then
                    dsLoad = MergeFilesByte(dataList)
                    Dim stream As New MemoryStream(dsLoad)
                    PdfViewerFormulirMPP.LoadDocument(stream)
                End If

                SplashScreenManager.CloseForm(False)

            Catch oErr As Exception
                SplashScreenManager.CloseForm(False)
                MsgBox("Cetak Formulir MPP" & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            End Try
        ElseIf XtraTabControl1.SelectedTabPageIndex = 14 Then
            Try
                Dim oReqCPPT As New Transaksi.clsReqCPPT

                Dim FolderSimpan = "C:/SIMRS/CPPT"

                If Not Directory.Exists(FolderSimpan) Then
                    Directory.CreateDirectory(FolderSimpan)
                Else
                    DeleteDirectory(FolderSimpan)
                    Directory.CreateDirectory(FolderSimpan)
                End If

                Dim dataList As New List(Of Byte())
                Dim dsLoad() As Byte
                Dim ds = oReqCPPT.GetDataByRMListSkin(RM)
                Dim rpt As New xtraDigital_CPPT_01_QR

                rpt.ShowPrintMarginsWarning = False
                rpt.Watermark.Text = sWATERMARK

                rpt.bindingSource.DataSource = ds
                rpt.ExportToPdf(FolderSimpan & "HASILSKIN" & ".pdf")
                dataList.Add(File.ReadAllBytes(FolderSimpan & "HASILSKIN" & ".pdf"))

                If ds.Count > 0 Then
                    dsLoad = oSetKoneksi.MergeFilesByte(dataList)
                    Dim stream As New MemoryStream(dsLoad)
                    PdfViewerSkinAnalysis.LoadDocument(stream)
                Else
                    PdfViewerSkinAnalysis.CloseDocument()
                End If

                'PdfViewerCPPTRawatJalan.LoadDocument("C:/SIMRS/CPPT/" & lblRegister.Text & ".pdf")

                'Dim printTool As New DevExpress.XtraReports.UI.ReportPrintTool(rpt)
                'printTool.ShowPreviewDialog(DevExpress.LookAndFeel.UserLookAndFeel.Default)
            Catch oErr As Exception
                SplashScreenManager.CloseForm(False)
                MsgBox("Cetak CPPT" & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            End Try
        ElseIf XtraTabControl1.SelectedTabPageIndex = 15 Then
            Try
                Dim oMCU As New Digital.clsDigital_MedicalChekUp

                Dim FolderSimpan = "C:/SIMRS/CPPT"

                If Not Directory.Exists(FolderSimpan) Then
                    Directory.CreateDirectory(FolderSimpan)
                Else
                    DeleteDirectory(FolderSimpan)
                    Directory.CreateDirectory(FolderSimpan)
                End If

                Dim dataList As New List(Of Byte())
                Dim dsLoad() As Byte
                Dim ds = oMCU.GetData(txtKDPENDAFTARAN.Text)
                Dim rpt As New xtraMedicalCheckUp

                rpt.ShowPrintMarginsWarning = False
                rpt.Watermark.Text = sWATERMARK

                rpt.bindingSource.DataSource = ds
                rpt.ExportToPdf(FolderSimpan & "MCU" & ".pdf")
                dataList.Add(File.ReadAllBytes(FolderSimpan & "MCU" & ".pdf"))

                'Dim rpt2 As New xtraMedicalCheckUp2

                'rpt2.ShowPrintMarginsWarning = False
                'rpt2.Watermark.Text = sWATERMARK

                'rpt2.bindingSource.DataSource = ds
                'rpt2.ExportToPdf(FolderSimpan & "MCU2" & ".pdf")
                'dataList.Add(File.ReadAllBytes(FolderSimpan & "MCU2" & ".pdf"))

                If ds IsNot Nothing Then
                    dsLoad = oSetKoneksi.MergeFilesByte(dataList)
                    Dim stream As New MemoryStream(dsLoad)
                    PdfViewerMCU.LoadDocument(stream)
                Else
                    PdfViewerMCU.CloseDocument()
                End If

                'PdfViewerCPPTRawatJalan.LoadDocument("C:/SIMRS/CPPT/" & lblRegister.Text & ".pdf")

                'Dim printTool As New DevExpress.XtraReports.UI.ReportPrintTool(rpt)
                'printTool.ShowPreviewDialog(DevExpress.LookAndFeel.UserLookAndFeel.Default)
            Catch oErr As Exception
                SplashScreenManager.CloseForm(False)
                MsgBox("Cetak CPPT" & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            End Try
        End If
    End Sub
#End Region
#Region "Command Button"
    Private Sub btnOrderLaboratorium_Click(sender As Object, e As EventArgs)
        'Dim frmOrderPenunjangLab As New frmOrderPenunjangLab
        'Try
        '    frmOrderPenunjangLab.LoadMe("LABORATORIUM", sNoidSimpan, grdDPJP.EditValue, txtOBJEKTIF_BERATBADAN.Text, txtOBJEKTIF_TINGGIBADAN.Text)
        '    frmOrderPenunjangLab.ShowDialog(Me)
        'Catch ex As Exception
        '    MsgBox("Load Form Detail : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
        'Finally
        '    If Not frmOrderPenunjangLab Is Nothing Then frmOrderPenunjangLab.Dispose()
        '    frmOrderPenunjangLab = Nothing

        '    If listOrderLab.Count > 0 Then
        '        grvTindakan.Focus()
        '        grvTindakan.AddNewRow()
        '        grvTindakan.SetFocusedRowCellValue(colKETERANGAN_, listOrderLab)
        '        grvTindakan.UpdateCurrentRow()
        '    End If
        '    If listOrderRad.Count > 0 Then
        '        grvTindakan.Focus()
        '        grvTindakan.AddNewRow()
        '        grvTindakan.SetFocusedRowCellValue(colKETERANGAN_, listOrderRad)
        '        grvTindakan.UpdateCurrentRow()
        '    End If
        'End Try
    End Sub
    Private Sub btnOrderRadiologi_Click(sender As Object, e As EventArgs)
        'Dim frmOrderPenunjangLab As New frmOrderPenunjangLab
        'Try
        '    frmOrderPenunjangLab.LoadMe("RADIOLOGI", sNoidSimpan, grdDPJP.EditValue, txtOBJEKTIF_BERATBADAN.Text, txtOBJEKTIF_TINGGIBADAN.Text)
        '    frmOrderPenunjangLab.ShowDialog(Me)
        'Catch ex As Exception
        '    MsgBox("Load Form Detail : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
        'Finally
        '    If Not frmOrderPenunjangLab Is Nothing Then frmOrderPenunjangLab.Dispose()
        '    frmOrderPenunjangLab = Nothing

        '    If listOrderLab.Count > 0 Then
        '        grvTindakan.Focus()
        '        grvTindakan.AddNewRow()
        '        grvTindakan.SetFocusedRowCellValue(colKETERANGAN_, listOrderLab)
        '        grvTindakan.UpdateCurrentRow()
        '    End If
        '    If listOrderRad.Count > 0 Then
        '        grvTindakan.Focus()
        '        grvTindakan.AddNewRow()
        '        grvTindakan.SetFocusedRowCellValue(colKETERANGAN_, listOrderRad)
        '        grvTindakan.UpdateCurrentRow()
        '    End If
        'End Try
    End Sub
    Private Sub btnJawabKonsul_Click(sender As Object, e As EventArgs) Handles btnJawabKonsul.Click
        If grvDokterBersama.GetFocusedRowCellValue("SEQ") = 0 Then
            MsgBox("Silahkan Pilih data", MsgBoxStyle.Exclamation, Me.Text)
            Exit Sub
        End If

        Dim frmKonsulDokter As New frmKonsulDokter
        Try
            frmKonsulDokter.fn_LoadMe(grvDokterBersama.GetFocusedRowCellValue("KDREG"), grvDokterBersama.GetFocusedRowCellValue("KDDOCTOR_DARI"), grvDokterBersama.GetFocusedRowCellValue("KDDOCTOR_KEPADA"), grvDokterBersama.GetFocusedRowCellValue("SEQ"), grvDokterBersama.GetFocusedRowCellValue("ISIKONSUL"), grvDokterBersama.GetFocusedRowCellValue("JAWABKONSUL"), grvDokterBersama.GetFocusedRowCellValue("TGLKONSUL"), grvDokterBersama.GetFocusedRowCellValue("TGLJAWAB"))
            frmKonsulDokter.ShowDialog(Me)
        Catch ex As Exception
            MsgBox("Load Form Detail : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
        Finally
            If Not frmKonsulDokter Is Nothing Then frmKonsulDokter.Dispose()
            frmKonsulDokter = Nothing
            fn_DokterBersama(txtKDCUSTOMER.Text)
            'If sKONSULTASI <> "" Then
            '    txtINTRUKSILAIN.Text = txtINTRUKSILAIN.Text & vbCrLf & "Konsultasi Ke Dokter" & sKONSULTASI
            'End If
        End Try
    End Sub
    Private Sub fn_PrintStrukResumeRawatJalan(ByVal NoTransaksi As String, ByVal RM As String, ByVal isPDF As Boolean, ByVal sKDREGfisio As String)
        Try
            Dim oKoding As New Transaksi.clsReq_Recipe
            Dim oCPPT As New Transaksi.clsCPPT

            'Dim dsKoding = oKoding.GetDataKoding(NoTransaksi)
            'If dsKoding IsNot Nothing Then
            '    Dim rpt As New xtraKoding

            '    rpt.bindingSource.DataSource = dsKoding

            '    DownloadIamge1 = AlamatDownloadIamge1 & RM & "/" & RM & ".png"

            '    'KDDOCTOR = dsKoding.S_PENDAFTARAN_H.KDDOCTOR
            '    sUserIDTandaTangan = dsKoding.KDDOCTOR

            '    If isPDF = True Then
            '        rpt.bindingSource.DataSource = dsKoding
            '        rpt.ExportToPdf("C:/farmasi/resep/B.pdf")
            '        PdfViewerResume.LoadDocument("C:/farmasi/resep/B.pdf")
            '    Else
            '        Dim printTool As New DevExpress.XtraReports.UI.ReportPrintTool(rpt)
            '        printTool.ShowPreviewDialog(DevExpress.LookAndFeel.UserLookAndFeel.Default)
            '    End If
            'End If


            Try
                Dim FolderSimpan = "C:/SIMRS/MEDREK/RESUME/"

                If Not Directory.Exists(FolderSimpan) Then
                    Directory.CreateDirectory(FolderSimpan)
                Else
                    DeleteDirectory(FolderSimpan)
                    Directory.CreateDirectory(FolderSimpan)
                End If

                Dim dataList As New List(Of Byte())
                Dim dsLoad() As Byte

                'For Each xloop In oIGD.GetDataByRM(RM)
                '    Dim ds = oIGD.GetData(xloop.KDPENDAFTARAN)
                '    If ds IsNot Nothing Then
                '        Dim rpt As New xtraReportAsesmenKeperawatanGD_New

                '        rpt.ShowPrintMarginsWarning = False
                '        rpt.Watermark.Text = sWATERMARK

                '        sASESEMEN_IGD = ds.SIMPANGAMBAR_1
                '        rpt.BindingSource.DataSource = ds
                '        rpt.ExportToPdf(FolderSimpan & ds.KDPENDAFTARAN & ".pdf")
                '        dataList.Add(File.ReadAllBytes(FolderSimpan & ds.KDPENDAFTARAN & ".pdf"))
                '    End If
                'Next

                Dim dsKoding = oKoding.GetDataKoding(NoTransaksi)
                If dsKoding IsNot Nothing Then
                    Dim rpt As New xtraKoding

                    rpt.bindingSource.DataSource = dsKoding

                    DownloadIamge1 = AlamatDownloadIamge1 & RM & "/" & RM & ".png"

                    'KDDOCTOR = dsKoding.S_PENDAFTARAN_H.KDDOCTOR
                    sUserIDTandaTangan = dsKoding.KDDOCTOR

                    Dim Waktu As String = Now.ToString("yyyyMMddHHmmss")
                    rpt.ExportToPdf(FolderSimpan & Waktu & dsKoding.KDKODING & ".pdf")

                    If isPDF = True Then
                        dataList.Add(File.ReadAllBytes(FolderSimpan & Waktu & dsKoding.KDKODING & ".pdf"))
                    Else
                        Dim printTool As New DevExpress.XtraReports.UI.ReportPrintTool(rpt)
                        printTool.ShowPreviewDialog(DevExpress.LookAndFeel.UserLookAndFeel.Default)
                    End If
                End If

                If grdKDDEPARTMENT.Text = "REHAB MEDIK" Then
                    Dim dsCPPT = oCPPT.GetDataByRegisterDokter(sKDREGfisio)

                    If dsCPPT IsNot Nothing Then
                        Dim rpt As New xtraLembarUjiFungsiRehabResume

                        rpt.bindingSource.DataSource = dsCPPT

                        Dim Waktu As String = Now.ToString("yyyyMMddHHmmss")
                        rpt.ExportToPdf(FolderSimpan & Waktu & dsCPPT.KDCPPT & ".pdf")
                        dataList.Add(File.ReadAllBytes(FolderSimpan & Waktu & dsCPPT.KDCPPT & ".pdf"))
                    End If
                End If

                If dataList.Count > 0 Then
                    dsLoad = oSetKoneksi.MergeFilesByte(dataList)
                    Dim stream As New MemoryStream(dsLoad)

                    PdfViewerResume.LoadDocument(stream)
                End If
            Catch oErr As Exception
                MsgBox("Cetak Asesemen Medis IGD" & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            End Try
        Catch oErr As Exception
            MsgBox("Print Data : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Function IntegerToRoman(IntNumberValue As Integer) As String
        Dim RomanNumbers As New Dictionary(Of String, Integer)()
        RomanNumbers.Add("M", 1000)
        RomanNumbers.Add("CM", 900)
        RomanNumbers.Add("D", 500)
        RomanNumbers.Add("CD", 400)
        RomanNumbers.Add("C", 100)
        RomanNumbers.Add("XC", 90)
        RomanNumbers.Add("L", 50)
        RomanNumbers.Add("XL", 40)
        RomanNumbers.Add("X", 10)
        RomanNumbers.Add("IX", 9)
        RomanNumbers.Add("V", 5)
        RomanNumbers.Add("IV", 4)
        RomanNumbers.Add("I", 1)

        Dim result As String = ""

        For Each pair As KeyValuePair(Of String, Integer) In RomanNumbers
            While IntNumberValue >= pair.Value
                IntNumberValue -= pair.Value
                result += pair.Key
            End While
        Next
        Return result
    End Function
    Private Sub fn_PrintStrukAsesemenPerawatIGD(ByVal RM As String)
        Try
            Dim oIGD As New Transaksi.clsDigital_IGD_02
            Dim FolderSimpan = "C:/SIMRS/MEDREK/"

            If Not Directory.Exists(FolderSimpan) Then
                Directory.CreateDirectory(FolderSimpan)
            Else
                DeleteDirectory(FolderSimpan)
                Directory.CreateDirectory(FolderSimpan)
            End If

            Dim dataList As New List(Of Byte())
            Dim dsLoad() As Byte

            For Each xloop In oIGD.GetDataByRM(RM)
                Dim ds = oIGD.GetData(xloop.KDPENDAFTARAN)
                If ds IsNot Nothing Then
                    Dim rpt As New xtraReportAsesmenKeperawatanGD_New

                    rpt.ShowPrintMarginsWarning = False
                    rpt.Watermark.Text = sWATERMARK

                    sASESEMEN_IGD = ds.SIMPANGAMBAR_1
                    rpt.BindingSource.DataSource = ds
                    rpt.ExportToPdf(FolderSimpan & ds.KDPENDAFTARAN & ".pdf")
                    dataList.Add(File.ReadAllBytes(FolderSimpan & ds.KDPENDAFTARAN & ".pdf"))
                End If
            Next

            If dataList.Count > 0 Then
                dsLoad = oSetKoneksi.MergeFilesByte(dataList)
                Dim stream As New MemoryStream(dsLoad)
                PdfViewerPerawatIGD.LoadDocument(stream)
            End If

        Catch oErr As Exception
            MsgBox("Cetak Asesemen Medis IGD" & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_PrintStrukAsesemenMedisIGD(ByVal RM As String)
        Try
            Dim oIGD As New Transaksi.clsDigital_IGD_01
            Dim FolderSimpan = "C:/SIMRS/MEDREK/"


            If Not Directory.Exists(FolderSimpan) Then
                Directory.CreateDirectory(FolderSimpan)
            Else
                DeleteDirectory(FolderSimpan)
                Directory.CreateDirectory(FolderSimpan)
            End If

            Dim ListdataKDCPPT As New List(Of String)
            Dim dataList As New List(Of Byte())
            Dim dsLoad() As Byte

            For Each xloop In oIGD.GetDataByRM(RM)
                Dim ds = oIGD.GetData(xloop.KDPENDAFTARAN)
                If ds IsNot Nothing Then
                    sASESEMEN_IGD = ds.SURVEY_PERUT_1
                    sUSIADIASESMENIGD = deDATETANGGALLAHIR.DateTime.ToString("dd-MM-yyyy") & " (" & HitungUmur(deDATETANGGALLAHIR.DateTime, deDATE.DateTime) & ")"

                    Dim rpt As New xtraReportFormulirIGD1

                    rpt.ShowPrintMarginsWarning = False
                    rpt.Watermark.Text = sWATERMARK

                    ListdataKDCPPT.Add(ds.KDPENDAFTARAN)
                    rpt.BindingSource.DataSource = ds
                    rpt.ExportToPdf(FolderSimpan & ds.KDPENDAFTARAN & ".pdf")
                    dataList.Add(File.ReadAllBytes(FolderSimpan & ds.KDPENDAFTARAN & ".pdf"))
                End If
            Next

            If dataList.Count > 0 Then
                dsLoad = oSetKoneksi.MergeFilesByte(dataList)
                Dim stream As New MemoryStream(dsLoad)
                PdfViewer_IGD.LoadDocument(stream)
            End If

        Catch oErr As Exception
            MsgBox("Cetak Asesemen Medis IGD" & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Function HitungUmur(ByVal tanggllahir As Date, ByVal tanggaldatang As Date) As String
        Dim y, m, d As Integer
        d = tanggaldatang.Day - tanggllahir.Day
        m = tanggaldatang.Month - tanggllahir.Month
        y = tanggaldatang.Year - tanggllahir.Year
        If Math.Sign(d) = -1 Then
            d = 30 - Math.Abs(m)
            m -= 1
        End If
        If Math.Sign(m) = -1 Then
            m = 12 - Math.Abs(m)
            y -= 1

        End If

        HitungUmur = y & " Tahun, " & m & " bulan, " & d & " hari"
        'UMUR = y & " Tahun"
        'Return y & " Tahun, " & m & " bulan, " & d & " hari"

    End Function
#End Region
#Region "Lookup / Event"
    Private Sub grvListPasien_FocusedRowChanged(ByVal sender As System.Object, ByVal e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs) Handles grvListPasien.FocusedRowChanged
        fn_EmptyMe()

        If grvListPasien.GetFocusedRowCellValue("NoRegister") Is Nothing Then
            Exit Sub
        End If

        deTANGGALDATANG.DateTime = grvListPasien.GetFocusedRowCellValue("DATE")
        txtKDPENDAFTARAN.Text = grvListPasien.GetFocusedRowCellValue("NoRegister")
        txtKDCUSTOMER.Text = grvListPasien.GetFocusedRowCellValue("KDCUSTOMER")
        txtNAMAPASIEN.Text = grvListPasien.GetFocusedRowCellValue("Pasien")
        deDATETANGGALLAHIR.DateTime = grvListPasien.GetFocusedRowCellValue("TANGGALLAHIR")
        txtOBJEKTIF_JENISKELAMIN.Text = IIf(grvListPasien.GetFocusedRowCellValue("JK") = "1", "Laki-laki", "Perempuan")
        txtDIAGNOOSA.Text = grvListPasien.GetFocusedRowCellValue("DIAGNOSA")
        txtOBJEKTIF_UMUR.Text = frmReqAwalPemeriksaan.GetUmurPasien(Now, deDATETANGGALLAHIR.DateTime)
        txtPenjamin.Text = grvListPasien.GetFocusedRowCellValue("PENJAMIN")
        txtKARTUBPJS.Text = grvListPasien.GetFocusedRowCellValue("KARTUBPJS")
        txtKODEBOOKING.Text = grvListPasien.GetFocusedRowCellValue("KodeBooking")
        txtNOMORSEP.Text = grvListPasien.GetFocusedRowCellValue("NOMORSEP")
        sALAMATPASIEN = grvListPasien.GetFocusedRowCellValue("ADDRESS_STREET")
        sTEMPATLAHIR = grvListPasien.GetFocusedRowCellValue("TEMPATLAHIR")
        sKDDEPARTMENT = grvListPasien.GetFocusedRowCellValue("KDDEPARTMENT")

        fn_LoadDocument(txtKDPENDAFTARAN.Text, txtKDCUSTOMER.Text)

    End Sub
    Private Sub XtraTabControl1_SelectedPageChanged(sender As Object, e As DevExpress.XtraTab.TabPageChangedEventArgs) Handles XtraTabControl1.SelectedPageChanged
        If txtKDPENDAFTARAN.Text = "" Then
            fn_EmptyMe()
            Exit Sub
        End If

        fn_LoadDocument(txtKDPENDAFTARAN.Text, txtKDCUSTOMER.Text)
    End Sub
    Private Sub grvUpload_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles grvUpload.DoubleClick
        If grvUpload.GetFocusedRowCellValue("AlamatUpload") Is Nothing Then
            Exit Sub
        End If

        If txtKDCUSTOMER.Text = "" Then
            Exit Sub
        End If

        If grvUpload.GetFocusedRowCellValue("Type") = "RONTGEN" Then
            'frmBrowseUpload.fn_LoadMe(2, grvUpload.GetFocusedRowCellValue("Type"), grvUpload.GetFocusedRowCellValue("AlamatUpload"), txtKDCUSTOMER.Text, txtNAMAPASIEN.Text, deDATETANGGALLAHIR.DateTime.ToString("dd-MM-yyyy"), txtOBJEKTIF_JENISKELAMIN.Text, grvUpload.GetFocusedRowCellValue("Kodeupload"))
            'frmBrowseUpload.Show()
            ''frmBrowseUpload.BringToFront()
            'frmBrowseUpload.WindowState = FormWindowState.Normal

            Dim frmBrowseUpload As New frmBrowseUpload
            Try
                frmBrowseUpload.WindowState = FormWindowState.Normal
                frmBrowseUpload.fn_LoadMe(2, grvUpload.GetFocusedRowCellValue("Type"), grvUpload.GetFocusedRowCellValue("AlamatUpload"), txtKDCUSTOMER.Text, txtNAMAPASIEN.Text, deDATETANGGALLAHIR.DateTime.ToString("dd-MM-yyyy"), txtOBJEKTIF_JENISKELAMIN.Text, grvUpload.GetFocusedRowCellValue("Kodeupload"))
                frmBrowseUpload.ShowDialog(Me)
            Catch ex As Exception
                MsgBox("Load Form Detail : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
            Finally
                If Not frmBrowseUpload Is Nothing Then frmBrowseUploadRadiologi.Dispose()
                frmBrowseUpload = Nothing
            End Try
        ElseIf grvUpload.GetFocusedRowCellValue("Type") = "USG" Then
            'frmBrowseUpload.fn_LoadMe(1, grvUpload.GetFocusedRowCellValue("Type"), grvUpload.GetFocusedRowCellValue("AlamatUpload"), txtKDCUSTOMER.Text, txtNAMAPASIEN.Text, deDATETANGGALLAHIR.DateTime.ToString("dd-MM-yyyy"), txtOBJEKTIF_JENISKELAMIN.Text, grvUpload.GetFocusedRowCellValue("Kodeupload"))
            'frmBrowseUpload.Show()
            ''frmBrowseUpload.BringToFront()
            'frmBrowseUpload.WindowState = FormWindowState.Normal

            Dim frmBrowseUpload As New frmBrowseUpload
            Try
                frmBrowseUpload.WindowState = FormWindowState.Normal
                frmBrowseUpload.fn_LoadMe(1, grvUpload.GetFocusedRowCellValue("Type"), grvUpload.GetFocusedRowCellValue("AlamatUpload"), txtKDCUSTOMER.Text, txtNAMAPASIEN.Text, deDATETANGGALLAHIR.DateTime.ToString("dd-MM-yyyy"), txtOBJEKTIF_JENISKELAMIN.Text, grvUpload.GetFocusedRowCellValue("Kodeupload"))
                frmBrowseUpload.ShowDialog(Me)
            Catch ex As Exception
                MsgBox("Load Form Detail : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
            Finally
                If Not frmBrowseUpload Is Nothing Then frmBrowseUploadRadiologi.Dispose()
                frmBrowseUpload = Nothing
            End Try
        ElseIf grvUpload.GetFocusedRowCellValue("Type") = "APIRADIOLOGI" Then
            'frmBrowseUploadRadiologi.fn_LoadMe(txtKDCUSTOMER.Text, txtNAMAPASIEN.Text, deDATETANGGALLAHIR.DateTime.ToString("dd-MM-yyyy"), txtOBJEKTIF_JENISKELAMIN.Text, grvUpload.GetFocusedRowCellValue("AlamatUpload"))
            'frmBrowseUploadRadiologi.Show()
            'frmBrowseUploadRadiologi.WindowState = FormWindowState.Normal

            Dim frmBrowseUploadRadiologi As New frmBrowseUploadRadiologi
            Try
                frmBrowseUploadRadiologi.WindowState = FormWindowState.Normal
                frmBrowseUploadRadiologi.fn_LoadMe(txtKDCUSTOMER.Text, txtNAMAPASIEN.Text, deDATETANGGALLAHIR.DateTime.ToString("dd-MM-yyyy"), txtOBJEKTIF_JENISKELAMIN.Text, grvUpload.GetFocusedRowCellValue("AlamatUpload"), txtKDPENDAFTARAN.Text)
                frmBrowseUploadRadiologi.ShowDialog(Me)
            Catch ex As Exception
                MsgBox("Load Form Detail : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
            Finally
                If Not frmBrowseUploadRadiologi Is Nothing Then frmBrowseUploadRadiologi.Dispose()
                frmBrowseUploadRadiologi = Nothing

            End Try
        Else
            Dim frmBrowseUpload As New frmBrowseUpload
            Try
                frmBrowseUpload.WindowState = FormWindowState.Normal
                frmBrowseUpload.fn_LoadMe(0, grvUpload.GetFocusedRowCellValue("Type"), grvUpload.GetFocusedRowCellValue("AlamatUpload"), txtKDCUSTOMER.Text, txtNAMAPASIEN.Text, deDATETANGGALLAHIR.DateTime.ToString("dd-MM-yyyy"), txtOBJEKTIF_JENISKELAMIN.Text, grvUpload.GetFocusedRowCellValue("Kodeupload"))
                frmBrowseUpload.ShowDialog(Me)
            Catch ex As Exception
                MsgBox("Load Form Detail : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
            Finally
                If Not frmBrowseUpload Is Nothing Then frmBrowseUploadRadiologi.Dispose()
                frmBrowseUpload = Nothing
            End Try

            'frmBrowseUpload.fn_LoadMe(0, grvUpload.GetFocusedRowCellValue("Type"), grvUpload.GetFocusedRowCellValue("AlamatUpload"), txtKDCUSTOMER.Text, txtNAMAPASIEN.Text, deDATETANGGALLAHIR.DateTime.ToString("dd-MM-yyyy"), txtOBJEKTIF_JENISKELAMIN.Text, grvUpload.GetFocusedRowCellValue("Kodeupload"))
            'frmBrowseUpload.Show()
            ''frmBrowseUpload.BringToFront()
            'frmBrowseUpload.WindowState = FormWindowState.Normal
        End If
    End Sub
    Private Sub grvListPasien_RowStyle(sender As System.Object, e As DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs) Handles grvListPasien.RowStyle
        If grvListPasien.IsFilterRow(e.RowHandle) Then Exit Sub
        If grvListPasien.GetRowCellValue(e.RowHandle, "Cek") = True Then
            If grvListPasien.GetRowCellValue(e.RowHandle, "Keterangan").ToString.Contains("PENUNJANG HARI INI") Then
                If grvListPasien.GetRowCellValue(e.RowHandle, "KeteranganPenunjangHariIni").ToString <> "" Then
                    e.Appearance.BackColor = Color.Aqua
                Else
                    e.Appearance.BackColor = Color.Yellow
                End If
            Else
                e.Appearance.BackColor = Color.LawnGreen
            End If
        Else
            If grvListPasien.GetRowCellValue(e.RowHandle, "ISPERAWAT") = True Then
                e.Appearance.BackColor = Color.Orange
            End If
        End If
    End Sub
    Private Sub grvListantrian_RowStyle(sender As System.Object, e As DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs) Handles grvListantrian.RowStyle
        If grvListantrian.IsFilterRow(e.RowHandle) Then Exit Sub
        If grvListantrian.GetRowCellValue(e.RowHandle, "Cek") = True Then
            If grvListantrian.GetRowCellValue(e.RowHandle, "Keterangan").ToString.Contains("PENUNJANG HARI INI") Then
                If grvListantrian.GetRowCellValue(e.RowHandle, "KeteranganPenunjangHariIni").ToString <> "" Then
                    e.Appearance.BackColor = Color.Aqua
                Else
                    e.Appearance.BackColor = Color.Yellow
                End If
            Else
                e.Appearance.BackColor = Color.LawnGreen
            End If
        Else
            If grvListantrian.GetRowCellValue(e.RowHandle, "ISPERAWAT") = True Then
                e.Appearance.BackColor = Color.Orange
            End If
        End If
    End Sub
    Private Sub grvLaporanOperasi_FocusedRowChanged(ByVal sender As System.Object, ByVal e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs) Handles grvLaporanOperasi.FocusedRowChanged
        If grvLaporanOperasi.GetFocusedRowCellValue("KODE") Is Nothing Then
            Exit Sub
        End If

        Try
            PdfViewerLaporanOperasi.CloseDocument()

            SplashScreenManager.ShowForm(Me, GetType(frmLoadingPanel), True, True, False)

            SplashScreenManager.Default.SetWaitFormCaption("Processing data Laporan Operasi.....")


            Dim FolderSimpan = "C:/LAPORANOPERASI_LISTERM/"

            If Not Directory.Exists(FolderSimpan) Then
                Directory.CreateDirectory(FolderSimpan)
            Else
                DeleteDirectory(FolderSimpan)
                Directory.CreateDirectory(FolderSimpan)
            End If

            Dim oLaporanOperasi As New EMedrek.clsS_DIGITAL_OK_LAPORANOPERASI

            If grvLaporanOperasi.GetFocusedRowCellValue("KETERANGAN") = "LAPORAN OPERASI" Then
                Dim ds = oLaporanOperasi.GetDataKode(grvLaporanOperasi.GetFocusedRowCellValue("KODE"))
                If ds IsNot Nothing Then
                    NAMA = txtNAMAPASIEN.Text
                    JENISKELAMIN = txtOBJEKTIF_JENISKELAMIN.Text
                    TANGGALLAHIR = deDATETANGGALLAHIR.DateTime.ToString("dd-MM-yyyy")
                    sKDUSER_TTD = ds.KDUSER
                    USIA = frmRawatInapList.GetUmurPasien(ds.DATE, deDATETANGGALLAHIR.DateTime)

                    Dim rpt As New xtraReportLAPORANOPERASI

                    rpt.ShowPrintMarginsWarning = False
                    rpt.Watermark.Text = sWATERMARK

                    rpt.BindingSource1.DataSource = ds

                    Dim waktu As String = Now.ToString("ddMMyyyyHHmm")
                    rpt.ExportToPdf(FolderSimpan & waktu & ds.KDLAPORANOPERASI & ".pdf")
                    PdfViewerLaporanOperasi.LoadDocument(FolderSimpan & waktu & ds.KDLAPORANOPERASI & ".pdf")
                End If
            Else
                Dim ds = oLaporanOperasi.GetDataKodeTindakan(grvLaporanOperasi.GetFocusedRowCellValue("KODE"))
                If ds IsNot Nothing Then
                    NAMA = txtNAMAPASIEN.Text
                    JENISKELAMIN = txtOBJEKTIF_JENISKELAMIN.Text
                    TANGGALLAHIR = deDATETANGGALLAHIR.DateTime.ToString("dd-MM-yyyy")
                    sKDUSER_TTD = ds.KDUSER
                    USIA = frmRawatInapList.GetUmurPasien(ds.DATE, deDATETANGGALLAHIR.DateTime)

                    Dim rpt As New xtraREPORTLAPORANTINDAKAN

                    rpt.ShowPrintMarginsWarning = False
                    rpt.Watermark.Text = sWATERMARK

                    rpt.BindingSource1.DataSource = ds

                    Dim waktu As String = Now.ToString("ddMMyyyyHHmm")
                    rpt.ExportToPdf(FolderSimpan & waktu & ds.KDLAPORANTINDAKAN & ".pdf")
                    PdfViewerLaporanOperasi.LoadDocument(FolderSimpan & waktu & ds.KDLAPORANTINDAKAN & ".pdf")
                End If
            End If

            SplashScreenManager.CloseForm(False)
        Catch oErr As Exception
            SplashScreenManager.CloseForm(False)
            MsgBox("Load data Laporan Operasi: " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadDEPARTMENT()
        Dim oDepartment As New Reference.clsDepartment
        Try
            grdKDDEPARTMENT.Properties.DataSource = oDepartment.GetData.Where(Function(x) x.ISACTIVE = True And x.KDDEPARTMENT_BPJS <> "").ToList()
            grdKDDEPARTMENT.Properties.ValueMember = "KDDEPARTMENT"
            grdKDDEPARTMENT.Properties.DisplayMember = "NAME_DISPLAY"
        Catch oErr As Exception
            MsgBox("Load Poli Data : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Public Sub DeleteDirectory(path As String)
        If Directory.Exists(path) Then
            If Directory.Exists(path) Then
                'Delete all files from the Directory
                For Each filepath As String In Directory.GetFiles(path)
                    File.Delete(filepath)
                Next
                'Delete all child Directories
                For Each dir As String In Directory.GetDirectories(path)
                    DeleteDirectory(dir)
                Next
                'Delete a Directory
                Directory.Delete(path)
            End If

        End If
    End Sub
    Private Sub picRefreshListPasien_Click() Handles picRefreshListPasien.Click
        fn_LoadSecurity()
    End Sub
    Private Function MergeFilesByte(ByVal sourceFiles As List(Of Byte())) As Byte()
        Try
            Dim mergedPdf As Byte() = Nothing
            Using ms As New MemoryStream()
                Using document As New Document()
                    Using copy As New PdfCopy(document, ms)
                        document.Open()
                        For i As Integer = 0 To sourceFiles.Count - 1
                            Dim reader As New PdfReader(sourceFiles(i))
                            ' loop over the pages in that document
                            Dim n As Integer = reader.NumberOfPages
                            Dim page As Integer = 0
                            While page < n
                                page = page + 1
                                copy.AddPage(copy.GetImportedPage(reader, page))
                            End While
                        Next
                    End Using
                End Using

                mergedPdf = ms.ToArray()

                Return mergedPdf

            End Using
        Catch ex As Exception
            MergeFilesByte = Nothing
            MsgBox("Load Merge Data : " & vbCrLf & ex.Message, MsgBoxStyle.Information, Me.Text)
        End Try
    End Function
    'Public Function MergeFilesByte(ByVal sourceFiles As List(Of Byte())) As Byte()
    '    Try
    '        Dim mergedPdf As Byte() = Nothing
    '        Using ms As New MemoryStream()
    '            Using document As New Document()
    '                Using copy As New PdfCopy(document, ms)
    '                    document.Open()
    '                    For i As Integer = 0 To sourceFiles.Count - 1
    '                        Dim reader As New PdfReader(sourceFiles(i))
    '                        ' loop over the pages in that document
    '                        Dim n As Integer = reader.NumberOfPages
    '                        Dim page As Integer = 0
    '                        While page < n
    '                            page = page + 1
    '                            copy.AddPage(copy.GetImportedPage(reader, page))
    '                        End While
    '                    Next
    '                End Using
    '            End Using

    '            mergedPdf = ms.ToArray()

    '            Return mergedPdf

    '        End Using
    '    Catch ex As Exception
    '        MergeFilesByte = Nothing
    '        MsgBox("Load Merge Data : " & vbCrLf & ex.Message, MsgBoxStyle.Information, Me.Text)
    '    End Try
    'End Function
#End Region
#Region "Form"
    Private Sub fn_LoadDokter(ByVal Paramater As String)
        Try
            If Paramater <> "1058" Then
                If grdKDDEPARTMENT.Text.Contains("MCU") Then
                    If grdKDDEPARTMENT.Text = String.Empty Then Exit Sub

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
                    SQL &= "B.KDDOCTOR, B.NAME_DISPLAY "
                    SQL &= "FROM  "
                    SQL &= "M_DOCTOR_DEPARTMENT A "
                    SQL &= "INNER JOIN M_DOCTOR B "
                    SQL &= "ON A.KDDOCTOR = B.KDDOCTOR "
                    SQL &= "WHERE "
                    SQL &= "B.ISACTIVE = '1' "
                    SQL &= "AND A.KDDEPARTMENT = '" & grdKDDEPARTMENT.EditValue & "' "

                    SQL &= "ORDER BY NAME_DISPLAY "

                    oComm.Connection = oConn
                    oComm.CommandText = SQL
                    oComm.CommandTimeout = 120
                    oComm.CommandType = CommandType.Text

                    da = New SqlDataAdapter(oComm)
                    da.Fill(ds, "M_DOCTOR")

                    grdDPJP.Properties.DataSource = ds.Tables("M_DOCTOR")
                    grdDPJP.Properties.ValueMember = "KDDOCTOR"
                    grdDPJP.Properties.DisplayMember = "NAME_DISPLAY"

                    If oConn.State = ConnectionState.Open Then
                        oConn.Close()
                    End If

                    grdDPJP.Text = Paramater
                Else
                    If grdKDDEPARTMENT.Text = String.Empty Then Exit Sub

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
                    SQL &= "KDDOCTOR, NAME_DISPLAY "
                    SQL &= "FROM  "
                    SQL &= "M_DOCTOR "
                    SQL &= "WHERE "
                    SQL &= "ISACTIVE = '1' "
                    SQL &= "AND KDDEPARTMENT = '" & grdKDDEPARTMENT.EditValue & "' "

                    SQL &= "ORDER BY NAME_DISPLAY "

                    oComm.Connection = oConn
                    oComm.CommandText = SQL
                    oComm.CommandTimeout = 120
                    oComm.CommandType = CommandType.Text

                    da = New SqlDataAdapter(oComm)
                    da.Fill(ds, "M_DOCTOR")

                    grdDPJP.Properties.DataSource = ds.Tables("M_DOCTOR")
                    grdDPJP.Properties.ValueMember = "KDDOCTOR"
                    grdDPJP.Properties.DisplayMember = "NAME_DISPLAY"

                    If oConn.State = ConnectionState.Open Then
                        oConn.Close()
                    End If

                    grdDPJP.Text = Paramater
                End If

            Else
                If grdKDDEPARTMENT.Text = String.Empty Then Exit Sub

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
                SQL &= "KDDOCTOR, NAME_DISPLAY "
                SQL &= "FROM  "
                SQL &= "M_DOCTOR "
                SQL &= "WHERE "
                SQL &= "ISACTIVE = '1' "
                SQL &= "AND OTHER = 'SKIN' "
                SQL &= "ORDER BY NAME_DISPLAY "

                oComm.Connection = oConn
                oComm.CommandText = SQL
                oComm.CommandTimeout = 120
                oComm.CommandType = CommandType.Text

                da = New SqlDataAdapter(oComm)
                da.Fill(ds, "M_DOCTOR")

                grdDPJP.Properties.DataSource = ds.Tables("M_DOCTOR")
                grdDPJP.Properties.ValueMember = "KDDOCTOR"
                grdDPJP.Properties.DisplayMember = "NAME_DISPLAY"

                If oConn.State = ConnectionState.Open Then
                    oConn.Close()
                End If

                grdDPJP.Text = Paramater
            End If


        Catch oErr As Exception
            MsgBox("Load Dokter Data : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub frmItem_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        'If sisDOkter = False Then Exit Sub
        'Select Case e.KeyCode
        '    Case Keys.F3
        '        If lblRegister.Text = "" Then
        '            MsgBox("Pasien Belum dipilih, Silahkan kembali ke List Pasien", MsgBoxStyle.Exclamation, Me.Text)
        '            Exit Sub
        '        End If

        '        If grvListPasien.GetFocusedRowCellValue("Antrian") <> "" Then
        '            Dim uTime As Integer = (DateTime.UtcNow - New DateTime(1970, 1, 1, 0, 0, 0)).TotalSeconds
        '            Dim JsonRequest As String = fn_RequestUpdateWaktuAntrean(grvListPasien.GetFocusedRowCellValue("Antrian"), 4, uTime)
        '            If JsonRequest <> "" Then
        '                MsgBox(fn_UpdateWaktuAntrean(JsonRequest, uTime), MsgBoxStyle.Information, Me.Text)
        '            End If
        '        End If

        '        Dim oKoding As New Admission.clsKoding
        '        Dim dsKoding = oKoding.GetDataByKDPENDAFTARAN(lblRegister.Text)
        '        If dsKoding IsNot Nothing Then
        '            Dim frmMedrekRawatJalan As New frmMedrekRawatJalan
        '            Try
        '                frmMedrekRawatJalan.fn_LoadIdentias(txtBERATBADAN.Text, txtTB.Text, txtALERGIOBAT.Text, lblRegister.Text, lblTanggalLahirUsia.Text, lblNamaPasien.Text, lblRM.Text, lblDPJP.Text, grdKDDEPARTMENT.Text, lblSIP.Text, lblPenjamin.Text, lblKodeBooking.Text, grdKDDEPARTMENT.EditValue, grdDPJP.EditValue, lblNOMORSEP.Text, lblNOKARTUBPJS.Text, txtDIAGNOSAUTAMA.Text, deDATELAHIR.DateTime, IIf(lblJK.Text = "L", "1", "2"), grvListPasien.GetFocusedRowCellValue("AntrianDokter"))
        '                frmMedrekRawatJalan.LoadMe(FORM_MODE.FORM_MODE_EDIT, dsKoding.KDKODING)
        '                frmMedrekRawatJalan.ShowDialog(Me)
        '                fn_LoadSecurity()
        '            Catch ex As Exception
        '                MsgBox("Load Form Detail : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
        '            Finally
        '                If Not frmMedrekRawatJalan Is Nothing Then frmMedrekRawatJalan.Dispose()
        '                frmMedrekRawatJalan = Nothing

        '                Dim rowHandle As Integer = grvListPasien.LocateByValue(rowHandle, grvListPasien.Columns("NoRegister"), sCode)
        '                If rowHandle > 0 Then grvListPasien.FocusedRowHandle = rowHandle

        '                If sStatusSave = "NEW" Then
        '                    sStatusSave = "NONE"
        '                End If
        '            End Try
        '        Else
        '            Dim frmMedrekRawatJalan As New frmMedrekRawatJalan
        '            Try
        '                frmMedrekRawatJalan.fn_LoadIdentias(txtBERATBADAN.Text, txtTB.Text, txtALERGIOBAT.Text, lblRegister.Text, lblTanggalLahirUsia.Text, lblNamaPasien.Text, lblRM.Text, lblDPJP.Text, grdKDDEPARTMENT.Text, lblSIP.Text, lblPenjamin.Text, lblKodeBooking.Text, grdKDDEPARTMENT.EditValue, grdDPJP.EditValue, lblNOMORSEP.Text, lblNOKARTUBPJS.Text, txtDIAGNOSAUTAMA.Text, deDATELAHIR.DateTime, IIf(lblJK.Text = "L", "1", "2"), grvListPasien.GetFocusedRowCellValue("AntrianDokter"))
        '                frmMedrekRawatJalan.LoadMe(FORM_MODE.FORM_MODE_ADD)
        '                frmMedrekRawatJalan.ShowDialog(Me)
        '                fn_LoadSecurity()
        '            Catch ex As Exception
        '                MsgBox("Load Form Detail : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
        '            Finally
        '                If Not frmMedrekRawatJalan Is Nothing Then frmMedrekRawatJalan.Dispose()
        '                frmMedrekRawatJalan = Nothing

        '                Dim rowHandle As Integer = grvListPasien.LocateByValue(rowHandle, grvListPasien.Columns("NoRegister"), sCode)
        '                If rowHandle > 0 Then grvListPasien.FocusedRowHandle = rowHandle

        '                If sStatusSave = "NEW" Then
        '                    sStatusSave = "NONE"
        '                End If
        '            End Try
        '        End If
        'End Select
    End Sub
    Private Sub grdKDDEPARTMENT_EditValueChanged(sender As Object, e As EventArgs) Handles grdKDDEPARTMENT.EditValueChanged
        fn_LoadDokter(grdKDDEPARTMENT.EditValue)

        If grdKDDEPARTMENT.EditValue = "1058"
            XtraTabControl1.SelectedTabPageIndex = 14
        Else
            XtraTabControl1.SelectedTabPageIndex = 0
        End If

        If isLoad = True Then
            fn_LoadView()
        End If
    End Sub
    Private Sub chkALERGI_TIDAK_CheckedChanged(sender As Object, e As EventArgs) Handles chkALERGI_TIDAK.CheckedChanged
        If isLoad = True Then
            If chkALERGI_TIDAK.Checked = True Then
                txtALERGIOBAT.ResetText()
                chkALERGI_YA.Checked = False
            End If
        End If
    End Sub
    Private Sub chkALERGI_YA_CheckedChanged(sender As Object, e As EventArgs) Handles chkALERGI_YA.CheckedChanged
        If isLoad = True Then
            If chkALERGI_YA.Checked = True Then
                chkALERGI_TIDAK.Checked = False
            End If
        End If
    End Sub
    'Private Sub btnMasukPoli_Click(sender As Object, e As EventArgs) Handles btnMasukPoli.Click
    '    Try
    '        If grvListPasien.GetFocusedRowCellValue("Antrian") Is Nothing Then
    '            Exit Sub
    '        End If

    '        If grvListPasien.GetFocusedRowCellValue("Antrian") <> "" Then
    '            Dim uTime As Integer = (DateTime.UtcNow - New DateTime(1970, 1, 1, 0, 0, 0)).TotalSeconds
    '            Dim JsonRequest As String = fn_RequestUpdateWaktuAntrean(grvListPasien.GetFocusedRowCellValue("Antrian"), 4, uTime)
    '            If JsonRequest <> "" Then
    '                MsgBox(fn_UpdateWaktuAntrean(JsonRequest, uTime), MsgBoxStyle.Information, Me.Text)
    '            End If
    '        End If
    '    Catch oErr As Exception
    '        MsgBox("Print Data : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
    '    End Try
    'End Sub
    Private Function fn_RequestUpdateWaktuAntrean(ByVal KODEBOOKING As String, ByVal ISPANGGIL As Integer, ByVal uTime As Integer) As String
        Try
            Dim jsonRequest As String = String.Empty

            jsonRequest = " { "
            jsonRequest &= """kodebooking"": """ & KODEBOOKING & ""","
            jsonRequest &= """taskid"": """ & ISPANGGIL & """, "
            jsonRequest &= """waktu"": """ & uTime & "000" & """ "
            jsonRequest &= "}  "

            fn_RequestUpdateWaktuAntrean = jsonRequest

        Catch oErr As Exception
            fn_RequestUpdateWaktuAntrean = ""
        End Try
    End Function
    Public Function fn_UpdateWaktuAntrean(ByVal jsonRequest As String, ByVal uTime As Integer) As String
        Try
            Dim oSetKoneksi As New Brigging.clsSetKoneksi

            Dim dsSetKoneksi = oSetKoneksi.UpdateWaktuAntrean(sUrlAntrean, sConsidAntrean, sSecreateKeyAntrean, sUserKeyAntrean, uTime, jsonRequest)

            Dim allData = JObject.Parse(dsSetKoneksi)

            Dim CodeResponse As String = String.Empty
            Dim messageResponse As String = String.Empty

            CodeResponse = IIf(IsDBNull(allData.Item("metadata").Item("code")) = True, "", allData.Item("metadata").Item("code"))
            messageResponse = allData("metadata")("message").ToString

            If CodeResponse = "200" Then
                fn_UpdateWaktuAntrean = CodeResponse
            Else
                fn_UpdateWaktuAntrean = CodeResponse & "-" & messageResponse
            End If

        Catch oErr As Exception
            fn_UpdateWaktuAntrean = oErr.Message
        End Try
    End Function
    Private Sub PemeriksaanAwalToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PemeriksaanAwalToolStripMenuItem.Click
        If txtKDPENDAFTARAN.Text = "" Then
            fn_EmptyMe()
            Exit Sub
        End If

        Dim oReqAwalPemeriksaan As New Transaksi.clsReqAwalPemeriksaan

        Dim ds = oReqAwalPemeriksaan.GetData(txtKDPENDAFTARAN.Text)

        If ds IsNot Nothing Then
            Dim frmReqAwalPemeriksaan As New frmReqAwalPemeriksaan
            Try
                frmReqAwalPemeriksaan.LoadMe(FORM_MODE.FORM_MODE_EDIT, txtNAMAPASIEN.Text, deDATETANGGALLAHIR.DateTime, txtKDCUSTOMER.Text, txtOBJEKTIF_JENISKELAMIN.Text, txtKDPENDAFTARAN.Text, grdDPJP.EditValue)
                frmReqAwalPemeriksaan.ShowDialog(Me)
                fn_LoadSecurity()
            Catch ex As Exception
                MsgBox("Load Form Detail : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
            Finally
                If Not frmReqAwalPemeriksaan Is Nothing Then frmReqAwalPemeriksaan.Dispose()
                frmReqAwalPemeriksaan = Nothing
            End Try
        Else
            Dim frmReqAwalPemeriksaan As New frmReqAwalPemeriksaan
            Try
                frmReqAwalPemeriksaan.LoadMe(FORM_MODE.FORM_MODE_ADD, txtNAMAPASIEN.Text, deDATETANGGALLAHIR.DateTime, txtKDCUSTOMER.Text, txtOBJEKTIF_JENISKELAMIN.Text, txtKDPENDAFTARAN.Text, grdDPJP.EditValue)
                frmReqAwalPemeriksaan.ShowDialog(Me)
                fn_LoadSecurity()
            Catch ex As Exception
                MsgBox("Load Form Detail : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
            Finally
                If Not frmReqAwalPemeriksaan Is Nothing Then frmReqAwalPemeriksaan.Dispose()
                frmReqAwalPemeriksaan = Nothing
            End Try
        End If

    End Sub
    Private Sub AddToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AddToolStripMenuItem.Click
        If txtKDPENDAFTARAN.Text = "" Then
            fn_EmptyMe()
            Exit Sub
        End If

        colKDITEM.OptionsColumn.ReadOnly = False
        colQTY.OptionsColumn.ReadOnly = False
        colREMARKS_DOKTER.OptionsColumn.ReadOnly = False
        colKDSIGNA.OptionsColumn.ReadOnly = False
        colKDCARAPAKAI.OptionsColumn.ReadOnly = False
        grdDetailResep.ContextMenuStrip = mnuStrip
        GroupControl1.Text = "Resep"

        If sisDOkter = True Then
            If grdKDDEPARTMENT.Text <> "IGD" Then
                Try
                    Dim oConn As New SqlConnection
                    Dim oComm As New SqlCommand
                    Dim da As SqlDataAdapter
                    Dim ds As New DataSet
                    Dim SQL As String
                    oConn = New SqlConnection(sConnOld)

                    If oConn.State = ConnectionState.Closed Then
                        oConn.Open()
                    End If

                    SQL = "SELECT "
                    SQL &= "* "
                    SQL &= "FROM "
                    SQL &= "S_PENDAFTARAN_H A "
                    SQL &= "INNER JOIN M_DEPARTMENT B "
                    SQL &= "ON A.KDDEPARTMENT = B.KDDEPARTMENT "
                    SQL &= "INNER JOIN M_DOCTOR C "
                    SQL &= "ON A.KDDOCTOR = C.KDDOCTOR "
                    SQL &= "WHERE "
                    SQL &= "A.KDREG = '" & txtKDPENDAFTARAN.Text & "' "

                    oComm.Connection = oConn
                    oComm.CommandText = SQL
                    oComm.CommandTimeout = 120
                    oComm.CommandType = CommandType.Text

                    da = New SqlDataAdapter(oComm)
                    da.Fill(ds, "SEARCHPENDAFTARAN")

                    If oConn.State = ConnectionState.Open Then
                        oConn.Close()
                    End If

                    For iLoop As Integer = 0 To ds.Tables("SEARCHPENDAFTARAN").Rows.Count - 1
                        With ds.Tables("SEARCHPENDAFTARAN")
                            If .Rows(iLoop)("NOMORANTRIAN") <> "" Then
                                If fn_subcektTaksId4(.Rows(iLoop)("NOMORANTRIAN"), 4) = False Then
                                    'fn_SaveTransaksi(.Rows(iLoop)("KDPOLIBPJS"), .Rows(iLoop)("KODEANTRIAN"), .Rows(iLoop)("ANTRIANDOKTER"), "ULANG")
                                Else
                                    'fn_SaveTransaksi(.Rows(iLoop)("KDPOLIBPJS"), .Rows(iLoop)("KODEANTRIAN"), .Rows(iLoop)("ANTRIANDOKTER"), "")
                                End If
                            Else
                                'fn_SaveTransaksi(.Rows(iLoop)("KDPOLIBPJS"), .Rows(iLoop)("KODEANTRIAN"), .Rows(iLoop)("ANTRIANDOKTER"), "")
                            End If
                        End With
                    Next

                Catch oErr As Exception
                    MsgBox("Print Data : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            End If
        End If

        If grdKDDEPARTMENT.Text = "REHAB MEDIK" Then
            lFisio1.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            lFisio2.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            lFisio3.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            lFisio4.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            lFisio5.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            lFisio6.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            lFisio7.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            lFisio8.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            lFisio9.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            lFisio10.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            lFisio11.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        Else
            lFisio1.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            lFisio2.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            lFisio3.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            lFisio4.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            lFisio5.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            lFisio6.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            lFisio7.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            lFisio8.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            lFisio9.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            lFisio10.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            lFisio11.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        End If

        fn_ChangeFormState()
        fn_LoadTindakLanjut(txtKDPENDAFTARAN.Text)

        lGROUPLISTPASIEN.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        lGROUPFORM.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        If chkLISTANTRIAN.Checked = True Then
            fn_LoadDataList()
            lLISTANTRIAN.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        End If
        'lLISTANTRIANCHEK.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always

        'If txtKODEBOOKING.Text <> "" Then
        '    Dim uTime As Integer = (DateTime.UtcNow - New DateTime(1970, 1, 1, 0, 0, 0)).TotalSeconds
        '    Dim JsonRequest As String = fn_RequestUpdateWaktuAntrean(txtKODEBOOKING.Text, 4, uTime)
        '    If JsonRequest <> "" Then
        '        MsgBox(fn_UpdateWaktuAntrean(JsonRequest, uTime), MsgBoxStyle.Information, Me.Text)
        '    End If
        'End If

        Dim oKoding As New Admission.clsKoding
        Dim dsKoding = oKoding.GetDataByKDPENDAFTARAN(txtKDPENDAFTARAN.Text)

        If dsKoding IsNot Nothing Then
            sNoidSimpan = dsKoding.KDKODING
            oFormMode = FORM_MODE.FORM_MODE_EDIT

            Try
                Dim oConn As New SqlConnection
                Dim oComm As New SqlCommand
                Dim da As SqlDataAdapter
                Dim ds As New DataSet
                Dim SQL As String
                oConn = New SqlConnection(sConnOld)

                If oConn.State = ConnectionState.Closed Then
                    oConn.Open()
                End If

                SQL = "SELECT "
                SQL &= "TOP 1 A.KDREQRECIPE "
                SQL &= "FROM "
                SQL &= "BUDIJAYAGROUP_RSKC..S_RECIPE_H A "
                SQL &= "WHERE "
                SQL &= "A.KDREQRECIPE = '" & sNoidSimpan & "' "

                oComm.Connection = oConn
                oComm.CommandText = SQL
                oComm.CommandTimeout = 120
                oComm.CommandType = CommandType.Text

                da = New SqlDataAdapter(oComm)
                da.Fill(ds, "S_RECIPE_H")

                If oConn.State = ConnectionState.Open Then
                    oConn.Close()
                End If

                GroupControl1.Text = "Resep"

                For iLoop As Integer = 0 To ds.Tables("S_RECIPE_H").Rows.Count - 1
                    GroupControl1.Text = "Resep Sudah di Terima"

                    grdDetailResep.ContextMenuStrip = Nothing

                    colKDITEM.OptionsColumn.ReadOnly = True
                    colQTY.OptionsColumn.ReadOnly = True
                    colREMARKS_DOKTER.OptionsColumn.ReadOnly = True
                    colKDSIGNA.OptionsColumn.ReadOnly = True
                    colKDCARAPAKAI.OptionsColumn.ReadOnly = True
                Next

            Catch oErr As Exception
                MsgBox("Load Data Resep  " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            End Try

            fn_LoadData(sNoidSimpan)
        Else
            sNoidSimpan = ""

            'sPicture = Nothing
            'chkALERGI_YA.Checked = False
            'chkALERGI_TIDAK.Checked = False
            'deDATE.DateTime = Now
            'txtDIAGNOOSA.ResetText()
            'deDATETANGGALLAHIR.DateTime = Now
            'txtOBJEKTIF_JENISKELAMIN.ResetText()
            'txtALAMATGAMBAR.ResetText()

            grvTindakanPoli.OptionsSelection.MultiSelect = True
            grvTindakanPoli.SelectAll()
            grvTindakanPoli.DeleteSelectedRows()
            grvTindakanPoli.OptionsSelection.MultiSelect = False

            oFormMode = FORM_MODE.FORM_MODE_ADD
            Dim oReqAwalPemeriksaan As New Transaksi.clsReqAwalPemeriksaan
            Dim dsReqAwalPemeriksaan = oReqAwalPemeriksaan.GetData(txtKDPENDAFTARAN.Text)

            Dim dsTambahan = oReqAwalPemeriksaan.GetDataTambahan(txtKDPENDAFTARAN.Text)
            If dsTambahan IsNot Nothing Then
                txtOBJEKTIF_KESADARAN.Text = dsTambahan.TAMBAH1
                txtOBJEKTIF_IMT.Text = dsTambahan.TAMBAH2
                txtOBJEKTIF_BBIDEAL.Text = dsTambahan.TAMBAH3
                txtOBJEKTIF_BBDITURUNKAN.Text = dsTambahan.TAMBAH4
            End If

            If dsReqAwalPemeriksaan IsNot Nothing Then
                chkALERGI_YA.Checked = dsReqAwalPemeriksaan.ALERGI_YA
                chkALERGI_TIDAK.Checked = dsReqAwalPemeriksaan.ALERGI_TIDAK
                txtALERGIOBAT.Text = dsReqAwalPemeriksaan.ALERGI_TEXT
                txtSUBJEKTIF.Text = dsReqAwalPemeriksaan.SUBJEKTIF
                txtOBJEKTIF_UMUR.Text = dsReqAwalPemeriksaan.OBJEKTIF_UMUR
                txtOBJEKTIF_BERATBADAN.Text = IIf(CDec(dsReqAwalPemeriksaan.OBJEKTIF_BERATBADAN) = 0, "", dsReqAwalPemeriksaan.OBJEKTIF_BERATBADAN)
                txtOBJEKTIF_TINGGIBADAN.Text = IIf(CDec(dsReqAwalPemeriksaan.OBJEKTIF_TINGGIBADAN) = 0, "", dsReqAwalPemeriksaan.OBJEKTIF_TINGGIBADAN)
                txtOBJEKTIF_TEKANANDARAH.Text = dsReqAwalPemeriksaan.OBJEKTIF_TEKANANDARAH
                txtOBJEKTIF_NADI.Text = dsReqAwalPemeriksaan.OBJEKTIF_NADI
                txtOBJEKTIF_RESPIRASI.Text = dsReqAwalPemeriksaan.OBJEKTIF_RESPIRASI
                txtOBJEKTIF_SATURASIOKSIGEN.Text = dsReqAwalPemeriksaan.OBJEKTIF_SATURASIOKSIGEN
                txtOBJEKTIF_SUHU.Text = dsReqAwalPemeriksaan.OBJEKTIF_SUHU
                txtDESKRIPSI.Text = dsReqAwalPemeriksaan.DESKRIPSI

                For Each xloop In oReqAwalPemeriksaan.GetDataDetail(txtKDPENDAFTARAN.Text)
                    grvTindakanPoli.Focus()
                    grvTindakanPoli.AddNewRow()
                    grvTindakanPoli.SetFocusedRowCellValue(colKETERANGAN_, xloop.KETERANGAN)
                    grvTindakanPoli.UpdateCurrentRow()
                Next
            Else
                Try
                    ' ***** HEADER *****
                    Dim ds = oReqAwalPemeriksaan.GetDataLast(txtKDCUSTOMER.Text)

                    If ds IsNot Nothing Then
                        'If MsgBox("Apakah akan di load data pertama?", MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, Me.Text) = MsgBoxResult.No Then Exit Sub

                        With ds
                            chkALERGI_YA.Checked = .ALERGI_YA
                            chkALERGI_TIDAK.Checked = .ALERGI_TIDAK
                            txtALERGIOBAT.Text = .ALERGI_TEXT
                            txtOBJEKTIF_BERATBADAN.Text = IIf(CDec(.OBJEKTIF_BERATBADAN) = 0, "", .OBJEKTIF_BERATBADAN)
                            txtOBJEKTIF_TINGGIBADAN.Text = IIf(CDec(.OBJEKTIF_TINGGIBADAN) = 0, "", .OBJEKTIF_TINGGIBADAN)
                        End With
                    End If
                Catch oErr As Exception
                    MsgBox("Load Data Awal Pasien" & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            End If

            grvTindakanPoli.AddNewRow()
            grvTindakanPoli.SetFocusedRowCellValue(colKETERANGAN_, "Konsultasi Medis")
            grvTindakanPoli.UpdateCurrentRow()
        End If

        If grdKDDEPARTMENT.EditValue = "1058" Then
            lRujukEksternal.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            'lRujukInternal.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            lRujukInternalAlihRawat.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            lRujukanHabis.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            lPRB.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            lSatuKaliIterasi.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            lDuaKaliIterasi.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            lRujukBalik.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            lRawatInap.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            'lPenunjangHariIni.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            lSuratKeteranganSehat.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            lSuratKeterangSakit.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        Else
            lRujukEksternal.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            'lRujukInternal.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            lRujukInternalAlihRawat.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            lRujukanHabis.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            lPRB.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            lSatuKaliIterasi.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            lDuaKaliIterasi.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            lRujukBalik.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            lRawatInap.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            'lPenunjangHariIni.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            lSuratKeteranganSehat.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            lSuratKeterangSakit.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        End If

        txtPenjamin.Focus()

        isLoad = True

    End Sub
    Private Sub AsesmenMedisIGDToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AsesmenMedisIGDToolStripMenuItem.Click
        'If lblRegister.Text = "" Then
        '    MsgBox("Pasien Belum dipilih, Silahkan kembali ke List Pasien", MsgBoxStyle.Exclamation, Me.Text)
        '    Exit Sub
        'End If
        'If lblRegister.Text = "Nul" Then
        '    MsgBox("Pasien Belum dipilih, Silahkan kembali ke List Pasien", MsgBoxStyle.Exclamation, Me.Text)
        '    Exit Sub
        'End If

        'Dim frmEMedrekIDG_01 As New frmEMedrekIDG_01
        'Try
        '    frmEMedrekIDG_01.LoadMe(lblRegister.Text, grdDPJP.EditValue, lblRM.Text, lblNamaPasien.Text, lblJK.Text, deDATELAHIR.DateTime.ToString("dd-MM-yyyy"), "-")
        '    frmEMedrekIDG_01.ShowDialog(Me)
        '    fn_LoadSecurity()
        'Catch ex As Exception
        '    MsgBox("Load Form Detail : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
        'Finally
        '    If Not frmEMedrekIDG_01 Is Nothing Then frmEMedrekIDG_01.Dispose()
        '    frmEMedrekIDG_01 = Nothing

        '    Dim rowHandle As Integer = grvListPasien.LocateByValue(rowHandle, grvListPasien.Columns("NoRegister"), sCode)
        '    If rowHandle > 0 Then grvListPasien.FocusedRowHandle = rowHandle

        '    If sStatusSave = "NEW" Then
        '        sStatusSave = "NONE"
        '    End If

        '    XtraTabControl1.SelectedTabPageIndex = 4
        'End Try
    End Sub
    Private Sub AsesmenAwalPerawatIGDToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AsesmenAwalPerawatIGDToolStripMenuItem.Click
        'If lblRegister.Text = "" Then
        '    MsgBox("Pasien Belum dipilih, Silahkan kembali ke List Pasien", MsgBoxStyle.Exclamation, Me.Text)
        '    Exit Sub
        'End If
        'If lblRegister.Text = "Nul" Then
        '    MsgBox("Pasien Belum dipilih, Silahkan kembali ke List Pasien", MsgBoxStyle.Exclamation, Me.Text)
        '    Exit Sub
        'End If

        'Dim frmEMedrekIDG_02_New As New frmEMedrekIDG_02_New
        'Try
        '    frmEMedrekIDG_02_New.LoadRegister(lblRegister.Text, grdDPJP.EditValue, lblRM.Text, lblNamaPasien.Text, lblJK.Text, deDATELAHIR.DateTime.ToString("dd-MM-yyyy"), deDATE_MASUK.DateTime.ToString("dd-MM-yyyy HH:mm:ss"))
        '    frmEMedrekIDG_02_New.LoadMe(lblRegister.Text)
        '    frmEMedrekIDG_02_New.ShowDialog(Me)
        '    fn_LoadSecurity()
        'Catch ex As Exception
        '    MsgBox("Load Form Detail : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
        'Finally
        '    If Not frmEMedrekIDG_02_New Is Nothing Then frmEMedrekIDG_02_New.Dispose()
        '    frmEMedrekIDG_02_New = Nothing

        '    XtraTabControl1.SelectedTabPageIndex = 5
        'End Try
    End Sub
#End Region
#Region "Form Medrek Rawat Jalan"
    Private Function fn_Validate() As Boolean
        Try
            fn_Validate = True

            Try
                If txtOBJEKTIF_BERATBADAN.Text <> "" Then
                    Dim TES = CDec(txtOBJEKTIF_BERATBADAN.Text)
                End If
            Catch ex As Exception
                MsgBox("Berat Badan Harus Angka" & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
                fn_Validate = False
                Exit Function
            End Try

            Try
                If txtOBJEKTIF_TINGGIBADAN.Text <> "" Then
                    Dim TES = CDec(txtOBJEKTIF_TINGGIBADAN.Text)
                End If
            Catch ex As Exception
                MsgBox("Tinggi Badan Harus Angka" & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
                fn_Validate = False
                Exit Function
            End Try

            If txtKDPENDAFTARAN.Text = String.Empty Then
                MsgBox("Dibutuhkan Register", MsgBoxStyle.Exclamation, Me.Text)
                'lblRegister.Focus()
                fn_Validate = False
                Exit Function
            End If

            If txtDIAGNOOSA.Text = "----" Then
                MsgBox("Dibutuhkan Diagnosa", MsgBoxStyle.Exclamation, Me.Text)
                txtDIAGNOOSA.Focus()
                fn_Validate = False
                Exit Function
            End If
            If txtDIAGNOOSA.Text = "- - -" Then
                MsgBox("Dibutuhkan Diagnosa", MsgBoxStyle.Exclamation, Me.Text)
                txtDIAGNOOSA.Focus()
                fn_Validate = False
                Exit Function
            End If
            If txtDIAGNOOSA.Text = String.Empty Then
                MsgBox("Dibutuhkan Diagnosa", MsgBoxStyle.Exclamation, Me.Text)
                txtDIAGNOOSA.Focus()
                fn_Validate = False
                Exit Function
            End If
            If txtTINDAKLANJUT.Text = String.Empty Then
                MsgBox("Dibutuhkan Tindak Lanjut", MsgBoxStyle.Exclamation, Me.Text)
                txtTINDAKLANJUT.Focus()
                fn_Validate = False
                Exit Function
            End If
        Catch oErr As Exception
            MsgBox("Validate Data : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Function
    Private Function fn_SaveCustomer_Detil() As Boolean
        Try
            fn_SaveCustomer_Detil = True

            Dim oCustomer_Detil As New Reference.clsCustomerDetil
            ' ***** HEADER *****
            Dim ds = oCustomer_Detil.GetStructureHeader
            With ds
                .DATECREATED = Now
                .DATEUPDATED = Now
                .KDCUSTOMER = txtKDCUSTOMER.Text
                .BB = IIf(txtOBJEKTIF_BERATBADAN.Text = "", 0, txtOBJEKTIF_BERATBADAN.Text.Replace(",", "."))
                If txtOBJEKTIF_TINGGIBADAN.Text = "" Then
                    .TT = 0
                Else
                    .TT = txtOBJEKTIF_TINGGIBADAN.Text.Replace(",", ".")
                End If

                .ALERGI = txtALERGIOBAT.Text
                .DIAGNOSA = txtDIAGNOOSA.Text
            End With

            Dim dsCsutomer_Detil = oCustomer_Detil.GetData(txtKDCUSTOMER.Text)

            If dsCsutomer_Detil Is Nothing Then
                oCustomer_Detil.InsertData(ds)
            Else
                oCustomer_Detil.UpdateData(ds)
            End If

        Catch oErr As Exception
            MsgBox("Simpan Data Save Customer Detil : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            fn_SaveCustomer_Detil = False
        End Try
    End Function
    Private Sub btnBATAL_Click(sender As Object, e As EventArgs) Handles btnBATAL.Click
        isLoad = False

        sNoidSimpan = ""
        lGROUPLISTPASIEN.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        lGROUPFORM.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        lLISTANTRIAN.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        'lLISTANTRIANCHEK.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        fn_LoadSecurity()
    End Sub
    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        If fn_Validate() = False Then
            Exit Sub
        End If

        Dim sOrder As Boolean = False
        Dim sLab As Boolean = False
        Dim sRad As Boolean = False

        For i As Integer = 0 To grvTindakan.RowCount - 2
            If grvTindakan.GetRowCellValue(i, colTINDAKAN).ToString.Contains("LABORATORIUM") Then
                sLab = True
            Else
                sOrder = True
            End If
            If grvTindakan.GetRowCellValue(i, colTINDAKAN).ToString.Contains("RADIOLOGI") Then
                sRad = True
            Else
                sOrder = True
            End If
        Next

        Dim frmPilihanMedrek As New frmPilihanMedrek
        frmPilihanMedrek.LoadMe(True, True, sOrder, IIf(txtTINDAKLANJUT.Text = "", False, True), sLab, sRad)
        frmPilihanMedrek.ShowDialog(Me)

        If sSIMPAN = False Then
            'isLoad = False
            'sNoidSimpan = ""
            'lGROUPLISTPASIEN.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            'lGROUPFORM.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            Exit Sub
        End If

        If fn_SaveCustomer_Detil() = False Then
            MsgBox("Simpan Data Save Customer Detil!", MsgBoxStyle.Exclamation, Me.Text)
        Else
            If fn_Save(sNoidSimpan) = False Then
                MsgBox("Save gagal! Tolong kontak admin anda!", MsgBoxStyle.Exclamation, Me.Text)
                Exit Sub
            Else
                frmMedrekRawatJalan.fn_subcekPoli(txtKODEBOOKING.Text)

                MsgBox("Save " & txtKDPENDAFTARAN.Text.Trim.ToUpper & " success!", MsgBoxStyle.Information, Me.Text)

                'Try
                '    If txtKODEBOOKING.Text <> "" Then
                '        Dim uTime As Integer = (DateTime.UtcNow - New DateTime(1970, 1, 1, 0, 0, 0)).TotalSeconds
                '        Dim JsonRequest As String = fn_RequestUpdateWaktuAntrean(txtKODEBOOKING.Text, 5, uTime)
                '        If JsonRequest <> "" Then
                '            fn_UpdateWaktuAntrean(JsonRequest, uTime)
                '            'MsgBox(fn_UpdateWaktuAntrean(JsonRequest, uTime), MsgBoxStyle.Information, Me.Text)
                '        End If
                '    End If
                'Catch oErr As Exception
                '    MsgBox("Update BPJS Waktu Tunggu : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
                'End Try

                Dim oNameModul As New Setting.clsCounter
                oNameModul.UpdateDataCekSuara()
            End If
        End If

        isLoad = False
        sNoidSimpan = ""
        lGROUPLISTPASIEN.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        lGROUPFORM.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        lLISTANTRIAN.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        'lLISTANTRIANCHEK.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        fn_EmptyMe()

        fn_LoadSecurity()

    End Sub
    Private Sub fn_ChangeFormState()
        fn_LoadTEMPLATE()
        fn_LoadITEM()
        fn_LoadUOM()
        fn_LoadSIGNA()
        fn_LoadCARAPAKAI()
    End Sub
    Private Sub fn_LoadTEMPLATE()
        Dim oTemplate As New Reference.clsTemplateResep
        Try
            grdTEMPLATE.Properties.DataSource = oTemplate.GetData.Where(Function(x) x.NOIDUSER = sUserID).ToList()
            grdTEMPLATE.Properties.ValueMember = "KDTEMPLATE"
            grdTEMPLATE.Properties.DisplayMember = "DESCRIPTION"
        Catch oErr As Exception
            MsgBox("Load Template Data : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadITEM()
        Try
            Dim oConn As New SqlConnection
            Dim oComm As New SqlCommand
            Dim da As SqlDataAdapter
            Dim ds As New DataSet
            Dim SQL As String
            'Dim sConn As String = Decrypt(My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Software\SIMRS\SW\", "Database", "").ToString())

            oConn = New SqlConnection(sConnOld)

            If oConn.State = ConnectionState.Closed Then
                oConn.Open()
            End If

            'ds.PRICEPURCHASESTANDARD + (ds.PRICEPURCHASESTANDARD * (ds.MARGIN / 100))

            SQL = "SELECT "
            SQL &= "* "
            SQL &= "FROM ( "
            SQL &= "SELECT "
            SQL &= "A.KDITEM "
            SQL &= ",A.NMITEM1 "
            SQL &= ",A.NMITEM2 "
            'SQL &= ",HARGA = B.PRICEPURCHASESTANDARD "
            SQL &= ",HARGA = B.PRICEPURCHASESTANDARD + (B.PRICEPURCHASESTANDARD * (B.MARGIN / 100)) "
            SQL &= ",SATUAN = C.DESCRIPTION "
            SQL &= ",STOK = ISNULL((SELECT SUM(AMOUNT) FROM M_ITEM_WAREHOUSE WHERE B.KDITEM = KDITEM AND B.KDUOM = KDUOM GROUP BY KDITEM) ,0) "
            SQL &= "FROM M_ITEM A "
            SQL &= "INNER JOIN M_ITEM_UOM B "
            SQL &= "ON A.KDITEM = B.KDITEM "
            SQL &= "INNER JOIN M_UOM C "
            SQL &= "ON B.KDUOM = C.KDUOM "
            SQL &= "WHERE "
            SQL &= "A.ISACTIVE = 1 "
            SQL &= "AND B.RATE = 1 "

            SQL &= "UNION "

            SQL &= "SELECT "
            SQL &= "KDITEM = 999999 "
            SQL &= ",NMITEM1 = A.DESCRIPTION "
            SQL &= ",NMITEM2 = A.DESCRIPTION "
            SQL &= ",HARGA = 0 "
            SQL &= ",SATUAN = '-' "
            SQL &= ",STOK = 0 "
            SQL &= "FROM M_ITEM_RACIK A "
            SQL &= ") X "
            SQL &= "ORDER BY "
            SQL &= "X.HARGA "

            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "ITEM")

            grdKDITEM.DataSource = ds.Tables("ITEM")
            grdKDITEM.ValueMember = "KDITEM"
            grdKDITEM.DisplayMember = "NMITEM2"

            grdKDITEMOBATRACIKAN.DataSource = ds.Tables("ITEM")
            grdKDITEMOBATRACIKAN.ValueMember = "KDITEM"
            grdKDITEMOBATRACIKAN.DisplayMember = "NMITEM2"

            If oConn.State = ConnectionState.Open Then
                oConn.Close()
            End If
        Catch oErr As Exception
            MsgBox("Load Obat Data : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadUOM()
        Try
            Dim oConn As New SqlConnection
            Dim oComm As New SqlCommand
            Dim da As SqlDataAdapter
            Dim ds As New DataSet
            Dim SQL As String

            oConn = New SqlConnection(sConnOld)

            If oConn.State = ConnectionState.Closed Then
                oConn.Open()
            End If

            SQL = "SELECT "
            SQL &= "A.KDUOM "
            SQL &= ",A.DESCRIPTION "
            SQL &= "FROM M_UOM A "
            SQL &= "WHERE "
            SQL &= "A.ISACTIVE = 1 "

            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "UOM")

            grdUOM.DataSource = ds.Tables("UOM")
            grdUOM.ValueMember = "KDUOM"
            grdUOM.DisplayMember = "DESCRIPTION"

            grdKDUOMRACIKAN.DataSource = ds.Tables("UOM")
            grdKDUOMRACIKAN.ValueMember = "KDUOM"
            grdKDUOMRACIKAN.DisplayMember = "DESCRIPTION"

            If oConn.State = ConnectionState.Open Then
                oConn.Close()
            End If
        Catch oErr As Exception
            MsgBox("Load Satuan Data : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadSIGNA()
        Try
            Dim oConn As New SqlConnection
            Dim oComm As New SqlCommand
            Dim da As SqlDataAdapter
            Dim ds As New DataSet
            Dim SQL As String

            oConn = New SqlConnection(sConnOld)

            If oConn.State = ConnectionState.Closed Then
                oConn.Open()
            End If

            SQL = "SELECT "
            SQL &= "A.KDSIGNA "
            SQL &= ",A.DESCRIPTION "
            SQL &= "FROM M_SIGNA A "
            SQL &= "WHERE "
            SQL &= "A.ISACTIVE = 1 "

            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "SIGNA")

            grdKDSIGNA.DataSource = ds.Tables("SIGNA")
            grdKDSIGNA.ValueMember = "KDSIGNA"
            grdKDSIGNA.DisplayMember = "DESCRIPTION"

            If oConn.State = ConnectionState.Open Then
                oConn.Close()
            End If
        Catch oErr As Exception
            MsgBox("Load Signa Data : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadCARAPAKAI()
        Try
            Dim oConn As New SqlConnection
            Dim oComm As New SqlCommand
            Dim da As SqlDataAdapter
            Dim ds As New DataSet
            Dim SQL As String

            oConn = New SqlConnection(sConnOld)

            If oConn.State = ConnectionState.Closed Then
                oConn.Open()
            End If

            SQL = "SELECT "
            SQL &= "A.KDCP "
            SQL &= ",A.DESCRIPTION "
            SQL &= "FROM M_CARAPAKAI A "
            SQL &= "WHERE "
            SQL &= "A.ISACTIVE = 1 "

            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "CARAPAKAI")

            grdCARAPAKAI.DataSource = ds.Tables("CARAPAKAI")
            grdCARAPAKAI.ValueMember = "KDCP"
            grdCARAPAKAI.DisplayMember = "DESCRIPTION"

            If oConn.State = ConnectionState.Open Then
                oConn.Close()
            End If
        Catch oErr As Exception
            MsgBox("Load Signa Data : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadUpdateSudah(ByVal kdreg As String)
        Try
            Dim oConn As New SqlConnection
            Dim oComm As New SqlCommand
            Dim da As SqlDataAdapter
            Dim ds As New DataSet
            Dim SQL As String
            'Dim sConn As String = Decrypt(My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Software\SIMRS\SW\", "Database", "").ToString())

            oConn = New SqlConnection(sConnOld)

            If oConn.State = ConnectionState.Closed Then
                oConn.Open()
            End If

            SQL = "UPDATE "
            SQL &= "S_PENDAFTARAN_H "
            SQL &= "SET ISRESUME = 1 "
            SQL &= ",KETERANGAN_TINDAKLANJUT = '" & txtTINDAKLANJUT.Text & "' "
            SQL &= "WHERE "
            SQL &= "KDREG = '" & kdreg & "' "

            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "UPDATEPENDAFTARAN")

            If oConn.State = ConnectionState.Open Then
                oConn.Close()
            End If
        Catch oErr As Exception
            MsgBox("Load Obat Data : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadDataUpdateAntian()
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
            SQL &= ",KELOMPOK = ISNULL((SELECT BB.KELOMPOK FROM Z_COUNTER_ANTRIAN_D AA INNER JOIN Z_COUNTER_ANTRIAN_H BB ON AA.KDCOUNTER = BB.KDCOUNTER  WHERE A.KDANTRIANNEW = AA.KDCOUNTER + CONVERT(nvarchar(50),AA.SEQ) + AA.ANTRIAN AND CONVERT(VARCHAR(8), BB.DATE, 112) = '" & deDATE.DateTime.ToString("yyyyMMdd") & "') , '1')  "
            SQL &= "FROM "
            SQL &= "S_PENDAFTARAN_H A "
            SQL &= "WHERE CONVERT(VARCHAR(8), A.DATE, 112) = '" & deDATE.DateTime.ToString("yyyyMMdd") & "' "
            SQL &= "AND A.KDDEPARTMENT = " & grdKDDEPARTMENT.EditValue & " "
            SQL &= "AND A.KDDOCTOR = " & grdDPJP.EditValue & " "

            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "S_PENDAFTARAN_H")

            Dim TotalKelompok1 As Integer = 0
            Dim TotalKelompok2 As Integer = 0
            Dim SudahTotalKelompok1 As Integer = 0
            Dim SudahTotalKelompok2 As Integer = 0

            For iLoop As Integer = 0 To ds.Tables("S_PENDAFTARAN_H").Rows.Count - 1
                With ds.Tables("S_PENDAFTARAN_H")
                    If .Rows(iLoop)("KELOMPOK") = "1" Then
                        TotalKelompok1 += 1

                        If .Rows(iLoop)("ISRESUME") = "1" Then
                            SudahTotalKelompok1 += 1
                        End If
                    Else
                        TotalKelompok2 += 1

                        If .Rows(iLoop)("ISRESUME") = "1" Then
                            SudahTotalKelompok2 += 1
                        End If
                    End If
                End With
            Next

            If oConn.State = ConnectionState.Open Then
                oConn.Close()
            End If

            fn_UpdateAntian(TotalKelompok1, SudahTotalKelompok1, "1")
            fn_UpdateAntian(TotalKelompok2, SudahTotalKelompok2, "2")

        Catch oErr As Exception
            MsgBox("Load List Pasien Data : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation)
        End Try
    End Sub
    Private Sub fn_UpdateAntian(ByVal Total As Integer, ByVal Sisa As Integer, ByVal KELOMPOK As String)
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

            SQL = "UPDATE "
            SQL &= "Z_MASTER_ATRIANSEMUAPOLI "
            SQL &= "SET "
            SQL &= "KETERANGAN = '" & Sisa.ToString.PadLeft(3, "0") & "/" & Total.ToString.PadLeft(3, "0") & "' "
            SQL &= "WHERE "
            SQL &= "KDDEPARTMENT = " & grdKDDEPARTMENT.EditValue & " "
            SQL &= "AND KDDOCTOR = " & grdDPJP.EditValue & " "
            SQL &= "AND KELOMPOK = '" & KELOMPOK & "' "

            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "UPDATE_ S_PENDAFTARAN_H")

            If oConn.State = ConnectionState.Open Then
                oConn.Close()
            End If

        Catch oErr As Exception
            MsgBox("Load List Pasien Data : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation)
        End Try
    End Sub
    Private Function Fn_carpakai(ByVal kdcp As Integer) As String
        Try
            Fn_carpakai = ""

            Dim oConn_1 As New SqlConnection
            Dim oComm_1 As New SqlCommand
            Dim da_1 As SqlDataAdapter
            Dim ds_1 As New DataSet
            Dim SQL_1 As String

            oConn_1 = New SqlConnection(sConnOld)

            If oConn_1.State = ConnectionState.Closed Then
                oConn_1.Open()
            End If

            SQL_1 = "SELECT "
            SQL_1 &= "* "
            SQL_1 &= "FROM M_CARAPAKAI AS A "
            SQL_1 &= "WHERE A.KDCP = '" & kdcp & "' "

            oComm_1.Connection = oConn_1
            oComm_1.CommandText = SQL_1
            oComm_1.CommandTimeout = 120
            oComm_1.CommandType = CommandType.Text

            da_1 = New SqlDataAdapter(oComm_1)
            da_1.Fill(ds_1, "M_CARAPAKAI")

            If oConn_1.State = ConnectionState.Open Then
                oConn_1.Close()
            End If

            For xLoop As Integer = 0 To ds_1.Tables("M_CARAPAKAI").Rows.Count - 1
                With ds_1.Tables("M_CARAPAKAI")
                    Fn_carpakai = .Rows(xLoop)("DESCRIPTION")
                End With
            Next

        Catch ex As Exception
            Fn_carpakai = ""
        End Try
    End Function
    Private Function fn_LoadITEM(ByVal KDITEM As String) As String
        Try
            fn_LoadITEM = "RACIKAN"

            Dim oConn As New SqlConnection
            Dim oComm As New SqlCommand
            Dim da As SqlDataAdapter
            Dim ds As New DataSet
            Dim SQL As String
            oConn = New SqlConnection(sConnOld)

            If oConn.State = ConnectionState.Closed Then
                oConn.Open()
            End If

            SQL = "SELECT "
            SQL &= "* "
            SQL &= "FROM "
            SQL &= "M_ITEM A "
            SQL &= "WHERE "
            SQL &= "A.KDITEM = '" & KDITEM & "' "

            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "SEARCHITEM")

            For iLoop As Integer = 0 To ds.Tables("SEARCHITEM").Rows.Count - 1
                With ds.Tables("SEARCHITEM")
                    fn_LoadITEM = .Rows(iLoop)("NMITEM2")
                End With
            Next

            If oConn.State = ConnectionState.Open Then
                oConn.Close()
            End If
        Catch oErr As Exception
            fn_LoadITEM = ""
            MsgBox("Load Item" & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Function
    Private Function fn_LoadUOMKDUOM(ByVal KDITEM As String) As String
        Try
            fn_LoadUOMKDUOM = "-"

            Dim oConn As New SqlConnection
            Dim oComm As New SqlCommand
            Dim da As SqlDataAdapter
            Dim ds As New DataSet
            Dim SQL As String
            oConn = New SqlConnection(sConnOld)

            If oConn.State = ConnectionState.Closed Then
                oConn.Open()
            End If

            SQL = "SELECT "
            SQL &= "TOP 1 * "
            SQL &= "FROM "
            SQL &= "M_ITEM_UOM A "
            SQL &= "WHERE "
            SQL &= "A.KDITEM = '" & KDITEM & "' "
            SQL &= "AND A.RATE = 1 "

            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "SEARCHKDUOM")

            For iLoop As Integer = 0 To ds.Tables("SEARCHKDUOM").Rows.Count - 1
                With ds.Tables("SEARCHKDUOM")
                    fn_LoadUOMKDUOM = .Rows(iLoop)("KDUOM")
                End With
            Next

            If oConn.State = ConnectionState.Open Then
                oConn.Close()
            End If
        Catch oErr As Exception
            fn_LoadUOMKDUOM = ""
            MsgBox("Load Satuan" & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Function
    Private Function fn_LoadUOMKDUOMHARGA(ByVal KDITEM As String, ByVal kduom As String) As Decimal
        Try
            fn_LoadUOMKDUOMHARGA = 0

            Dim oConn As New SqlConnection
            Dim oComm As New SqlCommand
            Dim da As SqlDataAdapter
            Dim ds As New DataSet
            Dim SQL As String
            oConn = New SqlConnection(sConnOld)

            If oConn.State = ConnectionState.Closed Then
                oConn.Open()
            End If

            SQL = "SELECT "
            SQL &= "TOP 1 * "
            SQL &= "FROM "
            SQL &= "M_ITEM_UOM A "
            SQL &= "WHERE "
            SQL &= "A.KDITEM = '" & KDITEM & "' "
            SQL &= "AND A.KDUOM = '" & kduom & "' "
            SQL &= "AND A.RATE = 1 "

            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "SEARCHKDUOMHARGA")

            For iLoop As Integer = 0 To ds.Tables("SEARCHKDUOMHARGA").Rows.Count - 1
                With ds.Tables("SEARCHKDUOMHARGA")
                    'fn_LoadUOMKDUOMHARGA = .Rows(iLoop)("PRICESALESSTANDARD")
                    fn_LoadUOMKDUOMHARGA = .Rows(iLoop)("PRICEPURCHASESTANDARD")
                End With
            Next

            If oConn.State = ConnectionState.Open Then
                oConn.Close()
            End If
        Catch oErr As Exception
            fn_LoadUOMKDUOMHARGA = 0
            MsgBox("Load Satuan" & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Function
    Private Function fn_LoadUOMKDUOMMARGIN(ByVal KDITEM As String, ByVal kduom As String) As Decimal
        Try
            fn_LoadUOMKDUOMMARGIN = 0

            Dim oConn As New SqlConnection
            Dim oComm As New SqlCommand
            Dim da As SqlDataAdapter
            Dim ds As New DataSet
            Dim SQL As String
            oConn = New SqlConnection(sConnOld)

            If oConn.State = ConnectionState.Closed Then
                oConn.Open()
            End If

            SQL = "SELECT "
            SQL &= "TOP 1 * "
            SQL &= "FROM "
            SQL &= "M_ITEM_UOM A "
            SQL &= "WHERE "
            SQL &= "A.KDITEM = '" & KDITEM & "' "
            SQL &= "AND A.KDUOM = '" & kduom & "' "
            SQL &= "AND A.RATE = 1 "

            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "SEARCHKDUOMHARGA")

            For iLoop As Integer = 0 To ds.Tables("SEARCHKDUOMHARGA").Rows.Count - 1
                With ds.Tables("SEARCHKDUOMHARGA")
                    'fn_LoadUOMKDUOMHARGA = .Rows(iLoop)("PRICESALESSTANDARD")
                    fn_LoadUOMKDUOMMARGIN = .Rows(iLoop)("MARGIN")
                End With
            Next

            If oConn.State = ConnectionState.Open Then
                oConn.Close()
            End If
        Catch oErr As Exception
            fn_LoadUOMKDUOMMARGIN = 0
            MsgBox("Load Margin" & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Function
    Private Function fn_LoadUOMDESCRIPTION(ByVal KDUOM As String) As String
        Try
            fn_LoadUOMDESCRIPTION = ""

            Dim oConn As New SqlConnection
            Dim oComm As New SqlCommand
            Dim da As SqlDataAdapter
            Dim ds As New DataSet
            Dim SQL As String
            oConn = New SqlConnection(sConnOld)

            If oConn.State = ConnectionState.Closed Then
                oConn.Open()
            End If

            SQL = "SELECT "
            SQL &= "* "
            SQL &= "FROM "
            SQL &= "M_UOM A "
            SQL &= "WHERE "
            SQL &= "A.KDUOM = '" & KDUOM & "' "

            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "SEARCHUOM")

            For iLoop As Integer = 0 To ds.Tables("SEARCHUOM").Rows.Count - 1
                With ds.Tables("SEARCHUOM")
                    fn_LoadUOMDESCRIPTION = .Rows(iLoop)("DESCRIPTION")
                End With
            Next

            If oConn.State = ConnectionState.Open Then
                oConn.Close()
            End If
        Catch oErr As Exception
            fn_LoadUOMDESCRIPTION = ""
            MsgBox("Load Satuan" & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Function
    Private Function fn_LoadSIGNA(ByVal KDSIGNA As String) As String
        Try
            fn_LoadSIGNA = ""

            Dim oConn As New SqlConnection
            Dim oComm As New SqlCommand
            Dim da As SqlDataAdapter
            Dim ds As New DataSet
            Dim SQL As String
            oConn = New SqlConnection(sConnOld)

            If oConn.State = ConnectionState.Closed Then
                oConn.Open()
            End If

            SQL = "SELECT "
            SQL &= "* "
            SQL &= "FROM "
            SQL &= "M_SIGNA A "
            SQL &= "WHERE "
            SQL &= "A.KDSIGNA = '" & KDSIGNA & "' "

            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "SEARCHSIGNA")

            For iLoop As Integer = 0 To ds.Tables("SEARCHSIGNA").Rows.Count - 1
                With ds.Tables("SEARCHSIGNA")
                    fn_LoadSIGNA = .Rows(iLoop)("DESCRIPTION")
                End With
            Next

            If oConn.State = ConnectionState.Open Then
                oConn.Close()
            End If
        Catch oErr As Exception
            fn_LoadSIGNA = ""
            MsgBox("Load Signa" & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Function
    Private Function fn_LoadCARAPAKAI(ByVal KDCP As String) As String
        Try
            fn_LoadCARAPAKAI = ""

            Dim oConn As New SqlConnection
            Dim oComm As New SqlCommand
            Dim da As SqlDataAdapter
            Dim ds As New DataSet
            Dim SQL As String
            oConn = New SqlConnection(sConnOld)

            If oConn.State = ConnectionState.Closed Then
                oConn.Open()
            End If

            SQL = "SELECT "
            SQL &= "* "
            SQL &= "FROM "
            SQL &= "M_CARAPAKAI A "
            SQL &= "WHERE "
            SQL &= "A.KDCP = '" & KDCP & "' "

            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "SEARCHCARAPAKAI")

            For iLoop As Integer = 0 To ds.Tables("SEARCHCARAPAKAI").Rows.Count - 1
                With ds.Tables("SEARCHCARAPAKAI")
                    fn_LoadCARAPAKAI = .Rows(iLoop)("DESCRIPTION")
                End With
            Next

            If oConn.State = ConnectionState.Open Then
                oConn.Close()
            End If
        Catch oErr As Exception
            fn_LoadCARAPAKAI = ""
            MsgBox("Load Cara Pakai" & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Function
    Private Sub fn_LoadData(ByVal sNoid As String)
        Try
            Dim oReq_Recipe As New Transaksi.clsReq_Recipe
            Dim oKoding As New Admission.clsKoding
            Dim oReqAkhirPemeriksaan As New Transaksi.clsReqAkhirPemeriksaan

            ' ***** HEADER *****
            Dim ds = oReq_Recipe.GetData(sNoid)

            With ds
                txtDIAGNOOSA.Text = .DIAGNOSA
                deDATE.DateTime = .DATE

                Dim dsKoding = oKoding.GetData(sNoid)
                If dsKoding IsNot Nothing Then
                    Try
                        txtTINDAKLANJUT.Text = dsKoding.DESCRIPTION
                        'txtTINDAKLANJUT.Text = txtTINDAKLANJUT.Rtf
                    Catch ex As Exception

                    End Try
                End If

                txtGRANDTOTAL.Text = .GRANDTOTAL

                BindingSource.DataSource = oReq_Recipe.GetDataDetail(sNoid).Where(Function(x) x.KDITEM <> "").OrderBy(Function(x) x.SEQ).ToList()
                grdDetailResep.DataSource = BindingSource

                BindingSource1.DataSource = oReq_Recipe.GetDataDetailDiagnosaPenyerta(sNoid).OrderBy(Function(x) x.SEQ).ToList()
                grdDiagnosaPenyerta.DataSource = BindingSource1

                BindingSource2.DataSource = oReq_Recipe.GetDataDetailTindakan(sNoid).Where(Function(x) x.TINDAKAN <> "").OrderBy(Function(x) x.SEQ).ToList()
                grdTindakan.DataSource = BindingSource2

                BindingSource3.DataSource = oReq_Recipe.GetDataDetailTindakan_(sNoid).Where(Function(x) x.KETERANGAN <> "").OrderBy(Function(x) x.SEQ).ToList()
                grdTindakanPoli.DataSource = BindingSource3

                BindingSource4.DataSource = oReq_Recipe.GetDataDetailRacikan(sNoid).OrderBy(Function(x) x.SEQ).ToList()
                grdOBATRACIKAN.DataSource = BindingSource4
            End With

            Dim dsAkhir = oReqAkhirPemeriksaan.GetData(sNoid)
            If dsAkhir IsNot Nothing Then
                With dsAkhir
                    chkALERGI_YA.Checked = .ALERGI_YA
                    chkALERGI_TIDAK.Checked = .ALERGI_TIDAK
                    txtALERGIOBAT.Text = .ALERGI_TEXT
                    txtNAMAPASIEN.Text = .NAMAPASIEN
                    deDATETANGGALLAHIR.DateTime = .TANGGALLAHIR
                    txtKDCUSTOMER.Text = .KDCUSTOMER
                    txtSUBJEKTIF.Text = .SUBJEKTIF
                    txtOBJEKTIF_JENISKELAMIN.Text = .OBJEKTIF_JENISKELAMIN
                    txtOBJEKTIF_UMUR.Text = .OBJEKTIF_UMUR
                    txtOBJEKTIF_BERATBADAN.Text = IIf(CDec(.OBJEKTIF_BERATBADAN) = 0, "", .OBJEKTIF_BERATBADAN)
                    txtOBJEKTIF_TINGGIBADAN.Text = IIf(CDec(.OBJEKTIF_TINGGIBADAN) = 0, "", .OBJEKTIF_TINGGIBADAN)
                    txtOBJEKTIF_TEKANANDARAH.Text = .OBJEKTIF_TEKANANDARAH
                    txtOBJEKTIF_NADI.Text = .OBJEKTIF_NADI
                    txtOBJEKTIF_RESPIRASI.Text = .OBJEKTIF_RESPIRASI
                    txtOBJEKTIF_SATURASIOKSIGEN.Text = .OBJEKTIF_SATURASIOKSIGEN
                    txtOBJEKTIF_SUHU.Text = .OBJEKTIF_SUHU
                    txtDESKRIPSI.Text = .DESKRIPSI
                    txtALAMATGAMBAR.Text = .ALAMATGAMBAR

                    If .ALAMATGAMBAR <> "" Then
                        'picGAMBAR2.Image = Image.FromFile(txtALAMATGAMBAR.Text)

                        Dim img As System.Drawing.Image = System.Drawing.Image.FromFile(txtALAMATGAMBAR.Text)
                        picGAMBAR2.Image = img
                        sPicture = picGAMBAR2.Image
                    Else
                        'picGAMBAR2.Image = CType(My.Resources.ResourceManager.GetObject("gambar"), Image)
                        If grdKDDEPARTMENT.EditValue = "1058" Then
                            If txtOBJEKTIF_JENISKELAMIN.Text = "Laki-laki" Then
                                picGAMBAR2.Image = My.Resources.ResourceManager.GetObject("gambarwajah")
                            Else
                                picGAMBAR2.Image = My.Resources.ResourceManager.GetObject("gambarwajahlakilaki")
                            End If
                        Else
                            picGAMBAR2.Image = My.Resources.ResourceManager.GetObject("gambar")
                        End If
                        sPicture = Nothing
                    End If
                End With

                Dim dsTambahan = oReq_Recipe.GetDataTambahan(sNoid)
                If dsTambahan IsNot Nothing Then
                    txtOBJEKTIF_KESADARAN.Text = dsTambahan.TAMBAH1
                    txtOBJEKTIF_IMT.Text = dsTambahan.TAMBAH2
                    txtOBJEKTIF_BBIDEAL.Text = dsTambahan.TAMBAH3
                    txtOBJEKTIF_BBDITURUNKAN.Text = dsTambahan.TAMBAH4
                    txtGoalOfTreatment.Text = dsTambahan.TAMBAH5
                    txtEdukasi.Text = dsTambahan.TAMBAH6
                    txtFrekuensi.Text = dsTambahan.TAMBAH7
                End If
            Else
                deDATETANGGALLAHIR.DateTime = deDATETANGGALLAHIR.DateTime
            End If

        Catch oErr As Exception
            MsgBox("Load Data Data : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Function fn_Save(ByVal sNoid As String) As Boolean
        Try
            Dim sTindakan As Integer = 0
            Dim sTerapi As Integer = 0
            Dim oKoding As New Admission.clsKoding
            Dim oReq_Recipe As New Transaksi.clsReq_Recipe

            ' ***** HEADER *****
            Dim ds = oReq_Recipe.GetStructureHeader
            With ds
                Try
                    .DATECREATED = oReq_Recipe.GetData(sNoid).DATECREATED
                Catch ex As Exception
                    .DATECREATED = Now
                End Try
                .DATEUPDATED = Now
                .DATE = deTANGGALDATANG.DateTime
                .KDREQRECIPE = sNoid
                Try
                    .NOANTRIAN = oReq_Recipe.GetData(sNoid).NOANTRIAN
                Catch ex As Exception
                    .NOANTRIAN = String.Empty
                End Try
                .KDPENDAFTARAN = txtKDPENDAFTARAN.Text
                .KDWAREHOUSE = 1
                .ALERGIOBAT = IIf(txtALERGIOBAT.Text.ToString.Trim = String.Empty, "-", txtALERGIOBAT.Text)
                .BERATBADAN = txtOBJEKTIF_BERATBADAN.Text
                Try
                    .DESCRIPTION = oReq_Recipe.GetData(sNoid).DESCRIPTION
                Catch ex As Exception
                    .DESCRIPTION = ""
                End Try
                Try
                    .ISCHEKED = oReq_Recipe.GetData(sNoid).ISCHEKED
                Catch ex As Exception
                    .ISCHEKED = False
                End Try
                Try
                    .KONFIRMASIRESEP = oReq_Recipe.GetData(sNoid).KONFIRMASIRESEP
                Catch ex As Exception
                    .KONFIRMASIRESEP = "Resep Belum Diterima"
                End Try
                .SUBTOTAL_RINCIAN = CDec(0)
                .SUBTOTAL_PAKET = CDec(0)
                .SUBTOTAL_KRONIS = CDec(0)

                Calculate()

                .GRANDTOTAL = CDec(txtGRANDTOTAL.Text)
                .NOIDUSER = sUserID
                .ISPERUBAHANRESEP = False
                .ISAPPROVAL = False
                .NOIDUSER_FARMASI = String.Empty
                .DESCRIPTION_PERUBAHAN = String.Empty
                .PENJAMIN = txtPenjamin.Text
                .KDCUSTOMER = txtKDCUSTOMER.Text
                .PASIEN = txtNAMAPASIEN.Text
                .ALAMAT = ""
                .DOKTER = grdDPJP.Text
                .TUJUAN = grdKDDEPARTMENT.Text
                .DIAGNOSA = txtDIAGNOOSA.Text
                .SIPDOKTER = lblSIP.Text
                Try
                    .KDRECIPE = oReq_Recipe.GetData(sNoid).KDRECIPE
                Catch ex As Exception
                    .KDRECIPE = ""
                End Try

                
            End With

            Dim arrDetail = oReq_Recipe.GetStructureDetailList
            For i As Integer = 0 To grvDetailResep.RowCount - 2
                Dim dsDetail = oReq_Recipe.GetStructureDetail
                With dsDetail
                    sTerapi += 1

                    .SEQ = i
                    .KDREQRECIPE = ds.KDREQRECIPE
                    .NAMAOBAT = fn_LoadITEM(grvDetailResep.GetRowCellValue(i, colKDITEM))
                    .SATUAN = fn_LoadUOMDESCRIPTION(grvDetailResep.GetRowCellValue(i, colKDUOM))
                    .SIGNA = IIf(String.IsNullOrEmpty(grvDetailResep.GetRowCellValue(i, colKDSIGNA)), "", fn_LoadSIGNA(grvDetailResep.GetRowCellValue(i, colKDSIGNA)))
                    .CARAPAKAI = IIf(String.IsNullOrEmpty(grvDetailResep.GetRowCellValue(i, colKDCARAPAKAI)), "", fn_LoadCARAPAKAI(grvDetailResep.GetRowCellValue(i, colKDCARAPAKAI)))
                    .QTY = CDec(grvDetailResep.GetRowCellValue(i, colQTY))
                    .PRICE = CDec(grvDetailResep.GetRowCellValue(i, colPRICE))
                    .GRANDTOTAL = CDec(grvDetailResep.GetRowCellValue(i, colGRANDTOTAL))
                    .ROMAWI = IntegerToRoman(CInt(grvDetailResep.GetRowCellValue(i, colQTY)))
                    .REMARKS_DOKTER = IIf(String.IsNullOrEmpty(grvDetailResep.GetRowCellValue(i, colREMARKS_DOKTER)), "", grvDetailResep.GetRowCellValue(i, colREMARKS_DOKTER))
                    .KDITEM = grvDetailResep.GetRowCellValue(i, colKDITEM)
                    .KDUOM = grvDetailResep.GetRowCellValue(i, colKDUOM)
                    .KDSIGNA = IIf(String.IsNullOrEmpty(grvDetailResep.GetRowCellValue(i, colKDSIGNA)), "1286", grvDetailResep.GetRowCellValue(i, colKDSIGNA))
                    .KDCARAPAKAI = IIf(String.IsNullOrEmpty(grvDetailResep.GetRowCellValue(i, colKDCARAPAKAI)), "10", grvDetailResep.GetRowCellValue(i, colKDCARAPAKAI))
                    .QTY_PERUBAHAN = grvDetailResep.GetRowCellValue(i, colQTY)
                    .REMARKS_FARMASI = ""
                    .DOSIS = ""
                End With

                If grvDetailResep.GetRowCellValue(i, colKDITEM) IsNot Nothing Then
                    arrDetail.Add(dsDetail)
                End If
            Next

            Dim arrDetailObatRacikan = oKoding.GetStructureDetail_ObatRacikanList
            For i As Integer = 0 To grvOBATRACIKAN.RowCount - 2
                Dim dsDetail = oKoding.GetStructureDetail_ObatRacikan
                With dsDetail
                    sTerapi += 1

                    .KDREQRECIPE = ds.KDREQRECIPE
                    .SEQ = i
                    .KDITEM = grvOBATRACIKAN.GetRowCellValue(i, colKDITEMOBATRACIKAN)
                    .NAMAOBAT = fn_LoadITEM(grvOBATRACIKAN.GetRowCellValue(i, colKDITEMOBATRACIKAN))
                    .KDUOM = grvOBATRACIKAN.GetRowCellValue(i, colKDUOMOBATRACIKAN)
                    .SATUAN = fn_LoadUOMDESCRIPTION(grvOBATRACIKAN.GetRowCellValue(i, colKDUOMOBATRACIKAN))
                    .SIGNA = grvOBATRACIKAN.GetRowCellValue(i, colSIGNAOBATRACIKAN)
                    .PERMINTAAN = IIf(String.IsNullOrEmpty(grvOBATRACIKAN.GetRowCellValue(i, colPERMINTAANOBATRACIKAN)), "", grvOBATRACIKAN.GetRowCellValue(i, colPERMINTAANOBATRACIKAN))
                    .QTY = grvOBATRACIKAN.GetRowCellValue(i, colQTYOBATRACIKAN)
                    .REMARKS = grvOBATRACIKAN.GetRowCellValue(i, colREMARKSRACIKAN)
                End With
                arrDetailObatRacikan.Add(dsDetail)
            Next

            Dim dsTelaah = oReq_Recipe.GetStructureTelaah

            With dsTelaah
                .DATECREATED = Now
                .DATEUPDATED = Now
                .KDREQRECIPE = ds.KDREQRECIPE
                .TELAAH_01 = False
                .TELAAH_02 = False
                .TELAAH_03 = False
                .TELAAH_04 = False
                .TELAAH_05 = False
                .TELAAH_06 = False
                .TELAAH_07 = False
                .TELAAH_08 = False
                .TELAAH_09 = False
                .TELAAH_10 = False
                .TELAAH_11 = False
                .TELAAH_12 = False
                .TELAAH_13 = False
                .TELAAH_14 = False
                .TELAAH_15 = False
                .TELAAH_16 = False
                .TELAAH_17 = False
                .TELAAH_18 = False
                .TELAAH_19 = False
                .TELAAH_20 = False
                .TELAAH_21 = False
                .TELAAH_22 = False
                .TELAAH_23 = False
                .TELAAH_24 = False
                .NOIDUSER = ""
                .REMARKS = ""
            End With

            Dim dsTelaahObat1 = oReq_Recipe.GetStructureTelaahObat1

            With dsTelaahObat1
                .DATECREATED = Now
                .DATEUPDATED = Now
                .TELAAH_01 = False
                .TELAAH_02 = False
                .TELAAH_03 = False
                .TELAAH_04 = False
                .TELAAH_05 = False
                .NOIDUSER = ""
                .REMARKS = ""
            End With

            Dim dsTelaahObat2 = oReq_Recipe.GetStructureTelaahObat2

            With dsTelaahObat2
                .DATECREATED = Now
                .DATEUPDATED = Now
                .TELAAH_01 = False
                .TELAAH_02 = False
                .TELAAH_03 = False
                .TELAAH_04 = False
                .TELAAH_05 = False
                .NOIDUSER = ""
                .REMARKS = ""
            End With

            ' ***** KODING *****

            Dim dsKoding = oKoding.GetStructureHeader
            With dsKoding
                Try
                    .DATECREATED = oKoding.GetData(sNoid).DATECREATED
                Catch ex As Exception
                    .DATECREATED = Now
                End Try
                .DATEUPDATED = Now
                .KDKODING = sNoid
                .KDPENDAFTARAN = txtKDPENDAFTARAN.Text.Trim.ToUpper
                .DATE = deTANGGALDATANG.DateTime
                ''new Font("Tahoma", 12, FontStyle.Bold)
                ''txtTINDAKLANJUT.Font = New Font("Times New Roman", 9, FontStyle.Regular)
                'txtTINDAKLANJUT.Text = txtTINDAKLANJUT.Text
                .DESCRIPTION = txtTINDAKLANJUT.Text
                .NOIDUSER = sUserID
                .DOKTER = grdDPJP.Text
                .TUJUAN = grdKDDEPARTMENT.Text
                .KDCUSTOMER = txtKDCUSTOMER.Text
                .NAMAPASIEN = txtNAMAPASIEN.Text
                .KARTUBPJS = txtKARTUBPJS.Text
                .TANGGALLAHIR = deDATETANGGALLAHIR.DateTime
                .KDDOCTOR = grdDPJP.EditValue
            End With

            Dim arrDetailDiagnosaPenyerta = oKoding.GetStructureDetail_DianosisPenyertaList
            For i As Integer = 0 To grvDiagnosaPenyerta.RowCount - 2
                Dim dsDetail = oKoding.GetStructureDetail_DianosisPenyerta
                With dsDetail
                    .SEQ = i
                    .KDKODING = ds.KDREQRECIPE
                    .KDDIAGNOSA = "-"
                    .KETERANGAN = grvDiagnosaPenyerta.GetRowCellValue(i, colKETERANGAN)
                End With
                arrDetailDiagnosaPenyerta.Add(dsDetail)
            Next

            For i As Integer = 0 To grvTindakan.RowCount - 2
                sTindakan += 1
            Next

            Dim arrDetailDiagnosaPenyertaTindakan = oKoding.GetStructureDetail_Terapi_TindakanList

            If sTerapi >= sTindakan Then
                Dim iSeq As Integer = 0

                For i As Integer = 0 To grvDetailResep.RowCount - 2
                    iSeq = i
                    Dim dsDetail = oKoding.GetStructureDetail_Terapi_Tindakan
                    With dsDetail
                        Dim TINDAKAN As String = String.Empty

                        .SEQ = i
                        .KDKODING = ds.KDREQRECIPE
                        .TERAPI = fn_LoadITEM(grvDetailResep.GetRowCellValue(i, colKDITEM)) & " " & grvDetailResep.GetRowCellValue(i, colREMARKS_DOKTER)
                        For y As Integer = 0 To grvTindakan.RowCount - 2
                            If iSeq = y Then
                                TINDAKAN = grvTindakan.GetRowCellValue(i, colTINDAKAN)
                                Exit For
                            End If
                        Next
                        .TINDAKAN = TINDAKAN
                    End With

                    arrDetailDiagnosaPenyertaTindakan.Add(dsDetail)
                Next

                If arrDetailDiagnosaPenyertaTindakan.Count > 0 Then
                    iSeq += 1
                End If

                For i As Integer = 0 To grvOBATRACIKAN.RowCount - 2
                    Dim dsDetail = oKoding.GetStructureDetail_Terapi_Tindakan
                    With dsDetail
                        Dim TINDAKAN As String = String.Empty

                        .SEQ = iSeq
                        .KDKODING = ds.KDREQRECIPE
                        .TERAPI = fn_LoadITEM(grvOBATRACIKAN.GetRowCellValue(i, colKDITEMOBATRACIKAN)) & " " & grvOBATRACIKAN.GetRowCellValue(i, colREMARKSRACIKAN)

                        For y As Integer = 0 To grvTindakan.RowCount - 2
                            If iSeq = y Then
                                TINDAKAN = grvTindakan.GetRowCellValue(i, colTINDAKAN)
                                Exit For
                            End If
                        Next
                        .TINDAKAN = TINDAKAN
                    End With

                    iSeq += 1

                    arrDetailDiagnosaPenyertaTindakan.Add(dsDetail)
                Next
            Else
                Dim listTranskasi As New List(Of DataAccess.S_KODING_TERAPI_DIAGNOSISPENYERTA)
                Dim iSeq As Integer = 0

                For i As Integer = 0 To grvDetailResep.RowCount - 2
                    Dim dsRekap1 As New DataAccess.S_KODING_TERAPI_DIAGNOSISPENYERTA

                    dsRekap1.SEQ = i
                    dsRekap1.KDKODING = ""
                    dsRekap1.TERAPI = fn_LoadITEM(grvDetailResep.GetRowCellValue(i, colKDITEM)) & " " & grvDetailResep.GetRowCellValue(i, colREMARKS_DOKTER)
                    dsRekap1.TINDAKAN = ""

                    listTranskasi.Add(dsRekap1)

                    iSeq = i
                Next

                If listTranskasi.Count > 0 Then
                    iSeq += 1
                End If

                For i As Integer = 0 To grvOBATRACIKAN.RowCount - 2
                    Dim dsRekap1 As New DataAccess.S_KODING_TERAPI_DIAGNOSISPENYERTA

                    dsRekap1.SEQ = iSeq
                    dsRekap1.KDKODING = ""
                    dsRekap1.TERAPI = fn_LoadITEM(grvOBATRACIKAN.GetRowCellValue(i, colKDITEMOBATRACIKAN)) & " " & grvOBATRACIKAN.GetRowCellValue(i, colREMARKSRACIKAN)
                    dsRekap1.TINDAKAN = ""

                    listTranskasi.Add(dsRekap1)

                    iSeq += 1
                Next


                For i As Integer = 0 To grvTindakan.RowCount - 2
                    Dim dsDetail = oKoding.GetStructureDetail_Terapi_Tindakan
                    With dsDetail
                        Dim TERAPI As String = String.Empty

                        .SEQ = i
                        .KDKODING = ds.KDREQRECIPE

                        For Each xloopT In listTranskasi
                            If i = xloopT.SEQ Then
                                TERAPI = xloopT.TERAPI
                                Exit For
                            End If
                        Next
                        'For y As Integer = 0 To grvDetailResep.RowCount - 2
                        '    If i = y Then
                        '        TERAPI = fn_LoadITEM(grvDetailResep.GetRowCellValue(i, colKDITEM)) & " " & grvDetailResep.GetRowCellValue(i, colREMARKS_DOKTER)
                        '        Exit For
                        '    End If
                        'Next
                        .TERAPI = TERAPI
                        .TINDAKAN = grvTindakan.GetRowCellValue(i, colTINDAKAN)
                    End With
                    arrDetailDiagnosaPenyertaTindakan.Add(dsDetail)
                Next
            End If

            Dim arrDetailTindakan = oKoding.GetStructureDetail_TindakanList
            For i As Integer = 0 To grvTindakanPoli.RowCount - 2
                Dim dsDetail = oKoding.GetStructureDetail_Tindakan
                With dsDetail
                    .SEQ = i
                    .KDKODING = ds.KDREQRECIPE
                    .KETERANGAN = grvTindakanPoli.GetRowCellValue(i, colKETERANGAN_)
                End With
                arrDetailTindakan.Add(dsDetail)
            Next

            Dim oReqAkhirPemeriksaan As New Transaksi.clsReqAkhirPemeriksaan

            Dim dsPemeriksaan = oReqAkhirPemeriksaan.GetStructureHeader
            With dsPemeriksaan
                Try
                    .DATECREATED = oReqAkhirPemeriksaan.GetData(sNoid).DATECREATED
                Catch oErr As Exception
                    .DATECREATED = Now
                End Try
                .DATEUPDATED = Now
                .DATE = deTANGGALDATANG.DateTime
                .KDAKHIRPEMERIKSAAN = sNoid
                .KDPENDAFTARAN = txtKDPENDAFTARAN.Text
                .ALERGI_YA = chkALERGI_YA.Checked
                .ALERGI_TIDAK = chkALERGI_TIDAK.Checked
                .ALERGI_TEXT = txtALERGIOBAT.Text
                .NAMAPASIEN = txtNAMAPASIEN.Text
                .TANGGALLAHIR = deDATETANGGALLAHIR.DateTime
                .KDCUSTOMER = txtKDCUSTOMER.Text
                .SUBJEKTIF = txtSUBJEKTIF.Text
                .OBJEKTIF_JENISKELAMIN = txtOBJEKTIF_JENISKELAMIN.Text
                .OBJEKTIF_UMUR = txtOBJEKTIF_UMUR.Text
                .OBJEKTIF_TINGGIBADAN = IIf(txtOBJEKTIF_TINGGIBADAN.Text = "", 0, txtOBJEKTIF_TINGGIBADAN.Text.Replace(",", "."))
                .OBJEKTIF_BERATBADAN = IIf(txtOBJEKTIF_BERATBADAN.Text = "", 0, txtOBJEKTIF_BERATBADAN.Text.Replace(",", "."))
                .OBJEKTIF_TEKANANDARAH = txtOBJEKTIF_TEKANANDARAH.Text
                .OBJEKTIF_NADI = txtOBJEKTIF_NADI.Text
                .OBJEKTIF_RESPIRASI = txtOBJEKTIF_RESPIRASI.Text
                .OBJEKTIF_SATURASIOKSIGEN = txtOBJEKTIF_SATURASIOKSIGEN.Text
                .OBJEKTIF_SUHU = txtOBJEKTIF_SUHU.Text
                .KDUSER = sUserID
                .DESKRIPSI = txtDESKRIPSI.Text
                If sPicture Is Nothing Then
                    Try
                        .ALAMATGAMBAR = oReqAkhirPemeriksaan.GetData(sNoid).ALAMATGAMBAR
                    Catch ex As Exception
                        .ALAMATGAMBAR = ""
                    End Try
                Else
                    Dim oReqAwalPemeriksaan As New Transaksi.clsReqAwalPemeriksaan
                    Dim sALAMATSIMPAN As String = String.Empty
                    Dim dsAlamat = oReqAwalPemeriksaan.GetDataAlamatSimpan()
                    If dsAlamat IsNot Nothing Then
                        sALAMATSIMPAN = dsAlamat.ALAMAT_SERVER
                    End If

                    Dim Alamat As String = sALAMATSIMPAN & Now.ToString("ddMMyyyyHHmm") & "." & txtKDPENDAFTARAN.Text & ".jpg"
                    picGAMBAR2.Image.Save(Alamat, System.Drawing.Imaging.ImageFormat.Jpeg)
                    .ALAMATGAMBAR = Alamat
                End If
            End With

            Dim dsTambahan = oReq_Recipe.GetStructureHeaderTambahan

            With dsTambahan
                .KDREQRECIPE = ds.KDREQRECIPE
                .TAMBAH1 = txtOBJEKTIF_KESADARAN.Text
                .TAMBAH2 = txtOBJEKTIF_IMT.Text
                .TAMBAH3 = txtOBJEKTIF_BBIDEAL.Text
                .TAMBAH4 = txtOBJEKTIF_BBDITURUNKAN.Text
                .TAMBAH5 = txtGoalOfTreatment.Text
                .TAMBAH6 = txtEdukasi.Text
                .TAMBAH7 = txtFrekuensi.Text
                .TAMBAH8 = ""
                .TAMBAH9 = ""
                .TAMBAH10 = ""
            End With

            If oFormMode = FORM_MODE.FORM_MODE_ADD Then
                Dim KDREQRECIPE As String = oReq_Recipe.InsertData(IIf(sKDDEPARTMENT = "1058", True, False), ds, arrDetail, dsTelaah, dsTelaahObat1, dsTelaahObat2, dsKoding, arrDetailDiagnosaPenyerta, arrDetailDiagnosaPenyertaTindakan, arrDetailTindakan, dsPemeriksaan, sConnOld, arrDetailObatRacikan, dsTambahan)

                If KDREQRECIPE <> "" Then
                    fn_Save = True
                Else
                    fn_Save = False
                End If
            ElseIf oFormMode = FORM_MODE.FORM_MODE_EDIT Then
                fn_Save = oReq_Recipe.UpdateData(IIf(sKDDEPARTMENT = "1058", True, False), ds, arrDetail, dsKoding, arrDetailDiagnosaPenyerta, arrDetailDiagnosaPenyertaTindakan, arrDetailTindakan, dsPemeriksaan, arrDetailObatRacikan, dsTambahan)

                If txtTINDAKLANJUT.Text.Contains("PENUNJANG HARI INI") Then
                    Dim oDaftarPenunjangHariIni As New Admission.clsPendafataranPenunjangHariIni

                    Dim dsPenunjangHariIni = oDaftarPenunjangHariIni.GetStructureHeader

                    With dsPenunjangHariIni
                        .KDREG = txtKDPENDAFTARAN.Text
                        .KET_1 = sUserID
                        .KET_3 = Now.ToString("dd-MM-yyyy HH:mm:ss")
                    End With

                    Dim dsCek = oDaftarPenunjangHariIni.GetData(txtKDPENDAFTARAN.Text)
                    If dsCek Is Nothing Then
                        oDaftarPenunjangHariIni.InsertData(dsPenunjangHariIni)
                    Else
                        oDaftarPenunjangHariIni.UpdateData(dsPenunjangHariIni)
                    End If
                End If
            End If

            fn_LoadUpdateSudah(txtKDPENDAFTARAN.Text)
            fn_LoadDataUpdateAntian()

        Catch oErr As Exception
            MsgBox("Simpan Data Resep : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            fn_Save = False
        End Try
    End Function
#Region "Grid Method"
    Private Sub OnValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles grvDetailResep.FocusedRowChanged
        If isLoad Then
            Calculate()
        End If
    End Sub
    Private Sub Calculate()
        Dim sSubTotal = 0
        For i As Integer = 0 To grvDetailResep.RowCount - 2
            sSubTotal += CDec(grvDetailResep.GetRowCellValue(i, colGRANDTOTAL))
        Next

        txtGRANDTOTAL.Text = sSubTotal
    End Sub
    Private Sub grvDiagnosaPenyerta_RowStyle(sender As System.Object, e As DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs) Handles grvDiagnosaPenyerta.RowStyle
        If grvDiagnosaPenyerta.IsFilterRow(e.RowHandle) Then Exit Sub
        e.Appearance.BackColor = Color.GreenYellow
    End Sub
    Private Sub grvTindakanPoli_RowStyle(sender As System.Object, e As DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs) Handles grvTindakanPoli.RowStyle
        If grvTindakanPoli.IsFilterRow(e.RowHandle) Then Exit Sub
        If grvTindakanPoli.GetRowCellValue(e.RowHandle, "KETERANGAN") <> "" Then
            e.Appearance.BackColor = Color.GreenYellow
        End If
    End Sub
    Private Sub grvTindakan_RowStyle(sender As System.Object, e As DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs) Handles grvTindakan.RowStyle
        If grvTindakan.IsFilterRow(e.RowHandle) Then Exit Sub
        If grvTindakan.GetRowCellValue(e.RowHandle, "TINDAKAN") <> "" Then
            e.Appearance.BackColor = Color.GreenYellow
        End If
    End Sub
    Private Sub grvDetailResep_RowStyle(sender As System.Object, e As DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs) Handles grvDetailResep.RowStyle
        If grvTindakanPoli.IsFilterRow(e.RowHandle) Then Exit Sub
        e.Appearance.BackColor = Color.GreenYellow
    End Sub
    Private Sub grvDetailResep_CellValueChanged(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs) Handles grvDetailResep.CellValueChanged
        If e.Column.Name = colKDITEM.Name Then
            Try
                If grvDetailResep.GetFocusedRowCellValue(colKDITEM) IsNot Nothing Then
                    grvDetailResep.SetFocusedRowCellValue(colKDUOM, fn_LoadUOMKDUOM(grvDetailResep.GetFocusedRowCellValue(colKDITEM)))
                    grvDetailResep.SetFocusedRowCellValue(colREMARKS_DOKTER, "")
                    grvDetailResep.SetFocusedRowCellValue(colQTY, 0)
                End If
            Catch oErr As Exception
                MsgBox("Load Detail : " & vbCrLf & oErr.Message, MsgBoxStyle.Critical, Me.Text)
            End Try
        ElseIf e.Column.Name = colREMARKS_DOKTER.Name Then
            'grvTindakan.Focus()
            'grvTindakan.AddNewRow()
            'grvTindakan.SetFocusedRowCellValue(colTERAPI, fn_LoadITEM(grvDetailResep.GetFocusedRowCellValue(colKDITEM)) & " " & fn_LoadSIGNA(grvDetailResep.GetFocusedRowCellValue(colKDSIGNA)))
            'grvTindakan.SetFocusedRowCellValue(colTINDAKAN, "")
            'grvTindakan.UpdateCurrentRow()
        ElseIf e.Column.Name = colKDUOM.Name Then
            Dim hargajualawal As Decimal = fn_LoadUOMKDUOMHARGA(grvDetailResep.GetFocusedRowCellValue(colKDITEM), grvDetailResep.GetFocusedRowCellValue(colKDUOM))
            Dim margin As Decimal = fn_LoadUOMKDUOMMARGIN(grvDetailResep.GetFocusedRowCellValue(colKDITEM), grvDetailResep.GetFocusedRowCellValue(colKDUOM))

            Dim sPriceSales As Decimal = hargajualawal + (hargajualawal * (margin / 100))

            grvDetailResep.SetFocusedRowCellValue(colPRICE, sPriceSales)
        ElseIf e.Column.Name = colQTY.Name Or e.Column.Name = colPRICE.Name Then
            Dim sSubTotal As Decimal = CDec(grvDetailResep.GetFocusedRowCellValue(colQTY)) * CDec(grvDetailResep.GetFocusedRowCellValue(colPRICE))
            grvDetailResep.SetFocusedRowCellValue(colGRANDTOTAL, sSubTotal)
        End If
    End Sub
    Private Sub grvOBATRACIKAN_CellValueChanged(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs) Handles grvOBATRACIKAN.CellValueChanged
        If e.Column.Name = colKDITEMOBATRACIKAN.Name Then
            Try
                If grvOBATRACIKAN.GetFocusedRowCellValue(colKDITEMOBATRACIKAN) IsNot Nothing Then
                    Dim Tes = fn_LoadUOMKDUOM(grvOBATRACIKAN.GetFocusedRowCellValue(colKDITEMOBATRACIKAN))

                    grvOBATRACIKAN.SetFocusedRowCellValue(colKDUOMOBATRACIKAN, IIf(Tes = "-", 99, Tes))
                    'grvOBATRACIKAN.SetFocusedRowCellValue(colREMARKS_DOKTER, "")
                    grvOBATRACIKAN.SetFocusedRowCellValue(colQTY, 0)
                    grvOBATRACIKAN.SetFocusedRowCellValue(colREMARKSRACIKAN, "-")
                End If
            Catch oErr As Exception
                MsgBox("Load Detail : " & vbCrLf & oErr.Message, MsgBoxStyle.Critical, Me.Text)
            End Try
        End If
    End Sub
    Private Sub grdDPJP_EditValueChanged(sender As Object, e As EventArgs) Handles grdDPJP.EditValueChanged
        If grdDPJP.Text <> "" Then
            fn_LoadJadwalDokter(grdDPJP.EditValue)
        End If
    End Sub
    Private Sub SimpleButton3_Click(sender As Object, e As EventArgs)
        'picGAMBAR2.Image = CType(My.Resources.ResourceManager.GetObject("gambar"), Image)
        sPicture = Nothing
    End Sub
    Private Sub SimpleButton2_Click(sender As Object, e As EventArgs) Handles SimpleButton2.Click
        Dim frmPopUp_Image As New frmPopUp_img
        frmPopUp_Image.fn_LoadMe(picGAMBAR2.Image)
        frmPopUp_Image.ShowDialog()

        If sPicture IsNot Nothing Then
            picGAMBAR2.Image = sPicture
        End If

        picGAMBAR2.Focus()
    End Sub
    Private Sub cboPOLIMATA_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboPOLIMATA.SelectedIndexChanged
        If isLoad = True Then
            grvTindakanPoli.Focus()
            grvTindakanPoli.AddNewRow()
            grvTindakanPoli.SetFocusedRowCellValue(colKETERANGAN_, cboPOLIMATA.Text)
            grvTindakanPoli.UpdateCurrentRow()
        End If
    End Sub
    Private Sub SimpleButton5_Click(sender As Object, e As EventArgs) Handles SimpleButton5.Click
        grvTindakanPoli.Focus()
        grvTindakanPoli.AddNewRow()
        grvTindakanPoli.SetFocusedRowCellValue(colKETERANGAN_, cboPOLIMATA.Text)
        grvTindakanPoli.UpdateCurrentRow()
    End Sub
    Private Sub btnRiwayatPemberianResep_Click(sender As Object, e As EventArgs) Handles btnRiwayatPemberianResep.Click
        If txtKDCUSTOMER.Text <> "" Then
            Dim frmRiwayatResep As New frmRiwayatResep
            Try
                frmRiwayatResep.LoadMe(txtKDCUSTOMER.Text, 0)
                frmRiwayatResep.ShowDialog(Me)

                If sNoTransaksi <> String.Empty Then
                    Dim oReq_Recipe As New Transaksi.clsReq_Recipe

                    Dim dsTemplate = oReq_Recipe.GetDataDetail(sNoTransaksi)

                    For Each iLoop In dsTemplate.OrderBy(Function(x) x.SEQ)
                        grvDetailResep.Focus()
                        grvDetailResep.AddNewRow()

                        grvDetailResep.SetFocusedRowCellValue(colKDITEM, iLoop.KDITEM)
                        'grvDetailResep.SetFocusedRowCellValue(colKDUOM, iLoop.M_UOM.MEMO)
                        'grvDetailResep.SetFocusedRowCellValue(colKDSIGNA, iLoop.M_SIGNA.MEMO)
                        'grvDetailResep.SetFocusedRowCellValue(colKDCARAPAKAI, iLoop.M_CARAPAKAI.MEMO)
                        grvDetailResep.SetFocusedRowCellValue(colQTY, iLoop.QTY)
                        grvDetailResep.SetFocusedRowCellValue(colREMARKS_DOKTER, iLoop.REMARKS_DOKTER)

                        'grvDetailResep.SetFocusedRowCellValue(colKDITEM, iLoop.KDITEM)
                        grvDetailResep.SetFocusedRowCellValue(colKDUOM, iLoop.KDUOM)
                        grvDetailResep.SetFocusedRowCellValue(colKDSIGNA, iLoop.KDSIGNA)
                        grvDetailResep.SetFocusedRowCellValue(colKDCARAPAKAI, iLoop.KDCARAPAKAI)
                        'grvDetailResep.SetFocusedRowCellValue(colQTY_PERUBAHAN, iLoop.QTY)
                        'grvDetailResep.SetFocusedRowCellValue(colREMARKS_FARMASI, iLoop.REMARKS_FARMASI)
                        grvDetailResep.UpdateCurrentRow()
                    Next
                End If

            Catch ex As Exception
                MsgBox("Load Form Riwayat Resep : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
            End Try
        Else
            MsgBox("Nomor Rekam Medis Kosong!!!", MsgBoxStyle.Exclamation, Me.Text)
        End If
    End Sub
    Private Sub SimpleButton4_Click(sender As Object, e As EventArgs) Handles SimpleButton4.Click
        If txtKDCUSTOMER.Text <> "" Then
            Dim frmRiwayatResep As New frmRiwayatResep
            Try
                frmRiwayatResep.LoadMe(txtKDCUSTOMER.Text, 1)
                frmRiwayatResep.ShowDialog(Me)

                If sNoTransaksi <> String.Empty Then
                    Dim oConn_1 As New SqlConnection
                    Dim oComm_1 As New SqlCommand
                    Dim da_1 As SqlDataAdapter
                    Dim ds_1 As New DataSet
                    Dim SQL_1 As String

                    oConn_1 = New SqlConnection(sConnOld)

                    If oConn_1.State = ConnectionState.Closed Then
                        oConn_1.Open()
                    End If

                    SQL_1 = "SELECT "
                    SQL_1 &= "* "
                    SQL_1 &= "FROM S_RECIPE_D AS A "
                    SQL_1 &= "WHERE A.KDRECIPE = '" & sNoTransaksi & "' "


                    oComm_1.Connection = oConn_1
                    oComm_1.CommandText = SQL_1
                    oComm_1.CommandTimeout = 120
                    oComm_1.CommandType = CommandType.Text

                    da_1 = New SqlDataAdapter(oComm_1)
                    da_1.Fill(ds_1, "S_RECIPE_D")

                    If oConn_1.State = ConnectionState.Open Then
                        oConn_1.Close()
                    End If

                    For xLoop As Integer = 0 To ds_1.Tables("S_RECIPE_D").Rows.Count - 1
                        With ds_1.Tables("S_RECIPE_D")
                            grvDetailResep.Focus()
                            grvDetailResep.AddNewRow()

                            grvDetailResep.SetFocusedRowCellValue(colKDITEM, .Rows(xLoop)("KDITEM"))
                            grvDetailResep.SetFocusedRowCellValue(colQTY, .Rows(xLoop)("QTY"))

                            Dim Tes As String = "-"
                            Try
                                Tes = .Rows(xLoop)("DESCRIPTION")
                            Catch ex As Exception

                            End Try
                            grvDetailResep.SetFocusedRowCellValue(colREMARKS_DOKTER, Tes)
                            grvDetailResep.SetFocusedRowCellValue(colKDUOM, .Rows(xLoop)("KDUOM"))
                            grvDetailResep.SetFocusedRowCellValue(colKDSIGNA, FormatNumber(.Rows(xLoop)("SIGNA_1"), 0) & "x" & FormatNumber(.Rows(xLoop)("SIGNA_2"), 0))
                            grvDetailResep.SetFocusedRowCellValue(colKDCARAPAKAI, Fn_carpakai(.Rows(xLoop)("KDCP")))
                            grvDetailResep.UpdateCurrentRow()

                        End With
                    Next
                End If

            Catch ex As Exception
                MsgBox("Load Form Riwayat Resep : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
            End Try
        Else
            MsgBox("Nomor Rekam Medis Kosong!!!", MsgBoxStyle.Exclamation, Me.Text)
        End If
    End Sub
    Private Sub grdTEMPLATE_EditValueChanged(sender As Object, e As EventArgs) Handles grdTEMPLATE.EditValueChanged
        If isLoad = True Then
            If grdTEMPLATE.Text <> "" Then
                Dim oTemplate As New Reference.clsTemplateResep

                Dim dsTemplate = oTemplate.GetDataDetail(grdTEMPLATE.EditValue)

                For Each iLoop In dsTemplate.OrderBy(Function(x) x.SEQ)
                    'txtDIAGNOOSA.Text = iLoop.S_REQ_RECIPE_TEMPLATE_H.DESCRIPTION
                    grvDetailResep.Focus()
                    grvDetailResep.AddNewRow()
                    grvDetailResep.SetFocusedRowCellValue(colKDITEM, iLoop.KDITEM)
                    grvDetailResep.SetFocusedRowCellValue(colKDUOM, iLoop.KDUOM)
                    grvDetailResep.SetFocusedRowCellValue(colKDSIGNA, iLoop.KDSIGNA)
                    grvDetailResep.SetFocusedRowCellValue(colKDCARAPAKAI, iLoop.KDCARAPAKAI)
                    grvDetailResep.SetFocusedRowCellValue(colQTY, iLoop.QTY)
                    grvDetailResep.SetFocusedRowCellValue(colREMARKS_DOKTER, iLoop.REMARKS_DOKTER)
                    grvDetailResep.UpdateCurrentRow()


                    '.NAMAOBAT = fn_LoadITEM(grvDetailResep.GetRowCellValue(i, colKDITEM))
                    '.SATUAN = fn_LoadUOMDESCRIPTION(grvDetailResep.GetRowCellValue(i, colKDUOM))
                    '.SIGNA = IIf(String.IsNullOrEmpty(grvDetailResep.GetRowCellValue(i, colKDSIGNA)), "", fn_LoadSIGNA(grvDetailResep.GetRowCellValue(i, colKDSIGNA)))
                    '.CARAPAKAI = IIf(String.IsNullOrEmpty(grvDetailResep.GetRowCellValue(i, colKDCARAPAKAI)), "", fn_LoadCARAPAKAI(grvDetailResep.GetRowCellValue(i, colKDCARAPAKAI)))
                    '.QTY = CDec(grvDetailResep.GetRowCellValue(i, colQTY))
                    '.PRICE = CDec(grvDetailResep.GetRowCellValue(i, colPRICE))
                    '.GRANDTOTAL = CDec(grvDetailResep.GetRowCellValue(i, colGRANDTOTAL))
                    '.ROMAWI = IntegerToRoman(CInt(grvDetailResep.GetFocusedRowCellValue(colQTY)))
                    '.REMARKS_DOKTER = IIf(String.IsNullOrEmpty(grvDetailResep.GetRowCellValue(i, colREMARKS_DOKTER)), "", grvDetailResep.GetRowCellValue(i, colREMARKS_DOKTER))
                    '.KDITEM = grvDetailResep.GetRowCellValue(i, colKDITEM)
                    '.KDUOM = grvDetailResep.GetRowCellValue(i, colKDUOM)
                    '.KDSIGNA = IIf(String.IsNullOrEmpty(grvDetailResep.GetRowCellValue(i, colKDSIGNA)), "1286", grvDetailResep.GetRowCellValue(i, colKDSIGNA))
                    '.KDCARAPAKAI = IIf(String.IsNullOrEmpty(grvDetailResep.GetRowCellValue(i, colKDCARAPAKAI)), "10", grvDetailResep.GetRowCellValue(i, colKDCARAPAKAI))
                    '.QTY_PERUBAHAN = grvDetailResep.GetRowCellValue(i, colQTY)
                    '.REMARKS_FARMASI = ""
                Next
            End If
        End If
    End Sub
    Private Sub btnSKD_Click(sender As Object, e As EventArgs) Handles btnSKD.Click
        If txtKDPENDAFTARAN.Text = "" Then
            MsgBox("Register Kosong", MsgBoxStyle.Exclamation, Me.Text)
            Exit Sub
        End If
        'Dim oSKD As New Admission.clsSKD
        'Dim ds = oSKD.GetDataPendaftaranalasan(txtKDPENDAFTARAN.Text, "KONTROL")

        'If ds IsNot Nothing Then
        '    Dim frmSKD As New frmSKD
        '    Try
        '        frmSKD.fn_LoadKategori(0)
        '        frmSKD.LoadMe(FORM_MODE.FORM_MODE_EDIT, ds.KDSKD)
        '        frmSKD.ShowDialog(Me)
        '    Catch ex As Exception
        '        MsgBox("Load Form Detail : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
        '    Finally
        '        If Not frmSKD Is Nothing Then frmSKD.Dispose()
        '        frmSKD = Nothing
        '    End Try
        'Else
        '    Dim frmSKD As New frmSKD
        '    Try
        '        frmSKD.fn_LoadNoPendaftaran(txtKDPENDAFTARAN.Text, txtKDCUSTOMER.Text, grdKDDEPARTMENT.EditValue, grdDPJP.EditValue, txtNOMORSEP.Text, txtPenjamin.Text, txtNAMAPASIEN.Text)
        '        frmSKD.fn_LoadKategori(0)
        '        frmSKD.LoadMe(FORM_MODE.FORM_MODE_ADD)
        '        frmSKD.ShowDialog(Me)
        '    Catch ex As Exception
        '        MsgBox("Load Form Detail : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
        '    End Try
        'End If

        'Dim oSKD As New Admission.clsSKD
        'Dim ds = oSKD.GetDataPendaftaranalasan(txtKDPENDAFTARAN.Text, "KONTROL")

        Dim Kode As String = fn_LoadTindakLanjutAlasanCek(txtKDPENDAFTARAN.Text, "KONTROL")
        If Kode <> "" Then
            Dim frmSKD As New frmSKD
            Try
                frmSKD.fn_LoadNoPendaftaran(txtKDPENDAFTARAN.Text, txtKDCUSTOMER.Text, grdKDDEPARTMENT.EditValue, grdDPJP.EditValue, txtNOMORSEP.Text, txtPenjamin.Text, txtNAMAPASIEN.Text, txtDIAGNOOSA.Text)
                frmSKD.fn_LoadKategori(0)
                frmSKD.LoadMe(FORM_MODE.FORM_MODE_EDIT, Kode)
                frmSKD.ShowDialog(Me)
            Catch ex As Exception
                MsgBox("Load Form Detail : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
            Finally
                If Not frmSKD Is Nothing Then frmSKD.Dispose()
                frmSKD = Nothing
            End Try
        Else
            Dim frmSKD As New frmSKD
            Try
                frmSKD.fn_LoadNoPendaftaran(txtKDPENDAFTARAN.Text, txtKDCUSTOMER.Text, grdKDDEPARTMENT.EditValue, grdDPJP.EditValue, txtNOMORSEP.Text, txtPenjamin.Text, txtNAMAPASIEN.Text, txtDIAGNOOSA.Text)
                frmSKD.fn_LoadKategori(0)
                frmSKD.LoadMe(FORM_MODE.FORM_MODE_ADD)
                frmSKD.ShowDialog(Me)
            Catch ex As Exception
                MsgBox("Load Form Detail : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
            End Try
        End If

        fn_LoadTindakLanjut(txtKDPENDAFTARAN.Text)

    End Sub
    Private Function fn_LoadTindakLanjutAlasanCek(ByVal KDREG As String, ByVal ALASAN As String) As String
        fn_LoadTindakLanjutAlasanCek = ""

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
            SQL &= "FROM S_PENDAFTARAN_SKD WHERE KDPENDAFTARAN = '" & KDREG & "' AND ALASAN = '" & ALASAN & "'"

            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "S_PENDAFTARAN_SKD")

            If oConn.State = ConnectionState.Open Then
                oConn.Close()
            End If

            For iLoop As Integer = 0 To ds.Tables("S_PENDAFTARAN_SKD").Rows.Count - 1
                With ds.Tables("S_PENDAFTARAN_SKD")
                    fn_LoadTindakLanjutAlasanCek = .Rows(iLoop)("KDSKD").ToString()
                    sRemarks_Ruangan = .Rows(iLoop)("REQUEST").ToString()
                    sRemarks_RencanaPembedahan = .Rows(iLoop)("RESPONSE").ToString()
                    sRemarks_IntruksiDokter = .Rows(iLoop)("DESCRIPTION").ToString()
                End With
            Next

        Catch ex As Exception
            MsgBox("Tindak Lanjut : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Function
    Private Function fn_LoadTindakLanjutAlasan(ByVal KDREG As String, ByVal ALASAN As String) As String
        fn_LoadTindakLanjutAlasan = ""

        Try
            Dim oSkd As New Admission.clsSKD

            Dim ds = oSkd.GetDataPendaftaranalasanCek(KDREG, ALASAN)
            If ds IsNot Nothing Then
                fn_LoadTindakLanjutAlasan = ds.KDSKD
            End If
        Catch ex As Exception
            MsgBox("Tindak Lanjut : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Function
    Private Sub fn_LoadTindakLanjut(ByVal KDREG As String)
        Dim oSkd As New Admission.clsSKD
        txtTINDAKLANJUT.ResetText()

        Dim TindakLanjut As String = String.Empty

        For Each xloop In oSkd.GetDataByKdregList(KDREG)

            Dim dsSKD = oSkd.GetData(xloop.KDSKD)
            If dsSKD IsNot Nothing Then
                If dsSKD.ALASAN = "KONTROL" Then
                    Dim sTindakLanjut As String = "Tanggal Kontrol : " & dsSKD.DATEKONTROL.ToString("dd-MM-yyyy") & vbCrLf & "Alasan : " & dsSKD.ALASAN.ToString.Trim & vbCrLf & "Tindak Lanjut : " & dsSKD.TINDAKLANJUT.ToString.Trim & vbCrLf & "Rencana Pemeriksaan : " & dsSKD.DESCRIPTION.ToString.Trim & " " & dsSKD.ORDERPENUNJANG
                    txtTINDAKLANJUT.Text = IIf(txtTINDAKLANJUT.Text = "", "", txtTINDAKLANJUT.Text & vbCrLf & vbCrLf) & sTindakLanjut
                End If
                If dsSKD.ALASAN = "RUJUKAN EKSTERNAL" Then
                    Dim sTindakLanjut As String = "Tanggal Kontrol : " & dsSKD.DATEKONTROL.ToString("dd-MM-yyyy") & vbCrLf & "Alasan : " & dsSKD.ALASAN.ToString.Trim & vbCrLf & "Alasan di rujuk : " & dsSKD.DESCRIPTION.ToString.Trim
                    txtTINDAKLANJUT.Text = IIf(txtTINDAKLANJUT.Text = "", "", txtTINDAKLANJUT.Text & vbCrLf & vbCrLf) & sTindakLanjut
                End If
                If dsSKD.ALASAN = "RUJUKAN HABIS" Then
                    Dim sTindakLanjut As String = "Tanggal Kontrol : " & dsSKD.DATEKONTROL.ToString("dd-MM-yyyy") & vbCrLf & "Alasan : " & dsSKD.ALASAN.ToString.Trim & vbCrLf & "Tindak Lanjut : " & dsSKD.TINDAKLANJUT.ToString.Trim & vbCrLf & "Rencana Pemeriksaan : " & dsSKD.DESCRIPTION.ToString.Trim & " " & dsSKD.ORDERPENUNJANG
                    txtTINDAKLANJUT.Text = IIf(txtTINDAKLANJUT.Text = "", "", txtTINDAKLANJUT.Text & vbCrLf & vbCrLf) & sTindakLanjut
                End If
                If dsSKD.ALASAN = "KONSUL INTERNAL" Then
                    Dim sTindakLanjut As String = "Tanggal Kontrol : " & dsSKD.DATEKONTROL.ToString("dd-MM-yyyy") & vbCrLf & "Alasan : " & dsSKD.ALASAN.ToString.Trim & vbCrLf & "Tujuan Poli : " & dsSKD.M_DEPARTMENT.NAME_DISPLAY.ToString.Trim & vbCrLf & "Dokter yang di tuju: " & dsSKD.M_DOCTOR.NAME_DISPLAY.ToString.Trim & vbCrLf & "Alasan di konsul : " & dsSKD.DESCRIPTION.ToString.Trim
                    txtTINDAKLANJUT.Text = IIf(txtTINDAKLANJUT.Text = "", "", txtTINDAKLANJUT.Text & vbCrLf & vbCrLf) & sTindakLanjut
                End If
                If dsSKD.ALASAN = "PRB" Then
                    Dim sTindakLanjut As String = "Tanggal Kontrol : " & dsSKD.DATEKONTROL.ToString("dd-MM-yyyy") & vbCrLf & "Alasan : " & dsSKD.ALASAN.ToString.Trim & vbCrLf & "Alasan di rujuk : " & dsSKD.DESCRIPTION.ToString.Trim
                    txtTINDAKLANJUT.Text = IIf(txtTINDAKLANJUT.Text = "", "", txtTINDAKLANJUT.Text & vbCrLf & vbCrLf) & sTindakLanjut
                End If
                If dsSKD.ALASAN = "RUJUK BALIK" Then
                    Dim sTindakLanjut As String = "Tanggal Kontrol : " & dsSKD.DATEKONTROL.ToString("dd-MM-yyyy") & vbCrLf & "Alasan : " & dsSKD.ALASAN.ToString.Trim & vbCrLf & "Alasan di rujuk : " & dsSKD.DESCRIPTION.ToString.Trim
                    txtTINDAKLANJUT.Text = IIf(txtTINDAKLANJUT.Text = "", "", txtTINDAKLANJUT.Text & vbCrLf & vbCrLf) & sTindakLanjut
                End If
                If dsSKD.ALASAN = "SELESAI PENGOBATAN" Then
                    Dim sTindakLanjut As String = "Keterangan : " & dsSKD.ALASAN.ToString.Trim
                    txtTINDAKLANJUT.Text = IIf(txtTINDAKLANJUT.Text = "", "", txtTINDAKLANJUT.Text & vbCrLf & vbCrLf) & sTindakLanjut
                End If
                If dsSKD.ALASAN = "RAWAT INAP" Then
                    Dim sTindakLanjut As String = "Keterangan : " & dsSKD.ALASAN.ToString.Trim & vbCrLf & sRemarks_IntruksiDokter
                    txtTINDAKLANJUT.Text = IIf(txtTINDAKLANJUT.Text = "", "", txtTINDAKLANJUT.Text & vbCrLf & vbCrLf) & sTindakLanjut
                End If
                If dsSKD.ALASAN = "PENJADWALAN OPERASI" Then
                    Dim sTindakLanjut As String = "Keterangan : " & dsSKD.ALASAN.ToString.Trim
                    txtTINDAKLANJUT.Text = IIf(txtTINDAKLANJUT.Text = "", "", txtTINDAKLANJUT.Text & vbCrLf & vbCrLf) & sTindakLanjut
                End If
                If dsSKD.ALASAN = "PENUNJANG HARI INI" Then
                    Dim sTindakLanjut As String = "Keterangan : " & dsSKD.ALASAN.ToString.Trim
                    txtTINDAKLANJUT.Text = IIf(txtTINDAKLANJUT.Text = "", "", txtTINDAKLANJUT.Text & vbCrLf & vbCrLf) & sTindakLanjut
                End If
                If dsSKD.ALASAN = "ALIH RAWAT" Then
                    Dim sTindakLanjut As String = "Tanggal Kontrol : " & dsSKD.DATEKONTROL.ToString("dd-MM-yyyy") & vbCrLf & "Alasan : " & dsSKD.ALASAN.ToString.Trim & vbCrLf & "Tujuan Poli : " & dsSKD.M_DEPARTMENT.NAME_DISPLAY.ToString.Trim & vbCrLf & "Dokter yang di tuju: " & dsSKD.M_DOCTOR.NAME_DISPLAY.ToString.Trim & vbCrLf & "Alasan di rujuk : " & dsSKD.DESCRIPTION.ToString.Trim
                    txtTINDAKLANJUT.Text = IIf(txtTINDAKLANJUT.Text = "", "", txtTINDAKLANJUT.Text & vbCrLf & vbCrLf) & sTindakLanjut
                End If
                If dsSKD.ALASAN = "ITERASI1" Then
                    Dim sTindakLanjut As String = "Tanggal Iterasi Ke 1 : " & dsSKD.DATEKONTROL.ToString("dd-MM-yyyy") & vbCrLf & "Alasan : " & dsSKD.ALASAN.ToString.Trim & vbCrLf & "Asal Poli : " & dsSKD.M_DEPARTMENT.NAME_DISPLAY.ToString.Trim & vbCrLf & "Dokter Pemberi Iterasi: " & dsSKD.M_DOCTOR.NAME_DISPLAY.ToString.Trim & vbCrLf & "Alasan di Iterasi : " & dsSKD.DESCRIPTION.ToString.Trim
                    txtTINDAKLANJUT.Text = IIf(txtTINDAKLANJUT.Text = "", "", txtTINDAKLANJUT.Text & vbCrLf & vbCrLf) & sTindakLanjut
                End If
                If dsSKD.ALASAN = "ITERASI2" Then
                    Dim sTindakLanjut As String = "Tanggal Iterasi Ke 1 : " & dsSKD.DATEKONTROL.ToString("dd-MM-yyyy") & vbCrLf & "Tanggal Iterasi Ke 2 : " & dsSKD.DATEKONTROL_2.ToString("dd-MM-yyyy") & vbCrLf & "Alasan : " & dsSKD.ALASAN.ToString.Trim & vbCrLf & "Asal Poli : " & dsSKD.M_DEPARTMENT.NAME_DISPLAY.ToString.Trim & vbCrLf & "Dokter Pemberi Iterasi: " & dsSKD.M_DOCTOR.NAME_DISPLAY.ToString.Trim & vbCrLf & "Alasan di Iterasi : " & dsSKD.DESCRIPTION.ToString.Trim
                    txtTINDAKLANJUT.Text = IIf(txtTINDAKLANJUT.Text = "", "", txtTINDAKLANJUT.Text & vbCrLf & vbCrLf) & sTindakLanjut
                End If
            End If

            TindakLanjut = TindakLanjut & vbCrLf & txtTINDAKLANJUT.Text
        Next

    End Sub
    Private Sub btnEksternal_Click(sender As Object, e As EventArgs) Handles btnEksternal.Click
        If txtKDPENDAFTARAN.Text = "" Then
            MsgBox("Register Kosong", MsgBoxStyle.Exclamation, Me.Text)
            Exit Sub
        End If

        'Dim oSKD As New Admission.clsSKD
        'Dim ds = oSKD.GetDataPendaftaranalasan(txtKDPENDAFTARAN.Text, "RUJUK")

        'If ds IsNot Nothing Then
        '    Dim frmSKD As New frmSKD
        '    Try
        '        frmSKD.fn_LoadKategori(1)
        '        frmSKD.LoadMe(FORM_MODE.FORM_MODE_EDIT, ds.KDSKD)
        '        frmSKD.ShowDialog(Me)
        '    Catch ex As Exception
        '        MsgBox("Load Form Detail : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
        '    Finally
        '        If Not frmSKD Is Nothing Then frmSKD.Dispose()
        '        frmSKD = Nothing
        '    End Try
        'Else
        '    Dim frmSKD As New frmSKD
        '    Try
        '        frmSKD.fn_LoadNoPendaftaran(txtKDPENDAFTARAN.Text, txtKDCUSTOMER.Text, grdKDDEPARTMENT.EditValue, grdDPJP.EditValue, txtNOMORSEP.Text, txtPenjamin.Text, txtNAMAPASIEN.Text)
        '        frmSKD.fn_LoadKategori(1)
        '        frmSKD.LoadMe(FORM_MODE.FORM_MODE_ADD)
        '        frmSKD.ShowDialog(Me)
        '    Catch ex As Exception
        '        MsgBox("Load Form Detail : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
        '    End Try
        'End If

        Dim Kode As String = fn_LoadTindakLanjutAlasanCek(txtKDPENDAFTARAN.Text, "RUJUKAN EKSTERNAL")
        If Kode <> "" Then
            Dim frmSKD As New frmSKD
            Try
                frmSKD.fn_LoadKategori(1)
                frmSKD.LoadMe(FORM_MODE.FORM_MODE_EDIT, Kode)
                frmSKD.ShowDialog(Me)
            Catch ex As Exception
                MsgBox("Load Form Detail : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
            Finally
                If Not frmSKD Is Nothing Then frmSKD.Dispose()
                frmSKD = Nothing
            End Try
        Else
            Dim frmSKD As New frmSKD
            Try
                frmSKD.fn_LoadNoPendaftaran(txtKDPENDAFTARAN.Text, txtKDCUSTOMER.Text, grdKDDEPARTMENT.EditValue, grdDPJP.EditValue, txtNOMORSEP.Text, txtPenjamin.Text, txtNAMAPASIEN.Text, txtDIAGNOOSA.Text)
                frmSKD.fn_LoadKategori(1)
                frmSKD.LoadMe(FORM_MODE.FORM_MODE_ADD)
                frmSKD.ShowDialog(Me)
            Catch ex As Exception
                MsgBox("Load Form Detail : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
            End Try
        End If

        fn_LoadTindakLanjut(txtKDPENDAFTARAN.Text)

    End Sub
    Private Sub btnInternal_Click(sender As Object, e As EventArgs) Handles btnInternal.Click
        If txtKDPENDAFTARAN.Text = "" Then
            MsgBox("Register Kosong", MsgBoxStyle.Exclamation, Me.Text)
            Exit Sub
        End If

        'Dim oSKD As New Admission.clsSKD
        'Dim ds = oSKD.GetDataPendaftaranalasan(txtKDPENDAFTARAN.Text, "RUJUK INTERNAL")

        'If ds IsNot Nothing Then
        '    Dim frmSKD As New frmSKD
        '    Try
        '        frmSKD.fn_LoadKategori(3)
        '        frmSKD.LoadMe(FORM_MODE.FORM_MODE_EDIT, ds.KDSKD)
        '        frmSKD.ShowDialog(Me)
        '    Catch ex As Exception
        '        MsgBox("Load Form Detail : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
        '    Finally
        '        If Not frmSKD Is Nothing Then frmSKD.Dispose()
        '        frmSKD = Nothing
        '    End Try
        'Else
        '    Dim frmSKD As New frmSKD
        '    Try
        '        frmSKD.fn_LoadNoPendaftaran(txtKDPENDAFTARAN.Text, txtKDCUSTOMER.Text, grdKDDEPARTMENT.EditValue, grdDPJP.EditValue, txtNOMORSEP.Text, txtPenjamin.Text, txtNAMAPASIEN.Text)
        '        frmSKD.fn_LoadKategori(3)
        '        frmSKD.LoadMe(FORM_MODE.FORM_MODE_ADD)
        '        frmSKD.ShowDialog(Me)
        '    Catch ex As Exception
        '        MsgBox("Load Form Detail : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
        '    End Try
        'End If

        Dim Kode As String = fn_LoadTindakLanjutAlasanCek(txtKDPENDAFTARAN.Text, "KONSUL INTERNAL")
        If Kode <> "" Then
            Dim frmSKD As New frmSKD
            Try
                frmSKD.fn_LoadKategori(3)
                frmSKD.LoadMe(FORM_MODE.FORM_MODE_EDIT, Kode)
                frmSKD.ShowDialog(Me)
            Catch ex As Exception
                MsgBox("Load Form Detail : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
            Finally
                If Not frmSKD Is Nothing Then frmSKD.Dispose()
                frmSKD = Nothing
            End Try
        Else
            Dim frmSKD As New frmSKD
            Try
                frmSKD.fn_LoadNoPendaftaran(txtKDPENDAFTARAN.Text, txtKDCUSTOMER.Text, grdKDDEPARTMENT.EditValue, grdDPJP.EditValue, txtNOMORSEP.Text, txtPenjamin.Text, txtNAMAPASIEN.Text, txtDIAGNOOSA.Text)
                frmSKD.fn_LoadKategori(3)
                frmSKD.LoadMe(FORM_MODE.FORM_MODE_ADD)
                frmSKD.ShowDialog(Me)
            Catch ex As Exception
                MsgBox("Load Form Detail : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
            End Try
        End If

        fn_LoadTindakLanjut(txtKDPENDAFTARAN.Text)

    End Sub
    Private Sub btnAlihRawat_Click(sender As Object, e As EventArgs) Handles btnAlihRawat.Click
        If txtKDPENDAFTARAN.Text = "" Then
            MsgBox("Register Kosong", MsgBoxStyle.Exclamation, Me.Text)
            Exit Sub
        End If

        Dim Kode As String = fn_LoadTindakLanjutAlasanCek(txtKDPENDAFTARAN.Text, "ALIH RAWAT")
        If Kode <> "" Then
            Dim frmSKD As New frmSKD
            Try
                frmSKD.fn_LoadKategori(8)
                frmSKD.LoadMe(FORM_MODE.FORM_MODE_EDIT, Kode)
                frmSKD.ShowDialog(Me)
            Catch ex As Exception
                MsgBox("Load Form Detail : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
            Finally
                If Not frmSKD Is Nothing Then frmSKD.Dispose()
                frmSKD = Nothing
            End Try
        Else
            Dim frmSKD As New frmSKD
            Try
                frmSKD.fn_LoadNoPendaftaran(txtKDPENDAFTARAN.Text, txtKDCUSTOMER.Text, grdKDDEPARTMENT.EditValue, grdDPJP.EditValue, txtNOMORSEP.Text, txtPenjamin.Text, txtNAMAPASIEN.Text, txtDIAGNOOSA.Text)
                frmSKD.fn_LoadKategori(8)
                frmSKD.LoadMe(FORM_MODE.FORM_MODE_ADD)
                frmSKD.ShowDialog(Me)
            Catch ex As Exception
                MsgBox("Load Form Detail : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
            End Try
        End If

        fn_LoadTindakLanjut(txtKDPENDAFTARAN.Text)
    End Sub
    Private Sub btnRujukanHabis_Click(sender As Object, e As EventArgs) Handles btnRujukanHabis.Click
        If txtKDPENDAFTARAN.Text = "" Then
            MsgBox("Register Kosong", MsgBoxStyle.Exclamation, Me.Text)
            Exit Sub
        End If

        'Dim oSKD As New Admission.clsSKD
        'Dim ds = oSKD.GetDataPendaftaranalasan(txtKDPENDAFTARAN.Text, "RUJUKAN HABIS")

        'If ds IsNot Nothing Then
        '    Dim frmSKD As New frmSKD
        '    Try
        '        frmSKD.fn_LoadKategori(2)
        '        frmSKD.LoadMe(FORM_MODE.FORM_MODE_EDIT, ds.KDSKD)
        '        frmSKD.ShowDialog(Me)
        '    Catch ex As Exception
        '        MsgBox("Load Form Detail : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
        '    Finally
        '        If Not frmSKD Is Nothing Then frmSKD.Dispose()
        '        frmSKD = Nothing
        '    End Try
        'Else
        '    Dim frmSKD As New frmSKD
        '    Try
        '        frmSKD.fn_LoadNoPendaftaran(txtKDPENDAFTARAN.Text, txtKDCUSTOMER.Text, grdKDDEPARTMENT.EditValue, grdDPJP.EditValue, txtNOMORSEP.Text, txtPenjamin.Text, txtNAMAPASIEN.Text)
        '        frmSKD.fn_LoadKategori(2)
        '        frmSKD.LoadMe(FORM_MODE.FORM_MODE_ADD)
        '        frmSKD.ShowDialog(Me)
        '    Catch ex As Exception
        '        MsgBox("Load Form Detail : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
        '    End Try
        'End If

        Dim Kode As String = fn_LoadTindakLanjutAlasanCek(txtKDPENDAFTARAN.Text, "RUJUKAN HABIS")
        If Kode <> "" Then
            Dim frmSKD As New frmSKD
            Try
                frmSKD.fn_LoadNoPendaftaran(txtKDPENDAFTARAN.Text, txtKDCUSTOMER.Text, grdKDDEPARTMENT.EditValue, grdDPJP.EditValue, txtNOMORSEP.Text, txtPenjamin.Text, txtNAMAPASIEN.Text, txtDIAGNOOSA.Text)
                frmSKD.fn_LoadKategori(2)
                frmSKD.LoadMe(FORM_MODE.FORM_MODE_EDIT, Kode)
                frmSKD.ShowDialog(Me)
            Catch ex As Exception
                MsgBox("Load Form Detail : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
            Finally
                If Not frmSKD Is Nothing Then frmSKD.Dispose()
                frmSKD = Nothing
            End Try
        Else
            Dim frmSKD As New frmSKD
            Try
                frmSKD.fn_LoadNoPendaftaran(txtKDPENDAFTARAN.Text, txtKDCUSTOMER.Text, grdKDDEPARTMENT.EditValue, grdDPJP.EditValue, txtNOMORSEP.Text, txtPenjamin.Text, txtNAMAPASIEN.Text, txtDIAGNOOSA.Text)
                frmSKD.fn_LoadKategori(2)
                frmSKD.LoadMe(FORM_MODE.FORM_MODE_ADD)
                frmSKD.ShowDialog(Me)
            Catch ex As Exception
                MsgBox("Load Form Detail : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
            End Try
        End If

        fn_LoadTindakLanjut(txtKDPENDAFTARAN.Text)

    End Sub
    Private Sub btnPRB_Click(sender As Object, e As EventArgs) Handles btnPRB.Click
        If txtKDPENDAFTARAN.Text = "" Then
            MsgBox("Register Kosong", MsgBoxStyle.Exclamation, Me.Text)
            Exit Sub
        End If

        'Dim oSKD As New Admission.clsSKD
        'Dim ds = oSKD.GetDataPendaftaranalasan(txtKDPENDAFTARAN.Text, "PRB")

        'If ds IsNot Nothing Then
        '    Dim frmSKD As New frmSKD
        '    Try
        '        frmSKD.fn_LoadKategori(4)
        '        frmSKD.LoadMe(FORM_MODE.FORM_MODE_EDIT, ds.KDSKD)
        '        frmSKD.ShowDialog(Me)
        '    Catch ex As Exception
        '        MsgBox("Load Form Detail : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
        '    Finally
        '        If Not frmSKD Is Nothing Then frmSKD.Dispose()
        '        frmSKD = Nothing
        '    End Try
        'Else
        '    Dim frmSKD As New frmSKD
        '    Try
        '        frmSKD.fn_LoadNoPendaftaran(txtKDPENDAFTARAN.Text, txtKDCUSTOMER.Text, grdKDDEPARTMENT.EditValue, grdDPJP.EditValue, txtNOMORSEP.Text, txtPenjamin.Text, txtNAMAPASIEN.Text)
        '        frmSKD.fn_LoadKategori(4)
        '        frmSKD.LoadMe(FORM_MODE.FORM_MODE_ADD)
        '        frmSKD.ShowDialog(Me)
        '    Catch ex As Exception
        '        MsgBox("Load Form Detail : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
        '    End Try
        'End If

        Dim Kode As String = fn_LoadTindakLanjutAlasanCek(txtKDPENDAFTARAN.Text, "PRB")
        If Kode <> "" Then
            Dim frmSKD As New frmSKD
            Try
                frmSKD.fn_LoadKategori(4)
                frmSKD.LoadMe(FORM_MODE.FORM_MODE_EDIT, Kode)
                frmSKD.ShowDialog(Me)
            Catch ex As Exception
                MsgBox("Load Form Detail : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
            Finally
                If Not frmSKD Is Nothing Then frmSKD.Dispose()
                frmSKD = Nothing
            End Try
        Else
            Dim frmSKD As New frmSKD
            Try
                frmSKD.fn_LoadNoPendaftaran(txtKDPENDAFTARAN.Text, txtKDCUSTOMER.Text, grdKDDEPARTMENT.EditValue, grdDPJP.EditValue, txtNOMORSEP.Text, txtPenjamin.Text, txtNAMAPASIEN.Text, txtDIAGNOOSA.Text)
                frmSKD.fn_LoadKategori(4)
                frmSKD.LoadMe(FORM_MODE.FORM_MODE_ADD)
                frmSKD.ShowDialog(Me)
            Catch ex As Exception
                MsgBox("Load Form Detail : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
            End Try
        End If

        fn_LoadTindakLanjut(txtKDPENDAFTARAN.Text)
    End Sub
    Private Sub btnITERASI_Click(sender As Object, e As EventArgs) Handles btnITERASI.Click
        If txtKDPENDAFTARAN.Text = "" Then
            MsgBox("Register Kosong", MsgBoxStyle.Exclamation, Me.Text)
            Exit Sub
        End If

        Dim Kode As String = fn_LoadTindakLanjutAlasanCek(txtKDPENDAFTARAN.Text, "ITERASI1")
        If Kode <> "" Then
            Dim frmSKD As New frmSKD
            Try
                frmSKD.fn_LoadKategori(9)
                frmSKD.LoadMe(FORM_MODE.FORM_MODE_EDIT, Kode)
                frmSKD.ShowDialog(Me)
            Catch ex As Exception
                MsgBox("Load Form Detail : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
            Finally
                If Not frmSKD Is Nothing Then frmSKD.Dispose()
                frmSKD = Nothing
            End Try
        Else
            Dim frmSKD As New frmSKD
            Try
                frmSKD.fn_LoadNoPendaftaran(txtKDPENDAFTARAN.Text, txtKDCUSTOMER.Text, grdKDDEPARTMENT.EditValue, grdDPJP.EditValue, txtNOMORSEP.Text, txtPenjamin.Text, txtNAMAPASIEN.Text, txtDIAGNOOSA.Text)
                frmSKD.fn_LoadKategori(9)
                frmSKD.LoadMe(FORM_MODE.FORM_MODE_ADD)
                frmSKD.ShowDialog(Me)
            Catch ex As Exception
                MsgBox("Load Form Detail : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
            End Try
        End If

        fn_LoadTindakLanjut(txtKDPENDAFTARAN.Text)

    End Sub
    Private Sub SimpleButton6_Click(sender As Object, e As EventArgs) Handles SimpleButton6.Click
        If txtKDPENDAFTARAN.Text = "" Then
            MsgBox("Register Kosong", MsgBoxStyle.Exclamation, Me.Text)
            Exit Sub
        End If

        Dim Kode As String = fn_LoadTindakLanjutAlasanCek(txtKDPENDAFTARAN.Text, "ITERASI2")
        If Kode <> "" Then
            Dim frmSKD As New frmSKD
            Try
                frmSKD.fn_LoadKategori(9)
                frmSKD.LoadMe(FORM_MODE.FORM_MODE_EDIT, Kode)
                frmSKD.ShowDialog(Me)
            Catch ex As Exception
                MsgBox("Load Form Detail : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
            Finally
                If Not frmSKD Is Nothing Then frmSKD.Dispose()
                frmSKD = Nothing
            End Try
        Else
            Dim frmSKD As New frmSKD
            Try
                frmSKD.fn_LoadNoPendaftaran(txtKDPENDAFTARAN.Text, txtKDCUSTOMER.Text, grdKDDEPARTMENT.EditValue, grdDPJP.EditValue, txtNOMORSEP.Text, txtPenjamin.Text, txtNAMAPASIEN.Text, txtDIAGNOOSA.Text)
                frmSKD.fn_LoadKategori(10)
                frmSKD.LoadMe(FORM_MODE.FORM_MODE_ADD)
                frmSKD.ShowDialog(Me)
            Catch ex As Exception
                MsgBox("Load Form Detail : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
            End Try
        End If

        fn_LoadTindakLanjut(txtKDPENDAFTARAN.Text)

    End Sub
    Private Sub btnRujukBalik_Click(sender As Object, e As EventArgs) Handles btnRujukBalik.Click
        If txtKDPENDAFTARAN.Text = "" Then
            MsgBox("Register Kosong", MsgBoxStyle.Exclamation, Me.Text)
            Exit Sub
        End If

        'Dim oSKD As New Admission.clsSKD
        'Dim ds = oSKD.GetDataPendaftaranalasan(txtKDPENDAFTARAN.Text, "RUJUK BALIK")

        'If ds IsNot Nothing Then
        '    Dim frmSKD As New frmSKD
        '    Try
        '        frmSKD.fn_LoadKategori(5)
        '        frmSKD.LoadMe(FORM_MODE.FORM_MODE_EDIT, ds.KDSKD)
        '        frmSKD.ShowDialog(Me)
        '    Catch ex As Exception
        '        MsgBox("Load Form Detail : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
        '    Finally
        '        If Not frmSKD Is Nothing Then frmSKD.Dispose()
        '        frmSKD = Nothing
        '    End Try
        'Else
        '    Dim frmSKD As New frmSKD
        '    Try
        '        frmSKD.fn_LoadNoPendaftaran(txtKDPENDAFTARAN.Text, txtKDCUSTOMER.Text, grdKDDEPARTMENT.EditValue, grdDPJP.EditValue, txtNOMORSEP.Text, txtPenjamin.Text, txtNAMAPASIEN.Text)
        '        frmSKD.fn_LoadKategori(5)
        '        frmSKD.LoadMe(FORM_MODE.FORM_MODE_ADD)
        '        frmSKD.ShowDialog(Me)
        '    Catch ex As Exception
        '        MsgBox("Load Form Detail : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
        '    End Try
        'End If

        Dim Kode As String = fn_LoadTindakLanjutAlasanCek(txtKDPENDAFTARAN.Text, "RUJUK BALIK")
        If Kode <> "" Then
            Dim frmSKD As New frmSKD
            Try
                frmSKD.fn_LoadKategori(5)
                frmSKD.LoadMe(FORM_MODE.FORM_MODE_EDIT, Kode)
                frmSKD.ShowDialog(Me)
            Catch ex As Exception
                MsgBox("Load Form Detail : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
            Finally
                If Not frmSKD Is Nothing Then frmSKD.Dispose()
                frmSKD = Nothing
            End Try
        Else
            Dim frmSKD As New frmSKD
            Try
                frmSKD.fn_LoadNoPendaftaran(txtKDPENDAFTARAN.Text, txtKDCUSTOMER.Text, grdKDDEPARTMENT.EditValue, grdDPJP.EditValue, txtNOMORSEP.Text, txtPenjamin.Text, txtNAMAPASIEN.Text, txtDIAGNOOSA.Text)
                frmSKD.fn_LoadKategori(5)
                frmSKD.LoadMe(FORM_MODE.FORM_MODE_ADD)
                frmSKD.ShowDialog(Me)
            Catch ex As Exception
                MsgBox("Load Form Detail : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
            End Try
        End If

        fn_LoadTindakLanjut(txtKDPENDAFTARAN.Text)
    End Sub
    Private Sub btnRawatInap_Click(sender As Object, e As EventArgs) Handles btnRawatInap.Click
        If txtKDPENDAFTARAN.Text = "" Then
            MsgBox("Register Kosong", MsgBoxStyle.Exclamation, Me.Text)
            Exit Sub
        End If

        If MsgBox("Apakah Rawat Inap?", MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, Me.Text) = MsgBoxResult.No Then Exit Sub

        'Dim oSKD As New Admission.clsSKD
        'Dim ds = oSKD.GetDataPendaftaranalasan(txtKDPENDAFTARAN.Text, "RAWAT INAP")

        'If ds IsNot Nothing Then
        '    fn_SaveSKD(False, ds.KDSKD, "RAWAT INAP")
        'Else
        '    fn_SaveSKD(True, "", "RAWAT INAP")
        'End If

        sRemarks_Ruangan = ""
        sRemarks_RencanaPembedahan = ""
        sRemarks_IntruksiDokter = ""

        Dim Kode As String = fn_LoadTindakLanjutAlasanCek(txtKDPENDAFTARAN.Text, "RAWAT INAP")

        Dim frmRemarksRawatInap As New frmRemarksRawatInap
        Try
            frmRemarksRawatInap.ShowDialog(Me)
        Catch ex As Exception
            MsgBox("Load Form Detail : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try

        If Kode <> "" Then
            fn_SaveSKD(False, Kode, "RAWAT INAP")
        Else
            fn_SaveSKD(True, "", "RAWAT INAP")
        End If

        fn_LoadTindakLanjut(txtKDPENDAFTARAN.Text)

        sRemarks = ""
        sRemarks_Ruangan = ""
        sRemarks_RencanaPembedahan = ""
        sRemarks_IntruksiDokter = ""
    End Sub
    Private Function fn_SaveSKD(ByVal isAdd As Boolean, ByVal NOIID As String, ByVal ALASAN As String) As Boolean
        Try
            ' ***** HEADER *****
            Dim oSKD As New Admission.clsSKD
            Dim ds = oSKD.GetStructureHeader
            With ds
                Try
                    .DATECREATED = oSKD.GetData(NOIID).DATECREATED
                Catch oErr As Exception
                    .DATECREATED = Now
                End Try

                .DATEUPDATED = Now
                .KDSKD = NOIID
                .KDANTRIANMANUAL = 0
                Try
                    .KDPENDAFTARAN = oSKD.GetData(NOIID).KDPENDAFTARAN
                Catch oErr As Exception
                    .KDPENDAFTARAN = txtKDPENDAFTARAN.Text
                End Try
                .DATE = deDATE.DateTime
                .ISCATEGORY = 0
                Try
                    .KDDIAGNOSA = oSKD.GetData(NOIID).KDDIAGNOSA
                Catch oErr As Exception
                    .KDDIAGNOSA = "-"
                End Try
                .KDDEPARTMENT = grdKDDEPARTMENT.EditValue
                .KDDOCTOR = grdDPJP.EditValue
                .NOMORRUJUKAN = ""
                .DESCRIPTION = sRemarks_IntruksiDokter
                Try
                    .ISCHEKED = oSKD.GetData(NOIID).ISCHEKED
                Catch oErr As Exception
                    .ISCHEKED = False
                End Try
                .KDUSER = sUserID
                .DATEKONTROL = Now
                .ALASAN = ALASAN
                .TINDAKLANJUT = ""
                .TANGGALPERIKSA_TEXT = Now.ToString("ddMMyyyy")
                .KDJADWALDOKTER = 0
                .SEQ = 0
                .REQUEST = sRemarks_Ruangan
                .RESPONSE = sRemarks_RencanaPembedahan
                .NOMORSEP = ""
                .SEARCH = Now.ToString("ddMMyyyy")
                Try
                    .KDCUSTOMER = oSKD.GetData(NOIID).KDCUSTOMER
                Catch oErr As Exception
                    .KDCUSTOMER = txtKDCUSTOMER.Text
                End Try
                .ORDERPENUNJANG = ""
                .DATEKONTROL_2 = deDATE.DateTime
            End With

            If isAdd = True Then
                Dim KDSKD As String = oSKD.InsertData(ds, NOIID)

                If KDSKD <> "" Then
                    fn_SaveSKD = True
                Else
                    fn_SaveSKD = False
                End If
            Else
                fn_SaveSKD = oSKD.UpdateData(ds)
            End If
        Catch oErr As Exception
            MsgBox("Save SKD" & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            fn_SaveSKD = False
        End Try
    End Function
    Private Sub btnPenjadwalanOperasi_Click(sender As Object, e As EventArgs) Handles btnPenjadwalanOperasi.Click
        If txtKDPENDAFTARAN.Text = "" Then
            MsgBox("Register Kosong", MsgBoxStyle.Exclamation, Me.Text)
            Exit Sub
        End If

        If MsgBox("Apakah Penjadwalan Operasi?", MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, Me.Text) = MsgBoxResult.No Then Exit Sub

        Dim oSKD As New Admission.clsSKD
        Dim ds = oSKD.GetDataPendaftaranalasan(txtKDPENDAFTARAN.Text, "PENJADWALAN OPERASI")

        If ds IsNot Nothing Then
            fn_SaveSKD(False, ds.KDSKD, "PENJADWALAN OPERASI")
        Else
            fn_SaveSKD(True, "", "PENJADWALAN OPERASI")
        End If

        fn_LoadTindakLanjut(txtKDPENDAFTARAN.Text)

    End Sub
    Private Sub btnSelesaiPengobatan_Click(sender As Object, e As EventArgs) Handles btnSelesaiPengobatan.Click
        If txtKDPENDAFTARAN.Text Is Nothing Then
            MsgBox("Register Kosong", MsgBoxStyle.Exclamation, Me.Text)
            Exit Sub
        End If

        If MsgBox("Apakah Selesai Pengobatan?", MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, Me.Text) = MsgBoxResult.No Then Exit Sub

        Dim oSKD As New Admission.clsSKD
        Dim ds = oSKD.GetDataPendaftaranalasan(txtKDPENDAFTARAN.Text, "SELESAI PENGOBATAN")

        If ds IsNot Nothing Then
            fn_SaveSKD(False, ds.KDSKD, "SELESAI PENGOBATAN")
        Else
            fn_SaveSKD(True, "", "SELESAI PENGOBATAN")
        End If

        fn_LoadTindakLanjut(txtKDPENDAFTARAN.Text)
    End Sub
    Private Sub btnKembaliKeDokter_Click(sender As Object, e As EventArgs) Handles btnKembaliKeDokter.Click
        If txtKDPENDAFTARAN.Text Is Nothing Then
            MsgBox("Register Kosong", MsgBoxStyle.Exclamation, Me.Text)
            Exit Sub
        End If

        If MsgBox("Apakah Akan dilakukan Penunjang hari Ini?", MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, Me.Text) = MsgBoxResult.No Then Exit Sub

        Dim oSKD As New Admission.clsSKD
        Dim ds = oSKD.GetDataPendaftaranalasan(txtKDPENDAFTARAN.Text, "PENUNJANG HARI INI")

        If ds IsNot Nothing Then
            fn_SaveSKD(False, ds.KDSKD, "PENUNJANG HARI INI")
        Else
            fn_SaveSKD(True, "", "PENUNJANG HARI INI")
        End If

        'txtTINDAKLANJUT.Text = "Penunjang Hari Ini, Kembali Ke Dokter"

        fn_LoadTindakLanjut(txtKDPENDAFTARAN.Text)
    End Sub
    Private Sub DeleteToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DeleteToolStripMenuItem.Click
        grvDetailResep.DeleteSelectedRows()
    End Sub
    Private Sub DeleteAllToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DeleteAllToolStripMenuItem.Click
        grvDetailResep.OptionsSelection.MultiSelect = True
        grvDetailResep.SelectAll()
        grvDetailResep.DeleteSelectedRows()
        grvDetailResep.OptionsSelection.MultiSelect = False
    End Sub
    Private Sub ToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem1.Click
        grvDiagnosaPenyerta.DeleteSelectedRows()
    End Sub
    Private Sub DeleteAllToolStripMenuItem2_Click(sender As Object, e As EventArgs) Handles DeleteAllToolStripMenuItem2.Click
        grvDiagnosaPenyerta.OptionsSelection.MultiSelect = True
        grvDiagnosaPenyerta.SelectAll()
        grvDiagnosaPenyerta.DeleteSelectedRows()
        grvDiagnosaPenyerta.OptionsSelection.MultiSelect = False
    End Sub
    Private Sub ToolStripMenuItem2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem2.Click
        grvTindakan.DeleteSelectedRows()
    End Sub
    Private Sub DeleteAllToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles DeleteAllToolStripMenuItem1.Click
        grvTindakan.OptionsSelection.MultiSelect = True
        grvTindakan.SelectAll()
        grvTindakan.DeleteSelectedRows()
        grvTindakan.OptionsSelection.MultiSelect = False
    End Sub
    Private Sub ToolStripMenuItem3_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem3.Click
        grvTindakanPoli.DeleteSelectedRows()
    End Sub
    Private Sub ToolStripMenuItem4_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem4.Click
        grvTindakanPoli.OptionsSelection.MultiSelect = True
        grvTindakanPoli.SelectAll()
        grvTindakanPoli.DeleteSelectedRows()
        grvTindakanPoli.OptionsSelection.MultiSelect = False
    End Sub
    Private Sub ToolStripMenuItem5_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem5.Click
        grvOBATRACIKAN.DeleteSelectedRows()
    End Sub
    Private Sub ToolStripMenuItem6_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem6.Click
        grvOBATRACIKAN.OptionsSelection.MultiSelect = True
        grvOBATRACIKAN.SelectAll()
        grvOBATRACIKAN.DeleteSelectedRows()
        grvOBATRACIKAN.OptionsSelection.MultiSelect = False
    End Sub
    Private Sub PasteToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PasteToolStripMenuItem.Click
        For Each iLoop In listCopy
            grvDetailResep.Focus()
            grvDetailResep.AddNewRow()

            grvDetailResep.SetFocusedRowCellValue(colKDITEM, iLoop.KDITEM)
            grvDetailResep.SetFocusedRowCellValue(colKDUOM, iLoop.KDUOM)
            grvDetailResep.SetFocusedRowCellValue(colKDSIGNA, iLoop.KDSIGNA)
            grvDetailResep.SetFocusedRowCellValue(colKDCARAPAKAI, iLoop.KDCARAPAKAI)
            grvDetailResep.SetFocusedRowCellValue(colQTY, iLoop.QTY)
            grvDetailResep.SetFocusedRowCellValue(colREMARKS_DOKTER, iLoop.REMARKS_DOKTER)
            grvDetailResep.UpdateCurrentRow()
        Next
    End Sub
    Private Sub SimpleButton1_Click(sender As Object, e As EventArgs)
        Dim frmReportOrderPenunjang As New frmReportOrderPenunjang
        Try
            frmReportOrderPenunjang.ShowDialog(Me)

            For Each xloop In sListRincianLab
                grvTindakan.Focus()
                grvTindakan.AddNewRow()
                grvTindakan.SetFocusedRowCellValue(colTERAPI, "")
                grvTindakan.SetFocusedRowCellValue(colTINDAKAN, "LABORATORIUM " & xloop)
                grvTindakan.UpdateCurrentRow()
            Next

            For Each xloop In sListRincianRad
                grvTindakan.Focus()
                grvTindakan.AddNewRow()
                grvTindakan.SetFocusedRowCellValue(colTERAPI, "")
                grvTindakan.SetFocusedRowCellValue(colTINDAKAN, "RADIOLOGI " & xloop)
                grvTindakan.UpdateCurrentRow()
            Next
        Catch ex As Exception
            MsgBox("Load Form Detail Penunjang: " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub btnMasukPoli_Click(sender As Object, e As EventArgs) Handles btnMasukPoli.Click
        Try
            If grvListPasien.GetFocusedRowCellValue("NoRegister") Is Nothing Then
                MsgBox("Silahkan Pilih Antrian Pasien yang akan dipanggil", MsgBoxStyle.Information, Me.Text)
                Exit Sub
            End If

            'If grvListPasien.GetFocusedRowCellValue("Antrian") <> "" Then
            '    Dim uTime As Integer = (DateTime.UtcNow - New DateTime(1970, 1, 1, 0, 0, 0)).TotalSeconds
            '    Dim JsonRequest As String = fn_RequestUpdateWaktuAntrean(grvListPasien.GetFocusedRowCellValue("Antrian"), 4, uTime)
            '    If JsonRequest <> "" Then
            '        MsgBox(fn_UpdateWaktuAntrean(JsonRequest, uTime), MsgBoxStyle.Information, Me.Text)
            '    End If
            'End If

            Dim oConn As New SqlConnection
            Dim oComm As New SqlCommand
            Dim da As SqlDataAdapter
            Dim ds As New DataSet
            Dim SQL As String
            oConn = New SqlConnection(sConnOld)

            If oConn.State = ConnectionState.Closed Then
                oConn.Open()
            End If

            SQL = "SELECT "
            SQL &= "* "
            SQL &= "FROM "
            SQL &= "S_PENDAFTARAN_H A "
            SQL &= "INNER JOIN M_DEPARTMENT B "
            SQL &= "ON A.KDDEPARTMENT = B.KDDEPARTMENT "
            SQL &= "INNER JOIN M_DOCTOR C "
            SQL &= "ON A.KDDOCTOR = C.KDDOCTOR "
            SQL &= "WHERE "
            SQL &= "A.KDREG = '" & grvListPasien.GetFocusedRowCellValue("NoRegister") & "' "

            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "SEARCHPENDAFTARAN")

            If oConn.State = ConnectionState.Open Then
                oConn.Close()
            End If

            For iLoop As Integer = 0 To ds.Tables("SEARCHPENDAFTARAN").Rows.Count - 1
                With ds.Tables("SEARCHPENDAFTARAN")
                    If .Rows(iLoop)("NOMORANTRIAN") <> "" Then
                        If fn_subcektTaksId4(.Rows(iLoop)("NOMORANTRIAN"), 4) = False Then
                            fn_SaveTransaksi(.Rows(iLoop)("KDPOLIBPJS"), .Rows(iLoop)("KODEANTRIAN"), .Rows(iLoop)("ANTRIANDOKTER"), "ULANG")
                        Else
                            fn_SaveTransaksi(.Rows(iLoop)("KDPOLIBPJS"), .Rows(iLoop)("KODEANTRIAN"), .Rows(iLoop)("ANTRIANDOKTER"), "")
                        End If
                    Else
                        fn_SaveTransaksi(.Rows(iLoop)("KDPOLIBPJS"), .Rows(iLoop)("KODEANTRIAN"), .Rows(iLoop)("ANTRIANDOKTER"), "")
                    End If
                End With
            Next
        Catch oErr As Exception
            MsgBox("Print Data : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Function fn_subcektTaksId4(ByVal Antrian As String, ByVal Taksid As Integer) As Boolean
        Try
            fn_subcektTaksId4 = False

            If fn_CEKADAANTRIAN(Antrian) = False Then
                fn_subcektTaksId4 = True
                Exit Function
            End If

            Dim oConn As New SqlConnection
            Dim oComm As New SqlCommand
            Dim da As SqlDataAdapter
            Dim ds As New DataSet
            Dim SQL As String
            oConn = New SqlConnection(sConnOld)

            If oConn.State = ConnectionState.Closed Then
                oConn.Open()
            End If

            SQL = "SELECT "
            SQL &= "* "
            SQL &= "FROM "
            SQL &= "Z_ANTRIAN_POLI A "
            SQL &= "INNER JOIN Z_ANTRIAN_H B "
            SQL &= "ON A.KODEBOOKING = B.KODEBOOKING "
            SQL &= "WHERE "
            SQL &= "A.KODEBOOKING = '" & Antrian & "' "
            SQL &= "AND A.TAKSID = '" & Taksid & "' "

            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "Z_ANTRIAN_POLI")

            Dim Ada As Boolean = False

            For iLoop As Integer = 0 To ds.Tables("Z_ANTRIAN_POLI").Rows.Count - 1
                Ada = True
                'With ds.Tables("Z_ANTRIAN_POLI")
                '    If .Rows(iLoop)("TAKSID") = "4" Then
                '        cek = True
                '    End If
                'End With
            Next

            If oConn.State = ConnectionState.Open Then
                oConn.Close()
            End If

            If Ada = False Then
                fn_SaveTaksIDPoli(Antrian, Taksid)
                fn_subcektTaksId4 = True
            Else
                fn_subcektTaksId4 = True
            End If

        Catch oErr As Exception
            fn_subcektTaksId4 = False
            MsgBox("Print Data : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Function
    Private Function fn_LoadDateSever() As Date
        Try
            Dim oConn As New SqlConnection
            Dim oComm As New SqlCommand
            Dim da As SqlDataAdapter
            Dim ds As New DataSet
            Dim SQL As String
            oConn = New SqlConnection(sConnOld)

            If oConn.State = ConnectionState.Closed Then
                oConn.Open()
            End If

            SQL = " Select TANGGAL = GetDate() "

            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "TANGGAL")

            fn_LoadDateSever = Now

            For iLoop As Integer = 0 To ds.Tables("TANGGAL").Rows.Count - 1
                With ds.Tables("TANGGAL")
                    fn_LoadDateSever = .Rows(iLoop)("TANGGAL")
                End With
            Next

            If oConn.State = ConnectionState.Connecting Then
                oConn.Close()
            End If
        Catch oErr As Exception
            fn_LoadDateSever = Now
            MsgBox("Load Date : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Function
    Private Sub fn_SaveTransaksi(ByVal sKODE_POLI As String, ByVal sKODE_DOKTER As String, ByVal sNOMORANTRIAN As Integer, ByVal sDESCRIPTION As String)
        Try
            Dim oSet_Panggilan As New Antrian.clsSET_PANGGIL_ANTRIAN

            ' ***** HEADER *****

            Dim ds = oSet_Panggilan.GetStructureHeader

            With ds
                .KODE_POLI = sKODE_POLI
                .KODE_DOKTER = sKODE_DOKTER
                .NOMORANTRIAN = sNOMORANTRIAN
                .DESCRIPTION = sDESCRIPTION
                .ISPANGGIL = False
            End With

            Dim dsSetPanggilan = oSet_Panggilan.GetData(sKODE_POLI, sKODE_DOKTER)

            If dsSetPanggilan Is Nothing Then
                oSet_Panggilan.InsertData(ds)
            Else
                oSet_Panggilan.UpdateData(ds)
            End If

        Catch oErr As Exception
            MsgBox("Simpan Panggil: " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Function fn_CEKADAANTRIAN(ByVal sKODEBOOKING As String) As Boolean
        Try
            fn_CEKADAANTRIAN = False

            Dim oConn As New SqlConnection
            Dim oComm As New SqlCommand
            Dim da As SqlDataAdapter
            Dim ds As New DataSet
            Dim SQL As String

            oConn = New SqlConnection(sConnOld)

            If oConn.State = ConnectionState.Closed Then
                oConn.Open()
            End If

            SQL = "SELECT "
            SQL &= "* "
            SQL &= "FROM "
            SQL &= "Z_ANTRIAN_H A "
            SQL &= "WHERE "
            SQL &= "A.KODEBOOKING = '" & sKODEBOOKING & "' "

            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "Z_ANTRIAN_HGET")

            If oConn.State = ConnectionState.Connecting Then
                oConn.Close()
            End If

            For iLoop As Integer = 0 To ds.Tables("Z_ANTRIAN_HGET").Rows.Count - 1
                fn_CEKADAANTRIAN = True
            Next

        Catch oErr As Exception
            fn_CEKADAANTRIAN = False
            MsgBox("Simpan Taks Id Gagal: " & vbCrLf & oErr.Message)
        End Try
    End Function
    Public Sub fn_SaveTaksIDPoli(ByVal sKODEBOOKING As String, ByVal TAKSID As Integer)
        Try
            Dim oConn As New SqlConnection
            Dim oComm As New SqlCommand
            Dim da As SqlDataAdapter
            Dim ds As New DataSet
            Dim SQL As String

            oConn = New SqlConnection(sConnOld)

            If oConn.State = ConnectionState.Closed Then
                oConn.Open()
            End If

            SQL = "INSERT INTO "
            SQL &= "Z_ANTRIAN_POLI "
            SQL &= "( "
            SQL &= "KODEBOOKING "
            SQL &= ",TAKSID "
            SQL &= ",WAKTU "
            SQL &= ",WAKTU_TEXT "
            SQL &= ",KODE_BPJS "
            SQL &= ",REQUEST "
            SQL &= ",RESPONS "
            SQL &= ") "
            SQL &= "VALUES "
            SQL &= "( "
            SQL &= "'" & sKODEBOOKING & "' "
            SQL &= ",'" & TAKSID & "' "
            SQL &= ",'" & fn_LoadDateSever().ToString("yyyy-MM-dd HH:mm:ss") & "' "
            SQL &= ",'" & fn_LoadDateSever().ToString("yyyyMMdd") & "' "
            SQL &= ",'0' "
            SQL &= ",'' "
            SQL &= ",'' "
            SQL &= ") "

            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "INSERTZ_ANTRIAN_POLI")

            If oConn.State = ConnectionState.Connecting Then
                oConn.Close()
            End If
        Catch oErr As Exception
            MsgBox("Simpan Taks Id Gagal: " & vbCrLf & oErr.Message)
        End Try
    End Sub
    Private Sub btnDiagnosaTerakhir_Click(sender As Object, e As EventArgs) Handles btnDiagnosaTerakhir.Click
        Dim oReq_Recipe As New Transaksi.clsReq_Recipe
        Dim dsDiagnosaAkhir = oReq_Recipe.GetDiagnosaTerakhir(txtKDCUSTOMER.Text, sUserID)
        If dsDiagnosaAkhir IsNot Nothing Then
            grvDiagnosaPenyerta.OptionsSelection.MultiSelect = True
            grvDiagnosaPenyerta.SelectAll()
            grvDiagnosaPenyerta.DeleteSelectedRows()
            grvDiagnosaPenyerta.OptionsSelection.MultiSelect = False
            txtDIAGNOOSA.ResetText()

            txtDIAGNOOSA.Text = dsDiagnosaAkhir.DIAGNOSA

            For Each xloop In oReq_Recipe.GetDataDetailDiagnosaPenyerta(dsDiagnosaAkhir.KDREQRECIPE).OrderBy(Function(x) x.SEQ).ToList()
                grvDiagnosaPenyerta.AddNewRow()
                grvDiagnosaPenyerta.SetFocusedRowCellValue(colKETERANGAN, xloop.KETERANGAN)
                grvDiagnosaPenyerta.UpdateCurrentRow()
            Next

        Else
            MsgBox("Pasien Belum Ada diagnosa Terkahir")
        End If
    End Sub
    Private Sub SimpleButton3_Click_1(sender As Object, e As EventArgs) Handles SimpleButton3.Click
        Try
            If txtKARTUBPJS.Text = "" Then
                MsgBox("Kartu BPJS Pasien Kosong", MsgBoxStyle.Exclamation, Me.Text)
                Exit Sub
            End If
            If grdDPJP.Text = "" Then Exit Sub

            Dim oDokter As New Reference.clsDoctor
            Dim oKoneksi As New Brigging.clsSetKoneksi
            Dim uTime As Integer = (DateTime.UtcNow - New DateTime(1970, 1, 1, 0, 0, 0)).TotalSeconds
            Dim allData = JObject.Parse(oKoneksi.GetDataIcare(sSecreateKeyVclaim, txtKARTUBPJS.Text, oDokter.GetData(grdDPJP.EditValue).VCLAIM_KDDPJP, uTime))

            Dim CodeResponse As String = String.Empty
            Dim messageResponse As String = String.Empty

            CodeResponse = IIf(IsDBNull(allData.Item("metaData").Item("code")) = True, "", allData.Item("metaData").Item("code"))
            messageResponse = allData("metaData")("message").ToString

            If CodeResponse = "200" Then
                Dim DataDecrypt = JObject.Parse(oKoneksi.Decrypt(allData("response"), "16694" & sSecreateKeyVclaim & uTime))
                Dim variabel As String = "CMD /c Start chrome /profile-directory=""Default"" """ & DataDecrypt.Item("url").ToString() & """"
                Shell(variabel, vbNormalFocus)
            Else
                MsgBox(CodeResponse & " - " & messageResponse, MsgBoxStyle.Exclamation, Me.Text)
            End If
        Catch oErr As Exception
            MsgBox("Icare: " & vbCrLf & oErr.Message)
        End Try
    End Sub
    Private Sub GERDQToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles GERDQToolStripMenuItem.Click
        If txtKDPENDAFTARAN.Text = "" Then
            MsgBox("Silhakan Pilih Pasien", MsgBoxStyle.Information, Me.Text)
            fn_EmptyMe()
            Exit Sub
        End If

        Dim oGerd As New Inventory.clsDigital_IGD_01_GERD
        Dim dsGerd = oGerd.GetDataByKdreg(txtKDPENDAFTARAN.Text)
        If dsGerd Is Nothing Then
            Dim frmDigital_IGD_01_GERD As New frmDigital_IGD_01_GERD
            Try
                frmDigital_IGD_01_GERD.LoadMe(FORM_MODE.FORM_MODE_ADD, txtKDPENDAFTARAN.Text, txtNAMAPASIEN.Text, txtKDCUSTOMER.Text)
                frmDigital_IGD_01_GERD.ShowDialog(Me)
                fn_LoadSecurity()
            Catch ex As Exception
                MsgBox("Load Form Detail : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
            Finally
                If Not frmDigital_IGD_01_GERD Is Nothing Then frmDigital_IGD_01_GERD.Dispose()
                frmDigital_IGD_01_GERD = Nothing
            End Try
        Else
            Dim frmDigital_IGD_01_GERD As New frmDigital_IGD_01_GERD
            Try
                frmDigital_IGD_01_GERD.LoadMe(FORM_MODE.FORM_MODE_EDIT, txtKDPENDAFTARAN.Text, txtNAMAPASIEN.Text, txtKDCUSTOMER.Text, dsGerd.KDGERD)
                frmDigital_IGD_01_GERD.ShowDialog(Me)
                fn_LoadSecurity()
            Catch ex As Exception
                MsgBox("Load Form Detail : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
            Finally
                If Not frmDigital_IGD_01_GERD Is Nothing Then frmDigital_IGD_01_GERD.Dispose()
                frmDigital_IGD_01_GERD = Nothing
            End Try
        End If
    End Sub
    Private Sub btnGERDQ_Click(sender As Object, e As EventArgs) Handles btnGERDQ.Click
        If txtKDPENDAFTARAN.Text = "" Then
            MsgBox("Silhakan Pilih Pasien", MsgBoxStyle.Information, Me.Text)
            fn_EmptyMe()
            Exit Sub
        End If

        Dim oGerd As New Inventory.clsDigital_IGD_01_GERD
        Dim dsGerd = oGerd.GetDataByKdreg(txtKDPENDAFTARAN.Text)
        If dsGerd Is Nothing Then
            Dim frmDigital_IGD_01_GERD As New frmDigital_IGD_01_GERD
            Try
                frmDigital_IGD_01_GERD.LoadMe(FORM_MODE.FORM_MODE_ADD, txtKDPENDAFTARAN.Text, txtNAMAPASIEN.Text, txtKDCUSTOMER.Text)
                frmDigital_IGD_01_GERD.ShowDialog(Me)
                fn_LoadSecurity()
            Catch ex As Exception
                MsgBox("Load Form Detail : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
            Finally
                If Not frmDigital_IGD_01_GERD Is Nothing Then frmDigital_IGD_01_GERD.Dispose()
                frmDigital_IGD_01_GERD = Nothing
            End Try
        Else
            Dim frmDigital_IGD_01_GERD As New frmDigital_IGD_01_GERD
            Try
                frmDigital_IGD_01_GERD.LoadMe(FORM_MODE.FORM_MODE_EDIT, txtKDPENDAFTARAN.Text, txtNAMAPASIEN.Text, txtKDCUSTOMER.Text, dsGerd.KDGERD)
                frmDigital_IGD_01_GERD.ShowDialog(Me)
                fn_LoadSecurity()
            Catch ex As Exception
                MsgBox("Load Form Detail : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
            Finally
                If Not frmDigital_IGD_01_GERD Is Nothing Then frmDigital_IGD_01_GERD.Dispose()
                frmDigital_IGD_01_GERD = Nothing
            End Try
        End If
    End Sub
    Private Sub LaporanTindakanToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles LaporanTindakanToolStripMenuItem.Click
        If txtKDPENDAFTARAN.Text = "" Then
            MsgBox("Silhakan Pilih Pasien", MsgBoxStyle.Information, Me.Text)
            fn_EmptyMe()
            Exit Sub
        End If

        Dim frmLaporanTindakanList As New frmLaporanTindakanList
        Try
            frmLaporanTindakanList.fn_LoadMe(grdDPJP.EditValue, grdDPJP.EditValue, txtKDPENDAFTARAN.Text, txtKDCUSTOMER.Text, txtNAMAPASIEN.Text, txtOBJEKTIF_JENISKELAMIN.Text, deDATETANGGALLAHIR.DateTime)
            frmLaporanTindakanList.ShowDialog(Me)
        Catch ex As Exception
            MsgBox("Load Form Detail : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
        Finally
            If Not frmLaporanTindakanList Is Nothing Then frmLaporanTindakanList.Dispose()
            frmLaporanTindakanList = Nothing
        End Try
    End Sub
    Private Sub LaporanOperasiToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles LaporanOperasiToolStripMenuItem.Click
        If txtKDPENDAFTARAN.Text = "" Then
            MsgBox("Silhakan Pilih Pasien", MsgBoxStyle.Information, Me.Text)
            fn_EmptyMe()
            Exit Sub
        End If

        Dim frmLaporanOperasiList As New frmLaporanOperasiList
        Try
            frmLaporanOperasiList.fn_LoadMe(grdDPJP.EditValue, grdDPJP.EditValue, txtKDPENDAFTARAN.Text, txtKDCUSTOMER.Text, txtNAMAPASIEN.Text, txtOBJEKTIF_JENISKELAMIN.Text, deDATETANGGALLAHIR.DateTime)
            frmLaporanOperasiList.ShowDialog(Me)
        Catch ex As Exception
            MsgBox("Load Form Detail : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
        Finally
            If Not frmLaporanOperasiList Is Nothing Then frmLaporanOperasiList.Dispose()
            frmLaporanOperasiList = Nothing
        End Try
    End Sub
    Private Sub btnTutupListLaporanOperasi_Click(sender As Object, e As EventArgs) Handles btnTutupListLaporanOperasi.Click
        If btnTutupListLaporanOperasi.Text = "Tutup List" Then
            btnTutupListLaporanOperasi.Text = "Buka List"
            lGrdLaporanOperasi.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        Else
            btnTutupListLaporanOperasi.Text = "Tutup List"
            lGrdLaporanOperasi.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        End If
    End Sub
    Private Sub LembarObservasiToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles LembarObservasiToolStripMenuItem.Click
        If txtKDPENDAFTARAN.Text = "" Then
            MsgBox("Silhakan Pilih Pasien", MsgBoxStyle.Information, Me.Text)
            fn_EmptyMe()
            Exit Sub
        End If

        Dim frmLembarObservasiList As New frmLembarObservasiList
        Try
            frmLembarObservasiList.fn_LoadMe(grdDPJP.EditValue, grdKDDEPARTMENT.Text, "", txtKARTUBPJS.Text, grdDPJP.Text, txtKDPENDAFTARAN.Text, txtKDCUSTOMER.Text, txtNAMAPASIEN.Text, txtOBJEKTIF_JENISKELAMIN.Text, deDATETANGGALLAHIR.DateTime)
            frmLembarObservasiList.ShowDialog(Me)
        Catch ex As Exception
            MsgBox("Load Form Detail : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
        Finally
            If Not frmLembarObservasiList Is Nothing Then frmLembarObservasiList.Dispose()
            frmLembarObservasiList = Nothing
        End Try
    End Sub
    Private Sub deDATE_EditValueChanged(sender As Object, e As EventArgs) Handles deDATE.EditValueChanged
        If grdDPJP.Text <> "" Then
            fn_LoadJadwalDokter(grdDPJP.EditValue)
        End If
    End Sub

#End Region
#End Region
    'Private Sub Form1_MouseWheel(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Me.MouseWheel
    '    'If lblScroll.Text = "" Then Exit Sub

    '    If e.Delta > 0 Then
    '        Trace.WriteLine("Scrolled up!")
    '        fn_ScrollPage(True)
    '    Else
    '        Trace.WriteLine("Scrolled down!")
    '        fn_ScrollPage(False)
    '    End If
    'End Sub
    ''Private Sub frmRawatInapList_MouseWheel(sender As Object, e As MouseEventArgs) Handles MyBase.MouseWheel
    ''    If e.Delta > 0 Then
    ''        'up
    ''        fn_ScrollPage(True)
    ''    Else
    ''        'down
    ''        fn_ScrollPage(False)
    ''    End If
    ''End Sub

    'Private Sub fn_ScrollPage(ByVal isUp As Boolean)
    '    Dim myView As Point = Me.Panel1.AutoScrollPosition
    '    Dim scrollchange As Integer = 50

    '    If isUp Then
    '        'up
    '        myView.X = -myView.X
    '        myView.Y = -scrollchange - myView.Y
    '    Else
    '        'down
    '        myView.X = -myView.X
    '        myView.Y = scrollchange - myView.Y
    '    End If

    '    Me.Panel1.AutoScrollPosition = myView
    'End Sub
    Private Sub btnSuratKeteranganSakit_Click(sender As Object, e As EventArgs) Handles btnSuratKeteranganSakit.Click
        If txtKDPENDAFTARAN.Text = "" Then
            MsgBox("Silhakan Pilih Pasien", MsgBoxStyle.Information, Me.Text)
            fn_EmptyMe()
            Exit Sub
        End If
        Dim oSuratKeteranganSakit As New Digital.clsSuratKeteranganSakit

        Dim dsSurat = oSuratKeteranganSakit.GetData(txtKDPENDAFTARAN.Text)
        If dsSurat Is Nothing Then
            Dim frmSuratKeteranganSakit As New frmSuratKeteranganSakit
            Try
                frmSuratKeteranganSakit.LoadMe(FORM_MODE.FORM_MODE_ADD, txtKDPENDAFTARAN.Text, deTANGGALDATANG.DateTime, txtKDCUSTOMER.Text, txtNAMAPASIEN.Text, txtOBJEKTIF_UMUR.Text, sALAMATPASIEN, grdDPJP.EditValue)
                frmSuratKeteranganSakit.ShowDialog(Me)
            Catch ex As Exception
                MsgBox("Load Form Detail : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
            Finally
                If Not frmSuratKeteranganSakit Is Nothing Then frmSuratKeteranganSakit.Dispose()
                frmSuratKeteranganSakit = Nothing
            End Try
        Else
            Dim frmSuratKeteranganSakit As New frmSuratKeteranganSakit
            Try
                frmSuratKeteranganSakit.LoadMe(FORM_MODE.FORM_MODE_EDIT, txtKDPENDAFTARAN.Text, deTANGGALDATANG.DateTime, txtKDCUSTOMER.Text, txtNAMAPASIEN.Text, txtOBJEKTIF_UMUR.Text, sALAMATPASIEN, grdDPJP.EditValue)
                frmSuratKeteranganSakit.ShowDialog(Me)
            Catch ex As Exception
                MsgBox("Load Form Detail : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
            Finally
                If Not frmSuratKeteranganSakit Is Nothing Then frmSuratKeteranganSakit.Dispose()
                frmSuratKeteranganSakit = Nothing
            End Try
        End If
    End Sub
    Private Sub btnSuratKeteranganSehat_Click(sender As Object, e As EventArgs) Handles btnSuratKeteranganSehat.Click
        If txtKDPENDAFTARAN.Text = "" Then
            MsgBox("Silhakan Pilih Pasien", MsgBoxStyle.Information, Me.Text)
            fn_EmptyMe()
            Exit Sub
        End If
        Dim oSuratKeteranganSehat As New Digital.clsSuratKeteranganSehat

        Dim dsSurat = oSuratKeteranganSehat.GetDataByRegister(txtKDPENDAFTARAN.Text)
        If dsSurat Is Nothing Then
            Dim frmSuratKeteranganSehat As New frmSuratKeteranganSehat
            Try
                frmSuratKeteranganSehat.fn_LoadTTV(txtOBJEKTIF_TINGGIBADAN.Text, txtOBJEKTIF_BERATBADAN.Text, txtOBJEKTIF_TEKANANDARAH.Text, txtOBJEKTIF_NADI.Text, txtOBJEKTIF_RESPIRASI.Text, txtOBJEKTIF_SUHU.Text, "Dalam batas normal")
                frmSuratKeteranganSehat.LoadMe(FORM_MODE.FORM_MODE_ADD, txtKDPENDAFTARAN.Text, deTANGGALDATANG.DateTime, txtKDCUSTOMER.Text, txtNAMAPASIEN.Text, sTEMPATLAHIR, deDATETANGGALLAHIR.DateTime, txtOBJEKTIF_JENISKELAMIN.Text, sALAMATPASIEN, grdDPJP.EditValue, "")
                frmSuratKeteranganSehat.ShowDialog(Me)
            Catch ex As Exception
                MsgBox("Load Form Detail : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
            Finally
                If Not frmSuratKeteranganSehat Is Nothing Then frmSuratKeteranganSehat.Dispose()
                frmSuratKeteranganSehat = Nothing
            End Try
        Else
            Dim frmSuratKeteranganSehat As New frmSuratKeteranganSehat
            Try
                frmSuratKeteranganSehat.LoadMe(FORM_MODE.FORM_MODE_EDIT, txtKDPENDAFTARAN.Text, deTANGGALDATANG.DateTime, txtKDCUSTOMER.Text, txtNAMAPASIEN.Text, sTEMPATLAHIR, deDATETANGGALLAHIR.DateTime, txtOBJEKTIF_JENISKELAMIN.Text, sALAMATPASIEN, grdDPJP.EditValue, dsSurat.NOMORSURAT)
                frmSuratKeteranganSehat.ShowDialog(Me)
            Catch ex As Exception
                MsgBox("Load Form Detail : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
            Finally
                If Not frmSuratKeteranganSehat Is Nothing Then frmSuratKeteranganSehat.Dispose()
                frmSuratKeteranganSehat = Nothing
            End Try
        End If
    End Sub
    Private Sub FormulirMPPToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles FormulirMPPToolStripMenuItem.Click
        If txtKDPENDAFTARAN.Text = "" Then
            MsgBox("Silhakan Pilih Pasien", MsgBoxStyle.Information, Me.Text)
            fn_EmptyMe()
            Exit Sub
        End If

        Dim frmDigital_MPPAList As New frmDigital_MPPAList
        Try
            frmDigital_MPPAList.fn_LoadData(txtKDPENDAFTARAN.Text, grdKDDEPARTMENT.Text, txtKDCUSTOMER.Text, txtNAMAPASIEN.Text, txtOBJEKTIF_JENISKELAMIN.Text, deDATETANGGALLAHIR.DateTime)
            frmDigital_MPPAList.ShowDialog(Me)
        Catch ex As Exception
            MsgBox("Load Form Detail : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
        Finally
            If Not frmDigital_MPPAList Is Nothing Then frmDigital_MPPAList.Dispose()
            frmDigital_MPPAList = Nothing
        End Try
    End Sub
    Private Sub MedicalCheckUpToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles MedicalCheckUpToolStripMenuItem.Click
        If txtKDPENDAFTARAN.Text = "" Then
            MsgBox("Silhakan Pilih Pasien", MsgBoxStyle.Information, Me.Text)
            fn_EmptyMe()
            Exit Sub
        End If
        Dim oMCU As New Digital.clsDigital_MedicalChekUp

        Dim dsSurat = oMCU.GetData(txtKDPENDAFTARAN.Text)
        If dsSurat Is Nothing Then
            Dim frmMedicalCheckUP As New frmMedicalCheckUP
            Try
                frmMedicalCheckUP.fn_LoadDataMaster(deDATE.DateTime.ToString("dd-MM-yyyy"), txtKDCUSTOMER.Text, txtNAMAPASIEN.Text, txtKDPENDAFTARAN.Text, grdDPJP.EditValue, deDATETANGGALLAHIR.DateTime.ToString("dd-MM-yyyy"), txtOBJEKTIF_JENISKELAMIN.Text, txtOBJEKTIF_UMUR.Text)
                frmMedicalCheckUP.LoadMe(FORM_MODE.FORM_MODE_ADD)
                frmMedicalCheckUP.ShowDialog(Me)
            Catch ex As Exception
                MsgBox("Load Form Detail : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
            Finally
                If Not frmMedicalCheckUP Is Nothing Then frmMedicalCheckUP.Dispose()
                frmMedicalCheckUP = Nothing
            End Try
        Else
            Dim frmMedicalCheckUP As New frmMedicalCheckUP
            Try
                frmMedicalCheckUP.fn_LoadDataMaster(deDATE.DateTime.ToString("dd-MM-yyyy"), txtKDCUSTOMER.Text, txtNAMAPASIEN.Text, txtKDPENDAFTARAN.Text, grdDPJP.EditValue, deDATETANGGALLAHIR.DateTime.ToString("dd-MM-yyyy"), txtOBJEKTIF_JENISKELAMIN.Text, txtOBJEKTIF_UMUR.Text)
                frmMedicalCheckUP.LoadMe(FORM_MODE.FORM_MODE_EDIT, dsSurat.KDMEDICALCHECKUP)
                frmMedicalCheckUP.ShowDialog(Me)
            Catch ex As Exception
                MsgBox("Load Form Detail : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
            Finally
                If Not frmMedicalCheckUP Is Nothing Then frmMedicalCheckUP.Dispose()
                frmMedicalCheckUP = Nothing
            End Try
        End If
    End Sub
End Class