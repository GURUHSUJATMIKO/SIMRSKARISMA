Imports DataAccess
Imports UI.WIN.MAIN.My.Resources
Imports System.Linq

Public Class frmKelurahan
#Region "Declaration"
    Private oFormMode As FORM_MODE = FORM_MODE.FORM_MODE_VIEW
    Private sNoId As String
    Private isLoad As Boolean = False
    Private oKELURAHAN As New Reference.clsKelurahan
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
            Me.Text = Kelurahan.TITLE

            lKECAMATAN.Text = Kelurahan.KDKECAMATAN & " *"
            lMEMO.Text = Kelurahan.MEMO & " *"
            chkISACTIVE.Text = Kelurahan.ISACTIVE
            chkISDEFAULT.Text = Kelurahan.ISDEFAULT
            lKODEPOS.Text = Kelurahan.KODEPOS & " *"

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
        fn_LoadKECAMATAN()

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

        grdKDKECAMATAN.Properties.ReadOnly = Status
        txtMEMO.Properties.ReadOnly = Status
        chkISACTIVE.Properties.ReadOnly = Status
        chkISDEFAULT.Properties.ReadOnly = Status
        txtKODEPOS.Properties.ReadOnly = Status
    End Sub
    Private Sub fn_EmptyMe()
        grdKDKECAMATAN.ResetText()
        txtMEMO.ResetText()

        chkISACTIVE.Checked = True
        chkISDEFAULT.Checked = False

        txtMEMO.ResetText()
    End Sub
    Private Sub fn_LoadData()
        Try
            Dim ds = oKELURAHAN.GetData(sNoId)

            With ds
                grdKDKECAMATAN.Text = .KDKECAMATAN
                txtMEMO.Text = .MEMO
                chkISACTIVE.Checked = .ISACTIVE
                chkISDEFAULT.Checked = .ISDEFAULT
                txtKODEPOS.Text = .kodepos
            End With
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Function fn_Validate() As Boolean
        Try
            fn_Validate = True
            If grdKDKECAMATAN.Text = String.Empty Then
                grdKDKECAMATAN.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                grdKDKECAMATAN.ErrorText = Statement.ErrorRequired

                grdKDKECAMATAN.Focus()
                fn_Validate = False
                Exit Function
            End If
            If txtKODEPOS.Text = String.Empty Then
                txtKODEPOS.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                txtKODEPOS.ErrorText = Statement.ErrorRequired

                txtKODEPOS.Focus()
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
            If oFormMode = FORM_MODE.FORM_MODE_ADD Then
                If oKELURAHAN.IsExist(txtMEMO.Text.ToUpper.Trim) = True Then
                    txtMEMO.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                    txtMEMO.ErrorText = Statement.ErrorRegistered

                    txtMEMO.Focus()
                    fn_Validate = False
                    Exit Function
                End If
            Else
                If txtMEMO.Text.Trim.ToUpper <> oKELURAHAN.GetData(sNoId).MEMO Then
                    If oKELURAHAN.IsExist(txtMEMO.Text.ToUpper.Trim) = True Then
                        txtMEMO.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                        txtMEMO.ErrorText = Statement.ErrorRegistered

                        txtMEMO.Focus()
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
            Dim ds = oKELURAHAN.GetStructureHeader
            With ds
                Try
                    .DATECREATED = oKELURAHAN.GetData(sNoId).DATECREATED
                Catch oErr As Exception
                    .DATECREATED = Now
                End Try
                .DATEUPDATED = Now

                .KDKELURAHAN = sNoId
                .KDKECAMATAN = IIf(String.IsNullOrEmpty(grdKDKECAMATAN.EditValue), String.Empty, grdKDKECAMATAN.EditValue)
                .MEMO = txtMEMO.Text.Trim.ToUpper
                .ISACTIVE = chkISACTIVE.Checked
                .ISDEFAULT = chkISDEFAULT.Checked
                .kodepos = txtKODEPOS.Text.ToString.Trim.ToUpper
            End With

            If oFormMode = FORM_MODE.FORM_MODE_ADD Then
                Try
                    fn_Save = oKELURAHAN.InsertData(ds)
                Catch oErr As Exception
                    MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            ElseIf oFormMode = FORM_MODE.FORM_MODE_EDIT Then
                Try
                    fn_Save = oKELURAHAN.UpdateData(ds)
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
    Private Sub fn_LoadKECAMATAN()
        Dim oKECAMATAN As New Reference.clsKecamatan
        Try
            Dim dsKABUPATEN = From x In oKECAMATAN.GetData
                              Where x.ISACTIVE = True And x.VCLAIM_KODE <> ""
                              Select x.KDKECAMATAN, PROPINSI = x.M_KABUPATEN.M_PROPINSI.MEMO, KABUPATEN = x.M_KABUPATEN.MEMO, x.MEMO

            grdKDKECAMATAN.Properties.DataSource = dsKABUPATEN.ToList()
            grdKDKECAMATAN.Properties.ValueMember = "KDKECAMATAN"
            grdKDKECAMATAN.Properties.DisplayMember = "MEMO"
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub SimpleButton1_Click(sender As Object, e As EventArgs) Handles SimpleButton1.Click
        Dim frmKecamatan As New frmKecamatan
        Try
            frmKecamatan.LoadMe(FORM_MODE.FORM_MODE_ADD)
            frmKecamatan.ShowDialog(Me)

            fn_LoadKECAMATAN()
            grdKDKECAMATAN.Text = sCode
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try

    End Sub

#End Region
End Class