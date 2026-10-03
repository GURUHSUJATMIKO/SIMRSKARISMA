Namespace Setting
    Public Class clsCounter
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

            sMODUL = "COUNTER"
        End Sub
        Protected Overrides Sub Finalize()
            MyBase.Finalize()
            oConnection = Nothing
            GC.SuppressFinalize(Me)
        End Sub
        Public Function GetStructureHeader() As SET_COUNTER
            If Not oConnection.GetConnection() Then
                GetStructureHeader = Nothing
            End If
            GetStructureHeader = New SET_COUNTER
        End Function
        Public Function GetData() As List(Of SET_COUNTER)
            If Not oConnection.GetConnection() Then
                GetData = Nothing
                Exit Function
            End If
            GetData = oConnection.db.SET_COUNTERs.ToList()
        End Function
        Public Function GetData(ByVal Parameter As String) As SET_COUNTER
            If Not oConnection.GetConnection() Then
                GetData = Nothing
                Exit Function
            End If
            GetData = oConnection.db.SET_COUNTERs.FirstOrDefault(Function(x) x.KDCOUNTER = Parameter)
        End Function
        Public Function GetDataNameModul(ByVal Parameter As String) As SET_NAMEMODUL
            If Not oConnection.GetConnection() Then
                GetDataNameModul = Nothing
                Exit Function
            End If
            GetDataNameModul = oConnection.db.SET_NAMEMODULs.FirstOrDefault(Function(x) x.KDNAMEMODUL = Parameter)
        End Function
        Public Function GetLastNumber(ByVal sKDCOUNTER As String, ByVal sDATE As DateTime) As Integer
            Try
                If Not oConnection.GetConnection() Then
                    GetLastNumber = Nothing
                    Exit Function
                End If
                GetLastNumber = oConnection.db.SET_COUNTERs.FirstOrDefault(Function(x) x.KDCOUNTER = sKDCOUNTER And x.MONTH = Month(sDATE) And x.YEAR = Year(sDATE)).LASTNUMBER
            Catch ex As Exception
                GetLastNumber = 0
            End Try
        End Function
        Public Function GetLastNumberTahun(ByVal sKDCOUNTER As String, ByVal sDATE As DateTime) As Integer
            Try
                If Not oConnection.GetConnection() Then
                    GetLastNumberTahun = Nothing
                    Exit Function
                End If
                GetLastNumberTahun = oConnection.db.SET_COUNTERs.FirstOrDefault(Function(x) x.KDCOUNTER = sKDCOUNTER And x.YEAR = Year(sDATE)).LASTNUMBER
            Catch ex As Exception
                GetLastNumberTahun = 0
            End Try
        End Function
        Public Function GetLastNumberdDay(ByVal sKDCOUNTER As String, ByVal sDATE As DateTime) As Integer
            Try
                If Not oConnection.GetConnection() Then
                    GetLastNumberdDay = Nothing
                    Exit Function
                End If
                GetLastNumberdDay = oConnection.db.SET_COUNTERs.FirstOrDefault(Function(x) x.KDCOUNTER = sKDCOUNTER And x.MONTH = Month(sDATE) And x.YEAR = Year(sDATE) And x.DAY = Day(sDATE)).LASTNUMBER
            Catch ex As Exception
                GetLastNumberdDay = 0
            End Try
        End Function
        Public Function GetLastNumber(ByVal sKDCOUNTER As String) As Integer
            Try
                If Not oConnection.GetConnection() Then
                    GetLastNumber = Nothing
                    Exit Function
                End If
                GetLastNumber = oConnection.db.SET_COUNTERs.FirstOrDefault(Function(x) x.KDCOUNTER = sKDCOUNTER).LASTNUMBER
            Catch ex As Exception
                GetLastNumber = 0
            End Try
        End Function
        Public Function GetLastNumberBarcoder() As Integer
            Try
                If Not oConnection.GetConnection Then
                    GetLastNumberBarcoder = Nothing
                    Exit Function
                End If
                GetLastNumberBarcoder = oConnection.db.SET_COUNTERs.FirstOrDefault(Function(x) x.KDCOUNTER = "BARCODE").LASTNUMBER
            Catch ex As Exception
                GetLastNumberBarcoder = 0
            End Try
        End Function
        Public Function InsertData(ByVal sKDCOUNTER As String, ByVal sDATE As DateTime) As Boolean
            Try
                If Not oConnection.GetConnection() Then
                    InsertData = False
                    Exit Function
                End If

                sREFERENCE = sMODUL & " - " & sKDCOUNTER
                sSTATUS = "INSERT"

                Try
                    Dim entity As New SET_COUNTER
                    With entity
                        .KDCOUNTER = sKDCOUNTER
                        .LASTNUMBER = 0
                        .MONTH = Month(sDATE)
                        .YEAR = Year(sDATE)
                        .DAY = Day(sDATE)
                    End With

                    oConnection.db.SET_COUNTERs.InsertOnSubmit(entity)
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
        Public Function InsertDataBarcode() As Boolean
            Try
                If Not oConnection.GetConnection Then
                    InsertDataBarcode = False
                    Exit Function
                End If

                Try
                    Dim entity As New SET_COUNTER
                    With entity
                        .KDCOUNTER = "BARCODE"
                        .LASTNUMBER = 0
                        .MONTH = 0
                        .YEAR = 0
                    End With

                    oConnection.db.SET_COUNTERs.InsertOnSubmit(entity)
                Catch ex As Exception
                    oError.InsertData("COUNTER", "INSERTDATABARCODE", ex.ToString, sREFERENCE)
                    Throw ex
                End Try
                Try
                    oConnection.db.SubmitChanges()
                Catch ex As Exception
                    oError.InsertData("COUNTER", "INSERTDATABARCODE", ex.ToString, sREFERENCE)
                    Throw ex
                End Try

                InsertDataBarcode = True
            Catch ex As Exception
                InsertDataBarcode = False
                oError.InsertData("COUNTER", "INSERTDATABARCODE", ex.ToString, sREFERENCE)
                Throw ex
            End Try
        End Function
        Public Function UpdateData(ByVal sKDCOUNTER As String, ByVal sLASTNUMBER As Integer, ByVal sMONTH As Integer, ByVal sYEAR As Integer) As Boolean
            Try
                If Not oConnection.GetConnection() Then
                    UpdateData = False
                    Exit Function
                End If

                sREFERENCE = sMODUL & " - " & sKDCOUNTER
                sSTATUS = "UPDATE"

                Dim ds = oConnection.db.SET_COUNTERs.FirstOrDefault(Function(x) x.KDCOUNTER = sKDCOUNTER And x.MONTH = sMONTH And x.YEAR = sYEAR)

                With ds
                    .LASTNUMBER = sLASTNUMBER
                End With

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
        Public Function UpdateData(ByVal sKDCOUNTER As String, ByVal sLASTNUMBER As Integer) As Boolean
            Try
                If Not oConnection.GetConnection() Then
                    UpdateData = False
                    Exit Function
                End If

                sREFERENCE = sMODUL & " - " & sKDCOUNTER
                sSTATUS = "UPDATE"

                Dim ds = oConnection.db.SET_COUNTERs.FirstOrDefault(Function(x) x.KDCOUNTER = sKDCOUNTER)

                With ds
                    .LASTNUMBER = sLASTNUMBER
                End With

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
        Public Function UpdateDataBarcode(ByVal sLASTNUMBER As Integer) As Boolean
            Try
                If Not oConnection.GetConnection Then
                    UpdateDataBarcode = False
                    Exit Function
                End If

                Dim ds = oConnection.db.SET_COUNTERs.FirstOrDefault(Function(x) x.KDCOUNTER = "BARCODE")

                With ds
                    .LASTNUMBER = sLASTNUMBER
                End With

                Try
                    oConnection.db.SubmitChanges()
                Catch ex As Exception
                    oError.InsertData("COUNTER", "UPDATEDATABARCODE", ex.ToString, sREFERENCE)
                    Throw ex
                End Try

                UpdateDataBarcode = True
            Catch ex As Exception
                UpdateDataBarcode = False
                oError.InsertData("COUNTER", "UPDATEDATABARCODE", ex.ToString, sREFERENCE)
                Throw ex
            End Try
        End Function
        Public Function GetLastNumberStaff(ByVal sKDCOUNTER As String) As Integer
            Try
                If Not oConnection.GetConnection() Then
                    GetLastNumberStaff = Nothing
                    Exit Function
                End If
                GetLastNumberStaff = oConnection.db.SET_COUNTERs.FirstOrDefault(Function(x) x.KDCOUNTER = sKDCOUNTER).LASTNUMBER
            Catch ex As Exception
                GetLastNumberStaff = 0
            End Try
        End Function
        Public Function InsertDataStaff(ByVal sKDCOUNTER As String) As Boolean
            Try
                If Not oConnection.GetConnection() Then
                    InsertDataStaff = False
                    Exit Function
                End If

                sREFERENCE = sMODUL & " - " & sKDCOUNTER
                sSTATUS = "INSERT"

                Try
                    Dim entity As New SET_COUNTER
                    With entity
                        .KDCOUNTER = sKDCOUNTER
                        .LASTNUMBER = 0
                        .MONTH = 0
                        .YEAR = 0
                    End With

                    oConnection.db.SET_COUNTERs.InsertOnSubmit(entity)
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

                InsertDataStaff = True
            Catch ex As Exception
                InsertDataStaff = False
                oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                Throw ex
            End Try
        End Function
        Public Function UpdateDataStaff(ByVal sKDCOUNTER As String, ByVal sLASTNUMBER As Integer) As Boolean
            Try
                If Not oConnection.GetConnection() Then
                    UpdateDataStaff = False
                    Exit Function
                End If

                sREFERENCE = sMODUL & " - " & sKDCOUNTER
                sSTATUS = "UPDATE"

                Dim ds = oConnection.db.SET_COUNTERs.FirstOrDefault(Function(x) x.KDCOUNTER = sKDCOUNTER)

                With ds
                    .LASTNUMBER = sLASTNUMBER
                End With

                Try
                    oConnection.db.SubmitChanges()
                Catch ex As Exception
                    oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                    Throw ex
                End Try

                UpdateDataStaff = True
            Catch ex As Exception
                UpdateDataStaff = False
                oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                Throw ex
            End Try
        End Function
        Public Function UpdateData(ByVal sKDCOUNTER As String, ByVal sLASTNUMBER As Integer, ByVal sDay As Integer, ByVal sMONTH As Integer, ByVal sYEAR As Integer) As Boolean
            Try
                If Not oConnection.GetConnection() Then
                    UpdateData = False
                    Exit Function
                End If

                sREFERENCE = sMODUL & " - " & sKDCOUNTER
                sSTATUS = "UPDATE"

                Dim ds = oConnection.db.SET_COUNTERs.FirstOrDefault(Function(x) x.KDCOUNTER = sKDCOUNTER And x.DAY = sDay And x.MONTH = sMONTH And x.YEAR = sYEAR)

                With ds
                    .LASTNUMBER = sLASTNUMBER
                End With

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
        Public Function UpdateDataCekSuara() As Boolean
            Try
                If Not oConnection.GetConnection() Then
                    UpdateDataCekSuara = False
                    Exit Function
                End If

                sREFERENCE = "SUARA"

                Dim ds = oConnection.db.SET_NAMEMODULs.FirstOrDefault(Function(x) x.KDNAMEMODUL = "DEMO")

                With ds
                    .KDDAFTAR_L2_1 = "SUARA"
                End With

                Try
                    oConnection.db.SubmitChanges()
                Catch ex As Exception
                    oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                    Throw ex
                End Try

                UpdateDataCekSuara = True
            Catch ex As Exception
                UpdateDataCekSuara = False
                oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                Throw ex
            End Try
        End Function
    End Class
End Namespace