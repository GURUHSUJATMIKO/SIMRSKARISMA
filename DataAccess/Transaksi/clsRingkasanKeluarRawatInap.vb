Imports System.Threading

Namespace Transaksi
    Public Class clsRingkasanKeluarRawatInap
        Public oConnection As Setting.clsConnectionMain = Nothing
        Public oError As Setting.clsError = Nothing
        Public sMODUL As String = ""
        Public sREFERENCE As String = ""
        Public sSTATUS As String = ""
        Public oCounter As Setting.clsCounter = Nothing

        Public Sub New()
            oConnection = New Setting.clsConnectionMain
            oError = New Setting.clsError
            sMODUL = "RKRI"

            oCounter = New Setting.clsCounter
        End Sub
        Protected Overrides Sub Finalize()
            MyBase.Finalize()
            oConnection = Nothing
            GC.SuppressFinalize(Me)
        End Sub
        Public Function GetStructureHeader() As S_RINGKASANKELUARRAWATINAP
            If Not oConnection.GetConnection() Then
                GetStructureHeader = Nothing
            End If
            GetStructureHeader = New S_RINGKASANKELUARRAWATINAP
        End Function
        Public Function GetStructureDetailDiagnosa() As S_RINGKASANKELUARRAWATINAP_D
            If Not oConnection.GetConnection() Then
                GetStructureDetailDiagnosa = Nothing
            End If
            GetStructureDetailDiagnosa = New S_RINGKASANKELUARRAWATINAP_D
        End Function
        Public Function GetStructureDetailProsedur() As S_RINGKASANKELUARRAWATINAP_P
            If Not oConnection.GetConnection() Then
                GetStructureDetailProsedur = Nothing
            End If
            GetStructureDetailProsedur = New S_RINGKASANKELUARRAWATINAP_P
        End Function
        Public Function GetStructureDetaiDiagnosalList() As List(Of S_RINGKASANKELUARRAWATINAP_D)
            If Not oConnection.GetConnection() Then
                GetStructureDetaiDiagnosalList = Nothing
            End If
            GetStructureDetaiDiagnosalList = New List(Of S_RINGKASANKELUARRAWATINAP_D)
        End Function
        Public Function GetStructureDetailProsedurList() As List(Of S_RINGKASANKELUARRAWATINAP_P)
            If Not oConnection.GetConnection() Then
                GetStructureDetailProsedurList = Nothing
            End If
            GetStructureDetailProsedurList = New List(Of S_RINGKASANKELUARRAWATINAP_P)
        End Function
        Public Function GetStructureDetailFarmasi() As S_RINGKASANKELUARRAWATINAP_OBAT
            If Not oConnection.GetConnection() Then
                GetStructureDetailFarmasi = Nothing
            End If
            GetStructureDetailFarmasi = New S_RINGKASANKELUARRAWATINAP_OBAT
        End Function
        Public Function GetStructureDetailFarmasiList() As List(Of S_RINGKASANKELUARRAWATINAP_OBAT)
            If Not oConnection.GetConnection() Then
                GetStructureDetailFarmasiList = Nothing
            End If
            GetStructureDetailFarmasiList = New List(Of S_RINGKASANKELUARRAWATINAP_OBAT)
        End Function
        Public Function GetData() As List(Of S_RINGKASANKELUARRAWATINAP)
            If Not oConnection.GetConnection() Then
                GetData = Nothing
                Exit Function
            End If
            GetData = oConnection.db.S_RINGKASANKELUARRAWATINAPs.OrderByDescending(Function(x) x.KDREG).ToList()
        End Function
        Public Function GetData(ByVal Parameter As String) As S_RINGKASANKELUARRAWATINAP
            If Not oConnection.GetConnection() Then
                GetData = Nothing
                Exit Function
            End If
            GetData = oConnection.db.S_RINGKASANKELUARRAWATINAPs.FirstOrDefault(Function(x) x.KDREG = Parameter)
        End Function
        Public Function GetDataDetailDiagnosa() As List(Of S_RINGKASANKELUARRAWATINAP_D)
            If Not oConnection.GetConnection() Then
                GetDataDetailDiagnosa = Nothing
                Exit Function
            End If
            GetDataDetailDiagnosa = oConnection.db.S_RINGKASANKELUARRAWATINAP_Ds.ToList()
        End Function
        Public Function GetDataDetail_Prosedur() As List(Of S_RINGKASANKELUARRAWATINAP_P)
            If Not oConnection.GetConnection() Then
                GetDataDetail_Prosedur = Nothing
                Exit Function
            End If
            GetDataDetail_Prosedur = oConnection.db.S_RINGKASANKELUARRAWATINAP_Ps.OrderBy(Function(X) X.SEQ).ToList()
        End Function
        Public Function GetDataDetail_Prosedur(ByVal sKDREG As String) As List(Of S_RINGKASANKELUARRAWATINAP_P)
            If Not oConnection.GetConnection() Then
                GetDataDetail_Prosedur = Nothing
                Exit Function
            End If
            GetDataDetail_Prosedur = oConnection.db.S_RINGKASANKELUARRAWATINAP_Ps.Where(Function(X) X.KDREG = sKDREG).OrderBy(Function(X) X.SEQ).ToList()
        End Function
        Public Function GetDataDetailDiagnosa(ByVal sKDREG As String) As List(Of S_RINGKASANKELUARRAWATINAP_D)
            If Not oConnection.GetConnection() Then
                GetDataDetailDiagnosa = Nothing
                Exit Function
            End If
            GetDataDetailDiagnosa = oConnection.db.S_RINGKASANKELUARRAWATINAP_Ds.Where(Function(x) x.KDREG = sKDREG).ToList()
        End Function
        Public Function GetDataDetailFarmasi() As List(Of S_RINGKASANKELUARRAWATINAP_OBAT)
            If Not oConnection.GetConnection() Then
                GetDataDetailFarmasi = Nothing
                Exit Function
            End If
            GetDataDetailFarmasi = oConnection.db.S_RINGKASANKELUARRAWATINAP_OBATs.ToList()
        End Function
        Public Function GetDataDetailFarmasi(ByVal sKDREG As String) As List(Of S_RINGKASANKELUARRAWATINAP_OBAT)
            If Not oConnection.GetConnection() Then
                GetDataDetailFarmasi = Nothing
                Exit Function
            End If
            GetDataDetailFarmasi = oConnection.db.S_RINGKASANKELUARRAWATINAP_OBATs.Where(Function(x) x.KDREG = sKDREG).OrderBy(Function(X) X.SEQ).ToList()
        End Function
        Public Function GetDataKodingByRM(ByVal Parameter As String) As List(Of S_RINGKASANKELUARRAWATINAP)
            If Not oConnection.GetConnection Then
                GetDataKodingByRM = Nothing
                Exit Function
            End If
            GetDataKodingByRM = oConnection.db.S_RINGKASANKELUARRAWATINAPs.Where(Function(x) x.KDCUSTOMER = Parameter).ToList()
        End Function
        Public Function InsertData(ByVal entity As S_RINGKASANKELUARRAWATINAP, ByVal entityDetail As List(Of S_RINGKASANKELUARRAWATINAP_D), ByVal entityDetailBHP As List(Of S_RINGKASANKELUARRAWATINAP_P)) As Boolean
            Try
                If Not oConnection.GetConnection() Then
                    InsertData = False
                    Exit Function
                End If

                sREFERENCE = entity.KDREG
                sSTATUS = "INSERT"

                Try
                    oConnection.db.S_RINGKASANKELUARRAWATINAPs.InsertOnSubmit(entity)
                    If entityDetail IsNot Nothing Then
                        oConnection.db.S_RINGKASANKELUARRAWATINAP_Ds.InsertAllOnSubmit(entityDetail)
                    End If
                    'If entityDetailFarmasi IsNot Nothing Then
                    '    oConnection.db.S_RINGKASANKELUARRAWATINAP_OBATs.InsertAllOnSubmit(entityDetailFarmasi)
                    'End If
                    If entityDetailBHP IsNot Nothing Then
                        oConnection.db.S_RINGKASANKELUARRAWATINAP_Ps.InsertAllOnSubmit(entityDetailBHP)
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
        Public Function UpdateData(ByVal entity As S_RINGKASANKELUARRAWATINAP, ByVal entityDetail As List(Of S_RINGKASANKELUARRAWATINAP_D), ByVal entityDetailBHP As List(Of S_RINGKASANKELUARRAWATINAP_P)) As Boolean
            Try
                If Not oConnection.GetConnection() Then
                    UpdateData = False
                    Exit Function
                End If

                sREFERENCE = entity.KDREG
                sSTATUS = "UPDATE"

                Dim ds = oConnection.db.S_RINGKASANKELUARRAWATINAPs.FirstOrDefault(Function(x) x.KDREG = entity.KDREG)
                Dim dsDetail = oConnection.db.S_RINGKASANKELUARRAWATINAP_Ds.Where(Function(x) x.KDREG = entity.KDREG)
                'Dim dsDetailFarmasi = oConnection.db.S_RINGKASANKELUARRAWATINAP_OBATs.Where(Function(x) x.KDREG = entity.KDREG)
                Dim dsDetailBHP = oConnection.db.S_RINGKASANKELUARRAWATINAP_Ps.Where(Function(x) x.KDREG = entity.KDREG)

                Try
                    oConnection.db.S_RINGKASANKELUARRAWATINAPs.DeleteOnSubmit(ds)
                    oConnection.db.S_RINGKASANKELUARRAWATINAPs.InsertOnSubmit(entity)
                    If entityDetail IsNot Nothing Then
                        oConnection.db.S_RINGKASANKELUARRAWATINAP_Ds.DeleteAllOnSubmit(dsDetail)
                        oConnection.db.S_RINGKASANKELUARRAWATINAP_Ds.InsertAllOnSubmit(entityDetail)
                    End If
                    'If entityDetailFarmasi IsNot Nothing Then
                    '    oConnection.db.S_RINGKASANKELUARRAWATINAP_OBATs.DeleteAllOnSubmit(dsDetailFarmasi)
                    '    oConnection.db.S_RINGKASANKELUARRAWATINAP_OBATs.InsertAllOnSubmit(entityDetailFarmasi)
                    'End If
                    If entityDetailBHP IsNot Nothing Then
                        oConnection.db.S_RINGKASANKELUARRAWATINAP_Ps.DeleteAllOnSubmit(dsDetailBHP)
                        oConnection.db.S_RINGKASANKELUARRAWATINAP_Ps.InsertAllOnSubmit(entityDetailBHP)
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

                Dim ds = oConnection.db.S_RINGKASANKELUARRAWATINAPs.FirstOrDefault(Function(x) x.KDREG = Parameter)
                Dim dsDetail = oConnection.db.S_RINGKASANKELUARRAWATINAP_Ds.Where(Function(x) x.KDREG = Parameter)
                Dim dsDetailFarmasi = oConnection.db.S_RINGKASANKELUARRAWATINAP_OBATs.Where(Function(x) x.KDREG = Parameter)
                Dim dsDetaiLBHP = oConnection.db.S_RINGKASANKELUARRAWATINAP_Ps.Where(Function(x) x.KDREG = Parameter)

                Try
                    oConnection.db.S_RINGKASANKELUARRAWATINAPs.DeleteOnSubmit(ds)
                    If dsDetail IsNot Nothing Then
                        oConnection.db.S_RINGKASANKELUARRAWATINAP_Ds.DeleteAllOnSubmit(dsDetail)
                    End If
                    If dsDetailFarmasi IsNot Nothing Then
                        oConnection.db.S_RINGKASANKELUARRAWATINAP_OBATs.DeleteAllOnSubmit(dsDetailFarmasi)
                    End If
                    If dsDetaiLBHP IsNot Nothing Then
                        oConnection.db.S_RINGKASANKELUARRAWATINAP_Ps.DeleteAllOnSubmit(dsDetaiLBHP)
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
        Public Function UpdateTanggalKeluar(ByVal sKDPENDAFATRAN As String, sTANGGALPULANG As DateTime) As Boolean
            Try
                If Not oConnection.GetConnection Then
                    UpdateTanggalKeluar = False
                    Exit Function
                End If

                UpdateTanggalKeluar = True

                Dim ds = oConnection.db.S_RINGKASANKELUARRAWATINAPs.FirstOrDefault(Function(x) x.KDREG = sKDPENDAFATRAN)

                ds.TANGGALPULANG = sTANGGALPULANG

                oConnection.db.SubmitChanges()

            Catch ex As Exception
                UpdateTanggalKeluar = False
                Throw ex
            End Try
        End Function
    End Class
End Namespace