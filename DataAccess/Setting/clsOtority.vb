Namespace Setting
    Public Class clsOtority
        Public oConnection As Setting.clsConnectionUser = Nothing
        Public oError As Setting.clsError = Nothing
        Public sMODUL As String = ""
        Public sREFERENCE As String = ""
        Public sSTATUS As String = ""

        Public Sub New(Optional ByVal sConnection As String = "")
            oConnection = New Setting.clsConnectionUser
            If sConnection = "" Then
                oError = New Setting.clsError
            Else
                oError = New Setting.clsError("TAX")
            End If
            sMODUL = "OTORITY"
        End Sub
        Protected Overrides Sub Finalize()
            MyBase.Finalize()
            oConnection = Nothing
            GC.SuppressFinalize(Me)
        End Sub
        Public Function GetStructureHeader() As SET_OTORITY_H
            If Not oConnection.GetConnection() Then
                GetStructureHeader = Nothing
            End If
            GetStructureHeader = New SET_OTORITY_H
        End Function
        Public Function GetStructureDetail() As SET_OTORITY_D
            If Not oConnection.GetConnection() Then
                GetStructureDetail = Nothing
            End If
            GetStructureDetail = New SET_OTORITY_D
        End Function
        Public Function GetStructureDetailList() As List(Of SET_OTORITY_D)
            If Not oConnection.GetConnection() Then
                GetStructureDetailList = Nothing
            End If
            GetStructureDetailList = New List(Of SET_OTORITY_D)
        End Function
        Public Function GetData() As List(Of SET_OTORITY_H)
            If Not oConnection.GetConnection() Then
                GetData = Nothing
                Exit Function
            End If
            GetData = oConnection.db.SET_OTORITY_Hs.OrderBy(Function(x) x.KDOTORITY).ToList()
        End Function
        Public Function GetData(ByVal sParameter As String) As SET_OTORITY_H
            If Not oConnection.GetConnection() Then
                GetData = Nothing
                Exit Function
            End If
            GetData = oConnection.db.SET_OTORITY_Hs.FirstOrDefault(Function(x) x.KDOTORITY = sParameter)
        End Function
        Public Function GetDataDetail() As List(Of SET_OTORITY_D)
            If Not oConnection.GetConnection() Then
                GetDataDetail = Nothing
                Exit Function
            End If
            GetDataDetail = oConnection.db.SET_OTORITY_Ds.ToList()
        End Function
        Public Function GetDataDetail(ByVal sParameter As String) As List(Of SET_OTORITY_D)
            If Not oConnection.GetConnection() Then
                GetDataDetail = Nothing
                Exit Function
            End If
            GetDataDetail = oConnection.db.SET_OTORITY_Ds.Where(Function(x) x.KDOTORITY = sParameter).ToList()
        End Function
        Public Function IsExist(ByVal sKDOTORITY As String) As Boolean
            If Not oConnection.GetConnection() Then
                IsExist = False
                Exit Function
            End If

            Dim ds = oConnection.db.SET_OTORITY_Hs.FirstOrDefault(Function(x) x.KDOTORITY = sKDOTORITY)

            If ds IsNot Nothing Then
                IsExist = True
            Else
                IsExist = False
            End If
        End Function
        Public Function InsertData(ByVal entity As SET_OTORITY_H, ByVal entity_D As List(Of SET_OTORITY_D)) As Boolean
            Try
                If Not oConnection.GetConnection() Then
                    InsertData = False
                    Exit Function
                End If

                sREFERENCE = entity.KDOTORITY
                sSTATUS = "INSERT"

                Try
                    oConnection.db.SET_OTORITY_Hs.InsertOnSubmit(entity)
                    oConnection.db.SET_OTORITY_Ds.InsertAllOnSubmit(entity_D)
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
        Public Function UpdateData(ByVal entity As SET_OTORITY_H, ByVal entity_D As List(Of SET_OTORITY_D)) As Boolean
            Try
                If Not oConnection.GetConnection() Then
                    UpdateData = False
                    Exit Function
                End If

                sREFERENCE = entity.KDOTORITY
                sSTATUS = "UPDATE"

                Dim ds = oConnection.db.SET_OTORITY_Hs.FirstOrDefault(Function(x) x.KDOTORITY = entity.KDOTORITY)

                Try
                    oConnection.db.SET_OTORITY_Hs.DeleteOnSubmit(ds)
                    oConnection.db.SET_OTORITY_Hs.InsertOnSubmit(entity)
                Catch ex As Exception
                    oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                    Throw ex
                End Try

                Dim ds_D = oConnection.db.SET_OTORITY_Ds.Where(Function(x) x.KDOTORITY = entity.KDOTORITY)

                Try
                    oConnection.db.SET_OTORITY_Ds.DeleteAllOnSubmit(ds_D)
                    oConnection.db.SET_OTORITY_Ds.InsertAllOnSubmit(entity_D)
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
        Public Function DeleteData(ByVal sKDOTORITY As String) As Boolean
            Try
                If Not oConnection.GetConnection() Then
                    DeleteData = False
                    Exit Function
                End If

                sREFERENCE = sKDOTORITY
                sSTATUS = "DELETE"

                Dim ds = oConnection.db.SET_OTORITY_Hs.FirstOrDefault(Function(x) x.KDOTORITY = sKDOTORITY)

                Try
                    oConnection.db.SET_OTORITY_Hs.DeleteOnSubmit(ds)
                Catch ex As Exception
                    oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                    Throw ex
                End Try

                Dim ds_D = oConnection.db.SET_OTORITY_Ds.Where(Function(x) x.KDOTORITY = sKDOTORITY)

                Try
                    oConnection.db.SET_OTORITY_Ds.DeleteAllOnSubmit(ds_D)
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