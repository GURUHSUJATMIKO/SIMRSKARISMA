Namespace Reference
    Public Class clsVendor
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

            sMODUL = "VENDOR"
        End Sub
        Protected Overrides Sub Finalize()
            MyBase.Finalize()
            oConnection = Nothing
            GC.SuppressFinalize(Me)
        End Sub
        Public Function GetStructureHeader() As M_VENDOR
            If Not oConnection.GetConnection() Then
                GetStructureHeader = Nothing
            End If
            GetStructureHeader = New M_VENDOR
        End Function
        Public Function GetData() As List(Of M_VENDOR)
            If Not oConnection.GetConnection() Then
                GetData = Nothing
                Exit Function
            End If
            GetData = oConnection.db.M_VENDORs.OrderBy(Function(x) x.NAME_DISPLAY).ToList()
        End Function
        Public Function GetData(ByVal sKDVENDOR As String) As M_VENDOR
            If Not oConnection.GetConnection() Then
                GetData = Nothing
                Exit Function
            End If
            GetData = oConnection.db.M_VENDORs.FirstOrDefault(Function(x) x.KDVENDOR = sKDVENDOR)
        End Function
        Public Function GetDataSync() As List(Of M_VENDOR)
            If Not oConnection.GetConnection() Then
                GetDataSync = Nothing
                Exit Function
            End If
            GetDataSync = oConnection.db.M_VENDORs.OrderBy(Function(x) x.NAME_DISPLAY).ToList()
        End Function
        Public Function IsExist(ByVal sNAME_DISPLAY As String) As Boolean
            If Not oConnection.GetConnection() Then
                IsExist = False
                Exit Function
            End If

            Dim ds = oConnection.db.M_VENDORs.FirstOrDefault(Function(x) x.NAME_DISPLAY = sNAME_DISPLAY)

            If ds IsNot Nothing Then
                IsExist = True
            Else
                IsExist = False
            End If
        End Function
        Public Function InsertData(ByVal entity As M_VENDOR) As Boolean
            Try
                If Not oConnection.GetConnection() Then
                    InsertData = False
                    Exit Function
                End If

                sREFERENCE = entity.KDVENDOR
                sSTATUS = "INSERT"

                'Generate Auto Number
                Try
                    sLASTNUMBER = CInt(oConnection.db.M_VENDORs.OrderByDescending(Function(x) x.KDVENDOR).FirstOrDefault().KDVENDOR.Remove(0, (sMODUL & " _ ").Length)) + 1
                Catch ex As Exception
                    sLASTNUMBER = 1
                End Try
                'End Generate

                Try
                    entity.KDVENDOR = sMODUL & "_" & AutoNumberCode(sLASTNUMBER)
                    oConnection.db.M_VENDORs.InsertOnSubmit(entity)
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
        Public Function UpdateData(ByVal entity As M_VENDOR) As Boolean
            Try
                If Not oConnection.GetConnection() Then
                    UpdateData = False
                    Exit Function
                End If

                sREFERENCE = entity.KDVENDOR
                sSTATUS = "UPDATE"

                Dim ds = oConnection.db.M_VENDORs.FirstOrDefault(Function(x) x.KDVENDOR = entity.KDVENDOR)

                Try
                    oConnection.db.M_VENDORs.DeleteOnSubmit(ds)
                    oConnection.db.M_VENDORs.InsertOnSubmit(entity)
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
        Public Function DeleteData(ByVal sKDVENDOR As String) As Boolean
            Try
                If Not oConnection.GetConnection() Then
                    DeleteData = False
                    Exit Function
                End If

                sREFERENCE = sKDVENDOR
                sSTATUS = "DELETE"

                Dim ds = oConnection.db.M_VENDORs.FirstOrDefault(Function(x) x.KDVENDOR = sKDVENDOR)

                Try
                    oConnection.db.M_VENDORs.DeleteOnSubmit(ds)
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