Imports DataAccess.My.Resources

Namespace Reference
    Public Class clsOrganizationBPJSRME
        Public oConnection As Setting.clsConnectionMain2 = Nothing
        Public oError As Setting.clsError = Nothing

        Public sMODUL As String = ""
        Public sREFERENCE As String = ""
        Public sSTATUS As String = ""
        Public sLASTNUMBER As Integer = 0

        Public Sub New(Optional ByVal sConnection As String = "")
            oConnection = New Setting.clsConnectionMain2
            oError = New Setting.clsError

            sMODUL = "ORGANIZATIONBPJSRME"
        End Sub
        Protected Overrides Sub Finalize()
            MyBase.Finalize()
            oConnection = Nothing
            GC.SuppressFinalize(Me)
        End Sub
        Public Function GetStructureHeader() As M_ORGANIZATION
            If Not oConnection.GetConnection() Then
                GetStructureHeader = Nothing
            End If
            GetStructureHeader = New M_ORGANIZATION
        End Function
        'Public Function GetData() As List(Of M_ORGANIZATION)
        '    If Not oConnection.GetConnection() Then
        '        GetData = Nothing
        '        Exit Function
        '    End If
        '    GetData = oConnection.db.M_ORGANIZATIONs.OrderBy(Function(x) x.KDORGANIZATION).ToList()
        'End Function
        Public Function GetData() As M_ORGANIZATION
            If Not oConnection.GetConnection() Then
                GetData = Nothing
                Exit Function
            End If
            GetData = oConnection.db.M_ORGANIZATIONs.FirstOrDefault()
        End Function
        Public Function InsertData(ByVal entity As M_ORGANIZATION) As Boolean
            Try
                If Not oConnection.GetConnection() Then
                    InsertData = False
                    Exit Function
                End If

                sREFERENCE = entity.KDORGANIZATION
                sSTATUS = "INSERT"

                Try
                    oConnection.db.M_ORGANIZATIONs.InsertOnSubmit(entity)
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
        Public Function UpdateData(ByVal entity As M_ORGANIZATION) As Boolean
            Try
                If Not oConnection.GetConnection() Then
                    UpdateData = False
                    Exit Function
                End If

                sREFERENCE = entity.KDORGANIZATION
                sSTATUS = "UPDATE"

                Dim ds = oConnection.db.M_ORGANIZATIONs.FirstOrDefault(Function(x) x.KDORGANIZATION = entity.KDORGANIZATION)

                Try
                    oConnection.db.M_ORGANIZATIONs.DeleteOnSubmit(ds)
                    oConnection.db.M_ORGANIZATIONs.InsertOnSubmit(entity)
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
        Public Function DeleteData(ByVal sKDORGANIZATION As String) As Boolean
            Try
                If Not oConnection.GetConnection() Then
                    DeleteData = False
                    Exit Function
                End If

                sREFERENCE = sKDORGANIZATION
                sSTATUS = "DELETE"

                Dim ds = oConnection.db.M_ORGANIZATIONs.FirstOrDefault(Function(x) x.KDORGANIZATION = sKDORGANIZATION)

                Try
                    oConnection.db.M_ORGANIZATIONs.DeleteOnSubmit(ds)
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