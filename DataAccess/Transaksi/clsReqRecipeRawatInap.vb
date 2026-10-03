Namespace Transaksi
    Public Class clsReqRecipeRawatInap
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

            sMODUL = "REQRI"
        End Sub
        Protected Overrides Sub Finalize()
            MyBase.Finalize()
            oConnection = Nothing
            GC.SuppressFinalize(Me)
        End Sub
        Public Function GetStructureHeader() As S_REQ_RECIPE_RI_H
            If Not oConnection.GetConnection() Then
                GetStructureHeader = Nothing
            End If
            GetStructureHeader = New S_REQ_RECIPE_RI_H
        End Function
        Public Function GetStructureDetail() As S_REQ_RECIPE_RI_D
            If Not oConnection.GetConnection() Then
                GetStructureDetail = Nothing
            End If
            GetStructureDetail = New S_REQ_RECIPE_RI_D
        End Function
        Public Function GetStructureDetailList() As List(Of S_REQ_RECIPE_RI_D)
            If Not oConnection.GetConnection() Then
                GetStructureDetailList = Nothing
            End If
            GetStructureDetailList = New List(Of S_REQ_RECIPE_RI_D)
        End Function
        Public Function GetData() As List(Of S_REQ_RECIPE_RI_H)
            If Not oConnection.GetConnection() Then
                GetData = Nothing
                Exit Function
            End If
            GetData = oConnection.db.S_REQ_RECIPE_RI_Hs.OrderByDescending(Function(x) x.KDREQRECIPE_RI).ToList()
        End Function
        Public Function GetData(ByVal KDREQRECIPE_RI As String) As S_REQ_RECIPE_RI_H
            If Not oConnection.GetConnection() Then
                GetData = Nothing
                Exit Function
            End If
            GetData = oConnection.db.S_REQ_RECIPE_RI_Hs.FirstOrDefault(Function(x) x.KDREQRECIPE_RI = KDREQRECIPE_RI)
        End Function
        Public Function GetDataObatBySeq(ByVal KDREQRECIPE_RI As String, ByVal sSEQ As Integer) As S_REQ_RECIPE_RI_D
            If Not oConnection.GetConnection() Then
                GetDataObatBySeq = Nothing
                Exit Function
            End If
            GetDataObatBySeq = oConnection.db.S_REQ_RECIPE_RI_Ds.FirstOrDefault(Function(x) x.KDREQRECIPE_RI = KDREQRECIPE_RI And x.SEQ = sSEQ)
        End Function
        Public Function GetDataDetailByKdpendaftaranH(ByVal sKDPENDAFTARAN As String) As List(Of S_REQ_RECIPE_RI_H)
            If Not oConnection.GetConnection() Then
                GetDataDetailByKdpendaftaranH = Nothing
                Exit Function
            End If
            GetDataDetailByKdpendaftaranH = oConnection.db.S_REQ_RECIPE_RI_Hs.Where(Function(x) x.KDPENDAFTARAN = sKDPENDAFTARAN).OrderBy(Function(x) x.DATE).ToList()
        End Function
        Public Function GetDataDetailByKdpendaftaran(ByVal sKDPENDAFTARAN As String) As List(Of S_REQ_RECIPE_RI_D)
            If Not oConnection.GetConnection() Then
                GetDataDetailByKdpendaftaran = Nothing
                Exit Function
            End If
            GetDataDetailByKdpendaftaran = oConnection.db.S_REQ_RECIPE_RI_Ds.Where(Function(x) x.S_REQ_RECIPE_RI_H.KDPENDAFTARAN = sKDPENDAFTARAN).OrderBy(Function(x) x.S_REQ_RECIPE_RI_H.DATE).ToList()
        End Function
        Public Function GetDataDetail() As List(Of S_REQ_RECIPE_RI_D)
            If Not oConnection.GetConnection() Then
                GetDataDetail = Nothing
                Exit Function
            End If
            GetDataDetail = oConnection.db.S_REQ_RECIPE_RI_Ds.ToList()
        End Function
        Public Function GetDataDetail(ByVal KDREQRECIPE_RI As String) As List(Of S_REQ_RECIPE_RI_D)
            If Not oConnection.GetConnection() Then
                GetDataDetail = Nothing
                Exit Function
            End If
            GetDataDetail = oConnection.db.S_REQ_RECIPE_RI_Ds.Where(Function(x) x.KDREQRECIPE_RI = KDREQRECIPE_RI).ToList()
        End Function
        Public Function GetDataDetailByHariIni(ByVal kdpendaftran As String, ByVal tanggal As String) As List(Of S_REQ_RECIPE_RI_D)
            If Not oConnection.GetConnection() Then
                GetDataDetailByHariIni = Nothing
                Exit Function
            End If
            GetDataDetailByHariIni = oConnection.db.S_REQ_RECIPE_RI_Ds.Where(Function(x) x.S_REQ_RECIPE_RI_H.KDPENDAFTARAN = kdpendaftran And x.S_REQ_RECIPE_RI_H.DESCRIPTION = tanggal).ToList()
        End Function
        'Public Function GetDataDetailByKdpendaftarandankddokterBlmPulang(ByVal sKDPENDAFTARAN As String, ByVal sNOIDUSER As String) As S_REQ_RECIPE_RI_H
        '    If Not oConnection.GetConnection() Then
        '        GetDataDetailByKdpendaftarandankddokterBlmPulang = Nothing
        '        Exit Function
        '    End If
        '    GetDataDetailByKdpendaftarandankddokterBlmPulang = oConnection.db.S_REQ_RECIPE_RI_Hs.FirstOrDefault(Function(x) x.KDPENDAFTARAN = sKDPENDAFTARAN And x.ALAMAT = sNOIDUSER)
        'End Function
        'Public Function GetDataDetailByKdpendaftarandankddokterPulang(ByVal sKDPENDAFTARAN As String, ByVal sNOIDUSER As String) As S_REQ_RECIPE_RI_H
        '    If Not oConnection.GetConnection() Then
        '        GetDataDetailByKdpendaftarandankddokterPulang = Nothing
        '        Exit Function
        '    End If
        '    GetDataDetailByKdpendaftarandankddokterPulang = oConnection.db.S_REQ_RECIPE_RI_Hs.FirstOrDefault(Function(x) x.KDPENDAFTARAN = sKDPENDAFTARAN And x.ALAMAT = sNOIDUSER And x.ISPULANG = True)
        'End Function
        Public Function InsertData(ByVal entity As S_REQ_RECIPE_RI_H, ByVal entityDetail As List(Of S_REQ_RECIPE_RI_D)) As Boolean
            Try
                If Not oConnection.GetConnection() Then
                    InsertData = False
                    Exit Function
                End If

                sREFERENCE = entity.KDREQRECIPE_RI
                sSTATUS = "INSERT"

                Dim AUTO As Boolean = False

                If entity.KDREQRECIPE_RI = "" Then
                    Try
                        AUTO = True
                        sLASTNUMBER = oCounter.GetLastNumber(sMODUL, entity.DATE)
                        If sLASTNUMBER = 0 Then
                            Try
                                oCounter.InsertData(sMODUL, entity.DATE)
                                sLASTNUMBER = oCounter.GetLastNumber(sMODUL, entity.DATE)
                            Catch ex As Exception
                                sLASTNUMBER = 0
                            End Try
                        End If

                        entity.KDREQRECIPE_RI = AutoNumber(sMODUL, sLASTNUMBER + 1, entity.DATE)
                        For Each iLoop In entityDetail
                            iLoop.KDREQRECIPE_RI = entity.KDREQRECIPE_RI
                        Next
                    Catch ex As Exception
                        oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                        Throw ex
                    End Try
                End If


                Try
                    oConnection.db.S_REQ_RECIPE_RI_Hs.InsertOnSubmit(entity)
                    oConnection.db.S_REQ_RECIPE_RI_Ds.InsertAllOnSubmit(entityDetail)
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

                If AUTO = True Then
                    Try
                        oCounter.UpdateData(sMODUL, sLASTNUMBER + 1, Month(entity.DATE), Year(entity.DATE))
                    Catch ex As Exception
                        oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                        Throw ex
                    End Try
                End If

                InsertData = True
            Catch ex As Exception
                InsertData = False
                oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                Throw ex
            End Try
        End Function
        Public Function UpdateData(ByVal entity As S_REQ_RECIPE_RI_H, ByVal entityDetail As List(Of S_REQ_RECIPE_RI_D)) As Boolean
            Try
                If Not oConnection.GetConnection() Then
                    UpdateData = False
                    Exit Function
                End If

                sREFERENCE = entity.KDREQRECIPE_RI
                sSTATUS = "UPDATE"

                Dim ds = oConnection.db.S_REQ_RECIPE_RI_Hs.FirstOrDefault(Function(x) x.KDREQRECIPE_RI = entity.KDREQRECIPE_RI)

                Try
                    oConnection.db.S_REQ_RECIPE_RI_Hs.DeleteOnSubmit(ds)
                    oConnection.db.S_REQ_RECIPE_RI_Hs.InsertOnSubmit(entity)
                Catch ex As Exception
                    oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                    Throw ex
                End Try

                Dim dsDetail = oConnection.db.S_REQ_RECIPE_RI_Ds.Where(Function(x) x.KDREQRECIPE_RI = entity.KDREQRECIPE_RI)

                Try
                    oConnection.db.S_REQ_RECIPE_RI_Ds.DeleteAllOnSubmit(dsDetail)
                    oConnection.db.S_REQ_RECIPE_RI_Ds.InsertAllOnSubmit(entityDetail)
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
        Public Function DeleteData(ByVal KDREQRECIPE_RI As String) As Boolean
            Try
                If Not oConnection.GetConnection() Then
                    DeleteData = False
                    Exit Function
                End If

                sREFERENCE = KDREQRECIPE_RI
                sSTATUS = "DELETE"

                Dim ds = oConnection.db.S_REQ_RECIPE_RI_Hs.FirstOrDefault(Function(x) x.KDREQRECIPE_RI = KDREQRECIPE_RI)
                Dim dsDetail = oConnection.db.S_REQ_RECIPE_RI_Ds.Where(Function(x) x.KDREQRECIPE_RI = KDREQRECIPE_RI)

                Try
                    oConnection.db.S_REQ_RECIPE_RI_Hs.DeleteOnSubmit(ds)
                    oConnection.db.S_REQ_RECIPE_RI_Ds.DeleteAllOnSubmit(dsDetail)
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
        Public Function UpdatePemberiObat(ByVal sKDREQRECIPE_RI As String, ByVal sSEQ As Integer, ByVal keterangan As String) As Boolean
            Try
                If Not oConnection.GetConnection Then
                    UpdatePemberiObat = False
                    Exit Function
                End If

                UpdatePemberiObat = True

                Dim ds = oConnection.db.S_REQ_RECIPE_RI_Ds.FirstOrDefault(Function(x) x.KDREQRECIPE_RI = sKDREQRECIPE_RI And x.SEQ = sSEQ)

                If ds IsNot Nothing Then
                    ds.REMARKS_FARMASI = IIf(ds.REMARKS_FARMASI = "", keterangan, ds.REMARKS_FARMASI & "#" & keterangan)

                    oConnection.db.SubmitChanges()

                End If

            Catch ex As Exception
                UpdatePemberiObat = False
                Throw ex
            End Try
        End Function
    End Class
End Namespace