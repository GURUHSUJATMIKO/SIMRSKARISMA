Imports DataAccess.My.Resources

Namespace Digital
    Public Class clsDigital_RJ_36
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

            sMODUL = "DIGITAL_36"
        End Sub
        Protected Overrides Sub Finalize()
            MyBase.Finalize()
            oConnection = Nothing
            GC.SuppressFinalize(Me)
        End Sub
        Public Function GetStructureHeader() As S_DIGITAL_RJ_36
            If Not oConnection.GetConnection() Then
                GetStructureHeader = Nothing
            End If
            GetStructureHeader = New S_DIGITAL_RJ_36
        End Function
        Public Function GetData(ByVal sKDPENDAFTARAN As String) As S_DIGITAL_RJ_36
            If Not oConnection.GetConnection() Then
                GetData = Nothing
                Exit Function
            End If
            GetData = oConnection.db.S_DIGITAL_RJ_36s.FirstOrDefault(Function(x) x.KDPENDAFTARAN = sKDPENDAFTARAN)
        End Function
        Public Function InsertData(ByVal entity As S_DIGITAL_RJ_36) As Boolean
            Try
                If Not oConnection.GetConnection() Then
                    InsertData = False
                    Exit Function
                End If

                sREFERENCE = entity.KDPENDAFTARAN
                sSTATUS = "INSERT"

                'sLASTNUMBER = oCounter.GetLastNumber(sMODUL, entity.DATE)
                'If sLASTNUMBER = 0 Then
                '    Try
                '        oCounter.InsertData(sMODUL, entity.DATE)
                '        sLASTNUMBER = oCounter.GetLastNumber(sMODUL, entity.DATE)
                '    Catch ex As Exception
                '        sLASTNUMBER = 0
                '    End Try
                'End If

                'entity.KDPENDAFTARAN = AutoNumber(sMODUL, sLASTNUMBER + 1, entity.DATE)

                Try
                    oConnection.db.S_DIGITAL_RJ_36s.InsertOnSubmit(entity)
                Catch ex As Exception
                    oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                    Throw ex
                End Try

                'Try
                '    oCounter.UpdateData(sMODUL, sLASTNUMBER + 1, Month(entity.DATE), Year(entity.DATE))
                'Catch ex As Exception
                '    oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                '    Throw ex
                'End Try

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
        Public Function UpdateData(ByVal entity As S_DIGITAL_RJ_36) As Boolean
            Try
                If Not oConnection.GetConnection() Then
                    UpdateData = False
                    Exit Function
                End If

                sREFERENCE = entity.KDPENDAFTARAN
                sSTATUS = "UPDATE"

                Dim ds = oConnection.db.S_DIGITAL_RJ_36s.FirstOrDefault(Function(x) x.KDPENDAFTARAN = entity.KDPENDAFTARAN)

                Try
                    oConnection.db.S_DIGITAL_RJ_36s.DeleteOnSubmit(ds)
                    oConnection.db.S_DIGITAL_RJ_36s.InsertOnSubmit(entity)
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
    End Class
End Namespace