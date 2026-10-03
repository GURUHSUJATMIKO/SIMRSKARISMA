Imports DataAccess
Imports UI.WIN.MAIN.My.Resources
Imports System.Linq
Imports System.Data.SqlClient

Public Class frmLaporanTindakanList
    Implements ILanguage

    Private oFormMode As FORM_MODE = FORM_MODE.FORM_MODE_VIEW
    Private oLaporanTindakan As New EMedrek.clsS_DIGITAL_OK_LAPORANTINDAKAN
    Private sKDDOCTOR_INPUT As String = String.Empty
    Private sKDDOCTOR_DPJPUTAMA As String = String.Empty
    Private sREKAMMEDIS As String = String.Empty
    Private sREGISTERADD As String = String.Empty
    Private sNAMA As String = String.Empty
    Private sJENISKELAMIN As String = String.Empty
    Private sTANGGALLAHIR As DateTime = Now

#Region "Function"
    Public Sub fn_LoadMe(ByVal KDDOCTOR_INPUT As String, ByVal KDDOCTOR_DPJPUTAMA As String, ByVal Noregister As String, ByVal RM As String, ByVal NAMA As String, ByVal JENISKELAMIN As String, ByVal TANGGALLAHIR As DateTime)
        sKDDOCTOR_INPUT = KDDOCTOR_INPUT
        sKDDOCTOR_DPJPUTAMA = KDDOCTOR_DPJPUTAMA
        sREGISTERADD = Noregister
        sREKAMMEDIS = RM
        sNAMA = NAMA
        sJENISKELAMIN = JENISKELAMIN
        sTANGGALLAHIR = sTANGGALLAHIR
    End Sub
    Private Sub Me_Load() Handles Me.Load
        fn_LoadSecurity()
    End Sub
    Private Sub Me_FormClosed(sender As System.Object, e As System.Windows.Forms.FormClosedEventArgs) Handles MyBase.FormClosed

    End Sub
    Private Sub fn_LoadSecurity()
        Try
            Dim oOtority As New Setting.clsOtority
            Dim oUser As New Setting.clsUser

            Dim ds = (From x In oOtority.GetDataDetail
                      Join y In oUser.GetData
                       On x.KDOTORITY Equals y.KDOTORITY
                      Where x.MODUL = "MEDREK_RJ" _
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
            Me.Text = "Laporan Tindakan"

            'grv.Columns("MEMO").Caption = LaporanTindakan.MEMO
            'grv.Columns("ISACTIVE").Caption = LaporanTindakan.ISACTIVE
            'grv.Columns("ISDEFAULT").Caption = LaporanTindakan.ISDEFAULT

            ColumnChooserToolStripMenuItem.Text = Caption.ColumnChooser
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadData()
        'Try
        '    grv.OptionsSelection.MultiSelect = True
        '    grv.SelectAll()
        '    grv.DeleteSelectedRows()
        '    grv.OptionsSelection.MultiSelect = False

        '    Dim ds = From x In oLaporanTindakan.GetDataByRMList(sREKAMMEDIS)
        '             Where x.ISDELETE = 0
        '             Select x.KDLAPORANTINDAKAN, x.KDPENDAFTARAN, TANGGAL = x.DATE, x.JENIS_OPERASI, DOKTER_OPERATOR = x.DOKTER_NAMEDISPLAY, PEMBUATLAPORAN = x.KDUSER

        '    grd.DataSource = ds.OrderByDescending(Function(x) x.TANGGAL)

        '    fn_LoadFormatData()
        'Catch oErr As Exception
        '    MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        'End Try
        Try
            grv.Columns.Clear()
            grd.DataSource = Nothing

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
            SQL &= "A.KDLAPORANTINDAKAN "
            SQL &= ",A.KDPENDAFTARAN "
            SQL &= ",TANGGAL = A.DATE "
            SQL &= ",A.JENIS_OPERASI "
            SQL &= ",DOKTER_OPERATOR = A.DOKTER_NAMEDISPLAY "
            'SQL &= ",PEMBUATLAPORAN = A.KDUSER "
            SQL &= ",JENISTINDAKAN = A.PROSEDUR2 "
            SQL &= "FROM S_DIGITAL_OK_LAPORANTINDAKAN A "
            SQL &= "WHERE A.KDCUSTOMER = '" & sREKAMMEDIS & "' "
            SQL &= "ORDER BY A.DATE DESC "

            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "ALL")

            grd.MainView = grv
            grd.DataSource = ds.Tables("ALL")
            grd.ForceInitialize()

            fn_LoadFormatData()

            If oConn.State = ConnectionState.Open Then
                oConn.Close()
            End If
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

        grv.Columns("KDLAPORANTINDAKAN").Visible = False
        grv.Columns("KDLAPORANTINDAKAN").OptionsColumn.ShowInCustomizationForm = False
        grv.BestFitColumns()
    End Sub
    Private Sub ColumnChooserToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles ColumnChooserToolStripMenuItem.Click
        grv.ShowCustomization()
    End Sub
    Private Function fn_DeleteData(ByVal sKDLaporanTindakan As String) As Boolean
        Try
            oLaporanTindakan.UpdateDeleteLaporanTindakan(sKDLaporanTindakan, sUserID)
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
        If grv.GetFocusedRowCellValue("KDLAPORANTINDAKAN") Is Nothing Then
            Exit Sub
        End If

        Dim frmLaporanTindakan As New frmLaporanTindakan
        Try
            frmLaporanTindakan.LoadMe(True, FORM_MODE.FORM_MODE_VIEW, grv.GetFocusedRowCellValue("KDLAPORANTINDAKAN"), sKDDOCTOR_INPUT, sKDDOCTOR_DPJPUTAMA, sREKAMMEDIS, grv.GetFocusedRowCellValue("KDPENDAFTARAN"), "")
            frmLaporanTindakan.ShowDialog(Me)
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub picAdd_Click() Handles picAdd.Click
        Dim frmLaporanTindakan As New frmLaporanTindakan
        Try
            frmLaporanTindakan.LoadMe(True, FORM_MODE.FORM_MODE_ADD, "", sKDDOCTOR_INPUT, sKDDOCTOR_DPJPUTAMA, sREKAMMEDIS, sREGISTERADD, "")
            frmLaporanTindakan.ShowDialog(Me)
            fn_LoadSecurity()
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        Finally
            If Not frmLaporanTindakan Is Nothing Then frmLaporanTindakan.Dispose()
            frmLaporanTindakan = Nothing

            Dim rowHandle As Integer = grv.LocateByValue(rowHandle, grv.Columns("MEMO"), sCode)
            If rowHandle > 0 Then grv.FocusedRowHandle = rowHandle

            If sStatusSave = "NEW" Then
                sStatusSave = "NONE"
                picAdd_Click()
            End If
        End Try
    End Sub
    Private Sub picUpdate_Click() Handles picUpdate.Click
        If grv.GetFocusedRowCellValue("KDLAPORANTINDAKAN") Is Nothing Then
            Exit Sub
        End If
        Dim frmLaporanTindakan As New frmLaporanTindakan
        Try
            frmLaporanTindakan.LoadMe(True, FORM_MODE.FORM_MODE_EDIT, grv.GetFocusedRowCellValue("KDLAPORANTINDAKAN"), sKDDOCTOR_INPUT, sKDDOCTOR_DPJPUTAMA, sREKAMMEDIS, grv.GetFocusedRowCellValue("KDPENDAFTARAN"), "")
            frmLaporanTindakan.ShowDialog(Me)
            fn_LoadSecurity()
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        Finally
            If Not frmLaporanTindakan Is Nothing Then frmLaporanTindakan.Dispose()
            frmLaporanTindakan = Nothing

            Dim rowHandle As Integer = grv.LocateByValue(rowHandle, grv.Columns("MEMO"), sCode)
            If rowHandle > 0 Then grv.FocusedRowHandle = rowHandle

            If sStatusSave = "NEW" Then
                sStatusSave = "NONE"
                picAdd_Click()
            End If
        End Try
    End Sub
    Private Sub picDelete_Click() Handles picDelete.Click
        If grv.GetFocusedRowCellValue("KDLAPORANTINDAKAN") Is Nothing Then
            Exit Sub
        End If
        If MsgBox(Statement.DeleteQuestion, MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, Me.Text) = MsgBoxResult.No Then Exit Sub
        If fn_DeleteData(grv.GetFocusedRowCellValue("KDLAPORANTINDAKAN")) = False Then
            MsgBox(Statement.DeleteFail, MsgBoxStyle.Exclamation, Me.Text)
            Exit Sub
        End If
        MsgBox(Statement.DeleteSuccess, MsgBoxStyle.Information, Me.Text)
        fn_LoadSecurity()
    End Sub
    Private Sub picPrint_Click() Handles picPrint.Click
        Try
            If grv.GetFocusedRowCellValue("KDLAPORANTINDAKAN") Is Nothing Then
                Exit Sub
            End If

            Dim ds = oLaporanTindakan.GetData(grv.GetFocusedRowCellValue("KDLAPORANTINDAKAN"))

            If ds IsNot Nothing Then
                Dim xtraLaporanTindakan As New xtraREPORTLAPORANTINDAKAN

                NAMA = sNAMA
                JENISKELAMIN = sJENISKELAMIN
                TANGGALLAHIR = sTANGGALLAHIR.ToString("dd-MM-yyyy")
                sKDUSER_TTD = ds.KDUSER
                USIA = frmRawatInapList.GetUmurPasien(ds.DATE, sTANGGALLAHIR)

                xtraLaporanTindakan.BindingSource1.DataSource = ds
                Dim printTool As New DevExpress.XtraReports.UI.ReportPrintTool(xtraLaporanTindakan)
                printTool.ShowPreviewDialog(DevExpress.LookAndFeel.UserLookAndFeel.Default)
            End If
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub picRefresh_Click() Handles picRefresh.Click
        fn_LoadSecurity()
    End Sub
#End Region
End Class