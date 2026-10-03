Imports DataAccess
Imports UI.WIN.MAIN.My.Resources
Imports System.Linq
Imports DevExpress.XtraPrinting
Imports Newtonsoft.Json.Linq

Public Class frmReportRencaKontrol
    Implements ILanguage

#Region "Function"
    Private Sub Me_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.Text = SKD.TITLE_REPORT

        lTYPE.Text = Report.FILTER_TYPE
        lDATEFROM.Text = Report.FILTER_DATEFROM
        lDATETO.Text = Report.FILTER_DATETO

        cboTYPE.Properties.Items.Clear()
        cboTYPE.Properties.Items.Add("Data Nomor Surat Kontrol")
        cboTYPE.Properties.Items.Add("Data Poli/Spesialistik")
        cboTYPE.Properties.Items.Add("Data Dokter")
        cboTYPE.Properties.Items.Add("Cari SEP")
        cboTYPE.Properties.Items.Add("Cari Nomor Surat Kontrol")
        cboTYPE.SelectedIndex = 0

        deDATEFrom.DateTime = Now.AddDays((-Now.Day) + 1)
        deDATETo.DateTime = Now
        deDateKontrol.DateTime = Now

        'lJenisKontrol.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        'lPoli.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        'lTanggalKontrol.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
    End Sub
    Public Sub fn_LoadLanguage() Implements ILanguage.fn_LoadLanguage
        Try
            Me.Text = SKD.TITLE_REPORT

            lTYPE.Text = Report.FILTER_TYPE
            lDATEFROM.Text = Report.FILTER_DATEFROM
            lDATETO.Text = Report.FILTER_DATETO

            cboTYPE.Properties.Items.Clear()
            cboTYPE.Properties.Items.Add("Data Nomor Surat Kontrol")
            cboTYPE.Properties.Items.Add("Data Poli/Spesialistik")
            cboTYPE.Properties.Items.Add("Data Dokter")
            cboTYPE.Properties.Items.Add("Cari SEP")
            cboTYPE.Properties.Items.Add("Cari Nomor Surat Kontrol")

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
                      Where x.MODUL = "SKDSPRI" _
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
{"", "Laporan Rencana Kontrol / SPRI " & cboTYPE.Text.ToString.Trim.ToUpper, ""})

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
                    fn_LoadData01()
                Case 1
                    fn_LoadData02()
                Case 2
                    fn_LoadData03()
                Case 3
                    fn_LoadData04()
                Case 4
                    fn_LoadData05()
            End Select

        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub

