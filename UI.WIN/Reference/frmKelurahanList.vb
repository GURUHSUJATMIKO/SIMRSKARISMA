Imports DataAccess
Imports UI.WIN.MAIN.My.Resources
Imports System.Linq

Public Class frmKelurahanList
    Implements ILanguage

    Private oFormMode As FORM_MODE = FORM_MODE.FORM_MODE_VIEW
    Private oKELURAHAN As New Reference.clsKelurahan

#Region "Function"
    Private Sub Me_Load() Handles Me.Load
        Me.Text = Kelurahan.TITLE

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
                      Where x.MODUL = "KELURAHAN" _
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
            Me.Text = Kelurahan.TITLE

            grv.Columns("PROPINSI").Caption = Propinsi.MEMO
            grv.Columns("KABUPATEN").Caption = Kabupaten.MEMO
            grv.Columns("KECAMATAN").Caption = Kecamatan.MEMO
            grv.Columns("KODEPOS").Caption = Kelurahan.KODEPOS
            grv.Columns("MEMO").Caption = Kelurahan.MEMO
            grv.Columns("ISACTIVE").Caption = Kelurahan.ISACTIVE
            grv.Columns("ISDEFAULT").Caption = Kelurahan.ISDEFAULT

            ColumnChooserToolStripMenuItem.Text = Caption.ColumnChooser
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadData()
        Try
            Dim ds = From x In oKELURAHAN.GetData
                     Select x.KDKELURAHAN, PROPINSI = x.M_KECAMATAN.M_KABUPATEN.M_PROPINSI.MEMO, KABUPATEN = x.M_KECAMATAN.M_KABUPATEN.MEMO, KECAMATAN = x.M_KECAMATAN.MEMO, x.KODEPOS, x.MEMO, x.ISACTIVE, x.ISDEFAULT

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

        grv.Columns("KDKELURAHAN").Visible = False
        grv.Columns("ISDEFAULT").Visible = False
        grv.Columns("ISACTIVE").Visible = False

        grv.Columns("KDKELURAHAN").OptionsColumn.ShowInCustomizationForm = False
    End Sub
    Private Sub ColumnChooserToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles ColumnChooserToolStripMenuItem.Click
        grv.ShowCustomization()
    End Sub
    Private Function fn_DeleteData(ByVal sKDKELURAHAN As String) As Boolean
        Try
            oKELURAHAN.DeleteData(sKDKELURAHAN)
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
        If grv.GetFocusedRowCellValue("KDKELURAHAN") Is Nothing Then
            Exit Sub
        End If

        Dim frmKELURAHAN As New frmKELURAHAN
        Try
            frmKELURAHAN.LoadMe(FORM_MODE.FORM_MODE_VIEW, grv.GetFocusedRowCellValue("KDKELURAHAN"))
            frmKELURAHAN.ShowDialog(Me)
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub picAdd_Click() Handles picAdd.Click
        Dim frmKELURAHAN As New frmKELURAHAN
        Try
            frmKELURAHAN.LoadMe(FORM_MODE.FORM_MODE_ADD)
            frmKELURAHAN.ShowDialog(Me)
            fn_LoadSecurity()
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        Finally
            If Not frmKELURAHAN Is Nothing Then frmKELURAHAN.Dispose()
            frmKELURAHAN = Nothing

            Dim rowHandle As Integer = grv.LocateByValue(rowHandle, grv.Columns("MEMO"), sCode)
            If rowHandle > 0 Then grv.FocusedRowHandle = rowHandle

            If sStatusSave = "NEW" Then
                sStatusSave = "NONE"
                picAdd_Click()
            End If
        End Try
    End Sub
    Private Sub picUpdate_Click() Handles picUpdate.Click
        If grv.GetFocusedRowCellValue("KDKELURAHAN") Is Nothing Then
            Exit Sub
        End If
        Dim frmKELURAHAN As New frmKELURAHAN
        Try
            frmKELURAHAN.LoadMe(FORM_MODE.FORM_MODE_EDIT, grv.GetFocusedRowCellValue("KDKELURAHAN"))
            frmKELURAHAN.ShowDialog(Me)
            fn_LoadSecurity()
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        Finally
            If Not frmKELURAHAN Is Nothing Then frmKELURAHAN.Dispose()
            frmKELURAHAN = Nothing

            Dim rowHandle As Integer = grv.LocateByValue(rowHandle, grv.Columns("MEMO"), sCode)
            If rowHandle > 0 Then grv.FocusedRowHandle = rowHandle

            If sStatusSave = "NEW" Then
                sStatusSave = "NONE"
                picAdd_Click()
            End If
        End Try
    End Sub
    Private Sub picDelete_Click() Handles picDelete.Click
        If grv.GetFocusedRowCellValue("KDKELURAHAN") Is Nothing Then
            Exit Sub
        End If
        If MsgBox(Statement.DeleteQuestion, MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, Me.Text) = MsgBoxResult.No Then Exit Sub
        If fn_DeleteData(grv.GetFocusedRowCellValue("KDKELURAHAN")) = False Then
            MsgBox(Statement.DeleteFail, MsgBoxStyle.Exclamation, Me.Text)
            Exit Sub
        End If
        MsgBox(Statement.DeleteSuccess, MsgBoxStyle.Information, Me.Text)
        fn_LoadSecurity()
    End Sub
    Private Sub picPrint_Click() Handles picPrint.Click
        'Try
        '    Dim xtraKELURAHAN As New xtraKELURAHAN

        '    Dim sKDKELURAHAN As New List(Of String)
        '    For i As Integer = 0 To grv.RowCount - 1
        '        sKDKELURAHAN.Add(grv.GetRowCellValue(i, "KDKELURAHAN"))
        '    Next

        '    Dim ds = oKELURAHAN.GetData.Where(Function(x) sKDKELURAHAN.Contains(x.KDKELURAHAN)).ToList

        '    xtraKELURAHAN.bindingSource.DataSource = ds
        '    Dim printTool As New DevExpress.XtraReports.UI.ReportPrintTool(xtraKELURAHAN)
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