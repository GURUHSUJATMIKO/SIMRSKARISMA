Namespace Master
    Public Class clsItemDiagnosaPerawat
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

            sMODUL = "ITEMOPERASI"
        End Sub
        Protected Overrides Sub Finalize()
            MyBase.Finalize()
            oConnection = Nothing
            GC.SuppressFinalize(Me)
        End Sub
        Public Function GetStructureHeader() As M_ITEM_DIAGNOSA_PERAWAT_H
            If Not oConnection.GetConnection Then
                GetStructureHeader = Nothing
            End If
            GetStructureHeader = New M_ITEM_DIAGNOSA_PERAWAT_H
        End Function
        Public Function GetStructureDetail() As M_ITEM_DIAGNOSA_PERAWAT_D
            If Not oConnection.GetConnection Then
                GetStructureDetail = Nothing
            End If
            GetStructureDetail = New M_ITEM_DIAGNOSA_PERAWAT_D
        End Function
        Public Function GetStructureDetailList() As List(Of M_ITEM_DIAGNOSA_PERAWAT_D)
            If Not oConnection.GetConnection Then
                GetStructureDetailList = Nothing
            End If
            GetStructureDetailList = New List(Of M_ITEM_DIAGNOSA_PERAWAT_D)
        End Function
        Public Function GetData() As List(Of M_ITEM_DIAGNOSA_PERAWAT_H)
            If Not oConnection.GetConnection Then
                GetData = Nothing
                Exit Function
            End If
            GetData = oConnection.db.M_ITEM_DIAGNOSA_PERAWAT_Hs.OrderByDescending(Function(x) x.KDITEMDIAGNOSAPERAWAT).ToList()
        End Function
        Public Function GetData(ByVal Parameter As String) As M_ITEM_DIAGNOSA_PERAWAT_H
            If Not oConnection.GetConnection Then
                GetData = Nothing
                Exit Function
            End If
            GetData = oConnection.db.M_ITEM_DIAGNOSA_PERAWAT_Hs.FirstOrDefault(Function(x) x.KDITEMDIAGNOSAPERAWAT = Parameter)
        End Function
        Public Function GetDataDetail() As List(Of M_ITEM_DIAGNOSA_PERAWAT_D)
            If Not oConnection.GetConnection Then
                GetDataDetail = Nothing
                Exit Function
            End If
            GetDataDetail = oConnection.db.M_ITEM_DIAGNOSA_PERAWAT_Ds.ToList()
        End Function
        Public Function GetDataDetail(ByVal Parameter As String) As List(Of M_ITEM_DIAGNOSA_PERAWAT_D)
            If Not oConnection.GetConnection Then
                GetDataDetail = Nothing
                Exit Function
            End If
            GetDataDetail = oConnection.db.M_ITEM_DIAGNOSA_PERAWAT_Ds.Where(Function(x) x.KDITEMDIAGNOSAPERAWAT = Parameter).ToList()
        End Function
        Public Function GetDataDetailByName(ByVal Parameter As String) As List(Of M_ITEM_DIAGNOSA_PERAWAT_H)
            If Not oConnection.GetConnection Then
                GetDataDetailByName = Nothing
                Exit Function
            End If
            GetDataDetailByName = oConnection.db.M_ITEM_DIAGNOSA_PERAWAT_Hs.Where(Function(x) x.DESCRIPTION.Contains(Parameter)).ToList()
        End Function
        Public Function InsertData(ByVal entity As M_ITEM_DIAGNOSA_PERAWAT_H, ByVal entityDetail As List(Of M_ITEM_DIAGNOSA_PERAWAT_D)) As Boolean
            Try
                If Not oConnection.GetConnection Then
                    InsertData = False
                    Exit Function
                End If

                sREFERENCE = entity.KDITEMDIAGNOSAPERAWAT
                sSTATUS = "INSERT"

                'Generate Auto Number

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

                    entity.KDITEMDIAGNOSAPERAWAT = AutoNumber(sMODUL, sLASTNUMBER + 1, entity.DATECREATED)
                    For Each iLoop In entityDetail
                        iLoop.KDITEMDIAGNOSAPERAWAT = entity.KDITEMDIAGNOSAPERAWAT
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

                'End Generate

                Try
                    oConnection.db.M_ITEM_DIAGNOSA_PERAWAT_Hs.InsertOnSubmit(entity)
                    oConnection.db.M_ITEM_DIAGNOSA_PERAWAT_Ds.InsertAllOnSubmit(entityDetail)
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
        Public Function UpdateData(ByVal entity As M_ITEM_DIAGNOSA_PERAWAT_H, ByVal entityDetail As List(Of M_ITEM_DIAGNOSA_PERAWAT_D)) As Boolean
            Try
                If Not oConnection.GetConnection Then
                    UpdateData = False
                    Exit Function
                End If

                sREFERENCE = entity.KDITEMDIAGNOSAPERAWAT
                sSTATUS = "UPDATE"


                Dim ds = oConnection.db.M_ITEM_DIAGNOSA_PERAWAT_Hs.FirstOrDefault(Function(x) x.KDITEMDIAGNOSAPERAWAT = entity.KDITEMDIAGNOSAPERAWAT)

                Try
                    oConnection.db.M_ITEM_DIAGNOSA_PERAWAT_Hs.DeleteOnSubmit(ds)
                    oConnection.db.M_ITEM_DIAGNOSA_PERAWAT_Hs.InsertOnSubmit(entity)
                Catch ex As Exception
                    oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                    Throw ex
                End Try

                Dim dsDetail = oConnection.db.M_ITEM_DIAGNOSA_PERAWAT_Ds.Where(Function(x) x.KDITEMDIAGNOSAPERAWAT = entity.KDITEMDIAGNOSAPERAWAT)


                Try
                    oConnection.db.M_ITEM_DIAGNOSA_PERAWAT_Ds.DeleteAllOnSubmit(dsDetail)
                    oConnection.db.M_ITEM_DIAGNOSA_PERAWAT_Ds.InsertAllOnSubmit(entityDetail)
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


                Dim ds = oConnection.db.M_ITEM_DIAGNOSA_PERAWAT_Hs.FirstOrDefault(Function(x) x.KDITEMDIAGNOSAPERAWAT = Parameter)
                Dim dsDetail = oConnection.db.M_ITEM_DIAGNOSA_PERAWAT_Ds.Where(Function(x) x.KDITEMDIAGNOSAPERAWAT = Parameter)

                Try
                    oConnection.db.M_ITEM_DIAGNOSA_PERAWAT_Hs.DeleteOnSubmit(ds)
                Catch ex As Exception
                    oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                    Throw ex
                End Try
                Try
                    oConnection.db.M_ITEM_DIAGNOSA_PERAWAT_Ds.DeleteAllOnSubmit(dsDetail)
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