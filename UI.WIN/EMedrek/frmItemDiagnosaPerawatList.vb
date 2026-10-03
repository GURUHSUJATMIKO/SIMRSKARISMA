Imports DataAccess
Imports UI.WIN.MAIN.My.Resources
Imports System.Linq
Imports System.Data.SqlClient

Public Class frmItemDiagnosaPerawatList
    Private oFormMode As FORM_MODE = FORM_MODE.FORM_MODE_VIEW
    Private oItemDiagnosaPerawat As New Master.clsItemDiagnosaPerawat

#Region "Function"
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
                      Where x.MODUL = "ITEMDIAGNOSAPERAWAT" _
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
        Try
            Dim ds = From x In oItemDiagnosaPerawat.GetData
                     Select x.KDITEMDIAGNOSAPERAWAT, x.DESCRIPTION, x.ISACTIVE
            grd.DataSource = ds.ToList

            fn_LoadFormatData()

            If ds.Count < 1 Then Exit Sub

            fn_LoadDetail()
        Catch ex As Exception
            MsgBox("Load Data : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadFormatData()
        grv.Columns("KDITEMDIAGNOSAPERAWAT").Visible = False

        grv.Columns("DESCRIPTION").Caption = "Nama Diagnosa"
        grv.Columns("ISACTIVE").Caption = "Aktif?"
    End Sub
    Private Sub fn_LoadDetail()
        Try
            Dim ds = (From x In oItemDiagnosaPerawat.GetData
                      Where x.KDITEMDIAGNOSAPERAWAT = grv.GetFocusedRowCellValue("KDITEMDIAGNOSAPERAWAT")
                      Select x).FirstOrDefault

            With ds
                txtDESCRIPTION.Text = .DESCRIPTION
            End With
        Catch ex As Exception
            MsgBox("Load Detail : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_EmptyMe()
        txtDESCRIPTION.ResetText()
    End Sub
    Private Function fn_DeleteData(ByVal sKDItemDiagnosaPerawat As String) As Boolean
        Try
            oItemDiagnosaPerawat.DeleteData(sKDItemDiagnosaPerawat)
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
        If grv.GetFocusedRowCellValue("KDITEMDIAGNOSAPERAWAT") Is Nothing Then
            fn_EmptyMe()
            Exit Sub
        End If

        fn_LoadDetail()
    End Sub
    Private Sub grv_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles grv.DoubleClick, btnView.Click
        If grv.GetFocusedRowCellValue("KDITEMDIAGNOSAPERAWAT") Is Nothing Then
            fn_EmptyMe()
            Exit Sub
        End If

        Dim frmItemDiagnosaPerawat As New frmItemDiagnosaPerawat
        Try
            frmItemDiagnosaPerawat.LoadMe(FORM_MODE.FORM_MODE_VIEW, grv.GetFocusedRowCellValue("KDITEMDIAGNOSAPERAWAT"))
            frmItemDiagnosaPerawat.ShowDialog(Me)
        Catch ex As Exception
            MsgBox("Load Form Detail : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub picAdd_Click() Handles picAdd.Click
        Dim frmItemDiagnosaPerawat As New frmItemDiagnosaPerawat
        Try
            frmItemDiagnosaPerawat.LoadMe(FORM_MODE.FORM_MODE_ADD)
            frmItemDiagnosaPerawat.ShowDialog(Me)
            fn_LoadSecurity()
        Catch ex As Exception
            MsgBox("Load Form Detail : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
        Finally
            If Not frmItemDiagnosaPerawat Is Nothing Then frmItemDiagnosaPerawat.Dispose()
            frmItemDiagnosaPerawat = Nothing

            Dim rowHandle As Integer = grv.LocateByValue(rowHandle, grv.Columns("DESCRIPTION"), sCode)
            If rowHandle > 0 Then grv.FocusedRowHandle = rowHandle

            'If sStatusSave = "NEW" Then
            '    sStatusSave = "NONE"
            '    picAdd_Click()
            'End If
        End Try
    End Sub
    Private Sub picUpdate_Click() Handles picUpdate.Click
        If grv.GetFocusedRowCellValue("KDITEMDIAGNOSAPERAWAT") Is Nothing Then
            fn_EmptyMe()
            Exit Sub
        End If
        Dim frmItemDiagnosaPerawat As New frmItemDiagnosaPerawat
        Try
            frmItemDiagnosaPerawat.LoadMe(FORM_MODE.FORM_MODE_EDIT, grv.GetFocusedRowCellValue("KDITEMDIAGNOSAPERAWAT"))
            frmItemDiagnosaPerawat.ShowDialog(Me)
            fn_LoadSecurity()
        Catch ex As Exception
            MsgBox("Load Form Detail : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
        Finally
            If Not frmItemDiagnosaPerawat Is Nothing Then frmItemDiagnosaPerawat.Dispose()
            frmItemDiagnosaPerawat = Nothing

            Dim rowHandle As Integer = grv.LocateByValue(rowHandle, grv.Columns("DESCRIPTION"), sCode)
            If rowHandle > 0 Then grv.FocusedRowHandle = rowHandle

            'If sStatusSave = "NEW" Then
            '    sStatusSave = "NONE"
            '    picAdd_Click()
            'End If
        End Try
    End Sub
    Private Sub picDelete_Click() Handles picDelete.Click
        If grv.GetFocusedRowCellValue("KDITEMDIAGNOSAPERAWAT") Is Nothing Then
            fn_EmptyMe()
            Exit Sub
        End If
        If MsgBox("Delete " & grv.GetFocusedRowCellValue("DESCRIPTION") & "?", MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, Me.Text) = MsgBoxResult.No Then Exit Sub
        If fn_DeleteData(grv.GetFocusedRowCellValue("KDITEMDIAGNOSAPERAWAT")) = False Then
            MsgBox("Hapus gagal! tolong kontak admin anda!", MsgBoxStyle.Exclamation, Me.Text)
            Exit Sub
        End If
        MsgBox("Delete " & grv.GetFocusedRowCellValue("DESCRIPTION") & " success!", MsgBoxStyle.Information, Me.Text)
        fn_LoadSecurity()
    End Sub
    Private Sub picPrint_Click() Handles picPrint.Click
        Try

        Catch oErr As Exception
            MsgBox("Print Data : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub picRefresh_Click() Handles picRefresh.Click
        fn_LoadSecurity()
    End Sub
#End Region
End Class