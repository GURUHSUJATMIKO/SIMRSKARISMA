Imports DataAccess
Imports UI.WIN.MAIN.My.Resources

Public Class frmChangePassword
#Region "Function"
    Public Function fn_Validate() As Boolean
        fn_Validate = True

        If txtCurrent.Text = String.Empty Then
            MsgBox("Current password is incorrect!", MsgBoxStyle.Exclamation, Me.Text)
            txtCurrent.Focus()
            Return False
        End If
        Dim oUser As New Setting.clsUser
        Dim ds = oUser.GetData(sUserID)

        If txtCurrent.Text <> ds.PASSWORD Then
            MsgBox("Current password is incorrect!", MsgBoxStyle.Exclamation, Me.Text)
            txtCurrent.Focus()
            Return False
        End If
        If txtNew.Text = String.Empty Then
            MsgBox("New password is incorrect!", MsgBoxStyle.Exclamation, Me.Text)
            txtNew.Focus()
            Return False
        End If
        If txtConfirm.Text = String.Empty Then
            MsgBox("Confirm password is incorrect!", MsgBoxStyle.Exclamation, Me.Text)
            txtConfirm.Focus()
            Return False
        End If
        If txtNew.Text <> txtConfirm.Text Then
            MsgBox("Password New and Confirm Incorrect!", MsgBoxStyle.Exclamation, Me.Text)
            txtConfirm.Focus()
            Return False
        End If
    End Function
    Public Function fn_Save() As Boolean
        Try
            Dim oUser As New Setting.clsUser
            Dim ds = oUser.GetData(sUserID)
            Dim sOtority = ds.KDOTORITY

            Dim dsSave = oUser.GetStructureHeader
            With dsSave
                .KDUSER = sUserID
                .KDSERVER = ds.KDSERVER
                .KDDATABASE = ds.KDDATABASE
                .KDSERVER_TAX = ds.KDSERVER_TAX
                .KDDATABASE_TAX = ds.KDDATABASE_TAX
                .KDOTORITY = ds.KDOTORITY
                .KDCOMPANY = ds.KDCOMPANY
                .PASSWORD = txtNew.Text.Trim
                .ISACTIVE = True
                Try
                    .NIK = oUser.GetData(sUserID).NIK
                Catch ex As Exception
                    .NIK = ""
                End Try

            End With

            Dim arrDetail = oUser.GetStructureDetailList

            Dim dsDetail_ = oUser.GetDataDetail(sUserID)

            For Each xloop In dsDetail_
                Dim dsDetail = oUser.GetStructureDetail
                With dsDetail
                    .KDUSER = xloop.KDUSER
                    .SEQ = xloop.SEQ
                    .KDWAREHOUSE = xloop.KDWAREHOUSE
                    .ISDEFAULT = xloop.ISDEFAULT
                    .REMARKS = ""
                End With
                arrDetail.Add(dsDetail)
            Next
            Try
                fn_Save = oUser.UpdateData(dsSave, Nothing, Nothing)
            Catch ex As Exception
                MsgBox(Statement.ErrorStatement & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
            End Try
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
#End Region
#Region "Command Button"
    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        If fn_Validate() = False Then Exit Sub
        If fn_Save() = False Then
            MsgBox(Statement.SaveFail, MsgBoxStyle.Exclamation, Me.Text)
        Else
            MsgBox(Statement.SaveSuccess, MsgBoxStyle.Information, Me.Text)
            Me.Close()
        End If
    End Sub
    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Me.Close()
    End Sub
#End Region
End Class