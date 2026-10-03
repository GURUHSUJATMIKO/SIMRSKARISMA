Imports DataAccess
Imports UI.WIN.MAIN.My.Resources
Imports System.Linq

Public Class frmUser
#Region "Declaration"
    Private oFormMode As FORM_MODE = FORM_MODE.FORM_MODE_VIEW
    Private sNoId As String
    Private isLoad As Boolean = False
    Private oUser As New Setting.clsUser
#End Region
#Region "Function"
    Public Sub LoadMe(ByVal FormMode As Integer, Optional ByVal NoId As String = "")
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
            Me.Text = User.TITLE

            lKDUSER.Text = User.KDUSER
            lPASSWORD1.Text = User.PASSWORD1
            lPASSWORD2.Text = User.PASSWORD2
            lKDOTORITY.Text = User.KDOTORITY
            chkISACTIVE.Text = User.ISACTIVE

            btnSaveNew.Caption = Caption.FormSaveNew
            btnSaveClose.Caption = Caption.FormSaveClose
            btnClose.Caption = Caption.FormClose
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub Form_Closing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        sCode = txtKDUSER.Text.Trim.ToUpper
    End Sub
    Private Sub fn_ChangeFormState()
        fn_LoadKDOTORITY()
        fn_LoadWarehouse()
        fn_LoadItem_L1()

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
                txtKDUSER.Properties.ReadOnly = True
            Case Else
                fn_ViewMode(True)
        End Select
    End Sub
    Private Sub fn_ViewMode(ByVal Status As Boolean)
        btnSaveNew.Enabled = Not Status
        btnSaveClose.Enabled = Not Status

        txtKDUSER.Properties.ReadOnly = Status
        txtNIK.Properties.ReadOnly = Status
        txtPASSWORD1.Properties.ReadOnly = Status
        txtPASSWORD2.Properties.ReadOnly = Status
        grdKDOTORITY.Properties.ReadOnly = Status
        chkISACTIVE.Properties.ReadOnly = Status

        grvDetail.OptionsBehavior.ReadOnly = Status
        grvDetail_Tarif.OptionsBehavior.ReadOnly = Status
    End Sub
    Private Sub fn_EmptyMe()
        txtKDUSER.ResetText()
        txtNIK.ResetText()
        txtPASSWORD1.ResetText()
        txtPASSWORD2.ResetText()
        grdKDOTORITY.ResetText()
        chkISACTIVE.Checked = True
    End Sub
    Private Sub fn_LoadData()
        Try
            ' ***** HEADER *****
            Dim ds = oUser.GetData(sNoId)

            With ds
                txtNIK.Text = .NIK
                txtKDUSER.Text = .KDUSER
                txtPASSWORD1.Text = .PASSWORD
                txtPASSWORD2.Text = .PASSWORD
                grdKDOTORITY.Text = .KDOTORITY
                chkISACTIVE.Checked = .ISACTIVE
                BindingSource.DataSource = oUser.GetDataDetail.Where(Function(x) x.KDUSER = sNoId).OrderBy(Function(x) x.SEQ).ToList()
                grdDetail.DataSource = BindingSource
                BindingSourceT.DataSource = oUser.GetDataDetailT.Where(Function(x) x.KDUSER = sNoId).OrderBy(Function(x) x.SEQ).ToList()
                grdDetail_Tarif.DataSource = BindingSourceT
            End With
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Function fn_Validate() As Boolean
        Try
            fn_Validate = True

            If txtKDUSER.ErrorIconAlignment = ErrorIconAlignment.MiddleRight Then
                txtKDUSER.ErrorText = Statement.ErrorRequired
                txtKDUSER.Focus()
                fn_Validate = False
                Exit Function
            End If
            If txtNIK.ErrorIconAlignment = ErrorIconAlignment.MiddleRight Then
                txtNIK.ErrorText = Statement.ErrorRequired
                txtNIK.Focus()
                fn_Validate = False
                Exit Function
            End If
            If txtPASSWORD1.Text = String.Empty Then
                txtPASSWORD1.ErrorText = Statement.ErrorRequired
                txtPASSWORD1.Focus()
                fn_Validate = False
                Exit Function
            End If
            If txtPASSWORD2.Text = String.Empty Then
                txtPASSWORD2.ErrorText = Statement.ErrorRequired
                txtPASSWORD2.Focus()
                fn_Validate = False
                Exit Function
            End If
            If txtPASSWORD1.Text <> txtPASSWORD2.Text Then
                txtPASSWORD2.ErrorText = Statement.ErrorPasswordRepeat
                txtPASSWORD2.Focus()
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
            Dim ds = oUser.GetStructureHeader
            With ds
                .KDUSER = txtKDUSER.Text.Trim.ToUpper
                .NIK = txtNIK.Text.ToString.Trim
                .KDSERVER = sKDSERVER
                .KDDATABASE = sKDDATABASE
                .KDSERVER_TAX = sKDSERVER_TAX
                .KDDATABASE_TAX = sKDDATABASE_TAX
                .KDOTORITY = grdKDOTORITY.EditValue
                .KDCOMPANY = sKDCOMPANY
                .PASSWORD = txtPASSWORD1.Text.Trim
                .ISACTIVE = chkISACTIVE.Checked
            End With
            ' ***** DETIL *****
            Dim arrDetail = oUser.GetStructureDetailList
            For i As Integer = 0 To grvDetail.RowCount - 2
                Dim dsDetail = oUser.GetStructureDetail
                With dsDetail
                    .KDUSER = ds.KDUSER
                    .SEQ = i
                    .KDWAREHOUSE = grvDetail.GetRowCellValue(i, colKDWAREHOUSE)
                    .ISDEFAULT = grvDetail.GetRowCellValue(i, colISDEFAULT)
                    .REMARKS = ""
                End With
                arrDetail.Add(dsDetail)
            Next
            ' ***** TARIF *****
            Dim arrDetailT = oUser.GetStructureDetaiTlList
            For i As Integer = 0 To grvDetail_Tarif.RowCount - 2
                Dim dsDetail = oUser.GetStructureDetailT
                With dsDetail
                    .KDUSER = ds.KDUSER
                    .SEQ = i
                    .KDITEM_L1 = grvDetail_Tarif.GetRowCellValue(i, colKDITEM_L1)
                    .ISDEFAULT = grvDetail_Tarif.GetRowCellValue(i, colISDEFAULTT)
                    .REMARKS = ""
                End With
                arrDetailT.Add(dsDetail)
            Next
            If oFormMode = FORM_MODE.FORM_MODE_ADD Then
                Try
                    fn_Save = oUser.InsertData(ds, arrDetail, arrDetailT)

                Catch oErr As Exception
                    MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            ElseIf oFormMode = FORM_MODE.FORM_MODE_EDIT Then
                Try
                    fn_Save = oUser.UpdateData(ds, arrDetail, arrDetailT)

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
#Region "Command Button"
    Private Sub frmUser_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
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
    Private Sub DeleteToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DeleteToolStripMenuItem.Click
        If oFormMode = FORM_MODE.FORM_MODE_VIEW Then Exit Sub
        grvDetail.DeleteSelectedRows()
    End Sub
    Private Sub ToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem1.Click
        If oFormMode = FORM_MODE.FORM_MODE_VIEW Then Exit Sub
        grvDetail_Tarif.DeleteSelectedRows()
    End Sub
    Private Sub fn_LoadKDOTORITY()
        Dim oOTORITY As New Setting.clsOtority
        Try
            grdKDOTORITY.Properties.DataSource = oOTORITY.GetData.Where(Function(x) x.ISACTIVE = True).ToList()
            grdKDOTORITY.Properties.ValueMember = "KDOTORITY"
            grdKDOTORITY.Properties.DisplayMember = "KDOTORITY"
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub grdKDOTORITY_KeyDown(sender As System.Object, e As System.Windows.Forms.KeyEventArgs) Handles grdKDOTORITY.KeyDown
        If e.KeyCode = Keys.Delete Then
            grdKDOTORITY.ResetText()
        End If
    End Sub
    Private Sub fn_LoadWarehouse()
        Dim oWarehouse As New Reference.clsWarehouse
        Try
            grdKDWAREHOUSE.DataSource = oWarehouse.GetData.Where(Function(x) x.ISACTIVE = True).ToList()
            grdKDWAREHOUSE.ValueMember = "KDWAREHOUSE"
            grdKDWAREHOUSE.DisplayMember = "NAME_DISPLAY"
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadItem_L1()
        Dim oItem_L1 As New Reference.clsItem_L1
        Try
            grdITEM_L1.DataSource = oItem_L1.GetData.Where(Function(x) x.ISACTIVE = True).ToList()
            grdITEM_L1.ValueMember = "KDITEM_L1"
            grdITEM_L1.DisplayMember = "MEMO"
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
#End Region
End Class