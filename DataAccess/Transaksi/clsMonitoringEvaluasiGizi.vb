Imports DataAccess.My.Resources

Namespace Digital
    Public Class clsMonitoringEvaluasiGizi
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

            sMODUL = "MONEVGIZI"
        End Sub
        Protected Overrides Sub Finalize()
            MyBase.Finalize()
            oConnection = Nothing
            GC.SuppressFinalize(Me)
        End Sub
        Public Function GetStructureHeader() As S_DIGITAL_MONITORINGEVALUASIGIZI_H
            If Not oConnection.GetConnection() Then
                GetStructureHeader = Nothing
            End If
            GetStructureHeader = New S_DIGITAL_MONITORINGEVALUASIGIZI_H
        End Function
        Public Function GetStructureDetail() As S_DIGITAL_MONITORINGEVALUASIGIZI_D
            If Not oConnection.GetConnection() Then
                GetStructureDetail = Nothing
            End If
            GetStructureDetail = New S_DIGITAL_MONITORINGEVALUASIGIZI_D
        End Function
        Public Function GetStructureDetailList() As List(Of S_DIGITAL_MONITORINGEVALUASIGIZI_D)
            If Not oConnection.GetConnection() Then
                GetStructureDetailList = Nothing
            End If
            GetStructureDetailList = New List(Of S_DIGITAL_MONITORINGEVALUASIGIZI_D)
        End Function

        Public Function GetData() As List(Of S_DIGITAL_MONITORINGEVALUASIGIZI_H)
            If Not oConnection.GetConnection Then
                GetData = Nothing
                Exit Function
            End If
            GetData = oConnection.db.S_DIGITAL_MONITORINGEVALUASIGIZI_Hs.OrderBy(Function(x) x.KDMONEVG).ToList()
        End Function
        Public Function GetData(ByVal sKDMONEVG As String) As S_DIGITAL_MONITORINGEVALUASIGIZI_H
            If Not oConnection.GetConnection() Then
                GetData = Nothing
                Exit Function
            End If
            GetData = oConnection.db.S_DIGITAL_MONITORINGEVALUASIGIZI_Hs.FirstOrDefault(Function(x) x.KDMONEVG = sKDMONEVG)
        End Function
        Public Function GetDataDetail() As List(Of S_DIGITAL_MONITORINGEVALUASIGIZI_D)
            If Not oConnection.GetConnection Then
                GetDataDetail = Nothing
                Exit Function
            End If
            GetDataDetail = oConnection.db.S_DIGITAL_MONITORINGEVALUASIGIZI_Ds.ToList()
        End Function
        Public Function GetDataDetail(ByVal Parameter As String) As List(Of S_DIGITAL_MONITORINGEVALUASIGIZI_D)
            If Not oConnection.GetConnection Then
                GetDataDetail = Nothing
                Exit Function
            End If
            GetDataDetail = oConnection.db.S_DIGITAL_MONITORINGEVALUASIGIZI_Ds.Where(Function(x) x.KDMONEVG = Parameter).OrderBy(Function(x) x.SEQ).ToList()
        End Function
        Public Function GetDataByKDREG(ByVal sKDREG As String) As List(Of S_DIGITAL_MONITORINGEVALUASIGIZI_H)
            If Not oConnection.GetConnection() Then
                GetDataByKDREG = Nothing
                Exit Function
            End If
            GetDataByKDREG = oConnection.db.S_DIGITAL_MONITORINGEVALUASIGIZI_Hs.Where(Function(x) x.KDREG = sKDREG).OrderByDescending(Function(x) x.KDREG).ToList()
        End Function
        Public Function InsertData(ByVal entity As S_DIGITAL_MONITORINGEVALUASIGIZI_H, ByVal entityDetail As List(Of S_DIGITAL_MONITORINGEVALUASIGIZI_D)) As Boolean
            Try
                If Not oConnection.GetConnection() Then
                    InsertData = False
                    Exit Function
                End If

                sREFERENCE = entity.KDMONEVG
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

                entity.KDMONEVG = AutoNumber(sMODUL, sLASTNUMBER + 1, entity.DATE)

                If entityDetail IsNot Nothing Then
                    For Each iLoop In entityDetail
                        iLoop.KDMONEVG = entity.KDMONEVG
                    Next
                End If

                Try
                    oConnection.db.S_DIGITAL_MONITORINGEVALUASIGIZI_Hs.InsertOnSubmit(entity)
                    oConnection.db.S_DIGITAL_MONITORINGEVALUASIGIZI_Ds.InsertAllOnSubmit(entityDetail)
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
        Public Function UpdateData(ByVal Kode As String, ByVal entity As S_DIGITAL_MONITORINGEVALUASIGIZI_H, ByVal entityDetail As List(Of S_DIGITAL_MONITORINGEVALUASIGIZI_D)) As Boolean
            Try
                If Not oConnection.GetConnection() Then
                    UpdateData = False
                    Exit Function
                End If

                sREFERENCE = entity.KDMONEVG
                sSTATUS = "UPDATE"

                Dim ds = oConnection.db.S_DIGITAL_MONITORINGEVALUASIGIZI_Hs.FirstOrDefault(Function(x) x.KDMONEVG = Kode)
                Dim dsDetail = oConnection.db.S_DIGITAL_MONITORINGEVALUASIGIZI_Ds.Where(Function(x) x.KDMONEVG = Kode)

                Try
                    oConnection.db.S_DIGITAL_MONITORINGEVALUASIGIZI_Hs.DeleteOnSubmit(ds)
                    oConnection.db.S_DIGITAL_MONITORINGEVALUASIGIZI_Hs.InsertOnSubmit(entity)

                    oConnection.db.S_DIGITAL_MONITORINGEVALUASIGIZI_Ds.DeleteAllOnSubmit(dsDetail)
                    oConnection.db.S_DIGITAL_MONITORINGEVALUASIGIZI_Ds.InsertAllOnSubmit(entityDetail)
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

                Dim ds = oConnection.db.S_DIGITAL_MONITORINGEVALUASIGIZI_Hs.FirstOrDefault(Function(x) x.KDMONEVG = Parameter)
                Dim dsDetail = oConnection.db.S_DIGITAL_MONITORINGEVALUASIGIZI_Ds.Where(Function(x) x.KDMONEVG = Parameter)

                Try
                    oConnection.db.S_DIGITAL_MONITORINGEVALUASIGIZI_Hs.DeleteOnSubmit(ds)
                    oConnection.db.S_DIGITAL_MONITORINGEVALUASIGIZI_Ds.DeleteAllOnSubmit(dsDetail)
                Catch ex As Exception
                    oError.InsertData("S_DIGITAL_MONITORINGEVALUASIGIZI_H", "DELETEDATA", ex.ToString, Parameter)
                    Throw ex
                End Try
                Try
                    oConnection.db.SubmitChanges()
                Catch ex As Exception
                    oError.InsertData("S_DIGITAL_MONITORINGEVALUASIGIZI_H", "DELETEDATA", ex.ToString, Parameter)
                    Throw ex
                End Try

                DeleteData = True
            Catch ex As Exception
                DeleteData = False
                oError.InsertData("S_DIGITAL_MONITORINGEVALUASIGIZI_H", "DELETEDATA", ex.ToString, Parameter)
                Throw ex
            End Try
        End Function
    End Class
End Namespace