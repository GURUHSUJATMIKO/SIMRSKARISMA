Imports System.Threading

Namespace Billing
    Public Class clsBillingPoli
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
        Public Function GetStructureHeader() As S_BILLING_POLI_H
            If Not oConnection.GetConnection() Then
                GetStructureHeader = Nothing
            End If
            GetStructureHeader = New S_BILLING_POLI_H
        End Function
        Public Function GetStructureDetail() As S_BILLING_POLI_D
            If Not oConnection.GetConnection() Then
                GetStructureDetail = Nothing
            End If
            GetStructureDetail = New S_BILLING_POLI_D
        End Function
        Public Function GetStructureDetailList() As List(Of S_BILLING_POLI_D)
            If Not oConnection.GetConnection() Then
                GetStructureDetailList = Nothing
            End If
            GetStructureDetailList = New List(Of S_BILLING_POLI_D)
        End Function
        Public Function GetData() As List(Of S_BILLING_POLI_H)
            If Not oConnection.GetConnection() Then
                GetData = Nothing
                Exit Function
            End If
            GetData = oConnection.db.S_BILLING_POLI_Hs.OrderByDescending(Function(x) x.KDBILLING_POLI).ToList()
        End Function
        Public Function GetData(ByVal Parameter As String) As S_BILLING_POLI_H
            If Not oConnection.GetConnection() Then
                GetData = Nothing
                Exit Function
            End If
            GetData = oConnection.db.S_BILLING_POLI_Hs.FirstOrDefault(Function(x) x.KDBILLING_POLI = Parameter)
        End Function
        Public Function GetDataDetail() As List(Of S_BILLING_POLI_D)
            If Not oConnection.GetConnection() Then
                GetDataDetail = Nothing
                Exit Function
            End If
            GetDataDetail = oConnection.db.S_BILLING_POLI_Ds.ToList()
        End Function
        Public Function GetDataDetail(ByVal sKDBILLING_POLI As String) As List(Of S_BILLING_POLI_D)
            If Not oConnection.GetConnection() Then
                GetDataDetail = Nothing
                Exit Function
            End If
            GetDataDetail = oConnection.db.S_BILLING_POLI_Ds.Where(Function(x) x.KDBILLING_POLI = sKDBILLING_POLI).ToList()
        End Function
        Public Function GetDataBykdregpenjamin(ByVal Parameter1 As String, ByVal Parameter2 As String) As List(Of S_BILLING_POLI_H)
            If Not oConnection.GetConnection() Then
                GetDataBykdregpenjamin = Nothing
                Exit Function
            End If
            GetDataBykdregpenjamin = oConnection.db.S_BILLING_POLI_Hs.Where(Function(x) x.KDPENDAFTARAN = Parameter1 And x.KDPENJAMIN = Parameter2 And x.PAYAMOUNT = 0).ToList()
        End Function
        Public Function GetDataByRekamMedis(ByVal Parameter As String) As List(Of S_BILLING_POLI_H)
            If Not oConnection.GetConnection() Then
                GetDataByRekamMedis = Nothing
                Exit Function
            End If
            GetDataByRekamMedis = oConnection.db.S_BILLING_POLI_Hs.Where(Function(x) x.S_PENDAFTARAN_H.KDCUSTOMER = Parameter And x.S_PENDAFTARAN_H.KDPENDAFTARAN_AWAL = "").ToList()
        End Function
        Public Function GetDataByRekamNama(ByVal Parameter As String) As List(Of S_BILLING_POLI_H)
            If Not oConnection.GetConnection() Then
                GetDataByRekamNama = Nothing
                Exit Function
            End If
            GetDataByRekamNama = oConnection.db.S_BILLING_POLI_Hs.Where(Function(x) x.S_PENDAFTARAN_H.M_CUSTOMER.NAME_DISPLAY.Contains(Parameter) And x.S_PENDAFTARAN_H.KDPENDAFTARAN_AWAL = "").ToList()
        End Function
        Public Function InsertData(ByVal entity As S_BILLING_POLI_H, ByVal entityDetail As List(Of S_BILLING_POLI_D)) As Boolean
            Try
                If Not oConnection.GetConnection() Then
                    InsertData = False
                    Exit Function
                End If

                sREFERENCE = entity.KDBILLING_POLI
                sSTATUS = "INSERT"

                If entity.CATEGORY = 0 Then
                    sMODUL = sMODUL & "RJ"
                ElseIf entity.CATEGORY = 1 Then
                    sMODUL = sMODUL & "RI"
                ElseIf entity.CATEGORY = 2 Then
                    sMODUL = sMODUL & "LB"
                ElseIf entity.CATEGORY = 3 Then
                    sMODUL = sMODUL & "LB"
                ElseIf entity.CATEGORY = 4 Then
                    sMODUL = sMODUL & "RD"
                ElseIf entity.CATEGORY = 5 Then
                    sMODUL = sMODUL & "RD"
                End If

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

                    entity.KDBILLING_POLI = AutoNumber(sMODUL, sLASTNUMBER + 1, entity.DATE)

                    For Each iLoop In entityDetail
                        iLoop.KDBILLING_POLI = entity.KDBILLING_POLI
                    Next

                Catch ex As Exception
                    oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                    Throw ex
                End Try

                Try

                    oConnection.db.S_BILLING_POLI_Hs.InsertOnSubmit(entity)
                    oConnection.db.S_BILLING_POLI_Ds.InsertAllOnSubmit(entityDetail)
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
        Public Function UpdateData(ByVal entity As S_BILLING_POLI_H, ByVal entityDetail As List(Of S_BILLING_POLI_D)) As Boolean
            Try
                If Not oConnection.GetConnection() Then
                    UpdateData = False
                    Exit Function
                End If

                sREFERENCE = entity.KDBILLING_POLI
                sSTATUS = "UPDATE"

                Dim ds = oConnection.db.S_BILLING_POLI_Hs.FirstOrDefault(Function(x) x.KDBILLING_POLI = entity.KDBILLING_POLI)
                Dim dsDetail = oConnection.db.S_BILLING_POLI_Ds.Where(Function(x) x.KDBILLING_POLI = entity.KDBILLING_POLI)

                Try
                    oConnection.db.S_BILLING_POLI_Hs.DeleteOnSubmit(ds)
                    oConnection.db.S_BILLING_POLI_Hs.InsertOnSubmit(entity)
                    oConnection.db.S_BILLING_POLI_Ds.DeleteAllOnSubmit(dsDetail)
                    oConnection.db.S_BILLING_POLI_Ds.InsertAllOnSubmit(entityDetail)
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

                Dim ds = oConnection.db.S_BILLING_POLI_Hs.FirstOrDefault(Function(x) x.KDBILLING_POLI = Parameter)
                Dim dsDetail = oConnection.db.S_BILLING_POLI_Ds.Where(Function(x) x.KDBILLING_POLI = Parameter)

                Try
                    oConnection.db.S_BILLING_POLI_Hs.DeleteOnSubmit(ds)
                    oConnection.db.S_BILLING_POLI_Ds.DeleteAllOnSubmit(dsDetail)
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