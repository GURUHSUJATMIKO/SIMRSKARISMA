Namespace Master
    Public Class clsTemplateLaporanOperasi
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

            sMODUL = ""
        End Sub
        Protected Overrides Sub Finalize()
            MyBase.Finalize()
            oConnection = Nothing
            GC.SuppressFinalize(Me)
        End Sub
        Public Function GetStructureHeader() As M_TEMPLATE_LAPORANOPERASI
            If Not oConnection.GetConnection Then
                GetStructureHeader = Nothing
            End If
            GetStructureHeader = New M_TEMPLATE_LAPORANOPERASI
        End Function
        Public Function GetDataAll() As List(Of M_TEMPLATE_LAPORANOPERASI)
            If Not oConnection.GetConnection Then
                GetDataAll = Nothing
                Exit Function
            End If
            GetDataAll = oConnection.db.M_TEMPLATE_LAPORANOPERASIs.OrderByDescending(Function(x) x.KDTLO).ToList()
        End Function
        Public Function GetData() As List(Of M_TEMPLATE_LAPORANOPERASI)
            If Not oConnection.GetConnection Then
                GetData = Nothing
                Exit Function
            End If
            GetData = oConnection.db.M_TEMPLATE_LAPORANOPERASIs.Where(Function(x) x.ISACTIVE = True).OrderByDescending(Function(x) x.KDTLO).ToList()
        End Function
        Public Function GetData(ByVal Parameter As String) As M_TEMPLATE_LAPORANOPERASI
            If Not oConnection.GetConnection Then
                GetData = Nothing
                Exit Function
            End If
            GetData = oConnection.db.M_TEMPLATE_LAPORANOPERASIs.FirstOrDefault(Function(x) x.KDTLO = Parameter)
        End Function
        Public Function InsertData(ByVal entity As M_TEMPLATE_LAPORANOPERASI) As Boolean
            Try
                If Not oConnection.GetConnection Then
                    InsertData = False
                    Exit Function
                End If

                sREFERENCE = entity.KDTLO
                sSTATUS = "INSERT"

                Try
                    oConnection.db.M_TEMPLATE_LAPORANOPERASIs.InsertOnSubmit(entity)
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
        Public Function UpdateData(ByVal entity As M_TEMPLATE_LAPORANOPERASI) As Boolean
            Try
                If Not oConnection.GetConnection Then
                    UpdateData = False
                    Exit Function
                End If

                sREFERENCE = entity.KDTLO
                sSTATUS = "UPDATE"


                Dim ds = oConnection.db.M_TEMPLATE_LAPORANOPERASIs.FirstOrDefault(Function(x) x.KDTLO = entity.KDTLO)

                Try
                    oConnection.db.M_TEMPLATE_LAPORANOPERASIs.DeleteOnSubmit(ds)
                    oConnection.db.M_TEMPLATE_LAPORANOPERASIs.InsertOnSubmit(entity)
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
        Public Function DeleteData(ByVal Parameter As Integer) As Boolean
            Try
                If Not oConnection.GetConnection Then
                    DeleteData = False
                    Exit Function
                End If

                sREFERENCE = Parameter
                sSTATUS = "DELETE"

                Dim ds = oConnection.db.M_TEMPLATE_LAPORANOPERASIs.FirstOrDefault(Function(x) x.KDTLO = Parameter)

                Try
                    oConnection.db.M_TEMPLATE_LAPORANOPERASIs.DeleteOnSubmit(ds)
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