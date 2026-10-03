Imports System.Threading

Namespace Purchasing
    Public Class clsPurchaseInvoice
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

            sMODUL = "PI"
        End Sub
        Protected Overrides Sub Finalize()
            MyBase.Finalize()
            oConnection = Nothing
            GC.SuppressFinalize(Me)
        End Sub
        Public Function GetStructureHeader() As P_PI_H
            If Not oConnection.GetConnection() Then
                GetStructureHeader = Nothing
            End If
            GetStructureHeader = New P_PI_H
        End Function
        Public Function GetStructureDetail() As P_PI_D
            If Not oConnection.GetConnection() Then
                GetStructureDetail = Nothing
            End If
            GetStructureDetail = New P_PI_D
        End Function
        Public Function GetStructureDetailList() As List(Of P_PI_D)
            If Not oConnection.GetConnection() Then
                GetStructureDetailList = Nothing
            End If
            GetStructureDetailList = New List(Of P_PI_D)
        End Function
        Public Function GetData() As List(Of P_PI_H)
            If Not oConnection.GetConnection() Then
                GetData = Nothing
                Exit Function
            End If
            GetData = oConnection.db.P_PI_Hs.OrderByDescending(Function(x) x.KDPI).ToList()
        End Function
        Public Function GetData(ByVal sKDPI As String, Optional ByVal isTAX As Boolean = False) As P_PI_H
            If Not oConnection.GetConnection() Then
                GetData = Nothing
                Exit Function
            End If
            If isTAX = False Then
                GetData = oConnection.db.P_PI_Hs.FirstOrDefault(Function(x) x.KDPI = sKDPI)
            Else
                GetData = oConnection.db.P_PI_Hs.FirstOrDefault(Function(x) x.MEMO = sKDPI)
            End If
        End Function
        Public Function GetDataDetail() As List(Of P_PI_D)
            If Not oConnection.GetConnection() Then
                GetDataDetail = Nothing
                Exit Function
            End If
            GetDataDetail = oConnection.db.P_PI_Ds.ToList()
        End Function
        Public Function GetDataDetail(ByVal sKDPI As String) As List(Of P_PI_D)
            If Not oConnection.GetConnection() Then
                GetDataDetail = Nothing
                Exit Function
            End If
            GetDataDetail = oConnection.db.P_PI_Ds.Where(Function(x) x.KDPI = sKDPI).ToList()
        End Function
        Public Function InsertData(ByVal entity As P_PI_H, ByVal entityDetail As List(Of P_PI_D)) As Boolean
            Try
                If Not oConnection.GetConnection() Then
                    InsertData = False
                    Exit Function
                End If

                sREFERENCE = entity.KDPI
                sSTATUS = "INSERT"

                Try
                    oConnection.db.P_PI_Hs.InsertOnSubmit(entity)
                    oConnection.db.P_PI_Ds.InsertAllOnSubmit(entityDetail)
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
                        Dim sKdItem = iLoop.KDITEM
                        Dim dsItem = oConnection.db.M_ITEM_UOMs.FirstOrDefault(Function(x) x.KDITEM = iLoop.KDITEM And x.KDUOM = iLoop.KDUOM)

                        If iLoop.ISUPDATEHARGA = True Then

                            dsItem.PRICEPURCHASESTANDARD = iLoop.GRANDTOTAL / iLoop.QTY
                            dsItem.PRICESALESSTANDARD = iLoop.SUBPRICE + ((iLoop.SUBPRICE / 100) * dsItem.MARGIN)

                            If dsItem.RATE <> 1 Then
                                Dim dsRATE = oConnection.db.M_ITEM_UOMs.FirstOrDefault(Function(x) x.KDITEM = iLoop.KDITEM And x.RATE = 1)
                                dsRATE.PRICEPURCHASESTANDARD = iLoop.PRICE / dsItem.RATE
                            End If

                        End If
                    Next
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
                            dsStock.AMOUNT += iLoop.QTY

                            oConnection.db.SubmitChanges()
                        Else
                            Dim dsStock As New M_ITEM_WAREHOUSE
                            With dsStock
                                .DATECREATED = entity.DATECREATED
                                .DATEUPDATED = entity.DATEUPDATED
                                .KDWAREHOUSE = entity.KDWAREHOUSE
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
                    For Each iLoop In entityDetail
                        UpdateExpire(iLoop.KDITEM, iLoop.DATE_EXPIRED)
                    Next
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
        Public Function UpdateData(ByVal entity As P_PI_H, ByVal entityDetail As List(Of P_PI_D)) As Boolean
            Try
                If Not oConnection.GetConnection() Then
                    UpdateData = False
                    Exit Function
                End If

                sREFERENCE = entity.KDPI
                sSTATUS = "UPDATE"

                Dim ds = oConnection.db.P_PI_Hs.FirstOrDefault(Function(x) x.KDPI = entity.KDPI)

                Try
                    oConnection.db.P_PI_Hs.DeleteOnSubmit(ds)
                    oConnection.db.P_PI_Hs.InsertOnSubmit(entity)
                Catch ex As Exception
                    oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                    Throw ex
                End Try

                Dim dsDetail = oConnection.db.P_PI_Ds.Where(Function(x) x.KDPI = entity.KDPI)

                Try
                    For Each iLoop In dsDetail
                        Dim sKdItem = iLoop.KDITEM
                        Dim dsItem = oConnection.db.M_ITEM_UOMs.FirstOrDefault(Function(x) x.KDITEM = iLoop.KDITEM And x.KDUOM = iLoop.KDUOM)

                        dsItem.PRICEPURCHASESTANDARD = iLoop.SUBPRICE
                        dsItem.PRICESALESSTANDARD = iLoop.SUBPRICE + ((iLoop.SUBPRICE / 100) * dsItem.MARGIN)
                    Next
                Catch ex As Exception
                    oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                    Throw ex
                End Try

                Try
                    For Each iLoop In dsDetail
                        Dim sKDITEM = iLoop.KDITEM
                        Dim sKDUOM = iLoop.KDUOM

                        If oItem.GetDataDetail_WAREHOUSE(sKDITEM, ds.KDWAREHOUSE, sKDUOM) IsNot Nothing Then
                            Dim dsStock = oConnection.db.M_ITEM_WAREHOUSEs.FirstOrDefault(Function(x) x.KDITEM = sKDITEM And x.KDWAREHOUSE = ds.KDWAREHOUSE And x.KDUOM = sKDUOM)
                            dsStock.AMOUNT -= iLoop.QTY

                            oConnection.db.SubmitChanges()
                        Else
                            Dim dsStock As New M_ITEM_WAREHOUSE
                            With dsStock
                                .DATECREATED = entity.DATECREATED
                                .DATEUPDATED = entity.DATEUPDATED
                                .KDWAREHOUSE = ds.KDWAREHOUSE
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
                    For Each iLoop In dsDetail
                        sKDITEM.Add(iLoop.KDITEM)
                    Next
                Catch ex As Exception
                    oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                    Throw ex
                End Try
                Try
                    oConnection.db.P_PI_Ds.DeleteAllOnSubmit(dsDetail)
                    oConnection.db.P_PI_Ds.InsertAllOnSubmit(entityDetail)
                Catch ex As Exception
                    oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                    Throw ex
                End Try
                Try
                    For Each iLoop In entityDetail
                        Dim sKdItem = iLoop.KDITEM
                        Dim dsItem = oConnection.db.M_ITEM_UOMs.FirstOrDefault(Function(x) x.KDITEM = iLoop.KDITEM And x.KDUOM = iLoop.KDUOM)

                        dsItem.PRICEPURCHASESTANDARD = iLoop.SUBPRICE
                        dsItem.PRICESALESSTANDARD = iLoop.SUBPRICE + ((iLoop.SUBPRICE / 100) * dsItem.MARGIN)
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
                Try
                    For Each iLoop In entityDetail
                        Dim sKDITEM = iLoop.KDITEM
                        Dim sKDUOM = iLoop.KDUOM

                        If oItem.GetDataDetail_WAREHOUSE(sKDITEM, entity.KDWAREHOUSE, sKDUOM) IsNot Nothing Then
                            Dim dsStock = oConnection.db.M_ITEM_WAREHOUSEs.FirstOrDefault(Function(x) x.KDITEM = sKDITEM And x.KDWAREHOUSE = entity.KDWAREHOUSE And x.KDUOM = sKDUOM)
                            dsStock.AMOUNT += iLoop.QTY

                            oConnection.db.SubmitChanges()
                        Else
                            Dim dsStock As New M_ITEM_WAREHOUSE
                            With dsStock
                                .DATECREATED = entity.DATECREATED
                                .DATEUPDATED = entity.DATEUPDATED
                                .KDWAREHOUSE = entity.KDWAREHOUSE
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
                    For Each iLoop In entityDetail
                        UpdateExpire(iLoop.KDITEM, iLoop.DATE_EXPIRED)
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
        Public Function DeleteData(ByVal sKDPI As String) As Boolean
            Try
                If Not oConnection.GetConnection() Then
                    DeleteData = False
                    Exit Function
                End If

                sREFERENCE = sKDPI
                sSTATUS = "DELETE"

                Dim ds = oConnection.db.P_PI_Hs.FirstOrDefault(Function(x) x.KDPI = sKDPI)
                Dim dsDetail = oConnection.db.P_PI_Ds.Where(Function(x) x.KDPI = sKDPI)

                Try
                    oConnection.db.P_PI_Hs.DeleteOnSubmit(ds)
                    oConnection.db.P_PI_Ds.DeleteAllOnSubmit(dsDetail)
                Catch ex As Exception
                    oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                    Throw ex
                End Try
                Try
                    For Each iLoop In dsDetail
                        Dim sKDITEM = iLoop.KDITEM
                        Dim sKDUOM = iLoop.KDUOM

                        Dim dsStock = oConnection.db.M_ITEM_WAREHOUSEs.FirstOrDefault(Function(x) x.KDITEM = sKDITEM And x.KDWAREHOUSE = ds.KDWAREHOUSE And x.KDUOM = sKDUOM)

                        dsStock.AMOUNT -= iLoop.QTY
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
        Public Function UpdateHargaJualBeli(ByVal KDITEM As String, KDUOM As String, HARGABELI As Decimal, HARGAJUAL As Decimal) As Boolean
            Try
                If Not oConnection.GetConnection Then
                    UpdateHargaJualBeli = False

                    Exit Function
                End If

                Dim ds = oConnection.db.M_ITEM_UOMs.FirstOrDefault(Function(x) x.KDITEM = KDITEM And x.KDUOM = KDUOM And x.RATE = 1)

                If ds IsNot Nothing Then
                    Try
                        ds.PRICEPURCHASESTANDARD = HARGABELI
                        ds.PRICESALESSTANDARD = HARGAJUAL
                        oConnection.db.SubmitChanges()
                    Catch ex As Exception
                        MsgBox(ex)
                    End Try
                End If

                UpdateHargaJualBeli = True
            Catch ex As Exception
                UpdateHargaJualBeli = False
                MsgBox(ex)
            Finally
                oConnection.db.Dispose()
            End Try
        End Function
        Public Function UpdateExpire(ByVal KDITEM As String, ByVal DATE_EXPIRE As DateTime) As Boolean
            Try
                If Not oConnection.GetConnection Then
                    UpdateExpire = False

                    Exit Function
                End If

                Dim ds = oConnection.db.M_ITEMs.FirstOrDefault(Function(x) x.KDITEM = KDITEM)

                If ds IsNot Nothing Then
                    Try
                        ds.DATE_EXPIRE = DATE_EXPIRE
                        oConnection.db.SubmitChanges()
                    Catch ex As Exception
                        MsgBox(ex)
                    End Try
                End If

                UpdateExpire = True
            Catch ex As Exception
                UpdateExpire = False
                MsgBox(ex)
            Finally
                oConnection.db.Dispose()
            End Try
        End Function
    End Class
End Namespace