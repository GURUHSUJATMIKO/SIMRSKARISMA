Imports System.Data.SqlClient
Imports System.Threading

Namespace EMedrek
    Public Class clsS_DIGITAL_OK_LAPORANTINDAKAN
        Public oConnection As Setting.clsConnectionMain = Nothing
        Public oError As Setting.clsError = Nothing
        Public oCounter As Setting.clsCounter = Nothing
        Public sREFERENCE As String = ""
        Public sMODUL As String = ""
        Public sLASTNUMBER As Integer = 0

        Public Sub New()
            oConnection = New Setting.clsConnectionMain
            oError = New Setting.clsError
            oCounter = New Setting.clsCounter
            sMODUL = "LTN"
        End Sub
        Protected Overrides Sub Finalize()
            MyBase.Finalize()
            oConnection = Nothing
            GC.SuppressFinalize(Me)
        End Sub
        Public Function GetStructureHeader() As S_DIGITAL_OK_LAPORANTINDAKAN
            If Not oConnection.GetConnection Then
                GetStructureHeader = Nothing
            End If
            GetStructureHeader = New S_DIGITAL_OK_LAPORANTINDAKAN
        End Function
        Public Function GetStructureHeaderTemplate() As S_DIGITAL_OK_LAPORANTINDAKAN_TEMPLATE
            If Not oConnection.GetConnection Then
                GetStructureHeaderTemplate = Nothing
            End If
            GetStructureHeaderTemplate = New S_DIGITAL_OK_LAPORANTINDAKAN_TEMPLATE
        End Function
        Public Function GetStructureDetailDiagnosaList() As List(Of S_DIGITAL_OK_LAPORANTINDAKAN_DIAGNOSA)
            If Not oConnection.GetConnection() Then
                GetStructureDetailDiagnosaList = Nothing
            End If
            GetStructureDetailDiagnosaList = New List(Of S_DIGITAL_OK_LAPORANTINDAKAN_DIAGNOSA)
        End Function
        Public Function GetStructureDetailDiagnosaTemplateList() As List(Of S_DIGITAL_OK_LAPORANTINDAKAN_TEMPLATE_DIAGNOSA)
            If Not oConnection.GetConnection() Then
                GetStructureDetailDiagnosaTemplateList = Nothing
            End If
            GetStructureDetailDiagnosaTemplateList = New List(Of S_DIGITAL_OK_LAPORANTINDAKAN_TEMPLATE_DIAGNOSA)
        End Function
        Public Function GetStructureDetailDiagnosa2List() As List(Of S_DIGITAL_OK_LAPORANTINDAKAN_DIAGNOSA_2)
            If Not oConnection.GetConnection() Then
                GetStructureDetailDiagnosa2List = Nothing
            End If
            GetStructureDetailDiagnosa2List = New List(Of S_DIGITAL_OK_LAPORANTINDAKAN_DIAGNOSA_2)
        End Function
        Public Function GetStructureDetailDiagnosa2TemplateList() As List(Of S_DIGITAL_OK_LAPORANTINDAKAN_TEMPLATE_DIAGNOSA_2)
            If Not oConnection.GetConnection() Then
                GetStructureDetailDiagnosa2TemplateList = Nothing
            End If
            GetStructureDetailDiagnosa2TemplateList = New List(Of S_DIGITAL_OK_LAPORANTINDAKAN_TEMPLATE_DIAGNOSA_2)
        End Function
        Public Function GetStructureDetailProsedurList() As List(Of S_DIGITAL_OK_LAPORANTINDAKAN_PROSEDUR)
            If Not oConnection.GetConnection() Then
                GetStructureDetailProsedurList = Nothing
            End If
            GetStructureDetailProsedurList = New List(Of S_DIGITAL_OK_LAPORANTINDAKAN_PROSEDUR)
        End Function
        Public Function GetStructureDetailProsedurTemplateList() As List(Of S_DIGITAL_OK_LAPORANTINDAKAN_TEMPLATE_PROSEDUR)
            If Not oConnection.GetConnection() Then
                GetStructureDetailProsedurTemplateList = Nothing
            End If
            GetStructureDetailProsedurTemplateList = New List(Of S_DIGITAL_OK_LAPORANTINDAKAN_TEMPLATE_PROSEDUR)
        End Function
        Public Function GetStructureDetailDiagnosa() As S_DIGITAL_OK_LAPORANTINDAKAN_DIAGNOSA
            If Not oConnection.GetConnection() Then
                GetStructureDetailDiagnosa = Nothing
            End If
            GetStructureDetailDiagnosa = New S_DIGITAL_OK_LAPORANTINDAKAN_DIAGNOSA
        End Function
        Public Function GetStructureDetailDiagnosaTemplate() As S_DIGITAL_OK_LAPORANTINDAKAN_TEMPLATE_DIAGNOSA
            If Not oConnection.GetConnection() Then
                GetStructureDetailDiagnosaTemplate = Nothing
            End If
            GetStructureDetailDiagnosaTemplate = New S_DIGITAL_OK_LAPORANTINDAKAN_TEMPLATE_DIAGNOSA
        End Function
        Public Function GetStructureDetailDiagnosa2() As S_DIGITAL_OK_LAPORANTINDAKAN_DIAGNOSA_2
            If Not oConnection.GetConnection() Then
                GetStructureDetailDiagnosa2 = Nothing
            End If
            GetStructureDetailDiagnosa2 = New S_DIGITAL_OK_LAPORANTINDAKAN_DIAGNOSA_2
        End Function
        Public Function GetStructureDetailDiagnosa2Template() As S_DIGITAL_OK_LAPORANTINDAKAN_TEMPLATE_DIAGNOSA_2
            If Not oConnection.GetConnection() Then
                GetStructureDetailDiagnosa2Template = Nothing
            End If
            GetStructureDetailDiagnosa2Template = New S_DIGITAL_OK_LAPORANTINDAKAN_TEMPLATE_DIAGNOSA_2
        End Function
        Public Function GetStructureDetailProsedurTemplate() As S_DIGITAL_OK_LAPORANTINDAKAN_TEMPLATE_PROSEDUR
            If Not oConnection.GetConnection() Then
                GetStructureDetailProsedurTemplate = Nothing
            End If
            GetStructureDetailProsedurTemplate = New S_DIGITAL_OK_LAPORANTINDAKAN_TEMPLATE_PROSEDUR
        End Function
        Public Function GetStructureDetailProsedur() As S_DIGITAL_OK_LAPORANTINDAKAN_PROSEDUR
            If Not oConnection.GetConnection() Then
                GetStructureDetailProsedur = Nothing
            End If
            GetStructureDetailProsedur = New S_DIGITAL_OK_LAPORANTINDAKAN_PROSEDUR
        End Function
        Public Function GetData() As List(Of S_DIGITAL_OK_LAPORANTINDAKAN)
            If Not oConnection.GetConnection Then
                GetData = Nothing
                Exit Function
            End If
            GetData = oConnection.db.S_DIGITAL_OK_LAPORANTINDAKANs.OrderBy(Function(x) x.KDLAPORANTINDAKAN).ToList()
        End Function
        Public Function GetDataByRMList(ByVal RM As String) As List(Of S_DIGITAL_OK_LAPORANTINDAKAN)
            If Not oConnection.GetConnection Then
                GetDataByRMList = Nothing
                Exit Function
            End If
            GetDataByRMList = oConnection.db.S_DIGITAL_OK_LAPORANTINDAKANs.Where(Function(x) x.KDCUSTOMER = RM).OrderBy(Function(x) x.KDPENDAFTARAN).ToList()
        End Function
        Public Function GetDataByRegisterList(ByVal sKDPENDAFTARAN As String) As List(Of S_DIGITAL_OK_LAPORANTINDAKAN)
            If Not oConnection.GetConnection Then
                GetDataByRegisterList = Nothing
                Exit Function
            End If
            GetDataByRegisterList = oConnection.db.S_DIGITAL_OK_LAPORANTINDAKANs.Where(Function(x) x.KDPENDAFTARAN = sKDPENDAFTARAN).OrderBy(Function(x) x.DATE).ToList()
        End Function
        Public Function GetData(ByVal sParameter As String) As S_DIGITAL_OK_LAPORANTINDAKAN
            If Not oConnection.GetConnection Then
                GetData = Nothing
                Exit Function
            End If
            GetData = oConnection.db.S_DIGITAL_OK_LAPORANTINDAKANs.FirstOrDefault(Function(x) x.KDLAPORANTINDAKAN = sParameter)
        End Function
        Public Function GetDataTemplate(ByVal sParameter As String) As S_DIGITAL_OK_LAPORANTINDAKAN_TEMPLATE
            If Not oConnection.GetConnection Then
                GetDataTemplate = Nothing
                Exit Function
            End If
            GetDataTemplate = oConnection.db.S_DIGITAL_OK_LAPORANTINDAKAN_TEMPLATEs.FirstOrDefault(Function(x) x.KDJUDUL = sParameter)
        End Function
        Public Function GetDataByRM(ByVal kdcustomer As String) As List(Of S_DIGITAL_OK_LAPORANTINDAKAN)
            If Not oConnection.GetConnection() Then
                GetDataByRM = Nothing
                Exit Function
            End If
            GetDataByRM = oConnection.db.S_DIGITAL_OK_LAPORANTINDAKANs.Where(Function(x) x.KDCUSTOMER = kdcustomer).OrderByDescending(Function(x) x.KDPENDAFTARAN).ToList()
        End Function
        Public Function GetDataDetailDiagnosa(ByVal sKDLAPORANTINDAKAN As String) As List(Of S_DIGITAL_OK_LAPORANTINDAKAN_DIAGNOSA)
            If Not oConnection.GetConnection() Then
                GetDataDetailDiagnosa = Nothing
                Exit Function
            End If
            GetDataDetailDiagnosa = oConnection.db.S_DIGITAL_OK_LAPORANTINDAKAN_DIAGNOSAs.Where(Function(x) x.KDLAPORANTINDAKAN = sKDLAPORANTINDAKAN).OrderBy(Function(x) x.SEQ).ToList()
        End Function
        Public Function GetDataDetailDiagnosa2(ByVal sKDLAPORANTINDAKAN As String) As List(Of S_DIGITAL_OK_LAPORANTINDAKAN_DIAGNOSA_2)
            If Not oConnection.GetConnection() Then
                GetDataDetailDiagnosa2 = Nothing
                Exit Function
            End If
            GetDataDetailDiagnosa2 = oConnection.db.S_DIGITAL_OK_LAPORANTINDAKAN_DIAGNOSA_2s.Where(Function(x) x.KDLAPORANTINDAKAN = sKDLAPORANTINDAKAN).OrderBy(Function(x) x.SEQ).ToList()
        End Function
        Public Function GetDataDetailProsedur(ByVal sKDLAPORANTINDAKAN As String) As List(Of S_DIGITAL_OK_LAPORANTINDAKAN_PROSEDUR)
            If Not oConnection.GetConnection() Then
                GetDataDetailProsedur = Nothing
                Exit Function
            End If
            GetDataDetailProsedur = oConnection.db.S_DIGITAL_OK_LAPORANTINDAKAN_PROSEDURs.Where(Function(x) x.KDLAPORANTINDAKAN = sKDLAPORANTINDAKAN).OrderBy(Function(x) x.SEQ).ToList()
        End Function
        Public Function GetDataDetailDiagnosaTemplate(ByVal sKDLAPORANTINDAKAN As String) As List(Of S_DIGITAL_OK_LAPORANTINDAKAN_TEMPLATE_DIAGNOSA)
            If Not oConnection.GetConnection() Then
                GetDataDetailDiagnosaTemplate = Nothing
                Exit Function
            End If
            GetDataDetailDiagnosaTemplate = oConnection.db.S_DIGITAL_OK_LAPORANTINDAKAN_TEMPLATE_DIAGNOSAs.Where(Function(x) x.KDJUDUL = sKDLAPORANTINDAKAN).OrderBy(Function(x) x.SEQ).ToList()
        End Function
        Public Function GetDataDetailDiagnosa2Template(ByVal sKDLAPORANTINDAKAN As String) As List(Of S_DIGITAL_OK_LAPORANTINDAKAN_TEMPLATE_DIAGNOSA_2)
            If Not oConnection.GetConnection() Then
                GetDataDetailDiagnosa2Template = Nothing
                Exit Function
            End If
            GetDataDetailDiagnosa2Template = oConnection.db.S_DIGITAL_OK_LAPORANTINDAKAN_TEMPLATE_DIAGNOSA_2s.Where(Function(x) x.KDJUDUL = sKDLAPORANTINDAKAN).OrderBy(Function(x) x.SEQ).ToList()
        End Function
        Public Function GetDataDetailProsedurTemplate(ByVal sKDLAPORANTINDAKAN As String) As List(Of S_DIGITAL_OK_LAPORANTINDAKAN_TEMPLATE_PROSEDUR)
            If Not oConnection.GetConnection() Then
                GetDataDetailProsedurTemplate = Nothing
                Exit Function
            End If
            GetDataDetailProsedurTemplate = oConnection.db.S_DIGITAL_OK_LAPORANTINDAKAN_TEMPLATE_PROSEDURs.Where(Function(x) x.KDJUDUL = sKDLAPORANTINDAKAN).OrderBy(Function(x) x.SEQ).ToList()
        End Function
        Public Function InsertData(ByVal entity As S_DIGITAL_OK_LAPORANTINDAKAN, ByVal entityDiagnosa As List(Of S_DIGITAL_OK_LAPORANTINDAKAN_DIAGNOSA), ByVal entityDiagnosa2 As List(Of S_DIGITAL_OK_LAPORANTINDAKAN_DIAGNOSA_2), ByVal entityProsedur As List(Of S_DIGITAL_OK_LAPORANTINDAKAN_PROSEDUR)) As Boolean
            Try
                If Not oConnection.GetConnection Then
                    InsertData = False
                    Exit Function
                End If

                sREFERENCE = entity.KDLAPORANTINDAKAN

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

                    entity.KDLAPORANTINDAKAN = AutoNumber(sMODUL, sLASTNUMBER + 1, entity.DATE)

                    For Each iLoop In entityDiagnosa
                        iLoop.KDLAPORANTINDAKAN = entity.KDLAPORANTINDAKAN
                    Next

                    'For Each iLoop In entityDiagnosa2
                    '    iLoop.KDLAPORANTINDAKAN = entity.KDLAPORANTINDAKAN
                    'Next

                    For Each iLoop In entityProsedur
                        iLoop.KDLAPORANTINDAKAN = entity.KDLAPORANTINDAKAN
                    Next

                Catch ex As Exception
                    oError.InsertData(sMODUL, "S_DIGITAL_OK_LAPORANTINDAKAN", ex.ToString, sREFERENCE)
                    Throw ex
                End Try

                Try
                    oConnection.db.S_DIGITAL_OK_LAPORANTINDAKANs.InsertOnSubmit(entity)
                    If entityDiagnosa.Count > 0 Then
                        oConnection.db.S_DIGITAL_OK_LAPORANTINDAKAN_DIAGNOSAs.InsertAllOnSubmit(entityDiagnosa)
                    End If
                    'If entityDiagnosa2.Count > 0 Then
                    '    oConnection.db.S_DIGITAL_OK_LAPORANTINDAKAN_DIAGNOSA_2s.InsertAllOnSubmit(entityDiagnosa2)
                    'End If
                    If entityProsedur.Count > 0 Then
                        oConnection.db.S_DIGITAL_OK_LAPORANTINDAKAN_PROSEDURs.InsertAllOnSubmit(entityProsedur)
                    End If
                Catch ex As Exception
                    oError.InsertData("S_DIGITAL_OK_LAPORANTINDAKAN", "INSERTDATA", ex.ToString, sREFERENCE)
                    Throw ex
                End Try
                Try
                    oConnection.db.SubmitChanges()
                Catch ex As Exception
                    oError.InsertData("S_DIGITAL_OK_LAPORANTINDAKAN", "INSERTDATA", ex.ToString, sREFERENCE)
                    Throw ex
                End Try

                Try
                    oCounter.UpdateData(sMODUL, sLASTNUMBER + 1, Month(entity.DATE), Year(entity.DATE))
                Catch ex As Exception
                    oError.InsertData(sMODUL, "INSERTDATA", ex.ToString, sREFERENCE)
                    Throw ex
                End Try

                InsertData = True
            Catch ex As Exception
                InsertData = False
                oError.InsertData("S_DIGITAL_OK_LAPORANTINDAKAN", "INSERTDATA", ex.ToString, sREFERENCE)
                Throw ex
            End Try
        End Function
        Public Function UpdateData(ByVal entity As S_DIGITAL_OK_LAPORANTINDAKAN, ByVal entityDiagnosa As List(Of S_DIGITAL_OK_LAPORANTINDAKAN_DIAGNOSA), ByVal entityDiagnosa2 As List(Of S_DIGITAL_OK_LAPORANTINDAKAN_DIAGNOSA_2), ByVal entityProsedur As List(Of S_DIGITAL_OK_LAPORANTINDAKAN_PROSEDUR)) As Boolean
            Try
                If Not oConnection.GetConnection Then
                    UpdateData = False
                    Exit Function
                End If

                sREFERENCE = entity.KDLAPORANTINDAKAN

                Dim ds = oConnection.db.S_DIGITAL_OK_LAPORANTINDAKANs.FirstOrDefault(Function(x) x.KDLAPORANTINDAKAN = entity.KDLAPORANTINDAKAN)
                Dim dsDiagnosa = oConnection.db.S_DIGITAL_OK_LAPORANTINDAKAN_DIAGNOSAs.Where(Function(x) x.KDLAPORANTINDAKAN = entity.KDLAPORANTINDAKAN)
                Dim dsDiagnosa2 = oConnection.db.S_DIGITAL_OK_LAPORANTINDAKAN_DIAGNOSA_2s.Where(Function(x) x.KDLAPORANTINDAKAN = entity.KDLAPORANTINDAKAN)
                Dim dsProsedur = oConnection.db.S_DIGITAL_OK_LAPORANTINDAKAN_PROSEDURs.Where(Function(x) x.KDLAPORANTINDAKAN = entity.KDLAPORANTINDAKAN)

                Try
                    oConnection.db.S_DIGITAL_OK_LAPORANTINDAKANs.DeleteOnSubmit(ds)
                    oConnection.db.S_DIGITAL_OK_LAPORANTINDAKANs.InsertOnSubmit(entity)

                    If dsDiagnosa.Count > 0 Then
                        oConnection.db.S_DIGITAL_OK_LAPORANTINDAKAN_DIAGNOSAs.DeleteAllOnSubmit(dsDiagnosa)
                    End If
                    If entityDiagnosa.Count > 0 Then
                        oConnection.db.S_DIGITAL_OK_LAPORANTINDAKAN_DIAGNOSAs.InsertAllOnSubmit(entityDiagnosa)
                    End If

                    If dsDiagnosa2.Count > 0 Then
                        oConnection.db.S_DIGITAL_OK_LAPORANTINDAKAN_DIAGNOSA_2s.DeleteAllOnSubmit(dsDiagnosa2)
                    End If
                    'If entityDiagnosa2.Count > 0 Then
                    '    oConnection.db.S_DIGITAL_OK_LAPORANTINDAKAN_DIAGNOSA_2s.InsertAllOnSubmit(entityDiagnosa2)
                    'End If

                    If dsProsedur.Count > 0 Then
                        oConnection.db.S_DIGITAL_OK_LAPORANTINDAKAN_PROSEDURs.DeleteAllOnSubmit(dsProsedur)
                    End If
                    If entityProsedur.Count > 0 Then
                        oConnection.db.S_DIGITAL_OK_LAPORANTINDAKAN_PROSEDURs.InsertAllOnSubmit(entityProsedur)
                    End If

                Catch ex As Exception
                    oError.InsertData("S_DIGITAL_OK_LAPORANTINDAKAN", "UPDATEDATA", ex.ToString, sREFERENCE)
                    Throw ex
                End Try
                Try
                    oConnection.db.SubmitChanges()
                Catch ex As Exception
                    oError.InsertData("S_DIGITAL_OK_LAPORANTINDAKAN", "UPDATEDATA", ex.ToString, sREFERENCE)
                    Throw ex
                End Try

                UpdateData = True
            Catch ex As Exception
                UpdateData = False
                oError.InsertData("S_DIGITAL_OK_LAPORANTINDAKAN", "UPDATEDATA", ex.ToString, sREFERENCE)
                Throw ex
            End Try
        End Function
        Public Function DeleteData(ByVal Parameter As Integer, ByVal sSEQ As Integer) As Boolean
            Try
                If Not oConnection.GetConnection Then
                    DeleteData = False
                    Exit Function
                End If

                sREFERENCE = Parameter


                Dim ds = oConnection.db.S_DIGITAL_OK_LAPORANTINDAKANs.FirstOrDefault(Function(x) x.KDLAPORANTINDAKAN = Parameter)
                Dim dsDiagnosa = oConnection.db.S_DIGITAL_OK_LAPORANTINDAKAN_DIAGNOSAs.Where(Function(x) x.KDLAPORANTINDAKAN = Parameter)
                Dim dsDiagnosa2 = oConnection.db.S_DIGITAL_OK_LAPORANTINDAKAN_DIAGNOSA_2s.Where(Function(x) x.KDLAPORANTINDAKAN = Parameter)
                Dim dsProsedur = oConnection.db.S_DIGITAL_OK_LAPORANTINDAKAN_PROSEDURs.Where(Function(x) x.KDLAPORANTINDAKAN = Parameter)

                Try
                    oConnection.db.S_DIGITAL_OK_LAPORANTINDAKANs.DeleteOnSubmit(ds)

                    If dsDiagnosa.Count > 0 Then
                        oConnection.db.S_DIGITAL_OK_LAPORANTINDAKAN_DIAGNOSAs.DeleteAllOnSubmit(dsDiagnosa)
                    End If

                    If dsDiagnosa2.Count > 0 Then
                        oConnection.db.S_DIGITAL_OK_LAPORANTINDAKAN_DIAGNOSA_2s.DeleteAllOnSubmit(dsDiagnosa2)
                    End If

                    If dsProsedur.Count > 0 Then
                        oConnection.db.S_DIGITAL_OK_LAPORANTINDAKAN_PROSEDURs.DeleteAllOnSubmit(dsProsedur)
                    End If
                Catch ex As Exception
                    oError.InsertData("S_DIGITAL_OK_LAPORANTINDAKAN", "DELETEDATA", ex.ToString, sREFERENCE)
                    Throw ex
                End Try
                Try
                    oConnection.db.SubmitChanges()
                Catch ex As Exception
                    oError.InsertData("S_DIGITAL_OK_LAPORANTINDAKAN", "DELETEDATA", ex.ToString, sREFERENCE)
                    Throw ex
                End Try

                DeleteData = True
            Catch ex As Exception
                DeleteData = False
                oError.InsertData("S_DIGITAL_OK_LAPORANTINDAKAN", "DELETEDATA", ex.ToString, sREFERENCE)
                Throw ex
            End Try
        End Function
        Public Function UpdateCetak(ByVal sKDPENDAFTARAN As String) As Boolean
            Try
                If Not oConnection.GetConnection Then
                    UpdateCetak = False
                    Exit Function
                End If

                sREFERENCE = sKDPENDAFTARAN

                Try
                    Dim ds = oConnection.db.S_DIGITAL_OK_LAPORANTINDAKANs.FirstOrDefault(Function(x) x.KDPENDAFTARAN = sKDPENDAFTARAN)

                    ds.CETAK += 1

                    oConnection.db.SubmitChanges()

                Catch ex As Exception
                    oError.InsertData("S_DIGITAL_OK_LAPORANTINDAKAN", "UPDATECETAK", ex.ToString, sREFERENCE)
                    Throw ex
                End Try

                UpdateCetak = True
            Catch ex As Exception
                UpdateCetak = False
                oError.InsertData("S_DIGITAL_OK_LAPORANTINDAKAN", "UPDATECETAK", ex.ToString, sREFERENCE)
                Throw ex
            End Try
        End Function
        Public Function UpdateDeleteLaporanTindakan(ByVal sKDLAPORANTINDAKAN As String, ByVal sUSER As String) As Boolean
            Try
                If Not oConnection.GetConnection Then
                    UpdateDeleteLaporanTindakan = False
                    Exit Function
                End If

                sREFERENCE = sKDLAPORANTINDAKAN

                Try
                    Dim ds = oConnection.db.S_DIGITAL_OK_LAPORANTINDAKANs.FirstOrDefault(Function(x) x.KDLAPORANTINDAKAN = sKDLAPORANTINDAKAN)

                    ds.ISDELETE = 1
                    ds.DATEDELETE = Now
                    ds.USERDELETE = sUSER

                    oConnection.db.SubmitChanges()

                Catch ex As Exception
                    oError.InsertData("S_DIGITAL_OK_LAPORANOPERASI", "UPDATEDELETE", ex.ToString, sREFERENCE)
                    Throw ex
                End Try

                UpdateDeleteLaporanTindakan = True
            Catch ex As Exception
                UpdateDeleteLaporanTindakan = False
                oError.InsertData("S_DIGITAL_OK_LAPORANOPERASI", "UPDATEDELETE", ex.ToString, sREFERENCE)
                Throw ex
            End Try

        End Function
        Public Function InsertDataTemplate(ByVal entity As S_DIGITAL_OK_LAPORANTINDAKAN_TEMPLATE, ByVal entityDiagnosa As List(Of S_DIGITAL_OK_LAPORANTINDAKAN_TEMPLATE_DIAGNOSA), ByVal entityDiagnosa2 As List(Of S_DIGITAL_OK_LAPORANTINDAKAN_TEMPLATE_DIAGNOSA_2), ByVal entityProsedur As List(Of S_DIGITAL_OK_LAPORANTINDAKAN_TEMPLATE_PROSEDUR)) As Boolean
            Try
                If Not oConnection.GetConnection Then
                    InsertDataTemplate = False
                    Exit Function
                End If

                sREFERENCE = entity.KDJUDUL

                Try
                    For Each iLoop In entityDiagnosa
                        iLoop.KDJUDUL = entity.KDJUDUL
                    Next

                    For Each iLoop In entityDiagnosa2
                        iLoop.KDJUDUL = entity.KDJUDUL
                    Next

                    For Each iLoop In entityProsedur
                        iLoop.KDJUDUL = entity.KDJUDUL
                    Next

                Catch ex As Exception
                    oError.InsertData(sMODUL, "S_DIGITAL_OK_LAPORANTINDAKAN", ex.ToString, sREFERENCE)
                    Throw ex
                End Try

                Try
                    oConnection.db.S_DIGITAL_OK_LAPORANTINDAKAN_TEMPLATEs.InsertOnSubmit(entity)
                    If entityDiagnosa.Count > 0 Then
                        oConnection.db.S_DIGITAL_OK_LAPORANTINDAKAN_TEMPLATE_DIAGNOSAs.InsertAllOnSubmit(entityDiagnosa)
                    End If
                    If entityDiagnosa2.Count > 0 Then
                        oConnection.db.S_DIGITAL_OK_LAPORANTINDAKAN_TEMPLATE_DIAGNOSA_2s.InsertAllOnSubmit(entityDiagnosa2)
                    End If
                    If entityProsedur.Count > 0 Then
                        oConnection.db.S_DIGITAL_OK_LAPORANTINDAKAN_TEMPLATE_PROSEDURs.InsertAllOnSubmit(entityProsedur)
                    End If
                Catch ex As Exception
                    oError.InsertData("S_DIGITAL_OK_LAPORANTINDAKAN", "INSERTDATA", ex.ToString, sREFERENCE)
                    Throw ex
                End Try
                Try
                    oConnection.db.SubmitChanges()
                Catch ex As Exception
                    oError.InsertData("S_DIGITAL_OK_LAPORANTINDAKAN", "INSERTDATA", ex.ToString, sREFERENCE)
                    Throw ex
                End Try

                InsertDataTemplate = True
            Catch ex As Exception
                InsertDataTemplate = False
                oError.InsertData("S_DIGITAL_OK_LAPORANTINDAKAN", "INSERTDATA", ex.ToString, sREFERENCE)
                Throw ex
            End Try
        End Function
        Public Function UpdateDataTemplate(ByVal entity As S_DIGITAL_OK_LAPORANTINDAKAN_TEMPLATE, ByVal entityDiagnosa As List(Of S_DIGITAL_OK_LAPORANTINDAKAN_TEMPLATE_DIAGNOSA), ByVal entityDiagnosa2 As List(Of S_DIGITAL_OK_LAPORANTINDAKAN_TEMPLATE_DIAGNOSA_2), ByVal entityProsedur As List(Of S_DIGITAL_OK_LAPORANTINDAKAN_TEMPLATE_PROSEDUR)) As Boolean
            Try
                If Not oConnection.GetConnection Then
                    UpdateDataTemplate = False
                    Exit Function
                End If

                sREFERENCE = entity.KDJUDUL

                Dim ds = oConnection.db.S_DIGITAL_OK_LAPORANTINDAKAN_TEMPLATEs.FirstOrDefault(Function(x) x.KDJUDUL = entity.KDJUDUL)
                Dim dsDiagnosa = oConnection.db.S_DIGITAL_OK_LAPORANTINDAKAN_TEMPLATE_DIAGNOSAs.Where(Function(x) x.KDJUDUL = entity.KDJUDUL)
                Dim dsDiagnosa2 = oConnection.db.S_DIGITAL_OK_LAPORANTINDAKAN_TEMPLATE_DIAGNOSA_2s.Where(Function(x) x.KDJUDUL = entity.KDJUDUL)
                Dim dsProsedur = oConnection.db.S_DIGITAL_OK_LAPORANTINDAKAN_TEMPLATE_PROSEDURs.Where(Function(x) x.KDJUDUL = entity.KDJUDUL)

                Try
                    oConnection.db.S_DIGITAL_OK_LAPORANTINDAKAN_TEMPLATEs.DeleteOnSubmit(ds)
                    oConnection.db.S_DIGITAL_OK_LAPORANTINDAKAN_TEMPLATEs.InsertOnSubmit(entity)

                    If dsDiagnosa.Count > 0 Then
                        oConnection.db.S_DIGITAL_OK_LAPORANTINDAKAN_TEMPLATE_DIAGNOSAs.DeleteAllOnSubmit(dsDiagnosa)
                    End If
                    If entityDiagnosa.Count > 0 Then
                        oConnection.db.S_DIGITAL_OK_LAPORANTINDAKAN_TEMPLATE_DIAGNOSAs.InsertAllOnSubmit(entityDiagnosa)
                    End If

                    If dsDiagnosa2.Count > 0 Then
                        oConnection.db.S_DIGITAL_OK_LAPORANTINDAKAN_TEMPLATE_DIAGNOSA_2s.DeleteAllOnSubmit(dsDiagnosa2)
                    End If
                    If entityDiagnosa2.Count > 0 Then
                        oConnection.db.S_DIGITAL_OK_LAPORANTINDAKAN_TEMPLATE_DIAGNOSA_2s.InsertAllOnSubmit(entityDiagnosa2)
                    End If

                    If dsProsedur.Count > 0 Then
                        oConnection.db.S_DIGITAL_OK_LAPORANTINDAKAN_TEMPLATE_PROSEDURs.DeleteAllOnSubmit(dsProsedur)
                    End If
                    If entityProsedur.Count > 0 Then
                        oConnection.db.S_DIGITAL_OK_LAPORANTINDAKAN_TEMPLATE_PROSEDURs.InsertAllOnSubmit(entityProsedur)
                    End If

                Catch ex As Exception
                    oError.InsertData("S_DIGITAL_OK_LAPORANTINDAKAN", "UPDATEDATA", ex.ToString, sREFERENCE)
                    Throw ex
                End Try
                Try
                    oConnection.db.SubmitChanges()
                Catch ex As Exception
                    oError.InsertData("S_DIGITAL_OK_LAPORANTINDAKAN", "UPDATEDATA", ex.ToString, sREFERENCE)
                    Throw ex
                End Try

                UpdateDataTemplate = True
            Catch ex As Exception
                UpdateDataTemplate = False
                oError.InsertData("S_DIGITAL_OK_LAPORANTINDAKAN", "UPDATEDATA", ex.ToString, sREFERENCE)
                Throw ex
            End Try
        End Function

    End Class
End Namespace