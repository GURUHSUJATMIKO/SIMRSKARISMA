Imports System.Threading

Namespace Transaksi
    Public Class clsDigital_DischargePlanningTemplate
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
        Public Function GetStructureHeader() As S_DIGITAL_RI_13_TEMPLATE
            If Not oConnection.GetConnection() Then
                GetStructureHeader = Nothing
            End If
            GetStructureHeader = New S_DIGITAL_RI_13_TEMPLATE
        End Function
        Public Function GetData() As List(Of S_DIGITAL_RI_13_TEMPLATE)
            If Not oConnection.GetConnection() Then
                GetData = Nothing
                Exit Function
            End If
            GetData = oConnection.db.S_DIGITAL_RI_13_TEMPLATEs.OrderByDescending(Function(x) x.DATECREATED).ToList()
        End Function
        Public Function GetData(ByVal Parameter As String) As S_DIGITAL_RI_13_TEMPLATE
            If Not oConnection.GetConnection() Then
                GetData = Nothing
                Exit Function
            End If
            GetData = oConnection.db.S_DIGITAL_RI_13_TEMPLATEs.FirstOrDefault(Function(x) x.KDJUDUL = Parameter)
        End Function
        Public Function InsertData(ByVal entity As S_DIGITAL_RI_13_TEMPLATE) As String
            Try
                If Not oConnection.GetConnection() Then
                    InsertData = ""
                    Exit Function
                End If

                sREFERENCE = entity.KDJUDUL
                sSTATUS = "INSERT"

                Try
                    oConnection.db.S_DIGITAL_RI_13_TEMPLATEs.InsertOnSubmit(entity)
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

                InsertData = entity.KDJUDUL
            Catch ex As Exception
                InsertData = ""
                oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                Throw ex
            End Try
        End Function
        Public Function UpdateData(ByVal entity As S_DIGITAL_RI_13_TEMPLATE) As Boolean
            Try
                If Not oConnection.GetConnection() Then
                    UpdateData = False
                    Exit Function
                End If

                sREFERENCE = entity.KDJUDUL
                sSTATUS = "UPDATE"

                Dim ds = oConnection.db.S_DIGITAL_RI_13_TEMPLATEs.FirstOrDefault(Function(x) x.KDJUDUL = entity.KDJUDUL)

                Try
                    oConnection.db.S_DIGITAL_RI_13_TEMPLATEs.DeleteOnSubmit(ds)
                    oConnection.db.S_DIGITAL_RI_13_TEMPLATEs.InsertOnSubmit(entity)
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

                Dim ds = oConnection.db.S_DIGITAL_RI_13_TEMPLATEs.FirstOrDefault(Function(x) x.KDJUDUL = Parameter)

                Try
                    oConnection.db.S_DIGITAL_RI_13_TEMPLATEs.DeleteOnSubmit(ds)
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
                'Finally
                '    oConnection.db.Dispose()
            End Try
        End Function
    End Class
End Namespace