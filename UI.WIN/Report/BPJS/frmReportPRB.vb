Imports DataAccess
Imports UI.WIN.MAIN.My.Resources
Imports System.Linq

Imports DevExpress.XtraPrinting
Imports Newtonsoft.Json.Linq

Imports System.Data
Imports System.Data.SqlClient


Public Class frmReportPRB
    Implements ILanguage

#Region "Function"
    Private Sub Me_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.Text = "Laporan SRB"
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
            Me.Text = "Laporan SRB"

            'fn_LoadLanguageAll()

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
                      Where x.MODUL = "LAPORAN_PRB" _
                      And y.KDUSER = sUserID
                      Select x.ISADD, x.ISDELETE, x.ISUPDATE, x.ISPRINT, x.ISVIEW).FirstOrDefault

            Try
                picPrint.Enabled = ds.ISPRINT
                picRefresh.Enabled = ds.ISVIEW
                txtNomorSRB.Properties.ReadOnly = False

                If ds.ISVIEW = True Then
                    fn_Preview()
                    fn_LoadLanguage()
                End If
            Catch oErr As Exception
                MsgBox(Statement.SecurityNotInstalled, MsgBoxStyle.Exclamation, Me.Text)

                picPrint.Enabled = False
                picRefresh.Enabled = False
                txtNomorSRB.Properties.ReadOnly = True
            End Try
        Catch oErr As Exception
            MsgBox(Statement.SecurityNotInstalled, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_Print()
        Try
            printableComponentLink.Landscape = True
            printableComponentLink.PaperKind = Printing.PaperKind.A4

            Dim phf As PageHeaderFooter =
        TryCast(printableComponentLink.PageHeaderFooter, PageHeaderFooter)
            phf.Header.Content.Clear()
            phf.Header.Font = New Font("Times New Roman", 14, FontStyle.Bold)
            phf.Header.LineAlignment = BrickAlignment.Center
            phf.Footer.Font = New Font("Times New Roman", 9.75)
            phf.Footer.LineAlignment = BrickAlignment.Far
            phf.Footer.Content.AddRange(New String() _
        {sWATERMARK, "", Report.REPORT_PAGE & " : [Page # of Pages #]"})

            phf.Header.Content.AddRange(New String() _
{"", "Laporan Program PRB " & cboType.Text.ToString.Trim.ToUpper, ""})

            printableComponentLink.Component = grd
            printableComponentLink.CreateDocument()
            printableComponentLink.ShowPreviewDialog(Me)
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

            Select Case cboType.SelectedIndex
                Case 0
                    If txtNomorSRB.Text <> "" And txtNomorSEP.Text <> "" Then
                        fn_LoadData01()
                    End If
                Case 1
                    fn_LoadData02()
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
                Dim dsSetKoneksi = oSetKoneksi.NomorSRB(sUrlVclaim, sConsidVclaim, sSecreateKeyVclaim, sUserKeyVclaim, uTime, txtNomorSRB.Text, txtNomorSEP.Text)

                If dsSetKoneksi <> "" Then
                    Dim allData = JObject.Parse(dsSetKoneksi)

                    Dim CodeResponse As String = String.Empty
                    Dim messageResponse As String = String.Empty

                    CodeResponse = IIf(IsDBNull(allData.Item("metaData").Item("code")) = True, "", allData.Item("metaData").Item("code"))
                    messageResponse = allData("metaData")("message").ToString

                    If CodeResponse = "200" Then
                        Dim DataDecrypt = JObject.Parse(oSetKoneksi.Decrypt(allData("response"), sConsidVclaim & sSecreateKeyVclaim & uTime))

                        MsgBox(CodeResponse & " - " & DataDecrypt.ToString(), MsgBoxStyle.Exclamation, Me.Text)

                        'Dim table As DataTable

                        'table = New DataTable("ObatGenerikPRB")
                        'table.Columns.Add("kode")
                        'table.Columns.Add("nama")

                        'For Each item In DataDecrypt("list")
                        '    table.Rows.Add(New String() {item("kode"), item("nama")})
                        'Next

                        'grd.MainView = grv
                        'grd.DataSource = table
                        'grd.ForceInitialize()
                        'fn_LoadFormatData()
                        'txtNomorSRB.ResetText()
                    Else
                        MsgBox(CodeResponse & " - " & messageResponse, MsgBoxStyle.Exclamation, Me.Text)
                    End If
                Else
                    MsgBox("Obat Generik Program PRB Kosong", MsgBoxStyle.Exclamation, Me.Text)
                End If
            Else
                MsgBox("Consid Kosong", MsgBoxStyle.Exclamation, Me.Text)
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
                Dim dsSetKoneksi = oSetKoneksi.TanggalSRB(sUrlVclaim, sConsidVclaim, sSecreateKeyVclaim, sUserKeyVclaim, uTime, deDATEFrom.DateTime.ToString("yyyy-MM-dd"), deDateTo.DateTime.ToString("yyyy-MM-dd"))

                If dsSetKoneksi <> "" Then
                    Dim allData = JObject.Parse(dsSetKoneksi)

                    Dim CodeResponse As String = String.Empty
                    Dim messageResponse As String = String.Empty

                    CodeResponse = IIf(IsDBNull(allData.Item("metaData").Item("code")) = True, "", allData.Item("metaData").Item("code"))
                    messageResponse = allData("metaData")("message").ToString

                    If CodeResponse = "200" Then
                        Dim DataDecrypt = JObject.Parse(oSetKoneksi.Decrypt(allData("response"), sConsidVclaim & sUserKeyVclaim & uTime))

                        MsgBox(CodeResponse & " - " & DataDecrypt.ToString(), MsgBoxStyle.Exclamation, Me.Text)

                    Else
                        MsgBox(CodeResponse & " - " & messageResponse, MsgBoxStyle.Exclamation, Me.Text)
                    End If
                Else
                    MsgBox("Obat Generik Program PRB Kosong", MsgBoxStyle.Exclamation, Me.Text)
                End If
            Else
                MsgBox("Consid Kosong", MsgBoxStyle.Exclamation, Me.Text)
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
    Private Sub cboType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboType.SelectedIndexChanged
        If cboType.SelectedIndex = 0 Then
            deDATEFrom.Properties.ReadOnly = True
            deDateTo.Properties.ReadOnly = True

            txtNomorSEP.Properties.ReadOnly = False
            txtNomorSRB.Properties.ReadOnly = False

        Else
            deDATEFrom.Properties.ReadOnly = False
            deDateTo.Properties.ReadOnly = False

            txtNomorSEP.Properties.ReadOnly = True
            txtNomorSRB.Properties.ReadOnly = True
            txtNomorSRB.ResetText()
            txtNomorSEP.ResetText()
        End If
    End Sub
#End Region
End Class