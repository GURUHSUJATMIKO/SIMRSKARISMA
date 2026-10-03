Imports DataAccess
Imports UI.WIN.MAIN.My.Resources
Imports System.Linq

Imports DevExpress.XtraPrinting

Imports System.Data
Imports System.Data.SqlClient


Public Class frmReportAdmissionRI
    Implements ILanguage

#Region "Function"
    Private Sub Me_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.Text = "Laporan Admission Rawat Inap"

        lTYPE.Text = Report.FILTER_TYPE
        lDATEFROM.Text = Report.FILTER_DATEFROM
        lDATETO.Text = Report.FILTER_DATETO

        cboTYPE.Properties.Items.Clear()
        cboTYPE.Properties.Items.Add("Rekap")
        cboTYPE.SelectedIndex = 0

        deDATEFrom.DateTime = Now.AddDays((-Now.Day) + 1)
        deDATETo.DateTime = Now
    End Sub
    Public Sub fn_LoadLanguage() Implements ILanguage.fn_LoadLanguage
        Try
            Me.Text = "Laporan Admission Rawat Inap"

            lTYPE.Text = Report.FILTER_TYPE
            lDATEFROM.Text = Report.FILTER_DATEFROM
            lDATETO.Text = Report.FILTER_DATETO

            cboTYPE.Properties.Items.Clear()
            cboTYPE.Properties.Items.Add("Rekap")
            'cboTYPE.Properties.Items.Add(Report.FILTER_TYPE_02)
            'cboTYPE.Properties.Items.Add(Report.FILTER_TYPE_03)
            'cboTYPE.Properties.Items.Add(Report.FILTER_TYPE_04)

            'If cboTYPE.SelectedIndex = 3 Then
            '    fn_LoadLanguageAll()
            'Else
            '    fn_LoadLanguageMaster()
            '    fn_LoadLanguageDetail()
            'End If

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
                      Where x.MODUL = "ADMISSION_R" _
                      And y.KDUSER = sUserID
                      Select x.ISADD, x.ISDELETE, x.ISUPDATE, x.ISPRINT, x.ISVIEW).FirstOrDefault

            Try
                picPrint.Enabled = ds.ISPRINT
                picRefresh.Enabled = ds.ISVIEW

                If ds.ISVIEW = True Then
                    fn_Preview()
                    fn_LoadLanguage()
                End If
            Catch oErr As Exception
                MsgBox(Statement.SecurityNotInstalled, MsgBoxStyle.Exclamation, Me.Text)

                picPrint.Enabled = False
                picRefresh.Enabled = False
            End Try
        Catch oErr As Exception
            MsgBox(Statement.SecurityNotInstalled, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_Print()
        Try
            PrintableComponentLink.Landscape = True
            PrintableComponentLink.PaperKind = Printing.PaperKind.A4

            Dim phf As PageHeaderFooter =
        TryCast(PrintableComponentLink.PageHeaderFooter, PageHeaderFooter)
            phf.Header.Content.Clear()
            phf.Header.Font = New Font("Times New Roman", 14, FontStyle.Bold)
            phf.Header.LineAlignment = BrickAlignment.Center
            phf.Footer.Font = New Font("Times New Roman", 9.75)
            phf.Footer.LineAlignment = BrickAlignment.Far
            phf.Footer.Content.AddRange(New String() _
        {sWATERMARK, "", Report.REPORT_PAGE & " : [Page # of Pages #]"})

            phf.Header.Content.AddRange(New String() _
{"", "Laporan Admission" & vbCrLf & deDATEFrom.DateTime.ToString("dd/MM/yyyy") & " - " & deDATETo.DateTime.ToString("dd/MM/yyyy"), ""})

            PrintableComponentLink.Component = grd
            PrintableComponentLink.CreateDocument()
            PrintableComponentLink.ShowPreviewDialog(Me)
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_Preview()
        Try
            grv.Columns.Clear()
            grd.DataSource = Nothing
            grv.OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways
            grv1.OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways

            fn_LoadData()

        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub

#Region "Master Detail"
    Private Sub fn_LoadData()
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
            SQL &= "NoPendaftaran = A.KDPENDAFTARAN "
            SQL &= ",Nama = B.NAME_DISPLAY "
            SQL &= ",NoRM = A.KDCUSTOMER "
            SQL &= ",Alamat = (SELECT TOP 1 ALAMAT FROM S_PENDAFTARAN_KUNJUNGAN WHERE A.KDPENDAFTARAN = KDPENDAFTARAN) "
            SQL &= ",Jk = (SELECT CASE B.KDJENISKELAMIN WHEN 0 THEN 'Perempuan' WHEN 1 THEN 'Laki-Laki' ELSE '-' END) "
            SQL &= ",UmurHari = DATEDIFF(DD, B.TANGGALLAHIR, A.DATE) "
            SQL &= ",UmurBulan = DATEDIFF(MM, B.TANGGALLAHIR, A.DATE) "
            SQL &= ",UmurTahun =  DATEDIFF(YYYY, B.TANGGALLAHIR, A.DATE) "
            SQL &= ",NamaKeluarga = B.NAMAKELUARGA "
            SQL &= ",NoTelepon = B.PHONE "
            SQL &= ",PangkatGol = (SELECT TOP 1 BB.MEMO FROM S_PENDAFTARAN_KUNJUNGAN AA INNER JOIN M_PANGKAT BB ON AA.KDPANGKAT = BB.KDPANGKAT WHERE A.KDPENDAFTARAN = AA.KDPENDAFTARAN) "
            SQL &= ",B.NRP "
            SQL &= ",Kesatuan = (SELECT TOP 1 BB.MEMO FROM S_PENDAFTARAN_KUNJUNGAN AA INNER JOIN M_KESATUAN BB ON AA.KDKESATUAN = BB.KDKESATUAN WHERE A.KDPENDAFTARAN = AA.KDPENDAFTARAN) "
            SQL &= ",CaraBayar = (SELECT MEMO FROM M_PENJAMIN WHERE B.KDPENJAMIN = KDPENJAMIN) "
            SQL &= ",Tanggalmasuk = A.DATE "
            SQL &= ",TanggalPulang = ISNULL((SELECT DATE FROM T_UPDATE_TANGGAL_PULANG WHERE A.KDPENDAFTARAN = KDPENDAFTARAN), A.DATE) "
            SQL &= ",SudahPulang = ISNULL((SELECT CONVERT(BIT, 1) FROM T_UPDATE_TANGGAL_PULANG WHERE A.KDPENDAFTARAN = KDPENDAFTARAN), CONVERT(BIT, 0)) "
            SQL &= ",DiganosaAwal = (SELECT MEMO FROM M_DIAGNOSA WHERE A.KDDIAGNOSA = KDDIAGNOSA) "
            SQL &= ",DokterDPJP = D.NAME_DISPLAY "
            SQL &= ",Ruangan = C.NAME_DISPLAY "
            SQL &= ",CaraPulang = ISNULL((SELECT BB.MEMO FROM T_UPDATE_TANGGAL_PULANG AA INNER JOIN M_CARAKELUAR BB ON AA.CARAPULANG = BB.KDCARAKELUAR WHERE A.KDPENDAFTARAN = KDPENDAFTARAN), '-') "
            SQL &= ",[User] = A.KDUSER "
            SQL &= "FROM S_PENDAFTARAN_H A "
            SQL &= "INNER JOIN M_CUSTOMER B "
            SQL &= "On A.KDCUSTOMER = B.KDCUSTOMER "
            SQL &= "INNER JOIN M_DEPARTMENT C "
            SQL &= "On A.KDDEPARTMENT = C.KDDEPARTMENT "
            SQL &= "INNER JOIN M_DOCTOR D "
            SQL &= "On A.KDDOCTOR = D.KDDOCTOR "
            SQL &= "WHERE CONVERT(VARCHAR(8), A.Date, 112) >= '" & deDATEFrom.DateTime.ToString("yyyyMMdd") & "' AND CONVERT(VARCHAR(8), A.DATE, 112) <= '" & deDATETo.DateTime.ToString("yyyyMMdd") & "' "
            SQL &= "AND A.CATEGORY = 1 "

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
        For iLoop As Integer = 0 To grv.Columns.Count - 1
            If grv.Columns(iLoop).ColumnType.Name = "Decimal" Then
                grv.Columns(iLoop).DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                grv.Columns(iLoop).DisplayFormat.FormatString = "{0:n2}"
                grv.Columns(iLoop).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
            ElseIf grv.Columns(iLoop).ColumnType.Name = "DateTime" Then
                grv.Columns(iLoop).DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
                grv.Columns(iLoop).DisplayFormat.FormatString = "{0:dd/MM/yyyy HH:mm:ss}"
            End If
        Next
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
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub picRefresh_Click() Handles picRefresh.Click
        fn_LoadSecurity()
    End Sub
#End Region
End Class