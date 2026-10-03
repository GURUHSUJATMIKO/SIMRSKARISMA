Imports DataAccess
Imports UI.WIN.MAIN.My.Resources
Imports System.Linq
Imports Newtonsoft.Json.Linq
Imports System.Data.SqlClient

Public Class frmRiwayatDiagnosa
    Private oFormMode As FORM_MODE = FORM_MODE.FORM_MODE_VIEW
    Private sIdentitas As String = String.Empty
    Private sNoRekamMedis As String = String.Empty

#Region "Function"
    Public Sub LoadMe(ByVal NoRekamMedis As String)
        sNoTransaksi = String.Empty
        sNoRekamMedis = NoRekamMedis
        sIdentitas = NoRekamMedis
        'Dim oCustomer As New Reference.clsCustomer
        'Dim dsCustomer = oCustomer.GetData(NoRekamMedis)
        'If dsCustomer IsNot Nothing Then
        '    sIdentitas = "Riwayat Resume " & dsCustomer.KDCUSTOMER & " - " & dsCustomer.NAME_DISPLAY
        'End If
    End Sub
    Private Sub fn_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.Text = sIdentitas
        fn_LoadSecurity()
    End Sub
    Private Sub fn_LoadSecurity()
        Try
            Dim oOtority As New Setting.clsOtority
            Dim oUser As New Setting.clsUser

            Dim ds = (From x In oOtority.GetDataDetail
                      Join y In oUser.GetData
                      On x.KDOTORITY Equals y.KDOTORITY
                      Where x.MODUL = "REQ_RECIPE" _
                      And y.KDUSER = sUserID
                      Select x.ISADD, x.ISDELETE, x.ISUPDATE, x.ISPRINT, x.ISVIEW).FirstOrDefault

            Try

                picPrint.Enabled = ds.ISPRINT
                picRefresh.Enabled = ds.ISVIEW

                If ds.ISVIEW = True Then
                    fn_Preview()
                End If
            Catch ex As Exception
                MsgBox("Keamanan belum dipasang, tolong kontak admin anda!", MsgBoxStyle.Exclamation, Me.Text)

                picPrint.Enabled = False
                picRefresh.Enabled = False
            End Try
        Catch ex As Exception
            MsgBox("Load Security : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Function fn_Validate() As Boolean
        fn_Validate = True

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
            '{"", sIdentitas & vbCrLf, ""})

            '    PrintableComponentLink.CreateDocument()
            '    PrintableComponentLink.ShowPreview()
            'Catch ex As Exception
            '    MsgBox("Print Data : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
            'End Try
        End If
    End Sub
    Private Sub fn_Preview()
        If fn_Validate() Then
            Try
                grv.Columns.Clear()
                grd.DataSource = Nothing
                grv.OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleIfExpanded

                fn_LoadDataRiwayatDiagnosa()

            Catch oErr As Exception
                MsgBox("Preview Data : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            End Try
        End If
    End Sub
    Private Sub fn_LoadDataRiwayatDiagnosa()
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
        SQL &= "Kategori = (SELECT CASE B.CATEGORY WHEN 1 THEN 'Rawat Jalan' ELSE 'Rawat Inap' END) "
        SQL &= ",NoRegister = A.KDPENDAFTARAN "
        SQL &= ",NoTransaksi = A.KDKODING "
        SQL &= ",Tanggal = A.DATE "
        'SQL &= ",KodeDiagnosa = G.KDDIAGNOSA "
        SQL &= ",Diagnosa = G.MEMO "
        SQL &= ",Tujuan = C.NAME_DISPLAY "
        SQL &= ",DPJP = E.NAME_DISPLAY "
        SQL &= "FROM "
        SQL &= "S_KODING_H A "
        SQL &= "INNER JOIN S_PENDAFTARAN_H B "
        SQL &= "ON A.KDPENDAFTARAN = B.KDPENDAFTARAN "
        SQL &= "INNER JOIN M_DEPARTMENT C "
        SQL &= "ON B.KDDEPARTMENT = C.KDDEPARTMENT "
        SQL &= "INNER JOIN M_DOCTOR E "
        SQL &= "ON B.KDDOCTOR = E.KDDOCTOR "
        SQL &= "INNER JOIN S_KODING_DX F "
        SQL &= "ON A.KDKODING = F.KDKODING "
        SQL &= "INNER JOIN M_DIAGNOSA G "
        SQL &= "ON F.KDDIAGNOSA = G.KDDIAGNOSA "
        SQL &= "WHERE B.KDCUSTOMER = '" & sNoRekamMedis & "' "
        'SQL &= "AND B.CATEGORY = 1 "
        SQL &= "ORDER BY A.KDPENDAFTARAN "

        oComm.Connection = oConn
        oComm.CommandText = SQL
        oComm.CommandTimeout = 120
        oComm.CommandType = CommandType.Text

        da = New SqlDataAdapter(oComm)
        da.Fill(ds, "S_LIS_H")

        grd.DataSource = ds.Tables("S_LIS_H")
        grd.ForceInitialize()
        fn_SetFormat()

        If oConn.State = ConnectionState.Open Then
            oConn.Close()
        End If
    End Sub
    Private Sub fn_SetFormat()
        For iLoop As Integer = 0 To grv.Columns.Count - 1
            If grv.Columns(iLoop).ColumnType.Name = "Decimal" Then
                grv.Columns(iLoop).DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                grv.Columns(iLoop).DisplayFormat.FormatString = "{0:n2}"
                grv.Columns(iLoop).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
                grv.GroupSummary.Add(DevExpress.Data.SummaryItemType.Sum, grv.Columns(iLoop).FieldName, grv.Columns(iLoop),
                                     "{0:n2}")
                grv.Columns(iLoop).SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                grv.Columns(iLoop).SummaryItem.DisplayFormat = "{0:n2}"
            ElseIf grv.Columns(iLoop).ColumnType.Name = "DateTime" Then
                grv.Columns(iLoop).DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
                grv.Columns(iLoop).DisplayFormat.FormatString = "{0:dd/MM/yyyy HH:mm:ss}"
            End If
        Next

        grv.Columns("NoRegister").VisibleIndex = -1
        grv.Columns("NoTransaksi").VisibleIndex = -1
        grv.Columns("Tanggal").Group()
        grv.ExpandAllGroups()

        grv.BestFitColumns()

    End Sub
#End Region
#Region "Command Button"
    Private Sub frmMember_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        Select Case e.KeyCode
            Case Keys.Escape
                Me.Close()
            Case Keys.P
                If e.Alt = True And picPrint.Enabled = True Then
                    picPrint_Click()
                End If
            Case Keys.R
                If e.Alt = True And picRefresh.Enabled = True Then
                    picRefresh_Click()
                End If
        End Select
    End Sub
    Private Sub picPrint_Click() Handles picPrint.Click
        Try
            fn_Print()
        Catch oErr As Exception
            MsgBox("Print Data : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub picRefresh_Click() Handles picRefresh.Click
        fn_Preview()
    End Sub
    Private Sub AmbilTransaksiToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AmbilTransaksiToolStripMenuItem.Click
        If grv.GetFocusedRowCellValue("NoTransaksi") Is Nothing Then
            sNoTransaksi = String.Empty
            Exit Sub
        End If

        sNoTransaksi = grv.GetFocusedRowCellValue("NoTransaksi")
        Me.Close()
    End Sub
#End Region
End Class