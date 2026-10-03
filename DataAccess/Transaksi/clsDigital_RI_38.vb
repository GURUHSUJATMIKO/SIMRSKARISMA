Imports System.Data.SqlClient
Imports DataAccess.My.Resources

Namespace Inventory
    Public Class clsDigital_RI_38
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
        End Sub
        Protected Overrides Sub Finalize()
            MyBase.Finalize()
            oConnection = Nothing
            GC.SuppressFinalize(Me)
        End Sub
        Public Function GetStructureHeader() As S_DIGITAL_RI_38
            If Not oConnection.GetConnection Then
                GetStructureHeader = Nothing
            End If
            GetStructureHeader = New S_DIGITAL_RI_38
        End Function
        Public Function GetStructureDetail() As S_DIGITAL_RI_38_DETIL
            If Not oConnection.GetConnection Then
                GetStructureDetail = Nothing
            End If
            GetStructureDetail = New S_DIGITAL_RI_38_DETIL
        End Function
        Public Function GetStructureDetail2() As S_DIGITAL_RI_38_RESIKOJATUH
            If Not oConnection.GetConnection Then
                GetStructureDetail2 = Nothing
            End If
            GetStructureDetail2 = New S_DIGITAL_RI_38_RESIKOJATUH
        End Function
        Public Function GetStructureDetail3() As S_DIGITAL_RI_38_MONITORING
            If Not oConnection.GetConnection Then
                GetStructureDetail3 = Nothing
            End If
            GetStructureDetail3 = New S_DIGITAL_RI_38_MONITORING
        End Function
        Public Function GetStructureDetailList() As List(Of S_DIGITAL_RI_38_DETIL)
            If Not oConnection.GetConnection Then
                GetStructureDetailList = Nothing
            End If
            GetStructureDetailList = New List(Of S_DIGITAL_RI_38_DETIL)
        End Function
        Public Function GetStructureDetailList2() As List(Of S_DIGITAL_RI_38_RESIKOJATUH)
            If Not oConnection.GetConnection Then
                GetStructureDetailList2 = Nothing
            End If
            GetStructureDetailList2 = New List(Of S_DIGITAL_RI_38_RESIKOJATUH)
        End Function
        Public Function GetStructureDetailList3() As List(Of S_DIGITAL_RI_38_MONITORING)
            If Not oConnection.GetConnection Then
                GetStructureDetailList3 = Nothing
            End If
            GetStructureDetailList3 = New List(Of S_DIGITAL_RI_38_MONITORING)
        End Function
        Public Function GetData() As List(Of S_DIGITAL_RI_38)
            If Not oConnection.GetConnection Then
                GetData = Nothing
                Exit Function
            End If
            GetData = oConnection.db.S_DIGITAL_RI_38s.OrderBy(Function(x) x.KDPENDAFTARAN).ToList()
        End Function
        Public Function GetData(ByVal sParameter As String) As S_DIGITAL_RI_38
            If Not oConnection.GetConnection Then
                GetData = Nothing
                Exit Function
            End If
            GetData = oConnection.db.S_DIGITAL_RI_38s.FirstOrDefault(Function(x) x.KDPENDAFTARAN = sParameter)
        End Function
        Public Function GetDataDetail() As List(Of S_DIGITAL_RI_38_DETIL)
            If Not oConnection.GetConnection Then
                GetDataDetail = Nothing
                Exit Function
            End If
            GetDataDetail = oConnection.db.S_DIGITAL_RI_38_DETILs.ToList()
        End Function
        Public Function GetDataDetail2() As List(Of S_DIGITAL_RI_38_RESIKOJATUH)
            If Not oConnection.GetConnection Then
                GetDataDetail2 = Nothing
                Exit Function
            End If
            GetDataDetail2 = oConnection.db.S_DIGITAL_RI_38_RESIKOJATUHs.ToList()
        End Function
        Public Function GetDataDetail3() As List(Of S_DIGITAL_RI_38_MONITORING)
            If Not oConnection.GetConnection Then
                GetDataDetail3 = Nothing
                Exit Function
            End If
            GetDataDetail3 = oConnection.db.S_DIGITAL_RI_38_MONITORINGs.ToList()
        End Function
        Public Function GetDataDetail(ByVal Parameter As String) As List(Of S_DIGITAL_RI_38_DETIL)
            If Not oConnection.GetConnection Then
                GetDataDetail = Nothing
                Exit Function
            End If
            GetDataDetail = oConnection.db.S_DIGITAL_RI_38_DETILs.Where(Function(x) x.KDPENDAFTARAN = Parameter).ToList()
        End Function
        Public Function GetDataDetail2(ByVal Parameter As String) As List(Of S_DIGITAL_RI_38_RESIKOJATUH)
            If Not oConnection.GetConnection Then
                GetDataDetail2 = Nothing
                Exit Function
            End If
            GetDataDetail2 = oConnection.db.S_DIGITAL_RI_38_RESIKOJATUHs.Where(Function(x) x.KDPENDAFTARAN = Parameter).ToList()
        End Function
        Public Function GetDataDetail3(ByVal Parameter As String) As List(Of S_DIGITAL_RI_38_MONITORING)
            If Not oConnection.GetConnection Then
                GetDataDetail3 = Nothing
                Exit Function
            End If
            GetDataDetail3 = oConnection.db.S_DIGITAL_RI_38_MONITORINGs.Where(Function(x) x.KDPENDAFTARAN = Parameter).ToList()
        End Function
        Public Function IsExist(ByVal sParameter As String) As Boolean
            If Not oConnection.GetConnection Then
                IsExist = False
                Exit Function
            End If

            Dim ds = oConnection.db.S_DIGITAL_RI_38s.FirstOrDefault(Function(x) x.KDPENDAFTARAN = sParameter)

            If ds IsNot Nothing Then
                IsExist = True
            Else
                IsExist = False
            End If
        End Function
        Public Function InsertData(ByVal entity As S_DIGITAL_RI_38, ByVal entityDetail As List(Of S_DIGITAL_RI_38_DETIL), ByVal entityDetail2 As List(Of S_DIGITAL_RI_38_RESIKOJATUH), ByVal entityDetail3 As List(Of S_DIGITAL_RI_38_MONITORING)) As Boolean
            Try
                If Not oConnection.GetConnection Then
                    InsertData = False
                    Exit Function
                End If

                Try
                    oConnection.db.S_DIGITAL_RI_38s.InsertOnSubmit(entity)
                    oConnection.db.S_DIGITAL_RI_38_DETILs.InsertAllOnSubmit(entityDetail)
                    oConnection.db.S_DIGITAL_RI_38_RESIKOJATUHs.InsertAllOnSubmit(entityDetail2)
                    oConnection.db.S_DIGITAL_RI_38_MONITORINGs.InsertAllOnSubmit(entityDetail3)
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
        Public Function UpdateData(ByVal entity As S_DIGITAL_RI_38, ByVal entityDetail As List(Of S_DIGITAL_RI_38_DETIL), ByVal entityDetail2 As List(Of S_DIGITAL_RI_38_RESIKOJATUH), ByVal entityDetail3 As List(Of S_DIGITAL_RI_38_MONITORING)) As Boolean
            Try
                If Not oConnection.GetConnection Then
                    UpdateData = False
                    Exit Function
                End If

                Dim ds = oConnection.db.S_DIGITAL_RI_38s.FirstOrDefault(Function(x) x.KDPENDAFTARAN = entity.KDPENDAFTARAN)
                Dim dsDetail = oConnection.db.S_DIGITAL_RI_38_DETILs.Where(Function(x) x.KDPENDAFTARAN = entity.KDPENDAFTARAN)
                Dim dsDetail2 = oConnection.db.S_DIGITAL_RI_38_RESIKOJATUHs.Where(Function(x) x.KDPENDAFTARAN = entity.KDPENDAFTARAN)
                Dim dsDetail3 = oConnection.db.S_DIGITAL_RI_38_MONITORINGs.Where(Function(x) x.KDPENDAFTARAN = entity.KDPENDAFTARAN)
                Try
                    oConnection.db.S_DIGITAL_RI_38s.DeleteOnSubmit(ds)
                    oConnection.db.S_DIGITAL_RI_38s.InsertOnSubmit(entity)

                    oConnection.db.S_DIGITAL_RI_38_DETILs.DeleteAllOnSubmit(dsDetail)
                    oConnection.db.S_DIGITAL_RI_38_DETILs.InsertAllOnSubmit(entityDetail)

                    oConnection.db.S_DIGITAL_RI_38_RESIKOJATUHs.DeleteAllOnSubmit(dsDetail2)
                    oConnection.db.S_DIGITAL_RI_38_RESIKOJATUHs.InsertAllOnSubmit(entityDetail2)

                    oConnection.db.S_DIGITAL_RI_38_MONITORINGs.DeleteAllOnSubmit(dsDetail3)
                    oConnection.db.S_DIGITAL_RI_38_MONITORINGs.InsertAllOnSubmit(entityDetail3)
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
        Public Function DeleteData(ByVal Parameter As Integer) As Boolean
            Try
                If Not oConnection.GetConnection Then
                    DeleteData = False
                    Exit Function
                End If

                Dim ds = oConnection.db.S_DIGITAL_RI_38s.FirstOrDefault(Function(x) x.KDPENDAFTARAN = Parameter)
                Dim dsDetail = oConnection.db.S_DIGITAL_RI_38_DETILs.Where(Function(x) x.KDPENDAFTARAN = Parameter)
                Dim dsDetail2 = oConnection.db.S_DIGITAL_RI_38_RESIKOJATUHs.Where(Function(x) x.KDPENDAFTARAN = Parameter)
                Dim dsDetail3 = oConnection.db.S_DIGITAL_RI_38_MONITORINGs.Where(Function(x) x.KDPENDAFTARAN = Parameter)

                Try
                    oConnection.db.S_DIGITAL_RI_38s.DeleteOnSubmit(ds)
                    oConnection.db.S_DIGITAL_RI_38_DETILs.DeleteAllOnSubmit(dsDetail)
                    oConnection.db.S_DIGITAL_RI_38_RESIKOJATUHs.DeleteAllOnSubmit(dsDetail2)
                    oConnection.db.S_DIGITAL_RI_38_MONITORINGs.DeleteAllOnSubmit(dsDetail3)
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
        Public Function UpdateCetak(ByVal sKDPENDAFTARAN As String) As Boolean
            Try
                If Not oConnection.GetConnection Then
                    UpdateCetak = False
                    Exit Function
                End If

                Try
                    Dim ds = oConnection.db.S_DIGITAL_RI_38s.FirstOrDefault(Function(x) x.KDPENDAFTARAN = sKDPENDAFTARAN)

                    ds.CETAK += 1

                    oConnection.db.SubmitChanges()

                Catch ex As Exception
                    oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                    Throw ex
                End Try

                UpdateCetak = True
            Catch ex As Exception
                UpdateCetak = False
                oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                Throw ex
            End Try
        End Function
    End Class
End Namespace