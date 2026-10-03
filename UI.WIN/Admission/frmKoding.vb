Imports DataAccess
Imports UI.WIN.MAIN.My.Resources
Imports System.Linq
Imports Newtonsoft.Json.Linq
Imports System.Data.SqlClient


Public Class frmKoding
#Region "Declaration"
    Private oFormMode As FORM_MODE = FORM_MODE.FORM_MODE_VIEW
    Private sNoId As String
    Private isLoad As Boolean = False
    Private sKDREG As String
    Private oKoding As New Admission.clsKoding
    Private sReq As String = String.Empty

#End Region
#Region "Function"
    Public Sub fn_Req(ByVal Req As String)
        sReq = Req
    End Sub
    Public Sub LoadMe(ByVal FormMode As Integer, ByVal Register As String, Optional ByVal NoId As String = "")
        oFormMode = FormMode
        sNoId = NoId
        sKDREG = Register
    End Sub
    Private Sub Form_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        fn_LoadFilter()
        fn_ChangeFormState()
        isLoad = True
    End Sub
    Private Sub Form_Closing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        Dispose()
        sCode = txtNAME_DISPLAY.Text.Trim.ToUpper
    End Sub
    Private Overloads Sub Dispose()
        MyBase.Dispose()
        Me.Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
    Private Sub fn_ChangeFormState()
        fn_LoadDIGANOSA()
        fn_LoadTINDAKAN()
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
    Private Sub fn_LoadFilter()

    End Sub
    Private Sub fn_ViewMode(ByVal Status As Boolean)
        btnSaveNew.Enabled = Not Status
        btnSaveClose.Enabled = Not Status

        txtKDREG.Properties.ReadOnly = Status
        txtNORM.Properties.ReadOnly = Status
        txtNAME_DISPLAY.Properties.ReadOnly = Status
        txtPOLI.Properties.ReadOnly = Status
        txtDOKTER.Properties.ReadOnly = Status
        deDATE.Properties.ReadOnly = Status

        txtMEMO.Properties.ReadOnly = Status
    End Sub
    Private Sub fn_EmptyMe()
        txtCODE.Text = "<--- AUTO --->"
        txtKDREG.Text = sKDREG
        deDATE.DateTime = Now

        txtMEMO.ResetText()

        If sReq <> "" Then
            Dim dsKodingICDX = oKoding.GetDataDetail_DX(sReq)

            For Each xloop In dsKodingICDX
                grvDetail_DX.AddNewRow()
                grvDetail_DX.SetFocusedRowCellValue(colKDDIAGNOSA, xloop.KDDIAGNOSA)
                grvDetail_DX.SetFocusedRowCellValue(colKET_DX, xloop.KETERANGAN)
                grvDetail_DX.UpdateCurrentRow()
            Next

            tabControlResume.SelectedTabPage = tab2

            Dim dsKodingICDIX = oKoding.GetDataDetail_DIX(sReq)

            For Each xloop In dsKodingICDIX
                grvDetail_D9.AddNewRow()
                grvDetail_D9.SetFocusedRowCellValue(colID, xloop.KDPROSEDUR)
                grvDetail_D9.SetFocusedRowCellValue(colKET_D9, xloop.KETERANGAN)
                grvDetail_D9.UpdateCurrentRow()
            Next

            Dim oReqBHP As New Transaksi.clsREQ_BHP

            Dim dsReqGHPHeader = oReqBHP.GetDataKDPENDAFTARANList(txtKDREG.Text)

            For Each iloop In dsReqGHPHeader

                If iloop.LAB <> "" Then
                    grvDetail_D9.AddNewRow()
                    grvDetail_D9.SetFocusedRowCellValue(colID, oReqBHP.AccountDefault)
                    grvDetail_D9.SetFocusedRowCellValue(colKET_D9, "Lab: " & iloop.LAB)
                    grvDetail_D9.UpdateCurrentRow()
                End If
                If iloop.RONTGEN <> "" Then
                    grvDetail_D9.AddNewRow()
                    grvDetail_D9.SetFocusedRowCellValue(colID, oReqBHP.AccountDefault)
                    grvDetail_D9.SetFocusedRowCellValue(colKET_D9, "Rontgen: " & iloop.RONTGEN)
                    grvDetail_D9.UpdateCurrentRow()
                End If
                If iloop.USG <> "" Then
                    grvDetail_D9.AddNewRow()
                    grvDetail_D9.SetFocusedRowCellValue(colID, oReqBHP.AccountDefault)
                    grvDetail_D9.SetFocusedRowCellValue(colKET_D9, "USG: " & iloop.USG)
                    grvDetail_D9.UpdateCurrentRow()
                End If

                Dim dsReqBHP = oReqBHP.GetDataDetailByKDREQ(iloop.KDREQBHP)
                For Each xloop In dsReqBHP
                    grvDetail_D9.AddNewRow()
                    grvDetail_D9.SetFocusedRowCellValue(colID, oReqBHP.AccountDefault)
                    grvDetail_D9.SetFocusedRowCellValue(colKET_D9, xloop.RINCIAN)
                    grvDetail_D9.UpdateCurrentRow()
                Next

            Next

            tabControlResume.SelectedTabPage = tab3

            Dim dsTerapi = oKoding.GetDataDetail_Terapi(sReq)
            For Each xloop In dsTerapi
                grvTerapi.AddNewRow()
                grvTerapi.SetFocusedRowCellValue(colKETERANGAN, xloop.KETERANGAN)
                grvTerapi.UpdateCurrentRow()
            Next

            Dim dsTindakLanjut = oKoding.GetData(sReq)
            If dsTindakLanjut IsNot Nothing Then
                txtMEMO.Text = dsTindakLanjut.DESCRIPTION
            End If

            tabControlResume.SelectedTabPage = tab1

        End If

    End Sub
    Private Sub fn_LoadData()
        Try
            ' ***** HEADER *****
            Dim ds = oKoding.GetData(sNoId)

            With ds

                txtCODE.Text = .KDKODING
                txtKDREG.Text = .KDPENDAFTARAN

                deDATE.DateTime = .DATE

                txtMEMO.Text = .DESCRIPTION

                BindingSource_DX.DataSource = oKoding.GetDataDetail_DX.Where(Function(x) x.KDKODING = sNoId).OrderBy(Function(x) x.SEQ).ToList()
                grdDetail_DX.DataSource = BindingSource_DX

                BindingSource_D9.DataSource = oKoding.GetDataDetail_D9.Where(Function(x) x.KDKODING = sNoId).OrderBy(Function(x) x.SEQ).ToList()
                grdDetail_D9.DataSource = BindingSource_D9

                BindingSource_Terapi.DataSource = oKoding.GetDataDetail_Terapi.Where(Function(x) x.KDKODING = sNoId).OrderBy(Function(x) x.SEQ).ToList()
                grdTerapi.DataSource = BindingSource_Terapi

                tabControlResume.SelectedTabPage = tab4
                tabControlResume.SelectedTabPage = tab3
                tabControlResume.SelectedTabPage = tab2
                tabControlResume.SelectedTabPage = tab1
            End With
        Catch oErr As Exception
            MsgBox("Load List Data : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Function fn_Validate() As Boolean
        Try
            fn_Validate = True

            grvDetail_DX.UpdateCurrentRow()

            'If grvDetail_DX.RowCount < 2 Then
            '    MsgBox("Dibutuhkan Informasi Alamat", MsgBoxStyle.Exclamation, Me.Text)
            '    tabControl.SelectedTabPageIndex = 1
            '    fn_Validate = False
            '    Exit Function
            'End If

            grvDetail_D9.UpdateCurrentRow()

            'If grvDetail_D9.RowCount < 2 Then
            '    MsgBox("Dibutuhkan Informasi Penanggung Jawab", MsgBoxStyle.Exclamation, Me.Text)
            '    tabControl.SelectedTabPageIndex = 3
            '    fn_Validate = False
            '    Exit Function
            'End If

        Catch oErr As Exception
            MsgBox("Validate Data : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Function
    Private Function fn_Save() As Boolean
        Try
            ' ***** HEADER *****
            Dim ds = oKoding.GetStructureHeader
            With ds
                Try
                    .DATECREATED = oKoding.GetData(sNoId).DATECREATED
                Catch ex As Exception
                    .DATECREATED = Now
                End Try
                .DATEUPDATED = Now

                .KDKODING = sNoId
                .KDPENDAFTARAN = txtKDREG.Text.Trim.ToUpper
                .DATE = deDATE.DateTime
                .DESCRIPTION = txtMEMO.Text.Trim.ToUpper
                .NOIDUSER = sUserID
            End With

            Dim oDiagnosa As New Reference.clsDiagnosa
            Dim oTindakan As New Reference.clsProsedur
            Dim dsDetail_DiagnosisUtama = oKoding.GetStructureDetail_DianosisUtama
            Dim arrDetail_DianosisPenyerta = oKoding.GetStructureDetail_DianosisPenyertaList
            Dim arrDetail_Terapi_Tindakan = oKoding.GetStructureDetail_Terapi_TindakanList

            Dim arrDetail_DX = oKoding.GetStructureDetail_DXList
            For i As Integer = 0 To grvDetail_DX.RowCount - 2
                Dim dsDetail_ADDRESS = oKoding.GetStructureDetail_DX
                With dsDetail_ADDRESS
                    .SEQ = i
                    .KDKODING = ds.KDKODING
                    .KDDIAGNOSA = grvDetail_DX.GetRowCellValue(i, colKDDIAGNOSA)
                    .KETERANGAN = grvDetail_DX.GetRowCellValue(i, colKET_DX)
                End With

                arrDetail_DX.Add(dsDetail_ADDRESS)

                If i = 0 Then
                    With dsDetail_DiagnosisUtama
                        .KDKODING = ds.KDKODING
                        .KDDIAGNOSA = grvDetail_DX.GetRowCellValue(i, colKDDIAGNOSA)
                        .KETERANGAN = IIf(oDiagnosa.GetData(grvDetail_DX.GetRowCellValue(i, colKDDIAGNOSA)).MEMO = "-", grvDetail_DX.GetRowCellValue(i, colKET_DX), grvDetail_DX.GetRowCellValue(i, colKDDIAGNOSA) & " - " & oDiagnosa.GetData(grvDetail_DX.GetRowCellValue(i, colKDDIAGNOSA)).MEMO)
                    End With
                Else
                    Dim dsDetail_DiagnosisPenyerta = oKoding.GetStructureDetail_DianosisPenyerta
                    With dsDetail_DiagnosisPenyerta
                        .KDKODING = ds.KDKODING
                        .KDDIAGNOSA = grvDetail_DX.GetRowCellValue(i, colKDDIAGNOSA)
                        .SEQ = i
                        .KETERANGAN = IIf(oDiagnosa.GetData(grvDetail_DX.GetRowCellValue(i, colKDDIAGNOSA)).MEMO = "-", grvDetail_DX.GetRowCellValue(i, colKET_DX), grvDetail_DX.GetRowCellValue(i, colKDDIAGNOSA) & " - " & oDiagnosa.GetData(grvDetail_DX.GetRowCellValue(i, colKDDIAGNOSA)).MEMO)
                    End With
                    arrDetail_DianosisPenyerta.Add(dsDetail_DiagnosisPenyerta)
                End If
            Next

            Dim SEQ_TERAPI As Integer = 0
            Dim SEQ_TINDAKAN As Integer = 0
            Dim ListTerapi As New List(Of String)
            Dim ListTindakan As New List(Of String)

            Dim arrDetail_PJ = oKoding.GetStructureDetail_D9List
            For j As Integer = 0 To grvDetail_D9.RowCount - 2
                Dim dsDetail_PJ = oKoding.GetStructureDetail_D9
                With dsDetail_PJ
                    .SEQ = j
                    .KDKODING = ds.KDKODING
                    .KDPROSEDUR = grvDetail_D9.GetRowCellValue(j, colID)
                    .KETERANGAN = grvDetail_D9.GetRowCellValue(j, colKET_D9)
                End With
                arrDetail_PJ.Add(dsDetail_PJ)

                ListTindakan.Add(j & "." & IIf(oTindakan.GetData(grvDetail_D9.GetRowCellValue(j, colID)).MEMO = "-", grvDetail_D9.GetRowCellValue(j, colKET_D9), grvDetail_D9.GetRowCellValue(j, colID) & " - " & oTindakan.GetData(grvDetail_D9.GetRowCellValue(j, colID)).MEMO))
                SEQ_TINDAKAN += 1
            Next

            Dim arrDetail_Terapi = oKoding.GetStructureDetail_TerapiList

            For k As Integer = 0 To grvTerapi.RowCount - 2
                Dim dsDetail_Terapi = oKoding.GetStructureDetail_Terapi
                With dsDetail_Terapi
                    .SEQ = k
                    .KDKODING = ds.KDKODING
                    .KETERANGAN = grvTerapi.GetRowCellValue(k, colKETERANGAN)
                End With
                arrDetail_Terapi.Add(dsDetail_Terapi)

                ListTerapi.Add(k & "." & grvTerapi.GetRowCellValue(k, colKETERANGAN))
                SEQ_TERAPI += 1
            Next

            If SEQ_TERAPI >= SEQ_TINDAKAN Then
                Dim TINDAKAN As String = String.Empty

                For l As Integer = 0 To grvTerapi.RowCount - 2
                    TINDAKAN = ""

                    Dim dsDetail_TerapiTindakan = oKoding.GetStructureDetail_Terapi_Tindakan
                    With dsDetail_TerapiTindakan
                        .KDKODING = ds.KDKODING
                        .SEQ = l
                        .TERAPI = grvTerapi.GetRowCellValue(l, colKETERANGAN)
                        For Each xLoop In ListTindakan.Distinct
                            If xLoop.ToString.Contains(l & ".") Then
                                TINDAKAN = xLoop.Replace(l & ".", "")
                                '.TINDAKAN = xLoop.Replace(l & ".", "")
                            End If
                        Next
                        .TINDAKAN = TINDAKAN
                        'Dim dsTindakan = ListTindakan.ToString.Contains(l & ".")
                        'If dsTindakan IsNot Nothing Then
                        '    .TINDAKAN = ListTindakan.Contains(l & ".")
                        'End If
                    End With
                    arrDetail_Terapi_Tindakan.Add(dsDetail_TerapiTindakan)
                Next
            Else
                Dim TES As Integer = SEQ_TINDAKAN - SEQ_TERAPI
                For i As Integer = 1 To TES
                    grvTerapi.AddNewRow()
                    grvTerapi.SetFocusedRowCellValue(colKETERANGAN, "")
                    grvTerapi.UpdateCurrentRow()
                Next

                ListTindakan.Clear()

                For j As Integer = 0 To grvDetail_D9.RowCount - 2
                    ListTindakan.Add(j & "." & IIf(oTindakan.GetData(grvDetail_D9.GetRowCellValue(j, colID)).MEMO = "-", grvDetail_D9.GetRowCellValue(j, colKET_D9), grvDetail_D9.GetRowCellValue(j, colID) & " - " & oTindakan.GetData(grvDetail_D9.GetRowCellValue(j, colID)).MEMO))
                Next

                Dim TINDAKAN As String = String.Empty

                For l As Integer = 0 To grvTerapi.RowCount - 2

                    TINDAKAN = ""

                    Dim dsDetail_TerapiTindakan = oKoding.GetStructureDetail_Terapi_Tindakan
                    With dsDetail_TerapiTindakan
                        .KDKODING = ds.KDKODING
                        .SEQ = l
                        .TERAPI = grvTerapi.GetRowCellValue(l, colKETERANGAN)

                        For Each xLoop In ListTindakan.Distinct
                            If xLoop.ToString.Contains(l & ".") Then
                                TINDAKAN = xLoop.Replace(l & ".", "")
                                '.TINDAKAN = xLoop.Replace(l & ".", "")
                            End If
                        Next

                        .TINDAKAN = TINDAKAN
                    End With

                    arrDetail_Terapi_Tindakan.Add(dsDetail_TerapiTindakan)
                Next
            End If


            If oFormMode = FORM_MODE.FORM_MODE_ADD Then
                Try
                    fn_Save = oKoding.InsertData(ds, arrDetail_DX, arrDetail_PJ, arrDetail_Terapi, dsDetail_DiagnosisUtama, arrDetail_DianosisPenyerta, arrDetail_Terapi_Tindakan)
                Catch ex As Exception
                    MsgBox("Simpan Data : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            ElseIf oFormMode = FORM_MODE.FORM_MODE_EDIT Then
                Try
                    fn_Save = oKoding.UpdateData(ds, arrDetail_DX, arrDetail_PJ, arrDetail_Terapi, dsDetail_DiagnosisUtama, arrDetail_DianosisPenyerta, arrDetail_Terapi_Tindakan)
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
#Region "Grid Method"
    'Private Sub grvDetail_CustomUnboundColumnData(ByVal sender As System.Object, ByVal e As DevExpress.XtraGrid.Views.Base.CustomColumnDataEventArgs) Handles grvDetail_DX.CustomUnboundColumnData
    '    If e.Column.Name = colKDDIAGNOSA.Name Then
    '        Dim oDiagnosa As New Master.clsDiagnosaICD10
    '        Try
    '            If grvDetail_DX.GetRowCellValue(e.ListSourceRowIndex, colKDDIAGNOSA) <> String.Empty Then
    '                e.Value = oDiagnosa.GetData(grvDetail_DX.GetRowCellValue(e.ListSourceRowIndex, colKDDIAGNOSA)).DESCRIPTION
    '            ElseIf grvDetail_DX.GetFocusedRowCellValue(colKDDIAGNOSA) <> String.Empty Then
    '                e.Value = oDiagnosa.GetData(grvDetail_DX.GetFocusedRowCellValue(colKDDIAGNOSA)).DESCRIPTION
    '            End If
    '        Catch oErr As Exception
    '            MsgBox("Load Detail : " & vbCrLf & oErr.Message, MsgBoxStyle.Critical, Me.Text)
    '        End Try
    '    End If
    'End Sub
    'Private Sub grvDetail_D9_CustomUnboundColumnData(ByVal sender As System.Object, ByVal e As DevExpress.XtraGrid.Views.Base.CustomColumnDataEventArgs) Handles grvDetail_D9.CustomUnboundColumnData
    '    If e.Column.Name = colTINDAKAN.Name Then
    '        Dim oTindakan As New Master.clsDiagnosaICD9
    '        Try
    '            If grvDetail_D9.GetRowCellValue(e.ListSourceRowIndex, colID) IsNot Nothing Then
    '                e.Value = oTindakan.GetData(grvDetail_D9.GetRowCellValue(e.ListSourceRowIndex, colID)).DESCRIPTION
    '            ElseIf grvDetail_D9.GetFocusedRowCellValue(colID) IsNot Nothing Then
    '                e.Value = oTindakan.GetData(grvDetail_D9.GetFocusedRowCellValue(colID)).DESCRIPTION
    '            End If
    '        Catch oErr As Exception
    '            MsgBox("Load Detail : " & vbCrLf & oErr.Message, MsgBoxStyle.Critical, Me.Text)
    '        End Try
    '    End If
    'End Sub
    Private Sub DeleteToolStripADDRESS_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DeleteToolStripMenuItem_ADDRESS.Click
        If oFormMode = FORM_MODE.FORM_MODE_VIEW Then Exit Sub
        grvDetail_DX.DeleteSelectedRows()
    End Sub
    Private Sub DeleteToolStripPJ_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DeleteToolStripMenuItem_PJ.Click
        If oFormMode = FORM_MODE.FORM_MODE_VIEW Then Exit Sub
        grvDetail_D9.DeleteSelectedRows()
    End Sub
    Private Sub ToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem1.Click
        If oFormMode = FORM_MODE.FORM_MODE_VIEW Then Exit Sub
        grvTerapi.DeleteSelectedRows()
    End Sub

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
                'Case Keys.F5
                '    If tabControl.SelectedTabPageIndex = 0 Then
                '        'btnTambah_ADDRESS_Click()
                '    ElseIf tabControl.SelectedTabPageIndex = 1 Then
                '        'btnTambahPJ_Click()
                '    End If
        End Select
    End Sub
    Private Sub btnSaveNew_Click() Handles btnSaveNew.ItemClick
        If fn_Validate() = False Then Exit Sub
        If MsgBox("Save " & txtNAME_DISPLAY.Text.Trim.ToUpper & "?", MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, Me.Text) = MsgBoxResult.No Then Exit Sub
        If fn_Save() = False Then
            MsgBox("Save gagal! Tolong kontak admin anda!", MsgBoxStyle.Exclamation, Me.Text)
        Else
            MsgBox("Save " & txtNAME_DISPLAY.Text.Trim.ToUpper & " success!", MsgBoxStyle.Information, Me.Text)
            sStatusSave = "NEW"
            Me.Close()
        End If
    End Sub
    Private Sub btnSaveClose_Click() Handles btnSaveClose.ItemClick
        If fn_Validate() = False Then Exit Sub
        If MsgBox("Save " & txtNAME_DISPLAY.Text.Trim.ToUpper & "?", MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, Me.Text) = MsgBoxResult.No Then Exit Sub
        If fn_Save() = False Then
            MsgBox("Save gagal! Tolong kontak admin anda!", MsgBoxStyle.Exclamation, Me.Text)
        Else
            MsgBox("Save " & txtNAME_DISPLAY.Text.Trim.ToUpper & " success!", MsgBoxStyle.Information, Me.Text)
            Me.Close()
        End If
    End Sub
    Private Sub btnClose_Click() Handles btnClose.ItemClick
        Me.Close()
    End Sub
#End Region
#Region "Lookup / Event"
    Private Sub fn_LoadDIGANOSA()
        Dim oDiagnosa As New Reference.clsDiagnosa
        Try
            grdKDDIAGNOSA.DataSource = oDiagnosa.GetData.Where(Function(x) x.ISACTIVE = True).ToList()
            grdKDDIAGNOSA.ValueMember = "KDDIAGNOSA"
            grdKDDIAGNOSA.DisplayMember = "MEMO"
        Catch oErr As Exception
            MsgBox("Load Diagnosa Data : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadTINDAKAN()
        Dim oTindakan As New Reference.clsProsedur
        Try
            grdKDDIAGNOSA9.DataSource = oTindakan.GetData.Where(Function(x) x.ISACTIVE = True).ToList()
            grdKDDIAGNOSA9.ValueMember = "KDPROSEDUR"
            grdKDDIAGNOSA9.DisplayMember = "MEMO"
        Catch oErr As Exception
            MsgBox("Load Tindakan Data : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    'Private Sub btnTambah_ADDRESS_Click() Handles btnTambah_ADDRESS.Click
    '    If oFormMode = FORM_MODE.FORM_MODE_VIEW Then Exit Sub
    '    Dim frmPopUpDiagnosa As New frmPopUpDiagnosa

    '    frmPopUpDiagnosa.fn_LoadMe(0)
    '    frmPopUpDiagnosa.ShowDialog(Me)

    '    If sFind1 <> String.Empty Then
    '        grvDetail_DX.AddNewRow()
    '        grvDetail_DX.SetFocusedRowCellValue(colKDDIAGNOSA, sFind1)
    '        grvDetail_DX.SetFocusedRowCellValue(colKET_DX, "-")
    '        grvDetail_DX.UpdateCurrentRow()
    '    End If

    '    sFind1 = String.Empty
    '    sFind2 = String.Empty
    'End Sub
    'Private Sub btnTambahPJ_Click() Handles btnTambahPJ.Click
    '    If oFormMode = FORM_MODE.FORM_MODE_VIEW Then Exit Sub
    '    Dim frmPopUpTindakan As New frmPopUpTindakan

    '    frmPopUpTindakan.fn_LoadMe(0)
    '    frmPopUpTindakan.ShowDialog(Me)

    '    If sFind1 <> "" Then
    '        grvDetail_D9.AddNewRow()
    '        grvDetail_D9.SetFocusedRowCellValue(colKDDIAGNOSA9, sFind3)
    '        grvDetail_D9.SetFocusedRowCellValue(colKET_D9, "-")
    '        grvDetail_D9.UpdateCurrentRow()
    '    End If

    '    sFind1 = String.Empty
    '    sFind2 = String.Empty
    '    sFind3 = String.Empty
    'End Sub
    Private Sub fn_LoadDataPendaftaran(ByVal sNoTransaksi As String)
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

            SQL = "SELECT B.KDCUSTOMER, B.NAME_DISPLAY, KDDEPARTMENT = C.NAME_DISPLAY, KDDOCTOR = D.NAME_DISPLAY "
            SQL &= "FROM S_PENDAFTARAN_H AS A "
            SQL &= "INNER JOIN M_CUSTOMER AS B "
            SQL &= "ON A.KDCUSTOMER = B.KDCUSTOMER "
            SQL &= "INNER JOIN M_DEPARTMENT AS C "
            SQL &= "ON A.KDDEPARTMENT = C.KDDEPARTMENT "
            SQL &= "INNER JOIN M_DOCTOR AS D "
            SQL &= "ON A.KDDOCTOR = D.KDDOCTOR "
            SQL &= "WHERE A.KDPENDAFTARAN = '" & sNoTransaksi & "' "


            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "S_PENDAFTARAN_H")

            For xloop As Integer = 0 To ds.Tables("S_PENDAFTARAN_H").Rows.Count - 1
                txtNAME_DISPLAY.Text = ds.Tables("S_PENDAFTARAN_H").Rows(xloop)("NAME_DISPLAY")
                txtNORM.Text = ds.Tables("S_PENDAFTARAN_H").Rows(xloop)("KDCUSTOMER")
                txtPOLI.Text = ds.Tables("S_PENDAFTARAN_H").Rows(xloop)("KDDEPARTMENT")
                txtDOKTER.Text = ds.Tables("S_PENDAFTARAN_H").Rows(xloop)("KDDOCTOR")
            Next

            If oConn.State = ConnectionState.Open Then
                oConn.Close()
            End If

        Catch oErr As Exception
            MsgBox("Preview Data : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub txtKDREG_EditValueChanged(sender As Object, e As EventArgs) Handles txtKDREG.EditValueChanged
        If txtKDREG.Text = String.Empty Then Exit Sub
        Try

            fn_LoadDataPendaftaran(txtKDREG.Text)

        Catch ex As Exception

        End Try
    End Sub
    Private Sub btnResep_Click(sender As Object, e As EventArgs) Handles btnResep.Click
        Try

            grvTerapi.OptionsSelection.MultiSelect = True
            grvTerapi.SelectAll()
            grvTerapi.DeleteSelectedRows()
            grvTerapi.OptionsSelection.MultiSelect = False

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
            SQL &= "Terapi = B.NAMAOBAT "
            SQL &= ",Signa = B.SIGNA "
            SQL &= "FROM S_REQ_RECIPE_H AS A "
            SQL &= "INNER JOIN S_REQ_RECIPE_D AS B "
            SQL &= "ON A.KDREQRECIPE = B.KDREQRECIPE "
            SQL &= "WHERE A.KDPENDAFTARAN = '" & txtKDREG.Text & "' "

            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "S_REQUEST")

            For xloop As Integer = 0 To ds.Tables("S_REQUEST").Rows.Count - 1
                grvTerapi.Focus()
                grvTerapi.AddNewRow()
                grvTerapi.SetFocusedRowCellValue(colKETERANGAN, ds.Tables("S_REQUEST").Rows(xloop)("Terapi") & " " & ds.Tables("S_REQUEST").Rows(xloop)("Signa"))
                grvTerapi.UpdateCurrentRow()
            Next

            If oConn.State = ConnectionState.Open Then
                oConn.Close()
            End If

        Catch oErr As Exception
            MsgBox("Preview Data : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub SimpleButton1_Click(sender As Object, e As EventArgs) Handles SimpleButton1.Click

        grvDetail_DX.OptionsSelection.MultiSelect = True
        grvDetail_DX.SelectAll()
        grvDetail_DX.DeleteSelectedRows()
        grvDetail_DX.OptionsSelection.MultiSelect = False

        grvDetail_D9.OptionsSelection.MultiSelect = True
        grvDetail_D9.SelectAll()
        grvDetail_D9.DeleteSelectedRows()
        grvDetail_D9.OptionsSelection.MultiSelect = False

        grvTerapi.OptionsSelection.MultiSelect = True
        grvTerapi.SelectAll()
        grvTerapi.DeleteSelectedRows()
        grvTerapi.OptionsSelection.MultiSelect = False

        'Dim ds = oKoding.GetDataByRM(txtNORM.Text)
        'If ds IsNot Nothing Then
        '    txtMEMO.Text = ds.DESCRIPTION

        '    Dim dsDiagnosa = oKoding.GetDataDetail_DX(ds.KDKODING)
        '    For Each xloop In dsDiagnosa
        '        grvDetail_DX.Focus()
        '        grvDetail_DX.AddNewRow()
        '        grvDetail_DX.SetFocusedRowCellValue(colKDDIAGNOSA, xloop.KDDIAGNOSA)
        '        grvDetail_DX.SetFocusedRowCellValue(colKET_DX, xloop.KETERANGAN)
        '        grvDetail_DX.UpdateCurrentRow()
        '    Next
        '    Dim dsDiagnosa9 = oKoding.GetDataDetail_DIX(ds.KDKODING)
        '    For Each xloop In dsDiagnosa9
        '        grvDetail_D9.Focus()
        '        grvDetail_D9.AddNewRow()
        '        grvDetail_D9.SetFocusedRowCellValue(colID, xloop.KDPROSEDUR)
        '        grvDetail_D9.SetFocusedRowCellValue(colKET_DX, xloop.KETERANGAN)
        '        grvDetail_D9.UpdateCurrentRow()
        '    Next
        '    Dim dsTerapi = oKoding.GetDataDetail_Terapi(ds.KDKODING)
        '    For Each xloop In dsTerapi
        '        grvTerapi.Focus()
        '        grvTerapi.AddNewRow()
        '        grvTerapi.SetFocusedRowCellValue(colKETERANGAN, xloop.KETERANGAN)
        '        grvTerapi.UpdateCurrentRow()
        '    Next
        'Else
        '    MsgBox("Belum Pernah Input Resume Rawat jalan", MsgBoxStyle.Exclamation, Me.Text)
        'End If
    End Sub
#End Region
End Class