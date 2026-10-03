Imports DataAccess
Imports System.Linq

Public Class frmTemplateReseptList
    Private oFormMode As FORM_MODE = FORM_MODE.FORM_MODE_VIEW
    Private oTemplateResep As New Reference.clsTemplateResep

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
                      Where x.MODUL = "TEMPLATERESEP" _
                      And y.KDUSER = sUserID
                      Select x.ISADD, x.ISDELETE, x.ISUPDATE, x.ISPRINT, x.ISVIEW).FirstOrDefault

            Try
                picAdd.Enabled = ds.ISADD
                picDelete.Enabled = ds.ISDELETE
                picUpdate.Enabled = ds.ISUPDATE
                picRefresh.Enabled = ds.ISVIEW

                If ds.ISVIEW = True Then
                    fn_LoadData()
                End If
            Catch ex As Exception
                MsgBox("Keamanan belum dipasang, tolong kontak admin anda!", MsgBoxStyle.Exclamation, Me.Text)

                picAdd.Enabled = False
                picDelete.Enabled = False
                picUpdate.Enabled = False
                picRefresh.Enabled = False
            End Try
        Catch ex As Exception
            MsgBox("Load Security : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadData()
        Try
            Dim ds = From x In oTemplateResep.GetData
                     Select x.KDTEMPLATE, x.DESCRIPTION, x.NOIDUSER
            grd.DataSource = ds.ToList

            fn_LoadFormatData()
        Catch ex As Exception
            MsgBox("Load Data : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadFormatData()
        grv.Columns("KDTEMPLATE").Caption = "Nomer."
        grv.Columns("DESCRIPTION").Caption = "Keterangan"
        grv.Columns("NOIDUSER").Caption = "User"

        grv.Columns("KDTEMPLATE").VisibleIndex = -1
    End Sub
    Private Function fn_DeleteData(ByVal sKDTEMPLATE As String) As Boolean
        Try
            oTemplateResep.DeleteData(sKDTEMPLATE)
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
            Case Keys.R
                If e.Alt = True And picRefresh.Enabled = True Then
                    picRefresh_Click()
                End If
        End Select
    End Sub
    Private Sub grv_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles grv.DoubleClick
        If grv.GetFocusedRowCellValue("KDTEMPLATE") Is Nothing Then
            Exit Sub
        End If

        Dim frmTemplateResep As New frmTemplateResep
        Try
            frmTemplateResep.LoadMe(FORM_MODE.FORM_MODE_VIEW, grv.GetFocusedRowCellValue("KDTEMPLATE"))
            frmTemplateResep.ShowDialog(Me)
        Catch ex As Exception
            MsgBox("Load Form Detail : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub picAdd_Click() Handles picAdd.Click
        Dim frmTemplateResep As New frmTemplateResep
        Try
            frmTemplateResep.LoadMe(FORM_MODE.FORM_MODE_ADD)
            frmTemplateResep.ShowDialog(Me)
            fn_LoadSecurity()
        Catch ex As Exception
            MsgBox("Load Form Detail : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
        Finally
            If Not frmTemplateResep Is Nothing Then frmTemplateResep.Dispose()
            frmTemplateResep = Nothing

            Dim rowHandle As Integer = grv.LocateByValue(rowHandle, grv.Columns("KDTEMPLATE"), sCode)
            If rowHandle > 0 Then grv.FocusedRowHandle = rowHandle

            If sStatusSave = "NEW" Then
                sStatusSave = "NONE"
                picAdd_Click()
            End If
        End Try
    End Sub
    Private Sub picUpdate_Click() Handles picUpdate.Click
        If grv.GetFocusedRowCellValue("KDTEMPLATE") Is Nothing Then
            Exit Sub
        End If
        Dim frmTemplateResep As New frmTemplateResep
        Try
            frmTemplateResep.LoadMe(FORM_MODE.FORM_MODE_EDIT, grv.GetFocusedRowCellValue("KDTEMPLATE"))
            frmTemplateResep.ShowDialog(Me)
            fn_LoadSecurity()
        Catch ex As Exception
            MsgBox("Load Form Detail : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
        Finally
            If Not frmTemplateResep Is Nothing Then frmTemplateResep.Dispose()
            frmTemplateResep = Nothing

            Dim rowHandle As Integer = grv.LocateByValue(rowHandle, grv.Columns("KDTEMPLATE"), sCode)
            If rowHandle > 0 Then grv.FocusedRowHandle = rowHandle

            If sStatusSave = "NEW" Then
                sStatusSave = "NONE"
                picAdd_Click()
            End If
        End Try
    End Sub
    Private Sub picDelete_Click() Handles picDelete.Click
        If grv.GetFocusedRowCellValue("KDTEMPLATE") Is Nothing Then
            Exit Sub
        End If
        If MsgBox("Delete " & grv.GetFocusedRowCellValue("KDTEMPLATE") & "?", MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, Me.Text) = MsgBoxResult.No Then Exit Sub
        If fn_DeleteData(grv.GetFocusedRowCellValue("KDTEMPLATE")) = False Then
            MsgBox("Hapus gagal! tolong kontak admin anda!", MsgBoxStyle.Exclamation, Me.Text)
            Exit Sub
        End If
        MsgBox("Delete " & grv.GetFocusedRowCellValue("KDTEMPLATE") & " success!", MsgBoxStyle.Information, Me.Text)
        fn_LoadSecurity()
    End Sub
    Private Sub picRefresh_Click() Handles picRefresh.Click
        fn_LoadSecurity()
    End Sub
#End Region
End Class