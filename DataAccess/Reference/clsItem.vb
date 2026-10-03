Imports DataAccess.My.Resources

Namespace Reference
    Public Class clsItem
        Public oConnection As Setting.clsConnectionMain = Nothing
        Public oError As Setting.clsError = Nothing

        Public sMODUL As String = ""
        Public sREFERENCE As String = ""
        Public sSTATUS As String = ""
        Public sLASTNUMBER As Integer = 0

        Public oCounter As Setting.clsCounter = Nothing

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

            sMODUL = "ITEM"

        End Sub
        Protected Overrides Sub Finalize()
            MyBase.Finalize()
            oConnection = Nothing
            GC.SuppressFinalize(Me)
        End Sub
        Public Function GetStructureHeader() As M_ITEM
            If Not oConnection.GetConnection() Then
                GetStructureHeader = Nothing
            End If
            GetStructureHeader = New M_ITEM
        End Function
        Public Function GetStructureDetail_VENDOR() As M_ITEM_VENDOR
            If Not oConnection.GetConnection() Then
                GetStructureDetail_VENDOR = Nothing
            End If
            GetStructureDetail_VENDOR = New M_ITEM_VENDOR
        End Function
        Public Function GetStructureDetail_QTY() As M_ITEM_QTY
            If Not oConnection.GetConnection() Then
                GetStructureDetail_QTY = Nothing
            End If
            GetStructureDetail_QTY = New M_ITEM_QTY
        End Function
        Public Function GetStructureDetail_UOM() As M_ITEM_UOM
            If Not oConnection.GetConnection() Then
                GetStructureDetail_UOM = Nothing
            End If
            GetStructureDetail_UOM = New M_ITEM_UOM
        End Function
        Public Function GetStructureDetail_JASA() As M_ITEM_JASA
            If Not oConnection.GetConnection() Then
                GetStructureDetail_JASA = Nothing
            End If
            GetStructureDetail_JASA = New M_ITEM_JASA
        End Function
        Public Function GetStructureDetail_ITEM() As M_ITEM_BHP
            If Not oConnection.GetConnection() Then
                GetStructureDetail_ITEM = Nothing
            End If
            GetStructureDetail_ITEM = New M_ITEM_BHP
        End Function
        Public Function GetStructureDetail_UOMList() As List(Of M_ITEM_UOM)
            If Not oConnection.GetConnection() Then
                GetStructureDetail_UOMList = Nothing
            End If
            GetStructureDetail_UOMList = New List(Of M_ITEM_UOM)
        End Function
        Public Function GetStructureDetail_VENDORList() As List(Of M_ITEM_VENDOR)
            If Not oConnection.GetConnection() Then
                GetStructureDetail_VENDORList = Nothing
            End If
            GetStructureDetail_VENDORList = New List(Of M_ITEM_VENDOR)
        End Function
        Public Function GetStructureDetail_QTYList() As List(Of M_ITEM_QTY)
            If Not oConnection.GetConnection() Then
                GetStructureDetail_QTYList = Nothing
            End If
            GetStructureDetail_QTYList = New List(Of M_ITEM_QTY)
        End Function
        Public Function GetStructureDetail_JASAList() As List(Of M_ITEM_JASA)
            If Not oConnection.GetConnection() Then
                GetStructureDetail_JASAList = Nothing
            End If
            GetStructureDetail_JASAList = New List(Of M_ITEM_JASA)
        End Function
        Public Function GetStructureDetail_ITEMList() As List(Of M_ITEM_BHP)
            If Not oConnection.GetConnection() Then
                GetStructureDetail_ITEMList = Nothing
            End If
            GetStructureDetail_ITEMList = New List(Of M_ITEM_BHP)
        End Function
        Public Function GetStructureDetail_WAREHOUSE() As M_ITEM_WAREHOUSE
            If Not oConnection.GetConnection() Then
                GetStructureDetail_WAREHOUSE = Nothing
            End If
            GetStructureDetail_WAREHOUSE = New M_ITEM_WAREHOUSE
        End Function
        Public Function GetStructureDetail_WAREHOUSEList() As List(Of M_ITEM_WAREHOUSE)
            If Not oConnection.GetConnection() Then
                GetStructureDetail_WAREHOUSEList = Nothing
            End If
            GetStructureDetail_WAREHOUSEList = New List(Of M_ITEM_WAREHOUSE)
        End Function
        Public Function GetData() As List(Of M_ITEM)
            If Not oConnection.GetConnection() Then
                GetData = Nothing
                Exit Function
            End If
            GetData = oConnection.db.M_ITEMs.OrderBy(Function(x) x.NMITEM1).ToList()
        End Function
        Public Function GetDataL1() As List(Of M_ITEM)
            If Not oConnection.GetConnection() Then
                GetDataL1 = Nothing
                Exit Function
            End If
            GetDataL1 = oConnection.db.M_ITEMs.Where(Function(x) x.M_ITEM_L1.MEMO = "OBAT").OrderBy(Function(x) x.NMITEM1).ToList()
        End Function
        Public Function GetDataL1_() As List(Of M_ITEM)
            If Not oConnection.GetConnection() Then
                GetDataL1_ = Nothing
                Exit Function
            End If
            GetDataL1_ = oConnection.db.M_ITEMs.Where(Function(x) x.M_ITEM_L1.MEMO <> "OBAT").OrderBy(Function(x) x.NMITEM1).ToList()
        End Function
        Public Function GetData(ByVal sKDITEM As String) As M_ITEM
            If Not oConnection.GetConnection() Then
                GetData = Nothing
                Exit Function
            End If
            GetData = oConnection.db.M_ITEMs.FirstOrDefault(Function(x) x.KDITEM = sKDITEM)
        End Function
        Public Function GetDataByName(ByVal sKDITEM As String) As M_ITEM
            If Not oConnection.GetConnection() Then
                GetDataByName = Nothing
                Exit Function
            End If
            GetDataByName = oConnection.db.M_ITEMs.FirstOrDefault(Function(x) x.NMITEM2 = sKDITEM)
        End Function
        Public Function GetDataDetail_UOM() As List(Of M_ITEM_UOM)
            If Not oConnection.GetConnection() Then
                GetDataDetail_UOM = Nothing
                Exit Function
            End If
            GetDataDetail_UOM = oConnection.db.M_ITEM_UOMs.ToList()
        End Function
        Public Function GetDataDetail_UOM(ByVal sKDITEM As String) As List(Of M_ITEM_UOM)
            If Not oConnection.GetConnection() Then
                GetDataDetail_UOM = Nothing
                Exit Function
            End If
            GetDataDetail_UOM = oConnection.db.M_ITEM_UOMs.Where(Function(x) x.KDITEM = sKDITEM).ToList()
        End Function
        Public Function GetDataDetail_VENDOR(ByVal sKDITEM As String) As List(Of M_ITEM_VENDOR)
            If Not oConnection.GetConnection() Then
                GetDataDetail_VENDOR = Nothing
                Exit Function
            End If
            GetDataDetail_VENDOR = oConnection.db.M_ITEM_VENDORs.Where(Function(x) x.KDITEM = sKDITEM).ToList()
        End Function
        Public Function GetDataDetail_QTY(ByVal sKDITEM As String) As List(Of M_ITEM_QTY)
            If Not oConnection.GetConnection() Then
                GetDataDetail_QTY = Nothing
                Exit Function
            End If
            GetDataDetail_QTY = oConnection.db.M_ITEM_QTies.Where(Function(x) x.KDITEM = sKDITEM).ToList()
        End Function
        Public Function GetDataDetail_UOM(ByVal sKDITEM As String, ByVal sKDUOM As String) As M_ITEM_UOM
            If Not oConnection.GetConnection() Then
                GetDataDetail_UOM = Nothing
                Exit Function
            End If
            GetDataDetail_UOM = oConnection.db.M_ITEM_UOMs.FirstOrDefault(Function(x) x.KDITEM = sKDITEM And x.KDUOM = sKDUOM)
        End Function
        Public Function GetDataDetail_WAROUSE(ByVal sKDITEM As String, ByVal sKDUOM As String, ByVal sKDWAREHOUSE As String) As M_ITEM_WAREHOUSE
            If Not oConnection.GetConnection() Then
                GetDataDetail_WAROUSE = Nothing
                Exit Function
            End If
            GetDataDetail_WAROUSE = oConnection.db.M_ITEM_WAREHOUSEs.FirstOrDefault(Function(x) x.KDITEM = sKDITEM And x.KDUOM = sKDUOM And x.KDWAREHOUSE = sKDWAREHOUSE)
        End Function
        Public Function GetDataDetail_JASA() As List(Of M_ITEM_JASA)
            If Not oConnection.GetConnection() Then
                GetDataDetail_JASA = Nothing
                Exit Function
            End If
            GetDataDetail_JASA = oConnection.db.M_ITEM_JASAs.ToList()
        End Function
        Public Function GetDataDetail_JASA(ByVal sKDITEM As String) As List(Of M_ITEM_JASA)
            If Not oConnection.GetConnection() Then
                GetDataDetail_JASA = Nothing
                Exit Function
            End If
            GetDataDetail_JASA = oConnection.db.M_ITEM_JASAs.Where(Function(x) x.KDITEM = sKDITEM).ToList()
        End Function
        Public Function GetDataDetail_WAREHOUSE() As List(Of M_ITEM_WAREHOUSE)
            If Not oConnection.GetConnection() Then
                GetDataDetail_WAREHOUSE = Nothing
                Exit Function
            End If
            GetDataDetail_WAREHOUSE = oConnection.db.M_ITEM_WAREHOUSEs.ToList()
        End Function
        Public Function GetDataDetail_WAREHOUSEList(ByVal sKDWAREHOUSE As String) As List(Of M_ITEM_WAREHOUSE)
            If Not oConnection.GetConnection() Then
                GetDataDetail_WAREHOUSEList = Nothing
                Exit Function
            End If
            GetDataDetail_WAREHOUSEList = oConnection.db.M_ITEM_WAREHOUSEs.Where(Function(x) x.KDWAREHOUSE = sKDWAREHOUSE And x.AMOUNT <> 0).ToList()
        End Function
        Public Function GetDataDetail_WAREHOUSE(ByVal sKDITEM As String) As List(Of M_ITEM_WAREHOUSE)
            If Not oConnection.GetConnection() Then
                GetDataDetail_WAREHOUSE = Nothing
                Exit Function
            End If
            GetDataDetail_WAREHOUSE = oConnection.db.M_ITEM_WAREHOUSEs.Where(Function(x) x.KDITEM = sKDITEM).ToList
        End Function
        Public Function GetDataDetail_WAREHOUSE(ByVal sKDITEM As String, ByVal sKDWAREHOUSE As String) As List(Of M_ITEM_WAREHOUSE)
            If Not oConnection.GetConnection() Then
                GetDataDetail_WAREHOUSE = Nothing
                Exit Function
            End If
            GetDataDetail_WAREHOUSE = oConnection.db.M_ITEM_WAREHOUSEs.Where(Function(x) x.KDITEM = sKDITEM And x.KDWAREHOUSE = sKDWAREHOUSE).ToList
        End Function
        Public Function GetDataDetail_WAREHOUSE(ByVal sKDITEM As String, ByVal sKDWAREHOUSE As String, ByVal sKDUOM As String) As M_ITEM_WAREHOUSE
            If Not oConnection.GetConnection() Then
                GetDataDetail_WAREHOUSE = Nothing
                Exit Function
            End If
            GetDataDetail_WAREHOUSE = oConnection.db.M_ITEM_WAREHOUSEs.FirstOrDefault(Function(x) x.KDITEM = sKDITEM And x.KDWAREHOUSE = sKDWAREHOUSE And x.KDUOM = sKDUOM)
        End Function
        Public Function GetDataRate(ByVal sKDITEM As String, ByVal sKDUOM As String) As Decimal
            If Not oConnection.GetConnection() Then
                GetDataRate = Nothing
                Exit Function
            End If
            GetDataRate = oConnection.db.M_ITEM_UOMs.FirstOrDefault(Function(x) x.KDITEM = sKDITEM And x.KDUOM = sKDUOM).RATE
        End Function
        Public Function GetDataDetail_ITEM() As List(Of M_ITEM_BHP)
            If Not oConnection.GetConnection() Then
                GetDataDetail_ITEM = Nothing
                Exit Function
            End If
            GetDataDetail_ITEM = oConnection.db.M_ITEM_BHPs.ToList()
        End Function
        Public Function GetDataDetail_ITEM(ByVal sKDITEM As String) As List(Of M_ITEM_BHP)
            If Not oConnection.GetConnection() Then
                GetDataDetail_ITEM = Nothing
                Exit Function
            End If
            GetDataDetail_ITEM = oConnection.db.M_ITEM_BHPs.Where(Function(x) x.KDITEM_H = sKDITEM).ToList()
        End Function
        Public Function IsExist(ByVal sNMITEM2 As String) As Boolean
            If Not oConnection.GetConnection() Then
                IsExist = False
                Exit Function
            End If

            Dim ds = oConnection.db.M_ITEMs.FirstOrDefault(Function(x) x.NMITEM2 = sNMITEM2)

            If ds IsNot Nothing Then
                IsExist = True
            Else
                IsExist = False
            End If
        End Function
        Public Function InsertData(ByVal Item As String, ByVal entity As M_ITEM, ByVal entityDetail_UOM As List(Of M_ITEM_UOM), ByVal entityDetail_JASA As List(Of M_ITEM_JASA), ByVal entityDetail_ITEM As List(Of M_ITEM_BHP), ByVal entityDetail_QTY As List(Of M_ITEM_QTY), ByVal entityDetail_VENDOR As List(Of M_ITEM_VENDOR)) As Boolean
            Try
                If Not oConnection.GetConnection() Then
                    InsertData = False
                    Exit Function
                End If

                sREFERENCE = entity.KDITEM
                sSTATUS = "INSERT"

                sMODUL = Item

                If sMODUL = "OBAT" Then
                    Try
                        sLASTNUMBER = oCounter.GetLastNumber(sMODUL)
                        If sLASTNUMBER = 0 Then
                            Try
                                oCounter.InsertData(sMODUL, entity.DATECREATED)
                                sLASTNUMBER = oCounter.GetLastNumber(sMODUL, entity.DATECREATED)
                            Catch ex As Exception
                                sLASTNUMBER = 0
                            End Try
                        End If

                        entity.KDITEM = sLASTNUMBER + 1

                    Catch ex As Exception
                        oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                        Throw ex
                    End Try

                    Try
                        oCounter.UpdateData(sMODUL, sLASTNUMBER + 1)
                    Catch ex As Exception
                        oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                        Throw ex
                    End Try
                Else
                    Try
                        sLASTNUMBER = oCounter.GetLastNumber(sMODUL)
                        If sLASTNUMBER = 0 Then
                            Try
                                oCounter.InsertData(sMODUL, entity.DATECREATED)
                                sLASTNUMBER = oCounter.GetLastNumber(sMODUL, entity.DATECREATED)
                            Catch ex As Exception
                                sLASTNUMBER = 0
                            End Try
                        End If

                        entity.KDITEM = "L" & sLASTNUMBER + 1

                    Catch ex As Exception
                        oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                        Throw ex
                    End Try

                    Try
                        oCounter.UpdateData(sMODUL, sLASTNUMBER + 1)
                    Catch ex As Exception
                        oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                        Throw ex
                    End Try
                End If

                Try
                    oConnection.db.M_ITEMs.InsertOnSubmit(entity)
                Catch ex As Exception
                    oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                    Throw ex
                End Try

                Try
                    If entityDetail_UOM IsNot Nothing Then
                        For Each iLoop In entityDetail_UOM
                            iLoop.KDITEM = entity.KDITEM
                        Next

                        oConnection.db.M_ITEM_UOMs.InsertAllOnSubmit(entityDetail_UOM)
                    End If
                Catch ex As Exception
                    oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                    Throw ex
                End Try

                Try
                    If entityDetail_JASA IsNot Nothing Then
                        For Each iLoop In entityDetail_JASA
                            iLoop.KDITEM = entity.KDITEM
                        Next

                        oConnection.db.M_ITEM_JASAs.InsertAllOnSubmit(entityDetail_JASA)
                    End If
                Catch ex As Exception
                    oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                    Throw ex
                End Try

                Try
                    If entityDetail_ITEM IsNot Nothing Then
                        For Each iLoop In entityDetail_ITEM
                            iLoop.KDITEM_H = entity.KDITEM
                        Next

                        oConnection.db.M_ITEM_BHPs.InsertAllOnSubmit(entityDetail_ITEM)
                    End If
                Catch ex As Exception
                    oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                    Throw ex
                End Try

                Try
                    If entityDetail_QTY IsNot Nothing Then
                        For Each iLoop In entityDetail_QTY
                            iLoop.KDITEM = entity.KDITEM
                        Next

                        oConnection.db.M_ITEM_QTies.InsertAllOnSubmit(entityDetail_QTY)
                    End If
                Catch ex As Exception
                    oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                    Throw ex
                End Try

                Try
                    If entityDetail_VENDOR IsNot Nothing Then
                        For Each iLoop In entityDetail_VENDOR
                            iLoop.KDITEM = entity.KDITEM
                        Next

                        oConnection.db.M_ITEM_VENDORs.InsertAllOnSubmit(entityDetail_VENDOR)
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

                InsertData = True
            Catch ex As Exception
                InsertData = False
                oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                Throw ex
            End Try
        End Function
        Public Function UpdateData(ByVal entity As M_ITEM, ByVal entityDetail_UOM As List(Of M_ITEM_UOM), ByVal entityDetail_JASA As List(Of M_ITEM_JASA), ByVal entityDetail_ITEM As List(Of M_ITEM_BHP), ByVal entityDetail_QTY As List(Of M_ITEM_QTY), ByVal entityDetail_VENDOR As List(Of M_ITEM_VENDOR)) As Boolean
            Try
                If Not oConnection.GetConnection() Then
                    UpdateData = False
                    Exit Function
                End If

                sREFERENCE = entity.KDITEM
                sSTATUS = "UPDATE"

                Dim ds = oConnection.db.M_ITEMs.FirstOrDefault(Function(x) x.KDITEM = entity.KDITEM)

                Try
                    oConnection.db.M_ITEMs.DeleteOnSubmit(ds)
                    oConnection.db.M_ITEMs.InsertOnSubmit(entity)
                Catch ex As Exception
                    oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                    Throw ex
                End Try

                Dim dsDetail_UOM = oConnection.db.M_ITEM_UOMs.Where(Function(x) x.KDITEM = entity.KDITEM)

                Try
                    oConnection.db.M_ITEM_UOMs.DeleteAllOnSubmit(dsDetail_UOM)
                    oConnection.db.M_ITEM_UOMs.InsertAllOnSubmit(entityDetail_UOM)
                Catch ex As Exception
                    oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                    Throw ex
                End Try

                Dim dsDetail_JASA = oConnection.db.M_ITEM_JASAs.Where(Function(x) x.KDITEM = entity.KDITEM)

                Try
                    oConnection.db.M_ITEM_JASAs.DeleteAllOnSubmit(dsDetail_JASA)
                    oConnection.db.M_ITEM_JASAs.InsertAllOnSubmit(entityDetail_JASA)
                Catch ex As Exception
                    oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                    Throw ex
                End Try

                Dim dsDetail_ITEM = oConnection.db.M_ITEM_BHPs.Where(Function(x) x.KDITEM_H = entity.KDITEM)

                Try
                    oConnection.db.M_ITEM_BHPs.DeleteAllOnSubmit(dsDetail_ITEM)
                    oConnection.db.M_ITEM_BHPs.InsertAllOnSubmit(entityDetail_ITEM)
                Catch ex As Exception
                    oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                    Throw ex
                End Try

                Dim dsDetail_QTY = oConnection.db.M_ITEM_QTies.Where(Function(x) x.KDITEM = entity.KDITEM)

                Try
                    oConnection.db.M_ITEM_QTies.DeleteAllOnSubmit(dsDetail_QTY)
                    oConnection.db.M_ITEM_QTies.InsertAllOnSubmit(entityDetail_QTY)
                Catch ex As Exception
                    oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                    Throw ex
                End Try

                Dim dsDetail_VENDOR = oConnection.db.M_ITEM_VENDORs.Where(Function(x) x.KDITEM = entity.KDITEM)

                Try
                    oConnection.db.M_ITEM_VENDORs.DeleteAllOnSubmit(dsDetail_VENDOR)
                    oConnection.db.M_ITEM_VENDORs.InsertAllOnSubmit(entityDetail_VENDOR)
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
        Public Function DeleteData(ByVal sKDITEM As String) As Boolean
            Try
                If Not oConnection.GetConnection() Then
                    DeleteData = False
                    Exit Function
                End If

                sREFERENCE = sKDITEM
                sSTATUS = "DELETE"

                Dim ds = oConnection.db.M_ITEMs.FirstOrDefault(Function(x) x.KDITEM = sKDITEM)
                Dim dsDetail_UOM = oConnection.db.M_ITEM_UOMs.Where(Function(x) x.KDITEM = sKDITEM)
                Dim dsDetail_JASA = oConnection.db.M_ITEM_JASAs.Where(Function(x) x.KDITEM = sKDITEM)
                Dim dsDetail_ITEM = oConnection.db.M_ITEM_BHPs.Where(Function(x) x.KDITEM_H = sKDITEM)
                Dim dsDetail_QTY = oConnection.db.M_ITEM_QTies.Where(Function(x) x.KDITEM = sKDITEM)
                Dim dsDetail_VENDOR = oConnection.db.M_ITEM_VENDORs.Where(Function(x) x.KDITEM = sKDITEM)

                Try
                    oConnection.db.M_ITEMs.DeleteOnSubmit(ds)
                    oConnection.db.M_ITEM_UOMs.DeleteAllOnSubmit(dsDetail_UOM)
                    oConnection.db.M_ITEM_JASAs.DeleteAllOnSubmit(dsDetail_JASA)
                    oConnection.db.M_ITEM_BHPs.DeleteAllOnSubmit(dsDetail_ITEM)
                    oConnection.db.M_ITEM_QTies.DeleteAllOnSubmit(dsDetail_QTY)
                    oConnection.db.M_ITEM_VENDORs.DeleteAllOnSubmit(dsDetail_VENDOR)
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
        Public Function DefaultItem_L1() As String
            Try
                If Not oConnection.GetConnection() Then
                    DefaultItem_L1 = String.Empty
                    Exit Function
                End If

                Dim ds = oConnection.db.M_ITEM_L1s.FirstOrDefault(Function(x) x.ISDEFAULT = True)
                Try
                    DefaultItem_L1 = ds.KDITEM_L1
                Catch ex As Exception
                    DefaultItem_L1 = String.Empty
                End Try
            Catch ex As Exception
                DefaultItem_L1 = String.Empty
                Throw ex
            End Try
        End Function
        Public Function DefaultItem_L2() As String
            Try
                If Not oConnection.GetConnection() Then
                    DefaultItem_L2 = String.Empty
                    Exit Function
                End If

                Dim ds = oConnection.db.M_ITEM_L2s.FirstOrDefault(Function(x) x.ISDEFAULT = True)
                Try
                    DefaultItem_L2 = ds.KDITEM_L2
                Catch ex As Exception
                    DefaultItem_L2 = String.Empty
                End Try
            Catch ex As Exception
                DefaultItem_L2 = String.Empty
                Throw ex
            End Try
        End Function
        Public Function DefaultProducer() As String
            Try
                If Not oConnection.GetConnection() Then
                    DefaultProducer = String.Empty
                    Exit Function
                End If

                Dim ds = oConnection.db.M_PRODUCERs.FirstOrDefault(Function(x) x.ISDEFAULT = True)
                Try
                    DefaultProducer = ds.KDPRODUCER
                Catch ex As Exception
                    DefaultProducer = String.Empty
                End Try
            Catch ex As Exception
                DefaultProducer = String.Empty
                Throw ex
            End Try
        End Function
        Public Function DefaultItem_L3() As String
            Try
                If Not oConnection.GetConnection() Then
                    DefaultItem_L3 = String.Empty
                    Exit Function
                End If

                Dim ds = oConnection.db.M_ITEM_L3s.FirstOrDefault(Function(x) x.ISDEFAULT = True)
                Try
                    DefaultItem_L3 = ds.KDITEM_L3
                Catch ex As Exception
                    DefaultItem_L3 = String.Empty
                End Try
            Catch ex As Exception
                DefaultItem_L3 = String.Empty
                Throw ex
            End Try
        End Function
        Public Function DefaultItem_L4() As String
            Try
                If Not oConnection.GetConnection() Then
                    DefaultItem_L4 = String.Empty
                    Exit Function
                End If

                Dim ds = oConnection.db.M_ITEM_L4s.FirstOrDefault(Function(x) x.ISDEFAULT = True)
                Try
                    DefaultItem_L4 = ds.KDITEM_L4
                Catch ex As Exception
                    DefaultItem_L4 = String.Empty
                End Try
            Catch ex As Exception
                DefaultItem_L4 = String.Empty
                Throw ex
            End Try
        End Function
        Public Function DefaultItem_L5() As String
            Try
                If Not oConnection.GetConnection() Then
                    DefaultItem_L5 = String.Empty
                    Exit Function
                End If

                Dim ds = oConnection.db.M_ITEM_L5s.FirstOrDefault(Function(x) x.ISDEFAULT = True)
                Try
                    DefaultItem_L5 = ds.KDITEM_L5
                Catch ex As Exception
                    DefaultItem_L5 = String.Empty
                End Try
            Catch ex As Exception
                DefaultItem_L5 = String.Empty
                Throw ex
            End Try
        End Function
        Public Function DefaultItem_L6() As String
            Try
                If Not oConnection.GetConnection() Then
                    DefaultItem_L6 = String.Empty
                    Exit Function
                End If

                Dim ds = oConnection.db.M_ITEM_L6s.FirstOrDefault(Function(x) x.ISDEFAULT = True)
                Try
                    DefaultItem_L6 = ds.KDITEM_L6
                Catch ex As Exception
                    DefaultItem_L6 = String.Empty
                End Try
            Catch ex As Exception
                DefaultItem_L6 = String.Empty
                Throw ex
            End Try
        End Function
        Public Function DefaultItem_L7() As String
            Try
                If Not oConnection.GetConnection() Then
                    DefaultItem_L7 = String.Empty
                    Exit Function
                End If

                Dim ds = oConnection.db.M_ITEM_L7s.FirstOrDefault(Function(x) x.ISDEFAULT = True)
                Try
                    DefaultItem_L7 = ds.KDITEM_L7
                Catch ex As Exception
                    DefaultItem_L7 = String.Empty
                End Try
            Catch ex As Exception
                DefaultItem_L7 = String.Empty
                Throw ex
            End Try
        End Function
        Public Function DefaultItem_L8() As String
            Try
                If Not oConnection.GetConnection() Then
                    DefaultItem_L8 = String.Empty
                    Exit Function
                End If

                Dim ds = oConnection.db.M_ITEM_L8s.FirstOrDefault(Function(x) x.ISDEFAULT = True)
                Try
                    DefaultItem_L8 = ds.KDITEM_L8
                Catch ex As Exception
                    DefaultItem_L8 = String.Empty
                End Try
            Catch ex As Exception
                DefaultItem_L8 = String.Empty
                Throw ex
            End Try
        End Function
        Public Function DefaultItem_L9() As String
            Try
                If Not oConnection.GetConnection() Then
                    DefaultItem_L9 = String.Empty
                    Exit Function
                End If

                Dim ds = oConnection.db.M_ITEM_L9s.FirstOrDefault(Function(x) x.ISDEFAULT = True)
                Try
                    DefaultItem_L9 = ds.KDITEM_L9
                Catch ex As Exception
                    DefaultItem_L9 = String.Empty
                End Try
            Catch ex As Exception
                DefaultItem_L9 = String.Empty
                Throw ex
            End Try
        End Function
        Public Function DefautlSigna() As String
            Try
                If Not oConnection.GetConnection() Then
                    DefautlSigna = String.Empty
                    Exit Function
                End If

                Dim ds = oConnection.db.M_SIGNAs.FirstOrDefault(Function(x) x.ISDEFAULT = True)
                Try
                    DefautlSigna = ds.KDSIGNA
                Catch ex As Exception
                    DefautlSigna = String.Empty
                End Try
            Catch ex As Exception
                DefautlSigna = String.Empty
                Throw ex
            End Try
        End Function
        Public Function DefautlCaraPakai() As String
            Try
                If Not oConnection.GetConnection() Then
                    DefautlCaraPakai = String.Empty
                    Exit Function
                End If

                Dim ds = oConnection.db.M_CARAPAKAIs.FirstOrDefault(Function(x) x.ISDEFAULT = True)
                Try
                    DefautlCaraPakai = ds.KDCARAPAKAI
                Catch ex As Exception
                    DefautlCaraPakai = String.Empty
                End Try
            Catch ex As Exception
                DefautlCaraPakai = String.Empty
                Throw ex
            End Try
        End Function
    End Class
End Namespace