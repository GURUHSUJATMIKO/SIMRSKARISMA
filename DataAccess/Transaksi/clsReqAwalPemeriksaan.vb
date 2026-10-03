Imports System.Threading

Namespace Transaksi
    Public Class clsReqAwalPemeriksaan
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
            sMODUL = "PEMAWAL"

            oCounter = New Setting.clsCounter
        End Sub
        Protected Overrides Sub Finalize()
            MyBase.Finalize()
            oConnection = Nothing
            GC.SuppressFinalize(Me)
        End Sub
        Public Function GetStructureHeader() As S_REQ_AWALPEMERIKSAAN
            If Not oConnection.GetConnection() Then
                GetStructureHeader = Nothing
            End If
            GetStructureHeader = New S_REQ_AWALPEMERIKSAAN
        End Function
        Public Function GetStructureHeaderTambahan() As S_REQ_AWALPEMERIKSAAN_TAMBAHAN
            If Not oConnection.GetConnection() Then
                GetStructureHeaderTambahan = Nothing
            End If
            GetStructureHeaderTambahan = New S_REQ_AWALPEMERIKSAAN_TAMBAHAN
        End Function
        Public Function GetStructureDetail() As S_REQ_AWALPEMERIKSAAN_D
            If Not oConnection.GetConnection Then
                GetStructureDetail = Nothing
            End If
            GetStructureDetail = New S_REQ_AWALPEMERIKSAAN_D
        End Function
        Public Function GetStructureDetailList() As List(Of S_REQ_AWALPEMERIKSAAN_D)
            If Not oConnection.GetConnection Then
                GetStructureDetailList = Nothing
            End If
            GetStructureDetailList = New List(Of S_REQ_AWALPEMERIKSAAN_D)
        End Function
        Public Function GetData() As List(Of S_REQ_AWALPEMERIKSAAN)
            If Not oConnection.GetConnection() Then
                GetData = Nothing
                Exit Function
            End If
            GetData = oConnection.db.S_REQ_AWALPEMERIKSAANs.OrderByDescending(Function(x) x.KDAWALPEMERIKSAAN).ToList()
        End Function
        Public Function GetData(ByVal Parameter As String) As S_REQ_AWALPEMERIKSAAN
            If Not oConnection.GetConnection() Then
                GetData = Nothing
                Exit Function
            End If
            GetData = oConnection.db.S_REQ_AWALPEMERIKSAANs.FirstOrDefault(Function(x) x.KDAWALPEMERIKSAAN = Parameter)
        End Function
        Public Function GetDataTambahan(ByVal Parameter As String) As S_REQ_AWALPEMERIKSAAN_TAMBAHAN
            If Not oConnection.GetConnection() Then
                GetDataTambahan = Nothing
                Exit Function
            End If
            GetDataTambahan = oConnection.db.S_REQ_AWALPEMERIKSAAN_TAMBAHANs.FirstOrDefault(Function(x) x.KDAWALPEMERIKSAAN = Parameter)
        End Function
        Public Function GetDataByKdreg(ByVal Parameter As String) As S_REQ_AWALPEMERIKSAAN
            If Not oConnection.GetConnection() Then
                GetDataByKdreg = Nothing
                Exit Function
            End If
            GetDataByKdreg = oConnection.db.S_REQ_AWALPEMERIKSAANs.FirstOrDefault(Function(x) x.KDPENDAFTARAN = Parameter)
        End Function
        Public Function GetDataLast(ByVal Parameter As String) As S_REQ_AWALPEMERIKSAAN
            If Not oConnection.GetConnection() Then
                GetDataLast = Nothing
                Exit Function
            End If

            GetDataLast = oConnection.db.S_REQ_AWALPEMERIKSAANs.Where(Function(x) x.KDCUSTOMER = Parameter).OrderByDescending(Function(x) x.KDPENDAFTARAN).FirstOrDefault

        End Function
        Public Function GetDataAlamatSimpan() As SET_SERVER_PANGGIL
            If Not oConnection.GetConnection() Then
                GetDataAlamatSimpan = Nothing
                Exit Function
            End If
            GetDataAlamatSimpan = oConnection.db.SET_SERVER_PANGGILs.FirstOrDefault(Function(x) x.KDSERVER = "SIMPANGAMBAR")
        End Function
        Public Function GetDataDetail(ByVal Parameter As String) As List(Of S_REQ_AWALPEMERIKSAAN_D)
            If Not oConnection.GetConnection Then
                GetDataDetail = Nothing
                Exit Function
            End If
            GetDataDetail = oConnection.db.S_REQ_AWALPEMERIKSAAN_Ds.Where(Function(x) x.KDAWALPEMERIKSAAN = Parameter).ToList()
        End Function
        Public Function InsertData(ByVal entity As S_REQ_AWALPEMERIKSAAN, entityDetail As List(Of S_REQ_AWALPEMERIKSAAN_D), ByVal entityTambahan As S_REQ_AWALPEMERIKSAAN_TAMBAHAN) As Boolean
            Try
                If Not oConnection.GetConnection() Then
                    InsertData = False
                    Exit Function
                End If

                sREFERENCE = entity.KDAWALPEMERIKSAAN
                sSTATUS = "INSERT"

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

                '    entity.KDAWALPEMERIKSAAN = AutoNumber(sMODUL, sLASTNUMBER + 1, entity.DATE)

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

                Try
                    oConnection.db.S_REQ_AWALPEMERIKSAANs.InsertOnSubmit(entity)
                    oConnection.db.S_REQ_AWALPEMERIKSAAN_TAMBAHANs.InsertOnSubmit(entityTambahan)
                    If entity IsNot Nothing Then
                        oConnection.db.S_REQ_AWALPEMERIKSAAN_Ds.InsertAllOnSubmit(entityDetail)
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
        Public Function UpdateData(ByVal entity As S_REQ_AWALPEMERIKSAAN, entityDetail As List(Of S_REQ_AWALPEMERIKSAAN_D), ByVal entityTambahan As S_REQ_AWALPEMERIKSAAN_TAMBAHAN) As Boolean
            Try
                If Not oConnection.GetConnection() Then
                    UpdateData = False
                    Exit Function
                End If

                sREFERENCE = entity.KDAWALPEMERIKSAAN
                sSTATUS = "UPDATE"

                Dim ds = oConnection.db.S_REQ_AWALPEMERIKSAANs.FirstOrDefault(Function(x) x.KDAWALPEMERIKSAAN = entity.KDAWALPEMERIKSAAN)
                Dim dsDetail = oConnection.db.S_REQ_AWALPEMERIKSAAN_Ds.Where(Function(x) x.KDAWALPEMERIKSAAN = entity.KDAWALPEMERIKSAAN)
                Dim dsDetailTambahan = oConnection.db.S_REQ_AWALPEMERIKSAAN_TAMBAHANs.FirstOrDefault(Function(x) x.KDAWALPEMERIKSAAN = entity.KDAWALPEMERIKSAAN)

                Try
                    oConnection.db.S_REQ_AWALPEMERIKSAANs.DeleteOnSubmit(ds)
                    oConnection.db.S_REQ_AWALPEMERIKSAANs.InsertOnSubmit(entity)

                    If dsDetail.Count > 0 Then
                        oConnection.db.S_REQ_AWALPEMERIKSAAN_Ds.DeleteAllOnSubmit(dsDetail)
                    End If

                    If entityDetail IsNot Nothing Then
                        oConnection.db.S_REQ_AWALPEMERIKSAAN_Ds.InsertAllOnSubmit(entityDetail)
                    End If

                    If dsDetailTambahan IsNot Nothing Then
                        oConnection.db.S_REQ_AWALPEMERIKSAAN_TAMBAHANs.DeleteOnSubmit(dsDetailTambahan)
                    End If

                    If entityTambahan IsNot Nothing Then
                        oConnection.db.S_REQ_AWALPEMERIKSAAN_TAMBAHANs.InsertOnSubmit(entityTambahan)
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

                Dim ds = oConnection.db.S_REQ_AWALPEMERIKSAANs.FirstOrDefault(Function(x) x.KDAWALPEMERIKSAAN = Parameter)
                Dim dsDetail = oConnection.db.S_REQ_AWALPEMERIKSAAN_Ds.Where(Function(x) x.KDAWALPEMERIKSAAN = Parameter)
                Dim dsDetailTambahan = oConnection.db.S_REQ_AWALPEMERIKSAAN_TAMBAHANs.FirstOrDefault(Function(x) x.KDAWALPEMERIKSAAN = Parameter)

                Try
                    oConnection.db.S_REQ_AWALPEMERIKSAANs.DeleteOnSubmit(ds)
                    oConnection.db.S_REQ_AWALPEMERIKSAAN_Ds.DeleteAllOnSubmit(dsDetail)
                    oConnection.db.S_REQ_AWALPEMERIKSAAN_TAMBAHANs.DeleteOnSubmit(dsDetailTambahan)
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