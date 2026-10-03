Imports DataAccess
Imports UI.WIN.MAIN.My.Resources
Imports System.Linq
Imports DevExpress.XtraPrinting
Imports System.Data
Imports System.Data.SqlClient


Public Class frmReportTracking
    Implements ILanguage

#Region "Function"
    Private Sub Me_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.Text = "Laporan Tracking"

        lTYPE.Text = Report.FILTER_TYPE
        lDATEFROM.Text = Report.FILTER_DATEFROM
        lDATETO.Text = Report.FILTER_DATETO

        cboTYPE.Properties.Items.Clear()
        cboTYPE.Properties.Items.Add("Rawat Jalan")
        cboTYPE.Properties.Items.Add("Rawat Inap")
        cboTYPE.Properties.Items.Add("Kembali")
        cboTYPE.Properties.Items.Add("Semua")
        cboTYPE.SelectedIndex = 0

        'deDATEFrom.DateTime = Now.AddDays((-Now.Day) + 1)
        deDATEFrom.DateTime = Now
        deDATETo.DateTime = Now

        fn_LoadSecurity()
    End Sub
    Public Sub fn_LoadLanguage() Implements ILanguage.fn_LoadLanguage
        Try
            Me.Text = "Laporan Tracking"

            lTYPE.Text = Report.FILTER_TYPE
            lDATEFROM.Text = Report.FILTER_DATEFROM
            lDATETO.Text = Report.FILTER_DATETO

            cboTYPE.Properties.Items.Clear()
            cboTYPE.Properties.Items.Add("Rawat Jalan")
            cboTYPE.Properties.Items.Add("Rawat Inap")
            cboTYPE.Properties.Items.Add("Kembali")
            cboTYPE.Properties.Items.Add("Semua")

        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadSecurity()
        Try
            Dim oOtority As New Setting.clsOtority
            Dim oUser As New Setting.clsUser

            Dim ds = (From x In oOtority.GetDataDetail
                      Join y In oUser.GetData
                      On x.KDOTORITY Equals y.KDOTORITY
                      Where x.MODUL = "TRACKING_R" _
                      And y.KDUSER = sUserID
                      Select x.ISADD, x.ISDELETE, x.ISUPDATE, x.ISPRINT, x.ISVIEW).FirstOrDefault

            Try
                picPrint.Enabled = ds.ISPRINT
                picRefresh.Enabled = ds.ISVIEW
                picConfirm.Enabled = ds.ISVIEW

                If ds.ISVIEW = True Then
                    fn_Preview()
                    fn_LoadLanguage()
                End If
            Catch oErr As Exception
                MsgBox(Statement.SecurityNotInstalled, MsgBoxStyle.Exclamation, Me.Text)

                picPrint.Enabled = False
                picRefresh.Enabled = False
                picConfirm.Enabled = False
            End Try
        Catch oErr As Exception
            MsgBox(Statement.SecurityNotInstalled, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_Print()
        If cboTYPE.SelectedIndex = 0 Or cboTYPE.SelectedIndex = 1 Then
            Dim listTracking As New List(Of DataAccess.S_TRACKING)

            For iLoop As Integer = 0 To grv1.RowCount - 1
                If grv1.IsRowSelected(iLoop) = True Then
                    Dim dsRekap As New DataAccess.S_TRACKING
                    dsRekap.DATECREATED = Now
                    dsRekap.DATEUPDATED = Now
                    dsRekap.KDTRACKING = grv1.GetRowCellValue(iLoop, "NoTracking")
                    dsRekap.CATEGORY = cboTYPE.SelectedIndex
                    dsRekap.STATUS = ""
                    dsRekap.KDPENDAFTARAN = ""
                    dsRekap.KDCUSTOMER = grv1.GetRowCellValue(iLoop, "NoRekamMedis")
                    dsRekap.TUJUAN_SEKARANG = grv1.GetRowCellValue(iLoop, "Tujuan")
                    dsRekap.TUJUAN_SEBELUM = grv1.GetRowCellValue(iLoop, "PoliAkhir")
                    dsRekap.DATE_SEBELUM = grv1.GetRowCellValue(iLoop, "TanggalAkhir")
                    dsRekap.JENIS_PASIEN = grv1.GetRowCellValue(iLoop, "Status")
                    dsRekap.DATE_KIRIM = Now
                    dsRekap.DATE_KEMBALI = Now
                    dsRekap.ISCHEKED = False
                    dsRekap.MEMO = grv1.GetRowCellValue(iLoop, "JenisPeserta")
                    dsRekap.KDUSER = sUserID
                    listTracking.Add(dsRekap)
                End If
            Next

            Dim rpt As New xtraTrackingPasien

            sHeaderJudulTracking = "Laporan Tracking " & cboTYPE.Text & vbCrLf & deDATEFrom.DateTime.ToString("dd/MM/yyyy") & " - " & deDATETo.DateTime.ToString("dd/MM/yyyy")
            rpt.bindingSource.DataSource = listTracking
            Dim printTool As New DevExpress.XtraReports.UI.ReportPrintTool(rpt)
            printTool.ShowPreviewDialog(DevExpress.LookAndFeel.UserLookAndFeel.Default)

            If sPrintRegister = True Then
                Dim oTracking As New Admission.clsTracking

                For iLoop As Integer = 0 To grv1.RowCount - 1
                    If grv1.IsRowSelected(iLoop) = True Then
                        oTracking.UpdateIsChekedKirim(grv1.GetRowCellValue(iLoop, "KDTRACKING"), Now)
                    End If
                Next
            End If
        Else
            Try
                printableComponentLink.Landscape = False
                printableComponentLink.PaperKind = Printing.PaperKind.A4

                Dim phf As PageHeaderFooter =
            TryCast(printableComponentLink.PageHeaderFooter, PageHeaderFooter)
                phf.Header.Content.Clear()
                phf.Header.Font = New Font("Times New Roman", 12, FontStyle.Bold)
                phf.Header.LineAlignment = BrickAlignment.Center
                phf.Footer.Font = New Font("Times New Roman", 9.75)
                phf.Footer.LineAlignment = BrickAlignment.Far
                phf.Footer.Content.AddRange(New String() _
            {sWATERMARK, "", Report.REPORT_PAGE & " : [Page # of Pages #]"})

                phf.Header.Content.AddRange(New String() _
    {"", "Laporan Tracking " & cboTYPE.Text & vbCrLf & deDATEFrom.DateTime.ToString("dd/MM/yyyy") & " - " & deDATETo.DateTime.ToString("dd/MM/yyyy"), ""})

                printableComponentLink.Component = grd
                printableComponentLink.CreateDocument()
                printableComponentLink.ShowPreviewDialog(Me)
            Catch oErr As Exception
                MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            End Try
        End If
    End Sub
    Private Sub fn_Confirm()
        Try
            If cboTYPE.SelectedIndex = 0 Or cboTYPE.SelectedIndex = 1 Then
                If MsgBox("Apakah Berkas Akan di Kirim?", MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, Me.Text) = MsgBoxResult.No Then Exit Sub

                Dim oTracking As New Admission.clsTracking

                For iLoop As Integer = 0 To grv1.RowCount - 1
                    If grv1.IsRowSelected(iLoop) = True Then
                        oTracking.UpdateIsChekedKirim(grv1.GetRowCellValue(iLoop, "KDTRACKING"), Now)
                    End If
                Next
            ElseIf cboTYPE.SelectedIndex = 2 Then
                If MsgBox("Apakah Berkas Sudah di Kembalikan?", MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, Me.Text) = MsgBoxResult.No Then Exit Sub

                Dim oTracking As New Admission.clsTracking

                For iLoop As Integer = 0 To grv1.RowCount - 1
                    If grv1.IsRowSelected(iLoop) = True Then
                        oTracking.UpdateIsChekedKembali(grv1.GetRowCellValue(iLoop, "KDTRACKING"), Now)
                    End If
                Next
            End If
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_Preview()
        Try
            grv1.Columns.Clear()
            grd.DataSource = Nothing
            grv1.OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways
            grv1.OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways

            If cboTYPE.SelectedIndex = 0 Then
                fn_LoadData01()
            ElseIf cboTYPE.SelectedIndex = 1 Then
                fn_LoadData02()
            ElseIf cboTYPE.SelectedIndex = 2 Then
                fn_LoadData03()
            ElseIf cboTYPE.SelectedIndex = 3 Then
                fn_LoadData04()
            End If

        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
#Region "Master Detail"
    Private Sub fn_LoadData01()
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
            SQL &= "A.KDTRACKING "
            SQL &= ",NoRekamMedis = A.KDCUSTOMER "
            SQL &= ",NoTracking =  RIGHT(A.KDTRACKING, 5) "
            SQL &= ",JenisPeserta = C.MEMO "
            SQL &= ",NamaPasien = B.NAME_DISPLAY "
            SQL &= ",Tujuan = A.TUJUAN_SEKARANG "
            SQL &= ",PoliAkhir = A.TUJUAN_SEBELUM "
            SQL &= ",TanggalAkhir = A.DATE_SEBELUM "
            SQL &= ",Status = A.JENIS_PASIEN "
            SQL &= "FROM S_TRACKING A "
            SQL &= "INNER JOIN M_CUSTOMER B "
            SQL &= "ON A.KDCUSTOMER = B.KDCUSTOMER "
            SQL &= "INNER JOIN M_DAFTAR_L1 C "
            SQL &= "ON B.KDDAFTAR_L1 = C.KDDAFTAR_L1 "
            SQL &= "WHERE CONVERT(VARCHAR(8), A.DATE_KIRIM, 112) >= '" & deDATEFrom.DateTime.ToString("yyyyMMdd") & "' AND CONVERT(VARCHAR(8), A.DATE_KIRIM, 112) <= '" & deDATETo.DateTime.ToString("yyyyMMdd") & "' "
            SQL &= "AND A.CATEGORY = 0 "
            SQL &= "AND A.ISCHEKED = 0 "

            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "I_MUTATION_D")

            grd.MainView = grv1
            grd.DataSource = ds.Tables("I_MUTATION_D")
            grd.ForceInitialize()

            fn_LoadFormatData()
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadData02()
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
            SQL &= "A.KDTRACKING "
            SQL &= ",NoRekamMedis = A.KDCUSTOMER "
            SQL &= ",NoTracking =  RIGHT(A.KDTRACKING, 5) "
            SQL &= ",JenisPeserta = C.MEMO "
            SQL &= ",NamaPasien = B.NAME_DISPLAY "
            SQL &= ",Tujuan = A.TUJUAN_SEKARANG "
            SQL &= ",PoliAkhir = A.TUJUAN_SEBELUM "
            SQL &= ",TanggalAkhir = A.DATE_SEBELUM "
            SQL &= ",Status = A.JENIS_PASIEN "
            SQL &= "FROM S_TRACKING A "
            SQL &= "INNER JOIN M_CUSTOMER B "
            SQL &= "ON A.KDCUSTOMER = B.KDCUSTOMER "
            SQL &= "INNER JOIN M_DAFTAR_L1 C "
            SQL &= "ON B.KDDAFTAR_L1 = C.KDDAFTAR_L1 "
            SQL &= "WHERE CONVERT(VARCHAR(8), A.DATE_KIRIM, 112) >= '" & deDATEFrom.DateTime.ToString("yyyyMMdd") & "' AND CONVERT(VARCHAR(8), A.DATE_KIRIM, 112) <= '" & deDATETo.DateTime.ToString("yyyyMMdd") & "' "
            SQL &= "AND A.CATEGORY = 1 "
            SQL &= "AND A.ISCHEKED = 0 "

            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "I_MUTATION_D")

            grd.MainView = grv1
            grd.DataSource = ds.Tables("I_MUTATION_D")
            grd.ForceInitialize()

            fn_LoadFormatData()
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadData03()
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
            SQL &= "A.KDTRACKING "
            SQL &= ",Kategori = (SELECT CASE A.CATEGORY WHEN 0 THEN 'Rawat Jalan' WHEN 1 THEN 'Rawat Inap' ELSE 'Lain-lain' END) "
            SQL &= ",NoRekamMedis = A.KDCUSTOMER "
            SQL &= ",NoTracking =  RIGHT(A.KDTRACKING, 5) "
            SQL &= ",JenisPeserta = C.MEMO "
            SQL &= ",NamaPasien = B.NAME_DISPLAY "
            SQL &= ",Tujuan = A.TUJUAN_SEKARANG "
            SQL &= ",PoliAkhir = A.TUJUAN_SEBELUM "
            SQL &= ",TanggalAkhir = A.DATE_SEBELUM "
            SQL &= ",Status = A.JENIS_PASIEN "
            SQL &= ",Deskripsi = A.MEMO "
            SQL &= "FROM S_TRACKING A "
            SQL &= "INNER JOIN M_CUSTOMER B "
            SQL &= "ON A.KDCUSTOMER = B.KDCUSTOMER "
            SQL &= "INNER JOIN M_DAFTAR_L1 C "
            SQL &= "ON B.KDDAFTAR_L1 = C.KDDAFTAR_L1 "
            SQL &= "WHERE CONVERT(VARCHAR(8), A.DATE_KIRIM, 112) >= '" & deDATEFrom.DateTime.ToString("yyyyMMdd") & "' AND CONVERT(VARCHAR(8), A.DATE_KIRIM, 112) <= '" & deDATETo.DateTime.ToString("yyyyMMdd") & "' "
            SQL &= "AND A.STATUS = 'KIRIM' "

            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "I_MUTATION_D")

            grd.MainView = grv1
            grd.DataSource = ds.Tables("I_MUTATION_D")
            grd.ForceInitialize()

            fn_LoadFormatData()
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadData04()
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
            SQL &= "A.KDTRACKING "
            SQL &= ",Kategori = (SELECT CASE A.CATEGORY WHEN 0 THEN 'Rawat Jalan' WHEN 1 THEN 'Rawat Inap' ELSE 'Lain-lain' END) "
            SQL &= ",NoRekamMedis = A.KDCUSTOMER "
            SQL &= ",NoTracking =  RIGHT(A.KDTRACKING, 5) "
            SQL &= ",JenisPeserta = C.MEMO "
            SQL &= ",NamaPasien = B.NAME_DISPLAY "
            SQL &= ",Tujuan = A.TUJUAN_SEKARANG "
            SQL &= ",PoliAkhir = A.TUJUAN_SEBELUM "
            SQL &= ",TanggalAkhir = A.DATE_SEBELUM "
            SQL &= ",TanggalKirim = A.DATE_KIRIM "
            SQL &= ",TanggalKembali = A.DATE_KEMBALI "
            SQL &= ",Status = A.JENIS_PASIEN "
            SQL &= ",Keterangan = A.STATUS "
            SQL &= ",Deskripsi = A.MEMO "
            SQL &= "FROM S_TRACKING A "
            SQL &= "INNER JOIN M_CUSTOMER B "
            SQL &= "ON A.KDCUSTOMER = B.KDCUSTOMER "
            SQL &= "INNER JOIN M_DAFTAR_L1 C "
            SQL &= "ON B.KDDAFTAR_L1 = C.KDDAFTAR_L1 "
            SQL &= "WHERE CONVERT(VARCHAR(8), A.DATE_KIRIM, 112) >= '" & deDATEFrom.DateTime.ToString("yyyyMMdd") & "' AND CONVERT(VARCHAR(8), A.DATE_KIRIM, 112) <= '" & deDATETo.DateTime.ToString("yyyyMMdd") & "' "

            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "I_MUTATION_D")

            grd.MainView = grv1
            grd.DataSource = ds.Tables("I_MUTATION_D")
            grd.ForceInitialize()

            fn_LoadFormatData()
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadFormatData()
        For iLoop As Integer = 0 To grv1.Columns.Count - 1
            If grv1.Columns(iLoop).ColumnType.Name = "Decimal" Then
                grv1.Columns(iLoop).DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                grv1.Columns(iLoop).DisplayFormat.FormatString = "{0:n2}"
                grv1.Columns(iLoop).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
            ElseIf grv1.Columns(iLoop).ColumnType.Name = "DateTime" Then
                grv1.Columns(iLoop).DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
                grv1.Columns(iLoop).DisplayFormat.FormatString = "{0:dd/MM/yyyy}"
            End If
        Next

        grv1.Columns("KDTRACKING").VisibleIndex = -1
        grv1.BestFitColumns()

    End Sub
#End Region
    Private Sub MasterColumnChooserToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles MasterColumnChooserToolStripMenuItem.Click
        grv.ShowCustomization()
    End Sub
    Private Sub DetailColumnChooserToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles DetailColumnChooserToolStripMenuItem.Click
        grv1.ShowCustomization()
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
            fn_LoadSecurity()
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub picRefresh_Click() Handles picRefresh.Click
        fn_LoadSecurity()
    End Sub
    Private Sub picConfirm_Click() Handles picConfirm.Click
        Try
            fn_Confirm()
            fn_LoadSecurity()
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
#End Region
End Class