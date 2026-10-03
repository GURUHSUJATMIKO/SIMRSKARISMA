Imports DataAccess
Imports UI.WIN.MAIN.My.Resources
Imports System.Linq
Imports System.Data.SqlClient
Imports System.IO
Imports iTextSharp.text
Imports iTextSharp.text.pdf
Imports DevExpress.XtraSplashScreen
Imports Newtonsoft.Json.Linq

Public Class frmEmedrekReportList
#Region "Function"
    Private oCPPT As New Transaksi.clsCPPT
    Private oRINGKASANKELUAR As New Transaksi.clsRingkasanKeluarRawatInap
    Private oS_DIGITAL_HANDOVER As New Transaksi.clsDigital_HandOver
    Private oTransferInternal As New Transaksi.clsTransferInternal
    Private oTindakanEvaluasiKeperawatan As New Digital.clsDiagnosaPerawat
    Private oS_DIGITAL_FARMASI_REKONSILIASIOBAT_H As New Transaksi.clsRekonsiliasiObat
    Private oLaporanOperasi As New EMedrek.clsS_DIGITAL_OK_LAPORANOPERASI
    Private oLaporanTindakan As New EMedrek.clsS_DIGITAL_OK_LAPORANTINDAKAN
    Private oBrigging As New Brigging.clsSetKoneksi
    Private oLembarObservasi As New Inventory.clsLembarObservasi
    Private oIGD_GerdQ As New Inventory.clsDigital_IGD_01_GERD
    Private arrfname As New List(Of String)()
    Private oCatatatanKeperawatan As New Inventory.clsCatatanPerawat
    Private oKoding As New Transaksi.clsReq_Recipe
    Private oKoneksi As New Brigging.clsSetKoneksi
    Private AlamatpdfLaboratorium As New List(Of String)
    Private AlamatpdfRadiologi As New List(Of String)
    Private AlamatpdfUpload As New List(Of String)
    Private oIGD As New Transaksi.clsDigital_IGD_01
    Private oMerge As New Reference.clsMerge
    Private sTanggalPulangDiFo As DateTime = Now
    Private sLoad As Boolean = False

    Private Sub Me_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.Text = "Medrek List"
        chkTanggalPulang.Visible = False
        deDATEFrom.DateTime = Now
        deDATETo.DateTime = Now
        sUmurPasienDiCPPT = ""
        fn_LoadSecurity()
        tabbedControlGroup1.SelectedTabPageIndex = 0
        sLoad = True

        Try
            If Not Directory.Exists("C:/BJG/TEMPLATE") Then
                Directory.CreateDirectory("C:/BJG/TEMPLATE")
            Else
                DeleteDirectory("C:/BJG/TEMPLATE")
                Directory.CreateDirectory("C:/BJG/TEMPLATE")
            End If
        Catch ex As Exception

        End Try

    End Sub
    Private Sub Me_FormClosed(sender As System.Object, e As System.Windows.Forms.FormClosedEventArgs) Handles MyBase.FormClosed
        fn_ClosePDF()
    End Sub
    Private Sub fn_ClosePDF()
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
        PdfViewerMerge.CloseDocument()
        PdfViewerLaporanOperasi.CloseDocument()
    End Sub
    Private Sub fn_LoadSecurity()
        Try
            grvDetail.OptionsSelection.MultiSelect = True
            grvDetail.SelectAll()
            grvDetail.DeleteSelectedRows()
            grvDetail.OptionsSelection.MultiSelect = False

            Dim oOtority As New Setting.clsOtority
            Dim oUser As New Setting.clsUser

            If sLoad = False Then
                Dim dsCasemix = (From x In oOtority.GetDataDetail
                                 Join y In oUser.GetData
                             On x.KDOTORITY Equals y.KDOTORITY
                                 Where x.MODUL = "CASEMIX" _
                             And y.KDUSER = sUserID
                                 Select x.ISADD, x.ISDELETE, x.ISUPDATE, x.ISPRINT, x.ISVIEW).FirstOrDefault

                If dsCasemix IsNot Nothing Then
                    tab15.PageVisible = True
                    grdDetail.ContextMenuStrip = ContextMenuStrip1
                    btnKonfirmasi.Visible = True
                    btnBrowse.Visible = True
                    txtFolder.Visible = True
                Else
                    tab15.PageVisible = False
                    grdDetail.ContextMenuStrip = Nothing
                    btnKonfirmasi.Visible = False
                    btnBrowse.Visible = False
                    txtFolder.Visible = False
                End If

                Dim dsKoding = (From x In oOtority.GetDataDetail
                                Join y In oUser.GetData
                            On x.KDOTORITY Equals y.KDOTORITY
                                Where x.MODUL = "KODING_KLAIM" _
                            And y.KDUSER = sUserID
                                Select x.ISADD, x.ISDELETE, x.ISUPDATE, x.ISPRINT, x.ISVIEW).FirstOrDefault

                If dsKoding IsNot Nothing Then
                    tab16.Visible = True
                Else
                    tab16.Visible = False
                End If

            End If

            Dim ds = (From x In oOtority.GetDataDetail
                      Join y In oUser.GetData
                      On x.KDOTORITY Equals y.KDOTORITY
                      Where x.MODUL = "REKAMMEDIS_ERM" _
                      And y.KDUSER = sUserID
                      Select x.ISADD, x.ISDELETE, x.ISUPDATE, x.ISPRINT, x.ISVIEW).FirstOrDefault

            Try
                btnRefresh.Enabled = ds.ISVIEW

                If ds.ISVIEW = True Then
                    If cboFILTER.SelectedIndex = 0 Then
                        If txtPARAMETER.Text = "" Then Exit Sub
                    ElseIf cboFILTER.SelectedIndex = 1 Then
                        If txtPARAMETER.Text = "" Then Exit Sub
                    End If
                    fn_LoadDataPendaftaran()
                End If
            Catch oErr As Exception
                MsgBox(Statement.SecurityNotInstalled, MsgBoxStyle.Exclamation, Me.Text)

                btnRefresh.Enabled = False
            End Try

        Catch oErr As Exception
            MsgBox(Statement.SecurityNotInstalled, MsgBoxStyle.Exclamation, Me.Text)
        End Try
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

            SQL = "SELECT "
            SQL &= "Cek = ISNULL((SELECT CONVERT(BIT,1) FROM S_MERGE WHERE B.KDREG = KDREG), CONVERT(BIT,0)) "
            SQL &= ",Penjamin = I.NAME_DISPLAY "
            SQL &= ",NoRekamMedis = A.KDCUSTOMER "
            SQL &= ",Pasien = A.NAME_DISPLAY + ' ' + IIf(A.FRONT_TITLE IS NULL, '', A.FRONT_TITLE + ' ') + A.BACK_TITLE "
            SQL &= ",TanggalLahir = A.TANGGALLAHIR "
            SQL &= ",JenisKelamin = (SELECT CASE A.JK WHEN 1 THEN 'LAKI-LAKI' ELSE 'PEREMPUAN' END) "
            SQL &= ",Tujuan = C.NAME_DISPLAY "
            SQL &= ",Dokter = D.NAME_DISPLAY "
            SQL &= ",NoRegister = B.KDREG "
            SQL &= ",NoRegister_Awal = ISNULL((B.KDREGAWAL), '') "
            SQL &= ",NoSEP = B.NOMORSEP "
            SQL &= ",NoKartu = B.KARTUBPJS "
            SQL &= ",NoTelepon = B.PHONE "
            SQL &= ",Faskes = ISNULL((SELECT DESCRIPTION FROM M_PPK WHERE B.KDPPK = KDPPK), '') "
            SQL &= ",Faskes_Label = CASE B.KDASALRUJUKAN WHEN '1' THEN 'Faskes Tingkat 1' ELSE 'Faskes Tingkat 2' END "
            SQL &= ",DiagnosaAwal = F.KDDIAGNOSA + '-' + F.DESCRIPTION "
            SQL &= ",Catatan = B.CATATAN "
            SQL &= ",TanggalDaftar = B.DATE "
            SQL &= ",Peserta = ISNULL((SELECT DESCRIPTION FROM M_JENISPESERTA WHERE B.KDPESERTA = KDPESERTA), '') "
            SQL &= ",Cob = ISNULL((SELECT DESCRIPTION FROM M_COB WHERE B.KDCOB = KDCOB), '-') "
            SQL &= ",JenisRawat = CASE B.CATEGORY WHEN '1' THEN 'RAWAT JALAN' ELSE 'RAWAT INAP' END "
            SQL &= ",KelasRawat = ISNULL((SELECT DESCRIPTION FROM M_KELASRAWAT WHERE B.KDKELASRAWAT = KDKELASRAWAT), '') "
            SQL &= ",Status = CASE B.STATUSDAFTAR WHEN '1' THEN 'DALAM ANTRIAN' WHEN '2' THEN 'SELESAI' WHEN '3' THEN 'BATAL' END "
            SQL &= ",Resume = B.ISRESUME "
            SQL &= ",PulangdiRanap = B.ISUPDATEPULANG "
            SQL &= ",TanggalPulang = B.DATEPULANG "
            SQL &= ",TanggalPulangResume = ISNULL((SELECT FORMAT(TANGGALPULANG, 'dd-MM-yyyy') FROM RSKC_NEW..S_RINGKASANKELUARRAWATINAP WHERE B.KDREG = KDREG COLLATE Latin1_General_CI_AS), '-') "
            SQL &= ",CekTanggalPulang = CASE B.ISUPDATEPULANG WHEN 0 THEN (CASE B.CATEGORY WHEN 1 THEN '-' ELSE 'Belum di Pulangkan' END) ELSE 'Pulang' END "
            SQL &= ",Ket = CASE B.ISUPDATEPULANG WHEN 0 THEN (CASE B.CATEGORY WHEN 1 THEN '-' ELSE 'Belum di Pulangkan' END) ELSE 'Pulang' END  "
            If cboFILTER.SelectedIndex = 0 Then
                SQL &= ",Invoice = '' "
            ElseIf cboFILTER.SelectedIndex = 1 Then
                SQL &= ",Invoice = '' "
            ElseIf cboFILTER.SelectedIndex = 2 Then
                SQL &= ",Invoice = '' "
            ElseIf cboFILTER.SelectedIndex = 3 Then
                SQL &= ",Invoice = J.KDCASHIER "
            ElseIf cboFILTER.SelectedIndex = 4 Then
                SQL &= ",Invoice = J.KDCASHIER "
            End If
            SQL &= "FROM "
            SQL &= "M_CUSTOMER A "
            SQL &= "INNER JOIN S_PENDAFTARAN_H B "
            SQL &= "ON A.KDCUSTOMER = B.KDCUSTOMER "
            SQL &= "INNER JOIN M_DEPARTMENT C "
            SQL &= "ON B.KDDEPARTMENT = C.KDDEPARTMENT "
            SQL &= "INNER JOIN M_DOCTOR D "
            SQL &= "ON B.KDDOCTOR = D.KDDOCTOR "
            SQL &= "INNER JOIN M_DIAGNOSAICD10 F "
            SQL &= "ON B.KDDIAGNOSA = F.KDDIAGNOSA "
            SQL &= "INNER JOIN M_DEBTOR I "
            SQL &= "ON B.KDDEBTOR = I.KDDEBTOR "
            If cboFILTER.SelectedIndex = 0 Then
                SQL &= "WHERE A.KDCUSTOMER LIKE '%" & txtPARAMETER.Text & "%' "
            ElseIf cboFILTER.SelectedIndex = 1 Then
                SQL &= "WHERE A.NAME_DISPLAY LIKE '%" & txtPARAMETER.Text & "%' "
            ElseIf cboFILTER.SelectedIndex = 2 Then
                SQL &= "WHERE CONVERT(VARCHAR(8), B.DATE, 112) >= '" & deDATEFrom.DateTime.ToString("yyyyMMdd") & "' AND CONVERT(VARCHAR(8), B.DATE, 112) <= '" & deDATETo.DateTime.ToString("yyyyMMdd") & "' "
            ElseIf cboFILTER.SelectedIndex = 3 Then
                SQL &= "INNER JOIN F_CASHIER_H J "
                SQL &= "ON B.KDREG = J.KDREG "
                SQL &= "WHERE CONVERT(VARCHAR(8), B.DATE, 112) >= '" & deDATEFrom.DateTime.ToString("yyyyMMdd") & "' AND CONVERT(VARCHAR(8), B.DATE, 112) <= '" & deDATETo.DateTime.ToString("yyyyMMdd") & "' "
                SQL &= "AND B.CATEGORY = 1 "
            ElseIf cboFILTER.SelectedIndex = 4 Then
                SQL &= "INNER JOIN F_CASHIER_H J "
                SQL &= "ON B.KDREG = J.KDREG "
                If chkTanggalPulang.Checked = False Then
                    SQL &= "WHERE CONVERT(VARCHAR(8), B.DATE, 112) >= '" & deDATEFrom.DateTime.ToString("yyyyMMdd") & "' AND CONVERT(VARCHAR(8), B.DATE, 112) <= '" & deDATETo.DateTime.ToString("yyyyMMdd") & "' "
                Else
                    SQL &= "WHERE CONVERT(VARCHAR(8), B.DATEPULANG, 112) >= '" & deDATEFrom.DateTime.ToString("yyyyMMdd") & "' AND CONVERT(VARCHAR(8), B.DATEPULANG, 112) <= '" & deDATETo.DateTime.ToString("yyyyMMdd") & "' "
                End If
                SQL &= "AND B.CATEGORY = 2 "
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

            'Dim listPendaftaran As New List(Of DataAccess.R_LISTPASIEN)

            'For iLoop As Integer = 0 To ds.Tables("S_LISTPASIEN").Rows.Count - 1
            '    Dim dsRekap As New DataAccess.R_LISTPASIEN
            '    With ds.Tables("S_LISTPASIEN")
            '        dsRekap.Cek = .Rows(iLoop)("Cek")
            '        dsRekap.Penjamin = .Rows(iLoop)("Penjamin")
            '        dsRekap.NoRekamMedis = .Rows(iLoop)("NoRekamMedis")
            '        dsRekap.Pasien = .Rows(iLoop)("Pasien")
            '        dsRekap.TanggalLahir = .Rows(iLoop)("TanggalLahir")
            '        dsRekap.JenisKelamin = .Rows(iLoop)("JenisKelamin")
            '        dsRekap.Tujuan = .Rows(iLoop)("Tujuan")
            '        dsRekap.Dokter = .Rows(iLoop)("Dokter")
            '        dsRekap.NoRegister = .Rows(iLoop)("NoRegister")
            '        dsRekap.NoRegister_Awal = .Rows(iLoop)("NoRegisterAwal")
            '        dsRekap.NoSEP = .Rows(iLoop)("NoSEP")
            '        dsRekap.NoTelepon = .Rows(iLoop)("NoTelepon")
            '        dsRekap.Faskes = .Rows(iLoop)("Faskes")
            '        dsRekap.Faskes_Label = .Rows(iLoop)("Faskes_Label")
            '        dsRekap.DiagnosaAwal = .Rows(iLoop)("DiagnosaAwal")
            '        dsRekap.Catatan = .Rows(iLoop)("Catatan")
            '        dsRekap.TanggalDaftar = .Rows(iLoop)("TanggalDaftar")
            '        dsRekap.Peserta = .Rows(iLoop)("Peserta")
            '        dsRekap.Cob = .Rows(iLoop)("Cob")
            '        dsRekap.JenisRawat = .Rows(iLoop)("JenisRawat")
            '        dsRekap.KelasRawat = .Rows(iLoop)("KelasRawat")
            '        dsRekap.Status = .Rows(iLoop)("Status")
            '        dsRekap.Resume = .Rows(iLoop)("Resume")
            '        dsRekap.PulangdiRanap = .Rows(iLoop)("PulangdiRanap")
            '        dsRekap.TanggalPulang = CDate(.Rows(iLoop)("TanggalPulang")).ToString("dd-MM-yyyy")
            '        Dim dsCek = oRINGKASANKELUAR.GetData(.Rows(iLoop)("NoRegister"))
            '        If dsCek IsNot Nothing Then
            '            dsRekap.TanggalPulangResume = dsCek.TANGGALPULANG.ToString("dd-MM-yyyy")
            '        Else
            '            dsRekap.TanggalPulangResume = "-"
            '        End If
            '        dsRekap.CekTanggalPulang = .Rows(iLoop)("CekTanggalPulang")
            '        If dsCek Is Nothing Then
            '            dsRekap.Ket = "-"
            '        Else
            '            dsRekap.Ket = IIf(CDate(.Rows(iLoop)("TanggalPulang")).ToString("ddMMyyy") = dsCek.TANGGALPULANG.ToString("ddMMyyyy"), "Sama", "Beda")
            '        End If
            '        dsRekap.Invoice = .Rows(iLoop)("Invoice")
            '        listPendaftaran.Add(dsRekap)
            '    End With
            'Next

            'grdDetail.DataSource = listPendaftaran
            'grdDetail.ForceInitialize()

            grdDetail.DataSource = ds.Tables("S_LISTPASIEN")
            grdDetail.ForceInitialize()

            fn_LoadFormatData()

        Catch oErr As Exception
            MsgBox("Load List Pasien Data : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadFormatData()
        For iLoop As Integer = 0 To grvDetail.Columns.Count - 1
            If grvDetail.Columns(iLoop).ColumnType.Name = "Decimal" Then
                grvDetail.Columns(iLoop).DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                grvDetail.Columns(iLoop).DisplayFormat.FormatString = "{0:n2}"
                grvDetail.Columns(iLoop).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
            ElseIf grvDetail.Columns(iLoop).ColumnType.Name = "DateTime" Then
                grvDetail.Columns(iLoop).DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
                grvDetail.Columns(iLoop).DisplayFormat.FormatString = "{0:dd/MM/yyyy}"
            End If
        Next

        grvDetail.BestFitColumns()
        txtPARAMETER.ResetText()
    End Sub
    Private Sub DeleteDirectory(path As String)
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
#End Region
#Region "Load Data"
    Private Sub fn_LoadPdfViewerCPPTRANANP()
        Try
            pdfViewerCPPTRANAP.CloseDocument()

            SplashScreenManager.ShowForm(Me, GetType(frmLoadingPanel), True, True, False)

            SplashScreenManager.Default.SetWaitFormCaption("Processing data CPPT Rawat Inap.....")

            Dim FolderSimpan = "C:/CPPTRANAP/"

            If Not Directory.Exists(FolderSimpan) Then
                Directory.CreateDirectory(FolderSimpan)
            Else
                DeleteDirectory(FolderSimpan)
                Directory.CreateDirectory(FolderSimpan)
            End If

            Dim dataList As New List(Of Byte())
            Dim dsLoad() As Byte
            Dim ds = oCPPT.GetDataByRMRANAP(grvDetail.GetFocusedRowCellValue("NoRekamMedis"))

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

            Dim FolderSimpan = "C:/CPPTRAJAL/"

            If Not Directory.Exists(FolderSimpan) Then
                Directory.CreateDirectory(FolderSimpan)
            Else
                DeleteDirectory(FolderSimpan)
                Directory.CreateDirectory(FolderSimpan)
            End If

            Dim dataList As New List(Of Byte())
            Dim dsLoad() As Byte
            Dim ds = oCPPT.GetDataByRMRAJAL(grvDetail.GetFocusedRowCellValue("NoRekamMedis"))

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
            SQL &= ",TANGGAL = A.TANGGALMASUK "
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

            If KATEGORI = "RESUME RAWAT JALAN" Then
                Dim dsKoding = oKoding.GetDataKoding(KODE)
                If dsKoding IsNot Nothing Then
                    Dim rpt As New xtraKoding

                    rpt.bindingSource.DataSource = dsKoding

                    DownloadIamge1 = AlamatDownloadIamge1 & grvDetail.GetFocusedRowCellValue("NoRekamMedis") & "/" & grvDetail.GetFocusedRowCellValue("NoRekamMedis") & ".png"

                    sUserIDTandaTangan = dsKoding.KDDOCTOR

                    rpt.bindingSource.DataSource = dsKoding
                    rpt.ExportToPdf(FolderSimpan & dsKoding.KDKODING & ".pdf")
                    PdfViewerResume.LoadDocument(FolderSimpan & dsKoding.KDKODING & ".pdf")
                End If
            Else
                Dim dsRingkasan = oRINGKASANKELUAR.GetData(KODE)
                If dsRingkasan IsNot Nothing Then
                    Dim rpt As New xtraRingkasanKeluarRawatInap

                    rpt.bindingSource.DataSource = dsRingkasan

                    DownloadIamge1 = AlamatDownloadIamge1 & grvDetail.GetFocusedRowCellValue("NoRekamMedis") & "/" & grvDetail.GetFocusedRowCellValue("NoRekamMedis") & ".png"

                    rpt.bindingSource.DataSource = dsRingkasan
                    rpt.ExportToPdf(FolderSimpan & dsRingkasan.KDREG & ".pdf")
                    PdfViewerResume.LoadDocument(FolderSimpan & dsRingkasan.KDREG & ".pdf")
                End If
            End If

            SplashScreenManager.CloseForm(False)

        Catch oErr As Exception
            SplashScreenManager.CloseForm(False)

            MsgBox("Cetak Resume : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
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
            SQL &= "WHERE B.KDCUSTOMER = '" & grvDetail.GetFocusedRowCellValue("NoRekamMedis") & "' "
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
        Catch oErr As Exception
            MsgBox("Search Folder : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
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

            'SQL = "SELECT "
            'SQL &= "Tanggal = D.DATE "
            'SQL &= ",Catatan = B.NAMA_TARIF "
            'SQL &= ",NoRadiologi = C.NORADIOLOGI "
            'SQL &= ",URL = 'Double Klik' "
            'SQL &= ",A.KDTRANSAKSI "
            'SQL &= ",DOKTER = (CASE E.FRONT_TITLE WHEN '' THEN '' ELSE E.FRONT_TITLE END) + ' ' + E.NAME_DISPLAY + ' ' + (CASE E.BACK_TITLE WHEN '' THEN '' ELSE E.BACK_TITLE END) "
            'SQL &= "FROM "
            'SQL &= "S_TRANSAKSI_RJ A "
            'SQL &= "INNER JOIN M_TARIF_NEW AS B "
            'SQL &= "ON A.KDTARIF = B.KDTARIF "
            'SQL &= "INNER JOIN S_TRANSAKSI_H AS C "
            'SQL &= "ON A.KDTRANSAKSI = C.KDTRANSAKSI "
            'SQL &= "INNER JOIN S_PENDAFTARAN_H D "
            'SQL &= "ON C.KDREG = D.KDREG "
            'SQL &= "INNER JOIN M_DOCTOR E "
            'SQL &= "ON C.KDDOCTOR = E.KDDOCTOR "
            'SQL &= "WHERE D.KDCUSTOMER = '" & lblNoRM.Text & "' "
            'SQL &= "ORDER BY D.DATE DESC "

            SQL = "SELECT "
            SQL &= "* "
            SQL &= "FROM ( "
            SQL &= "SELECT "
            SQL &= "Tanggal = D.DATE "
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
            SQL &= "WHERE D.KDCUSTOMER = '" & grvDetail.GetFocusedRowCellValue("NoRekamMedis") & "' "

            SQL &= "UNION "

            SQL &= "SELECT "
            SQL &= "Tanggal = D.DATE "
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
            SQL &= "WHERE D.KDCUSTOMER = '" & grvDetail.GetFocusedRowCellValue("NoRekamMedis") & "' "

            SQL &= "UNION "

            SQL &= "SELECT "
            SQL &= "Tanggal = D.DATE "
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
            SQL &= "WHERE D.KDCUSTOMER = '" & grvDetail.GetFocusedRowCellValue("NoRekamMedis") & "' "

            SQL &= "UNION "

            SQL &= "SELECT "
            SQL &= "Tanggal = B.DATE "
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
            SQL &= "WHERE B.KDCUSTOMER = '" & grvDetail.GetFocusedRowCellValue("NoRekamMedis") & "' "
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
            SQL &= ",KDDOCTOR_DARI "
            SQL &= ",KDDOCTOR_KEPADA "
            SQL &= "FROM "
            SQL &= "I_TRACKING_KDDOCTOR A "
            SQL &= "INNER JOIN M_DOCTOR B "
            SQL &= "ON A.KDDOCTOR_KEPADA = B.KDDOCTOR "
            SQL &= "INNER JOIN M_DOCTOR C "
            SQL &= "ON A.KDDOCTOR_DARI = C.KDDOCTOR "
            SQL &= "INNER JOIN S_PENDAFTARAN_H D "
            SQL &= "ON A.KDREG = D.KDREG "
            SQL &= "WHERE "
            SQL &= "D.KDCUSTOMER = '" & grvDetail.GetFocusedRowCellValue("NoRekamMedis") & "' "

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

            For Each xloop In oDigital.GetDataByRM(grvDetail.GetFocusedRowCellValue("NoRekamMedis"))
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
    Private Sub fn_PrintStrukAsesemenMedisIGD(ByVal RM As String)
        Try
            PdfViewerAsesmenMedisAwalIGD.CloseDocument()

            SplashScreenManager.ShowForm(Me, GetType(frmLoadingPanel), True, True, False)

            SplashScreenManager.Default.SetWaitFormCaption("Processing data Asesmen Medis Awal IGD.....")

            Dim oIGD As New Transaksi.clsDigital_IGD_01
            Dim FolderSimpan = "C:/ASESMENMEDISAWALIGD/"

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

                    sUSIADIASESMENIGD = CDate(grvDetail.GetFocusedRowCellValue("TanggalLahir")).ToString("dd-MM-yyyy") & " (" & HitungUmur(CDate(grvDetail.GetFocusedRowCellValue("TanggalLahir"))) & ")"

                    'HitungUmur(CDate(grvDetail.GetFocusedRowCellValue("TanggalLahir")), CDate(grvDetail.GetFocusedRowCellValue("TanggalDaftar")))

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
                dsLoad = MergeFilesByte(dataList)
                Dim stream As New MemoryStream(dsLoad)
                PdfViewerAsesmenMedisAwalIGD.LoadDocument(stream)
            End If

            SplashScreenManager.CloseForm(False)
        Catch oErr As Exception
            SplashScreenManager.CloseForm(False)
            MsgBox("Cetak Asesemen Medis IGD" & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_PrintStrukAsesemenPerawatIGD(ByVal RM As String)
        Try
            PdfViewerAsesmenAwalPerawatIGD.CloseDocument()

            SplashScreenManager.ShowForm(Me, GetType(frmLoadingPanel), True, True, False)

            SplashScreenManager.Default.SetWaitFormCaption("Processing data Asesmen Perawat Awal IGD.....")

            Dim oIGD As New Transaksi.clsDigital_IGD_02
            Dim FolderSimpan = "C:/ASESMENPERAWATAWALIGD/"

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
                Try
                    Dim ds = oIGD.GetData(xloop.KDPENDAFTARAN)
                    If ds IsNot Nothing Then
                        sASESEMEN_IGD = ds.SIMPANGAMBAR_1

                        Dim rpt As New xtraReportAsesmenKeperawatanGD_New

                        rpt.ShowPrintMarginsWarning = False
                        rpt.Watermark.Text = sWATERMARK

                        ListdataKDCPPT.Add(ds.KDPENDAFTARAN)
                        rpt.BindingSource.DataSource = ds
                        rpt.ExportToPdf(FolderSimpan & ds.KDPENDAFTARAN & ".pdf")
                        dataList.Add(File.ReadAllBytes(FolderSimpan & ds.KDPENDAFTARAN & ".pdf"))
                    End If
                Catch ex As Exception

                End Try
            Next

            If dataList.Count > 0 Then
                dsLoad = MergeFilesByte(dataList)
                Dim stream As New MemoryStream(dsLoad)
                PdfViewerAsesmenAwalPerawatIGD.LoadDocument(stream)
            End If

            SplashScreenManager.CloseForm(False)
        Catch oErr As Exception
            SplashScreenManager.CloseForm(False)
            MsgBox("Cetak Asesemen Perawat IGD" & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadPdfViewerLembarObservasi()
        Try
            PdfViewerLembarObservasi.CloseDocument()

            SplashScreenManager.ShowForm(Me, GetType(frmLoadingPanel), True, True, False)

            SplashScreenManager.Default.SetWaitFormCaption("Processing data Lembar Observasi.....")

            Dim FolderSimpan = "C:/LEMBAROBSERVASI/"

            If Not Directory.Exists(FolderSimpan) Then
                Directory.CreateDirectory(FolderSimpan)
            Else
                DeleteDirectory(FolderSimpan)
                Directory.CreateDirectory(FolderSimpan)
            End If

            Dim dataList As New List(Of Byte())
            Dim dsLoad() As Byte
            Dim ds = oLembarObservasi.GetDataByRM(grvDetail.GetFocusedRowCellValue("NoRekamMedis"))

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
            SQL &= "WHERE B.KDCUSTOMER = '" & grvDetail.GetFocusedRowCellValue("NoRekamMedis") & "' "
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
            SQL &= "WHERE A.NOREKAMMEDIS = '" & grvDetail.GetFocusedRowCellValue("NoRekamMedis") & "' "
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
    Private Sub fn_LoadPdfViewerCatatanKeperawatan()
        Try
            PdfViewerCatatanKeperawatan.CloseDocument()

            SplashScreenManager.ShowForm(Me, GetType(frmLoadingPanel), True, True, False)

            SplashScreenManager.Default.SetWaitFormCaption("Processing data Catatan Keperawatan.....")

            Dim FolderSimpan = "C:/CATATANKEPERAWATANLIST/"

            If Not Directory.Exists(FolderSimpan) Then
                Directory.CreateDirectory(FolderSimpan)
            Else
                DeleteDirectory(FolderSimpan)
                Directory.CreateDirectory(FolderSimpan)
            End If

            Dim dataList As New List(Of Byte())
            Dim dsLoad() As Byte
            Dim ds = oCatatatanKeperawatan.GetDataByRM(grvDetail.GetFocusedRowCellValue("NoRekamMedis"))

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

            NAMA = grvDetail.GetFocusedRowCellValue("Pasien")
            RM = grvDetail.GetFocusedRowCellValue("NoRekamMedis")
            TANGGALLAHIR = CDate(grvDetail.GetFocusedRowCellValue("TanggalLahir")).ToString("dd-MM-yyyy")

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
            SQL &= "A.KDCUSTOMER = '" & grvDetail.GetFocusedRowCellValue("NoRekamMedis") & "' "

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
    Private Sub fn_LoadPdfViewerLaporanOperasi()
        Try
            PdfViewerLaporanOperasi.CloseDocument()

            SplashScreenManager.ShowForm(Me, GetType(frmLoadingPanel), True, True, False)

            SplashScreenManager.Default.SetWaitFormCaption("Processing data Laporan Operasi")

            Dim oDigital As New EMedrek.clsS_DIGITAL_OK_LAPORANOPERASI
            Dim FolderSimpan = "C:/LAPORANOPERASI_R/"

            If Not Directory.Exists(FolderSimpan) Then
                Directory.CreateDirectory(FolderSimpan)
            Else
                DeleteDirectory(FolderSimpan)
                Directory.CreateDirectory(FolderSimpan)
            End If

            Dim dataList As New List(Of Byte())
            Dim dsLoad() As Byte

            For Each xloop In oDigital.GetDataByRM(grvDetail.GetFocusedRowCellValue("NoRekamMedis"))
                Dim ds = oDigital.GetDataKode(xloop.KDLAPORANOPERASI)

                If ds IsNot Nothing Then
                    NAMA = grvDetail.GetFocusedRowCellValue("Pasien")
                    RM = grvDetail.GetFocusedRowCellValue("NoRekamMedis")
                    TANGGALLAHIR = CDate(grvDetail.GetFocusedRowCellValue("TanggalLahir")).ToString("dd-MM-yyyy")

                    sKDUSER_TTD = ds.KDUSER

                    Dim rpt As New xtraReportLAPORANOPERASI

                    rpt.ShowPrintMarginsWarning = False
                    rpt.Watermark.Text = sWATERMARK

                    rpt.BindingSource1.DataSource = ds
                    rpt.ExportToPdf(FolderSimpan & ds.KDPENDAFTARAN & ds.SEQ & ".pdf")
                    dataList.Add(File.ReadAllBytes(FolderSimpan & ds.KDPENDAFTARAN & ds.SEQ & ".pdf"))
                End If
            Next

            If dataList.Count > 0 Then
                dsLoad = MergeFilesByte(dataList)
                Dim stream As New MemoryStream(dsLoad)
                PdfViewerLaporanOperasi.LoadDocument(stream)
            End If

            SplashScreenManager.CloseForm(False)
        Catch oErr As Exception
            SplashScreenManager.CloseForm(False)
            MsgBox("Cetak Laporan Operasi" & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
#End Region
#Region "Load Data Merge"
    Private Sub fn_LoadDataMerge(ByVal sSplashScreenManager As Boolean, ByVal KDCASHIER As String, ByVal PENJAMIN As String, ByVal KDREG As String, ByVal KDREGAWAL As String, ByVal NOREKAMMEDIS As String, ByVal NOMORSEP As String, ByVal TANGGALSEP As DateTime, ByVal NOKARTU As String, ByVal NAMAPASIEN As String, ByVal TANGGALLAHIR As DateTime, ByVal JENISKELAMIN As String, ByVal NOMORTELEPON As String, ByVal FASKES As String, ByVal FASKES_LABEL As String, ByVal TUJUAN As String, ByVal DIAGNOSAAWAL As String, ByVal CATATAN As String, ByVal PESERTA As String, ByVal COB As String, ByVal JENISRAWAT As String, ByVal KELASRAWAT As String)
        Try
            If sSplashScreenManager = True Then
                PdfViewerMerge.CloseDocument()
            End If

            If KDREG = "" Then Exit Sub

            Dim dataList As New List(Of Byte())

            Dim AlamatSimpanPDFMerge As String = "C:\Mergedata\"

            If My.Computer.FileSystem.DirectoryExists(AlamatSimpanPDFMerge) Then
                Try
                    DeleteDirectory(AlamatSimpanPDFMerge)
                Catch ex As Exception

                End Try
                Directory.CreateDirectory(AlamatSimpanPDFMerge)
            Else
                Directory.CreateDirectory(AlamatSimpanPDFMerge)
            End If

            '*************** MULAI SEP ***************
            If NOMORSEP <> "" Then
                If NOMORSEP <> "-" Then
                    If sSplashScreenManager = True Then
                        SplashScreenManager.ShowForm(Me, GetType(frmLoadingPanel), True, True, False)
                        SplashScreenManager.Default.SetWaitFormCaption("Processing SEP.....")
                    End If

                    Dim listSEP As New List(Of DataAccess.R_SEP)
                    Dim dsRekapSEP As New DataAccess.R_SEP
                    Dim Bulan As String = String.Empty

                    Select Case CInt(TANGGALSEP.ToString("MM"))
                        Case 1
                            Bulan = "Januari"
                        Case 2
                            Bulan = "Februari"
                        Case 3
                            Bulan = "Maret"
                        Case 4
                            Bulan = "April"
                        Case 5
                            Bulan = "Mei"
                        Case 6
                            Bulan = "Juni"
                        Case 7
                            Bulan = "Juli"
                        Case 8
                            Bulan = "Agustus"
                        Case 9
                            Bulan = "September"
                        Case 10
                            Bulan = "Oktober"
                        Case 11
                            Bulan = "Nopember"
                        Case 12
                            Bulan = "Desember"
                    End Select

                    dsRekapSEP.NOMORSEP = NOMORSEP
                    dsRekapSEP.TANGGALSEP = TANGGALSEP.ToString("dd") & " " & Bulan & " " & TANGGALSEP.ToString("yyyy")
                    dsRekapSEP.NOKARTU = NOKARTU
                    dsRekapSEP.NAMAPESERTA = NAMAPASIEN
                    dsRekapSEP.TANGGALLAHIR = TANGGALLAHIR.ToString("dd-MM-yyyy")
                    dsRekapSEP.JENISKELAMIN = IIf(JENISKELAMIN = "LAKI-LAKI", "(L)", "(P)")
                    dsRekapSEP.NOMORTELEPON = NOMORTELEPON
                    dsRekapSEP.TUJUAN = TUJUAN
                    dsRekapSEP.FASKES_LABEL = FASKES_LABEL
                    dsRekapSEP.FASKES = FASKES
                    dsRekapSEP.DIAGNOSAAWAL = DIAGNOSAAWAL
                    dsRekapSEP.CATATAN = CATATAN
                    dsRekapSEP.TANGGALCETAK = TANGGALSEP.ToString("dd-MM-yyyy")
                    dsRekapSEP.JAMCETAK = TANGGALSEP.ToString("HH:mm")
                    dsRekapSEP.CETAKKE = 1
                    dsRekapSEP.NOREKAMMEDIS = NOREKAMMEDIS
                    dsRekapSEP.KDREG = KDREG
                    dsRekapSEP.PESERTA = PESERTA
                    dsRekapSEP.COB = COB
                    dsRekapSEP.JENISRAWAT = JENISRAWAT
                    dsRekapSEP.KELASRAWAT = KELASRAWAT

                    listSEP.Add(dsRekapSEP)

                    Dim rpt As New xtraSEP_New
                    Dim AlamatSimpan As String = AlamatSimpanPDFMerge & "1" & KDREG & "_MERGE.pdf"

                    rpt.BindingSource.DataSource = listSEP
                    rpt.ExportToPdf(AlamatSimpan)

                    If FileIO.FileSystem.FileExists(AlamatSimpan) Then
                        dataList.Add(File.ReadAllBytes(AlamatSimpan))
                    End If

                    If sSplashScreenManager = True Then
                        SplashScreenManager.CloseForm(False)
                    End If
                End If
            End If
            '*************** AKHIR SEP ***************

            '*************** AKHIR RESUME ***************
            If sSplashScreenManager = True Then
                SplashScreenManager.ShowForm(Me, GetType(frmLoadingPanel), True, True, False)
                SplashScreenManager.Default.SetWaitFormCaption("Processing Resume.....")
            End If

            If JENISRAWAT = "RAWAT JALAN" Then
                Dim dsKoding = oKoding.GetDataKodingByRegister(KDREG)
                If dsKoding IsNot Nothing Then
                    Dim rpt As New xtraKoding
                    Dim AlamatSimpan As String = AlamatSimpanPDFMerge & "2" & KDREG & "_MERGE.pdf"

                    rpt.bindingSource.DataSource = dsKoding

                    DownloadIamge1 = AlamatDownloadIamge1 & NOREKAMMEDIS & "/" & NOREKAMMEDIS & ".png"

                    sUserIDTandaTangan = dsKoding.KDDOCTOR

                    rpt.bindingSource.DataSource = dsKoding
                    rpt.ExportToPdf(AlamatSimpan)

                    If FileIO.FileSystem.FileExists(AlamatSimpan) Then
                        dataList.Add(File.ReadAllBytes(AlamatSimpan))
                    End If
                End If
            Else
                Dim dsRingkasan = oRINGKASANKELUAR.GetData(KDREG)
                If dsRingkasan IsNot Nothing Then
                    Dim rpt As New xtraRingkasanKeluarRawatInap
                    Dim AlamatSimpan As String = AlamatSimpanPDFMerge & "2" & KDREG & "_MERGE.pdf"

                    rpt.bindingSource.DataSource = dsRingkasan

                    DownloadIamge1 = AlamatDownloadIamge1 & NOREKAMMEDIS & "/" & NOREKAMMEDIS & ".png"

                    rpt.bindingSource.DataSource = dsRingkasan
                    rpt.ExportToPdf(AlamatSimpan)

                    If FileIO.FileSystem.FileExists(AlamatSimpan) Then
                        dataList.Add(File.ReadAllBytes(AlamatSimpan))
                    End If
                End If
            End If
            If sSplashScreenManager = True Then
                SplashScreenManager.CloseForm(False)
            End If
            '*************** AKHIR RESUME ***************

            '*************** AKHIR LIP EKLAIM ***************
            If PENJAMIN = "BPJS KESEHATAN" Then
                If sSplashScreenManager = True Then
                    SplashScreenManager.ShowForm(Me, GetType(frmLoadingPanel), True, True, False)
                    SplashScreenManager.Default.SetWaitFormCaption("Processing LIP.....")
                End If
                Dim Hasil_claim_print As String = oKoneksi.fn_claim_print(NOMORSEP)

                If Hasil_claim_print <> "" Then
                    Dim jsonDecode = JObject.Parse(Hasil_claim_print)
                    If jsonDecode("metadata")("code").ToString = "200" Then
                        dataList.Add(Convert.FromBase64String(jsonDecode("data").ToString))
                    End If
                End If

                If sSplashScreenManager = True Then
                    SplashScreenManager.CloseForm(False)
                End If
            End If
            '*************** AKHIR LIP EKLAIM ***************

            '*************** MULAI BILLING ***************
            If KDCASHIER <> "" Then
                If sSplashScreenManager = True Then
                    SplashScreenManager.ShowForm(Me, GetType(frmLoadingPanel), True, True, False)
                    SplashScreenManager.Default.SetWaitFormCaption("Processing Billing.....")
                End If
                Dim AlamatSimpanBilling As String = AlamatSimpanPDFMerge & "3" & KDREG & "_MERGE.pdf"

                If JENISRAWAT = "RAWAT JALAN" Then
                    If fn_PrintKwitansiRJ(KDCASHIER, PENJAMIN, AlamatSimpanBilling) = True Then
                        If FileIO.FileSystem.FileExists(AlamatSimpanBilling) Then
                            dataList.Add(File.ReadAllBytes(AlamatSimpanBilling))
                        End If
                    End If
                Else
                    If fn_PrintRincianPerawatanNaikRanap(KDREG, KDREGAWAL, AlamatSimpanBilling) = True Then
                        If FileIO.FileSystem.FileExists(AlamatSimpanBilling) Then
                            dataList.Add(File.ReadAllBytes(AlamatSimpanBilling))
                        End If
                    End If
                End If
                If sSplashScreenManager = True Then
                    SplashScreenManager.CloseForm(False)
                End If
            End If
            '*************** AKHIR BILLING ***************

            '*************** MULAI LABORATORIUM ***************
            If sSplashScreenManager = True Then
                SplashScreenManager.ShowForm(Me, GetType(frmLoadingPanel), True, True, False)
                SplashScreenManager.Default.SetWaitFormCaption("Processing Hasil Laboratorium.....")
            End If
            fn_PrintHasilLab(KDREG, KDREGAWAL)

            If AlamatpdfLaboratorium.Count > 0 Then
                For Each xloop In AlamatpdfLaboratorium
                    If FileIO.FileSystem.FileExists(xloop) Then
                        dataList.Add(File.ReadAllBytes(xloop))
                    End If
                Next
            End If
            If sSplashScreenManager = True Then
                SplashScreenManager.CloseForm(False)
            End If
            '*************** AKHIR LABORATORIUM ***************

            '*************** MULAI RADIOLOGI ***************
            If sSplashScreenManager = True Then
                SplashScreenManager.ShowForm(Me, GetType(frmLoadingPanel), True, True, False)
                SplashScreenManager.Default.SetWaitFormCaption("Processing Hasil Radiologi.....")
            End If

            fn_PrintHasilRadiologi(KDREG, KDREGAWAL, TUJUAN)

            If AlamatpdfRadiologi.Count > 0 Then
                For Each xloop In AlamatpdfRadiologi
                    If FileIO.FileSystem.FileExists(xloop) Then
                        dataList.Add(File.ReadAllBytes(xloop))
                    End If
                Next
            End If
            If sSplashScreenManager = True Then
                SplashScreenManager.CloseForm(False)
            End If
            '*************** AKHIR RADIOLOGI ***************

            '*************** MULAI LOAD PDF ***************
            If sSplashScreenManager = True Then
                SplashScreenManager.ShowForm(Me, GetType(frmLoadingPanel), True, True, False)
                SplashScreenManager.Default.SetWaitFormCaption("Processing Hasil Load PDF.....")
            End If
            fn_LoadDataUpload(KDREG, KDREGAWAL)

            If AlamatpdfUpload.Count > 0 Then
                For Each xloop In AlamatpdfUpload
                    If FileIO.FileSystem.FileExists(xloop) Then
                        dataList.Add(File.ReadAllBytes(xloop))
                    End If
                Next
            End If
            If sSplashScreenManager = True Then
                SplashScreenManager.CloseForm(False)
            End If
            '*************** AKHIR LOAD PDF ***************

            '*************** MULAI ASESMEN AWAL MEDIS IGD ***************
            If TUJUAN = "IGD" Then
                If sSplashScreenManager = True Then
                    SplashScreenManager.ShowForm(Me, GetType(frmLoadingPanel), True, True, False)
                    SplashScreenManager.Default.SetWaitFormCaption("Processing Asesmen Awal Medis IGD.....")
                End If

                Dim AlamatSimpanAsesmenAwalIGD As String = AlamatSimpanPDFMerge & "4" & KDREG & "_IGD.pdf"

                Dim dsigd = oIGD.GetData(KDREG)
                If dsigd IsNot Nothing Then
                    sUSIADIASESMENIGD = CDate(grvDetail.GetFocusedRowCellValue("TanggalLahir")).ToString("dd-MM-yyyy") & " (" & HitungUmur(CDate(grvDetail.GetFocusedRowCellValue("TanggalLahir"))) & ")"
                    sASESEMEN_IGD = dsigd.SURVEY_PERUT_1
                    Dim rpt As New xtraReportFormulirIGD1

                    rpt.ShowPrintMarginsWarning = False
                    rpt.Watermark.Text = sWATERMARK

                    rpt.BindingSource.DataSource = dsigd
                    rpt.ExportToPdf(AlamatSimpanAsesmenAwalIGD)
                    If FileIO.FileSystem.FileExists(AlamatSimpanAsesmenAwalIGD) Then
                        dataList.Add(File.ReadAllBytes(AlamatSimpanAsesmenAwalIGD))
                    End If
                End If

                If sSplashScreenManager = True Then
                    SplashScreenManager.CloseForm(False)
                End If
            End If
            '*************** AKHIR ASESMEN AWAL MEDIS IGDF ***************

            '*************** MULAI GERD Q ***************
            If sSplashScreenManager = True Then
                SplashScreenManager.ShowForm(Me, GetType(frmLoadingPanel), True, True, False)
                SplashScreenManager.Default.SetWaitFormCaption("Processing Ger Q.....")
            End If

            For Each xloop In oIGD_GerdQ.GetDataByListKDPENDAFTARAN(KDREG)
                Dim AlamatSimpanGerdQ As String = AlamatSimpanPDFMerge & xloop.KDGERD & "_IGD.pdf"

                Dim dsGerd = oIGD_GerdQ.GetData(xloop.KDGERD)
                If dsGerd IsNot Nothing Then
                    sWAKTU = dsGerd.DATE.ToString("dd-MM-yyyy")
                    sKDUSER_TTD = dsGerd.KDUSER

                    Dim rpt As New xtraGerd_Q

                    rpt.ShowPrintMarginsWarning = False
                    rpt.Watermark.Text = sWATERMARK

                    rpt.bindingSource.DataSource = dsGerd
                    rpt.ExportToPdf(AlamatSimpanGerdQ)
                    If FileIO.FileSystem.FileExists(AlamatSimpanGerdQ) Then
                        dataList.Add(File.ReadAllBytes(AlamatSimpanGerdQ))
                    End If
                End If
            Next

            If sSplashScreenManager = True Then
                SplashScreenManager.CloseForm(False)
            End If
            '*************** AKHIR GERD Q ***************

            '*************** MULAI LAPORAN OPERASI ***************
            If sSplashScreenManager = True Then
                SplashScreenManager.ShowForm(Me, GetType(frmLoadingPanel), True, True, False)
                SplashScreenManager.Default.SetWaitFormCaption("Processing Laporan Operasi.....")
            End If

            For Each xloop In oLaporanOperasi.GetDataByRegisterList(KDREG)
                Dim waktu As String = xloop.DATE.ToString("ddMMyyyyHHmm")
                Dim AlamatSimpanLaporanOperasi As String = AlamatSimpanPDFMerge & waktu & xloop.KDLAPORANOPERASI & ".pdf"

                Dim ds = oLaporanOperasi.GetDataKode(xloop.KDLAPORANOPERASI)
                If ds IsNot Nothing Then
                    NAMA = NAMAPASIEN
                    JENISKELAMIN = JENISKELAMIN
                    TANGGALLAHIR = TANGGALLAHIR.ToString("dd-MM-yyyy")
                    sKDUSER_TTD = ds.KDUSER
                    USIA = frmRawatInapList.GetUmurPasien(ds.DATE, TANGGALLAHIR)

                    Dim rpt As New xtraReportLAPORANOPERASI

                    rpt.ShowPrintMarginsWarning = False
                    rpt.Watermark.Text = sWATERMARK

                    rpt.BindingSource1.DataSource = ds

                    rpt.ExportToPdf(AlamatSimpanLaporanOperasi)

                    If FileIO.FileSystem.FileExists(AlamatSimpanLaporanOperasi) Then
                        dataList.Add(File.ReadAllBytes(AlamatSimpanLaporanOperasi))
                    End If
                End If
            Next

            If sSplashScreenManager = True Then
                SplashScreenManager.CloseForm(False)
            End If
            '*************** AKHIR LAPORAN OPERASI ***************

            '*************** MULAI LAPORAN TINDAKAN ***************
            If sSplashScreenManager = True Then
                SplashScreenManager.ShowForm(Me, GetType(frmLoadingPanel), True, True, False)
                SplashScreenManager.Default.SetWaitFormCaption("Processing Laporan Tindakan.....")
            End If

            For Each xloop In oLaporanTindakan.GetDataByRegisterList(KDREG)
                Dim waktu As String = xloop.DATE.ToString("ddMMyyyyHHmm")
                Dim AlamatSimpanLaporanTindakan As String = AlamatSimpanPDFMerge & waktu & xloop.KDLAPORANTINDAKAN & ".pdf"

                Dim ds = oLaporanTindakan.GetData(xloop.KDLAPORANTINDAKAN)
                If ds IsNot Nothing Then
                    NAMA = NAMAPASIEN
                    JENISKELAMIN = JENISKELAMIN
                    TANGGALLAHIR = TANGGALLAHIR.ToString("dd-MM-yyyy")
                    sKDUSER_TTD = ds.KDUSER
                    USIA = frmRawatInapList.GetUmurPasien(ds.DATE, TANGGALLAHIR)

                    Dim rpt As New xtraREPORTLAPORANTINDAKAN

                    rpt.ShowPrintMarginsWarning = False
                    rpt.Watermark.Text = sWATERMARK

                    rpt.BindingSource1.DataSource = ds

                    rpt.ExportToPdf(AlamatSimpanLaporanTindakan)

                    If FileIO.FileSystem.FileExists(AlamatSimpanLaporanTindakan) Then
                        dataList.Add(File.ReadAllBytes(AlamatSimpanLaporanTindakan))
                    End If
                End If
            Next

            If fn_SearchFolderPdfCari(TANGGALSEP.ToString("ddMMyyyy") & " - " & NOREKAMMEDIS, False) = True Then
                dataList.Add(File.ReadAllBytes("C:/BJG/TEMPLATE/FOLDERX.pdf"))
            End If

            If sSplashScreenManager = True Then
                SplashScreenManager.CloseForm(False)
            End If

            '*************** AKHIR LAPORAN OPERASI ***************

            '*************** MULAI MERGE ***************
            If sSplashScreenManager = True Then
                Dim Base64Byte() As Byte
                Base64Byte = MergeFilesByte(dataList)
                File.WriteAllBytes(AlamatSimpanPDFMerge & "HASIL.pdf", Base64Byte)
                PdfViewerMerge.LoadDocument(AlamatSimpanPDFMerge & "HASIL.pdf")
            Else
                Dim Base64Byte() As Byte
                Base64Byte = MergeFilesByte(dataList)
                File.WriteAllBytes(txtFolder.Text & "\" & NOMORSEP & ".pdf", Base64Byte)
            End If
            '*************** AKHIR MERGE ***************
        Catch oErr As Exception
            SplashScreenManager.CloseForm(False)
            MsgBox("Load Data Merge" & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Function fn_SearchFolderPdfCari(ByVal sFolder As String, ByVal Pesan As Boolean) As Boolean
        Try
            If txtCariFolder.Text = "" Then
                Exit Function
            End If

            If Not Directory.Exists(txtCariFolder.Text) Then
                Exit Function
            End If

            fn_SearchFolderPdfCari = True

            Dim Folder As New DirectoryInfo(txtCariFolder.Text)
            Dim fi As List(Of FileInfo) = New List(Of FileInfo)
            Dim arrfname As New List(Of String)()

            For Each File In Folder.GetFiles()
                If (File IsNot Nothing) Then
                    If (Path.GetExtension(File.ToString.ToLower) = ".pdf") Then
                        If (File.ToString.ToLower.Contains(sFolder)) Then
                            fi.Add(File)
                            arrfname.Add(File.FullName)
                        End If
                    End If
                End If
            Next

            If fi.Count > 0 Then
                Dim sinputFiles() As String = {}
                For Each ifname In arrfname

                    sinputFiles = AppendArray(sinputFiles, ifname)

                Next

                MergePdfFiles(sinputFiles, "C:/BJG/TEMPLATE/FOLDERX.pdf")

                arrfname.Clear()
            Else
                fn_SearchFolderPdfCari = False
            End If
        Catch oErr As Exception
            fn_SearchFolderPdfCari = False
            If Pesan = True Then
                MsgBox("Search Folder : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            End If
        End Try
    End Function
    Private Function fn_PrintKwitansiRJ(ByVal KDCASHIER As String, ByVal Penjamin As String, ByVal AlamatSimpan As String) As Boolean
        Try
            fn_PrintKwitansiRJ = True

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

            SQL = "SELECT A.NOIDUSER, A.KDCASHIER, A.KDREG, A.NAMAPASIEN, A.POLI, A.TANGGALMASUK, A.KDCUSTOMER, A.PENJAMIN, A.DOKTOR, A.KATEGORI, A.NAMATARIF, A.QTY, A.GRANDTOTAL, A.SUBTOTAL, A.DISKON, A.CHARGE, A.PPN, A.TOTAL, A.PRINTON FROM( "
            SQL &= "(SELECT  "
            SQL &= "KDCASHIER = D.KDCASHIER "
            SQL &= ",D.KDREG "
            SQL &= ",NAMAPASIEN = E.NAME_DISPLAY + ' ' + IIf(E.FRONT_TITLE IS NULL, '', E.FRONT_TITLE + ' ') + E.BACK_TITLE "
            SQL &= ",POLI = ISNULL((SELECT BB.NAME_DISPLAY FROM S_PENDAFTARAN_H AA INNER JOIN M_DEPARTMENT BB ON AA.KDDEPARTMENT = BB.KDDEPARTMENT WHERE AA.KDREG = D.KDREG), '-')  "
            SQL &= ",TANGGALMASUK = (SELECT AA.DATE FROM S_PENDAFTARAN_H AA WHERE AA.KDREG = D.KDREG)  "
            SQL &= ",KDCUSTOMER = D.KDCUSTOMER "
            SQL &= ",DOKTOR = (SELECT IIf(BB.FRONT_TITLE IS NULL, '', BB.FRONT_TITLE + ' ') + BB.NAME_DISPLAY  + ' ' + BB.BACK_TITLE FROM S_PENDAFTARAN_H AS AA INNER JOIN M_DOCTOR AS BB ON AA.KDDOCTOR = BB.KDDOCTOR WHERE AA.KDREG = D.KDREG) "
            SQL &= ",PENJAMIN = F.NAME_DISPLAY "
            SQL &= ",KATEGORI = C.DESCRIPTION "
            SQL &= ",NAMATARIF = B.NAMA_TARIF "
            SQL &= ",QTY = A.QTY  "
            SQL &= ",TOTAL = A.GRANDTOTAL "
            SQL &= ",SUBTOTAL = D.SUBTOTAL "
            SQL &= ",DISKON = D.PERSENDISKON "
            SQL &= ",CHARGE = D.PERSENCHARGE "
            SQL &= ",PPN = D.PERSENPPN "
            SQL &= ",GRANDTOTAL = D.GRANDTOTAL "
            SQL &= ",SEQ = 1 "
            SQL &= ",PRINTON = D.DATE "
            SQL &= ",D.NOIDUSER "

            SQL &= "FROM F_CASHIER_D_TRANSAKSI AS A "
            SQL &= "INNER JOIN M_TARIF_NEW AS B "
            SQL &= "ON A.KDTARIF = B.KDTARIF "
            SQL &= "INNER JOIN M_KATEGORI_BPJS AS C "
            SQL &= "ON B.KDKATEGORI_BPJS = C.KDKATEGORI_BPJS "
            SQL &= "INNER JOIN F_CASHIER_H AS D "
            SQL &= "ON A.KDCASHIER = D.KDCASHIER "
            SQL &= "INNER JOIN M_CUSTOMER AS E "
            SQL &= "ON D.KDCUSTOMER = E.KDCUSTOMER "
            SQL &= "INNER JOIN M_DEBTOR AS F "
            SQL &= "ON D.KDDEBTOR = F.KDDEBTOR "
            SQL &= "WHERE A.KDCASHIER = '" & KDCASHIER & "' "

            SQL &= ") UNION ALL( "

            SQL &= "SELECT  "
            SQL &= "KDCASHIER = C.KDCASHIER "
            SQL &= ",C.KDREG "
            SQL &= ",NAMAPASIEN = E.NAME_DISPLAY + ' ' + IIf(E.FRONT_TITLE IS NULL, '', E.FRONT_TITLE + ' ') + E.BACK_TITLE "
            SQL &= ",POLI = ISNULL((SELECT BB.NAME_DISPLAY FROM S_PENDAFTARAN_H AA INNER JOIN M_DEPARTMENT BB ON AA.KDDEPARTMENT = BB.KDDEPARTMENT WHERE AA.KDREG = C.KDREG), '-')  "
            SQL &= ",TANGGALMASUK = (SELECT AA.DATE FROM S_PENDAFTARAN_H AA WHERE AA.KDREG = C.KDREG)  "
            SQL &= ",KDCUSTOMER = C.KDCUSTOMER "
            SQL &= ",DOKTOR = (SELECT IIf(BB.FRONT_TITLE IS NULL, '', BB.FRONT_TITLE + ' ') + BB.NAME_DISPLAY  + ' ' + BB.BACK_TITLE FROM S_PENDAFTARAN_H AS AA INNER JOIN M_DOCTOR AS BB ON AA.KDDOCTOR = BB.KDDOCTOR WHERE AA.KDREG = C.KDREG) "
            SQL &= ",PENJAMIN = F.NAME_DISPLAY "
            'SQL &= ",KATEGORI = 'OBAT' "
            SQL &= ",KATEGORI = (SELECT CASE B.KDKATEGORI_BPJS WHEN 15 THEN 'ALKES' WHEN 16 THEN 'BMHP' ELSE 'OBAT' END) "
            SQL &= ",NAMATARIF = D.NMITEM2 "
            SQL &= ",QTY = A.QTY  "
            SQL &= ",TOTAL = A.GRANDTOTAL "
            SQL &= ",SUBTOTAL = C.SUBTOTAL "
            SQL &= ",DISKON = C.PERSENDISKON "
            SQL &= ",CHARGE = C.PERSENCHARGE "
            SQL &= ",PPN = C.PERSENPPN "
            SQL &= ",GRANDTOTAL = C.GRANDTOTAL "
            SQL &= ",SEQ = 2 "
            SQL &= ",PRINTON = C.DATE "
            SQL &= ",C.NOIDUSER "

            SQL &= "FROM F_CASHIER_D AS A "
            SQL &= "INNER JOIN M_KATEGORI_BPJS AS B "
            SQL &= "ON A.KDKATEGORI_BPJS = B.KDKATEGORI_BPJS "
            SQL &= "INNER JOIN F_CASHIER_H AS C "
            SQL &= "ON A.KDCASHIER = C.KDCASHIER "
            SQL &= "INNER JOIN M_ITEM AS D "
            SQL &= "ON A.KDITEM = D.KDITEM "
            SQL &= "INNER JOIN M_CUSTOMER AS E "
            SQL &= "ON C.KDCUSTOMER = E.KDCUSTOMER "
            SQL &= "INNER JOIN M_DEBTOR AS F "
            SQL &= "ON C.KDDEBTOR = F.KDDEBTOR "
            SQL &= "WHERE A.KDCASHIER = '" & KDCASHIER & "' AND A.ISPROLANIS = 0 "

            SQL &= ") UNION ALL( "

            SQL &= "SELECT  "
            SQL &= "KDCASHIER = C.KDCASHIER "
            SQL &= ",C.KDREG "
            SQL &= ",NAMAPASIEN = E.NAME_DISPLAY + ' ' + IIf(E.FRONT_TITLE IS NULL, '', E.FRONT_TITLE + ' ') + E.BACK_TITLE "
            SQL &= ",POLI = ISNULL((SELECT BB.NAME_DISPLAY FROM S_PENDAFTARAN_H AA INNER JOIN M_DEPARTMENT BB ON AA.KDDEPARTMENT = BB.KDDEPARTMENT WHERE AA.KDREG = C.KDREG), '-')  "
            SQL &= ",TANGGALMASUK = (SELECT AA.DATE FROM S_PENDAFTARAN_H AA WHERE AA.KDREG = C.KDREG)  "
            SQL &= ",KDCUSTOMER = C.KDCUSTOMER "
            SQL &= ",DOKTOR = (SELECT IIf(BB.FRONT_TITLE IS NULL, '', BB.FRONT_TITLE + ' ') + BB.NAME_DISPLAY  + ' ' + BB.BACK_TITLE FROM S_PENDAFTARAN_H AS AA INNER JOIN M_DOCTOR AS BB ON AA.KDDOCTOR = BB.KDDOCTOR WHERE AA.KDREG = C.KDREG) "
            SQL &= ",PENJAMIN = F.NAME_DISPLAY "
            SQL &= ",KATEGORI = 'OBAT KRONIS' "
            SQL &= ",NAMATARIF = D.NMITEM2 "
            SQL &= ",QTY = A.QTY  "
            SQL &= ",TOTAL = A.GRANDTOTAL "
            SQL &= ",SUBTOTAL = C.SUBTOTAL "
            SQL &= ",DISKON = C.PERSENDISKON "
            SQL &= ",CHARGE = C.PERSENCHARGE "
            SQL &= ",PPN = C.PERSENPPN "
            SQL &= ",GRANDTOTAL = C.GRANDTOTAL "
            SQL &= ",SEQ = 3 "
            SQL &= ",PRINTON = C.DATE "
            SQL &= ",C.NOIDUSER "

            SQL &= "FROM F_CASHIER_D AS A "
            SQL &= "INNER JOIN M_KATEGORI_BPJS AS B "
            SQL &= "ON A.KDKATEGORI_BPJS = B.KDKATEGORI_BPJS "
            SQL &= "INNER JOIN F_CASHIER_H AS C "
            SQL &= "ON A.KDCASHIER = C.KDCASHIER "
            SQL &= "INNER JOIN M_ITEM AS D "
            SQL &= "ON A.KDITEM = D.KDITEM "
            SQL &= "INNER JOIN M_CUSTOMER AS E "
            SQL &= "ON C.KDCUSTOMER = E.KDCUSTOMER "
            SQL &= "INNER JOIN M_DEBTOR AS F "
            SQL &= "ON C.KDDEBTOR = F.KDDEBTOR "
            SQL &= "WHERE A.KDCASHIER = '" & KDCASHIER & "' AND A.ISPROLANIS = 1 "
            SQL &= ") "
            SQL &= ") AS A "
            SQL &= "ORDER BY A.SEQ ASC "

            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "F_CASHIER_D")

            Dim listPendaftaranH As New List(Of DataAccess.R_RINCIAN_KWITANSIRJ)

            sTotalProlanis = 0
            sTotalPaket = 0
            sGrandtotalRS = 0

            Dim sDiscount As Decimal = 0
            Dim sPPN As Decimal = 0
            Dim sCHARGE As Decimal = 0
            Dim sTotal As Decimal = 0

            For iLoop As Integer = 0 To ds.Tables("F_CASHIER_D").Rows.Count - 1
                Dim dsCashierH As New DataAccess.R_RINCIAN_KWITANSIRJ
                With ds.Tables("F_CASHIER_D")
                    dsCashierH.KDCASHIER = .Rows(iLoop)("KDCASHIER").ToString
                    dsCashierH.KDREG = .Rows(iLoop)("KDREG").ToString
                    dsCashierH.KDCUSTOMER = .Rows(iLoop)("KDCUSTOMER").ToString
                    dsCashierH.NAMAPASIEN = .Rows(iLoop)("NAMAPASIEN").ToString
                    dsCashierH.POLI = .Rows(iLoop)("POLI").ToString
                    dsCashierH.TANGGALMASUK = .Rows(iLoop)("TANGGALMASUK").ToString
                    dsCashierH.PENJAMIN = .Rows(iLoop)("PENJAMIN").ToString
                    dsCashierH.DOKTOR = .Rows(iLoop)("DOKTOR").ToString
                    dsCashierH.KATEGORI = .Rows(iLoop)("KATEGORI").ToString
                    dsCashierH.NAMATARIF = .Rows(iLoop)("NAMATARIF").ToString
                    dsCashierH.QTY = .Rows(iLoop)("QTY").ToString
                    dsCashierH.TOTAL = .Rows(iLoop)("TOTAL").ToString
                    dsCashierH.SUBTOTAL = .Rows(iLoop)("SUBTOTAL").ToString
                    dsCashierH.DISKON = .Rows(iLoop)("DISKON").ToString
                    dsCashierH.CHARGE = .Rows(iLoop)("CHARGE").ToString
                    dsCashierH.PPN = .Rows(iLoop)("PPN").ToString
                    dsCashierH.[GRANDTOTAL] = .Rows(iLoop)("GRANDTOTAL").ToString
                    dsCashierH.PRINTON = .Rows(iLoop)("PRINTON").ToString

                    If .Rows(iLoop)("KATEGORI").ToString = "OBAT KRONIS" Then
                        sTotalProlanis += .Rows(iLoop)("TOTAL").ToString
                    End If

                    sTotal += .Rows(iLoop)("TOTAL").ToString
                    sDiscount = .Rows(iLoop)("DISKON").ToString
                    sPPN = .Rows(iLoop)("PPN").ToString
                    sCHARGE = .Rows(iLoop)("CHARGE").ToString
                    listPendaftaranH.Add(dsCashierH)

                    sUSERCASHIER = .Rows(iLoop)("NOIDUSER").ToString
                End With
            Next

            sGrandtotalRS = sTotal - (sTotal * (sDiscount / 100)) + (sTotal - (sTotal * (sDiscount / 100))) * (sPPN / 100) + (((sTotal - (sTotal * (sDiscount / 100))) + (sTotal - (sTotal * (sDiscount / 100))) * (sPPN / 100)) * (sCHARGE / 100))

            sTotalPaket = sGrandtotalRS - sTotalProlanis

            If Penjamin = "BPJS KESEHATAN" Or Penjamin = "BPJS KETENAGAKERJAAN" Then
                Dim rpt As New xtraRincianRawatJalanBPJS
                rpt.BindingSource.DataSource = listPendaftaranH
                rpt.ExportToPdf(AlamatSimpan)
            Else
                Dim rpt As New xtraRincianRawatJalan
                rpt.BindingSource.DataSource = listPendaftaranH
                rpt.ExportToPdf(AlamatSimpan)
            End If
            If oConn.State = ConnectionState.Open Then
                oConn.Close()
            End If
        Catch oErr As Exception
            fn_PrintKwitansiRJ = False
        End Try
    End Function
    Private Function fn_PrintRincianPerawatanNaikRanap(ByVal sKDREG As String, ByVal sKDREGAWAL As String, ByVal AlamatSimpan As String) As Boolean
        Try
            fn_PrintRincianPerawatanNaikRanap = True

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

            SQL = "SELECT * FROM( "
            SQL &= "(SELECT  "
            SQL &= "A.KDREG "
            SQL &= ",NAMAPASIEN = D.NAME_DISPLAY + ' ' + IIf(D.FRONT_TITLE IS NULL, '', D.FRONT_TITLE + ' ') + D.BACK_TITLE "
            SQL &= ",NAMAPJ = C.NAMA_WALI "
            SQL &= ",PENJAMIN = E.NAME_DISPLAY "
            SQL &= ",DOKTOR = IIf(F.FRONT_TITLE IS NULL, '', F.FRONT_TITLE + ' ') + F.NAME_DISPLAY  + ' ' + F.BACK_TITLE "
            SQL &= ",DIAGNOSA = G.DESCRIPTION "
            SQL &= ",TANGGALMASUK = CONVERT(VARCHAR(20), C.DATE, 113) "
            SQL &= ",TANGGALKELUAR = ISNULL((SELECT CONVERT(VARCHAR(20), AA.DATEPULANG, 113) FROM S_PENDAFTARAN_H AS AA WHERE AA.KDREG = A.KDREG AND ISUPDATEPULANG = 1), '-') "
            SQL &= ",KATEGORI = H.DESCRIPTION "
            SQL &= ",TANGGAL = B.TANGGAL "
            SQL &= ",NAMATARIF = I.NMITEM2 "
            SQL &= ",QTY = B.QTY "
            SQL &= ",HARGA = B.GRANDTOTAL / B.QTY "
            SQL &= ",SUBTOTAL = B.SUBTOTAL "
            SQL &= ",GRANDTOTAL = B.GRANDTOTAL "
            SQL &= ",KDCUSTOMER = A.KDCUSTOMER "
            SQL &= ",DEPOSIT = C.UANGMUKA "
            SQL &= ",KDCASHIER = A.KDCASHIER "
            SQL &= ",KDREGAWAL = ISNULL(C.KDREGAWAL, '-') "
            SQL &= ",POLIAWAL = ISNULL((SELECT BB.NAME_DISPLAY FROM M_DEPARTMENT BB WHERE BB.KDDEPARTMENT = (SELECT AA.KDDEPARTMENT FROM S_PENDAFTARAN_H AA WHERE AA.KDREG = C.KDREGAWAL)), '-') "
            SQL &= ",ISRAJAL = B.ISRAJAL "
            SQL &= ",KDITEM = B.KDITEM "
            SQL &= ",GRANDTOTALALL = A.GRANDTOTAL "

            SQL &= "FROM F_CASHIER_H AS A "
            SQL &= "INNER JOIN F_CASHIER_D AS B "
            SQL &= "ON A.KDCASHIER = B.KDCASHIER "
            SQL &= "INNER JOIN S_PENDAFTARAN_H AS C "
            SQL &= "ON A.KDREG = C.KDREG "
            SQL &= "INNER JOIN M_CUSTOMER AS D "
            SQL &= "ON A.KDCUSTOMER = D.KDCUSTOMER "
            SQL &= "INNER JOIN M_DEBTOR AS E "
            SQL &= "ON A.KDDEBTOR = E.KDDEBTOR "
            SQL &= "INNER JOIN M_DOCTOR AS F "
            SQL &= "ON C.KDDOCTOR = F.KDDOCTOR "
            SQL &= "INNER JOIN M_DIAGNOSAICD10 AS G "
            SQL &= "ON C.KDDIAGNOSA = G.KDDIAGNOSA "
            SQL &= "INNER JOIN M_KATEGORI_BPJS AS H "
            SQL &= "ON B.KDKATEGORI_BPJS = H.KDKATEGORI_BPJS "
            SQL &= "INNER JOIN M_ITEM AS I "
            SQL &= "ON B.KDITEM = I.KDITEM "

            SQL &= "WHERE A.KDREG = '" & sKDREG & "' "

            SQL &= ") UNION ALL( "

            SQL &= "SELECT  "
            SQL &= "A.KDREG "
            SQL &= ",NAMAPASIEN = D.NAME_DISPLAY + ' ' + IIf(D.FRONT_TITLE IS NULL, '', D.FRONT_TITLE + ' ') + D.BACK_TITLE "
            SQL &= ",NAMAPJ = C.NAMA_WALI "
            SQL &= ",PENJAMIN = E.NAME_DISPLAY "
            SQL &= ",DOKTOR = IIf(F.FRONT_TITLE IS NULL, '', F.FRONT_TITLE + ' ') + F.NAME_DISPLAY  + ' ' + F.BACK_TITLE "
            SQL &= ",DIAGNOSA = G.DESCRIPTION "
            SQL &= ",TANGGALMASUK = CONVERT(VARCHAR(20), C.DATE, 113) "
            SQL &= ",TANGGALKELUAR = ISNULL((SELECT CONVERT(VARCHAR(20), AA.DATEPULANG, 113) FROM S_PENDAFTARAN_H AS AA WHERE AA.KDREG = A.KDREG AND ISUPDATEPULANG = 1), '-') "
            SQL &= ",KATEGORI = H.DESCRIPTION "
            SQL &= ",TANGGAL = B.TANGGAL "
            SQL &= ",NAMATARIF = I.NAMA_TARIF + ' ; ' + B.DITINDAK "
            SQL &= ",QTY = B.QTY "
            SQL &= ",HARGA = B.GRANDTOTAL / B.QTY "
            SQL &= ",SUBTOTAL = B.SUBTOTAL "
            SQL &= ",GRANDTOTAL = B.GRANDTOTAL "
            SQL &= ",KDCUSTOMER = A.KDCUSTOMER "
            SQL &= ",DEPOSIT = C.UANGMUKA "
            SQL &= ",KDCASHIER = A.KDCASHIER "
            SQL &= ",KDREGAWAL = ISNULL(C.KDREGAWAL, '-') "
            SQL &= ",POLIAWAL = ISNULL((SELECT BB.NAME_DISPLAY FROM M_DEPARTMENT BB WHERE BB.KDDEPARTMENT = (SELECT AA.KDDEPARTMENT FROM S_PENDAFTARAN_H AA WHERE AA.KDREG = C.KDREGAWAL)), '-') "
            SQL &= ",ISRAJAL = B.ISRAJAL "
            SQL &= ",KDITEM = B.KDTARIF "
            SQL &= ",GRANDTOTALALL = A.GRANDTOTAL "

            SQL &= "FROM F_CASHIER_H AS A "
            SQL &= "INNER JOIN F_CASHIER_D_TRANSAKSI AS B "
            SQL &= "ON A.KDCASHIER = B.KDCASHIER "
            SQL &= "INNER JOIN S_PENDAFTARAN_H AS C "
            SQL &= "ON A.KDREG = C.KDREG "
            SQL &= "INNER JOIN M_CUSTOMER AS D "
            SQL &= "ON A.KDCUSTOMER = D.KDCUSTOMER "
            SQL &= "INNER JOIN M_DEBTOR AS E "
            SQL &= "ON A.KDDEBTOR = E.KDDEBTOR "
            SQL &= "INNER JOIN M_DOCTOR AS F "
            SQL &= "ON C.KDDOCTOR = F.KDDOCTOR "
            SQL &= "INNER JOIN M_DIAGNOSAICD10 AS G "
            SQL &= "ON C.KDDIAGNOSA = G.KDDIAGNOSA "
            SQL &= "INNER JOIN M_TARIF_NEW AS I "
            SQL &= "ON B.KDTARIF = I.KDTARIF "
            SQL &= "INNER JOIN M_KATEGORI_BPJS AS H "
            SQL &= "ON I.KDKATEGORI_BPJS = H.KDKATEGORI_BPJS "

            SQL &= "WHERE A.KDREG = '" & sKDREG & "' "

            SQL &= ") "
            SQL &= ") AS A "
            SQL &= "ORDER BY A.KATEGORI DESC "

            ', A.KATEGORI ASC

            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "F_CASHIER_D")

            Dim sSisaTagihan As Decimal = 0
            For iLoop As Integer = 0 To ds.Tables("F_CASHIER_D").Rows.Count - 1
                With ds.Tables("F_CASHIER_D")
                    sSisaTagihan += CDec(.Rows(iLoop)("GRANDTOTAL"))
                End With
            Next


            Dim sSISA As Decimal = 0
            Dim listPendaftaran As New List(Of DataAccess.R_RINCIAN_NAIKRANAP)
            For iLoop As Integer = 0 To ds.Tables("F_CASHIER_D").Rows.Count - 1
                Dim dsCashier As New DataAccess.R_RINCIAN_NAIKRANAP
                With ds.Tables("F_CASHIER_D")
                    Dim sSubTotal As Decimal = 0
                    sSubTotal = CDec(.Rows(iLoop)("GRANDTOTAL"))
                    sSISA += sSubTotal
                    dsCashier.KDREG = .Rows(iLoop)("KDREG").ToString
                    dsCashier.NAMAPASIEN = .Rows(iLoop)("NAMAPASIEN").ToString
                    dsCashier.NAMAPJ = .Rows(iLoop)("NAMAPJ").ToString
                    dsCashier.PENJAMIN = .Rows(iLoop)("PENJAMIN").ToString
                    dsCashier.DOKTOR = .Rows(iLoop)("DOKTOR").ToString
                    dsCashier.DIAGNOSA = .Rows(iLoop)("DIAGNOSA").ToString
                    dsCashier.TANGGALMASUK = .Rows(iLoop)("TANGGALMASUK").ToString
                    dsCashier.TANGGALKELUAR = .Rows(iLoop)("TANGGALKELUAR").ToString
                    dsCashier.KATEGORI = .Rows(iLoop)("KATEGORI").ToString
                    dsCashier.TANGGAL = .Rows(iLoop)("TANGGAL").ToString
                    dsCashier.NAMATARIF = .Rows(iLoop)("NAMATARIF").ToString
                    dsCashier.QTY = .Rows(iLoop)("QTY").ToString
                    dsCashier.PRICE = .Rows(iLoop)("HARGA").ToString
                    dsCashier.SUBTOTAL = .Rows(iLoop)("GRANDTOTAL").ToString
                    dsCashier.GRANDTOTAL = sSISA
                    dsCashier.KDCUSTOMER = .Rows(iLoop)("KDCUSTOMER").ToString
                    dsCashier.DEPOSIT = .Rows(iLoop)("DEPOSIT").ToString
                    dsCashier.KDCASHIER = ""
                    dsCashier.KDREGAWAL = .Rows(iLoop)("KDREGAWAL").ToString
                    dsCashier.POLIAWAL = .Rows(iLoop)("POLIAWAL").ToString
                    dsCashier.ISRAJAL = .Rows(iLoop)("ISRAJAL").ToString
                    dsCashier.KDITEM = .Rows(iLoop)("KDITEM").ToString
                    dsCashier.GRANDTOTALALL = sSisaTagihan

                    listPendaftaran.Add(dsCashier)
                End With
            Next

            Dim listPendaftaranH As New List(Of DataAccess.R_RINCIAN_NAIKRANAP_H)

            Dim dsCashierH As New DataAccess.R_RINCIAN_NAIKRANAP_H
            With listPendaftaran.FirstOrDefault
                dsCashierH.KDCASHIER = .KDCASHIER
                dsCashierH.KDREG = .KDREG
                dsCashierH.NAMAPASIEN = .NAMAPASIEN
                dsCashierH.NAMAPJ = .NAMAPJ
                dsCashierH.PENJAMIN = .PENJAMIN
                dsCashierH.DOKTOR = .DOKTOR
                dsCashierH.DIAGNOSA = .DIAGNOSA
                dsCashierH.KDCUSTOMER = .KDCUSTOMER
                dsCashierH.TANGGALMASUK = .TANGGALMASUK
                dsCashierH.TANGGALKELUAR = .TANGGALKELUAR
                dsCashierH.DEPOSIT = .DEPOSIT
                dsCashierH.KDREGAWAL = .KDREGAWAL
                dsCashierH.POLIAWAL = .POLIAWAL
                dsCashierH.GRANDTOTALALL = .GRANDTOTALALL

                listPendaftaranH.Add(dsCashierH)
            End With


            Dim listPendaftaranD As New List(Of DataAccess.R_RINCIAN_NAIKRANAP_D)
            For Each iLoop In listPendaftaran.Where(Function(x) x.ISRAJAL = True)
                Dim dsCashierD As New DataAccess.R_RINCIAN_NAIKRANAP_D
                With iLoop
                    dsCashierD.KDCASHIER = .KDCASHIER
                    dsCashierD.KATEGORI = .KATEGORI
                    dsCashierD.TANGGAL = .TANGGAL
                    dsCashierD.KDITEM = .KDITEM
                    dsCashierD.NAMATARIF = .NAMATARIF
                    dsCashierD.QTY = .QTY
                    dsCashierD.PRICE = .PRICE
                    dsCashierD.SUBTOTAL = .SUBTOTAL
                    dsCashierD.GRANDTOTAL = .GRANDTOTAL
                    dsCashierD.KDREGAWAL = .KDREGAWAL
                    dsCashierD.POLIAWAL = .POLIAWAL

                    listPendaftaranD.Add(dsCashierD)
                End With
            Next

            Dim listPendaftaranD_RI As New List(Of DataAccess.R_RINCIAN_NAIKRANAP_D_RI)
            For Each iLoop In listPendaftaran.Where(Function(x) x.ISRAJAL = False)
                Dim dsCashierD_RI As New DataAccess.R_RINCIAN_NAIKRANAP_D_RI
                With iLoop
                    dsCashierD_RI.KDCASHIER = .KDCASHIER
                    dsCashierD_RI.KATEGORI = .KATEGORI
                    dsCashierD_RI.TANGGAL = .TANGGAL
                    dsCashierD_RI.KDITEM = .KDITEM
                    dsCashierD_RI.NAMATARIF = .NAMATARIF
                    dsCashierD_RI.QTY = .QTY
                    dsCashierD_RI.PRICE = .PRICE
                    dsCashierD_RI.SUBTOTAL = .SUBTOTAL
                    dsCashierD_RI.GRANDTOTAL = .GRANDTOTAL

                    listPendaftaranD_RI.Add(dsCashierD_RI)
                End With
            Next

            Dim rpt As New xtraRincianPerawatanNaikRanap

            rpt.bindingSource.DataSource = listPendaftaranH

            Dim rptsub2 As New xtraRincianPerawatanNaikRanap_D

            rptsub2.bindingSource.DataSource = listPendaftaranD

            rpt.XrSubreport2.ReportSource = rptsub2

            Dim rptsub As New xtraRincianPerawatanNaikRanap_D_RI

            rptsub.bindingSource.DataSource = listPendaftaranD_RI

            rpt.XrSubreport1.ReportSource = rptsub

            Dim printTool As New DevExpress.XtraReports.UI.ReportPrintTool(rpt)

            rpt.ExportToPdf(AlamatSimpan)

            If oConn.State = ConnectionState.Open Then
                oConn.Close()
            End If
        Catch oErr As Exception
            fn_PrintRincianPerawatanNaikRanap = False

            MsgBox("Print Data : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Function
    Private Sub fn_PrintHasilLab(ByVal KDREG As String, ByVal KDREGAWAL As String)
        Try
            AlamatpdfLaboratorium.Clear()

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
            SQL &= "A.KDTRANSAKSILAB "
            SQL &= ",B.KDCUSTOMER "
            SQL &= ",NAME_DISPLAY = C.NAME_DISPLAY + ' ' + IIf(C.FRONT_TITLE IS NULL, '', C.FRONT_TITLE + ' ') + C.BACK_TITLE "
            SQL &= "FROM "
            SQL &= "S_TRANSAKSILAB_H A "
            SQL &= "INNER JOIN S_PENDAFTARAN_H B "
            SQL &= "ON A.KDREG = B.KDREG "
            SQL &= "INNER JOIN M_CUSTOMER C "
            SQL &= "ON B.KDCUSTOMER = C.KDCUSTOMER "
            SQL &= "WHERE A.KDREG = '" & KDREG & "' "
            If KDREGAWAL <> "" Then
                SQL &= "OR A.KDREG = '" & KDREGAWAL & "' "
            End If

            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "S_LIS_D_NEW")

            If oConn.State = ConnectionState.Open Then
                oConn.Close()
            End If

            For iLoop As Integer = 0 To ds.Tables("S_LIS_D_NEW").Rows.Count - 1
                With ds.Tables("S_LIS_D_NEW")
                    Dim sPDF As String = "\\192.168.2.79\Users\SIMRS\δupload berkas rmδ\" & .Rows(iLoop)("KDTRANSAKSILAB").ToString & "_" & CInt(.Rows(iLoop)("KDCUSTOMER").ToString) & "_" & .Rows(iLoop)("NAME_DISPLAY").ToString.Trim.Replace("'", "") & ".pdf"
                    If FileIO.FileSystem.FileExists(sPDF) Then
                        AlamatpdfLaboratorium.Add(sPDF)
                    Else
                        Dim sPDF2 As String = "\\192.168.2.79\Users\SIMRS\δupload berkas rmδ\" & .Rows(iLoop)("KDTRANSAKSILAB").ToString & ".pdf"
                        If FileIO.FileSystem.FileExists(sPDF) Then
                            AlamatpdfLaboratorium.Add(sPDF2)
                        End If
                    End If
                End With
            Next
        Catch oErr As Exception
            MsgBox("Print Data Laboratorium: " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_PrintHasilRadiologi(ByVal KDREG As String, ByVal KDREGAWAL As String, ByVal TUJUAN As String)
        Try
            AlamatpdfRadiologi.Clear()

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
            SQL &= "Tanggal = D.DATE "
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
            SQL &= "WHERE C.KDREG = '" & KDREG & "' "
            If KDREGAWAL <> "" Then
                SQL &= "OR C.KDREG = '" & KDREGAWAL & "' "
            End If

            SQL &= "UNION "

            SQL &= "SELECT "
            SQL &= "Tanggal = D.DATE "
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
            SQL &= "WHERE C.KDREG = '" & KDREG & "' "
            If KDREGAWAL <> "" Then
                SQL &= "OR C.KDREG = '" & KDREGAWAL & "' "
            End If

            SQL &= "UNION "

            SQL &= "SELECT "
            SQL &= "Tanggal = D.DATE "
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
            SQL &= "WHERE C.KDREG = '" & KDREG & "' "
            If KDREGAWAL <> "" Then
                SQL &= "OR C.KDREG = '" & KDREGAWAL & "' "
            End If

            SQL &= ") X "
            SQL &= "ORDER BY X.Tanggal DESC "

            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "S_LIS_D_NEW")

            If oConn.State = ConnectionState.Open Then
                oConn.Close()
            End If

            For iLoop As Integer = 0 To ds.Tables("S_LIS_D_NEW").Rows.Count - 1
                With ds.Tables("S_LIS_D_NEW")
                    Dim dsRadiologi = oBrigging.GetDataRadiologi(.Rows(iLoop)("NoRadiologi").ToString, sAlamatBriggingRadiologi)

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

                    If dsRadiologi <> "" Then
                        '000398907

                        Dim allData = JObject.Parse(dsRadiologi)
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
                            RichTextBox1.Rtf = RenderHTML(allData("response")("Expertise").ToString())
                            sHasilLoadRadiologi = RichTextBox1.Rtf

                            If .Rows(iLoop)("Catatan").ToString.Contains("USG") = True Then
                                Try
                                    Dim rpt As New xtraUSG

                                    sNOMORUSG_RAD = allData("response")("OrderNumber").ToString()
                                    sUMUR_RAD = allData("response")("Age").ToString()
                                    sKDCUSTOMER_RAD = allData("response")("MedrekNumber").ToString()
                                    sNAME_DISPLAY_RAD = allData("response")("PatientName").ToString()
                                    sDATE_RAD = allData("response")("StudyDate").ToString()
                                    sREPORTDATE_RAD = allData("response")("StudyDate").ToString()
                                    sEXAMPDESC_RAD = ""
                                    sDOKTER_RAD = allData("response")("Radiologist").ToString()
                                    sDIAGNOSA_RAD = allData("response")("Diagnose").ToString()
                                    sDESCRIPTION_RAD = RichTextBox1.Text
                                    sDOKTER_RAD2 = allData("response")("Radiologist").ToString()

                                    Try
                                        sPEMERIKSAAN_RAD = allData("response")("BodyPart").ToString()
                                    Catch ex As Exception

                                    End Try

                                    Dim sPDF As String = "C:\Mergedata\" & sNOMORUSG_RAD & ".pdf"
                                    rpt.ExportToPdf(sPDF)
                                    If FileIO.FileSystem.FileExists(sPDF) Then
                                        AlamatpdfRadiologi.Add(sPDF)
                                    End If

                                Catch oErr As Exception
                                    MsgBox("Print Data USG: " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
                                End Try
                            Else
                                Try
                                    Dim rpt As New xtraRadiologi

                                    sNOMORUSG_RAD = allData("response")("OrderNumber").ToString()
                                    sKDCUSTOMER_RAD = allData("response")("MedrekNumber").ToString()
                                    sNAME_DISPLAY_RAD = allData("response")("PatientName").ToString()
                                    sDATE_RAD = allData("response")("StudyDate").ToString()
                                    sDEPARTMENT_RAD = TUJUAN
                                    sREPORTDATE_RAD = allData("response")("StudyDate").ToString()
                                    sEXAMPDESC_RAD = ""
                                    sDOKTER_RAD = allData("response")("ReferringDoctor").ToString()
                                    sDIAGNOSA_RAD = allData("response")("Diagnose").ToString()
                                    sDESCRIPTION_RAD = RichTextBox1.Text
                                    sDOKTER_RAD2 = allData("response")("Radiologist").ToString()

                                    Try
                                        sPEMERIKSAAN_RAD = allData("response")("BodyPart").ToString()
                                    Catch ex As Exception

                                    End Try

                                    Dim sPDF As String = "C:\Mergedata\" & sNOMORUSG_RAD & ".pdf"
                                    rpt.ExportToPdf(sPDF)
                                    If FileIO.FileSystem.FileExists(sPDF) Then
                                        AlamatpdfRadiologi.Add(sPDF)
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
                End With
            Next
        Catch oErr As Exception
            MsgBox("Print Data Radiologi: " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadDataUpload(ByVal KDREG As String, ByVal KDREGAWAL As String)
        Try
            AlamatpdfUpload.Clear()

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
            SQL &= "B.SEQ "
            SQL &= ",B.ALAMATAKHIR_PDF "
            SQL &= "FROM "
            SQL &= "S_PDF_H A "
            SQL &= "INNER JOIN S_PDF_D B "
            SQL &= "ON A.KDPDFTRANSAKSI = B.KDPDFTRANSAKSI "
            SQL &= "WHERE A.KDREG = '" & KDREG & "' "
            If KDREGAWAL <> "" Then
                SQL &= "OR A.KDREG = '" & KDREGAWAL & "' "
            End If

            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "S_PDF_H")

            For iLoop As Integer = 0 To ds.Tables("S_PDF_H").Rows.Count - 1
                With ds.Tables("S_PDF_H")
                    Dim sPDF As String = .Rows(iLoop)("ALAMATAKHIR_PDF").ToString()
                    If FileIO.FileSystem.FileExists(sPDF) Then
                        AlamatpdfUpload.Add(sPDF)
                    End If
                End With
            Next

            If oConn.State = ConnectionState.Open Then
                oConn.Close()
            End If

        Catch oErr As Exception
            MsgBox("Load S_PDF_H Data : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
#End Region
#Region "Koding Rekam Medis"
    Private Sub fn_LoadDetailKoding(ByVal KDREG As String)
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

            grvKoding.OptionsSelection.MultiSelect = True
            grvKoding.SelectAll()
            grvKoding.DeleteSelectedRows()
            grvKoding.OptionsSelection.MultiSelect = False

            SQL = "SELECT A.* "
            SQL &= "FROM ( "
            SQL &= "(SELECT "
            SQL &= "NoKoding = A.KDKODING "
            SQL &= ",Tgl = A.DATE "
            SQL &= ",Kode = B.KDDIAGNOSA "
            SQL &= ",Diagnosa = C.DESCRIPTION "
            SQL &= ",Ket = A.DESCRIPTION "
            SQL &= ",[User] = A.NOIDUSER "
            SQL &= ",Seq = B.SEQ "

            SQL &= "FROM S_KODING_H A "
            SQL &= "INNER JOIN S_KODING_DX B "
            SQL &= "ON A.KDKODING = B.KDKODING "
            SQL &= "INNER JOIN M_DIAGNOSAICD10 C "
            SQL &= "ON B.KDDIAGNOSA = C.KDDIAGNOSA "

            SQL &= "WHERE A.KDREG = '" & KDREG & "' "
            SQL &= ") "
            SQL &= ") AS A "
            SQL &= "ORDER BY A.Seq ASC "

            oComm.Connection = oConn

            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "S_PENDAFTARAN_KODING_DETIL")

            grdKoding.MainView = grvKoding
            grdKoding.DataSource = ds.Tables("S_PENDAFTARAN_KODING_DETIL")
            grdKoding.ForceInitialize()

            If oConn.State = ConnectionState.Open Then
                oConn.Close()
            End If

        Catch ex As Exception
            MsgBox("Load Detail Koding: " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
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
#Region "Command Button"
    Private Sub grvDetail_RowStyle(sender As System.Object, e As DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs) Handles grvDetail.RowStyle
        If CBool(grvDetail.GetRowCellValue(e.RowHandle, "Cek")) = True Then
            e.Appearance.BackColor = Color.Yellow
        End If
    End Sub
    Private Sub frmMember_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        'Select Case e.KeyCode
        '    Case Keys.R
        '        If e.Alt = True And picRefresh.Enabled = True Then
        '            picRefresh_Click()
        '        End If
        'End Select
    End Sub
    Private Sub grvDetail_FocusedRowChanged(ByVal sender As System.Object, ByVal e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs) Handles grvDetail.FocusedRowChanged
        If grvDetail.GetFocusedRowCellValue("NoRekamMedis") Is Nothing Then
            fn_ClosePDF()
            Exit Sub
        End If

        sTanggalPulangDiFo = grvDetail.GetFocusedRowCellValue("TanggalPulang")
        'sUmurPasienDiCPPT = DateDiff(DateInterval.Year, CDate(grvDetail.GetFocusedRowCellValue("TanggalLahir")), Now) & " Tahun"
        sUmurPasienDiCPPT = HitungUmur(CDate(grvDetail.GetFocusedRowCellValue("TanggalLahir")))
        XtraTabControl1_SelectedPageChanged()
    End Sub
    Function HitungUmur(ByVal tanggllahir As Date) As String
        Dim y, m, d As Integer
        d = Now.Day - tanggllahir.Day
        m = Now.Month - tanggllahir.Month
        y = Now.Year - tanggllahir.Year
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
    Private Sub XtraTabControl1_SelectedPageChanged() Handles tabbedControlGroup1.SelectedPageChanged
        fn_ClosePDF()
        If grvDetail.GetFocusedRowCellValue("NoRekamMedis") Is Nothing Then
            Exit Sub
        End If

        If tabbedControlGroup1.SelectedTabPageIndex = 0 Then
            fn_LoadPdfViewerCPPTRANANP()
        ElseIf tabbedControlGroup1.SelectedTabPageIndex = 1 Then
            fn_LoadPdfViewerCPPTRAJAL()
        ElseIf tabbedControlGroup1.SelectedTabPageIndex = 2 Then
            fn_LoadResumeRawatJlalanRawatInap(grvDetail.GetFocusedRowCellValue("NoRekamMedis"))
        ElseIf tabbedControlGroup1.SelectedTabPageIndex = 3 Then
            fn_LoadDataLaboratorium(grvDetail.GetFocusedRowCellValue("NoRekamMedis"))
        ElseIf tabbedControlGroup1.SelectedTabPageIndex = 4 Then
            fn_LoadDataRadiologi()
        ElseIf tabbedControlGroup1.SelectedTabPageIndex = 5 Then
            fn_DokterBersama()
        ElseIf tabbedControlGroup1.SelectedTabPageIndex = 6 Then
            fn_TransferInternal()
        ElseIf tabbedControlGroup1.SelectedTabPageIndex = 7 Then
            fn_PrintStrukAsesemenMedisIGD(grvDetail.GetFocusedRowCellValue("NoRekamMedis"))
        ElseIf tabbedControlGroup1.SelectedTabPageIndex = 8 Then
            fn_PrintStrukAsesemenPerawatIGD(grvDetail.GetFocusedRowCellValue("NoRekamMedis"))
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
            fn_LoadDataMerge(True, grvDetail.GetFocusedRowCellValue("Invoice"), grvDetail.GetFocusedRowCellValue("Penjamin"), grvDetail.GetFocusedRowCellValue("NoRegister"), grvDetail.GetFocusedRowCellValue("NoRegister_Awal"), grvDetail.GetFocusedRowCellValue("NoRekamMedis"), grvDetail.GetFocusedRowCellValue("NoSEP"), grvDetail.GetFocusedRowCellValue("TanggalDaftar"), grvDetail.GetFocusedRowCellValue("NoKartu"), grvDetail.GetFocusedRowCellValue("Pasien"), grvDetail.GetFocusedRowCellValue("TanggalLahir"), grvDetail.GetFocusedRowCellValue("JenisKelamin"), grvDetail.GetFocusedRowCellValue("NoTelepon"), grvDetail.GetFocusedRowCellValue("Faskes"), grvDetail.GetFocusedRowCellValue("Faskes_Label"), grvDetail.GetFocusedRowCellValue("Tujuan"), grvDetail.GetFocusedRowCellValue("DiagnosaAwal"), grvDetail.GetFocusedRowCellValue("Catatan"), grvDetail.GetFocusedRowCellValue("Peserta"), grvDetail.GetFocusedRowCellValue("Cob"), grvDetail.GetFocusedRowCellValue("JenisRawat"), grvDetail.GetFocusedRowCellValue("KelasRawat"))
        ElseIf tabbedControlGroup1.SelectedTabPageIndex = 15 Then
            fn_LoadDetailKoding(grvDetail.GetFocusedRowCellValue("NoRegister"))
        ElseIf tabbedControlGroup1.SelectedTabPageIndex = 16 Then
            fn_LoadPdfViewerLaporanOperasi()
        End If
    End Sub
    Private Sub btnRefresh_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click
        fn_LoadSecurity()
    End Sub
    Private Sub btnKonfirmasi_Click(sender As Object, e As EventArgs) Handles btnKonfirmasi.Click
        If Not Directory.Exists(txtFolder.Text) Then
            MsgBox("Simpan Folder Masih Kosong", MsgBoxStyle.Information, Me.Text)
            Exit Sub
        End If

        Dim sTotal As Integer = 0
        Dim sProcess As Integer = 0

        For i As Integer = 0 To grvDetail.RowCount - 1
            sTotal += 1
        Next i

        If MsgBox("Apa anda yakin akan mengimport " & sTotal & " Baris data ?", MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, Me.Text) = MsgBoxResult.No Then Exit Sub

        Try
            SplashScreenManager.ShowForm(Me, GetType(frmLoadingPanel), True, True, False)

            For iLoop As Integer = 0 To grvDetail.RowCount - 1
                fn_LoadDataMerge(False, grvDetail.GetRowCellValue(iLoop, "Invoice"), grvDetail.GetRowCellValue(iLoop, "Penjamin"), grvDetail.GetRowCellValue(iLoop, "NoRegister"), grvDetail.GetRowCellValue(iLoop, "NoRegister_Awal"), grvDetail.GetRowCellValue(iLoop, "NoRekamMedis"), grvDetail.GetRowCellValue(iLoop, "NoSEP"), grvDetail.GetRowCellValue(iLoop, "TanggalDaftar"), grvDetail.GetRowCellValue(iLoop, "NoKartu"), grvDetail.GetRowCellValue(iLoop, "Pasien"), grvDetail.GetRowCellValue(iLoop, "TanggalLahir"), grvDetail.GetRowCellValue(iLoop, "JenisKelamin"), grvDetail.GetRowCellValue(iLoop, "NoTelepon"), grvDetail.GetRowCellValue(iLoop, "Faskes"), grvDetail.GetRowCellValue(iLoop, "Faskes_Label"), grvDetail.GetRowCellValue(iLoop, "Tujuan"), grvDetail.GetRowCellValue(iLoop, "DiagnosaAwal"), grvDetail.GetRowCellValue(iLoop, "Catatan"), grvDetail.GetRowCellValue(iLoop, "Peserta"), grvDetail.GetRowCellValue(iLoop, "Cob"), grvDetail.GetRowCellValue(iLoop, "JenisRawat"), grvDetail.GetRowCellValue(iLoop, "KelasRawat"))

                Dim dsNosep = oMerge.GetDataByNosep(grvDetail.GetRowCellValue(iLoop, "NoSEP"))

                Dim ds = oMerge.GetStructureHeader

                With ds
                    .KDMERGE = 0
                    .CATEGORY = IIf(grvDetail.GetRowCellValue(iLoop, "JenisRawat") = "RAWAT JALAN", 1, 2)
                    .DATECREATED = Now
                    .KDREG = grvDetail.GetRowCellValue(iLoop, "NoRegister")
                    .NOSEP = grvDetail.GetRowCellValue(iLoop, "NoSEP")
                    .KDCUSTOMER = grvDetail.GetRowCellValue(iLoop, "NoRekamMedis")
                    .ISCHEKED = False
                    .MEMO = "NEW"
                    .NOIDUSER = sUserID
                End With

                If dsNosep IsNot Nothing Then
                    ds.KDMERGE = dsNosep.KDMERGE
                    oMerge.UpdateData(ds)
                Else
                    oMerge.InsertData(ds)
                End If

                SplashScreenManager.Default.SetWaitFormCaption("Processing data " & sProcess & " of " & sTotal & "")
                sProcess += 1
            Next
        Catch oErr As Exception
            SplashScreenManager.CloseForm(False)
            MsgBox("Hitung Jumlah Record :  " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        Finally
            SplashScreenManager.CloseForm(False)
            MsgBox("Selesai", MsgBoxStyle.Information, Me.Text)
        End Try
    End Sub
    Private Sub btnBrowse_Click(sender As Object, e As EventArgs) Handles btnBrowse.Click
        If (FolderBrowserDialog1.ShowDialog() = DialogResult.OK) Then
            txtFolder.Text = FolderBrowserDialog1.SelectedPath
        End If
    End Sub
    Private Sub cboFILTER_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboFILTER.SelectedIndexChanged
        If cboFILTER.SelectedIndex = 2 Then
            chkTanggalPulang.Visible = False
            deDATEFrom.Visible = True
            deDATETo.Visible = True
            txtPARAMETER.Visible = False
        ElseIf cboFILTER.SelectedIndex = 3 Then
            chkTanggalPulang.Visible = False
            deDATEFrom.Visible = True
            deDATETo.Visible = True
            txtPARAMETER.Visible = False
        ElseIf cboFILTER.SelectedIndex = 4 Then
            chkTanggalPulang.Visible = True
            deDATEFrom.Visible = True
            deDATETo.Visible = True
            txtPARAMETER.Visible = False
        Else
            chkTanggalPulang.Visible = False
            deDATEFrom.Visible = False
            deDATETo.Visible = False
            txtPARAMETER.Visible = True
        End If
    End Sub
    Private Sub txtPARAMETER_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtPARAMETER.KeyPress
        If Asc(e.KeyChar) = Keys.Tab Then
            fn_LoadSecurity()
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
    Private Sub grvResume_FocusedRowChanged(ByVal sender As System.Object, ByVal e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs) Handles grvResume.FocusedRowChanged
        If grvResume.GetFocusedRowCellValue("KODE") Is Nothing Then
            Exit Sub
        End If

        fn_PrintStrukResumeRawatJalanRawatInap(grvResume.GetFocusedRowCellValue("KODE"), grvResume.GetFocusedRowCellValue("KATEGORI"))

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
    Private Sub grvLaboratorium_FocusedRowChanged(ByVal sender As System.Object, ByVal e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs) Handles grvLaboratorium.FocusedRowChanged
        If grvLaboratorium.GetFocusedRowCellValue("KODE") Is Nothing Then
            Exit Sub
        End If

        fn_SearchFolderPdf(grvLaboratorium.GetFocusedRowCellValue("KODE"), grvLaboratorium.GetFocusedRowCellValue("KDCUSTOMER"), grvLaboratorium.GetFocusedRowCellValue("NAME_DISPLAY"))

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
    Private Sub grvRadiologi_FocusedRowChanged(ByVal sender As System.Object, ByVal e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs) Handles grvRadiologi.FocusedRowChanged
        If grvRadiologi.GetFocusedRowCellValue("NoRadiologi") Is Nothing Then
            Exit Sub
        End If

        Dim radiologi As String = grvRadiologi.GetFocusedRowCellValue("NoRadiologi")

        PdfViewerRadiologi.CloseDocument()
        frmRawatInapList.HasilBriggingRadiologi(False, grvRadiologi.GetFocusedRowCellValue("NoRadiologi"), grvRadiologi.GetFocusedRowCellValue("Catatan"), CDate(grvRadiologi.GetFocusedRowCellValue("Tanggal")).ToString("yyyy-MM-dd HH:mm:ss"), grvRadiologi.GetFocusedRowCellValue("DOKTER"), False)

        Try
            PdfViewerRadiologi.LoadDocument("C:/HASILRADIOLGI/" & grvRadiologi.GetFocusedRowCellValue("NoRadiologi") & ".pdf")
        Catch ex As Exception

        End Try
    End Sub
    Private Sub grvRadiologi_DoubleClick(sender As Object, e As EventArgs) Handles grvRadiologi.DoubleClick
        If grvRadiologi.GetFocusedRowCellValue("NoRadiologi") Is Nothing Then
            Exit Sub
        End If

        PdfViewerRadiologi.CloseDocument()
        frmRawatInapList.HasilBriggingRadiologi(True, grvRadiologi.GetFocusedRowCellValue("NoRadiologi"), grvRadiologi.GetFocusedRowCellValue("Catatan"), CDate(grvRadiologi.GetFocusedRowCellValue("Tanggal")).ToString("yyyy-MM-dd HH:mm:ss"), grvRadiologi.GetFocusedRowCellValue("DOKTER"), False)

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
    Private Sub btnTutupListGerdQ_Click(sender As Object, e As EventArgs) Handles btnTutupListGerdQ.Click
        If btnTutupListGerdQ.Text = "Tutup List" Then
            btnTutupListGerdQ.Text = "Buka List"
            lLISTGERDQ.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        Else
            btnTutupListGerdQ.Text = "Tutup List"
            lLISTGERDQ.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        End If
    End Sub
    Private Sub grvGerdQ_FocusedRowChanged(ByVal sender As System.Object, ByVal e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs) Handles grvGerdQ.FocusedRowChanged
        If grvGerdQ.GetFocusedRowCellValue("KODE") Is Nothing Then
            Exit Sub
        End If

        Try
            PdfViewerGerdQ.CloseDocument()

            Dim FolderSimpan = "C:/GERDQ_LIST/"

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
    Private Sub btnUpload_Click(sender As Object, e As EventArgs) Handles btnUpload.Click
        If grvDetail.GetFocusedRowCellValue("NoRekamMedis") Is Nothing Then
            Exit Sub
        End If
        Dim frmPDFTransaksiList As New frmPDFTransaksiList
        Try
            frmPDFTransaksiList.fn_LoadMe(grvDetail.GetFocusedRowCellValue("NoRegister"), grvDetail.GetFocusedRowCellValue("NoRekamMedis"))
            frmPDFTransaksiList.ShowDialog(Me)
        Catch ex As Exception
            MsgBox("Load Form Detail : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
        Finally
            If Not frmPDFTransaksiList Is Nothing Then frmPDFTransaksiList.Dispose()
            frmPDFTransaksiList = Nothing
        End Try
    End Sub
    Private Sub UpdateTanggalPulangToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles UpdateTanggalPulangToolStripMenuItem.Click
        If grvDetail.GetFocusedRowCellValue("NoRekamMedis") Is Nothing Then
            Exit Sub
        End If
        If grvDetail.GetFocusedRowCellValue("CekTanggalPulang") = "Pulang" Then
            Dim dsCek = oRINGKASANKELUAR.GetData(grvDetail.GetFocusedRowCellValue("NoRegister"))
            If dsCek IsNot Nothing Then
                If MsgBox("Apakah Yakin Tanggal Keluar di Resume Rawat Inap akan di Rubah dengan Nomor " & dsCek.KDREG & "?", MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, Me.Text) = MsgBoxResult.No Then Exit Sub

                If oRINGKASANKELUAR.UpdateTanggalKeluar(grvDetail.GetFocusedRowCellValue("NoRegister"), sTanggalPulangDiFo) = True Then
                    MsgBox("Berhasil Update, Silahkan Refresh Untuk Melihat data", MsgBoxStyle.Information, Me.Text)
                Else
                    MsgBox("Gagal Update", MsgBoxStyle.Exclamation, Me.Text)
                End If
            Else
                MsgBox("Resume Rawat Inap Belum di Buat", MsgBoxStyle.Exclamation, Me.Text)
            End If
        Else
            MsgBox("Pasien Belum di Pulangkan Oleh Admin Rawat Inap", MsgBoxStyle.Exclamation, Me.Text)
        End If
    End Sub
#End Region
End Class