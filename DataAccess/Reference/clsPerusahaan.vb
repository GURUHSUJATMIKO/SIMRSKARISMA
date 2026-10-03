Namespace Reference
    Public Class clsPerusahaan
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

            sMODUL = "PERUSAHAAN"
        End Sub
        Protected Overrides Sub Finalize()
            MyBase.Finalize()
            oConnection = Nothing
            GC.SuppressFinalize(Me)
        End Sub
        Public Function GetStructureHeader() As M_PERUSAHAAN
            If Not oConnection.GetConnection() Then
                GetStructureHeader = Nothing
            End If
            GetStructureHeader = New M_PERUSAHAAN
        End Function
        Public Function GetData() As List(Of M_PERUSAHAAN)
            If Not oConnection.GetConnection() Then
                GetData = Nothing
                Exit Function
            End If
            GetData = oConnection.db.M_PERUSAHAANs.OrderBy(Function(x) x.NAME_DISPLAY).ToList()
        End Function
        Public Function GetData(ByVal sKDPERUSAHAAN As String) As M_PERUSAHAAN
            If Not oConnection.GetConnection() Then
                GetData = Nothing
                Exit Function
            End If
            GetData = oConnection.db.M_PERUSAHAANs.FirstOrDefault(Function(x) x.KDPERUSAHAAN = sKDPERUSAHAAN)
        End Function
        Public Function GetDataSync() As List(Of M_PERUSAHAAN)
            If Not oConnection.GetConnection() Then
                GetDataSync = Nothing
                Exit Function
            End If
            GetDataSync = oConnection.db.M_PERUSAHAANs.OrderBy(Function(x) x.NAME_DISPLAY).ToList()
        End Function
        Public Function IsExist(ByVal sNAME_DISPLAY As String) As Boolean
            If Not oConnection.GetConnection() Then
                IsExist = False
                Exit Function
            End If

            Dim ds = oConnection.db.M_PERUSAHAANs.FirstOrDefault(Function(x) x.NAME_DISPLAY = sNAME_DISPLAY)

            If ds IsNot Nothing Then
                IsExist = True
            Else
                IsExist = False
            End If
        End Function
        Public Function InsertData(ByVal entity As M_PERUSAHAAN) As Boolean
            Try
                If Not oConnection.GetConnection() Then
                    InsertData = False
                    Exit Function
                End If

                sREFERENCE = entity.KDPERUSAHAAN
                sSTATUS = "INSERT"

                'Generate Auto Number
                Try
                    sLASTNUMBER = CInt(oConnection.db.M_PERUSAHAANs.OrderByDescending(Function(x) x.KDPERUSAHAAN).FirstOrDefault().KDPERUSAHAAN.Remove(0, (sMODUL & " _ ").Length)) + 1
                Catch ex As Exception
                    sLASTNUMBER = 1
                End Try
                'End Generate

                Try
                    entity.KDPERUSAHAAN = sMODUL & "_" & AutoNumberCode(sLASTNUMBER)
                    oConnection.db.M_PERUSAHAANs.InsertOnSubmit(entity)
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
        Public Function UpdateData(ByVal entity As M_PERUSAHAAN) As Boolean
            Try
                If Not oConnection.GetConnection() Then
                    UpdateData = False
                    Exit Function
                End If

                sREFERENCE = entity.KDPERUSAHAAN
                sSTATUS = "UPDATE"

                Dim ds = oConnection.db.M_PERUSAHAANs.FirstOrDefault(Function(x) x.KDPERUSAHAAN = entity.KDPERUSAHAAN)

                Try
                    oConnection.db.M_PERUSAHAANs.DeleteOnSubmit(ds)
                    oConnection.db.M_PERUSAHAANs.InsertOnSubmit(entity)
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
        Public Function DeleteData(ByVal sKDPERUSAHAAN As String) As Boolean
            Try
                If Not oConnection.GetConnection() Then
                    DeleteData = False
                    Exit Function
                End If

                sREFERENCE = sKDPERUSAHAAN
                sSTATUS = "DELETE"

                Dim ds = oConnection.db.M_PERUSAHAANs.FirstOrDefault(Function(x) x.KDPERUSAHAAN = sKDPERUSAHAAN)

                Try
                    oConnection.db.M_PERUSAHAANs.DeleteOnSubmit(ds)
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