Imports DataAccess
Imports UI.WIN.MAIN.My.Resources
Imports System.Linq

Imports DevExpress.XtraPrinting
Imports Newtonsoft.Json.Linq

Imports System.Data
Imports System.Data.SqlClient


Public Class frmMonitoringBPJS
    Implements ILanguage

#Region "Function"
    Private Sub Me_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.Text = "Monitoring BPJS"
        deDATEFrom.DateTime = Now
        deDateTo.DateTime = Now
        fn_LoadSecurity()
    End Sub
    Private Sub Me_FormClosed(sender As System.Object, e As System.Windows.Forms.FormClosedEventArgs) Handles MyBase.FormClosed
        Try
            grv.SaveLayoutToRegistry("HKEY_CURRENT_USER\Software\SIMRS\SW\" & sUserID & "\" & Me.Text & "\")
        Catch oErr As Exception

        End Try
    End Sub
    Public Sub fn_LoadLanguage() Implements ILanguage.fn_LoadLanguage
        Try
            Me.Text = "Monitoring BPJS"

            'fn_LoadLanguageAll()

        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_Sembunyikan()
        If cboMonitoring.SelectedIndex = 0 Then
            deDATEFrom.Properties.ReadOnly = False
            cboTYPE.Properties.ReadOnly = False
            cboStatus.Properties.ReadOnly = True
            deDateTo.Properties.ReadOnly = True
            txtNoKartu.Properties.ReadOnly = True
        ElseIf cboMonitoring.SelectedIndex = 1 Then
            deDATEFrom.Properties.ReadOnly = False
            cboTYPE.Properties.ReadOnly = True
            cboStatus.Properties.ReadOnly = False
            deDateTo.Properties.ReadOnly = True
            txtNoKartu.Properties.ReadOnly = True
        ElseIf cboMonitoring.SelectedIndex = 2 Then
            deDATEFrom.Properties.ReadOnly = False
            cboTYPE.Properties.ReadOnly = True
            cboStatus.Properties.ReadOnly = True
            deDateTo.Properties.ReadOnly = False
            txtNoKartu.Properties.ReadOnly = False
        Else
            deDATEFrom.Properties.ReadOnly = False
            cboTYPE.Properties.ReadOnly = True
            cboStatus.Properties.ReadOnly = True
            deDateTo.Properties.ReadOnly = False
            txtNoKartu.Properties.ReadOnly = True
        End If
    End Sub
    Private Sub fn_LoadSecurity()
        Try
            Dim oOtority As New Setting.clsOtority
            Dim oUser As New Setting.clsUser

            Dim ds = (From x In oOtority.GetDataDetail
                      Join y In oUser.GetData
                      On x.KDOTORITY Equals y.KDOTORITY
                      Where x.MODUL = "DATA_KUNJUNGAN_BPJS" _
                      And y.KDUSER = sUserID
                      Select x.ISADD, x.ISDELETE, x.ISUPDATE, x.ISPRINT, x.ISVIEW).FirstOrDefault

            Try
                picPrint.Enabled = ds.ISPRINT
                picRefresh.Enabled = ds.ISVIEW

                If ds.ISVIEW = True Then
                    fn_Preview()
                    fn_LoadLanguage()
                    fn_Sembunyikan()
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
{"", "MONITORING " & cboMonitoring.Text.ToString.Trim.ToUpper, ""})

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

            Select Case cboMonitoring.SelectedIndex
                Case 0
                    fn_LoadData01()
                Case 1
                    fn_LoadData02()
                Case 2
                    fn_LoadData03()
                Case 3
                    fn_LoadData04()
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
                Dim dsSetKoneksi = oSetKoneksi.GetDataVClaimMonitoringDataKunjungan(sUrlVclaim, sConsidVclaim, sSecreateKeyVclaim, sUserKeyVclaim, uTime, deDATEFrom.DateTime.ToString("yyyy-MM-dd"), IIf(cboTYPE.SelectedIndex = 0, 2, 1))

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
                        table.Columns.Add("diagnosa")
                        table.Columns.Add("jnsPelayanan")
                        table.Columns.Add("kelasRawat")
                        table.Columns.Add("nama")
                        table.Columns.Add("noKartu")
                        table.Columns.Add("noSep")
                        table.Columns.Add("noRujukan")
                        table.Columns.Add("poli")
                        table.Columns.Add("tglPlgSep")
                        table.Columns.Add("tglSep")

                        For Each item In DataDecrypt("sep")
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
                    MsgBox("Insert SEP Data Gagal/Kosong", MsgBoxStyle.Exclamation, Me.Text)
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
            Dim oSetKoneksi As New Brigging.clsSetKoneksi
            Dim uTime As Integer = 0

            If sConsidVclaim <> "" Then
                uTime = (DateTime.UtcNow - New DateTime(1970, 1, 1, 0, 0, 0)).TotalSeconds
                Dim dsSetKoneksi = oSetKoneksi.GetDataVClaimMonitoringDataKlaim(sUrlVclaim, sConsidVclaim, sSecreateKeyVclaim, sUserKeyVclaim, uTime, deDATEFrom.DateTime.ToString("yyyy-MM-dd"), IIf(cboTYPE.SelectedIndex = 0, 2, 1), cboStatus.SelectedIndex + 1)

                If dsSetKoneksi <> "" Then
                    Dim allData = JObject.Parse(dsSetKoneksi)

                    Dim CodeResponse As String = String.Empty
                    Dim messageResponse As String = String.Empty

                    CodeResponse = IIf(IsDBNull(allData.Item("metaData").Item("code")) = True, "", allData.Item("metaData").Item("code"))
                    messageResponse = allData("metaData")("message").ToString

                    If CodeResponse = "200" Then
                        Dim DataDecrypt = JObject.Parse(oSetKoneksi.Decrypt(allData("response"), sConsidVclaim & sSecreateKeyVclaim & uTime))

                        Dim table As DataTable

                        table = New DataTable("DATAKLAIM")
                        table.Columns.Add("Inacbgnama")
                        table.Columns.Add("Inacbgkode")
                        table.Columns.Add("byPengajuan")
                        table.Columns.Add("bySetujui")
                        table.Columns.Add("byTarifGruper")
                        table.Columns.Add("byTarifRS")
                        table.Columns.Add("byTopup")
                        table.Columns.Add("kelasRawat")
                        table.Columns.Add("noFPK")
                        table.Columns.Add("noSEP")
                        table.Columns.Add("pesertanama")
                        table.Columns.Add("pesertanoKartu")
                        table.Columns.Add("pesertanonoMR")
                        table.Columns.Add("poli")
                        table.Columns.Add("status")
                        table.Columns.Add("tglPulang")
                        table.Columns.Add("tglSep")

                        For Each item In DataDecrypt("klaim")
                            table.Rows.Add(New String() {
                                       item("Inacbg")("kode") _
                                       , item("Inacbg")("nama") _
                                       , item("biaya")("byPengajuan") _
                                       , item("biaya")("bySetujui") _
                                       , item("biaya")("byTarifGruper") _
                                       , item("biaya")("byTarifRS") _
                                       , item("biaya")("byTopup") _
                                       , item("kelasRawat") _
                                       , item("noFPK") _
                                       , item("peserta")("nama") _
                                       , item("peserta")("noKartu") _
                                       , item("peserta")("noMR") _
                                       , item("poli") _
                                       , item("status") _
                                       , item("tglPulang") _
                                       , item("tglSep")})
                        Next

                        grd.MainView = grv
                        grd.DataSource = table
                        grd.ForceInitialize()
                        fn_LoadFormatData()
                    Else
                        MsgBox(CodeResponse & " - " & messageResponse, MsgBoxStyle.Exclamation, Me.Text)
                    End If
                Else
                    MsgBox("Insert SEP Data Gagal/Kosong", MsgBoxStyle.Exclamation, Me.Text)
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
            If txtNoKartu.Text = String.Empty Then Exit Sub

            Dim oSetKoneksi As New Brigging.clsSetKoneksi
            Dim uTime As Integer = 0

            If sConsidVclaim <> "" Then
                uTime = (DateTime.UtcNow - New DateTime(1970, 1, 1, 0, 0, 0)).TotalSeconds
                Dim dsSetKoneksi = oSetKoneksi.GetDataVClaimMonitoringDataHistoriPelayananPeserta(sUrlVclaim, sConsidVclaim, sSecreateKeyVclaim, sUserKeyVclaim, uTime, txtNoKartu.Text.ToString.Trim, deDATEFrom.DateTime.ToString("yyyy-MM-dd"), deDateTo.DateTime.ToString("yyyy-MM-dd"))

                If dsSetKoneksi <> "" Then
                    Dim allData = JObject.Parse(dsSetKoneksi)

                    Dim CodeResponse As String = String.Empty
                    Dim messageResponse As String = String.Empty

                    CodeResponse = IIf(IsDBNull(allData.Item("metaData").Item("code")) = True, "", allData.Item("metaData").Item("code"))
                    messageResponse = allData("metaData")("message").ToString

                    If CodeResponse = "200" Then
                        Dim DataDecrypt = JObject.Parse(oSetKoneksi.Decrypt(allData("response"), sConsidVclaim & sSecreateKeyVclaim & uTime))

                        Dim table As DataTable

                        table = New DataTable("DATAHISTORIPELAYANAN")
                        table.Columns.Add("diagnosa")
                        table.Columns.Add("jnsPelayanan")
                        table.Columns.Add("kelasRawat")
                        table.Columns.Add("namaPeserta")
                        table.Columns.Add("noKartu")
                        table.Columns.Add("noSep")
                        table.Columns.Add("noRujukan")
                        table.Columns.Add("poli")
                        table.Columns.Add("ppkPelayanan")
                        table.Columns.Add("tglPlgSep")
                        table.Columns.Add("tglSep")

                        For Each item In DataDecrypt("histori")
                            table.Rows.Add(New String() {item("diagnosa"), item("jnsPelayanan"), item("kelasRawat"), item("namaPeserta"), item("noKartu"), item("noSep"), item("noRujukan"), item("poli"), item("ppkPelayanan"), item("tglPlgSep"), item("tglSep")})
                        Next

                        grd.MainView = grv
                        grd.DataSource = table
                        grd.ForceInitialize()
                        fn_LoadFormatData()
                    Else
                        MsgBox(CodeResponse & " - " & messageResponse, MsgBoxStyle.Exclamation, Me.Text)
                    End If
                Else
                    MsgBox("Insert SEP Data Gagal/Kosong", MsgBoxStyle.Exclamation, Me.Text)
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
                Dim dsSetKoneksi = oSetKoneksi.GetDataVClaimMonitoringDataKlaimJaminanJasaRaharja(sUrlVclaim, sConsidVclaim, sSecreateKeyVclaim, sUserKeyVclaim, uTime, deDATEFrom.DateTime.ToString("yyyy-mmdd"), deDateTo.DateTime.ToString("yyyy-mmdd"))

                If dsSetKoneksi <> "" Then
                    Dim allData = JObject.Parse(dsSetKoneksi)

                    Dim CodeResponse As String = String.Empty
                    Dim messageResponse As String = String.Empty

                    CodeResponse = IIf(IsDBNull(allData.Item("metaData").Item("code")) = True, "", allData.Item("metaData").Item("code"))
                    messageResponse = allData("metaData")("message").ToString

                    If CodeResponse = "200" Then
                        Dim DataDecrypt = JObject.Parse(oSetKoneksi.Decrypt(allData("response"), sConsidVclaim & sSecreateKeyVclaim & uTime))

                        'Dim table As DataTable

                        'table = New DataTable("DATAHISTORIPELAYANAN")
                        'table.Columns.Add("diagnosa")
                        'table.Columns.Add("jnsPelayanan")
                        'table.Columns.Add("kelasRawat")
                        'table.Columns.Add("namaPeserta")
                        'table.Columns.Add("noKartu")
                        'table.Columns.Add("noSep")
                        'table.Columns.Add("noRujukan")
                        'table.Columns.Add("poli")
                        'table.Columns.Add("ppkPelayanan")
                        'table.Columns.Add("tglPlgSep")
                        'table.Columns.Add("tglSep")

                        'For Each item In DataDecrypt("histori")
                        '    table.Rows.Add(New String() {item("diagnosa"), item("jnsPelayanan"), item("kelasRawat"), item("namaPeserta"), item("noKartu"), item("noSep"), item("noRujukan"), item("poli"), item("ppkPelayanan"), item("tglPlgSep"), item("tglSep")})
                        'Next

                        grd.MainView = grv
                        grd.DataSource = DataDecrypt
                        grd.ForceInitialize()
                        fn_LoadFormatData()
                    Else
                        MsgBox(CodeResponse & " - " & messageResponse, MsgBoxStyle.Exclamation, Me.Text)
                    End If
                Else
                    MsgBox("Insert SEP Data Gagal/Kosong", MsgBoxStyle.Exclamation, Me.Text)
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
                grv.Columns(iLoop).DisplayFormat.FormatString = "{0:dd/MM/yyyy}"
            End If
        Next
        'grv.Columns("KELAS").Group()
        'grv.Columns("RUANGAN").Group()
        'grv.ExpandAllGroups()
    End Sub
    Private Sub grv_RowStyle(sender As System.Object, e As DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs) Handles grv.RowStyle
        If grv.IsFilterRow(e.RowHandle) Then Exit Sub
        If CDec(grv.GetRowCellValue(e.RowHandle, "TERSEDIA")) = 0 Then
            'e.Appearance.BackColor = Color.YellowGreen
        Else
            e.Appearance.BackColor = Color.LightGreen
        End If
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

        Try
            grv.RestoreLayoutFromRegistry("HKEY_CURRENT_USER\Software\SIMRS\SW\" & sUserID & "\" & Me.Text)
        Catch ex As Exception

        End Try
    End Sub
    Private Sub cboMonitoring_SelectedValueChanged(sender As Object, e As EventArgs) Handles cboMonitoring.SelectedValueChanged
        fn_Sembunyikan()
    End Sub
#End Region
End Class