Imports iPOS.DA.dcEntity

Namespace Master
    Public Class clsPDF
        Public sMODUL As String = ""
        Public sLASTNUMBER As Integer = 0

        Public oConnection As Setting.clsConnection = Nothing
        Public oError As Setting.clsError = Nothing
        Public Sub New()
            oConnection = New Setting.clsConnection
            oError = New Setting.clsError
            sMODUL = "PDF"
        End Sub
        Protected Overrides Sub Finalize()
            MyBase.Finalize()
            oConnection = Nothing
            GC.SuppressFinalize(Me)
        End Sub
        Public Function GetStructureHeader() As M_PDF
            If Not oConnection.GetConnection Then
                GetStructureHeader = Nothing
            End If
            GetStructureHeader = New M_PDF
        End Function
        Public Function GetData() As List(Of M_PDF)
            If Not oConnection.GetConnection Then
                GetData = Nothing
                Exit Function
            End If
            GetData = oConnection.db.M_PDFs.OrderBy(Function(x) x.DESCRIPTION).ToList()
        End Function
        Public Function GetData(ByVal sParameter As String) As M_PDF
            If Not oConnection.GetConnection Then
                GetData = Nothing
                Exit Function
            End If
            GetData = oConnection.db.M_PDFs.FirstOrDefault(Function(x) x.KDPDF = sParameter)
        End Function
        Public Function IsExist(ByVal sParameter As String) As Boolean
            If Not oConnection.GetConnection Then
                IsExist = False
                Exit Function
            End If

            Dim ds = oConnection.db.M_PDFs.FirstOrDefault(Function(x) x.DESCRIPTION = sParameter)

            If ds IsNot Nothing Then
                IsExist = True
            Else
                IsExist = False
            End If
        End Function
        Public Function InsertData(ByVal entity As M_PDF) As Boolean
            Try
                If Not oConnection.GetConnection Then
                    InsertData = False
                    Exit Function
                End If

                'If CheckDefault(0, entity.ISDEFAULT) = False Then
                '    InsertData = False
                '    Exit Function
                'End If

                'Generate Auto Number
                Try
                    sLASTNUMBER = CInt(oConnection.db.M_PDFs.OrderByDescending(Function(x) x.KDPDF).FirstOrDefault().KDPDF.Remove(0, (sMODUL & " _ ").Length)) + 1
                Catch ex As Exception
                    sLASTNUMBER = 1
                End Try
                'End Generate

                Try
                    entity.KDPDF = sMODUL & "_" & GLB.Globals.AutoNumberRegister(sLASTNUMBER)
                    oConnection.db.M_PDFs.InsertOnSubmit(entity)
                Catch ex As Exception
                    oError.InsertData("M_PDF", "INSERTDATA", ex.ToString)
                    Throw ex
                End Try
                Try
                    oConnection.db.SubmitChanges()
                Catch ex As Exception
                    oError.InsertData("M_PDF", "INSERTDATA", ex.ToString)
                    Throw ex
                End Try

                InsertData = True
            Catch ex As Exception
                InsertData = False
                oError.InsertData("M_PDF", "INSERTDATA", ex.ToString)
                Throw ex
            End Try
        End Function
        Public Function UpdateData(ByVal entity As M_PDF) As Boolean
            Try
                If Not oConnection.GetConnection Then
                    UpdateData = False
                    Exit Function
                End If

                'If CheckDefault(0, entity.ISDEFAULT) = False Then
                '    UpdateData = False
                '    Exit Function
                'End If

                Dim ds = oConnection.db.M_PDFs.FirstOrDefault(Function(x) x.KDPDF = entity.KDPDF)

                Try
                    oConnection.db.M_PDFs.DeleteOnSubmit(ds)
                    oConnection.db.M_PDFs.InsertOnSubmit(entity)
                Catch ex As Exception
                    oError.InsertData("M_PDF", "UPDATEDATA", ex.ToString)
                    Throw ex
                End Try
                Try
                    oConnection.db.SubmitChanges()
                Catch ex As Exception
                    oError.InsertData("M_PDF", "UPDATEDATA", ex.ToString)
                    Throw ex
                End Try

                UpdateData = True
            Catch ex As Exception
                UpdateData = False
                oError.InsertData("M_PDF", "UPDATEDATA", ex.ToString)
                Throw ex
            End Try
        End Function
        Public Function DeleteData(ByVal Parameter As String) As Boolean
            Try
                If Not oConnection.GetConnection Then
                    DeleteData = False
                    Exit Function
                End If

                'If CheckDefault(1, 0) = False Then
                '    DeleteData = False
                '    Exit Function
                'End If

                Dim ds = oConnection.db.M_PDFs.FirstOrDefault(Function(x) x.KDPDF = Parameter)

                Try
                    oConnection.db.M_PDFs.DeleteOnSubmit(ds)
                Catch ex As Exception
                    oError.InsertData("M_PDF", "DELETEDATA", ex.ToString)
                    Throw ex
                End Try
                Try
                    oConnection.db.SubmitChanges()
                Catch ex As Exception
                    oError.InsertData("M_PDF", "DELETEDATA", ex.ToString)
                    Throw ex
                End Try

                DeleteData = True
            Catch ex As Exception
                DeleteData = False
                oError.InsertData("M_PDF", "DELETEDATA", ex.ToString)
                Throw ex
            End Try
        End Function
        Public Function CheckDefault(ByVal sState As Integer, ByVal sDefault As Boolean) As Boolean
            Try
                If Not oConnection.GetConnection Then
                    CheckDefault = False
                    Exit Function
                End If


                If sState = 0 Then
                    Dim ds = oConnection.db.M_ITEM_L1s.Where(Function(x) x.ISDEFAULT = True)
                    If sDefault = True Then
                        For Each iLoop In ds
                            iLoop.ISDEFAULT = False
                        Next
                    Else
                        If ds.Count < 1 Then
                            MsgBox("Harus ada minimal satu yang terdaftar sebagai default!", MsgBoxStyle.Exclamation)
                            CheckDefault = False
                            Exit Function
                        End If
                    End If
                Else
                    Dim ds = oConnection.db.M_ITEM_L1s.Where(Function(x) x.ISDEFAULT = True)

                    If ds.Count < 1 Then
                        MsgBox("Harus ada minimal satu yang terdaftar sebagai default!", MsgBoxStyle.Exclamation)
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