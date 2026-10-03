Imports DataAccess
Imports UI.WIN.MAIN.My.Resources
Imports System.Linq

Imports DevExpress.XtraPrinting
Imports Newtonsoft.Json.Linq

Imports System.Data
Imports System.Data.SqlClient


Public Class frmDasboardTempatTidurRS
    Implements ILanguage

#Region "Function"
    Private Sub Me_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.Text = "Laporan Aplicare"
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
            Me.Text = "Laporan Aplicare"

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
                      Where x.MODUL = "DASBOARDAPLICARE" _
                      And y.KDUSER = sUserID
                      Select x.ISADD, x.ISDELETE, x.ISUPDATE, x.ISPRINT, x.ISVIEW).FirstOrDefault

            Try
                'picPrint.Enabled = ds.ISPRINT
                'picRefresh.Enabled = ds.ISVIEW

                If ds.ISVIEW = True Then
                    fn_Preview()
                    fn_LoadLanguage()
                    Timer1.Start()
                End If
            Catch oErr As Exception
                MsgBox(Statement.SecurityNotInstalled, MsgBoxStyle.Exclamation, Me.Text)

                'picPrint.Enabled = False
                'picRefresh.Enabled = False
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
{"", "APLICARE ", ""})

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

            fn_LoadData01()

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
            SQL &= "A.KDKELASAPLICARE "
            SQL &= ",Kelas = B.MEMO "
            SQL &= ",NamaRuangan = C.NAME_DISPLAY "
            SQL &= ",A.KAPASITAS "
            SQL &= ",A.TERSEDIA "
            SQL &= ",A.TERSEDIA_LAKI "
            SQL &= ",A.TERSEDIA_PEREMPUAN "
            SQL &= ",A.TERSEDIA_LAKIPEREMPUAN "
            SQL &= "FROM "
            SQL &= "M_KELASAPLICARE_DEPARTMENT A "
            SQL &= "INNER JOIN M_KELASAPLICARE B "
            SQL &= "ON A.KDKELASAPLICARE = B.KDKELASAPLICARE "
            SQL &= "INNER JOIN M_DEPARTMENT C "
            SQL &= "ON A.KDDEPARTMENT = C.KDDEPARTMENT "

            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "I_MONITRING_H")

            grd.MainView = grv
            grd.DataSource = ds.Tables("I_MONITRING_H")
            grd.ForceInitialize()

            fn_LoadFormatData()

            lblVVIP.Text = 0
            lblVIP.Text = 0
            lblUTAMA.Text = 0
            lblKELASI.Text = 0
            lblKELASII.Text = 0
            lblKELASIII.Text = 0
            lblICU.Text = 0
            lblICCU.Text = 0
            lblNICU.Text = 0
            lblPICU.Text = 0
            lblIGD.Text = 0
            lblUGD.Text = 0
            lblBERSALIN.Text = 0
            lblHCU.Text = 0
            lblISOLASI.Text = 0
            lblNONKELAS.Text = 0

            For iLoop As Integer = 0 To ds.Tables("I_MONITRING_H").Rows.Count - 1
                With ds.Tables("I_MONITRING_H")
                    If .Rows(iLoop)("KDKELASAPLICARE") = "VVP" Then
                        lblVVIP.Text += .Rows(iLoop)("TERSEDIA")
                    End If
                    If .Rows(iLoop)("KDKELASAPLICARE") = "VIP" Then
                        lblVIP.Text += .Rows(iLoop)("TERSEDIA")
                    End If
                    If .Rows(iLoop)("KDKELASAPLICARE") = "UTM" Then
                        lblUTAMA.Text += .Rows(iLoop)("TERSEDIA")
                    End If
                    If .Rows(iLoop)("KDKELASAPLICARE") = "KL1" Then
                        lblKELASI.Text += .Rows(iLoop)("TERSEDIA")
                    End If
                    If .Rows(iLoop)("KDKELASAPLICARE") = "KL2" Then
                        lblKELASII.Text += .Rows(iLoop)("TERSEDIA")
                    End If
                    If .Rows(iLoop)("KDKELASAPLICARE") = "KL3" Then
                        lblKELASIII.Text += .Rows(iLoop)("TERSEDIA")
                    End If
                    If .Rows(iLoop)("KDKELASAPLICARE") = "ICU" Then
                        lblICU.Text += .Rows(iLoop)("TERSEDIA")
                    End If
                    If .Rows(iLoop)("KDKELASAPLICARE") = "ICC" Then
                        lblICCU.Text += .Rows(iLoop)("TERSEDIA")
                    End If
                    If .Rows(iLoop)("KDKELASAPLICARE") = "NIC" Then
                        lblNICU.Text += .Rows(iLoop)("TERSEDIA")
                    End If
                    If .Rows(iLoop)("KDKELASAPLICARE") = "PIC" Then
                        lblPICU.Text += .Rows(iLoop)("TERSEDIA")
                    End If
                    If .Rows(iLoop)("KDKELASAPLICARE") = "IGD" Then
                        lblIGD.Text += .Rows(iLoop)("TERSEDIA")
                    End If
                    If .Rows(iLoop)("KDKELASAPLICARE") = "UGD" Then
                        lblUGD.Text += .Rows(iLoop)("TERSEDIA")
                    End If
                    If .Rows(iLoop)("KDKELASAPLICARE") = "SAL" Then
                        lblBERSALIN.Text += .Rows(iLoop)("TERSEDIA")
                    End If
                    If .Rows(iLoop)("KDKELASAPLICARE") = "HCU" Then
                        lblHCU.Text += .Rows(iLoop)("TERSEDIA")
                    End If
                    If .Rows(iLoop)("KDKELASAPLICARE") = "ISO" Then
                        lblISOLASI.Text += .Rows(iLoop)("TERSEDIA")
                    End If
                    If .Rows(iLoop)("KDKELASAPLICARE") = "NON" Then
                        lblNONKELAS.Text += .Rows(iLoop)("TERSEDIA")
                    End If
                End With
            Next

            If oConn.State = ConnectionState.Open Then
                oConn.Close()
            End If

        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadData02()
        'Try
        '    Dim uTime As Integer
        '    Dim thisDayUTC As DateTime = DateTime.UtcNow
        '    Dim HasilKey As String = ""
        '    Dim result As String = String.Empty

        '    'Dim COnsIDAplicares = "16694"
        '    'Dim SecretKeyAplicares = "9kODD4D793"

        '    uTime = (thisDayUTC - New DateTime(1970, 1, 1, 0, 0, 0)).TotalSeconds

        '    ' Initialize the keyed hash object using the secret key as the key
        '    Dim hashObject As New HMACSHA256(Encoding.UTF8.GetBytes(SecretKeyAplicares))
        '    ' Computes the signature by hashing the salt with the secret key as the key
        '    Dim signature = hashObject.ComputeHash(Encoding.UTF8.GetBytes(COnsIDAplicares & "&" & uTime))
        '    ' Base 64 Encode
        '    HasilKey = Convert.ToBase64String(signature)

        '    Dim req As WinHttp.WinHttpRequest

        '    'Dim Url As String = "https://new-api.bpjs-kesehatan.go.id/aplicaresws/" & "rest/ref/kelas"

        '    Dim Url As String = "https://new-api.bpjs-kesehatan.go.id/aplicaresws/" & "rest/bed/read/" & PPKPelayanan & "/1/100"

        '    req = New WinHttp.WinHttpRequest
        '    req.Open("GET", Url, False)
        '    req.SetRequestHeader("X-Cons-ID", COnsIDAplicares)
        '    req.SetRequestHeader("X-Timestamp", uTime)
        '    req.SetRequestHeader("X-Signature", HasilKey)

        '    req.Send()

        '    If req.Status = "200" Then
        '        result = req.ResponseText

        '        Dim allData = JObject.Parse(result)
        '        Dim OK = allData("metadata")("code").ToString

        '        If OK = 1 Then
        '            Dim table As DataTable

        '            table = New DataTable("I_REPOT_REKAP")
        '            table.Columns.Add("rownumber")
        '            table.Columns.Add("namakelas")
        '            table.Columns.Add("kodekelas")
        '            table.Columns.Add("koderuang")
        '            table.Columns.Add("namaruang")
        '            table.Columns.Add("kapasitas")
        '            table.Columns.Add("tersedia")
        '            table.Columns.Add("tersediapria")
        '            table.Columns.Add("tersediawanita")
        '            table.Columns.Add("tersediapriawanita")
        '            table.Columns.Add("lastupdate")

        '            For Each item In allData("response")("list")

        '                table.Rows.Add(New String() {CInt(item("rownumber")), item("namakelas"), item("kodekelas"), item("koderuang"), item("namaruang"), item("kapasitas"), item("tersedia"), item("tersediapria"), item("tersediawanita"), item("tersediapriawanita"), item("lastupdate")})

        '            Next

        '            grd.DataSource = table
        '            grd.ForceInitialize()

        '            fn_SetFormat()

        '        Else

        '            MsgBox("BPJS Error Code : " & allData("metadata")("code").ToString & " Pesan : " & allData("metadata")("message").ToString, MsgBoxStyle.Information)

        '        End If

        '    Else
        '        MsgBox("BPJS Error " & req.Status, MsgBoxStyle.Information)
        '    End If

        'Catch ex As Exception
        '    MsgBox("Error ", MsgBoxStyle.Exclamation, Me.Text)
        'End Try
    End Sub
    Private Sub fn_LoadFormatData()
        'For iLoop As Integer = 0 To grv.Columns.Count - 1
        '    If grv.Columns(iLoop).ColumnType.Name = "Decimal" Then
        '        grv.Columns(iLoop).DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        '        grv.Columns(iLoop).DisplayFormat.FormatString = "{0:n2}"
        '        grv.Columns(iLoop).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        '    ElseIf grv.Columns(iLoop).ColumnType.Name = "DateTime" Then
        '        grv.Columns(iLoop).DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        '        grv.Columns(iLoop).DisplayFormat.FormatString = "{0:dd/MM/yyyy}"
        '    End If
        'Next

        grv.Columns("Kelas").Group()
        grv.ExpandAllGroups()

        grv.Columns("KAPASITAS").Caption = "Kapasitas"
        grv.Columns("TERSEDIA").Caption = "Tersedia"
        grv.Columns("TERSEDIA_LAKI").Caption = "Tersedia (Laki-laki)"
        grv.Columns("TERSEDIA_PEREMPUAN").Caption = "Tersedia (Perempuan)"
        grv.Columns("TERSEDIA_LAKIPEREMPUAN").Caption = "Tersedia (Laki-laki/Perempuan)"

        grv.Columns("KDKELASAPLICARE").VisibleIndex = -1
    End Sub
    'Private Sub grv_RowStyle(sender As System.Object, e As DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs) Handles grv.RowStyle
    '    If grv.IsFilterRow(e.RowHandle) Then Exit Sub
    '    If CDec(grv.GetRowCellValue(e.RowHandle, "TERSEDIA")) = 0 Then
    '        'e.Appearance.BackColor = Color.YellowGreen
    '    Else
    '        e.Appearance.BackColor = Color.LightGreen
    '    End If
    'End Sub
