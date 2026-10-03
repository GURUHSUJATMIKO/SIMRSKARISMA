Namespace Antrian
    Public Class clsSet_CounterAntrian
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

            sMODUL = "COUNTERANTRIAN"
        End Sub
        Protected Overrides Sub Finalize()
            MyBase.Finalize()
            oConnection = Nothing
            GC.SuppressFinalize(Me)
        End Sub
        Public Function GetStructureHeader() As SET_COUNTERANTRIAN
            If Not oConnection.GetConnection() Then
                GetStructureHeader = Nothing
            End If
            GetStructureHeader = New SET_COUNTERANTRIAN
        End Function
        Public Function GetData() As List(Of SET_COUNTERANTRIAN)
            If Not oConnection.GetConnection() Then
                GetData = Nothing
                Exit Function
            End If
            GetData = oConnection.db.SET_COUNTERANTRIANs.ToList()
        End Function
        Public Function GetData(ByVal Parameter As String) As SET_COUNTERANTRIAN
            If Not oConnection.GetConnection() Then
                GetData = Nothing
                Exit Function
            End If
            GetData = oConnection.db.SET_COUNTERANTRIANs.FirstOrDefault(Function(x) x.KDCOUNTERANTRIAN = Parameter)
        End Function
        Public Function GetLastNumberDay(ByVal sKDCOUNTERANTRIAN As String, ByVal sDATE As DateTime) As Integer
            Try
                If Not oConnection.GetConnection() Then
                    GetLastNumberDay = Nothing
                    Exit Function
                End If
                GetLastNumberDay = oConnection.db.SET_COUNTERANTRIANs.FirstOrDefault(Function(x) x.KDCOUNTERANTRIAN = sKDCOUNTERANTRIAN And x.MONTH = Month(sDATE) And x.YEAR = Year(sDATE) And x.DAY = Day(sDATE)).LASTNUMBER
            Catch ex As Exception
                GetLastNumberDay = 0
            End Try
        End Function
        Public Function InsertData(ByVal sKDCOUNTERANTRIAN As String, ByVal sDATE As DateTime) As Boolean
            Try
                If Not oConnection.GetConnection() Then
                    InsertData = False
                    Exit Function
                End If

                sREFERENCE = sMODUL & " - " & sKDCOUNTERANTRIAN
                sSTATUS = "INSERT"

                Try
                    Dim entity As New SET_COUNTERANTRIAN
                    With entity
                        .KDCOUNTERANTRIAN = sKDCOUNTERANTRIAN
                        .LASTNUMBER = 0
                        .MONTH = Month(sDATE)
                        .YEAR = Year(sDATE)
                        .DAY = Day(sDATE)
                    End With

                    oConnection.db.SET_COUNTERANTRIANs.InsertOnSubmit(entity)
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
        Public Function UpdateData(ByVal sKDCOUNTERANTRIAN As String, ByVal sLASTNUMBER As Integer, ByVal sDay As Integer, ByVal sMONTH As Integer, ByVal sYEAR As Integer) As Boolean
            Try
                If Not oConnection.GetConnection() Then
                    UpdateData = False
                    Exit Function
                End If

                sREFERENCE = sMODUL & " - " & sKDCOUNTERANTRIAN
                sSTATUS = "UPDATE"

                Dim ds = oConnection.db.SET_COUNTERANTRIANs.FirstOrDefault(Function(x) x.KDCOUNTERANTRIAN = sKDCOUNTERANTRIAN And x.DAY = sDay And x.MONTH = sMONTH And x.YEAR = sYEAR)

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
    End Class
End Namespace