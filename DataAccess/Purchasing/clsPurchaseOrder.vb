Namespace Purchasing
    Public Class clsPurchaseOrder
        Public oConnection As Setting.clsConnectionMain = Nothing
        Public oError As Setting.clsError = Nothing
        Public oCounter As Setting.clsCounter = Nothing

        Public sMODUL As String = ""
        Public sREFERENCE As String = ""
        Public sSTATUS As String = ""
        Public sLASTNUMBER As Integer = 0

        Public Sub New(Optional ByVal sConnection As String = "")
            If sConnection = "" Then
                oConnection = New Setting.clsConnectionMain
                oError = New Setting.clsError
                oCounter = New Setting.clsCounter
            Else
                oConnection = New Setting.clsConnectionMain("TAX")
                oError = New Setting.clsError("TAX")
                oCounter = New Setting.clsCounter("TAX")
            End If

            sMODUL = "PO"
        End Sub
        Protected Overrides Sub Finalize()
            MyBase.Finalize()
            oConnection = Nothing
            GC.SuppressFinalize(Me)
        End Sub
        Public Function GetStructureHeader() As P_PO_H
            If Not oConnection.GetConnection() Then
                GetStructureHeader = Nothing
            End If
            GetStructureHeader = New P_PO_H
        End Function
        Public Function GetStructureDetail() As P_PO_D
            If Not oConnection.GetConnection() Then
                GetStructureDetail = Nothing
            End If
            GetStructureDetail = New P_PO_D
        End Function
        Public Function GetStructureDetailList() As List(Of P_PO_D)
            If Not oConnection.GetConnection() Then
                GetStructureDetailList = Nothing
            End If
            GetStructureDetailList = New List(Of P_PO_D)
        End Function
        Public Function GetData() As List(Of P_PO_H)
            If Not oConnection.GetConnection() Then
                GetData = Nothing
                Exit Function
            End If
            GetData = oConnection.db.P_PO_Hs.OrderByDescending(Function(x) x.KDPO).ToList()
        End Function
        Public Function GetData(ByVal sKDPO As String) As P_PO_H
            If Not oConnection.GetConnection() Then
                GetData = Nothing
                Exit Function
            End If
            GetData = oConnection.db.P_PO_Hs.FirstOrDefault(Function(x) x.KDPO = sKDPO)
        End Function
        Public Function GetDataDetail() As List(Of P_PO_D)
            If Not oConnection.GetConnection() Then
                GetDataDetail = Nothing
                Exit Function
            End If
            GetDataDetail = oConnection.db.P_PO_Ds.ToList()
        End Function
        Public Function GetDataDetail(ByVal sKDPO As String) As List(Of P_PO_D)
            If Not oConnection.GetConnection() Then
                GetDataDetail = Nothing
                Exit Function
            End If
            GetDataDetail = oConnection.db.P_PO_Ds.Where(Function(x) x.KDPO = sKDPO).ToList()
        End Function
        Public Function InsertData(ByVal entity As P_PO_H, ByVal entityDetail As List(Of P_PO_D)) As Boolean
            Try
                If Not oConnection.GetConnection() Then
                    InsertData = False
                    Exit Function
                End If

                sREFERENCE = entity.KDPO
                sSTATUS = "INSERT"

                Try
                    sLASTNUMBER = oCounter.GetLastNumber(sMODUL, entity.DATE)
                    If sLASTNUMBER = 0 Then
                        Try
                            oCounter.InsertData(sMODUL, entity.DATE)
                            sLASTNUMBER = oCounter.GetLastNumber(sMODUL, entity.DATE)
                        Catch ex As Exception
                            sLASTNUMBER = 0
                        End Try
                    End If

                    entity.KDPO = AutoNumber(sMODUL, sLASTNUMBER + 1, entity.DATE)
                    For Each iLoop In entityDetail
                        iLoop.KDPO = entity.KDPO
                    Next
                Catch ex As Exception
                    oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                    Throw ex
                End Try

                Try
                    oConnection.db.P_PO_Hs.InsertOnSubmit(entity)
                    oConnection.db.P_PO_Ds.InsertAllOnSubmit(entityDetail)
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
                Try
                    oCounter.UpdateData(sMODUL, sLASTNUMBER + 1, Month(entity.DATE), Year(entity.DATE))
                Catch ex As Exception
                    oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                    Throw ex
                End Try

                InsertData = True
            Catch ex As Exception
                InsertData = False
                oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                Throw ex
            Finally
                oConnection.db.Dispose()

                oConnection = Nothing
                oError = Nothing
                oCounter = Nothing
            End Try
        End Function
        Public Function UpdateData(ByVal entity As P_PO_H, ByVal entityDetail As List(Of P_PO_D)) As Boolean
            Try
                If Not oConnection.GetConnection() Then
                    UpdateData = False
                    Exit Function
                End If

                sREFERENCE = entity.KDPO
                sSTATUS = "UPDATE"

                Dim ds = oConnection.db.P_PO_Hs.FirstOrDefault(Function(x) x.KDPO = entity.KDPO)

                Try
                    oConnection.db.P_PO_Hs.DeleteOnSubmit(ds)
                    oConnection.db.P_PO_Hs.InsertOnSubmit(entity)
                Catch ex As Exception
                    oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                    Throw ex
                End Try

                Dim dsDetail = oConnection.db.P_PO_Ds.Where(Function(x) x.KDPO = entity.KDPO)

                Try
                    oConnection.db.P_PO_Ds.DeleteAllOnSubmit(dsDetail)
                    oConnection.db.P_PO_Ds.InsertAllOnSubmit(entityDetail)
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
            Finally
                oConnection.db.Dispose()

                oConnection = Nothing
                oError = Nothing
                oCounter = Nothing
            End Try
        End Function
        Public Function DeleteData(ByVal sKDPO As String) As Boolean
            Try
                If Not oConnection.GetConnection() Then
                    DeleteData = False
                    Exit Function
                End If

                sREFERENCE = sKDPO
                sSTATUS = "DELETE"

                Dim ds = oConnection.db.P_PO_Hs.FirstOrDefault(Function(x) x.KDPO = sKDPO)
                Dim dsDetail = oConnection.db.P_PO_Ds.Where(Function(x) x.KDPO = sKDPO)

                Try
                    oConnection.db.P_PO_Hs.DeleteOnSubmit(ds)
                    oConnection.db.P_PO_Ds.DeleteAllOnSubmit(dsDetail)
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
            Finally
                oConnection.db.Dispose()

                oConnection = Nothing
                oError = Nothing
                oCounter = Nothing
            End Try
        End Function
    End Class
End Namespace