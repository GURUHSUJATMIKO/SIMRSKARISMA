Imports System.Threading

Namespace Admission
    Public Class clsTracking
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
            sMODUL = "TRACKING"

            oCounter = New Setting.clsCounter
        End Sub
        Protected Overrides Sub Finalize()
            MyBase.Finalize()
            oConnection = Nothing
            GC.SuppressFinalize(Me)
        End Sub
        Public Function GetStructureHeader() As S_TRACKING
            If Not oConnection.GetConnection() Then
                GetStructureHeader = Nothing
            End If
            GetStructureHeader = New S_TRACKING
        End Function
        Public Function GetData() As List(Of S_TRACKING)
            If Not oConnection.GetConnection() Then
                GetData = Nothing
                Exit Function
            End If
            GetData = oConnection.db.S_TRACKINGs.OrderByDescending(Function(x) x.KDTRACKING).ToList()
        End Function
        Public Function GetData(ByVal sDateFrom As DateTime, ByVal sDateTo As Date) As List(Of S_TRACKING)
            If Not oConnection.GetConnection() Then
                GetData = Nothing
                Exit Function
            End If
            GetData = oConnection.db.S_TRACKINGs.Where(Function(x) x.DATE_KIRIM >= sDateFrom.ToString("yyyy-MM-dd") & " 00:00:00" And x.DATE_KIRIM <= sDateTo.ToString("yyyy-MM-dd") & " 23:59:59").OrderByDescending(Function(x) x.KDTRACKING).ToList()
        End Function
        Public Function GetData(ByVal Parameter As String) As S_TRACKING
            If Not oConnection.GetConnection() Then
                GetData = Nothing
                Exit Function
            End If
            GetData = oConnection.db.S_TRACKINGs.FirstOrDefault(Function(x) x.KDTRACKING = Parameter)
        End Function
        Public Function GetDataOrderByDesc(ByVal Parameter As String) As S_TRACKING
            If Not oConnection.GetConnection() Then
                GetDataOrderByDesc = Nothing
                Exit Function
            End If
            GetDataOrderByDesc = oConnection.db.S_TRACKINGs.Where(Function(x) x.KDCUSTOMER = Parameter).OrderByDescending(Function(x) x.DATE_KIRIM).FirstOrDefault()
        End Function
        Public Function InsertData(ByVal entity As S_TRACKING) As Boolean
            Try
                If Not oConnection.GetConnection() Then
                    InsertData = False
                    Exit Function
                End If

                sREFERENCE = entity.KDTRACKING
                sSTATUS = "INSERT"

                sMODUL = sMODUL & Day(entity.DATE_KIRIM) & Month(entity.DATE_KIRIM) & Year(entity.DATE_KIRIM)

                Try
                    sLASTNUMBER = oCounter.GetLastNumberdDay(sMODUL, entity.DATE_KIRIM)
                    If sLASTNUMBER = 0 Then
                        Try
                            oCounter.InsertData(sMODUL, entity.DATE_KIRIM)
                            sLASTNUMBER = oCounter.GetLastNumberdDay(sMODUL, entity.DATE_KIRIM)
                        Catch ex As Exception
                            sLASTNUMBER = 0
                        End Try
                    End If

                    sLASTNUMBER = sLASTNUMBER + 1
                    entity.KDTRACKING = sMODUL & sLASTNUMBER.ToString.PadLeft(3, "0")

                Catch ex As Exception
                    oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                    Throw ex
                End Try

                Try
                    oConnection.db.S_TRACKINGs.InsertOnSubmit(entity)
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
                    oCounter.UpdateData(sMODUL, sLASTNUMBER + 1, Day(entity.DATE_KIRIM), Month(entity.DATE_KIRIM), Year(entity.DATE_KIRIM))
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
        Public Function UpdateData(ByVal entity As S_TRACKING) As Boolean
            Try
                If Not oConnection.GetConnection() Then
                    UpdateData = False
                    Exit Function
                End If

                sREFERENCE = entity.KDTRACKING
                sSTATUS = "UPDATE"

                Dim ds = oConnection.db.S_TRACKINGs.FirstOrDefault(Function(x) x.KDTRACKING = entity.KDTRACKING)

                Try
                    oConnection.db.S_TRACKINGs.DeleteOnSubmit(ds)
                    oConnection.db.S_TRACKINGs.InsertOnSubmit(entity)

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

                Dim ds = oConnection.db.S_TRACKINGs.FirstOrDefault(Function(x) x.KDTRACKING.Contains(Parameter))

                If ds IsNot Nothing Then
                    Try
                        oConnection.db.S_TRACKINGs.DeleteOnSubmit(ds)
                    Catch ex As Exception
                        oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                        Throw ex
                    End Try

                End If

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
        Public Function UpdateIsChekedKirim(ByVal sKDTRACKING As String, ByVal sDATE_KIRIM As DateTime) As Boolean
            Try
                If Not oConnection.GetConnection Then
                    UpdateIsChekedKirim = False
                    Exit Function
                End If

                UpdateIsChekedKirim = True

                Dim ds = oConnection.db.S_TRACKINGs.FirstOrDefault(Function(x) x.KDTRACKING = sKDTRACKING)

                ds.DATE_KIRIM = sDATE_KIRIM
                ds.ISCHEKED = True
                ds.STATUS = "KIRIM"

                oConnection.db.SubmitChanges()
            Catch ex As Exception
                UpdateIsChekedKirim = False
                Throw ex
            End Try
        End Function
        Public Function UpdateIsChekedKembali(ByVal sKDTRACKING As String, ByVal sDATE_KEMBALI As DateTime) As Boolean
            Try
                If Not oConnection.GetConnection Then
                    UpdateIsChekedKembali = False
                    Exit Function
                End If

                UpdateIsChekedKembali = True

                Dim ds = oConnection.db.S_TRACKINGs.FirstOrDefault(Function(x) x.KDTRACKING = sKDTRACKING)

                ds.DATE_KEMBALI = sDATE_KEMBALI
                ds.STATUS = "KEMBALI"

                oConnection.db.SubmitChanges()
            Catch ex As Exception
                UpdateIsChekedKembali = False
                Throw ex
            End Try
        End Function
    End Class
End Namespace