Imports DataAccess.My.Resources

Namespace Reference
    Public Class clsPendaftaranBPJSRME
        Public oConnection As Setting.clsConnectionAdmision = Nothing
        Public oError As Setting.clsError = Nothing

        Public sMODUL As String = ""
        Public sREFERENCE As String = ""
        Public sSTATUS As String = ""
        Public sLASTNUMBER As Integer = 0

        Public Sub New(Optional ByVal sConnection As String = "")
            oConnection = New Setting.clsConnectionAdmision
            oError = New Setting.clsError

            sMODUL = "CUSTOMERBPJSRME"
        End Sub
        Protected Overrides Sub Finalize()
            MyBase.Finalize()
            oConnection = Nothing
            GC.SuppressFinalize(Me)
        End Sub
        Public Function GetStructureHeader() As S_PENDAFTARAN_BPJSRME
            If Not oConnection.GetConnection() Then
                GetStructureHeader = Nothing
            End If
            GetStructureHeader = New S_PENDAFTARAN_BPJSRME
        End Function
        Public Function GetData() As List(Of S_PENDAFTARAN_BPJSRME)
            If Not oConnection.GetConnection() Then
                GetData = Nothing
                Exit Function
            End If
            GetData = oConnection.db.S_PENDAFTARAN_BPJSRMEs.OrderBy(Function(x) x.KDREG).ToList()
        End Function
        Public Function GetData(ByVal sKDREG As String) As S_PENDAFTARAN_BPJSRME
            If Not oConnection.GetConnection() Then
                GetData = Nothing
                Exit Function
            End If
            GetData = oConnection.db.S_PENDAFTARAN_BPJSRMEs.FirstOrDefault(Function(x) x.KDREG = sKDREG)
        End Function
        Public Function GetDataPendaftaranByRegister(ByVal Parameter As String) As S_PENDAFTARAN_H
            If Not oConnection.GetConnection() Then
                GetDataPendaftaranByRegister = Nothing
                Exit Function
            End If
            GetDataPendaftaranByRegister = oConnection.db.S_PENDAFTARAN_Hs.FirstOrDefault(Function(x) x.KDREG = Parameter)
        End Function
        Public Function InsertData(ByVal entity As S_PENDAFTARAN_BPJSRME) As Boolean
            Try
                If Not oConnection.GetConnection() Then
                    InsertData = False
                    Exit Function
                End If

                sREFERENCE = entity.KDREG
                sSTATUS = "INSERT"

                Try
                    oConnection.db.S_PENDAFTARAN_BPJSRMEs.InsertOnSubmit(entity)
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
        Public Function UpdateData(ByVal entity As S_PENDAFTARAN_BPJSRME) As Boolean
            Try
                If Not oConnection.GetConnection() Then
                    UpdateData = False
                    Exit Function
                End If

                sREFERENCE = entity.KDREG
                sSTATUS = "UPDATE"

                Dim ds = oConnection.db.S_PENDAFTARAN_BPJSRMEs.FirstOrDefault(Function(x) x.KDREG = entity.KDREG)

                Try
                    oConnection.db.S_PENDAFTARAN_BPJSRMEs.DeleteOnSubmit(ds)
                    oConnection.db.S_PENDAFTARAN_BPJSRMEs.InsertOnSubmit(entity)
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
        Public Function DeleteData(ByVal sKDREG As String) As Boolean
            Try
                If Not oConnection.GetConnection() Then
                    DeleteData = False
                    Exit Function
                End If

                sREFERENCE = sKDREG
                sSTATUS = "DELETE"

                Dim ds = oConnection.db.S_PENDAFTARAN_BPJSRMEs.FirstOrDefault(Function(x) x.KDREG = sKDREG)

                Try
                    oConnection.db.S_PENDAFTARAN_BPJSRMEs.DeleteOnSubmit(ds)
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