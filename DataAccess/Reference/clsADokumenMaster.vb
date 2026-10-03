Imports DataAccess.My.Resources

Namespace Reference
    Public Class clsADokumenMaster
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

            sMODUL = "DOKUMENMASTER"
        End Sub
        Protected Overrides Sub Finalize()
            MyBase.Finalize()
            oConnection = Nothing
            GC.SuppressFinalize(Me)
        End Sub
        Public Function GetStructureHeader() As A_DOKUMEN_MASTER
            If Not oConnection.GetConnection() Then
                GetStructureHeader = Nothing
            End If
            GetStructureHeader = New A_DOKUMEN_MASTER
        End Function
        Public Function GetData() As List(Of A_DOKUMEN_MASTER)
            If Not oConnection.GetConnection() Then
                GetData = Nothing
                Exit Function
            End If
            GetData = oConnection.db.A_DOKUMEN_MASTERs.OrderBy(Function(x) x.MEMO).ToList()
        End Function
        Public Function GetData(ByVal sKDDOKUMENMASTER As String) As A_DOKUMEN_MASTER
            If Not oConnection.GetConnection() Then
                GetData = Nothing
                Exit Function
            End If
            GetData = oConnection.db.A_DOKUMEN_MASTERs.FirstOrDefault(Function(x) x.KDDOKUMENMASTER = sKDDOKUMENMASTER)
        End Function
        Public Function GetDataByName(ByVal sMEMO As String) As A_DOKUMEN_MASTER
            If Not oConnection.GetConnection() Then
                GetDataByName = Nothing
                Exit Function
            End If
            GetDataByName = oConnection.db.A_DOKUMEN_MASTERs.FirstOrDefault(Function(x) x.MEMO = sMEMO)
        End Function
        Public Function GetDataSync() As List(Of A_DOKUMEN_MASTER)
            If Not oConnection.GetConnection() Then
                GetDataSync = Nothing
                Exit Function
            End If
            GetDataSync = oConnection.db.A_DOKUMEN_MASTERs.OrderBy(Function(x) x.MEMO).ToList()
        End Function
        Public Function IsExist(ByVal sMEMO As String) As Boolean
            If Not oConnection.GetConnection() Then
                IsExist = False
                Exit Function
            End If

            Dim ds = oConnection.db.A_DOKUMEN_MASTERs.FirstOrDefault(Function(x) x.MEMO = sMEMO)

            If ds IsNot Nothing Then
                IsExist = True
            Else
                IsExist = False
            End If
        End Function
        Public Function InsertData(ByVal entity As A_DOKUMEN_MASTER) As Boolean
            Try
                If Not oConnection.GetConnection() Then
                    InsertData = False
                    Exit Function
                End If

                sREFERENCE = entity.KDDOKUMENMASTER
                sSTATUS = "INSERT"

                'Generate Auto Number
                Try
                    sLASTNUMBER = CInt(oConnection.db.A_DOKUMEN_MASTERs.OrderByDescending(Function(x) x.KDDOKUMENMASTER).FirstOrDefault().KDDOKUMENMASTER.Remove(0, (sMODUL & " _ ").Length)) + 1
                Catch ex As Exception
                    sLASTNUMBER = 1
                End Try
                'End Generate

                entity.KDDOKUMENMASTER = sMODUL & "_" & AutoNumberCode(sLASTNUMBER)

                Try
                    oConnection.db.A_DOKUMEN_MASTERs.InsertOnSubmit(entity)
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
        Public Function UpdateData(ByVal entity As A_DOKUMEN_MASTER) As Boolean
            Try
                If Not oConnection.GetConnection() Then
                    UpdateData = False
                    Exit Function
                End If

                sREFERENCE = entity.KDDOKUMENMASTER
                sSTATUS = "UPDATE"

                Dim ds = oConnection.db.A_DOKUMEN_MASTERs.FirstOrDefault(Function(x) x.KDDOKUMENMASTER = entity.KDDOKUMENMASTER)

                Try
                    oConnection.db.A_DOKUMEN_MASTERs.DeleteOnSubmit(ds)
                    oConnection.db.A_DOKUMEN_MASTERs.InsertOnSubmit(entity)
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
        Public Function DeleteData(ByVal sKDDOKUMENMASTER As String) As Boolean
            Try
                If Not oConnection.GetConnection() Then
                    DeleteData = False
                    Exit Function
                End If

                sREFERENCE = sKDDOKUMENMASTER
                sSTATUS = "DELETE"


                Dim ds = oConnection.db.A_DOKUMEN_MASTERs.FirstOrDefault(Function(x) x.KDDOKUMENMASTER = sKDDOKUMENMASTER)

                Try
                    oConnection.db.A_DOKUMEN_MASTERs.DeleteOnSubmit(ds)
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