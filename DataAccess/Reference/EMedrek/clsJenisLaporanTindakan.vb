Imports DataAccess.My.Resources

Namespace Reference
    Public Class clsJenisLaporanTindakan
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

            sMODUL = "M_LAPORAN_TINDAKAN"
        End Sub
        Protected Overrides Sub Finalize()
            MyBase.Finalize()
            oConnection = Nothing
            GC.SuppressFinalize(Me)
        End Sub
        Public Function GetStructureHeader() As M_LAPORAN_TINDAKAN
            If Not oConnection.GetConnection() Then
                GetStructureHeader = Nothing
            End If
            GetStructureHeader = New M_LAPORAN_TINDAKAN
        End Function
        Public Function GetData() As List(Of M_LAPORAN_TINDAKAN)
            If Not oConnection.GetConnection() Then
                GetData = Nothing
                Exit Function
            End If
            GetData = oConnection.db.M_LAPORAN_TINDAKANs.OrderBy(Function(x) x.NAMALAPORAN).ToList()
        End Function
        Public Function GetData(ByVal sKDLAPORAN As Integer) As M_LAPORAN_TINDAKAN
            If Not oConnection.GetConnection() Then
                GetData = Nothing
                Exit Function
            End If
            GetData = oConnection.db.M_LAPORAN_TINDAKANs.FirstOrDefault(Function(x) x.KDLAPORAN = sKDLAPORAN)
        End Function
        Public Function GetDataSync() As List(Of M_LAPORAN_TINDAKAN)
            If Not oConnection.GetConnection() Then
                GetDataSync = Nothing
                Exit Function
            End If
            GetDataSync = oConnection.db.M_LAPORAN_TINDAKANs.OrderBy(Function(x) x.NAMALAPORAN).ToList()
        End Function
        Public Function IsExist(ByVal sNAMALAPORAN As String) As Boolean
            If Not oConnection.GetConnection() Then
                IsExist = False
                Exit Function
            End If

            Dim ds = oConnection.db.M_LAPORAN_TINDAKANs.FirstOrDefault(Function(x) x.NAMALAPORAN = sNAMALAPORAN)

            If ds IsNot Nothing Then
                IsExist = True
            Else
                IsExist = False
            End If
        End Function
        Public Function InsertData(ByVal entity As M_LAPORAN_TINDAKAN) As Boolean
            Try
                If Not oConnection.GetConnection() Then
                    InsertData = False
                    Exit Function
                End If

                sREFERENCE = entity.KDLAPORAN
                sSTATUS = "INSERT"

                Try
                    oConnection.db.M_LAPORAN_TINDAKANs.InsertOnSubmit(entity)
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
        Public Function UpdateData(ByVal entity As M_LAPORAN_TINDAKAN) As Boolean
            Try
                If Not oConnection.GetConnection() Then
                    UpdateData = False
                    Exit Function
                End If

                sREFERENCE = entity.KDLAPORAN
                sSTATUS = "UPDATE"

                Dim ds = oConnection.db.M_LAPORAN_TINDAKANs.FirstOrDefault(Function(x) x.KDLAPORAN = entity.KDLAPORAN)

                Try
                    oConnection.db.M_LAPORAN_TINDAKANs.DeleteOnSubmit(ds)
                    oConnection.db.M_LAPORAN_TINDAKANs.InsertOnSubmit(entity)
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
        Public Function DeleteData(ByVal sKDLAPORAN As Integer) As Boolean
            Try
                If Not oConnection.GetConnection() Then
                    DeleteData = False
                    Exit Function
                End If

                sREFERENCE = sKDLAPORAN
                sSTATUS = "DELETE"

                Dim ds = oConnection.db.M_LAPORAN_TINDAKANs.FirstOrDefault(Function(x) x.KDLAPORAN = sKDLAPORAN)

                Try
                    oConnection.db.M_LAPORAN_TINDAKANs.DeleteOnSubmit(ds)
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
    End Class
End Namespace