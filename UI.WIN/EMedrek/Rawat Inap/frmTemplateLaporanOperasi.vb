Imports DataAccess
Imports System.Data.SqlClient
Imports UI.WIN.MAIN.My.Resources
Imports System.Linq
Imports System.IO

Public Class frmTemplateLaporanOperasi
#Region "Declaration"
        Private oFormMode As FORM_MODE = FORM_MODE.FORM_MODE_VIEW
        Private sNoId As Integer
        Private isLoad As Boolean = False
        Private oTemplateLaporanOperasi As New Master.clsTemplateLaporanOperasi
#End Region
#Region "Function"
    Public Sub LoadMe(ByVal FormMode As Integer, Optional ByVal NoId As Integer = 0)
        oFormMode = FormMode
        sNoId = NoId

    End Sub
    Private Sub Form_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        fn_ChangeFormState()
        fn_LoadLanguage()
        isLoad = True
    End Sub
    Public Sub fn_LoadLanguage()
        Try
            Me.Text = "Template Laporan Operasi"

            btnSaveNew.Caption = Caption.FormSaveNew
            btnSaveClose.Caption = Caption.FormSaveClose
            btnClose.Caption = Caption.FormClose
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub Form_Closing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        Dispose()
        sCode = txtJUDUL.Text.Trim.ToUpper
    End Sub
    Private Overloads Sub Dispose()
        MyBase.Dispose()
        Me.Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
    Private Sub fn_ChangeFormState()
        fn_JENISOPERASI()
        'fn_LoadOperator()
        'fn_LoadAsisten()
        'fn_LoadOperator2()
        'fn_DIAGNOSA()
        'fn_PROSEDUR()

        Select Case oFormMode
            Case FORM_MODE.FORM_MODE_VIEW
                fn_ViewMode(True)
                fn_LoadData()
            Case FORM_MODE.FORM_MODE_ADD
                fn_ViewMode(False)
                fn_EmptyMe()
            Case FORM_MODE.FORM_MODE_EDIT
                fn_ViewMode(False)
                fn_LoadData()
            Case Else
                fn_ViewMode(True)
        End Select
    End Sub
    Private Sub fn_ViewMode(ByVal Status As Boolean)
        btnSaveNew.Enabled = Not Status
        btnSaveClose.Enabled = Not Status

        'txtPERAWAT_3.Properties.ReadOnly = False
        TextEdit1.Properties.ReadOnly = False
        TextEdit2.Properties.ReadOnly = False
        TextEdit3.Properties.ReadOnly = False
        TextEdit4.Properties.ReadOnly = False
        TextEdit5.Properties.ReadOnly = False
        TextEdit6.Properties.ReadOnly = False
        TextEdit7.Properties.ReadOnly = False
        TextEdit8.Properties.ReadOnly = False
        TextEdit9.Properties.ReadOnly = False
        TextEdit10.Properties.ReadOnly = False
        TextEdit11.Properties.ReadOnly = False
        TextEdit12.Properties.ReadOnly = False
        TextEdit13.Properties.ReadOnly = False
        TextEdit14.Properties.ReadOnly = False
        TextEdit15.Properties.ReadOnly = False
        TextEdit16.Properties.ReadOnly = False
        TextEdit17.Properties.ReadOnly = False
        TextEdit18.Properties.ReadOnly = False
        TextEdit19.Properties.ReadOnly = False
        MemoEdit4.Properties.ReadOnly = False
        MemoEdit5.Properties.ReadOnly = False
        MemoEdit6.Properties.ReadOnly = False
        CheckEdit1.Properties.ReadOnly = False
        CheckEdit2.Properties.ReadOnly = False
        CheckEdit3.Properties.ReadOnly = False
        CheckEdit4.Properties.ReadOnly = False
        CheckEdit5.Properties.ReadOnly = False
        CheckEdit6.Properties.ReadOnly = False
        CheckEdit7.Properties.ReadOnly = False
        CheckEdit8.Properties.ReadOnly = False
        CheckEdit9.Properties.ReadOnly = False
        CheckEdit10.Properties.ReadOnly = False
        CheckEdit11.Properties.ReadOnly = False
        CheckEdit12.Properties.ReadOnly = False
        CheckEdit13.Properties.ReadOnly = False
        CheckEdit14.Properties.ReadOnly = False
        CheckEdit15.Properties.ReadOnly = False
        CheckEdit16.Properties.ReadOnly = False
        CheckEdit17.Properties.ReadOnly = False
        CheckEdit18.Properties.ReadOnly = False
        CheckEdit19.Properties.ReadOnly = False
        CheckEdit20.Properties.ReadOnly = False

        txtJUDUL.Properties.ReadOnly = False
        chkISACTIVE.Properties.ReadOnly = False

        'cboJenisOperasi

    End Sub
    Private Sub fn_EmptyMe()
        'txtCODE.Text = "<--- AUTO --->"

        'chkDIAGNOSA_1.Checked = False

        TextEdit3.ResetText()
        TextEdit4.ResetText()
        TextEdit5.ResetText()

        TextEdit8.ResetText()
        TextEdit9.ResetText()
        TextEdit10.ResetText()
        TextEdit11.ResetText()
        TextEdit12.ResetText()
        TextEdit13.Text = Now.ToString("HH:mm")
        TextEdit14.Text = Now.ToString("HH:mm")
        TextEdit15.Text = Now.ToString("HH:mm")
        TextEdit16.Text = "00:00"
        TextEdit17.ResetText()
        TextEdit18.ResetText()
        TextEdit19.ResetText()
        MemoEdit4.ResetText()
        MemoEdit5.ResetText()
        MemoEdit6.ResetText()
        CheckEdit1.Checked = False
        CheckEdit2.Checked = False
        CheckEdit3.Checked = False
        CheckEdit4.Checked = False
        CheckEdit5.Checked = False
        CheckEdit6.Checked = False
        CheckEdit7.Checked = False
        CheckEdit8.Checked = False
        CheckEdit9.Checked = False
        CheckEdit10.Checked = False
        CheckEdit11.Checked = False
        CheckEdit12.Checked = False
        CheckEdit13.Checked = False
        CheckEdit14.Checked = False
        CheckEdit15.Checked = False
        CheckEdit16.Checked = False
        CheckEdit17.Checked = False
        CheckEdit18.Checked = False
        CheckEdit19.Checked = False
        CheckEdit20.Checked = False

        txtJUDUL.ResetText()
        chkISACTIVE.Checked = False

        'cboJenisOperasi.SelectedIndex = 0
        'strDiagnosa = ""
        'strDiagnosa2 = ""
        'strProsedur = ""
    End Sub
    Private Sub fn_LoadData()
        Try
            ' ***** HEADER *****
            Dim ds = oTemplateLaporanOperasi.GetData(sNoId)
            With ds
                txtJUDUL.Text = .JUDUL
                TextEdit1.EditValue = .TextEdit1
                TextEdit2.EditValue = .TextEdit2
                TextEdit3.Text = .TextEdit3
                TextEdit4.Text = .TextEdit4
                TextEdit5.Text = .TextEdit5
                TextEdit6.EditValue = .TextEdit6
                TextEdit7.EditValue = .TextEdit7
                TextEdit8.Text = .TextEdit8
                TextEdit9.Text = .TextEdit9
                TextEdit10.Text = .TextEdit10
                TextEdit11.Text = .TextEdit11
                TextEdit12.Text = .TextEdit12
                'TextEdit13.EditValue = CDate(.TextEdit13)
                'TextEdit14.EditValue = CDate(.TextEdit14)
                'TextEdit15.EditValue = CDate(.TextEdit15)
                'TextEdit16.EditValue = CDate(.TextEdit16)
                TextEdit13.Text = .TextEdit13
                TextEdit14.Text = .TextEdit14
                TextEdit15.Text = .TextEdit15
                TextEdit16.Text = .TextEdit16
                TextEdit17.Text = .TextEdit17
                TextEdit18.Text = .TextEdit18
                TextEdit19.EditValue = .TextEdit19
                MemoEdit4.Text = .MemoEdit4
                MemoEdit5.Text = .MemoEdit5
                MemoEdit6.Text = .MemoEdit6
                CheckEdit1.Checked = .CheckEdit1
                CheckEdit2.Checked = .CheckEdit2
                CheckEdit3.Checked = .CheckEdit3
                CheckEdit4.Checked = .CheckEdit4
                CheckEdit5.Checked = .CheckEdit5
                CheckEdit6.Checked = .CheckEdit6
                CheckEdit7.Checked = .CheckEdit7
                CheckEdit8.Checked = .CheckEdit8
                CheckEdit9.Checked = .CheckEdit9
                CheckEdit10.Checked = .CheckEdit10
                CheckEdit11.Checked = .CheckEdit11
                CheckEdit12.Checked = .CheckEdit12
                CheckEdit13.Checked = .CheckEdit13
                CheckEdit14.Checked = .CheckEdit14
                CheckEdit15.Checked = .CheckEdit15
                CheckEdit16.Checked = .CheckEdit16
                CheckEdit17.Checked = .CheckEdit17
                CheckEdit18.Checked = .CheckEdit18
                CheckEdit19.Checked = .CheckEdit19
                CheckEdit20.Checked = .CheckEdit20
                cboJenisOperasi.EditValue = .JENIS_OPERASI

                Try
                    txtDiagnosaPreOP.Text = .MemoEdit1 + vbCrLf + .MemoEdit7 + vbCrLf + .MemoEdit8
                    If String.IsNullOrWhiteSpace(txtDiagnosaPreOP.Text) Then
                        txtDiagnosaPreOP.ResetText()
                    End If
                Catch ex As Exception
                    txtDiagnosaPreOP.Text = .MemoEdit1
                End Try

                Try
                    txtDiagnosaPostOP.Text = .MemoEdit2 + vbCrLf + .TXTDIAGNOSAPOSTOP2 + vbCrLf + .TXTDIAGNOSAPOSTOP3
                    If String.IsNullOrWhiteSpace(txtDiagnosaPostOP.Text) Then
                        txtDiagnosaPostOP.ResetText()
                    End If
                Catch ex As Exception
                    txtDiagnosaPostOP.Text = .MemoEdit2
                End Try

                Try
                    txtProsedur.Text = .MemoEdit3 + vbCrLf + .PROSEDUR2 + vbCrLf + .PROSEDUR3
                    If String.IsNullOrWhiteSpace(txtProsedur.Text) Then
                        txtProsedur.ResetText()
                    End If
                Catch ex As Exception
                    txtProsedur.Text = .MemoEdit3
                End Try

                chkISACTIVE.Checked = .ISACTIVE

                txtDokter.EditValue = .DOKTER_KODE
            End With
        Catch oErr As Exception
            MsgBox("Load List Data : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Function fn_Validate() As Boolean
        Try
            fn_Validate = True
            If txtJUDUL.Text = String.Empty Then
                txtJUDUL.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                txtJUDUL.ErrorText = Statement.ErrorRequired

                txtJUDUL.Focus()
                fn_Validate = False
                Exit Function
            End If

        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Function
    Private Function fn_Save() As Boolean
        Try
            Dim ds = oTemplateLaporanOperasi.GetStructureHeader
            With ds
                .KDTLO = sNoId
                .JUDUL = txtJUDUL.Text
                .JENIS_OPERASI = IIf(String.IsNullOrEmpty(cboJenisOperasi.EditValue), "",cboJenisOperasi.EditValue)
                .TextEdit1 = TextEdit1.Text
                .TextEdit2 = TextEdit2.Text
                .TextEdit3 = TextEdit3.Text
                .TextEdit4 = TextEdit4.Text
                .TextEdit5 = TextEdit5.Text
                .TextEdit6 = TextEdit6.Text
                .TextEdit7 = TextEdit7.Text
                .TextEdit8 = TextEdit8.Text
                .TextEdit9 = TextEdit9.Text
                .TextEdit10 = TextEdit10.Text
                .TextEdit11 = TextEdit11.Text
                .TextEdit12 = TextEdit12.Text
                '.TextEdit13 = DateTime.Parse(TextEdit13.EditValue).ToString("dd/MM/yyyy HH:mm:ss")
                '.TextEdit14 = DateTime.Parse(TextEdit14.EditValue).ToString("dd/MM/yyyy HH:mm:ss")
                '.TextEdit15 = DateTime.Parse(TextEdit15.EditValue).ToString("dd/MM/yyyy HH:mm:ss")
                '.TextEdit16 = DateTime.Parse(TextEdit16.EditValue).ToString("dd/MM/yyyy HH:mm:ss")
                .TextEdit13 = TextEdit13.Text
                .TextEdit14 = TextEdit14.Text
                .TextEdit15 = TextEdit15.Text
                .TextEdit16 = TextEdit16.Text
                .TextEdit17 = TextEdit17.Text
                .TextEdit18 = TextEdit18.Text
                .TextEdit19 = IIf(String.IsNullOrEmpty(TextEdit19.EditValue), "",TextEdit19.EditValue)

                .MemoEdit1 = txtDiagnosaPreOP.Text
                .MemoEdit2 = txtDiagnosaPostOP.Text
                .MemoEdit3 = txtProsedur.Text


                'If grdKDDIAGNOSAPREOP2.EditValue Is Nothing Then
                .MemoEdit7 = " "
                'Else
                '    .MemoEdit7 = grdKDDIAGNOSAPREOP2.EditValue
                'End If
                'If grdKDDIAGNOSAPREOP3.EditValue Is Nothing Then
                .MemoEdit8 = " "
                'Else
                '    .MemoEdit8 = grdKDDIAGNOSAPREOP3.EditValue
                'End If



                'If grdKDDIAGNOSAPOSTOP2.EditValue Is Nothing Then
                .TXTDIAGNOSAPOSTOP2 = " "
                'Else
                '    .TXTDIAGNOSAPOSTOP2 = grdKDDIAGNOSAPOSTOP2.EditValue
                'End If
                'If grdKDDIAGNOSAPOSTOP3.EditValue Is Nothing Then
                .TXTDIAGNOSAPOSTOP3 = " "
                'Else
                '    .TXTDIAGNOSAPOSTOP3 = grdKDDIAGNOSAPOSTOP3.EditValue
                'End If


                'If grdKDPROSEDUR2.EditValue Is Nothing Then
                .PROSEDUR2 = " "
                'Else
                '    .PROSEDUR2 = grdKDPROSEDUR2.EditValue
                'End If
                'If grdKDPROSEDUR3.EditValue Is Nothing Then
                .PROSEDUR3 = " "
                'Else
                '    .PROSEDUR3 = grdKDPROSEDUR3.EditValue
                'End If

                .MemoEdit4 = MemoEdit4.Text
                .MemoEdit5 = MemoEdit5.Text
                .MemoEdit6 = MemoEdit6.Text
                .CheckEdit1 = CheckEdit1.Checked
                .CheckEdit2 = CheckEdit2.Checked
                .CheckEdit3 = CheckEdit3.Checked
                .CheckEdit4 = CheckEdit4.Checked
                .CheckEdit5 = CheckEdit5.Checked
                .CheckEdit6 = CheckEdit6.Checked
                .CheckEdit7 = CheckEdit7.Checked
                .CheckEdit8 = CheckEdit8.Checked
                .CheckEdit9 = CheckEdit9.Checked
                .CheckEdit10 = CheckEdit10.Checked
                .CheckEdit11 = CheckEdit11.Checked
                .CheckEdit12 = CheckEdit12.Checked
                .CheckEdit13 = CheckEdit13.Checked
                .CheckEdit14 = CheckEdit14.Checked
                .CheckEdit15 = CheckEdit15.Checked
                .CheckEdit16 = CheckEdit16.Checked
                .CheckEdit17 = CheckEdit17.Checked
                .CheckEdit18 = CheckEdit18.Checked
                .CheckEdit19 = CheckEdit19.Checked
                .CheckEdit20 = CheckEdit20.Checked
                .KDDOCTOR = ""
                .DOKTER_KODE = IIf(String.IsNullOrEmpty(txtDokter.EditValue), "",txtDokter.EditValue)
                .DOKTER_NAMEDISPLAY = txtDokter.Text
                .KDUSER = sUserID
                .ISACTIVE = chkISACTIVE.Checked
            End With

            'oFormMode = 1
            If oFormMode = FORM_MODE.FORM_MODE_ADD Then
                Try
                    fn_Save = oTemplateLaporanOperasi.InsertData(ds)
                Catch ex As Exception
                    MsgBox("Simpan Data : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            ElseIf oFormMode = FORM_MODE.FORM_MODE_EDIT Then
                Try
                    fn_Save = oTemplateLaporanOperasi.UpdateData(ds)
                Catch ex As Exception
                    MsgBox("Simpan Data : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            End If
        Catch oErr As Exception
            MsgBox("Simpan Data : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            fn_Save = False
        End Try
    End Function
#End Region
#Region "Command Button"
    Private Sub frmItem_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        Select Case e.KeyCode
            Case Keys.F12
                btnClose_Click()
            Case Keys.F2
                If btnSaveNew.Enabled = True Then
                    btnSaveNew_Click()
                End If
            Case Keys.F3
                If btnSaveClose.Enabled = True Then
                    btnSaveClose_Click()
                End If
            Case Keys.PageUp
	            fn_ScrollPage(True)
            Case Keys.PageDown
	            fn_ScrollPage(False)
        End Select
    End Sub
    Private Sub btnSaveNew_Click() Handles btnSaveNew.ItemClick
        If fn_Validate() = False Then Exit Sub
        If MsgBox("Save " & txtJUDUL.Text.Trim.ToUpper & "?", MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, Me.Text) = MsgBoxResult.No Then Exit Sub
        If fn_Save() = False Then
            MsgBox("Save gagal! Tolong kontak admin anda!", MsgBoxStyle.Exclamation, Me.Text)
        Else
            MsgBox("Save " & txtJUDUL.Text.Trim.ToUpper & " success!", MsgBoxStyle.Information, Me.Text)
            'sStatusSave = "NEW"
            Me.Close()
        End If
    End Sub
    Private Sub btnSaveClose_Click() Handles btnSaveClose.ItemClick
        If fn_Validate() = False Then Exit Sub
        If MsgBox("Save " & txtJUDUL.Text.Trim.ToUpper & "?", MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, Me.Text) = MsgBoxResult.No Then Exit Sub
        If fn_Save() = False Then
            MsgBox("Save gagal! Tolong kontak admin anda!", MsgBoxStyle.Exclamation, Me.Text)
        Else
            MsgBox("Save " & txtJUDUL.Text.Trim.ToUpper & " success!", MsgBoxStyle.Information, Me.Text)
            Me.Close()
        End If
    End Sub
    Private Sub btnClose_Click() Handles btnClose.ItemClick
        Me.Close()
    End Sub
#End Region
#Region "Lookup / Event"
    Private Sub fn_NOIDUSER()
        'Dim oUser As New Setting.clsUser
        'Try
        '    grdNOIDUSER.Properties.DataSource = oUser.GetData.Where(Function(x) x.ISACTIVE = True).ToList()
        '    grdNOIDUSER.Properties.ValueMember = "NOIDUSER"
        '    grdNOIDUSER.Properties.DisplayMember = "NOIDUSER"
        'Catch oErr As Exception
        '    MsgBox("Load Sub Spesialis Data : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        'End Try
    End Sub

    Private Sub fn_LoadOperator()
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
            SQL &= "* "
            SQL &= "FROM "
            SQL &= "M_UNIT A "
            SQL &= "WHERE "
            SQL &= "A.ISACTIVE = 1 "

            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "DOKTER")

            TextEdit1.Properties.DataSource = ds.Tables("DOKTER")
            TextEdit1.Properties.ValueMember = "NAME_DISPLAY"
            TextEdit1.Properties.DisplayMember = "NAME_DISPLAY"

            TextEdit19.Properties.DataSource = ds.Tables("DOKTER")
            TextEdit19.Properties.ValueMember = "NAME_DISPLAY"
            TextEdit19.Properties.DisplayMember = "NAME_DISPLAY"

            TextEdit2.Properties.DataSource = ds.Tables("DOKTER")
            TextEdit2.Properties.ValueMember = "NAME_DISPLAY"
            TextEdit2.Properties.DisplayMember = "NAME_DISPLAY"

            TextEdit6.Properties.DataSource = ds.Tables("DOKTER")
            TextEdit6.Properties.ValueMember = "NAME_DISPLAY"
            TextEdit6.Properties.DisplayMember = "NAME_DISPLAY"

            TextEdit7.Properties.DataSource = ds.Tables("DOKTER")
            TextEdit7.Properties.ValueMember = "NAME_DISPLAY"
            TextEdit7.Properties.DisplayMember = "NAME_DISPLAY"

            txtDokter.Properties.DataSource = ds.Tables("DOKTER")
            txtDokter.Properties.ValueMember = "KDSTAFF"
            txtDokter.Properties.DisplayMember = "NAME_DISPLAY"


            If oConn.State = ConnectionState.Open Then
                oConn.Close()
            End If
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try

        'Try
        '    Dim oConn As New SqlConnection
        '    Dim oComm As New SqlCommand
        '    Dim da As SqlDataAdapter
        '    Dim ds As New DataSet
        '    Dim SQL As String
        '    Dim sConn As String = sKoneksi
        '    oConn = New SqlConnection(sConn)

        '    If oConn.State = ConnectionState.Closed Then
        '        oConn.Open()
        '    End If

        '    SQL = "SELECT "
        '    SQL &= "* "
        '    SQL &= "FROM "
        '    SQL &= "M_DOCTOR A "
        '    SQL &= "WHERE "
        '    SQL &= "A.ISACTIVE = 1 "
        '    'SQL &= "And A.KDDOCTORSUB = 8 "

        '    oComm.Connection = oConn
        '    oComm.CommandText = SQL
        '    oComm.CommandTimeout = 120
        '    oComm.CommandType = CommandType.Text

        '    da = New SqlDataAdapter(oComm)
        '    da.Fill(ds, "DOCTOR")

        '    TextEdit1.Properties.DataSource = ds.Tables("DOCTOR")
        '    TextEdit1.Properties.ValueMember = "NAME_DISPLAY"
        '    TextEdit1.Properties.DisplayMember = "NAME_DISPLAY"

        '    TextEdit19.Properties.DataSource = ds.Tables("DOCTOR")
        '    TextEdit19.Properties.ValueMember = "NAME_DISPLAY"
        '    TextEdit19.Properties.DisplayMember = "NAME_DISPLAY"

        '    If oConn.State = ConnectionState.Open Then
        '        oConn.Close()
        '    End If
        'Catch oErr As Exception
        '    MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        'End Try
    End Sub

    Private Sub fn_LoadOperator2()
        'Try
        '    Dim oConn As New SqlConnection
        '    Dim oComm As New SqlCommand
        '    Dim da As SqlDataAdapter
        '    Dim ds As New DataSet
        '    Dim SQL As String
        '    Dim sConn As String = sKoneksi
        '    oConn = New SqlConnection(sConn)

        '    If oConn.State = ConnectionState.Closed Then
        '        oConn.Open()
        '    End If

        '    SQL = "SELECT "
        '    SQL &= "* "
        '    SQL &= "FROM "
        '    SQL &= "M_DOCTOR A "
        '    SQL &= "WHERE "
        '    SQL &= "A.ISACTIVE = 1 "
        '    'SQL &= "And A.KDDOCTORSUB = 16 "

        '    oComm.Connection = oConn
        '    oComm.CommandText = SQL
        '    oComm.CommandTimeout = 120
        '    oComm.CommandType = CommandType.Text

        '    da = New SqlDataAdapter(oComm)
        '    da.Fill(ds, "DOCTOR")

        '    TextEdit6.Properties.DataSource = ds.Tables("DOCTOR")
        '    TextEdit6.Properties.ValueMember = "NAME_DISPLAY"
        '    TextEdit6.Properties.DisplayMember = "NAME_DISPLAY"

        '    If oConn.State = ConnectionState.Open Then
        '        oConn.Close()
        '    End If
        'Catch oErr As Exception
        '    MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        'End Try
    End Sub

    Private Sub fn_LoadAsisten()
        'Try
        '    Dim oConn As New SqlConnection
        '    Dim oComm As New SqlCommand
        '    Dim da As SqlDataAdapter
        '    Dim ds As New DataSet
        '    Dim SQL As String
        '    Dim sConn As String = sKoneksi
        '    oConn = New SqlConnection(sConn)

        '    If oConn.State = ConnectionState.Closed Then
        '        oConn.Open()
        '    End If

        '    SQL = "SELECT "
        '    SQL &= "* "
        '    SQL &= "FROM "
        '    SQL &= "M_DOCTOR A "
        '    SQL &= "WHERE "
        '    SQL &= "A.ISACTIVE = 1 "

        '    oComm.Connection = oConn
        '    oComm.CommandText = SQL
        '    oComm.CommandTimeout = 120
        '    oComm.CommandType = CommandType.Text

        '    da = New SqlDataAdapter(oComm)
        '    da.Fill(ds, "DOCTOR")

        '    TextEdit2.Properties.DataSource = ds.Tables("DOCTOR")
        '    TextEdit2.Properties.ValueMember = "NAME_DISPLAY"
        '    TextEdit2.Properties.DisplayMember = "NAME_DISPLAY"

        '    TextEdit7.Properties.DataSource = ds.Tables("DOCTOR")
        '    TextEdit7.Properties.ValueMember = "NAME_DISPLAY"
        '    TextEdit7.Properties.DisplayMember = "NAME_DISPLAY"

        '    If oConn.State = ConnectionState.Open Then
        '        oConn.Close()
        '    End If
        'Catch oErr As Exception
        '    MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        'End Try
    End Sub

    'Private Sub TextEdit15_EditValueChanged(sender As Object, e As EventArgs) Handles TextEdit15.EditValueChanged
    '    TextEdit16.Text = (TextEdit15.EditValue - TextEdit14.EditValue).ToString
    'End Sub

    Private Sub fn_DIAGNOSA()
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
            SQL &= "* "
            SQL &= "FROM "
            SQL &= "M_DIAGNOSA A "
            SQL &= "WHERE "
            SQL &= "A.ISACTIVE = 1 "

            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "DIAGNOSA")

            grdKDDIAGNOSAPREOP1.Properties.DataSource = ds.Tables("DIAGNOSA")
            grdKDDIAGNOSAPREOP1.Properties.ValueMember = "MEMO"   'KDDIAGNOSA
            grdKDDIAGNOSAPREOP1.Properties.DisplayMember = "MEMO"

            'grdKDDIAGNOSAPREOP2.Properties.DataSource = ds.Tables("DIAGNOSA")
            'grdKDDIAGNOSAPREOP2.Properties.ValueMember = "MEMO"   'KDDIAGNOSA
            'grdKDDIAGNOSAPREOP2.Properties.DisplayMember = "MEMO"

            'grdKDDIAGNOSAPREOP3.Properties.DataSource = ds.Tables("DIAGNOSA")
            'grdKDDIAGNOSAPREOP3.Properties.ValueMember = "MEMO"   'KDDIAGNOSA
            'grdKDDIAGNOSAPREOP3.Properties.DisplayMember = "MEMO"

            grdKDDIAGNOSAPOSTOP1.Properties.DataSource = ds.Tables("DIAGNOSA")
            grdKDDIAGNOSAPOSTOP1.Properties.ValueMember = "MEMO"
            grdKDDIAGNOSAPOSTOP1.Properties.DisplayMember = "MEMO"

            'grdKDDIAGNOSAPOSTOP2.Properties.DataSource = ds.Tables("DIAGNOSA")
            'grdKDDIAGNOSAPOSTOP2.Properties.ValueMember = "MEMO"
            'grdKDDIAGNOSAPOSTOP2.Properties.DisplayMember = "MEMO"

            'grdKDDIAGNOSAPOSTOP3.Properties.DataSource = ds.Tables("DIAGNOSA")
            'grdKDDIAGNOSAPOSTOP3.Properties.ValueMember = "MEMO"
            'grdKDDIAGNOSAPOSTOP3.Properties.DisplayMember = "MEMO"

            If oConn.State = ConnectionState.Open Then
                oConn.Close()
            End If
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_PROSEDUR()
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
            SQL &= "* "
            SQL &= "FROM "
            SQL &= "M_PROSEDUR A "
            SQL &= "WHERE "
            SQL &= "A.ISACTIVE = 1 "

            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "PROSEDUR")

            grdKDPROSEDUR1.Properties.DataSource = ds.Tables("PROSEDUR")
            grdKDPROSEDUR1.Properties.ValueMember = "MEMO"
            grdKDPROSEDUR1.Properties.DisplayMember = "MEMO"

            'grdKDPROSEDUR2.Properties.DataSource = ds.Tables("PROSEDUR")
            'grdKDPROSEDUR2.Properties.ValueMember = "MEMO"
            'grdKDPROSEDUR2.Properties.DisplayMember = "MEMO"

            'grdKDPROSEDUR3.Properties.DataSource = ds.Tables("PROSEDUR")
            'grdKDPROSEDUR3.Properties.ValueMember = "MEMO"
            'grdKDPROSEDUR3.Properties.DisplayMember = "MEMO"

            If oConn.State = ConnectionState.Open Then
                oConn.Close()
            End If
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub

    Private Sub fn_JENISOPERASI(Optional ByVal sSpesialis As String = "")
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
            SQL &= "* "
            SQL &= "FROM "
            SQL &= "M_LAPORAN_OPERASI A "
            SQL &= "WHERE "
            SQL &= "A.ISACTIVE = 1 "

            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "JENISLAPORAN")

            cboJenisOperasi.Properties.DataSource = ds.Tables("JENISLAPORAN")
            cboJenisOperasi.Properties.ValueMember = "NAMALAPORAN"
            cboJenisOperasi.Properties.DisplayMember = "NAMALAPORAN"

            If oConn.State = ConnectionState.Open Then
                oConn.Close()
            End If

            If sSpesialis <> "" Then
                For i As Integer = 0 To ds.Tables("JENISLAPORAN").Rows.Count -1
                    If ds.Tables("JENISLAPORAN").Rows(i)("LAMPIRAN").ToString.Contains(sSpesialis) Then
                        cboJenisOperasi.EditValue = ds.Tables("JENISLAPORAN").Rows(i)("NAMALAPORAN").ToString()
                    End If
                Next
            End If
            

        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub

