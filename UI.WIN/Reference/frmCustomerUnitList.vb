Imports DataAccess
Imports UI.WIN.MAIN.My.Resources
Imports System.Linq

Public Class frmCustomerUnitList
    Implements ILanguage

    Private oFormMode As FORM_MODE = FORM_MODE.FORM_MODE_VIEW
    Private oCustomerUnit As New Reference.clsCustomerUnit

#Region "Function"
    Private Sub Me_Load() Handles Me.Load
        Me.Text = Customerunit.TITLE

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

            Dim ds = (From x In oOtority.GetDataDetail
                      Join y In oUser.GetData
                     On x.KDOTORITY Equals y.KDOTORITY
                      Where x.MODUL = "CUSTOMERUNIT" _
                     And y.KDUSER = sUserID
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
            Me.Text = CustomerUnit.TITLE

            grv.Columns("NAME_DISPLAY").Caption = CustomerUnit.NAME_DISPLAY
            grv.Columns("EMAIL").Caption = CustomerUnit.EMAIL
            grv.Columns("PHONE").Caption = CustomerUnit.PHONE
            grv.Columns("MOBILE").Caption = CustomerUnit.MOBILE
            grv.Columns("FAX").Caption = CustomerUnit.FAX
            grv.Columns("OTHER").Caption = CustomerUnit.OTHER
            grv.Columns("WEBSITE").Caption = CustomerUnit.WEBSITE
            grv.Columns("BILL_STREET").Caption = CustomerUnit.BILL_STREET
            grv.Columns("BILL_CITY").Caption = CustomerUnit.BILL_CITY
            grv.Columns("BILL_STATE").Caption = CustomerUnit.BILL_STATE
            grv.Columns("BILL_ZIP").Caption = CustomerUnit.BILL_ZIP
            grv.Columns("BILL_COUNTRY").Caption = CustomerUnit.BILL_COUNTRY
            grv.Columns("MEMO").Caption = CustomerUnit.MEMO
            grv.Columns("ISACTIVE").Caption = CustomerUnit.ISACTIVE

            ColumnChooserToolStripMenuItem.Text = Caption.ColumnChooser
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadData()
        Try
            Dim ds = From x In oCustomerUnit.GetData
                     Select x.KDCUSTOMER_UNIT, x.NAME_DISPLAY, x.EMAIL, x.PHONE, x.MOBILE, x.FAX, x.OTHER, x.WEBSITE, x.BILL_STREET, x.BILL_CITY, x.BILL_STATE, x.BILL_ZIP, x.BILL_COUNTRY, x.MEMO, x.ISACTIVE

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

        grv.Columns("KDCUSTOMER_UNIT").Visible = False
        grv.Columns("EMAIL").Visible = False
        grv.Columns("PHONE").Visible = False
        grv.Columns("MOBILE").Visible = False
        grv.Columns("FAX").Visible = False
        grv.Columns("OTHER").Visible = False
        grv.Columns("WEBSITE").Visible = False
        grv.Columns("BILL_STREET").Visible = False
        grv.Columns("BILL_CITY").Visible = False
        grv.Columns("BILL_STATE").Visible = False
        grv.Columns("BILL_ZIP").Visible = False
        grv.Columns("BILL_COUNTRY").Visible = False
        grv.Columns("MEMO").Visible = False
        grv.Columns("ISACTIVE").Visible = False

        grv.Columns("KDCUSTOMER_UNIT").OptionsColumn.ShowInCustomizationForm = False
    End Sub
    Private Sub ColumnChooserToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles ColumnChooserToolStripMenuItem.Click
        grv.ShowCustomization()
    End Sub
    Private Function fn_DeleteData(ByVal sKDCustomerUnit As String) As Boolean
        Try
            oCustomerUnit.DeleteData(sKDCustomerUnit)
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
        If grv.GetFocusedRowCellValue("KDCUSTOMER_UNIT") Is Nothing Then
            Exit Sub
        End If

        Dim frmCustomerUnit As New frmCustomerUnit
        Try
            frmCustomerUnit.LoadMe(FORM_MODE.FORM_MODE_VIEW, grv.GetFocusedRowCellValue("KDCUSTOMER_UNIT"))
            frmCustomerUnit.ShowDialog(Me)
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub picAdd_Click() Handles picAdd.Click
        Dim frmCustomerUnit As New frmCustomerUnit
        Try
            frmCustomerUnit.LoadMe(FORM_MODE.FORM_MODE_ADD)
            frmCustomerUnit.ShowDialog(Me)
            fn_LoadSecurity()
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        Finally
            If Not frmCustomerUnit Is Nothing Then frmCustomerUnit.Dispose()
            frmCustomerUnit = Nothing

            Dim rowHandle As Integer = grv.LocateByValue(rowHandle, grv.Columns("NAME_DISPLAY"), sCode)
            If rowHandle > 0 Then grv.FocusedRowHandle = rowHandle

            If sStatusSave = "NEW" Then
                sStatusSave = "NONE"
                picAdd_Click()
            End If
        End Try
    End Sub
    Private Sub picUpdate_Click() Handles picUpdate.Click
        If grv.GetFocusedRowCellValue("KDCUSTOMER_UNIT") Is Nothing Then
            Exit Sub
        End If
        Dim frmCustomerUnit As New frmCustomerUnit
        Try
            frmCustomerUnit.LoadMe(FORM_MODE.FORM_MODE_EDIT, grv.GetFocusedRowCellValue("KDCUSTOMER_UNIT"))
            frmCustomerUnit.ShowDialog(Me)
            fn_LoadSecurity()
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        Finally
            If Not frmCustomerUnit Is Nothing Then frmCustomerUnit.Dispose()
            frmCustomerUnit = Nothing

            Dim rowHandle As Integer = grv.LocateByValue(rowHandle, grv.Columns("NAME_DISPLAY"), sCode)
            If rowHandle > 0 Then grv.FocusedRowHandle = rowHandle

            If sStatusSave = "NEW" Then
                sStatusSave = "NONE"
                picAdd_Click()
            End If
        End Try
    End Sub
    Private Sub picDelete_Click() Handles picDelete.Click
        If grv.GetFocusedRowCellValue("KDCUSTOMER_UNIT") Is Nothing Then
            Exit Sub
        End If
        If MsgBox(Statement.DeleteQuestion, MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, Me.Text) = MsgBoxResult.No Then Exit Sub
        If fn_DeleteData(grv.GetFocusedRowCellValue("KDCUSTOMER_UNIT")) = False Then
            MsgBox(Statement.DeleteFail, MsgBoxStyle.Exclamation, Me.Text)
            Exit Sub
        End If
        MsgBox(Statement.DeleteSuccess, MsgBoxStyle.Information, Me.Text)
        fn_LoadSecurity()
    End Sub
    Private Sub picPrint_Click() Handles picPrint.Click
        'Try
        '    Dim xtraCustomerUnit As New xtraCustomerUnit

        '    Dim sKDCustomerUnit As New List(Of String)
        '    For i As Integer = 0 To grv.RowCount - 1
        '        sKDCustomerUnit.Add(grv.GetRowCellValue(i, "KDCUSTOMER_UNIT"))
        '    Next

        '    Dim ds = oCustomerUnit.GetData.Where(Function(x) sKDCustomerUnit.Contains(x.KDCustomerUnit)).ToList

        '    xtraCustomerUnit.bindingSource.DataSource = ds
        '    Dim printTool As New DevExpress.XtraReports.UI.ReportPrintTool(xtraCustomerUnit)
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