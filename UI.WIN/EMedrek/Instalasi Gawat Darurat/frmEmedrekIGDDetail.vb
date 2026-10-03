Imports DataAccess
Imports UI.WIN.MAIN.My.Resources
Imports System.Linq

Public Class frmEmedrekIGDDetail
#Region "Declaration"
    Private oFormMode As FORM_MODE = FORM_MODE.FORM_MODE_VIEW
    Private sNoId As String
    Private isLoad As Boolean = False
    Private oS_DIGITAL_IGD_01 As New Digital.clsDigital_IGD_01
#End Region
#Region "Function"
    Public Sub LoadMe(ByVal NoId As String)
        sNoId = NoId
        fn_NOIDUSER()
        fn_LoadData()
    End Sub
    Private Sub Form_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        isLoad = True
    End Sub
    Private Sub Form_Closing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        'sCode = txtCODE.Text.Trim.ToUpper
    End Sub
    Private Sub fn_LoadData()
        Try
            ' ***** HEADER *****
            Dim ds = oS_DIGITAL_IGD_01.GetData(sNoId)

            With ds
                BindingSource.DataSource = oS_DIGITAL_IGD_01.GetDataDetail(sNoId).OrderBy(Function(x) x.SEQ).ToList()
                grdDetail.DataSource = bindingSource
            End With
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Function fn_Validate() As Boolean
        Try
            fn_Validate = True

            If sNoId = String.Empty Then
                MsgBox("Kode Kunjungan Kosong", MsgBoxStyle.Exclamation, Me.Text)

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
            ' ***** DETIL *****
            Dim arrDetail = oS_DIGITAL_IGD_01.GetStructureDetailList
            For i As Integer = 0 To grvDetail.RowCount - 2
                Dim dsDetail = oS_DIGITAL_IGD_01.GetStructureDetail
                With dsDetail
                    .SEQ = i
                    .KDKUNJUNGAN = sNoId
                    .PUKUL = CDate(grvDetail.GetRowCellValue(i, colPUKUL))
                    .PENANGANAN = grvDetail.GetRowCellValue(i, colPENANGANAN)
                    .NAMA = grvDetail.GetRowCellValue(i, colNAMA)
                    .PARAF = grvDetail.GetRowCellValue(i, colPARAF)
                End With
                arrDetail.Add(dsDetail)
            Next

            Try
                fn_Save = oS_DIGITAL_IGD_01.UpdateDataDetail(sNoId, arrDetail)
            Catch oErr As Exception
                MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            End Try
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            fn_Save = False
        End Try
    End Function
#End Region
#Region "Grid Method"
    Private Sub DeleteToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DeleteToolStripMenuItem.Click
        If oFormMode = FORM_MODE.FORM_MODE_VIEW Then Exit Sub
        'grvDetail.DeleteSelectedRows()
    End Sub
#End Region
#Region "Command Button"
    Private Sub frmOrderLab_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
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
    Private Sub fn_NOIDUSER()
        Dim oUser As New Setting.clsUser
        Try
            grdUSER.DataSource = oUser.GetData.Where(Function(x) x.ISACTIVE = True).ToList()
            grdUSER.ValueMember = "KDUSER"
            grdUSER.DisplayMember = "KDUSER"
        Catch oErr As Exception
            MsgBox("Load User Data : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
#End Region
End Class