Namespace Antrian
    Public Class clsSET_PANGGIL_ANTRIAN
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

            sMODUL = "ANTRIANSIMPAN"
        End Sub
        Protected Overrides Sub Finalize()
            MyBase.Finalize()
            oConnection = Nothing
            GC.SuppressFinalize(Me)
        End Sub
        Public Function GetStructureHeader() As SET_PANGGIL_ANTRIAN
            If Not oConnection.GetConnection() Then
                GetStructureHeader = Nothing
            End If
            GetStructureHeader = New SET_PANGGIL_ANTRIAN
        End Function
        Public Function GetData() As List(Of SET_PANGGIL_ANTRIAN)
            If Not oConnection.GetConnection() Then
                GetData = Nothing
                Exit Function
            End If
            GetData = oConnection.db.SET_PANGGIL_ANTRIANs.OrderBy(Function(x) x.KODE_POLI).ToList()
        End Function
        Public Function GetDataByIschekd() As List(Of SET_PANGGIL_ANTRIAN)
            If Not oConnection.GetConnection() Then
                GetDataByIschekd = Nothing
                Exit Function
            End If
            GetDataByIschekd = oConnection.db.SET_PANGGIL_ANTRIANs.OrderBy(Function(x) x.KODE_POLI).ToList()
        End Function
        'Public Function GetDataFolder() As SET_KONEKSI
        '    If Not oConnection.GetConnection() Then
        '        GetDataFolder = Nothing
        '        Exit Function
        '    End If
        '    GetDataFolder = oConnection.db.SET_KONEKSIs.FirstOrDefault(Function(x) x.NAME_DISPLAY = "VCLAIM")
        'End Function
        Public Function GetData(ByVal sKODE_POLI As String, sKODE_DOKTER As String) As SET_PANGGIL_ANTRIAN
            If Not oConnection.GetConnection() Then
                GetData = Nothing
                Exit Function
            End If
            GetData = oConnection.db.SET_PANGGIL_ANTRIANs.FirstOrDefault(Function(x) x.KODE_POLI = sKODE_POLI And x.KODE_DOKTER = sKODE_DOKTER)
        End Function
        Public Function InsertData(ByVal entity As SET_PANGGIL_ANTRIAN) As Boolean
            Try
                If Not oConnection.GetConnection() Then
                    InsertData = False
                    Exit Function
                End If

                sREFERENCE = entity.KODE_POLI
                sSTATUS = "INSERT"

                Try
                    oConnection.db.SET_PANGGIL_ANTRIANs.InsertOnSubmit(entity)
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
        Public Function UpdateData(ByVal entity As SET_PANGGIL_ANTRIAN) As Boolean
            Try
                If Not oConnection.GetConnection() Then
                    UpdateData = False
                    Exit Function
                End If

                sREFERENCE = entity.KODE_POLI
                sSTATUS = "UPDATE"

                Dim ds = oConnection.db.SET_PANGGIL_ANTRIANs.FirstOrDefault(Function(x) x.KODE_POLI = entity.KODE_POLI)

                Try
                    oConnection.db.SET_PANGGIL_ANTRIANs.DeleteOnSubmit(ds)
                    oConnection.db.SET_PANGGIL_ANTRIANs.InsertOnSubmit(entity)
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
        Public Function UpdateDataIsCheked(ByVal sKODE_POLI As Integer) As Boolean
            Try
                If Not oConnection.GetConnection Then
                    UpdateDataIsCheked = False
                    Exit Function
                End If

                UpdateDataIsCheked = True

                Dim ds = oConnection.db.SET_PANGGIL_ANTRIANs.FirstOrDefault(Function(x) x.KODE_POLI = sKODE_POLI)

                ds.ISPANGGIL = True

                oConnection.db.SubmitChanges()
            Catch ex As Exception
                UpdateDataIsCheked = False
                Throw ex
            End Try
        End Function
        Public Function UpdateIsPanggil(ByVal sKODE_POLI As String, ByVal sKODE_DOKTER As String) As String
            Try
                If Not oConnection.GetConnection Then
                    UpdateIsPanggil = False
                    Exit Function
                End If

                Dim ds = oConnection.db.SET_PANGGIL_ANTRIANs.FirstOrDefault(Function(x) x.KODE_POLI = sKODE_POLI And x.KODE_DOKTER = sKODE_DOKTER)

                ds.ISPANGGIL = Not ds.ISPANGGIL

                oConnection.db.SubmitChanges()

                UpdateIsPanggil = True
            Catch ex As Exception
                UpdateIsPanggil = False
                Throw ex
            End Try
        End Function
    End Class
End Namespace