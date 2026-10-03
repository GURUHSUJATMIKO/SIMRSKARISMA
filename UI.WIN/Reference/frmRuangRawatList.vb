Imports DataAccess
Imports UI.WIN.MAIN.My.Resources
Imports System.Linq

Public Class frmRuangRawatList
    Implements ILanguage

    Private oFormMode As FORM_MODE = FORM_MODE.FORM_MODE_VIEW
    Private oRuangRawat As New Reference.clsRuangRawat

#Region "Function"
    Private Sub Me_Load() Handles Me.Load
        Me.Text = RuangRawat.TITLE

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

            Dim ds = (From x In oOtority.GetDataDetail
                      Join y In oUser.GetData
                      On x.KDOTORITY Equals y.KDOTORITY
                      Where x.MODUL = "RUANGRAWAT" _
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
            Me.Text = RuangRawat.TITLE

            grv.Columns("NAME_DISPLAY").Caption = RuangRawat.NAME_DISPLAY
            grv.Columns("EMAIL").Caption = RuangRawat.EMAIL
            grv.Columns("PHONE").Caption = RuangRawat.PHONE
            grv.Columns("MOBILE").Caption = RuangRawat.MOBILE
            grv.Columns("FAX").Caption = RuangRawat.FAX
            grv.Columns("OTHER").Caption = RuangRawat.OTHER
            grv.Columns("WEBSITE").Caption = RuangRawat.WEBSITE
            grv.Columns("BILL_STREET").Caption = RuangRawat.BILL_STREET
            grv.Columns("BILL_CITY").Caption = RuangRawat.BILL_CITY
            grv.Columns("BILL_STATE").Caption = RuangRawat.BILL_STATE
            grv.Columns("BILL_ZIP").Caption = RuangRawat.BILL_ZIP
            grv.Columns("BILL_COUNTRY").Caption = RuangRawat.BILL_COUNTRY
            grv.Columns("MEMO").Caption = RuangRawat.MEMO
            grv.Columns("ISACTIVE").Caption = RuangRawat.ISACTIVE

            ColumnChooserToolStripMenuItem.Text = Caption.ColumnChooser
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadData()
        Try
            Dim ds = From x In oRuangRawat.GetData
                     Select x.KDRUANGRAWAT, x.NAME_DISPLAY, x.EMAIL, x.PHONE, x.MOBILE, x.FAX, x.OTHER, x.WEBSITE, x.BILL_STREET, x.BILL_CITY, x.BILL_STATE, x.BILL_ZIP, x.BILL_COUNTRY, x.MEMO, x.ISACTIVE

            grd.DataSource = ds.OrderBy(Function(x) x.NAME_DISPLAY).ToList()

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

        grv.Columns("KDRUANGRAWAT").Visible = False
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

        grv.Columns("KDRUANGRAWAT").OptionsColumn.ShowInCustomizationForm = False
    End Sub
    Private Sub ColumnChooserToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles ColumnChooserToolStripMenuItem.Click
        grv.ShowCustomization()
    End Sub
    Private Function fn_DeleteData(ByVal sKDRuangRawat As String) As Boolean
        Try
            oRuangRawat.DeleteData(sKDRuangRawat)
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
        If grv.GetFocusedRowCellValue("KDRUANGRAWAT") Is Nothing Then
            Exit Sub
        End If

        Dim frmRuangRawat As New frmRuangRawat
        Try
            frmRuangRawat.LoadMe(FORM_MODE.FORM_MODE_VIEW, grv.GetFocusedRowCellValue("KDRUANGRAWAT"))
            frmRuangRawat.ShowDialog(Me)
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub picAdd_Click() Handles picAdd.Click
        Dim frmRuangRawat As New frmRuangRawat
        Try
            frmRuangRawat.LoadMe(FORM_MODE.FORM_MODE_ADD)
            frmRuangRawat.ShowDialog(Me)
            fn_LoadSecurity()
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        Finally
            If Not frmRuangRawat Is Nothing Then frmRuangRawat.Dispose()
            frmRuangRawat = Nothing

            Dim rowHandle As Integer = grv.LocateByValue(rowHandle, grv.Columns("NAME_DISPLAY"), sCode)
            If rowHandle > 0 Then grv.FocusedRowHandle = rowHandle

            If sStatusSave = "NEW" Then
                sStatusSave = "NONE"
                picAdd_Click()
            End If
        End Try
    End Sub
    Private Sub picUpdate_Click() Handles picUpdate.Click
        If grv.GetFocusedRowCellValue("KDRUANGRAWAT") Is Nothing Then
            Exit Sub
        End If
        Dim frmRuangRawat As New frmRuangRawat
        Try
            frmRuangRawat.LoadMe(FORM_MODE.FORM_MODE_EDIT, grv.GetFocusedRowCellValue("KDRUANGRAWAT"))
            frmRuangRawat.ShowDialog(Me)
            fn_LoadSecurity()
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        Finally
            If Not frmRuangRawat Is Nothing Then frmRuangRawat.Dispose()
            frmRuangRawat = Nothing

            Dim rowHandle As Integer = grv.LocateByValue(rowHandle, grv.Columns("NAME_DISPLAY"), sCode)
            If rowHandle > 0 Then grv.FocusedRowHandle = rowHandle

            If sStatusSave = "NEW" Then
                sStatusSave = "NONE"
                picAdd_Click()
            End If
        End Try
    End Sub
    Private Sub picDelete_Click() Handles picDelete.Click
        If grv.GetFocusedRowCellValue("KDRUANGRAWAT") Is Nothing Then
            Exit Sub
        End If
        If MsgBox(Statement.DeleteQuestion, MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, Me.Text) = MsgBoxResult.No Then Exit Sub
        If fn_DeleteData(grv.GetFocusedRowCellValue("KDRUANGRAWAT")) = False Then
            MsgBox(Statement.DeleteFail, MsgBoxStyle.Exclamation, Me.Text)
            Exit Sub
        End If
        MsgBox(Statement.DeleteSuccess, MsgBoxStyle.Information, Me.Text)
        fn_LoadSecurity()
    End Sub
    Private Sub picPrint_Click() Handles picPrint.Click
        'Try
        '    Dim xtraRuangRawat As New xtraRuangRawat

        '    Dim sKDRuangRawat As New List(Of String)
        '    For i As Integer = 0 To grv.RowCount - 1
        '        sKDRuangRawat.Add(grv.GetRowCellValue(i, "KDRUANGRAWAT"))
        '    Next

        '    Dim ds = oRuangRawat.GetData.Where(Function(x) sKDRuangRawat.Contains(x.KDRuangRawat)).ToList

        '    xtraRuangRawat.bindingSource.DataSource = ds
        '    Dim printTool As New DevExpress.XtraReports.UI.ReportPrintTool(xtraRuangRawat)
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