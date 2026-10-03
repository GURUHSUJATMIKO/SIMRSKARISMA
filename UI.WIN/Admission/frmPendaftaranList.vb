Imports DataAccess
Imports UI.WIN.MAIN.My.Resources
Imports System.Linq
Imports System.Data.SqlClient
Imports Newtonsoft.Json.Linq
Imports DevExpress.XtraPrinting

Public Class frmPendaftaranList
    Implements ILanguage

    Private oFormMode As FORM_MODE = FORM_MODE.FORM_MODE_VIEW
    Private oPendaftaran As New Admission.clsPendaftaran
#Region "Function"
    Private Sub Me_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.Text = Pendaftaran.TITLE

        deDATEFrom.DateTime = Now
        deDATETo.DateTime = Now

        fn_LoadSecurity()

        Try
            grv.RestoreLayoutFromRegistry("HKEY_CURRENT_USER\Software\SIMRS\SW\" & sUserID & "\" & Me.Text)
        Catch ex As Exception

        End Try
    End Sub
    Private Sub Me_FormClosed(sender As System.Object, e As System.Windows.Forms.FormClosedEventArgs) Handles MyBase.FormClosed
        Try
            grv.SaveLayoutToRegistry("HKEY_CURRENT_USER\Software\SIMRS\SW\" & sUserID & "\" & Me.Text)
        Catch ex As Exception

        End Try
    End Sub
    Private Sub fn_LoadSecurity()
        Try
            Dim oOtority As New Setting.clsOtority
            Dim oUser As New Setting.clsUser

            Dim ds = (From x In oOtority.GetDataDetail
                      Join y In oUser.GetData
                      On x.KDOTORITY Equals y.KDOTORITY
                      Where x.MODUL = "PENDAFTARAN" _
                      And y.KDUSER = sUserID
                      Select x.ISADD, x.ISDELETE, x.ISUPDATE, x.ISPRINT, x.ISVIEW).FirstOrDefault

            Try
                picAdd.Enabled = ds.ISADD
                picDelete.Enabled = ds.ISDELETE
                picBatal.Enabled = ds.ISDELETE
                picUpdate.Enabled = ds.ISUPDATE
                picPrint.Enabled = ds.ISPRINT
                picRefresh.Enabled = ds.ISVIEW

                If ds.ISVIEW = True Then
                    fn_LoadData()
                    fn_LoadLanguage()

                    Try
                        grv.RestoreLayoutFromRegistry("HKEY_CURRENT_USER\Software\SIMRS\SW\" & sUserID & "\" & Me.Text)
                    Catch ex As Exception

                    End Try
                End If
            Catch oErr As Exception
                MsgBox(Statement.SecurityNotInstalled, MsgBoxStyle.Exclamation, Me.Text)

                picAdd.Enabled = False
                picDelete.Enabled = False
                picBatal.Enabled = False
                picUpdate.Enabled = False
                picPrint.Enabled = False
                picRefresh.Enabled = False
            End Try
        Catch oErr As Exception
            MsgBox(Statement.SecurityNotInstalled, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Public Sub fn_LoadLanguage() Implements ILanguage.fn_LoadLanguage
        Try
            Me.Text = Pendaftaran.TITLE

            fn_LoadLanguageMaster()
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Public Sub fn_LoadLanguageMaster()
        Try
            grv.Columns("KDPENDAFTARAN").Caption = Pendaftaran.KDPENDAFTARAN
            grv.Columns("PENJAMIN").Caption = "Penjamin"
            grv.Columns("TANGGAL").Caption = Pendaftaran.TANGGAL
            grv.Columns("TUJUAN").Caption = Pendaftaran.KDDEPARTMENT
            grv.Columns("DOKTER").Caption = Pendaftaran.KDDOCTOR
            'grv.Columns("DOKTER_PELAYANAN").Caption = Pendaftaran.KDDOCTOR_PELAYANAN
            grv.Columns("KDCUSTOMER").Caption = Customer.KDCUSTOMER
            grv.Columns("PASIEN").Caption = Customer.NAME_DISPLAY
            grv.Columns("KDUSER").Caption = Caption.User
            'grv.Columns("DAFTAR_L1").Caption = sDaftar_L1
            grv.Columns("DAFTAR_L2").Caption = sDaftar_L2
            'grv.Columns("DAFTAR_L3").Caption = sDaftar_L3
            grv.Columns("DAFTAR_L4").Caption = sDaftar_L4
            'grv.Columns("DAFTAR_L5").Caption = sDaftar_L5
            grv.Columns("USIA").Caption = "Usia"
            grv.Columns("DAFTAR_L6").Caption = sDaftar_L6
            grv.Columns("STATUSDAFTAR").Caption = Pendaftaran.STATUSDAFTAR
            grv.Columns("LAMA").Caption = "Lama?"
        Catch oErr As Exception

        End Try
    End Sub
    Private Sub fn_LoadData()
        Try
            grv.Columns.Clear()
            grd.DataSource = Nothing

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
            'SQL &= "JenisRawat = (SELECT CASE A.CATEGORY WHEN 0 THEN 'Rawat Jalan' ELSE 'Rawat Inap' END) "
            SQL &= "A.KDPENDAFTARAN "
            SQL &= ",NoSEP = A.NOMORSEP "
            SQL &= ",PASIEN = D.NAME_DISPLAY "
            SQL &= ",PENJAMIN = M.MEMO "
            SQL &= ",A.KDCUSTOMER "
            SQL &= ",DOKTER = C.NAME_DISPLAY "
            SQL &= ",A.USIA "
            SQL &= ",TUJUAN = B.NAME_DISPLAY  "
            SQL &= ",TANGGAL = A.DATE "
            'SQL &= ",DAFTAR_L1 = E.MEMO "
            'SQL &= ",DAFTAR_L3 = G.MEMO "
            SQL &= ",DAFTAR_L4 = H.MEMO "
            SQL &= ",DAFTAR_L2 = F.MEMO "
            SQL &= ",FaskesRujukan = K.MEMO  "
            SQL &= ",DAFTAR_L6 = J.MEMO "
            SQL &= ",DokumenRM = ISNULL((SELECT CONVERT(bit,1) FROM S_TRACKING WHERE A.KDPENDAFTARAN = KDPENDAFTARAN AND ISCHEKED = 1), ISNULL((SELECT CONVERT(bit,0) FROM S_TRACKING WHERE A.KDPENDAFTARAN = KDPENDAFTARAN AND ISCHEKED = 0), CONVERT(bit,0))) "
            'SQL &= ",DAFTAR_L5 = I.MEMO "
            SQL &= ",WaktuKetemuBerkas = ISNULL((SELECT convert(varchar,DATE_KIRIM,108) FROM S_TRACKING WHERE A.KDPENDAFTARAN = KDPENDAFTARAN AND ISCHEKED = 1), '') "
            SQL &= ",STATUSDAFTAR = (SELECT CASE A.STATUSDAFTAR WHEN 0 THEN 'DALAM ANTRIAN' WHEN 1 THEN 'BATAL' ELSE 'SELESAI' END) "
            SQL &= ",Keterangan = A.CATATAN "
            SQL &= ",LAMA = A.ISPASIENLAMA "
            SQL &= ",Praktek = (SELECT CASE A.ISJAGA WHEN 0 THEN 'PAGI' ELSE 'SORE' END) "
            'SQL &= ",DOKTER_PELAYANAN = L.NAME_DISPLAY "
            'SQL &= ",NoRujukan = A.NOMORRUJUKAN "
            SQL &= ",SuratKontrol = ISNULL((SELECT KDSKD FROM S_PENDAFTARAN_SKD WHERE KDPENDAFTARAN = A.KDPENDAFTARAN), '') "
            SQL &= ",NaikRanap = (SELECT CASE A.CATEGORY WHEN 0 THEN (SELECT CASE A.KDPENDAFTARAN_AWAL WHEN '' THEN CONVERT(BIT, 0) ELSE CONVERT(BIT, 1) END) ELSE CONVERT(BIT, 0) END) "
            SQL &= ",A.KDUSER "
            SQL &= "FROM "
            SQL &= "S_PENDAFTARAN_H A "
            SQL &= "INNER JOIN M_DEPARTMENT B "
            SQL &= "ON A.KDDEPARTMENT = B.KDDEPARTMENT "
            SQL &= "INNER JOIN M_DOCTOR C "
            SQL &= "ON A.KDDOCTOR = C.KDDOCTOR "
            SQL &= "INNER JOIN M_CUSTOMER D "
            SQL &= "ON A.KDCUSTOMER = D.KDCUSTOMER "
            SQL &= "INNER JOIN M_DAFTAR_L1 E "
            SQL &= "ON A.KDDAFTAR_L1 = E.KDDAFTAR_L1 "
            SQL &= "INNER JOIN M_DAFTAR_L2 F "
            SQL &= "ON A.KDDAFTAR_L2 = F.KDDAFTAR_L2 "
            SQL &= "INNER JOIN M_DAFTAR_L3 G "
            SQL &= "ON A.KDDAFTAR_L3 = G.KDDAFTAR_L3 "
            SQL &= "INNER JOIN M_DAFTAR_L4 H "
            SQL &= "ON A.KDDAFTAR_L4 = H.KDDAFTAR_L4 "
            SQL &= "INNER JOIN M_DAFTAR_L5 I "
            SQL &= "ON A.KDDAFTAR_L5 = I.KDDAFTAR_L5 "
            SQL &= "INNER JOIN M_DAFTAR_L6 J "
            SQL &= "ON A.KDDAFTAR_L6 = J.KDDAFTAR_L6 "
            SQL &= "INNER JOIN M_PPK K "
            SQL &= "ON A.KDPPK = K.KDPPK "
            SQL &= "INNER JOIN M_DOCTOR L "
            SQL &= "ON A.KDDOCTOR = L.KDDOCTOR "
            SQL &= "INNER JOIN M_PENJAMIN M "
            SQL &= "ON A.KDPENJAMIN = M.KDPENJAMIN "
            SQL &= "WHERE CONVERT(VARCHAR(8), A.DATE, 112) >= '" & deDATEFrom.DateTime.ToString("yyyyMMdd") & "' AND CONVERT(VARCHAR(8), A.DATE, 112) <= '" & deDATETo.DateTime.ToString("yyyyMMdd") & "' "
            SQL &= "ORDER BY A.DATE DESC "

            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "ALL")

            grd.MainView = grv
            grd.DataSource = ds.Tables("ALL")
            grd.ForceInitialize()

            fn_LoadFormatData()

            If oConn.State = ConnectionState.Open Then
                oConn.Close()
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
        grv.Columns("NoSEP").VisibleIndex = -1
        grv.BestFitColumns()
    End Sub
    Private Sub MasterColumnChooserToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles MasterColumnChooserToolStripMenuItem.Click
        grv.ShowCustomization()
    End Sub
    Private Function fn_CariSEP() As String
        Try
            If grv.GetFocusedRowCellValue("NoSEP") = String.Empty Then
                fn_CariSEP = "KOSONG"
                Exit Function
            End If
            '
            If grv.GetFocusedRowCellValue("NoSEP") = "" Or grv.GetFocusedRowCellValue("NoSEP") = "<--- AUTO --->" Then
                fn_CariSEP = "KOSONG"
                Exit Function
            End If

            Dim oSetKoneksi As New Brigging.clsSetKoneksi
            Dim dsDataSetKoneksi = oSetKoneksi.GetData().FirstOrDefault(Function(x) x.ISACTIVE = True And x.NAME_DISPLAY = "VCLAIM")
            Dim uTime As Integer = 0

            If dsDataSetKoneksi IsNot Nothing Then
                uTime = (DateTime.UtcNow - New DateTime(1970, 1, 1, 0, 0, 0)).TotalSeconds
                Dim dsSetKoneksi = oSetKoneksi.CariSEP(dsDataSetKoneksi.ALAMATWEB, dsDataSetKoneksi.CONSID, dsDataSetKoneksi.SECREATKEY, dsDataSetKoneksi.USER_KEY, uTime, grv.GetFocusedRowCellValue("NoSEP"))

                If dsSetKoneksi <> "" Then
                    Dim allData = JObject.Parse(dsSetKoneksi)

                    Dim CodeResponse As String = String.Empty
                    Dim messageResponse As String = String.Empty

                    CodeResponse = IIf(IsDBNull(allData.Item("metaData").Item("code")) = True, "", allData.Item("metaData").Item("code"))
                    messageResponse = allData("metaData")("message").ToString

                    If CodeResponse = "200" Then
                        fn_CariSEP = "ADA"
                        'Dim DataDecrypt = JObject.Parse(oSetKoneksi.Decrypt(allData("response"), dsDataSetKoneksi.CONSID & dsDataSetKoneksi.SECREATKEY & uTime))
                    Else
                        fn_CariSEP = "KOSONG"
                    End If
                Else
                    fn_CariSEP = "KOSONG"
                End If
            Else
                fn_CariSEP = "KOSONG"
            End If
        Catch oErr As Exception
            fn_CariSEP = "KOSONG"
        End Try
    End Function
    Private Function fn_DeleteSEP() As Boolean
        Try
            If grv.GetFocusedRowCellValue("NoSEP") = String.Empty Then
                fn_DeleteSEP = True
                Exit Function
            End If

            Dim oSetKoneksi As New Brigging.clsSetKoneksi
            Dim dsDataSetKoneksi = oSetKoneksi.GetData().FirstOrDefault(Function(x) x.ISACTIVE = True And x.NAME_DISPLAY = "VCLAIM")
            Dim uTime As Integer = 0

            If dsDataSetKoneksi IsNot Nothing Then
                Dim jsonRequest As String = String.Empty

                jsonRequest = "{" & """request"": {" & """t_sep"": {" & """noSep"": """ & grv.GetFocusedRowCellValue("NoSEP") & """," & """user"": """ & sUserID & """" & "}}}"

                uTime = (DateTime.UtcNow - New DateTime(1970, 1, 1, 0, 0, 0)).TotalSeconds
                Dim dsSetKoneksi = oSetKoneksi.HapusSEP(dsDataSetKoneksi.ALAMATWEB, dsDataSetKoneksi.CONSID, dsDataSetKoneksi.SECREATKEY, dsDataSetKoneksi.USER_KEY, uTime, jsonRequest)

                If dsSetKoneksi <> "" Then
                    Dim allData = JObject.Parse(dsSetKoneksi)

                    Dim CodeResponse As String = String.Empty
                    Dim messageResponse As String = String.Empty

                    CodeResponse = IIf(IsDBNull(allData.Item("metaData").Item("code")) = True, "", allData.Item("metaData").Item("code"))
                    messageResponse = allData("metaData")("message").ToString

                    If CodeResponse = "200" Then
                        fn_DeleteSEP = True
                        Dim DataDecrypt = oSetKoneksi.Decrypt(allData("response"), dsDataSetKoneksi.CONSID & dsDataSetKoneksi.SECREATKEY & uTime)
                    Else
                        fn_DeleteSEP = False
                        MsgBox(CodeResponse & " - " & messageResponse, MsgBoxStyle.Exclamation, Me.Text)
                    End If
                Else
                    fn_DeleteSEP = False
                    MsgBox("Delete SEP Data Gagal/Kosong", MsgBoxStyle.Exclamation, Me.Text)
                End If
            Else
                fn_DeleteSEP = False
                MsgBox("Koneksi Tidak ditemukan", MsgBoxStyle.Exclamation, Me.Text)
            End If
        Catch oErr As Exception
            fn_DeleteSEP = False
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Function
    Private Function fn_DeleteSEPv2() As Boolean
        Try
            If grv.GetFocusedRowCellValue("NoSEP") = String.Empty Then
                fn_DeleteSEPv2 = True
                Exit Function
            End If

            Dim oSetKoneksi As New Brigging.clsSetKoneksi
            Dim dsDataSetKoneksi = oSetKoneksi.GetData().FirstOrDefault(Function(x) x.ISACTIVE = True And x.NAME_DISPLAY = "VCLAIM")
            Dim uTime As Integer = 0

            If dsDataSetKoneksi IsNot Nothing Then
                Dim jsonRequest As String = String.Empty

                jsonRequest = "{" & """request"": {" & """t_sep"": {" & """noSep"": """ & grv.GetFocusedRowCellValue("NoSEP") & """," & """user"": """ & sUserID & """" & "}}}"

                uTime = (DateTime.UtcNow - New DateTime(1970, 1, 1, 0, 0, 0)).TotalSeconds
                Dim dsSetKoneksi = oSetKoneksi.HapusSEPv2(dsDataSetKoneksi.ALAMATWEB, dsDataSetKoneksi.CONSID, dsDataSetKoneksi.SECREATKEY, dsDataSetKoneksi.USER_KEY, uTime, jsonRequest)

                If dsSetKoneksi <> "" Then
                    Dim allData = JObject.Parse(dsSetKoneksi)

                    Dim CodeResponse As String = String.Empty
                    Dim messageResponse As String = String.Empty

                    CodeResponse = IIf(IsDBNull(allData.Item("metaData").Item("code")) = True, "", allData.Item("metaData").Item("code"))
                    messageResponse = allData("metaData")("message").ToString

                    If CodeResponse = "200" Then
                        fn_DeleteSEPv2 = True
                        Dim DataDecrypt = oSetKoneksi.Decrypt(allData("response"), dsDataSetKoneksi.CONSID & dsDataSetKoneksi.SECREATKEY & uTime)
                    Else
                        fn_DeleteSEPv2 = False
                        MsgBox(CodeResponse & " - " & messageResponse, MsgBoxStyle.Exclamation, Me.Text)
                    End If
                Else
                    fn_DeleteSEPv2 = False
                    MsgBox("Delete SEP Data Gagal/Kosong", MsgBoxStyle.Exclamation, Me.Text)
                End If
            Else
                fn_DeleteSEPv2 = False
                MsgBox("Koneksi Tidak ditemukan", MsgBoxStyle.Exclamation, Me.Text)
            End If
        Catch oErr As Exception
            fn_DeleteSEPv2 = False
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Function
    Private Sub grv_RowStyle(sender As System.Object, e As DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs)
        If grv.IsFilterRow(e.RowHandle) Then Exit Sub
        If grv.GetRowCellValue(e.RowHandle, "STATUSDAFTAR") = "BATAL" Then
            e.Appearance.BackColor = Color.LightYellow
        End If
    End Sub
    Private Sub grv_FocusedRowChanged(ByVal sender As System.Object, ByVal e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs)
        If grv.GetFocusedRowCellValue("KDPENDAFTARAN") Is Nothing Then
            fn_LoadSecurity()
            Exit Sub
        End If
    End Sub
