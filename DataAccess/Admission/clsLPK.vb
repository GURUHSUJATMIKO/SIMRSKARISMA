Imports System.Threading

Namespace Admission
    Public Class clsLPK
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
            sMODUL = "LPK"

            oCounter = New Setting.clsCounter
        End Sub
        Protected Overrides Sub Finalize()
            MyBase.Finalize()
            oConnection = Nothing
            GC.SuppressFinalize(Me)
        End Sub
        Public Function GetStructureHeader() As S_PENDAFTARAN_LPK
            If Not oConnection.GetConnection() Then
                GetStructureHeader = Nothing
            End If
            GetStructureHeader = New S_PENDAFTARAN_LPK
        End Function
        Public Function GetStructureDetail1List() As List(Of S_PENDAFTARAN_LPK_D1)
            If Not oConnection.GetConnection() Then
                GetStructureDetail1List = Nothing
            End If
            GetStructureDetail1List = New List(Of S_PENDAFTARAN_LPK_D1)
        End Function
        Public Function GetStructureDetail1() As S_PENDAFTARAN_LPK_D1
            If Not oConnection.GetConnection() Then
                GetStructureDetail1 = Nothing
            End If
            GetStructureDetail1 = New S_PENDAFTARAN_LPK_D1
        End Function
        Public Function GetStructureDetail2() As S_PENDAFTARAN_LPK_D2
            If Not oConnection.GetConnection() Then
                GetStructureDetail2 = Nothing
            End If
            GetStructureDetail2 = New S_PENDAFTARAN_LPK_D2
        End Function
        Public Function GetStructureDetail2List() As List(Of S_PENDAFTARAN_LPK_D2)
            If Not oConnection.GetConnection() Then
                GetStructureDetail2List = Nothing
            End If
            GetStructureDetail2List = New List(Of S_PENDAFTARAN_LPK_D2)
        End Function
        Public Function GetData() As List(Of S_PENDAFTARAN_LPK)
            If Not oConnection.GetConnection() Then
                GetData = Nothing
                Exit Function
            End If
            GetData = oConnection.db.S_PENDAFTARAN_LPKs.OrderByDescending(Function(x) x.KDLPK).ToList()
        End Function
        Public Function GetData(ByVal Parameter As String) As S_PENDAFTARAN_LPK
            If Not oConnection.GetConnection() Then
                GetData = Nothing
                Exit Function
            End If
            GetData = oConnection.db.S_PENDAFTARAN_LPKs.FirstOrDefault(Function(x) x.KDLPK = Parameter)
        End Function
        Public Function GetDataDetail1() As List(Of S_PENDAFTARAN_LPK_D1)
            If Not oConnection.GetConnection() Then
                GetDataDetail1 = Nothing
                Exit Function
            End If
            GetDataDetail1 = oConnection.db.S_PENDAFTARAN_LPK_D1s.ToList()
        End Function
        Public Function GetDataDetail1(ByVal sKDLPK As String) As List(Of S_PENDAFTARAN_LPK_D1)
            If Not oConnection.GetConnection() Then
                GetDataDetail1 = Nothing
                Exit Function
            End If
            GetDataDetail1 = oConnection.db.S_PENDAFTARAN_LPK_D1s.Where(Function(x) x.KDLPK = sKDLPK).ToList()
        End Function
        Public Function GetDataDetail2() As List(Of S_PENDAFTARAN_LPK_D2)
            If Not oConnection.GetConnection() Then
                GetDataDetail2 = Nothing
                Exit Function
            End If
            GetDataDetail2 = oConnection.db.S_PENDAFTARAN_LPK_D2s.ToList()
        End Function
        Public Function GetDataDetail2(ByVal sKDLPK As String) As List(Of S_PENDAFTARAN_LPK_D2)
            If Not oConnection.GetConnection() Then
                GetDataDetail2 = Nothing
                Exit Function
            End If
            GetDataDetail2 = oConnection.db.S_PENDAFTARAN_LPK_D2s.Where(Function(x) x.KDLPK = sKDLPK).ToList()
        End Function
        Public Function InsertData(ByVal entity As S_PENDAFTARAN_LPK, ByVal entityDetail1 As List(Of S_PENDAFTARAN_LPK_D1), Optional entityDetail2 As List(Of S_PENDAFTARAN_LPK_D2) = Nothing) As String
            Try
                If Not oConnection.GetConnection() Then
                    InsertData = ""
                    Exit Function
                End If

                sREFERENCE = entity.KDLPK
                sSTATUS = "INSERT"
                Dim sKDLPK As String = String.Empty

                Try
                    If entity.KDLPK = String.Empty Then
                        sLASTNUMBER = oCounter.GetLastNumber(sMODUL, entity.DATE_MASUK)
                        If sLASTNUMBER = 0 Then
                            Try
                                oCounter.InsertData(sMODUL, entity.DATE_MASUK)
                                sLASTNUMBER = oCounter.GetLastNumber(sMODUL, entity.DATE_MASUK)
                            Catch ex As Exception
                                sLASTNUMBER = 0
                            End Try
                        End If

                        entity.KDLPK = AutoNumber(sMODUL, sLASTNUMBER + 1, entity.DATE_MASUK)
                        For Each iLoop In entityDetail1
                            iLoop.KDLPK = entity.KDLPK
                        Next
                        Try
                            oCounter.UpdateData(sMODUL, sLASTNUMBER + 1, Month(entity.DATE_MASUK), Year(entity.DATE_MASUK))
                            oConnection.db.S_PENDAFTARAN_LPK_D1s.InsertAllOnSubmit(entityDetail1)
                            If entityDetail2 IsNot Nothing Then
                                For Each iLoop In entityDetail2
                                    iLoop.KDLPK = entity.KDLPK
                                Next
                                oConnection.db.S_PENDAFTARAN_LPK_D2s.InsertAllOnSubmit(entityDetail2)
                            End If
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
                    oConnection.db.S_PENDAFTARAN_LPKs.InsertOnSubmit(entity)
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

                InsertData = entity.KDLPK
            Catch ex As Exception
                InsertData = ""
                oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                Throw ex
            End Try
        End Function
        Public Function UpdateData(ByVal entity As S_PENDAFTARAN_LPK, ByVal entityDetail1 As List(Of S_PENDAFTARAN_LPK_D1), Optional entityDetail2 As List(Of S_PENDAFTARAN_LPK_D2) = Nothing) As Boolean
            Try
                If Not oConnection.GetConnection() Then
                    UpdateData = False
                    Exit Function
                End If

                sREFERENCE = entity.KDLPK
                sSTATUS = "UPDATE"

                Dim ds = oConnection.db.S_PENDAFTARAN_LPKs.FirstOrDefault(Function(x) x.KDLPK = entity.KDLPK)
                Dim dsDetail1 = oConnection.db.S_PENDAFTARAN_LPK_D1s.Where(Function(x) x.KDLPK = entity.KDLPK)
                Dim dsDetail2 = oConnection.db.S_PENDAFTARAN_LPK_D2s.Where(Function(x) x.KDLPK = entity.KDLPK)

                Try
                    oConnection.db.S_PENDAFTARAN_LPKs.DeleteOnSubmit(ds)
                    oConnection.db.S_PENDAFTARAN_LPKs.InsertOnSubmit(entity)
                    oConnection.db.S_PENDAFTARAN_LPK_D1s.DeleteAllOnSubmit(dsDetail1)
                    oConnection.db.S_PENDAFTARAN_LPK_D1s.InsertAllOnSubmit(entityDetail1)
                    If dsDetail2 IsNot Nothing Then
                        oConnection.db.S_PENDAFTARAN_LPK_D2s.DeleteAllOnSubmit(dsDetail2)
                    End If
                    If entityDetail2 IsNot Nothing Then
                        oConnection.db.S_PENDAFTARAN_LPK_D2s.InsertAllOnSubmit(entityDetail2)
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

                Dim ds = oConnection.db.S_PENDAFTARAN_LPKs.FirstOrDefault(Function(x) x.KDLPK.Contains(Parameter))
                Dim dsDetail1 = oConnection.db.S_PENDAFTARAN_LPK_D1s.Where(Function(x) x.KDLPK = Parameter)
                Dim dsDetail2 = oConnection.db.S_PENDAFTARAN_LPK_D2s.Where(Function(x) x.KDLPK = Parameter)

                Try
                    oConnection.db.S_PENDAFTARAN_LPKs.DeleteOnSubmit(ds)
                    oConnection.db.S_PENDAFTARAN_LPK_D1s.DeleteAllOnSubmit(dsDetail1)
                    If dsDetail2.Count > 0 Then
                        oConnection.db.S_PENDAFTARAN_LPK_D2s.DeleteAllOnSubmit(dsDetail2)
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
            End Try
        End Function
        Public Function DIAGNOSA_Default() As String
            Try
                If Not oConnection.GetConnection() Then
                    DIAGNOSA_Default = String.Empty
                    Exit Function
                End If

                Dim ds = oConnection.db.M_DIAGNOSAs.FirstOrDefault(Function(x) x.ISDEFAULT = True)
                If ds IsNot Nothing Then
                    DIAGNOSA_Default = ds.KDDIAGNOSA
                Else
                    DIAGNOSA_Default = String.Empty
                End If
            Catch ex As Exception
                DIAGNOSA_Default = String.Empty
                Throw ex
            End Try
        End Function
        Public Function PROSEDUR_Default() As String
            Try
                If Not oConnection.GetConnection() Then
                    PROSEDUR_Default = String.Empty
                    Exit Function
                End If

                Dim ds = oConnection.db.M_PROSEDURs.FirstOrDefault(Function(x) x.ISDEFAULT = True)
                If ds IsNot Nothing Then
                    PROSEDUR_Default = ds.KDPROSEDUR
                Else
                    PROSEDUR_Default = String.Empty
                End If
            Catch ex As Exception
                PROSEDUR_Default = String.Empty
                Throw ex
            End Try
        End Function

        'Public Function UpdateDataFix(ByVal isNew As Boolean) As Boolean
        '    Try
        '        If Not oConnection.GetConnection Then
        '            UpdateDataFix = False
        '            Exit Function
        '        End If


        '        Dim entity = oConnection.db.S_PENDAFTARAN_LPKs

        '        For Each iLoop In entity
        '            Dim sKDLPK = iLoop.KDLPK
        '            Dim entityDetail = oConnection.db.F_PENDAFTARAN_Ds.Where(Function(x) x.KDLPK = sKDLPK And x.SEQ < 100)
        '            Dim entityDetail_R = oConnection.db.F_PENDAFTARAN_Ds.Where(Function(x) x.KDLPK = sKDLPK And x.SEQ >= 100)

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