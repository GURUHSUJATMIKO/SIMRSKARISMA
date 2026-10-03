Imports System.Threading
Imports System.Data.SqlClient

Namespace Transaksi
    Public Class clsReq_Recipe
        Public oConnection As Setting.clsConnectionMain = Nothing
        Public oError As Setting.clsError = Nothing
        Public sMODUL As String = ""
        Public sREFERENCE As String = ""
        Public sSTATUS As String = ""
        Public sLASTNUMBER As Integer = 0
        Public oRequestRecipeTracking As Transaksi.clsRequestRecipeTracking = Nothing
        Public oReqCPPT As Transaksi.clsReqCPPT = Nothing
        Public oCounter As Setting.clsCounter = Nothing

        Public Sub New()
            oConnection = New Setting.clsConnectionMain
            oError = New Setting.clsError
            sMODUL = "REQ_RSPNEW"

            oCounter = New Setting.clsCounter
            oRequestRecipeTracking = New Transaksi.clsRequestRecipeTracking
            oReqCPPT = New Transaksi.clsReqCPPT
        End Sub
        Protected Overrides Sub Finalize()
            MyBase.Finalize()
            oConnection = Nothing
            GC.SuppressFinalize(Me)
        End Sub
        Public Function GetStructureHeader() As S_REQ_RECIPE_H
            If Not oConnection.GetConnection Then
                GetStructureHeader = Nothing
            End If
            GetStructureHeader = New S_REQ_RECIPE_H
        End Function
        Public Function GetStructureHeaderTambahan() As S_REQ_RECIPE_TAMBAHAN
            If Not oConnection.GetConnection Then
                GetStructureHeaderTambahan = Nothing
            End If
            GetStructureHeaderTambahan = New S_REQ_RECIPE_TAMBAHAN
        End Function
        Public Function GetStructureTelaah() As S_REQ_RECIPE_TELAAHRESEP
            If Not oConnection.GetConnection Then
                GetStructureTelaah = Nothing
            End If
            GetStructureTelaah = New S_REQ_RECIPE_TELAAHRESEP
        End Function
        Public Function GetStructureTelaahObat1() As S_REQ_RECIPE_TELAAHOBAT1
            If Not oConnection.GetConnection Then
                GetStructureTelaahObat1 = Nothing
            End If
            GetStructureTelaahObat1 = New S_REQ_RECIPE_TELAAHOBAT1
        End Function
        Public Function GetStructureTelaahObat2() As S_REQ_RECIPE_TELAAHOBAT2
            If Not oConnection.GetConnection Then
                GetStructureTelaahObat2 = Nothing
            End If
            GetStructureTelaahObat2 = New S_REQ_RECIPE_TELAAHOBAT2
        End Function
        Public Function GetStructureDetail() As S_REQ_RECIPE_D
            If Not oConnection.GetConnection Then
                GetStructureDetail = Nothing
            End If
            GetStructureDetail = New S_REQ_RECIPE_D
        End Function
        Public Function GetStructureDetailTindakanPoli() As S_DIGITAL_IGD_01_TINDAKANPOLI
            If Not oConnection.GetConnection Then
                GetStructureDetailTindakanPoli = Nothing
            End If
            GetStructureDetailTindakanPoli = New S_DIGITAL_IGD_01_TINDAKANPOLI
        End Function
        Public Function GetStructureDetailPenunjang() As S_DIGITAL_IGD_01_PENUNJANG
            If Not oConnection.GetConnection Then
                GetStructureDetailPenunjang = Nothing
            End If
            GetStructureDetailPenunjang = New S_DIGITAL_IGD_01_PENUNJANG
        End Function
        Public Function GetStructureDetailList() As List(Of S_REQ_RECIPE_D)
            If Not oConnection.GetConnection Then
                GetStructureDetailList = Nothing
            End If
            GetStructureDetailList = New List(Of S_REQ_RECIPE_D)
        End Function
        Public Function GetStructureDetailTindakanPoliList() As List(Of S_DIGITAL_IGD_01_TINDAKANPOLI)
            If Not oConnection.GetConnection Then
                GetStructureDetailTindakanPoliList = Nothing
            End If
            GetStructureDetailTindakanPoliList = New List(Of S_DIGITAL_IGD_01_TINDAKANPOLI)
        End Function
        Public Function GetStructureDetailPenunjangList() As List(Of S_DIGITAL_IGD_01_PENUNJANG)
            If Not oConnection.GetConnection Then
                GetStructureDetailPenunjangList = Nothing
            End If
            GetStructureDetailPenunjangList = New List(Of S_DIGITAL_IGD_01_PENUNJANG)
        End Function
        Public Function GetData() As List(Of S_REQ_RECIPE_H)
            If Not oConnection.GetConnection Then
                GetData = Nothing
                Exit Function
            End If
            GetData = oConnection.db.S_REQ_RECIPE_Hs.OrderByDescending(Function(x) x.KDREQRECIPE).ToList()
        End Function
        Public Function GetData(ByVal Parameter As String) As S_REQ_RECIPE_H
            If Not oConnection.GetConnection Then
                GetData = Nothing
                Exit Function
            End If
            GetData = oConnection.db.S_REQ_RECIPE_Hs.FirstOrDefault(Function(x) x.KDREQRECIPE = Parameter)
        End Function
        Public Function GetDataTambahan(ByVal Parameter As String) As S_REQ_RECIPE_TAMBAHAN
            If Not oConnection.GetConnection Then
                GetDataTambahan = Nothing
                Exit Function
            End If
            GetDataTambahan = oConnection.db.S_REQ_RECIPE_TAMBAHANs.FirstOrDefault(Function(x) x.KDREQRECIPE = Parameter)
        End Function
        Public Function GetDatakdreg(ByVal Parameter As String) As S_REQ_RECIPE_H
            If Not oConnection.GetConnection Then
                GetDatakdreg = Nothing
                Exit Function
            End If
            GetDatakdreg = oConnection.db.S_REQ_RECIPE_Hs.FirstOrDefault(Function(x) x.KDPENDAFTARAN = Parameter)
        End Function
        Public Function GetDataKodingByRegister(ByVal Parameter As String) As S_KODING_H
            If Not oConnection.GetConnection Then
                GetDataKodingByRegister = Nothing
                Exit Function
            End If
            GetDataKodingByRegister = oConnection.db.S_KODING_Hs.FirstOrDefault(Function(x) x.KDPENDAFTARAN = Parameter)
        End Function
        Public Function GetDataKoding(ByVal Parameter As String) As S_KODING_H
            If Not oConnection.GetConnection Then
                GetDataKoding = Nothing
                Exit Function
            End If
            GetDataKoding = oConnection.db.S_KODING_Hs.FirstOrDefault(Function(x) x.KDKODING = Parameter)
        End Function
        Public Function GetDataKodingByRM(ByVal Parameter As String) As List(Of S_KODING_H)
            If Not oConnection.GetConnection Then
                GetDataKodingByRM = Nothing
                Exit Function
            End If
            GetDataKodingByRM = oConnection.db.S_KODING_Hs.Where(Function(x) x.KDCUSTOMER = Parameter).ToList()
        End Function
        Public Function GetDataKodingTIndakan(ByVal Parameter As String) As List(Of S_KODING_TERAPI_DIAGNOSISPENYERTA)
            If Not oConnection.GetConnection Then
                GetDataKodingTIndakan = Nothing
                Exit Function
            End If
            GetDataKodingTIndakan = oConnection.db.S_KODING_TERAPI_DIAGNOSISPENYERTAs.Where(Function(x) x.S_KODING_H.KDPENDAFTARAN = Parameter).ToList()
        End Function
        Public Function GetDiagnosaTerakhir(ByVal Parameter As String, ByVal KDUSER As String) As S_REQ_RECIPE_H
            If Not oConnection.GetConnection Then
                GetDiagnosaTerakhir = Nothing
                Exit Function
            End If

            'Dim ds = oConnection.db.S_REQ_RECIPE_Hs.Where(Function(x) x.KDCUSTOMER = Parameter And x.NOIDUSER = KDUSER).OrderBy(Function(x) x.KDREQRECIPE)
            Dim ds = oConnection.db.S_REQ_RECIPE_Hs.Where(Function(x) x.KDCUSTOMER = Parameter And x.NOIDUSER = KDUSER).OrderByDescending(Function(x) x.KDREQRECIPE)

            GetDiagnosaTerakhir = ds.FirstOrDefault()
        End Function
        Public Function GetDataDiagnosa(ByVal Parameter As String) As S_KODING_H
            If Not oConnection.GetConnection Then
                GetDataDiagnosa = Nothing
                Exit Function
            End If
            GetDataDiagnosa = oConnection.db.S_KODING_Hs.FirstOrDefault(Function(x) x.KDPENDAFTARAN = Parameter)
        End Function
        Public Function GetDataDiagnosaUtama(ByVal Parameter As String) As S_KODING_DIAGNOSISUTAMA
            If Not oConnection.GetConnection Then
                GetDataDiagnosaUtama = Nothing
                Exit Function
            End If
            GetDataDiagnosaUtama = oConnection.db.S_KODING_DIAGNOSISUTAMAs.FirstOrDefault(Function(x) x.KDKODING = Parameter)
        End Function
        Public Function GetDataCetakLab(ByVal Parameter As String) As S_REQ_BHP_H
            If Not oConnection.GetConnection Then
                GetDataCetakLab = Nothing
                Exit Function
            End If
            GetDataCetakLab = oConnection.db.S_REQ_BHP_Hs.FirstOrDefault(Function(x) x.KDREQBHP = Parameter And x.S_REQ_BHP_Ds.FirstOrDefault.KETERANGAN_PENUNJANG = "TARIF LABORATORIUM")
        End Function
        Public Function GetDataCetakRontgen(ByVal Parameter As String) As S_REQ_BHP_H
            If Not oConnection.GetConnection Then
                GetDataCetakRontgen = Nothing
                Exit Function
            End If
            GetDataCetakRontgen = oConnection.db.S_REQ_BHP_Hs.FirstOrDefault(Function(x) x.KDREQBHP = Parameter And x.S_REQ_BHP_Ds.FirstOrDefault.KETERANGAN_PENUNJANG = "TARIF RADIOLOGI")
        End Function
        Public Function GetDataDetail() As List(Of S_REQ_RECIPE_D)
            If Not oConnection.GetConnection Then
                GetDataDetail = Nothing
                Exit Function
            End If
            GetDataDetail = oConnection.db.S_REQ_RECIPE_Ds.ToList()
        End Function
        Public Function GetDataByRekamMedis(ByVal Paramater As String) As List(Of S_REQ_RECIPE_H)
            If Not oConnection.GetConnection Then
                GetDataByRekamMedis = Nothing
                Exit Function
            End If
            GetDataByRekamMedis = oConnection.db.S_REQ_RECIPE_Hs.Where(Function(x) x.KDCUSTOMER = Paramater).OrderByDescending(Function(x) x.KDREQRECIPE).ToList()
        End Function
        Public Function GetDataDetail(ByVal Parameter As String) As List(Of S_REQ_RECIPE_D)
            If Not oConnection.GetConnection Then
                GetDataDetail = Nothing
                Exit Function
            End If
            GetDataDetail = oConnection.db.S_REQ_RECIPE_Ds.Where(Function(x) x.KDREQRECIPE = Parameter).ToList()
        End Function
        Public Function GetDataDetailDiagnosaPenyerta(ByVal Parameter As String) As List(Of S_KODING_DIAGNOSISPENYERTA)
            If Not oConnection.GetConnection Then
                GetDataDetailDiagnosaPenyerta = Nothing
                Exit Function
            End If
            GetDataDetailDiagnosaPenyerta = oConnection.db.S_KODING_DIAGNOSISPENYERTAs.Where(Function(x) x.KDKODING = Parameter).ToList()
        End Function
        Public Function GetDataDetailTindakan(ByVal Parameter As String) As List(Of S_KODING_TERAPI_DIAGNOSISPENYERTA)
            If Not oConnection.GetConnection Then
                GetDataDetailTindakan = Nothing
                Exit Function
            End If
            GetDataDetailTindakan = oConnection.db.S_KODING_TERAPI_DIAGNOSISPENYERTAs.Where(Function(x) x.KDKODING = Parameter).ToList()
        End Function
        Public Function GetDataDetailTindakan_(ByVal Parameter As String) As List(Of S_KODING_TERAPI)
            If Not oConnection.GetConnection Then
                GetDataDetailTindakan_ = Nothing
                Exit Function
            End If
            GetDataDetailTindakan_ = oConnection.db.S_KODING_TERAPIs.Where(Function(x) x.KDKODING = Parameter).ToList()
        End Function
        Public Function GetDataDetailRacikan(ByVal Parameter As String) As List(Of S_REQ_RECIPE_RACIKAN)
            If Not oConnection.GetConnection Then
                GetDataDetailRacikan = Nothing
                Exit Function
            End If
            GetDataDetailRacikan = oConnection.db.S_REQ_RECIPE_RACIKANs.Where(Function(x) x.KDREQRECIPE = Parameter).ToList()
        End Function
        Public Function GetDataByDate(ByVal sDateFrom As DateTime, ByVal sDateTo As DateTime) As List(Of S_REQ_RECIPE_H)
            If Not oConnection.GetConnection Then
                GetDataByDate = Nothing
                Exit Function
            End If
            GetDataByDate = oConnection.db.S_REQ_RECIPE_Hs.Where(Function(x) x.DATE >= sDateFrom.ToString("yyyy-MM-dd") & " 00:00:00" And x.DATE <= sDateTo.ToString("yyyy-MM-dd") & " 23:59:59").OrderByDescending(Function(x) x.KDREQRECIPE).ToList()
        End Function
        Public Function InsertData(ByVal Kecantikan As Boolean,ByVal entity As S_REQ_RECIPE_H, ByVal entityDetail As List(Of S_REQ_RECIPE_D), ByVal entityDetailTelaah1 As S_REQ_RECIPE_TELAAHRESEP, ByVal entityDetailTelaah2 As S_REQ_RECIPE_TELAAHOBAT1, ByVal entityDetailTelaah3 As S_REQ_RECIPE_TELAAHOBAT2, ByVal entityKoding As S_KODING_H, ByVal entityDetailDiagnosisPenyerta As List(Of S_KODING_DIAGNOSISPENYERTA), ByVal entityDetailDiagnosisPenyertaTindakan As List(Of S_KODING_TERAPI_DIAGNOSISPENYERTA), ByVal entityDetailTindakan As List(Of S_KODING_TERAPI), ByVal entityPemeriksaan As S_REQ_AKHIRPEMERIKSAAN, ByVal ConnOld As String, Optional entityDetailObatRacikan As List(Of S_REQ_RECIPE_RACIKAN) = Nothing, Optional entityTambahan As S_REQ_RECIPE_TAMBAHAN = Nothing) As String
            Try
                If Not oConnection.GetConnection Then
                    InsertData = ""
                    Exit Function
                End If

                sREFERENCE = entity.KDREQRECIPE
                sSTATUS = "INSERT"

                'Generate Auto Number
                Try
                    sLASTNUMBER = oCounter.GetLastNumberdDay(sMODUL, entity.DATE)
                    If sLASTNUMBER = 0 Then
                        Try
                            oCounter.InsertData(sMODUL, entity.DATE)
                            sLASTNUMBER = oCounter.GetLastNumber(sMODUL, entity.DATE)
                        Catch ex As Exception
                            sLASTNUMBER = 0
                        End Try
                    End If

                    Try
                        oCounter.UpdateData(sMODUL, sLASTNUMBER + 1, Day(entity.DATE), Month(entity.DATE), Year(entity.DATE))
                    Catch ex As Exception
                        oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                        Throw ex
                    End Try

                    entity.KDREQRECIPE = AutoNumberRegister(sMODUL, sLASTNUMBER + 1, entity.DATE)

                    For Each iLoop In entityDetail
                        iLoop.KDREQRECIPE = entity.KDREQRECIPE
                    Next

                    For Each iLoop In entityDetailDiagnosisPenyerta
                        iLoop.KDKODING = entity.KDREQRECIPE
                    Next

                    For Each iLoop In entityDetailDiagnosisPenyertaTindakan
                        iLoop.KDKODING = entity.KDREQRECIPE
                    Next

                    For Each iLoop In entityDetailTindakan
                        iLoop.KDKODING = entity.KDREQRECIPE
                    Next

                    entityTambahan.KDREQRECIPE = entity.KDREQRECIPE

                    entityDetailTelaah1.KDREQRECIPE = entity.KDREQRECIPE
                    entityDetailTelaah2.KDREQRECIPE = entity.KDREQRECIPE
                    entityDetailTelaah3.KDREQRECIPE = entity.KDREQRECIPE
                    entityPemeriksaan.KDAKHIRPEMERIKSAAN = entity.KDREQRECIPE

                    If entityDetailObatRacikan IsNot Nothing Then
                        For Each iLoop In entityDetailObatRacikan
                            iLoop.KDREQRECIPE = entity.KDREQRECIPE
                        Next
                    End If
                Catch ex As Exception
                    oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                    Throw ex
                End Try

                'End Generate
                Dim oKoding As New Admission.clsKoding
                ' ***** KODING *****
                Dim dsKodingUtama = oKoding.GetStructureHeaderUtama

                Try
                    oConnection.db.S_REQ_RECIPE_Hs.InsertOnSubmit(entity)
                    oConnection.db.S_REQ_RECIPE_Ds.InsertAllOnSubmit(entityDetail)
                    oConnection.db.S_REQ_RECIPE_TELAAHRESEPs.InsertOnSubmit(entityDetailTelaah1)
                    oConnection.db.S_REQ_RECIPE_TELAAHOBAT1s.InsertOnSubmit(entityDetailTelaah2)
                    oConnection.db.S_REQ_RECIPE_TELAAHOBAT2s.InsertOnSubmit(entityDetailTelaah3)
                    oConnection.db.S_REQ_AKHIRPEMERIKSAANs.InsertOnSubmit(entityPemeriksaan)
                    oConnection.db.S_REQ_RECIPE_TAMBAHANs.InsertOnSubmit(entityTambahan)

                    If entityDetailObatRacikan IsNot Nothing Then
                        oConnection.db.S_REQ_RECIPE_RACIKANs.InsertAllOnSubmit(entityDetailObatRacikan)
                    End If

                    If entityKoding IsNot Nothing Then
                        entityKoding.KDKODING = entity.KDREQRECIPE

                        With dsKodingUtama
                            .KDKODING = entity.KDREQRECIPE
                            .KDDIAGNOSA = "-"
                            .KETERANGAN = entity.DIAGNOSA
                        End With

                        'oKoding.InsertData(entityKoding, dsKodingUtama)

                        Try
                            oConnection.db.S_KODING_Hs.InsertOnSubmit(entityKoding)
                            'transaction.Complete()
                        Catch ex As Exception
                            oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                            Throw ex
                        End Try

                        Try
                            If dsKodingUtama IsNot Nothing Then
                                oConnection.db.S_KODING_DIAGNOSISUTAMAs.InsertOnSubmit(dsKodingUtama)
                            End If
                        Catch ex As Exception
                            oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                            Throw ex
                        End Try

                        If entityDetailDiagnosisPenyerta IsNot Nothing Then
                            oConnection.db.S_KODING_DIAGNOSISPENYERTAs.InsertAllOnSubmit(entityDetailDiagnosisPenyerta)
                        End If

                        If entityDetailDiagnosisPenyertaTindakan IsNot Nothing Then
                            oConnection.db.S_KODING_TERAPI_DIAGNOSISPENYERTAs.InsertAllOnSubmit(entityDetailDiagnosisPenyertaTindakan)
                        End If

                        If entityDetailTindakan IsNot Nothing Then
                            oConnection.db.S_KODING_TERAPIs.InsertAllOnSubmit(entityDetailTindakan)
                        End If
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

                InsertData = entity.KDREQRECIPE

                Try
                    fn_SaveCPPT(IIf(Kecantikan =False,"DOKTER","DOKTER SKIN"), entity, entityPemeriksaan, entityDetailDiagnosisPenyerta, entityDetailTindakan, entityDetailDiagnosisPenyertaTindakan, entityKoding, entityTambahan)
                Catch ex As Exception
                    oError.InsertData("CPPT", sSTATUS, ex.ToString, sREFERENCE)
                    Throw ex
                End Try

                Try
                    Dim ds = oRequestRecipeTracking.GetStructureHeader
                    With ds
                        .DATE = Now
                        .KDREQRECIPE = entity.KDREQRECIPE
                        .NAMAPASIEN = ""
                        .KDCUSTOMER = entity.KDCUSTOMER
                        .NOANTRIAN = entity.NOANTRIAN
                        .KONFIRMASIRESEP = entity.KONFIRMASIRESEP
                        .KDCUSTOMER = entity.KDCUSTOMER
                    End With

                    oRequestRecipeTracking.InsertData(ds)
                Catch ex As Exception
                    oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                    Throw ex
                End Try

            Catch ex As Exception
                InsertData = ""
                oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                Throw ex
            End Try
        End Function
        Public Function UpdateData(ByVal Kecantikan As Boolean,ByVal entity As S_REQ_RECIPE_H, ByVal entityDetail As List(Of S_REQ_RECIPE_D), ByVal entityKoding As S_KODING_H, ByVal entityDetailDiagnosisPenyerta As List(Of S_KODING_DIAGNOSISPENYERTA), ByVal entityDetailDiagnosisPenyertaTindakan As List(Of S_KODING_TERAPI_DIAGNOSISPENYERTA), ByVal entityDetailTindakan As List(Of S_KODING_TERAPI), ByVal entityPemeriksaan As S_REQ_AKHIRPEMERIKSAAN, Optional entityDetailObatRacikan As List(Of S_REQ_RECIPE_RACIKAN) = Nothing, Optional entityTambahan As S_REQ_RECIPE_TAMBAHAN = Nothing) As Boolean
            Try
                If Not oConnection.GetConnection Then
                    UpdateData = False
                    Exit Function
                End If

                sREFERENCE = entity.KDREQRECIPE
                sSTATUS = "UPDATE"

                Dim ds = oConnection.db.S_REQ_RECIPE_Hs.FirstOrDefault(Function(x) x.KDREQRECIPE = entity.KDREQRECIPE)

                If ds IsNot Nothing Then
                    oConnection.db.S_REQ_RECIPE_Hs.DeleteOnSubmit(ds)
                    oConnection.db.S_REQ_RECIPE_Hs.InsertOnSubmit(entity)
                End If

                Dim dsTambahan = oConnection.db.S_REQ_RECIPE_TAMBAHANs.FirstOrDefault(Function(x) x.KDREQRECIPE = entity.KDREQRECIPE)

                If dsTambahan IsNot Nothing Then
                    oConnection.db.S_REQ_RECIPE_TAMBAHANs.DeleteOnSubmit(dsTambahan)
                    oConnection.db.S_REQ_RECIPE_TAMBAHANs.InsertOnSubmit(entityTambahan)
                End If

                Dim dsDetail = oConnection.db.S_REQ_RECIPE_Ds.Where(Function(x) x.KDREQRECIPE = entity.KDREQRECIPE)

                If dsDetail.Count > 0 Then
                    oConnection.db.S_REQ_RECIPE_Ds.DeleteAllOnSubmit(dsDetail)
                    oConnection.db.S_REQ_RECIPE_Ds.InsertAllOnSubmit(entityDetail)
                Else
                    If entityDetail.Count > 0 Then
                        oConnection.db.S_REQ_RECIPE_Ds.InsertAllOnSubmit(entityDetail)
                    End If
                End If

                Dim dsDetailObatRacikan = oConnection.db.S_REQ_RECIPE_RACIKANs.Where(Function(x) x.KDREQRECIPE = entity.KDREQRECIPE)

                If dsDetailObatRacikan.Count > 0 Then
                    oConnection.db.S_REQ_RECIPE_RACIKANs.DeleteAllOnSubmit(dsDetailObatRacikan)
                    oConnection.db.S_REQ_RECIPE_RACIKANs.InsertAllOnSubmit(entityDetailObatRacikan)
                Else
                    If entityDetailObatRacikan.Count > 0 Then
                        oConnection.db.S_REQ_RECIPE_RACIKANs.InsertAllOnSubmit(entityDetailObatRacikan)
                    End If
                End If

                Dim dsDetail_ = oConnection.db.S_KODING_TERAPIs.Where(Function(x) x.KDKODING = entity.KDREQRECIPE)

                If dsDetail_.Count > 0 Then
                    oConnection.db.S_KODING_TERAPIs.DeleteAllOnSubmit(dsDetail_)
                    oConnection.db.S_KODING_TERAPIs.InsertAllOnSubmit(entityDetailTindakan)
                Else
                    If entityDetailTindakan.Count > 0 Then
                        oConnection.db.S_KODING_TERAPIs.InsertAllOnSubmit(entityDetailTindakan)
                    End If
                End If


                Dim dsPemeriksaan = oConnection.db.S_REQ_AKHIRPEMERIKSAANs.FirstOrDefault(Function(x) x.KDAKHIRPEMERIKSAAN = entity.KDREQRECIPE)

                If dsPemeriksaan IsNot Nothing Then
                    oConnection.db.S_REQ_AKHIRPEMERIKSAANs.DeleteOnSubmit(dsPemeriksaan)
                    oConnection.db.S_REQ_AKHIRPEMERIKSAANs.InsertOnSubmit(entityPemeriksaan)
                Else
                    If entityPemeriksaan IsNot Nothing Then
                        oConnection.db.S_REQ_AKHIRPEMERIKSAANs.InsertOnSubmit(entityPemeriksaan)
                    End If
                End If

                Dim dsDetailKoding = oConnection.db.S_KODING_Hs.FirstOrDefault(Function(x) x.KDKODING = entity.KDREQRECIPE)

                If dsDetailKoding IsNot Nothing Then

                    Dim oKoding As New Admission.clsKoding
                    ' ***** KODING *****
                    Dim dsKodingUtama = oKoding.GetStructureHeaderUtama
                    With dsKodingUtama
                        .KDKODING = entity.KDREQRECIPE
                        .KDDIAGNOSA = "-"
                        .KETERANGAN = entity.DIAGNOSA
                    End With

                    oKoding.UpdateData(entityKoding, dsKodingUtama, entityDetailDiagnosisPenyerta, entityDetailDiagnosisPenyertaTindakan)
                End If

                Try
                    oConnection.db.SubmitChanges()
                Catch ex As Exception
                    oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                    Throw ex
                End Try

                UpdateData = True

                Try
                    fn_SaveCPPT(IIf(Kecantikan =False,"DOKTER","DOKTER SKIN"), entity, entityPemeriksaan, entityDetailDiagnosisPenyerta, entityDetailTindakan, entityDetailDiagnosisPenyertaTindakan, entityKoding, entityTambahan)
                Catch ex As Exception
                    oError.InsertData("CPPT", sSTATUS, ex.ToString, sREFERENCE)
                    Throw ex
                End Try

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

                sREFERENCE = Parameter
                sSTATUS = "DELETE"

                Dim ds = oConnection.db.S_REQ_RECIPE_Hs.FirstOrDefault(Function(x) x.KDREQRECIPE = Parameter)
                Dim dsDetail = oConnection.db.S_REQ_RECIPE_Ds.Where(Function(x) x.KDREQRECIPE = Parameter)
                Dim dsDetailTelaah1 = oConnection.db.S_REQ_RECIPE_TELAAHRESEPs.FirstOrDefault(Function(x) x.KDREQRECIPE = Parameter)
                Dim dsDetailTelaah2 = oConnection.db.S_REQ_RECIPE_TELAAHOBAT1s.FirstOrDefault(Function(x) x.KDREQRECIPE = Parameter)
                Dim dsDetailTelaah3 = oConnection.db.S_REQ_RECIPE_TELAAHOBAT2s.FirstOrDefault(Function(x) x.KDREQRECIPE = Parameter)
                Dim dsRequestTracking = oConnection.db.S_REQ_RECIPE_TRACKINGs.FirstOrDefault(Function(x) x.KDREQRECIPE = Parameter)

                Try
                    oConnection.db.S_REQ_RECIPE_Hs.DeleteOnSubmit(ds)
                Catch ex As Exception
                    oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                    Throw ex
                End Try
                Try
                    oConnection.db.S_REQ_RECIPE_Ds.DeleteAllOnSubmit(dsDetail)
                    oConnection.db.S_REQ_RECIPE_TELAAHRESEPs.DeleteOnSubmit(dsDetailTelaah1)
                    oConnection.db.S_REQ_RECIPE_TELAAHOBAT1s.DeleteOnSubmit(dsDetailTelaah2)
                    oConnection.db.S_REQ_RECIPE_TELAAHOBAT2s.DeleteOnSubmit(dsDetailTelaah3)
                    oConnection.db.S_REQ_RECIPE_TRACKINGs.DeleteOnSubmit(dsRequestTracking)
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
        Public Function InsertOld(ByVal sConnOld As String, ByVal entityDIagnosaUtama As S_KODING_DIAGNOSISUTAMA, ByVal entityKoding As S_KODING_H, ByVal entityDetailDiagnosisPenyerta As List(Of S_KODING_DIAGNOSISPENYERTA)) As Boolean
            Try
                If Not oConnection.GetConnection Then
                    InsertOld = False
                    Exit Function
                End If

                sREFERENCE = entityKoding.KDKODING
                sSTATUS = "DELETE"

                Dim oConn_1 As New SqlConnection
                Dim oComm_1 As New SqlCommand
                Dim da_1 As SqlDataAdapter
                Dim ds_1 As New DataSet
                Dim SQL_1 As String

                oConn_1 = New SqlConnection(sConnOld)

                If oConn_1.State = ConnectionState.Closed Then
                    oConn_1.Open()
                End If

                SQL_1 = "INSERT INTO "
                SQL_1 &= "S_KODING_H "
                SQL_1 &= "( "
                SQL_1 &= "KDKODING "
                SQL_1 &= ", KDREG "
                SQL_1 &= ", DATECREATED "
                SQL_1 &= ", DATEUPDATED "
                SQL_1 &= ", DATE "
                SQL_1 &= ", DESCRIPTION "
                SQL_1 &= ", NOIDUSER "
                SQL_1 &= ") "
                SQL_1 &= "VALUES "
                SQL_1 &= "( "
                SQL_1 &= "'" & entityKoding.KDKODING & "' "
                SQL_1 &= ", '" & entityKoding.KDPENDAFTARAN & "' "
                SQL_1 &= ", '" & entityKoding.DATECREATED.ToString("yyyy-MM-dd 00:00:00") & "' "
                SQL_1 &= ", '" & entityKoding.DATEUPDATED.ToString("yyyy-MM-dd 00:00:00") & "' "
                SQL_1 &= ", '" & entityKoding.DATE.ToString("yyyy-MM-dd 00:00:00") & "' "
                SQL_1 &= ", '" & entityKoding.DESCRIPTION & "' "
                SQL_1 &= ", '" & entityKoding.NOIDUSER & "' "
                SQL_1 &= ") "

                oComm_1.Connection = oConn_1
                oComm_1.CommandText = SQL_1
                oComm_1.CommandTimeout = 120
                oComm_1.CommandType = CommandType.Text

                da_1 = New SqlDataAdapter(oComm_1)
                da_1.Fill(ds_1, "INERTINTOS_KODING_H")


                SQL_1 = "INSERT INTO "
                SQL_1 &= "S_KODING_H "
                SQL_1 &= "( "
                SQL_1 &= "KDKODING "
                SQL_1 &= ", KDDIAGNOSA "
                SQL_1 &= ", KETERANGAN "
                SQL_1 &= ") "
                SQL_1 &= "VALUES "
                SQL_1 &= "( "
                SQL_1 &= "'" & entityDIagnosaUtama.KDKODING & "' "
                SQL_1 &= ", '" & entityDIagnosaUtama.KDDIAGNOSA & "' "
                SQL_1 &= ", '" & entityDIagnosaUtama.KETERANGAN & "' "
                SQL_1 &= ") "

                oComm_1.Connection = oConn_1
                oComm_1.CommandText = SQL_1
                oComm_1.CommandTimeout = 120
                oComm_1.CommandType = CommandType.Text

                da_1 = New SqlDataAdapter(oComm_1)
                da_1.Fill(ds_1, "INERTINTOS_KODING_UTAMA")

                For Each xloop In entityDetailDiagnosisPenyerta
                    SQL_1 = "INSERT INTO "
                    SQL_1 &= "S_KODING_DIAGNOSISPENYERTA "
                    SQL_1 &= "( "
                    SQL_1 &= "  KDKODING "
                    SQL_1 &= ", KDDIAGNOSA "
                    SQL_1 &= ", SEQ "
                    SQL_1 &= ", KETERANGAN "
                    SQL_1 &= ") "
                    SQL_1 &= "VALUES "
                    SQL_1 &= "( "
                    SQL_1 &= "'" & xloop.KDKODING & "' "
                    SQL_1 &= ", '" & xloop.KDDIAGNOSA & "' "
                    SQL_1 &= ", '" & xloop.SEQ & "' "
                    SQL_1 &= ", '" & xloop.KETERANGAN & "' "
                    SQL_1 &= ") "

                    oComm_1.Connection = oConn_1
                    oComm_1.CommandText = SQL_1
                    oComm_1.CommandTimeout = 120
                    oComm_1.CommandType = CommandType.Text

                    da_1 = New SqlDataAdapter(oComm_1)
                    da_1.Fill(ds_1, "INERTINTOS_KODING_D1")
                Next

                If oConn_1.State = ConnectionState.Open Then
                    oConn_1.Close()
                End If

                InsertOld = True
            Catch ex As Exception
                InsertOld = False
                oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                Throw ex
            End Try
        End Function
        Public Function KonfirmasiResep(ByVal sKDREQRECIPE As String, ByVal sKONFIRMASIRESEP As String, ByVal sANTRIAN As String) As String
            Try
                If Not oConnection.GetConnection Then
                    KonfirmasiResep = False
                    Exit Function
                End If

                Try
                    Dim ds = oConnection.db.S_REQ_RECIPE_Hs.FirstOrDefault(Function(x) x.KDREQRECIPE = sKDREQRECIPE)

                    ds.KONFIRMASIRESEP = sKONFIRMASIRESEP

                    'Generate Auto Number
                    Try
                        sMODUL = sANTRIAN

                        sLASTNUMBER = oCounter.GetLastNumber(sMODUL, ds.DATE)

                        If sLASTNUMBER = 0 Then
                            Try
                                oCounter.InsertData(sMODUL, ds.DATE)
                                sLASTNUMBER = oCounter.GetLastNumber(sMODUL, ds.DATE)
                            Catch ex As Exception
                                sLASTNUMBER = 0
                            End Try
                        End If

                        ds.NOANTRIAN = AutoNumberAntrianFarmasi(sMODUL, sLASTNUMBER + 1)

                    Catch ex As Exception
                        oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                        Throw ex
                    End Try

                    'End Generate

                    oConnection.db.SubmitChanges()

                    Try
                        oCounter.UpdateData(sMODUL, sLASTNUMBER + 1, Day(ds.DATE), Month(ds.DATE), Year(ds.DATE))
                    Catch ex As Exception
                        oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                        Throw ex
                    End Try

                    KonfirmasiTracking(ds.KDREQRECIPE, sKONFIRMASIRESEP, ds.NOANTRIAN)

                Catch ex As Exception
                    oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                    Throw ex
                End Try

                KonfirmasiResep = True
            Catch ex As Exception
                KonfirmasiResep = False
                oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                Throw ex
            End Try
        End Function
        Public Function KonfirmasiFarmasi(ByVal sKDREQRECIPE As String, ByVal sKONFIRMASIRESEP As String, ByVal sNOANTRIAN As String) As String
            Try
                If Not oConnection.GetConnection Then
                    KonfirmasiFarmasi = False
                    Exit Function
                End If

                Dim sDate As DateTime = Now

                Try
                    Dim ds = oConnection.db.S_REQ_RECIPE_Hs.FirstOrDefault(Function(x) x.KDREQRECIPE = sKDREQRECIPE)

                    ds.KONFIRMASIRESEP = sKONFIRMASIRESEP

                    If sNOANTRIAN <> "" Then
                        ds.NOANTRIAN = sNOANTRIAN
                    End If

                    oConnection.db.SubmitChanges()

                    KonfirmasiTracking(ds.KDREQRECIPE, sKONFIRMASIRESEP, sNOANTRIAN)

                Catch ex As Exception
                    oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                    Throw ex
                End Try

                KonfirmasiFarmasi = True

            Catch ex As Exception
                KonfirmasiFarmasi = False
                oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                Throw ex
            End Try
        End Function
        Private Function KonfirmasiTracking(ByVal sKDREQRECIPE As String, ByVal sKONFIRMASIRESEP As String, ByVal sNOANTRIAN As String) As String
            Try
                If Not oConnection.GetConnection Then
                    KonfirmasiTracking = False
                    Exit Function
                End If

                Dim sDate As DateTime = Now

                Try
                    Dim ds = oConnection.db.S_REQ_RECIPE_TRACKINGs.FirstOrDefault(Function(x) x.KDREQRECIPE = sKDREQRECIPE)

                    ds.KONFIRMASIRESEP = sKONFIRMASIRESEP

                    If sNOANTRIAN <> "" Then
                        ds.NOANTRIAN = sNOANTRIAN
                    End If

                    oConnection.db.SubmitChanges()

                Catch ex As Exception
                    oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                    Throw ex
                End Try

                KonfirmasiTracking = True

            Catch ex As Exception
                KonfirmasiTracking = False
                oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                Throw ex
            End Try
        End Function
        Public Function Approval(ByVal sKDREQRECIPE As String) As String
            Try
                If Not oConnection.GetConnection Then
                    Approval = False
                    Exit Function
                End If

                Dim sDate As DateTime = Now

                Try
                    Dim ds = oConnection.db.S_REQ_RECIPE_Hs.FirstOrDefault(Function(x) x.KDREQRECIPE = sKDREQRECIPE)

                    ds.ISAPPROVAL = Not ds.ISAPPROVAL

                    oConnection.db.SubmitChanges()

                Catch ex As Exception
                    oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                    Throw ex
                End Try

                Approval = True

            Catch ex As Exception
                Approval = False
                oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                Throw ex
            End Try
        End Function
        Public Function DefaultDiagnosaTidakKepakai() As String
            Try
                If Not oConnection.GetConnection() Then
                    DefaultDiagnosaTidakKepakai = String.Empty
                    Exit Function
                End If

                Dim ds = oConnection.db.M_DIAGNOSAs.FirstOrDefault(Function(x) x.ISDEFAULT = True)
                If ds IsNot Nothing Then
                    DefaultDiagnosaTidakKepakai = ds.KDDIAGNOSA
                Else
                    DefaultDiagnosaTidakKepakai = String.Empty
                End If
            Catch ex As Exception
                DefaultDiagnosaTidakKepakai = String.Empty
                Throw ex
            End Try
        End Function
        Private Sub fn_SaveCPPT(ByVal PROFESI As String, ByVal entity As S_REQ_RECIPE_H, ByVal entityPemeriksaan As S_REQ_AKHIRPEMERIKSAAN, ByVal entityDetailDiagnosisPenyerta As List(Of S_KODING_DIAGNOSISPENYERTA), ByVal entityDetailTindakan As List(Of S_KODING_TERAPI), ByVal entityDetailDiagnosisPenyertaTindakan As List(Of S_KODING_TERAPI_DIAGNOSISPENYERTA), ByVal entityKoding As S_KODING_H, ByVal entityTambahan As S_REQ_RECIPE_TAMBAHAN)
            'CPPT 
            Dim dsReqPemeriksaanAkhir = oReqCPPT.GetDataByProfesidanRegister(PROFESI, entity.KDPENDAFTARAN)
            If dsReqPemeriksaanAkhir Is Nothing Then
                Dim dsCPPT = oReqCPPT.GetStructureHeader
                With dsCPPT
                    .DATECREATED = entity.DATECREATED
                    .DATEUPDATED = entity.DATEUPDATED
                    .DATE = entity.DATE
                    .KDCPPT = ""
                    .KDCUSTOMER = entity.KDCUSTOMER
                    .KDPENDAFTARAN = entity.KDPENDAFTARAN
                    .PROFESI = PROFESI
                    .NAMAPASIEN = entityPemeriksaan.NAMAPASIEN
                    .JK = entityPemeriksaan.OBJEKTIF_JENISKELAMIN
                    .NIK = ""
                    .TEMPATLAHIR = ""
                    .TANGGALLAHIR = entityPemeriksaan.TANGGALLAHIR
                    .AGAMA = ""
                    .PENJAMIN = entity.PENJAMIN
                    .NOTELEPON = ""
                    .SUKU = ""
                    .ALAMAT = ""
                    .SUBJEKTIF = entityPemeriksaan.SUBJEKTIF

                    Dim listSUBOBJEKTIF As New List(Of String)
                    'If entityPemeriksaan.OBJEKTIF_JENISKELAMIN <> "" Then
                    '    listSUBJEKTIF.Add("Jenis Kelamin : " & entityPemeriksaan.OBJEKTIF_JENISKELAMIN)
                    'End If
                    If entityPemeriksaan.OBJEKTIF_UMUR <> "" Then
                        listSUBOBJEKTIF.Add("Umur : " & entityPemeriksaan.OBJEKTIF_UMUR)
                    End If
                    If entityPemeriksaan.OBJEKTIF_BERATBADAN <> "" Then
                        If CDec(entityPemeriksaan.OBJEKTIF_BERATBADAN) <> 0 Then
                            listSUBOBJEKTIF.Add("BB : " & entityPemeriksaan.OBJEKTIF_BERATBADAN & " kg")
                            '0.00
                        End If
                    End If
                    If entityPemeriksaan.OBJEKTIF_TINGGIBADAN <> "" Then
                        If CDec(entityPemeriksaan.OBJEKTIF_TINGGIBADAN) <> 0 Then
                            listSUBOBJEKTIF.Add("TT : " & entityPemeriksaan.OBJEKTIF_TINGGIBADAN & " cm")
                        End If
                    End If
                    If entityPemeriksaan.OBJEKTIF_TEKANANDARAH <> "" Then
                        listSUBOBJEKTIF.Add("Tekanan Darah : " & entityPemeriksaan.OBJEKTIF_TEKANANDARAH & " mmHg")
                    End If
                    If entityPemeriksaan.OBJEKTIF_NADI <> "" Then
                        listSUBOBJEKTIF.Add("Nadi : " & entityPemeriksaan.OBJEKTIF_NADI & " x/mnt")
                    End If
                    If entityPemeriksaan.OBJEKTIF_RESPIRASI <> "" Then
                        listSUBOBJEKTIF.Add("Respirasi : " & entityPemeriksaan.OBJEKTIF_RESPIRASI & " x/mnt")
                    End If
                    If entityPemeriksaan.OBJEKTIF_SATURASIOKSIGEN <> "" Then
                        listSUBOBJEKTIF.Add("Saturasi Oksigen : " & entityPemeriksaan.OBJEKTIF_SATURASIOKSIGEN & " %")
                    End If
                    If entityPemeriksaan.OBJEKTIF_SUHU <> "" Then
                        listSUBOBJEKTIF.Add("Suhu : " & entityPemeriksaan.OBJEKTIF_SUHU & " oC")
                    End If
                    If entityPemeriksaan.DESKRIPSI <> "" Then
                        listSUBOBJEKTIF.Add("Pemeriksaan : " & entityPemeriksaan.DESKRIPSI)
                    End If

                    If entityTambahan.TAMBAH1 <> "" Then
                        listSUBOBJEKTIF.Add("Kesadaran : " & entityTambahan.TAMBAH1)
                    End If
                    If entityTambahan.TAMBAH2 <> "" Then
                        listSUBOBJEKTIF.Add("IMT : " & entityTambahan.TAMBAH2 & " %")
                    End If
                    If entityTambahan.TAMBAH3 <> "" Then
                        listSUBOBJEKTIF.Add("BB Ideal : " & entityTambahan.TAMBAH3 & " Kg")
                    End If
                    If entityTambahan.TAMBAH4 <> "" Then
                        listSUBOBJEKTIF.Add("BB Yang turunkan : " & entityTambahan.TAMBAH4 & " Kg")
                    End If

                    .OBJEKTIF = String.Join(vbCrLf, listSUBOBJEKTIF.ToArray)

                    Dim list As New List(Of String)
                    For Each xloop In entityDetailDiagnosisPenyerta
                        list.Add(xloop.KETERANGAN)
                    Next
                    If list.Count > 0 Then
                        .ASSEMENT = "Diagnosa Utama : " & entity.DIAGNOSA & vbCrLf & "Diagnosa Penyerta : " & String.Join(", ", list.ToArray)
                    Else
                        .ASSEMENT = "Diagnosa Utama : " & entity.DIAGNOSA
                    End If

                    Dim listTindakanPoli As New List(Of String)
                    Dim TindakanPoli As String = String.Empty
                    For Each xloop In entityDetailTindakan
                        listTindakanPoli.Add(xloop.KETERANGAN)
                    Next

                    If listTindakanPoli.Count > 0 Then
                        TindakanPoli = "Tindakan di Poli : " & String.Join(vbCrLf, listTindakanPoli.ToArray)
                    End If

                    Dim listPemeriksaanPenunjang As New List(Of String)
                    Dim PemeriksaanPenunjang As String = String.Empty
                    For Each xloop In entityDetailDiagnosisPenyertaTindakan
                        If xloop.TINDAKAN <> "" Then
                            listPemeriksaanPenunjang.Add(xloop.TINDAKAN)
                        End If
                    Next

                    If listPemeriksaanPenunjang.Count > 0 Then
                        PemeriksaanPenunjang = "Pemeriksaan Penunjang : " & String.Join(vbCrLf, listPemeriksaanPenunjang.ToArray)
                    End If


                    Dim listPemeriksaanObat As New List(Of String)
                    Dim PemeriksaanObat As String = String.Empty
                    For Each xloop In entityDetailDiagnosisPenyertaTindakan
                        If xloop.TERAPI <> "" Then
                            listPemeriksaanObat.Add(xloop.TERAPI)
                        End If
                    Next

                    If listPemeriksaanObat.Count > 0 Then
                        PemeriksaanObat = "Medikamentosa : " & String.Join(vbCrLf, listPemeriksaanObat.ToArray)
                    End If

                    Dim TindakLanjut As String = IIf(entityKoding.DESCRIPTION = "", "", "Tindak Lanjut : " & entityKoding.DESCRIPTION)

                    .PLANNING = TindakanPoli & vbCrLf & vbCrLf & PemeriksaanPenunjang & vbCrLf & vbCrLf & PemeriksaanObat & vbCrLf & vbCrLf & TindakLanjut
                    .KDUSER = entity.NOIDUSER
                    .ISCHEKED = False

                    .KETERANGAN_1 = ""
                    .KETERANGAN_2 = ""
                    .KETERANGAN_3 = ""

                    .HANDOVERPEMBERI_TANGGAL = ""
                    .HANDOVERPEMBERI_KDUSER = ""
                    .HANDOVERPENERIMA_TANGGAL = ""
                    .HANDOVERPENERIMA_KDUSER = ""

                    .NILAIKRITISPEMBERI_TANGGAL = ""
                    .NILAIKRITISPEMBERI_KDUSER = ""
                    .NILAIKRITISPENERIMA_TANGGAL = ""
                    .NILAIKRITISPENERIMA_KDUSER = ""

                    .SBARPEMBERI_TANGGAL = ""
                    .SBARPEMBERI_KDUSER = ""
                    .SBARPENERIMA_TANGGAL = ""
                    .SBARPENERIMA_KDUSER = ""
                End With

                Dim oCPPT As New Transaksi.clsCPPT

                Dim dsLainnya = oCPPT.GetStructureHeaderlainnya
                With dsLainnya
                    .KDCPPT = dsCPPT.KDCPPT
                    .ALERGI_TIDAK = entityPemeriksaan.ALERGI_TIDAK
                    .ALERGI_YA = entityPemeriksaan.ALERGI_YA
                    .ALERGI_TEXT = entityPemeriksaan.ALERGI_TEXT
                    .OBJEKTIF_JENISKELAMIN = entityPemeriksaan.OBJEKTIF_JENISKELAMIN
                    .OBJEKTIF_UMUR = entityPemeriksaan.OBJEKTIF_UMUR

                    .OBJEKTIF_BERATBADAN = entityPemeriksaan.OBJEKTIF_BERATBADAN
                    .OBJEKTIF_NADI = entityPemeriksaan.OBJEKTIF_NADI
                    .OBJEKTIF_RESPIRASI = entityPemeriksaan.OBJEKTIF_RESPIRASI
                    .OBJEKTIF_SATURASIOKSIGEN = entityPemeriksaan.OBJEKTIF_SATURASIOKSIGEN
                    .OBJEKTIF_SUHU = entityPemeriksaan.OBJEKTIF_SUHU
                    .OBJEKTIF_TEKANANDARAH = entityPemeriksaan.OBJEKTIF_TEKANANDARAH
                    .OBJEKTIF_TINGGIBADAN = entityPemeriksaan.OBJEKTIF_TINGGIBADAN
                    .INTRUKSILAIN = ""
                End With

                oReqCPPT.InsertData(dsCPPT, dsLainnya)
            Else
                Dim dsCPPT = oReqCPPT.GetStructureHeader
                With dsCPPT
                    .DATECREATED = dsReqPemeriksaanAkhir.DATECREATED
                    .DATEUPDATED = dsReqPemeriksaanAkhir.DATEUPDATED
                    .DATE = entity.DATE
                    .KDCPPT = dsReqPemeriksaanAkhir.KDCPPT
                    .KDCUSTOMER = entity.KDCUSTOMER
                    .KDPENDAFTARAN = entity.KDPENDAFTARAN
                    .PROFESI = "DOKTER"
                    .NAMAPASIEN = entityPemeriksaan.NAMAPASIEN
                    .JK = entityPemeriksaan.OBJEKTIF_JENISKELAMIN
                    .NIK = ""
                    .TEMPATLAHIR = ""
                    .TANGGALLAHIR = entityPemeriksaan.TANGGALLAHIR
                    .AGAMA = ""
                    .PENJAMIN = entity.PENJAMIN
                    .NOTELEPON = ""
                    .SUKU = ""
                    .ALAMAT = ""
                    .SUBJEKTIF = entityPemeriksaan.SUBJEKTIF
                    Dim listSUBOBJEKTIF As New List(Of String)
                    'If entityPemeriksaan.OBJEKTIF_JENISKELAMIN <> "" Then
                    '    listSUBJEKTIF.Add("Jenis Kelamin : " & entityPemeriksaan.OBJEKTIF_JENISKELAMIN)
                    'End If
                    If entityPemeriksaan.OBJEKTIF_UMUR <> "" Then
                        listSUBOBJEKTIF.Add("Umur : " & entityPemeriksaan.OBJEKTIF_UMUR)
                    End If
                    'If entityPemeriksaan.OBJEKTIF_BERATBADAN <> "" Then
                    '    listSUBOBJEKTIF.Add("BB : " & entityPemeriksaan.OBJEKTIF_BERATBADAN & " kg")
                    'End If
                    'If entityPemeriksaan.OBJEKTIF_TINGGIBADAN <> "" Then
                    '    listSUBOBJEKTIF.Add("TT : " & entityPemeriksaan.OBJEKTIF_TINGGIBADAN & " cm")
                    'End If
                    If entityPemeriksaan.OBJEKTIF_BERATBADAN <> "" Then
                        If CDec(entityPemeriksaan.OBJEKTIF_BERATBADAN) <> 0 Then
                            listSUBOBJEKTIF.Add("BB : " & entityPemeriksaan.OBJEKTIF_BERATBADAN & " kg")
                            '0.00
                        End If
                    End If
                    If entityPemeriksaan.OBJEKTIF_TINGGIBADAN <> "" Then
                        If CDec(entityPemeriksaan.OBJEKTIF_TINGGIBADAN) <> 0 Then
                            listSUBOBJEKTIF.Add("TT : " & entityPemeriksaan.OBJEKTIF_TINGGIBADAN & " cm")
                        End If
                    End If
                    If entityPemeriksaan.OBJEKTIF_TEKANANDARAH <> "" Then
                        listSUBOBJEKTIF.Add("Tekanan Darah : " & entityPemeriksaan.OBJEKTIF_TEKANANDARAH & " mmHg")
                    End If
                    If entityPemeriksaan.OBJEKTIF_NADI <> "" Then
                        listSUBOBJEKTIF.Add("Nadi : " & entityPemeriksaan.OBJEKTIF_NADI & " x/mnt")
                    End If
                    If entityPemeriksaan.OBJEKTIF_RESPIRASI <> "" Then
                        listSUBOBJEKTIF.Add("Respirasi : " & entityPemeriksaan.OBJEKTIF_RESPIRASI & " x/mnt")
                    End If
                    If entityPemeriksaan.OBJEKTIF_SATURASIOKSIGEN <> "" Then
                        listSUBOBJEKTIF.Add("Saturasi Oksigen : " & entityPemeriksaan.OBJEKTIF_SATURASIOKSIGEN & " %")
                    End If
                    If entityPemeriksaan.OBJEKTIF_SUHU <> "" Then
                        listSUBOBJEKTIF.Add("Suhu : " & entityPemeriksaan.OBJEKTIF_SUHU & " oC")
                    End If
                    If entityPemeriksaan.DESKRIPSI <> "" Then
                        listSUBOBJEKTIF.Add("Pemeriksaan : " & entityPemeriksaan.DESKRIPSI)
                    End If

                    If entityTambahan.TAMBAH1 <> "" Then
                        listSUBOBJEKTIF.Add("Kesadaran : " & entityTambahan.TAMBAH1)
                    End If
                    If entityTambahan.TAMBAH2 <> "" Then
                        listSUBOBJEKTIF.Add("IMT : " & entityTambahan.TAMBAH2 & " %")
                    End If
                    If entityTambahan.TAMBAH3 <> "" Then
                        listSUBOBJEKTIF.Add("BB Ideal : " & entityTambahan.TAMBAH3 & " Kg")
                    End If
                    If entityTambahan.TAMBAH4 <> "" Then
                        listSUBOBJEKTIF.Add("BB Yang turunkan : " & entityTambahan.TAMBAH4 & " Kg")
                    End If

                    .OBJEKTIF = String.Join(vbCrLf, listSUBOBJEKTIF.ToArray)

                    Dim list As New List(Of String)
                    For Each xloop In entityDetailDiagnosisPenyerta
                        list.Add(xloop.KETERANGAN)
                    Next
                    If list.Count > 0 Then
                        .ASSEMENT = "Diagnosa Utama : " & entity.DIAGNOSA & vbCrLf & "Diagnosa Penyerta : " & String.Join(", ", list.ToArray)
                    Else
                        .ASSEMENT = "Diagnosa Utama : " & entity.DIAGNOSA
                    End If

                    Dim listTindakanPoli As New List(Of String)
                    Dim TindakanPoli As String = String.Empty
                    For Each xloop In entityDetailTindakan
                        listTindakanPoli.Add(xloop.KETERANGAN)
                    Next

                    If listTindakanPoli.Count > 0 Then
                        TindakanPoli = "Tindakan di Poli : " & String.Join(vbCrLf, listTindakanPoli.ToArray)
                    End If

                    Dim listPemeriksaanPenunjang As New List(Of String)
                    Dim PemeriksaanPenunjang As String = String.Empty
                    For Each xloop In entityDetailDiagnosisPenyertaTindakan
                        If xloop.TINDAKAN <> "" Then
                            listPemeriksaanPenunjang.Add(xloop.TINDAKAN)
                        End If
                    Next

                    If listPemeriksaanPenunjang.Count > 0 Then
                        PemeriksaanPenunjang = "Pemeriksaan Penunjang : " & String.Join(vbCrLf, listPemeriksaanPenunjang.ToArray)
                    End If

                    Dim listPemeriksaanObat As New List(Of String)
                    Dim PemeriksaanObat As String = String.Empty
                    For Each xloop In entityDetailDiagnosisPenyertaTindakan
                        If xloop.TERAPI <> "" Then
                            listPemeriksaanObat.Add(xloop.TERAPI)
                        End If
                    Next

                    If listPemeriksaanObat.Count > 0 Then
                        PemeriksaanObat = "Medikamentosa : " & String.Join(vbCrLf, listPemeriksaanObat.ToArray)
                    End If

                    Dim TindakLanjut As String = IIf(entityKoding.DESCRIPTION = "", "", "Tindak Lanjut : " & entityKoding.DESCRIPTION)
                    .PLANNING = TindakanPoli & vbCrLf & vbCrLf & PemeriksaanPenunjang & vbCrLf & vbCrLf & PemeriksaanObat & vbCrLf & vbCrLf & TindakLanjut
                    .KDUSER = entity.NOIDUSER
                    .ISCHEKED = False


                    .KETERANGAN_1 = dsReqPemeriksaanAkhir.KETERANGAN_1
                    .KETERANGAN_2 = dsReqPemeriksaanAkhir.KETERANGAN_2
                    .KETERANGAN_3 = dsReqPemeriksaanAkhir.KETERANGAN_3

                    .HANDOVERPEMBERI_TANGGAL = dsReqPemeriksaanAkhir.HANDOVERPEMBERI_TANGGAL
                    .HANDOVERPEMBERI_KDUSER = dsReqPemeriksaanAkhir.HANDOVERPEMBERI_KDUSER
                    .HANDOVERPENERIMA_TANGGAL = dsReqPemeriksaanAkhir.HANDOVERPENERIMA_TANGGAL
                    .HANDOVERPENERIMA_KDUSER = dsReqPemeriksaanAkhir.HANDOVERPENERIMA_KDUSER

                    .NILAIKRITISPEMBERI_TANGGAL = dsReqPemeriksaanAkhir.NILAIKRITISPEMBERI_TANGGAL
                    .NILAIKRITISPEMBERI_KDUSER = dsReqPemeriksaanAkhir.NILAIKRITISPEMBERI_KDUSER
                    .NILAIKRITISPENERIMA_TANGGAL = dsReqPemeriksaanAkhir.NILAIKRITISPENERIMA_TANGGAL
                    .NILAIKRITISPENERIMA_KDUSER = dsReqPemeriksaanAkhir.NILAIKRITISPENERIMA_KDUSER

                    .SBARPEMBERI_TANGGAL = dsReqPemeriksaanAkhir.SBARPEMBERI_TANGGAL
                    .SBARPEMBERI_KDUSER = dsReqPemeriksaanAkhir.SBARPEMBERI_KDUSER
                    .SBARPENERIMA_TANGGAL = dsReqPemeriksaanAkhir.SBARPENERIMA_TANGGAL
                    .SBARPENERIMA_KDUSER = dsReqPemeriksaanAkhir.SBARPENERIMA_KDUSER
                End With

                Dim oCPPT As New Transaksi.clsCPPT

                Dim dsLainnya = oCPPT.GetStructureHeaderlainnya
                With dsLainnya
                    .KDCPPT = dsCPPT.KDCPPT
                    .ALERGI_TIDAK = entityPemeriksaan.ALERGI_TIDAK
                    .ALERGI_YA = entityPemeriksaan.ALERGI_YA
                    .ALERGI_TEXT = entityPemeriksaan.ALERGI_TEXT
                    .OBJEKTIF_JENISKELAMIN = entityPemeriksaan.OBJEKTIF_JENISKELAMIN
                    .OBJEKTIF_UMUR = entityPemeriksaan.OBJEKTIF_UMUR
                    .OBJEKTIF_BERATBADAN = entityPemeriksaan.OBJEKTIF_BERATBADAN
                    .OBJEKTIF_NADI = entityPemeriksaan.OBJEKTIF_NADI
                    .OBJEKTIF_RESPIRASI = entityPemeriksaan.OBJEKTIF_RESPIRASI
                    .OBJEKTIF_SATURASIOKSIGEN = entityPemeriksaan.OBJEKTIF_SATURASIOKSIGEN
                    .OBJEKTIF_SUHU = entityPemeriksaan.OBJEKTIF_SUHU
                    .OBJEKTIF_TEKANANDARAH = entityPemeriksaan.OBJEKTIF_TEKANANDARAH
                    .OBJEKTIF_TINGGIBADAN = entityPemeriksaan.OBJEKTIF_TINGGIBADAN
                    .INTRUKSILAIN = ""
                End With

                oReqCPPT.UpdateData(dsCPPT, dsLainnya)
            End If
        End Sub
    End Class
End Namespace