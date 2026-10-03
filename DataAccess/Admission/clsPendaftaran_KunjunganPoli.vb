Imports System.Threading
Imports System.Data.SqlClient

Namespace Admission
    Public Class clsPendaftaran_KunjunganPoli
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
        Public Function GetStructureHeader() As S_PENDAFTARAN_KUNJUNGANPOLI
            If Not oConnection.GetConnection() Then
                GetStructureHeader = Nothing
            End If
            GetStructureHeader = New S_PENDAFTARAN_KUNJUNGANPOLI
        End Function
        Public Function GetData() As List(Of S_PENDAFTARAN_KUNJUNGANPOLI)
            If Not oConnection.GetConnection() Then
                GetData = Nothing
                Exit Function
            End If
            GetData = oConnection.db.S_PENDAFTARAN_KUNJUNGANPOLIs.OrderByDescending(Function(x) x.KDKUNJUNGAN_POLI).ToList()
        End Function
        Public Function GetData(ByVal sDATEFROM As DateTime, ByVal sDATETO As DateTime) As List(Of S_PENDAFTARAN_KUNJUNGANPOLI)
            If Not oConnection.GetConnection() Then
                GetData = Nothing
                Exit Function
            End If
            GetData = oConnection.db.S_PENDAFTARAN_KUNJUNGANPOLIs.Where(Function(x) x.DATE_MASUK >= sDATEFROM.ToString("yyyy-MM-dd") & " 00:00:00" And x.DATE_MASUK <= sDATETO.ToString("yyyy-MM-dd") & " 23:59:59").OrderByDescending(Function(x) x.KDKUNJUNGAN_POLI).ToList()
        End Function
        Public Function GetData(ByVal Parameter As String) As S_PENDAFTARAN_KUNJUNGANPOLI
            If Not oConnection.GetConnection() Then
                GetData = Nothing
                Exit Function
            End If
            GetData = oConnection.db.S_PENDAFTARAN_KUNJUNGANPOLIs.FirstOrDefault(Function(x) x.KDKUNJUNGAN_POLI = Parameter)
        End Function
        Public Function GetDataByRekamMedis(ByVal Parameter As String) As List(Of S_PENDAFTARAN_KUNJUNGANPOLI)
            If Not oConnection.GetConnection() Then
                GetDataByRekamMedis = Nothing
                Exit Function
            End If
            GetDataByRekamMedis = oConnection.db.S_PENDAFTARAN_KUNJUNGANPOLIs.Where(Function(x) x.S_PENDAFTARAN_H.KDCUSTOMER = Parameter And x.S_PENDAFTARAN_H.KDPENDAFTARAN_AWAL = "").ToList()
        End Function
        Public Function GetDataByRekamNama(ByVal Parameter As String) As List(Of S_PENDAFTARAN_KUNJUNGANPOLI)
            If Not oConnection.GetConnection() Then
                GetDataByRekamNama = Nothing
                Exit Function
            End If
            GetDataByRekamNama = oConnection.db.S_PENDAFTARAN_KUNJUNGANPOLIs.Where(Function(x) x.S_PENDAFTARAN_H.M_CUSTOMER.NAME_DISPLAY.Contains(Parameter) And x.S_PENDAFTARAN_H.KDPENDAFTARAN_AWAL = "").ToList()
        End Function
        Public Function GetDataPoli1(ByVal Parameter As String) As S_PENDAFTARAN_KUNJUNGANPOLI
            If Not oConnection.GetConnection() Then
                GetDataPoli1 = Nothing
                Exit Function
            End If
            GetDataPoli1 = oConnection.db.S_PENDAFTARAN_KUNJUNGANPOLIs.FirstOrDefault(Function(x) x.KDPENDAFTARAN = Parameter And x.MEMO = "POLI 1")
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
        Public Function InsertData(ByVal entity As S_PENDAFTARAN_KUNJUNGANPOLI, ByVal MODUL As String) As String
            Try
                If Not oConnection.GetConnection() Then
                    InsertData = ""
                    Exit Function
                End If

                sREFERENCE = entity.KDKUNJUNGAN_POLI
                sSTATUS = "INSERT"

                Try
                    sMODUL = Day(entity.DATE_MASUK) & Month(entity.DATE_MASUK) & Year(entity.DATE_MASUK) & "-" & MODUL

                    If entity.KDKUNJUNGAN_POLI = String.Empty Then
                        sLASTNUMBER = oCounter.GetLastNumberdDay(sMODUL, entity.DATE_MASUK)
                        If sLASTNUMBER = 0 Then
                            Try
                                oCounter.InsertData(sMODUL, entity.DATE_MASUK)
                                sLASTNUMBER = oCounter.GetLastNumberdDay(sMODUL, entity.DATE_MASUK)
                            Catch ex As Exception
                                sLASTNUMBER = 0
                            End Try
                        End If

                        Try
                            oCounter.UpdateData(sMODUL, sLASTNUMBER + 1, Day(entity.DATE_MASUK), Month(entity.DATE_MASUK), Year(entity.DATE_MASUK))
                        Catch ex As Exception
                            oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                            Throw ex
                        End Try

                        sLASTNUMBER = sLASTNUMBER + 1
                        entity.KDKUNJUNGAN_POLI = sMODUL & sLASTNUMBER.ToString.PadLeft(3, "0")

                    End If

                Catch ex As Exception
                    oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                    Throw ex
                End Try

                Try
                    oConnection.db.S_PENDAFTARAN_KUNJUNGANPOLIs.InsertOnSubmit(entity)
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

                InsertData = entity.KDKUNJUNGAN_POLI

                UpdateNomorAntrian(entity.KDPENDAFTARAN, entity.KDKUNJUNGAN_POLI)

            Catch ex As Exception
                InsertData = ""
                oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                Throw ex
            End Try
        End Function
        Public Function UpdateData(ByVal entity As S_PENDAFTARAN_KUNJUNGANPOLI) As Boolean
            Try
                If Not oConnection.GetConnection() Then
                    UpdateData = False
                    Exit Function
                End If

                sREFERENCE = entity.KDKUNJUNGAN_POLI
                sSTATUS = "UPDATE"

                Dim ds = oConnection.db.S_PENDAFTARAN_KUNJUNGANPOLIs.FirstOrDefault(Function(x) x.KDKUNJUNGAN_POLI = entity.KDKUNJUNGAN_POLI)

                Try
                    oConnection.db.S_PENDAFTARAN_KUNJUNGANPOLIs.DeleteOnSubmit(ds)
                    oConnection.db.S_PENDAFTARAN_KUNJUNGANPOLIs.InsertOnSubmit(entity)

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

                Dim ds = oConnection.db.S_PENDAFTARAN_KUNJUNGANPOLIs.FirstOrDefault(Function(x) x.KDKUNJUNGAN_POLI.Contains(Parameter))

                If ds IsNot Nothing Then
                    Try
                        oConnection.db.S_PENDAFTARAN_KUNJUNGANPOLIs.DeleteOnSubmit(ds)
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
        'Public Function UpdateDataFix(ByVal isNew As Boolean) As Boolean
        '    Try
        '        If Not oConnection.GetConnection Then
        '            UpdateDataFix = False
        '            Exit Function
        '        End If


        '        Dim entity = oConnection.db.S_PENDAFTARAN_KUNJUNGANPOLIs

        '        For Each iLoop In entity
        '            Dim sKDKUNJUNGAN_POLI = iLoop.KDKUNJUNGAN_POLI
        '            Dim entityDetail = oConnection.db.F_PENDAFTARAN_Ds.Where(Function(x) x.KDKUNJUNGAN_POLI = sKDKUNJUNGAN_POLI And x.SEQ < 100)
        '            Dim entityDetail_R = oConnection.db.F_PENDAFTARAN_Ds.Where(Function(x) x.KDKUNJUNGAN_POLI = sKDKUNJUNGAN_POLI And x.SEQ >= 100)

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