Namespace Pendaftaran2
    Public Class clsPendaftaranWaktuIGD
        Public oConnection As Setting.clsConnectionAdmision = Nothing
        Public oError As Setting.clsError = Nothing
        Public Sub New()
            oConnection = New Setting.clsConnectionAdmision
            oError = New Setting.clsError
        End Sub
        Protected Overrides Sub Finalize()
            MyBase.Finalize()
            oConnection = Nothing
            GC.SuppressFinalize(Me)
        End Sub
        Public Function GetStructureHeader() As S_PENDAFTARAN_WAKTUIGD
            If Not oConnection.GetConnection Then
                GetStructureHeader = Nothing
            End If
            GetStructureHeader = New S_PENDAFTARAN_WAKTUIGD
        End Function
        Public Function GetData(ByVal sParameter As String) As S_PENDAFTARAN_WAKTUIGD
            If Not oConnection.GetConnection Then
                GetData = Nothing
                Exit Function
            End If
            GetData = oConnection.db.S_PENDAFTARAN_WAKTUIGDs.FirstOrDefault(Function(x) x.KDREG = sParameter)
        End Function
        Public Function GetDataAntrianPoli(ByVal sParameter As String, ByVal TaskId As Integer) As Z_ANTRIAN_ADMISI
            If Not oConnection.GetConnection Then
                GetDataAntrianPoli = Nothing
                Exit Function
            End If
            GetDataAntrianPoli = oConnection.db.Z_ANTRIAN_ADMISIs.FirstOrDefault(Function(x) x.KODEBOOKING = sParameter And x.TAKSID = TaskId)
        End Function
        Public Function InsertData(ByVal entity As S_PENDAFTARAN_WAKTUIGD) As Boolean
            Try
                If Not oConnection.GetConnection Then
                    InsertData = False
                    Exit Function
                End If

                Try
                    oConnection.db.S_PENDAFTARAN_WAKTUIGDs.InsertOnSubmit(entity)
                Catch ex As Exception
                    'oError.InsertData("PENDAFTARAN WAKTUIGD", "INSERTDATA", ex.ToString)
                    Throw ex
                End Try
                Try
                    oConnection.db.SubmitChanges()
                Catch ex As Exception
                    'oError.InsertData("PENDAFTARAN WAKTUIGD", "INSERTDATA", ex.ToString)
                    Throw ex
                End Try

                InsertData = True
            Catch ex As Exception
                InsertData = False
                'oError.InsertData("PENDAFTARAN WAKTUIGD", "INSERTDATA", ex.ToString)
                Throw ex
            End Try
        End Function
        Public Function UpdateData(ByVal entity As S_PENDAFTARAN_WAKTUIGD) As Boolean
            Try
                If Not oConnection.GetConnection Then
                    UpdateData = False
                    Exit Function
                End If

                Dim ds = oConnection.db.S_PENDAFTARAN_WAKTUIGDs.FirstOrDefault(Function(x) x.KDREG = entity.KDREG)

                Try
                    oConnection.db.S_PENDAFTARAN_WAKTUIGDs.DeleteOnSubmit(ds)
                    oConnection.db.S_PENDAFTARAN_WAKTUIGDs.InsertOnSubmit(entity)
                Catch ex As Exception
                    'oError.InsertData("PENDAFTARAN WAKTUIGD", "UPDATEDATA", ex.ToString)
                    Throw ex
                End Try
                Try
                    oConnection.db.SubmitChanges()
                Catch ex As Exception
                    'oError.InsertData("PENDAFTARAN WAKTUIGD", "UPDATEDATA", ex.ToString)
                    Throw ex
                End Try

                UpdateData = True
            Catch ex As Exception
                UpdateData = False
                'oError.InsertData("PENDAFTARAN WAKTUIGD", "UPDATEDATA", ex.ToString)
                Throw ex
            End Try
        End Function
        Public Function DeleteData(ByVal Parameter As Integer) As Boolean
            Try
                If Not oConnection.GetConnection Then
                    DeleteData = False
                    Exit Function
                End If

                Dim ds = oConnection.db.S_PENDAFTARAN_WAKTUIGDs.FirstOrDefault(Function(x) x.KDREG = Parameter)

                Try
                    oConnection.db.S_PENDAFTARAN_WAKTUIGDs.DeleteOnSubmit(ds)
                Catch ex As Exception
                    'oError.InsertData("PENDAFTARAN WAKTUIGD", "DELETEDATA", ex.ToString)
                    Throw ex
                End Try
                Try
                    oConnection.db.SubmitChanges()
                Catch ex As Exception
                    'oError.InsertData("PENDAFTARAN WAKTUIGD", "DELETEDATA", ex.ToString)
                    Throw ex
                End Try

                DeleteData = True
            Catch ex As Exception
                DeleteData = False
                'oError.InsertData("PENDAFTARAN WAKTUIGD", "DELETEDATA", ex.ToString)
                Throw ex
            End Try
        End Function
    End Class
End Namespace