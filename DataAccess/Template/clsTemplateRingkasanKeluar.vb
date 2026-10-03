Imports System.Threading

Namespace Inventory
    Public Class clsTemplateRingkasanKeluar
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
            sMODUL = "RINGKASANKELUAR_TEMPLATE"

            oCounter = New Setting.clsCounter
        End Sub
        Protected Overrides Sub Finalize()
            MyBase.Finalize()
            oConnection = Nothing
            GC.SuppressFinalize(Me)
        End Sub
        Public Function GetStructureHeader() As S_TEMPLATE_RINGKASANKELUARRAWATINAP
            If Not oConnection.GetConnection() Then
                GetStructureHeader = Nothing
            End If
            GetStructureHeader = New S_TEMPLATE_RINGKASANKELUARRAWATINAP
        End Function
        Public Function GetStructureDetailDiagnosa() As S_TEMPLATE_RINGKASANKELUARRAWATINAP_D
            If Not oConnection.GetConnection() Then
                GetStructureDetailDiagnosa = Nothing
            End If
            GetStructureDetailDiagnosa = New S_TEMPLATE_RINGKASANKELUARRAWATINAP_D
        End Function
        Public Function GetStructureDetailDiagnosaList() As List(Of S_TEMPLATE_RINGKASANKELUARRAWATINAP_D)
            If Not oConnection.GetConnection() Then
                GetStructureDetailDiagnosaList = Nothing
            End If
            GetStructureDetailDiagnosaList = New List(Of S_TEMPLATE_RINGKASANKELUARRAWATINAP_D)
        End Function
        Public Function GetStructureDetailProsedur() As S_TEMPLATE_RINGKASANKELUARRAWATINAP_P
            If Not oConnection.GetConnection() Then
                GetStructureDetailProsedur = Nothing
            End If
            GetStructureDetailProsedur = New S_TEMPLATE_RINGKASANKELUARRAWATINAP_P
        End Function
        Public Function GetStructureDetailProsedurList() As List(Of S_TEMPLATE_RINGKASANKELUARRAWATINAP_P)
            If Not oConnection.GetConnection() Then
                GetStructureDetailProsedurList = Nothing
            End If
            GetStructureDetailProsedurList = New List(Of S_TEMPLATE_RINGKASANKELUARRAWATINAP_P)
        End Function
        Public Function GetData() As List(Of S_TEMPLATE_RINGKASANKELUARRAWATINAP)
            If Not oConnection.GetConnection() Then
                GetData = Nothing
                Exit Function
            End If
            GetData = oConnection.db.S_TEMPLATE_RINGKASANKELUARRAWATINAPs.OrderByDescending(Function(x) x.KDTEMPLATE_RINGKASANKELUAR).ToList()
        End Function
        Public Function GetData(ByVal sKDTEMPLATE_RINGKASANKELUAR As String) As S_TEMPLATE_RINGKASANKELUARRAWATINAP
            If Not oConnection.GetConnection() Then
                GetData = Nothing
                Exit Function
            End If
            GetData = oConnection.db.S_TEMPLATE_RINGKASANKELUARRAWATINAPs.FirstOrDefault(Function(x) x.KDTEMPLATE_RINGKASANKELUAR = sKDTEMPLATE_RINGKASANKELUAR)
        End Function
        Public Function GetDataDetailDiagnosa() As List(Of S_TEMPLATE_RINGKASANKELUARRAWATINAP_D)
            If Not oConnection.GetConnection() Then
                GetDataDetailDiagnosa = Nothing
                Exit Function
            End If
            GetDataDetailDiagnosa = oConnection.db.S_TEMPLATE_RINGKASANKELUARRAWATINAP_Ds.ToList()
        End Function
        Public Function GetDataDetailDiagnosa(ByVal sKDTEMPLATE_RINGKASANKELUAR As String) As List(Of S_TEMPLATE_RINGKASANKELUARRAWATINAP_D)
            If Not oConnection.GetConnection() Then
                GetDataDetailDiagnosa = Nothing
                Exit Function
            End If
            GetDataDetailDiagnosa = oConnection.db.S_TEMPLATE_RINGKASANKELUARRAWATINAP_Ds.Where(Function(x) x.KDTEMPLATE_RINGKASANKELUAR = sKDTEMPLATE_RINGKASANKELUAR).ToList()
        End Function
        Public Function GetDataDetailProsedur() As List(Of S_TEMPLATE_RINGKASANKELUARRAWATINAP_P)
            If Not oConnection.GetConnection() Then
                GetDataDetailProsedur = Nothing
                Exit Function
            End If
            GetDataDetailProsedur = oConnection.db.S_TEMPLATE_RINGKASANKELUARRAWATINAP_Ps.ToList()
        End Function
        Public Function GetDataDetailProsedur(ByVal sKDTEMPLATE_RINGKASANKELUAR As String) As List(Of S_TEMPLATE_RINGKASANKELUARRAWATINAP_P)
            If Not oConnection.GetConnection() Then
                GetDataDetailProsedur = Nothing
                Exit Function
            End If
            GetDataDetailProsedur = oConnection.db.S_TEMPLATE_RINGKASANKELUARRAWATINAP_Ps.Where(Function(x) x.KDTEMPLATE_RINGKASANKELUAR = sKDTEMPLATE_RINGKASANKELUAR).ToList()
        End Function
        Public Function InsertData(ByVal entity As S_TEMPLATE_RINGKASANKELUARRAWATINAP, ByVal entityDetailDiagnosa As List(Of S_TEMPLATE_RINGKASANKELUARRAWATINAP_D), ByVal entityDetailProsedur As List(Of S_TEMPLATE_RINGKASANKELUARRAWATINAP_P)) As Boolean
            Try
                If Not oConnection.GetConnection() Then
                    InsertData = False
                    Exit Function
                End If

                sREFERENCE = entity.KDTEMPLATE_RINGKASANKELUAR
                sSTATUS = "INSERT"

                Try
                    sLASTNUMBER = oCounter.GetLastNumber(sMODUL, entity.DATECREATED)
                    If sLASTNUMBER = 0 Then
                        Try
                            oCounter.InsertData(sMODUL, entity.DATECREATED)
                            sLASTNUMBER = oCounter.GetLastNumber(sMODUL, entity.DATECREATED)
                        Catch ex As Exception
                            sLASTNUMBER = 0
                        End Try
                    End If

                    entity.KDTEMPLATE_RINGKASANKELUAR = AutoNumber(sMODUL, sLASTNUMBER + 1, entity.DATECREATED)
                    For Each iLoop In entityDetailDiagnosa
                        iLoop.KDTEMPLATE_RINGKASANKELUAR = entity.KDTEMPLATE_RINGKASANKELUAR
                    Next
                    For Each iLoop In entityDetailProsedur
                        iLoop.KDTEMPLATE_RINGKASANKELUAR = entity.KDTEMPLATE_RINGKASANKELUAR
                    Next
                Catch ex As Exception
                    oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                    Throw ex
                End Try

                Try
                    oConnection.db.S_TEMPLATE_RINGKASANKELUARRAWATINAPs.InsertOnSubmit(entity)
                    oConnection.db.S_TEMPLATE_RINGKASANKELUARRAWATINAP_Ds.InsertAllOnSubmit(entityDetailDiagnosa)
                    oConnection.db.S_TEMPLATE_RINGKASANKELUARRAWATINAP_Ps.InsertAllOnSubmit(entityDetailProsedur)
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
                    oCounter.UpdateData(sMODUL, sLASTNUMBER + 1, Month(entity.DATECREATED), Year(entity.DATECREATED))
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
        Public Function UpdateData(ByVal entity As S_TEMPLATE_RINGKASANKELUARRAWATINAP, ByVal entityDetail As List(Of S_TEMPLATE_RINGKASANKELUARRAWATINAP_D), ByVal entityDetailProsedur As List(Of S_TEMPLATE_RINGKASANKELUARRAWATINAP_P)) As Boolean
            Try
                If Not oConnection.GetConnection() Then
                    UpdateData = False
                    Exit Function
                End If

                sREFERENCE = entity.KDTEMPLATE_RINGKASANKELUAR
                sSTATUS = "UPDATE"

                Dim ds = oConnection.db.S_TEMPLATE_RINGKASANKELUARRAWATINAPs.FirstOrDefault(Function(x) x.KDTEMPLATE_RINGKASANKELUAR = entity.KDTEMPLATE_RINGKASANKELUAR)

                Try
                    oConnection.db.S_TEMPLATE_RINGKASANKELUARRAWATINAPs.DeleteOnSubmit(ds)
                    oConnection.db.S_TEMPLATE_RINGKASANKELUARRAWATINAPs.InsertOnSubmit(entity)
                Catch ex As Exception
                    oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                    Throw ex
                End Try

                Dim dsDetail = oConnection.db.S_TEMPLATE_RINGKASANKELUARRAWATINAP_Ds.Where(Function(x) x.KDTEMPLATE_RINGKASANKELUAR = entity.KDTEMPLATE_RINGKASANKELUAR)

                Try
                    oConnection.db.S_TEMPLATE_RINGKASANKELUARRAWATINAP_Ds.DeleteAllOnSubmit(dsDetail)
                    oConnection.db.S_TEMPLATE_RINGKASANKELUARRAWATINAP_Ds.InsertAllOnSubmit(entityDetail)
                Catch ex As Exception
                    oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                    Throw ex
                End Try

                Dim dsDetailProsedur = oConnection.db.S_TEMPLATE_RINGKASANKELUARRAWATINAP_Ps.Where(Function(x) x.KDTEMPLATE_RINGKASANKELUAR = entity.KDTEMPLATE_RINGKASANKELUAR)

                Try
                    oConnection.db.S_TEMPLATE_RINGKASANKELUARRAWATINAP_Ps.DeleteAllOnSubmit(dsDetailProsedur)
                    oConnection.db.S_TEMPLATE_RINGKASANKELUARRAWATINAP_Ps.InsertAllOnSubmit(entityDetailProsedur)
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
    End Class
End Namespace