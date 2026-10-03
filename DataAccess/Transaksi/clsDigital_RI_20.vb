Imports DataAccess.My.Resources

Namespace Digital
    Public Class clsDigital_RI_20
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

            sMODUL = "DIGITAL_20"
        End Sub
        Protected Overrides Sub Finalize()
            MyBase.Finalize()
            oConnection = Nothing
            GC.SuppressFinalize(Me)
        End Sub
        Public Function GetStructureHeader() As S_DIGITAL_RI_20
            If Not oConnection.GetConnection() Then
                GetStructureHeader = Nothing
            End If
            GetStructureHeader = New S_DIGITAL_RI_20
        End Function
        Public Function GetData(ByVal sKDREG As String) As S_DIGITAL_RI_20
            If Not oConnection.GetConnection() Then
                GetData = Nothing
                Exit Function
            End If
            GetData = oConnection.db.S_DIGITAL_RI_20s.FirstOrDefault(Function(x) x.KDREG = sKDREG)
        End Function
        Public Function GetDataByKDREG(ByVal sKDREG As String) As List(Of S_DIGITAL_RI_20)
            If Not oConnection.GetConnection() Then
                GetDataByKDREG = Nothing
                Exit Function
            End If
            GetDataByKDREG = oConnection.db.S_DIGITAL_RI_20s.Where(Function(x) x.KDREG = sKDREG).OrderByDescending(Function(x) x.KDREG).ToList()
        End Function
        Public Function InsertData(ByVal entity As S_DIGITAL_RI_20) As Boolean
            Try
                If Not oConnection.GetConnection() Then
                    InsertData = False
                    Exit Function
                End If

                sREFERENCE = entity.KDREG
                sSTATUS = "INSERT"

                Try
                    oConnection.db.S_DIGITAL_RI_20s.InsertOnSubmit(entity)
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
        Public Function UpdateData(ByVal entity As S_DIGITAL_RI_20) As Boolean
            Try
                If Not oConnection.GetConnection() Then
                    UpdateData = False
                    Exit Function
                End If

                sREFERENCE = entity.KDREG
                sSTATUS = "UPDATE"

                Dim ds = oConnection.db.S_DIGITAL_RI_20s.FirstOrDefault(Function(x) x.KDREG = entity.KDREG)

                Try
                    oConnection.db.S_DIGITAL_RI_20s.DeleteOnSubmit(ds)
                    oConnection.db.S_DIGITAL_RI_20s.InsertOnSubmit(entity)
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

                Dim ds = oConnection.db.S_DIGITAL_RI_20s.FirstOrDefault(Function(x) x.KDREG = sKDREG)

                Try
                    oConnection.db.S_DIGITAL_RI_20s.DeleteOnSubmit(ds)
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