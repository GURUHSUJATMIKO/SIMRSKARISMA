Namespace Reference
    Public Class clsCustomerUnit
        Public oConnection As Setting.clsConnectionMain = Nothing
        Public oError As Setting.clsError = Nothing
        Public oCounter As Setting.clsCounter = Nothing

        Public sMODUL As String = ""
        Public sREFERENCE As String = ""
        Public sSTATUS As String = ""
        Public sLASTNUMBER As Integer = 0

        Public Sub New(Optional ByVal sConnection As String = "")
            If sConnection = "" Then
                oConnection = New Setting.clsConnectionMain
                oError = New Setting.clsError
                oCounter = New Setting.clsCounter

            Else
                oConnection = New Setting.clsConnectionMain("TAX")
                oError = New Setting.clsError("TAX")
                oCounter = New Setting.clsCounter("TAX")

            End If

            sMODUL = "RMUNIT"
        End Sub
        Protected Overrides Sub Finalize()
            MyBase.Finalize()
            oConnection = Nothing
            GC.SuppressFinalize(Me)
        End Sub
        Public Function GetStructureHeader() As M_CUSTOMER_UNIT
            If Not oConnection.GetConnection() Then
                GetStructureHeader = Nothing
            End If
            GetStructureHeader = New M_CUSTOMER_UNIT
        End Function
        Public Function GetData() As List(Of M_CUSTOMER_UNIT)
            If Not oConnection.GetConnection() Then
                GetData = Nothing
                Exit Function
            End If
            GetData = oConnection.db.M_CUSTOMER_UNITs.OrderBy(Function(x) x.NAME_DISPLAY).ToList()
        End Function
        Public Function GetData(ByVal sKDCUSTOMER_UNIT As String) As M_CUSTOMER_UNIT
            If Not oConnection.GetConnection() Then
                GetData = Nothing
                Exit Function
            End If
            GetData = oConnection.db.M_CUSTOMER_UNITs.FirstOrDefault(Function(x) x.KDCUSTOMER_UNIT = sKDCUSTOMER_UNIT)
        End Function
        Public Function GetDataSync(ByVal sKDCUSTOMER_UNIT As String) As List(Of M_CUSTOMER_UNIT)
            If Not oConnection.GetConnection() Then
                GetDataSync = Nothing
                Exit Function
            End If
            GetDataSync = oConnection.db.M_CUSTOMER_UNITs.Where(Function(x) x.KDCUSTOMER_UNIT = sKDCUSTOMER_UNIT).OrderBy(Function(x) x.NAME_DISPLAY).ToList()
        End Function
        Public Function GetDataSyncbyName(ByVal sNAME_DISPLAY As String) As List(Of M_CUSTOMER_UNIT)
            If Not oConnection.GetConnection() Then
                GetDataSyncbyName = Nothing
                Exit Function
            End If
            GetDataSyncbyName = oConnection.db.M_CUSTOMER_UNITs.Where(Function(x) x.NAME_DISPLAY.Contains(sNAME_DISPLAY)).OrderBy(Function(x) x.NAME_DISPLAY).ToList()
        End Function
        Public Function GetDataSync() As List(Of M_CUSTOMER_UNIT)
            If Not oConnection.GetConnection() Then
                GetDataSync = Nothing
                Exit Function
            End If
            GetDataSync = oConnection.db.M_CUSTOMER_UNITs.OrderBy(Function(x) x.NAME_DISPLAY).ToList()
        End Function
        Public Function InsertData(ByVal entity As M_CUSTOMER_UNIT) As String
            Try
                If Not oConnection.GetConnection() Then
                    InsertData = ""
                    Exit Function
                End If

                sREFERENCE = entity.KDCUSTOMER_UNIT
                sSTATUS = "INSERT"

                If entity.KDCUSTOMER_UNIT = "" Then
                    'Generate Auto Number
                    Dim DateCretaed As DateTime = "2021-02-01"

                    Try
                        sLASTNUMBER = oCounter.GetLastNumber(sMODUL)
                        If sLASTNUMBER = 0 Then
                            Try
                                oCounter.InsertData(sMODUL, DateCretaed)
                                sLASTNUMBER = oCounter.GetLastNumber(sMODUL, DateCretaed)
                            Catch ex As Exception
                                sLASTNUMBER = 0
                            End Try
                        End If

                    Catch ex As Exception
                        oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                        Throw ex
                    End Try

                    Try
                        oCounter.UpdateData(sMODUL, sLASTNUMBER + 1, Month(DateCretaed), Year(DateCretaed))
                    Catch ex As Exception
                        oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                        Throw ex
                    End Try

                    entity.KDCUSTOMER_UNIT = AutoNumberRM(sLASTNUMBER + 1)
                    'End Generate
                Else
                    entity.KDCUSTOMER_UNIT = entity.KDCUSTOMER_UNIT
                End If

                Try
                    oConnection.db.M_CUSTOMER_UNITs.InsertOnSubmit(entity)
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

                InsertData = entity.KDCUSTOMER_UNIT
            Catch ex As Exception
                InsertData = ""
                oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                Throw ex
            End Try
        End Function
        Public Function UpdateData(ByVal entity As M_CUSTOMER_UNIT) As Boolean
            Try
                If Not oConnection.GetConnection() Then
                    UpdateData = False
                    Exit Function
                End If

                sREFERENCE = entity.KDCUSTOMER_UNIT
                sSTATUS = "UPDATE"

                Dim ds = oConnection.db.M_CUSTOMER_UNITs.FirstOrDefault(Function(x) x.KDCUSTOMER_UNIT = entity.KDCUSTOMER_UNIT)

                Try
                    oConnection.db.M_CUSTOMER_UNITs.DeleteOnSubmit(ds)
                    oConnection.db.M_CUSTOMER_UNITs.InsertOnSubmit(entity)
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
        Public Function DeleteData(ByVal sKDCUSTOMER_UNIT As String) As Boolean
            Try
                If Not oConnection.GetConnection() Then
                    DeleteData = False
                    Exit Function
                End If

                sREFERENCE = sKDCUSTOMER_UNIT
                sSTATUS = "DELETE"

                Dim ds = oConnection.db.M_CUSTOMER_UNITs.FirstOrDefault(Function(x) x.KDCUSTOMER_UNIT = sKDCUSTOMER_UNIT)

                Try
                    oConnection.db.M_CUSTOMER_UNITs.DeleteOnSubmit(ds)
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