#End Region
    'Private Sub MasterColumnChooserToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles MasterColumnChooserToolStripMenuItem.Click
    '    grv.ShowCustomization()
    'End Sub
    'Private Sub DetailColumnChooserToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles DetailColumnChooserToolStripMenuItem.Click
    '    grv.ShowCustomization()
    'End Sub
#End Region
#Region "Command Button"
    Private Sub frmMember_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        Select Case e.KeyCode
            Case Keys.Escape
                Me.Close()
                'Case Keys.P
                '    If e.Alt = True And picPrint.Enabled = True Then
                '        picPrint_Click()
                '    End If
            Case Keys.R
                fn_LoadSecurity()
        End Select
    End Sub
    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        fn_LoadData01()
    End Sub
    'Private Sub picPrint_Click() Handles picPrint.Click
    '    Try
    '        fn_Print()
    '    Catch oErr As Exception
    '        MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
    '    End Try
    'End Sub
    'Private Sub picRefresh_Click() Handles picRefresh.Click
    '    fn_LoadSecurity()

    '    Try
    '        grv.RestoreLayoutFromRegistry("HKEY_CURRENT_USER\Software\SIMRS\SW\" & sUserID & "\" & Me.Text)
    '    Catch ex As Exception

    '    End Try
    'End Sub


#End Region
End Class