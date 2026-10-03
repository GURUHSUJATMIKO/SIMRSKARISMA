Imports DataAccess.My.Resources

Namespace Digital
    Public Class clsDigital_MPP
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

            sMODUL = "MPP"
        End Sub
        Protected Overrides Sub Finalize()
            MyBase.Finalize()
            oConnection = Nothing
            GC.SuppressFinalize(Me)
        End Sub
        Public Function GetStructureHeader() As S_DIGITAL_MPP_H
            If Not oConnection.GetConnection() Then
                GetStructureHeader = Nothing
            End If
            GetStructureHeader = New S_DIGITAL_MPP_H
        End Function
        Public Function GetStructureDetail() As S_DIGITAL_MPP_D
            If Not oConnection.GetConnection() Then
                GetStructureDetail = Nothing
            End If
            GetStructureDetail = New S_DIGITAL_MPP_D
        End Function
        Public Function GetStructureDetailList() As List(Of S_DIGITAL_MPP_D)
            If Not oConnection.GetConnection() Then
                GetStructureDetailList = Nothing
            End If
            GetStructureDetailList = New List(Of S_DIGITAL_MPP_D)
        End Function
        Public Function GetData(ByVal sKDPENDAFTARAN As String) As S_DIGITAL_MPP_H
            If Not oConnection.GetConnection() Then
                GetData = Nothing
                Exit Function
            End If
            GetData = oConnection.db.S_DIGITAL_MPP_Hs.FirstOrDefault(Function(x) x.KDPENDAFTARAN = sKDPENDAFTARAN)
        End Function
        Public Function GetDataDetail(ByVal sKDPENDAFTARAN As String) As List(Of S_DIGITAL_MPP_D)
            If Not oConnection.GetConnection() Then
                GetDataDetail = Nothing
                Exit Function
            End If
            GetDataDetail = oConnection.db.S_DIGITAL_MPP_Ds.Where(Function(x) x.KDPENDAFTARAN = sKDPENDAFTARAN).ToList()
        End Function
        Public Function InsertData(ByVal entity As S_DIGITAL_MPP_H, ByVal entityDetail As List(Of S_DIGITAL_MPP_D)) As Boolean
            Try
                If Not oConnection.GetConnection() Then
                    InsertData = False
                    Exit Function
                End If

                sREFERENCE = entity.KDPENDAFTARAN
                sSTATUS = "INSERT"

                Try
                    oConnection.db.S_DIGITAL_MPP_Hs.InsertOnSubmit(entity)
                    If entityDetail.Count > 0 Then
                        oConnection.db.S_DIGITAL_MPP_Ds.InsertAllOnSubmit(entityDetail)
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
        Public Function UpdateData(ByVal entity As S_DIGITAL_MPP_H, ByVal entityDetail As List(Of S_DIGITAL_MPP_D)) As Boolean
            Try
                If Not oConnection.GetConnection() Then
                    UpdateData = False
                    Exit Function
                End If

                sREFERENCE = entity.KDPENDAFTARAN
                sSTATUS = "UPDATE"

                Dim ds = oConnection.db.S_DIGITAL_MPP_Hs.FirstOrDefault(Function(x) x.KDPENDAFTARAN = entity.KDPENDAFTARAN)
                Dim dsDetail = oConnection.db.S_DIGITAL_MPP_Ds.Where(Function(x) x.KDPENDAFTARAN = entity.KDPENDAFTARAN)

                Try
                    oConnection.db.S_DIGITAL_MPP_Hs.DeleteOnSubmit(ds)
                    oConnection.db.S_DIGITAL_MPP_Hs.InsertOnSubmit(entity)

                    If dsDetail.Count > 0 Then
                        oConnection.db.S_DIGITAL_MPP_Ds.DeleteAllOnSubmit(dsDetail)
                    End If
                    If entityDetail.Count > 0 Then
                        oConnection.db.S_DIGITAL_MPP_Ds.InsertAllOnSubmit(entityDetail)
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

                Dim ds = oConnection.db.S_DIGITAL_MPP_Hs.FirstOrDefault(Function(x) x.KDPENDAFTARAN = Parameter)

                Try
                    oConnection.db.S_DIGITAL_MPP_Hs.DeleteOnSubmit(ds)
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