#End Region
#Region "Command Button"
    Private Sub MenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If grv.GetFocusedRowCellValue("KDPENDAFTARAN") Is Nothing Then Exit Sub
        mnuStripCetak.Show(picPrint.Location.X, picPrint.Location.Y + 125)
    End Sub
    Private Sub frmMember_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        Select Case e.KeyCode
            Case Keys.A
                If e.Alt = True And picAdd.Enabled = True Then
                    picAdd_Click()
                End If
            Case Keys.B
                If e.Alt = True And picBatal.Enabled = True Then
                    picBatal_Click()
                End If
            Case Keys.E
                If e.Alt = True And picUpdate.Enabled = True Then
                    picUpdate_Click()
                End If
            Case Keys.D
                If e.Alt = True And picDelete.Enabled = True Then
                    picDelete_Click()
                End If
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
    Private Sub grv_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles grv.DoubleClick
        If grv.GetFocusedRowCellValue("KDPENDAFTARAN") Is Nothing Then
            Exit Sub
        End If

        Dim frmPendaftaran As New frmPendaftaran
        Try
            frmPendaftaran.LoadMe(FORM_MODE.FORM_MODE_VIEW, grv.GetFocusedRowCellValue("KDPENDAFTARAN"))
            frmPendaftaran.ShowDialog(Me)
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub picAdd_Click() Handles picAdd.Click
        Dim frmPendaftaran As New frmPendaftaran
        Try
            frmPendaftaran.LoadMe(FORM_MODE.FORM_MODE_ADD)
            frmPendaftaran.ShowDialog(Me)

            If sStatusSave <> "NEW" Then
                fn_LoadSecurity()
            End If

        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        Finally
            If Not frmPendaftaran Is Nothing Then frmPendaftaran.Dispose()
            frmPendaftaran = Nothing

            Dim rowHandle As Integer = grv.LocateByValue(rowHandle, grv.Columns("KDPENDAFTARAN"), sCode)
            If rowHandle > 0 Then grv.FocusedRowHandle = rowHandle

            If sStatusSave = "NEW" Then
                sStatusSave = "NONE"
                picAdd_Click()
            End If
        End Try
    End Sub
    Private Sub picUpdate_Click() Handles picUpdate.Click
        If grv.GetFocusedRowCellValue("KDPENDAFTARAN") Is Nothing Then
            Exit Sub
        End If
        Dim dsPendaftaran = oPendaftaran.GetData(grv.GetFocusedRowCellValue("KDPENDAFTARAN"))

        If dsPendaftaran.STATUSDAFTAR <> 0 Then
            MsgBox("Status Sudah Selesai", MsgBoxStyle.Information, Me.Text)
            Exit Sub
        End If

        Dim frmPendaftaran As New frmPendaftaran
        Try
            frmPendaftaran.LoadMe(FORM_MODE.FORM_MODE_EDIT, grv.GetFocusedRowCellValue("KDPENDAFTARAN"))
            frmPendaftaran.ShowDialog(Me)
            fn_LoadSecurity()
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        Finally
            If Not frmPendaftaran Is Nothing Then frmPendaftaran.Dispose()
            frmPendaftaran = Nothing

            Dim rowHandle As Integer = grv.LocateByValue(rowHandle, grv.Columns("KDPENDAFTARAN"), sCode)
            If rowHandle > 0 Then grv.FocusedRowHandle = rowHandle

            If sStatusSave = "NEW" Then
                sStatusSave = "NONE"
                picAdd_Click()
            End If
        End Try
    End Sub
    Private Sub picDelete_Click() Handles picDelete.Click
        If grv.GetFocusedRowCellValue("KDPENDAFTARAN") Is Nothing Then
            Exit Sub
        End If

        Dim dsPendaftaran = oPendaftaran.GetData(grv.GetFocusedRowCellValue("KDPENDAFTARAN"))

        If dsPendaftaran.STATUSDAFTAR <> 0 Then
            MsgBox("Status Sudah Selesai", MsgBoxStyle.Information, Me.Text)
            Exit Sub
        End If

        Dim KDPENDAFTARAN As String = grv.GetFocusedRowCellValue("KDPENDAFTARAN")

        If grv.GetFocusedRowCellValue("NaikRanap") = True Then
            MsgBox("Naik Ranap", MsgBoxStyle.Information, Me.Text)
            Exit Sub
        End If
        If MsgBox(Statement.DeleteQuestion, MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, Me.Text) = MsgBoxResult.No Then Exit Sub

        Dim frmPesanDelete As New frmPesanDelete
        frmPesanDelete.ShowDialog(Me)

        If sDeletePesan <> "" Then
            Dim oDelete As New Setting.clsDelete

            oDelete.InsertData("PENDAFTARAN", "DELETE", grv.GetFocusedRowCellValue("NoSEP") & "Tanggal " & Now & " Oleh " & sUserID & " Alasan " & sDeletePesan, KDPENDAFTARAN)

            If fn_CariSEP() = "KOSONG" Then
                oPendaftaran.DeleteData(grv.GetFocusedRowCellValue("KDPENDAFTARAN"))
                fn_LoadSecurity()
            Else
                If sAktiveVersi2 = False Then
                    If fn_DeleteSEP() = True Then
                        oPendaftaran.DeleteData(grv.GetFocusedRowCellValue("KDPENDAFTARAN"))
                        fn_LoadSecurity()
                    End If
                Else
                    If fn_DeleteSEPv2() = True Then
                        oPendaftaran.DeleteData(grv.GetFocusedRowCellValue("KDPENDAFTARAN"))
                        fn_LoadSecurity()
                    End If
                End If
            End If

            MsgBox(Statement.DeleteSuccess, MsgBoxStyle.Information, Me.Text)

        End If
    End Sub
    Private Sub picPrint_Click() Handles picPrint.Click
        Try
            If grv.GetFocusedRowCellValue("KDPENDAFTARAN") = String.Empty Then Exit Sub
            Dim ds = oPendaftaran.GetData(grv.GetFocusedRowCellValue("KDPENDAFTARAN"))

            sCetakSEP = False

            If fn_CariSEP() = "ADA" Then
                Dim rpt As New xtraSEP
                rpt.bindingSource.DataSource = ds
                Dim printTool As New DevExpress.XtraReports.UI.ReportPrintTool(rpt)
                printTool.ShowPreviewDialog(DevExpress.LookAndFeel.UserLookAndFeel.Default)
            Else
                Dim rpt As New xtraUmum
                rpt.bindingSource.DataSource = ds
                Dim printTool As New DevExpress.XtraReports.UI.ReportPrintTool(rpt)
                printTool.ShowPreviewDialog(DevExpress.LookAndFeel.UserLookAndFeel.Default)
            End If

            If sCetakSEP = True Then
                oPendaftaran.UpdateCetak(ds.KDPENDAFTARAN)
            End If
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub picBatal_Click() Handles picBatal.Click
        Try
            If grv.GetFocusedRowCellValue("KDPENDAFTARAN") = String.Empty Then Exit Sub
            Dim dsPendaftaran = oPendaftaran.GetData(grv.GetFocusedRowCellValue("KDPENDAFTARAN"))

            If dsPendaftaran.STATUSDAFTAR = 2 Then
                MsgBox("Status Sudah Selesai", MsgBoxStyle.Information, Me.Text)
                Exit Sub
            End If

            If oPendaftaran.UpdateDataBatal(grv.GetFocusedRowCellValue("KDPENDAFTARAN")) = True Then
                MsgBox("Status Daftar diperbaharui", MsgBoxStyle.Information, Me.Text)
            End If

            fn_LoadSecurity()

        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub picRefresh_Click() Handles picRefresh.Click
        fn_LoadSecurity()
    End Sub
    Private Sub RegistrasiToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles RegistrasiToolStripMenuItem.Click
        Try
            If grv.GetFocusedRowCellValue("KDPENDAFTARAN") = String.Empty Then Exit Sub
            Dim ds = oPendaftaran.GetData(grv.GetFocusedRowCellValue("KDPENDAFTARAN"))

            sCetakSEP = False

            If ds.NOMORSEP = "" Then
                Dim rpt As New xtraUmum
                rpt.bindingSource.DataSource = ds
                Dim printTool As New DevExpress.XtraReports.UI.ReportPrintTool(rpt)
                printTool.ShowPreviewDialog(DevExpress.LookAndFeel.UserLookAndFeel.Default)
            Else
                If fn_CariSEP() = "KOSONG" Then
                    Dim rpt As New xtraUmum
                    rpt.bindingSource.DataSource = ds
                    Dim printTool As New DevExpress.XtraReports.UI.ReportPrintTool(rpt)
                    printTool.ShowPreviewDialog(DevExpress.LookAndFeel.UserLookAndFeel.Default)
                Else
                    Dim rpt As New xtraSEP
                    rpt.bindingSource.DataSource = ds
                    Dim printTool As New DevExpress.XtraReports.UI.ReportPrintTool(rpt)
                    printTool.ShowPreviewDialog(DevExpress.LookAndFeel.UserLookAndFeel.Default)
                End If
            End If

            If sCetakSEP = True Then
                oPendaftaran.UpdateCetak(ds.KDPENDAFTARAN)
            End If
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Function HitungUmur(ByVal tanggllahir As Date, ByVal tanggaldatang As Date) As String
        Dim y, m, d As Integer
        d = tanggaldatang.Day - tanggllahir.Day
        m = tanggaldatang.Month - tanggllahir.Month
        y = tanggaldatang.Year - tanggllahir.Year
        If Math.Sign(d) = -1 Then
            d = 30 - Math.Abs(m)
            m -= 1
        End If
        If Math.Sign(m) = -1 Then
            m = 12 - Math.Abs(m)
            y -= 1
        End If

        Return y & " Tahun, " & m & " bulan, " & d & " hari"

    End Function
#End Region
End Class