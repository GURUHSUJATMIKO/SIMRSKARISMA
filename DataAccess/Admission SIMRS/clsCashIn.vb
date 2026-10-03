Imports System.Threading

Namespace Finance
    Public Class clsCashIn
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
            oCounter = New Setting.clsCounter
            sMODUL = "CI"
        End Sub
        Protected Overrides Sub Finalize()
            MyBase.Finalize()
            oConnection = Nothing
            GC.SuppressFinalize(Me)
        End Sub
        Public Function GetStructureHeader() As F_CASHIN_H
            If Not oConnection.GetConnection() Then
                GetStructureHeader = Nothing
            End If
            GetStructureHeader = New F_CASHIN_H
        End Function
        Public Function GetStructureDetail() As F_CASHIN_D
            If Not oConnection.GetConnection() Then
                GetStructureDetail = Nothing
            End If
            GetStructureDetail = New F_CASHIN_D
        End Function
        Public Function GetStructureDetailList() As List(Of F_CASHIN_D)
            If Not oConnection.GetConnection() Then
                GetStructureDetailList = Nothing
            End If
            GetStructureDetailList = New List(Of F_CASHIN_D)
        End Function
        Public Function GetData() As List(Of F_CASHIN_H)
            If Not oConnection.GetConnection() Then
                GetData = Nothing
                Exit Function
            End If
            GetData = oConnection.db.F_CASHIN_Hs.OrderByDescending(Function(x) x.KDCASHIN).ToList()
        End Function
        Public Function GetData(ByVal Parameter As String) As F_CASHIN_H
            If Not oConnection.GetConnection() Then
                GetData = Nothing
                Exit Function
            End If
            GetData = oConnection.db.F_CASHIN_Hs.FirstOrDefault(Function(x) x.KDCASHIN = Parameter)
        End Function
        Public Function GetDataDetail() As List(Of F_CASHIN_D)
            If Not oConnection.GetConnection() Then
                GetDataDetail = Nothing
                Exit Function
            End If
            GetDataDetail = oConnection.db.F_CASHIN_Ds.ToList()
        End Function
        Public Function GetDataDetail(ByVal Parameter As String) As List(Of F_CASHIN_D)
            If Not oConnection.GetConnection() Then
                GetDataDetail = Nothing
                Exit Function
            End If
            GetDataDetail = oConnection.db.F_CASHIN_Ds.Where(Function(x) x.KDCASHIN = Parameter).ToList()
        End Function
        Public Function InsertData(ByVal entity As F_CASHIN_H, ByVal entityDetail As List(Of F_CASHIN_D)) As String
            Try
                If Not oConnection.GetConnection() Then
                    InsertData = ""
                    Exit Function
                End If

                sREFERENCE = entity.KDCASHIN
                sSTATUS = "INSERT"

                sMODUL = entity.MODUL

                Try
                    sLASTNUMBER = oCounter.GetLastNumberTahun(sMODUL, entity.DATE)

                    If sLASTNUMBER = 0 Then
                        Try
                            oCounter.InsertData(sMODUL, entity.DATE)
                            sLASTNUMBER = oCounter.GetLastNumberTahun(sMODUL, entity.DATE)
                        Catch ex As Exception
                            sLASTNUMBER = 0
                        End Try
                    End If

                    entity.KDCASHIN = Year(entity.DATE) & Month(entity.DATE) & Day(entity.DATE) & sMODUL & (sLASTNUMBER + 1).ToString.PadLeft(6, "0")

                    For Each iLoop In entityDetail
                        iLoop.KDCASHIN = entity.KDCASHIN
                    Next

                Catch ex As Exception
                    oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                    Throw ex
                End Try

                Try
                    oConnection.db.F_CASHIN_Hs.InsertOnSubmit(entity)
                    oConnection.db.F_CASHIN_Ds.InsertAllOnSubmit(entityDetail)
                Catch ex As Exception
                    oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                    Throw ex
                End Try
                Try
                    For Each iLoop In entityDetail
                        Dim sINVOICE = iLoop.NOINVOICE

                        Dim dsInvoice = oConnection.db.S_BILLING_Hs.FirstOrDefault(Function(x) x.KDBILLING = sINVOICE)

                        If dsInvoice IsNot Nothing Then
                            dsInvoice.PAYAMOUNT += iLoop.AMOUNTPAYMENT
                        End If

                        Dim dsInvoiceTanpaResep = oConnection.db.S_BILLING_TANPARESEP_Hs.FirstOrDefault(Function(x) x.KDBILLING_TANPARESEP = sINVOICE)

                        If dsInvoiceTanpaResep IsNot Nothing Then
                            dsInvoiceTanpaResep.PAYAMOUNT += iLoop.AMOUNTPAYMENT
                        End If

                    Next
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
                Try
                    oCounter.UpdateData(sMODUL, sLASTNUMBER + 1)
                Catch ex As Exception
                    oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                    Throw ex
                End Try

                UpdateStatusDaftar(entity.KDPENDAFTARAN, 2)

                InsertData = entity.KDCASHIN
            Catch ex As Exception
                InsertData = ""
                oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                Throw ex
            End Try
        End Function
        Public Function UpdateData(ByVal entity As F_CASHIN_H, ByVal entityDetail As List(Of F_CASHIN_D)) As Boolean
            Dim sMODUL As String = String.Empty

            Try
                If Not oConnection.GetConnection() Then
                    UpdateData = False
                    Exit Function
                End If

                Dim sKDPENDATARANDS As String = String.Empty

                sREFERENCE = entity.KDCASHIN
                sSTATUS = "UPDATE"

                Dim ds = oConnection.db.F_CASHIN_Hs.FirstOrDefault(Function(x) x.KDCASHIN = entity.KDCASHIN)

                Try
                    sKDPENDATARANDS = ds.KDPENDAFTARAN
                    sMODUL = ds.MODUL
                    oConnection.db.F_CASHIN_Hs.DeleteOnSubmit(ds)
                    oConnection.db.F_CASHIN_Hs.InsertOnSubmit(entity)
                Catch ex As Exception
                    oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                    Throw ex
                End Try

                Dim dsDetail = oConnection.db.F_CASHIN_Ds.Where(Function(x) x.KDCASHIN = entity.KDCASHIN And x.SEQ < 100)

                Try
                    For Each iLoop In dsDetail
                        Dim sINVOICE = iLoop.NOINVOICE

                        Dim dsInvoice = oConnection.db.S_BILLING_Hs.FirstOrDefault(Function(x) x.KDBILLING = sINVOICE)

                        If dsInvoice IsNot Nothing Then
                            dsInvoice.PAYAMOUNT -= iLoop.AMOUNTPAYMENT
                        End If

                        Dim dsInvoiceTanpaResep = oConnection.db.S_BILLING_TANPARESEP_Hs.FirstOrDefault(Function(x) x.KDBILLING_TANPARESEP = sINVOICE)

                        If dsInvoiceTanpaResep IsNot Nothing Then
                            dsInvoiceTanpaResep.PAYAMOUNT -= iLoop.AMOUNTPAYMENT
                        End If

                    Next
                Catch ex As Exception
                    oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                    Throw ex
                End Try

                Try
                    oConnection.db.F_CASHIN_Ds.DeleteAllOnSubmit(dsDetail)
                    oConnection.db.F_CASHIN_Ds.InsertAllOnSubmit(entityDetail)
                Catch ex As Exception
                    oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                    Throw ex
                End Try

                Try
                    For Each iLoop In entityDetail
                        Dim sINVOICE = iLoop.NOINVOICE

                        Dim dsInvoice = oConnection.db.S_BILLING_Hs.FirstOrDefault(Function(x) x.KDBILLING = sINVOICE)

                        If dsInvoice IsNot Nothing Then
                            dsInvoice.PAYAMOUNT += iLoop.AMOUNTPAYMENT
                        End If

                        Dim dsInvoiceTanpaResep = oConnection.db.S_BILLING_TANPARESEP_Hs.FirstOrDefault(Function(x) x.KDBILLING_TANPARESEP = sINVOICE)

                        If dsInvoiceTanpaResep IsNot Nothing Then
                            dsInvoiceTanpaResep.PAYAMOUNT += iLoop.AMOUNTPAYMENT
                        End If

                    Next
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

                UpdateStatusDaftar(sKDPENDATARANDS, 0)
                UpdateStatusDaftar(entity.KDPENDAFTARAN, 2)

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

                Dim sKDPENDATARANDS As String = String.Empty
                Dim ds = oConnection.db.F_CASHIN_Hs.Where(Function(x) x.KDCASHIN.Contains(Parameter))

                sMODUL = ds.FirstOrDefault.MODUL
                sKDPENDATARANDS = ds.FirstOrDefault.KDPENDAFTARAN

                For Each xLoop In ds
                    Dim sNOCASH = xLoop.KDCASHIN

                    Dim dsDetail = oConnection.db.F_CASHIN_Ds.Where(Function(x) x.KDCASHIN = sNOCASH And x.SEQ < 100)

                    Try
                        For Each iLoop In dsDetail
                            Dim sINVOICE = iLoop.NOINVOICE

                            Dim dsInvoice = oConnection.db.S_BILLING_Hs.FirstOrDefault(Function(x) x.KDBILLING = sINVOICE)

                            If dsInvoice IsNot Nothing Then
                                dsInvoice.PAYAMOUNT -= iLoop.AMOUNTPAYMENT
                            End If

                            Dim dsInvoiceTanpaResep = oConnection.db.S_BILLING_TANPARESEP_Hs.FirstOrDefault(Function(x) x.KDBILLING_TANPARESEP = sINVOICE)

                            If dsInvoiceTanpaResep IsNot Nothing Then
                                dsInvoiceTanpaResep.PAYAMOUNT -= iLoop.AMOUNTPAYMENT
                            End If
                        Next
                    Catch ex As Exception
                        oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                        Throw ex
                    End Try

                    Try
                        oConnection.db.F_CASHIN_Hs.DeleteOnSubmit(xLoop)
                        oConnection.db.F_CASHIN_Ds.DeleteAllOnSubmit(dsDetail)
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
                Next

                UpdateStatusDaftar(sKDPENDATARANDS, 0)

                DeleteData = True
            Catch ex As Exception
                DeleteData = False
                oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                Throw ex
            End Try
        End Function
        Public Function TypePembayaran_Default() As String
            Try
                If Not oConnection.GetConnection() Then
                    TypePembayaran_Default = String.Empty
                    Exit Function
                End If

                Dim ds = oConnection.db.M_PAYMENTTYPEs.FirstOrDefault(Function(x) x.ISDEFAULT = True)
                If ds IsNot Nothing Then
                    TypePembayaran_Default = ds.KDPAYMENTTYPE
                Else
                    TypePembayaran_Default = String.Empty
                End If
            Catch ex As Exception
                TypePembayaran_Default = String.Empty
                Throw ex
            End Try
        End Function
        Public Function UnitKasir_Default() As String
            Try
                If Not oConnection.GetConnection() Then
                    UnitKasir_Default = String.Empty
                    Exit Function
                End If

                Dim ds = oConnection.db.M_UNITKASIRs.FirstOrDefault(Function(x) x.ISDEFAULT = True)
                If ds IsNot Nothing Then
                    UnitKasir_Default = ds.KDUNITKASIR
                Else
                    UnitKasir_Default = String.Empty
                End If
            Catch ex As Exception
                UnitKasir_Default = String.Empty
                Throw ex
            End Try
        End Function
        Public Function UpdateStatusDaftar(ByVal sKDPENDAFTARAN As String, ByVal sSTATUSDAFTAR As Integer) As Boolean
            Try
                If Not oConnection.GetConnection Then
                    UpdateStatusDaftar = False
                    Exit Function
                End If

                UpdateStatusDaftar = True

                Dim ds = oConnection.db.S_PENDAFTARAN_Hs.FirstOrDefault(Function(x) x.KDPENDAFTARAN = sKDPENDAFTARAN)

                ds.STATUSDAFTAR = sSTATUSDAFTAR

                oConnection.db.SubmitChanges()

            Catch ex As Exception
                UpdateStatusDaftar = False
                Throw ex
            End Try
        End Function
    End Class
End Namespace