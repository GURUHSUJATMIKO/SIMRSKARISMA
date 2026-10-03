Imports DataAccess
Imports UI.WIN.MAIN.My.Resources
Imports System.Linq

Public Class frmMutationList
    Implements ILanguage

    Private oFormMode As FORM_MODE = FORM_MODE.FORM_MODE_VIEW
    Private oMutation As New Inventory.clsMutation

#Region "Function"
    Private Sub Me_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.Text = Mutation.TITLE

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
                     Where x.MODUL = "MUTATION" _
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
            Me.Text = Mutation.TITLE

            fn_LoadLanguageMaster()
            fn_LoadLanguageDetail()
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Public Sub fn_LoadLanguageMaster()
        Try
            grv.Columns("KDMUTATION").Caption = Mutation.KDMUTATION
            grv.Columns("TANGGAL").Caption = Mutation.TANGGAL
            grv.Columns("KDWAREHOUSEFROM").Caption = Mutation.KDWAREHOUSEFROM
            grv.Columns("KDWAREHOUSETO").Caption = Mutation.KDWAREHOUSETO
            grv.Columns("MEMO").Caption = Mutation.MEMO
            grv.Columns("KDUSER").Caption = Caption.User
        Catch oErr As Exception

        End Try
    End Sub
    Public Sub fn_LoadLanguageDetail()
        Try
            grv1.Columns("QTY").Caption = Mutation.DETAIL_QTY
            grv1.Columns("REMARKS").Caption = Mutation.DETAIL_REMARKS

            grv1.Columns("M_ITEM.NMITEM1").Caption = Item.NMITEM1
            grv1.Columns("M_ITEM.NMITEM2").Caption = Item.NMITEM2
            'grv1.Columns("M_ITEM.NMITEM3").Caption = Item.NMITEM3
            grv1.Columns("M_UOM.MEMO").Caption = Mutation.DETAIL_KDUOM
        Catch oErr As Exception

        End Try
    End Sub
    Private Sub fn_LoadData()
        Try
            Dim ds = From x In oMutation.GetData _
                     Select x.KDMUTATION, TANGGAL = x.DATE, KDWAREHOUSEFROM = x.M_WAREHOUSE.NAME_DISPLAY, KDWAREHOUSETO = x.M_WAREHOUSE1.NAME_DISPLAY, x.MEMO, Details = x.I_MUTATION_Ds, x.KDUSER
            grd.DataSource = ds.ToList

            fn_LoadFormatData()
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub grv_MasterRowExpanded(sender As System.Object, e As DevExpress.XtraGrid.Views.Grid.CustomMasterRowEventArgs) Handles grv.MasterRowExpanded
        grv1 = TryCast(grv.GetDetailView(e.RowHandle, e.RelationIndex), DevExpress.XtraGrid.Views.Grid.GridView)

        fn_LoadFormatDataDetail()
        fn_LoadLanguageDetail()
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
    End Sub
    Private Sub fn_LoadFormatDataDetail()
        For iLoop As Integer = 0 To grv1.Columns.Count - 1
            If grv1.Columns(iLoop).ColumnType.Name = "Decimal" Then
                grv1.Columns(iLoop).DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                grv1.Columns(iLoop).DisplayFormat.FormatString = "{0:n2}"
                grv1.Columns(iLoop).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
            ElseIf grv1.Columns(iLoop).ColumnType.Name = "DateTime" Then
                grv1.Columns(iLoop).DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
                grv1.Columns(iLoop).DisplayFormat.FormatString = "{0:dd/MM/yyyy}"
            End If
        Next

        grv1.Columns().AddVisible("M_ITEM.NMITEM1")
        grv1.Columns().AddVisible("M_ITEM.NMITEM2")
        'grv1.Columns().AddVisible("M_ITEM.NMITEM3")
        grv1.Columns().AddVisible("M_UOM.MEMO")

        grv1.Columns("DATECREATED").Visible = False
        grv1.Columns("DATEUPDATED").Visible = False
        grv1.Columns("SEQ").Visible = False
        grv1.Columns("KDMUTATION").Visible = False
        grv1.Columns("M_ITEM").Visible = False
        grv1.Columns("M_UOM").Visible = False
        grv1.Columns("I_MUTATION_H").Visible = False
        grv1.Columns("KDITEM").Visible = False
        grv1.Columns("KDUOM").Visible = False

        grv1.Columns("DATECREATED").OptionsColumn.ShowInCustomizationForm = False
        grv1.Columns("DATEUPDATED").OptionsColumn.ShowInCustomizationForm = False
        grv1.Columns("SEQ").OptionsColumn.ShowInCustomizationForm = False
        grv1.Columns("KDMUTATION").OptionsColumn.ShowInCustomizationForm = False
        grv1.Columns("M_ITEM").OptionsColumn.ShowInCustomizationForm = False
        grv1.Columns("M_UOM").OptionsColumn.ShowInCustomizationForm = False
        grv1.Columns("I_MUTATION_H").OptionsColumn.ShowInCustomizationForm = False
        grv1.Columns("KDITEM").OptionsColumn.ShowInCustomizationForm = False
        grv1.Columns("KDUOM").OptionsColumn.ShowInCustomizationForm = False

        grv1.Columns("M_ITEM.NMITEM1").VisibleIndex = 0
        grv1.Columns("M_ITEM.NMITEM2").VisibleIndex = 1
        'grv1.Columns("M_ITEM.NMITEM3").VisibleIndex = 2
        grv1.Columns("M_UOM.MEMO").VisibleIndex = 4
    End Sub
    Private Sub MasterColumnChooserToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles MasterColumnChooserToolStripMenuItem.Click
        grv.ShowCustomization()
    End Sub
    Private Sub DetailColumnChooserToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles DetailColumnChooserToolStripMenuItem.Click
        grv1.ShowCustomization()
    End Sub
    Private Function fn_DeleteData(ByVal sKDMUTATION As String) As Boolean
        Try
            oMutation.DeleteData(sKDMUTATION)

            fn_DeleteData = True
        Catch oErr As Exception
            fn_DeleteData = False
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Function
    Private Sub grv_RowStyle(sender As System.Object, e As DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs) Handles grv.RowStyle
        If grv.IsFilterRow(e.RowHandle) Then Exit Sub
        If CBool(grv.GetRowCellValue(e.RowHandle, "ISDELETE")) = True Then
            e.Appearance.BackColor = Color.LightGray
        Else
            e.Appearance.BackColor = Color.LightGreen
        End If
    End Sub
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
        If grv.GetFocusedRowCellValue("KDMUTATION") Is Nothing Then
            Exit Sub
        End If

        Dim frmMutation As New frmMutation
        Try
            frmMutation.LoadMe(FORM_MODE.FORM_MODE_VIEW, grv.GetFocusedRowCellValue("KDMUTATION"))
            frmMutation.ShowDialog(Me)
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub picAdd_Click() Handles picAdd.Click
        Dim frmMutation As New frmMutation
        Try
            frmMutation.LoadMe(FORM_MODE.FORM_MODE_ADD)
            frmMutation.ShowDialog(Me)
            fn_LoadSecurity()
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        Finally
            If Not frmMutation Is Nothing Then frmMutation.Dispose()
            frmMutation = Nothing

            Dim rowHandle As Integer = grv.LocateByValue(rowHandle, grv.Columns("KDMUTATION"), sCode)
            If rowHandle > 0 Then grv.FocusedRowHandle = rowHandle

            If sStatusSave = "NEW" Then
                sStatusSave = "NONE"
                picAdd_Click()
            End If
        End Try
    End Sub
    Private Sub picUpdate_Click() Handles picUpdate.Click
        If grv.GetFocusedRowCellValue("KDMUTATION") Is Nothing Then
            Exit Sub
        End If
        Dim frmMutation As New frmMutation
        Try
            frmMutation.LoadMe(FORM_MODE.FORM_MODE_EDIT, grv.GetFocusedRowCellValue("KDMUTATION"))
            frmMutation.ShowDialog(Me)
            fn_LoadSecurity()
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        Finally
            If Not frmMutation Is Nothing Then frmMutation.Dispose()
            frmMutation = Nothing

            Dim rowHandle As Integer = grv.LocateByValue(rowHandle, grv.Columns("KDMUTATION"), sCode)
            If rowHandle > 0 Then grv.FocusedRowHandle = rowHandle

            If sStatusSave = "NEW" Then
                sStatusSave = "NONE"
                picAdd_Click()
            End If
        End Try
    End Sub
    Private Sub picDelete_Click() Handles picDelete.Click
        If grv.GetFocusedRowCellValue("KDMUTATION") Is Nothing Then
            Exit Sub
        End If
        If MsgBox(Statement.DeleteQuestion, MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, Me.Text) = MsgBoxResult.No Then Exit Sub
        If fn_DeleteData(grv.GetFocusedRowCellValue("KDMUTATION")) = False Then
            MsgBox(Statement.DeleteFail, MsgBoxStyle.Exclamation, Me.Text)
            Exit Sub
        End If
        MsgBox(Statement.DeleteSuccess, MsgBoxStyle.Information, Me.Text)
        fn_LoadSecurity()
    End Sub
    Private Sub picPrint_Click() Handles picPrint.Click
        'Try
        '    If grv.GetFocusedRowCellValue("KDMUTATION") = String.Empty Then Exit Sub

        '    Dim rpt As New xtraMutation

        '    rpt.ShowPrintMarginsWarning = False
        '    rpt.Watermark.Text = sWATERMARK
        '    Dim ds = oMutation.GetData(grv.GetFocusedRowCellValue("KDMUTATION"))
        '    rpt.bindingSource.DataSource = ds
        '    Dim printTool As New DevExpress.XtraReports.UI.ReportPrintTool(rpt)
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