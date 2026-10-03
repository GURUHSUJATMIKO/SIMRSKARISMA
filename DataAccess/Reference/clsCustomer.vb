Namespace Reference
    Public Class clsCustomer
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

            sMODUL = "RM"
        End Sub
        Protected Overrides Sub Finalize()
            MyBase.Finalize()
            oConnection = Nothing
            GC.SuppressFinalize(Me)
        End Sub
        Public Function GetStructureHeader() As M_CUSTOMER
            If Not oConnection.GetConnection() Then
                GetStructureHeader = Nothing
            End If
            GetStructureHeader = New M_CUSTOMER
        End Function
        Public Function GetData() As List(Of M_CUSTOMER)
            If Not oConnection.GetConnection() Then
                GetData = Nothing
                Exit Function
            End If
            GetData = oConnection.db.M_CUSTOMERs.OrderBy(Function(x) x.NAME_DISPLAY).ToList()
        End Function
        Public Function GetData(ByVal sKDCUSTOMER As String) As M_CUSTOMER
            If Not oConnection.GetConnection() Then
                GetData = Nothing
                Exit Function
            End If
            GetData = oConnection.db.M_CUSTOMERs.FirstOrDefault(Function(x) x.KDCUSTOMER = sKDCUSTOMER)
        End Function
        Public Function GetDataSync(ByVal sKDCUSTOMER As String) As List(Of M_CUSTOMER)
            If Not oConnection.GetConnection() Then
                GetDataSync = Nothing
                Exit Function
            End If
            GetDataSync = oConnection.db.M_CUSTOMERs.Where(Function(x) x.KDCUSTOMER = sKDCUSTOMER).OrderBy(Function(x) x.NAME_DISPLAY).ToList()
        End Function
        Public Function GetDataSyncbyName(ByVal sNAME_DISPLAY As String) As List(Of M_CUSTOMER)
            If Not oConnection.GetConnection() Then
                GetDataSyncbyName = Nothing
                Exit Function
            End If
            GetDataSyncbyName = oConnection.db.M_CUSTOMERs.Where(Function(x) x.NAME_DISPLAY.Contains(sNAME_DISPLAY)).OrderBy(Function(x) x.NAME_DISPLAY).ToList()
        End Function
        Public Function GetDataSync() As List(Of M_CUSTOMER)
            If Not oConnection.GetConnection() Then
                GetDataSync = Nothing
                Exit Function
            End If
            GetDataSync = oConnection.db.M_CUSTOMERs.OrderBy(Function(x) x.NAME_DISPLAY).ToList()
        End Function
        Public Function IsExist(ByVal sKTP As String) As Boolean
            If Not oConnection.GetConnection() Then
                IsExist = False
                Exit Function
            End If

            Dim ds = oConnection.db.M_CUSTOMERs.FirstOrDefault(Function(x) x.KTP = sKTP)

            If ds IsNot Nothing Then
                IsExist = True
            Else
                IsExist = False
            End If
        End Function
        'Public Function GetDataKoneksi(ByVal Parameter As String) As SET_KONEKSI
        '    If Not oConnection.GetConnection() Then
        '        GetDataKoneksi = Nothing
        '        Exit Function
        '    End If
        '    GetDataKoneksi = oConnection.db.SET_KONEKSIs.FirstOrDefault(Function(x) x.NAME_DISPLAY = Parameter And x.ISACTIVE = True)
        'End Function
        Public Function InsertData(ByVal entity As M_CUSTOMER) As String
            Try
                If Not oConnection.GetConnection() Then
                    InsertData = ""
                    Exit Function
                End If

                sREFERENCE = entity.KDCUSTOMER
                sSTATUS = "INSERT"

                If entity.KDCUSTOMER = "" Then
                    'Generate Auto Number
                    'Dim DateCretaed As DateTime = "2021-02-01"

                    Try
                        sLASTNUMBER = oCounter.GetLastNumber(sMODUL)
                        'If sLASTNUMBER = 0 Then
                        '    Try
                        '        oCounter.InsertData(sMODUL, DateCretaed)
                        '        sLASTNUMBER = oCounter.GetLastNumber(sMODUL, DateCretaed)
                        '    Catch ex As Exception
                        '        sLASTNUMBER = 0
                        '    End Try
                        'End If

                    Catch ex As Exception
                        oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                        Throw ex
                    End Try

                    Try
                        oCounter.UpdateData(sMODUL, sLASTNUMBER + 1)
                    Catch ex As Exception
                        oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                        Throw ex
                    End Try

                    entity.KDCUSTOMER = AutoNumberRM(sLASTNUMBER + 1)
                    'End Generate
                Else
                    entity.KDCUSTOMER = entity.KDCUSTOMER
                End If

                Try
                    oConnection.db.M_CUSTOMERs.InsertOnSubmit(entity)
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

                InsertData = entity.KDCUSTOMER
            Catch ex As Exception
                InsertData = ""
                oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                Throw ex
            End Try
        End Function
        Public Function UpdateData(ByVal entity As M_CUSTOMER) As Boolean
            Try
                If Not oConnection.GetConnection() Then
                    UpdateData = False
                    Exit Function
                End If

                sREFERENCE = entity.KDCUSTOMER
                sSTATUS = "UPDATE"

                Dim ds = oConnection.db.M_CUSTOMERs.FirstOrDefault(Function(x) x.KDCUSTOMER = entity.KDCUSTOMER)

                Try
                    oConnection.db.M_CUSTOMERs.DeleteOnSubmit(ds)
                    oConnection.db.M_CUSTOMERs.InsertOnSubmit(entity)
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
        Public Function DeleteData(ByVal sKDCUSTOMER As String) As Boolean
            Try
                If Not oConnection.GetConnection() Then
                    DeleteData = False
                    Exit Function
                End If

                sREFERENCE = sKDCUSTOMER
                sSTATUS = "DELETE"

                Dim ds = oConnection.db.M_CUSTOMERs.FirstOrDefault(Function(x) x.KDCUSTOMER = sKDCUSTOMER)

                Try
                    oConnection.db.M_CUSTOMERs.DeleteOnSubmit(ds)
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
        Public Function AgamaDefault() As String
            Try
                If Not oConnection.GetConnection() Then
                    AgamaDefault = String.Empty
                    Exit Function
                End If

                Dim ds = oConnection.db.M_AGAMAs.FirstOrDefault(Function(x) x.ISDEFAULT = True)
                If ds IsNot Nothing Then
                    AgamaDefault = ds.KDAGAMA
                Else
                    AgamaDefault = String.Empty
                End If
            Catch ex As Exception
                AgamaDefault = String.Empty
                Throw ex
            End Try
        End Function
        Public Function SukuDefault() As String
            Try
                If Not oConnection.GetConnection() Then
                    SukuDefault = String.Empty
                    Exit Function
                End If

                Dim ds = oConnection.db.M_SUKUs.FirstOrDefault(Function(x) x.ISDEFAULT = True)
                If ds IsNot Nothing Then
                    SukuDefault = ds.KDSUKU
                Else
                    SukuDefault = String.Empty
                End If
            Catch ex As Exception
                SukuDefault = String.Empty
                Throw ex
            End Try
        End Function
        Public Function GetDataCustomerByApproval(ByVal Parameter As String, ByVal Categori As Integer) As List(Of M_CUSTOMER)
            If Not oConnection.GetConnection() Then
                GetDataCustomerByApproval = Nothing
                Exit Function
            End If
            GetDataCustomerByApproval = oConnection.db.M_CUSTOMERs.Where(Function(x) IIf(Categori = 0, x.KDCUSTOMER.Contains(Parameter), IIf(Categori = 1, x.NAME_DISPLAY.Contains(Parameter), x.KARTUBPJS.Contains(Parameter)))).OrderBy(Function(x) x.NAME_DISPLAY).ToList()
        End Function
    End Class
End Namespace