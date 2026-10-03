Namespace Setting
    Public Class clsError
        Public oConnection As Setting.clsConnectionMain = Nothing
        Public Sub New(Optional ByVal sConnection As String = "")
            If sConnection = "" Then
                oConnection = New Setting.clsConnectionMain
            Else
                oConnection = New Setting.clsConnectionMain("TAX")
            End If
        End Sub
        Protected Overrides Sub Finalize()
            MyBase.Finalize()
            oConnection = Nothing
            GC.SuppressFinalize(Me)
        End Sub
        Public Function GetStructureHeader() As SET_ERROR
            If Not oConnection.GetConnection() Then
                GetStructureHeader = Nothing
            End If
            GetStructureHeader = New SET_ERROR
        End Function
        Public Function GetData() As List(Of SET_ERROR)
            If Not oConnection.GetConnection() Then
                GetData = Nothing
                Exit Function
            End If
            GetData = oConnection.db.SET_ERRORs.OrderBy(Function(x) x.DATE).ToList()
        End Function
        Public Function InsertData(ByVal sMODUL As String, ByVal sSTATUS As String, ByVal sDESCRIPTION As String, ByVal sREFERENCE As String) As Boolean
            Try
                If Not oConnection.GetConnection() Then
                    InsertData = False
                    Exit Function
                End If
                Dim entity = GetStructureHeader()
                Try
                    With entity
                        .MODUL = sMODUL
                        .STATUS = sSTATUS
                        .DESCRIPTION = sDESCRIPTION
                        .DATE = Now
                        .REFERENCE = sREFERENCE
                    End With
                Catch ex As Exception
                    Throw ex
                End Try
                Try
                    oConnection.db.SET_ERRORs.InsertOnSubmit(entity)
                Catch ex As Exception
                    Throw ex
                End Try
                Try
                    oConnection.db.SubmitChanges()
                Catch ex As Exception
                    Throw ex
                End Try

                InsertData = True
            Catch ex As Exception
                InsertData = False
                Throw ex
            End Try
        End Function
    End Class
End Namespace