#Region "Master Detail"
    Private Sub fn_LoadData01()
        Try
            Dim oSetKoneksi As New Brigging.clsSetKoneksi
            Dim uTime As Integer = 0

            If sConsidVclaim <> "" Then
                uTime = (DateTime.UtcNow - New DateTime(1970, 1, 1, 0, 0, 0)).TotalSeconds
                Dim dsSetKoneksi = oSetKoneksi.DataNomorRencanaKontrol(sUrlVclaim, sConsidVclaim, sSecreateKeyVclaim, sUserKeyVclaim, uTime, deDATEFrom.DateTime.ToString("yyyy-MM-dd"), deDATETo.DateTime.ToString("yyyy-MM-dd"), IIf(cboFormatFilter.SelectedIndex = 0, 1, 2))

                If dsSetKoneksi <> "" Then
                    Dim allData = JObject.Parse(dsSetKoneksi)

                    Dim CodeResponse As String = String.Empty
                    Dim messageResponse As String = String.Empty

                    CodeResponse = IIf(IsDBNull(allData.Item("metaData").Item("code")) = True, "", allData.Item("metaData").Item("code"))
                    messageResponse = allData("metaData")("message").ToString

                    If CodeResponse = "200" Then
                        Dim DataDecrypt = JObject.Parse(oSetKoneksi.Decrypt(allData("response"), sConsidVclaim & sSecreateKeyVclaim & uTime))

                        Dim table As DataTable

                        table = New DataTable("DATAKUNJUNGAN")
                        table.Columns.Add("noSuratKontrol")
                        table.Columns.Add("jnsPelayanan")
                        table.Columns.Add("jnsKontrol")
                        table.Columns.Add("namaJnsKontrol")
                        table.Columns.Add("tglRencanaKontrol")
                        table.Columns.Add("tglTerbitKontrol")
                        table.Columns.Add("noSepAsalKontrol")
                        table.Columns.Add("poliAsal")
                        table.Columns.Add("namaPoliAsal")
                        table.Columns.Add("poliTujuan")
                        table.Columns.Add("namaPoliTujuan")
                        table.Columns.Add("tglSEP")
                        table.Columns.Add("kodeDokter")
                        table.Columns.Add("namaDokter")
                        table.Columns.Add("noKartu")
                        table.Columns.Add("nama")

                        For Each item In DataDecrypt("list")
                            table.Rows.Add(New String() {item("noSuratKontrol"), item("jnsPelayanan"), item("jnsKontrol"), item("namaJnsKontrol"), item("tglRencanaKontrol"), item("tglTerbitKontrol"),
                                           item("noSepAsalKontrol"), item("poliAsal"), item("namaPoliAsal"), item("poliTujuan"), item("namaPoliAsal"), item("tglSEP"), item("kodeDokter"), item("namaDokter"), item("noKartu"), item("nama")})
                        Next

                        grd.MainView = grv
                        grd.DataSource = table
                        grd.ForceInitialize()
                        fn_LoadFormatData()
                    Else
                        MsgBox(CodeResponse & " - " & messageResponse, MsgBoxStyle.Exclamation, Me.Text)
                    End If
                Else
                    MsgBox("Load Data Gagal", MsgBoxStyle.Exclamation, Me.Text)
                End If
            Else
                MsgBox("Koneksi Tidak ditemukan", MsgBoxStyle.Exclamation, Me.Text)
            End If
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadData02()
        Try
            If txtCari.Text = String.Empty Then
                MsgBox("Nomor SEP / No Kartu Kosong", MsgBoxStyle.Exclamation, Me.Text)
                Exit Sub
            End If
            Dim oSetKoneksi As New Brigging.clsSetKoneksi
            Dim uTime As Integer = 0

            If sConsidVclaim <> "" Then
                uTime = (DateTime.UtcNow - New DateTime(1970, 1, 1, 0, 0, 0)).TotalSeconds
                Dim dsSetKoneksi = oSetKoneksi.DataPoliSpesialistik(sUrlVclaim, sConsidVclaim, sSecreateKeyVclaim, sUserKeyVclaim, uTime, IIf(cboJenisKontrol.SelectedIndex = 0, 2, 1), txtCari.Text, deDateKontrol.DateTime.ToString("yyyy-MM-dd"))

                If dsSetKoneksi <> "" Then
                    Dim allData = JObject.Parse(dsSetKoneksi)

                    Dim CodeResponse As String = String.Empty
                    Dim messageResponse As String = String.Empty

                    CodeResponse = IIf(IsDBNull(allData.Item("metaData").Item("code")) = True, "", allData.Item("metaData").Item("code"))
                    messageResponse = allData("metaData")("message").ToString

                    If CodeResponse = "200" Then
                        Dim DataDecrypt = JObject.Parse(oSetKoneksi.Decrypt(allData("response"), sConsidVclaim & sSecreateKeyVclaim & uTime))

                        Dim table As DataTable

                        table = New DataTable("DATAKUNJUNGAN")
                        table.Columns.Add("kodePoli")
                        table.Columns.Add("namaPoli")
                        table.Columns.Add("kapasitas")
                        table.Columns.Add("jmlRencanaKontroldanRujukan")
                        table.Columns.Add("persentase")

                        For Each item In DataDecrypt("list")
                            table.Rows.Add(New String() {item("kodePoli"), item("namaPoli"), item("kapasitas"), item("jmlRencanaKontroldanRujukan"), item("persentase")})
                        Next

                        grd.MainView = grv
                        grd.DataSource = table
                        grd.ForceInitialize()
                        fn_LoadFormatData()
                    Else
                        MsgBox(CodeResponse & " - " & messageResponse, MsgBoxStyle.Exclamation, Me.Text)
                    End If
                Else
                    MsgBox("Load Data Gagal", MsgBoxStyle.Exclamation, Me.Text)
                End If
            Else
                MsgBox("Koneksi Tidak ditemukan", MsgBoxStyle.Exclamation, Me.Text)
            End If
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadData03()
        Try
            Dim oSetKoneksi As New Brigging.clsSetKoneksi
            Dim uTime As Integer = 0

            If sConsidVclaim <> "" Then
                uTime = (DateTime.UtcNow - New DateTime(1970, 1, 1, 0, 0, 0)).TotalSeconds
                Dim dsSetKoneksi = oSetKoneksi.DataDokter(sUrlVclaim, sConsidVclaim, sSecreateKeyVclaim, sUserKeyVclaim, uTime, IIf(cboJenisKontrol.SelectedIndex = 0, 1, 2), grdKDDEPARTMENT.EditValue, deDateKontrol.DateTime.ToString("yyyy-MM-dd"))

                If dsSetKoneksi <> "" Then
                    Dim allData = JObject.Parse(dsSetKoneksi)

                    Dim CodeResponse As String = String.Empty
                    Dim messageResponse As String = String.Empty

                    CodeResponse = IIf(IsDBNull(allData.Item("metaData").Item("code")) = True, "", allData.Item("metaData").Item("code"))
                    messageResponse = allData("metaData")("message").ToString

                    If CodeResponse = "200" Then
                        Dim DataDecrypt = JObject.Parse(oSetKoneksi.Decrypt(allData("response"), sConsidVclaim & sSecreateKeyVclaim & uTime))

                        Dim table As DataTable

                        table = New DataTable("DATAKUNJUNGAN")
                        table.Columns.Add("kodeDokter")
                        table.Columns.Add("namaDokter")
                        table.Columns.Add("jadwalPraktek")
                        table.Columns.Add("kapasitas")

                        For Each item In DataDecrypt("list")
                            table.Rows.Add(New String() {item("kodeDokter"), item("namaDokter"), item("jadwalPraktek"), item("kapasitas")})
                        Next

                        grd.MainView = grv
                        grd.DataSource = table
                        grd.ForceInitialize()
                        fn_LoadFormatData()
                    Else
                        MsgBox(CodeResponse & " - " & messageResponse, MsgBoxStyle.Exclamation, Me.Text)
                    End If
                Else
                    MsgBox("Load Data Gagal", MsgBoxStyle.Exclamation, Me.Text)
                End If
            Else
                MsgBox("Koneksi Tidak ditemukan", MsgBoxStyle.Exclamation, Me.Text)
            End If
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadData04()
        Try
            Dim oSetKoneksi As New Brigging.clsSetKoneksi
            Dim uTime As Integer = 0

            If sConsidVclaim <> "" Then
                uTime = (DateTime.UtcNow - New DateTime(1970, 1, 1, 0, 0, 0)).TotalSeconds
                Dim dsSetKoneksi = oSetKoneksi.CariSEPSuratKontrolSPRI(sUrlVclaim, sConsidVclaim, sSecreateKeyVclaim, sUserKeyVclaim, uTime, txtCari.Text)

                If dsSetKoneksi <> "" Then
                    Dim allData = JObject.Parse(dsSetKoneksi)

                    Dim CodeResponse As String = String.Empty
                    Dim messageResponse As String = String.Empty

                    CodeResponse = IIf(IsDBNull(allData.Item("metaData").Item("code")) = True, "", allData.Item("metaData").Item("code"))
                    messageResponse = allData("metaData")("message").ToString

                    If CodeResponse = "200" Then
                        Dim DataDecrypt = JObject.Parse(oSetKoneksi.Decrypt(allData("response"), sConsidVclaim & sSecreateKeyVclaim & uTime))
                        MsgBox(CodeResponse & " - " & DataDecrypt.ToString(), MsgBoxStyle.Exclamation, Me.Text)
                    Else
                        MsgBox(CodeResponse & " - " & messageResponse, MsgBoxStyle.Exclamation, Me.Text)
                    End If
                Else
                    MsgBox("Load Data Gagal", MsgBoxStyle.Exclamation, Me.Text)
                End If
            Else
                MsgBox("Koneksi Tidak ditemukan", MsgBoxStyle.Exclamation, Me.Text)
            End If
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadData05()
        Try
            Dim oSetKoneksi As New Brigging.clsSetKoneksi
            Dim uTime As Integer = 0

            If sConsidVclaim <> "" Then
                uTime = (DateTime.UtcNow - New DateTime(1970, 1, 1, 0, 0, 0)).TotalSeconds
                Dim dsSetKoneksi = oSetKoneksi.CariNomorRencanaKontrol(sUrlVclaim, sConsidVclaim, sSecreateKeyVclaim, sUserKeyVclaim, uTime, txtCari.Text)

                If dsSetKoneksi <> "" Then
                    Dim allData = JObject.Parse(dsSetKoneksi)

                    Dim CodeResponse As String = String.Empty
                    Dim messageResponse As String = String.Empty

                    CodeResponse = IIf(IsDBNull(allData.Item("metaData").Item("code")) = True, "", allData.Item("metaData").Item("code"))
                    messageResponse = allData("metaData")("message").ToString

                    If CodeResponse = "200" Then
                        Dim DataDecrypt = JObject.Parse(oSetKoneksi.Decrypt(allData("response"), sConsidVclaim & sSecreateKeyVclaim & uTime))
                        MsgBox(CodeResponse & " - " & DataDecrypt.ToString(), MsgBoxStyle.Exclamation, Me.Text)
                    Else
                        MsgBox(CodeResponse & " - " & messageResponse, MsgBoxStyle.Exclamation, Me.Text)
                    End If
                Else
                    MsgBox("Load Data Gagal", MsgBoxStyle.Exclamation, Me.Text)
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
    Private Sub fn_LoadDepartment()
        Dim oDepartment As New Reference.clsDepartment
        Try
            grdKDDEPARTMENT.Properties.DataSource = oDepartment.GetData.Where(Function(x) x.ISACTIVE = True).ToList()
            grdKDDEPARTMENT.Properties.ValueMember = "KDDEPARTMENT_BPJS"
            grdKDDEPARTMENT.Properties.DisplayMember = "NAME_DISPLAY"

        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
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
    Private Sub cboTYPE_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboTYPE.SelectedIndexChanged
        lCARI.Text = "Cari"
        txtCari.ResetText()
        fn_LoadDepartment()

        If cboTYPE.SelectedIndex = 0 Then
            lDATEFROM.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            lDATETO.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            lFormatFilter.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            lJenisKontrol.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            lPoli.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            lTanggalKontrol.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            lCARI.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        ElseIf cboTYPE.SelectedIndex = 1 Then
            cboJenisKontrol.SelectedIndex = 0
            lCARI.Text = "Nomor SEP"
            lDATEFROM.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            lDATETO.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            lFormatFilter.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            lJenisKontrol.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            lPoli.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            lTanggalKontrol.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            lCARI.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        ElseIf cboTYPE.SelectedIndex = 2 Then
            lDATEFROM.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            lDATETO.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            lFormatFilter.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            lJenisKontrol.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            lPoli.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            lTanggalKontrol.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            lCARI.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        ElseIf cboTYPE.SelectedIndex = 3 Then
            lCARI.Text = "Nomor SEP"
            lDATEFROM.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            lDATETO.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            lFormatFilter.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            lJenisKontrol.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            lPoli.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            lTanggalKontrol.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            lCARI.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        Else
            lCARI.Text = "Nomor Surat Kontrol"
            lDATEFROM.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            lDATETO.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            lFormatFilter.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            lJenisKontrol.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            lPoli.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            lTanggalKontrol.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            lCARI.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        End If
    End Sub
    Private Sub cboJenisKontrol_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboJenisKontrol.SelectedIndexChanged
        If cboTYPE.SelectedIndex = 1 Then
            If cboJenisKontrol.SelectedIndex = 0 Then
                lCARI.Text = "Nomor SEP"
            Else
                lCARI.Text = "Nomor Kartu"
            End If
        Else
            lCARI.Text = "Cari"
        End If
    End Sub
    Private Sub AmbilDataToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AmbilDataToolStripMenuItem.Click
        If grv.GetFocusedRowCellValue("noSuratKontrol") Is Nothing Then
            Exit Sub
        End If

        'table.Columns.Add("noSuratKontrol")
        'table.Columns.Add("jnsPelayanan")
        'table.Columns.Add("jnsKontrol")
        'table.Columns.Add("namaJnsKontrol")
        'table.Columns.Add("tglRencanaKontrol")
        'table.Columns.Add("tglTerbitKontrol")
        'table.Columns.Add("noSepAsalKontrol")
        'table.Columns.Add("poliAsal")
        'table.Columns.Add("namaPoliAsal")
        'table.Columns.Add("poliTujuan")
        'table.Columns.Add("namaPoliTujuan")
        'table.Columns.Add("tglSEP")
        'table.Columns.Add("kodeDokter")
        'table.Columns.Add("namaDokter")
        'table.Columns.Add("noKartu")
        'table.Columns.Add("nama")

        If cboTYPE.SelectedIndex = 0 Then
            'If sCategori = 0 Then
            '    sKODEBPJS = grv.GetFocusedRowCellValue("poliAsal")
            '    sNomorSEPKartu = grv.GetFocusedRowCellValue("noSepAsalKontrol")
            'Else
            '    sKODEBPJS = grv.GetFocusedRowCellValue("poliAsal")
            '    sNomorSEPKartu = grv.GetFocusedRowCellValue("noKartu")
            'End If
            Me.Close()
        End If
    End Sub
#End Region
End Class