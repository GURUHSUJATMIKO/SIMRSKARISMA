Imports DataAccess.My.Resources

Namespace Reference
    Public Class clsItemRadiologi
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

            sMODUL = "DAFTAR_L1"
        End Sub
        Protected Overrides Sub Finalize()
            MyBase.Finalize()
            oConnection = Nothing
            GC.SuppressFinalize(Me)
        End Sub
        Public Function GetStructureHeader() As M_ITEMRONTGEN
            If Not oConnection.GetConnection() Then
                GetStructureHeader = Nothing
            End If
            GetStructureHeader = New M_ITEMRONTGEN
        End Function
        Public Function GetData() As List(Of M_ITEMRONTGEN)
            If Not oConnection.GetConnection() Then
                GetData = Nothing
                Exit Function
            End If
            GetData = oConnection.db.M_ITEMRONTGENs.OrderBy(Function(x) x.MEMO).ToList()
        End Function
        Public Function GetData(ByVal sKDRONTGEN As Integer) As M_ITEMRONTGEN
            If Not oConnection.GetConnection() Then
                GetData = Nothing
                Exit Function
            End If
            GetData = oConnection.db.M_ITEMRONTGENs.FirstOrDefault(Function(x) x.KDRONTGEN = sKDRONTGEN)
        End Function
        Public Function GetDataSync() As List(Of M_ITEMRONTGEN)
            If Not oConnection.GetConnection() Then
                GetDataSync = Nothing
                Exit Function
            End If
            GetDataSync = oConnection.db.M_ITEMRONTGENs.OrderBy(Function(x) x.MEMO).ToList()
        End Function
        Public Function IsExist(ByVal sMEMO As String) As Boolean
            If Not oConnection.GetConnection() Then
                IsExist = False
                Exit Function
            End If

            Dim ds = oConnection.db.M_ITEMRONTGENs.FirstOrDefault(Function(x) x.MEMO = sMEMO)

            If ds IsNot Nothing Then
                IsExist = True
            Else
                IsExist = False
            End If
        End Function
        Public Function IsExistKode(ByVal sKDRONTGEN As String) As Boolean
            If Not oConnection.GetConnection() Then
                IsExistKode = False
                Exit Function
            End If

            Dim ds = oConnection.db.M_ITEMRONTGENs.FirstOrDefault(Function(x) x.KDRONTGEN = sKDRONTGEN)

            If ds IsNot Nothing Then
                IsExistKode = True
            Else
                IsExistKode = False
            End If
        End Function
        Public Function InsertData(ByVal entity As M_ITEMRONTGEN) As Boolean
            Try
                If Not oConnection.GetConnection() Then
                    InsertData = False
                    Exit Function
                End If

                sREFERENCE = entity.KDRONTGEN
                sSTATUS = "INSERT"

                Try
                    oConnection.db.M_ITEMRONTGENs.InsertOnSubmit(entity)
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
        Public Function UpdateData(ByVal entity As M_ITEMRONTGEN) As Boolean
            Try
                If Not oConnection.GetConnection() Then
                    UpdateData = False
                    Exit Function
                End If

                sREFERENCE = entity.KDRONTGEN
                sSTATUS = "UPDATE"

                Dim ds = oConnection.db.M_ITEMRONTGENs.FirstOrDefault(Function(x) x.KDRONTGEN = entity.KDRONTGEN)

                Try
                    oConnection.db.M_ITEMRONTGENs.DeleteOnSubmit(ds)
                    oConnection.db.M_ITEMRONTGENs.InsertOnSubmit(entity)
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
        Public Function DeleteData(ByVal sKDRONTGEN As String) As Boolean
            Try
                If Not oConnection.GetConnection() Then
                    DeleteData = False
                    Exit Function
                End If

                sREFERENCE = sKDRONTGEN
                sSTATUS = "DELETE"

                Dim ds = oConnection.db.M_ITEMRONTGENs.FirstOrDefault(Function(x) x.KDRONTGEN = sKDRONTGEN)

                Try
                    oConnection.db.M_ITEMRONTGENs.DeleteOnSubmit(ds)
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