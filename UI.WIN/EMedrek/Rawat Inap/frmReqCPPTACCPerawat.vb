Imports DataAccess
Imports UI.WIN.MAIN.My.Resources
Imports System.Linq

Public Class frmReqCPPTACCPerawat
#Region "Declaration"
    Private oFormMode As FORM_MODE = FORM_MODE.FORM_MODE_VIEW
    Private sNoId As String
    Private isLoad As Boolean = False
    Private oReqCPPTACCPerawat As New Inventory.clsReqCPPTACCPerawat

#End Region
#Region "Function"
    Public Sub LoadMe(ByVal FormMode As Integer, ByVal sCATEGORY As String, ByVal sKDCPPT As String, Optional ByVal NoId As String = "")
        oFormMode = FormMode
        sNoId = NoId
    End Sub
    Private Sub Form_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        fn_ChangeFormState()
        isLoad = True
    End Sub
    Private Sub Form_Closing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        sCode = txtMEMO.Text.Trim.ToUpper
    End Sub
    Private Sub fn_ChangeFormState()
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

        If oFormMode = FORM_MODE.FORM_MODE_ADD Then
            txtCODE.Properties.ReadOnly = False
        Else
            txtCODE.Properties.ReadOnly = True
        End If
        txtMEMO.Properties.ReadOnly = Status
    End Sub
    Private Sub fn_EmptyMe()
        txtCODE.Text = "<---AUTO--->"
        txtMEMO.ResetText()
    End Sub
    Private Sub fn_LoadData()
        'Try
        '    Dim ds = oReqCPPTACCPerawat.GetData(sNoId)

        '    With ds
        '        txtCODE.Text = sNoId
        '        txtMEMO.Text = .MEMO
        '    End With
        'Catch oErr As Exception
        '    MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        'End Try
    End Sub
    Private Function fn_Validate() As Boolean
        'Try
        '    fn_Validate = True
        '    If txtMEMO.Text = String.Empty Then
        '        txtMEMO.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
        '        txtMEMO.ErrorText = Statement.ErrorRequired

        '        txtMEMO.Focus()
        '        fn_Validate = False
        '        Exit Function
        '    End If
        '    If oFormMode = FORM_MODE.FORM_MODE_ADD Then
        '        If oReqCPPTACCPerawat.IsExist(txtMEMO.Text.ToUpper.Trim) = True Then
        '            txtMEMO.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
        '            txtMEMO.ErrorText = Statement.ErrorRegistered

        '            txtMEMO.Focus()
        '            fn_Validate = False
        '            Exit Function
        '        End If
        '    Else
        '        If txtMEMO.Text.Trim.ToUpper <> oReqCPPTACCPerawat.GetData(sNoId).MEMO.ToString.Trim.ToUpper Then
        '            If oReqCPPTACCPerawat.IsExist(txtMEMO.Text.ToUpper.Trim) = True Then
        '                txtMEMO.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
        '                txtMEMO.ErrorText = Statement.ErrorRegistered

        '                txtMEMO.Focus()
        '                fn_Validate = False
        '                Exit Function
        '            End If
        '        End If
        '    End If
        'Catch oErr As Exception
        '    MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        'End Try
    End Function
    Private Function fn_Save() As Boolean
        'Try
        '    ' ***** HEADER *****
        '    Dim ds = oReqCPPTACCPerawat.GetStructureHeader
        '    With ds
        '        Try
        '            .DATECREATED = oReqCPPTACCPerawat.GetData(sNoId).DATECREATED
        '        Catch oErr As Exception
        '            .DATECREATED = Now
        '        End Try
        '        .DATEUPDATED = Now

        '        .KDReqCPPTACCPerawat = IIf(txtCODE.Text = "<---AUTO--->", sNoId, txtCODE.Text)
        '        .MEMO = txtMEMO.Text
        '        .ISACTIVE = chkISACTIVE.Checked
        '        .ISDEFAULT = chkISDEFAULT.Checked
        '    End With

        '    If oFormMode = FORM_MODE.FORM_MODE_ADD Then
        '        Try
        '            fn_Save = oReqCPPTACCPerawat.InsertData(ds)
        '        Catch oErr As Exception
        '            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        '        End Try
        '    ElseIf oFormMode = FORM_MODE.FORM_MODE_EDIT Then
        '        Try
        '            fn_Save = oReqCPPTACCPerawat.UpdateData(ds)
        '        Catch oErr As Exception
        '            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        '        End Try
        '    End If
        'Catch oErr As Exception
        '    MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        '    fn_Save = False
        'End Try
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

#End Region
End Class