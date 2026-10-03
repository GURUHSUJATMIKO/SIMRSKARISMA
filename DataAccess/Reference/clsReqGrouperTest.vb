Imports DataAccess.My.Resources

Namespace Reference
    Public Class clsReqGrouperTest
        Public oConnection As Setting.clsConnectionMain = Nothing
        Public oError As Setting.clsError = Nothing

        Public sMODUL As String = ""
        Public sREFERENCE As String = ""
        Public sSTATUS As String = ""
        Public sLASTNUMBER As Integer = 0

        Public Sub New(Optional ByVal sConnection As String = "")
            If sConnection = "" Then
                oConnection = New Setting.clsConnectionMain
                oError = New Setting.clsError
            Else
                oConnection = New Setting.clsConnectionMain("TAX")
                oError = New Setting.clsError("TAX")
            End If

            sMODUL = "GROUPER TEST"
        End Sub
        Protected Overrides Sub Finalize()
            MyBase.Finalize()
            oConnection = Nothing
            GC.SuppressFinalize(Me)
        End Sub
        Public Function GetStructureHeader() As S_REQ_GROUPER_TEST
            If Not oConnection.GetConnection() Then
                GetStructureHeader = Nothing
            End If
            GetStructureHeader = New S_REQ_GROUPER_TEST
        End Function
        Public Function GetData(ByVal sKDREG As String) As S_REQ_GROUPER_TEST
            If Not oConnection.GetConnection() Then
                GetData = Nothing
                Exit Function
            End If
            GetData = oConnection.db.S_REQ_GROUPER_TESTs.FirstOrDefault(Function(x) x.KDREG = sKDREG)
        End Function
        Public Function InsertData(ByVal entity As S_REQ_GROUPER_TEST) As Boolean
            Try
                If Not oConnection.GetConnection() Then
                    InsertData = False
                    Exit Function
                End If

                sREFERENCE = entity.KDREG
                sSTATUS = "INSERT"

                Try
                    oConnection.db.S_REQ_GROUPER_TESTs.InsertOnSubmit(entity)
                Catch ex As Exception
                    oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                    Throw ex
                End Try
                Try
                    oConnection.db.SubmitChanges()
                Catch ex As Exception
                    oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                    Throw ex
                End Try

                InsertData = True
            Catch ex As Exception
                InsertData = False
                oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                Throw ex
            End Try
        End Function
        Public Function UpdateData(ByVal entity As S_REQ_GROUPER_TEST) As Boolean
            Try
                If Not oConnection.GetConnection() Then
                    UpdateData = False
                    Exit Function
                End If

                sREFERENCE = entity.KDREG
                sSTATUS = "UPDATE"

                Dim ds = oConnection.db.S_REQ_GROUPER_TESTs.FirstOrDefault(Function(x) x.KDREG = entity.KDREG)

                Try
                    oConnection.db.S_REQ_GROUPER_TESTs.DeleteOnSubmit(ds)
                    oConnection.db.S_REQ_GROUPER_TESTs.InsertOnSubmit(entity)
                Catch ex As Exception
                    oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                    Throw ex
                End Try
                Try
                    oConnection.db.SubmitChanges()
                Catch ex As Exception
                    oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                    Throw ex
                End Try

                UpdateData = True
            Catch ex As Exception
                UpdateData = False
                oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                Throw ex
            End Try
        End Function
        Public Function DeleteData(ByVal sKDREG As String) As Boolean
            Try
                If Not oConnection.GetConnection() Then
                    DeleteData = False
                    Exit Function
                End If

                sREFERENCE = sKDREG
                sSTATUS = "DELETE"

                Dim ds = oConnection.db.S_REQ_GROUPER_TESTs.FirstOrDefault(Function(x) x.KDREG = sKDREG)

                Try
                    oConnection.db.S_REQ_GROUPER_TESTs.DeleteOnSubmit(ds)
                Catch ex As Exception
                    oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                    Throw ex
                End Try
                Try
                    oConnection.db.SubmitChanges()
                Catch ex As Exception
                    oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                    Throw ex
                End Try

                DeleteData = True
            Catch ex As Exception
                DeleteData = False
                oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                Throw ex
            End Try
        End Function
    End Class
End Namespace