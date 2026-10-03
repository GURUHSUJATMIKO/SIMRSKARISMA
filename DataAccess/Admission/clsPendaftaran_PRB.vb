Imports System.Threading

Namespace Admission
    Public Class clsPendaftaran_PRB
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
            sMODUL = "PRB"

            oCounter = New Setting.clsCounter
        End Sub
        Protected Overrides Sub Finalize()
            MyBase.Finalize()
            oConnection = Nothing
            GC.SuppressFinalize(Me)
        End Sub
        Public Function GetStructureHeader() As S_PENDAFTARAN_PRB
            If Not oConnection.GetConnection() Then
                GetStructureHeader = Nothing
            End If
            GetStructureHeader = New S_PENDAFTARAN_PRB
        End Function
        Public Function GetStructureDetail() As S_PENDAFTARAN_PRB_D
            If Not oConnection.GetConnection() Then
                GetStructureDetail = Nothing
            End If
            GetStructureDetail = New S_PENDAFTARAN_PRB_D
        End Function
        Public Function GetStructureDetailList() As List(Of S_PENDAFTARAN_PRB_D)
            If Not oConnection.GetConnection() Then
                GetStructureDetailList = Nothing
            End If
            GetStructureDetailList = New List(Of S_PENDAFTARAN_PRB_D)
        End Function
        Public Function GetData() As List(Of S_PENDAFTARAN_PRB)
            If Not oConnection.GetConnection() Then
                GetData = Nothing
                Exit Function
            End If
            GetData = oConnection.db.S_PENDAFTARAN_PRBs.OrderByDescending(Function(x) x.KDPRB).ToList()
        End Function
        Public Function GetData(ByVal Parameter As String) As S_PENDAFTARAN_PRB
            If Not oConnection.GetConnection() Then
                GetData = Nothing
                Exit Function
            End If
            GetData = oConnection.db.S_PENDAFTARAN_PRBs.FirstOrDefault(Function(x) x.KDPRB = Parameter)
        End Function
        Public Function GetData_(ByVal Parameter As String) As S_PENDAFTARAN_PRB
            If Not oConnection.GetConnection() Then
                GetData_ = Nothing
                Exit Function
            End If
            GetData_ = oConnection.db.S_PENDAFTARAN_PRBs.FirstOrDefault(Function(x) x.KDPENDAFTARAN = Parameter)
        End Function
        Public Function GetDataDetail() As List(Of S_PENDAFTARAN_PRB_D)
            If Not oConnection.GetConnection() Then
                GetDataDetail = Nothing
                Exit Function
            End If
            GetDataDetail = oConnection.db.S_PENDAFTARAN_PRB_Ds.ToList()
        End Function
        Public Function GetDataDetail(ByVal sKDPRB As String) As List(Of S_PENDAFTARAN_PRB_D)
            If Not oConnection.GetConnection() Then
                GetDataDetail = Nothing
                Exit Function
            End If
            GetDataDetail = oConnection.db.S_PENDAFTARAN_PRB_Ds.Where(Function(x) x.KDPRB = sKDPRB).ToList()
        End Function
        Public Function InsertData(ByVal entity As S_PENDAFTARAN_PRB, ByVal entityDetail As List(Of S_PENDAFTARAN_PRB_D)) As Boolean
            Try
                If Not oConnection.GetConnection() Then
                    InsertData = False
                    Exit Function
                End If

                sREFERENCE = entity.KDPRB
                sSTATUS = "INSERT"

                Try
                    sLASTNUMBER = oCounter.GetLastNumber(sMODUL, entity.DATECREATED)

                    If sLASTNUMBER = 0 Then
                        Try
                            oCounter.InsertData(sMODUL, entity.DATECREATED)
                            sLASTNUMBER = oCounter.GetLastNumber(sMODUL, entity.DATECREATED)
                        Catch ex As Exception
                            sLASTNUMBER = 0
                        End Try
                    End If

                    entity.KDPRB = AutoNumber(sMODUL, sLASTNUMBER + 1, entity.DATECREATED)
                Catch ex As Exception
                    oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                    Throw ex
                End Try

                Try
                    oCounter.UpdateData(sMODUL, sLASTNUMBER + 1, Month(entity.DATECREATED), Year(entity.DATECREATED))
                Catch ex As Exception
                    oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                    Throw ex
                End Try

                Try
                    For Each iLoop In entityDetail
                        iLoop.KDPRB = entity.KDPRB
                    Next

                    oConnection.db.S_PENDAFTARAN_PRBs.InsertOnSubmit(entity)
                    If entityDetail.Count > 0 Then
                        oConnection.db.S_PENDAFTARAN_PRB_Ds.InsertAllOnSubmit(entityDetail)
                    End If
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
        Public Function UpdateData(ByVal entity As S_PENDAFTARAN_PRB, ByVal entityDetail As List(Of S_PENDAFTARAN_PRB_D)) As Boolean
            Try
                If Not oConnection.GetConnection() Then
                    UpdateData = False
                    Exit Function
                End If

                sREFERENCE = entity.KDPRB
                sSTATUS = "UPDATE"

                Dim ds = oConnection.db.S_PENDAFTARAN_PRBs.FirstOrDefault(Function(x) x.KDPRB = entity.KDPRB)
                Dim dsDetail = oConnection.db.S_PENDAFTARAN_PRB_Ds.Where(Function(x) x.KDPRB = entity.KDPRB)

                Try
                    oConnection.db.S_PENDAFTARAN_PRBs.DeleteOnSubmit(ds)
                    oConnection.db.S_PENDAFTARAN_PRBs.InsertOnSubmit(entity)
                    oConnection.db.S_PENDAFTARAN_PRB_Ds.DeleteAllOnSubmit(dsDetail)
                    oConnection.db.S_PENDAFTARAN_PRB_Ds.InsertAllOnSubmit(entityDetail)
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

                Dim ds = oConnection.db.S_PENDAFTARAN_PRBs.FirstOrDefault(Function(x) x.KDPRB = Parameter)
                Dim dsDetail = oConnection.db.S_PENDAFTARAN_PRB_Ds.Where(Function(x) x.KDPRB = Parameter)

                Try
                    oConnection.db.S_PENDAFTARAN_PRBs.DeleteOnSubmit(ds)
                    oConnection.db.S_PENDAFTARAN_PRB_Ds.DeleteAllOnSubmit(dsDetail)
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