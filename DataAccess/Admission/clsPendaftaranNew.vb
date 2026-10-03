Namespace Antrian
    Public Class clsPendaftaranNew
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

            sMODUL = "PENDAFTARAN_NEW"
        End Sub
        Protected Overrides Sub Finalize()
            MyBase.Finalize()
            oConnection = Nothing
            GC.SuppressFinalize(Me)
        End Sub
        Public Function GetStructureHeader() As S_PENDAFTARANNEW_H
            If Not oConnection.GetConnection() Then
                GetStructureHeader = Nothing
            End If
            GetStructureHeader = New S_PENDAFTARANNEW_H
        End Function
        Public Function GetData() As List(Of S_PENDAFTARANNEW_H)
            If Not oConnection.GetConnection() Then
                GetData = Nothing
                Exit Function
            End If
            GetData = oConnection.db.S_PENDAFTARANNEW_Hs.OrderBy(Function(x) x.KDPENDAFTARAN).ToList()
        End Function
        Public Function GetDataByIschekd() As List(Of S_PENDAFTARANNEW_H)
            If Not oConnection.GetConnection() Then
                GetDataByIschekd = Nothing
                Exit Function
            End If
            GetDataByIschekd = oConnection.db.S_PENDAFTARANNEW_Hs.OrderBy(Function(x) x.KDPENDAFTARAN).ToList()
        End Function
        Public Function GetData(ByVal sKDPENDAFTARAN As String) As S_PENDAFTARANNEW_H
            If Not oConnection.GetConnection() Then
                GetData = Nothing
                Exit Function
            End If
            GetData = oConnection.db.S_PENDAFTARANNEW_Hs.FirstOrDefault(Function(x) x.KDPENDAFTARAN = sKDPENDAFTARAN)
        End Function
        Public Function InsertData(ByVal entity As S_PENDAFTARANNEW_H) As Boolean
            Try
                If Not oConnection.GetConnection() Then
                    InsertData = False
                    Exit Function
                End If

                sREFERENCE = entity.KDPENDAFTARAN
                sSTATUS = "INSERT"

                Try
                    oConnection.db.S_PENDAFTARANNEW_Hs.InsertOnSubmit(entity)
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
        Public Function UpdateData(ByVal entity As S_PENDAFTARANNEW_H) As Boolean
            Try
                If Not oConnection.GetConnection() Then
                    UpdateData = False
                    Exit Function
                End If

                sREFERENCE = entity.KDPENDAFTARAN
                sSTATUS = "UPDATE"

                Dim ds = oConnection.db.S_PENDAFTARANNEW_Hs.FirstOrDefault(Function(x) x.KDPENDAFTARAN = entity.KDPENDAFTARAN)

                Try
                    oConnection.db.S_PENDAFTARANNEW_Hs.DeleteOnSubmit(ds)
                    oConnection.db.S_PENDAFTARANNEW_Hs.InsertOnSubmit(entity)
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
    End Class
End Namespace