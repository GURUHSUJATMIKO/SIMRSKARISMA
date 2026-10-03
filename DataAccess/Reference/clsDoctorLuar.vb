Namespace Reference
    Public Class clsDoctorLuar
        Public oConnection As Setting.clsConnectionMain = Nothing
        Public oError As Setting.clsError = Nothing

        Public sMODUL As String = ""
        Public sREFERENCE As String = ""
        Public sSTATUS As String = ""
        Public sLASTNUMBER As Integer = 0

        Public Sub New(Optional ByVal sConnection As String = "")
            If sConnection = "" Then
                oConnection = New Setting.clsConnectionMain
                oError = New Setting.clsError
            Else
                oConnection = New Setting.clsConnectionMain("TAX")
                oError = New Setting.clsError("TAX")
            End If

            sMODUL = "DOCTOR"
        End Sub
        Protected Overrides Sub Finalize()
            MyBase.Finalize()
            oConnection = Nothing
            GC.SuppressFinalize(Me)
        End Sub
        Public Function GetStructureHeader() As M_DOCTOR_LUAR
            If Not oConnection.GetConnection() Then
                GetStructureHeader = Nothing
            End If
            GetStructureHeader = New M_DOCTOR_LUAR
        End Function
        Public Function GetData() As List(Of M_DOCTOR_LUAR)
            If Not oConnection.GetConnection() Then
                GetData = Nothing
                Exit Function
            End If
            GetData = oConnection.db.M_DOCTOR_LUARs.OrderBy(Function(x) x.NAME_DISPLAY).ToList()
        End Function
        Public Function GetData(ByVal sKDDOCTORLUAR As String) As M_DOCTOR_LUAR
            If Not oConnection.GetConnection() Then
                GetData = Nothing
                Exit Function
            End If
            GetData = oConnection.db.M_DOCTOR_LUARs.FirstOrDefault(Function(x) x.KDDOCTORLUAR = sKDDOCTORLUAR)
        End Function
        Public Function GetDataByName(ByVal sKDDOCTORLUAR As String) As M_DOCTOR_LUAR
            If Not oConnection.GetConnection() Then
                GetDataByName = Nothing
                Exit Function
            End If
            GetDataByName = oConnection.db.M_DOCTOR_LUARs.FirstOrDefault(Function(x) x.NAME_DISPLAY.Contains(sKDDOCTORLUAR))
        End Function
        Public Function GetDataSync() As List(Of M_DOCTOR_LUAR)
            If Not oConnection.GetConnection() Then
                GetDataSync = Nothing
                Exit Function
            End If
            GetDataSync = oConnection.db.M_DOCTOR_LUARs.OrderBy(Function(x) x.NAME_DISPLAY).ToList()
        End Function
        Public Function IsExist(ByVal sNAME_DISPLAY As String) As Boolean
            If Not oConnection.GetConnection() Then
                IsExist = False
                Exit Function
            End If

            Dim ds = oConnection.db.M_DOCTOR_LUARs.FirstOrDefault(Function(x) x.NAME_DISPLAY = sNAME_DISPLAY)

            If ds IsNot Nothing Then
                IsExist = True
            Else
                IsExist = False
            End If
        End Function
        Public Function InsertData(ByVal entity As M_DOCTOR_LUAR) As Boolean
            Try
                If Not oConnection.GetConnection() Then
                    InsertData = False
                    Exit Function
                End If

                sREFERENCE = entity.KDDOCTORLUAR
                sSTATUS = "INSERT"

                If entity.KDDOCTORLUAR = "" Then
                    'Generate Auto Number
                    Try
                        sLASTNUMBER = CInt(oConnection.db.M_DEPARTMENTs.OrderByDescending(Function(x) x.KDDEPARTMENT).FirstOrDefault().KDDEPARTMENT.Remove(0, (sMODUL & " _ ").Length)) + 1
                    Catch ex As Exception
                        sLASTNUMBER = 1
                    End Try
                    'End Generate

                    entity.KDDOCTORLUAR = sMODUL & "_" & AutoNumberCode(sLASTNUMBER)
                End If

                Try
                    oConnection.db.M_DOCTOR_LUARs.InsertOnSubmit(entity)
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
        Public Function UpdateData(ByVal entity As M_DOCTOR_LUAR) As Boolean
            Try
                If Not oConnection.GetConnection() Then
                    UpdateData = False
                    Exit Function
                End If

                sREFERENCE = entity.KDDOCTORLUAR
                sSTATUS = "UPDATE"

                Dim ds = oConnection.db.M_DOCTOR_LUARs.FirstOrDefault(Function(x) x.KDDOCTORLUAR = entity.KDDOCTORLUAR)

                Try
                    oConnection.db.M_DOCTOR_LUARs.DeleteOnSubmit(ds)
                    oConnection.db.M_DOCTOR_LUARs.InsertOnSubmit(entity)
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
        Public Function DeleteData(ByVal sKDDOCTORLUAR As String) As Boolean
            Try
                If Not oConnection.GetConnection() Then
                    DeleteData = False
                    Exit Function
                End If

                sREFERENCE = sKDDOCTORLUAR
                sSTATUS = "DELETE"

                Dim ds = oConnection.db.M_DOCTOR_LUARs.FirstOrDefault(Function(x) x.KDDOCTORLUAR = sKDDOCTORLUAR)

                Try
                    oConnection.db.M_DOCTOR_LUARs.DeleteOnSubmit(ds)
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