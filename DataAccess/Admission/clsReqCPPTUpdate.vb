Imports System.Threading
Imports System.Data.SqlClient

Namespace Admission
    Public Class clsReqCPPTUpdate
        Public oConnection As Setting.clsConnectionAdmision = Nothing
        Public oError As Setting.clsError = Nothing
        Public sMODUL As String = ""
        Public sREFERENCE As String = ""
        Public sSTATUS As String = ""
        Public sLASTNUMBER As Integer = 0

        Public oCounter As Setting.clsCounter = Nothing

        Public Sub New()
            oConnection = New Setting.clsConnectionAdmision
            oError = New Setting.clsError
            sMODUL = "UPDATECPPT"

            oCounter = New Setting.clsCounter
        End Sub
        Protected Overrides Sub Finalize()
            MyBase.Finalize()
            oConnection = Nothing
            GC.SuppressFinalize(Me)
        End Sub
        Public Function GetStructureHeader() As S_REQ_CPPT_UPDATE
            If Not oConnection.GetConnection() Then
                GetStructureHeader = Nothing
            End If
            GetStructureHeader = New S_REQ_CPPT_UPDATE
        End Function
        Public Function GetData() As List(Of S_REQ_CPPT_UPDATE)
            If Not oConnection.GetConnection() Then
                GetData = Nothing
                Exit Function
            End If
            GetData = oConnection.db.S_REQ_CPPT_UPDATEs.OrderByDescending(Function(x) x.KDREG).ToList()
        End Function
        Public Function GetData(ByVal Parameter As String) As S_REQ_CPPT_UPDATE
            If Not oConnection.GetConnection() Then
                GetData = Nothing
                Exit Function
            End If
            GetData = oConnection.db.S_REQ_CPPT_UPDATEs.FirstOrDefault(Function(x) x.KDREG = Parameter)
        End Function
        Public Function InsertData(ByVal entity As S_REQ_CPPT_UPDATE) As Boolean
            Try
                If Not oConnection.GetConnection() Then
                    InsertData = False
                    Exit Function
                End If

                sREFERENCE = entity.KDREG
                sSTATUS = "INSERT"

                Try
                    oConnection.db.S_REQ_CPPT_UPDATEs.InsertOnSubmit(entity)
                Catch ex As Exception
                    'oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                    Throw ex
                End Try

                Try
                    oConnection.db.SubmitChanges()
                Catch ex As Exception
                    'oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                    Throw ex
                End Try

                InsertData = True

            Catch ex As Exception
                InsertData = False
                'oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                Throw ex
            End Try
        End Function
        'Public Function UpdateData(ByVal entity As S_REQ_CPPT_UPDATE) As Boolean
        '    Try
        '        If Not oConnection.GetConnection() Then
        '            UpdateData = False
        '            Exit Function
        '        End If

        '        sREFERENCE = entity.KDREG
        '        sSTATUS = "UPDATE"

        '        Dim ds = oConnection.db.S_REQ_CPPT_UPDATEs.FirstOrDefault(Function(x) x.KDREG = entity.KDREG)

        '        Try
        '            oConnection.db.S_REQ_CPPT_UPDATEs.DeleteOnSubmit(ds)
        '            oConnection.db.S_REQ_CPPT_UPDATEs.InsertOnSubmit(entity)

        '        Catch ex As Exception
        '            'oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
        '            Throw ex
        '        End Try

        '        Try
        '            oConnection.db.SubmitChanges()
        '        Catch ex As Exception
        '            'oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
        '            Throw ex
        '        End Try

        '        UpdateData = True
        '    Catch ex As Exception
        '        UpdateData = False
        '        'oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
        '        Throw ex
        '    End Try
        'End Function
        Public Function DeleteData(ByVal Parameter As String) As Boolean
            Try
                If Not oConnection.GetConnection() Then
                    DeleteData = False
                    Exit Function
                End If

                sREFERENCE = Parameter
                sSTATUS = "DELETE"

                Dim ds = oConnection.db.S_REQ_CPPT_UPDATEs.FirstOrDefault(Function(x) x.KDREG = Parameter)

                If ds IsNot Nothing Then
                    Try
                        oConnection.db.S_REQ_CPPT_UPDATEs.DeleteOnSubmit(ds)
                        oConnection.db.SubmitChanges()
                    Catch ex As Exception
                        'oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                        Throw ex
                    End Try

                End If

                DeleteData = True
            Catch ex As Exception
                DeleteData = False
                'oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                Throw ex
            End Try
        End Function
    End Class
End Namespace