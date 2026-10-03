Imports DataAccess.My.Resources

Namespace Transaksi
    Public Class clsDigital_SBAR
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

            sMODUL = "SBR"
        End Sub
        Protected Overrides Sub Finalize()
            MyBase.Finalize()
            oConnection = Nothing
            GC.SuppressFinalize(Me)
        End Sub
        Public Function GetStructureHeader() As S_DIGITAL_RI_31
            If Not oConnection.GetConnection() Then
                GetStructureHeader = Nothing
            End If
            GetStructureHeader = New S_DIGITAL_RI_31
        End Function
        Public Function GetData(ByVal sKDPINDAHAN As String) As S_DIGITAL_RI_31
            If Not oConnection.GetConnection() Then
                GetData = Nothing
                Exit Function
            End If
            GetData = oConnection.db.S_DIGITAL_RI_31s.FirstOrDefault(Function(x) x.KDPINDAHAN = sKDPINDAHAN)
        End Function
        Public Function GetStructureDetail() As S_DIGITAL_RI_31_DETIL
            If Not oConnection.GetConnection Then
                GetStructureDetail = Nothing
            End If
            GetStructureDetail = New S_DIGITAL_RI_31_DETIL
        End Function
        Public Function GetStructureDetailList() As List(Of S_DIGITAL_RI_31_DETIL)
            If Not oConnection.GetConnection Then
                GetStructureDetailList = Nothing
            End If
            GetStructureDetailList = New List(Of S_DIGITAL_RI_31_DETIL)
        End Function
        Public Function GetDataDetail() As List(Of S_DIGITAL_RI_31_DETIL)
            If Not oConnection.GetConnection Then
                GetDataDetail = Nothing
                Exit Function
            End If
            GetDataDetail = oConnection.db.S_DIGITAL_RI_31_DETILs.ToList()
        End Function
        Public Function GetDataDetail(ByVal Parameter As String) As List(Of S_DIGITAL_RI_31_DETIL)
            If Not oConnection.GetConnection Then
                GetDataDetail = Nothing
                Exit Function
            End If
            GetDataDetail = oConnection.db.S_DIGITAL_RI_31_DETILs.Where(Function(x) x.KDPINDAHAN = Parameter).ToList()
        End Function
        Public Function GetDataByRM(ByVal sKDCUSTOMER As String) As List(Of S_DIGITAL_RI_31)
            If Not oConnection.GetConnection() Then
                GetDataByRM = Nothing
                Exit Function
            End If
            GetDataByRM = oConnection.db.S_DIGITAL_RI_31s.Where(Function(x) x.KDCUSTOMER = sKDCUSTOMER).OrderByDescending(Function(x) x.KDPINDAHAN).ToList()
        End Function
        Public Function InsertData(ByVal entity As S_DIGITAL_RI_31, ByVal entityDetail As List(Of S_DIGITAL_RI_31_DETIL)) As Boolean
            Try
                If Not oConnection.GetConnection() Then
                    InsertData = False
                    Exit Function
                End If

                sREFERENCE = entity.KDPINDAHAN
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

                    entity.KDPINDAHAN = AutoNumber(sMODUL, sLASTNUMBER + 1, entity.DATE)

                    If entityDetail IsNot Nothing Then
                        For Each iLoop In entityDetail
                            iLoop.KDPINDAHAN = entity.KDPINDAHAN
                        Next
                    End If
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
                    oConnection.db.S_DIGITAL_RI_31s.InsertOnSubmit(entity)
                    oConnection.db.S_DIGITAL_RI_31_DETILs.InsertAllOnSubmit(entityDetail)
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
        Public Function UpdateData(ByVal entity As S_DIGITAL_RI_31, ByVal entityDetail As List(Of S_DIGITAL_RI_31_DETIL)) As Boolean
            Try
                If Not oConnection.GetConnection() Then
                    UpdateData = False
                    Exit Function
                End If

                sREFERENCE = entity.KDPINDAHAN
                sSTATUS = "UPDATE"

                Dim ds = oConnection.db.S_DIGITAL_RI_31s.FirstOrDefault(Function(x) x.KDPINDAHAN = entity.KDPINDAHAN)
                Dim dsDetail = oConnection.db.S_DIGITAL_RI_31_DETILs.Where(Function(x) x.KDPINDAHAN = entity.KDPINDAHAN)

                Try
                    oConnection.db.S_DIGITAL_RI_31s.DeleteOnSubmit(ds)
                    oConnection.db.S_DIGITAL_RI_31s.InsertOnSubmit(entity)

                    oConnection.db.S_DIGITAL_RI_31_DETILs.DeleteAllOnSubmit(dsDetail)
                    oConnection.db.S_DIGITAL_RI_31_DETILs.InsertAllOnSubmit(entityDetail)
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

                Dim ds = oConnection.db.S_DIGITAL_RI_31s.FirstOrDefault(Function(x) x.KDPINDAHAN = Parameter)
                Dim dsDetail = oConnection.db.S_DIGITAL_RI_31_DETILs.Where(Function(x) x.KDPINDAHAN = Parameter)

                Try
                    oConnection.db.S_DIGITAL_RI_31s.DeleteOnSubmit(ds)
                    If dsDetail IsNot Nothing Then
                        oConnection.db.S_DIGITAL_RI_31_DETILs.DeleteAllOnSubmit(dsDetail)
                    End If
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