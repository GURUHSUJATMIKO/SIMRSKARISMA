Imports System.Threading

Namespace Transaksi
    Public Class clsRadiologi
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
            sMODUL = "T"

            oCounter = New Setting.clsCounter
        End Sub
        Protected Overrides Sub Finalize()
            MyBase.Finalize()
            oConnection = Nothing
            GC.SuppressFinalize(Me)
        End Sub
        Public Function GetStructureHeader() As S_RADIOLOGI_H
            If Not oConnection.GetConnection() Then
                GetStructureHeader = Nothing
            End If
            GetStructureHeader = New S_RADIOLOGI_H
        End Function
        Public Function GetData() As List(Of S_RADIOLOGI_H)
            If Not oConnection.GetConnection() Then
                GetData = Nothing
                Exit Function
            End If
            GetData = oConnection.db.S_RADIOLOGI_Hs.OrderByDescending(Function(x) x.KDRAD).ToList()
        End Function
        Public Function GetData(ByVal Parameter As Integer) As S_RADIOLOGI_H
            If Not oConnection.GetConnection() Then
                GetData = Nothing
                Exit Function
            End If
            GetData = oConnection.db.S_RADIOLOGI_Hs.FirstOrDefault(Function(x) x.KDRAD = Parameter)
        End Function
        Public Function GetData(ByVal sDateFrom As DateTime, ByVal sDateTo As Date) As List(Of S_RADIOLOGI_H)
            If Not oConnection.GetConnection() Then
                GetData = Nothing
                Exit Function
            End If
            GetData = oConnection.db.S_RADIOLOGI_Hs.Where(Function(x) x.DATE >= sDateFrom.ToString("yyyy-MM-dd") & " 00:00:00" And x.DATE <= sDateTo.ToString("yyyy-MM-dd") & " 23:59:59").OrderByDescending(Function(x) x.KDRAD).ToList()
        End Function
        Public Function InsertData(ByVal entity As S_RADIOLOGI_H) As Boolean
            Try
                If Not oConnection.GetConnection() Then
                    InsertData = False
                    Exit Function
                End If

                sREFERENCE = entity.KDRAD
                sSTATUS = "INSERT"

                Try
                    oConnection.db.S_RADIOLOGI_Hs.InsertOnSubmit(entity)
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
        Public Function UpdateData(ByVal entity As S_RADIOLOGI_H) As Boolean
            Try
                If Not oConnection.GetConnection() Then
                    UpdateData = False
                    Exit Function
                End If

                sREFERENCE = entity.KDRAD
                sSTATUS = "UPDATE"

                Dim ds = oConnection.db.S_RADIOLOGI_Hs.FirstOrDefault(Function(x) x.KDRAD = entity.KDRAD)

                Try
                    oConnection.db.S_RADIOLOGI_Hs.DeleteOnSubmit(ds)
                    oConnection.db.S_RADIOLOGI_Hs.InsertOnSubmit(entity)
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

                Dim ds = oConnection.db.S_RADIOLOGI_Hs.FirstOrDefault(Function(x) x.KDRAD = Parameter)

                Try
                    oConnection.db.S_RADIOLOGI_Hs.DeleteOnSubmit(ds)
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