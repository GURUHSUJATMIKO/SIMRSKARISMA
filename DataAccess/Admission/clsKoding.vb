Imports System.Threading

Namespace Admission
    Public Class clsKoding
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
            sMODUL = "CD"

            oCounter = New Setting.clsCounter
        End Sub
        Protected Overrides Sub Finalize()
            MyBase.Finalize()
            oConnection = Nothing
            GC.SuppressFinalize(Me)
        End Sub
        Public Function GetStructureHeader() As S_KODING_H
            If Not oConnection.GetConnection Then
                GetStructureHeader = Nothing
            End If
            GetStructureHeader = New S_KODING_H
        End Function
        Public Function GetStructureHeaderUtama() As S_KODING_DIAGNOSISUTAMA
            If Not oConnection.GetConnection Then
                GetStructureHeaderUtama = Nothing
            End If
            GetStructureHeaderUtama = New S_KODING_DIAGNOSISUTAMA
        End Function
        Public Function GetDataTambahan(ByVal Parameter As String) As S_REQ_RECIPE_TAMBAHAN
            If Not oConnection.GetConnection Then
                GetDataTambahan = Nothing
                Exit Function
            End If
            GetDataTambahan = oConnection.db.S_REQ_RECIPE_TAMBAHANs.FirstOrDefault(Function(x) x.KDREQRECIPE = Parameter)
        End Function
        Public Function GetDataDetailTindakan_(ByVal Parameter As String) As List(Of S_KODING_TERAPI)
            If Not oConnection.GetConnection Then
                GetDataDetailTindakan_ = Nothing
                Exit Function
            End If
            GetDataDetailTindakan_ = oConnection.db.S_KODING_TERAPIs.Where(Function(x) x.KDKODING = Parameter).ToList()
        End Function
        Public Function GetDataDetailTindakan(ByVal Parameter As String) As List(Of S_KODING_TERAPI_DIAGNOSISPENYERTA)
            If Not oConnection.GetConnection Then
                GetDataDetailTindakan = Nothing
                Exit Function
            End If
            GetDataDetailTindakan = oConnection.db.S_KODING_TERAPI_DIAGNOSISPENYERTAs.Where(Function(x) x.KDKODING = Parameter).ToList()
        End Function
        Public Function GetDataDetail(ByVal Parameter As String) As List(Of S_REQ_RECIPE_D)
            If Not oConnection.GetConnection Then
                GetDataDetail = Nothing
                Exit Function
            End If
            GetDataDetail = oConnection.db.S_REQ_RECIPE_Ds.Where(Function(x) x.KDREQRECIPE = Parameter).ToList()
        End Function
        Public Function GetDataDetailRacikan(ByVal Parameter As String) As List(Of S_REQ_RECIPE_RACIKAN)
            If Not oConnection.GetConnection Then
                GetDataDetailRacikan = Nothing
                Exit Function
            End If
            GetDataDetailRacikan = oConnection.db.S_REQ_RECIPE_RACIKANs.Where(Function(x) x.KDREQRECIPE = Parameter).ToList()
        End Function
        'Public Function GetStructureDetail_DX() As S_KODING_DX
        '    If Not oConnection.GetConnection Then
        '        GetStructureDetail_DX = Nothing
        '    End If
        '    GetStructureDetail_DX = New S_KODING_DX
        'End Function
        'Public Function GetStructureDetail_DXList() As List(Of S_KODING_DX)
        '    If Not oConnection.GetConnection Then
        '        GetStructureDetail_DXList = Nothing
        '    End If
        '    GetStructureDetail_DXList = New List(Of S_KODING_DX)
        'End Function
        'Public Function GetDataDetail_DX() As List(Of S_KODING_DX)
        '    If Not oConnection.GetConnection Then
        '        GetDataDetail_DX = Nothing
        '        Exit Function
        '    End If
        '    GetDataDetail_DX = oConnection.db.S_KODING_DXes.ToList()
        'End Function
        'Public Function GetDataDetail_DX(ByVal Parameter As String) As List(Of S_KODING_DX)
        '    If Not oConnection.GetConnection Then
        '        GetDataDetail_DX = Nothing
        '        Exit Function
        '    End If
        '    GetDataDetail_DX = oConnection.db.S_KODING_DXes.Where(Function(x) x.KDKODING = Parameter).OrderBy(Function(x) x.SEQ).ToList()
        'End Function
        'Public Function GetDataDetail_DIX(ByVal Parameter As String) As List(Of S_KODING_D9)
        '    If Not oConnection.GetConnection Then
        '        GetDataDetail_DIX = Nothing
        '        Exit Function
        '    End If
        '    GetDataDetail_DIX = oConnection.db.S_KODING_D9s.Where(Function(x) x.KDKODING = Parameter).OrderBy(Function(x) x.SEQ).ToList()
        'End Function
        'Public Function GetDataDetail_Terapi(ByVal Parameter As String) As List(Of S_KODING_TERAPI)
        '    If Not oConnection.GetConnection Then
        '        GetDataDetail_Terapi = Nothing
        '        Exit Function
        '    End If
        '    GetDataDetail_Terapi = oConnection.db.S_KODING_TERAPIs.Where(Function(x) x.KDKODING = Parameter).OrderBy(Function(x) x.SEQ).ToList()
        'End Function
        'Public Function GetDataDetail_DXByKDPENDAFTARAN(ByVal sParameter As String) As List(Of S_KODING_DX)
        '    If Not oConnection.GetConnection Then
        '        GetDataDetail_DXByKDPENDAFTARAN = Nothing
        '        Exit Function
        '    End If
        '    GetDataDetail_DXByKDPENDAFTARAN = oConnection.db.S_KODING_DXes.Where(Function(x) x.S_KODING_H.KDPENDAFTARAN = sParameter).ToList()
        'End Function
        Public Function GetDataByKDPENDAFTARAN(ByVal sParameter As String) As S_KODING_H
            If Not oConnection.GetConnection Then
                GetDataByKDPENDAFTARAN = Nothing
                Exit Function
            End If
            GetDataByKDPENDAFTARAN = oConnection.db.S_KODING_Hs.FirstOrDefault(Function(x) x.KDPENDAFTARAN = sParameter)
        End Function
        Public Function GetDataByRekamMedis(ByVal Paramater As String) As List(Of S_KODING_H)
            If Not oConnection.GetConnection Then
                GetDataByRekamMedis = Nothing
                Exit Function
            End If
            GetDataByRekamMedis = oConnection.db.S_KODING_Hs.Where(Function(x) x.KDCUSTOMER = Paramater).OrderByDescending(Function(x) x.KDKODING).ToList()
        End Function
        'Public Function GetStructureDetail_D9() As S_KODING_D9
        '    If Not oConnection.GetConnection Then
        '        GetStructureDetail_D9 = Nothing
        '    End If
        '    GetStructureDetail_D9 = New S_KODING_D9
        'End Function
        'Public Function GetStructureDetail_D9List() As List(Of S_KODING_D9)
        '    If Not oConnection.GetConnection Then
        '        GetStructureDetail_D9List = Nothing
        '    End If
        '    GetStructureDetail_D9List = New List(Of S_KODING_D9)
        'End Function
        'Public Function GetDataDetail_D9() As List(Of S_KODING_D9)
        '    If Not oConnection.GetConnection Then
        '        GetDataDetail_D9 = Nothing
        '        Exit Function
        '    End If
        '    GetDataDetail_D9 = oConnection.db.S_KODING_D9s.ToList()
        'End Function
        Public Function GetStructureDetail_Terapi() As S_KODING_TERAPI
            If Not oConnection.GetConnection Then
                GetStructureDetail_Terapi = Nothing
            End If
            GetStructureDetail_Terapi = New S_KODING_TERAPI
        End Function
        Public Function GetStructureDetail_TerapiList() As List(Of S_KODING_TERAPI)
            If Not oConnection.GetConnection Then
                GetStructureDetail_TerapiList = Nothing
            End If
            GetStructureDetail_TerapiList = New List(Of S_KODING_TERAPI)
        End Function
        Public Function GetDataDetail_Terapi() As List(Of S_KODING_TERAPI)
            If Not oConnection.GetConnection Then
                GetDataDetail_Terapi = Nothing
                Exit Function
            End If
            GetDataDetail_Terapi = oConnection.db.S_KODING_TERAPIs.ToList()
        End Function
        Public Function GetStructureDetail_DianosisUtama() As S_KODING_DIAGNOSISUTAMA
            If Not oConnection.GetConnection Then
                GetStructureDetail_DianosisUtama = Nothing
            End If
            GetStructureDetail_DianosisUtama = New S_KODING_DIAGNOSISUTAMA
        End Function
        Public Function GetStructureDetail_TindakanList() As List(Of S_KODING_TERAPI)
            If Not oConnection.GetConnection Then
                GetStructureDetail_TindakanList = Nothing
            End If
            GetStructureDetail_TindakanList = New List(Of S_KODING_TERAPI)
        End Function
        Public Function GetStructureDetail_ObatRacikanList() As List(Of S_REQ_RECIPE_RACIKAN)
            If Not oConnection.GetConnection Then
                GetStructureDetail_ObatRacikanList = Nothing
            End If
            GetStructureDetail_ObatRacikanList = New List(Of S_REQ_RECIPE_RACIKAN)
        End Function
        Public Function GetStructureDetail_DianosisPenyertaList() As List(Of S_KODING_DIAGNOSISPENYERTA)
            If Not oConnection.GetConnection Then
                GetStructureDetail_DianosisPenyertaList = Nothing
            End If
            GetStructureDetail_DianosisPenyertaList = New List(Of S_KODING_DIAGNOSISPENYERTA)
        End Function
        Public Function GetStructureDetail_DianosisPenyerta() As S_KODING_DIAGNOSISPENYERTA
            If Not oConnection.GetConnection Then
                GetStructureDetail_DianosisPenyerta = Nothing
            End If
            GetStructureDetail_DianosisPenyerta = New S_KODING_DIAGNOSISPENYERTA
        End Function
        Public Function GetStructureDetail_Tindakan() As S_KODING_TERAPI
            If Not oConnection.GetConnection Then
                GetStructureDetail_Tindakan = Nothing
            End If
            GetStructureDetail_Tindakan = New S_KODING_TERAPI
        End Function
        Public Function GetStructureDetail_ObatRacikan() As S_REQ_RECIPE_RACIKAN
            If Not oConnection.GetConnection Then
                GetStructureDetail_ObatRacikan = Nothing
            End If
            GetStructureDetail_ObatRacikan = New S_REQ_RECIPE_RACIKAN
        End Function
        Public Function GetStructureDetail_Terapi_TindakanList() As List(Of S_KODING_TERAPI_DIAGNOSISPENYERTA)
            If Not oConnection.GetConnection Then
                GetStructureDetail_Terapi_TindakanList = Nothing
            End If
            GetStructureDetail_Terapi_TindakanList = New List(Of S_KODING_TERAPI_DIAGNOSISPENYERTA)
        End Function
        Public Function GetStructureDetail_Terapi_Tindakan() As S_KODING_TERAPI_DIAGNOSISPENYERTA
            If Not oConnection.GetConnection Then
                GetStructureDetail_Terapi_Tindakan = Nothing
            End If
            GetStructureDetail_Terapi_Tindakan = New S_KODING_TERAPI_DIAGNOSISPENYERTA
        End Function
        Public Function GetData() As List(Of S_KODING_H)
            If Not oConnection.GetConnection Then
                GetData = Nothing
                Exit Function
            End If
            GetData = oConnection.db.S_KODING_Hs.OrderBy(Function(x) x.KDKODING).ToList()
        End Function
        Public Function GetData(ByVal sParameter As String) As S_KODING_H
            If Not oConnection.GetConnection Then
                GetData = Nothing
                Exit Function
            End If
            GetData = oConnection.db.S_KODING_Hs.FirstOrDefault(Function(x) x.KDKODING = sParameter)
        End Function
        Public Function GetData_(ByVal sParameter As String) As S_KODING_DIAGNOSISUTAMA
            If Not oConnection.GetConnection Then
                GetData_ = Nothing
                Exit Function
            End If
            GetData_ = oConnection.db.S_KODING_DIAGNOSISUTAMAs.FirstOrDefault(Function(x) x.KDKODING = sParameter)
        End Function
        'Public Function GetDataDetailPenyerta() As List(Of S_KODING_DIAGNOSISPENYERTA)
        '    If Not oConnection.GetConnection Then
        '        GetDataDetailPenyerta = Nothing
        '        Exit Function
        '    End If
        '    GetDataDetailPenyerta = oConnection.db.S_KODING_DIAGNOSISPENYERTAs.ToList()
        'End Function
        Public Function GetDataDetailPenyertaTerapi() As List(Of S_KODING_TERAPI_DIAGNOSISPENYERTA)
            If Not oConnection.GetConnection Then
                GetDataDetailPenyertaTerapi = Nothing
                Exit Function
            End If
            GetDataDetailPenyertaTerapi = oConnection.db.S_KODING_TERAPI_DIAGNOSISPENYERTAs.ToList()
        End Function
        'Public Function GetDataByRM(ByVal sParameter As String) As S_KODING_H
        '    If Not oConnection.GetConnection Then
        '        GetDataByRM = Nothing
        '        Exit Function
        '    End If
        '    GetDataByRM = oConnection.db.S_KODING_Hs.FirstOrDefault(Function(x) x.S_PENDAFTARAN_H.KDCUSTOMER = sParameter)
        'End Function
        'Public Function GetDetail_D9ByKdKoding(ByVal sParameter As String) As S_KODING_D9
        '    If Not oConnection.GetConnection Then
        '        GetDetail_D9ByKdKoding = Nothing
        '        Exit Function
        '    End If
        '    GetDetail_D9ByKdKoding = oConnection.db.S_KODING_D9s.Where(Function(x) x.KDKODING = sParameter).OrderByDescending(Function(x) x.SEQ).FirstOrDefault()
        'End Function
        Public Function InsertData(ByVal entity As S_KODING_H, ByVal entityDetail_DiagnosisUtama As S_KODING_DIAGNOSISUTAMA) As Boolean
            Try
                If Not oConnection.GetConnection Then
                    InsertData = False
                    Exit Function
                End If

                sSTATUS = "INSERT"
                sREFERENCE = entity.KDKODING

                ''Generate Auto Number
                'Try
                '    sLASTNUMBER = oCounter.GetLastNumber(sMODUL, entity.DATE)
                '    If sLASTNUMBER = 0 Then
                '        Try
                '            oCounter.InsertData(sMODUL, entity.DATE)
                '            sLASTNUMBER = oCounter.GetLastNumber(sMODUL, entity.DATE)
                '        Catch ex As Exception
                '            sLASTNUMBER = 0
                '        End Try
                '    End If

                '    entity.KDKODING = AutoNumber(sMODUL, sLASTNUMBER + 1, entity.DATE)
                '    entityDetail_DiagnosisUtama.KDKODING = entity.KDKODING

                '    'For Each iLoop In entityDetail_Penyerta
                '    '    iLoop.KDKODING = entity.KDKODING
                '    'Next

                '    'For Each iLoop In entityDetail_Terapi_Tindakan
                '    '    iLoop.KDKODING = entity.KDKODING
                '    'Next

                'Catch ex As Exception
                '    oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                '    Throw ex
                'End Try
                'Try
                '    oCounter.UpdateData(sMODUL, sLASTNUMBER + 1, Month(entity.DATE), Year(entity.DATE))
                'Catch ex As Exception
                '    oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                '    Throw ex
                'End Try
                ''End Generate

                Try
                    entityDetail_DiagnosisUtama.KDKODING = entity.KDKODING

                    oConnection.db.S_KODING_Hs.InsertOnSubmit(entity)

                    'transaction.Complete()
                Catch ex As Exception
                    oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                    Throw ex
                End Try

                Try
                    If entityDetail_DiagnosisUtama IsNot Nothing Then
                        oConnection.db.S_KODING_DIAGNOSISUTAMAs.InsertOnSubmit(entityDetail_DiagnosisUtama)
                    End If
                Catch ex As Exception
                    oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                    Throw ex
                End Try

                'Try
                '    If entityDetail_Penyerta IsNot Nothing Then
                '        oConnection.db.S_KODING_DIAGNOSISPENYERTAs.InsertAllOnSubmit(entityDetail_Penyerta)
                '    End If
                'Catch ex As Exception
                '    oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                '    Throw ex
                'End Try

                'Try
                '    If entityDetail_Terapi_Tindakan IsNot Nothing Then
                '        oConnection.db.S_KODING_TERAPI_DIAGNOSISPENYERTAs.InsertAllOnSubmit(entityDetail_Terapi_Tindakan)
                '    End If
                'Catch ex As Exception
                '    oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                '    Throw ex
                'End Try

                Try
                    oConnection.db.SubmitChanges()
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
        Public Function UpdateData(ByVal entity As S_KODING_H, ByVal entityDetail_DiagnosisUtama As S_KODING_DIAGNOSISUTAMA, ByVal entityDetail_Penyerta As List(Of S_KODING_DIAGNOSISPENYERTA), ByVal entityDetailDiagnosisPenyertaTindakan As List(Of S_KODING_TERAPI_DIAGNOSISPENYERTA)) As Boolean
            Try
                If Not oConnection.GetConnection Then
                    UpdateData = False
                    Exit Function
                End If

                sSTATUS = "UPDATE"
                sREFERENCE = entity.KDKODING

                Dim ds = oConnection.db.S_KODING_Hs.FirstOrDefault(Function(x) x.KDKODING = entity.KDKODING)

                Try
                    oConnection.db.S_KODING_Hs.DeleteOnSubmit(ds)
                    oConnection.db.S_KODING_Hs.InsertOnSubmit(entity)
                Catch ex As Exception
                    oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                    Throw ex
                End Try

                'Dim dsDetail_Terapi = oConnection.db.S_KODING_TERAPIs.Where(Function(x) x.KDKODING = entity.KDKODING)

                'Try
                '    If dsDetail_Terapi.Count > 0 Then
                '        oConnection.db.S_KODING_TERAPIs.DeleteAllOnSubmit(dsDetail_Terapi)
                '    End If
                '    oConnection.db.S_KODING_TERAPIs.InsertAllOnSubmit(entityDetail_Terapi)
                'Catch ex As Exception
                '    oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                '    Throw ex
                'End Try

                Dim dsDetail_DiagnosisUtama = oConnection.db.S_KODING_DIAGNOSISUTAMAs.FirstOrDefault(Function(x) x.KDKODING = entity.KDKODING)

                Try
                    If dsDetail_DiagnosisUtama IsNot Nothing Then
                        oConnection.db.S_KODING_DIAGNOSISUTAMAs.DeleteOnSubmit(dsDetail_DiagnosisUtama)
                    End If
                    oConnection.db.S_KODING_DIAGNOSISUTAMAs.InsertOnSubmit(entityDetail_DiagnosisUtama)
                Catch ex As Exception
                    oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                    Throw ex
                End Try

                Dim dsDetail_DiagnosisPenyerta = oConnection.db.S_KODING_DIAGNOSISPENYERTAs.Where(Function(x) x.KDKODING = entity.KDKODING)

                Try
                    If dsDetail_DiagnosisPenyerta.Count > 0 Then
                        oConnection.db.S_KODING_DIAGNOSISPENYERTAs.DeleteAllOnSubmit(dsDetail_DiagnosisPenyerta)
                    End If
                    oConnection.db.S_KODING_DIAGNOSISPENYERTAs.InsertAllOnSubmit(entityDetail_Penyerta)
                Catch ex As Exception
                    oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                    Throw ex
                End Try

                Dim dsDetail_Terapi_Tindakan = oConnection.db.S_KODING_TERAPI_DIAGNOSISPENYERTAs.Where(Function(x) x.KDKODING = entity.KDKODING)

                Try
                    If dsDetail_Terapi_Tindakan.Count > 0 Then
                        oConnection.db.S_KODING_TERAPI_DIAGNOSISPENYERTAs.DeleteAllOnSubmit(dsDetail_Terapi_Tindakan)
                    End If
                    oConnection.db.S_KODING_TERAPI_DIAGNOSISPENYERTAs.InsertAllOnSubmit(entityDetailDiagnosisPenyertaTindakan)
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

                Dim ds = oConnection.db.S_KODING_Hs.FirstOrDefault(Function(x) x.KDKODING = Parameter)

                Try
                    oConnection.db.S_KODING_Hs.DeleteOnSubmit(ds)
                Catch ex As Exception
                    oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                    Throw ex
                End Try


                Dim dsDetail_Terapi = oConnection.db.S_KODING_TERAPIs.Where(Function(x) x.KDKODING = Parameter)

                Try
                    If dsDetail_Terapi.Count > 0 Then
                        oConnection.db.S_KODING_TERAPIs.DeleteAllOnSubmit(dsDetail_Terapi)
                    End If
                Catch ex As Exception
                    oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                    Throw ex
                End Try

                Dim dsDetail_DiagnosisUtama = oConnection.db.S_KODING_DIAGNOSISUTAMAs.FirstOrDefault(Function(x) x.KDKODING = Parameter)

                Try
                    If dsDetail_DiagnosisUtama IsNot Nothing Then
                        oConnection.db.S_KODING_DIAGNOSISUTAMAs.DeleteOnSubmit(dsDetail_DiagnosisUtama)
                    End If
                Catch ex As Exception
                    oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                    Throw ex
                End Try

                Dim dsDetail_DiagnosisPenyerta = oConnection.db.S_KODING_DIAGNOSISPENYERTAs.Where(Function(x) x.KDKODING = Parameter)

                Try
                    If dsDetail_DiagnosisPenyerta.Count > 0 Then
                        oConnection.db.S_KODING_DIAGNOSISPENYERTAs.DeleteAllOnSubmit(dsDetail_DiagnosisPenyerta)
                    End If
                Catch ex As Exception
                    oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                    Throw ex
                End Try

                Dim dsDetail_Terapi_Tindakan = oConnection.db.S_KODING_TERAPI_DIAGNOSISPENYERTAs.Where(Function(x) x.KDKODING = Parameter)

                Try
                    If dsDetail_Terapi_Tindakan.Count > 0 Then
                        oConnection.db.S_KODING_TERAPI_DIAGNOSISPENYERTAs.DeleteAllOnSubmit(dsDetail_Terapi_Tindakan)
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

                DeleteData = True
            Catch ex As Exception
                DeleteData = False
                oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                Throw ex
            End Try
        End Function
    End Class
End Namespace