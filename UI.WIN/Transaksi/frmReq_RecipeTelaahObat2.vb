Imports iPOS.GLB.Globals
Imports iPOS.DA

Public Class frmReq_RecipeTelaahObat2
#Region "Declaration"
    Private oFormMode As FORM_MODE = FORM_MODE.FORM_MODE_VIEW
    Private sNoId As String
    Private isLoad As Boolean = False
    Private oReq_RecipeTelaahObat2 As New Sales.clsReq_RecipeTelaahObat2

#End Region
#Region "Function"
    Public Sub LoadMe(ByVal FormMode As FORM_MODE, ByVal sKDRECIPE As String, Optional ByVal NoId As String = "")
        isSave = False
        oFormMode = FormMode
        sNoId = NoId
        If oFormMode = FORM_MODE.FORM_MODE_ADD Then
            txtCODE.Text = sKDRECIPE
        End If
    End Sub
    Private Sub Form_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        fn_ChangeFormState()
        isLoad = True
    End Sub
    Private Sub Form_Closing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        sCode = txtCODE.Text.Trim.ToUpper
    End Sub
    Private Overloads Sub Dispose()
        MyBase.Dispose()
        Me.Dispose(True)
        GC.SuppressFinalize(Me)
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
        btnChekAll.Enabled = Not Status

        If oFormMode = FORM_MODE.FORM_MODE_ADD Then
            txtCODE.Properties.ReadOnly = False
        Else
            txtCODE.Properties.ReadOnly = True
        End If

        CheckEdit1.Properties.ReadOnly = Status
        CheckEdit2.Properties.ReadOnly = Status
        CheckEdit3.Properties.ReadOnly = Status
        CheckEdit4.Properties.ReadOnly = Status
        CheckEdit5.Properties.ReadOnly = Status

    End Sub
    Private Sub fn_EmptyMe()
        'txtCODE.Text = "<--- AUTO --->"
        CheckEdit1.Checked = True
        CheckEdit2.Checked = True
        CheckEdit3.Checked = True
        CheckEdit4.Checked = True
        CheckEdit5.Checked = True
    End Sub
    Private Sub fn_LoadData()
        Try
            ' ***** HEADER *****
            Dim ds = oReq_RecipeTelaahObat2.GetData(sNoId)

            With ds
                txtCODE.Text = .KDREQRECIPE
                CheckEdit1.Checked = .TELAAH_01
                CheckEdit2.Checked = .TELAAH_02
                CheckEdit3.Checked = .TELAAH_03
                CheckEdit4.Checked = .TELAAH_04
                CheckEdit5.Checked = .TELAAH_05
            End With
        Catch oErr As Exception
            MsgBox("Load List Data : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Function fn_Validate() As Boolean
        Try
            fn_Validate = True

            If txtCODE.Text = String.Empty Then
                MsgBox("Dibutuhkan Kode Resep", MsgBoxStyle.Exclamation, Me.Text)
                txtCODE.Focus()
                fn_Validate = False
                Exit Function
            End If

        Catch oErr As Exception
            MsgBox("Validate Data : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Function
    Private Function fn_Save() As Boolean
        Try
            ' ***** HEADER *****
            Dim ds = oReq_RecipeTelaahObat2.GetStructureHeader
            With ds
                .KDREQRECIPE = txtCODE.Text
                Try
                    .DATECREATED = oReq_RecipeTelaahObat2.GetData(sNoId).DATECREATED
                Catch ex As Exception
                    .DATECREATED = Now
                End Try
                .DATEUPDATED = Now
                .TELAAH_01 = CheckEdit1.Checked
                .TELAAH_02 = CheckEdit2.Checked
                .TELAAH_03 = CheckEdit3.Checked
                .TELAAH_04 = CheckEdit4.Checked
                .TELAAH_05 = CheckEdit5.Checked
                .NOIDUSER = sUserID
                .REMARKS = "Y"

            End With

            If oFormMode = FORM_MODE.FORM_MODE_ADD Then
                Try
                    fn_Save = oReq_RecipeTelaahObat2.InsertData(ds)
                    isSave = True
                Catch ex As Exception
                    MsgBox("Simpan Data : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
                    isSave = False
                End Try
            ElseIf oFormMode = FORM_MODE.FORM_MODE_EDIT Then
                Try
                    fn_Save = oReq_RecipeTelaahObat2.UpdateData(ds)
                    isSave = True
                Catch ex As Exception
                    MsgBox("Simpan Data : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
                    isSave = False
                End Try
            End If
        Catch oErr As Exception
            MsgBox("Simpan Data : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            fn_Save = False
            isSave = False
        End Try
    End Function
    Private Function fn_PrintStruk(ByVal sCode As String) As Boolean
        'Dim rpt As New xtraReq_RecipeTelaahObat2

        'Dim dsDetail = oReq_RecipeTelaahObat2.GetDataDetail(sCode)

        'rpt.bindingSource.DataSource = dsDetail
        'Dim printTool As New DevExpress.XtraReports.UI.ReportPrintTool(rpt)
        'printTool.Print()
    End Function
#End Region
#Region "Grid Method"

#End Region
#Region "Command Button"
    Private Sub frmReq_RecipeTelaahObat2_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        Select Case e.KeyCode
            Case Keys.F2
                If btnSaveNew.Enabled = True Then
                    btnSaveNew_Click()
                End If
            Case Keys.F3
                If btnSaveClose.Enabled = True Then
                    btnSaveClose_Click()
                End If
            Case Keys.F5
                If btnChekAll.Enabled = True Then
                    btnChekAll_Click()
                End If
        End Select
    End Sub
    Private Sub btnChekAll_Click() Handles btnChekAll.ItemClick
        CheckEdit1.Checked = True
        CheckEdit2.Checked = True
        CheckEdit3.Checked = True
        CheckEdit4.Checked = True
        CheckEdit5.Checked = True
    End Sub
    Private Sub btnSaveNew_Click() Handles btnSaveNew.ItemClick
        If fn_Validate() = False Then Exit Sub
        If MsgBox("Save " & txtCODE.Text.Trim.ToUpper & "?", MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, Me.Text) = MsgBoxResult.No Then Exit Sub
        If fn_Save() = False Then
            MsgBox("Save gagal! Tolong kontak admin anda!", MsgBoxStyle.Exclamation, Me.Text)
        Else
            MsgBox("Save " & txtCODE.Text.Trim.ToUpper & " success!", MsgBoxStyle.Information, Me.Text)
            sStatusSave = "NEW"
            Me.Close()
        End If
    End Sub
    Private Sub btnSaveClose_Click() Handles btnSaveClose.ItemClick
        If fn_Validate() = False Then Exit Sub
        If MsgBox("Save " & txtCODE.Text.Trim.ToUpper & "?", MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, Me.Text) = MsgBoxResult.No Then Exit Sub
        If fn_Save() = False Then
            MsgBox("Save gagal! Tolong kontak admin anda!", MsgBoxStyle.Exclamation, Me.Text)
        Else
            MsgBox("Save " & txtCODE.Text.Trim.ToUpper & " success!", MsgBoxStyle.Information, Me.Text)
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