Imports System.Threading

Namespace EClaim
    Public Class clsKlaim
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
            sMODUL = "KLAIM"

            oCounter = New Setting.clsCounter
        End Sub
        Protected Overrides Sub Finalize()
            MyBase.Finalize()
            oConnection = Nothing
            GC.SuppressFinalize(Me)
        End Sub
        Public Function GetStructureHeader() As S_PENDAFTARAN_KLAIM
            If Not oConnection.GetConnection() Then
                GetStructureHeader = Nothing
            End If
            GetStructureHeader = New S_PENDAFTARAN_KLAIM
        End Function
        Public Function GetData() As List(Of S_PENDAFTARAN_KLAIM)
            If Not oConnection.GetConnection() Then
                GetData = Nothing
                Exit Function
            End If
            GetData = oConnection.db.S_PENDAFTARAN_KLAIMs.OrderByDescending(Function(x) x.KDPENDAFTARAN).ToList()
        End Function
        Public Function GetData(ByVal Parameter As String) As S_PENDAFTARAN_KLAIM
            If Not oConnection.GetConnection() Then
                GetData = Nothing
                Exit Function
            End If
            GetData = oConnection.db.S_PENDAFTARAN_KLAIMs.FirstOrDefault(Function(x) x.KDPENDAFTARAN = Parameter)
        End Function
        Public Function InsertData(ByVal entity As S_PENDAFTARAN_KLAIM) As Boolean
            Try
                If Not oConnection.GetConnection() Then
                    InsertData = False
                    Exit Function
                End If

                sREFERENCE = entity.KDPENDAFTARAN
                sSTATUS = "INSERT"

                'If entity.CATEGORY = 0 Then
                '    sMODUL = "KLAIM-RJ"
                'Else
                '    sMODUL = "KLAIM-RI"
                'End If

                'Try
                '    sLASTNUMBER = oCounter.GetLastNumber(sMODUL, entity.DATE_MASUK)

                '    If sLASTNUMBER = 0 Then
                '        Try
                '            oCounter.InsertData(sMODUL, entity.DATE_MASUK)
                '            sLASTNUMBER = oCounter.GetLastNumber(sMODUL, entity.DATE_MASUK)
                '        Catch ex As Exception
                '            sLASTNUMBER = 0
                '        End Try
                '    End If

                '    entity.KDPENDAFTARAN = AutoNumber(sMODUL, sLASTNUMBER + 1, entity.DATE_MASUK)

                'Catch ex As Exception
                '    oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                '    Throw ex
                'End Try

                'Try
                '    oCounter.UpdateData(sMODUL, sLASTNUMBER + 1, Month(entity.DATE_MASUK), Year(entity.DATE_MASUK))
                'Catch ex As Exception
                '    oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                '    Throw ex
                'End Try

                Try
                    oConnection.db.S_PENDAFTARAN_KLAIMs.InsertOnSubmit(entity)
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
        Public Function UpdateData(ByVal entity As S_PENDAFTARAN_KLAIM) As Boolean
            Try
                If Not oConnection.GetConnection() Then
                    UpdateData = False
                    Exit Function
                End If

                sREFERENCE = entity.KDPENDAFTARAN
                sSTATUS = "UPDATE"

                Dim ds = oConnection.db.S_PENDAFTARAN_KLAIMs.FirstOrDefault(Function(x) x.KDPENDAFTARAN = entity.KDPENDAFTARAN)

                Try
                    oConnection.db.S_PENDAFTARAN_KLAIMs.DeleteOnSubmit(ds)
                    oConnection.db.S_PENDAFTARAN_KLAIMs.InsertOnSubmit(entity)

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
                If Not oConnection.GetConnection() Then
                    DeleteData = False
                    Exit Function
                End If

                sREFERENCE = Parameter
                sSTATUS = "DELETE"

                Dim ds = oConnection.db.S_PENDAFTARAN_KLAIMs.FirstOrDefault(Function(x) x.KDPENDAFTARAN.Contains(Parameter))

                If ds IsNot Nothing Then
                    Try
                        oConnection.db.S_PENDAFTARAN_KLAIMs.DeleteOnSubmit(ds)
                    Catch ex As Exception
                        oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                        Throw ex
                    End Try

                End If

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
        Public Function KodeTarifKlaim_Default() As String
            Try
                If Not oConnection.GetConnection() Then
                    KodeTarifKlaim_Default = String.Empty
                    Exit Function
                End If

                Dim ds = oConnection.db.M_KODETARIF_KLAIMs.FirstOrDefault(Function(x) x.ISDEFAULT = True)
                If ds IsNot Nothing Then
                    KodeTarifKlaim_Default = ds.KODETARIF_KLAIM
                Else
                    KodeTarifKlaim_Default = String.Empty
                End If
            Catch ex As Exception
                KodeTarifKlaim_Default = String.Empty
                Throw ex
            End Try
        End Function
        Public Function COB_Default() As String
            Try
                If Not oConnection.GetConnection() Then
                    COB_Default = String.Empty
                    Exit Function
                End If

                Dim ds = oConnection.db.M_COBs.FirstOrDefault(Function(x) x.ISDEFAULT = True)
                If ds IsNot Nothing Then
                    COB_Default = ds.KDCOB
                Else
                    COB_Default = String.Empty
                End If
            Catch ex As Exception
                COB_Default = String.Empty
                Throw ex
            End Try
        End Function
        Public Function CaraKeluar_Default() As String
            Try
                If Not oConnection.GetConnection() Then
                    CaraKeluar_Default = String.Empty
                    Exit Function
                End If

                Dim ds = oConnection.db.M_CARAKELUARs.FirstOrDefault(Function(x) x.ISDEFAULT = True)
                If ds IsNot Nothing Then
                    CaraKeluar_Default = ds.KDCARAKELUAR
                Else
                    CaraKeluar_Default = String.Empty
                End If
            Catch ex As Exception
                CaraKeluar_Default = String.Empty
                Throw ex
            End Try
        End Function
    End Class
End Namespace