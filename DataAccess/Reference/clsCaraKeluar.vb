Imports DataAccess.My.Resources

Namespace Reference
    Public Class clsCaraKeluar
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

            sMODUL = "CARAKELUAR"
        End Sub
        Protected Overrides Sub Finalize()
            MyBase.Finalize()
            oConnection = Nothing
            GC.SuppressFinalize(Me)
        End Sub
        Public Function GetStructureHeader() As M_CARAKELUAR
            If Not oConnection.GetConnection() Then
                GetStructureHeader = Nothing
            End If
            GetStructureHeader = New M_CARAKELUAR
        End Function
        Public Function GetData() As List(Of M_CARAKELUAR)
            If Not oConnection.GetConnection() Then
                GetData = Nothing
                Exit Function
            End If
            GetData = oConnection.db.M_CARAKELUARs.OrderBy(Function(x) x.MEMO).ToList()
        End Function
        Public Function GetData(ByVal sKDCARAKELUAR As String) As M_CARAKELUAR
            If Not oConnection.GetConnection() Then
                GetData = Nothing
                Exit Function
            End If
            GetData = oConnection.db.M_CARAKELUARs.FirstOrDefault(Function(x) x.KDCARAKELUAR = sKDCARAKELUAR)
        End Function
        Public Function GetDataSync() As List(Of M_CARAKELUAR)
            If Not oConnection.GetConnection() Then
                GetDataSync = Nothing
                Exit Function
            End If
            GetDataSync = oConnection.db.M_CARAKELUARs.OrderBy(Function(x) x.MEMO).ToList()
        End Function
        Public Function IsExist(ByVal sMEMO As String) As Boolean
            If Not oConnection.GetConnection() Then
                IsExist = False
                Exit Function
            End If

            Dim ds = oConnection.db.M_CARAKELUARs.FirstOrDefault(Function(x) x.MEMO = sMEMO)

            If ds IsNot Nothing Then
                IsExist = True
            Else
                IsExist = False
            End If
        End Function
        Public Function InsertData(ByVal entity As M_CARAKELUAR) As Boolean
            Try
                If Not oConnection.GetConnection() Then
                    InsertData = False
                    Exit Function
                End If

                sREFERENCE = entity.KDCARAKELUAR
                sSTATUS = "INSERT"

                If CheckDefault(0, entity.ISDEFAULT) = False Then
                    InsertData = False
                    Exit Function
                End If

                ''Generate Auto Number
                'Try
                '    sLASTNUMBER = CInt(oConnection.db.M_CARAKELUARs.OrderByDescending(Function(x) x.KDCARAKELUAR).FirstOrDefault().KDCARAKELUAR.Remove(0, (sMODUL & " _ ").Length)) + 1
                'Catch ex As Exception
                '    sLASTNUMBER = 1
                'End Try
                ''End Generate

                Try
                    'entity.KDCARAKELUAR = sMODUL & "_" & AutoNumberCode(sLASTNUMBER)
                    oConnection.db.M_CARAKELUARs.InsertOnSubmit(entity)
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
        Public Function UpdateData(ByVal entity As M_CARAKELUAR) As Boolean
            Try
                If Not oConnection.GetConnection() Then
                    UpdateData = False
                    Exit Function
                End If

                sREFERENCE = entity.KDCARAKELUAR
                sSTATUS = "UPDATE"

                If CheckDefault(0, entity.ISDEFAULT) = False Then
                    UpdateData = False
                    Exit Function
                End If

                Dim ds = oConnection.db.M_CARAKELUARs.FirstOrDefault(Function(x) x.KDCARAKELUAR = entity.KDCARAKELUAR)

                Try
                    oConnection.db.M_CARAKELUARs.DeleteOnSubmit(ds)
                    oConnection.db.M_CARAKELUARs.InsertOnSubmit(entity)
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
        Public Function DeleteData(ByVal sKDCARAKELUAR As String) As Boolean
            Try
                If Not oConnection.GetConnection() Then
                    DeleteData = False
                    Exit Function
                End If

                sREFERENCE = sKDCARAKELUAR
                sSTATUS = "DELETE"

                If CheckDefault(1, 0) = False Then
                    DeleteData = False
                    Exit Function
                End If

                Dim ds = oConnection.db.M_CARAKELUARs.FirstOrDefault(Function(x) x.KDCARAKELUAR = sKDCARAKELUAR)

                Try
                    oConnection.db.M_CARAKELUARs.DeleteOnSubmit(ds)
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
                    Dim ds = oConnection.db.M_CARAKELUARs.Where(Function(x) x.ISDEFAULT = True)
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
                    Dim ds = oConnection.db.M_CARAKELUARs.Where(Function(x) x.ISDEFAULT = True)

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