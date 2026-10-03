Imports DataAccess
Imports UI.WIN.MAIN.My.Resources
Imports System.Linq
Imports System.Data.SqlClient

Public Class frmLembarObservasi
#Region "Declaration"
    Private oFormMode As FORM_MODE = FORM_MODE.FORM_MODE_VIEW
    Private sNoId As String
    Private isLoad As Boolean = False
    Private oLembarObservasi As New Inventory.clsLembarObservasi
#End Region
#Region "Function"
    Public Sub LoadMe(ByVal FormMode As Integer, ByVal CopyKode As String, ByVal KDREG As String, ByVal KDCUSTOMER As String, ByVal NAMAPASIEN As String, ByVal TANGGALLAHIR As DateTime, ByVal UMUR As String, ByVal JK As String, ByVal PENJAMIN As String, ByVal TUJUAN As String, ByVal KDDOCTOR As String, ByVal KARTUBPJS As String, ByVal NoId As String)
        oFormMode = FormMode
        sNoId = NoId
        txtKDPENDAFTARAN.Text = KDREG
        grdKDDOCTOR.Text = KDDOCTOR
        txtTUJUAN.Text = TUJUAN
        txtKDCUSTOMER.Text = KDCUSTOMER
        txtKARTUBPJS.Text = KARTUBPJS
        txtNAMAPASIEN.Text = NAMAPASIEN
        txtJENISKELAMIN.Text = JK
        deTANGGALLAHIR.DateTime = TANGGALLAHIR
    End Sub
    Private Sub Form_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        fn_ChangeFormState()
        fn_LoadLanguage()
        isLoad = True
    End Sub
    Public Sub fn_LoadLanguage()
        Try
            Me.Text = "Lembar Observasi"

            btnSaveNew.Caption = Caption.FormSaveNew
            btnSaveClose.Caption = Caption.FormSaveClose
            btnClose.Caption = Caption.FormClose
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub Form_Closing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        sCode = txtKDLEMBAROBSERVASI.Text.Trim.ToUpper
    End Sub
    Private Sub fn_ChangeFormState()
        fn_LoadKDDOCTOR()
        fn_LoadKDUNIT()

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
        'txtKDPENDAFTARAN.Properties.ReadOnly = Status
        'grdKDDOCTOR.Properties.ReadOnly = Status
        'txtTUJUAN.Properties.ReadOnly = Status
        'txtKDCUSTOMER.Properties.ReadOnly = Status
        'txtKARTUBPJS.Properties.ReadOnly = Status
        'txtNAMAPASIEN.Properties.ReadOnly = Status
        'deTANGGALLAHIR.Properties.ReadOnly = Status
        'txtJENISKELAMIN.Properties.ReadOnly = Status
        txtMEMO.Properties.ReadOnly = Status

        grvDetail.OptionsBehavior.ReadOnly = Status
    End Sub
    Private Sub fn_EmptyMe()
        txtKDLEMBAROBSERVASI.Text = "<--- AUTO --->"
        deDATE.DateTime = Now
        'grdKDDOCTOR.ResetText()
        'txtMEMO.ResetText()
        'txtTUJUAN.ResetText()
        'txtKDCUSTOMER.ResetText()
        'txtKARTUBPJS.ResetText()
        'txtNAMAPASIEN.ResetText()
        'deTANGGALLAHIR.DateTime = Now
        'txtJENISKELAMIN.ResetText()
    End Sub
    Private Sub fn_LoadData()
        Try
            ' ***** HEADER *****
            Dim ds = oLembarObservasi.GetData(sNoId)

            With ds
                txtKDLembarObservasi.Text = .KDLEMBAROBSERVASI
                deDATE.DateTime = .DATE
                'txtKDPENDAFTARAN.Text = .KDPENDAFTARAN
                'grdKDDOCTOR.Text = .KDDOCTOR
                'txtTUJUAN.Text = .TUJUAN
                'txtKDCUSTOMER.Text = .KDCUSTOMER
                'txtKARTUBPJS.Text = .KARTUBPJS
                'txtNAMAPASIEN.Text = .NAMAPASIEN
                'deTANGGALLAHIR.DateTime = .TANGGALLAHIR
                'txtJENISKELAMIN.Text = .JENISKELAMIN
                txtMEMO.Text = .DESCRIPTION

                bindingSource.DataSource = oLembarObservasi.GetDataDetail(sNoId).OrderBy(Function(x) x.SEQ).ToList()
                grdDetail.DataSource = bindingSource
            End With
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Function fn_Validate() As Boolean
        Try
            fn_Validate = True

            If txtKDPENDAFTARAN.Text = String.Empty Then
                txtKDPENDAFTARAN.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                txtKDPENDAFTARAN.ErrorText = Statement.ErrorRequired

                txtKDPENDAFTARAN.Focus()
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

            grvDetail.UpdateCurrentRow()

            If grvDetail.RowCount < 2 Then
                MsgBox(Statement.ErrorDetail, MsgBoxStyle.Exclamation, Me.Text)
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
            Dim ds = oLembarObservasi.GetStructureHeader
            With ds
                Try
                    .DATECREATED = oLembarObservasi.GetData(sNoId).DATECREATED
                Catch oErr As Exception
                    .DATECREATED = Now
                End Try
                .DATEUPDATED = Now

                .KDLEMBAROBSERVASI = sNoId
                .DATE = deDATE.DateTime
                .KDPENDAFTARAN = txtKDPENDAFTARAN.Text
                .KDDOCTOR = IIf(String.IsNullOrEmpty(grdKDDOCTOR.EditValue), String.Empty, grdKDDOCTOR.EditValue)
                .DOKTER = grdKDDOCTOR.Text
                .TUJUAN = txtTUJUAN.Text
                .KDCUSTOMER = txtKDCUSTOMER.Text
                .KARTUBPJS = txtKARTUBPJS.Text
                .NAMAPASIEN = txtNAMAPASIEN.Text
                .TANGGALLAHIR = deTANGGALLAHIR.DateTime
                .JENISKELAMIN = txtJENISKELAMIN.Text
                .DESCRIPTION = txtMEMO.Text.Trim.ToUpper
                .KDUSER = sUserID
            End With

            ' ***** DETIL *****
            Dim arrDetail = oLembarObservasi.GetStructureDetailList
            For i As Integer = 0 To grvDetail.RowCount - 2
                Dim dsDetail = oLembarObservasi.GetStructureDetail
                With dsDetail
                    .KDLEMBAROBSERVASI = ds.KDLEMBAROBSERVASI
                    .SEQ = i
                    .DATE = IIf(String.IsNullOrEmpty(grvDetail.GetRowCellValue(i, colDATE)), Now, CDate(grvDetail.GetRowCellValue(i, colDATE)))
                    .TEKANANDARAH = IIf(String.IsNullOrEmpty(grvDetail.GetRowCellValue(i, colTEKANANDARAH)), "", grvDetail.GetRowCellValue(i, colTEKANANDARAH))
                    .NADI = IIf(String.IsNullOrEmpty(grvDetail.GetRowCellValue(i, colNADI)), "", grvDetail.GetRowCellValue(i, colNADI))
                    .RESPIRASI = IIf(String.IsNullOrEmpty(grvDetail.GetRowCellValue(i, colRESPIRASI)), "", grvDetail.GetRowCellValue(i, colRESPIRASI))
                    .SUHU = IIf(String.IsNullOrEmpty(grvDetail.GetRowCellValue(i, colSUHU)), "", grvDetail.GetRowCellValue(i, colSUHU))
                    .SATURASI = IIf(String.IsNullOrEmpty(grvDetail.GetRowCellValue(i, colSATURASI)), "", grvDetail.GetRowCellValue(i, colSATURASI))
                    .INPUT = IIf(String.IsNullOrEmpty(grvDetail.GetRowCellValue(i, colINPUT)), "", grvDetail.GetRowCellValue(i, colINPUT))
                    .OUTPUT = IIf(String.IsNullOrEmpty(grvDetail.GetRowCellValue(i, colOUTPUT)), "", grvDetail.GetRowCellValue(i, colOUTPUT))
                    .TINDAKAN = IIf(String.IsNullOrEmpty(grvDetail.GetRowCellValue(i, colTINDAKAN)), "", grvDetail.GetRowCellValue(i, colTINDAKAN))
                    .NAMAPERAWAT = IIf(String.IsNullOrEmpty(grvDetail.GetRowCellValue(i, colNAMAPERAWAT)), "", grvDetail.GetRowCellValue(i, colNAMAPERAWAT))
                    .DESCRIPTION = IIf(String.IsNullOrEmpty(grvDetail.GetRowCellValue(i, colDESCRIPTION)), "", grvDetail.GetRowCellValue(i, colDESCRIPTION))
                End With
                arrDetail.Add(dsDetail)
            Next

            If oFormMode = FORM_MODE.FORM_MODE_ADD Then
                Try
                    fn_Save = oLembarObservasi.InsertData(ds, arrDetail)
                Catch oErr As Exception
                    MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            ElseIf oFormMode = FORM_MODE.FORM_MODE_EDIT Then
                Try
                    fn_Save = oLembarObservasi.UpdateData(ds, arrDetail)
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
    Private Sub grvDetail_CellValueChanged(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs) Handles grvDetail.CellValueChanged
        'If e.Column.Name = colKDITEM.Name Then
        '    Dim oItem As New Reference.clsItem
        '    Try
        '        If grvDetail.GetFocusedRowCellValue(colKDITEM) IsNot Nothing Then
        '            Dim ds = oItem.GetDataDetail_UOM(grvDetail.GetFocusedRowCellValue(colKDITEM))

        '            If ds IsNot Nothing Then
        '                grvDetail.SetFocusedRowCellValue(colKDUOM, ds.FirstOrDefault(Function(x) x.RATE = 1).KDUOM)
        '            Else
        '                MsgBox(Statement.ErrorUOM, MsgBoxStyle.Exclamation, Me.Text)

        '                grvDetail.CancelUpdateCurrentRow()
        '            End If
        '        End If
        '    Catch oErr As Exception
        '        MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        '    End Try
        'ElseIf e.Column.Name = colKDUOM.Name Then
        '    Dim oItem As New Reference.clsItem
        '    Try
        '        If grvDetail.GetFocusedRowCellValue(colKDITEM) IsNot Nothing And grvDetail.GetFocusedRowCellValue(colKDUOM) IsNot Nothing Then
        '            Dim ds = oItem.GetDataDetail_UOM(grvDetail.GetFocusedRowCellValue(colKDITEM), grvDetail.GetFocusedRowCellValue(colKDUOM))

        '            If ds IsNot Nothing Then

        '            Else
        '                MsgBox(Statement.ErrorUOM, MsgBoxStyle.Exclamation, Me.Text)

        '                Dim sItem = grvDetail.GetFocusedRowCellValue(colKDITEM)
        '                grvDetail.CancelUpdateCurrentRow()

        '                grvDetail.AddNewRow()
        '                grvDetail.SetFocusedRowCellValue(colKDITEM, sItem)
        '            End If
        '        End If
        '    Catch oErr As Exception
        '        MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        '    End Try
        'ElseIf e.Column.Name = colQTY.Name Then
        '    If CDec(grvDetail.GetFocusedRowCellValue(colQTY)) = 0 Then
        '        grvDetail.SetFocusedRowCellValue(colQTY, 1)
        '    End If
        'End If
    End Sub
    Private Sub DeleteToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DeleteToolStripMenuItem.Click
        If oFormMode = FORM_MODE.FORM_MODE_VIEW Then Exit Sub
        grvDetail.DeleteSelectedRows()
    End Sub
