Imports System.Data.SqlClient
Imports DataAccess.My.Resources

Namespace Digital
    Public Class clsS_DIGITAL_LEMBARNEWS
        Public oConnection As Setting.clsConnectionMain = Nothing
        Public oError As Setting.clsError = Nothing
        Public oCounter As Setting.clsCounter = Nothing
        Public sLASTNUMBER As Integer = 0
        Public Sub New()
            oConnection = New Setting.clsConnectionMain
            oError = New Setting.clsError
        End Sub
        Protected Overrides Sub Finalize()
            MyBase.Finalize()
            oConnection = Nothing
            GC.SuppressFinalize(Me)
        End Sub
        Public Function GetStructureHeader() As S_DIGITAL_LEMBARNEWS_H
            If Not oConnection.GetConnection Then
                GetStructureHeader = Nothing
            End If
            GetStructureHeader = New S_DIGITAL_LEMBARNEWS_H
        End Function
        Public Function GetStructureDetail() As S_DIGITAL_LEMBARNEWS_D
            If Not oConnection.GetConnection Then
                GetStructureDetail = Nothing
            End If
            GetStructureDetail = New S_DIGITAL_LEMBARNEWS_D
        End Function
        Public Function GetStructureDetailList() As List(Of S_DIGITAL_LEMBARNEWS_D)
            If Not oConnection.GetConnection Then
                GetStructureDetailList = Nothing
            End If
            GetStructureDetailList = New List(Of S_DIGITAL_LEMBARNEWS_D)
        End Function
        Public Function GetData() As List(Of S_DIGITAL_LEMBARNEWS_H)
            If Not oConnection.GetConnection Then
                GetData = Nothing
                Exit Function
            End If
            GetData = oConnection.db.S_DIGITAL_LEMBARNEWS_Hs.OrderBy(Function(x) x.KDPENDAFTARAN).ToList()
        End Function
        Public Function GetData(ByVal sParameter As String) As S_DIGITAL_LEMBARNEWS_H
            If Not oConnection.GetConnection Then
                GetData = Nothing
                Exit Function
            End If
            GetData = oConnection.db.S_DIGITAL_LEMBARNEWS_Hs.FirstOrDefault(Function(x) x.KDPENDAFTARAN = sParameter)
        End Function
        Public Function GetData(ByVal sParameter As String, ByVal sParameter2 As String) As S_DIGITAL_LEMBARNEWS_H
            If Not oConnection.GetConnection Then
                GetData = Nothing
                Exit Function
            End If
            GetData = oConnection.db.S_DIGITAL_LEMBARNEWS_Hs.FirstOrDefault(Function(x) x.KDPENDAFTARAN = sParameter And x.KODE = sParameter2)
        End Function
        Public Function GetDataGabung(ByVal sParameter As String) As S_DIGITAL_LEMBARNEWS_H
            If Not oConnection.GetConnection Then
                GetDataGabung = Nothing
                Exit Function
            End If
            GetDataGabung = oConnection.db.S_DIGITAL_LEMBARNEWS_Hs.FirstOrDefault(Function(x) x.KDPENDAFTARAN & x.KODE = sParameter)
        End Function
        Public Function GetDataDetail() As List(Of S_DIGITAL_LEMBARNEWS_D)
            If Not oConnection.GetConnection Then
                GetDataDetail = Nothing
                Exit Function
            End If
            GetDataDetail = oConnection.db.S_DIGITAL_LEMBARNEWS_Ds.ToList()
        End Function
        Public Function GetDataDetail(ByVal Parameter As String) As List(Of S_DIGITAL_LEMBARNEWS_D)
            If Not oConnection.GetConnection Then
                GetDataDetail = Nothing
                Exit Function
            End If
            GetDataDetail = oConnection.db.S_DIGITAL_LEMBARNEWS_Ds.Where(Function(x) x.KDPENDAFTARAN = Parameter).OrderBy(Function(x) x.KDPENDAFTARAN).ToList()
        End Function
        Public Function GetDataDetail(ByVal Parameter As String, ByVal Parameter2 As String) As List(Of S_DIGITAL_LEMBARNEWS_D)
            If Not oConnection.GetConnection Then
                GetDataDetail = Nothing
                Exit Function
            End If
            GetDataDetail = oConnection.db.S_DIGITAL_LEMBARNEWS_Ds.Where(Function(x) x.KDPENDAFTARAN = Parameter And x.KODE = Parameter2).OrderBy(Function(x) x.SEQ).ToList()
        End Function

        Public Function GetDataDetailCount(ByVal Parameter As String) As Integer
            If Not oConnection.GetConnection Then
                GetDataDetailCount = Nothing
                Exit Function
            End If
            GetDataDetailCount = oConnection.db.S_DIGITAL_LEMBARNEWS_Hs.Where(Function(x) x.KDPENDAFTARAN = Parameter).Count()
        End Function
        Public Function IsExist(ByVal sParameter As String) As Boolean
            If Not oConnection.GetConnection Then
                IsExist = False
                Exit Function
            End If

            Dim ds = oConnection.db.S_DIGITAL_LEMBARNEWS_Hs.FirstOrDefault(Function(x) x.KDPENDAFTARAN = sParameter)

            If ds IsNot Nothing Then
                IsExist = True
            Else
                IsExist = False
            End If
        End Function
        Public Function InsertData(ByVal entity As S_DIGITAL_LEMBARNEWS_H, ByVal entityDetail As List(Of S_DIGITAL_LEMBARNEWS_D)) As Boolean
            Try
                If Not oConnection.GetConnection Then
                    InsertData = False
                    Exit Function
                End If

                Try
                    Dim KODE As Integer = GetDataDetailCount(entity.KDPENDAFTARAN)

                    entity.KODE = KODE
                    For Each iLoop In entityDetail
                        iLoop.KODE = KODE
                    Next

                    oConnection.db.S_DIGITAL_LEMBARNEWS_Hs.InsertOnSubmit(entity)
                    oConnection.db.S_DIGITAL_LEMBARNEWS_Ds.InsertAllOnSubmit(entityDetail)
                Catch ex As Exception
                    oError.InsertData("S_DIGITAL_LEMBARNEWS_H", "INSERTDATA", ex.ToString, entity.KDPENDAFTARAN)
                    Throw ex
                End Try
                Try
                    oConnection.db.SubmitChanges()
                Catch ex As Exception
                    oError.InsertData("S_DIGITAL_LEMBARNEWS_H", "INSERTDATA", ex.ToString, entity.KDPENDAFTARAN)
                    Throw ex
                End Try

                InsertData = True
            Catch ex As Exception
                InsertData = False
                oError.InsertData("S_DIGITAL_LEMBARNEWS_H", "INSERTDATA", ex.ToString, entity.KDPENDAFTARAN)
                Throw ex
            End Try
        End Function
        Public Function UpdateData(ByVal sKDPENDAFTARAN As String, ByVal sKODE As String, ByVal entity As S_DIGITAL_LEMBARNEWS_H, ByVal entityDetail As List(Of S_DIGITAL_LEMBARNEWS_D)) As Boolean
            Try
                If Not oConnection.GetConnection Then
                    UpdateData = False
                    Exit Function
                End If

                Dim ds = oConnection.db.S_DIGITAL_LEMBARNEWS_Hs.FirstOrDefault(Function(x) x.KDPENDAFTARAN = sKDPENDAFTARAN And x.KODE = sKODE)
                Dim dsDetail = oConnection.db.S_DIGITAL_LEMBARNEWS_Ds.Where(Function(x) x.KDPENDAFTARAN = sKDPENDAFTARAN And x.KODE = sKODE)
                Try
                    oConnection.db.S_DIGITAL_LEMBARNEWS_Hs.DeleteOnSubmit(ds)
                    oConnection.db.S_DIGITAL_LEMBARNEWS_Hs.InsertOnSubmit(entity)

                    oConnection.db.S_DIGITAL_LEMBARNEWS_Ds.DeleteAllOnSubmit(dsDetail)
                    oConnection.db.S_DIGITAL_LEMBARNEWS_Ds.InsertAllOnSubmit(entityDetail)
                Catch ex As Exception
                    oError.InsertData("S_DIGITAL_LEMBARNEWS_H", "UPDATEDATA", ex.ToString, entity.KDPENDAFTARAN)
                    Throw ex
                End Try
                Try
                    oConnection.db.SubmitChanges()
                Catch ex As Exception
                    oError.InsertData("S_DIGITAL_LEMBARNEWS_H", "UPDATEDATA", ex.ToString, entity.KDPENDAFTARAN)
                    Throw ex
                End Try

                UpdateData = True
            Catch ex As Exception
                UpdateData = False
                oError.InsertData("S_DIGITAL_LEMBARNEWS_H", "UPDATEDATA", ex.ToString, entity.KDPENDAFTARAN)
                Throw ex
            End Try
        End Function
        Public Function DeleteData(ByVal Parameter As String, ByVal sKode As String) As Boolean
            Try
                If Not oConnection.GetConnection Then
                    DeleteData = False
                    Exit Function
                End If

                Dim ds = oConnection.db.S_DIGITAL_LEMBARNEWS_Hs.FirstOrDefault(Function(x) x.KDPENDAFTARAN = Parameter And x.KODE = sKode)
                Dim dsDetail = oConnection.db.S_DIGITAL_LEMBARNEWS_Ds.Where(Function(x) x.KDPENDAFTARAN = Parameter And x.KODE = sKode)

                Try
                    oConnection.db.S_DIGITAL_LEMBARNEWS_Hs.DeleteOnSubmit(ds)
                    oConnection.db.S_DIGITAL_LEMBARNEWS_Ds.DeleteAllOnSubmit(dsDetail)
                Catch ex As Exception
                    oError.InsertData("S_DIGITAL_LEMBARNEWS_H", "DELETEDATA", ex.ToString, Parameter)
                    Throw ex
                End Try
                Try
                    oConnection.db.SubmitChanges()
                Catch ex As Exception
                    oError.InsertData("S_DIGITAL_LEMBARNEWS_H", "DELETEDATA", ex.ToString, Parameter)
                    Throw ex
                End Try

                DeleteData = True
            Catch ex As Exception
                DeleteData = False
                oError.InsertData("S_DIGITAL_LEMBARNEWS_H", "DELETEDATA", ex.ToString, Parameter)
                Throw ex
            End Try
        End Function
        Public Function UpdateCetak(ByVal sKDPENDAFTARAN As String, ByVal sKode As String) As Boolean
            Try
                If Not oConnection.GetConnection Then
                    UpdateCetak = False
                    Exit Function
                End If

                Try
                    Dim ds = oConnection.db.S_DIGITAL_LEMBARNEWS_Hs.FirstOrDefault(Function(x) x.KDPENDAFTARAN = sKDPENDAFTARAN And x.KODE = sKode)

                    ds.CETAK += 1

                    oConnection.db.SubmitChanges()

                Catch ex As Exception
                    oError.InsertData("S_DIGITAL_LEMBARNEWS_H", "UPDATECETAK", ex.ToString, sKDPENDAFTARAN)
                    Throw ex
                End Try

                UpdateCetak = True
            Catch ex As Exception
                UpdateCetak = False
                oError.InsertData("S_DIGITAL_LEMBARNEWS_H", "UPDATECETAK", ex.ToString, sKDPENDAFTARAN)
                Throw ex
            End Try
        End Function
    End Class
End Namespace