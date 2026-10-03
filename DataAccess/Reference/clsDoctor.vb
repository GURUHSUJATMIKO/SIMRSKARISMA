Namespace Reference
    Public Class clsDoctor
        Public oConnection As Setting.clsConnectionMain = Nothing
        Public oConnectionDaftar As Setting.clsConnectionAdmision = Nothing
        Public oError As Setting.clsError = Nothing

        Public sMODUL As String = ""
        Public sREFERENCE As String = ""
        Public sSTATUS As String = ""
        Public sLASTNUMBER As Integer = 0

        Public Sub New(Optional ByVal sConnection As String = "")
            If sConnection = "" Then
                oConnection = New Setting.clsConnectionMain
                oConnectionDaftar = New Setting.clsConnectionAdmision
                oError = New Setting.clsError
            Else
                oConnection = New Setting.clsConnectionMain("TAX")
                'oConnectionDaftar = New Setting.clsConnectionAdmision("TAX")
                oError = New Setting.clsError("TAX")
            End If

            sMODUL = "DOCTOR"
        End Sub
        Protected Overrides Sub Finalize()
            MyBase.Finalize()
            oConnection = Nothing
            GC.SuppressFinalize(Me)
        End Sub
        Public Function GetStructureHeader() As M_DOCTOR
            If Not oConnection.GetConnection() Then
                GetStructureHeader = Nothing
            End If
            GetStructureHeader = New M_DOCTOR
        End Function
        Public Function GetStructureDetail() As M_DOCTOR_JADWAL
            If Not oConnection.GetConnection() Then
                GetStructureDetail = Nothing
            End If
            GetStructureDetail = New M_DOCTOR_JADWAL
        End Function
        Public Function GetStructureDetailList() As List(Of M_DOCTOR_JADWAL)
            If Not oConnection.GetConnection() Then
                GetStructureDetailList = Nothing
            End If
            GetStructureDetailList = New List(Of M_DOCTOR_JADWAL)
        End Function
        Public Function GetData() As List(Of M_DOCTOR)
            If Not oConnection.GetConnection() Then
                GetData = Nothing
                Exit Function
            End If
            GetData = oConnection.db.M_DOCTORs.OrderBy(Function(x) x.NAME_DISPLAY).ToList()
        End Function
        Public Function GetData(ByVal sKDDOCTOR As String) As M_DOCTOR
            If Not oConnection.GetConnection() Then
                GetData = Nothing
                Exit Function
            End If
            GetData = oConnection.db.M_DOCTORs.FirstOrDefault(Function(x) x.KDDOCTOR = sKDDOCTOR)
        End Function
        Public Function GetDataByKDUSER(ByVal sKDUSER As String) As M_DOCTOR
            If Not oConnection.GetConnection() Then
                GetDataByKDUSER = Nothing
                Exit Function
            End If
            GetDataByKDUSER = oConnection.db.M_DOCTORs.FirstOrDefault(Function(x) x.KDUSER = sKDUSER)
        End Function
        Public Function GetDataByName(ByVal sKDDOCTOR As String) As M_DOCTOR
            If Not oConnection.GetConnection() Then
                GetDataByName = Nothing
                Exit Function
            End If
            GetDataByName = oConnection.db.M_DOCTORs.FirstOrDefault(Function(x) x.NAME_DISPLAY.Contains(sKDDOCTOR))
        End Function
        Public Function GetDataByIDUser(ByVal sKDUSER As String) As M_DOCTOR
            If Not oConnection.GetConnection() Then
                GetDataByIDUser = Nothing
                Exit Function
            End If
            GetDataByIDUser = oConnection.db.M_DOCTORs.FirstOrDefault(Function(x) x.KDUSER = sKDUSER)
        End Function
        Public Function GetDataSync() As List(Of M_DOCTOR)
            If Not oConnection.GetConnection() Then
                GetDataSync = Nothing
                Exit Function
            End If
            GetDataSync = oConnection.db.M_DOCTORs.OrderBy(Function(x) x.NAME_DISPLAY).ToList()
        End Function
        Public Function GetDataDetail() As List(Of M_DOCTOR_JADWAL)
            If Not oConnection.GetConnection() Then
                GetDataDetail = Nothing
                Exit Function
            End If
            GetDataDetail = oConnection.db.M_DOCTOR_JADWALs.ToList()
        End Function
        Public Function GetDataDetail(ByVal sKDDOCTOR As String) As List(Of M_DOCTOR_JADWAL)
            If Not oConnection.GetConnection() Then
                GetDataDetail = Nothing
                Exit Function
            End If
            GetDataDetail = oConnection.db.M_DOCTOR_JADWALs.Where(Function(x) x.KDDOCTOR = sKDDOCTOR).ToList()
        End Function
        Public Function GetDataDetailJadwal(ByVal sKDDEPARTMENT As String, ByVal sHARI As String) As List(Of M_DOCTOR_JADWAL)
            If Not oConnection.GetConnection() Then
                GetDataDetailJadwal = Nothing
                Exit Function
            End If
            GetDataDetailJadwal = oConnection.db.M_DOCTOR_JADWALs.Where(Function(x) x.M_DOCTOR.KDDEPARTMENT = sKDDEPARTMENT And x.HARI = sHARI And x.M_DOCTOR.VCLAIM_KDDPJP <> "").ToList()
        End Function
        Public Function GetDataJadwalDokter(ByVal sKDDOCTOR As String, ByVal sHARI As String) As I_JADWALDOCTOR_D
            If Not oConnectionDaftar.GetConnection() Then
                GetDataJadwalDokter = Nothing
                Exit Function
            End If
            GetDataJadwalDokter = oConnectionDaftar.db.I_JADWALDOCTOR_Ds.FirstOrDefault(Function(x) x.I_JADWALDOCTOR_H.KDDOCTOR = sKDDOCTOR And x.HARI = sHARI)
        End Function
        Public Function GetDataJadwalLibur(ByVal sKDJADWAL As String) As A_JADWAL
            If Not oConnectionDaftar.GetConnection() Then
                GetDataJadwalLibur = Nothing
                Exit Function
            End If
            GetDataJadwalLibur = oConnectionDaftar.db.A_JADWALs.FirstOrDefault(Function(x) x.KDJADWAL = sKDJADWAL)
        End Function
        Public Function IsExist(ByVal sNAME_DISPLAY As String) As Boolean
            If Not oConnection.GetConnection() Then
                IsExist = False
                Exit Function
            End If

            Dim ds = oConnection.db.M_DOCTORs.FirstOrDefault(Function(x) x.NAME_DISPLAY = sNAME_DISPLAY)

            If ds IsNot Nothing Then
                IsExist = True
            Else
                IsExist = False
            End If
        End Function
        Public Function InsertData(ByVal entity As M_DOCTOR, ByVal entityDetail As List(Of M_DOCTOR_JADWAL)) As Boolean
            Try
                If Not oConnection.GetConnection() Then
                    InsertData = False
                    Exit Function
                End If

                sREFERENCE = entity.KDDOCTOR
                sSTATUS = "INSERT"

                If entity.KDDOCTOR = "" Then
                    'Generate Auto Number
                    Try
                        sLASTNUMBER = CInt(oConnection.db.M_DOCTORs.OrderByDescending(Function(x) x.KDDOCTOR).FirstOrDefault().KDDOCTOR.Remove(0, (sMODUL & " _ ").Length)) + 1
                    Catch ex As Exception
                        sLASTNUMBER = 1
                    End Try
                    'End Generate

                    entity.KDDOCTOR = sMODUL & "_" & AutoNumberCode(sLASTNUMBER)

                    For Each iLoop In entityDetail
                        iLoop.KDDOCTOR = entity.KDDOCTOR
                    Next
                End If

                Try
                    oConnection.db.M_DOCTORs.InsertOnSubmit(entity)
                    oConnection.db.M_DOCTOR_JADWALs.InsertAllOnSubmit(entityDetail)
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
            Catch ex As Exception
                InsertData = False
                oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                Throw ex
            End Try
        End Function
        Public Function UpdateData(ByVal entity As M_DOCTOR, ByVal entityDetail As List(Of M_DOCTOR_JADWAL)) As Boolean
            Try
                If Not oConnection.GetConnection() Then
                    UpdateData = False
                    Exit Function
                End If

                sREFERENCE = entity.KDDOCTOR
                sSTATUS = "UPDATE"

                Dim ds = oConnection.db.M_DOCTORs.FirstOrDefault(Function(x) x.KDDOCTOR = entity.KDDOCTOR)
                Dim dsDetail = oConnection.db.M_DOCTOR_JADWALs.Where(Function(x) x.KDDOCTOR = entity.KDDOCTOR)

                Try
                    oConnection.db.M_DOCTORs.DeleteOnSubmit(ds)
                    oConnection.db.M_DOCTORs.InsertOnSubmit(entity)
                    If dsDetail.Count > 0 Then
                        oConnection.db.M_DOCTOR_JADWALs.DeleteAllOnSubmit(dsDetail)
                    End If
                    If entityDetail.Count > 0 Then
                        oConnection.db.M_DOCTOR_JADWALs.InsertAllOnSubmit(entityDetail)
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
        Public Function DeleteData(ByVal sKDDOCTOR As String) As Boolean
            Try
                If Not oConnection.GetConnection() Then
                    DeleteData = False
                    Exit Function
                End If

                sREFERENCE = sKDDOCTOR
                sSTATUS = "DELETE"

                Dim ds = oConnection.db.M_DOCTORs.FirstOrDefault(Function(x) x.KDDOCTOR = sKDDOCTOR)
                Dim dsDetail = oConnection.db.M_DOCTOR_JADWALs.Where(Function(x) x.KDDOCTOR = sKDDOCTOR)

                Try
                    oConnection.db.M_DOCTORs.DeleteOnSubmit(ds)
                    oConnection.db.M_DOCTOR_JADWALs.DeleteAllOnSubmit(dsDetail)
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
        Public Function Spesialistik_Default() As String
            Try
                If Not oConnection.GetConnection() Then
                    Spesialistik_Default = String.Empty
                    Exit Function
                End If

                Dim ds = oConnection.db.M_SPESIALISTIKs.FirstOrDefault(Function(x) x.ISDEFAULT = True)
                If ds IsNot Nothing Then
                    Spesialistik_Default = ds.KDSPESIALISTIK
                Else
                    Spesialistik_Default = String.Empty
                End If
            Catch ex As Exception
                Spesialistik_Default = String.Empty
                Throw ex
            End Try
        End Function
    End Class
End Namespace