#End Region
#Region "Command Button"
    Private Sub frmLembarObservasi_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
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
    Private Sub fn_LoadKDDOCTOR()
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
            SQL &= "* "
            SQL &= "FROM  "
            SQL &= "M_DOCTOR "
            SQL &= "WHERE "
            SQL &= "ISACTIVE = '1' "

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
    Private Sub fn_LoadKDUNIT()
        Try
            Dim oTemplate As New Reference.clsUnit
            Try
                grdUNIT.DataSource = oTemplate.GetData.Where(Function(x) x.ISACTIVE = True).ToList()
                grdUNIT.ValueMember = "KDUNIT"
                grdUNIT.DisplayMember = "MEMO"
            Catch oErr As Exception
                MsgBox("Load User : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            End Try
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub btnAmbilAkhir_Click(sender As Object, e As EventArgs) Handles btnAmbilAkhir.Click
        Try
            Dim ds = oLembarObservasi.GetDataByRMTerakhir(txtKDCUSTOMER.Text).FirstOrDefault()

            If ds IsNot Nothing Then
                grvDetail.Focus()
                grvDetail.AddNewRow()
                grvDetail.SetFocusedRowCellValue(colDATE, Now)
                grvDetail.SetFocusedRowCellValue(colTEKANANDARAH, ds.TEKANANDARAH)
                grvDetail.SetFocusedRowCellValue(colNADI, ds.NADI)
                grvDetail.SetFocusedRowCellValue(colRESPIRASI, ds.RESPIRASI)
                grvDetail.SetFocusedRowCellValue(colSUHU, ds.SUHU)
                grvDetail.SetFocusedRowCellValue(colSATURASI, ds.SATURASI)
                grvDetail.SetFocusedRowCellValue(colINPUT, ds.INPUT)
                grvDetail.SetFocusedRowCellValue(colOUTPUT, ds.OUTPUT)
                grvDetail.SetFocusedRowCellValue(colTINDAKAN, ds.TINDAKAN)
                grvDetail.SetFocusedRowCellValue(colNAMAPERAWAT, "")
                grvDetail.UpdateCurrentRow()
            End If

        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try

    End Sub
#End Region
End Class