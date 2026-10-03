Imports DataAccess
Imports System.Linq
Imports System.Data.SqlClient
Imports Newtonsoft.Json.Linq
Imports System.IO
Imports iTextSharp.text
Imports iTextSharp.text.pdf
Imports DevExpress.XtraSplashScreen

Public Class frmMedrekRawatNewJalanList
    Private isLoad As Boolean = False
    Private sisDOkter As Boolean = False
    Private isLoad_Splash As Boolean = False
    Private sJenisRawat As Integer = 0

#Region "Function"
    Public Sub fn_LoadMe(ByVal isDOKTER As Boolean, ByVal isJenisrawat As Integer)
        sisDOkter = isDOKTER
        sJenisRawat = isJenisrawat
    End Sub
    Private Sub Me_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        deDATE.DateTime = Now
        fn_LoadDEPARTMENT()
        fn_LoadRuangan()
        fn_LoadKDUSER()

        tabbedControlGroup1.SelectedTabPageIndex = 0

        If sJenisRawat = 0 Then
            lRI.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        Else
            If sisDOkter = False Then
                lRJ_3.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                lRI.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            Else
                lRJ_3.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                lRI.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            End If

            lRJ_1.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            lRJ_4.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            lRJ_BukaSelesai.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            lRJ_2.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            lRJ_6.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        End If

        Dim oDoctor As New Reference.clsDoctor
        Dim dsDepartment = oDoctor.GetDataByIDUser(sUserID)
        If dsDepartment IsNot Nothing Then
            grdKDDEPARTMENT.EditValue = dsDepartment.KDDEPARTMENT
            fn_LoadDokter(dsDepartment.KDDOCTOR)
        Else
            If sJenisRawat = 1 Then
                fn_LoadDokter("")
            End If
        End If

        fn_LoadSecurity()

        XtraTabControl1.SelectedTabPageIndex = 0
        isLoad = True
        isLoad_Splash = True

        If sAlamatSimpanPDFAsesmenIGD <> "" Then
            Try
                If Not Directory.Exists(sAlamatSimpanPDFAsesmenIGD) Then
                    Directory.CreateDirectory(sAlamatSimpanPDFAsesmenIGD)
                End If
            Catch oErr As Exception
                MsgBox("Alamat Simpan PDF" & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
                sAlamatSimpanPDFAsesmenIGD = ""
            End Try
        End If
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
                'picAdd.Enabled = ds.ISADD
                'picDelete.Enabled = ds.ISDELETE
                'picUpdate.Enabled = ds.ISUPDATE
                'picPrint.Enabled = ds.ISPRINT
                'picRefresh.Enabled = ds.ISVIEW

                If ds.ISVIEW = True Then
                    fn_LoadView()
                    fn_LoadData()
                End If

            Catch ex As Exception
                MsgBox("Keamanan belum dipasang, tolong kontak admin anda!", MsgBoxStyle.Exclamation, Me.Text)
                picRefreshListPasien.Enabled = False

                'picAdd.Enabled = False
                'picDelete.Enabled = False
                'picUpdate.Enabled = False
                'picPrint.Enabled = False
                'picRefresh.Enabled = False
            End Try
        Catch ex As Exception
            MsgBox("Load Security : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadView()
        fn_EmptyMe()

        TindakLanjutToolStripMenuItem.Visible = False

        If sisDOkter = False Then
            lblPERAWAT.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        Else
            lblPERAWAT.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        End If

        If sJenisRawat = 0 Then
            'NaikRanapToolStripMenuItem.Visible = False

            Dim oDepartment As New Reference.clsDepartment
            Dim dsDepartment1 = oDepartment.GetData(grdKDDEPARTMENT.EditValue)

            If dsDepartment1 IsNot Nothing Then
                If dsDepartment1.KDDEPARTMENT_BPJS = "IGD" Then
                    lRJ_4.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                    lRJ_BukaSelesai.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                    AddToolStripMenuItem.Visible = False
                    PemeriksaanAwalToolStripMenuItem.Visible = False
                    AsesmenAwalKeperawatanRawatJalanToolStripMenuItem.Visible = False

                    If sisDOkter = False Then
                        AsesmenAwalPerawatIGDToolStripMenuItem.Visible = True
                        AsesmenMedisIGDToolStripMenuItem.Visible = False
                        GERDQToolStripMenuItem.Visible = False
                        AsesmenKebidananToolStripMenuItem.Visible = True
                    Else
                        TindakLanjutToolStripMenuItem.Visible = True

                        AsesmenAwalPerawatIGDToolStripMenuItem.Visible = False
                        AsesmenMedisIGDToolStripMenuItem.Visible = True
                        GERDQToolStripMenuItem.Visible = True
                        AsesmenKebidananToolStripMenuItem.Visible = False
                    End If
                Else
                    lRJ_4.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                    lRJ_BukaSelesai.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                    AsesmenAwalPerawatIGDToolStripMenuItem.Visible = False
                    AsesmenMedisIGDToolStripMenuItem.Visible = False
                    GERDQToolStripMenuItem.Visible = False
                    AsesmenKebidananToolStripMenuItem.Visible = False

                    If sisDOkter = False Then
                        AddToolStripMenuItem.Visible = False
                        PemeriksaanAwalToolStripMenuItem.Visible = True
                        AsesmenAwalKeperawatanRawatJalanToolStripMenuItem.Visible = True
                    Else
                        AddToolStripMenuItem.Visible = True
                        PemeriksaanAwalToolStripMenuItem.Visible = False
                        AsesmenAwalKeperawatanRawatJalanToolStripMenuItem.Visible = False
                    End If
                End If
            End If
        Else
            'NaikRanapToolStripMenuItem.Visible = True

            AddToolStripMenuItem.Visible = False
            PemeriksaanAwalToolStripMenuItem.Visible = False
            AsesmenMedisIGDToolStripMenuItem.Visible = False
            AsesmenAwalKeperawatanRawatJalanToolStripMenuItem.Visible = False
            AsesmenAwalPerawatIGDToolStripMenuItem.Visible = False
            GERDQToolStripMenuItem.Visible = False
            AsesmenKebidananToolStripMenuItem.Visible = False
        End If

        If sisDOkter = False Then
            Me.Text = IIf(sJenisRawat = 0, "List Rawat Jalan ", "List Rawat Inap ") & "Perawat/Bidan"
        Else
            Me.Text = IIf(sJenisRawat = 0, "List Rawat Jalan ", "List Rawat Inap ") & "Dokter"
        End If
    End Sub
    Private Sub fn_EmptyMe()
        lblNamaPasien.Text = "Nul"
        lblTanggalLahirUsia.Text = "Nul"
        lblRM.Text = "Nul"
        lblRegister.Text = "Nul"
        lblDPJP.Text = "Nul"
        lblPenjamin.Text = "Nul"
        lblSIP.Text = "Nul"
        lblNOMORSEP.Text = "Nul"
        deDATELAHIR.DateTime = Now
        lblJK.Text = "Nul"
        lblNOKARTUBPJS.Text = "Nul"
        lblKodeBooking.Text = "Nul"
        lblKTP.Text = "Nul"
        lblSelesai.Text = "Nul"
        lblKDDOCTOR.Text = "Nul"
        txtTB.Text = "Nul"
        txtBERATBADAN.Text = "Nul"
        txtDIAGNOSAUTAMA.Text = "Nul"
        txtALERGIOBAT.Text = "Nul"
        lblKamar.Text = "Nul"
        lblKDKELASRAWAT.Text = "Nul"

        grvHystori.OptionsSelection.MultiSelect = True
        grvHystori.SelectAll()
        grvHystori.DeleteSelectedRows()
        grvHystori.OptionsSelection.MultiSelect = False

        grvUpload.OptionsSelection.MultiSelect = True
        grvUpload.SelectAll()
        grvUpload.DeleteSelectedRows()
        grvUpload.OptionsSelection.MultiSelect = False

        PdfViewerAskepRawatJalan.CloseDocument()
        PdfViewerCPPTRawatJalan.CloseDocument()
        PdfViewerResume.CloseDocument()
        PdfViewer_IGD.CloseDocument()
        PdfViewer1Vonek.CloseDocument()
        pdfGERD.CloseDocument()
        PdfViewerPerawatIGD.CloseDocument()
        PdfViewerHadnOver.CloseDocument()
        PdfViewerSBAR.CloseDocument()
        PdfViewerCPPT_RI.CloseDocument()
        PdfViewerRekonsiliasiObat.CloseDocument()
    End Sub
    Private Sub grvListPasien_FocusedRowChanged(ByVal sender As System.Object, ByVal e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs) Handles grvListPasien.FocusedRowChanged
        If grvListPasien.GetFocusedRowCellValue("NoRegister") Is Nothing Then
            fn_EmptyMe()
            Exit Sub
        End If

        lblKDKELASRAWAT.Text = grvListPasien.GetFocusedRowCellValue("KDKELASRAWAT_NAME")
        lblNamaPasien.Text = grvListPasien.GetFocusedRowCellValue("Pasien")
        lblRM.Text = grvListPasien.GetFocusedRowCellValue("KDCUSTOMER")
        lblTanggalLahirUsia.Text = grvListPasien.GetFocusedRowCellValue("USIA")
        deDATELAHIR.DateTime = grvListPasien.GetFocusedRowCellValue("TANGGALLAHIR")
        txtDIAGNOSAUTAMA.Text = grvListPasien.GetFocusedRowCellValue("DIAGNOSA")
        deDATE_MASUK.DateTime = CDate(grvListPasien.GetFocusedRowCellValue("DATE"))
        lblRegister.Text = grvListPasien.GetFocusedRowCellValue("NoRegister")
        lblPenjamin.Text = grvListPasien.GetFocusedRowCellValue("PENJAMIN")
        lblDPJP.Text = grvListPasien.GetFocusedRowCellValue("Dokter")
        lblKDDOCTOR.Text = grvListPasien.GetFocusedRowCellValue("KDDOCTOR")
        lblSIP.Text = grvListPasien.GetFocusedRowCellValue("SIP")
        lblNOMORSEP.Text = grvListPasien.GetFocusedRowCellValue("NOMORSEP")
        lblJK.Text = IIf(grvListPasien.GetFocusedRowCellValue("JK") = "1", "L", "P")
        lblNOKARTUBPJS.Text = grvListPasien.GetFocusedRowCellValue("KARTUBPJS")
        lblKodeBooking.Text = grvListPasien.GetFocusedRowCellValue("Antrian")
        lblKTP.Text = grvListPasien.GetFocusedRowCellValue("NIK")
        lblSelesai.Text = grvListPasien.GetFocusedRowCellValue("Selesai")
        lblKamar.Text = grvListPasien.GetFocusedRowCellValue("NamaKamar")

        Dim oReqAwalPemeriksaan As New Transaksi.clsReqAwalPemeriksaan
        Dim dsReqAwalPemeriksaan = oReqAwalPemeriksaan.GetData(lblRegister.Text)
        If dsReqAwalPemeriksaan IsNot Nothing Then
            txtBERATBADAN.Text = dsReqAwalPemeriksaan.OBJEKTIF_BERATBADAN
            txtTB.Text = dsReqAwalPemeriksaan.OBJEKTIF_BERATBADAN
        Else
            Dim oCustomer_Detil As New Reference.clsCustomerDetil
            Dim dsCustomer_Detil = oCustomer_Detil.GetData(lblRM.Text)
            If dsCustomer_Detil IsNot Nothing Then
                txtBERATBADAN.Text = dsCustomer_Detil.BB
                txtTB.Text = dsCustomer_Detil.TT
                txtALERGIOBAT.Text = dsCustomer_Detil.ALERGI
            Else
                txtBERATBADAN.Text = 0
                txtTB.Text = 0
            End If
        End If

        Dim oCustomer_Detil_ As New Reference.clsCustomerDetil
        Dim dsCustomer_Detil_ = oCustomer_Detil_.GetData(lblRM.Text)
        If dsCustomer_Detil_ IsNot Nothing Then
            txtALERGIOBAT.Text = dsCustomer_Detil_.ALERGI
        End If

        If sisDOkter = True Then
            If grdKDDEPARTMENT.Text = "IGD" Then
                fn_SaveObatYangdiInputFaramsi(lblRegister.Text)
            End If
        End If

        tabbedControlGroup1_SelectedPageChanged()

        If sisDOkter = True Then
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
                            If fn_subcek(.Rows(iLoop)("NOMORANTRIAN"), 4) = False Then
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
    End Sub
    Private Sub fn_LoadData()
        Try
            If isLoad_Splash = False Then Exit Sub

            If grdKDDEPARTMENT.Text = "IGD" Then
                lWaktuIGD.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            Else
                lWaktuIGD.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            End If

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

            If sJenisRawat = 0 Then
                If grdDPJP.Text = "" Then Exit Sub
                If grdKDDEPARTMENT.Text = "" Then Exit Sub

                SQL = "SELECT "
                SQL &= "* "
                SQL &= "FROM ( "
                SQL &= "SELECT "
                SQL &= "NoRegister = B.KDREG "
                SQL &= ",AntrianDokter = (SELECT CASE B.ISRESUME WHEN 0 THEN (SELECT CASE B.ANTRIANDOKTER WHEN 0 THEN 'X' ELSE 'A' + RIGHT('000' + CONVERT(NVARCHAR(50), B.ANTRIANDOKTER), 3) END) ELSE 'Y' END) "
                SQL &= ",Antrian = B.NOMORANTRIAN "
                SQL &= ",Pasien = D.NAME_DISPLAY + ' ' + IIf(D.FRONT_TITLE IS NULL, '', D.FRONT_TITLE + ' ') + D.BACK_TITLE "
                SQL &= ",Dokter = C.NAME_DISPLAY "
                SQL &= ",SIP = C.SIP "
                SQL &= ",B.KDCUSTOMER "
                SQL &= ",B.USIA "
                SQL &= ",D.TANGGALLAHIR "
                SQL &= ",DIAGNOSA = E.KDDIAGNOSA + ' - ' + E.DESCRIPTION "
                SQL &= ",B.DATE "
                If grdKDDEPARTMENT.Text = "IGD" Then
                    'SQL &= ",WaktuIGD = ISNULL((SELECT TOP 1 C FROM F_CASHIER_H WHERE B.KDREG = KDREG), ISNULL((SELECT TOP 1 CONCAT(DATEDIFF(minute, B.DATE, BBB.DATE) / 60, ' jam ', DATEDIFF(minute, B.DATE, BBB.DATE) % 60, ' menit') FROM RSKC_NEW..S_REQ_CPPT BBB WHERE BBB.KDCUSTOMER = B.KDCUSTOMER COLLATE SQL_Latin1_General_CP1_CI_AS AND BBB.PROFESI <> 'DOKTER' COLLATE SQL_Latin1_General_CP1_CI_AS ORDER BY BBB.DATE ASC), '-')) "
                    SQL &= ",WaktuIGD = ISNULL((SELECT CONCAT(DATEDIFF(minute, B.DATE, WAKTU) / 60, ' jam ', DATEDIFF(minute, B.DATE, WAKTU) % 60, ' menit') FROM S_PENDAFTARAN_WAKTUIGD WHERE B.KDREG = KDREG), '-') "
                    SQL &= ",WaktuSelisih = ISNULL((SELECT DATEDIFF(MINUTE, B.DATE, WAKTU) FROM S_PENDAFTARAN_WAKTUIGD WHERE B.KDREG = KDREG), '') "
                Else
                    SQL &= ",WaktuIGD = '-' "
                    SQL &= ",WaktuSelisih = '' "
                End If
                SQL &= ",PENJAMIN = F.NAME_DISPLAY "
                SQL &= ",B.NOMORSEP "
                SQL &= ",B.KARTUBPJS "
                SQL &= ",Cek = B.ISRESUME "
                SQL &= ",C.KDDOCTOR "
                SQL &= ",D.JK "
                SQL &= ",B.ISPERAWAT "
                SQL &= ",Keterangan = B.KETERANGAN_TINDAKLANJUT "
                SQL &= ",D.NIK "
                'SQL &= ",Selesai = ISNULL((SELECT CASE KETERANGAN WHEN '' THEN 'Y' ELSE 'X' END FROM S_SELESAI WHERE B.KDREG = KDPENDAFTARAN), 'N') "
                SQL &= ",Selesai = ISNULL((SELECT KETERANGAN FROM S_SELESAI WHERE B.KDREG = KDPENDAFTARAN), '') "
                SQL &= ",NamaKamar = ''  "
                If sUserID = "RSKC" Then
                    SQL &= ",Bed = '' "
                Else
                    SQL &= ",Bed = ISNULL((SELECT STRING_AGG(KET_1,',') FROM S_PENDAFTARAN_BED WHERE B.KDREG = KET_2 GROUP BY KET_2), '') "
                End If
                SQL &= ",KDKELASRAWAT_NAME = ISNULL((SELECT DESCRIPTION FROM M_KELASRAWAT WHERE B.KDKELASRAWAT = KDKELASRAWAT), '') "
                'SQL &= ",WaktuIGD =  "
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
                'SQL &= "AND B.KDDEPARTMENT = " & grdKDDEPARTMENT.EditValue & " "
                'SQL &= "AND B.KDDOCTOR = " & grdDPJP.EditValue & " "
                'SQL &= "AND B.STATUSDAFTAR <> '3' "
                'SQL &= "AND B.CATEGORY = 1 "

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
                SQL &= ") Z "
                SQL &= "ORDER BY Z.ANTRIANDOKTER, Z.NoRegister ASC "
            Else
                If sisDOkter = False Then
                    If grdKAMAR.Text = "" Then Exit Sub
                Else
                    If grdDPJP.Text = "" Then Exit Sub
                End If
                SQL = "SELECT "
                SQL &= "NoRegister = D.KDREG "
                SQL &= ",NamaKamar = A.LOKASI + '-' + A.NAME_DISPLAY  "
                SQL &= ",KelasRawat = B.NAME_DISPLAY "
                SQL &= ",NomorBed = C.SEQ "
                SQL &= ",AntrianDokter = (SELECT CASE D.ISRESUME WHEN 0 THEN (SELECT CASE D.ANTRIANDOKTER WHEN 0 THEN 'X' ELSE 'A' + RIGHT('000' + CONVERT(NVARCHAR(50), D.ANTRIANDOKTER), 3) END) ELSE 'Y' END) "
                SQL &= ",Antrian = D.NOMORANTRIAN "
                SQL &= ",Pasien = F.NAME_DISPLAY + ' ' + IIf(F.FRONT_TITLE IS NULL, '', F.FRONT_TITLE + ' ') + F.BACK_TITLE "
                SQL &= ",Dokter = E.NAME_DISPLAY "
                SQL &= ",SIP = E.SIP "
                SQL &= ",D.KDCUSTOMER "
                SQL &= ",D.USIA "
                SQL &= ",F.TANGGALLAHIR "
                SQL &= ",DIAGNOSA = G.KDDIAGNOSA + ' - ' + G.DESCRIPTION "
                SQL &= ",D.DATE "
                SQL &= ",PENJAMIN = H.NAME_DISPLAY "
                SQL &= ",D.NOMORSEP "
                SQL &= ",D.KARTUBPJS "
                SQL &= ",Cek = D.ISRESUME "
                SQL &= ",E.KDDOCTOR "
                SQL &= ",F.JK "
                SQL &= ",D.ISPERAWAT "
                SQL &= ",Keterangan = 'UTAMA' "
                SQL &= ",D.NIK "
                If sisDOkter = False Then
                    SQL &= ",Selesai = ISNULL((SELECT 'Y' FROM I_TRACKING_CPPT WHERE KDDOCTOR = E.KDDOCTOR AND D.KDREG = KDREG AND TANGGAL = '" & Now.ToString("yyyyMMdd") & "' AND DESCRIPTION <> 'DOKTER'), 'N') "
                Else
                    SQL &= ",Selesai = ISNULL((SELECT 'Y' FROM I_TRACKING_CPPT WHERE KDDOCTOR = E.KDDOCTOR AND D.KDREG = KDREG AND TANGGAL = '" & Now.ToString("yyyyMMdd") & "' AND DESCRIPTION = 'DOKTER'), 'N') "
                End If
                SQL &= ",KDKELASRAWAT_NAME = ISNULL((SELECT DESCRIPTION FROM M_KELASRAWAT WHERE D.KDKELASRAWAT = KDKELASRAWAT), '') "
                SQL &= "FROM "
                SQL &= "M_MEDICAL_ROOM A "
                SQL &= "INNER JOIN M_TIPE_KAMAR B "
                SQL &= "ON A.KDKAMAR = B.KDKAMAR "
                SQL &= "INNER JOIN M_MEDICAL_ROOM_DETIL C "
                SQL &= "ON A.KDMROOM = C.KDMROOM "
                SQL &= "INNER JOIN S_PENDAFTARAN_H D "
                SQL &= "ON C.KDREG = D.KDREG "
                SQL &= "INNER JOIN M_DOCTOR E "
                SQL &= "ON D.KDDOCTOR = E.KDDOCTOR "
                SQL &= "INNER JOIN M_CUSTOMER F "
                SQL &= "ON D.KDCUSTOMER = F.KDCUSTOMER "
                SQL &= "INNER JOIN M_DIAGNOSAICD10 G "
                SQL &= "ON D.KDDIAGNOSA = G.KDDIAGNOSA "
                SQL &= "INNER JOIN M_DEBTOR H "
                SQL &= "ON D.KDDEBTOR = H.KDDEBTOR "
                SQL &= "WHERE A.ISACTIVE = 1 "
                If sisDOkter = False Then
                    SQL &= "AND A.KDMROOM = " & grdKAMAR.EditValue & " "
                Else
                    SQL &= "AND E.KDDOCTOR = " & grdDPJP.EditValue & " "
                End If


                SQL &= "UNION "

                SQL &= "SELECT "
                SQL &= "NoRegister = D.KDREG "
                SQL &= ",NamaKamar = A.LOKASI + '-' + A.NAME_DISPLAY  "
                SQL &= ",KelasRawat = B.NAME_DISPLAY "
                SQL &= ",NomorBed = C.SEQ "
                SQL &= ",AntrianDokter = (SELECT CASE D.ISRESUME WHEN 0 THEN (SELECT CASE D.ANTRIANDOKTER WHEN 0 THEN 'X' ELSE 'A' + RIGHT('000' + CONVERT(NVARCHAR(50), D.ANTRIANDOKTER), 3) END) ELSE 'Y' END) "
                SQL &= ",Antrian = D.NOMORANTRIAN "
                SQL &= ",Pasien = F.NAME_DISPLAY + ' ' + IIf(F.FRONT_TITLE IS NULL, '', F.FRONT_TITLE + ' ') + F.BACK_TITLE "
                SQL &= ",Dokter = J.NAME_DISPLAY "
                SQL &= ",SIP = J.SIP "
                SQL &= ",D.KDCUSTOMER "
                SQL &= ",D.USIA "
                SQL &= ",F.TANGGALLAHIR "
                SQL &= ",DIAGNOSA = G.KDDIAGNOSA + ' - ' + G.DESCRIPTION "
                SQL &= ",D.DATE "
                SQL &= ",PENJAMIN = H.NAME_DISPLAY "
                SQL &= ",D.NOMORSEP "
                SQL &= ",D.KARTUBPJS "
                SQL &= ",Cek = D.ISRESUME "
                SQL &= ",J.KDDOCTOR "
                SQL &= ",F.JK "
                SQL &= ",D.ISPERAWAT "
                SQL &= ",Keterangan = 'PENDAMPING' "
                SQL &= ",D.NIK "
                If sisDOkter = False Then
                    SQL &= ",Selesai = ISNULL((SELECT 'Y' FROM I_TRACKING_CPPT WHERE KDDOCTOR = J.KDDOCTOR AND D.KDREG = KDREG AND TANGGAL = '" & Now.ToString("yyyyMMdd") & "' AND DESCRIPTION <> 'DOKTER'), 'N') "
                Else
                    SQL &= ",Selesai = ISNULL((SELECT 'Y' FROM I_TRACKING_CPPT WHERE KDDOCTOR = J.KDDOCTOR AND D.KDREG = KDREG AND TANGGAL = '" & Now.ToString("yyyyMMdd") & "' AND DESCRIPTION = 'DOKTER'), 'N') "
                End If
                SQL &= ",KDKELASRAWAT_NAME = ISNULL((SELECT DESCRIPTION FROM M_KELASRAWAT WHERE D.KDKELASRAWAT = KDKELASRAWAT), '') "
                SQL &= "FROM "
                SQL &= "M_MEDICAL_ROOM A "
                SQL &= "INNER JOIN M_TIPE_KAMAR B "
                SQL &= "ON A.KDKAMAR = B.KDKAMAR "
                SQL &= "INNER JOIN M_MEDICAL_ROOM_DETIL C "
                SQL &= "ON A.KDMROOM = C.KDMROOM "
                SQL &= "INNER JOIN S_PENDAFTARAN_H D "
                SQL &= "ON C.KDREG = D.KDREG "
                SQL &= "INNER JOIN M_CUSTOMER F "
                SQL &= "ON D.KDCUSTOMER = F.KDCUSTOMER "
                SQL &= "INNER JOIN M_DIAGNOSAICD10 G "
                SQL &= "ON D.KDDIAGNOSA = G.KDDIAGNOSA "
                SQL &= "INNER JOIN M_DEBTOR H "
                SQL &= "ON D.KDDEBTOR = H.KDDEBTOR "
                SQL &= "INNER JOIN I_TRACKING_KDDOCTOR I "
                SQL &= "ON D.KDREG = I.KDREG "
                SQL &= "INNER JOIN M_DOCTOR J "
                SQL &= "ON I.KDDOCTOR = J.KDDOCTOR "
                SQL &= "WHERE A.ISACTIVE = 1 "
                If sisDOkter = False Then
                    SQL &= "AND A.KDMROOM = " & grdKAMAR.EditValue & " "
                Else
                    SQL &= "AND J.KDDOCTOR = " & grdDPJP.EditValue & " "
                End If

                'SQL &= "ORDER BY A.KDMROOM, C.SEQ ASC "
            End If

            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "S_LISTPASIEN")

            If oConn.State = ConnectionState.Open Then
                oConn.Close()
            End If

            grdListPasien.DataSource = ds.Tables("S_LISTPASIEN")
            grdListPasien.ForceInitialize()

            fn_LoadFormatData()
            'grvListPasien.BestFitColumns()

            Rata()

            SplashScreenManager.CloseForm(False)
        Catch oErr As Exception
            SplashScreenManager.CloseForm(False)
            MsgBox("Load List Pasien Data : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub Rata()
        Try
            If grdKDDEPARTMENT.Text <> "IGD" Then
                txtLayananRataRata.ResetText()
                Exit Sub
            End If

            Dim totalMenit As Integer = 0
            Dim jumlahBaris As Integer = 0

            For i As Integer = 0 To grvListPasien.RowCount - 1
                If grvListPasien.GetRowCellValue(i, "WaktuSelisih") <> "0" Then
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

            If rataRata >= 120 Then
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
    Private Sub fn_LoadFormatData()
        grvListPasien.Columns("KDKELASRAWAT_NAME").VisibleIndex = -1
        grvListPasien.Columns("Antrian").VisibleIndex = -1
        grvListPasien.Columns("NoRegister").VisibleIndex = -1
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
        grvListPasien.Columns("NIK").VisibleIndex = -1
        grvListPasien.Columns("Selesai").VisibleIndex = -1

        If grdKDDEPARTMENT.Text = "IGD" Then
            grvListPasien.Columns("AntrianDokter").VisibleIndex = -1
            grvListPasien.Columns("Bed").VisibleIndex = 4
        Else
            grvListPasien.Columns("Bed").VisibleIndex = -1
        End If
        If sJenisRawat = 0 Then
            grvListPasien.Columns("Dokter").VisibleIndex = -1
            grvListPasien.Columns("NamaKamar").VisibleIndex = -1
            grvListPasien.Columns("WaktuSelisih").VisibleIndex = -1

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

            grvListPasien.Columns("PENJAMIN").Caption = "Penjamin"
            grvListPasien.Columns("DATE").Caption = "Jam Pendaftaran"
        Else
            If sisDOkter = True Then
                grvListPasien.Columns("Dokter").VisibleIndex = -1
            Else
                grvListPasien.Columns("NamaKamar").VisibleIndex = -1
            End If

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
                    grvListPasien.Columns(iLoop).DisplayFormat.FormatString = "{0:dd/MM/yyyy}"
                End If
            Next

            grvListPasien.Columns("PENJAMIN").Caption = "Penjamin"
            grvListPasien.Columns("DATE").Caption = "Tanggal Daftar"
            grvListPasien.Columns("AntrianDokter").VisibleIndex = -1
        End If
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


            BindingSource.DataSource = ds.Tables("S_HISTORY")

            grdHystori.DataSource = BindingSource
            grdHystori.ForceInitialize()

            If oConn.State = ConnectionState.Open Then
                oConn.Close()
            End If

            fn_SetFormatHystori()

            grvHystori.BestFitColumns()

        Catch oErr As Exception
            MsgBox("Load History Data Obat Data : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_DokterBersama(ByVal kdreg As String)
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
            SQL &= "NAMADOKTER = B.NAME_DISPLAY "
            SQL &= ",KETERANGAN = 'UTAMA' "
            SQL &= "FROM "
            SQL &= "S_PENDAFTARAN_H A "
            SQL &= "INNER JOIN M_DOCTOR B "
            SQL &= "ON A.KDDOCTOR = B.KDDOCTOR "
            SQL &= "WHERE "
            SQL &= "A.KDREG = '" & kdreg & "' "

            SQL &= "UNION "

            SQL &= "SELECT "
            SQL &= "NAMADOKTER = B.NAME_DISPLAY "
            SQL &= ",KETERANGAN = 'PENDAMPING' "
            SQL &= "FROM "
            SQL &= "I_TRACKING_KDDOCTOR A "
            SQL &= "INNER JOIN M_DOCTOR B "
            SQL &= "ON A.KDDOCTOR = B.KDDOCTOR "
            SQL &= "WHERE "
            SQL &= "A.KDREG = '" & kdreg & "' "

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
    Private Sub fn_SetFormatHystori()
        For iLoop As Integer = 0 To grvHystori.Columns.Count - 1
            If grvHystori.Columns(iLoop).ColumnType.Name = "Decimal" Then
                grvHystori.Columns(iLoop).DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                grvHystori.Columns(iLoop).DisplayFormat.FormatString = "{0:n2}"
                grvHystori.Columns(iLoop).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
                'grvHystori.GroupSummary.Add(DevExpress.Data.SummaryItemType.Sum, grvHystori.Columns(iLoop).FieldName, grvHystori.Columns(iLoop),
                '                     "{0:n2}")
                'grvHystori.Columns(iLoop).SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                'grvHystori.Columns(iLoop).SummaryItem.DisplayFormat = "{0:n2}"
            ElseIf grvHystori.Columns(iLoop).ColumnType.Name = "DateTime" Then
                grvHystori.Columns(iLoop).DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
                grvHystori.Columns(iLoop).DisplayFormat.FormatString = "{0:dd/MM/yyyy HH:mm:ss}"
            End If
        Next


        grvHystori.Columns("NoRegister").VisibleIndex = -1
        grvHystori.Columns("NoTransaksi").VisibleIndex = -1
        grvHystori.Columns("KDITEM").VisibleIndex = -1
        grvHystori.Columns("KDUOM").VisibleIndex = -1
        'grvHystori.Columns("SATUAN").VisibleIndex = -1
        'grvHystori.Columns("JUMLAH").VisibleIndex = -1
        'grvHystori.Columns("CARAPAKAI").VisibleIndex = -1
        'grvHystori.Columns("KET1").VisibleIndex = -1
        'grvHystori.Columns("KET2").VisibleIndex = -1

        grvHystori.Columns("Kategori").Group()
        grvHystori.Columns("Tipe").Group()
        grvHystori.ExpandAllGroups()
    End Sub
    Private Sub fn_LoadDataPenunjang()
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
            SQL &= "WHERE B.KDCUSTOMER = '" & lblRM.Text & "' "
            SQL &= "AND D.DESCRIPTION <> 'ADMINISTRASI' "

            SQL &= "UNION "

            SQL &= "SELECT "
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
            SQL &= "WHERE B.KDCUSTOMER = '" & lblRM.Text & "' "

            SQL &= "UNION "

            SQL &= "SELECT "
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
            SQL &= "WHERE B.KDCUSTOMER = '" & lblRM.Text & "' "

            SQL &= "UNION "

            SQL &= "SELECT "
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
            SQL &= "WHERE B.KDCUSTOMER = '" & lblRM.Text & "' "

            SQL &= "UNION "

            SQL &= "SELECT "
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
            SQL &= "WHERE D.KDCUSTOMER = '" & lblRM.Text & "' "

            SQL &= "UNION "

            SQL &= "SELECT "
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
            SQL &= "WHERE D.KDCUSTOMER = '" & lblRM.Text & "' "

            SQL &= "UNION "

            SQL &= "SELECT "
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
            SQL &= "WHERE D.KDCUSTOMER = '" & lblRM.Text & "' "

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
            MsgBox("Load Data Penunjang Rawat Jalan : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
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
    Private Sub grvUpload_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles grvUpload.DoubleClick
        If grvUpload.GetFocusedRowCellValue("AlamatUpload") Is Nothing Then
            Exit Sub
        End If

        If lblRM.Text = "" Then
            Exit Sub
        End If

        If grvUpload.GetFocusedRowCellValue("AlamatUpload") Is Nothing Then
            Exit Sub
        End If

        If lblRM.Text = "" Then
            Exit Sub
        End If

        If grvUpload.GetFocusedRowCellValue("Type") = "RONTGEN" Then
            'frmBrowseUpload.fn_LoadMe(2, grvUpload.GetFocusedRowCellValue("Type"), grvUpload.GetFocusedRowCellValue("AlamatUpload"), lblRM.Text, lblNamaPasien.Text, deDATELAHIR.DateTime.ToString("dd-MM-yyyy"), IIf(lblJK.Text = "L", "Laki-laki", "Perempuan"), grvUpload.GetFocusedRowCellValue("Kodeupload"))
            'frmBrowseUpload.Show()
            'frmBrowseUpload.WindowState = FormWindowState.Normal

            Dim frmBrowseUpload As New frmBrowseUpload
            Try
                frmBrowseUpload.WindowState = FormWindowState.Normal
                frmBrowseUpload.fn_LoadMe(2, grvUpload.GetFocusedRowCellValue("Type"), grvUpload.GetFocusedRowCellValue("AlamatUpload"), lblRM.Text, lblNamaPasien.Text, deDATELAHIR.DateTime.ToString("dd-MM-yyyy"), IIf(lblJK.Text = "L", "Laki-laki", "Perempuan"), grvUpload.GetFocusedRowCellValue("Kodeupload"))
                frmBrowseUpload.ShowDialog(Me)
            Catch ex As Exception
                MsgBox("Load Form Detail : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
            Finally
                If Not frmBrowseUpload Is Nothing Then frmBrowseUploadRadiologi.Dispose()
                frmBrowseUpload = Nothing
            End Try
        ElseIf grvUpload.GetFocusedRowCellValue("Type") = "USG" Then
            'frmBrowseUpload.fn_LoadMe(1, grvUpload.GetFocusedRowCellValue("Type"), grvUpload.GetFocusedRowCellValue("AlamatUpload"), lblRM.Text, lblNamaPasien.Text, deDATELAHIR.DateTime.ToString("dd-MM-yyyy"), IIf(lblJK.Text = "L", "Laki-laki", "Perempuan"), grvUpload.GetFocusedRowCellValue("Kodeupload"))
            'frmBrowseUpload.Show()
            'frmBrowseUpload.WindowState = FormWindowState.Normal

            Dim frmBrowseUpload As New frmBrowseUpload
            Try
                frmBrowseUpload.WindowState = FormWindowState.Normal
                frmBrowseUpload.fn_LoadMe(1, grvUpload.GetFocusedRowCellValue("Type"), grvUpload.GetFocusedRowCellValue("AlamatUpload"), lblRM.Text, lblNamaPasien.Text, deDATELAHIR.DateTime.ToString("dd-MM-yyyy"), IIf(lblJK.Text = "L", "Laki-laki", "Perempuan"), grvUpload.GetFocusedRowCellValue("Kodeupload"))
                frmBrowseUpload.ShowDialog(Me)
            Catch ex As Exception
                MsgBox("Load Form Detail : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
            Finally
                If Not frmBrowseUpload Is Nothing Then frmBrowseUploadRadiologi.Dispose()
                frmBrowseUpload = Nothing
            End Try
        ElseIf grvUpload.GetFocusedRowCellValue("Type") = "APIRADIOLOGI" Then
            Dim frmBrowseUploadRadiologi As New frmBrowseUploadRadiologi
            Try
                frmBrowseUploadRadiologi.WindowState = FormWindowState.Normal
                frmBrowseUploadRadiologi.fn_LoadMe(lblRM.Text, lblNamaPasien.Text, deDATELAHIR.DateTime.ToString("dd-MM-yyyy"), IIf(lblJK.Text = "L", "Laki-laki", "Perempuan"), grvUpload.GetFocusedRowCellValue("AlamatUpload"), lblRegister.Text)
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
                frmBrowseUpload.fn_LoadMe(0, grvUpload.GetFocusedRowCellValue("Type"), grvUpload.GetFocusedRowCellValue("AlamatUpload"), lblRM.Text, lblNamaPasien.Text, deDATELAHIR.DateTime.ToString("dd-MM-yyyy"), IIf(lblJK.Text = "L", "Laki-laki", "Perempuan"), grvUpload.GetFocusedRowCellValue("Kodeupload"))
                frmBrowseUpload.ShowDialog(Me)
            Catch ex As Exception
                MsgBox("Load Form Detail : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
            Finally
                If Not frmBrowseUpload Is Nothing Then frmBrowseUploadRadiologi.Dispose()
                frmBrowseUpload = Nothing
            End Try
        End If
    End Sub
    Private Function fn_SaveCustomer_Detil() As Boolean
        Try
            fn_SaveCustomer_Detil = True

            Dim oCustomer_Detil As New Reference.clsCustomerDetil
            ' ***** HEADER *****
            Dim ds = oCustomer_Detil.GetStructureHeader
            With ds
                Try
                    .DATECREATED = oCustomer_Detil.GetData(lblRegister.Text).DATECREATED
                Catch ex As Exception
                    .DATECREATED = Now
                End Try
                .DATEUPDATED = Now
                .KDCUSTOMER = lblRM.Text
                .BB = txtBERATBADAN.Text
                .TT = txtTB.Text
                .ALERGI = txtALERGIOBAT.Text
                .DIAGNOSA = txtDIAGNOSAUTAMA.Text
            End With

            Dim dsCsutomer_Detil = oCustomer_Detil.GetData(lblRM.Text)

            If dsCsutomer_Detil Is Nothing Then
                oCustomer_Detil.InsertData(ds)
            Else
                oCustomer_Detil.UpdateData(ds)
            End If
        Catch oErr As Exception
            MsgBox("Simpan Data : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            fn_SaveCustomer_Detil = False
        End Try
    End Function
    Private Sub grvListPasien_RowStyle(sender As System.Object, e As DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs) Handles grvListPasien.RowStyle
        If grvListPasien.IsFilterRow(e.RowHandle) Then Exit Sub
        If sJenisRawat = 0 Then
            If grdKDDEPARTMENT.Text = "IGD" Then
                If grvListPasien.GetRowCellValue(e.RowHandle, "Selesai") = "Y" Then
                    e.Appearance.BackColor = Color.LawnGreen
                Else
                    If grvListPasien.GetRowCellValue(e.RowHandle, "Selesai") = "X" Then
                        e.Appearance.BackColor = Color.LightPink
                    Else
                        'If grvListPasien.GetRowCellValue(e.RowHandle, "ISPERAWAT") = True Then
                        '    e.Appearance.BackColor = Color.Orange
                        'End If
                        If sisDOkter = True Then
                            If grvListPasien.GetRowCellValue(e.RowHandle, "Cek") = True Then
                                e.Appearance.BackColor = Color.Yellow
                            Else
                                If grvListPasien.GetRowCellValue(e.RowHandle, "ISPERAWAT") = True Then
                                    e.Appearance.BackColor = Color.Orange
                                End If
                            End If
                        Else
                            e.Appearance.BackColor = Color.Orange
                        End If
                    End If

                    'If sisDOkter = False Then
                    '    If grvListPasien.GetRowCellValue(e.RowHandle, "Selesai") = "X" Then
                    '        e.Appearance.BackColor = Color.LightPink
                    '    Else
                    '        If grvListPasien.GetRowCellValue(e.RowHandle, "ISPERAWAT") = True Then
                    '            e.Appearance.BackColor = Color.Orange
                    '        End If
                    '    End If
                    'Else
                    '    If grvListPasien.GetRowCellValue(e.RowHandle, "Cek") = True Then
                    '        e.Appearance.BackColor = Color.Yellow
                    '    End If
                    'End If
                End If
            Else
                If grvListPasien.GetRowCellValue(e.RowHandle, "Cek") = True Then
                    e.Appearance.BackColor = Color.LawnGreen
                Else
                    If grvListPasien.GetRowCellValue(e.RowHandle, "ISPERAWAT") = True Then
                        e.Appearance.BackColor = Color.Orange
                    End If
                End If
            End If
        Else
            If grvListPasien.GetRowCellValue(e.RowHandle, "Selesai") = "Y" Then
                e.Appearance.BackColor = Color.Yellow
            End If
        End If
    End Sub
#End Region
#Region "Command Button"
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
#End Region
#Region "Lookup / Event"
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
    Private Sub fn_LoadRuangan()
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
            SQL &= "A.KDMROOM "
            SQL &= ",A.NAME_DISPLAY "
            SQL &= "FROM "
            SQL &= "M_MEDICAL_ROOM A "

            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "RUANGAN")

            If oConn.State = ConnectionState.Open Then
                oConn.Close()
            End If

            grdKAMAR.Properties.DataSource = ds.Tables("RUANGAN")
            grdKAMAR.Properties.ValueMember = "KDMROOM"
            grdKAMAR.Properties.DisplayMember = "NAME_DISPLAY"

        Catch oErr As Exception
            MsgBox("Load Ruangan Data : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
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
    Private Sub tabbedControlGroup1_SelectedPageChanged() Handles tabbedControlGroup1.SelectedPageChanged
        If lblRegister.Text = "No. Register" Then
            fn_EmptyMe()
            Exit Sub
        End If
        If lblRegister.Text = "-" Then
            fn_EmptyMe()
            Exit Sub
        End If
        If lblRegister.Text = "Nul" Then
            fn_EmptyMe()
            Exit Sub
        End If

        If tabbedControlGroup1.SelectedTabPageIndex = 0 Then
            XtraTabControl1_SelectedPageChanged()
        ElseIf tabbedControlGroup1.SelectedTabPageIndex = 1 Then
            XtraTabControl2_SelectedPageChanged()
        ElseIf tabbedControlGroup1.SelectedTabPageIndex = 2 Then
            fn_LoadDataPenunjang()
        ElseIf tabbedControlGroup1.SelectedTabPageIndex = 3 Then
            fn_LoadHystori(lblRM.Text)
        ElseIf tabbedControlGroup1.SelectedTabPageIndex = 4 Then
            fn_DokterBersama(lblRegister.Text)
        End If
    End Sub
    Private Sub XtraTabControl1_SelectedPageChanged() Handles XtraTabControl1.SelectedPageChanged
        If isLoad_Splash = False Then Exit Sub

        If lblRegister.Text = "-" Then
            fn_EmptyMe()
            Exit Sub
        End If
        If lblRegister.Text = "Nul" Then
            fn_EmptyMe()
            Exit Sub
        End If

        PdfViewerAskepRawatJalan.CloseDocument()
        PdfViewerCPPTRawatJalan.CloseDocument()
        PdfViewerResume.CloseDocument()
        PdfViewer_IGD.CloseDocument()
        PdfViewer1Vonek.CloseDocument()
        pdfGERD.CloseDocument()
        PdfViewerPerawatIGD.CloseDocument()

        Dim FolderSimpan = "C:/CEKDATARPORT/"

        If Not Directory.Exists(FolderSimpan) Then
            Directory.CreateDirectory(FolderSimpan)
        Else
            DeleteDirectory(FolderSimpan)
            Directory.CreateDirectory(FolderSimpan)
        End If

        If XtraTabControl1.SelectedTabPageIndex = 0 Then
            If sCPPT_QRRJ = False Then
                Try
                    SplashScreenManager.ShowForm(Me, GetType(frmLoadingPanel), True, True, False)

                    SplashScreenManager.Default.SetWaitFormCaption("Processing data CPPT Rawat Jalan.....")

                    Dim oReqCPPT As New Transaksi.clsReqCPPT

                    Dim dataList As New List(Of Byte())
                    Dim dsLoad() As Byte
                    Dim ds = oReqCPPT.GetDataByRMList(lblRM.Text)

                    If ds.Count > 0 Then
                        Dim rpt As New xtraDigital_CPPT_01

                        rpt.ShowPrintMarginsWarning = False
                        rpt.Watermark.Text = sWATERMARK

                        rpt.bindingSource.DataSource = ds
                        rpt.ExportToPdf(FolderSimpan & "HASIL" & ".pdf")
                        dataList.Add(File.ReadAllBytes(FolderSimpan & "HASIL" & ".pdf"))

                        If dataList.Count > 0 Then
                            dsLoad = MergeFilesByte(dataList)
                            Dim stream As New MemoryStream(dsLoad)
                            PdfViewerCPPTRawatJalan.LoadDocument(stream)
                        End If
                    End If

                    SplashScreenManager.CloseForm(False)
                Catch oErr As Exception
                    SplashScreenManager.CloseForm(False)
                    MsgBox("Cetak CPPT" & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            Else
                Try
                    SplashScreenManager.ShowForm(Me, GetType(frmLoadingPanel), True, True, False)

                    SplashScreenManager.Default.SetWaitFormCaption("Processing data CPPT Rawat Jalan.....")

                    Dim oReqCPPT As New Transaksi.clsReqCPPT

                    Dim dataList As New List(Of Byte())
                    Dim dsLoad() As Byte
                    Dim ds = oReqCPPT.GetDataByRMList(lblRM.Text)

                    If ds.Count > 0 Then
                        Dim rpt As New xtraDigital_CPPT_01_QR

                        rpt.ShowPrintMarginsWarning = False
                        rpt.Watermark.Text = sWATERMARK

                        rpt.bindingSource.DataSource = ds
                        rpt.ExportToPdf(FolderSimpan & "HASIL" & ".pdf")
                        dataList.Add(File.ReadAllBytes(FolderSimpan & "HASIL" & ".pdf"))

                        If dataList.Count > 0 Then
                            dsLoad = MergeFilesByte(dataList)
                            Dim stream As New MemoryStream(dsLoad)
                            PdfViewerCPPTRawatJalan.LoadDocument(stream)
                        End If
                    End If

                    SplashScreenManager.CloseForm(False)
                Catch oErr As Exception
                    SplashScreenManager.CloseForm(False)
                    MsgBox("Cetak CPPT" & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            End If
        ElseIf XtraTabControl1.SelectedTabPageIndex = 1 Then
            Try
                SplashScreenManager.ShowForm(Me, GetType(frmLoadingPanel), True, True, False)

                SplashScreenManager.Default.SetWaitFormCaption("Processing data Resume Rawat Jalan.....")

                Dim oKoding As New Admission.clsKoding
                Dim dataList As New List(Of Byte())
                Dim dsLoad() As Byte

                Dim dsKoding = oKoding.GetDataByKDPENDAFTARAN(lblRegister.Text)
                If dsKoding IsNot Nothing Then
                    Dim rpt As New xtraKoding

                    rpt.bindingSource.DataSource = dsKoding

                    DownloadIamge1 = AlamatDownloadIamge1 & lblRM.Text & "/" & lblRM.Text & ".png"

                    sUserIDTandaTangan = dsKoding.KDDOCTOR

                    rpt.bindingSource.DataSource = dsKoding

                    Dim Waktu As String = Now.ToString("yyyyMMddHHmmss")

                    rpt.ExportToPdf(FolderSimpan & Waktu & dsKoding.KDKODING & ".pdf")

                    dataList.Add(File.ReadAllBytes(FolderSimpan & Waktu & dsKoding.KDKODING & ".pdf"))

                    'PdfViewerResume.LoadDocument(FolderSimpan & "HASIL" & ".pdf")
                End If

                Dim oCPPT As New Transaksi.clsCPPT

                Dim dsCPPT = oCPPT.GetDataByRegisterProfesi(lblRegister.Text, "FISIOTERAPI")

                If dsCPPT IsNot Nothing Then
                    sKDDOCTOR_PENDAFTARAN = lblKDDOCTOR.Text

                    Dim rpt As New xtraLembarUjiFungsiRehabResumeFisioterapi

                    rpt.bindingSource.DataSource = dsCPPT

                    Dim Waktu As String = Now.ToString("yyyyMMddHHmmss")
                    rpt.ExportToPdf(FolderSimpan & Waktu & dsCPPT.KDCPPT & ".pdf")
                    dataList.Add(File.ReadAllBytes(FolderSimpan & Waktu & dsCPPT.KDCPPT & ".pdf"))
                End If

                Dim oSetKoneksi As New Brigging.clsSetKoneksi

                If dataList.Count > 0 Then
                    dsLoad = oSetKoneksi.MergeFilesByte(dataList)
                    Dim stream As New MemoryStream(dsLoad)

                    PdfViewerResume.LoadDocument(stream)
                End If

                SplashScreenManager.CloseForm(False)
            Catch oErr As Exception
                SplashScreenManager.CloseForm(False)
                MsgBox("Resume Rawat Jalan Data : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            End Try
        ElseIf XtraTabControl1.SelectedTabPageIndex = 2 Then
            Try
                SplashScreenManager.ShowForm(Me, GetType(frmLoadingPanel), True, True, False)

                SplashScreenManager.Default.SetWaitFormCaption("Processing data Asesemen Awal Medis IGD.....")

                Dim oIGD As New Transaksi.clsDigital_IGD_01

                Dim dataList As New List(Of Byte())
                Dim dsLoad() As Byte

                For Each xloop In oIGD.GetDataByRM(lblRM.Text)
                    Dim ds = oIGD.GetData(xloop.KDPENDAFTARAN)
                    If ds IsNot Nothing Then
                        sASESEMEN_IGD = ds.SURVEY_PERUT_1

                        sUSIADIASESMENIGD = deDATELAHIR.DateTime.ToString("dd-MM-yyyy") & " (" & HitungUmur(deDATELAHIR.DateTime, deDATE.DateTime) & ")"
                        Dim rpt As New xtraReportFormulirIGD1
                        rpt.ShowPrintMarginsWarning = False
                        rpt.Watermark.Text = sWATERMARK
                        rpt.BindingSource.DataSource = ds
                        rpt.ExportToPdf(FolderSimpan & ds.KDPENDAFTARAN & ".pdf")
                        dataList.Add(File.ReadAllBytes(FolderSimpan & ds.KDPENDAFTARAN & ".pdf"))
                    End If
                Next

                If dataList.Count > 0 Then
                    dsLoad = MergeFilesByte(dataList)
                    Dim stream As New MemoryStream(dsLoad)
                    PdfViewer_IGD.LoadDocument(stream)
                End If

                SplashScreenManager.CloseForm(False)

            Catch oErr As Exception
                SplashScreenManager.CloseForm(False)
                MsgBox("Cetak Asesemen Medis IGD" & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            End Try
        ElseIf XtraTabControl1.SelectedTabPageIndex = 3 Then
            Try
                SplashScreenManager.ShowForm(Me, GetType(frmLoadingPanel), True, True, False)

                SplashScreenManager.Default.SetWaitFormCaption("Processing data Asesemen Awal Perawatan IGD.....")

                Dim oIGD As New Transaksi.clsDigital_IGD_02

                Dim dataList As New List(Of Byte())
                Dim dsLoad() As Byte

                For Each xloop In oIGD.GetDataByRM(lblRM.Text)
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
                    dsLoad = MergeFilesByte(dataList)
                    Dim stream As New MemoryStream(dsLoad)
                    PdfViewerPerawatIGD.LoadDocument(stream)
                End If

                SplashScreenManager.CloseForm(False)

            Catch oErr As Exception
                SplashScreenManager.CloseForm(False)
                MsgBox("Cetak Asesemen Medis IGD" & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            End Try
        ElseIf XtraTabControl1.SelectedTabPageIndex = 4 Then
            Try
                SplashScreenManager.ShowForm(Me, GetType(frmLoadingPanel), True, True, False)

                SplashScreenManager.Default.SetWaitFormCaption("Processing data Tabel Diagnosis GERD-Q.....")

                Dim oIGD As New Inventory.clsDigital_IGD_01_GERD

                Dim dataList As New List(Of Byte())
                Dim dsLoad() As Byte

                For Each xloop In oIGD.GetDataByRM(lblRM.Text)
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
                    dsLoad = MergeFilesByte(dataList)
                    Dim stream As New MemoryStream(dsLoad)
                    pdfGERD.LoadDocument(stream)
                End If

                SplashScreenManager.CloseForm(False)
            Catch oErr As Exception
                SplashScreenManager.CloseForm(False)
                MsgBox("Cetak Gerd Q IGD" & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            End Try
        ElseIf XtraTabControl1.SelectedTabPageIndex = 5 Then
            Try
                SplashScreenManager.ShowForm(Me, GetType(frmLoadingPanel), True, True, False)

                SplashScreenManager.Default.SetWaitFormCaption("Processing data Tabel Asesmen Kebidanan Vonek.....")

                Dim oIGD As New Transaksi.clsDigital_IGD_03

                Dim dataList As New List(Of Byte())
                Dim dsLoad() As Byte

                NAMA = lblNamaPasien.Text
                RM = lblRM.Text
                TANGGALLAHIR = deDATELAHIR.DateTime.ToString("dd-MM-yyyy")

                Dim ds = oIGD.GetData(lblRegister.Text)
                If ds IsNot Nothing Then
                    Dim rpt As New xtraReportFormulirIGD3

                    rpt.ShowPrintMarginsWarning = False
                    rpt.Watermark.Text = sWATERMARK

                    rpt.BindingSource.DataSource = ds
                    rpt.ExportToPdf(FolderSimpan & ds.KDPENDAFTARAN & ".pdf")
                    dataList.Add(File.ReadAllBytes(FolderSimpan & ds.KDPENDAFTARAN & ".pdf"))
                End If

                If dataList.Count > 0 Then
                    dsLoad = MergeFilesByte(dataList)
                    Dim stream As New MemoryStream(dsLoad)
                    PdfViewer1Vonek.LoadDocument(stream)
                End If

                SplashScreenManager.CloseForm(False)

            Catch oErr As Exception
                SplashScreenManager.CloseForm(False)
                MsgBox("Cetak Asesemen Vonek IGD" & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            End Try
        ElseIf XtraTabControl1.SelectedTabPageIndex = 6 Then
            Try
                SplashScreenManager.ShowForm(Me, GetType(frmLoadingPanel), True, True, False)

                SplashScreenManager.Default.SetWaitFormCaption("Processing data Asemsen Awal Keperawatan Rawat Jalan.....")

                Dim oIGD As New Digital.clsDigital_AskepRawatJalan

                Dim dataList As New List(Of Byte())
                Dim dsLoad() As Byte

                NAMA = lblNamaPasien.Text
                RM = lblRM.Text
                TANGGALLAHIR = deDATELAHIR.DateTime.ToString("dd-MM-yyyy")

                Dim ds = oIGD.GetData(lblRegister.Text)
                If ds IsNot Nothing Then
                    Dim rpt As New xtraReportAskepRawatJalan

                    rpt.ShowPrintMarginsWarning = False
                    rpt.Watermark.Text = sWATERMARK

                    rpt.BindingSource1.DataSource = ds


                    Dim alamat = Now.ToString("yyyyMMddHHmmss") & ds.KDPENDAFTARAN
                    rpt.ExportToPdf(FolderSimpan & alamat & ".pdf")
                    dataList.Add(File.ReadAllBytes(FolderSimpan & alamat & ".pdf"))
                End If

                If dataList.Count > 0 Then
                    dsLoad = MergeFilesByte(dataList)
                    Dim stream As New MemoryStream(dsLoad)
                    PdfViewerAskepRawatJalan.LoadDocument(stream)
                End If

                SplashScreenManager.CloseForm(False)

            Catch oErr As Exception
                SplashScreenManager.CloseForm(False)
                MsgBox("Cetak Asesemen Vonek IGD" & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            End Try
        End If
    End Sub
    Private Sub XtraTabControl2_SelectedPageChanged() Handles XtraTabControl2.SelectedPageChanged
        If isLoad_Splash = False Then Exit Sub

        If lblRegister.Text = "-" Then
            fn_EmptyMe()
            Exit Sub
        End If
        If lblRegister.Text = "Nul" Then
            fn_EmptyMe()
            Exit Sub
        End If

        PdfViewerHadnOver.CloseDocument()
        PdfViewerSBAR.CloseDocument()
        PdfViewerCPPT_RI.CloseDocument()
        PdfViewerRekonsiliasiObat.CloseDocument()

        NAMA = lblNamaPasien.Text
        TANGGALLAHIR = deDATELAHIR.DateTime.ToString("dd-MM-yyyy")
        JENISKELAMIN = IIf(lblJK.Text = "L", "Laki-laki", "Perempuan")

        Dim FolderSimpan = "C:/CEKREPORTRI/"

        If Not Directory.Exists(FolderSimpan) Then
            Directory.CreateDirectory(FolderSimpan)
        Else
            DeleteDirectory(FolderSimpan)
            Directory.CreateDirectory(FolderSimpan)
        End If

        If XtraTabControl2.SelectedTabPageIndex = 0 Then
            Try
                SplashScreenManager.ShowForm(Me, GetType(frmLoadingPanel), True, True, False)

                SplashScreenManager.Default.SetWaitFormCaption("Processing data CPPT Rawat Inap.....")

                If lblRM.Text = String.Empty Then Exit Sub
                Dim oReqCPPT As New Transaksi.clsCPPT

                Dim dataList As New List(Of Byte())
                Dim dsLoad() As Byte
                Dim ds = oReqCPPT.GetDataByRMRANAP(lblRM.Text)

                If ds.Count > 0 Then
                    Dim rpt As New xtraDigital_CPPT_01_RawatInap

                    rpt.ShowPrintMarginsWarning = False
                    rpt.Watermark.Text = sWATERMARK

                    rpt.bindingSource.DataSource = ds
                    rpt.ExportToPdf(FolderSimpan & "HASIL" & ".pdf")
                    dataList.Add(File.ReadAllBytes(FolderSimpan & "HASIL" & ".pdf"))

                    If dataList.Count > 0 Then
                        dsLoad = MergeFilesByte(dataList)
                        Dim stream As New MemoryStream(dsLoad)
                        PdfViewerCPPT_RI.LoadDocument(stream)
                    End If
                End If

                SplashScreenManager.CloseForm(False)
            Catch oErr As Exception
                SplashScreenManager.CloseForm(False)
                MsgBox("Cetak CPPT" & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            End Try
        ElseIf XtraTabControl2.SelectedTabPageIndex = 1 Then
            Try
                SplashScreenManager.ShowForm(Me, GetType(frmLoadingPanel), True, True, False)

                SplashScreenManager.Default.SetWaitFormCaption("Processing data Hand Over.....")

                If lblRM.Text = "" Then Exit Sub
                Dim oHandOver As New Transaksi.clsDigital_HandOver

                Dim dataList As New List(Of Byte())
                Dim dsLoad() As Byte

                For Each xloop In oHandOver.GetDataByRM(lblRM.Text)
                    Dim ds = oHandOver.GetData(xloop.KDHO)
                    If ds IsNot Nothing Then
                        Dim rpt As New xtraReportHandOver
                        rpt.ShowPrintMarginsWarning = False
                        rpt.Watermark.Text = sWATERMARK
                        rpt.BindingSource1.DataSource = ds
                        rpt.ExportToPdf(FolderSimpan & ds.KDHO & ".pdf")
                        dataList.Add(File.ReadAllBytes(FolderSimpan & ds.KDHO & ".pdf"))
                    End If
                Next

                If dataList.Count > 0 Then
                    dsLoad = MergeFilesByte(dataList)
                    Dim stream As New MemoryStream(dsLoad)
                    PdfViewerHadnOver.LoadDocument(stream)
                End If

                SplashScreenManager.CloseForm(False)

            Catch oErr As Exception
                SplashScreenManager.CloseForm(False)
                MsgBox("Cetak Hand Over" & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            End Try
        ElseIf XtraTabControl2.SelectedTabPageIndex = 2 Then
            Try
                SplashScreenManager.ShowForm(Me, GetType(frmLoadingPanel), True, True, False)

                SplashScreenManager.Default.SetWaitFormCaption("Processing data Transfer Internal.....")

                If lblRM.Text = "" Then Exit Sub
                Dim oDigital As New Transaksi.clsTransferInternal

                Dim dataList As New List(Of Byte())
                Dim dsLoad() As Byte

                For Each xloop In oDigital.GetDataByRM(lblRM.Text)
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
                    PdfViewerSBAR.LoadDocument(stream)
                End If

                SplashScreenManager.CloseForm(False)

            Catch oErr As Exception
                SplashScreenManager.CloseForm(False)
                MsgBox("Cetak SBAR" & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            End Try
        ElseIf XtraTabControl2.SelectedTabPageIndex = 3 Then
            Try
                SplashScreenManager.ShowForm(Me, GetType(frmLoadingPanel), True, True, False)

                SplashScreenManager.Default.SetWaitFormCaption("Processing data Rekonsiliasi Obat.....")

                If lblRM.Text = "" Then Exit Sub
                Dim oRekonsiliasi As New Transaksi.clsRekonsiliasiObat

                Dim dataList As New List(Of Byte())
                Dim dsLoad() As Byte

                For Each xloop In oRekonsiliasi.GetDataByRM(lblRM.Text)
                    Dim ds = oRekonsiliasi.GetData(xloop.KDREKONSILIASI)
                    If ds IsNot Nothing Then
                        Dim rpt As New xtraReportRekonsiliasiObat
                        rpt.ShowPrintMarginsWarning = False
                        rpt.Watermark.Text = sWATERMARK
                        rpt.BindingSource1.DataSource = ds
                        rpt.ExportToPdf(FolderSimpan & ds.KDREKONSILIASI & ".pdf")
                        dataList.Add(File.ReadAllBytes(FolderSimpan & ds.KDREKONSILIASI & ".pdf"))
                    End If
                Next

                If dataList.Count > 0 Then
                    dsLoad = MergeFilesByte(dataList)
                    Dim stream As New MemoryStream(dsLoad)
                    PdfViewerRekonsiliasiObat.LoadDocument(stream)
                End If

                SplashScreenManager.CloseForm(False)

            Catch oErr As Exception
                SplashScreenManager.CloseForm(False)
                MsgBox("Cetak Hand Over" & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            End Try
        End If
    End Sub
    Private Sub picRefreshListPasien_Click() Handles picRefreshListPasien.Click
        fn_LoadData()
    End Sub
    Public Function MergeFilesByte(ByVal sourceFiles As List(Of Byte())) As Byte()
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
#End Region
#Region "Form"
    Private Sub fn_LoadDokter(ByVal Paramater As String)
        Try
            If sJenisRawat = 0 Then

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
                        SQL &= ",KET = '' "
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
                        SQL &= "A.KDDOCTOR,A. NAME_DISPLAY "
                        SQL &= ",KET = ISNULL((SELECT TOP 1 'ADA' FROM BUDIJAYAGROUP_RSKC..S_PENDAFTARAN_H WHERE A.KDDOCTOR = KDDOCTOR AND CONVERT(VARCHAR(8), DATE, 112) = '" & deDATE.DateTime.ToString("yyyyMMdd") & "'), '') "
                        SQL &= "FROM  "
                        SQL &= "M_DOCTOR A "
                        SQL &= "WHERE "
                        SQL &= "A.ISACTIVE = '1' "
                        SQL &= "AND A.KDDEPARTMENT = '" & grdKDDEPARTMENT.EditValue & "' "

                        SQL &= "ORDER BY A.NAME_DISPLAY "

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
                    SQL &= "A.KDDOCTOR, A.NAME_DISPLAY "
                    SQL &= ",KET = ISNULL((SELECT TOP 1 'ADA' FROM BUDIJAYAGROUP_RSKC..S_PENDAFTARAN_H WHERE A.KDDOCTOR = KDDOCTOR AND CONVERT(VARCHAR(8), DATE, 112) = '" & deDATE.DateTime.ToString("yyyyMMdd") & "'), '') "
                    SQL &= "FROM  "
                    SQL &= "M_DOCTOR A "
                    SQL &= "WHERE "
                    SQL &= "A.ISACTIVE = '1' "
                    SQL &= "AND A.OTHER = 'SKIN' "
                    SQL &= "ORDER BY A.NAME_DISPLAY "

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
                'If grdKDDEPARTMENT.Text = String.Empty Then Exit Sub

                'Dim oConn As New SqlConnection
                'Dim oComm As New SqlCommand
                'Dim da As SqlDataAdapter
                'Dim ds As New DataSet
                'Dim SQL As String
                'Dim sConn As String = Decrypt(My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Software\SIMRS\SW\", "Database", "").ToString())

                'oConn = New SqlConnection(sConn)

                'If oConn.State = ConnectionState.Closed Then
                '    oConn.Open()
                'End If

                'SQL = "SELECT "
                'SQL &= "A.KDDOCTOR, A.NAME_DISPLAY "
                'SQL &= ",KET = ISNULL((SELECT TOP 1 'ADA' FROM BUDIJAYAGROUP_RSKC..S_PENDAFTARAN_H WHERE A.KDDOCTOR = KDDOCTOR AND CONVERT(VARCHAR(8), DATE, 112) = '" & deDATE.DateTime.ToString("yyyyMMdd") & "'), '') "
                'SQL &= "FROM  "
                'SQL &= "M_DOCTOR A "
                'SQL &= "WHERE "
                'SQL &= "A.ISACTIVE = '1' "
                'If Paramater <> "1058" Then
                '    SQL &= "AND A.KDDEPARTMENT = '" & grdKDDEPARTMENT.EditValue & "' "
                'Else
                '    SQL &= "AND A.OTHER = 'SKIN' "
                'End If

                'oComm.Connection = oConn
                'oComm.CommandText = SQL
                'oComm.CommandTimeout = 120
                'oComm.CommandType = CommandType.Text

                'da = New SqlDataAdapter(oComm)
                'da.Fill(ds, "M_DOCTOR")

                'grdDPJP.Properties.DataSource = ds.Tables("M_DOCTOR")
                'grdDPJP.Properties.ValueMember = "KDDOCTOR"
                'grdDPJP.Properties.DisplayMember = "NAME_DISPLAY"

                'If oConn.State = ConnectionState.Open Then
                '    oConn.Close()
                'End If

                'grdDPJP.Text = Paramater
            Else
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
                SQL &= ",KET = '' "
                SQL &= "FROM  "
                SQL &= "M_DOCTOR "
                SQL &= "WHERE "
                SQL &= "ISACTIVE = '1' "

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
        If sisDOkter = False Then Exit Sub
        Select Case e.KeyCode
            Case Keys.F3
                MsgBox("Silhkan Pilih Modul Rawat Jalan", MsgBoxStyle.Exclamation, Me.Text)
                Exit Sub

                If lblRegister.Text = "" Then
                    MsgBox("Pasien Belum dipilih, Silahkan kembali ke List Pasien", MsgBoxStyle.Exclamation, Me.Text)
                    fn_EmptyMe()
                    Exit Sub
                End If

                If grvListPasien.GetFocusedRowCellValue("Antrian") <> "" Then
                    Dim uTime As Integer = (DateTime.UtcNow - New DateTime(1970, 1, 1, 0, 0, 0)).TotalSeconds
                    Dim JsonRequest As String = fn_RequestUpdateWaktuAntrean(grvListPasien.GetFocusedRowCellValue("Antrian"), 4, uTime)
                    If JsonRequest <> "" Then
                        MsgBox(fn_UpdateWaktuAntrean(JsonRequest, uTime), MsgBoxStyle.Information, Me.Text)
                    End If
                End If

                Dim oKoding As New Admission.clsKoding
                Dim dsKoding = oKoding.GetDataByKDPENDAFTARAN(lblRegister.Text)
                If dsKoding IsNot Nothing Then
                    Dim frmMedrekRawatJalan As New frmMedrekRawatJalan
                    Try
                        frmMedrekRawatJalan.fn_LoadIdentias(txtBERATBADAN.Text, txtTB.Text, txtALERGIOBAT.Text, lblRegister.Text, lblTanggalLahirUsia.Text, lblNamaPasien.Text, lblRM.Text, lblDPJP.Text, grdKDDEPARTMENT.Text, lblSIP.Text, lblPenjamin.Text, lblKodeBooking.Text, grdKDDEPARTMENT.EditValue, lblKDDOCTOR.Text, lblNOMORSEP.Text, lblNOKARTUBPJS.Text, txtDIAGNOSAUTAMA.Text, deDATELAHIR.DateTime, IIf(lblJK.Text = "L", "1", "2"))
                        frmMedrekRawatJalan.LoadMe(FORM_MODE.FORM_MODE_EDIT, dsKoding.KDKODING)
                        frmMedrekRawatJalan.ShowDialog(Me)
                        fn_LoadSecurity()
                    Catch ex As Exception
                        MsgBox("Load Form Detail : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
                    Finally
                        If Not frmMedrekRawatJalan Is Nothing Then frmMedrekRawatJalan.Dispose()
                        frmMedrekRawatJalan = Nothing

                        Dim rowHandle As Integer = grvListPasien.LocateByValue(rowHandle, grvListPasien.Columns("NoRegister"), sCode)
                        If rowHandle > 0 Then grvListPasien.FocusedRowHandle = rowHandle

                        If sStatusSave = "NEW" Then
                            sStatusSave = "NONE"
                        End If
                    End Try
                Else
                    Dim frmMedrekRawatJalan As New frmMedrekRawatJalan
                    Try
                        frmMedrekRawatJalan.fn_LoadIdentias(txtBERATBADAN.Text, txtTB.Text, txtALERGIOBAT.Text, lblRegister.Text, lblTanggalLahirUsia.Text, lblNamaPasien.Text, lblRM.Text, lblDPJP.Text, grdKDDEPARTMENT.Text, lblSIP.Text, lblPenjamin.Text, lblKodeBooking.Text, grdKDDEPARTMENT.EditValue, lblKDDOCTOR.Text, lblNOMORSEP.Text, lblNOKARTUBPJS.Text, txtDIAGNOSAUTAMA.Text, deDATELAHIR.DateTime, IIf(lblJK.Text = "L", "1", "2"))
                        frmMedrekRawatJalan.LoadMe(FORM_MODE.FORM_MODE_ADD)
                        frmMedrekRawatJalan.ShowDialog(Me)
                        fn_LoadSecurity()
                    Catch ex As Exception
                        MsgBox("Load Form Detail : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
                    Finally
                        If Not frmMedrekRawatJalan Is Nothing Then frmMedrekRawatJalan.Dispose()
                        frmMedrekRawatJalan = Nothing

                        Dim rowHandle As Integer = grvListPasien.LocateByValue(rowHandle, grvListPasien.Columns("NoRegister"), sCode)
                        If rowHandle > 0 Then grvListPasien.FocusedRowHandle = rowHandle

                        If sStatusSave = "NEW" Then
                            sStatusSave = "NONE"
                        End If
                    End Try
                End If
        End Select
    End Sub
    Private Sub grdKDDEPARTMENT_EditValueChanged(sender As Object, e As EventArgs) Handles grdKDDEPARTMENT.EditValueChanged
        If isLoad = True Then
            fn_LoadView()
            fn_LoadDokter(grdKDDEPARTMENT.EditValue)
        End If
    End Sub
    Private Sub PemeriksaanAwalToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PemeriksaanAwalToolStripMenuItem.Click
        If lblRegister.Text = "" Then
            MsgBox("Pasien Belum dipilih, Silahkan kembali ke List Pasien", MsgBoxStyle.Exclamation, Me.Text)
            fn_EmptyMe()
            Exit Sub
        End If
        If lblRegister.Text = "Nul" Then
            MsgBox("Pasien Belum dipilih, Silahkan kembali ke List Pasien", MsgBoxStyle.Exclamation, Me.Text)
            fn_EmptyMe()
            Exit Sub
        End If
        If lblRegister.Text <> "" Then
            Dim oReqAwalPemeriksaan As New Transaksi.clsReqAwalPemeriksaan

            Dim ds = oReqAwalPemeriksaan.GetData(lblRegister.Text)

            If ds IsNot Nothing Then
                Dim frmReqAwalPemeriksaan As New frmReqAwalPemeriksaan
                Try
                    frmReqAwalPemeriksaan.LoadMe(FORM_MODE.FORM_MODE_EDIT, lblNamaPasien.Text, deDATELAHIR.DateTime, lblRM.Text, IIf(lblJK.Text = "L", "1", "2"), lblRegister.Text, lblKDDOCTOR.Text)
                    frmReqAwalPemeriksaan.ShowDialog(Me)
                    fn_LoadData()
                Catch ex As Exception
                    MsgBox("Load Form Detail : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
                Finally
                    If Not frmReqAwalPemeriksaan Is Nothing Then frmReqAwalPemeriksaan.Dispose()
                    frmReqAwalPemeriksaan = Nothing

                    Dim rowHandle As Integer = grvListPasien.LocateByValue(rowHandle, grvListPasien.Columns("NoRegister"), sCode)
                    If rowHandle > 0 Then grvListPasien.FocusedRowHandle = rowHandle

                    If sStatusSave = "NEW" Then
                        sStatusSave = "NONE"
                    End If
                End Try
            Else
                Dim frmReqAwalPemeriksaan As New frmReqAwalPemeriksaan
                Try
                    frmReqAwalPemeriksaan.LoadMe(FORM_MODE.FORM_MODE_ADD, lblNamaPasien.Text, deDATELAHIR.DateTime, lblRM.Text, IIf(lblJK.Text = "L", "1", "2"), lblRegister.Text, lblKDDOCTOR.Text)
                    frmReqAwalPemeriksaan.ShowDialog(Me)
                    fn_LoadData()
                Catch ex As Exception
                    MsgBox("Load Form Detail : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
                Finally
                    If Not frmReqAwalPemeriksaan Is Nothing Then frmReqAwalPemeriksaan.Dispose()
                    frmReqAwalPemeriksaan = Nothing

                    Dim rowHandle As Integer = grvListPasien.LocateByValue(rowHandle, grvListPasien.Columns("NoRegister"), sCode)
                    If rowHandle > 0 Then grvListPasien.FocusedRowHandle = rowHandle

                    If sStatusSave = "NEW" Then
                        sStatusSave = "NONE"
                    End If
                End Try
            End If
        End If
    End Sub
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
    Private Sub AddToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AddToolStripMenuItem.Click
        MsgBox("Silahkan Pilih Form Rawat Jalan", MsgBoxStyle.Exclamation, Me.Text)
        Exit Sub

        If lblRegister.Text = "" Then
            MsgBox("Pasien Belum dipilih, Silahkan kembali ke List Pasien", MsgBoxStyle.Exclamation, Me.Text)
            fn_EmptyMe()
            Exit Sub
        End If
        If lblRegister.Text = "Nul" Then
            MsgBox("Pasien Belum dipilih, Silahkan kembali ke List Pasien", MsgBoxStyle.Exclamation, Me.Text)
            fn_EmptyMe()
            Exit Sub
        End If

        MsgBox("Silhkan Pilih Modul Rawat Jalan", MsgBoxStyle.Exclamation, Me.Text)
        Exit Sub

        Dim oKoding As New Admission.clsKoding
        Dim dsKoding = oKoding.GetDataByKDPENDAFTARAN(lblRegister.Text)

        If dsKoding IsNot Nothing Then
            Dim frmMedrekRawatJalan As New frmMedrekRawatJalan
            Try
                frmMedrekRawatJalan.fn_LoadIdentias(txtBERATBADAN.Text, txtTB.Text, txtALERGIOBAT.Text, lblRegister.Text, lblTanggalLahirUsia.Text, lblNamaPasien.Text, lblRM.Text, lblDPJP.Text, grdKDDEPARTMENT.Text, lblSIP.Text, lblPenjamin.Text, lblKodeBooking.Text, grdKDDEPARTMENT.EditValue, lblKDDOCTOR.Text, lblNOMORSEP.Text, lblNOKARTUBPJS.Text, txtDIAGNOSAUTAMA.Text, deDATELAHIR.DateTime, IIf(lblJK.Text = "L", "1", "2"))
                frmMedrekRawatJalan.LoadMe(FORM_MODE.FORM_MODE_EDIT, dsKoding.KDKODING)
                frmMedrekRawatJalan.ShowDialog(Me)
                fn_LoadSecurity()
            Catch ex As Exception
                MsgBox("Load Form Detail : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
            Finally
                If Not frmMedrekRawatJalan Is Nothing Then frmMedrekRawatJalan.Dispose()
                frmMedrekRawatJalan = Nothing

                Dim rowHandle As Integer = grvListPasien.LocateByValue(rowHandle, grvListPasien.Columns("NoRegister"), sCode)
                If rowHandle > 0 Then grvListPasien.FocusedRowHandle = rowHandle

                If sStatusSave = "NEW" Then
                    sStatusSave = "NONE"
                End If
            End Try
        Else
            Dim frmMedrekRawatJalan As New frmMedrekRawatJalan
            Try
                frmMedrekRawatJalan.fn_LoadIdentias(txtBERATBADAN.Text, txtTB.Text, txtALERGIOBAT.Text, lblRegister.Text, lblTanggalLahirUsia.Text, lblNamaPasien.Text, lblRM.Text, lblDPJP.Text, grdKDDEPARTMENT.Text, lblSIP.Text, lblPenjamin.Text, lblKodeBooking.Text, grdKDDEPARTMENT.EditValue, lblKDDOCTOR.Text, lblNOMORSEP.Text, lblNOKARTUBPJS.Text, txtDIAGNOSAUTAMA.Text, deDATELAHIR.DateTime, IIf(lblJK.Text = "L", "1", "2"))
                frmMedrekRawatJalan.LoadMe(FORM_MODE.FORM_MODE_ADD)
                frmMedrekRawatJalan.ShowDialog(Me)
                fn_LoadSecurity()
            Catch ex As Exception
                MsgBox("Load Form Detail : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
            Finally
                If Not frmMedrekRawatJalan Is Nothing Then frmMedrekRawatJalan.Dispose()
                frmMedrekRawatJalan = Nothing

                Dim rowHandle As Integer = grvListPasien.LocateByValue(rowHandle, grvListPasien.Columns("NoRegister"), sCode)
                If rowHandle > 0 Then grvListPasien.FocusedRowHandle = rowHandle

                If sStatusSave = "NEW" Then
                    sStatusSave = "NONE"
                End If
            End Try
        End If
    End Sub
    Private Sub AsesmenMedisIGDToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AsesmenMedisIGDToolStripMenuItem.Click
        If lblRegister.Text = "" Then
            MsgBox("Pasien Belum dipilih, Silahkan kembali ke List Pasien", MsgBoxStyle.Exclamation, Me.Text)
            fn_EmptyMe()
            Exit Sub
        End If
        If lblRegister.Text = "Nul" Then
            MsgBox("Pasien Belum dipilih, Silahkan kembali ke List Pasien", MsgBoxStyle.Exclamation, Me.Text)
            fn_EmptyMe()
            Exit Sub
        End If
        If grvListPasien.GetFocusedRowCellValue("Selesai") = "Y" Then
            MsgBox("Sudah Selesai tidak dapat dirubah", MsgBoxStyle.Information, Me.Text)
            Exit Sub
        End If

        Dim frmEMedrekIDG_01 As New frmEMedrekIDG_01
        Try
            frmEMedrekIDG_01.LoadMe(lblRegister.Text, grdKDDEPARTMENT.EditValue, lblNOMORSEP.Text, lblKDDOCTOR.Text, lblRM.Text, lblNamaPasien.Text, lblJK.Text, deDATELAHIR.DateTime, "-", lblPenjamin.Text, grdKDDEPARTMENT.Text, lblSIP.Text, deDATE_MASUK.DateTime, lblNOKARTUBPJS.Text, lblKTP.Text, lblTanggalLahirUsia.Text)
            frmEMedrekIDG_01.ShowDialog(Me)
            fn_LoadSecurity()
        Catch ex As Exception
            MsgBox("Load Form Detail : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
        Finally
            If Not frmEMedrekIDG_01 Is Nothing Then frmEMedrekIDG_01.Dispose()
            frmEMedrekIDG_01 = Nothing

            Dim rowHandle As Integer = grvListPasien.LocateByValue(rowHandle, grvListPasien.Columns("NoRegister"), sCode)
            If rowHandle > 0 Then grvListPasien.FocusedRowHandle = rowHandle

            If sStatusSave = "NEW" Then
                sStatusSave = "NONE"
            End If
        End Try
    End Sub
    Private Sub AsesmenAwalPerawatIGDToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AsesmenAwalPerawatIGDToolStripMenuItem.Click
        If lblRegister.Text = "" Then
            MsgBox("Pasien Belum dipilih, Silahkan kembali ke List Pasien", MsgBoxStyle.Exclamation, Me.Text)
            fn_EmptyMe()
            Exit Sub
        End If
        If lblRegister.Text = "Nul" Then
            MsgBox("Pasien Belum dipilih, Silahkan kembali ke List Pasien", MsgBoxStyle.Exclamation, Me.Text)
            fn_EmptyMe()
            Exit Sub
        End If
        If grvListPasien.GetFocusedRowCellValue("Selesai") = "Y" Then
            MsgBox("Sudah Selesai tidak dapat dirubah", MsgBoxStyle.Information, Me.Text)
            Exit Sub
        End If

        Dim frmEMedrekIDG_02_New As New frmEMedrekIDG_02_New
        Try
            frmEMedrekIDG_02_New.LoadRegister(lblRegister.Text, lblRM.Text, lblNamaPasien.Text, lblJK.Text, deDATELAHIR.DateTime.ToString("dd-MM-yyyy"), deDATE_MASUK.DateTime.ToString("dd-MM-yyyy HH:mm:ss"), HitungUmur(deDATELAHIR.DateTime, deDATE_MASUK.DateTime))
            'frmEMedrekIDG_02_New.LoadMe(lblRegister.Text)
            frmEMedrekIDG_02_New.ShowDialog(Me)
            fn_LoadSecurity()
        Catch ex As Exception
            MsgBox("Load Form Detail : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
        Finally
            If Not frmEMedrekIDG_02_New Is Nothing Then frmEMedrekIDG_02_New.Dispose()
            frmEMedrekIDG_02_New = Nothing
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
                        If fn_subcek(.Rows(iLoop)("NOMORANTRIAN"), 4) = False Then
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
    Private Function fn_subcek(ByVal Antrian As String, ByVal Taksid As Integer) As Boolean
        Try
            fn_subcek = False

            If fn_CEKADAANTRIAN(Antrian) = False Then
                fn_subcek = True
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
                fn_SaveTaksIDPoli(Antrian)
                fn_subcek = True
            Else
                fn_subcek = True
            End If

        Catch oErr As Exception
            fn_subcek = False
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
    Public Sub fn_SaveTaksIDPoli(ByVal sKODEBOOKING As String)
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
            SQL &= ",4 "
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
    Private Sub GERDQToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles GERDQToolStripMenuItem.Click
        If lblRegister.Text = "" Then
            MsgBox("Pasien Belum dipilih, Silahkan kembali ke List Pasien", MsgBoxStyle.Exclamation, Me.Text)
            fn_EmptyMe()
            Exit Sub
        End If
        If lblRegister.Text = "Nul" Then
            MsgBox("Pasien Belum dipilih, Silahkan kembali ke List Pasien", MsgBoxStyle.Exclamation, Me.Text)
            fn_EmptyMe()
            Exit Sub
        End If
        If grvListPasien.GetFocusedRowCellValue("Selesai") = "Y" Then
            MsgBox("Sudah Selesai tidak dapat dirubah", MsgBoxStyle.Information, Me.Text)
            Exit Sub
        End If

        Dim oGerd As New Inventory.clsDigital_IGD_01_GERD
        Dim dsGerd = oGerd.GetDataByKdreg(lblRegister.Text)
        If dsGerd Is Nothing Then
            Dim frmDigital_IGD_01_GERD As New frmDigital_IGD_01_GERD
            Try
                frmDigital_IGD_01_GERD.LoadMe(FORM_MODE.FORM_MODE_ADD, lblRegister.Text, lblNamaPasien.Text, lblRM.Text)
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
                frmDigital_IGD_01_GERD.LoadMe(FORM_MODE.FORM_MODE_EDIT, lblRegister.Text, lblNamaPasien.Text, lblRM.Text, dsGerd.KDGERD)
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
    ' Private Sub TerapiToolStripMenuItem_Click(sender As Object, e As EventArgs)
    'If lblRegister.Text = "" Then
    '    MsgBox("Pasien Belum dipilih, Silahkan kembali ke List Pasien", MsgBoxStyle.Exclamation, Me.Text)
    '    fn_EmptyMe()
    '    Exit Sub
    'End If
    'If lblRegister.Text = "Nul" Then
    '    MsgBox("Pasien Belum dipilih, Silahkan kembali ke List Pasien", MsgBoxStyle.Exclamation, Me.Text)
    '    fn_EmptyMe()
    '    Exit Sub
    'End If
    'If grvListPasien.GetFocusedRowCellValue("Selesai") = "Y" Then
    '    MsgBox("Sudah Selesai tidak dapat dirubah", MsgBoxStyle.Information, Me.Text)
    '    Exit Sub
    'End If

    'Dim frmResepIGD As New frmResepIGD
    'Try
    '    frmResepIGD.LoadMe(lblRegister.Text, grdKDDEPARTMENT.EditValue, lblNOMORSEP.Text, lblKDDOCTOR.Text, lblRM.Text, lblNamaPasien.Text, lblJK.Text, deDATELAHIR.DateTime, "-", lblPenjamin.Text, grdKDDEPARTMENT.Text, lblSIP.Text, deDATE_MASUK.DateTime, lblNOKARTUBPJS.Text, lblKTP.Text)
    '    frmResepIGD.ShowDialog(Me)
    '    fn_LoadSecurity()
    'Catch ex As Exception
    '    MsgBox("Load Form Detail : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
    'Finally
    '    If Not frmResepIGD Is Nothing Then frmResepIGD.Dispose()
    '    frmResepIGD = Nothing
    'End Try
    'End Sub
    Private Sub btnSelesai_Click(sender As Object, e As EventArgs) Handles btnSelesai.Click
        If lblRegister.Text = "Nul" Then
            MsgBox("Silahkan Pilih Pasien", MsgBoxStyle.Exclamation, Me.Text)
            Exit Sub
        End If
        If lblRegister.Text = "" Then
            MsgBox("Silahkan Pilih Pasien", MsgBoxStyle.Exclamation, Me.Text)
            Exit Sub
        End If

        Dim oS_DIGITAL_IGD_01 As New Transaksi.clsDigital_IGD_01
        Dim oS_DIGITAL_IGD_02_NEW As New Transaksi.clsDigital_IGD_02
        Dim oS_DIGITAL_IGD_03 As New Transaksi.clsDigital_IGD_03

        Dim dsAsesmenMedis = oS_DIGITAL_IGD_01.GetData(lblRegister.Text)
        If dsAsesmenMedis Is Nothing Then
            MsgBox("Asesmen Medis IGD Belum di Input !!!", MsgBoxStyle.Exclamation, Me.Text)
            Exit Sub
        End If

        Dim Perawat As Boolean = False

        Dim dsAsesmenPerawat = oS_DIGITAL_IGD_02_NEW.GetData(lblRegister.Text)
        If dsAsesmenPerawat IsNot Nothing Then
            Perawat = True
        End If

        Dim dsAsesmenBidan = oS_DIGITAL_IGD_03.GetData(lblRegister.Text)
        If dsAsesmenBidan IsNot Nothing Then
            Perawat = True
        End If

        If Perawat = False Then
            MsgBox("Asesmen Perawat IGD atau Asesmen Vonek IGD Belum di Input !!!", MsgBoxStyle.Exclamation, Me.Text)
            Exit Sub
        End If

        If fn_SaveObatYangdiInputFaramsi(lblRegister.Text) = False Then
            If MsgBox("Apakah benar pasien tersebut tidak di beri obat pulang/obat ranap?", MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, Me.Text) = MsgBoxResult.No Then Exit Sub
        End If

        If lblSelesai.Text <> "Y" Then
            If sAlamatSimpanPDFAsesmenIGD = "" Then
                MsgBox("Maap alamat simpan pdf kosong", MsgBoxStyle.Exclamation, Me.Text)
            Else
                If fn_SavePDFCasmix() = True Then
                    fn_Selesai("Y")
                End If
            End If
        Else
            MsgBox("Maap Sudah di Klik Tombol Selesai", MsgBoxStyle.Exclamation, Me.Text)
        End If
    End Sub
    Private Function fn_SavePDFCasmix() As Boolean
        Try
            fn_SavePDFCasmix = False

            Dim oIGD As New Transaksi.clsDigital_IGD_01
            Dim oIGD_GerdQ As New Inventory.clsDigital_IGD_01_GERD

            Dim FolderSimpan = "C:/UPLOADSELESAI/"

            If Not Directory.Exists(FolderSimpan) Then
                Directory.CreateDirectory(FolderSimpan)
            Else
                DeleteDirectory(FolderSimpan)
                Directory.CreateDirectory(FolderSimpan)
            End If

            Dim ds = oIGD.GetData(lblRegister.Text)
            Dim dsGerQ = oIGD_GerdQ.GetDataByKdreg(lblRegister.Text)

            If ds IsNot Nothing Then
                sASESEMEN_IGD = ds.SURVEY_PERUT_1
                sUSIADIASESMENIGD = deDATELAHIR.DateTime.ToString("dd-MM-yyyy") & " (" & HitungUmur(deDATELAHIR.DateTime, deDATE.DateTime) & ")"

                Dim rpt As New xtraReportFormulirIGD1

                rpt.ShowPrintMarginsWarning = False
                rpt.Watermark.Text = sWATERMARK

                rpt.BindingSource.DataSource = ds
                rpt.ExportToPdf(FolderSimpan & "ASESMENMEDISIGD_" & ds.KDPENDAFTARAN & ".pdf")

                Dim FileToCopy As String
                Dim NewCopy As String

                FileToCopy = FolderSimpan & "ASESMENMEDISIGD_" & ds.KDPENDAFTARAN & ".pdf"
                NewCopy = sAlamatSimpanPDFAsesmenIGD & "ASESMENMEDISIGD_" & ds.KDPENDAFTARAN & ".pdf"

                If System.IO.File.Exists(NewCopy) = True Then
                    Dim FileToDelete As String = NewCopy
                    Try
                        If System.IO.File.Exists(FileToDelete) = True Then
                            System.IO.File.Delete(FileToDelete)
                        End If
                    Catch ex As Exception

                    End Try
                End If

                If System.IO.File.Exists(FileToCopy) = True Then
                    Try
                        System.IO.File.Copy(FileToCopy, NewCopy)
                    Catch ex As Exception

                    End Try

                    MessageBox.Show("Berhasil Asemen Medis Kirim ke Unit Casmix dan di Tutup")
                    fn_SavePDFCasmix = True
                Else
                    fn_SavePDFCasmix = False
                    MessageBox.Show("Gagal Asemen Medis Kirim ke Unit Casmix")
                End If
            Else
                MessageBox.Show("Asesmen Medis Belum dibuat, Silahkan Buat Asemen Medis terlebih Dahulu")
            End If

            If dsGerQ IsNot Nothing Then
                Dim rpt As New xtraGerd_Q

                rpt.ShowPrintMarginsWarning = False
                rpt.Watermark.Text = sWATERMARK

                rpt.bindingSource.DataSource = dsGerQ
                rpt.ExportToPdf(FolderSimpan & "GERDQ_" & dsGerQ.KDPENDAFTARAN & ".pdf")

                Dim FileToCopy As String
                Dim NewCopy As String

                FileToCopy = FolderSimpan & "GERDQ_" & dsGerQ.KDPENDAFTARAN & ".pdf"
                NewCopy = sAlamatSimpanPDFAsesmenIGD & "GERDQ_" & dsGerQ.KDPENDAFTARAN & ".pdf"

                If System.IO.File.Exists(NewCopy) = True Then
                    Dim FileToDelete As String = NewCopy

                    If System.IO.File.Exists(FileToDelete) = True Then
                        System.IO.File.Delete(FileToDelete)
                    End If
                End If

                If System.IO.File.Exists(FileToCopy) = True Then
                    System.IO.File.Copy(FileToCopy, NewCopy)
                    fn_SavePDFCasmix = True
                Else
                    fn_SavePDFCasmix = False
                    MessageBox.Show("Gagal Ger Q Kirim ke Unit Casmix")
                End If
            End If
        Catch oErr As Exception
            fn_SavePDFCasmix = False
            MsgBox("Kirim Casmix" & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Function
    Private Sub fn_Selesai(ByVal KETERANGAN As String)
        Try
            If KETERANGAN = "" Then
                Dim oS_DIGITAL_IGD_03 As New Transaksi.clsDigital_IGD_03
                Dim dsCekTombolWarnaPink = oS_DIGITAL_IGD_03.GetData(lblRegister.Text)
                If dsCekTombolWarnaPink IsNot Nothing Then
                    KETERANGAN = "X"
                    'Else
                    '    If KETERANGAN <> "Y" Then
                    '        KETERANGAN = "N"
                    '    End If
                End If
            End If

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
            SQL &= "S_SELESAI "
            SQL &= "WHERE "
            SQL &= "KDPENDAFTARAN = '" & lblRegister.Text & "' "

            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "GETDATASELESAI")

            Dim Cek As Boolean = False

            For iLoop As Integer = 0 To ds.Tables("GETDATASELESAI").Rows.Count - 1
                Cek = True
            Next

            If Cek = False Then
                SQL = "INSERT "
                SQL &= "INTO "
                SQL &= "S_SELESAI "
                SQL &= "( "
                SQL &= "KDPENDAFTARAN "
                SQL &= ",KETERANGAN "
                SQL &= ") "
                SQL &= "VALUES "
                SQL &= "( "
                SQL &= "'" & lblRegister.Text & "' "
                SQL &= ",'" & KETERANGAN & "' "
                SQL &= ") "

                oComm.Connection = oConn
                oComm.CommandText = SQL
                oComm.CommandTimeout = 120
                oComm.CommandType = CommandType.Text

                da = New SqlDataAdapter(oComm)
                da.Fill(ds, "INSERTSELESAI")
            Else
                SQL = "UPDATE S_SELESAI SET KETERANGAN = '" & KETERANGAN & "' WHERE KDPENDAFTARAN = '" & lblRegister.Text & "' "

                oComm.Connection = oConn
                oComm.CommandText = SQL
                oComm.CommandTimeout = 120
                oComm.CommandType = CommandType.Text

                da = New SqlDataAdapter(oComm)
                da.Fill(ds, "INSERTSELESAI")
            End If

            If oConn.State = ConnectionState.Open Then
                oConn.Close()
            End If

            fn_LoadData()

        Catch oErr As Exception
            MsgBox("Update Data : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub AsesmenKebidananToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AsesmenKebidananToolStripMenuItem.Click
        If lblRegister.Text = "" Then
            MsgBox("Pasien Belum dipilih, Silahkan kembali ke List Pasien", MsgBoxStyle.Exclamation, Me.Text)
            fn_EmptyMe()
            Exit Sub
        End If
        If lblRegister.Text = "Nul" Then
            MsgBox("Pasien Belum dipilih, Silahkan kembali ke List Pasien", MsgBoxStyle.Exclamation, Me.Text)
            fn_EmptyMe()
            Exit Sub
        End If
        If grvListPasien.GetFocusedRowCellValue("Selesai") = "Y" Then
            MsgBox("Sudah Selesai tidak dapat dirubah", MsgBoxStyle.Information, Me.Text)
            Exit Sub
        End If

        Dim frmEMedrekIDG_03 As New frmEMedrekIDG_03
        Try
            frmEMedrekIDG_03.LoadMe(lblRegister.Text, lblRM.Text, lblNamaPasien.Text, deDATELAHIR.DateTime.ToString("dd-MM-yyyy"), deDATE_MASUK.DateTime.ToString("dd-MM-yyyy HH:mm:ss"), lblDPJP.Text)
            frmEMedrekIDG_03.ShowDialog(Me)
            fn_LoadSecurity()
        Catch ex As Exception
            MsgBox("Load Form Detail : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
        Finally
            If Not frmEMedrekIDG_03 Is Nothing Then frmEMedrekIDG_03.Dispose()
            frmEMedrekIDG_03 = Nothing

            If sStatusSaveVonekN = True Then
                fn_Selesai("X")
            End If
        End Try
    End Sub
    Private Sub TransferInternalToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles TransferInternalToolStripMenuItem.Click
        If lblRegister.Text = "" Then
            MsgBox("Pasien Belum dipilih, Silahkan kembali ke List Pasien", MsgBoxStyle.Exclamation, Me.Text)
            fn_EmptyMe()
            Exit Sub
        End If
        If lblRegister.Text = "Nul" Then
            MsgBox("Pasien Belum dipilih, Silahkan kembali ke List Pasien", MsgBoxStyle.Exclamation, Me.Text)
            fn_EmptyMe()
            Exit Sub
        End If

        Dim frmTransferInternalList As New frmTransferInternalList
        Try
            frmTransferInternalList.fn_LoadMe(lblRegister.Text, IIf(lblJK.Text = "L", "Laki-laki", "Perempuan"), lblKDDOCTOR.Text, lblRM.Text, lblNamaPasien.Text, deDATELAHIR.DateTime, lblKamar.Text, deDATE_MASUK.DateTime)
            frmTransferInternalList.ShowDialog(Me)
            fn_LoadSecurity()
        Catch ex As Exception
            MsgBox("Load Form Detail : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
        Finally
            If Not frmEMedrekIDG_03 Is Nothing Then frmTransferInternalList.Dispose()
            frmTransferInternalList = Nothing

        End Try
    End Sub
#End Region
    Private Function fn_SaveObatYangdiInputFaramsi(ByVal NoRegister As String) As Boolean
        Try
            fn_SaveObatYangdiInputFaramsi = True

            Dim sTindakan As Integer = 0
            Dim sTerapi As Integer = 0
            Dim sGRANDTOTAL As Decimal = 0
            Dim oS_DIGITAL_IGD_01 As New Transaksi.clsDigital_IGD_01
            Dim dsCek = oS_DIGITAL_IGD_01.GetData(NoRegister)
            Dim listObatPulang As New List(Of String)
            Dim listObatIGD As New List(Of String)
            Dim listObatRanap As New List(Of String)
            Dim oKoding As New Admission.clsKoding
            Dim oReq_Recipe As New Transaksi.clsReq_Recipe

            Dim arrDetail = oReq_Recipe.GetStructureDetailList

            If dsCek IsNot Nothing Then
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
                SQL &= "B.* "
                SQL &= ",KATEGOR_IGD = ISNULL((B.BATCH), '') "
                SQL &= "FROM "
                SQL &= "S_RECIPE_H A "
                SQL &= "INNER JOIN S_RECIPE_D B "
                SQL &= "ON A.KDRECIPE = B.KDRECIPE "
                SQL &= "WHERE "
                SQL &= "A.KDREG = '" & NoRegister & "' "

                oComm.Connection = oConn
                oComm.CommandText = SQL
                oComm.CommandTimeout = 120
                oComm.CommandType = CommandType.Text

                da = New SqlDataAdapter(oComm)
                da.Fill(ds, "S_RECIPE_H01")

                If oConn.State = ConnectionState.Open Then
                    oConn.Close()
                End If

                Dim i_ObatIGD As Integer = 100
                Dim i_ObatRanap As Integer = 200

                Dim arrDetailPenunjang = oReq_Recipe.GetStructureDetailPenunjangList

                For Each xloop In oS_DIGITAL_IGD_01.GetDataDetailPenunjang(dsCek.KDPENDAFTARAN)
                    Dim dsDetail = oReq_Recipe.GetStructureDetailPenunjang
                    With dsDetail
                        .SEQ = xloop.SEQ
                        .KDPENDAFTARAN = dsCek.KDPENDAFTARAN
                        .PENANGANAN = xloop.PENANGANAN
                    End With
                    arrDetailPenunjang.Add(dsDetail)
                    sTindakan += 1
                Next

                For Each xloop In oS_DIGITAL_IGD_01.GetDataDetailResepPulang("IGD_" & dsCek.KDPENDAFTARAN)
                    Dim dsDetail = oReq_Recipe.GetStructureDetail
                    With dsDetail
                        .SEQ = xloop.SEQ
                        .KDREQRECIPE = xloop.KDREQRECIPE
                        .NAMAOBAT = xloop.NAMAOBAT
                        .SATUAN = xloop.SATUAN
                        .SIGNA = xloop.SIGNA
                        .CARAPAKAI = xloop.CARAPAKAI
                        .QTY = xloop.QTY
                        .PRICE = xloop.PRICE
                        .GRANDTOTAL = xloop.GRANDTOTAL
                        .ROMAWI = xloop.ROMAWI
                        .REMARKS_DOKTER = xloop.REMARKS_DOKTER
                        .KDITEM = xloop.KDITEM
                        .KDUOM = xloop.KDUOM
                        .KDSIGNA = xloop.KDSIGNA
                        .KDCARAPAKAI = xloop.KDCARAPAKAI
                        .QTY_PERUBAHAN = xloop.QTY_PERUBAHAN
                        .REMARKS_FARMASI = "OBAT PULANG"
                        .DOSIS = ""
                        sGRANDTOTAL += xloop.GRANDTOTAL
                    End With

                    sTerapi += 1

                    arrDetail.Add(dsDetail)

                    listObatPulang.Add(xloop.NAMAOBAT & " No " & xloop.ROMAWI & " " & xloop.SATUAN & " " & xloop.SIGNA & " " & xloop.CARAPAKAI & " " & xloop.REMARKS_DOKTER)

                Next

                For iLoop As Integer = 0 To ds.Tables("S_RECIPE_H01").Rows.Count - 1
                    With ds.Tables("S_RECIPE_H01")
                        Dim KDITEM As String = .Rows(iLoop)("KDITEM")
                        Dim KDUOM As String = .Rows(iLoop)("KDUOM")
                        Dim KDSIGNA As String = .Rows(iLoop)("KDSIGNA")
                        Dim KDCP As String = .Rows(iLoop)("KDCP")
                        Dim QTY As String = .Rows(iLoop)("QTY")
                        Dim PRICE As Decimal = .Rows(iLoop)("PRICE")
                        Dim GRANDTOTAL As Decimal = .Rows(iLoop)("GRANDTOTAL")
                        sGRANDTOTAL += .Rows(iLoop)("GRANDTOTAL")

                        If .Rows(iLoop)("KATEGOR_IGD") = "RESEP SELAMA IGD" Then
                            sTerapi += 1

                            listObatIGD.Add(frmEMedrekIDG_01.fn_LoadITEM(.Rows(iLoop)("KDITEM")) & " No " & IntegerToRoman(CInt(.Rows(iLoop)("QTY"))) & " " & frmEMedrekIDG_01.fn_LoadUOMDESCRIPTION(.Rows(iLoop)("KDUOM")) & " " & frmEMedrekIDG_01.fn_LoadSIGNA(.Rows(iLoop)("KDSIGNA")) & " " & frmEMedrekIDG_01.fn_LoadCARAPAKAI(.Rows(iLoop)("KDCP")))

                            Dim dsDetail = oReq_Recipe.GetStructureDetail
                            With dsDetail
                                .SEQ = i_ObatIGD
                                .KDREQRECIPE = "IGD_" & dsCek.KDPENDAFTARAN
                                .NAMAOBAT = frmEMedrekIDG_01.fn_LoadITEM(KDITEM)
                                .SATUAN = frmEMedrekIDG_01.fn_LoadUOMDESCRIPTION(KDUOM)
                                .SIGNA = frmEMedrekIDG_01.fn_LoadSIGNA(KDSIGNA)
                                .CARAPAKAI = frmEMedrekIDG_01.fn_LoadCARAPAKAI(KDCP)
                                .QTY = CDec(QTY)
                                .PRICE = CDec(PRICE)
                                .GRANDTOTAL = CDec(GRANDTOTAL)
                                .ROMAWI = IntegerToRoman(CInt(QTY))
                                .REMARKS_DOKTER = ""
                                .KDITEM = KDITEM
                                .KDUOM = KDUOM
                                .KDSIGNA = KDSIGNA
                                .KDCARAPAKAI = KDCP
                                .QTY_PERUBAHAN = 0
                                .REMARKS_FARMASI = "OBAT IGD"
                                .DOSIS = ""
                                i_ObatIGD += 1
                            End With

                            arrDetail.Add(dsDetail)

                        ElseIf .Rows(iLoop)("KATEGOR_IGD") = "RESEP RAWAT INAP" Then
                            listObatRanap.Add(frmEMedrekIDG_01.fn_LoadITEM(.Rows(iLoop)("KDITEM")) & " No " & IntegerToRoman(CInt(.Rows(iLoop)("QTY"))) & " " & frmEMedrekIDG_01.fn_LoadUOMDESCRIPTION(.Rows(iLoop)("KDUOM")) & " " & frmEMedrekIDG_01.fn_LoadSIGNA(.Rows(iLoop)("KDSIGNA")) & " " & frmEMedrekIDG_01.fn_LoadCARAPAKAI(.Rows(iLoop)("KDCP")))

                            Dim dsDetail = oReq_Recipe.GetStructureDetail
                            With dsDetail
                                .SEQ = i_ObatRanap
                                .KDREQRECIPE = "IGD_" & dsCek.KDPENDAFTARAN
                                .NAMAOBAT = frmEMedrekIDG_01.fn_LoadITEM(KDITEM)
                                .SATUAN = frmEMedrekIDG_01.fn_LoadUOMDESCRIPTION(KDUOM)
                                .SIGNA = frmEMedrekIDG_01.fn_LoadSIGNA(KDSIGNA)
                                .CARAPAKAI = frmEMedrekIDG_01.fn_LoadCARAPAKAI(KDCP)
                                .QTY = CDec(QTY)
                                .PRICE = CDec(PRICE)
                                .GRANDTOTAL = CDec(GRANDTOTAL)
                                .ROMAWI = IntegerToRoman(CInt(QTY))
                                .REMARKS_DOKTER = ""
                                .KDITEM = KDITEM
                                .KDUOM = KDUOM
                                .KDSIGNA = KDSIGNA
                                .KDCARAPAKAI = KDCP
                                .QTY_PERUBAHAN = 0
                                .REMARKS_FARMASI = "OBAT RANAP"
                                .DOSIS = ""
                                i_ObatRanap += 1
                            End With

                            arrDetail.Add(dsDetail)
                        End If
                    End With
                Next

                Dim ObatPulang As String = ""
                Dim ObatIGD As String = ""
                Dim Obatranap As String = ""
                Dim CekInputFarmasi As Boolean = False

                If listObatIGD.Count > 0 Then
                    ObatIGD = "OBAT SELAMA IGD :" & vbCrLf & String.Join(", ", listObatIGD.ToArray)
                End If
                If listObatRanap.Count > 0 Then
                    CekInputFarmasi = True
                    Obatranap = "OBAT RANAP :" & vbCrLf & String.Join(", ", listObatRanap.ToArray)
                End If
                If listObatPulang.Count > 0 Then
                    CekInputFarmasi = True
                    ObatPulang = "OBAT PULANG :" & vbCrLf & String.Join(", ", listObatPulang.ToArray)
                End If

                Dim arrDetailDiagnosaPenyertaTindakan = oKoding.GetStructureDetail_Terapi_TindakanList

                oS_DIGITAL_IGD_01.UpdateDataObat(dsCek.KDPENDAFTARAN, ObatIGD & vbCrLf & Obatranap & vbCrLf & ObatPulang)

                oS_DIGITAL_IGD_01.UpdateGrandTotal("IGD_" & dsCek.KDPENDAFTARAN, sGRANDTOTAL)
                oS_DIGITAL_IGD_01.UpdateDataFarmasi("IGD_" & dsCek.KDPENDAFTARAN, arrDetail)

                If CekInputFarmasi = True Then
                    If sTerapi >= sTindakan Then
                        For Each xloop In arrDetail.Where(Function(x) x.REMARKS_FARMASI <> "OBAT RANAP")
                            Dim dsDetail = oKoding.GetStructureDetail_Terapi_Tindakan
                            With dsDetail
                                Dim TINDAKAN As String = String.Empty

                                .SEQ = xloop.SEQ
                                .KDKODING = "IGD_" & dsCek.KDPENDAFTARAN
                                .TERAPI = frmEMedrekIDG_01.fn_LoadITEM(xloop.KDITEM) & " " & xloop.REMARKS_DOKTER

                                For Each xp In arrDetailPenunjang
                                    If xloop.SEQ = xp.SEQ Then
                                        TINDAKAN = xp.PENANGANAN
                                        Exit For
                                    End If
                                Next

                                .TINDAKAN = TINDAKAN
                            End With
                            arrDetailDiagnosaPenyertaTindakan.Add(dsDetail)
                        Next
                    Else
                        For Each xp In arrDetailPenunjang
                            Dim dsDetail = oKoding.GetStructureDetail_Terapi_Tindakan
                            With dsDetail
                                Dim TERAPI As String = String.Empty

                                .SEQ = xp.SEQ
                                .KDKODING = "IGD_" & dsCek.KDPENDAFTARAN

                                For Each xloop In arrDetail.Where(Function(x) x.REMARKS_FARMASI <> "OBAT RANAP")
                                    If xp.SEQ = xloop.SEQ Then
                                        TERAPI = frmEMedrekIDG_01.fn_LoadITEM(xloop.KDITEM) & " " & xloop.REMARKS_DOKTER
                                        Exit For
                                    End If
                                Next

                                .TERAPI = TERAPI
                                .TINDAKAN = xp.PENANGANAN
                            End With
                            arrDetailDiagnosaPenyertaTindakan.Add(dsDetail)
                        Next

                    End If

                    If arrDetailDiagnosaPenyertaTindakan.Count > 0 Then
                        fn_UpdateDetil(arrDetailDiagnosaPenyertaTindakan.FirstOrDefault.KDKODING, arrDetailDiagnosaPenyertaTindakan)
                    End If
                Else
                    fn_SaveObatYangdiInputFaramsi = False
                End If
            End If
        Catch oErr As Exception
            fn_SaveObatYangdiInputFaramsi = False
            MsgBox("Simpan Data Resep Farmasi: " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Function
    Private Sub fn_UpdateDetil(ByVal Kode As String, ByVal entityDetailDiagnosisPenyertaTindakan As List(Of S_KODING_TERAPI_DIAGNOSISPENYERTA))
        Try
            Dim oConn As New SqlConnection
            Dim oComm As New SqlCommand
            Dim da As SqlDataAdapter
            Dim ds As New DataSet
            Dim SQL As String
            oConn = New SqlConnection(Decrypt(My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Software\SIMRS\SW\", "Database", "").ToString()))

            If oConn.State = ConnectionState.Closed Then
                oConn.Open()
            End If

            SQL = "DELETE FROM S_KODING_TERAPI_DIAGNOSISPENYERTA WHERE KDKODING = '" & Kode & "' "

            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "S_KODING_TERAPI_DIAGNOSISPENYERTA_DELETE")

            For Each xloop In entityDetailDiagnosisPenyertaTindakan
                SQL = "INSERT INTO S_KODING_TERAPI_DIAGNOSISPENYERTA (KDKODING, SEQ, TERAPI, TINDAKAN) "
                SQL &= "VALUES ('" & xloop.KDKODING & "', " & xloop.SEQ & ", '" & xloop.TERAPI & "', '" & xloop.TINDAKAN & "') "

                oComm.Connection = oConn
                oComm.CommandText = SQL
                oComm.CommandTimeout = 120
                oComm.CommandType = CommandType.Text

                da = New SqlDataAdapter(oComm)
                da.Fill(ds, "S_KODING_TERAPI_DIAGNOSISPENYERTA_INSERT")
            Next

            If oConn.State = ConnectionState.Open Then
                oConn.Close()
            End If
        Catch oErr As Exception
            MsgBox("S_KODING_TERAPI_DIAGNOSISPENYERTA: " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub HandOverToolStripMenuItem1_Click(sender As Object, e As EventArgs)
        If lblRegister.Text = "" Then
            MsgBox("Pasien Belum dipilih, Silahkan kembali ke List Pasien", MsgBoxStyle.Exclamation, Me.Text)
            fn_EmptyMe()
            Exit Sub
        End If
        If lblRegister.Text = "Nul" Then
            MsgBox("Pasien Belum dipilih, Silahkan kembali ke List Pasien", MsgBoxStyle.Exclamation, Me.Text)
            fn_EmptyMe()
            Exit Sub
        End If

        Dim frmHandOverList As New frmHandOverList
        Try
            frmHandOverList.fn_LoadMe(lblRegister.Text, IIf(lblJK.Text = "L", "Laki-laki", "Perempuan"), lblKDDOCTOR.Text, lblRM.Text, lblNamaPasien.Text, deDATELAHIR.DateTime, lblKamar.Text, deDATE_MASUK.DateTime)
            frmHandOverList.ShowDialog(Me)
            fn_LoadSecurity()
        Catch ex As Exception
            MsgBox("Load Form Detail : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
        Finally
            If Not frmEMedrekIDG_03 Is Nothing Then frmHandOverList.Dispose()
            frmHandOverList = Nothing

        End Try
    End Sub
    Private Sub SBARPerpindahanToolStripMenuItem_Click(sender As Object, e As EventArgs)
        If lblRegister.Text = "" Then
            MsgBox("Pasien Belum dipilih, Silahkan kembali ke List Pasien", MsgBoxStyle.Exclamation, Me.Text)
            fn_EmptyMe()
            Exit Sub
        End If
        If lblRegister.Text = "Nul" Then
            MsgBox("Pasien Belum dipilih, Silahkan kembali ke List Pasien", MsgBoxStyle.Exclamation, Me.Text)
            fn_EmptyMe()
            Exit Sub
        End If

        Dim frmEMedrekRI_31List As New frmEMedrekRI_31List
        Try
            frmEMedrekRI_31List.fn_LoadMe(lblRegister.Text, IIf(lblJK.Text = "L", "Laki-laki", "Perempuan"), lblKDDOCTOR.Text, lblRM.Text, lblNamaPasien.Text, deDATELAHIR.DateTime, lblKamar.Text, deDATE_MASUK.DateTime)
            frmEMedrekRI_31List.ShowDialog(Me)
            fn_LoadSecurity()
        Catch ex As Exception
            MsgBox("Load Form Detail : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
        Finally
            If Not frmEMedrekIDG_03 Is Nothing Then frmEMedrekRI_31List.Dispose()
            frmEMedrekRI_31List = Nothing

        End Try
    End Sub
    Private Sub CPPTToolStripMenuItem_Click(sender As Object, e As EventArgs)
        'If lblRegister.Text = "" Then
        '    MsgBox("Pasien Belum dipilih, Silahkan kembali ke List Pasien", MsgBoxStyle.Exclamation, Me.Text)
        '    fn_EmptyMe()
        '    Exit Sub
        'End If
        'If lblRegister.Text = "Nul" Then
        '    MsgBox("Pasien Belum dipilih, Silahkan kembali ke List Pasien", MsgBoxStyle.Exclamation, Me.Text)
        '    fn_EmptyMe()
        '    Exit Sub
        'End If

        'Dim frmCPPTList As New frmCPPTList
        'Try
        '    frmCPPTList.fn_LoadMe(lblRegister.Text, sisDOkter, IIf(lblJK.Text = "L", "Laki-laki", "Perempuan"), lblRM.Text, lblNamaPasien.Text, deDATELAHIR.DateTime, GetUmurPasien(deDATE_MASUK.DateTime, deDATELAHIR.DateTime), lblPenjamin.Text, lblKamar.Text, lblKDDOCTOR.Text)
        '    frmCPPTList.ShowDialog(Me)
        '    fn_LoadSecurity()
        'Catch ex As Exception
        '    MsgBox("Load Form Detail : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
        'Finally
        '    If Not frmEMedrekIDG_03 Is Nothing Then frmCPPTList.Dispose()
        '    frmCPPTList = Nothing

        'End Try
    End Sub
    Public Function GetUmurPasien(ByVal dateNow As Date, ByVal tgllahir As Date) As String
        Dim years As Long
        Dim months As Long
        Dim days As Long
        Dim yearWord As String
        Dim monthWord As String
        Dim dayWord As String

        ' menghitung tahun
        years = DateDiff("yyyy", tgllahir, dateNow)
        If Month(tgllahir) > Month(dateNow) Then
            years = years - 1
        ElseIf Month(tgllahir) = Month(dateNow) And tgllahir.Day > dateNow.Day Then
            years = years - 1
        ElseIf Month(tgllahir) = Month(dateNow) And tgllahir.Day = dateNow.Day Then
            'GoTo Finish ' jika bulan dan tanggal sama maka perhitungan selesai
        End If
        ' menghitung bulan
        tgllahir = DateAdd("yyyy", years, tgllahir)
        months = DateDiff("m", tgllahir, dateNow)
        If tgllahir.Day > dateNow.Day Then
            months = months - 1
        ElseIf Month(tgllahir) = Month(dateNow) And tgllahir.Day >= dateNow.Day Then
            months = months - 1
        End If
        tgllahir = DateAdd("m", months, tgllahir)
        ' menghitung hari
        days = DateDiff("d", tgllahir, dateNow)

        yearWord = IIf(years = 0, "", years & " Tahun ")
        monthWord = IIf(months = 0, "", months & " Bulan ")
        dayWord = IIf(days = 0, "", days & " Hari ")
        'calculateAge = yearWord & monthWord & dayWord
        'calculateAge = Trim(calculateAge)

        'GetUmurPasien = yearWord & " " & monthWord & " " & dayWord
        GetUmurPasien = yearWord
    End Function
    Private Sub RekonsiliasiObatToolStripMenuItem_Click(sender As Object, e As EventArgs)
        If lblRegister.Text = "" Then
            MsgBox("Pasien Belum dipilih, Silahkan kembali ke List Pasien", MsgBoxStyle.Exclamation, Me.Text)
            fn_EmptyMe()
            Exit Sub
        End If
        If lblRegister.Text = "Nul" Then
            MsgBox("Pasien Belum dipilih, Silahkan kembali ke List Pasien", MsgBoxStyle.Exclamation, Me.Text)
            fn_EmptyMe()
            Exit Sub
        End If

        Dim frmRekonsiliasiObatList As New frmRekonsiliasiObatList
        Try
            frmRekonsiliasiObatList.fn_LoadMe(lblRegister.Text, IIf(lblJK.Text = "L", "Laki-laki", "Perempuan"), lblRM.Text, lblNamaPasien.Text, deDATELAHIR.DateTime, GetUmurPasien(deDATE_MASUK.DateTime, deDATELAHIR.DateTime), lblPenjamin.Text, lblKamar.Text, lblKDDOCTOR.Text)
            frmRekonsiliasiObatList.ShowDialog(Me)
            fn_LoadSecurity()
        Catch ex As Exception
            MsgBox("Load Form Detail : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
        Finally
            If Not frmEMedrekIDG_03 Is Nothing Then frmRekonsiliasiObatList.Dispose()
            frmRekonsiliasiObatList = Nothing

        End Try
    End Sub
    Private Sub KonsulToolStripMenuItem_Click(sender As Object, e As EventArgs)
        'Dim frmKonsulDokter As New frmKonsulDokter
        'Try
        '    frmKonsulDokter.fn_LoadMe(lblRegister.Text, grdDPJP.EditValue)
        '    frmKonsulDokter.ShowDialog(Me)
        '    fn_LoadSecurity()
        'Catch ex As Exception
        '    MsgBox("Load Form Detail : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
        'Finally
        '    If Not frmKonsulDokter Is Nothing Then frmKonsulDokter.Dispose()
        '    frmKonsulDokter = Nothing

        'End Try
    End Sub
    Private Sub CatatanMedisAwalDanDischargePlanningToolStripMenuItem_Click(sender As Object, e As EventArgs)
        'If lblRegister.Text = "" Then
        '    MsgBox("Pasien Belum dipilih, Silahkan kembali ke List Pasien", MsgBoxStyle.Exclamation, Me.Text)
        '    fn_EmptyMe()
        '    Exit Sub
        'End If
        'If lblRegister.Text = "Nul" Then
        '    MsgBox("Pasien Belum dipilih, Silahkan kembali ke List Pasien", MsgBoxStyle.Exclamation, Me.Text)
        '    fn_EmptyMe()
        '    Exit Sub
        'End If

        'Dim oS_DIGITAL_RI_13 As New Transaksi.clsDigital_DischargePlanning
        'Dim ds = oS_DIGITAL_RI_13.GetData(lblRegister.Text)

        'If ds IsNot Nothing Then
        '    Dim frmEMedrekRI_13 As New frmEMedrekRI_13
        '    Try
        '        frmEMedrekRI_13.LoadMe(FORM_MODE.FORM_MODE_ADD, "", lblRegister.Text, lblRM.Text, lblNamaPasien.Text, deDATELAHIR.DateTime, GetUmurPasien(deDATE_MASUK.DateTime, deDATELAHIR.DateTime), IIf(lblJK.Text = "L", "Laki-laki", "Perempuan"), lblPenjamin.Text, lblKamar.Text, lblKDDOCTOR.Text, ds.KDREG)
        '        frmEMedrekRI_13.ShowDialog(Me)
        '        fn_LoadSecurity()
        '    Catch oErr As Exception
        '        MsgBox("Load Form Detail : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        '    Finally
        '        If Not frmEMedrekRI_13 Is Nothing Then frmEMedrekRI_13.Dispose()
        '        frmEMedrekRI_13 = Nothing
        '    End Try
        'Else
        '    Dim frmEMedrekRI_13 As New frmEMedrekRI_13
        '    Try
        '        frmEMedrekRI_13.LoadMe(FORM_MODE.FORM_MODE_ADD, "", lblRegister.Text, lblRM.Text, lblNamaPasien.Text, deDATELAHIR.DateTime, GetUmurPasien(deDATE_MASUK.DateTime, deDATELAHIR.DateTime), IIf(lblJK.Text = "L", "Laki-laki", "Perempuan"), lblPenjamin.Text, lblKamar.Text, lblKDDOCTOR.Text, "")
        '        frmEMedrekRI_13.ShowDialog(Me)
        '        fn_LoadSecurity()
        '    Catch oErr As Exception
        '        MsgBox("Load Form Detail : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        '    Finally
        '        If Not frmEMedrekRI_13 Is Nothing Then frmEMedrekRI_13.Dispose()
        '        frmEMedrekRI_13 = Nothing
        '    End Try
        'End If
    End Sub
    Private Sub SimpleButton3_Click(sender As Object, e As EventArgs) Handles SimpleButton3.Click
        Try
            If lblNOKARTUBPJS.Text = "" Then
                MsgBox("Kartu BPJS Pasien Kosong", MsgBoxStyle.Exclamation, Me.Text)
                Exit Sub
            End If
            If grdDPJP.Text = "" Then Exit Sub

            Dim oDokter As New Reference.clsDoctor
            Dim oKoneksi As New Brigging.clsSetKoneksi
            Dim uTime As Integer = (DateTime.UtcNow - New DateTime(1970, 1, 1, 0, 0, 0)).TotalSeconds
            Dim allData = JObject.Parse(oKoneksi.GetDataIcare(sSecreateKeyVclaim, lblNOKARTUBPJS.Text, oDokter.GetData(grdDPJP.EditValue).VCLAIM_KDDPJP, uTime))

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
    Private Function fn_SaveSKD(ByVal isAdd As Boolean, ByVal NOIID As String, ByVal ALASAN As String, ByVal TINDAKLANJUT As String, ByVal tindaklanjutselesai As String) As String
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
                    .KDPENDAFTARAN = lblRegister.Text
                End Try
                .DATE = deDATE.DateTime
                .ISCATEGORY = 0
                Try
                    .KDDIAGNOSA = oSKD.GetData(NOIID).KDDIAGNOSA
                Catch oErr As Exception
                    .KDDIAGNOSA = "-"
                End Try
                .KDDEPARTMENT = 18
                .KDDOCTOR = grdDPJP.EditValue
                .NOMORRUJUKAN = ""
                .DESCRIPTION = ""
                Try
                    .ISCHEKED = oSKD.GetData(NOIID).ISCHEKED
                Catch oErr As Exception
                    .ISCHEKED = False
                End Try
                .KDUSER = sUserID
                .DATEKONTROL = Now
                .ALASAN = ALASAN
                .TINDAKLANJUT = TINDAKLANJUT
                .TANGGALPERIKSA_TEXT = Now.ToString("ddMMyyyy")
                .KDJADWALDOKTER = 0
                .SEQ = 0
                .REQUEST = ""
                .RESPONSE = ""
                .NOMORSEP = ""
                .SEARCH = Now.ToString("ddMMyyyy")
                .KDCUSTOMER = lblRM.Text
                .ORDERPENUNJANG = ""
                .DATEKONTROL_2 = Now
            End With

            If isAdd = True Then
                fn_SaveSKD = oSKD.InsertData(ds, NOIID)
            Else
                fn_SaveSKD = oSKD.UpdateData(ds)
                If fn_SaveSKD = False Then
                    fn_SaveSKD = ""
                Else
                    fn_SaveSKD = NOIID
                End If
            End If

            If fn_SaveSKD <> "" Then
                frmEMedrekIDG_01.fn_LoadUpdateSudah(lblRegister.Text, tindaklanjutselesai)
            End If
        Catch oErr As Exception
            fn_SaveSKD = ""
            MsgBox("Save SKD" & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Function
    Private Sub MeninggalToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles MeninggalToolStripMenuItem.Click
        If lblRegister.Text = "" Then
            MsgBox("Pasien Belum dipilih, Silahkan kembali ke List Pasien", MsgBoxStyle.Exclamation, Me.Text)
            fn_EmptyMe()
            Exit Sub
        End If
        If lblRegister.Text = "Nul" Then
            MsgBox("Pasien Belum dipilih, Silahkan kembali ke List Pasien", MsgBoxStyle.Exclamation, Me.Text)
            fn_EmptyMe()
            Exit Sub
        End If

        If grvListPasien.GetFocusedRowCellValue("Selesai") = "Y" Then
            MsgBox("Sudah Selesai tidak dapat dirubah", MsgBoxStyle.Information, Me.Text)
            Exit Sub
        End If

        If frmEMedrekIDG_01.fn_ValiadsiSudahAdaSKD(lblRegister.Text) = False Then
            Exit Sub
        End If

        Dim oS_DIGITAL_IGD_01 As New Transaksi.clsDigital_IGD_01
        Dim ds = oS_DIGITAL_IGD_01.GetData(lblRegister.Text)
        If ds IsNot Nothing Then
            If MsgBox("Apakah Pasien Meninggal ?", MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, Me.Text) = MsgBoxResult.No Then Exit Sub

            Dim oSKD As New Admission.clsSKD
            Dim dsSKD = oSKD.GetDataPendaftaranalasan(ds.KDPENDAFTARAN, "MENINGGAL")

            If dsSKD IsNot Nothing Then
                If fn_SaveSKD(False, dsSKD.KDSKD, "MENINGGAL", "MENINGGAL", "MENINGGAL") <> "" Then
                    oS_DIGITAL_IGD_01.UpdateTindakLanjut(ds.KDPENDAFTARAN, "MENINGGAL", False, False, False, False)
                    fn_LoadSecurity()
                End If
            Else
                Dim Kode As String = fn_SaveSKD(True, "", "MENINGGAL", "MENINGGAL", "MENINGGAL")
                If Kode <> "" Then
                    oS_DIGITAL_IGD_01.UpdateTindakLanjut(ds.KDPENDAFTARAN, "MENINGGAL", False, False, False, False)
                    fn_LoadSecurity()
                End If
            End If
        Else
            MsgBox("belum ada input asesmen", MsgBoxStyle.Exclamation, Me.Text)
        End If
    End Sub
    Private Sub RujukToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles RujukToolStripMenuItem.Click
        If lblRegister.Text = "" Then
            MsgBox("Pasien Belum dipilih, Silahkan kembali ke List Pasien", MsgBoxStyle.Exclamation, Me.Text)
            fn_EmptyMe()
            Exit Sub
        End If
        If lblRegister.Text = "Nul" Then
            MsgBox("Pasien Belum dipilih, Silahkan kembali ke List Pasien", MsgBoxStyle.Exclamation, Me.Text)
            fn_EmptyMe()
            Exit Sub
        End If

        If grvListPasien.GetFocusedRowCellValue("Selesai") = "Y" Then
            MsgBox("Sudah Selesai tidak dapat dirubah", MsgBoxStyle.Information, Me.Text)
            Exit Sub
        End If

        If frmEMedrekIDG_01.fn_ValiadsiSudahAdaSKD(lblRegister.Text) = False Then
            Exit Sub
        End If

        Dim oS_DIGITAL_IGD_01 As New Transaksi.clsDigital_IGD_01
        Dim ds = oS_DIGITAL_IGD_01.GetData(lblRegister.Text)
        If ds IsNot Nothing Then
            Dim oSKD As New Admission.clsSKD
            Dim dsSKD = oSKD.GetDataPendaftaranalasan(ds.KDPENDAFTARAN, "RUJUKAN EKSTERNAL")

            If dsSKD IsNot Nothing Then
                Dim frmSKD As New frmSKD
                Try
                    frmSKD.fn_LoadKategori(1)
                    frmSKD.LoadMe(FORM_MODE.FORM_MODE_EDIT, dsSKD.KDSKD)
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
                    frmSKD.fn_LoadNoPendaftaran(ds.KDPENDAFTARAN, lblRM.Text, 18, grdDPJP.EditValue, lblNOMORSEP.Text, lblPenjamin.Text, lblNamaPasien.Text, "")
                    frmSKD.fn_LoadKategori(1)
                    frmSKD.LoadMe(FORM_MODE.FORM_MODE_ADD)
                    frmSKD.ShowDialog(Me)
                Catch ex As Exception
                    MsgBox("Load Form Detail : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            End If

            Dim dsCek = oSKD.GetDataPendaftaranalasan(ds.KDPENDAFTARAN, "RUJUKAN EKSTERNAL")
            If dsCek IsNot Nothing Then
                Dim sTindakLanjut As String = "Tanggal Kontrol : " & dsCek.DATEKONTROL.ToString("dd-MM-yyyy") & vbCrLf & "Alasan : " & dsCek.ALASAN.ToString.Trim & vbCrLf & "Alasan di rujuk : " & dsCek.DESCRIPTION.ToString.Trim
                'txtTINDAKLANJUT.Text = IIf(txtTINDAKLANJUT.Text = "", "", txtTINDAKLANJUT.Text & vbCrLf & vbCrLf) & sTindakLanjut

                oS_DIGITAL_IGD_01.UpdateTindakLanjut(ds.KDPENDAFTARAN, sTindakLanjut, True, False, False, False)
                frmEMedrekIDG_01.fn_LoadUpdateSudah(lblRegister.Text, sTindakLanjut)
                fn_LoadSecurity()
            End If
        Else
            MsgBox("belum ada input asesmen", MsgBoxStyle.Exclamation, Me.Text)
        End If
    End Sub
    Private Sub PulangToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PulangToolStripMenuItem.Click
        If lblRegister.Text = "" Then
            MsgBox("Pasien Belum dipilih, Silahkan kembali ke List Pasien", MsgBoxStyle.Exclamation, Me.Text)
            fn_EmptyMe()
            Exit Sub
        End If
        If lblRegister.Text = "Nul" Then
            MsgBox("Pasien Belum dipilih, Silahkan kembali ke List Pasien", MsgBoxStyle.Exclamation, Me.Text)
            fn_EmptyMe()
            Exit Sub
        End If

        If grvListPasien.GetFocusedRowCellValue("Selesai") = "Y" Then
            MsgBox("Sudah Selesai tidak dapat dirubah", MsgBoxStyle.Information, Me.Text)
            Exit Sub
        End If

        If frmEMedrekIDG_01.fn_ValiadsiSudahAdaSKD(lblRegister.Text) = False Then
            Exit Sub
        End If

        Dim oS_DIGITAL_IGD_01 As New Transaksi.clsDigital_IGD_01
        Dim ds = oS_DIGITAL_IGD_01.GetData(lblRegister.Text)
        If ds IsNot Nothing Then
            If MsgBox("Apakah Pulang ?", MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, Me.Text) = MsgBoxResult.No Then Exit Sub

            Dim oSKD As New Admission.clsSKD
            Dim dsSKD = oSKD.GetDataPendaftaranalasan(ds.KDPENDAFTARAN, "SELESAI PENGOBATAN")

            If dsSKD IsNot Nothing Then
                If fn_SaveSKD(False, dsSKD.KDSKD, "SELESAI PENGOBATAN", "PULANG", "PULANG") <> "" Then
                    oS_DIGITAL_IGD_01.UpdateTindakLanjut(ds.KDPENDAFTARAN, "PULANG", False, False, False, True)
                    fn_LoadSecurity()
                End If
            Else
                Dim kode As String = fn_SaveSKD(True, "", "SELESAI PENGOBATAN", "PULANG", "PULANG")
                If kode <> "" Then
                    oS_DIGITAL_IGD_01.UpdateTindakLanjut(ds.KDPENDAFTARAN, "PULANG", False, False, False, True)
                    fn_LoadSecurity()
                End If
            End If

        Else
            MsgBox("belum ada input asesmen", MsgBoxStyle.Exclamation, Me.Text)
        End If
    End Sub
    Private Sub PulangPaksaToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PulangPaksaToolStripMenuItem.Click
        If lblRegister.Text = "" Then
            MsgBox("Pasien Belum dipilih, Silahkan kembali ke List Pasien", MsgBoxStyle.Exclamation, Me.Text)
            fn_EmptyMe()
            Exit Sub
        End If
        If lblRegister.Text = "Nul" Then
            MsgBox("Pasien Belum dipilih, Silahkan kembali ke List Pasien", MsgBoxStyle.Exclamation, Me.Text)
            fn_EmptyMe()
            Exit Sub
        End If

        If grvListPasien.GetFocusedRowCellValue("Selesai") = "Y" Then
            MsgBox("Sudah Selesai tidak dapat dirubah", MsgBoxStyle.Information, Me.Text)
            Exit Sub
        End If

        If frmEMedrekIDG_01.fn_ValiadsiSudahAdaSKD(lblRegister.Text) = False Then
            Exit Sub
        End If

        Dim oS_DIGITAL_IGD_01 As New Transaksi.clsDigital_IGD_01
        Dim ds = oS_DIGITAL_IGD_01.GetData(lblRegister.Text)
        If ds IsNot Nothing Then
            If MsgBox("Apakah Pulang Paksa?", MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, Me.Text) = MsgBoxResult.No Then Exit Sub

            Dim oSKD As New Admission.clsSKD
            Dim dsSKD = oSKD.GetDataPendaftaranalasan(ds.KDPENDAFTARAN, "SELESAI PENGOBATAN")

            If dsSKD IsNot Nothing Then
                If fn_SaveSKD(False, dsSKD.KDSKD, "SELESAI PENGOBATAN", "PULANG PAKSA", "PULANG PAKSA") <> "" Then
                    oS_DIGITAL_IGD_01.UpdateTindakLanjut(ds.KDPENDAFTARAN, "PULANG PAKSA", False, False, True, False)
                    fn_LoadSecurity()
                End If
            Else
                Dim kode As String = fn_SaveSKD(True, "", "SELESAI PENGOBATAN", "PULANG PAKSA", "PULANG PAKSA")
                If kode <> "" Then
                    oS_DIGITAL_IGD_01.UpdateTindakLanjut(ds.KDPENDAFTARAN, "PULANG PAKSA", False, False, True, False)
                    fn_LoadSecurity()
                End If
            End If

        Else
            MsgBox("belum ada input asesmen", MsgBoxStyle.Exclamation, Me.Text)
        End If
    End Sub
    Private Sub RawatInapToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles RawatInapToolStripMenuItem.Click
        If lblRegister.Text = "" Then
            MsgBox("Pasien Belum dipilih, Silahkan kembali ke List Pasien", MsgBoxStyle.Exclamation, Me.Text)
            fn_EmptyMe()
            Exit Sub
        End If
        If lblRegister.Text = "Nul" Then
            MsgBox("Pasien Belum dipilih, Silahkan kembali ke List Pasien", MsgBoxStyle.Exclamation, Me.Text)
            fn_EmptyMe()
            Exit Sub
        End If

        If grvListPasien.GetFocusedRowCellValue("Selesai") = "Y" Then
            MsgBox("Sudah Selesai tidak dapat dirubah", MsgBoxStyle.Information, Me.Text)
            Exit Sub
        End If

        If frmEMedrekIDG_01.fn_ValiadsiSudahAdaSKD(lblRegister.Text) = False Then
            Exit Sub
        End If

        Dim oS_DIGITAL_IGD_01 As New Transaksi.clsDigital_IGD_01
        Dim ds = oS_DIGITAL_IGD_01.GetData(lblRegister.Text)
        If ds IsNot Nothing Then
            If MsgBox("Apakah Rawat Inap?", MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, Me.Text) = MsgBoxResult.No Then Exit Sub

            Dim frmRemarks As New frmRemarks
            Try
                sRemarks = ""
                frmRemarks.ShowDialog(Me)
            Catch ex As Exception
                MsgBox("Load Form Detail : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
            End Try

            Dim oSKD As New Admission.clsSKD
            Dim dsSKD = oSKD.GetDataPendaftaranalasan(ds.KDPENDAFTARAN, "RAWAT INAP")

            If dsSKD IsNot Nothing Then
                If fn_SaveSKD(False, dsSKD.KDSKD, "RAWAT INAP", "", "RAWAT INAP" & vbCrLf & sRemarks) <> "" Then
                    oS_DIGITAL_IGD_01.UpdateTindakLanjut(ds.KDPENDAFTARAN, "RAWAT INAP" & vbCrLf & sRemarks, False, True, False, False)

                    Dim frmTransferInternalList As New frmTransferInternalList
                    Try
                        frmTransferInternalList.fn_LoadMe(lblRegister.Text, IIf(lblJK.Text = "L", "Laki-laki", "Perempuan"), lblKDDOCTOR.Text, lblRM.Text, lblNamaPasien.Text, deDATELAHIR.DateTime, lblKamar.Text, deDATE_MASUK.DateTime)
                        frmTransferInternalList.ShowDialog(Me)
                        fn_LoadSecurity()
                    Catch ex As Exception
                        MsgBox("Load Form Detail : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
                    Finally
                        If Not frmEMedrekIDG_03 Is Nothing Then frmTransferInternalList.Dispose()
                        frmTransferInternalList = Nothing

                        fn_LoadSecurity()
                    End Try
                End If
            Else
                Dim kode As String = fn_SaveSKD(True, "", "RAWAT INAP", "", "RAWAT INAP" & vbCrLf & sRemarks)
                If kode <> "" Then
                    oS_DIGITAL_IGD_01.UpdateTindakLanjut(ds.KDPENDAFTARAN, "RAWAT INAP" & vbCrLf & sRemarks, False, True, False, False)

                    Dim frmTransferInternalList As New frmTransferInternalList
                    Try
                        frmTransferInternalList.fn_LoadMe(lblRegister.Text, IIf(lblJK.Text = "L", "Laki-laki", "Perempuan"), lblKDDOCTOR.Text, lblRM.Text, lblNamaPasien.Text, deDATELAHIR.DateTime, lblKamar.Text, deDATE_MASUK.DateTime)
                        frmTransferInternalList.ShowDialog(Me)
                        fn_LoadSecurity()
                    Catch ex As Exception
                        MsgBox("Load Form Detail : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
                    Finally
                        If Not frmEMedrekIDG_03 Is Nothing Then frmTransferInternalList.Dispose()
                        frmTransferInternalList = Nothing

                        fn_LoadSecurity()
                    End Try

                End If
            End If

        Else
            MsgBox("belum ada input asesmen", MsgBoxStyle.Exclamation, Me.Text)
        End If
    End Sub
    Private Sub btnBukaSelesai_Click(sender As Object, e As EventArgs) Handles btnBukaSelesai.Click
        If lblSelesai.Text = "Y" Then
            fn_Selesai("")
        Else
            MsgBox("belum klik selesai", MsgBoxStyle.Exclamation, Me.Text)
        End If
    End Sub
    Private Sub EditRawatInapToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles EditRawatInapToolStripMenuItem.Click
        If lblRegister.Text = "No. Register" Then
            fn_EmptyMe()
            Exit Sub
        End If
        If lblRegister.Text = "-" Then
            fn_EmptyMe()
            Exit Sub
        End If
        If lblRegister.Text = "Nul" Then
            fn_EmptyMe()
            Exit Sub
        End If

        Dim Register As String = fn_RegisterNaikRanap(lblRegister.Text)

        If Register = "" Then
            MsgBox("Belum Ada Register Rawat Inap", MsgBoxStyle.Exclamation, Me.Text)
            Exit Sub
        End If

        If grdDPJP.Text = "" Then
            MsgBox("Silahkan Pilih dokter yang akan di Input", MsgBoxStyle.Exclamation, Me.Text)
            grdDPJP.ShowPopup()
            Exit Sub
        End If
        If sisDOkter = False Then
            If grdUSERHADNOVER.Text = "" Then
                MsgBox("User Perawat Kosong", MsgBoxStyle.Exclamation, Me.Text)
                grdUSERHADNOVER.Focus()
                Exit Sub
            End If
        End If

        Try
            Dim TANGGALMASUK As DateTime = deDATE_MASUK.DateTime
            Dim frmRawatInapList As New frmRawatInapList
            frmRawatInapList.fn_LoadMe(False, sisDOkter, lblKDKELASRAWAT.Text, lblNOKARTUBPJS.Text, lblNOMORSEP.Text, Register, grdDPJP.EditValue, fn_kamar(Register), lblPenjamin.Text, lblRM.Text, lblNamaPasien.Text, IIf(lblJK.Text = "L", "Laki-laki", "Perempuan"), deDATELAHIR.DateTime, "", TANGGALMASUK, grdDPJP.Text, "", "", "", "", grdDPJP.Text, lblRegister.Text, grdUSERHADNOVER.Text, grdDPJP.EditValue, Now)
            frmRawatInapList.ShowDialog(Me)

            'If sisDOkter = False Then
            '    grv.SetFocusedRowCellValue("KDCPPT_PERAWAT", Load_SimpanCPPTSelesai(grv.GetFocusedRowCellValue("NoTransaksi")))
            'Else
            '    grv.SetFocusedRowCellValue("KDCPPT", Load_SimpanCPPTSelesai(grv.GetFocusedRowCellValue("NoTransaksi")))
            'End If
        Catch ex As Exception
            MsgBox("Load Form Detail : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
        Finally
            If Not frmEMedrekIDG_03 Is Nothing Then frmRawatInapList.Dispose()
            frmRawatInapList = Nothing
        End Try
    End Sub
    Private Function fn_RegisterNaikRanap(ByVal KDREG As String) As String
        Try
            fn_RegisterNaikRanap = ""

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
            SQL &= "WHERE "
            SQL &= "A.KDREGAWAL = '" & KDREG & "' "

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
                    fn_RegisterNaikRanap = .Rows(iLoop)("KDREG")
                End With
            Next

        Catch oErr As Exception
            fn_RegisterNaikRanap = ""
            MsgBox("Get Data Register Rawat Inap : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Function
    Private Function fn_kamar(ByVal KDREG As String) As String
        Try
            fn_kamar = ""

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
            SQL &= "NamaKamar = A.LOKASI + '-' + A.NAME_DISPLAY "
            SQL &= "FROM "
            SQL &= "M_MEDICAL_ROOM A "
            SQL &= "INNER JOIN S_PENDAFTARAN_H B "
            SQL &= "ON A.KDMROOM = B.KDMROOM "
            SQL &= "WHERE "
            SQL &= "B.KDREG = '" & KDREG & "' "

            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "M_MEDICAL_ROOM")

            If oConn.State = ConnectionState.Open Then
                oConn.Close()
            End If

            For iLoop As Integer = 0 To ds.Tables("M_MEDICAL_ROOM").Rows.Count - 1
                With ds.Tables("M_MEDICAL_ROOM")
                    fn_kamar = .Rows(iLoop)("NamaKamar")
                End With
            Next

        Catch oErr As Exception
            fn_kamar = ""
            MsgBox("Get Data Kamar Rawat Inap : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Function
    Private Sub fn_LoadKDUSER()
        Dim oTemplate As New Reference.clsUnit
        Try
            grdUSERHADNOVER.Properties.DataSource = oTemplate.GetData.Where(Function(x) x.ISACTIVE = True).ToList()
            grdUSERHADNOVER.Properties.ValueMember = "KDUNIT"
            grdUSERHADNOVER.Properties.DisplayMember = "MEMO"
        Catch oErr As Exception
            MsgBox("Load User : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub LaporanObservasiiToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles LaporanObservasiiToolStripMenuItem.Click
        If lblRegister.Text = "" Then
            MsgBox("Silhakan Pilih Pasien", MsgBoxStyle.Information, Me.Text)
            fn_EmptyMe()
            Exit Sub
        End If

        Dim frmLembarObservasiList As New frmLembarObservasiList
        Try
            frmLembarObservasiList.fn_LoadMe(grdDPJP.EditValue, grdKDDEPARTMENT.Text, "", lblNOKARTUBPJS.Text, grdDPJP.Text, lblRegister.Text, lblRM.Text, lblNamaPasien.Text, lblJK.Text, deDATELAHIR.DateTime)
            frmLembarObservasiList.ShowDialog(Me)
        Catch ex As Exception
            MsgBox("Load Form Detail : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
        Finally
            If Not frmLembarObservasiList Is Nothing Then frmLembarObservasiList.Dispose()
            frmLembarObservasiList = Nothing
        End Try
    End Sub
    Private Sub AsesmenAwalKeperawatanRawatJalanToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AsesmenAwalKeperawatanRawatJalanToolStripMenuItem.Click
        If lblRegister.Text = "" Then
            MsgBox("Pasien Belum dipilih, Silahkan kembali ke List Pasien", MsgBoxStyle.Exclamation, Me.Text)
            fn_EmptyMe()
            Exit Sub
        End If
        If lblRegister.Text = "Nul" Then
            MsgBox("Pasien Belum dipilih, Silahkan kembali ke List Pasien", MsgBoxStyle.Exclamation, Me.Text)
            fn_EmptyMe()
            Exit Sub
        End If
        If grvListPasien.GetFocusedRowCellValue("Selesai") = "Y" Then
            MsgBox("Sudah Selesai tidak dapat dirubah", MsgBoxStyle.Information, Me.Text)
            Exit Sub
        End If

        Dim oDigital As New Digital.clsDigital_AskepRawatJalan
        Dim dsCek = oDigital.GetData(lblRegister.Text)

        If dsCek Is Nothing Then
            Dim frmAskepRawatJalan As New frmAskepRawatJalan
            Try
                frmAskepRawatJalan.LoadMe(FORM_MODE.FORM_MODE_ADD, grdUSERHADNOVER.Text, lblRM.Text, lblNamaPasien.Text, lblJK.Text, deDATELAHIR.DateTime, lblRegister.Text)
                frmAskepRawatJalan.ShowDialog(Me)
                fn_LoadSecurity()
            Catch ex As Exception
                MsgBox("Load Form Detail : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
            Finally
                If Not frmAskepRawatJalan Is Nothing Then frmAskepRawatJalan.Dispose()
                frmAskepRawatJalan = Nothing
            End Try
        Else
            Dim frmAskepRawatJalan As New frmAskepRawatJalan
            Try
                frmAskepRawatJalan.LoadMe(FORM_MODE.FORM_MODE_EDIT, grdUSERHADNOVER.Text, lblRM.Text, lblNamaPasien.Text, lblJK.Text, deDATELAHIR.DateTime, lblRegister.Text)
                frmAskepRawatJalan.ShowDialog(Me)
                fn_LoadSecurity()
            Catch ex As Exception
                MsgBox("Load Form Detail : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
            Finally
                If Not frmAskepRawatJalan Is Nothing Then frmAskepRawatJalan.Dispose()
                frmAskepRawatJalan = Nothing
            End Try
        End If
    End Sub
        Private Sub FormulirMPPAToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles FormulirMPPAToolStripMenuItem.Click
        If lblRegister.Text = "" Then
            MsgBox("Silhakan Pilih Pasien", MsgBoxStyle.Information, Me.Text)
            fn_EmptyMe()
            Exit Sub
        End If

        Dim frmDigital_MPPAList As New frmDigital_MPPAList
        Try
            frmDigital_MPPAList.fn_LoadData(lblRegister.Text, grdKDDEPARTMENT.Text, lblRM.Text, lblNamaPasien.Text, lblJK.Text, deDATELAHIR.DateTime)
            frmDigital_MPPAList.ShowDialog(Me)
        Catch ex As Exception
            MsgBox("Load Form Detail : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
        Finally
            If Not frmDigital_MPPAList Is Nothing Then frmDigital_MPPAList.Dispose()
            frmDigital_MPPAList = Nothing
        End Try
    End Sub
End Class