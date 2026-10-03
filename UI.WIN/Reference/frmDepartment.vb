Imports DataAccess
Imports UI.WIN.MAIN.My.Resources
Imports System.Linq
Imports Newtonsoft.Json.Linq

Public Class frmDepartment
#Region "Declaration"
    Private oFormMode As FORM_MODE = FORM_MODE.FORM_MODE_VIEW
    Private sNoId As String
    Private isLoad As Boolean = False
    Private oDepartment As New Reference.clsDepartment
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
            Me.Text = Department.TITLE

            lCODE.Text = Department.KDDEPARTMENT & " *"
            lKDDEPARTMENT_BPJS.Text = Department.KDDEPARTMENT_BPJS
            lNAME_DISPLAY.Text = Department.NAME_DISPLAY & " *"
            chkISACTIVE.Text = Department.ISACTIVE

            lPHONE.Text = Department.PHONE
            lFAX.Text = Department.FAX
            lMOBILE.Text = Department.MOBILE
            lOTHER.Text = Department.OTHER
            lEMAIL.Text = Department.EMAIL
            lWEBSITE.Text = Department.WEBSITE

            lBILL_STREET.Text = Department.BILL_STREET
            lBILL_CITY.Text = Department.BILL_CITY
            lBILL_STATE.Text = Department.BILL_STATE
            lBILL_ZIP.Text = Department.BILL_ZIP
            lBILL_COUNTRY.Text = Department.BILL_COUNTRY

            tab1.Text = Department.TAB_CONTACT
            tab2.Text = Department.TAB_BILL
            tab4.Text = Department.TAB_OTHER

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
        btnRuanganBaru.Enabled = Not Status
        btnHapusRuangan.Enabled = Not Status

        If oFormMode = FORM_MODE.FORM_MODE_ADD Then
            txtCODE.Properties.ReadOnly = False
        Else
            txtCODE.Properties.ReadOnly = True
        End If
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
        chkISEKSEKUTIF.Properties.ReadOnly = Status
        chkISKATARAK.Properties.ReadOnly = Status
        txtKDDEPARTMENT_BPJS.Properties.ReadOnly = Status
    End Sub
    Private Sub fn_EmptyMe()
        txtCODE.Text = "<---AUTO--->"
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
        txtKDDEPARTMENT_BPJS.ResetText()
    End Sub
    Private Sub fn_LoadData()
        Try
            Dim ds = oDepartment.GetData(sNoId)

            With ds
                txtCODE.Text = .KDDEPARTMENT
                txtKDDEPARTMENT_BPJS.Text = .KDDEPARTMENT_BPJS
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
                chkISEKSEKUTIF.Checked = .ISEKSEKUTIF
                chkISKATARAK.Checked = .ISKATARAK
                tabControl.SelectedTabPage = tab4
                tabControl.SelectedTabPage = tab2
                tabControl.SelectedTabPage = tab1
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
            If txtKDDEPARTMENT_BPJS.Text = String.Empty Then
                txtKDDEPARTMENT_BPJS.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                txtKDDEPARTMENT_BPJS.ErrorText = Statement.ErrorRequired

                txtKDDEPARTMENT_BPJS.Focus()
                fn_Validate = False
                Exit Function
            End If

            If oFormMode = FORM_MODE.FORM_MODE_ADD Then
                If oDepartment.IsExist(txtNAME_DISPLAY.Text.ToUpper.Trim) = True Then
                    txtNAME_DISPLAY.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                    txtNAME_DISPLAY.ErrorText = Statement.ErrorRegistered

                    txtNAME_DISPLAY.Focus()
                    fn_Validate = False
                    Exit Function
                End If
            Else
                If txtNAME_DISPLAY.Text.Trim <> oDepartment.GetData(sNoId).NAME_DISPLAY Then
                    If oDepartment.IsExist(txtNAME_DISPLAY.Text.ToUpper.Trim) = True Then
                        txtNAME_DISPLAY.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                        txtNAME_DISPLAY.ErrorText = Statement.ErrorRegistered

                        txtNAME_DISPLAY.Focus()
                        fn_Validate = False
                        Exit Function
                    End If
                End If
            End If
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Function
    Private Function fn_Save() As Boolean
        Try
            ' ***** HEADER *****
            Dim ds = oDepartment.GetStructureHeader
            With ds
                Try
                    .DATECREATED = oDepartment.GetData(sNoId).DATECREATED
                Catch oErr As Exception
                    .DATECREATED = Now
                End Try
                .DATEUPDATED = Now
                .KDDEPARTMENT = IIf(txtCODE.Text = "<---AUTO--->", sNoId, txtCODE.Text)
                .KDDEPARTMENT_BPJS = txtKDDEPARTMENT_BPJS.Text.ToString.Trim
                .NAME_DISPLAY = txtNAME_DISPLAY.Text.Trim
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
                .ISEKSEKUTIF = chkISEKSEKUTIF.Checked
                .ISKATARAK = chkISKATARAK.Checked
            End With

            If oFormMode = FORM_MODE.FORM_MODE_ADD Then
                Try
                    fn_Save = oDepartment.InsertData(ds)
                Catch oErr As Exception
                    MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            ElseIf oFormMode = FORM_MODE.FORM_MODE_EDIT Then
                Try
                    fn_Save = oDepartment.UpdateData(ds)
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
    Private Sub txtKDDEPARTMENT_BPJS_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtKDDEPARTMENT_BPJS.KeyPress
        If Asc(e.KeyChar) = 13 Then
            Try
                Dim oSetKoneksi As New Brigging.clsSetKoneksi
                Dim oSetKoneksiNew As New Setting.clsBPJSKoneksi
                Dim dsDataSetKoneksi = oSetKoneksiNew.GetData("VCLAIM")
                Dim uTime As Integer = 0

                If dsDataSetKoneksi IsNot Nothing Then
                    uTime = (DateTime.UtcNow - New DateTime(1970, 1, 1, 0, 0, 0)).TotalSeconds
                    Dim dsSetKoneksi = oSetKoneksi.GetDataVClaimReferensiPoli(dsDataSetKoneksi.ALAMATWEB, dsDataSetKoneksi.CONSID, dsDataSetKoneksi.SECREATKEY, dsDataSetKoneksi.REMARKS, uTime, txtKDDEPARTMENT_BPJS.Text)
                    Dim allData = JObject.Parse(dsSetKoneksi)
                    tabControl.SelectedTabPage = tab4
                    txtMEMO.Text = oSetKoneksi.Decrypt(allData("response"), dsDataSetKoneksi.CONSID & dsDataSetKoneksi.SECREATKEY & uTime)
                Else
                    MsgBox("Koneksi Tidak ditemukan", MsgBoxStyle.Exclamation, Me.Text)
                End If
            Catch oErr As Exception
                MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            End Try
        End If
    End Sub
#End Region
End Class