Imports DataAccess
Imports UI.WIN.MAIN.My.Resources
Imports System.Linq

Public Class frmRujukanKhusus
#Region "Declaration"
    Private oFormMode As FORM_MODE = FORM_MODE.FORM_MODE_VIEW
    Private isLoad As Boolean = False
    Private oRujukan As New Admission.clsRujukan

#End Region
#Region "Function"
    Public Sub LoadMe(ByVal FormMode As Integer, ByVal NoId As String)
        oFormMode = FormMode
        txtCODE.Text = NoId
    End Sub
    Private Sub Form_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        fn_ChangeFormState()
        fn_LoadLanguage()
        isLoad = True
    End Sub
    Public Sub fn_LoadLanguage()
        Try
            Me.Text = Rujukan.TITLE

            lKDRUJUKAN.Text = Rujukan.KDRUJUKAN

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
        fn_LoadKDDIAGNOSA()
        fn_LoadKDPROSEDUR()

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

        grvDetail_Diagnosa.OptionsBehavior.ReadOnly = Status
        grvDetail_Prosedur.OptionsBehavior.ReadOnly = Status
    End Sub
    Private Sub fn_EmptyMe()
        txtCODE.Text = "<--- AUTO --->"
    End Sub
    Private Sub fn_LoadData()
        Try
            Dim ds = oRujukan.GetData(txtCODE.Text)

            With ds
                txtCODE.Text = txtCODE.Text

                BindingSource1.DataSource = oRujukan.GetDataDetail1.Where(Function(x) x.KDRUJUKAN = txtCODE.Text).OrderBy(Function(x) x.SEQ).ToList()
                grdDetail_Diagnosa.DataSource = BindingSource1

                BindingSource2.DataSource = oRujukan.GetDataDetail2.Where(Function(x) x.KDRUJUKAN = txtCODE.Text).OrderBy(Function(x) x.SEQ).ToList()
                grdDetail_Prosedur.DataSource = BindingSource2

            End With
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Function fn_Validate() As Boolean
        Try
            fn_Validate = True
            If txtCODE.Text = String.Empty Then
                txtCODE.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                txtCODE.ErrorText = Statement.ErrorRequired

                txtCODE.Focus()
                fn_Validate = False
                Exit Function
            End If

            grvDetail_Diagnosa.UpdateCurrentRow()

            If grvDetail_Diagnosa.RowCount < 2 Then
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
            Dim arrDetail1 = oRujukan.GetStructureDetail1List
            For i As Integer = 0 To grvDetail_Diagnosa.RowCount - 2
                Dim dsDetail = oRujukan.GetStructureDetail1
                With dsDetail
                    .SEQ = i
                    .KDRUJUKAN = txtCODE.Text
                    .KDDIAGNOSA = grvDetail_Diagnosa.GetRowCellValue(i, colKDDIAGNOSA)
                    .REMARKS = "-"
                End With
                arrDetail1.Add(dsDetail)
            Next
            ' ***** PROSEDUR *****
            Dim arrDetail2 = oRujukan.GetStructureDetail2List
            For i As Integer = 0 To grvDetail_Prosedur.RowCount - 2
                Dim dsDetail = oRujukan.GetStructureDetail2
                With dsDetail
                    .SEQ = i
                    .KDRUJUKAN = txtCODE.Text
                    .KDPROSEDUR = grvDetail_Prosedur.GetRowCellValue(i, colKDPROSEDUR)
                    .REMARKS = "-"
                End With
                arrDetail2.Add(dsDetail)
            Next
            If oFormMode = FORM_MODE.FORM_MODE_ADD Then
                Try
                    txtCODE.Text = oRujukan.InsertDataDetail(txtCODE.Text, arrDetail1, arrDetail2)
                    If txtCODE.Text = "" Then
                        fn_Save = False
                    Else
                        fn_Save = True
                        CetakRegister(txtCODE.Text)
                    End If
                Catch oErr As Exception
                    MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            ElseIf oFormMode = FORM_MODE.FORM_MODE_EDIT Then
                Try
                    fn_Save = oRujukan.UpdateDataDetail(txtCODE.Text, arrDetail1, arrDetail2)
                    CetakRegister(txtCODE.Text)
                Catch oErr As Exception
                    MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            End If
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            fn_Save = False
        End Try
    End Function
    Private Sub CetakRegister(ByVal KDRujukan As String)
        'Dim rpt As New xtraRujukan

        'Dim ds = oRujukan.GetData(KDRujukan)
        'rpt.bindingSource.DataSource = ds
        'Dim printTool As New DevExpress.XtraReports.UI.ReportPrintTool(rpt)
        'printTool.PrintDialog()
    End Sub
    Private Sub DeleteToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DeleteToolStripMenuItem.Click
        If oFormMode = FORM_MODE.FORM_MODE_VIEW Then Exit Sub
        grvDetail_Diagnosa.DeleteSelectedRows()
    End Sub
    Private Sub ToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem1.Click
        If oFormMode = FORM_MODE.FORM_MODE_VIEW Then Exit Sub
        grvDetail_Prosedur.DeleteSelectedRows()
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
    Private Sub fn_LoadKDDIAGNOSA()
        Dim oDIAGNOSA As New Reference.clsDiagnosa

        Try
            grdKDDIAGNOSA.DataSource = oDIAGNOSA.GetData.Where(Function(x) x.ISACTIVE = True).ToList()
            grdKDDIAGNOSA.ValueMember = "KDDIAGNOSA"
            grdKDDIAGNOSA.DisplayMember = "MEMO"

        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadKDPROSEDUR()
        Dim oPROSEDUR As New Reference.clsProsedur

        Try
            grdKDPROSEDUR.DataSource = oPROSEDUR.GetData.Where(Function(x) x.ISACTIVE = True).ToList()
            grdKDPROSEDUR.ValueMember = "KDPROSEDUR"
            grdKDPROSEDUR.DisplayMember = "MEMO"

        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
#End Region
End Class