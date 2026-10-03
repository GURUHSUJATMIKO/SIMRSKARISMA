Imports System.Linq
Imports System.IO
Imports iTextSharp.text.pdf
Imports DataAccess
Imports System.Data.SqlClient

Public Class frmBrowseUpload
    Public Sub fn_LoadMe(ByVal Kategori As Integer, ByVal Type As String, ByVal FindBrowseFile As String, ByVal KDCUSTOMER As String, ByVal NAMA As String, ByVal TTL As String, ByVal JK As String, ByVal KODE As String)
        Try
            sForm0 = True

            lblkdreg.Text = KODE
            lblRM.Text = KDCUSTOMER
            lblNama.Text = NAMA
            lblTanggalLahir.Text = TTL
            lblJenisKelamin.Text = JK

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

            If Not Directory.Exists("C:/SIMRS") Then
                Directory.CreateDirectory("C:/SIMRS")
            End If

            If Kategori = 0 Then
                If Type = ".pdf" Then
                    lPDF.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                    PdfViewer1.LoadDocument(FindBrowseFile)
                ElseIf Type = "browser" Then
                    lPDF.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                    Dim sPDF As String = "\\192.168.2.79\Users\SIMRS\δupload berkas rmδ\" & KODE & "_" & CInt(KDCUSTOMER) & "_" & NAMA.Trim.ToString.Replace("'", "") & ".pdf"

                    If FileIO.FileSystem.FileExists(sPDF) Then
                        PdfViewer1.LoadDocument(sPDF)
                    Else
                        Dim sPDF2 As String = "\\192.168.2.79\Users\SIMRS\δupload berkas rmδ\" & KODE & "_" & CInt(KDCUSTOMER) & ".pdf"
                        If FileIO.FileSystem.FileExists(sPDF) Then
                            PdfViewer1.LoadDocument(sPDF2)
                        Else
                            PdfViewer1.CloseDocument()
                        End If
                    End If

                    'PdfViewer1.LoadDocument(fn_SearchFolderPdf(KODE, sAlamatSimpanPDFLaboratorium))
                End If
            ElseIf Kategori = 1 Then
                Try
                    If fn_Loadusg(KODE) = True Then
                        Dim rpt As New xtraUSG
                        rpt.ExportToPdf("C:/SIMRS/UPLOADUSG" & lblkdreg.Text & ".pdf")
                        PdfViewer1.LoadDocument("C:/SIMRS/UPLOADUSG" & lblkdreg.Text & ".pdf")
                    Else
                        fn_LoadNoRadiologi(KODE, "USG")
                    End If
                Catch oErr As Exception
                    MsgBox("Print Data USG: " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            ElseIf Kategori = 2 Then
                Try
                    Dim rpt As New xtraRadiologi

                    If fn_LoadRadiologi(KODE) = True Then
                        rpt.ExportToPdf("C:/SIMRS/UPLOADRAD" & lblkdreg.Text & ".pdf")
                        PdfViewer1.LoadDocument("C:/SIMRS/UPLOADRAD" & lblkdreg.Text & ".pdf")
                    Else
                        fn_LoadNoRadiologi(KODE, "RONTGEN")
                    End If
                Catch oErr As Exception
                    MsgBox("Print Data Rad: " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            End If
        Catch oErr As Exception
            MsgBox("Print Data : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub Form_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        sForm0 = False
    End Sub
    Private Sub Form_Closing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        PdfViewer1.CloseDocument()
        Dispose()
    End Sub
    Private Function fn_Loadusg(ByVal KDUSG As String) As Boolean
        Try
            fn_Loadusg = False

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
            SQL &= "A.NOUSG "
            SQL &= ",TANGGAL = A.DATE "
            SQL &= ",DOKTER = C.FRONT_TITLE + ' ' + C.NAME_DISPLAY + ' ' + C.BACK_TITLE "
            SQL &= ",A.KDTARIF "
            SQL &= ",A.GEJALA "
            SQL &= ",A.DESCRIPTION "
            SQL &= "FROM "
            SQL &= "S_USG_H A "
            SQL &= "INNER JOIN S_PENDAFTARAN_H B "
            SQL &= "ON A.KDREG = B.KDREG "
            SQL &= "INNER JOIN M_DOCTOR C "
            SQL &= "ON B.KDDOCTOR = C.KDDOCTOR "
            SQL &= "WHERE "
            SQL &= "A.KDUSG = " & KDUSG & " "

            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "SEARCHUOM")

            For iLoop As Integer = 0 To ds.Tables("SEARCHUOM").Rows.Count - 1
                With ds.Tables("SEARCHUOM")
                    fn_Loadusg = True

                    sNOMORUSG_RAD = .Rows(iLoop)("NOUSG")
                    sUMUR_RAD = lblTanggalLahir.Text
                    sKDCUSTOMER_RAD = lblRM.Text
                    sNAME_DISPLAY_RAD = lblNama.Text
                    sDATE_RAD = .Rows(iLoop)("TANGGAL")
                    sDEPARTMENT_RAD = ""
                    sREPORTDATE_RAD = ""
                    sEXAMPDESC_RAD = ""
                    sDOKTER_RAD = .Rows(iLoop)("DOKTER")
                    sDIAGNOSA_RAD = .Rows(iLoop)("GEJALA")
                    sDESCRIPTION_RAD = .Rows(iLoop)("DESCRIPTION")
                    sDOKTER_RAD2 = ""
                    sPEMERIKSAAN_RAD = .Rows(iLoop)("GEJALA")

                    'sNO_USG = .Rows(iLoop)("NOUSG")
                    'sDATE_USG = .Rows(iLoop)("TANGGAL")
                    'sKDTARIF_USG = .Rows(iLoop)("KDTARIF")
                    'sGEJALA_USG = .Rows(iLoop)("GEJALA")
                    'sDOKTER_USG = .Rows(iLoop)("DOKTER")
                    'sDESCRIPTION_USG = .Rows(iLoop)("DESCRIPTION")
                End With
            Next

            If oConn.State = ConnectionState.Open Then
                oConn.Close()
            End If
        Catch oErr As Exception
            MsgBox("Load Data Usg" & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Function
    Private Function fn_LoadRadiologi(ByVal KDRAD As String) As Boolean
        Try
            fn_LoadRadiologi = False

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

            SQL = "Select "
            SQL &= "A.KDRAD "
            SQL &= ",B.KDCUSTOMER "
            SQL &= ",TANGGAL = A.Date "
            SQL &= ",DOKTER = C.FRONT_TITLE + ' ' + C.NAME_DISPLAY + ' ' + C.BACK_TITLE "
            SQL &= ",DEPARTMENT = D.NAME_DISPLAY "
            SQL &= ",A.REPORTDATE "
            SQL &= ",A.EXAMDESC "
            SQL &= ",A.DIAGNOSA "
            SQL &= ",A.DESCRIPTION "
            SQL &= "FROM "
            SQL &= "S_RADIOLOGI_H A "
            SQL &= "INNER JOIN S_PENDAFTARAN_H B "
            SQL &= "ON A.KDREG = B.KDREG "
            SQL &= "INNER JOIN M_DOCTOR C "
            SQL &= "ON B.KDDOCTOR = C.KDDOCTOR "
            SQL &= "INNER JOIN M_DEPARTMENT D "
            SQL &= "ON B.KDDEPARTMENT = D.KDDEPARTMENT "
            SQL &= "WHERE "
            SQL &= "A.KDRAD = " & KDRAD & " "

            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "SEARCHUOM")

            For iLoop As Integer = 0 To ds.Tables("SEARCHUOM").Rows.Count - 1
                With ds.Tables("SEARCHUOM")
                    fn_LoadRadiologi = True

                    sKDCUSTOMER_RAD = .Rows(iLoop)("KDCUSTOMER")
                    sNAME_DISPLAY_RAD = lblNama.Text
                    sDATE_RAD = .Rows(iLoop)("TANGGAL")
                    sDEPARTMENT_RAD = .Rows(iLoop)("DEPARTMENT")
                    sREPORTDATE_RAD = .Rows(iLoop)("REPORTDATE")
                    sEXAMPDESC_RAD = .Rows(iLoop)("EXAMDESC")
                    sDOKTER_RAD = .Rows(iLoop)("DOKTER")
                    sDIAGNOSA_RAD = .Rows(iLoop)("DIAGNOSA")
                    sDESCRIPTION_RAD = .Rows(iLoop)("DESCRIPTION")
                End With
            Next

            If oConn.State = ConnectionState.Open Then
                oConn.Close()
            End If
        Catch oErr As Exception
            MsgBox("Load Data Usg" & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Function
    Private Sub fn_LoadNoRadiologi(ByVal KDRAD As String, ByVal Catatan As String)
        Try
            '"C:/HASILRADIOLGI/" & KodeRad & ".pdf"
            If Not Directory.Exists("C:/HASILRADIOLGI/") Then
                Directory.CreateDirectory("C:/HASILRADIOLGI/")
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
            SQL &= ",DOKTER = (CASE E.FRONT_TITLE WHEN '' THEN '' ELSE E.FRONT_TITLE END) + ' ' + E.NAME_DISPLAY + ' ' + (CASE E.BACK_TITLE WHEN '' THEN '' ELSE E.BACK_TITLE END) "
            SQL &= "FROM "
            SQL &= "S_TRANSAKSI_H A "
            SQL &= "INNER JOIN M_DOCTOR E "
            SQL &= "ON A.KDDOCTOR = E.KDDOCTOR "
            SQL &= "WHERE "
            SQL &= "A.KDTRANSAKSI = " & KDRAD & " "

            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "S_TRANSAKSI_H")

            Dim NoRadiologi As String = String.Empty
            Dim DOKTER As String = String.Empty
            Dim sDATE As DateTime = Now

            For iLoop As Integer = 0 To ds.Tables("S_TRANSAKSI_H").Rows.Count - 1
                With ds.Tables("S_TRANSAKSI_H")
                    NoRadiologi = .Rows(iLoop)("NORADIOLOGI")
                    sDATE = .Rows(iLoop)("DATE")
                    DOKTER = .Rows(iLoop)("DOKTER")
                End With
            Next

            If NoRadiologi <> "" Then
                frmRawatInapList.HasilBriggingRadiologi(False, NoRadiologi, Catatan, sDATE.ToString("yyyy-MM-dd HH:mm:ss"), DOKTER, False)
                PdfViewer1.LoadDocument("C:/HASILRADIOLGI/" & NoRadiologi & ".pdf")
            Else
                MsgBox("No Radiologi Tidak ada atau tidak ditemukan", MsgBoxStyle.Exclamation, Me.Text)
            End If

            If oConn.State = ConnectionState.Open Then
                oConn.Close()
            End If
        Catch oErr As Exception
            MsgBox("Load Data Transaksi" & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    'Private Function fn_SearchFolderPdf(ByVal sKode As String, ByVal sAlamatCari As String) As String
    '    Try
    '        Dim Folder As New DirectoryInfo(sAlamatCari)
    '        Dim fi As List(Of FileInfo) = New List(Of FileInfo)
    '        Dim arrfname As New List(Of String)()

    '        For Each File In Folder.GetFiles()
    '            If (File IsNot Nothing) Then
    '                'If (Path.GetExtension(File.ToString.ToLower) = ".pdf") Then
    '                '    If (File.ToString.ToLower.Contains(sKode)) Then
    '                '        fi.Add(File)
    '                '        arrfname.Add(File.FullName)
    '                '    End If
    '                'End If
    '                If File.ToString.Contains(sKode) Then
    '                    fi.Add(File)
    '                    arrfname.Add(File.FullName)
    '                End If

    '            End If
    '        Next

    '        If fi.Count > 0 Then
    '            Dim sinputFiles() As String = {}
    '            For Each ifname In arrfname

    '                sinputFiles = AppendArray(sinputFiles, ifname)

    '            Next

    '            MergePdfFiles(sinputFiles, "C:/SIMRS/BROWSER_" & sKode & ".pdf")

    '            fn_SearchFolderPdf = "C:/SIMRS/BROWSER_" & sKode & ".pdf"

    '            arrfname.Clear()
    '        Else
    '            fn_SearchFolderPdf = ""
    '        End If
    '    Catch oErr As Exception
    '        fn_SearchFolderPdf = ""
    '        MsgBox("Search Folder : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
    '    End Try
    'End Function
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
End Class