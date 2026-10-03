Imports DataAccess.My.Resources

Namespace Digital
    Public Class clsSuratKeteranganSakit
        Public oConnection As Setting.clsConnectionMain = Nothing
        Public oError As Setting.clsError = Nothing

        Public sMODUL As String = ""
        Public sREFERENCE As String = ""
        Public sSTATUS As String = ""
        Public sLASTNUMBER As Integer = 0
        Public oCounter As Setting.clsCounter = Nothing

        Public Sub New(Optional ByVal sConnection As String = "")
            If sConnection = "" Then
                oConnection = New Setting.clsConnectionMain
                oError = New Setting.clsError
                oCounter = New Setting.clsCounter
            Else
                oConnection = New Setting.clsConnectionMain("TAX")
                oError = New Setting.clsError("TAX")
                oCounter = New Setting.clsCounter
            End If

            sMODUL = "SUKET"
        End Sub
        Protected Overrides Sub Finalize()
            MyBase.Finalize()
            oConnection = Nothing
            GC.SuppressFinalize(Me)
        End Sub
        Public Function GetStructureHeader() As S_KETERANGANSAKIT
            If Not oConnection.GetConnection() Then
                GetStructureHeader = Nothing
            End If
            GetStructureHeader = New S_KETERANGANSAKIT
        End Function
        Public Function GetData(ByVal sKDPENDAFTARAN As String) As S_KETERANGANSAKIT
            If Not oConnection.GetConnection() Then
                GetData = Nothing
                Exit Function
            End If
            GetData = oConnection.db.S_KETERANGANSAKITs.FirstOrDefault(Function(x) x.KDREG = sKDPENDAFTARAN)
        End Function
        Public Function GetDataByRM(ByVal kdcustomer As String) As List(Of S_KETERANGANSAKIT)
            If Not oConnection.GetConnection() Then
                GetDataByRM = Nothing
                Exit Function
            End If
            GetDataByRM = oConnection.db.S_KETERANGANSAKITs.Where(Function(x) x.KDCUSTOMER = kdcustomer).OrderByDescending(Function(x) x.KDREG).ToList()
        End Function
        Public Function InsertData(ByVal entity As S_KETERANGANSAKIT) As Boolean
            Try
                If Not oConnection.GetConnection() Then
                    InsertData = False
                    Exit Function
                End If

                sREFERENCE = entity.KDREG
                sSTATUS = "INSERT"

                Try
                    oConnection.db.S_KETERANGANSAKITs.InsertOnSubmit(entity)
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
                'oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                Throw ex
            End Try
        End Function
        Public Function UpdateData(ByVal entity As S_KETERANGANSAKIT) As Boolean
            Try
                If Not oConnection.GetConnection() Then
                    UpdateData = False
                    Exit Function
                End If

                sREFERENCE = entity.KDREG
                sSTATUS = "UPDATE"

                Dim ds = oConnection.db.S_KETERANGANSAKITs.FirstOrDefault(Function(x) x.KDREG = entity.KDREG)

                Try
                    oConnection.db.S_KETERANGANSAKITs.DeleteOnSubmit(ds)
                    oConnection.db.S_KETERANGANSAKITs.InsertOnSubmit(entity)
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
                'oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                Throw ex
            End Try
        End Function
    End Class
End Namespace