Imports DataAccess
Imports UI.WIN.MAIN.My.Resources
Imports System.Linq

Public Class frmCustomerUnit
#Region "Declaration"
    Private oFormMode As FORM_MODE = FORM_MODE.FORM_MODE_VIEW
    Private sNoId As String
    Private isLoad As Boolean = False
    Private oCustomerUnit As New Reference.clsCustomerUnit
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
            Me.Text = CustomerUnit.TITLE

            lNAME_DISPLAY.Text = CustomerUnit.NAME_DISPLAY & " *"
            chkISACTIVE.Text = CustomerUnit.ISACTIVE

            lPHONE.Text = CustomerUnit.PHONE
            lFAX.Text = CustomerUnit.FAX
            lMOBILE.Text = CustomerUnit.MOBILE
            lOTHER.Text = CustomerUnit.OTHER
            lEMAIL.Text = CustomerUnit.EMAIL
            lWEBSITE.Text = CustomerUnit.WEBSITE

            lBILL_STREET.Text = CustomerUnit.BILL_STREET
            lBILL_CITY.Text = CustomerUnit.BILL_CITY
            lBILL_STATE.Text = CustomerUnit.BILL_STATE
            lBILL_ZIP.Text = CustomerUnit.BILL_ZIP
            lBILL_COUNTRY.Text = CustomerUnit.BILL_COUNTRY
            tab1.Text = CustomerUnit.TAB_CONTACT
            tab2.Text = CustomerUnit.TAB_BILL
            tab4.Text = CustomerUnit.TAB_OTHER

            btnSaveNew.Caption = Caption.FormSaveNew
            btnSaveClose.Caption = Caption.FormSaveClose
            btnClose.Caption = Caption.FormClose
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub Form_Closing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        sCode = txtCODE.Text.Trim.ToUpper
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

        txtNAME_DISPLAY.Properties.ReadOnly = Status
        chkISACTIVE.Properties.ReadOnly = Status
        txtPHONE.Properties.ReadOnly = Status
        txtFAX.Properties.ReadOnly = Status
        txtMOBILE.Properties.ReadOnly = Status
        txtEMAIL.Properties.ReadOnly = Status
        txtOTHER.Properties.ReadOnly = Status
        txtWEBSITE.Properties.ReadOnly = Status
        txtBILL_STREET.Properties.ReadOnly = Status
        txtBILL_CITY.Properties.ReadOnly = Status
        txtBILL_STATE.Properties.ReadOnly = Status
        txtBILL_ZIP.Properties.ReadOnly = Status
        txtBILL_COUNTRY.Properties.ReadOnly = Status
        txtMEMO.Properties.ReadOnly = Status
    End Sub
    Private Sub fn_EmptyMe()
        txtCODE.Text = "<--- AUTO --->"
        txtNAME_DISPLAY.ResetText()
        chkISACTIVE.Checked = True
        txtPHONE.ResetText()
        txtFAX.ResetText()
        txtMOBILE.ResetText()
        txtEMAIL.ResetText()
        txtOTHER.ResetText()
        txtWEBSITE.ResetText()
        txtBILL_STREET.ResetText()
        txtBILL_CITY.ResetText()
        txtBILL_STATE.ResetText()
        txtBILL_ZIP.ResetText()
        txtBILL_COUNTRY.ResetText()
        txtMEMO.ResetText()
    End Sub
    Private Sub fn_LoadData()
        Try
            Dim ds = oCustomerUnit.GetData(sNoId)

            With ds
                txtCODE.Text = sNoId
                txtNAME_DISPLAY.Text = .NAME_DISPLAY
                chkISACTIVE.Checked = .ISACTIVE
                txtPHONE.Text = .PHONE
                txtFAX.Text = .FAX
                txtMOBILE.Text = .MOBILE
                txtEMAIL.Text = .EMAIL
                txtOTHER.Text = .OTHER
                txtWEBSITE.Text = .WEBSITE
                txtBILL_STREET.Text = .BILL_STREET
                txtBILL_CITY.Text = .BILL_CITY
                txtBILL_STATE.Text = .BILL_STATE
                txtBILL_ZIP.Text = .BILL_ZIP
                txtBILL_COUNTRY.Text = .BILL_COUNTRY
                txtMEMO.Text = .MEMO
            End With
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Function fn_Validate() As Boolean
        Try
            fn_Validate = True
            If txtNAME_DISPLAY.Text = String.Empty Then
                txtNAME_DISPLAY.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                txtNAME_DISPLAY.ErrorText = Statement.ErrorRequired

                txtNAME_DISPLAY.Focus()
                fn_Validate = False
                Exit Function
            End If
            'If oFormMode = FORM_MODE.FORM_MODE_ADD Then
            '    If oCustomerUnit.IsExist(txtNAME_DISPLAY.Text.ToUpper.Trim) = True Then
            '        txtNAME_DISPLAY.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
            '        txtNAME_DISPLAY.ErrorText = Statement.ErrorRegistered

            '        txtNAME_DISPLAY.Focus()
            '        fn_Validate = False
            '        Exit Function
            '    End If
            'Else
            '    If txtNAME_DISPLAY.Text.Trim.ToUpper <> oCustomerUnit.GetData(sNoId).NAME_DISPLAY Then
            '        If oCustomerUnit.IsExist(txtNAME_DISPLAY.Text.ToUpper.Trim) = True Then
            '            txtNAME_DISPLAY.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
            '            txtNAME_DISPLAY.ErrorText = Statement.ErrorRegistered

            '            txtNAME_DISPLAY.Focus()
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
            Dim ds = oCustomerUnit.GetStructureHeader
            With ds
                Try
                    .DATECREATED = oCustomerUnit.GetData(sNoId).DATECREATED
                Catch oErr As Exception
                    .DATECREATED = Now
                End Try
                .DATEUPDATED = Now

                .KDCUSTOMER_UNIT = sNoId
                .NAME_DISPLAY = txtNAME_DISPLAY.Text.Trim.ToUpper
                .PHONE = txtPHONE.Text.Trim.ToUpper
                .FAX = txtFAX.Text.Trim.ToUpper
                .MOBILE = txtMOBILE.Text.Trim.ToUpper
                .EMAIL = txtEMAIL.Text.Trim.ToUpper
                .OTHER = txtOTHER.Text.Trim.ToUpper
                .WEBSITE = txtWEBSITE.Text.Trim.ToUpper
                .BILL_STREET = txtBILL_STREET.Text.Trim.ToUpper
                .BILL_CITY = txtBILL_CITY.Text.Trim.ToUpper
                .BILL_STATE = txtBILL_STATE.Text.Trim.ToUpper
                .BILL_ZIP = txtBILL_ZIP.Text.Trim.ToUpper
                .BILL_COUNTRY = txtBILL_COUNTRY.Text.Trim.ToUpper
                .MEMO = txtMEMO.Text.Trim.ToUpper
                .ISACTIVE = chkISACTIVE.Checked
            End With

            If oFormMode = FORM_MODE.FORM_MODE_ADD Then
                Try
                    txtCODE.Text = oCustomerUnit.InsertData(ds)
                    If txtCODE.Text = "" Then
                        fn_Save = False
                    Else
                        fn_Save = True
                    End If
                Catch oErr As Exception
                    MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            ElseIf oFormMode = FORM_MODE.FORM_MODE_EDIT Then
                Try
                    fn_Save = oCustomerUnit.UpdateData(ds)
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