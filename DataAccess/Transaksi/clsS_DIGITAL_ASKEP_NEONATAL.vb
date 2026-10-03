Imports DataAccess.My.Resources

Namespace Digital
    Public Class clsS_DIGITAL_ASKEP_NEONATAL
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

            sMODUL = "ASKEPNEONATAL"
        End Sub
        Protected Overrides Sub Finalize()
            MyBase.Finalize()
            oConnection = Nothing
            GC.SuppressFinalize(Me)
        End Sub
        Public Function GetStructureHeader() As S_DIGITAL_ASKEP_NEONATAL
            If Not oConnection.GetConnection() Then
                GetStructureHeader = Nothing
            End If
            GetStructureHeader = New S_DIGITAL_ASKEP_NEONATAL
        End Function
        Public Function GetStructureDetail() As S_DIGITAL_ASKEP_NEONATAL_DETIL
            If Not oConnection.GetConnection() Then
                GetStructureDetail = Nothing
            End If
            GetStructureDetail = New S_DIGITAL_ASKEP_NEONATAL_DETIL
        End Function


        Public Function GetStructureDetailList() As List(Of S_DIGITAL_ASKEP_NEONATAL_DETIL)
            If Not oConnection.GetConnection() Then
                GetStructureDetailList = Nothing
            End If
            GetStructureDetailList = New List(Of S_DIGITAL_ASKEP_NEONATAL_DETIL)
        End Function


        Public Function GetData() As List(Of S_DIGITAL_ASKEP_NEONATAL)
            If Not oConnection.GetConnection Then
                GetData = Nothing
                Exit Function
            End If
            GetData = oConnection.db.S_DIGITAL_ASKEP_NEONATALs.OrderBy(Function(x) x.KDASESMEN).ToList()
        End Function
        Public Function GetData(ByVal sKDASESMEN As String) As S_DIGITAL_ASKEP_NEONATAL
            If Not oConnection.GetConnection() Then
                GetData = Nothing
                Exit Function
            End If
            GetData = oConnection.db.S_DIGITAL_ASKEP_NEONATALs.FirstOrDefault(Function(x) x.KDASESMEN = sKDASESMEN)
        End Function

        Public Function GetDataDetail() As List(Of S_DIGITAL_ASKEP_NEONATAL_DETIL)
            If Not oConnection.GetConnection Then
                GetDataDetail = Nothing
                Exit Function
            End If
            GetDataDetail = oConnection.db.S_DIGITAL_ASKEP_NEONATAL_DETILs.ToList()
        End Function
        Public Function GetDataDetail(ByVal Parameter As String) As List(Of S_DIGITAL_ASKEP_NEONATAL_DETIL)
            If Not oConnection.GetConnection Then
                GetDataDetail = Nothing
                Exit Function
            End If
            GetDataDetail = oConnection.db.S_DIGITAL_ASKEP_NEONATAL_DETILs.Where(Function(x) x.KDASESMEN = Parameter).ToList()
        End Function

        Public Function IsExist(ByVal sParameter As String) As Boolean
            If Not oConnection.GetConnection Then
                IsExist = False
                Exit Function
            End If

            Dim ds = oConnection.db.S_DIGITAL_ASKEP_NEONATALs.FirstOrDefault(Function(x) x.KDPENDAFTARAN = sParameter)

            If ds IsNot Nothing Then
                IsExist = True
            Else
                IsExist = False
            End If
        End Function
        Public Function InsertData(ByVal entity As S_DIGITAL_ASKEP_NEONATAL, ByVal entityDetail As List(Of S_DIGITAL_ASKEP_NEONATAL_DETIL)) As Boolean
            Try
                If Not oConnection.GetConnection() Then
                    InsertData = False
                    Exit Function
                End If

                sREFERENCE = entity.KDASESMEN
                sSTATUS = "INSERT"

                sLASTNUMBER = oCounter.GetLastNumber(sMODUL, entity.DATE)
                If sLASTNUMBER = 0 Then
                    Try
                        oCounter.InsertData(sMODUL, entity.DATE)
                        sLASTNUMBER = oCounter.GetLastNumber(sMODUL, entity.DATE)
                    Catch ex As Exception
                        sLASTNUMBER = 0
                    End Try
                End If

                entity.KDASESMEN = AutoNumber(sMODUL, sLASTNUMBER + 1, entity.DATE)

                If entityDetail IsNot Nothing Then
                    For Each iLoop In entityDetail
                        iLoop.KDASESMEN = entity.KDASESMEN
                    Next
                End If


                Try
                    oConnection.db.S_DIGITAL_ASKEP_NEONATALs.InsertOnSubmit(entity)
                    oConnection.db.S_DIGITAL_ASKEP_NEONATAL_DETILs.InsertAllOnSubmit(entityDetail)
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

        Public Function UpdateData(ByVal Kode As String, ByVal entity As S_DIGITAL_ASKEP_NEONATAL, ByVal entityDetail As List(Of S_DIGITAL_ASKEP_NEONATAL_DETIL)) As Boolean
            Try
                If Not oConnection.GetConnection() Then
                    UpdateData = False
                    Exit Function
                End If

                sREFERENCE = entity.KDASESMEN
                sSTATUS = "UPDATE"

                Dim ds = oConnection.db.S_DIGITAL_ASKEP_NEONATALs.FirstOrDefault(Function(x) x.KDASESMEN = Kode)
                Dim dsDetail = oConnection.db.S_DIGITAL_ASKEP_NEONATAL_DETILs.Where(Function(x) x.KDASESMEN = Kode)

                Try
                    oConnection.db.S_DIGITAL_ASKEP_NEONATALs.DeleteOnSubmit(ds)
                    oConnection.db.S_DIGITAL_ASKEP_NEONATALs.InsertOnSubmit(entity)

                    oConnection.db.S_DIGITAL_ASKEP_NEONATAL_DETILs.DeleteAllOnSubmit(dsDetail)
                    oConnection.db.S_DIGITAL_ASKEP_NEONATAL_DETILs.InsertAllOnSubmit(entityDetail)
                    
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
                If Not oConnection.GetConnection Then
                    DeleteData = False
                    Exit Function
                End If

                Dim ds = oConnection.db.S_DIGITAL_ASKEP_NEONATALs.FirstOrDefault(Function(x) x.KDASESMEN = Parameter)
                Dim dsDetail = oConnection.db.S_DIGITAL_ASKEP_NEONATAL_DETILs.Where(Function(x) x.KDASESMEN = Parameter)

                Try
                    oConnection.db.S_DIGITAL_ASKEP_NEONATALs.DeleteOnSubmit(ds)
                    oConnection.db.S_DIGITAL_ASKEP_NEONATAL_DETILs.DeleteAllOnSubmit(dsDetail)
                Catch ex As Exception
                    oError.InsertData("S_DIGITAL_ASKEP_NEONATAL", "DELETEDATA", ex.ToString, Parameter)
                    Throw ex
                End Try
                Try
                    oConnection.db.SubmitChanges()
                Catch ex As Exception
                    oError.InsertData("S_DIGITAL_ASKEP_NEONATAL", "DELETEDATA", ex.ToString, Parameter)
                    Throw ex
                End Try

                DeleteData = True
            Catch ex As Exception
                DeleteData = False
                oError.InsertData("S_DIGITAL_ASKEP_NEONATAL", "DELETEDATA", ex.ToString, Parameter)
                Throw ex
            End Try
        End Function

        Public Function UpdateCetak(ByVal sKDASESMEN As String) As Boolean
            Try
                If Not oConnection.GetConnection Then
                    UpdateCetak = False
                    Exit Function
                End If

                Try
                    Dim ds = oConnection.db.S_DIGITAL_ASKEP_NEONATALs.FirstOrDefault(Function(x) x.KDASESMEN = sKDASESMEN)

                    ds.CETAK += 1

                    oConnection.db.SubmitChanges()

                Catch ex As Exception
                    oError.InsertData("S_DIGITAL_ASKEP_NEONATAL", "UPDATECETAK", ex.ToString, sKDASESMEN)
                    Throw ex
                End Try

                UpdateCetak = True
            Catch ex As Exception
                UpdateCetak = False
                oError.InsertData("S_DIGITAL_ASKEP_NEONATAL", "UPDATECETAK", ex.ToString, sKDASESMEN)
                Throw ex
            End Try
        End Function

        Public Function UpdateDelete(ByVal sKDASESMEN As String, ByVal sUSER As String) As Boolean
            Try
                If Not oConnection.GetConnection Then
                    UpdateDelete = False
                    Exit Function
                End If

                Try
                    Dim ds = oConnection.db.S_DIGITAL_ASKEP_NEONATALs.FirstOrDefault(Function(x) x.KDASESMEN = sKDASESMEN)

                    ds.ISDELETE = 1
                    ds.DATEDELETE = Now
                    ds.USERDELETE = sUSER

                    oConnection.db.SubmitChanges()

                Catch ex As Exception
                    oError.InsertData("S_DIGITAL_ASKEP_NEONATAL", "UPDATEDELETE", ex.ToString, sKDASESMEN)
                    Throw ex
                End Try

                UpdateDelete = True
            Catch oErr As Exception
                UpdateDelete = False
                 MsgBox("Gagal Delete Data " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation)
                Throw oErr
            End Try
        End Function
    End Class
End Namespace