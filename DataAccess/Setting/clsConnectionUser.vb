Namespace Setting
    Public Class clsConnectionUser
        Public db As DataUserDataContext

        Public Function GetConnection() As Boolean
            Try
                If My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Software\SIMRS\USER\", "Database", "") <> Nothing Then
                    db = New DataUserDataContext(Decrypt(My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Software\SIMRS\USER\", "Database", "").ToString()))

                    If db.DatabaseExists() Then
                        Return True
                    Else
                        db = Nothing
                        MsgBox("Failed To Connect Database! Please Check Database Connection!", MsgBoxStyle.Critical, "Connection Error")
                        Return False
                    End If
                Else
                    db = Nothing
                    MsgBox("Failed To Connect Database! Please Check Database Connection!", MsgBoxStyle.Critical, "Connection Error")
                    Return False
                End If
            Catch ex As Exception
                db = Nothing
                MsgBox("Failed To Connect Database! Please Check Database Connection!", MsgBoxStyle.Critical, "Connection Error")
                Return False
            End Try
        End Function
        Public Function SaveToRegistry(ByVal sDatabase As String) As Boolean
            Try
                My.Computer.Registry.SetValue("HKEY_CURRENT_USER\Software\SIMRS\USER\", "Database", sDatabase)
                Return True
            Catch ex As Exception
                Return False
            End Try
        End Function
    End Class
End Namespace
