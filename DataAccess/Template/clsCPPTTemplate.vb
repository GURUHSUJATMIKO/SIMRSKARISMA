Imports System.Threading

Namespace Inventory
    Public Class clsCPPTTemplate
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
            sMODUL = "CPPT_TEMPLATE"

            oCounter = New Setting.clsCounter
        End Sub
        Protected Overrides Sub Finalize()
            MyBase.Finalize()
            oConnection = Nothing
            GC.SuppressFinalize(Me)
        End Sub
        Public Function GetStructureHeader() As S_REQ_CPPT_TEMPLATE
            If Not oConnection.GetConnection() Then
                GetStructureHeader = Nothing
            End If
            GetStructureHeader = New S_REQ_CPPT_TEMPLATE
        End Function
        Public Function GetStructureDetailDiagnosa() As S_REQ_CPPT_DIAGNOSA_TEMPLATE
            If Not oConnection.GetConnection() Then
                GetStructureDetailDiagnosa = Nothing
            End If
            GetStructureDetailDiagnosa = New S_REQ_CPPT_DIAGNOSA_TEMPLATE
        End Function
        Public Function GetStructureDetailDiagnosaList() As List(Of S_REQ_CPPT_DIAGNOSA_TEMPLATE)
            If Not oConnection.GetConnection() Then
                GetStructureDetailDiagnosaList = Nothing
            End If
            GetStructureDetailDiagnosaList = New List(Of S_REQ_CPPT_DIAGNOSA_TEMPLATE)
        End Function
        Public Function GetStructureDetailProsedur() As S_REQ_CPPT_PROSEDUR_TEMPLATE
            If Not oConnection.GetConnection() Then
                GetStructureDetailProsedur = Nothing
            End If
            GetStructureDetailProsedur = New S_REQ_CPPT_PROSEDUR_TEMPLATE
        End Function
        Public Function GetStructureDetailProsedurList() As List(Of S_REQ_CPPT_PROSEDUR_TEMPLATE)
            If Not oConnection.GetConnection() Then
                GetStructureDetailProsedurList = Nothing
            End If
            GetStructureDetailProsedurList = New List(Of S_REQ_CPPT_PROSEDUR_TEMPLATE)
        End Function
        Public Function GetData() As List(Of S_REQ_CPPT_TEMPLATE)
            If Not oConnection.GetConnection() Then
                GetData = Nothing
                Exit Function
            End If
            GetData = oConnection.db.S_REQ_CPPT_TEMPLATEs.OrderByDescending(Function(x) x.KDCPPT_TEMPLATE).ToList()
        End Function
        Public Function GetData(ByVal sKDCPPT_TEMPLATE As String) As S_REQ_CPPT_TEMPLATE
            If Not oConnection.GetConnection() Then
                GetData = Nothing
                Exit Function
            End If
            GetData = oConnection.db.S_REQ_CPPT_TEMPLATEs.FirstOrDefault(Function(x) x.KDCPPT_TEMPLATE = sKDCPPT_TEMPLATE)
        End Function
        Public Function GetDataDetailDiagnosa() As List(Of S_REQ_CPPT_DIAGNOSA_TEMPLATE)
            If Not oConnection.GetConnection() Then
                GetDataDetailDiagnosa = Nothing
                Exit Function
            End If
            GetDataDetailDiagnosa = oConnection.db.S_REQ_CPPT_DIAGNOSA_TEMPLATEs.ToList()
        End Function
        Public Function GetDataDetailDiagnosa(ByVal sKDCPPT_TEMPLATE As String) As List(Of S_REQ_CPPT_DIAGNOSA_TEMPLATE)
            If Not oConnection.GetConnection() Then
                GetDataDetailDiagnosa = Nothing
                Exit Function
            End If
            GetDataDetailDiagnosa = oConnection.db.S_REQ_CPPT_DIAGNOSA_TEMPLATEs.Where(Function(x) x.KDCPPT_TEMPLATE = sKDCPPT_TEMPLATE).ToList()
        End Function
        Public Function GetDataDetailProsedur() As List(Of S_REQ_CPPT_PROSEDUR_TEMPLATE)
            If Not oConnection.GetConnection() Then
                GetDataDetailProsedur = Nothing
                Exit Function
            End If
            GetDataDetailProsedur = oConnection.db.S_REQ_CPPT_PROSEDUR_TEMPLATEs.ToList()
        End Function
        Public Function GetDataDetailProsedur(ByVal sKDCPPT_TEMPLATE As String) As List(Of S_REQ_CPPT_PROSEDUR_TEMPLATE)
            If Not oConnection.GetConnection() Then
                GetDataDetailProsedur = Nothing
                Exit Function
            End If
            GetDataDetailProsedur = oConnection.db.S_REQ_CPPT_PROSEDUR_TEMPLATEs.Where(Function(x) x.KDCPPT_TEMPLATE = sKDCPPT_TEMPLATE).ToList()
        End Function
        Public Function InsertData(ByVal entity As S_REQ_CPPT_TEMPLATE, ByVal entityDetailDiagnosa As List(Of S_REQ_CPPT_DIAGNOSA_TEMPLATE), ByVal entityDetailProsedur As List(Of S_REQ_CPPT_PROSEDUR_TEMPLATE)) As Boolean
            Try
                If Not oConnection.GetConnection() Then
                    InsertData = False
                    Exit Function
                End If

                sREFERENCE = entity.KDCPPT_TEMPLATE
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

                    entity.KDCPPT_TEMPLATE = AutoNumber(sMODUL, sLASTNUMBER + 1, entity.DATECREATED)
                    For Each iLoop In entityDetailDiagnosa
                        iLoop.KDCPPT_TEMPLATE = entity.KDCPPT_TEMPLATE
                    Next
                    For Each iLoop In entityDetailProsedur
                        iLoop.KDCPPT_TEMPLATE = entity.KDCPPT_TEMPLATE
                    Next
                Catch ex As Exception
                    oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                    Throw ex
                End Try

                Try
                    oConnection.db.S_REQ_CPPT_TEMPLATEs.InsertOnSubmit(entity)
                    oConnection.db.S_REQ_CPPT_DIAGNOSA_TEMPLATEs.InsertAllOnSubmit(entityDetailDiagnosa)
                    oConnection.db.S_REQ_CPPT_PROSEDUR_TEMPLATEs.InsertAllOnSubmit(entityDetailProsedur)
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
        Public Function UpdateData(ByVal entity As S_REQ_CPPT_TEMPLATE, ByVal entityDetail As List(Of S_REQ_CPPT_DIAGNOSA_TEMPLATE), ByVal entityDetailProsedur As List(Of S_REQ_CPPT_PROSEDUR_TEMPLATE)) As Boolean
            Try
                If Not oConnection.GetConnection() Then
                    UpdateData = False
                    Exit Function
                End If

                sREFERENCE = entity.KDCPPT_TEMPLATE
                sSTATUS = "UPDATE"

                Dim ds = oConnection.db.S_REQ_CPPT_TEMPLATEs.FirstOrDefault(Function(x) x.KDCPPT_TEMPLATE = entity.KDCPPT_TEMPLATE)

                Try
                    oConnection.db.S_REQ_CPPT_TEMPLATEs.DeleteOnSubmit(ds)
                    oConnection.db.S_REQ_CPPT_TEMPLATEs.InsertOnSubmit(entity)
                Catch ex As Exception
                    oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                    Throw ex
                End Try

                Dim dsDetail = oConnection.db.S_REQ_CPPT_DIAGNOSA_TEMPLATEs.Where(Function(x) x.KDCPPT_TEMPLATE = entity.KDCPPT_TEMPLATE)

                Try
                    oConnection.db.S_REQ_CPPT_DIAGNOSA_TEMPLATEs.DeleteAllOnSubmit(dsDetail)
                    oConnection.db.S_REQ_CPPT_DIAGNOSA_TEMPLATEs.InsertAllOnSubmit(entityDetail)
                Catch ex As Exception
                    oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                    Throw ex
                End Try

                Dim dsDetailProsedur = oConnection.db.S_REQ_CPPT_PROSEDUR_TEMPLATEs.Where(Function(x) x.KDCPPT_TEMPLATE = entity.KDCPPT_TEMPLATE)

                Try
                    oConnection.db.S_REQ_CPPT_PROSEDUR_TEMPLATEs.DeleteAllOnSubmit(dsDetailProsedur)
                    oConnection.db.S_REQ_CPPT_PROSEDUR_TEMPLATEs.InsertAllOnSubmit(entityDetailProsedur)
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
        Public Function DeleteData(ByVal sKDCPPT_TEMPLATE As String) As Boolean
            Try
                If Not oConnection.GetConnection() Then
                    DeleteData = False
                    Exit Function
                End If

                sREFERENCE = sKDCPPT_TEMPLATE
                sSTATUS = "DELETE"

                Dim ds = oConnection.db.S_REQ_CPPT_TEMPLATEs.FirstOrDefault(Function(x) x.KDCPPT_TEMPLATE = sKDCPPT_TEMPLATE)
                Dim dsDetail = oConnection.db.S_REQ_CPPT_DIAGNOSA_TEMPLATEs.Where(Function(x) x.KDCPPT_TEMPLATE = sKDCPPT_TEMPLATE)
                Dim dsDetail2 = oConnection.db.S_REQ_CPPT_PROSEDUR_TEMPLATEs.Where(Function(x) x.KDCPPT_TEMPLATE = sKDCPPT_TEMPLATE)

                Try
                    oConnection.db.S_REQ_CPPT_TEMPLATEs.DeleteOnSubmit(ds)
                    oConnection.db.S_REQ_CPPT_DIAGNOSA_TEMPLATEs.DeleteAllOnSubmit(dsDetail)
                    oConnection.db.S_REQ_CPPT_PROSEDUR_TEMPLATEs.DeleteAllOnSubmit(dsDetail2)
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