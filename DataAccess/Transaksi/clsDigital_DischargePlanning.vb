Imports System.Threading

Namespace Transaksi
    Public Class clsDigital_DischargePlanning
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
            sMODUL = "HNO"

            oCounter = New Setting.clsCounter
        End Sub
        Protected Overrides Sub Finalize()
            MyBase.Finalize()
            oConnection = Nothing
            GC.SuppressFinalize(Me)
        End Sub
        Public Function GetStructureHeader() As S_DIGITAL_RI_13
            If Not oConnection.GetConnection() Then
                GetStructureHeader = Nothing
            End If
            GetStructureHeader = New S_DIGITAL_RI_13
        End Function
        Public Function GetStructureDetaiDiagnosalList() As List(Of S_DIGITAL_RI_13_DIAGNOSA)
            If Not oConnection.GetConnection() Then
                GetStructureDetaiDiagnosalList = Nothing
            End If
            GetStructureDetaiDiagnosalList = New List(Of S_DIGITAL_RI_13_DIAGNOSA)
        End Function
        Public Function GetStructureDetailDiagnosa() As S_DIGITAL_RI_13_DIAGNOSA
            If Not oConnection.GetConnection() Then
                GetStructureDetailDiagnosa = Nothing
            End If
            GetStructureDetailDiagnosa = New S_DIGITAL_RI_13_DIAGNOSA
        End Function
        Public Function GetData() As List(Of S_DIGITAL_RI_13)
            If Not oConnection.GetConnection() Then
                GetData = Nothing
                Exit Function
            End If
            GetData = oConnection.db.S_DIGITAL_RI_13s.OrderByDescending(Function(x) x.KDREG).ToList()
        End Function
        Public Function GetDataByRM(ByVal kdcustomer As String) As List(Of S_DIGITAL_RI_13)
            If Not oConnection.GetConnection() Then
                GetDataByRM = Nothing
                Exit Function
            End If
            GetDataByRM = oConnection.db.S_DIGITAL_RI_13s.Where(Function(x) x.KDCUSTOMER = kdcustomer).OrderByDescending(Function(x) x.KDREG).ToList()
        End Function
        Public Function GetData(ByVal Parameter As String) As S_DIGITAL_RI_13
            If Not oConnection.GetConnection() Then
                GetData = Nothing
                Exit Function
            End If
            GetData = oConnection.db.S_DIGITAL_RI_13s.FirstOrDefault(Function(x) x.KDREG = Parameter)
        End Function
        Public Function GetDataDetailDiagnosa(ByVal sKDREG As String) As List(Of S_DIGITAL_RI_13_DIAGNOSA)
            If Not oConnection.GetConnection() Then
                GetDataDetailDiagnosa = Nothing
                Exit Function
            End If
            GetDataDetailDiagnosa = oConnection.db.S_DIGITAL_RI_13_DIAGNOSAs.Where(Function(x) x.KDREG = sKDREG).OrderBy(Function(x) x.SEQ).ToList()
        End Function
        Public Function InsertData(ByVal entity As S_DIGITAL_RI_13, ByVal entityDetail As List(Of S_DIGITAL_RI_13_DIAGNOSA)) As Boolean
            Try
                If Not oConnection.GetConnection() Then
                    InsertData = False
                    Exit Function
                End If

                sREFERENCE = entity.KDREG
                sSTATUS = "INSERT"

                'Try
                '    sLASTNUMBER = oCounter.GetLastNumber(sMODUL, entity.DATE)
                '    If sLASTNUMBER = 0 Then
                '        Try
                '            oCounter.InsertData(sMODUL, entity.DATE)
                '            sLASTNUMBER = oCounter.GetLastNumber(sMODUL, entity.DATE)
                '        Catch ex As Exception
                '            sLASTNUMBER = 0
                '        End Try
                '    End If

                '    entity.KDREG = AutoNumber(sMODUL, sLASTNUMBER + 1, entity.DATE)

                'Catch ex As Exception
                '    oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                '    Throw ex
                'End Try

                'Try
                '    oCounter.UpdateData(sMODUL, sLASTNUMBER + 1, Month(entity.DATE), Year(entity.DATE))
                'Catch ex As Exception
                '    oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                '    Throw ex
                'End Try

                Try
                    oConnection.db.S_DIGITAL_RI_13s.InsertOnSubmit(entity)
                    oConnection.db.S_DIGITAL_RI_13_DIAGNOSAs.InsertAllOnSubmit(entityDetail)
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
        Public Function UpdateData(ByVal entity As S_DIGITAL_RI_13, ByVal entityDetail As List(Of S_DIGITAL_RI_13_DIAGNOSA)) As Boolean
            Try
                If Not oConnection.GetConnection() Then
                    UpdateData = False
                    Exit Function
                End If

                sREFERENCE = entity.KDREG
                sSTATUS = "UPDATE"

                Dim ds = oConnection.db.S_DIGITAL_RI_13s.FirstOrDefault(Function(x) x.KDREG = entity.KDREG)
                Dim dsDetail = oConnection.db.S_DIGITAL_RI_13_DIAGNOSAs.Where(Function(x) x.KDREG = entity.KDREG)

                Try
                    oConnection.db.S_DIGITAL_RI_13s.DeleteOnSubmit(ds)
                    oConnection.db.S_DIGITAL_RI_13s.InsertOnSubmit(entity)

                    If dsDetail IsNot Nothing Then
                        oConnection.db.S_DIGITAL_RI_13_DIAGNOSAs.DeleteAllOnSubmit(dsDetail)
                    End If

                    If entityDetail IsNot Nothing Then
                        oConnection.db.S_DIGITAL_RI_13_DIAGNOSAs.InsertAllOnSubmit(entityDetail)
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

                Dim ds = oConnection.db.S_DIGITAL_RI_13s.FirstOrDefault(Function(x) x.KDREG = Parameter)

                Try
                    oConnection.db.S_DIGITAL_RI_13s.DeleteOnSubmit(ds)
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