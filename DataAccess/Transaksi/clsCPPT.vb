Imports System.Threading

Namespace Transaksi
    Public Class clsCPPT
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
            sMODUL = "CPPT"

            oCounter = New Setting.clsCounter
        End Sub
        Protected Overrides Sub Finalize()
            MyBase.Finalize()
            oConnection = Nothing
            GC.SuppressFinalize(Me)
        End Sub
        Public Function GetStructureHeader() As S_REQ_CPPT
            If Not oConnection.GetConnection() Then
                GetStructureHeader = Nothing
            End If
            GetStructureHeader = New S_REQ_CPPT
        End Function
        Public Function GetStructureHeaderlainnya() As S_REQ_CPPT_LAINNYA
            If Not oConnection.GetConnection() Then
                GetStructureHeaderlainnya = Nothing
            End If
            GetStructureHeaderlainnya = New S_REQ_CPPT_LAINNYA
        End Function
        Public Function GetStructureDetaiDiagnosalList() As List(Of S_REQ_CPPT_DIAGNOSA)
            If Not oConnection.GetConnection() Then
                GetStructureDetaiDiagnosalList = Nothing
            End If
            GetStructureDetaiDiagnosalList = New List(Of S_REQ_CPPT_DIAGNOSA)
        End Function
        Public Function GetStructureDetaiProsedurlList() As List(Of S_REQ_CPPT_PROSEDUR)
            If Not oConnection.GetConnection() Then
                GetStructureDetaiProsedurlList = Nothing
            End If
            GetStructureDetaiProsedurlList = New List(Of S_REQ_CPPT_PROSEDUR)
        End Function
        Public Function GetStructureHeaderlainnyaTambah() As S_REQ_CPPT_LAINNYA_TAMBAH
            If Not oConnection.GetConnection() Then
                GetStructureHeaderlainnyaTambah = Nothing
            End If
            GetStructureHeaderlainnyaTambah = New S_REQ_CPPT_LAINNYA_TAMBAH
        End Function
        Public Function GetStructureDetailDiagnosa() As S_REQ_CPPT_DIAGNOSA
            If Not oConnection.GetConnection() Then
                GetStructureDetailDiagnosa = Nothing
            End If
            GetStructureDetailDiagnosa = New S_REQ_CPPT_DIAGNOSA
        End Function
        Public Function GetStructureDetailProsedur() As S_REQ_CPPT_PROSEDUR
            If Not oConnection.GetConnection() Then
                GetStructureDetailProsedur = Nothing
            End If
            GetStructureDetailProsedur = New S_REQ_CPPT_PROSEDUR
        End Function
        Public Function GetStructureDetail() As S_REQ_CPPT_TINDAKANRAWATINAP
            If Not oConnection.GetConnection() Then
                GetStructureDetail = Nothing
            End If
            GetStructureDetail = New S_REQ_CPPT_TINDAKANRAWATINAP
        End Function
        Public Function GetStructureDetailBHP() As S_REQ_CPPT_PENUNJANGRAWATINAP
            If Not oConnection.GetConnection() Then
                GetStructureDetailBHP = Nothing
            End If
            GetStructureDetailBHP = New S_REQ_CPPT_PENUNJANGRAWATINAP
        End Function
        Public Function GetStructureDetailList() As List(Of S_REQ_CPPT_TINDAKANRAWATINAP)
            If Not oConnection.GetConnection() Then
                GetStructureDetailList = Nothing
            End If
            GetStructureDetailList = New List(Of S_REQ_CPPT_TINDAKANRAWATINAP)
        End Function
        Public Function GetStructureDetailBHPList() As List(Of S_REQ_CPPT_PENUNJANGRAWATINAP)
            If Not oConnection.GetConnection() Then
                GetStructureDetailBHPList = Nothing
            End If
            GetStructureDetailBHPList = New List(Of S_REQ_CPPT_PENUNJANGRAWATINAP)
        End Function
        'Public Function GetStructureDetailFarmasi() As S_REQ_CPPT_OBATRAWATINAP
        '    If Not oConnection.GetConnection() Then
        '        GetStructureDetailFarmasi = Nothing
        '    End If
        '    GetStructureDetailFarmasi = New S_REQ_CPPT_OBATRAWATINAP
        'End Function
        Public Function GetStructureDetailFarmasiList() As List(Of S_REQ_CPPT_OBATRAWATINAP)
            If Not oConnection.GetConnection() Then
                GetStructureDetailFarmasiList = Nothing
            End If
            GetStructureDetailFarmasiList = New List(Of S_REQ_CPPT_OBATRAWATINAP)
        End Function
        Public Function GetData() As List(Of S_REQ_CPPT)
            If Not oConnection.GetConnection() Then
                GetData = Nothing
                Exit Function
            End If
            GetData = oConnection.db.S_REQ_CPPTs.OrderByDescending(Function(x) x.KDCPPT).ToList()
        End Function
        Public Function GetData(ByVal Parameter As String) As S_REQ_CPPT
            If Not oConnection.GetConnection() Then
                GetData = Nothing
                Exit Function
            End If
            GetData = oConnection.db.S_REQ_CPPTs.FirstOrDefault(Function(x) x.KDCPPT = Parameter)
        End Function
        Public Function GetDataByRegisterDokter(ByVal Parameter As String) As S_REQ_CPPT
            If Not oConnection.GetConnection() Then
                GetDataByRegisterDokter = Nothing
                Exit Function
            End If
            GetDataByRegisterDokter = oConnection.db.S_REQ_CPPTs.FirstOrDefault(Function(x) x.KDPENDAFTARAN = Parameter And x.PROFESI = "DOKTER")
        End Function
        Public Function GetDataByRegisterProfesi(ByVal Parameter As String, ByVal Profesi As String) As S_REQ_CPPT
            If Not oConnection.GetConnection() Then
                GetDataByRegisterProfesi = Nothing
                Exit Function
            End If
            GetDataByRegisterProfesi = oConnection.db.S_REQ_CPPTs.FirstOrDefault(Function(x) x.KDPENDAFTARAN = Parameter And x.PROFESI = Profesi)
        End Function
        Public Function GetDataLainnya(ByVal Parameter As String) As S_REQ_CPPT_LAINNYA
            If Not oConnection.GetConnection() Then
                GetDataLainnya = Nothing
                Exit Function
            End If
            GetDataLainnya = oConnection.db.S_REQ_CPPT_LAINNYAs.FirstOrDefault(Function(x) x.KDCPPT = Parameter)
        End Function
        Public Function GetDataLainnyaTambah(ByVal Parameter As String) As S_REQ_CPPT_LAINNYA_TAMBAH
            If Not oConnection.GetConnection() Then
                GetDataLainnyaTambah = Nothing
                Exit Function
            End If
            GetDataLainnyaTambah = oConnection.db.S_REQ_CPPT_LAINNYA_TAMBAHs.FirstOrDefault(Function(x) x.KDCPPT = Parameter)
        End Function
        Public Function GetDataDetail() As List(Of S_REQ_CPPT_TINDAKANRAWATINAP)
            If Not oConnection.GetConnection() Then
                GetDataDetail = Nothing
                Exit Function
            End If
            GetDataDetail = oConnection.db.S_REQ_CPPT_TINDAKANRAWATINAPs.ToList()
        End Function
        Public Function GetDataDetail_BHP() As List(Of S_REQ_CPPT_PENUNJANGRAWATINAP)
            If Not oConnection.GetConnection() Then
                GetDataDetail_BHP = Nothing
                Exit Function
            End If
            GetDataDetail_BHP = oConnection.db.S_REQ_CPPT_PENUNJANGRAWATINAPs.OrderBy(Function(X) X.SEQ).ToList()
        End Function
        Public Function GetDataDetail_BHP(ByVal sKDCPPT As String) As List(Of S_REQ_CPPT_PENUNJANGRAWATINAP)
            If Not oConnection.GetConnection() Then
                GetDataDetail_BHP = Nothing
                Exit Function
            End If
            GetDataDetail_BHP = oConnection.db.S_REQ_CPPT_PENUNJANGRAWATINAPs.Where(Function(X) X.KDCPPT = sKDCPPT).OrderBy(Function(X) X.SEQ).ToList()
        End Function
        Public Function GetDataDetail(ByVal sKDCPPT As String) As List(Of S_REQ_CPPT_TINDAKANRAWATINAP)
            If Not oConnection.GetConnection() Then
                GetDataDetail = Nothing
                Exit Function
            End If
            GetDataDetail = oConnection.db.S_REQ_CPPT_TINDAKANRAWATINAPs.Where(Function(x) x.KDCPPT = sKDCPPT).ToList()
        End Function
        Public Function GetDataDetailFarmasi() As List(Of S_REQ_CPPT_OBATRAWATINAP)
            If Not oConnection.GetConnection() Then
                GetDataDetailFarmasi = Nothing
                Exit Function
            End If
            GetDataDetailFarmasi = oConnection.db.S_REQ_CPPT_OBATRAWATINAPs.ToList()
        End Function
        Public Function GetDataDetailFarmasi(ByVal sKDCPPT As String) As List(Of S_REQ_CPPT_OBATRAWATINAP)
            If Not oConnection.GetConnection() Then
                GetDataDetailFarmasi = Nothing
                Exit Function
            End If
            GetDataDetailFarmasi = oConnection.db.S_REQ_CPPT_OBATRAWATINAPs.Where(Function(x) x.KDCPPT = sKDCPPT).OrderBy(Function(X) X.SEQ).ToList()
        End Function
        Public Function GetDataByRMRANAP(ByVal kdcustomer As String) As List(Of S_REQ_CPPT)
            If Not oConnection.GetConnection() Then
                GetDataByRMRANAP = Nothing
                Exit Function
            End If
            GetDataByRMRANAP = oConnection.db.S_REQ_CPPTs.Where(Function(x) x.KDCUSTOMER = kdcustomer And x.KDPENDAFTARAN.Contains("RI")).OrderByDescending(Function(x) x.DATE).ToList()
        End Function
        Public Function GetDataByRMRAJAL(ByVal kdcustomer As String) As List(Of S_REQ_CPPT)
            If Not oConnection.GetConnection() Then
                GetDataByRMRAJAL = Nothing
                Exit Function
            End If
            GetDataByRMRAJAL = oConnection.db.S_REQ_CPPTs.Where(Function(x) x.KDCUSTOMER = kdcustomer And x.KDPENDAFTARAN.Contains("RJ")).OrderByDescending(Function(x) x.DATE).ToList()
        End Function
        Public Function GetDataByRegister(ByVal sKDPENDAFTARAN As String) As List(Of S_REQ_CPPT)
            If Not oConnection.GetConnection() Then
                GetDataByRegister = Nothing
                Exit Function
            End If
            GetDataByRegister = oConnection.db.S_REQ_CPPTs.Where(Function(x) x.KDPENDAFTARAN = sKDPENDAFTARAN).OrderByDescending(Function(x) x.KDCPPT).ToList()
        End Function
        Public Function GetDataByRMAll(ByVal kdcustomer As String) As List(Of S_REQ_CPPT)
            If Not oConnection.GetConnection() Then
                GetDataByRMAll = Nothing
                Exit Function
            End If
            GetDataByRMAll = oConnection.db.S_REQ_CPPTs.Where(Function(x) x.KDCUSTOMER = kdcustomer).OrderByDescending(Function(x) x.KDCPPT).ToList()
        End Function
        Public Function GetDataPemeriksaan(ByVal Parameter As String) As S_REQ_AKHIRPEMERIKSAAN
            If Not oConnection.GetConnection() Then
                GetDataPemeriksaan = Nothing
                Exit Function
            End If
            GetDataPemeriksaan = oConnection.db.S_REQ_AKHIRPEMERIKSAANs.FirstOrDefault(Function(x) x.KDPENDAFTARAN = Parameter)
        End Function
        Public Function GetDataCPPTRJ(ByVal Parameter As String) As S_REQ_CPPT
            If Not oConnection.GetConnection() Then
                GetDataCPPTRJ = Nothing
                Exit Function
            End If
            GetDataCPPTRJ = oConnection.db.S_REQ_CPPTs.FirstOrDefault(Function(x) x.KDPENDAFTARAN = Parameter)
        End Function
        Public Function GetDataCPPT_AwalDokter(ByVal Parameter As String) As S_REQ_CPPT
            If Not oConnection.GetConnection() Then
                GetDataCPPT_AwalDokter = Nothing
                Exit Function
            End If
            GetDataCPPT_AwalDokter = oConnection.db.S_REQ_CPPTs.Where(Function(x) x.KDPENDAFTARAN = Parameter And x.PROFESI = "DOKTER").OrderBy(Function(x) x.KDCPPT).FirstOrDefault()
        End Function
        Public Function GetDataCPPT_AkhirDokter(ByVal Parameter As String) As S_REQ_CPPT
            If Not oConnection.GetConnection() Then
                GetDataCPPT_AkhirDokter = Nothing
                Exit Function
            End If
            GetDataCPPT_AkhirDokter = oConnection.db.S_REQ_CPPTs.Where(Function(x) x.KDPENDAFTARAN = Parameter And x.PROFESI = "DOKTER").OrderByDescending(Function(x) x.KDCPPT).FirstOrDefault()
        End Function
        Public Function GetDataAsesmenIGD(ByVal Parameter As String) As S_DIGITAL_IGD_01
            If Not oConnection.GetConnection() Then
                GetDataAsesmenIGD = Nothing
                Exit Function
            End If
            GetDataAsesmenIGD = oConnection.db.S_DIGITAL_IGD_01s.FirstOrDefault(Function(x) x.KDPENDAFTARAN = Parameter)
        End Function
        'Public Function GetDataHariIniByUser(ByVal Parameter As String) As S_REQ_CPPT_LAINNYA
        '    If Not oConnection.GetConnection() Then
        '        GetDataHariIniByUser = Nothing
        '        Exit Function
        '    End If
        '    GetDataHariIniByUser = oConnection.db.S_REQ_CPPT_LAINNYAs.FirstOrDefault(Function(x) x.KDCPPT = Parameter)
        'End Function
        Public Function GetDataDetailObatHariIni(ByVal sDATE As DateTime, ByVal KDCPPT As String) As List(Of S_REQ_CPPT_OBATRAWATINAP)
            If Not oConnection.GetConnection() Then
                GetDataDetailObatHariIni = Nothing
                Exit Function
            End If
            GetDataDetailObatHariIni = oConnection.db.S_REQ_CPPT_OBATRAWATINAPs.Where(Function(x) x.KDCPPT <> KDCPPT And x.S_REQ_CPPT.DATE.Year = Year(sDATE) And x.S_REQ_CPPT.DATE.Month = Month(sDATE) And x.S_REQ_CPPT.DATE.Day = Day(sDATE)).ToList()
        End Function
        Public Function GetDataDetailDiagnosa(ByVal sKDCPPT As String) As List(Of S_REQ_CPPT_DIAGNOSA)
            If Not oConnection.GetConnection() Then
                GetDataDetailDiagnosa = Nothing
                Exit Function
            End If
            GetDataDetailDiagnosa = oConnection.db.S_REQ_CPPT_DIAGNOSAs.Where(Function(x) x.KDCPPT = sKDCPPT).OrderBy(Function(x) x.SEQ).ToList()
        End Function
        Public Function GetDataDetailProsedur(ByVal sKDCPPT As String) As List(Of S_REQ_CPPT_PROSEDUR)
            If Not oConnection.GetConnection() Then
                GetDataDetailProsedur = Nothing
                Exit Function
            End If
            GetDataDetailProsedur = oConnection.db.S_REQ_CPPT_PROSEDURs.Where(Function(x) x.KDCPPT = sKDCPPT).OrderBy(Function(x) x.SEQ).ToList()
        End Function
        Public Function InsertDataCPPTGizidanFarmasi(ByVal entity As S_REQ_CPPT) As Boolean
            Try
                If Not oConnection.GetConnection() Then
                    InsertDataCPPTGizidanFarmasi = False
                    Exit Function
                End If

                sREFERENCE = entity.KDCPPT
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

                    entity.KDCPPT = AutoNumber(sMODUL, sLASTNUMBER + 1, entity.DATE)
                Catch ex As Exception
                    oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                    Throw ex
                End Try
                Try
                    oConnection.db.S_REQ_CPPTs.InsertOnSubmit(entity)
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

                InsertDataCPPTGizidanFarmasi = True
            Catch ex As Exception
                InsertDataCPPTGizidanFarmasi = False
                oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                Throw ex
            End Try
        End Function
        Public Function UpdateDataCPPTGizidanFarmasi(ByVal entity As S_REQ_CPPT) As Boolean
            Try
                If Not oConnection.GetConnection() Then
                    UpdateDataCPPTGizidanFarmasi = False
                    Exit Function
                End If

                sREFERENCE = entity.KDCPPT
                sSTATUS = "UPDATE"

                Dim ds = oConnection.db.S_REQ_CPPTs.FirstOrDefault(Function(x) x.KDCPPT = entity.KDCPPT)

                Try
                    oConnection.db.S_REQ_CPPTs.DeleteOnSubmit(ds)
                    oConnection.db.S_REQ_CPPTs.InsertOnSubmit(entity)
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

                UpdateDataCPPTGizidanFarmasi = True
            Catch ex As Exception
                UpdateDataCPPTGizidanFarmasi = False
                oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                Throw ex
            End Try
        End Function
        Public Function InsertData(ByVal entity As S_REQ_CPPT, ByVal entityLainnya As S_REQ_CPPT_LAINNYA, ByVal entityLainnyaTambah As S_REQ_CPPT_LAINNYA_TAMBAH, Optional entityLainnyaDiagnosa As List(Of S_REQ_CPPT_DIAGNOSA) = Nothing, Optional entityLainnyaProsedur As List(Of S_REQ_CPPT_PROSEDUR) = Nothing) As Boolean
            Try
                If Not oConnection.GetConnection() Then
                    InsertData = False
                    Exit Function
                End If

                sREFERENCE = entity.KDCPPT
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

                    entity.KDCPPT = AutoNumber(sMODUL, sLASTNUMBER + 1, entity.DATE)
                    entityLainnya.KDCPPT = entity.KDCPPT
                    entityLainnyaTambah.KDCPPT = entity.KDCPPT

                    For Each iLoop In entityLainnyaDiagnosa
                        iLoop.KDCPPT = entity.KDCPPT
                    Next

                    For Each iLoop In entityLainnyaProsedur
                        iLoop.KDCPPT = entity.KDCPPT
                    Next
                    'If entityDetailFarmasi IsNot Nothing Then
                    '    For Each iLoop In entityDetailFarmasi
                    '        iLoop.KDCPPT = entity.KDCPPT
                    '    Next
                    'End If
                    'If entityDetailBHP IsNot Nothing Then
                    '    For Each iLoop In entityDetailBHP
                    '        iLoop.KDCPPT = entity.KDCPPT
                    '    Next
                    'End If
                Catch ex As Exception
                    oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                    Throw ex
                End Try
                Try
                    oConnection.db.S_REQ_CPPTs.InsertOnSubmit(entity)
                    oConnection.db.S_REQ_CPPT_LAINNYAs.InsertOnSubmit(entityLainnya)
                    oConnection.db.S_REQ_CPPT_LAINNYA_TAMBAHs.InsertOnSubmit(entityLainnyaTambah)
                    If entityLainnyaDiagnosa.Count > 0 Then
                        oConnection.db.S_REQ_CPPT_DIAGNOSAs.InsertAllOnSubmit(entityLainnyaDiagnosa)
                    End If
                    If entityLainnyaProsedur.Count > 0 Then
                        oConnection.db.S_REQ_CPPT_PROSEDURs.InsertAllOnSubmit(entityLainnyaProsedur)
                    End If
                    'If entityDetailFarmasi.Count > 0 Then
                    '    oConnection.db.S_REQ_CPPT_OBATRAWATINAPs.InsertAllOnSubmit(entityDetailFarmasi)
                    'End If
                    'If entityDetailBHP.Count > 0 Then
                    '    oConnection.db.S_REQ_CPPT_PENUNJANGRAWATINAPs.InsertAllOnSubmit(entityDetailBHP)
                    'End If
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
        Public Function UpdateData(ByVal entity As S_REQ_CPPT, ByVal entityLainnya As S_REQ_CPPT_LAINNYA, ByVal entityLainnyaTambah As S_REQ_CPPT_LAINNYA_TAMBAH, Optional entityLainnyaDiagnosa As List(Of S_REQ_CPPT_DIAGNOSA) = Nothing, Optional entityLainnyaProsedur As List(Of S_REQ_CPPT_PROSEDUR) = Nothing) As Boolean
            Try
                If Not oConnection.GetConnection() Then
                    UpdateData = False
                    Exit Function
                End If

                sREFERENCE = entity.KDCPPT
                sSTATUS = "UPDATE"

                Dim ds = oConnection.db.S_REQ_CPPTs.FirstOrDefault(Function(x) x.KDCPPT = entity.KDCPPT)
                Dim dsLainnya = oConnection.db.S_REQ_CPPT_LAINNYAs.FirstOrDefault(Function(x) x.KDCPPT = entity.KDCPPT)
                Dim dsLainnyaTambah = oConnection.db.S_REQ_CPPT_LAINNYA_TAMBAHs.FirstOrDefault(Function(x) x.KDCPPT = entity.KDCPPT)
                'Dim dsDetail = oConnection.db.S_REQ_CPPT_TINDAKANRAWATINAPs.Where(Function(x) x.KDCPPT = entity.KDCPPT)
                'Dim dsDetailFarmasi = oConnection.db.S_REQ_CPPT_OBATRAWATINAPs.Where(Function(x) x.KDCPPT = entity.KDCPPT)
                'Dim dsDetailBHP = oConnection.db.S_REQ_CPPT_PENUNJANGRAWATINAPs.Where(Function(x) x.KDCPPT = entity.KDCPPT)
                Dim dsDetailDiagnosa = oConnection.db.S_REQ_CPPT_DIAGNOSAs.Where(Function(x) x.KDCPPT = entity.KDCPPT)
                Dim dsDetailProsedur = oConnection.db.S_REQ_CPPT_PROSEDURs.Where(Function(x) x.KDCPPT = entity.KDCPPT)

                Try
                    oConnection.db.S_REQ_CPPTs.DeleteOnSubmit(ds)
                    oConnection.db.S_REQ_CPPTs.InsertOnSubmit(entity)

                    If dsLainnya IsNot Nothing Then
                        oConnection.db.S_REQ_CPPT_LAINNYAs.DeleteOnSubmit(dsLainnya)
                    End If
                    oConnection.db.S_REQ_CPPT_LAINNYAs.InsertOnSubmit(entityLainnya)
                    If dsLainnyaTambah IsNot Nothing Then
                        oConnection.db.S_REQ_CPPT_LAINNYA_TAMBAHs.DeleteOnSubmit(dsLainnyaTambah)
                    End If
                    oConnection.db.S_REQ_CPPT_LAINNYA_TAMBAHs.InsertOnSubmit(entityLainnyaTambah)

                    If dsDetailDiagnosa.Count > 0 Then
                        oConnection.db.S_REQ_CPPT_DIAGNOSAs.DeleteAllOnSubmit(dsDetailDiagnosa)
                    End If
                    If entityLainnyaDiagnosa.Count > 0 Then
                        oConnection.db.S_REQ_CPPT_DIAGNOSAs.InsertAllOnSubmit(entityLainnyaDiagnosa)
                    End If
                    If dsDetailProsedur.Count > 0 Then
                        oConnection.db.S_REQ_CPPT_PROSEDURs.DeleteAllOnSubmit(dsDetailProsedur)
                    End If
                    If entityLainnyaProsedur.Count > 0 Then
                        oConnection.db.S_REQ_CPPT_PROSEDURs.InsertAllOnSubmit(entityLainnyaProsedur)
                    End If
                    'If dsDetailFarmasi.Count > 0 Then
                    '    oConnection.db.S_REQ_CPPT_OBATRAWATINAPs.DeleteAllOnSubmit(dsDetailFarmasi)
                    'End If
                    'If entityDetailFarmasi IsNot Nothing Then
                    '    oConnection.db.S_REQ_CPPT_OBATRAWATINAPs.InsertAllOnSubmit(entityDetailFarmasi)
                    'End If
                    'If dsDetailBHP.Count > 0 Then
                    '    oConnection.db.S_REQ_CPPT_PENUNJANGRAWATINAPs.DeleteAllOnSubmit(dsDetailBHP)
                    'End If
                    'If entityDetailBHP IsNot Nothing Then
                    '    oConnection.db.S_REQ_CPPT_PENUNJANGRAWATINAPs.InsertAllOnSubmit(entityDetailBHP)
                    'End If
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

                Dim ds = oConnection.db.S_REQ_CPPTs.FirstOrDefault(Function(x) x.KDCPPT = Parameter)
                Dim dsLainnya = oConnection.db.S_REQ_CPPT_LAINNYAs.FirstOrDefault(Function(x) x.KDCPPT = Parameter)
                Dim dsLainnyaTambah = oConnection.db.S_REQ_CPPT_LAINNYA_TAMBAHs.FirstOrDefault(Function(x) x.KDCPPT = Parameter)
                Dim dsDetail = oConnection.db.S_REQ_CPPT_TINDAKANRAWATINAPs.Where(Function(x) x.KDCPPT = Parameter)
                Dim dsDetailFarmasi = oConnection.db.S_REQ_CPPT_OBATRAWATINAPs.Where(Function(x) x.KDCPPT = Parameter)
                Dim dsDetaiLBHP = oConnection.db.S_REQ_CPPT_PENUNJANGRAWATINAPs.Where(Function(x) x.KDCPPT = Parameter)

                Try
                    oConnection.db.S_REQ_CPPTs.DeleteOnSubmit(ds)
                    oConnection.db.S_REQ_CPPT_LAINNYAs.DeleteOnSubmit(dsLainnya)
                    oConnection.db.S_REQ_CPPT_LAINNYA_TAMBAHs.DeleteOnSubmit(dsLainnyaTambah)
                    If dsDetail IsNot Nothing Then
                        oConnection.db.S_REQ_CPPT_TINDAKANRAWATINAPs.DeleteAllOnSubmit(dsDetail)
                    End If
                    If dsDetailFarmasi IsNot Nothing Then
                        oConnection.db.S_REQ_CPPT_OBATRAWATINAPs.DeleteAllOnSubmit(dsDetailFarmasi)
                    End If
                    If dsDetaiLBHP IsNot Nothing Then
                        oConnection.db.S_REQ_CPPT_PENUNJANGRAWATINAPs.DeleteAllOnSubmit(dsDetaiLBHP)
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
        Public Function UpdatePemberiHandOver(ByVal KDCPPT As String, ByVal tgl As String, ByVal user As String, ByVal isPemberi As Boolean) As Boolean
            Try
                If Not oConnection.GetConnection Then
                    UpdatePemberiHandOver = False
                    Exit Function
                End If

                UpdatePemberiHandOver = True

                Dim ds = oConnection.db.S_REQ_CPPTs.FirstOrDefault(Function(x) x.KDCPPT = KDCPPT)

                If isPemberi = True Then
                    ds.HANDOVERPEMBERI_KDUSER = user
                    ds.HANDOVERPEMBERI_TANGGAL = tgl

                    oConnection.db.SubmitChanges()

                Else
                    ds.HANDOVERPENERIMA_KDUSER = user
                    ds.HANDOVERPENERIMA_TANGGAL = tgl

                    oConnection.db.SubmitChanges()

                End If

            Catch ex As Exception
                UpdatePemberiHandOver = False
                Throw ex
            End Try
        End Function
        Public Function UpdatePemberiNilaiKritis(ByVal KDCPPT As String, ByVal tgl As String, ByVal user As String, ByVal isPemberi As Boolean) As Boolean
            Try
                If Not oConnection.GetConnection Then
                    UpdatePemberiNilaiKritis = False
                    Exit Function
                End If

                UpdatePemberiNilaiKritis = True

                Dim ds = oConnection.db.S_REQ_CPPTs.FirstOrDefault(Function(x) x.KDCPPT = KDCPPT)

                If isPemberi = True Then
                    ds.NILAIKRITISPEMBERI_KDUSER = user
                    ds.NILAIKRITISPEMBERI_TANGGAL = tgl

                    oConnection.db.SubmitChanges()

                Else
                    ds.NILAIKRITISPENERIMA_KDUSER = user
                    ds.NILAIKRITISPENERIMA_TANGGAL = tgl

                    oConnection.db.SubmitChanges()

                End If

            Catch ex As Exception
                UpdatePemberiNilaiKritis = False
                Throw ex
            End Try
        End Function
        Public Function UpdatePemberiSBAR(ByVal KDCPPT As String, ByVal tgl As String, ByVal user As String, ByVal isPemberi As Boolean) As Boolean
            Try
                If Not oConnection.GetConnection Then
                    UpdatePemberiSBAR = False
                    Exit Function
                End If

                UpdatePemberiSBAR = True

                Dim ds = oConnection.db.S_REQ_CPPTs.FirstOrDefault(Function(x) x.KDCPPT = KDCPPT)

                If isPemberi = True Then
                    ds.SBARPEMBERI_KDUSER = user
                    ds.SBARPEMBERI_TANGGAL = tgl

                    oConnection.db.SubmitChanges()

                Else
                    ds.SBARPENERIMA_KDUSER = user
                    ds.SBARPENERIMA_TANGGAL = tgl

                    oConnection.db.SubmitChanges()

                End If

            Catch ex As Exception
                UpdatePemberiSBAR = False
                Throw ex
            End Try
        End Function
        Public Function UpdatePemberi(ByVal KDCPPT As String, ByVal keterangan As String) As Boolean
            Try
                If Not oConnection.GetConnection Then
                    UpdatePemberi = False
                    Exit Function
                End If

                UpdatePemberi = True

                Dim ds = oConnection.db.S_REQ_CPPTs.FirstOrDefault(Function(x) x.KDCPPT = KDCPPT)

                ds.AGAMA = ds.AGAMA & vbCrLf & "==========================" & vbCrLf & keterangan

                oConnection.db.SubmitChanges()

            Catch ex As Exception
                UpdatePemberi = False
                Throw ex
            End Try
        End Function
        Public Function UpdatenikSEBAGAIVERIFIKASI(ByVal KDCPPT As String, ByVal keterangan As String) As Boolean
            Try
                If Not oConnection.GetConnection Then
                    UpdatenikSEBAGAIVERIFIKASI = False
                    Exit Function
                End If

                UpdatenikSEBAGAIVERIFIKASI = True

                Dim ds = oConnection.db.S_REQ_CPPTs.FirstOrDefault(Function(x) x.KDCPPT = KDCPPT)

                ds.NIK = keterangan

                oConnection.db.SubmitChanges()

            Catch ex As Exception
                UpdatenikSEBAGAIVERIFIKASI = False
                Throw ex
            End Try
        End Function
    End Class
End Namespace