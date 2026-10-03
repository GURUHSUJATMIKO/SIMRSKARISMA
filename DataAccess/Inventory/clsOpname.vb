Imports System.Threading

Namespace Inventory
    Public Class clsOpname
        Public oConnection As Setting.clsConnectionMain = Nothing
        Public oError As Setting.clsError = Nothing
        Public sMODUL As String = ""
        Public sREFERENCE As String = ""
        Public sSTATUS As String = ""
        Public sLASTNUMBER As Integer = 0

        Public oCounter As Setting.clsCounter = Nothing

        Public sKDITEM As New List(Of String)

        Public Sub New()
            oConnection = New Setting.clsConnectionMain
            oError = New Setting.clsError
            sMODUL = "OPM"

            oCounter = New Setting.clsCounter
        End Sub
        Protected Overrides Sub Finalize()
            MyBase.Finalize()
            oConnection = Nothing
            GC.SuppressFinalize(Me)
        End Sub
        Public Function GetStructureHeader() As I_OPNAME_H
            If Not oConnection.GetConnection() Then
                GetStructureHeader = Nothing
            End If
            GetStructureHeader = New I_OPNAME_H
        End Function
        Public Function GetStructureDetail() As I_OPNAME_D
            If Not oConnection.GetConnection() Then
                GetStructureDetail = Nothing
            End If
            GetStructureDetail = New I_OPNAME_D
        End Function
        Public Function GetStructureDetailList() As List(Of I_OPNAME_D)
            If Not oConnection.GetConnection() Then
                GetStructureDetailList = Nothing
            End If
            GetStructureDetailList = New List(Of I_OPNAME_D)
        End Function
        Public Function GetData() As List(Of I_OPNAME_H)
            If Not oConnection.GetConnection() Then
                GetData = Nothing
                Exit Function
            End If
            GetData = oConnection.db.I_OPNAME_Hs.OrderByDescending(Function(x) x.KDOPNAME).ToList()
        End Function
        Public Function GetData(ByVal sKDOPNAME As String) As I_OPNAME_H
            If Not oConnection.GetConnection() Then
                GetData = Nothing
                Exit Function
            End If
            GetData = oConnection.db.I_OPNAME_Hs.FirstOrDefault(Function(x) x.KDOPNAME = sKDOPNAME)
        End Function
        Public Function GetDataDetail() As List(Of I_OPNAME_D)
            If Not oConnection.GetConnection() Then
                GetDataDetail = Nothing
                Exit Function
            End If
            GetDataDetail = oConnection.db.I_OPNAME_Ds.ToList()
        End Function
        Public Function GetDataDetail(ByVal sKDOPNAME As String) As List(Of I_OPNAME_D)
            If Not oConnection.GetConnection() Then
                GetDataDetail = Nothing
                Exit Function
            End If
            GetDataDetail = oConnection.db.I_OPNAME_Ds.Where(Function(x) x.KDOPNAME = sKDOPNAME).ToList()
        End Function
        Public Function InsertData(ByVal entity As I_OPNAME_H, ByVal entityDetail As List(Of I_OPNAME_D)) As Boolean
            Try
                If Not oConnection.GetConnection() Then
                    InsertData = False
                    Exit Function
                End If

                sREFERENCE = entity.KDOPNAME
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

                    entity.KDOPNAME = AutoNumber(sMODUL, sLASTNUMBER + 1, entity.DATE)
                    For Each iLoop In entityDetail
                        iLoop.KDOPNAME = entity.KDOPNAME
                    Next
                Catch ex As Exception
                    oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                    Throw ex
                End Try

                Try
                    oConnection.db.I_OPNAME_Hs.InsertOnSubmit(entity)
                    oConnection.db.I_OPNAME_Ds.InsertAllOnSubmit(entityDetail)
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
                        Dim oItem As New Reference.clsItem
                        Dim sKDITEM = iLoop.KDITEM
                        Dim sKDUOM = iLoop.KDUOM

                        If oItem.GetDataDetail_WAREHOUSE(sKDITEM, entity.KDWAREHOUSE, sKDUOM) IsNot Nothing Then
                            Dim dsStock = oConnection.db.M_ITEM_WAREHOUSEs.FirstOrDefault(Function(x) x.KDITEM = sKDITEM And x.KDWAREHOUSE = entity.KDWAREHOUSE And x.KDUOM = sKDUOM)
                            If iLoop.QTY > 0 Then
                                dsStock.AMOUNT += Math.Abs(iLoop.QTY)
                            Else
                                dsStock.AMOUNT -= Math.Abs(iLoop.QTY)
                            End If


                            oConnection.db.SubmitChanges()
                        Else
                            Dim dsStock As New M_ITEM_WAREHOUSE
                            With dsStock
                                .DATECREATED = entity.DATECREATED
                                .DATEUPDATED = entity.DATEUPDATED
                                .KDWAREHOUSE = entity.KDWAREHOUSE
                                .KDITEM = sKDITEM
                                .KDUOM = sKDUOM
                                If iLoop.QTY > 0 Then
                                    .AMOUNT = Math.Abs(iLoop.QTY)
                                Else
                                    .AMOUNT = -Math.Abs(iLoop.QTY)
                                End If
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
            End Try
        End Function
        Public Function UpdateData(ByVal entity As I_OPNAME_H, ByVal entityDetail As List(Of I_OPNAME_D)) As Boolean
            Try
                If Not oConnection.GetConnection() Then
                    UpdateData = False
                    Exit Function
                End If

                sREFERENCE = entity.KDOPNAME
                sSTATUS = "UPDATE"

                Dim ds = oConnection.db.I_OPNAME_Hs.FirstOrDefault(Function(x) x.KDOPNAME = entity.KDOPNAME)

                Try
                    oConnection.db.I_OPNAME_Hs.DeleteOnSubmit(ds)
                    oConnection.db.I_OPNAME_Hs.InsertOnSubmit(entity)
                Catch ex As Exception
                    oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                    Throw ex
                End Try

                Dim dsDetail = oConnection.db.I_OPNAME_Ds.Where(Function(x) x.KDOPNAME = entity.KDOPNAME)

                Try
                    For Each iLoop In dsDetail
                        Dim oItem As New Reference.clsItem
                        Dim sKDITEM = iLoop.KDITEM
                        Dim sKDUOM = iLoop.KDUOM

                        If oItem.GetDataDetail_WAREHOUSE(sKDITEM, ds.KDWAREHOUSE, sKDUOM) IsNot Nothing Then
                            Dim dsStock = oConnection.db.M_ITEM_WAREHOUSEs.FirstOrDefault(Function(x) x.KDITEM = sKDITEM And x.KDWAREHOUSE = ds.KDWAREHOUSE And x.KDUOM = sKDUOM)
                            If iLoop.QTY > 0 Then
                                dsStock.AMOUNT -= Math.Abs(iLoop.QTY)
                            Else
                                dsStock.AMOUNT += Math.Abs(iLoop.QTY)
                            End If


                            oConnection.db.SubmitChanges()
                        Else
                            Dim dsStock As New M_ITEM_WAREHOUSE
                            With dsStock
                                .DATECREATED = entity.DATECREATED
                                .DATEUPDATED = entity.DATEUPDATED
                                .KDWAREHOUSE = ds.KDWAREHOUSE
                                .KDITEM = sKDITEM
                                .KDUOM = sKDUOM
                                If iLoop.QTY > 0 Then
                                    .AMOUNT = -Math.Abs(iLoop.QTY)
                                Else
                                    .AMOUNT = Math.Abs(iLoop.QTY)
                                End If

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
                    oConnection.db.I_OPNAME_Ds.DeleteAllOnSubmit(dsDetail)
                    oConnection.db.I_OPNAME_Ds.InsertAllOnSubmit(entityDetail)
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
                        Dim oItem As New Reference.clsItem
                        Dim sKDITEM = iLoop.KDITEM
                        Dim sKDUOM = iLoop.KDUOM

                        If oItem.GetDataDetail_WAREHOUSE(sKDITEM, entity.KDWAREHOUSE, sKDUOM) IsNot Nothing Then
                            Dim dsStock = oConnection.db.M_ITEM_WAREHOUSEs.FirstOrDefault(Function(x) x.KDITEM = sKDITEM And x.KDWAREHOUSE = entity.KDWAREHOUSE And x.KDUOM = sKDUOM)

                            If iLoop.QTY > 0 Then
                                dsStock.AMOUNT += Math.Abs(iLoop.QTY)
                            Else
                                dsStock.AMOUNT -= Math.Abs(iLoop.QTY)
                            End If


                            oConnection.db.SubmitChanges()
                        Else
                            Dim dsStock As New M_ITEM_WAREHOUSE
                            With dsStock
                                .DATECREATED = entity.DATECREATED
                                .DATEUPDATED = entity.DATEUPDATED
                                .KDWAREHOUSE = entity.KDWAREHOUSE
                                .KDITEM = sKDITEM
                                .KDUOM = sKDUOM
                                If iLoop.QTY > 0 Then
                                    .AMOUNT = Math.Abs(iLoop.QTY)
                                Else
                                    .AMOUNT = -Math.Abs(iLoop.QTY)
                                End If
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
            End Try
        End Function
        Public Function DeleteData(ByVal sKDOPNAME As String) As Boolean
            Try
                If Not oConnection.GetConnection() Then
                    DeleteData = False
                    Exit Function
                End If

                sREFERENCE = sKDOPNAME
                sSTATUS = "DELETE"

                Dim ds = oConnection.db.I_OPNAME_Hs.FirstOrDefault(Function(x) x.KDOPNAME = sKDOPNAME)
                Dim dsDetail = oConnection.db.I_OPNAME_Ds.Where(Function(x) x.KDOPNAME = sKDOPNAME)

                Try
                    oConnection.db.I_OPNAME_Hs.DeleteOnSubmit(ds)
                    oConnection.db.I_OPNAME_Ds.DeleteAllOnSubmit(dsDetail)

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

                        If iLoop.QTY > 0 Then
                            dsStock.AMOUNT -= Math.Abs(iLoop.QTY)
                        Else
                            dsStock.AMOUNT += Math.Abs(iLoop.QTY)
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