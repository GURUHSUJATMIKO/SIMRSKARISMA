Imports DataAccess
Imports UI.WIN.MAIN.My.Resources
Imports System.Linq
Imports System.Data.SqlClient

Public Class frmSuratKeteranganSehat
#Region "Declaration"
    Private oFormMode As FORM_MODE = FORM_MODE.FORM_MODE_VIEW
    Private isLoad As Boolean = False
    Private sDOCTOR As String = String.Empty
    Private sNoId As String = String.Empty
    Private oSuratKeteranganSehat As New Digital.clsSuratKeteranganSehat
#End Region
#Region "Function"
    Public Sub fn_LoadTTV(ByVal tinggibadan As String, ByVal beratbadan As String, ByVal tekanandarah As String, ByVal nadi As String, ByVal pernapasan As String, ByVal suhutubuh As String, ByVal pemeriksaanumum As String)
        txtTINGGIBADAN.Text = tinggibadan
        txtBERATBADAN.Text = beratbadan
        txtTEKANANDARAH.Text = tekanandarah
        txtNADI.Text = nadi
        txtPERNAPASAN.Text = pernapasan
        txtSUHUTUBUH.Text = suhutubuh
        txtPEMERIKSAANFISIK.Text = pemeriksaanumum
    End Sub
    Public Sub LoadMe(ByVal FormMode As Integer, ByVal noreg As String, ByVal tanggalmasuk As DateTime, ByVal norm As String, ByVal nama As String, ByVal tempatlahir As String, ByVal tanggallahir As DateTime, ByVal JK As String, ByVal alamat As String, ByVal kddoctor As String, ByVal NoId As String)
        sNoId = NoId
        txtKDREG.Text = noreg
        deDATE.DateTime = tanggalmasuk
        txtKDCUSTOMER.Text = norm
        txtNAMA.Text = nama
        txtTEMPATLAHIR.Text = tempatlahir
        dedTANGGALLAHIR.DateTime = tanggallahir
        txtJENISKELAMIN.Text = JK
        sDOCTOR = kddoctor
        txtALAMAT.Text = alamat
        oFormMode = FormMode
    End Sub
    Private Sub Form_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.Text = "Surat Keterangan Sehat"
        fn_ChangeFormState()
        isLoad = True
    End Sub
    Private Sub Form_Closing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        'sCode = txtKDSuratKeteranganSehat.Text.Trim.ToUpper
    End Sub
    Private Sub fn_ChangeFormState()
        fn_LoadDokter()

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

        txtTINGGIBADAN.Properties.ReadOnly = Status
        txtBERATBADAN.Properties.ReadOnly = Status
        txtTEKANANDARAH.Properties.ReadOnly = Status
        txtNADI.Properties.ReadOnly = Status
        txtPERNAPASAN.Properties.ReadOnly = Status
        txtSUHUTUBUH.Properties.ReadOnly = Status
        txtPEMERIKSAANFISIK.Properties.ReadOnly = Status
        txtKESIMPULAN.Properties.ReadOnly = Status
        txtKEPERLUAN.Properties.ReadOnly = Status
        deDATE.Properties.ReadOnly = Status
        txtALAMAT.Properties.ReadOnly = Status
        grdKDDOCTOR.Properties.ReadOnly = Status
    End Sub
    Private Sub fn_EmptyMe()
        txtNOMORSURAT.Text = "<--- AUTO --->"
        'txtTINGGIBADAN.ResetText()
        'txtBERATBADAN.ResetText()
        'txtTEKANANDARAH.ResetText()
        'txtNADI.ResetText()
        'txtPERNAPASAN.ResetText()
        'txtSUHUTUBUH.ResetText()
        'txtPEMERIKSAANFISIK.ResetText()
        'txtKESIMPULAN.ResetText()
        'txtKEPERLUAN.ResetText()
        grdKDDOCTOR.Text = sDOCTOR
        'deDATE.DateTime = Now
    End Sub
    Private Sub fn_LoadData()
        Try
            ' ***** HEADER *****
            Dim ds = oSuratKeteranganSehat.GetData(sNoId)

            With ds
                txtNOMORSURAT.Text = .NOMORSURAT
                deDATE.DateTime = .DATE
                txtALAMAT.Text = .ALAMAT
                grdKDDOCTOR.Text = .DOCTOR_KODE
                txtTINGGIBADAN.Text = .TINGGIBADAN
                txtBERATBADAN.Text = .BERATBADAN
                txtTEKANANDARAH.Text = .TEKANANDARAH
                txtNADI.Text = .NADI
                txtPERNAPASAN.Text = .PERNAPASAN
                txtSUHUTUBUH.Text = .SUHUTUBUH
                txtPEMERIKSAANFISIK.Text = .PEMERIKSAANFISIKUMUM
                txtKESIMPULAN.Text = .KESIMPULAN
                txtKEPERLUAN.Text = .UNTUKKEPERLUAN
            End With
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Function fn_Validate() As Boolean
        Try
            fn_Validate = True

            If txtKDREG.Text = String.Empty Then
                txtKDREG.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                txtKDREG.ErrorText = Statement.ErrorRequired

                txtKDREG.Focus()
                fn_Validate = False
                Exit Function
            End If
            If txtKDCUSTOMER.Text = String.Empty Then
                txtKDCUSTOMER.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                txtKDCUSTOMER.ErrorText = Statement.ErrorRequired

                txtKDCUSTOMER.Focus()
                fn_Validate = False
                Exit Function
            End If
            If grdKDDOCTOR.Text = String.Empty Then
                grdKDDOCTOR.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                grdKDDOCTOR.ErrorText = Statement.ErrorRequired

                grdKDDOCTOR.Focus()
                fn_Validate = False
                Exit Function
            End If
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Function
    Private Function fn_Save() As Boolean
        Try
            ' ***** HEADER *****
            Dim ds = oSuratKeteranganSehat.GetStructureHeader
            With ds
                .NOMORSURAT = sNoId
                .KDCUSTOMER = txtKDCUSTOMER.Text
                .KDREG = txtKDREG.Text
                Try
                    .DATECREATED = oSuratKeteranganSehat.GetData(sNoId).DATECREATED
                Catch oErr As Exception
                    .DATECREATED = Now
                End Try
                .DATEUPDATED = Now
                .DATE = deDATE.DateTime
                .NAMA = txtNAMA.Text
                .TEMPAT = txtTEMPATLAHIR.Text
                .TANGGALLAHIR = dedTANGGALLAHIR.DateTime
                .JENISKELAMIN = txtJENISKELAMIN.Text
                .ALAMAT = txtALAMAT.Text
                .TELAHDIPERIKSA = "Telah diperiksa pada tanggal " & deDATE.DateTime.ToString("dd-MM-yyyy") & " di RS Karisma Cimareme, dengan hasil pemeriksaan sebagai berikut:"
                .TINGGIBADAN = txtTINGGIBADAN.Text
                .BERATBADAN = txtBERATBADAN.Text
                .TEKANANDARAH = txtTEKANANDARAH.Text
                .NADI = txtNADI.Text
                .PERNAPASAN = txtPERNAPASAN.Text
                .SUHUTUBUH = txtSUHUTUBUH.Text
                .PEMERIKSAANFISIKUMUM = txtPEMERIKSAANFISIK.Text
                .KESIMPULAN = txtKESIMPULAN.Text
                .UNTUKKEPERLUAN = txtKEPERLUAN.Text
                .DOCTOR_KODE = grdKDDOCTOR.EditValue
                .DOCTOR_NAME_DISPLAY = grdKDDOCTOR.Text
                .DOCTOR_NIP = fn_LoadDokternip(.DOCTOR_KODE)
                .KDUSER = sUserID
            End With

            If oFormMode = FORM_MODE.FORM_MODE_ADD Then
                Try
                    fn_Save = oSuratKeteranganSehat.InsertData(ds)
                Catch oErr As Exception
                    MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            ElseIf oFormMode = FORM_MODE.FORM_MODE_EDIT Then
                Try
                    fn_Save = oSuratKeteranganSehat.UpdateData(ds)
                Catch oErr As Exception
                    MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            End If
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            fn_Save = False
        End Try
    End Function
    Private Function fn_LoadDokternip(ByVal Paramater As String) As String
        Try
            fn_LoadDokternip = ""

            Dim oConn As New SqlConnection
            Dim oComm As New SqlCommand
            Dim da As SqlDataAdapter
            Dim ds As New DataSet
            Dim SQL As String
            Dim sConn As String = sConnOld

            oConn = New SqlConnection(sConn)

            If oConn.State = ConnectionState.Closed Then
                oConn.Open()
            End If

            SQL = "SELECT "
            SQL &= "* "
            SQL &= "FROM  "
            SQL &= "M_DOCTOR "
            SQL &= "WHERE "
            SQL &= "KDDOCTOR = '" & Paramater & "' "

            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "M_DOCTOR")

            If oConn.State = ConnectionState.Open Then
                oConn.Close()
            End If

            For iLoop As Integer = 0 To ds.Tables("M_DOCTOR").Rows.Count - 1
                With ds.Tables("M_DOCTOR")
                    fn_LoadDokternip = .Rows(iLoop)("SIP")
                End With
            Next
        Catch oErr As Exception
            fn_LoadDokternip = ""
            MsgBox("Load Dokter Data : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Function
#End Region
#Region "Grid Method"

#End Region
#Region "Command Button"
    Private Sub frmSuratKeteranganSehat_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        Select Case e.KeyCode
            Case Keys.F12
                btnClose_Click()
            'Case Keys.F2
            '    If btnSaveNew.Enabled = True Then
            '        btnSaveNew_Click()
            '    End If
            Case Keys.F3
                If btnSaveClose.Enabled = True Then
                    btnSaveClose_Click()
                End If
        End Select
    End Sub
    Private Sub btnSaveNew_Click() Handles btnSaveNew.ItemClick
        If fn_Validate() = False Then Exit Sub
        If MsgBox(Statement.SaveQuestion, MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, Me.Text) = MsgBoxResult.No Then Exit Sub
        If fn_Save() = False Then
            MsgBox(Statement.SaveFail, MsgBoxStyle.Exclamation, Me.Text)
        Else
            MsgBox(Statement.SaveSuccess, MsgBoxStyle.Information, Me.Text)
            sStatusSave = "NEW"
            Me.Close()
        End If
    End Sub
    Private Sub btnSaveClose_Click() Handles btnSaveClose.ItemClick
        If fn_Validate() = False Then Exit Sub
        If MsgBox(Statement.SaveQuestion, MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, Me.Text) = MsgBoxResult.No Then Exit Sub
        If fn_Save() = False Then
            MsgBox(Statement.SaveFail, MsgBoxStyle.Exclamation, Me.Text)
        Else
            MsgBox(Statement.SaveSuccess, MsgBoxStyle.Information, Me.Text)
            Me.Close()
        End If
    End Sub
    Private Sub btnClose_Click() Handles btnClose.ItemClick
        Me.Close()
    End Sub
#End Region
#Region "Lookup / Event"
    Private Sub fn_LoadDokter()
        Try
            Dim oConn As New SqlConnection
            Dim oComm As New SqlCommand
            Dim da As SqlDataAdapter
            Dim ds As New DataSet
            Dim SQL As String
            Dim sConn As String = sConnOld

            oConn = New SqlConnection(sConn)

            If oConn.State = ConnectionState.Closed Then
                oConn.Open()
            End If

            SQL = "SELECT "
            SQL &= "A.KDDOCTOR "
            SQL &= ",NAME_DISPLAY = (SELECT CASE A.FRONT_TITLE WHEN '' THEN '' ELSE A.FRONT_TITLE + ' ' END) + A.NAME_DISPLAY + A.BACK_TITLE "
            SQL &= "FROM  "
            SQL &= "M_DOCTOR A "
            SQL &= "WHERE "
            SQL &= "ISACTIVE = '1' "
            SQL &= "AND CATEGORY = 1 "
            SQL &= "ORDER BY NAME_DISPLAY "

            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "M_DOCTOR")

            grdKDDOCTOR.Properties.DataSource = ds.Tables("M_DOCTOR")
            grdKDDOCTOR.Properties.ValueMember = "KDDOCTOR"
            grdKDDOCTOR.Properties.DisplayMember = "NAME_DISPLAY"

            If oConn.State = ConnectionState.Open Then
                oConn.Close()
            End If
        Catch oErr As Exception
            MsgBox("Load Dokter Data : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
#End Region
End Class