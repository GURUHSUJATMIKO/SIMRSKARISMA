Namespace Reference
    Public Class clsRuangRawat
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

            sMODUL = "RUANGRAWAT"
        End Sub
        Protected Overrides Sub Finalize()
            MyBase.Finalize()
            oConnection = Nothing
            GC.SuppressFinalize(Me)
        End Sub
        Public Function GetStructureHeader() As M_RUANGRAWAT
            If Not oConnection.GetConnection() Then
                GetStructureHeader = Nothing
            End If
            GetStructureHeader = New M_RUANGRAWAT
        End Function
        Public Function GetStructureDetail() As M_RUANGRAWAT_D
            If Not oConnection.GetConnection() Then
                GetStructureDetail = Nothing
            End If
            GetStructureDetail = New M_RUANGRAWAT_D
        End Function
        Public Function GetStructureDetailList() As List(Of M_RUANGRAWAT_D)
            If Not oConnection.GetConnection() Then
                GetStructureDetailList = Nothing
            End If
            GetStructureDetailList = New List(Of M_RUANGRAWAT_D)
        End Function
        Public Function GetData() As List(Of M_RUANGRAWAT)
            If Not oConnection.GetConnection() Then
                GetData = Nothing
                Exit Function
            End If
            GetData = oConnection.db.M_RUANGRAWATs.OrderBy(Function(x) x.NAME_DISPLAY).ToList()
        End Function
        Public Function GetData(ByVal sKDRUANGRAWAT As String) As M_RUANGRAWAT
            If Not oConnection.GetConnection() Then
                GetData = Nothing
                Exit Function
            End If
            GetData = oConnection.db.M_RUANGRAWATs.FirstOrDefault(Function(x) x.KDRUANGRAWAT = sKDRUANGRAWAT)
        End Function
        Public Function GetDataDetail() As List(Of M_RUANGRAWAT_D)
            If Not oConnection.GetConnection() Then
                GetDataDetail = Nothing
                Exit Function
            End If
            GetDataDetail = oConnection.db.M_RUANGRAWAT_Ds.ToList()
        End Function
        Public Function GetDataDetail(ByVal sKDRUANGRAWAT As String) As List(Of M_RUANGRAWAT_D)
            If Not oConnection.GetConnection() Then
                GetDataDetail = Nothing
                Exit Function
            End If
            GetDataDetail = oConnection.db.M_RUANGRAWAT_Ds.Where(Function(x) x.KDRUANGRAWAT = sKDRUANGRAWAT).ToList()
        End Function
        Public Function GetDataDetail(ByVal sKDRUANGRAWAT As String, ByVal sSEQ As Integer) As M_RUANGRAWAT_D
            If Not oConnection.GetConnection() Then
                GetDataDetail = Nothing
                Exit Function
            End If
            GetDataDetail = oConnection.db.M_RUANGRAWAT_Ds.FirstOrDefault(Function(x) x.KDRUANGRAWAT = sKDRUANGRAWAT And x.SEQ = sSEQ)
        End Function
        Public Function GetDataSync() As List(Of M_RUANGRAWAT)
            If Not oConnection.GetConnection() Then
                GetDataSync = Nothing
                Exit Function
            End If
            GetDataSync = oConnection.db.M_RUANGRAWATs.OrderBy(Function(x) x.NAME_DISPLAY).ToList()
        End Function
        Public Function IsExist(ByVal sNAME_DISPLAY As String) As Boolean
            If Not oConnection.GetConnection() Then
                IsExist = False
                Exit Function
            End If

            Dim ds = oConnection.db.M_RUANGRAWATs.FirstOrDefault(Function(x) x.NAME_DISPLAY = sNAME_DISPLAY)

            If ds IsNot Nothing Then
                IsExist = True
            Else
                IsExist = False
            End If
        End Function
        Public Function InsertData(ByVal entity As M_RUANGRAWAT, ByVal entityDetail As List(Of M_RUANGRAWAT_D)) As Boolean
            Try
                If Not oConnection.GetConnection() Then
                    InsertData = False
                    Exit Function
                End If

                sREFERENCE = entity.KDRUANGRAWAT
                sSTATUS = "INSERT"

                If entity.KDRUANGRAWAT = "" Then
                    'Generate Auto Number
                    Try
                        sLASTNUMBER = CInt(oConnection.db.M_RUANGRAWATs.OrderByDescending(Function(x) x.KDRUANGRAWAT).FirstOrDefault().KDRUANGRAWAT.Remove(0, (sMODUL & " _ ").Length)) + 1
                    Catch ex As Exception
                        sLASTNUMBER = 1
                    End Try
                    'End Generate

                    entity.KDRUANGRAWAT = sMODUL & "_" & AutoNumberCode(sLASTNUMBER)

                    For Each iLoop In entityDetail
                        iLoop.KDRUANGRAWAT = entity.KDRUANGRAWAT
                    Next

                End If

                Try
                    oConnection.db.M_RUANGRAWATs.InsertOnSubmit(entity)
                    oConnection.db.M_RUANGRAWAT_Ds.InsertAllOnSubmit(entityDetail)
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
        Public Function UpdateData(ByVal entity As M_RUANGRAWAT, ByVal entityDetail As List(Of M_RUANGRAWAT_D)) As Boolean
            Try
                If Not oConnection.GetConnection() Then
                    UpdateData = False
                    Exit Function
                End If

                sREFERENCE = entity.KDRUANGRAWAT
                sSTATUS = "UPDATE"

                Dim ds = oConnection.db.M_RUANGRAWATs.FirstOrDefault(Function(x) x.KDRUANGRAWAT = entity.KDRUANGRAWAT)
                Dim dsDetail = oConnection.db.M_RUANGRAWAT_Ds.Where(Function(x) x.KDRUANGRAWAT = entity.KDRUANGRAWAT)

                Try
                    oConnection.db.M_RUANGRAWATs.DeleteOnSubmit(ds)
                    oConnection.db.M_RUANGRAWATs.InsertOnSubmit(entity)
                    oConnection.db.M_RUANGRAWAT_Ds.DeleteAllOnSubmit(dsDetail)
                    oConnection.db.M_RUANGRAWAT_Ds.InsertAllOnSubmit(entityDetail)
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
        Public Function DeleteData(ByVal Parameter As String) As Boolean
            Try
                If Not oConnection.GetConnection() Then
                    DeleteData = False
                    Exit Function
                End If

                sREFERENCE = Parameter
                sSTATUS = "DELETE"

                Dim ds = oConnection.db.M_RUANGRAWATs.FirstOrDefault(Function(x) x.KDRUANGRAWAT = Parameter)
                Dim dsDetail = oConnection.db.M_RUANGRAWAT_Ds.Where(Function(x) x.KDRUANGRAWAT = Parameter)

                Try
                    oConnection.db.M_RUANGRAWATs.DeleteOnSubmit(ds)
                    oConnection.db.M_RUANGRAWAT_Ds.DeleteAllOnSubmit(dsDetail)
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