Imports DataAccess
Imports UI.WIN.MAIN.My.Resources
Imports System.Linq
Imports System.Data.SqlClient

Public Class frmSuratKeteranganSakit
#Region "Declaration"
    Private oFormMode As FORM_MODE = FORM_MODE.FORM_MODE_VIEW
    Private isLoad As Boolean = False
    Private sDOCTOR As String = String.Empty

    Private oSuratKeteranganSakit As New Digital.clsSuratKeteranganSakit
#End Region
#Region "Function"
    Public Sub LoadMe(ByVal FormMode As Integer, ByVal NoId As String, ByVal tanggalmasuk As DateTime, ByVal norm As String, ByVal nama As String, ByVal umur As String, ByVal alamat As String, ByVal kddoctor As String)
        txtKDREG.Text = NoId
        deDATE.DateTime = tanggalmasuk
        txtKDCUSTOMER.Text = norm
        txtNAMA.Text = nama
        txtUMUR.Text = umur
        txtALAMAT.Text = alamat
        sDOCTOR = kddoctor
        oFormMode = FormMode
    End Sub
    Private Sub Form_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.Text = "Surat Keterangan Sakit"
        fn_ChangeFormState()
        isLoad = True
    End Sub
    Private Sub Form_Closing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        'sCode = txtKDSuratKeteranganSakit.Text.Trim.ToUpper
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

        deDATE.Properties.ReadOnly = Status
        cboSELAMA.Properties.ReadOnly = Status
        deDARITANGGAL.Properties.ReadOnly = Status
        deSAMPAITANGGAL.Properties.ReadOnly = Status
        txtALAMAT.Properties.ReadOnly = Status
        grdKDDOCTOR.Properties.ReadOnly = Status
    End Sub
    Private Sub fn_EmptyMe()
        cboSELAMA.ResetText()
        deDARITANGGAL.DateTime = deDATE.DateTime
        deSAMPAITANGGAL.DateTime = deDATE.DateTime
        grdKDDOCTOR.Text = sDOCTOR
    End Sub
    Private Sub fn_LoadData()
        Try
            ' ***** HEADER *****
            Dim ds = oSuratKeteranganSakit.GetData(txtKDREG.Text)

            With ds
                deDATE.DateTime = .DATE
                cboSELAMA.Text = .SELAMA
                deDARITANGGAL.DateTime = .DARITANGGAL
                deSAMPAITANGGAL.DateTime = .SAMPAITANGGAL
                txtALAMAT.Text = .ALAMAT
                grdKDDOCTOR.Text = .DOCTOR_KODE
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
            If cboSELAMA.Text = String.Empty Then
                cboSELAMA.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                grdKDDOCTOR.ErrorText = Statement.ErrorRequired

                cboSELAMA.Focus()
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
            Dim ds = oSuratKeteranganSakit.GetStructureHeader
            With ds
                Try
                    .DATECREATED = oSuratKeteranganSakit.GetData(txtKDREG.Text).DATECREATED
                Catch oErr As Exception
                    .DATECREATED = Now
                End Try
                .DATEUPDATED = Now
                .KDCUSTOMER = txtKDCUSTOMER.Text
                .KDREG = txtKDREG.Text
                .DATE = deDATE.DateTime
                .NAMA = txtNAMA.Text
                .UMUR = txtUMUR.Text
                .ALAMAT = txtALAMAT.Text
                .SELAMA = cboSELAMA.Text
                .DARITANGGAL = deDARITANGGAL.DateTime
                .SAMPAITANGGAL = deSAMPAITANGGAL.DateTime
                .DOCTOR_KODE = grdKDDOCTOR.EditValue
                .DOCTOR_NAME_DISPLAY = grdKDDOCTOR.Text
                .KDUSER = sUserID
            End With

            If oFormMode = FORM_MODE.FORM_MODE_ADD Then
                Try
                    fn_Save = oSuratKeteranganSakit.InsertData(ds)
                Catch oErr As Exception
                    MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            ElseIf oFormMode = FORM_MODE.FORM_MODE_EDIT Then
                Try
                    fn_Save = oSuratKeteranganSakit.UpdateData(ds)
                Catch oErr As Exception
                    MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            End If
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            fn_Save = False
        End Try
    End Function
#End Region
#Region "Grid Method"

#End Region
#Region "Command Button"
    Private Sub frmSuratKeteranganSakit_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
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
    Private Sub cboSELAMA_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboSELAMA.SelectedIndexChanged
        If isLoad = False Then Exit Sub
        If cboSELAMA.Text <> "" Then
            deSAMPAITANGGAL.DateTime = deDARITANGGAL.DateTime.AddDays(cboSELAMA.Text)
        End If
    End Sub
#End Region
End Class