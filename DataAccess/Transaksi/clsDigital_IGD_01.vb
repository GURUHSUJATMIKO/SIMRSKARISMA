Imports System.Threading

Namespace Transaksi
    Public Class clsDigital_IGD_01
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
            sMODUL = "STR"

            oCounter = New Setting.clsCounter
        End Sub
        Protected Overrides Sub Finalize()
            MyBase.Finalize()
            oConnection = Nothing
            GC.SuppressFinalize(Me)
        End Sub
        Public Function GetStructureHeader() As S_DIGITAL_IGD_01
            If Not oConnection.GetConnection() Then
                GetStructureHeader = Nothing
            End If
            GetStructureHeader = New S_DIGITAL_IGD_01
        End Function
        Public Function GetStructureDetail() As S_DIGITAL_IGD_01_DIAGNOSA
            If Not oConnection.GetConnection() Then
                GetStructureDetail = Nothing
            End If
            GetStructureDetail = New S_DIGITAL_IGD_01_DIAGNOSA
        End Function
        Public Function GetStructureDetailList() As List(Of S_DIGITAL_IGD_01_DIAGNOSA)
            If Not oConnection.GetConnection() Then
                GetStructureDetailList = Nothing
            End If
            GetStructureDetailList = New List(Of S_DIGITAL_IGD_01_DIAGNOSA)
        End Function
        Public Function GetDataByRM(ByVal sKDCUSTOMER As String) As List(Of S_DIGITAL_IGD_01)
            If Not oConnection.GetConnection() Then
                GetDataByRM = Nothing
                Exit Function
            End If
            GetDataByRM = oConnection.db.S_DIGITAL_IGD_01s.Where(Function(x) x.KDCUSTOMER = sKDCUSTOMER).OrderByDescending(Function(x) x.KDPENDAFTARAN).ToList()
        End Function
        Public Function GetDataByRMTerakhir(ByVal sKDCUSTOMER As String) As S_DIGITAL_IGD_01
            If Not oConnection.GetConnection() Then
                GetDataByRMTerakhir = Nothing
                Exit Function
            End If
            GetDataByRMTerakhir = oConnection.db.S_DIGITAL_IGD_01s.Where(Function(x) x.KDCUSTOMER = sKDCUSTOMER).OrderByDescending(Function(x) x.KDPENDAFTARAN).FirstOrDefault()
        End Function
        Public Function GetData() As List(Of S_DIGITAL_IGD_01)
            If Not oConnection.GetConnection() Then
                GetData = Nothing
                Exit Function
            End If
            GetData = oConnection.db.S_DIGITAL_IGD_01s.OrderByDescending(Function(x) x.KDPENDAFTARAN).ToList()
        End Function
        Public Function GetData(ByVal Parameter As String) As S_DIGITAL_IGD_01
            If Not oConnection.GetConnection() Then
                GetData = Nothing
                Exit Function
            End If
            GetData = oConnection.db.S_DIGITAL_IGD_01s.FirstOrDefault(Function(x) x.KDPENDAFTARAN = Parameter)
        End Function
        Public Function GetDataDetail() As List(Of S_DIGITAL_IGD_01_DIAGNOSA)
            If Not oConnection.GetConnection() Then
                GetDataDetail = Nothing
                Exit Function
            End If
            GetDataDetail = oConnection.db.S_DIGITAL_IGD_01_DIAGNOSAs.ToList()
        End Function
        Public Function GetDataDetail(ByVal sKDPENDAFTARAN As String) As List(Of S_DIGITAL_IGD_01_DIAGNOSA)
            If Not oConnection.GetConnection() Then
                GetDataDetail = Nothing
                Exit Function
            End If
            GetDataDetail = oConnection.db.S_DIGITAL_IGD_01_DIAGNOSAs.Where(Function(x) x.KDPENDAFTARAN = sKDPENDAFTARAN).ToList()
        End Function
        Public Function GetDataDetailResep(ByVal sKDREQRECIPE As String) As List(Of S_REQ_RECIPE_D)
            If Not oConnection.GetConnection() Then
                GetDataDetailResep = Nothing
                Exit Function
            End If
            GetDataDetailResep = oConnection.db.S_REQ_RECIPE_Ds.Where(Function(x) x.KDREQRECIPE = sKDREQRECIPE).ToList()
        End Function
        'Public Function GetDataDetailResepObatPulang(ByVal sKDREQRECIPE As String) As List(Of S_REQ_RECIPE_D)
        '    If Not oConnection.GetConnection() Then
        '        GetDataDetailResepObatPulang = Nothing
        '        Exit Function
        '    End If
        '    GetDataDetailResepObatPulang = oConnection.db.S_REQ_RECIPE_Ds.Where(Function(x) x.KDREQRECIPE = sKDREQRECIPE And x.SEQ < 100).ToList()
        'End Function
        Public Function GetDataDetailResepSelamaIGD(ByVal sKDREQRECIPE As String) As List(Of S_REQ_RECIPE_D)
            If Not oConnection.GetConnection() Then
                GetDataDetailResepSelamaIGD = Nothing
                Exit Function
            End If
            GetDataDetailResepSelamaIGD = oConnection.db.S_REQ_RECIPE_Ds.Where(Function(x) x.KDREQRECIPE = sKDREQRECIPE And x.REMARKS_FARMASI = "OBAT IGD").ToList()
        End Function
        Public Function GetDataDetailResepRanap(ByVal sKDREQRECIPE As String) As List(Of S_REQ_RECIPE_D)
            If Not oConnection.GetConnection() Then
                GetDataDetailResepRanap = Nothing
                Exit Function
            End If
            GetDataDetailResepRanap = oConnection.db.S_REQ_RECIPE_Ds.Where(Function(x) x.KDREQRECIPE = sKDREQRECIPE And x.REMARKS_FARMASI = "OBAT RANAP").ToList()
        End Function
        Public Function GetDataDetailResepPulang(ByVal sKDREQRECIPE As String) As List(Of S_REQ_RECIPE_D)
            If Not oConnection.GetConnection() Then
                GetDataDetailResepPulang = Nothing
                Exit Function
            End If
            GetDataDetailResepPulang = oConnection.db.S_REQ_RECIPE_Ds.Where(Function(x) x.KDREQRECIPE = sKDREQRECIPE And x.REMARKS_FARMASI = "OBAT PULANG").ToList()
        End Function
        Public Function GetDataDetailTindakanPoli(ByVal sKDPENDAFTARAN As String) As List(Of S_DIGITAL_IGD_01_TINDAKANPOLI)
            If Not oConnection.GetConnection() Then
                GetDataDetailTindakanPoli = Nothing
                Exit Function
            End If
            GetDataDetailTindakanPoli = oConnection.db.S_DIGITAL_IGD_01_TINDAKANPOLIs.Where(Function(x) x.KDPENDAFTARAN = sKDPENDAFTARAN).ToList()
        End Function
        Public Function GetDataDetailPenunjang(ByVal sKDPENDAFTARAN As String) As List(Of S_DIGITAL_IGD_01_PENUNJANG)
            If Not oConnection.GetConnection() Then
                GetDataDetailPenunjang = Nothing
                Exit Function
            End If
            GetDataDetailPenunjang = oConnection.db.S_DIGITAL_IGD_01_PENUNJANGs.Where(Function(x) x.KDPENDAFTARAN = sKDPENDAFTARAN).ToList()
        End Function
        Public Function InsertData(ByVal entity As S_DIGITAL_IGD_01, ByVal entityResep_H As S_REQ_RECIPE_H, ByVal entityDetail As List(Of S_REQ_RECIPE_D), ByVal entityDetailTelaah1 As S_REQ_RECIPE_TELAAHRESEP, ByVal entityDetailTelaah2 As S_REQ_RECIPE_TELAAHOBAT1, ByVal entityDetailTelaah3 As S_REQ_RECIPE_TELAAHOBAT2, ByVal entityDetailTindakanPoli As List(Of S_DIGITAL_IGD_01_TINDAKANPOLI), ByVal entityDetailPenunjang As List(Of S_DIGITAL_IGD_01_PENUNJANG), ByVal entityKoding As S_KODING_H, ByVal entityDIagnosaUtama As S_KODING_DIAGNOSISUTAMA, ByVal entityDetailDiagnosisPenyertaTindakan As List(Of S_KODING_TERAPI_DIAGNOSISPENYERTA), ByVal entityDetailTindakan As List(Of S_KODING_TERAPI), ByVal entityDetailDiagnosa As List(Of S_DIGITAL_IGD_01_DIAGNOSA)) As Boolean
            Try
                If Not oConnection.GetConnection() Then
                    InsertData = False
                    Exit Function
                End If

                sREFERENCE = entity.KDPENDAFTARAN
                sSTATUS = "INSERT"

                Try
                    oConnection.db.S_DIGITAL_IGD_01s.InsertOnSubmit(entity)
                    oConnection.db.S_REQ_RECIPE_Hs.InsertOnSubmit(entityResep_H)
                    If entityKoding IsNot Nothing Then
                        oConnection.db.S_KODING_Hs.InsertOnSubmit(entityKoding)
                    End If
                    If entityDetail IsNot Nothing Then
                        oConnection.db.S_REQ_RECIPE_Ds.InsertAllOnSubmit(entityDetail)
                    End If
                    If entityDetailTelaah1 IsNot Nothing Then
                        oConnection.db.S_REQ_RECIPE_TELAAHRESEPs.InsertOnSubmit(entityDetailTelaah1)
                    End If
                    If entityDetailTelaah2 IsNot Nothing Then
                        oConnection.db.S_REQ_RECIPE_TELAAHOBAT1s.InsertOnSubmit(entityDetailTelaah2)
                    End If
                    If entityDetailTelaah3 IsNot Nothing Then
                        oConnection.db.S_REQ_RECIPE_TELAAHOBAT2s.InsertOnSubmit(entityDetailTelaah3)
                    End If
                    If entityDetailTindakanPoli IsNot Nothing Then
                        oConnection.db.S_DIGITAL_IGD_01_TINDAKANPOLIs.InsertAllOnSubmit(entityDetailTindakanPoli)
                    End If
                    If entityDetailPenunjang IsNot Nothing Then
                        oConnection.db.S_DIGITAL_IGD_01_PENUNJANGs.InsertAllOnSubmit(entityDetailPenunjang)
                    End If
                    If entityDIagnosaUtama IsNot Nothing Then
                        oConnection.db.S_KODING_DIAGNOSISUTAMAs.InsertOnSubmit(entityDIagnosaUtama)
                    End If
                    If entityDetailDiagnosisPenyertaTindakan IsNot Nothing Then
                        oConnection.db.S_KODING_TERAPI_DIAGNOSISPENYERTAs.InsertAllOnSubmit(entityDetailDiagnosisPenyertaTindakan)
                    End If
                    If entityDetailTindakan IsNot Nothing Then
                        oConnection.db.S_KODING_TERAPIs.InsertAllOnSubmit(entityDetailTindakan)
                    End If
                    If entityDetailDiagnosa IsNot Nothing Then
                        oConnection.db.S_DIGITAL_IGD_01_DIAGNOSAs.InsertAllOnSubmit(entityDetailDiagnosa)
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
                'Try
                '    oCounter.UpdateData(sMODUL, sLASTNUMBER + 1, Month(entity.DATE), Year(entity.DATE))
                'Catch ex As Exception
                '    oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                '    Throw ex
                'End Try

                InsertData = True
            Catch ex As Exception
                InsertData = False
                oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                Throw ex
            End Try
        End Function
        Public Function UpdateData(ByVal entity As S_DIGITAL_IGD_01, ByVal entityResep_H As S_REQ_RECIPE_H, ByVal entityDetail As List(Of S_REQ_RECIPE_D), ByVal entityDetailTindakanPoli As List(Of S_DIGITAL_IGD_01_TINDAKANPOLI), ByVal entityDetailPenunjang As List(Of S_DIGITAL_IGD_01_PENUNJANG), ByVal entityKoding As S_KODING_H, ByVal entityDIagnosaUtama As S_KODING_DIAGNOSISUTAMA, ByVal entityDetailDiagnosisPenyertaTindakan As List(Of S_KODING_TERAPI_DIAGNOSISPENYERTA), ByVal entityDetailTindakan As List(Of S_KODING_TERAPI), ByVal entityDetailDiagnosa As List(Of S_DIGITAL_IGD_01_DIAGNOSA)) As Boolean
            Try
                If Not oConnection.GetConnection() Then
                    UpdateData = False
                    Exit Function
                End If

                sREFERENCE = entity.KDPENDAFTARAN
                sSTATUS = "UPDATE"

                Dim ds = oConnection.db.S_DIGITAL_IGD_01s.FirstOrDefault(Function(x) x.KDPENDAFTARAN = entity.KDPENDAFTARAN)
                Dim dsResep = oConnection.db.S_REQ_RECIPE_Hs.FirstOrDefault(Function(x) x.KDREQRECIPE = "IGD_" & entity.KDPENDAFTARAN)
                Dim dsDetail = oConnection.db.S_REQ_RECIPE_Ds.Where(Function(x) x.KDREQRECIPE = "IGD_" & entity.KDPENDAFTARAN)
                Dim dsDetailTindakanPoli = oConnection.db.S_DIGITAL_IGD_01_TINDAKANPOLIs.Where(Function(x) x.KDPENDAFTARAN = entity.KDPENDAFTARAN)
                Dim dsDetailPenunjang = oConnection.db.S_DIGITAL_IGD_01_PENUNJANGs.Where(Function(x) x.KDPENDAFTARAN = entity.KDPENDAFTARAN)
                Dim dsKoding = oConnection.db.S_KODING_Hs.FirstOrDefault(Function(x) x.KDKODING = "IGD_" & entity.KDPENDAFTARAN)
                Dim dsDiagnosaUtama = oConnection.db.S_KODING_DIAGNOSISUTAMAs.FirstOrDefault(Function(x) x.KDKODING = "IGD_" & entity.KDPENDAFTARAN)
                Dim dsTerapiPenerta = oConnection.db.S_KODING_TERAPI_DIAGNOSISPENYERTAs.Where(Function(x) x.KDKODING = "IGD_" & entity.KDPENDAFTARAN)
                Dim dsTerapi = oConnection.db.S_KODING_TERAPIs.Where(Function(x) x.KDKODING = "IGD_" & entity.KDPENDAFTARAN)
                Dim dsDiagnosa = oConnection.db.S_DIGITAL_IGD_01_DIAGNOSAs.Where(Function(x) x.KDPENDAFTARAN = entity.KDPENDAFTARAN)

                Try
                    oConnection.db.S_DIGITAL_IGD_01s.DeleteOnSubmit(ds)
                    oConnection.db.S_DIGITAL_IGD_01s.InsertOnSubmit(entity)

                    If dsResep IsNot Nothing Then
                        oConnection.db.S_REQ_RECIPE_Hs.DeleteOnSubmit(dsResep)
                    End If
                    If entityResep_H IsNot Nothing Then
                        oConnection.db.S_REQ_RECIPE_Hs.InsertOnSubmit(entityResep_H)
                    End If

                    If entityDetail IsNot Nothing Then
                        oConnection.db.S_REQ_RECIPE_Ds.DeleteAllOnSubmit(dsDetail)
                        oConnection.db.S_REQ_RECIPE_Ds.InsertAllOnSubmit(entityDetail)
                    End If

                    If dsDetailTindakanPoli IsNot Nothing Then
                        oConnection.db.S_DIGITAL_IGD_01_TINDAKANPOLIs.DeleteAllOnSubmit(dsDetailTindakanPoli)
                    End If
                    If entityDetailTindakanPoli IsNot Nothing Then
                        oConnection.db.S_DIGITAL_IGD_01_TINDAKANPOLIs.InsertAllOnSubmit(entityDetailTindakanPoli)
                    End If

                    If dsDetailPenunjang IsNot Nothing Then
                        oConnection.db.S_DIGITAL_IGD_01_PENUNJANGs.DeleteAllOnSubmit(dsDetailPenunjang)
                    End If
                    If entityDetailPenunjang IsNot Nothing Then
                        oConnection.db.S_DIGITAL_IGD_01_PENUNJANGs.InsertAllOnSubmit(entityDetailPenunjang)
                    End If

                    If dsKoding IsNot Nothing Then
                        oConnection.db.S_KODING_Hs.DeleteOnSubmit(dsKoding)
                    End If
                    If entityKoding IsNot Nothing Then
                        oConnection.db.S_KODING_Hs.InsertOnSubmit(entityKoding)
                    End If

                    If dsDiagnosaUtama IsNot Nothing Then
                        oConnection.db.S_KODING_DIAGNOSISUTAMAs.DeleteOnSubmit(dsDiagnosaUtama)
                    End If
                    If entityDIagnosaUtama IsNot Nothing Then
                        oConnection.db.S_KODING_DIAGNOSISUTAMAs.InsertOnSubmit(entityDIagnosaUtama)
                    End If

                    If dsTerapiPenerta IsNot Nothing Then
                        oConnection.db.S_KODING_TERAPI_DIAGNOSISPENYERTAs.DeleteAllOnSubmit(dsTerapiPenerta)
                    End If
                    If entityDetailDiagnosisPenyertaTindakan IsNot Nothing Then
                        oConnection.db.S_KODING_TERAPI_DIAGNOSISPENYERTAs.InsertAllOnSubmit(entityDetailDiagnosisPenyertaTindakan)
                    End If

                    If dsTerapi IsNot Nothing Then
                        oConnection.db.S_KODING_TERAPIs.DeleteAllOnSubmit(dsTerapi)
                    End If
                    If entityDetailTindakan IsNot Nothing Then
                        oConnection.db.S_KODING_TERAPIs.InsertAllOnSubmit(entityDetailTindakan)
                    End If
                    If dsDiagnosa IsNot Nothing Then
                        oConnection.db.S_DIGITAL_IGD_01_DIAGNOSAs.DeleteAllOnSubmit(dsDiagnosa)
                    End If
                    If entityDetailDiagnosa IsNot Nothing Then
                        oConnection.db.S_DIGITAL_IGD_01_DIAGNOSAs.InsertAllOnSubmit(entityDetailDiagnosa)
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

                Dim ds = oConnection.db.S_DIGITAL_IGD_01s.FirstOrDefault(Function(x) x.KDPENDAFTARAN = Parameter)
                Dim dsResep = oConnection.db.S_REQ_RECIPE_Hs.FirstOrDefault(Function(x) x.KDREQRECIPE = "IGD_" & Parameter)
                Dim dsDetail = oConnection.db.S_REQ_RECIPE_Ds.Where(Function(x) x.KDREQRECIPE = "IGD_" & Parameter)
                Dim dsDetailTindakanPoli = oConnection.db.S_DIGITAL_IGD_01_TINDAKANPOLIs.Where(Function(x) x.KDPENDAFTARAN = Parameter)
                Dim dsDetailPenunjang = oConnection.db.S_DIGITAL_IGD_01_PENUNJANGs.Where(Function(x) x.KDPENDAFTARAN = Parameter)

                Try
                    oConnection.db.S_DIGITAL_IGD_01s.DeleteOnSubmit(ds)
                    If dsResep IsNot Nothing Then
                        oConnection.db.S_REQ_RECIPE_Hs.DeleteOnSubmit(dsResep)
                    End If
                    If dsDetail IsNot Nothing Then
                        oConnection.db.S_REQ_RECIPE_Ds.DeleteAllOnSubmit(dsDetail)
                    End If

                    If dsDetailTindakanPoli IsNot Nothing Then
                        oConnection.db.S_DIGITAL_IGD_01_TINDAKANPOLIs.DeleteAllOnSubmit(dsDetailTindakanPoli)
                    End If
                    If dsDetailPenunjang IsNot Nothing Then
                        oConnection.db.S_DIGITAL_IGD_01_PENUNJANGs.DeleteAllOnSubmit(dsDetailPenunjang)
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
        Public Function UpdateDataFarmasi(ByVal Paramater As String, ByVal entityDetail As List(Of S_REQ_RECIPE_D)) As Boolean
            Try
                If Not oConnection.GetConnection() Then
                    UpdateDataFarmasi = False
                    Exit Function
                End If

                sREFERENCE = Paramater
                sSTATUS = "UPDATE"

                Dim dsDetail = oConnection.db.S_REQ_RECIPE_Ds.Where(Function(x) x.KDREQRECIPE = Paramater)

                Try
                    If dsDetail.Count > 0 Then
                        oConnection.db.S_REQ_RECIPE_Ds.DeleteAllOnSubmit(dsDetail)
                    End If
                    If entityDetail.Count > 0 Then
                        oConnection.db.S_REQ_RECIPE_Ds.InsertAllOnSubmit(entityDetail)
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

                UpdateDataFarmasi = True
            Catch ex As Exception
                UpdateDataFarmasi = False
                oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                Throw ex
            End Try
        End Function
        Public Function UpdateDataObat(ByVal NoRegister As String, ByVal Obat As String) As Boolean
            Try
                If Not oConnection.GetConnection Then
                    UpdateDataObat = False
                    Exit Function
                End If

                Dim ds = oConnection.db.S_DIGITAL_IGD_01s.FirstOrDefault(Function(x) x.KDPENDAFTARAN = NoRegister)

                If ds IsNot Nothing Then
                    ds.OBATSAATPULANG = Obat
                    oConnection.db.SubmitChanges()
                End If

                UpdateDataObat = True
            Catch ex As Exception
                UpdateDataObat = False
                oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                Throw ex
            End Try
        End Function
        Public Function UpdateWaktuSaatPulang(ByVal NoRegister As String, ByVal SAATPASIENPULANG_TIME As DateTime) As Boolean
            Try
                If Not oConnection.GetConnection Then
                    UpdateWaktuSaatPulang = False
                    Exit Function
                End If

                Dim ds = oConnection.db.S_DIGITAL_IGD_01s.FirstOrDefault(Function(x) x.KDPENDAFTARAN = NoRegister)

                If ds IsNot Nothing Then
                    ds.SAATPASIENPULANG_TIME = SAATPASIENPULANG_TIME
                    oConnection.db.SubmitChanges()
                End If

                UpdateWaktuSaatPulang = True
            Catch ex As Exception
                UpdateWaktuSaatPulang = False
                oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                Throw ex
            End Try
        End Function
        Public Function UpdateGrandTotal(ByVal sKDRECIPE As String, ByVal sGRANDTOTAL As Decimal) As Boolean
            Try
                If Not oConnection.GetConnection Then
                    UpdateGrandTotal = False
                    Exit Function
                End If

                Dim ds = oConnection.db.S_REQ_RECIPE_Hs.FirstOrDefault(Function(x) x.KDRECIPE = sKDRECIPE)

                If ds IsNot Nothing Then
                    ds.GRANDTOTAL = sGRANDTOTAL
                    oConnection.db.SubmitChanges()
                End If

                UpdateGrandTotal = True
            Catch ex As Exception
                UpdateGrandTotal = False
                oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                Throw ex
            End Try
        End Function
        Public Function UpdateTindakLanjut(ByVal sKDRECIPE As String, ByVal sSURVEY_KEPALA_1 As String, ByVal sTINDAKLANJUT_RUJUK As Boolean, ByVal sTINDAKLANJUT_RAWAT As Boolean, ByVal sTINDAKLANJUT_PULANGPAKSA As Boolean, ByVal sTINDAKLANJUT_PULANG As Boolean) As Boolean
            Try
                If Not oConnection.GetConnection Then
                    UpdateTindakLanjut = False
                    Exit Function
                End If

                Dim ds = oConnection.db.S_DIGITAL_IGD_01s.FirstOrDefault(Function(x) x.KDPENDAFTARAN = sKDRECIPE)

                If ds IsNot Nothing Then
                    ds.SURVEY_KEPALA_1 = sSURVEY_KEPALA_1
                    ds.TINDAKLANJUT_RUJUK = sTINDAKLANJUT_RUJUK
                    ds.TINDAKLANJUT_RAWAT = sTINDAKLANJUT_RAWAT
                    ds.TINDAKLANJUT_PULANGPAKSA = sTINDAKLANJUT_PULANGPAKSA
                    ds.TINDAKLANJUT_PULANG = sTINDAKLANJUT_PULANG
                    oConnection.db.SubmitChanges()
                End If

                UpdateTindakLanjut = True
            Catch ex As Exception
                UpdateTindakLanjut = False
                oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                Throw ex
            End Try
        End Function
    End Class
End Namespace