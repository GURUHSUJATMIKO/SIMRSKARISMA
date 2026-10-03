Imports DataAccess
Imports UI.WIN.MAIN.My.Resources
Imports System.Linq

Public Class frmDaftar_L2
#Region "Declaration"
    Private oFormMode As FORM_MODE = FORM_MODE.FORM_MODE_VIEW
    Private sNoId As String
    Private isLoad As Boolean = False
    Private oDAFTAR_L2 As New Reference.clsDaftar_L2
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
            'Me.Text = DAFTAR_L2.TITLE
            Me.Text = sDaftar_L2

            lDAFTAR_L2_1.Text = sDaftar_L2_1 & " *"
            lDAFTAR_L2_2.Text = sDaftar_L2_2 & " *"
            lDAFTAR_L2.Text = Daftar_L2.MEMO & " *"
            chkISACTIVE.Text = Daftar_L2.ISACTIVE
            chkISDEFAULT.Text = Daftar_L2.ISDEFAULT

            btnSaveNew.Caption = Caption.FormSaveNew
            btnSaveClose.Caption = Caption.FormSaveClose
            btnClose.Caption = Caption.FormClose
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
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

        txtMEMO_1.Properties.ReadOnly = Status
        txtMEMO_2.Properties.ReadOnly = Status
        txtMEMO.Properties.ReadOnly = Status
        chkISACTIVE.Properties.ReadOnly = Status
        chkISDEFAULT.Properties.ReadOnly = Status
    End Sub
    Private Sub fn_EmptyMe()
        txtMEMO_1.ResetText()
        txtMEMO_2.ResetText()
        txtMEMO.ResetText()
        chkISACTIVE.Checked = True
        chkISDEFAULT.Checked = False
    End Sub
    Private Sub fn_LoadData()
        Try
            Dim ds = oDAFTAR_L2.GetData(sNoId)

            With ds
                txtMEMO_1.Text = .MEMO_1
                txtMEMO_2.Text = .MEMO_2
                txtMEMO.Text = .MEMO
                chkISACTIVE.Checked = .ISACTIVE
                chkISDEFAULT.Checked = .ISDEFAULT
            End With
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Function fn_Validate() As Boolean
        Try
            fn_Validate = True
            If txtMEMO_1.Text = String.Empty Then
                txtMEMO_1.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                txtMEMO_1.ErrorText = Statement.ErrorRequired

                txtMEMO_1.Focus()
                fn_Validate = False
                Exit Function
            End If
            If txtMEMO_2.Text = String.Empty Then
                txtMEMO_2.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                txtMEMO_2.ErrorText = Statement.ErrorRequired

                txtMEMO_2.Focus()
                fn_Validate = False
                Exit Function
            End If
            If txtMEMO.Text = String.Empty Then
                txtMEMO.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                txtMEMO.ErrorText = Statement.ErrorRequired

                txtMEMO.Focus()
                fn_Validate = False
                Exit Function
            End If
            'If oFormMode = FORM_MODE.FORM_MODE_ADD Then
            '    If oDAFTAR_L2.IsExist(txtMEMO.Text.ToUpper.Trim) = True Then
            '        txtMEMO.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
            '        txtMEMO.ErrorText = Statement.ErrorRegistered

            '        txtMEMO.Focus()
            '        fn_Validate = False
            '        Exit Function
            '    End If
            'Else
            '    If txtMEMO.Text.Trim.ToUpper <> oDAFTAR_L2.GetData(sNoId).MEMO Then
            '        If oDAFTAR_L2.IsExist(txtMEMO.Text.ToUpper.Trim) = True Then
            '            txtMEMO.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
            '            txtMEMO.ErrorText = Statement.ErrorRegistered

            '            txtMEMO.Focus()
            '            fn_Validate = False
            '            Exit Function
            '        End If
            '    End If
            'End If
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Function
    Private Function fn_Save() As Boolean
        Try
            ' ***** HEADER *****
            Dim ds = oDAFTAR_L2.GetStructureHeader
            With ds
                Try
                    .DATECREATED = oDAFTAR_L2.GetData(sNoId).DATECREATED
                Catch oErr As Exception
                    .DATECREATED = Now
                End Try
                .DATEUPDATED = Now

                .KDDAFTAR_L2 = sNoId
                .MEMO_1 = txtMEMO_1.Text.Trim.ToUpper
                .MEMO_2 = txtMEMO_2.Text.Trim.ToUpper
                .MEMO = txtMEMO.Text.Trim.ToUpper
                .ISACTIVE = chkISACTIVE.Checked
                .ISDEFAULT = chkISDEFAULT.Checked
            End With

            If oFormMode = FORM_MODE.FORM_MODE_ADD Then
                Try
                    fn_Save = oDAFTAR_L2.InsertData(ds)
                Catch oErr As Exception
                    MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            ElseIf oFormMode = FORM_MODE.FORM_MODE_EDIT Then
                Try
                    fn_Save = oDAFTAR_L2.UpdateData(ds)
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