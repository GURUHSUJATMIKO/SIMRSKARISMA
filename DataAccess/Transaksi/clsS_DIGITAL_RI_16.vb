Imports System.Data.SqlClient
Imports DataAccess.My.Resources

Namespace EMedrek
    Public Class clsS_DIGITAL_RI_16
        Public oConnection As Setting.clsConnectionMain = Nothing
        Public oError As Setting.clsError = Nothing
        Public Sub New()
            oConnection = New Setting.clsConnectionMain
            oError = New Setting.clsError
        End Sub
        Protected Overrides Sub Finalize()
            MyBase.Finalize()
            oConnection = Nothing
            GC.SuppressFinalize(Me)
        End Sub
        Public Function GetStructureHeader() As S_DIGITAL_RI_16
            If Not oConnection.GetConnection Then
                GetStructureHeader = Nothing
            End If
            GetStructureHeader = New S_DIGITAL_RI_16
        End Function
        Public Function GetData() As List(Of S_DIGITAL_RI_16)
            If Not oConnection.GetConnection Then
                GetData = Nothing
                Exit Function
            End If
            GetData = oConnection.db.S_DIGITAL_RI_16s.OrderBy(Function(x) x.KDPENDAFTARAN).ToList()
        End Function
        Public Function GetData(ByVal sParameter As String) As S_DIGITAL_RI_16
            If Not oConnection.GetConnection Then
                GetData = Nothing
                Exit Function
            End If
            GetData = oConnection.db.S_DIGITAL_RI_16s.FirstOrDefault(Function(x) x.KDPENDAFTARAN = sParameter)
        End Function
        Public Function IsExist(ByVal sParameter As String) As Boolean
            If Not oConnection.GetConnection Then
                IsExist = False
                Exit Function
            End If

            Dim ds = oConnection.db.S_DIGITAL_RI_16s.FirstOrDefault(Function(x) x.KDPENDAFTARAN = sParameter)

            If ds IsNot Nothing Then
                IsExist = True
            Else
                IsExist = False
            End If
        End Function
        Public Function InsertData(ByVal entity As S_DIGITAL_RI_16) As Boolean
            Try
                If Not oConnection.GetConnection Then
                    InsertData = False
                    Exit Function
                End If

                Try
                    oConnection.db.S_DIGITAL_RI_16s.InsertOnSubmit(entity)
                Catch ex As Exception
                    oError.InsertData("S_DIGITAL_RI_16", "INSERTDATA", ex.ToString, entity.KDPENDAFTARAN)
                    Throw ex
                End Try
                Try
                    oConnection.db.SubmitChanges()
                Catch ex As Exception
                    oError.InsertData("S_DIGITAL_RI_16", "INSERTDATA", ex.ToString, entity.KDPENDAFTARAN)
                    Throw ex
                End Try

                InsertData = True
            Catch ex As Exception
                InsertData = False
                oError.InsertData("S_DIGITAL_RI_16", "INSERTDATA", ex.ToString, entity.KDPENDAFTARAN)
                Throw ex
            End Try
        End Function
        Public Function UpdateData(ByVal entity As S_DIGITAL_RI_16) As Boolean
            Try
                If Not oConnection.GetConnection Then
                    UpdateData = False
                    Exit Function
                End If

                Dim ds = oConnection.db.S_DIGITAL_RI_16s.FirstOrDefault(Function(x) x.KDPENDAFTARAN = entity.KDPENDAFTARAN)
                Try
                    oConnection.db.S_DIGITAL_RI_16s.DeleteOnSubmit(ds)
                    oConnection.db.S_DIGITAL_RI_16s.InsertOnSubmit(entity)
                Catch ex As Exception
                    oError.InsertData("S_DIGITAL_RI_16", "UPDATEDATA", ex.ToString, entity.KDPENDAFTARAN)
                    Throw ex
                End Try
                Try
                    oConnection.db.SubmitChanges()
                Catch ex As Exception
                    oError.InsertData("S_DIGITAL_RI_16", "UPDATEDATA", ex.ToString, entity.KDPENDAFTARAN)
                    Throw ex
                End Try

                UpdateData = True
            Catch ex As Exception
                UpdateData = False
                oError.InsertData("S_DIGITAL_RI_16", "UPDATEDATA", ex.ToString, entity.KDPENDAFTARAN)
                Throw ex
            End Try
        End Function
        Public Function DeleteData(ByVal Parameter As Integer) As Boolean
            Try
                If Not oConnection.GetConnection Then
                    DeleteData = False
                    Exit Function
                End If

                Dim ds = oConnection.db.S_DIGITAL_RI_16s.FirstOrDefault(Function(x) x.KDPENDAFTARAN = Parameter)

                Try
                    oConnection.db.S_DIGITAL_RI_16s.DeleteOnSubmit(ds)
                Catch ex As Exception
                    oError.InsertData("S_DIGITAL_RI_16", "DELETEDATA", ex.ToString, Parameter)
                    Throw ex
                End Try
                Try
                    oConnection.db.SubmitChanges()
                Catch ex As Exception
                    oError.InsertData("S_DIGITAL_RI_16", "DELETEDATA", ex.ToString, Parameter)
                    Throw ex
                End Try

                DeleteData = True
            Catch ex As Exception
                DeleteData = False
                oError.InsertData("S_DIGITAL_RI_16", "DELETEDATA", ex.ToString, Parameter)
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
                    Dim ds = oConnection.db.S_DIGITAL_RI_16s.FirstOrDefault(Function(x) x.KDPENDAFTARAN = sKDPENDAFTARAN)

                    ds.CETAK += 1

                    oConnection.db.SubmitChanges()

                Catch ex As Exception
                    oError.InsertData("S_DIGITAL_RI_16", "UPDATECETAK", ex.ToString, sKDPENDAFTARAN)
                    Throw ex
                End Try

                UpdateCetak = True
            Catch ex As Exception
                UpdateCetak = False
                oError.InsertData("S_DIGITAL_RI_16", "UPDATECETAK", ex.ToString, sKDPENDAFTARAN)
                Throw ex
            End Try
        End Function
    End Class
End Namespace