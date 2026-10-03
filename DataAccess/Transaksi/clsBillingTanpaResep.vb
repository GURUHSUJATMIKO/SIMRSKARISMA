Imports System.Threading

Namespace Transaksi
    Public Class clsBillingTanpaResep
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
            sMODUL = "STR"

            oCounter = New Setting.clsCounter
        End Sub
        Protected Overrides Sub Finalize()
            MyBase.Finalize()
            oConnection = Nothing
            GC.SuppressFinalize(Me)
        End Sub
        Public Function GetStructureHeader() As S_BILLING_TANPARESEP_H
            If Not oConnection.GetConnection() Then
                GetStructureHeader = Nothing
            End If
            GetStructureHeader = New S_BILLING_TANPARESEP_H
        End Function
        Public Function GetStructureDetail() As S_BILLING_TANPARESEP_D
            If Not oConnection.GetConnection() Then
                GetStructureDetail = Nothing
            End If
            GetStructureDetail = New S_BILLING_TANPARESEP_D
        End Function
        Public Function GetStructureDetailList() As List(Of S_BILLING_TANPARESEP_D)
            If Not oConnection.GetConnection() Then
                GetStructureDetailList = Nothing
            End If
            GetStructureDetailList = New List(Of S_BILLING_TANPARESEP_D)
        End Function
        Public Function GetData() As List(Of S_BILLING_TANPARESEP_H)
            If Not oConnection.GetConnection() Then
                GetData = Nothing
                Exit Function
            End If
            GetData = oConnection.db.S_BILLING_TANPARESEP_Hs.OrderByDescending(Function(x) x.KDBILLING_TANPARESEP).ToList()
        End Function
        Public Function GetData(ByVal Parameter As String) As S_BILLING_TANPARESEP_H
            If Not oConnection.GetConnection() Then
                GetData = Nothing
                Exit Function
            End If
            GetData = oConnection.db.S_BILLING_TANPARESEP_Hs.FirstOrDefault(Function(x) x.KDBILLING_TANPARESEP = Parameter)
        End Function
        Public Function GetDataBYKode(ByVal sKDBILLING_TANPARESEP As String) As List(Of S_BILLING_TANPARESEP_H)
            If Not oConnection.GetConnection() Then
                GetDataBYKode = Nothing
                Exit Function
            End If
            GetDataBYKode = oConnection.db.S_BILLING_TANPARESEP_Hs.Where(Function(x) x.KDBILLING_TANPARESEP.Contains(sKDBILLING_TANPARESEP)).ToList()
        End Function
        Public Function GetDataBYRM(ByVal sKDCUSTOMER_UNIT As String) As List(Of S_BILLING_TANPARESEP_H)
            If Not oConnection.GetConnection() Then
                GetDataBYRM = Nothing
                Exit Function
            End If
            GetDataBYRM = oConnection.db.S_BILLING_TANPARESEP_Hs.Where(Function(x) x.KDCUSTOMER_UNIT = sKDCUSTOMER_UNIT).ToList()
        End Function
        Public Function GetDataBYNama(ByVal sNAME_DISPLAY As String) As List(Of S_BILLING_TANPARESEP_H)
            If Not oConnection.GetConnection() Then
                GetDataBYNama = Nothing
                Exit Function
            End If
            GetDataBYNama = oConnection.db.S_BILLING_TANPARESEP_Hs.Where(Function(x) x.M_CUSTOMER_UNIT.NAME_DISPLAY.Contains(sNAME_DISPLAY)).ToList()
        End Function
        Public Function GetDataDetail() As List(Of S_BILLING_TANPARESEP_D)
            If Not oConnection.GetConnection() Then
                GetDataDetail = Nothing
                Exit Function
            End If
            GetDataDetail = oConnection.db.S_BILLING_TANPARESEP_Ds.ToList()
        End Function
        Public Function GetDataByKodeDeatil(ByVal Parameter As String) As List(Of S_BILLING_TANPARESEP_H)
            If Not oConnection.GetConnection() Then
                GetDataByKodeDeatil = Nothing
                Exit Function
            End If
            GetDataByKodeDeatil = oConnection.db.S_BILLING_TANPARESEP_Hs.Where(Function(x) x.KDBILLING_TANPARESEP = Parameter And x.PAYAMOUNT = 0).ToList()
        End Function
        Public Function GetDataDetail(ByVal sKDBILLING_TANPARESEP As String) As List(Of S_BILLING_TANPARESEP_D)
            If Not oConnection.GetConnection() Then
                GetDataDetail = Nothing
                Exit Function
            End If
            GetDataDetail = oConnection.db.S_BILLING_TANPARESEP_Ds.Where(Function(x) x.KDBILLING_TANPARESEP = sKDBILLING_TANPARESEP).ToList()
        End Function
        Public Function InsertData(ByVal entity As S_BILLING_TANPARESEP_H, ByVal entityDetail As List(Of S_BILLING_TANPARESEP_D)) As Boolean
            Try
                If Not oConnection.GetConnection() Then
                    InsertData = False
                    Exit Function
                End If

                sREFERENCE = entity.KDBILLING_TANPARESEP
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

                    entity.KDBILLING_TANPARESEP = AutoNumber(sMODUL, sLASTNUMBER + 1, entity.DATE)

                    If entityDetail IsNot Nothing Then
                        For Each iLoop In entityDetail
                            iLoop.KDBILLING_TANPARESEP = entity.KDBILLING_TANPARESEP
                        Next
                    End If
                Catch ex As Exception
                    oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                    Throw ex
                End Try
                Try
                    oConnection.db.S_BILLING_TANPARESEP_Hs.InsertOnSubmit(entity)
                    If entityDetail IsNot Nothing Then
                        oConnection.db.S_BILLING_TANPARESEP_Ds.InsertAllOnSubmit(entityDetail)
                    End If
                Catch ex As Exception
                    oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                    Throw ex
                End Try
                If entityDetail IsNot Nothing Then
                    Try
                        For Each iLoop In entityDetail
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
        Public Function UpdateData(ByVal entity As S_BILLING_TANPARESEP_H, ByVal entityDetail As List(Of S_BILLING_TANPARESEP_D)) As Boolean
            Try
                If Not oConnection.GetConnection() Then
                    UpdateData = False
                    Exit Function
                End If

                sREFERENCE = entity.KDBILLING_TANPARESEP
                sSTATUS = "UPDATE"

                Dim ds = oConnection.db.S_BILLING_TANPARESEP_Hs.FirstOrDefault(Function(x) x.KDBILLING_TANPARESEP = entity.KDBILLING_TANPARESEP)
                Dim dsDetail = oConnection.db.S_BILLING_TANPARESEP_Ds.Where(Function(x) x.KDBILLING_TANPARESEP = entity.KDBILLING_TANPARESEP)

                If dsDetail IsNot Nothing Then
                    Try
                        For Each iLoop In dsDetail

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
                    oConnection.db.S_BILLING_TANPARESEP_Hs.DeleteOnSubmit(ds)
                    oConnection.db.S_BILLING_TANPARESEP_Hs.InsertOnSubmit(entity)
                    If entityDetail IsNot Nothing Then
                        oConnection.db.S_BILLING_TANPARESEP_Ds.DeleteAllOnSubmit(dsDetail)
                        oConnection.db.S_BILLING_TANPARESEP_Ds.InsertAllOnSubmit(entityDetail)
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

                If entityDetail IsNot Nothing Then
                    For Each iLoop In entityDetail

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

                Dim ds = oConnection.db.S_BILLING_TANPARESEP_Hs.FirstOrDefault(Function(x) x.KDBILLING_TANPARESEP = Parameter)
                Dim dsDetail = oConnection.db.S_BILLING_TANPARESEP_Ds.Where(Function(x) x.KDBILLING_TANPARESEP = Parameter)

                Try
                    oConnection.db.S_BILLING_TANPARESEP_Hs.DeleteOnSubmit(ds)
                    If dsDetail IsNot Nothing Then
                        oConnection.db.S_BILLING_TANPARESEP_Ds.DeleteAllOnSubmit(dsDetail)
                        For Each iLoop In dsDetail

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
        Public Function Dokter_Default() As String
            Try
                If Not oConnection.GetConnection() Then
                    Dokter_Default = String.Empty
                    Exit Function
                End If

                Dim ds = oConnection.db.M_DOCTOR_LUARs.FirstOrDefault(Function(x) x.ISDEFAULT = True)
                If ds IsNot Nothing Then
                    Dokter_Default = ds.KDDOCTORLUAR
                Else
                    Dokter_Default = String.Empty
                End If
            Catch ex As Exception
                Dokter_Default = String.Empty
                Throw ex
            End Try
        End Function
    End Class
End Namespace