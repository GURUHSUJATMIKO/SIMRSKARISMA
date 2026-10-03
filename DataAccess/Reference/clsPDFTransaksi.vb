Imports DataAccess

Namespace Sales
    Public Class clsPDFTransaksi
        Public oConnection As Setting.clsConnectionAdmision = Nothing
        Public oError As Setting.clsError = Nothing
        Public oCounter As Setting.clsCounter = Nothing
        Public sLASTNUMBER As Integer = 0
        Public sMODUL As String = "PDFT"
        Public sREFERENCE As String = ""
        Public sSTATUS As String = ""

        Public Sub New()
            oConnection = New Setting.clsConnectionAdmision
            oError = New Setting.clsError
            oCounter = New Setting.clsCounter
        End Sub
        Protected Overrides Sub Finalize()
            MyBase.Finalize()
            oConnection = Nothing
            GC.SuppressFinalize(Me)
        End Sub
        Public Function GetStructureHeader() As S_PDF_H
            If Not oConnection.GetConnection Then
                GetStructureHeader = Nothing
            End If
            GetStructureHeader = New S_PDF_H
        End Function
        Public Function GetStructureDetail() As S_PDF_D
            If Not oConnection.GetConnection Then
                GetStructureDetail = Nothing
            End If
            GetStructureDetail = New S_PDF_D
        End Function
        Public Function GetStructureDetailList() As List(Of S_PDF_D)
            If Not oConnection.GetConnection Then
                GetStructureDetailList = Nothing
            End If
            GetStructureDetailList = New List(Of S_PDF_D)
        End Function
        Public Function GetData() As List(Of S_PDF_H)
            If Not oConnection.GetConnection Then
                GetData = Nothing
                Exit Function
            End If
            GetData = oConnection.db.S_PDF_Hs.OrderByDescending(Function(x) x.KDPDFTRANSAKSI).ToList()
        End Function
        Public Function GetDataByRegister(ByVal sKDREG As String) As List(Of S_PDF_H)
            If Not oConnection.GetConnection Then
                GetDataByRegister = Nothing
                Exit Function
            End If
            GetDataByRegister = oConnection.db.S_PDF_Hs.Where(Function(x) x.KDREG = sKDREG).OrderByDescending(Function(x) x.KDPDFTRANSAKSI).ToList()
        End Function
        Public Function GetData(ByVal Parameter As String) As S_PDF_H
            If Not oConnection.GetConnection Then
                GetData = Nothing
                Exit Function
            End If
            GetData = oConnection.db.S_PDF_Hs.FirstOrDefault(Function(x) x.KDPDFTRANSAKSI = Parameter)
        End Function
        Public Function GetDataDetail() As List(Of S_PDF_D)
            If Not oConnection.GetConnection Then
                GetDataDetail = Nothing
                Exit Function
            End If
            GetDataDetail = oConnection.db.S_PDF_Ds.ToList()
        End Function
        Public Function GetDataDetail(ByVal Parameter As String) As List(Of S_PDF_D)
            If Not oConnection.GetConnection Then
                GetDataDetail = Nothing
                Exit Function
            End If
            GetDataDetail = oConnection.db.S_PDF_Ds.Where(Function(x) x.KDPDFTRANSAKSI = Parameter).ToList()
        End Function
        Public Function GetDataRegister(ByVal Parameter As String) As List(Of S_PDF_H)
            If Not oConnection.GetConnection Then
                GetDataRegister = Nothing
                Exit Function
            End If
            GetDataRegister = oConnection.db.S_PDF_Hs.Where(Function(x) x.KDREG = Parameter).ToList()
        End Function
        Public Function GetDataAlamatSimpan() As SET_MODUL
            If Not oConnection.GetConnection Then
                GetDataAlamatSimpan = Nothing
                Exit Function
            End If
            GetDataAlamatSimpan = oConnection.db.SET_MODULs.FirstOrDefault(Function(x) x.NOIDMODUL = "UPLOADDATAFILE")
        End Function
        Public Function GetDataPDF() As List(Of M_PDF)
            If Not oConnection.GetConnection Then
                GetDataPDF = Nothing
                Exit Function
            End If
            GetDataPDF = oConnection.db.M_PDFs().ToList()
        End Function
        Public Function GetDataPDF(ByVal Parameter As String) As M_PDF
            If Not oConnection.GetConnection Then
                GetDataPDF = Nothing
                Exit Function
            End If
            GetDataPDF = oConnection.db.M_PDFs.FirstOrDefault(Function(x) x.KDPDF = Parameter)
        End Function
        Public Function InsertData(ByVal entity As S_PDF_H, ByVal entityDetail As List(Of S_PDF_D), ByVal KDCUSTOMER As String) As Boolean
            Try
                If Not oConnection.GetConnection Then
                    InsertData = False
                    Exit Function
                End If

                sREFERENCE = entity.KDPDFTRANSAKSI
                sSTATUS = "INSERT"

                'Generate Auto Number
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

                    entity.KDPDFTRANSAKSI = AutoNumber(sMODUL, sLASTNUMBER + 1, entity.DATE)
                    For Each iLoop In entityDetail
                        iLoop.KDPDFTRANSAKSI = entity.KDPDFTRANSAKSI
                    Next
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
                'End Generate

                Try
                    oConnection.db.S_PDF_Hs.InsertOnSubmit(entity)
                    oConnection.db.S_PDF_Ds.InsertAllOnSubmit(entityDetail)
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

                Try
                    For Each iLoop In entityDetail
                        UpdateAlamatSimpan(iLoop.KDPDFTRANSAKSI, iLoop.SEQ, fn_CopyPasteFileAdd(iLoop.KDPDFTRANSAKSI, iLoop.SEQ, iLoop.S_PDF_H.DATE, iLoop.ALAMATAWAL_PDF, iLoop.ALAMATAKHIR_PDF, iLoop.TYPE, KDCUSTOMER))
                    Next
                Catch ex As Exception
                    oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                    Throw ex
                End Try
            Catch ex As Exception
                InsertData = False
                oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                Throw ex
            End Try
        End Function
        Public Function UpdateData(ByVal entity As S_PDF_H, ByVal entityDetail As List(Of S_PDF_D), ByVal KDCUSTOMER As String) As Boolean
            Try
                If Not oConnection.GetConnection Then
                    UpdateData = False
                    Exit Function
                End If

                sREFERENCE = entity.KDPDFTRANSAKSI
                sSTATUS = "UPDATEDATA"

                Dim ds = oConnection.db.S_PDF_Hs.FirstOrDefault(Function(x) x.KDPDFTRANSAKSI = entity.KDPDFTRANSAKSI)

                Try
                    oConnection.db.S_PDF_Hs.DeleteOnSubmit(ds)
                    oConnection.db.S_PDF_Hs.InsertOnSubmit(entity)
                Catch ex As Exception
                    oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                    Throw ex
                End Try

                Dim dsDetail = oConnection.db.S_PDF_Ds.Where(Function(x) x.KDPDFTRANSAKSI = entity.KDPDFTRANSAKSI)
                Dim list As New List(Of String)

                For Each xloop In dsDetail
                    If xloop.ALAMATAKHIR_PDF <> "" Then
                        list.Add(xloop.ALAMATAKHIR_PDF)
                    End If
                Next

                Try
                    oConnection.db.S_PDF_Ds.DeleteAllOnSubmit(dsDetail)
                    oConnection.db.S_PDF_Ds.InsertAllOnSubmit(entityDetail)
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

                Try
                    For Each iloop In list
                        If iloop <> "" Then
                            If System.IO.File.Exists(iloop) = True Then
                                My.Computer.FileSystem.DeleteFile(iloop, Microsoft.VisualBasic.FileIO.RecycleOption.DeletePermanently, Microsoft.VisualBasic.FileIO.UICancelOption.DoNothing)
                            End If
                        End If
                    Next
                    For Each iLoop In entityDetail
                        UpdateAlamatSimpan(iLoop.KDPDFTRANSAKSI, iLoop.SEQ, fn_CopyPasteFileAdd(iLoop.KDPDFTRANSAKSI, iLoop.SEQ, iLoop.S_PDF_H.DATE, iLoop.ALAMATAWAL_PDF, iLoop.ALAMATAKHIR_PDF, iLoop.TYPE, KDCUSTOMER))
                    Next
                Catch ex As Exception
                    oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                    Throw ex
                End Try
            Catch ex As Exception
                UpdateData = False
                oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                Throw ex
            End Try
        End Function
        Public Function DeleteData(ByVal Parameter As String) As Boolean
            Try
                If Not oConnection.GetConnection Then
                    DeleteData = False
                    Exit Function
                End If

                sREFERENCE = Parameter
                sSTATUS = "DELETEDATA"

                Dim list As New List(Of String)

                Dim ds = oConnection.db.S_PDF_Hs.FirstOrDefault(Function(x) x.KDPDFTRANSAKSI = Parameter)
                Dim dsDetail = oConnection.db.S_PDF_Ds.Where(Function(x) x.KDPDFTRANSAKSI = Parameter)

                For Each xloop In dsDetail
                    If xloop.ALAMATAKHIR_PDF <> "" Then
                        list.Add(xloop.ALAMATAKHIR_PDF)
                    End If
                Next
                Try
                    oConnection.db.S_PDF_Hs.DeleteOnSubmit(ds)
                Catch ex As Exception
                    oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                    Throw ex
                End Try
                Try
                    oConnection.db.S_PDF_Ds.DeleteAllOnSubmit(dsDetail)
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

                Try
                    For Each xloop In list
                        If System.IO.File.Exists(xloop) = True Then
                            My.Computer.FileSystem.DeleteFile(xloop, Microsoft.VisualBasic.FileIO.RecycleOption.DeletePermanently, Microsoft.VisualBasic.FileIO.UICancelOption.DoNothing)
                        End If
                    Next
                Catch ex As Exception

                End Try
            Catch ex As Exception
                DeleteData = False
                oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                Throw ex
            End Try
        End Function
        Private Function fn_CopyPasteFileAdd(ByVal Kode As String, ByVal SEQ As Integer, ByVal TANGGAL As DateTime, ByVal AlamatCopy As String, ByVal AlamatSimpanEdit As String, ByVal Type As String, ByVal kdcustomer As String) As String
            Try
                Dim dsData = GetDataAlamatSimpan()

                If dsData IsNot Nothing Then
                    Dim AlamatPaste As String = dsData.DESCRIPTION & "\" & TANGGAL.ToString("yyyy") & "\" & TANGGAL.ToString("MM") & "\" & TANGGAL.ToString("dd") & "\" & kdcustomer & "\"
                    If System.IO.File.Exists(AlamatCopy) = True Then
                        If Not IO.Directory.Exists(AlamatPaste) Then
                            IO.Directory.CreateDirectory(AlamatPaste)
                        End If
                        System.IO.File.Copy(AlamatCopy, AlamatPaste & Kode & SEQ & Type)
                        fn_CopyPasteFileAdd = AlamatPaste & Kode & SEQ & Type
                    Else
                        fn_CopyPasteFileAdd = "Data Copy Tidak Ada " & AlamatCopy
                    End If
                Else
                    fn_CopyPasteFileAdd = "Alamat Paste Tidak Ada"
                End If
            Catch ex As Exception
                fn_CopyPasteFileAdd = ex.ToString
            End Try
        End Function
        Public Function UpdateAlamatSimpan(ByVal sKDPDFTRANSAKSI As String, ByVal sSEQ As Integer, ByVal sALAMATAKHIR_PDF As String) As Boolean
            Try
                If Not oConnection.GetConnection Then
                    UpdateAlamatSimpan = False
                    Exit Function
                End If

                sREFERENCE = sKDPDFTRANSAKSI
                sSTATUS = "UPDATEDATA"

                Try
                    Dim ds = oConnection.db.S_PDF_Ds.FirstOrDefault(Function(x) x.KDPDFTRANSAKSI = sKDPDFTRANSAKSI And x.SEQ = sSEQ)
                    ds.ALAMATAKHIR_PDF = sALAMATAKHIR_PDF
                    oConnection.db.SubmitChanges()
                Catch ex As Exception
                    oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                    Throw ex
                End Try

                UpdateAlamatSimpan = True
            Catch ex As Exception
                UpdateAlamatSimpan = False
                oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                Throw ex
            End Try
        End Function
    End Class
End Namespace