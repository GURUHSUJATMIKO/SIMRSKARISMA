Imports DataAccess.My.Resources

Namespace Reference
    Public Class clsUnit
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

            sMODUL = "UOM"
        End Sub
        Protected Overrides Sub Finalize()
            MyBase.Finalize()
            oConnection = Nothing
            GC.SuppressFinalize(Me)
        End Sub
        Public Function GetStructureHeader() As M_UNIT
            If Not oConnection.GetConnection() Then
                GetStructureHeader = Nothing
            End If
            GetStructureHeader = New M_UNIT
        End Function
        Public Function GetData() As List(Of M_UNIT)
            If Not oConnection.GetConnection() Then
                GetData = Nothing
                Exit Function
            End If
            GetData = oConnection.db.M_UNITs.OrderBy(Function(x) x.MEMO).ToList()
        End Function
        Public Function GetData(ByVal sKDUNIT As String) As M_UNIT
            If Not oConnection.GetConnection() Then
                GetData = Nothing
                Exit Function
            End If
            GetData = oConnection.db.M_UNITs.FirstOrDefault(Function(x) x.KDUNIT = sKDUNIT)
        End Function
        Public Function GetDataByKdUser(ByVal sKDUSER As String) As M_UNIT
            If Not oConnection.GetConnection() Then
                GetDataByKdUser = Nothing
                Exit Function
            End If
            GetDataByKdUser = oConnection.db.M_UNITs.FirstOrDefault(Function(x) x.KDUSER = sKDUSER And x.ISACTIVE = True)
        End Function
        Public Function GetDataByName(ByVal sKDUNIT As String) As M_UNIT
            If Not oConnection.GetConnection() Then
                GetDataByName = Nothing
                Exit Function
            End If
            GetDataByName = oConnection.db.M_UNITs.FirstOrDefault(Function(x) x.MEMO = sKDUNIT)
        End Function
        Public Function GetDataSync() As List(Of M_UNIT)
            If Not oConnection.GetConnection() Then
                GetDataSync = Nothing
                Exit Function
            End If
            GetDataSync = oConnection.db.M_UNITs.OrderBy(Function(x) x.MEMO).ToList()
        End Function
        Public Function IsExist(ByVal sMEMO As String) As Boolean
            If Not oConnection.GetConnection() Then
                IsExist = False
                Exit Function
            End If

            Dim ds = oConnection.db.M_UNITs.FirstOrDefault(Function(x) x.MEMO = sMEMO)

            If ds IsNot Nothing Then
                IsExist = True
            Else
                IsExist = False
            End If
        End Function
        Public Function InsertData(ByVal entity As M_UNIT) As Boolean
            Try
                If Not oConnection.GetConnection() Then
                    InsertData = False
                    Exit Function
                End If

                sREFERENCE = entity.KDUNIT
                sSTATUS = "INSERT"

                If CheckDefault(0, entity.ISDEFAULT) = False Then
                    InsertData = False
                    Exit Function
                End If

                'Generate Auto Number
                Try
                    sLASTNUMBER = CInt(oConnection.db.M_UNITs.OrderByDescending(Function(x) x.KDUNIT).FirstOrDefault().KDUNIT.Remove(0, (sMODUL & " _ ").Length)) + 1
                Catch ex As Exception
                    sLASTNUMBER = 1
                End Try
                'End Generate

                Try
                    entity.KDUNIT = sMODUL & "_" & AutoNumberCode(sLASTNUMBER)
                    oConnection.db.M_UNITs.InsertOnSubmit(entity)
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
        Public Function UpdateData(ByVal entity As M_UNIT) As Boolean
            Try
                If Not oConnection.GetConnection() Then
                    UpdateData = False
                    Exit Function
                End If

                sREFERENCE = entity.KDUNIT
                sSTATUS = "UPDATE"

                If CheckDefault(0, entity.ISDEFAULT) = False Then
                    UpdateData = False
                    Exit Function
                End If

                Dim ds = oConnection.db.M_UNITs.FirstOrDefault(Function(x) x.KDUNIT = entity.KDUNIT)

                Try
                    oConnection.db.M_UNITs.DeleteOnSubmit(ds)
                    oConnection.db.M_UNITs.InsertOnSubmit(entity)
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
        Public Function DeleteData(ByVal sKDUNIT As String) As Boolean
            Try
                If Not oConnection.GetConnection() Then
                    DeleteData = False
                    Exit Function
                End If

                sREFERENCE = sKDUNIT
                sSTATUS = "DELETE"

                If CheckDefault(1, 0) = False Then
                    DeleteData = False
                    Exit Function
                End If

                Dim ds = oConnection.db.M_UNITs.FirstOrDefault(Function(x) x.KDUNIT = sKDUNIT)

                Try
                    oConnection.db.M_UNITs.DeleteOnSubmit(ds)
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
        Public Function CheckDefault(ByVal sState As Integer, ByVal sDefault As Boolean) As Boolean
            Try
                If Not oConnection.GetConnection() Then
                    CheckDefault = False
                    Exit Function
                End If


                If sState = 0 Then
                    Dim ds = oConnection.db.M_UNITs.Where(Function(x) x.ISDEFAULT = True)
                    If sDefault = True Then
                        For Each iLoop In ds
                            iLoop.ISDEFAULT = False
                        Next
                    Else
                        If ds.Count < 1 Then
                            MsgBox(Statement.CheckDefault, MsgBoxStyle.Exclamation)
                            CheckDefault = False
                            Exit Function
                        End If
                    End If
                Else
                    Dim ds = oConnection.db.M_UNITs.Where(Function(x) x.ISDEFAULT = True)

                    If ds.Count < 1 Then
                        MsgBox(Statement.CheckDefault, MsgBoxStyle.Exclamation)
                        CheckDefault = False
                        Exit Function
                    End If
                End If
                CheckDefault = True
            Catch ex As Exception
                CheckDefault = False
                Throw ex
            End Try
        End Function
    End Class
End Namespace