Imports System.Threading

Namespace Transaksi
    Public Class clsRekonsiliasiObat
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
            sMODUL = "RKO"

            oCounter = New Setting.clsCounter
        End Sub
        Protected Overrides Sub Finalize()
            MyBase.Finalize()
            oConnection = Nothing
            GC.SuppressFinalize(Me)
        End Sub
        Public Function GetStructureHeader() As S_DIGITAL_FARMASI_REKONSILIASIOBAT_H
            If Not oConnection.GetConnection() Then
                GetStructureHeader = Nothing
            End If
            GetStructureHeader = New S_DIGITAL_FARMASI_REKONSILIASIOBAT_H
        End Function
        Public Function GetStructureDetail1() As S_DIGITAL_FARMASI_REKONSILIASIOBAT_D1
            If Not oConnection.GetConnection() Then
                GetStructureDetail1 = Nothing
            End If
            GetStructureDetail1 = New S_DIGITAL_FARMASI_REKONSILIASIOBAT_D1
        End Function
        Public Function GetStructureDetail2() As S_DIGITAL_FARMASI_REKONSILIASIOBAT_D2
            If Not oConnection.GetConnection() Then
                GetStructureDetail2 = Nothing
            End If
            GetStructureDetail2 = New S_DIGITAL_FARMASI_REKONSILIASIOBAT_D2
        End Function
        Public Function GetStructureDetail1List() As List(Of S_DIGITAL_FARMASI_REKONSILIASIOBAT_D1)
            If Not oConnection.GetConnection() Then
                GetStructureDetail1List = Nothing
            End If
            GetStructureDetail1List = New List(Of S_DIGITAL_FARMASI_REKONSILIASIOBAT_D1)
        End Function
        Public Function GetStructureDetail2List() As List(Of S_DIGITAL_FARMASI_REKONSILIASIOBAT_D2)
            If Not oConnection.GetConnection() Then
                GetStructureDetail2List = Nothing
            End If
            GetStructureDetail2List = New List(Of S_DIGITAL_FARMASI_REKONSILIASIOBAT_D2)
        End Function
        Public Function GetData() As List(Of S_DIGITAL_FARMASI_REKONSILIASIOBAT_H)
            If Not oConnection.GetConnection() Then
                GetData = Nothing
                Exit Function
            End If
            GetData = oConnection.db.S_DIGITAL_FARMASI_REKONSILIASIOBAT_Hs.OrderByDescending(Function(x) x.KDREKONSILIASI).ToList()
        End Function
        Public Function GetData(ByVal Parameter As String) As S_DIGITAL_FARMASI_REKONSILIASIOBAT_H
            If Not oConnection.GetConnection() Then
                GetData = Nothing
                Exit Function
            End If
            GetData = oConnection.db.S_DIGITAL_FARMASI_REKONSILIASIOBAT_Hs.FirstOrDefault(Function(x) x.KDREKONSILIASI = Parameter)
        End Function
        Public Function GetDataDetail1() As List(Of S_DIGITAL_FARMASI_REKONSILIASIOBAT_D1)
            If Not oConnection.GetConnection() Then
                GetDataDetail1 = Nothing
                Exit Function
            End If
            GetDataDetail1 = oConnection.db.S_DIGITAL_FARMASI_REKONSILIASIOBAT_D1s.OrderBy(Function(X) X.SEQ).ToList()
        End Function
        Public Function GetDataDetail2() As List(Of S_DIGITAL_FARMASI_REKONSILIASIOBAT_D2)
            If Not oConnection.GetConnection() Then
                GetDataDetail2 = Nothing
                Exit Function
            End If
            GetDataDetail2 = oConnection.db.S_DIGITAL_FARMASI_REKONSILIASIOBAT_D2s.OrderBy(Function(X) X.SEQ).ToList()
        End Function
        Public Function GetDataDetail1(ByVal sKDREKONSILIASI As String) As List(Of S_DIGITAL_FARMASI_REKONSILIASIOBAT_D1)
            If Not oConnection.GetConnection() Then
                GetDataDetail1 = Nothing
                Exit Function
            End If
            GetDataDetail1 = oConnection.db.S_DIGITAL_FARMASI_REKONSILIASIOBAT_D1s.Where(Function(X) X.KDREKONSILIASI = sKDREKONSILIASI).OrderBy(Function(X) X.SEQ).ToList()
        End Function
        Public Function GetDataDetail2(ByVal sKDREKONSILIASI As String) As List(Of S_DIGITAL_FARMASI_REKONSILIASIOBAT_D2)
            If Not oConnection.GetConnection() Then
                GetDataDetail2 = Nothing
                Exit Function
            End If
            GetDataDetail2 = oConnection.db.S_DIGITAL_FARMASI_REKONSILIASIOBAT_D2s.Where(Function(x) x.KDREKONSILIASI = sKDREKONSILIASI).ToList()
        End Function
        Public Function GetDataByRM(ByVal kdcustomer As String) As List(Of S_DIGITAL_FARMASI_REKONSILIASIOBAT_H)
            If Not oConnection.GetConnection() Then
                GetDataByRM = Nothing
                Exit Function
            End If
            GetDataByRM = oConnection.db.S_DIGITAL_FARMASI_REKONSILIASIOBAT_Hs.Where(Function(x) x.KDCUSTOMER = kdcustomer).OrderByDescending(Function(x) x.KDREKONSILIASI).ToList()
        End Function
        Public Function InsertData(ByVal entity As S_DIGITAL_FARMASI_REKONSILIASIOBAT_H, ByVal entityDetail1 As List(Of S_DIGITAL_FARMASI_REKONSILIASIOBAT_D1), ByVal entityDetail2 As List(Of S_DIGITAL_FARMASI_REKONSILIASIOBAT_D2)) As Boolean
            Try
                If Not oConnection.GetConnection() Then
                    InsertData = False
                    Exit Function
                End If

                sREFERENCE = entity.KDREKONSILIASI
                sSTATUS = "INSERT"

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

                    entity.KDREKONSILIASI = AutoNumber(sMODUL, sLASTNUMBER + 1, entity.DATE)

                    For Each iLoop In entityDetail1
                        iLoop.KDREKONSILIASI = entity.KDREKONSILIASI
                    Next
                    For Each iLoop In entityDetail2
                        iLoop.KDREKONSILIASI = entity.KDREKONSILIASI
                    Next
                Catch ex As Exception
                    oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                    Throw ex
                End Try
                Try
                    oConnection.db.S_DIGITAL_FARMASI_REKONSILIASIOBAT_Hs.InsertOnSubmit(entity)
                    If entityDetail1.Count > 0 Then
                        oConnection.db.S_DIGITAL_FARMASI_REKONSILIASIOBAT_D1s.InsertAllOnSubmit(entityDetail1)
                    End If
                    If entityDetail2.Count > 0 Then
                        oConnection.db.S_DIGITAL_FARMASI_REKONSILIASIOBAT_D2s.InsertAllOnSubmit(entityDetail2)
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
        Public Function UpdateData(ByVal entity As S_DIGITAL_FARMASI_REKONSILIASIOBAT_H, ByVal entityDetail1 As List(Of S_DIGITAL_FARMASI_REKONSILIASIOBAT_D1), ByVal entityDetail2 As List(Of S_DIGITAL_FARMASI_REKONSILIASIOBAT_D2)) As Boolean
            Try
                If Not oConnection.GetConnection() Then
                    UpdateData = False
                    Exit Function
                End If

                sREFERENCE = entity.KDREKONSILIASI
                sSTATUS = "UPDATE"

                Dim ds = oConnection.db.S_DIGITAL_FARMASI_REKONSILIASIOBAT_Hs.FirstOrDefault(Function(x) x.KDREKONSILIASI = entity.KDREKONSILIASI)
                Dim dsDetail1 = oConnection.db.S_DIGITAL_FARMASI_REKONSILIASIOBAT_D1s.Where(Function(x) x.KDREKONSILIASI = entity.KDREKONSILIASI)
                Dim dsDetail2 = oConnection.db.S_DIGITAL_FARMASI_REKONSILIASIOBAT_D2s.Where(Function(x) x.KDREKONSILIASI = entity.KDREKONSILIASI)

                Try
                    oConnection.db.S_DIGITAL_FARMASI_REKONSILIASIOBAT_Hs.DeleteOnSubmit(ds)
                    oConnection.db.S_DIGITAL_FARMASI_REKONSILIASIOBAT_Hs.InsertOnSubmit(entity)

                    If dsDetail1.Count > 0 Then
                        oConnection.db.S_DIGITAL_FARMASI_REKONSILIASIOBAT_D1s.DeleteAllOnSubmit(dsDetail1)
                    End If
                    If entityDetail1.Count > 0 Then
                        oConnection.db.S_DIGITAL_FARMASI_REKONSILIASIOBAT_D1s.InsertAllOnSubmit(entityDetail1)
                    End If
                    If dsDetail2.Count > 0 Then
                        oConnection.db.S_DIGITAL_FARMASI_REKONSILIASIOBAT_D2s.DeleteAllOnSubmit(dsDetail2)
                    End If
                    If entityDetail2.Count > 0 Then
                        oConnection.db.S_DIGITAL_FARMASI_REKONSILIASIOBAT_D2s.InsertAllOnSubmit(entityDetail2)
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

                Dim ds = oConnection.db.S_DIGITAL_FARMASI_REKONSILIASIOBAT_Hs.FirstOrDefault(Function(x) x.KDREKONSILIASI = Parameter)
                Dim dsDetail1 = oConnection.db.S_DIGITAL_FARMASI_REKONSILIASIOBAT_D1s.Where(Function(x) x.KDREKONSILIASI = Parameter)
                Dim dsDetail2 = oConnection.db.S_DIGITAL_FARMASI_REKONSILIASIOBAT_D2s.Where(Function(x) x.KDREKONSILIASI = Parameter)

                Try
                    oConnection.db.S_DIGITAL_FARMASI_REKONSILIASIOBAT_Hs.DeleteOnSubmit(ds)
                    If dsDetail1.Count > 0 Then
                        oConnection.db.S_DIGITAL_FARMASI_REKONSILIASIOBAT_D1s.DeleteAllOnSubmit(dsDetail1)
                    End If
                    If dsDetail2.Count > 0 Then
                        oConnection.db.S_DIGITAL_FARMASI_REKONSILIASIOBAT_D2s.DeleteAllOnSubmit(dsDetail2)
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
    End Class
End Namespace