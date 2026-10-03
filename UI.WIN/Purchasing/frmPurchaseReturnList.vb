Imports DataAccess
Imports UI.WIN.MAIN.My.Resources
Imports System.Linq

Public Class frmPurchaseReturnList
    Implements ILanguage

    Private oFormMode As FORM_MODE = FORM_MODE.FORM_MODE_VIEW
    Private oPurchaseReturn As New Purchasing.clsPurchaseReturn

#Region "Function"
    Private Sub Me_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.Text = PurchaseReturn.TITLE

        fn_LoadSecurity()

        Try
            grv.RestoreLayoutFromRegistry("HKEY_CURRENT_USER\Software\BUDIJAYAGROUP\SW\" & sUserID & "\" & Me.Text)
        Catch ex As Exception

        End Try
    End Sub
    Private Sub Me_FormClosed(sender As System.Object, e As System.Windows.Forms.FormClosedEventArgs) Handles MyBase.FormClosed
        Try
            grv.SaveLayoutToRegistry("HKEY_CURRENT_USER\Software\BUDIJAYAGROUP\SW\" & sUserID & "\" & Me.Text)
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
                     Where x.MODUL = "PR" _
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

            Dim dsPrice = (From x In oOtority.GetDataDetail _
                    Join y In oUser.GetData _
                    On x.KDOTORITY Equals y.KDOTORITY _
                    Where x.MODUL = "ITEM_PRICE" _
                    And y.KDUSER = sUserID _
                    Select x.ISADD, x.ISDELETE, x.ISUPDATE, x.ISPRINT, x.ISVIEW).FirstOrDefault


            Try
                If dsPrice.ISVIEW = True Then
                    grv.Columns("SUBTOTAL").Visible = True
                    grv.Columns("DISCOUNT").Visible = True
                    grv.Columns("TAX").Visible = True
                    grv.Columns("GRANDTOTAL").Visible = True

                    grv.Columns("SUBTOTAL").OptionsColumn.ShowInCustomizationForm = True
                    grv.Columns("DISCOUNT").OptionsColumn.ShowInCustomizationForm = True
                    grv.Columns("TAX").OptionsColumn.ShowInCustomizationForm = True
                    grv.Columns("GRANDTOTAL").OptionsColumn.ShowInCustomizationForm = True
                Else
                    grv.Columns("SUBTOTAL").Visible = False
                    grv.Columns("DISCOUNT").Visible = False
                    grv.Columns("TAX").Visible = False
                    grv.Columns("GRANDTOTAL").Visible = False

                    grv.Columns("SUBTOTAL").OptionsColumn.ShowInCustomizationForm = False
                    grv.Columns("DISCOUNT").OptionsColumn.ShowInCustomizationForm = False
                    grv.Columns("TAX").OptionsColumn.ShowInCustomizationForm = False
                    grv.Columns("GRANDTOTAL").OptionsColumn.ShowInCustomizationForm = False
                End If
            Catch ex As Exception
                MsgBox(Statement.SecurityNotInstalled, MsgBoxStyle.Exclamation, Me.Text)

                grv.Columns("SUBTOTAL").Visible = False
                grv.Columns("DISCOUNT").Visible = False
                grv.Columns("TAX").Visible = False
                grv.Columns("GRANDTOTAL").Visible = False

                grv.Columns("SUBTOTAL").OptionsColumn.ShowInCustomizationForm = False
                grv.Columns("DISCOUNT").OptionsColumn.ShowInCustomizationForm = False
                grv.Columns("TAX").OptionsColumn.ShowInCustomizationForm = False
                grv.Columns("GRANDTOTAL").OptionsColumn.ShowInCustomizationForm = False
            End Try
        Catch oErr As Exception
            MsgBox(Statement.SecurityNotInstalled, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Public Sub fn_LoadLanguage() Implements ILanguage.fn_LoadLanguage
        Try
            Me.Text = PurchaseReturn.TITLE

            fn_LoadLanguageMaster()
            fn_LoadLanguageDetail()
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Public Sub fn_LoadLanguageMaster()
        Try
            grv.Columns("KDPR").Caption = PurchaseReturn.KDPR
            grv.Columns("TANGGAL").Caption = PurchaseReturn.TANGGAL
            grv.Columns("KDVENDOR").Caption = PurchaseReturn.KDVENDOR
            grv.Columns("KDWAREHOUSE").Caption = PurchaseReturn.KDWAREHOUSE
            grv.Columns("MEMO").Caption = PurchaseReturn.MEMO
            grv.Columns("SUBTOTAL").Caption = PurchaseReturn.SUBTOTAL
            grv.Columns("DISCOUNT").Caption = PurchaseReturn.DISCOUNT
            grv.Columns("TAX").Caption = PurchaseReturn.TAX
            grv.Columns("GRANDTOTAL").Caption = PurchaseReturn.GRANDTOTAL
            grv.Columns("KDUSER").Caption = Caption.User
        Catch oErr As Exception

        End Try
    End Sub
    Public Sub fn_LoadLanguageDetail()
        Try
            grv1.Columns("QTY").Caption = PurchaseReturn.DETAIL_QTY
            grv1.Columns("PRICE").Caption = PurchaseReturn.DETAIL_PRICE
            grv1.Columns("SUBTOTAL").Caption = PurchaseReturn.DETAIL_SUBTOTAL
            grv1.Columns("DISCOUNT").Caption = PurchaseReturn.DETAIL_DISCOUNT
            grv1.Columns("GRANDTOTAL").Caption = PurchaseReturn.DETAIL_GRANDTOTAL
            grv1.Columns("REMARKS").Caption = PurchaseReturn.DETAIL_REMARKS

            grv1.Columns("M_ITEM.NMITEM1").Caption = Item.NMITEM1
            grv1.Columns("M_ITEM.NMITEM2").Caption = Item.NMITEM2
            'grv1.Columns("M_ITEM.NMITEM3").Caption = Item.NMITEM3
            grv1.Columns("M_UOM.MEMO").Caption = PurchaseReturn.DETAIL_KDUOM
        Catch oErr As Exception

        End Try
    End Sub
    Private Sub fn_LoadData()
        Try
            Dim ds = From x In oPurchaseReturn.GetData _
                     Select x.KDPR, TANGGAL = x.DATE, KDVENDOR = x.M_VENDOR.NAME_DISPLAY, KDWAREHOUSE = x.M_WAREHOUSE.NAME_DISPLAY, x.MEMO, Details = x.P_PR_Ds, x.KDUSER, x.SUBTOTAL, x.DISCOUNT, x.TAX, x.GRANDTOTAL, x.PAYAMOUNT
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

        grv.Columns("PAYAMOUNT").Visible = False
        grv.Columns("PAYAMOUNT").OptionsColumn.ShowInCustomizationForm = False
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
        grv1.Columns("KDPR").Visible = False
        grv1.Columns("M_ITEM").Visible = False
        grv1.Columns("M_UOM").Visible = False
        grv1.Columns("P_PR_H").Visible = False
        grv1.Columns("KDITEM").Visible = False
        grv1.Columns("KDUOM").Visible = False

        grv1.Columns("DATECREATED").OptionsColumn.ShowInCustomizationForm = False
        grv1.Columns("DATEUPDATED").OptionsColumn.ShowInCustomizationForm = False
        grv1.Columns("SEQ").OptionsColumn.ShowInCustomizationForm = False
        grv1.Columns("KDPR").OptionsColumn.ShowInCustomizationForm = False
        grv1.Columns("M_ITEM").OptionsColumn.ShowInCustomizationForm = False
        grv1.Columns("M_UOM").OptionsColumn.ShowInCustomizationForm = False
        grv1.Columns("P_PR_H").OptionsColumn.ShowInCustomizationForm = False
        grv1.Columns("KDITEM").OptionsColumn.ShowInCustomizationForm = False
        grv1.Columns("KDUOM").OptionsColumn.ShowInCustomizationForm = False

        grv1.Columns("M_ITEM.NMITEM1").VisibleIndex = 0
        grv1.Columns("M_ITEM.NMITEM2").VisibleIndex = 1
        'grv1.Columns("M_ITEM.NMITEM3").VisibleIndex = 2
        grv1.Columns("M_UOM.MEMO").VisibleIndex = 4

        Dim oOtority As New Setting.clsOtority
        Dim oUser As New Setting.clsUser

        Dim dsPrice = (From x In oOtority.GetDataDetail _
                Join y In oUser.GetData _
                On x.KDOTORITY Equals y.KDOTORITY _
                Where x.MODUL = "ITEM_PRICE" _
                And y.KDUSER = sUserID _
                Select x.ISADD, x.ISDELETE, x.ISUPDATE, x.ISPRINT, x.ISVIEW).FirstOrDefault


        Try
            If dsPrice.ISVIEW = True Then
                grv1.Columns("PRICE").Visible = True
                grv1.Columns("SUBTOTAL").Visible = True
                grv1.Columns("DISCOUNT").Visible = True
                grv1.Columns("GRANDTOTAL").Visible = True

                grv1.Columns("PRICE").OptionsColumn.ShowInCustomizationForm = True
                grv1.Columns("SUBTOTAL").OptionsColumn.ShowInCustomizationForm = True
                grv1.Columns("DISCOUNT").OptionsColumn.ShowInCustomizationForm = True
                grv1.Columns("GRANDTOTAL").OptionsColumn.ShowInCustomizationForm = True
            Else
                grv1.Columns("PRICE").Visible = False
                grv1.Columns("SUBTOTAL").Visible = False
                grv1.Columns("DISCOUNT").Visible = False
                grv1.Columns("GRANDTOTAL").Visible = False

                grv1.Columns("PRICE").OptionsColumn.ShowInCustomizationForm = False
                grv1.Columns("SUBTOTAL").OptionsColumn.ShowInCustomizationForm = False
                grv1.Columns("DISCOUNT").OptionsColumn.ShowInCustomizationForm = False
                grv1.Columns("GRANDTOTAL").OptionsColumn.ShowInCustomizationForm = False
            End If
        Catch ex As Exception
            grv1.Columns("PRICE").Visible = False
            grv1.Columns("SUBTOTAL").Visible = False
            grv1.Columns("DISCOUNT").Visible = False
            grv1.Columns("GRANDTOTAL").Visible = False

            grv1.Columns("PRICE").OptionsColumn.ShowInCustomizationForm = False
            grv1.Columns("SUBTOTAL").OptionsColumn.ShowInCustomizationForm = False
            grv1.Columns("DISCOUNT").OptionsColumn.ShowInCustomizationForm = False
            grv1.Columns("GRANDTOTAL").OptionsColumn.ShowInCustomizationForm = False
        End Try
    End Sub
    Private Sub MasterColumnChooserToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles MasterColumnChooserToolStripMenuItem.Click
        grv.ShowCustomization()
    End Sub
    Private Sub DetailColumnChooserToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles DetailColumnChooserToolStripMenuItem.Click
        grv1.ShowCustomization()
    End Sub
    Private Function fn_DeleteData(ByVal sKDPR As String) As Boolean
        Try
            oPurchaseReturn.DeleteData(sKDPR)

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
            If CDec(grv.GetRowCellValue(e.RowHandle, "GRANDTOTAL")) - CDec(grv.GetRowCellValue(e.RowHandle, "PAYAMOUNT")) = 0 Then
                e.Appearance.BackColor = Color.LightGreen
            Else
                e.Appearance.BackColor = Color.LightPink
            End If
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
    Private Sub grv_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs)
        If grv.GetFocusedRowCellValue("KDPR") Is Nothing Then
            Exit Sub
        End If

        Dim frmPurchaseReturn As New frmPurchaseReturn
        Try
            frmPurchaseReturn.LoadMe(FORM_MODE.FORM_MODE_VIEW, grv.GetFocusedRowCellValue("KDPR"))
            frmPurchaseReturn.ShowDialog(Me)
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub picAdd_Click() Handles picAdd.Click
        Dim frmPurchaseReturn As New frmPurchaseReturn
        Try
            frmPurchaseReturn.LoadMe(FORM_MODE.FORM_MODE_ADD)
            frmPurchaseReturn.ShowDialog(Me)
            fn_LoadSecurity()
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        Finally
            If Not frmPurchaseReturn Is Nothing Then frmPurchaseReturn.Dispose()
            frmPurchaseReturn = Nothing

            Dim rowHandle As Integer = grv.LocateByValue(rowHandle, grv.Columns("KDPR"), sCode)
            If rowHandle > 0 Then grv.FocusedRowHandle = rowHandle

            If sStatusSave = "NEW" Then
                sStatusSave = "NONE"
                picAdd_Click()
            End If
        End Try
    End Sub
    Private Sub picUpdate_Click() Handles picUpdate.Click
        If grv.GetFocusedRowCellValue("KDPR") Is Nothing Then
            Exit Sub
        End If
        Dim frmPurchaseReturn As New frmPurchaseReturn
        Try
            frmPurchaseReturn.LoadMe(FORM_MODE.FORM_MODE_EDIT, grv.GetFocusedRowCellValue("KDPR"))
            frmPurchaseReturn.ShowDialog(Me)
            fn_LoadSecurity()
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        Finally
            If Not frmPurchaseReturn Is Nothing Then frmPurchaseReturn.Dispose()
            frmPurchaseReturn = Nothing

            Dim rowHandle As Integer = grv.LocateByValue(rowHandle, grv.Columns("KDPR"), sCode)
            If rowHandle > 0 Then grv.FocusedRowHandle = rowHandle

            If sStatusSave = "NEW" Then
                sStatusSave = "NONE"
                picAdd_Click()
            End If
        End Try
    End Sub
    Private Sub picDelete_Click() Handles picDelete.Click
        If grv.GetFocusedRowCellValue("KDPR") Is Nothing Then
            Exit Sub
        End If
        If MsgBox(Statement.DeleteQuestion, MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, Me.Text) = MsgBoxResult.No Then Exit Sub
        If fn_DeleteData(grv.GetFocusedRowCellValue("KDPR")) = False Then
            MsgBox(Statement.DeleteFail, MsgBoxStyle.Exclamation, Me.Text)
            Exit Sub
        End If
        MsgBox(Statement.DeleteSuccess, MsgBoxStyle.Information, Me.Text)
        fn_LoadSecurity()
    End Sub
    Private Sub picPrint_Click() Handles picPrint.Click
        'Try
        '    If grv.GetFocusedRowCellValue("KDPR") = String.Empty Then Exit Sub

        '    Dim rpt As New xtraPurchaseReturn

        '    rpt.ShowPrintMarginsWarning = False
        '    rpt.Watermark.Text = sWATERMARK
        '    Dim ds = oPurchaseReturn.GetData(grv.GetFocusedRowCellValue("KDPR"))
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