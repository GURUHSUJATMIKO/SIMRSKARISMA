Imports DataAccess.My.Resources

Namespace Reference
    Public Class clsTemplateResep
        Public oConnection As Setting.clsConnectionMain = Nothing
        Public oError As Setting.clsError = Nothing
        Public sMODUL As String = ""
        Public sREFERENCE As String = ""
        Public sSTATUS As String = ""
        Public sLASTNUMBER As Integer = 0

        Public oCounter As Setting.clsCounter = Nothing

        Public Sub New()
            oConnection = New Setting.clsConnectionMain
            oError = New Setting.clsError
            sMODUL = "REQTEMPLATE"

            oCounter = New Setting.clsCounter
        End Sub
        Protected Overrides Sub Finalize()
            MyBase.Finalize()
            oConnection = Nothing
            GC.SuppressFinalize(Me)
        End Sub
        Public Function GetStructureHeader() As S_REQ_RECIPE_TEMPLATE_H
            If Not oConnection.GetConnection Then
                GetStructureHeader = Nothing
            End If
            GetStructureHeader = New S_REQ_RECIPE_TEMPLATE_H
        End Function
        Public Function GetStructureDetail() As S_REQ_RECIPE_TEMPLATE_D
            If Not oConnection.GetConnection Then
                GetStructureDetail = Nothing
            End If
            GetStructureDetail = New S_REQ_RECIPE_TEMPLATE_D
        End Function
        Public Function GetStructureDetailList() As List(Of S_REQ_RECIPE_TEMPLATE_D)
            If Not oConnection.GetConnection Then
                GetStructureDetailList = Nothing
            End If
            GetStructureDetailList = New List(Of S_REQ_RECIPE_TEMPLATE_D)
        End Function
        Public Function GetData() As List(Of S_REQ_RECIPE_TEMPLATE_H)
            If Not oConnection.GetConnection Then
                GetData = Nothing
                Exit Function
            End If
            GetData = oConnection.db.S_REQ_RECIPE_TEMPLATE_Hs.OrderByDescending(Function(x) x.KDTEMPLATE).ToList()
        End Function
        Public Function GetData(ByVal Parameter As String) As S_REQ_RECIPE_TEMPLATE_H
            If Not oConnection.GetConnection Then
                GetData = Nothing
                Exit Function
            End If
            GetData = oConnection.db.S_REQ_RECIPE_TEMPLATE_Hs.FirstOrDefault(Function(x) x.KDTEMPLATE = Parameter)
        End Function
        Public Function GetDataDetail() As List(Of S_REQ_RECIPE_TEMPLATE_D)
            If Not oConnection.GetConnection Then
                GetDataDetail = Nothing
                Exit Function
            End If
            GetDataDetail = oConnection.db.S_REQ_RECIPE_TEMPLATE_Ds.ToList()
        End Function
        Public Function GetDataDetail(ByVal Parameter As String) As List(Of S_REQ_RECIPE_TEMPLATE_D)
            If Not oConnection.GetConnection Then
                GetDataDetail = Nothing
                Exit Function
            End If
            GetDataDetail = oConnection.db.S_REQ_RECIPE_TEMPLATE_Ds.Where(Function(x) x.KDTEMPLATE = Parameter).ToList()
        End Function
        Public Function InsertData(ByVal entity As S_REQ_RECIPE_TEMPLATE_H, ByVal entityDetail As List(Of S_REQ_RECIPE_TEMPLATE_D)) As Boolean
            Try
                If Not oConnection.GetConnection Then
                    InsertData = False
                    Exit Function
                End If

                sREFERENCE = entity.KDTEMPLATE
                sSTATUS = "INSERT"

                Try
                    sLASTNUMBER = oCounter.GetLastNumber(sMODUL, entity.DATECREATED)
                    If sLASTNUMBER = 0 Then
                        Try
                            oCounter.InsertData(sMODUL, entity.DATECREATED)
                            sLASTNUMBER = oCounter.GetLastNumber(sMODUL, entity.DATECREATED)
                        Catch ex As Exception
                            sLASTNUMBER = 0
                        End Try
                    End If

                    entity.KDTEMPLATE = AutoNumber(sMODUL, sLASTNUMBER + 1, entity.DATECREATED)

                    For Each iLoop In entityDetail
                        iLoop.KDTEMPLATE = entity.KDTEMPLATE
                    Next

                Catch ex As Exception
                    oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                    Throw ex
                End Try

                Try
                    oCounter.UpdateData(sMODUL, sLASTNUMBER + 1, Month(entity.DATECREATED), Year(entity.DATECREATED))
                Catch ex As Exception
                    oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                    Throw ex
                End Try

                Try
                    oConnection.db.S_REQ_RECIPE_TEMPLATE_Hs.InsertOnSubmit(entity)
                    oConnection.db.S_REQ_RECIPE_TEMPLATE_Ds.InsertAllOnSubmit(entityDetail)
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
                InsertData = ""
                oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                Throw ex
            End Try
        End Function
        Public Function UpdateData(ByVal entity As S_REQ_RECIPE_TEMPLATE_H, ByVal entityDetail As List(Of S_REQ_RECIPE_TEMPLATE_D)) As Boolean
            Try
                If Not oConnection.GetConnection Then
                    UpdateData = False
                    Exit Function
                End If

                sREFERENCE = entity.KDTEMPLATE
                sSTATUS = "UPDATE"

                Dim ds = oConnection.db.S_REQ_RECIPE_TEMPLATE_Hs.FirstOrDefault(Function(x) x.KDTEMPLATE = entity.KDTEMPLATE)

                Try
                    oConnection.db.S_REQ_RECIPE_TEMPLATE_Hs.DeleteOnSubmit(ds)
                    oConnection.db.S_REQ_RECIPE_TEMPLATE_Hs.InsertOnSubmit(entity)
                Catch ex As Exception
                    oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                    Throw ex
                End Try

                Dim dsDetail = oConnection.db.S_REQ_RECIPE_TEMPLATE_Ds.Where(Function(x) x.KDTEMPLATE = entity.KDTEMPLATE)

                Try
                    oConnection.db.S_REQ_RECIPE_TEMPLATE_Ds.DeleteAllOnSubmit(dsDetail)
                    oConnection.db.S_REQ_RECIPE_TEMPLATE_Ds.InsertAllOnSubmit(entityDetail)
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
                If Not oConnection.GetConnection Then
                    DeleteData = False
                    Exit Function
                End If

                sREFERENCE = Parameter
                sSTATUS = "DELETE"

                Dim ds = oConnection.db.S_REQ_RECIPE_TEMPLATE_Hs.FirstOrDefault(Function(x) x.KDTEMPLATE = Parameter)
                Dim dsDetail = oConnection.db.S_REQ_RECIPE_TEMPLATE_Ds.Where(Function(x) x.KDTEMPLATE = Parameter)

                Try
                    oConnection.db.S_REQ_RECIPE_TEMPLATE_Hs.DeleteOnSubmit(ds)
                Catch ex As Exception
                    oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                    Throw ex
                End Try
                Try
                    oConnection.db.S_REQ_RECIPE_TEMPLATE_Ds.DeleteAllOnSubmit(dsDetail)
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