#End Region

#Region "Handlle"
    Private Sub chKamarOperasi_Click(sender As Object, e As EventArgs) Handles CheckEdit1.Click, CheckEdit2.Click, CheckEdit3.Click, CheckEdit4.Click, CheckEdit5.Click, CheckEdit6.Click
        CheckEdit1.Checked = False
        CheckEdit2.Checked = False
        CheckEdit3.Checked = False
        CheckEdit4.Checked = False
        CheckEdit5.Checked = False
        CheckEdit6.Checked = False
    End Sub
    Private Sub chJenisAnestesi_Click(sender As Object, e As EventArgs) Handles CheckEdit7.Click, CheckEdit8.Click, CheckEdit9.Click, CheckEdit10.Click
        CheckEdit7.Checked = False
        CheckEdit8.Checked = False
        CheckEdit9.Checked = False
        CheckEdit10.Checked = False
    End Sub
    Private Sub chKlasifikasi_Click(sender As Object, e As EventArgs) Handles CheckEdit11.Click, CheckEdit12.Click
        CheckEdit11.Checked = False
        CheckEdit12.Checked = False
    End Sub
    Private Sub chJenisOperasi_Click(sender As Object, e As EventArgs) Handles CheckEdit13.Click, CheckEdit14.Click, CheckEdit15.Click, CheckEdit16.Click
        CheckEdit13.Checked = False
        CheckEdit14.Checked = False
        CheckEdit15.Checked = False
        CheckEdit16.Checked = False
    End Sub
    Private Sub chPemeriksaanPA_Click(sender As Object, e As EventArgs) Handles CheckEdit17.Click, CheckEdit18.Click
        CheckEdit17.Checked = False
        CheckEdit18.Checked = False
    End Sub
    Private Sub chPemeriksaanCairan_Click(sender As Object, e As EventArgs) Handles CheckEdit19.Click, CheckEdit20.Click
        CheckEdit19.Checked = False
        CheckEdit20.Checked = False
    End Sub


    Private Sub grdKDDIAGNOSAPREOP1_EditValueChanged(sender As Object, e As EventArgs) Handles grdKDDIAGNOSAPREOP1.EditValueChanged
        txtDiagnosaPreOP.Text = txtDiagnosaPreOP.Text + grdKDDIAGNOSAPREOP1.EditValue + " " & vbCrLf
    End Sub

    Private Sub grdKDDIAGNOSAPOSTOP1_EditValueChanged(sender As Object, e As EventArgs) Handles grdKDDIAGNOSAPOSTOP1.EditValueChanged
        txtDiagnosaPostOP.Text = txtDiagnosaPostOP.Text + grdKDDIAGNOSAPOSTOP1.EditValue + " " & vbCrLf
    End Sub

    Private Sub grdKDPROSEDUR1_EditValueChanged(sender As Object, e As EventArgs) Handles grdKDPROSEDUR1.EditValueChanged
        txtProsedur.Text = txtProsedur.Text + grdKDPROSEDUR1.EditValue + " " & vbCrLf
    End Sub

    Private Sub LabelControl57_Click(sender As Object, e As EventArgs) 

    End Sub

    Private Sub SimpleButton2_Click(sender As Object, e As EventArgs) 
        'Shell("explorer ""\\172.165.115.250\barcode""", vbNormalFocus)
        Dim fBrowse As New OpenFileDialog
        With fBrowse
            .Filter = "image jpg files(*.jpg)|*.jpg"
            .Title = "Upload Barcode Implan"
            .Multiselect = False
        End With

        If fBrowse.ShowDialog() = Windows.Forms.DialogResult.OK Then
            Dim src = fBrowse.FileName
            Dim fileName = Path.GetFileNameWithoutExtension(src)
            Dim fileNameExt = Path.GetExtension(src)
            Dim oDate As String = DateTime.Now.ToString("yyyyMMddHHmmss")

            Dim dest = "\\172.165.115.250\barcode\Uploaded\" & fileName & "_" & oDate & fileNameExt
            'TextEdit20.Text = dest
            System.IO.File.Copy(src, dest)
        End If

    End Sub

    Private Sub txtDokter_EditValueChanged(sender As Object, e As EventArgs) Handles txtDokter.EditValueChanged
        If txtDokter.Text IsNot Nothing Then
            Try
                Dim index = txtDokter.Text.ToUpper.IndexOf(" SP")
                If index > -1 Then
                    Dim str As String = ""

                    Try
                        str = txtDokter.Text.ToString.ToUpper.Substring(index,6)
                    Catch ex As Exception
                        str = txtDokter.Text.ToString.ToUpper.Substring(index,5)
                    End Try

                    fn_JENISOPERASI(str.Trim())
                Else
                    fn_JENISOPERASI()
                End If
            Catch ex As Exception
                Exit Sub
            End Try
        End If
    End Sub

    Private Sub frmTemplateLaporanOperasi_MouseWheel(sender As Object, e As MouseEventArgs) Handles MyBase.MouseWheel
	    If e.Delta > 0 Then
		    'up
		    fn_ScrollPage(True)
	    Else
		    'down
		    fn_ScrollPage(False)
	    End If
    End Sub

    Private Sub fn_ScrollPage(ByVal isUp As Boolean)
	    Dim myView As Point = Me.Panel1.AutoScrollPosition
	    Dim scrollchange As Integer = 50

	    If isUp Then
		    'up
		    myView.X = -myView.X
		    myView.y = -scrollchange - myView.Y
	    Else
		    'down
		    myView.X = -myView.X
		    myView.y = scrollchange - myView.Y
	    End If

	    Me.Panel1.AutoScrollPosition = myView
    End Sub

#End Region
End Class