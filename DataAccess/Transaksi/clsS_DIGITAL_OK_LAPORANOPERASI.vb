Imports System.Data.SqlClient
Imports System.Threading

Namespace EMedrek
    Public Class clsS_DIGITAL_OK_LAPORANOPERASI
        Public oConnection As Setting.clsConnectionMain = Nothing
        Public oError As Setting.clsError = Nothing
        Public oCounter As Setting.clsCounter = Nothing
        Public sREFERENCE As String = ""
        Public sMODUL As String = ""
        Public sLASTNUMBER As Integer = 0

        Public Sub New()
            oConnection = New Setting.clsConnectionMain
            oError = New Setting.clsError
            oCounter = New Setting.clsCounter
            sMODUL = "LO"
        End Sub
        Protected Overrides Sub Finalize()
            MyBase.Finalize()
            oConnection = Nothing
            GC.SuppressFinalize(Me)
        End Sub
        Public Function GetStructureHeader() As S_DIGITAL_OK_LAPORANOPERASI
            If Not oConnection.GetConnection Then
                GetStructureHeader = Nothing
            End If
            GetStructureHeader = New S_DIGITAL_OK_LAPORANOPERASI
        End Function
        Public Function GetStructureDetailDiagnosaList() As List(Of S_DIGITAL_OK_LAPORANOPERASI_DIAGNOSA)
            If Not oConnection.GetConnection() Then
                GetStructureDetailDiagnosaList = Nothing
            End If
            GetStructureDetailDiagnosaList = New List(Of S_DIGITAL_OK_LAPORANOPERASI_DIAGNOSA)
        End Function
        Public Function GetStructureDetailDiagnosa2List() As List(Of S_DIGITAL_OK_LAPORANOPERASI_DIAGNOSA_2)
            If Not oConnection.GetConnection() Then
                GetStructureDetailDiagnosa2List = Nothing
            End If
            GetStructureDetailDiagnosa2List = New List(Of S_DIGITAL_OK_LAPORANOPERASI_DIAGNOSA_2)
        End Function
        Public Function GetStructureDetailProsedurList() As List(Of S_DIGITAL_OK_LAPORANOPERASI_PROSEDUR)
            If Not oConnection.GetConnection() Then
                GetStructureDetailProsedurList = Nothing
            End If
            GetStructureDetailProsedurList = New List(Of S_DIGITAL_OK_LAPORANOPERASI_PROSEDUR)
        End Function
        Public Function GetStructureDetailDiagnosa() As S_DIGITAL_OK_LAPORANOPERASI_DIAGNOSA
            If Not oConnection.GetConnection() Then
                GetStructureDetailDiagnosa = Nothing
            End If
            GetStructureDetailDiagnosa = New S_DIGITAL_OK_LAPORANOPERASI_DIAGNOSA
        End Function
        Public Function GetStructureDetailDiagnosa2() As S_DIGITAL_OK_LAPORANOPERASI_DIAGNOSA_2
            If Not oConnection.GetConnection() Then
                GetStructureDetailDiagnosa2 = Nothing
            End If
            GetStructureDetailDiagnosa2 = New S_DIGITAL_OK_LAPORANOPERASI_DIAGNOSA_2
        End Function
        Public Function GetStructureDetailProsedur() As S_DIGITAL_OK_LAPORANOPERASI_PROSEDUR
            If Not oConnection.GetConnection() Then
                GetStructureDetailProsedur = Nothing
            End If
            GetStructureDetailProsedur = New S_DIGITAL_OK_LAPORANOPERASI_PROSEDUR
        End Function
        Public Function GetDataList() As List(Of S_DIGITAL_OK_LAPORANOPERASI)
            If Not oConnection.GetConnection Then
                GetDataList = Nothing
                Exit Function
            End If
            GetDataList = oConnection.db.S_DIGITAL_OK_LAPORANOPERASIs.OrderBy(Function(x) x.KDPENDAFTARAN).ToList()
        End Function
        Public Function GetDataByRMList(ByVal RM As String) As List(Of S_DIGITAL_OK_LAPORANOPERASI)
            If Not oConnection.GetConnection Then
                GetDataByRMList = Nothing
                Exit Function
            End If
            GetDataByRMList = oConnection.db.S_DIGITAL_OK_LAPORANOPERASIs.Where(Function(x) x.KDCUSTOMER = RM).OrderBy(Function(x) x.KDPENDAFTARAN).ToList()
        End Function
        Public Function GetDataByRegisterList(ByVal sKDPENDAFTARAN As String) As List(Of S_DIGITAL_OK_LAPORANOPERASI)
            If Not oConnection.GetConnection Then
                GetDataByRegisterList = Nothing
                Exit Function
            End If
            GetDataByRegisterList = oConnection.db.S_DIGITAL_OK_LAPORANOPERASIs.Where(Function(x) x.KDPENDAFTARAN = sKDPENDAFTARAN).OrderBy(Function(x) x.DATE).ToList()
        End Function
        Public Function GetDataByKDPENDFATRAN(ByVal sParameter As String) As S_DIGITAL_OK_LAPORANOPERASI
            If Not oConnection.GetConnection Then
                GetDataByKDPENDFATRAN = Nothing
                Exit Function
            End If
            GetDataByKDPENDFATRAN = oConnection.db.S_DIGITAL_OK_LAPORANOPERASIs.FirstOrDefault(Function(x) x.KDPENDAFTARAN = sParameter)
        End Function
        Public Function GetDataKode(ByVal sParameter As String) As S_DIGITAL_OK_LAPORANOPERASI
            If Not oConnection.GetConnection Then
                GetDataKode = Nothing
                Exit Function
            End If
            GetDataKode = oConnection.db.S_DIGITAL_OK_LAPORANOPERASIs.FirstOrDefault(Function(x) x.KDLAPORANOPERASI = sParameter)
        End Function
        Public Function GetDataKodeTindakan(ByVal sParameter As String) As S_DIGITAL_OK_LAPORANTINDAKAN
            If Not oConnection.GetConnection Then
                GetDataKodeTindakan = Nothing
                Exit Function
            End If
            GetDataKodeTindakan = oConnection.db.S_DIGITAL_OK_LAPORANTINDAKANs.FirstOrDefault(Function(x) x.KDLAPORANTINDAKAN = sParameter)
        End Function
        'Public Function GetData(ByVal sParameter As String) As S_DIGITAL_OK_LAPORANOPERASI
        '    If Not oConnection.GetConnection Then
        '        GetData = Nothing
        '        Exit Function
        '    End If
        '    GetData = oConnection.db.S_DIGITAL_OK_LAPORANOPERASIs.FirstOrDefault(Function(x) x.KDPENDAFTARAN = sParameter)
        'End Function
        Public Function GetDataByRM(ByVal kdcustomer As String) As List(Of S_DIGITAL_OK_LAPORANOPERASI)
            If Not oConnection.GetConnection() Then
                GetDataByRM = Nothing
                Exit Function
            End If
            GetDataByRM = oConnection.db.S_DIGITAL_OK_LAPORANOPERASIs.Where(Function(x) x.KDCUSTOMER = kdcustomer).OrderByDescending(Function(x) x.KDPENDAFTARAN).ToList()
        End Function
        Public Function GetDataDetail(ByVal sParameter As String) As List(Of S_DIGITAL_OK_LAPORANOPERASI)
            If Not oConnection.GetConnection() Then
                GetDataDetail = Nothing
                Exit Function
            End If
            'Dim KDPENDAFTARAN As String = oConnection.db.R_IDENTITAS_PASIENs.FirstOrDefault(Function(x) x.KDPENDAFTARAN = sParameter).KDPENDAFTARAN
            GetDataDetail = oConnection.db.S_DIGITAL_OK_LAPORANOPERASIs.Where(Function(x) x.KDPENDAFTARAN = sParameter).ToList()
        End Function
        Public Function GetDataDetailDiagnosa(ByVal sKDLAPORANOPERASI As String) As List(Of S_DIGITAL_OK_LAPORANOPERASI_DIAGNOSA)
            If Not oConnection.GetConnection() Then
                GetDataDetailDiagnosa = Nothing
                Exit Function
            End If
            GetDataDetailDiagnosa = oConnection.db.S_DIGITAL_OK_LAPORANOPERASI_DIAGNOSAs.Where(Function(x) x.KDLAPORANOPERASI = sKDLAPORANOPERASI).OrderBy(Function(x) x.SEQ).ToList()
        End Function
        Public Function GetDataDetailDiagnosa2(ByVal sKDLAPORANOPERASI As String) As List(Of S_DIGITAL_OK_LAPORANOPERASI_DIAGNOSA_2)
            If Not oConnection.GetConnection() Then
                GetDataDetailDiagnosa2 = Nothing
                Exit Function
            End If
            GetDataDetailDiagnosa2 = oConnection.db.S_DIGITAL_OK_LAPORANOPERASI_DIAGNOSA_2s.Where(Function(x) x.KDLAPORANOPERASI = sKDLAPORANOPERASI).OrderBy(Function(x) x.SEQ).ToList()
        End Function
        Public Function GetDataDetailProsedur(ByVal sKDLAPORANOPERASI As String) As List(Of S_DIGITAL_OK_LAPORANOPERASI_PROSEDUR)
            If Not oConnection.GetConnection() Then
                GetDataDetailProsedur = Nothing
                Exit Function
            End If
            GetDataDetailProsedur = oConnection.db.S_DIGITAL_OK_LAPORANOPERASI_PROSEDURs.Where(Function(x) x.KDLAPORANOPERASI = sKDLAPORANOPERASI).OrderBy(Function(x) x.SEQ).ToList()
        End Function
        Public Function GetSequence(ByVal sParameter As String) As Integer
            If Not oConnection.GetConnection() Then
                GetSequence = Nothing
                Exit Function
            End If
            'Dim KDPENDAFTARAN As String = oConnection.db.R_IDENTITAS_PASIENs.FirstOrDefault(Function(x) x.KDPENDAFTARAN = sParameter).KDPENDAFTARAN
            GetSequence = oConnection.db.S_DIGITAL_OK_LAPORANOPERASIs.Where(Function(x) x.KDPENDAFTARAN = sParameter).OrderByDescending(Function(x) x.SEQ).FirstOrDefault.SEQ
        End Function
        Public Function IsExist(ByVal sParameter As String) As Boolean
            If Not oConnection.GetConnection Then
                IsExist = False
                Exit Function
            End If

            Dim ds = oConnection.db.S_DIGITAL_OK_LAPORANOPERASIs.FirstOrDefault(Function(x) x.KDPENDAFTARAN = sParameter)

            If ds IsNot Nothing Then
                IsExist = True
            Else
                IsExist = False
            End If
        End Function
        Public Function InsertData(ByVal entity As S_DIGITAL_OK_LAPORANOPERASI, ByVal entityDiagnosa As List(Of S_DIGITAL_OK_LAPORANOPERASI_DIAGNOSA), ByVal entityDiagnosa2 As List(Of S_DIGITAL_OK_LAPORANOPERASI_DIAGNOSA_2), ByVal entityProsedur As List(Of S_DIGITAL_OK_LAPORANOPERASI_PROSEDUR)) As Boolean
            Try
                If Not oConnection.GetConnection Then
                    InsertData = False
                    Exit Function
                End If

                sREFERENCE = entity.KDPENDAFTARAN

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

                    entity.KDLAPORANOPERASI = AutoNumber(sMODUL, sLASTNUMBER + 1, entity.DATE)

                    For Each iLoop In entityDiagnosa
                        iLoop.KDLAPORANOPERASI = entity.KDLAPORANOPERASI
                    Next

                    For Each iLoop In entityDiagnosa2
                        iLoop.KDLAPORANOPERASI = entity.KDLAPORANOPERASI
                    Next

                    For Each iLoop In entityProsedur
                        iLoop.KDLAPORANOPERASI = entity.KDLAPORANOPERASI
                    Next

                Catch ex As Exception
                    oError.InsertData(sMODUL, "S_DIGITAL_OK_LAPORANOPERASI", ex.ToString, sREFERENCE)
                    Throw ex
                End Try

                Try
                    oConnection.db.S_DIGITAL_OK_LAPORANOPERASIs.InsertOnSubmit(entity)
                    If entityDiagnosa.Count > 0 Then
                        oConnection.db.S_DIGITAL_OK_LAPORANOPERASI_DIAGNOSAs.InsertAllOnSubmit(entityDiagnosa)
                    End If
                    If entityDiagnosa2.Count > 0 Then
                        oConnection.db.S_DIGITAL_OK_LAPORANOPERASI_DIAGNOSA_2s.InsertAllOnSubmit(entityDiagnosa2)
                    End If
                    If entityProsedur.Count > 0 Then
                        oConnection.db.S_DIGITAL_OK_LAPORANOPERASI_PROSEDURs.InsertAllOnSubmit(entityProsedur)
                    End If
                Catch ex As Exception
                    oError.InsertData("S_DIGITAL_OK_LAPORANOPERASI", "INSERTDATA", ex.ToString, sREFERENCE)
                    Throw ex
                End Try
                Try
                    oConnection.db.SubmitChanges()
                Catch ex As Exception
                    oError.InsertData("S_DIGITAL_OK_LAPORANOPERASI", "INSERTDATA", ex.ToString, sREFERENCE)
                    Throw ex
                End Try

                Try
                    oCounter.UpdateData(sMODUL, sLASTNUMBER + 1, Month(entity.DATE), Year(entity.DATE))
                Catch ex As Exception
                    oError.InsertData(sMODUL, "INSERTDATA", ex.ToString, sREFERENCE)
                    Throw ex
                End Try

                InsertData = True
            Catch ex As Exception
                InsertData = False
                oError.InsertData("S_DIGITAL_OK_LAPORANOPERASI", "INSERTDATA", ex.ToString, sREFERENCE)
                Throw ex
            End Try
        End Function
        Public Function UpdateData(ByVal entity As S_DIGITAL_OK_LAPORANOPERASI, ByVal entityDiagnosa As List(Of S_DIGITAL_OK_LAPORANOPERASI_DIAGNOSA), ByVal entityDiagnosa2 As List(Of S_DIGITAL_OK_LAPORANOPERASI_DIAGNOSA_2), ByVal entityProsedur As List(Of S_DIGITAL_OK_LAPORANOPERASI_PROSEDUR)) As Boolean
            Try
                If Not oConnection.GetConnection Then
                    UpdateData = False
                    Exit Function
                End If

                sREFERENCE = entity.KDPENDAFTARAN

                Dim ds = oConnection.db.S_DIGITAL_OK_LAPORANOPERASIs.FirstOrDefault(Function(x) x.KDLAPORANOPERASI = entity.KDLAPORANOPERASI)
                Dim dsDiagnosa = oConnection.db.S_DIGITAL_OK_LAPORANOPERASI_DIAGNOSAs.Where(Function(x) x.KDLAPORANOPERASI = entity.KDLAPORANOPERASI)
                Dim dsDiagnosa2 = oConnection.db.S_DIGITAL_OK_LAPORANOPERASI_DIAGNOSA_2s.Where(Function(x) x.KDLAPORANOPERASI = entity.KDLAPORANOPERASI)
                Dim dsProsedur = oConnection.db.S_DIGITAL_OK_LAPORANOPERASI_PROSEDURs.Where(Function(x) x.KDLAPORANOPERASI = entity.KDLAPORANOPERASI)

                Try
                    oConnection.db.S_DIGITAL_OK_LAPORANOPERASIs.DeleteOnSubmit(ds)
                    oConnection.db.S_DIGITAL_OK_LAPORANOPERASIs.InsertOnSubmit(entity)

                    If dsDiagnosa.Count > 0 Then
                        oConnection.db.S_DIGITAL_OK_LAPORANOPERASI_DIAGNOSAs.DeleteAllOnSubmit(dsDiagnosa)
                    End If
                    If entityDiagnosa.Count > 0 Then
                        oConnection.db.S_DIGITAL_OK_LAPORANOPERASI_DIAGNOSAs.InsertAllOnSubmit(entityDiagnosa)
                    End If

                    If dsDiagnosa2.Count > 0 Then
                        oConnection.db.S_DIGITAL_OK_LAPORANOPERASI_DIAGNOSA_2s.DeleteAllOnSubmit(dsDiagnosa2)
                    End If
                    If entityDiagnosa2.Count > 0 Then
                        oConnection.db.S_DIGITAL_OK_LAPORANOPERASI_DIAGNOSA_2s.InsertAllOnSubmit(entityDiagnosa2)
                    End If

                    If dsProsedur.Count > 0 Then
                        oConnection.db.S_DIGITAL_OK_LAPORANOPERASI_PROSEDURs.DeleteAllOnSubmit(dsProsedur)
                    End If
                    If entityProsedur.Count > 0 Then
                        oConnection.db.S_DIGITAL_OK_LAPORANOPERASI_PROSEDURs.InsertAllOnSubmit(entityProsedur)
                    End If

                Catch ex As Exception
                    oError.InsertData("S_DIGITAL_OK_LAPORANOPERASI", "UPDATEDATA", ex.ToString, sREFERENCE)
                    Throw ex
                End Try
                Try
                    oConnection.db.SubmitChanges()
                Catch ex As Exception
                    oError.InsertData("S_DIGITAL_OK_LAPORANOPERASI", "UPDATEDATA", ex.ToString, sREFERENCE)
                    Throw ex
                End Try

                UpdateData = True
            Catch ex As Exception
                UpdateData = False
                oError.InsertData("S_DIGITAL_OK_LAPORANOPERASI", "UPDATEDATA", ex.ToString, sREFERENCE)
                Throw ex
            End Try
        End Function
        Public Function DeleteData(ByVal Parameter As Integer, ByVal sSEQ As Integer) As Boolean
            Try
                If Not oConnection.GetConnection Then
                    DeleteData = False
                    Exit Function
                End If

                sREFERENCE = Parameter


                Dim ds = oConnection.db.S_DIGITAL_OK_LAPORANOPERASIs.FirstOrDefault(Function(x) x.KDLAPORANOPERASI = Parameter)
                Dim dsDiagnosa = oConnection.db.S_DIGITAL_OK_LAPORANOPERASI_DIAGNOSAs.Where(Function(x) x.KDLAPORANOPERASI = Parameter)
                Dim dsDiagnosa2 = oConnection.db.S_DIGITAL_OK_LAPORANOPERASI_DIAGNOSA_2s.Where(Function(x) x.KDLAPORANOPERASI = Parameter)
                Dim dsProsedur = oConnection.db.S_DIGITAL_OK_LAPORANOPERASI_PROSEDURs.Where(Function(x) x.KDLAPORANOPERASI = Parameter)

                Try
                    oConnection.db.S_DIGITAL_OK_LAPORANOPERASIs.DeleteOnSubmit(ds)

                    If dsDiagnosa.Count > 0 Then
                        oConnection.db.S_DIGITAL_OK_LAPORANOPERASI_DIAGNOSAs.DeleteAllOnSubmit(dsDiagnosa)
                    End If

                    If dsDiagnosa2.Count > 0 Then
                        oConnection.db.S_DIGITAL_OK_LAPORANOPERASI_DIAGNOSA_2s.DeleteAllOnSubmit(dsDiagnosa2)
                    End If

                    If dsProsedur.Count > 0 Then
                        oConnection.db.S_DIGITAL_OK_LAPORANOPERASI_PROSEDURs.DeleteAllOnSubmit(dsProsedur)
                    End If
                Catch ex As Exception
                    oError.InsertData("S_DIGITAL_OK_LAPORANOPERASI", "DELETEDATA", ex.ToString, sREFERENCE)
                    Throw ex
                End Try
                Try
                    oConnection.db.SubmitChanges()
                Catch ex As Exception
                    oError.InsertData("S_DIGITAL_OK_LAPORANOPERASI", "DELETEDATA", ex.ToString, sREFERENCE)
                    Throw ex
                End Try

                DeleteData = True
            Catch ex As Exception
                DeleteData = False
                oError.InsertData("S_DIGITAL_OK_LAPORANOPERASI", "DELETEDATA", ex.ToString, sREFERENCE)
                Throw ex
            End Try
        End Function
        Public Function UpdateCetak(ByVal sKDPENDAFTARAN As String) As Boolean
            Try
                If Not oConnection.GetConnection Then
                    UpdateCetak = False
                    Exit Function
                End If

                sREFERENCE = sKDPENDAFTARAN

                Try
                    Dim ds = oConnection.db.S_DIGITAL_OK_LAPORANOPERASIs.FirstOrDefault(Function(x) x.KDPENDAFTARAN = sKDPENDAFTARAN)

                    ds.CETAK += 1

                    oConnection.db.SubmitChanges()

                Catch ex As Exception
                    oError.InsertData("S_DIGITAL_OK_LAPORANOPERASI", "UPDATECETAK", ex.ToString, sREFERENCE)
                    Throw ex
                End Try

                UpdateCetak = True
            Catch ex As Exception
                UpdateCetak = False
                oError.InsertData("S_DIGITAL_OK_LAPORANOPERASI", "UPDATECETAK", ex.ToString, sREFERENCE)
                Throw ex
            End Try
        End Function
        Public Function UpdateDeleteLaporanOperasi(ByVal KDLAPORANOPERASI As String, ByVal sUSER As String) As Boolean
            Try
                If Not oConnection.GetConnection Then
                    UpdateDeleteLaporanOperasi = False
                    Exit Function
                End If

                sREFERENCE = KDLAPORANOPERASI

                Try
                    Dim ds = oConnection.db.S_DIGITAL_OK_LAPORANOPERASIs.FirstOrDefault(Function(x) x.KDLAPORANOPERASI = KDLAPORANOPERASI)

                    ds.ISDELETE = 1
                    ds.DATEDELETE = Now
                    ds.USERDELETE = sUSER

                    oConnection.db.SubmitChanges()

                Catch ex As Exception
                    oError.InsertData("S_DIGITAL_OK_LAPORANOPERASI", "UPDATEDELETE", ex.ToString, sREFERENCE)
                    Throw ex
                End Try

                UpdateDeleteLaporanOperasi = True
            Catch ex As Exception
                UpdateDeleteLaporanOperasi = False
                oError.InsertData("S_DIGITAL_OK_LAPORANOPERASI", "UPDATEDELETE", ex.ToString, sREFERENCE)
                Throw ex
            End Try

        End Function
    End Class
End Namespace