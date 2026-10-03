Imports System.Threading

Namespace Admission
    Public Class clsPendaftaran_KunjunganRuangan
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
            sMODUL = "KP"

            oCounter = New Setting.clsCounter
        End Sub
        Protected Overrides Sub Finalize()
            MyBase.Finalize()
            oConnection = Nothing
            GC.SuppressFinalize(Me)
        End Sub
        Public Function GetStructureHeader() As S_PENDAFTARAN_KUNJUNGANRUANGAN
            If Not oConnection.GetConnection() Then
                GetStructureHeader = Nothing
            End If
            GetStructureHeader = New S_PENDAFTARAN_KUNJUNGANRUANGAN
        End Function
        Public Function GetData() As List(Of S_PENDAFTARAN_KUNJUNGANRUANGAN)
            If Not oConnection.GetConnection() Then
                GetData = Nothing
                Exit Function
            End If
            GetData = oConnection.db.S_PENDAFTARAN_KUNJUNGANRUANGANs.OrderByDescending(Function(x) x.KDKUNJUNGAN_RUANGAN).ToList()
        End Function
        Public Function GetDataByRuangan(ByVal Paramater As String) As List(Of S_PENDAFTARAN_KUNJUNGANRUANGAN)
            If Not oConnection.GetConnection() Then
                GetDataByRuangan = Nothing
                Exit Function
            End If
            GetDataByRuangan = oConnection.db.S_PENDAFTARAN_KUNJUNGANRUANGANs.Where(Function(x) x.KDRUANGRAWAT = Paramater).OrderByDescending(Function(x) x.KDKUNJUNGAN_RUANGAN).ToList()
        End Function
        Public Function GetData(ByVal Parameter As String) As S_PENDAFTARAN_KUNJUNGANRUANGAN
            If Not oConnection.GetConnection() Then
                GetData = Nothing
                Exit Function
            End If
            GetData = oConnection.db.S_PENDAFTARAN_KUNJUNGANRUANGANs.FirstOrDefault(Function(x) x.KDKUNJUNGAN_RUANGAN = Parameter)
        End Function
        Public Function GetData(ByVal sDATEFROM As DateTime, ByVal sDATETO As DateTime) As List(Of S_PENDAFTARAN_KUNJUNGANRUANGAN)
            If Not oConnection.GetConnection() Then
                GetData = Nothing
                Exit Function
            End If
            GetData = oConnection.db.S_PENDAFTARAN_KUNJUNGANRUANGANs.Where(Function(x) x.DATE_MASUK >= sDATEFROM.ToString("yyyy-MM-dd") & " 00:00:00" And x.DATE_MASUK <= sDATETO.ToString("yyyy-MM-dd") & " 23:59:59").OrderByDescending(Function(x) x.KDKUNJUNGAN_RUANGAN).ToList()
        End Function
        Public Function GetDataByRekamMedis(ByVal Parameter As String) As List(Of S_PENDAFTARAN_KUNJUNGANRUANGAN)
            If Not oConnection.GetConnection() Then
                GetDataByRekamMedis = Nothing
                Exit Function
            End If
            GetDataByRekamMedis = oConnection.db.S_PENDAFTARAN_KUNJUNGANRUANGANs.Where(Function(x) x.S_PENDAFTARAN_H.KDCUSTOMER = Parameter).ToList()
        End Function
        Public Function GetDataByRekamNama(ByVal Parameter As String) As List(Of S_PENDAFTARAN_KUNJUNGANRUANGAN)
            If Not oConnection.GetConnection() Then
                GetDataByRekamNama = Nothing
                Exit Function
            End If
            GetDataByRekamNama = oConnection.db.S_PENDAFTARAN_KUNJUNGANRUANGANs.Where(Function(x) x.S_PENDAFTARAN_H.M_CUSTOMER.NAME_DISPLAY.Contains(Parameter)).ToList()
        End Function
        Public Function GetDataRuangan1(ByVal Parameter As String) As S_PENDAFTARAN_KUNJUNGANRUANGAN
            If Not oConnection.GetConnection() Then
                GetDataRuangan1 = Nothing
                Exit Function
            End If
            GetDataRuangan1 = oConnection.db.S_PENDAFTARAN_KUNJUNGANRUANGANs.FirstOrDefault(Function(x) x.KDPENDAFTARAN = Parameter And x.MEMO = "RUANGAN 1")
        End Function
        Public Function GetDataPendaftaran(ByVal Parameter As String) As S_PENDAFTARAN_H
            If Not oConnection.GetConnection() Then
                GetDataPendaftaran = Nothing
                Exit Function
            End If
            GetDataPendaftaran = oConnection.db.S_PENDAFTARAN_Hs.FirstOrDefault(Function(x) x.KDPENDAFTARAN = Parameter)
        End Function
        Public Function InsertData(ByVal entity As S_PENDAFTARAN_KUNJUNGANRUANGAN, ByVal MODUL As String) As String
            Try
                If Not oConnection.GetConnection() Then
                    InsertData = ""
                    Exit Function
                End If

                sREFERENCE = entity.KDKUNJUNGAN_RUANGAN
                sSTATUS = "INSERT"

                Try
                    sMODUL = Day(entity.DATE_MASUK) & Month(entity.DATE_MASUK) & Year(entity.DATE_MASUK) & "-" & MODUL

                    If entity.KDKUNJUNGAN_RUANGAN = String.Empty Then
                        sLASTNUMBER = oCounter.GetLastNumberdDay(sMODUL, entity.DATE_MASUK)
                        If sLASTNUMBER = 0 Then
                            Try
                                oCounter.InsertData(sMODUL, entity.DATE_MASUK)
                                sLASTNUMBER = oCounter.GetLastNumberdDay(sMODUL, entity.DATE_MASUK)
                            Catch ex As Exception
                                sLASTNUMBER = 0
                            End Try
                        End If

                        sLASTNUMBER = sLASTNUMBER + 1
                        entity.KDKUNJUNGAN_RUANGAN = sMODUL & sLASTNUMBER.ToString.PadLeft(3, "0")

                        Try
                            oCounter.UpdateData(sMODUL, sLASTNUMBER + 1, Day(entity.DATE_MASUK), Month(entity.DATE_MASUK), Year(entity.DATE_MASUK))
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
                    oConnection.db.S_PENDAFTARAN_KUNJUNGANRUANGANs.InsertOnSubmit(entity)
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

                InsertData = entity.KDKUNJUNGAN_RUANGAN
                'UpdateNomorAntrian(entity.KDPENDAFTARAN, entity.KDKUNJUNGAN_RUANGAN)
                UpdateDataIsTerisi(entity.KDRUANGRAWAT, entity.SEQ, True)
            Catch ex As Exception
                InsertData = ""
                oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                Throw ex
            End Try
        End Function
        Public Function UpdateData(ByVal entity As S_PENDAFTARAN_KUNJUNGANRUANGAN) As Boolean
            Try
                If Not oConnection.GetConnection() Then
                    UpdateData = False
                    Exit Function
                End If

                sREFERENCE = entity.KDKUNJUNGAN_RUANGAN
                sSTATUS = "UPDATE"

                Dim ds = oConnection.db.S_PENDAFTARAN_KUNJUNGANRUANGANs.FirstOrDefault(Function(x) x.KDKUNJUNGAN_RUANGAN = entity.KDKUNJUNGAN_RUANGAN)
                Dim KDRUANGRAWAT As String = String.Empty
                Dim SEQ As Integer = 0

                Try
                    KDRUANGRAWAT = ds.KDRUANGRAWAT
                    SEQ = ds.SEQ

                    oConnection.db.S_PENDAFTARAN_KUNJUNGANRUANGANs.DeleteOnSubmit(ds)
                    oConnection.db.S_PENDAFTARAN_KUNJUNGANRUANGANs.InsertOnSubmit(entity)

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

                Dim dsKunjunganRuangan = oConnection.db.S_PENDAFTARAN_KUNJUNGANRUANGANs.Where(Function(x) x.KDPENDAFTARAN = entity.KDPENDAFTARAN)

                Dim sKunjungan As Boolean = False

                For Each xloop In dsKunjunganRuangan
                    If xloop.MEMO <> "RUANGAN 1" Then
                        sKunjungan = True
                    End If
                Next

                If sKunjungan = False Then
                    UpdateDataIsTerisi(KDRUANGRAWAT, SEQ, False)
                    UpdateDataIsTerisi(entity.KDRUANGRAWAT, entity.SEQ, True)
                End If
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

                Dim ds = oConnection.db.S_PENDAFTARAN_KUNJUNGANRUANGANs.FirstOrDefault(Function(x) x.KDKUNJUNGAN_RUANGAN.Contains(Parameter))
                Dim KDRUANGRAWAT As String = String.Empty
                Dim SEQ As Integer = 0

                If ds IsNot Nothing Then
                    Try
                        KDRUANGRAWAT = ds.KDRUANGRAWAT
                        SEQ = ds.SEQ
                        oConnection.db.S_PENDAFTARAN_KUNJUNGANRUANGANs.DeleteOnSubmit(ds)
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

                UpdateDataIsTerisi(KDRUANGRAWAT, SEQ, False)

            Catch ex As Exception
                DeleteData = False
                oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                Throw ex
            End Try
        End Function
        Public Function UpdateNomorAntrian(ByVal sKDPENDAFATRAN As String, ByVal sNOMORANTRIAN As String) As Boolean
            Try
                If Not oConnection.GetConnection Then
                    UpdateNomorAntrian = False
                    Exit Function
                End If

                Dim ds = oConnection.db.S_PENDAFTARAN_Hs.FirstOrDefault(Function(x) x.KDPENDAFTARAN = sKDPENDAFATRAN)

                ds.NOMORANTRIAN = sNOMORANTRIAN
                ds.NOMORLABEL = Microsoft.VisualBasic.Right(sNOMORANTRIAN, 4)

                oConnection.db.SubmitChanges()

                UpdateNomorAntrian = True

            Catch ex As Exception
                UpdateNomorAntrian = False
                Throw ex
            End Try
        End Function
        Public Function UpdateDataIsCheked(ByVal sKDKUNJUNGAN_RUANGAN As String, ByVal sISCHEKED As Boolean) As Boolean
            Try
                If Not oConnection.GetConnection Then
                    UpdateDataIsCheked = False
                    Exit Function
                End If

                UpdateDataIsCheked = sISCHEKED

                Dim ds = oConnection.db.S_PENDAFTARAN_KUNJUNGANRUANGANs.FirstOrDefault(Function(x) x.KDKUNJUNGAN_RUANGAN = sKDKUNJUNGAN_RUANGAN)

                ds.ISCHEKED = True

                oConnection.db.SubmitChanges()

            Catch ex As Exception
                UpdateDataIsCheked = False
                Throw ex
            End Try
        End Function
        Public Function UpdateDataIsTerisi(ByVal sKDRUANGRAWAT As String, ByVal sSEQ As Integer, ByVal sIsterisi As Boolean) As Boolean
            Try
                If Not oConnection.GetConnection Then
                    UpdateDataIsTerisi = False
                    Exit Function
                End If

                UpdateDataIsTerisi = True

                Dim ds = oConnection.db.M_RUANGRAWAT_Ds.FirstOrDefault(Function(x) x.KDRUANGRAWAT = sKDRUANGRAWAT And x.SEQ = sSEQ)

                ds.ISTERISI = sIsterisi

                oConnection.db.SubmitChanges()

            Catch ex As Exception
                UpdateDataIsTerisi = False
                Throw ex
            End Try
        End Function
        'Public Function UpdateDataFix(ByVal isNew As Boolean) As Boolean
        '    Try
        '        If Not oConnection.GetConnection Then
        '            UpdateDataFix = False
        '            Exit Function
        '        End If


        '        Dim entity = oConnection.db.S_PENDAFTARAN_KUNJUNGANRUANGANs

        '        For Each iLoop In entity
        '            Dim sKDKUNJUNGAN_RUANGAN = iLoop.KDKUNJUNGAN_RUANGAN
        '            Dim entityDetail = oConnection.db.F_PENDAFTARAN_Ds.Where(Function(x) x.KDKUNJUNGAN_RUANGAN = sKDKUNJUNGAN_RUANGAN And x.SEQ < 100)
        '            Dim entityDetail_R = oConnection.db.F_PENDAFTARAN_Ds.Where(Function(x) x.KDKUNJUNGAN_RUANGAN = sKDKUNJUNGAN_RUANGAN And x.SEQ >= 100)

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