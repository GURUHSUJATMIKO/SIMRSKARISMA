Imports DataAccess
Imports UI.WIN.MAIN.My.Resources
Imports System.Linq

Public Class frmItem_L5List
    Implements ILanguage

    Private oFormMode As FORM_MODE = FORM_MODE.FORM_MODE_VIEW
    Private oItem_L5 As New Reference.clsItem_L5

#Region "Function"
    Private Sub Me_Load() Handles Me.Load
        Me.Text = sItem_L5

        fn_LoadSecurity()

        Try
            grv.RestoreLayoutFromRegistry("HKEY_CURRENT_USER\Software\SIMRS\SW\" & sUserID & "\" & Me.Text)
        Catch ex As Exception

        End Try
    End Sub
    Private Sub Me_FormClosed(sender As System.Object, e As System.Windows.Forms.FormClosedEventArgs) Handles MyBase.FormClosed
        Try
            grv.SaveLayoutToRegistry("HKEY_CURRENT_USER\Software\SIMRS\SW\" & sUserID & "\" & Me.Text)
        Catch ex As Exception

        End Try
    End Sub
    Private Sub fn_LoadSecurity()
        Try
            Dim oOtority As New Setting.clsOtority
            Dim oUser As New Setting.clsUser

            Dim ds = (From x In oOtority.GetDataDetail _
                     Join y In oUser.GetData _
                     On x.KDOTORITY Equals y.KDOTORITY _
                     Where x.MODUL = "ITEM_L5" _
                     And y.KDUSER = sUserID _
                     Select x.ISADD, x.ISDELETE, x.ISUPDATE, x.ISPRINT, x.ISVIEW).FirstOrDefault

            Try
                picAdd.Enabled = ds.ISADD
                picDelete.Enabled = ds.ISDELETE
                picUpdate.Enabled = ds.ISUPDATE
                picPrint.Enabled = ds.ISPRINT
                picRefresh.Enabled = ds.ISVIEW

                If ds.ISVIEW = True Then
                    fn_LoadData()
                    fn_LoadLanguage()
                End If
            Catch oErr As Exception
                MsgBox(Statement.SecurityNotInstalled, MsgBoxStyle.Exclamation, Me.Text)

                picAdd.Enabled = False
                picDelete.Enabled = False
                picUpdate.Enabled = False
                picPrint.Enabled = False
                picRefresh.Enabled = False
            End Try
        Catch oErr As Exception
            MsgBox(Statement.SecurityNotInstalled, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Public Sub fn_LoadLanguage() Implements ILanguage.fn_LoadLanguage
        Try
            Me.Text = sItem_L2

            grv.Columns("MEMO").Caption = Item_L5.MEMO
            grv.Columns("ISACTIVE").Caption = Item_L5.ISACTIVE
            grv.Columns("ISDEFAULT").Caption = Item_L5.ISDEFAULT

            ColumnChooserToolStripMenuItem.Text = Caption.ColumnChooser
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadData()
        Try
            Dim ds = From x In oItem_L5.GetData _
                     Select x.KDITEM_L5, x.MEMO, x.ISACTIVE, x.ISDEFAULT

            grd.DataSource = ds.ToList

            fn_LoadFormatData()
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadFormatData()
        For iLoop As Integer = 0 To grv.Columns.Count - 1
            If grv.Columns(iLoop).ColumnType.Name = "Decimal" Then
                grv.Columns(iLoop).DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                grv.Columns(iLoop).DisplayFormat.FormatString = "{0:n2}"
                grv.Columns(iLoop).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
            ElseIf grv.Columns(iLoop).ColumnType.Name = "DateTime" Then
                grv.Columns(iLoop).DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
                grv.Columns(iLoop).DisplayFormat.FormatString = "{0:dd/MM/yyyy}"
            End If
        Next

        grv.Columns("KDITEM_L5").Visible = False
        grv.Columns("ISDEFAULT").Visible = False
        grv.Columns("ISACTIVE").Visible = False

        grv.Columns("KDITEM_L5").OptionsColumn.ShowInCustomizationForm = False
    End Sub
    Private Sub ColumnChooserToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles ColumnChooserToolStripMenuItem.Click
        grv.ShowCustomization()
    End Sub
    Private Function fn_DeleteData(ByVal sKDITEM_L5 As String) As Boolean
        Try
            oItem_L5.DeleteData(sKDItem_L5)
            fn_DeleteData = True
        Catch oErr As Exception
            fn_DeleteData = False
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Function
#End Region
#Region "Command Button"
    Private Sub frmMember_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        Select Case e.KeyCode
            Case Keys.A
                If e.Alt = True And picAdd.Enabled = True Then
                    picAdd_Click()
                End If
            Case Keys.E
                If e.Alt = True And picUpdate.Enabled = True Then
                    picUpdate_Click()
                End If
            Case Keys.D
                If e.Alt = True And picDelete.Enabled = True Then
                    picDelete_Click()
                End If
            Case Keys.P
                If e.Alt = True And picPrint.Enabled = True Then
                    picPrint_Click()
                End If
            Case Keys.R
                If e.Alt = True And picRefresh.Enabled = True Then
                    picRefresh_Click()
                End If
        End Select
    End Sub
    Private Sub grv_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles grv.DoubleClick
        If grv.GetFocusedRowCellValue("KDITEM_L5") Is Nothing Then
            Exit Sub
        End If

        Dim frmItem_L5 As New frmItem_L5
        Try
            frmItem_L5.LoadMe(FORM_MODE.FORM_MODE_VIEW, grv.GetFocusedRowCellValue("KDITEM_L5"))
            frmItem_L5.ShowDialog(Me)
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub picAdd_Click() Handles picAdd.Click
        Dim frmItem_L5 As New frmItem_L5
        Try
            frmItem_L5.LoadMe(FORM_MODE.FORM_MODE_ADD)
            frmItem_L5.ShowDialog(Me)
            fn_LoadSecurity()
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        Finally
            If Not frmItem_L5 Is Nothing Then frmItem_L5.Dispose()
            frmItem_L5 = Nothing

            Dim rowHandle As Integer = grv.LocateByValue(rowHandle, grv.Columns("NAME_DISPLAY"), sCode)
            If rowHandle > 0 Then grv.FocusedRowHandle = rowHandle

            If sStatusSave = "NEW" Then
                sStatusSave = "NONE"
                picAdd_Click()
            End If
        End Try
    End Sub
    Private Sub picUpdate_Click() Handles picUpdate.Click
        If grv.GetFocusedRowCellValue("KDITEM_L5") Is Nothing Then
            Exit Sub
        End If
        Dim frmItem_L5 As New frmItem_L5
        Try
            frmItem_L5.LoadMe(FORM_MODE.FORM_MODE_EDIT, grv.GetFocusedRowCellValue("KDITEM_L5"))
            frmItem_L5.ShowDialog(Me)
            fn_LoadSecurity()
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        Finally
            If Not frmItem_L5 Is Nothing Then frmItem_L5.Dispose()
            frmItem_L5 = Nothing

            Dim rowHandle As Integer = grv.LocateByValue(rowHandle, grv.Columns("NAME_DISPLAY"), sCode)
            If rowHandle > 0 Then grv.FocusedRowHandle = rowHandle

            If sStatusSave = "NEW" Then
                sStatusSave = "NONE"
                picAdd_Click()
            End If
        End Try
    End Sub
    Private Sub picDelete_Click() Handles picDelete.Click
        If grv.GetFocusedRowCellValue("KDITEM_L5") Is Nothing Then
            Exit Sub
        End If
        If MsgBox(Statement.DeleteQuestion, MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, Me.Text) = MsgBoxResult.No Then Exit Sub
        If fn_DeleteData(grv.GetFocusedRowCellValue("KDITEM_L5")) = False Then
            MsgBox(Statement.DeleteFail, MsgBoxStyle.Exclamation, Me.Text)
            Exit Sub
        End If
        MsgBox(Statement.DeleteSuccess, MsgBoxStyle.Information, Me.Text)
        fn_LoadSecurity()
    End Sub
    Private Sub picPrint_Click() Handles picPrint.Click
        'Try
        '    Dim xtraItem_L5 As New xtraItem_L5

        '    Dim sKDITEM_L5 As New List(Of String)
        '    For i As Integer = 0 To grv.RowCount - 1
        '        sKDITEM_L5.Add(grv.GetRowCellValue(i, "KDITEM_L5"))
        '    Next

        '    Dim ds = oItem_L5.GetData.Where(Function(x) sKDITEM_L5.Contains(x.KDITEM_L5)).ToList

        '    xtraItem_L5.bindingSource.DataSource = ds
        '    Dim printTool As New DevExpress.XtraReports.UI.ReportPrintTool(xtraItem_L5)
        '    printTool.ShowPreviewDialog(DevExpress.LookAndFeel.UserLookAndFeel.Default)
        'Catch oErr As Exception
        '    MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        'End Try
    End Sub
    Private Sub picRefresh_Click() Handles picRefresh.Click
        fn_LoadSecurity()
    End Sub
#End Region
End Class