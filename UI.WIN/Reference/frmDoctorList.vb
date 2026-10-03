Imports DataAccess
Imports UI.WIN.MAIN.My.Resources
Imports System.Linq
Imports System.Data.SqlClient

Public Class frmDoctorList
    Implements ILanguage

    Private oFormMode As FORM_MODE = FORM_MODE.FORM_MODE_VIEW
    Private oDoctor As New Reference.clsDoctor

#Region "Function"
    Private Sub Me_Load() Handles Me.Load
        Me.Text = Doctor.TITLE

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
                      Where x.MODUL = "DOCTOR" _
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
            Me.Text = Doctor.TITLE

            grv.Columns("NAME_DISPLAY").Caption = Doctor.NAME_DISPLAY
            'grv.Columns("EMAIL").Caption = Doctor.EMAIL
            'grv.Columns("PHONE").Caption = Doctor.PHONE
            'grv.Columns("MOBILE").Caption = Doctor.MOBILE
            'grv.Columns("FAX").Caption = Doctor.FAX
            grv.Columns("OTHER").Caption = Doctor.OTHER
            'grv.Columns("WEBSITE").Caption = Doctor.WEBSITE
            'grv.Columns("BILL_STREET").Caption = Doctor.BILL_STREET
            'grv.Columns("BILL_CITY").Caption = Doctor.BILL_CITY
            'grv.Columns("BILL_STATE").Caption = Doctor.BILL_STATE
            'grv.Columns("BILL_ZIP").Caption = Doctor.BILL_ZIP
            'grv.Columns("BILL_COUNTRY").Caption = Doctor.BILL_COUNTRY
            grv.Columns("MEMO").Caption = Doctor.MEMO
            grv.Columns("ISACTIVE").Caption = Doctor.ISACTIVE
            'grv.Columns("SUBSPESIALIS").Caption = Doctor.SUBSPESIALIS
            'grv.Columns("DEPARTMENT").Caption = Doctor.KDDEPARTMENT

            ColumnChooserToolStripMenuItem.Text = Caption.ColumnChooser
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadData()
        'Try
        '    Dim ds = From x In oDoctor.GetData
        '             Select x.KDDOCTOR, x.NAME_DISPLAY, DEPARTMENT = x.M_DEPARTMENT.NAME_DISPLAY, SUBSPESIALIS = IIf(x.SUBSPESIALIS = 0, "Subspesialis", "Umum"), x.EMAIL, x.PHONE, x.MOBILE, x.FAX, x.OTHER, x.WEBSITE, x.BILL_STREET, x.BILL_CITY, x.BILL_STATE, x.BILL_ZIP, x.BILL_COUNTRY, x.MEMO, x.ISACTIVE

        '    grd.DataSource = ds.ToList

        '    fn_LoadFormatData()
        'Catch oErr As Exception
        '    MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        'End Try
        Try
            Dim oConn As New SqlConnection
            Dim oComm As New SqlCommand
            Dim da As SqlDataAdapter
            Dim ds As New DataSet
            Dim SQL As String
            Dim sConn As String = Decrypt(My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Software\SIMRS\SW\", "Database", "").ToString())

            oConn = New SqlConnection(sConn)

            If oConn.State = ConnectionState.Closed Then
                oConn.Open()
            End If

            SQL = "SELECT "
            SQL &= "KDDOCTOR, NAME_DISPLAY, ISACTIVE, MEMO, OTHER "
            SQL &= "FROM  "
            SQL &= "M_DOCTOR "
            'SQL &= "WHERE "
            'SQL &= "ISACTIVE = '1' "

            SQL &= "ORDER BY NAME_DISPLAY "

            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "M_DOCTOR")

            grd.MainView = grv
            grd.DataSource = ds.Tables("M_DOCTOR")
            grd.ForceInitialize()

            fn_LoadFormatData()

            If oConn.State = ConnectionState.Open Then
                oConn.Close()
            End If

        Catch oErr As Exception
            MsgBox("Load Dokter Data : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
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

        grv.Columns("KDDOCTOR").Visible = False
        'grv.Columns("EMAIL").Visible = False
        'grv.Columns("PHONE").Visible = False
        'grv.Columns("MOBILE").Visible = False
        'grv.Columns("FAX").Visible = False
        'grv.Columns("OTHER").Visible = False
        'grv.Columns("WEBSITE").Visible = False
        'grv.Columns("BILL_STREET").Visible = False
        'grv.Columns("BILL_CITY").Visible = False
        'grv.Columns("BILL_STATE").Visible = False
        'grv.Columns("BILL_ZIP").Visible = False
        'grv.Columns("BILL_COUNTRY").Visible = False
        'grv.Columns("MEMO").Visible = False
        'grv.Columns("ISACTIVE").Visible = False

        grv.Columns("KDDOCTOR").OptionsColumn.ShowInCustomizationForm = False
    End Sub
    Private Sub ColumnChooserToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles ColumnChooserToolStripMenuItem.Click
        grv.ShowCustomization()
    End Sub
    Private Function fn_DeleteData(ByVal sKDDOCTOR As String) As Boolean
        Try
            oDoctor.DeleteData(sKDDOCTOR)
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
        If grv.GetFocusedRowCellValue("KDDOCTOR") Is Nothing Then
            Exit Sub
        End If

        Dim frmDoctor As New frmDoctor
        Try
            frmDoctor.LoadMe(FORM_MODE.FORM_MODE_VIEW, grv.GetFocusedRowCellValue("KDDOCTOR"))
            frmDoctor.ShowDialog(Me)
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub picAdd_Click() Handles picAdd.Click
        Dim frmDoctor As New frmDoctor
        Try
            frmDoctor.LoadMe(FORM_MODE.FORM_MODE_ADD)
            frmDoctor.ShowDialog(Me)
            fn_LoadSecurity()
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        Finally
            If Not frmDoctor Is Nothing Then frmDoctor.Dispose()
            frmDoctor = Nothing

            Dim rowHandle As Integer = grv.LocateByValue(rowHandle, grv.Columns("NAME_DISPLAY"), sCode)
            If rowHandle > 0 Then grv.FocusedRowHandle = rowHandle

            If sStatusSave = "NEW" Then
                sStatusSave = "NONE"
                picAdd_Click()
            End If
        End Try
    End Sub
    Private Sub picUpdate_Click() Handles picUpdate.Click
        If grv.GetFocusedRowCellValue("KDDOCTOR") Is Nothing Then
            Exit Sub
        End If
        Dim frmDoctor As New frmDoctor
        Try
            frmDoctor.LoadMe(FORM_MODE.FORM_MODE_EDIT, grv.GetFocusedRowCellValue("KDDOCTOR"))
            frmDoctor.ShowDialog(Me)
            fn_LoadSecurity()
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        Finally
            If Not frmDoctor Is Nothing Then frmDoctor.Dispose()
            frmDoctor = Nothing

            Dim rowHandle As Integer = grv.LocateByValue(rowHandle, grv.Columns("NAME_DISPLAY"), sCode)
            If rowHandle > 0 Then grv.FocusedRowHandle = rowHandle

            If sStatusSave = "NEW" Then
                sStatusSave = "NONE"
                picAdd_Click()
            End If
        End Try
    End Sub
    Private Sub picDelete_Click() Handles picDelete.Click
        If grv.GetFocusedRowCellValue("KDDOCTOR") Is Nothing Then
            Exit Sub
        End If
        If MsgBox(Statement.DeleteQuestion, MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, Me.Text) = MsgBoxResult.No Then Exit Sub
        If fn_DeleteData(grv.GetFocusedRowCellValue("KDDOCTOR")) = False Then
            MsgBox(Statement.DeleteFail, MsgBoxStyle.Exclamation, Me.Text)
            Exit Sub
        End If
        MsgBox(Statement.DeleteSuccess, MsgBoxStyle.Information, Me.Text)
        fn_LoadSecurity()
    End Sub
    Private Sub picPrint_Click() Handles picPrint.Click
        'Try
        '    Dim xtraDoctor As New xtraDoctor

        '    Dim sKDDOCTOR As New List(Of String)
        '    For i As Integer = 0 To grv.RowCount - 1
        '        sKDDOCTOR.Add(grv.GetRowCellValue(i, "KDDOCTOR"))
        '    Next

        '    Dim ds = oDoctor.GetData.Where(Function(x) sKDDOCTOR.Contains(x.KDDOCTOR)).ToList

        '    xtraDoctor.bindingSource.DataSource = ds
        '    Dim printTool As New DevExpress.XtraReports.UI.ReportPrintTool(xtraDoctor)
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