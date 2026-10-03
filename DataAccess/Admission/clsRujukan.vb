Imports System.Threading

Namespace Admission
    Public Class clsRujukan
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
            sMODUL = "RUJUKAN_INTERNAL"

            oCounter = New Setting.clsCounter
        End Sub
        Protected Overrides Sub Finalize()
            MyBase.Finalize()
            oConnection = Nothing
            GC.SuppressFinalize(Me)
        End Sub
        Public Function GetStructureHeader() As S_PENDAFTARAN_RUJUKAN
            If Not oConnection.GetConnection() Then
                GetStructureHeader = Nothing
            End If
            GetStructureHeader = New S_PENDAFTARAN_RUJUKAN
        End Function
        Public Function GetStructureDetail1List() As List(Of S_PENDAFTARAN_RUJUKAN_D1)
            If Not oConnection.GetConnection() Then
                GetStructureDetail1List = Nothing
            End If
            GetStructureDetail1List = New List(Of S_PENDAFTARAN_RUJUKAN_D1)
        End Function
        Public Function GetStructureDetail1() As S_PENDAFTARAN_RUJUKAN_D1
            If Not oConnection.GetConnection() Then
                GetStructureDetail1 = Nothing
            End If
            GetStructureDetail1 = New S_PENDAFTARAN_RUJUKAN_D1
        End Function
        Public Function GetStructureDetail2() As S_PENDAFTARAN_RUJUKAN_D2
            If Not oConnection.GetConnection() Then
                GetStructureDetail2 = Nothing
            End If
            GetStructureDetail2 = New S_PENDAFTARAN_RUJUKAN_D2
        End Function
        Public Function GetStructureDetail2List() As List(Of S_PENDAFTARAN_RUJUKAN_D2)
            If Not oConnection.GetConnection() Then
                GetStructureDetail2List = Nothing
            End If
            GetStructureDetail2List = New List(Of S_PENDAFTARAN_RUJUKAN_D2)
        End Function
        Public Function GetData() As List(Of S_PENDAFTARAN_RUJUKAN)
            If Not oConnection.GetConnection() Then
                GetData = Nothing
                Exit Function
            End If
            GetData = oConnection.db.S_PENDAFTARAN_RUJUKANs.OrderByDescending(Function(x) x.KDRUJUKAN).ToList()
        End Function
        Public Function GetData(ByVal Parameter As String) As S_PENDAFTARAN_RUJUKAN
            If Not oConnection.GetConnection() Then
                GetData = Nothing
                Exit Function
            End If
            GetData = oConnection.db.S_PENDAFTARAN_RUJUKANs.FirstOrDefault(Function(x) x.KDRUJUKAN = Parameter)
        End Function
        Public Function GetDataDetail1() As List(Of S_PENDAFTARAN_RUJUKAN_D1)
            If Not oConnection.GetConnection() Then
                GetDataDetail1 = Nothing
                Exit Function
            End If
            GetDataDetail1 = oConnection.db.S_PENDAFTARAN_RUJUKAN_D1s.ToList()
        End Function
        Public Function GetDataDetail1(ByVal sKDLPK As String) As List(Of S_PENDAFTARAN_RUJUKAN_D1)
            If Not oConnection.GetConnection() Then
                GetDataDetail1 = Nothing
                Exit Function
            End If
            GetDataDetail1 = oConnection.db.S_PENDAFTARAN_RUJUKAN_D1s.Where(Function(x) x.KDRUJUKAN = sKDLPK).ToList()
        End Function
        Public Function GetDataDetail2() As List(Of S_PENDAFTARAN_RUJUKAN_D2)
            If Not oConnection.GetConnection() Then
                GetDataDetail2 = Nothing
                Exit Function
            End If
            GetDataDetail2 = oConnection.db.S_PENDAFTARAN_RUJUKAN_D2s.ToList()
        End Function
        Public Function GetDataDetail2(ByVal sKDLPK As String) As List(Of S_PENDAFTARAN_RUJUKAN_D2)
            If Not oConnection.GetConnection() Then
                GetDataDetail2 = Nothing
                Exit Function
            End If
            GetDataDetail2 = oConnection.db.S_PENDAFTARAN_RUJUKAN_D2s.Where(Function(x) x.KDRUJUKAN = sKDLPK).ToList()
        End Function
        Public Function InsertDataDetail(ByVal Parameter As String, ByVal entityDetail1 As List(Of S_PENDAFTARAN_RUJUKAN_D1), Optional entityDetail2 As List(Of S_PENDAFTARAN_RUJUKAN_D2) = Nothing) As String
            Try
                If Not oConnection.GetConnection() Then
                    InsertDataDetail = ""
                    Exit Function
                End If

                sREFERENCE = Parameter
                sSTATUS = "INSERT"

                Try
                    For Each iLoop In entityDetail1
                        iLoop.KDRUJUKAN = Parameter
                    Next
                    Try
                        oConnection.db.S_PENDAFTARAN_RUJUKAN_D1s.InsertAllOnSubmit(entityDetail1)
                        If entityDetail2 IsNot Nothing Then
                            For Each iLoop In entityDetail2
                                iLoop.KDRUJUKAN = Parameter
                            Next
                            oConnection.db.S_PENDAFTARAN_RUJUKAN_D2s.InsertAllOnSubmit(entityDetail2)
                        End If
                    Catch ex As Exception
                        oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                        Throw ex
                    End Try
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

                InsertDataDetail = Parameter
            Catch ex As Exception
                InsertDataDetail = ""
                oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                Throw ex
            End Try
        End Function
        Public Function UpdateDataDetail(ByVal Parameter As String, ByVal entityDetail1 As List(Of S_PENDAFTARAN_RUJUKAN_D1), Optional entityDetail2 As List(Of S_PENDAFTARAN_RUJUKAN_D2) = Nothing) As Boolean
            Try
                If Not oConnection.GetConnection() Then
                    UpdateDataDetail = False
                    Exit Function
                End If

                sREFERENCE = Parameter
                sSTATUS = "UPDATE"

                Dim dsDetail1 = oConnection.db.S_PENDAFTARAN_RUJUKAN_D1s.Where(Function(x) x.KDRUJUKAN = Parameter)
                Dim dsDetail2 = oConnection.db.S_PENDAFTARAN_RUJUKAN_D2s.Where(Function(x) x.KDRUJUKAN = Parameter)

                Try
                    oConnection.db.S_PENDAFTARAN_RUJUKAN_D1s.DeleteAllOnSubmit(dsDetail1)
                    oConnection.db.S_PENDAFTARAN_RUJUKAN_D1s.InsertAllOnSubmit(entityDetail1)
                    If dsDetail2 IsNot Nothing Then
                        oConnection.db.S_PENDAFTARAN_RUJUKAN_D2s.DeleteAllOnSubmit(dsDetail2)
                    End If
                    If entityDetail2 IsNot Nothing Then
                        oConnection.db.S_PENDAFTARAN_RUJUKAN_D2s.InsertAllOnSubmit(entityDetail2)
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

                UpdateDataDetail = True
            Catch ex As Exception
                UpdateDataDetail = False
                oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                Throw ex
            End Try
        End Function
        Public Function DeleteDataDetail(ByVal Parameter As String) As Boolean
            Try
                If Not oConnection.GetConnection() Then
                    DeleteDataDetail = False
                    Exit Function
                End If

                sREFERENCE = Parameter
                sSTATUS = "DELETE"

                Dim dsDetail1 = oConnection.db.S_PENDAFTARAN_RUJUKAN_D1s.Where(Function(x) x.KDRUJUKAN = Parameter)
                Dim dsDetail2 = oConnection.db.S_PENDAFTARAN_RUJUKAN_D2s.Where(Function(x) x.KDRUJUKAN = Parameter)

                Try
                    oConnection.db.S_PENDAFTARAN_RUJUKAN_D1s.DeleteAllOnSubmit(dsDetail1)
                    If dsDetail2.Count > 0 Then
                        oConnection.db.S_PENDAFTARAN_RUJUKAN_D2s.DeleteAllOnSubmit(dsDetail2)
                    End If
                    oConnection.db.SubmitChanges()
                Catch ex As Exception
                    oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                    Throw ex
                End Try

                DeleteDataDetail = True
            Catch ex As Exception
                DeleteDataDetail = False
                oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                Throw ex
            End Try
        End Function
        Public Function InsertData(ByVal entity As S_PENDAFTARAN_RUJUKAN) As String
            Try
                If Not oConnection.GetConnection() Then
                    InsertData = ""
                    Exit Function
                End If

                sREFERENCE = entity.KDRUJUKAN
                sSTATUS = "INSERT"
                Dim sKDRUJUKAN As String = String.Empty

                Try
                    If entity.KDRUJUKAN = String.Empty Then
                        sLASTNUMBER = oCounter.GetLastNumber(sMODUL, entity.DATE)
                        If sLASTNUMBER = 0 Then
                            Try
                                oCounter.InsertData(sMODUL, entity.DATE)
                                sLASTNUMBER = oCounter.GetLastNumber(sMODUL, entity.DATE)
                            Catch ex As Exception
                                sLASTNUMBER = 0
                            End Try
                        End If

                        entity.KDRUJUKAN = AutoNumber(sMODUL, sLASTNUMBER + 1, entity.DATE)

                        Try
                            oCounter.UpdateData(sMODUL, sLASTNUMBER + 1, Month(entity.DATE), Year(entity.DATE))
                        Catch ex As Exception
                            oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                            Throw ex
                        End Try
                    End If

                Catch ex As Exception
                    oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                    Throw ex
                End Try

                Try
                    oConnection.db.S_PENDAFTARAN_RUJUKANs.InsertOnSubmit(entity)
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

                InsertData = entity.KDRUJUKAN
            Catch ex As Exception
                InsertData = ""
                oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                Throw ex
            End Try
        End Function
        Public Function UpdateData(ByVal entity As S_PENDAFTARAN_RUJUKAN) As Boolean
            Try
                If Not oConnection.GetConnection() Then
                    UpdateData = False
                    Exit Function
                End If

                sREFERENCE = entity.KDRUJUKAN
                sSTATUS = "UPDATE"

                Dim ds = oConnection.db.S_PENDAFTARAN_RUJUKANs.FirstOrDefault(Function(x) x.KDRUJUKAN = entity.KDRUJUKAN)

                Try
                    oConnection.db.S_PENDAFTARAN_RUJUKANs.DeleteOnSubmit(ds)
                    oConnection.db.S_PENDAFTARAN_RUJUKANs.InsertOnSubmit(entity)

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

                Dim ds = oConnection.db.S_PENDAFTARAN_RUJUKANs.FirstOrDefault(Function(x) x.KDRUJUKAN.Contains(Parameter))

                If ds IsNot Nothing Then
                    Try
                        oConnection.db.S_PENDAFTARAN_RUJUKANs.DeleteOnSubmit(ds)
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
        'Public Function UpdateDataFix(ByVal isNew As Boolean) As Boolean
        '    Try
        '        If Not oConnection.GetConnection Then
        '            UpdateDataFix = False
        '            Exit Function
        '        End If


        '        Dim entity = oConnection.db.S_PENDAFTARAN_RUJUKANs

        '        For Each iLoop In entity
        '            Dim sKDRUJUKAN = iLoop.KDRUJUKAN
        '            Dim entityDetail = oConnection.db.F_PENDAFTARAN_Ds.Where(Function(x) x.KDRUJUKAN = sKDRUJUKAN And x.SEQ < 100)
        '            Dim entityDetail_R = oConnection.db.F_PENDAFTARAN_Ds.Where(Function(x) x.KDRUJUKAN = sKDRUJUKAN And x.SEQ >= 100)

        '            Try
        '                If Not AutoJournal(iLoop, isNew) Then
        '                    Return False
        '                End If
        '            Catch ex As Exception
        '                Throw ex
        '            End Try

        '            Thread.Sleep(100)
        '        Next

        '        UpdateDataFix = True
        '    Catch ex As Exception
        '        UpdateDataFix = False
        '        Throw ex
        '    End Try
        'End Function
    End Class
End Namespace