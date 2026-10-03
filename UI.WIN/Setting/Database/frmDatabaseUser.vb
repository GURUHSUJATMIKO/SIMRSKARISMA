Imports System.Configuration
Imports DataAccess
Imports UI.WIN.MAIN.My.Resources

Public Class frmDatabaseUser
    Private Sub frmConfiguration_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Software\SIMRS\USER\", "Database", "") <> Nothing Then
            Dim arrMain() As String = Decrypt(My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Software\SIMRS\USER\", "Database", "").ToString()).Split(";")

            For iLoop As Integer = 0 To arrMain.Length - 1
                Dim arrMainResult() As String = arrMain(iLoop).Split("=")

                For xLoop As Integer = 0 To arrMainResult.Length - 1
                    If arrMainResult(xLoop) = "Data Source" Then
                        txtMainServer.Text = arrMainResult(xLoop + 1)
                    ElseIf arrMainResult(xLoop) = "Initial Catalog" Then
                        txtMainDatabase.Text = arrMainResult(xLoop + 1)
                    ElseIf arrMainResult(xLoop) = "User ID" Then
                        txtID.Text = arrMainResult(xLoop + 1)
                    End If
                Next
            Next
        End If
    End Sub
    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        If Not DataIsValid() Then Exit Sub
        If MsgBox(Statement.SaveDatabase, MsgBoxStyle.YesNo, Me.Text) = MsgBoxResult.No Then Exit Sub

        Dim oConnection As New Setting.clsConnectionUser

        If oConnection.SaveToRegistry(Encrypt("Data Source=" & txtMainServer.Text & ";Initial Catalog=" & txtMainDatabase.Text & ";Persist Security Info=True;User ID=" & txtID.Text & ";Password=" & txtPassword.Text & "")) Then
            Environment.Exit(1)
        End If
    End Sub
    Private Function DataIsValid() As Boolean
        DataIsValid = True

        If txtMainServer.Text = String.Empty Then
            txtMainServer.ErrorText = Statement.ErrorRequired
            txtMainServer.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
            DataIsValid = False
        End If
        If txtMainDatabase.Text = String.Empty Then
            txtMainDatabase.ErrorText = Statement.ErrorRequired
            txtMainDatabase.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
            DataIsValid = False
        End If
    End Function
    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Me.Close()
    End Sub
End Class