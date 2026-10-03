Imports DataAccess
Imports UI.WIN.MAIN.My.Resources
Imports System.Linq
Imports Newtonsoft.Json.Linq
Imports System.Data.SqlClient

Public Class frmReportOrderPenunjang
    Private oFormMode As FORM_MODE = FORM_MODE.FORM_MODE_VIEW
    Private sKategori As String = String.Empty
#Region "Function"
    Public Sub fn_LoadKategori(ByVal Paramater As String)
        sKategori = Paramater
    End Sub
    Private Sub fn_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        deDATE.DateTime = Now
        'fn_LoadSecurity()
        fn_Preview()

        sListRincianLab.Clear()
        sListRincianRad.Clear()
    End Sub
    'Private Sub fn_LoadSecurity()
    '    Try
    '        Dim oOtority As New Setting.clsOtority
    '        Dim oUser As New Setting.clsUser

    '        Dim ds = (From x In oOtority.GetDataDetail
    '                  Join y In oUser.GetData
    '                  On x.KDOTORITY Equals y.KDOTORITY
    '                  Where x.MODUL = "REQ_PENUNJANG" _
    '                  And y.KDUSER = sUserID
    '                  Select x.ISADD, x.ISDELETE, x.ISUPDATE, x.ISPRINT, x.ISVIEW).FirstOrDefault
    '        Try
    '            If ds.ISVIEW = True Then
    '                fn_Preview()
    '            End If
    '        Catch ex As Exception
    '            MsgBox("Keamanan belum dipasang, tolong kontak admin anda!", MsgBoxStyle.Exclamation, Me.Text)
    '        End Try
    '    Catch ex As Exception
    '        MsgBox("Load Security : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
    '    End Try
    'End Sub
    Private Function fn_Validate() As Boolean
        fn_Validate = True

        'If deDATETo.DateTime.ToString("yyyyMMdd") < deDATEFrom.DateTime.ToString("yyyyMMdd") Then
        '    MsgBox("Tanggal Sampai harus lebih besar dari Tanggal Dari!", MsgBoxStyle.OkOnly, Me.Text)
        '    fn_Validate = False
        '    Exit Function
        'End If
    End Function
    Private Sub fn_Print()
        If fn_Validate() Then
            'Try
            '    PrintableComponentLink.Landscape = True
            '    PrintableComponentLink.PaperKind = Printing.PaperKind.A4

            '    Dim phf As PageHeaderFooter =
            'TryCast(PrintableComponentLink.PageHeaderFooter, PageHeaderFooter)
            '    phf.Header.Content.Clear()
            '    phf.Header.Font = New Font("Times New Roman", 14, FontStyle.Bold)
            '    phf.Header.LineAlignment = BrickAlignment.Center
            '    phf.Footer.Font = New Font("Times New Roman", 9.75)
            '    phf.Footer.LineAlignment = BrickAlignment.Far
            '    phf.Footer.Content.AddRange(New String() _
            '{"", "", "Halaman: [Page # of Pages #]"})

            '    phf.Header.Content.AddRange(New String() _
            '{"", "PERMINTAAN", ""})


            '    printableComponentLink.CreateDocument()
            '    PrintableComponentLink.ShowPreview()
            'Catch ex As Exception
            '    MsgBox("Print Data : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
            'End Try
        End If
    End Sub
    Private Sub fn_Preview()
        If fn_Validate() Then
            Try
                grv_2.Columns.Clear()
                grd_2.DataSource = Nothing
                grv_2.OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleIfExpanded
                fn_Load01()
            Catch oErr As Exception
                MsgBox("Preview Data : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            End Try
        End If
    End Sub
    Private Sub fn_Load01()
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
            SQL &= "A.KDTARIF "
            'SQL &= ",KATEGORI = (SELECT CASE A.KATEGORI WHEN 'LAINNYA' THEN (SELECT CASE B.DESCRIPTION WHEN 'TARIF LABORATORIUM' THEN 'LAINNYA LAB' ELSE 'LAINNYA RAD' END) ELSE A.KATEGORI END) "
            SQL &= ",A.KATEGORI "
            SQL &= ",A.KATEGORI_SUB "
            SQL &= ",A.NAMA_TARIF "
            SQL &= "FROM "
            SQL &= "M_TARIF_NEW A "
            SQL &= "INNER JOIN M_KODETARIF_NEW B "
            SQL &= "ON A.KDKODETARIF = B.KDKODETARIF "
            SQL &= "WHERE "
            SQL &= "B.DESCRIPTION = 'TARIF LABORATORIUM' "
            SQL &= "AND A.KATEGORI <> '' "
            'SQL &= "OR "
            'SQL &= "B.DESCRIPTION = 'TARIF LABORATORIUM' "
            'SQL &= "AND A.KATEGORI <> '' "
            SQL &= "ORDER BY "
            SQL &= "A.NAMA_TARIF "
            'SQL &= "B.DESCRIPTION "
            'SQL &= "A.KATEGORI "
            'SQL &= ",A.KATEGORI_SUB "

            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "M_TARIF_LAB")

            If oConn.State = ConnectionState.Open Then
                oConn.Close()
            End If

            Dim listLab As New List(Of DataAccess.R_ITEM_ORDER)
            Dim sSeqLab As Integer = 0

            For iLoop As Integer = 0 To ds.Tables("M_TARIF_LAB").Rows.Count - 1
                Dim dsRekap As New DataAccess.R_ITEM_ORDER
                sSeqLab += 1
                With ds.Tables("M_TARIF_LAB")
                    dsRekap.SEQ = sSeqLab
                    dsRekap.KDTARIF = .Rows(iLoop)("KDTARIF")
                    dsRekap.NAMA_TARIF = .Rows(iLoop)("NAMA_TARIF")
                    dsRekap.KATEGORI = .Rows(iLoop)("KATEGORI")
                    dsRekap.KATEGORI_SUB = .Rows(iLoop)("KATEGORI_SUB")
                    dsRekap.ISCHEKED = False
                End With
                listLab.Add(dsRekap)
            Next

            Dim list_1 As New List(Of DataAccess.R_ITEM_ORDER)
            Dim list_2 As New List(Of DataAccess.R_ITEM_ORDER)

            For Each xloop In listLab
                If xloop.SEQ <= 25 Then
                    Dim dsRekap As New DataAccess.R_ITEM_ORDER
                    dsRekap.SEQ = xloop.SEQ
                    dsRekap.KDTARIF = xloop.KDTARIF
                    dsRekap.NAMA_TARIF = xloop.NAMA_TARIF
                    dsRekap.KATEGORI = xloop.KATEGORI & " " & xloop.KATEGORI_SUB
                    dsRekap.KATEGORI_SUB = xloop.KATEGORI_SUB
                    dsRekap.ISCHEKED = False
                    list_1.Add(dsRekap)
                Else
                    Dim dsRekap As New DataAccess.R_ITEM_ORDER
                    dsRekap.SEQ = xloop.SEQ
                    dsRekap.KDTARIF = xloop.KDTARIF
                    dsRekap.NAMA_TARIF = xloop.NAMA_TARIF
                    dsRekap.KATEGORI = xloop.KATEGORI & " " & xloop.KATEGORI_SUB
                    dsRekap.KATEGORI_SUB = xloop.KATEGORI_SUB
                    dsRekap.ISCHEKED = False
                    list_2.Add(dsRekap)
                End If
            Next

            grd_1.DataSource = list_1.OrderBy(Function(x) x.NAMA_TARIF)
            grd_2.DataSource = list_2.OrderBy(Function(x) x.NAMA_TARIF)


            SQL = "SELECT "
            SQL &= "A.KDTARIF "
            'SQL &= ",KATEGORI = (SELECT CASE A.KATEGORI WHEN 'LAINNYA' THEN (SELECT CASE B.DESCRIPTION WHEN 'TARIF LABORATORIUM' THEN 'LAINNYA LAB' ELSE 'LAINNYA RAD' END) ELSE A.KATEGORI END) "
            SQL &= ",A.KATEGORI "
            SQL &= ",A.KATEGORI_SUB "
            SQL &= ",A.NAMA_TARIF "
            SQL &= "FROM "
            SQL &= "M_TARIF_NEW A "
            SQL &= "INNER JOIN M_KODETARIF_NEW B "
            SQL &= "ON A.KDKODETARIF = B.KDKODETARIF "
            SQL &= "WHERE "
            SQL &= "B.DESCRIPTION = 'TARIF RADIOLOGI' "
            SQL &= "AND A.KATEGORI <> '' "
            SQL &= "ORDER BY "
            SQL &= "A.NAMA_TARIF "
            'SQL &= "B.DESCRIPTION "
            'SQL &= "A.KATEGORI "
            'SQL &= ",A.KATEGORI_SUB "

            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "M_TARIF_RAD")

            If oConn.State = ConnectionState.Open Then
                oConn.Close()
            End If

            Dim listRad As New List(Of DataAccess.R_ITEM_ORDER)
            Dim sSeqRad As Integer = 0

            For iLoop As Integer = 0 To ds.Tables("M_TARIF_RAD").Rows.Count - 1
                Dim dsRekap As New DataAccess.R_ITEM_ORDER
                sSeqRad += 1
                With ds.Tables("M_TARIF_RAD")
                    dsRekap.SEQ = sSeqRad
                    dsRekap.KDTARIF = .Rows(iLoop)("KDTARIF")
                    dsRekap.NAMA_TARIF = .Rows(iLoop)("NAMA_TARIF")
                    dsRekap.KATEGORI = .Rows(iLoop)("KATEGORI")
                    dsRekap.KATEGORI_SUB = .Rows(iLoop)("KATEGORI_SUB")
                    dsRekap.ISCHEKED = False
                End With
                listRad.Add(dsRekap)
            Next

            Dim list_3 As New List(Of DataAccess.R_ITEM_ORDER)
            Dim list_4 As New List(Of DataAccess.R_ITEM_ORDER)

            For Each xloop In listRad
                If xloop.SEQ <= 25 Then
                    Dim dsRekap As New DataAccess.R_ITEM_ORDER
                    dsRekap.SEQ = xloop.SEQ
                    dsRekap.KDTARIF = xloop.KDTARIF
                    dsRekap.NAMA_TARIF = xloop.NAMA_TARIF
                    dsRekap.KATEGORI = xloop.KATEGORI & " " & xloop.KATEGORI_SUB
                    dsRekap.KATEGORI_SUB = xloop.KATEGORI_SUB
                    dsRekap.ISCHEKED = False
                    list_3.Add(dsRekap)
                Else
                    Dim dsRekap As New DataAccess.R_ITEM_ORDER
                    dsRekap.SEQ = xloop.SEQ
                    dsRekap.KDTARIF = xloop.KDTARIF
                    dsRekap.NAMA_TARIF = xloop.NAMA_TARIF
                    dsRekap.KATEGORI = xloop.KATEGORI & " " & xloop.KATEGORI_SUB
                    dsRekap.KATEGORI_SUB = xloop.KATEGORI_SUB
                    dsRekap.ISCHEKED = False
                    list_4.Add(dsRekap)
                End If
            Next

            grd_3.DataSource = list_3.OrderBy(Function(x) x.NAMA_TARIF)
            grd_4.DataSource = list_4.OrderBy(Function(x) x.NAMA_TARIF)

            fn_SetFormat()

            If sKategori = "LABORATORIUM" Then
                l_3.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                l_4.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            ElseIf sKategori = "RADIOLOGI" Then
                l_1.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                l_2.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            End If
        Catch oErr As Exception
            MsgBox("Preview Data : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_SetFormat()
        grv_1.Columns("KDTARIF").VisibleIndex = -1
        grv_2.Columns("KDTARIF").VisibleIndex = -1
        grv_3.Columns("KDTARIF").VisibleIndex = -1
        grv_4.Columns("KDTARIF").VisibleIndex = -1

        grv_1.Columns("SEQ").VisibleIndex = -1
        grv_2.Columns("SEQ").VisibleIndex = -1
        grv_3.Columns("SEQ").VisibleIndex = -1
        grv_4.Columns("SEQ").VisibleIndex = -1

        grv_1.Columns("KATEGORI_SUB").VisibleIndex = -1
        grv_2.Columns("KATEGORI_SUB").VisibleIndex = -1
        grv_3.Columns("KATEGORI_SUB").VisibleIndex = -1
        grv_4.Columns("KATEGORI_SUB").VisibleIndex = -1

        grv_1.Columns("KATEGORI").Group()
        grv_1.ExpandAllGroups()

        grv_1.BestFitColumns()

        grv_2.Columns("KATEGORI").Group()
        grv_2.ExpandAllGroups()

        grv_2.BestFitColumns()

        grv_3.Columns("KATEGORI").Group()
        grv_3.ExpandAllGroups()

        grv_3.BestFitColumns()

        grv_4.Columns("KATEGORI").Group()
        grv_4.ExpandAllGroups()

        grv_4.BestFitColumns()

        grv_1.Columns("NAMA_TARIF").Caption = "Laboratorium"
        grv_2.Columns("NAMA_TARIF").Caption = "Laboratorium"
        grv_3.Columns("NAMA_TARIF").Caption = "Radiologi"
        grv_4.Columns("NAMA_TARIF").Caption = "Radiologi"

        grv_1.Columns("ISCHEKED").Caption = "Aksi"
        grv_2.Columns("ISCHEKED").Caption = "Aksi"
        grv_3.Columns("ISCHEKED").Caption = "Aksi"
        grv_4.Columns("ISCHEKED").Caption = "Aksi"
    End Sub
#End Region
#Region "Command Button"
    Private Sub frmMember_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        Select Case e.KeyCode
            Case Keys.Escape
                Me.Close()
                'Case Keys.R
                '    If e.Alt = True And picRefresh.Enabled = True Then
                '        picRefresh_Click()
                '    End If
        End Select
    End Sub
    Private Sub btnPilih_Click(sender As Object, e As EventArgs) Handles btnPilih.Click
        sDatePemeriksaan = deDATE.DateTime
        For i As Integer = 0 To grv_1.RowCount - 2
            If grv_1.GetRowCellValue(i, "ISCHEKED") = True Then
                sListRincianLab.Add(grv_1.GetRowCellValue(i, "NAMA_TARIF"))
            End If
        Next
        For i As Integer = 0 To grv_2.RowCount - 2
            If grv_2.GetRowCellValue(i, "ISCHEKED") = True Then
                sListRincianLab.Add(grv_2.GetRowCellValue(i, "NAMA_TARIF"))
            End If
        Next
        For i As Integer = 0 To grv_3.RowCount - 2
            If grv_3.GetRowCellValue(i, "ISCHEKED") = True Then
                sListRincianRad.Add(grv_3.GetRowCellValue(i, "NAMA_TARIF"))
            End If
        Next
        For i As Integer = 0 To grv_4.RowCount - 2
            If grv_4.GetRowCellValue(i, "ISCHEKED") = True Then
                sListRincianRad.Add(grv_4.GetRowCellValue(i, "NAMA_TARIF"))
            End If
        Next
        Me.Close()
    End Sub
    'Private Sub picRefresh_Click() Handles picRefresh.Click
    '    fn_Preview()
    'End Sub
#End Region
End Class