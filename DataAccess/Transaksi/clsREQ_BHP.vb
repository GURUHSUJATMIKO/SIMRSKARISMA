Imports System.Threading

Namespace Transaksi
    Public Class clsREQ_BHP
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
            sMODUL = "REQBHP"

            oCounter = New Setting.clsCounter
        End Sub
        Protected Overrides Sub Finalize()
            MyBase.Finalize()
            oConnection = Nothing
            GC.SuppressFinalize(Me)
        End Sub
        Public Function GetStructureHeader() As S_REQ_BHP_H
            If Not oConnection.GetConnection Then
                GetStructureHeader = Nothing
            End If
            GetStructureHeader = New S_REQ_BHP_H
        End Function
        Public Function GetStructureDetail() As S_REQ_BHP_D
            If Not oConnection.GetConnection Then
                GetStructureDetail = Nothing
            End If
            GetStructureDetail = New S_REQ_BHP_D
        End Function
        Public Function GetStructureDetailList() As List(Of S_REQ_BHP_D)
            If Not oConnection.GetConnection Then
                GetStructureDetailList = Nothing
            End If
            GetStructureDetailList = New List(Of S_REQ_BHP_D)
        End Function
        Public Function GetData() As List(Of S_REQ_BHP_H)
            If Not oConnection.GetConnection Then
                GetData = Nothing
                Exit Function
            End If
            GetData = oConnection.db.S_REQ_BHP_Hs.OrderByDescending(Function(x) x.KDREQBHP).ToList()
        End Function
        Public Function GetData(ByVal Parameter As String) As S_REQ_BHP_H
            If Not oConnection.GetConnection Then
                GetData = Nothing
                Exit Function
            End If
            GetData = oConnection.db.S_REQ_BHP_Hs.FirstOrDefault(Function(x) x.KDREQBHP = Parameter)
        End Function
        Public Function GetDataKDPENDAFTARAN(ByVal Parameter As String) As S_REQ_BHP_H
            If Not oConnection.GetConnection Then
                GetDataKDPENDAFTARAN = Nothing
                Exit Function
            End If
            GetDataKDPENDAFTARAN = oConnection.db.S_REQ_BHP_Hs.FirstOrDefault(Function(x) x.KDPENDAFTARAN = Parameter)
        End Function
        Public Function GetDataKDPENDAFTARANList(ByVal Parameter As String) As List(Of S_REQ_BHP_H)
            If Not oConnection.GetConnection Then
                GetDataKDPENDAFTARANList = Nothing
                Exit Function
            End If
            GetDataKDPENDAFTARANList = oConnection.db.S_REQ_BHP_Hs.Where(Function(x) x.KDPENDAFTARAN = Parameter).ToList()
        End Function
        Public Function GetDataDetail() As List(Of S_REQ_BHP_D)
            If Not oConnection.GetConnection Then
                GetDataDetail = Nothing
                Exit Function
            End If
            GetDataDetail = oConnection.db.S_REQ_BHP_Ds.ToList()
        End Function
        Public Function GetDataDetailByKDREQ(ByVal sKDREQBHP As String) As List(Of S_REQ_BHP_D)
            If Not oConnection.GetConnection Then
                GetDataDetailByKDREQ = Nothing
                Exit Function
            End If
            GetDataDetailByKDREQ = oConnection.db.S_REQ_BHP_Ds.Where(Function(x) x.KDREQBHP = sKDREQBHP).OrderBy(Function(x) x.SEQ).ToList()
        End Function
        Public Function GetDataDetail(ByVal Parameter As String) As List(Of S_REQ_BHP_D)
            If Not oConnection.GetConnection Then
                GetDataDetail = Nothing
                Exit Function
            End If
            GetDataDetail = oConnection.db.S_REQ_BHP_Ds.Where(Function(x) x.KDREQBHP = Parameter).ToList()
        End Function
        Public Function GetDataByDate(ByVal sDateFrom As DateTime, ByVal sDateTo As DateTime) As List(Of S_REQ_BHP_H)
            If Not oConnection.GetConnection Then
                GetDataByDate = Nothing
                Exit Function
            End If
            GetDataByDate = oConnection.db.S_REQ_BHP_Hs.Where(Function(x) x.DATE >= sDateFrom.ToString("yyyy-MM-dd") & " 00:00:00" And x.DATE <= sDateTo.ToString("yyyy-MM-dd") & " 23:59:59").OrderByDescending(Function(x) x.KDREQBHP).ToList()
        End Function
        'Public Function GetDataByRekamMedis(ByVal Paramater As String) As List(Of S_REQ_BHP_H)
        '    If Not oConnection.GetConnection Then
        '        GetDataByRekamMedis = Nothing
        '        Exit Function
        '    End If
        '    GetDataByRekamMedis = oConnection.db.S_REQ_BHP_Hs.Where(Function(x) x.S_PENDAFTARAN_H.KDCUSTOMER = Paramater).OrderByDescending(Function(x) x.KDREQBHP).ToList()
        'End Function
        'Public Function GetDataDetailByRekamMedisLab(ByVal Parameter As String) As List(Of S_REQ_BHP_D)
        '    If Not oConnection.GetConnection Then
        '        GetDataDetailByRekamMedisLab = Nothing
        '        Exit Function
        '    End If
        '    GetDataDetailByRekamMedisLab = oConnection.db.S_REQ_BHP_Ds.Where(Function(x) x.S_REQ_BHP_H.S_PENDAFTARAN_H.KDCUSTOMER = Parameter And x.ISCHEKED = False And x.KETERANGAN_PENUNJANG = "LABORATORIUM").ToList()
        'End Function
        Public Function InsertData(ByVal entity As S_REQ_BHP_H, ByVal entityDetail As List(Of S_REQ_BHP_D)) As String
            Try
                If Not oConnection.GetConnection Then
                    InsertData = ""
                    Exit Function
                End If


                sSTATUS = "ISNERT"
                sREFERENCE = entity.KDREQBHP

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

                    entity.KDREQBHP = AutoNumber(sMODUL, sLASTNUMBER + 1, entity.DATE)

                    For Each iLoop In entityDetail
                        iLoop.KDREQBHP = entity.KDREQBHP
                    Next

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

                For Each xLoop In entityDetail
                    If xLoop.KETERANGAN_PENUNJANG = "LABORATORIUM" Then
                        Dim sLASTNUMBERLAB As Integer = 0
                        Dim sMODULLAB As String = "OL"

                        Try
                            sLASTNUMBERLAB = oCounter.GetLastNumber(sMODULLAB, entity.DATE)
                            If sLASTNUMBERLAB = 0 Then
                                Try
                                    oCounter.InsertData(sMODULLAB, entity.DATE)
                                    sLASTNUMBERLAB = oCounter.GetLastNumber(sMODULLAB, entity.DATE)
                                Catch ex As Exception
                                    sLASTNUMBERLAB = 0
                                End Try
                            End If

                            For Each iLoop In entityDetail
                                If iLoop.KETERANGAN_PENUNJANG = "LABORATORIUM" Then
                                    iLoop.GROUP = AutoNumber(sMODULLAB, sLASTNUMBERLAB + 1, entity.DATE)
                                End If
                            Next

                        Catch ex As Exception
                            oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                            Throw ex
                        End Try

                        Try
                            oCounter.UpdateData(sMODULLAB, sLASTNUMBERLAB + 1, Month(entity.DATE), Year(entity.DATE))
                        Catch ex As Exception
                            oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                            Throw ex
                        End Try

                        Exit For
                    End If
                Next

                For Each xLoop In entityDetail
                    If xLoop.KETERANGAN_PENUNJANG = "RADIOLOGI" Then
                        Dim sLASTNUMBERRAD As Integer = 0
                        Dim sMODULRAD As String = "OR"

                        Try
                            sLASTNUMBERRAD = oCounter.GetLastNumber(sMODULRAD, entity.DATE)
                            If sLASTNUMBERRAD = 0 Then
                                Try
                                    oCounter.InsertData(sMODULRAD, entity.DATE)
                                    sLASTNUMBERRAD = oCounter.GetLastNumber(sMODULRAD, entity.DATE)
                                Catch ex As Exception
                                    sLASTNUMBERRAD = 0
                                End Try
                            End If

                            For Each iLoop In entityDetail
                                If iLoop.KETERANGAN_PENUNJANG = "RADIOLOGI" Then
                                    iLoop.GROUP = AutoNumber(sMODULRAD, sLASTNUMBERRAD + 1, entity.DATE)
                                End If
                            Next

                        Catch ex As Exception
                            oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                            Throw ex
                        End Try

                        Try
                            oCounter.UpdateData(sMODULRAD, sLASTNUMBERRAD + 1, Month(entity.DATE), Year(entity.DATE))
                        Catch ex As Exception
                            oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                            Throw ex
                        End Try

                        Exit For
                    End If
                Next

                Try
                    oConnection.db.S_REQ_BHP_Hs.InsertOnSubmit(entity)
                    oConnection.db.S_REQ_BHP_Ds.InsertAllOnSubmit(entityDetail)
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

                InsertData = entity.KDREQBHP
            Catch ex As Exception
                InsertData = ""
                oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                Throw ex
            End Try
        End Function
        Public Function UpdateData(ByVal entity As S_REQ_BHP_H, ByVal entityDetail As List(Of S_REQ_BHP_D)) As Boolean
            Try
                If Not oConnection.GetConnection Then
                    UpdateData = False
                    Exit Function
                End If

                sSTATUS = "UPDATE"
                sREFERENCE = entity.KDREQBHP

                Dim ds = oConnection.db.S_REQ_BHP_Hs.FirstOrDefault(Function(x) x.KDREQBHP = entity.KDREQBHP)

                Try
                    oConnection.db.S_REQ_BHP_Hs.DeleteOnSubmit(ds)
                    oConnection.db.S_REQ_BHP_Hs.InsertOnSubmit(entity)
                Catch ex As Exception
                    oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                    Throw ex
                End Try

                Dim dsDetail = oConnection.db.S_REQ_BHP_Ds.Where(Function(x) x.KDREQBHP = entity.KDREQBHP)

                Try
                    oConnection.db.S_REQ_BHP_Ds.DeleteAllOnSubmit(dsDetail)
                    oConnection.db.S_REQ_BHP_Ds.InsertAllOnSubmit(entityDetail)
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
                If Not oConnection.GetConnection Then
                    DeleteData = False
                    Exit Function
                End If

                sSTATUS = "DELETE"
                sREFERENCE = Parameter

                Dim ds = oConnection.db.S_REQ_BHP_Hs.FirstOrDefault(Function(x) x.KDREQBHP = Parameter)
                Dim dsDetail = oConnection.db.S_REQ_BHP_Ds.Where(Function(x) x.KDREQBHP = Parameter)

                Try
                    oConnection.db.S_REQ_BHP_Hs.DeleteOnSubmit(ds)
                Catch ex As Exception
                    oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                    Throw ex
                End Try
                Try
                    oConnection.db.S_REQ_BHP_Ds.DeleteAllOnSubmit(dsDetail)
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
        Public Function AccountDefault() As String
            Try
                If Not oConnection.GetConnection() Then
                    AccountDefault = String.Empty
                    Exit Function
                End If

                Dim ds = oConnection.db.M_PROSEDURs.FirstOrDefault(Function(x) x.ISDEFAULT = True).KDPROSEDUR
                Try
                    AccountDefault = ds
                Catch ex As Exception
                    AccountDefault = String.Empty
                End Try
            Catch ex As Exception
                AccountDefault = String.Empty
                Throw ex
            End Try
        End Function
    End Class
End Namespace