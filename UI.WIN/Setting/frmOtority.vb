Imports DataAccess
Imports UI.WIN.MAIN.My.Resources
Imports System.Linq

Public Class frmOtority
#Region "Declaration"
    Private oFormMode As FORM_MODE = FORM_MODE.FORM_MODE_VIEW
    Private sNoId As String
    Private isLoad As Boolean = False
    Private oOtority As New Setting.clsOtority
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
            Me.Text = Otority.TITLE

            lKDOTORITY.Text = Otority.KDOTORITY

            tab1.Text = Otority.TAB_DETAIL
            tab2.Text = Otority.TAB_DESCRIPTION

            grvDetail.Columns("MODUL").Caption = Otority.DETAIL_MODUL
            grvDetail.Columns("MENUNAME").Caption = Otority.DETAIL_MENUNAME
            grvDetail.Columns("ISADD").Caption = Otority.DETAIL_ISADD
            grvDetail.Columns("ISUPDATE").Caption = Otority.DETAIL_ISUPDATE
            grvDetail.Columns("ISDELETE").Caption = Otority.DETAIL_ISDELETE
            grvDetail.Columns("ISPRINT").Caption = Otority.DETAIL_ISPRINT
            grvDetail.Columns("ISVIEW").Caption = Otority.DETAIL_ISVIEW

            btnSaveNew.Caption = Caption.FormSaveNew
            btnSaveClose.Caption = Caption.FormSaveClose
            btnClose.Caption = Caption.FormClose
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub Form_Closing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        sCode = txtKDOTORITY.Text.Trim.ToUpper
    End Sub
    Private Sub fn_ChangeFormState()
        Select Case oFormMode
            Case FORM_MODE.FORM_MODE_VIEW
                fn_ViewMode(True)
                fn_LoadData()
            Case FORM_MODE.FORM_MODE_ADD
                fn_ViewMode(False)
                fn_EmptyMe()
                fn_LoadOtority()
            Case FORM_MODE.FORM_MODE_EDIT
                fn_ViewMode(False)
                fn_LoadData()
                txtKDOTORITY.Properties.ReadOnly = True
            Case Else
                fn_ViewMode(True)
        End Select
    End Sub
    Private Sub fn_ViewMode(ByVal Status As Boolean)
        btnSaveNew.Enabled = Not Status
        btnSaveClose.Enabled = Not Status

        txtKDOTORITY.Properties.ReadOnly = Status
        txtDESCRIPTION.Properties.ReadOnly = Status

        grvDetail.OptionsBehavior.ReadOnly = Status
    End Sub
    Private Sub fn_EmptyMe()
        txtKDOTORITY.ResetText()
        txtDESCRIPTION.ResetText()
    End Sub
    Private Sub fn_LoadData()
        Try
            ' ***** HEADER *****
            Dim ds = oOtority.GetData(sNoId)

            With ds
                txtKDOTORITY.Text = .KDOTORITY
                txtDESCRIPTION.Text = .DESCRIPTION

                bindingSource.DataSource = oOtority.GetDataDetail.Where(Function(x) x.KDOTORITY = sNoId).OrderBy(Function(x) x.MENUNAME).ToList()
                grdDetail.DataSource = bindingSource
            End With
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Function fn_Validate() As Boolean
        Try
            fn_Validate = True

            If txtKDOTORITY.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                txtKDOTORITY.ErrorText = Statement.ErrorRequired

                txtKDOTORITY.Focus()
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
            Dim ds = oOtority.GetStructureHeader
            With ds
                .KDOTORITY = txtKDOTORITY.Text.Trim.ToUpper
                .DESCRIPTION = txtDESCRIPTION.Text.Trim.ToUpper
                .ISACTIVE = True
            End With

            ' ***** DETIL *****
            Dim arrDetail = oOtority.GetStructureDetailList
            For i As Integer = 0 To grvDetail.RowCount - 2
                Dim dsDetail = oOtority.GetStructureDetail
                With dsDetail
                    .KDOTORITY = ds.KDOTORITY
                    .MODUL = IIf(String.IsNullOrEmpty(grvDetail.GetRowCellValue(i, colMODUL)), grvDetail.GetRowCellValue(i, colMENUNAME), grvDetail.GetRowCellValue(i, colMODUL))
                    .MENUNAME = grvDetail.GetRowCellValue(i, colMENUNAME)
                    .ISADD = CBool(grvDetail.GetRowCellValue(i, colISADD))
                    .ISUPDATE = CBool(grvDetail.GetRowCellValue(i, colISUPDATE))
                    .ISDELETE = CBool(grvDetail.GetRowCellValue(i, colISDELETE))
                    .ISPRINT = CBool(grvDetail.GetRowCellValue(i, colISPRINT))
                    .ISVIEW = CBool(grvDetail.GetRowCellValue(i, colISVIEW))
                End With

                arrDetail.Add(dsDetail)

            Next

            If oFormMode = FORM_MODE.FORM_MODE_ADD Then
                Try
                    fn_Save = oOtority.InsertData(ds, arrDetail)
                Catch oErr As Exception
                    MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            ElseIf oFormMode = FORM_MODE.FORM_MODE_EDIT Then
                Try
                    fn_Save = oOtority.UpdateData(ds, arrDetail)
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
    Private Sub grvDetail_CellValueChanged(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs) Handles grvDetail.CellValueChanging
        If e.Column.Name = colISVIEW.Name Then
            Try
                If grvDetail.GetFocusedRowCellValue(colMODUL) IsNot Nothing Then
                    If e.Value = True Then
                        grvDetail.SetFocusedRowCellValue(colISADD, True)
                        grvDetail.SetFocusedRowCellValue(colISUPDATE, True)
                        grvDetail.SetFocusedRowCellValue(colISDELETE, True)
                        grvDetail.SetFocusedRowCellValue(colISPRINT, True)
                    Else
                        grvDetail.SetFocusedRowCellValue(colISADD, False)
                        grvDetail.SetFocusedRowCellValue(colISUPDATE, False)
                        grvDetail.SetFocusedRowCellValue(colISDELETE, False)
                        grvDetail.SetFocusedRowCellValue(colISPRINT, False)
                    End If

                    grvDetail.UpdateCurrentRow()
                End If
            Catch oErr As Exception
                MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            End Try
        End If
    End Sub
#End Region
#Region "Command Button"
    Private Sub frmOtority_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
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
    Public Sub fn_LoadOtority()
        'Dim ds = oOtority.GetDataDetail("DEMO")

        'grvDetail.Focus()
        'For Each iLoop In ds
        '    grvDetail.AddNewRow()

        '    grvDetail.SetFocusedRowCellValue(colMODUL, iLoop.MODUL)
        '    grvDetail.SetFocusedRowCellValue(colMENUNAME, iLoop.MENUNAME)
        '    grvDetail.SetFocusedRowCellValue(colISADD, iLoop.ISADD)
        '    grvDetail.SetFocusedRowCellValue(colISUPDATE, iLoop.ISUPDATE)
        '    grvDetail.SetFocusedRowCellValue(colISDELETE, iLoop.ISDELETE)
        '    grvDetail.SetFocusedRowCellValue(colISPRINT, iLoop.ISPRINT)
        '    grvDetail.SetFocusedRowCellValue(colISVIEW, iLoop.ISVIEW)

        '    grvDetail.UpdateCurrentRow()
        'Next
    End Sub
#End Region

End Class