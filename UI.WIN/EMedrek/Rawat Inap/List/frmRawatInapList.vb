Imports DataAccess
Imports UI.WIN.MAIN.My.Resources
Imports System.Linq
Imports DevExpress.XtraSplashScreen
Imports System.IO
Imports iTextSharp.text
Imports iTextSharp.text.pdf
Imports System.Data.SqlClient
Imports System.Net
Imports Newtonsoft.Json.Linq
Imports System.Text.RegularExpressions
Imports System.Globalization

Public Class frmRawatInapList
    Private oCPPT As New Transaksi.clsCPPT
    Private oCPPTTemplate As New Inventory.clsCPPTTemplate
    Private oRINGKASANKELUAR As New Transaksi.clsRingkasanKeluarRawatInap
    Private oRINGKASANKELUARTemplate As New Inventory.clsTemplateRingkasanKeluar
    Private oS_DIGITAL_HANDOVER As New Transaksi.clsDigital_HandOver
    Private oTransferInternal As New Transaksi.clsTransferInternal
    Private oTindakanEvaluasiKeperawatan As New Digital.clsDiagnosaPerawat
    Private oS_DIGITAL_FARMASI_REKONSILIASIOBAT_H As New Transaksi.clsRekonsiliasiObat
    Private oLaporanOperasi As New EMedrek.clsS_DIGITAL_OK_LAPORANOPERASI
    Private oBrigging As New Brigging.clsSetKoneksi
    Private oLembarObservasi As New Inventory.clsLembarObservasi
    Private oCatatatanKeperawatan As New Inventory.clsCatatanPerawat
    Private oIGD_GerdQ As New Inventory.clsDigital_IGD_01_GERD
    Private oCatatanSkriningdanEdukasiGizi As New Digital.clsDigital_RI_20
    Private oAsesmenLanjutGizi As New Digital.clsS_DIGITAL_ASESMENGIZILANJUT
    Private oMonitoringEvaluasiGizi As New Digital.clsMonitoringEvaluasiGizi
    Private oReqRecipeRawatInap As New Transaksi.clsReqRecipeRawatInap
    Private oDigital_DischargePlanning As New Transaksi.clsDigital_DischargePlanning
    Private oDigital_LaporanTindakan As New EMedrek.clsS_DIGITAL_OK_LAPORANTINDAKAN
    Private oOrderPenunjang As New Order.clsOrderPenunjang
    Private isLoad As Boolean = False
    Private isLoadResume As Boolean = False
    Private isLoadAsesmen As Boolean = False
    Private isLoadLaporanOperasi As Boolean = False
    Private sISDOKTER As Boolean = False
    Private sKDDOCTOR_INPUT As String = String.Empty
    Private sKDDOCTOR_DPJPUTAMA As String = String.Empty
    Private sTanggalLahir As DateTime = Now
    Private arrfname As New List(Of String)()
    Private sCOPYCPPTDOKTERTERAKHIR As String = String.Empty
    Private sCOPYKDCPPTPERAWAT As String = String.Empty
    Private sDOKTERUTAMA As String = String.Empty
    Private sDOKTERKEDUA As String = String.Empty
    Private sDOKTERKETIGA As String = String.Empty
    Private sDOKTERKEEMPAT As String = String.Empty
    Private sDOKTERKELIMA As String = String.Empty
    Private sNAMADOKTERINPUT As String = String.Empty
    Private sKDREGRAWATJALAN As String = String.Empty
    Private sKDUSER_PERAWAT As String = String.Empty
    Private sUMURPASIEN As String = String.Empty
    Private sKODELAPORANOPERASI As String = String.Empty
    Private oAdmisi As New Admission.clsPendafataranPenunjangHariIni

    'TARIF
    Private sBedah As Decimal = 0
    Private sNonBedah As Decimal = 0
    Private sKonsultasig As Decimal = 0
    Private sTenagaAhli As Decimal = 0
    Private sKeperawatan As Decimal = 0
    Private sPenunjang As Decimal = 0
    Private sRadiologi As Decimal = 0
    Private sLab As Decimal = 0
    Private sPelayananDarah As Decimal = 0
    Private sRehabilitasi As Decimal = 0
    Private sKamar As Decimal = 0
    Private sRawatIntensif As Decimal = 0
    Private sObat As Decimal = 0
    Private sAlkes As Decimal = 0
    Private sObatKronis As Decimal = 0
    Private sObatKemoterpi As Decimal = 0
    Private sBMHP As Decimal = 0
    Private sSewaAlatMedis As Decimal = 0
    Private sCasemix As Boolean = False
    Private sDateWaktuCPPT As DateTime = Now
    Private sReplacePenunjangCPPT As String = String.Empty
    Private sURL_Claim As String = String.Empty
    Private sGenerat_Claim As String = String.Empty
    Private sRESEPPULANGDICPPT As Boolean = False

#Region "Function"
    Public Sub fn_LoadMe(ByVal Casemix As Boolean, ByVal ISDOKTER As Boolean, ByVal KDKELASRAWAT As String, ByVal KARTUBPJS As String, ByVal NOMORSEP As String, ByVal KDREG As String, ByVal KDDOCTOR As String, ByVal RUANGAN As String, ByVal PENJAMIN As String, ByVal KDCUSTOMER As String, ByVal NAMAPASIEN As String, ByVal JENISKELAMIN As String, ByVal TANGGALLAHIR As DateTime, ByVal COPYKDCCPTPERAWAT As String, ByVal TANGGALMASUK As DateTime, ByVal DOKTERUTAMA As String, ByVal DOKTERKEDUA As String, ByVal DOKTERKETIGA As String, ByVal DOKTERKEEMPAT As String, ByVal DOKTERKELIMA As String, ByVal NAMADOKTERINPUT As String, ByVal KDREGRAWATJALAN As String, ByVal KDUSER_PERAWAT As String, ByVal KDDOCTOR_DPJPUTAMA As String, ByVal TANGGALPULANG As DateTime)
        sLoadAsesmenAwal = False
        sLoadLaporanOperasi = False
        sCasemix = Casemix
        sISDOKTER = ISDOKTER
        sKDDOCTOR_INPUT = KDDOCTOR
        sKDDOCTOR_DPJPUTAMA = KDDOCTOR_DPJPUTAMA
        sTanggalLahir = TANGGALLAHIR

        Dim dsCPPTAkhir = oCPPT.GetDataCPPT_AkhirDokter(KDREG)
        If dsCPPTAkhir IsNot Nothing Then
            sCOPYCPPTDOKTERTERAKHIR = dsCPPTAkhir.KDCPPT
        End If

        sReplacePenunjangCPPT = ""

        sCOPYKDCPPTPERAWAT = COPYKDCCPTPERAWAT
        lblKELAS.Text = KDKELASRAWAT
        lblRegister.Text = KDREG
        lblKartuBPJS.Text = KARTUBPJS
        lblRuangan.Text = RUANGAN
        lblPenjamin.Text = PENJAMIN
        lblNoRM.Text = KDCUSTOMER
        lblNamaPasien.Text = NAMAPASIEN
        lblJenisKelamin.Text = JENISKELAMIN
        lblTanggalLahir.Text = TANGGALLAHIR.ToString("dd-MM-yyyy", CultureInfo.InvariantCulture)

        lblNOMORSEP.Text = NOMORSEP
        tabCPPT.Text = "CPPT " & Now.ToString("dd-MM-yyyy HH:mm", CultureInfo.InvariantCulture)
        deTANGGALMASUK.DateTime = TANGGALMASUK
        deTANGGALPULANG.DateTime = TANGGALPULANG
        sDOKTERUTAMA = DOKTERUTAMA
        sDOKTERKEDUA = DOKTERKEDUA
        sDOKTERKETIGA = DOKTERKETIGA
        sDOKTERKEEMPAT = DOKTERKEEMPAT
        sDOKTERKELIMA = DOKTERKELIMA
        sNAMADOKTERINPUT = NAMADOKTERINPUT
        sKDREGRAWATJALAN = KDREGRAWATJALAN
        sKDUSER_PERAWAT = KDUSER_PERAWAT
        'DateDiff(DateInterval.Year, sTanggalLahir, Now) & " Tahun"
        sUMURPASIEN = HitungUmur(sTanggalLahir, deTANGGALMASUK.DateTime)
        sUmurPasienDiCPPT = sUMURPASIEN
        deWAKTUPEMBERIANRESEP.DateTime = Now
        lblUsia.Text = sUMURPASIEN

        If sISDOKTER = True Then
            'btnSBAR.Visible = False
            'tabHandOver.PageVisible = False
            tabTransferInternal.PageVisible = False
            tabAsesmenAwalPerawat.PageVisible = False
            tabTindakanEvaluasiKeperawatan.PageVisible = False
            tabLembarEWS.PageVisible = False
            tabInstalasiFarmasi.PageVisible = False
            tabPersetuajuandanPenolakan.PageVisible = False
            tabRencanaOperasi.PageVisible = False
            tabLembarObservasi.PageVisible = False
            tabCatatatnKeperawatan.PageVisible = False
            'tabGerdQ.PageVisible = False
            'tabGizi.PageVisible = False

            grdDetailResep.Enabled = True
            grdDetailResep.ContextMenuStrip = mnuStrip
        Else
            grdDetailResep.Enabled = False
            grdDetailResep.ContextMenuStrip = Nothing
        End If

        LoadPenunjang(lblRegister.Text)

        Dim oBPJSKoneksi As New Setting.clsBPJSKoneksi
        Dim ds = oBPJSKoneksi.GetData("ECLAIM")
        If ds IsNot Nothing Then
            sURL_Claim = ds.ALAMATWEB
            sGenerat_Claim = ds.REMARKS
        End If
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
    Private Sub Me_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        sKONSULTASI = ""
        fn_LoadSecurity()
        isLoad = True
        fn_LoadRincian(False)

        'btnEditListTindakan.Visible = False
        'lTindakanOpearsiFormulir.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        lTindakanOPerasi_Add.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        lFormLaporanOperasi.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never


        Dim dsDisCharegPlanning = oDigital_DischargePlanning.GetData(lblRegister.Text)
        If dsDisCharegPlanning Is Nothing Then
            XtraTabControl1.SelectedTabPageIndex = 1
        End If

    End Sub
    Private Overloads Sub Dispose()
        MyBase.Dispose()
        Me.Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
    Private Sub Me_FormClosed(sender As System.Object, e As System.Windows.Forms.FormClosedEventArgs) Handles MyBase.FormClosed
        'skodeorderlab = String.Empty
        'skodeorderRad = String.Empty

        pdfViewerCPPTRANAP.CloseDocument()
        pdfViewerCPPTRAJAL.CloseDocument()
        PdfViewerResume.CloseDocument()
        PdfViewerLaboratorium.CloseDocument()
        PdfViewerTransferInternal.CloseDocument()
        PdfViewerAsesmenMedisAwalIGD.CloseDocument()
        PdfViewerAsesmenAwalPerawatIGD.CloseDocument()
        PdfViewerRadiologi.CloseDocument()
        PdfViewerLembarObservasi.CloseDocument()
        PdfViewerUpload.CloseDocument()
        PdfViewerGerdQ.CloseDocument()
        PdfViewerCatatanKeperawatan.CloseDocument()
        PdfViewerPonek.CloseDocument()
        PdfViewerLaporanOperasi.CloseDocument()
        PdfViewerKonsul.CloseDocument()
        PdfViewerPengantar.CloseDocument()
        PdfViewerFormulirMPP.CloseDocument()
        'PdfViewerSBAR.CloseDocument()
        'PictureEdit1.Image = Nothing
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
                    fn_LoadOperator()
                    fn_LoadPERAWAT()

                    fn_LoadDataAsesmenAwalMedisCek(lblRegister.Text)

                    tabbedControlGroup1.SelectedTabPageIndex = 0
                    tabbedControlGroup1_SelectedPageChanged()
                    XtraTabControl1_SelectedPageChanged()
                End If
            Catch oErr As Exception
                MsgBox(Statement.SecurityNotInstalled, MsgBoxStyle.Exclamation, Me.Text)
            End Try
        Catch oErr As Exception
            MsgBox(Statement.SecurityNotInstalled, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub DeleteDirectory(path As String)
        Try
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
        Catch ex As Exception

        End Try
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
    Private Sub fn_LoadPdfViewerCPPTRANANP()
        Try
            pdfViewerCPPTRANAP.CloseDocument()

            SplashScreenManager.ShowForm(Me, GetType(frmLoadingPanel), True, True, False)

            SplashScreenManager.Default.SetWaitFormCaption("Processing data CPPT Rawat Inap.....")

            If lblNoRM.Text = String.Empty Then Exit Sub

            Dim FolderSimpan = "C:/CPPTRANAP/"

            If Not Directory.Exists(FolderSimpan) Then
                Directory.CreateDirectory(FolderSimpan)
            Else
                DeleteDirectory(FolderSimpan)
                Directory.CreateDirectory(FolderSimpan)
            End If

            Dim dataList As New List(Of Byte())
            Dim dsLoad() As Byte
            Dim ds = oCPPT.GetDataByRMRANAP(lblNoRM.Text)

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
                    pdfViewerCPPTRANAP.LoadDocument(stream)
                End If
            End If

            SplashScreenManager.CloseForm(False)
        Catch oErr As Exception
            SplashScreenManager.CloseForm(False)
            MsgBox("Cetak CPPT Ranap" & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadPdfViewerCPPTRAJAL()
        Try
            pdfViewerCPPTRAJAL.CloseDocument()

            SplashScreenManager.ShowForm(Me, GetType(frmLoadingPanel), True, True, False)

            SplashScreenManager.Default.SetWaitFormCaption("Processing data CPPT Rawat Inap.....")

            If lblNoRM.Text = String.Empty Then Exit Sub

            Dim FolderSimpan = "C:/CPPTRAJAL/"

            If Not Directory.Exists(FolderSimpan) Then
                Directory.CreateDirectory(FolderSimpan)
            Else
                DeleteDirectory(FolderSimpan)
                Directory.CreateDirectory(FolderSimpan)
            End If

            Dim dataList As New List(Of Byte())
            Dim dsLoad() As Byte
            Dim ds = oCPPT.GetDataByRMRAJAL(lblNoRM.Text)

            If sCPPT_QRRJ = False Then
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
                        pdfViewerCPPTRAJAL.LoadDocument(stream)
                    End If
                End If
            Else
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
                        pdfViewerCPPTRAJAL.LoadDocument(stream)
                    End If
                End If
            End If

            SplashScreenManager.CloseForm(False)
        Catch oErr As Exception
            SplashScreenManager.CloseForm(False)
            MsgBox("Cetak CPPT Rajal" & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadResumeRawatJlalanRawatInap(ByVal RekamMedis As String)
        Try
            grvResume.OptionsSelection.MultiSelect = True
            grvResume.SelectAll()
            grvResume.DeleteSelectedRows()
            grvResume.OptionsSelection.MultiSelect = False

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
            SQL &= "KATEGORI = 'RESUME RAWAT JALAN' "
            SQL &= ",TANGGAL = A.DATE "
            SQL &= ",KODE = A.KDKODING "
            SQL &= ",[USER] = A.NOIDUSER "
            SQL &= ",ASAL = A.TUJUAN "
            SQL &= ",KETERANGAN = A.DESCRIPTION  "
            SQL &= "FROM "
            SQL &= "S_KODING_H A "
            SQL &= "WHERE "
            SQL &= "A.KDCUSTOMER = '" & RekamMedis & "' "

            SQL &= "UNION "

            SQL &= "SELECT "
            SQL &= "KATEGORI = 'RESUME RAWAT INAP' "
            SQL &= ",TANGGAL = A.TANGGALPULANG "
            SQL &= ",KODE = A.KDREG "
            SQL &= ",[USER] = A.KDUSER "
            SQL &= ",ASAL = A.RUANGRAWAT "
            SQL &= ",KETERANGAN = A.ANAMNESA  "
            SQL &= "FROM "
            SQL &= "S_RINGKASANKELUARRAWATINAP A "
            SQL &= "WHERE "
            SQL &= "A.KDCUSTOMER = '" & RekamMedis & "' "

            SQL &= ") Z "
            SQL &= "ORDER BY "
            SQL &= "Z.TANGGAL DESC "

            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "S_KODING_H_RJRI")

            grdResume.DataSource = ds.Tables("S_KODING_H_RJRI")
            grdResume.ForceInitialize()

            If oConn.State = ConnectionState.Open Then
                oConn.Close()
            End If

            grvResume.Columns("KODE").Visible = False
            grvResume.Columns("KATEGORI").Visible = False

            ' grvResume.BestFitColumns()

            For iLoop As Integer = 0 To grvResume.Columns.Count - 1
                If grvResume.Columns(iLoop).ColumnType.Name = "Decimal" Then
                    grvResume.Columns(iLoop).DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                    grvResume.Columns(iLoop).DisplayFormat.FormatString = "{0:n2}"
                    grvResume.Columns(iLoop).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
                ElseIf grvResume.Columns(iLoop).ColumnType.Name = "DateTime" Then
                    grvResume.Columns(iLoop).DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
                    grvResume.Columns(iLoop).DisplayFormat.FormatString = "{0:dd/MM/yyyy}"
                End If
            Next

        Catch oErr As Exception
            MsgBox("Load S_KODING_H_RJRI : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_PrintStrukResumeRawatJalanRawatInap(ByVal KODE As String, ByVal KATEGORI As String)
        Try
            PdfViewerResume.CloseDocument()

            SplashScreenManager.ShowForm(Me, GetType(frmLoadingPanel), True, True, False)

            SplashScreenManager.Default.SetWaitFormCaption("Processing data Resume.....")

            Dim FolderSimpan = "C:/RESUMESIMRS/"

            If Not Directory.Exists(FolderSimpan) Then
                Directory.CreateDirectory(FolderSimpan)
            Else
                DeleteDirectory(FolderSimpan)
                Directory.CreateDirectory(FolderSimpan)
            End If

            Dim oKoding As New Transaksi.clsReq_Recipe
            Dim oRingkasanKeluarRI As New Transaksi.clsRingkasanKeluarRawatInap
            'Dim dataList As New List(Of Byte())
            'Dim dsLoad() As Byte

            If KATEGORI = "RESUME RAWAT JALAN" Then
                Dim dsKoding = oKoding.GetDataKoding(KODE)
                If dsKoding IsNot Nothing Then
                    Dim rpt As New xtraKoding

                    rpt.bindingSource.DataSource = dsKoding

                    DownloadIamge1 = AlamatDownloadIamge1 & lblNoRM.Text & "/" & lblNoRM.Text & ".png"

                    sUserIDTandaTangan = dsKoding.KDDOCTOR

                    rpt.bindingSource.DataSource = dsKoding
                    rpt.ExportToPdf(FolderSimpan & dsKoding.KDKODING & ".pdf")
                    PdfViewerResume.LoadDocument(FolderSimpan & dsKoding.KDKODING & ".pdf")
                End If
            Else
                Dim dsRingkasan = oRingkasanKeluarRI.GetData(KODE)
                If dsRingkasan IsNot Nothing Then
                    Dim rpt As New xtraRingkasanKeluarRawatInap

                    rpt.bindingSource.DataSource = dsRingkasan

                    DownloadIamge1 = AlamatDownloadIamge1 & lblNoRM.Text & "/" & lblNoRM.Text & ".png"
                    rpt.bindingSource.DataSource = dsRingkasan
                    rpt.ExportToPdf(FolderSimpan & dsRingkasan.KDREG & ".pdf")
                    PdfViewerResume.LoadDocument(FolderSimpan & dsRingkasan.KDREG & ".pdf")
                End If
            End If

            'For Each xloop In oKoding.GetDataKodingByRM(lblNoRM.Text)
            '    Dim dsKoding = oKoding.GetDataKoding(xloop.KDKODING)
            '    If dsKoding IsNot Nothing Then
            '        Dim rpt As New xtraKoding

            '        rpt.bindingSource.DataSource = dsKoding

            '        DownloadIamge1 = AlamatDownloadIamge1 & lblNoRM.Text & "/" & lblNoRM.Text & ".png"

            '        sUserIDTandaTangan = dsKoding.KDDOCTOR

            '        rpt.bindingSource.DataSource = dsKoding
            '        rpt.ExportToPdf(FolderSimpan & dsKoding.KDKODING & ".pdf")
            '        dataList.Add(File.ReadAllBytes(FolderSimpan & dsKoding.KDKODING & ".pdf"))
            '    End If
            'Next

            'For Each xloop In oRingkasanKeluarRI.GetDataKodingByRM(lblNoRM.Text)
            '    Dim dsRingkasan = oRingkasanKeluarRI.GetData(xloop.KDREG)
            '    If dsRingkasan IsNot Nothing Then
            '        Dim rpt As New xtraRingkasanKeluarRawatInap

            '        rpt.bindingSource.DataSource = dsRingkasan

            '        DownloadIamge1 = AlamatDownloadIamge1 & lblNoRM.Text & "/" & lblNoRM.Text & ".png"

            '        'sUserIDTandaTangan = dsKoding.KDDOCTOR

            '        rpt.bindingSource.DataSource = dsRingkasan
            '        rpt.ExportToPdf(FolderSimpan & dsRingkasan.KDREG & ".pdf")
            '        dataList.Add(File.ReadAllBytes(FolderSimpan & dsRingkasan.KDREG & ".pdf"))
            '    End If
            'Next

            'If dataList.Count > 0 Then
            '    dsLoad = MergeFilesByte(dataList)
            '    Dim stream As New MemoryStream(dsLoad)
            '    PdfViewerResume.LoadDocument(stream)
            'End If

            SplashScreenManager.CloseForm(False)

        Catch oErr As Exception
            SplashScreenManager.CloseForm(False)

            MsgBox("Cetak Resume : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadPdfViewerLembarObservasi()
        Try
            PdfViewerLembarObservasi.CloseDocument()

            SplashScreenManager.ShowForm(Me, GetType(frmLoadingPanel), True, True, False)

            SplashScreenManager.Default.SetWaitFormCaption("Processing data Lembar Observasi.....")

            If lblNoRM.Text = String.Empty Then Exit Sub

            Dim FolderSimpan = "C:/LEMBAROBSERVASI/"

            If Not Directory.Exists(FolderSimpan) Then
                Directory.CreateDirectory(FolderSimpan)
            Else
                DeleteDirectory(FolderSimpan)
                Directory.CreateDirectory(FolderSimpan)
            End If

            Dim dataList As New List(Of Byte())
            Dim dsLoad() As Byte

            'For Each xloop In oLembarObservasi.GetDataByRM(lblNoRM.Text)
            '    Dim ds = oLembarObservasi.GetData(xloop.KDLEMBAROBSERVASI)
            '    If ds IsNot Nothing Then

            '    End If
            'Next

            Dim ds = oLembarObservasi.GetDataByRM(lblNoRM.Text)

            If ds.Count > 0 Then
                Dim rpt As New xtraLembarObservasi

                rpt.ShowPrintMarginsWarning = False
                rpt.Watermark.Text = sWATERMARK

                rpt.bindingSource.DataSource = ds
                rpt.ExportToPdf(FolderSimpan & "HASIL" & ".pdf")
                dataList.Add(File.ReadAllBytes(FolderSimpan & "HASIL" & ".pdf"))

                If dataList.Count > 0 Then
                    dsLoad = MergeFilesByte(dataList)
                    Dim stream As New MemoryStream(dsLoad)
                    PdfViewerLembarObservasi.LoadDocument(stream)
                End If
            End If

            SplashScreenManager.CloseForm(False)
        Catch oErr As Exception
            SplashScreenManager.CloseForm(False)
            MsgBox("Cetak Lembar Observasi" & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadPdfViewerCatatanKeperawatan()
        Try
            PdfViewerCatatanKeperawatan.CloseDocument()

            SplashScreenManager.ShowForm(Me, GetType(frmLoadingPanel), True, True, False)

            SplashScreenManager.Default.SetWaitFormCaption("Processing data Catatan Keperawatan.....")

            If lblNoRM.Text = String.Empty Then Exit Sub

            Dim FolderSimpan = "C:/CATATANKEPERAWATAN/"

            If Not Directory.Exists(FolderSimpan) Then
                Directory.CreateDirectory(FolderSimpan)
            Else
                DeleteDirectory(FolderSimpan)
                Directory.CreateDirectory(FolderSimpan)
            End If

            Dim dataList As New List(Of Byte())
            Dim dsLoad() As Byte
            Dim ds = oCatatatanKeperawatan.GetDataByRM(lblNoRM.Text)

            If ds.Count > 0 Then
                Dim rpt As New xtraCatatanKeperawatan

                rpt.ShowPrintMarginsWarning = False
                rpt.Watermark.Text = sWATERMARK

                rpt.bindingSource.DataSource = ds
                rpt.ExportToPdf(FolderSimpan & "HASIL" & ".pdf")
                dataList.Add(File.ReadAllBytes(FolderSimpan & "HASIL" & ".pdf"))

                If dataList.Count > 0 Then
                    dsLoad = MergeFilesByte(dataList)
                    Dim stream As New MemoryStream(dsLoad)
                    PdfViewerCatatanKeperawatan.LoadDocument(stream)
                End If
            End If

            SplashScreenManager.CloseForm(False)
        Catch oErr As Exception
            SplashScreenManager.CloseForm(False)
            MsgBox("Cetak Catatan Keperawatan" & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_DokterBersama()
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
            SQL &= "DARIDOKTER = C.NAME_DISPLAY "
            SQL &= ",KEPADADOKTER = B.NAME_DISPLAY "
            SQL &= ",KETERANGAN = 'DPJP KE ' + CONVERT(NVARCHAR(2), A.SEQ)  "
            SQL &= ",A.ISIKONSUL "
            SQL &= ",A.JAWABKONSUL "
            SQL &= ",A.SEQ "
            SQL &= ",A.KDDOCTOR_DARI "
            SQL &= ",A.KDDOCTOR_KEPADA "
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
            SQL &= "WHERE "
            SQL &= "A.KDREG = '" & lblRegister.Text & "' "

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

            grvDokterBersama.Columns("KDDOCTOR_DARI").VisibleIndex = -1
            grvDokterBersama.Columns("KDDOCTOR_KEPADA").VisibleIndex = -1
            grvDokterBersama.Columns("SEQ").VisibleIndex = -1

            grvDokterBersama.BestFitColumns()

        Catch oErr As Exception
            MsgBox("Load Tracking Dokter : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadDataLaboratorium(ByVal RekamMedis As String)
        Try
            grvLaboratorium.OptionsSelection.MultiSelect = True
            grvLaboratorium.SelectAll()
            grvLaboratorium.DeleteSelectedRows()
            grvLaboratorium.OptionsSelection.MultiSelect = False

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
            SQL &= "KODE = A.KDTRANSAKSILAB "
            SQL &= ",TANGGAL = A.DATE "
            SQL &= ",TINDAKAN = D.NAMA_TARIF "
            'SQL &= ",TINDAKAN = STRING_AGG(D.NAMA_TARIF,', ') "
            SQL &= ",B.KDCUSTOMER "
            SQL &= ",NAME_DISPLAY = E.NAME_DISPLAY + ' ' + IIf(E.FRONT_TITLE IS NULL, '', E.FRONT_TITLE + ' ') + E.BACK_TITLE "
            SQL &= "FROM "
            SQL &= "S_TRANSAKSILAB_H A "
            SQL &= "INNER JOIN S_PENDAFTARAN_H B "
            SQL &= "ON A.KDREG = B.KDREG "
            SQL &= "INNER JOIN S_TRANSAKSILAB_D C "
            SQL &= "ON A.KDTRANSAKSILAB = C.KDTRANSAKSILAB "
            SQL &= "INNER JOIN M_TARIF_NEW D "
            SQL &= "ON C.KDTARIF = D.KDTARIF "
            SQL &= "INNER JOIN M_CUSTOMER E "
            SQL &= "ON B.KDCUSTOMER = E.KDCUSTOMER "
            SQL &= "WHERE B.KDCUSTOMER = '" & lblNoRM.Text & "' "
            'SQL &= "GROUP BY A.DATE, A.KDTRANSAKSILAB  "
            SQL &= "ORDER BY A.DATE DESC "

            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "S_TRANSAKSILAB_H_DATA")

            grdLaboratoium.DataSource = ds.Tables("S_TRANSAKSILAB_H_DATA")
            grdLaboratoium.ForceInitialize()

            If oConn.State = ConnectionState.Open Then
                oConn.Close()
            End If

            grvLaboratorium.Columns("KODE").Visible = False
            grvLaboratorium.Columns("KDCUSTOMER").Visible = False
            grvLaboratorium.Columns("NAME_DISPLAY").Visible = False

            For iLoop As Integer = 0 To grvLaboratorium.Columns.Count - 1
                If grvLaboratorium.Columns(iLoop).ColumnType.Name = "Decimal" Then
                    grvLaboratorium.Columns(iLoop).DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                    grvLaboratorium.Columns(iLoop).DisplayFormat.FormatString = "{0:n2}"
                    grvLaboratorium.Columns(iLoop).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
                ElseIf grvLaboratorium.Columns(iLoop).ColumnType.Name = "DateTime" Then
                    grvLaboratorium.Columns(iLoop).DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
                    grvLaboratorium.Columns(iLoop).DisplayFormat.FormatString = "{0:dd/MM/yyyy HH:mm}"
                End If
            Next

        Catch oErr As Exception
            MsgBox("Load S_TRANSAKSILAB_H : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_SearchFolderPdf(ByVal kode As String, ByVal kdcustomer As String, ByVal nama As String)
        Try
            Dim sPDF As String = "\\192.168.2.79\Users\SIMRS\δupload berkas rmδ\" & kode & "_" & CInt(kdcustomer) & "_" & nama.Trim.ToString.Replace("'", "") & ".pdf"

            If FileIO.FileSystem.FileExists(sPDF) Then
                PdfViewerLaboratorium.LoadDocument(sPDF)
            Else
                Dim sPDF2 As String = "\\192.168.2.79\Users\SIMRS\δupload berkas rmδ\" & kode & "_" & CInt(kdcustomer) & ".pdf"
                If FileIO.FileSystem.FileExists(sPDF) Then
                    PdfViewerLaboratorium.LoadDocument(sPDF2)
                Else
                    PdfViewerLaboratorium.CloseDocument()
                End If
            End If

            'Dim FolderSimpan = "C:/BROWSERPDF/"

            'If Not Directory.Exists(FolderSimpan) Then
            '    Directory.CreateDirectory(FolderSimpan)
            'Else
            '    DeleteDirectory(FolderSimpan)
            '    Directory.CreateDirectory(FolderSimpan)
            'End If

            'Dim Folder As New DirectoryInfo(sAlamatSimpanPDFLaboratorium)
            'Dim fi As List(Of FileInfo) = New List(Of FileInfo)

            'For Each File In Folder.GetFiles()
            '    If (File IsNot Nothing) Then
            '        If File.ToString.Contains(kode) Then
            '            fi.Add(File)
            '            arrfname.Add(File.FullName)
            '        End If

            '    End If
            'Next

            'If fi.Count > 0 Then
            '    Try
            '        Dim sinputFiles() As String = {}
            '        For Each ifname In arrfname.OrderByDescending(Function(x) x.Distinct)
            '            sinputFiles = AppendArray(sinputFiles, ifname)
            '        Next

            '        MergePdfFiles(sinputFiles, FolderSimpan & lblRegister.Text & ".pdf")

            '        PdfViewerLaboratorium.LoadDocument(FolderSimpan & lblRegister.Text & ".pdf")

            '        arrfname.Clear()
            '    Catch ex As Exception

            '    End Try
            'End If
        Catch oErr As Exception
            MsgBox("Search Folder : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Public Function GetUmurPasien(ByVal dateNow As Date, ByVal tgllahir As Date) As String
        Try
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

            GetUmurPasien = yearWord & " " & monthWord & " " & dayWord
        Catch oErr As Exception
            GetUmurPasien = ""
            MsgBox(Statement.ErrorStatement, MsgBoxStyle.Exclamation)
        End Try
    End Function
    Private Sub fn_LoadDataRadiologi()
        Try
            Try
                Dim FolderSimpan = "C:/HASILRADIOLGI/"

                If Not Directory.Exists(FolderSimpan) Then
                    Directory.CreateDirectory(FolderSimpan)
                Else
                    DeleteDirectory(FolderSimpan)
                    Directory.CreateDirectory(FolderSimpan)
                End If
            Catch ex As Exception

            End Try

            'PictureEdit1.Image = Nothing

            'grvFoto.OptionsSelection.MultiSelect = True
            'grvFoto.SelectAll()
            'grvFoto.DeleteSelectedRows()
            'grvFoto.OptionsSelection.MultiSelect = False

            grvRadiologi.OptionsSelection.MultiSelect = True
            grvRadiologi.SelectAll()
            grvRadiologi.DeleteSelectedRows()
            grvRadiologi.OptionsSelection.MultiSelect = False

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
            SQL &= "Tanggal = C.DATE "
            SQL &= ",Catatan = B.NAMA_TARIF "
            SQL &= ",NoRadiologi = C.NORADIOLOGI "
            SQL &= ",URL = 'Double Klik' "
            SQL &= ",A.KDTRANSAKSI "
            SQL &= ",DOKTER = (CASE E.FRONT_TITLE WHEN '' THEN '' ELSE E.FRONT_TITLE END) + ' ' + E.NAME_DISPLAY + ' ' + (CASE E.BACK_TITLE WHEN '' THEN '' ELSE E.BACK_TITLE END) "
            SQL &= "FROM "
            SQL &= "S_TRANSAKSI_RJ A "
            SQL &= "INNER JOIN M_TARIF_NEW AS B "
            SQL &= "ON A.KDTARIF = B.KDTARIF "
            SQL &= "INNER JOIN S_TRANSAKSI_H AS C "
            SQL &= "ON A.KDTRANSAKSI = C.KDTRANSAKSI "
            SQL &= "INNER JOIN S_PENDAFTARAN_H D "
            SQL &= "ON C.KDREG = D.KDREG "
            SQL &= "INNER JOIN M_DOCTOR E "
            SQL &= "ON C.KDDOCTOR = E.KDDOCTOR "
            SQL &= "WHERE D.KDCUSTOMER = '" & lblNoRM.Text & "' "

            SQL &= "UNION "

            SQL &= "SELECT "
            SQL &= "Tanggal = C.DATE "
            SQL &= ",Catatan = B.NAMA_TARIF "
            SQL &= ",NoRadiologi = E.NORADIOLOGI "
            SQL &= ",URL = 'Double Klik' "
            SQL &= ",KDTRANSAKSI = A.KDTRANSAKSIRJ "
            SQL &= ",DOKTER = (CASE F.FRONT_TITLE WHEN '' THEN '' ELSE F.FRONT_TITLE END) + ' ' + F.NAME_DISPLAY + ' ' + (CASE F.BACK_TITLE WHEN '' THEN '' ELSE F.BACK_TITLE END) "
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
            SQL &= "INNER JOIN M_DOCTOR F "
            SQL &= "ON C.KDDOCTOR = F.KDDOCTOR "
            SQL &= "WHERE D.KDCUSTOMER = '" & lblNoRM.Text & "' "
            SQL &= "AND B.KDKODETARIF ='169' "

            SQL &= "UNION "

            SQL &= "SELECT "
            SQL &= "Tanggal = C.DATE "
            SQL &= ",Catatan = B.NAMA_TARIF "
            SQL &= ",NoRadiologi = E.NORADIOLOGI "
            SQL &= ",URL = 'Double Klik' "
            SQL &= ",KDTRANSAKSI = A.KDTRANSAKSIRI "
            SQL &= ",DOKTER = (CASE F.FRONT_TITLE WHEN '' THEN '' ELSE F.FRONT_TITLE END) + ' ' + F.NAME_DISPLAY + ' ' + (CASE F.BACK_TITLE WHEN '' THEN '' ELSE F.BACK_TITLE END) "
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
            SQL &= "INNER JOIN M_DOCTOR F "
            SQL &= "ON C.KDDOCTOR = F.KDDOCTOR "
            SQL &= "WHERE D.KDCUSTOMER = '" & lblNoRM.Text & "' "

            SQL &= "UNION "

            SQL &= "SELECT "
            SQL &= "Tanggal = A.DATE "
            SQL &= ",Catatan = C.MEMO "
            SQL &= ",NoRadiologi = '' "
            SQL &= ",URL = 'Double Klik' "
            SQL &= ",KDTRANSAKSI = A.KDPDFTRANSAKSI "
            SQL &= ",DOKTER = (CASE F.FRONT_TITLE WHEN '' THEN '' ELSE F.FRONT_TITLE END) + ' ' + F.NAME_DISPLAY + ' ' + (CASE F.BACK_TITLE WHEN '' THEN '' ELSE F.BACK_TITLE END) "
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
            SQL &= "WHERE B.KDCUSTOMER = '" & lblNoRM.Text & "' "
            SQL &= "AND D.ISDEFAULT = 1 "
            SQL &= "AND D.DESCRIPTION <> 'ADMINISTRASI' "

            SQL &= ") X "
            SQL &= "ORDER BY X.Tanggal DESC "

            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "S_TRANSAKSI_RJ")

            grdRadiologi.DataSource = ds.Tables("S_TRANSAKSI_RJ")
            grdRadiologi.ForceInitialize()

            If oConn.State = ConnectionState.Open Then
                oConn.Close()
            End If

            For iLoop As Integer = 0 To grvRadiologi.Columns.Count - 1
                If grvRadiologi.Columns(iLoop).ColumnType.Name = "Decimal" Then
                    grvRadiologi.Columns(iLoop).DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                    grvRadiologi.Columns(iLoop).DisplayFormat.FormatString = "{0:n2}"
                    grvRadiologi.Columns(iLoop).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
                ElseIf grvRadiologi.Columns(iLoop).ColumnType.Name = "DateTime" Then
                    grvRadiologi.Columns(iLoop).DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
                    grvRadiologi.Columns(iLoop).DisplayFormat.FormatString = "{0:dd/MM/yyyy HH:mm}"
                End If
            Next

            grvRadiologi.BestFitColumns()

        Catch oErr As Exception
            MsgBox("Load Data Penunjang Radiologi : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_TransferInternal()
        Try
            PdfViewerTransferInternal.CloseDocument()

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

            For Each xloop In oDigital.GetDataByRM(lblNoRM.Text)
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
    End Sub
    Private Sub fn_PrintStrukAsesemenMedis(ByVal KATEGORI As String, ByVal Kode As String)
        PdfViewerAsesmenMedisAwalIGD.CloseDocument()

        If KATEGORI = "ASESMEN AWAL MEDIS IGD" Then
            Try
                SplashScreenManager.ShowForm(Me, GetType(frmLoadingPanel), True, True, False)

                SplashScreenManager.Default.SetWaitFormCaption("Processing data " & KATEGORI & ".....")

                Dim oIGD As New Transaksi.clsDigital_IGD_01
                Dim FolderSimpan = "C:/ASESMENMEDISAWAL/"

                If Not Directory.Exists(FolderSimpan) Then
                    Directory.CreateDirectory(FolderSimpan)
                Else
                    DeleteDirectory(FolderSimpan)
                    Directory.CreateDirectory(FolderSimpan)
                End If

                Dim ListdataKDCPPT As New List(Of String)
                Dim dataList As New List(Of Byte())
                Dim dsLoad() As Byte

                Dim ds = oIGD.GetData(Kode)
                If ds IsNot Nothing Then
                    sASESEMEN_IGD = ds.SURVEY_PERUT_1
                    sUSIADIASESMENIGD = sTanggalLahir.ToString("dd-MM-yyyy", CultureInfo.InvariantCulture) & " (" & HitungUmur(sTanggalLahir, deTANGGALMASUK.DateTime) & ")"

                    Dim rpt As New xtraReportFormulirIGD1

                    rpt.ShowPrintMarginsWarning = False
                    rpt.Watermark.Text = sWATERMARK

                    ListdataKDCPPT.Add(ds.KDPENDAFTARAN)
                    rpt.BindingSource.DataSource = ds
                    rpt.ExportToPdf(FolderSimpan & ds.KDPENDAFTARAN & ".pdf")
                    dataList.Add(File.ReadAllBytes(FolderSimpan & ds.KDPENDAFTARAN & ".pdf"))
                End If

                If dataList.Count > 0 Then
                    dsLoad = MergeFilesByte(dataList)
                    Dim stream As New MemoryStream(dsLoad)
                    PdfViewerAsesmenMedisAwalIGD.LoadDocument(stream)
                End If

                SplashScreenManager.CloseForm(False)
            Catch oErr As Exception
                SplashScreenManager.CloseForm(False)
                MsgBox("Cetak " & KATEGORI & "" & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            End Try
        ElseIf KATEGORI = "DPJP DAN PPJP" Then
            Try
                SplashScreenManager.ShowForm(Me, GetType(frmLoadingPanel), True, True, False)

                SplashScreenManager.Default.SetWaitFormCaption("Processing data " & KATEGORI & ".....")

                Dim oDigital As New EMedrek.clsS_DIGITAL_RI_16
                Dim FolderSimpan = "C:/ASESMENMEDISAWAL/"

                If Not Directory.Exists(FolderSimpan) Then
                    Directory.CreateDirectory(FolderSimpan)
                Else
                    DeleteDirectory(FolderSimpan)
                    Directory.CreateDirectory(FolderSimpan)
                End If

                Dim ListdataKDCPPT As New List(Of String)
                Dim dataList As New List(Of Byte())
                Dim dsLoad() As Byte

                Dim ds = oDigital.GetData(Kode)
                If ds IsNot Nothing Then
                    NAMA = lblNamaPasien.Text
                    JENISKELAMIN = lblJenisKelamin.Text
                    TANGGALLAHIR = lblTanggalLahir.Text

                    Dim rpt As New xtraReportEMedrekRI_16

                    rpt.ShowPrintMarginsWarning = False
                    rpt.Watermark.Text = sWATERMARK

                    ListdataKDCPPT.Add(ds.KDPENDAFTARAN)
                    rpt.BindingSource.DataSource = ds
                    rpt.ExportToPdf(FolderSimpan & ds.KDPENDAFTARAN & ".pdf")
                    dataList.Add(File.ReadAllBytes(FolderSimpan & ds.KDPENDAFTARAN & ".pdf"))
                End If

                If dataList.Count > 0 Then
                    dsLoad = MergeFilesByte(dataList)
                    Dim stream As New MemoryStream(dsLoad)
                    PdfViewerAsesmenMedisAwalIGD.LoadDocument(stream)
                End If

                SplashScreenManager.CloseForm(False)
            Catch oErr As Exception
                SplashScreenManager.CloseForm(False)
                MsgBox("Cetak " & KATEGORI & "" & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            End Try
        ElseIf KATEGORI = "ASESMEN AWAL MEDIS RAWAT INAP" Then
            Try
                SplashScreenManager.ShowForm(Me, GetType(frmLoadingPanel), True, True, False)

                SplashScreenManager.Default.SetWaitFormCaption("Processing data " & KATEGORI & ".....")

                Dim FolderSimpan = "C:/ASESMENMEDISAWAL/"

                If Not Directory.Exists(FolderSimpan) Then
                    Directory.CreateDirectory(FolderSimpan)
                Else
                    DeleteDirectory(FolderSimpan)
                    Directory.CreateDirectory(FolderSimpan)
                End If

                Dim ListdataKDCPPT As New List(Of String)
                Dim dataList As New List(Of Byte())
                Dim dsLoad() As Byte

                Dim ds = oDigital_DischargePlanning.GetData(Kode)
                If ds IsNot Nothing Then
                    NAMA = lblNamaPasien.Text
                    JENISKELAMIN = lblJenisKelamin.Text
                    TANGGALLAHIR = lblTanggalLahir.Text
                    sKDUSER_TTD = ds.KDUSER

                    Dim rpt As New xtraReportEMedrekRI_13

                    rpt.ShowPrintMarginsWarning = False
                    rpt.Watermark.Text = sWATERMARK

                    ListdataKDCPPT.Add(ds.KDREG)
                    rpt.BindingSource.DataSource = ds
                    rpt.ExportToPdf(FolderSimpan & ds.KDREG & ".pdf")
                    dataList.Add(File.ReadAllBytes(FolderSimpan & ds.KDREG & ".pdf"))
                End If

                If dataList.Count > 0 Then
                    dsLoad = MergeFilesByte(dataList)
                    Dim stream As New MemoryStream(dsLoad)
                    PdfViewerAsesmenMedisAwalIGD.LoadDocument(stream)
                End If

                SplashScreenManager.CloseForm(False)
            Catch oErr As Exception
                SplashScreenManager.CloseForm(False)
                MsgBox("Cetak " & KATEGORI & "" & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            End Try
        ElseIf KATEGORI = "ASESMEN AWAL MEDIS PASIEN PARU" Then
            Try
                SplashScreenManager.ShowForm(Me, GetType(frmLoadingPanel), True, True, False)

                SplashScreenManager.Default.SetWaitFormCaption("Processing data " & KATEGORI & ".....")

                Dim oDigital As New Digital.clsDigital_RJ_36
                Dim FolderSimpan = "C:/ASESMENMEDISAWAL/"

                If Not Directory.Exists(FolderSimpan) Then
                    Directory.CreateDirectory(FolderSimpan)
                Else
                    DeleteDirectory(FolderSimpan)
                    Directory.CreateDirectory(FolderSimpan)
                End If

                Dim ListdataKDCPPT As New List(Of String)
                Dim dataList As New List(Of Byte())
                Dim dsLoad() As Byte

                Dim ds = oDigital.GetData(Kode)
                If ds IsNot Nothing Then
                    NAMA = lblNamaPasien.Text
                    JENISKELAMIN = lblJenisKelamin.Text
                    TANGGALLAHIR = lblTanggalLahir.Text

                    Dim rpt As New xtraReportEMedrekRJ_36

                    rpt.ShowPrintMarginsWarning = False
                    rpt.Watermark.Text = sWATERMARK

                    ListdataKDCPPT.Add(ds.KDPENDAFTARAN)
                    rpt.BindingSource.DataSource = ds
                    rpt.ExportToPdf(FolderSimpan & ds.KDPENDAFTARAN & ".pdf")
                    dataList.Add(File.ReadAllBytes(FolderSimpan & ds.KDPENDAFTARAN & ".pdf"))
                End If

                If dataList.Count > 0 Then
                    dsLoad = MergeFilesByte(dataList)
                    Dim stream As New MemoryStream(dsLoad)
                    PdfViewerAsesmenMedisAwalIGD.LoadDocument(stream)
                End If

                SplashScreenManager.CloseForm(False)
            Catch oErr As Exception
                SplashScreenManager.CloseForm(False)
                MsgBox("Cetak " & KATEGORI & "" & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            End Try
        Else
            MsgBox("Cetak " & KATEGORI & "" & " Belum Tersedia", MsgBoxStyle.Exclamation, Me.Text)
        End If
    End Sub
    Private Sub fn_LoadPdfViewerPonek()
        Try
            PdfViewerAsesmenAwalPerawatIGD.CloseDocument()

            SplashScreenManager.ShowForm(Me, GetType(frmLoadingPanel), True, True, False)

            SplashScreenManager.Default.SetWaitFormCaption("Processing data Asesmen Kebidanan IGD Ponek.....")

            Dim FolderSimpan = "C:/PONEKIGD/"

            If Not Directory.Exists(FolderSimpan) Then
                Directory.CreateDirectory(FolderSimpan)
            Else
                DeleteDirectory(FolderSimpan)
                Directory.CreateDirectory(FolderSimpan)
            End If

            Dim oIGD As New Transaksi.clsDigital_IGD_03

            Dim dataList As New List(Of Byte())
            Dim dsLoad() As Byte

            NAMA = lblNamaPasien.Text
            RM = lblNoRM.Text
            TANGGALLAHIR = sTanggalLahir.ToString("dd-MM-yyyy", CultureInfo.InvariantCulture)

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
            SQL &= "S_PENDAFTARAN_H A "
            SQL &= "WHERE "
            SQL &= "A.KDCUSTOMER = '" & lblNoRM.Text & "' "

            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "S_TRANSAKSIRI_H")

            For xloop As Integer = 0 To ds.Tables("S_TRANSAKSIRI_H").Rows.Count - 1
                Dim dsek = oIGD.GetData(ds.Tables("S_TRANSAKSIRI_H").Rows(xloop)("KDREG"))
                If dsek IsNot Nothing Then
                    Dim rpt As New xtraReportFormulirIGD3

                    rpt.ShowPrintMarginsWarning = False
                    rpt.Watermark.Text = sWATERMARK

                    rpt.BindingSource.DataSource = dsek
                    rpt.ExportToPdf(FolderSimpan & dsek.KDPENDAFTARAN & ".pdf")
                    dataList.Add(File.ReadAllBytes(FolderSimpan & dsek.KDPENDAFTARAN & ".pdf"))
                End If
            Next

            If dataList.Count > 0 Then
                dsLoad = MergeFilesByte(dataList)
                Dim stream As New MemoryStream(dsLoad)
                PdfViewerPonek.LoadDocument(stream)
            End If

            SplashScreenManager.CloseForm(False)
        Catch oErr As Exception
            SplashScreenManager.CloseForm(False)
            MsgBox("Cetak Asesmen Kebidanan IGD Ponek" & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_BookingOperasi()
        Try
            grvRencanaOperasi.OptionsSelection.MultiSelect = True
            grvRencanaOperasi.SelectAll()
            grvRencanaOperasi.DeleteSelectedRows()
            grvRencanaOperasi.OptionsSelection.MultiSelect = False

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
            SQL &= "A.TANGGALOPERASI "
            SQL &= ",A.JENISTINDAKAN "
            SQL &= ",A.ISAPROVAL "
            SQL &= ",A.NOIDUSER "
            SQL &= ",A.REMARKS "
            SQL &= "FROM "
            SQL &= "SET_BOOKING_JADWALOPERASI A "
            SQL &= "INNER JOIN S_PENDAFTARAN_H B "
            SQL &= "ON A.KDREG = B.KDREG "
            SQL &= "WHERE "
            SQL &= "B.KDCUSTOMER = '" & lblNoRM.Text & "' "

            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "SET_BOOKING_JADWALOPERASI")

            grdRencanaOperasi.DataSource = ds.Tables("SET_BOOKING_JADWALOPERASI")
            grdRencanaOperasi.ForceInitialize()

            If oConn.State = ConnectionState.Open Then
                oConn.Close()
            End If

            For iLoop As Integer = 0 To grvRencanaOperasi.Columns.Count - 1
                If grvRencanaOperasi.Columns(iLoop).ColumnType.Name = "Decimal" Then
                    grvRencanaOperasi.Columns(iLoop).DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                    grvRencanaOperasi.Columns(iLoop).DisplayFormat.FormatString = "{0:n2}"
                    grvRencanaOperasi.Columns(iLoop).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
                ElseIf grvRencanaOperasi.Columns(iLoop).ColumnType.Name = "DateTime" Then
                    grvRencanaOperasi.Columns(iLoop).DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
                    grvRencanaOperasi.Columns(iLoop).DisplayFormat.FormatString = "{0:dd/MM/yyyy}"
                End If
            Next

            grvRencanaOperasi.BestFitColumns()

        Catch oErr As Exception
            MsgBox("Load Booking Jadwal Operasi : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadDataPenunjang()
        Try
            grvUpload.OptionsSelection.MultiSelect = True
            grvUpload.SelectAll()
            grvUpload.DeleteSelectedRows()
            grvUpload.OptionsSelection.MultiSelect = False

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
            SQL &= ",CekData = D.ISDEFAULT "
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
            SQL &= "WHERE B.KDCUSTOMER = '" & lblNoRM.Text & "' "
            SQL &= "AND D.DESCRIPTION <> 'ADMINISTRASI' "

            'SQL &= "AND D.ISDEFAULT = 0 "

            'SQL &= "UNION "

            'SQL &= "SELECT "
            'SQL &= "Tanggal = B.DATE "
            'SQL &= ",NamaUpload = 'RONTGEN' "
            'SQL &= ",Catatan = A.EXAMDESC "
            'SQL &= ",AlamatUpload = A.EXAMDESC "
            'SQL &= ",Type = 'RONTGEN' "
            'SQL &= ",Kodeupload = CONVERT(NVARCHAR(50), A.KDRAD) "
            'SQL &= "FROM "
            'SQL &= "S_RADIOLOGI_H A "
            'SQL &= "INNER JOIN S_PENDAFTARAN_H B "
            'SQL &= "ON A.KDREG = B.KDREG "
            'SQL &= "INNER JOIN M_DEPARTMENT E "
            'SQL &= "ON B.KDDEPARTMENT = E.KDDEPARTMENT "
            'SQL &= "INNER JOIN M_DOCTOR F "
            'SQL &= "ON B.KDDOCTOR = F.KDDOCTOR "
            'SQL &= "WHERE B.KDCUSTOMER = '" & lblNoRM.Text & "' "

            'SQL &= "UNION "

            'SQL &= "SELECT "
            'SQL &= "Tanggal = B.DATE "
            'SQL &= ",NamaUpload = 'USG' "
            'SQL &= ",Catatan = A.KDTARIF "
            'SQL &= ",AlamatUpload = A.GEJALA "
            'SQL &= ",Type = 'USG' "
            'SQL &= ",Kodeupload =  CONVERT(NVARCHAR(50), A.KDUSG)  "
            'SQL &= "FROM "
            'SQL &= "S_USG_H A "
            'SQL &= "INNER JOIN S_PENDAFTARAN_H B "
            'SQL &= "ON A.KDREG = B.KDREG "
            'SQL &= "INNER JOIN M_DEPARTMENT E "
            'SQL &= "ON B.KDDEPARTMENT = E.KDDEPARTMENT "
            'SQL &= "INNER JOIN M_DOCTOR F "
            'SQL &= "ON B.KDDOCTOR = F.KDDOCTOR "
            'SQL &= "WHERE B.KDCUSTOMER = '" & lblNoRM.Text & "' "

            'SQL &= "UNION "

            'SQL &= "SELECT "
            'SQL &= "Tanggal = B.DATE "
            'SQL &= ",NamaUpload = 'LABORATORIUM' "
            'SQL &= ",Catatan = H.NAMA_TARIF "
            'SQL &= ",AlamatUpload = '" & oFolder & "' "
            'SQL &= ",Type = 'browser' "
            'SQL &= ",Kodeupload = A.KDTRANSAKSILAB "
            'SQL &= "FROM "
            'SQL &= "S_TRANSAKSILAB_H A "
            'SQL &= "INNER JOIN S_PENDAFTARAN_H B "
            'SQL &= "ON A.KDREG = B.KDREG "
            'SQL &= "INNER JOIN M_DEPARTMENT E "
            'SQL &= "ON B.KDDEPARTMENT = E.KDDEPARTMENT "
            'SQL &= "INNER JOIN M_DOCTOR F "
            'SQL &= "ON B.KDDOCTOR = F.KDDOCTOR "
            'SQL &= "INNER JOIN S_TRANSAKSILAB_D G "
            'SQL &= "ON A.KDTRANSAKSILAB = G.KDTRANSAKSILAB "
            'SQL &= "INNER JOIN M_TARIF_NEW AS H "
            'SQL &= "ON G.KDTARIF = H.KDTARIF "
            'SQL &= "WHERE B.KDCUSTOMER = '" & lblNoRM.Text & "' "

            'SQL &= "UNION "

            'SQL &= "SELECT "
            'SQL &= "Tanggal = D.DATE "
            'SQL &= ",NamaUpload = 'RADIOLOGI' "
            'SQL &= ",Catatan = B.NAMA_TARIF "
            'SQL &= ",AlamatUpload = C.NORADIOLOGI "
            'SQL &= ",Type = 'APIRADIOLOGI' "
            'SQL &= ",Kodeupload = A.KDTRANSAKSI "
            'SQL &= "FROM "
            'SQL &= "S_TRANSAKSI_RJ A "
            'SQL &= "INNER JOIN M_TARIF_NEW AS B "
            'SQL &= "ON A.KDTARIF = B.KDTARIF "
            'SQL &= "INNER JOIN S_TRANSAKSI_H AS C "
            'SQL &= "ON A.KDTRANSAKSI = C.KDTRANSAKSI "
            'SQL &= "INNER JOIN S_PENDAFTARAN_H D "
            'SQL &= "ON C.KDREG = D.KDREG "
            'SQL &= "WHERE D.KDCUSTOMER = '" & lblNoRM.Text & "' "

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
        Try
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
        Catch ex As Exception

        End Try

    End Sub
    Private Sub fn_LoadDataGerdQ()
        Try
            grvGerdQ.OptionsSelection.MultiSelect = True
            grvGerdQ.SelectAll()
            grvGerdQ.DeleteSelectedRows()
            grvGerdQ.OptionsSelection.MultiSelect = False

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
            SQL &= "KODE = A.KDGERD "
            SQL &= ",TANGGAL = A.DATE "
            SQL &= ",A.RUANGRAWAT "
            SQL &= ",A.DIAGNOSA "
            SQL &= "FROM "
            SQL &= "S_DIGITAL_IGD_01_GERD_H A "
            SQL &= "WHERE A.NOREKAMMEDIS = '" & lblNoRM.Text & "' "
            SQL &= "ORDER BY A.DATE DESC "

            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "S_DIGITAL_IGD_01_GERD_H")

            grdGerdQ.DataSource = ds.Tables("S_DIGITAL_IGD_01_GERD_H")
            grdGerdQ.ForceInitialize()

            If oConn.State = ConnectionState.Open Then
                oConn.Close()
            End If

            grvGerdQ.Columns("KODE").Visible = False

            For iLoop As Integer = 0 To grvGerdQ.Columns.Count - 1
                If grvGerdQ.Columns(iLoop).ColumnType.Name = "Decimal" Then
                    grvGerdQ.Columns(iLoop).DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                    grvGerdQ.Columns(iLoop).DisplayFormat.FormatString = "{0:n2}"
                    grvGerdQ.Columns(iLoop).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
                ElseIf grvGerdQ.Columns(iLoop).ColumnType.Name = "DateTime" Then
                    grvGerdQ.Columns(iLoop).DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
                    grvGerdQ.Columns(iLoop).DisplayFormat.FormatString = "{0:dd/MM/yyyy HH:mm}"
                End If
            Next

        Catch oErr As Exception
            MsgBox("Load Gerd Q : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadDataLaporanOperasiList()
        Try
            grvLaporanOperasi.OptionsSelection.MultiSelect = True
            grvLaporanOperasi.SelectAll()
            grvLaporanOperasi.DeleteSelectedRows()
            grvLaporanOperasi.OptionsSelection.MultiSelect = False

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
            SQL &= ",JENISTINDAKAN = A.PROSEDUR2 "
            'SQL &= ",[DOKTER ANESTESI] = A.TextEdit4 "
            'SQL &= ",PEMBUATLAPORAN = A.KDUSER "
            SQL &= "FROM "
            SQL &= "S_DIGITAL_OK_LAPORANOPERASI A "
            SQL &= "WHERE A.KDCUSTOMER = '" & lblNoRM.Text & "' "
            SQL &= "AND A.ISDELETE = '0' "

            SQL &= "UNION "

            SQL &= "SELECT "
            SQL &= "KETERANGAN = 'LAPORAN TINDAKAN' "
            SQL &= ",KODE = A.KDLAPORANTINDAKAN "
            SQL &= ",TANGGAL = A.DATE "
            SQL &= ",A.JENIS_OPERASI "
            SQL &= ",[DOKTER OPERATOR] = A.DOKTER_NAMEDISPLAY "
            SQL &= ",JENISTINDAKAN = A.PROSEDUR2 "
            'SQL &= ",[DOKTER ANESTESI] = '-' "
            'SQL &= ",PEMBUATLAPORAN = A.KDUSER "
            SQL &= "FROM "
            SQL &= "S_DIGITAL_OK_LAPORANTINDAKAN A "
            SQL &= "WHERE A.KDCUSTOMER = '" & lblNoRM.Text & "' "
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

            grvLaporanOperasi.Columns("JENIS_OPERASI").Caption = "JENIS"

        Catch oErr As Exception
            MsgBox("Load Gerd Q : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadDataPengantarList()
        Try
            grvPengantar.OptionsSelection.MultiSelect = True
            grvPengantar.SelectAll()
            grvPengantar.DeleteSelectedRows()
            grvPengantar.OptionsSelection.MultiSelect = False

            If lblNoRM.Text = "" Then Exit Sub

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

            SQL = "EXEC ORDERPENUNJANG_LIST @KDCUSTOMER = '" & lblNoRM.Text & "' "

            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "PENGANTAR")

            grdPengantar.DataSource = ds.Tables("PENGANTAR")
            grdPengantar.ForceInitialize()

            If oConn.State = ConnectionState.Open Then
                oConn.Close()
            End If

            grvPengantar.Columns("KODE").Visible = False
            grvPengantar.Columns("SEQ").Visible = False

            For iLoop As Integer = 0 To grvPengantar.Columns.Count - 1
                If grvPengantar.Columns(iLoop).ColumnType.Name = "Decimal" Then
                    grvPengantar.Columns(iLoop).DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                    grvPengantar.Columns(iLoop).DisplayFormat.FormatString = "{0:n2}"
                    grvPengantar.Columns(iLoop).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
                ElseIf grvPengantar.Columns(iLoop).ColumnType.Name = "DateTime" Then
                    grvPengantar.Columns(iLoop).DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
                    grvPengantar.Columns(iLoop).DisplayFormat.FormatString = "{0:dd/MM/yyyy HH:mm}"
                End If
            Next

        Catch oErr As Exception
            MsgBox("Load Gerd Q : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadDataLaporanAsesmenAwalPerawatList(ByVal Parameter As String)
        Try
            grvListAsesmenAwalPerawat.OptionsSelection.MultiSelect = True
            grvListAsesmenAwalPerawat.SelectAll()
            grvListAsesmenAwalPerawat.DeleteSelectedRows()
            grvListAsesmenAwalPerawat.OptionsSelection.MultiSelect = False

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

            'Penyakit Dalam, Bedah, Jantung
            SQL = "SELECT "
            SQL &= "* "
            SQL &= "FROM ( "
            SQL &= "(SELECT "
            SQL &= "Tanggal = A.DATE "
            'SQL &= ",TanggalDiPerbaharui = A.DATEUPDATED "
            SQL &= ",FORMULIR = 'ASESMEN AWAL KEPERAWATAN BEDAH, PENYAKIT DALAM DAN JANTUNG' "
            SQL &= ",NoRegister = A.KDPENDAFTARAN "
            SQL &= ",Kode = A.KDPENDAFTARAN "
            SQL &= ",Deskripsi = '' "
            SQL &= ",DPJP = A.DOKTER2_NAMEDISPLAY "
            SQL &= ",A.KDUSER "
            SQL &= "FROM "
            SQL &= "S_DIGITAL_RI_17 A "
            SQL &= "WHERE A.KDCUSTOMER = '" & Parameter & "' "

            'SQL &= ")UNION ALL( "

            'SQL &= "SELECT "
            'SQL &= "Tanggal = A.DATE "
            ''SQL &= ",TanggalDiPerbaharui = A.DATEUPDATED "
            'SQL &= ",FORMULIR = 'ASESMEN AWAL KEPERAWATAN ICU' "
            'SQL &= ",Kode = A.KDPENDAFTARAN "
            'SQL &= ",Deskripsi = '' "
            'SQL &= ",A.KDUSER "
            'SQL &= "FROM "
            'SQL &= "S_DIGITAL_RI_24 A "
            'SQL &= "WHERE A.KDPENDAFTARAN = '" & Parameter & "' "

            'SQL &= ")UNION ALL( "

            'SQL &= "SELECT "
            'SQL &= "Tanggal = A.DATE "
            ''SQL &= ",TanggalDiPerbaharui = A.DATEUPDATED "
            'SQL &= ",FORMULIR = 'ASESMEN KEPERAWATAN REHABMEDIK' "
            'SQL &= ",Kode = A.KDPENDAFTARAN "
            'SQL &= ",Deskripsi = '' "
            'SQL &= ",A.KDUSER "
            'SQL &= "FROM "
            'SQL &= "S_DIGITAL_RI_25 A "
            'SQL &= "WHERE A.KDPENDAFTARAN = '" & Parameter & "' "

            SQL &= ")UNION ALL( "

            SQL &= "SELECT "
            SQL &= "Tanggal = A.DATE "
            'SQL &= ",TanggalDiPerbaharui = A.DATEUPDATED "
            SQL &= ",FORMULIR = 'ASESMEN AWAL KEPERAWATAN ANAK' "
            SQL &= ",NoRegister = A.KDPENDAFTARAN "
            SQL &= ",Kode = A.KDPENDAFTARAN "
            SQL &= ",Deskripsi = '' "
            SQL &= ",DPJP = A.DOKTER_NAMEDISPLAY "
            SQL &= ",A.KDUSER "
            SQL &= "FROM "
            SQL &= "S_DIGITAL_RI_40 A "
            SQL &= "WHERE A.KDCUSTOMER = '" & Parameter & "' "

            SQL &= ")UNION ALL( "

            SQL &= "SELECT "
            SQL &= "Tanggal = A.ASESMEN_01 "
            'SQL &= ",TanggalDiPerbaharui = A.DATEUPDATED "
            SQL &= ",FORMULIR = 'ASESMEN AWAL KEPERAWATAN RAWAT INAP' "
            SQL &= ",NoRegister = A.KDPENDAFTARAN "
            SQL &= ",Kode = A.KDPENDAFTARAN "
            SQL &= ",Deskripsi = '' "
            SQL &= ",DPJP = A.DOKTER2_NAMEDISPLAY "
            SQL &= ",A.KDUSER "
            SQL &= "FROM "
            SQL &= "S_DIGITAL_RI_38 A "
            SQL &= "WHERE A.KDCUSTOMER = '" & Parameter & "' "

            'SQL &= ")UNION ALL( "

            'SQL &= "SELECT "
            'SQL &= "Tanggal = A.DATE "
            ''SQL &= ",TanggalDiPerbaharui = A.DATEUPDATED "
            'SQL &= ",FORMULIR = 'ASESMEN AWAL KEPERAWATAN PASIEN HEMODIALISA' "
            'SQL &= ",Kode = A.KDPENDAFTARAN "
            'SQL &= ",Deskripsi = '' "
            'SQL &= ",A.KDUSER "
            'SQL &= "FROM "
            'SQL &= "S_DIGITAL_RJ_38 A "
            'SQL &= "WHERE A.KDPENDAFTARAN = '" & Parameter & "' "

            SQL &= ")UNION ALL( "

            SQL &= "SELECT "
            SQL &= "Tanggal = A.DATE "
            'SQL &= ",TanggalDiPerbaharui = A.DATEUPDATED "
            SQL &= ",FORMULIR = 'ASESMEN AWAL KEPERAWATAN KEBIDANAN' "
            SQL &= ",NoRegister = A.KDPENDAFTARAN "
            SQL &= ",Kode = A.KDPENDAFTARAN "
            SQL &= ",Deskripsi = '' "
            SQL &= ",DPJP = A.DOKTER_NAMEDISPLAY "
            SQL &= ",A.KDUSER "
            SQL &= "FROM "
            SQL &= "S_DIGITAL_RI_29 A "
            SQL &= "WHERE A.KDCUSTOMER = '" & Parameter & "' "

            'SQL &= ")UNION ALL( "

            'SQL &= "SELECT "
            'SQL &= "Tanggal = A.DATE "
            ''SQL &= ",TanggalDiPerbaharui = A.DATEUPDATED "
            'SQL &= ",FORMULIR = 'ASESMEN AWAL KEPERAWATAN ENDOSKOPI' "
            'SQL &= ",Kode = A.KDPENDAFTARAN "
            'SQL &= ",Deskripsi = '' "
            'SQL &= ",A.KDUSER "
            'SQL &= "FROM "
            'SQL &= "S_DIGITAL_RJ_05 A "
            'SQL &= "WHERE A.KDPENDAFTARAN = '" & Parameter & "' "

            'SQL &= ")UNION ALL( "

            'SQL &= "SELECT "
            'SQL &= "Tanggal = A.DATE "
            ''SQL &= ",TanggalDiPerbaharui = A.DATEUPDATED "
            'SQL &= ",FORMULIR = 'PENGKAJIAN AWAL KEPERAWATAN RAWAT INAP JIWA' "
            'SQL &= ",Kode = A.KDPENDAFTARAN "
            'SQL &= ",Deskripsi = '' "
            'SQL &= ",A.KDUSER "
            'SQL &= "FROM "
            'SQL &= "S_DIGITAL_RI_44 A "
            'SQL &= "WHERE A.KDPENDAFTARAN = '" & Parameter & "' "

            'SQL &= ")UNION ALL( "

            'SQL &= "SELECT "
            'SQL &= "Tanggal = A.DATE "
            ''SQL &= ",TanggalDiPerbaharui = A.DATEUPDATED "
            'SQL &= ",FORMULIR = 'ASESMEN AWAL KEPERAWATAN MATERNITAS' "
            'SQL &= ",Kode = A.KDPENDAFTARAN "
            'SQL &= ",Deskripsi = '' "
            'SQL &= ",A.KDUSER "
            'SQL &= "FROM "
            'SQL &= "S_DIGITAL_RI_OBSTETRI_PERAWAT A "
            'SQL &= "WHERE A.KDPENDAFTARAN = '" & Parameter & "' "

            SQL &= ")UNION ALL( "

            SQL &= "SELECT "
            SQL &= "Tanggal = A.DATE "
            'SQL &= ",TanggalDiPerbaharui = A.DATEUPDATED "
            SQL &= ",FORMULIR = 'ASESMEN AWAL KEPERAWATAN PASIEN NEONATAL' "
            SQL &= ",NoRegister = A.KDPENDAFTARAN "
            SQL &= ",Kode = A.KDASESMEN "
            SQL &= ",Deskripsi = A.KDASESMEN "
            SQL &= ",DPJP = '-' "
            SQL &= ",A.KDUSER "
            SQL &= "FROM "
            SQL &= "S_DIGITAL_ASKEP_NEONATAL A "
            SQL &= "WHERE A.KDCUSTOMER = '" & Parameter & "' "

            SQL &= ")UNION ALL( "

            SQL &= "SELECT "
            SQL &= "Tanggal = A.DATE "
            'SQL &= ",TanggalDiPerbaharui = A.DATEUPDATED "
            SQL &= ",FORMULIR = 'ASESMEN AWAL KEPERAWATAN GAWAT DARURAT' "
            SQL &= ",NoRegister = A.KDPENDAFTARAN "
            SQL &= ",Kode = A.KDPENDAFTARAN "
            SQL &= ",Deskripsi = '' "
            SQL &= ",DPJP = '-' "
            SQL &= ",A.KDUSER "
            SQL &= "FROM "
            SQL &= "S_DIGITAL_IGD_02_NEW A "
            SQL &= "WHERE A.KDCUSTOMER = '" & Parameter & "' "

            SQL &= ")UNION ALL( "

            SQL &= "SELECT "
            SQL &= "Tanggal = A.DATE "
            'SQL &= ",TanggalDiPerbaharui = A.DATEUPDATED "
            SQL &= ",FORMULIR = 'LEMBAR DISCHARGE PLANNING' "
            SQL &= ",NoRegister = A.KDPENDAFTARAN "
            SQL &= ",Kode = A.KDPENDAFTARAN "
            SQL &= ",Deskripsi = A.DIAGNOSAKEPERAWATAN "
            SQL &= ",DPJP = '-' "
            SQL &= ",A.KDUSER "
            SQL &= "FROM "
            SQL &= "S_DIGITAL_LEMBARDISCHARGEPLANNING A "
            SQL &= "WHERE A.KDCUSTOMER = '" & Parameter & "' "

            SQL &= ")UNION ALL( "

            SQL &= "SELECT "
            SQL &= "Tanggal = A.DATE "
            'SQL &= ",TanggalDiPerbaharui = A.DATEUPDATED "
            SQL &= ",FORMULIR = 'CATATAN SKRINING DAN EDUKASI' "
            SQL &= ",NoRegister = A.KDREG "
            SQL &= ",Kode = A.KDREG "
            SQL &= ",Deskripsi = A.STATUS_GIZI "
            SQL &= ",DPJP = A.NMDOCTOR "
            SQL &= ",A.KDUSER "
            SQL &= "FROM "
            SQL &= "S_DIGITAL_RI_20 A "
            SQL &= "WHERE A.KDCUSTOMER = '" & Parameter & "' "

            SQL &= ")UNION ALL( "

            SQL &= "SELECT "
            SQL &= "Tanggal = A.DATE "
            'SQL &= ",TanggalDiPerbaharui = A.DATEUPDATED "
            SQL &= ",FORMULIR = 'ASESMEN LANJUT' "
            SQL &= ",NoRegister = A.KDREG "
            SQL &= ",Kode = A.KDREG "
            SQL &= ",Deskripsi = A.MOBILITAS "
            SQL &= ",DPJP = A.NMDOCTOR "
            SQL &= ",A.KDUSER "
            SQL &= "FROM "
            SQL &= "S_DIGITAL_ASESMENGIZILANJUT A "
            SQL &= "WHERE A.KDCUSTOMER = '" & Parameter & "' "

            SQL &= ") "
            SQL &= ") AS X "
            SQL &= "ORDER BY X.Tanggal DESC "

            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "ASESMENAWALPERAWAT")

            grdListAsesmenAwalPerawat.DataSource = ds.Tables("ASESMENAWALPERAWAT")
            grdListAsesmenAwalPerawat.ForceInitialize()

            If oConn.State = ConnectionState.Open Then
                oConn.Close()
            End If

            grvListAsesmenAwalPerawat.Columns("NoRegister").Visible = False
            grvListAsesmenAwalPerawat.Columns("Kode").Visible = False
            grvListAsesmenAwalPerawat.Columns("DPJP").Visible = False
            grvListAsesmenAwalPerawat.Columns("Deskripsi").Visible = False

            grvListAsesmenAwalPerawat.Columns("FORMULIR").Caption = "Formulir"
            grvListAsesmenAwalPerawat.Columns("KDUSER").Caption = "User"

            For iLoop As Integer = 0 To grvListAsesmenAwalPerawat.Columns.Count - 1
                If grvListAsesmenAwalPerawat.Columns(iLoop).ColumnType.Name = "Decimal" Then
                    grvListAsesmenAwalPerawat.Columns(iLoop).DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                    grvListAsesmenAwalPerawat.Columns(iLoop).DisplayFormat.FormatString = "{0:n2}"
                    grvListAsesmenAwalPerawat.Columns(iLoop).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
                ElseIf grvListAsesmenAwalPerawat.Columns(iLoop).ColumnType.Name = "DateTime" Then
                    grvListAsesmenAwalPerawat.Columns(iLoop).DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
                    grvListAsesmenAwalPerawat.Columns(iLoop).DisplayFormat.FormatString = "{0:dd/MM/yyyy HH:mm}"
                End If
            Next

        Catch oErr As Exception
            MsgBox("Load Asesmen Awal Perawat List : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    'Private Sub fn_LoadPdfViewerSBAR()
    '    Try
    '        'PdfViewerSBAR.CloseDocument()

    '        SplashScreenManager.ShowForm(Me, GetType(frmLoadingPanel), True, True, False)

    '        SplashScreenManager.Default.SetWaitFormCaption("Processing data SBAR.....")

    '        If lblNoRM.Text = String.Empty Then Exit Sub

    '        Dim FolderSimpan = "C:/SBAR/"

    '        If Not Directory.Exists(FolderSimpan) Then
    '            Directory.CreateDirectory(FolderSimpan)
    '        Else
    '            DeleteDirectory(FolderSimpan)
    '            Directory.CreateDirectory(FolderSimpan)
    '        End If

    '        Dim dataList As New List(Of Byte())
    '        Dim dsLoad() As Byte
    '        Dim oS_DIGITAL_RI_31 As New Transaksi.clsDigital_SBAR

    '        For Each xloop In oS_DIGITAL_RI_31.GetDataByRM(lblNoRM.Text)
    '            Dim ds = oS_DIGITAL_RI_31.GetData(xloop.KDPINDAHAN)
    '            If ds IsNot Nothing Then
    '                Dim rpt As New xtraReportEMedrekRI_31

    '                rpt.ShowPrintMarginsWarning = False
    '                rpt.Watermark.Text = sWATERMARK
    '                rpt.BindingSource1.DataSource = ds

    '                NAMA = lblNamaPasien.Text
    '                TANGGALLAHIR = sTanggalLahir.ToString("dd-MM-yyyy")
    '                JENISKELAMIN = lblJenisKelamin.Text

    '                rpt.ExportToPdf(FolderSimpan & "HASIL" & ds.KDPINDAHAN & ".pdf")
    '                dataList.Add(File.ReadAllBytes(FolderSimpan & "HASIL" & ds.KDPINDAHAN & ".pdf"))
    '            End If
    '        Next

    '        If dataList.Count > 0 Then
    '            dsLoad = MergeFilesByte(dataList)
    '            Dim stream As New MemoryStream(dsLoad)
    '            'PdfViewerSBAR.LoadDocument(stream)
    '        End If
    '        SplashScreenManager.CloseForm(False)
    '    Catch oErr As Exception
    '        SplashScreenManager.CloseForm(False)
    '        MsgBox("Cetak CPPT Ranap" & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
    '    End Try
    'End Sub
#End Region
#Region "Command Button"
    Private Sub frmItem_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        'Select Case e.KeyCode
        '    Case Keys.F3
        '        btnSimpanCPPT_Click()
        '        'Case Keys.F8
        '        '    btnGrouper_CPPT_Click()
        'End Select
    End Sub
    Private Sub XtraTabControl1_SelectedPageChanged() Handles XtraTabControl1.SelectedPageChanged
        Try

            sKODEASESMENCOPY = String.Empty

            If XtraTabControl1.SelectedTabPageIndex = 0 Then
                fn_ChangeFormStateCPPT()
            ElseIf XtraTabControl1.SelectedTabPageIndex = 1 Then
                fn_ChangeFormStateDischargePlanning()
            ElseIf XtraTabControl1.SelectedTabPageIndex = 2 Then
                fn_ChangeFormStateRINGKASANKELUAR()
            ElseIf XtraTabControl1.SelectedTabPageIndex = 3 Then
                Try
                    grvHandOver.OptionsSelection.MultiSelect = True
                    grvHandOver.SelectAll()
                    grvHandOver.DeleteSelectedRows()
                    grvHandOver.OptionsSelection.MultiSelect = False

                    Dim ds = From x In oCPPT.GetDataByRMRANAP(lblNoRM.Text)
                             Where x.SUKU <> "TAMBAH GIZI"
                             Select x.KDCPPT, STATUS = IIf(x.SUKU = "TAMBAH SBAR", "SBAR", "CPPT"), CEKUPDATE = IIf(x.DATECREATED <> x.DATEUPDATED, "UPDATE", ""), TANGGAL = x.DATE, x.PROFESI, x.SUBJEKTIF, x.OBJEKTIF, x.ASSEMENT, x.PLANNING, [USER] = x.KDUSER

                    grdHandOver.DataSource = ds.ToList

                    For iLoop As Integer = 0 To grvHandOver.Columns.Count - 1
                        If grvHandOver.Columns(iLoop).ColumnType.Name = "Decimal" Then
                            grvHandOver.Columns(iLoop).DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                            grvHandOver.Columns(iLoop).DisplayFormat.FormatString = "{0:n2}"
                            grvHandOver.Columns(iLoop).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
                        ElseIf grvHandOver.Columns(iLoop).ColumnType.Name = "DateTime" Then
                            grvHandOver.Columns(iLoop).DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
                            grvHandOver.Columns(iLoop).DisplayFormat.FormatString = "{0:dd/MM/yyyy}"
                        End If
                    Next

                    grvHandOver.Columns("KDCPPT").Visible = False

                Catch oErr As Exception
                    MsgBox("Load Data CPPT Rawat Inap" & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            ElseIf XtraTabControl1.SelectedTabPageIndex = 4 Then
                fn_LoadDataTransferInternal()
            ElseIf XtraTabControl1.SelectedTabPageIndex = 5 Then
                fn_LoadDataAsesmenAwalPerawat(lblNoRM.Text)
            ElseIf XtraTabControl1.SelectedTabPageIndex = 6 Then
                fn_LoadDataTindakanEvaluasiKeperawatan()
            ElseIf XtraTabControl1.SelectedTabPageIndex = 7 Then
                fn_LoadDataLembarEWS(lblNoRM.Text)
            ElseIf XtraTabControl1.SelectedTabPageIndex = 8 Then
                XtraTabControl3_SelectedPageChanged()
            ElseIf XtraTabControl1.SelectedTabPageIndex = 9 Then
                'Persetujuan dan Penolakan
            ElseIf XtraTabControl1.SelectedTabPageIndex = 10 Then
                fn_BookingOperasi()
            ElseIf XtraTabControl1.SelectedTabPageIndex = 11 Then
                fn_LoadLaporanOperasi(lblNoRM.Text)
            ElseIf XtraTabControl1.SelectedTabPageIndex = 12 Then
                fn_LoadDataLembarObservasi()
            ElseIf XtraTabControl1.SelectedTabPageIndex = 13 Then
                fn_LoadDataCatatanKeperawatan()
            ElseIf XtraTabControl1.SelectedTabPageIndex = 14 Then
                fn_LoadDataGerdQList()
            ElseIf XtraTabControl1.SelectedTabPageIndex = 15 Then
                XtraTabControl2_SelectedPageChanged()
            ElseIf XtraTabControl1.SelectedTabPageIndex = 16 Then
                XtraTabControl4_SelectedPageChanged()
            ElseIf XtraTabControl1.SelectedTabPageIndex = 17 Then
                fn_LoadLaporanTindakan(lblNoRM.Text)
            ElseIf XtraTabControl1.SelectedTabPageIndex = 18 Then
                fn_Konsultasi()
            ElseIf XtraTabControl1.SelectedTabPageIndex = 19 Then
                fn_LoadDataFormulirMPPList()
            ElseIf XtraTabControl1.SelectedTabPageIndex = 20
                tabOrderList_SelectedPageChanged()
            End If
        Catch ex As Exception

        End Try

    End Sub
    Private Sub tabbedControlGroup1_SelectedPageChanged() Handles tabbedControlGroup1.SelectedPageChanged
        Try
            If tabbedControlGroup1.SelectedTabPageIndex = 0 Then
                fn_LoadPdfViewerCPPTRANANP()
            ElseIf tabbedControlGroup1.SelectedTabPageIndex = 1 Then
                fn_LoadPdfViewerCPPTRAJAL()
            ElseIf tabbedControlGroup1.SelectedTabPageIndex = 2 Then
                fn_LoadResumeRawatJlalanRawatInap(lblNoRM.Text)
            ElseIf tabbedControlGroup1.SelectedTabPageIndex = 3 Then
                fn_LoadDataLaboratorium(lblNoRM.Text)
            ElseIf tabbedControlGroup1.SelectedTabPageIndex = 4 Then
                fn_LoadDataRadiologi()
            ElseIf tabbedControlGroup1.SelectedTabPageIndex = 5 Then
                fn_DokterBersama()
            ElseIf tabbedControlGroup1.SelectedTabPageIndex = 6 Then
                fn_TransferInternal()
            ElseIf tabbedControlGroup1.SelectedTabPageIndex = 7 Then
                fn_LoadDataAsesmenAwalMedisList(lblNoRM.Text)
            ElseIf tabbedControlGroup1.SelectedTabPageIndex = 8 Then
                fn_LoadDataLaporanAsesmenAwalPerawatList(lblNoRM.Text)
            ElseIf tabbedControlGroup1.SelectedTabPageIndex = 9 Then
                fn_LoadPdfViewerLembarObservasi()
            ElseIf tabbedControlGroup1.SelectedTabPageIndex = 10 Then
                fn_LoadDataPenunjang()
            ElseIf tabbedControlGroup1.SelectedTabPageIndex = 11 Then
                fn_LoadDataGerdQ()
            ElseIf tabbedControlGroup1.SelectedTabPageIndex = 12 Then
                fn_LoadPdfViewerCatatanKeperawatan()
            ElseIf tabbedControlGroup1.SelectedTabPageIndex = 13 Then
                fn_LoadPdfViewerPonek()
            ElseIf tabbedControlGroup1.SelectedTabPageIndex = 14 Then
                fn_LoadDataLaporanOperasiList()
            ElseIf tabbedControlGroup1.SelectedTabPageIndex = 15 Then
                fn_LoadDataPengantarList()
            ElseIf tabbedControlGroup1.SelectedTabPageIndex = 16 Then
                fn_LoadDataFormulirMPPListViewer()
            End If
        Catch ex As Exception

        End Try

    End Sub
    Private Sub XtraTabControl2_SelectedPageChanged() Handles XtraTabControl2.SelectedPageChanged
        Try
            If XtraTabControl2.SelectedTabPageIndex = 1 Then
                fn_LoadDataCatatanSkriningdanEdukasiGiziList()
            ElseIf XtraTabControl2.SelectedTabPageIndex = 2 Then
                fn_LoadDataAsesmenLanjutGiziList()
            ElseIf XtraTabControl2.SelectedTabPageIndex = 3 Then
                fn_LoadDataMonitoringEvaluasiGiziList()
            End If
        Catch ex As Exception

        End Try

    End Sub
    Private Sub XtraTabControl3_SelectedPageChanged() Handles XtraTabControl3.SelectedPageChanged
        Try
            If XtraTabControl3.SelectedTabPageIndex = 1 Then
                fn_LoadDataRekonsiliasiObat()
            ElseIf XtraTabControl3.SelectedTabPageIndex = 2 Then

            ElseIf XtraTabControl3.SelectedTabPageIndex = 3 Then

            End If
        Catch ex As Exception

        End Try

    End Sub
    Private Sub XtraTabControl4_SelectedPageChanged() Handles XtraTabControl4.SelectedPageChanged
        Try
            If XtraTabControl4.SelectedTabPageIndex = 0 Then
                fn_LoadDataResepList()
            ElseIf XtraTabControl4.SelectedTabPageIndex = 1 Then

            ElseIf XtraTabControl4.SelectedTabPageIndex = 2 Then
                fn_LoadDataRekonsiliasiObatdiKOP()
            End If
        Catch ex As Exception

        End Try

    End Sub
    Private Sub tabOrderList_SelectedPageChanged() Handles tabOrderList.SelectedPageChanged
        Try
            If tabOrderList.SelectedTabPageIndex = 0 Then
                'Resep
                fn_LoadDataOrderResep()
            ElseIf tabOrderList.SelectedTabPageIndex = 1 Then
                'Laboratorium
                fn_LoadDataOrderLaboratorium()
            ElseIf tabOrderList.SelectedTabPageIndex = 2 Then
                'Radiologi
                fn_LoadDataOrderRadiologi()
            End If
        Catch ex As Exception

        End Try

    End Sub
    Private Sub btnSimpanCPPT_Click() Handles btnSimpanCPPT.Click
        Try
            If fn_ValidateCPPT(False) = False Then Exit Sub
            If MsgBox("Save CPPT Jam " & Now.ToString("HH:mm", CultureInfo.InvariantCulture) & "?", MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, Me.Text) = MsgBoxResult.No Then Exit Sub
            If fn_SaveCPPT(lblKODECPPT.Text, False, False) = False Then
                MsgBox("Save gagal! Tolong kontak admin anda!", MsgBoxStyle.Exclamation, Me.Text)
            Else
                'MsgBox("Save success!", MsgBoxStyle.Information, Me.Text)
                tabbedControlGroup1.SelectedTabPageIndex = 0
                tabbedControlGroup1_SelectedPageChanged()
            End If
        Catch ex As Exception

        End Try

    End Sub
    'Private Sub btnSimpanCPPT_Click(sender As Object, e As EventArgs) Handles btnSimpanCPPT.Click
    '    'lblKODECPPT
    'End Sub
    Private Sub btnSimpanCPPTPulang_Click(sender As Object, e As EventArgs) Handles btnSimpanCPPTPulang.Click
        Try
            If fn_ValidateCPPT(False) = False Then Exit Sub
            If MsgBox("Save CPPT Jam " & Now.ToString("HH:mm", CultureInfo.InvariantCulture) & "?", MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, Me.Text) = MsgBoxResult.No Then Exit Sub
            If fn_SaveCPPT(lblKODECPPT.Text, False, False) = False Then
                MsgBox("Save gagal! Tolong kontak admin anda!", MsgBoxStyle.Exclamation, Me.Text)
            Else
                XtraTabControl1.SelectedTabPageIndex = 2
                btnSimpanResume_Click()
            End If
        Catch ex As Exception

        End Try

    End Sub
    Private Sub btnCPPTGizi_Click() Handles btnCPPTGizi.Click
        Try
            If fn_ValidateCPPT(True) = False Then Exit Sub
            If MsgBox("Save CPPT Jam " & Now.ToString("HH:mm", CultureInfo.InvariantCulture) & "?", MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, Me.Text) = MsgBoxResult.No Then Exit Sub
            If fn_SaveCPPT(lblKODECPPT.Text, True, False) = False Then
                MsgBox("Save gagal! Tolong kontak admin anda!", MsgBoxStyle.Exclamation, Me.Text)
            Else
                'MsgBox("Save success!", MsgBoxStyle.Information, Me.Text)
                tabbedControlGroup1.SelectedTabPageIndex = 0
                tabbedControlGroup1_SelectedPageChanged()
            End If
        Catch ex As Exception

        End Try

    End Sub
    Private Sub fn_AksiGoruper(ByVal PESAN As Boolean, iscppt As Boolean)
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
            SQL &= "A.KDREG "
            SQL &= "FROM "
            SQL &= "S_PENDAFTARAN_H A "
            SQL &= "WHERE "
            SQL &= "A.KDREG = '" & lblRegister.Text & "' "
            SQL &= "AND A.ISUPDATEPULANG = 1 "

            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "CEKPULANG")

            If oConn.State = ConnectionState.Open Then
                oConn.Close()
            End If

            For xloop As Integer = 0 To ds.Tables("CEKPULANG").Rows.Count - 1
                If PESAN = True Then
                    MsgBox("Pasien Sudah Pulang tidak dapat di Grouper", MsgBoxStyle.Exclamation, Me.Text)
                End If
                Exit Sub
            Next

            '"123123123123"
            '3217061107840001
            If sISDOKTER = True Then
                If sKodeTarifEClaim <> "" Then
                    If sURL_Claim <> "" Then
                        If lblPenjamin.Text = "BPJS KESEHATAN" And lblNOMORSEP.Text <> "" Then
                            fn_NilaiGrouping(iscppt)

                            'Dim listDiagnosa As New List(Of String)
                            'Dim listProsedur As New List(Of String)

                            'If iscppt = False Then
                            '    For i As Integer = 0 To grvDIAGNOSA.RowCount - 2
                            '        If grvDIAGNOSA.GetRowCellValue(i, colKDDIAGNOSA) <> "" Then
                            '            listDiagnosa.Add(grvDIAGNOSA.GetRowCellValue(i, colKDDIAGNOSA))
                            '        End If
                            '    Next

                            '    For i As Integer = 0 To grvPROSEDUR.RowCount - 2
                            '        If grvPROSEDUR.GetRowCellValue(i, colKDPROSEDUR) <> "" Then
                            '            listProsedur.Add(grvPROSEDUR.GetRowCellValue(i, colKDPROSEDUR))
                            '        End If
                            '    Next
                            'Else
                            '    For i As Integer = 0 To grvDIAGNOSA_CPPT.RowCount - 2
                            '        If grvDIAGNOSA_CPPT.GetRowCellValue(i, colKDDIAGNOSA_CPPT) <> "" Then
                            '            listDiagnosa.Add(grvDIAGNOSA_CPPT.GetRowCellValue(i, colKDDIAGNOSA_CPPT))
                            '        End If
                            '    Next

                            '    For i As Integer = 0 To grvPROSEDUR_CPPT.RowCount - 2
                            '        If grvPROSEDUR_CPPT.GetRowCellValue(i, colKDPROSEDUR_CPPT) <> "" Then
                            '            listProsedur.Add(grvPROSEDUR_CPPT.GetRowCellValue(i, colKDPROSEDUR_CPPT))
                            '        End If
                            '    Next
                            'End If

                            'Dim dsUmpanBalik = oAdmisi.GetDataUmpanBalik(String.Join(";", listDiagnosa.ToArray) & String.Join("#", listProsedur.ToArray))

                            'If dsUmpanBalik IsNot Nothing Then
                            '    TextBox2.Text = dsUmpanBalik.INACBG & "-" & dsUmpanBalik.INACBG_DESKRIPSI

                            '    fn_SaveGrouper(IIf(dsUmpanBalik.BIAYA_DISETUJUI <> 0, dsUmpanBalik.BIAYA_DISETUJUI, dsUmpanBalik.BIAYA_INACBG), "", dsUmpanBalik.INACBG & "-" & dsUmpanBalik.INACBG_DESKRIPSI)

                            'End If
                        Else
                            MsgBox("Bukan Pasien BPJS", MsgBoxStyle.Exclamation, Me.Text)
                        End If
                    Else
                        MsgBox("Url Kosong", MsgBoxStyle.Exclamation, Me.Text)
                    End If
                Else
                    MsgBox("Kode Tarif Kosong", MsgBoxStyle.Exclamation, Me.Text)
                End If
            End If
        Catch oErr As Exception
            MsgBox("CEKPULANG : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub btnGrouperCPPT_Click(sender As Object, e As EventArgs) Handles btnGrouperCPPT.Click
        If sISDOKTER = False Then
            MsgBox("Silahkan Pilih Modul Dokter", MsgBoxStyle.Exclamation, Me.Text)
            Exit Sub
        End If

        fn_AksiGoruper(True, True)

        'If sKodeTarifEClaim <> "" Then
        '    If lblPenjamin.Text = "BPJS KESEHATAN" And lblNOMORSEP.Text <> "" Then
        '        If fn_Membuatklaimbaru() = True Then
        '            If fn_MengisiUpdateDataKlaimKlaim(sKodeTarifEClaim, "123123123123", True) = True Then
        '                fn_GroupingStage()
        '            End If
        '        End If
        '    Else
        '        MsgBox("Bukan Pasien BPJS Kesehatan", MsgBoxStyle.Exclamation, Me.Text)
        '    End If
        'Else
        '    MsgBox("Kode Tarif Kosong", MsgBoxStyle.Exclamation, Me.Text)
        'End If
    End Sub
    Private Sub btnGrouperResume_Click() Handles btnGrouperResume.Click
        'If MsgBox("Simpan Grouper?", MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, Me.Text) = MsgBoxResult.No Then Exit Sub
        If sISDOKTER = False Then
            MsgBox("Silahkan Pilih Modul Dokter", MsgBoxStyle.Exclamation, Me.Text)
            Exit Sub
        End If

        fn_AksiGoruper(True, False)

        'If sKodeTarifEClaim <> "" Then
        '    If lblPenjamin.Text = "BPJS KESEHATAN" And lblNOMORSEP.Text <> "" Then
        '        If fn_Membuatklaimbaru() = True Then
        '            If fn_MengisiUpdateDataKlaimKlaim(sKodeTarifEClaim, "123123123123", False) = True Then
        '                fn_GroupingStage()
        '            End If
        '        End If
        '    Else
        '        MsgBox("Bukan Pasien BPJS Kesehatan", MsgBoxStyle.Exclamation, Me.Text)
        '    End If
        'Else
        '    MsgBox("Kode Tarif Kosong", MsgBoxStyle.Exclamation, Me.Text)
        'End If
    End Sub
    Private Sub btnSimpanResume_Click() Handles btnSimpanResume.Click
        If fn_ValidateRINGKASANKELUAR(False) = False Then Exit Sub
        'If MsgBox("Save?", MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, Me.Text) = MsgBoxResult.No Then Exit Sub
        If fn_SaveRINGKASANKELUAR() = False Then
            MsgBox("Save gagal! Tolong kontak admin anda!", MsgBoxStyle.Exclamation, Me.Text)
        Else
            tabbedControlGroup1.SelectedTabPageIndex = 2
            tabbedControlGroup1_SelectedPageChanged()

            fn_LoadUpdateSudah(lblRegister.Text, "1", "RESUME")
            fn_AksiGoruper(False, False)
        End If
    End Sub
    Private Sub btnFinishResume_Click() Handles btnFinishResume.Click
        If sISDOKTER = False Then
            MsgBox("Silahkan Pilih Modul Dokter", MsgBoxStyle.Exclamation, Me.Text)
            Exit Sub
        End If
        If fn_ValidateRINGKASANKELUAR(True) = False Then Exit Sub
        'If MsgBox("Save?", MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, Me.Text) = MsgBoxResult.No Then Exit Sub
        If fn_SaveRINGKASANKELUAR() = False Then
            MsgBox("Save gagal! Tolong kontak admin anda!", MsgBoxStyle.Exclamation, Me.Text)
        Else
            fn_SaveVerifikasi()

            tabbedControlGroup1.SelectedTabPageIndex = 2
            tabbedControlGroup1_SelectedPageChanged()

            fn_LoadUpdateSudah(lblRegister.Text, "1", "FINISH RESUME")
            fn_AksiGoruper(False, False)
        End If
    End Sub
    Private Sub fn_SaveVerifikasi()
        Try
            Dim oVerifikasi As New Inventory.clsReqCPPTACCPerawat
            For Each xloop In oCPPT.GetDataByRegister(lblRegister.Text)
                Dim dsverifikasi = oVerifikasi.GetData(xloop.KDCPPT)

                If dsverifikasi Is Nothing Then
                    ' ***** HEADER *****
                    Dim ds = oVerifikasi.GetStructureHeader
                    With ds
                        .DATECREATED = Now
                        .DATEUPDATE = Now
                        .KDACC = xloop.KDCPPT
                        .KDCPPT = xloop.KDCPPT
                        .CATEGORY = 0
                        .STATUS = sUserID
                        .KDUNIT = "======VERIFIKASI======"
                        .DATE = Now
                        .DESCRIPTION = sDOKTERUTAMA
                    End With

                    Dim dsCekCPPT = oVerifikasi.GetDataKDCPPT(ds.KDCPPT)

                    oVerifikasi.InsertData(ds)

                    oCPPT.UpdatenikSEBAGAIVERIFIKASI(ds.KDCPPT, sUserID)
                End If
            Next
        Catch oErr As Exception
            MsgBox("Save Verifikasi" & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadUpdateSudah(ByVal kdreg As String, ByVal ISRESUME As String, ByVal keterangan As String)
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
            SQL &= "SET ISRESUME = " & ISRESUME & " "
            SQL &= ",KETERANGAN_TINDAKLANJUT = '" & keterangan & "' "
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
    Private Sub btnKonsultasi_Click() Handles btnKonsultasi.Click
        Dim frmKonsulDokter As New frmKonsulDokter
        Try
            frmKonsulDokter.fn_LoadMe(lblRegister.Text, sKDDOCTOR_INPUT, "", "", "", "", "", "")
            frmKonsulDokter.ShowDialog(Me)
        Catch ex As Exception
            MsgBox("Load Form Detail : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
        Finally
            If Not frmKonsulDokter Is Nothing Then frmKonsulDokter.Dispose()
            frmKonsulDokter = Nothing

            If sKONSULTASI <> "" Then
                txtINTRUKSILAIN.Text = txtINTRUKSILAIN.Text & vbCrLf & "Konsultasi Ke Dokter" & sKONSULTASI

                'If lblNoCPPT.Text <> "" Then
                '    btnSimpanCPPT_Click()
                'End If
            End If
        End Try
    End Sub
    Private Sub grvRadiologi_FocusedRowChanged(ByVal sender As System.Object, ByVal e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs) Handles grvRadiologi.FocusedRowChanged
        'Try
        '    grvFoto.OptionsSelection.MultiSelect = True
        '    grvFoto.SelectAll()
        '    grvFoto.DeleteSelectedRows()
        '    grvFoto.OptionsSelection.MultiSelect = False

        '    If grvRadiologi.GetFocusedRowCellValue("NoRadiologi") Is Nothing Then
        '        Exit Sub
        '    End If

        '    '"R23.07694"
        '    Dim ds = oBrigging.GetDataRadiologi(grvRadiologi.GetFocusedRowCellValue("NoRadiologi"), oBrigging.GetDataAktive().CONSID_APLICARE)

        '    If ds <> "" Then
        '        Dim allData = JObject.Parse(ds)

        '        Dim CodeResponse As String = IIf(IsDBNull(allData.Item("metaData").Item("code")) = True, "", allData.Item("metaData").Item("code"))
        '        Dim messageResponse As String = allData("metaData")("message").ToString
        '        If CodeResponse = "200" Then
        '            'LabelControl2.Text = allData("response")("Status").ToString()

        '            Try
        '                'Gambar
        '                Dim table As DataTable

        '                table = New DataTable("I_REPOT_REKAP")
        '                table.Columns.Add("Keterangan")
        '                table.Columns.Add("Alamat")

        '                Dim i As Integer = 0
        '                Dim Nomor As Integer = 1

        '                Dim tes = allData("response")("image")

        '                For Each item In allData("response")("image")
        '                    table.Rows.Add(New String() {"Foto " & Nomor, item})
        '                    Nomor += 1
        '                    i += 1
        '                Next

        '                grdFoto.DataSource = table
        '                grdFoto.ForceInitialize()

        '                grvFoto.Columns("Alamat").VisibleIndex = -1
        '            Catch ex As Exception
        '                MsgBox("Gambar : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
        '            End Try

        '            Try
        '                'URL
        '                txtURL.Text = allData("response")("LinkViewer").ToString()
        '            Catch ex As Exception
        '                MsgBox("URL : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
        '            End Try

        '            Try
        '                RichTextBox1.Rtf = RenderHTML(allData("response")("Expertise").ToString())
        '            Catch ex As Exception
        '                MsgBox("RTF : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
        '            End Try
        '        Else
        '            MsgBox(CodeResponse & "-" & messageResponse, MsgBoxStyle.Information, Me.Text)
        '        End If
        '    Else
        '        txtURL.ResetText()
        '        RichTextBox1.ResetText()
        '    End If
        'Catch oErr As Exception
        '    MsgBox("Browser Data : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        'End Try

        Try
            If grvRadiologi.GetFocusedRowCellValue("NoRadiologi") Is Nothing Then
                Exit Sub
            End If
            If grvRadiologi.GetFocusedRowCellValue("NoRadiologi") = "" Then
                Exit Sub
            End If

            PdfViewerRadiologi.CloseDocument()
            HasilBriggingRadiologi(False, grvRadiologi.GetFocusedRowCellValue("NoRadiologi"), grvRadiologi.GetFocusedRowCellValue("Catatan"), CDate(grvRadiologi.GetFocusedRowCellValue("Tanggal")).ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture), grvRadiologi.GetFocusedRowCellValue("DOKTER"), True)
        Catch ex As Exception

        End Try
    End Sub
    'Private Sub grvFoto_FocusedRowChanged(ByVal sender As System.Object, ByVal e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs)
    '    PictureEdit1.Image = Nothing

    '    If grvFoto.GetFocusedRowCellValue("Alamat") Is Nothing Then
    '        Exit Sub
    '    End If
    '    If grvFoto.GetFocusedRowCellValue("Alamat") <> "" Then
    '        Dim tClient As WebClient = New WebClient
    '        Dim url As String = grvFoto.GetFocusedRowCellValue("Alamat")
    '        Dim tImage As Bitmap = Bitmap.FromStream(New MemoryStream(tClient.DownloadData(url)))

    '        PictureEdit1.Image = tImage
    '    Else
    '        PictureEdit1.Image = Nothing
    '    End If
    'End Sub
    'Private Sub SimpleButton1_Click(sender As Object, e As EventArgs)
    '    If txtURL.Text <> "" Then
    '        Dim variabel As String = "CMD /c Start chrome /profile-directory=""Default"" """ & txtURL.Text & """"
    '        Shell(variabel, vbNormalFocus)
    '    End If
    'End Sub
    Private Sub grvRadiologi_DoubleClick(sender As Object, e As EventArgs) Handles grvRadiologi.DoubleClick
        Try
            If grvRadiologi.GetFocusedRowCellValue("NoRadiologi") Is Nothing Then
                Exit Sub
            End If
            If grvRadiologi.GetFocusedRowCellValue("NoRadiologi") = "" Then
                Exit Sub
            End If

            PdfViewerRadiologi.CloseDocument()
            HasilBriggingRadiologi(True, grvRadiologi.GetFocusedRowCellValue("NoRadiologi"), grvRadiologi.GetFocusedRowCellValue("Catatan"), CDate(grvRadiologi.GetFocusedRowCellValue("Tanggal")).ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture), grvRadiologi.GetFocusedRowCellValue("DOKTER"), True)
        Catch ex As Exception

        End Try

    End Sub
    Public Sub HasilBriggingRadiologi(ByVal bukaurl As Boolean, ByVal KodeRad As String, ByVal catatan As String, ByVal TANGGAL_INPUT As String, ByVal DOKTERPENGIRIM As String, ByVal isBukan As Boolean)
        Try
            '"R23.07694"
            Dim ds = oBrigging.GetDataRadiologi(KodeRad, sAlamatBriggingRadiologi)

            sNOMORUSG_RAD = String.Empty
            sPEMERIKSAAN_RAD = String.Empty
            sUMUR_RAD = String.Empty
            sKDCUSTOMER_RAD = String.Empty
            sNAME_DISPLAY_RAD = String.Empty
            sDATE_RAD = Now
            sDEPARTMENT_RAD = String.Empty
            sREPORTDATE_RAD = Now
            sEXAMPDESC_RAD = String.Empty
            sDOKTER_RAD = String.Empty
            sDIAGNOSA_RAD = String.Empty
            sDESCRIPTION_RAD = String.Empty
            sDOKTER_RAD2 = String.Empty
            sHasilLoadRadiologi = String.Empty

            If ds <> "" Then
                Dim allData = JObject.Parse(ds)

                Dim CodeResponse As String = String.Empty
                Dim messageResponse As String = String.Empty

                Try
                    CodeResponse = IIf(IsDBNull(allData.Item("metaData")("code")) = True, "", allData.Item("metaData")("code"))
                    messageResponse = allData("metaData")("message").ToString
                Catch ex As Exception
                    CodeResponse = IIf(IsDBNull(allData.Item("status")) = True, "", allData.Item("status"))
                    messageResponse = allData("messages")("error").ToString
                End Try

                If CodeResponse = "200" Then
                    If bukaurl = True Then
                        Try
                            'URL
                            Dim variabel As String = "CMD /c Start chrome /profile-directory=""Default"" """ & allData("response")("LinkViewer").ToString() & """"
                            Shell(variabel, vbNormalFocus)
                        Catch ex As Exception
                            MsgBox("URL : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
                        End Try
                    End If

                    If isBukan = True Then
                        Try
                            RichTextBox1.Rtf = RenderHTML(allData("response")("Expertise").ToString())
                        Catch ex As Exception
                            MsgBox("RTF : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
                        End Try
                    Else
                        RichTextBox1.Rtf = RenderHTML(allData("response")("Expertise").ToString())
                        sHasilLoadRadiologi = RichTextBox1.Rtf
                    End If

                    If catatan.ToString.Contains("USG") = True Then
                        Try
                            Dim rpt As New xtraUSG

                            sNOMORUSG_RAD = KodeRad
                            sUMUR_RAD = sUMURPASIEN
                            sKDCUSTOMER_RAD = lblNoRM.Text
                            sNAME_DISPLAY_RAD = lblNamaPasien.Text
                            sDATE_RAD = TANGGAL_INPUT
                            sDEPARTMENT_RAD = lblRuangan.Text
                            sREPORTDATE_RAD = allData("response")("StudyDate").ToString()
                            sEXAMPDESC_RAD = ""
                            'sDOKTER_RAD = allData("response")("ReferringDoctor").ToString()
                            sDOKTER_RAD = DOKTERPENGIRIM
                            sDIAGNOSA_RAD = allData("response")("Diagnose").ToString()
                            sDESCRIPTION_RAD = RichTextBox1.Text
                            sDOKTER_RAD2 = allData("response")("Radiologist").ToString()

                            Try
                                sPEMERIKSAAN_RAD = allData("response")("BodyPart").ToString()
                            Catch ex As Exception

                            End Try

                            rpt.ExportToPdf("C:/HASILRADIOLGI/" & KodeRad & ".pdf")
                            If isBukan = True Then
                                PdfViewerRadiologi.LoadDocument("C:/HASILRADIOLGI/" & KodeRad & ".pdf")
                            End If
                        Catch oErr As Exception
                            MsgBox("Print Data USG: " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
                        End Try
                    Else
                        Try
                            Dim rpt As New xtraRadiologi

                            sKDCUSTOMER_RAD = lblNoRM.Text
                            sNAME_DISPLAY_RAD = lblNamaPasien.Text
                            sDATE_RAD = TANGGAL_INPUT
                            sDEPARTMENT_RAD = lblRuangan.Text
                            sREPORTDATE_RAD = allData("response")("StudyDate").ToString()
                            sEXAMPDESC_RAD = ""
                            'sDOKTER_RAD = allData("response")("ReferringDoctor").ToString()
                            sDOKTER_RAD = DOKTERPENGIRIM
                            sDIAGNOSA_RAD = allData("response")("Diagnose").ToString()
                            sDESCRIPTION_RAD = RichTextBox1.Text
                            sDOKTER_RAD2 = allData("response")("Radiologist").ToString()

                            Try
                                sPEMERIKSAAN_RAD = allData("response")("BodyPart").ToString()
                            Catch ex As Exception

                            End Try

                            rpt.ExportToPdf("C:/HASILRADIOLGI/" & KodeRad & ".pdf")
                            If isBukan = True Then
                                PdfViewerRadiologi.LoadDocument("C:/HASILRADIOLGI/" & KodeRad & ".pdf")
                            End If
                        Catch oErr As Exception
                            MsgBox("Print Data Rad: " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
                        End Try
                    End If
                Else
                    MsgBox(CodeResponse & "-" & messageResponse, MsgBoxStyle.Information, Me.Text)
                End If
            Else
                MsgBox("Url Tidak ditemukan", MsgBoxStyle.Exclamation, Me.Text)
            End If
        Catch oErr As Exception
            MsgBox("Brigging Radiologi Data : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub btnURL_Click(sender As Object, e As EventArgs) Handles btnURL.Click

    End Sub
    Private Sub grvResume_FocusedRowChanged(ByVal sender As System.Object, ByVal e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs) Handles grvResume.FocusedRowChanged
        If grvResume.GetFocusedRowCellValue("KODE") Is Nothing Then
            Exit Sub
        End If

        Try
            fn_PrintStrukResumeRawatJalanRawatInap(grvResume.GetFocusedRowCellValue("KODE"), grvResume.GetFocusedRowCellValue("KATEGORI"))
        Catch ex As Exception

        End Try

    End Sub
    Private Sub grvLaboratorium_FocusedRowChanged(ByVal sender As System.Object, ByVal e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs) Handles grvLaboratorium.FocusedRowChanged
        If grvLaboratorium.GetFocusedRowCellValue("KODE") Is Nothing Then
            Exit Sub
        End If

        Try
            fn_SearchFolderPdf(grvLaboratorium.GetFocusedRowCellValue("KODE"), grvLaboratorium.GetFocusedRowCellValue("KDCUSTOMER"), grvLaboratorium.GetFocusedRowCellValue("NAME_DISPLAY"))
        Catch ex As Exception

        End Try
    End Sub
    Private Sub grvAsesmenAwalMedisList_FocusedRowChanged(ByVal sender As System.Object, ByVal e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs) Handles grvAsesmenAwalMedisList.FocusedRowChanged
        If grvAsesmenAwalMedisList.GetFocusedRowCellValue("Kode") Is Nothing Then
            Exit Sub
        End If

        Try
            fn_PrintStrukAsesemenMedis(grvAsesmenAwalMedisList.GetFocusedRowCellValue("FORMULIR"), grvAsesmenAwalMedisList.GetFocusedRowCellValue("Kode"))
        Catch ex As Exception

        End Try
    End Sub
    Private Sub ToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem1.Click
        grvDIAGNOSA.DeleteSelectedRows()
    End Sub
    Private Sub ToolStripMenuItem3_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem3.Click
        grvPROSEDUR.DeleteSelectedRows()
    End Sub
    Private Sub ToolStripMenuItem2_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem2.Click
        grvDIAGNOSA_CPPT.DeleteSelectedRows()
    End Sub
    Private Sub ToolStripMenuItem5_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem5.Click
        grvPROSEDUR_CPPT.DeleteSelectedRows()
    End Sub
    Private Sub TextBox9_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox9.KeyPress
        If Asc(e.KeyChar) = 13 Then
            btnCari_Click()
        End If
    End Sub
    Private Sub txtCARIDIAGNOSA_CPPT_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtCARIDIAGNOSA_CPPT.KeyPress
        If Asc(e.KeyChar) = 13 Then
            If sISDOKTER = True Then
                Try
                    Dim jsonDecode = JObject.Parse(oBrigging.fn_Pencariandiagnosa(txtCARIDIAGNOSA_CPPT.Text))
                    Dim sDataDuplicate As String = String.Empty
                    Dim smessage As String = String.Empty

                    sDataDuplicate = jsonDecode("metadata")("code").ToString
                    smessage = jsonDecode("metadata")("message").ToString

                    If sDataDuplicate = "200" Then
                        Dim table As DataTable

                        table = New DataTable("M_DIAGNOSA")
                        table.Columns.Add("nama")
                        table.Columns.Add("kode")

                        For Each item In jsonDecode("response")("data")
                            table.Rows.Add(New String() {item(0), item(1)})
                        Next

                        grdCARIDIAGNOSA_CPPT.Properties.DataSource = table
                        grdCARIDIAGNOSA_CPPT.Properties.ValueMember = "kode"
                        grdCARIDIAGNOSA_CPPT.Properties.DisplayMember = "nama"

                        GridView11.BestFitColumns()

                        grdCARIDIAGNOSA_CPPT.ShowPopup()

                        txtCARIDIAGNOSA_CPPT.ResetText()
                    Else
                        MsgBox("Pencarian Diagnosa" & vbCrLf & sDataDuplicate & "-" & smessage, MsgBoxStyle.Information, Me.Text)
                    End If
                Catch oErr As Exception
                    MsgBox("Pencarian Diagnosa: " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            Else
                Dim oItemDiagnosaPerawat As New Master.clsItemDiagnosaPerawat

                Try
                    Dim dsList = From x In oItemDiagnosaPerawat.GetDataDetailByName(txtCARIDIAGNOSA_CPPT.Text)
                                 Select nama = x.DESCRIPTION, kode = x.KDITEMDIAGNOSAPERAWAT

                    grdCARIDIAGNOSA_CPPT.Properties.DataSource = dsList.ToList()
                    grdCARIDIAGNOSA_CPPT.Properties.ValueMember = "kode"
                    grdCARIDIAGNOSA_CPPT.Properties.DisplayMember = "nama"

                    GridView11.BestFitColumns()

                    grdCARIDIAGNOSA_CPPT.ShowPopup()

                    txtCARIDIAGNOSA_CPPT.ResetText()
                Catch oErr As Exception
                    MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            End If
        End If
    End Sub
    Private Sub txtCARIPROSEDUR_CPPT_CPPT_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtCARIPROSEDUR_CPPT.KeyPress
        If Asc(e.KeyChar) = 13 Then
            If sISDOKTER = True Then
                Try
                    Dim jsonDecode = JObject.Parse(oBrigging.fn_PencarianProsedur("VCLAIM", txtCARIPROSEDUR_CPPT.Text))
                    Dim sDataDuplicate As String = String.Empty
                    Dim smessage As String = String.Empty

                    sDataDuplicate = jsonDecode("metadata")("code").ToString
                    smessage = jsonDecode("metadata")("message").ToString

                    If sDataDuplicate = "200" Then
                        Dim table As DataTable

                        table = New DataTable("M_PROSEDUR")
                        table.Columns.Add("nama")
                        table.Columns.Add("kode")

                        For Each item In jsonDecode("response")("data")
                            table.Rows.Add(New String() {item(0), item(1)})
                        Next

                        grdCARIPROSEDUR_CPPT.Properties.DataSource = table
                        grdCARIPROSEDUR_CPPT.Properties.ValueMember = "kode"
                        grdCARIPROSEDUR_CPPT.Properties.DisplayMember = "nama"

                        GridView12.BestFitColumns()

                        grdCARIPROSEDUR_CPPT.ShowPopup()

                        txtCARIPROSEDUR_CPPT.ResetText()
                    Else
                        MsgBox("Pencarian Prosedur" & vbCrLf & sDataDuplicate & "-" & smessage, MsgBoxStyle.Information, Me.Text)
                    End If
                Catch oErr As Exception
                    MsgBox("Pencarian Prosedur: " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            Else

            End If
        End If
    End Sub
    Private Sub grdCARIDIAGNOSA_CPPT_EditValueChanged(sender As Object, e As EventArgs) Handles grdCARIDIAGNOSA_CPPT.EditValueChanged
        If grdCARIDIAGNOSA_CPPT.Text <> "" Then
            Dim sCek As Integer = 0

            For i As Integer = 0 To grvDIAGNOSA_CPPT.RowCount - 2
                sCek += 1
            Next

            grvDIAGNOSA_CPPT.Focus()
            grvDIAGNOSA_CPPT.AddNewRow()
            'grvDIAGNOSA_CPPT.SetFocusedRowCellValue(colKATEGORI_CPPT, IIf(sCek = 0, "Primer", "Sekunder"))
            grvDIAGNOSA_CPPT.SetFocusedRowCellValue(colKDDIAGNOSA_CPPT, grdCARIDIAGNOSA_CPPT.EditValue)
            grvDIAGNOSA_CPPT.SetFocusedRowCellValue(colNAMADIAGNOSA_CPPT, grdCARIDIAGNOSA_CPPT.Text)
            grvDIAGNOSA_CPPT.UpdateCurrentRow()

            txtCARIDIAGNOSA_CPPT.Focus()
        End If
    End Sub
    Private Sub grvDIAGNOSA_CPPT_CellValueChanged(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs) Handles grvDIAGNOSA_CPPT.CellValueChanged
        If e.Column.Name = colNAMADIAGNOSA_CPPT.Name Then
            Dim CEK As Boolean = False

            For i As Integer = 0 To grvDIAGNOSA_CPPT.RowCount - 2
                If grvDIAGNOSA_CPPT.GetRowCellValue(i, colKATEGORI_CPPT) = "Primer" Then
                    CEK = True
                End If
            Next

            If CEK = False Then
                grvDIAGNOSA_CPPT.SetFocusedRowCellValue(colKATEGORI_CPPT, "Primer")
            Else
                grvDIAGNOSA_CPPT.SetFocusedRowCellValue(colKATEGORI_CPPT, "Sekunder")
            End If
        End If
    End Sub
    Private Sub grdCARIPROSEDUR_CPPT_EditValueChanged(sender As Object, e As EventArgs) Handles grdCARIPROSEDUR_CPPT.EditValueChanged
        If grdCARIPROSEDUR_CPPT.Text <> "" Then
            grvPROSEDUR_CPPT.Focus()
            grvPROSEDUR_CPPT.AddNewRow()
            grvPROSEDUR_CPPT.SetFocusedRowCellValue(colKDPROSEDUR_CPPT, grdCARIPROSEDUR_CPPT.EditValue)
            grvPROSEDUR_CPPT.SetFocusedRowCellValue(colNAMAPROSEDUR_CPPT, grdCARIPROSEDUR_CPPT.Text)
            grvPROSEDUR_CPPT.UpdateCurrentRow()

            txtCARIPROSEDUR_CPPT.Focus()
        End If
    End Sub
    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        btnCari_Click()
    End Sub
    Private Sub GridLookUpEdit1_EditValueChanged(sender As Object, e As EventArgs) Handles GridLookUpEdit1.EditValueChanged
        If GridLookUpEdit1.Text <> "" Then
            Dim sCek As Integer = 0

            For i As Integer = 0 To grvDIAGNOSA.RowCount - 2
                sCek += 1
            Next

            grvDIAGNOSA.Focus()
            grvDIAGNOSA.AddNewRow()
            grvDIAGNOSA.SetFocusedRowCellValue(colKATEGORI, IIf(sCek = 0, "Primer", "Sekunder"))
            grvDIAGNOSA.SetFocusedRowCellValue(colKDDIAGNOSA, GridLookUpEdit1.EditValue)
            grvDIAGNOSA.SetFocusedRowCellValue(colNAMADIAGNOSA, GridLookUpEdit1.Text)
            grvDIAGNOSA.UpdateCurrentRow()
        End If
    End Sub
    Private Sub txtCARI_PROSEDUR_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox10.KeyPress
        If Asc(e.KeyChar) = 13 Then
            btnCariProsedur_Click()
        End If
    End Sub
    Private Sub Button9_Click(sender As Object, e As EventArgs) Handles Button9.Click
        btnCariProsedur_Click()
    End Sub
    Private Sub txtOBJEKTIF_BERATBADAN_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtOBJEKTIF_BERATBADAN.KeyPress
        If Asc(e.KeyChar) = 13 Then
            txtOBJEKTIF_TINGGIBADAN.Focus()
        End If
    End Sub
    Private Sub txtOBJEKTIF_TINGGIBADAN_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtOBJEKTIF_TINGGIBADAN.KeyPress
        If Asc(e.KeyChar) = 13 Then
            txtOBJEKTIF_TEKANANDARAH.Focus()
        End If
    End Sub
    Private Sub txtOBJEKTIF_TEKANANDARAH_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtOBJEKTIF_TEKANANDARAH.KeyPress
        If Asc(e.KeyChar) = 13 Then
            txtOBJEKTIF_NADI.Focus()
        End If
    End Sub
    Private Sub txtOBJEKTIF_NADI_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtOBJEKTIF_NADI.KeyPress
        If Asc(e.KeyChar) = 13 Then
            txtOBJEKTIF_RESPIRASI.Focus()
        End If
    End Sub
    Private Sub txtOBJEKTIF_RESPIRASI_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtOBJEKTIF_RESPIRASI.KeyPress
        If Asc(e.KeyChar) = 13 Then
            txtOBJEKTIF_SATURASIOKSIGEN.Focus()
        End If
    End Sub
    Private Sub txtOBJEKTIF_SATURASIOKSIGEN_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtOBJEKTIF_SATURASIOKSIGEN.KeyPress
        If Asc(e.KeyChar) = 13 Then
            txtOBJEKTIF_SUHU.Focus()
        End If
    End Sub
    Private Sub txtOBJEKTIF_SUHU_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtOBJEKTIF_SUHU.KeyPress
        If Asc(e.KeyChar) = 13 Then
            txtPEMERIKSAANLAIN.Focus()
        End If
    End Sub
    Private Sub grvUpload_FocusedRowChanged(ByVal sender As System.Object, ByVal e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs) Handles grvUpload.FocusedRowChanged
        If grvUpload.GetFocusedRowCellValue("AlamatUpload") Is Nothing Then
            Exit Sub
        End If

        Try
            PdfViewerUpload.CloseDocument()

            If grvUpload.GetFocusedRowCellValue("AlamatUpload") = "" Then Exit Sub

            PdfViewerUpload.LoadDocument(grvUpload.GetFocusedRowCellValue("AlamatUpload"))
        Catch oErr As Exception
            MsgBox("Load data Upload: " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try

    End Sub
#End Region
#Region "CPPT"
    Private Sub fn_ChangeFormStateCPPT()
        If isLoad = True Then Exit Sub

        If sISDOKTER = True Then
            fn_LoadITEM()
            fn_LoadUOM()
            fn_LoadSIGNA()
            fn_LoadCARAPAKAI()
        End If

        If sISDOKTER = False Then
            cboProfesi.Properties.Items.Add("PERAWAT")
            cboProfesi.Properties.Items.Add("BIDAN")
            'cboProfesi.Properties.Items.Add("GIZI")
            cboProfesi.Properties.Items.Add("FISIOTERAPI")
            cboProfesi.Properties.Items.Add("APOTEKER")
            cboProfesi.Properties.Items.Add("ANALIS")
            cboProfesi.Properties.Items.Add("RADIOGRAFER")
            cboProfesi.Properties.Items.Add("PETUGAS KHUSUS LAINNYA")
            cboProfesi.Properties.Items.Add("PSIKOLOG")
            cboProfesi.Properties.Items.Add("REFRAKSIONIS OPTISIEN")
            cboProfesi.Properties.Items.Add("TERAPI WICARA")
            cboProfesi.Properties.Items.Add("DOKTER RADIOLOGI")
            cboProfesi.Properties.Items.Add("PERAWAT RADIOLOGI")
            cboProfesi.Properties.Items.Add("TERAPIS GIGI DAN MULUT")
        Else
            cboProfesi.Properties.Items.Add("DOKTER")
        End If

        fn_ViewModeCPPT(False)
        fn_EmptyMeCPPT()
        fn_LoadDataGrouper()

        'fn_LoadResepPerawat()

        'If lblNoCPPT.Text = "" Then
        '    fn_ViewModeCPPT(False)
        '    fn_EmptyMeCPPT()
        'Else
        '    fn_ViewModeCPPT(False)
        '    fn_LoadDataCPPT()
        'End If
    End Sub
    Private Sub fn_ViewModeCPPT(ByVal Status As Boolean)
        Try
            txtSUBJEKTIF.Properties.ReadOnly = Status
            chkALERGI_TIDAK.Properties.ReadOnly = Status
            chkALERGI_YA.Properties.ReadOnly = Status
            txtALERGI_TEXT.Properties.ReadOnly = Status
            txtOBJEKTIF_BERATBADAN.Properties.ReadOnly = Status
            txtOBJEKTIF_NADI.Properties.ReadOnly = Status
            txtOBJEKTIF_RESPIRASI.Properties.ReadOnly = Status
            txtOBJEKTIF_SATURASIOKSIGEN.Properties.ReadOnly = Status
            txtOBJEKTIF_SUHU.Properties.ReadOnly = Status
            txtOBJEKTIF_TEKANANDARAH.Properties.ReadOnly = Status
            txtOBJEKTIF_TINGGIBADAN.Properties.ReadOnly = Status
            txtPEMERIKSAANLAIN.Properties.ReadOnly = Status
            'txtDIAGNOOSA.Properties.ReadOnly = Status
            txtINTRUKSILAIN.Properties.ReadOnly = Status
            txtINTRUKSILAIN_OBATPULANG.Properties.ReadOnly = Status
            txtINTRUKSILAIN_EDUKASI.Properties.ReadOnly = Status
            'txtINTRUKSIORDERCPPT.Properties.ReadOnly = Status
            txtPlanning.Properties.ReadOnly = Status
            'txtPlanningObat.Properties.ReadOnly = Status

            If sISDOKTER = False Then
                cboProfesi.Properties.ReadOnly = False
                'lProfesi.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                'lProfesiName.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                'lCARIDIAGNOSA_CPPT.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                'lGRDCARIDIAGNOSA_CPPT.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                'lGRDDIAGNOSA_CPPT.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                'lCARIPROSEDUR_CPPT.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                'lGRDCARIPROSEDUR_CPPT.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                'lGRDPROSEDUR_CPPT.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                'chkALERGI_TIDAK.Properties.ReadOnly = True
                'chkALERGI_YA.Properties.ReadOnly = True
                'txtALERGI_TEXT.Properties.ReadOnly = True
                'txtPlanningObat.Properties.ReadOnly = True
                grdDetailResep.Enabled = False
                btnPasienPulang.Enabled = False
            Else
                grdDetailResep.Enabled = True
                cboProfesi.Properties.ReadOnly = True
                btnPasienPulang.Enabled = True
                'lProfesi.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                'lProfesiName.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never

                'lCARIDIAGNOSA_CPPT.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                'lGRDCARIDIAGNOSA_CPPT.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                'lGRDDIAGNOSA_CPPT.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                'lCARIPROSEDUR_CPPT.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                'lGRDCARIPROSEDUR_CPPT.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                'lGRDPROSEDUR_CPPT.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            End If
            'cboProfesi.Properties.ReadOnly = Status
        Catch ex As Exception

        End Try

    End Sub
    Private Sub fn_EmptyMeCPPT()
        txtSUBJEKTIF.ResetText()
        txtALERGI_TEXT.ResetText()
        txtOBJEKTIF_BERATBADAN.Text = "0"
        txtOBJEKTIF_NADI.ResetText()
        txtOBJEKTIF_RESPIRASI.ResetText()
        txtOBJEKTIF_SATURASIOKSIGEN.ResetText()
        txtOBJEKTIF_SUHU.ResetText()
        txtOBJEKTIF_TEKANANDARAH.ResetText()
        txtOBJEKTIF_TINGGIBADAN.ResetText()
        txtPEMERIKSAANLAIN.ResetText()
        'txtDIAGNOOSA.ResetText()
        txtINTRUKSILAIN.ResetText()
        txtINTRUKSILAIN_OBATPULANG.ResetText()
        txtINTRUKSILAIN_EDUKASI.ResetText()
        'txtINTRUKSIORDERCPPT.ResetText()
        txtPlanning.ResetText()
        'txtPlanningObat.ResetText()
        If isLoad = False Then
            lblKODECPPT.Text = "TAMBAH CPPT/SBAR"
        End If

        btnSBAR.Text = "TAMBAH SBAR"

        cboProfesi.SelectedIndex = 0

        If sISDOKTER = False Then
            If sCOPYKDCPPTPERAWAT = "" Then
                fn_LoadAsesmenAwalMedisIGDdiCPPT(sKDREGRAWATJALAN)
            Else
                lblCOPY.Text = "Copy CPPT dari Perawat"
                fn_CopyCPPTAkhirdiCPPT(sCOPYKDCPPTPERAWAT)
            End If
        Else
            If sCOPYCPPTDOKTERTERAKHIR = "" Then
                fn_LoadAsemenMedisdiCPPT(lblRegister.Text)
            Else
                lblCOPY.Text = "Copy CPPT dari Dokter"
                fn_CopyCPPTAkhirdiCPPT(sCOPYCPPTDOKTERTERAKHIR)
            End If

            If sCOPYKDCPPTPERAWAT <> "" Then
                fn_CopyCPPTAkhirdiCPPTTTVPerawat(sCOPYKDCPPTPERAWAT)
            End If


            If fn_LoadResepBykddoctor(lblRegister.Text, sKDDOCTOR_INPUT, False, True) = "" Then
                If fn_LoadResepBykddoctor(lblRegister.Text, sKDDOCTOR_DPJPUTAMA, False, True) = "" Then

                End If
            End If
        End If

        CheckEdit39.Checked = True

        'Dim list As New List(Of String)
        'For Each xloop In oReqRecipeRawatInap.GetDataDetailByHariIni(lblRegister.Text, Now.ToString("ddMMyyyy"))
        '    list.Add(xloop.NAMAOBAT & " No " & xloop.ROMAWI & " " & xloop.SATUAN & " " & xloop.SIGNA & " " & xloop.CARAPAKAI & " " & xloop.REMARKS_DOKTER)
        'Next

        'txtPlanningObat.Text = String.Join(vbCrLf, list.ToArray)
    End Sub
    Private Function fn_LoadResepBykddoctor(ByVal register As String, ByVal kddoctor As String, ByVal ResepPulang As Boolean, ByVal icppt As Boolean) As String
        Try
            fn_LoadResepBykddoctor = ""

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
            SQL &= "B.* "
            SQL &= "FROM "
            SQL &= "S_REQ_RECIPE_RI_H A "
            SQL &= "INNER JOIN S_REQ_RECIPE_RI_D B "
            SQL &= "ON A.KDREQRECIPE_RI = B.KDREQRECIPE_RI "
            SQL &= "INNER JOIN BUDIJAYAGROUP_RSKC..M_ITEM C "
            SQL &= "ON B.KDITEM = C.KDITEM "
            SQL &= "WHERE "
            If ResepPulang = True Then
                If icppt = True Then
                    'CPPT
                    SQL &= "A.KDPENDAFTARAN = '" & register & "' "
                    SQL &= "AND A.ISPULANG = 1 "
                    SQL &= "AND ALAMAT = '" & kddoctor & "' "
                    SQL &= "AND C.KDKATEGORI_BPJS NOT IN ('15','16', '17') "
                Else
                    'RESUME
                    SQL &= "A.KDPENDAFTARAN = '" & register & "' "
                    SQL &= "AND A.ISPULANG = 1 "
                    SQL &= "AND C.KDKATEGORI_BPJS NOT IN ('15','16', '17') "
                End If
            Else
                SQL &= "A.KDPENDAFTARAN = '" & register & "' "
                SQL &= "AND ALAMAT = '" & kddoctor & "' "
                SQL &= "AND C.KDKATEGORI_BPJS NOT IN ('15','16', '17') "
                SQL &= "AND A.ISPULANG = 0 "
            End If

            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "S_REQ_RECIPE_RI_D")

            For iLoop As Integer = 0 To ds.Tables("S_REQ_RECIPE_RI_D").Rows.Count - 1
                With ds.Tables("S_REQ_RECIPE_RI_D")
                    fn_LoadResepBykddoctor = .Rows(iLoop)("KDREQRECIPE_RI")
                    grvDetailResep.Focus()
                    grvDetailResep.AddNewRow()
                    grvDetailResep.SetFocusedRowCellValue(colKDITEM, .Rows(iLoop)("KDITEM"))
                    grvDetailResep.SetFocusedRowCellValue(colKDSIGNA, .Rows(iLoop)("KDSIGNA"))
                    grvDetailResep.SetFocusedRowCellValue(colKDCARAPAKAI, .Rows(iLoop)("KDCARAPAKAI"))
                    grvDetailResep.SetFocusedRowCellValue(colQTY, .Rows(iLoop)("QTY"))
                    grvDetailResep.SetFocusedRowCellValue(colREMARKS_DOKTER, .Rows(iLoop)("REMARKS_DOKTER"))
                    grvDetailResep.SetFocusedRowCellValue(colDOSIS, .Rows(iLoop)("DOSIS"))
                    grvDetailResep.SetFocusedRowCellValue(colSTATUSKIRIMRESEP, "MASUK CPPT")

                    'If .Rows(iLoop)("STATUSKIRIMRESEP") = "" Then
                    '    grvDetailResep.SetFocusedRowCellValue(colSTATUSKIRIMRESEP, "MASUK CPPT")
                    'Else
                    '    grvDetailResep.SetFocusedRowCellValue(colSTATUSKIRIMRESEP, .Rows(iLoop)("STATUSKIRIMRESEP"))
                    'End If
                    grvDetailResep.UpdateCurrentRow()

                End With
            Next

            If oConn.State = ConnectionState.Open Then
                oConn.Close()
            End If

            Calculate()
        Catch oErr As Exception
            fn_LoadResepBykddoctor = ""
            MsgBox("Load Resep" & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Function
    Private Sub fn_LoadAsemenMedisdiCPPT(ByVal Register As String)
        Try
            'Alergi
            Dim oIGD As New Transaksi.clsDigital_IGD_01
            Dim dsIGD = oIGD.GetData(sKDREGRAWATJALAN)
            If dsIGD IsNot Nothing Then
                If dsIGD.RIWAYAT_ALERGI <> "" Then
                    chkALERGI_TIDAK.Checked = False
                    chkALERGI_YA.Checked = True
                Else
                    chkALERGI_TIDAK.Checked = True
                    chkALERGI_YA.Checked = False
                End If

                txtALERGI_TEXT.Text = dsIGD.RIWAYAT_ALERGI

            End If

            Dim ds = oDigital_DischargePlanning.GetData(Register)
            If ds IsNot Nothing Then
                txtSUBJEKTIF.Text = ds.ANAMNESIS_01 & vbCrLf & IIf(ds.ANAMNESIS_02 = "", "", ds.ANAMNESIS_02)

                Dim listPemeriksaanFisik As New List(Of String)
                If ds.ANAMNESIS_05 <> "" Then
                    listPemeriksaanFisik.Add("Keadaan Umum : " & ds.ANAMNESIS_05)
                End If
                If ds.ANAMNESIS_06 <> "" Then
                    listPemeriksaanFisik.Add("Kesadaran : " & ds.ANAMNESIS_06)
                End If

                If ds.ANAMNESIS_15_2_CHEK = True Then
                    listPemeriksaanFisik.Add("Kepala Tidak Normal" & IIf(ds.ANAMNESIS_15 <> "", " (" & ds.ANAMNESIS_15 & ")", ""))
                End If

                If ds.ANAMNESIS_16_2_CHEK = True Then
                    listPemeriksaanFisik.Add("Mata Tidak Normal" & IIf(ds.ANAMNESIS_16 <> "", " (" & ds.ANAMNESIS_16 & ")", ""))
                End If

                If ds.ANAMNESIS_17_2_CHEK = True Then
                    listPemeriksaanFisik.Add("Leher Tidak Normal" & IIf(ds.ANAMNESIS_17 <> "", " (" & ds.ANAMNESIS_17 & ")", ""))
                End If

                If ds.ANAMNESIS_18_2_CHEK = True Then
                    listPemeriksaanFisik.Add("Leher Tidak Normal" & IIf(ds.ANAMNESIS_18 <> "", " (" & ds.ANAMNESIS_18 & ")", ""))
                End If

                If ds.ANAMNESIS_19_2_CHEK = True Then
                    listPemeriksaanFisik.Add("Perut Tidak Normal" & IIf(ds.ANAMNESIS_19 <> "", " (" & ds.ANAMNESIS_19 & ")", ""))
                End If

                If ds.ANAMNESIS_20_2_CHEK = True Then
                    listPemeriksaanFisik.Add("Perut Tidak Normal" & IIf(ds.ANAMNESIS_20 <> "", " (" & ds.ANAMNESIS_20 & ")", ""))
                End If
                If ds.ANAMNESIS_29 <> "" Then
                    listPemeriksaanFisik.Add("Genetalia : " & ds.ANAMNESIS_29)
                End If
                If ds.ANAMNESIS_30 <> "" Then
                    listPemeriksaanFisik.Add("Ekstremitas : " & ds.ANAMNESIS_30)
                End If
                If ds.ANAMNESIS_31 <> "" Then
                    listPemeriksaanFisik.Add("Kulit : " & ds.ANAMNESIS_31)
                End If
                If ds.ANAMNESIS_32 <> "" Then
                    listPemeriksaanFisik.Add("status lokalis : " & ds.ANAMNESIS_32)
                End If

                txtPEMERIKSAANLAIN.Text = String.Join(vbCrLf, listPemeriksaanFisik.ToArray)

                txtOBJEKTIF_BERATBADAN.Text = ds.ANAMNESIS_08
                txtOBJEKTIF_NADI.Text = ds.ANAMNESIS_11
                txtOBJEKTIF_RESPIRASI.Text = ds.ANAMNESIS_14
                'txtOBJEKTIF_SATURASIOKSIGEN.Text = .OBJEKTIF_SATURASIOKSIGEN
                txtOBJEKTIF_SUHU.Text = ds.ANAMNESIS_13
                txtOBJEKTIF_TEKANANDARAH.Text = ds.ANAMNESIS_12
                txtOBJEKTIF_TINGGIBADAN.Text = ds.ANAMNESIS_09

                txtPEMERIKSAANPENUNJANG_CPPT.Text = ds.ANAMNESIS_33

                BindingSourceDiagnosa_CPPT.DataSource = oDigital_DischargePlanning.GetDataDetailDiagnosa(lblRegister.Text).OrderBy(Function(x) x.SEQ).ToList()
                grdDIAGNOSA_CPPT.DataSource = BindingSourceDiagnosa_CPPT

                txtPlanning.Text = ds.ANAMNESIS_36
                'txtPlanningObat.Text = ds.ANAMNESIS_36 & IIf(ds.ANAMNESIS_37 <> "", vbCrLf & ds.ANAMNESIS_37, "")

                lblCOPY.Text = "Copy CPPT dari Reload Asesmen Awal Medis"
            End If

        Catch oErr As Exception
            MsgBox("Form Browse Load CPPT" & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_CopyCPPTAkhirdiCPPTTTVPerawat(ByVal Parameter As String)
        Try
            ' ***** HEADER *****
            Dim dsLainnya = oCPPT.GetDataLainnya(Parameter)

            If dsLainnya IsNot Nothing Then
                chkALERGI_TIDAK.Checked = dsLainnya.ALERGI_TIDAK
                chkALERGI_YA.Checked = dsLainnya.ALERGI_YA
                txtALERGI_TEXT.Text = dsLainnya.ALERGI_TEXT
                txtOBJEKTIF_BERATBADAN.Text = dsLainnya.OBJEKTIF_BERATBADAN
                txtOBJEKTIF_NADI.Text = dsLainnya.OBJEKTIF_NADI
                txtOBJEKTIF_RESPIRASI.Text = dsLainnya.OBJEKTIF_RESPIRASI
                txtOBJEKTIF_SATURASIOKSIGEN.Text = dsLainnya.OBJEKTIF_SATURASIOKSIGEN
                txtOBJEKTIF_SUHU.Text = dsLainnya.OBJEKTIF_SUHU
                txtOBJEKTIF_TEKANANDARAH.Text = dsLainnya.OBJEKTIF_TEKANANDARAH
                txtOBJEKTIF_TINGGIBADAN.Text = dsLainnya.OBJEKTIF_TINGGIBADAN
            End If

        Catch oErr As Exception
            MsgBox("Load List Data : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_CopyCPPTAkhirdiCPPT(ByVal Parameter As String)
        Try
            ' ***** HEADER *****
            Dim ds = oCPPT.GetData(Parameter)

            If ds Is Nothing Then
                Exit Sub
            End If

            With ds
                txtSUBJEKTIF.Text = .SUBJEKTIF
                txtPEMERIKSAANLAIN.Text = .ALAMAT
            End With

            Dim dsLainnya = oCPPT.GetDataLainnya(Parameter)

            With dsLainnya
                chkALERGI_TIDAK.Checked = .ALERGI_TIDAK
                chkALERGI_YA.Checked = .ALERGI_YA
                txtALERGI_TEXT.Text = .ALERGI_TEXT
                txtOBJEKTIF_BERATBADAN.Text = .OBJEKTIF_BERATBADAN
                txtOBJEKTIF_NADI.Text = .OBJEKTIF_NADI
                txtOBJEKTIF_RESPIRASI.Text = .OBJEKTIF_RESPIRASI
                txtOBJEKTIF_SATURASIOKSIGEN.Text = .OBJEKTIF_SATURASIOKSIGEN
                txtOBJEKTIF_SUHU.Text = .OBJEKTIF_SUHU
                txtOBJEKTIF_TEKANANDARAH.Text = .OBJEKTIF_TEKANANDARAH
                txtOBJEKTIF_TINGGIBADAN.Text = .OBJEKTIF_TINGGIBADAN

                txtINTRUKSILAIN.Text = .INTRUKSILAIN
            End With

            Dim dsLainnyaTambah = oCPPT.GetDataLainnyaTambah(Parameter)
            If dsLainnyaTambah IsNot Nothing Then
                With dsLainnyaTambah
                    txtPlanning.Text = .CATATAN_1
                    'txtPlanningObat.Text = .CATATAN_2
                    txtPEMERIKSAANPENUNJANG_CPPT.Text = .CATATAN_5
                    sReplacePenunjangCPPT = .CATATAN_5
                End With
            End If

            BindingSourceDiagnosa_CPPT.DataSource = oCPPT.GetDataDetailDiagnosa(Parameter).OrderBy(Function(x) x.SEQ).ToList()
            grdDIAGNOSA_CPPT.DataSource = BindingSourceDiagnosa_CPPT

            BindingSourceProsedur_CPPT.DataSource = oCPPT.GetDataDetailProsedur(Parameter).OrderBy(Function(x) x.SEQ).ToList()
            grdPROSEDUR_CPPT.DataSource = BindingSourceProsedur_CPPT
        Catch oErr As Exception
            MsgBox("Load List Data : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadAsesmenAwalMedisIGDdiCPPT(ByVal KDREG As String)
        Try
            Dim oIGD As New Transaksi.clsDigital_IGD_01
            Dim dsIGD = oIGD.GetData(KDREG)
            If dsIGD IsNot Nothing Then
                If dsIGD.RIWAYAT_ALERGI <> "" Then
                    chkALERGI_TIDAK.Checked = False
                    chkALERGI_YA.Checked = True
                Else
                    chkALERGI_TIDAK.Checked = True
                    chkALERGI_YA.Checked = False
                End If

                cboTINGKATKESADARAN.Text = dsIGD.TINGKATKESADARAN
                txtALERGI_TEXT.Text = dsIGD.RIWAYAT_ALERGI
                txtOBJEKTIF_BERATBADAN.Text = dsIGD.BERATBADAN
                txtOBJEKTIF_NADI.Text = dsIGD.HR
                txtOBJEKTIF_RESPIRASI.Text = dsIGD.RR
                txtOBJEKTIF_SATURASIOKSIGEN.Text = dsIGD.SP02
                txtOBJEKTIF_SUHU.Text = dsIGD.T
                txtOBJEKTIF_TEKANANDARAH.Text = dsIGD.BP
                txtOBJEKTIF_TINGGIBADAN.Text = dsIGD.TINGGIBADAN
                'txtINTRUKSILAIN.Text = .INTRUKSILAIN
                'txtPEMERIKSAANLAIN
                'cboProfesi.Text = .PROFESI
                txtSUBJEKTIF.Text = dsIGD.RIWAYAT_PENYAKITDAHULU
                'txtPEMERIKSAANLAIN.Text = .ALAMAT
                'txtDIAGNOOSA.Text = dsIGD.KDDIAGNOSA

                Dim Pemeriksaan As String = String.Empty

                If dsIGD.TIDAK_NORMAL_KEPALA = True Then
                    Pemeriksaan = "Kepala Tidak Normal" & IIf(dsIGD.SURVEY_KEPALA_2 <> "", " (" & dsIGD.SURVEY_KEPALA_2 & ")", "")
                Else
                    Pemeriksaan = "Kepala Normal" & IIf(dsIGD.SURVEY_KEPALA_2 <> "", " (" & dsIGD.SURVEY_KEPALA_2 & ")", "")
                End If

                If dsIGD.TIDAK_NORMAL_MATA = True Then
                    Pemeriksaan = Pemeriksaan & vbCrLf & "Mata Tidak Normal" & IIf(dsIGD.SURVEY_MATA_2 <> "", " (" & dsIGD.SURVEY_MATA_2 & ")", "")
                Else
                    Pemeriksaan = Pemeriksaan & vbCrLf & "Mata Normal" & IIf(dsIGD.SURVEY_MATA_2 <> "", " (" & dsIGD.SURVEY_MATA_2 & ")", "")
                End If

                If dsIGD.TIDAK_NORMAL_LEHER = True Then
                    Pemeriksaan = Pemeriksaan & vbCrLf & "Leher Tidak Normal" & IIf(dsIGD.SURVEY_LEHER_2 <> "", " (" & dsIGD.SURVEY_LEHER_2 & ")", "")
                Else
                    Pemeriksaan = Pemeriksaan & vbCrLf & "Leher Normal" & IIf(dsIGD.SURVEY_LEHER_2 <> "", " (" & dsIGD.SURVEY_LEHER_2 & ")", "")
                End If

                If dsIGD.TIDAK_NORMAL_DADA = True Then
                    Pemeriksaan = Pemeriksaan & vbCrLf & "Dada Tidak Normal" & IIf(dsIGD.SURVEY_DADA_2 <> "", " (" & dsIGD.SURVEY_DADA_2 & ")", "")
                Else
                    Pemeriksaan = Pemeriksaan & vbCrLf & "Dada Normal" & IIf(dsIGD.SURVEY_DADA_2 <> "", " (" & dsIGD.SURVEY_DADA_2 & ")", "")
                End If

                If dsIGD.TIDAK_NORMAL_PERUT = True Then
                    Pemeriksaan = Pemeriksaan & vbCrLf & "Perut Tidak Normal" & IIf(dsIGD.SURVEY_PERUT_2 <> "", " (" & dsIGD.SURVEY_PERUT_2 & ")", "")
                Else
                    Pemeriksaan = Pemeriksaan & vbCrLf & "Perut Normal" & IIf(dsIGD.SURVEY_PERUT_2 <> "", " (" & dsIGD.SURVEY_PERUT_2 & ")", "")
                End If

                If dsIGD.TIDAK_NORMAL_ALATGERAK = True Then
                    Pemeriksaan = Pemeriksaan & vbCrLf & "Alat Gerak Tidak Normal" & IIf(dsIGD.SURVEY_ALATGERAK_2 <> "", " (" & dsIGD.SURVEY_ALATGERAK_2 & ")", "")
                Else
                    Pemeriksaan = Pemeriksaan & vbCrLf & "Alat Gerak Normal" & IIf(dsIGD.SURVEY_ALATGERAK_2 <> "", " (" & dsIGD.SURVEY_ALATGERAK_2 & ")", "")
                End If

                txtPEMERIKSAANLAIN.Text = Pemeriksaan & dsIGD.SURVEY_DADA_1

                lblCOPY.Text = "Copy CPPT dari Asesemen Awal Medis IGD"

                BindingSourceDiagnosa_CPPT.DataSource = oIGD.GetDataDetail(KDREG).OrderBy(Function(x) x.SEQ).ToList()
                grdDIAGNOSA_CPPT.DataSource = BindingSourceDiagnosa_CPPT
            Else
                MsgBox("Asesmen Awal Medis IGD Belum di Buat", MsgBoxStyle.Exclamation, Me.Text)
            End If
        Catch oErr As Exception
            MsgBox("Load List Data Asesemen Awal Medis IGD: " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadDataCPPT()
        Try
            ' ***** HEADER *****
            Dim dsLainnya = oCPPT.GetDataLainnya(lblKODECPPT.Text)

            If dsLainnya IsNot Nothing Then
                With dsLainnya
                    chkALERGI_TIDAK.Checked = .ALERGI_TIDAK
                    chkALERGI_YA.Checked = .ALERGI_YA
                    txtALERGI_TEXT.Text = .ALERGI_TEXT
                    txtOBJEKTIF_BERATBADAN.Text = .OBJEKTIF_BERATBADAN
                    txtOBJEKTIF_NADI.Text = .OBJEKTIF_NADI
                    txtOBJEKTIF_RESPIRASI.Text = .OBJEKTIF_RESPIRASI
                    txtOBJEKTIF_SATURASIOKSIGEN.Text = .OBJEKTIF_SATURASIOKSIGEN
                    txtOBJEKTIF_SUHU.Text = .OBJEKTIF_SUHU
                    txtOBJEKTIF_TEKANANDARAH.Text = .OBJEKTIF_TEKANANDARAH
                    txtOBJEKTIF_TINGGIBADAN.Text = .OBJEKTIF_TINGGIBADAN
                    txtINTRUKSILAIN.Text = .INTRUKSILAIN
                End With
            End If


            Dim ds = oCPPT.GetData(lblKODECPPT.Text)

            If ds IsNot Nothing Then
                With ds
                    txtSUBJEKTIF.Text = .SUBJEKTIF
                    txtPEMERIKSAANLAIN.Text = .ALAMAT
                    'txtDIAGNOOSA.Text = .ASSEMENT
                    cboProfesi.Text = .PROFESI

                    fn_ViewCPPTSBAR(.SUKU)
                End With
            End If


            Dim dsLainnyaTambah = oCPPT.GetDataLainnyaTambah(lblKODECPPT.Text)

            If dsLainnyaTambah IsNot Nothing Then
                With dsLainnyaTambah
                    txtPlanning.Text = .CATATAN_1
                    'txtPlanningObat.Text = .CATATAN_2
                    txtPEMERIKSAANPENUNJANG_CPPT.Text = .CATATAN_5
                    'txtINTRUKSIORDERCPPT.Text = .CATATAN_6
                    txtINTRUKSILAIN_OBATPULANG.Text = .CATATAN_6
                    txtINTRUKSILAIN_EDUKASI.Text = .CATATAN_7
                End With
            End If


            BindingSourceDiagnosa_CPPT.DataSource = oCPPT.GetDataDetailDiagnosa(lblKODECPPT.Text).OrderBy(Function(x) x.SEQ).ToList()
            grdDIAGNOSA_CPPT.DataSource = BindingSourceDiagnosa_CPPT

            BindingSourceProsedur_CPPT.DataSource = oCPPT.GetDataDetailProsedur(lblKODECPPT.Text).OrderBy(Function(x) x.SEQ).ToList()
            grdPROSEDUR_CPPT.DataSource = BindingSourceProsedur_CPPT

            Dim listdiagnosa As New List(Of String)

            For i As Integer = 0 To grvDIAGNOSA_CPPT.RowCount - 2
                listdiagnosa.Add(i & "." & grvDIAGNOSA_CPPT.GetRowCellValue(i, colKDDIAGNOSA_CPPT) & "-" & grvDIAGNOSA_CPPT.GetRowCellValue(i, colNAMADIAGNOSA_CPPT))
            Next

            'If listdiagnosa.Count > 0 Then
            '    txtDIAGNOOSA.Text = txtDIAGNOOSA.Text.ToString.Replace(String.Join(vbCrLf, listdiagnosa.ToArray), "")
            'End If

            'Dim listprosedur As New List(Of String)
            'For i As Integer = 0 To grvPROSEDUR_CPPT.RowCount - 2
            '    listprosedur.Add(i & "." & grvPROSEDUR_CPPT.GetRowCellValue(i, colKDPROSEDUR_CPPT) & "-" & grvPROSEDUR_CPPT.GetRowCellValue(i, colNAMAPROSEDUR_CPPT))
            'Next

            'If listdiagnosa.Count > 0 Then
            '    txtPlanning.Text = txtPlanning.Text.ToString.Replace(String.Join(vbCrLf, listprosedur.ToArray), "")
            'End If

            Dim dsResep = oReqRecipeRawatInap.GetData(lblKODECPPT.Text)

            If dsResep IsNot Nothing Then
                lblTOTALRESEPDICPPT.Text = FormatNumber(dsResep.GRANDTOTAL, 0)

                bindingSource.DataSource = oReqRecipeRawatInap.GetDataDetail(dsResep.KDREQRECIPE_RI).OrderBy(Function(x) x.SEQ).ToList()
                grdDetailResep.DataSource = bindingSource
            End If

        Catch oErr As Exception
            MsgBox("Load List Data : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Function fn_ValidateCPPT(ByVal sGizi As Boolean) As Boolean
        Try
            fn_ValidateCPPT = True
            If lblRegister.Text = String.Empty Then
                MsgBox("Dibutuhkan Register", MsgBoxStyle.Exclamation, Me.Text)
                'txtRegister.Focus()
                fn_ValidateCPPT = False
                Exit Function
            End If
            If sGizi = False Then
                If sISDOKTER = False Then
                    If cboProfesi.Text = String.Empty Then
                        MsgBox("Dibutuhkan Profesi", MsgBoxStyle.Exclamation, Me.Text)
                        'txtRegister.Focus()
                        fn_ValidateCPPT = False
                        Exit Function
                    End If
                End If
            End If

            'If sCasemix = False Then
            '    Dim Pesan As String = fn_Cek(1, "CPPT", sDateWaktuCPPT)

            '    If Pesan <> "" Then
            '        MsgBox(Pesan, MsgBoxStyle.Information, Me.Text)
            '        fn_ValidateCPPT = False
            '        Exit Function
            '    End If
            'End If
        Catch oErr As Exception
            MsgBox("Validate Data : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Function
    Private Function fn_SaveCPPT(ByVal Kode As String, ByVal sGizi As Boolean, ByVal isfarmasi As Boolean) As Boolean
        Try
            ' ***** HEADER *****
            Dim listdiagnosa As New List(Of String)

            Dim dsAmbildata = oCPPT.GetData(Kode)

            Dim ds = oCPPT.GetStructureHeader
            With ds
                .DATEUPDATED = Now
                .KDCPPT = Kode
                .KDCUSTOMER = lblNoRM.Text
                .KDPENDAFTARAN = lblRegister.Text
                .PROFESI = IIf(isfarmasi = True, "APOTEKER", IIf(sGizi = False, IIf(sISDOKTER = True, "DOKTER", cboProfesi.Text), "GIZI"))
                .NAMAPASIEN = lblNamaPasien.Text
                .JK = lblJenisKelamin.Text
                .TANGGALLAHIR = sTanggalLahir
                .PENJAMIN = lblPenjamin.Text
                .ALAMAT = IIf(isfarmasi = True, "", IIf(sGizi = False, txtPEMERIKSAANLAIN.Text, ""))
                .SUBJEKTIF = IIf(isfarmasi = True, txtSubjektifFarmasi.Text, IIf(sGizi = False, txtSUBJEKTIF.Text, txtA_CPPTGizi.Text))

                Dim listSUBOBJEKTIF As New List(Of String)

                listSUBOBJEKTIF.Add("Umur : " & sUMURPASIEN)
                If txtOBJEKTIF_BERATBADAN.Text <> "" Then
                    If txtOBJEKTIF_BERATBADAN.Text <> "-" Then
                        listSUBOBJEKTIF.Add("BB : " & txtOBJEKTIF_BERATBADAN.Text & " kg")
                    End If
                End If
                If txtOBJEKTIF_TINGGIBADAN.Text <> "" Then
                    If txtOBJEKTIF_TINGGIBADAN.Text <> "-" Then
                        listSUBOBJEKTIF.Add("TB : " & txtOBJEKTIF_TINGGIBADAN.Text & " cm")
                    End If
                End If
                If cboTINGKATKESADARAN.Text <> "" Then
                    listSUBOBJEKTIF.Add("Tingkat Kesadaran : " & cboTINGKATKESADARAN.Text)
                End If
                If txtOBJEKTIF_TEKANANDARAH.Text <> "" Then
                    If txtOBJEKTIF_TEKANANDARAH.Text <> "-" Then
                        listSUBOBJEKTIF.Add("Tekanan Darah : " & txtOBJEKTIF_TEKANANDARAH.Text & " mmHg")
                    End If
                End If
                If txtOBJEKTIF_NADI.Text <> "" Then
                    If txtOBJEKTIF_NADI.Text <> "-" Then
                        listSUBOBJEKTIF.Add("Nadi : " & txtOBJEKTIF_NADI.Text & " x/mnt")
                    End If
                End If
                If txtOBJEKTIF_RESPIRASI.Text <> "" Then
                    If txtOBJEKTIF_RESPIRASI.Text <> "-" Then
                        listSUBOBJEKTIF.Add("Respirasi : " & txtOBJEKTIF_RESPIRASI.Text & " x/mnt")
                    End If
                End If
                If txtOBJEKTIF_SATURASIOKSIGEN.Text <> "" Then
                    If txtOBJEKTIF_SATURASIOKSIGEN.Text <> "-" Then
                        listSUBOBJEKTIF.Add("Saturasi Oksigen : " & txtOBJEKTIF_SATURASIOKSIGEN.Text & " %")
                    End If
                End If
                If txtOBJEKTIF_SUHU.Text <> "" Then
                    If txtOBJEKTIF_SUHU.Text <> "-" Then
                        listSUBOBJEKTIF.Add("Suhu : " & txtOBJEKTIF_SUHU.Text & " oC")
                    End If
                End If
                If txtPEMERIKSAANLAIN.Text <> "" Then
                    If txtPEMERIKSAANLAIN.Text <> "-" Then
                        listSUBOBJEKTIF.Add("Pemeriksaan : " & txtPEMERIKSAANLAIN.Text)
                    End If
                End If
                If txtPEMERIKSAANPENUNJANG_CPPT.Text <> "" Then
                    listSUBOBJEKTIF.Add("Pemeriksaan Penunjang : " & txtPEMERIKSAANPENUNJANG_CPPT.Text)
                End If

                .OBJEKTIF = IIf(isfarmasi = True, txtObjektifFarmasi.Text, IIf(sGizi = False, String.Join(vbCrLf, listSUBOBJEKTIF.ToArray), txtD_CPPTGizi.Text))

                For i As Integer = 0 To grvDIAGNOSA_CPPT.RowCount - 2
                    listdiagnosa.Add(grvDIAGNOSA_CPPT.GetRowCellValue(i, colNAMADIAGNOSA_CPPT))
                Next

                Dim Diagnosalabel As String = String.Join(vbCrLf, listdiagnosa.ToArray)

                Dim listprosedur As New List(Of String)
                For i As Integer = 0 To grvPROSEDUR_CPPT.RowCount - 2
                    listprosedur.Add(grvPROSEDUR_CPPT.GetRowCellValue(i, colNAMAPROSEDUR_CPPT))
                Next

                Dim Prosedurlabel As String = String.Join(vbCrLf, listprosedur.ToArray)

                Dim listResep As New List(Of String)
                Dim planning As String = String.Empty

                For i As Integer = 0 To grvDetailResep.RowCount - 2
                    If grvDetailResep.GetRowCellValue(i, colSTATUSKIRIMRESEP) <> "TIDAK MASUK CPPT" Then
                        listResep.Add(frmReqRecipeRawatInap.fn_LoadITEM(grvDetailResep.GetRowCellValue(i, colKDITEM)).Replace("(EC)", "").ToString.Trim & IIf(String.IsNullOrEmpty(grvDetailResep.GetRowCellValue(i, colDOSIS)), "", (" " & grvDetailResep.GetRowCellValue(i, colDOSIS)).ToString.Trim) & IIf(String.IsNullOrEmpty(grvDetailResep.GetRowCellValue(i, colKDSIGNA)), "", (" " & frmReqRecipeRawatInap.fn_LoadSIGNA(grvDetailResep.GetRowCellValue(i, colKDSIGNA))).ToString.Trim) & IIf(String.IsNullOrEmpty(grvDetailResep.GetRowCellValue(i, colKDCARAPAKAI)), "", frmReqRecipeRawatInap.fn_LoadCARAPAKAI((" " & grvDetailResep.GetRowCellValue(i, colKDCARAPAKAI)))).ToString.Trim)
                    End If
                Next

                If listResep.Count > 0 Then
                    planning = (String.Join(vbCrLf, listResep.ToArray) & vbCrLf & IIf(isfarmasi = True, txtPlanningFarmasi.Text, IIf(sGizi = False, txtPlanning.Text, txtME_CPPTGizi.Text))).ToString.Trim
                Else
                    planning = IIf(isfarmasi = True, txtPlanningFarmasi.Text, IIf(sGizi = False, txtPlanning.Text, txtME_CPPTGizi.Text))
                End If

                .ASSEMENT = IIf(isfarmasi = True, txtAsesmenFarmasi.Text, IIf(sGizi = False, IIf(Diagnosalabel = "", "", Diagnosalabel) & vbCrLf & IIf(Prosedurlabel = "", "", vbCrLf & Prosedurlabel), txtI_CPPTGizi.Text))
                .PLANNING = planning

                If dsAmbildata IsNot Nothing Then
                    .DATECREATED = dsAmbildata.DATECREATED
                    .DATE = dsAmbildata.DATE
                    .NIK = dsAmbildata.NIK
                    .SUKU = dsAmbildata.SUKU
                    .AGAMA = dsAmbildata.AGAMA
                    .ISCHEKED = dsAmbildata.ISCHEKED
                    .NOTELEPON = dsAmbildata.NOTELEPON

                    .KDUSER = IIf(sCasemix = True, dsAmbildata.KDUSER, IIf(sISDOKTER = False, sKDUSER_PERAWAT, sUserID))
                    .TEMPATLAHIR = IIf(sCasemix = True, dsAmbildata.TEMPATLAHIR, lblRuangan.Text)

                    .KETERANGAN_1 = dsAmbildata.KETERANGAN_1
                    .KETERANGAN_2 = dsAmbildata.KETERANGAN_2
                    .KETERANGAN_3 = dsAmbildata.KETERANGAN_3

                    .HANDOVERPEMBERI_TANGGAL = dsAmbildata.HANDOVERPEMBERI_TANGGAL
                    .HANDOVERPEMBERI_KDUSER = dsAmbildata.HANDOVERPEMBERI_KDUSER
                    .HANDOVERPENERIMA_TANGGAL = dsAmbildata.HANDOVERPENERIMA_TANGGAL
                    .HANDOVERPENERIMA_KDUSER = dsAmbildata.HANDOVERPENERIMA_KDUSER

                    .NILAIKRITISPEMBERI_TANGGAL = dsAmbildata.NILAIKRITISPEMBERI_TANGGAL
                    .NILAIKRITISPEMBERI_KDUSER = dsAmbildata.NILAIKRITISPEMBERI_KDUSER
                    .NILAIKRITISPENERIMA_TANGGAL = dsAmbildata.NILAIKRITISPENERIMA_TANGGAL
                    .NILAIKRITISPENERIMA_KDUSER = dsAmbildata.NILAIKRITISPENERIMA_KDUSER

                    .SBARPEMBERI_TANGGAL = dsAmbildata.SBARPEMBERI_TANGGAL
                    .SBARPEMBERI_KDUSER = dsAmbildata.SBARPEMBERI_KDUSER
                    .SBARPENERIMA_TANGGAL = dsAmbildata.SBARPENERIMA_TANGGAL
                    .SBARPENERIMA_KDUSER = dsAmbildata.SBARPENERIMA_KDUSER
                Else
                    .DATECREATED = Now
                    .DATE = Now
                    .NIK = ""
                    .SUKU = IIf(isfarmasi = True, "TAMBAH FARMASI", IIf(sGizi = False, IIf(tabCPPT.Text.Contains("CPPT"), "TAMBAH CPPT", "TAMBAH SBAR"), "TAMBAH GIZI"))
                    .AGAMA = ""
                    .ISCHEKED = False
                    .NOTELEPON = ""
                    If sISDOKTER = False Then
                        .KDUSER = sKDUSER_PERAWAT
                    Else
                        .KDUSER = sUserID
                    End If
                    .TEMPATLAHIR = lblRuangan.Text
                    .KETERANGAN_1 = ""
                    .KETERANGAN_2 = ""
                    .KETERANGAN_3 = ""

                    .HANDOVERPEMBERI_TANGGAL = ""
                    .HANDOVERPEMBERI_KDUSER = ""
                    .HANDOVERPENERIMA_TANGGAL = ""
                    .HANDOVERPENERIMA_KDUSER = ""

                    .NILAIKRITISPEMBERI_TANGGAL = ""
                    .NILAIKRITISPEMBERI_KDUSER = ""
                    .NILAIKRITISPENERIMA_TANGGAL = ""
                    .NILAIKRITISPENERIMA_KDUSER = ""

                    .SBARPEMBERI_TANGGAL = ""
                    .SBARPEMBERI_KDUSER = ""
                    .SBARPENERIMA_TANGGAL = ""
                    .SBARPENERIMA_KDUSER = ""
                End If

            End With

            Dim dsLainnya = oCPPT.GetStructureHeaderlainnya
            With dsLainnya
                .KDCPPT = ds.KDCPPT
                .ALERGI_TIDAK = chkALERGI_TIDAK.Checked
                .ALERGI_YA = chkALERGI_YA.Checked
                .ALERGI_TEXT = txtALERGI_TEXT.Text
                .OBJEKTIF_JENISKELAMIN = lblJenisKelamin.Text
                .OBJEKTIF_UMUR = sUMURPASIEN
                .OBJEKTIF_BERATBADAN = txtOBJEKTIF_BERATBADAN.Text
                .OBJEKTIF_NADI = txtOBJEKTIF_NADI.Text
                .OBJEKTIF_RESPIRASI = txtOBJEKTIF_RESPIRASI.Text
                .OBJEKTIF_SATURASIOKSIGEN = txtOBJEKTIF_SATURASIOKSIGEN.Text
                .OBJEKTIF_SUHU = txtOBJEKTIF_SUHU.Text
                .OBJEKTIF_TEKANANDARAH = txtOBJEKTIF_TEKANANDARAH.Text
                .OBJEKTIF_TINGGIBADAN = txtOBJEKTIF_TINGGIBADAN.Text
                .INTRUKSILAIN = txtINTRUKSILAIN.Text
            End With

            Dim dsLainnyaTambah = oCPPT.GetStructureHeaderlainnyaTambah
            With dsLainnyaTambah
                .KDCPPT = ds.KDCPPT
                .CATATAN_1 = txtPlanning.Text
                .CATATAN_2 = ""
                .CATATAN_3 = ""
                .CATATAN_4 = lblCOPY.Text
                Try
                    '.CATATAN_5 = IIf(sReplacePenunjangCPPT <> "", txtPEMERIKSAANPENUNJANG_CPPT.Text.ToString.Replace(sReplacePenunjangCPPT, ""), txtPEMERIKSAANPENUNJANG_CPPT.Text)
                    .CATATAN_5 = txtPEMERIKSAANPENUNJANG_CPPT.Text
                Catch ex As Exception
                    .CATATAN_5 = txtPEMERIKSAANPENUNJANG_CPPT.Text
                End Try
                .CATATAN_6 = txtINTRUKSILAIN_OBATPULANG.Text
                .CATATAN_7 = txtINTRUKSILAIN_EDUKASI.Text
                .CATATAN_8 = cboTINGKATKESADARAN.Text
                .CATATAN_9 = ""
                .CATATAN_10 = ""
                .CATATAN_11 = ""
            End With

            'DIAGNOSA
            Dim arrDetailDiagnosa = oCPPT.GetStructureDetaiDiagnosalList
            For i As Integer = 0 To grvDIAGNOSA_CPPT.RowCount - 2
                Dim dsDetail = oCPPT.GetStructureDetailDiagnosa
                With dsDetail
                    .SEQ = i
                    .KDCPPT = ds.KDCPPT
                    .KATEGORI = grvDIAGNOSA_CPPT.GetRowCellValue(i, colKATEGORI_CPPT)
                    .NAMADIAGNOSA = grvDIAGNOSA_CPPT.GetRowCellValue(i, colNAMADIAGNOSA_CPPT)
                    .KDDIAGNOSA = IIf(String.IsNullOrEmpty(grvDIAGNOSA_CPPT.GetRowCellValue(i, colKDDIAGNOSA_CPPT)), "", grvDIAGNOSA_CPPT.GetRowCellValue(i, colKDDIAGNOSA_CPPT))
                End With
                arrDetailDiagnosa.Add(dsDetail)
            Next
            'PROSEDUR
            Dim arrDetailProsedur = oCPPT.GetStructureDetaiProsedurlList
            For i As Integer = 0 To grvPROSEDUR_CPPT.RowCount - 2
                Dim dsDetail = oCPPT.GetStructureDetailProsedur
                With dsDetail
                    .SEQ = i
                    .KDCPPT = ds.KDCPPT
                    .KATEGORI = ""
                    .NAMAPROSEDUR = grvPROSEDUR_CPPT.GetRowCellValue(i, colNAMAPROSEDUR_CPPT)
                    .KDPROSEDUR = IIf(String.IsNullOrEmpty(grvPROSEDUR_CPPT.GetRowCellValue(i, colKDPROSEDUR_CPPT)), "", grvPROSEDUR_CPPT.GetRowCellValue(i, colKDPROSEDUR_CPPT))
                End With
                arrDetailProsedur.Add(dsDetail)
            Next

            If sGizi = False Then
                If sISDOKTER = False Then
                    Dim oUpdateCPPT As New Admission.clsReqCPPTUpdate
                    If oUpdateCPPT.DeleteData(lblRegister.Text) = True Then

                    End If
                End If

                If Kode = "TAMBAH CPPT/SBAR" Then
                    fn_SaveCPPT = oCPPT.InsertData(ds, dsLainnya, dsLainnyaTambah, arrDetailDiagnosa, arrDetailProsedur)
                    fn_SimpanCPPTSelesai(ds.DATE.ToString("yyyyMMdd", CultureInfo.InvariantCulture), ds.KDCPPT, ds.KDPENDAFTARAN, IIf(sISDOKTER = False, "", sKDDOCTOR_INPUT), IIf(sISDOKTER = False, "PERAWAT", sUserID))
                Else
                    fn_SaveCPPT = oCPPT.UpdateData(ds, dsLainnya, dsLainnyaTambah, arrDetailDiagnosa, arrDetailProsedur)

                    If sISDOKTER = True Then
                        Dim oUpdateCPPT As New Admission.clsReqCPPTUpdate

                        If oUpdateCPPT.DeleteData(lblRegister.Text) = True Then
                            Dim dsCekUpdate = oUpdateCPPT.GetData(lblRegister.Text)
                            If dsCekUpdate Is Nothing Then
                                Dim dsIsnerUpdateCPPT = oUpdateCPPT.GetStructureHeader
                                With dsIsnerUpdateCPPT
                                    .DATECREATED = Now
                                    .KDREG = lblRegister.Text
                                    .DESCRIPTION = sUserID
                                End With

                                oUpdateCPPT.InsertData(dsIsnerUpdateCPPT)
                            End If
                        End If
                    End If
                End If
            ElseIf isfarmasi = True Then
                Dim dsCek = oCPPT.GetData(lblKODECPPT.Text)
                If dsCek Is Nothing Then
                    fn_SaveCPPT = oCPPT.InsertDataCPPTGizidanFarmasi(ds)
                Else
                    fn_SaveCPPT = oCPPT.UpdateDataCPPTGizidanFarmasi(ds)
                End If
            Else
                Dim dsCek = oCPPT.GetData(lblKODECPPT.Text)
                If dsCek Is Nothing Then
                    fn_SaveCPPT = oCPPT.InsertDataCPPTGizidanFarmasi(ds)
                Else
                    fn_SaveCPPT = oCPPT.UpdateDataCPPTGizidanFarmasi(ds)
                End If
            End If

            lblKODECPPT.Text = ds.KDCPPT

            Try
                Dim oTerimaResep As New Reference.clsResepTerima
                Dim dsCekTerimaResep = oTerimaResep.GetData(lblRegister.Text & "-" & ds.DATE.ToString("ddMMyyyy", CultureInfo.InvariantCulture))

                If dsCekTerimaResep IsNot Nothing Then
                    Dim dsTerimaResep = oTerimaResep.GetStructureHeader
                    With dsTerimaResep
                        .KDTERIMARESEP = dsCekTerimaResep.KDTERIMARESEP
                        .DESCRIPTION = "PERBAIKAN"
                    End With

                    oTerimaResep.UpdateData(dsTerimaResep)
                End If
            Catch ex As Exception
                MsgBox("Update Data Terima Resep: " & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
            End Try

            'If fn_SaveCPPT = True Then
            '    If skodeorderlab <> "" Then
            '        Try
            '            Dim dscekdata = oOrderPenunjang.GetDataKDCPPTLABAWALBYKODE(skodeorderlab)
            '            If dscekdata IsNot Nothing Then
            '                ' ***** DETIL *****
            '                Dim arrDetail = oOrderPenunjang.GetStructureDetailAwalList
            '                For Each xloop In oOrderPenunjang.GetDataDetailLisLabAwal(skodeorderlab).OrderBy(Function(x) x.SEQ)
            '                    Dim dsDetail = oOrderPenunjang.GetStructureDetailAwal
            '                    With dsDetail
            '                        .KODE = skodeorderlab
            '                        .SEQ = xloop.SEQ
            '                        .DATECREATED = xloop.DATECREATED
            '                        .DATEUPDATED = xloop.DATEUPDATED
            '                        .DATE = xloop.DATE
            '                        .KDCPPT = ds.KDCPPT
            '                        .KDREG = lblRegister.Text
            '                        .KDCUSTOMER = ds.KDCUSTOMER
            '                        .PASIEN = ds.NAMAPASIEN
            '                        .TUJUAN = lblRuangan.Text
            '                        .DOKTER = xloop.DOKTER
            '                        .KATEGORI = "LABORATORIUM"
            '                        .NAMAORDER = xloop.NAMAORDER
            '                        .DIAGNOSA = String.Join(vbCrLf, listdiagnosa.ToArray)
            '                        .MEMO = "SIMPAN DARI CPPT"
            '                        .ISAPPROVE = False
            '                        .INDIKASIMEDIS = xloop.INDIKASIMEDIS
            '                    End With
            '                    arrDetail.Add(dsDetail)
            '                Next

            '                If arrDetail.Count > 0 Then
            '                    If oOrderPenunjang.UpdateDataAwal(arrDetail) = False Then
            '                        MsgBox("Order Laboratorium gagal", MsgBoxStyle.Exclamation, Me.Text)
            '                    End If
            '                End If
            '            End If
            '        Catch oErr As Exception
            '            MsgBox("Order Laboratorium gagal", MsgBoxStyle.Exclamation, Me.Text)
            '        End Try
            '    End If

            '    If skodeorderRad <> "" Then
            '        Try
            '            Dim dscekdata = oOrderPenunjang.GetDataKDCPPTRADAWALBYKODE(skodeorderRad)
            '            If dscekdata IsNot Nothing Then
            '                ' ***** DETIL *****
            '                Dim arrDetail = oOrderPenunjang.GetStructureDetailAwalList
            '                For Each xloop In oOrderPenunjang.GetDataDetailLisRadAwal(skodeorderRad).OrderBy(Function(x) x.SEQ)
            '                    Dim dsDetail = oOrderPenunjang.GetStructureDetailAwal
            '                    With dsDetail
            '                        .KODE = skodeorderRad
            '                        .SEQ = xloop.SEQ
            '                        .DATECREATED = xloop.DATECREATED
            '                        .DATEUPDATED = xloop.DATEUPDATED
            '                        .DATE = xloop.DATE
            '                        .KDCPPT = ds.KDCPPT
            '                        .KDREG = lblRegister.Text
            '                        .KDCUSTOMER = ds.KDCUSTOMER
            '                        .PASIEN = ds.NAMAPASIEN
            '                        .TUJUAN = lblRuangan.Text
            '                        .DOKTER = xloop.DOKTER
            '                        .KATEGORI = "RADIOLOGI"
            '                        .NAMAORDER = xloop.NAMAORDER
            '                        .DIAGNOSA = String.Join(vbCrLf, listdiagnosa.ToArray)
            '                        .MEMO = "SIMPAN DARI CPPT"
            '                        .ISAPPROVE = False
            '                        .INDIKASIMEDIS = xloop.INDIKASIMEDIS
            '                    End With
            '                    arrDetail.Add(dsDetail)
            '                Next

            '                If arrDetail.Count > 0 Then
            '                    If oOrderPenunjang.UpdateDataAwal(arrDetail) = False Then
            '                        MsgBox("Order Radiolgi gagal", MsgBoxStyle.Exclamation, Me.Text)
            '                    End If
            '                End If

            '            End If
            '        Catch oErr As Exception
            '            MsgBox("Order Radiolgi gagal", MsgBoxStyle.Exclamation, Me.Text)
            '        End Try
            '    End If
            'End If
            Dim dsCPPTAkhir = oCPPT.GetDataCPPT_AkhirDokter(lblRegister.Text)
            If dsCPPTAkhir IsNot Nothing Then
                sCOPYCPPTDOKTERTERAKHIR = dsCPPTAkhir.KDCPPT
            End If

            Try
                If fn_SaveCPPT = True Then
                    If sKDREGRAWATJALAN <> "" Then
                        If cboProfesi.Text <> "DOKTER" Then
                            Dim oWaktuIGD As New Pendaftaran2.clsPendaftaranWaktuIGD

                            Dim dsWaktuIGD = oWaktuIGD.GetStructureHeader
                            With dsWaktuIGD
                                .KDREG = sKDREGRAWATJALAN
                                .WAKTU = ds.DATE
                                .DESCRIPTION = ""
                            End With

                            Dim dsWaktuIGDCek = oWaktuIGD.GetData(sKDREGRAWATJALAN)

                            If dsWaktuIGDCek IsNot Nothing Then
                                'oWaktuIGD.UpdateData(dsWaktuIGD)
                            Else
                                oWaktuIGD.InsertData(dsWaktuIGD)

                                Dim oS_DIGITAL_IGD_01 As New Transaksi.clsDigital_IGD_01

                                Dim dsAsesmenAwalIgd = oS_DIGITAL_IGD_01.GetData(sKDREGRAWATJALAN)

                                If dsAsesmenAwalIgd IsNot Nothing Then
                                    oS_DIGITAL_IGD_01.UpdateWaktuSaatPulang(sKDREGRAWATJALAN, ds.DATE)
                                End If
                            End If
                        End If
                    End If

                End If
            Catch oErr As Exception
                MsgBox("Simpan Data Waktu IGD: " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            End Try

            If fn_SaveCPPT = True Then
                If sISDOKTER = True Then
                    Dim adaresep As Boolean = False

                    For i As Integer = 0 To grvDetailResep.RowCount - 2
                        adaresep = True
                    Next
                    If adaresep = True Then
                        fn_SaveResepdiCPPT(ds.KDCPPT, ds.DATE, ds.ASSEMENT)
                    End If
                End If
            End If
        Catch oErr As Exception
            MsgBox("Simpan Data : " & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            fn_SaveCPPT = False
        End Try
    End Function
    Private Sub fn_SimpanCPPTSelesai(ByVal TANGGAL As String, ByVal KDCPPT As String, ByVal KDREG As String, ByVal KDDCOTOR As String, ByVal DESCRIPTION As String)
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

            SQL = "INSERT INTO I_TRACKING_CPPT (TANGGAL, KDCPPT, KDREG, KDDOCTOR, DESCRIPTION) "
            SQL &= "VALUES ('" & TANGGAL & "', '" & KDCPPT & "', '" & KDREG & "', '" & KDDCOTOR & "','" & DESCRIPTION & "')"

            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "INSERT_I_TRACKING_CPPT")

            If oConn.State = ConnectionState.Open Then
                oConn.Close()
            End If
        Catch oErr As Exception
            MsgBox("Insert I_TRACKING_CPPT : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Function fn_SaveResepdiCPPT(ByVal kodesimpan As String, ByVal tanggalcppt As DateTime, ByVal diagnosa As String) As Boolean
        Try
            ' ***** HEADER *****
            Dim ds = oReqRecipeRawatInap.GetStructureHeader
            With ds
                Try
                    .DATECREATED = oReqRecipeRawatInap.GetData(kodesimpan).DATECREATED
                Catch ex As Exception
                    .DATECREATED = Now
                End Try
                .DATEUPDATED = Now
                .DATE = tanggalcppt
                .KDREQRECIPE_RI = kodesimpan
                .NOANTRIAN = String.Empty
                .KDPENDAFTARAN = lblRegister.Text
                .KDWAREHOUSE = 1
                .ALERGIOBAT = IIf(txtALERGI_TEXT.Text.ToString.Trim = String.Empty, "-", txtALERGI_TEXT.Text)
                .BERATBADAN = txtOBJEKTIF_BERATBADAN.Text
                Try
                    .DESCRIPTION = oReqRecipeRawatInap.GetData(kodesimpan).DESCRIPTION
                Catch ex As Exception
                    .DESCRIPTION = tanggalcppt.ToString("ddMMyyyy")
                End Try
                Try
                    .ISCHEKED = oReqRecipeRawatInap.GetData(kodesimpan).ISCHEKED
                Catch ex As Exception
                    .ISCHEKED = False
                End Try
                Try
                    .KONFIRMASIRESEP = oReqRecipeRawatInap.GetData(kodesimpan).KONFIRMASIRESEP
                Catch ex As Exception
                    .KONFIRMASIRESEP = "Resep Belum Diterima"
                End Try
                .SUBTOTAL_RINCIAN = CDec(0)
                .SUBTOTAL_PAKET = CDec(0)
                .SUBTOTAL_KRONIS = CDec(0)

                Try
                    .GRANDTOTAL = CDec(lblTOTALRESEPDICPPT.Text)
                Catch ex As Exception
                    .GRANDTOTAL = CDec(0)
                End Try


                .NOIDUSER = sUserID

                .ISPERUBAHANRESEP = chkALERGI_TIDAK.Checked
                .ISAPPROVAL = chkALERGI_YA.Checked
                Try
                    .NOIDUSER_FARMASI = oReqRecipeRawatInap.GetData(kodesimpan).NOIDUSER_FARMASI
                Catch ex As Exception
                    .NOIDUSER_FARMASI = String.Empty
                End Try
                Try
                    .DESCRIPTION_PERUBAHAN = oReqRecipeRawatInap.GetData(kodesimpan).DESCRIPTION_PERUBAHAN
                Catch ex As Exception
                    .DESCRIPTION_PERUBAHAN = ""
                End Try

                .PENJAMIN = lblPenjamin.Text
                .KDCUSTOMER = lblNoRM.Text
                .PASIEN = lblNamaPasien.Text
                .ALAMAT = IIf(sKDDOCTOR_INPUT = "", sKDDOCTOR_DPJPUTAMA, sKDDOCTOR_INPUT)
                .DOKTER = frmReqRecipeRawatInap.fn_LoadDokter(IIf(sKDDOCTOR_INPUT = "", sKDDOCTOR_DPJPUTAMA, sKDDOCTOR_INPUT))
                .TUJUAN = lblRuangan.Text
                .DIAGNOSA = diagnosa

                Dim oDoctor As New Reference.clsDoctor
                Dim dsDoctor = oDoctor.GetData(grdDPJP.EditValue)
                If dsDoctor IsNot Nothing Then
                    .SIPDOKTER = dsDoctor.SIP
                Else
                    .SIPDOKTER = ""
                End If
                Try
                    .KDRECIPE = oReqRecipeRawatInap.GetData(kodesimpan).KDRECIPE
                Catch ex As Exception
                    .KDRECIPE = ""
                End Try

                .ISPULANG = False
            End With

            ' ***** DETIL *****
            Dim arrDetail = oReqRecipeRawatInap.GetStructureDetailList
            For i As Integer = 0 To grvDetailResep.RowCount - 2
                Dim dsDetail = oReqRecipeRawatInap.GetStructureDetail
                With dsDetail
                    .SEQ = i
                    .KDREQRECIPE_RI = ds.KDREQRECIPE_RI
                    .NAMAOBAT = frmReqRecipeRawatInap.fn_LoadITEM(grvDetailResep.GetRowCellValue(i, colKDITEM))
                    .SATUAN = frmReqRecipeRawatInap.fn_LoadUOMDESCRIPTION(grvDetailResep.GetRowCellValue(i, colKDUOM))
                    .SIGNA = IIf(String.IsNullOrEmpty(grvDetailResep.GetRowCellValue(i, colKDSIGNA)), "", frmReqRecipeRawatInap.fn_LoadSIGNA(grvDetailResep.GetRowCellValue(i, colKDSIGNA)))
                    .CARAPAKAI = IIf(String.IsNullOrEmpty(grvDetailResep.GetRowCellValue(i, colKDCARAPAKAI)), "", frmReqRecipeRawatInap.fn_LoadCARAPAKAI(grvDetailResep.GetRowCellValue(i, colKDCARAPAKAI)))
                    .QTY = CDec(grvDetailResep.GetRowCellValue(i, colQTY))
                    .PRICE = CDec(grvDetailResep.GetRowCellValue(i, colPRICE))
                    .GRANDTOTAL = CDec(grvDetailResep.GetRowCellValue(i, colGRANDTOTAL))
                    .ROMAWI = frmReqRecipeRawatInap.IntegerToRoman(CInt(grvDetailResep.GetRowCellValue(i, colQTY)))
                    .REMARKS_DOKTER = IIf(String.IsNullOrEmpty(grvDetailResep.GetRowCellValue(i, colREMARKS_DOKTER)), "", grvDetailResep.GetRowCellValue(i, colREMARKS_DOKTER))
                    .KDITEM = grvDetailResep.GetRowCellValue(i, colKDITEM)
                    .KDUOM = grvDetailResep.GetRowCellValue(i, colKDUOM)
                    .KDSIGNA = IIf(String.IsNullOrEmpty(grvDetailResep.GetRowCellValue(i, colKDSIGNA)), "1286", grvDetailResep.GetRowCellValue(i, colKDSIGNA))
                    .KDCARAPAKAI = IIf(String.IsNullOrEmpty(grvDetailResep.GetRowCellValue(i, colKDCARAPAKAI)), "10", grvDetailResep.GetRowCellValue(i, colKDCARAPAKAI))
                    .QTY_PERUBAHAN = grvDetailResep.GetRowCellValue(i, colQTY)
                    Try
                        .REMARKS_FARMASI = oReqRecipeRawatInap.GetDataObatBySeq(ds.KDREQRECIPE_RI, i).REMARKS_FARMASI
                    Catch ex As Exception
                        .REMARKS_FARMASI = ""
                    End Try
                    .DOSIS = IIf(String.IsNullOrEmpty(grvDetailResep.GetRowCellValue(i, colDOSIS)), "", grvDetailResep.GetRowCellValue(i, colDOSIS))
                    .STATUSKIRIMRESEP = IIf(String.IsNullOrEmpty(grvDetailResep.GetRowCellValue(i, colSTATUSKIRIMRESEP)), "RESEP", grvDetailResep.GetRowCellValue(i, colSTATUSKIRIMRESEP))
                End With

                If grvDetailResep.GetRowCellValue(i, colKDITEM) IsNot Nothing Then
                    arrDetail.Add(dsDetail)
                End If
            Next

            Dim dscek = oReqRecipeRawatInap.GetData(kodesimpan)

            If dscek Is Nothing Then
                Try
                    fn_SaveResepdiCPPT = oReqRecipeRawatInap.InsertData(ds, arrDetail)
                Catch oErr As Exception
                    MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            Else
                Try
                    If dscek.KONFIRMASIRESEP <> "Resep Belum Diterima" Then
                        MsgBox("Resep Sudah " & dscek.KONFIRMASIRESEP & " Silahkan kordinasi dengan farmasi", MsgBoxStyle.Exclamation, Me.Text)
                    Else
                        fn_SaveResepdiCPPT = oReqRecipeRawatInap.UpdateData(ds, arrDetail)
                    End If
                Catch oErr As Exception
                    MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            End If

        Catch oErr As Exception
            MsgBox("Simpan Resep" & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            fn_SaveResepdiCPPT = False
        End Try
    End Function
    Private Sub chkALERGI_TIDAK_CheckedChanged(sender As Object, e As EventArgs) Handles chkALERGI_TIDAK.CheckedChanged
        If chkALERGI_TIDAK.Checked = True Then
            txtALERGI_TEXT.ResetText()
            chkALERGI_YA.Checked = False
        End If
    End Sub
    Private Sub chkALERGI_YA_CheckedChanged(sender As Object, e As EventArgs) Handles chkALERGI_YA.CheckedChanged
        If chkALERGI_YA.Checked = True Then
            chkALERGI_TIDAK.Checked = False
        End If
    End Sub
    Private Sub btnCopyAsesmenMedisIGD_Click(sender As Object, e As EventArgs) Handles btnCopyAsesmenMedisIGD.Click
        fn_LoadAsesmenAwalMedisIGDdiCPPT(sKDREGRAWATJALAN)
    End Sub
    Private Sub btnReloadAsesmenAwalMedis_Click(sender As Object, e As EventArgs) Handles btnReloadAsesmenAwalMedis.Click
        fn_LoadAsemenMedisdiCPPT(lblRegister.Text)
    End Sub
    Private Sub btnReloadCPPT_Click(sender As Object, e As EventArgs) Handles btnReloadCPPT.Click
        Dim frmListTemplate As New frmListTemplate
        Try
            sKODECPPTCOPY = String.Empty

            frmListTemplate.fn_LoadKategori(2, lblNoRM.Text)
            frmListTemplate.ShowDialog(Me)

            If sKODECPPTCOPY <> "" Then
                lblCOPY.Text = "Copy CPPT dari Reload CPPT"
                fn_CopyCPPTAkhirdiCPPT(sKODECPPTCOPY)
            End If
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub btnSBAR_Click(sender As Object, e As EventArgs) Handles btnSBAR.Click
        'Dim frmEMedrekRI_31List As New frmEMedrekRI_31List
        'Try
        '    frmEMedrekRI_31List.fn_LoadMe(lblRegister.Text, lblJenisKelamin.Text, sDOKTERUTAMA, lblNoRM.Text, lblNamaPasien.Text, sTanggalLahir, lblRuangan.Text, deTANGGALMASUK.DateTime)
        '    frmEMedrekRI_31List.ShowDialog(Me)
        '    fn_LoadSecurity()
        'Catch ex As Exception
        '    MsgBox("Load Form Detail : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
        'Finally
        '    If Not frmEMedrekIDG_03 Is Nothing Then frmEMedrekRI_31List.Dispose()
        '    frmEMedrekRI_31List = Nothing

        'End Try

        If lblKODECPPT.Text <> "TAMBAH CPPT/SBAR" Then
            MsgBox("Tidak dapat diganti", MsgBoxStyle.Exclamation, Me.Text)
            Exit Sub
        End If

        fn_ViewCPPTSBAR(btnSBAR.Text)
    End Sub
    Private Sub fn_ViewCPPTSBAR(ByVal Paramater As String)
        If Paramater = "TAMBAH SBAR" Then
            tabCPPT.Text = "SBAR " & Now.ToString("dd-MM-yyyy HH:mm", CultureInfo.InvariantCulture)
            btnSBAR.Text = "TAMBAH CPPT"

            lblSubjekstif.Text = "Situation"
            lblObjektif.Text = "Background"
            lblAsesesment.Text = "Assesment"
            lblPlanning.Text = "Recommendation"
        Else
            tabCPPT.Text = "CPPT " & Now.ToString("dd-MM-yyyy HH:mm", CultureInfo.InvariantCulture)
            btnSBAR.Text = "TAMBAH SBAR"

            lblSubjekstif.Text = "Subjektif"
            lblObjektif.Text = "Objektif"
            lblAsesesment.Text = "Assesment"
            lblPlanning.Text = "Planning"
        End If
    End Sub
    Private Sub btnLoadDataCPPTTemplate_Click(sender As Object, e As EventArgs) Handles btnLoadDataCPPTTemplate.Click
        Dim frmListTemplate As New frmListTemplate
        Try
            sKODETEMPLATE = String.Empty

            frmListTemplate.fn_LoadKategori(0, lblNoRM.Text)
            frmListTemplate.ShowDialog(Me)

            If sKODETEMPLATE <> "" Then
                fn_LoadDataCPPTTemplate(sKODETEMPLATE)
            End If
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub btnSaveAsCPPTTemplate_Click(sender As Object, e As EventArgs) Handles btnSaveAsCPPTTemplate.Click
        sRemarks = String.Empty
        frmJudulTemplate.ShowDialog(Me)

        If sRemarks = "" Then
            MsgBox("Judul Template Masih Kosong", MsgBoxStyle.Exclamation, Me.Text)
        Else
            sKODETEMPLATE = String.Empty
            fn_SaveCPPTTemplate(sKODETEMPLATE)
            sRemarks = String.Empty
        End If
    End Sub
    Private Sub btnSaveCPPTTemplate_Click(sender As Object, e As EventArgs) Handles btnSaveCPPTTemplate.Click
        If sKODETEMPLATE = "" Then
            MsgBox("Load Data Template Belum di pilih", MsgBoxStyle.Exclamation, Me.Text)
        Else
            fn_SaveCPPTTemplate(sKODETEMPLATE)
            'sKODETEMPLATE = String.Empty
        End If
    End Sub
    Private Sub fn_LoadDataCPPTTemplate(ByVal Kode As String)
        Try
            ' ***** HEADER *****
            Dim ds = oCPPTTemplate.GetData(Kode)

            With ds
                cboProfesi.Text = .PROFESI
                If ds.SUBJEKTIF <> "" Then
                    txtSUBJEKTIF.Text = .SUBJEKTIF
                End If
                chkALERGI_TIDAK.Checked = .ALERGI_TIDAK
                chkALERGI_YA.Checked = .ALERGI_YA
                If .ALERGI_TEXT <> "" Then
                    txtALERGI_TEXT.Text = .ALERGI_TEXT
                End If
                If .OBJEKTIF_BERATBADAN <> "" Then
                    txtOBJEKTIF_BERATBADAN.Text = .OBJEKTIF_BERATBADAN
                End If
                If .OBJEKTIF_NADI <> "" Then
                    txtOBJEKTIF_NADI.Text = .OBJEKTIF_NADI
                End If
                If .OBJEKTIF_RESPIRASI <> "" Then
                    txtOBJEKTIF_RESPIRASI.Text = .OBJEKTIF_RESPIRASI
                End If
                If .OBJEKTIF_SATURASIOKSIGEN <> "" Then
                    txtOBJEKTIF_SATURASIOKSIGEN.Text = .OBJEKTIF_SATURASIOKSIGEN
                End If
                If .OBJEKTIF_SUHU <> "" Then
                    txtOBJEKTIF_SUHU.Text = .OBJEKTIF_SUHU
                End If
                If .OBJEKTIF_TEKANANDARAH <> "" Then
                    txtOBJEKTIF_TEKANANDARAH.Text = .OBJEKTIF_TEKANANDARAH
                End If
                If .OBJEKTIF_TINGGIBADAN <> "" Then
                    txtOBJEKTIF_TINGGIBADAN.Text = .OBJEKTIF_TINGGIBADAN
                End If
                If .OBJEKTIF_PEMERIKSAAN <> "" Then
                    txtPEMERIKSAANLAIN.Text = .OBJEKTIF_PEMERIKSAAN
                End If
                'txtDIAGNOOSA.Text = .ASESMENT_KETIK
                If .PLANNING <> "" Then
                    txtPlanning.Text = .PLANNING
                End If
                If .INTRUKSILAIN <> "" Then
                    txtINTRUKSILAIN.Text = .INTRUKSILAIN
                End If
            End With

            BindingSourceDiagnosa_CPPT.DataSource = oCPPTTemplate.GetDataDetailDiagnosa(Kode).OrderBy(Function(x) x.SEQ).ToList()
            grdDIAGNOSA_CPPT.DataSource = BindingSourceDiagnosa_CPPT

            BindingSourceProsedur_CPPT.DataSource = oCPPTTemplate.GetDataDetailProsedur(Kode).OrderBy(Function(x) x.SEQ).ToList()
            grdPROSEDUR_CPPT.DataSource = BindingSourceProsedur_CPPT

        Catch oErr As Exception
            MsgBox("Load List Data CPPT Template : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Function fn_SaveCPPTTemplate(ByVal Kode As String) As Boolean
        Try
            ' ***** HEADER *****

            Dim ds = oCPPTTemplate.GetStructureHeader
            With ds
                Try
                    .DATECREATED = oCPPTTemplate.GetData(Kode).DATECREATED
                Catch ex As Exception
                    .DATECREATED = Now
                End Try
                .DATEUPDATED = Now
                .KDCPPT_TEMPLATE = Kode
                Try
                    .NAMATEMPLATE = oCPPTTemplate.GetData(Kode).NAMATEMPLATE
                Catch ex As Exception
                    .NAMATEMPLATE = sRemarks
                End Try
                .PROFESI = IIf(sISDOKTER = True, "DOKTER", cboProfesi.Text)
                .SUBJEKTIF = txtSUBJEKTIF.Text
                .ALERGI_YA = chkALERGI_YA.Checked
                .ALERGI_TIDAK = chkALERGI_TIDAK.Checked
                .ALERGI_TEXT = txtALERGI_TEXT.Text
                .OBJEKTIF_BERATBADAN = txtOBJEKTIF_BERATBADAN.Text
                .OBJEKTIF_NADI = txtOBJEKTIF_NADI.Text
                .OBJEKTIF_RESPIRASI = txtOBJEKTIF_RESPIRASI.Text
                .OBJEKTIF_SATURASIOKSIGEN = txtOBJEKTIF_SATURASIOKSIGEN.Text
                .OBJEKTIF_SUHU = txtOBJEKTIF_SUHU.Text
                .OBJEKTIF_TEKANANDARAH = txtOBJEKTIF_TEKANANDARAH.Text
                .OBJEKTIF_TINGGIBADAN = txtOBJEKTIF_TINGGIBADAN.Text
                .OBJEKTIF_PEMERIKSAAN = txtPEMERIKSAANLAIN.Text
                .ASESMENT_KETIK = ""
                .PLANNING = txtPlanning.Text
                .INTRUKSILAIN = txtINTRUKSILAIN.Text
                .KDUSER = sUserID
                .ISCHEKED = False
            End With

            'DIAGNOSA
            Dim arrDetailDiagnosa = oCPPTTemplate.GetStructureDetailDiagnosaList
            For i As Integer = 0 To grvDIAGNOSA_CPPT.RowCount - 2
                Dim dsDetail = oCPPTTemplate.GetStructureDetailDiagnosa
                With dsDetail
                    .SEQ = i
                    .KDCPPT_TEMPLATE = ds.KDCPPT_TEMPLATE
                    .KATEGORI = grvDIAGNOSA_CPPT.GetRowCellValue(i, colKATEGORI_CPPT)
                    .NAMADIAGNOSA = grvDIAGNOSA_CPPT.GetRowCellValue(i, colNAMADIAGNOSA_CPPT)
                    .KDDIAGNOSA = grvDIAGNOSA_CPPT.GetRowCellValue(i, colKDDIAGNOSA_CPPT)
                End With
                arrDetailDiagnosa.Add(dsDetail)
            Next
            'PROSEDUR
            Dim arrDetailProsedur = oCPPTTemplate.GetStructureDetailProsedurList
            For i As Integer = 0 To grvPROSEDUR_CPPT.RowCount - 2
                Dim dsDetail = oCPPTTemplate.GetStructureDetailProsedur
                With dsDetail
                    .SEQ = i
                    .KDCPPT_TEMPLATE = ds.KDCPPT_TEMPLATE
                    .KATEGORI = ""
                    .NAMAPROSEDUR = grvPROSEDUR_CPPT.GetRowCellValue(i, colNAMAPROSEDUR_CPPT)
                    .KDPROSEDUR = grvPROSEDUR_CPPT.GetRowCellValue(i, colKDPROSEDUR_CPPT)
                End With
                arrDetailProsedur.Add(dsDetail)
            Next

            If Kode = "" Then
                fn_SaveCPPTTemplate = oCPPTTemplate.InsertData(ds, arrDetailDiagnosa, arrDetailProsedur)
            Else
                fn_SaveCPPTTemplate = oCPPTTemplate.UpdateData(ds, arrDetailDiagnosa, arrDetailProsedur)
            End If

        Catch oErr As Exception
            MsgBox("Simpan Data CPPT Template: " & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            fn_SaveCPPTTemplate = False
        End Try
    End Function
    Private Sub btnRequestResep_Click(sender As Object, e As EventArgs) Handles btnRequestResep.Click
        'Dim frmReqRecipeRawatInap As New frmReqRecipeRawatInap
        'Try
        '    Dim listdiagnosa As New List(Of String)
        '    For i As Integer = 0 To grvDIAGNOSA_CPPT.RowCount - 2
        '        listdiagnosa.Add(grvDIAGNOSA_CPPT.GetRowCellValue(i, colNAMADIAGNOSA_CPPT))
        '    Next

        '    Dim Diagnosalabel As String = String.Join(vbCrLf, listdiagnosa.ToArray)

        '    frmReqRecipeRawatInap.fn_LoaDataPasien(False, sISDOKTER, lblPenjamin.Text, lblRegister.Text, lblNoRM.Text, lblNamaPasien.Text, IIf(sISDOKTER = False, sKDDOCTOR_DPJPUTAMA, sKDDOCTOR_INPUT), lblRuangan.Text, Diagnosalabel, txtOBJEKTIF_BERATBADAN.Text, sKDUSER_PERAWAT)
        '    frmReqRecipeRawatInap.LoadMe(FORM_MODE.FORM_MODE_ADD)
        '    frmReqRecipeRawatInap.ShowDialog(Me)
        'Catch oErr As Exception
        '    MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        'Finally
        '    If Not frmReqRecipeRawatInap Is Nothing Then frmReqRecipeRawatInap.Dispose()
        '    frmReqRecipeRawatInap = Nothing

        '    'fn_LoadResepPerawat()
        '    'Dim list As New List(Of String)
        '    'For Each xloop In oReqRecipeRawatInap.GetDataDetailByHariIni(lblRegister.Text, Now.ToString("ddMMyyyy"))
        '    '    list.Add(xloop.NAMAOBAT & " No " & xloop.ROMAWI & " " & xloop.SATUAN & " " & xloop.SIGNA & " " & xloop.CARAPAKAI & " " & xloop.REMARKS_DOKTER)
        '    'Next

        '    'txtPlanningObat.Text = String.Join(vbCrLf, list.ToArray)

        'End Try
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
            'If sDokter = False Then
            '    SQL &= "AND "
            '    SQL &= "KDKATEGORI_BPJS IN ('15','16', '17') "
            'Else
            '    SQL &= "AND "
            '    SQL &= "KDKATEGORI_BPJS NOT IN ('15','16', '17') "
            'End If

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
    Private Sub ToolStripMenuItem8_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem8.Click
        grvDetailResep.DeleteSelectedRows()
    End Sub
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

        lblTOTALRESEPDICPPT.Text = FormatNumber(sSubTotal, 0)
    End Sub
    Private Sub grvDetailResep_CellValueChanged(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs) Handles grvDetailResep.CellValueChanged
        If e.Column.Name = colKDITEM.Name Then
            Try
                If grvDetailResep.GetFocusedRowCellValue(colKDITEM) IsNot Nothing Then
                    grvDetailResep.SetFocusedRowCellValue(colKDUOM, frmReqRecipeRawatInap.fn_LoadUOMKDUOM(grvDetailResep.GetFocusedRowCellValue(colKDITEM)))
                    grvDetailResep.SetFocusedRowCellValue(colREMARKS_DOKTER, "")
                    grvDetailResep.SetFocusedRowCellValue(colQTY, 1)
                    grvDetailResep.SetFocusedRowCellValue(colSTATUSKIRIMRESEP, "RESEP")
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
            Dim hargajualawal As Decimal = frmReqRecipeRawatInap.fn_LoadUOMKDUOMHARGA(grvDetailResep.GetFocusedRowCellValue(colKDITEM), grvDetailResep.GetFocusedRowCellValue(colKDUOM))
            grvDetailResep.SetFocusedRowCellValue(colPRICE, hargajualawal)
        ElseIf e.Column.Name = colQTY.Name Or e.Column.Name = colPRICE.Name Then
            Dim sSubTotal As Decimal = CDec(grvDetailResep.GetFocusedRowCellValue(colQTY)) * CDec(grvDetailResep.GetFocusedRowCellValue(colPRICE))
            grvDetailResep.SetFocusedRowCellValue(colGRANDTOTAL, sSubTotal)
        End If
    End Sub
#End Region
#Region "RINGKASAN KELUAR"
    Private Sub fn_ChangeFormStateRINGKASANKELUAR()
        If isLoadResume = True Then
            MsgBox("Data Sudah di Reload, Jika ingin mereload kembali silahkan klik tombol Reload Data Terakhir !!!", MsgBoxStyle.Information, Me.Text)
            Exit Sub
        End If

        Dim dsCek = oRINGKASANKELUAR.GetData(lblRegister.Text)
        If dsCek Is Nothing Then
            fn_EmptyMeRINGKASANKELUAR()
        Else
            MsgBox("Ringkasan Keluar sudah disimpan, akan reload data pada saat penyimpanan terakhir Ringkasan keluar, Jika ingin mereload kembali silahkan klik tombol Reload Data Terakhir !!!", MsgBoxStyle.Information, Me.Text)
            fn_LoadDataRINGKASANKELUAR()
        End If

        fn_LoadDataGrouper()

        'If sCOPYKDCPPTPERAWAT <> "" Then
        '    fn_CopyCPPTAkhirdiResume(sCOPYKDCPPTPERAWAT)
        'Else
        '    fn_CopyCPPTAkhirdiResume(sCOPYKDCPPTDOKTER)
        'End If

        isLoadResume = True
    End Sub
    'Private Sub fn_ViewModeRINGKASANKELUAR(ByVal Status As Boolean)
    '    deTANGGALPULANG.Properties.ReadOnly = Status
    '    txtKeluhanUtama.Properties.ReadOnly = Status
    '    txtAnamnesa.Properties.ReadOnly = Status
    '    txtKomorbiditasLain.Properties.ReadOnly = Status
    '    txtPemeriksaanFisik.Properties.ReadOnly = Status
    '    txtHasilPemeriksaan.Properties.ReadOnly = Status
    '    txtIndikasiRawat.Properties.ReadOnly = Status
    '    txtTerapi.Properties.ReadOnly = Status
    '    cboKeadaanUmum.Properties.ReadOnly = Status
    '    ComboBoxEdit1.Properties.ReadOnly = Status
    '    TextBox12.Properties.ReadOnly = Status
    '    TextBox15.Properties.ReadOnly = Status
    '    TextBox16.Properties.ReadOnly = Status
    '    TextBox13.Properties.ReadOnly = Status
    '    TextBox14.Properties.ReadOnly = Status
    '    TextBox18.Properties.ReadOnly = Status
    '    txtCatatanPenting.Properties.ReadOnly = Status
    '    txtSebabKematian.Properties.ReadOnly = Status
    '    chkKEADAANSAATKELUAR_1.Properties.ReadOnly = Status
    '    chkKEADAANSAATKELUAR_2.Properties.ReadOnly = Status
    '    chkKEADAANSAATKELUAR_3.Properties.ReadOnly = Status
    '    chkKEADAANSAATKELUAR_4.Properties.ReadOnly = Status
    '    chkKEADAANSAATKELUAR_5.Properties.ReadOnly = Status
    '    CheckEdit26.Properties.ReadOnly = Status
    '    CheckEdit27.Properties.ReadOnly = Status
    '    CheckEdit28.Properties.ReadOnly = Status
    '    CheckEdit29.Properties.ReadOnly = Status
    '    CheckEdit30.Properties.ReadOnly = Status
    '    CheckEdit21.Properties.ReadOnly = Status
    '    CheckEdit22.Properties.ReadOnly = Status
    '    CheckEdit23.Properties.ReadOnly = Status
    '    CheckEdit24.Properties.ReadOnly = Status
    '    TextBox22.Properties.ReadOnly = Status
    '    DateEdit4.Properties.ReadOnly = Status
    '    TextBox23.Properties.ReadOnly = Status
    '    TextBox24.Properties.ReadOnly = Status
    '    CheckEdit25.Properties.ReadOnly = Status
    '    CheckEdit31.Properties.ReadOnly = Status
    '    CheckEdit32.Properties.ReadOnly = Status
    '    txtEdukasidanIntruksi.Properties.ReadOnly = Status
    '    TextBox17.Properties.ReadOnly = Status
    '    txtObatPulang.Properties.ReadOnly = Status
    'End Sub
    Private Sub fn_EmptyMeRINGKASANKELUAR()
        'Dim dsTerakhir = oCPPT.GetDataCPPTRI_Akhir(lblRegister.Text)
        'If dsTerakhir IsNot Nothing Then
        '    deTANGGALPULANG.DateTime = dsTerakhir.DATE
        'Else
        '    deTANGGALPULANG.DateTime = Now
        'End If
        txtKeluhanUtama.ResetText()
        txtAnamnesa.ResetText()
        txtKomorbiditasLain.ResetText()
        txtPemeriksaanFisik.ResetText()
        txtHasilPemeriksaan.ResetText()
        txtIndikasiRawat.ResetText()
        txtTerapi.ResetText()
        txtKeadaanUmum.ResetText()
        txtKesadaran.ResetText()
        TextBox12.ResetText()
        TextBox15.ResetText()
        TextBox16.ResetText()
        TextBox13.Text = "0"
        TextBox14.Text = "0"
        TextBox18.ResetText()
        txtCatatanPenting.ResetText()
        txtSebabKematian.ResetText()
        chkKEADAANSAATKELUAR_1.Checked = False
        chkKEADAANSAATKELUAR_2.Checked = False
        chkKEADAANSAATKELUAR_3.Checked = False
        chkKEADAANSAATKELUAR_4.Checked = False
        chkKEADAANSAATKELUAR_5.Checked = False
        CheckEdit26.Checked = False
        CheckEdit27.Checked = False
        CheckEdit28.Checked = False
        CheckEdit29.Checked = False
        CheckEdit30.Checked = False
        CheckEdit21.Checked = False
        CheckEdit22.Checked = False
        CheckEdit23.Checked = False
        CheckEdit24.Checked = False
        ComboBoxEdit1.Text = "0"
        DateEdit4.DateTime = Now
        TextBox23.ResetText()
        TextBox24.ResetText()
        CheckEdit25.Checked = False
        CheckEdit31.Checked = False
        CheckEdit32.Checked = False
        txtEdukasidanIntruksi.ResetText()
        TextBox17.ResetText()
        TextBox2.Text = "-"
        'txtObatPulang.ResetText()
        txtBeratBadanResume.Text = "0"

        If sCOPYCPPTDOKTERTERAKHIR <> "" Then
            fn_CopyCPPTAkhirdiResumeDokter(sCOPYCPPTDOKTERTERAKHIR)
        End If
    End Sub
    'Private Sub fn_LoadAsemenMedisdiResume(ByVal Register As String)
    '    Try
    '        Dim ds = oDigital_DischargePlanning.GetData(Register)
    '        If ds IsNot Nothing Then
    '            txtKeluhanUtama.Text = ds.ANAMNESIS_01
    '            txtAnamnesa.Text = ds.ANAMNESIS_02

    '            Dim Pemeriksaan As String = String.Empty

    '            If ds.ANAMNESIS_15_2_CHEK = True Then
    '                Pemeriksaan = "Kepala Tidak Normal" & IIf(ds.ANAMNESIS_15 <> "", " (" & ds.ANAMNESIS_15 & ")", "")
    '            Else
    '                Pemeriksaan = "Kepala Normal" & IIf(ds.ANAMNESIS_15 <> "", " (" & ds.ANAMNESIS_15 & ")", "")
    '            End If

    '            If ds.ANAMNESIS_16_2_CHEK = True Then
    '                Pemeriksaan = Pemeriksaan & vbCrLf & "Mata Tidak Normal" & IIf(ds.ANAMNESIS_16 <> "", " (" & ds.ANAMNESIS_16 & ")", "")
    '            Else
    '                Pemeriksaan = Pemeriksaan & vbCrLf & "Mata Normal" & IIf(ds.ANAMNESIS_16 <> "", " (" & ds.ANAMNESIS_16 & ")", "")
    '            End If

    '            If ds.ANAMNESIS_17_2_CHEK = True Then
    '                Pemeriksaan = Pemeriksaan & vbCrLf & "Leher Tidak Normal" & IIf(ds.ANAMNESIS_17 <> "", " (" & ds.ANAMNESIS_17 & ")", "")
    '            Else
    '                Pemeriksaan = Pemeriksaan & vbCrLf & "Leher Normal" & IIf(ds.ANAMNESIS_17 <> "", " (" & ds.ANAMNESIS_17 & ")", "")
    '            End If

    '            If ds.ANAMNESIS_18_2_CHEK = True Then
    '                Pemeriksaan = Pemeriksaan & vbCrLf & "Leher Tidak Normal" & IIf(ds.ANAMNESIS_18 <> "", " (" & ds.ANAMNESIS_18 & ")", "")
    '            Else
    '                Pemeriksaan = Pemeriksaan & vbCrLf & "Leher Normal" & IIf(ds.ANAMNESIS_18 <> "", " (" & ds.ANAMNESIS_18 & ")", "")
    '            End If

    '            If ds.ANAMNESIS_19_2_CHEK = True Then
    '                Pemeriksaan = Pemeriksaan & vbCrLf & "Leher Tidak Normal" & IIf(ds.ANAMNESIS_19 <> "", " (" & ds.ANAMNESIS_19 & ")", "")
    '            Else
    '                Pemeriksaan = Pemeriksaan & vbCrLf & "Leher Normal" & IIf(ds.ANAMNESIS_19 <> "", " (" & ds.ANAMNESIS_19 & ")", "")
    '            End If

    '            If ds.ANAMNESIS_20_2_CHEK = True Then
    '                Pemeriksaan = Pemeriksaan & vbCrLf & "Leher Tidak Normal" & IIf(ds.ANAMNESIS_20 <> "", " (" & ds.ANAMNESIS_20 & ")", "")
    '            Else
    '                Pemeriksaan = Pemeriksaan & vbCrLf & "Leher Normal" & IIf(ds.ANAMNESIS_20 <> "", " (" & ds.ANAMNESIS_20 & ")", "")
    '            End If

    '            txtPemeriksaanFisik.Text = IIf(ds.ANAMNESIS_08 = "", "", "BB: " & ds.ANAMNESIS_08 & " Kg ") & IIf(ds.ANAMNESIS_09, "", "TB: " & ds.ANAMNESIS_09 & " cm ") & IIf(ds.ANAMNESIS_11, "", "Nadi: " & ds.ANAMNESIS_11 & " x/mnt ") & IIf(ds.ANAMNESIS_12, "", "Suhu: " & ds.ANAMNESIS_12 & " mmHg ") & IIf(ds.ANAMNESIS_13, "", "Pernapasan: " & ds.ANAMNESIS_13 & " X / mnt") _
    '                                        & vbCrLf & Pemeriksaan & IIf(ds.ANAMNESIS_29 <> "", "", "Genetalia: " & vbCrLf & ds.ANAMNESIS_29) & IIf(ds.ANAMNESIS_30 <> "", "", "Ekstremitas: " & vbCrLf & ds.ANAMNESIS_30) & IIf(ds.ANAMNESIS_31 <> "", "", "Kulit: " & vbCrLf & ds.ANAMNESIS_31)


    '            txtTerapi.Text = ds.ANAMNESIS_36
    '            txtKeadaanUmum.Text = ds.ANAMNESIS_05
    '            txtKesadaran.Text = ds.ANAMNESIS_06

    '            Dim oS_DIGITAL_IGD_01 As New Transaksi.clsDigital_IGD_01

    '            Dim dsIGd = oS_DIGITAL_IGD_01.GetData(sKDREGRAWATJALAN)
    '            If dsIGd IsNot Nothing Then
    '                TextBox12.Text = dsIGd.GCS
    '            End If

    '            BindingSourceDiagnosa.DataSource = oDigital_DischargePlanning.GetDataDetailDiagnosa(lblRegister.Text).OrderBy(Function(x) x.SEQ).ToList()
    '            grdDIADNOSA.DataSource = BindingSourceDiagnosa
    '        Else
    '            'MsgBox("Belum Ada Asesmen Medis", MsgBoxStyle.Exclamation, Me.Text)
    '        End If
    '    Catch oErr As Exception
    '        MsgBox("Form Browse Load CPPT" & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
    '    End Try
    'End Sub
    Private Sub fn_CopyCPPTAkhirdiResumeDokter(ByVal Parameter As String)
        Try
            ' ***** HEADER *****
            Dim ds = oCPPT.GetData(Parameter)
            Dim dsTambahan = oCPPT.GetDataLainnyaTambah(Parameter)

            Dim dsAsesmen = oDigital_DischargePlanning.GetData(lblRegister.Text)

            If dsAsesmen IsNot Nothing Then
                txtKeluhanUtama.Text = dsAsesmen.ANAMNESIS_01
                txtAnamnesa.Text = dsAsesmen.ANAMNESIS_02
                txtTerapi.Text = dsAsesmen.ANAMNESIS_36 & vbCrLf & dsAsesmen.ANAMNESIS_37
                txtKomorbiditasLain.Text = dsAsesmen.ANAMNESIS_03

                Dim listPemeriksaanFisik As New List(Of String)

                If dsAsesmen.ANAMNESIS_08 <> "" Then
                    listPemeriksaanFisik.Add("Berat Badan : " & dsAsesmen.ANAMNESIS_08 & " Kg")
                End If
                If dsAsesmen.ANAMNESIS_12 <> "" Then
                    listPemeriksaanFisik.Add("Tensi : " & dsAsesmen.ANAMNESIS_12 & " mmHg")
                End If
                If dsAsesmen.ANAMNESIS_11 <> "" Then
                    listPemeriksaanFisik.Add("Nadi : " & dsAsesmen.ANAMNESIS_11 & " x/mnt")
                End If
                If dsAsesmen.ANAMNESIS_14 <> "" Then
                    listPemeriksaanFisik.Add("Pernapasan : " & dsAsesmen.ANAMNESIS_14 & " x/mnt")
                End If
                If dsAsesmen.ANAMNESIS_13 <> "" Then
                    listPemeriksaanFisik.Add("Suhu : " & dsAsesmen.ANAMNESIS_13 & " oC")
                End If
                If dsAsesmen.ANAMNESIS_09 <> "" Then
                    listPemeriksaanFisik.Add("Tinggi Badan : " & dsAsesmen.ANAMNESIS_09 & " Cm")
                End If
                If dsAsesmen.ANAMNESIS_10 <> "" Then
                    listPemeriksaanFisik.Add("Gizi : " & dsAsesmen.ANAMNESIS_10)
                End If
                If dsAsesmen.ANAMNESIS_22 <> "" Then
                    listPemeriksaanFisik.Add("SpO2 : " & dsAsesmen.ANAMNESIS_22 & "% ")
                End If
                If dsAsesmen.ANAMNESIS_05 <> "" Then
                    listPemeriksaanFisik.Add("Keadaan Umum : " & dsAsesmen.ANAMNESIS_05)
                End If
                If dsAsesmen.ANAMNESIS_06 <> "" Then
                    listPemeriksaanFisik.Add("Kesadaran : " & dsAsesmen.ANAMNESIS_06)
                End If

                If dsAsesmen.ANAMNESIS_15_2_CHEK = True Then
                    If dsAsesmen.ANAMNESIS_15 <> "" Then
                        listPemeriksaanFisik.Add("Kepala " & dsAsesmen.ANAMNESIS_15)
                    End If
                End If

                If dsAsesmen.ANAMNESIS_16_2_CHEK = True Then
                    If dsAsesmen.ANAMNESIS_16 <> "" Then
                        listPemeriksaanFisik.Add("Mata " & dsAsesmen.ANAMNESIS_16)
                    End If
                End If

                If dsAsesmen.ANAMNESIS_17_2_CHEK = True Then
                    If dsAsesmen.ANAMNESIS_17 <> "" Then
                        listPemeriksaanFisik.Add("Leher " & dsAsesmen.ANAMNESIS_17)
                    End If
                End If

                If dsAsesmen.ANAMNESIS_18_2_CHEK = True Then
                    If dsAsesmen.ANAMNESIS_18 <> "" Then
                        listPemeriksaanFisik.Add("Dada " & dsAsesmen.ANAMNESIS_18)
                    End If
                End If

                If dsAsesmen.ANAMNESIS_19_2_CHEK = True Then
                    If dsAsesmen.ANAMNESIS_19 <> "" Then
                        listPemeriksaanFisik.Add("Perut " & dsAsesmen.ANAMNESIS_19)
                    End If
                End If

                If dsAsesmen.ANAMNESIS_20_2_CHEK = True Then
                    If dsAsesmen.ANAMNESIS_20 <> "" Then
                        listPemeriksaanFisik.Add("Alat Gerak " & dsAsesmen.ANAMNESIS_20)
                    End If
                End If

                If dsAsesmen.ANAMNESIS_29 <> "" Then
                    listPemeriksaanFisik.Add("Genetalia : " & dsAsesmen.ANAMNESIS_29)
                End If
                If dsAsesmen.ANAMNESIS_30 <> "" Then
                    listPemeriksaanFisik.Add("Ekstremitas : " & dsAsesmen.ANAMNESIS_30)
                End If
                If dsAsesmen.ANAMNESIS_31 <> "" Then
                    listPemeriksaanFisik.Add("Kulit : " & dsAsesmen.ANAMNESIS_31)
                End If
                If dsAsesmen.ANAMNESIS_32 <> "" Then
                    listPemeriksaanFisik.Add("status lokalis : " & dsAsesmen.ANAMNESIS_32)
                End If


                txtPemeriksaanFisik.Text = String.Join(vbCrLf, listPemeriksaanFisik.ToArray)

                txtIndikasiRawat.Text = dsAsesmen.ANAMNESIS_28
            Else
                MsgBox("Asesmen Medis Belum di Input!", MsgBoxStyle.Information, Me.Text)
            End If

            If ds Is Nothing Then
                MsgBox("CPPT Belum di Input", MsgBoxStyle.Information, Me.Text)
                Exit Sub
            Else
                'txtObatPulang.Text = ds.PLANNING
                ''If txtTerapi.Text = "" Then
                ''    txtTerapi.Text = ds.PLANNING
                ''End If

                'If txtAnamnesa.Text = "" Then
                '    If dsAsesmen Is Nothing Then
                '        txtAnamnesa.Text = ds.SUBJEKTIF
                '    End If
                'End If
                'If txtPemeriksaanFisik.Text = "" Then
                '    txtPemeriksaanFisik.Text = ds.OBJEKTIF
                'End If
            End If

            If dsTambahan IsNot Nothing Then
                txtTerapi.Text = dsTambahan.CATATAN_1
                txtHasilPemeriksaan.Text = dsTambahan.CATATAN_5
                txtObatPulang.Text = dsTambahan.CATATAN_6
                txtEdukasidanIntruksi.Text = dsTambahan.CATATAN_7
            End If

            Dim dsLainnya = oCPPT.GetDataLainnya(Parameter)

            With dsLainnya
                TextBox15.Text = .OBJEKTIF_NADI
                TextBox18.Text = .OBJEKTIF_RESPIRASI
                TextBox17.Text = .OBJEKTIF_SATURASIOKSIGEN
                TextBox16.Text = .OBJEKTIF_SUHU
                txtBeratBadanResume.Text = .OBJEKTIF_BERATBADAN

                Try
                    TextBox13.Text = (Microsoft.VisualBasic.Left(.OBJEKTIF_TEKANANDARAH, 3)).Replace("/", "")
                Catch ex As Exception

                End Try
                Try
                    TextBox14.Text = (Microsoft.VisualBasic.Right(.OBJEKTIF_TEKANANDARAH, 3)).Replace("/", "")
                Catch ex As Exception

                End Try
            End With

            grvDIAGNOSA.OptionsSelection.MultiSelect = True
            grvDIAGNOSA.SelectAll()
            grvDIAGNOSA.DeleteSelectedRows()
            grvDIAGNOSA.OptionsSelection.MultiSelect = False

            grvPROSEDUR.OptionsSelection.MultiSelect = True
            grvPROSEDUR.SelectAll()
            grvPROSEDUR.DeleteSelectedRows()
            grvPROSEDUR.OptionsSelection.MultiSelect = False

            For Each xloop In oCPPT.GetDataDetailDiagnosa(ds.KDCPPT)
                grvDIAGNOSA.Focus()
                grvDIAGNOSA.AddNewRow()
                grvDIAGNOSA.SetFocusedRowCellValue(colKATEGORI, xloop.KATEGORI)
                grvDIAGNOSA.SetFocusedRowCellValue(colKDDIAGNOSA, xloop.KDDIAGNOSA)
                grvDIAGNOSA.SetFocusedRowCellValue(colNAMADIAGNOSA, xloop.NAMADIAGNOSA)
                grvDIAGNOSA.UpdateCurrentRow()
            Next

            For Each xloop In oCPPT.GetDataDetailProsedur(ds.KDCPPT)
                grvPROSEDUR.Focus()
                grvPROSEDUR.AddNewRow()
                grvPROSEDUR.SetFocusedRowCellValue(colKDPROSEDUR, xloop.KDPROSEDUR)
                grvPROSEDUR.SetFocusedRowCellValue(colNAMA, xloop.NAMAPROSEDUR)
                grvPROSEDUR.UpdateCurrentRow()
            Next

        Catch oErr As Exception
            MsgBox("Load List Data : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadDataRINGKASANKELUAR()
        Try
            ' ***** HEADER *****
            Dim ds = oRINGKASANKELUAR.GetData(lblRegister.Text)
            'lblRuangan
            With ds
                'deTANGGALMASUK.DateTime = .TANGGALMASUK
                deTANGGALPULANG.DateTime = .TANGGALPULANG
                txtKeluhanUtama.Text = .KELUHANUTAMA
                txtAnamnesa.Text = .ANAMNESA
                txtKomorbiditasLain.Text = .KOMORBIDITASLAIN
                txtPemeriksaanFisik.Text = .PEMERIKSAANFISIK
                txtHasilPemeriksaan.Text = .HASILPEMERIKSAAN
                txtIndikasiRawat.Text = .INDIKASIRAWAT
                txtTerapi.Text = .TERAPI
                txtKeadaanUmum.Text = .KEADAANUMUM
                txtKesadaran.Text = .KESADARAN
                TextBox12.Text = .GCS
                TextBox15.Text = .TEKANANDARAH
                TextBox16.Text = .SUHU
                TextBox13.Text = .NADI_1
                TextBox14.Text = .NADI_2
                TextBox18.Text = .FREKUENSINAPAS
                txtCatatanPenting.Text = .CATATANPENTING
                txtSebabKematian.Text = .SEBABKEMATIAN
                chkKEADAANSAATKELUAR_1.Checked = .KEADAANKELUARRS_1
                chkKEADAANSAATKELUAR_2.Checked = .KEADAANKELUARRS_2
                chkKEADAANSAATKELUAR_3.Checked = .KEADAANKELUARRS_3
                chkKEADAANSAATKELUAR_4.Checked = .KEADAANKELUARRS_4
                chkKEADAANSAATKELUAR_5.Checked = .KEADAANKELUARRS_5
                CheckEdit26.Checked = .CARAKELUAR_1
                CheckEdit27.Checked = .CARAKELUAR_2
                CheckEdit28.Checked = .CARAKELUAR_3
                CheckEdit29.Checked = .CARAKELUAR_4
                CheckEdit30.Checked = .CARAKELUAR_5
                CheckEdit21.Checked = .KONTROL_1
                CheckEdit22.Checked = .KONTROL_2
                CheckEdit23.Checked = .KONTROL_3
                CheckEdit24.Checked = .KONTROL_4
                ComboBoxEdit1.Text = .KONTROL_HARI
                DateEdit4.DateTime = .TANGGALKONTROL
                TextBox23.Text = .POLIKLINIK
                TextBox24.Text = .INSTITUSI
                CheckEdit25.Checked = .OBATPULANG_1
                CheckEdit31.Checked = .OBATPULANG_2
                CheckEdit32.Checked = .OBATPULANG_3
                txtEdukasidanIntruksi.Text = .EDUKASI
                txtObatPulang.Text = .OBATPULANG
                TextBox1.Text = FormatNumber(.TARIF_GROUPER, 0)
                TextBox17.Text = .SATURASI
                TextBox2.Text = .HASIL_GROUPER
                txtBeratBadanResume.Text = .BERATBADAN
                lblRuangan.Text = .RUANGRAWAT


                BindingSourceDiagnosa.DataSource = oRINGKASANKELUAR.GetDataDetailDiagnosa(lblRegister.Text).OrderBy(Function(x) x.SEQ).ToList()
                grdDIADNOSA.DataSource = BindingSourceDiagnosa

                BindingSourceProsedur.DataSource = oRINGKASANKELUAR.GetDataDetail_Prosedur(lblRegister.Text).OrderBy(Function(x) x.SEQ).ToList()
                grdPROSEDUR.DataSource = BindingSourceProsedur

            End With

        Catch oErr As Exception
            MsgBox("Load List Data : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Function fn_ValidateRINGKASANKELUAR(ByVal Finish As Boolean) As Boolean
        Try
            fn_ValidateRINGKASANKELUAR = False

            If lblRegister.Text = String.Empty Then
                MsgBox("Dibutuhkan Register", MsgBoxStyle.Exclamation, Me.Text)
                'txtRegister.Focus()
                fn_ValidateRINGKASANKELUAR = False
                Exit Function
            End If

            'If sCasemix = False Then
            '    Dim Pesan As String = fn_Cek(2, "RESUME MEDIS", deTANGGALPULANG.DateTime)

            '    If Pesan <> "" Then
            '        MsgBox(Pesan, MsgBoxStyle.Information, Me.Text)
            '        fn_ValidateRINGKASANKELUAR = False
            '        Exit Function
            '    End If
            'End If
            'If lblTandaAsesmen.Text <> "Sudah Ada Catatan Medis" Then
            '    MsgBox("Catatan Awal Medis Belum dibuat", MsgBoxStyle.Exclamation, Me.Text)
            '    'txtRegister.Focus()
            '    fn_ValidateRINGKASANKELUAR = False
            '    Exit Function
            'End If
            If deTANGGALMASUK.DateTime > deTANGGALPULANG.DateTime Then
                MsgBox("Tanggal Keluar Lebih Kecil dari Tanggal Masuk", MsgBoxStyle.Exclamation, Me.Text)
                deTANGGALPULANG.Focus()
                fn_ValidateRINGKASANKELUAR = False
                Exit Function
            End If

            If Finish = True Then
                If txtKeluhanUtama.Text = "" Then
                    MsgBox("Dibutuhkan Keluhan Utama", MsgBoxStyle.Exclamation, Me.Text)
                    txtKeluhanUtama.Focus()
                    fn_ValidateRINGKASANKELUAR = False
                    Exit Function
                End If
                If txtAnamnesa.Text = "" Then
                    MsgBox("Dibutuhkan Anamnesa", MsgBoxStyle.Exclamation, Me.Text)
                    txtAnamnesa.Focus()
                    fn_ValidateRINGKASANKELUAR = False
                    Exit Function
                End If
                If txtKomorbiditasLain.Text = "" Then
                    MsgBox("Dibutuhkan Komorbiditas Lain", MsgBoxStyle.Exclamation, Me.Text)
                    txtKomorbiditasLain.Focus()
                    fn_ValidateRINGKASANKELUAR = False
                    Exit Function
                End If
                If txtPemeriksaanFisik.Text = "" Then
                    MsgBox("Dibutuhkan Pemeriksaan Fisik", MsgBoxStyle.Exclamation, Me.Text)
                    txtPemeriksaanFisik.Focus()
                    fn_ValidateRINGKASANKELUAR = False
                    Exit Function
                End If
                If txtHasilPemeriksaan.Text = "" Then
                    MsgBox("Dibutuhkan Hasil Pemeriksaan", MsgBoxStyle.Exclamation, Me.Text)
                    txtHasilPemeriksaan.Focus()
                    fn_ValidateRINGKASANKELUAR = False
                    Exit Function
                End If
                If txtIndikasiRawat.Text = "" Then
                    MsgBox("Dibutuhkan Indikasi Rawat", MsgBoxStyle.Exclamation, Me.Text)
                    txtIndikasiRawat.Focus()
                    fn_ValidateRINGKASANKELUAR = False
                    Exit Function
                End If
                If txtTerapi.Text = "" Then
                    MsgBox("Dibutuhkan Terapi", MsgBoxStyle.Exclamation, Me.Text)
                    txtTerapi.Focus()
                    fn_ValidateRINGKASANKELUAR = False
                    Exit Function
                End If
                If txtKeadaanUmum.Text = "" Then
                    MsgBox("Dibutuhkan Keadaan Umum", MsgBoxStyle.Exclamation, Me.Text)
                    txtKeadaanUmum.Focus()
                    fn_ValidateRINGKASANKELUAR = False
                    Exit Function
                End If
                If txtKesadaran.Text = "" Then
                    MsgBox("Dibutuhkan Kesadaran", MsgBoxStyle.Exclamation, Me.Text)
                    txtKesadaran.Focus()
                    fn_ValidateRINGKASANKELUAR = False
                    Exit Function
                End If
                'If TextBox12.Text = "" Then
                '    MsgBox("Dibutuhkan GCS", MsgBoxStyle.Exclamation, Me.Text)
                '    TextBox12.Focus()
                '    fn_ValidateRINGKASANKELUAR = False
                '    Exit Function
                'End If
                If chkKEADAANSAATKELUAR_3.Checked = False Then
                    If chkKEADAANSAATKELUAR_5.Checked = False Then
                        If TextBox13.Text = "0" Then
                            MsgBox("Dibutuhkan Tekanan Darah Sistole", MsgBoxStyle.Exclamation, Me.Text)
                            TextBox13.Focus()
                            fn_ValidateRINGKASANKELUAR = False
                            Exit Function
                        End If
                        If TextBox14.Text = "0" Then
                            MsgBox("Dibutuhkan Tekanan Darah Diastole", MsgBoxStyle.Exclamation, Me.Text)
                            TextBox14.Focus()
                            fn_ValidateRINGKASANKELUAR = False
                            Exit Function
                        End If
                        If TextBox16.Text = "" Then
                            MsgBox("Dibutuhkan Suhu", MsgBoxStyle.Exclamation, Me.Text)
                            TextBox16.Focus()
                            fn_ValidateRINGKASANKELUAR = False
                            Exit Function
                        End If
                        If TextBox15.Text = "" Then
                            MsgBox("Dibutuhkan Nadi", MsgBoxStyle.Exclamation, Me.Text)
                            TextBox15.Focus()
                            fn_ValidateRINGKASANKELUAR = False
                            Exit Function
                        End If
                        If TextBox18.Text = "0" Then
                            MsgBox("Dibutuhkan Frekuensi Napas", MsgBoxStyle.Exclamation, Me.Text)
                            TextBox18.Focus()
                            fn_ValidateRINGKASANKELUAR = False
                            Exit Function
                        End If
                        'If txtCatatanPenting.Text = "" Then
                        '    MsgBox("Dibutuhkan Catatan Penting", MsgBoxStyle.Exclamation, Me.Text)
                        '    txtCatatanPenting.Focus()
                        '    fn_ValidateRINGKASANKELUAR = False
                        '    Exit Function
                        'End If
                    End If
                End If

                Dim cek As Boolean = False
                If CheckEdit26.Checked = True Then
                    cek = True
                End If
                If CheckEdit27.Checked = True Then
                    cek = True
                End If
                If CheckEdit28.Checked = True Then
                    cek = True
                End If
                If CheckEdit29.Checked = True Then
                    cek = True
                End If
                If CheckEdit30.Checked = True Then
                    cek = True
                End If
                If chkKEADAANSAATKELUAR_3.Checked = True Then
                    cek = True
                End If
                If chkKEADAANSAATKELUAR_5.Checked = True Then
                    cek = True
                End If

                If cek = False Then
                    MsgBox("Dibutuhkan Cara Keluar Pasien", MsgBoxStyle.Exclamation, Me.Text)
                    fn_ValidateRINGKASANKELUAR = False
                    Exit Function
                End If

                Dim CekDiagnosa As Boolean = False

                For i As Integer = 0 To grvDIAGNOSA.RowCount - 2
                    CekDiagnosa = True
                Next

                If CekDiagnosa = False Then
                    MsgBox("Dibutuhkan Diagnosa Pasien", MsgBoxStyle.Exclamation, Me.Text)
                    fn_ValidateRINGKASANKELUAR = False
                    Exit Function
                End If

            End If

            fn_ValidateRINGKASANKELUAR = True
        Catch oErr As Exception
            MsgBox("Validate Data : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Function
    Private Function fn_SaveRINGKASANKELUAR() As Boolean
        Try
            ' ***** HEADER *****

            Dim ds = oRINGKASANKELUAR.GetStructureHeader
            With ds
                Try
                    .DATECREATED = oRINGKASANKELUAR.GetData(lblRegister.Text).DATECREATED
                Catch ex As Exception
                    .DATECREATED = Now
                End Try
                .DATEUPDATED = Now

                .KDREG = lblRegister.Text
                .KDCUSTOMER = lblNoRM.Text
                .NAMAPASIEN = lblNamaPasien.Text
                .JK = lblJenisKelamin.Text
                .TANGGALLAHIR = sTanggalLahir
                .TANGGALMASUK = deTANGGALMASUK.DateTime
                .TANGGALPULANG = deTANGGALPULANG.DateTime
                .DOKTERUTAMA = sDOKTERUTAMA
                .DOKTERKEDUA = sDOKTERKEDUA
                .DOKTERKETIGA = sDOKTERKETIGA
                .DOKTERKEEMPAT = sDOKTERKEEMPAT
                .DOKTERKELIMA = sDOKTERKELIMA
                .KELUHANUTAMA = txtKeluhanUtama.Text
                .ANAMNESA = txtAnamnesa.Text
                .KOMORBIDITASLAIN = txtKomorbiditasLain.Text
                .PEMERIKSAANFISIK = txtPemeriksaanFisik.Text
                .HASILPEMERIKSAAN = txtHasilPemeriksaan.Text
                .INDIKASIRAWAT = txtIndikasiRawat.Text
                .TERAPI = txtTerapi.Text

                Dim listdiagnosa As New List(Of String)
                For i As Integer = 0 To grvDIAGNOSA.RowCount - 2
                    If grvDIAGNOSA.GetRowCellValue(i, colKATEGORI) = "Primer" Then
                        listdiagnosa.Add(grvDIAGNOSA.GetRowCellValue(i, colNAMADIAGNOSA))
                    End If
                Next
                .DIAGNOSAUTAMA = String.Join(", ", listdiagnosa.ToArray)

                Dim listdiagnosaPenyerta As New List(Of String)
                For i As Integer = 0 To grvDIAGNOSA.RowCount - 2
                    If grvDIAGNOSA.GetRowCellValue(i, colKATEGORI) <> "Primer" Then
                        listdiagnosaPenyerta.Add(i & "." & grvDIAGNOSA.GetRowCellValue(i, colNAMADIAGNOSA))
                    End If
                Next
                .DIAGNOSAPENYERTA = String.Join(vbCrLf, listdiagnosaPenyerta.ToArray)

                Dim listdiagnosaTindakan As New List(Of String)
                For i As Integer = 0 To grvPROSEDUR.RowCount - 2
                    listdiagnosaTindakan.Add(grvPROSEDUR.GetRowCellValue(i, colNAMA) & " " & grvPROSEDUR.GetRowCellValue(i, colKDPROSEDUR))
                Next

                .TINDAKAN = String.Join(", ", listdiagnosaTindakan.ToArray)
                .KEADAANUMUM = txtKeadaanUmum.Text
                .KESADARAN = txtKesadaran.Text
                .GCS = TextBox12.Text
                .TEKANANDARAH = TextBox15.Text
                .SUHU = TextBox16.Text
                .NADI_1 = TextBox13.Text
                .NADI_2 = TextBox14.Text
                .FREKUENSINAPAS = TextBox18.Text
                .CATATANPENTING = txtCatatanPenting.Text
                .SEBABKEMATIAN = txtSebabKematian.Text
                .KEADAANKELUARRS_1 = chkKEADAANSAATKELUAR_1.Checked
                .KEADAANKELUARRS_2 = chkKEADAANSAATKELUAR_2.Checked
                .KEADAANKELUARRS_3 = chkKEADAANSAATKELUAR_3.Checked
                .KEADAANKELUARRS_4 = chkKEADAANSAATKELUAR_4.Checked
                .KEADAANKELUARRS_5 = chkKEADAANSAATKELUAR_5.Checked
                .CARAKELUAR_1 = CheckEdit26.Checked
                .CARAKELUAR_2 = CheckEdit27.Checked
                .CARAKELUAR_3 = CheckEdit28.Checked
                .CARAKELUAR_4 = CheckEdit29.Checked
                .CARAKELUAR_5 = CheckEdit30.Checked
                .KONTROL_1 = CheckEdit21.Checked
                .KONTROL_2 = CheckEdit22.Checked
                .KONTROL_3 = CheckEdit23.Checked
                .KONTROL_4 = CheckEdit24.Checked
                .KONTROL_HARI = CInt(ComboBoxEdit1.Text)
                .TANGGALKONTROL = DateEdit4.DateTime
                .POLIKLINIK = TextBox23.Text
                .INSTITUSI = TextBox24.Text
                .OBATPULANG_1 = CheckEdit25.Checked
                .OBATPULANG_2 = CheckEdit31.Checked
                .OBATPULANG_3 = CheckEdit32.Checked
                'Dim listObatPulang As New List(Of String)
                .OBATPULANG = txtObatPulang.Text
                .EDUKASI = txtEdukasidanIntruksi.Text

                Dim oStaff As New Reference.clsDoctor
                Dim dsDoctor = oStaff.GetData(sKDDOCTOR_DPJPUTAMA)
                If dsDoctor IsNot Nothing Then
                    If dsDoctor.KDUSER = "" Then
                        .KDUSER = sUserID
                    Else
                        .KDUSER = dsDoctor.KDUSER
                    End If
                Else
                    .KDUSER = sUserID
                End If
                .RUANGRAWAT = lblRuangan.Text
                .TARIF_GROUPER = CDec(TextBox1.Text)
                .SATURASI = TextBox17.Text
                .HASIL_GROUPER = TextBox2.Text
                .KDDOCTOR = sKDDOCTOR_DPJPUTAMA
                .BERATBADAN = txtBeratBadanResume.Text
            End With

            'DIAGNOSA
            Dim arrDetailDiagnosa = oRINGKASANKELUAR.GetStructureDetaiDiagnosalList
            For i As Integer = 0 To grvDIAGNOSA.RowCount - 2
                Dim dsDetail = oRINGKASANKELUAR.GetStructureDetailDiagnosa
                With dsDetail
                    .SEQ = i
                    .KDREG = ds.KDREG
                    .KATEGORI = IIf(String.IsNullOrEmpty(grvDIAGNOSA.GetRowCellValue(i, colKATEGORI)), "", grvDIAGNOSA.GetRowCellValue(i, colKATEGORI))
                    .NAMADIAGNOSA = IIf(String.IsNullOrEmpty(grvDIAGNOSA.GetRowCellValue(i, colNAMADIAGNOSA)), "", grvDIAGNOSA.GetRowCellValue(i, colNAMADIAGNOSA))
                    .KDDIAGNOSA = IIf(String.IsNullOrEmpty(grvDIAGNOSA.GetRowCellValue(i, colKDDIAGNOSA)), "", grvDIAGNOSA.GetRowCellValue(i, colKDDIAGNOSA))
                End With
                arrDetailDiagnosa.Add(dsDetail)
            Next

            'PROSEDUR
            Dim arrDetailProsedur = oRINGKASANKELUAR.GetStructureDetailProsedurList
            For i As Integer = 0 To grvPROSEDUR.RowCount - 2
                Dim dsDetail = oRINGKASANKELUAR.GetStructureDetailProsedur
                With dsDetail
                    .SEQ = i
                    .KDREG = ds.KDREG
                    .NAMA = IIf(String.IsNullOrEmpty(grvPROSEDUR.GetRowCellValue(i, colNAMA)), "", grvPROSEDUR.GetRowCellValue(i, colNAMA))
                    .KDPROSEDUR = IIf(String.IsNullOrEmpty(grvPROSEDUR.GetRowCellValue(i, colKDPROSEDUR)), "", grvPROSEDUR.GetRowCellValue(i, colKDPROSEDUR))
                End With
                arrDetailProsedur.Add(dsDetail)
            Next

            ''OBAT PULANG
            'Dim arrDetail = oRINGKASANKELUAR.GetStructureDetailFarmasiList
            'For i As Integer = 0 To grvOBATPULANG.RowCount - 2
            '    Dim dsDetail = oRINGKASANKELUAR.GetStructureDetailFarmasi
            '    With dsDetail
            '        .SEQ = i
            '        .KDREG = ds.KDREG
            '        .NAMAOBAT = fn_LoadITEM(grvOBATPULANG.GetRowCellValue(i, colKDITEM_OBATPULANG))
            '        .SATUAN = fn_LoadUOMDESCRIPTION(grvOBATPULANG.GetRowCellValue(i, colKDUOM_OBATPULANG))
            '        .SIGNA = IIf(String.IsNullOrEmpty(grvOBATPULANG.GetRowCellValue(i, colKDISGNA_OBATPULANG)), "", fn_LoadSIGNA(grvOBATPULANG.GetRowCellValue(i, colKDISGNA_OBATPULANG)))
            '        .CARAPAKAI = IIf(String.IsNullOrEmpty(grvOBATPULANG.GetRowCellValue(i, colKDCARAPAKAI_OBATPULANG)), "", fn_LoadCARAPAKAI(grvOBATPULANG.GetRowCellValue(i, colKDCARAPAKAI_OBATPULANG)))
            '        .QTY = CDec(grvOBATPULANG.GetRowCellValue(i, colQTY_OBATPULANG))
            '        .PRICE = CDec(grvOBATPULANG.GetRowCellValue(i, colPRICE_OBATPULANG))
            '        .GRANDTOTAL = CDec(grvOBATPULANG.GetRowCellValue(i, colGRANDTOTAL_OBATPULANG))
            '        .ROMAWI = IntegerToRoman(CInt(grvOBATPULANG.GetFocusedRowCellValue(colQTY_OBATPULANG)))
            '        .REMARKS_DOKTER = IIf(String.IsNullOrEmpty(grvOBATPULANG.GetRowCellValue(i, colREMARKS_DOKTER_OBATPULANG)), "", grvOBATPULANG.GetRowCellValue(i, colREMARKS_DOKTER_OBATPULANG))
            '        .KDITEM = grvOBATPULANG.GetRowCellValue(i, colKDITEM_OBATPULANG)
            '        .KDUOM = grvOBATPULANG.GetRowCellValue(i, colKDUOM_OBATPULANG)
            '        .KDSIGNA = IIf(String.IsNullOrEmpty(grvOBATPULANG.GetRowCellValue(i, colKDISGNA_OBATPULANG)), "1286", grvOBATPULANG.GetRowCellValue(i, colKDISGNA_OBATPULANG))
            '        .KDCARAPAKAI = IIf(String.IsNullOrEmpty(grvOBATPULANG.GetRowCellValue(i, colKDCARAPAKAI_OBATPULANG)), "10", grvOBATPULANG.GetRowCellValue(i, colKDCARAPAKAI_OBATPULANG))
            '        .QTY_PERUBAHAN = grvOBATPULANG.GetRowCellValue(i, colQTY_OBATPULANG)
            '        .REMARKS_FARMASI = ""
            '    End With

            '    arrDetail.Add(dsDetail)
            'Next

            Dim dsCek = oRINGKASANKELUAR.GetData(lblRegister.Text)

            If dsCek Is Nothing Then
                Try
                    fn_SaveRINGKASANKELUAR = oRINGKASANKELUAR.InsertData(ds, arrDetailDiagnosa, arrDetailProsedur)
                Catch ex As Exception
                    MsgBox("Simpan Data : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            Else
                Try
                    fn_SaveRINGKASANKELUAR = oRINGKASANKELUAR.UpdateData(ds, arrDetailDiagnosa, arrDetailProsedur)
                Catch ex As Exception
                    MsgBox("Simpan Data : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            End If

            Try
                Dim oTerimaResep As New Reference.clsResepTerima
                Dim dsCekTerimaResep = oTerimaResep.GetData(lblRegister.Text & "-" & Now.ToString("ddMMyyyy", CultureInfo.InvariantCulture))

                If dsCekTerimaResep IsNot Nothing Then
                    Dim dsTerimaResep = oTerimaResep.GetStructureHeader
                    With dsTerimaResep
                        .KDTERIMARESEP = dsCekTerimaResep.KDTERIMARESEP
                        .DESCRIPTION = "PERBAIKAN"
                    End With

                    oTerimaResep.UpdateData(dsTerimaResep)
                End If
            Catch ex As Exception
                MsgBox("Update Data Terima Resep: " & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
            End Try
        Catch oErr As Exception
            MsgBox("Simpan Data : " & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            fn_SaveRINGKASANKELUAR = False
        End Try
    End Function
    Private Sub Button8_Click(sender As Object, e As EventArgs) Handles Button8.Click
        Dim frmListTemplate As New frmListTemplate
        Try
            sKODECPPTCOPY = String.Empty

            frmListTemplate.fn_LoadKategori(2, lblNoRM.Text)
            frmListTemplate.ShowDialog(Me)

            If sKODECPPTCOPY <> "" Then
                fn_CopyCPPTAkhirdiResumeDokter(sKODECPPTCOPY)
            End If
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        'Reload Terkahir
        Try
            'Reload Data Terakhir
            If sCOPYCPPTDOKTERTERAKHIR <> "" Then
                fn_CopyCPPTAkhirdiResumeDokter(sCOPYCPPTDOKTERTERAKHIR)
            End If
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub CheckEdit26_CheckedChanged(sender As Object, e As EventArgs) Handles CheckEdit26.CheckedChanged
        If CheckEdit26.Checked = True Then
            CheckEdit27.Checked = False
            CheckEdit28.Checked = False
            CheckEdit29.Checked = False
            CheckEdit30.Checked = False
        End If
    End Sub
    Private Sub CheckEdit27_CheckedChanged(sender As Object, e As EventArgs) Handles CheckEdit27.CheckedChanged
        If CheckEdit27.Checked = True Then
            CheckEdit26.Checked = False
            CheckEdit28.Checked = False
            CheckEdit29.Checked = False
            CheckEdit30.Checked = False
        End If
    End Sub
    Private Sub CheckEdit28_CheckedChanged(sender As Object, e As EventArgs) Handles CheckEdit28.CheckedChanged
        If CheckEdit28.Checked = True Then
            CheckEdit26.Checked = False
            CheckEdit27.Checked = False
            CheckEdit29.Checked = False
            CheckEdit30.Checked = False
        End If
    End Sub
    Private Sub CheckEdit29_CheckedChanged(sender As Object, e As EventArgs) Handles CheckEdit29.CheckedChanged
        If CheckEdit29.Checked = True Then
            CheckEdit26.Checked = False
            CheckEdit27.Checked = False
            CheckEdit28.Checked = False
            CheckEdit30.Checked = False
        End If
    End Sub
    Private Sub CheckEdit30_CheckedChanged(sender As Object, e As EventArgs) Handles CheckEdit30.CheckedChanged
        If CheckEdit30.Checked = True Then
            CheckEdit26.Checked = False
            CheckEdit27.Checked = False
            CheckEdit28.Checked = False
            CheckEdit29.Checked = False
        End If
    End Sub
    Private Sub CheckEdit21_CheckedChanged(sender As Object, e As EventArgs) Handles CheckEdit21.CheckedChanged, CheckEdit32.CheckedChanged, CheckEdit31.CheckedChanged, CheckEdit25.CheckedChanged
        If CheckEdit21.Checked = True Then
            CheckEdit22.Checked = False
            CheckEdit23.Checked = False
            CheckEdit24.Checked = False
        End If

        fn_HitungTanggal()

    End Sub
    Private Sub CheckEdit22_CheckedChanged(sender As Object, e As EventArgs) Handles CheckEdit22.CheckedChanged
        If CheckEdit22.Checked = True Then
            CheckEdit21.Checked = False
            CheckEdit23.Checked = False
            CheckEdit24.Checked = False
        End If

        fn_HitungTanggal()
    End Sub
    Private Sub CheckEdit23_CheckedChanged(sender As Object, e As EventArgs) Handles CheckEdit23.CheckedChanged
        If CheckEdit23.Checked = True Then
            CheckEdit21.Checked = False
            CheckEdit22.Checked = False
            CheckEdit24.Checked = False
        End If

        fn_HitungTanggal()
    End Sub
    Private Sub CheckEdit24_CheckedChanged(sender As Object, e As EventArgs) Handles CheckEdit24.CheckedChanged
        If CheckEdit24.Checked = True Then
            CheckEdit21.Checked = False
            CheckEdit22.Checked = False
            CheckEdit23.Checked = False
        End If

        fn_HitungTanggal()
    End Sub
    Private Sub ComboBoxEdit1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBoxEdit1.SelectedIndexChanged
        fn_HitungTanggal()
    End Sub
    Private Sub fn_HitungTanggal()
        DateEdit4.DateTime = deTANGGALPULANG.DateTime

        If CheckEdit21.Checked = True Then
            DateEdit4.DateTime = deTANGGALPULANG.DateTime.AddDays(3)
        End If
        If CheckEdit22.Checked = True Then
            DateEdit4.DateTime = deTANGGALPULANG.DateTime.AddDays(ComboBoxEdit1.Text)
        End If
        If CheckEdit23.Checked = True Then
            DateEdit4.DateTime = deTANGGALPULANG.DateTime.AddDays(7)
        End If
    End Sub
    Private Sub fn_LoadDataRINGKASANKELUARTEMPLATE(ByVal KODE As String)
        Try
            ' ***** HEADER *****
            Dim ds = oRINGKASANKELUARTemplate.GetData(KODE)

            With ds
                'deTANGGALMASUK.DateTime = .TANGGALMASUK
                'deTANGGALPULANG.DateTime = .TANGGALPULANG
                txtKeluhanUtama.Text = .KELUHANUTAMA
                txtAnamnesa.Text = .ANAMNESA
                txtKomorbiditasLain.Text = .KOMORBIDITASLAIN
                txtPemeriksaanFisik.Text = .PEMERIKSAANFISIK
                txtHasilPemeriksaan.Text = .HASILPEMERIKSAAN
                txtIndikasiRawat.Text = .INDIKASIRAWAT
                txtTerapi.Text = .TERAPI
                txtKeadaanUmum.Text = .KEADAANUMUM
                txtKesadaran.Text = .KESADARAN
                TextBox12.Text = .GCS
                TextBox15.Text = .TEKANANDARAH
                TextBox16.Text = .SUHU
                TextBox13.Text = .NADI_1
                TextBox14.Text = .NADI_2
                TextBox18.Text = .FREKUENSINAPAS
                txtCatatanPenting.Text = .CATATANPENTING
                txtSebabKematian.Text = .SEBABKEMATIAN
                chkKEADAANSAATKELUAR_1.Checked = .KEADAANKELUARRS_1
                chkKEADAANSAATKELUAR_2.Checked = .KEADAANKELUARRS_2
                chkKEADAANSAATKELUAR_3.Checked = .KEADAANKELUARRS_3
                chkKEADAANSAATKELUAR_4.Checked = .KEADAANKELUARRS_4
                chkKEADAANSAATKELUAR_5.Checked = .KEADAANKELUARRS_5
                CheckEdit26.Checked = .CARAKELUAR_1
                CheckEdit27.Checked = .CARAKELUAR_2
                CheckEdit28.Checked = .CARAKELUAR_3
                CheckEdit29.Checked = .CARAKELUAR_4
                CheckEdit30.Checked = .CARAKELUAR_5
                CheckEdit21.Checked = .KONTROL_1
                CheckEdit22.Checked = .KONTROL_2
                CheckEdit23.Checked = .KONTROL_3
                CheckEdit24.Checked = .KONTROL_4
                ComboBoxEdit1.Text = .KONTROL_HARI
                DateEdit4.DateTime = .TANGGALKONTROL
                TextBox23.Text = .POLIKLINIK
                TextBox24.Text = .INSTITUSI
                CheckEdit25.Checked = .OBATPULANG_1
                CheckEdit31.Checked = .OBATPULANG_2
                CheckEdit32.Checked = .OBATPULANG_3
                txtEdukasidanIntruksi.Text = .EDUKASI
                txtObatPulang.Text = .OBATPULANG
                TextBox1.Text = 0
                TextBox17.Text = .SATURASI
                TextBox2.Text = .HASIL_GROUPER

                For Each xloop In oRINGKASANKELUARTemplate.GetDataDetailDiagnosa(KODE)
                    grvDIAGNOSA.Focus()
                    grvDIAGNOSA.AddNewRow()
                    grvDIAGNOSA.SetFocusedRowCellValue(colKATEGORI, xloop.KATEGORI)
                    grvDIAGNOSA.SetFocusedRowCellValue(colKDDIAGNOSA, xloop.KDDIAGNOSA)
                    grvDIAGNOSA.SetFocusedRowCellValue(colNAMADIAGNOSA, xloop.NAMADIAGNOSA)
                    grvDIAGNOSA.UpdateCurrentRow()
                Next

                For Each xloop In oRINGKASANKELUARTemplate.GetDataDetailProsedur(KODE)
                    grvPROSEDUR.Focus()
                    grvPROSEDUR.AddNewRow()
                    grvPROSEDUR.SetFocusedRowCellValue(colKDPROSEDUR, xloop.KDPROSEDUR)
                    grvPROSEDUR.SetFocusedRowCellValue(colNAMA, xloop.NAMA)
                    grvPROSEDUR.UpdateCurrentRow()
                Next

            End With

        Catch oErr As Exception
            MsgBox("Load List Data Template Ringkasan Keluar: " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Function fn_SaveRINGKASANKELUARTemplate(ByVal kode As String) As Boolean
        Try
            ' ***** HEADER *****

            Dim ds = oRINGKASANKELUARTemplate.GetStructureHeader
            With ds
                Try
                    .DATECREATED = oRINGKASANKELUAR.GetData(kode).DATECREATED
                Catch ex As Exception
                    .DATECREATED = Now
                End Try
                .DATEUPDATED = Now
                .RUANGRAWAT = lblRuangan.Text
                .KDTEMPLATE_RINGKASANKELUAR = kode
                .KDCUSTOMER = lblNoRM.Text
                Try
                    .NAMAPASIEN = oRINGKASANKELUARTemplate.GetData(kode).NAMAPASIEN
                Catch ex As Exception
                    .NAMAPASIEN = sRemarks
                End Try
                .JK = lblJenisKelamin.Text
                .TANGGALLAHIR = sTanggalLahir
                .TANGGALMASUK = deTANGGALMASUK.DateTime
                .TANGGALPULANG = deTANGGALPULANG.DateTime
                .DOKTERUTAMA = sDOKTERUTAMA
                .DOKTERKEDUA = sDOKTERKEDUA
                .DOKTERKETIGA = sDOKTERKETIGA
                .DOKTERKEEMPAT = sDOKTERKEEMPAT
                .DOKTERKELIMA = sDOKTERKELIMA
                .KELUHANUTAMA = txtKeluhanUtama.Text
                .ANAMNESA = txtAnamnesa.Text
                .KOMORBIDITASLAIN = txtKomorbiditasLain.Text
                .PEMERIKSAANFISIK = txtPemeriksaanFisik.Text
                .HASILPEMERIKSAAN = txtHasilPemeriksaan.Text
                .INDIKASIRAWAT = txtIndikasiRawat.Text
                .TERAPI = txtTerapi.Text

                Dim listdiagnosa As New List(Of String)
                For i As Integer = 0 To grvDIAGNOSA.RowCount - 2
                    If grvDIAGNOSA.GetRowCellValue(i, colKATEGORI) = "Primer" Then
                        listdiagnosa.Add(grvDIAGNOSA.GetRowCellValue(i, colNAMADIAGNOSA))
                    End If
                Next
                .DIAGNOSAUTAMA = String.Join(", ", listdiagnosa.ToArray)

                Dim listdiagnosaPenyerta As New List(Of String)
                For i As Integer = 0 To grvDIAGNOSA.RowCount - 2
                    If grvDIAGNOSA.GetRowCellValue(i, colKATEGORI) <> "Primer" Then
                        listdiagnosaPenyerta.Add(i & "." & grvDIAGNOSA.GetRowCellValue(i, colNAMADIAGNOSA))
                    End If
                Next
                .DIAGNOSAPENYERTA = String.Join(vbCrLf, listdiagnosaPenyerta.ToArray)

                Dim listdiagnosaTindakan As New List(Of String)
                For i As Integer = 0 To grvPROSEDUR.RowCount - 2
                    listdiagnosaTindakan.Add(grvPROSEDUR.GetRowCellValue(i, colNAMA) & " " & grvPROSEDUR.GetRowCellValue(i, colKDPROSEDUR))
                Next

                .TINDAKAN = String.Join(", ", listdiagnosaTindakan.ToArray)
                .KEADAANUMUM = txtKeadaanUmum.Text
                .KESADARAN = txtKesadaran.Text
                .GCS = TextBox12.Text
                .TEKANANDARAH = TextBox15.Text
                .SUHU = TextBox16.Text
                .NADI_1 = TextBox13.Text
                .NADI_2 = TextBox14.Text
                .FREKUENSINAPAS = TextBox18.Text
                .CATATANPENTING = txtCatatanPenting.Text
                .SEBABKEMATIAN = txtSebabKematian.Text
                .KEADAANKELUARRS_1 = chkKEADAANSAATKELUAR_1.Checked
                .KEADAANKELUARRS_2 = chkKEADAANSAATKELUAR_2.Checked
                .KEADAANKELUARRS_3 = chkKEADAANSAATKELUAR_3.Checked
                .KEADAANKELUARRS_4 = chkKEADAANSAATKELUAR_4.Checked
                .KEADAANKELUARRS_5 = chkKEADAANSAATKELUAR_5.Checked
                .CARAKELUAR_1 = CheckEdit26.Checked
                .CARAKELUAR_2 = CheckEdit27.Checked
                .CARAKELUAR_3 = CheckEdit28.Checked
                .CARAKELUAR_4 = CheckEdit29.Checked
                .CARAKELUAR_5 = CheckEdit30.Checked
                .KONTROL_1 = CheckEdit21.Checked
                .KONTROL_2 = CheckEdit22.Checked
                .KONTROL_3 = CheckEdit23.Checked
                .KONTROL_4 = CheckEdit24.Checked
                .KONTROL_HARI = CInt(ComboBoxEdit1.Text)
                .TANGGALKONTROL = DateEdit4.DateTime
                .POLIKLINIK = TextBox23.Text
                .INSTITUSI = TextBox24.Text
                .OBATPULANG_1 = CheckEdit25.Checked
                .OBATPULANG_2 = CheckEdit31.Checked
                .OBATPULANG_3 = CheckEdit32.Checked
                'Dim listObatPulang As New List(Of String)
                .OBATPULANG = txtObatPulang.Text
                .EDUKASI = txtEdukasidanIntruksi.Text
                .KDUSER = sUserID
                .TARIF_GROUPER = CDec(0)
                .SATURASI = TextBox17.Text
                .HASIL_GROUPER = TextBox2.Text
            End With

            'DIAGNOSA
            Dim arrDetailDiagnosa = oRINGKASANKELUARTemplate.GetStructureDetailDiagnosaList
            For i As Integer = 0 To grvDIAGNOSA.RowCount - 2
                Dim dsDetail = oRINGKASANKELUARTemplate.GetStructureDetailDiagnosa
                With dsDetail
                    .SEQ = i
                    .KDTEMPLATE_RINGKASANKELUAR = ds.KDTEMPLATE_RINGKASANKELUAR
                    .KATEGORI = IIf(String.IsNullOrEmpty(grvDIAGNOSA.GetRowCellValue(i, colKATEGORI)), "", grvDIAGNOSA.GetRowCellValue(i, colKATEGORI))
                    .NAMADIAGNOSA = IIf(String.IsNullOrEmpty(grvDIAGNOSA.GetRowCellValue(i, colNAMADIAGNOSA)), "", grvDIAGNOSA.GetRowCellValue(i, colNAMADIAGNOSA))
                    .KDDIAGNOSA = IIf(String.IsNullOrEmpty(grvDIAGNOSA.GetRowCellValue(i, colKDDIAGNOSA)), "", grvDIAGNOSA.GetRowCellValue(i, colKDDIAGNOSA))
                End With
                arrDetailDiagnosa.Add(dsDetail)
            Next

            'PROSEDUR
            Dim arrDetailProsedur = oRINGKASANKELUARTemplate.GetStructureDetailProsedurList
            For i As Integer = 0 To grvPROSEDUR.RowCount - 2
                Dim dsDetail = oRINGKASANKELUARTemplate.GetStructureDetailProsedur
                With dsDetail
                    .SEQ = i
                    .KDTEMPLATE_RINGKASANKELUAR = ds.KDTEMPLATE_RINGKASANKELUAR
                    .NAMA = IIf(String.IsNullOrEmpty(grvPROSEDUR.GetRowCellValue(i, colNAMA)), "", grvPROSEDUR.GetRowCellValue(i, colNAMA))
                    .KDPROSEDUR = IIf(String.IsNullOrEmpty(grvPROSEDUR.GetRowCellValue(i, colKDPROSEDUR)), "", grvPROSEDUR.GetRowCellValue(i, colKDPROSEDUR))
                End With
                arrDetailProsedur.Add(dsDetail)
            Next

            If kode = "" Then
                Try
                    fn_SaveRINGKASANKELUARTemplate = oRINGKASANKELUARTemplate.InsertData(ds, arrDetailDiagnosa, arrDetailProsedur)
                Catch ex As Exception
                    MsgBox("Simpan Data : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            Else
                Try
                    fn_SaveRINGKASANKELUARTemplate = oRINGKASANKELUARTemplate.UpdateData(ds, arrDetailDiagnosa, arrDetailProsedur)
                Catch ex As Exception
                    MsgBox("Simpan Data : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            End If

        Catch oErr As Exception
            MsgBox("Simpan Data Ringkasan Keluar Template: " & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            fn_SaveRINGKASANKELUARTemplate = False
        End Try
    End Function
    Private Sub grvDIAGNOSA_CellValueChanged(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs) Handles grvDIAGNOSA.CellValueChanged
        If e.Column.Name = colNAMADIAGNOSA.Name Then
            Dim CEK As Boolean = False

            For i As Integer = 0 To grvDIAGNOSA.RowCount - 2
                If grvDIAGNOSA.GetRowCellValue(i, colKATEGORI) = "Primer" Then
                    CEK = True
                End If
            Next

            If CEK = False Then
                grvDIAGNOSA.SetFocusedRowCellValue(colKATEGORI, "Primer")
            Else
                grvDIAGNOSA.SetFocusedRowCellValue(colKATEGORI, "Sekunder")
            End If
        End If
    End Sub
    Private Sub deTANGGALPULANG_EditValueChanged_1(sender As Object, e As EventArgs) Handles deTANGGALPULANG.EditValueChanged
        fn_HitungTanggal()
    End Sub
    Private Sub GridLookUpEdit2_EditValueChanged_1(sender As Object, e As EventArgs) Handles GridLookUpEdit2.EditValueChanged
        If GridLookUpEdit2.Text <> "" Then
            grvPROSEDUR.Focus()
            grvPROSEDUR.AddNewRow()
            grvPROSEDUR.SetFocusedRowCellValue(colKDPROSEDUR, GridLookUpEdit2.EditValue)
            grvPROSEDUR.SetFocusedRowCellValue(colNAMA, GridLookUpEdit2.Text)
            grvPROSEDUR.UpdateCurrentRow()
        End If
    End Sub
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim frmListTemplate As New frmListTemplate
        Try
            sKODETEMPLATE_RINGKASAN = String.Empty

            frmListTemplate.fn_LoadKategori(1, lblNoRM.Text)
            frmListTemplate.ShowDialog(Me)

            If sKODETEMPLATE_RINGKASAN <> "" Then
                fn_LoadDataRINGKASANKELUARTEMPLATE(sKODETEMPLATE_RINGKASAN)
            End If
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        sRemarks = String.Empty
        frmJudulTemplate.ShowDialog(Me)

        If sRemarks = "" Then
            MsgBox("Judul Template Masih Kosong", MsgBoxStyle.Exclamation, Me.Text)
        Else
            sKODETEMPLATE_RINGKASAN = String.Empty
            fn_SaveRINGKASANKELUARTemplate(sKODETEMPLATE_RINGKASAN)
            sRemarks = String.Empty
        End If
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        If sKODETEMPLATE_RINGKASAN = "" Then
            MsgBox("Load Data Template Belum di pilih", MsgBoxStyle.Exclamation, Me.Text)
        Else
            fn_SaveRINGKASANKELUARTemplate(sKODETEMPLATE_RINGKASAN)
            'sKODETEMPLATE_RINGKASAN = String.Empty
        End If
    End Sub
    Private Sub txtCariDiagnosaAsesmen_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtCariDiagnosaAsesmen.KeyPress
        If Asc(e.KeyChar) = 13 Then
            btnCariDiagnosaAsesmen_Click()
        End If
    End Sub
    Private Sub btnCariDiagnosaAsesmen_Click(sender As Object, e As EventArgs) Handles btnCariDiagnosaAsesmen.Click
        btnCariDiagnosaAsesmen_Click()
    End Sub
    Private Sub btnCariDiagnosaAsesmen_Click()
        If txtCariDiagnosaAsesmen.Text.Length < 3 Then
            MsgBox("Minimal 3 Huruf !!!", MsgBoxStyle.Exclamation, Me.Text)
            Exit Sub
        End If
        Try
            Dim jsonDecode = JObject.Parse(oBrigging.fn_Pencariandiagnosa(txtCariDiagnosaAsesmen.Text))
            Dim sDataDuplicate As String = String.Empty
            Dim smessage As String = String.Empty

            sDataDuplicate = jsonDecode("metadata")("code").ToString
            smessage = jsonDecode("metadata")("message").ToString

            If sDataDuplicate = "200" Then
                Dim table As DataTable

                table = New DataTable("M_DIAGNOSA")
                table.Columns.Add("nama")
                table.Columns.Add("kode")

                For Each item In jsonDecode("response")("data")
                    table.Rows.Add(New String() {item(0), item(1)})
                Next

                GridLookUpEdit3.Properties.DataSource = table
                GridLookUpEdit3.Properties.ValueMember = "kode"
                GridLookUpEdit3.Properties.DisplayMember = "nama"

                GridLookUpEdit3.ShowPopup()

                txtCariDiagnosaAsesmen.ResetText()
            Else
                MsgBox("Pencarian Diagnosa" & vbCrLf & sDataDuplicate & "-" & smessage, MsgBoxStyle.Information, Me.Text)
            End If
        Catch oErr As Exception
            MsgBox("Pencarian Diagnosa: " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub GridLookUpEdit3_EditValueChanged(sender As Object, e As EventArgs) Handles GridLookUpEdit3.EditValueChanged
        If GridLookUpEdit3.Text <> "" Then
            Dim sCek As Integer = 0

            For i As Integer = 0 To grvDiagnosaAsesmen.RowCount - 2
                sCek += 1
            Next

            grvDiagnosaAsesmen.Focus()
            grvDiagnosaAsesmen.AddNewRow()
            grvDiagnosaAsesmen.SetFocusedRowCellValue(colKATEGORIASESMEN, IIf(sCek = 0, "Primer", "Sekunder"))
            grvDiagnosaAsesmen.SetFocusedRowCellValue(colKDDIAGNOSAASESMEN, GridLookUpEdit3.EditValue)
            grvDiagnosaAsesmen.SetFocusedRowCellValue(colNAMADIAGNOSAASESMEN, GridLookUpEdit3.Text)
            grvDiagnosaAsesmen.UpdateCurrentRow()
        End If
    End Sub
    Private Sub Button13_Click(sender As Object, e As EventArgs) Handles Button13.Click
        Dim frmListTemplate As New frmListTemplate
        Try
            sKODEASESMENCOPY = String.Empty

            frmListTemplate.fn_LoadKategori(3, "")
            frmListTemplate.ShowDialog(Me)

            If sKODEASESMENCOPY <> "" Then
                fn_LoadDataTemplateDischargePlanning(sKODEASESMENCOPY)
            End If
        Catch oErr As Exception
            MsgBox("Load Data Template" & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub Button12_Click(sender As Object, e As EventArgs) Handles Button12.Click
        sRemarks = String.Empty
        frmJudulTemplate.ShowDialog(Me)

        If sRemarks = "" Then
            MsgBox("Judul Template Masih Kosong", MsgBoxStyle.Exclamation, Me.Text)
        Else
            sKODEASESMENCOPY = sRemarks
            Dim oS_DIGITAL_RI_13_Template As New Transaksi.clsDigital_DischargePlanningTemplate

            Dim ds = oS_DIGITAL_RI_13_Template.GetData(sKODEASESMENCOPY)
            If ds IsNot Nothing Then
                MsgBox("Judul Sudah Ada", MsgBoxStyle.Exclamation, Me.Text)
                Exit Sub
            End If

            If MsgBox("Save As " & sKODEASESMENCOPY & "?", MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, Me.Text) = MsgBoxResult.No Then Exit Sub
            If fn_SaveTemplateDischargePlanning() = False Then
                sKODEASESMENCOPY = ""
                MsgBox("Save As gagal! Tolong kontak admin anda!", MsgBoxStyle.Exclamation, Me.Text)
            Else
                MsgBox("Save As " & sKODEASESMENCOPY & " success!", MsgBoxStyle.Information, Me.Text)
            End If

            sRemarks = String.Empty
        End If
    End Sub
    Private Sub Button11_Click(sender As Object, e As EventArgs) Handles Button11.Click
        If sKODEASESMENCOPY = "" Then
            MsgBox("Silahkan Load Data Terlebih Dahulu", MsgBoxStyle.Exclamation, Me.Text)
            Exit Sub
        End If

        If MsgBox("Save As " & sKODEASESMENCOPY & "?", MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, Me.Text) = MsgBoxResult.No Then Exit Sub
        If fn_SaveTemplateDischargePlanning() = False Then
            MsgBox("Save gagal! Tolong kontak admin anda!", MsgBoxStyle.Exclamation, Me.Text)
        Else
            MsgBox("Save " & sKODEASESMENCOPY & " success!", MsgBoxStyle.Information, Me.Text)
        End If
    End Sub
    Private Sub ToolStripMenuItem7_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem7.Click
        grvDiagnosaAsesmen.DeleteSelectedRows()
    End Sub
#End Region
#Region "Hand Over, Nilai Kritis, SBAR"
    Private Sub grvHandOver_DoubleClick(sender As Object, e As EventArgs) Handles grvHandOver.DoubleClick
        Try
            If grvHandOver.GetFocusedRowCellValue("KDCPPT") = String.Empty Then
                MsgBox("silahkan pilih Kode CPPT !!!", MsgBoxStyle.Exclamation, Me.Text)
                Exit Sub
            End If

            'If sCasemix = False Then
            '    MsgBox("Update Dapat Dilakukan Oleh Bagian Casemix !!!", MsgBoxStyle.Exclamation, Me.Text)
            '    Exit Sub
            'End If

            lblKODECPPT.Text = grvHandOver.GetFocusedRowCellValue("KDCPPT")

            XtraTabControl1.SelectedTabPageIndex = 0

            fn_ViewModeCPPT(False)
            'fn_EmptyMeCPPT()
            fn_LoadDataCPPT()

        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub picPemberiPesan_HandOver_Click() Handles picPemberiPesan_HandOver.Click
        Try
            If grvHandOver.GetFocusedRowCellValue("KDCPPT") = String.Empty Then
                MsgBox("silahkan pilih Kode CPPT !!!", MsgBoxStyle.Exclamation, Me.Text)
                Exit Sub
            End If

            If grvHandOver.GetFocusedRowCellValue("STATUS") = "SBAR" Then
                MsgBox("Silahkan Pilih Status CPPT !!!", MsgBoxStyle.Exclamation, Me.Text)
                Exit Sub
            End If

            Dim CatatanKategori As String = "Pemberi Hand Over"
            Dim CatatanKategoriTanggal As String = "Tanggal " & Now.ToString("dd-MM-yyyy HH:mm", CultureInfo.InvariantCulture)
            Dim userpemberi As String = String.Empty

            If sISDOKTER = False Then
                If grdUSERHADNOVER.Text = "" Then
                    MsgBox("User Masih Kosong !!!", MsgBoxStyle.Exclamation, Me.Text)
                    Exit Sub
                End If
                CatatanKategori = CatatanKategori & " Oleh " & grdUSERHADNOVER.Text
                userpemberi = grdUSERHADNOVER.Text
            Else
                If grdDPJPUtama.Text = "" Then
                    MsgBox("User Masih Kosong !!!", MsgBoxStyle.Exclamation, Me.Text)
                    Exit Sub
                End If
                CatatanKategori = CatatanKategori & " Oleh " & grdDPJPUtama.Text
                userpemberi = grdDPJPUtama.Text
            End If

            Dim UpdateCatatan As String = CatatanKategori & vbCrLf & CatatanKategoriTanggal

            Dim dsCPPT = oCPPT.GetData(grvHandOver.GetFocusedRowCellValue("KDCPPT"))

            Dim Catatan As String = String.Empty

            If dsCPPT IsNot Nothing Then
                Catatan = dsCPPT.AGAMA
            End If

            If Catatan.Contains(CatatanKategori) Then
                MsgBox("Sudah melakukan " & CatatanKategori, MsgBoxStyle.Exclamation, Me.Text)
            Else
                If MsgBox("Apakah Yakin " & UpdateCatatan & " ?", MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, Me.Text) = MsgBoxResult.No Then Exit Sub
                oCPPT.UpdatePemberi(grvHandOver.GetFocusedRowCellValue("KDCPPT"), UpdateCatatan)
                oCPPT.UpdatePemberiHandOver(grvHandOver.GetFocusedRowCellValue("KDCPPT"), CatatanKategoriTanggal.Replace("Tanggal ", ""), userpemberi, True)
                fn_LoadPdfViewerCPPTRANANP()
            End If
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub PicPenerimaPesan_HandOver_Click() Handles PicPenerimaPesan_HandOver.Click
        Try
            If grvHandOver.GetFocusedRowCellValue("KDCPPT") = String.Empty Then
                MsgBox("silahkan pilih Kode CPPT !!!", MsgBoxStyle.Exclamation, Me.Text)
                Exit Sub
            End If

            If grvHandOver.GetFocusedRowCellValue("STATUS") = "SBAR" Then
                MsgBox("Silahkan Pilih Status CPPT !!!", MsgBoxStyle.Exclamation, Me.Text)
                Exit Sub
            End If

            Dim CatatanKategori As String = "Penerima Hand Over"
            Dim CatatanKategoriTanggal As String = "Tanggal " & Now.ToString("dd-MM-yyyy HH:mm", CultureInfo.InvariantCulture)
            Dim catatanValidasi As String = "Pemberi Hand Over"
            Dim userpenerima As String = String.Empty

            If sISDOKTER = False Then
                If grdUSERHADNOVER.Text = "" Then
                    MsgBox("User Masih Kosong !!!", MsgBoxStyle.Exclamation, Me.Text)
                    Exit Sub
                End If
                CatatanKategori = CatatanKategori & " Oleh " & grdUSERHADNOVER.Text
                catatanValidasi = catatanValidasi & " Oleh " & grdUSERHADNOVER.Text

                userpenerima = grdUSERHADNOVER.Text
            Else
                If grdDPJPUtama.Text = "" Then
                    MsgBox("User Masih Kosong !!!", MsgBoxStyle.Exclamation, Me.Text)
                    Exit Sub
                End If
                CatatanKategori = CatatanKategori & " Oleh " & grdDPJPUtama.Text
                catatanValidasi = catatanValidasi & " Oleh " & grdDPJPUtama.Text

                userpenerima = grdDPJPUtama.Text
            End If

            Dim UpdateCatatan As String = CatatanKategori & vbCrLf & CatatanKategoriTanggal

            Dim dsCPPT = oCPPT.GetData(grvHandOver.GetFocusedRowCellValue("KDCPPT"))

            Dim Catatan As String = String.Empty

            If dsCPPT IsNot Nothing Then
                Catatan = dsCPPT.AGAMA
            End If

            If Catatan.Contains(CatatanKategori) Then
                MsgBox("Sudah melakukan " & CatatanKategori, MsgBoxStyle.Exclamation, Me.Text)
            Else
                If Catatan.Contains(catatanValidasi) Then
                    MsgBox("Sudah melakukan " & catatanValidasi, MsgBoxStyle.Exclamation, Me.Text)
                Else
                    If MsgBox("Apakah Yakin " & UpdateCatatan & " ?", MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, Me.Text) = MsgBoxResult.No Then Exit Sub
                    oCPPT.UpdatePemberi(grvHandOver.GetFocusedRowCellValue("KDCPPT"), UpdateCatatan)
                    oCPPT.UpdatePemberiHandOver(grvHandOver.GetFocusedRowCellValue("KDCPPT"), CatatanKategoriTanggal.Replace("Tanggal ", ""), userpenerima, False)
                    fn_LoadPdfViewerCPPTRANANP()
                End If
            End If
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub picPemberiNilaiKritis_Click() Handles picPemberiNilaiKritis.Click
        Try
            If grvHandOver.GetFocusedRowCellValue("KDCPPT") = String.Empty Then
                MsgBox("silahkan pilih Kode CPPT !!!", MsgBoxStyle.Exclamation, Me.Text)
                Exit Sub
            End If

            Dim CatatanKategori As String = "Pemberi Nilai Kritis"
            Dim CatatanKategoriTanggal As String = "Tanggal " & Now.ToString("dd-MM-yyyy HH:mm", CultureInfo.InvariantCulture)

            'If sISDOKTER = False Then
            '    If grdUSERHADNOVER.Text = "" Then
            '        MsgBox("User Masih Kosong !!!", MsgBoxStyle.Exclamation, Me.Text)
            '        Exit Sub
            '    End If
            '    CatatanKategori = CatatanKategori & " Oleh " & grdUSERHADNOVER.Text
            'Else
            '    If grdDPJPUtama.Text = "" Then
            '        MsgBox("User Masih Kosong !!!", MsgBoxStyle.Exclamation, Me.Text)
            '        Exit Sub
            '    End If
            '    CatatanKategori = CatatanKategori & " Oleh " & grdDPJPUtama.Text
            'End If

            If grdDPJPUtama.Text = "" Then
                MsgBox("DPJP Masih Kosong !!!", MsgBoxStyle.Exclamation, Me.Text)
                Exit Sub
            Else
                CatatanKategori = CatatanKategori & " Oleh " & grdDPJPUtama.Text
            End If

            Dim UpdateCatatan As String = CatatanKategori & vbCrLf & CatatanKategoriTanggal

            Dim dsCPPT = oCPPT.GetData(grvHandOver.GetFocusedRowCellValue("KDCPPT"))

            Dim Catatan As String = String.Empty

            If dsCPPT IsNot Nothing Then
                Catatan = dsCPPT.AGAMA
            End If

            If Catatan.Contains(CatatanKategori) Then
                MsgBox("Sudah melakukan " & CatatanKategori, MsgBoxStyle.Exclamation, Me.Text)
            Else
                If MsgBox("Apakah Yakin " & UpdateCatatan & " ?", MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, Me.Text) = MsgBoxResult.No Then Exit Sub
                oCPPT.UpdatePemberi(grvHandOver.GetFocusedRowCellValue("KDCPPT"), UpdateCatatan)
                oCPPT.UpdatePemberiNilaiKritis(grvHandOver.GetFocusedRowCellValue("KDCPPT"), CatatanKategoriTanggal.Replace("Tanggal ", ""), grdDPJPUtama.Text, True)
                fn_LoadPdfViewerCPPTRANANP()
            End If
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub picPenerimaNilaiKritis_Click() Handles picPenerimaNilaiKritis.Click
        Try
            If grvHandOver.GetFocusedRowCellValue("KDCPPT") = String.Empty Then
                MsgBox("silahkan pilih Kode CPPT !!!", MsgBoxStyle.Exclamation, Me.Text)
                Exit Sub
            End If

            Dim CatatanKategori As String = "Penerima Nilai Kritis"
            Dim CatatanKategoriTanggal As String = "Tanggal " & Now.ToString("dd-MM-yyyy HH:mm", CultureInfo.InvariantCulture)
            Dim catatanValidasi As String = "Pemberi Nilai Kritis"

            'If sISDOKTER = False Then
            '    If grdUSERHADNOVER.Text = "" Then
            '        MsgBox("User Masih Kosong !!!", MsgBoxStyle.Exclamation, Me.Text)
            '        Exit Sub
            '    End If
            '    CatatanKategori = CatatanKategori & " Oleh " & grdUSERHADNOVER.Text
            '    catatanValidasi = catatanValidasi & " Oleh " & grdUSERHADNOVER.Text
            'Else
            '    If grdDPJPUtama.Text = "" Then
            '        MsgBox("User Masih Kosong !!!", MsgBoxStyle.Exclamation, Me.Text)
            '        Exit Sub
            '    End If
            '    CatatanKategori = CatatanKategori & " Oleh " & grdDPJPUtama.Text
            '    catatanValidasi = catatanValidasi & " Oleh " & grdDPJPUtama.Text
            'End If

            If grdUSERHADNOVER.Text = "" Then
                MsgBox("User Masih Kosong !!!", MsgBoxStyle.Exclamation, Me.Text)
                Exit Sub
            Else
                CatatanKategori = CatatanKategori & " Oleh " & grdUSERHADNOVER.Text
                catatanValidasi = catatanValidasi & " Oleh " & grdUSERHADNOVER.Text
            End If

            Dim UpdateCatatan As String = CatatanKategori & vbCrLf & CatatanKategoriTanggal

            Dim dsCPPT = oCPPT.GetData(grvHandOver.GetFocusedRowCellValue("KDCPPT"))

            Dim Catatan As String = String.Empty

            If dsCPPT IsNot Nothing Then
                Catatan = dsCPPT.AGAMA
            End If

            If Catatan.Contains(CatatanKategori) Then
                MsgBox("Sudah melakukan " & CatatanKategori, MsgBoxStyle.Exclamation, Me.Text)
            Else
                If Catatan.Contains(catatanValidasi) Then
                    MsgBox("Sudah melakukan " & catatanValidasi, MsgBoxStyle.Exclamation, Me.Text)
                Else
                    If MsgBox("Apakah Yakin " & UpdateCatatan & " ?", MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, Me.Text) = MsgBoxResult.No Then Exit Sub
                    oCPPT.UpdatePemberi(grvHandOver.GetFocusedRowCellValue("KDCPPT"), UpdateCatatan)
                    oCPPT.UpdatePemberiNilaiKritis(grvHandOver.GetFocusedRowCellValue("KDCPPT"), CatatanKategoriTanggal.Replace("Tanggal ", ""), grdUSERHADNOVER.Text, False)
                    fn_LoadPdfViewerCPPTRANANP()
                End If
            End If
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub picPemberiSBAR_Click() Handles picPemberiSBAR.Click
        Try
            If grvHandOver.GetFocusedRowCellValue("KDCPPT") = String.Empty Then
                MsgBox("silahkan pilih Kode CPPT !!!", MsgBoxStyle.Exclamation, Me.Text)
                Exit Sub
            End If
            If grvHandOver.GetFocusedRowCellValue("STATUS") <> "SBAR" Then
                MsgBox("Silahkan Pilih Status SBAR !!!", MsgBoxStyle.Exclamation, Me.Text)
                Exit Sub
            End If

            Dim CatatanKategori As String = "Pemberi SBAR"
            Dim CatatanKategoriTanggal As String = "Tanggal " & Now.ToString("dd-MM-yyyy HH:mm", CultureInfo.InvariantCulture)

            'If sISDOKTER = False Then
            '    'If grdUSERHADNOVER.Text = "" Then
            '    '    MsgBox("User Masih Kosong !!!", MsgBoxStyle.Exclamation, Me.Text)
            '    '    Exit Sub
            '    'End If
            '    CatatanKategori = CatatanKategori & " Oleh " & grdUSERHADNOVER.Text
            'Else
            '    'If grdDPJPUtama.Text = "" Then
            '    '    MsgBox("User Masih Kosong !!!", MsgBoxStyle.Exclamation, Me.Text)
            '    '    Exit Sub
            '    'End If
            '    CatatanKategori = CatatanKategori & " Oleh " & grdDPJPUtama.Text
            'End If

            'If grdUSERHADNOVER.Text = "" Then
            '    MsgBox("User Masih Kosong !!!", MsgBoxStyle.Exclamation, Me.Text)
            '    Exit Sub
            'End If

            If grdDPJPUtama.Text = "" Then
                MsgBox("DPJP Masih Kosong !!!", MsgBoxStyle.Exclamation, Me.Text)
                Exit Sub
            Else
                CatatanKategori = CatatanKategori & " Oleh " & grdDPJPUtama.Text
            End If

            Dim UpdateCatatan As String = CatatanKategori & vbCrLf & CatatanKategoriTanggal

            Dim dsCPPT = oCPPT.GetData(grvHandOver.GetFocusedRowCellValue("KDCPPT"))

            Dim Catatan As String = String.Empty

            If dsCPPT IsNot Nothing Then
                Catatan = dsCPPT.AGAMA
            End If

            If Catatan.Contains(CatatanKategori) Then
                MsgBox("Sudah melakukan " & CatatanKategori, MsgBoxStyle.Exclamation, Me.Text)
            Else
                If MsgBox("Apakah Yakin " & UpdateCatatan & " ?", MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, Me.Text) = MsgBoxResult.No Then Exit Sub
                oCPPT.UpdatePemberi(grvHandOver.GetFocusedRowCellValue("KDCPPT"), UpdateCatatan)
                oCPPT.UpdatePemberiSBAR(grvHandOver.GetFocusedRowCellValue("KDCPPT"), CatatanKategoriTanggal.Replace("Tanggal ", ""), grdDPJPUtama.Text, True)
                fn_LoadPdfViewerCPPTRANANP()
            End If
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub picPenerimaSBAR_Click() Handles picPenerimaSBAR.Click
        Try
            If grvHandOver.GetFocusedRowCellValue("KDCPPT") = String.Empty Then
                MsgBox("silahkan pilih Kode CPPT !!!", MsgBoxStyle.Exclamation, Me.Text)
                Exit Sub
            End If

            If grvHandOver.GetFocusedRowCellValue("STATUS") <> "SBAR" Then
                MsgBox("Silahkan Pilih Status SBAR !!!", MsgBoxStyle.Exclamation, Me.Text)
                Exit Sub
            End If

            Dim CatatanKategori As String = "Penerima SBAR"
            Dim CatatanKategoriTanggal As String = "Tanggal " & Now.ToString("dd-MM-yyyy HH:mm", CultureInfo.InvariantCulture)
            Dim catatanValidasi As String = "Pemberi SBAR"

            'If sISDOKTER = False Then
            '    'If grdUSERHADNOVER.Text = "" Then
            '    '    MsgBox("User Masih Kosong !!!", MsgBoxStyle.Exclamation, Me.Text)
            '    '    Exit Sub
            '    'End If
            '    CatatanKategori = CatatanKategori & " Oleh " & grdUSERHADNOVER.Text
            '    catatanValidasi = catatanValidasi & " Oleh " & grdUSERHADNOVER.Text
            'Else
            '    'If grdDPJPUtama.Text = "" Then
            '    '    MsgBox("User Masih Kosong !!!", MsgBoxStyle.Exclamation, Me.Text)
            '    '    Exit Sub
            '    'End If
            '    CatatanKategori = CatatanKategori & " Oleh " & grdDPJPUtama.Text
            '    catatanValidasi = catatanValidasi & " Oleh " & grdDPJPUtama.Text
            'End If

            If grdUSERHADNOVER.Text = "" Then
                MsgBox("User Masih Kosong !!!", MsgBoxStyle.Exclamation, Me.Text)
                Exit Sub
            Else
                CatatanKategori = CatatanKategori & " Oleh " & grdUSERHADNOVER.Text
                catatanValidasi = catatanValidasi & " Oleh " & grdUSERHADNOVER.Text
            End If

            'If grdDPJPUtama.Text = "" Then
            '    MsgBox("User Masih Kosong !!!", MsgBoxStyle.Exclamation, Me.Text)
            '    Exit Sub
            'End If

            Dim UpdateCatatan As String = CatatanKategori & vbCrLf & CatatanKategoriTanggal

            Dim dsCPPT = oCPPT.GetData(grvHandOver.GetFocusedRowCellValue("KDCPPT"))

            Dim Catatan As String = String.Empty

            If dsCPPT IsNot Nothing Then
                Catatan = dsCPPT.AGAMA
            End If

            If Catatan.Contains(CatatanKategori) Then
                MsgBox("Sudah melakukan " & CatatanKategori, MsgBoxStyle.Exclamation, Me.Text)
            Else
                If Catatan.Contains(catatanValidasi) Then
                    MsgBox("Sudah melakukan " & catatanValidasi, MsgBoxStyle.Exclamation, Me.Text)
                Else
                    If MsgBox("Apakah Yakin " & UpdateCatatan & " ?", MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, Me.Text) = MsgBoxResult.No Then Exit Sub
                    oCPPT.UpdatePemberi(grvHandOver.GetFocusedRowCellValue("KDCPPT"), UpdateCatatan)
                    oCPPT.UpdatePemberiSBAR(grvHandOver.GetFocusedRowCellValue("KDCPPT"), CatatanKategoriTanggal.Replace("Tanggal ", ""), grdUSERHADNOVER.Text, False)
                    fn_LoadPdfViewerCPPTRANANP()
                End If
            End If
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub picVerifikasiDPJP_Click() Handles picVerifikasiDPJP.Click
        If grvHandOver.GetFocusedRowCellValue("KDCPPT") = String.Empty Then
            MsgBox("silahkan pilih Kode CPPT !!!", MsgBoxStyle.Exclamation, Me.Text)
            Exit Sub
        End If

        'If sISDOKTER = False Then
        '    MsgBox("silahkan pilih modul dokter", MsgBoxStyle.Exclamation, Me.Text)
        '    Exit Sub
        'End If

        If grdDPJPUtama.Text = "" Then
            MsgBox("DPJP Masih Kosong !!!", MsgBoxStyle.Exclamation, Me.Text)
            Exit Sub
        End If

        Dim sNoId = ""
        Dim oVerifikasi As New Inventory.clsReqCPPTACCPerawat

        Try
            'frmACCDokter.ShowDialog(Me)

            ' ***** HEADER *****
            Dim ds = oVerifikasi.GetStructureHeader
            With ds
                .DATECREATED = Now
                .DATEUPDATE = Now
                .KDACC = grvHandOver.GetFocusedRowCellValue("KDCPPT")
                .KDCPPT = grvHandOver.GetFocusedRowCellValue("KDCPPT")
                .CATEGORY = 0
                .STATUS = sUserID
                .KDUNIT = "======VERIFIKASI======"
                .DATE = Now
                .DESCRIPTION = grdDPJPUtama.Text
            End With

            Dim dsCekCPPT = oVerifikasi.GetDataKDCPPT(ds.KDCPPT)

            If dsCekCPPT Is Nothing Then
                oVerifikasi.InsertData(ds)
            Else
                oVerifikasi.UpdateData(ds)
            End If

            oCPPT.UpdatenikSEBAGAIVERIFIKASI(ds.KDCPPT, sUserID)

            fn_LoadPdfViewerCPPTRANANP()

        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try

        'Dim Catatan As String = String.Empty

        'If dsCPPT IsNot Nothing Then
        '    Catatan = dsCPPT.AGAMA
        'End If

        'If Catatan.Contains("Verifikasi DPJP") Then
        '    MsgBox("Sudah melakukan Verifikasi DPJP", MsgBoxStyle.Exclamation, Me.Text)
        'Else
        '    If MsgBox("Apakah Yakin " & "Verifikasi DPJP" & " ?", MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, Me.Text) = MsgBoxResult.No Then Exit Sub

        '    Dim frmACCDokter As New frmACCDokter
        '    Try
        '        frmACCDokter.ShowDialog(Me)

        '        oCPPT.UpdatePemberi(grvHandOver.GetFocusedRowCellValue("KDCPPT"), "Verifikasi DPJP" & vbCrLf & sRemarks)
        '        fn_LoadPdfViewerCPPTRANANP()

        '    Catch oErr As Exception
        '        MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        '    End Try
        'End If
    End Sub

#End Region
#Region "Transfer Internal"
    Private Sub fn_LoadDataTransferInternal()
        Try
            Dim ds = From x In oTransferInternal.GetDataByRM(lblNoRM.Text)
                     Select x.KDTRANSFERINTERNAL, x.KDREG, TANGGAL = x.DATE, x.KDUSER

            grdTransferInternal.DataSource = ds.ToList

            fn_LoadFormatDataTransferInternal()
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadFormatDataTransferInternal()
        For iLoop As Integer = 0 To grvTransferInternal.Columns.Count - 1
            If grvTransferInternal.Columns(iLoop).ColumnType.Name = "Decimal" Then
                grvTransferInternal.Columns(iLoop).DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                grvTransferInternal.Columns(iLoop).DisplayFormat.FormatString = "{0:n2}"
                grvTransferInternal.Columns(iLoop).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
            ElseIf grvTransferInternal.Columns(iLoop).ColumnType.Name = "DateTime" Then
                grvTransferInternal.Columns(iLoop).DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
                grvTransferInternal.Columns(iLoop).DisplayFormat.FormatString = "{0:dd/MM/yyyy}"
            End If
        Next
    End Sub
    Private Function fn_DeleteDataTransferInternal(ByVal sKDTransferInternal As String) As Boolean
        Try
            oTransferInternal.DeleteData(sKDTransferInternal)

            fn_DeleteDataTransferInternal = True
        Catch oErr As Exception
            fn_DeleteDataTransferInternal = False
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Function
    Private Sub grvTransferInternal_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles grvTransferInternal.DoubleClick
        If grvTransferInternal.GetFocusedRowCellValue("KDTRANSFERINTERNAL") Is Nothing Then
            Exit Sub
        End If

        Dim frmTransferInternal As New frmTransferInternal
        Try
            frmTransferInternal.LoadMe(FORM_MODE.FORM_MODE_VIEW, "", lblRegister.Text, sKDDOCTOR_INPUT, lblNoRM.Text, lblNamaPasien.Text, grvTransferInternal.GetFocusedRowCellValue("KDTRANSFERINTERNAL"), sKDUSER_PERAWAT)
            frmTransferInternal.ShowDialog(Me)
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub picAddTransferInternal_Click() Handles picAddTransferInternal.Click
        Dim frmTransferInternal As New frmTransferInternal
        Try
            frmTransferInternal.LoadMe(FORM_MODE.FORM_MODE_ADD, "", lblRegister.Text, sKDDOCTOR_INPUT, lblNoRM.Text, lblNamaPasien.Text, "", sKDUSER_PERAWAT)
            frmTransferInternal.ShowDialog(Me)
            XtraTabControl1_SelectedPageChanged()
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        Finally
            If Not frmTransferInternal Is Nothing Then frmTransferInternal.Dispose()
            frmTransferInternal = Nothing

            Dim rowHandle As Integer = grvTransferInternal.LocateByValue(rowHandle, grvTransferInternal.Columns("KDTRANSFERINTERNAL"), sCode)
            If rowHandle > 0 Then grvTransferInternal.FocusedRowHandle = rowHandle

            If sStatusSave = "NEW" Then
                sStatusSave = "NONE"
                picAddTransferInternal_Click()
            End If
        End Try
    End Sub
    Private Sub picUpdateTransferInternal_Click() Handles picUpdateTransferInternal.Click
        If grvTransferInternal.GetFocusedRowCellValue("KDTRANSFERINTERNAL") Is Nothing Then
            Exit Sub
        End If
        Dim frmTransferInternal As New frmTransferInternal
        Try
            frmTransferInternal.LoadMe(FORM_MODE.FORM_MODE_EDIT, "", lblRegister.Text, sKDDOCTOR_INPUT, lblNoRM.Text, lblNamaPasien.Text, grvTransferInternal.GetFocusedRowCellValue("KDTRANSFERINTERNAL"), sKDUSER_PERAWAT)
            frmTransferInternal.ShowDialog(Me)
            XtraTabControl1_SelectedPageChanged()
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        Finally
            If Not frmTransferInternal Is Nothing Then frmTransferInternal.Dispose()
            frmTransferInternal = Nothing

            Dim rowHandle As Integer = grvTransferInternal.LocateByValue(rowHandle, grvTransferInternal.Columns("KDTRANSFERINTERNAL"), sCode)
            If rowHandle > 0 Then grvTransferInternal.FocusedRowHandle = rowHandle

            If sStatusSave = "NEW" Then
                sStatusSave = "NONE"
                picAddTransferInternal_Click()
            End If
        End Try
    End Sub
    Private Sub picDeleteTransferInternal_Click() Handles picDeleteTransferInternal.Click
        If grvTransferInternal.GetFocusedRowCellValue("KDTRANSFERINTERNAL") Is Nothing Then
            Exit Sub
        End If
        If MsgBox(Statement.DeleteQuestion, MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, Me.Text) = MsgBoxResult.No Then Exit Sub
        If fn_DeleteDataTransferInternal(grvTransferInternal.GetFocusedRowCellValue("KDTRANSFERINTERNAL")) = False Then
            MsgBox(Statement.DeleteFail, MsgBoxStyle.Exclamation, Me.Text)
            Exit Sub
        End If
        MsgBox(Statement.DeleteSuccess, MsgBoxStyle.Information, Me.Text)
        XtraTabControl1_SelectedPageChanged()
    End Sub
    Private Sub picRefreshTransferInternal_Click() Handles picRefreshTransferInternal.Click
        XtraTabControl1_SelectedPageChanged()
    End Sub
    Private Sub CopyHandOverToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CopyHandOverToolStripMenuItem.Click
        If grvTransferInternal.GetFocusedRowCellValue("KDTRANSFERINTERNAL") = String.Empty Then Exit Sub

        Dim frmTransferInternal As New frmTransferInternal
        Try
            frmTransferInternal.LoadMe(FORM_MODE.FORM_MODE_ADD, grvTransferInternal.GetFocusedRowCellValue("KDTRANSFERINTERNAL"), lblRegister.Text, sKDDOCTOR_INPUT, lblNoRM.Text, lblNamaPasien.Text, "", sKDUSER_PERAWAT)
            frmTransferInternal.ShowDialog(Me)
            XtraTabControl1_SelectedPageChanged()
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        Finally
            If Not frmTransferInternal Is Nothing Then frmTransferInternal.Dispose()
            frmTransferInternal = Nothing

            Dim rowHandle As Integer = grvTransferInternal.LocateByValue(rowHandle, grvTransferInternal.Columns("KDTRANSFERINTERNAL"), sCode)
            If rowHandle > 0 Then grvTransferInternal.FocusedRowHandle = rowHandle

            If sStatusSave = "NEW" Then
                sStatusSave = "NONE"
                picAddTransferInternal_Click()
            End If
        End Try
    End Sub
#End Region
#Region "Tindakan Evaluasi Keperawatan"
    Private Sub fn_LoadDataTindakanEvaluasiKeperawatan()
        Try
            grvTindakanEvaluasiKeperawatan.Columns.Clear()
            grdTindakanEvaluasiKeperawatan.DataSource = Nothing

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
            SQL &= "KATEGORI = '1. B.D' "
            SQL &= ",TANGGAL = CONVERT(VARCHAR(8), A.DATE, 103) "
            SQL &= ",A.KDPENDAFTARAN "
            SQL &= ",A.KDDIAGNOSAPERAWAT "
            SQL &= ",A.KDITEMDIAGNOSAPERAWAT "
            SQL &= ",DESKRIPSI = C.BERHUBUNGANDENGAN "
            SQL &= ",EVALUASI = '' "
            SQL &= ",A.KDUSER "
            SQL &= ",C.SEQ "
            SQL &= "FROM "
            SQL &= "S_DIGITAL_DIAGNOSAPERAWAT_H A "
            SQL &= "INNER JOIN S_DIGITAL_DIAGNOSAPERAWAT_D_1 C "
            SQL &= "ON A.KDDIAGNOSAPERAWAT = C.KDDIAGNOSAPERAWAT "
            SQL &= "WHERE A.KDCUSTOMER = '" & lblNoRM.Text & "' "
            SQL &= "AND C.BERHUBUNGANDENGAN_ISCHEKED = 1 "

            SQL &= "UNION "

            SQL &= "SELECT "
            SQL &= "KATEGORI = '2. D.D' "
            SQL &= ",TANGGAL = CONVERT(VARCHAR(8), A.DATE, 103) "
            SQL &= ",A.KDPENDAFTARAN "
            SQL &= ",A.KDDIAGNOSAPERAWAT "
            SQL &= ",A.KDITEMDIAGNOSAPERAWAT "
            SQL &= ",DESKRIPSI = C.DITANDAIDENGAN "
            SQL &= ",EVALUASI = '' "
            SQL &= ",A.KDUSER "
            SQL &= ",C.SEQ "
            SQL &= "FROM "
            SQL &= "S_DIGITAL_DIAGNOSAPERAWAT_H A "
            SQL &= "INNER JOIN S_DIGITAL_DIAGNOSAPERAWAT_D_2 C "
            SQL &= "ON A.KDDIAGNOSAPERAWAT = C.KDDIAGNOSAPERAWAT "
            SQL &= "WHERE A.KDCUSTOMER = '" & lblNoRM.Text & "' "
            SQL &= "AND C.DITANDAIDENGAN_ISCHEKED = 1 "

            SQL &= "UNION "

            SQL &= "SELECT "
            SQL &= "KATEGORI = '3. TUJUAN' "
            SQL &= ",TANGGAL = CONVERT(VARCHAR(8), A.DATE, 103) "
            SQL &= ",A.KDPENDAFTARAN "
            SQL &= ",A.KDDIAGNOSAPERAWAT "
            SQL &= ",A.KDITEMDIAGNOSAPERAWAT "
            SQL &= ",DESKRIPSI = C.TUJUAN "
            SQL &= ",EVALUASI = '' "
            SQL &= ",A.KDUSER "
            SQL &= ",C.SEQ "
            SQL &= "FROM "
            SQL &= "S_DIGITAL_DIAGNOSAPERAWAT_H A "
            SQL &= "INNER JOIN S_DIGITAL_DIAGNOSAPERAWAT_D_3 C "
            SQL &= "ON A.KDDIAGNOSAPERAWAT = C.KDDIAGNOSAPERAWAT "
            SQL &= "WHERE A.KDCUSTOMER = '" & lblNoRM.Text & "' "
            SQL &= "AND C.TUJUAN_ISCHEKED = 1 "

            SQL &= "UNION "

            SQL &= "SELECT "
            SQL &= "KATEGORI = '4. KRITERIA' "
            SQL &= ",TANGGAL = CONVERT(VARCHAR(8), A.DATE, 103) "
            SQL &= ",A.KDPENDAFTARAN "
            SQL &= ",A.KDDIAGNOSAPERAWAT "
            SQL &= ",A.KDITEMDIAGNOSAPERAWAT "
            SQL &= ",DESKRIPSI = C.KRITERIA "
            SQL &= ",EVALUASI = '' "
            SQL &= ",A.KDUSER "
            SQL &= ",C.SEQ "
            SQL &= "FROM "
            SQL &= "S_DIGITAL_DIAGNOSAPERAWAT_H A "
            SQL &= "INNER JOIN S_DIGITAL_DIAGNOSAPERAWAT_D_4 C "
            SQL &= "ON A.KDDIAGNOSAPERAWAT = C.KDDIAGNOSAPERAWAT "
            SQL &= "WHERE A.KDCUSTOMER = '" & lblNoRM.Text & "' "
            SQL &= "AND C.KRITERIA_ISCHEKED = 1 "

            SQL &= "UNION "

            SQL &= "SELECT "
            SQL &= "KATEGORI = '5. INTERVENSI' "
            SQL &= ",TANGGAL = CONVERT(VARCHAR(8), A.DATE, 103) "
            SQL &= ",A.KDPENDAFTARAN "
            SQL &= ",A.KDDIAGNOSAPERAWAT "
            SQL &= ",A.KDITEMDIAGNOSAPERAWAT "
            SQL &= ",DESKRIPSI = C.INTERVENSI "
            SQL &= ",EVALUASI = '' "
            SQL &= ",A.KDUSER "
            SQL &= ",C.SEQ "
            SQL &= "FROM "
            SQL &= "S_DIGITAL_DIAGNOSAPERAWAT_H A "
            SQL &= "INNER JOIN S_DIGITAL_DIAGNOSAPERAWAT_D_5 C "
            SQL &= "ON A.KDDIAGNOSAPERAWAT = C.KDDIAGNOSAPERAWAT "
            SQL &= "WHERE A.KDCUSTOMER = '" & lblNoRM.Text & "' "
            SQL &= "AND C.INTERVENSI_ISCHEKED = 1 "

            SQL &= "UNION "

            SQL &= "SELECT "
            SQL &= "KATEGORI = '6. IMPLEMENTASI' "
            SQL &= ",C.TANGGAL "
            SQL &= ",A.KDPENDAFTARAN "
            SQL &= ",A.KDDIAGNOSAPERAWAT "
            SQL &= ",A.KDITEMDIAGNOSAPERAWAT "
            SQL &= ",DESKRIPSI = C.IMPLEMENTASI "
            SQL &= ",C.EVALUASI "
            SQL &= ",C.KDUSER "
            SQL &= ",C.SEQ "
            SQL &= "FROM "
            SQL &= "S_DIGITAL_DIAGNOSAPERAWAT_H A "
            SQL &= "INNER JOIN S_DIGITAL_DIAGNOSAPERAWAT_D_6 C "
            SQL &= "ON A.KDDIAGNOSAPERAWAT = C.KDDIAGNOSAPERAWAT "
            SQL &= "WHERE A.KDCUSTOMER = '" & lblNoRM.Text & "' "
            SQL &= "AND C.IMPLEMENTASI_ISCHEKED = 1 "

            SQL &= ") Z "
            SQL &= "ORDER BY Z.KDPENDAFTARAN, Z.SEQ "

            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "TINDAKANEVALUASIKEPERAWATAN")

            grdTindakanEvaluasiKeperawatan.MainView = grvTindakanEvaluasiKeperawatan
            grdTindakanEvaluasiKeperawatan.DataSource = ds.Tables("TINDAKANEVALUASIKEPERAWATAN")
            grdTindakanEvaluasiKeperawatan.ForceInitialize()

            fn_LoadFormatDataTindakanEvaluasiKeperawatan()

            If oConn.State = ConnectionState.Open Then
                oConn.Close()
            End If
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadFormatDataTindakanEvaluasiKeperawatan()
        For iLoop As Integer = 0 To grvTindakanEvaluasiKeperawatan.Columns.Count - 1
            If grvTindakanEvaluasiKeperawatan.Columns(iLoop).ColumnType.Name = "Decimal" Then
                grvTindakanEvaluasiKeperawatan.Columns(iLoop).DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                grvTindakanEvaluasiKeperawatan.Columns(iLoop).DisplayFormat.FormatString = "{0:n2}"
                grvTindakanEvaluasiKeperawatan.Columns(iLoop).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
            ElseIf grvTindakanEvaluasiKeperawatan.Columns(iLoop).ColumnType.Name = "DateTime" Then
                grvTindakanEvaluasiKeperawatan.Columns(iLoop).DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
                grvTindakanEvaluasiKeperawatan.Columns(iLoop).DisplayFormat.FormatString = "{0:dd/MM/yyyy}"
            End If
        Next

        grvTindakanEvaluasiKeperawatan.Columns("KDUSER").Caption = "User"
        grvTindakanEvaluasiKeperawatan.Columns("SEQ").Visible = False
        grvTindakanEvaluasiKeperawatan.Columns("KDPENDAFTARAN").Visible = False

        grvTindakanEvaluasiKeperawatan.Columns("KDDIAGNOSAPERAWAT").Group()
        grvTindakanEvaluasiKeperawatan.Columns("KDITEMDIAGNOSAPERAWAT").Group()
        grvTindakanEvaluasiKeperawatan.Columns("KATEGORI").Group()
        grvTindakanEvaluasiKeperawatan.ExpandAllGroups()
    End Sub
    Private Function fn_DeleteDataTindakanEvaluasiKeperawatan(ByVal sKDTransferInternal As String) As Boolean
        Try
            fn_DeleteDataTindakanEvaluasiKeperawatan = oTindakanEvaluasiKeperawatan.DeleteData(sKDTransferInternal)
        Catch oErr As Exception
            fn_DeleteDataTindakanEvaluasiKeperawatan = False
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Function
    Private Sub grvTindakanEvaluasiKeperawatan_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles grvTindakanEvaluasiKeperawatan.DoubleClick
        If grvTindakanEvaluasiKeperawatan.GetFocusedRowCellValue("KDDIAGNOSAPERAWAT") Is Nothing Then
            Exit Sub
        End If

        Dim frmDiagnosaPerawat As New frmDiagnosaPerawat
        Try
            frmDiagnosaPerawat.LoadMe(FORM_MODE.FORM_MODE_VIEW, grvTindakanEvaluasiKeperawatan.GetFocusedRowCellValue("KDPENDAFTARAN"), lblNoRM.Text, sKDUSER_PERAWAT, grvTindakanEvaluasiKeperawatan.GetFocusedRowCellValue("KDDIAGNOSAPERAWAT"))
            frmDiagnosaPerawat.ShowDialog(Me)
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub picAddTindakanEvaluasiKeperawatan_Click() Handles picAddTindakanEvaluasiKeperawatan.Click
        Dim frmDiagnosaPerawat As New frmDiagnosaPerawat
        Try
            frmDiagnosaPerawat.LoadMe(FORM_MODE.FORM_MODE_ADD, lblRegister.Text, lblNoRM.Text, sKDUSER_PERAWAT)
            frmDiagnosaPerawat.ShowDialog(Me)

            XtraTabControl1_SelectedPageChanged()
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        Finally
            If Not frmDiagnosaPerawat Is Nothing Then frmDiagnosaPerawat.Dispose()
            frmDiagnosaPerawat = Nothing

            Dim rowHandle As Integer = grvTindakanEvaluasiKeperawatan.LocateByValue(rowHandle, grvTindakanEvaluasiKeperawatan.Columns("KDDIAGNOSAPERAWAT"), sCode)
            If rowHandle > 0 Then grvTindakanEvaluasiKeperawatan.FocusedRowHandle = rowHandle

            If sStatusSave = "NEW" Then
                sStatusSave = "NONE"
                picAddTindakanEvaluasiKeperawatan_Click()
            End If
        End Try
    End Sub
    Private Sub picEditTindakanEvaluasiKeperawatan_Click() Handles picEditTindakanEvaluasiKeperawatan.Click
        If grvTindakanEvaluasiKeperawatan.GetFocusedRowCellValue("KDDIAGNOSAPERAWAT") Is Nothing Then
            Exit Sub
        End If
        Dim frmDiagnosaPerawat As New frmDiagnosaPerawat
        Try
            frmDiagnosaPerawat.LoadMe(FORM_MODE.FORM_MODE_EDIT, grvTindakanEvaluasiKeperawatan.GetFocusedRowCellValue("KDPENDAFTARAN"), lblNoRM.Text, sKDUSER_PERAWAT, grvTindakanEvaluasiKeperawatan.GetFocusedRowCellValue("KDDIAGNOSAPERAWAT"))
            frmDiagnosaPerawat.ShowDialog(Me)
            XtraTabControl1_SelectedPageChanged()
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        Finally
            If Not frmDiagnosaPerawat Is Nothing Then frmDiagnosaPerawat.Dispose()
            frmDiagnosaPerawat = Nothing

            Dim rowHandle As Integer = grvTindakanEvaluasiKeperawatan.LocateByValue(rowHandle, grvTindakanEvaluasiKeperawatan.Columns("KDDIAGNOSAPERAWAT"), sCode)
            If rowHandle > 0 Then grvTindakanEvaluasiKeperawatan.FocusedRowHandle = rowHandle

            If sStatusSave = "NEW" Then
                sStatusSave = "NONE"
                picAddTindakanEvaluasiKeperawatan_Click()
            End If
        End Try
    End Sub
    Private Sub picDeleteTindakanEvaluasiKeperawatan_Click() Handles picDeleteTindakanEvaluasiKeperawatan.Click
        If grvTindakanEvaluasiKeperawatan.GetFocusedRowCellValue("KDDIAGNOSAPERAWAT") Is Nothing Then
            Exit Sub
        End If
        If MsgBox(Statement.DeleteQuestion, MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, Me.Text) = MsgBoxResult.No Then Exit Sub
        If fn_DeleteDataTindakanEvaluasiKeperawatan(grvTindakanEvaluasiKeperawatan.GetFocusedRowCellValue("KDDIAGNOSAPERAWAT")) = False Then
            MsgBox(Statement.DeleteFail, MsgBoxStyle.Exclamation, Me.Text)
            Exit Sub
        End If
        MsgBox(Statement.DeleteSuccess, MsgBoxStyle.Information, Me.Text)
        XtraTabControl1_SelectedPageChanged()
    End Sub
    Private Sub picRefreshTindakanEvaluasiKeperawatan_Click() Handles picRefreshTindakanEvaluasiKeperawatan.Click
        XtraTabControl1_SelectedPageChanged()
    End Sub
#End Region
#Region "Asesmen Awal Perawat"
    Private Sub fn_LoadDataAsesmenAwalPerawat(ByVal Parameter As String)
        Try
            grvAsesmenAwalPerawat.Columns.Clear()
            grdAsesmenAwalPerawat.DataSource = Nothing

            If Parameter = String.Empty Then Exit Sub

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

            'Penyakit Dalam, Bedah, Jantung
            SQL = "SELECT "
            SQL &= "* "
            SQL &= "FROM ( "
            SQL &= "(SELECT "
            SQL &= "Tanggal = A.DATE "
            'SQL &= ",TanggalDiPerbaharui = A.DATEUPDATED "
            SQL &= ",FORMULIR = 'ASESMEN AWAL KEPERAWATAN BEDAH, PENYAKIT DALAM DAN JANTUNG' "
            SQL &= ",NoRegister = A.KDPENDAFTARAN "
            SQL &= ",Kode = A.KDPENDAFTARAN "
            SQL &= ",Deskripsi = '' "
            SQL &= ",DPJP = A.DOKTER2_NAMEDISPLAY "
            SQL &= ",A.KDUSER "
            SQL &= "FROM "
            SQL &= "S_DIGITAL_RI_17 A "
            SQL &= "WHERE A.KDCUSTOMER = '" & Parameter & "' "

            'SQL &= ")UNION ALL( "

            'SQL &= "SELECT "
            'SQL &= "Tanggal = A.DATE "
            ''SQL &= ",TanggalDiPerbaharui = A.DATEUPDATED "
            'SQL &= ",FORMULIR = 'ASESMEN AWAL KEPERAWATAN ICU' "
            'SQL &= ",Kode = A.KDPENDAFTARAN "
            'SQL &= ",Deskripsi = '' "
            'SQL &= ",A.KDUSER "
            'SQL &= "FROM "
            'SQL &= "S_DIGITAL_RI_24 A "
            'SQL &= "WHERE A.KDPENDAFTARAN = '" & Parameter & "' "

            'SQL &= ")UNION ALL( "

            'SQL &= "SELECT "
            'SQL &= "Tanggal = A.DATE "
            ''SQL &= ",TanggalDiPerbaharui = A.DATEUPDATED "
            'SQL &= ",FORMULIR = 'ASESMEN KEPERAWATAN REHABMEDIK' "
            'SQL &= ",Kode = A.KDPENDAFTARAN "
            'SQL &= ",Deskripsi = '' "
            'SQL &= ",A.KDUSER "
            'SQL &= "FROM "
            'SQL &= "S_DIGITAL_RI_25 A "
            'SQL &= "WHERE A.KDPENDAFTARAN = '" & Parameter & "' "

            SQL &= ")UNION ALL( "

            SQL &= "SELECT "
            SQL &= "Tanggal = A.DATE "
            'SQL &= ",TanggalDiPerbaharui = A.DATEUPDATED "
            SQL &= ",FORMULIR = 'ASESMEN AWAL KEPERAWATAN ANAK' "
            SQL &= ",NoRegister = A.KDPENDAFTARAN "
            SQL &= ",Kode = A.KDPENDAFTARAN "
            SQL &= ",Deskripsi = '' "
            SQL &= ",DPJP = A.DOKTER_NAMEDISPLAY "
            SQL &= ",A.KDUSER "
            SQL &= "FROM "
            SQL &= "S_DIGITAL_RI_40 A "
            SQL &= "WHERE A.KDCUSTOMER = '" & Parameter & "' "

            SQL &= ")UNION ALL( "

            SQL &= "SELECT "
            SQL &= "Tanggal = A.ASESMEN_01 "
            'SQL &= ",TanggalDiPerbaharui = A.DATEUPDATED "
            SQL &= ",FORMULIR = 'ASESMEN AWAL KEPERAWATAN RAWAT INAP' "
            SQL &= ",NoRegister = A.KDPENDAFTARAN "
            SQL &= ",Kode = A.KDPENDAFTARAN "
            SQL &= ",Deskripsi = '' "
            SQL &= ",DPJP = A.DOKTER2_NAMEDISPLAY "
            SQL &= ",A.KDUSER "
            SQL &= "FROM "
            SQL &= "S_DIGITAL_RI_38 A "
            SQL &= "WHERE A.KDCUSTOMER = '" & Parameter & "' "

            'SQL &= ")UNION ALL( "

            'SQL &= "SELECT "
            'SQL &= "Tanggal = A.DATE "
            ''SQL &= ",TanggalDiPerbaharui = A.DATEUPDATED "
            'SQL &= ",FORMULIR = 'ASESMEN AWAL KEPERAWATAN PASIEN HEMODIALISA' "
            'SQL &= ",Kode = A.KDPENDAFTARAN "
            'SQL &= ",Deskripsi = '' "
            'SQL &= ",A.KDUSER "
            'SQL &= "FROM "
            'SQL &= "S_DIGITAL_RJ_38 A "
            'SQL &= "WHERE A.KDPENDAFTARAN = '" & Parameter & "' "

            SQL &= ")UNION ALL( "

            SQL &= "SELECT "
            SQL &= "Tanggal = A.DATE "
            'SQL &= ",TanggalDiPerbaharui = A.DATEUPDATED "
            SQL &= ",FORMULIR = 'ASESMEN AWAL KEPERAWATAN KEBIDANAN' "
            SQL &= ",NoRegister = A.KDPENDAFTARAN "
            SQL &= ",Kode = A.KDPENDAFTARAN "
            SQL &= ",Deskripsi = '' "
            SQL &= ",DPJP = A.DOKTER_NAMEDISPLAY "
            SQL &= ",A.KDUSER "
            SQL &= "FROM "
            SQL &= "S_DIGITAL_RI_29 A "
            SQL &= "WHERE A.KDCUSTOMER = '" & Parameter & "' "

            'SQL &= ")UNION ALL( "

            'SQL &= "SELECT "
            'SQL &= "Tanggal = A.DATE "
            ''SQL &= ",TanggalDiPerbaharui = A.DATEUPDATED "
            'SQL &= ",FORMULIR = 'ASESMEN AWAL KEPERAWATAN ENDOSKOPI' "
            'SQL &= ",Kode = A.KDPENDAFTARAN "
            'SQL &= ",Deskripsi = '' "
            'SQL &= ",A.KDUSER "
            'SQL &= "FROM "
            'SQL &= "S_DIGITAL_RJ_05 A "
            'SQL &= "WHERE A.KDPENDAFTARAN = '" & Parameter & "' "

            'SQL &= ")UNION ALL( "

            'SQL &= "SELECT "
            'SQL &= "Tanggal = A.DATE "
            ''SQL &= ",TanggalDiPerbaharui = A.DATEUPDATED "
            'SQL &= ",FORMULIR = 'PENGKAJIAN AWAL KEPERAWATAN RAWAT INAP JIWA' "
            'SQL &= ",Kode = A.KDPENDAFTARAN "
            'SQL &= ",Deskripsi = '' "
            'SQL &= ",A.KDUSER "
            'SQL &= "FROM "
            'SQL &= "S_DIGITAL_RI_44 A "
            'SQL &= "WHERE A.KDPENDAFTARAN = '" & Parameter & "' "

            'SQL &= ")UNION ALL( "

            'SQL &= "SELECT "
            'SQL &= "Tanggal = A.DATE "
            ''SQL &= ",TanggalDiPerbaharui = A.DATEUPDATED "
            'SQL &= ",FORMULIR = 'ASESMEN AWAL KEPERAWATAN MATERNITAS' "
            'SQL &= ",Kode = A.KDPENDAFTARAN "
            'SQL &= ",Deskripsi = '' "
            'SQL &= ",A.KDUSER "
            'SQL &= "FROM "
            'SQL &= "S_DIGITAL_RI_OBSTETRI_PERAWAT A "
            'SQL &= "WHERE A.KDPENDAFTARAN = '" & Parameter & "' "

            SQL &= ")UNION ALL( "

            SQL &= "SELECT "
            SQL &= "Tanggal = A.DATE "
            'SQL &= ",TanggalDiPerbaharui = A.DATEUPDATED "
            SQL &= ",FORMULIR = 'ASESMEN AWAL KEPERAWATAN PASIEN NEONATAL' "
            SQL &= ",NoRegister = A.KDPENDAFTARAN "
            SQL &= ",Kode = A.KDASESMEN "
            SQL &= ",Deskripsi = A.KDASESMEN "
            SQL &= ",DPJP = '-' "
            SQL &= ",A.KDUSER "
            SQL &= "FROM "
            SQL &= "S_DIGITAL_ASKEP_NEONATAL A "
            SQL &= "WHERE A.KDCUSTOMER = '" & Parameter & "' "

            SQL &= ")UNION ALL( "

            SQL &= "SELECT "
            SQL &= "Tanggal = A.DATE "
            'SQL &= ",TanggalDiPerbaharui = A.DATEUPDATED "
            SQL &= ",FORMULIR = 'LEMBAR DISCHARGE PLANNING' "
            SQL &= ",NoRegister = A.KDPENDAFTARAN "
            SQL &= ",Kode = A.KDPENDAFTARAN "
            SQL &= ",Deskripsi = A.DIAGNOSAKEPERAWATAN "
            SQL &= ",DPJP = '-' "
            SQL &= ",A.KDUSER "
            SQL &= "FROM "
            SQL &= "S_DIGITAL_LEMBARDISCHARGEPLANNING A "
            SQL &= "WHERE A.KDCUSTOMER = '" & Parameter & "' "


            SQL &= ") "
            SQL &= ") AS X "
            SQL &= "ORDER BY X.Tanggal DESC "

            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "ASESMENAWALPERAWAT")

            grdAsesmenAwalPerawat.MainView = grvAsesmenAwalPerawat
            grdAsesmenAwalPerawat.DataSource = ds.Tables("ASESMENAWALPERAWAT")
            grdAsesmenAwalPerawat.ForceInitialize()

            fn_LoadFormatDataAsesmenAwalPerawat()

            If oConn.State = ConnectionState.Open Then
                oConn.Close()
            End If
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadFormatDataAsesmenAwalPerawat()
        For iLoop As Integer = 0 To grvAsesmenAwalPerawat.Columns.Count - 1
            If grvAsesmenAwalPerawat.Columns(iLoop).ColumnType.Name = "Decimal" Then
                grvAsesmenAwalPerawat.Columns(iLoop).DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                grvAsesmenAwalPerawat.Columns(iLoop).DisplayFormat.FormatString = "{0:n2}"
                grvAsesmenAwalPerawat.Columns(iLoop).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
            ElseIf grvAsesmenAwalPerawat.Columns(iLoop).ColumnType.Name = "DateTime" Then
                grvAsesmenAwalPerawat.Columns(iLoop).DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
                grvAsesmenAwalPerawat.Columns(iLoop).DisplayFormat.FormatString = "{0:dd/MM/yyyy}"
            End If
        Next

        grvAsesmenAwalPerawat.Columns("FORMULIR").Caption = "Formulir"
        grvAsesmenAwalPerawat.Columns("KDUSER").Caption = "User"

        grvAsesmenAwalPerawat.Columns("Kode").VisibleIndex = -1
        grvAsesmenAwalPerawat.Columns("Deskripsi").VisibleIndex = -1
        grvAsesmenAwalPerawat.BestFitColumns()
    End Sub
    Private Sub grvAsesmenAwalPerawat_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles grvAsesmenAwalPerawat.DoubleClick
        If grvAsesmenAwalPerawat.GetFocusedRowCellValue("Kode") Is Nothing Then
            Exit Sub
        End If
        If grvAsesmenAwalPerawat.GetFocusedRowCellValue("FORMULIR") = "ASESMEN AWAL KEPERAWATAN BEDAH, PENYAKIT DALAM DAN JANTUNG" Then
            Dim frmEMedrekRI_17 As New frmEMedrekRI_17
            Try
                frmEMedrekRI_17.LoadMe(FORM_MODE.FORM_MODE_VIEW, grvAsesmenAwalPerawat.GetFocusedRowCellValue("Kode"), lblNoRM.Text, lblNamaPasien.Text, lblJenisKelamin.Text, grvAsesmenAwalPerawat.GetFocusedRowCellValue("KDUSER"), grvAsesmenAwalPerawat.GetFocusedRowCellValue("DPJP"))
                frmEMedrekRI_17.ShowDialog(Me)
            Catch oErr As Exception
                MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            End Try
        ElseIf grvAsesmenAwalPerawat.GetFocusedRowCellValue("FORMULIR") = "ASESMEN AWAL KEPERAWATAN ICU" Then

        ElseIf grvAsesmenAwalPerawat.GetFocusedRowCellValue("FORMULIR") = "ASESMEN KEPERAWATAN REHABMEDIK" Then

        ElseIf grvAsesmenAwalPerawat.GetFocusedRowCellValue("FORMULIR") = "ASESMEN AWAL KEPERAWATAN ANAK" Then
            Dim frmEMedrekRI_40 As New frmEMedrekRI_40
            Try
                frmEMedrekRI_40.LoadMe(FORM_MODE.FORM_MODE_VIEW, grvAsesmenAwalPerawat.GetFocusedRowCellValue("Kode"), lblNoRM.Text, lblNamaPasien.Text, lblJenisKelamin.Text, grvAsesmenAwalPerawat.GetFocusedRowCellValue("KDUSER"), grvAsesmenAwalPerawat.GetFocusedRowCellValue("DPJP"))
                frmEMedrekRI_40.ShowDialog(Me)
            Catch oErr As Exception
                MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            End Try
        ElseIf grvAsesmenAwalPerawat.GetFocusedRowCellValue("FORMULIR") = "ASESMEN AWAL KEPERAWATAN RAWAT INAP" Then
            Dim frmEMedrekRI_38 As New frmEMedrekRI_38
            Try
                frmEMedrekRI_38.LoadMe(FORM_MODE.FORM_MODE_VIEW, grvAsesmenAwalPerawat.GetFocusedRowCellValue("Kode"), lblNoRM.Text, lblNamaPasien.Text, lblJenisKelamin.Text, grvAsesmenAwalPerawat.GetFocusedRowCellValue("KDUSER"), grvAsesmenAwalPerawat.GetFocusedRowCellValue("DPJP"))
                frmEMedrekRI_38.ShowDialog(Me)
            Catch oErr As Exception
                MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            End Try
        ElseIf grvAsesmenAwalPerawat.GetFocusedRowCellValue("FORMULIR") = "ASESMEN AWAL KEPERAWATAN PASIEN HEMODIALISA" Then

        ElseIf grvAsesmenAwalPerawat.GetFocusedRowCellValue("FORMULIR") = "ASESMEN AWAL KEPERAWATAN KEBIDANAN" Then
            Dim frmEMedrekRI_29 As New frmEMedrekRI_29
            Try
                frmEMedrekRI_29.LoadMe(FORM_MODE.FORM_MODE_VIEW, grvAsesmenAwalPerawat.GetFocusedRowCellValue("Kode"), lblNoRM.Text, lblNamaPasien.Text, lblJenisKelamin.Text, grvAsesmenAwalPerawat.GetFocusedRowCellValue("KDUSER"), grvAsesmenAwalPerawat.GetFocusedRowCellValue("DPJP"))
                frmEMedrekRI_29.ShowDialog(Me)
            Catch oErr As Exception
                MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            End Try
        ElseIf grvAsesmenAwalPerawat.GetFocusedRowCellValue("FORMULIR") = "ASESMEN AWAL KEPERAWATAN ENDOSKOPI" Then

        ElseIf grvAsesmenAwalPerawat.GetFocusedRowCellValue("FORMULIR") = "PENGKAJIAN AWAL KEPERAWATAN RAWAT INAP JIWA" Then

        ElseIf grvAsesmenAwalPerawat.GetFocusedRowCellValue("FORMULIR") = "ASESMEN AWAL KEPERAWATAN MATERNITAS" Then

        ElseIf grvAsesmenAwalPerawat.GetFocusedRowCellValue("FORMULIR") = "ASESMEN AWAL KEPERAWATAN PASIEN NEONATAL" Then
            Dim frmAsesmenAwalKeperawatanNeonatal As New frmAsesmenAwalKeperawatanNeonatal
            Try
                frmAsesmenAwalKeperawatanNeonatal.LoadMe(FORM_MODE.FORM_MODE_VIEW, grvAsesmenAwalPerawat.GetFocusedRowCellValue("NoRegister"), lblNoRM.Text, lblNamaPasien.Text, lblJenisKelamin.Text, grvAsesmenAwalPerawat.GetFocusedRowCellValue("KDUSER"), grvAsesmenAwalPerawat.GetFocusedRowCellValue("DPJP"), grvAsesmenAwalPerawat.GetFocusedRowCellValue("Kode"))
                frmAsesmenAwalKeperawatanNeonatal.ShowDialog(Me)
            Catch oErr As Exception
                MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            End Try
        ElseIf grvAsesmenAwalPerawat.GetFocusedRowCellValue("FORMULIR") = "LEMBAR DISCHARGE PLANNING" Then
            Dim frmLembarDischargePlanning As New frmLembarDischargePlanning
            Try
                frmLembarDischargePlanning.LoadMe(FORM_MODE.FORM_MODE_VIEW, grvAsesmenAwalPerawat.GetFocusedRowCellValue("Kode"), sDOKTERUTAMA, lblNoRM.Text, lblNamaPasien.Text, lblJenisKelamin.Text, lblRuangan.Text, grvAsesmenAwalPerawat.GetFocusedRowCellValue("KDUSER"))
                frmLembarDischargePlanning.ShowDialog(Me)
            Catch oErr As Exception
                MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            End Try
        End If
    End Sub
    Private Function fn_Cek(ByVal Aturan As Integer, ByVal Judul As String, ByVal Waktu As DateTime) As String
        fn_Cek = ""

        Dim cek As DateTime = Waktu.AddHours(Aturan * 24)

        If Now > cek Then
            fn_Cek = Judul & " Sudah Melewati " & Aturan & " x 24 Jam"
        End If
    End Function
    Private Sub PicAddAsesmenAwalPerawat_Click() Handles PicAddAsesmenAwalPerawat.Click
        If sISDOKTER = True Then
            MsgBox("Silahkan Pilih Modul Perawat", MsgBoxStyle.Exclamation, Me.Text)
            Exit Sub
        End If

        If cboAsesmenAwalPerawat.SelectedIndex = 1 Then
            Dim Pesan As String = fn_Cek(2, "Asesmen Awal Perawat", deTANGGALMASUK.DateTime)

            If Pesan <> "" Then
                MsgBox(Pesan, MsgBoxStyle.Information, Me.Text)
                Exit Sub
            End If
        End If


        Dim list As New List(Of String)

        For i As Integer = 0 To grvAsesmenAwalPerawat.RowCount - 1
            list.Add(grvAsesmenAwalPerawat.GetRowCellValue(i, "FORMULIR") & grvAsesmenAwalPerawat.GetRowCellValue(i, "Kode"))
        Next

        For Each xloop In list
            If cboAsesmenAwalPerawat.Text & lblRegister.Text = xloop Then
                MsgBox("Formulir " & cboAsesmenAwalPerawat.Text & " Sudah di buat", MsgBoxStyle.Exclamation, Me.Text)
                Exit Sub
            End If
        Next

        If cboAsesmenAwalPerawat.Text = "LEMBAR DISCHARGE PLANNING" Then
            Dim frmLembarDischargePlanning As New frmLembarDischargePlanning
            Try
                frmLembarDischargePlanning.LoadMe(FORM_MODE.FORM_MODE_ADD, lblRegister.Text, sDOKTERUTAMA, lblNoRM.Text, lblNamaPasien.Text, lblJenisKelamin.Text, lblRuangan.Text, sKDUSER_PERAWAT)
                frmLembarDischargePlanning.ShowDialog(Me)
                XtraTabControl1_SelectedPageChanged()
            Catch oErr As Exception
                MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            Finally
                If Not frmLembarDischargePlanning Is Nothing Then frmLembarDischargePlanning.Dispose()
                frmLembarDischargePlanning = Nothing

                Dim rowHandle As Integer = grvTransferInternal.LocateByValue(rowHandle, grvAsesmenAwalPerawat.Columns("Kode"), sCode)
                If rowHandle > 0 Then grvTransferInternal.FocusedRowHandle = rowHandle
            End Try
        ElseIf cboAsesmenAwalPerawat.Text = "ASESMEN AWAL KEPERAWATAN RAWAT INAP" Then
            Dim frmEMedrekRI_38 As New frmEMedrekRI_38
            Try
                frmEMedrekRI_38.LoadMe(FORM_MODE.FORM_MODE_ADD, lblRegister.Text, lblNoRM.Text, lblNamaPasien.Text, lblJenisKelamin.Text, sKDUSER_PERAWAT, sDOKTERUTAMA)
                frmEMedrekRI_38.ShowDialog(Me)
                XtraTabControl1_SelectedPageChanged()
            Catch oErr As Exception
                MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            Finally
                If Not frmEMedrekRI_38 Is Nothing Then frmEMedrekRI_38.Dispose()
                frmEMedrekRI_38 = Nothing

                Dim rowHandle As Integer = grvTransferInternal.LocateByValue(rowHandle, grvAsesmenAwalPerawat.Columns("Kode"), sCode)
                If rowHandle > 0 Then grvTransferInternal.FocusedRowHandle = rowHandle
            End Try
        ElseIf cboAsesmenAwalPerawat.Text = "ASESMEN AWAL KEPERAWATAN BEDAH, PENYAKIT DALAM DAN JANTUNG" Then
            Dim frmEMedrekRI_17 As New frmEMedrekRI_17
            Try
                frmEMedrekRI_17.LoadMe(FORM_MODE.FORM_MODE_ADD, lblRegister.Text, lblNoRM.Text, lblNamaPasien.Text, lblJenisKelamin.Text, sKDUSER_PERAWAT, sDOKTERUTAMA)
                frmEMedrekRI_17.ShowDialog(Me)
                XtraTabControl1_SelectedPageChanged()
            Catch oErr As Exception
                MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            Finally
                If Not frmEMedrekRI_17 Is Nothing Then frmEMedrekRI_17.Dispose()
                frmEMedrekRI_17 = Nothing

                Dim rowHandle As Integer = grvTransferInternal.LocateByValue(rowHandle, grvAsesmenAwalPerawat.Columns("Kode"), sCode)
                If rowHandle > 0 Then grvTransferInternal.FocusedRowHandle = rowHandle
            End Try
        ElseIf cboAsesmenAwalPerawat.Text = "ASESMEN AWAL KEPERAWATAN KEBIDANAN" Then
            Dim frmEMedrekRI_29 As New frmEMedrekRI_29
            Try
                frmEMedrekRI_29.LoadMe(FORM_MODE.FORM_MODE_ADD, lblRegister.Text, lblNoRM.Text, lblNamaPasien.Text, lblJenisKelamin.Text, sKDUSER_PERAWAT, sDOKTERUTAMA)
                frmEMedrekRI_29.ShowDialog(Me)
                XtraTabControl1_SelectedPageChanged()
            Catch oErr As Exception
                MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            Finally
                If Not frmEMedrekRI_29 Is Nothing Then frmEMedrekRI_29.Dispose()
                frmEMedrekRI_29 = Nothing

                Dim rowHandle As Integer = grvTransferInternal.LocateByValue(rowHandle, grvAsesmenAwalPerawat.Columns("Kode"), sCode)
                If rowHandle > 0 Then grvTransferInternal.FocusedRowHandle = rowHandle
            End Try
        ElseIf cboAsesmenAwalPerawat.Text = "ASESMEN AWAL KEPERAWATAN ANAK" Then
            Dim frmEMedrekRI_40 As New frmEMedrekRI_40
            Try
                frmEMedrekRI_40.LoadMe(FORM_MODE.FORM_MODE_ADD, lblRegister.Text, lblNoRM.Text, lblNamaPasien.Text, lblJenisKelamin.Text, sKDUSER_PERAWAT, sDOKTERUTAMA)
                frmEMedrekRI_40.ShowDialog(Me)
                XtraTabControl1_SelectedPageChanged()
            Catch oErr As Exception
                MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            Finally
                If Not frmEMedrekRI_40 Is Nothing Then frmEMedrekRI_40.Dispose()
                frmEMedrekRI_40 = Nothing

                Dim rowHandle As Integer = grvTransferInternal.LocateByValue(rowHandle, grvAsesmenAwalPerawat.Columns("Kode"), sCode)
                If rowHandle > 0 Then grvTransferInternal.FocusedRowHandle = rowHandle
            End Try
        ElseIf cboAsesmenAwalPerawat.Text = "ASESMEN AWAL KEPERAWATAN PASIEN NEONATAL" Then
            Dim frmEMedrekRI_40 As New frmEMedrekRI_40
            Try
                frmAsesmenAwalKeperawatanNeonatal.LoadMe(FORM_MODE.FORM_MODE_ADD, lblRegister.Text, lblNoRM.Text, lblNamaPasien.Text, lblJenisKelamin.Text, sKDUSER_PERAWAT, sDOKTERUTAMA)
                frmAsesmenAwalKeperawatanNeonatal.ShowDialog(Me)
                XtraTabControl1_SelectedPageChanged()
            Catch oErr As Exception
                MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            Finally
                If Not frmAsesmenAwalKeperawatanNeonatal Is Nothing Then frmAsesmenAwalKeperawatanNeonatal.Dispose()
                frmAsesmenAwalKeperawatanNeonatal = Nothing

                Dim rowHandle As Integer = grvTransferInternal.LocateByValue(rowHandle, grvAsesmenAwalPerawat.Columns("Kode"), sCode)
                If rowHandle > 0 Then grvTransferInternal.FocusedRowHandle = rowHandle
            End Try
        End If
    End Sub
    Private Sub PicEditAsesmenAwalPerawat_Click() Handles PicEditAsesmenAwalPerawat.Click
        If grvAsesmenAwalPerawat.GetFocusedRowCellValue("Kode") Is Nothing Then
            Exit Sub
        End If

        If sISDOKTER = True Then
            MsgBox("Silahkan Pilih Modul Perawat", MsgBoxStyle.Exclamation, Me.Text)
            Exit Sub
        End If

        If cboAsesmenAwalPerawat.SelectedIndex = 1 Then
            Dim Pesan As String = fn_Cek(2, "Asesmen Awal Perawat", deTANGGALMASUK.DateTime)

            If Pesan <> "" Then
                MsgBox(Pesan, MsgBoxStyle.Information, Me.Text)
                Exit Sub
            End If
        End If

        If grvAsesmenAwalPerawat.GetFocusedRowCellValue("FORMULIR") = "ASESMEN AWAL KEPERAWATAN BEDAH, PENYAKIT DALAM DAN JANTUNG" Then
            Dim frmEMedrekRI_17 As New frmEMedrekRI_17
            Try
                frmEMedrekRI_17.LoadMe(FORM_MODE.FORM_MODE_EDIT, grvAsesmenAwalPerawat.GetFocusedRowCellValue("Kode"), lblNoRM.Text, lblNamaPasien.Text, lblJenisKelamin.Text, sKDUSER_PERAWAT, grvAsesmenAwalPerawat.GetFocusedRowCellValue("DPJP"))
                frmEMedrekRI_17.ShowDialog(Me)
                XtraTabControl1_SelectedPageChanged()
            Catch oErr As Exception
                MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            Finally
                If Not frmEMedrekRI_17 Is Nothing Then frmEMedrekRI_17.Dispose()
                frmEMedrekRI_17 = Nothing

                Dim rowHandle As Integer = grvTransferInternal.LocateByValue(rowHandle, grvAsesmenAwalPerawat.Columns("Kode"), sCode)
                If rowHandle > 0 Then grvTransferInternal.FocusedRowHandle = rowHandle
            End Try
        ElseIf grvAsesmenAwalPerawat.GetFocusedRowCellValue("FORMULIR") = "ASESMEN AWAL KEPERAWATAN ICU" Then

        ElseIf grvAsesmenAwalPerawat.GetFocusedRowCellValue("FORMULIR") = "ASESMEN KEPERAWATAN REHABMEDIK" Then

        ElseIf grvAsesmenAwalPerawat.GetFocusedRowCellValue("FORMULIR") = "ASESMEN AWAL KEPERAWATAN ANAK" Then
            Dim frmEMedrekRI_40 As New frmEMedrekRI_40
            Try
                frmEMedrekRI_40.LoadMe(FORM_MODE.FORM_MODE_EDIT, grvAsesmenAwalPerawat.GetFocusedRowCellValue("Kode"), lblNoRM.Text, lblNamaPasien.Text, lblJenisKelamin.Text, sKDUSER_PERAWAT, grvAsesmenAwalPerawat.GetFocusedRowCellValue("DPJP"))
                frmEMedrekRI_40.ShowDialog(Me)
                XtraTabControl1_SelectedPageChanged()
            Catch oErr As Exception
                MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            Finally
                If Not frmEMedrekRI_40 Is Nothing Then frmEMedrekRI_40.Dispose()
                frmEMedrekRI_40 = Nothing

                Dim rowHandle As Integer = grvTransferInternal.LocateByValue(rowHandle, grvAsesmenAwalPerawat.Columns("Kode"), sCode)
                If rowHandle > 0 Then grvTransferInternal.FocusedRowHandle = rowHandle
            End Try
        ElseIf grvAsesmenAwalPerawat.GetFocusedRowCellValue("FORMULIR") = "ASESMEN AWAL KEPERAWATAN RAWAT INAP" Then
            Dim frmEMedrekRI_38 As New frmEMedrekRI_38
            Try
                frmEMedrekRI_38.LoadMe(FORM_MODE.FORM_MODE_EDIT, grvAsesmenAwalPerawat.GetFocusedRowCellValue("Kode"), lblNoRM.Text, lblNamaPasien.Text, lblJenisKelamin.Text, sKDUSER_PERAWAT, grvAsesmenAwalPerawat.GetFocusedRowCellValue("DPJP"))
                frmEMedrekRI_38.ShowDialog(Me)
                XtraTabControl1_SelectedPageChanged()
            Catch oErr As Exception
                MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            Finally
                If Not frmEMedrekRI_38 Is Nothing Then frmEMedrekRI_38.Dispose()
                frmEMedrekRI_38 = Nothing

                Dim rowHandle As Integer = grvTransferInternal.LocateByValue(rowHandle, grvAsesmenAwalPerawat.Columns("Kode"), sCode)
                If rowHandle > 0 Then grvTransferInternal.FocusedRowHandle = rowHandle
            End Try
        ElseIf grvAsesmenAwalPerawat.GetFocusedRowCellValue("FORMULIR") = "ASESMEN AWAL KEPERAWATAN PASIEN HEMODIALISA" Then

        ElseIf grvAsesmenAwalPerawat.GetFocusedRowCellValue("FORMULIR") = "ASESMEN AWAL KEPERAWATAN KEBIDANAN" Then
            Dim frmEMedrekRI_29 As New frmEMedrekRI_29
            Try
                frmEMedrekRI_29.LoadMe(FORM_MODE.FORM_MODE_EDIT, grvAsesmenAwalPerawat.GetFocusedRowCellValue("Kode"), lblNoRM.Text, lblNamaPasien.Text, lblJenisKelamin.Text, sKDUSER_PERAWAT, grvAsesmenAwalPerawat.GetFocusedRowCellValue("DPJP"))
                frmEMedrekRI_29.ShowDialog(Me)
                XtraTabControl1_SelectedPageChanged()
            Catch oErr As Exception
                MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            Finally
                If Not frmEMedrekRI_29 Is Nothing Then frmEMedrekRI_29.Dispose()
                frmEMedrekRI_29 = Nothing

                Dim rowHandle As Integer = grvTransferInternal.LocateByValue(rowHandle, grvAsesmenAwalPerawat.Columns("Kode"), sCode)
                If rowHandle > 0 Then grvTransferInternal.FocusedRowHandle = rowHandle
            End Try
        ElseIf grvAsesmenAwalPerawat.GetFocusedRowCellValue("FORMULIR") = "ASESMEN AWAL KEPERAWATAN ENDOSKOPI" Then

        ElseIf grvAsesmenAwalPerawat.GetFocusedRowCellValue("FORMULIR") = "PENGKAJIAN AWAL KEPERAWATAN RAWAT INAP JIWA" Then

        ElseIf grvAsesmenAwalPerawat.GetFocusedRowCellValue("FORMULIR") = "ASESMEN AWAL KEPERAWATAN MATERNITAS" Then

        ElseIf grvAsesmenAwalPerawat.GetFocusedRowCellValue("FORMULIR") = "ASESMEN AWAL KEPERAWATAN PASIEN NEONATAL" Then
            Dim frmAsesmenAwalKeperawatanNeonatal As New frmAsesmenAwalKeperawatanNeonatal
            Try
                frmAsesmenAwalKeperawatanNeonatal.LoadMe(FORM_MODE.FORM_MODE_EDIT, grvAsesmenAwalPerawat.GetFocusedRowCellValue("NoRegister"), lblNoRM.Text, lblNamaPasien.Text, lblJenisKelamin.Text, sKDUSER_PERAWAT, grvAsesmenAwalPerawat.GetFocusedRowCellValue("DPJP"), grvAsesmenAwalPerawat.GetFocusedRowCellValue("Kode"))
                frmAsesmenAwalKeperawatanNeonatal.ShowDialog(Me)
                XtraTabControl1_SelectedPageChanged()
            Catch oErr As Exception
                MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            Finally
                If Not frmAsesmenAwalKeperawatanNeonatal Is Nothing Then frmAsesmenAwalKeperawatanNeonatal.Dispose()
                frmAsesmenAwalKeperawatanNeonatal = Nothing

                Dim rowHandle As Integer = grvTransferInternal.LocateByValue(rowHandle, grvAsesmenAwalPerawat.Columns("Kode"), sCode)
                If rowHandle > 0 Then grvTransferInternal.FocusedRowHandle = rowHandle
            End Try
        ElseIf grvAsesmenAwalPerawat.GetFocusedRowCellValue("FORMULIR") = "LEMBAR DISCHARGE PLANNING" Then
            Dim frmLembarDischargePlanning As New frmLembarDischargePlanning
            Try
                frmLembarDischargePlanning.LoadMe(FORM_MODE.FORM_MODE_EDIT, grvAsesmenAwalPerawat.GetFocusedRowCellValue("Kode"), sDOKTERUTAMA, lblNoRM.Text, lblNamaPasien.Text, lblJenisKelamin.Text, lblRuangan.Text, sKDUSER_PERAWAT)
                frmLembarDischargePlanning.ShowDialog(Me)
                XtraTabControl1_SelectedPageChanged()
            Catch oErr As Exception
                MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            Finally
                If Not frmLembarDischargePlanning Is Nothing Then frmLembarDischargePlanning.Dispose()
                frmLembarDischargePlanning = Nothing

                Dim rowHandle As Integer = grvTransferInternal.LocateByValue(rowHandle, grvAsesmenAwalPerawat.Columns("Kode"), sCode)
                If rowHandle > 0 Then grvTransferInternal.FocusedRowHandle = rowHandle
            End Try
        End If
    End Sub
    Private Sub PicDeleteAsesmenAwalPerawat_Click() Handles PicDeleteAsesmenAwalPerawat.Click
        If sISDOKTER = True Then
            MsgBox("Silahkan Pilih Modul Perawat", MsgBoxStyle.Exclamation, Me.Text)
            Exit Sub
        End If

        MsgBox("Belum Tersedia", MsgBoxStyle.Exclamation, Me.Text)

        Exit Sub

        If grvAsesmenAwalPerawat.GetFocusedRowCellValue("Kode") Is Nothing Then
            Exit Sub
        End If

        If grvAsesmenAwalPerawat.GetFocusedRowCellValue("FORMULIR") = "ASESMEN AWAL KEPERAWATAN BEDAH, PENYAKIT DALAM DAN JANTUNG" Then

        ElseIf grvAsesmenAwalPerawat.GetFocusedRowCellValue("FORMULIR") = "ASESMEN AWAL KEPERAWATAN ICU" Then

        ElseIf grvAsesmenAwalPerawat.GetFocusedRowCellValue("FORMULIR") = "ASESMEN KEPERAWATAN REHABMEDIK" Then

        ElseIf grvAsesmenAwalPerawat.GetFocusedRowCellValue("FORMULIR") = "ASESMEN AWAL KEPERAWATAN ANAK" Then

        ElseIf grvAsesmenAwalPerawat.GetFocusedRowCellValue("FORMULIR") = "ASESMEN AWAL KEPERAWATAN RAWAT INAP" Then

        ElseIf grvAsesmenAwalPerawat.GetFocusedRowCellValue("FORMULIR") = "ASESMEN AWAL KEPERAWATAN PASIEN HEMODIALISA" Then

        ElseIf grvAsesmenAwalPerawat.GetFocusedRowCellValue("FORMULIR") = "ASESMEN AWAL KEPERAWATAN KEBIDANAN" Then

        ElseIf grvAsesmenAwalPerawat.GetFocusedRowCellValue("FORMULIR") = "ASESMEN AWAL KEPERAWATAN ENDOSKOPI" Then

        ElseIf grvAsesmenAwalPerawat.GetFocusedRowCellValue("FORMULIR") = "PENGKAJIAN AWAL KEPERAWATAN RAWAT INAP JIWA" Then

        ElseIf grvAsesmenAwalPerawat.GetFocusedRowCellValue("FORMULIR") = "ASESMEN AWAL KEPERAWATAN MATERNITAS" Then

        ElseIf grvAsesmenAwalPerawat.GetFocusedRowCellValue("FORMULIR") = "ASESMEN AWAL KEPERAWATAN PASIEN NEONATAL" Then

        ElseIf grvAsesmenAwalPerawat.GetFocusedRowCellValue("FORMULIR") = "LEMBAR DISCHARGE PLANNING" Then

        End If

        MsgBox(Statement.DeleteSuccess, MsgBoxStyle.Information, Me.Text)
        XtraTabControl1_SelectedPageChanged()
    End Sub
    Private Sub PicRefreshAsesmenAwalPerawat_Click() Handles PicRefreshAsesmenAwalPerawat.Click
        XtraTabControl1_SelectedPageChanged()
    End Sub
    Private Sub cboAsesmenAwalPerawat_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboAsesmenAwalPerawat.SelectedIndexChanged

    End Sub
    Private Sub grvListAsesmenAwalPerawat_FocusedRowChanged(ByVal sender As System.Object, ByVal e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs) Handles grvListAsesmenAwalPerawat.FocusedRowChanged
        PdfViewerAsesmenAwalPerawatIGD.CloseDocument()

        If grvListAsesmenAwalPerawat.GetFocusedRowCellValue("NoRegister") Is Nothing Then
            Exit Sub
        End If

        If grvListAsesmenAwalPerawat.GetFocusedRowCellValue("FORMULIR") = "ASESMEN AWAL KEPERAWATAN GAWAT DARURAT" Then
            Try
                SplashScreenManager.ShowForm(Me, GetType(frmLoadingPanel), True, True, False)

                SplashScreenManager.Default.SetWaitFormCaption("Processing data Asemen Awal Perawat.....")


                Dim FolderSimpan = "C:/ASESMENPERAWATAWALIGD/"

                If Not Directory.Exists(FolderSimpan) Then
                    Directory.CreateDirectory(FolderSimpan)
                Else
                    DeleteDirectory(FolderSimpan)
                    Directory.CreateDirectory(FolderSimpan)
                End If
                Dim oIGD As New Transaksi.clsDigital_IGD_02

                Dim ds = oIGD.GetData(grvListAsesmenAwalPerawat.GetFocusedRowCellValue("NoRegister"))
                If ds IsNot Nothing Then
                    sASESEMEN_IGD = ds.SIMPANGAMBAR_1

                    Dim rpt As New xtraReportAsesmenKeperawatanGD_New

                    rpt.ShowPrintMarginsWarning = False
                    rpt.Watermark.Text = sWATERMARK

                    rpt.BindingSource.DataSource = ds

                    Dim waktu As String = Now.ToString("ddMMyyyyHHmm", CultureInfo.InvariantCulture)
                    rpt.ExportToPdf(FolderSimpan & waktu & ds.KDPENDAFTARAN & ".pdf")
                    PdfViewerAsesmenAwalPerawatIGD.LoadDocument(FolderSimpan & waktu & ds.KDPENDAFTARAN & ".pdf")
                End If

                SplashScreenManager.CloseForm(False)
            Catch oErr As Exception
                SplashScreenManager.CloseForm(False)
                MsgBox("Load data Asemen Awal Perawat: " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            End Try
        ElseIf grvListAsesmenAwalPerawat.GetFocusedRowCellValue("FORMULIR") = "CATATAN SKRINING DAN EDUKASI" Then
            Try
                SplashScreenManager.ShowForm(Me, GetType(frmLoadingPanel), True, True, False)

                SplashScreenManager.Default.SetWaitFormCaption("Processing data Catatan Skring dan Edukasi.....")

                Dim FolderSimpan = "C:/ASESMENPERAWATAWALIGD/"

                If Not Directory.Exists(FolderSimpan) Then
                    Directory.CreateDirectory(FolderSimpan)
                Else
                    DeleteDirectory(FolderSimpan)
                    Directory.CreateDirectory(FolderSimpan)
                End If
                Dim oIGD As New Digital.clsDigital_RI_20

                Dim ds = oIGD.GetData(grvListAsesmenAwalPerawat.GetFocusedRowCellValue("NoRegister"))
                If ds IsNot Nothing Then
                    NAMA = lblNamaPasien.Text
                    TANGGALLAHIR = lblTanggalLahir.Text

                    Dim rpt As New xtraReportEMedrekRI_20

                    rpt.ShowPrintMarginsWarning = False
                    rpt.Watermark.Text = sWATERMARK

                    rpt.BindingSource.DataSource = ds

                    Dim waktu As String = Now.ToString("ddMMyyyyHHmm", CultureInfo.InvariantCulture) & "GIZI"
                    rpt.ExportToPdf(FolderSimpan & waktu & ds.KDREG & ".pdf")
                    PdfViewerAsesmenAwalPerawatIGD.LoadDocument(FolderSimpan & waktu & ds.KDREG & ".pdf")
                End If

                SplashScreenManager.CloseForm(False)
            Catch oErr As Exception
                SplashScreenManager.CloseForm(False)
                MsgBox("Load data Catatan Skring dan Edukasi: " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            End Try
        ElseIf grvListAsesmenAwalPerawat.GetFocusedRowCellValue("FORMULIR") = "ASESMEN LANJUT" Then
            Try
                SplashScreenManager.ShowForm(Me, GetType(frmLoadingPanel), True, True, False)

                SplashScreenManager.Default.SetWaitFormCaption("Processing data Asesmen Lanjut.....")

                Dim FolderSimpan = "C:/ASESMENPERAWATAWALIGD/"

                If Not Directory.Exists(FolderSimpan) Then
                    Directory.CreateDirectory(FolderSimpan)
                Else
                    DeleteDirectory(FolderSimpan)
                    Directory.CreateDirectory(FolderSimpan)
                End If
                Dim oIGD As New Digital.clsS_DIGITAL_ASESMENGIZILANJUT

                Dim ds = oIGD.GetData(grvListAsesmenAwalPerawat.GetFocusedRowCellValue("NoRegister"))
                If ds IsNot Nothing Then
                    NAMA = lblNamaPasien.Text
                    TANGGALLAHIR = lblTanggalLahir.Text

                    Dim rpt As New xtraReportAsesmenGiziLanjut

                    rpt.ShowPrintMarginsWarning = False
                    rpt.Watermark.Text = sWATERMARK

                    rpt.BindingSource1.DataSource = ds

                    Dim waktu As String = Now.ToString("ddMMyyyyHHmm", CultureInfo.InvariantCulture) & "ASESMENLANJUT"
                    rpt.ExportToPdf(FolderSimpan & waktu & ds.KDREG & ".pdf")
                    PdfViewerAsesmenAwalPerawatIGD.LoadDocument(FolderSimpan & waktu & ds.KDREG & ".pdf")
                End If

                SplashScreenManager.CloseForm(False)
            Catch oErr As Exception
                SplashScreenManager.CloseForm(False)
                MsgBox("Load data Asesmen Lanjut: " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            End Try
        ElseIf grvListAsesmenAwalPerawat.GetFocusedRowCellValue("FORMULIR") = "LEMBAR DISCHARGE PLANNING" Then
            Try
                SplashScreenManager.ShowForm(Me, GetType(frmLoadingPanel), True, True, False)

                SplashScreenManager.Default.SetWaitFormCaption("Processing data Asesmen Lanjut.....")

                Dim FolderSimpan = "C:/ASESMENPERAWATAWALIGD/"

                If Not Directory.Exists(FolderSimpan) Then
                    Directory.CreateDirectory(FolderSimpan)
                Else
                    DeleteDirectory(FolderSimpan)
                    Directory.CreateDirectory(FolderSimpan)
                End If
                Dim oIGD As New Digital.clsS_DIGITAL_LEMBARDISCHARGEPLANNING

                Dim ds = oIGD.GetData(grvListAsesmenAwalPerawat.GetFocusedRowCellValue("NoRegister"))
                If ds IsNot Nothing Then
                    NAMA = lblNamaPasien.Text
                    TANGGALLAHIR = lblTanggalLahir.Text

                    Dim rpt As New xtraReportLembarDischargePlanning

                    rpt.ShowPrintMarginsWarning = False
                    rpt.Watermark.Text = sWATERMARK

                    rpt.BindingSource1.DataSource = ds

                    Dim waktu As String = Now.ToString("ddMMyyyyHHmm", CultureInfo.InvariantCulture) & "ASESMENLANJUT"
                    rpt.ExportToPdf(FolderSimpan & waktu & ds.KDPENDAFTARAN & ".pdf")
                    PdfViewerAsesmenAwalPerawatIGD.LoadDocument(FolderSimpan & waktu & ds.KDPENDAFTARAN & ".pdf")
                End If

                SplashScreenManager.CloseForm(False)
            Catch oErr As Exception
                SplashScreenManager.CloseForm(False)
                MsgBox("Load data Asesmen Lanjut: " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            End Try
        ElseIf grvListAsesmenAwalPerawat.GetFocusedRowCellValue("FORMULIR") = "ASESMEN AWAL KEPERAWATAN RAWAT INAP" Then
            Try
                SplashScreenManager.ShowForm(Me, GetType(frmLoadingPanel), True, True, False)

                SplashScreenManager.Default.SetWaitFormCaption("Processing data Asesmen Lanjut.....")

                Dim FolderSimpan = "C:/ASESMENPERAWATAWALIGD/"

                If Not Directory.Exists(FolderSimpan) Then
                    Directory.CreateDirectory(FolderSimpan)
                Else
                    DeleteDirectory(FolderSimpan)
                    Directory.CreateDirectory(FolderSimpan)
                End If
                Dim oIGD As New Inventory.clsDigital_RI_38

                Dim ds = oIGD.GetData(grvListAsesmenAwalPerawat.GetFocusedRowCellValue("NoRegister"))
                If ds IsNot Nothing Then
                    NAMA = lblNamaPasien.Text
                    TANGGALLAHIR = lblTanggalLahir.Text

                    Dim rpt As New xtraReportEMedrekRI_38

                    rpt.ShowPrintMarginsWarning = False
                    rpt.Watermark.Text = sWATERMARK

                    rpt.BindingSource1.DataSource = ds

                    Dim waktu As String = Now.ToString("ddMMyyyyHHmm", CultureInfo.InvariantCulture) & "ASESMENLANJUT"
                    rpt.ExportToPdf(FolderSimpan & waktu & ds.KDPENDAFTARAN & ".pdf")
                    PdfViewerAsesmenAwalPerawatIGD.LoadDocument(FolderSimpan & waktu & ds.KDPENDAFTARAN & ".pdf")
                End If

                SplashScreenManager.CloseForm(False)
            Catch oErr As Exception
                SplashScreenManager.CloseForm(False)
                MsgBox("Load data Asesmen Lanjut: " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            End Try
        ElseIf grvListAsesmenAwalPerawat.GetFocusedRowCellValue("FORMULIR") = "ASESMEN AWAL KEPERAWATAN BEDAH, PENYAKIT DALAM DAN JANTUNG" Then
            Try
                SplashScreenManager.ShowForm(Me, GetType(frmLoadingPanel), True, True, False)

                SplashScreenManager.Default.SetWaitFormCaption("Processing data Asesmen Lanjut.....")

                Dim FolderSimpan = "C:/ASESMENPERAWATAWALIGD/"

                If Not Directory.Exists(FolderSimpan) Then
                    Directory.CreateDirectory(FolderSimpan)
                Else
                    DeleteDirectory(FolderSimpan)
                    Directory.CreateDirectory(FolderSimpan)
                End If
                Dim oIGD As New Digital.clsDigital_RI_17

                Dim ds = oIGD.GetData(grvListAsesmenAwalPerawat.GetFocusedRowCellValue("NoRegister"))
                If ds IsNot Nothing Then
                    NAMA = lblNamaPasien.Text
                    TANGGALLAHIR = lblTanggalLahir.Text

                    Dim rpt As New xtraReportEMedrekRI_17

                    rpt.ShowPrintMarginsWarning = False
                    rpt.Watermark.Text = sWATERMARK

                    rpt.BindingSource.DataSource = ds

                    Dim waktu As String = Now.ToString("ddMMyyyyHHmm", CultureInfo.InvariantCulture) & "ASESMENLANJUT"
                    rpt.ExportToPdf(FolderSimpan & waktu & ds.KDPENDAFTARAN & ".pdf")
                    PdfViewerAsesmenAwalPerawatIGD.LoadDocument(FolderSimpan & waktu & ds.KDPENDAFTARAN & ".pdf")
                End If

                SplashScreenManager.CloseForm(False)
            Catch oErr As Exception
                SplashScreenManager.CloseForm(False)
                MsgBox("Load data Asesmen Lanjut: " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            End Try
        ElseIf grvListAsesmenAwalPerawat.GetFocusedRowCellValue("FORMULIR") = "ASESMEN AWAL KEPERAWATAN KEBIDANAN" Then
            Try
                SplashScreenManager.ShowForm(Me, GetType(frmLoadingPanel), True, True, False)

                SplashScreenManager.Default.SetWaitFormCaption("Processing data Asesmen Lanjut.....")

                Dim FolderSimpan = "C:/ASESMENPERAWATAWALIGD/"

                If Not Directory.Exists(FolderSimpan) Then
                    Directory.CreateDirectory(FolderSimpan)
                Else
                    DeleteDirectory(FolderSimpan)
                    Directory.CreateDirectory(FolderSimpan)
                End If
                Dim oIGD As New Digital.clsS_DIGITAL_RI_29

                Dim ds = oIGD.GetData(grvListAsesmenAwalPerawat.GetFocusedRowCellValue("NoRegister"))
                If ds IsNot Nothing Then
                    NAMA = lblNamaPasien.Text
                    TANGGALLAHIR = lblTanggalLahir.Text

                    Dim rpt As New xtraReportEMedrekRI_29

                    rpt.ShowPrintMarginsWarning = False
                    rpt.Watermark.Text = sWATERMARK

                    rpt.BindingSource1.DataSource = ds

                    Dim waktu As String = Now.ToString("ddMMyyyyHHmm", CultureInfo.InvariantCulture) & "ASESMENLANJUT"
                    rpt.ExportToPdf(FolderSimpan & waktu & ds.KDPENDAFTARAN & ".pdf")
                    PdfViewerAsesmenAwalPerawatIGD.LoadDocument(FolderSimpan & waktu & ds.KDPENDAFTARAN & ".pdf")
                End If

                SplashScreenManager.CloseForm(False)
            Catch oErr As Exception
                SplashScreenManager.CloseForm(False)
                MsgBox("Load data Asesmen Lanjut: " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            End Try
        ElseIf grvListAsesmenAwalPerawat.GetFocusedRowCellValue("FORMULIR") = "ASESMEN AWAL KEPERAWATAN ANAK" Then
            Try
                SplashScreenManager.ShowForm(Me, GetType(frmLoadingPanel), True, True, False)

                SplashScreenManager.Default.SetWaitFormCaption("Processing data Asesmen Lanjut.....")

                Dim FolderSimpan = "C:/ASESMENPERAWATAWALIGD/"

                If Not Directory.Exists(FolderSimpan) Then
                    Directory.CreateDirectory(FolderSimpan)
                Else
                    DeleteDirectory(FolderSimpan)
                    Directory.CreateDirectory(FolderSimpan)
                End If
                Dim oIGD As New Digital.clsDigital_RI_40

                Dim ds = oIGD.GetData(grvListAsesmenAwalPerawat.GetFocusedRowCellValue("NoRegister"))
                If ds IsNot Nothing Then
                    NAMA = lblNamaPasien.Text
                    TANGGALLAHIR = lblTanggalLahir.Text

                    Dim rpt As New xtraReportEMedrekRI_40

                    rpt.ShowPrintMarginsWarning = False
                    rpt.Watermark.Text = sWATERMARK

                    rpt.BindingSource1.DataSource = ds

                    Dim waktu As String = Now.ToString("ddMMyyyyHHmm", CultureInfo.InvariantCulture) & "ASESMENLANJUT"
                    rpt.ExportToPdf(FolderSimpan & waktu & ds.KDPENDAFTARAN & ".pdf")
                    PdfViewerAsesmenAwalPerawatIGD.LoadDocument(FolderSimpan & waktu & ds.KDPENDAFTARAN & ".pdf")
                End If

                SplashScreenManager.CloseForm(False)
            Catch oErr As Exception
                SplashScreenManager.CloseForm(False)
                MsgBox("Load data Asesmen Lanjut: " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            End Try
        End If
    End Sub
#End Region
#Region "Asesmen Awal Medis"
#Region "List"
    Private Sub fn_LoadDataAsesmenAwalMedisCek(ByVal Parameter As String)
        Try
            If Parameter = String.Empty Then Exit Sub

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
            SQL &= "S_DIGITAL_RI_13 A "
            SQL &= "WHERE A.KDREG = '" & Parameter & "' "

            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "ASESMENAWALMEDISCEK")

            If oConn.State = ConnectionState.Open Then
                oConn.Close()
            End If

            For xLoop As Integer = 0 To ds.Tables("ASESMENAWALMEDISCEK").Rows.Count - 1
                With ds.Tables("ASESMENAWALMEDISCEK")
                    lblTandaAsesmen.Text = "Sudah Ada Catatan Medis"
                End With
            Next
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadDataAsesmenAwalMedisList(ByVal Parameter As String)
        Try
            grvAsesmenAwalMedisList.Columns.Clear()
            grdAsesmenAwalMedisList.DataSource = Nothing

            If Parameter = String.Empty Then Exit Sub

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
            SQL &= "(SELECT "
            SQL &= "Tanggal = A.DATE "
            SQL &= ",FORMULIR = 'ASESMEN AWAL MEDIS PASIEN PARU' "
            SQL &= ",Kode = A.KDPENDAFTARAN "
            SQL &= ",DPJP = A.DOKTER_NAMEDISPLAY "
            SQL &= ",KDDPJP = '' "
            SQL &= ",A.KDUSER "
            SQL &= "FROM "
            SQL &= "S_DIGITAL_RJ_36 A "
            SQL &= "WHERE A.KDCUSTOMER = '" & Parameter & "' "

            SQL &= ")UNION ALL( "

            SQL &= "SELECT "
            SQL &= "Tanggal = A.DATE "
            SQL &= ",FORMULIR = 'ASESMEN AWAL MEDIS PASIEN ICU' "
            SQL &= ",Kode = A.KDPENDAFTARAN "
            SQL &= ",DPJP = A.DOKTER_NAMEDISPLAY "
            SQL &= ",KDDPJP = '' "
            SQL &= ",A.KDUSER "
            SQL &= "FROM "
            SQL &= "S_DIGITAL_RI_MEDIS_ICU A "
            SQL &= "WHERE A.KDCUSTOMER = '" & Parameter & "' "

            SQL &= ")UNION ALL( "

            SQL &= "SELECT "
            SQL &= "Tanggal = A.DATEUPDATED "
            SQL &= ",FORMULIR = 'ASESMEN AWAL MEDIS IGD' "
            SQL &= ",Kode = A.KDPENDAFTARAN "
            SQL &= ",DPJP = B.NAME_DISPLAY "
            SQL &= ",KDDPJP = '' "
            SQL &= ",A.KDUSER "
            SQL &= "FROM "
            SQL &= "S_DIGITAL_IGD_01 A "
            SQL &= "INNER JOIN M_DOCTOR B "
            SQL &= "ON A.DOCTOR_KODE = B.KDDOCTOR "
            SQL &= "WHERE A.KDCUSTOMER = '" & Parameter & "' "

            SQL &= ")UNION ALL( "

            SQL &= "SELECT "
            SQL &= "Tanggal = A.DATE "
            SQL &= ",FORMULIR = 'ASESMEN AWAL MEDIS PASIEN BEDAH' "
            SQL &= ",Kode = A.KDPENDAFTARAN "
            SQL &= ",DPJP = A.KDDOCTOR_NAME_DISPLAY "
            SQL &= ",KDDPJP = A.KDDOCTOR_KODE "
            SQL &= ",A.KDUSER "
            SQL &= "FROM "
            SQL &= "S_DIGITAL_RJ_34 A "
            SQL &= "WHERE A.KDCUSTOMER = '" & Parameter & "' "

            SQL &= ")UNION ALL( "

            SQL &= "SELECT "
            SQL &= "Tanggal = A.DATE "
            SQL &= ",FORMULIR = 'ASESMEN AWAL MEDIS PASIEN PENYAKIT DALAM' "
            SQL &= ",Kode = A.KDPENDAFTARAN "
            SQL &= ",DPJP = A.DOCTOR_NAME_DISPLAY "
            SQL &= ",KDDPJP = A.DOCTOR_KODE "
            SQL &= ",A.KDUSER "
            SQL &= "FROM "
            SQL &= "S_DIGITAL_RJ_31 A "
            SQL &= "WHERE A.KDCUSTOMER = '" & Parameter & "' "

            SQL &= ")UNION ALL( "

            SQL &= "SELECT "
            SQL &= "Tanggal = A.DATE "
            SQL &= ",FORMULIR = 'ASESMEN AWAL MEDIS PASIEN JANTUNG' "
            SQL &= ",Kode = A.KDPENDAFTARAN "
            SQL &= ",DPJP = A.DOKTER_NAMEDISPLAY "
            SQL &= ",KDDPJP = A.DOKTER_KODE "
            SQL &= ",A.KDUSER "
            SQL &= "FROM "
            SQL &= "S_DIGITAL_RJ_29 A "
            SQL &= "WHERE A.KDCUSTOMER = '" & Parameter & "' "

            SQL &= ")UNION ALL( "

            SQL &= "SELECT "
            SQL &= "Tanggal = A.DATE "
            SQL &= ",FORMULIR = 'ASESMEN AWAL MEDIS RAWAT INAP' "
            SQL &= ",Kode = A.KDREG "
            SQL &= ",DPJP = A.DOCTOR_NAME_DISPLAY "
            SQL &= ",KDDPJP = A.DOCTOR_KODE "
            SQL &= ",A.KDUSER "
            SQL &= "FROM "
            SQL &= "S_DIGITAL_RI_13 A "
            SQL &= "WHERE A.KDCUSTOMER = '" & Parameter & "' "


            SQL &= ")UNION ALL( "

            SQL &= "SELECT "
            SQL &= "Tanggal = A.DATECREATED "
            SQL &= ",FORMULIR = 'DPJP DAN PPJP' "
            SQL &= ",Kode = A.KDPENDAFTARAN "
            SQL &= ",DPJP = A.DOKTER_NAMEDISPLAY "
            SQL &= ",KDDPJP = A.DOKTER_KODE "
            SQL &= ",A.KDUSER "
            SQL &= "FROM "
            SQL &= "S_DIGITAL_RI_16 A "
            SQL &= "WHERE A.KDCUSTOMER = '" & Parameter & "' "

            SQL &= ") "
            SQL &= ") AS X "
            SQL &= "ORDER BY X.Tanggal DESC "

            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "ASESMENAWALMEDIS")

            grdAsesmenAwalMedisList.MainView = grvAsesmenAwalMedisList
            grdAsesmenAwalMedisList.DataSource = ds.Tables("ASESMENAWALMEDIS")
            grdAsesmenAwalMedisList.ForceInitialize()

            fn_LoadFormatDataAsesmenAwalMedis()

            If oConn.State = ConnectionState.Open Then
                oConn.Close()
            End If

        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadFormatDataAsesmenAwalMedis()
        Try
            For iLoop As Integer = 0 To grvAsesmenAwalMedisList.Columns.Count - 1
                If grvAsesmenAwalMedisList.Columns(iLoop).ColumnType.Name = "Decimal" Then
                    grvAsesmenAwalMedisList.Columns(iLoop).DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                    grvAsesmenAwalMedisList.Columns(iLoop).DisplayFormat.FormatString = "{0:n2}"
                    grvAsesmenAwalMedisList.Columns(iLoop).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
                ElseIf grvAsesmenAwalMedisList.Columns(iLoop).ColumnType.Name = "DateTime" Then
                    grvAsesmenAwalMedisList.Columns(iLoop).DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
                    grvAsesmenAwalMedisList.Columns(iLoop).DisplayFormat.FormatString = "{0:dd/MM/yyyy}"
                End If
            Next

            grvAsesmenAwalMedisList.Columns("FORMULIR").Caption = "Formulir"
            grvAsesmenAwalMedisList.Columns("KDUSER").Caption = "User"

            grvAsesmenAwalMedisList.Columns("Kode").VisibleIndex = -1
            grvAsesmenAwalMedisList.Columns("KDDPJP").VisibleIndex = -1
            grvAsesmenAwalMedisList.BestFitColumns()
        Catch ex As Exception

        End Try
    End Sub
#End Region
#Region "Function"
    Private Sub fn_ChangeFormStateDischargePlanning()
        If isLoadAsesmen = True Then Exit Sub

        fn_Doctor()

        Dim ds = oDigital_DischargePlanning.GetData(lblRegister.Text)

        If ds Is Nothing Then
            fn_EmptyMeDischargePlanning()
        Else
            fn_LoadDataDischargePlanning()
        End If

        isLoadAsesmen = True
    End Sub
    Private Sub fn_EmptyMeDischargePlanning()
        deDATEAsesmen.DateTime = Now
        txtANAMNESIS_01.ResetText()
        txtANAMNESIS_02.ResetText()
        txtANAMNESIS_03.ResetText()
        'txtANAMNESIS_04.ResetText()
        cboKeadaanUmumAsesmen.ResetText()
        cboKesadaranAsesmen.ResetText()
        'txtANAMNESIS_07.ResetText()
        txtANAMNESIS_08.ResetText()
        txtANAMNESIS_09.ResetText()
        txtANAMNESIS_10.ResetText()
        txtANAMNESIS_11.ResetText()
        txtANAMNESIS_12.ResetText()
        txtANAMNESIS_13.ResetText()
        txtANAMNESIS_14.ResetText()
        txtSPO2_ASESMENMEDIS.ResetText()
        txtANAMNESIS_15.ResetText()
        txtANAMNESIS_16.ResetText()
        txtANAMNESIS_17.ResetText()
        txtANAMNESIS_18.ResetText()
        txtANAMNESIS_19.ResetText()
        txtANAMNESIS_20.ResetText()
        txtANAMNESIS_29.ResetText()
        'txtANAMNESIS_30.ResetText()
        'txtANAMNESIS_31.ResetText()
        txtANAMNESIS_32.ResetText()
        txtANAMNESIS_33.ResetText()
        txtINDIKASIRAWATDIASESMEN.ResetText()
        txtANAMNESIS_34.ResetText()
        'txtANAMNESIS_35.ResetText()
        txtANAMNESIS_36.ResetText()
        txtANAMNESIS_37.ResetText()
        txtANAMNESIS_38.ResetText()
        cboRencanaPerawatan.ResetText()
        grdDPJP.Text = sKDDOCTOR_DPJPUTAMA

        chkANAMNESIS_15_1_CHEK.Checked = False
        chkANAMNESIS_16_1_CHEK.Checked = False
        chkANAMNESIS_17_1_CHEK.Checked = False
        chkANAMNESIS_18_1_CHEK.Checked = False
        chkANAMNESIS_19_1_CHEK.Checked = False
        chkANAMNESIS_20_1_CHEK.Checked = False
        chkANAMNESIS_15_2_CHEK.Checked = False
        chkANAMNESIS_16_2_CHEK.Checked = False
        chkANAMNESIS_17_2_CHEK.Checked = False
        chkANAMNESIS_18_2_CHEK.Checked = False
        chkANAMNESIS_19_2_CHEK.Checked = False
        chkANAMNESIS_20_2_CHEK.Checked = False

        LoadAsesmenIGDDischargePlanning(sKDREGRAWATJALAN)

    End Sub
    Private Sub fn_LoadDataDischargePlanning()
        Try
            ' ***** HEADER *****
            Dim ds = oDigital_DischargePlanning.GetData(lblRegister.Text)

            With ds
                deDATEAsesmen.DateTime = .DATE
                grdDPJP.Text = .DOCTOR_KODE
                txtANAMNESIS_01.Text = .ANAMNESIS_01
                txtANAMNESIS_02.Text = .ANAMNESIS_02
                txtANAMNESIS_03.Text = .ANAMNESIS_03
                'txtANAMNESIS_04.Text = .ANAMNESIS_04
                cboKeadaanUmumAsesmen.Text = .ANAMNESIS_05
                cboKesadaranAsesmen.Text = .ANAMNESIS_06
                'txtANAMNESIS_07.Text = .ANAMNESIS_07
                txtANAMNESIS_08.Text = .ANAMNESIS_08
                txtANAMNESIS_09.Text = .ANAMNESIS_09
                txtANAMNESIS_10.Text = .ANAMNESIS_10
                txtANAMNESIS_11.Text = .ANAMNESIS_11
                txtANAMNESIS_12.Text = .ANAMNESIS_12
                txtANAMNESIS_13.Text = .ANAMNESIS_13
                txtANAMNESIS_14.Text = .ANAMNESIS_14
                txtSPO2_ASESMENMEDIS.Text = .ANAMNESIS_22
                txtANAMNESIS_15.Text = .ANAMNESIS_15
                txtANAMNESIS_16.Text = .ANAMNESIS_16
                txtANAMNESIS_17.Text = .ANAMNESIS_17
                txtANAMNESIS_18.Text = .ANAMNESIS_18
                txtANAMNESIS_19.Text = .ANAMNESIS_19
                txtANAMNESIS_20.Text = .ANAMNESIS_20
                txtANAMNESIS_29.Text = .ANAMNESIS_29
                'txtANAMNESIS_30.Text = .ANAMNESIS_30
                'txtANAMNESIS_31.Text = .ANAMNESIS_31
                txtANAMNESIS_32.Text = .ANAMNESIS_32
                txtANAMNESIS_33.Text = .ANAMNESIS_33
                txtINDIKASIRAWATDIASESMEN.Text = .ANAMNESIS_28
                txtANAMNESIS_34.Text = .ANAMNESIS_34
                'txtANAMNESIS_35.Text = .ANAMNESIS_35
                txtANAMNESIS_36.Text = .ANAMNESIS_36
                txtANAMNESIS_37.Text = .ANAMNESIS_37
                txtANAMNESIS_38.Text = .ANAMNESIS_38
                cboRencanaPerawatan.Text = .HARI

                chkANAMNESIS_15_1_CHEK.Checked = .ANAMNESIS_15_1_CHEK
                chkANAMNESIS_16_1_CHEK.Checked = .ANAMNESIS_16_1_CHEK
                chkANAMNESIS_17_1_CHEK.Checked = .ANAMNESIS_17_1_CHEK
                chkANAMNESIS_18_1_CHEK.Checked = .ANAMNESIS_18_1_CHEK
                chkANAMNESIS_19_1_CHEK.Checked = .ANAMNESIS_19_1_CHEK
                chkANAMNESIS_20_1_CHEK.Checked = .ANAMNESIS_20_1_CHEK
                chkANAMNESIS_15_2_CHEK.Checked = .ANAMNESIS_15_2_CHEK
                chkANAMNESIS_16_2_CHEK.Checked = .ANAMNESIS_16_2_CHEK
                chkANAMNESIS_17_2_CHEK.Checked = .ANAMNESIS_17_2_CHEK
                chkANAMNESIS_18_2_CHEK.Checked = .ANAMNESIS_18_2_CHEK
                chkANAMNESIS_19_2_CHEK.Checked = .ANAMNESIS_19_2_CHEK
                chkANAMNESIS_20_2_CHEK.Checked = .ANAMNESIS_20_2_CHEK
            End With

            BindingSourceDiagnosa_Asesmen.DataSource = oDigital_DischargePlanning.GetDataDetailDiagnosa(lblRegister.Text).OrderBy(Function(x) x.SEQ).ToList()
            grdDiagnosaAsesmen.DataSource = BindingSourceDiagnosa_Asesmen

        Catch oErr As Exception
            MsgBox("Load List Data : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Function fn_ValidateDischargePlanning() As Boolean
        Try
            fn_ValidateDischargePlanning = True
            If lblRegister.Text = String.Empty Then
                MsgBox("Dibutuhkan Register", MsgBoxStyle.Exclamation, Me.Text)
                fn_ValidateDischargePlanning = False
                Exit Function
            End If
            If cboRencanaPerawatan.Text = String.Empty Then
                MsgBox("Dibutuhkan Rencana Perawatan", MsgBoxStyle.Exclamation, Me.Text)
                cboRencanaPerawatan.Focus()
                fn_ValidateDischargePlanning = False
                Exit Function
            End If
            If txtANAMNESIS_37.Text = String.Empty Then
                MsgBox("Rekonsiliasi Obat masih kosong", MsgBoxStyle.Exclamation, Me.Text)
                txtANAMNESIS_37.Focus()
                fn_ValidateDischargePlanning = False
                Exit Function
            End If

            If sISDOKTER = False Then
                MsgBox("Silahkan Pilih Modul Dokter", MsgBoxStyle.Exclamation, Me.Text)
                cboRencanaPerawatan.Focus()
                fn_ValidateDischargePlanning = False
                Exit Function
            End If


        Catch oErr As Exception
            MsgBox("Validate Data Discharge Planning: " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Function
    Private Function fn_SaveDischargePlanning() As Boolean
        Try
            ' ***** HEADER *****
            Dim ds = oDigital_DischargePlanning.GetStructureHeader
            With ds
                .KDREG = lblRegister.Text
                .KDCUSTOMER = lblNoRM.Text
                Try
                    .DATECREATED = oDigital_DischargePlanning.GetData(lblRegister.Text).DATECREATED
                Catch ex As Exception
                    .DATECREATED = Now
                End Try
                .DATEUPDATED = Now
                .DATE = deDATEAsesmen.DateTime
                .JAM = deDATEAsesmen.DateTime.ToString("HH:mm", CultureInfo.InvariantCulture)
                .DOCTOR_KODE = grdDPJP.EditValue
                .DOCTOR_NAME_DISPLAY = grdDPJP.Text
                .ANAMNESIS_01 = txtANAMNESIS_01.Text
                .ANAMNESIS_02 = txtANAMNESIS_02.Text
                .ANAMNESIS_03 = txtANAMNESIS_03.Text
                .ANAMNESIS_04 = ""
                .ANAMNESIS_05 = cboKeadaanUmumAsesmen.Text
                .ANAMNESIS_06 = cboKesadaranAsesmen.Text
                .ANAMNESIS_07 = ""
                .ANAMNESIS_08 = txtANAMNESIS_08.Text
                .ANAMNESIS_09 = txtANAMNESIS_09.Text
                .ANAMNESIS_10 = txtANAMNESIS_10.Text
                .ANAMNESIS_11 = txtANAMNESIS_11.Text
                .ANAMNESIS_12 = txtANAMNESIS_12.Text
                .ANAMNESIS_13 = txtANAMNESIS_13.Text
                .ANAMNESIS_14 = txtANAMNESIS_14.Text
                .ANAMNESIS_15 = txtANAMNESIS_15.Text
                .ANAMNESIS_16 = txtANAMNESIS_16.Text
                .ANAMNESIS_17 = txtANAMNESIS_17.Text
                .ANAMNESIS_18 = txtANAMNESIS_18.Text
                .ANAMNESIS_19 = txtANAMNESIS_19.Text
                .ANAMNESIS_20 = txtANAMNESIS_20.Text
                Try
                    Dim getdata = oDigital_DischargePlanning.GetData(lblRegister.Text)
                    .ANAMNESIS_21 = getdata.ANAMNESIS_15 & ", " & getdata.ANAMNESIS_16 & ", " & getdata.ANAMNESIS_17 & ", " & getdata.ANAMNESIS_18 & ", " & getdata.ANAMNESIS_19 & ", " & getdata.ANAMNESIS_20
                Catch ex As Exception
                    .ANAMNESIS_21 = ""
                End Try
                .ANAMNESIS_22 = txtSPO2_ASESMENMEDIS.Text
                .ANAMNESIS_23 = ""
                .ANAMNESIS_24 = ""
                .ANAMNESIS_25 = ""
                .ANAMNESIS_26 = ""
                .ANAMNESIS_27 = ""
                .ANAMNESIS_28 = txtINDIKASIRAWATDIASESMEN.Text
                .ANAMNESIS_29 = txtANAMNESIS_29.Text

                Try
                    .ANAMNESIS_30 = oDigital_DischargePlanning.GetData(lblRegister.Text).ANAMNESIS_30

                Catch ex As Exception
                    .ANAMNESIS_30 = ""

                End Try
                Try
                    .ANAMNESIS_31 = oDigital_DischargePlanning.GetData(lblRegister.Text).ANAMNESIS_31

                Catch ex As Exception
                    .ANAMNESIS_31 = ""

                End Try


                .ANAMNESIS_32 = txtANAMNESIS_32.Text
                .ANAMNESIS_33 = txtANAMNESIS_33.Text
                .ANAMNESIS_34 = txtANAMNESIS_34.Text

                Try
                    .ANAMNESIS_35 = oDigital_DischargePlanning.GetData(lblRegister.Text).ANAMNESIS_35
                Catch ex As Exception
                    .ANAMNESIS_35 = ""
                End Try

                .ANAMNESIS_36 = txtANAMNESIS_36.Text
                .ANAMNESIS_37 = txtANAMNESIS_37.Text
                .ANAMNESIS_38 = txtANAMNESIS_38.Text
                Try
                    .CETAK = oDigital_DischargePlanning.GetData(lblRegister.Text).CETAK
                Catch ex As Exception
                    .CETAK = 0
                End Try
                .KDUSER = sUserID
                .KDUSER_SIGNATURE = ""
                .HARI = cboRencanaPerawatan.Text
                .ANAMNESIS_15_1_CHEK = chkANAMNESIS_15_1_CHEK.Checked
                .ANAMNESIS_16_1_CHEK = chkANAMNESIS_16_1_CHEK.Checked
                .ANAMNESIS_17_1_CHEK = chkANAMNESIS_17_1_CHEK.Checked
                .ANAMNESIS_18_1_CHEK = chkANAMNESIS_18_1_CHEK.Checked
                .ANAMNESIS_19_1_CHEK = chkANAMNESIS_19_1_CHEK.Checked
                .ANAMNESIS_20_1_CHEK = chkANAMNESIS_20_1_CHEK.Checked
                .ANAMNESIS_15_2_CHEK = chkANAMNESIS_15_2_CHEK.Checked
                .ANAMNESIS_16_2_CHEK = chkANAMNESIS_16_2_CHEK.Checked
                .ANAMNESIS_17_2_CHEK = chkANAMNESIS_17_2_CHEK.Checked
                .ANAMNESIS_18_2_CHEK = chkANAMNESIS_18_2_CHEK.Checked
                .ANAMNESIS_19_2_CHEK = chkANAMNESIS_19_2_CHEK.Checked
                .ANAMNESIS_20_2_CHEK = chkANAMNESIS_20_2_CHEK.Checked
            End With

            'DIAGNOSA
            Dim listdiagnosa As New List(Of String)

            Dim arrDetailDiagnosa = oDigital_DischargePlanning.GetStructureDetaiDiagnosalList
            For i As Integer = 0 To grvDiagnosaAsesmen.RowCount - 2
                Dim dsDetail = oDigital_DischargePlanning.GetStructureDetailDiagnosa
                With dsDetail
                    .SEQ = i
                    .KDREG = ds.KDREG
                    .KATEGORI = IIf(String.IsNullOrEmpty(grvDiagnosaAsesmen.GetRowCellValue(i, colKATEGORIASESMEN)), "", grvDiagnosaAsesmen.GetRowCellValue(i, colKATEGORIASESMEN))
                    .NAMADIAGNOSA = IIf(String.IsNullOrEmpty(grvDiagnosaAsesmen.GetRowCellValue(i, colNAMADIAGNOSAASESMEN)), "", grvDiagnosaAsesmen.GetRowCellValue(i, colNAMADIAGNOSAASESMEN))
                    .KDDIAGNOSA = IIf(String.IsNullOrEmpty(grvDiagnosaAsesmen.GetRowCellValue(i, colKDDIAGNOSAASESMEN)), "", grvDiagnosaAsesmen.GetRowCellValue(i, colKDDIAGNOSAASESMEN))
                End With
                arrDetailDiagnosa.Add(dsDetail)
            Next

            For Each xloop In arrDetailDiagnosa
                listdiagnosa.Add(xloop.KDDIAGNOSA & " " & xloop.NAMADIAGNOSA)
            Next

            Dim dsSave = oDigital_DischargePlanning.GetData(lblRegister.Text)

            If dsSave Is Nothing Then
                Try
                    ds.ANAMNESIS_35 = String.Join(vbCrLf, listdiagnosa.ToArray)
                    fn_SaveDischargePlanning = oDigital_DischargePlanning.InsertData(ds, arrDetailDiagnosa)
                Catch ex As Exception
                    MsgBox("Simpan Data Discharge Planning: " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            Else
                Try
                    ds.ANAMNESIS_35 = String.Join(vbCrLf, listdiagnosa.ToArray)
                    fn_SaveDischargePlanning = oDigital_DischargePlanning.UpdateData(ds, arrDetailDiagnosa)
                Catch ex As Exception
                    MsgBox("Simpan Data Discharge Planning: " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            End If
        Catch oErr As Exception
            MsgBox("Simpan Data Discharge Planning: " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            fn_SaveDischargePlanning = False
        End Try
    End Function
    Private Sub fn_LoadDataTemplateDischargePlanning(ByVal kode As String)
        Try
            Dim oS_DIGITAL_RI_13_Template As New Transaksi.clsDigital_DischargePlanningTemplate
            ' ***** HEADER *****
            Dim ds = oS_DIGITAL_RI_13_Template.GetData(kode)

            With ds
                'deDATEAsesment.DateTime = .DATE
                txtANAMNESIS_01.Text = .ANAMNESIS_01
                txtANAMNESIS_02.Text = .ANAMNESIS_02
                txtANAMNESIS_03.Text = .ANAMNESIS_03
                'txtANAMNESIS_04.Text = .ANAMNESIS_04
                cboKeadaanUmumAsesmen.Text = .ANAMNESIS_05
                cboKesadaranAsesmen.Text = .ANAMNESIS_06
                'txtANAMNESIS_07.Text = .ANAMNESIS_07
                txtANAMNESIS_08.Text = .ANAMNESIS_08
                txtANAMNESIS_09.Text = .ANAMNESIS_09
                txtANAMNESIS_10.Text = .ANAMNESIS_10
                txtANAMNESIS_11.Text = .ANAMNESIS_11
                txtANAMNESIS_12.Text = .ANAMNESIS_12
                txtANAMNESIS_13.Text = .ANAMNESIS_13
                txtANAMNESIS_14.Text = .ANAMNESIS_14
                txtSPO2_ASESMENMEDIS.Text = .ANAMNESIS_22
                txtANAMNESIS_15.Text = .ANAMNESIS_15
                txtANAMNESIS_16.Text = .ANAMNESIS_16
                txtANAMNESIS_17.Text = .ANAMNESIS_17
                txtANAMNESIS_18.Text = .ANAMNESIS_18
                txtANAMNESIS_19.Text = .ANAMNESIS_19
                txtANAMNESIS_20.Text = .ANAMNESIS_20
                txtANAMNESIS_29.Text = .ANAMNESIS_29
                'txtANAMNESIS_30.Text = .ANAMNESIS_30
                'txtANAMNESIS_31.Text = .ANAMNESIS_31
                txtANAMNESIS_32.Text = .ANAMNESIS_32
                txtANAMNESIS_33.Text = .ANAMNESIS_33
                txtANAMNESIS_34.Text = .ANAMNESIS_34
                'txtANAMNESIS_35.Text = .ANAMNESIS_35
                txtANAMNESIS_36.Text = .ANAMNESIS_36
                txtANAMNESIS_37.Text = .ANAMNESIS_37
                txtANAMNESIS_38.Text = .ANAMNESIS_38
                cboRencanaPerawatan.Text = .HARI

                chkANAMNESIS_15_1_CHEK.Checked = .ANAMNESIS_15_1_CHEK
                chkANAMNESIS_16_1_CHEK.Checked = .ANAMNESIS_16_1_CHEK
                chkANAMNESIS_17_1_CHEK.Checked = .ANAMNESIS_17_1_CHEK
                chkANAMNESIS_18_1_CHEK.Checked = .ANAMNESIS_18_1_CHEK
                chkANAMNESIS_19_1_CHEK.Checked = .ANAMNESIS_19_1_CHEK
                chkANAMNESIS_20_1_CHEK.Checked = .ANAMNESIS_20_1_CHEK
                chkANAMNESIS_15_2_CHEK.Checked = .ANAMNESIS_15_2_CHEK
                chkANAMNESIS_16_2_CHEK.Checked = .ANAMNESIS_16_2_CHEK
                chkANAMNESIS_17_2_CHEK.Checked = .ANAMNESIS_17_2_CHEK
                chkANAMNESIS_18_2_CHEK.Checked = .ANAMNESIS_18_2_CHEK
                chkANAMNESIS_19_2_CHEK.Checked = .ANAMNESIS_19_2_CHEK
                chkANAMNESIS_20_2_CHEK.Checked = .ANAMNESIS_20_2_CHEK
            End With
        Catch oErr As Exception
            MsgBox("Load List Data Template Discharge Planning: " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Function fn_SaveTemplateDischargePlanning() As Boolean
        Try
            Dim oS_DIGITAL_RI_13_Template As New Transaksi.clsDigital_DischargePlanningTemplate
            Dim oAdd As Boolean = True

            ' ***** HEADER *****
            Dim ds = oS_DIGITAL_RI_13_Template.GetStructureHeader
            With ds
                .KDJUDUL = sKODEASESMENCOPY
                Try
                    .DATECREATED = oS_DIGITAL_RI_13_Template.GetData(sKODEASESMENCOPY).DATECREATED
                Catch ex As Exception
                    oAdd = False
                    .DATECREATED = Now
                End Try
                .DATEUPDATED = Now

                .DATE = Now
                .JAM = Now.ToString("HH:mm", CultureInfo.InvariantCulture)
                .DOCTOR_KODE = ""
                .DOCTOR_NAME_DISPLAY = ""
                .ANAMNESIS_01 = txtANAMNESIS_01.Text
                .ANAMNESIS_02 = txtANAMNESIS_02.Text
                .ANAMNESIS_03 = txtANAMNESIS_03.Text
                .ANAMNESIS_04 = ""
                .ANAMNESIS_05 = cboKeadaanUmumAsesmen.Text
                .ANAMNESIS_06 = cboKesadaranAsesmen.Text
                .ANAMNESIS_07 = ""
                .ANAMNESIS_08 = txtANAMNESIS_08.Text
                .ANAMNESIS_09 = txtANAMNESIS_09.Text
                .ANAMNESIS_10 = txtANAMNESIS_10.Text
                .ANAMNESIS_11 = txtANAMNESIS_11.Text
                .ANAMNESIS_12 = txtANAMNESIS_12.Text
                .ANAMNESIS_13 = txtANAMNESIS_13.Text
                .ANAMNESIS_14 = txtANAMNESIS_14.Text
                .ANAMNESIS_22 = txtSPO2_ASESMENMEDIS.Text
                .ANAMNESIS_15 = txtANAMNESIS_15.Text
                .ANAMNESIS_16 = txtANAMNESIS_16.Text
                .ANAMNESIS_17 = txtANAMNESIS_17.Text
                .ANAMNESIS_18 = txtANAMNESIS_18.Text
                .ANAMNESIS_19 = txtANAMNESIS_19.Text
                .ANAMNESIS_20 = txtANAMNESIS_20.Text
                Try
                    Dim getdata = oS_DIGITAL_RI_13_Template.GetData(sKODEASESMENCOPY)
                    .ANAMNESIS_21 = getdata.ANAMNESIS_15 & ", " & getdata.ANAMNESIS_16 & ", " & getdata.ANAMNESIS_17 & ", " & getdata.ANAMNESIS_18 & ", " & getdata.ANAMNESIS_19 & ", " & getdata.ANAMNESIS_20
                Catch ex As Exception
                    .ANAMNESIS_21 = ""
                End Try
                .ANAMNESIS_22 = ""
                .ANAMNESIS_23 = ""
                .ANAMNESIS_24 = ""
                .ANAMNESIS_25 = ""
                .ANAMNESIS_26 = ""
                .ANAMNESIS_27 = ""
                .ANAMNESIS_28 = txtINDIKASIRAWATDIASESMEN.Text
                .ANAMNESIS_29 = txtANAMNESIS_29.Text
                .ANAMNESIS_30 = ""
                .ANAMNESIS_31 = ""
                .ANAMNESIS_32 = txtANAMNESIS_32.Text
                .ANAMNESIS_33 = txtANAMNESIS_33.Text
                .ANAMNESIS_34 = txtANAMNESIS_34.Text
                .ANAMNESIS_35 = ""
                .ANAMNESIS_36 = txtANAMNESIS_36.Text
                .ANAMNESIS_37 = txtANAMNESIS_37.Text
                .ANAMNESIS_38 = txtANAMNESIS_38.Text
                Try
                    .CETAK = oS_DIGITAL_RI_13_Template.GetData(sKODEASESMENCOPY).CETAK
                Catch ex As Exception
                    .CETAK = 0
                End Try
                .KDUSER = sUserID
                .KDUSER_SIGNATURE = ""
                .HARI = cboRencanaPerawatan.Text
                .ANAMNESIS_15_1_CHEK = chkANAMNESIS_15_1_CHEK.Checked
                .ANAMNESIS_16_1_CHEK = chkANAMNESIS_16_1_CHEK.Checked
                .ANAMNESIS_17_1_CHEK = chkANAMNESIS_17_1_CHEK.Checked
                .ANAMNESIS_18_1_CHEK = chkANAMNESIS_18_1_CHEK.Checked
                .ANAMNESIS_19_1_CHEK = chkANAMNESIS_19_1_CHEK.Checked
                .ANAMNESIS_20_1_CHEK = chkANAMNESIS_20_1_CHEK.Checked
                .ANAMNESIS_15_2_CHEK = chkANAMNESIS_15_2_CHEK.Checked
                .ANAMNESIS_16_2_CHEK = chkANAMNESIS_16_2_CHEK.Checked
                .ANAMNESIS_17_2_CHEK = chkANAMNESIS_17_2_CHEK.Checked
                .ANAMNESIS_18_2_CHEK = chkANAMNESIS_18_2_CHEK.Checked
                .ANAMNESIS_19_2_CHEK = chkANAMNESIS_19_2_CHEK.Checked
                .ANAMNESIS_20_2_CHEK = chkANAMNESIS_20_2_CHEK.Checked
            End With

            If oAdd = False Then
                Try
                    sKODEASESMENCOPY = oS_DIGITAL_RI_13_Template.InsertData(ds)

                    If sKODEASESMENCOPY = "" Then
                        fn_SaveTemplateDischargePlanning = False
                    Else
                        fn_SaveTemplateDischargePlanning = True
                    End If
                Catch ex As Exception
                    MsgBox("Simpan Data Discharge Planning: " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            Else
                Try
                    fn_SaveTemplateDischargePlanning = oS_DIGITAL_RI_13_Template.UpdateData(ds)
                Catch ex As Exception
                    MsgBox("Simpan Data Discharge Planning: " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            End If
        Catch oErr As Exception
            MsgBox("Simpan Data Discharge Planning: " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            fn_SaveTemplateDischargePlanning = False
        End Try
    End Function
#End Region
#Region "Command Button"
    Private Sub Button14_Click() Handles Button14.Click
        If fn_ValidateDischargePlanning() = False Then Exit Sub
        'If MsgBox("Save " & lblRegister.Text.Trim.ToUpper & "?", MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, Me.Text) = MsgBoxResult.No Then Exit Sub
        If fn_SaveDischargePlanning() = False Then
            MsgBox("Save gagal! Tolong kontak admin anda!", MsgBoxStyle.Exclamation, Me.Text)
        Else
            tabbedControlGroup1.SelectedTabPageIndex = 7
            tabbedControlGroup1_SelectedPageChanged()
        End If
    End Sub
    Private Sub chkNORMAL_KEPALA_CheckedChanged(sender As Object, e As EventArgs)
        chkANAMNESIS_15_1_CHEK.Checked = False
        chkANAMNESIS_15_2_CHEK.Checked = False
    End Sub
    Private Sub chkNORMAL_MATA_CheckedChanged(sender As Object, e As EventArgs)
        chkANAMNESIS_16_1_CHEK.Checked = False
        chkANAMNESIS_16_2_CHEK.Checked = False
    End Sub
    Private Sub chkNORMAL_LEHER_CheckedChanged(sender As Object, e As EventArgs)
        chkANAMNESIS_17_1_CHEK.Checked = False
        chkANAMNESIS_17_2_CHEK.Checked = False
    End Sub
    Private Sub chkNORMAL_DADA_CheckedChanged(sender As Object, e As EventArgs)
        chkANAMNESIS_18_1_CHEK.Checked = False
        chkANAMNESIS_18_2_CHEK.Checked = False
    End Sub
    Private Sub chkNORMAL_PERUT_CheckedChanged(sender As Object, e As EventArgs)
        chkANAMNESIS_19_1_CHEK.Checked = False
        chkANAMNESIS_19_2_CHEK.Checked = False
    End Sub
    Private Sub chkNORMAL_ALATGERAK_CheckedChanged(sender As Object, e As EventArgs)
        chkANAMNESIS_20_1_CHEK.Checked = False
        chkANAMNESIS_20_2_CHEK.Checked = False
    End Sub
#End Region
#Region "Lookup / Event"
    Private Sub CheckEdit35_Click(sender As Object, e As EventArgs) Handles CheckEdit35.Click
        CheckEdit34.Checked = False
        CheckEdit33.Checked = False

        txtKeadaanUmum.Text = "Ringan"
    End Sub
    Private Sub CheckEdit34_Click(sender As Object, e As EventArgs) Handles CheckEdit34.Click
        CheckEdit35.Checked = False
        CheckEdit33.Checked = False

        txtKeadaanUmum.Text = "Sedang"
    End Sub
    Private Sub CheckEdit33_Click(sender As Object, e As EventArgs) Handles CheckEdit33.Click
        CheckEdit34.Checked = False
        CheckEdit35.Checked = False

        txtKeadaanUmum.Text = "Berat"
    End Sub
    Private Sub chkKeadaanUmumAsesmenRingan_Click(sender As Object, e As EventArgs) Handles chkKeadaanUmumAsesmenRingan.Click
        chkKeadaanUmumAsesmenSedang.Checked = False
        chkKeadaanUmumAsesmenBerat.Checked = False

        cboKeadaanUmumAsesmen.Text = "Ringan"
    End Sub
    Private Sub chkKeadaanUmumAsesmenSedang_Click(sender As Object, e As EventArgs) Handles chkKeadaanUmumAsesmenSedang.Click
        chkKeadaanUmumAsesmenRingan.Checked = False
        chkKeadaanUmumAsesmenBerat.Checked = False

        cboKeadaanUmumAsesmen.Text = "Sedang"
    End Sub
    Private Sub chkKeadaanUmumAsesmenBerat_Click(sender As Object, e As EventArgs) Handles chkKeadaanUmumAsesmenBerat.Click
        chkKeadaanUmumAsesmenRingan.Checked = False
        chkKeadaanUmumAsesmenSedang.Checked = False

        cboKeadaanUmumAsesmen.Text = "Berat"
    End Sub
    Private Sub chkKesadaranAsesmenComposMentis_Click(sender As Object, e As EventArgs) Handles chkKesadaranAsesmenComposMentis.Click
        chkKesadaranAsesmenSomnolen.Checked = False
        chkKesadaranAsesmenSopor.Checked = False
        chkKesadaranAsesmenKoma.Checked = False

        cboKesadaranAsesmen.Text = "Compos Mentis"
    End Sub
    Private Sub chkKesadaranAsesmenSomnolen_Click(sender As Object, e As EventArgs) Handles chkKesadaranAsesmenSomnolen.Click
        chkKesadaranAsesmenComposMentis.Checked = False
        chkKesadaranAsesmenSopor.Checked = False
        chkKesadaranAsesmenKoma.Checked = False

        cboKesadaranAsesmen.Text = "Somnolen"
    End Sub
    Private Sub chkKesadaranAsesmenSopor_Click(sender As Object, e As EventArgs) Handles chkKesadaranAsesmenSopor.Click
        chkKesadaranAsesmenComposMentis.Checked = False
        chkKesadaranAsesmenSomnolen.Checked = False
        chkKesadaranAsesmenKoma.Checked = False

        cboKesadaranAsesmen.Text = "Sopor"
    End Sub
    Private Sub chkKesadaranAsesmenKoma_Click(sender As Object, e As EventArgs) Handles chkKesadaranAsesmenKoma.Click
        chkKesadaranAsesmenComposMentis.Checked = False
        chkKesadaranAsesmenSomnolen.Checked = False
        chkKesadaranAsesmenSopor.Checked = False

        cboKesadaranAsesmen.Text = "Koma"
    End Sub
    Private Sub CheckEdit53_Click(sender As Object, e As EventArgs) Handles CheckEdit53.Click
        CheckEdit38.Checked = False
        CheckEdit37.Checked = False
        CheckEdit36.Checked = False

        txtKesadaran.Text = "Compos Mentis"
    End Sub
    Private Sub CheckEdit38_Click(sender As Object, e As EventArgs) Handles CheckEdit38.Click
        CheckEdit53.Checked = False
        CheckEdit37.Checked = False
        CheckEdit36.Checked = False

        txtKesadaran.Text = "Somnolen"
    End Sub
    Private Sub CheckEdit37_Click(sender As Object, e As EventArgs) Handles CheckEdit37.Click
        CheckEdit53.Checked = False
        CheckEdit38.Checked = False
        CheckEdit36.Checked = False

        txtKesadaran.Text = "Sopor"
    End Sub
    Private Sub CheckEdit36_Click(sender As Object, e As EventArgs) Handles CheckEdit36.Click
        CheckEdit53.Checked = False
        CheckEdit37.Checked = False
        CheckEdit38.Checked = False

        txtKesadaran.Text = "Koma"
    End Sub
    Private Sub fn_Doctor()
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
            SQL &= "A.KDDOCTOR "
            SQL &= ",NAME_DISPLAY = (SELECT CASE A.FRONT_TITLE WHEN '' THEN '' ELSE A.FRONT_TITLE + ' ' END) + A.NAME_DISPLAY + A.BACK_TITLE "
            SQL &= "FROM  "
            SQL &= "M_DOCTOR A "
            SQL &= "WHERE "
            SQL &= "A.ISACTIVE = '1' "
            SQL &= "AND A.CATEGORY = 1 "
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

        Catch oErr As Exception
            MsgBox("Load Dokter Data : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub LoadAsesmenIGDDischargePlanning(ByVal Kode As String)
        Dim oS_DIGITAL_IGD_01 As New Transaksi.clsDigital_IGD_01
        Try
            Dim ds = oS_DIGITAL_IGD_01.GetData(Kode)
            If ds IsNot Nothing Then
                txtANAMNESIS_01.Text = ds.RIWAYAT_PENYAKITDAHULU
                txtANAMNESIS_01.Text = ds.SURVEY_LEHER_1
                txtANAMNESIS_03.Text = ds.RIWAYAT
                txtANAMNESIS_02.Text = ds.RIWAYAT_PENYAKITDAHULU
                cboKeadaanUmumAsesmen.Text = ds.KEADAANUMUM
                cboKesadaranAsesmen.Text = ds.TINGKATKESADARAN
                txtANAMNESIS_08.Text = ds.BERATBADAN
                txtANAMNESIS_09.Text = ds.TINGGIBADAN
                txtANAMNESIS_11.Text = ds.HR
                txtANAMNESIS_12.Text = ds.BP
                txtANAMNESIS_13.Text = ds.T
                txtANAMNESIS_14.Text = ds.RR
                txtSPO2_ASESMENMEDIS.Text = ds.SP02
                Dim list As New List(Of String)

                For Each xloop In oS_DIGITAL_IGD_01.GetDataDetailPenunjang(ds.KDPENDAFTARAN)
                    list.Add(xloop.PENANGANAN)
                Next

                txtANAMNESIS_33.Text = String.Join(", ", list.ToArray)

                chkANAMNESIS_15_1_CHEK.Checked = ds.NORMAL_KEPALA
                chkANAMNESIS_15_2_CHEK.Checked = ds.TIDAK_NORMAL_KEPALA

                chkANAMNESIS_16_1_CHEK.Checked = ds.NORMAL_MATA
                chkANAMNESIS_16_2_CHEK.Checked = ds.TIDAK_NORMAL_MATA

                chkANAMNESIS_17_1_CHEK.Checked = ds.NORMAL_LEHER
                chkANAMNESIS_17_2_CHEK.Checked = ds.TIDAK_NORMAL_LEHER

                chkANAMNESIS_18_1_CHEK.Checked = ds.NORMAL_DADA
                chkANAMNESIS_18_2_CHEK.Checked = ds.TIDAK_NORMAL_DADA

                chkANAMNESIS_19_1_CHEK.Checked = ds.NORMAL_PERUT
                chkANAMNESIS_19_2_CHEK.Checked = ds.TIDAK_NORMAL_PERUT

                chkANAMNESIS_20_1_CHEK.Checked = ds.NORMAL_ALATGERAK
                chkANAMNESIS_20_2_CHEK.Checked = ds.TIDAK_NORMAL_ALATGERAK

                txtANAMNESIS_15.Text = ds.SURVEY_KEPALA_2
                txtANAMNESIS_16.Text = ds.SURVEY_MATA_2
                txtANAMNESIS_17.Text = ds.SURVEY_LEHER_2
                txtANAMNESIS_18.Text = ds.SURVEY_DADA_2
                txtANAMNESIS_19.Text = ds.SURVEY_PERUT_2
                txtANAMNESIS_20.Text = ds.SURVEY_ALATGERAK_2

                Dim Indikasi As String = String.Empty
                If ds.KRITERIA_01 = True Then
                    Indikasi = "Mengancam nyawa, membahayakan diri dan orang lain/lingkungan"
                ElseIf ds.KRITERIA_02 = True Then
                    Indikasi = "Adanya gangguan pada jalan nafas, pernafasan dan sirkulasi"
                ElseIf ds.KRITERIA_03 = True Then
                    Indikasi = "Adanya penurunan kesadaran"
                ElseIf ds.KRITERIA_04 = True Then
                    Indikasi = "Adanya gangguan hemodiamik"
                ElseIf ds.KRITERIA_05 = True Then
                    Indikasi = "Memerlukan tindakan segera"
                ElseIf ds.KRITERIA_06 = True Then
                    Indikasi = "Tidak Ada Kegawat Daruratan"
                End If

                txtINDIKASIRAWATDIASESMEN.Text = Indikasi

                BindingSourceDiagnosa_Asesmen.DataSource = oS_DIGITAL_IGD_01.GetDataDetail(ds.KDPENDAFTARAN).OrderBy(Function(x) x.SEQ).ToList()
                grdDiagnosaAsesmen.DataSource = BindingSourceDiagnosa_Asesmen
            Else
                Dim dsTerakhir = oS_DIGITAL_IGD_01.GetDataByRMTerakhir(lblNoRM.Text)

                If dsTerakhir IsNot Nothing Then
                    txtANAMNESIS_01.Text = dsTerakhir.RIWAYAT_PENYAKITDAHULU
                    txtANAMNESIS_01.Text = dsTerakhir.SURVEY_LEHER_1
                    txtANAMNESIS_03.Text = dsTerakhir.RIWAYAT
                    txtANAMNESIS_02.Text = dsTerakhir.RIWAYAT_PENYAKITDAHULU
                    cboKeadaanUmumAsesmen.Text = dsTerakhir.KEADAANUMUM
                    cboKesadaranAsesmen.Text = dsTerakhir.TINGKATKESADARAN
                    txtANAMNESIS_08.Text = dsTerakhir.BERATBADAN
                    txtANAMNESIS_09.Text = dsTerakhir.TINGGIBADAN
                    txtANAMNESIS_11.Text = dsTerakhir.HR
                    txtANAMNESIS_12.Text = dsTerakhir.BP
                    txtANAMNESIS_13.Text = dsTerakhir.T
                    txtANAMNESIS_14.Text = dsTerakhir.RR
                    txtSPO2_ASESMENMEDIS.Text = dsTerakhir.SP02
                    Dim list As New List(Of String)

                    For Each xloop In oS_DIGITAL_IGD_01.GetDataDetailPenunjang(dsTerakhir.KDPENDAFTARAN)
                        list.Add(xloop.PENANGANAN)
                    Next

                    txtANAMNESIS_33.Text = String.Join(", ", list.ToArray)

                    chkANAMNESIS_15_1_CHEK.Checked = dsTerakhir.NORMAL_KEPALA
                    chkANAMNESIS_15_2_CHEK.Checked = dsTerakhir.TIDAK_NORMAL_KEPALA

                    chkANAMNESIS_16_1_CHEK.Checked = dsTerakhir.NORMAL_MATA
                    chkANAMNESIS_16_2_CHEK.Checked = dsTerakhir.TIDAK_NORMAL_MATA

                    chkANAMNESIS_17_1_CHEK.Checked = dsTerakhir.NORMAL_LEHER
                    chkANAMNESIS_17_2_CHEK.Checked = dsTerakhir.TIDAK_NORMAL_LEHER

                    chkANAMNESIS_18_1_CHEK.Checked = dsTerakhir.NORMAL_DADA
                    chkANAMNESIS_18_2_CHEK.Checked = dsTerakhir.TIDAK_NORMAL_DADA

                    chkANAMNESIS_19_1_CHEK.Checked = dsTerakhir.NORMAL_PERUT
                    chkANAMNESIS_19_2_CHEK.Checked = dsTerakhir.TIDAK_NORMAL_PERUT

                    chkANAMNESIS_20_1_CHEK.Checked = dsTerakhir.NORMAL_ALATGERAK
                    chkANAMNESIS_20_2_CHEK.Checked = dsTerakhir.TIDAK_NORMAL_ALATGERAK

                    txtANAMNESIS_15.Text = dsTerakhir.SURVEY_KEPALA_2
                    txtANAMNESIS_16.Text = dsTerakhir.SURVEY_MATA_2
                    txtANAMNESIS_17.Text = dsTerakhir.SURVEY_LEHER_2
                    txtANAMNESIS_18.Text = dsTerakhir.SURVEY_DADA_2
                    txtANAMNESIS_19.Text = dsTerakhir.SURVEY_PERUT_2
                    txtANAMNESIS_20.Text = dsTerakhir.SURVEY_ALATGERAK_2

                    Dim Indikasi As String = String.Empty
                    If ds.KRITERIA_01 = True Then
                        Indikasi = "Mengancam nyawa, membahayakan diri dan orang lain/lingkungan"
                    ElseIf ds.KRITERIA_02 = True Then
                        Indikasi = "Adanya gangguan pada jalan nafas, pernafasan dan sirkulasi"
                    ElseIf ds.KRITERIA_03 = True Then
                        Indikasi = "Adanya penurunan kesadaran"
                    ElseIf ds.KRITERIA_04 = True Then
                        Indikasi = "Adanya gangguan hemodiamik"
                    ElseIf ds.KRITERIA_05 = True Then
                        Indikasi = "Memerlukan tindakan segera"
                    ElseIf ds.KRITERIA_06 = True Then
                        Indikasi = "Tidak Ada Kegawat Daruratan"
                    End If

                    txtINDIKASIRAWATDIASESMEN.Text = Indikasi

                    BindingSourceDiagnosa_Asesmen.DataSource = oS_DIGITAL_IGD_01.GetDataDetail(dsTerakhir.KDPENDAFTARAN).OrderBy(Function(x) x.SEQ).ToList()
                    grdDiagnosaAsesmen.DataSource = BindingSourceDiagnosa_Asesmen
                End If
            End If

            If sCOPYKDCPPTPERAWAT <> "" Then
                fn_CopyCPPTAkhirdiAsesmenAwalTTVPerawat(sCOPYKDCPPTPERAWAT)
            End If

        Catch oErr As Exception
            MsgBox("Form Browse Load Assemen Medis Awal IGD" & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_CopyCPPTAkhirdiAsesmenAwalTTVPerawat(ByVal Parameter As String)
        Try
            ' ***** HEADER *****
            Dim dsLainnya = oCPPT.GetDataLainnya(Parameter)

            If dsLainnya IsNot Nothing Then
                txtANAMNESIS_08.Text = dsLainnya.OBJEKTIF_BERATBADAN
                txtANAMNESIS_09.Text = dsLainnya.OBJEKTIF_TINGGIBADAN

                'txtANAMNESIS_11.Text = dsLainnya.OBJEKTIF_TEKANANDARAH
                'txtANAMNESIS_12.Text = dsLainnya.OBJEKTIF_NADI

                txtANAMNESIS_11.Text = dsLainnya.OBJEKTIF_NADI
                txtANAMNESIS_12.Text = dsLainnya.OBJEKTIF_TEKANANDARAH

                txtANAMNESIS_13.Text = dsLainnya.OBJEKTIF_SUHU
                txtANAMNESIS_14.Text = dsLainnya.OBJEKTIF_RESPIRASI
                txtSPO2_ASESMENMEDIS.Text = dsLainnya.OBJEKTIF_SATURASIOKSIGEN
            End If

        Catch oErr As Exception
            MsgBox("Load List Data : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
#End Region
#End Region
#Region "Asesmen Lembar EWS"
    Private Sub fn_LoadDataLembarEWS(ByVal Parameter As String)
        Try
            grvAsesmenAwalPerawat.Columns.Clear()
            grdAsesmenAwalPerawat.DataSource = Nothing

            If Parameter = String.Empty Then Exit Sub

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

            'Lembar EWS NEWS
            SQL = "SELECT "
            SQL &= "* "
            SQL &= "FROM ( "
            SQL &= "(SELECT "
            SQL &= "Tanggal = A.DATEUPDATED "
            SQL &= ",FORMULIR = 'LEMBAR EWS NEWS' "
            SQL &= ",Register = A.KDPENDAFTARAN "
            SQL &= ",Kode = A.KODE "
            SQL &= ",A.KDUSER "
            SQL &= "FROM "
            SQL &= "S_DIGITAL_LEMBARNEWS_H A "
            SQL &= "WHERE A.KDCUSTOMER = '" & Parameter & "' "

            'Lembar EWS PEWS
            SQL &= ")UNION ALL( "

            SQL &= "SELECT "
            SQL &= "Tanggal = A.DATE "
            SQL &= ",FORMULIR = 'LEMBAR EWS PEWS' "
            SQL &= ",Register = A.KDPENDAFTARAN "
            SQL &= ",Kode = A.KDPEWS "
            SQL &= ",A.KDUSER "
            SQL &= "FROM "
            SQL &= "S_DIGITAL_LEMBARPEWS_H A "
            SQL &= "WHERE A.KDCUSTOMER = '" & Parameter & "' "

            'Lembar EWS MEOWS
            SQL &= ")UNION ALL( "

            SQL &= "SELECT "
            SQL &= "Tanggal = A.DATE "
            SQL &= ",FORMULIR = 'LEMBAR EWS MEOWS' "
            SQL &= ",Register = A.KDPENDAFTARAN "
            SQL &= ",Kode = A.KDMEOWS "
            SQL &= ",A.KDUSER "
            SQL &= "FROM "
            SQL &= "S_DIGITAL_LEMBARMEOWS_H A "
            SQL &= "WHERE A.KDCUSTOMER = '" & Parameter & "' "

            SQL &= ") "
            SQL &= ") AS X "
            SQL &= "ORDER BY X.Tanggal DESC "

            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "LEMBAREWS")

            grdLembarEWS.MainView = grvLembarEWS
            grdLembarEWS.DataSource = ds.Tables("LEMBAREWS")
            grdLembarEWS.ForceInitialize()

            fn_LoadFormatLembarEWS()

            If oConn.State = ConnectionState.Open Then
                oConn.Close()
            End If
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadFormatLembarEWS()
        For iLoop As Integer = 0 To grvLembarEWS.Columns.Count - 1
            If grvLembarEWS.Columns(iLoop).ColumnType.Name = "Decimal" Then
                grvLembarEWS.Columns(iLoop).DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                grvLembarEWS.Columns(iLoop).DisplayFormat.FormatString = "{0:n2}"
                grvLembarEWS.Columns(iLoop).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
            ElseIf grvLembarEWS.Columns(iLoop).ColumnType.Name = "DateTime" Then
                grvLembarEWS.Columns(iLoop).DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
                grvLembarEWS.Columns(iLoop).DisplayFormat.FormatString = "{0:dd/MM/yyyy}"
            End If
        Next

        grvLembarEWS.Columns("FORMULIR").Caption = "Formulir"
        grvLembarEWS.Columns("KDUSER").Caption = "User"

        grvLembarEWS.Columns("Kode").VisibleIndex = -1
        grvLembarEWS.Columns("Register").VisibleIndex = -1
        grvLembarEWS.BestFitColumns()
    End Sub
    Private Sub grvLembarEWS_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles grvLembarEWS.DoubleClick
        If grvLembarEWS.GetFocusedRowCellValue("Kode") Is Nothing Then
            Exit Sub
        End If

        If grvLembarEWS.GetFocusedRowCellValue("FORMULIR") = "LEMBAR EWS NEWS" Then
            Dim frmLembarNEWS As New frmLembarNEWS
            Try
                frmLembarNEWS.LoadMe(FORM_MODE.FORM_MODE_VIEW, grvLembarEWS.GetFocusedRowCellValue("Register"), lblNoRM.Text, lblNamaPasien.Text, lblJenisKelamin.Text, grvLembarEWS.GetFocusedRowCellValue("KDUSER"), grvLembarEWS.GetFocusedRowCellValue("Kode"))
                frmLembarNEWS.ShowDialog(Me)
            Catch oErr As Exception
                MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            End Try
        ElseIf grvLembarEWS.GetFocusedRowCellValue("FORMULIR") = "LEMBAR EWS PEWS" Then
            Dim frmLembarPEWS As New frmLembarPEWS
            Try
                frmLembarPEWS.LoadMe(FORM_MODE.FORM_MODE_VIEW, grvLembarEWS.GetFocusedRowCellValue("Register"), lblNoRM.Text, lblNamaPasien.Text, lblJenisKelamin.Text, grvLembarEWS.GetFocusedRowCellValue("KDUSER"), grvLembarEWS.GetFocusedRowCellValue("Kode"))
                frmLembarPEWS.ShowDialog(Me)
            Catch oErr As Exception
                MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            End Try
        ElseIf grvLembarEWS.GetFocusedRowCellValue("FORMULIR") = "LEMBAR EWS MEOWS" Then
            Dim frmLembarMEOWS As New frmLembarMEOWS
            Try
                frmLembarMEOWS.LoadMe(FORM_MODE.FORM_MODE_VIEW, grvLembarEWS.GetFocusedRowCellValue("Register"), lblNoRM.Text, lblNamaPasien.Text, lblJenisKelamin.Text, grvLembarEWS.GetFocusedRowCellValue("KDUSER"), grvLembarEWS.GetFocusedRowCellValue("Kode"))
                frmLembarMEOWS.ShowDialog(Me)
            Catch oErr As Exception
                MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            End Try
        End If
    End Sub
    Private Sub picAddLembarEWS_Click() Handles picAddLembarEWS.Click
        'If sISDOKTER = True Then
        '    MsgBox("Silahkan Pilih Modul Perawat", MsgBoxStyle.Exclamation, Me.Text)
        '    Exit Sub
        'End If

        If cboLembarEWS.Text = "LEMBAR EWS NEWS" Then
            Dim frmLembarNEWS As New frmLembarNEWS
            Try
                frmLembarNEWS.LoadMe(FORM_MODE.FORM_MODE_ADD, lblRegister.Text, lblNoRM.Text, lblNamaPasien.Text, lblJenisKelamin.Text, sKDUSER_PERAWAT)
                frmLembarNEWS.ShowDialog(Me)
                XtraTabControl1_SelectedPageChanged()
            Catch oErr As Exception
                MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            Finally
                If Not frmLembarNEWS Is Nothing Then frmLembarNEWS.Dispose()
                frmLembarNEWS = Nothing

                Dim rowHandle As Integer = grvTransferInternal.LocateByValue(rowHandle, grvAsesmenAwalPerawat.Columns("Kode"), sCode)
                If rowHandle > 0 Then grvTransferInternal.FocusedRowHandle = rowHandle
            End Try
        ElseIf cboAsesmenAwalPerawat.Text = "LEMBAR EWS PEWS" Then
            Dim frmLembarNEWS As New frmLembarNEWS
            Try
                frmLembarPEWS.LoadMe(FORM_MODE.FORM_MODE_ADD, lblRegister.Text, lblNoRM.Text, lblNamaPasien.Text, lblJenisKelamin.Text, sKDUSER_PERAWAT)
                frmLembarPEWS.ShowDialog(Me)
                XtraTabControl1_SelectedPageChanged()
            Catch oErr As Exception
                MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            Finally
                If Not frmLembarPEWS Is Nothing Then frmLembarPEWS.Dispose()
                frmLembarPEWS = Nothing

                Dim rowHandle As Integer = grvTransferInternal.LocateByValue(rowHandle, grvAsesmenAwalPerawat.Columns("Kode"), sCode)
                If rowHandle > 0 Then grvTransferInternal.FocusedRowHandle = rowHandle
            End Try
        ElseIf cboAsesmenAwalPerawat.Text = "LEMBAR EWS MEOWS" Then
            Dim frmLembarMEOWS As New frmLembarMEOWS
            Try
                frmLembarPEWS.LoadMe(FORM_MODE.FORM_MODE_ADD, lblRegister.Text, lblNoRM.Text, lblNamaPasien.Text, lblJenisKelamin.Text, sKDUSER_PERAWAT)
                frmLembarPEWS.ShowDialog(Me)
                XtraTabControl1_SelectedPageChanged()
            Catch oErr As Exception
                MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            Finally
                If Not frmLembarPEWS Is Nothing Then frmLembarPEWS.Dispose()
                frmLembarPEWS = Nothing

                Dim rowHandle As Integer = grvTransferInternal.LocateByValue(rowHandle, grvAsesmenAwalPerawat.Columns("Kode"), sCode)
                If rowHandle > 0 Then grvTransferInternal.FocusedRowHandle = rowHandle
            End Try
        End If
    End Sub
    Private Sub picEditLembarEWS_Click() Handles picEditLembarEWS.Click
        If grvLembarEWS.GetFocusedRowCellValue("Kode") Is Nothing Then
            Exit Sub
        End If

        'If sISDOKTER = True Then
        '    MsgBox("Silahkan Pili Modul Perawat", MsgBoxStyle.Exclamation, Me.Text)
        '    Exit Sub
        'End If

        If grvLembarEWS.GetFocusedRowCellValue("FORMULIR") = "LEMBAR EWS NEWS" Then
            Dim frmLembarNEWS As New frmLembarNEWS
            Try
                frmLembarNEWS.LoadMe(FORM_MODE.FORM_MODE_EDIT, grvLembarEWS.GetFocusedRowCellValue("Register"), lblNoRM.Text, lblNamaPasien.Text, lblJenisKelamin.Text, sKDUSER_PERAWAT, grvLembarEWS.GetFocusedRowCellValue("Kode"))
                frmLembarNEWS.ShowDialog(Me)
                XtraTabControl1_SelectedPageChanged()
            Catch oErr As Exception
                MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            Finally
                If Not frmLembarNEWS Is Nothing Then frmLembarNEWS.Dispose()
                frmLembarNEWS = Nothing

                Dim rowHandle As Integer = grvTransferInternal.LocateByValue(rowHandle, grvLembarEWS.Columns("Kode"), sCode)
                If rowHandle > 0 Then grvTransferInternal.FocusedRowHandle = rowHandle
            End Try
        ElseIf grvLembarEWS.GetFocusedRowCellValue("FORMULIR") = "LEMBAR EWS PEWS" Then
            Dim frmLembarPEWS As New frmLembarPEWS
            Try
                frmLembarPEWS.LoadMe(FORM_MODE.FORM_MODE_EDIT, grvLembarEWS.GetFocusedRowCellValue("Register"), lblNoRM.Text, lblNamaPasien.Text, lblJenisKelamin.Text, sKDUSER_PERAWAT, grvLembarEWS.GetFocusedRowCellValue("Kode"))
                frmLembarPEWS.ShowDialog(Me)
                XtraTabControl1_SelectedPageChanged()
            Catch oErr As Exception
                MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            Finally
                If Not frmLembarPEWS Is Nothing Then frmLembarPEWS.Dispose()
                frmLembarPEWS = Nothing

                Dim rowHandle As Integer = grvTransferInternal.LocateByValue(rowHandle, grvLembarEWS.Columns("Kode"), sCode)
                If rowHandle > 0 Then grvTransferInternal.FocusedRowHandle = rowHandle
            End Try
        ElseIf grvLembarEWS.GetFocusedRowCellValue("FORMULIR") = "LEMBAR EWS MEOWS" Then
            Dim frmLembarMEOWS As New frmLembarMEOWS
            Try
                frmLembarMEOWS.LoadMe(FORM_MODE.FORM_MODE_EDIT, grvLembarEWS.GetFocusedRowCellValue("Register"), lblNoRM.Text, lblNamaPasien.Text, lblJenisKelamin.Text, sKDUSER_PERAWAT, grvLembarEWS.GetFocusedRowCellValue("Kode"))
                frmLembarMEOWS.ShowDialog(Me)
                XtraTabControl1_SelectedPageChanged()
            Catch oErr As Exception
                MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            Finally
                If Not frmLembarMEOWS Is Nothing Then frmLembarMEOWS.Dispose()
                frmLembarMEOWS = Nothing

                Dim rowHandle As Integer = grvTransferInternal.LocateByValue(rowHandle, grvLembarEWS.Columns("Kode"), sCode)
                If rowHandle > 0 Then grvTransferInternal.FocusedRowHandle = rowHandle
            End Try
        End If
    End Sub
    Private Sub picDeleteLembarEWS_Click() Handles picDeleteLembarEWS.Click
        If sISDOKTER = True Then
            MsgBox("Silahkan Pili Modul Perawat", MsgBoxStyle.Exclamation, Me.Text)
            Exit Sub
        End If

        MsgBox("Belum Tersedia", MsgBoxStyle.Exclamation, Me.Text)

        Exit Sub

        If grvAsesmenAwalPerawat.GetFocusedRowCellValue("Kode") Is Nothing Then
            Exit Sub
        End If

        If grvLembarEWS.GetFocusedRowCellValue("FORMULIR") = "LEMBAR EWS NEWS" Then

        ElseIf grvLembarEWS.GetFocusedRowCellValue("FORMULIR") = "LEMBAR EWS PEWS" Then

        ElseIf grvLembarEWS.GetFocusedRowCellValue("FORMULIR") = "LEMBAR EWS MEOWS" Then

        End If

        MsgBox(Statement.DeleteSuccess, MsgBoxStyle.Information, Me.Text)
        XtraTabControl1_SelectedPageChanged()
    End Sub
    Private Sub picRefreshLembarEWS_Click() Handles picRefreshLembarEWS.Click
        XtraTabControl1_SelectedPageChanged()
    End Sub
#End Region
#Region "Instalasi Farmasi"
#Region "CPPT Farmasi"
    Private Sub btnSimpanCPPTFarmasi_Click() Handles btnSimpanCPPTFarmasi.Click
        If fn_ValidateCPPT(True) = False Then Exit Sub
        If MsgBox("Save CPPT Jam " & Now.ToString("HH:mm", CultureInfo.InvariantCulture) & "?", MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, Me.Text) = MsgBoxResult.No Then Exit Sub
        If fn_SaveCPPT(lblKODECPPT.Text, False, True) = False Then
            MsgBox("Save gagal! Tolong kontak admin anda!", MsgBoxStyle.Exclamation, Me.Text)
        Else
            'MsgBox("Save success!", MsgBoxStyle.Information, Me.Text)
            tabbedControlGroup1.SelectedTabPageIndex = 0
            tabbedControlGroup1_SelectedPageChanged()
        End If
    End Sub
#End Region
#Region "Rekonsiliasi Obat"
    Private Sub fn_LoadDataRekonsiliasiObat()
        Try
            Dim ds = From x In oS_DIGITAL_FARMASI_REKONSILIASIOBAT_H.GetDataByRM(lblNoRM.Text)
                     Select x.KDREKONSILIASI, x.KDPENDAFTARAN, TANGGAL = x.DATE, RUANGAN = x.KDUSER_SIGNATURE, x.KDUSER

            grdRekonsiliasiObat.DataSource = ds.ToList

            fn_LoadFormatDataRekonsiliasiObat()
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadFormatDataRekonsiliasiObat()
        For iLoop As Integer = 0 To grvRekonsiliasiObat.Columns.Count - 1
            If grvRekonsiliasiObat.Columns(iLoop).ColumnType.Name = "Decimal" Then
                grvRekonsiliasiObat.Columns(iLoop).DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                grvRekonsiliasiObat.Columns(iLoop).DisplayFormat.FormatString = "{0:n2}"
                grvRekonsiliasiObat.Columns(iLoop).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
            ElseIf grvRekonsiliasiObat.Columns(iLoop).ColumnType.Name = "DateTime" Then
                grvRekonsiliasiObat.Columns(iLoop).DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
                grvRekonsiliasiObat.Columns(iLoop).DisplayFormat.FormatString = "{0:dd/MM/yyyy}"
            End If
        Next

        grvRekonsiliasiObat.Columns("KDREKONSILIASI").VisibleIndex = -1
        grvRekonsiliasiObat.Columns("KDPENDAFTARAN").VisibleIndex = -1
    End Sub
    Private Function fn_DeleteDataRekonsiliasiObat(ByVal sKDREKONSILIASI As String) As Boolean
        Try
            oS_DIGITAL_FARMASI_REKONSILIASIOBAT_H.DeleteData(sKDREKONSILIASI)

            fn_DeleteDataRekonsiliasiObat = True
        Catch oErr As Exception
            fn_DeleteDataRekonsiliasiObat = False
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Function
    Private Sub grvRekonsiliasiObat_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles grvRekonsiliasiObat.DoubleClick
        If grvRekonsiliasiObat.GetFocusedRowCellValue("KDREKONSILIASI") Is Nothing Then
            Exit Sub
        End If

        Dim frmRekonsiliasiObat As New frmRekonsiliasiObat
        Try
            frmRekonsiliasiObat.LoadMe(FORM_MODE.FORM_MODE_VIEW, "", grvRekonsiliasiObat.GetFocusedRowCellValue("KDPENDAFTARAN"), lblNoRM.Text, lblNamaPasien.Text, sTanggalLahir, sUMURPASIEN, lblJenisKelamin.Text, "-", grvRekonsiliasiObat.GetFocusedRowCellValue("RUANGAN"), sKDDOCTOR_INPUT, grvRekonsiliasiObat.GetFocusedRowCellValue("KDREKONSILIASI"))
            frmRekonsiliasiObat.ShowDialog(Me)
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub picAddReconsiliasiObat_Click() Handles picAddReconsiliasiObat.Click
        Dim frmRekonsiliasiObat As New frmRekonsiliasiObat
        Try
            frmRekonsiliasiObat.LoadMe(FORM_MODE.FORM_MODE_ADD, "", lblRegister.Text, lblNoRM.Text, lblNamaPasien.Text, sTanggalLahir, sUMURPASIEN, lblJenisKelamin.Text, "-", lblRuangan.Text, sKDDOCTOR_INPUT, "")
            frmRekonsiliasiObat.ShowDialog(Me)
            XtraTabControl1_SelectedPageChanged()
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        Finally
            If Not frmRekonsiliasiObat Is Nothing Then frmRekonsiliasiObat.Dispose()
            frmRekonsiliasiObat = Nothing

            Dim rowHandle As Integer = grvRekonsiliasiObat.LocateByValue(rowHandle, grvRekonsiliasiObat.Columns("KDREKONSILIASI"), sCode)
            If rowHandle > 0 Then grvRekonsiliasiObat.FocusedRowHandle = rowHandle

            If sStatusSave = "NEW" Then
                sStatusSave = "NONE"
            End If
        End Try
    End Sub
    Private Sub picEditReconsiliasiObat_Click() Handles picEditReconsiliasiObat.Click
        If grvRekonsiliasiObat.GetFocusedRowCellValue("KDREKONSILIASI") Is Nothing Then
            Exit Sub
        End If
        Dim frmRekonsiliasiObat As New frmRekonsiliasiObat
        Try
            frmRekonsiliasiObat.LoadMe(FORM_MODE.FORM_MODE_EDIT, "", grvRekonsiliasiObat.GetFocusedRowCellValue("KDPENDAFTARAN"), lblNoRM.Text, lblNamaPasien.Text, sTanggalLahir, sUMURPASIEN, lblJenisKelamin.Text, "-", grvRekonsiliasiObat.GetFocusedRowCellValue("RUANGAN"), sKDDOCTOR_INPUT, grvRekonsiliasiObat.GetFocusedRowCellValue("KDREKONSILIASI"))
            frmRekonsiliasiObat.ShowDialog(Me)
            XtraTabControl1_SelectedPageChanged()
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        Finally
            If Not frmRekonsiliasiObat Is Nothing Then frmRekonsiliasiObat.Dispose()
            frmRekonsiliasiObat = Nothing

            Dim rowHandle As Integer = grvRekonsiliasiObat.LocateByValue(rowHandle, grvRekonsiliasiObat.Columns("KDREKONSILIASI"), sCode)
            If rowHandle > 0 Then grvRekonsiliasiObat.FocusedRowHandle = rowHandle

            If sStatusSave = "NEW" Then
                sStatusSave = "NONE"
                picAddReconsiliasiObat_Click()
            End If
        End Try
    End Sub
    Private Sub picDeleteReconsiliasiObat_Click() Handles picDeleteReconsiliasiObat.Click
        If grvRekonsiliasiObat.GetFocusedRowCellValue("KDREKONSILIASI") Is Nothing Then
            Exit Sub
        End If
        If MsgBox(Statement.DeleteQuestion, MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, Me.Text) = MsgBoxResult.No Then Exit Sub
        If fn_DeleteDataRekonsiliasiObat(grvRekonsiliasiObat.GetFocusedRowCellValue("KDREKONSILIASI")) = False Then
            MsgBox(Statement.DeleteFail, MsgBoxStyle.Exclamation, Me.Text)
            Exit Sub
        End If
        MsgBox(Statement.DeleteSuccess, MsgBoxStyle.Information, Me.Text)
        XtraTabControl1_SelectedPageChanged()
    End Sub
    Private Sub picRefreshReconsiliasiObat_Click() Handles picRefreshReconsiliasiObat.Click
        XtraTabControl1_SelectedPageChanged()
    End Sub
#End Region
#End Region
#Region "Laporan Operasi"
#Region "Function"
    Private Sub fn_LoadLaporanOperasi(ByVal RekamMedis As String)
        Try
            grvListLaporanOperasi.OptionsSelection.MultiSelect = True
            grvListLaporanOperasi.SelectAll()
            grvListLaporanOperasi.DeleteSelectedRows()
            grvListLaporanOperasi.OptionsSelection.MultiSelect = False

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
            SQL &= "A.KDLAPORANOPERASI "
            SQL &= ",TANGGAL = A.DATE "
            SQL &= ",A.JENIS_OPERASI "
            SQL &= ",[USER] = A.KDUSER "
            SQL &= "FROM "
            SQL &= "S_DIGITAL_OK_LAPORANOPERASI A "
            SQL &= "WHERE "
            SQL &= "A.KDCUSTOMER = '" & RekamMedis & "' "
            SQL &= "AND A.ISDELETE = '0' "

            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "S_DIGITAL_OK_LAPORANOPERASI")

            grdListLaporanOperasi.DataSource = ds.Tables("S_DIGITAL_OK_LAPORANOPERASI")
            grdListLaporanOperasi.ForceInitialize()

            If oConn.State = ConnectionState.Open Then
                oConn.Close()
            End If

            grvListLaporanOperasi.Columns("KDLAPORANOPERASI").Visible = False

            For iLoop As Integer = 0 To grvResume.Columns.Count - 1
                If grvListLaporanOperasi.Columns(iLoop).ColumnType.Name = "Decimal" Then
                    grvListLaporanOperasi.Columns(iLoop).DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                    grvListLaporanOperasi.Columns(iLoop).DisplayFormat.FormatString = "{0:n2}"
                    grvListLaporanOperasi.Columns(iLoop).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
                ElseIf grvListLaporanOperasi.Columns(iLoop).ColumnType.Name = "DateTime" Then
                    grvListLaporanOperasi.Columns(iLoop).DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
                    grvListLaporanOperasi.Columns(iLoop).DisplayFormat.FormatString = "{0:dd/MM/yyyy}"
                End If
            Next

            grvListLaporanOperasi.Columns("JENIS_OPERASI").Caption = "JENIS"

        Catch oErr As Exception
            MsgBox("LOAD LAPORAN OPERASI : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_ChangeFormStateLaporanOperasi()
        If isLoadLaporanOperasi = True Then Exit Sub

        fn_JENISOPERASI()

        Dim ds = oLaporanOperasi.GetDataKode(lblKodeLaporanOperasi.Text)

        If ds Is Nothing Then
            sKODELAPORANOPERASI = String.Empty
            fn_EmptyMeLaporanOperasi()
        Else
            sKODELAPORANOPERASI = ds.KDLAPORANOPERASI
            fn_LoadDataLaporanOperasi()
        End If

        'TextEdit18.Text = sUserID

        isLoadLaporanOperasi = True

    End Sub
    Private Sub fn_EmptyMeLaporanOperasi()
        DateEdit1.DateTime = Now
        grdOperator_LO.ResetText()
        grdAnestesi_LO.ResetText()
        grdAsisten1_LO.ResetText()
        grdAsisten2_LO.ResetText()
        'TextEdit10.ResetText()
        TextEdit11.ResetText()
        TextEdit12.ResetText()
        TimeEdit13.ResetText()
        TimeEdit14.ResetText()
        TimeEdit15.ResetText()
        txtLamaOperasi.ResetText()
        TextEdit17.ResetText()
        'TextEdit18.Text = sUserID
        TextEdit19.ResetText()
        grdPerawatAnestesiLO.ResetText()
        MemoEdit4.ResetText()
        MemoEdit5.Text = "-"
        MemoEdit6.ResetText()
        CheckEdit1.Checked = False
        CheckEdit2.Checked = False
        CheckEdit3.Checked = False
        CheckEdit4.Checked = False
        CheckEdit5.Checked = False
        CheckEdit6.Checked = False
        CheckEdit7.Checked = False
        CheckEdit8.Checked = False
        CheckEdit9.Checked = False
        CheckEdit10.Checked = False
        CheckEdit11.Checked = False
        CheckEdit12.Checked = False
        CheckEdit13.Checked = False
        CheckEdit14.Checked = False
        CheckEdit15.Checked = False
        CheckEdit16.Checked = False
        CheckEdit17.Checked = False
        CheckEdit18.Checked = False
        CheckEdit19.Checked = False
        CheckEdit20.Checked = False
        cboJenisOperasi.ResetText()
        chkPemasanganImpan_Tidak.Checked = False
        chkPemasanganImpan_Ya.Checked = False
        grdPerawatInstrumen.ResetText()

        MemoEdit1.ResetText()
        MemoEdit2.ResetText()
        MemoEdit3.ResetText()

        TextEdit19.Text = sKDDOCTOR_INPUT
        grdOperator_LO.Text = sKDDOCTOR_INPUT
        grdPembuatLaporan.Text = sKDDOCTOR_INPUT

        grvLODiagnosaPre.OptionsSelection.MultiSelect = True
        grvLODiagnosaPre.SelectAll()
        grvLODiagnosaPre.DeleteSelectedRows()
        grvLODiagnosaPre.OptionsSelection.MultiSelect = False

        grvLODiagnosa2.OptionsSelection.MultiSelect = True
        grvLODiagnosa2.SelectAll()
        grvLODiagnosa2.DeleteSelectedRows()
        grvLODiagnosa2.OptionsSelection.MultiSelect = False

        grvLOProsedur.OptionsSelection.MultiSelect = True
        grvLOProsedur.SelectAll()
        grvLOProsedur.DeleteSelectedRows()
        grvLOProsedur.OptionsSelection.MultiSelect = False

        grdCariDiganosaLO1.ResetText()
        grdCariDiganosaLO2.ResetText()
        grdCariProsedur_LO.ResetText()

        grdPembuatLaporan.ResetText()
        grdPembuatLaporan.ResetText()
        grdPembuatLaporan.ResetText()
    End Sub
    Private Sub fn_LoadDataLaporanOperasi()
        Try
            ' ***** HEADER *****
            Dim ds = oLaporanOperasi.GetDataKode(sKODELAPORANOPERASI)
            With ds
                DateEdit1.DateTime = .DATE
                grdOperator_LO.EditValue = .DOKTER_KODE
                grdAnestesi_LO.EditValue = .TextEdit3
                TextEdit19.EditValue = .TextEdit19

                grdAsisten1_LO.EditValue = .TextEdit5
                grdAsisten2_LO.Text = .TextEdit6
                grdPerawatInstrumen.Text = .TextEdit7
                grdPerawatAnestesiLO.Text = .TextEdit9

                'TextEdit10.Text = .TextEdit10
                TextEdit11.Text = .TextEdit11
                TextEdit12.Text = .TextEdit12
                TimeEdit13.Time = .TextEdit13
                TimeEdit14.Time = .TextEdit14
                TimeEdit15.Time = .TextEdit15
                txtLamaOperasi.Text = .TextEdit16
                TextEdit17.Text = .TextEdit17
                grdPembuatLaporan.Text = .TextEdit18

                MemoEdit4.Text = .MemoEdit4
                MemoEdit5.Text = .MemoEdit5
                MemoEdit6.Text = .MemoEdit6
                CheckEdit1.Checked = .CheckEdit1
                CheckEdit2.Checked = .CheckEdit2
                CheckEdit3.Checked = .CheckEdit3
                CheckEdit4.Checked = .CheckEdit4
                CheckEdit5.Checked = .CheckEdit5
                CheckEdit6.Checked = .CheckEdit6
                CheckEdit7.Checked = .CheckEdit7
                CheckEdit8.Checked = .CheckEdit8
                CheckEdit9.Checked = .CheckEdit9
                CheckEdit10.Checked = .CheckEdit10
                CheckEdit11.Checked = .CheckEdit11
                CheckEdit12.Checked = .CheckEdit12
                CheckEdit13.Checked = .CheckEdit13
                CheckEdit14.Checked = .CheckEdit14
                CheckEdit15.Checked = .CheckEdit15
                CheckEdit16.Checked = .CheckEdit16
                CheckEdit17.Checked = .CheckEdit17
                CheckEdit18.Checked = .CheckEdit18
                CheckEdit19.Checked = .CheckEdit19
                CheckEdit20.Checked = .CheckEdit20
                cboJenisOperasi.EditValue = .JENIS_OPERASI
                'txtDokter_LO.EditValue = .DOKTER_KODE

                MemoEdit1.Text = .MemoEdit1
                MemoEdit2.Text = .MemoEdit2
                MemoEdit3.Text = .MemoEdit3

                chkPemasanganImpan_Tidak.Checked = .PEMASANGANIMPLAN_TIDAK
                chkPemasanganImpan_Ya.Checked = .PEMASANGANIMPLAN_YA

                BindingSourceLODiagnosa.DataSource = oLaporanOperasi.GetDataDetailDiagnosa(.KDLAPORANOPERASI).OrderBy(Function(x) x.SEQ).ToList()
                grdLODiagnosaPre.DataSource = BindingSourceLODiagnosa

                BindingSourceLODiagnosa_2.DataSource = oLaporanOperasi.GetDataDetailDiagnosa2(.KDLAPORANOPERASI).OrderBy(Function(x) x.SEQ).ToList()
                grdLODiagnosa2.DataSource = BindingSourceLODiagnosa_2

                BindingSourceLOProsedur.DataSource = oLaporanOperasi.GetDataDetailProsedur(.KDLAPORANOPERASI).OrderBy(Function(x) x.SEQ).ToList()
                grdLOProsedur.DataSource = BindingSourceLOProsedur
            End With
        Catch oErr As Exception
            MsgBox("Load List Data Laporan Operasi: " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Function fn_ValidateLaporanOperasi() As Boolean
        Try
            fn_ValidateLaporanOperasi = True

            If lblRegister.Text = String.Empty Then
                MsgBox("Dibutuhkan Register", MsgBoxStyle.Exclamation, Me.Text)
                fn_ValidateLaporanOperasi = False
                Exit Function
            End If
            If cboJenisOperasi.Text = String.Empty Then
                MsgBox("Dibutuhkan Laporan Jenis Operasi", MsgBoxStyle.Exclamation, Me.Text)
                cboJenisOperasi.Focus()
                fn_ValidateLaporanOperasi = False
                Exit Function
            End If


            If MemoEdit3.Text = "" Then
                MsgBox("Posisi Pasien pada saat Operasi Masih Kosong", MsgBoxStyle.Exclamation, Me.Text)
                MemoEdit3.Focus()
                fn_ValidateLaporanOperasi = False
                Exit Function
            End If

            Dim kamar As Boolean = False
            If CheckEdit1.Checked = True Then
                kamar = True
            End If
            If CheckEdit2.Checked = True Then
                kamar = True
            End If
            If CheckEdit3.Checked = True Then
                kamar = True
            End If
            If CheckEdit4.Checked = True Then
                kamar = True
            End If
            If CheckEdit5.Checked = True Then
                kamar = True
            End If
            If CheckEdit6.Checked = True Then
                kamar = True
            End If
            If kamar = False Then
                MsgBox("kamar Operasi Belum di Pilih", MsgBoxStyle.Exclamation, Me.Text)
                CheckEdit1.Focus()
                fn_ValidateLaporanOperasi = False
                Exit Function
            End If
            If grdAnestesi_LO.Text = "" Then
                MsgBox("Dokter Anestesi Masih Kosong", MsgBoxStyle.Exclamation, Me.Text)
                grdAnestesi_LO.Focus()
                fn_ValidateLaporanOperasi = False
                Exit Function
            End If
            If grdAsisten1_LO.Text = "" Then
                MsgBox("Asisten 1 Masih Kosong", MsgBoxStyle.Exclamation, Me.Text)
                grdAsisten1_LO.Focus()
                fn_ValidateLaporanOperasi = False
                Exit Function
            End If
            If grdPerawatInstrumen.Text = "" Then
                MsgBox("Perawat Instrumen Masih Kosong", MsgBoxStyle.Exclamation, Me.Text)
                grdPerawatInstrumen.Focus()
                fn_ValidateLaporanOperasi = False
                Exit Function
            End If
            If grdPerawatAnestesiLO.Text = "" Then
                MsgBox("Perawat Anestesi Masih Kosong", MsgBoxStyle.Exclamation, Me.Text)
                grdPerawatAnestesiLO.Focus()
                fn_ValidateLaporanOperasi = False
                Exit Function
            End If

            Dim JenisAnestesi As Boolean = False
            If CheckEdit7.Checked = True Then
                JenisAnestesi = True
            End If
            If CheckEdit8.Checked = True Then
                JenisAnestesi = True
            End If
            If CheckEdit9.Checked = True Then
                JenisAnestesi = True
            End If
            If CheckEdit10.Checked = True Then
                JenisAnestesi = True
            End If

            If JenisAnestesi = False Then
                MsgBox("Jenis Anestesi Belum di Pilih", MsgBoxStyle.Exclamation, Me.Text)
                CheckEdit7.Focus()
                fn_ValidateLaporanOperasi = False
                Exit Function
            End If

            Dim PemeriksaanCairan As Boolean = False
            If CheckEdit19.Checked = True Then
                PemeriksaanCairan = True
            End If
            If CheckEdit20.Checked = True Then
                PemeriksaanCairan = True
            End If

            If PemeriksaanCairan = False Then
                MsgBox("Pemeriksaan Cairan Belum di Pilih", MsgBoxStyle.Exclamation, Me.Text)
                CheckEdit19.Focus()
                fn_ValidateLaporanOperasi = False
                Exit Function
            End If

            Dim PemasanganImpan As Boolean = False

            If chkPemasanganImpan_Ya.Checked = True Then
                PemasanganImpan = True
            End If
            If chkPemasanganImpan_Tidak.Checked = True Then
                PemasanganImpan = True
            End If

            If PemasanganImpan = False Then
                MsgBox("Pemasangan Implan Belum di Pilih", MsgBoxStyle.Exclamation, Me.Text)
                chkPemasanganImpan_Ya.Focus()
                fn_ValidateLaporanOperasi = False
                Exit Function
            End If
            If TextEdit17.Text = "" Then
                MsgBox("Pendarahan Masih Kosong", MsgBoxStyle.Exclamation, Me.Text)
                TextEdit17.Focus()
                fn_ValidateLaporanOperasi = False
                Exit Function
            End If


            Dim jammulaianestesi As String = TimeEdit13.Time.ToString("HH:mm:ss", CultureInfo.InvariantCulture)
            Dim jammulaioperasi As String = TimeEdit14.Time.ToString("HH:mm:ss", CultureInfo.InvariantCulture)
            Dim jamselesaioperasi As String = TimeEdit15.Time.ToString("HH:mm:ss", CultureInfo.InvariantCulture)

            If jammulaianestesi = "00:00:00" Then
                MsgBox("Jam mulai anestesi belum di isi", MsgBoxStyle.Exclamation, Me.Text)
                fn_ValidateLaporanOperasi = False
                TimeEdit13.Focus()
                Exit Function
            End If
            If jammulaioperasi = "00:00:00" Then
                MsgBox("Jam mulai operasi belum di isi", MsgBoxStyle.Exclamation, Me.Text)
                fn_ValidateLaporanOperasi = False
                TimeEdit14.Focus()
                Exit Function
            End If
            If jamselesaioperasi = "00:00:00" Then
                MsgBox("Jam selesai operasi belum di isi", MsgBoxStyle.Exclamation, Me.Text)
                fn_ValidateLaporanOperasi = False
                TimeEdit15.Focus()
                Exit Function
            End If

            If txtLamaOperasi.Text = "00:00" Then
                MsgBox("Lama Operasi Berlangsung Masih Kosong", MsgBoxStyle.Exclamation, Me.Text)
                txtLamaOperasi.Focus()
                fn_ValidateLaporanOperasi = False
                Exit Function
            End If
            If txtLamaOperasi.Text = "" Then
                MsgBox("Lama Operasi Berlangsung Masih Kosong", MsgBoxStyle.Exclamation, Me.Text)
                txtLamaOperasi.Focus()
                fn_ValidateLaporanOperasi = False
                Exit Function
            End If

            'If SearchLookUpEdit1.Text = String.Empty Then
            '    MsgBox("Operator Belum Di Isi", MsgBoxStyle.Exclamation, Me.Text)
            '    SearchLookUpEdit1.Focus()
            '    fn_ValidateLaporanOperasi = False
            '    Exit Function
            'End If
            'If SearchLookUpEdit2.Text = String.Empty Then
            '    MsgBox("Asisten I Belum Di Isi", MsgBoxStyle.Exclamation, Me.Text)
            '    SearchLookUpEdit2.Focus()
            '    fn_ValidateLaporanOperasi = False
            '    Exit Function
            'End If
            'If SearchLookUpEdit4.Text = String.Empty Then
            '    MsgBox("Asisten II Belum Di Isi", MsgBoxStyle.Exclamation, Me.Text)
            '    SearchLookUpEdit4.Focus()
            '    fn_ValidateLaporanOperasi = False
            '    Exit Function
            'End If
            'If SearchLookUpEdit3.Text = String.Empty Then
            '    'MsgBox("Anestesi Belum Di Isi", MsgBoxStyle.Exclamation, Me.Text)
            '    SearchLookUpEdit3.Text = "Tidak Ada"
            '    'fn_ValidateLaporanOperasi = False
            '    'Exit Function
            'End If
            'If TextEdit5.Text = String.Empty Then
            '    MsgBox("Perawat Instrumen Belum Di Isi", MsgBoxStyle.Exclamation, Me.Text)
            '    TextEdit5.Focus()
            '    fn_ValidateLaporanOperasi = False
            '    Exit Function
            'End If
            'If txtDiagnosaPreOP.Text = String.Empty Then
            '    MsgBox("Diagnosa Pre Operatif Belum Di Isi", MsgBoxStyle.Exclamation, Me.Text)
            '    grdKDDIAGNOSAPREOP1.Focus()
            '    fn_ValidateLaporanOperasi = False
            '    Exit Function
            'End If
            'If txtDiagnosaPostOP.Text = String.Empty Then
            '    MsgBox("Diagnosa Post Operatif Belum Di Isi", MsgBoxStyle.Exclamation, Me.Text)
            '    grdKDDIAGNOSAPOSTOP1.Focus()
            '    fn_ValidateLaporanOperasi = False
            '    Exit Function
            'End If
            'If txtProsedur.Text = String.Empty Then
            '    MsgBox("Tindakan Operasi Belum Di Isi", MsgBoxStyle.Exclamation, Me.Text)
            '    grdKDPROSEDUR1.Focus()
            '    fn_ValidateLaporanOperasi = False
            '    Exit Function
            'End If
            'If TextEdit10.Text = String.Empty Then
            '    MsgBox("Jaringan yang di Eksisi/insisi Belum Di Isi", MsgBoxStyle.Exclamation, Me.Text)
            '    TextEdit10.Focus()
            '    fn_ValidateLaporanOperasi = False
            '    Exit Function
            'End If
            'If TextEdit11.Text = String.Empty Then
            '    MsgBox("Jenis Jaringan Belum Di Isi", MsgBoxStyle.Exclamation, Me.Text)
            '    TextEdit11.Focus()
            '    fn_ValidateLaporanOperasi = False
            '    Exit Function
            'End If
            'If TextEdit12.Text = String.Empty Then
            '    MsgBox("Jenis Pemeriksaan Belum Di Isi", MsgBoxStyle.Exclamation, Me.Text)
            '    TextEdit12.Focus()
            '    fn_ValidateLaporanOperasi = False
            '    Exit Function
            'End If
            'If MemoEdit4.Text = String.Empty Then
            '    MsgBox("Laporan Operasi Belum Di Isi", MsgBoxStyle.Exclamation, Me.Text)
            '    MemoEdit4.Focus()
            '    fn_ValidateLaporanOperasi = False
            '    Exit Function
            'End If
            'If TextEdit17.Text = String.Empty Then
            '    MsgBox("Pendarahan Belum Di Isi.", MsgBoxStyle.Exclamation, Me.Text)
            '    TextEdit17.Focus()
            '    fn_ValidateLaporanOperasi = False
            '    Exit Function
            'End If
            'If MemoEdit5.Text = String.Empty Then
            '    MsgBox("Komplikasi Belum Di Isi.", MsgBoxStyle.Exclamation, Me.Text)
            '    MemoEdit5.Focus()
            '    fn_ValidateLaporanOperasi = False
            '    Exit Function
            'End If
            'If MemoEdit6.Text = String.Empty Then
            '    MsgBox("Instruksi Pasca Bedah Belum Di Isi.", MsgBoxStyle.Exclamation, Me.Text)
            '    MemoEdit6.Focus()
            '    fn_ValidateLaporanOperasi = False
            '    Exit Function
            'End If
            'If TextEdit18.Text = String.Empty Then
            '    MsgBox("Pembuat Laporan Belum Di Isi.", MsgBoxStyle.Exclamation, Me.Text)
            '    TextEdit18.Focus()
            '    fn_ValidateLaporanOperasi = False
            '    Exit Function
            'End If
            'If TextEdit19.Text = String.Empty Then
            '    MsgBox("Dokter Ahli Bedah Belum Di Isi.", MsgBoxStyle.Exclamation, Me.Text)
            '    TextEdit19.Focus()
            '    fn_ValidateLaporanOperasi = False
            '    Exit Function
            'End If
        Catch oErr As Exception
            MsgBox("Validate Data Laporan Operasi: " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Function
    Private Function fn_SaveLaporanOperasi() As Boolean
        Try
            ' ***** HEADER *****

            Dim ds = oLaporanOperasi.GetStructureHeader
            With ds
                .KDLAPORANOPERASI = sKODELAPORANOPERASI
                .KDPENDAFTARAN = lblRegister.Text
                .KDCUSTOMER = lblNoRM.Text
                Try
                    .DATECREATED = oLaporanOperasi.GetDataKode(sKODELAPORANOPERASI).DATECREATED
                Catch ex As Exception
                    .DATECREATED = Now
                End Try
                .DATEUPDATED = Now
                .JENIS_OPERASI = cboJenisOperasi.EditValue
                .DATE = DateEdit1.DateTime

                .TextEdit1 = grdOperator_LO.EditValue
                .TextEdit2 = grdOperator_LO.Text
                .TextEdit19 = TextEdit19.EditValue
                .TextEdit8 = TextEdit19.Text
                .TextEdit3 = grdAnestesi_LO.EditValue
                .TextEdit4 = grdAnestesi_LO.Text

                .TextEdit5 = grdAsisten1_LO.Text
                .TextEdit6 = grdAsisten2_LO.Text
                .TextEdit7 = grdPerawatInstrumen.Text
                .TextEdit9 = grdPerawatAnestesiLO.Text
                .TextEdit10 = grdPembuatLaporan.Text
                .TextEdit11 = TextEdit11.Text
                .TextEdit12 = TextEdit12.Text
                .TextEdit13 = TimeEdit13.Time
                .TextEdit14 = TimeEdit14.Time
                .TextEdit15 = TimeEdit15.Time
                .TextEdit16 = txtLamaOperasi.Text
                .TextEdit17 = TextEdit17.Text
                .TextEdit18 = grdPembuatLaporan.EditValue
                .MemoEdit1 = MemoEdit1.Text
                .MemoEdit2 = MemoEdit2.Text
                .MemoEdit3 = MemoEdit3.Text
                .BARCODEALAT = ""
                .MemoEdit7 = ""
                .MemoEdit8 = ""

                'Dim listdiagnosa1 As New List(Of String)
                'For i As Integer = 0 To grvLODiagnosaPre.RowCount - 2
                '    listdiagnosa1.Add(grvLODiagnosaPre.GetRowCellValue(i, colNAMADIAGNOSA_LO) & grvLODiagnosaPre.GetRowCellValue(i, colKDDIAGNOSA_LO) & " (" & grvLODiagnosaPre.GetRowCellValue(i, colKATEGORI_LO) & ")")
                'Next

                Dim listdiagnosa1 As New List(Of String)
                For i As Integer = 0 To grvLODiagnosaPre.RowCount - 2
                    listdiagnosa1.Add(grvLODiagnosaPre.GetRowCellValue(i, colNAMADIAGNOSA_LO))
                Next

                .TXTDIAGNOSAPOSTOP2 = String.Join(vbCrLf, listdiagnosa1.ToArray)

                Dim listdiagnosa2 As New List(Of String)
                For i As Integer = 0 To grvLODiagnosa2.RowCount - 2
                    listdiagnosa2.Add(grvLODiagnosa2.GetRowCellValue(i, colNAMADIAGNOSA_LO2))
                Next

                .TXTDIAGNOSAPOSTOP3 = String.Join(vbCrLf, listdiagnosa2.ToArray)

                Dim listPrsedur As New List(Of String)
                For i As Integer = 0 To grvLOProsedur.RowCount - 2
                    listPrsedur.Add(grvLOProsedur.GetRowCellValue(i, colNAMAPROSEDUR_LO))
                Next

                .PROSEDUR2 = String.Join(vbCrLf, listPrsedur.ToArray)
                .PROSEDUR3 = ""
                .MemoEdit4 = MemoEdit4.Text
                .MemoEdit5 = MemoEdit5.Text
                .MemoEdit6 = MemoEdit6.Text
                .CheckEdit1 = CheckEdit1.Checked
                .CheckEdit2 = CheckEdit2.Checked
                .CheckEdit3 = CheckEdit3.Checked
                .CheckEdit4 = CheckEdit4.Checked
                .CheckEdit5 = CheckEdit5.Checked
                .CheckEdit6 = CheckEdit6.Checked
                .CheckEdit7 = CheckEdit7.Checked
                .CheckEdit8 = CheckEdit8.Checked
                .CheckEdit9 = CheckEdit9.Checked
                .CheckEdit10 = CheckEdit10.Checked
                .CheckEdit11 = CheckEdit11.Checked
                .CheckEdit12 = CheckEdit12.Checked
                .CheckEdit13 = CheckEdit13.Checked
                .CheckEdit14 = CheckEdit14.Checked
                .CheckEdit15 = CheckEdit15.Checked
                .CheckEdit16 = CheckEdit16.Checked
                .CheckEdit17 = CheckEdit17.Checked
                .CheckEdit18 = CheckEdit18.Checked
                .CheckEdit19 = CheckEdit19.Checked
                .CheckEdit20 = CheckEdit20.Checked
                .KDDOCTOR = sKDDOCTOR_DPJPUTAMA
                .BARCODEALAT = ""
                '.DOKTER_KODE = txtDokter_LO.EditValue
                '.DOKTER_NAMEDISPLAY = txtDokter_LO.Text

                .DOKTER_KODE = grdOperator_LO.EditValue
                .DOKTER_NAMEDISPLAY = grdOperator_LO.Text

                .SEQ = 0
                Try
                    .CETAK = oLaporanOperasi.GetDataKode(sKODELAPORANOPERASI).CETAK
                Catch ex As Exception
                    .CETAK = 0
                End Try
                .KDUSER = sUserID
                .KDUSER_SIGNATURE = ""
                .DATEDELETE = Now
                Try
                    .ISDELETE = oLaporanOperasi.GetDataKode(sKODELAPORANOPERASI).ISDELETE
                Catch ex As Exception
                    .ISDELETE = 0
                End Try
                .USERDELETE = ""
                .PEMASANGANIMPLAN_TIDAK = chkPemasanganImpan_Tidak.Checked
                .PEMASANGANIMPLAN_YA = chkPemasanganImpan_Ya.Checked
            End With

            'DIAGNOSA
            Dim arrDetailDiagnosa = oLaporanOperasi.GetStructureDetailDiagnosaList
            For i As Integer = 0 To grvLODiagnosaPre.RowCount - 2
                Dim dsDetail = oLaporanOperasi.GetStructureDetailDiagnosa
                With dsDetail
                    .SEQ = i
                    .KDLAPORANOPERASI = ds.KDLAPORANOPERASI
                    .KATEGORI = IIf(String.IsNullOrEmpty(grvLODiagnosaPre.GetRowCellValue(i, colKATEGORI_LO)), IIf(i = 0, "Primer", "Sekunder"), grvLODiagnosaPre.GetRowCellValue(i, colKATEGORI_LO))
                    .NAMADIAGNOSA = IIf(String.IsNullOrEmpty(grvLODiagnosaPre.GetRowCellValue(i, colNAMADIAGNOSA_LO)), "", grvLODiagnosaPre.GetRowCellValue(i, colNAMADIAGNOSA_LO))
                    .KDDIAGNOSA = IIf(String.IsNullOrEmpty(grvLODiagnosaPre.GetRowCellValue(i, colKDDIAGNOSA_LO)), "", grvLODiagnosaPre.GetRowCellValue(i, colKDDIAGNOSA_LO))
                End With
                arrDetailDiagnosa.Add(dsDetail)
            Next

            Dim arrDetailDiagnosa2 = oLaporanOperasi.GetStructureDetailDiagnosa2List
            For i As Integer = 0 To grvLODiagnosa2.RowCount - 2
                Dim dsDetail = oLaporanOperasi.GetStructureDetailDiagnosa2
                With dsDetail
                    .SEQ = i
                    .KDLAPORANOPERASI = ds.KDLAPORANOPERASI
                    .KATEGORI = IIf(String.IsNullOrEmpty(grvLODiagnosa2.GetRowCellValue(i, colKATEGORI_LO2)), IIf(i = 0, "Primer", "Sekunder"), grvLODiagnosa2.GetRowCellValue(i, colKATEGORI_LO2))
                    .NAMADIAGNOSA = IIf(String.IsNullOrEmpty(grvLODiagnosa2.GetRowCellValue(i, colNAMADIAGNOSA_LO2)), "", grvLODiagnosa2.GetRowCellValue(i, colNAMADIAGNOSA_LO2))
                    .KDDIAGNOSA = IIf(String.IsNullOrEmpty(grvLODiagnosa2.GetRowCellValue(i, colKDDIAGNOSA_LO2)), "", grvLODiagnosa2.GetRowCellValue(i, colKDDIAGNOSA_LO2))
                End With
                arrDetailDiagnosa2.Add(dsDetail)
            Next

            'PROSEDUR
            Dim arrDetailProsedur = oLaporanOperasi.GetStructureDetailProsedurList
            For i As Integer = 0 To grvLOProsedur.RowCount - 2
                Dim dsDetail = oLaporanOperasi.GetStructureDetailProsedur
                With dsDetail
                    .SEQ = i
                    .KDLAPORANOPERASI = ds.KDLAPORANOPERASI
                    .KATEGORI = ""
                    .NAMAPROSEDUR = IIf(String.IsNullOrEmpty(grvLOProsedur.GetRowCellValue(i, colNAMAPROSEDUR_LO)), "", grvLOProsedur.GetRowCellValue(i, colNAMAPROSEDUR_LO))
                    .KDPROSEDUR = IIf(String.IsNullOrEmpty(grvLOProsedur.GetRowCellValue(i, colKDPROSEDUR_LO)), "", grvLOProsedur.GetRowCellValue(i, colKDPROSEDUR_LO))
                End With
                arrDetailProsedur.Add(dsDetail)
            Next

            If sKODELAPORANOPERASI = "" Then
                Try
                    fn_SaveLaporanOperasi = oLaporanOperasi.InsertData(ds, arrDetailDiagnosa, arrDetailDiagnosa2, arrDetailProsedur)
                Catch ex As Exception
                    MsgBox("Simpan Data Laporan Operasi: " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            Else
                Try
                    fn_SaveLaporanOperasi = oLaporanOperasi.UpdateData(ds, arrDetailDiagnosa, arrDetailDiagnosa2, arrDetailProsedur)
                Catch ex As Exception
                    MsgBox("Simpan Data Laporan Operasi: " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            End If
        Catch oErr As Exception
            MsgBox("Simpan Data Laporan Operasi: " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            fn_SaveLaporanOperasi = False
        End Try
    End Function
    Private Sub fn_LoadDataTemplateLaporanOperasi(ByVal Kode As String)
        Try
            Dim oS_DIGITAL_OK_LAPORANOPERASITEMPLATE As New Transaksi.clsS_DIGITAL_OK_LAPORANOPERASITemplate

            ' ***** HEADER *****
            Dim ds = oS_DIGITAL_OK_LAPORANOPERASITEMPLATE.GetData(Kode)
            With ds
                'DateEdit1.DateTime = .DATE
                grdOperator_LO.EditValue = .TextEdit1
                grdAnestesi_LO.EditValue = .TextEdit3
                TextEdit19.EditValue = .TextEdit19

                grdAsisten1_LO.EditValue = .TextEdit5
                grdAsisten2_LO.Text = .TextEdit6
                grdPerawatInstrumen.Text = .TextEdit7
                grdPerawatAnestesiLO.Text = .TextEdit9

                'TextEdit10.Text = .TextEdit10
                TextEdit11.Text = .TextEdit11
                TextEdit12.Text = .TextEdit12
                TimeEdit13.Time = .TextEdit13
                TimeEdit14.Time = .TextEdit14
                TimeEdit15.Time = .TextEdit15
                txtLamaOperasi.Text = .TextEdit16
                TextEdit17.Text = .TextEdit17
                'TextEdit18.Text = .TextEdit18

                MemoEdit4.Text = .MemoEdit4
                MemoEdit5.Text = .MemoEdit5
                MemoEdit6.Text = .MemoEdit6
                CheckEdit1.Checked = .CheckEdit1
                CheckEdit2.Checked = .CheckEdit2
                CheckEdit3.Checked = .CheckEdit3
                CheckEdit4.Checked = .CheckEdit4
                CheckEdit5.Checked = .CheckEdit5
                CheckEdit6.Checked = .CheckEdit6
                CheckEdit7.Checked = .CheckEdit7
                CheckEdit8.Checked = .CheckEdit8
                CheckEdit9.Checked = .CheckEdit9
                CheckEdit10.Checked = .CheckEdit10
                CheckEdit11.Checked = .CheckEdit11
                CheckEdit12.Checked = .CheckEdit12
                CheckEdit13.Checked = .CheckEdit13
                CheckEdit14.Checked = .CheckEdit14
                CheckEdit15.Checked = .CheckEdit15
                CheckEdit16.Checked = .CheckEdit16
                CheckEdit17.Checked = .CheckEdit17
                CheckEdit18.Checked = .CheckEdit18
                CheckEdit19.Checked = .CheckEdit19
                CheckEdit20.Checked = .CheckEdit20
                cboJenisOperasi.EditValue = .JENIS_OPERASI
                'txtDokter_LO.EditValue = .DOKTER_KODE

                MemoEdit1.Text = .MemoEdit1
                MemoEdit2.Text = .MemoEdit2
                MemoEdit3.Text = .MemoEdit3

                If .MemoEdit7 = "YA" Then
                    chkPemasanganImpan_Tidak.Checked = True
                Else
                    chkPemasanganImpan_Tidak.Checked = False
                End If
                If .MemoEdit8 = "YA" Then
                    chkPemasanganImpan_Ya.Checked = True
                Else
                    chkPemasanganImpan_Ya.Checked = False
                End If

                For Each xloop In oS_DIGITAL_OK_LAPORANOPERASITEMPLATE.GetDataDetailDiagnosa(ds.KDJUDUL)
                    grvLODiagnosaPre.Focus()
                    grvLODiagnosaPre.AddNewRow()
                    grvLODiagnosaPre.SetFocusedRowCellValue(colKATEGORI_LO, xloop.KATEGORI)
                    grvLODiagnosaPre.SetFocusedRowCellValue(colKDDIAGNOSA_LO, xloop.KDDIAGNOSA)
                    grvLODiagnosaPre.SetFocusedRowCellValue(colNAMADIAGNOSA_LO, xloop.NAMADIAGNOSA)
                    grvLODiagnosaPre.UpdateCurrentRow()
                Next

                For Each xloop In oS_DIGITAL_OK_LAPORANOPERASITEMPLATE.GetDataDetailDiagnosa2(ds.KDJUDUL)
                    grvLODiagnosa2.Focus()
                    grvLODiagnosa2.AddNewRow()
                    grvLODiagnosa2.SetFocusedRowCellValue(colKATEGORI_LO2, xloop.KATEGORI)
                    grvLODiagnosa2.SetFocusedRowCellValue(colKDDIAGNOSA_LO2, xloop.KDDIAGNOSA)
                    grvLODiagnosa2.SetFocusedRowCellValue(colNAMADIAGNOSA_LO2, xloop.NAMADIAGNOSA)
                    grvLODiagnosa2.UpdateCurrentRow()
                Next

                For Each xloop In oS_DIGITAL_OK_LAPORANOPERASITEMPLATE.GetDataDetailProsedur(ds.KDJUDUL)
                    grvLOProsedur.Focus()
                    grvLOProsedur.AddNewRow()
                    grvLOProsedur.SetFocusedRowCellValue(colKDPROSEDUR_LO, xloop.KDPROSEDUR)
                    grvLOProsedur.SetFocusedRowCellValue(colNAMAPROSEDUR_LO, xloop.NAMAPROSEDUR)
                    grvLOProsedur.UpdateCurrentRow()
                Next
            End With
        Catch oErr As Exception
            MsgBox("Load List Data Template Laporan Operasi: " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Function fn_SaveTemplateLaporanOperasi() As Boolean
        Try
            Dim oS_DIGITAL_OK_LAPORANOPERASITEMPLATE As New Transaksi.clsS_DIGITAL_OK_LAPORANOPERASITemplate

            ' ***** HEADER *****

            Dim ds = oS_DIGITAL_OK_LAPORANOPERASITEMPLATE.GetStructureHeader
            With ds
                .KDJUDUL = sKODEASESMENCOPY
                Try
                    .DATECREATED = oS_DIGITAL_OK_LAPORANOPERASITEMPLATE.GetData(sKODEASESMENCOPY).DATECREATED
                Catch ex As Exception
                    .DATECREATED = Now
                End Try
                .DATEUPDATED = Now
                .JENIS_OPERASI = cboJenisOperasi.EditValue
                '.DATE = DateEdit1.DateTime

                .TextEdit1 = grdOperator_LO.EditValue
                .TextEdit2 = grdOperator_LO.Text
                .TextEdit19 = TextEdit19.EditValue
                .TextEdit8 = TextEdit19.Text
                .TextEdit3 = grdAnestesi_LO.EditValue
                .TextEdit4 = grdAnestesi_LO.Text

                .TextEdit5 = grdAsisten1_LO.Text
                .TextEdit6 = grdAsisten2_LO.Text
                .TextEdit7 = grdPerawatInstrumen.Text
                .TextEdit9 = grdPerawatAnestesiLO.Text
                .TextEdit10 = ""
                .TextEdit11 = TextEdit11.Text
                .TextEdit12 = TextEdit12.Text
                .TextEdit13 = TimeEdit13.Time
                .TextEdit14 = TimeEdit14.Time
                .TextEdit15 = TimeEdit15.Time
                .TextEdit16 = txtLamaOperasi.Text
                .TextEdit17 = TextEdit17.Text
                .TextEdit18 = sUserID
                .MemoEdit1 = MemoEdit1.Text
                .MemoEdit2 = MemoEdit2.Text
                .MemoEdit3 = MemoEdit3.Text
                .BARCODEALAT = ""
                .MemoEdit7 = IIf(chkPemasanganImpan_Tidak.Checked = True, "YA", "TIDAK")
                .MemoEdit8 = IIf(chkPemasanganImpan_Ya.Checked = True, "YA", "TIDAK")

                Dim listdiagnosa1 As New List(Of String)
                For i As Integer = 0 To grvLODiagnosaPre.RowCount - 2
                    listdiagnosa1.Add(grvLODiagnosaPre.GetRowCellValue(i, colNAMADIAGNOSA_LO) & grvLODiagnosaPre.GetRowCellValue(i, colKDDIAGNOSA_LO) & " (" & grvLODiagnosaPre.GetRowCellValue(i, colKATEGORI_LO) & ")")
                Next

                .TXTDIAGNOSAPOSTOP2 = String.Join(vbCrLf, listdiagnosa1.ToArray)

                Dim listdiagnosa2 As New List(Of String)
                For i As Integer = 0 To grvLODiagnosa2.RowCount - 2
                    listdiagnosa2.Add(grvLODiagnosa2.GetRowCellValue(i, colNAMADIAGNOSA_LO2) & grvLODiagnosa2.GetRowCellValue(i, colKDDIAGNOSA_LO2) & " (" & grvLODiagnosa2.GetRowCellValue(i, colKATEGORI_LO2) & ")")
                Next

                .TXTDIAGNOSAPOSTOP3 = String.Join(vbCrLf, listdiagnosa2.ToArray)

                Dim listPrsedur As New List(Of String)
                For i As Integer = 0 To grvLOProsedur.RowCount - 2
                    listPrsedur.Add(grvLOProsedur.GetRowCellValue(i, colNAMAPROSEDUR_LO) & grvLOProsedur.GetRowCellValue(i, colKDPROSEDUR_LO))
                Next

                .PROSEDUR2 = String.Join(vbCrLf, listPrsedur.ToArray)
                .PROSEDUR3 = ""
                .MemoEdit4 = MemoEdit4.Text
                .MemoEdit5 = MemoEdit5.Text
                .MemoEdit6 = MemoEdit6.Text
                .CheckEdit1 = CheckEdit1.Checked
                .CheckEdit2 = CheckEdit2.Checked
                .CheckEdit3 = CheckEdit3.Checked
                .CheckEdit4 = CheckEdit4.Checked
                .CheckEdit5 = CheckEdit5.Checked
                .CheckEdit6 = CheckEdit6.Checked
                .CheckEdit7 = CheckEdit7.Checked
                .CheckEdit8 = CheckEdit8.Checked
                .CheckEdit9 = CheckEdit9.Checked
                .CheckEdit10 = CheckEdit10.Checked
                .CheckEdit11 = CheckEdit11.Checked
                .CheckEdit12 = CheckEdit12.Checked
                .CheckEdit13 = CheckEdit13.Checked
                .CheckEdit14 = CheckEdit14.Checked
                .CheckEdit15 = CheckEdit15.Checked
                .CheckEdit16 = CheckEdit16.Checked
                .CheckEdit17 = CheckEdit17.Checked
                .CheckEdit18 = CheckEdit18.Checked
                .CheckEdit19 = CheckEdit19.Checked
                .CheckEdit20 = CheckEdit20.Checked
                .KDDOCTOR = sKDDOCTOR_DPJPUTAMA
                .BARCODEALAT = ""
                .DOKTER_KODE = grdOperator_LO.EditValue
                .DOKTER_NAMEDISPLAY = grdOperator_LO.Text
                '.SEQ = 0
                Try
                    .CETAK = oS_DIGITAL_OK_LAPORANOPERASITEMPLATE.GetData(sKODEASESMENCOPY).CETAK
                Catch ex As Exception
                    .CETAK = 0
                End Try
                .KDUSER = sUserID
                .KDUSER_SIGNATURE = ""
                .DATEDELETE = Now
            End With

            'DIAGNOSA
            Dim arrDetailDiagnosa = oS_DIGITAL_OK_LAPORANOPERASITEMPLATE.GetStructureDetailDiagnosaList
            For i As Integer = 0 To grvLODiagnosaPre.RowCount - 2
                Dim dsDetail = oS_DIGITAL_OK_LAPORANOPERASITEMPLATE.GetStructureDetailDiagnosa
                With dsDetail
                    .SEQ = i
                    .KDJUDUL = ds.KDJUDUL
                    .KATEGORI = IIf(String.IsNullOrEmpty(grvLODiagnosaPre.GetRowCellValue(i, colKATEGORI_LO)), IIf(i = 0, "Primer", "Sekunder"), grvLODiagnosaPre.GetRowCellValue(i, colKATEGORI_LO))
                    .NAMADIAGNOSA = IIf(String.IsNullOrEmpty(grvLODiagnosaPre.GetRowCellValue(i, colNAMADIAGNOSA_LO)), "", grvLODiagnosaPre.GetRowCellValue(i, colNAMADIAGNOSA_LO))
                    .KDDIAGNOSA = IIf(String.IsNullOrEmpty(grvLODiagnosaPre.GetRowCellValue(i, colKDDIAGNOSA_LO)), "", grvLODiagnosaPre.GetRowCellValue(i, colKDDIAGNOSA_LO))
                End With
                arrDetailDiagnosa.Add(dsDetail)
            Next

            Dim arrDetailDiagnosa2 = oS_DIGITAL_OK_LAPORANOPERASITEMPLATE.GetStructureDetailDiagnosa2List
            For i As Integer = 0 To grvLODiagnosa2.RowCount - 2
                Dim dsDetail = oS_DIGITAL_OK_LAPORANOPERASITEMPLATE.GetStructureDetailDiagnosa2
                With dsDetail
                    .SEQ = i
                    .KDJUDUL = ds.KDJUDUL
                    .KATEGORI = IIf(String.IsNullOrEmpty(grvLODiagnosa2.GetRowCellValue(i, colKATEGORI_LO2)), IIf(i = 0, "Primer", "Sekunder"), grvLODiagnosa2.GetRowCellValue(i, colKATEGORI_LO2))
                    .NAMADIAGNOSA = IIf(String.IsNullOrEmpty(grvLODiagnosa2.GetRowCellValue(i, colNAMADIAGNOSA_LO2)), "", grvLODiagnosa2.GetRowCellValue(i, colNAMADIAGNOSA_LO2))
                    .KDDIAGNOSA = IIf(String.IsNullOrEmpty(grvLODiagnosa2.GetRowCellValue(i, colKDDIAGNOSA_LO2)), "", grvLODiagnosa2.GetRowCellValue(i, colKDDIAGNOSA_LO2))
                End With
                arrDetailDiagnosa2.Add(dsDetail)
            Next

            'PROSEDUR
            Dim arrDetailProsedur = oS_DIGITAL_OK_LAPORANOPERASITEMPLATE.GetStructureDetailProsedurList
            For i As Integer = 0 To grvLOProsedur.RowCount - 2
                Dim dsDetail = oS_DIGITAL_OK_LAPORANOPERASITEMPLATE.GetStructureDetailProsedur
                With dsDetail
                    .SEQ = i
                    .KDJUDUL = ds.KDJUDUL
                    .KATEGORI = ""
                    .NAMAPROSEDUR = grvLOProsedur.GetRowCellValue(i, colNAMAPROSEDUR_LO)
                    .KDPROSEDUR = IIf(String.IsNullOrEmpty(grvLOProsedur.GetRowCellValue(i, colKDPROSEDUR_LO)), "", grvLOProsedur.GetRowCellValue(i, colKDPROSEDUR_LO))
                End With
                arrDetailProsedur.Add(dsDetail)
            Next

            Dim dsCek = oS_DIGITAL_OK_LAPORANOPERASITEMPLATE.GetData(sKODEASESMENCOPY)

            If dsCek Is Nothing Then
                Try
                    sKODEASESMENCOPY = oS_DIGITAL_OK_LAPORANOPERASITEMPLATE.InsertData(ds, arrDetailDiagnosa, arrDetailDiagnosa2, arrDetailProsedur)

                    If sKODEASESMENCOPY = "" Then
                        fn_SaveTemplateLaporanOperasi = False
                    Else
                        fn_SaveTemplateLaporanOperasi = True
                    End If
                Catch ex As Exception
                    MsgBox("Simpan Data Template Laporan Operasi Template: " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            Else
                Try
                    fn_SaveTemplateLaporanOperasi = oS_DIGITAL_OK_LAPORANOPERASITEMPLATE.UpdateData(ds, arrDetailDiagnosa, arrDetailDiagnosa2, arrDetailProsedur)
                Catch ex As Exception
                    MsgBox("Simpan Data Template Laporan Operasi Template: " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            End If
        Catch oErr As Exception
            MsgBox("Simpan Data Template Laporan Operasi: " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            fn_SaveTemplateLaporanOperasi = False
        End Try
    End Function
#End Region
#Region "Command Button"
    Private Sub grvFormulirMPP_Viewer_FocusedRowChanged(ByVal sender As System.Object, ByVal e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs) Handles grvFormulirMPP_Viewer.FocusedRowChanged
        Try
            If grvFormulirMPP_Viewer.GetFocusedRowCellValue("KODE") Is Nothing Then
                Exit Sub
            End If

            PdfViewerFormulirMPP.CloseDocument()

            SplashScreenManager.ShowForm(Me, GetType(frmLoadingPanel), True, True, False)

            SplashScreenManager.Default.SetWaitFormCaption("Processing Formulir MPP.....")


            Dim FolderSimpan = "C:/FORMULIRMPP/"

            If Not Directory.Exists(FolderSimpan) Then
                Directory.CreateDirectory(FolderSimpan)
            Else
                DeleteDirectory(FolderSimpan)
                Directory.CreateDirectory(FolderSimpan)
            End If

            sUSIADIASESMENIGD = sTanggalLahir.ToString("dd-MM-yyyy") & " (" & HitungUmur(sTanggalLahir, deTANGGALMASUK.DateTime) & ")"

            If grvFormulirMPP_Viewer.GetFocusedRowCellValue("KATEGORI") = "MPPA" Then
                Dim oDigitalFormulirMPP As New Digital.clsDigital_MPPA

                Dim ds = oDigitalFormulirMPP.GetData(grvFormulirMPP_Viewer.GetFocusedRowCellValue("KODE"))

                If ds IsNot Nothing Then
                    Dim rpt As New xtraDigital_MPPA

                    rpt.ShowPrintMarginsWarning = False
                    rpt.Watermark.Text = sWATERMARK

                    rpt.bindingSource.DataSource = ds

                    Dim waktu As String = Now.ToString("ddMMyyyyHHmm")
                    rpt.ExportToPdf(FolderSimpan & waktu & ds.KDMPPA & ".pdf")
                    PdfViewerFormulirMPP.LoadDocument(FolderSimpan & waktu & ds.KDMPPA & ".pdf")
                End If
            Else
                Dim oDigitalFormulirMPP As New Digital.clsDigital_MPPB

                Dim ds = oDigitalFormulirMPP.GetData(grvFormulirMPP_Viewer.GetFocusedRowCellValue("KODE"))

                If ds IsNot Nothing Then
                    Dim rpt As New xtraDigital_MPPB

                    rpt.ShowPrintMarginsWarning = False
                    rpt.Watermark.Text = sWATERMARK

                    rpt.bindingSource.DataSource = ds

                    Dim waktu As String = Now.ToString("ddMMyyyyHHmm")
                    rpt.ExportToPdf(FolderSimpan & waktu & ds.KDMPPB & ".pdf")
                    PdfViewerFormulirMPP.LoadDocument(FolderSimpan & waktu & ds.KDMPPB & ".pdf")
                End If
            End If

            SplashScreenManager.CloseForm(False)
        Catch oErr As Exception
            SplashScreenManager.CloseForm(False)
            MsgBox("Load Pengantar: " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub grvPengantar_FocusedRowChanged(ByVal sender As System.Object, ByVal e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs) Handles grvPengantar.FocusedRowChanged
        Try
            If grvPengantar.GetFocusedRowCellValue("KODE") Is Nothing Then
                Exit Sub
            End If

            PdfViewerPengantar.CloseDocument()

            SplashScreenManager.ShowForm(Me, GetType(frmLoadingPanel), True, True, False)

            SplashScreenManager.Default.SetWaitFormCaption("Processing data Pengantar.....")


            Dim FolderSimpan = "C:/LAPORANPENGANTAR/"

            If Not Directory.Exists(FolderSimpan) Then
                Directory.CreateDirectory(FolderSimpan)
            Else
                DeleteDirectory(FolderSimpan)
                Directory.CreateDirectory(FolderSimpan)
            End If

            Dim oPengantar As New Order.clsOrderPenunjang

            Dim ds = oPengantar.GetDataDetailLisAll(grvPengantar.GetFocusedRowCellValue("KODE"))

            If ds IsNot Nothing Then
                Dim rpt As New xtraPengantar

                rpt.ShowPrintMarginsWarning = False
                rpt.Watermark.Text = sWATERMARK

                rpt.bindingSource.DataSource = ds

                Dim waktu As String = Now.ToString("ddMMyyyyHHmm")
                rpt.ExportToPdf(FolderSimpan & waktu & ds.FirstOrDefault.KODE & ".pdf")
                PdfViewerPengantar.LoadDocument(FolderSimpan & waktu & ds.FirstOrDefault.KODE & ".pdf")
            End If

            SplashScreenManager.CloseForm(False)
        Catch oErr As Exception
            SplashScreenManager.CloseForm(False)
            MsgBox("Load Pengantar: " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
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

            If grvLaporanOperasi.GetFocusedRowCellValue("KETERANGAN") = "LAPORAN OPERASI" Then
                Dim ds = oLaporanOperasi.GetDataKode(grvLaporanOperasi.GetFocusedRowCellValue("KODE"))
                If ds IsNot Nothing Then
                    NAMA = lblNamaPasien.Text
                    JENISKELAMIN = lblJenisKelamin.Text
                    TANGGALLAHIR = lblTanggalLahir.Text
                    sKDUSER_TTD = ds.KDUSER
                    USIA = GetUmurPasien(ds.DATE, sTanggalLahir)

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
                    NAMA = lblNamaPasien.Text
                    JENISKELAMIN = lblJenisKelamin.Text
                    TANGGALLAHIR = lblTanggalLahir.Text
                    sKDUSER_TTD = ds.KDUSER
                    USIA = GetUmurPasien(ds.DATE, sTanggalLahir)

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
    Private Sub btnListLaporanOperasi_Click(sender As Object, e As EventArgs) Handles btnListLaporanOperasi.Click
        IListLaporanOperasi.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        lFormLaporanOperasi.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never

        fn_LoadLaporanOperasi(lblNoRM.Text)
    End Sub
    Private Sub picAddLaporanOperasi_Click(sender As Object, e As EventArgs) Handles picAddLaporanOperasi.Click
        lblKodeLaporanOperasi.ResetText()
        IListLaporanOperasi.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        lFormLaporanOperasi.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always

        isLoadLaporanOperasi = False
        lblKodeLaporanOperasi.ResetText()
        fn_ChangeFormStateLaporanOperasi()
    End Sub
    Private Sub picUpdateLaporanOperasi_Click(sender As Object, e As EventArgs) Handles picUpdateLaporanOperasi.Click
        If grvListLaporanOperasi.GetFocusedRowCellValue("KDLAPORANOPERASI") Is Nothing Then
            Exit Sub
        End If

        IListLaporanOperasi.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        lFormLaporanOperasi.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always

        isLoadLaporanOperasi = False
        lblKodeLaporanOperasi.Text = grvListLaporanOperasi.GetFocusedRowCellValue("KDLAPORANOPERASI")
        fn_ChangeFormStateLaporanOperasi()
    End Sub
    Private Sub picDeleteLaporanOperasi_Click(sender As Object, e As EventArgs) Handles picDeleteLaporanOperasi.Click
        If grvListLaporanOperasi.GetFocusedRowCellValue("KDLAPORANOPERASI") Is Nothing Then
            Exit Sub
        End If
        If oLaporanOperasi.UpdateDeleteLaporanOperasi(grvListLaporanOperasi.GetFocusedRowCellValue("KDLAPORANOPERASI"), sUserID) = True Then
            MsgBox("Berhasil Delete!", MsgBoxStyle.Exclamation, Me.Text)
            fn_LoadLaporanOperasi(lblNoRM.Text)
        End If
    End Sub
    Private Sub picRefreshLaporanOperasi_Click(sender As Object, e As EventArgs) Handles picRefreshLaporanOperasi.Click
        fn_LoadLaporanOperasi(lblNoRM.Text)
    End Sub
    Private Sub btnSimpanLaporanOperasi_Click() Handles btnSimpanLaporanOperasi.Click
        If fn_ValidateLaporanOperasi() = False Then Exit Sub
        'If MsgBox("Save " & txtNoRegister.Text.Trim.ToUpper & "?", MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, Me.Text) = MsgBoxResult.No Then Exit Sub
        If fn_SaveLaporanOperasi() = False Then
            MsgBox("Save gagal! Tolong kontak admin anda!", MsgBoxStyle.Exclamation, Me.Text)
        Else
            tabbedControlGroup1.SelectedTabPageIndex = 14
            tabbedControlGroup1_SelectedPageChanged()

            IListLaporanOperasi.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            lFormLaporanOperasi.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never

            fn_LoadLaporanOperasi(lblNoRM.Text)
        End If
    End Sub
#End Region
#Region "Lookup / Event"
    Private Sub txtLamaOperasi_PreviewKeyDown(sender As Object, e As PreviewKeyDownEventArgs) Handles txtLamaOperasi.PreviewKeyDown
        If e.KeyCode = Keys.Tab Then
            MemoEdit3.Focus()
        End If
    End Sub
    Private Sub DeleteToolStripLODiagnosa_Click(sender As Object, e As EventArgs) Handles DeleteToolStripLODiagnosa.Click
        grvLODiagnosaPre.DeleteSelectedRows()
    End Sub
    Private Sub DeleteToolStripLODiagnosa_2_Click(sender As Object, e As EventArgs) Handles DeleteToolStripLODiagnosa_2.Click
        grvLODiagnosa2.DeleteSelectedRows()
    End Sub
    Private Sub DeleteToolStripLOProsedur_Click(sender As Object, e As EventArgs) Handles DeleteToolStripLOProsedur.Click
        grvLOProsedur.DeleteSelectedRows()
    End Sub
    Private Sub TimeEdit15_EditValueChanging(sender As Object, e As EventArgs) Handles TimeEdit15.EditValueChanged
        If isLoadLaporanOperasi = True Then
            ' Ambil nilai waktu dari TimeEdit
            Dim waktuAwal As DateTime = TimeEdit14.EditValue
            Dim waktuAkhir As DateTime = TimeEdit15.EditValue

            If waktuAkhir >= waktuAwal Then
                Dim selisih As TimeSpan = waktuAkhir - waktuAwal
                txtLamaOperasi.Text = selisih.Hours & " Jam " & selisih.Minutes & " Menit"
            Else
                txtLamaOperasi.Text = "00 Jam 00 Menit"
            End If
        End If
    End Sub
    Private Sub txtCariDiagnosaLO1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtCariDiagnosaLO1.KeyPress
        If Asc(e.KeyChar) = 13 Then
            If txtCariDiagnosaLO1.Text.Length < 3 Then
                MsgBox("Minimal 3 Huruf !!!", MsgBoxStyle.Exclamation, Me.Text)
                Exit Sub
            End If
            Try
                Dim jsonDecode = JObject.Parse(oBrigging.fn_Pencariandiagnosa(txtCariDiagnosaLO1.Text))
                Dim sDataDuplicate As String = String.Empty
                Dim smessage As String = String.Empty

                sDataDuplicate = jsonDecode("metadata")("code").ToString
                smessage = jsonDecode("metadata")("message").ToString

                If sDataDuplicate = "200" Then
                    Dim table As DataTable

                    table = New DataTable("M_DIAGNOSA")
                    table.Columns.Add("nama")
                    table.Columns.Add("kode")

                    For Each item In jsonDecode("response")("data")
                        table.Rows.Add(New String() {item(0), item(1)})
                    Next

                    grdCariDiganosaLO1.Properties.DataSource = table
                    grdCariDiganosaLO1.Properties.ValueMember = "kode"
                    grdCariDiganosaLO1.Properties.DisplayMember = "nama"

                    grdCariDiganosaLO1.ShowPopup()

                    txtCariDiagnosaLO1.ResetText()
                Else
                    MsgBox("Pencarian Diagnosa" & vbCrLf & sDataDuplicate & "-" & smessage, MsgBoxStyle.Information, Me.Text)
                End If
            Catch oErr As Exception
                MsgBox("Pencarian Diagnosa: " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            End Try
        End If
    End Sub
    Private Sub grdCariDiganosaLO1_EditValueChanged(sender As Object, e As EventArgs) Handles grdCariDiganosaLO1.EditValueChanged
        If isLoadLaporanOperasi = False Then Exit Sub

        If grdCariDiganosaLO1.Text <> "" Then
            Dim sCek As Integer = 0

            For i As Integer = 0 To grvLODiagnosaPre.RowCount - 2
                sCek += 1
            Next

            grvLODiagnosaPre.Focus()
            grvLODiagnosaPre.AddNewRow()
            grvLODiagnosaPre.SetFocusedRowCellValue(colKATEGORI_LO, IIf(sCek = 0, "Primer", "Sekunder"))
            grvLODiagnosaPre.SetFocusedRowCellValue(colKDDIAGNOSA_LO, grdCariDiganosaLO1.EditValue)
            grvLODiagnosaPre.SetFocusedRowCellValue(colNAMADIAGNOSA_LO, grdCariDiganosaLO1.Text)
            grvLODiagnosaPre.UpdateCurrentRow()

            grvLODiagnosaPre.Focus()
        End If
    End Sub
    Private Sub txtCariDiagnosaLO2_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtCariDiagnosaLO2.KeyPress
        If Asc(e.KeyChar) = 13 Then
            If txtCariDiagnosaLO2.Text.Length < 3 Then
                MsgBox("Minimal 3 Huruf !!!", MsgBoxStyle.Exclamation, Me.Text)
                Exit Sub
            End If
            Try
                Dim jsonDecode = JObject.Parse(oBrigging.fn_Pencariandiagnosa(txtCariDiagnosaLO2.Text))
                Dim sDataDuplicate As String = String.Empty
                Dim smessage As String = String.Empty

                sDataDuplicate = jsonDecode("metadata")("code").ToString
                smessage = jsonDecode("metadata")("message").ToString

                If sDataDuplicate = "200" Then
                    Dim table As DataTable

                    table = New DataTable("M_DIAGNOSA")
                    table.Columns.Add("nama")
                    table.Columns.Add("kode")

                    For Each item In jsonDecode("response")("data")
                        table.Rows.Add(New String() {item(0), item(1)})
                    Next

                    grdCariDiganosaLO2.Properties.DataSource = table
                    grdCariDiganosaLO2.Properties.ValueMember = "kode"
                    grdCariDiganosaLO2.Properties.DisplayMember = "nama"

                    grdCariDiganosaLO2.ShowPopup()

                    txtCariDiagnosaLO2.ResetText()
                Else
                    MsgBox("Pencarian Diagnosa" & vbCrLf & sDataDuplicate & "-" & smessage, MsgBoxStyle.Information, Me.Text)
                End If
            Catch oErr As Exception
                MsgBox("Pencarian Diagnosa: " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            End Try
        End If
    End Sub
    Private Sub grdCariDiganosaLO2_EditValueChanged(sender As Object, e As EventArgs) Handles grdCariDiganosaLO2.EditValueChanged
        If isLoadLaporanOperasi = False Then Exit Sub

        If grdCariDiganosaLO2.Text <> "" Then
            Dim sCek As Integer = 0

            For i As Integer = 0 To grvLODiagnosa2.RowCount - 2
                sCek += 1
            Next

            grvLODiagnosa2.Focus()
            grvLODiagnosa2.AddNewRow()
            grvLODiagnosa2.SetFocusedRowCellValue(colKATEGORI_LO2, IIf(sCek = 0, "Primer", "Sekunder"))
            grvLODiagnosa2.SetFocusedRowCellValue(colKDDIAGNOSA_LO2, grdCariDiganosaLO2.EditValue)
            grvLODiagnosa2.SetFocusedRowCellValue(colNAMADIAGNOSA_LO2, grdCariDiganosaLO2.Text)
            grvLODiagnosa2.UpdateCurrentRow()

            grvLODiagnosa2.Focus()
        End If
    End Sub
    Private Sub txtCariProsedur_LO_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtCariProsedur_LO.KeyPress
        If Asc(e.KeyChar) = 13 Then
            Try
                If txtCariProsedur_LO.Text.Length < 3 Then
                    MsgBox("Minimal 3 Huruf !!!", MsgBoxStyle.Exclamation, Me.Text)
                    Exit Sub
                End If

                Try
                    Dim jsonDecode = JObject.Parse(oBrigging.fn_PencarianProsedur("VCLAIM", txtCariProsedur_LO.Text))
                    Dim sDataDuplicate As String = String.Empty
                    Dim smessage As String = String.Empty

                    sDataDuplicate = jsonDecode("metadata")("code").ToString
                    smessage = jsonDecode("metadata")("message").ToString

                    If sDataDuplicate = "200" Then
                        Dim table As DataTable

                        table = New DataTable("M_PROSEDUR")
                        table.Columns.Add("nama")
                        table.Columns.Add("kode")

                        For Each item In jsonDecode("response")("data")
                            table.Rows.Add(New String() {item(0), item(1)})
                        Next

                        grdCariProsedur_LO.Properties.DataSource = table
                        grdCariProsedur_LO.Properties.ValueMember = "kode"
                        grdCariProsedur_LO.Properties.DisplayMember = "nama"

                        grdCariProsedur_LO.ShowPopup()

                        txtCariProsedur_LO.ResetText()
                    Else
                        MsgBox("Pencarian Prosedur" & vbCrLf & sDataDuplicate & "-" & smessage, MsgBoxStyle.Information, Me.Text)
                    End If
                Catch oErr As Exception
                    MsgBox("Pencarian Prosedur: " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            Catch oErr As Exception
                MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            End Try
        End If
    End Sub
    Private Sub grdCariProsedur_LO_EditValueChanged(sender As Object, e As EventArgs) Handles grdCariProsedur_LO.EditValueChanged
        If isLoadLaporanOperasi = False Then Exit Sub

        If grdCariProsedur_LO.Text <> "" Then
            grvLOProsedur.Focus()
            grvLOProsedur.AddNewRow()
            grvLOProsedur.SetFocusedRowCellValue(colKDPROSEDUR_LO, grdCariProsedur_LO.EditValue)
            grvLOProsedur.SetFocusedRowCellValue(colNAMAPROSEDUR_LO, grdCariProsedur_LO.Text)
            grvLOProsedur.UpdateCurrentRow()

            txtCariProsedur_LO.Focus()
        End If
    End Sub
    Private Sub fn_LoadOperator()
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
            SQL &= "A.KDDOCTOR "
            SQL &= ",NAME_DISPLAY = (SELECT CASE A.FRONT_TITLE WHEN '' THEN '' ELSE A.FRONT_TITLE + ' ' END) + A.NAME_DISPLAY + A.BACK_TITLE "
            SQL &= "FROM "
            SQL &= "M_DOCTOR A "
            SQL &= "WHERE "
            SQL &= "A.ISACTIVE = 1 "
            SQL &= "AND A.CATEGORY = 1 "
            SQL &= "ORDER BY A.NAME_DISPLAY "

            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "DOKTER")

            grdOperator_LO.Properties.DataSource = ds.Tables("DOKTER")
            grdOperator_LO.Properties.ValueMember = "KDDOCTOR"
            grdOperator_LO.Properties.DisplayMember = "NAME_DISPLAY"

            TextEdit19.Properties.DataSource = ds.Tables("DOKTER")
            TextEdit19.Properties.ValueMember = "KDDOCTOR"
            TextEdit19.Properties.DisplayMember = "NAME_DISPLAY"

            grdAnestesi_LO.Properties.DataSource = ds.Tables("DOKTER")
            grdAnestesi_LO.Properties.ValueMember = "KDDOCTOR"
            grdAnestesi_LO.Properties.DisplayMember = "NAME_DISPLAY"

            grdPembuatLaporan.Properties.DataSource = ds.Tables("DOKTER")
            grdPembuatLaporan.Properties.ValueMember = "KDDOCTOR"
            grdPembuatLaporan.Properties.DisplayMember = "NAME_DISPLAY"

            grdDPJPUtama.Properties.DataSource = ds.Tables("DOKTER")
            grdDPJPUtama.Properties.ValueMember = "KDDOCTOR"
            grdDPJPUtama.Properties.DisplayMember = "NAME_DISPLAY"

            If oConn.State = ConnectionState.Open Then
                oConn.Close()
            End If
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadPERAWAT()
        Dim oTemplate As New Reference.clsUnit
        Try
            Dim ds = oTemplate.GetData.Where(Function(x) x.ISACTIVE = True).ToList()

            grdAsisten1_LO.Properties.DataSource = ds
            grdAsisten1_LO.Properties.ValueMember = "MEMO"
            grdAsisten1_LO.Properties.DisplayMember = "MEMO"

            grdAsisten2_LO.Properties.DataSource = ds
            grdAsisten2_LO.Properties.ValueMember = "MEMO"
            grdAsisten2_LO.Properties.DisplayMember = "MEMO"

            grdPerawatInstrumen.Properties.DataSource = ds
            grdPerawatInstrumen.Properties.ValueMember = "MEMO"
            grdPerawatInstrumen.Properties.DisplayMember = "MEMO"

            grdPerawatAnestesiLO.Properties.DataSource = ds
            grdPerawatAnestesiLO.Properties.ValueMember = "MEMO"
            grdPerawatAnestesiLO.Properties.DisplayMember = "MEMO"

            grdUSERHADNOVER.Properties.DataSource = ds
            grdUSERHADNOVER.Properties.ValueMember = "KDUNIT"
            grdUSERHADNOVER.Properties.DisplayMember = "MEMO"
        Catch oErr As Exception
            MsgBox("Load Perawat : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_JENISOPERASI()
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
            SQL &= "M_LAPORAN_OPERASI A "
            SQL &= "WHERE "
            SQL &= "A.ISACTIVE = 1 "

            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "JENISLAPORAN")

            cboJenisOperasi.Properties.DataSource = ds.Tables("JENISLAPORAN")
            cboJenisOperasi.Properties.ValueMember = "NAMALAPORAN"
            cboJenisOperasi.Properties.DisplayMember = "NAMALAPORAN"

            If oConn.State = ConnectionState.Open Then
                oConn.Close()
            End If

        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
#End Region
#Region "Handlle"
    Private Sub chKamarOperasi_Click(sender As Object, e As EventArgs) Handles CheckEdit1.Click, CheckEdit2.Click, CheckEdit3.Click, CheckEdit4.Click, CheckEdit5.Click, CheckEdit6.Click
        CheckEdit1.Checked = False
        CheckEdit2.Checked = False
        CheckEdit3.Checked = False
        CheckEdit4.Checked = False
        CheckEdit5.Checked = False
        CheckEdit6.Checked = False
    End Sub
    Private Sub chJenisAnestesi_Click(sender As Object, e As EventArgs) Handles CheckEdit7.Click, CheckEdit8.Click, CheckEdit9.Click, CheckEdit10.Click
        CheckEdit7.Checked = False
        CheckEdit8.Checked = False
        CheckEdit9.Checked = False
        CheckEdit10.Checked = False
    End Sub
    Private Sub chKlasifikasi_Click(sender As Object, e As EventArgs) Handles CheckEdit11.Click, CheckEdit12.Click
        CheckEdit11.Checked = False
        CheckEdit12.Checked = False
    End Sub
    Private Sub chJenisOperasi_Click(sender As Object, e As EventArgs) Handles CheckEdit13.Click, CheckEdit14.Click, CheckEdit15.Click, CheckEdit16.Click
        CheckEdit13.Checked = False
        CheckEdit14.Checked = False
        CheckEdit15.Checked = False
        CheckEdit16.Checked = False
    End Sub
    Private Sub chPemeriksaanPA_Click(sender As Object, e As EventArgs) Handles CheckEdit17.Click, CheckEdit18.Click
        CheckEdit17.Checked = False
        CheckEdit18.Checked = False
    End Sub
    Private Sub chPemeriksaanCairan_Click(sender As Object, e As EventArgs) Handles CheckEdit19.Click, CheckEdit20.Click
        CheckEdit19.Checked = False
        CheckEdit20.Checked = False
    End Sub
    Private Sub chPemasanganImplan_Click(sender As Object, e As EventArgs) Handles chkPemasanganImpan_Tidak.Click, chkPemasanganImpan_Ya.Click
        chkPemasanganImpan_Tidak.Checked = False
        chkPemasanganImpan_Ya.Checked = False
    End Sub
    Private Sub btnLoadDataLaporanOperasi_Click(sender As Object, e As EventArgs) Handles btnLoadData.Click
        Dim frmListTemplate As New frmListTemplate
        Try
            sKODEASESMENCOPY = String.Empty

            frmListTemplate.fn_LoadKategori(4, "")
            frmListTemplate.ShowDialog(Me)

            If sKODEASESMENCOPY <> "" Then
                fn_LoadDataTemplateLaporanOperasi(sKODEASESMENCOPY)
            End If
        Catch oErr As Exception
            MsgBox("Load Data Template" & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub btnSaveLaporanOperasi_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        If sKODEASESMENCOPY = "" Then
            MsgBox("Silahkan Load Data Terlebih Dahulu", MsgBoxStyle.Exclamation, Me.Text)
            Exit Sub
        End If

        If MsgBox("Save " & sKODEASESMENCOPY & "?", MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, Me.Text) = MsgBoxResult.No Then Exit Sub
        If fn_SaveTemplateLaporanOperasi() = False Then
            MsgBox("Save gagal! Tolong kontak admin anda!", MsgBoxStyle.Exclamation, Me.Text)
        Else
            MsgBox("Save " & sKODEASESMENCOPY & " success!", MsgBoxStyle.Information, Me.Text)
        End If
    End Sub
    Private Sub btnSaveAsLaporanOperasi_Click(sender As Object, e As EventArgs) Handles btnSaveAs.Click
        sRemarks = String.Empty
        frmJudulTemplate.ShowDialog(Me)

        If sRemarks = "" Then
            MsgBox("Judul Template Masih Kosong", MsgBoxStyle.Exclamation, Me.Text)
        Else
            sKODEASESMENCOPY = sRemarks

            Dim oS_DIGITAL_OK_LAPORANOPERASITEMPLATE As New Transaksi.clsS_DIGITAL_OK_LAPORANOPERASITemplate

            Dim ds = oS_DIGITAL_OK_LAPORANOPERASITEMPLATE.GetData(sKODEASESMENCOPY)
            If ds IsNot Nothing Then
                MsgBox("Judul Sudah Ada", MsgBoxStyle.Exclamation, Me.Text)
                Exit Sub
            End If

            If MsgBox("Save As " & sKODEASESMENCOPY & "?", MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, Me.Text) = MsgBoxResult.No Then Exit Sub
            If fn_SaveTemplateLaporanOperasi() = False Then
                sKODEASESMENCOPY = ""
                MsgBox("Save As gagal! Tolong kontak admin anda!", MsgBoxStyle.Exclamation, Me.Text)
            Else
                MsgBox("Save As " & sKODEASESMENCOPY & " success!", MsgBoxStyle.Information, Me.Text)
            End If
        End If

        sRemarks = String.Empty

    End Sub
#End Region
#End Region
#Region "Laporan Tindakan"
#Region "Function"
    Private Sub fn_LoadLaporanTindakan(ByVal RekamMedis As String)
        Try
            If RekamMedis = "" Then Exit Sub

            grvLaporanTindakan_List.OptionsSelection.MultiSelect = True
            grvLaporanTindakan_List.SelectAll()
            grvLaporanTindakan_List.DeleteSelectedRows()
            grvLaporanTindakan_List.OptionsSelection.MultiSelect = False

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
            SQL &= "A.KDLAPORANTINDAKAN "
            SQL &= ",TANGGAL = A.DATE "
            SQL &= ",A.JENIS_OPERASI "
            SQL &= ",A.KDPENDAFTARAN "
            SQL &= ",JENISTINDAKAN = A.PROSEDUR2 "
            SQL &= ",[USER] = A.KDUSER "
            SQL &= "FROM "
            SQL &= "S_DIGITAL_OK_LAPORANTINDAKAN A "
            SQL &= "WHERE "
            SQL &= "A.KDCUSTOMER = '" & RekamMedis & "' "
            SQL &= "AND A.ISDELETE = '0' "

            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "S_DIGITAL_OK_LAPORANTINDAKAN")

            grdLaporanTindakan_List.DataSource = ds.Tables("S_DIGITAL_OK_LAPORANTINDAKAN")
            grdLaporanTindakan_List.ForceInitialize()

            If oConn.State = ConnectionState.Open Then
                oConn.Close()
            End If

            grvLaporanTindakan_List.Columns("KDLAPORANTINDAKAN").Visible = False

            For iLoop As Integer = 0 To grvResume.Columns.Count - 1
                If grvLaporanTindakan_List.Columns(iLoop).ColumnType.Name = "Decimal" Then
                    grvLaporanTindakan_List.Columns(iLoop).DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                    grvLaporanTindakan_List.Columns(iLoop).DisplayFormat.FormatString = "{0:n2}"
                    grvLaporanTindakan_List.Columns(iLoop).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
                ElseIf grvLaporanTindakan_List.Columns(iLoop).ColumnType.Name = "DateTime" Then
                    grvLaporanTindakan_List.Columns(iLoop).DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
                    grvLaporanTindakan_List.Columns(iLoop).DisplayFormat.FormatString = "{0:dd/MM/yyyy}"
                End If
            Next

            grvLaporanTindakan_List.Columns("JENIS_OPERASI").Caption = "JENIS"

        Catch oErr As Exception
            MsgBox("LOAD LAPORAN OPERASI : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    'Private Sub fn_ChangeFormStateLaporanTindakan()
    '    fn_JENISTINDAKAN()
    '    fn_LoadOperatorTindkan()
    '    fn_LoadPERAWATTINDAKAN()

    '    Dim ds = oDigital_LaporanTindakan.GetData(lblKodeLT.Text)

    '    If ds Is Nothing Then
    '        fn_EmptyMeLaporanTindakan()
    '    Else
    '        fn_LoadDataLaporanTindakan(ds.KDLAPORANTINDAKAN)
    '    End If
    'End Sub
    'Private Sub fn_EmptyMeLaporanTindakan()

    'End Sub
    'Private Sub fn_LoadDataLaporanTindakan(ByVal Kode As String)
    '    'Try
    '    '    ' ***** HEADER *****
    '    '    Dim ds = oLaporanOperasi.GetDataKode(sKODELAPORANTINDAKAN)
    '    '    With ds
    '    '        DateEdit1.DateTime = .DATE
    '    '        grdOperator_LO.EditValue = .TextEdit1
    '    '        grdAnestesi_LO.EditValue = .TextEdit3
    '    '        TextEdit19.EditValue = .TextEdit19

    '    '        grdAsisten1_LO.EditValue = .TextEdit5
    '    '        grdAsisten2_LO.Text = .TextEdit6
    '    '        grdPerawatInstrumen.Text = .TextEdit7
    '    '        grdPerawatAnestesiLO.Text = .TextEdit9

    '    '        TextEdit10.Text = .TextEdit10
    '    '        TextEdit11.Text = .TextEdit11
    '    '        TextEdit12.Text = .TextEdit12
    '    '        TimeEdit13.Time = .TextEdit13
    '    '        TimeEdit14.Time = .TextEdit14
    '    '        TimeEdit15.Time = .TextEdit15
    '    '        txtLamaOperasi.Text = .TextEdit16
    '    '        TextEdit17.Text = .TextEdit17
    '    '        TextEdit18.Text = .TextEdit18

    '    '        MemoEdit4.Text = .MemoEdit4
    '    '        MemoEdit5.Text = .MemoEdit5
    '    '        MemoEdit6.Text = .MemoEdit6
    '    '        CheckEdit1.Checked = .CheckEdit1
    '    '        CheckEdit2.Checked = .CheckEdit2
    '    '        CheckEdit3.Checked = .CheckEdit3
    '    '        CheckEdit4.Checked = .CheckEdit4
    '    '        CheckEdit5.Checked = .CheckEdit5
    '    '        CheckEdit6.Checked = .CheckEdit6
    '    '        CheckEdit7.Checked = .CheckEdit7
    '    '        CheckEdit8.Checked = .CheckEdit8
    '    '        CheckEdit9.Checked = .CheckEdit9
    '    '        CheckEdit10.Checked = .CheckEdit10
    '    '        CheckEdit11.Checked = .CheckEdit11
    '    '        CheckEdit12.Checked = .CheckEdit12
    '    '        CheckEdit13.Checked = .CheckEdit13
    '    '        CheckEdit14.Checked = .CheckEdit14
    '    '        CheckEdit15.Checked = .CheckEdit15
    '    '        CheckEdit16.Checked = .CheckEdit16
    '    '        CheckEdit17.Checked = .CheckEdit17
    '    '        CheckEdit18.Checked = .CheckEdit18
    '    '        CheckEdit19.Checked = .CheckEdit19
    '    '        CheckEdit20.Checked = .CheckEdit20
    '    '        cboJenisOperasi.EditValue = .JENIS_OPERASI
    '    '        txtDokter_LO.EditValue = .DOKTER_KODE

    '    '        MemoEdit1.Text = .MemoEdit1
    '    '        MemoEdit2.Text = .MemoEdit2
    '    '        MemoEdit3.Text = .MemoEdit3

    '    '        BindingSourceLODiagnosa.DataSource = oLaporanOperasi.GetDataDetailDiagnosa(.KDLAPORANOPERASI).OrderBy(Function(x) x.SEQ).ToList()
    '    '        grdLODiagnosaPre.DataSource = BindingSourceLODiagnosa

    '    '        BindingSourceLODiagnosa_2.DataSource = oLaporanOperasi.GetDataDetailDiagnosa2(.KDLAPORANOPERASI).OrderBy(Function(x) x.SEQ).ToList()
    '    '        grdLODiagnosa2.DataSource = BindingSourceLODiagnosa_2

    '    '        BindingSourceLOProsedur.DataSource = oLaporanOperasi.GetDataDetailProsedur(.KDLAPORANOPERASI).OrderBy(Function(x) x.SEQ).ToList()
    '    '        grdLOProsedur.DataSource = BindingSourceLOProsedur
    '    '    End With
    '    'Catch oErr As Exception
    '    '    MsgBox("Load List Data Laporan Tindakan: " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
    '    'End Try
    'End Sub
    'Private Function fn_ValidateLaporanTindakan() As Boolean
    '    Try
    '        fn_ValidateLaporanTindakan = True
    '        If lblRegister.Text = String.Empty Then
    '            MsgBox("Dibutuhkan Register", MsgBoxStyle.Exclamation, Me.Text)
    '            fn_ValidateLaporanTindakan = False
    '            Exit Function
    '        End If
    '    Catch oErr As Exception
    '        MsgBox("Validate Data Laporan Tindakan: " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
    '    End Try
    'End Function
    'Private Function fn_SaveLaporanTindakan() As Boolean
    '    'Try
    '    '    ' ***** HEADER *****

    '    '    Dim ds = oLaporanOperasi.GetStructureHeader
    '    '    With ds
    '    '        .KDLAPORANOPERASI = sKODELAPORANTINDAKAN
    '    '        .KDPENDAFTARAN = lblRegister.Text
    '    '        .KDCUSTOMER = lblNoRM.Text
    '    '        Try
    '    '            .DATECREATED = oLaporanOperasi.GetDataKode(sKODELAPORANTINDAKAN).DATECREATED
    '    '        Catch ex As Exception
    '    '            .DATECREATED = Now
    '    '        End Try
    '    '        .DATEUPDATED = Now
    '    '        .JENIS_OPERASI = cboJenisOperasi.EditValue
    '    '        .DATE = DateEdit1.DateTime

    '    '        .TextEdit1 = grdOperator_LO.EditValue
    '    '        .TextEdit2 = grdOperator_LO.Text
    '    '        .TextEdit19 = TextEdit19.EditValue
    '    '        .TextEdit8 = TextEdit19.Text
    '    '        .TextEdit3 = grdAnestesi_LO.EditValue
    '    '        .TextEdit4 = grdAnestesi_LO.Text

    '    '        .TextEdit5 = grdAsisten1_LO.Text
    '    '        .TextEdit6 = grdAsisten2_LO.Text
    '    '        .TextEdit7 = grdPerawatInstrumen.Text
    '    '        .TextEdit9 = grdPerawatAnestesiLO.Text
    '    '        .TextEdit10 = TextEdit10.Text
    '    '        .TextEdit11 = TextEdit11.Text
    '    '        .TextEdit12 = TextEdit12.Text
    '    '        .TextEdit13 = TimeEdit13.Time
    '    '        .TextEdit14 = TimeEdit14.Time
    '    '        .TextEdit15 = TimeEdit15.Time
    '    '        .TextEdit16 = txtLamaOperasi.Text
    '    '        .TextEdit17 = TextEdit17.Text
    '    '        .TextEdit18 = TextEdit18.Text
    '    '        .MemoEdit1 = MemoEdit1.Text
    '    '        .MemoEdit2 = MemoEdit2.Text
    '    '        .MemoEdit3 = MemoEdit3.Text
    '    '        .BARCODEALAT = ""
    '    '        .MemoEdit7 = ""
    '    '        .MemoEdit8 = ""

    '    '        Dim listdiagnosa1 As New List(Of String)
    '    '        For i As Integer = 0 To grvLODiagnosaPre.RowCount - 2
    '    '            listdiagnosa1.Add(i + 1 & ". " & grvLODiagnosaPre.GetRowCellValue(i, colNAMADIAGNOSA_LO) & grvLODiagnosaPre.GetRowCellValue(i, colKDDIAGNOSA_LO) & " (" & grvLODiagnosaPre.GetRowCellValue(i, colKATEGORI_LO) & ")")
    '    '        Next

    '    '        .TXTDIAGNOSAPOSTOP2 = String.Join(vbCrLf, listdiagnosa1.ToArray)

    '    '        Dim listdiagnosa2 As New List(Of String)
    '    '        For i As Integer = 0 To grvLODiagnosa2.RowCount - 2
    '    '            listdiagnosa2.Add(i + 1 & ". " & grvLODiagnosa2.GetRowCellValue(i, colNAMADIAGNOSA_LO2) & grvLODiagnosa2.GetRowCellValue(i, colKDDIAGNOSA_LO2) & " (" & grvLODiagnosa2.GetRowCellValue(i, colKATEGORI_LO2) & ")")
    '    '        Next

    '    '        .TXTDIAGNOSAPOSTOP3 = String.Join(vbCrLf, listdiagnosa2.ToArray)

    '    '        Dim listPrsedur As New List(Of String)
    '    '        For i As Integer = 0 To grvLOProsedur.RowCount - 2
    '    '            listPrsedur.Add(i + 1 & ". " & grvLOProsedur.GetRowCellValue(i, colNAMAPROSEDUR_LO) & grvLOProsedur.GetRowCellValue(i, colKDPROSEDUR_LO))
    '    '        Next

    '    '        .PROSEDUR2 = String.Join(vbCrLf, listPrsedur.ToArray)
    '    '        .PROSEDUR3 = ""
    '    '        .MemoEdit4 = MemoEdit4.Text
    '    '        .MemoEdit5 = MemoEdit5.Text
    '    '        .MemoEdit6 = MemoEdit6.Text
    '    '        .CheckEdit1 = CheckEdit1.Checked
    '    '        .CheckEdit2 = CheckEdit2.Checked
    '    '        .CheckEdit3 = CheckEdit3.Checked
    '    '        .CheckEdit4 = CheckEdit4.Checked
    '    '        .CheckEdit5 = CheckEdit5.Checked
    '    '        .CheckEdit6 = CheckEdit6.Checked
    '    '        .CheckEdit7 = CheckEdit7.Checked
    '    '        .CheckEdit8 = CheckEdit8.Checked
    '    '        .CheckEdit9 = CheckEdit9.Checked
    '    '        .CheckEdit10 = CheckEdit10.Checked
    '    '        .CheckEdit11 = CheckEdit11.Checked
    '    '        .CheckEdit12 = CheckEdit12.Checked
    '    '        .CheckEdit13 = CheckEdit13.Checked
    '    '        .CheckEdit14 = CheckEdit14.Checked
    '    '        .CheckEdit15 = CheckEdit15.Checked
    '    '        .CheckEdit16 = CheckEdit16.Checked
    '    '        .CheckEdit17 = CheckEdit17.Checked
    '    '        .CheckEdit18 = CheckEdit18.Checked
    '    '        .CheckEdit19 = CheckEdit19.Checked
    '    '        .CheckEdit20 = CheckEdit20.Checked
    '    '        .KDDOCTOR = sKDDOCTOR_DPJPUTAMA
    '    '        .BARCODEALAT = ""
    '    '        .DOKTER_KODE = txtDokter_LO.EditValue
    '    '        .DOKTER_NAMEDISPLAY = txtDokter_LO.Text
    '    '        .SEQ = 0
    '    '        Try
    '    '            .CETAK = oLaporanOperasi.GetDataKode(sKODELAPORANTINDAKAN).CETAK
    '    '        Catch ex As Exception
    '    '            .CETAK = 0
    '    '        End Try
    '    '        .KDUSER = sUserID
    '    '        .KDUSER_SIGNATURE = ""
    '    '        .DATEDELETE = Now
    '    '    End With

    '    '    'DIAGNOSA
    '    '    Dim arrDetailDiagnosa = oLaporanOperasi.GetStructureDetailDiagnosaList
    '    '    For i As Integer = 0 To grvLODiagnosaPre.RowCount - 2
    '    '        Dim dsDetail = oLaporanOperasi.GetStructureDetailDiagnosa
    '    '        With dsDetail
    '    '            .SEQ = i
    '    '            .KDLAPORANOPERASI = ds.KDLAPORANOPERASI
    '    '            .KATEGORI = grvLODiagnosaPre.GetRowCellValue(i, colKATEGORI_LO)
    '    '            .NAMADIAGNOSA = grvLODiagnosaPre.GetRowCellValue(i, colNAMADIAGNOSA_LO)
    '    '            .KDDIAGNOSA = IIf(String.IsNullOrEmpty(grvLODiagnosaPre.GetRowCellValue(i, colKDDIAGNOSA_LO)), "", grvLODiagnosaPre.GetRowCellValue(i, colKDDIAGNOSA_LO))
    '    '        End With
    '    '        arrDetailDiagnosa.Add(dsDetail)
    '    '    Next

    '    '    Dim arrDetailDiagnosa2 = oLaporanOperasi.GetStructureDetailDiagnosa2List
    '    '    For i As Integer = 0 To grvLODiagnosa2.RowCount - 2
    '    '        Dim dsDetail = oLaporanOperasi.GetStructureDetailDiagnosa2
    '    '        With dsDetail
    '    '            .SEQ = i
    '    '            .KDLAPORANOPERASI = ds.KDLAPORANOPERASI
    '    '            .KATEGORI = grvLODiagnosa2.GetRowCellValue(i, colKATEGORI_LO2)
    '    '            .NAMADIAGNOSA = grvLODiagnosa2.GetRowCellValue(i, colNAMADIAGNOSA_LO2)
    '    '            .KDDIAGNOSA = IIf(String.IsNullOrEmpty(grvLODiagnosa2.GetRowCellValue(i, colKDDIAGNOSA_LO2)), "", grvLODiagnosa2.GetRowCellValue(i, colKDDIAGNOSA_LO2))
    '    '        End With
    '    '        arrDetailDiagnosa2.Add(dsDetail)
    '    '    Next

    '    '    'PROSEDUR
    '    '    Dim arrDetailProsedur = oLaporanOperasi.GetStructureDetailProsedurList
    '    '    For i As Integer = 0 To grvLOProsedur.RowCount - 2
    '    '        Dim dsDetail = oLaporanOperasi.GetStructureDetailProsedur
    '    '        With dsDetail
    '    '            .SEQ = i
    '    '            .KDLAPORANOPERASI = ds.KDLAPORANOPERASI
    '    '            .KATEGORI = ""
    '    '            .NAMAPROSEDUR = grvLOProsedur.GetRowCellValue(i, colNAMAPROSEDUR_LO)
    '    '            .KDPROSEDUR = IIf(String.IsNullOrEmpty(grvLOProsedur.GetRowCellValue(i, colKDPROSEDUR_LO)), "", grvLOProsedur.GetRowCellValue(i, colKDPROSEDUR_LO))
    '    '        End With
    '    '        arrDetailProsedur.Add(dsDetail)
    '    '    Next

    '    '    If sKODELAPORANTINDAKAN = "" Then
    '    '        Try
    '    '            fn_SaveLaporanTindakan = oLaporanOperasi.InsertData(ds, arrDetailDiagnosa, arrDetailDiagnosa2, arrDetailProsedur)
    '    '        Catch ex As Exception
    '    '            MsgBox("Simpan Data Laporan Operasi: " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
    '    '        End Try
    '    '    Else
    '    '        Try
    '    '            fn_SaveLaporanTindakan = oLaporanOperasi.UpdateData(ds, arrDetailDiagnosa, arrDetailDiagnosa2, arrDetailProsedur)
    '    '        Catch ex As Exception
    '    '            MsgBox("Simpan Data Laporan Tindakan: " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
    '    '        End Try
    '    '    End If
    '    'Catch oErr As Exception
    '    '    MsgBox("Simpan Data Laporan Tindakan: " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
    '    '    fn_SaveLaporanTindakan = False
    '    'End Try
    'End Function
    'Private Sub fn_LoadDataTemplateLaporanTindakan(ByVal Kode As String)
    '    Try
    '        Dim oS_DIGITAL_OK_LAPORANOPERASITEMPLATE As New Transaksi.clsS_DIGITAL_OK_LAPORANOPERASITemplate

    '        ' ***** HEADER *****
    '        Dim ds = oS_DIGITAL_OK_LAPORANOPERASITEMPLATE.GetData(Kode)
    '        With ds
    '            'DateEdit1.DateTime = .DATE
    '            grdOperator_LO.EditValue = .TextEdit1
    '            grdAnestesi_LO.EditValue = .TextEdit3
    '            TextEdit19.EditValue = .TextEdit19

    '            grdAsisten1_LO.EditValue = .TextEdit5
    '            grdAsisten2_LO.Text = .TextEdit6
    '            grdPerawatInstrumen.Text = .TextEdit7
    '            grdPerawatAnestesiLO.Text = .TextEdit9

    '            'TextEdit10.Text = .TextEdit10
    '            TextEdit11.Text = .TextEdit11
    '            TextEdit12.Text = .TextEdit12
    '            TimeEdit13.Time = .TextEdit13
    '            TimeEdit14.Time = .TextEdit14
    '            TimeEdit15.Time = .TextEdit15
    '            txtLamaOperasi.Text = .TextEdit16
    '            TextEdit17.Text = .TextEdit17
    '            'TextEdit18.Text = .TextEdit18

    '            MemoEdit4.Text = .MemoEdit4
    '            MemoEdit5.Text = .MemoEdit5
    '            MemoEdit6.Text = .MemoEdit6
    '            CheckEdit1.Checked = .CheckEdit1
    '            CheckEdit2.Checked = .CheckEdit2
    '            CheckEdit3.Checked = .CheckEdit3
    '            CheckEdit4.Checked = .CheckEdit4
    '            CheckEdit5.Checked = .CheckEdit5
    '            CheckEdit6.Checked = .CheckEdit6
    '            CheckEdit7.Checked = .CheckEdit7
    '            CheckEdit8.Checked = .CheckEdit8
    '            CheckEdit9.Checked = .CheckEdit9
    '            CheckEdit10.Checked = .CheckEdit10
    '            CheckEdit11.Checked = .CheckEdit11
    '            CheckEdit12.Checked = .CheckEdit12
    '            CheckEdit13.Checked = .CheckEdit13
    '            CheckEdit14.Checked = .CheckEdit14
    '            CheckEdit15.Checked = .CheckEdit15
    '            CheckEdit16.Checked = .CheckEdit16
    '            CheckEdit17.Checked = .CheckEdit17
    '            CheckEdit18.Checked = .CheckEdit18
    '            CheckEdit19.Checked = .CheckEdit19
    '            CheckEdit20.Checked = .CheckEdit20
    '            cboJenisOperasi.EditValue = .JENIS_OPERASI
    '            'txtDokter_LO.EditValue = .DOKTER_KODE

    '            MemoEdit1.Text = .MemoEdit1
    '            MemoEdit2.Text = .MemoEdit2
    '            MemoEdit3.Text = .MemoEdit3


    '            For Each xloop In oS_DIGITAL_OK_LAPORANOPERASITEMPLATE.GetDataDetailDiagnosa(ds.KDJUDUL)
    '                grvLODiagnosaPre.Focus()
    '                grvLODiagnosaPre.AddNewRow()
    '                grvLODiagnosaPre.SetFocusedRowCellValue(colKATEGORI_LO, xloop.KATEGORI)
    '                grvLODiagnosaPre.SetFocusedRowCellValue(colKDDIAGNOSA_LO, xloop.KDDIAGNOSA)
    '                grvLODiagnosaPre.SetFocusedRowCellValue(colNAMADIAGNOSA_LO, xloop.NAMADIAGNOSA)
    '                grvLODiagnosaPre.UpdateCurrentRow()
    '            Next

    '            For Each xloop In oS_DIGITAL_OK_LAPORANOPERASITEMPLATE.GetDataDetailDiagnosa2(ds.KDJUDUL)
    '                grvLODiagnosa2.Focus()
    '                grvLODiagnosa2.AddNewRow()
    '                grvLODiagnosa2.SetFocusedRowCellValue(colKATEGORI_LO2, xloop.KATEGORI)
    '                grvLODiagnosa2.SetFocusedRowCellValue(colKDDIAGNOSA_LO2, xloop.KDDIAGNOSA)
    '                grvLODiagnosa2.SetFocusedRowCellValue(colNAMADIAGNOSA_LO2, xloop.NAMADIAGNOSA)
    '                grvLODiagnosa2.UpdateCurrentRow()
    '            Next

    '            For Each xloop In oS_DIGITAL_OK_LAPORANOPERASITEMPLATE.GetDataDetailProsedur(ds.KDJUDUL)
    '                grvLOProsedur.Focus()
    '                grvLOProsedur.AddNewRow()
    '                grvLOProsedur.SetFocusedRowCellValue(colKDPROSEDUR_LO, xloop.KDPROSEDUR)
    '                grvLOProsedur.SetFocusedRowCellValue(colNAMAPROSEDUR_LO, xloop.NAMAPROSEDUR)
    '                grvLOProsedur.UpdateCurrentRow()
    '            Next
    '        End With
    '    Catch oErr As Exception
    '        MsgBox("Load List Data Template Laporan Tindakan: " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
    '    End Try
    'End Sub
    'Private Function fn_SaveTemplateLaporanTindakan() As Boolean
    '    Try
    '        Dim oS_DIGITAL_OK_LAPORANOPERASITEMPLATE As New Transaksi.clsS_DIGITAL_OK_LAPORANOPERASITemplate

    '        ' ***** HEADER *****

    '        Dim ds = oS_DIGITAL_OK_LAPORANOPERASITEMPLATE.GetStructureHeader
    '        With ds
    '            .KDJUDUL = sKODEASESMENCOPY
    '            Try
    '                .DATECREATED = oS_DIGITAL_OK_LAPORANOPERASITEMPLATE.GetData(sKODEASESMENCOPY).DATECREATED
    '            Catch ex As Exception
    '                .DATECREATED = Now
    '            End Try
    '            .DATEUPDATED = Now
    '            .JENIS_OPERASI = cboJenisOperasi.EditValue
    '            '.DATE = DateEdit1.DateTime

    '            .TextEdit1 = grdOperator_LO.EditValue
    '            .TextEdit2 = grdOperator_LO.Text
    '            .TextEdit19 = TextEdit19.EditValue
    '            .TextEdit8 = TextEdit19.Text
    '            .TextEdit3 = grdAnestesi_LO.EditValue
    '            .TextEdit4 = grdAnestesi_LO.Text

    '            .TextEdit5 = grdAsisten1_LO.Text
    '            .TextEdit6 = grdAsisten2_LO.Text
    '            .TextEdit7 = grdPerawatInstrumen.Text
    '            .TextEdit9 = grdPerawatAnestesiLO.Text
    '            .TextEdit10 = ""
    '            .TextEdit11 = TextEdit11.Text
    '            .TextEdit12 = TextEdit12.Text
    '            .TextEdit13 = TimeEdit13.Time
    '            .TextEdit14 = TimeEdit14.Time
    '            .TextEdit15 = TimeEdit15.Time
    '            .TextEdit16 = txtLamaOperasi.Text
    '            .TextEdit17 = TextEdit17.Text
    '            .TextEdit18 = sUserID
    '            .MemoEdit1 = MemoEdit1.Text
    '            .MemoEdit2 = MemoEdit2.Text
    '            .MemoEdit3 = MemoEdit3.Text
    '            .BARCODEALAT = ""
    '            .MemoEdit7 = ""
    '            .MemoEdit8 = ""

    '            Dim listdiagnosa1 As New List(Of String)
    '            For i As Integer = 0 To grvLODiagnosaPre.RowCount - 2
    '                listdiagnosa1.Add(i + 1 & ". " & grvLODiagnosaPre.GetRowCellValue(i, colNAMADIAGNOSA_LO) & grvLODiagnosaPre.GetRowCellValue(i, colKDDIAGNOSA_LO) & " (" & grvLODiagnosaPre.GetRowCellValue(i, colKATEGORI_LO) & ")")
    '            Next

    '            .TXTDIAGNOSAPOSTOP2 = String.Join(vbCrLf, listdiagnosa1.ToArray)

    '            Dim listdiagnosa2 As New List(Of String)
    '            For i As Integer = 0 To grvLODiagnosa2.RowCount - 2
    '                listdiagnosa2.Add(i + 1 & ". " & grvLODiagnosa2.GetRowCellValue(i, colNAMADIAGNOSA_LO2) & grvLODiagnosa2.GetRowCellValue(i, colKDDIAGNOSA_LO2) & " (" & grvLODiagnosa2.GetRowCellValue(i, colKATEGORI_LO2) & ")")
    '            Next

    '            .TXTDIAGNOSAPOSTOP3 = String.Join(vbCrLf, listdiagnosa2.ToArray)

    '            Dim listPrsedur As New List(Of String)
    '            For i As Integer = 0 To grvLOProsedur.RowCount - 2
    '                listPrsedur.Add(i + 1 & ". " & grvLOProsedur.GetRowCellValue(i, colNAMAPROSEDUR_LO) & grvLOProsedur.GetRowCellValue(i, colKDPROSEDUR_LO))
    '            Next

    '            .PROSEDUR2 = String.Join(vbCrLf, listPrsedur.ToArray)
    '            .PROSEDUR3 = ""
    '            .MemoEdit4 = MemoEdit4.Text
    '            .MemoEdit5 = MemoEdit5.Text
    '            .MemoEdit6 = MemoEdit6.Text
    '            .CheckEdit1 = CheckEdit1.Checked
    '            .CheckEdit2 = CheckEdit2.Checked
    '            .CheckEdit3 = CheckEdit3.Checked
    '            .CheckEdit4 = CheckEdit4.Checked
    '            .CheckEdit5 = CheckEdit5.Checked
    '            .CheckEdit6 = CheckEdit6.Checked
    '            .CheckEdit7 = CheckEdit7.Checked
    '            .CheckEdit8 = CheckEdit8.Checked
    '            .CheckEdit9 = CheckEdit9.Checked
    '            .CheckEdit10 = CheckEdit10.Checked
    '            .CheckEdit11 = CheckEdit11.Checked
    '            .CheckEdit12 = CheckEdit12.Checked
    '            .CheckEdit13 = CheckEdit13.Checked
    '            .CheckEdit14 = CheckEdit14.Checked
    '            .CheckEdit15 = CheckEdit15.Checked
    '            .CheckEdit16 = CheckEdit16.Checked
    '            .CheckEdit17 = CheckEdit17.Checked
    '            .CheckEdit18 = CheckEdit18.Checked
    '            .CheckEdit19 = CheckEdit19.Checked
    '            .CheckEdit20 = CheckEdit20.Checked
    '            .KDDOCTOR = sKDDOCTOR_DPJPUTAMA
    '            .BARCODEALAT = ""
    '            .DOKTER_KODE = ""
    '            .DOKTER_NAMEDISPLAY = ""
    '            '.SEQ = 0
    '            Try
    '                .CETAK = oS_DIGITAL_OK_LAPORANOPERASITEMPLATE.GetData(sKODEASESMENCOPY).CETAK
    '            Catch ex As Exception
    '                .CETAK = 0
    '            End Try
    '            .KDUSER = sUserID
    '            .KDUSER_SIGNATURE = ""
    '            .DATEDELETE = Now
    '        End With

    '        'DIAGNOSA
    '        Dim arrDetailDiagnosa = oS_DIGITAL_OK_LAPORANOPERASITEMPLATE.GetStructureDetailDiagnosaList
    '        For i As Integer = 0 To grvLODiagnosaPre.RowCount - 2
    '            Dim dsDetail = oS_DIGITAL_OK_LAPORANOPERASITEMPLATE.GetStructureDetailDiagnosa
    '            With dsDetail
    '                .SEQ = i
    '                .KDJUDUL = ds.KDJUDUL
    '                .KATEGORI = grvLODiagnosaPre.GetRowCellValue(i, colKATEGORI_LO)
    '                .NAMADIAGNOSA = grvLODiagnosaPre.GetRowCellValue(i, colNAMADIAGNOSA_LO)
    '                .KDDIAGNOSA = IIf(String.IsNullOrEmpty(grvLODiagnosaPre.GetRowCellValue(i, colKDDIAGNOSA_LO)), "", grvLODiagnosaPre.GetRowCellValue(i, colKDDIAGNOSA_LO))
    '            End With
    '            arrDetailDiagnosa.Add(dsDetail)
    '        Next

    '        Dim arrDetailDiagnosa2 = oS_DIGITAL_OK_LAPORANOPERASITEMPLATE.GetStructureDetailDiagnosa2List
    '        For i As Integer = 0 To grvLODiagnosa2.RowCount - 2
    '            Dim dsDetail = oS_DIGITAL_OK_LAPORANOPERASITEMPLATE.GetStructureDetailDiagnosa2
    '            With dsDetail
    '                .SEQ = i
    '                .KDJUDUL = ds.KDJUDUL
    '                .KATEGORI = grvLODiagnosa2.GetRowCellValue(i, colKATEGORI_LO2)
    '                .NAMADIAGNOSA = grvLODiagnosa2.GetRowCellValue(i, colNAMADIAGNOSA_LO2)
    '                .KDDIAGNOSA = IIf(String.IsNullOrEmpty(grvLODiagnosa2.GetRowCellValue(i, colKDDIAGNOSA_LO2)), "", grvLODiagnosa2.GetRowCellValue(i, colKDDIAGNOSA_LO2))
    '            End With
    '            arrDetailDiagnosa2.Add(dsDetail)
    '        Next

    '        'PROSEDUR
    '        Dim arrDetailProsedur = oS_DIGITAL_OK_LAPORANOPERASITEMPLATE.GetStructureDetailProsedurList
    '        For i As Integer = 0 To grvLOProsedur.RowCount - 2
    '            Dim dsDetail = oS_DIGITAL_OK_LAPORANOPERASITEMPLATE.GetStructureDetailProsedur
    '            With dsDetail
    '                .SEQ = i
    '                .KDJUDUL = ds.KDJUDUL
    '                .KATEGORI = ""
    '                .NAMAPROSEDUR = grvLOProsedur.GetRowCellValue(i, colNAMAPROSEDUR_LO)
    '                .KDPROSEDUR = IIf(String.IsNullOrEmpty(grvLOProsedur.GetRowCellValue(i, colKDPROSEDUR_LO)), "", grvLOProsedur.GetRowCellValue(i, colKDPROSEDUR_LO))
    '            End With
    '            arrDetailProsedur.Add(dsDetail)
    '        Next

    '        Dim dsCek = oS_DIGITAL_OK_LAPORANOPERASITEMPLATE.GetData(sKODEASESMENCOPY)

    '        If dsCek Is Nothing Then
    '            Try
    '                sKODEASESMENCOPY = oS_DIGITAL_OK_LAPORANOPERASITEMPLATE.InsertData(ds, arrDetailDiagnosa, arrDetailDiagnosa2, arrDetailProsedur)

    '                If sKODEASESMENCOPY = "" Then
    '                    fn_SaveTemplateLaporanTindakan = False
    '                Else
    '                    fn_SaveTemplateLaporanTindakan = True
    '                End If
    '            Catch ex As Exception
    '                MsgBox("Simpan Data Template Laporan Tindakan Template: " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
    '            End Try
    '        Else
    '            Try
    '                fn_SaveTemplateLaporanTindakan = oS_DIGITAL_OK_LAPORANOPERASITEMPLATE.UpdateData(ds, arrDetailDiagnosa, arrDetailDiagnosa2, arrDetailProsedur)
    '            Catch ex As Exception
    '                MsgBox("Simpan Data Template Laporan Tindakan Template: " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
    '            End Try
    '        End If
    '    Catch oErr As Exception
    '        MsgBox("Simpan Data Template Laporan Tindakan: " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
    '        fn_SaveTemplateLaporanTindakan = False
    '    End Try
    'End Function
#End Region
#Region "Command Button"
    Private Sub btnBackLaporanTindakan_Click(sender As Object, e As EventArgs) Handles btnBackLaporanTindakan.Click
        lTINDAKAN_GRD.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        lTINDAKAN_PANEL.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        PanelControl27.Visible = True
        fn_LoadLaporanTindakan(lblNoRM.Text)
        pnlLaporanTindakan.Controls.Clear()
        If Not frmLaporanTindakan Is Nothing Then frmLaporanTindakan.Dispose()
        frmLaporanTindakan = Nothing

        tabbedControlGroup1.SelectedTabPageIndex = 14
        tabbedControlGroup1_SelectedPageChanged()
    End Sub
    Private Sub picLaporanTindakan_Add_Click(sender As Object, e As EventArgs) Handles picLaporanTindakan_Add.Click
        'Dim frmLaporanTindakan As New frmLaporanTindakan
        'Try
        '    frmLaporanTindakan.LoadMe(FORM_MODE.FORM_MODE_ADD, "", sKDDOCTOR_INPUT, sKDDOCTOR_DPJPUTAMA, lblNoRM.Text, lblRegister.Text)
        '    frmLaporanTindakan.ShowDialog(Me)
        '    fn_LoadLaporanTindakan(lblNoRM.Text)
        'Catch oErr As Exception
        '    MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        'Finally
        '    If Not frmLaporanTindakan Is Nothing Then frmLaporanTindakan.Dispose()
        '    frmLaporanTindakan = Nothing
        'End Try
        Try
            lTINDAKAN_GRD.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            lTINDAKAN_PANEL.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            PanelControl27.Visible = False

            ' Bersihkan konten panel
            pnlLaporanTindakan.Controls.Clear()

            ' Set properti form anak
            frmLaporanTindakan.TopLevel = False
            frmLaporanTindakan.FormBorderStyle = FormBorderStyle.None
            frmLaporanTindakan.Dock = DockStyle.Fill

            ' Tambahkan form anak ke panel
            pnlLaporanTindakan.Controls.Add(frmLaporanTindakan)

            ' Tampilkan form anak
            frmLaporanTindakan.LoadMe(False, FORM_MODE.FORM_MODE_ADD, "", sKDDOCTOR_INPUT, sKDDOCTOR_DPJPUTAMA, lblNoRM.Text, lblRegister.Text, IIf(sISDOKTER = True, "", sKDUSER_PERAWAT))
            frmLaporanTindakan.Show()
        Catch ex As Exception
            MsgBox("Load Form Detail : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub picLaporanTindakan_Update_Click(sender As Object, e As EventArgs) Handles picLaporanTindakan_Update.Click
        If grvLaporanTindakan_List.GetFocusedRowCellValue("KDLAPORANTINDAKAN") Is Nothing Then
            Exit Sub
        End If

        'Dim frmLaporanTindakan As New frmLaporanTindakan
        'Try
        '    frmLaporanTindakan.LoadMe(FORM_MODE.FORM_MODE_EDIT, grvLaporanTindakan_List.GetFocusedRowCellValue("KDLAPORANTINDAKAN"), sKDDOCTOR_INPUT, sKDDOCTOR_DPJPUTAMA, lblNoRM.Text, grvLaporanTindakan_List.GetFocusedRowCellValue("KDPENDAFTARAN"))
        '    frmLaporanTindakan.ShowDialog(Me)
        '    fn_LoadLaporanTindakan(lblNoRM.Text)
        'Catch oErr As Exception
        '    MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        'Finally
        '    If Not frmLaporanTindakan Is Nothing Then frmLaporanTindakan.Dispose()
        '    frmLaporanTindakan = Nothing
        'End Try
        Try
            lTINDAKAN_GRD.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            lTINDAKAN_PANEL.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            PanelControl27.Visible = False

            ' Bersihkan konten panel
            pnlLaporanTindakan.Controls.Clear()

            ' Set properti form anak
            frmLaporanTindakan.TopLevel = False
            frmLaporanTindakan.FormBorderStyle = FormBorderStyle.None
            frmLaporanTindakan.Dock = DockStyle.Fill

            ' Tambahkan form anak ke panel
            pnlLaporanTindakan.Controls.Add(frmLaporanTindakan)

            ' Tampilkan form anak
            frmLaporanTindakan.LoadMe(False, FORM_MODE.FORM_MODE_EDIT, grvLaporanTindakan_List.GetFocusedRowCellValue("KDLAPORANTINDAKAN"), sKDDOCTOR_INPUT, sKDDOCTOR_DPJPUTAMA, lblNoRM.Text, grvLaporanTindakan_List.GetFocusedRowCellValue("KDPENDAFTARAN"), IIf(sISDOKTER = True, "", sKDUSER_PERAWAT))
            frmLaporanTindakan.Show()
        Catch ex As Exception
            MsgBox("Load Form Detail : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub picLaporanTindakan_Delete_Click(sender As Object, e As EventArgs) Handles picLaporanTindakan_Delete.Click
        If grvLaporanTindakan_List.GetFocusedRowCellValue("KDLAPORANTINDAKAN") Is Nothing Then
            Exit Sub
        End If
        Dim oLaporanTindakan As New EMedrek.clsS_DIGITAL_OK_LAPORANTINDAKAN

        If oLaporanTindakan.UpdateDeleteLaporanTindakan(grvLaporanTindakan_List.GetFocusedRowCellValue("KDLAPORANTINDAKAN"), sUserID) = True Then
            MsgBox("Berhasil Delete!", MsgBoxStyle.Exclamation, Me.Text)
            fn_LoadLaporanTindakan(lblNoRM.Text)
        End If
    End Sub
    Private Sub picLaporanTindakan_Refresh_Click(sender As Object, e As EventArgs) Handles picLaporanTindakan_Refresh.Click
        fn_LoadLaporanTindakan(lblNoRM.Text)
    End Sub
    'Private Sub btnSimpanLaporanTindakan_Click() Handles btnSimpanLaporanTindakan.Click
    '    If fn_ValidateLaporanTindakan() = False Then Exit Sub
    '    'If MsgBox("Save " & txtNoRegister.Text.Trim.ToUpper & "?", MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, Me.Text) = MsgBoxResult.No Then Exit Sub
    '    If fn_SaveLaporanTindakan() = False Then
    '        MsgBox("Save gagal! Tolong kontak admin anda!", MsgBoxStyle.Exclamation, Me.Text)
    '    Else
    '        tabbedControlGroup1.SelectedTabPageIndex = 14
    '        tabbedControlGroup1_SelectedPageChanged()
    '    End If
    'End Sub
#End Region
#Region "Lookup / Event"
    'Private Sub DeleteToolStripLODiagnosa_Click(sender As Object, e As EventArgs) Handles DeleteToolStripLODiagnosa.Click
    '    grvLODiagnosaPre.DeleteSelectedRows()
    'End Sub
    'Private Sub DeleteToolStripLODiagnosa_2_Click(sender As Object, e As EventArgs) Handles DeleteToolStripLODiagnosa_2.Click
    '    grvLODiagnosa2.DeleteSelectedRows()
    'End Sub
    'Private Sub DeleteToolStripLOProsedur_Click(sender As Object, e As EventArgs) Handles DeleteToolStripLOProsedur.Click
    '    grvLOProsedur.DeleteSelectedRows()
    'End Sub
    'Private Sub TimeEdit15_EditValueChanging(sender As Object, e As EventArgs) Handles TimeEdit15.EditValueChanged
    '    If isLoadLaporanTindkan = True Then
    '        ' Ambil nilai waktu dari TimeEdit
    '        Dim waktuAwal As DateTime = TimeEdit14.EditValue
    '        Dim waktuAkhir As DateTime = TimeEdit15.EditValue

    '        If waktuAkhir >= waktuAwal Then
    '            Dim selisih As TimeSpan = waktuAkhir - waktuAwal
    '            txtLamaOperasi.Text = selisih.Hours & " Jam " & selisih.Minutes & " Menit"
    '        Else
    '            txtLamaOperasi.Text = "00 Jam 00 Menit"
    '        End If
    '    End If
    'End Sub
    'Private Sub txtCariDiagnosaLO1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtCariDiagnosaLO1.KeyPress
    '    If Asc(e.KeyChar) = 13 Then
    '        If txtCariDiagnosaLO1.Text.Length < 3 Then
    '            MsgBox("Minimal 3 Huruf !!!", MsgBoxStyle.Exclamation, Me.Text)
    '            Exit Sub
    '        End If
    '        Try
    '            Dim jsonDecode = JObject.Parse(oBrigging.fn_Pencariandiagnosa(txtCariDiagnosaLO1.Text))
    '            Dim sDataDuplicate As String = String.Empty
    '            Dim smessage As String = String.Empty

    '            sDataDuplicate = jsonDecode("metadata")("code").ToString
    '            smessage = jsonDecode("metadata")("message").ToString

    '            If sDataDuplicate = "200" Then
    '                Dim table As DataTable

    '                table = New DataTable("M_DIAGNOSA")
    '                table.Columns.Add("nama")
    '                table.Columns.Add("kode")

    '                For Each item In jsonDecode("response")("data")
    '                    table.Rows.Add(New String() {item(0), item(1)})
    '                Next

    '                grdCariDiganosaLO1.Properties.DataSource = table
    '                grdCariDiganosaLO1.Properties.ValueMember = "kode"
    '                grdCariDiganosaLO1.Properties.DisplayMember = "nama"

    '                grdCariDiganosaLO1.ShowPopup()

    '                txtCariDiagnosaLO1.ResetText()
    '            Else
    '                MsgBox("Pencarian Diagnosa" & vbCrLf & sDataDuplicate & "-" & smessage, MsgBoxStyle.Information, Me.Text)
    '            End If
    '        Catch oErr As Exception
    '            MsgBox("Pencarian Diagnosa: " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
    '        End Try
    '    End If
    'End Sub
    'Private Sub grdCariDiganosaLO1_EditValueChanged(sender As Object, e As EventArgs) Handles grdCariDiganosaLO1.EditValueChanged
    '    If isLoadLaporanTindkan = False Then Exit Sub

    '    If grdCariDiganosaLO1.Text <> "" Then
    '        Dim sCek As Integer = 0

    '        For i As Integer = 0 To grvLODiagnosaPre.RowCount - 2
    '            sCek += 1
    '        Next

    '        grvLODiagnosaPre.Focus()
    '        grvLODiagnosaPre.AddNewRow()
    '        grvLODiagnosaPre.SetFocusedRowCellValue(colKATEGORI_LO, IIf(sCek = 0, "Primer", "Sekunder"))
    '        grvLODiagnosaPre.SetFocusedRowCellValue(colKDDIAGNOSA_LO, grdCariDiganosaLO1.EditValue)
    '        grvLODiagnosaPre.SetFocusedRowCellValue(colNAMADIAGNOSA_LO, grdCariDiganosaLO1.Text)
    '        grvLODiagnosaPre.UpdateCurrentRow()

    '        grvLODiagnosaPre.Focus()
    '    End If
    'End Sub
    'Private Sub txtCariDiagnosaLO2_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtCariDiagnosaLO2.KeyPress
    '    If Asc(e.KeyChar) = 13 Then
    '        If txtCariDiagnosaLO2.Text.Length < 3 Then
    '            MsgBox("Minimal 3 Huruf !!!", MsgBoxStyle.Exclamation, Me.Text)
    '            Exit Sub
    '        End If
    '        Try
    '            Dim jsonDecode = JObject.Parse(oBrigging.fn_Pencariandiagnosa(txtCariDiagnosaLO2.Text))
    '            Dim sDataDuplicate As String = String.Empty
    '            Dim smessage As String = String.Empty

    '            sDataDuplicate = jsonDecode("metadata")("code").ToString
    '            smessage = jsonDecode("metadata")("message").ToString

    '            If sDataDuplicate = "200" Then
    '                Dim table As DataTable

    '                table = New DataTable("M_DIAGNOSA")
    '                table.Columns.Add("nama")
    '                table.Columns.Add("kode")

    '                For Each item In jsonDecode("response")("data")
    '                    table.Rows.Add(New String() {item(0), item(1)})
    '                Next

    '                grdCariDiganosaLO2.Properties.DataSource = table
    '                grdCariDiganosaLO2.Properties.ValueMember = "kode"
    '                grdCariDiganosaLO2.Properties.DisplayMember = "nama"

    '                grdCariDiganosaLO2.ShowPopup()

    '                txtCariDiagnosaLO2.ResetText()
    '            Else
    '                MsgBox("Pencarian Diagnosa" & vbCrLf & sDataDuplicate & "-" & smessage, MsgBoxStyle.Information, Me.Text)
    '            End If
    '        Catch oErr As Exception
    '            MsgBox("Pencarian Diagnosa: " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
    '        End Try
    '    End If
    'End Sub
    'Private Sub grdCariDiganosaLO2_EditValueChanged(sender As Object, e As EventArgs) Handles grdCariDiganosaLO2.EditValueChanged
    '    If isLoadLaporanTindkan = False Then Exit Sub

    '    If grdCariDiganosaLO2.Text <> "" Then
    '        Dim sCek As Integer = 0

    '        For i As Integer = 0 To grvLODiagnosa2.RowCount - 2
    '            sCek += 1
    '        Next

    '        grvLODiagnosa2.Focus()
    '        grvLODiagnosa2.AddNewRow()
    '        grvLODiagnosa2.SetFocusedRowCellValue(colKATEGORI_LO2, IIf(sCek = 0, "Primer", "Sekunder"))
    '        grvLODiagnosa2.SetFocusedRowCellValue(colKDDIAGNOSA_LO2, grdCariDiganosaLO2.EditValue)
    '        grvLODiagnosa2.SetFocusedRowCellValue(colNAMADIAGNOSA_LO2, grdCariDiganosaLO2.Text)
    '        grvLODiagnosa2.UpdateCurrentRow()

    '        grvLODiagnosa2.Focus()
    '    End If
    'End Sub
    'Private Sub txtCariProsedur_LO_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtCariProsedur_LO.KeyPress
    '    If Asc(e.KeyChar) = 13 Then
    '        Try
    '            If txtCariProsedur_LO.Text.Length < 3 Then
    '                MsgBox("Minimal 3 Huruf !!!", MsgBoxStyle.Exclamation, Me.Text)
    '                Exit Sub
    '            End If

    '            Try
    '                Dim jsonDecode = JObject.Parse(oBrigging.fn_PencarianProsedur("VCLAIM", txtCariProsedur_LO.Text))
    '                Dim sDataDuplicate As String = String.Empty
    '                Dim smessage As String = String.Empty

    '                sDataDuplicate = jsonDecode("metadata")("code").ToString
    '                smessage = jsonDecode("metadata")("message").ToString

    '                If sDataDuplicate = "200" Then
    '                    Dim table As DataTable

    '                    table = New DataTable("M_PROSEDUR")
    '                    table.Columns.Add("nama")
    '                    table.Columns.Add("kode")

    '                    For Each item In jsonDecode("response")("data")
    '                        table.Rows.Add(New String() {item(0), item(1)})
    '                    Next

    '                    grdCariProsedur_LO.Properties.DataSource = table
    '                    grdCariProsedur_LO.Properties.ValueMember = "kode"
    '                    grdCariProsedur_LO.Properties.DisplayMember = "nama"

    '                    grdCariProsedur_LO.ShowPopup()

    '                    txtCariProsedur_LO.ResetText()
    '                Else
    '                    MsgBox("Pencarian Prosedur" & vbCrLf & sDataDuplicate & "-" & smessage, MsgBoxStyle.Information, Me.Text)
    '                End If
    '            Catch oErr As Exception
    '                MsgBox("Pencarian Prosedur: " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
    '            End Try
    '        Catch oErr As Exception
    '            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
    '        End Try
    '    End If
    'End Sub
    'Private Sub grdCariProsedur_LO_EditValueChanged(sender As Object, e As EventArgs) Handles grdCariProsedur_LO.EditValueChanged
    '    If isLoadLaporanTindkan = False Then Exit Sub

    '    If grdCariProsedur_LO.Text <> "" Then
    '        grvLOProsedur.Focus()
    '        grvLOProsedur.AddNewRow()
    '        grvLOProsedur.SetFocusedRowCellValue(colKDPROSEDUR_LO, grdCariProsedur_LO.EditValue)
    '        grvLOProsedur.SetFocusedRowCellValue(colNAMAPROSEDUR_LO, grdCariProsedur_LO.Text)
    '        grvLOProsedur.UpdateCurrentRow()

    '        txtCariProsedur_LO.Focus()
    '    End If
    'End Sub
    'Private Sub fn_LoadOperatorTindkan()
    '    Try
    '        Dim oConn As New SqlConnection
    '        Dim oComm As New SqlCommand
    '        Dim da As SqlDataAdapter
    '        Dim ds As New DataSet
    '        Dim SQL As String
    '        Dim sConn As String = sConnOld
    '        oConn = New SqlConnection(sConn)

    '        If oConn.State = ConnectionState.Closed Then
    '            oConn.Open()
    '        End If

    '        SQL = "SELECT "
    '        SQL &= "A.KDDOCTOR "
    '        SQL &= ",NAME_DISPLAY = (SELECT CASE A.FRONT_TITLE WHEN '' THEN '' ELSE A.FRONT_TITLE + ' ' END) + A.NAME_DISPLAY + A.BACK_TITLE "
    '        SQL &= "FROM "
    '        SQL &= "M_DOCTOR A "
    '        SQL &= "WHERE "
    '        SQL &= "A.ISACTIVE = 1 "
    '        SQL &= "AND A.CATEGORY = 1 "
    '        SQL &= "ORDER BY A.NAME_DISPLAY "

    '        oComm.Connection = oConn
    '        oComm.CommandText = SQL
    '        oComm.CommandTimeout = 120
    '        oComm.CommandType = CommandType.Text

    '        da = New SqlDataAdapter(oComm)
    '        da.Fill(ds, "DOKTER")

    '        grdOperatorLT.Properties.DataSource = ds.Tables("DOKTER")
    '        grdOperatorLT.Properties.ValueMember = "KDDOCTOR"
    '        grdOperatorLT.Properties.DisplayMember = "NAME_DISPLAY"

    '        grdDokterAhliOperatorLT.Properties.DataSource = ds.Tables("DOKTER")
    '        grdDokterAhliOperatorLT.Properties.ValueMember = "KDDOCTOR"
    '        grdDokterAhliOperatorLT.Properties.DisplayMember = "NAME_DISPLAY"

    '        If oConn.State = ConnectionState.Open Then
    '            oConn.Close()
    '        End If
    '    Catch oErr As Exception
    '        MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
    '    End Try
    'End Sub
    'Private Sub fn_LoadPERAWATTINDAKAN()
    '    Dim oTemplate As New Reference.clsUnit
    '    Try
    '        Dim ds = oTemplate.GetData.Where(Function(x) x.ISACTIVE = True).ToList()

    '        grdAsistenLT.Properties.DataSource = ds
    '        grdAsistenLT.Properties.ValueMember = "MEMO"
    '        grdAsistenLT.Properties.DisplayMember = "MEMO"

    '        grdPerawatInstrumenLT.Properties.DataSource = ds
    '        grdPerawatInstrumenLT.Properties.ValueMember = "MEMO"
    '        grdPerawatInstrumenLT.Properties.DisplayMember = "MEMO"

    '    Catch oErr As Exception
    '        MsgBox("Load Perawat : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
    '    End Try
    'End Sub
    'Private Sub fn_JENISTINDAKAN()
    '    Try
    '        Dim oConn As New SqlConnection
    '        Dim oComm As New SqlCommand
    '        Dim da As SqlDataAdapter
    '        Dim ds As New DataSet
    '        Dim SQL As String
    '        Dim sConn As String = Decrypt(My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Software\SIMRS\SW\", "Database", "").ToString())
    '        oConn = New SqlConnection(sConn)

    '        If oConn.State = ConnectionState.Closed Then
    '            oConn.Open()
    '        End If

    '        SQL = "SELECT "
    '        SQL &= "* "
    '        SQL &= "FROM "
    '        SQL &= "M_LAPORAN_OPERASI A "
    '        SQL &= "WHERE "
    '        SQL &= "A.ISACTIVE = 1 "

    '        oComm.Connection = oConn
    '        oComm.CommandText = SQL
    '        oComm.CommandTimeout = 120
    '        oComm.CommandType = CommandType.Text

    '        da = New SqlDataAdapter(oComm)
    '        da.Fill(ds, "JENISLAPORAN")

    '        grdLAPORANTINDAKANJUDUL.Properties.DataSource = ds.Tables("JENISLAPORAN")
    '        grdLAPORANTINDAKANJUDUL.Properties.ValueMember = "NAMALAPORAN"
    '        grdLAPORANTINDAKANJUDUL.Properties.DisplayMember = "NAMALAPORAN"

    '        If oConn.State = ConnectionState.Open Then
    '            oConn.Close()
    '        End If

    '        'For i As Integer = 0 To ds.Tables("JENISLAPORAN").Rows.Count - 1
    '        '    If ds.Tables("JENISLAPORAN").Rows(i)("LAMPIRAN").ToString.Contains(sSpesialis) Then
    '        '        cboJenisOperasi.EditValue = ds.Tables("JENISLAPORAN").Rows(i)("NAMALAPORAN").ToString()
    '        '    End If
    '        'Next
    '    Catch oErr As Exception
    '        MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
    '    End Try
    'End Sub
#End Region
#Region "Handlle"
    'Private Sub chKamarOperasi_Click(sender As Object, e As EventArgs) Handles CheckEdit1.Click, CheckEdit2.Click, CheckEdit3.Click, CheckEdit4.Click, CheckEdit5.Click, CheckEdit6.Click
    '    CheckEdit1.Checked = False
    '    CheckEdit2.Checked = False
    '    CheckEdit3.Checked = False
    '    CheckEdit4.Checked = False
    '    CheckEdit5.Checked = False
    '    CheckEdit6.Checked = False
    'End Sub
    'Private Sub chJenisAnestesi_Click(sender As Object, e As EventArgs) Handles CheckEdit7.Click, CheckEdit8.Click, CheckEdit9.Click, CheckEdit10.Click
    '    CheckEdit7.Checked = False
    '    CheckEdit8.Checked = False
    '    CheckEdit9.Checked = False
    '    CheckEdit10.Checked = False
    'End Sub
    'Private Sub chKlasifikasi_Click(sender As Object, e As EventArgs) Handles CheckEdit11.Click, CheckEdit12.Click
    '    CheckEdit11.Checked = False
    '    CheckEdit12.Checked = False
    'End Sub
    'Private Sub chJenisOperasi_Click(sender As Object, e As EventArgs) Handles CheckEdit13.Click, CheckEdit14.Click, CheckEdit15.Click, CheckEdit16.Click
    '    CheckEdit13.Checked = False
    '    CheckEdit14.Checked = False
    '    CheckEdit15.Checked = False
    '    CheckEdit16.Checked = False
    'End Sub
    'Private Sub chPemeriksaanPA_Click(sender As Object, e As EventArgs) Handles CheckEdit17.Click, CheckEdit18.Click
    '    CheckEdit17.Checked = False
    '    CheckEdit18.Checked = False
    'End Sub
    'Private Sub chPemeriksaanCairan_Click(sender As Object, e As EventArgs) Handles CheckEdit19.Click, CheckEdit20.Click
    '    CheckEdit19.Checked = False
    '    CheckEdit20.Checked = False
    'End Sub
    'Private Sub btnLoadDataLaporanOperasi_Click(sender As Object, e As EventArgs) Handles btnLoadData.Click
    '    Dim frmListTemplate As New frmListTemplate
    '    Try
    '        sKODEASESMENCOPY = String.Empty

    '        frmListTemplate.fn_LoadKategori(4, "")
    '        frmListTemplate.ShowDialog(Me)

    '        If sKODEASESMENCOPY <> "" Then
    '            fn_LoadDataTemplateLaporanOperasi(sKODEASESMENCOPY)
    '        End If
    '    Catch oErr As Exception
    '        MsgBox("Load Data Template" & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
    '    End Try
    'End Sub
    'Private Sub btnSaveLaporanOperasi_Click(sender As Object, e As EventArgs) Handles btnSave.Click
    '    If sKODEASESMENCOPY = "" Then
    '        MsgBox("Silahkan Load Data Terlebih Dahulu", MsgBoxStyle.Exclamation, Me.Text)
    '        Exit Sub
    '    End If

    '    If MsgBox("Save " & sKODEASESMENCOPY & "?", MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, Me.Text) = MsgBoxResult.No Then Exit Sub
    '    If fn_SaveTemplateLaporanOperasi() = False Then
    '        MsgBox("Save gagal! Tolong kontak admin anda!", MsgBoxStyle.Exclamation, Me.Text)
    '    Else
    '        MsgBox("Save " & sKODEASESMENCOPY & " success!", MsgBoxStyle.Information, Me.Text)
    '    End If
    'End Sub
    'Private Sub btnSaveAsLaporanOperasi_Click(sender As Object, e As EventArgs) Handles btnSaveAs.Click
    '    sRemarks = String.Empty
    '    frmJudulTemplate.ShowDialog(Me)

    '    If sRemarks = "" Then
    '        MsgBox("Judul Template Masih Kosong", MsgBoxStyle.Exclamation, Me.Text)
    '    Else
    '        sKODEASESMENCOPY = sRemarks

    '        Dim oS_DIGITAL_OK_LAPORANOPERASITEMPLATE As New Transaksi.clsS_DIGITAL_OK_LAPORANOPERASITemplate

    '        Dim ds = oS_DIGITAL_OK_LAPORANOPERASITEMPLATE.GetData(sKODEASESMENCOPY)
    '        If ds IsNot Nothing Then
    '            MsgBox("Judul Sudah Ada", MsgBoxStyle.Exclamation, Me.Text)
    '            Exit Sub
    '        End If

    '        If MsgBox("Save As " & sKODEASESMENCOPY & "?", MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, Me.Text) = MsgBoxResult.No Then Exit Sub
    '        If fn_SaveTemplateLaporanOperasi() = False Then
    '            sKODEASESMENCOPY = ""
    '            MsgBox("Save As gagal! Tolong kontak admin anda!", MsgBoxStyle.Exclamation, Me.Text)
    '        Else
    '            MsgBox("Save As " & sKODEASESMENCOPY & " success!", MsgBoxStyle.Information, Me.Text)
    '        End If
    '    End If

    '    sRemarks = String.Empty

    'End Sub
#End Region
#End Region
#Region "Lembar Observasi"
    Private Sub fn_LoadDataLembarObservasi()
        Try
            Dim ds = From x In oLembarObservasi.GetDataByRM(lblNoRM.Text)
                     Select x.KDLEMBAROBSERVASI, x.KDPENDAFTARAN, TANGGAL = x.DATE, x.DOKTER, RUANGAN = x.TUJUAN, x.KDUSER

            grdLembarObservasi.DataSource = ds.ToList

            fn_LoadFormatDataLembarObservasi()
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadFormatDataLembarObservasi()
        For iLoop As Integer = 0 To grvLembarObservasi.Columns.Count - 1
            If grvLembarObservasi.Columns(iLoop).ColumnType.Name = "Decimal" Then
                grvLembarObservasi.Columns(iLoop).DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                grvLembarObservasi.Columns(iLoop).DisplayFormat.FormatString = "{0:n2}"
                grvLembarObservasi.Columns(iLoop).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
            ElseIf grvLembarObservasi.Columns(iLoop).ColumnType.Name = "DateTime" Then
                grvLembarObservasi.Columns(iLoop).DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
                grvLembarObservasi.Columns(iLoop).DisplayFormat.FormatString = "{0:dd/MM/yyyy}"
            End If
        Next

        grvLembarObservasi.Columns("KDLEMBAROBSERVASI").VisibleIndex = -1
        grvLembarObservasi.Columns("KDPENDAFTARAN").VisibleIndex = -1
    End Sub
    Private Function fn_DeleteDataLembarObservasi(ByVal sKDLEMBAROBSERVASI As String) As Boolean
        Try
            oLembarObservasi.DeleteData(sKDLEMBAROBSERVASI)

            fn_DeleteDataLembarObservasi = True
        Catch oErr As Exception
            fn_DeleteDataLembarObservasi = False
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Function
    Private Sub grvLembarObservasi_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles grvLembarObservasi.DoubleClick
        If grvLembarObservasi.GetFocusedRowCellValue("KDLEMBAROBSERVASI") Is Nothing Then
            Exit Sub
        End If

        Dim frmLembarObservasi As New frmLembarObservasi
        Try
            frmLembarObservasi.LoadMe(FORM_MODE.FORM_MODE_VIEW, "", grvLembarObservasi.GetFocusedRowCellValue("KDPENDAFTARAN"), lblNoRM.Text, lblNamaPasien.Text, sTanggalLahir, sUMURPASIEN, lblJenisKelamin.Text, "-", grvLembarObservasi.GetFocusedRowCellValue("RUANGAN"), sKDDOCTOR_INPUT, lblKartuBPJS.Text, grvLembarObservasi.GetFocusedRowCellValue("KDLEMBAROBSERVASI"))
            frmLembarObservasi.ShowDialog(Me)
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub picAddLembarObservasi_Click() Handles picAddLembarObservasi.Click
        Dim frmLembarObservasi As New frmLembarObservasi
        Try
            frmLembarObservasi.LoadMe(FORM_MODE.FORM_MODE_ADD, "", lblRegister.Text, lblNoRM.Text, lblNamaPasien.Text, sTanggalLahir, sUMURPASIEN, lblJenisKelamin.Text, "-", lblRuangan.Text, sKDDOCTOR_INPUT, lblKartuBPJS.Text, "")
            frmLembarObservasi.ShowDialog(Me)
            XtraTabControl1_SelectedPageChanged()
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        Finally
            If Not frmLembarObservasi Is Nothing Then frmLembarObservasi.Dispose()
            frmLembarObservasi = Nothing

            Dim rowHandle As Integer = grvLembarObservasi.LocateByValue(rowHandle, grvLembarObservasi.Columns("KDLEMBAROBSERVASI"), sCode)
            If rowHandle > 0 Then grvLembarObservasi.FocusedRowHandle = rowHandle

            If sStatusSave = "NEW" Then
                sStatusSave = "NONE"
            End If
        End Try
    End Sub
    Private Sub picpicUpdateLembarObservasi_Click() Handles picUpdateLembarObservasi.Click
        If grvLembarObservasi.GetFocusedRowCellValue("KDLEMBAROBSERVASI") Is Nothing Then
            Exit Sub
        End If
        Dim frmLembarObservasi As New frmLembarObservasi
        Try
            frmLembarObservasi.LoadMe(FORM_MODE.FORM_MODE_EDIT, "", grvLembarObservasi.GetFocusedRowCellValue("KDPENDAFTARAN"), lblNoRM.Text, lblNamaPasien.Text, sTanggalLahir, sUMURPASIEN, lblJenisKelamin.Text, "-", grvLembarObservasi.GetFocusedRowCellValue("RUANGAN"), sKDDOCTOR_INPUT, lblKartuBPJS.Text, grvLembarObservasi.GetFocusedRowCellValue("KDLEMBAROBSERVASI"))
            frmLembarObservasi.ShowDialog(Me)
            XtraTabControl1_SelectedPageChanged()
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        Finally
            If Not frmLembarObservasi Is Nothing Then frmLembarObservasi.Dispose()
            frmLembarObservasi = Nothing

            Dim rowHandle As Integer = grvLembarObservasi.LocateByValue(rowHandle, grvLembarObservasi.Columns("KDLEMBAROBSERVASI"), sCode)
            If rowHandle > 0 Then grvLembarObservasi.FocusedRowHandle = rowHandle

            If sStatusSave = "NEW" Then
                sStatusSave = "NONE"
                picAddLembarObservasi_Click()
            End If
        End Try
    End Sub
    Private Sub picpicDeleteLembarObservasi_Click() Handles picDeleteLembarObservasi.Click
        If grvLembarObservasi.GetFocusedRowCellValue("KDLEMBAROBSERVASI") Is Nothing Then
            Exit Sub
        End If
        If MsgBox(Statement.DeleteQuestion, MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, Me.Text) = MsgBoxResult.No Then Exit Sub
        If fn_DeleteDataLembarObservasi(grvLembarObservasi.GetFocusedRowCellValue("KDLEMBAROBSERVASI")) = False Then
            MsgBox(Statement.DeleteFail, MsgBoxStyle.Exclamation, Me.Text)
            Exit Sub
        End If
        MsgBox(Statement.DeleteSuccess, MsgBoxStyle.Information, Me.Text)
        XtraTabControl1_SelectedPageChanged()
    End Sub
    Private Sub picRefreshLembarObservasi_Click() Handles picRefreshLembarObservasi.Click
        XtraTabControl1_SelectedPageChanged()
    End Sub
#End Region
#Region "Catatan Keperawatan"
    Private Sub fn_LoadDataCatatanKeperawatan()
        Try
            Dim ds = From x In oCatatatanKeperawatan.GetDataByRM(lblNoRM.Text)
                     Select x.KDCATATANPERAWAT, x.KDPENDAFTARAN, TANGGAL = x.DATE, x.DOKTER, RUANGAN = x.TUJUAN, x.KDUSER

            grdCatatanKeperawatan.DataSource = ds.ToList

            fn_LoadFormatDataCatatanKeperawatan()
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadFormatDataCatatanKeperawatan()
        For iLoop As Integer = 0 To grvCatatanKeperawatan.Columns.Count - 1
            If grvCatatanKeperawatan.Columns(iLoop).ColumnType.Name = "Decimal" Then
                grvCatatanKeperawatan.Columns(iLoop).DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                grvCatatanKeperawatan.Columns(iLoop).DisplayFormat.FormatString = "{0:n2}"
                grvCatatanKeperawatan.Columns(iLoop).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
            ElseIf grvCatatanKeperawatan.Columns(iLoop).ColumnType.Name = "DateTime" Then
                grvCatatanKeperawatan.Columns(iLoop).DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
                grvCatatanKeperawatan.Columns(iLoop).DisplayFormat.FormatString = "{0:dd/MM/yyyy}"
            End If
        Next

        grvCatatanKeperawatan.Columns("KDCATATANPERAWAT").VisibleIndex = -1
        grvCatatanKeperawatan.Columns("KDPENDAFTARAN").VisibleIndex = -1
    End Sub
    Private Function fn_DeleteDataCatatanKeperawatan(ByVal sKDCatatanKeperawatan As String) As Boolean
        Try
            oCatatatanKeperawatan.DeleteData(sKDCatatanKeperawatan)

            fn_DeleteDataCatatanKeperawatan = True
        Catch oErr As Exception
            fn_DeleteDataCatatanKeperawatan = False
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Function
    Private Sub grvCatatanKeperawatan_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles grvCatatanKeperawatan.DoubleClick
        If grvCatatanKeperawatan.GetFocusedRowCellValue("KDCATATANPERAWAT") Is Nothing Then
            Exit Sub
        End If

        Dim frmCatatanKeperawatan As New frmCatatanKeperawatan
        Try
            frmCatatanKeperawatan.LoadMe(FORM_MODE.FORM_MODE_VIEW, "", grvCatatanKeperawatan.GetFocusedRowCellValue("KDPENDAFTARAN"), lblNoRM.Text, lblNamaPasien.Text, sTanggalLahir, sUMURPASIEN, lblJenisKelamin.Text, "-", grvCatatanKeperawatan.GetFocusedRowCellValue("RUANGAN"), sKDDOCTOR_INPUT, lblKartuBPJS.Text, grvCatatanKeperawatan.GetFocusedRowCellValue("KDCATATANPERAWAT"))
            frmCatatanKeperawatan.ShowDialog(Me)
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub picAddCatatanKeperawatan_Click() Handles picAddCatatanKeperawatan.Click
        Dim frmCatatanKeperawatan As New frmCatatanKeperawatan
        Try
            frmCatatanKeperawatan.LoadMe(FORM_MODE.FORM_MODE_ADD, "", lblRegister.Text, lblNoRM.Text, lblNamaPasien.Text, sTanggalLahir, sUMURPASIEN, lblJenisKelamin.Text, "-", lblRuangan.Text, sKDDOCTOR_INPUT, lblKartuBPJS.Text, "")
            frmCatatanKeperawatan.ShowDialog(Me)
            XtraTabControl1_SelectedPageChanged()
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        Finally
            If Not frmCatatanKeperawatan Is Nothing Then frmCatatanKeperawatan.Dispose()
            frmCatatanKeperawatan = Nothing

            Dim rowHandle As Integer = grvCatatanKeperawatan.LocateByValue(rowHandle, grvCatatanKeperawatan.Columns("KDCATATANPERAWAT"), sCode)
            If rowHandle > 0 Then grvCatatanKeperawatan.FocusedRowHandle = rowHandle

            If sStatusSave = "NEW" Then
                sStatusSave = "NONE"
            End If
        End Try
    End Sub
    Private Sub picpicUpdateCatatanKeperawatan_Click() Handles picUpdateCatatanKeperawatan.Click
        If grvCatatanKeperawatan.GetFocusedRowCellValue("KDCATATANPERAWAT") Is Nothing Then
            Exit Sub
        End If
        Dim frmCatatanKeperawatan As New frmCatatanKeperawatan
        Try
            frmCatatanKeperawatan.LoadMe(FORM_MODE.FORM_MODE_EDIT, "", grvCatatanKeperawatan.GetFocusedRowCellValue("KDPENDAFTARAN"), lblNoRM.Text, lblNamaPasien.Text, sTanggalLahir, sUMURPASIEN, lblJenisKelamin.Text, "-", grvCatatanKeperawatan.GetFocusedRowCellValue("RUANGAN"), sKDDOCTOR_INPUT, lblKartuBPJS.Text, grvCatatanKeperawatan.GetFocusedRowCellValue("KDCATATANPERAWAT"))
            frmCatatanKeperawatan.ShowDialog(Me)
            XtraTabControl1_SelectedPageChanged()
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        Finally
            If Not frmCatatanKeperawatan Is Nothing Then frmCatatanKeperawatan.Dispose()
            frmCatatanKeperawatan = Nothing

            Dim rowHandle As Integer = grvCatatanKeperawatan.LocateByValue(rowHandle, grvCatatanKeperawatan.Columns("KDCATATANPERAWAT"), sCode)
            If rowHandle > 0 Then grvCatatanKeperawatan.FocusedRowHandle = rowHandle

            If sStatusSave = "NEW" Then
                sStatusSave = "NONE"
                picAddCatatanKeperawatan_Click()
            End If
        End Try
    End Sub
    Private Sub picpicDeleteCatatanKeperawatan_Click() Handles picDeleteCatatanKeperawatan.Click
        If grvCatatanKeperawatan.GetFocusedRowCellValue("KDCATATANPERAWAT") Is Nothing Then
            Exit Sub
        End If
        If MsgBox(Statement.DeleteQuestion, MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, Me.Text) = MsgBoxResult.No Then Exit Sub
        If fn_DeleteDataCatatanKeperawatan(grvCatatanKeperawatan.GetFocusedRowCellValue("KDCATATANPERAWAT")) = False Then
            MsgBox(Statement.DeleteFail, MsgBoxStyle.Exclamation, Me.Text)
            Exit Sub
        End If
        MsgBox(Statement.DeleteSuccess, MsgBoxStyle.Information, Me.Text)
        XtraTabControl1_SelectedPageChanged()
    End Sub
    Private Sub picRefreshCatatanKeperawatan_Click() Handles picRefreshCatatanKeperawatan.Click
        XtraTabControl1_SelectedPageChanged()
    End Sub
#End Region
#Region "GerdQ"
    Private Sub fn_LoadDataGerdQList()
        Try
            Dim ds = From x In oIGD_GerdQ.GetDataByRMRi(lblNoRM.Text)
                     Select x.KDGERD, x.KDPENDAFTARAN, TANGGAL = x.DATE, RUANGAN = x.RUANGRAWAT, x.KDUSER

            grdGerdQList.DataSource = ds.ToList

            fn_LoadFormatDataGerdQList()
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadFormatDataGerdQList()
        For iLoop As Integer = 0 To grvGerdQList.Columns.Count - 1
            If grvGerdQList.Columns(iLoop).ColumnType.Name = "Decimal" Then
                grvGerdQList.Columns(iLoop).DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                grvGerdQList.Columns(iLoop).DisplayFormat.FormatString = "{0:n2}"
                grvGerdQList.Columns(iLoop).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
            ElseIf grvGerdQList.Columns(iLoop).ColumnType.Name = "DateTime" Then
                grvGerdQList.Columns(iLoop).DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
                grvGerdQList.Columns(iLoop).DisplayFormat.FormatString = "{0:dd/MM/yyyy}"
            End If
        Next

        grvGerdQList.Columns("KDGERD").VisibleIndex = -1
        grvGerdQList.Columns("KDPENDAFTARAN").VisibleIndex = -1
    End Sub
    Private Function fn_DeleteDataGerdQ(ByVal sKDGerdQ As String) As Boolean
        Try
            oIGD_GerdQ.DeleteData(sKDGerdQ)

            fn_DeleteDataGerdQ = True
        Catch oErr As Exception
            fn_DeleteDataGerdQ = False
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Function
    Private Sub grvGerdQList_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles grvGerdQList.DoubleClick
        If grvGerdQList.GetFocusedRowCellValue("KDGERD") Is Nothing Then
            Exit Sub
        End If

        Dim frmDigital_IGD_01_GERD As New frmDigital_IGD_01_GERD
        Try
            frmDigital_IGD_01_GERD.LoadMe(FORM_MODE.FORM_MODE_EDIT, lblRegister.Text, lblNamaPasien.Text, lblNoRM.Text, grvGerdQList.GetFocusedRowCellValue("KDGERD"))
            frmDigital_IGD_01_GERD.ShowDialog(Me)
        Catch ex As Exception
            MsgBox("Load Form Detail : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
        Finally
            If Not frmDigital_IGD_01_GERD Is Nothing Then frmDigital_IGD_01_GERD.Dispose()
            frmDigital_IGD_01_GERD = Nothing
        End Try
        'Dim frmGerdQ As New frmGerdQ
        'Try
        '    frmGerdQ.LoadMe(FORM_MODE.FORM_MODE_VIEW, "", grvGerdQList.GetFocusedRowCellValue("KDPENDAFTARAN"), lblNoRM.Text, lblNamaPasien.Text, sTanggalLahir, sUMURPASIEN, lblJenisKelamin.Text, "-", grvGerdQList.GetFocusedRowCellValue("RUANGAN"), sKDDOCTOR_INPUT, lblKartuBPJS.Text, grvGerdQList.GetFocusedRowCellValue("KDGERD"))
        '    frmGerdQ.ShowDialog(Me)
        'Catch oErr As Exception
        '    MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        'End Try
    End Sub
    Private Sub picAddGerQ_Click() Handles picAddGerQ.Click
        Dim frmDigital_IGD_01_GERD As New frmDigital_IGD_01_GERD
        Try
            frmDigital_IGD_01_GERD.LoadMe(FORM_MODE.FORM_MODE_ADD, lblRegister.Text, lblNamaPasien.Text, lblNoRM.Text)
            frmDigital_IGD_01_GERD.ShowDialog(Me)
            XtraTabControl1_SelectedPageChanged()
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        Finally
            If Not frmDigital_IGD_01_GERD Is Nothing Then frmDigital_IGD_01_GERD.Dispose()
            frmDigital_IGD_01_GERD = Nothing

            Dim rowHandle As Integer = grvGerdQList.LocateByValue(rowHandle, grvGerdQList.Columns("KDGERD"), sCode)
            If rowHandle > 0 Then grvGerdQList.FocusedRowHandle = rowHandle

            If sStatusSave = "NEW" Then
                sStatusSave = "NONE"
            End If
        End Try
    End Sub
    Private Sub picEditGerQ_Click() Handles picEditGerQ.Click
        If grvGerdQList.GetFocusedRowCellValue("KDGERD") Is Nothing Then
            Exit Sub
        End If
        Dim frmDigital_IGD_01_GERD As New frmDigital_IGD_01_GERD
        Try
            frmDigital_IGD_01_GERD.LoadMe(FORM_MODE.FORM_MODE_EDIT, lblRegister.Text, lblNamaPasien.Text, lblNoRM.Text, grvGerdQList.GetFocusedRowCellValue("KDGERD"))
            frmDigital_IGD_01_GERD.ShowDialog(Me)
            XtraTabControl1_SelectedPageChanged()
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        Finally
            If Not frmDigital_IGD_01_GERD Is Nothing Then frmDigital_IGD_01_GERD.Dispose()
            frmDigital_IGD_01_GERD = Nothing

            Dim rowHandle As Integer = grvGerdQList.LocateByValue(rowHandle, grvGerdQList.Columns("KDGERD"), sCode)
            If rowHandle > 0 Then grvGerdQList.FocusedRowHandle = rowHandle

            If sStatusSave = "NEW" Then
                sStatusSave = "NONE"
                picAddGerQ_Click()
            End If
        End Try
    End Sub
    Private Sub picDeleteGerQ_Click() Handles picDeleteGerQ.Click
        If grvGerdQList.GetFocusedRowCellValue("KDGERD") Is Nothing Then
            Exit Sub
        End If
        If MsgBox(Statement.DeleteQuestion, MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, Me.Text) = MsgBoxResult.No Then Exit Sub
        If fn_DeleteDataGerdQ(grvGerdQList.GetFocusedRowCellValue("KDGERD")) = False Then
            MsgBox(Statement.DeleteFail, MsgBoxStyle.Exclamation, Me.Text)
            Exit Sub
        End If
        MsgBox(Statement.DeleteSuccess, MsgBoxStyle.Information, Me.Text)
        XtraTabControl1_SelectedPageChanged()
    End Sub
    Private Sub picRefreshGerQ_Click() Handles picRefreshGerQ.Click
        XtraTabControl1_SelectedPageChanged()
    End Sub
#End Region
#Region "Catatan Skrining dan Edukasi Gizi"
    Private Sub fn_LoadDataCatatanSkriningdanEdukasiGiziList()
        Try
            Dim ds = From x In oCatatanSkriningdanEdukasiGizi.GetDataByKDREG(lblRegister.Text)
                     Select x.KDREG, TANGGAL = x.DATE, RUANGAN = x.RUANGRAWAT2, x.KDUSER

            grdCatatanSkriningdanEdukasiGizi.DataSource = ds.ToList

            fn_LoadFormatDataCatatanSkriningdanEdukasiGiziList()
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadFormatDataCatatanSkriningdanEdukasiGiziList()
        For iLoop As Integer = 0 To grvCatatanSkriningdanEdukasiGizi.Columns.Count - 1
            If grvCatatanSkriningdanEdukasiGizi.Columns(iLoop).ColumnType.Name = "Decimal" Then
                grvCatatanSkriningdanEdukasiGizi.Columns(iLoop).DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                grvCatatanSkriningdanEdukasiGizi.Columns(iLoop).DisplayFormat.FormatString = "{0:n2}"
                grvCatatanSkriningdanEdukasiGizi.Columns(iLoop).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
            ElseIf grvCatatanSkriningdanEdukasiGizi.Columns(iLoop).ColumnType.Name = "DateTime" Then
                grvCatatanSkriningdanEdukasiGizi.Columns(iLoop).DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
                grvCatatanSkriningdanEdukasiGizi.Columns(iLoop).DisplayFormat.FormatString = "{0:dd/MM/yyyy}"
            End If
        Next

        grvCatatanSkriningdanEdukasiGizi.Columns("KDREG").VisibleIndex = -1
    End Sub
    Private Function fn_DeleteDataCatatanSkriningdanEdukasiGizi(ByVal sKDCatatanSkriningdanEdukasiGizi As String) As Boolean
        Try
            oCatatanSkriningdanEdukasiGizi.DeleteData(sKDCatatanSkriningdanEdukasiGizi)

            fn_DeleteDataCatatanSkriningdanEdukasiGizi = True
        Catch oErr As Exception
            fn_DeleteDataCatatanSkriningdanEdukasiGizi = False
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Function
    Private Sub grvCatatanSkriningdanEdukasiGiziList_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles grvCatatanSkriningdanEdukasiGizi.DoubleClick
        If grvCatatanSkriningdanEdukasiGizi.GetFocusedRowCellValue("KDREG") Is Nothing Then
            Exit Sub
        End If
        Dim frmEMedrekRI_20 As New frmEMedrekRI_20
        Try
            frmEMedrekRI_20.LoadMe(FORM_MODE.FORM_MODE_EDIT, grvCatatanSkriningdanEdukasiGizi.GetFocusedRowCellValue("KDREG"), lblNoRM.Text, lblNamaPasien.Text, lblTanggalLahir.Text, sKDUSER_PERAWAT, sKDDOCTOR_DPJPUTAMA, grvCatatanSkriningdanEdukasiGizi.GetFocusedRowCellValue("RUANGAN"))
            frmEMedrekRI_20.ShowDialog(Me)
        Catch ex As Exception
            MsgBox("Load Form Detail : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
        Finally
            If Not frmEMedrekRI_20 Is Nothing Then frmEMedrekRI_20.Dispose()
            frmEMedrekRI_20 = Nothing
        End Try
    End Sub
    Private Sub picAddCatatanSkriningdanEdukasiGizi_Click() Handles picAddCatatanSkriningdanEdukasiGizi.Click
        If lblRegister.Text = "" Then Exit Sub

        Dim oS_DIGITAL_RI_20 As New Digital.clsDigital_RI_20
        Dim dsCek = oS_DIGITAL_RI_20.GetData(lblRegister.Text)

        If dsCek IsNot Nothing Then
            Dim frmEMedrekRI_20 As New frmEMedrekRI_20
            Try
                frmEMedrekRI_20.LoadMe(FORM_MODE.FORM_MODE_EDIT, dsCek.KDREG, lblNoRM.Text, lblNamaPasien.Text, lblTanggalLahir.Text, sKDUSER_PERAWAT, sKDDOCTOR_DPJPUTAMA, lblRuangan.Text)
                frmEMedrekRI_20.ShowDialog(Me)
                XtraTabControl1_SelectedPageChanged()
            Catch oErr As Exception
                MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            Finally
                If Not frmEMedrekRI_20 Is Nothing Then frmEMedrekRI_20.Dispose()
                frmEMedrekRI_20 = Nothing
            End Try
        Else
            Dim frmEMedrekRI_20 As New frmEMedrekRI_20
            Try
                frmEMedrekRI_20.LoadMe(FORM_MODE.FORM_MODE_ADD, lblRegister.Text, lblNoRM.Text, lblNamaPasien.Text, lblTanggalLahir.Text, sKDUSER_PERAWAT, sKDDOCTOR_DPJPUTAMA, lblRuangan.Text)
                frmEMedrekRI_20.ShowDialog(Me)
                XtraTabControl1_SelectedPageChanged()
            Catch oErr As Exception
                MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            Finally
                If Not frmEMedrekRI_20 Is Nothing Then frmEMedrekRI_20.Dispose()
                frmEMedrekRI_20 = Nothing
            End Try
        End If
    End Sub
    Private Sub picpicUpdateCatatanSkriningdanEdukasiGizi_Click() Handles picUpdateCatatanSkriningdanEdukasiGizi.Click
        If grvCatatanSkriningdanEdukasiGizi.GetFocusedRowCellValue("KDREG") Is Nothing Then
            Exit Sub
        End If

        Dim frmEMedrekRI_20 As New frmEMedrekRI_20
        Try
            frmEMedrekRI_20.LoadMe(FORM_MODE.FORM_MODE_EDIT, grvCatatanSkriningdanEdukasiGizi.GetFocusedRowCellValue("KDREG"), lblNoRM.Text, lblNamaPasien.Text, lblTanggalLahir.Text, sKDUSER_PERAWAT, sKDDOCTOR_DPJPUTAMA, grvCatatanSkriningdanEdukasiGizi.GetFocusedRowCellValue("RUANGAN"))
            frmEMedrekRI_20.ShowDialog(Me)
            XtraTabControl1_SelectedPageChanged()
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        Finally
            If Not frmEMedrekRI_20 Is Nothing Then frmEMedrekRI_20.Dispose()
            frmEMedrekRI_20 = Nothing
        End Try
    End Sub
    Private Sub picpicDeleteCatatanSkriningdanEdukasiGizi_Click() Handles picDeleteCatatanSkriningdanEdukasiGizi.Click
        If grvCatatanSkriningdanEdukasiGizi.GetFocusedRowCellValue("KDREG") Is Nothing Then
            Exit Sub
        End If

        If MsgBox(Statement.DeleteQuestion, MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, Me.Text) = MsgBoxResult.No Then Exit Sub
        If fn_DeleteDataCatatanSkriningdanEdukasiGizi(grvCatatanSkriningdanEdukasiGizi.GetFocusedRowCellValue("KDREG")) = False Then
            MsgBox(Statement.DeleteFail, MsgBoxStyle.Exclamation, Me.Text)
            Exit Sub
        End If
        MsgBox(Statement.DeleteSuccess, MsgBoxStyle.Information, Me.Text)
        XtraTabControl1_SelectedPageChanged()
    End Sub
    Private Sub picRefreshCatatanSkriningdanEdukasiGizi_Click() Handles picRefreshCatatanSkriningdanEdukasiGizi.Click
        XtraTabControl1_SelectedPageChanged()
    End Sub
#End Region
#Region "Asesmen Lanjut Gizi"
    Private Sub fn_LoadDataAsesmenLanjutGiziList()
        Try
            Dim ds = From x In oAsesmenLanjutGizi.GetDataByKDREG(lblRegister.Text)
                     Select x.KDREG, TANGGAL = x.DATE, x.KDUSER

            grdAsesmenLanjutGizi.DataSource = ds.ToList

            fn_LoadFormatDataAsesmenLanjutGiziList()
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadFormatDataAsesmenLanjutGiziList()
        For iLoop As Integer = 0 To grvAsesmenLanjutGizi.Columns.Count - 1
            If grvAsesmenLanjutGizi.Columns(iLoop).ColumnType.Name = "Decimal" Then
                grvAsesmenLanjutGizi.Columns(iLoop).DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                grvAsesmenLanjutGizi.Columns(iLoop).DisplayFormat.FormatString = "{0:n2}"
                grvAsesmenLanjutGizi.Columns(iLoop).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
            ElseIf grvAsesmenLanjutGizi.Columns(iLoop).ColumnType.Name = "DateTime" Then
                grvAsesmenLanjutGizi.Columns(iLoop).DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
                grvAsesmenLanjutGizi.Columns(iLoop).DisplayFormat.FormatString = "{0:dd/MM/yyyy}"
            End If
        Next

        grvAsesmenLanjutGizi.Columns("KDREG").VisibleIndex = -1
    End Sub
    Private Function fn_DeleteDataAsesmenLanjutGizi(ByVal sKDAsesmenLanjutGizi As String) As Boolean
        Try
            oAsesmenLanjutGizi.DeleteData(sKDAsesmenLanjutGizi)

            fn_DeleteDataAsesmenLanjutGizi = True
        Catch oErr As Exception
            fn_DeleteDataAsesmenLanjutGizi = False
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Function
    Private Sub grvAsesmenLanjutGiziList_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles grvAsesmenLanjutGizi.DoubleClick
        If grvAsesmenLanjutGizi.GetFocusedRowCellValue("KDREG") Is Nothing Then
            Exit Sub
        End If

        Dim frmAsesmenGiziLanjut As New frmAsesmenGiziLanjut
        Try
            frmAsesmenGiziLanjut.LoadMe(FORM_MODE.FORM_MODE_EDIT, grvAsesmenLanjutGizi.GetFocusedRowCellValue("KDREG"), lblNoRM.Text, lblNamaPasien.Text, sUMURPASIEN, sKDUSER_PERAWAT, lblJenisKelamin.Text, "")
            frmAsesmenGiziLanjut.ShowDialog(Me)
        Catch ex As Exception
            MsgBox("Load Form Detail : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
        Finally
            If Not frmAsesmenGiziLanjut Is Nothing Then frmAsesmenGiziLanjut.Dispose()
            frmAsesmenGiziLanjut = Nothing
        End Try
    End Sub
    Private Sub picAddAsesmenLanjutGizi_Click() Handles picAddAsesmenLanjutGizi.Click
        Dim frmAsesmenGiziLanjut As New frmAsesmenGiziLanjut
        Try
            frmAsesmenGiziLanjut.LoadMe(FORM_MODE.FORM_MODE_ADD, lblRegister.Text, lblNoRM.Text, lblNamaPasien.Text, sUMURPASIEN, sKDUSER_PERAWAT, lblJenisKelamin.Text, lblRuangan.Text)
            frmAsesmenGiziLanjut.ShowDialog(Me)
            XtraTabControl1_SelectedPageChanged()
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        Finally
            If Not frmAsesmenGiziLanjut Is Nothing Then frmAsesmenGiziLanjut.Dispose()
            frmAsesmenGiziLanjut = Nothing
        End Try
    End Sub
    Private Sub picpicUpdateAsesmenLanjutGizi_Click() Handles picUpdateAsesmenLanjutGizi.Click
        If grvAsesmenLanjutGizi.GetFocusedRowCellValue("KDREG") Is Nothing Then
            Exit Sub
        End If
        Dim frmAsesmenGiziLanjut As New frmAsesmenGiziLanjut
        Try
            frmAsesmenGiziLanjut.LoadMe(FORM_MODE.FORM_MODE_EDIT, grvAsesmenLanjutGizi.GetFocusedRowCellValue("KDREG"), lblNoRM.Text, lblNamaPasien.Text, sUMURPASIEN, sKDUSER_PERAWAT, lblJenisKelamin.Text, "")
            frmAsesmenGiziLanjut.ShowDialog(Me)
            XtraTabControl1_SelectedPageChanged()
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        Finally
            If Not frmAsesmenGiziLanjut Is Nothing Then frmAsesmenGiziLanjut.Dispose()
            frmAsesmenGiziLanjut = Nothing
        End Try
    End Sub
    Private Sub picpicDeleteAsesmenLanjutGizi_Click() Handles picDeleteAsesmenLanjutGizi.Click
        If grvAsesmenLanjutGizi.GetFocusedRowCellValue("KDREG") Is Nothing Then
            Exit Sub
        End If

        If MsgBox(Statement.DeleteQuestion, MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, Me.Text) = MsgBoxResult.No Then Exit Sub
        If fn_DeleteDataAsesmenLanjutGizi(grvAsesmenLanjutGizi.GetFocusedRowCellValue("KDREG")) = False Then
            MsgBox(Statement.DeleteFail, MsgBoxStyle.Exclamation, Me.Text)
            Exit Sub
        End If
        MsgBox(Statement.DeleteSuccess, MsgBoxStyle.Information, Me.Text)
        XtraTabControl1_SelectedPageChanged()
    End Sub
    Private Sub picRefreshAsesmenLanjutGizi_Click() Handles picRefreshAsesmenLanjutGizi.Click
        XtraTabControl1_SelectedPageChanged()
    End Sub
#End Region
#Region "Monitoring Evaluasi Gizi"
    Private Sub fn_LoadDataMonitoringEvaluasiGiziList()
        Try
            Dim ds = From x In oMonitoringEvaluasiGizi.GetDataByKDREG(lblRegister.Text)
                     Select x.KDMONEVG, x.KDREG, TANGGAL = x.DATE, x.DIAGNOSA

            grdMonitoringEvaluasiGizi.DataSource = ds.ToList

            fn_LoadFormatDataMonitoringEvaluasiGiziList()
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadFormatDataMonitoringEvaluasiGiziList()
        For iLoop As Integer = 0 To grvMonitoringEvaluasiGizi.Columns.Count - 1
            If grvMonitoringEvaluasiGizi.Columns(iLoop).ColumnType.Name = "Decimal" Then
                grvMonitoringEvaluasiGizi.Columns(iLoop).DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                grvMonitoringEvaluasiGizi.Columns(iLoop).DisplayFormat.FormatString = "{0:n2}"
                grvMonitoringEvaluasiGizi.Columns(iLoop).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
            ElseIf grvMonitoringEvaluasiGizi.Columns(iLoop).ColumnType.Name = "DateTime" Then
                grvMonitoringEvaluasiGizi.Columns(iLoop).DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
                grvMonitoringEvaluasiGizi.Columns(iLoop).DisplayFormat.FormatString = "{0:dd/MM/yyyy}"
            End If
        Next

        grvMonitoringEvaluasiGizi.Columns("KDMONEVG").VisibleIndex = -1
        grvMonitoringEvaluasiGizi.Columns("KDREG").VisibleIndex = -1
    End Sub
    Private Function fn_DeleteDataMonitoringEvaluasiGizi(ByVal sKDMonitoringEvaluasiGizi As String) As Boolean
        Try
            oMonitoringEvaluasiGizi.DeleteData(sKDMonitoringEvaluasiGizi)

            fn_DeleteDataMonitoringEvaluasiGizi = True
        Catch oErr As Exception
            fn_DeleteDataMonitoringEvaluasiGizi = False
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Function
    Private Sub grvMonitoringEvaluasiGiziList_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles grvMonitoringEvaluasiGizi.DoubleClick
        If grvMonitoringEvaluasiGizi.GetFocusedRowCellValue("KDMONEVG") Is Nothing Then
            Exit Sub
        End If

        Dim frmMonitoringEvaluasiGizi As New frmMonitoringEvaluasiGizi
        Try
            frmMonitoringEvaluasiGizi.LoadMe(FORM_MODE.FORM_MODE_EDIT, grvMonitoringEvaluasiGizi.GetFocusedRowCellValue("KDREG"), lblNoRM.Text, lblNamaPasien.Text, sUMURPASIEN, sKDUSER_PERAWAT, grvMonitoringEvaluasiGizi.GetFocusedRowCellValue("KDMONEVG"))
            frmMonitoringEvaluasiGizi.ShowDialog(Me)
        Catch ex As Exception
            MsgBox("Load Form Detail : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
        Finally
            If Not frmMonitoringEvaluasiGizi Is Nothing Then frmMonitoringEvaluasiGizi.Dispose()
            frmMonitoringEvaluasiGizi = Nothing
        End Try
    End Sub
    Private Sub picAddMonitoringEvaluasiGizi_Click() Handles picAddMonitoringEvaluasiGizi.Click
        Dim frmMonitoringEvaluasiGizi As New frmMonitoringEvaluasiGizi
        Try
            frmMonitoringEvaluasiGizi.LoadMe(FORM_MODE.FORM_MODE_ADD, lblRegister.Text, lblNoRM.Text, lblNamaPasien.Text, sUMURPASIEN, sKDUSER_PERAWAT, "")
            frmMonitoringEvaluasiGizi.ShowDialog(Me)
            XtraTabControl1_SelectedPageChanged()
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        Finally
            If Not frmMonitoringEvaluasiGizi Is Nothing Then frmMonitoringEvaluasiGizi.Dispose()
            frmMonitoringEvaluasiGizi = Nothing
        End Try
    End Sub
    Private Sub picpicUpdateMonitoringEvaluasiGizi_Click() Handles picUpdateMonitoringEvaluasiGizi.Click
        If grvMonitoringEvaluasiGizi.GetFocusedRowCellValue("KDMONEVG") Is Nothing Then
            Exit Sub
        End If
        Dim frmMonitoringEvaluasiGizi As New frmMonitoringEvaluasiGizi
        Try
            frmMonitoringEvaluasiGizi.LoadMe(FORM_MODE.FORM_MODE_EDIT, grvMonitoringEvaluasiGizi.GetFocusedRowCellValue("KDREG"), lblNoRM.Text, lblNamaPasien.Text, sUMURPASIEN, sKDUSER_PERAWAT, grvMonitoringEvaluasiGizi.GetFocusedRowCellValue("KDMONEVG"))
            frmMonitoringEvaluasiGizi.ShowDialog(Me)
            XtraTabControl1_SelectedPageChanged()
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        Finally
            If Not frmMonitoringEvaluasiGizi Is Nothing Then frmMonitoringEvaluasiGizi.Dispose()
            frmMonitoringEvaluasiGizi = Nothing
        End Try
    End Sub
    Private Sub picpicDeleteMonitoringEvaluasiGizi_Click() Handles picDeleteMonitoringEvaluasiGizi.Click
        If grvMonitoringEvaluasiGizi.GetFocusedRowCellValue("KDMONEVG") Is Nothing Then
            Exit Sub
        End If

        If MsgBox(Statement.DeleteQuestion, MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, Me.Text) = MsgBoxResult.No Then Exit Sub
        If fn_DeleteDataMonitoringEvaluasiGizi(grvMonitoringEvaluasiGizi.GetFocusedRowCellValue("KDMONEVG")) = False Then
            MsgBox(Statement.DeleteFail, MsgBoxStyle.Exclamation, Me.Text)
            Exit Sub
        End If
        MsgBox(Statement.DeleteSuccess, MsgBoxStyle.Information, Me.Text)
        XtraTabControl1_SelectedPageChanged()
    End Sub
    Private Sub picRefreshMonitoringEvaluasiGizi_Click() Handles picRefreshMonitoringEvaluasiGizi.Click
        XtraTabControl1_SelectedPageChanged()
    End Sub
#End Region
#Region "Formulir MPP"
    Private Sub fn_LoadDataFormulirMPPList()
        Try
            grvFormulirMPP.OptionsSelection.MultiSelect = True
            grvFormulirMPP.SelectAll()
            grvFormulirMPP.DeleteSelectedRows()
            grvFormulirMPP.OptionsSelection.MultiSelect = False

            If lblNoRM.Text = "" Then Exit Sub

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

            SQL = "EXEC FORMULRMPP @KDCUSTOMER = '" & lblNoRM.Text & "' "

            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "FOMRULIRMPP")

            grdFormulirMPP.DataSource = ds.Tables("FOMRULIRMPP")
            grdFormulirMPP.ForceInitialize()

            If oConn.State = ConnectionState.Open Then
                oConn.Close()
            End If

            fn_LoadFormatDataFormulirMPPList()
        Catch oErr As Exception
            MsgBox("Load Formulir MPP : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadFormatDataFormulirMPPList()
        For iLoop As Integer = 0 To grvFormulirMPP.Columns.Count - 1
            If grvFormulirMPP.Columns(iLoop).ColumnType.Name = "Decimal" Then
                grvFormulirMPP.Columns(iLoop).DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                grvFormulirMPP.Columns(iLoop).DisplayFormat.FormatString = "{0:n2}"
                grvFormulirMPP.Columns(iLoop).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
            ElseIf grvFormulirMPP.Columns(iLoop).ColumnType.Name = "DateTime" Then
                grvFormulirMPP.Columns(iLoop).DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
                grvFormulirMPP.Columns(iLoop).DisplayFormat.FormatString = "{0:dd/MM/yyyy}"
            End If
        Next

        grvFormulirMPP.Columns("KODE").VisibleIndex = -1
        'grvFormulirMPP.Columns("KDPENDAFTARAN").VisibleIndex = -1
    End Sub
    Private Sub fn_LoadDataFormulirMPPListViewer()
        Try
            grvFormulirMPP_Viewer.OptionsSelection.MultiSelect = True
            grvFormulirMPP_Viewer.SelectAll()
            grvFormulirMPP_Viewer.DeleteSelectedRows()
            grvFormulirMPP_Viewer.OptionsSelection.MultiSelect = False

            If lblNoRM.Text = "" Then Exit Sub

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

            SQL = "EXEC FORMULRMPP @KDCUSTOMER = '" & lblNoRM.Text & "' "

            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "FOMRULIRMPP")

            grdFormulirMPP_Viewer.DataSource = ds.Tables("FOMRULIRMPP")
            grdFormulirMPP_Viewer.ForceInitialize()

            If oConn.State = ConnectionState.Open Then
                oConn.Close()
            End If

            grvFormulirMPP_Viewer.Columns("KODE").Visible = False
            'grvFormulirMPP_Viewer.Columns("SEQ").Visible = False

            For iLoop As Integer = 0 To grvFormulirMPP_Viewer.Columns.Count - 1
                If grvFormulirMPP_Viewer.Columns(iLoop).ColumnType.Name = "Decimal" Then
                    grvFormulirMPP_Viewer.Columns(iLoop).DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                    grvFormulirMPP_Viewer.Columns(iLoop).DisplayFormat.FormatString = "{0:n2}"
                    grvFormulirMPP_Viewer.Columns(iLoop).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
                ElseIf grvFormulirMPP_Viewer.Columns(iLoop).ColumnType.Name = "DateTime" Then
                    grvFormulirMPP_Viewer.Columns(iLoop).DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
                    grvFormulirMPP_Viewer.Columns(iLoop).DisplayFormat.FormatString = "{0:dd/MM/yyyy HH:mm}"
                End If
            Next
        Catch oErr As Exception
            MsgBox("Load Formulir MPP : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Function fn_DeleteDataFormulirMPP(ByVal sKDGerdQ As String, ByVal KATEGORI As String) As Boolean
        Try
            If KATEGORI = "MPPA" Then
                Dim oDigitalMPP As New Digital.clsDigital_MPPA

                oDigitalMPP.DeleteData(sKDGerdQ)
            Else
                Dim oDigitalMPP As New Digital.clsDigital_MPPB

                oDigitalMPP.DeleteData(sKDGerdQ)
            End If

            fn_DeleteDataFormulirMPP = True
        Catch oErr As Exception
            fn_DeleteDataFormulirMPP = False
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Function
    Private Sub grvFormulirMPP_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles grvFormulirMPP.DoubleClick
        If grvFormulirMPP.GetFocusedRowCellValue("KODE") Is Nothing Then
            Exit Sub
        End If

        If grvFormulirMPP.GetFocusedRowCellValue("KATEGORI") = "MPPA" Then
            Dim frmDigital_MPPA As New frmDigital_MPPA
            Try
                frmDigital_MPPA.LoadMe(FORM_MODE.FORM_MODE_VIEW, grvFormulirMPP.GetFocusedRowCellValue("KDPENDAFTARAN"), grvFormulirMPP.GetFocusedRowCellValue("RUANGAN"), lblNoRM.Text, lblNamaPasien.Text, lblJenisKelamin.Text, sTanggalLahir, grvFormulirMPP.GetFocusedRowCellValue("KODE"))
                frmDigital_MPPA.ShowDialog(Me)
            Catch ex As Exception
                MsgBox("Load Form Detail : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
            End Try
        Else
            Dim frmDigital_MPPB As New frmDigital_MPPB
            Try
                frmDigital_MPPB.LoadMe(FORM_MODE.FORM_MODE_VIEW, grvFormulirMPP.GetFocusedRowCellValue("KDPENDAFTARAN"), grvFormulirMPP.GetFocusedRowCellValue("RUANGAN"), lblNoRM.Text, lblNamaPasien.Text, lblJenisKelamin.Text, sTanggalLahir, grvFormulirMPP.GetFocusedRowCellValue("KODE"))
                frmDigital_MPPB.ShowDialog(Me)
            Catch ex As Exception
                MsgBox("Load Form Detail : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
            End Try
        End If

    End Sub
    Private Sub picAddFormulirMPP_Click() Handles picAddFormulirMPP.Click
        Dim frmDigital_MPPA As New frmDigital_MPPA
        Try
            frmDigital_MPPA.LoadMe(FORM_MODE.FORM_MODE_ADD, lblRegister.Text, lblRuangan.Text, lblNoRM.Text, lblNamaPasien.Text, lblJenisKelamin.Text, sTanggalLahir, "")
            frmDigital_MPPA.ShowDialog(Me)
            XtraTabControl1_SelectedPageChanged()
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        Finally
            If Not frmDigital_MPPA Is Nothing Then frmDigital_MPPA.Dispose()
            frmDigital_MPPA = Nothing

            Dim rowHandle As Integer = grvFormulirMPP.LocateByValue(rowHandle, grvFormulirMPP.Columns("KODE"), sCode)
            If rowHandle > 0 Then grvFormulirMPP.FocusedRowHandle = rowHandle

            If sStatusSave = "NEW" Then
                sStatusSave = "NONE"
            End If
        End Try
    End Sub
    Private Sub picAddMPPB_Click() Handles picAddMPPB.Click
        Dim frmDigital_MPPB As New frmDigital_MPPB
        Try
            frmDigital_MPPB.LoadMe(FORM_MODE.FORM_MODE_ADD, lblRegister.Text, lblRuangan.Text, lblNoRM.Text, lblNamaPasien.Text, lblJenisKelamin.Text, sTanggalLahir, "")
            frmDigital_MPPB.ShowDialog(Me)
            XtraTabControl1_SelectedPageChanged()
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        Finally
            If Not frmDigital_MPPB Is Nothing Then frmDigital_MPPB.Dispose()
            frmDigital_MPPB = Nothing

            Dim rowHandle As Integer = grvFormulirMPP.LocateByValue(rowHandle, grvFormulirMPP.Columns("KODE"), sCode)
            If rowHandle > 0 Then grvFormulirMPP.FocusedRowHandle = rowHandle

            If sStatusSave = "NEW" Then
                sStatusSave = "NONE"
            End If
        End Try
    End Sub
    Private Sub picUpdateFormulirMPP_Click() Handles picUpdateFormulirMPP.Click
        If grvFormulirMPP.GetFocusedRowCellValue("KODE") Is Nothing Then
            Exit Sub
        End If

        If grvFormulirMPP.GetFocusedRowCellValue("KATEGORI") = "MPPA" Then
            Dim frmDigital_MPPA As New frmDigital_MPPA
            Try
                frmDigital_MPPA.LoadMe(FORM_MODE.FORM_MODE_EDIT, grvFormulirMPP.GetFocusedRowCellValue("KDPENDAFTARAN"), grvFormulirMPP.GetFocusedRowCellValue("RUANGAN"), lblNoRM.Text, lblNamaPasien.Text, lblJenisKelamin.Text, sTanggalLahir, grvFormulirMPP.GetFocusedRowCellValue("KODE"))
                frmDigital_MPPA.ShowDialog(Me)
                XtraTabControl1_SelectedPageChanged()
            Catch oErr As Exception
                MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            Finally
                If Not frmDigital_MPPA Is Nothing Then frmDigital_MPPA.Dispose()
                frmDigital_MPPA = Nothing

                Dim rowHandle As Integer = grvFormulirMPP.LocateByValue(rowHandle, grvFormulirMPP.Columns("KODE"), sCode)
                If rowHandle > 0 Then grvFormulirMPP.FocusedRowHandle = rowHandle

                If sStatusSave = "NEW" Then
                    sStatusSave = "NONE"
                    picAddGerQ_Click()
                End If
            End Try
        Else
            Dim frmDigital_MPPB As New frmDigital_MPPB
            Try
                frmDigital_MPPB.LoadMe(FORM_MODE.FORM_MODE_EDIT, grvFormulirMPP.GetFocusedRowCellValue("KDPENDAFTARAN"), grvFormulirMPP.GetFocusedRowCellValue("RUANGAN"), lblNoRM.Text, lblNamaPasien.Text, lblJenisKelamin.Text, sTanggalLahir, grvFormulirMPP.GetFocusedRowCellValue("KODE"))
                frmDigital_MPPB.ShowDialog(Me)
                XtraTabControl1_SelectedPageChanged()
            Catch oErr As Exception
                MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            Finally
                If Not frmDigital_MPPB Is Nothing Then frmDigital_MPPB.Dispose()
                frmDigital_MPPB = Nothing

                Dim rowHandle As Integer = grvFormulirMPP.LocateByValue(rowHandle, grvFormulirMPP.Columns("KODE"), sCode)
                If rowHandle > 0 Then grvFormulirMPP.FocusedRowHandle = rowHandle

                If sStatusSave = "NEW" Then
                    sStatusSave = "NONE"
                    picAddGerQ_Click()
                End If
            End Try
        End If

    End Sub
    Private Sub picDeleteFormulirMPP_Click() Handles picDeleteFormulirMPP.Click
        If grvFormulirMPP.GetFocusedRowCellValue("KODE") Is Nothing Then
            Exit Sub
        End If
        If MsgBox(Statement.DeleteQuestion, MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, Me.Text) = MsgBoxResult.No Then Exit Sub
        If fn_DeleteDataFormulirMPP(grvFormulirMPP.GetFocusedRowCellValue("KODE"), grvFormulirMPP.GetFocusedRowCellValue("KATEGORI")) = False Then
            MsgBox(Statement.DeleteFail, MsgBoxStyle.Exclamation, Me.Text)
            Exit Sub
        End If
        MsgBox(Statement.DeleteSuccess, MsgBoxStyle.Information, Me.Text)
        XtraTabControl1_SelectedPageChanged()
    End Sub
    Private Sub picRefreshFormulirMPP_Click() Handles picRefreshFormulirMPP.Click
        XtraTabControl1_SelectedPageChanged()
    End Sub
#End Region
#Region "Resep"
    Private Sub fn_LoadDataResepList()
        Try
            Dim ds = From x In oReqRecipeRawatInap.GetDataDetailByKdpendaftaran(lblRegister.Text)
                     Select x.KDREQRECIPE_RI, x.SEQ, TANGGALRESEP = x.S_REQ_RECIPE_RI_H.DATE, x.NAMAOBAT, x.SIGNA, x.CARAPAKAI, x.QTY, CATATANDOKTER = x.REMARKS_DOKTER, PEMBERIANOBAT = x.REMARKS_FARMASI, KETERANGAN = x.S_REQ_RECIPE_RI_H.KONFIRMASIRESEP

            grdResep.DataSource = ds.ToList

            fn_LoadFormatResepList()
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadFormatResepList()
        For iLoop As Integer = 0 To grvResep.Columns.Count - 1
            If grvResep.Columns(iLoop).ColumnType.Name = "Decimal" Then
                grvResep.Columns(iLoop).DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                grvResep.Columns(iLoop).DisplayFormat.FormatString = "{0:n2}"
                grvResep.Columns(iLoop).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
            ElseIf grvResep.Columns(iLoop).ColumnType.Name = "DateTime" Then
                grvResep.Columns(iLoop).DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
                grvResep.Columns(iLoop).DisplayFormat.FormatString = "{0:dd/MM/yyyy}"
            End If
        Next

        grvResep.Columns("KDREQRECIPE_RI").VisibleIndex = -1
        grvResep.Columns("SEQ").VisibleIndex = -1

        grvResep.BestFitColumns()
    End Sub
    Private Sub fn_LoadDataRekonsiliasiObatdiKOP()
        Try
            Dim ds = From x In oS_DIGITAL_FARMASI_REKONSILIASIOBAT_H.GetDataByRM(lblNoRM.Text)
                     Select x.KDREKONSILIASI, x.KDPENDAFTARAN, TANGGAL = x.DATE, RUANGAN = x.KDUSER_SIGNATURE, x.KDUSER

            grdRekonsilasiObatdiKOP.DataSource = ds.ToList

            fn_LoadFormatDataRekonsiliasiObatdiKOP()
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadFormatDataRekonsiliasiObatdiKOP()
        For iLoop As Integer = 0 To grvRekonsilasiObatdiKOP.Columns.Count - 1
            If grvRekonsilasiObatdiKOP.Columns(iLoop).ColumnType.Name = "Decimal" Then
                grvRekonsilasiObatdiKOP.Columns(iLoop).DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                grvRekonsilasiObatdiKOP.Columns(iLoop).DisplayFormat.FormatString = "{0:n2}"
                grvRekonsilasiObatdiKOP.Columns(iLoop).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
            ElseIf grvRekonsilasiObatdiKOP.Columns(iLoop).ColumnType.Name = "DateTime" Then
                grvRekonsilasiObatdiKOP.Columns(iLoop).DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
                grvRekonsilasiObatdiKOP.Columns(iLoop).DisplayFormat.FormatString = "{0:dd/MM/yyyy}"
            End If
        Next

        grvRekonsilasiObatdiKOP.Columns("KDREKONSILIASI").VisibleIndex = -1
        grvRekonsilasiObatdiKOP.Columns("KDPENDAFTARAN").VisibleIndex = -1
    End Sub
    Private Function fn_DeleteDataResep(ByVal sKDREQRECIPE_RI As String) As Boolean
        Try
            oReqRecipeRawatInap.DeleteData(sKDREQRECIPE_RI)

            fn_DeleteDataResep = True
        Catch oErr As Exception
            fn_DeleteDataResep = False
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Function
    Private Sub grvResep_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles grvResep.DoubleClick
        If grvResep.GetFocusedRowCellValue("KDREQRECIPE_RI") Is Nothing Then
            Exit Sub
        End If

        Dim dsCek = oReqRecipeRawatInap.GetDataObatBySeq(grvResep.GetFocusedRowCellValue("KDREQRECIPE_RI"), grvResep.GetFocusedRowCellValue("SEQ"))
        If dsCek IsNot Nothing Then
            Dim sPEMBERIANOBAT As String = deWAKTUPEMBERIANRESEP.DateTime.ToString("HH:mm") & "-" & sKDUSER_PERAWAT
            If oReqRecipeRawatInap.UpdatePemberiObat(dsCek.KDREQRECIPE_RI, dsCek.SEQ, sPEMBERIANOBAT) = True Then
                fn_LoadDataResepList()
            End If
        Else
            MsgBox("Data Tidak ada", MsgBoxStyle.Exclamation, Me.Text)
        End If
        'Dim frmReqRecipeRawatInap As New frmReqRecipeRawatInap
        'Try
        '    frmReqRecipeRawatInap.LoadMe(FORM_MODE.FORM_MODE_EDIT, grvResep.GetFocusedRowCellValue("KDREQRECIPE_RI"))
        '    frmReqRecipeRawatInap.ShowDialog(Me)
        'Catch ex As Exception
        '    MsgBox("Load Form Detail : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
        'Finally
        '    If Not frmReqRecipeRawatInap Is Nothing Then frmReqRecipeRawatInap.Dispose()
        '    frmReqRecipeRawatInap = Nothing
        'End Try
    End Sub
    'Private Sub picAddResep_Click() Handles picAddResep.Click
    '    Dim frmReqRecipeRawatInap As New frmReqRecipeRawatInap
    '    Try
    '        frmReqRecipeRawatInap.LoadMe(FORM_MODE.FORM_MODE_ADD)
    '        frmReqRecipeRawatInap.ShowDialog(Me)
    '        XtraTabControl1_SelectedPageChanged()
    '    Catch oErr As Exception
    '        MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
    '    Finally
    '        If Not frmReqRecipeRawatInap Is Nothing Then frmReqRecipeRawatInap.Dispose()
    '        frmReqRecipeRawatInap = Nothing
    '    End Try
    'End Sub
    Private Sub picEditResep_Click() Handles picEditResep.Click
        'If grvResep.GetFocusedRowCellValue("KDREQRECIPE_RI") Is Nothing Then
        '    Exit Sub
        'End If

        'Dim ds = oReqRecipeRawatInap.GetData(grvResep.GetFocusedRowCellValue("KDREQRECIPE_RI"))

        'If ds IsNot Nothing Then
        '    If ds.KONFIRMASIRESEP = "Resep Belum Diterima" Then
        '        Dim frmReqRecipeRawatInap As New frmReqRecipeRawatInap
        '        Try
        '            frmReqRecipeRawatInap.LoadMe(FORM_MODE.FORM_MODE_EDIT, grvResep.GetFocusedRowCellValue("KDREQRECIPE_RI"))
        '            frmReqRecipeRawatInap.ShowDialog(Me)
        '            XtraTabControl1_SelectedPageChanged()
        '        Catch oErr As Exception
        '            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        '        Finally
        '            If Not frmReqRecipeRawatInap Is Nothing Then frmReqRecipeRawatInap.Dispose()
        '            frmReqRecipeRawatInap = Nothing
        '        End Try
        '    Else
        '        MsgBox("Resep " & ds.KONFIRMASIRESEP & " Silahkan hubungi bagian farmasi", MsgBoxStyle.Exclamation, Me.Text)
        '    End If
        'Else
        '    MsgBox("Resep Tidak Ada", MsgBoxStyle.Exclamation, Me.Text)
        'End If
    End Sub
    Private Sub picDeleteResep_Click() Handles picDeleteResep.Click
        'If grvResep.GetFocusedRowCellValue("KDREQRECIPE_RI") Is Nothing Then
        '    Exit Sub
        'End If

        'Dim ds = oReqRecipeRawatInap.GetData(grvResep.GetFocusedRowCellValue("KDREQRECIPE_RI"))

        'If ds IsNot Nothing Then
        '    If ds.KONFIRMASIRESEP = "Resep Belum Diterima" Then
        '        If MsgBox(Statement.DeleteQuestion, MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, Me.Text) = MsgBoxResult.No Then Exit Sub
        '        If fn_DeleteDataResep(grvResep.GetFocusedRowCellValue("KDREQRECIPE_RI")) = False Then
        '            MsgBox(Statement.DeleteFail, MsgBoxStyle.Exclamation, Me.Text)
        '            Exit Sub
        '        End If
        '        MsgBox(Statement.DeleteSuccess, MsgBoxStyle.Information, Me.Text)
        '        XtraTabControl1_SelectedPageChanged()
        '    Else
        '        MsgBox("Resep " & ds.KONFIRMASIRESEP & " Silahkan hubungi bagian farmasi", MsgBoxStyle.Exclamation, Me.Text)
        '    End If
        'Else
        '    MsgBox("Resep Tidak Ada", MsgBoxStyle.Exclamation, Me.Text)
        'End If
    End Sub
    Private Sub picRefreshResep_Click() Handles picRefreshResep.Click
        XtraTabControl1_SelectedPageChanged()
    End Sub
#End Region
#Region "Order"
#Region "Resep"
    Private Sub fn_LoadDataOrderResep()
        Try
            Dim ds = From x In oReqRecipeRawatInap.GetDataDetailByKdpendaftaranH(lblRegister.Text)
                     Select x.KDREQRECIPE_RI, TANGGALRESEP = x.DATE, [USER] = x.NOIDUSER, x.KONFIRMASIRESEP

            grdOrderResep.DataSource = ds.ToList

            fn_LoadFormatDataOrderResep()
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadFormatDataOrderResep()
        For iLoop As Integer = 0 To grvOrderResep.Columns.Count - 1
            If grvOrderResep.Columns(iLoop).ColumnType.Name = "Decimal" Then
                grvOrderResep.Columns(iLoop).DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                grvOrderResep.Columns(iLoop).DisplayFormat.FormatString = "{0:n2}"
                grvOrderResep.Columns(iLoop).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
            ElseIf grvOrderResep.Columns(iLoop).ColumnType.Name = "DateTime" Then
                grvOrderResep.Columns(iLoop).DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
                grvOrderResep.Columns(iLoop).DisplayFormat.FormatString = "{0:dd/MM/yyyy}"
            End If
        Next

        grvOrderResep.Columns("KDREQRECIPE_RI").VisibleIndex = -1
    End Sub
    Private Function fn_DeleteDataOrderResep(ByVal sKDOrderResep As String) As Boolean
        Try
            Dim ds = oReqRecipeRawatInap.GetData(sKDOrderResep)
            If ds IsNot Nothing Then
                If ds.KONFIRMASIRESEP <> "Resep Belum Diterima" Then
                    fn_DeleteDataOrderResep = False
                    MsgBox("Resep sudah " & ds.KONFIRMASIRESEP & " Silahkan hubungi bagian farmasi", MsgBoxStyle.Exclamation, Me.Text)
                Else
                    oReqRecipeRawatInap.DeleteData(sKDOrderResep)
                    fn_DeleteDataOrderResep = True
                End If
            Else
                fn_DeleteDataOrderResep = False
                MsgBox("Resep Tidak di Temukan", MsgBoxStyle.Exclamation, Me.Text)
            End If
        Catch oErr As Exception
            fn_DeleteDataOrderResep = False
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Function
    Private Sub grvOrderResep_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles grvOrderResep.DoubleClick
        If grvOrderResep.GetFocusedRowCellValue("KDREQRECIPE_RI") Is Nothing Then
            Exit Sub
        End If

        Dim frmReqRecipeRawatInap As New frmReqRecipeRawatInap
        Try
            Dim listdiagnosa As New List(Of String)
            For i As Integer = 0 To grvDIAGNOSA_CPPT.RowCount - 2
                listdiagnosa.Add(grvDIAGNOSA_CPPT.GetRowCellValue(i, colNAMADIAGNOSA_CPPT))
            Next

            Dim Diagnosalabel As String = String.Join(vbCrLf, listdiagnosa.ToArray)

            frmReqRecipeRawatInap.fn_LoaDataPasien(False, sISDOKTER, lblPenjamin.Text, lblRegister.Text, lblNoRM.Text, lblNamaPasien.Text, IIf(sISDOKTER = False, sKDDOCTOR_DPJPUTAMA, sKDDOCTOR_INPUT), lblRuangan.Text, Diagnosalabel, txtOBJEKTIF_BERATBADAN.Text, IIf(sISDOKTER = True, sUserID, sKDUSER_PERAWAT))
            frmReqRecipeRawatInap.LoadMe(FORM_MODE.FORM_MODE_VIEW, grvOrderResep.GetFocusedRowCellValue("KDREQRECIPE_RI"))
            frmReqRecipeRawatInap.ShowDialog(Me)
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        Finally
            If Not frmReqRecipeRawatInap Is Nothing Then frmReqRecipeRawatInap.Dispose()
            frmReqRecipeRawatInap = Nothing
        End Try
    End Sub
    Private Sub picAddOrderResep_Click() Handles picAddOrderResep.Click
        Dim frmReqRecipeRawatInap As New frmReqRecipeRawatInap
        Try
            Dim listdiagnosa As New List(Of String)
            For i As Integer = 0 To grvDIAGNOSA_CPPT.RowCount - 2
                listdiagnosa.Add(grvDIAGNOSA_CPPT.GetRowCellValue(i, colNAMADIAGNOSA_CPPT))
            Next

            Dim Diagnosalabel As String = String.Join(vbCrLf, listdiagnosa.ToArray)

            frmReqRecipeRawatInap.fn_LoaDataPasien(False, sISDOKTER, lblPenjamin.Text, lblRegister.Text, lblNoRM.Text, lblNamaPasien.Text, IIf(sISDOKTER = False, sKDDOCTOR_DPJPUTAMA, sKDDOCTOR_INPUT), lblRuangan.Text, Diagnosalabel, txtOBJEKTIF_BERATBADAN.Text, IIf(sISDOKTER = True, sUserID, sKDUSER_PERAWAT))
            frmReqRecipeRawatInap.LoadMe(FORM_MODE.FORM_MODE_ADD)
            frmReqRecipeRawatInap.ShowDialog(Me)

            tabOrderList_SelectedPageChanged()
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        Finally
            If Not frmReqRecipeRawatInap Is Nothing Then frmReqRecipeRawatInap.Dispose()
            frmReqRecipeRawatInap = Nothing

        End Try
    End Sub
    Private Sub picAddPulangOrderResep_Click() Handles picAddPulangOrderResep.Click
        Try
            Dim kode As String = fn_LoadResepBykddoctor(lblRegister.Text, sKDDOCTOR_INPUT, True, True)

            If kode = "" Then
                kode = fn_LoadResepBykddoctor(lblRegister.Text, sKDDOCTOR_DPJPUTAMA, True, True)
            End If

            If kode = "" Then
                Dim frmReqRecipeRawatInap As New frmReqRecipeRawatInap
                Try
                    Dim listdiagnosa As New List(Of String)
                    For i As Integer = 0 To grvDIAGNOSA_CPPT.RowCount - 2
                        listdiagnosa.Add(grvDIAGNOSA_CPPT.GetRowCellValue(i, colNAMADIAGNOSA_CPPT))
                    Next

                    Dim Diagnosalabel As String = String.Join(vbCrLf, listdiagnosa.ToArray)

                    frmReqRecipeRawatInap.fn_LoaDataPasien(True, sISDOKTER, lblPenjamin.Text, lblRegister.Text, lblNoRM.Text, lblNamaPasien.Text, IIf(sISDOKTER = False, sKDDOCTOR_DPJPUTAMA, sKDDOCTOR_INPUT), lblRuangan.Text, Diagnosalabel, txtOBJEKTIF_BERATBADAN.Text, IIf(sISDOKTER = True, sUserID, sKDUSER_PERAWAT))
                    frmReqRecipeRawatInap.LoadMe(FORM_MODE.FORM_MODE_ADD)
                    frmReqRecipeRawatInap.ShowDialog(Me)

                    tabOrderList_SelectedPageChanged()
                Catch oErr As Exception
                    MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
                Finally
                    If Not frmReqRecipeRawatInap Is Nothing Then frmReqRecipeRawatInap.Dispose()
                    frmReqRecipeRawatInap = Nothing

                End Try
            Else
                Dim dsResep = oReqRecipeRawatInap.GetData(kode)
                If dsResep Is Nothing Then
                    Dim frmReqRecipeRawatInap As New frmReqRecipeRawatInap
                    Try
                        Dim listdiagnosa As New List(Of String)
                        For i As Integer = 0 To grvDIAGNOSA_CPPT.RowCount - 2
                            listdiagnosa.Add(grvDIAGNOSA_CPPT.GetRowCellValue(i, colNAMADIAGNOSA_CPPT))
                        Next

                        Dim Diagnosalabel As String = String.Join(vbCrLf, listdiagnosa.ToArray)

                        frmReqRecipeRawatInap.fn_LoaDataPasien(True, sISDOKTER, lblPenjamin.Text, lblRegister.Text, lblNoRM.Text, lblNamaPasien.Text, IIf(sISDOKTER = False, sKDDOCTOR_DPJPUTAMA, sKDDOCTOR_INPUT), lblRuangan.Text, Diagnosalabel, txtOBJEKTIF_BERATBADAN.Text, IIf(sISDOKTER = True, sUserID, sKDUSER_PERAWAT))
                        frmReqRecipeRawatInap.LoadMe(FORM_MODE.FORM_MODE_ADD)
                        frmReqRecipeRawatInap.ShowDialog(Me)

                        tabOrderList_SelectedPageChanged()
                    Catch oErr As Exception
                        MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
                    Finally
                        If Not frmReqRecipeRawatInap Is Nothing Then frmReqRecipeRawatInap.Dispose()
                        frmReqRecipeRawatInap = Nothing

                    End Try
                Else
                    If dsResep.KONFIRMASIRESEP <> "Resep Belum Diterima" Then
                        MsgBox("Resep sudah " & dsResep.KONFIRMASIRESEP & " Silahkan hubungi bagian farmasi", MsgBoxStyle.Exclamation, Me.Text)
                    Else
                        Dim frmReqRecipeRawatInap As New frmReqRecipeRawatInap
                        Try
                            Dim listdiagnosa As New List(Of String)
                            For i As Integer = 0 To grvDIAGNOSA_CPPT.RowCount - 2
                                listdiagnosa.Add(grvDIAGNOSA_CPPT.GetRowCellValue(i, colNAMADIAGNOSA_CPPT))
                            Next

                            Dim Diagnosalabel As String = String.Join(vbCrLf, listdiagnosa.ToArray)

                            frmReqRecipeRawatInap.fn_LoaDataPasien(True, sISDOKTER, lblPenjamin.Text, lblRegister.Text, lblNoRM.Text, lblNamaPasien.Text, IIf(sISDOKTER = False, sKDDOCTOR_DPJPUTAMA, sKDDOCTOR_INPUT), lblRuangan.Text, Diagnosalabel, txtOBJEKTIF_BERATBADAN.Text, IIf(sISDOKTER = True, sUserID, sKDUSER_PERAWAT))
                            frmReqRecipeRawatInap.LoadMe(FORM_MODE.FORM_MODE_EDIT, dsResep.KDREQRECIPE_RI)
                            frmReqRecipeRawatInap.ShowDialog(Me)

                            tabOrderList_SelectedPageChanged()
                        Catch oErr As Exception
                            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
                        Finally
                            If Not frmReqRecipeRawatInap Is Nothing Then frmReqRecipeRawatInap.Dispose()
                            frmReqRecipeRawatInap = Nothing

                        End Try
                    End If
                End If

            End If

        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub picEditOrderResep_Click() Handles picEditOrderResep.Click
        If grvOrderResep.GetFocusedRowCellValue("KDREQRECIPE_RI") Is Nothing Then
            Exit Sub
        End If

        Dim ds = oReqRecipeRawatInap.GetData(grvOrderResep.GetFocusedRowCellValue("KDREQRECIPE_RI"))

        If ds IsNot Nothing Then
            If ds.KONFIRMASIRESEP <> "Resep Belum Diterima" Then
                MsgBox("Resep sudah " & ds.KONFIRMASIRESEP & " Silahkan hubungi bagian farmasi", MsgBoxStyle.Exclamation, Me.Text)
            Else
                Dim frmReqRecipeRawatInap As New frmReqRecipeRawatInap
                Try
                    Dim listdiagnosa As New List(Of String)
                    For i As Integer = 0 To grvDIAGNOSA_CPPT.RowCount - 2
                        listdiagnosa.Add(grvDIAGNOSA_CPPT.GetRowCellValue(i, colNAMADIAGNOSA_CPPT))
                    Next

                    Dim Diagnosalabel As String = String.Join(vbCrLf, listdiagnosa.ToArray)

                    frmReqRecipeRawatInap.fn_LoaDataPasien(False, sISDOKTER, lblPenjamin.Text, lblRegister.Text, lblNoRM.Text, lblNamaPasien.Text, IIf(sISDOKTER = False, sKDDOCTOR_DPJPUTAMA, sKDDOCTOR_INPUT), lblRuangan.Text, Diagnosalabel, txtOBJEKTIF_BERATBADAN.Text, IIf(sISDOKTER = True, sUserID, sKDUSER_PERAWAT))
                    frmReqRecipeRawatInap.LoadMe(FORM_MODE.FORM_MODE_EDIT, ds.KDREQRECIPE_RI)
                    frmReqRecipeRawatInap.ShowDialog(Me)

                    tabOrderList_SelectedPageChanged()
                Catch oErr As Exception
                    MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
                Finally
                    If Not frmReqRecipeRawatInap Is Nothing Then frmReqRecipeRawatInap.Dispose()
                    frmReqRecipeRawatInap = Nothing
                End Try
            End If
        Else
            MsgBox("Resep Tidak di Temukan", MsgBoxStyle.Exclamation, Me.Text)
        End If
    End Sub
    Private Sub picDeleteOrderResep_Click() Handles picDeleteOrderResep.Click
        If grvOrderResep.GetFocusedRowCellValue("KDREQRECIPE_RI") Is Nothing Then
            Exit Sub
        End If
        If MsgBox(Statement.DeleteQuestion, MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, Me.Text) = MsgBoxResult.No Then Exit Sub
        If fn_DeleteDataOrderResep(grvOrderResep.GetFocusedRowCellValue("KDREQRECIPE_RI")) = False Then
            MsgBox(Statement.DeleteFail, MsgBoxStyle.Exclamation, Me.Text)
            Exit Sub
        End If
        MsgBox(Statement.DeleteSuccess, MsgBoxStyle.Information, Me.Text)
        tabOrderList_SelectedPageChanged()
    End Sub
    Private Sub picRefreshOrderResep_Click() Handles picRefreshOrderResep.Click
        tabOrderList_SelectedPageChanged()
    End Sub
#End Region
#Region "Laboratorium"
    Private Sub fn_LoadDataOrderLaboratorium()
        Try
            Dim ds = From x In oOrderPenunjang.GetDataDetailByKdpendaftaranLabH(lblRegister.Text)
                     Select x.KODE, x.SEQ, TANGGALORDER = x.DATE, x.NAMAORDER, x.MEMO, x.ISAPPROVE

            grdOrderLaboratorium.DataSource = ds.ToList

            fn_LoadFormatDataOrderLaboratorium()
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadFormatDataOrderLaboratorium()
        For iLoop As Integer = 0 To grvOrderLaboratorium.Columns.Count - 1
            If grvOrderLaboratorium.Columns(iLoop).ColumnType.Name = "Decimal" Then
                grvOrderLaboratorium.Columns(iLoop).DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                grvOrderLaboratorium.Columns(iLoop).DisplayFormat.FormatString = "{0:n2}"
                grvOrderLaboratorium.Columns(iLoop).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
            ElseIf grvOrderLaboratorium.Columns(iLoop).ColumnType.Name = "DateTime" Then
                grvOrderLaboratorium.Columns(iLoop).DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
                grvOrderLaboratorium.Columns(iLoop).DisplayFormat.FormatString = "{0:dd/MM/yyyy}"
            End If
        Next

        grvOrderLaboratorium.Columns("KODE").VisibleIndex = -1
        grvOrderLaboratorium.Columns("SEQ").VisibleIndex = -1
    End Sub
    Private Sub picAddOrderLaboratorium_Click() Handles picAddOrderLaboratorium.Click
        Dim frmOrderPenunjangLab As New frmOrderPenunjangLab

        Try
            If txtOBJEKTIF_BERATBADAN.Text = "" Then
                MsgBox("Dibutuhkan Berat Badan", MsgBoxStyle.Exclamation, Me.Text)
                Exit Sub
            End If
            If txtOBJEKTIF_TINGGIBADAN.Text = "" Then
                MsgBox("Dibutuhkan Tinggi Badan", MsgBoxStyle.Exclamation, Me.Text)
                Exit Sub
            End If
            Dim listdiagnosa As New List(Of String)

            If sISDOKTER = True Then
                For i As Integer = 0 To grvDIAGNOSA_CPPT.RowCount - 2
                    listdiagnosa.Add(grvDIAGNOSA_CPPT.GetRowCellValue(i, colNAMADIAGNOSA_CPPT))
                Next
            Else
                If sCOPYCPPTDOKTERTERAKHIR <> "" Then
                    For Each xloop In oCPPT.GetDataDetailDiagnosa(sCOPYCPPTDOKTERTERAKHIR)
                        listdiagnosa.Add(xloop.NAMADIAGNOSA)
                    Next
                End If
            End If

            frmOrderPenunjangLab.LoadMe(IIf(sISDOKTER = True, sUserID, IIf(sKDUSER_PERAWAT = "", sUserID, sKDUSER_PERAWAT)), String.Join(", ", listdiagnosa.ToArray), "LABORATORIUM", sKDDOCTOR_DPJPUTAMA, txtOBJEKTIF_BERATBADAN.Text, txtOBJEKTIF_TINGGIBADAN.Text, Now.ToString("yyyyMMddHHmmss") & lblNoRM.Text, lblRegister.Text, lblNoRM.Text, lblNamaPasien.Text, lblRuangan.Text, FORM_MODE.FORM_MODE_ADD)
            frmOrderPenunjangLab.ShowDialog(Me)

            tabOrderList_SelectedPageChanged()

        Catch ex As Exception
            MsgBox("Load Form Detail : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
        Finally
            If Not frmOrderPenunjangLab Is Nothing Then frmOrderPenunjangLab.Dispose()
            frmOrderPenunjangLab = Nothing
        End Try
    End Sub
    Private Sub picDeleteOrderLaboratorium_Click() Handles picDeleteOrderLaboratorium.Click
        'If grvOrderLaboratorium.GetFocusedRowCellValue("KODE") Is Nothing Then
        '    Exit Sub
        'End If
        If MsgBox(Statement.DeleteQuestion, MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, Me.Text) = MsgBoxResult.No Then Exit Sub
        'If fn_DeleteDataOrderLaboratorium(grvOrderLaboratorium.GetFocusedRowCellValue("KDREQRECIPE_RI")) = False Then
        '    MsgBox(Statement.DeleteFail, MsgBoxStyle.Exclamation, Me.Text)
        '    Exit Sub
        'End If
        'MsgBox(Statement.DeleteSuccess, MsgBoxStyle.Information, Me.Text)

        Try
            If grvOrderLaboratorium.GetFocusedRowCellValue("SEQ") Is Nothing Then
                Exit Sub
            End If

            If MsgBox("Apakah Yakin Akan dibatalkan?", MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, Me.Text) = MsgBoxResult.No Then Exit Sub

            Dim Kode As String = grvOrderLaboratorium.GetFocusedRowCellValue("KODE")
            Dim SEQ As String = grvOrderLaboratorium.GetFocusedRowCellValue("SEQ")

            Dim oConn As New SqlConnection
            Dim oComm As New SqlCommand
            Dim da As SqlDataAdapter
            Dim ds As New DataSet
            Dim SQL As String

            Dim sConn As String = Decrypt(My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Software\SIMRS\SW\", "Database", "").ToString())
            '
            oConn = New SqlConnection(sConn)
            If oConn.State = ConnectionState.Closed Then
                oConn.Open()
            End If

            SQL = "UPDATE S_REQ_ORDER_PENUNJANG_AWAL SET ISAPPROVE = 1, MEMO = 'BATAL ORDER', DATEUPDATED = '" & Now.ToString("yyyy-MM-dd HH:mm:ss") & "' "
            SQL &= "WHERE SEQ = " & SEQ & " AND KODE = '" & Kode & "' "

            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "UPDATE_S_REQ_ORDER_PENUNJANG")

            If oConn.State = ConnectionState.Open Then
                oConn.Close()
            End If

            tabOrderList_SelectedPageChanged()

        Catch ex As Exception
            MsgBox("Load Data : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub picRefreshOrderLaboratorium_Click() Handles picRefreshOrderLaboratorium.Click
        tabOrderList_SelectedPageChanged()
    End Sub
#End Region
#Region "Radiologi"
    Private Sub fn_LoadDataOrderRadiologi()
        Try
            Dim ds = From x In oOrderPenunjang.GetDataDetailByKdpendaftaranLabH(lblRegister.Text)
                     Select x.KODE, x.SEQ, TANGGALORDER = x.DATE, x.NAMAORDER, x.MEMO, x.ISAPPROVE

            grdOrderRadiologi.DataSource = ds.ToList

            fn_LoadFormatDataOrderRadiologi()
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadFormatDataOrderRadiologi()
        For iLoop As Integer = 0 To grvOrderRadiologi.Columns.Count - 1
            If grvOrderRadiologi.Columns(iLoop).ColumnType.Name = "Decimal" Then
                grvOrderRadiologi.Columns(iLoop).DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                grvOrderRadiologi.Columns(iLoop).DisplayFormat.FormatString = "{0:n2}"
                grvOrderRadiologi.Columns(iLoop).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
            ElseIf grvOrderRadiologi.Columns(iLoop).ColumnType.Name = "DateTime" Then
                grvOrderRadiologi.Columns(iLoop).DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
                grvOrderRadiologi.Columns(iLoop).DisplayFormat.FormatString = "{0:dd/MM/yyyy}"
            End If
        Next

        grvOrderRadiologi.Columns("KODE").VisibleIndex = -1
        grvOrderRadiologi.Columns("SEQ").VisibleIndex = -1
    End Sub
    Private Sub picAddOrderRadiologi_Click() Handles picAddOrderRadiologi.Click
        Dim frmOrderPenunjangLab As New frmOrderPenunjangLab

        Try
            If txtOBJEKTIF_BERATBADAN.Text = "" Then
                MsgBox("Dibutuhkan Berat Badan", MsgBoxStyle.Exclamation, Me.Text)
                Exit Sub
            End If
            If txtOBJEKTIF_TINGGIBADAN.Text = "" Then
                MsgBox("Dibutuhkan Tinggi Badan", MsgBoxStyle.Exclamation, Me.Text)
                Exit Sub
            End If
            Dim listdiagnosa As New List(Of String)

            If sISDOKTER = True Then
                For i As Integer = 0 To grvDIAGNOSA_CPPT.RowCount - 2
                    listdiagnosa.Add(grvDIAGNOSA_CPPT.GetRowCellValue(i, colNAMADIAGNOSA_CPPT))
                Next
            Else
                If sCOPYCPPTDOKTERTERAKHIR <> "" Then
                    For Each xloop In oCPPT.GetDataDetailDiagnosa(sCOPYCPPTDOKTERTERAKHIR)
                        listdiagnosa.Add(xloop.NAMADIAGNOSA)
                    Next
                End If
            End If

            frmOrderPenunjangLab.LoadMe(IIf(sISDOKTER = True, sUserID, IIf(sKDUSER_PERAWAT = "", sUserID, sKDUSER_PERAWAT)), String.Join(", ", listdiagnosa.ToArray), "RADIOLOGI", sKDDOCTOR_DPJPUTAMA, txtOBJEKTIF_BERATBADAN.Text, txtOBJEKTIF_TINGGIBADAN.Text, Now.ToString("yyyyMMddHHmmss") & lblNoRM.Text, lblRegister.Text, lblNoRM.Text, lblNamaPasien.Text, lblRuangan.Text, FORM_MODE.FORM_MODE_ADD)
            frmOrderPenunjangLab.ShowDialog(Me)

            tabOrderList_SelectedPageChanged()

        Catch ex As Exception
            MsgBox("Load Form Detail : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
        Finally
            If Not frmOrderPenunjangLab Is Nothing Then frmOrderPenunjangLab.Dispose()
            frmOrderPenunjangLab = Nothing
        End Try
    End Sub
    Private Sub picDeleteOrderRadiologi_Click() Handles picDeleteOrderRadiologi.Click
        'If grvOrderRadiologi.GetFocusedRowCellValue("KODE") Is Nothing Then
        '    Exit Sub
        'End If
        If MsgBox(Statement.DeleteQuestion, MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, Me.Text) = MsgBoxResult.No Then Exit Sub
        'If fn_DeleteDataOrderRadiologi(grvOrderRadiologi.GetFocusedRowCellValue("KDREQRECIPE_RI")) = False Then
        '    MsgBox(Statement.DeleteFail, MsgBoxStyle.Exclamation, Me.Text)
        '    Exit Sub
        'End If
        'MsgBox(Statement.DeleteSuccess, MsgBoxStyle.Information, Me.Text)

        Try
            If grvOrderRadiologi.GetFocusedRowCellValue("SEQ") Is Nothing Then
                Exit Sub
            End If

            If MsgBox("Apakah Yakin Akan dibatalkan?", MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, Me.Text) = MsgBoxResult.No Then Exit Sub

            Dim Kode As String = grvOrderRadiologi.GetFocusedRowCellValue("KODE")
            Dim SEQ As String = grvOrderRadiologi.GetFocusedRowCellValue("SEQ")

            Dim oConn As New SqlConnection
            Dim oComm As New SqlCommand
            Dim da As SqlDataAdapter
            Dim ds As New DataSet
            Dim SQL As String

            Dim sConn As String = Decrypt(My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Software\SIMRS\SW\", "Database", "").ToString())
            '
            oConn = New SqlConnection(sConn)
            If oConn.State = ConnectionState.Closed Then
                oConn.Open()
            End If

            SQL = "UPDATE S_REQ_ORDER_PENUNJANG_AWAL SET ISAPPROVE = 1, MEMO = 'BATAL ORDER', DATEUPDATED = '" & Now.ToString("yyyy-MM-dd HH:mm:ss") & "' "
            SQL &= "WHERE SEQ = " & SEQ & " AND KODE = '" & Kode & "' "

            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "UPDATE_S_REQ_ORDER_PENUNJANG")

            If oConn.State = ConnectionState.Open Then
                oConn.Close()
            End If

            tabOrderList_SelectedPageChanged()

        Catch ex As Exception
            MsgBox("Load Data : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub picRefreshOrderRadiologi_Click() Handles picRefreshOrderRadiologi.Click
        tabOrderList_SelectedPageChanged()
    End Sub
#End Region
#End Region
#Region "Brigging Radiologi"
    ' =======================================================================
    ' = Define constants that are need to build the outline RTF file format =
    ' =======================================================================

    ' Define Start and End strings for RTF Formatting
    Const RTF_START = "{\rtf1\ansi\ansicpg1252\deff0\deflang1033"
    Const RTF_END = "}"
    ' Define Start and End strings for FONT tables
    Const FONT_TABLE_START = "{\fonttbl"
    Const FONT_TABLE_END = "}"
    ' Define Start and End strings for COLOR tables (Next version maybe)
    'Const COLOR_TABLE_START = "{\colortbl "
    'Const COLOR_TABLE_END = ";}"
    ' Define End Paragraph constant
    Const LINE_BREAK = "\par" & vbCrLf
    ' Define Indent constant - No HTML equivalent ;-)
    'Const RTF_TAB = "\tab "
    ' Define New Paragraph constant
    Const NEW_PARAGRAPH = "\pard"
    ' Define Bold constants, as unlikely that you are only enboldening one character
    Const BOLD_START = "\b "
    Const BOLD_END = "\b0 "
    ' Define Underline constants
    Const UNDERLINE_START = "\ul "
    Const UNDERLINE_END = "\ul0 "
    ' Define Italic constants
    Const ITALIC_START = "\i "
    Const ITALIC_END = "\i0 "
    ' Define View and charset
    Const DOCUMENT_START = "\viewkind4\uc1"
    ' ======================================================
    ' = RenderHTML                                         =
    ' =                                                    =
    ' = Input : HTML encoded string (Content of body only) =
    ' =                                                    =
    ' = Output : RTF encoded string                        =
    ' =                                                    = 
    ' ======================================================
    Private Function RenderHTML(ByVal strInput As String) As String
        Dim intPos As Integer
        Dim strText As String

        ' Create initial header strings for the RTF format - Set font to Arial
        strText = RTF_START + FONT_TABLE_START + "{\f0\fnil\fcharset0 Arial;}" + FONT_TABLE_END + vbCrLf
        ' Add view and charset
        strText += DOCUMENT_START
        ' Start the document
        strText += NEW_PARAGRAPH
        ' Set the font to Arial 11 and Justify the text
        strText += "\sa200\sl276\slmult1\qj\f0\fs22\lang9 "

        ' Start Processing the HTML string
        Do
            ' Check for < in HTML string
            If Mid(strInput, 1, 1) = "<" Then
                ' Look for different tags and move input to next element in HTML
                If Mid(strInput, 1, 3) = "<p>" Or Mid(strInput, 1, 3) = "<P>" Then
                    strInput = Mid(strInput, 4)
                ElseIf Mid(strInput, 1, 4) = "</p>" Or Mid(strInput, 1, 3) = "</P>" Then
                    strText += LINE_BREAK
                    strInput = Mid(strInput, 5)
                ElseIf Mid(strInput, 1, 3) = "<b>" Or Mid(strInput, 1, 3) = "<B>" Then
                    strText += BOLD_START
                    strInput = Mid(strInput, 4)
                ElseIf Mid(strInput, 1, 4) = "</b>" Or Mid(strInput, 1, 3) = "</B>" Then
                    strText += BOLD_END
                    strInput = Mid(strInput, 5)
                ElseIf Mid(strInput, 1, 3) = "<i>" Or Mid(strInput, 1, 3) = "<I>" Then
                    strText += ITALIC_START
                    strInput = Mid(strInput, 4)
                ElseIf Mid(strInput, 1, 4) = "</i>" Or Mid(strInput, 1, 3) = "</I>" Then
                    strText += ITALIC_END
                    strInput = Mid(strInput, 5)
                ElseIf Mid(strInput, 1, 8) = "<strong>" Or Mid(strInput, 1, 8) = "<STRONG>" Then
                    strText += BOLD_START
                    strInput = Mid(strInput, 9)
                ElseIf Mid(strInput, 1, 9) = "</strong>" Or Mid(strInput, 1, 9) = "</STRONG>" Then
                    strText += BOLD_END
                    strInput = Mid(strInput, 10)
                ElseIf Mid(strInput, 1, 4) = "<em>" Or Mid(strInput, 1, 4) = "<EM>" Then
                    strText += ITALIC_START
                    strInput = Mid(strInput, 5)
                ElseIf Mid(strInput, 1, 5) = "</em>" Or Mid(strInput, 1, 5) = "</EM>" Then
                    strText += ITALIC_END
                    strInput = Mid(strInput, 6)
                ElseIf Mid(strInput, 1, 3) = "<u>" Or Mid(strInput, 1, 3) = "<U>" Then
                    strText += UNDERLINE_START
                    strInput = Mid(strInput, 4)
                ElseIf Mid(strInput, 1, 4) = "</u>" Or Mid(strInput, 1, 3) = "</U>" Then
                    strText += UNDERLINE_END
                    strInput = Mid(strInput, 5)
                Else
                    ' ============================================================================
                    ' = Catch all remaining HTML and show on the browser the unsupported element = 
                    ' ============================================================================
                    intPos = InStr(strInput, ">")
                    'HttpContext.Current.Response.Write("UNSUPPORTED  " + Mid(strInput, 1, intPos) + "<br/>")
                    strInput = Mid(strInput, intPos + 1)
                End If
            Else
                ' Check for & in the HTML input and replace
                If Mid(strInput, 1, 1) = "&" Then
                    If Mid(strInput, 1, 6) = "&nbsp;" Then
                        strText += " "
                        strInput = Mid(strInput, 7)
                    ElseIf Mid(strInput, 1, 5) = "&amp;" Then
                        strText += "&"
                        strInput = Mid(strInput, 6)
                    ElseIf Mid(strInput, 1, 4) = "&lt;" Then
                        strText += "<"
                        strInput = Mid(strInput, 5)
                    ElseIf Mid(strInput, 1, 4) = "&gt;" Then
                        strText += ">"
                        strInput = Mid(strInput, 5)
                    ElseIf Mid(strInput, 1, 6) = "&copy;" Then
                        strText += "\'a9"
                        strInput = Mid(strInput, 7)
                    ElseIf Mid(strInput, 1, 5) = "&reg;" Then
                        strText += "\'ae"
                        strInput = Mid(strInput, 6)
                    ElseIf Mid(strInput, 1, 7) = "&trade;" Then
                        strText += "\'99"
                        strInput = Mid(strInput, 8)
                    ElseIf Mid(strInput, 1, 7) = "&pound;" Then
                        strText += "£"
                        strInput = Mid(strInput, 8)
                    ElseIf Mid(strInput, 1, 6) = "&euro;" Then
                        strText += "\'80"
                        strInput = Mid(strInput, 7)
                    ElseIf Mid(strInput, 1, 2) = "&#" Then
                        ' Handle &# 
                        If CType(Mid(strInput, 3, InStr(strInput, ";") - 1), Integer) <= 127 Then
                            strText += Chr(CType(Mid(strInput, 3, InStr(strInput, ";") - 1), Integer))
                        ElseIf CType(Mid(strInput, 3, InStr(strInput, ";") - 1), Integer) <= 255 Then
                            strText += "\'" + Hex(CType(Mid(strInput, 3, InStr(strInput, ";") - 1), Integer))
                        Else
                            strText += "\u" + Hex(CType(Mid(strInput, 3, InStr(strInput, ";") - 1), Integer))
                        End If
                        strInput = Mid(strInput, 3, InStr(strInput, ";") + 1)
                    Else
                        ' ============================================================================
                        ' = Catch all remaining HTML and show on the browser the unsupported element = 
                        ' ============================================================================
                        intPos = InStr(strInput, ";")
                        'HttpContext.Current.Response.Write("UNSUPPORTED : " + Mid(strInput, 1, intPos) + "<br/>")
                        strInput = Mid(strInput, intPos + 1)
                    End If
                Else
                    strText += Mid(strInput, 1, 1)
                    strInput = Mid(strInput, 2)
                End If
            End If
        Loop Until strInput = ""
        strText += RTF_END
        Return strText
    End Function
#End Region
#Region "Matser Data"
    Private Sub btnCari_Click()
        If TextBox9.Text.Length < 3 Then
            MsgBox("Minimal 3 Huruf !!!", MsgBoxStyle.Exclamation, Me.Text)
            Exit Sub
        End If
        'Try
        '    Dim dsDataSetKoneksi = oBrigging.GetData().FirstOrDefault(Function(x) x.ISACTIVE = True And x.NAME_DISPLAY = "VCLAIM")
        '    Dim uTime As Integer = 0

        '    If dsDataSetKoneksi IsNot Nothing Then
        '        uTime = (DateTime.UtcNow - New DateTime(1970, 1, 1, 0, 0, 0)).TotalSeconds
        '        Dim dsSetKoneksi = oBrigging.GetDataVClaimReferensiDiagnosa(dsDataSetKoneksi.ALAMATWEB, dsDataSetKoneksi.CONSID, dsDataSetKoneksi.SECREATKEY, dsDataSetKoneksi.USER_KEY, uTime, TextBox9.Text)

        '        If dsSetKoneksi <> "" Then
        '            Try
        '                Dim allData = JObject.Parse(dsSetKoneksi)

        '                Dim table As DataTable

        '                table = New DataTable("M_KELAS")
        '                table.Columns.Add("kode")
        '                table.Columns.Add("nama")

        '                Dim dsData = oBrigging.Decrypt(allData("response"), dsDataSetKoneksi.CONSID & dsDataSetKoneksi.SECREATKEY & uTime)
        '                Dim dsDiagnosa = JObject.Parse(dsData)

        '                For Each item In dsDiagnosa("diagnosa")
        '                    table.Rows.Add(New String() {item("kode"), item("nama")})
        '                Next

        '                GridLookUpEdit1.Properties.DataSource = table
        '                GridLookUpEdit1.Properties.ValueMember = "kode"
        '                GridLookUpEdit1.Properties.DisplayMember = "nama"

        '                GridLookUpEdit1.ShowPopup()

        '                TextBox9.ResetText()

        '            Catch oErr As Exception
        '                MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message & vbCrLf & vbCrLf & "Result : " & vbCrLf & dsSetKoneksi, MsgBoxStyle.Exclamation, Me.Text)
        '            End Try
        '        End If
        '    Else
        '        MsgBox("Koneksi Tidak ditemukan", MsgBoxStyle.Exclamation, Me.Text)
        '    End If

        '    TextBox9.ResetText()
        'Catch oErr As Exception
        '    MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        'End Try
        Try
            Dim jsonDecode = JObject.Parse(oBrigging.fn_Pencariandiagnosa(TextBox9.Text))
            Dim sDataDuplicate As String = String.Empty
            Dim smessage As String = String.Empty

            sDataDuplicate = jsonDecode("metadata")("code").ToString
            smessage = jsonDecode("metadata")("message").ToString

            If sDataDuplicate = "200" Then
                Dim table As DataTable

                table = New DataTable("M_DIAGNOSA")
                table.Columns.Add("nama")
                table.Columns.Add("kode")

                For Each item In jsonDecode("response")("data")
                    table.Rows.Add(New String() {item(0), item(1)})
                Next

                GridLookUpEdit1.Properties.DataSource = table
                GridLookUpEdit1.Properties.ValueMember = "kode"
                GridLookUpEdit1.Properties.DisplayMember = "nama"

                GridLookUpEdit1.ShowPopup()

                TextBox9.ResetText()
            Else
                MsgBox("Pencarian Diagnosa" & vbCrLf & sDataDuplicate & "-" & smessage, MsgBoxStyle.Information, Me.Text)
            End If
        Catch oErr As Exception
            MsgBox("Pencarian Diagnosa: " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub btnCariProsedur_Click()
        Try
            If TextBox10.Text.Length < 3 Then
                MsgBox("Minimal 3 Huruf !!!", MsgBoxStyle.Exclamation, Me.Text)
                Exit Sub
            End If

            'Dim oSetKoneksi As New Brigging.clsSetKoneksi
            'Dim dsDataSetKoneksi = oSetKoneksi.GetData().FirstOrDefault(Function(x) x.ISACTIVE = True And x.NAME_DISPLAY = "VCLAIM")
            'Dim uTime As Integer = 0

            'If dsDataSetKoneksi IsNot Nothing Then
            '    uTime = (DateTime.UtcNow - New DateTime(1970, 1, 1, 0, 0, 0)).TotalSeconds
            '    Dim dsSetKoneksi = oSetKoneksi.GetDataVClaimReferensiProcedure(dsDataSetKoneksi.ALAMATWEB, dsDataSetKoneksi.CONSID, dsDataSetKoneksi.SECREATKEY, dsDataSetKoneksi.USER_KEY, uTime, txtCARI_PROSEDUR.Text)

            '    If dsSetKoneksi <> "" Then
            '        Try
            '            Dim allData = JObject.Parse(dsSetKoneksi)

            '            Dim table As DataTable

            '            table = New DataTable("M_TABEL")
            '            table.Columns.Add("kode")
            '            table.Columns.Add("nama")

            '            Dim dsData = oSetKoneksi.Decrypt(allData("response"), dsDataSetKoneksi.CONSID & dsDataSetKoneksi.SECREATKEY & uTime)
            '            Dim ds = JObject.Parse(dsData)

            '            For Each item In ds("procedure")
            '                table.Rows.Add(New String() {item("kode"), item("nama")})
            '            Next

            '            GridLookUpEdit2.Properties.DataSource = table
            '            GridLookUpEdit2.Properties.ValueMember = "kode"
            '            GridLookUpEdit2.Properties.DisplayMember = "nama"

            '            GridLookUpEdit2.ShowPopup()

            '        Catch oErr As Exception
            '            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message & vbCrLf & vbCrLf & "Result : " & vbCrLf & dsSetKoneksi, MsgBoxStyle.Exclamation, Me.Text)
            '        End Try
            '    End If
            'Else
            '    MsgBox("Koneksi Tidak ditemukan", MsgBoxStyle.Exclamation, Me.Text)
            'End If

            'txtCARI_PROSEDUR.ResetText()

            Try
                Dim jsonDecode = JObject.Parse(oBrigging.fn_PencarianProsedur("VCLAIM", TextBox10.Text))
                Dim sDataDuplicate As String = String.Empty
                Dim smessage As String = String.Empty

                sDataDuplicate = jsonDecode("metadata")("code").ToString
                smessage = jsonDecode("metadata")("message").ToString

                If sDataDuplicate = "200" Then
                    Dim table As DataTable

                    table = New DataTable("M_PROSEDUR")
                    table.Columns.Add("nama")
                    table.Columns.Add("kode")

                    For Each item In jsonDecode("response")("data")
                        table.Rows.Add(New String() {item(0), item(1)})
                    Next

                    GridLookUpEdit2.Properties.DataSource = table
                    GridLookUpEdit2.Properties.ValueMember = "kode"
                    GridLookUpEdit2.Properties.DisplayMember = "nama"

                    GridLookUpEdit2.ShowPopup()

                    TextBox10.ResetText()
                Else
                    MsgBox("Pencarian Prosedur" & vbCrLf & sDataDuplicate & "-" & smessage, MsgBoxStyle.Information, Me.Text)
                End If
            Catch oErr As Exception
                MsgBox("Pencarian Prosedur: " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            End Try
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Function AppendArray(Of T)(ByVal thisArray() As T, ByVal itemToAppend As T) As T()

        If thisArray Is Nothing Then thisArray = New T() {}
        Dim tempList As List(Of T) = thisArray.ToList
        tempList.Add(itemToAppend)
        Return tempList.ToArray

    End Function
    Private Function MergePdfFiles(ByVal pdfFiles() As String, ByVal outputPath As String) As Boolean
        Dim result As Boolean = False
        Dim pdfCount As Integer = 0     'total input pdf file count
        Dim f As Integer = 0    'pointer to current input pdf file
        Dim fileName As String
        Dim reader As iTextSharp.text.pdf.PdfReader = Nothing
        Dim pageCount As Integer = 0
        Dim pdfDoc As iTextSharp.text.Document = Nothing    'the output pdf document
        Dim writer As PdfWriter = Nothing
        Dim cb As PdfContentByte = Nothing

        Dim page As PdfImportedPage = Nothing
        Dim rotation As Integer = 0

        Try
            pdfCount = pdfFiles.Length
            If pdfCount >= 1 Then
                'Open the 1st item in the array PDFFiles
                fileName = pdfFiles(f)
                reader = New iTextSharp.text.pdf.PdfReader(fileName)
                'Get page count
                pageCount = reader.NumberOfPages

                pdfDoc = New iTextSharp.text.Document(reader.GetPageSizeWithRotation(1), 18, 18, 18, 18)

                writer = PdfWriter.GetInstance(pdfDoc, New FileStream(outputPath, FileMode.OpenOrCreate))

                With pdfDoc
                    .Open()
                End With
                'Instantiate a PdfContentByte object
                cb = writer.DirectContent
                'Now loop thru the input pdfs
                While f < pdfCount
                    'Declare a page counter variable
                    Dim i As Integer = 0
                    'Loop thru the current input pdf's pages starting at page 1
                    While i < pageCount
                        i += 1
                        'Get the input page size
                        pdfDoc.SetPageSize(reader.GetPageSizeWithRotation(i))
                        'Create a new page on the output document
                        pdfDoc.NewPage()
                        'If it is the 1st page, we add bookmarks to the page
                        'Now we get the imported page
                        page = writer.GetImportedPage(reader, i)
                        'Read the imported page's rotation
                        rotation = reader.GetPageRotation(i)
                        'Then add the imported page to the PdfContentByte object as a template based on the page's rotation
                        If rotation = 90 Then
                            cb.AddTemplate(page, 0, -1.0F, 1.0F, 0, 0, reader.GetPageSizeWithRotation(i).Height)
                        ElseIf rotation = 270 Then
                            cb.AddTemplate(page, 0, 1.0F, -1.0F, 0, reader.GetPageSizeWithRotation(i).Width + 60, -30)
                        Else
                            cb.AddTemplate(page, 1.0F, 0, 0, 1.0F, 0, 0)
                        End If
                    End While
                    'Increment f and read the next input pdf file
                    f += 1
                    If f < pdfCount Then
                        fileName = pdfFiles(f)
                        reader = New iTextSharp.text.pdf.PdfReader(fileName)
                        pageCount = reader.NumberOfPages
                    End If
                End While
                'When all done, we close the document so that the pdfwriter object can write it to the output file
                pdfDoc.Close()
                result = True

            End If
        Catch ex As Exception
            pdfDoc.Close()
            Return False
        End Try
        Return result
    End Function
    Private Sub btnJawabKonsul_Click(sender As Object, e As EventArgs) Handles btnJawabKonsul.Click
        If grvDokterBersama.GetFocusedRowCellValue("SEQ") = 0 Then
            MsgBox("Silahkan Pilih data", MsgBoxStyle.Exclamation, Me.Text)
            Exit Sub
        End If

        Dim frmKonsulDokter As New frmKonsulDokter
        Try
            frmKonsulDokter.fn_LoadMe(lblRegister.Text, grvDokterBersama.GetFocusedRowCellValue("KDDOCTOR_DARI"), grvDokterBersama.GetFocusedRowCellValue("KDDOCTOR_KEPADA"), grvDokterBersama.GetFocusedRowCellValue("SEQ"), grvDokterBersama.GetFocusedRowCellValue("ISIKONSUL"), grvDokterBersama.GetFocusedRowCellValue("JAWABKONSUL"), grvDokterBersama.GetFocusedRowCellValue("TGLKONSUL"), grvDokterBersama.GetFocusedRowCellValue("TGLJAWAB"))
            frmKonsulDokter.ShowDialog(Me)
        Catch ex As Exception
            MsgBox("Load Form Detail : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
        Finally
            If Not frmKonsulDokter Is Nothing Then frmKonsulDokter.Dispose()
            frmKonsulDokter = Nothing
            fn_DokterBersama()
            'If sKONSULTASI <> "" Then
            '    txtINTRUKSILAIN.Text = txtINTRUKSILAIN.Text & vbCrLf & "Konsultasi Ke Dokter" & sKONSULTASI
            'End If
        End Try
    End Sub
    Private Sub btnHapusKonsul_Click(sender As Object, e As EventArgs) Handles btnHapusKonsul.Click
        If grvDokterBersama.GetFocusedRowCellValue("SEQ") Is Nothing Then Exit Sub

        If grvDokterBersama.GetFocusedRowCellValue("SEQ") = 0 Then
            MsgBox("Silahkan Pilih data", MsgBoxStyle.Exclamation, Me.Text)
            Exit Sub
        End If

        If MsgBox("Apakah Yakin di Hapus ?", MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, Me.Text) = MsgBoxResult.No Then Exit Sub

        fn_HapusKonsul(grvDokterBersama.GetFocusedRowCellValue("SEQ"))
        fn_DokterBersama()
    End Sub
    Private Sub fn_HapusKonsul(ByVal SEQ As Integer)
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

            SQL = "DELETE I_TRACKING_KDDOCTOR WHERE KDREG = '" & lblRegister.Text & "' AND SEQ = " & SEQ & " "

            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "DELETE_I_TRACKING_KDDOCTOR")

            If oConn.State = ConnectionState.Open Then
                oConn.Close()
            End If
        Catch oErr As Exception
            MsgBox("DELETE I_TRACKING_KDDOCTOR : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub grvDokterBersama_FocusedRowChanged(ByVal sender As System.Object, ByVal e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs) Handles grvDokterBersama.FocusedRowChanged
        If grvDokterBersama.GetFocusedRowCellValue("ISIKONSUL") Is Nothing Then
            PdfViewerKonsul.CloseDocument()
            Exit Sub
        End If

        'Dim List As New List(Of String)

        'List.Add("Dari Dokter : " & grvDokterBersama.GetFocusedRowCellValue("DARIDOKTER"))
        'List.Add("Kepada Dokter : " & grvDokterBersama.GetFocusedRowCellValue("KEPADADOKTER"))
        'List.Add("")
        'List.Add("Isi Konsul : " & grvDokterBersama.GetFocusedRowCellValue("ISIKONSUL"))
        'List.Add("")
        'List.Add("Jawab Konsul : " & grvDokterBersama.GetFocusedRowCellValue("JAWABKONSUL"))

        'txtISIKONSULTASI.Text = String.Join(vbCrLf, List.ToArray)

        Try
            sNAMA_DIKONSUL = lblNamaPasien.Text
            sRM_DIKONSUL = lblNoRM.Text
            sRUANG_DIKONSUL = lblRuangan.Text
            sDOKTERDARI_DIKONSUL = grvDokterBersama.GetFocusedRowCellValue("DARIDOKTER")
            sDOKTERKEPADA_DIKONSUL = grvDokterBersama.GetFocusedRowCellValue("KEPADADOKTER")
            sISIKONSUL_DIKONSUL = grvDokterBersama.GetFocusedRowCellValue("ISIKONSUL")
            sJAWABKONSUL_DIKONSUL = grvDokterBersama.GetFocusedRowCellValue("JAWABKONSUL")

            Dim rpt As New xtraKonsul

            rpt.ShowPrintMarginsWarning = False
            rpt.Watermark.Text = sWATERMARK

            'rpt.BindingSource1.DataSource = ds
            Dim FolderSimpan = "C:/KONSULTASI/"

            Try

                If Not Directory.Exists(FolderSimpan) Then
                    Directory.CreateDirectory(FolderSimpan)
                Else
                    DeleteDirectory(FolderSimpan)
                    Directory.CreateDirectory(FolderSimpan)
                End If
            Catch ex As Exception

            End Try

            Dim waktu As String = Now.ToString("ddMMyyyyHHmmss")
            rpt.ExportToPdf(FolderSimpan & waktu & grvDokterBersama.GetFocusedRowCellValue("SEQ") & ".pdf")
            PdfViewerKonsul.LoadDocument(FolderSimpan & waktu & grvDokterBersama.GetFocusedRowCellValue("SEQ") & ".pdf")

        Catch ex As Exception
            MsgBox("Cetak Konsultasi " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
#End Region
#Region "EKLAIM"
    Private Sub fn_LoadRincian(ByVal Pesan As Boolean)
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
            SQL &= "GRANDTOTAL = ISNULL((SUM(A.GRANDTOTAL)), 0) "
            SQL &= "FROM "
            SQL &= "S_TRANSAKSIRI_H A "
            SQL &= "WHERE "
            SQL &= "A.KDREG = '" & lblRegister.Text & "' "
            SQL &= "OR "
            SQL &= "A.KDREG = '" & sKDREGRAWATJALAN & "' "

            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "S_TRANSAKSIRI_H")

            Dim totalrs As Decimal = 0

            For iLoop As Integer = 0 To ds.Tables("S_TRANSAKSIRI_H").Rows.Count - 1
                With ds.Tables("S_TRANSAKSIRI_H")
                    totalrs += .Rows(iLoop)("GRANDTOTAL")
                End With
            Next

            SQL = "SELECT "
            SQL &= "GRANDTOTAL = ISNULL((SUM(A.GRANDTOTAL)), 0) "
            SQL &= "FROM "
            SQL &= "S_TRANSAKSILAB_H A "
            SQL &= "WHERE "
            SQL &= "A.KDREG = '" & lblRegister.Text & "' "
            SQL &= "OR "
            SQL &= "A.KDREG = '" & sKDREGRAWATJALAN & "' "

            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "S_TRANSAKSILAB_H")


            For iLoop As Integer = 0 To ds.Tables("S_TRANSAKSILAB_H").Rows.Count - 1
                With ds.Tables("S_TRANSAKSILAB_H")
                    totalrs += .Rows(iLoop)("GRANDTOTAL")
                End With
            Next

            SQL = "SELECT "
            SQL &= "GRANDTOTAL = ISNULL((SUM(A.GRANDTOTAL)), 0) "
            SQL &= "FROM "
            SQL &= "S_TRANSAKSI_H A "
            SQL &= "WHERE "
            SQL &= "A.KDREG = '" & lblRegister.Text & "' "
            SQL &= "OR "
            SQL &= "A.KDREG = '" & sKDREGRAWATJALAN & "' "


            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "S_TRANSAKSI_H")

            For iLoop As Integer = 0 To ds.Tables("S_TRANSAKSI_H").Rows.Count - 1
                With ds.Tables("S_TRANSAKSI_H")
                    totalrs += .Rows(iLoop)("GRANDTOTAL")
                End With
            Next

            SQL = "SELECT "
            SQL &= "GRANDTOTAL = ISNULL((SUM(A.GRANDTOTAL)), 0) "
            SQL &= "FROM "
            SQL &= "S_RECIPE_D A "
            SQL &= "INNER JOIN S_RECIPE_H B "
            SQL &= "ON A.KDRECIPE = B.KDRECIPE "
            SQL &= "WHERE "
            SQL &= "B.KDREG = '" & lblRegister.Text & "' "
            SQL &= "AND "
            SQL &= "A.ISPROLANIS = 0 "
            SQL &= "OR "
            SQL &= "B.KDREG = '" & sKDREGRAWATJALAN & "' "
            SQL &= "AND "
            SQL &= "A.ISPROLANIS = 0 "

            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "S_SO_H")

            For iLoop As Integer = 0 To ds.Tables("S_SO_H").Rows.Count - 1
                With ds.Tables("S_SO_H")
                    totalrs += .Rows(iLoop)("GRANDTOTAL")
                End With
            Next

            TextBox27.Text = FormatNumber(totalrs, 0)
            lblTotalRincianRSCPPT.Text = FormatNumber(totalrs, 0)

            grvObatTotalHariIni.OptionsSelection.MultiSelect = True
            grvObatTotalHariIni.SelectAll()
            grvObatTotalHariIni.DeleteSelectedRows()
            grvObatTotalHariIni.OptionsSelection.MultiSelect = False

            SQL = "SELECT "
            SQL &= "ObatHariIni = C.NMITEM2 "
            SQL &= ",Satuan = D.DESCRIPTION "
            SQL &= ",Jumlah = A.QTY "
            SQL &= ",Signa = E.DESCRIPTION "
            SQL &= ",CaraPakai = F.DESCRIPTION "
            SQL &= ",Total = A.GRANDTOTAL "
            SQL &= "FROM "
            SQL &= "S_RECIPE_D A "
            SQL &= "INNER JOIN S_RECIPE_H B "
            SQL &= "ON A.KDRECIPE = B.KDRECIPE "
            SQL &= "INNER JOIN M_ITEM C "
            SQL &= "ON A.KDITEM = C.KDITEM "
            SQL &= "INNER JOIN M_UOM D "
            SQL &= "ON A.KDUOM = D.KDUOM "
            SQL &= "INNER JOIN M_SIGNA E "
            SQL &= "ON A.KDSIGNA = E.KDSIGNA "
            SQL &= "INNER JOIN M_CARAPAKAI F "
            SQL &= "ON A.KDCP = F.KDCP "
            SQL &= "WHERE "
            SQL &= "B.KDREG = '" & lblRegister.Text & "' "
            SQL &= "AND "
            SQL &= "A.ISPROLANIS = 0 "
            SQL &= "AND CONVERT(VARCHAR(8), B.DATE, 112) = '" & Now.ToString("yyyyMMdd") & "'  "
            SQL &= "OR "
            SQL &= "B.KDREG = '" & sKDREGRAWATJALAN & "' "
            SQL &= "AND "
            SQL &= "A.ISPROLANIS = 0 "
            SQL &= "AND CONVERT(VARCHAR(8), B.DATE, 112) = '" & Now.ToString("yyyyMMdd") & "'  "

            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "S_DATAOBAT")

            Dim totalobathariini As Decimal = 0
            For iLoop As Integer = 0 To ds.Tables("S_DATAOBAT").Rows.Count - 1
                With ds.Tables("S_DATAOBAT")
                    totalobathariini += .Rows(iLoop)("Total")
                End With
            Next

            lblTotalObatHariIni.Text = FormatNumber(totalobathariini, 0)

            grdObatTotalHariIni.DataSource = ds.Tables("S_DATAOBAT")
            grdObatTotalHariIni.ForceInitialize()

            For iLoop As Integer = 0 To grvObatTotalHariIni.Columns.Count - 1
                If grvObatTotalHariIni.Columns(iLoop).ColumnType.Name = "Decimal" Then
                    grvObatTotalHariIni.Columns(iLoop).DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                    grvObatTotalHariIni.Columns(iLoop).DisplayFormat.FormatString = "{0:n2}"
                    grvObatTotalHariIni.Columns(iLoop).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
                ElseIf grvObatTotalHariIni.Columns(iLoop).ColumnType.Name = "DateTime" Then
                    grvObatTotalHariIni.Columns(iLoop).DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
                    grvObatTotalHariIni.Columns(iLoop).DisplayFormat.FormatString = "{0:dd/MM/yyyy HH:mm}"
                End If
            Next

            grvObatTotalHariIni.BestFitColumns()

            If oConn.State = ConnectionState.Open Then
                oConn.Close()
            End If

            If Pesan = True Then
                MsgBox("Data Rinician " & totalrs & vbCrLf & "Total Obat Hari Ini " & totalobathariini, MsgBoxStyle.Exclamation, Me.Text)
            End If
        Catch oErr As Exception
            MsgBox("Load Rincian: " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    'Private Sub fn_LoadResepPerawat()
    '    Try
    '        Dim oConn As New SqlConnection
    '        Dim oComm As New SqlCommand
    '        Dim da As SqlDataAdapter
    '        Dim ds As New DataSet
    '        Dim SQL As String
    '        Dim sConn As String = Decrypt(My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Software\SIMRS\SW\", "Database", "").ToString())

    '        oConn = New SqlConnection(sConn)

    '        If oConn.State = ConnectionState.Closed Then
    '            oConn.Open()
    '        End If

    '        SQL = "SELECT "
    '        SQL &= "B.NAMAOBAT "
    '        SQL &= ",B.SATUAN "
    '        SQL &= ",B.DOSIS "
    '        SQL &= ",B.SIGNA "
    '        SQL &= ",B.CARAPAKAI "
    '        SQL &= ",JUMLAH = B.QTY "
    '        SQL &= ",KETERANGAN = B.REMARKS_DOKTER "
    '        SQL &= ",CATATAN = B.REMARKS_FARMASI "
    '        SQL &= "FROM S_REQ_RECIPE_RI_H A "
    '        SQL &= "INNER JOIN S_REQ_RECIPE_RI_D B "
    '        SQL &= "ON A.KDREQRECIPE_RI = A.KDREQRECIPE_RI "
    '        SQL &= "WHERE A.KDPENDAFTARAN = '" & lblRegister.Text & "' "
    '        SQL &= "AND CONVERT(VARCHAR(8), A.DATE, 112) = '" & Now.ToString("yyyyMMdd") & "'  "

    '        oComm.Connection = oConn
    '        oComm.CommandText = SQL
    '        oComm.CommandTimeout = 120
    '        oComm.CommandType = CommandType.Text

    '        da = New SqlDataAdapter(oComm)
    '        da.Fill(ds, "S_TRANSAKSIRI_H")

    '        grdResepPerawat.DataSource = ds.Tables("S_DATAOBAT")
    '        grdResepPerawat.ForceInitialize()

    '        grvResepPerawat.BestFitColumns()

    '        If oConn.State = ConnectionState.Open Then
    '            oConn.Close()
    '        End If

    '    Catch oErr As Exception
    '        MsgBox("Load Resep Perawat: " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
    '    End Try
    'End Sub
    Private Sub fn_CariRegister(ByVal sRegister As String)
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

            SQL = "SELECT A.TanggalMasuk, A.TanggalKeluar, A.JenisRawat, A.Register, A.NoRM, A.NoKartu, A.NIK, A.Pasien, A.TanggalLahir, A.NomorSEP, A.Dokter, A.Penjamin, A.Tindakan, Lainnya = SUM(A.Lainnya), ProsedurNonBedah = SUM(A.ProsedurNonBedah), ProsedurBedah = SUM(A.ProsedurBedah), TenagaAhli = SUM(A.TenagaAhli), Konsultasi = SUM(A.Konsultasi), Keperawatan = SUM(A.Keperawatan), Penunjang = SUM(A.Penunjang), Radiologi = SUM(A.Radiologi), Laboratorium = SUM(A.Laboratorium), PelayananDarah = SUM(A.PelayananDarah), Rehabilitasi = SUM(A.Rehabilitasi), KamarAkomodasi = SUM(A.KamarAkomodasi), RawatIntensif = SUM(A.RawatIntensif), Obat = SUM(A.Obat), Alkes = SUM(A.Alkes), BMHP = SUM(A.BMHP), AlatMedis = SUM(A.AlatMedis), ObatKronis = SUM(A.ObatKronis), ObatKemoterapi = SUM(A.ObatKemoterapi), "
            SQL &= "Total = SUM(A.Lainnya) + SUM(A.ProsedurNonBedah) + SUM(A.ProsedurBedah) + SUM(A.TenagaAhli) + SUM(A.Konsultasi) + SUM(A.Keperawatan) + SUM(A.Penunjang) + SUM(A.Radiologi) + SUM(A.Laboratorium) + SUM(A.PelayananDarah) + SUM(A.Rehabilitasi) + SUM(A.KamarAkomodasi) + SUM(A.RawatIntensif) + SUM(A.Obat) + SUM(A.Alkes) + SUM(A.BMHP) + SUM(A.AlatMedis) + SUM(A.ObatKronis) + SUM(A.ObatKemoterapi) FROM( "

#Region "SELECT"

            SQL &= "(SELECT "
            SQL &= "TanggalMasuk = D.DATE "
            SQL &= ",TanggalKeluar = D.DATEPULANG "
            SQL &= ",JenisRawat = D.CATEGORY "
            SQL &= ",Register = A.KDREG "
            SQL &= ",NoRM = A.KDCUSTOMER "
            SQL &= ",NoKartu = D.KARTUBPJS "
            SQL &= ",NIK = D.NIK "
            SQL &= ",Pasien = (SELECT AA.NAME_DISPLAY FROM M_CUSTOMER AA WHERE AA.KDCUSTOMER = A.KDCUSTOMER) "
            SQL &= ",TanggalLahir = (SELECT AA.TANGGALLAHIR FROM M_CUSTOMER AA WHERE AA.KDCUSTOMER = A.KDCUSTOMER) "
            SQL &= ",NomorSEP = ISNULL((D.NOMORSEP), '') "
            SQL &= ",Dokter = D.KDDOCTOR "
            SQL &= ",Penjamin = (SELECT BB.NAME_DISPLAY FROM M_DEBTOR BB WHERE BB.KDDEBTOR = A.KDDEBTOR) "
            SQL &= ",Tindakan = 'Prosedur Non Bedah' "
            SQL &= ",SEQ = B.SEQ "
            SQL &= ",Lainnya = 0 "
            SQL &= ",ProsedurNonBedah = B.GRANDTOTAL "
            SQL &= ",ProsedurBedah = 0 "
            SQL &= ",TenagaAhli = 0 "
            SQL &= ",Konsultasi = 0 "
            SQL &= ",Keperawatan = 0 "
            SQL &= ",Penunjang = 0 "
            SQL &= ",Radiologi = 0 "
            SQL &= ",Laboratorium = 0 "
            SQL &= ",PelayananDarah = 0 "
            SQL &= ",Rehabilitasi = 0 "
            SQL &= ",KamarAkomodasi = 0 "
            SQL &= ",RawatIntensif = 0 "
            SQL &= ",Obat = 0 "
            SQL &= ",Alkes = 0 "
            SQL &= ",BMHP = 0 "
            SQL &= ",AlatMedis = 0 "
            SQL &= ",ObatKronis = 0 "
            SQL &= ",ObatKemoterapi = 0 "

            SQL &= "FROM F_CASHIER_H A "
            SQL &= "INNER JOIN F_CASHIER_D_TRANSAKSI B "
            SQL &= "ON A.KDCASHIER = B.KDCASHIER  "
            SQL &= "INNER JOIN M_TARIF_NEW C  "
            SQL &= "ON B.KDTARIF = C.KDTARIF "
            SQL &= "INNER JOIN S_PENDAFTARAN_H D "
            SQL &= "ON A.KDREG = D.KDREG "

            SQL &= "WHERE A.KDREG = '" & sRegister & "' AND C.KDKATEGORI_BPJS = 2 "


            SQL &= ") UNION ALL( "

            SQL &= "SELECT "
            SQL &= "TanggalMasuk = D.DATE "
            SQL &= ",TanggalKeluar = D.DATEPULANG "
            SQL &= ",JenisRawat = D.CATEGORY "
            SQL &= ",Register = A.KDREG "
            SQL &= ",NoRM = A.KDCUSTOMER "
            SQL &= ",NoKartu = D.KARTUBPJS "
            SQL &= ",NIK = D.NIK "
            SQL &= ",Pasien = (SELECT AA.NAME_DISPLAY FROM M_CUSTOMER AA WHERE AA.KDCUSTOMER = A.KDCUSTOMER) "
            SQL &= ",TanggalLahir = (SELECT AA.TANGGALLAHIR FROM M_CUSTOMER AA WHERE AA.KDCUSTOMER = A.KDCUSTOMER) "
            SQL &= ",NomorSEP = ISNULL((D.NOMORSEP), '') "
            SQL &= ",Dokter = D.KDDOCTOR "
            SQL &= ",Penjamin = (SELECT BB.NAME_DISPLAY FROM M_DEBTOR BB WHERE BB.KDDEBTOR = A.KDDEBTOR) "
            SQL &= ",Tindakan = 'Prosedur Bedah' "
            SQL &= ",SEQ = B.SEQ "
            SQL &= ",Lainnya = 0 "
            SQL &= ",ProsedurNonBedah = 0 "
            SQL &= ",ProsedurBedah = B.GRANDTOTAL "
            SQL &= ",TenagaAhli = 0 "
            SQL &= ",Konsultasi = 0 "
            SQL &= ",Keperawatan = 0 "
            SQL &= ",Penunjang = 0 "
            SQL &= ",Radiologi = 0 "
            SQL &= ",Laboratorium = 0 "
            SQL &= ",PelayananDarah = 0 "
            SQL &= ",Rehabilitasi = 0 "
            SQL &= ",KamarAkomodasi = 0 "
            SQL &= ",RawatIntensif = 0 "
            SQL &= ",Obat = 0 "
            SQL &= ",Alkes = 0 "
            SQL &= ",BMHP = 0 "
            SQL &= ",AlatMedis = 0 "
            SQL &= ",ObatKronis = 0 "
            SQL &= ",ObatKemoterapi = 0 "

            SQL &= "FROM F_CASHIER_H A "
            SQL &= "INNER JOIN F_CASHIER_D_TRANSAKSI B "
            SQL &= "ON A.KDCASHIER = B.KDCASHIER  "
            SQL &= "INNER JOIN M_TARIF_NEW C  "
            SQL &= "ON B.KDTARIF = C.KDTARIF "
            SQL &= "INNER JOIN S_PENDAFTARAN_H D "
            SQL &= "ON A.KDREG = D.KDREG "


            SQL &= "WHERE A.KDREG = '" & sRegister & "' AND C.KDKATEGORI_BPJS = 3 "


            SQL &= ") UNION ALL( "

            SQL &= "SELECT "
            SQL &= "TanggalMasuk = D.DATE "
            SQL &= ",TanggalKeluar = D.DATEPULANG "
            SQL &= ",JenisRawat = D.CATEGORY "
            SQL &= ",Register = A.KDREG "
            SQL &= ",NoRM = A.KDCUSTOMER "
            SQL &= ",NoKartu = D.KARTUBPJS "
            SQL &= ",NIK = D.NIK "
            SQL &= ",Pasien = (SELECT AA.NAME_DISPLAY FROM M_CUSTOMER AA WHERE AA.KDCUSTOMER = A.KDCUSTOMER) "
            SQL &= ",TanggalLahir = (SELECT AA.TANGGALLAHIR FROM M_CUSTOMER AA WHERE AA.KDCUSTOMER = A.KDCUSTOMER) "
            SQL &= ",NomorSEP = ISNULL((D.NOMORSEP), '') "
            SQL &= ",Dokter = D.KDDOCTOR "
            SQL &= ",Penjamin = (SELECT BB.NAME_DISPLAY FROM M_DEBTOR BB WHERE BB.KDDEBTOR = A.KDDEBTOR) "
            SQL &= ",Tindakan = 'Tenaga Ahli' "
            SQL &= ",SEQ = B.SEQ "
            SQL &= ",Lainnya = 0 "
            SQL &= ",ProsedurNonBedah = 0 "
            SQL &= ",ProsedurBedah = 0 "
            SQL &= ",TenagaAhli = B.GRANDTOTAL "
            SQL &= ",Konsultasi = 0 "
            SQL &= ",Keperawatan = 0 "
            SQL &= ",Penunjang = 0 "
            SQL &= ",Radiologi = 0 "
            SQL &= ",Laboratorium = 0 "
            SQL &= ",PelayananDarah = 0 "
            SQL &= ",Rehabilitasi = 0 "
            SQL &= ",KamarAkomodasi = 0 "
            SQL &= ",RawatIntensif = 0 "
            SQL &= ",Obat = 0 "
            SQL &= ",Alkes = 0 "
            SQL &= ",BMHP = 0 "
            SQL &= ",AlatMedis = 0 "
            SQL &= ",ObatKronis = 0 "
            SQL &= ",ObatKemoterapi = 0 "

            SQL &= "FROM F_CASHIER_H A "
            SQL &= "INNER JOIN F_CASHIER_D_TRANSAKSI B "
            SQL &= "ON A.KDCASHIER = B.KDCASHIER  "
            SQL &= "INNER JOIN M_TARIF_NEW C  "
            SQL &= "ON B.KDTARIF = C.KDTARIF "
            SQL &= "INNER JOIN S_PENDAFTARAN_H D "
            SQL &= "ON A.KDREG = D.KDREG "


            SQL &= "WHERE A.KDREG = '" & sRegister & "' AND C.KDKATEGORI_BPJS = 4 "


            SQL &= ") UNION ALL( "

            SQL &= "SELECT "
            SQL &= "TanggalMasuk = D.DATE "
            SQL &= ",TanggalKeluar = D.DATEPULANG "
            SQL &= ",JenisRawat = D.CATEGORY "
            SQL &= ",Register = A.KDREG "
            SQL &= ",NoRM = A.KDCUSTOMER "
            SQL &= ",NoKartu = D.KARTUBPJS "
            SQL &= ",NIK = D.NIK "
            SQL &= ",Pasien = (SELECT AA.NAME_DISPLAY FROM M_CUSTOMER AA WHERE AA.KDCUSTOMER = A.KDCUSTOMER) "
            SQL &= ",TanggalLahir = (SELECT AA.TANGGALLAHIR FROM M_CUSTOMER AA WHERE AA.KDCUSTOMER = A.KDCUSTOMER) "
            SQL &= ",NomorSEP = ISNULL((D.NOMORSEP), '') "
            SQL &= ",Dokter = D.KDDOCTOR "
            SQL &= ",Penjamin = (SELECT BB.NAME_DISPLAY FROM M_DEBTOR BB WHERE BB.KDDEBTOR = A.KDDEBTOR) "
            SQL &= ",Tindakan = 'Konsultasi' "
            SQL &= ",SEQ = B.SEQ "
            SQL &= ",Lainnya = 0 "
            SQL &= ",ProsedurNonBedah = 0 "
            SQL &= ",ProsedurBedah = 0 "
            SQL &= ",TenagaAhli = 0 "
            SQL &= ",Konsultasi = B.GRANDTOTAL "
            SQL &= ",Keperawatan = 0 "
            SQL &= ",Penunjang = 0 "
            SQL &= ",Radiologi = 0 "
            SQL &= ",Laboratorium = 0 "
            SQL &= ",PelayananDarah = 0 "
            SQL &= ",Rehabilitasi = 0 "
            SQL &= ",KamarAkomodasi = 0 "
            SQL &= ",RawatIntensif = 0 "
            SQL &= ",Obat = 0 "
            SQL &= ",Alkes = 0 "
            SQL &= ",BMHP = 0 "
            SQL &= ",AlatMedis = 0 "
            SQL &= ",ObatKronis = 0 "
            SQL &= ",ObatKemoterapi = 0 "

            SQL &= "FROM F_CASHIER_H A "
            SQL &= "INNER JOIN F_CASHIER_D_TRANSAKSI B "
            SQL &= "ON A.KDCASHIER = B.KDCASHIER  "
            SQL &= "INNER JOIN M_TARIF_NEW C  "
            SQL &= "ON B.KDTARIF = C.KDTARIF "
            SQL &= "INNER JOIN S_PENDAFTARAN_H D "
            SQL &= "ON A.KDREG = D.KDREG "


            SQL &= "WHERE A.KDREG = '" & sRegister & "' AND C.KDKATEGORI_BPJS = 5 "


            SQL &= ") UNION ALL( "

            SQL &= "SELECT "
            SQL &= "TanggalMasuk = D.DATE "
            SQL &= ",TanggalKeluar = D.DATEPULANG "
            SQL &= ",JenisRawat = D.CATEGORY "
            SQL &= ",Register = A.KDREG "
            SQL &= ",NoRM = A.KDCUSTOMER "
            SQL &= ",NoKartu = D.KARTUBPJS "
            SQL &= ",NIK = D.NIK "
            SQL &= ",Pasien = (SELECT AA.NAME_DISPLAY FROM M_CUSTOMER AA WHERE AA.KDCUSTOMER = A.KDCUSTOMER) "
            SQL &= ",TanggalLahir = (SELECT AA.TANGGALLAHIR FROM M_CUSTOMER AA WHERE AA.KDCUSTOMER = A.KDCUSTOMER) "
            SQL &= ",NomorSEP = ISNULL((D.NOMORSEP), '') "
            SQL &= ",Dokter = D.KDDOCTOR "
            SQL &= ",Penjamin = (SELECT BB.NAME_DISPLAY FROM M_DEBTOR BB WHERE BB.KDDEBTOR = A.KDDEBTOR) "
            SQL &= ",Tindakan = 'Keperawatan' "
            SQL &= ",SEQ = B.SEQ "
            SQL &= ",Lainnya = 0 "
            SQL &= ",ProsedurNonBedah = 0 "
            SQL &= ",ProsedurBedah = 0 "
            SQL &= ",TenagaAhli = 0 "
            SQL &= ",Konsultasi = 0 "
            SQL &= ",Keperawatan = B.GRANDTOTAL "
            SQL &= ",Penunjang = 0 "
            SQL &= ",Radiologi = 0 "
            SQL &= ",Laboratorium = 0 "
            SQL &= ",PelayananDarah = 0 "
            SQL &= ",Rehabilitasi = 0 "
            SQL &= ",KamarAkomodasi = 0 "
            SQL &= ",RawatIntensif = 0 "
            SQL &= ",Obat = 0 "
            SQL &= ",Alkes = 0 "
            SQL &= ",BMHP = 0 "
            SQL &= ",AlatMedis = 0 "
            SQL &= ",ObatKronis = 0 "
            SQL &= ",ObatKemoterapi = 0 "

            SQL &= "FROM F_CASHIER_H A "
            SQL &= "INNER JOIN F_CASHIER_D_TRANSAKSI B "
            SQL &= "ON A.KDCASHIER = B.KDCASHIER  "
            SQL &= "INNER JOIN M_TARIF_NEW C  "
            SQL &= "ON B.KDTARIF = C.KDTARIF "
            SQL &= "INNER JOIN S_PENDAFTARAN_H D "
            SQL &= "ON A.KDREG = D.KDREG "


            SQL &= "WHERE A.KDREG = '" & sRegister & "' AND C.KDKATEGORI_BPJS = 6 "


            SQL &= ") UNION ALL( "

            SQL &= "SELECT "
            SQL &= "TanggalMasuk = D.DATE "
            SQL &= ",TanggalKeluar = D.DATEPULANG "
            SQL &= ",JenisRawat = D.CATEGORY "
            SQL &= ",Register = A.KDREG "
            SQL &= ",NoRM = A.KDCUSTOMER "
            SQL &= ",NoKartu = D.KARTUBPJS "
            SQL &= ",NIK = D.NIK "
            SQL &= ",Pasien = (SELECT AA.NAME_DISPLAY FROM M_CUSTOMER AA WHERE AA.KDCUSTOMER = A.KDCUSTOMER) "
            SQL &= ",TanggalLahir = (SELECT AA.TANGGALLAHIR FROM M_CUSTOMER AA WHERE AA.KDCUSTOMER = A.KDCUSTOMER) "
            SQL &= ",NomorSEP = ISNULL((D.NOMORSEP), '') "
            SQL &= ",Dokter = D.KDDOCTOR "
            SQL &= ",Penjamin = (SELECT BB.NAME_DISPLAY FROM M_DEBTOR BB WHERE BB.KDDEBTOR = A.KDDEBTOR) "
            SQL &= ",Tindakan = 'Penunjang' "
            SQL &= ",SEQ = B.SEQ "
            SQL &= ",Lainnya = 0 "
            SQL &= ",ProsedurNonBedah = 0 "
            SQL &= ",ProsedurBedah = 0 "
            SQL &= ",TenagaAhli = 0 "
            SQL &= ",Konsultasi = 0 "
            SQL &= ",Keperawatan = 0 "
            SQL &= ",Penunjang = B.GRANDTOTAL "
            SQL &= ",Radiologi = 0 "
            SQL &= ",Laboratorium = 0 "
            SQL &= ",PelayananDarah = 0 "
            SQL &= ",Rehabilitasi = 0 "
            SQL &= ",KamarAkomodasi = 0 "
            SQL &= ",RawatIntensif = 0 "
            SQL &= ",Obat = 0 "
            SQL &= ",Alkes = 0 "
            SQL &= ",BMHP = 0 "
            SQL &= ",AlatMedis = 0 "
            SQL &= ",ObatKronis = 0 "
            SQL &= ",ObatKemoterapi = 0 "

            SQL &= "FROM F_CASHIER_H A "
            SQL &= "INNER JOIN F_CASHIER_D_TRANSAKSI B "
            SQL &= "ON A.KDCASHIER = B.KDCASHIER  "
            SQL &= "INNER JOIN M_TARIF_NEW C  "
            SQL &= "ON B.KDTARIF = C.KDTARIF "
            SQL &= "INNER JOIN S_PENDAFTARAN_H D "
            SQL &= "ON A.KDREG = D.KDREG "


            SQL &= "WHERE A.KDREG = '" & sRegister & "' AND C.KDKATEGORI_BPJS = 7 "


            SQL &= ") UNION ALL( "

            SQL &= "SELECT "
            SQL &= "TanggalMasuk = D.DATE "
            SQL &= ",TanggalKeluar = D.DATEPULANG "
            SQL &= ",JenisRawat = D.CATEGORY "
            SQL &= ",Register = A.KDREG "
            SQL &= ",NoRM = A.KDCUSTOMER "
            SQL &= ",NoKartu = D.KARTUBPJS "
            SQL &= ",NIK = D.NIK "
            SQL &= ",Pasien = (SELECT AA.NAME_DISPLAY FROM M_CUSTOMER AA WHERE AA.KDCUSTOMER = A.KDCUSTOMER) "
            SQL &= ",TanggalLahir = (SELECT AA.TANGGALLAHIR FROM M_CUSTOMER AA WHERE AA.KDCUSTOMER = A.KDCUSTOMER) "
            SQL &= ",NomorSEP = ISNULL((D.NOMORSEP), '') "
            SQL &= ",Dokter = D.KDDOCTOR "
            SQL &= ",Penjamin = (SELECT BB.NAME_DISPLAY FROM M_DEBTOR BB WHERE BB.KDDEBTOR = A.KDDEBTOR) "
            SQL &= ",Tindakan = 'Radiologi' "
            SQL &= ",SEQ = B.SEQ "
            SQL &= ",Lainnya = 0 "
            SQL &= ",ProsedurNonBedah = 0 "
            SQL &= ",ProsedurBedah = 0 "
            SQL &= ",TenagaAhli = 0 "
            SQL &= ",Konsultasi = 0 "
            SQL &= ",Keperawatan = 0 "
            SQL &= ",Penunjang = 0 "
            SQL &= ",Radiologi = B.GRANDTOTAL "
            SQL &= ",Laboratorium = 0 "
            SQL &= ",PelayananDarah = 0 "
            SQL &= ",Rehabilitasi = 0 "
            SQL &= ",KamarAkomodasi = 0 "
            SQL &= ",RawatIntensif = 0 "
            SQL &= ",Obat = 0 "
            SQL &= ",Alkes = 0 "
            SQL &= ",BMHP = 0 "
            SQL &= ",AlatMedis = 0 "
            SQL &= ",ObatKronis = 0 "
            SQL &= ",ObatKemoterapi = 0 "

            SQL &= "FROM F_CASHIER_H A "
            SQL &= "INNER JOIN F_CASHIER_D_TRANSAKSI B "
            SQL &= "ON A.KDCASHIER = B.KDCASHIER  "
            SQL &= "INNER JOIN M_TARIF_NEW C  "
            SQL &= "ON B.KDTARIF = C.KDTARIF "
            SQL &= "INNER JOIN S_PENDAFTARAN_H D "
            SQL &= "ON A.KDREG = D.KDREG "


            SQL &= "WHERE A.KDREG = '" & sRegister & "' AND C.KDKATEGORI_BPJS = 8 "


            SQL &= ") UNION ALL( "

            SQL &= "SELECT "
            SQL &= "TanggalMasuk = D.DATE "
            SQL &= ",TanggalKeluar = D.DATEPULANG "
            SQL &= ",JenisRawat = D.CATEGORY "
            SQL &= ",Register = A.KDREG "
            SQL &= ",NoRM = A.KDCUSTOMER "
            SQL &= ",NoKartu = D.KARTUBPJS "
            SQL &= ",NIK = D.NIK "
            SQL &= ",Pasien = (SELECT AA.NAME_DISPLAY FROM M_CUSTOMER AA WHERE AA.KDCUSTOMER = A.KDCUSTOMER) "
            SQL &= ",TanggalLahir = (SELECT AA.TANGGALLAHIR FROM M_CUSTOMER AA WHERE AA.KDCUSTOMER = A.KDCUSTOMER) "
            SQL &= ",NomorSEP = ISNULL((D.NOMORSEP), '') "
            SQL &= ",Dokter = D.KDDOCTOR "
            SQL &= ",Penjamin = (SELECT BB.NAME_DISPLAY FROM M_DEBTOR BB WHERE BB.KDDEBTOR = A.KDDEBTOR) "
            SQL &= ",Tindakan = 'Laboratorium' "
            SQL &= ",SEQ = B.SEQ "
            SQL &= ",Lainnya = 0 "
            SQL &= ",ProsedurNonBedah = 0 "
            SQL &= ",ProsedurBedah = 0 "
            SQL &= ",TenagaAhli = 0 "
            SQL &= ",Konsultasi = 0 "
            SQL &= ",Keperawatan = 0 "
            SQL &= ",Penunjang = 0 "
            SQL &= ",Radiologi = 0 "
            SQL &= ",Laboratorium = B.GRANDTOTAL "
            SQL &= ",PelayananDarah = 0 "
            SQL &= ",Rehabilitasi = 0 "
            SQL &= ",KamarAkomodasi = 0 "
            SQL &= ",RawatIntensif = 0 "
            SQL &= ",Obat = 0 "
            SQL &= ",Alkes = 0 "
            SQL &= ",BMHP = 0 "
            SQL &= ",AlatMedis = 0 "
            SQL &= ",ObatKronis = 0 "
            SQL &= ",ObatKemoterapi = 0 "

            SQL &= "FROM F_CASHIER_H A "
            SQL &= "INNER JOIN F_CASHIER_D_TRANSAKSI B "
            SQL &= "ON A.KDCASHIER = B.KDCASHIER  "
            SQL &= "INNER JOIN M_TARIF_NEW C  "
            SQL &= "ON B.KDTARIF = C.KDTARIF "
            SQL &= "INNER JOIN S_PENDAFTARAN_H D "
            SQL &= "ON A.KDREG = D.KDREG "


            SQL &= "WHERE A.KDREG = '" & sRegister & "' AND C.KDKATEGORI_BPJS = 9 "


            SQL &= ") UNION ALL( "

            SQL &= "SELECT "
            SQL &= "TanggalMasuk = D.DATE "
            SQL &= ",TanggalKeluar = D.DATEPULANG "
            SQL &= ",JenisRawat = D.CATEGORY "
            SQL &= ",Register = A.KDREG "
            SQL &= ",NoRM = A.KDCUSTOMER "
            SQL &= ",NoKartu = D.KARTUBPJS "
            SQL &= ",NIK = D.NIK "
            SQL &= ",Pasien = (SELECT AA.NAME_DISPLAY FROM M_CUSTOMER AA WHERE AA.KDCUSTOMER = A.KDCUSTOMER) "
            SQL &= ",TanggalLahir = (SELECT AA.TANGGALLAHIR FROM M_CUSTOMER AA WHERE AA.KDCUSTOMER = A.KDCUSTOMER) "
            SQL &= ",NomorSEP = ISNULL((D.NOMORSEP), '') "
            SQL &= ",Dokter = D.KDDOCTOR "
            SQL &= ",Penjamin = (SELECT BB.NAME_DISPLAY FROM M_DEBTOR BB WHERE BB.KDDEBTOR = A.KDDEBTOR) "
            SQL &= ",Tindakan = 'PelayananDarah' "
            SQL &= ",SEQ = B.SEQ "
            SQL &= ",Lainnya = 0 "
            SQL &= ",ProsedurNonBedah = 0 "
            SQL &= ",ProsedurBedah = 0 "
            SQL &= ",TenagaAhli = 0 "
            SQL &= ",Konsultasi = 0 "
            SQL &= ",Keperawatan = 0 "
            SQL &= ",Penunjang = 0 "
            SQL &= ",Radiologi = 0 "
            SQL &= ",Laboratorium = 0 "
            SQL &= ",PelayananDarah = B.GRANDTOTAL "
            SQL &= ",Rehabilitasi = 0 "
            SQL &= ",KamarAkomodasi = 0 "
            SQL &= ",RawatIntensif = 0 "
            SQL &= ",Obat = 0 "
            SQL &= ",Alkes = 0 "
            SQL &= ",BMHP = 0 "
            SQL &= ",AlatMedis = 0 "
            SQL &= ",ObatKronis = 0 "
            SQL &= ",ObatKemoterapi = 0 "

            SQL &= "FROM F_CASHIER_H A "
            SQL &= "INNER JOIN F_CASHIER_D_TRANSAKSI B "
            SQL &= "ON A.KDCASHIER = B.KDCASHIER  "
            SQL &= "INNER JOIN M_TARIF_NEW C  "
            SQL &= "ON B.KDTARIF = C.KDTARIF "
            SQL &= "INNER JOIN S_PENDAFTARAN_H D "
            SQL &= "ON A.KDREG = D.KDREG "


            SQL &= "WHERE  A.KDREG = '" & sRegister & "' AND C.KDKATEGORI_BPJS = 10 "


            SQL &= ") UNION ALL( "

            SQL &= "SELECT "
            SQL &= "TanggalMasuk = D.DATE "
            SQL &= ",TanggalKeluar = D.DATEPULANG "
            SQL &= ",JenisRawat = D.CATEGORY "
            SQL &= ",Register = A.KDREG "
            SQL &= ",NoRM = A.KDCUSTOMER "
            SQL &= ",NoKartu = D.KARTUBPJS "
            SQL &= ",NIK = D.NIK "
            SQL &= ",Pasien = (SELECT AA.NAME_DISPLAY FROM M_CUSTOMER AA WHERE AA.KDCUSTOMER = A.KDCUSTOMER) "
            SQL &= ",TanggalLahir = (SELECT AA.TANGGALLAHIR FROM M_CUSTOMER AA WHERE AA.KDCUSTOMER = A.KDCUSTOMER) "
            SQL &= ",NomorSEP = ISNULL((D.NOMORSEP), '') "
            SQL &= ",Dokter = D.KDDOCTOR "
            SQL &= ",Penjamin = (SELECT BB.NAME_DISPLAY FROM M_DEBTOR BB WHERE BB.KDDEBTOR = A.KDDEBTOR) "
            SQL &= ",Tindakan = 'Rehabilitasi' "
            SQL &= ",SEQ = B.SEQ "
            SQL &= ",Lainnya = 0 "
            SQL &= ",ProsedurNonBedah = 0 "
            SQL &= ",ProsedurBedah = 0 "
            SQL &= ",TenagaAhli = 0 "
            SQL &= ",Konsultasi = 0 "
            SQL &= ",Keperawatan = 0 "
            SQL &= ",Penunjang = 0 "
            SQL &= ",Radiologi = 0 "
            SQL &= ",Laboratorium = 0 "
            SQL &= ",PelayananDarah = 0 "
            SQL &= ",Rehabilitasi = B.GRANDTOTAL "
            SQL &= ",KamarAkomodasi = 0 "
            SQL &= ",RawatIntensif = 0 "
            SQL &= ",Obat = 0 "
            SQL &= ",Alkes = 0 "
            SQL &= ",BMHP = 0 "
            SQL &= ",AlatMedis = 0 "
            SQL &= ",ObatKronis = 0 "
            SQL &= ",ObatKemoterapi = 0 "

            SQL &= "FROM F_CASHIER_H A "
            SQL &= "INNER JOIN F_CASHIER_D_TRANSAKSI B "
            SQL &= "ON A.KDCASHIER = B.KDCASHIER  "
            SQL &= "INNER JOIN M_TARIF_NEW C  "
            SQL &= "ON B.KDTARIF = C.KDTARIF "
            SQL &= "INNER JOIN S_PENDAFTARAN_H D "
            SQL &= "ON A.KDREG = D.KDREG "


            SQL &= "WHERE A.KDREG = '" & sRegister & "' AND C.KDKATEGORI_BPJS = 11 "


            SQL &= ") UNION ALL( "

            SQL &= "SELECT "
            SQL &= "TanggalMasuk = D.DATE "
            SQL &= ",TanggalKeluar = D.DATEPULANG "
            SQL &= ",JenisRawat = D.CATEGORY "
            SQL &= ",Register = A.KDREG "
            SQL &= ",NoRM = A.KDCUSTOMER "
            SQL &= ",NoKartu = D.KARTUBPJS "
            SQL &= ",NIK = D.NIK "
            SQL &= ",Pasien = (SELECT AA.NAME_DISPLAY FROM M_CUSTOMER AA WHERE AA.KDCUSTOMER = A.KDCUSTOMER) "
            SQL &= ",TanggalLahir = (SELECT AA.TANGGALLAHIR FROM M_CUSTOMER AA WHERE AA.KDCUSTOMER = A.KDCUSTOMER) "
            SQL &= ",NomorSEP = ISNULL((D.NOMORSEP), '') "
            SQL &= ",Dokter = D.KDDOCTOR "
            SQL &= ",Penjamin = (SELECT BB.NAME_DISPLAY FROM M_DEBTOR BB WHERE BB.KDDEBTOR = A.KDDEBTOR) "
            SQL &= ",Tindakan = 'Kamar Akomodasi' "
            SQL &= ",SEQ = B.SEQ "
            SQL &= ",Lainnya = 0 "
            SQL &= ",ProsedurNonBedah = 0 "
            SQL &= ",ProsedurBedah = 0 "
            SQL &= ",TenagaAhli = 0 "
            SQL &= ",Konsultasi = 0 "
            SQL &= ",Keperawatan = 0 "
            SQL &= ",Penunjang = 0 "
            SQL &= ",Radiologi = 0 "
            SQL &= ",Laboratorium = 0 "
            SQL &= ",PelayananDarah = 0 "
            SQL &= ",Rehabilitasi = 0 "
            SQL &= ",KamarAkomodasi = B.GRANDTOTAL "
            SQL &= ",RawatIntensif = 0 "
            SQL &= ",Obat = 0 "
            SQL &= ",Alkes = 0 "
            SQL &= ",BMHP = 0 "
            SQL &= ",AlatMedis = 0 "
            SQL &= ",ObatKronis = 0 "
            SQL &= ",ObatKemoterapi = 0 "

            SQL &= "FROM F_CASHIER_H A "
            SQL &= "INNER JOIN F_CASHIER_D_TRANSAKSI B "
            SQL &= "ON A.KDCASHIER = B.KDCASHIER  "
            SQL &= "INNER JOIN M_TARIF_NEW C  "
            SQL &= "ON B.KDTARIF = C.KDTARIF "
            SQL &= "INNER JOIN S_PENDAFTARAN_H D "
            SQL &= "ON A.KDREG = D.KDREG "


            SQL &= "WHERE A.KDREG = '" & sRegister & "' AND C.KDKATEGORI_BPJS = 12 "


            SQL &= ") UNION ALL( "

            SQL &= "SELECT "
            SQL &= "TanggalMasuk = D.DATE "
            SQL &= ",TanggalKeluar = D.DATEPULANG "
            SQL &= ",JenisRawat = D.CATEGORY "
            SQL &= ",Register = A.KDREG "
            SQL &= ",NoRM = A.KDCUSTOMER "
            SQL &= ",NoKartu = D.KARTUBPJS "
            SQL &= ",NIK = D.NIK "
            SQL &= ",Pasien = (SELECT AA.NAME_DISPLAY FROM M_CUSTOMER AA WHERE AA.KDCUSTOMER = A.KDCUSTOMER) "
            SQL &= ",TanggalLahir = (SELECT AA.TANGGALLAHIR FROM M_CUSTOMER AA WHERE AA.KDCUSTOMER = A.KDCUSTOMER) "
            SQL &= ",NomorSEP = ISNULL((D.NOMORSEP), '') "
            SQL &= ",Dokter = D.KDDOCTOR "
            SQL &= ",Penjamin = (SELECT BB.NAME_DISPLAY FROM M_DEBTOR BB WHERE BB.KDDEBTOR = A.KDDEBTOR) "
            SQL &= ",Tindakan = 'RawatIntensif' "
            SQL &= ",SEQ = B.SEQ "
            SQL &= ",Lainnya = 0 "
            SQL &= ",ProsedurNonBedah = 0 "
            SQL &= ",ProsedurBedah = 0 "
            SQL &= ",TenagaAhli = 0 "
            SQL &= ",Konsultasi = 0 "
            SQL &= ",Keperawatan = 0 "
            SQL &= ",Penunjang = 0 "
            SQL &= ",Radiologi = 0 "
            SQL &= ",Laboratorium = 0 "
            SQL &= ",PelayananDarah = 0 "
            SQL &= ",Rehabilitasi = 0 "
            SQL &= ",KamarAkomodasi = 0 "
            SQL &= ",RawatIntensif = B.GRANDTOTAL "
            SQL &= ",Obat = 0 "
            SQL &= ",Alkes = 0 "
            SQL &= ",BMHP = 0 "
            SQL &= ",AlatMedis = 0 "
            SQL &= ",ObatKronis = 0 "
            SQL &= ",ObatKemoterapi = 0 "

            SQL &= "FROM F_CASHIER_H A "
            SQL &= "INNER JOIN F_CASHIER_D_TRANSAKSI B "
            SQL &= "ON A.KDCASHIER = B.KDCASHIER  "
            SQL &= "INNER JOIN M_TARIF_NEW C  "
            SQL &= "ON B.KDTARIF = C.KDTARIF "
            SQL &= "INNER JOIN S_PENDAFTARAN_H D "
            SQL &= "ON A.KDREG = D.KDREG "


            SQL &= "WHERE A.KDREG = '" & sRegister & "' AND C.KDKATEGORI_BPJS = 13 "


            SQL &= ") UNION ALL( "

            SQL &= "SELECT "
            SQL &= "TanggalMasuk = D.DATE "
            SQL &= ",TanggalKeluar = D.DATEPULANG "
            SQL &= ",JenisRawat = D.CATEGORY "
            SQL &= ",Register = A.KDREG "
            SQL &= ",NoRM = A.KDCUSTOMER "
            SQL &= ",NoKartu = D.KARTUBPJS "
            SQL &= ",NIK = D.NIK "
            SQL &= ",Pasien = (SELECT AA.NAME_DISPLAY FROM M_CUSTOMER AA WHERE AA.KDCUSTOMER = A.KDCUSTOMER) "
            SQL &= ",TanggalLahir = (SELECT AA.TANGGALLAHIR FROM M_CUSTOMER AA WHERE AA.KDCUSTOMER = A.KDCUSTOMER) "
            SQL &= ",NomorSEP = ISNULL((D.NOMORSEP), '') "
            SQL &= ",Dokter = D.KDDOCTOR "
            SQL &= ",Penjamin = (SELECT BB.NAME_DISPLAY FROM M_DEBTOR BB WHERE BB.KDDEBTOR = A.KDDEBTOR) "
            SQL &= ",Tindakan = 'AlatMedis' "
            SQL &= ",SEQ = B.SEQ "
            SQL &= ",Lainnya = 0 "
            SQL &= ",ProsedurNonBedah = 0 "
            SQL &= ",ProsedurBedah = 0 "
            SQL &= ",TenagaAhli = 0 "
            SQL &= ",Konsultasi = 0 "
            SQL &= ",Keperawatan = 0 "
            SQL &= ",Penunjang = 0 "
            SQL &= ",Radiologi = 0 "
            SQL &= ",Laboratorium = 0 "
            SQL &= ",PelayananDarah = 0 "
            SQL &= ",Rehabilitasi = 0 "
            SQL &= ",KamarAkomodasi = 0 "
            SQL &= ",RawatIntensif = 0 "
            SQL &= ",Obat = 0 "
            SQL &= ",Alkes = 0 "
            SQL &= ",BMHP = 0 "
            SQL &= ",AlatMedis = B.GRANDTOTAL "
            SQL &= ",ObatKronis = 0 "
            SQL &= ",ObatKemoterapi = 0 "

            SQL &= "FROM F_CASHIER_H A "
            SQL &= "INNER JOIN F_CASHIER_D_TRANSAKSI B "
            SQL &= "ON A.KDCASHIER = B.KDCASHIER  "
            SQL &= "INNER JOIN M_TARIF_NEW C  "
            SQL &= "ON B.KDTARIF = C.KDTARIF "
            SQL &= "INNER JOIN S_PENDAFTARAN_H D "
            SQL &= "ON A.KDREG = D.KDREG "

            SQL &= "WHERE  A.KDREG = '" & sRegister & "' AND C.KDKATEGORI_BPJS = 17 "

            'SQL &= ") UNION ALL( "

            'SQL &= "SELECT "
            'SQL &= "TanggalMasuk = D.DATE "
            'SQL &= ",TanggalKeluar = D.DATEPULANG "
            'SQL &= ",JenisRawat = D.CATEGORY "
            'SQL &= ",Register = A.KDREG "
            'SQL &= ",NoRM = A.KDCUSTOMER "
            'SQL &= ",NoKartu = D.KARTUBPJS "
            'SQL &= ",NIK = D.NIK "
            'SQL &= ",Pasien = (SELECT AA.NAME_DISPLAY FROM M_CUSTOMER AA WHERE AA.KDCUSTOMER = A.KDCUSTOMER) "
            'SQL &= ",TanggalLahir = (SELECT AA.TANGGALLAHIR FROM M_CUSTOMER AA WHERE AA.KDCUSTOMER = A.KDCUSTOMER) "
            'SQL &= ",NomorSEP = ISNULL((D.NOMORSEP), '') "
            'SQL &= ",Dokter = D.KDDOCTOR "
            'SQL &= ",Penjamin = (SELECT BB.NAME_DISPLAY FROM M_DEBTOR BB WHERE BB.KDDEBTOR = A.KDDEBTOR) "
            'SQL &= ",Tindakan = 'AlatMedis' "
            'SQL &= ",SEQ = B.SEQ "
            'SQL &= ",Lainnya = 0 "
            'SQL &= ",ProsedurNonBedah = 0 "
            'SQL &= ",ProsedurBedah = 0 "
            'SQL &= ",TenagaAhli = 0 "
            'SQL &= ",Konsultasi = 0 "
            'SQL &= ",Keperawatan = 0 "
            'SQL &= ",Penunjang = 0 "
            'SQL &= ",Radiologi = 0 "
            'SQL &= ",Laboratorium = 0 "
            'SQL &= ",PelayananDarah = 0 "
            'SQL &= ",Rehabilitasi = 0 "
            'SQL &= ",KamarAkomodasi = 0 "
            'SQL &= ",RawatIntensif = 0 "
            'SQL &= ",Obat = 0 "
            'SQL &= ",Alkes = 0 "
            'SQL &= ",BMHP = 0 "
            'SQL &= ",AlatMedis = B.GRANDTOTAL "
            'SQL &= ",ObatKronis = 0 "
            'SQL &= ",ObatKemoterapi = 0 "

            'SQL &= "FROM F_CASHIER_H A "
            'SQL &= "INNER JOIN F_CASHIER_D B "
            'SQL &= "ON A.KDCASHIER = B.KDCASHIER "
            'SQL &= "INNER JOIN S_PENDAFTARAN_H D "
            'SQL &= "ON A.KDREG = D.KDREG "
            'SQL &= "INNER JOIN M_ITEM H "
            'SQL &= "ON B.KDITEM = H.KDITEM "
            'SQL &= "INNER JOIN M_UOM I "
            'SQL &= "ON B.KDUOM = I.KDUOM "

            'SQL &= "WHERE A.KDREG = '" & sRegister & "' AND B.KDKATEGORI_BPJS = 17 "

            SQL &= ") UNION ALL( "

            SQL &= "SELECT "
            SQL &= "TanggalMasuk = D.DATE "
            SQL &= ",TanggalKeluar = D.DATEPULANG "
            SQL &= ",JenisRawat = D.CATEGORY "
            SQL &= ",Register = A.KDREG "
            SQL &= ",NoRM = A.KDCUSTOMER "
            SQL &= ",NoKartu = D.KARTUBPJS "
            SQL &= ",NIK = D.NIK "
            SQL &= ",Pasien = (SELECT AA.NAME_DISPLAY FROM M_CUSTOMER AA WHERE AA.KDCUSTOMER = A.KDCUSTOMER) "
            SQL &= ",TanggalLahir = (SELECT AA.TANGGALLAHIR FROM M_CUSTOMER AA WHERE AA.KDCUSTOMER = A.KDCUSTOMER) "
            SQL &= ",NomorSEP = ISNULL((D.NOMORSEP), '') "
            SQL &= ",Dokter = D.KDDOCTOR "
            SQL &= ",Penjamin = (SELECT BB.NAME_DISPLAY FROM M_DEBTOR BB WHERE BB.KDDEBTOR = A.KDDEBTOR) "
            SQL &= ",Tindakan = 'Lainnya' "
            SQL &= ",SEQ = B.SEQ "
            SQL &= ",Lainnya = B.GRANDTOTAL "
            SQL &= ",ProsedurNonBedah = 0 "
            SQL &= ",ProsedurBedah = 0 "
            SQL &= ",TenagaAhli = 0 "
            SQL &= ",Konsultasi = 0 "
            SQL &= ",Keperawatan = 0 "
            SQL &= ",Penunjang = 0 "
            SQL &= ",Radiologi = 0 "
            SQL &= ",Laboratorium = 0 "
            SQL &= ",PelayananDarah = 0 "
            SQL &= ",Rehabilitasi = 0 "
            SQL &= ",KamarAkomodasi = 0 "
            SQL &= ",RawatIntensif = 0 "
            SQL &= ",Obat = 0 "
            SQL &= ",Alkes = 0 "
            SQL &= ",BMHP = 0 "
            SQL &= ",AlatMedis = 0 "
            SQL &= ",ObatKronis = 0 "
            SQL &= ",ObatKemoterapi = 0 "

            SQL &= "FROM F_CASHIER_H A "
            SQL &= "INNER JOIN F_CASHIER_D B "
            SQL &= "ON A.KDCASHIER = B.KDCASHIER "
            SQL &= "INNER JOIN S_PENDAFTARAN_H D "
            SQL &= "ON A.KDREG = D.KDREG "
            SQL &= "INNER JOIN M_ITEM H "
            SQL &= "ON B.KDITEM = H.KDITEM "
            SQL &= "INNER JOIN M_UOM I "
            SQL &= "ON B.KDUOM = I.KDUOM "


            SQL &= "WHERE A.KDREG = '" & sRegister & "' AND B.KDKATEGORI_BPJS = 1 "


            SQL &= ") UNION ALL( "

            SQL &= "SELECT "
            SQL &= "TanggalMasuk = D.DATE "
            SQL &= ",TanggalKeluar = D.DATEPULANG "
            SQL &= ",JenisRawat = D.CATEGORY "
            SQL &= ",Register = A.KDREG "
            SQL &= ",NoRM = A.KDCUSTOMER "
            SQL &= ",NoKartu = D.KARTUBPJS "
            SQL &= ",NIK = D.NIK "
            SQL &= ",Pasien = (SELECT AA.NAME_DISPLAY FROM M_CUSTOMER AA WHERE AA.KDCUSTOMER = A.KDCUSTOMER) "
            SQL &= ",TanggalLahir = (SELECT AA.TANGGALLAHIR FROM M_CUSTOMER AA WHERE AA.KDCUSTOMER = A.KDCUSTOMER) "
            SQL &= ",NomorSEP = ISNULL((D.NOMORSEP), '') "
            SQL &= ",Dokter = D.KDDOCTOR "
            SQL &= ",Penjamin = (SELECT BB.NAME_DISPLAY FROM M_DEBTOR BB WHERE BB.KDDEBTOR = A.KDDEBTOR) "
            SQL &= ",Tindakan = 'Obat' "
            SQL &= ",SEQ = B.SEQ "
            SQL &= ",Lainnya = 0 "
            SQL &= ",ProsedurNonBedah = 0 "
            SQL &= ",ProsedurBedah = 0 "
            SQL &= ",TenagaAhli = 0 "
            SQL &= ",Konsultasi = 0 "
            SQL &= ",Keperawatan = 0 "
            SQL &= ",Penunjang = 0 "
            SQL &= ",Radiologi = 0 "
            SQL &= ",Laboratorium = 0 "
            SQL &= ",PelayananDarah = 0 "
            SQL &= ",Rehabilitasi = 0 "
            SQL &= ",KamarAkomodasi = 0 "
            SQL &= ",RawatIntensif = 0 "
            SQL &= ",Obat = B.GRANDTOTAL "
            SQL &= ",Alkes = 0 "
            SQL &= ",BMHP = 0 "
            SQL &= ",AlatMedis = 0 "
            SQL &= ",ObatKronis = 0 "
            SQL &= ",ObatKemoterapi = 0 "

            SQL &= "FROM F_CASHIER_H A "
            SQL &= "INNER JOIN F_CASHIER_D B "
            SQL &= "ON A.KDCASHIER = B.KDCASHIER "
            SQL &= "INNER JOIN S_PENDAFTARAN_H D "
            SQL &= "ON A.KDREG = D.KDREG "
            SQL &= "INNER JOIN M_ITEM H "
            SQL &= "ON B.KDITEM = H.KDITEM "
            SQL &= "INNER JOIN M_UOM I "
            SQL &= "ON B.KDUOM = I.KDUOM "


            'SQL &= "WHERE A.KDREG = '" & sRegister & "' AND B.KDKATEGORI_BPJS = 14 "
            SQL &= "WHERE A.KDREG = '" & sRegister & "' AND B.ISPROLANIS = 0 AND B.KDKATEGORI_BPJS <> 15 AND A.KDREG = '" & sRegister & "' AND B.ISPROLANIS = 0 AND B.KDKATEGORI_BPJS <> 16 "


            SQL &= ") UNION ALL( "

            SQL &= "SELECT "
            SQL &= "TanggalMasuk = D.DATE "
            SQL &= ",TanggalKeluar = D.DATEPULANG "
            SQL &= ",JenisRawat = D.CATEGORY "
            SQL &= ",Register = A.KDREG "
            SQL &= ",NoRM = A.KDCUSTOMER "
            SQL &= ",NoKartu = D.KARTUBPJS "
            SQL &= ",NIK = D.NIK "
            SQL &= ",Pasien = (SELECT AA.NAME_DISPLAY FROM M_CUSTOMER AA WHERE AA.KDCUSTOMER = A.KDCUSTOMER) "
            SQL &= ",TanggalLahir = (SELECT AA.TANGGALLAHIR FROM M_CUSTOMER AA WHERE AA.KDCUSTOMER = A.KDCUSTOMER) "
            SQL &= ",NomorSEP = ISNULL((D.NOMORSEP), '') "
            SQL &= ",Dokter = D.KDDOCTOR "
            SQL &= ",Penjamin = (SELECT BB.NAME_DISPLAY FROM M_DEBTOR BB WHERE BB.KDDEBTOR = A.KDDEBTOR) "
            SQL &= ",Tindakan = 'Alkes' "
            SQL &= ",SEQ = B.SEQ "
            SQL &= ",Lainnya = 0 "
            SQL &= ",ProsedurNonBedah = 0 "
            SQL &= ",ProsedurBedah = 0 "
            SQL &= ",TenagaAhli = 0 "
            SQL &= ",Konsultasi = 0 "
            SQL &= ",Keperawatan = 0 "
            SQL &= ",Penunjang = 0 "
            SQL &= ",Radiologi = 0 "
            SQL &= ",Laboratorium = 0 "
            SQL &= ",PelayananDarah = 0 "
            SQL &= ",Rehabilitasi = 0 "
            SQL &= ",KamarAkomodasi = 0 "
            SQL &= ",RawatIntensif = 0 "
            SQL &= ",Obat = 0 "
            SQL &= ",Alkes = B.GRANDTOTAL "
            SQL &= ",BMHP = 0 "
            SQL &= ",AlatMedis = 0 "
            SQL &= ",ObatKronis = 0 "
            SQL &= ",ObatKemoterapi = 0 "

            SQL &= "FROM F_CASHIER_H A "
            SQL &= "INNER JOIN F_CASHIER_D B "
            SQL &= "ON A.KDCASHIER = B.KDCASHIER "
            SQL &= "INNER JOIN S_PENDAFTARAN_H D "
            SQL &= "ON A.KDREG = D.KDREG "
            SQL &= "INNER JOIN M_ITEM H "
            SQL &= "ON B.KDITEM = H.KDITEM "
            SQL &= "INNER JOIN M_UOM I "
            SQL &= "ON B.KDUOM = I.KDUOM "


            SQL &= "WHERE A.KDREG = '" & sRegister & "' AND B.KDKATEGORI_BPJS = 15 "


            SQL &= ") UNION ALL( "

            SQL &= "SELECT "
            SQL &= "TanggalMasuk = D.DATE "
            SQL &= ",TanggalKeluar = D.DATEPULANG "
            SQL &= ",JenisRawat = D.CATEGORY "
            SQL &= ",Register = A.KDREG "
            SQL &= ",NoRM = A.KDCUSTOMER "
            SQL &= ",NoKartu = D.KARTUBPJS "
            SQL &= ",NIK = D.NIK "
            SQL &= ",Pasien = (SELECT AA.NAME_DISPLAY FROM M_CUSTOMER AA WHERE AA.KDCUSTOMER = A.KDCUSTOMER) "
            SQL &= ",TanggalLahir = (SELECT AA.TANGGALLAHIR FROM M_CUSTOMER AA WHERE AA.KDCUSTOMER = A.KDCUSTOMER) "
            SQL &= ",NomorSEP = ISNULL((D.NOMORSEP), '') "
            SQL &= ",Dokter = D.KDDOCTOR "
            SQL &= ",Penjamin = (SELECT BB.NAME_DISPLAY FROM M_DEBTOR BB WHERE BB.KDDEBTOR = A.KDDEBTOR) "
            SQL &= ",Tindakan = 'BMHP' "
            SQL &= ",SEQ = B.SEQ "
            SQL &= ",Lainnya = 0 "
            SQL &= ",ProsedurNonBedah = 0 "
            SQL &= ",ProsedurBedah = 0 "
            SQL &= ",TenagaAhli = 0 "
            SQL &= ",Konsultasi = 0 "
            SQL &= ",Keperawatan = 0 "
            SQL &= ",Penunjang = 0 "
            SQL &= ",Radiologi = 0 "
            SQL &= ",Laboratorium = 0 "
            SQL &= ",PelayananDarah = 0 "
            SQL &= ",Rehabilitasi = 0 "
            SQL &= ",KamarAkomodasi = 0 "
            SQL &= ",RawatIntensif = 0 "
            SQL &= ",Obat = 0 "
            SQL &= ",Alkes = 0 "
            SQL &= ",BMHP = B.GRANDTOTAL "
            SQL &= ",AlatMedis = 0 "
            SQL &= ",ObatKronis = 0 "
            SQL &= ",ObatKemoterapi = 0 "

            SQL &= "FROM F_CASHIER_H A "
            SQL &= "INNER JOIN F_CASHIER_D B "
            SQL &= "ON A.KDCASHIER = B.KDCASHIER "
            SQL &= "INNER JOIN S_PENDAFTARAN_H D "
            SQL &= "ON A.KDREG = D.KDREG "
            SQL &= "INNER JOIN M_ITEM H "
            SQL &= "ON B.KDITEM = H.KDITEM "
            SQL &= "INNER JOIN M_UOM I "
            SQL &= "ON B.KDUOM = I.KDUOM "


            SQL &= "WHERE A.KDREG = '" & sRegister & "' AND B.KDKATEGORI_BPJS = 16 "

            SQL &= ") UNION ALL( "

            SQL &= "SELECT "
            SQL &= "TanggalMasuk = D.DATE "
            SQL &= ",TanggalKeluar = D.DATEPULANG "
            SQL &= ",JenisRawat = D.CATEGORY "
            SQL &= ",Register = A.KDREG "
            SQL &= ",NoRM = A.KDCUSTOMER "
            SQL &= ",NoKartu = D.KARTUBPJS "
            SQL &= ",NIK = D.NIK "
            SQL &= ",Pasien = (SELECT AA.NAME_DISPLAY FROM M_CUSTOMER AA WHERE AA.KDCUSTOMER = A.KDCUSTOMER) "
            SQL &= ",TanggalLahir = (SELECT AA.TANGGALLAHIR FROM M_CUSTOMER AA WHERE AA.KDCUSTOMER = A.KDCUSTOMER) "
            SQL &= ",NomorSEP = ISNULL((D.NOMORSEP), '') "
            SQL &= ",Dokter = D.KDDOCTOR "
            SQL &= ",Penjamin = (SELECT BB.NAME_DISPLAY FROM M_DEBTOR BB WHERE BB.KDDEBTOR = A.KDDEBTOR) "
            SQL &= ",Tindakan = 'BMHP' "
            SQL &= ",SEQ = B.SEQ "
            SQL &= ",Lainnya = 0 "
            SQL &= ",ProsedurNonBedah = 0 "
            SQL &= ",ProsedurBedah = 0 "
            SQL &= ",TenagaAhli = 0 "
            SQL &= ",Konsultasi = 0 "
            SQL &= ",Keperawatan = 0 "
            SQL &= ",Penunjang = 0 "
            SQL &= ",Radiologi = 0 "
            SQL &= ",Laboratorium = 0 "
            SQL &= ",PelayananDarah = 0 "
            SQL &= ",Rehabilitasi = 0 "
            SQL &= ",KamarAkomodasi = 0 "
            SQL &= ",RawatIntensif = 0 "
            SQL &= ",Obat = 0 "
            SQL &= ",Alkes = 0 "
            SQL &= ",BMHP = B.GRANDTOTAL "
            SQL &= ",AlatMedis = 0 "
            SQL &= ",ObatKronis = 0 "
            SQL &= ",ObatKemoterapi = 0 "

            SQL &= "FROM F_CASHIER_H A "
            SQL &= "INNER JOIN F_CASHIER_D_TRANSAKSI B "
            SQL &= "ON A.KDCASHIER = B.KDCASHIER  "
            SQL &= "INNER JOIN M_TARIF_NEW C  "
            SQL &= "ON B.KDTARIF = C.KDTARIF "
            SQL &= "INNER JOIN S_PENDAFTARAN_H D "
            SQL &= "ON A.KDREG = D.KDREG "


            SQL &= "WHERE A.KDREG = '" & sRegister & "' AND C.KDKATEGORI_BPJS = 16 "


            SQL &= ") UNION ALL( "

            SQL &= "SELECT "
            SQL &= "TanggalMasuk = D.DATE "
            SQL &= ",TanggalKeluar = D.DATEPULANG "
            SQL &= ",JenisRawat = D.CATEGORY "
            SQL &= ",Register = A.KDREG "
            SQL &= ",NoRM = A.KDCUSTOMER "
            SQL &= ",NoKartu = D.KARTUBPJS "
            SQL &= ",NIK = D.NIK "
            SQL &= ",Pasien = (SELECT AA.NAME_DISPLAY  + ' ' + IIf(AA.FRONT_TITLE IS NULL, '', AA.FRONT_TITLE + ' ') + AA.BACK_TITLE FROM M_CUSTOMER AA WHERE AA.KDCUSTOMER = A.KDCUSTOMER) "
            SQL &= ",TanggalLahir = (SELECT AA.TANGGALLAHIR FROM M_CUSTOMER AA WHERE AA.KDCUSTOMER = A.KDCUSTOMER) "
            SQL &= ",NomorSEP = ISNULL((D.NOMORSEP), '') "
            SQL &= ",Dokter = D.KDDOCTOR "
            SQL &= ",Penjamin = (SELECT BB.NAME_DISPLAY FROM M_DEBTOR BB WHERE BB.KDDEBTOR = A.KDDEBTOR) "
            SQL &= ",Tindakan = 'ObatKronis' "
            SQL &= ",SEQ = B.SEQ "
            SQL &= ",Lainnya = 0 "
            SQL &= ",ProsedurNonBedah = 0 "
            SQL &= ",ProsedurBedah = 0 "
            SQL &= ",TenagaAhli = 0 "
            SQL &= ",Konsultasi = 0 "
            SQL &= ",Keperawatan = 0 "
            SQL &= ",Penunjang = 0 "
            SQL &= ",Radiologi = 0 "
            SQL &= ",Laboratorium = 0 "
            SQL &= ",PelayananDarah = 0 "
            SQL &= ",Rehabilitasi = 0 "
            SQL &= ",KamarAkomodasi = 0 "
            SQL &= ",RawatIntensif = 0 "
            SQL &= ",Obat = 0 "
            SQL &= ",Alkes = 0 "
            SQL &= ",BMHP = 0 "
            SQL &= ",AlatMedis = 0 "
            SQL &= ",ObatKronis = B.GRANDTOTAL "
            SQL &= ",ObatKemoterapi = 0 "

            SQL &= "FROM F_CASHIER_H A "
            SQL &= "INNER JOIN F_CASHIER_D B "
            SQL &= "ON A.KDCASHIER = B.KDCASHIER "
            SQL &= "INNER JOIN S_PENDAFTARAN_H D "
            SQL &= "ON A.KDREG = D.KDREG "
            SQL &= "INNER JOIN M_ITEM H "
            SQL &= "ON B.KDITEM = H.KDITEM "
            SQL &= "INNER JOIN M_UOM I "
            SQL &= "ON B.KDUOM = I.KDUOM "

            'SQL &= "WHERE A.KDREG = '" & sRegister & "' AND B.KDKATEGORI_BPJS = 18 "
            SQL &= "WHERE A.KDREG = '" & sRegister & "' AND B.ISPROLANIS = 1 AND B.KDKATEGORI_BPJS <> 15 AND A.KDREG = '" & sRegister & "' AND B.ISPROLANIS = 1 AND B.KDKATEGORI_BPJS <> 16 "
#End Region
            SQL &= ") "
            SQL &= ") AS A "
            SQL &= "GROUP BY A.TanggalMasuk, A.TanggalKeluar, A.JenisRawat, A.Register, A.NoRM, A.NoKartu, A.NIK, A.Pasien, A.TanggalLahir, A.NomorSEP, A.Dokter, A.Penjamin, A.Tindakan "

            ' , A.ProsedurNonBedah, A.Penunjang, A.Radiologi, A.SEQ, A.Lainnya,  A.ProsedurBedah, A.TenagaAhli, A.Konsultasi, A.Keperawatan, A.Laboratorium, A.PelayananDarah, A.Rehabilitasi, A.KamarAkomodasi, A.RawatIntensif, A.Obat, A.Alkes, A.BMHP, A.AlatMedis, A.ObatKronis, A.ObatKemoterapi

            SQL &= "ORDER BY A.TanggalMasuk ASC"

            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)

            da.Fill(ds, "S_GROUPER_H")

            sBedah = 0
            sNonBedah = 0
            sKonsultasig = 0
            sTenagaAhli = 0
            sKeperawatan = 0
            sPenunjang = 0
            sRadiologi = 0
            sLab = 0
            sPelayananDarah = 0
            sRehabilitasi = 0
            sKamar = 0
            sRawatIntensif = 0
            sObat = 0
            sAlkes = 0
            sObatKronis = 0
            sObatKemoterpi = 0
            sBMHP = 0
            sSewaAlatMedis = 0

            If ds.Tables("S_GROUPER_H").Rows.Count > 1 Then
                For xloop As Integer = 0 To ds.Tables("S_GROUPER_H").Rows.Count - 1
                    sBedah = sBedah + ds.Tables("S_GROUPER_H").Rows(xloop)("ProsedurBedah")
                    sNonBedah = sNonBedah + ds.Tables("S_GROUPER_H").Rows(xloop)("ProsedurNonBedah")
                    sKonsultasig = sKonsultasig + ds.Tables("S_GROUPER_H").Rows(xloop)("Konsultasi")
                    sTenagaAhli = sTenagaAhli + ds.Tables("S_GROUPER_H").Rows(xloop)("TenagaAhli")
                    sKeperawatan = sKeperawatan + ds.Tables("S_GROUPER_H").Rows(xloop)("Keperawatan")
                    sPenunjang = sPenunjang + ds.Tables("S_GROUPER_H").Rows(xloop)("Penunjang")
                    sRadiologi = sRadiologi + ds.Tables("S_GROUPER_H").Rows(xloop)("Radiologi")
                    sLab = sLab + ds.Tables("S_GROUPER_H").Rows(xloop)("Laboratorium")
                    sPelayananDarah = sPelayananDarah + ds.Tables("S_GROUPER_H").Rows(xloop)("PelayananDarah")
                    sRehabilitasi = sRehabilitasi + ds.Tables("S_GROUPER_H").Rows(xloop)("Rehabilitasi")
                    sKamar = sKamar + ds.Tables("S_GROUPER_H").Rows(xloop)("KamarAkomodasi")
                    sRawatIntensif = sRawatIntensif + ds.Tables("S_GROUPER_H").Rows(xloop)("RawatIntensif")
                    sObat = sObat + ds.Tables("S_GROUPER_H").Rows(xloop)("Obat")
                    sAlkes = sAlkes + ds.Tables("S_GROUPER_H").Rows(xloop)("Alkes")
                    sObatKronis = sObatKronis + ds.Tables("S_GROUPER_H").Rows(xloop)("ObatKronis")
                    sObatKemoterpi = sObatKemoterpi + ds.Tables("S_GROUPER_H").Rows(xloop)("ObatKemoterapi")
                    sBMHP = sBMHP + ds.Tables("S_GROUPER_H").Rows(xloop)("BMHP")
                    sSewaAlatMedis = sSewaAlatMedis + ds.Tables("S_GROUPER_H").Rows(xloop)("AlatMedis")
                Next

                'Else
                '    MsgBox("Transaksi Masih Kosong", MsgBoxStyle.Exclamation, Me.Text)
            End If

            If sBedah + sNonBedah + sKonsultasig + sTenagaAhli + sKeperawatan + sPenunjang + sRadiologi + sLab + sPelayananDarah + sRehabilitasi + sKamar + sRawatIntensif + sObat + sAlkes + sObatKronis + sBMHP + sSewaAlatMedis = 0 Then
                sKonsultasig = 50000
            End If

            If oConn.State = ConnectionState.Open Then
                oConn.Close()
            End If
        Catch ex As Exception
            MsgBox("Load List Data : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadDataGrouper()
        Try
            Dim oGrouper As New Reference.clsReqGrouperTest

            ' ***** HEADER *****
            Dim ds = oGrouper.GetData(lblRegister.Text)

            If ds IsNot Nothing Then
                lblTotalGrpuperCPPT.Text = FormatNumber(ds.TARIFF, 0)
                TextBox1.Text = FormatNumber(ds.TARIFF, 0)
                'txtCATATANGROUPER.Text = ds.CATATAN
                TextBox2.Text = ds.CATATAN
                lblGrouperHasil.Text = ds.CATATAN
            End If

        Catch oErr As Exception
            MsgBox("Load List Data : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Function fn_SaveGrouper(ByVal TARIFF As Decimal, ByVal RESPON As String, ByVal CATATAN As String) As Boolean
        Try
            ' ***** HEADER *****
            Dim oGrouper As New Reference.clsReqGrouperTest
            Dim KodeGrouper As String = String.Empty

            Dim dsGrouper = oGrouper.GetData(lblRegister.Text)

            If dsGrouper IsNot Nothing Then
                KodeGrouper = dsGrouper.KDREG
            End If

            Dim ds = oGrouper.GetStructureHeader
            With ds
                .KDREG = lblRegister.Text
                .KATEGORI = ""
                .RESPON = RESPON
                .TARIFF = TARIFF
                .CATATAN = CATATAN
            End With

            If KodeGrouper = String.Empty Then
                Try
                    fn_SaveGrouper = oGrouper.InsertData(ds)
                Catch ex As Exception
                    MsgBox("Simpan Data : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            Else
                Try
                    fn_SaveGrouper = oGrouper.UpdateData(ds)
                Catch ex As Exception
                    MsgBox("Simpan Data : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            End If
        Catch oErr As Exception
            MsgBox("Simpan Data : " & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            fn_SaveGrouper = False
        End Try
    End Function
    Private Function fn_Untukmengambildatadetailperklaim(ByVal nomorsep As String) As Boolean
        Try
            fn_Untukmengambildatadetailperklaim = False

            Dim jsonEncode As String = "{" & """metadata"": {" & """method"": " & """get_claim_data""    }," & """data"": {" & """nomor_sep"": """ & "" & nomorsep & "" & """} } "

            Dim cek As String = oBrigging.fn_GetRequsetEklaim(jsonEncode)

            If cek.Contains("xx400-") Then
                MsgBox("Koneksi " & cek, MsgBoxStyle.Exclamation, Me.Text)
                Exit Function
            End If

            Dim jsonDecode = JObject.Parse(cek)
            Dim sDataDuplicate As String = String.Empty
            Dim smessage As String = String.Empty

            sDataDuplicate = jsonDecode("metadata")("code").ToString
            smessage = jsonDecode("metadata")("message").ToString

            If sDataDuplicate = "200" Then
                If jsonDecode("response")("data")("coder_nm") <> "INACBG" Then
                    fn_Untukmengambildatadetailperklaim = True
                Else
                    fn_Untukmengambildatadetailperklaim = False
                End If
            Else
                fn_Untukmengambildatadetailperklaim = True
            End If
        Catch oErr As Exception
            MsgBox("Membuat Klaim Baru: " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Function
    Private Function fn_Membuatklaimbaru() As Boolean
        Try
            fn_Membuatklaimbaru = False

            Dim jsonEncode As String = "{" & """metadata"": {" & """method"": " & """new_claim""    }," & """data"": {" & """nomor_kartu"": """ & "" & lblKartuBPJS.Text & "" & """, " & """nomor_sep"": """ & "" & lblNOMORSEP.Text & "" & """, " & """nomor_rm"": """ & "" & lblNoRM.Text & "" & """, " & """nama_pasien"": """ & "" & lblNamaPasien.Text & "" & """, " & """tgl_lahir"": """ & "" & sTanggalLahir.ToString("yyyy-MM-dd HH:mm:ss") & "" & """, " & """gender"": """ & "" & IIf(lblJenisKelamin.Text = "Laki-laki", "1", "2") & "" & """   } } "

            Dim cek As String = oBrigging.fn_Membuatklaimbaru(jsonEncode)

            If cek.Contains("xx400-") Then
                MsgBox("Koneksi " & cek, MsgBoxStyle.Exclamation, Me.Text)
                Exit Function
            End If

            Dim jsonDecode = JObject.Parse(cek)
            Dim sDataDuplicate As String = String.Empty
            Dim smessage As String = String.Empty

            sDataDuplicate = jsonDecode("metadata")("code").ToString
            smessage = jsonDecode("metadata")("message").ToString

            If sDataDuplicate = "200" Or sDataDuplicate = "400" Then
                fn_Membuatklaimbaru = True
            Else
                MsgBox("Membuat Klaim Baru" & vbCrLf & sDataDuplicate & "-" & smessage, MsgBoxStyle.Information, Me.Text)
            End If
        Catch oErr As Exception
            MsgBox("Membuat Klaim Baru: " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Function
    Private Function fn_MengisiUpdateDataKlaimKlaim(ByVal kode_tarif As String, ByVal coder_nik As String, ByVal iscppt As Boolean, ByVal pesan As Boolean) As Boolean
        Try
            fn_CariRegister(lblRegister.Text)

            fn_MengisiUpdateDataKlaimKlaim = False

            Dim jsonEncode As String = String.Empty

            Dim listDiagnosa As New List(Of String)
            Dim listProsedur As New List(Of String)

            If iscppt = False Then
                For i As Integer = 0 To grvDIAGNOSA.RowCount - 2
                    If grvDIAGNOSA.GetRowCellValue(i, colKDDIAGNOSA) <> "" Then
                        listDiagnosa.Add(grvDIAGNOSA.GetRowCellValue(i, colKDDIAGNOSA))
                    End If
                Next

                For i As Integer = 0 To grvPROSEDUR.RowCount - 2
                    If grvPROSEDUR.GetRowCellValue(i, colKDPROSEDUR) <> "" Then
                        listProsedur.Add(grvPROSEDUR.GetRowCellValue(i, colKDPROSEDUR))
                    End If
                Next
            Else
                For i As Integer = 0 To grvDIAGNOSA_CPPT.RowCount - 2
                    If grvDIAGNOSA_CPPT.GetRowCellValue(i, colKDDIAGNOSA_CPPT) <> "" Then
                        listDiagnosa.Add(grvDIAGNOSA_CPPT.GetRowCellValue(i, colKDDIAGNOSA_CPPT))
                    End If
                Next

                For i As Integer = 0 To grvPROSEDUR_CPPT.RowCount - 2
                    If grvPROSEDUR_CPPT.GetRowCellValue(i, colKDPROSEDUR_CPPT) <> "" Then
                        listProsedur.Add(grvPROSEDUR_CPPT.GetRowCellValue(i, colKDPROSEDUR_CPPT))
                    End If
                Next
            End If

            If listDiagnosa.Count <= 0 Then
                If pesan = True Then
                    MsgBox("Diagnosa Masih Kosong", MsgBoxStyle.Exclamation, Me.Text)
                End If
                Exit Function
            End If

            Dim kelas_rawat = "3"

            If lblKELAS.Text.ToString.ToUpper = "KELAS 1" Then
                kelas_rawat = "1"
            ElseIf lblKELAS.Text.ToString.ToUpper = "KELAS 2" Then
                kelas_rawat = "2"
            Else
                kelas_rawat = "3"
            End If

            jsonEncode = "{" & """metadata"": { " & """method"": " & """set_claim_data"",    " & """nomor_sep"": """ & "" & lblNOMORSEP.Text & "" & """  },  "
            jsonEncode &= """data"": {    " & """nomor_sep"": """ & "" & lblNOMORSEP.Text & "" & """,    "
            jsonEncode &= """nomor_kartu"": """ & "" & lblKartuBPJS.Text & "" & """,    "
            jsonEncode &= """tgl_masuk"": """ & deTANGGALMASUK.DateTime.ToString("yyyy-MM-dd HH:mm:ss") & """,    "
            jsonEncode &= """tgl_pulang"": """ & deTANGGALPULANG.DateTime.ToString("yyyy-MM-dd HH:mm:ss") & """,    "
            'jsonEncode &= """cara_masuk"": """ & "gp" & """,    "
            jsonEncode &= """jenis_rawat"": """ & "1" & """,    "
            jsonEncode &= """kelas_rawat"": """ & kelas_rawat & """,    "
            jsonEncode &= """adl_sub_acute"": """ & "" & """,    "
            jsonEncode &= """adl_chronic"": """ & "" & """,    "
            jsonEncode &= """icu_indikator"": """ & "0" & """,    "
            jsonEncode &= """icu_los"": """ & "0" & """,    "
            jsonEncode &= """ventilator_hour"": """ & "" & """,    "
            jsonEncode &= """ventilator"": {      " & """use_ind"": """ & "0" & """,      "
            jsonEncode &= """start_dttm"": """ & "" & """,      "
            jsonEncode &= """stop_dttm"": """ & "" & """    },    "
            jsonEncode &= """upgrade_class_ind"": """ & "0" & """,    "
            jsonEncode &= """upgrade_class_class"": """ & "" & """,    "
            jsonEncode &= """upgrade_class_los"": """ & "0" & """,    "
            jsonEncode &= """upgrade_class_payor"": """ & "" & """,    "
            jsonEncode &= """add_payment_pct"": """ & "" & """,    "
            Try
                jsonEncode &= """birth_weight"": """ & IIf(iscppt = False, txtBeratBadanResume.Text / 1000, txtOBJEKTIF_BERATBADAN.Text / 1000) & """,    "
            Catch ex As Exception
                jsonEncode &= """birth_weight"": """ & "" & """,    "
            End Try
            jsonEncode &= """sistole"": """ & TextBox13.Text & """,    "
            jsonEncode &= """diastole"": """ & TextBox14.Text & """,    "
            jsonEncode &= """discharge_status"": """ & "1" & """,    "
            jsonEncode &= """diagnosa"": """ & String.Join("#", listDiagnosa.ToArray) & """,    "
            jsonEncode &= """procedure"": """ & String.Join("#", listProsedur.ToArray) & """,    "
            jsonEncode &= """diagnosa_inagrouper"": """ & "" & """,    "
            jsonEncode &= """procedure_inagrouper"": """ & "" & """,    "
            jsonEncode &= """tarif_rs"": {      " & """prosedur_non_bedah"": """ & CInt(sNonBedah) & """,      "
            jsonEncode &= """prosedur_bedah"": """ & CInt(sBedah) & """,      "
            jsonEncode &= """konsultasi"": """ & CInt(sKonsultasig) & """,      "
            jsonEncode &= """tenaga_ahli"": """ & CInt(sTenagaAhli) & """,      "
            jsonEncode &= """keperawatan"": """ & CInt(sKeperawatan) & """,      "
            jsonEncode &= """penunjang"": """ & CInt(sPenunjang) & """,      "
            jsonEncode &= """radiologi"": """ & CInt(sRadiologi) & """,      "
            jsonEncode &= """laboratorium"": """ & CInt(sLab) & """,      "
            jsonEncode &= """pelayanan_darah"": """ & CInt(sPelayananDarah) & """,      "
            jsonEncode &= """rehabilitasi"": """ & CInt(sRehabilitasi) & """,      "
            jsonEncode &= """kamar"": """ & CInt(sKamar) & """,      "
            jsonEncode &= """rawat_intensif"": """ & CInt(sRawatIntensif) & """,   "
            jsonEncode &= """obat"": """ & CInt(sObat) & """,   "
            jsonEncode &= """obat_kronis"": """ & CInt(sObatKronis) & """, "
            jsonEncode &= """obat_kemoterapi"": """ & CInt(sObatKemoterpi) & """,      "
            jsonEncode &= """alkes"": """ & CInt(sAlkes) & """,      "
            jsonEncode &= """bmhp"": """ & CInt(sBMHP) & """,      "
            jsonEncode &= """sewa_alat"": """ & CInt(sSewaAlatMedis) & """    },    "
            jsonEncode &= """pemulasaraan_jenazah"": """ & "0" & """,      "
            jsonEncode &= """kantong_jenazah"": """ & "0" & """,      "
            jsonEncode &= """peti_jenazah"": """ & "0" & """,      "
            jsonEncode &= """plastik_erat"": """ & "0" & """,      "
            jsonEncode &= """desinfektan_jenazah"": """ & "0" & """,      "
            jsonEncode &= """mobil_jenazah"": """ & "0" & """,      "
            jsonEncode &= """desinfektan_mobil_jenazah"": """ & "0" & """,      "
            jsonEncode &= """covid19_status_cd"": """ & "" & """,      "
            jsonEncode &= """nomor_kartu_t"": """ & "" & """,      "
            jsonEncode &= """episodes"": """ & "" & """,      "
            jsonEncode &= """covid19_cc_ind"": """ & "" & """,      "
            jsonEncode &= """covid19_rs_darurat_ind"": """ & "" & """,      "
            jsonEncode &= """covid19_co_insidense_ind"": """ & "" & """,      "
            jsonEncode &= """covid19_penunjang_pengurang"": {      " & """lab_asam_laktat"": """ & "1" & """,      "
            jsonEncode &= """lab_procalcitonin"": """ & "1" & """,      "
            jsonEncode &= """lab_crp"": """ & "1" & """,      "
            jsonEncode &= """lab_kultur"": """ & "1" & """,      "
            jsonEncode &= """lab_d_dimer"": """ & "1" & """,      "
            jsonEncode &= """lab_pt"": """ & "1" & """,      "
            jsonEncode &= """lab_aptt"": """ & "1" & """,      "
            jsonEncode &= """lab_waktu_pendarahan"": """ & "1" & """,      "
            jsonEncode &= """lab_anti_hiv"": """ & "1" & """,      "
            jsonEncode &= """lab_analisa_gas"": """ & "1" & """,      "
            jsonEncode &= """lab_albumin"": """ & "1" & """,      "
            jsonEncode &= """rad_thorax_ap_pa"": """ & "1" & """    },    "
            jsonEncode &= """terapi_konvalesen"": """ & "0" & """,      "
            jsonEncode &= """akses_naat"": """ & "" & """,      "
            jsonEncode &= """isoman_ind"": """ & "0" & """,      "
            jsonEncode &= """bayi_lahir_status_cd"": """ & "" & """,      "
            jsonEncode &= """dializer_single_use"": """ & "" & """,    "
            jsonEncode &= """tarif_poli_eks"": """ & "0" & """,    "
            jsonEncode &= """nama_dokter"": """ & "" & sDOKTERUTAMA & "" & """,    "
            jsonEncode &= """kode_tarif"": """ & "" & kode_tarif & "" & """,    "
            jsonEncode &= """payor_id"": """ & "3" & """,    "
            jsonEncode &= """payor_cd"": """ & "JKN" & """,    "
            jsonEncode &= """cob_cd"": """ & "#" & """,    "
            jsonEncode &= """coder_nik"": """ & "" & coder_nik & "" & """  } } "

            Dim jsonDecode = JObject.Parse(oBrigging.fn_MengisiUpdateDataKlaim(jsonEncode))
            Dim sDataDuplicate = String.Empty
            Dim smessage As String = String.Empty

            sDataDuplicate = jsonDecode("metadata")("code").ToString
            smessage = jsonDecode("metadata")("message").ToString

            If sDataDuplicate = "200" Then
                fn_MengisiUpdateDataKlaimKlaim = True
            Else
                If smessage = "Klaim sudah final" Then
                    MsgBox(sDataDuplicate & "-" & smessage, MsgBoxStyle.Exclamation, Me.Text)
                Else
                    MsgBox("Mengisi Update Data Klaim" & vbCrLf & sDataDuplicate & "-" & smessage, MsgBoxStyle.Exclamation, Me.Text)
                End If
            End If
        Catch oErr As Exception
            MsgBox("Mengisi Update Data Klaim: " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Function
    Private Function fn_GroupingStage() As Boolean
        Try
            fn_GroupingStage = False

            Dim jsonEncode As String = "{" & """metadata"": {      " & """method"":" & """grouper"",      " & """stage"":""" & 1 & """   },   " & """data"": {      " & """nomor_sep"":""" & "" & lblNOMORSEP.Text & "" & """   } } "
            Dim jsonDecode = JObject.Parse(oBrigging.fn_GroupingStage(jsonEncode))
            Dim sDataDuplicate As String = String.Empty
            Dim smessage As String = String.Empty

            sDataDuplicate = jsonDecode("metadata")("code").ToString
            smessage = jsonDecode("metadata")("message").ToString

            sSpesialCMG = ""

            If sDataDuplicate = "200" Then
                If jsonDecode("special_cmg_option") IsNot Nothing Then
                    TextBox1.Text = FormatNumber(jsonDecode("response")("cbg")("tariff").ToString, 0)

                    Dim frmGrouperStage2New As New frmGrouperStage2New
                    Try
                        frmGrouperStage2New.fn_LoadMe(jsonDecode)
                        frmGrouperStage2New.ShowDialog(Me)
                    Catch oErr As Exception
                        MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
                    Finally
                        If Not frmGrouperStage2New Is Nothing Then frmGrouperStage2New.Dispose()
                        frmGrouperStage2New = Nothing

                        If sSpesialCMG <> "" Then
                            jsonEncode = "{" & """metadata"": {      " & """method"":" & """grouper"",      " & """stage"":""" & 2 & """   },   " & """data"": {      " & """nomor_sep"":""" & "" & lblNOMORSEP.Text & "" & """, " & """special_cmg"": """ & sSpesialCMG & """    } } "

                            Dim jsonDecode2 = JObject.Parse(oBrigging.fn_GroupingStage(jsonEncode))
                            If jsonDecode2("metadata")("code").ToString = "200" Then
                                Dim simpan2 As String = jsonDecode2("response")("special_cmg").ToString
                                Dim TarifTambahan As Decimal = 0

                                If jsonDecode2("response")("special_cmg") IsNot Nothing Then
                                    For Each item In jsonDecode2("response")("special_cmg")
                                        If item("type") = "Special Procedure" Then
                                            TarifTambahan += item("tariff").ToString
                                        ElseIf item("type") = "Special Prosthesis" Then
                                            TarifTambahan += item("tariff").ToString
                                        ElseIf item("type") = "Special Investigation" Then
                                            TarifTambahan += item("tariff").ToString
                                        ElseIf item("type") = "Special Drug" Then
                                            TarifTambahan += item("tariff").ToString
                                        End If
                                    Next
                                End If

                                Try
                                    lblTotalGrpuperCPPT.Text = FormatNumber(CDec(jsonDecode("response")("cbg")("tariff").ToString) + TarifTambahan, 0)
                                    TextBox1.Text = FormatNumber(CDec(jsonDecode("response")("cbg")("tariff").ToString) + TarifTambahan, 0)

                                    Dim CodeCBG As String = jsonDecode("response")("cbg")("code").ToString
                                    Dim DeskripsiCBG As String = jsonDecode("response")("cbg")("description").ToString

                                    'txtCATATANGROUPER.Text = CodeCBG & "-" & DeskripsiCBG
                                    TextBox2.Text = CodeCBG & "-" & DeskripsiCBG

                                    fn_SaveGrouper(jsonDecode("response")("cbg")("tariff").ToString, "", CodeCBG & "-" & DeskripsiCBG)

                                    fn_GroupingStage = True
                                Catch ex As Exception
                                    MsgBox("Gagal Grouper" & vbCrLf & jsonDecode("response")("cbg")("description").ToString, MsgBoxStyle.Exclamation, Me.Text)
                                End Try
                            End If
                        End If
                    End Try
                Else
                    Try
                        TextBox1.Text = FormatNumber(jsonDecode("response")("cbg")("tariff").ToString, 0)
                        lblTotalGrpuperCPPT.Text = FormatNumber(CDec(jsonDecode("response")("cbg")("tariff").ToString), 0)

                        Dim CodeCBG As String = jsonDecode("response")("cbg")("code").ToString
                        Dim DeskripsiCBG As String = jsonDecode("response")("cbg")("description").ToString

                        'txtCATATANGROUPER.Text = CodeCBG & "-" & DeskripsiCBG
                        TextBox2.Text = CodeCBG & "-" & DeskripsiCBG

                        fn_SaveGrouper(jsonDecode("response")("cbg")("tariff").ToString, "", CodeCBG & "-" & DeskripsiCBG)

                        fn_GroupingStage = True
                    Catch ex As Exception
                        MsgBox("Gagal Grouper" & vbCrLf & jsonDecode("response").ToString, MsgBoxStyle.Exclamation, Me.Text)
                    End Try
                End If
            Else
                MsgBox("Gagal Grouper" & vbCrLf & sDataDuplicate & "-" & smessage, MsgBoxStyle.Exclamation, Me.Text)
            End If
        Catch oErr As Exception
            MsgBox("Grouper: " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Function
    Private Sub btnListKonsul_Click(sender As Object, e As EventArgs) Handles btnListKonsul.Click
        If btnListKonsul.Text = "Tutup List" Then
            btnListKonsul.Text = "Buka List"
            lKonsul1.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            lKonsul2.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            lKonsul3.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            lKonsul4.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        Else
            btnListKonsul.Text = "Tutup List"
            lKonsul1.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            lKonsul2.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            lKonsul3.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            lKonsul4.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        End If
    End Sub
    Private Sub btnTutupListResume_Click(sender As Object, e As EventArgs) Handles btnTutupListResume.Click
        If btnTutupListResume.Text = "Tutup List" Then
            btnTutupListResume.Text = "Buka List"
            lLitResume.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        Else
            btnTutupListResume.Text = "Tutup List"
            lLitResume.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        End If
    End Sub
    Private Sub btnTutupListLaboratorium_Click(sender As Object, e As EventArgs) Handles btnTutupListLaboratorium.Click
        If btnTutupListLaboratorium.Text = "Tutup List" Then
            btnTutupListLaboratorium.Text = "Buka List"
            lLISTLABORATORIUM.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        Else
            btnTutupListLaboratorium.Text = "Tutup List"
            lLISTLABORATORIUM.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        End If
    End Sub
    Private Sub btnListRadiologi_Click(sender As Object, e As EventArgs) Handles btnListRadiologi.Click
        If btnListRadiologi.Text = "Tutup List" Then
            btnListRadiologi.Text = "Buka List"
            lLISTRADIOLOGI.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        Else
            btnListRadiologi.Text = "Tutup List"
            lLISTRADIOLOGI.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        End If
    End Sub
    Private Sub btnTutupListAsesmenAwalMedis_Click(sender As Object, e As EventArgs) Handles btnTutupListAsesmenAwalMedis.Click
        If btnTutupListAsesmenAwalMedis.Text = "Tutup List" Then
            btnTutupListAsesmenAwalMedis.Text = "Buka List"
            lLISTASESMENAWALMEDIS.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        Else
            btnTutupListAsesmenAwalMedis.Text = "Tutup List"
            lLISTASESMENAWALMEDIS.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        End If
    End Sub
#End Region
#Region "BAK"
    'Private Sub fn_LoadCPPTRajalAwal(ByVal KDREG_RJ As String)
    '    Try
    '        Dim dsReqAwalPemeriksaan = oCPPT.GetDataPemeriksaan(KDREG_RJ)

    '        If dsReqAwalPemeriksaan IsNot Nothing Then
    '            chkALERGI_YA.Checked = dsReqAwalPemeriksaan.ALERGI_YA
    '            chkALERGI_TIDAK.Checked = dsReqAwalPemeriksaan.ALERGI_TIDAK
    '            txtALERGI_TEXT.Text = dsReqAwalPemeriksaan.ALERGI_TEXT
    '            txtOBJEKTIF_BERATBADAN.Text = IIf(CDec(dsReqAwalPemeriksaan.OBJEKTIF_BERATBADAN) = 0, "", dsReqAwalPemeriksaan.OBJEKTIF_BERATBADAN)
    '            txtOBJEKTIF_TINGGIBADAN.Text = IIf(CDec(dsReqAwalPemeriksaan.OBJEKTIF_TINGGIBADAN) = 0, "", dsReqAwalPemeriksaan.OBJEKTIF_TINGGIBADAN)
    '            txtOBJEKTIF_TEKANANDARAH.Text = dsReqAwalPemeriksaan.OBJEKTIF_TEKANANDARAH
    '            txtOBJEKTIF_NADI.Text = dsReqAwalPemeriksaan.OBJEKTIF_NADI
    '            txtOBJEKTIF_RESPIRASI.Text = dsReqAwalPemeriksaan.OBJEKTIF_RESPIRASI
    '            txtOBJEKTIF_SATURASIOKSIGEN.Text = dsReqAwalPemeriksaan.OBJEKTIF_SATURASIOKSIGEN
    '            txtOBJEKTIF_SUHU.Text = dsReqAwalPemeriksaan.OBJEKTIF_SUHU
    '            txtPEMERIKSAANLAIN.Text = dsReqAwalPemeriksaan.DESKRIPSI
    '        End If

    '        Dim dsCPPT = oCPPT.GetDataCPPTRJ(KDREG_RJ)
    '        If dsCPPT IsNot Nothing Then
    '            txtSUBJEKTIF.Text = dsCPPT.SUBJEKTIF
    '            txtDIAGNOOSA.Text = dsCPPT.ASSEMENT
    '        End If
    '    Catch oErr As Exception
    '        SplashScreenManager.CloseForm(False)
    '    End Try
    'End Sub
    'Private Sub fn_LoadAsesmenIGDAwal(ByVal KDREG_RJ As String)
    'Try
    '        Dim dsReqAwalPemeriksaan = oCPPT.GetDataAsesmenIGD(KDREG_RJ)

    '        If dsReqAwalPemeriksaan IsNot Nothing Then
    '            If dsReqAwalPemeriksaan.RIWAYAT_ALERGI = "" Then
    '                chkALERGI_YA.Checked = False
    '                chkALERGI_TIDAK.Checked = True
    '            Else
    '                chkALERGI_YA.Checked = True
    '                chkALERGI_TIDAK.Checked = False
    '            End If

    '            txtALERGI_TEXT.Text = dsReqAwalPemeriksaan.RIWAYAT_ALERGI
    '            txtOBJEKTIF_BERATBADAN.Text = dsReqAwalPemeriksaan.BERATBADAN
    '            txtOBJEKTIF_TINGGIBADAN.Text = dsReqAwalPemeriksaan.TINGGIBADAN
    '            txtOBJEKTIF_TEKANANDARAH.Text = dsReqAwalPemeriksaan.BP
    '            txtOBJEKTIF_NADI.Text = dsReqAwalPemeriksaan.NAMAPASIEN
    '            txtOBJEKTIF_RESPIRASI.Text = dsReqAwalPemeriksaan.RR
    '            txtOBJEKTIF_SATURASIOKSIGEN.Text = dsReqAwalPemeriksaan.SP02
    '            txtOBJEKTIF_SUHU.Text = dsReqAwalPemeriksaan.T
    '            txtDIAGNOOSA.Text = dsReqAwalPemeriksaan.KDDIAGNOSA
    '            txtSUBJEKTIF.Text = dsReqAwalPemeriksaan.RIWAYAT
    '        End If

    '    Catch oErr As Exception
    '        SplashScreenManager.CloseForm(False)
    '    End Try
    'End Sub
    Private Sub Form1_MouseWheel(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Me.MouseWheel
        If lblScroll.Text = "" Then Exit Sub

        If e.Delta > 0 Then
            Trace.WriteLine("Scrolled up!")
            fn_ScrollPage(True)
        Else
            Trace.WriteLine("Scrolled down!")
            fn_ScrollPage(False)
        End If
    End Sub
    'Private Sub frmRawatInapList_MouseWheel(sender As Object, e As MouseEventArgs) Handles MyBase.MouseWheel
    '    If e.Delta > 0 Then
    '        'up
    '        fn_ScrollPage(True)
    '    Else
    '        'down
    '        fn_ScrollPage(False)
    '    End If
    'End Sub

    Private Sub fn_ScrollPage(ByVal isUp As Boolean)
        Dim myView As Point = Me.Panel3.AutoScrollPosition
        Dim myView_ As Point = Me.Panel5.AutoScrollPosition
        Dim myView_Resume As Point = Me.Panel9.AutoScrollPosition
        Dim myView_Asesmen As Point = Me.Panel4.AutoScrollPosition
        Dim myView_LO As Point = Me.Panel7.AutoScrollPosition

        Dim scrollchange As Integer = 50

        If isUp Then
            'up
            myView.X = -myView.X
            myView.Y = -scrollchange - myView.Y

            myView_.X = -myView_.X
            myView_.Y = -scrollchange - myView_.Y

            myView_Resume.X = -myView_Resume.X
            myView_Resume.Y = -scrollchange - myView_Resume.Y

            myView_Asesmen.X = -myView_Asesmen.X
            myView_Asesmen.Y = -scrollchange - myView_Asesmen.Y

            myView_LO.X = -myView_LO.X
            myView_LO.Y = -scrollchange - myView_LO.Y
        Else
            'down
            myView.X = -myView.X
            myView.Y = scrollchange - myView.Y

            myView_.X = -myView_.X
            myView_.Y = scrollchange - myView_.Y

            myView_Resume.X = -myView_Resume.X
            myView_Resume.Y = scrollchange - myView_Resume.Y

            myView_Asesmen.X = -myView_Asesmen.X
            myView_Asesmen.Y = scrollchange - myView_Asesmen.Y

            myView_LO.X = -myView_LO.X
            myView_LO.Y = scrollchange - myView_LO.Y
        End If

        Me.Panel3.AutoScrollPosition = myView
        Me.Panel5.AutoScrollPosition = myView_
        Me.Panel9.AutoScrollPosition = myView_Resume
        Me.Panel4.AutoScrollPosition = myView_Asesmen
        Me.Panel7.AutoScrollPosition = myView_LO

    End Sub
    Private Sub ToolStripMenuItem6_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem6.Click
        If lblScroll.Text = "Scroll" Then
            lblScroll.Text = ""
        Else
            lblScroll.Text = "Scroll"
        End If
    End Sub
    Private Sub btnUploadList_Click(sender As Object, e As EventArgs) Handles btnUploadList.Click
        If btnUploadList.Text = "Tutup List" Then
            btnUploadList.Text = "Buka List"
            lUPLOAD.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        Else
            btnUploadList.Text = "Tutup List"
            lUPLOAD.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        End If
    End Sub
    Private Sub btnTutupListGerdQ_Click(sender As Object, e As EventArgs) Handles btnTutupListGerdQ.Click
        If btnTutupListGerdQ.Text = "Tutup List" Then
            btnTutupListGerdQ.Text = "Buka List"
            lLISTGERD.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        Else
            btnTutupListGerdQ.Text = "Tutup List"
            lLISTGERD.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        End If
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
    Private Sub btnTutupListAsesmenAwalPerawat_Click(sender As Object, e As EventArgs) Handles btnTutupListAsesmenAwalPerawat.Click
        If btnTutupListAsesmenAwalPerawat.Text = "Tutup List" Then
            btnTutupListAsesmenAwalPerawat.Text = "Buka List"
            lListAsesmenAwalPerawat.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        Else
            btnTutupListAsesmenAwalPerawat.Text = "Tutup List"
            lListAsesmenAwalPerawat.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        End If
    End Sub
    Private Sub grvGerdQ_FocusedRowChanged(ByVal sender As System.Object, ByVal e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs) Handles grvGerdQ.FocusedRowChanged
        If grvGerdQ.GetFocusedRowCellValue("KODE") Is Nothing Then
            Exit Sub
        End If

        Try
            PdfViewerGerdQ.CloseDocument()

            Dim FolderSimpan = "C:/GERDQ_LISTERM/"

            If Not Directory.Exists(FolderSimpan) Then
                Directory.CreateDirectory(FolderSimpan)
            Else
                DeleteDirectory(FolderSimpan)
                Directory.CreateDirectory(FolderSimpan)
            End If

            Dim ds = oIGD_GerdQ.GetData(grvGerdQ.GetFocusedRowCellValue("KODE"))
            If ds IsNot Nothing Then
                sWAKTU = ds.DATE.ToString("dd-MM-yyyy")
                sKDUSER_TTD = ds.KDUSER

                Dim rpt As New xtraGerd_Q

                rpt.ShowPrintMarginsWarning = False
                rpt.Watermark.Text = sWATERMARK

                rpt.bindingSource.DataSource = ds
                rpt.ExportToPdf(FolderSimpan & ds.KDGERD & ".pdf")
                PdfViewerGerdQ.LoadDocument(FolderSimpan & ds.KDGERD & ".pdf")
            End If
        Catch oErr As Exception
            MsgBox("Load data Gerd Q: " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub BillingToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles BillingToolStripMenuItem.Click
        fn_LoadRincian(True)
    End Sub
    Private Sub Button17_Click(sender As Object, e As EventArgs) Handles Button17.Click
        LoadAsesmenIGDDischargePlanning(sKDREGRAWATJALAN)
    End Sub
    Private Sub SimpleButton1_Click(sender As Object, e As EventArgs) Handles SimpleButton1.Click
        Dim frmListTemplate As New frmListTemplate
        Try
            sKODECPPTCOPY = String.Empty

            frmListTemplate.fn_LoadKategori(2, lblNoRM.Text)
            frmListTemplate.ShowDialog(Me)

            If sKODECPPTCOPY <> "" Then
                Dim dscPPT = oCPPT.GetData(sKODECPPTCOPY)
                If dscPPT IsNot Nothing Then
                    txtANAMNESIS_36.Text = dscPPT.PLANNING
                End If
            End If
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub btnRencanaKontrol_Click(sender As Object, e As EventArgs) Handles btnRencanaKontrol.Click
        If lblRegister.Text = "" Then
            MsgBox("Register Kosong", MsgBoxStyle.Exclamation, Me.Text)
            Exit Sub
        End If
        Dim oSKD As New Admission.clsSKD
        Dim ds = oSKD.GetDataPendaftaranalasan(lblRegister.Text, "KONTROL")

        Dim listdiagnosa As New List(Of String)
        For i As Integer = 0 To grvDIAGNOSA.RowCount - 2
            If grvDIAGNOSA.GetRowCellValue(i, colKATEGORI) = "Primer" Then
                listdiagnosa.Add(grvDIAGNOSA.GetRowCellValue(i, colNAMADIAGNOSA))
            End If
        Next

        If ds IsNot Nothing Then
            Dim frmSKD As New frmSKD
            Try
                frmSKD.fn_LoadNoPendaftaran(lblRegister.Text, lblNoRM.Text, "", sKDDOCTOR_INPUT, lblNOMORSEP.Text, lblPenjamin.Text, lblNamaPasien.Text, String.Join(", ", listdiagnosa.ToArray))
                frmSKD.fn_LoadKategori(0)
                frmSKD.LoadMe(FORM_MODE.FORM_MODE_EDIT, ds.KDSKD)
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
                frmSKD.fn_LoadNoPendaftaran(lblRegister.Text, lblNoRM.Text, "", sKDDOCTOR_INPUT, lblNOMORSEP.Text, lblPenjamin.Text, lblNamaPasien.Text, String.Join(", ", listdiagnosa.ToArray))
                frmSKD.fn_LoadKategori(0)
                frmSKD.LoadMe(FORM_MODE.FORM_MODE_ADD)
                frmSKD.ShowDialog(Me)
            Catch ex As Exception
                MsgBox("Load Form Detail : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
            End Try
        End If
    End Sub
    Private Sub LoadPenunjang(ByVal kdreg As String)
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
            SQL &= "A.KODE  "
            SQL &= ",A.SEQ "
            SQL &= ",TanggalOrder = A.DATE "
            SQL &= ",Penunjang = A.KATEGORI "
            SQL &= ",JenisPemeriksaan = A.NAMAORDER "
            SQL &= ",Dokter = ISNULL((SELECT NAME_DISPLAY FROM M_DOCTOR WHERE A.DOKTER = KDDOCTOR), '') "
            SQL &= ",Terima = A.ISAPPROVE "
            SQL &= ",Keterangan = A.MEMO "
            SQL &= "FROM "
            SQL &= "S_REQ_ORDER_PENUNJANG_AWAL A "
            SQL &= "WHERE "
            SQL &= "A.KDREG = '" & kdreg & "' "
            SQL &= "ORDER BY A.KATEGORI, A.SEQ "

            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "ORDERPENUNJANG")

            grdPenunjang.DataSource = ds.Tables("ORDERPENUNJANG")
            grdPenunjang.ForceInitialize()

            For iLoop As Integer = 0 To grvPenunjang.Columns.Count - 1
                If grvPenunjang.Columns(iLoop).ColumnType.Name = "Decimal" Then
                    grvPenunjang.Columns(iLoop).DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                    grvPenunjang.Columns(iLoop).DisplayFormat.FormatString = "{0:n2}"
                    grvPenunjang.Columns(iLoop).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
                ElseIf grvPenunjang.Columns(iLoop).ColumnType.Name = "DateTime" Then
                    grvPenunjang.Columns(iLoop).DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
                    grvPenunjang.Columns(iLoop).DisplayFormat.FormatString = "{0:dd/MM/yyyy HH:mm}"
                End If
            Next

            grvPenunjang.Columns("KODE").VisibleIndex = -1
            grvPenunjang.Columns("SEQ").VisibleIndex = -1

            grvPenunjang.BestFitColumns()

            If oConn.State = ConnectionState.Open Then
                oConn.Close()
            End If

        Catch oErr As Exception
            MsgBox("Load Rincian: " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub btnOrderLaboratorium_Click(sender As Object, e As EventArgs) Handles btnOrderLaboratorium.Click
        'Dim frmOrderPenunjangLab As New frmOrderPenunjangLab

        'Try
        '    If txtOBJEKTIF_BERATBADAN.Text = "" Then
        '        MsgBox("Dibutuhkan Berat Badan", MsgBoxStyle.Exclamation, Me.Text)
        '        Exit Sub
        '    End If
        '    If txtOBJEKTIF_TINGGIBADAN.Text = "" Then
        '        MsgBox("Dibutuhkan Tinggi Badan", MsgBoxStyle.Exclamation, Me.Text)
        '        Exit Sub
        '    End If
        '    Dim listdiagnosa As New List(Of String)

        '    If sISDOKTER = True Then
        '        For i As Integer = 0 To grvDIAGNOSA_CPPT.RowCount - 2
        '            listdiagnosa.Add(grvDIAGNOSA_CPPT.GetRowCellValue(i, colNAMADIAGNOSA_CPPT))
        '        Next
        '    Else
        '        If sCOPYCPPTDOKTERTERAKHIR <> "" Then
        '            For Each xloop In oCPPT.GetDataDetailDiagnosa(sCOPYCPPTDOKTERTERAKHIR)
        '                listdiagnosa.Add(xloop.NAMADIAGNOSA)
        '            Next
        '            'Else
        '            '    MsgBox("Dokter Belum Isi CPPT, diagnosa tidak bisa diambil", MsgBoxStyle.Exclamation, Me.Text)
        '            '    Exit Sub
        '        End If

        '    End If

        '    'If listdiagnosa.Count <= 0 Then
        '    '    MsgBox("Dibutuhkan Diagnosa", MsgBoxStyle.Exclamation, Me.Text)
        '    '    Exit Sub
        '    'End If

        '    frmOrderPenunjangLab.LoadMe(String.Join(", ", listdiagnosa.ToArray), "LABORATORIUM", sKDDOCTOR_DPJPUTAMA, txtOBJEKTIF_BERATBADAN.Text, txtOBJEKTIF_TINGGIBADAN.Text, Now.ToString("yyyyMMddHHmmss") & lblNoRM.Text, lblRegister.Text, lblNoRM.Text, lblNamaPasien.Text, lblRuangan.Text, FORM_MODE.FORM_MODE_ADD)
        '    frmOrderPenunjangLab.ShowDialog(Me)

        '    LoadPenunjang(lblRegister.Text)

        '    'Dim kodecppt As String = IIf(lblKODECPPT.Text = "TAMBAH CPPT/SBAR", "", lblKODECPPT.Text)

        '    'Dim dsCek = oOrderPenunjang.GetDataKDCPPTLABAWALBYCPPT(kodecppt)
        '    'If dsCek Is Nothing Then
        '    '    skodeorderlab = Now.ToString("yyyyMMddHHmmss") & lblNoRM.Text
        '    '    frmOrderPenunjangLab.LoadMe(String.Join(", ", listdiagnosa.ToArray), "LABORATORIUM", sKDDOCTOR_DPJPUTAMA, txtOBJEKTIF_BERATBADAN.Text, txtOBJEKTIF_TINGGIBADAN.Text, skodeorderlab, FORM_MODE.FORM_MODE_ADD)
        '    '    frmOrderPenunjangLab.ShowDialog(Me)
        '    'Else
        '    '    skodeorderlab = dsCek.KODE
        '    '    frmOrderPenunjangLab.LoadMe(String.Join(", ", listdiagnosa.ToArray), "LABORATORIUM", sKDDOCTOR_DPJPUTAMA, txtOBJEKTIF_BERATBADAN.Text, txtOBJEKTIF_TINGGIBADAN.Text, skodeorderlab, FORM_MODE.FORM_MODE_EDIT)
        '    '    frmOrderPenunjangLab.ShowDialog(Me)
        '    'End If

        '    'Dim listOrderLab As New List(Of String)

        '    'For Each xloop In oOrderPenunjang.GetDataDetailLisLabAwal(skodeorderlab)
        '    '    listOrderLab.Add(xloop.NAMAORDER)
        '    'Next

        '    'If skodeorderlab <> "" Then
        '    '    If txtINTRUKSIORDERCPPT.Text.Contains("Radiologi") Then
        '    '        txtINTRUKSIORDERCPPT.Text = txtINTRUKSIORDERCPPT.Text & vbCrLf & vbCrLf & String.Join(vbCrLf, listOrderLab.ToArray)
        '    '    Else
        '    '        txtINTRUKSIORDERCPPT.Text = String.Join(vbCrLf, listOrderLab.ToArray)
        '    '    End If
        '    'End If
        'Catch ex As Exception
        '    MsgBox("Load Form Detail : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
        'Finally
        '    If Not frmOrderPenunjangLab Is Nothing Then frmOrderPenunjangLab.Dispose()
        '    frmOrderPenunjangLab = Nothing
        'End Try
    End Sub
    Private Sub btnOrderRadiologi_Click(sender As Object, e As EventArgs) Handles btnOrderRadiologi.Click
        'Dim frmOrderPenunjangLab As New frmOrderPenunjangLab
        'Try
        '    If txtOBJEKTIF_BERATBADAN.Text = "" Then
        '        MsgBox("Dibutuhkan Berat Badan", MsgBoxStyle.Exclamation, Me.Text)
        '        Exit Sub
        '    End If
        '    If txtOBJEKTIF_TINGGIBADAN.Text = "" Then
        '        MsgBox("Dibutuhkan Tinggi Badan", MsgBoxStyle.Exclamation, Me.Text)
        '        Exit Sub
        '    End If

        '    Dim listdiagnosa As New List(Of String)

        '    If sISDOKTER = True Then
        '        For i As Integer = 0 To grvDIAGNOSA_CPPT.RowCount - 2
        '            listdiagnosa.Add(grvDIAGNOSA_CPPT.GetRowCellValue(i, colNAMADIAGNOSA_CPPT))
        '        Next
        '    Else
        '        If sCOPYCPPTDOKTERTERAKHIR <> "" Then
        '            For Each xloop In oCPPT.GetDataDetailDiagnosa(sCOPYCPPTDOKTERTERAKHIR)
        '                listdiagnosa.Add(xloop.NAMADIAGNOSA)
        '            Next
        '            'Else
        '            '    MsgBox("Dokter Belum Isi CPPT, diagnosa tidak bisa diambil", MsgBoxStyle.Exclamation, Me.Text)
        '            '    Exit Sub
        '        End If
        '    End If

        '    frmOrderPenunjangLab.LoadMe(String.Join(", ", listdiagnosa.ToArray), "RADIOLOGI", sKDDOCTOR_DPJPUTAMA, txtOBJEKTIF_BERATBADAN.Text, txtOBJEKTIF_TINGGIBADAN.Text, Now.ToString("yyyyMMddHHmmss") & lblNoRM.Text, lblRegister.Text, lblNoRM.Text, lblNamaPasien.Text, lblRuangan.Text, FORM_MODE.FORM_MODE_ADD)
        '    frmOrderPenunjangLab.ShowDialog(Me)

        '    LoadPenunjang(lblRegister.Text)

        '    'Dim kodecppt As String = IIf(lblKODECPPT.Text = "TAMBAH CPPT/SBAR", "", lblKODECPPT.Text)

        '    'Dim dsCek = oOrderPenunjang.GetDataKDCPPTRADAWALBYCPPT(kodecppt)
        '    'If dsCek Is Nothing Then
        '    '    skodeorderRad = Now.ToString("yyyyMMddHHmmss") & lblNoRM.Text
        '    '    frmOrderPenunjangLab.LoadMe(String.Join(", ", listdiagnosa.ToArray), "RADIOLOGI", sKDDOCTOR_DPJPUTAMA, txtOBJEKTIF_BERATBADAN.Text, txtOBJEKTIF_TINGGIBADAN.Text, skodeorderRad, FORM_MODE.FORM_MODE_ADD)
        '    '    frmOrderPenunjangLab.ShowDialog(Me)
        '    'Else
        '    '    skodeorderRad = dsCek.KODE
        '    '    frmOrderPenunjangLab.LoadMe(String.Join(", ", listdiagnosa.ToArray), "RADIOLOGI", sKDDOCTOR_DPJPUTAMA, txtOBJEKTIF_BERATBADAN.Text, txtOBJEKTIF_TINGGIBADAN.Text, skodeorderRad, FORM_MODE.FORM_MODE_EDIT)
        '    '    frmOrderPenunjangLab.ShowDialog(Me)
        '    'End If

        '    'Dim listOrderLab As New List(Of String)

        '    'For Each xloop In oOrderPenunjang.GetDataDetailLisRadAwal(skodeorderRad)
        '    '    listOrderLab.Add(xloop.NAMAORDER)
        '    'Next

        '    'If skodeorderRad <> "" Then
        '    '    If txtINTRUKSIORDERCPPT.Text.Contains("Laboratorium") Then
        '    '        txtINTRUKSIORDERCPPT.Text = txtINTRUKSIORDERCPPT.Text & vbCrLf & String.Join(vbCrLf, listOrderLab.ToArray)
        '    '    Else
        '    '        txtINTRUKSIORDERCPPT.Text = String.Join(vbCrLf, listOrderLab.ToArray)
        '    '    End If
        '    'End If

        'Catch ex As Exception
        '    MsgBox("Load Form Detail : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
        'Finally
        '    If Not frmOrderPenunjangLab Is Nothing Then frmOrderPenunjangLab.Dispose()
        '    frmOrderPenunjangLab = Nothing
        'End Try
    End Sub
    Private Sub btnLoadTTVTerakhir_Click(sender As Object, e As EventArgs) Handles btnLoadTTVTerakhir.Click
        If sCOPYKDCPPTPERAWAT <> "" Then
            fn_CopyCPPTAkhirdiCPPTTTVPerawat(sCOPYKDCPPTPERAWAT)
        End If
    End Sub
    Private Sub btnPasienPulang_Click(sender As Object, e As EventArgs) Handles btnPasienPulang.Click
        If sISDOKTER = False Then
            MsgBox("Silahkan pilih RME Dokter", MsgBoxStyle.Exclamation, Me.Text)
            Exit Sub
        End If

        If sRESEPPULANGDICPPT = False Then
            Try
                sRESEPPULANGDICPPT = True

                lITRUKSI_2.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                lITRUKSI_3.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                lINTRUKSILAIN_5.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                lNTRUKSILAIN_4.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always

                Dim kode As String = fn_LoadResepBykddoctor(lblRegister.Text, sKDDOCTOR_INPUT, True, True)

                If kode = "" Then
                    kode = fn_LoadResepBykddoctor(lblRegister.Text, sKDDOCTOR_DPJPUTAMA, True, True)
                End If

                If kode = "" Then
                    Dim frmReqRecipeRawatInap As New frmReqRecipeRawatInap
                    Try
                        Dim listdiagnosa As New List(Of String)
                        For i As Integer = 0 To grvDIAGNOSA_CPPT.RowCount - 2
                            listdiagnosa.Add(grvDIAGNOSA_CPPT.GetRowCellValue(i, colNAMADIAGNOSA_CPPT))
                        Next

                        Dim Diagnosalabel As String = String.Join(vbCrLf, listdiagnosa.ToArray)

                        frmReqRecipeRawatInap.fn_LoaDataPasien(True, sISDOKTER, lblPenjamin.Text, lblRegister.Text, lblNoRM.Text, lblNamaPasien.Text, IIf(sISDOKTER = False, sKDDOCTOR_DPJPUTAMA, sKDDOCTOR_INPUT), lblRuangan.Text, Diagnosalabel, txtOBJEKTIF_BERATBADAN.Text, IIf(sISDOKTER = True, sUserID, sKDUSER_PERAWAT))
                        frmReqRecipeRawatInap.LoadMe(FORM_MODE.FORM_MODE_ADD)
                        frmReqRecipeRawatInap.ShowDialog(Me)

                        tabOrderList_SelectedPageChanged()
                    Catch oErr As Exception
                        MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
                    Finally
                        If Not frmReqRecipeRawatInap Is Nothing Then frmReqRecipeRawatInap.Dispose()
                        frmReqRecipeRawatInap = Nothing

                    End Try
                Else
                    Dim dsResep = oReqRecipeRawatInap.GetData(kode)
                    If dsResep Is Nothing Then
                        Dim frmReqRecipeRawatInap As New frmReqRecipeRawatInap
                        Try
                            Dim listdiagnosa As New List(Of String)
                            For i As Integer = 0 To grvDIAGNOSA_CPPT.RowCount - 2
                                listdiagnosa.Add(grvDIAGNOSA_CPPT.GetRowCellValue(i, colNAMADIAGNOSA_CPPT))
                            Next

                            Dim Diagnosalabel As String = String.Join(vbCrLf, listdiagnosa.ToArray)

                            frmReqRecipeRawatInap.fn_LoaDataPasien(True, sISDOKTER, lblPenjamin.Text, lblRegister.Text, lblNoRM.Text, lblNamaPasien.Text, IIf(sISDOKTER = False, sKDDOCTOR_DPJPUTAMA, sKDDOCTOR_INPUT), lblRuangan.Text, Diagnosalabel, txtOBJEKTIF_BERATBADAN.Text, IIf(sISDOKTER = True, sUserID, sKDUSER_PERAWAT))
                            frmReqRecipeRawatInap.LoadMe(FORM_MODE.FORM_MODE_ADD)
                            frmReqRecipeRawatInap.ShowDialog(Me)

                            tabOrderList_SelectedPageChanged()
                        Catch oErr As Exception
                            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
                        Finally
                            If Not frmReqRecipeRawatInap Is Nothing Then frmReqRecipeRawatInap.Dispose()
                            frmReqRecipeRawatInap = Nothing

                        End Try
                    Else
                        If dsResep.KONFIRMASIRESEP <> "Resep Belum Diterima" Then
                            MsgBox("Resep sudah " & dsResep.KONFIRMASIRESEP & " Silahkan hubungi bagian farmasi", MsgBoxStyle.Exclamation, Me.Text)
                        Else
                            Dim frmReqRecipeRawatInap As New frmReqRecipeRawatInap
                            Try
                                Dim listdiagnosa As New List(Of String)
                                For i As Integer = 0 To grvDIAGNOSA_CPPT.RowCount - 2
                                    listdiagnosa.Add(grvDIAGNOSA_CPPT.GetRowCellValue(i, colNAMADIAGNOSA_CPPT))
                                Next

                                Dim Diagnosalabel As String = String.Join(vbCrLf, listdiagnosa.ToArray)

                                frmReqRecipeRawatInap.fn_LoaDataPasien(True, sISDOKTER, lblPenjamin.Text, lblRegister.Text, lblNoRM.Text, lblNamaPasien.Text, IIf(sISDOKTER = False, sKDDOCTOR_DPJPUTAMA, sKDDOCTOR_INPUT), lblRuangan.Text, Diagnosalabel, txtOBJEKTIF_BERATBADAN.Text, IIf(sISDOKTER = True, sUserID, sKDUSER_PERAWAT))
                                frmReqRecipeRawatInap.LoadMe(FORM_MODE.FORM_MODE_EDIT, dsResep.KDREQRECIPE_RI)
                                frmReqRecipeRawatInap.ShowDialog(Me)

                                tabOrderList_SelectedPageChanged()
                            Catch oErr As Exception
                                MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
                            Finally
                                If Not frmReqRecipeRawatInap Is Nothing Then frmReqRecipeRawatInap.Dispose()
                                frmReqRecipeRawatInap = Nothing

                            End Try
                        End If
                    End If

                End If

                fn_loadResepPulang(False)

            Catch oErr As Exception
                MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            End Try
        Else
            sRESEPPULANGDICPPT = False

            lITRUKSI_2.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            lITRUKSI_3.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            lINTRUKSILAIN_5.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            lNTRUKSILAIN_4.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never

            txtINTRUKSILAIN_OBATPULANG.ResetText()
            txtINTRUKSILAIN_EDUKASI.ResetText()
        End If


    End Sub
    Private Sub btnResepPulangResume_Click(sender As Object, e As EventArgs) Handles btnResepPulangResume.Click
        If sISDOKTER = False Then
            MsgBox("Silahkan pilih RME Dokter", MsgBoxStyle.Exclamation, Me.Text)
            Exit Sub
        End If

        'Dim frmReqRecipeRawatInap As New frmReqRecipeRawatInap
        'Try
        '    Dim listdiagnosa As New List(Of String)
        '    For i As Integer = 0 To grvDIAGNOSA_CPPT.RowCount - 2
        '        listdiagnosa.Add(grvDIAGNOSA_CPPT.GetRowCellValue(i, colNAMADIAGNOSA_CPPT))
        '    Next

        '    Dim Diagnosalabel As String = String.Join(vbCrLf, listdiagnosa.ToArray)

        '    frmReqRecipeRawatInap.fn_LoaDataPasien(True, sISDOKTER, lblPenjamin.Text, lblRegister.Text, lblNoRM.Text, lblNamaPasien.Text, IIf(sISDOKTER = False, sKDDOCTOR_DPJPUTAMA, sKDDOCTOR_INPUT), lblRuangan.Text, Diagnosalabel, txtOBJEKTIF_BERATBADAN.Text, sKDUSER_PERAWAT)
        '    frmReqRecipeRawatInap.LoadMe(FORM_MODE.FORM_MODE_ADD)
        '    frmReqRecipeRawatInap.ShowDialog(Me)
        'Catch oErr As Exception
        '    MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        'Finally
        '    If Not frmReqRecipeRawatInap Is Nothing Then frmReqRecipeRawatInap.Dispose()
        '    frmReqRecipeRawatInap = Nothing

        '    fn_loadResepPulang(True)
        'End Try
        Try
            Dim kode As String = fn_LoadResepBykddoctor(lblRegister.Text, sKDDOCTOR_INPUT, True, False)

            If kode = "" Then
                kode = fn_LoadResepBykddoctor(lblRegister.Text, sKDDOCTOR_DPJPUTAMA, True, False)
            End If

            If kode = "" Then
                Dim frmReqRecipeRawatInap As New frmReqRecipeRawatInap
                Try
                    Dim listdiagnosa As New List(Of String)
                    For i As Integer = 0 To grvDIAGNOSA_CPPT.RowCount - 2
                        listdiagnosa.Add(grvDIAGNOSA_CPPT.GetRowCellValue(i, colNAMADIAGNOSA_CPPT))
                    Next

                    Dim Diagnosalabel As String = String.Join(vbCrLf, listdiagnosa.ToArray)

                    frmReqRecipeRawatInap.fn_LoaDataPasien(True, sISDOKTER, lblPenjamin.Text, lblRegister.Text, lblNoRM.Text, lblNamaPasien.Text, IIf(sISDOKTER = False, sKDDOCTOR_DPJPUTAMA, sKDDOCTOR_INPUT), lblRuangan.Text, Diagnosalabel, txtOBJEKTIF_BERATBADAN.Text, IIf(sISDOKTER = True, sUserID, sKDUSER_PERAWAT))
                    frmReqRecipeRawatInap.LoadMe(FORM_MODE.FORM_MODE_ADD)
                    frmReqRecipeRawatInap.ShowDialog(Me)

                    tabOrderList_SelectedPageChanged()
                Catch oErr As Exception
                    MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
                Finally
                    If Not frmReqRecipeRawatInap Is Nothing Then frmReqRecipeRawatInap.Dispose()
                    frmReqRecipeRawatInap = Nothing

                End Try
            Else
                Dim dsResep = oReqRecipeRawatInap.GetData(kode)
                If dsResep Is Nothing Then
                    Dim frmReqRecipeRawatInap As New frmReqRecipeRawatInap
                    Try
                        Dim listdiagnosa As New List(Of String)
                        For i As Integer = 0 To grvDIAGNOSA_CPPT.RowCount - 2
                            listdiagnosa.Add(grvDIAGNOSA_CPPT.GetRowCellValue(i, colNAMADIAGNOSA_CPPT))
                        Next

                        Dim Diagnosalabel As String = String.Join(vbCrLf, listdiagnosa.ToArray)

                        frmReqRecipeRawatInap.fn_LoaDataPasien(True, sISDOKTER, lblPenjamin.Text, lblRegister.Text, lblNoRM.Text, lblNamaPasien.Text, IIf(sISDOKTER = False, sKDDOCTOR_DPJPUTAMA, sKDDOCTOR_INPUT), lblRuangan.Text, Diagnosalabel, txtOBJEKTIF_BERATBADAN.Text, IIf(sISDOKTER = True, sUserID, sKDUSER_PERAWAT))
                        frmReqRecipeRawatInap.LoadMe(FORM_MODE.FORM_MODE_ADD)
                        frmReqRecipeRawatInap.ShowDialog(Me)

                        tabOrderList_SelectedPageChanged()
                    Catch oErr As Exception
                        MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
                    Finally
                        If Not frmReqRecipeRawatInap Is Nothing Then frmReqRecipeRawatInap.Dispose()
                        frmReqRecipeRawatInap = Nothing

                    End Try
                Else
                    If dsResep.KONFIRMASIRESEP <> "Resep Belum Diterima" Then
                        MsgBox("Resep sudah " & dsResep.KONFIRMASIRESEP & " Silahkan hubungi bagian farmasi", MsgBoxStyle.Exclamation, Me.Text)
                    Else
                        Dim frmReqRecipeRawatInap As New frmReqRecipeRawatInap
                        Try
                            Dim listdiagnosa As New List(Of String)
                            For i As Integer = 0 To grvDIAGNOSA_CPPT.RowCount - 2
                                listdiagnosa.Add(grvDIAGNOSA_CPPT.GetRowCellValue(i, colNAMADIAGNOSA_CPPT))
                            Next

                            Dim Diagnosalabel As String = String.Join(vbCrLf, listdiagnosa.ToArray)

                            frmReqRecipeRawatInap.fn_LoaDataPasien(True, sISDOKTER, lblPenjamin.Text, lblRegister.Text, lblNoRM.Text, lblNamaPasien.Text, IIf(sISDOKTER = False, sKDDOCTOR_DPJPUTAMA, sKDDOCTOR_INPUT), lblRuangan.Text, Diagnosalabel, txtOBJEKTIF_BERATBADAN.Text, IIf(sISDOKTER = True, sUserID, sKDUSER_PERAWAT))
                            frmReqRecipeRawatInap.LoadMe(FORM_MODE.FORM_MODE_EDIT, dsResep.KDREQRECIPE_RI)
                            frmReqRecipeRawatInap.ShowDialog(Me)

                            tabOrderList_SelectedPageChanged()
                        Catch oErr As Exception
                            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
                        Finally
                            If Not frmReqRecipeRawatInap Is Nothing Then frmReqRecipeRawatInap.Dispose()
                            frmReqRecipeRawatInap = Nothing

                        End Try
                    End If
                End If

            End If


            fn_loadResepPulang(True)
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_loadResepPulang(ByVal isResume As Boolean)
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
            SQL &= "S_REQ_RECIPE_RI_H A "
            SQL &= "INNER JOIN S_REQ_RECIPE_RI_D B "
            SQL &= "ON A.KDREQRECIPE_RI = B.KDREQRECIPE_RI "
            SQL &= "WHERE "
            SQL &= "A.KDPENDAFTARAN = '" & lblRegister.Text & "' "
            SQL &= "AND ISPULANG = 1 "
            If isResume = False Then
                SQL &= "AND ALAMAT = '" & IIf(sKDDOCTOR_INPUT = "", sKDDOCTOR_DPJPUTAMA, sKDDOCTOR_INPUT) & "' "
            End If
            SQL &= "ORDER BY B.SEQ "

            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "S_REQ_RECIPE_RI_D")

            Dim listObat As New List(Of String)

            For iLoop As Integer = 0 To ds.Tables("S_REQ_RECIPE_RI_D").Rows.Count - 1
                With ds.Tables("S_REQ_RECIPE_RI_D")
                    listObat.Add(.Rows(iLoop)("NAMAOBAT").Replace("(EC)", "").ToString.Trim & (" " & .Rows(iLoop)("DOSIS")).ToString.Trim & (" " & .Rows(iLoop)("SIGNA")).ToString.Trim & (" " & .Rows(iLoop)("CARAPAKAI")).ToString.Trim)
                    'listObat.Add((.Rows(iLoop)("NAMAOBAT") & " " & .Rows(iLoop)("SATUAN") & " " & .Rows(iLoop)("SIGNA") & " " & .Rows(iLoop)("CARAPAKAI") & " " & .Rows(iLoop)("REMARKS_DOKTER") & " " & .Rows(iLoop)("ROMAWI") & " " & .Rows(iLoop)("DOSIS")).ToString.Trim)
                End With
            Next

            If isResume = False Then
                txtINTRUKSILAIN_OBATPULANG.ResetText()
                txtINTRUKSILAIN_OBATPULANG.Text = String.Join(vbCrLf, listObat.ToArray)
            Else
                txtObatPulang.ResetText()
                txtObatPulang.Text = String.Join(vbCrLf, listObat.ToArray)
            End If
            If oConn.State = ConnectionState.Open Then
                oConn.Close()
            End If
        Catch oErr As Exception
            MsgBox("Load Resep Pulang: " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub SimpleButton2_Click(sender As Object, e As EventArgs) Handles SimpleButton2.Click
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
            SQL &= "KETERANGAN_TINDAKLANJUT "
            SQL &= "FROM "
            SQL &= "S_PENDAFTARAN_H A "
            SQL &= "WHERE "
            SQL &= "A.KDREG = '" & sKDREGRAWATJALAN & "' "

            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "S_PENDAFTARAN_H_AMBILREGEX")

            Dim teks As String = "Test Regex"

            For iLoop As Integer = 0 To ds.Tables("S_PENDAFTARAN_H_AMBILREGEX").Rows.Count - 1
                With ds.Tables("S_PENDAFTARAN_H_AMBILREGEX")
                    teks = .Rows(iLoop)("KETERANGAN_TINDAKLANJUT")
                End With
            Next

            If oConn.State = ConnectionState.Open Then
                oConn.Close()
            End If

            Dim pola As String = "Indikasi\s*(.*)" ' Mencari teks setelah "Indikasi"
            Dim hasil As Match = Regex.Match(teks, pola)

            If hasil.Success Then
                txtINDIKASIRAWATDIASESMEN.Text = hasil.Groups(1).Value
            Else
                MsgBox("Data Tidak ditemukan di Register RAwat Jalan" & vbCrLf & sKDREGRAWATJALAN, MsgBoxStyle.Exclamation, Me.Text)
            End If
        Catch oErr As Exception
            MsgBox("Load Regex: " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub SimpleButton3_Click(sender As Object, e As EventArgs) Handles SimpleButton3.Click
        Try
            'Asesmen
            Dim dsAsesmen = oDigital_DischargePlanning.GetData(lblRegister.Text)

            If dsAsesmen IsNot Nothing Then
                txtKeluhanUtama.Text = dsAsesmen.ANAMNESIS_01
                txtAnamnesa.Text = dsAsesmen.ANAMNESIS_02
                txtKomorbiditasLain.Text = dsAsesmen.ANAMNESIS_03
            Else
                MsgBox("Asesmen Medis Belum di Input!", MsgBoxStyle.Information, Me.Text)
            End If
        Catch oErr As Exception
            MsgBox("Load List Data : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub SimpleButton4_Click(sender As Object, e As EventArgs) Handles SimpleButton4.Click
        Try
            ' ***** HEADER *****
            Dim ds = oCPPT.GetData(sCOPYCPPTDOKTERTERAKHIR)
            Dim dsTambahan = oCPPT.GetDataLainnyaTambah(sCOPYCPPTDOKTERTERAKHIR)

            If ds Is Nothing Then
                MsgBox("CPPT Belum di Input", MsgBoxStyle.Information, Me.Text)
                Exit Sub
            Else
                txtAnamnesa.Text = ds.SUBJEKTIF
            End If
        Catch oErr As Exception
            MsgBox("Load List Data : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub SimpleButton5_Click(sender As Object, e As EventArgs) Handles SimpleButton5.Click
        Try
            'Asesmen
            Dim dsAsesmen = oDigital_DischargePlanning.GetData(lblRegister.Text)

            If dsAsesmen IsNot Nothing Then
                Dim listPemeriksaanFisik As New List(Of String)

                If dsAsesmen.ANAMNESIS_08 <> "" Then
                    listPemeriksaanFisik.Add("Berat Badan : " & dsAsesmen.ANAMNESIS_08 & " Kg")
                End If
                If dsAsesmen.ANAMNESIS_12 <> "" Then
                    listPemeriksaanFisik.Add("Tensi : " & dsAsesmen.ANAMNESIS_12 & " mmHg")
                End If
                If dsAsesmen.ANAMNESIS_11 <> "" Then
                    listPemeriksaanFisik.Add("Nadi : " & dsAsesmen.ANAMNESIS_11 & " x/mnt")
                End If
                If dsAsesmen.ANAMNESIS_14 <> "" Then
                    listPemeriksaanFisik.Add("Pernapasan : " & dsAsesmen.ANAMNESIS_14 & " x/mnt")
                End If
                If dsAsesmen.ANAMNESIS_13 <> "" Then
                    listPemeriksaanFisik.Add("Suhu : " & dsAsesmen.ANAMNESIS_13 & " oC")
                End If
                If dsAsesmen.ANAMNESIS_09 <> "" Then
                    listPemeriksaanFisik.Add("Tinggi Badan : " & dsAsesmen.ANAMNESIS_09 & " Cm")
                End If
                If dsAsesmen.ANAMNESIS_10 <> "" Then
                    listPemeriksaanFisik.Add("Gizi : " & dsAsesmen.ANAMNESIS_10)
                End If
                If dsAsesmen.ANAMNESIS_22 <> "" Then
                    listPemeriksaanFisik.Add("SpO2 : " & dsAsesmen.ANAMNESIS_22 & "% ")
                End If
                If dsAsesmen.ANAMNESIS_05 <> "" Then
                    listPemeriksaanFisik.Add("Keadaan Umum : " & dsAsesmen.ANAMNESIS_05)
                End If
                If dsAsesmen.ANAMNESIS_06 <> "" Then
                    listPemeriksaanFisik.Add("Kesadaran : " & dsAsesmen.ANAMNESIS_06)
                End If

                If dsAsesmen.ANAMNESIS_15_2_CHEK = True Then
                    If dsAsesmen.ANAMNESIS_15 <> "" Then
                        listPemeriksaanFisik.Add("Kepala " & dsAsesmen.ANAMNESIS_15)
                    End If
                End If

                If dsAsesmen.ANAMNESIS_16_2_CHEK = True Then
                    If dsAsesmen.ANAMNESIS_16 <> "" Then
                        listPemeriksaanFisik.Add("Mata " & dsAsesmen.ANAMNESIS_16)
                    End If
                End If

                If dsAsesmen.ANAMNESIS_17_2_CHEK = True Then
                    If dsAsesmen.ANAMNESIS_17 <> "" Then
                        listPemeriksaanFisik.Add("Leher " & dsAsesmen.ANAMNESIS_17)
                    End If
                End If

                If dsAsesmen.ANAMNESIS_18_2_CHEK = True Then
                    If dsAsesmen.ANAMNESIS_18 <> "" Then
                        listPemeriksaanFisik.Add("Dada " & dsAsesmen.ANAMNESIS_18)
                    End If
                End If

                If dsAsesmen.ANAMNESIS_19_2_CHEK = True Then
                    If dsAsesmen.ANAMNESIS_19 <> "" Then
                        listPemeriksaanFisik.Add("Perut " & dsAsesmen.ANAMNESIS_19)
                    End If
                End If

                If dsAsesmen.ANAMNESIS_20_2_CHEK = True Then
                    If dsAsesmen.ANAMNESIS_20 <> "" Then
                        listPemeriksaanFisik.Add("Alat Gerak " & dsAsesmen.ANAMNESIS_20)
                    End If
                End If

                If dsAsesmen.ANAMNESIS_29 <> "" Then
                    listPemeriksaanFisik.Add("Genetalia : " & dsAsesmen.ANAMNESIS_29)
                End If
                If dsAsesmen.ANAMNESIS_30 <> "" Then
                    listPemeriksaanFisik.Add("Ekstremitas : " & dsAsesmen.ANAMNESIS_30)
                End If
                If dsAsesmen.ANAMNESIS_31 <> "" Then
                    listPemeriksaanFisik.Add("Kulit : " & dsAsesmen.ANAMNESIS_31)
                End If
                If dsAsesmen.ANAMNESIS_32 <> "" Then
                    listPemeriksaanFisik.Add("status lokalis : " & dsAsesmen.ANAMNESIS_32)
                End If


                txtPemeriksaanFisik.Text = String.Join(vbCrLf, listPemeriksaanFisik.ToArray)

                txtIndikasiRawat.Text = dsAsesmen.ANAMNESIS_28
            Else
                MsgBox("Asesmen Medis Belum di Input!", MsgBoxStyle.Information, Me.Text)
            End If
        Catch oErr As Exception
            MsgBox("Load List Data : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub SimpleButton6_Click(sender As Object, e As EventArgs) Handles SimpleButton6.Click
        Try
            ' ***** CPPT *****
            Dim ds = oCPPT.GetData(sCOPYCPPTDOKTERTERAKHIR)

            If ds IsNot Nothing Then
                txtPemeriksaanFisik.Text = ds.OBJEKTIF
            Else
                MsgBox("CPPT Belum di Input", MsgBoxStyle.Information, Me.Text)
            End If
        Catch oErr As Exception
            MsgBox("Load List Data : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub SimpleButton7_Click(sender As Object, e As EventArgs) Handles SimpleButton7.Click
        Try
            'Asesmen
            Dim dsAsesmen = oDigital_DischargePlanning.GetData(lblRegister.Text)

            If dsAsesmen IsNot Nothing Then
                txtHasilPemeriksaan.Text = dsAsesmen.ANAMNESIS_33
            Else
                MsgBox("Asesmen Medis Belum di Input!", MsgBoxStyle.Information, Me.Text)
            End If
        Catch oErr As Exception
            MsgBox("Load List Data : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub SimpleButton8_Click(sender As Object, e As EventArgs) Handles SimpleButton8.Click
        Try
            ' ***** CPPT *****
            'Dim ds = oCPPT.GetData(sCOPYCPPTDOKTERTERAKHIR)
            Dim dsTambahan = oCPPT.GetDataLainnyaTambah(sCOPYCPPTDOKTERTERAKHIR)

            If dsTambahan IsNot Nothing Then
                txtTerapi.Text = dsTambahan.CATATAN_1
                txtHasilPemeriksaan.Text = dsTambahan.CATATAN_5
                txtObatPulang.Text = dsTambahan.CATATAN_6
                txtEdukasidanIntruksi.Text = dsTambahan.CATATAN_7

                If txtHasilPemeriksaan.Text = "" Then
                    txtHasilPemeriksaan.Text = txtPEMERIKSAANPENUNJANG_CPPT.Text
                End If
            Else
                MsgBox("CPPT Belum di Input", MsgBoxStyle.Information, Me.Text)
            End If
        Catch oErr As Exception
            MsgBox("Load List Data : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub btnTutupListPengantar_Click(sender As Object, e As EventArgs) Handles btnTutupListPengantar.Click
        If btnTutupListPengantar.Text = "Tutup List" Then
            btnTutupListPengantar.Text = "Buka List"
            lGrdPengantar.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        Else
            btnTutupListPengantar.Text = "Tutup List"
            lGrdPengantar.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        End If
    End Sub
    Private Sub SimpleButton9_Click(sender As Object, e As EventArgs) Handles SimpleButton9.Click
        Try
            'S_DIGITAL_RI_13_DIAGNOSA

            For Each xloop In oDigital_DischargePlanning.GetDataDetailDiagnosa(lblRegister.Text)
                grvDIAGNOSA.Focus()
                grvDIAGNOSA.AddNewRow()
                grvDIAGNOSA.SetFocusedRowCellValue(colKATEGORI, xloop.KATEGORI)
                grvDIAGNOSA.SetFocusedRowCellValue(colKDDIAGNOSA, xloop.KDDIAGNOSA)
                grvDIAGNOSA.SetFocusedRowCellValue(colNAMADIAGNOSA, xloop.NAMADIAGNOSA)
                grvDIAGNOSA.UpdateCurrentRow()
            Next

        Catch oErr As Exception
            MsgBox("Load List Data : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub SimpleButton10_Click(sender As Object, e As EventArgs) Handles SimpleButton10.Click
        Try
            For Each xloop In oCPPT.GetDataDetailDiagnosa(sCOPYCPPTDOKTERTERAKHIR)
                grvDIAGNOSA.Focus()
                grvDIAGNOSA.AddNewRow()
                grvDIAGNOSA.SetFocusedRowCellValue(colKATEGORI, xloop.KATEGORI)
                grvDIAGNOSA.SetFocusedRowCellValue(colKDDIAGNOSA, xloop.KDDIAGNOSA)
                grvDIAGNOSA.SetFocusedRowCellValue(colNAMADIAGNOSA, xloop.NAMADIAGNOSA)
                grvDIAGNOSA.UpdateCurrentRow()
            Next

            For Each xloop In oCPPT.GetDataDetailProsedur(sCOPYCPPTDOKTERTERAKHIR)
                grvPROSEDUR.Focus()
                grvPROSEDUR.AddNewRow()
                grvPROSEDUR.SetFocusedRowCellValue(colKDPROSEDUR, xloop.KDPROSEDUR)
                grvPROSEDUR.SetFocusedRowCellValue(colNAMA, xloop.NAMAPROSEDUR)
                grvPROSEDUR.UpdateCurrentRow()
            Next
        Catch oErr As Exception
            MsgBox("Load List Data : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub BatalOrderToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles BatalOrderToolStripMenuItem.Click
        Try
            If grvPenunjang.GetFocusedRowCellValue("SEQ") Is Nothing Then
                Exit Sub
            End If

            If MsgBox("Apakah Yakin Akan dibatalkan?", MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, Me.Text) = MsgBoxResult.No Then Exit Sub

            Dim Kode As String = grvPenunjang.GetFocusedRowCellValue("KODE")
            Dim SEQ As String = grvPenunjang.GetFocusedRowCellValue("SEQ")

            Dim oConn As New SqlConnection
            Dim oComm As New SqlCommand
            Dim da As SqlDataAdapter
            Dim ds As New DataSet
            Dim SQL As String

            Dim sConn As String = Decrypt(My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Software\SIMRS\SW\", "Database", "").ToString())
            '
            oConn = New SqlConnection(sConn)
            If oConn.State = ConnectionState.Closed Then
                oConn.Open()
            End If

            SQL = "UPDATE S_REQ_ORDER_PENUNJANG_AWAL SET ISAPPROVE = 1, MEMO = 'BATAL ORDER', DATEUPDATED = '" & Now.ToString("yyyy-MM-dd HH:mm:ss") & "' "
            SQL &= "WHERE SEQ = " & SEQ & " AND KODE = '" & Kode & "' "

            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "UPDATE_S_REQ_ORDER_PENUNJANG")

            If oConn.State = ConnectionState.Open Then
                oConn.Close()
            End If

            LoadPenunjang(lblRegister.Text)

        Catch ex As Exception
            MsgBox("Load Data : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub grvPenunjang_RowStyle(sender As System.Object, e As DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs) Handles grvPenunjang.RowStyle
        If grvPenunjang.IsFilterRow(e.RowHandle) Then Exit Sub

        If grvPenunjang.GetRowCellValue(e.RowHandle, "Keterangan") = "BATAL ORDER" Then
            e.Appearance.BackColor = Color.Red
        ElseIf grvPenunjang.GetRowCellValue(e.RowHandle, "Keterangan") = "TERIMA" Then
            e.Appearance.BackColor = Color.Yellow
        Else
            If CBool(grvPenunjang.GetRowCellValue(e.RowHandle, "Terima")) = True Then
                e.Appearance.BackColor = Color.Green
            End If
        End If
    End Sub
    Private Sub grvPengantar_RowStyle(sender As System.Object, e As DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs) Handles grvPengantar.RowStyle
        If grvPengantar.IsFilterRow(e.RowHandle) Then Exit Sub

        If grvPengantar.GetRowCellValue(e.RowHandle, "Keterangan") = "BATAL ORDER" Then
            e.Appearance.BackColor = Color.Red
        ElseIf grvPengantar.GetRowCellValue(e.RowHandle, "Keterangan") = "TERIMA" Then
            e.Appearance.BackColor = Color.Yellow
        Else
            If CBool(grvPengantar.GetRowCellValue(e.RowHandle, "ISAPPROVE")) = True Then
                e.Appearance.BackColor = Color.Green
            End If
        End If
    End Sub
    Private Sub btnBackKonsultasi_Click(sender As Object, e As EventArgs) Handles btnBackKonsultasi.Click
        lgrdKonsultasi.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        lpnlKonsultasi.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        'PanelControl27.Visible = True

        fn_Konsultasi()
        pnlKonsultasi.Controls.Clear()
        If Not frmKonsulDokter Is Nothing Then frmKonsulDokter.Dispose()
        frmKonsulDokter = Nothing

        tabbedControlGroup1.SelectedTabPageIndex = 5
        tabbedControlGroup1_SelectedPageChanged()
    End Sub
    Private Sub fn_Konsultasi()
        Try
            grvKONSULTASI.OptionsSelection.MultiSelect = True
            grvKONSULTASI.SelectAll()
            grvKONSULTASI.DeleteSelectedRows()
            grvKONSULTASI.OptionsSelection.MultiSelect = False

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
            SQL &= "DARIDOKTER = C.NAME_DISPLAY "
            SQL &= ",KEPADADOKTER = B.NAME_DISPLAY "
            SQL &= ",KETERANGAN = 'DPJP KE ' + CONVERT(NVARCHAR(2), A.SEQ)  "
            SQL &= ",A.ISIKONSUL "
            SQL &= ",A.JAWABKONSUL "
            SQL &= ",A.SEQ "
            SQL &= ",A.KDDOCTOR_DARI "
            SQL &= ",A.KDDOCTOR_KEPADA "
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
            SQL &= "WHERE "
            SQL &= "A.KDREG = '" & lblRegister.Text & "' "

            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "I_TRACKING_KDDOCTOR")

            grdKONSULTASI.DataSource = ds.Tables("I_TRACKING_KDDOCTOR")
            grdKONSULTASI.ForceInitialize()

            If oConn.State = ConnectionState.Open Then
                oConn.Close()
            End If

            grvKONSULTASI.Columns("KDDOCTOR_DARI").VisibleIndex = -1
            grvKONSULTASI.Columns("KDDOCTOR_KEPADA").VisibleIndex = -1
            grvKONSULTASI.Columns("SEQ").VisibleIndex = -1

            grvKONSULTASI.BestFitColumns()

        Catch oErr As Exception
            MsgBox("Load Tracking Dokter : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub btnBUATKONSUL_Click(sender As Object, e As EventArgs) Handles btnBUATKONSUL.Click
        Try
            lgrdKonsultasi.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            lpnlKonsultasi.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            'PanelControl27.Visible = False

            ' Bersihkan konten panel
            pnlKonsultasi.Controls.Clear()

            ' Set properti form anak
            frmKonsulDokter.TopLevel = False
            frmKonsulDokter.FormBorderStyle = FormBorderStyle.None
            frmKonsulDokter.Dock = DockStyle.Fill

            ' Tambahkan form anak ke panel
            pnlKonsultasi.Controls.Add(frmKonsulDokter)

            ' Tampilkan form anak
            frmKonsulDokter.fn_LoadMe(lblRegister.Text, sKDDOCTOR_INPUT, "", "", "", "", "", "")
            frmKonsulDokter.Show()
        Catch ex As Exception
            MsgBox("Load Form Detail : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub btnJAWABKONSULTRANSAKSI_Click(sender As Object, e As EventArgs) Handles btnJAWABKONSULTRANSAKSI.Click
        Try
            If grvKONSULTASI.GetFocusedRowCellValue("SEQ") = 0 Then
                MsgBox("Silahkan Pilih data", MsgBoxStyle.Exclamation, Me.Text)
                Exit Sub
            End If

            lgrdKonsultasi.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            lpnlKonsultasi.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            'PanelControl27.Visible = False

            ' Bersihkan konten panel
            pnlKonsultasi.Controls.Clear()

            ' Set properti form anak
            frmKonsulDokter.TopLevel = False
            frmKonsulDokter.FormBorderStyle = FormBorderStyle.None
            frmKonsulDokter.Dock = DockStyle.Fill

            ' Tambahkan form anak ke panel
            pnlKonsultasi.Controls.Add(frmKonsulDokter)

            ' Tampilkan form anak
            frmKonsulDokter.fn_LoadMe(lblRegister.Text, grvKONSULTASI.GetFocusedRowCellValue("KDDOCTOR_DARI"), grvKONSULTASI.GetFocusedRowCellValue("KDDOCTOR_KEPADA"), grvKONSULTASI.GetFocusedRowCellValue("SEQ"), grvKONSULTASI.GetFocusedRowCellValue("ISIKONSUL"), grvKONSULTASI.GetFocusedRowCellValue("JAWABKONSUL"), grvKONSULTASI.GetFocusedRowCellValue("TGLKONSUL"), grvKONSULTASI.GetFocusedRowCellValue("TGLJAWAB"))
            frmKonsulDokter.Show()
        Catch ex As Exception
            MsgBox("Load Form Detail : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try

        'Dim frmKonsulDokter As New frmKonsulDokter
        'Try
        '    frmKonsulDokter.fn_LoadMe(lblRegister.Text, grvDokterBersama.GetFocusedRowCellValue("KDDOCTOR_DARI"), grvDokterBersama.GetFocusedRowCellValue("KDDOCTOR_KEPADA"), grvDokterBersama.GetFocusedRowCellValue("SEQ"), grvDokterBersama.GetFocusedRowCellValue("ISIKONSUL"), grvDokterBersama.GetFocusedRowCellValue("JAWABKONSUL"), grvDokterBersama.GetFocusedRowCellValue("TGLKONSUL"), grvDokterBersama.GetFocusedRowCellValue("TGLJAWAB"))
        '    frmKonsulDokter.ShowDialog(Me)
        'Catch ex As Exception
        '    MsgBox("Load Form Detail : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
        'Finally
        '    If Not frmKonsulDokter Is Nothing Then frmKonsulDokter.Dispose()
        '    frmKonsulDokter = Nothing
        '    fn_DokterBersama()
        '    'If sKONSULTASI <> "" Then
        '    '    txtINTRUKSILAIN.Text = txtINTRUKSILAIN.Text & vbCrLf & "Konsultasi Ke Dokter" & sKONSULTASI
        '    'End If
        'End Try
    End Sub
#End Region
#Region "idRG"
    Private Function fn_SaveCek(ByVal kode As String) As Boolean
        Try
            ' ***** HEADER *****
            Dim oCek As New Digital.clsCekGrouper
            Dim cek As Boolean = False

            Dim ds = oCek.GetStructureHeader
            With ds
                Try
                    .DATECREATED = oCek.GetData(kode).DATECREATED
                    cek = True
                Catch oErr As Exception
                    .DATECREATED = Now
                End Try
                .DATEUPDATED = Now
                .KDCEK = kode
                .MEMO = ""
                .KDUSER = sUserID
                .ISCHEKED = True
            End With

            If cek = False Then
                fn_SaveCek = oCek.InsertData(ds)
            Else
                fn_SaveCek = oCek.UpdateData(ds)
            End If
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            fn_SaveCek = False
        End Try
    End Function
    Private Function fn_NilaiGrouping(ByVal isCPPT As Boolean) As Integer
        Try
            fn_NilaiGrouping = 0

            Dim siastole As Integer = 0
            Dim diastole As Integer = 0

            Try
                siastole = TextBox13.Text
                diastole = TextBox14.Text
            Catch ex As Exception

            End Try


            'Dim oCek As New Digital.clsCekGrouper

            'Dim dsCek = oCek.GetData("CEK")
            'If dsCek Is Nothing Then
            '    If fn_SaveCek("CEK") = False Then
            '        Exit Function
            '    End If
            'Else
            '    If dsCek.ISCHEKED = True Then
            '        MsgBox("Silahkan Coba Beberapa Saat Lagi Untuk Grouper", MsgBoxStyle.Exclamation, Me.Text)
            '        Exit Function
            '    Else
            '        If fn_SaveCek("CEK") = False Then
            '            Exit Function
            '        End If
            '    End If
            'End If
            '
            'Dim nomor_kartu As String = "SIMRS12345678"
            'Dim nomor_sep As String = "SIMRS12345678"
            'Dim nomor_rm As String = "SIMRS"
            'Dim nama_pasien As String = "SIMRS TESTING"
            'Dim tgl_lahir As String = sTanggalLahir.ToString("yyyy-MM-dd HH:mm:ss")
            'Dim gender As String = IIf(lblJenisKelamin.Text = "Laki-laki", "1", "2")

            Dim nomor_kartu As String = lblKartuBPJS.Text
            Dim nomor_sep As String = lblNOMORSEP.Text
            Dim nomor_rm As String = lblNoRM.Text
            Dim nama_pasien As String = lblNamaPasien.Text
            Dim tgl_lahir As String = sTanggalLahir.ToString("yyyy-MM-dd HH:mm:ss")
            Dim gender As String = IIf(lblJenisKelamin.Text = "Laki-laki", "1", "2")

            Dim listDiagnosa As New List(Of String)
            Dim listProsedur As New List(Of String)

            If isCPPT = False Then
                For i As Integer = 0 To grvDIAGNOSA.RowCount - 2
                    If grvDIAGNOSA.GetRowCellValue(i, colKDDIAGNOSA) <> "" Then
                        listDiagnosa.Add(grvDIAGNOSA.GetRowCellValue(i, colKDDIAGNOSA))
                    End If
                Next

                For i As Integer = 0 To grvPROSEDUR.RowCount - 2
                    If grvPROSEDUR.GetRowCellValue(i, colKDPROSEDUR) <> "" Then
                        listProsedur.Add(grvPROSEDUR.GetRowCellValue(i, colKDPROSEDUR))
                    End If
                Next
            Else
                For i As Integer = 0 To grvDIAGNOSA_CPPT.RowCount - 2
                    If grvDIAGNOSA_CPPT.GetRowCellValue(i, colKDDIAGNOSA_CPPT) <> "" Then
                        listDiagnosa.Add(grvDIAGNOSA_CPPT.GetRowCellValue(i, colKDDIAGNOSA_CPPT))
                    End If
                Next

                For i As Integer = 0 To grvPROSEDUR_CPPT.RowCount - 2
                    If grvPROSEDUR_CPPT.GetRowCellValue(i, colKDPROSEDUR_CPPT) <> "" Then
                        listProsedur.Add(grvPROSEDUR_CPPT.GetRowCellValue(i, colKDPROSEDUR_CPPT))
                    End If
                Next
            End If

            If listDiagnosa.Count <= 0 Then
                MsgBox("Diagnosa Masih Kosong", MsgBoxStyle.Exclamation, Me.Text)
                Exit Function
            End If

            'If fn_DATAPASIEN(nomor_kartu, nomor_sep, nomor_rm, nama_pasien, tgl_lahir, gender) = True Then


            'End If

            If fn_08REEDIT(nomor_sep) = True Then
                If fn_00NEWCLAIM(nomor_kartu, nomor_sep, nomor_rm, nama_pasien, tgl_lahir, gender) = True Then
                    If fn_01SETCLAIMDATA(nomor_sep, deTANGGALMASUK.DateTime.ToString("yyyy-MM-dd HH:mm:ss"), deTANGGALPULANG.DateTime.ToString("yyyy-MM-dd HH:mm:ss"), nomor_kartu, siastole, diastole) = True Then
                        If fn_02IDRGDIAGNOSASET(nomor_sep, String.Join("#", listDiagnosa.ToArray)) = True Then
                            Dim Lanjut As Boolean = False

                            If listProsedur.Count > 0 Then
                                Lanjut = fn_04IDRGPROCEDURESET(nomor_sep, String.Join("#", listProsedur.ToArray))
                            Else
                                Lanjut = True
                            End If

                            If Lanjut = True Then
                                If fn_06GROUPINGIDRG(nomor_sep) = True Then
                                    If fn_07FINALIDRG(nomor_sep) = True Then
                                        If fn_09IDRGTOINACBGIMPORT(nomor_sep) = True Then
                                            Dim LanjutInacbg As Boolean = False
                                            If fn_11INACBGDIAGNOSASET(nomor_sep, String.Join("#", listDiagnosa.ToArray)) = True Then

                                                If listProsedur.Count > 0 Then
                                                    LanjutInacbg = fn_12INACBGPROCEDURESET(nomor_sep, String.Join("#", listProsedur.ToArray))
                                                Else
                                                    LanjutInacbg = True
                                                End If

                                                If LanjutInacbg = True Then
                                                    If fn_14GROUPINGINACBGSTAGE1(nomor_sep) = True Then

                                                    End If
                                                End If
                                            End If
                                        End If
                                    End If
                                End If
                            End If
                        End If
                    End If
                End If
            End If



            'oCek.UpdateIscheked("CEK", False)
        Catch oErr As Exception
            MsgBox("Nilai Grouping : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Function
    Private Function fn_DATAPASIEN(ByVal nomor_kartu As String, ByVal nomor_sep As String, ByVal nomor_rm As String, ByVal nama_pasien As String, ByVal tgl_lahir As String, ByVal gender As String) As Boolean
        Try
            fn_DATAPASIEN = False

            Dim jsonEncode As String = String.Empty

            jsonEncode = "{" & """metadata"": { " & """method"": " & """update_patient"",    " & """nomor_rm"": """ & nomor_rm & """  },  "
            jsonEncode &= """data"": {    " & """nomor_kartu"": """ & nomor_kartu & """,    "
            jsonEncode &= """nomor_rm"": """ & nomor_rm & """,    "
            jsonEncode &= """nama_pasien"": """ & nama_pasien & """,    "
            jsonEncode &= """tgl_lahir"": """ & tgl_lahir & """,    "
            jsonEncode &= """gender"": """ & gender & """  } } "


            Dim Klaim As String = oBrigging.fn_BriggingEKlaim(sURL_Claim, sGenerat_Claim, jsonEncode)

            If Klaim.Contains("ERORSIMRS") Then
                MsgBox(Klaim, MsgBoxStyle.Exclamation, Me.Text)
            Else
                Dim jsonDecode = JObject.Parse(Klaim)
                Dim sDataDuplicate = String.Empty
                Dim smessage As String = String.Empty

                sDataDuplicate = jsonDecode("metadata")("code").ToString
                smessage = jsonDecode("metadata")("message").ToString

                If sDataDuplicate = "200" Then
                    fn_DATAPASIEN = True
                Else
                    MsgBox(sDataDuplicate & "-" & smessage, MsgBoxStyle.Exclamation, Me.Text)
                End If
            End If
        Catch oErr As Exception
            MsgBox("Data Pasien: " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Function
    Public Function fn_00NEWCLAIM(ByVal nomor_kartu As String, ByVal nomor_sep As String, ByVal nomor_rm As String, ByVal nama_pasien As String, ByVal tgl_lahir As String, ByVal gender As String) As Boolean
        Try
            fn_00NEWCLAIM = False

            Dim Request As String = "{" & """metadata"": {" & """method"": " & """new_claim""    }," & """data"": {" & """nomor_kartu"": """ & nomor_kartu & """, " & """nomor_sep"": """ & nomor_sep & """, " & """nomor_rm"": """ & nomor_rm & """, " & """nama_pasien"": """ & nama_pasien & """, " & """tgl_lahir"": """ & tgl_lahir & """, " & """gender"": """ & gender & """   } } "

            Dim Klaim As String = oBrigging.fn_BriggingEKlaim(sURL_Claim, sGenerat_Claim, Request)

            If Klaim.Contains("ERORSIMRS") Then
                MsgBox(Klaim, MsgBoxStyle.Exclamation, Me.Text)
            Else
                Dim jsonDecode = JObject.Parse(Klaim)
                Dim sDataDuplicate As String = String.Empty
                Dim smessage As String = String.Empty

                sDataDuplicate = jsonDecode("metadata")("code").ToString
                smessage = jsonDecode("metadata")("message").ToString

                If sDataDuplicate = "200" Then
                    fn_00NEWCLAIM = True
                Else
                    If smessage.Contains("Duplikasi nomor SEP") Then
                        fn_00NEWCLAIM = True
                    Else
                        MsgBox("Membuat Klaim Baru" & vbCrLf & sDataDuplicate & "-" & smessage, MsgBoxStyle.Exclamation, Me.Text)
                    End If
                End If
            End If
        Catch oErr As Exception
            MsgBox("New Claim : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Function
    Private Function fn_01SETCLAIMDATA(ByVal nomor_sep As String, ByVal tgl_masuk As String, ByVal tgl_pulang As String, ByVal nomor_kartu As String, ByVal sSistole As Integer, ByVal sDiastole As Integer) As Boolean
        Try
            fn_CariRegister(lblRegister.Text)

            fn_01SETCLAIMDATA = False

            Dim jsonEncode As String = String.Empty

            Dim kelas_rawat = "3"

            If lblKELAS.Text.ToString.ToUpper = "KELAS 1" Then
                kelas_rawat = "1"
            ElseIf lblKELAS.Text.ToString.ToUpper = "KELAS 2" Then
                kelas_rawat = "2"
            Else
                kelas_rawat = "3"
            End If

            jsonEncode = "{" & """metadata"": { " & """method"": " & """set_claim_data"",    " & """nomor_sep"": """ & nomor_sep & """  },  "
            jsonEncode &= """data"": {    " & """nomor_sep"": """ & nomor_sep & """,    "
            jsonEncode &= """nomor_kartu"": """ & nomor_kartu & """,    "
            jsonEncode &= """tgl_masuk"": """ & tgl_masuk & """,    "
            jsonEncode &= """tgl_pulang"": """ & tgl_pulang & """,    "
            jsonEncode &= """cara_masuk"": """ & "gp" & """,    "
            jsonEncode &= """jenis_rawat"": """ & "1" & """,    "
            jsonEncode &= """kelas_rawat"": """ & kelas_rawat & """,    "
            jsonEncode &= """adl_sub_acute"": """ & "" & """,    "
            jsonEncode &= """adl_chronic"": """ & "" & """,    "
            jsonEncode &= """icu_indikator"": """ & "0" & """,    "
            jsonEncode &= """icu_los"": """ & "0" & """,    "
            jsonEncode &= """upgrade_class_ind"": """ & "0" & """,    "
            jsonEncode &= """add_payment_pct"": """ & "0" & """,    "
            jsonEncode &= """birth_weight"": """ & CInt(0) & """,    "
            jsonEncode &= """sistole"": """ & sSistole & """,    "
            jsonEncode &= """diastole"": """ & sDiastole & """,    "
            jsonEncode &= """discharge_status"": """ & "1" & """,    "
            jsonEncode &= """tarif_rs"": {      " & """prosedur_non_bedah"": """ & CInt(sNonBedah) & """,      "
            jsonEncode &= """prosedur_bedah"": """ & CInt(sBedah) & """,      "
            jsonEncode &= """konsultasi"": """ & CInt(sKonsultasig) & """,      "
            jsonEncode &= """tenaga_ahli"": """ & CInt(sTenagaAhli) & """,      "
            jsonEncode &= """keperawatan"": """ & CInt(sKeperawatan) & """,      "
            jsonEncode &= """penunjang"": """ & CInt(sPenunjang) & """,      "
            jsonEncode &= """radiologi"": """ & CInt(sRadiologi) & """,      "
            jsonEncode &= """laboratorium"": """ & CInt(sLab) & """,      "
            jsonEncode &= """pelayanan_darah"": """ & CInt(sPelayananDarah) & """,      "
            jsonEncode &= """rehabilitasi"": """ & CInt(sRehabilitasi) & """,      "
            jsonEncode &= """kamar"": """ & CInt(sKamar) & """,      "
            jsonEncode &= """rawat_intensif"": """ & CInt(sRawatIntensif) & """,   "
            jsonEncode &= """obat"": """ & CInt(sObat) & """,   "
            jsonEncode &= """obat_kronis"": """ & CInt(sObatKronis) & """, "
            jsonEncode &= """obat_kemoterapi"": """ & CInt(sObatKemoterpi) & """,      "
            jsonEncode &= """alkes"": """ & CInt(sAlkes) & """,      "
            jsonEncode &= """bmhp"": """ & CInt(sBMHP) & """,      "
            jsonEncode &= """sewa_alat"": """ & CInt(sSewaAlatMedis) & """    },    "
            jsonEncode &= """pemulasaraan_jenazah"": """ & "0" & """,      "
            jsonEncode &= """kantong_jenazah"": """ & "0" & """,      "
            jsonEncode &= """peti_jenazah"": """ & "0" & """,      "
            jsonEncode &= """plastik_erat"": """ & "0" & """,      "
            jsonEncode &= """desinfektan_jenazah"": """ & "0" & """,      "
            jsonEncode &= """mobil_jenazah"": """ & "0" & """,      "
            jsonEncode &= """desinfektan_mobil_jenazah"": """ & "0" & """,      "
            jsonEncode &= """covid19_status_cd"": """ & "" & """,      "
            jsonEncode &= """nomor_kartu_t"": """ & "" & """,      "
            jsonEncode &= """episodes"": """ & "" & """,      "
            jsonEncode &= """akses_naat"": """ & "" & """,      "
            jsonEncode &= """isoman_ind"": """ & "0" & """,      "
            jsonEncode &= """bayi_lahir_status_cd"": """ & "" & """,      "
            jsonEncode &= """dializer_single_use"": """ & "" & """,    "
            jsonEncode &= """kantong_darah"": """ & "" & """,    "
            jsonEncode &= """alteplase_ind"": """ & "" & """,    "
            jsonEncode &= """tarif_poli_eks"": """ & CInt(0) & """,    "
            jsonEncode &= """nama_dokter"": """ & sDOKTERUTAMA & """,    "
            jsonEncode &= """kode_tarif"": """ & sKodeTarifEClaim & """,    "
            jsonEncode &= """payor_id"": """ & "3" & """,    "
            jsonEncode &= """payor_cd"": """ & "JKN" & """,    "
            jsonEncode &= """cob_cd"": """ & "" & """,    "
            jsonEncode &= """coder_nik"": """ & "123123123123" & """  } } "

            Dim Klaim As String = oBrigging.fn_BriggingEKlaim(sURL_Claim, sGenerat_Claim, jsonEncode)

            If Klaim.Contains("ERORSIMRS") Then
                MsgBox(Klaim, MsgBoxStyle.Exclamation, Me.Text)
            Else
                Dim jsonDecode = JObject.Parse(Klaim)
                Dim sDataDuplicate = String.Empty
                Dim smessage As String = String.Empty

                sDataDuplicate = jsonDecode("metadata")("code").ToString
                smessage = jsonDecode("metadata")("message").ToString

                If sDataDuplicate = "200" Then
                    fn_01SETCLAIMDATA = True
                Else
                    MsgBox(sDataDuplicate & "-" & smessage, MsgBoxStyle.Exclamation, Me.Text)
                End If
            End If
        Catch oErr As Exception
            MsgBox("Set Claim Data : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Function
    Private Function fn_02IDRGDIAGNOSASET(ByVal nomor_sep As String, ByVal diganosa As String) As Boolean
        Try
            fn_02IDRGDIAGNOSASET = False

            Dim jsonString As String = "{" & """metadata"": {" & """method"": ""idrg_diagnosa_set""," & """nomor_sep"": """ & nomor_sep & """" & "}," & """data"": {" & """diagnosa"": """ & diganosa & """" & "}" & "}"

            Dim Klaim As String = oBrigging.fn_BriggingEKlaim(sURL_Claim, sGenerat_Claim, jsonString)

            If Klaim.Contains("ERORSIMRS") Then
                MsgBox(Klaim, MsgBoxStyle.Exclamation, Me.Text)
            Else
                Dim jsonDecode = JObject.Parse(Klaim)
                Dim sDataDuplicate As String = String.Empty
                Dim smessage As String = String.Empty

                sDataDuplicate = jsonDecode("metadata")("code").ToString
                smessage = jsonDecode("metadata")("message").ToString

                If sDataDuplicate = "200" Then
                    fn_02IDRGDIAGNOSASET = True
                Else
                    MsgBox("Diagnosa Set" & vbCrLf & sDataDuplicate & "-" & smessage, MsgBoxStyle.Exclamation, Me.Text)
                End If
            End If
        Catch oErr As Exception
            MsgBox("Diagnosa Set : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Function
    Private Function fn_04IDRGPROCEDURESET(ByVal nomor_sep As String, ByVal prosedur As String) As Boolean
        Try
            fn_04IDRGPROCEDURESET = False

            Dim jsonString As String = "{" & """metadata"": {" & """method"": ""idrg_procedure_set""," & """nomor_sep"": """ & nomor_sep & """" & "}," & """data"": {" & """procedure"": """ & prosedur & """" & "}" & "}"

            Dim Klaim As String = oBrigging.fn_BriggingEKlaim(sURL_Claim, sGenerat_Claim, jsonString)

            If Klaim.Contains("ERORSIMRS") Then
                MsgBox(Klaim, MsgBoxStyle.Exclamation, Me.Text)
            Else
                Dim jsonDecode = JObject.Parse(Klaim)
                Dim sDataDuplicate As String = String.Empty
                Dim smessage As String = String.Empty

                sDataDuplicate = jsonDecode("metadata")("code").ToString
                smessage = jsonDecode("metadata")("message").ToString

                If sDataDuplicate = "200" Then
                    fn_04IDRGPROCEDURESET = True
                Else
                    MsgBox("Membuat Prosedur Set" & vbCrLf & sDataDuplicate & "-" & smessage, MsgBoxStyle.Exclamation, Me.Text)
                End If
            End If
        Catch oErr As Exception
            MsgBox("Membuat Prosedur Set : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Function
    Private Function fn_06GROUPINGIDRG(ByVal nomor_sep As String) As Boolean
        Try
            fn_06GROUPINGIDRG = False

            Dim jsonString As String = "{" & """metadata"": {" & """method"": ""grouper""," & """stage"": """ & "1" & """" & "," & """grouper"": """ & "idrg" & """" & "}," & """data"": {" & """nomor_sep"": """ & nomor_sep & """" & "}" & "}"

            Dim Klaim As String = oBrigging.fn_BriggingEKlaim(sURL_Claim, sGenerat_Claim, jsonString)

            If Klaim.Contains("ERORSIMRS") Then
                MsgBox(Klaim, MsgBoxStyle.Exclamation, Me.Text)
            Else
                Dim jsonDecode = JObject.Parse(Klaim)
                Dim sDataDuplicate As String = String.Empty
                Dim smessage As String = String.Empty

                sDataDuplicate = jsonDecode("metadata")("code").ToString
                smessage = jsonDecode("metadata")("message").ToString

                If sDataDuplicate = "200" Then
                    fn_06GROUPINGIDRG = True
                Else
                    MsgBox("Grouping Stage 1" & vbCrLf & sDataDuplicate & "-" & smessage, MsgBoxStyle.Exclamation, Me.Text)
                End If
            End If
        Catch oErr As Exception
            MsgBox("Grouping Stage 1 : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Function
    Private Function fn_07FINALIDRG(ByVal nomor_sep As String) As Boolean
        Try
            fn_07FINALIDRG = False

            Dim jsonString As String = "{" & """metadata"": {" & """method"": ""idrg_grouper_final""}," & """data"": {" & """nomor_sep"": """ & nomor_sep & """" & "}" & "}"

            Dim Klaim As String = oBrigging.fn_BriggingEKlaim(sURL_Claim, sGenerat_Claim, jsonString)

            If Klaim.Contains("ERORSIMRS") Then
                MsgBox(Klaim, MsgBoxStyle.Exclamation, Me.Text)
            Else
                Dim jsonDecode = JObject.Parse(Klaim)
                Dim sDataDuplicate As String = String.Empty
                Dim smessage As String = String.Empty

                sDataDuplicate = jsonDecode("metadata")("code").ToString
                smessage = jsonDecode("metadata")("message").ToString

                If sDataDuplicate = "200" Then
                    fn_07FINALIDRG = True
                Else
                    MsgBox("Final iDRG" & vbCr & sDataDuplicate & "-" & smessage, MsgBoxStyle.Exclamation, Me.Text)
                End If
            End If
        Catch oErr As Exception
            MsgBox("Final iDRG : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Function
    Private Function fn_08REEDIT(ByVal nomor_sep As String) As Boolean
        Try
            fn_08REEDIT = False

            Dim jsonString As String = "{" & """metadata"": {" & """method"": ""idrg_grouper_reedit""}," & """data"": {" & """nomor_sep"": """ & nomor_sep & """" & "}" & "}"

            Dim Klaim As String = oBrigging.fn_BriggingEKlaim(sURL_Claim, sGenerat_Claim, jsonString)

            If Klaim.Contains("ERORSIMRS") Then
                MsgBox(Klaim, MsgBoxStyle.Exclamation, Me.Text)
            Else
                Dim jsonDecode = JObject.Parse(Klaim)
                Dim sDataDuplicate As String = String.Empty
                Dim smessage As String = String.Empty

                sDataDuplicate = jsonDecode("metadata")("code").ToString
                smessage = jsonDecode("metadata")("message").ToString

                If sDataDuplicate = "200" Then
                    fn_08REEDIT = True
                Else
                    If smessage = "iDRG coding belum final" Then
                        fn_08REEDIT = True
                    ElseIf smessage = "Nomor SEP tidak ditemukan"
                        fn_08REEDIT = True
                    Else
                        MsgBox("ReEdit idRG" & vbCrLf & sDataDuplicate & "-" & smessage, MsgBoxStyle.Exclamation, Me.Text)
                    End If
                End If
            End If
        Catch oErr As Exception
            MsgBox("ReEdit idRG : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Function
    Private Function fn_09IDRGTOINACBGIMPORT(ByVal nomor_sep As String) As Boolean
        Try
            fn_09IDRGTOINACBGIMPORT = False

            Dim jsonString As String = "{" & """metadata"": {" & """method"": ""idrg_to_inacbg_import""}," & """data"": {" & """nomor_sep"": """ & nomor_sep & """" & "}" & "}"

            Dim Klaim As String = oBrigging.fn_BriggingEKlaim(sURL_Claim, sGenerat_Claim, jsonString)

            If Klaim.Contains("ERORSIMRS") Then
                MsgBox(Klaim, MsgBoxStyle.Exclamation, Me.Text)
            Else
                Dim jsonDecode = JObject.Parse(Klaim)
                Dim sDataDuplicate As String = String.Empty
                Dim smessage As String = String.Empty

                sDataDuplicate = jsonDecode("metadata")("code").ToString
                smessage = jsonDecode("metadata")("message").ToString

                If sDataDuplicate = "200" Then
                    fn_09IDRGTOINACBGIMPORT = True
                Else
                    MsgBox("Import" & vbCrLf & sDataDuplicate & "-" & smessage, MsgBoxStyle.Exclamation, Me.Text)
                End If
            End If
        Catch oErr As Exception
            MsgBox("Import : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Function
    Private Function fn_11INACBGDIAGNOSASET(ByVal nomor_sep As String, ByVal diagnosa As String) As Boolean
        Try
            fn_11INACBGDIAGNOSASET = False


            Dim jsonString As String = "{" & """metadata"": {" & """method"": ""inacbg_diagnosa_set""," & """nomor_sep"": """ & nomor_sep & """" & "}," & """data"": {" & """diagnosa"": """ & diagnosa & """" & "}" & "}"

            Dim Klaim As String = oBrigging.fn_BriggingEKlaim(sURL_Claim, sGenerat_Claim, jsonString)

            If Klaim.Contains("ERORSIMRS") Then
                MsgBox(Klaim, MsgBoxStyle.Exclamation, Me.Text)
            Else
                Dim jsonDecode = JObject.Parse(Klaim)
                Dim sDataDuplicate As String = String.Empty
                Dim smessage As String = String.Empty

                sDataDuplicate = jsonDecode("metadata")("code").ToString
                smessage = jsonDecode("metadata")("message").ToString

                If sDataDuplicate = "200" Then
                    fn_11INACBGDIAGNOSASET = True
                Else
                    MsgBox("Diagnosa Icd Set" & vbCrLf & sDataDuplicate & "-" & smessage, MsgBoxStyle.Exclamation, Me.Text)
                End If
            End If
        Catch oErr As Exception
            MsgBox("New Claim fn_11INACBGDIAGNOSASET : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Function
    Private Function fn_12INACBGPROCEDURESET(ByVal nomor_sep As String, ByVal prosedur As String) As Boolean
        Try
            fn_12INACBGPROCEDURESET = False

            Dim jsonString As String = "{" & """metadata"": {" & """method"": ""inacbg_procedure_set""," & """nomor_sep"": """ & nomor_sep & """" & "}," & """data"": {" & """procedure"": """ & prosedur & """" & "}" & "}"

            Dim Klaim As String = oBrigging.fn_BriggingEKlaim(sURL_Claim, sGenerat_Claim, jsonString)

            If Klaim.Contains("ERORSIMRS") Then
                MsgBox(Klaim, MsgBoxStyle.Exclamation, Me.Text)
            Else
                Dim jsonDecode = JObject.Parse(Klaim)
                Dim sDataDuplicate As String = String.Empty
                Dim smessage As String = String.Empty

                sDataDuplicate = jsonDecode("metadata")("code").ToString
                smessage = jsonDecode("metadata")("message").ToString

                If sDataDuplicate = "200" Then
                    fn_12INACBGPROCEDURESET = True
                Else
                    MsgBox("Procedure Icd Set" & vbCrLf & sDataDuplicate & "-" & smessage, MsgBoxStyle.Exclamation, Me.Text)
                End If
            End If
        Catch oErr As Exception
            MsgBox("New Claim fn_11INACBGDIAGNOSASET : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Function
    Private Function fn_14GROUPINGINACBGSTAGE1(ByVal nomor_sep As String) As Boolean
        Try
            fn_14GROUPINGINACBGSTAGE1 = False

            Dim jsonString As String = "{" & """metadata"": {" & """method"": ""grouper""," & """stage"": """ & "1" & """" & "," & """grouper"": """ & "inacbg" & """" & "}," & """data"": {" & """nomor_sep"": """ & nomor_sep & """" & "}" & "}"

            Dim Klaim As String = oBrigging.fn_BriggingEKlaim(sURL_Claim, sGenerat_Claim, jsonString)

            If Klaim.Contains("ERORSIMRS") Then
                MsgBox(Klaim, MsgBoxStyle.Exclamation, Me.Text)
            Else
                Dim jsonDecode = JObject.Parse(Klaim)
                Dim sDataDuplicate As String = String.Empty
                Dim smessage As String = String.Empty

                sDataDuplicate = jsonDecode("metadata")("code").ToString
                smessage = jsonDecode("metadata")("message").ToString

                If sDataDuplicate = "200" Then
                    'Dim hasil As String = jsonDecode("response_inacbg").ToString
                    Dim CodeCBG As String = String.Empty
                    Dim CodeCBGHarga As Decimal = 0

                    Try
                        CodeCBG = jsonDecode("response_inacbg")("cbg")("code").ToString & "-" & jsonDecode("response_inacbg")("cbg")("description").ToString
                        CodeCBGHarga = jsonDecode("response_inacbg")("tariff").ToString
                    Catch ex As Exception
                    End Try
                    'Try
                    '    txtCodeSubAcute.Text = jsonDecode("response_inacbg")("sub_acute")("code").ToString
                    '    txtDescriptionSubAcute.Text = jsonDecode("response_inacbg")("sub_acute")("description").ToString
                    '    txtTarifSubAcute.Text = jsonDecode("response_inacbg")("sub_acute")("tariff").ToString
                    'Catch ex As Exception
                    'End Try
                    'Try
                    '    txtCodeChronic.Text = jsonDecode("response_inacbg")("chronic")("code").ToString
                    '    txtDescriptionChronic.Text = jsonDecode("response_inacbg")("chronic")("description").ToString
                    '    txtTarifChronic.Text = jsonDecode("response_inacbg")("chronic")("tariff").ToString
                    'Catch ex As Exception
                    'End Try

                    If jsonDecode("special_cmg_option") IsNot Nothing Then
                        Dim sPecialCMG As Boolean = False

                        'Dim arrDetail = oRIdentitasGrouperData.GetStructureDetaiSpecialCMGlList
                        'Dim sSeq As Integer = 0

                        For Each item In jsonDecode("special_cmg_option")
                            sPecialCMG = True

                            'Dim dsDetail = oRIdentitasGrouperData.GetStructureDetailSpecialCMG

                            'With dsDetail
                            '    .datecreated = Now
                            '    .dateupdated = Now
                            '    .KDGROUPER = sNoId
                            '    .seq = sSeq
                            '    .code = item("code")
                            '    .description = item("description")
                            '    .type = item("type")
                            '    .kduser = sUserID
                            'End With

                            'arrDetail.Add(dsDetail)

                            'sSeq += 1
                        Next

                        'If arrDetail.Count > 0 Then
                        '    oRIdentitasGrouperData.DeleteDataSpecialCMG(sNoId)
                        '    oRIdentitasGrouperData.InsertDataSpecialCMG(arrDetail)
                        'End If

                        'fn_LoadDataSpecialCMG()

                        If sPecialCMG = True Then
                            'fn_14GROUPINGINACBGSTAGE1 = True
                            MsgBox("Terdapat Spesial cmg !!!", MsgBoxStyle.Information, Me.Text)
                        Else
                            fn_14GROUPINGINACBGSTAGE1 = True
                        End If
                    Else
                        fn_14GROUPINGINACBGSTAGE1 = True
                    End If

                    fn_SaveGrouper(CodeCBGHarga, "", CodeCBG)
                    fn_LoadDataGrouper()
                Else
                    MsgBox("Stage 1" & vbCrLf & sDataDuplicate & "-" & smessage, MsgBoxStyle.Exclamation, Me.Text)
                End If
            End If
        Catch oErr As Exception
            MsgBox("Stage 1 : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Function
    Private Function fn_17REEDITINACBG(ByVal nomor_sep As String) As Boolean
        Try
            fn_17REEDITINACBG = False

            Dim jsonString As String = "{" & """metadata"": {" & """method"": ""inacbg_grouper_reedit""}," & """data"": {" & """nomor_sep"": """ & nomor_sep & """" & "}" & "}"

            Dim Klaim As String = oBrigging.fn_BriggingEKlaim(sURL_Claim, sGenerat_Claim, jsonString)

            If Klaim.Contains("ERORSIMRS") Then
                MsgBox(Klaim, MsgBoxStyle.Exclamation, Me.Text)
            Else
                Dim jsonDecode = JObject.Parse(Klaim)
                Dim sDataDuplicate As String = String.Empty
                Dim smessage As String = String.Empty

                sDataDuplicate = jsonDecode("metadata")("code").ToString
                smessage = jsonDecode("metadata")("message").ToString

                If sDataDuplicate = "200" Then
                    fn_17REEDITINACBG = True
                Else
                    If smessage = "INACBG coding belum final" Then
                        fn_17REEDITINACBG = True
                    ElseIf smessage = "Klaim sudah final" Then
                        MsgBox("ReEdit Inacbg" & vbCrLf & sDataDuplicate & "-" & smessage, MsgBoxStyle.Exclamation, Me.Text)
                    End If
                End If
            End If
        Catch oErr As Exception
            MsgBox("ReEdit Inacbg : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Function
    Private Sub btnTutupFormulirMPP_Click(sender As Object, e As EventArgs) Handles btnTutupFormulirMPP.Click
        If btnTutupFormulirMPP.Text = "Tutup List" Then
            btnTutupFormulirMPP.Text = "Buka List"
            lFormulirMPP_Viewer_grd.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        Else
            btnTutupFormulirMPP.Text = "Tutup List"
            lFormulirMPP_Viewer_grd.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        End If
    End Sub
    Private Sub CheckEdit39_CheckedChanged(sender As Object, e As EventArgs) Handles CheckEdit39.CheckedChanged
        cboTINGKATKESADARAN.Properties.ReadOnly = CheckEdit39.Checked
    End Sub
#End Region
End Class