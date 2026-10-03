Imports System.Threading

Namespace Inventory
    Public Class clsReqCPPTACCPerawat
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
            sMODUL = "ACP"

            oCounter = New Setting.clsCounter
        End Sub
        Protected Overrides Sub Finalize()
            MyBase.Finalize()
            oConnection = Nothing
            GC.SuppressFinalize(Me)
        End Sub
        Public Function GetStructureHeader() As S_REQ_CPPT_ACCPERAWAT
            If Not oConnection.GetConnection() Then
                GetStructureHeader = Nothing
            End If
            GetStructureHeader = New S_REQ_CPPT_ACCPERAWAT
        End Function
        Public Function GetData() As List(Of S_REQ_CPPT_ACCPERAWAT)
            If Not oConnection.GetConnection() Then
                GetData = Nothing
                Exit Function
            End If
            GetData = oConnection.db.S_REQ_CPPT_ACCPERAWATs.OrderByDescending(Function(x) x.KDACC).ToList()
        End Function
        Public Function GetData(ByVal sKDACC As String) As S_REQ_CPPT_ACCPERAWAT
            If Not oConnection.GetConnection() Then
                GetData = Nothing
                Exit Function
            End If
            GetData = oConnection.db.S_REQ_CPPT_ACCPERAWATs.FirstOrDefault(Function(x) x.KDACC = sKDACC)
        End Function
        Public Function GetDataKDCPPT(ByVal sKDCPPT As String) As S_REQ_CPPT_ACCPERAWAT
            If Not oConnection.GetConnection() Then
                GetDataKDCPPT = Nothing
                Exit Function
            End If
            GetDataKDCPPT = oConnection.db.S_REQ_CPPT_ACCPERAWATs.FirstOrDefault(Function(x) x.KDCPPT = sKDCPPT)
        End Function
        Public Function InsertData(ByVal entity As S_REQ_CPPT_ACCPERAWAT) As Boolean
            Try
                If Not oConnection.GetConnection() Then
                    InsertData = False
                    Exit Function
                End If

                sREFERENCE = entity.KDACC
                sSTATUS = "INSERT"

                'Try
                '    sLASTNUMBER = oCounter.GetLastNumber(sMODUL, entity.DATECREATED)
                '    If sLASTNUMBER = 0 Then
                '        Try
                '            oCounter.InsertData(sMODUL, entity.DATECREATED)
                '            sLASTNUMBER = oCounter.GetLastNumber(sMODUL, entity.DATECREATED)
                '        Catch ex As Exception
                '            sLASTNUMBER = 0
                '        End Try
                '    End If

                '    entity.KDACC = AutoNumber(sMODUL, sLASTNUMBER + 1, entity.DATECREATED)

                'Catch ex As Exception
                '    oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                '    Throw ex
                'End Try

                Try
                    oConnection.db.S_REQ_CPPT_ACCPERAWATs.InsertOnSubmit(entity)
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
        Public Function UpdateData(ByVal entity As S_REQ_CPPT_ACCPERAWAT) As Boolean
            Try
                If Not oConnection.GetConnection() Then
                    UpdateData = False
                    Exit Function
                End If

                sREFERENCE = entity.KDACC
                sSTATUS = "UPDATE"

                Dim ds = oConnection.db.S_REQ_CPPT_ACCPERAWATs.FirstOrDefault(Function(x) x.KDACC = entity.KDACC)

                Try
                    oConnection.db.S_REQ_CPPT_ACCPERAWATs.DeleteOnSubmit(ds)
                    oConnection.db.S_REQ_CPPT_ACCPERAWATs.InsertOnSubmit(entity)
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
        Public Function DeleteData(ByVal sKDACC As String) As Boolean
            Try
                If Not oConnection.GetConnection() Then
                    DeleteData = False
                    Exit Function
                End If

                sREFERENCE = sKDACC
                sSTATUS = "DELETE"

                Dim ds = oConnection.db.S_REQ_CPPT_ACCPERAWATs.FirstOrDefault(Function(x) x.KDACC = sKDACC)

                Try
                    oConnection.db.S_REQ_CPPT_ACCPERAWATs.DeleteOnSubmit(ds)
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