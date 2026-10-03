Imports DataAccess.My.Resources

Namespace Reference
    Public Class clsPPK
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

            sMODUL = "PPK"
        End Sub
        Protected Overrides Sub Finalize()
            MyBase.Finalize()
            oConnection = Nothing
            GC.SuppressFinalize(Me)
        End Sub
        Public Function GetStructureHeader() As M_PPK
            If Not oConnection.GetConnection() Then
                GetStructureHeader = Nothing
            End If
            GetStructureHeader = New M_PPK
        End Function
        Public Function GetData() As List(Of M_PPK)
            If Not oConnection.GetConnection() Then
                GetData = Nothing
                Exit Function
            End If
            GetData = oConnection.db.M_PPKs.OrderBy(Function(x) x.MEMO).ToList()
        End Function
        Public Function GetData(ByVal sKDPPK As String) As M_PPK
            If Not oConnection.GetConnection() Then
                GetData = Nothing
                Exit Function
            End If
            GetData = oConnection.db.M_PPKs.FirstOrDefault(Function(x) x.KDPPK = sKDPPK)
        End Function
        Public Function GetDataKodeFaskes(ByVal sKODEFASKES As String) As M_PPK
            If Not oConnection.GetConnection() Then
                GetDataKodeFaskes = Nothing
                Exit Function
            End If
            GetDataKodeFaskes = oConnection.db.M_PPKs.FirstOrDefault(Function(x) x.KODEFASKES = sKODEFASKES)
        End Function
        Public Function GetDataSync() As List(Of M_PPK)
            If Not oConnection.GetConnection() Then
                GetDataSync = Nothing
                Exit Function
            End If
            GetDataSync = oConnection.db.M_PPKs.OrderBy(Function(x) x.MEMO).ToList()
        End Function
        Public Function IsExist(ByVal sKDPPK As String) As Boolean
            If Not oConnection.GetConnection() Then
                IsExist = False
                Exit Function
            End If

            Dim ds = oConnection.db.M_PPKs.FirstOrDefault(Function(x) x.KDPPK = sKDPPK)

            If ds IsNot Nothing Then
                IsExist = True
            Else
                IsExist = False
            End If
        End Function
        Public Function IsExistKodeFaskes(ByVal sKODEFASKES As String) As Boolean
            If Not oConnection.GetConnection() Then
                IsExistKodeFaskes = False
                Exit Function
            End If

            Dim ds = oConnection.db.M_PPKs.FirstOrDefault(Function(x) x.KODEFASKES = sKODEFASKES)

            If ds IsNot Nothing Then
                IsExistKodeFaskes = True
            Else
                IsExistKodeFaskes = False
            End If
        End Function
        Public Function InsertData(ByVal entity As M_PPK) As Boolean
            Try
                If Not oConnection.GetConnection() Then
                    InsertData = False
                    Exit Function
                End If

                sREFERENCE = entity.KDPPK
                sSTATUS = "INSERT"

                If CheckDefault(0, entity.ISDEFAULT) = False Then
                    InsertData = False
                    Exit Function
                End If

                'Generate Auto Number
                Try
                    sLASTNUMBER = CInt(oConnection.db.M_PPKs.OrderByDescending(Function(x) x.KDPPK).FirstOrDefault().KDPPK.Remove(0, (sMODUL & " _ ").Length)) + 1
                Catch ex As Exception
                    sLASTNUMBER = 1
                End Try
                'End Generate

                Try
                    entity.KDPPK = sMODUL & "_" & AutoNumberCode(sLASTNUMBER)
                    oConnection.db.M_PPKs.InsertOnSubmit(entity)
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
        Public Function UpdateData(ByVal entity As M_PPK) As Boolean
            Try
                If Not oConnection.GetConnection() Then
                    UpdateData = False
                    Exit Function
                End If

                sREFERENCE = entity.KDPPK
                sSTATUS = "UPDATE"

                If CheckDefault(0, entity.ISDEFAULT) = False Then
                    UpdateData = False
                    Exit Function
                End If

                Dim ds = oConnection.db.M_PPKs.FirstOrDefault(Function(x) x.KDPPK = entity.KDPPK)

                Try
                    oConnection.db.M_PPKs.DeleteOnSubmit(ds)
                    oConnection.db.M_PPKs.InsertOnSubmit(entity)
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
        Public Function DeleteData(ByVal sKDPPK As String) As Boolean
            Try
                If Not oConnection.GetConnection() Then
                    DeleteData = False
                    Exit Function
                End If

                sREFERENCE = sKDPPK
                sSTATUS = "DELETE"

                If CheckDefault(1, 0) = False Then
                    DeleteData = False
                    Exit Function
                End If

                Dim ds = oConnection.db.M_PPKs.FirstOrDefault(Function(x) x.KDPPK = sKDPPK)

                Try
                    oConnection.db.M_PPKs.DeleteOnSubmit(ds)
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
                    Dim ds = oConnection.db.M_PPKs.Where(Function(x) x.ISDEFAULT = True)
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
                    Dim ds = oConnection.db.M_PPKs.Where(Function(x) x.ISDEFAULT = True)

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