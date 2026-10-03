Imports System.Threading

Namespace Transaksi
    Public Class clsDIGITAL_IGD_01_AWAL
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
            sMODUL = "AWL"

            oCounter = New Setting.clsCounter
        End Sub
        Protected Overrides Sub Finalize()
            MyBase.Finalize()
            oConnection = Nothing
            GC.SuppressFinalize(Me)
        End Sub
        Public Function GetStructureHeader() As S_DIGITAL_IGD_01_AWAL
            If Not oConnection.GetConnection() Then
                GetStructureHeader = Nothing
            End If
            GetStructureHeader = New S_DIGITAL_IGD_01_AWAL
        End Function
        Public Function GetStructureDetail() As S_DIGITAL_IGD_01_AWAL_D
            If Not oConnection.GetConnection() Then
                GetStructureDetail = Nothing
            End If
            GetStructureDetail = New S_DIGITAL_IGD_01_AWAL_D
        End Function
        Public Function GetStructureDetailList() As List(Of S_DIGITAL_IGD_01_AWAL_D)
            If Not oConnection.GetConnection() Then
                GetStructureDetailList = Nothing
            End If
            GetStructureDetailList = New List(Of S_DIGITAL_IGD_01_AWAL_D)
        End Function
        Public Function GetStructureDetail_P() As S_DIGITAL_IGD_01_AWAL_PENUNJANG
            If Not oConnection.GetConnection() Then
                GetStructureDetail_P = Nothing
            End If
            GetStructureDetail_P = New S_DIGITAL_IGD_01_AWAL_PENUNJANG
        End Function
        Public Function GetStructureDetailList_P() As List(Of S_DIGITAL_IGD_01_AWAL_PENUNJANG)
            If Not oConnection.GetConnection() Then
                GetStructureDetailList_P = Nothing
            End If
            GetStructureDetailList_P = New List(Of S_DIGITAL_IGD_01_AWAL_PENUNJANG)
        End Function
        Public Function GetStructureDetail_ResepPulang() As S_DIGITAL_IGD_01_AWAL_RECIPE
            If Not oConnection.GetConnection() Then
                GetStructureDetail_ResepPulang = Nothing
            End If
            GetStructureDetail_ResepPulang = New S_DIGITAL_IGD_01_AWAL_RECIPE
        End Function
        Public Function GetStructureDetailList_ResepPulang() As List(Of S_DIGITAL_IGD_01_AWAL_RECIPE)
            If Not oConnection.GetConnection() Then
                GetStructureDetailList_ResepPulang = Nothing
            End If
            GetStructureDetailList_ResepPulang = New List(Of S_DIGITAL_IGD_01_AWAL_RECIPE)
        End Function
        Public Function GetData() As List(Of S_DIGITAL_IGD_01_AWAL)
            If Not oConnection.GetConnection() Then
                GetData = Nothing
                Exit Function
            End If
            GetData = oConnection.db.S_DIGITAL_IGD_01_AWALs.OrderByDescending(Function(x) x.KDAWALASESMENIGD).ToList()
        End Function
        Public Function GetData(ByVal Parameter As String) As S_DIGITAL_IGD_01_AWAL
            If Not oConnection.GetConnection() Then
                GetData = Nothing
                Exit Function
            End If
            GetData = oConnection.db.S_DIGITAL_IGD_01_AWALs.FirstOrDefault(Function(x) x.KDAWALASESMENIGD = Parameter)
        End Function
        Public Function GetDataDetail(ByVal sKDAWALASESMENIGD As String) As List(Of S_DIGITAL_IGD_01_AWAL_D)
            If Not oConnection.GetConnection() Then
                GetDataDetail = Nothing
                Exit Function
            End If
            GetDataDetail = oConnection.db.S_DIGITAL_IGD_01_AWAL_Ds.Where(Function(x) x.KDAWALASESMENIGD = sKDAWALASESMENIGD).ToList()
        End Function
        Public Function GetDataDetail_P(ByVal sKDAWALASESMENIGD As String) As List(Of S_DIGITAL_IGD_01_AWAL_PENUNJANG)
            If Not oConnection.GetConnection() Then
                GetDataDetail_P = Nothing
                Exit Function
            End If
            GetDataDetail_P = oConnection.db.S_DIGITAL_IGD_01_AWAL_PENUNJANGs.Where(Function(x) x.KDAWALASESMENIGD = sKDAWALASESMENIGD).ToList()
        End Function
        Public Function GetDataDetail_ResepPulang(ByVal sKDAWALASESMENIGD As String) As List(Of S_DIGITAL_IGD_01_AWAL_RECIPE)
            If Not oConnection.GetConnection() Then
                GetDataDetail_ResepPulang = Nothing
                Exit Function
            End If
            GetDataDetail_ResepPulang = oConnection.db.S_DIGITAL_IGD_01_AWAL_RECIPEs.Where(Function(x) x.KDAWALASESMENIGD = sKDAWALASESMENIGD).ToList()
        End Function
        Public Function InsertData(ByVal entity As S_DIGITAL_IGD_01_AWAL, ByVal entityDetail As List(Of S_DIGITAL_IGD_01_AWAL_D), ByVal entityDetail_P As List(Of S_DIGITAL_IGD_01_AWAL_PENUNJANG), ByVal entityDetail_ResepPulang As List(Of S_DIGITAL_IGD_01_AWAL_RECIPE)) As Boolean
            Try
                If Not oConnection.GetConnection() Then
                    InsertData = False
                    Exit Function
                End If

                sREFERENCE = entity.KDAWALASESMENIGD
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

                    entity.KDAWALASESMENIGD = AutoNumber(sMODUL, sLASTNUMBER + 1, entity.DATE)

                    For Each iLoop In entityDetail
                        iLoop.KDAWALASESMENIGD = entity.KDAWALASESMENIGD
                    Next

                    For Each iLoop In entityDetail_P
                        iLoop.KDAWALASESMENIGD = entity.KDAWALASESMENIGD
                    Next

                    For Each iLoop In entityDetail_ResepPulang
                        iLoop.KDAWALASESMENIGD = entity.KDAWALASESMENIGD
                    Next
                Catch ex As Exception
                    oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                    Throw ex
                End Try
                Try
                    oConnection.db.S_DIGITAL_IGD_01_AWALs.InsertOnSubmit(entity)
                    If entityDetail.Count > 0 Then
                        oConnection.db.S_DIGITAL_IGD_01_AWAL_Ds.InsertAllOnSubmit(entityDetail)
                    End If
                    If entityDetail_P.Count > 0 Then
                        oConnection.db.S_DIGITAL_IGD_01_AWAL_PENUNJANGs.InsertAllOnSubmit(entityDetail_P)
                    End If
                    If entityDetail_ResepPulang.Count > 0 Then
                        oConnection.db.S_DIGITAL_IGD_01_AWAL_RECIPEs.InsertAllOnSubmit(entityDetail_ResepPulang)
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
        Public Function UpdateData(ByVal entity As S_DIGITAL_IGD_01_AWAL, ByVal entityDetail As List(Of S_DIGITAL_IGD_01_AWAL_D), ByVal entityDetail_P As List(Of S_DIGITAL_IGD_01_AWAL_PENUNJANG), ByVal entityDetail_ResepPulang As List(Of S_DIGITAL_IGD_01_AWAL_RECIPE)) As Boolean
            Try
                If Not oConnection.GetConnection() Then
                    UpdateData = False
                    Exit Function
                End If

                sREFERENCE = entity.KDAWALASESMENIGD
                sSTATUS = "UPDATE"

                Dim ds = oConnection.db.S_DIGITAL_IGD_01_AWALs.FirstOrDefault(Function(x) x.KDAWALASESMENIGD = entity.KDAWALASESMENIGD)
                Dim dsDetail = oConnection.db.S_DIGITAL_IGD_01_AWAL_Ds.Where(Function(x) x.KDAWALASESMENIGD = entity.KDAWALASESMENIGD)
                Dim dsDetail_P = oConnection.db.S_DIGITAL_IGD_01_AWAL_PENUNJANGs.Where(Function(x) x.KDAWALASESMENIGD = entity.KDAWALASESMENIGD)
                Dim dsDetail_ResepPulang = oConnection.db.S_DIGITAL_IGD_01_AWAL_RECIPEs.Where(Function(x) x.KDAWALASESMENIGD = entity.KDAWALASESMENIGD)

                Try
                    oConnection.db.S_DIGITAL_IGD_01_AWALs.DeleteOnSubmit(ds)
                    oConnection.db.S_DIGITAL_IGD_01_AWALs.InsertOnSubmit(entity)
                    If dsDetail.Count > 0 Then
                        oConnection.db.S_DIGITAL_IGD_01_AWAL_Ds.DeleteAllOnSubmit(dsDetail)
                    End If
                    If entityDetail.Count > 0 Then
                        oConnection.db.S_DIGITAL_IGD_01_AWAL_Ds.InsertAllOnSubmit(entityDetail)
                    End If
                    If dsDetail_P.Count > 0 Then
                        oConnection.db.S_DIGITAL_IGD_01_AWAL_PENUNJANGs.DeleteAllOnSubmit(dsDetail_P)
                    End If
                    If entityDetail_P.Count > 0 Then
                        oConnection.db.S_DIGITAL_IGD_01_AWAL_PENUNJANGs.InsertAllOnSubmit(entityDetail_P)
                    End If
                    If dsDetail_ResepPulang.Count > 0 Then
                        oConnection.db.S_DIGITAL_IGD_01_AWAL_RECIPEs.DeleteAllOnSubmit(dsDetail_ResepPulang)
                    End If
                    If entityDetail_ResepPulang.Count > 0 Then
                        oConnection.db.S_DIGITAL_IGD_01_AWAL_RECIPEs.InsertAllOnSubmit(entityDetail_ResepPulang)
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

                Dim ds = oConnection.db.S_DIGITAL_IGD_01_AWALs.FirstOrDefault(Function(x) x.KDAWALASESMENIGD = Parameter)
                Dim dsDetail = oConnection.db.S_DIGITAL_IGD_01_AWAL_Ds.Where(Function(x) x.KDAWALASESMENIGD = Parameter)
                Dim dsDetail_P = oConnection.db.S_DIGITAL_IGD_01_AWAL_PENUNJANGs.Where(Function(x) x.KDAWALASESMENIGD = Parameter)
                Dim dsDetail_ResepPulang = oConnection.db.S_DIGITAL_IGD_01_AWAL_RECIPEs.Where(Function(x) x.KDAWALASESMENIGD = Parameter)

                Try
                    oConnection.db.S_DIGITAL_IGD_01_AWALs.DeleteOnSubmit(ds)
                    oConnection.db.S_DIGITAL_IGD_01_AWAL_Ds.DeleteAllOnSubmit(dsDetail)
                    oConnection.db.S_DIGITAL_IGD_01_AWAL_PENUNJANGs.DeleteAllOnSubmit(dsDetail_P)
                    oConnection.db.S_DIGITAL_IGD_01_AWAL_RECIPEs.DeleteAllOnSubmit(dsDetail_ResepPulang)
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