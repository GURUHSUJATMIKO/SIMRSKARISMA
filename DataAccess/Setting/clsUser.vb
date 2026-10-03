Namespace Setting
    Public Class clsUser
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
            sMODUL = "USER"
        End Sub
        Protected Overrides Sub Finalize()
            MyBase.Finalize()
            oConnection = Nothing
            GC.SuppressFinalize(Me)
        End Sub
        Public Function GetStructureHeader() As SET_USER
            If Not oConnection.GetConnection() Then
                GetStructureHeader = Nothing
            End If
            GetStructureHeader = New SET_USER
        End Function
        Public Function GetStructureDetail() As SET_USER_D
            If Not oConnection.GetConnection() Then
                GetStructureDetail = Nothing
            End If
            GetStructureDetail = New SET_USER_D
        End Function
        Public Function GetStructureDetailList() As List(Of SET_USER_D)
            If Not oConnection.GetConnection() Then
                GetStructureDetailList = Nothing
            End If
            GetStructureDetailList = New List(Of SET_USER_D)
        End Function
        Public Function GetStructureDetailT() As SET_USER_KDITEM_L1
            If Not oConnection.GetConnection() Then
                GetStructureDetailT = Nothing
            End If
            GetStructureDetailT = New SET_USER_KDITEM_L1
        End Function
        Public Function GetStructureDetaiTlList() As List(Of SET_USER_KDITEM_L1)
            If Not oConnection.GetConnection() Then
                GetStructureDetaiTlList = Nothing
            End If
            GetStructureDetaiTlList = New List(Of SET_USER_KDITEM_L1)
        End Function
        Public Function GetData() As List(Of SET_USER)
            If Not oConnection.GetConnection() Then
                GetData = Nothing
                Exit Function
            End If
            GetData = oConnection.db.SET_USERs.OrderBy(Function(x) x.KDUSER).ToList()
        End Function
        Public Function GetData(ByVal sKDUSER As String) As SET_USER
            If Not oConnection.GetConnection() Then
                GetData = Nothing
                Exit Function
            End If
            GetData = oConnection.db.SET_USERs.FirstOrDefault(Function(x) x.KDUSER = sKDUSER)
        End Function
        Public Function GetDataDetail() As List(Of SET_USER_D)
            If Not oConnection.GetConnection() Then
                GetDataDetail = Nothing
                Exit Function
            End If
            GetDataDetail = oConnection.db.SET_USER_Ds.ToList()
        End Function
        Public Function GetDataDetail(ByVal sKDUSER As String) As List(Of SET_USER_D)
            If Not oConnection.GetConnection() Then
                GetDataDetail = Nothing
                Exit Function
            End If
            GetDataDetail = oConnection.db.SET_USER_Ds.Where(Function(x) x.KDUSER = sKDUSER).ToList()
        End Function
        Public Function GetDataDetailT() As List(Of SET_USER_KDITEM_L1)
            If Not oConnection.GetConnection() Then
                GetDataDetailT = Nothing
                Exit Function
            End If
            GetDataDetailT = oConnection.db.SET_USER_KDITEM_L1s.ToList()
        End Function
        Public Function GetDataDetailT(ByVal sKDUSER As String) As List(Of SET_USER_KDITEM_L1)
            If Not oConnection.GetConnection() Then
                GetDataDetailT = Nothing
                Exit Function
            End If
            GetDataDetailT = oConnection.db.SET_USER_KDITEM_L1s.Where(Function(x) x.KDUSER = sKDUSER).ToList()
        End Function
        Public Function IsExist(ByVal sKDUSER As String) As Boolean
            If Not oConnection.GetConnection() Then
                IsExist = False
                Exit Function
            End If

            Dim ds = oConnection.db.SET_USERs.FirstOrDefault(Function(x) x.KDUSER = sKDUSER)

            If ds IsNot Nothing Then
                IsExist = True
            Else
                IsExist = False
            End If
        End Function
        Public Function InsertData(ByVal entity As SET_USER, ByVal entityDetail As List(Of SET_USER_D), ByVal entityDetailT As List(Of SET_USER_KDITEM_L1)) As Boolean
            Try
                If Not oConnection.GetConnection() Then
                    InsertData = False
                    Exit Function
                End If

                sREFERENCE = entity.KDUSER
                sSTATUS = "INSERT"

                Try
                    oConnection.db.SET_USERs.InsertOnSubmit(entity)
                    oConnection.db.SET_USER_Ds.InsertAllOnSubmit(entityDetail)
                    oConnection.db.SET_USER_KDITEM_L1s.InsertAllOnSubmit(entityDetailT)
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
        Public Function UpdateData(ByVal entity As SET_USER, ByVal entityDetail As List(Of SET_USER_D), ByVal entityDetailT As List(Of SET_USER_KDITEM_L1)) As Boolean
            Try
                If Not oConnection.GetConnection() Then
                    UpdateData = False
                    Exit Function
                End If

                sREFERENCE = entity.KDUSER
                sSTATUS = "UPDATE"

                Dim ds = oConnection.db.SET_USERs.FirstOrDefault(Function(x) x.KDUSER = entity.KDUSER)
                Dim dsDetail = oConnection.db.SET_USER_Ds.Where(Function(x) x.KDUSER = entity.KDUSER)
                Dim dsDetailT = oConnection.db.SET_USER_KDITEM_L1s.Where(Function(x) x.KDUSER = entity.KDUSER)

                Try
                    oConnection.db.SET_USERs.DeleteOnSubmit(ds)
                    oConnection.db.SET_USERs.InsertOnSubmit(entity)
                    If entityDetail IsNot Nothing Then
                        oConnection.db.SET_USER_Ds.DeleteAllOnSubmit(dsDetail)
                        oConnection.db.SET_USER_Ds.InsertAllOnSubmit(entityDetail)
                    End If
                    If entityDetailT IsNot Nothing Then
                        oConnection.db.SET_USER_KDITEM_L1s.DeleteAllOnSubmit(dsDetailT)
                        oConnection.db.SET_USER_KDITEM_L1s.InsertAllOnSubmit(entityDetailT)
                    End If
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
        Public Function DeleteData(ByVal sKDUSER As String) As Boolean
            Try
                If Not oConnection.GetConnection() Then
                    DeleteData = False
                    Exit Function
                End If

                sREFERENCE = sKDUSER
                sSTATUS = "DELETE"

                Dim ds = oConnection.db.SET_USERs.FirstOrDefault(Function(x) x.KDUSER = sKDUSER)
                Dim dsDetail = oConnection.db.SET_USER_Ds.Where(Function(x) x.KDUSER = sKDUSER)
                Dim dsDetailT = oConnection.db.SET_USER_KDITEM_L1s.Where(Function(x) x.KDUSER = sKDUSER)

                Try
                    oConnection.db.SET_USERs.DeleteOnSubmit(ds)
                    oConnection.db.SET_USER_Ds.DeleteAllOnSubmit(dsDetail)
                    oConnection.db.SET_USER_KDITEM_L1s.DeleteAllOnSubmit(dsDetailT)
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