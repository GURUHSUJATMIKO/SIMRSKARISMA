Imports iPOS.GLB.Globals
Imports iPOS.DA
Imports System.Linq
Imports System.Data
Imports System.Data.SqlClient
Imports Microsoft.VisualBasic.PowerPacks.Printing.Compatibility.VB6

Public Class frmReq_RecipeList
    Private oFormMode As FORM_MODE = FORM_MODE.FORM_MODE_VIEW
    Private oReq_Recipe As New Sales.clsReq_Recipe

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
                      Where x.NOIDMODUL = "REQ_RECIPE" _
                      And y.NOIDUSER = sUserID
                      Select x.ISADD, x.ISDELETE, x.ISUPDATE, x.ISPRINT, x.ISVIEW).FirstOrDefault

            Try
                picAdd.Enabled = ds.ISADD
                picDelete.Enabled = ds.ISDELETE
                picUpdate.Enabled = ds.ISUPDATE
                picPrint.Enabled = ds.ISPRINT
                picRefresh.Enabled = ds.ISVIEW
                picConfirm.Enabled = ds.ISADD

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
                picConfirm.Enabled = False

            End Try
        Catch ex As Exception
            MsgBox("Load Security : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadData()
        Dim oDebtor As New Master.clsDebtor
        Try
            Dim ds = From x In oReq_Recipe.GetDataByDate(deDATEFrom.DateTime, deDATETo.DateTime)
                     Select x.KDREQRECIPE, x.KDREG, x.S_PENDAFTARAN_H.KDCUSTOMER, x.S_PENDAFTARAN_H.M_CUSTOMER.NAME_DISPLAY, PENJAMIN = x.S_PENDAFTARAN_H.M_DEBTOR.NAME_DISPLAY, x.ALERGIOBAT, x.KONFIRMASIRESEP, x.NOANTRIAN, x.NOIDUSER, x.ISPERUBAHANRESEP, x.ISAPPROVAL

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

        grv.Columns("KDREQRECIPE").Caption = "No. Transaksi"
        grv.Columns("KDREG").Caption = "No. Register"
        grv.Columns("KDCUSTOMER").Caption = "No. Rekam Medis"
        grv.Columns("NAME_DISPLAY").Caption = "Nama Pasien"
        grv.Columns("PENJAMIN").Caption = "Penjamin"
        grv.Columns("ALERGIOBAT").Caption = "Alergi Obat"
        grv.Columns("NOIDUSER").Caption = "User"
        grv.Columns("KONFIRMASIRESEP").Caption = "Status"
        grv.Columns("NOANTRIAN").Caption = "Antrian"
        grv.Columns("ISPERUBAHANRESEP").Caption = "Perubahan Resep"
        grv.Columns("ISAPPROVAL").Caption = "Disetujui"

    End Sub
    Private Function fn_DeleteData(ByVal sKDREQRECIPE As String) As Boolean
        Try
            oReq_Recipe.DeleteData(sKDREQRECIPE)
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
            Case Keys.S
                If e.Alt = True And picConfirm.Enabled = True Then
                    picConfirm_Click()
                End If
        End Select
    End Sub
    Private Sub grv_FocusedRowChanged(ByVal sender As System.Object, ByVal e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs) Handles grv.FocusedRowChanged
        If grv.GetFocusedRowCellValue("KDREQRECIPE") Is Nothing Then
            Exit Sub
        End If

    End Sub
    Private Sub grv_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles grv.DoubleClick
        If grv.GetFocusedRowCellValue("KDREQRECIPE") Is Nothing Then
            Exit Sub
        End If

        Dim frmReq_Recipe As New frmReq_Recipe
        Try
            frmReq_Recipe.LoadMe(FORM_MODE.FORM_MODE_VIEW, False, grv.GetFocusedRowCellValue("KDREQRECIPE"))
            frmReq_Recipe.ShowDialog(Me)
        Catch ex As Exception
            MsgBox("Load Form Detail : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub picConfirm_Click() Handles picConfirm.Click
        If grv.GetFocusedRowCellValue("KDREQRECIPE") Is Nothing Then
            Exit Sub
        End If

        If grv.GetFocusedRowCellValue("ISPERUBAHANRESEP") = True Then
            oReq_Recipe.Approval(grv.GetFocusedRowCellValue("KDREQRECIPE"))
        Else
            MsgBox("Tidak ada permintaan perubahan Resep", MsgBoxStyle.Exclamation, Me.Text)
        End If
    End Sub
    Private Sub picAdd_Click() Handles picAdd.Click
        Dim frmReq_Recipe As New frmReq_Recipe
        Try
            frmReq_Recipe.LoadMe(FORM_MODE.FORM_MODE_ADD, False)
            frmReq_Recipe.ShowDialog(Me)
            fn_LoadSecurity()
        Catch ex As Exception
            MsgBox("Load Form Detail : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
        Finally
            If Not frmReq_Recipe Is Nothing Then frmReq_Recipe.Dispose()
            frmReq_Recipe = Nothing

            Dim rowHandle As Integer = grv.LocateByValue(rowHandle, grv.Columns("NAME_DISPLAY"), sCode)
            If rowHandle > 0 Then grv.FocusedRowHandle = rowHandle

            If sStatusSave = "NEW" Then
                sStatusSave = "NONE"
                picAdd_Click()
            End If
        End Try
    End Sub
    Private Sub picUpdate_Click() Handles picUpdate.Click
        If grv.GetFocusedRowCellValue("KDREQRECIPE") Is Nothing Then
            Exit Sub
        End If
        Dim frmReq_Recipe As New frmReq_Recipe
        Try
            frmReq_Recipe.LoadMe(FORM_MODE.FORM_MODE_EDIT, False, grv.GetFocusedRowCellValue("KDREQRECIPE"))
            frmReq_Recipe.ShowDialog(Me)
            fn_LoadSecurity()
        Catch ex As Exception
            MsgBox("Load Form Detail : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
        Finally
            If Not frmReq_Recipe Is Nothing Then frmReq_Recipe.Dispose()
            frmReq_Recipe = Nothing

            Dim rowHandle As Integer = grv.LocateByValue(rowHandle, grv.Columns("NAME_DISPLAY"), sCode)
            If rowHandle > 0 Then grv.FocusedRowHandle = rowHandle

            If sStatusSave = "NEW" Then
                sStatusSave = "NONE"
                picAdd_Click()
            End If
        End Try
    End Sub
    Private Sub picDelete_Click() Handles picDelete.Click
        If grv.GetFocusedRowCellValue("KDREQRECIPE") Is Nothing Then
            Exit Sub
        End If

        If grv.GetFocusedRowCellValue("KONFIRMASIRESEP") <> "Resep Belum Diterima" Then
            MsgBox("Resep sudah diterima tidak dapat dihapus", MsgBoxStyle.Exclamation, Me.Text)
            Exit Sub
        End If

        If MsgBox("Delete " & grv.GetFocusedRowCellValue("KDREQRECIPE") & "?", MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, Me.Text) = MsgBoxResult.No Then Exit Sub
        If fn_DeleteData(grv.GetFocusedRowCellValue("KDREQRECIPE")) = False Then
            MsgBox("Hapus gagal! tolong kontak admin anda!", MsgBoxStyle.Exclamation, Me.Text)
            Exit Sub
        End If
        MsgBox("Delete " & grv.GetFocusedRowCellValue("KDREQRECIPE") & " success!", MsgBoxStyle.Information, Me.Text)
        fn_LoadSecurity()
    End Sub
    Private Sub picPrint_Click() Handles picPrint.Click
        Try
            If grv.GetFocusedRowCellValue("KDREQRECIPE") Is Nothing Then
                Exit Sub
            End If

            fn_PrintStruk()
        Catch oErr As Exception
            MsgBox("Print Data : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub picRefresh_Click() Handles picRefresh.Click
        fn_LoadSecurity()
        If grv.GetFocusedRowCellValue("KDREQRECIPE") Is Nothing Then
            Exit Sub
        End If

    End Sub
    Private Sub fn_PrintStruk()
        Try
            If grv.GetFocusedRowCellValue("KDREQRECIPE") Is Nothing Then Exit Sub

            Dim rpt As New xtraEResepNonRacikan

            Dim ds = oReq_Recipe.GetData(grv.GetFocusedRowCellValue("KDREQRECIPE"))

            Dim dsDetail = oReq_Recipe.GetDataDetail(grv.GetFocusedRowCellValue("KDREQRECIPE"))

            Dim listSebelum As New List(Of String)
            Dim listSesudah As New List(Of String)
            Dim oItem As New Master.clsItem

            For Each xloop In dsDetail
                If xloop.KDITEM_PERUBAHAN <> 0 Then
                    listSebelum.Add(xloop.M_ITEM.NMITEM2)
                    listSesudah.Add(oItem.GetData(xloop.KDITEM_PERUBAHAN).NMITEM2)
                End If
            Next

            lblTertulis = String.Join(vbCrLf, listSebelum.ToArray)
            lblMenjadi = String.Join(vbCrLf, listSesudah.ToArray)

            rpt.bindingSource.DataSource = ds
            Dim printTool As New DevExpress.XtraReports.UI.ReportPrintTool(rpt)
            printTool.ShowPreviewDialog(DevExpress.LookAndFeel.UserLookAndFeel.Default)
        Catch oErr As Exception
            MsgBox("Print Data : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try

    End Sub
#End Region
End Class