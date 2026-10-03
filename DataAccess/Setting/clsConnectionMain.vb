Namespace Setting
    Public Class clsConnectionMain
        Public db As DataMainDataContext
        Public isTax = False

        Public Sub New(Optional ByVal Connection As String = "")
            If Connection = "" Then
                isTax = False
            Else
                isTax = True
            End If
        End Sub
        Public Function GetConnection() As Boolean
            If isTax = False Then
                Try
                    If My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Software\SIMRS\SW\", "Database", "") <> Nothing Then
                        db = New DataMainDataContext(Decrypt(My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Software\SIMRS\SW\", "Database", "").ToString()))

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
            Else
                Try
                    If My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Software\SIMRS\SW\", "Database_Tax", "") <> Nothing Then
                        db = New DataMainDataContext(Decrypt(My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Software\SIMRS\SW\", "Database_Tax", "").ToString()))

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
            End If
        End Function
        Public Function SaveToRegistry(ByVal sDatabase As String) As Boolean
            Try
                My.Computer.Registry.SetValue("HKEY_CURRENT_USER\Software\SIMRS\SW\", "Database", sDatabase)
                Return True
            Catch ex As Exception
                Return False
            End Try
        End Function
        Public Function SaveToRegistryTax(ByVal sDatabase As String) As Boolean
            Try
                My.Computer.Registry.SetValue("HKEY_CURRENT_USER\Software\SIMRS\SW\", "Database_Tax", sDatabase)
                Return True
            Catch ex As Exception
                Return False
            End Try
        End Function
        Public Function BackupData(ByVal sDatabase As String, Optional ByVal sLocation As String = "") As Boolean
            Try
                If GetConnection() Then
                    Try
                        If System.IO.File.Exists(sLocation) Then
                            System.IO.File.Delete(sLocation)
                        End If

                        Dim sComm = "BACKUP DATABASE [" & sDatabase & "] TO DISK='" & sLocation & "' WITH FORMAT"

                        db.ExecuteCommand(sComm)

                        db.SubmitChanges()

                        Return True
                    Catch ex As Exception
                        Throw ex
                        Return False
                    End Try
                Else
                    Return False
                End If
            Catch ex As Exception
                Return False
            End Try
        End Function
        Public Function RestoreData(ByVal sDatabase As String, Optional ByVal sLocation As String = "") As Boolean
            Try
                If GetConnection() Then
                    Try
                        If db.Connection.State = ConnectionState.Open Then
                            db.Connection.Close()
                            db.Connection.Open()
                        End If

                        Dim sComm = "USE MASTER RESTORE DATABASE [" & sDatabase & "] FROM DISK='" & sLocation & "' WITH REPLACE"

                        db.ExecuteCommand(sComm)

                        db.SubmitChanges()

                        Return True
                    Catch ex As Exception
                        Return False
                    End Try
                Else
                    Return False
                End If
            Catch ex As Exception
                Return False
            End Try
        End Function
    End Class
End Namespace
