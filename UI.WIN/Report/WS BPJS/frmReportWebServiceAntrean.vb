Imports DataAccess
Imports UI.WIN.MAIN.My.Resources
Imports System.Linq
Imports Newtonsoft.Json.Linq
Imports DevExpress.XtraPrinting

Imports System.Data
Imports System.Data.SqlClient


Public Class frmReportWebServiceAntrean
    Implements ILanguage

#Region "Function"
    Private Sub Me_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.Text = "Laporan Admission Rawat Jalan"

        lTYPE.Text = Report.FILTER_TYPE
        lDATEFROM.Text = Report.FILTER_DATEFROM

        cboTYPE.Properties.Items.Clear()
        cboTYPE.Properties.Items.Add("Referensi Poli")
        cboTYPE.Properties.Items.Add("Referensi Dokter")
        cboTYPE.Properties.Items.Add("Referensi Jadwal Dokter")
        cboTYPE.SelectedIndex = 0

        fn_LoadDepartment()

        deDATEFrom.DateTime = Now
    End Sub
    Public Sub fn_LoadLanguage() Implements ILanguage.fn_LoadLanguage
        Try
            Me.Text = "Laporan Admission Rawat Jalan"

            lTYPE.Text = Report.FILTER_TYPE
            lDATEFROM.Text = Report.FILTER_DATEFROM
            cboTYPE.Properties.Items.Clear()
            cboTYPE.Properties.Items.Add("Referensi Poli")
            cboTYPE.Properties.Items.Add("Referensi Dokter")
            cboTYPE.Properties.Items.Add("Referensi Jadwal Dokter")

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
                      Where x.MODUL = "WEBSERVICEANTRIAN_R" _
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
{"", "Laporan Web Service Antrian" & "", ""})

            printableComponentLink.Component = grd
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

            Select Case cboTYPE.SelectedIndex
                Case 0
                    'fn_LoadData01()
                Case 1
                    'fn_LoadData02()
                Case 2
                    fn_LoadData03()
                Case 3
                    'fn_LoadData04()
            End Select

        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub

#Region "Master Detail"
    Private Sub fn_LoadData03()
        Try
            If grdKDDEPARTMENT.Text = String.Empty Then Exit Sub

            Dim oSetKoneksi As New Brigging.clsSetKoneksi
            Dim oDepartment As New Reference.clsDepartment
            Dim dsDataSetKoneksi = oSetKoneksi.GetData().FirstOrDefault(Function(x) x.ISACTIVE = True And x.NAME_DISPLAY = "VCLAIM")
            Dim uTime As Integer = 0

            If dsDataSetKoneksi IsNot Nothing Then
                uTime = (DateTime.UtcNow - New DateTime(1970, 1, 1, 0, 0, 0)).TotalSeconds
                Dim dsSetKoneksi = oSetKoneksi.ReferensiJadwalDokter(dsDataSetKoneksi.ALAMATWEB, dsDataSetKoneksi.CONSID, dsDataSetKoneksi.SECREATKEY, uTime, oDepartment.GetData(grdKDDEPARTMENT.EditValue).KDDEPARTMENT_BPJS, deDATEFrom.DateTime.ToString("yyyy-MM-dd"))

                If dsSetKoneksi <> "" Then
                    Dim allData = JObject.Parse(dsSetKoneksi)

                    Dim CodeResponse As String = String.Empty
                    Dim messageResponse As String = String.Empty

                    CodeResponse = IIf(IsDBNull(allData.Item("metaData").Item("code")) = True, "", allData.Item("metaData").Item("code"))
                    messageResponse = allData("metaData")("message").ToString

                    If CodeResponse = "200" Then
                        Dim DataDecrypt = JObject.Parse(oSetKoneksi.Decrypt(allData("response"), dsDataSetKoneksi.CONSID & dsDataSetKoneksi.SECREATKEY & uTime))

                        Dim table As DataTable

                        table = New DataTable("DATAKUNJUNGAN")
                        table.Columns.Add("kodesubspesialis")
                        table.Columns.Add("hari")
                        table.Columns.Add("kapasitaspasien")
                        table.Columns.Add("libur")
                        table.Columns.Add("namahari")
                        table.Columns.Add("jadwal")
                        table.Columns.Add("namasubspesialis")
                        table.Columns.Add("namadokter")
                        table.Columns.Add("kodepoli")
                        table.Columns.Add("namapoli")
                        table.Columns.Add("kodedokter")

                        For Each item In DataDecrypt("list")
                            table.Rows.Add(New String() {item("diagnosa"), item("jnsPelayanan"), item("kelasRawat"), item("nama"), item("noKartu"), item("noSep"), item("noRujukan"), item("poli"), item("tglPlgSep"), item("tglSep")})
                        Next

                        grd.MainView = grv
                        grd.DataSource = table
                        grd.ForceInitialize()
                        fn_LoadFormatData()
                    Else
                        MsgBox(CodeResponse & " - " & messageResponse, MsgBoxStyle.Exclamation, Me.Text)
                    End If
                Else
                    MsgBox("Referensi Jadwal Dokter", MsgBoxStyle.Exclamation, Me.Text)
                End If
            Else
                MsgBox("Koneksi Tidak ditemukan", MsgBoxStyle.Exclamation, Me.Text)
            End If
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
    Private Sub fn_LoadDepartment()
        Dim oDepartment As New Reference.clsDepartment
        Try
            grdKDDEPARTMENT.Properties.DataSource = oDepartment.GetData.Where(Function(x) x.ISACTIVE = True And x.KDDEPARTMENT_BPJS <> "").ToList()
            grdKDDEPARTMENT.Properties.ValueMember = "KDDEPARTMENT"
            grdKDDEPARTMENT.Properties.DisplayMember = "NAME_DISPLAY"

        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
#End Region
End Class