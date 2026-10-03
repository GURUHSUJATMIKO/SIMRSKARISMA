Imports DataAccess
Imports UI.WIN.MAIN.My.Resources
Imports System.Linq
Imports Newtonsoft.Json.Linq
Imports System.Data.SqlClient

Public Class frmListTemplate

#Region "Function"
    Private sKategori As Integer = 0
    Private sRekamMedis As String = String.Empty

    Public Sub fn_LoadKategori(ByVal Parameter As Integer, ByVal RekamMedis As String)
        sKategori = Parameter
        sRekamMedis = RekamMedis
    End Sub
    Private Sub fn_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'fn_LoadSecurity()
        sKODEASESMENCOPY = ""

        fn_Preview()
        If sKategori = 2 Then
            Me.Text = "CPPT"
        End If
    End Sub
    Private Sub fn_Preview()
        Try
            grv.Columns.Clear()
            grd.DataSource = Nothing
            grv.OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleIfExpanded

            If sKategori = 0 Then
                fn_Load01()
            ElseIf sKategori = 1 Then
                fn_Load02()
            ElseIf sKategori = 2 Then
                fn_Load03()
            ElseIf sKategori = 3 Then
                fn_Load04()
            ElseIf sKategori = 4 Then
                fn_Load05()
            ElseIf sKategori = 5 Then
                fn_Load06()
            End If

        Catch oErr As Exception
            MsgBox("Preview Data : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_Load01()
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
            SQL &= "A.KDCPPT_TEMPLATE "
            SQL &= ",A.NAMATEMPLATE "
            SQL &= "FROM S_REQ_CPPT_TEMPLATE A "
            SQL &= "WHERE A.KDUSER = '" & sUserID & "' "

            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "TEMPLATE")

            grd.MainView = grv
            grd.DataSource = ds.Tables("TEMPLATE")
            grd.ForceInitialize()

            fn_LoadFormatData()
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_Load02()
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
            SQL &= "KDCPPT_TEMPLATE = A.KDTEMPLATE_RINGKASANKELUAR "
            SQL &= ",NAMATEMPLATE = A.NAMAPASIEN "
            SQL &= "FROM S_TEMPLATE_RINGKASANKELUARRAWATINAP A "
            SQL &= "WHERE A.KDUSER = '" & sUserID & "' "

            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "TEMPLATE")

            grd.MainView = grv
            grd.DataSource = ds.Tables("TEMPLATE")
            grd.ForceInitialize()

            fn_LoadFormatData()
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_Load03()
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
            SQL &= "KDCPPT_TEMPLATE = A.KDCPPT "
            SQL &= ",TANGGAL = A.DATE "
            SQL &= ",A.PROFESI "
            'SQL &= ",KET = A.SUKU "
            SQL &= ",[USER] = A.KDUSER "
            SQL &= "FROM S_REQ_CPPT A "
            SQL &= "WHERE A.KDCUSTOMER = '" & sRekamMedis & "' "
            SQL &= "AND A.KDPENDAFTARAN LIKE '%RI%' "
            SQL &= "ORDER BY A.DATE DESC "

            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "TEMPLATE")

            grd.MainView = grv
            grd.DataSource = ds.Tables("TEMPLATE")
            grd.ForceInitialize()

            fn_LoadFormatData()
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_Load04()
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
            SQL &= "KDCPPT_TEMPLATE = A.KDJUDUL "
            SQL &= ",NAMATEMPLATE = A.KDJUDUL "
            SQL &= "FROM S_DIGITAL_RI_13_TEMPLATE A "
            SQL &= "WHERE A.KDUSER = '" & sUserID & "' "
            SQL &= "ORDER BY A.KDJUDUL "

            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "TEMPLATE")

            grd.MainView = grv
            grd.DataSource = ds.Tables("TEMPLATE")
            grd.ForceInitialize()

            fn_LoadFormatData()
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_Load05()
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
            SQL &= "KDCPPT_TEMPLATE = A.KDJUDUL "
            SQL &= ",NAMATEMPLATE = A.KDJUDUL "
            SQL &= "FROM S_DIGITAL_OK_LAPORANOPERASI_TEMPLATE A "
            SQL &= "WHERE A.KDUSER = '" & sUserID & "' "
            SQL &= "ORDER BY A.KDJUDUL "

            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "TEMPLATE")

            grd.MainView = grv
            grd.DataSource = ds.Tables("TEMPLATE")
            grd.ForceInitialize()

            fn_LoadFormatData()
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_Load06()
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
            SQL &= "KDCPPT_TEMPLATE = A.KDJUDUL "
            SQL &= ",NAMATEMPLATE = A.KDJUDUL "
            SQL &= "FROM S_DIGITAL_OK_LAPORANTINDAKAN_TEMPLATE A "
            SQL &= "WHERE A.KDUSER = '" & sUserID & "' "
            SQL &= "ORDER BY A.KDJUDUL "

            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "TEMPLATE")

            grd.MainView = grv
            grd.DataSource = ds.Tables("TEMPLATE")
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

        grv.Columns("KDCPPT_TEMPLATE").VisibleIndex = -1
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
        If grv.GetFocusedRowCellValue("KDCPPT_TEMPLATE") Is Nothing Then
            MsgBox("Silahkan Pilih Template", MsgBoxStyle.Exclamation, Me.Text)
            Exit Sub
        End If

        If sKategori = 0 Then
            sKODETEMPLATE = grv.GetFocusedRowCellValue("KDCPPT_TEMPLATE")
        ElseIf sKategori = 1 Then
            sKODETEMPLATE_RINGKASAN = grv.GetFocusedRowCellValue("KDCPPT_TEMPLATE")
        ElseIf sKategori = 2 Then
            sKODECPPTCOPY = grv.GetFocusedRowCellValue("KDCPPT_TEMPLATE")
        Else
            sKODEASESMENCOPY = grv.GetFocusedRowCellValue("KDCPPT_TEMPLATE")
        End If

        Me.Close()
    End Sub
    Private Sub grv_DoubleClick(sender As Object, e As EventArgs) Handles grv.DoubleClick
        If grv.GetFocusedRowCellValue("KDCPPT_TEMPLATE") Is Nothing Then
            MsgBox("Silahkan Pilih Template", MsgBoxStyle.Exclamation, Me.Text)
            Exit Sub
        End If

        If sKategori = 0 Then
            sKODETEMPLATE = grv.GetFocusedRowCellValue("KDCPPT_TEMPLATE")
        ElseIf sKategori = 1 Then
            sKODETEMPLATE_RINGKASAN = grv.GetFocusedRowCellValue("KDCPPT_TEMPLATE")
        ElseIf sKategori = 2 Then
            sKODECPPTCOPY = grv.GetFocusedRowCellValue("KDCPPT_TEMPLATE")
        Else
            sKODEASESMENCOPY = grv.GetFocusedRowCellValue("KDCPPT_TEMPLATE")
        End If

        Me.Close()
    End Sub
    Private Sub DeleteToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DeleteToolStripMenuItem.Click
        If sKategori = 1 Then
            If grv.GetFocusedRowCellValue("KDCPPT_TEMPLATE") Is Nothing Then
                MsgBox("Silahkan Pilih Template", MsgBoxStyle.Exclamation, Me.Text)
                Exit Sub
            End If

            If MsgBox("Delete " & grv.GetFocusedRowCellValue("KDCPPT_TEMPLATE") & "?", MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, Me.Text) = MsgBoxResult.No Then Exit Sub
            If fn_DeleteData(grv.GetFocusedRowCellValue("KDCPPT_TEMPLATE")) = False Then
                MsgBox("Hapus gagal! tolong kontak admin anda!", MsgBoxStyle.Exclamation, Me.Text)
                Exit Sub
            End If
            MsgBox("Delete " & grv.GetFocusedRowCellValue("KDCPPT_TEMPLATE") & " success!", MsgBoxStyle.Information, Me.Text)
            fn_Preview()
        ElseIf sKategori = 4 Then
            If grv.GetFocusedRowCellValue("KDCPPT_TEMPLATE") Is Nothing Then
                MsgBox("Silahkan Pilih Template", MsgBoxStyle.Exclamation, Me.Text)
                Exit Sub
            End If

            Dim oLaporanOperasiTemplate As New Transaksi.clsS_DIGITAL_OK_LAPORANOPERASITemplate

            If MsgBox("Delete " & grv.GetFocusedRowCellValue("KDCPPT_TEMPLATE") & "?", MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, Me.Text) = MsgBoxResult.No Then Exit Sub

            If oLaporanOperasiTemplate.DeleteData(grv.GetFocusedRowCellValue("KDCPPT_TEMPLATE")) = True Then
                MsgBox("Delete " & grv.GetFocusedRowCellValue("KDCPPT_TEMPLATE") & " success!", MsgBoxStyle.Information, Me.Text)
                fn_Preview()
            End If

        Else
            MsgBox("Belum Tersedia", MsgBoxStyle.Exclamation, Me.Text)
        End If
    End Sub
    Private Function fn_DeleteData(ByVal sKDCPPT_TEMPLATE As String) As Boolean
        Try
            Dim oCPPTTemplate As New Inventory.clsCPPTTemplate
            oCPPTTemplate.DeleteData(sKDCPPT_TEMPLATE)
            fn_DeleteData = True
        Catch oErr As Exception
            fn_DeleteData = False
            MsgBox("Hapus Data : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Function
#End Region
End Class