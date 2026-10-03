Imports iPOS.GLB.Globals
Imports iPOS.DA
Imports System.Linq
Imports System.Data
Imports System.Data.SqlClient
Imports Microsoft.VisualBasic.PowerPacks.Printing.Compatibility.VB6

Public Class frmReq_BHPList
    Private oFormMode As FORM_MODE = FORM_MODE.FORM_MODE_VIEW
    Private oReq_BHP As New Sales.clsREQ_BHP

#Region "Function"
    Private Sub Me_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        deDATEFrom.DateTime = Now
        deDATETo.DateTime = Now
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
                      On x.NOIDOTORITY Equals y.NOIDOTORITY
                      Where x.NOIDMODUL = "REQ_BHP" _
                      And y.NOIDUSER = sUserID
                      Select x.ISADD, x.ISDELETE, x.ISUPDATE, x.ISPRINT, x.ISVIEW).FirstOrDefault

            Try
                picAdd.Enabled = ds.ISADD
                picDelete.Enabled = ds.ISDELETE
                picUpdate.Enabled = ds.ISUPDATE
                picPrint.Enabled = ds.ISPRINT
                picRefresh.Enabled = ds.ISVIEW

                If ds.ISVIEW = True Then
                    fn_LoadData()
                End If
            Catch ex As Exception
                MsgBox("Keamanan belum dipasang, tolong kontak admin anda!", MsgBoxStyle.Exclamation, Me.Text)

                picAdd.Enabled = False
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
        Dim oDebtor As New Master.clsDebtor
        Try
            Dim ds = From x In oReq_BHP.GetDataByDate(deDATEFrom.DateTime, deDATETo.DateTime)
                     Select x.KDREQBHP, x.KDREG, x.S_PENDAFTARAN_H.KDCUSTOMER, x.S_PENDAFTARAN_H.M_CUSTOMER.NAME_DISPLAY, PENJAMIN = x.S_PENDAFTARAN_H.M_DEBTOR.NAME_DISPLAY, x.NOIDUSER

            grd.DataSource = ds.ToList

            fn_LoadFormatData()

        Catch ex As Exception
            MsgBox("Load Data : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadFormatData()
        For iLoop As Integer = 0 To grv.Columns.Count - 1
            If grv.Columns(iLoop).ColumnType.Name = "Decimal" Then
                grv.Columns(iLoop).DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                grv.Columns(iLoop).DisplayFormat.FormatString = "{0:n2}"
                grv.Columns(iLoop).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
                grv.GroupSummary.Add(DevExpress.Data.SummaryItemType.Sum, grv.Columns(iLoop).FieldName, grv.Columns(iLoop),
                                     "{0:n2}")
                grv.Columns(iLoop).SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                grv.Columns(iLoop).SummaryItem.DisplayFormat = "{0:n2}"
            ElseIf grv.Columns(iLoop).ColumnType.Name = "DateTime" Then
                grv.Columns(iLoop).DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
                grv.Columns(iLoop).DisplayFormat.FormatString = "{0:dd/MM/yyyy HH:mm:ss}"
            End If
        Next

        grv.Columns("KDREQBHP").Caption = "No. Transaksi"
        grv.Columns("KDREG").Caption = "No. Register"
        grv.Columns("KDCUSTOMER").Caption = "No. Rekam Medis"
        grv.Columns("NAME_DISPLAY").Caption = "Nama Pasien"
        grv.Columns("PENJAMIN").Caption = "Penjamin"
        grv.Columns("NOIDUSER").Caption = "User"
    End Sub
    Private Function fn_DeleteData(ByVal sKDREQBHP As String) As Boolean
        Try
            oReq_BHP.DeleteData(sKDREQBHP)
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
    Private Sub grv_FocusedRowChanged(ByVal sender As System.Object, ByVal e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs) Handles grv.FocusedRowChanged
        If grv.GetFocusedRowCellValue("KDREQBHP") Is Nothing Then
            Exit Sub
        End If

    End Sub
    Private Sub grv_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles grv.DoubleClick
        If grv.GetFocusedRowCellValue("KDREQBHP") Is Nothing Then
            Exit Sub
        End If

        Dim frmReq_BHP As New frmReq_BHP
        Try
            frmReq_BHP.LoadMe(FORM_MODE.FORM_MODE_VIEW, grv.GetFocusedRowCellValue("KDREQBHP"))
            frmReq_BHP.ShowDialog(Me)
        Catch ex As Exception
            MsgBox("Load Form Detail : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub picAdd_Click() Handles picAdd.Click
        Dim frmReq_BHP As New frmReq_BHP
        Try
            frmReq_BHP.LoadMe(FORM_MODE.FORM_MODE_ADD, grv.GetFocusedRowCellValue("KDREG"))
            frmReq_BHP.ShowDialog(Me)
            fn_LoadSecurity()
        Catch ex As Exception
            MsgBox("Load Form Detail : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
        Finally
            If Not frmReq_BHP Is Nothing Then frmReq_BHP.Dispose()
            frmReq_BHP = Nothing

            Dim rowHandle As Integer = grv.LocateByValue(rowHandle, grv.Columns("NAME_DISPLAY"), sCode)
            If rowHandle > 0 Then grv.FocusedRowHandle = rowHandle

            If sStatusSave = "NEW" Then
                sStatusSave = "NONE"
                picAdd_Click()
            End If
        End Try
    End Sub
    Private Sub picUpdate_Click() Handles picUpdate.Click
        If grv.GetFocusedRowCellValue("KDREQBHP") Is Nothing Then
            Exit Sub
        End If
        Dim frmReq_BHP As New frmReq_BHP
        Try
            frmReq_BHP.LoadMe(FORM_MODE.FORM_MODE_EDIT, grv.GetFocusedRowCellValue("KDREG"), grv.GetFocusedRowCellValue("KDREQBHP"))
            frmReq_BHP.ShowDialog(Me)
            fn_LoadSecurity()
        Catch ex As Exception
            MsgBox("Load Form Detail : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
        Finally
            If Not frmReq_BHP Is Nothing Then frmReq_BHP.Dispose()
            frmReq_BHP = Nothing

            Dim rowHandle As Integer = grv.LocateByValue(rowHandle, grv.Columns("NAME_DISPLAY"), sCode)
            If rowHandle > 0 Then grv.FocusedRowHandle = rowHandle

            If sStatusSave = "NEW" Then
                sStatusSave = "NONE"
                picAdd_Click()
            End If
        End Try
    End Sub
    Private Sub picDelete_Click() Handles picDelete.Click
        If grv.GetFocusedRowCellValue("KDREQBHP") Is Nothing Then
            Exit Sub
        End If

        If MsgBox("Delete " & grv.GetFocusedRowCellValue("KDREQBHP") & "?", MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, Me.Text) = MsgBoxResult.No Then Exit Sub
        If fn_DeleteData(grv.GetFocusedRowCellValue("KDREQBHP")) = False Then
            MsgBox("Hapus gagal! tolong kontak admin anda!", MsgBoxStyle.Exclamation, Me.Text)
            Exit Sub
        End If
        MsgBox("Delete " & grv.GetFocusedRowCellValue("KDREQBHP") & " success!", MsgBoxStyle.Information, Me.Text)
        fn_LoadSecurity()
    End Sub
    Private Sub picPrint_Click() Handles picPrint.Click
        Try
            If grv.GetFocusedRowCellValue("KDREQBHP") Is Nothing Then
                Exit Sub
            End If

            fn_PrintStruk()
        Catch oErr As Exception
            MsgBox("Print Data : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub picRefresh_Click() Handles picRefresh.Click
        fn_LoadSecurity()
        If grv.GetFocusedRowCellValue("KDREQBHP") Is Nothing Then
            Exit Sub
        End If
    End Sub
    Private Sub fn_PrintStruk()
        Try
            If grv.GetFocusedRowCellValue("KDREQBHP") Is Nothing Then Exit Sub

            Dim rpt As New xtraReqBHP
            Dim ds = oReq_BHP.GetData(grv.GetFocusedRowCellValue("KDREQBHP"))
            rpt.bindingSource.DataSource = ds
            Dim printTool As New DevExpress.XtraReports.UI.ReportPrintTool(rpt)
            printTool.ShowPreviewDialog(DevExpress.LookAndFeel.UserLookAndFeel.Default)
        Catch oErr As Exception
            MsgBox("Print Data : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
#End Region
End Class