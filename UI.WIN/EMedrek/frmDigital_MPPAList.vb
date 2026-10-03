Imports DataAccess
Imports UI.WIN.MAIN.My.Resources
Imports System.Linq
Imports System.Data.SqlClient

Public Class frmDigital_MPPAList
    Private oFormMode As FORM_MODE = FORM_MODE.FORM_MODE_VIEW
    Private oDigital_MPPA As New Digital.clsDigital_MPPA
    Private sRegister As String = String.Empty
    Private sRuangan As String = String.Empty
    Private sRM As String = String.Empty
    Private sNAMA As String = String.Empty
    Private sJK As String = String.Empty
    Private sTANGGALLAHIR As DateTime = Now

#Region "Function"
    Public sub fn_LoadData(ByVal Register As String, ByVal Ruangan As String, ByVal RM As String, ByVal NAMA As String, ByVal JK As String, ByVal TANGGALLAHIR As DateTime)
        sRegister = Register
        sRuangan = Ruangan
        sRM = rm
        sNAMA = NAMA
        sJK = jk
        sTANGGALLAHIR = TANGGALLAHIR
    End sub
    Private Sub Me_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        fn_LoadSecurity()
    End Sub
    Private Sub Form_Closing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        Dispose()
    End Sub
    Private Overloads Sub Dispose()
        MyBase.Dispose()
        Me.Dispose(True)
        GC.SuppressFinalize(Me)
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
                picAdd_A.Enabled = ds.ISADD
                picAdd_b.Enabled = ds.ISADD
                picDelete.Enabled = ds.ISDELETE
                picUpdate.Enabled = ds.ISUPDATE
                picPrint.Enabled = ds.ISPRINT
                picRefresh.Enabled = ds.ISVIEW

                If ds.ISVIEW = True Then
                    fn_LoadData()
                End If
            Catch ex As Exception
                MsgBox("Keamanan belum dipasang, tolong kontak admin anda!", MsgBoxStyle.Exclamation, Me.Text)

                picAdd_A.Enabled = False
                picAdd_b.Enabled = False
                picDelete.Enabled = False
                picUpdate.Enabled = False
                picPrint.Enabled = False
                picRefresh.Enabled = False
            End Try
        Catch ex As Exception
            MsgBox("Load Security : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadData()
        'Try
        '    Dim ds = From x In oDigital_MPPA.GetDataByRMList(sRM)
        '             Select x.KDMPPA, x.KDPENDAFTARAN, x.DATE, x.RUANGAN

        '    grd.DataSource = ds.ToList

        '    fn_LoadFormatData()

        'Catch ex As Exception
        '    MsgBox("Load Data : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
        'End Try
        Try
            grv.OptionsSelection.MultiSelect = True
            grv.SelectAll()
            grv.DeleteSelectedRows()
            grv.OptionsSelection.MultiSelect = False

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

            SQL = "EXEC FORMULRMPP @KDCUSTOMER = '" & sRM & "' "

            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "FOMRULIRMPP")

            grd.DataSource = ds.Tables("FOMRULIRMPP")
            grd.ForceInitialize()

            If oConn.State = ConnectionState.Open Then
                oConn.Close()
            End If

            grv.Columns("KODE").Visible = False
            'grv.Columns("SEQ").Visible = False

            For iLoop As Integer = 0 To grv.Columns.Count - 1
                If grv.Columns(iLoop).ColumnType.Name = "Decimal" Then
                    grv.Columns(iLoop).DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                    grv.Columns(iLoop).DisplayFormat.FormatString = "{0:n2}"
                    grv.Columns(iLoop).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
                ElseIf grv.Columns(iLoop).ColumnType.Name = "DateTime" Then
                    grv.Columns(iLoop).DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
                    grv.Columns(iLoop).DisplayFormat.FormatString = "{0:dd/MM/yyyy HH:mm}"
                End If
            Next
        Catch oErr As Exception
            MsgBox("Load Formulir MPP : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadFormatData()
        grv.Columns("KODE").Visible = False

        'grv.Columns("DATE").Caption = "Tanggal"
        'grv.Columns("RUANGAN").Caption = "Poli/Ruangan"
        'grv.Columns("KDPENDAFTARAN").Caption = "Register"
    End Sub
    Private Function fn_DeleteData(ByVal sKDDigital_MPPA As String) As Boolean
        Try
            If grv.GetFocusedRowCellValue("KATEGORI") = "MPPA"
                oDigital_MPPA.DeleteData(sKDDigital_MPPA)
            Else
                Dim oDigital_MPPB As New Digital.clsDigital_MPPB
                oDigital_MPPB.DeleteData(sKDDigital_MPPA)
            End If

            fn_DeleteData = True
        Catch oErr As Exception
            fn_DeleteData = False
            MsgBox("Hapus Data : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Function
#End Region
#Region "Command Button"
    Private Sub frmMember_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        Select Case e.KeyCode
            'Case Keys.A
            '    If e.Alt = True And picAdd_A.Enabled = True Then
            '        picAdd_Click()
            '    End If
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
    Private Sub grv_FocusedRowChanged(ByVal sender As System.Object, ByVal e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs) Handles grv.FocusedRowChanged
        If grv.GetFocusedRowCellValue("KODE") Is Nothing Then
            Exit Sub
        End If
    End Sub
    Private Sub grv_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles grv.DoubleClick
        If grv.GetFocusedRowCellValue("KODE") Is Nothing Then
            Exit Sub
        End If

        If grv.GetFocusedRowCellValue("KATEGORI") = "MPPA"
            Dim frmDigital_MPPA As New frmDigital_MPPA
            Try
                frmDigital_MPPA.LoadMe(FORM_MODE.FORM_MODE_VIEW, grv.GetFocusedRowCellValue("KDPENDAFTARAN"), grv.GetFocusedRowCellValue("RUANGAN"), sRM, sNAMA, sJK, sTANGGALLAHIR, grv.GetFocusedRowCellValue("KODE"))
                frmDigital_MPPA.ShowDialog(Me)
            Catch ex As Exception
                MsgBox("Load Form Detail : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
            End Try
        Else
            Dim frmDigital_MPPB As New frmDigital_MPPB
            Try
                frmDigital_MPPB.LoadMe(FORM_MODE.FORM_MODE_VIEW, grv.GetFocusedRowCellValue("KDPENDAFTARAN"), grv.GetFocusedRowCellValue("RUANGAN"), sRM, sNAMA, sJK, sTANGGALLAHIR, grv.GetFocusedRowCellValue("KODE"))
                frmDigital_MPPB.ShowDialog(Me)
            Catch ex As Exception
                MsgBox("Load Form Detail : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
            End Try
        End If
    End Sub
    Private Sub picAdd_A_Click() Handles picAdd_A.Click
        Dim frmDigital_MPPA As New frmDigital_MPPA
        Try
            frmDigital_MPPA.LoadMe(FORM_MODE.FORM_MODE_ADD, sRegister, sRuangan, sRM, sNAMA, sJK, sTANGGALLAHIR, "")
            frmDigital_MPPA.ShowDialog(Me)
            fn_LoadSecurity()
        Catch ex As Exception
            MsgBox("Load Form Detail : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
        Finally
            If Not frmDigital_MPPA Is Nothing Then frmDigital_MPPA.Dispose()
            frmDigital_MPPA = Nothing

            Dim rowHandle As Integer = grv.LocateByValue(rowHandle, grv.Columns("KDPENDAFTARAN"), sCode)
            If rowHandle > 0 Then grv.FocusedRowHandle = rowHandle

            'If sStatusSave = "NEW" Then
            '    sStatusSave = "NONE"
            '    picAdd_Click()
            'End If
        End Try
    End Sub
    Private Sub picAdd_B_Click() Handles picAdd_B.Click
        Dim frmDigital_MPPB As New frmDigital_MPPB
        Try
            frmDigital_MPPB.LoadMe(FORM_MODE.FORM_MODE_ADD, sRegister, sRuangan, sRM, sNAMA, sJK, sTANGGALLAHIR, "")
            frmDigital_MPPB.ShowDialog(Me)
            fn_LoadSecurity()
        Catch ex As Exception
            MsgBox("Load Form Detail : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
        Finally
            If Not frmDigital_MPPB Is Nothing Then frmDigital_MPPB.Dispose()
            frmDigital_MPPB = Nothing

            Dim rowHandle As Integer = grv.LocateByValue(rowHandle, grv.Columns("KDPENDAFTARAN"), sCode)
            If rowHandle > 0 Then grv.FocusedRowHandle = rowHandle

            'If sStatusSave = "NEW" Then
            '    sStatusSave = "NONE"
            '    picAdd_Click()
            'End If
        End Try
    End Sub
    Private Sub picUpdate_Click() Handles picUpdate.Click
        If grv.GetFocusedRowCellValue("KODE") Is Nothing Then
            Exit Sub
        End If

        If grv.GetFocusedRowCellValue("KATEGORI") = "MPPA"
            Dim frmDigital_MPPA As New frmDigital_MPPA
            Try
                frmDigital_MPPA.LoadMe(FORM_MODE.FORM_MODE_EDIT, grv.GetFocusedRowCellValue("KDPENDAFTARAN"), grv.GetFocusedRowCellValue("RUANGAN"), sRM, sNAMA, sJK, sTANGGALLAHIR, grv.GetFocusedRowCellValue("KODE"))
                frmDigital_MPPA.ShowDialog(Me)
                fn_LoadSecurity()
            Catch ex As Exception
                MsgBox("Load Form Detail : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
            Finally
                If Not frmDigital_MPPA Is Nothing Then frmDigital_MPPA.Dispose()
                frmDigital_MPPA = Nothing

                Dim rowHandle As Integer = grv.LocateByValue(rowHandle, grv.Columns("KDPENDAFTARAN"), sCode)
                If rowHandle > 0 Then grv.FocusedRowHandle = rowHandle

                'If sStatusSave = "NEW" Then
                '    sStatusSave = "NONE"
                '    picAdd_Click()
                'End If
            End Try
        Else
            Dim frmDigital_MPPB As New frmDigital_MPPB
            Try
                frmDigital_MPPB.LoadMe(FORM_MODE.FORM_MODE_EDIT, grv.GetFocusedRowCellValue("KDPENDAFTARAN"), grv.GetFocusedRowCellValue("RUANGAN"), sRM, sNAMA, sJK, sTANGGALLAHIR, grv.GetFocusedRowCellValue("KODE"))
                frmDigital_MPPB.ShowDialog(Me)
                fn_LoadSecurity()
            Catch ex As Exception
                MsgBox("Load Form Detail : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
            Finally
                If Not frmDigital_MPPB Is Nothing Then frmDigital_MPPB.Dispose()
                frmDigital_MPPB = Nothing

                Dim rowHandle As Integer = grv.LocateByValue(rowHandle, grv.Columns("KDPENDAFTARAN"), sCode)
                If rowHandle > 0 Then grv.FocusedRowHandle = rowHandle

                'If sStatusSave = "NEW" Then
                '    sStatusSave = "NONE"
                '    picAdd_Click()
                'End If
            End Try
        End If
    End Sub
    Private Sub picDelete_Click() Handles picDelete.Click
        If grv.GetFocusedRowCellValue("KODE") Is Nothing Then
            Exit Sub
        End If
        If MsgBox("Delete " & grv.GetFocusedRowCellValue("KDPENDAFTARAN") & "?", MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, Me.Text) = MsgBoxResult.No Then Exit Sub
        If fn_DeleteData(grv.GetFocusedRowCellValue("KODE")) = False Then
            MsgBox("Hapus gagal! tolong kontak admin anda!", MsgBoxStyle.Exclamation, Me.Text)
            Exit Sub
        End If
        MsgBox("Delete " & grv.GetFocusedRowCellValue("DESCRIPTION") & " success!", MsgBoxStyle.Information, Me.Text)
        fn_LoadSecurity()
    End Sub
    Private Sub picPrint_Click() Handles picPrint.Click
        Try
            If grv.GetFocusedRowCellValue("KODE") = String.Empty Then Exit Sub

            If grv.GetFocusedRowCellValue("KATEGORI") = "MPPA"
                Dim rpt As New xtraDigital_MPPA

                rpt.ShowPrintMarginsWarning = False
                rpt.Watermark.Text = sWATERMARK
                Dim ds = oDigital_MPPA.GetData(grv.GetFocusedRowCellValue("KODE"))
                rpt.bindingSource.DataSource = ds
                Dim printTool As New DevExpress.XtraReports.UI.ReportPrintTool(rpt)
                printTool.ShowPreviewDialog(DevExpress.LookAndFeel.UserLookAndFeel.Default)
            Else
                 Dim rpt As New xtraDigital_MPPB

                rpt.ShowPrintMarginsWarning = False
                rpt.Watermark.Text = sWATERMARK
                Dim ds = oDigital_MPPA.GetData(grv.GetFocusedRowCellValue("KODE"))
                rpt.bindingSource.DataSource = ds
                Dim printTool As New DevExpress.XtraReports.UI.ReportPrintTool(rpt)
                printTool.ShowPreviewDialog(DevExpress.LookAndFeel.UserLookAndFeel.Default)
            End If

        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub picRefresh_Click() Handles picRefresh.Click
        fn_LoadSecurity()
    End Sub

    Private Sub picAdd_Click(sender As Object, e As EventArgs) Handles picAdd_A.Click

    End Sub
#End Region
End Class