Imports DataAccess.My.Resources

Namespace Transaksi
    Public Class clsDigital_IGD_03
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

            sMODUL = "DIGITAL_8"
        End Sub
        Protected Overrides Sub Finalize()
            MyBase.Finalize()
            oConnection = Nothing
            GC.SuppressFinalize(Me)
        End Sub
        Public Function GetStructureHeader() As S_DIGITAL_IGD_03
            If Not oConnection.GetConnection() Then
                GetStructureHeader = Nothing
            End If
            GetStructureHeader = New S_DIGITAL_IGD_03
        End Function
        Public Function GetData(ByVal sKDPENDAFTARAN As String) As S_DIGITAL_IGD_03
            If Not oConnection.GetConnection() Then
                GetData = Nothing
                Exit Function
            End If
            GetData = oConnection.db.S_DIGITAL_IGD_03s.FirstOrDefault(Function(x) x.KDPENDAFTARAN = sKDPENDAFTARAN)
        End Function
        Public Function GetStructureDetail() As S_DIGITAL_IGD_03_DETIL
            If Not oConnection.GetConnection Then
                GetStructureDetail = Nothing
            End If
            GetStructureDetail = New S_DIGITAL_IGD_03_DETIL
        End Function
        Public Function GetStructureDetailList() As List(Of S_DIGITAL_IGD_03_DETIL)
            If Not oConnection.GetConnection Then
                GetStructureDetailList = Nothing
            End If
            GetStructureDetailList = New List(Of S_DIGITAL_IGD_03_DETIL)
        End Function
        Public Function GetDataDetail() As List(Of S_DIGITAL_IGD_03_DETIL)
            If Not oConnection.GetConnection Then
                GetDataDetail = Nothing
                Exit Function
            End If
            GetDataDetail = oConnection.db.S_DIGITAL_IGD_03_DETILs.ToList()
        End Function
        Public Function GetDataDetail(ByVal Parameter As String) As List(Of S_DIGITAL_IGD_03_DETIL)
            If Not oConnection.GetConnection Then
                GetDataDetail = Nothing
                Exit Function
            End If
            GetDataDetail = oConnection.db.S_DIGITAL_IGD_03_DETILs.Where(Function(x) x.KDPENDAFTARAN = Parameter).ToList()
        End Function
        'Public Function GetDataByRM(ByVal sKDCUSTOMER As String) As List(Of S_DIGITAL_IGD_03)
        '    If Not oConnection.GetConnection() Then
        '        GetDataByRM = Nothing
        '        Exit Function
        '    End If
        '    GetDataByRM = oConnection.db.S_DIGITAL_IGD_03s.Where(Function(x) x.KDCUSTOMER = sKDCUSTOMER).OrderByDescending(Function(x) x.KDPENDAFTARAN).ToList()
        'End Function
        Public Function InsertData(ByVal entity As S_DIGITAL_IGD_03, ByVal entityDetail As List(Of S_DIGITAL_IGD_03_DETIL)) As Boolean
            Try
                If Not oConnection.GetConnection() Then
                    InsertData = False
                    Exit Function
                End If

                sREFERENCE = entity.KDPENDAFTARAN
                sSTATUS = "INSERT"

                'sLASTNUMBER = oCounter.GetLastNumber(sMODUL, entity.DATE)
                'If sLASTNUMBER = 0 Then
                '    Try
                '        oCounter.InsertData(sMODUL, entity.DATE)
                '        sLASTNUMBER = oCounter.GetLastNumber(sMODUL, entity.DATE)
                '    Catch ex As Exception
                '        sLASTNUMBER = 0
                '    End Try
                'End If

                'entity.KDPENDAFTARAN = AutoNumber(sMODUL, sLASTNUMBER + 1, entity.DATE)

                Try
                    oConnection.db.S_DIGITAL_IGD_03s.InsertOnSubmit(entity)
                    oConnection.db.S_DIGITAL_IGD_03_DETILs.InsertAllOnSubmit(entityDetail)
                Catch ex As Exception
                    oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                    Throw ex
                End Try

                'Try
                '    oCounter.UpdateData(sMODUL, sLASTNUMBER + 1, Month(entity.DATE), Year(entity.DATE))
                'Catch ex As Exception
                '    oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                '    Throw ex
                'End Try

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
        Public Function UpdateData(ByVal entity As S_DIGITAL_IGD_03, ByVal entityDetail As List(Of S_DIGITAL_IGD_03_DETIL)) As Boolean
            Try
                If Not oConnection.GetConnection() Then
                    UpdateData = False
                    Exit Function
                End If

                sREFERENCE = entity.KDPENDAFTARAN
                sSTATUS = "UPDATE"

                Dim ds = oConnection.db.S_DIGITAL_IGD_03s.FirstOrDefault(Function(x) x.KDPENDAFTARAN = entity.KDPENDAFTARAN)
                Dim dsDetail = oConnection.db.S_DIGITAL_IGD_03_DETILs.Where(Function(x) x.KDPENDAFTARAN = entity.KDPENDAFTARAN)

                Try
                    oConnection.db.S_DIGITAL_IGD_03s.DeleteOnSubmit(ds)
                    oConnection.db.S_DIGITAL_IGD_03s.InsertOnSubmit(entity)

                    oConnection.db.S_DIGITAL_IGD_03_DETILs.DeleteAllOnSubmit(dsDetail)
                    oConnection.db.S_DIGITAL_IGD_03_DETILs.InsertAllOnSubmit(entityDetail)
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
    End Class
End Namespace