Namespace Antrian
    Public Class clsSet_Antrian_Simpan
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

            sMODUL = "ANTRIANSIMPAN"
        End Sub
        Protected Overrides Sub Finalize()
            MyBase.Finalize()
            oConnection = Nothing
            GC.SuppressFinalize(Me)
        End Sub
        Public Function GetStructureHeader() As SET_ANTRIAN_SIMPAN
            If Not oConnection.GetConnection() Then
                GetStructureHeader = Nothing
            End If
            GetStructureHeader = New SET_ANTRIAN_SIMPAN
        End Function
        Public Function GetData() As List(Of SET_ANTRIAN_SIMPAN)
            If Not oConnection.GetConnection() Then
                GetData = Nothing
                Exit Function
            End If
            GetData = oConnection.db.SET_ANTRIAN_SIMPANs.OrderBy(Function(x) x.KDANTRIANMANUAL).ToList()
        End Function
        Public Function GetData(ByVal sKDANTRIANMANUAL As String) As SET_ANTRIAN_SIMPAN
            If Not oConnection.GetConnection() Then
                GetData = Nothing
                Exit Function
            End If
            GetData = oConnection.db.SET_ANTRIAN_SIMPANs.FirstOrDefault(Function(x) x.KDANTRIANMANUAL = sKDANTRIANMANUAL)
        End Function
        Public Function GetDataLastUser(ByVal sDate As Date, ByVal sKODE As String) As SET_ANTRIAN_SIMPAN
            If Not oConnection.GetConnection Then
                GetDataLastUser = Nothing
                Exit Function
            End If

            Dim ds = (From x In oConnection.db.SET_ANTRIAN_SIMPANs.Where(Function(x) x.KODE = sKODE And x.DATECREATED.Year = Year(sDate) And x.DATECREATED.Month = Month(sDate) And x.DATECREATED.Day = Day(sDate) And x.ISPANGGIL = False)
                      Select x).ToList()

            If ds.Count > 0 Then
                GetDataLastUser = ds.OrderBy(Function(x) x.KDANTRIANMANUAL).FirstOrDefault()
            Else
                GetDataLastUser = Nothing
            End If
        End Function
        Public Function GetDataAllData(ByVal sDate As Date, ByVal sKODE As String) As Decimal
            If Not oConnection.GetConnection Then
                GetDataAllData = 0
                Exit Function
            End If

            Dim dsAll As Decimal = (From x In oConnection.db.SET_ANTRIAN_SIMPANs.Where(Function(x) x.KODE = sKODE And x.DATECREATED.Year = Year(sDate) And x.DATECREATED.Month = Month(sDate) And x.DATECREATED.Day = Day(sDate))).Count()
            Dim dsSudahDipanggil As Decimal = (From x In oConnection.db.SET_ANTRIAN_SIMPANs.Where(Function(x) x.KODE = sKODE And x.DATECREATED.Year = Year(sDate) And x.DATECREATED.Month = Month(sDate) And x.DATECREATED.Day = Day(sDate) And x.ISPANGGIL = 1)).Count()

            GetDataAllData = dsAll - dsSudahDipanggil

        End Function
        Public Function InsertData(ByVal entity As SET_ANTRIAN_SIMPAN) As Integer
            Try
                If Not oConnection.GetConnection() Then
                    InsertData = 0
                    Exit Function
                End If

                sREFERENCE = entity.KDANTRIANMANUAL
                sSTATUS = "INSERT"

                Dim oSetCounter As New Antrian.clsSet_CounterAntrian
                Dim dsSetConter = oSetCounter.GetData(entity.KODE)

                sMODUL = entity.KODE

                Try
                    sLASTNUMBER = oSetCounter.GetLastNumberDay(sMODUL, entity.DATECREATED)

                    If sLASTNUMBER = 0 Then
                        Try
                            oSetCounter.InsertData(sMODUL, entity.DATECREATED)
                            sLASTNUMBER = oSetCounter.GetLastNumberDay(sMODUL, entity.DATECREATED)
                        Catch ex As Exception
                            sLASTNUMBER = 0
                        End Try
                    End If

                    entity.NOMORANTRIAN = sLASTNUMBER + 1

                Catch ex As Exception
                    oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                    Throw ex
                End Try

                Try
                    oSetCounter.UpdateData(sMODUL, sLASTNUMBER + 1, Day(entity.DATECREATED), Month(entity.DATECREATED), Year(entity.DATECREATED))
                Catch ex As Exception
                    oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                    Throw ex
                End Try

                Try
                    oConnection.db.SET_ANTRIAN_SIMPANs.InsertOnSubmit(entity)
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

                UpdateDataConter(entity.KODE, entity.NOMORANTRIAN)

                InsertData = entity.KDANTRIANMANUAL
            Catch ex As Exception
                InsertData = 0
                oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                Throw ex
            End Try
        End Function
        Public Function UpdateDataConter(ByVal sKDCOUNTERANTRIAN As String, ByVal sLASTNUMBER As Integer) As Boolean
            Try
                If Not oConnection.GetConnection Then
                    UpdateDataConter = False
                    Exit Function
                End If

                UpdateDataConter = True

                Dim ds = oConnection.db.SET_COUNTERANTRIANs.FirstOrDefault(Function(x) x.KDCOUNTERANTRIAN = sKDCOUNTERANTRIAN)

                If ds IsNot Nothing Then
                    ds.LASTNUMBER = sLASTNUMBER

                    oConnection.db.SubmitChanges()
                Else
                    Dim dsSetCounterSave As New SET_COUNTERANTRIAN
                    With dsSetCounterSave
                        .KDCOUNTERANTRIAN = sKDCOUNTERANTRIAN
                        .LASTNUMBER = sLASTNUMBER
                    End With

                    oConnection.db.SET_COUNTERANTRIANs.InsertOnSubmit(dsSetCounterSave)
                    oConnection.db.SubmitChanges()
                End If
            Catch ex As Exception
                UpdateDataConter = False
                Throw ex
            End Try
        End Function
        Public Function UpdateDataIsCheked(ByVal sKDANTRIANMANUAL As Integer) As Boolean
            Try
                If Not oConnection.GetConnection Then
                    UpdateDataIsCheked = False
                    Exit Function
                End If

                UpdateDataIsCheked = True

                Dim ds = oConnection.db.SET_ANTRIAN_SIMPANs.FirstOrDefault(Function(x) x.KDANTRIANMANUAL = sKDANTRIANMANUAL)

                ds.ISPANGGIL = 1

                oConnection.db.SubmitChanges()
            Catch ex As Exception
                UpdateDataIsCheked = False
                Throw ex
            End Try
        End Function
        'Public Function DeleteDataALL() As Boolean
        '    Try
        '        If Not oConnection.GetConnection() Then
        '            DeleteDataALL = False
        '            Exit Function
        '        End If

        '        sREFERENCE = "ANTRIAN ALL"
        '        sSTATUS = "DELETE"

        '        Dim ds_SET_ANTRIAN_PANGGIL = oConnection.db.SET_ANTRIAN_PANGGILs.ToList()
        '        Dim ds_SET_ANTRIAN_SISA = oConnection.db.SET_ANTRIAN_SISAs.ToList()

        '        Try
        '            oConnection.db.SET_ANTRIAN_PANGGILs.DeleteAllOnSubmit(ds_SET_ANTRIAN_PANGGIL)
        '            oConnection.db.SET_ANTRIAN_SISAs.DeleteAllOnSubmit(ds_SET_ANTRIAN_SISA)
        '        Catch ex As Exception
        '            oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
        '            Throw ex
        '        End Try
        '        Try
        '            oConnection.db.SubmitChanges()
        '        Catch ex As Exception
        '            oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
        '            Throw ex
        '        End Try

        '        DeleteDataALL = True
        '    Catch ex As Exception
        '        DeleteDataALL = False
        '        oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
        '        Throw ex
        '    End Try
        'End Function
    End Class
End Namespace