Imports System.Threading
Imports System.Data.SqlClient

Namespace Admission
    Public Class clsSKD
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
            sMODUL = "SKD"

            oCounter = New Setting.clsCounter
        End Sub
        Protected Overrides Sub Finalize()
            MyBase.Finalize()
            oConnection = Nothing
            GC.SuppressFinalize(Me)
        End Sub
        Public Function GetStructureHeader() As S_PENDAFTARAN_SKD
            If Not oConnection.GetConnection() Then
                GetStructureHeader = Nothing
            End If
            GetStructureHeader = New S_PENDAFTARAN_SKD
        End Function
        Public Function GetData() As List(Of S_PENDAFTARAN_SKD)
            If Not oConnection.GetConnection() Then
                GetData = Nothing
                Exit Function
            End If
            GetData = oConnection.db.S_PENDAFTARAN_SKDs.OrderByDescending(Function(x) x.KDSKD).ToList()
        End Function
        Public Function GetData(ByVal sDateFrom As DateTime, ByVal sDateTo As Date) As List(Of S_PENDAFTARAN_SKD)
            If Not oConnection.GetConnection() Then
                GetData = Nothing
                Exit Function
            End If
            GetData = oConnection.db.S_PENDAFTARAN_SKDs.Where(Function(x) x.DATE >= sDateFrom.ToString("yyyy-MM-dd") & " 00:00:00" And x.DATE <= sDateTo.ToString("yyyy-MM-dd") & " 23:59:59").OrderByDescending(Function(x) x.KDSKD).ToList()
        End Function
        Public Function GetDataKontrol(ByVal sDateFrom As DateTime, ByVal sDateTo As Date) As List(Of S_PENDAFTARAN_SKD)
            If Not oConnection.GetConnection() Then
                GetDataKontrol = Nothing
                Exit Function
            End If
            GetDataKontrol = oConnection.db.S_PENDAFTARAN_SKDs.Where(Function(x) x.DATEKONTROL >= sDateFrom.ToString("yyyy-MM-dd") & " 00:00:00" And x.DATEKONTROL <= sDateTo.ToString("yyyy-MM-dd") & " 23:59:59").OrderByDescending(Function(x) x.KDSKD).ToList()
        End Function
        Public Function GetData(ByVal Parameter As String) As S_PENDAFTARAN_SKD
            If Not oConnection.GetConnection() Then
                GetData = Nothing
                Exit Function
            End If
            GetData = oConnection.db.S_PENDAFTARAN_SKDs.FirstOrDefault(Function(x) x.KDSKD = Parameter)
        End Function
        Public Function GetDataPendaftaran(ByVal Parameter As String) As S_PENDAFTARAN_SKD
            If Not oConnection.GetConnection() Then
                GetDataPendaftaran = Nothing
                Exit Function
            End If
            GetDataPendaftaran = oConnection.db.S_PENDAFTARAN_SKDs.FirstOrDefault(Function(x) x.KDPENDAFTARAN = Parameter)
        End Function
        Public Function GetDataPendaftaranalasan(ByVal Parameter As String, ByVal alasan As String) As S_PENDAFTARAN_SKD
            'If Not oConnection.GetConnection() Then
            '    GetDataPendaftaranalasan = Nothing
            '    Exit Function
            'End If
            'GetDataPendaftaranalasan = oConnection.db.S_PENDAFTARAN_SKDs.FirstOrDefault(Function(x) x.KDPENDAFTARAN = Parameter And x.ALASAN = alasan)

            Try
                If Not oConnection.GetConnection() Then
                    GetDataPendaftaranalasan = Nothing
                    Exit Function
                End If
                GetDataPendaftaranalasan = oConnection.db.S_PENDAFTARAN_SKDs.FirstOrDefault(Function(x) x.KDPENDAFTARAN = Parameter And x.ALASAN = alasan)
            Catch ex As Exception
                GetDataPendaftaranalasan = Nothing
            End Try
        End Function
        Public Function GetDataPendaftaranalasanCek(ByVal Parameter As String, ByVal alasan As String) As S_PENDAFTARAN_SKD
            Try
                If Not oConnection.GetConnection() Then
                    GetDataPendaftaranalasanCek = Nothing
                    Exit Function
                End If
                GetDataPendaftaranalasanCek = oConnection.db.S_PENDAFTARAN_SKDs.FirstOrDefault(Function(x) x.KDPENDAFTARAN = Parameter And x.ALASAN = alasan)
            Catch ex As Exception
                GetDataPendaftaranalasanCek = Nothing
            End Try
        End Function
        Public Function GetDataByKDANTRIANMANUAL(ByVal Parameter As Integer) As S_PENDAFTARAN_SKD
            If Not oConnection.GetConnection() Then
                GetDataByKDANTRIANMANUAL = Nothing
                Exit Function
            End If
            GetDataByKDANTRIANMANUAL = oConnection.db.S_PENDAFTARAN_SKDs.FirstOrDefault(Function(x) x.KDANTRIANMANUAL = Parameter)
        End Function
        Public Function GetDataByRegister(ByVal Parameter As String) As S_PENDAFTARAN_SKD
            If Not oConnection.GetConnection() Then
                GetDataByRegister = Nothing
                Exit Function
            End If
            GetDataByRegister = oConnection.db.S_PENDAFTARAN_SKDs.FirstOrDefault(Function(x) x.KDPENDAFTARAN = Parameter)
        End Function
        Public Function GetDataByRMList(ByVal sKDCUSTOMER As String) As List(Of S_PENDAFTARAN_SKD)
            Try
                If Not oConnection.GetConnection() Then
                    GetDataByRMList = Nothing
                    Exit Function
                End If
                GetDataByRMList = oConnection.db.S_PENDAFTARAN_SKDs.Where(Function(x) x.KDCUSTOMER = sKDCUSTOMER).ToList()
            Catch ex As Exception
                GetDataByRMList = Nothing
            End Try
        End Function
        Public Function GetDataByKdregList(ByVal KDREG As String) As List(Of S_PENDAFTARAN_SKD)
            Try
                If Not oConnection.GetConnection() Then
                    GetDataByKdregList = Nothing
                    Exit Function
                End If
                GetDataByKdregList = oConnection.db.S_PENDAFTARAN_SKDs.Where(Function(x) x.KDPENDAFTARAN = KDREG).OrderBy(Function(x) x.DATECREATED).ToList()
            Catch ex As Exception
                GetDataByKdregList = Nothing
            End Try
        End Function
        Public Function GetDataByRMDateKontrol(ByVal sKDCUSTOMER As String, ByVal sDATEKONTROL As DateTime) As S_PENDAFTARAN_SKD
            Try
                If Not oConnection.GetConnection() Then
                    GetDataByRMDateKontrol = Nothing
                    Exit Function
                End If
                GetDataByRMDateKontrol = oConnection.db.S_PENDAFTARAN_SKDs.FirstOrDefault(Function(x) x.KDCUSTOMER = sKDCUSTOMER And x.SEARCH = sDATEKONTROL.ToString("ddMMyyyy"))
            Catch ex As Exception
                GetDataByRMDateKontrol = Nothing
            End Try
        End Function
        Public Function GetDataByRMSKDDescending(ByVal sKDCUSTOMER As String) As S_PENDAFTARAN_SKD
            Try
                If Not oConnection.GetConnection() Then
                    GetDataByRMSKDDescending = Nothing
                    Exit Function
                End If
                GetDataByRMSKDDescending = oConnection.db.S_PENDAFTARAN_SKDs.OrderByDescending(Function(x) x.DATE).FirstOrDefault(Function(x) x.KDCUSTOMER = sKDCUSTOMER)
            Catch ex As Exception
                GetDataByRMSKDDescending = Nothing
            End Try
        End Function
        Public Function GetDataLastCustomer(ByVal sNOMORRUJUKAN As String, ByVal Tanggal As Date) As S_PENDAFTARAN_SKD
            If Not oConnection.GetConnection Then
                GetDataLastCustomer = Nothing
                Exit Function
            End If

            If Not oConnection.GetConnection Then
                GetDataLastCustomer = Nothing
                Exit Function
            End If

            Dim ds = (From x In oConnection.db.S_PENDAFTARAN_SKDs.Where(Function(x) x.NOMORRUJUKAN = sNOMORRUJUKAN And x.DATE.Year = Year(Tanggal) And x.DATE.Month = Month(Tanggal) And x.DATE.Day = Day(Tanggal))
                      Select x).ToList()

            GetDataLastCustomer = ds.OrderByDescending(Function(x) x.KDPENDAFTARAN).FirstOrDefault()

        End Function
        Public Function InsertData(ByVal entity As S_PENDAFTARAN_SKD, ByVal sKDSKD As String) As String
            Try
                If Not oConnection.GetConnection() Then
                    InsertData = ""
                    Exit Function
                End If

                sREFERENCE = entity.KDSKD
                sSTATUS = "INSERT"

                If entity.ALASAN = "KONTROL" Then
                    If entity.ISCATEGORY = 0 Then
                        sMODUL = "SKD-RJ"
                    Else
                        sMODUL = "SKD-RI"
                    End If
                ElseIf entity.ALASAN = "RUJUK" Then
                    sMODUL = "RUJUK"
                ElseIf entity.ALASAN = "RUJUKAN HABIS" Then
                    sMODUL = "RUJUKANHABIS"
                ElseIf entity.ALASAN = "RUJUK INTERNAL" Then
                    sMODUL = "RUJUKINTERNAL"
                ElseIf entity.ALASAN = "PRB" Then
                    sMODUL = "PRB"
                ElseIf entity.ALASAN = "RUJUK BALIK" Then
                    sMODUL = "RUJUKBALIK"
                ElseIf entity.ALASAN = "SELESAI PENGOBATAN" Then
                    sMODUL = "SELESAI"
                ElseIf entity.ALASAN = "RAWAT INAP" Then
                    sMODUL = "RAWATINAP"
                ElseIf entity.ALASAN = "ITERASI1" Then
                    sMODUL = "ITERASI1"
                ElseIf entity.ALASAN = "ITERASI2" Then
                    sMODUL = "ITERASI2"
                Else
                    sMODUL = "SKD"
                End If

                If sKDSKD = "" Then
                    Try
                        sLASTNUMBER = oCounter.GetLastNumber(sMODUL, entity.DATE)

                        If sLASTNUMBER = 0 Then
                            Try
                                oCounter.InsertData(sMODUL, entity.DATE)
                                sLASTNUMBER = oCounter.GetLastNumber(sMODUL, entity.DATE)
                            Catch ex As Exception
                                sLASTNUMBER = 0
                            End Try
                        End If

                        entity.KDSKD = AutoNumber(sMODUL, sLASTNUMBER + 1, entity.DATE)
                    Catch ex As Exception
                        oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                        Throw ex
                    End Try

                    Try
                        oCounter.UpdateData(sMODUL, sLASTNUMBER + 1, Month(entity.DATE), Year(entity.DATE))
                    Catch ex As Exception
                        oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                        Throw ex
                    End Try

                    Try
                        oConnection.db.S_PENDAFTARAN_SKDs.InsertOnSubmit(entity)
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
                Else
                    Try
                        entity.KDSKD = sKDSKD

                        oConnection.db.S_PENDAFTARAN_SKDs.InsertOnSubmit(entity)
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
                End If

                InsertData = entity.KDSKD
            Catch ex As Exception
                InsertData = ""
                oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                Throw ex
            End Try
        End Function
        Public Function UpdateData(ByVal entity As S_PENDAFTARAN_SKD) As Boolean
            Try
                If Not oConnection.GetConnection() Then
                    UpdateData = False
                    Exit Function
                End If

                sREFERENCE = entity.KDSKD
                sSTATUS = "UPDATE"

                Dim ds = oConnection.db.S_PENDAFTARAN_SKDs.FirstOrDefault(Function(x) x.KDSKD = entity.KDSKD)

                Try
                    oConnection.db.S_PENDAFTARAN_SKDs.DeleteOnSubmit(ds)
                    oConnection.db.S_PENDAFTARAN_SKDs.InsertOnSubmit(entity)
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

                Dim ds = oConnection.db.S_PENDAFTARAN_SKDs.FirstOrDefault(Function(x) x.KDSKD.Contains(Parameter))

                If ds IsNot Nothing Then
                    Try
                        oConnection.db.S_PENDAFTARAN_SKDs.DeleteOnSubmit(ds)
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
        Public Function DeleteDataAllSKDIGD(ByVal sConn As String, ByVal Parameter As String) As Boolean
            Try
                DeleteDataAllSKDIGD = True

                Dim oConn As New SqlConnection
                Dim oComm As New SqlCommand
                Dim da As SqlDataAdapter
                Dim ds As New DataSet
                Dim SQL As String
                oConn = New SqlConnection(sConn)

                If oConn.State = ConnectionState.Closed Then
                    oConn.Open()
                End If

                SQL = "DELETE FROM S_PENDAFTARAN_SKD WHERE KDPENDAFTARAN = '" & Parameter & "' "

                oComm.Connection = oConn
                oComm.CommandText = SQL
                oComm.CommandTimeout = 120
                oComm.CommandType = CommandType.Text

                da = New SqlDataAdapter(oComm)
                da.Fill(ds, "S_PENDAFTARAN_SKD")

                If oConn.State = ConnectionState.Open Then
                    oConn.Close()
                End If

            Catch oErr As Exception
                DeleteDataAllSKDIGD = False
                MsgBox("Print Data : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation)
            End Try
        End Function
    End Class
End Namespace