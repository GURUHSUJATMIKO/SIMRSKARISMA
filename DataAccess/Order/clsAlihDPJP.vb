Imports DataAccess.My.Resources

Namespace Digital
    Public Class clsAlihDPJP
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

            sMODUL = "ALIHDPJP"
        End Sub
        Protected Overrides Sub Finalize()
            MyBase.Finalize()
            oConnection = Nothing
            GC.SuppressFinalize(Me)
        End Sub
        Public Function GetStructureHeader() As S_DIGITAL_ALIHDPJP
            If Not oConnection.GetConnection() Then
                GetStructureHeader = Nothing
            End If
            GetStructureHeader = New S_DIGITAL_ALIHDPJP
        End Function
        Public Function GetData() As List(Of S_DIGITAL_ALIHDPJP)
            If Not oConnection.GetConnection() Then
                GetData = Nothing
                Exit Function
            End If
            GetData = oConnection.db.S_DIGITAL_ALIHDPJPs.OrderBy(Function(x) x.KDPENDAFTARAN).ToList()
        End Function
        Public Function GetData(ByVal sKDPENDAFTARAN As String) As S_DIGITAL_ALIHDPJP
            If Not oConnection.GetConnection() Then
                GetData = Nothing
                Exit Function
            End If
            GetData = oConnection.db.S_DIGITAL_ALIHDPJPs.FirstOrDefault(Function(x) x.KDPENDAFTARAN = sKDPENDAFTARAN)
        End Function
        Public Function InsertData(ByVal entity As S_DIGITAL_ALIHDPJP) As Boolean
            Try
                If Not oConnection.GetConnection() Then
                    InsertData = False
                    Exit Function
                End If

                sREFERENCE = entity.KDPENDAFTARAN
                sSTATUS = "INSERT"

                Try
                    oConnection.db.S_DIGITAL_ALIHDPJPs.InsertOnSubmit(entity)
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
        Public Function UpdateData(ByVal entity As S_DIGITAL_ALIHDPJP) As Boolean
            Try
                If Not oConnection.GetConnection() Then
                    UpdateData = False
                    Exit Function
                End If

                sREFERENCE = entity.KDPENDAFTARAN
                sSTATUS = "UPDATE"

                Dim ds = oConnection.db.S_DIGITAL_ALIHDPJPs.FirstOrDefault(Function(x) x.KDPENDAFTARAN = entity.KDPENDAFTARAN)

                Try
                    oConnection.db.S_DIGITAL_ALIHDPJPs.DeleteOnSubmit(ds)
                    oConnection.db.S_DIGITAL_ALIHDPJPs.InsertOnSubmit(entity)
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
        Public Function DeleteData(ByVal sKDPENDAFTARAN As String) As Boolean
            Try
                If Not oConnection.GetConnection() Then
                    DeleteData = False
                    Exit Function
                End If

                sREFERENCE = sKDPENDAFTARAN
                sSTATUS = "DELETE"

                Dim ds = oConnection.db.S_DIGITAL_ALIHDPJPs.FirstOrDefault(Function(x) x.KDPENDAFTARAN = sKDPENDAFTARAN)

                Try
                    oConnection.db.S_DIGITAL_ALIHDPJPs.DeleteOnSubmit(ds)
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