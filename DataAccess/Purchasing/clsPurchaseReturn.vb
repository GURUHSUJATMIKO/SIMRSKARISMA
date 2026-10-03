Imports System.Threading

Namespace Purchasing
    Public Class clsPurchaseReturn
        Public oConnection As Setting.clsConnectionMain = Nothing
        Public oError As Setting.clsError = Nothing
        Public oItem As Reference.clsItem = Nothing
        Public oVendor As Reference.clsVendor = Nothing
        Public oCounter As Setting.clsCounter = Nothing

        Public sMODUL As String = ""
        Public sREFERENCE As String = ""
        Public sSTATUS As String = ""
        Public sLASTNUMBER As Integer = 0
        Public sKDITEM As New List(Of String)

        Public Sub New(Optional ByVal sConnection As String = "")
            If sConnection = "" Then
                oConnection = New Setting.clsConnectionMain
                oError = New Setting.clsError
                oItem = New Reference.clsItem
                oVendor = New Reference.clsVendor
                oCounter = New Setting.clsCounter
            Else
                oConnection = New Setting.clsConnectionMain("TAX")
                oError = New Setting.clsError("TAX")
                oItem = New Reference.clsItem("TAX")
                oVendor = New Reference.clsVendor("TAX")
                oCounter = New Setting.clsCounter("TAX")
            End If

            sMODUL = "PR"
        End Sub
        Protected Overrides Sub Finalize()
            MyBase.Finalize()
            oConnection = Nothing
            GC.SuppressFinalize(Me)
        End Sub
        Public Function GetStructureHeader() As P_PR_H
            If Not oConnection.GetConnection() Then
                GetStructureHeader = Nothing
            End If
            GetStructureHeader = New P_PR_H
        End Function
        Public Function GetStructureDetail() As P_PR_D
            If Not oConnection.GetConnection() Then
                GetStructureDetail = Nothing
            End If
            GetStructureDetail = New P_PR_D
        End Function
        Public Function GetStructureDetailList() As List(Of P_PR_D)
            If Not oConnection.GetConnection() Then
                GetStructureDetailList = Nothing
            End If
            GetStructureDetailList = New List(Of P_PR_D)
        End Function
        Public Function GetData() As List(Of P_PR_H)
            If Not oConnection.GetConnection() Then
                GetData = Nothing
                Exit Function
            End If
            GetData = oConnection.db.P_PR_Hs.OrderByDescending(Function(x) x.KDPR).ToList()
        End Function
        Public Function GetData(ByVal sKDPR As String, Optional ByVal isTAX As Boolean = False) As P_PR_H
            If Not oConnection.GetConnection() Then
                GetData = Nothing
                Exit Function
            End If
            If isTAX = False Then
                GetData = oConnection.db.P_PR_Hs.FirstOrDefault(Function(x) x.KDPR = sKDPR)
            Else
                GetData = oConnection.db.P_PR_Hs.FirstOrDefault(Function(x) x.MEMO = sKDPR)
            End If
        End Function
        Public Function GetDataDetail() As List(Of P_PR_D)
            If Not oConnection.GetConnection() Then
                GetDataDetail = Nothing
                Exit Function
            End If
            GetDataDetail = oConnection.db.P_PR_Ds.ToList()
        End Function
        Public Function GetDataDetail(ByVal sKDPR As String) As List(Of P_PR_D)
            If Not oConnection.GetConnection() Then
                GetDataDetail = Nothing
                Exit Function
            End If
            GetDataDetail = oConnection.db.P_PR_Ds.Where(Function(x) x.KDPR = sKDPR).ToList()
        End Function
        Public Function InsertData(ByVal entity As P_PR_H, ByVal entityDetail As List(Of P_PR_D)) As Boolean
            Try
                If Not oConnection.GetConnection() Then
                    InsertData = False
                    Exit Function
                End If

                sREFERENCE = entity.KDPR
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

                    entity.KDPR = AutoNumber(sMODUL, sLASTNUMBER + 1, entity.DATE)
                    For Each iLoop In entityDetail
                        iLoop.KDPR = entity.KDPR
                    Next
                Catch ex As Exception
                    oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                    Throw ex
                End Try

                Try
                    oConnection.db.P_PR_Hs.InsertOnSubmit(entity)
                    oConnection.db.P_PR_Ds.InsertAllOnSubmit(entityDetail)
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
                    For Each iLoop In entityDetail
                        Dim sKDITEM = iLoop.KDITEM
                        Dim sKDUOM = iLoop.KDUOM

                        If oItem.GetDataDetail_WAREHOUSE(sKDITEM, entity.KDWAREHOUSE, sKDUOM) IsNot Nothing Then
                            Dim dsStock = oConnection.db.M_ITEM_WAREHOUSEs.FirstOrDefault(Function(x) x.KDITEM = sKDITEM And x.KDWAREHOUSE = entity.KDWAREHOUSE And x.KDUOM = sKDUOM)
                            dsStock.AMOUNT -= iLoop.QTY

                            oConnection.db.SubmitChanges()
                        Else
                            Dim dsStock As New M_ITEM_WAREHOUSE
                            With dsStock
                                .DATECREATED = entity.DATECREATED
                                .DATEUPDATED = entity.DATEUPDATED
                                .KDWAREHOUSE = entity.KDWAREHOUSE
                                .KDITEM = sKDITEM
                                .KDUOM = sKDUOM
                                .AMOUNT = -iLoop.QTY
                            End With

                            oConnection.db.M_ITEM_WAREHOUSEs.InsertOnSubmit(dsStock)
                            oConnection.db.SubmitChanges()
                        End If
                    Next
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
            End Try
        End Function
        Public Function UpdateData(ByVal entity As P_PR_H, ByVal entityDetail As List(Of P_PR_D)) As Boolean
            Try
                If Not oConnection.GetConnection() Then
                    UpdateData = False
                    Exit Function
                End If

                sREFERENCE = entity.KDPR
                sSTATUS = "UPDATE"

                Dim ds = oConnection.db.P_PR_Hs.FirstOrDefault(Function(x) x.KDPR = entity.KDPR)

                Try
                    oConnection.db.P_PR_Hs.DeleteOnSubmit(ds)
                    oConnection.db.P_PR_Hs.InsertOnSubmit(entity)
                Catch ex As Exception
                    oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                    Throw ex
                End Try

                Dim dsDetail = oConnection.db.P_PR_Ds.Where(Function(x) x.KDPR = entity.KDPR)

                Try
                    For Each iLoop In dsDetail
                        Dim sKDITEM = iLoop.KDITEM
                        Dim sKDUOM = iLoop.KDUOM

                        If oItem.GetDataDetail_WAREHOUSE(sKDITEM, ds.KDWAREHOUSE, sKDUOM) IsNot Nothing Then
                            Dim dsStock = oConnection.db.M_ITEM_WAREHOUSEs.FirstOrDefault(Function(x) x.KDITEM = sKDITEM And x.KDWAREHOUSE = ds.KDWAREHOUSE And x.KDUOM = sKDUOM)
                            dsStock.AMOUNT += iLoop.QTY

                            oConnection.db.SubmitChanges()
                        Else
                            Dim dsStock As New M_ITEM_WAREHOUSE
                            With dsStock
                                .DATECREATED = entity.DATECREATED
                                .DATEUPDATED = entity.DATEUPDATED
                                .KDWAREHOUSE = ds.KDWAREHOUSE
                                .KDITEM = sKDITEM
                                .KDUOM = sKDUOM
                                .AMOUNT = iLoop.QTY
                            End With

                            oConnection.db.M_ITEM_WAREHOUSEs.InsertOnSubmit(dsStock)
                            oConnection.db.SubmitChanges()
                        End If
                    Next
                Catch ex As Exception
                    oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                    Throw ex
                End Try
                Try
                    For Each iLoop In dsDetail
                        sKDITEM.Add(iLoop.KDITEM)
                    Next
                Catch ex As Exception
                    oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                    Throw ex
                End Try
                Try
                    oConnection.db.P_PR_Ds.DeleteAllOnSubmit(dsDetail)
                    oConnection.db.P_PR_Ds.InsertAllOnSubmit(entityDetail)
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
                    For Each iLoop In entityDetail
                        Dim sKDITEM = iLoop.KDITEM
                        Dim sKDUOM = iLoop.KDUOM

                        If oItem.GetDataDetail_WAREHOUSE(sKDITEM, entity.KDWAREHOUSE, sKDUOM) IsNot Nothing Then
                            Dim dsStock = oConnection.db.M_ITEM_WAREHOUSEs.FirstOrDefault(Function(x) x.KDITEM = sKDITEM And x.KDWAREHOUSE = entity.KDWAREHOUSE And x.KDUOM = sKDUOM)
                            dsStock.AMOUNT -= iLoop.QTY

                            oConnection.db.SubmitChanges()
                        Else
                            Dim dsStock As New M_ITEM_WAREHOUSE
                            With dsStock
                                .DATECREATED = entity.DATECREATED
                                .DATEUPDATED = entity.DATEUPDATED
                                .KDWAREHOUSE = entity.KDWAREHOUSE
                                .KDITEM = sKDITEM
                                .KDUOM = sKDUOM
                                .AMOUNT = -iLoop.QTY
                            End With

                            oConnection.db.M_ITEM_WAREHOUSEs.InsertOnSubmit(dsStock)
                            oConnection.db.SubmitChanges()
                        End If
                    Next
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
            End Try
        End Function
        Public Function DeleteData(ByVal sKDPR As String) As Boolean
            Try
                If Not oConnection.GetConnection() Then
                    DeleteData = False
                    Exit Function
                End If

                sREFERENCE = sKDPR
                sSTATUS = "DELETE"

                Dim ds = oConnection.db.P_PR_Hs.FirstOrDefault(Function(x) x.KDPR = sKDPR)
                Dim dsDetail = oConnection.db.P_PR_Ds.Where(Function(x) x.KDPR = sKDPR)

                Try
                    oConnection.db.P_PR_Hs.DeleteOnSubmit(ds)
                    oConnection.db.P_PR_Ds.DeleteAllOnSubmit(dsDetail)

                    'ds.ISDELETE = True
                Catch ex As Exception
                    oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                    Throw ex
                End Try
                Try
                    For Each iLoop In dsDetail
                        Dim sKDITEM = iLoop.KDITEM
                        Dim sKDUOM = iLoop.KDUOM

                        Dim dsStock = oConnection.db.M_ITEM_WAREHOUSEs.FirstOrDefault(Function(x) x.KDITEM = sKDITEM And x.KDWAREHOUSE = ds.KDWAREHOUSE And x.KDUOM = sKDUOM)

                        dsStock.AMOUNT += iLoop.QTY
                    Next
                Catch ex As Exception
                    oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                    Throw ex
                End Try
                Try
                    For Each iLoop In dsDetail
                        sKDITEM.Add(iLoop.KDITEM)
                    Next
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
            End Try
        End Function
    End Class
End Namespace