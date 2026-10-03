Imports DataAccess
Imports UI.WIN.MAIN.My.Resources
Imports System.Linq
Imports Newtonsoft.Json.Linq

Public Class frmTracking
#Region "Declaration"
    Private oFormMode As FORM_MODE = FORM_MODE.FORM_MODE_VIEW
    Private sNoId As String
    Private isLoad As Boolean = False
    Private oTracking As New Admission.clsTracking
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
            Me.Text = Tracking.TITLE

            'lKDTracking.Text = Tracking.KDTracking

            btnSaveNew.Caption = Caption.FormSaveNew
            btnSaveClose.Caption = Caption.FormSaveClose
            btnClose.Caption = Caption.FormClose
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub Form_Closing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        sCode = IIf(txtCODE.Text = "<--- AUTO --->", "", txtCODE.Text.Trim.ToUpper)
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

        grdCARIREKAMMEDIS.Properties.ReadOnly = Status
        txtKDCUSTOMER.Properties.ReadOnly = True
        txtTUJUAN_SEKARANG.Properties.ReadOnly = Status
        deDATE_KIRIM.Properties.ReadOnly = Status
        txtMEMO.Properties.ReadOnly = Status
    End Sub
    Private Sub fn_EmptyMe()
        txtCODE.Text = "<--- AUTO --->"

        grdCARIREKAMMEDIS.ResetText()
        txtKDCUSTOMER.ResetText()
        txtTUJUAN_SEKARANG.ResetText()
        deDATE_KIRIM.DateTime = Now
        txtMEMO.ResetText()
    End Sub
    Private Sub fn_LoadData()
        Try
            Dim ds = oTracking.GetData(sNoId)

            With ds
                txtCODE.Text = sNoId
                txtKDCUSTOMER.Text = .KDCUSTOMER
                txtTUJUAN_SEKARANG.Text = .TUJUAN_SEKARANG
                deDATE_KIRIM.DateTime = .DATE_KIRIM
                txtMEMO.Text = .MEMO
            End With
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Function fn_Validate() As Boolean
        Try
            fn_Validate = True
            If txtKDCUSTOMER.Text = String.Empty Then
                txtKDCUSTOMER.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                txtKDCUSTOMER.ErrorText = Statement.ErrorRequired

                txtKDCUSTOMER.Focus()
                fn_Validate = False
                Exit Function
            End If
            If txtTUJUAN_SEKARANG.Text = String.Empty Then
                txtTUJUAN_SEKARANG.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                txtTUJUAN_SEKARANG.ErrorText = Statement.ErrorRequired

                txtTUJUAN_SEKARANG.Focus()
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
            Dim ds = oTracking.GetStructureHeader
            With ds
                Try
                    .DATECREATED = oTracking.GetData(sNoId).DATECREATED
                Catch oErr As Exception
                    .DATECREATED = Now
                End Try

                .DATEUPDATED = Now
                .KDTRACKING = sNoId
                Try
                    .KDPENDAFTARAN = oTracking.GetData(sNoId).KDPENDAFTARAN
                Catch ex As Exception
                    .KDPENDAFTARAN = ""
                End Try
                .CATEGORY = 3
                .KDCUSTOMER = txtKDCUSTOMER.Text
                .TUJUAN_SEKARANG = txtTUJUAN_SEKARANG.Text
                Try
                    .TUJUAN_SEBELUM = oTracking.GetData(sNoId).TUJUAN_SEBELUM
                    .DATE_SEBELUM = oTracking.GetData(sNoId).DATE_SEBELUM
                Catch ex As Exception
                    Dim dsTracking = oTracking.GetDataOrderByDesc(txtKDCUSTOMER.Text)
                    If dsTracking IsNot Nothing Then
                        .TUJUAN_SEBELUM = dsTracking.TUJUAN_SEKARANG
                        .DATE_SEBELUM = dsTracking.DATE_KIRIM
                    Else
                        .TUJUAN_SEBELUM = ""
                        .DATE_SEBELUM = Now
                    End If
                End Try
                Try
                    .JENIS_PASIEN = oTracking.GetData(sNoId).JENIS_PASIEN
                Catch ex As Exception
                    .JENIS_PASIEN = "L"
                End Try
                .DATE_KIRIM = deDATE_KIRIM.DateTime
                Try
                    .DATE_KEMBALI = oTracking.GetData(sNoId).DATE_KEMBALI
                Catch ex As Exception
                    .DATE_KEMBALI = Now
                End Try
                Try
                    .ISCHEKED = oTracking.GetData(sNoId).ISCHEKED
                Catch ex As Exception
                    .ISCHEKED = True
                End Try
                .MEMO = txtMEMO.Text
                .KDUSER = sUserID
                Try
                    .STATUS = oTracking.GetData(sNoId).STATUS
                Catch ex As Exception
                    .STATUS = "KIRIM"
                End Try
            End With

            If oFormMode = FORM_MODE.FORM_MODE_ADD Then
                fn_Save = oTracking.InsertData(ds)
            ElseIf oFormMode = FORM_MODE.FORM_MODE_EDIT Then
                fn_Save = oTracking.UpdateData(ds)
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
            MsgBox("Nomor Tracking " & txtCODE.Text, MsgBoxStyle.Information, Me.Text)
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
            MsgBox("Nomor Tracking " & txtCODE.Text, MsgBoxStyle.Information, Me.Text)
            Me.Close()
        End If
    End Sub
    Private Sub btnClose_Click() Handles btnClose.ItemClick
        Me.Close()
    End Sub
    Private Sub txtCARI_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtCARI.KeyPress
        If Asc(e.KeyChar) = 13 Then
            Dim oCustomer As New Reference.clsCustomer

            If cboCARI.SelectedIndex = 0 Then
                grdCARIREKAMMEDIS.Properties.DataSource = oCustomer.GetDataSync(txtCARI.Text).Where(Function(x) x.ISACTIVE = True).ToList()
                grdCARIREKAMMEDIS.Properties.ValueMember = "KDCUSTOMER"
                grdCARIREKAMMEDIS.Properties.DisplayMember = "NAME_DISPLAY"
            Else
                grdCARIREKAMMEDIS.Properties.DataSource = oCustomer.GetDataSyncbyName(txtCARI.Text).Where(Function(x) x.ISACTIVE = True).ToList()
                grdCARIREKAMMEDIS.Properties.ValueMember = "KDCUSTOMER"
                grdCARIREKAMMEDIS.Properties.DisplayMember = "NAME_DISPLAY"
            End If
        End If
    End Sub
    Private Sub grdCARIREKAMMEDIS_KeyPress(sender As Object, e As KeyPressEventArgs) Handles grdCARIREKAMMEDIS.KeyPress
        If Asc(e.KeyChar) = 13 Then
            txtKDCUSTOMER.Text = grdCARIREKAMMEDIS.EditValue
        End If
    End Sub
#End Region
#Region "Lookup / Event"

#End Region
End Class