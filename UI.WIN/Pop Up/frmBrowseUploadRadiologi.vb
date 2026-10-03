Imports System.Linq
Imports System.IO
Imports iTextSharp.text.pdf
Imports DataAccess
Imports System.Data.SqlClient
Imports Newtonsoft.Json.Linq
Imports System.Net

Public Class frmBrowseUploadRadiologi
    Public Sub fn_LoadMe(ByVal KDCUSTOMER As String, ByVal NAMA As String, ByVal TTL As String, ByVal JK As String, ByVal KODE As String, ByVal kdreg As String)
        Try
            Me.Text = KDCUSTOMER & " - " & NAMA & " (" & JK & ")"

            Dim oKoneksi As New Brigging.clsSetKoneksi

            '"R23.07694"
            Dim ds = oKoneksi.GetDataRadiologi(KODE, sAlamatBriggingRadiologi)

            If ds <> "" Then
                Dim allData = JObject.Parse(ds)

                Dim CodeResponse As String = IIf(IsDBNull(allData.Item("metaData").Item("code")) = True, "", allData.Item("metaData").Item("code"))
                Dim messageResponse As String = allData("metaData")("message").ToString
                If CodeResponse = "200" Then
                    LabelControl2.Text = allData("response")("Status").ToString()

                    Try
                        'Gambar
                        Dim table As DataTable

                        table = New DataTable("I_REPOT_REKAP")
                        table.Columns.Add("Keterangan")
                        table.Columns.Add("Alamat")

                        Dim i As Integer = 0
                        Dim Nomor As Integer = 1

                        Dim tes = allData("response")("image")

                        For Each item In allData("response")("image")
                            table.Rows.Add(New String() {"Foto " & Nomor, item})
                            Nomor += 1
                            i += 1
                        Next

                        grd.DataSource = table
                        grd.ForceInitialize()

                        grv.Columns("Alamat").VisibleIndex = -1
                    Catch ex As Exception
                        MsgBox("Gambar : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
                    End Try

                    Try
                        'URL
                        txtURL.Text = allData("response")("LinkViewer").ToString()
                    Catch ex As Exception
                        MsgBox("URL : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
                    End Try

                    Try
                        RichTextBox1.Rtf = RenderHTML(allData("response")("Expertise").ToString())
                    Catch ex As Exception
                        MsgBox("RTF : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
                    End Try
                Else
                    MsgBox(CodeResponse & "-" & messageResponse, MsgBoxStyle.Information, Me.Text)
                End If

                fn_LoadDataPendaftaran(kdreg)
            End If
        Catch oErr As Exception
            MsgBox("Browser Data : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
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
                    'HttpContext.Current.Response.Write("UNSUPPORTED : " + Mid(strInput, 1, intPos) + "<br/>")
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
    Private Sub Form_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        sForm0 = False
    End Sub
    Private Sub Form_Closing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        PictureEdit1.Image = Nothing
        Dispose()
    End Sub
    Private Sub fn_LoadDataPendaftaran(ByVal KDREG As String)
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
            SQL &= "S_RADIOLOGI_H A "
            SQL &= "WHERE A.KDREG = '" & KDREG & "' "

            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "S_LISTPASIEN")

            If oConn.State = ConnectionState.Open Then
                oConn.Close()
            End If

            For iLoop As Integer = 0 To ds.Tables("S_LISTPASIEN").Rows.Count - 1
                With ds.Tables("S_LISTPASIEN")
                    'RichTextBox2.Text = .Rows(iLoop)("DESCRIPTION")
                End With
            Next

        Catch oErr As Exception
            MsgBox("Load List Pasien Data : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub SimpleButton1_Click(sender As Object, e As EventArgs) Handles SimpleButton1.Click
        If txtURL.Text <> "" Then
            Dim variabel As String = "CMD /c Start chrome /profile-directory=""Default"" """ & txtURL.Text & """"
            Shell(variabel, vbNormalFocus)
        End If
    End Sub
    Private Sub grv_FocusedRowChanged(ByVal sender As System.Object, ByVal e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs) Handles grv.FocusedRowChanged
        Try
            If grv.GetFocusedRowCellValue("Alamat") Is Nothing Then
                Exit Sub
            End If

            PictureEdit1.Image = Nothing

            Dim tClient As WebClient = New WebClient
            Dim url As String = grv.GetFocusedRowCellValue("Alamat")
            Dim tImage As Bitmap = Bitmap.FromStream(New MemoryStream(tClient.DownloadData(url)))

            PictureEdit1.Image = tImage

        Catch ex As Exception

        End Try
    End Sub
End Class