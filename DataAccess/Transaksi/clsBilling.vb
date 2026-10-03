Imports System.Threading

Namespace Transaksi
    Public Class clsBilling
        Public oConnection As Setting.clsConnectionMain = Nothing
        Public oError As Setting.clsError = Nothing
        Public sMODUL As String = ""
        Public sREFERENCE As String = ""
        Public sSTATUS As String = ""
        Public sLASTNUMBER As Integer = 0

        Public oCounter As Setting.clsCounter = Nothing

        Public Sub New()
            oConnection = New Setting.clsConnectionMain
            oError = New Setting.clsError
            sMODUL = "T"

            oCounter = New Setting.clsCounter
        End Sub
        Protected Overrides Sub Finalize()
            MyBase.Finalize()
            oConnection = Nothing
            GC.SuppressFinalize(Me)
        End Sub
        Public Function GetStructureHeader() As S_BILLING_H
            If Not oConnection.GetConnection() Then
                GetStructureHeader = Nothing
            End If
            GetStructureHeader = New S_BILLING_H
        End Function
        Public Function GetStructureDetail() As S_BILLING_D
            If Not oConnection.GetConnection() Then
                GetStructureDetail = Nothing
            End If
            GetStructureDetail = New S_BILLING_D
        End Function
        Public Function GetStructureDetailBHP() As S_BILLING_BHP
            If Not oConnection.GetConnection() Then
                GetStructureDetailBHP = Nothing
            End If
            GetStructureDetailBHP = New S_BILLING_BHP
        End Function
        Public Function GetStructureDetailList() As List(Of S_BILLING_D)
            If Not oConnection.GetConnection() Then
                GetStructureDetailList = Nothing
            End If
            GetStructureDetailList = New List(Of S_BILLING_D)
        End Function
        Public Function GetStructureDetailBHPList() As List(Of S_BILLING_BHP)
            If Not oConnection.GetConnection() Then
                GetStructureDetailBHPList = Nothing
            End If
            GetStructureDetailBHPList = New List(Of S_BILLING_BHP)
        End Function
        Public Function GetStructureDetailFarmasi() As S_BILLING_FARMASI
            If Not oConnection.GetConnection() Then
                GetStructureDetailFarmasi = Nothing
            End If
            GetStructureDetailFarmasi = New S_BILLING_FARMASI
        End Function
        Public Function GetStructureDetailFarmasiList() As List(Of S_BILLING_FARMASI)
            If Not oConnection.GetConnection() Then
                GetStructureDetailFarmasiList = Nothing
            End If
            GetStructureDetailFarmasiList = New List(Of S_BILLING_FARMASI)
        End Function
        Public Function GetData() As List(Of S_BILLING_H)
            If Not oConnection.GetConnection() Then
                GetData = Nothing
                Exit Function
            End If
            GetData = oConnection.db.S_BILLING_Hs.OrderByDescending(Function(x) x.KDBILLING).ToList()
        End Function
        Public Function GetData(ByVal Parameter As String) As S_BILLING_H
            If Not oConnection.GetConnection() Then
                GetData = Nothing
                Exit Function
            End If
            GetData = oConnection.db.S_BILLING_Hs.FirstOrDefault(Function(x) x.KDBILLING = Parameter)
        End Function
        Public Function GetDataDetail() As List(Of S_BILLING_D)
            If Not oConnection.GetConnection() Then
                GetDataDetail = Nothing
                Exit Function
            End If
            GetDataDetail = oConnection.db.S_BILLING_Ds.ToList()
        End Function
        Public Function GetDataDetail_BHP() As List(Of S_BILLING_BHP)
            If Not oConnection.GetConnection() Then
                GetDataDetail_BHP = Nothing
                Exit Function
            End If
            GetDataDetail_BHP = oConnection.db.S_BILLING_BHPs.OrderBy(Function(X) X.SEQ).ToList()
        End Function
        Public Function GetDataDetail_BHP(ByVal sKDBILLING As String) As List(Of S_BILLING_BHP)
            If Not oConnection.GetConnection() Then
                GetDataDetail_BHP = Nothing
                Exit Function
            End If
            GetDataDetail_BHP = oConnection.db.S_BILLING_BHPs.Where(Function(X) X.KDBILLING = sKDBILLING).OrderBy(Function(X) X.SEQ).ToList()
        End Function
        Public Function GetDataDetail(ByVal sKDBILLING As String) As List(Of S_BILLING_D)
            If Not oConnection.GetConnection() Then
                GetDataDetail = Nothing
                Exit Function
            End If
            GetDataDetail = oConnection.db.S_BILLING_Ds.Where(Function(x) x.KDBILLING = sKDBILLING).ToList()
        End Function
        Public Function GetDataDetailFarmasi() As List(Of S_BILLING_FARMASI)
            If Not oConnection.GetConnection() Then
                GetDataDetailFarmasi = Nothing
                Exit Function
            End If
            GetDataDetailFarmasi = oConnection.db.S_BILLING_FARMASIs.ToList()
        End Function
        Public Function GetDataDetailFarmasi(ByVal sKDBILLING As String) As List(Of S_BILLING_FARMASI)
            If Not oConnection.GetConnection() Then
                GetDataDetailFarmasi = Nothing
                Exit Function
            End If
            GetDataDetailFarmasi = oConnection.db.S_BILLING_FARMASIs.Where(Function(x) x.KDBILLING = sKDBILLING).OrderBy(Function(X) X.SEQ).ToList()
        End Function
        Public Function GetDataBykdregpenjamin(ByVal Parameter1 As String, ByVal Parameter2 As String) As List(Of S_BILLING_H)
            If Not oConnection.GetConnection() Then
                GetDataBykdregpenjamin = Nothing
                Exit Function
            End If
            GetDataBykdregpenjamin = oConnection.db.S_BILLING_Hs.Where(Function(x) x.KDPENDAFTARAN = Parameter1 And x.KDPENJAMIN = Parameter2 And x.PAYAMOUNT = 0).ToList()
        End Function
        Public Function GetDataByRekamMedis(ByVal Parameter As String) As List(Of S_BILLING_H)
            If Not oConnection.GetConnection() Then
                GetDataByRekamMedis = Nothing
                Exit Function
            End If
            GetDataByRekamMedis = oConnection.db.S_BILLING_Hs.Where(Function(x) x.S_PENDAFTARAN_H.KDCUSTOMER = Parameter And x.S_PENDAFTARAN_H.KDPENDAFTARAN_AWAL = "").ToList()
        End Function
        Public Function GetDataByRekamNama(ByVal Parameter As String) As List(Of S_BILLING_H)
            If Not oConnection.GetConnection() Then
                GetDataByRekamNama = Nothing
                Exit Function
            End If
            GetDataByRekamNama = oConnection.db.S_BILLING_Hs.Where(Function(x) x.S_PENDAFTARAN_H.M_CUSTOMER.NAME_DISPLAY.Contains(Parameter) And x.S_PENDAFTARAN_H.KDPENDAFTARAN_AWAL = "").ToList()
        End Function
        Public Function InsertData(ByVal entity As S_BILLING_H, ByVal entityDetail As List(Of S_BILLING_D), ByVal entityDetailFarmasi As List(Of S_BILLING_FARMASI), ByVal entityDetailBHP As List(Of S_BILLING_BHP)) As Boolean
            Try
                If Not oConnection.GetConnection() Then
                    InsertData = False
                    Exit Function
                End If

                sREFERENCE = entity.KDBILLING
                sSTATUS = "INSERT"

                If entity.CATEGORY = 0 Then
                    sMODUL = "TRJ"
                ElseIf entity.CATEGORY = 1 Then
                    sMODUL = "TRI"
                ElseIf entity.CATEGORY = 2 Then
                    sMODUL = "TLB"
                ElseIf entity.CATEGORY = 3 Then
                    sMODUL = "TRD"
                ElseIf entity.CATEGORY = 4 Then
                    sMODUL = "TFR"
                End If

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

                    entity.KDBILLING = AutoNumber(sMODUL, sLASTNUMBER + 1, entity.DATE)

                    If entityDetail IsNot Nothing Then
                        For Each iLoop In entityDetail
                            iLoop.KDBILLING = entity.KDBILLING
                        Next
                    End If
                    If entityDetailFarmasi IsNot Nothing Then
                        For Each iLoop In entityDetailFarmasi
                            iLoop.KDBILLING = entity.KDBILLING
                        Next
                    End If
                    If entityDetailBHP IsNot Nothing Then
                        For Each iLoop In entityDetailBHP
                            iLoop.KDBILLING = entity.KDBILLING
                        Next
                    End If
                Catch ex As Exception
                    oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                    Throw ex
                End Try
                Try
                    oConnection.db.S_BILLING_Hs.InsertOnSubmit(entity)
                    If entityDetail IsNot Nothing Then
                        oConnection.db.S_BILLING_Ds.InsertAllOnSubmit(entityDetail)
                    End If
                    If entityDetailFarmasi IsNot Nothing Then
                        oConnection.db.S_BILLING_FARMASIs.InsertAllOnSubmit(entityDetailFarmasi)
                    End If
                    If entityDetailBHP IsNot Nothing Then
                        oConnection.db.S_BILLING_BHPs.InsertAllOnSubmit(entityDetailBHP)
                    End If
                Catch ex As Exception
                    oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                    Throw ex
                End Try
                If entityDetailFarmasi IsNot Nothing Then
                    Try
                        For Each iLoop In entityDetailFarmasi
                            Dim oItem As New Reference.clsItem

                            If oItem.GetDataDetail_WAREHOUSE(iLoop.KDITEM, entity.KDWAREHOUSE, iLoop.KDUOM) IsNot Nothing Then
                                Dim dsStock = oConnection.db.M_ITEM_WAREHOUSEs.FirstOrDefault(Function(x) x.KDITEM = iLoop.KDITEM And x.KDWAREHOUSE = entity.KDWAREHOUSE And x.KDUOM = iLoop.KDUOM)

                                dsStock.AMOUNT -= iLoop.QTY

                                oConnection.db.SubmitChanges()
                            Else
                                Dim dsStock As New M_ITEM_WAREHOUSE
                                With dsStock
                                    .DATECREATED = entity.DATECREATED
                                    .DATEUPDATED = entity.DATEUPDATED
                                    .KDWAREHOUSE = entity.KDWAREHOUSE
                                    .KDITEM = iLoop.KDITEM
                                    .KDUOM = iLoop.KDUOM
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
                End If
                If entityDetailBHP IsNot Nothing Then
                    Try
                        For Each iLoop In entityDetailBHP
                            Dim oItem As New Reference.clsItem

                            If oItem.GetDataDetail_WAREHOUSE(iLoop.KDITEM, entity.KDWAREHOUSE, iLoop.KDUOM) IsNot Nothing Then
                                Dim dsStock = oConnection.db.M_ITEM_WAREHOUSEs.FirstOrDefault(Function(x) x.KDITEM = iLoop.KDITEM And x.KDWAREHOUSE = entity.KDWAREHOUSE And x.KDUOM = iLoop.KDUOM)

                                dsStock.AMOUNT -= iLoop.QTY

                                oConnection.db.SubmitChanges()
                            Else
                                Dim dsStock As New M_ITEM_WAREHOUSE
                                With dsStock
                                    .DATECREATED = entity.DATECREATED
                                    .DATEUPDATED = entity.DATEUPDATED
                                    .KDWAREHOUSE = entity.KDWAREHOUSE
                                    .KDITEM = iLoop.KDITEM
                                    .KDUOM = iLoop.KDUOM
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
                End If
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
            End Try
        End Function
        Public Function UpdateData(ByVal entity As S_BILLING_H, ByVal entityDetail As List(Of S_BILLING_D), ByVal entityDetailFarmasi As List(Of S_BILLING_FARMASI), ByVal entityDetailBHP As List(Of S_BILLING_BHP)) As Boolean
            Try
                If Not oConnection.GetConnection() Then
                    UpdateData = False
                    Exit Function
                End If

                sREFERENCE = entity.KDBILLING
                sSTATUS = "UPDATE"

                Dim ds = oConnection.db.S_BILLING_Hs.FirstOrDefault(Function(x) x.KDBILLING = entity.KDBILLING)
                Dim dsDetail = oConnection.db.S_BILLING_Ds.Where(Function(x) x.KDBILLING = entity.KDBILLING)
                Dim dsDetailFarmasi = oConnection.db.S_BILLING_FARMASIs.Where(Function(x) x.KDBILLING = entity.KDBILLING)
                Dim dsDetailBHP = oConnection.db.S_BILLING_BHPs.Where(Function(x) x.KDBILLING = entity.KDBILLING)

                If dsDetailFarmasi IsNot Nothing Then
                    Try
                        For Each iLoop In dsDetailFarmasi

                            Dim oItem As New Reference.clsItem
                            Dim dsStock = oConnection.db.M_ITEM_WAREHOUSEs.FirstOrDefault(Function(x) x.KDITEM = iLoop.KDITEM And x.KDWAREHOUSE = ds.KDWAREHOUSE And x.KDUOM = iLoop.KDUOM)

                            If dsStock IsNot Nothing Then

                                dsStock.AMOUNT += iLoop.QTY

                                oConnection.db.SubmitChanges()
                            Else
                                dsStock = New M_ITEM_WAREHOUSE
                                With dsStock
                                    .KDWAREHOUSE = ds.KDWAREHOUSE
                                    .KDITEM = iLoop.KDITEM
                                    .KDUOM = iLoop.KDUOM
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
                End If

                If dsDetailBHP IsNot Nothing Then
                    Try
                        For Each iLoop In dsDetailBHP

                            Dim oItem As New Reference.clsItem
                            Dim dsStock = oConnection.db.M_ITEM_WAREHOUSEs.FirstOrDefault(Function(x) x.KDITEM = iLoop.KDITEM And x.KDWAREHOUSE = ds.KDWAREHOUSE And x.KDUOM = iLoop.KDUOM)

                            If dsStock IsNot Nothing Then

                                dsStock.AMOUNT += iLoop.QTY

                                oConnection.db.SubmitChanges()
                            Else
                                dsStock = New M_ITEM_WAREHOUSE
                                With dsStock
                                    .KDWAREHOUSE = ds.KDWAREHOUSE
                                    .KDITEM = iLoop.KDITEM
                                    .KDUOM = iLoop.KDUOM
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
                End If

                Try
                    oConnection.db.S_BILLING_Hs.DeleteOnSubmit(ds)
                    oConnection.db.S_BILLING_Hs.InsertOnSubmit(entity)
                    If entityDetail IsNot Nothing Then
                        oConnection.db.S_BILLING_Ds.DeleteAllOnSubmit(dsDetail)
                        oConnection.db.S_BILLING_Ds.InsertAllOnSubmit(entityDetail)
                    End If
                    If entityDetailFarmasi IsNot Nothing Then
                        oConnection.db.S_BILLING_FARMASIs.DeleteAllOnSubmit(dsDetailFarmasi)
                        oConnection.db.S_BILLING_FARMASIs.InsertAllOnSubmit(entityDetailFarmasi)
                    End If
                    If entityDetailBHP IsNot Nothing Then
                        oConnection.db.S_BILLING_BHPs.DeleteAllOnSubmit(dsDetailBHP)
                        oConnection.db.S_BILLING_BHPs.InsertAllOnSubmit(entityDetailBHP)
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

                If entityDetailFarmasi IsNot Nothing Then
                    For Each iLoop In entityDetailFarmasi

                        Dim oItem As New Reference.clsItem
                        Dim dsStock = oConnection.db.M_ITEM_WAREHOUSEs.FirstOrDefault(Function(x) x.KDITEM = iLoop.KDITEM And x.KDWAREHOUSE = entity.KDWAREHOUSE And x.KDUOM = iLoop.KDUOM)

                        If dsStock IsNot Nothing Then

                            dsStock.AMOUNT -= iLoop.QTY

                            oConnection.db.SubmitChanges()
                        Else
                            dsStock = New M_ITEM_WAREHOUSE
                            With dsStock
                                .KDWAREHOUSE = entity.KDWAREHOUSE
                                .KDITEM = iLoop.KDITEM
                                .KDUOM = iLoop.KDUOM
                                .AMOUNT = -iLoop.QTY
                            End With

                            oConnection.db.M_ITEM_WAREHOUSEs.InsertOnSubmit(dsStock)
                            oConnection.db.SubmitChanges()
                        End If
                    Next
                End If
                If entityDetailBHP IsNot Nothing Then
                    For Each iLoop In entityDetailBHP

                        Dim oItem As New Reference.clsItem
                        Dim dsStock = oConnection.db.M_ITEM_WAREHOUSEs.FirstOrDefault(Function(x) x.KDITEM = iLoop.KDITEM And x.KDWAREHOUSE = entity.KDWAREHOUSE And x.KDUOM = iLoop.KDUOM)

                        If dsStock IsNot Nothing Then

                            dsStock.AMOUNT -= iLoop.QTY

                            oConnection.db.SubmitChanges()
                        Else
                            dsStock = New M_ITEM_WAREHOUSE
                            With dsStock
                                .KDWAREHOUSE = entity.KDWAREHOUSE
                                .KDITEM = iLoop.KDITEM
                                .KDUOM = iLoop.KDUOM
                                .AMOUNT = -iLoop.QTY
                            End With

                            oConnection.db.M_ITEM_WAREHOUSEs.InsertOnSubmit(dsStock)
                            oConnection.db.SubmitChanges()
                        End If
                    Next
                End If
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

                Dim ds = oConnection.db.S_BILLING_Hs.FirstOrDefault(Function(x) x.KDBILLING = Parameter)
                Dim dsDetail = oConnection.db.S_BILLING_Ds.Where(Function(x) x.KDBILLING = Parameter)
                Dim dsDetailFarmasi = oConnection.db.S_BILLING_FARMASIs.Where(Function(x) x.KDBILLING = Parameter)
                Dim dsDetaiLBHP = oConnection.db.S_BILLING_BHPs.Where(Function(x) x.KDBILLING = Parameter)

                Try
                    oConnection.db.S_BILLING_Hs.DeleteOnSubmit(ds)
                    If dsDetail IsNot Nothing Then
                        oConnection.db.S_BILLING_Ds.DeleteAllOnSubmit(dsDetail)
                    End If
                    If dsDetailFarmasi IsNot Nothing Then
                        oConnection.db.S_BILLING_FARMASIs.DeleteAllOnSubmit(dsDetailFarmasi)
                        For Each iLoop In dsDetailFarmasi

                            Dim sKDITEM = iLoop.KDITEM
                            Dim sKDUOM = iLoop.KDUOM

                            Dim dsStock = oConnection.db.M_ITEM_WAREHOUSEs.FirstOrDefault(Function(x) x.KDITEM = iLoop.KDITEM And x.KDWAREHOUSE = ds.KDWAREHOUSE And x.KDUOM = iLoop.KDUOM)

                            dsStock.AMOUNT += iLoop.QTY
                        Next
                    End If
                    If dsDetaiLBHP IsNot Nothing Then
                        oConnection.db.S_BILLING_BHPs.DeleteAllOnSubmit(dsDetaiLBHP)
                        For Each iLoop In dsDetaiLBHP

                            Dim sKDITEM = iLoop.KDITEM
                            Dim sKDUOM = iLoop.KDUOM

                            Dim dsStock = oConnection.db.M_ITEM_WAREHOUSEs.FirstOrDefault(Function(x) x.KDITEM = iLoop.KDITEM And x.KDWAREHOUSE = ds.KDWAREHOUSE And x.KDUOM = iLoop.KDUOM)

                            dsStock.AMOUNT += iLoop.QTY
                        Next
                    End If
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
        Public Function Daftar_Warehouse_Default() As String
            Try
                If Not oConnection.GetConnection() Then
                    Daftar_Warehouse_Default = String.Empty
                    Exit Function
                End If

                Dim ds = oConnection.db.M_WAREHOUSEs.FirstOrDefault(Function(x) x.ISDEFAULT = True)
                If ds IsNot Nothing Then
                    Daftar_Warehouse_Default = ds.KDWAREHOUSE
                Else
                    Daftar_Warehouse_Default = String.Empty
                End If
            Catch ex As Exception
                Daftar_Warehouse_Default = String.Empty
                Throw ex
            End Try
        End Function
    End Class
End Namespace