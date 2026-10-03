Imports System.Threading

Namespace Transaksi
    Public Class clsReqCPPT
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
            sMODUL = "CPPT"

            oCounter = New Setting.clsCounter
        End Sub
        Protected Overrides Sub Finalize()
            MyBase.Finalize()
            oConnection = Nothing
            GC.SuppressFinalize(Me)
        End Sub
        Public Function GetStructureHeader() As S_REQ_CPPT
            If Not oConnection.GetConnection() Then
                GetStructureHeader = Nothing
            End If
            GetStructureHeader = New S_REQ_CPPT
        End Function
        Public Function GetData() As List(Of S_REQ_CPPT)
            If Not oConnection.GetConnection() Then
                GetData = Nothing
                Exit Function
            End If
            GetData = oConnection.db.S_REQ_CPPTs.OrderByDescending(Function(x) x.KDCPPT).ToList()
        End Function
        Public Function GetDataByRMList(ByVal sKDCUSTOMER As String) As List(Of S_REQ_CPPT)
            If Not oConnection.GetConnection() Then
                GetDataByRMList = Nothing
                Exit Function
            End If
            GetDataByRMList = oConnection.db.S_REQ_CPPTs.Where(Function(x) x.KDCUSTOMER = sKDCUSTOMER And x.KDPENDAFTARAN.Contains("RJ") And x.PROFESI ="DOKTER").OrderByDescending(Function(x) x.KDCPPT).ToList()
        End Function
        Public Function GetDataByRMRIList(ByVal sKDCUSTOMER As String) As List(Of S_REQ_CPPT)
            If Not oConnection.GetConnection() Then
                GetDataByRMRIList = Nothing
                Exit Function
            End If
            GetDataByRMRIList = oConnection.db.S_REQ_CPPTs.Where(Function(x) x.KDCUSTOMER = sKDCUSTOMER And x.KDPENDAFTARAN.Contains("RI")).OrderByDescending(Function(x) x.KDCPPT).ToList()
        End Function
              Public Function GetDataByRMListSkin(ByVal sKDCUSTOMER As String) As List(Of S_REQ_CPPT)
            If Not oConnection.GetConnection() Then
                GetDataByRMListSkin = Nothing
                Exit Function
            End If
            GetDataByRMListSkin = oConnection.db.S_REQ_CPPTs.Where(Function(x) x.KDCUSTOMER = sKDCUSTOMER And x.KDPENDAFTARAN.Contains("RJ") And x.PROFESI ="DOKTER SKIN").OrderByDescending(Function(x) x.KDCPPT).ToList()
        End Function
        Public Function GetDataByRM(ByVal sKDCUSTOMER As String) As List(Of S_REQ_CPPT)
            If Not oConnection.GetConnection() Then
                GetDataByRM = Nothing
                Exit Function
            End If
            GetDataByRM = oConnection.db.S_REQ_CPPTs.Where(Function(x) x.KDCUSTOMER = sKDCUSTOMER).OrderByDescending(Function(x) x.KDCPPT).ToList()
        End Function
        Public Function GetDataByReg(ByVal sKDPENDAFTARAN As String) As List(Of S_REQ_CPPT)
            If Not oConnection.GetConnection() Then
                GetDataByReg = Nothing
                Exit Function
            End If
            GetDataByReg = oConnection.db.S_REQ_CPPTs.Where(Function(x) x.KDPENDAFTARAN = sKDPENDAFTARAN).OrderByDescending(Function(x) x.KDCPPT).ToList()
        End Function
        Public Function GetData(ByVal Parameter As String) As S_REQ_CPPT
            If Not oConnection.GetConnection() Then
                GetData = Nothing
                Exit Function
            End If
            GetData = oConnection.db.S_REQ_CPPTs.FirstOrDefault(Function(x) x.KDCPPT = Parameter)
        End Function
        Public Function GetDataByProfesidanRegister(ByVal sPROFESI As String, ByVal sKDPENDAFTARAN As String) As S_REQ_CPPT
            If Not oConnection.GetConnection() Then
                GetDataByProfesidanRegister = Nothing
                Exit Function
            End If
            GetDataByProfesidanRegister = oConnection.db.S_REQ_CPPTs.FirstOrDefault(Function(x) x.PROFESI = sPROFESI And x.KDPENDAFTARAN = sKDPENDAFTARAN)
        End Function
        Public Function InsertData(ByVal entity As S_REQ_CPPT, ByVal entityLainnya As S_REQ_CPPT_LAINNYA) As Boolean
            Try
                If Not oConnection.GetConnection() Then
                    InsertData = False
                    Exit Function
                End If

                sREFERENCE = entity.KDCPPT
                sSTATUS = "INSERT"

                If entity.PENJAMIN <> "AUTO" Then
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

                        entity.KDCPPT = AutoNumber(sMODUL, sLASTNUMBER + 1, entity.DATE)
                        If entityLainnya IsNot Nothing Then
                            entityLainnya.KDCPPT = entity.KDCPPT
                        End If

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
                End If

                Try
                    oConnection.db.S_REQ_CPPTs.InsertOnSubmit(entity)
                    If entityLainnya IsNot Nothing Then
                        oConnection.db.S_REQ_CPPT_LAINNYAs.InsertOnSubmit(entityLainnya)
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
            Catch ex As Exception
                InsertData = False
                oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                Throw ex
            End Try
        End Function
        Public Function UpdateData(ByVal entity As S_REQ_CPPT, ByVal entityLainnya As S_REQ_CPPT_LAINNYA) As Boolean
            Try
                If Not oConnection.GetConnection() Then
                    UpdateData = False
                    Exit Function
                End If

                sREFERENCE = entity.KDCPPT
                sSTATUS = "UPDATE"

                Dim ds = oConnection.db.S_REQ_CPPTs.FirstOrDefault(Function(x) x.KDCPPT = entity.KDCPPT)
                Dim dsLainnya = oConnection.db.S_REQ_CPPT_LAINNYAs.FirstOrDefault(Function(x) x.KDCPPT = entity.KDCPPT)

                Try
                    oConnection.db.S_REQ_CPPTs.DeleteOnSubmit(ds)
                    oConnection.db.S_REQ_CPPTs.InsertOnSubmit(entity)
                    If dsLainnya IsNot Nothing Then
                        oConnection.db.S_REQ_CPPT_LAINNYAs.DeleteOnSubmit(dsLainnya)
                    End If
                    If entityLainnya IsNot Nothing Then
                        oConnection.db.S_REQ_CPPT_LAINNYAs.InsertOnSubmit(entityLainnya)
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

                Dim ds = oConnection.db.S_REQ_CPPTs.FirstOrDefault(Function(x) x.KDCPPT = Parameter)
                Dim dsLainnya = oConnection.db.S_REQ_CPPT_LAINNYAs.FirstOrDefault(Function(x) x.KDCPPT = Parameter)

                Try
                    oConnection.db.S_REQ_CPPTs.DeleteOnSubmit(ds)
                    oConnection.db.S_REQ_CPPT_LAINNYAs.DeleteOnSubmit(dsLainnya)
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
    End Class
End Namespace