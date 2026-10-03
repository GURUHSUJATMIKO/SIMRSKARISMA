Imports DataAccess.My.Resources

Namespace Digital
    Public Class clsCekGrouper
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

            sMODUL = "CEKGROUPER"
        End Sub
        Protected Overrides Sub Finalize()
            MyBase.Finalize()
            oConnection = Nothing
            GC.SuppressFinalize(Me)
        End Sub
        Public Function GetStructureHeader() As A_CEKGROUPING
            If Not oConnection.GetConnection() Then
                GetStructureHeader = Nothing
            End If
            GetStructureHeader = New A_CEKGROUPING
        End Function
        Public Function GetData(ByVal sKDCEK As String) As A_CEKGROUPING
            If Not oConnection.GetConnection() Then
                GetData = Nothing
                Exit Function
            End If
            GetData = oConnection.db.A_CEKGROUPINGs.FirstOrDefault(Function(x) x.KDCEK = sKDCEK)
        End Function
        Public Function InsertData(ByVal entity As A_CEKGROUPING) As Boolean
            Try
                If Not oConnection.GetConnection() Then
                    InsertData = False
                    Exit Function
                End If

                sREFERENCE = entity.KDCEK
                sSTATUS = "INSERT"

                Try
                    oConnection.db.A_CEKGROUPINGs.InsertOnSubmit(entity)
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
        Public Function UpdateData(ByVal entity As A_CEKGROUPING) As Boolean
            Try
                If Not oConnection.GetConnection() Then
                    UpdateData = False
                    Exit Function
                End If

                sREFERENCE = entity.KDCEK
                sSTATUS = "UPDATE"

                Dim ds = oConnection.db.A_CEKGROUPINGs.FirstOrDefault(Function(x) x.KDCEK = entity.KDCEK)

                Try
                    oConnection.db.A_CEKGROUPINGs.DeleteOnSubmit(ds)
                    oConnection.db.A_CEKGROUPINGs.InsertOnSubmit(entity)
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
        Public Function DeleteData(ByVal Parameter As String) As Boolean
            Try
                If Not oConnection.GetConnection() Then
                    DeleteData = False
                    Exit Function
                End If

                sREFERENCE = Parameter
                sSTATUS = "DELETE"

                Dim ds = oConnection.db.A_CEKGROUPINGs.FirstOrDefault(Function(x) x.KDCEK = Parameter)

                Try
                    oConnection.db.A_CEKGROUPINGs.DeleteOnSubmit(ds)
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
            Finally
                oConnection.db.Dispose()
            End Try
        End Function
        Public Function UpdateIscheked(ByVal sKDCEK As String, ByVal sISCHEKED As Boolean) As Boolean
            Try
                If Not oConnection.GetConnection Then
                    UpdateIscheked = False

                    Exit Function
                End If

                Dim ds = oConnection.db.A_CEKGROUPINGs.FirstOrDefault(Function(x) x.KDCEK = sKDCEK)

                If ds IsNot Nothing Then
                    ds.ISCHEKED = sISCHEKED
                    oConnection.db.SubmitChanges()
                End If

                UpdateIscheked = True
            Catch ex As Exception
                UpdateIscheked = False
                MsgBox(ex)
            End Try
        End Function
    End Class
End Namespace