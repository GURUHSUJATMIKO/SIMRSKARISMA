Imports System.Threading

Namespace Inventory
    Public Class clsDigital_IGD_01_GERD
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
            sMODUL = "GERD"

            oCounter = New Setting.clsCounter
        End Sub
        Protected Overrides Sub Finalize()
            MyBase.Finalize()
            oConnection = Nothing
            GC.SuppressFinalize(Me)
        End Sub
        Public Function GetStructureHeader() As S_DIGITAL_IGD_01_GERD_H
            If Not oConnection.GetConnection() Then
                GetStructureHeader = Nothing
            End If
            GetStructureHeader = New S_DIGITAL_IGD_01_GERD_H
        End Function
        Public Function GetStructureDetail() As S_DIGITAL_IGD_01_GERD_D
            If Not oConnection.GetConnection() Then
                GetStructureDetail = Nothing
            End If
            GetStructureDetail = New S_DIGITAL_IGD_01_GERD_D
        End Function
        Public Function GetStructureDetailList() As List(Of S_DIGITAL_IGD_01_GERD_D)
            If Not oConnection.GetConnection() Then
                GetStructureDetailList = Nothing
            End If
            GetStructureDetailList = New List(Of S_DIGITAL_IGD_01_GERD_D)
        End Function
        Public Function GetData() As List(Of S_DIGITAL_IGD_01_GERD_H)
            If Not oConnection.GetConnection() Then
                GetData = Nothing
                Exit Function
            End If
            GetData = oConnection.db.S_DIGITAL_IGD_01_GERD_Hs.OrderByDescending(Function(x) x.KDGERD).ToList()
        End Function
        Public Function GetData(ByVal sKDGERD As String) As S_DIGITAL_IGD_01_GERD_H
            If Not oConnection.GetConnection() Then
                GetData = Nothing
                Exit Function
            End If
            GetData = oConnection.db.S_DIGITAL_IGD_01_GERD_Hs.FirstOrDefault(Function(x) x.KDGERD = sKDGERD)
        End Function
        Public Function GetDataByRM(ByVal sKDCUSTOMER As String) As List(Of S_DIGITAL_IGD_01_GERD_H)
            If Not oConnection.GetConnection() Then
                GetDataByRM = Nothing
                Exit Function
            End If
            GetDataByRM = oConnection.db.S_DIGITAL_IGD_01_GERD_Hs.Where(Function(x) x.NOREKAMMEDIS = sKDCUSTOMER).OrderByDescending(Function(x) x.KDGERD).ToList()
        End Function
        Public Function GetDataByRMRi(ByVal sKDCUSTOMER As String) As List(Of S_DIGITAL_IGD_01_GERD_H)
            If Not oConnection.GetConnection() Then
                GetDataByRMRi = Nothing
                Exit Function
            End If
            GetDataByRMRi = oConnection.db.S_DIGITAL_IGD_01_GERD_Hs.Where(Function(x) x.NOREKAMMEDIS = sKDCUSTOMER And x.KDPENDAFTARAN.Contains("RI")).OrderByDescending(Function(x) x.KDGERD).ToList()
        End Function
        Public Function GetDataByKdreg(ByVal sKDGERD As String) As S_DIGITAL_IGD_01_GERD_H
            If Not oConnection.GetConnection() Then
                GetDataByKdreg = Nothing
                Exit Function
            End If
            GetDataByKdreg = oConnection.db.S_DIGITAL_IGD_01_GERD_Hs.FirstOrDefault(Function(x) x.KDPENDAFTARAN = sKDGERD)
        End Function
        Public Function GetDataByListKDPENDAFTARAN(ByVal sKDPENDAFTARAN As String) As List(Of S_DIGITAL_IGD_01_GERD_H)
            If Not oConnection.GetConnection() Then
                GetDataByListKDPENDAFTARAN = Nothing
                Exit Function
            End If
            GetDataByListKDPENDAFTARAN = oConnection.db.S_DIGITAL_IGD_01_GERD_Hs.Where(Function(x) x.KDPENDAFTARAN = sKDPENDAFTARAN).OrderByDescending(Function(x) x.KDGERD).ToList()
        End Function
        Public Function GetDataDetail() As List(Of S_DIGITAL_IGD_01_GERD_D)
            If Not oConnection.GetConnection() Then
                GetDataDetail = Nothing
                Exit Function
            End If
            GetDataDetail = oConnection.db.S_DIGITAL_IGD_01_GERD_Ds.ToList()
        End Function
        Public Function GetDataDetail(ByVal sKDGERD As String) As List(Of S_DIGITAL_IGD_01_GERD_D)
            If Not oConnection.GetConnection() Then
                GetDataDetail = Nothing
                Exit Function
            End If
            GetDataDetail = oConnection.db.S_DIGITAL_IGD_01_GERD_Ds.Where(Function(x) x.KDGERD = sKDGERD).ToList()
        End Function
        Public Function InsertData(ByVal entity As S_DIGITAL_IGD_01_GERD_H, ByVal entityDetail As List(Of S_DIGITAL_IGD_01_GERD_D)) As Boolean
            Try
                If Not oConnection.GetConnection() Then
                    InsertData = False
                    Exit Function
                End If

                sREFERENCE = entity.KDGERD
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

                    entity.KDGERD = AutoNumber(sMODUL, sLASTNUMBER + 1, entity.DATE)
                    For Each iLoop In entityDetail
                        iLoop.KDGERD = entity.KDGERD
                    Next
                Catch ex As Exception
                    oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                    Throw ex
                End Try

                Try
                    oConnection.db.S_DIGITAL_IGD_01_GERD_Hs.InsertOnSubmit(entity)
                    oConnection.db.S_DIGITAL_IGD_01_GERD_Ds.InsertAllOnSubmit(entityDetail)
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
        Public Function UpdateData(ByVal entity As S_DIGITAL_IGD_01_GERD_H, ByVal entityDetail As List(Of S_DIGITAL_IGD_01_GERD_D)) As Boolean
            Try
                If Not oConnection.GetConnection() Then
                    UpdateData = False
                    Exit Function
                End If

                sREFERENCE = entity.KDGERD
                sSTATUS = "UPDATE"

                Dim ds = oConnection.db.S_DIGITAL_IGD_01_GERD_Hs.FirstOrDefault(Function(x) x.KDGERD = entity.KDGERD)

                Try
                    oConnection.db.S_DIGITAL_IGD_01_GERD_Hs.DeleteOnSubmit(ds)
                    oConnection.db.S_DIGITAL_IGD_01_GERD_Hs.InsertOnSubmit(entity)
                Catch ex As Exception
                    oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                    Throw ex
                End Try

                Dim dsDetail = oConnection.db.S_DIGITAL_IGD_01_GERD_Ds.Where(Function(x) x.KDGERD = entity.KDGERD)

                Try
                    oConnection.db.S_DIGITAL_IGD_01_GERD_Ds.DeleteAllOnSubmit(dsDetail)
                    oConnection.db.S_DIGITAL_IGD_01_GERD_Ds.InsertAllOnSubmit(entityDetail)
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
        Public Function DeleteData(ByVal sKDGERD As String) As Boolean
            Try
                If Not oConnection.GetConnection() Then
                    DeleteData = False
                    Exit Function
                End If

                sREFERENCE = sKDGERD
                sSTATUS = "DELETE"

                Dim ds = oConnection.db.S_DIGITAL_IGD_01_GERD_Hs.FirstOrDefault(Function(x) x.KDGERD = sKDGERD)
                Dim dsDetail = oConnection.db.S_DIGITAL_IGD_01_GERD_Ds.Where(Function(x) x.KDGERD = sKDGERD)

                Try
                    oConnection.db.S_DIGITAL_IGD_01_GERD_Hs.DeleteOnSubmit(ds)
                    oConnection.db.S_DIGITAL_IGD_01_GERD_Ds.DeleteAllOnSubmit(dsDetail)

                    'ds.ISDELETE = True
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

                DeleteData = True
            Catch ex As Exception
                DeleteData = False
                oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                Throw ex
            End Try
        End Function
    End Class
End Namespace