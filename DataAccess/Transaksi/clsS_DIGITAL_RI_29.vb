Imports System.Data.SqlClient
Imports DataAccess.My.Resources

Namespace Digital
    Public Class clsS_DIGITAL_RI_29
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
        Public Function GetStructureHeader() As S_DIGITAL_RI_29
            If Not oConnection.GetConnection Then
                GetStructureHeader = Nothing
            End If
            GetStructureHeader = New S_DIGITAL_RI_29
        End Function
        Public Function GetStructureDetail() As S_DIGITAL_RI_29_DETIL
            If Not oConnection.GetConnection Then
                GetStructureDetail = Nothing
            End If
            GetStructureDetail = New S_DIGITAL_RI_29_DETIL
        End Function
        Public Function GetStructureDetailList() As List(Of S_DIGITAL_RI_29_DETIL)
            If Not oConnection.GetConnection Then
                GetStructureDetailList = Nothing
            End If
            GetStructureDetailList = New List(Of S_DIGITAL_RI_29_DETIL)
        End Function
        Public Function GetData() As List(Of S_DIGITAL_RI_29)
            If Not oConnection.GetConnection Then
                GetData = Nothing
                Exit Function
            End If
            GetData = oConnection.db.S_DIGITAL_RI_29s.OrderBy(Function(x) x.KDPENDAFTARAN).ToList()
        End Function
        Public Function GetData(ByVal sParameter As String) As S_DIGITAL_RI_29
            If Not oConnection.GetConnection Then
                GetData = Nothing
                Exit Function
            End If
            GetData = oConnection.db.S_DIGITAL_RI_29s.FirstOrDefault(Function(x) x.KDPENDAFTARAN = sParameter)
        End Function
        Public Function GetDataDetail() As List(Of S_DIGITAL_RI_29_DETIL)
            If Not oConnection.GetConnection Then
                GetDataDetail = Nothing
                Exit Function
            End If
            GetDataDetail = oConnection.db.S_DIGITAL_RI_29_DETILs.ToList()
        End Function
        Public Function GetDataDetail(ByVal Parameter As String) As List(Of S_DIGITAL_RI_29_DETIL)
            If Not oConnection.GetConnection Then
                GetDataDetail = Nothing
                Exit Function
            End If
            GetDataDetail = oConnection.db.S_DIGITAL_RI_29_DETILs.Where(Function(x) x.KDPENDAFTARAN = Parameter).OrderBy(Function(x) x.SEQ).ToList()
        End Function
        Public Function IsExist(ByVal sParameter As String) As Boolean
            If Not oConnection.GetConnection Then
                IsExist = False
                Exit Function
            End If

            Dim ds = oConnection.db.S_DIGITAL_RI_29s.FirstOrDefault(Function(x) x.KDPENDAFTARAN = sParameter)

            If ds IsNot Nothing Then
                IsExist = True
            Else
                IsExist = False
            End If
        End Function
        Public Function InsertData(ByVal entity As S_DIGITAL_RI_29, ByVal entityDetail As List(Of S_DIGITAL_RI_29_DETIL)) As Boolean
            Try
                If Not oConnection.GetConnection Then
                    InsertData = False
                    Exit Function
                End If

                Try
                    oConnection.db.S_DIGITAL_RI_29s.InsertOnSubmit(entity)
                    oConnection.db.S_DIGITAL_RI_29_DETILs.InsertAllOnSubmit(entityDetail)
                Catch ex As Exception
                    oError.InsertData("S_DIGITAL_RI_29", "INSERTDATA", ex.ToString, entity.KDPENDAFTARAN)
                    Throw ex
                End Try
                Try
                    oConnection.db.SubmitChanges()
                Catch ex As Exception
                    oError.InsertData("S_DIGITAL_RI_29", "INSERTDATA", ex.ToString, entity.KDPENDAFTARAN)
                    Throw ex
                End Try

                InsertData = True
            Catch ex As Exception
                InsertData = False
                oError.InsertData("S_DIGITAL_RI_29", "INSERTDATA", ex.ToString, entity.KDPENDAFTARAN)
                Throw ex
            End Try
        End Function
        Public Function UpdateData(ByVal entity As S_DIGITAL_RI_29, ByVal entityDetail As List(Of S_DIGITAL_RI_29_DETIL)) As Boolean
            Try
                If Not oConnection.GetConnection Then
                    UpdateData = False
                    Exit Function
                End If

                Dim ds = oConnection.db.S_DIGITAL_RI_29s.FirstOrDefault(Function(x) x.KDPENDAFTARAN = entity.KDPENDAFTARAN)
                Dim dsDetail = oConnection.db.S_DIGITAL_RI_29_DETILs.Where(Function(x) x.KDPENDAFTARAN = entity.KDPENDAFTARAN)
                Try
                    oConnection.db.S_DIGITAL_RI_29s.DeleteOnSubmit(ds)
                    oConnection.db.S_DIGITAL_RI_29s.InsertOnSubmit(entity)

                    oConnection.db.S_DIGITAL_RI_29_DETILs.DeleteAllOnSubmit(dsDetail)
                    oConnection.db.S_DIGITAL_RI_29_DETILs.InsertAllOnSubmit(entityDetail)
                Catch ex As Exception
                    oError.InsertData("S_DIGITAL_RI_29", "UPDATEDATA", ex.ToString, entity.KDPENDAFTARAN)
                    Throw ex
                End Try
                Try
                    oConnection.db.SubmitChanges()
                Catch ex As Exception
                    oError.InsertData("S_DIGITAL_RI_29", "UPDATEDATA", ex.ToString, entity.KDPENDAFTARAN)
                    Throw ex
                End Try

                UpdateData = True
            Catch ex As Exception
                UpdateData = False
                oError.InsertData("S_DIGITAL_RI_29", "UPDATEDATA", ex.ToString, entity.KDPENDAFTARAN)
                Throw ex
            End Try
        End Function
        Public Function DeleteData(ByVal Parameter As Integer) As Boolean
            Try
                If Not oConnection.GetConnection Then
                    DeleteData = False
                    Exit Function
                End If

                Dim ds = oConnection.db.S_DIGITAL_RI_29s.FirstOrDefault(Function(x) x.KDPENDAFTARAN = Parameter)
                Dim dsDetail = oConnection.db.S_DIGITAL_RI_29_DETILs.Where(Function(x) x.KDPENDAFTARAN = Parameter)

                Try
                    oConnection.db.S_DIGITAL_RI_29s.DeleteOnSubmit(ds)
                    oConnection.db.S_DIGITAL_RI_29_DETILs.DeleteAllOnSubmit(dsDetail)
                Catch ex As Exception
                    oError.InsertData("S_DIGITAL_RI_29", "DELETEDATA", ex.ToString, Parameter)
                    Throw ex
                End Try
                Try
                    oConnection.db.SubmitChanges()
                Catch ex As Exception
                    oError.InsertData("S_DIGITAL_RI_29", "DELETEDATA", ex.ToString, Parameter)
                    Throw ex
                End Try

                DeleteData = True
            Catch ex As Exception
                DeleteData = False
                oError.InsertData("S_DIGITAL_RI_29", "DELETEDATA", ex.ToString, Parameter)
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
                    Dim ds = oConnection.db.S_DIGITAL_RI_29s.FirstOrDefault(Function(x) x.KDPENDAFTARAN = sKDPENDAFTARAN)

                    ds.CETAK += 1

                    oConnection.db.SubmitChanges()

                Catch ex As Exception
                    oError.InsertData("S_DIGITAL_RI_29", "UPDATECETAK", ex.ToString, sKDPENDAFTARAN)
                    Throw ex
                End Try

                UpdateCetak = True
            Catch ex As Exception
                UpdateCetak = False
                oError.InsertData("S_DIGITAL_RI_29", "UPDATECETAK", ex.ToString, sKDPENDAFTARAN)
                Throw ex
            End Try
        End Function
    End Class
End Namespace