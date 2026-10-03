Imports System.Threading

Namespace Admission
    Public Class clsPendafataranPenunjangHariIni
        Public oConnection As Setting.clsConnectionAdmision = Nothing
        Public oError As Setting.clsError = Nothing
        Public sMODUL As String = ""
        Public sREFERENCE As String = ""
        Public sSTATUS As String = ""
        Public sLASTNUMBER As Integer = 0

        Public oCounter As Setting.clsCounter = Nothing

        Public Sub New()
            oConnection = New Setting.clsConnectionAdmision
            oError = New Setting.clsError
            sMODUL = "PENUNJANGHARIINI"

            oCounter = New Setting.clsCounter
        End Sub
        Protected Overrides Sub Finalize()
            MyBase.Finalize()
            oConnection = Nothing
            GC.SuppressFinalize(Me)
        End Sub
        Public Function GetStructureHeader() As S_PENDAFTARAN_PENUNJANGHARIINISELESAI
            If Not oConnection.GetConnection() Then
                GetStructureHeader = Nothing
            End If
            GetStructureHeader = New S_PENDAFTARAN_PENUNJANGHARIINISELESAI
        End Function
        Public Function GetData() As List(Of S_PENDAFTARAN_PENUNJANGHARIINISELESAI)
            If Not oConnection.GetConnection() Then
                GetData = Nothing
                Exit Function
            End If
            GetData = oConnection.db.S_PENDAFTARAN_PENUNJANGHARIINISELESAIs.OrderByDescending(Function(x) x.KDREG).ToList()
        End Function
         Public Function GetData(ByVal Parameter As String) As S_PENDAFTARAN_PENUNJANGHARIINISELESAI
            If Not oConnection.GetConnection() Then
                GetData = Nothing
                Exit Function
            End If
            GetData = oConnection.db.S_PENDAFTARAN_PENUNJANGHARIINISELESAIs.FirstOrDefault(Function(x) x.KDREG = Parameter)
        End Function
        Public Function GetDataUmpanBalik(ByVal DiagnosaProsedur As String) As S_UMPANBALIK
            If Not oConnection.GetConnection() Then
                GetDataUmpanBalik = Nothing
                Exit Function
            End If
            GetDataUmpanBalik = oConnection.db.S_UMPANBALIKs.Where(Function(x) x.DIAGLIST & x.PROCLIST = DiagnosaProsedur).OrderByDescending(Function(x) x.DISCHARGE_DATE).FirstOrDefault()
        End Function
        Public Function InsertData(ByVal entity As S_PENDAFTARAN_PENUNJANGHARIINISELESAI) As Boolean
            Try
                If Not oConnection.GetConnection() Then
                    InsertData = False
                    Exit Function
                End If

                sREFERENCE = entity.KDREG
                sSTATUS = "INSERT"

                Try
                    oConnection.db.S_PENDAFTARAN_PENUNJANGHARIINISELESAIs.InsertOnSubmit(entity)
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
        Public Function UpdateData(ByVal entity As S_PENDAFTARAN_PENUNJANGHARIINISELESAI) As Boolean
            Try
                If Not oConnection.GetConnection() Then
                    UpdateData = False
                    Exit Function
                End If

                sREFERENCE = entity.KDREG
                sSTATUS = "UPDATE"

                Dim ds = oConnection.db.S_PENDAFTARAN_PENUNJANGHARIINISELESAIs.FirstOrDefault(Function(x) x.KDREG = entity.KDREG)

                Try
                    oConnection.db.S_PENDAFTARAN_PENUNJANGHARIINISELESAIs.DeleteOnSubmit(ds)
                    oConnection.db.S_PENDAFTARAN_PENUNJANGHARIINISELESAIs.InsertOnSubmit(entity)

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

                Dim ds = oConnection.db.S_PENDAFTARAN_PENUNJANGHARIINISELESAIs.FirstOrDefault(Function(x) x.KDREG.Contains(Parameter))

                If ds IsNot Nothing Then
                    Try
                        oConnection.db.S_PENDAFTARAN_PENUNJANGHARIINISELESAIs.DeleteOnSubmit(ds)
                    Catch ex As Exception
                        oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                        Throw ex
                    End Try

                End If

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
        'Public Function UpdateDataFix(ByVal isNew As Boolean) As Boolean
        '    Try
        '        If Not oConnection.GetConnection Then
        '            UpdateDataFix = False
        '            Exit Function
        '        End If


        '        Dim entity = oConnection.db.S_PENDAFTARAN_PENUNJANGHARIINISELESAIs

        '        For Each iLoop In entity
        '            Dim sKDREG = iLoop.KDREG
        '            Dim entityDetail = oConnection.db.F_PENDAFTARAN_Ds.Where(Function(x) x.KDREG = sKDREG And x.SEQ < 100)
        '            Dim entityDetail_R = oConnection.db.F_PENDAFTARAN_Ds.Where(Function(x) x.KDREG = sKDREG And x.SEQ >= 100)

        '            Try
        '                If Not AutoJournal(iLoop, isNew) Then
        '                    Return False
        '                End If
        '            Catch ex As Exception
        '                Throw ex
        '            End Try

        '            Thread.Sleep(100)
        '        Next

        '        UpdateDataFix = True
        '    Catch ex As Exception
        '        UpdateDataFix = False
        '        Throw ex
        '    End Try
        'End Function
    End Class
End Namespace