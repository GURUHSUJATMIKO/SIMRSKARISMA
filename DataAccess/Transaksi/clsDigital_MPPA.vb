Imports DataAccess.My.Resources

Namespace Digital
    Public Class clsDigital_MPPA
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

            sMODUL = "MPPA"
        End Sub
        Protected Overrides Sub Finalize()
            MyBase.Finalize()
            oConnection = Nothing
            GC.SuppressFinalize(Me)
        End Sub
        Public Function GetStructureHeader() As S_DIGITAL_MPPA_H
            If Not oConnection.GetConnection() Then
                GetStructureHeader = Nothing
            End If
            GetStructureHeader = New S_DIGITAL_MPPA_H
        End Function
        Public Function GetData(ByVal sKDMPPA As String) As S_DIGITAL_MPPA_H
            If Not oConnection.GetConnection() Then
                GetData = Nothing
                Exit Function
            End If
            GetData = oConnection.db.S_DIGITAL_MPPA_Hs.FirstOrDefault(Function(x) x.KDMPPA = sKDMPPA)
        End Function
        Public Function GetDataByKdpendaftaran(ByVal sKDPENDAFTARAN As String) As S_DIGITAL_MPPA_H
            If Not oConnection.GetConnection() Then
                GetDataByKdpendaftaran = Nothing
                Exit Function
            End If
            GetDataByKdpendaftaran = oConnection.db.S_DIGITAL_MPPA_Hs.FirstOrDefault(Function(x) x.KDPENDAFTARAN = sKDPENDAFTARAN)
        End Function
         Public Function GetDataByRMList(ByVal sKDCUSTOMER As String ) As List(Of S_DIGITAL_MPPA_H)
            If Not oConnection.GetConnection Then
                GetDataByRMList = Nothing
                Exit Function
            End If
            GetDataByRMList = oConnection.db.S_DIGITAL_MPPA_Hs.Where(Function(x) x.KDCUSTOMER =sKDCUSTOMER).OrderByDescending(Function(x) x.KDMPPA ).ToList()
        End Function
             Public Function GetDataByRMList2(ByVal sKDCUSTOMER As String ) As List(Of S_DIGITAL_MPPB_H)
            If Not oConnection.GetConnection Then
                GetDataByRMList2 = Nothing
                Exit Function
            End If
            GetDataByRMList2 = oConnection.db.S_DIGITAL_MPPB_HS.Where(Function(x) x.KDCUSTOMER =sKDCUSTOMER).OrderByDescending(Function(x) x.KDMPPB ).ToList()
        End Function
        Public Function InsertData(ByVal entity As S_DIGITAL_MPPA_H) As Boolean
            Try
                If Not oConnection.GetConnection() Then
                    InsertData = False
                    Exit Function
                End If

                sREFERENCE = entity.KDMPPA
                sSTATUS = "INSERT"

                Try
                    sLASTNUMBER = oCounter.GetLastNumber(sMODUL, entity.DATE)
                    If sLASTNUMBER = 0 Then
                        Try
                            oCounter.InsertData(sMODUL, entity.DATE)
                            sLASTNUMBER = oCounter.GetLastNumber(sMODUL, entity.DATE)
                        Catch ex As Exception
                            sLASTNUMBER = 0
                        End Try
                    End If

                    entity.KDMPPA = AutoNumber(sMODUL, sLASTNUMBER + 1, entity.DATE)
                  
                Catch ex As Exception
                    oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                    Throw ex
                End Try

                Try
                    oConnection.db.S_DIGITAL_MPPA_Hs.InsertOnSubmit(entity)
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
                Try
                    oCounter.UpdateData(sMODUL, sLASTNUMBER + 1, Month(entity.DATE), Year(entity.DATE))
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
        Public Function UpdateData(ByVal entity As S_DIGITAL_MPPA_H) As Boolean
            Try
                If Not oConnection.GetConnection() Then
                    UpdateData = False
                    Exit Function
                End If

                sREFERENCE = entity.KDMPPA
                sSTATUS = "UPDATE"

                Dim ds = oConnection.db.S_DIGITAL_MPPA_Hs.FirstOrDefault(Function(x) x.KDMPPA = entity.KDMPPA)

                Try
                    oConnection.db.S_DIGITAL_MPPA_Hs.DeleteOnSubmit(ds)
                    oConnection.db.S_DIGITAL_MPPA_Hs.InsertOnSubmit(entity)
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

                Dim ds = oConnection.db.S_DIGITAL_MPPA_Hs.FirstOrDefault(Function(x) x.KDMPPA = Parameter)

                Try
                    oConnection.db.S_DIGITAL_MPPA_Hs.DeleteOnSubmit(ds)
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
            Finally
                oConnection.db.Dispose()
            End Try
        End Function
    End Class
End Namespace