Imports DataAccess.My.Resources

Namespace Order
    Public Class clsOrderPenunjang
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

            sMODUL = "ORDER PENUNJANG"
        End Sub
        Protected Overrides Sub Finalize()
            MyBase.Finalize()
            oConnection = Nothing
            GC.SuppressFinalize(Me)
        End Sub
        'Public Function GetStructureDetail() As S_REQ_ORDER_PENUNJANG
        '    If Not oConnection.GetConnection() Then
        '        GetStructureDetail = Nothing
        '    End If
        '    GetStructureDetail = New S_REQ_ORDER_PENUNJANG
        'End Function
        Public Function GetStructureDetailAwal() As S_REQ_ORDER_PENUNJANG_AWAL
            If Not oConnection.GetConnection() Then
                GetStructureDetailAwal = Nothing
            End If
            GetStructureDetailAwal = New S_REQ_ORDER_PENUNJANG_AWAL
        End Function
        Public Function GetStructureSuara() As Z_SUARA
            If Not oConnection.GetConnection() Then
                GetStructureSuara = Nothing
            End If
            GetStructureSuara = New Z_SUARA
        End Function
        'Public Function GetStructureDetailList() As List(Of S_REQ_ORDER_PENUNJANG)
        '    If Not oConnection.GetConnection() Then
        '        GetStructureDetailList = Nothing
        '    End If
        '    GetStructureDetailList = New List(Of S_REQ_ORDER_PENUNJANG)
        'End Function
        Public Function GetStructureDetailAwalList() As List(Of S_REQ_ORDER_PENUNJANG_AWAL)
            If Not oConnection.GetConnection() Then
                GetStructureDetailAwalList = Nothing
            End If
            GetStructureDetailAwalList = New List(Of S_REQ_ORDER_PENUNJANG_AWAL)
        End Function
        'Public Function GetDataDetail() As List(Of S_REQ_ORDER_PENUNJANG)
        '    If Not oConnection.GetConnection() Then
        '        GetDataDetail = Nothing
        '        Exit Function
        '    End If
        '    GetDataDetail = oConnection.db.S_REQ_ORDER_PENUNJANGs.ToList()
        'End Function
        'Public Function GetDataDetailLisLab(ByVal sKDCPPT As String) As List(Of S_REQ_ORDER_PENUNJANG)
        '    If Not oConnection.GetConnection() Then
        '        GetDataDetailLisLab = Nothing
        '        Exit Function
        '    End If
        '    GetDataDetailLisLab = oConnection.db.S_REQ_ORDER_PENUNJANGs.Where(Function(x) x.KDCPPT = sKDCPPT And x.KATEGORI = "LABORATORIUM").OrderBy(Function(x) x.SEQ).ToList()
        'End Function
        Public Function GetDataDetailLisAll(ByVal sKODE As String) As List(Of S_REQ_ORDER_PENUNJANG_AWAL)
            If Not oConnection.GetConnection() Then
                GetDataDetailLisAll = Nothing
                Exit Function
            End If
            GetDataDetailLisAll = oConnection.db.S_REQ_ORDER_PENUNJANG_AWALs.Where(Function(x) x.KODE = sKODE).ToList()
        End Function
        Public Function GetDataDetailLisLabAwal(ByVal sKDCPPT As String) As List(Of S_REQ_ORDER_PENUNJANG_AWAL)
            If Not oConnection.GetConnection() Then
                GetDataDetailLisLabAwal = Nothing
                Exit Function
            End If
            GetDataDetailLisLabAwal = oConnection.db.S_REQ_ORDER_PENUNJANG_AWALs.Where(Function(x) x.KODE = sKDCPPT And x.KATEGORI = "LABORATORIUM").ToList()
        End Function
        'Public Function GetDataDetailLisRad(ByVal sKDCPPT As String) As List(Of S_REQ_ORDER_PENUNJANG)
        '    If Not oConnection.GetConnection() Then
        '        GetDataDetailLisRad = Nothing
        '        Exit Function
        '    End If
        '    GetDataDetailLisRad = oConnection.db.S_REQ_ORDER_PENUNJANGs.Where(Function(x) x.KDCPPT = sKDCPPT And x.KATEGORI = "RADIOLOGI").OrderBy(Function(x) x.SEQ).ToList()
        'End Function
        Public Function GetDataDetailLisRadAwal(ByVal sKDCPPT As String) As List(Of S_REQ_ORDER_PENUNJANG_AWAL)
            If Not oConnection.GetConnection() Then
                GetDataDetailLisRadAwal = Nothing
                Exit Function
            End If
            GetDataDetailLisRadAwal = oConnection.db.S_REQ_ORDER_PENUNJANG_AWALs.Where(Function(x) x.KODE = sKDCPPT And x.KATEGORI = "RADIOLOGI").ToList()
        End Function
        'Public Function GetDataKDCPPTLAB(ByVal sKDCPPT As String) As S_REQ_ORDER_PENUNJANG
        '    If Not oConnection.GetConnection() Then
        '        GetDataKDCPPTLAB = Nothing
        '        Exit Function
        '    End If
        '    GetDataKDCPPTLAB = oConnection.db.S_REQ_ORDER_PENUNJANGs.FirstOrDefault(Function(x) x.KDCPPT = sKDCPPT And x.KATEGORI = "LABORATORIUM")
        'End Function
        'Public Function GetDataKDCPPTRAD(ByVal sKDCPPT As String) As S_REQ_ORDER_PENUNJANG
        '    If Not oConnection.GetConnection() Then
        '        GetDataKDCPPTRAD = Nothing
        '        Exit Function
        '    End If
        '    GetDataKDCPPTRAD = oConnection.db.S_REQ_ORDER_PENUNJANGs.FirstOrDefault(Function(x) x.KDCPPT = sKDCPPT And x.KATEGORI = "RADIOLOGI")
        'End Function
        Public Function GetDataKDCPPTLABAWALBYKODE(ByVal sKODE As String) As S_REQ_ORDER_PENUNJANG_AWAL
            If Not oConnection.GetConnection() Then
                GetDataKDCPPTLABAWALBYKODE = Nothing
                Exit Function
            End If
            GetDataKDCPPTLABAWALBYKODE = oConnection.db.S_REQ_ORDER_PENUNJANG_AWALs.FirstOrDefault(Function(x) x.KODE = sKODE And x.KATEGORI = "LABORATORIUM")
        End Function
        Public Function GetDataKDCPPTRADAWALBYKODE(ByVal sKODE As String) As S_REQ_ORDER_PENUNJANG_AWAL
            If Not oConnection.GetConnection() Then
                GetDataKDCPPTRADAWALBYKODE = Nothing
                Exit Function
            End If
            GetDataKDCPPTRADAWALBYKODE = oConnection.db.S_REQ_ORDER_PENUNJANG_AWALs.FirstOrDefault(Function(x) x.KODE = sKODE And x.KATEGORI = "RADIOLOGI")
        End Function
        Public Function GetDataKDCPPTLABAWALBYCPPT(ByVal sKDCPPT As String) As S_REQ_ORDER_PENUNJANG_AWAL
            If Not oConnection.GetConnection() Then
                GetDataKDCPPTLABAWALBYCPPT = Nothing
                Exit Function
            End If
            GetDataKDCPPTLABAWALBYCPPT = oConnection.db.S_REQ_ORDER_PENUNJANG_AWALs.FirstOrDefault(Function(x) x.KDCPPT <> "" And x.KDCPPT = sKDCPPT And x.KATEGORI = "LABORATORIUM")
        End Function
        Public Function GetDataKDCPPTRADAWALBYCPPT(ByVal sKDCPPT As String) As S_REQ_ORDER_PENUNJANG_AWAL
            If Not oConnection.GetConnection() Then
                GetDataKDCPPTRADAWALBYCPPT = Nothing
                Exit Function
            End If
            GetDataKDCPPTRADAWALBYCPPT = oConnection.db.S_REQ_ORDER_PENUNJANG_AWALs.FirstOrDefault(Function(x) x.KDCPPT <> "" And x.KDCPPT = sKDCPPT And x.KATEGORI = "RADIOLOGI")
        End Function
        Public Function GetDataDetailByKdpendaftaranLabH(ByVal sKDPENDAFTARAN As String) As List(Of S_REQ_ORDER_PENUNJANG_AWAL)
            If Not oConnection.GetConnection() Then
                GetDataDetailByKdpendaftaranLabH = Nothing
                Exit Function
            End If
            GetDataDetailByKdpendaftaranLabH = oConnection.db.S_REQ_ORDER_PENUNJANG_AWALs.Where(Function(x) x.KDREG = sKDPENDAFTARAN And x.KATEGORI = "LABORATORIUM").OrderBy(Function(x) x.DATE).ToList()
        End Function
        Public Function InsertDataSuara(ByVal entity As Z_SUARA) As Boolean
            Try
                If Not oConnection.GetConnection() Then
                    InsertDataSuara = False
                    Exit Function
                End If

                sREFERENCE = entity.KODE
                sSTATUS = "INSERT"

                Try
                    oConnection.db.Z_SUARAs.InsertOnSubmit(entity)
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

                InsertDataSuara = True
            Catch ex As Exception
                InsertDataSuara = False
                oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                Throw ex
            End Try
        End Function
        Public Function InsertDataAwal(ByVal entityDetail As List(Of S_REQ_ORDER_PENUNJANG_AWAL)) As Boolean
            Try
                If Not oConnection.GetConnection() Then
                    InsertDataAwal = False
                    Exit Function
                End If

                sREFERENCE = entityDetail.FirstOrDefault.KDCPPT
                sSTATUS = "INSERT"

                Try
                    oConnection.db.S_REQ_ORDER_PENUNJANG_AWALs.InsertAllOnSubmit(entityDetail)
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

                InsertDataAwal = True
            Catch ex As Exception
                InsertDataAwal = False
                oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                Throw ex
            End Try
        End Function
        Public Function UpdateDataAwal(ByVal entityDetail As List(Of S_REQ_ORDER_PENUNJANG_AWAL)) As Boolean
            Try
                If Not oConnection.GetConnection() Then
                    UpdateDataAwal = False
                    Exit Function
                End If

                sREFERENCE = entityDetail.FirstOrDefault.KDCPPT
                sSTATUS = "UPDATE"

                Dim dsDetail = oConnection.db.S_REQ_ORDER_PENUNJANG_AWALs.Where(Function(x) x.KODE = entityDetail.FirstOrDefault.KODE And x.KATEGORI = entityDetail.FirstOrDefault.KATEGORI)

                Try
                    oConnection.db.S_REQ_ORDER_PENUNJANG_AWALs.DeleteAllOnSubmit(dsDetail)
                    oConnection.db.S_REQ_ORDER_PENUNJANG_AWALs.InsertAllOnSubmit(entityDetail)
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

                UpdateDataAwal = True
            Catch ex As Exception
                UpdateDataAwal = False
                oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                Throw ex
            End Try
        End Function
        'Public Function InsertData(ByVal entityDetail As List(Of S_REQ_ORDER_PENUNJANG)) As Boolean
        '    Try
        '        If Not oConnection.GetConnection() Then
        '            InsertData = False
        '            Exit Function
        '        End If

        '        sREFERENCE = entityDetail.FirstOrDefault.KDCPPT
        '        sSTATUS = "INSERT"

        '        Try
        '            oConnection.db.S_REQ_ORDER_PENUNJANGs.InsertAllOnSubmit(entityDetail)
        '        Catch ex As Exception
        '            oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
        '            Throw ex
        '        End Try
        '        Try
        '            oConnection.db.SubmitChanges()
        '        Catch ex As Exception
        '            oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
        '            Throw ex
        '        End Try

        '        InsertData = True
        '    Catch ex As Exception
        '        InsertData = False
        '        oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
        '        Throw ex
        '    End Try
        'End Function
        'Public Function UpdateData(ByVal entityDetail As List(Of S_REQ_ORDER_PENUNJANG)) As Boolean
        '    Try
        '        If Not oConnection.GetConnection() Then
        '            UpdateData = False
        '            Exit Function
        '        End If

        '        sREFERENCE = entityDetail.FirstOrDefault.KDCPPT
        '        sSTATUS = "UPDATE"

        '        Dim dsDetail = oConnection.db.S_REQ_ORDER_PENUNJANGs.Where(Function(x) x.KDCPPT = entityDetail.FirstOrDefault.KDCPPT)

        '        Try
        '            oConnection.db.S_REQ_ORDER_PENUNJANGs.DeleteAllOnSubmit(dsDetail)
        '            oConnection.db.S_REQ_ORDER_PENUNJANGs.InsertAllOnSubmit(entityDetail)
        '        Catch ex As Exception
        '            oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
        '            Throw ex
        '        End Try
        '        Try
        '            oConnection.db.SubmitChanges()
        '        Catch ex As Exception
        '            oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
        '            Throw ex
        '        End Try

        '        UpdateData = True
        '    Catch ex As Exception
        '        UpdateData = False
        '        oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
        '        Throw ex
        '    End Try
        'End Function
        'Public Function DeleteData(ByVal sKDPPT As String) As Boolean
        '    Try
        '        If Not oConnection.GetConnection() Then
        '            DeleteData = False
        '            Exit Function
        '        End If

        '        sREFERENCE = sKDPPT
        '        sSTATUS = "DELETE"

        '        Dim dsDetail = oConnection.db.S_REQ_ORDER_PENUNJANGs.Where(Function(x) x.KDCPPT = sKDPPT)

        '        Try
        '            oConnection.db.S_REQ_ORDER_PENUNJANGs.DeleteAllOnSubmit(dsDetail)
        '        Catch ex As Exception
        '            oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
        '            Throw ex
        '        End Try
        '        Try
        '            oConnection.db.SubmitChanges()
        '        Catch ex As Exception
        '            oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
        '            Throw ex
        '        End Try

        '        DeleteData = True
        '    Catch ex As Exception
        '        DeleteData = False
        '        oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
        '        Throw ex
        '    End Try
        'End Function
        'Public Function UpdateDataIsCatatan(ByVal sSEQ As Integer) As Boolean
        '    Try
        '        If Not oConnection.GetConnection Then
        '            UpdateDataIsCatatan = False
        '            Exit Function
        '        End If

        '        UpdateDataIsCatatan = True

        '        Dim ds = oConnection.db.S_REQ_ORDER_PENUNJANGs.FirstOrDefault(Function(x) x.SEQ = sSEQ)

        '        ds.MEMO = "DELETE"

        '        oConnection.db.SubmitChanges()
        '    Catch ex As Exception
        '        UpdateDataIsCatatan = False
        '        Throw ex
        '    End Try
        'End Function
    End Class
End Namespace