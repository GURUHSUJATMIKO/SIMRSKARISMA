Imports DataAccess.My.Resources
Imports System.Data.SqlClient

Namespace Reference
    Public Class clsDepartmentBPJSRME
        Public oConnection As Setting.clsConnectionMain2 = Nothing
        Public oError As Setting.clsError = Nothing

        Public sMODUL As String = ""
        Public sREFERENCE As String = ""
        Public sSTATUS As String = ""
        Public sLASTNUMBER As Integer = 0

        Public Sub New(Optional ByVal sConnection As String = "")
            oConnection = New Setting.clsConnectionMain2
            oError = New Setting.clsError

            sMODUL = "DEPARTMENTBPJSRME"
        End Sub
        Protected Overrides Sub Finalize()
            MyBase.Finalize()
            oConnection = Nothing
            GC.SuppressFinalize(Me)
        End Sub
        Public Function GetStructureHeader() As M_DEPARTMENT_BPJSRME
            If Not oConnection.GetConnection() Then
                GetStructureHeader = Nothing
            End If
            GetStructureHeader = New M_DEPARTMENT_BPJSRME
        End Function
        Public Function GetData() As List(Of M_DEPARTMENT_BPJSRME)
            If Not oConnection.GetConnection() Then
                GetData = Nothing
                Exit Function
            End If
            GetData = oConnection.db.M_DEPARTMENT_BPJSRMEs.OrderBy(Function(x) x.KDDEPARTMENT).ToList()
        End Function
        Public Function GetData(ByVal sKDDEPARTMENT As Integer, ByVal sConnOld As String) As String
            'If Not oConnection.GetConnection() Then
            '    GetData = Nothing
            '    Exit Function
            'End If
            'GetData = oConnection.db.M_DEPARTMENT_BPJSRMEs.FirstOrDefault(Function(x) x.KDDEPARTMENT = sKDDEPARTMENT)
            Try
                GetData = ""

                Dim oConn As New SqlConnection
                Dim oComm As New SqlCommand
                Dim da As SqlDataAdapter
                Dim ds As New DataSet
                Dim SQL As String
                oConn = New SqlConnection(sConnOld)

                If oConn.State = ConnectionState.Closed Then
                    oConn.Open()
                End If

                SQL = "SELECT "
                SQL &= "* "
                SQL &= "FROM "
                SQL &= "M_DEPARTMENT_BPJSRME A "
                SQL &= "WHERE "
                SQL &= "A.KDDEPARTMENT = " & sKDDEPARTMENT & " "

                oComm.Connection = oConn
                oComm.CommandText = SQL
                oComm.CommandTimeout = 120
                oComm.CommandType = CommandType.Text

                da = New SqlDataAdapter(oComm)
                da.Fill(ds, "M_DEPARTMENT_BPJSRME")

                If oConn.State = ConnectionState.Open Then
                    oConn.Close()
                End If

                For iLoop As Integer = 0 To ds.Tables("M_DEPARTMENT_BPJSRME").Rows.Count - 1
                    With ds.Tables("M_DEPARTMENT_BPJSRME")
                        GetData = .Rows(iLoop)("ID")
                    End With
                Next

            Catch oErr As Exception
                GetData = ""
                MsgBox("Load Data Dokter: " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation)
            End Try
        End Function
        Public Function InsertData(ByVal entity As M_DEPARTMENT_BPJSRME) As Boolean
            Try
                If Not oConnection.GetConnection() Then
                    InsertData = False
                    Exit Function
                End If

                sREFERENCE = entity.KDDEPARTMENT
                sSTATUS = "INSERT"

                Try
                    oConnection.db.M_DEPARTMENT_BPJSRMEs.InsertOnSubmit(entity)
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
        Public Function UpdateData(ByVal entity As M_DEPARTMENT_BPJSRME) As Boolean
            Try
                If Not oConnection.GetConnection() Then
                    UpdateData = False
                    Exit Function
                End If

                sREFERENCE = entity.KDDEPARTMENT
                sSTATUS = "UPDATE"

                Dim ds = oConnection.db.M_DEPARTMENT_BPJSRMEs.FirstOrDefault(Function(x) x.KDDEPARTMENT = entity.KDDEPARTMENT)

                Try
                    oConnection.db.M_DEPARTMENT_BPJSRMEs.DeleteOnSubmit(ds)
                    oConnection.db.M_DEPARTMENT_BPJSRMEs.InsertOnSubmit(entity)
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
        Public Function DeleteData(ByVal sKDDEPARTMENT As String) As Boolean
            Try
                If Not oConnection.GetConnection() Then
                    DeleteData = False
                    Exit Function
                End If

                sREFERENCE = sKDDEPARTMENT
                sSTATUS = "DELETE"

                Dim ds = oConnection.db.M_DEPARTMENT_BPJSRMEs.FirstOrDefault(Function(x) x.KDDEPARTMENT = sKDDEPARTMENT)

                Try
                    oConnection.db.M_DEPARTMENT_BPJSRMEs.DeleteOnSubmit(ds)
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