Imports System.Linq
Imports DataAccess
Imports System.Data.SqlClient
Imports DataAccess.My.Resources
Imports DevExpress.XtraSplashScreen
Imports System.IO
Imports iTextSharp.text
Imports iTextSharp.text.pdf

Public Class frmEMedrekRI_38
#Region "Declaration"
    Private oFormMode As FORM_MODE = FORM_MODE.FORM_MODE_VIEW
    Private isLoad As Boolean = False
    Private oDigital As New Inventory.clsDigital_RI_38
    Private sPERAWAT As String
    Private oCPPT As New Transaksi.clsCPPT

#End Region
#Region "Function"
    Public Sub LoadMe(ByVal FormMode As Integer, ByVal KDREG As String, ByVal KDCUSTOMER As String, ByVal NAMAPASIEN As String, ByVal JENISKELAMIN As String, ByVal KDUSER_PERAWAT As String, ByVal DPJP As String)
        oFormMode = FormMode

        txtNoPasien.Text = KDCUSTOMER
        txtNamaPasien.Text = NAMAPASIEN
        txtUmur.Text = JENISKELAMIN
        txtNoRegister.Text = KDREG
        sPERAWAT = KDUSER_PERAWAT
        txtDPJP.Text = DPJP
    End Sub
    Private Sub Form_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        fn_ChangeFormState()
        isLoad = True
    End Sub
    Private Sub Form_Closing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        'pdfViewerCPPTRAJAL.CloseDocument()
        'pdfViewerCPPTRANAP.CloseDocument()
        Dispose()
        sCode = txtNoRegister.Text.Trim.ToUpper
    End Sub
    Private Overloads Sub Dispose()
        MyBase.Dispose()
        Me.Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
    Private Sub fn_ChangeFormState()
        fn_tempilangrid()

        Select Case oFormMode
            Case FORM_MODE.FORM_MODE_VIEW
                fn_ViewMode(True)
                fn_LoadData()
            Case FORM_MODE.FORM_MODE_ADD
                fn_ViewMode(False)
                fn_EmptyMe()
            Case FORM_MODE.FORM_MODE_EDIT
                fn_ViewMode(False)
                fn_LoadData()
            Case Else
                fn_ViewMode(True)
        End Select

        'XtraTabControl1_SelectedPageChanged()
    End Sub
    'Private Sub fn_LoadPdfViewerCPPTRANANP()
    '    Try
    '        pdfViewerCPPTRANAP.CloseDocument()

    '        SplashScreenManager.ShowForm(Me, GetType(frmLoadingPanel), True, True, False)

    '        SplashScreenManager.Default.SetWaitFormCaption("Processing data CPPT Rawat Inap.....")

    '        If txtNoPasien.Text = String.Empty Then Exit Sub

    '        Dim FolderSimpan = "C:/CPPTRANAP/"

    '        If Not Directory.Exists(FolderSimpan) Then
    '            Directory.CreateDirectory(FolderSimpan)
    '        Else
    '            DeleteDirectory(FolderSimpan)
    '            Directory.CreateDirectory(FolderSimpan)
    '        End If

    '        Dim dataList As New List(Of Byte())
    '        Dim dsLoad() As Byte
    '        Dim ds = oCPPT.GetDataByRMRANAP(txtNoPasien.Text)

    '        If ds.Count > 0 Then
    '            Dim rpt As New xtraDigital_CPPT_01_RawatInap

    '            rpt.ShowPrintMarginsWarning = False
    '            rpt.Watermark.Text = sWATERMARK

    '            rpt.bindingSource.DataSource = ds
    '            rpt.ExportToPdf(FolderSimpan & "HASIL" & ".pdf")
    '            dataList.Add(File.ReadAllBytes(FolderSimpan & "HASIL" & ".pdf"))

    '            If dataList.Count > 0 Then
    '                dsLoad = MergeFilesByte(dataList)
    '                Dim stream As New MemoryStream(dsLoad)
    '                pdfViewerCPPTRANAP.LoadDocument(stream)
    '            End If
    '        End If

    '        SplashScreenManager.CloseForm(False)
    '    Catch oErr As Exception
    '        SplashScreenManager.CloseForm(False)
    '        MsgBox("Cetak CPPT Ranap" & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
    '    End Try
    'End Sub
    'Private Sub fn_LoadPdfViewerCPPTRAJAL()
    '    Try
    '        pdfViewerCPPTRAJAL.CloseDocument()

    '        SplashScreenManager.ShowForm(Me, GetType(frmLoadingPanel), True, True, False)

    '        SplashScreenManager.Default.SetWaitFormCaption("Processing data CPPT Rawat Inap.....")

    '        If txtNoPasien.Text = String.Empty Then Exit Sub

    '        Dim FolderSimpan = "C:/CPPTRAJAL/"

    '        If Not Directory.Exists(FolderSimpan) Then
    '            Directory.CreateDirectory(FolderSimpan)
    '        Else
    '            DeleteDirectory(FolderSimpan)
    '            Directory.CreateDirectory(FolderSimpan)
    '        End If

    '        Dim dataList As New List(Of Byte())
    '        Dim dsLoad() As Byte
    '        Dim ds = oCPPT.GetDataByRMRAJAL(txtNoPasien.Text)

    '        If sCPPT_QRRJ = False Then
    '            If ds.Count > 0 Then
    '                Dim rpt As New xtraDigital_CPPT_01

    '                rpt.ShowPrintMarginsWarning = False
    '                rpt.Watermark.Text = sWATERMARK

    '                rpt.bindingSource.DataSource = ds
    '                rpt.ExportToPdf(FolderSimpan & "HASIL" & ".pdf")
    '                dataList.Add(File.ReadAllBytes(FolderSimpan & "HASIL" & ".pdf"))

    '                If dataList.Count > 0 Then
    '                    dsLoad = MergeFilesByte(dataList)
    '                    Dim stream As New MemoryStream(dsLoad)
    '                    pdfViewerCPPTRAJAL.LoadDocument(stream)
    '                End If
    '            End If
    '        Else
    '            If ds.Count > 0 Then
    '                Dim rpt As New xtraDigital_CPPT_01_QR

    '                rpt.ShowPrintMarginsWarning = False
    '                rpt.Watermark.Text = sWATERMARK

    '                rpt.bindingSource.DataSource = ds
    '                rpt.ExportToPdf(FolderSimpan & "HASIL" & ".pdf")
    '                dataList.Add(File.ReadAllBytes(FolderSimpan & "HASIL" & ".pdf"))

    '                If dataList.Count > 0 Then
    '                    dsLoad = MergeFilesByte(dataList)
    '                    Dim stream As New MemoryStream(dsLoad)
    '                    pdfViewerCPPTRAJAL.LoadDocument(stream)
    '                End If
    '            End If
    '        End If

    '        SplashScreenManager.CloseForm(False)
    '    Catch oErr As Exception
    '        SplashScreenManager.CloseForm(False)
    '        MsgBox("Cetak CPPT Rajal" & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
    '    End Try
    'End Sub
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
    Private Sub fn_tempilangrid()
        'resikojatuh
        colTanggal.Width = lblresiko1.Width/2 - 9 
        colHari.Width = lblresiko1.Width/2 - 14 
        colA.Width = lblresiko2.Width + 2
        colB.Width = lblresiko3.Width + 1
        colC.Width = lblresiko4.Width - 10
        colD.Width = lblresiko5.Width
        colE.Width = lblresiko6.Width - 12
        colF.Width = lblresiko7.Width - 12
        colG.Width = lblresiko8.Width + 5
        colNamaPetugas.Width = lblresiko9.Width + 10

    End Sub
    'Private Sub XtraTabControl1_SelectedPageChanged() Handles XtraTabControl1.SelectedPageChanged
    '    If XtraTabControl1.SelectedTabPageIndex = 0 Then
    '        fn_LoadPdfViewerCPPTRANANP()
    '    ElseIf XtraTabControl1.SelectedTabPageIndex = 1 Then
    '        fn_LoadPdfViewerCPPTRAJAL()
    '    End If
    'End Sub
    Private Sub fn_ViewMode(ByVal Status As Boolean)
        btnSaveNew.Enabled = Not Status
        btnSaveClose.Enabled = Not Status

        txtASESMEN_02.Properties.ReadOnly = True
        chkASESMEN_03.Properties.ReadOnly = Status
        chkASESMEN_04.Properties.ReadOnly = Status
        txtASESMEN_05.Properties.ReadOnly = Status
        chkASESMEN_06.Properties.ReadOnly = Status
        chkASESMEN_07.Properties.ReadOnly = Status
        txtASESMEN_08.Properties.ReadOnly = Status
        txtASESMEN_09.Properties.ReadOnly = Status
        chkASESMEN_10.Properties.ReadOnly = Status
        chkASESMEN_11.Properties.ReadOnly = Status
        chkASESMEN_12.Properties.ReadOnly = Status
        chkASESMEN_13.Properties.ReadOnly = Status
        chkASESMEN_14.Properties.ReadOnly = Status
        chkASESMEN_15.Properties.ReadOnly = Status
        chkASESMEN_16.Properties.ReadOnly = Status
        chkASESMEN_17.Properties.ReadOnly = Status
        txtASESMEN_18.Properties.ReadOnly = Status
        txtASESMEN_19.Properties.ReadOnly = Status
        txtASESMEN_20.Properties.ReadOnly = Status
        chkASESMEN_21.Properties.ReadOnly = Status
        chkASESMEN_22.Properties.ReadOnly = Status
        txtASESMEN_23.Properties.ReadOnly = Status
        txtASESMEN_24.Properties.ReadOnly = Status
        chkASESMEN_25.Properties.ReadOnly = Status
        chkASESMEN_26.Properties.ReadOnly = Status
        txtASESMEN_27.Properties.ReadOnly = Status
        txtASESMEN_28.Properties.ReadOnly = Status
        chkASESMEN_29.Properties.ReadOnly = Status
        chkASESMEN_30.Properties.ReadOnly = Status
        txtASESMEN_31.Properties.ReadOnly = Status
        chkASESMEN_32.Properties.ReadOnly = Status
        chkASESMEN_33.Properties.ReadOnly = Status
        chkASESMEN_34.Properties.ReadOnly = Status
        chkASESMEN_35.Properties.ReadOnly = Status
        chkASESMEN_36.Properties.ReadOnly = Status
        chkASESMEN_37.Properties.ReadOnly = Status
        chkASESMEN_38.Properties.ReadOnly = Status
        chkASESMEN_39.Properties.ReadOnly = Status
        txtASESMEN_40.Properties.ReadOnly = Status
        chkASESMEN_41.Properties.ReadOnly = Status
        chkASESMEN_42.Properties.ReadOnly = Status
        chkASESMEN_43.Properties.ReadOnly = Status
        chkASESMEN_44.Properties.ReadOnly = Status
        txtASESMEN_45.Properties.ReadOnly = Status
        chkASESMEN_46.Properties.ReadOnly = Status
        chkASESMEN_47.Properties.ReadOnly = Status
        txtASESMEN_48.Properties.ReadOnly = Status
        chkASESMEN_49.Properties.ReadOnly = Status
        chkASESMEN_50.Properties.ReadOnly = Status
        chkASESMEN_51.Properties.ReadOnly = Status
        txtASESMEN_52.Properties.ReadOnly = Status
        chkASESMEN_52.Properties.ReadOnly = Status
        chkASESMEN_53.Properties.ReadOnly = Status
        chkASESMEN_54.Properties.ReadOnly = Status
        chkASESMEN_55.Properties.ReadOnly = Status
        chkASESMEN_56.Properties.ReadOnly = Status
        chkASESMEN_57.Properties.ReadOnly = Status
        chkASESMEN_58.Properties.ReadOnly = Status
        chkASESMEN_59.Properties.ReadOnly = Status
        chkASESMEN_60.Properties.ReadOnly = Status
        chkASESMEN_61.Properties.ReadOnly = Status
        chkASESMEN_62.Properties.ReadOnly = Status
        chkASESMEN_63.Properties.ReadOnly = Status
        chkASESMEN_64.Properties.ReadOnly = Status
        chkASESMEN_65.Properties.ReadOnly = Status
        chkASESMEN_66.Properties.ReadOnly = Status
        chkASESMEN_67.Properties.ReadOnly = Status
        chkASESMEN_68.Properties.ReadOnly = Status
        chkASESMEN_69.Properties.ReadOnly = Status
        chkASESMEN_70.Properties.ReadOnly = Status
        chkASESMEN_71.Properties.ReadOnly = Status
        chkASESMEN_72.Properties.ReadOnly = Status
        chkASESMEN_73.Properties.ReadOnly = Status
        chkASESMEN_74.Properties.ReadOnly = Status
        chkASESMEN_75.Properties.ReadOnly = Status
        chkASESMEN_76.Properties.ReadOnly = Status
        chkASESMEN_77.Properties.ReadOnly = Status
        chkASESMEN_78.Properties.ReadOnly = Status
        chkASESMEN_79.Properties.ReadOnly = Status
        chkASESMEN_80.Properties.ReadOnly = Status
        chkASESMEN_81.Properties.ReadOnly = Status
        chkASESMEN_82.Properties.ReadOnly = Status
        chkASESMEN_83.Properties.ReadOnly = Status
        chkASESMEN_84.Properties.ReadOnly = Status
        chkASESMEN_85.Properties.ReadOnly = Status
        chkASESMEN_86.Properties.ReadOnly = Status
        chkASESMEN_87.Properties.ReadOnly = Status
        chkASESMEN_88.Properties.ReadOnly = Status
        chkASESMEN_89.Properties.ReadOnly = Status
        chkASESMEN_90.Properties.ReadOnly = Status
        chkASESMEN_91.Properties.ReadOnly = Status
        chkASESMEN_92.Properties.ReadOnly = Status
        chkASESMEN_93.Properties.ReadOnly = Status
        chkASESMEN_94.Properties.ReadOnly = Status
        chkASESMEN_95.Properties.ReadOnly = Status
        chkASESMEN_96.Properties.ReadOnly = Status
        chkASESMEN_97.Properties.ReadOnly = Status
        chkASESMEN_98.Properties.ReadOnly = Status
        chkASESMEN_99.Properties.ReadOnly = Status
        chkASESMEN_100.Properties.ReadOnly = Status
        chkASESMEN_101.Properties.ReadOnly = Status
        chkASESMEN_102.Properties.ReadOnly = Status
        chkASESMEN_103.Properties.ReadOnly = Status
        chkASESMEN_104.Properties.ReadOnly = Status
        chkASESMEN_105.Properties.ReadOnly = Status
        chkASESMEN_106.Properties.ReadOnly = Status

        DateEdit1.Properties.ReadOnly = Status
        DateEdit2.Properties.ReadOnly = Status
        DateEdit3.Properties.ReadOnly = Status
        DateEdit4.Properties.ReadOnly = Status
        DateEdit5.Properties.ReadOnly = Status

        txtASESMEN_108.Properties.ReadOnly = Status
        txtASESMEN_109.Properties.ReadOnly = Status
        txtASESMEN_110.Properties.ReadOnly = Status
        txtASESMEN_111.Properties.ReadOnly = Status
        txtASESMEN_112.Properties.ReadOnly = Status
        txtASESMEN_113.Properties.ReadOnly = Status
        txtASESMEN_114.Properties.ReadOnly = Status
        txtASESMEN_115.Properties.ReadOnly = Status
        txtASESMEN_116.Properties.ReadOnly = Status
        txtASESMEN_117.Properties.ReadOnly = Status
        txtASESMEN_118.Properties.ReadOnly = Status
        txtASESMEN_119.Properties.ReadOnly = Status
        txtASESMEN_120.Properties.ReadOnly = Status
        txtASESMEN_121.Properties.ReadOnly = Status
        txtASESMEN_122.Properties.ReadOnly = Status
        txtASESMEN_123.Properties.ReadOnly = Status
        txtASESMEN_124.Properties.ReadOnly = Status
        txtASESMEN_125.Properties.ReadOnly = Status
        txtASESMEN_126.Properties.ReadOnly = Status
        txtASESMEN_127.Properties.ReadOnly = Status
        txtASESMEN_128.Properties.ReadOnly = Status
        txtASESMEN_129.Properties.ReadOnly = Status
        txtASESMEN_130.Properties.ReadOnly = Status
        txtASESMEN_131.Properties.ReadOnly = Status
        txtASESMEN_132.Properties.ReadOnly = Status
        txtASESMEN_133.Properties.ReadOnly = Status
        txtASESMEN_134.Properties.ReadOnly = Status
        txtASESMEN_135.Properties.ReadOnly = Status
        txtASESMEN_136.Properties.ReadOnly = Status
        txtASESMEN_137.Properties.ReadOnly = Status
        txtASESMEN_138.Properties.ReadOnly = Status
        txtASESMEN_139.Properties.ReadOnly = Status
        txtASESMEN_140.Properties.ReadOnly = Status
        txtASESMEN_141.Properties.ReadOnly = Status
        txtASESMEN_142.Properties.ReadOnly = Status
        txtASESMEN_143.Properties.ReadOnly = Status
        txtASESMEN_144.Properties.ReadOnly = Status
        txtASESMEN_145.Properties.ReadOnly = Status
        txtASESMEN_146.Properties.ReadOnly = Status
        txtASESMEN_147.Properties.ReadOnly = Status

        chkASESMEN_149.Properties.ReadOnly = Status
        chkASESMEN_150.Properties.ReadOnly = Status
        txtASESMEN_151.Properties.ReadOnly = Status
        chkASESMEN_152.Properties.ReadOnly = Status
        chkASESMEN_153.Properties.ReadOnly = Status
        txtASESMEN_154.Properties.ReadOnly = Status
        chkASESMEN_155.Properties.ReadOnly = Status
        chkASESMEN_156.Properties.ReadOnly = Status
        txtASESMEN_157.Properties.ReadOnly = Status
        chkASESMEN_158.Properties.ReadOnly = Status
        chkASESMEN_159.Properties.ReadOnly = Status
        txtASESMEN_160.Properties.ReadOnly = Status
        chkASESMEN_161.Properties.ReadOnly = Status
        chkASESMEN_162.Properties.ReadOnly = Status
        txtASESMEN_163.Properties.ReadOnly = Status
        chkASESMEN_164.Properties.ReadOnly = Status
        chkASESMEN_165.Properties.ReadOnly = Status
        txtASESMEN_166.Properties.ReadOnly = Status
        chkASESMEN_167.Properties.ReadOnly = Status
        chkASESMEN_168.Properties.ReadOnly = Status
        txtASESMEN_169.Properties.ReadOnly = Status
        chkASESMEN_170.Properties.ReadOnly = Status
        chkASESMEN_171.Properties.ReadOnly = Status
        txtASESMEN_172.Properties.ReadOnly = Status
        chkASESMEN_173.Properties.ReadOnly = Status
        chkASESMEN_174.Properties.ReadOnly = Status
        txtASESMEN_175.Properties.ReadOnly = Status
        chkASESMEN_176.Properties.ReadOnly = Status
        txtASESMEN_177.Properties.ReadOnly = Status
        chkASESMEN_178.Properties.ReadOnly = Status
        txtASESMEN_179.Properties.ReadOnly = Status
        chkASESMEN_180.Properties.ReadOnly = Status
        txtASESMEN_181.Properties.ReadOnly = Status
        chkASESMEN_182.Properties.ReadOnly = Status
        txtASESMEN_183.Properties.ReadOnly = Status
        chkASESMEN_184.Properties.ReadOnly = Status
        txtASESMEN_185.Properties.ReadOnly = Status
        chkASESMEN_186.Properties.ReadOnly = Status
        txtASESMEN_187.Properties.ReadOnly = Status
        chkASESMEN_188.Properties.ReadOnly = Status
        txtASESMEN_189.Properties.ReadOnly = Status
        chkASESMEN_190.Properties.ReadOnly = Status
        txtASESMEN_191.Properties.ReadOnly = Status
        txtASESMEN_192.Properties.ReadOnly = Status

        txtASESMEN_193.Properties.ReadOnly = Status
        txtASESMEN_194.Properties.ReadOnly = Status
        txtASESMEN_195.Properties.ReadOnly = Status
        txtASESMEN_196.Properties.ReadOnly = Status
        txtASESMEN_197.Properties.ReadOnly = Status
        txtASESMEN_198.Properties.ReadOnly = Status
        txtASESMEN_199.Properties.ReadOnly = Status

        txtASESMEN_200.Properties.ReadOnly = Status
        txtASESMEN_201.Properties.ReadOnly = Status
        txtASESMEN_202.Properties.ReadOnly = Status
        txtASESMEN_203.Properties.ReadOnly = Status
        txtASESMEN_204.Properties.ReadOnly = Status
        txtASESMEN_205.Properties.ReadOnly = Status
        txtASESMEN_206.Properties.ReadOnly = Status
        txtASESMEN_207.Properties.ReadOnly = Status
        txtASESMEN_208.Properties.ReadOnly = Status
        txtASESMEN_209.Properties.ReadOnly = Status
        txtASESMEN_210.Properties.ReadOnly = Status
        txtASESMEN_211.Properties.ReadOnly = Status
        txtASESMEN_212.Properties.ReadOnly = Status
        txtASESMEN_213.Properties.ReadOnly = Status
        txtASESMEN_214.Properties.ReadOnly = Status
        txtASESMEN_215.Properties.ReadOnly = Status
        txtASESMEN_216.Properties.ReadOnly = Status
        txtASESMEN_217.Properties.ReadOnly = Status
        txtASESMEN_218.Properties.ReadOnly = Status
        txtASESMEN_219.Properties.ReadOnly = Status
        txtASESMEN_220.Properties.ReadOnly = Status
        txtASESMEN_221.Properties.ReadOnly = Status
        txtASESMEN_222.Properties.ReadOnly = Status
        txtASESMEN_223.Properties.ReadOnly = Status
        txtASESMEN_224.Properties.ReadOnly = Status
        txtASESMEN_225.Properties.ReadOnly = Status
        txtASESMEN_226.Properties.ReadOnly = Status
        txtASESMEN_227.Properties.ReadOnly = Status
        txtASESMEN_228.Properties.ReadOnly = Status
        txtASESMEN_229.Properties.ReadOnly = Status
        txtASESMEN_230.Properties.ReadOnly = Status
        txtASESMEN_231.Properties.ReadOnly = Status
        txtASESMEN_232.Properties.ReadOnly = Status
        txtASESMEN_233.Properties.ReadOnly = Status
        txtASESMEN_234.Properties.ReadOnly = Status
        txtASESMEN_235.Properties.ReadOnly = Status
        txtASESMEN_236.Properties.ReadOnly = Status
        txtASESMEN_237.Properties.ReadOnly = Status
        txtASESMEN_238.Properties.ReadOnly = Status
        txtASESMEN_239.Properties.ReadOnly = Status
        txtASESMEN_240.Properties.ReadOnly = Status
        txtASESMEN_241.Properties.ReadOnly = Status
        txtASESMEN_242.Properties.ReadOnly = Status
        txtASESMEN_243.Properties.ReadOnly = Status
        txtASESMEN_244.Properties.ReadOnly = Status
        txtASESMEN_245.Properties.ReadOnly = Status
        txtASESMEN_246.Properties.ReadOnly = Status
        txtASESMEN_247.Properties.ReadOnly = Status
        txtASESMEN_248.Properties.ReadOnly = Status

        'monitoring v2
        CheckEdit1.Properties.ReadOnly = Status
        CheckEdit2.Properties.ReadOnly = Status
        CheckEdit3.Properties.ReadOnly = Status
        CheckEdit4.Properties.ReadOnly = Status
        CheckEdit5.Properties.ReadOnly = Status
        CheckEdit6.Properties.ReadOnly = Status
        CheckEdit7.Properties.ReadOnly = Status
        CheckEdit8.Properties.ReadOnly = Status
        CheckEdit9.Properties.ReadOnly = Status
        CheckEdit10.Properties.ReadOnly = Status
        CheckEdit11.Properties.ReadOnly = Status
        CheckEdit12.Properties.ReadOnly = Status
        CheckEdit13.Properties.ReadOnly = Status
        CheckEdit14.Properties.ReadOnly = Status
        CheckEdit15.Properties.ReadOnly = Status
        CheckEdit16.Properties.ReadOnly = Status
        CheckEdit17.Properties.ReadOnly = Status
        CheckEdit18.Properties.ReadOnly = Status
        CheckEdit19.Properties.ReadOnly = Status
        CheckEdit20.Properties.ReadOnly = Status
        CheckEdit21.Properties.ReadOnly = Status
        CheckEdit22.Properties.ReadOnly = Status
        CheckEdit23.Properties.ReadOnly = Status
        CheckEdit24.Properties.ReadOnly = Status
        CheckEdit25.Properties.ReadOnly = Status
        CheckEdit26.Properties.ReadOnly = Status
        CheckEdit27.Properties.ReadOnly = Status
        CheckEdit28.Properties.ReadOnly = Status
        CheckEdit29.Properties.ReadOnly = Status
        CheckEdit30.Properties.ReadOnly = Status
        CheckEdit31.Properties.ReadOnly = Status
        CheckEdit32.Properties.ReadOnly = Status
        CheckEdit33.Properties.ReadOnly = Status
        CheckEdit34.Properties.ReadOnly = Status
        CheckEdit35.Properties.ReadOnly = Status
        CheckEdit36.Properties.ReadOnly = Status
        CheckEdit37.Properties.ReadOnly = Status
        CheckEdit38.Properties.ReadOnly = Status
        CheckEdit39.Properties.ReadOnly = Status
        CheckEdit40.Properties.ReadOnly = Status
        CheckEdit41.Properties.ReadOnly = Status
        CheckEdit43.Properties.ReadOnly = Status
        CheckEdit44.Properties.ReadOnly = Status
        CheckEdit45.Properties.ReadOnly = Status
        CheckEdit46.Properties.ReadOnly = Status
        CheckEdit47.Properties.ReadOnly = Status
        CheckEdit48.Properties.ReadOnly = Status
        CheckEdit49.Properties.ReadOnly = Status
        CheckEdit50.Properties.ReadOnly = Status


        txtASESMEN_249.Properties.ReadOnly = Status
        txtASESMEN_250.Properties.ReadOnly = Status
        txtASESMEN_251.Properties.ReadOnly = Status
        txtASESMEN_252.Properties.ReadOnly = Status
        txtASESMEN_253.Properties.ReadOnly = Status
        txtASESMEN_254.Properties.ReadOnly = Status
        txtASESMEN_255.Properties.ReadOnly = Status
        txtASESMEN_256.Properties.ReadOnly = Status
        txtASESMEN_257.Properties.ReadOnly = Status
        txtASESMEN_258.Properties.ReadOnly = Status
        txtASESMEN_259.Properties.ReadOnly = Status
        txtASESMEN_260.Properties.ReadOnly = Status
        chkASESMEN_261.Properties.ReadOnly = Status
        chkASESMEN_262.Properties.ReadOnly = Status
        chkASESMEN_263.Properties.ReadOnly = Status
        chkASESMEN_264.Properties.ReadOnly = Status
        txtASESMEN_265.Properties.ReadOnly = Status
        txtASESMEN_266.Properties.ReadOnly = Status
        txtASESMEN_267.Properties.ReadOnly = Status
        txtASESMEN_268.Properties.ReadOnly = Status
        txtASESMEN_269.Properties.ReadOnly = Status
        txtASESMEN_270.Properties.ReadOnly = Status
        chkASESMEN_271.Properties.ReadOnly = Status
        chkASESMEN_272.Properties.ReadOnly = Status
        chkASESMEN_273.Properties.ReadOnly = Status
        chkASESMEN_274.Properties.ReadOnly = Status
        chkASESMEN_275.Properties.ReadOnly = Status
        chkASESMEN_276.Properties.ReadOnly = Status
        chkASESMEN_277.Properties.ReadOnly = Status
        chkASESMEN_278.Properties.ReadOnly = Status
        chkASESMEN_279.Properties.ReadOnly = Status
        chkASESMEN_280.Properties.ReadOnly = Status
        txtASESMEN_281.Properties.ReadOnly = Status
        chkASESMEN_282.Properties.ReadOnly = Status
        chkASESMEN_283.Properties.ReadOnly = Status
        chkASESMEN_284.Properties.ReadOnly = Status
        chkASESMEN_285.Properties.ReadOnly = Status
        chkASESMEN_286.Properties.ReadOnly = Status
        chkASESMEN_287.Properties.ReadOnly = Status
        chkASESMEN_288.Properties.ReadOnly = Status
        chkASESMEN_289.Properties.ReadOnly = Status
        chkASESMEN_290.Properties.ReadOnly = Status
        chkASESMEN_291.Properties.ReadOnly = Status
        chkASESMEN_292.Properties.ReadOnly = Status
        txtASESMEN_293.Properties.ReadOnly = Status
        chkASESMEN_294.Properties.ReadOnly = Status
        chkASESMEN_295.Properties.ReadOnly = Status
        chkASESMEN_296.Properties.ReadOnly = Status
        chkASESMEN_297.Properties.ReadOnly = Status
        txtASESMEN_298.Properties.ReadOnly = Status
        chkASESMEN_299.Properties.ReadOnly = Status
        chkASESMEN_300.Properties.ReadOnly = Status
        chkASESMEN_301.Properties.ReadOnly = Status
        chkASESMEN_302.Properties.ReadOnly = Status
        txtASESMEN_303.Properties.ReadOnly = Status
        chkASESMEN_304.Properties.ReadOnly = Status
        txtASESMEN_305.Properties.ReadOnly = Status
        chkASESMEN_306.Properties.ReadOnly = Status
        chkASESMEN_307.Properties.ReadOnly = Status
        chkASESMEN_308.Properties.ReadOnly = Status
        chkASESMEN_309.Properties.ReadOnly = Status
        chkASESMEN_310.Properties.ReadOnly = Status
        chkASESMEN_311.Properties.ReadOnly = Status
        chkASESMEN_312.Properties.ReadOnly = Status
        chkASESMEN_313.Properties.ReadOnly = Status
        chkASESMEN_314.Properties.ReadOnly = Status
        chkASESMEN_315.Properties.ReadOnly = Status
        chkASESMEN_316.Properties.ReadOnly = Status
        chkASESMEN_317.Properties.ReadOnly = Status
        chkASESMEN_318.Properties.ReadOnly = Status
        chkASESMEN_319.Properties.ReadOnly = Status
        chkASESMEN_320.Properties.ReadOnly = Status
        chkASESMEN_321.Properties.ReadOnly = Status
        chkASESMEN_322.Properties.ReadOnly = Status
        chkASESMEN_323.Properties.ReadOnly = Status
        chkASESMEN_324.Properties.ReadOnly = Status
        chkASESMEN_325.Properties.ReadOnly = Status
        chkASESMEN_326.Properties.ReadOnly = Status
        chkASESMEN_327.Properties.ReadOnly = Status
        chkASESMEN_328.Properties.ReadOnly = Status
        chkASESMEN_329.Properties.ReadOnly = Status
        chkASESMEN_330.Properties.ReadOnly = Status
        chkASESMEN_331.Properties.ReadOnly = Status
        chkASESMEN_332.Properties.ReadOnly = Status
        chkASESMEN_333.Properties.ReadOnly = Status
        chkASESMEN_334.Properties.ReadOnly = Status
        chkASESMEN_335.Properties.ReadOnly = Status
        chkASESMEN_336.Properties.ReadOnly = Status
        chkASESMEN_337.Properties.ReadOnly = Status
        chkASESMEN_338.Properties.ReadOnly = Status
        chkASESMEN_339.Properties.ReadOnly = Status
        chkASESMEN_340.Properties.ReadOnly = Status
        chkASESMEN_341.Properties.ReadOnly = Status
        chkASESMEN_342.Properties.ReadOnly = Status
        chkASESMEN_343.Properties.ReadOnly = Status
        chkASESMEN_344.Properties.ReadOnly = Status
        chkASESMEN_345.Properties.ReadOnly = Status
        chkASESMEN_346.Properties.ReadOnly = Status
        chkASESMEN_347.Properties.ReadOnly = Status
        chkASESMEN_348.Properties.ReadOnly = Status
        chkASESMEN_349.Properties.ReadOnly = Status
        chkASESMEN_350.Properties.ReadOnly = Status
        chkASESMEN_351.Properties.ReadOnly = Status
        chkASESMEN_352.Properties.ReadOnly = Status
        chkASESMEN_353.Properties.ReadOnly = Status
        chkASESMEN_354.Properties.ReadOnly = Status
        chkASESMEN_355.Properties.ReadOnly = Status
        chkASESMEN_356.Properties.ReadOnly = Status
        chkASESMEN_357.Properties.ReadOnly = Status
        chkASESMEN_358.Properties.ReadOnly = Status
        chkASESMEN_359.Properties.ReadOnly = Status
        txtASESMEN_360.Properties.ReadOnly = Status
        chkASESMEN_361.Properties.ReadOnly = Status
        chkASESMEN_362.Properties.ReadOnly = Status
        chkASESMEN_363.Properties.ReadOnly = Status
        chkASESMEN_364.Properties.ReadOnly = Status
        chkASESMEN_365.Properties.ReadOnly = Status
        chkASESMEN_366.Properties.ReadOnly = Status
        chkASESMEN_367.Properties.ReadOnly = Status
        chkASESMEN_368.Properties.ReadOnly = Status
        chkASESMEN_369.Properties.ReadOnly = Status
        chkASESMEN_370.Properties.ReadOnly = Status
        chkASESMEN_371.Properties.ReadOnly = Status
        chkASESMEN_372.Properties.ReadOnly = Status
        chkASESMEN_373.Properties.ReadOnly = Status
        chkASESMEN_374.Properties.ReadOnly = Status
        chkASESMEN_375.Properties.ReadOnly = Status
        chkASESMEN_376.Properties.ReadOnly = Status
        chkASESMEN_377.Properties.ReadOnly = Status
        chkASESMEN_378.Properties.ReadOnly = Status
        chkASESMEN_379.Properties.ReadOnly = Status
        chkASESMEN_380.Properties.ReadOnly = Status
        chkASESMEN_381.Properties.ReadOnly = Status
        chkASESMEN_382.Properties.ReadOnly = Status
        chkASESMEN_383.Properties.ReadOnly = Status
        chkASESMEN_384.Properties.ReadOnly = Status
        chkASESMEN_385.Properties.ReadOnly = Status
        chkASESMEN_386.Properties.ReadOnly = Status
        chkASESMEN_387.Properties.ReadOnly = Status
        txtASESMEN_388.Properties.ReadOnly = Status
        chkASESMEN_389.Properties.ReadOnly = Status
        chkASESMEN_390.Properties.ReadOnly = Status
        chkASESMEN_391.Properties.ReadOnly = Status
        chkASESMEN_392.Properties.ReadOnly = Status
        chkASESMEN_393.Properties.ReadOnly = Status
        chkASESMEN_394.Properties.ReadOnly = Status
        chkASESMEN_395.Properties.ReadOnly = Status
        chkASESMEN_396.Properties.ReadOnly = Status
        chkASESMEN_397.Properties.ReadOnly = Status
        chkASESMEN_398.Properties.ReadOnly = Status
        chkASESMEN_399.Properties.ReadOnly = Status
        chkASESMEN_400.Properties.ReadOnly = Status
        txtASESMEN_401.Properties.ReadOnly = Status
        txtASESMEN_402.Properties.ReadOnly = Status
        chkASESMEN_403.Properties.ReadOnly = Status
        chkASESMEN_404.Properties.ReadOnly = Status
        chkASESMEN_405.Properties.ReadOnly = Status
        chkASESMEN_406.Properties.ReadOnly = Status
        chkASESMEN_407.Properties.ReadOnly = Status
        txtASESMEN_408.Properties.ReadOnly = Status
        chkASESMEN_409.Properties.ReadOnly = Status
        chkASESMEN_410.Properties.ReadOnly = Status
        chkASESMEN_411.Properties.ReadOnly = Status
        chkASESMEN_412.Properties.ReadOnly = Status
        chkASESMEN_413.Properties.ReadOnly = Status
        chkASESMEN_414.Properties.ReadOnly = Status
        chkASESMEN_415.Properties.ReadOnly = Status
        chkASESMEN_416.Properties.ReadOnly = Status
        chkASESMEN_417.Properties.ReadOnly = Status
        chkASESMEN_418.Properties.ReadOnly = Status
        chkASESMEN_419.Properties.ReadOnly = Status
        chkASESMEN_420.Properties.ReadOnly = Status
        chkASESMEN_421.Properties.ReadOnly = Status
        chkASESMEN_422.Properties.ReadOnly = Status
        chkASESMEN_423.Properties.ReadOnly = Status
        chkASESMEN_424.Properties.ReadOnly = Status
        chkASESMEN_425.Properties.ReadOnly = Status
        chkASESMEN_426.Properties.ReadOnly = Status
        txtASESMEN_427.Properties.ReadOnly = Status
        chkASESMEN_428.Properties.ReadOnly = Status
        txtASESMEN_429.Properties.ReadOnly = Status
        txtASESMEN_430.Properties.ReadOnly = Status
        txtASESMEN_431.Properties.ReadOnly = Status
        txtASESMEN_432.Properties.ReadOnly = Status
        txtASESMEN_433.Properties.ReadOnly = Status
        chkASESMEN_434.Properties.ReadOnly = Status
        chkASESMEN_435.Properties.ReadOnly = Status
        txtASESMEN_436.Properties.ReadOnly = Status
        chkASESMEN_437.Properties.ReadOnly = Status
        chkASESMEN_438.Properties.ReadOnly = Status
        chkASESMEN_439.Properties.ReadOnly = Status
        chkASESMEN_440.Properties.ReadOnly = Status
        chkASESMEN_441.Properties.ReadOnly = Status
        txtASESMEN_442.Properties.ReadOnly = Status
        txtASESMEN_443.Properties.ReadOnly = Status
        chkASESMEN_444.Properties.ReadOnly = Status
        chkASESMEN_445.Properties.ReadOnly = Status
        chkASESMEN_446.Properties.ReadOnly = Status
        chkASESMEN_447.Properties.ReadOnly = Status
        chkASESMEN_448.Properties.ReadOnly = Status
        chkASESMEN_449.Properties.ReadOnly = Status
        chkASESMEN_450.Properties.ReadOnly = Status
        chkASESMEN_451.Properties.ReadOnly = Status
        chkASESMEN_452.Properties.ReadOnly = Status
        chkASESMEN_453.Properties.ReadOnly = Status
        chkASESMEN_454.Properties.ReadOnly = Status
        chkASESMEN_455.Properties.ReadOnly = Status
        chkASESMEN_456.Properties.ReadOnly = Status
        chkASESMEN_457.Properties.ReadOnly = Status
        chkASESMEN_458.Properties.ReadOnly = Status
        txtASESMEN_459.Properties.ReadOnly = Status
        chkASESMEN_460.Properties.ReadOnly = Status
        chkASESMEN_461.Properties.ReadOnly = Status
        chkASESMEN_462.Properties.ReadOnly = Status
        chkASESMEN_463.Properties.ReadOnly = Status
        chkASESMEN_464.Properties.ReadOnly = Status
        txtASESMEN_465.Properties.ReadOnly = Status
        chkASESMEN_466.Properties.ReadOnly = Status
        chkASESMEN_467.Properties.ReadOnly = Status
        chkASESMEN_468.Properties.ReadOnly = Status
        chkASESMEN_469.Properties.ReadOnly = Status
        txtASESMEN_470.Properties.ReadOnly = Status
        chkASESMEN_471.Properties.ReadOnly = Status
        chkASESMEN_472.Properties.ReadOnly = Status
        chkASESMEN_473.Properties.ReadOnly = Status
        chkASESMEN_474.Properties.ReadOnly = Status
        chkASESMEN_475.Properties.ReadOnly = Status
        chkASESMEN_476.Properties.ReadOnly = Status
        chkASESMEN_477.Properties.ReadOnly = Status
        chkASESMEN_478.Properties.ReadOnly = Status
        chkASESMEN_479.Properties.ReadOnly = Status
        chkASESMEN_480.Properties.ReadOnly = Status
        chkASESMEN_481.Properties.ReadOnly = Status
        chkASESMEN_482.Properties.ReadOnly = Status
        chkASESMEN_483.Properties.ReadOnly = Status
        chkASESMEN_484.Properties.ReadOnly = Status
        txtASESMEN_485.Properties.ReadOnly = Status
        chkASESMEN_486.Properties.ReadOnly = Status
        chkASESMEN_487.Properties.ReadOnly = Status
        chkASESMEN_488.Properties.ReadOnly = Status
        chkASESMEN_489.Properties.ReadOnly = Status
        txtASESMEN_490.Properties.ReadOnly = Status
        txtASESMEN_491.Properties.ReadOnly = Status
        txtASESMEN_492.Properties.ReadOnly = Status
        txtASESMEN_493.Properties.ReadOnly = Status
        txtASESMEN_494.Properties.ReadOnly = Status
        'txtASESMEN_495.Properties.ReadOnly = Status
        txtASESMEN_496.Properties.ReadOnly = Status
        txtASESMEN_497.Properties.ReadOnly = Status
        txtASESMEN_498.Properties.ReadOnly = Status
        txtASESMEN_499.Properties.ReadOnly = Status
        txtASESMEN_500.Properties.ReadOnly = Status
        txtASESMEN_501.Properties.ReadOnly = Status
        txtASESMEN_502.Properties.ReadOnly = Status
        txtASESMEN_503.Properties.ReadOnly = Status
        txtASESMEN_504.Properties.ReadOnly = Status
        txtASESMEN_505.Properties.ReadOnly = Status
        chkASESMEN_506.Properties.ReadOnly = Status
        chkASESMEN_507.Properties.ReadOnly = Status
        chkASESMEN_508.Properties.ReadOnly = Status
        chkASESMEN_509.Properties.ReadOnly = Status
        chkASESMEN_510.Properties.ReadOnly = Status
        chkASESMEN_511.Properties.ReadOnly = Status
        chkASESMEN_512.Properties.ReadOnly = Status
        chkASESMEN_513.Properties.ReadOnly = Status
        chkASESMEN_514.Properties.ReadOnly = Status
        chkASESMEN_515.Properties.ReadOnly = Status
        chkASESMEN_516.Properties.ReadOnly = Status
        chkASESMEN_517.Properties.ReadOnly = Status
        chkASESMEN_518.Properties.ReadOnly = Status
        chkASESMEN_519.Properties.ReadOnly = Status
        chkASESMEN_520.Properties.ReadOnly = Status
        chkASESMEN_521.Properties.ReadOnly = Status
        chkASESMEN_522.Properties.ReadOnly = Status
        chkASESMEN_523.Properties.ReadOnly = Status
        chkASESMEN_524.Properties.ReadOnly = Status
        chkASESMEN_525.Properties.ReadOnly = Status
        chkASESMEN_526.Properties.ReadOnly = Status
        chkASESMEN_527.Properties.ReadOnly = Status
        chkASESMEN_528.Properties.ReadOnly = Status
        chkASESMEN_529.Properties.ReadOnly = Status
        chkASESMEN_530.Properties.ReadOnly = Status
        chkASESMEN_531.Properties.ReadOnly = Status
        chkASESMEN_532.Properties.ReadOnly = Status
        chkASESMEN_533.Properties.ReadOnly = Status
        chkASESMEN_534.Properties.ReadOnly = Status
        chkASESMEN_535.Properties.ReadOnly = Status
        chkASESMEN_536.Properties.ReadOnly = Status
        chkASESMEN_537.Properties.ReadOnly = Status
        chkASESMEN_538.Properties.ReadOnly = Status
        chkASESMEN_539.Properties.ReadOnly = Status
        chkASESMEN_540.Properties.ReadOnly = Status
        chkASESMEN_541.Properties.ReadOnly = Status
        chkASESMEN_542.Properties.ReadOnly = Status
        chkASESMEN_543.Properties.ReadOnly = Status
        chkASESMEN_544.Properties.ReadOnly = Status
        chkASESMEN_545.Properties.ReadOnly = Status
        chkASESMEN_546.Properties.ReadOnly = Status
        chkASESMEN_547.Properties.ReadOnly = Status
        chkASESMEN_548.Properties.ReadOnly = Status
        chkASESMEN_549.Properties.ReadOnly = Status
        chkASESMEN_550.Properties.ReadOnly = Status
        chkASESMEN_551.Properties.ReadOnly = Status
        txtASESMEN_552.Properties.ReadOnly = Status
        chkASESMEN_553.Properties.ReadOnly = Status
        chkASESMEN_554.Properties.ReadOnly = Status
        txtASESMEN_555.Properties.ReadOnly = Status
        chkASESMEN_556.Properties.ReadOnly = Status
        chkASESMEN_557.Properties.ReadOnly = Status
        chkASESMEN_558.Properties.ReadOnly = Status
        chkASESMEN_559.Properties.ReadOnly = Status
        txtASESMEN_560.Properties.ReadOnly = Status
        txtASESMEN_561.Properties.ReadOnly = Status
        txtASESMEN_562.Properties.ReadOnly = Status
        txtASESMEN_563.Properties.ReadOnly = Status
        txtASESMEN_564.Properties.ReadOnly = Status
        chkASESMEN_565.Properties.ReadOnly = Status
        chkASESMEN_566.Properties.ReadOnly = Status
        chkASESMEN_567.Properties.ReadOnly = Status
        chkASESMEN_568.Properties.ReadOnly = Status
        txtASESMEN_569.Properties.ReadOnly = Status
        chkASESMEN_570.Properties.ReadOnly = Status
        chkASESMEN_571.Properties.ReadOnly = Status
        chkASESMEN_572.Properties.ReadOnly = Status
        chkASESMEN_573.Properties.ReadOnly = Status
        chkASESMEN_574.Properties.ReadOnly = Status
        chkASESMEN_575.Properties.ReadOnly = Status
        chkASESMEN_576.Properties.ReadOnly = Status
        chkASESMEN_577.Properties.ReadOnly = Status
        chkASESMEN_578.Properties.ReadOnly = Status
        chkASESMEN_579.Properties.ReadOnly = Status
        chkASESMEN_580.Properties.ReadOnly = Status
        chkASESMEN_581.Properties.ReadOnly = Status
        chkASESMEN_582.Properties.ReadOnly = Status
        chkASESMEN_583.Properties.ReadOnly = Status
        chkASESMEN_584.Properties.ReadOnly = Status
        chkASESMEN_585.Properties.ReadOnly = Status
        chkASESMEN_586.Properties.ReadOnly = Status
        chkASESMEN_587.Properties.ReadOnly = Status
        chkASESMEN_588.Properties.ReadOnly = Status
        chkASESMEN_589.Properties.ReadOnly = Status
        chkASESMEN_590.Properties.ReadOnly = Status
        chkASESMEN_591.Properties.ReadOnly = Status
        chkASESMEN_592.Properties.ReadOnly = Status
        chkASESMEN_593.Properties.ReadOnly = Status
        chkASESMEN_594.Properties.ReadOnly = Status
        chkASESMEN_595.Properties.ReadOnly = Status
        chkASESMEN_596.Properties.ReadOnly = Status
        chkASESMEN_597.Properties.ReadOnly = Status
        chkASESMEN_598.Properties.ReadOnly = Status
        chkASESMEN_599.Properties.ReadOnly = Status
        chkASESMEN_600.Properties.ReadOnly = Status
        chkASESMEN_601.Properties.ReadOnly = Status
        chkASESMEN_602.Properties.ReadOnly = Status
        chkASESMEN_603.Properties.ReadOnly = Status
        txtASESMEN_604.Properties.ReadOnly = Status
        txtJAM.Properties.ReadOnly = Status
        txtPASIEN_KELUARGA.Properties.ReadOnly = Status
        TextEdit1.Properties.ReadOnly = Status

        grvDetil.OptionsBehavior.ReadOnly = Status

        'Dim oSetUser As New Setting.clsUser
        'Dim dsUser = oSetUser.GetData(sUserID)
        'If dsUser IsNot Nothing Then
        '    If dsUser.ISOTORTY = True Then
        '        sIsOtority = True
        '        deASESMEN_01.Properties.ReadOnly = False
        '    Else
        '        sIsOtority = False
        '        deASESMEN_01.Properties.ReadOnly = True
        '    End If
        'End If

        'revision
        chkMASALAHKEP_1.Properties.ReadOnly = Status
        chkMASALAHKEP_2.Properties.ReadOnly = Status
        chkMASALAHKEP_3.Properties.ReadOnly = Status
        chkMASALAHKEP_4.Properties.ReadOnly = Status
        chkMASALAHKEP_5.Properties.ReadOnly = Status
        chkMASALAHKEP_6.Properties.ReadOnly = Status
        chkMASALAHKEP_7.Properties.ReadOnly = Status
        chkMASALAHKEP_8.Properties.ReadOnly = Status
        chkMASALAHKEP_9.Properties.ReadOnly = Status
        chkMASALAHKEP_10.Properties.ReadOnly = Status
        chkMASALAHKEP_11.Properties.ReadOnly = Status
        chkMASALAHKEP_12.Properties.ReadOnly = Status
        chkMASALAHKEP_13.Properties.ReadOnly = Status
        chkMASALAHKEP_14.Properties.ReadOnly = Status
        chkMASALAHKEP_15.Properties.ReadOnly = Status
        chkMASALAHKEP_16.Properties.ReadOnly = Status
        chkMASALAHKEP_17.Properties.ReadOnly = Status
        chkMASALAHKEP_18.Properties.ReadOnly = Status
        chkMASALAHKEP_19.Properties.ReadOnly = Status
        chkMASALAHKEP_20.Properties.ReadOnly = Status
        chkMASALAHKEP_21.Properties.ReadOnly = Status
        chkMASALAHKEP_22.Properties.ReadOnly = Status
        chkMASALAHKEP_23.Properties.ReadOnly = Status
        chkMASALAHKEP_24.Properties.ReadOnly = Status
        chkMASALAHKEP_25.Properties.ReadOnly = Status
        chkMASALAHKEP_26.Properties.ReadOnly = Status
        chkMASALAHKEP_27.Properties.ReadOnly = Status
        chkMASALAHKEP_28.Properties.ReadOnly = Status
        chkMASALAHKEP_29.Properties.ReadOnly = Status
        chkMASALAHKEP_30.Properties.ReadOnly = Status
        chkMASALAHKEP_31.Properties.ReadOnly = Status
        chkMASALAHKEP_32.Properties.ReadOnly = Status
        chkMASALAHKEP_33.Properties.ReadOnly = Status
        chkMASALAHKEP_34.Properties.ReadOnly = Status
        chkMASALAHKEP_35.Properties.ReadOnly = Status
        chkMASALAHKEP_36.Properties.ReadOnly = Status
        chkMASALAHKEP_37.Properties.ReadOnly = Status
        chkMASALAHKEP_38.Properties.ReadOnly = Status
        chkMASALAHKEP_39.Properties.ReadOnly = Status
        chkMASALAHKEP_40.Properties.ReadOnly = Status
        chkMASALAHKEP_41.Properties.ReadOnly = Status
        chkMASALAHKEP_42.Properties.ReadOnly = Status
        chkMASALAHKEP_43.Properties.ReadOnly = Status
        chkMASALAHKEP_44.Properties.ReadOnly = Status
        txtMASALAHKEP_44_TEXT.Properties.ReadOnly = Status

        'tambahan
        chkPOLANAFAS.Properties.ReadOnly = Status
        chkRESIKOASPIRASI.Properties.ReadOnly = Status
        chkRESIKOPENURUNANCURAHJANTUNG.Properties.ReadOnly = Status
        chkRESIKOPENDARAHAN.Properties.ReadOnly = Status
        chkBERATBADANBERLEBIH.Properties.ReadOnly = Status
        chkDISFUNGSIMGAS.Properties.ReadOnly = Status
        chkKESIAPANPNUTRISI.Properties.ReadOnly = Status
        chkMENYUSUITIDAKEFEKTIF.Properties.ReadOnly = Status
        chkINURIN.Properties.ReadOnly = Status
        chkINFEKAL.Properties.ReadOnly = Status
        chkPENINGKATANTIDUR.Properties.ReadOnly = Status
        chkGANGGUANMEMORI.Properties.ReadOnly = Status
        chkGANGGUANMENELAN.Properties.ReadOnly = Status
        chkKONFUSIAKUT.Properties.ReadOnly = Status
        chkKONFUSIKROSNIS.Properties.ReadOnly = Status
        chkPOLASEKSUALTE.Properties.ReadOnly = Status
        chkNYERIMELAHIRKAN.Properties.ReadOnly = Status
        chkHARGADIRIRENDAH.Properties.ReadOnly = Status
        chkKESIAPANKOPING.Properties.ReadOnly = Status
        chkSINDROMPASCATRAUMA.Properties.ReadOnly = Status
        chkGANGGUANPERTUMBUHAN.Properties.ReadOnly = Status
        chkGANGGUANPERKEMBANGAN.Properties.ReadOnly = Status
        chkKPMANAJEMENKES.Properties.ReadOnly = Status
        chkKESIAPANPP.Properties.ReadOnly = Status
        chkPEMELIHARAANKES.Properties.ReadOnly = Status
        chkGANGGUANINTERAKSISOSIAL.Properties.ReadOnly = Status
        chkKESIAPANPPKEL.Properties.ReadOnly = Status
        chkRESIKOBUNDIR.Properties.ReadOnly = Status
        chkRISIKOCEDERAPADAIBU.Properties.ReadOnly = Status
        chkCEDERAPADAJANIN.Properties.ReadOnly = Status
        chkRESIKOALERGI.Properties.ReadOnly = Status


        txtSuku.Properties.ReadOnly = Status
    End Sub
    Private Sub fn_EmptyMe()
        deASESMEN_01.DateTime = Now
        txtASESMEN_02.Text = Now.ToString("HH:mm")
        chkASESMEN_03.Checked = False
        chkASESMEN_04.Checked = False
        txtASESMEN_05.ResetText()
        chkASESMEN_06.Checked = False
        chkASESMEN_07.Checked = False
        txtASESMEN_08.ResetText()
        txtASESMEN_09.ResetText()
        chkASESMEN_10.Checked = False
        chkASESMEN_11.Checked = False
        chkASESMEN_12.Checked = False
        chkASESMEN_13.Checked = False
        chkASESMEN_14.Checked = False
        chkASESMEN_15.Checked = False
        chkASESMEN_16.Checked = False
        chkASESMEN_17.Checked = False
        txtASESMEN_18.ResetText()
        txtASESMEN_19.ResetText()
        txtASESMEN_20.ResetText()
        chkASESMEN_21.Checked = False
        chkASESMEN_22.Checked = False
        txtASESMEN_23.ResetText()
        txtASESMEN_24.ResetText()
        chkASESMEN_25.Checked = False
        chkASESMEN_26.Checked = False
        txtASESMEN_27.ResetText()
        txtASESMEN_28.ResetText()
        chkASESMEN_29.Checked = False
        chkASESMEN_30.Checked = False
        txtASESMEN_31.ResetText()
        chkASESMEN_32.Checked = False
        chkASESMEN_33.Checked = False
        chkASESMEN_34.Checked = False
        chkASESMEN_35.Checked = False
        chkASESMEN_36.Checked = False
        chkASESMEN_37.Checked = False
        chkASESMEN_38.Checked = False
        chkASESMEN_39.Checked = False
        txtASESMEN_40.ResetText()
        chkASESMEN_41.Checked = False
        chkASESMEN_42.Checked = False
        chkASESMEN_43.Checked = False
        chkASESMEN_44.Checked = False
        txtASESMEN_45.ResetText()
        chkASESMEN_46.Checked = False
        chkASESMEN_47.Checked = False
        txtASESMEN_48.ResetText()
        chkASESMEN_49.Checked = False
        chkASESMEN_50.Checked = False
        chkASESMEN_51.Checked = False
        txtASESMEN_52.ResetText()
        chkASESMEN_52.Checked = False
        chkASESMEN_53.Checked = False
        chkASESMEN_54.Checked = False
        chkASESMEN_55.Checked = False
        chkASESMEN_56.Checked = False
        chkASESMEN_57.Checked = False
        chkASESMEN_58.Checked = False
        chkASESMEN_59.Checked = False
        chkASESMEN_60.Checked = False
        chkASESMEN_61.Checked = False
        chkASESMEN_62.Checked = False
        chkASESMEN_63.Checked = False
        chkASESMEN_64.Checked = False
        chkASESMEN_65.Checked = False
        chkASESMEN_66.Checked = False
        chkASESMEN_67.Checked = False
        chkASESMEN_68.Checked = False
        chkASESMEN_69.Checked = False
        chkASESMEN_70.Checked = False
        chkASESMEN_71.Checked = False
        chkASESMEN_72.Checked = False
        chkASESMEN_73.Checked = False
        chkASESMEN_74.Checked = False
        chkASESMEN_75.Checked = False
        chkASESMEN_76.Checked = False
        chkASESMEN_77.Checked = False
        chkASESMEN_78.Checked = False
        chkASESMEN_79.Checked = False
        chkASESMEN_80.Checked = False
        chkASESMEN_81.Checked = False
        chkASESMEN_82.Checked = False
        chkASESMEN_83.Checked = False
        chkASESMEN_84.Checked = False
        chkASESMEN_85.Checked = False
        chkASESMEN_86.Checked = False
        chkASESMEN_87.Checked = False
        chkASESMEN_88.Checked = False
        chkASESMEN_89.Checked = False
        chkASESMEN_90.Checked = False
        chkASESMEN_91.Checked = False
        chkASESMEN_92.Checked = False
        chkASESMEN_93.Checked = False
        chkASESMEN_94.Checked = False
        chkASESMEN_95.Checked = False
        chkASESMEN_96.Checked = False
        chkASESMEN_97.Checked = False
        chkASESMEN_98.Checked = False
        chkASESMEN_99.Checked = False
        chkASESMEN_100.Checked = False
        chkASESMEN_101.Checked = False
        chkASESMEN_102.Checked = False
        chkASESMEN_103.Checked = False
        chkASESMEN_104.Checked = False
        chkASESMEN_105.Checked = False
        chkASESMEN_106.Checked = False

        DateEdit1.ResetText()
        DateEdit2.ResetText()
        DateEdit3.ResetText()
        DateEdit4.ResetText()
        DateEdit5.ResetText()

        txtASESMEN_108.ResetText()
        txtASESMEN_109.ResetText()
        txtASESMEN_110.ResetText()
        txtASESMEN_111.ResetText()
        txtASESMEN_112.ResetText()
        txtASESMEN_113.ResetText()
        txtASESMEN_114.ResetText()
        txtASESMEN_115.ResetText()
        txtASESMEN_116.ResetText()
        txtASESMEN_117.ResetText()
        txtASESMEN_118.ResetText()
        txtASESMEN_119.ResetText()
        txtASESMEN_120.ResetText()
        txtASESMEN_121.ResetText()
        txtASESMEN_122.ResetText()
        txtASESMEN_123.ResetText()
        txtASESMEN_124.ResetText()
        txtASESMEN_125.ResetText()
        txtASESMEN_126.ResetText()
        txtASESMEN_127.ResetText()
        txtASESMEN_128.ResetText()
        txtASESMEN_129.ResetText()
        txtASESMEN_130.ResetText()
        txtASESMEN_131.ResetText()
        txtASESMEN_132.ResetText()
        txtASESMEN_133.ResetText()
        txtASESMEN_134.ResetText()
        txtASESMEN_135.ResetText()
        txtASESMEN_136.ResetText()
        txtASESMEN_137.ResetText()
        txtASESMEN_138.ResetText()
        txtASESMEN_139.ResetText()
        txtASESMEN_140.ResetText()
        txtASESMEN_141.ResetText()
        txtASESMEN_142.ResetText()
        txtASESMEN_143.ResetText()
        txtASESMEN_144.ResetText()
        txtASESMEN_145.ResetText()
        txtASESMEN_146.ResetText()
        txtASESMEN_147.ResetText()

        chkASESMEN_149.Checked = False
        chkASESMEN_150.Checked = False
        txtASESMEN_151.ResetText()
        chkASESMEN_152.Checked = False
        chkASESMEN_153.Checked = False
        txtASESMEN_154.ResetText()
        chkASESMEN_155.Checked = False
        chkASESMEN_156.Checked = False
        txtASESMEN_157.ResetText()
        chkASESMEN_158.Checked = False
        chkASESMEN_159.Checked = False
        txtASESMEN_160.ResetText()
        chkASESMEN_161.Checked = False
        chkASESMEN_162.Checked = False
        txtASESMEN_163.ResetText()
        chkASESMEN_164.Checked = False
        chkASESMEN_165.Checked = False
        txtASESMEN_166.ResetText()
        chkASESMEN_167.Checked = False
        chkASESMEN_168.Checked = False
        txtASESMEN_169.ResetText()
        chkASESMEN_170.Checked = False
        chkASESMEN_171.Checked = False
        txtASESMEN_172.ResetText()
        chkASESMEN_173.Checked = False
        chkASESMEN_174.Checked = False
        txtASESMEN_175.ResetText()
        chkASESMEN_176.Checked = False
        txtASESMEN_177.ResetText()
        chkASESMEN_178.Checked = False
        txtASESMEN_179.ResetText()
        chkASESMEN_180.Checked = False
        txtASESMEN_181.ResetText()
        chkASESMEN_182.Checked = False
        txtASESMEN_183.ResetText()
        chkASESMEN_184.Checked = False
        txtASESMEN_185.ResetText()
        chkASESMEN_186.Checked = False
        txtASESMEN_187.ResetText()
        chkASESMEN_188.Checked = False
        txtASESMEN_189.ResetText()
        chkASESMEN_190.Checked = False
        txtASESMEN_191.ResetText()
        txtASESMEN_192.ResetText()

        txtASESMEN_193.ResetText()
        txtASESMEN_194.ResetText()
        txtASESMEN_195.ResetText()
        txtASESMEN_196.ResetText()
        txtASESMEN_197.ResetText()
        txtASESMEN_198.ResetText()
        txtASESMEN_199.ResetText()

        txtASESMEN_200.ResetText()
        txtASESMEN_201.ResetText()
        txtASESMEN_202.ResetText()
        txtASESMEN_203.ResetText()
        txtASESMEN_204.ResetText()
        txtASESMEN_205.ResetText()
        txtASESMEN_206.ResetText()
        txtASESMEN_207.ResetText()
        txtASESMEN_208.ResetText()
        txtASESMEN_209.ResetText()
        txtASESMEN_210.ResetText()
        txtASESMEN_211.ResetText()
        txtASESMEN_212.ResetText()
        txtASESMEN_213.ResetText()
        txtASESMEN_214.ResetText()
        txtASESMEN_215.ResetText()
        txtASESMEN_216.ResetText()
        txtASESMEN_217.ResetText()
        txtASESMEN_218.ResetText()
        txtASESMEN_219.ResetText()
        txtASESMEN_220.ResetText()
        txtASESMEN_221.ResetText()
        txtASESMEN_222.ResetText()
        txtASESMEN_223.ResetText()
        txtASESMEN_224.ResetText()
        txtASESMEN_225.ResetText()
        txtASESMEN_226.ResetText()
        txtASESMEN_227.ResetText()
        txtASESMEN_228.ResetText()
        txtASESMEN_229.ResetText()
        txtASESMEN_230.ResetText()
        txtASESMEN_231.ResetText()
        txtASESMEN_232.ResetText()
        txtASESMEN_233.ResetText()
        txtASESMEN_234.ResetText()
        txtASESMEN_235.ResetText()
        txtASESMEN_236.ResetText()
        txtASESMEN_237.ResetText()
        txtASESMEN_238.ResetText()
        txtASESMEN_239.ResetText()
        txtASESMEN_240.ResetText()
        txtASESMEN_241.ResetText()
        txtASESMEN_242.ResetText()
        txtASESMEN_243.ResetText()
        txtASESMEN_244.ResetText()
        txtASESMEN_245.ResetText()
        txtASESMEN_246.ResetText()
        txtASESMEN_247.ResetText()
        txtASESMEN_248.ResetText()

        'monitoring v2
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
        CheckEdit21.Checked = False
        CheckEdit22.Checked = False
        CheckEdit23.Checked = False
        CheckEdit24.Checked = False
        CheckEdit25.Checked = False
        CheckEdit26.Checked = False
        CheckEdit27.Checked = False
        CheckEdit28.Checked = False
        CheckEdit29.Checked = False
        CheckEdit30.Checked = False
        CheckEdit31.Checked = False
        CheckEdit32.Checked = False
        CheckEdit33.Checked = False
        CheckEdit34.Checked = False
        CheckEdit35.Checked = False
        CheckEdit36.Checked = False
        CheckEdit37.Checked = False
        CheckEdit38.Checked = False
        CheckEdit39.Checked = False
        CheckEdit40.Checked = False
        CheckEdit41.Checked = False
        CheckEdit43.Checked = False
        CheckEdit44.Checked = False
        CheckEdit45.Checked = False
        CheckEdit46.Checked = False
        CheckEdit47.Checked = False
        CheckEdit48.Checked = False
        CheckEdit49.Checked = False
        CheckEdit50.Checked = False


        txtASESMEN_249.ResetText()
        txtASESMEN_250.ResetText()
        txtASESMEN_251.ResetText()
        txtASESMEN_252.ResetText()
        txtASESMEN_253.ResetText()
        txtASESMEN_254.ResetText()
        txtASESMEN_255.ResetText()
        txtASESMEN_256.ResetText()
        txtASESMEN_257.ResetText()
        txtASESMEN_258.ResetText()
        txtASESMEN_259.ResetText()
        txtASESMEN_260.ResetText()
        chkASESMEN_261.Checked = False
        chkASESMEN_262.Checked = False
        chkASESMEN_263.Checked = False
        chkASESMEN_264.Checked = False
        txtASESMEN_265.ResetText()
        txtASESMEN_266.ResetText()
        txtASESMEN_267.ResetText()
        txtASESMEN_268.ResetText()
        txtASESMEN_269.ResetText()
        txtASESMEN_270.ResetText()
        chkASESMEN_271.Checked = False
        chkASESMEN_272.Checked = False
        chkASESMEN_273.Checked = False
        chkASESMEN_274.Checked = False
        chkASESMEN_275.Checked = False
        chkASESMEN_276.Checked = False
        chkASESMEN_277.Checked = False
        chkASESMEN_278.Checked = False
        chkASESMEN_279.Checked = False
        chkASESMEN_280.Checked = False
        txtASESMEN_281.ResetText()
        chkASESMEN_282.Checked = False
        chkASESMEN_283.Checked = False
        chkASESMEN_284.Checked = False
        chkASESMEN_285.Checked = False
        chkASESMEN_286.Checked = False
        chkASESMEN_287.Checked = False
        chkASESMEN_288.Checked = False
        chkASESMEN_289.Checked = False
        chkASESMEN_290.Checked = False
        chkASESMEN_291.Checked = False
        chkASESMEN_292.Checked = False
        txtASESMEN_293.ResetText()
        chkASESMEN_294.Checked = False
        chkASESMEN_295.Checked = False
        chkASESMEN_296.Checked = False
        chkASESMEN_297.Checked = False
        txtASESMEN_298.ResetText()
        chkASESMEN_299.Checked = False
        chkASESMEN_300.Checked = False
        chkASESMEN_301.Checked = False
        chkASESMEN_302.Checked = False
        txtASESMEN_303.ResetText()
        chkASESMEN_304.Checked = False
        txtASESMEN_305.ResetText()
        chkASESMEN_306.Checked = False
        chkASESMEN_307.Checked = False
        chkASESMEN_308.Checked = False
        chkASESMEN_309.Checked = False
        chkASESMEN_310.Checked = False
        chkASESMEN_311.Checked = False
        chkASESMEN_312.Checked = False
        chkASESMEN_313.Checked = False
        chkASESMEN_314.Checked = False
        chkASESMEN_315.Checked = False
        chkASESMEN_316.Checked = False
        chkASESMEN_317.Checked = False
        chkASESMEN_318.Checked = False
        chkASESMEN_319.Checked = False
        chkASESMEN_320.Checked = False
        chkASESMEN_321.Checked = False
        chkASESMEN_322.Checked = False
        chkASESMEN_323.Checked = False
        chkASESMEN_324.Checked = False
        chkASESMEN_325.Checked = False
        chkASESMEN_326.Checked = False
        chkASESMEN_327.Checked = False
        chkASESMEN_328.Checked = False
        chkASESMEN_329.Checked = False
        chkASESMEN_330.Checked = False
        chkASESMEN_331.Checked = False
        chkASESMEN_332.Checked = False
        chkASESMEN_333.Checked = False
        chkASESMEN_334.Checked = False
        chkASESMEN_335.Checked = False
        chkASESMEN_336.Checked = False
        chkASESMEN_337.Checked = False
        chkASESMEN_338.Checked = False
        chkASESMEN_339.Checked = False
        chkASESMEN_340.Checked = False
        chkASESMEN_341.Checked = False
        chkASESMEN_342.Checked = False
        chkASESMEN_343.Checked = False
        chkASESMEN_344.Checked = False
        chkASESMEN_345.Checked = False
        chkASESMEN_346.Checked = False
        chkASESMEN_347.Checked = False
        chkASESMEN_348.Checked = False
        chkASESMEN_349.Checked = False
        chkASESMEN_350.Checked = False
        chkASESMEN_351.Checked = False
        chkASESMEN_352.Checked = False
        chkASESMEN_353.Checked = False
        chkASESMEN_354.Checked = False
        chkASESMEN_355.Checked = False
        chkASESMEN_356.Checked = False
        chkASESMEN_357.Checked = False
        chkASESMEN_358.Checked = False
        chkASESMEN_359.Checked = False
        txtASESMEN_360.ResetText()
        chkASESMEN_361.Checked = False
        chkASESMEN_362.Checked = False
        chkASESMEN_363.Checked = False
        chkASESMEN_364.Checked = False
        chkASESMEN_365.Checked = False
        chkASESMEN_366.Checked = False
        chkASESMEN_367.Checked = False
        chkASESMEN_368.Checked = False
        chkASESMEN_369.Checked = False
        chkASESMEN_370.Checked = False
        chkASESMEN_371.Checked = False
        chkASESMEN_372.Checked = False
        chkASESMEN_373.Checked = False
        chkASESMEN_374.Checked = False
        chkASESMEN_375.Checked = False
        chkASESMEN_376.Checked = False
        chkASESMEN_377.Checked = False
        chkASESMEN_378.Checked = False
        chkASESMEN_379.Checked = False
        chkASESMEN_380.Checked = False
        chkASESMEN_381.Checked = False
        chkASESMEN_382.Checked = False
        chkASESMEN_383.Checked = False
        chkASESMEN_384.Checked = False
        chkASESMEN_385.Checked = False
        chkASESMEN_386.Checked = False
        chkASESMEN_387.Checked = False
        txtASESMEN_388.ResetText()
        chkASESMEN_389.Checked = False
        chkASESMEN_390.Checked = False
        chkASESMEN_391.Checked = False
        chkASESMEN_392.Checked = False
        chkASESMEN_393.Checked = False
        chkASESMEN_394.Checked = False
        chkASESMEN_395.Checked = False
        chkASESMEN_396.Checked = False
        chkASESMEN_397.Checked = False
        chkASESMEN_398.Checked = False
        chkASESMEN_399.Checked = False
        chkASESMEN_400.Checked = False
        txtASESMEN_401.ResetText()
        txtASESMEN_402.ResetText()
        chkASESMEN_403.Checked = False
        chkASESMEN_404.Checked = False
        chkASESMEN_405.Checked = False
        chkASESMEN_406.Checked = False
        chkASESMEN_407.Checked = False
        txtASESMEN_408.ResetText()
        chkASESMEN_409.Checked = False
        chkASESMEN_410.Checked = False
        chkASESMEN_411.Checked = False
        chkASESMEN_412.Checked = False
        chkASESMEN_413.Checked = False
        chkASESMEN_414.Checked = False
        chkASESMEN_415.Checked = False
        chkASESMEN_416.Checked = False
        chkASESMEN_417.Checked = False
        chkASESMEN_418.Checked = False
        chkASESMEN_419.Checked = False
        chkASESMEN_420.Checked = False
        chkASESMEN_421.Checked = False
        chkASESMEN_422.Checked = False
        chkASESMEN_423.Checked = False
        chkASESMEN_424.Checked = False
        chkASESMEN_425.Checked = False
        chkASESMEN_426.Checked = False
        txtASESMEN_427.ResetText()
        chkASESMEN_428.Checked = False
        txtASESMEN_429.ResetText()
        txtASESMEN_430.ResetText()
        txtASESMEN_431.ResetText()
        txtASESMEN_432.ResetText()
        txtASESMEN_433.ResetText()
        chkASESMEN_434.Checked = False
        chkASESMEN_435.Checked = False
        txtASESMEN_436.ResetText()
        chkASESMEN_437.Checked = False
        chkASESMEN_438.Checked = False
        chkASESMEN_439.Checked = False
        chkASESMEN_440.Checked = False
        chkASESMEN_441.Checked = False
        txtASESMEN_442.ResetText()
        txtASESMEN_443.ResetText()
        chkASESMEN_444.Checked = False
        chkASESMEN_445.Checked = False
        chkASESMEN_446.Checked = False
        chkASESMEN_447.Checked = False
        chkASESMEN_448.Checked = False
        chkASESMEN_449.Checked = False
        chkASESMEN_450.Checked = False
        chkASESMEN_451.Checked = False
        chkASESMEN_452.Checked = False
        chkASESMEN_453.Checked = False
        chkASESMEN_454.Checked = False
        chkASESMEN_455.Checked = False
        chkASESMEN_456.Checked = False
        chkASESMEN_457.Checked = False
        chkASESMEN_458.Checked = False
        txtASESMEN_459.ResetText()
        chkASESMEN_460.Checked = False
        chkASESMEN_461.Checked = False
        chkASESMEN_462.Checked = False
        chkASESMEN_463.Checked = False
        chkASESMEN_464.Checked = False
        txtASESMEN_465.ResetText()
        chkASESMEN_466.Checked = False
        chkASESMEN_467.Checked = False
        chkASESMEN_468.Checked = False
        chkASESMEN_469.Checked = False
        txtASESMEN_470.ResetText()
        chkASESMEN_471.Checked = False
        chkASESMEN_472.Checked = False
        chkASESMEN_473.Checked = False
        chkASESMEN_474.Checked = False
        chkASESMEN_475.Checked = False
        chkASESMEN_476.Checked = False
        chkASESMEN_477.Checked = False
        chkASESMEN_478.Checked = False
        chkASESMEN_479.Checked = False
        chkASESMEN_480.Checked = False
        chkASESMEN_481.Checked = False
        chkASESMEN_482.Checked = False
        chkASESMEN_483.Checked = False
        chkASESMEN_484.Checked = False
        txtASESMEN_485.ResetText()
        chkASESMEN_486.Checked = False
        chkASESMEN_487.Checked = False
        chkASESMEN_488.Checked = False
        chkASESMEN_489.Checked = False
        txtASESMEN_490.SelectedIndex = 0
        txtASESMEN_491.SelectedIndex = 0
        txtASESMEN_492.SelectedIndex = 0
        txtASESMEN_493.SelectedIndex = 0
        txtASESMEN_494.SelectedIndex = 0
        txtASESMEN_495.ResetText()
        txtASESMEN_496.ResetText()
        txtASESMEN_497.ResetText()
        txtASESMEN_498.ResetText()
        txtASESMEN_499.ResetText()
        txtASESMEN_500.ResetText()
        txtASESMEN_501.ResetText()
        txtASESMEN_502.ResetText()
        txtASESMEN_503.ResetText()
        txtASESMEN_504.ResetText()
        txtASESMEN_505.ResetText()
        chkASESMEN_506.Checked = False
        chkASESMEN_507.Checked = False
        chkASESMEN_508.Checked = False
        chkASESMEN_509.Checked = False
        chkASESMEN_510.Checked = False
        chkASESMEN_511.Checked = False
        chkASESMEN_512.Checked = False
        chkASESMEN_513.Checked = False
        chkASESMEN_514.Checked = False
        chkASESMEN_515.Checked = False
        chkASESMEN_516.Checked = False
        chkASESMEN_517.Checked = False
        chkASESMEN_518.Checked = False
        chkASESMEN_519.Checked = False
        chkASESMEN_520.Checked = False
        chkASESMEN_521.Checked = False
        chkASESMEN_522.Checked = False
        chkASESMEN_523.Checked = False
        chkASESMEN_524.Checked = False
        chkASESMEN_525.Checked = False
        chkASESMEN_526.Checked = False
        chkASESMEN_527.Checked = False
        chkASESMEN_528.Checked = False
        chkASESMEN_529.Checked = False
        chkASESMEN_530.Checked = False
        chkASESMEN_531.Checked = False
        chkASESMEN_532.Checked = False
        chkASESMEN_533.Checked = False
        chkASESMEN_534.Checked = False
        chkASESMEN_535.Checked = False
        chkASESMEN_536.Checked = False
        chkASESMEN_537.Checked = False
        chkASESMEN_538.Checked = False
        chkASESMEN_539.Checked = False
        chkASESMEN_540.Checked = False
        chkASESMEN_541.Checked = False
        chkASESMEN_542.Checked = False
        chkASESMEN_543.Checked = False
        chkASESMEN_544.Checked = False
        chkASESMEN_545.Checked = False
        chkASESMEN_546.Checked = False
        chkASESMEN_547.Checked = False
        chkASESMEN_548.Checked = False
        chkASESMEN_549.Checked = False
        chkASESMEN_550.Checked = False
        chkASESMEN_551.Checked = False
        txtASESMEN_552.ResetText()
        chkASESMEN_553.Checked = False
        chkASESMEN_554.Checked = False
        txtASESMEN_555.ResetText()
        chkASESMEN_556.Checked = False
        chkASESMEN_557.Checked = False
        chkASESMEN_558.Checked = False
        chkASESMEN_559.Checked = False
        txtASESMEN_560.ResetText()
        txtASESMEN_561.ResetText()
        txtASESMEN_562.ResetText()
        txtASESMEN_563.ResetText()
        txtASESMEN_564.ResetText()
        chkASESMEN_565.Checked = False
        chkASESMEN_566.Checked = False
        chkASESMEN_567.Checked = False
        chkASESMEN_568.Checked = False
        txtASESMEN_569.ResetText()
        chkASESMEN_570.Checked = False
        chkASESMEN_571.Checked = False
        chkASESMEN_572.Checked = False
        chkASESMEN_573.Checked = False
        chkASESMEN_574.Checked = False
        chkASESMEN_575.Checked = False
        chkASESMEN_576.Checked = False
        chkASESMEN_577.Checked = False
        chkASESMEN_578.Checked = False
        chkASESMEN_579.Checked = False
        chkASESMEN_580.Checked = False
        chkASESMEN_581.Checked = False
        chkASESMEN_582.Checked = False
        chkASESMEN_583.Checked = False
        chkASESMEN_584.Checked = False
        chkASESMEN_585.Checked = False
        chkASESMEN_586.Checked = False
        chkASESMEN_587.Checked = False
        chkASESMEN_588.Checked = False
        chkASESMEN_589.Checked = False
        chkASESMEN_590.Checked = False
        chkASESMEN_591.Checked = False
        chkASESMEN_592.Checked = False
        chkASESMEN_593.Checked = False
        chkASESMEN_594.Checked = False
        chkASESMEN_595.Checked = False
        chkASESMEN_596.Checked = False
        chkASESMEN_597.Checked = False
        chkASESMEN_598.Checked = False
        chkASESMEN_599.Checked = False
        chkASESMEN_600.Checked = False
        chkASESMEN_601.Checked = False
        chkASESMEN_602.Checked = False
        chkASESMEN_603.Checked = False
        txtASESMEN_604.ResetText()
        txtJAM.ResetText()
        txtPASIEN_KELUARGA.ResetText()
        TextEdit1.Text = sPERAWAT

        chkMASALAHKEP_1.Checked = False
        chkMASALAHKEP_2.Checked = False
        chkMASALAHKEP_3.Checked = False
        chkMASALAHKEP_4.Checked = False
        chkMASALAHKEP_5.Checked = False
        chkMASALAHKEP_6.Checked = False
        chkMASALAHKEP_7.Checked = False
        chkMASALAHKEP_8.Checked = False
        chkMASALAHKEP_9.Checked = False
        chkMASALAHKEP_10.Checked = False
        chkMASALAHKEP_11.Checked = False
        chkMASALAHKEP_12.Checked = False
        chkMASALAHKEP_13.Checked = False
        chkMASALAHKEP_14.Checked = False
        chkMASALAHKEP_15.Checked = False
        chkMASALAHKEP_16.Checked = False
        chkMASALAHKEP_17.Checked = False
        chkMASALAHKEP_18.Checked = False
        chkMASALAHKEP_19.Checked = False
        chkMASALAHKEP_20.Checked = False
        chkMASALAHKEP_21.Checked = False
        chkMASALAHKEP_22.Checked = False
        chkMASALAHKEP_23.Checked = False
        chkMASALAHKEP_24.Checked = False
        chkMASALAHKEP_25.Checked = False
        chkMASALAHKEP_26.Checked = False
        chkMASALAHKEP_27.Checked = False
        chkMASALAHKEP_28.Checked = False
        chkMASALAHKEP_29.Checked = False
        chkMASALAHKEP_30.Checked = False
        chkMASALAHKEP_31.Checked = False
        chkMASALAHKEP_32.Checked = False
        chkMASALAHKEP_33.Checked = False
        chkMASALAHKEP_34.Checked = False
        chkMASALAHKEP_35.Checked = False
        chkMASALAHKEP_36.Checked = False
        chkMASALAHKEP_37.Checked = False
        chkMASALAHKEP_38.Checked = False
        chkMASALAHKEP_39.Checked = False
        chkMASALAHKEP_40.Checked = False
        chkMASALAHKEP_41.Checked = False
        chkMASALAHKEP_42.Checked = False
        chkMASALAHKEP_43.Checked = False
        chkMASALAHKEP_44.Checked = False
        txtMASALAHKEP_44_TEXT.ResetText()

        chkPOLANAFAS.Checked = False
        chkRESIKOASPIRASI.Checked = False
        chkRESIKOPENURUNANCURAHJANTUNG.Checked = False
        chkRESIKOPENDARAHAN.Checked = False
        chkBERATBADANBERLEBIH.Checked = False
        chkDISFUNGSIMGAS.Checked = False
        chkKESIAPANPNUTRISI.Checked = False
        chkMENYUSUITIDAKEFEKTIF.Checked = False
        chkINURIN.Checked = False
        chkINFEKAL.Checked = False
        chkPENINGKATANTIDUR.Checked = False
        chkGANGGUANMEMORI.Checked = False
        chkGANGGUANMENELAN.Checked = False
        chkKONFUSIAKUT.Checked = False
        chkKONFUSIKROSNIS.Checked = False
        chkPOLASEKSUALTE.Checked = False
        chkNYERIMELAHIRKAN.Checked = False
        chkHARGADIRIRENDAH.Checked = False
        chkKESIAPANKOPING.Checked = False
        chkSINDROMPASCATRAUMA.Checked = False
        chkGANGGUANPERTUMBUHAN.Checked = False
        chkGANGGUANPERKEMBANGAN.Checked = False
        chkKPMANAJEMENKES.Checked = False
        chkKESIAPANPP.Checked = False
        chkPEMELIHARAANKES.Checked = False
        chkGANGGUANINTERAKSISOSIAL.Checked = False
        chkKESIAPANPPKEL.Checked = False
        chkRESIKOBUNDIR.Checked = False
        chkRISIKOCEDERAPADAIBU.Checked = False
        chkCEDERAPADAJANIN.Checked = False
        chkRESIKOALERGI.Checked = False

        txtSuku.ResetText()
    End Sub
    Private Sub fn_LoadData()
        Try
            ' ***** HEADER *****
            Dim ds = oDigital.GetData(txtNoRegister.Text)

            With ds
                deASESMEN_01.DateTime = .ASESMEN_01
                txtASESMEN_02.Text = .ASESMEN_02
                chkASESMEN_03.Checked = .ASESMEN_03
                chkASESMEN_04.Checked = .ASESMEN_04
                txtASESMEN_05.Text = .ASESMEN_05
                chkASESMEN_06.Checked = .ASESMEN_06
                chkASESMEN_07.Checked = .ASESMEN_07
                txtASESMEN_08.Text = .ASESMEN_08
                txtASESMEN_09.Text = .ASESMEN_09
                chkASESMEN_10.Checked = .ASESMEN_10
                chkASESMEN_11.Checked = .ASESMEN_11
                chkASESMEN_12.Checked = .ASESMEN_12
                chkASESMEN_13.Checked = .ASESMEN_13
                chkASESMEN_14.Checked = .ASESMEN_14
                chkASESMEN_15.Checked = .ASESMEN_15
                chkASESMEN_16.Checked = .ASESMEN_16
                chkASESMEN_17.Checked = .ASESMEN_17
                txtASESMEN_18.Text = .ASESMEN_18
                txtASESMEN_19.Text = .ASESMEN_19
                txtASESMEN_20.Text = .ASESMEN_20
                chkASESMEN_21.Checked = .ASESMEN_21
                chkASESMEN_22.Checked = .ASESMEN_22
                txtASESMEN_23.Text = .ASESMEN_23
                txtASESMEN_24.Text = .ASESMEN_24
                chkASESMEN_25.Checked = .ASESMEN_25
                chkASESMEN_26.Checked = .ASESMEN_26
                txtASESMEN_27.Text = .ASESMEN_27
                txtASESMEN_28.Text = .ASESMEN_28
                chkASESMEN_29.Checked = .ASESMEN_29
                chkASESMEN_30.Checked = .ASESMEN_30
                txtASESMEN_31.Text = .ASESMEN_31
                chkASESMEN_32.Checked = .ASESMEN_32
                chkASESMEN_33.Checked = .ASESMEN_33
                chkASESMEN_34.Checked = .ASESMEN_34
                chkASESMEN_35.Checked = .ASESMEN_35
                chkASESMEN_36.Checked = .ASESMEN_36
                chkASESMEN_37.Checked = .ASESMEN_37
                chkASESMEN_38.Checked = .ASESMEN_38
                chkASESMEN_39.Checked = .ASESMEN_39
                txtASESMEN_40.Text = .ASESMEN_40
                chkASESMEN_41.Checked = .ASESMEN_41
                chkASESMEN_42.Checked = .ASESMEN_42
                chkASESMEN_43.Checked = .ASESMEN_43
                chkASESMEN_44.Checked = .ASESMEN_44
                txtASESMEN_45.Text = .ASESMEN_45
                chkASESMEN_46.Checked = .ASESMEN_46
                chkASESMEN_47.Checked = .ASESMEN_47
                txtASESMEN_48.Text = .ASESMEN_48
                chkASESMEN_49.Checked = .ASESMEN_49
                chkASESMEN_50.Checked = .ASESMEN_50
                chkASESMEN_51.Checked = .ASESMEN_51
                txtASESMEN_52.Text = .ASESMEN_52
                chkASESMEN_52.Checked = .ASESMEN_605
                chkASESMEN_53.Checked = .ASESMEN_53
                chkASESMEN_54.Checked = .ASESMEN_54
                chkASESMEN_55.Checked = .ASESMEN_55
                chkASESMEN_56.Checked = .ASESMEN_56
                chkASESMEN_57.Checked = .ASESMEN_57
                chkASESMEN_58.Checked = .ASESMEN_58
                chkASESMEN_59.Checked = .ASESMEN_59
                chkASESMEN_60.Checked = .ASESMEN_60
                chkASESMEN_61.Checked = .ASESMEN_61
                chkASESMEN_62.Checked = .ASESMEN_62
                chkASESMEN_63.Checked = .ASESMEN_63
                chkASESMEN_64.Checked = .ASESMEN_64
                chkASESMEN_65.Checked = .ASESMEN_65
                chkASESMEN_66.Checked = .ASESMEN_66
                chkASESMEN_67.Checked = .ASESMEN_67
                chkASESMEN_68.Checked = .ASESMEN_68
                chkASESMEN_69.Checked = .ASESMEN_69
                chkASESMEN_70.Checked = .ASESMEN_70
                chkASESMEN_71.Checked = .ASESMEN_71
                chkASESMEN_72.Checked = .ASESMEN_72
                chkASESMEN_73.Checked = .ASESMEN_73
                chkASESMEN_74.Checked = .ASESMEN_74
                chkASESMEN_75.Checked = .ASESMEN_75
                chkASESMEN_76.Checked = .ASESMEN_76
                chkASESMEN_77.Checked = .ASESMEN_77
                chkASESMEN_78.Checked = .ASESMEN_78
                chkASESMEN_79.Checked = .ASESMEN_79
                chkASESMEN_80.Checked = .ASESMEN_80
                chkASESMEN_81.Checked = .ASESMEN_81
                chkASESMEN_82.Checked = .ASESMEN_82
                chkASESMEN_83.Checked = .ASESMEN_83
                chkASESMEN_84.Checked = .ASESMEN_84
                chkASESMEN_85.Checked = .ASESMEN_85
                chkASESMEN_86.Checked = .ASESMEN_86
                chkASESMEN_87.Checked = .ASESMEN_87
                chkASESMEN_88.Checked = .ASESMEN_88
                chkASESMEN_89.Checked = .ASESMEN_89
                chkASESMEN_90.Checked = .ASESMEN_90
                chkASESMEN_91.Checked = .ASESMEN_91
                chkASESMEN_92.Checked = .ASESMEN_92
                chkASESMEN_93.Checked = .ASESMEN_93
                chkASESMEN_94.Checked = .ASESMEN_94
                chkASESMEN_95.Checked = .ASESMEN_95
                chkASESMEN_96.Checked = .ASESMEN_96
                chkASESMEN_97.Checked = .ASESMEN_97
                chkASESMEN_98.Checked = .ASESMEN_98
                chkASESMEN_99.Checked = .ASESMEN_99
                chkASESMEN_100.Checked = .ASESMEN_100
                chkASESMEN_101.Checked = .ASESMEN_101
                chkASESMEN_102.Checked = .ASESMEN_102
                chkASESMEN_103.Checked = .ASESMEN_103
                chkASESMEN_104.Checked = .ASESMEN_104
                chkASESMEN_105.Checked = .ASESMEN_105
                chkASESMEN_106.Checked = .ASESMEN_106


                DateEdit1.Text = .TANGGALRESIKOJATUH1
                DateEdit2.Text = .TANGGALRESIKOJATUH2
                DateEdit3.Text = .TANGGALRESIKOJATUH3
                DateEdit4.Text = .TANGGALRESIKOJATUH4
                DateEdit5.Text = .TANGGALRESIKOJATUH5


                txtASESMEN_108.Text = .ASESMEN_108
                txtASESMEN_109.Text = .ASESMEN_109
                txtASESMEN_110.Text = .ASESMEN_110
                txtASESMEN_111.Text = .ASESMEN_111
                txtASESMEN_112.Text = .ASESMEN_112
                txtASESMEN_113.Text = .ASESMEN_113
                txtASESMEN_114.Text = .ASESMEN_114
                txtASESMEN_115.Text = .ASESMEN_115
                txtASESMEN_116.Text = .ASESMEN_116
                txtASESMEN_117.Text = .ASESMEN_117
                txtASESMEN_118.Text = .ASESMEN_118
                txtASESMEN_119.Text = .ASESMEN_119
                txtASESMEN_120.Text = .ASESMEN_120
                txtASESMEN_121.Text = .ASESMEN_121
                txtASESMEN_122.Text = .ASESMEN_122
                txtASESMEN_123.Text = .ASESMEN_123
                txtASESMEN_124.Text = .ASESMEN_124
                txtASESMEN_125.Text = .ASESMEN_125
                txtASESMEN_126.Text = .ASESMEN_126
                txtASESMEN_127.Text = .ASESMEN_127
                txtASESMEN_128.Text = .ASESMEN_128
                txtASESMEN_129.Text = .ASESMEN_129
                txtASESMEN_130.Text = .ASESMEN_130
                txtASESMEN_131.Text = .ASESMEN_131
                txtASESMEN_132.Text = .ASESMEN_132
                txtASESMEN_133.Text = .ASESMEN_133
                txtASESMEN_134.Text = .ASESMEN_134
                txtASESMEN_135.Text = .ASESMEN_135
                txtASESMEN_136.Text = .ASESMEN_136
                txtASESMEN_137.Text = .ASESMEN_137
                txtASESMEN_138.Text = .ASESMEN_138
                txtASESMEN_139.Text = .ASESMEN_139
                txtASESMEN_140.Text = .ASESMEN_140
                txtASESMEN_141.Text = .ASESMEN_141
                txtASESMEN_142.Text = .ASESMEN_142
                txtASESMEN_143.Text = .ASESMEN_143
                txtASESMEN_144.Text = .ASESMEN_144
                txtASESMEN_145.Text = .ASESMEN_145
                txtASESMEN_146.Text = .ASESMEN_146
                txtASESMEN_147.Text = .ASESMEN_147

                chkASESMEN_149.Checked = .ASESMEN_149
                chkASESMEN_150.Checked = .ASESMEN_150
                txtASESMEN_151.Text = .ASESMEN_151
                chkASESMEN_152.Checked = .ASESMEN_152
                chkASESMEN_153.Checked = .ASESMEN_153
                txtASESMEN_154.Text = .ASESMEN_154
                chkASESMEN_155.Checked = .ASESMEN_155
                chkASESMEN_156.Checked = .ASESMEN_156
                txtASESMEN_157.Text = .ASESMEN_157
                chkASESMEN_158.Checked = .ASESMEN_158
                chkASESMEN_159.Checked = .ASESMEN_159
                txtASESMEN_160.Text = .ASESMEN_160
                chkASESMEN_161.Checked = .ASESMEN_161
                chkASESMEN_162.Checked = .ASESMEN_162
                txtASESMEN_163.Text = .ASESMEN_163
                chkASESMEN_164.Checked = .ASESMEN_164
                chkASESMEN_165.Checked = .ASESMEN_165
                txtASESMEN_166.Text = .ASESMEN_166
                chkASESMEN_167.Checked = .ASESMEN_167
                chkASESMEN_168.Checked = .ASESMEN_168
                txtASESMEN_169.Text = .ASESMEN_169
                chkASESMEN_170.Checked = .ASESMEN_170
                chkASESMEN_171.Checked = .ASESMEN_171
                txtASESMEN_172.Text = .ASESMEN_172
                chkASESMEN_173.Checked = .ASESMEN_173
                chkASESMEN_174.Checked = .ASESMEN_174
                txtASESMEN_175.Text = .ASESMEN_175
                chkASESMEN_176.Checked = .ASESMEN_176
                txtASESMEN_177.Text = .ASESMEN_177
                chkASESMEN_178.Checked = .ASESMEN_178
                txtASESMEN_179.Text = .ASESMEN_179

                chkASESMEN_180.Checked = .ASESMEN_180
                txtASESMEN_181.Text = .ASESMEN_181
                chkASESMEN_182.Checked = .ASESMEN_182
                txtASESMEN_183.Text = .ASESMEN_183
                chkASESMEN_184.Checked = .ASESMEN_184
                txtASESMEN_185.Text = .ASESMEN_185
                chkASESMEN_186.Checked = .ASESMEN_186
                txtASESMEN_187.Text = .ASESMEN_187
                chkASESMEN_188.Checked = .ASESMEN_188
                txtASESMEN_189.Text = .ASESMEN_189
                chkASESMEN_190.Checked = .ASESMEN_190
                txtASESMEN_191.Text = .ASESMEN_191
                txtASESMEN_192.Text = .ASESMEN_192

                txtASESMEN_193.Text = .ASESMEN_193
                txtASESMEN_194.Text = .ASESMEN_194
                txtASESMEN_195.Text = .ASESMEN_195
                txtASESMEN_196.Text = .ASESMEN_196
                txtASESMEN_197.Text = .ASESMEN_197
                txtASESMEN_198.Text = .ASESMEN_198
                txtASESMEN_199.Text = .ASESMEN_199

                txtASESMEN_200.Text = .ASESMEN_200
                txtASESMEN_201.Text = .ASESMEN_201
                txtASESMEN_202.Text = .ASESMEN_202
                txtASESMEN_203.Text = .ASESMEN_203
                txtASESMEN_204.Text = .ASESMEN_204
                txtASESMEN_205.Text = .ASESMEN_205
                txtASESMEN_206.Text = .ASESMEN_206
                txtASESMEN_207.Text = .ASESMEN_207
                txtASESMEN_208.Text = .ASESMEN_208
                txtASESMEN_209.Text = .ASESMEN_209
                txtASESMEN_210.Text = .ASESMEN_210
                txtASESMEN_211.Text = .ASESMEN_211
                txtASESMEN_212.Text = .ASESMEN_212
                txtASESMEN_213.Text = .ASESMEN_213
                txtASESMEN_214.Text = .ASESMEN_214
                txtASESMEN_215.Text = .ASESMEN_215
                txtASESMEN_216.Text = .ASESMEN_216
                txtASESMEN_217.Text = .ASESMEN_217
                txtASESMEN_218.Text = .ASESMEN_218
                txtASESMEN_219.Text = .ASESMEN_219
                txtASESMEN_220.Text = .ASESMEN_220
                txtASESMEN_221.Text = .ASESMEN_221
                txtASESMEN_222.Text = .ASESMEN_222
                txtASESMEN_223.Text = .ASESMEN_223
                txtASESMEN_224.Text = .ASESMEN_224
                txtASESMEN_225.Text = .ASESMEN_225
                txtASESMEN_226.Text = .ASESMEN_226
                txtASESMEN_227.Text = .ASESMEN_227
                txtASESMEN_228.Text = .ASESMEN_228
                txtASESMEN_229.Text = .ASESMEN_229
                txtASESMEN_230.Text = .ASESMEN_230
                txtASESMEN_231.Text = .ASESMEN_231
                txtASESMEN_232.Text = .ASESMEN_232
                txtASESMEN_233.Text = .ASESMEN_233
                txtASESMEN_234.Text = .ASESMEN_234
                txtASESMEN_235.Text = .ASESMEN_235
                txtASESMEN_236.Text = .ASESMEN_236
                txtASESMEN_237.Text = .ASESMEN_237
                txtASESMEN_238.Text = .ASESMEN_238
                txtASESMEN_239.Text = .ASESMEN_239
                txtASESMEN_240.Text = .ASESMEN_240
                txtASESMEN_241.Text = .ASESMEN_241
                txtASESMEN_242.Text = .ASESMEN_242
                txtASESMEN_243.Text = .ASESMEN_243
                txtASESMEN_244.Text = .ASESMEN_244
                txtASESMEN_245.Text = .ASESMEN_245
                txtASESMEN_246.Text = .ASESMEN_246
                txtASESMEN_247.Text = .ASESMEN_247
                txtASESMEN_248.Text = .ASESMEN_248

                'monitoring v2
                CheckEdit1.Checked = .MONITORING0
                CheckEdit2.Checked = .MONITORING1
                CheckEdit3.Checked = .MONITORING2
                CheckEdit4.Checked = .MONITORING3
                CheckEdit5.Checked = .MONITORING4
                CheckEdit6.Checked = .MONITORING5
                CheckEdit7.Checked = .MONITORING6
                CheckEdit8.Checked = .MONITORING7
                CheckEdit9.Checked = .MONITORING8
                CheckEdit10.Checked = .MONITORING9
                CheckEdit11.Checked = .MONITORING10
                CheckEdit12.Checked = .MONITORING11
                CheckEdit13.Checked = .MONITORING12
                CheckEdit14.Checked = .MONITORING13
                CheckEdit15.Checked = .MONITORING14
                CheckEdit16.Checked = .MONITORING15
                CheckEdit17.Checked = .MONITORING16
                CheckEdit18.Checked = .MONITORING17
                CheckEdit19.Checked = .MONITORING18
                CheckEdit20.Checked = .MONITORING19
                CheckEdit21.Checked = .MONITORING20
                CheckEdit22.Checked = .MONITORING21
                CheckEdit23.Checked = .MONITORING22
                CheckEdit24.Checked = .MONITORING23
                CheckEdit25.Checked = .MONITORING24
                CheckEdit26.Checked = .MONITORING25
                CheckEdit27.Checked = .MONITORING26
                CheckEdit28.Checked = .MONITORING27
                CheckEdit29.Checked = .MONITORING28
                CheckEdit30.Checked = .MONITORING29
                CheckEdit31.Checked = .MONITORING30
                CheckEdit32.Checked = .MONITORING31
                CheckEdit33.Checked = .MONITORING32
                CheckEdit34.Checked = .MONITORING33
                CheckEdit35.Checked = .MONITORING34
                CheckEdit36.Checked = .MONITORING35
                CheckEdit37.Checked = .MONITORING36
                CheckEdit38.Checked = .MONITORING37
                CheckEdit39.Checked = .MONITORING38
                CheckEdit40.Checked = .MONITORING39
                CheckEdit41.Checked = .MONITORING40
                CheckEdit43.Checked = .MONITORING41
                CheckEdit44.Checked = .MONITORING42
                CheckEdit45.Checked = .MONITORING43
                CheckEdit46.Checked = .MONITORING44
                CheckEdit47.Checked = .MONITORING45
                CheckEdit48.Checked = .MONITORING46
                CheckEdit49.Checked = .MONITORING47
                CheckEdit50.Checked = .MONITORING48


                txtASESMEN_249.Text = .ASESMEN_249
                txtASESMEN_250.Text = .ASESMEN_250
                txtASESMEN_251.Text = .ASESMEN_251
                txtASESMEN_252.Text = .ASESMEN_252
                txtASESMEN_253.Text = .ASESMEN_253
                txtASESMEN_254.Text = .ASESMEN_254
                txtASESMEN_255.Text = .ASESMEN_255
                txtASESMEN_256.Text = .ASESMEN_256
                txtASESMEN_257.Text = .ASESMEN_257
                txtASESMEN_258.Text = .ASESMEN_258
                txtASESMEN_259.Text = .ASESMEN_259
                txtASESMEN_260.Text = .ASESMEN_260
                chkASESMEN_261.Checked = .ASESMEN_261
                chkASESMEN_262.Checked = .ASESMEN_262
                chkASESMEN_263.Checked = .ASESMEN_263
                chkASESMEN_264.Checked = .ASESMEN_264
                txtASESMEN_265.Text = .ASESMEN_265
                txtASESMEN_266.Text = .ASESMEN_266
                txtASESMEN_267.Text = .ASESMEN_267
                txtASESMEN_268.Text = .ASESMEN_268
                txtASESMEN_269.Text = .ASESMEN_269
                txtASESMEN_270.Text = .ASESMEN_270
                chkASESMEN_271.Checked = .ASESMEN_271
                chkASESMEN_272.Checked = .ASESMEN_272
                chkASESMEN_273.Checked = .ASESMEN_273
                chkASESMEN_274.Checked = .ASESMEN_274
                chkASESMEN_275.Checked = .ASESMEN_275
                chkASESMEN_276.Checked = .ASESMEN_276
                chkASESMEN_277.Checked = .ASESMEN_277
                chkASESMEN_278.Checked = .ASESMEN_278
                chkASESMEN_279.Checked = .ASESMEN_279
                chkASESMEN_280.Checked = .ASESMEN_280
                txtASESMEN_281.Text = .ASESMEN_281
                chkASESMEN_282.Checked = .ASESMEN_282
                chkASESMEN_283.Checked = .ASESMEN_283
                chkASESMEN_284.Checked = .ASESMEN_284
                chkASESMEN_285.Checked = .ASESMEN_285
                chkASESMEN_286.Checked = .ASESMEN_286
                chkASESMEN_287.Checked = .ASESMEN_287
                chkASESMEN_288.Checked = .ASESMEN_288
                chkASESMEN_289.Checked = .ASESMEN_289
                chkASESMEN_290.Checked = .ASESMEN_290
                chkASESMEN_291.Checked = .ASESMEN_291
                chkASESMEN_292.Checked = .ASESMEN_292
                txtASESMEN_293.Text = .ASESMEN_293
                chkASESMEN_294.Checked = .ASESMEN_294
                chkASESMEN_295.Checked = .ASESMEN_295
                chkASESMEN_296.Checked = .ASESMEN_296
                chkASESMEN_297.Checked = .ASESMEN_297
                txtASESMEN_298.Text = .ASESMEN_298
                chkASESMEN_299.Checked = .ASESMEN_299
                chkASESMEN_300.Checked = .ASESMEN_300
                chkASESMEN_301.Checked = .ASESMEN_301
                chkASESMEN_302.Checked = .ASESMEN_302
                txtASESMEN_303.Text = .ASESMEN_303
                chkASESMEN_304.Checked = .ASESMEN_304
                txtASESMEN_305.Text = .ASESMEN_305
                chkASESMEN_306.Checked = .ASESMEN_306
                chkASESMEN_307.Checked = .ASESMEN_307
                chkASESMEN_308.Checked = .ASESMEN_308
                chkASESMEN_309.Checked = .ASESMEN_309
                chkASESMEN_310.Checked = .ASESMEN_310
                chkASESMEN_311.Checked = .ASESMEN_311
                chkASESMEN_312.Checked = .ASESMEN_312
                chkASESMEN_313.Checked = .ASESMEN_313
                chkASESMEN_314.Checked = .ASESMEN_314
                chkASESMEN_315.Checked = .ASESMEN_315
                chkASESMEN_316.Checked = .ASESMEN_316
                chkASESMEN_317.Checked = .ASESMEN_317
                chkASESMEN_318.Checked = .ASESMEN_318
                chkASESMEN_319.Checked = .ASESMEN_319
                chkASESMEN_320.Checked = .ASESMEN_320
                chkASESMEN_321.Checked = .ASESMEN_321
                chkASESMEN_322.Checked = .ASESMEN_322
                chkASESMEN_323.Checked = .ASESMEN_323
                chkASESMEN_324.Checked = .ASESMEN_324
                chkASESMEN_325.Checked = .ASESMEN_325
                chkASESMEN_326.Checked = .ASESMEN_326
                chkASESMEN_327.Checked = .ASESMEN_327
                chkASESMEN_328.Checked = .ASESMEN_328
                chkASESMEN_329.Checked = .ASESMEN_329
                chkASESMEN_330.Checked = .ASESMEN_330
                chkASESMEN_331.Checked = .ASESMEN_331
                chkASESMEN_332.Checked = .ASESMEN_332
                chkASESMEN_333.Checked = .ASESMEN_333
                chkASESMEN_334.Checked = .ASESMEN_334
                chkASESMEN_335.Checked = .ASESMEN_335
                chkASESMEN_336.Checked = .ASESMEN_336
                chkASESMEN_337.Checked = .ASESMEN_337
                chkASESMEN_338.Checked = .ASESMEN_338
                chkASESMEN_339.Checked = .ASESMEN_339
                chkASESMEN_340.Checked = .ASESMEN_340
                chkASESMEN_341.Checked = .ASESMEN_341
                chkASESMEN_342.Checked = .ASESMEN_342
                chkASESMEN_343.Checked = .ASESMEN_343
                chkASESMEN_344.Checked = .ASESMEN_344
                chkASESMEN_345.Checked = .ASESMEN_345
                chkASESMEN_346.Checked = .ASESMEN_346
                chkASESMEN_347.Checked = .ASESMEN_347
                chkASESMEN_348.Checked = .ASESMEN_348
                chkASESMEN_349.Checked = .ASESMEN_349
                chkASESMEN_350.Checked = .ASESMEN_350
                chkASESMEN_351.Checked = .ASESMEN_351
                chkASESMEN_352.Checked = .ASESMEN_352
                chkASESMEN_353.Checked = .ASESMEN_353
                chkASESMEN_354.Checked = .ASESMEN_354
                chkASESMEN_355.Checked = .ASESMEN_355
                chkASESMEN_356.Checked = .ASESMEN_356
                chkASESMEN_357.Checked = .ASESMEN_357
                chkASESMEN_358.Checked = .ASESMEN_358
                chkASESMEN_359.Checked = .ASESMEN_359
                txtASESMEN_360.Text = .ASESMEN_360
                chkASESMEN_361.Checked = .ASESMEN_361
                chkASESMEN_362.Checked = .ASESMEN_362
                chkASESMEN_363.Checked = .ASESMEN_363
                chkASESMEN_364.Checked = .ASESMEN_364
                chkASESMEN_365.Checked = .ASESMEN_365
                chkASESMEN_366.Checked = .ASESMEN_366
                chkASESMEN_367.Checked = .ASESMEN_367
                chkASESMEN_368.Checked = .ASESMEN_368
                chkASESMEN_369.Checked = .ASESMEN_369
                chkASESMEN_370.Checked = .ASESMEN_370
                chkASESMEN_371.Checked = .ASESMEN_371
                chkASESMEN_372.Checked = .ASESMEN_372
                chkASESMEN_373.Checked = .ASESMEN_373
                chkASESMEN_374.Checked = .ASESMEN_374
                chkASESMEN_375.Checked = .ASESMEN_375
                chkASESMEN_376.Checked = .ASESMEN_376
                chkASESMEN_377.Checked = .ASESMEN_377
                chkASESMEN_378.Checked = .ASESMEN_378
                chkASESMEN_379.Checked = .ASESMEN_379
                chkASESMEN_380.Checked = .ASESMEN_380
                chkASESMEN_381.Checked = .ASESMEN_381
                chkASESMEN_382.Checked = .ASESMEN_382
                chkASESMEN_383.Checked = .ASESMEN_383
                chkASESMEN_384.Checked = .ASESMEN_384
                chkASESMEN_385.Checked = .ASESMEN_385
                chkASESMEN_386.Checked = .ASESMEN_386
                chkASESMEN_387.Checked = .ASESMEN_387
                txtASESMEN_388.Text = .ASESMEN_388
                chkASESMEN_389.Checked = .ASESMEN_389
                chkASESMEN_390.Checked = .ASESMEN_390
                chkASESMEN_391.Checked = .ASESMEN_391
                chkASESMEN_392.Checked = .ASESMEN_392
                chkASESMEN_393.Checked = .ASESMEN_393
                chkASESMEN_394.Checked = .ASESMEN_394
                chkASESMEN_395.Checked = .ASESMEN_395
                chkASESMEN_396.Checked = .ASESMEN_396
                chkASESMEN_397.Checked = .ASESMEN_397
                chkASESMEN_398.Checked = .ASESMEN_398
                chkASESMEN_399.Checked = .ASESMEN_399
                chkASESMEN_400.Checked = .ASESMEN_400
                txtASESMEN_401.Text = .ASESMEN_401
                txtASESMEN_402.Text = .ASESMEN_402
                chkASESMEN_403.Checked = .ASESMEN_403
                chkASESMEN_404.Checked = .ASESMEN_404
                chkASESMEN_405.Checked = .ASESMEN_405
                chkASESMEN_406.Checked = .ASESMEN_406
                chkASESMEN_407.Checked = .ASESMEN_407
                txtASESMEN_408.Text = .ASESMEN_408
                chkASESMEN_409.Checked = .ASESMEN_409
                chkASESMEN_410.Checked = .ASESMEN_410
                chkASESMEN_411.Checked = .ASESMEN_411
                chkASESMEN_412.Checked = .ASESMEN_412
                chkASESMEN_413.Checked = .ASESMEN_413
                chkASESMEN_414.Checked = .ASESMEN_414
                chkASESMEN_415.Checked = .ASESMEN_415
                chkASESMEN_416.Checked = .ASESMEN_416
                chkASESMEN_417.Checked = .ASESMEN_417
                chkASESMEN_418.Checked = .ASESMEN_418
                chkASESMEN_419.Checked = .ASESMEN_419
                chkASESMEN_420.Checked = .ASESMEN_420
                chkASESMEN_421.Checked = .ASESMEN_421
                chkASESMEN_422.Checked = .ASESMEN_422
                chkASESMEN_423.Checked = .ASESMEN_423
                chkASESMEN_424.Checked = .ASESMEN_424
                chkASESMEN_425.Checked = .ASESMEN_425
                chkASESMEN_426.Checked = .ASESMEN_426
                txtASESMEN_427.Text = .ASESMEN_427
                chkASESMEN_428.Checked = .ASESMEN_428
                txtASESMEN_429.Text = .ASESMEN_429
                txtASESMEN_430.Text = .ASESMEN_430
                txtASESMEN_431.Text = .ASESMEN_431
                txtASESMEN_432.Text = .ASESMEN_432
                txtASESMEN_433.Text = .ASESMEN_433
                chkASESMEN_434.Checked = .ASESMEN_434
                chkASESMEN_435.Checked = .ASESMEN_435
                txtASESMEN_436.Text = .ASESMEN_436
                chkASESMEN_437.Checked = .ASESMEN_437
                chkASESMEN_438.Checked = .ASESMEN_438
                chkASESMEN_439.Checked = .ASESMEN_439
                chkASESMEN_440.Checked = .ASESMEN_440
                chkASESMEN_441.Checked = .ASESMEN_441
                txtASESMEN_442.Text = .ASESMEN_442
                txtASESMEN_443.Text = .ASESMEN_443
                chkASESMEN_444.Checked = .ASESMEN_444
                chkASESMEN_445.Checked = .ASESMEN_445
                chkASESMEN_446.Checked = .ASESMEN_446
                chkASESMEN_447.Checked = .ASESMEN_447
                chkASESMEN_448.Checked = .ASESMEN_448
                chkASESMEN_449.Checked = .ASESMEN_449
                chkASESMEN_450.Checked = .ASESMEN_450
                chkASESMEN_451.Checked = .ASESMEN_451
                chkASESMEN_452.Checked = .ASESMEN_452
                chkASESMEN_453.Checked = .ASESMEN_453
                chkASESMEN_454.Checked = .ASESMEN_454
                chkASESMEN_455.Checked = .ASESMEN_455
                chkASESMEN_456.Checked = .ASESMEN_456
                chkASESMEN_457.Checked = .ASESMEN_457
                chkASESMEN_458.Checked = .ASESMEN_458
                txtASESMEN_459.Text = .ASESMEN_459
                chkASESMEN_460.Checked = .ASESMEN_460
                chkASESMEN_461.Checked = .ASESMEN_461
                chkASESMEN_462.Checked = .ASESMEN_462
                chkASESMEN_463.Checked = .ASESMEN_463
                chkASESMEN_464.Checked = .ASESMEN_464
                txtASESMEN_465.Text = .ASESMEN_465
                chkASESMEN_466.Checked = .ASESMEN_466
                chkASESMEN_467.Checked = .ASESMEN_467
                chkASESMEN_468.Checked = .ASESMEN_468
                chkASESMEN_469.Checked = .ASESMEN_469
                txtASESMEN_470.Text = .ASESMEN_470
                chkASESMEN_471.Checked = .ASESMEN_471
                chkASESMEN_472.Checked = .ASESMEN_472
                chkASESMEN_473.Checked = .ASESMEN_473
                chkASESMEN_474.Checked = .ASESMEN_474
                chkASESMEN_475.Checked = .ASESMEN_475
                chkASESMEN_476.Checked = .ASESMEN_476
                chkASESMEN_477.Checked = .ASESMEN_477
                chkASESMEN_478.Checked = .ASESMEN_478
                chkASESMEN_479.Checked = .ASESMEN_479
                chkASESMEN_480.Checked = .ASESMEN_480
                chkASESMEN_481.Checked = .ASESMEN_481
                chkASESMEN_482.Checked = .ASESMEN_482
                chkASESMEN_483.Checked = .ASESMEN_483
                chkASESMEN_484.Checked = .ASESMEN_484
                txtASESMEN_485.Text = .ASESMEN_485
                chkASESMEN_486.Checked = .ASESMEN_486
                chkASESMEN_487.Checked = .ASESMEN_487
                chkASESMEN_488.Checked = .ASESMEN_488
                chkASESMEN_489.Checked = .ASESMEN_489
                txtASESMEN_490.Text = .ASESMEN_490
                txtASESMEN_491.Text = .ASESMEN_491
                txtASESMEN_492.Text = .ASESMEN_492
                txtASESMEN_493.Text = .ASESMEN_493
                txtASESMEN_494.Text = .ASESMEN_494
                txtASESMEN_495.Text = .ASESMEN_495
                txtASESMEN_496.Text = .ASESMEN_496
                txtASESMEN_497.Text = .ASESMEN_497
                txtASESMEN_498.Text = .ASESMEN_498
                txtASESMEN_499.Text = .ASESMEN_499
                txtASESMEN_500.Text = .ASESMEN_500
                txtASESMEN_501.Text = .ASESMEN_501
                txtASESMEN_502.Text = .ASESMEN_502
                txtASESMEN_503.Text = .ASESMEN_503
                txtASESMEN_504.Text = .ASESMEN_504
                txtASESMEN_505.Text = .ASESMEN_505
                chkASESMEN_506.Checked = .ASESMEN_506
                chkASESMEN_507.Checked = .ASESMEN_507
                chkASESMEN_508.Checked = .ASESMEN_508
                chkASESMEN_509.Checked = .ASESMEN_509
                chkASESMEN_510.Checked = .ASESMEN_510
                chkASESMEN_511.Checked = .ASESMEN_511
                chkASESMEN_512.Checked = .ASESMEN_512
                chkASESMEN_513.Checked = .ASESMEN_513
                chkASESMEN_514.Checked = .ASESMEN_514
                chkASESMEN_515.Checked = .ASESMEN_515
                chkASESMEN_516.Checked = .ASESMEN_516
                chkASESMEN_517.Checked = .ASESMEN_517
                chkASESMEN_518.Checked = .ASESMEN_518
                chkASESMEN_519.Checked = .ASESMEN_519
                chkASESMEN_520.Checked = .ASESMEN_520
                chkASESMEN_521.Checked = .ASESMEN_521
                chkASESMEN_522.Checked = .ASESMEN_522
                chkASESMEN_523.Checked = .ASESMEN_523
                chkASESMEN_524.Checked = .ASESMEN_524
                chkASESMEN_525.Checked = .ASESMEN_525
                chkASESMEN_526.Checked = .ASESMEN_526
                chkASESMEN_527.Checked = .ASESMEN_527
                chkASESMEN_528.Checked = .ASESMEN_528
                chkASESMEN_529.Checked = .ASESMEN_529
                chkASESMEN_530.Checked = .ASESMEN_530
                chkASESMEN_531.Checked = .ASESMEN_531
                chkASESMEN_532.Checked = .ASESMEN_532
                chkASESMEN_533.Checked = .ASESMEN_533
                chkASESMEN_534.Checked = .ASESMEN_534
                chkASESMEN_535.Checked = .ASESMEN_535
                chkASESMEN_536.Checked = .ASESMEN_536
                chkASESMEN_537.Checked = .ASESMEN_537
                chkASESMEN_538.Checked = .ASESMEN_538
                chkASESMEN_539.Checked = .ASESMEN_539
                chkASESMEN_540.Checked = .ASESMEN_540
                chkASESMEN_541.Checked = .ASESMEN_541
                chkASESMEN_542.Checked = .ASESMEN_542
                chkASESMEN_543.Checked = .ASESMEN_543
                chkASESMEN_544.Checked = .ASESMEN_544
                chkASESMEN_545.Checked = .ASESMEN_545
                chkASESMEN_546.Checked = .ASESMEN_546
                chkASESMEN_547.Checked = .ASESMEN_547
                chkASESMEN_548.Checked = .ASESMEN_548
                chkASESMEN_549.Checked = .ASESMEN_549
                chkASESMEN_550.Checked = .ASESMEN_550
                chkASESMEN_551.Checked = .ASESMEN_551
                txtASESMEN_552.Text = .ASESMEN_552
                chkASESMEN_553.Checked = .ASESMEN_553
                chkASESMEN_554.Checked = .ASESMEN_554
                txtASESMEN_555.Text = .ASESMEN_555
                chkASESMEN_556.Checked = .ASESMEN_556
                chkASESMEN_557.Checked = .ASESMEN_557
                chkASESMEN_558.Checked = .ASESMEN_558
                chkASESMEN_559.Checked = .ASESMEN_559
                txtASESMEN_560.Text = .ASESMEN_560
                txtASESMEN_561.Text = .ASESMEN_561
                txtASESMEN_562.Text = .ASESMEN_562
                txtASESMEN_563.Text = .ASESMEN_563
                txtASESMEN_564.Text = .ASESMEN_564
                chkASESMEN_565.Checked = .ASESMEN_565
                chkASESMEN_566.Checked = .ASESMEN_566
                chkASESMEN_567.Checked = .ASESMEN_567
                chkASESMEN_568.Checked = .ASESMEN_568
                txtASESMEN_569.Text = .ASESMEN_569
                chkASESMEN_570.Checked = .ASESMEN_570
                chkASESMEN_571.Checked = .ASESMEN_571
                chkASESMEN_572.Checked = .ASESMEN_572
                chkASESMEN_573.Checked = .ASESMEN_573
                chkASESMEN_574.Checked = .ASESMEN_574
                chkASESMEN_575.Checked = .ASESMEN_575
                chkASESMEN_576.Checked = .ASESMEN_576
                chkASESMEN_577.Checked = .ASESMEN_577
                chkASESMEN_578.Checked = .ASESMEN_578
                chkASESMEN_579.Checked = .ASESMEN_579
                chkASESMEN_580.Checked = .ASESMEN_580
                chkASESMEN_581.Checked = .ASESMEN_581
                chkASESMEN_582.Checked = .ASESMEN_582
                chkASESMEN_583.Checked = .ASESMEN_583
                chkASESMEN_584.Checked = .ASESMEN_584
                chkASESMEN_585.Checked = .ASESMEN_585
                chkASESMEN_586.Checked = .ASESMEN_586
                chkASESMEN_587.Checked = .ASESMEN_587
                chkASESMEN_588.Checked = .ASESMEN_588
                chkASESMEN_589.Checked = .ASESMEN_589
                chkASESMEN_590.Checked = .ASESMEN_590
                chkASESMEN_591.Checked = .ASESMEN_591
                chkASESMEN_592.Checked = .ASESMEN_592
                chkASESMEN_593.Checked = .ASESMEN_593
                chkASESMEN_594.Checked = .ASESMEN_594
                chkASESMEN_595.Checked = .ASESMEN_595
                chkASESMEN_596.Checked = .ASESMEN_596
                chkASESMEN_597.Checked = .ASESMEN_597
                chkASESMEN_598.Checked = .ASESMEN_598
                chkASESMEN_599.Checked = .ASESMEN_599
                chkASESMEN_600.Checked = .ASESMEN_600
                chkASESMEN_601.Checked = .ASESMEN_601
                chkASESMEN_602.Checked = .ASESMEN_602
                chkASESMEN_603.Checked = .ASESMEN_603
                txtASESMEN_604.Text = .ASESMEN_604
                txtJAM.Text = .JAM
                txtPASIEN_KELUARGA.Text = .PASIEN_KELUARGA
                txtDPJP.Text = .DOKTER2_NAMEDISPLAY
                TextEdit1.Text = .KDUSER

                'tambahan
                chkMASALAHKEP_1.Checked = .MASALAHKEP_1
                chkMASALAHKEP_2.Checked = .MASALAHKEP_2
                chkMASALAHKEP_3.Checked = .MASALAHKEP_3
                chkMASALAHKEP_4.Checked = .MASALAHKEP_4
                chkMASALAHKEP_5.Checked = .MASALAHKEP_5
                chkMASALAHKEP_6.Checked = .MASALAHKEP_6
                chkMASALAHKEP_7.Checked = .MASALAHKEP_7
                chkMASALAHKEP_8.Checked = .MASALAHKEP_8
                chkMASALAHKEP_9.Checked = .MASALAHKEP_9
                chkMASALAHKEP_10.Checked = .MASALAHKEP_10
                chkMASALAHKEP_11.Checked = .MASALAHKEP_11
                chkMASALAHKEP_12.Checked = .MASALAHKEP_12
                chkMASALAHKEP_13.Checked = .MASALAHKEP_13
                chkMASALAHKEP_14.Checked = .MASALAHKEP_14
                chkMASALAHKEP_15.Checked = .MASALAHKEP_15
                chkMASALAHKEP_16.Checked = .MASALAHKEP_16
                chkMASALAHKEP_17.Checked = .MASALAHKEP_17
                chkMASALAHKEP_18.Checked = .MASALAHKEP_18
                chkMASALAHKEP_19.Checked = .MASALAHKEP_19
                chkMASALAHKEP_20.Checked = .MASALAHKEP_20
                chkMASALAHKEP_21.Checked = .MASALAHKEP_21
                chkMASALAHKEP_22.Checked = .MASALAHKEP_22
                chkMASALAHKEP_23.Checked = .MASALAHKEP_23
                chkMASALAHKEP_24.Checked = .MASALAHKEP_24
                chkMASALAHKEP_25.Checked = .MASALAHKEP_25
                chkMASALAHKEP_26.Checked = .MASALAHKEP_26
                chkMASALAHKEP_27.Checked = .MASALAHKEP_27
                chkMASALAHKEP_28.Checked = .MASALAHKEP_28
                chkMASALAHKEP_29.Checked = .MASALAHKEP_29
                chkMASALAHKEP_30.Checked = .MASALAHKEP_30
                chkMASALAHKEP_31.Checked = .MASALAHKEP_31
                chkMASALAHKEP_32.Checked = .MASALAHKEP_32
                chkMASALAHKEP_33.Checked = .MASALAHKEP_33
                chkMASALAHKEP_34.Checked = .MASALAHKEP_34
                chkMASALAHKEP_35.Checked = .MASALAHKEP_35
                chkMASALAHKEP_36.Checked = .MASALAHKEP_36
                chkMASALAHKEP_37.Checked = .MASALAHKEP_37
                chkMASALAHKEP_38.Checked = .MASALAHKEP_38
                chkMASALAHKEP_39.Checked = .MASALAHKEP_39
                chkMASALAHKEP_40.Checked = .MASALAHKEP_40
                chkMASALAHKEP_41.Checked = .MASALAHKEP_41
                chkMASALAHKEP_42.Checked = .MASALAHKEP_42
                chkMASALAHKEP_43.Checked = .MASALAHKEP_43
                chkMASALAHKEP_44.Checked = .MASALAHKEP_44
                txtMASALAHKEP_44_TEXT.Text = .MASALAHKEP_44_TEXT

                'tambahan
                chkPOLANAFAS.Checked = .POLANAFAS
                chkRESIKOASPIRASI.Checked = .RESIKOASPIRASI
                chkRESIKOPENURUNANCURAHJANTUNG.Checked = .RESIKOPENURUNANCURAHJANTUNG
                chkRESIKOPENDARAHAN.Checked = .RESIKOPENDARAHAN
                chkBERATBADANBERLEBIH.Checked = .BERATBADANBERLEBIH
                chkDISFUNGSIMGAS.Checked = .DISFUNGSIMGAS
                chkKESIAPANPNUTRISI.Checked = .KESIAPANPNUTRISI
                chkMENYUSUITIDAKEFEKTIF.Checked = .MENYUSUITIDAKEFEKTIF
                chkINURIN.Checked = .INURIN
                chkINFEKAL.Checked = .INFEKAL
                chkPENINGKATANTIDUR.Checked = .PENINGKATANTIDUR
                chkGANGGUANMEMORI.Checked = .GANGGUANMEMORI
                chkGANGGUANMENELAN.Checked = .GANGGUANMENELAN
                chkKONFUSIAKUT.Checked = .KONFUSIAKUT
                chkKONFUSIKROSNIS.Checked = .KONFUSIKROSNIS
                chkPOLASEKSUALTE.Checked = .POLASEKSUALTE
                chkNYERIMELAHIRKAN.Checked = .NYERIMELAHIRKAn
                chkHARGADIRIRENDAH.Checked = .HARGADIRIRENDAH
                chkKESIAPANKOPING.Checked = .KESIAPANKOPING
                chkSINDROMPASCATRAUMA.Checked = .SINDROMPASCATRAUMA
                chkGANGGUANPERTUMBUHAN.Checked = .GANGGUANPERTUMBUHAN
                chkGANGGUANPERKEMBANGAN.Checked = .GANGGUANPERKEMBANGAN
                chkKPMANAJEMENKES.Checked = .KPMANAJEMENKES
                chkKESIAPANPP.Checked = .KESIAPANPP
                chkPEMELIHARAANKES.Checked = .PEMELIHARAANKES
                chkGANGGUANINTERAKSISOSIAL.Checked = .GANGGUANINTERAKSISOSIAL
                chkKESIAPANPPKEL.Checked = .KESIAPANPPKEL
                chkRESIKOBUNDIR.Checked = .RESIKOBUNDIR
                chkRISIKOCEDERAPADAIBU.Checked = .RISIKOCEDERAPADAIBU
                chkCEDERAPADAJANIN.Checked = .CEDERAPADAJANIN
                chkRESIKOALERGI.Checked = .RESIKOALERGI

                txtSuku.Text = .DOKTER2_KODE
                'SDIGITALRJ25DETILBindingSource.DataSource = oDigital.GetDataDetail.Where(Function(x) x.KDKUNJUNGAN = txtNoRegister.Text).OrderBy(Function(x) x.SEQ).ToList()
                'grdDetil.DataSource = SDIGITALRJ25DETILBindingSource

                'bindingResikoJatuh.DataSource = oDigital.GetDataDetail2.Where(Function(x) x.KDKUNJUNGAN = txtNoRegister.Text).OrderBy(Function(x) x.SEQ).ToList()
                'grdResikoJatuh.DataSource = bindingResikoJatuh

                'bindingMonitoring.DataSource = oDigital.GetDataDetail3.Where(Function(x) x.KDKUNJUNGAN = txtNoRegister.Text).OrderBy(Function(x) x.SEQ).ToList()
                'grdMonitoringResikoJatuh.DataSource = bindingMonitoring
            End With
        Catch oErr As Exception
            MsgBox("Load List Data : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    'Private Function ByteArrayToImage(ByVal byteArrayIn() As Byte) As Image
    '    Using ms As New System.IO.MemoryStream(byteArrayIn)
    '        Dim returnImage = Image.FromStream(ms)
    '        Return returnImage
    '    End Using
    'End Function
    Private Function fn_Validate() As Boolean
        Try
            fn_Validate = True
            If txtNoRegister.Text = String.Empty Then
                MsgBox("Dibutuhkan Register", MsgBoxStyle.Exclamation, Me.Text)
                txtNoRegister.Focus()
                fn_Validate = False
                Exit Function
            End If
        Catch oErr As Exception
            MsgBox("Validate Data : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Function
    Private Function fn_Save() As Boolean
        Try
            ' ***** HEADER *****
            Dim ds = oDigital.GetStructureHeader
            With ds
                .KDPENDAFTARAN = txtNoRegister.Text
                Try
                    .DATECREATED = oDigital.GetData(txtNoRegister.Text).DATECREATED
                Catch ex As Exception
                    .DATECREATED = Now
                End Try

                .KDCUSTOMER = txtNoPasien.Text
                .DATEUPDATED = Now
                .ASESMEN_01 = deASESMEN_01.DateTime
                .ASESMEN_02 = Now.ToString("HH:mm")
                .ASESMEN_03 = chkASESMEN_03.Checked
                .ASESMEN_04 = chkASESMEN_04.Checked
                .ASESMEN_05 = txtASESMEN_05.Text
                .ASESMEN_06 = chkASESMEN_06.Checked
                .ASESMEN_07 = chkASESMEN_07.Checked
                .ASESMEN_08 = txtASESMEN_08.Text
                .ASESMEN_09 = txtASESMEN_09.Text
                .ASESMEN_10 = chkASESMEN_10.Checked
                .ASESMEN_11 = chkASESMEN_11.Checked
                .ASESMEN_12 = chkASESMEN_12.Checked
                .ASESMEN_13 = chkASESMEN_13.Checked
                .ASESMEN_14 = chkASESMEN_14.Checked
                .ASESMEN_15 = chkASESMEN_15.Checked
                .ASESMEN_16 = chkASESMEN_16.Checked
                .ASESMEN_17 = chkASESMEN_17.Checked
                .ASESMEN_18 = txtASESMEN_18.Text
                .ASESMEN_19 = txtASESMEN_19.Text
                .ASESMEN_20 = txtASESMEN_20.Text
                .ASESMEN_21 = chkASESMEN_21.Checked
                .ASESMEN_22 = chkASESMEN_22.Checked
                .ASESMEN_23 = txtASESMEN_23.Text
                .ASESMEN_24 = txtASESMEN_24.Text
                .ASESMEN_25 = chkASESMEN_25.Checked
                .ASESMEN_26 = chkASESMEN_26.Checked
                .ASESMEN_27 = txtASESMEN_27.Text
                .ASESMEN_28 = txtASESMEN_28.Text
                .ASESMEN_29 = chkASESMEN_29.Checked
                .ASESMEN_30 = chkASESMEN_30.Checked
                .ASESMEN_31 = txtASESMEN_31.Text
                .ASESMEN_32 = chkASESMEN_32.Checked
                .ASESMEN_33 = chkASESMEN_33.Checked
                .ASESMEN_34 = chkASESMEN_34.Checked
                .ASESMEN_35 = chkASESMEN_35.Checked
                .ASESMEN_36 = chkASESMEN_36.Checked
                .ASESMEN_37 = chkASESMEN_37.Checked
                .ASESMEN_38 = chkASESMEN_38.Checked
                .ASESMEN_39 = chkASESMEN_39.Checked
                .ASESMEN_40 = txtASESMEN_40.Text
                .ASESMEN_41 = chkASESMEN_41.Checked
                .ASESMEN_42 = chkASESMEN_42.Checked
                .ASESMEN_43 = chkASESMEN_43.Checked
                .ASESMEN_44 = chkASESMEN_44.Checked
                .ASESMEN_45 = txtASESMEN_45.Text
                .ASESMEN_46 = chkASESMEN_46.Checked
                .ASESMEN_47 = chkASESMEN_47.Checked
                .ASESMEN_48 = txtASESMEN_48.Text
                .ASESMEN_49 = chkASESMEN_49.Checked
                .ASESMEN_50 = chkASESMEN_50.Checked
                .ASESMEN_51 = chkASESMEN_51.Checked
                .ASESMEN_52 = txtASESMEN_52.Text
                .ASESMEN_605 = chkASESMEN_52.Checked
                .ASESMEN_53 = chkASESMEN_53.Checked
                .ASESMEN_54 = chkASESMEN_54.Checked
                .ASESMEN_55 = chkASESMEN_55.Checked
                .ASESMEN_56 = chkASESMEN_56.Checked
                .ASESMEN_57 = chkASESMEN_57.Checked
                .ASESMEN_58 = chkASESMEN_58.Checked
                .ASESMEN_59 = chkASESMEN_59.Checked
                .ASESMEN_60 = chkASESMEN_60.Checked
                .ASESMEN_61 = chkASESMEN_61.Checked
                .ASESMEN_62 = chkASESMEN_62.Checked
                .ASESMEN_63 = chkASESMEN_63.Checked
                .ASESMEN_64 = chkASESMEN_64.Checked
                .ASESMEN_65 = chkASESMEN_65.Checked
                .ASESMEN_66 = chkASESMEN_66.Checked
                .ASESMEN_67 = chkASESMEN_67.Checked
                .ASESMEN_68 = chkASESMEN_68.Checked
                .ASESMEN_69 = chkASESMEN_69.Checked
                .ASESMEN_70 = chkASESMEN_70.Checked
                .ASESMEN_71 = chkASESMEN_71.Checked
                .ASESMEN_72 = chkASESMEN_72.Checked
                .ASESMEN_73 = chkASESMEN_73.Checked
                .ASESMEN_74 = chkASESMEN_74.Checked
                .ASESMEN_75 = chkASESMEN_75.Checked
                .ASESMEN_76 = chkASESMEN_76.Checked
                .ASESMEN_77 = chkASESMEN_77.Checked
                .ASESMEN_78 = chkASESMEN_78.Checked
                .ASESMEN_79 = chkASESMEN_79.Checked
                .ASESMEN_80 = chkASESMEN_80.Checked
                .ASESMEN_81 = chkASESMEN_81.Checked
                .ASESMEN_82 = chkASESMEN_82.Checked
                .ASESMEN_83 = chkASESMEN_83.Checked
                .ASESMEN_84 = chkASESMEN_84.Checked
                .ASESMEN_85 = chkASESMEN_85.Checked
                .ASESMEN_86 = chkASESMEN_86.Checked
                .ASESMEN_87 = chkASESMEN_87.Checked
                .ASESMEN_88 = chkASESMEN_88.Checked
                .ASESMEN_89 = chkASESMEN_89.Checked
                .ASESMEN_90 = chkASESMEN_90.Checked
                .ASESMEN_91 = chkASESMEN_91.Checked
                .ASESMEN_92 = chkASESMEN_92.Checked
                .ASESMEN_93 = chkASESMEN_93.Checked
                .ASESMEN_94 = chkASESMEN_94.Checked
                .ASESMEN_95 = chkASESMEN_95.Checked
                .ASESMEN_96 = chkASESMEN_96.Checked
                .ASESMEN_97 = chkASESMEN_97.Checked
                .ASESMEN_98 = chkASESMEN_98.Checked
                .ASESMEN_99 = chkASESMEN_99.Checked
                .ASESMEN_100 = chkASESMEN_100.Checked
                .ASESMEN_101 = chkASESMEN_101.Checked
                .ASESMEN_102 = chkASESMEN_102.Checked
                .ASESMEN_103 = chkASESMEN_103.Checked
                .ASESMEN_104 = chkASESMEN_104.Checked
                .ASESMEN_105 = chkASESMEN_105.Checked
                .ASESMEN_106 = chkASESMEN_106.Checked


                .TANGGALRESIKOJATUH1 = DateEdit1.Text
                .TANGGALRESIKOJATUH2 = DateEdit2.Text
                .TANGGALRESIKOJATUH3 = DateEdit3.Text
                .TANGGALRESIKOJATUH4 = DateEdit4.Text
                .TANGGALRESIKOJATUH5 = DateEdit5.Text


                .ASESMEN_108 = txtASESMEN_108.Text
                .ASESMEN_109 = txtASESMEN_109.Text
                .ASESMEN_110 = txtASESMEN_110.Text
                .ASESMEN_111 = txtASESMEN_111.Text
                .ASESMEN_112 = txtASESMEN_112.Text
                .ASESMEN_113 = txtASESMEN_113.Text
                .ASESMEN_114 = txtASESMEN_114.Text
                .ASESMEN_115 = txtASESMEN_115.Text
                .ASESMEN_116 = txtASESMEN_116.Text
                .ASESMEN_117 = txtASESMEN_117.Text
                .ASESMEN_118 = txtASESMEN_118.Text
                .ASESMEN_119 = txtASESMEN_119.Text
                .ASESMEN_120 = txtASESMEN_120.Text
                .ASESMEN_121 = txtASESMEN_121.Text
                .ASESMEN_122 = txtASESMEN_122.Text
                .ASESMEN_123 = txtASESMEN_123.Text
                .ASESMEN_124 = txtASESMEN_124.Text
                .ASESMEN_125 = txtASESMEN_125.Text
                .ASESMEN_126 = txtASESMEN_126.Text
                .ASESMEN_127 = txtASESMEN_127.Text
                .ASESMEN_128 = txtASESMEN_128.Text
                .ASESMEN_129 = txtASESMEN_129.Text
                .ASESMEN_130 = txtASESMEN_130.Text
                .ASESMEN_131 = txtASESMEN_131.Text
                .ASESMEN_132 = txtASESMEN_132.Text
                .ASESMEN_133 = txtASESMEN_133.Text
                .ASESMEN_134 = txtASESMEN_134.Text
                .ASESMEN_135 = txtASESMEN_135.Text
                .ASESMEN_136 = txtASESMEN_136.Text
                .ASESMEN_137 = txtASESMEN_137.Text
                .ASESMEN_138 = txtASESMEN_138.Text
                .ASESMEN_139 = txtASESMEN_139.Text
                .ASESMEN_140 = txtASESMEN_140.Text
                .ASESMEN_141 = txtASESMEN_141.Text
                .ASESMEN_142 = txtASESMEN_142.Text
                .ASESMEN_143 = txtASESMEN_143.Text
                .ASESMEN_144 = txtASESMEN_144.Text
                .ASESMEN_145 = txtASESMEN_145.Text
                .ASESMEN_146 = txtASESMEN_146.Text
                .ASESMEN_147 = txtASESMEN_147.Text

                .ASESMEN_149 = chkASESMEN_149.Checked
                .ASESMEN_150 = chkASESMEN_150.Checked
                .ASESMEN_151 = txtASESMEN_151.Text
                .ASESMEN_152 = chkASESMEN_152.Checked
                .ASESMEN_153 = chkASESMEN_153.Checked
                .ASESMEN_154 = txtASESMEN_154.Text
                .ASESMEN_155 = chkASESMEN_155.Checked
                .ASESMEN_156 = chkASESMEN_156.Checked
                .ASESMEN_157 = txtASESMEN_157.Text
                .ASESMEN_158 = chkASESMEN_158.Checked
                .ASESMEN_159 = chkASESMEN_159.Checked
                .ASESMEN_160 = txtASESMEN_160.Text
                .ASESMEN_161 = chkASESMEN_161.Checked
                .ASESMEN_162 = chkASESMEN_162.Checked
                .ASESMEN_163 = txtASESMEN_163.Text
                .ASESMEN_164 = chkASESMEN_164.Checked
                .ASESMEN_165 = chkASESMEN_165.Checked
                .ASESMEN_166 = txtASESMEN_166.Text
                .ASESMEN_167 = chkASESMEN_167.Checked
                .ASESMEN_168 = chkASESMEN_168.Checked
                .ASESMEN_169 = txtASESMEN_169.Text
                .ASESMEN_170 = chkASESMEN_170.Checked
                .ASESMEN_171 = chkASESMEN_171.Checked
                .ASESMEN_172 = txtASESMEN_172.Text
                .ASESMEN_173 = chkASESMEN_173.Checked
                .ASESMEN_174 = chkASESMEN_174.Checked
                .ASESMEN_175 = txtASESMEN_175.Text
                .ASESMEN_176 = chkASESMEN_176.Checked
                .ASESMEN_177 = txtASESMEN_177.Text
                .ASESMEN_178 = chkASESMEN_178.Checked
                .ASESMEN_179 = txtASESMEN_179.Text
                .ASESMEN_180 = chkASESMEN_180.Checked
                .ASESMEN_181 = txtASESMEN_181.Text
                .ASESMEN_182 = chkASESMEN_182.Checked
                .ASESMEN_183 = txtASESMEN_183.Text
                .ASESMEN_184 = chkASESMEN_184.Checked
                .ASESMEN_185 = txtASESMEN_185.Text
                .ASESMEN_186 = chkASESMEN_186.Checked
                .ASESMEN_187 = txtASESMEN_187.Text
                .ASESMEN_188 = chkASESMEN_188.Checked
                .ASESMEN_189 = txtASESMEN_189.Text
                .ASESMEN_190 = chkASESMEN_190.Checked
                .ASESMEN_191 = txtASESMEN_191.Text
                .ASESMEN_192 = txtASESMEN_192.Text

                .ASESMEN_193 = txtASESMEN_193.Text
                .ASESMEN_194 = txtASESMEN_194.Text
                .ASESMEN_195 = txtASESMEN_195.Text
                .ASESMEN_196 = txtASESMEN_196.Text
                .ASESMEN_197 = txtASESMEN_197.Text
                .ASESMEN_198 = txtASESMEN_198.Text
                .ASESMEN_199 = txtASESMEN_199.Text


                .ASESMEN_200 = txtASESMEN_200.Text
                .ASESMEN_201 = txtASESMEN_201.Text
                .ASESMEN_202 = txtASESMEN_202.Text
                .ASESMEN_203 = txtASESMEN_203.Text
                .ASESMEN_204 = txtASESMEN_204.Text
                .ASESMEN_205 = txtASESMEN_205.Text
                .ASESMEN_206 = txtASESMEN_206.Text
                .ASESMEN_207 = txtASESMEN_207.Text
                .ASESMEN_208 = txtASESMEN_208.Text
                .ASESMEN_209 = txtASESMEN_209.Text
                .ASESMEN_210 = txtASESMEN_210.Text
                .ASESMEN_211 = txtASESMEN_211.Text
                .ASESMEN_212 = txtASESMEN_212.Text
                .ASESMEN_213 = txtASESMEN_213.Text
                .ASESMEN_214 = txtASESMEN_214.Text
                .ASESMEN_215 = txtASESMEN_215.Text
                .ASESMEN_216 = txtASESMEN_216.Text
                .ASESMEN_217 = txtASESMEN_217.Text
                .ASESMEN_218 = txtASESMEN_218.Text
                .ASESMEN_219 = txtASESMEN_219.Text
                .ASESMEN_220 = txtASESMEN_220.Text
                .ASESMEN_221 = txtASESMEN_221.Text
                .ASESMEN_222 = txtASESMEN_222.Text
                .ASESMEN_223 = txtASESMEN_223.Text
                .ASESMEN_224 = txtASESMEN_224.Text
                .ASESMEN_225 = txtASESMEN_225.Text
                .ASESMEN_226 = txtASESMEN_226.Text
                .ASESMEN_227 = txtASESMEN_227.Text
                .ASESMEN_228 = txtASESMEN_228.Text
                .ASESMEN_229 = txtASESMEN_229.Text
                .ASESMEN_230 = txtASESMEN_230.Text
                .ASESMEN_231 = txtASESMEN_231.Text
                .ASESMEN_232 = txtASESMEN_232.Text
                .ASESMEN_233 = txtASESMEN_233.Text
                .ASESMEN_234 = txtASESMEN_234.Text
                .ASESMEN_235 = txtASESMEN_235.Text
                .ASESMEN_236 = txtASESMEN_236.Text
                .ASESMEN_237 = txtASESMEN_237.Text
                .ASESMEN_238 = txtASESMEN_238.Text
                .ASESMEN_239 = txtASESMEN_239.Text
                .ASESMEN_240 = txtASESMEN_240.Text
                .ASESMEN_241 = txtASESMEN_241.Text
                .ASESMEN_242 = txtASESMEN_242.Text
                .ASESMEN_243 = txtASESMEN_243.Text
                .ASESMEN_244 = txtASESMEN_244.Text
                .ASESMEN_245 = txtASESMEN_245.Text
                .ASESMEN_246 = txtASESMEN_246.Text
                .ASESMEN_247 = txtASESMEN_247.Text
                .ASESMEN_248 = txtASESMEN_248.Text

                'monitoring v2
                .MONITORING0 = CheckEdit1.Checked
                .MONITORING1 = CheckEdit2.Checked
                .MONITORING2 = CheckEdit3.Checked
                .MONITORING3 = CheckEdit4.Checked
                .MONITORING4 = CheckEdit5.Checked
                .MONITORING5 = CheckEdit6.Checked
                .MONITORING6 = CheckEdit7.Checked
                .MONITORING7 = CheckEdit8.Checked
                .MONITORING8 = CheckEdit9.Checked
                .MONITORING9 = CheckEdit10.Checked
                .MONITORING10 = CheckEdit11.Checked
                .MONITORING11 = CheckEdit12.Checked
                .MONITORING12 = CheckEdit13.Checked
                .MONITORING13 = CheckEdit14.Checked
                .MONITORING14 = CheckEdit15.Checked
                .MONITORING15 = CheckEdit16.Checked
                .MONITORING16 = CheckEdit17.Checked
                .MONITORING17 = CheckEdit18.Checked
                .MONITORING18 = CheckEdit19.Checked
                .MONITORING19 = CheckEdit20.Checked
                .MONITORING20 = CheckEdit21.Checked
                .MONITORING21 = CheckEdit22.Checked
                .MONITORING22 = CheckEdit23.Checked
                .MONITORING23 = CheckEdit24.Checked
                .MONITORING24 = CheckEdit25.Checked
                .MONITORING25 = CheckEdit26.Checked
                .MONITORING26 = CheckEdit27.Checked
                .MONITORING27 = CheckEdit28.Checked
                .MONITORING28 = CheckEdit29.Checked
                .MONITORING29 = CheckEdit30.Checked
                .MONITORING30 = CheckEdit31.Checked
                .MONITORING31 = CheckEdit32.Checked
                .MONITORING32 = CheckEdit33.Checked
                .MONITORING33 = CheckEdit34.Checked
                .MONITORING34 = CheckEdit35.Checked
                .MONITORING35 = CheckEdit36.Checked
                .MONITORING36 = CheckEdit37.Checked
                .MONITORING37 = CheckEdit38.Checked
                .MONITORING38 = CheckEdit39.Checked
                .MONITORING39 = CheckEdit40.Checked
                .MONITORING40 = CheckEdit41.Checked
                .MONITORING41 = CheckEdit43.Checked
                .MONITORING42 = CheckEdit44.Checked
                .MONITORING43 = CheckEdit45.Checked
                .MONITORING44 = CheckEdit46.Checked
                .MONITORING45 = CheckEdit47.Checked
                .MONITORING46 = CheckEdit48.Checked
                .MONITORING47 = CheckEdit49.Checked
                .MONITORING48 = CheckEdit50.Checked


                .ASESMEN_249 = txtASESMEN_249.Text
                .ASESMEN_250 = txtASESMEN_250.Text
                .ASESMEN_251 = txtASESMEN_251.Text
                .ASESMEN_252 = txtASESMEN_252.Text
                .ASESMEN_253 = txtASESMEN_253.Text
                .ASESMEN_254 = txtASESMEN_254.Text
                .ASESMEN_255 = txtASESMEN_255.Text
                .ASESMEN_256 = txtASESMEN_256.Text
                .ASESMEN_257 = txtASESMEN_257.Text
                .ASESMEN_258 = txtASESMEN_258.Text
                .ASESMEN_259 = txtASESMEN_259.Text
                .ASESMEN_260 = txtASESMEN_260.Text
                .ASESMEN_261 = chkASESMEN_261.Checked
                .ASESMEN_262 = chkASESMEN_262.Checked
                .ASESMEN_263 = chkASESMEN_263.Checked
                .ASESMEN_264 = chkASESMEN_264.Checked
                .ASESMEN_265 = txtASESMEN_265.Text
                .ASESMEN_266 = txtASESMEN_266.Text
                .ASESMEN_267 = txtASESMEN_267.Text
                .ASESMEN_268 = txtASESMEN_268.Text
                .ASESMEN_269 = txtASESMEN_269.Text
                .ASESMEN_270 = txtASESMEN_270.Text
                .ASESMEN_271 = chkASESMEN_271.Checked
                .ASESMEN_272 = chkASESMEN_272.Checked
                .ASESMEN_273 = chkASESMEN_273.Checked
                .ASESMEN_274 = chkASESMEN_274.Checked
                .ASESMEN_275 = chkASESMEN_275.Checked
                .ASESMEN_276 = chkASESMEN_276.Checked
                .ASESMEN_277 = chkASESMEN_277.Checked
                .ASESMEN_278 = chkASESMEN_278.Checked
                .ASESMEN_279 = chkASESMEN_279.Checked
                .ASESMEN_280 = chkASESMEN_280.Checked
                .ASESMEN_281 = txtASESMEN_281.Text
                .ASESMEN_282 = chkASESMEN_282.Checked
                .ASESMEN_283 = chkASESMEN_283.Checked
                .ASESMEN_284 = chkASESMEN_284.Checked
                .ASESMEN_285 = chkASESMEN_285.Checked
                .ASESMEN_286 = chkASESMEN_286.Checked
                .ASESMEN_287 = chkASESMEN_287.Checked
                .ASESMEN_288 = chkASESMEN_288.Checked
                .ASESMEN_289 = chkASESMEN_289.Checked
                .ASESMEN_290 = chkASESMEN_290.Checked
                .ASESMEN_291 = chkASESMEN_291.Checked
                .ASESMEN_292 = chkASESMEN_292.Checked
                .ASESMEN_293 = txtASESMEN_293.Text
                .ASESMEN_294 = chkASESMEN_294.Checked
                .ASESMEN_295 = chkASESMEN_295.Checked
                .ASESMEN_296 = chkASESMEN_296.Checked
                .ASESMEN_297 = chkASESMEN_297.Checked
                .ASESMEN_298 = txtASESMEN_298.Text
                .ASESMEN_299 = chkASESMEN_299.Checked
                .ASESMEN_300 = chkASESMEN_300.Checked
                .ASESMEN_301 = chkASESMEN_301.Checked
                .ASESMEN_302 = chkASESMEN_302.Checked
                .ASESMEN_303 = txtASESMEN_303.Text
                .ASESMEN_304 = chkASESMEN_304.Checked
                .ASESMEN_305 = txtASESMEN_305.Text
                .ASESMEN_306 = chkASESMEN_306.Checked
                .ASESMEN_307 = chkASESMEN_307.Checked
                .ASESMEN_308 = chkASESMEN_308.Checked
                .ASESMEN_309 = chkASESMEN_309.Checked
                .ASESMEN_310 = chkASESMEN_310.Checked
                .ASESMEN_311 = chkASESMEN_311.Checked
                .ASESMEN_312 = chkASESMEN_312.Checked
                .ASESMEN_313 = chkASESMEN_313.Checked
                .ASESMEN_314 = chkASESMEN_314.Checked
                .ASESMEN_315 = chkASESMEN_315.Checked
                .ASESMEN_316 = chkASESMEN_316.Checked
                .ASESMEN_317 = chkASESMEN_317.Checked
                .ASESMEN_318 = chkASESMEN_318.Checked
                .ASESMEN_319 = chkASESMEN_319.Checked
                .ASESMEN_320 = chkASESMEN_320.Checked
                .ASESMEN_321 = chkASESMEN_321.Checked
                .ASESMEN_322 = chkASESMEN_322.Checked
                .ASESMEN_323 = chkASESMEN_323.Checked
                .ASESMEN_324 = chkASESMEN_324.Checked
                .ASESMEN_325 = chkASESMEN_325.Checked
                .ASESMEN_326 = chkASESMEN_326.Checked
                .ASESMEN_327 = chkASESMEN_327.Checked
                .ASESMEN_328 = chkASESMEN_328.Checked
                .ASESMEN_329 = chkASESMEN_329.Checked
                .ASESMEN_330 = chkASESMEN_330.Checked
                .ASESMEN_331 = chkASESMEN_331.Checked
                .ASESMEN_332 = chkASESMEN_332.Checked
                .ASESMEN_333 = chkASESMEN_333.Checked
                .ASESMEN_334 = chkASESMEN_334.Checked
                .ASESMEN_335 = chkASESMEN_335.Checked
                .ASESMEN_336 = chkASESMEN_336.Checked
                .ASESMEN_337 = chkASESMEN_337.Checked
                .ASESMEN_338 = chkASESMEN_338.Checked
                .ASESMEN_339 = chkASESMEN_339.Checked
                .ASESMEN_340 = chkASESMEN_340.Checked
                .ASESMEN_341 = chkASESMEN_341.Checked
                .ASESMEN_342 = chkASESMEN_342.Checked
                .ASESMEN_343 = chkASESMEN_343.Checked
                .ASESMEN_344 = chkASESMEN_344.Checked
                .ASESMEN_345 = chkASESMEN_345.Checked
                .ASESMEN_346 = chkASESMEN_346.Checked
                .ASESMEN_347 = chkASESMEN_347.Checked
                .ASESMEN_348 = chkASESMEN_348.Checked
                .ASESMEN_349 = chkASESMEN_349.Checked
                .ASESMEN_350 = chkASESMEN_350.Checked
                .ASESMEN_351 = chkASESMEN_351.Checked
                .ASESMEN_352 = chkASESMEN_352.Checked
                .ASESMEN_353 = chkASESMEN_353.Checked
                .ASESMEN_354 = chkASESMEN_354.Checked
                .ASESMEN_355 = chkASESMEN_355.Checked
                .ASESMEN_356 = chkASESMEN_356.Checked
                .ASESMEN_357 = chkASESMEN_357.Checked
                .ASESMEN_358 = chkASESMEN_358.Checked
                .ASESMEN_359 = chkASESMEN_359.Checked
                .ASESMEN_360 = txtASESMEN_360.Text
                .ASESMEN_361 = chkASESMEN_361.Checked
                .ASESMEN_362 = chkASESMEN_362.Checked
                .ASESMEN_363 = chkASESMEN_363.Checked
                .ASESMEN_364 = chkASESMEN_364.Checked
                .ASESMEN_365 = chkASESMEN_365.Checked
                .ASESMEN_366 = chkASESMEN_366.Checked
                .ASESMEN_367 = chkASESMEN_367.Checked
                .ASESMEN_368 = chkASESMEN_368.Checked
                .ASESMEN_369 = chkASESMEN_369.Checked
                .ASESMEN_370 = chkASESMEN_370.Checked
                .ASESMEN_371 = chkASESMEN_371.Checked
                .ASESMEN_372 = chkASESMEN_372.Checked
                .ASESMEN_373 = chkASESMEN_373.Checked
                .ASESMEN_374 = chkASESMEN_374.Checked
                .ASESMEN_375 = chkASESMEN_375.Checked
                .ASESMEN_376 = chkASESMEN_376.Checked
                .ASESMEN_377 = chkASESMEN_377.Checked
                .ASESMEN_378 = chkASESMEN_378.Checked
                .ASESMEN_379 = chkASESMEN_379.Checked
                .ASESMEN_380 = chkASESMEN_380.Checked
                .ASESMEN_381 = chkASESMEN_381.Checked
                .ASESMEN_382 = chkASESMEN_382.Checked
                .ASESMEN_383 = chkASESMEN_383.Checked
                .ASESMEN_384 = chkASESMEN_384.Checked
                .ASESMEN_385 = chkASESMEN_385.Checked
                .ASESMEN_386 = chkASESMEN_386.Checked
                .ASESMEN_387 = chkASESMEN_387.Checked
                .ASESMEN_388 = txtASESMEN_388.Text
                .ASESMEN_389 = chkASESMEN_389.Checked
                .ASESMEN_390 = chkASESMEN_390.Checked
                .ASESMEN_391 = chkASESMEN_391.Checked
                .ASESMEN_392 = chkASESMEN_392.Checked
                .ASESMEN_393 = chkASESMEN_393.Checked
                .ASESMEN_394 = chkASESMEN_394.Checked
                .ASESMEN_395 = chkASESMEN_395.Checked
                .ASESMEN_396 = chkASESMEN_396.Checked
                .ASESMEN_397 = chkASESMEN_397.Checked
                .ASESMEN_398 = chkASESMEN_398.Checked
                .ASESMEN_399 = chkASESMEN_399.Checked
                .ASESMEN_400 = chkASESMEN_400.Checked
                .ASESMEN_401 = txtASESMEN_401.Text
                .ASESMEN_402 = txtASESMEN_402.Text
                .ASESMEN_403 = chkASESMEN_403.Checked
                .ASESMEN_404 = chkASESMEN_404.Checked
                .ASESMEN_405 = chkASESMEN_405.Checked
                .ASESMEN_406 = chkASESMEN_406.Checked
                .ASESMEN_407 = chkASESMEN_407.Checked
                .ASESMEN_408 = txtASESMEN_408.Text
                .ASESMEN_409 = chkASESMEN_409.Checked
                .ASESMEN_410 = chkASESMEN_410.Checked
                .ASESMEN_411 = chkASESMEN_411.Checked
                .ASESMEN_412 = chkASESMEN_412.Checked
                .ASESMEN_413 = chkASESMEN_413.Checked
                .ASESMEN_414 = chkASESMEN_414.Checked
                .ASESMEN_415 = chkASESMEN_415.Checked
                .ASESMEN_416 = chkASESMEN_416.Checked
                .ASESMEN_417 = chkASESMEN_417.Checked
                .ASESMEN_418 = chkASESMEN_418.Checked
                .ASESMEN_419 = chkASESMEN_419.Checked
                .ASESMEN_420 = chkASESMEN_420.Checked
                .ASESMEN_421 = chkASESMEN_421.Checked
                .ASESMEN_422 = chkASESMEN_422.Checked
                .ASESMEN_423 = chkASESMEN_423.Checked
                .ASESMEN_424 = chkASESMEN_424.Checked
                .ASESMEN_425 = chkASESMEN_425.Checked
                .ASESMEN_426 = chkASESMEN_426.Checked
                .ASESMEN_427 = txtASESMEN_427.Text
                .ASESMEN_428 = chkASESMEN_428.Checked
                .ASESMEN_429 = txtASESMEN_429.Text
                .ASESMEN_430 = txtASESMEN_430.Text
                .ASESMEN_431 = txtASESMEN_431.Text
                .ASESMEN_432 = txtASESMEN_432.Text
                .ASESMEN_433 = txtASESMEN_433.Text
                .ASESMEN_434 = chkASESMEN_434.Checked
                .ASESMEN_435 = chkASESMEN_435.Checked
                .ASESMEN_436 = txtASESMEN_436.Text
                .ASESMEN_437 = chkASESMEN_437.Checked
                .ASESMEN_438 = chkASESMEN_438.Checked
                .ASESMEN_439 = chkASESMEN_439.Checked
                .ASESMEN_440 = chkASESMEN_440.Checked
                .ASESMEN_441 = chkASESMEN_441.Checked
                .ASESMEN_442 = txtASESMEN_442.Text
                .ASESMEN_443 = txtASESMEN_443.Text
                .ASESMEN_444 = chkASESMEN_444.Checked
                .ASESMEN_445 = chkASESMEN_445.Checked
                .ASESMEN_446 = chkASESMEN_446.Checked
                .ASESMEN_447 = chkASESMEN_447.Checked
                .ASESMEN_448 = chkASESMEN_448.Checked
                .ASESMEN_449 = chkASESMEN_449.Checked
                .ASESMEN_450 = chkASESMEN_450.Checked
                .ASESMEN_451 = chkASESMEN_451.Checked
                .ASESMEN_452 = chkASESMEN_452.Checked
                .ASESMEN_453 = chkASESMEN_453.Checked
                .ASESMEN_454 = chkASESMEN_454.Checked
                .ASESMEN_455 = chkASESMEN_455.Checked
                .ASESMEN_456 = chkASESMEN_456.Checked
                .ASESMEN_457 = chkASESMEN_457.Checked
                .ASESMEN_458 = chkASESMEN_458.Checked
                .ASESMEN_459 = txtASESMEN_459.Text
                .ASESMEN_460 = chkASESMEN_460.Checked
                .ASESMEN_461 = chkASESMEN_461.Checked
                .ASESMEN_462 = chkASESMEN_462.Checked
                .ASESMEN_463 = chkASESMEN_463.Checked
                .ASESMEN_464 = chkASESMEN_464.Checked
                .ASESMEN_465 = txtASESMEN_465.Text
                .ASESMEN_466 = chkASESMEN_466.Checked
                .ASESMEN_467 = chkASESMEN_467.Checked
                .ASESMEN_468 = chkASESMEN_468.Checked
                .ASESMEN_469 = chkASESMEN_469.Checked
                .ASESMEN_470 = txtASESMEN_470.Text
                .ASESMEN_471 = chkASESMEN_471.Checked
                .ASESMEN_472 = chkASESMEN_472.Checked
                .ASESMEN_473 = chkASESMEN_473.Checked
                .ASESMEN_474 = chkASESMEN_474.Checked
                .ASESMEN_475 = chkASESMEN_475.Checked
                .ASESMEN_476 = chkASESMEN_476.Checked
                .ASESMEN_477 = chkASESMEN_477.Checked
                .ASESMEN_478 = chkASESMEN_478.Checked
                .ASESMEN_479 = chkASESMEN_479.Checked
                .ASESMEN_480 = chkASESMEN_480.Checked
                .ASESMEN_481 = chkASESMEN_481.Checked
                .ASESMEN_482 = chkASESMEN_482.Checked
                .ASESMEN_483 = chkASESMEN_483.Checked
                .ASESMEN_484 = chkASESMEN_484.Checked
                .ASESMEN_485 = txtASESMEN_485.Text
                .ASESMEN_486 = chkASESMEN_486.Checked
                .ASESMEN_487 = chkASESMEN_487.Checked
                .ASESMEN_488 = chkASESMEN_488.Checked
                .ASESMEN_489 = chkASESMEN_489.Checked
                .ASESMEN_490 = txtASESMEN_490.Text
                .ASESMEN_491 = txtASESMEN_491.Text
                .ASESMEN_492 = txtASESMEN_492.Text
                .ASESMEN_493 = txtASESMEN_493.Text
                .ASESMEN_494 = txtASESMEN_494.Text
                .ASESMEN_495 = txtASESMEN_495.Text
                .ASESMEN_496 = txtASESMEN_496.Text
                .ASESMEN_497 = txtASESMEN_497.Text
                .ASESMEN_498 = txtASESMEN_498.Text
                .ASESMEN_499 = txtASESMEN_499.Text
                .ASESMEN_500 = txtASESMEN_500.Text
                .ASESMEN_501 = txtASESMEN_501.Text
                .ASESMEN_502 = txtASESMEN_502.Text
                .ASESMEN_503 = txtASESMEN_503.Text
                .ASESMEN_504 = txtASESMEN_504.Text
                .ASESMEN_505 = txtASESMEN_505.Text
                .ASESMEN_506 = chkASESMEN_506.Checked
                .ASESMEN_507 = chkASESMEN_507.Checked
                .ASESMEN_508 = chkASESMEN_508.Checked
                .ASESMEN_509 = chkASESMEN_509.Checked
                .ASESMEN_510 = chkASESMEN_510.Checked
                .ASESMEN_511 = chkASESMEN_511.Checked
                .ASESMEN_512 = chkASESMEN_512.Checked
                .ASESMEN_513 = chkASESMEN_513.Checked
                .ASESMEN_514 = chkASESMEN_514.Checked
                .ASESMEN_515 = chkASESMEN_515.Checked
                .ASESMEN_516 = chkASESMEN_516.Checked
                .ASESMEN_517 = chkASESMEN_517.Checked
                .ASESMEN_518 = chkASESMEN_518.Checked
                .ASESMEN_519 = chkASESMEN_519.Checked
                .ASESMEN_520 = chkASESMEN_520.Checked
                .ASESMEN_521 = chkASESMEN_521.Checked
                .ASESMEN_522 = chkASESMEN_522.Checked
                .ASESMEN_523 = chkASESMEN_523.Checked
                .ASESMEN_524 = chkASESMEN_524.Checked
                .ASESMEN_525 = chkASESMEN_525.Checked
                .ASESMEN_526 = chkASESMEN_526.Checked
                .ASESMEN_527 = chkASESMEN_527.Checked
                .ASESMEN_528 = chkASESMEN_528.Checked
                .ASESMEN_529 = chkASESMEN_529.Checked
                .ASESMEN_530 = chkASESMEN_530.Checked
                .ASESMEN_531 = chkASESMEN_531.Checked
                .ASESMEN_532 = chkASESMEN_532.Checked
                .ASESMEN_533 = chkASESMEN_533.Checked
                .ASESMEN_534 = chkASESMEN_534.Checked
                .ASESMEN_535 = chkASESMEN_535.Checked
                .ASESMEN_536 = chkASESMEN_536.Checked
                .ASESMEN_537 = chkASESMEN_537.Checked
                .ASESMEN_538 = chkASESMEN_538.Checked
                .ASESMEN_539 = chkASESMEN_539.Checked
                .ASESMEN_540 = chkASESMEN_540.Checked
                .ASESMEN_541 = chkASESMEN_541.Checked
                .ASESMEN_542 = chkASESMEN_542.Checked
                .ASESMEN_543 = chkASESMEN_543.Checked
                .ASESMEN_544 = chkASESMEN_544.Checked
                .ASESMEN_545 = chkASESMEN_545.Checked
                .ASESMEN_546 = chkASESMEN_546.Checked
                .ASESMEN_547 = chkASESMEN_547.Checked
                .ASESMEN_548 = chkASESMEN_548.Checked
                .ASESMEN_549 = chkASESMEN_549.Checked
                .ASESMEN_550 = chkASESMEN_550.Checked
                .ASESMEN_551 = chkASESMEN_551.Checked
                .ASESMEN_552 = txtASESMEN_552.Text
                .ASESMEN_553 = chkASESMEN_553.Checked
                .ASESMEN_554 = chkASESMEN_554.Checked
                .ASESMEN_555 = txtASESMEN_555.Text
                .ASESMEN_556 = chkASESMEN_556.Checked
                .ASESMEN_557 = chkASESMEN_557.Checked
                .ASESMEN_558 = chkASESMEN_558.Checked
                .ASESMEN_559 = chkASESMEN_559.Checked
                .ASESMEN_560 = txtASESMEN_560.Text
                .ASESMEN_561 = txtASESMEN_561.Text
                .ASESMEN_562 = txtASESMEN_562.Text
                .ASESMEN_563 = txtASESMEN_563.Text
                .ASESMEN_564 = txtASESMEN_564.Text
                .ASESMEN_565 = chkASESMEN_565.Checked
                .ASESMEN_566 = chkASESMEN_566.Checked
                .ASESMEN_567 = chkASESMEN_567.Checked
                .ASESMEN_568 = chkASESMEN_568.Checked
                .ASESMEN_569 = txtASESMEN_569.Text
                .ASESMEN_570 = chkASESMEN_570.Checked
                .ASESMEN_571 = chkASESMEN_571.Checked
                .ASESMEN_572 = chkASESMEN_572.Checked
                .ASESMEN_573 = chkASESMEN_573.Checked
                .ASESMEN_574 = chkASESMEN_574.Checked
                .ASESMEN_575 = chkASESMEN_575.Checked
                .ASESMEN_576 = chkASESMEN_576.Checked
                .ASESMEN_577 = chkASESMEN_577.Checked
                .ASESMEN_578 = chkASESMEN_578.Checked
                .ASESMEN_579 = chkASESMEN_579.Checked
                .ASESMEN_580 = chkASESMEN_580.Checked
                .ASESMEN_581 = chkASESMEN_581.Checked
                .ASESMEN_582 = chkASESMEN_582.Checked
                .ASESMEN_583 = chkASESMEN_583.Checked
                .ASESMEN_584 = chkASESMEN_584.Checked
                .ASESMEN_585 = chkASESMEN_585.Checked
                .ASESMEN_586 = chkASESMEN_586.Checked
                .ASESMEN_587 = chkASESMEN_587.Checked
                .ASESMEN_588 = chkASESMEN_588.Checked
                .ASESMEN_589 = chkASESMEN_589.Checked
                .ASESMEN_590 = chkASESMEN_590.Checked
                .ASESMEN_591 = chkASESMEN_591.Checked
                .ASESMEN_592 = chkASESMEN_592.Checked
                .ASESMEN_593 = chkASESMEN_593.Checked
                .ASESMEN_594 = chkASESMEN_594.Checked
                .ASESMEN_595 = chkASESMEN_595.Checked
                .ASESMEN_596 = chkASESMEN_596.Checked
                .ASESMEN_597 = chkASESMEN_597.Checked
                .ASESMEN_598 = chkASESMEN_598.Checked
                .ASESMEN_599 = chkASESMEN_599.Checked
                .ASESMEN_600 = chkASESMEN_600.Checked
                .ASESMEN_601 = chkASESMEN_601.Checked
                .ASESMEN_602 = chkASESMEN_602.Checked
                .ASESMEN_603 = chkASESMEN_603.Checked
                .ASESMEN_604 = txtASESMEN_604.Text
                .JAM = txtJAM.Text
                .PASIEN_KELUARGA = txtPASIEN_KELUARGA.Text
                .DOKTER2_KODE = txtSuku.Text
                .DOKTER2_NAMEDISPLAY = txtDPJP.Text
                Try
                    .CETAK = oDigital.GetData(txtNoRegister.Text).CETAK
                Catch ex As Exception
                    .CETAK = 0
                End Try

                .KDUSER = TextEdit1.Text
                .KDUSER_SIGNATURE = sUserID
                .MASALAHKEP_1 = chkMASALAHKEP_1.Checked
                .MASALAHKEP_2 = chkMASALAHKEP_2.Checked
                .MASALAHKEP_3 = chkMASALAHKEP_3.Checked
                .MASALAHKEP_4 = chkMASALAHKEP_4.Checked
                .MASALAHKEP_5 = chkMASALAHKEP_5.Checked
                .MASALAHKEP_6 = chkMASALAHKEP_6.Checked
                .MASALAHKEP_7 = chkMASALAHKEP_7.Checked
                .MASALAHKEP_8 = chkMASALAHKEP_8.Checked
                .MASALAHKEP_9 = chkMASALAHKEP_9.Checked
                .MASALAHKEP_10 = chkMASALAHKEP_10.Checked
                .MASALAHKEP_11 = chkMASALAHKEP_11.Checked
                .MASALAHKEP_12 = chkMASALAHKEP_12.Checked
                .MASALAHKEP_13 = chkMASALAHKEP_13.Checked
                .MASALAHKEP_14 = chkMASALAHKEP_14.Checked
                .MASALAHKEP_15 = chkMASALAHKEP_15.Checked
                .MASALAHKEP_16 = chkMASALAHKEP_16.Checked
                .MASALAHKEP_17 = chkMASALAHKEP_17.Checked
                .MASALAHKEP_18 = chkMASALAHKEP_18.Checked
                .MASALAHKEP_19 = chkMASALAHKEP_19.Checked
                .MASALAHKEP_20 = chkMASALAHKEP_20.Checked
                .MASALAHKEP_21 = chkMASALAHKEP_21.Checked
                .MASALAHKEP_22 = chkMASALAHKEP_22.Checked
                .MASALAHKEP_23 = chkMASALAHKEP_23.Checked
                .MASALAHKEP_24 = chkMASALAHKEP_24.Checked
                .MASALAHKEP_25 = chkMASALAHKEP_25.Checked
                .MASALAHKEP_26 = chkMASALAHKEP_26.Checked
                .MASALAHKEP_27 = chkMASALAHKEP_27.Checked
                .MASALAHKEP_28 = chkMASALAHKEP_28.Checked
                .MASALAHKEP_29 = chkMASALAHKEP_29.Checked
                .MASALAHKEP_30 = chkMASALAHKEP_30.Checked
                .MASALAHKEP_31 = chkMASALAHKEP_31.Checked
                .MASALAHKEP_32 = chkMASALAHKEP_32.Checked
                .MASALAHKEP_33 = chkMASALAHKEP_33.Checked
                .MASALAHKEP_34 = chkMASALAHKEP_34.Checked
                .MASALAHKEP_35 = chkMASALAHKEP_35.Checked
                .MASALAHKEP_36 = chkMASALAHKEP_36.Checked
                .MASALAHKEP_37 = chkMASALAHKEP_37.Checked
                .MASALAHKEP_38 = chkMASALAHKEP_38.Checked
                .MASALAHKEP_39 = chkMASALAHKEP_39.Checked
                .MASALAHKEP_40 = chkMASALAHKEP_40.Checked
                .MASALAHKEP_41 = chkMASALAHKEP_41.Checked
                .MASALAHKEP_42 = chkMASALAHKEP_42.Checked
                .MASALAHKEP_43 = chkMASALAHKEP_43.Checked
                .MASALAHKEP_44 = chkMASALAHKEP_44.Checked
                .MASALAHKEP_44_TEXT = txtMASALAHKEP_44_TEXT.Text

                'tambahan
                .POLANAFAS = chkPOLANAFAS.Checked
                .RESIKOASPIRASI = chkRESIKOASPIRASI.Checked
                .RESIKOPENURUNANCURAHJANTUNG = chkRESIKOPENURUNANCURAHJANTUNG.Checked
                .RESIKOPENDARAHAN = chkRESIKOPENDARAHAN.Checked
                .BERATBADANBERLEBIH = chkBERATBADANBERLEBIH.Checked
                .DISFUNGSIMGAS = chkDISFUNGSIMGAS.Checked
                .KESIAPANPNUTRISI = chkKESIAPANPNUTRISI.Checked
                .MENYUSUITIDAKEFEKTIF = chkMENYUSUITIDAKEFEKTIF.Checked
                .INURIN = chkINURIN.Checked
                .INFEKAL = chkINFEKAL.Checked
                .PENINGKATANTIDUR = chkPENINGKATANTIDUR.Checked
                .GANGGUANMEMORI = chkGANGGUANMEMORI.Checked
                .GANGGUANMENELAN = chkGANGGUANMENELAN.Checked
                .KONFUSIAKUT = chkKONFUSIAKUT.Checked
                .KONFUSIKROSNIS = chkKONFUSIKROSNIS.Checked
                .POLASEKSUALTE = chkPOLASEKSUALTE.Checked
                .NYERIMELAHIRKAN = chkNYERIMELAHIRKAN.Checked
                .HARGADIRIRENDAH = chkHARGADIRIRENDAH.Checked
                .KESIAPANKOPING = chkKESIAPANKOPING.Checked
                .SINDROMPASCATRAUMA = chkSINDROMPASCATRAUMA.Checked
                .GANGGUANPERTUMBUHAN = chkGANGGUANPERTUMBUHAN.Checked
                .GANGGUANPERKEMBANGAN = chkGANGGUANPERKEMBANGAN.Checked
                .KPMANAJEMENKES = chkKPMANAJEMENKES.Checked
                .KESIAPANPP = chkKESIAPANPP.Checked
                .PEMELIHARAANKES = chkPEMELIHARAANKES.Checked
                .GANGGUANINTERAKSISOSIAL = chkGANGGUANINTERAKSISOSIAL.Checked
                .KESIAPANPPKEL = chkKESIAPANPPKEL.Checked
                .RESIKOBUNDIR = chkRESIKOBUNDIR.Checked
                .RISIKOCEDERAPADAIBU = chkRISIKOCEDERAPADAIBU.Checked
                .CEDERAPADAJANIN = chkCEDERAPADAJANIN.Checked
                .RESIKOALERGI = chkRESIKOALERGI.Checked


            End With

            ' ***** DETIL *****
            Dim arrDetail = oDigital.GetStructureDetailList
            For i As Integer = 0 To grvDetil.RowCount - 1
                Dim dsDetail = oDigital.GetStructureDetail
                With dsDetail
                    .SEQ = i
                    .KDPENDAFTARAN = ds.KDPENDAFTARAN
                    .LABORATORIUM = grvDetil.GetRowCellValue(i, colLaboratorium)
                    .RADIOLOGI = grvDetil.GetRowCellValue(i, colRadiologi)
                    .DIAGNOSTIKLAIN = grvDetil.GetRowCellValue(i, colDiagnostik)

                End With
                arrDetail.Add(dsDetail)
            Next

            ' ***** DETIL RESIKO JATUH *****
            Dim arrDetail2 = oDigital.GetStructureDetailList2
            For i As Integer = 0 To grvResikoJatuh.RowCount - 2
                Dim dsDetail2 = oDigital.GetStructureDetail2
                With dsDetail2
                    .KDPENDAFTARAN = ds.KDPENDAFTARAN
                    .SEQ = i
                    .HARI = grvResikoJatuh.GetRowCellValue(i, colHari)
                    .TANGGAL = grvResikoJatuh.GetRowCellValue(i, colTanggal)
                    .A = grvResikoJatuh.GetRowCellValue(i, colA)
                    .B = grvResikoJatuh.GetRowCellValue(i, colB)
                    .C = grvResikoJatuh.GetRowCellValue(i, colC)
                    .D = grvResikoJatuh.GetRowCellValue(i, colD)
                    .E = grvResikoJatuh.GetRowCellValue(i, colE)
                    .F = grvResikoJatuh.GetRowCellValue(i, colF)
                    .G = grvResikoJatuh.GetRowCellValue(i, colG)
                    .NAMAPETUGAS = grvResikoJatuh.GetRowCellValue(i, colNamaPetugas)
                End With
                arrDetail2.Add(dsDetail2)
            Next

            ' ***** DETIL MONITORING RESIKO JATUH *****
            Dim arrDetail3 = oDigital.GetStructureDetailList3
            For i As Integer = 0 To grvMonitoringResikoJatuh.RowCount - 2
                Dim dsDetail3 = oDigital.GetStructureDetail3
                With dsDetail3
                    .KDPENDAFTARAN = ds.KDPENDAFTARAN
                    .SEQ = i
                    .HARI = grvMonitoringResikoJatuh.GetRowCellValue(i, colHari2)
                    .TANGGAL = grvMonitoringResikoJatuh.GetRowCellValue(i, colTanggal2)
                    .A = grvMonitoringResikoJatuh.GetRowCellValue(i, colA2)
                    .B = grvMonitoringResikoJatuh.GetRowCellValue(i, colB2)
                    .C = grvMonitoringResikoJatuh.GetRowCellValue(i, colC2)
                    .D = grvMonitoringResikoJatuh.GetRowCellValue(i, colD2)
                    .E = grvMonitoringResikoJatuh.GetRowCellValue(i, colE2)
                    .F = grvMonitoringResikoJatuh.GetRowCellValue(i, colF2)
                    .G = grvMonitoringResikoJatuh.GetRowCellValue(i, colG2)
                    .NAMAPETUGAS = grvMonitoringResikoJatuh.GetRowCellValue(i, colNamaPetugas2)

                End With
                arrDetail3.Add(dsDetail3)
            Next

            If oFormMode = FORM_MODE.FORM_MODE_ADD Then
                Try
                    fn_Save = oDigital.InsertData(ds, arrDetail, arrDetail2, arrDetail3)
                Catch ex As Exception
                    MsgBox("Simpan Data : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            ElseIf oFormMode = FORM_MODE.FORM_MODE_EDIT Then
                Try
                    fn_Save = oDigital.UpdateData(ds, arrDetail, arrDetail2, arrDetail3)
                Catch ex As Exception
                    MsgBox("Simpan Data : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            End If
        Catch oErr As Exception
            MsgBox("Simpan Data : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            fn_Save = False
        End Try
    End Function

#End Region
#Region "Command Button"
    Private Sub frmItem_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        Select Case e.KeyCode
            Case Keys.F12
                btnClose_Click()
            Case Keys.F3
                If btnSaveClose.Enabled = True Then
                    btnSaveClose_Click()
                End If
            Case Keys.PageUp
	            fn_ScrollPage(True)
            Case Keys.PageDown
	            fn_ScrollPage(False)
        End Select
    End Sub
    Private Sub btnSaveClose_Click() Handles btnSaveClose.ItemClick
        If fn_Validate() = False Then Exit Sub
        If MsgBox("Save " & txtNoRegister.Text.Trim.ToUpper & "?", MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, Me.Text) = MsgBoxResult.No Then Exit Sub
        If fn_Save() = False Then
            MsgBox("Save gagal! Tolong kontak admin anda!", MsgBoxStyle.Exclamation, Me.Text)
        Else
            MsgBox("Save " & txtNoRegister.Text.Trim.ToUpper & " success!", MsgBoxStyle.Information, Me.Text)
            Me.Close()
        End If
    End Sub
    Private Sub btnClose_Click() Handles btnClose.ItemClick
        Me.Close()
    End Sub
#End Region
#Region "Lookup / Event"
    Private Sub btnTAMBAH_Click(sender As Object, e As EventArgs) Handles btnTAMBAH.Click
        'Dim frmPopUpEMedrekRI_38 As New frmPopUpEMedrekRI_38
        'frmPopUpEMedrekRI_38.fn_LoadMe("", "", "")
        'frmPopUpEMedrekRI_38.ShowDialog()

        'If sFind1 <> String.Empty Then
        '    grvDetil.Focus()
        '    grvDetil.AddNewRow()
        '    grvDetil.SetFocusedRowCellValue(colLaboratorium, sFind1)
        '    grvDetil.SetFocusedRowCellValue(colRadiologi, sFind2)
        '    grvDetil.SetFocusedRowCellValue(colDiagnostik, sFind3)
        '    grvDetil.UpdateCurrentRow()
        'End If

        'sFind1 = String.Empty
        'sFind2 = String.Empty
        'sFind3 = String.Empty

        'grvDetil.Focus()
    End Sub
    Private Sub EditToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles EditToolStripMenuItem.Click
        'Dim frmPopUpEMedrekRI_38 As New frmPopUpEMedrekRI_38
        'frmPopUpEMedrekRI_38.fn_LoadMe(grvDetil.GetFocusedRowCellValue(colLaboratorium), grvDetil.GetFocusedRowCellValue(colRadiologi), grvDetil.GetFocusedRowCellValue(colDiagnostik))
        'frmPopUpEMedrekRI_38.ShowDialog()

        'If sFind1 <> String.Empty Or sFind2 <> String.Empty Or sFind3 <> String.Empty Then
        '    grvDetil.SetFocusedRowCellValue(colLaboratorium, sFind1)
        '    grvDetil.SetFocusedRowCellValue(colRadiologi, sFind2)
        '    grvDetil.SetFocusedRowCellValue(colDiagnostik, sFind3)

        '    grvDetil.UpdateCurrentRow()
        'End If

        'sFind1 = String.Empty
        'sFind2 = String.Empty
        'sFind3 = String.Empty

        'grvDetil.Focus()
    End Sub

    Private Sub DeleteToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DeleteToolStripMenuItem.Click
        grvDetil.DeleteSelectedRows()
    End Sub

    Private Sub txtASESMEN_490_EditValueChanged(sender As Object, e As EventArgs) Handles txtASESMEN_490.SelectedIndexChanged, txtASESMEN_491.SelectedIndexChanged, txtASESMEN_492.SelectedIndexChanged, txtASESMEN_493.SelectedIndexChanged, txtASESMEN_494.SelectedIndexChanged
        If isLoad Then
            Dim sNilai1 As Integer = 0
            Dim sNilai2 As Integer = 0
            Dim sNilai3 As Integer = 0
            Dim sNilai4 As Integer = 0
            Dim sNilai5 As Integer = 0

            sNilai1 = CInt(txtASESMEN_490.Text)
            sNilai2 = CInt(txtASESMEN_491.Text)
            sNilai3 = CInt(txtASESMEN_492.Text)
            sNilai4 = CInt(txtASESMEN_493.Text)
            sNilai5 = CInt(txtASESMEN_494.Text)

            txtASESMEN_495.Text = sNilai1 + sNilai2 + sNilai3 + sNilai4 + sNilai5

            fn_Warna(CInt(txtASESMEN_495.Text))
        End If
    End Sub
    Private Sub fn_Warna(ByVal sNilai As Integer)
        If sNilai >= 2 Then
            txtASESMEN_495.Properties.Appearance.BackColor = Color.Red
        Else
            txtASESMEN_495.Properties.Appearance.BackColor = Color.LightGreen
        End If
    End Sub

    Private Sub DeleteToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles DeleteToolStripMenuItem1.Click
        grvResikoJatuh.DeleteSelectedRows()
    End Sub

    Private Sub DeleteToolStripMenuItem2_Click(sender As Object, e As EventArgs) Handles DeleteToolStripMenuItem2.Click
        grvMonitoringResikoJatuh.DeleteSelectedRows()
    End Sub

    Private Sub grvResikoJatuh_CellValueChanged(sender As Object, e As DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs) Handles grvResikoJatuh.CellValueChanged
        If e.Column.Name = colF.Name Then
            Try
                If grvResikoJatuh.GetFocusedRowCellValue(colF) IsNot Nothing Then
                    Dim sTotal As Integer = 0
                    Dim sa As Integer = CInt(grvResikoJatuh.GetFocusedRowCellValue(colA))
                    Dim sb As Integer = CInt(grvResikoJatuh.GetFocusedRowCellValue(colB))
                    Dim sc As Integer = CInt(grvResikoJatuh.GetFocusedRowCellValue(colC))
                    Dim sd As Integer = CInt(grvResikoJatuh.GetFocusedRowCellValue(colD))
                    Dim se As Integer = CInt(grvResikoJatuh.GetFocusedRowCellValue(colE))
                    Dim sf As Integer = CInt(grvResikoJatuh.GetFocusedRowCellValue(colF))

                    sTotal = sa + sb + sc + sd + se + sf

                    grvResikoJatuh.SetFocusedRowCellValue(colG, sTotal)
                End If
            Catch oErr As Exception
                MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            End Try
        End If
    End Sub

    Private Sub frmEMedrekRI_38_MouseWheel(sender As Object, e As MouseEventArgs) Handles MyBase.MouseWheel
	    If e.Delta > 0 Then
		    'up
		    fn_ScrollPage(True)
	    Else
		    'down
		    fn_ScrollPage(False)
	    End If
    End Sub

    Private Sub fn_ScrollPage(ByVal isUp As Boolean)
	    Dim myView As Point = Me.Panel3.AutoScrollPosition
	    Dim scrollchange As Integer = 50

	    If isUp Then
		    'up
		    myView.X = -myView.X
		    myView.y = -scrollchange - myView.Y
	    Else
		    'down
		    myView.X = -myView.X
		    myView.y = scrollchange - myView.Y
	    End If

	    Me.Panel3.AutoScrollPosition = myView
    End Sub


#End Region
End Class