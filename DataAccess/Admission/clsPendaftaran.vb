Imports System.Threading

Namespace Admission
    Public Class clsPendaftaran
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
            sMODUL = "PENDAFTARAN"

            oCounter = New Setting.clsCounter
        End Sub
        Protected Overrides Sub Finalize()
            MyBase.Finalize()
            oConnection = Nothing
            GC.SuppressFinalize(Me)
        End Sub
        Public Function GetStructureHeader() As S_PENDAFTARAN_H
            If Not oConnection.GetConnection() Then
                GetStructureHeader = Nothing
            End If
            GetStructureHeader = New S_PENDAFTARAN_H
        End Function
        Public Function GetStructureHeader_PenanggungJawab() As S_PENDAFTARAN_PENANGGUNGJAWAB
            If Not oConnection.GetConnection() Then
                GetStructureHeader_PenanggungJawab = Nothing
            End If
            GetStructureHeader_PenanggungJawab = New S_PENDAFTARAN_PENANGGUNGJAWAB
        End Function
        Public Function GetStructureHeader_Tracking() As S_TRACKING
            If Not oConnection.GetConnection() Then
                GetStructureHeader_Tracking = Nothing
            End If
            GetStructureHeader_Tracking = New S_TRACKING
        End Function
        Public Function GetData() As List(Of S_PENDAFTARAN_H)
            If Not oConnection.GetConnection() Then
                GetData = Nothing
                Exit Function
            End If
            GetData = oConnection.db.S_PENDAFTARAN_Hs.OrderByDescending(Function(x) x.KDPENDAFTARAN).ToList()
        End Function
        'Public Function GetDataLastCustomer(ByVal sKDCUSTOMER As String) As S_PENDAFTARAN_PENANGGUNGJAWAB
        '    If Not oConnection.GetConnection Then
        '        GetDataLastCustomer = Nothing
        '        Exit Function
        '    End If

        '    Dim ds = (From x In oConnection.db.S_PENDAFTARAN_PENANGGUNGJAWABs.Where(Function(x) x.KDCUSTOMER = sKDCUSTOMER)
        '              Select x).ToList()

        '    GetDataLastCustomer = ds.OrderByDescending(Function(x) x.DATECREATED).FirstOrDefault()
        'End Function
        Public Function GetDataByRMUnitDate(ByVal sKDCUSTOMER As String, ByVal sKDDEPARTMENT As String, ByVal sDATE As DateTime) As S_PENDAFTARAN_H
            If Not oConnection.GetConnection() Then
                GetDataByRMUnitDate = Nothing
                Exit Function
            End If
            GetDataByRMUnitDate = oConnection.db.S_PENDAFTARAN_Hs.FirstOrDefault(Function(x) x.KDCUSTOMER = sKDCUSTOMER And x.KDDEPARTMENT = sKDDEPARTMENT And x.DATE.Year = Year(sDATE) And x.DATE.Month = Month(sDATE) And x.DATE.Day = Day(sDATE))
        End Function
        Public Function GetDataByPoliKunjunganTerkahir(ByVal sKDDEPARTMENT As String, ByVal sDate As Date) As Integer
            If Not oConnection.GetConnection Then
                GetDataByPoliKunjunganTerkahir = 0
                Exit Function
            End If

            Dim ds = (From x In oConnection.db.S_PENDAFTARAN_Hs.Where(Function(x) x.KDDEPARTMENT = sKDDEPARTMENT And x.DATE.Year = Year(sDate) And x.DATE.Month = Month(sDate) And x.DATE.Day = Day(sDate))
                      Select x).ToList()

            GetDataByPoliKunjunganTerkahir = ds.Count()

        End Function
        Public Function GetDataByRMKunjunganTerkahir(ByVal sKDCUSTOMER As String) As S_PENDAFTARAN_H
            If Not oConnection.GetConnection Then
                GetDataByRMKunjunganTerkahir = Nothing
                Exit Function
            End If

            Dim ds = (From x In oConnection.db.S_PENDAFTARAN_Hs.Where(Function(x) x.KDCUSTOMER = sKDCUSTOMER And x.CATEGORY = 0 And x.M_DEPARTMENT.KDDEPARTMENT <> "IGD")
                      Select x).ToList()

            GetDataByRMKunjunganTerkahir = ds.OrderByDescending(Function(x) x.DATE).FirstOrDefault()

        End Function
        Public Function GetDataByRMDateRawatInap(ByVal sKDCUSTOMER As String, ByVal sDATE As DateTime) As S_PENDAFTARAN_H
            If Not oConnection.GetConnection() Then
                GetDataByRMDateRawatInap = Nothing
                Exit Function
            End If
            GetDataByRMDateRawatInap = oConnection.db.S_PENDAFTARAN_Hs.FirstOrDefault(Function(x) x.CATEGORY = 1 And x.KDCUSTOMER = sKDCUSTOMER And x.DATE.Year = Year(sDATE) And x.DATE.Month = Month(sDATE) And x.DATE.Day = Day(sDATE))
        End Function
        Public Function GetDataBySKD(ByVal Parameter As String, ByVal Category As Integer) As List(Of S_PENDAFTARAN_H)
            If Not oConnection.GetConnection() Then
                GetDataBySKD = Nothing
                Exit Function
            End If
            GetDataBySKD = oConnection.db.S_PENDAFTARAN_Hs.Where(Function(x) IIf(Category = 0, x.KDCUSTOMER.Contains(Parameter), IIf(Category = 1, x.M_CUSTOMER.NAME_DISPLAY.Contains(Parameter), x.KDPENDAFTARAN.Contains(Parameter)))).ToList()
        End Function
        Public Function GetDataByRekamMedisRawatJalan(ByVal Parameter As String) As List(Of S_PENDAFTARAN_H)
            If Not oConnection.GetConnection() Then
                GetDataByRekamMedisRawatJalan = Nothing
                Exit Function
            End If
            GetDataByRekamMedisRawatJalan = oConnection.db.S_PENDAFTARAN_Hs.Where(Function(x) x.KDCUSTOMER = Parameter And x.CATEGORY = 0).ToList()
        End Function
        Public Function GetDataPenanggungJawabByPendaftaran(ByVal Parameter As String) As S_PENDAFTARAN_PENANGGUNGJAWAB
            If Not oConnection.GetConnection() Then
                GetDataPenanggungJawabByPendaftaran = Nothing
                Exit Function
            End If
            GetDataPenanggungJawabByPendaftaran = oConnection.db.S_PENDAFTARAN_PENANGGUNGJAWABs.FirstOrDefault(Function(x) x.KDPENDAFTARAN = Parameter)
        End Function
        Public Function GetData(ByVal Parameter As String) As S_PENDAFTARAN_H
            If Not oConnection.GetConnection() Then
                GetData = Nothing
                Exit Function
            End If
            GetData = oConnection.db.S_PENDAFTARAN_Hs.FirstOrDefault(Function(x) x.KDPENDAFTARAN = Parameter)
        End Function
        Public Function GetDataLast(ByVal Parameter As String) As S_PENDAFTARAN_H
            If Not oConnection.GetConnection() Then
                GetDataLast = Nothing
                Exit Function
            End If
            GetDataLast = oConnection.db.S_PENDAFTARAN_Hs.Where(Function(x) x.KDCUSTOMER = Parameter).OrderBy(Function(x) x.DATECREATED).FirstOrDefault(Function(x) x.KDPENDAFTARAN)
        End Function
        Public Function GetDataKoneksi(ByVal Parameter As String) As SET_KONEKSI
            If Not oConnection.GetConnection() Then
                GetDataKoneksi = Nothing
                Exit Function
            End If
            GetDataKoneksi = oConnection.db.SET_KONEKSIs.FirstOrDefault(Function(x) x.NAME_DISPLAY = Parameter And x.ISACTIVE = True)
        End Function
        Public Function GetDataMemoDepartment(ByVal Parameter As String) As M_DEPARTMENT
            If Not oConnection.GetConnection() Then
                GetDataMemoDepartment = Nothing
                Exit Function
            End If
            GetDataMemoDepartment = oConnection.db.M_DEPARTMENTs.FirstOrDefault(Function(x) x.KDDEPARTMENT = Parameter)
        End Function
        Public Function GetDataMemoDoctor(ByVal Parameter As String) As M_DOCTOR
            If Not oConnection.GetConnection() Then
                GetDataMemoDoctor = Nothing
                Exit Function
            End If
            GetDataMemoDoctor = oConnection.db.M_DOCTORs.FirstOrDefault(Function(x) x.KDDOCTOR = Parameter)
        End Function
        Public Function GetDataTanggalLahir(ByVal Parameter As String) As M_CUSTOMER
            If Not oConnection.GetConnection() Then
                GetDataTanggalLahir = Nothing
                Exit Function
            End If
            GetDataTanggalLahir = oConnection.db.M_CUSTOMERs.FirstOrDefault(Function(x) x.KDCUSTOMER = Parameter)
        End Function
        Public Function GetUmurPasien(ByVal dateNow As Date, ByVal tgllahir As Date) As String
            Dim years As Long
            Dim months As Long
            Dim days As Long
            Dim yearWord As String
            Dim monthWord As String
            Dim dayWord As String

            ' menghitung tahun
            years = DateDiff("yyyy", tgllahir, dateNow)
            If Month(tgllahir) > Month(dateNow) Then
                years = years - 1
            ElseIf Month(tgllahir) = Month(dateNow) And tgllahir.Day > dateNow.Day Then
                years = years - 1
            ElseIf Month(tgllahir) = Month(dateNow) And tgllahir.Day = dateNow.Day Then
                'GoTo Finish ' jika bulan dan tanggal sama maka perhitungan selesai
            End If
            ' menghitung bulan
            tgllahir = DateAdd("yyyy", years, tgllahir)
            months = DateDiff("m", tgllahir, dateNow)
            If tgllahir.Day > dateNow.Day Then
                months = months - 1
            ElseIf Month(tgllahir) = Month(dateNow) And tgllahir.Day >= dateNow.Day Then
                months = months - 1
            End If
            tgllahir = DateAdd("m", months, tgllahir)
            ' menghitung hari
            days = DateDiff("d", tgllahir, dateNow)

            yearWord = IIf(years = 0, "", years & " Tahun ")
            monthWord = IIf(months = 0, "", months & " Bulan ")
            dayWord = IIf(days = 0, "", days & " Hari ")
            'calculateAge = yearWord & monthWord & dayWord
            'calculateAge = Trim(calculateAge)

            GetUmurPasien = yearWord & " " & monthWord & " " & dayWord
        End Function
        Public Function InsertData(ByVal entity As S_PENDAFTARAN_H, ByVal entityPenanggungJawab As S_PENDAFTARAN_PENANGGUNGJAWAB, ByVal KDPENDAFTARAN As String) As Boolean
            Try
                If Not oConnection.GetConnection() Then
                    InsertData = False
                    Exit Function
                End If

                sREFERENCE = KDPENDAFTARAN
                sSTATUS = "INSERT"

                Try
                    entity.KDPENDAFTARAN = KDPENDAFTARAN
                    oConnection.db.S_PENDAFTARAN_Hs.InsertOnSubmit(entity)
                    If entityPenanggungJawab IsNot Nothing Then
                        entityPenanggungJawab.KDPENDAFTARAN = KDPENDAFTARAN
                        oConnection.db.S_PENDAFTARAN_PENANGGUNGJAWABs.InsertOnSubmit(entityPenanggungJawab)
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

                InsertData = True

                UpdateDataFix(entity.NOMORSKDP, True)

                If entity.KDPENDAFTARAN_AWAL <> "" Then
                    UpdateKDPENDAFTARANRI(entity.KDPENDAFTARAN_AWAL, entity.KDPENDAFTARAN)
                End If

                Try
                    Dim DownloadIamge1 = "http://203.210.87.29/img/SIGNATURE/" & entity.KDCUSTOMER & "/" & entity.KDCUSTOMER & ".png"
                    Dim ds = GetStructureHeaderTandaTangan()
                    With ds
                        .DATECREATED = Now
                        .DATEUPDATED = Now
                        .KDPENDAFTARAN = entity.KDPENDAFTARAN
                        .KDCUSTOMER = entity.KDCUSTOMER
                        .ALAMATTANDATANGAN = DownloadIamge1
                        .ALAMAT_URL = "http://203.210.87.29/img/SIGNATURE/"
                        .NAMA = entity.M_CUSTOMER.NAME_DISPLAY
                    End With

                    InsertDataTandaTangan(ds)
                Catch ex As Exception

                End Try
            Catch ex As Exception
                InsertData = False
                oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                Throw ex
            End Try
        End Function
        Private Function InsertDataTandaTangan(ByVal entity As S_PENDAFTARAN_TANDATANGAN) As Boolean
            Try
                If Not oConnection.GetConnection Then
                    InsertDataTandaTangan = False
                    Exit Function
                End If

                Try
                    oConnection.db.S_PENDAFTARAN_TANDATANGANs.InsertOnSubmit(entity)
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

                InsertDataTandaTangan = True

            Catch ex As Exception
                InsertDataTandaTangan = False
                oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                Throw ex
            End Try
        End Function
        Public Function GetStructureHeaderTandaTangan() As S_PENDAFTARAN_TANDATANGAN
            If Not oConnection.GetConnection Then
                GetStructureHeaderTandaTangan = Nothing
            End If
            GetStructureHeaderTandaTangan = New S_PENDAFTARAN_TANDATANGAN
        End Function
        Public Function UpdateData(ByVal entity As S_PENDAFTARAN_H, ByVal entityPenanggungJawab As S_PENDAFTARAN_PENANGGUNGJAWAB) As Boolean
            Try
                If Not oConnection.GetConnection() Then
                    UpdateData = False
                    Exit Function
                End If

                sREFERENCE = entity.KDPENDAFTARAN
                sSTATUS = "UPDATE"

                Dim sNOMORSKD_1 As String = String.Empty
                Dim sNOMORSKD_2 As String = String.Empty

                Dim sPENDAFTARANRI_1 As String = String.Empty
                Dim sPENDAFTARANRI_2 As String = String.Empty

                Dim ds = oConnection.db.S_PENDAFTARAN_Hs.FirstOrDefault(Function(x) x.KDPENDAFTARAN = entity.KDPENDAFTARAN)
                Dim dsPenanggunJawab = oConnection.db.S_PENDAFTARAN_PENANGGUNGJAWABs.FirstOrDefault(Function(x) x.KDPENDAFTARAN = entity.KDPENDAFTARAN)

                Try
                    sPENDAFTARANRI_1 = ds.KDPENDAFTARAN_AWAL
                    sPENDAFTARANRI_2 = entity.KDPENDAFTARAN_AWAL

                    sNOMORSKD_1 = ds.NOMORSKDP
                    sNOMORSKD_2 = entity.NOMORSKDP

                    oConnection.db.S_PENDAFTARAN_Hs.DeleteOnSubmit(ds)
                    oConnection.db.S_PENDAFTARAN_Hs.InsertOnSubmit(entity)

                    If dsPenanggunJawab IsNot Nothing Then
                        oConnection.db.S_PENDAFTARAN_PENANGGUNGJAWABs.DeleteOnSubmit(dsPenanggunJawab)
                        oConnection.db.S_PENDAFTARAN_PENANGGUNGJAWABs.InsertOnSubmit(entityPenanggungJawab)
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

                UpdateDataFix(sNOMORSKD_1, False)
                UpdateDataFix(sNOMORSKD_2, True)

                UpdateKDPENDAFTARANRI(sPENDAFTARANRI_1, "")
                UpdateKDPENDAFTARANRI(sPENDAFTARANRI_2, entity.KDPENDAFTARAN_AWAL)
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
                Dim sNOMORSKD As String = String.Empty
                Dim sPENDAFTARANRI_1 As String = String.Empty
                Dim sKDRUANGRAWAT As String = String.Empty
                Dim sSEQ As Integer = 0
                Dim sKunjungan As Boolean = False

                Dim ds = oConnection.db.S_PENDAFTARAN_Hs.FirstOrDefault(Function(x) x.KDPENDAFTARAN = Parameter)
                Dim dsSKDPendaftaran = oConnection.db.S_PENDAFTARAN_SKDs.Where(Function(x) x.KDPENDAFTARAN = Parameter)
                Dim dsPenanggunJawab = oConnection.db.S_PENDAFTARAN_PENANGGUNGJAWABs.FirstOrDefault(Function(x) x.KDPENDAFTARAN = Parameter)
                Dim dsKunjunganPoli = oConnection.db.S_PENDAFTARAN_KUNJUNGANPOLIs.Where(Function(x) x.KDPENDAFTARAN = Parameter)
                Dim dsKunjunganRuangan = oConnection.db.S_PENDAFTARAN_KUNJUNGANRUANGANs.Where(Function(x) x.KDPENDAFTARAN = Parameter)
                Dim dsTandaTangan = oConnection.db.S_PENDAFTARAN_TANDATANGANs.FirstOrDefault(Function(x) x.KDPENDAFTARAN = Parameter)

                If ds IsNot Nothing Then
                    sNOMORSKD = ds.NOMORSKDP
                    sPENDAFTARANRI_1 = ds.KDPENDAFTARAN_AWAL
                End If
                Try
                    oConnection.db.S_PENDAFTARAN_Hs.DeleteOnSubmit(ds)
                    If dsSKDPendaftaran.Count > 0 Then
                        For Each xloop In dsSKDPendaftaran
                            Dim dsSKD = oConnection.db.S_PENDAFTARAN_SKDs.FirstOrDefault(Function(x) x.KDSKD = xloop.KDSKD)
                            If dsSKD IsNot Nothing Then
                                oConnection.db.S_PENDAFTARAN_SKDs.DeleteOnSubmit(dsSKD)
                            End If
                        Next
                    End If
                    If dsPenanggunJawab IsNot Nothing Then
                        oConnection.db.S_PENDAFTARAN_PENANGGUNGJAWABs.DeleteOnSubmit(dsPenanggunJawab)
                    End If
                    If dsKunjunganPoli.Count > 0 Then
                        oConnection.db.S_PENDAFTARAN_KUNJUNGANPOLIs.DeleteAllOnSubmit(dsKunjunganPoli)
                    End If

                    For Each xloop In dsKunjunganRuangan
                        If xloop.MEMO = "RUANGAN 1" Then
                            sKDRUANGRAWAT = xloop.KDRUANGRAWAT
                            sSEQ = xloop.SEQ
                        Else
                            sKunjungan = True
                        End If
                    Next

                    If dsKunjunganRuangan.Count > 0 Then
                        oConnection.db.S_PENDAFTARAN_KUNJUNGANRUANGANs.DeleteAllOnSubmit(dsKunjunganRuangan)
                    End If
                    If dsTandaTangan IsNot Nothing Then
                        oConnection.db.S_PENDAFTARAN_TANDATANGANs.DeleteOnSubmit(dsTandaTangan)
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

                UpdateDataFix(sNOMORSKD, False)
                UpdateKDPENDAFTARANRI(sPENDAFTARANRI_1, "")

                If sKunjungan = False Then
                    If sKDRUANGRAWAT <> String.Empty Then
                        UpdateDataIsTerisiDelete(sKDRUANGRAWAT, sSEQ)
                    End If
                End If
            Catch ex As Exception
                DeleteData = False
                oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                Throw ex
            End Try
        End Function
        Private Function UpdateDataIsTerisiDelete(ByVal sKDRUANGRAWAT As String, ByVal sSEQ As Integer) As Boolean
            Try
                If Not oConnection.GetConnection Then
                    UpdateDataIsTerisiDelete = False
                    Exit Function
                End If

                UpdateDataIsTerisiDelete = True

                Dim ds = oConnection.db.M_RUANGRAWAT_Ds.FirstOrDefault(Function(x) x.KDRUANGRAWAT = sKDRUANGRAWAT And x.SEQ = sSEQ)

                ds.ISTERISI = False

                oConnection.db.SubmitChanges()

            Catch ex As Exception
                UpdateDataIsTerisiDelete = False
                Throw ex
            End Try
        End Function
        Public Function UpdateDataFix(ByVal kdskd As String, ByVal isCek As Boolean) As Boolean
            Try
                If Not oConnection.GetConnection Then
                    UpdateDataFix = False
                    Exit Function
                End If

                UpdateDataFix = True

                Dim ds = oConnection.db.S_PENDAFTARAN_SKDs.FirstOrDefault(Function(x) x.KDSKD = kdskd)
                If ds IsNot Nothing Then
                    ds.ISCHEKED = isCek

                    oConnection.db.SubmitChanges()
                End If

            Catch ex As Exception
                UpdateDataFix = False
                Throw ex
            End Try
        End Function
        Public Function Penjamin_Default() As String
            Try
                If Not oConnection.GetConnection() Then
                    Penjamin_Default = String.Empty
                    Exit Function
                End If

                Dim ds = oConnection.db.M_PENJAMINs.FirstOrDefault(Function(x) x.ISDEFAULT = True)
                If ds IsNot Nothing Then
                    Penjamin_Default = ds.KDPENJAMIN
                Else
                    Penjamin_Default = String.Empty
                End If
            Catch ex As Exception
                Penjamin_Default = String.Empty
                Throw ex
            End Try
        End Function
        Public Function Perusahaan_Default() As String
            Try
                If Not oConnection.GetConnection() Then
                    Perusahaan_Default = String.Empty
                    Exit Function
                End If

                Dim ds = oConnection.db.M_PERUSAHAANs.FirstOrDefault(Function(x) x.ISDEFAULT = True)
                If ds IsNot Nothing Then
                    Perusahaan_Default = ds.KDPERUSAHAAN
                Else
                    Perusahaan_Default = String.Empty
                End If
            Catch ex As Exception
                Perusahaan_Default = String.Empty
                Throw ex
            End Try
        End Function
        Public Function Daftar_L1_Default() As String
            Try
                If Not oConnection.GetConnection() Then
                    Daftar_L1_Default = String.Empty
                    Exit Function
                End If

                Dim ds = oConnection.db.M_DAFTAR_L1s.FirstOrDefault(Function(x) x.ISDEFAULT = True)
                If ds IsNot Nothing Then
                    Daftar_L1_Default = ds.KDDAFTAR_L1
                Else
                    Daftar_L1_Default = String.Empty
                End If
            Catch ex As Exception
                Daftar_L1_Default = String.Empty
                Throw ex
            End Try
        End Function
        Public Function Daftar_L2_Default() As String
            Try
                If Not oConnection.GetConnection() Then
                    Daftar_L2_Default = String.Empty
                    Exit Function
                End If

                Dim ds = oConnection.db.M_DAFTAR_L2s.FirstOrDefault(Function(x) x.ISDEFAULT = True)
                If ds IsNot Nothing Then
                    Daftar_L2_Default = ds.KDDAFTAR_L2
                Else
                    Daftar_L2_Default = String.Empty
                End If
            Catch ex As Exception
                Daftar_L2_Default = String.Empty
                Throw ex
            End Try
        End Function
        Public Function Daftar_L3_Default() As String
            Try
                If Not oConnection.GetConnection() Then
                    Daftar_L3_Default = String.Empty
                    Exit Function
                End If

                Dim ds = oConnection.db.M_DAFTAR_L3s.FirstOrDefault(Function(x) x.ISDEFAULT = True)
                If ds IsNot Nothing Then
                    Daftar_L3_Default = ds.KDDAFTAR_L3
                Else
                    Daftar_L3_Default = String.Empty
                End If
            Catch ex As Exception
                Daftar_L3_Default = String.Empty
                Throw ex
            End Try
        End Function
        Public Function Daftar_L4_Default() As String
            Try
                If Not oConnection.GetConnection() Then
                    Daftar_L4_Default = String.Empty
                    Exit Function
                End If

                Dim ds = oConnection.db.M_DAFTAR_L4s.FirstOrDefault(Function(x) x.ISDEFAULT = True)
                If ds IsNot Nothing Then
                    Daftar_L4_Default = ds.KDDAFTAR_L4
                Else
                    Daftar_L4_Default = String.Empty
                End If
            Catch ex As Exception
                Daftar_L4_Default = String.Empty
                Throw ex
            End Try
        End Function
        Public Function Daftar_L5_Default() As String
            Try
                If Not oConnection.GetConnection() Then
                    Daftar_L5_Default = String.Empty
                    Exit Function
                End If

                Dim ds = oConnection.db.M_DAFTAR_L5s.FirstOrDefault(Function(x) x.ISDEFAULT = True)
                If ds IsNot Nothing Then
                    Daftar_L5_Default = ds.KDDAFTAR_L5
                Else
                    Daftar_L5_Default = String.Empty
                End If
            Catch ex As Exception
                Daftar_L5_Default = String.Empty
                Throw ex
            End Try
        End Function
        Public Function Daftar_L6_Default() As String
            Try
                If Not oConnection.GetConnection() Then
                    Daftar_L6_Default = String.Empty
                    Exit Function
                End If

                Dim ds = oConnection.db.M_DAFTAR_L6s.FirstOrDefault(Function(x) x.ISDEFAULT = True)
                If ds IsNot Nothing Then
                    Daftar_L6_Default = ds.KDDAFTAR_L6
                Else
                    Daftar_L6_Default = String.Empty
                End If
            Catch ex As Exception
                Daftar_L6_Default = String.Empty
                Throw ex
            End Try
        End Function
        Public Function Daftar_PPK_Default() As String
            Try
                If Not oConnection.GetConnection() Then
                    Daftar_PPK_Default = String.Empty
                    Exit Function
                End If

                Dim ds = oConnection.db.M_PPKs.FirstOrDefault(Function(x) x.ISDEFAULT = True)
                If ds IsNot Nothing Then
                    Daftar_PPK_Default = ds.KDPPK
                Else
                    Daftar_PPK_Default = String.Empty
                End If
            Catch ex As Exception
                Daftar_PPK_Default = String.Empty
                Throw ex
            End Try
        End Function
        Public Function Daftar_COB_Default() As String
            Try
                If Not oConnection.GetConnection() Then
                    Daftar_COB_Default = String.Empty
                    Exit Function
                End If

                Dim ds = oConnection.db.M_COBs.FirstOrDefault(Function(x) x.ISDEFAULT = True)
                If ds IsNot Nothing Then
                    Daftar_COB_Default = ds.KDCOB
                Else
                    Daftar_COB_Default = String.Empty
                End If
            Catch ex As Exception
                Daftar_COB_Default = String.Empty
                Throw ex
            End Try
        End Function
        Public Function Daftar_KELASRAWAT_Default() As String
            Try
                If Not oConnection.GetConnection() Then
                    Daftar_KELASRAWAT_Default = String.Empty
                    Exit Function
                End If

                Dim ds = oConnection.db.M_KELASRAWATs.FirstOrDefault(Function(x) x.ISDEFAULT = True)
                If ds IsNot Nothing Then
                    Daftar_KELASRAWAT_Default = ds.KDKELASRAWAT
                Else
                    Daftar_KELASRAWAT_Default = String.Empty
                End If
            Catch ex As Exception
                Daftar_KELASRAWAT_Default = String.Empty
                Throw ex
            End Try
        End Function
        Public Function Daftar_HUBUNGAN_Default() As String
            Try
                If Not oConnection.GetConnection() Then
                    Daftar_HUBUNGAN_Default = String.Empty
                    Exit Function
                End If

                Dim ds = oConnection.db.M_HUBUNGANs.FirstOrDefault(Function(x) x.ISDEFAULT = True)
                If ds IsNot Nothing Then
                    Daftar_HUBUNGAN_Default = ds.KDHUBUNGAN
                Else
                    Daftar_HUBUNGAN_Default = String.Empty
                End If
            Catch ex As Exception
                Daftar_HUBUNGAN_Default = String.Empty
                Throw ex
            End Try
        End Function
        Public Function Daftar_DIAGNOSA_Default() As String
            Try
                If Not oConnection.GetConnection() Then
                    Daftar_DIAGNOSA_Default = String.Empty
                    Exit Function
                End If

                Dim ds = oConnection.db.M_DIAGNOSAs.FirstOrDefault(Function(x) x.ISDEFAULT = True)
                If ds IsNot Nothing Then
                    Daftar_DIAGNOSA_Default = ds.KDDIAGNOSA
                Else
                    Daftar_DIAGNOSA_Default = String.Empty
                End If
            Catch ex As Exception
                Daftar_DIAGNOSA_Default = String.Empty
                Throw ex
            End Try
        End Function
        Public Function UpdateKDPENDAFTARANRI(ByVal kdpendaftaran As String, ByVal KPENDAFTARANRI As String) As Boolean
            Try
                If Not oConnection.GetConnection Then
                    UpdateKDPENDAFTARANRI = False
                    Exit Function
                End If

                UpdateKDPENDAFTARANRI = True

                Dim ds = oConnection.db.S_PENDAFTARAN_Hs.FirstOrDefault(Function(x) x.KDPENDAFTARAN = kdpendaftaran)

                If ds IsNot Nothing Then
                    ds.KDPENDAFTARAN_AWAL = KPENDAFTARANRI

                    oConnection.db.SubmitChanges()
                End If

            Catch ex As Exception
                UpdateKDPENDAFTARANRI = False
                Throw ex
            End Try
        End Function
        Public Function UpdateCetak(ByVal kdpendaftaran As String) As Boolean
            Try
                If Not oConnection.GetConnection Then
                    UpdateCetak = False
                    Exit Function
                End If

                UpdateCetak = True

                Dim ds = oConnection.db.S_PENDAFTARAN_Hs.FirstOrDefault(Function(x) x.KDPENDAFTARAN = kdpendaftaran)

                ds.CETAK += 1

                oConnection.db.SubmitChanges()

            Catch ex As Exception
                UpdateCetak = False
                Throw ex
            End Try
        End Function
        Public Function UpdateDataBatal(ByVal kdpendaftaran As String) As Boolean
            Try
                If Not oConnection.GetConnection Then
                    UpdateDataBatal = False
                    Exit Function
                End If

                UpdateDataBatal = True

                Dim ds = oConnection.db.S_PENDAFTARAN_Hs.FirstOrDefault(Function(x) x.KDPENDAFTARAN = kdpendaftaran)

                If ds.STATUSDAFTAR = 0 Then
                    ds.STATUSDAFTAR = 1
                Else
                    ds.STATUSDAFTAR = 0
                End If

                oConnection.db.SubmitChanges()

            Catch ex As Exception
                UpdateDataBatal = False
                Throw ex
            End Try
        End Function
        Public Function UpdateSEP(ByVal kdpendaftaran As String, ByVal nomorsep As String, ByVal sREQ As String, ByVal sRESPONSE As String, ByVal KDUSER As String, ByVal sKDSKDP As String, ByVal sINFORMASIPRB As String) As Boolean
            Try
                If Not oConnection.GetConnection Then
                    UpdateSEP = False
                    Exit Function
                End If

                UpdateSEP = True

                Dim ds = oConnection.db.S_PENDAFTARAN_Hs.FirstOrDefault(Function(x) x.KDPENDAFTARAN = kdpendaftaran)

                ds.DATEUPDATED = Now
                ds.NOMORSEP = nomorsep
                ds.REQUEST = sREQ
                ds.RESPON = sRESPONSE
                ds.KDUSER = KDUSER
                ds.NOMORSKDP = sKDSKDP
                ds.ISOFFLINE = False
                ds.INFORMASIPRB = sINFORMASIPRB

                oConnection.db.SubmitChanges()

            Catch ex As Exception
                UpdateSEP = False
                Throw ex
            End Try
        End Function
        Public Function UpdateCustomer(ByVal sKDCUSTOMER As String, ByVal sNOTELEPON As String, ByVal sKDDAFTAR1 As String, ByVal sKDDAFTAR2 As String, ByVal sKDDAFTAR3 As String, ByVal sKDDAFTAR6 As String, ByVal sNAMAPENANGUNG As String, ByVal sKDHUBUNGAN As String, ByVal sALAMAT As String, ByVal sNOTELEPONPENANGGUNG As String) As Boolean
            Try
                If Not oConnection.GetConnection Then
                    UpdateCustomer = False
                    Exit Function
                End If

                UpdateCustomer = True

                Dim ds = oConnection.db.M_CUSTOMERs.FirstOrDefault(Function(x) x.KDCUSTOMER = sKDCUSTOMER)

                ds.DATEUPDATED = Now
                ds.KDDAFTAR_L1 = sKDDAFTAR1
                ds.KDDAFTAR_L2 = sKDDAFTAR2
                ds.KDDAFTAR_L3 = sKDDAFTAR3
                'ds.KDDAFTAR_L4 = sKDDAFTAR4
                'ds.KDDAFTAR_L5 = sKDDAFTAR5
                ds.KDDAFTAR_L6 = sKDDAFTAR6
                ds.NOTELEPON = sNOTELEPON
                ds.NAMAPENANGUNGJAWAB = sNAMAPENANGUNG
                ds.ALAMATNAMAPENANGUNGJAWAB = sALAMAT
                ds.KDHUBUNGAN = sKDHUBUNGAN
                ds.NOMORTELEPONPENANGGUNGJAWAB = sNOTELEPONPENANGGUNG

                oConnection.db.SubmitChanges()

            Catch ex As Exception
                UpdateCustomer = False
                Throw ex
            End Try
        End Function
    End Class
End Namespace