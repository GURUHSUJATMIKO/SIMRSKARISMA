Imports DataAccess
Imports UI.WIN.MAIN.My.Resources
Imports System.Linq
Imports System.Data
Imports System.Data.SqlClient

Public Class frmACCDokter
    Private Sub SimpleButton1_Click(sender As Object, e As EventArgs) Handles SimpleButton1.Click
        sRemarks = grdDPJPUtama.Text
        Me.Close()
    End Sub
    Private Sub fn_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        sRemarks = String.Empty

        'deTANGGALPENERIMA.DateTime = Now
        fn_LoadDokter()
    End Sub
    Private Sub fn_LoadDokter()
        Try
            Dim oDoctor As New Reference.clsDoctor

            Dim oConn As New SqlConnection
            Dim oComm As New SqlCommand
            Dim da As SqlDataAdapter
            Dim ds As New DataSet
            Dim SQL As String
            Dim sConn As String = sConnOld

            oConn = New SqlConnection(sConn)

            If oConn.State = ConnectionState.Closed Then
                oConn.Open()
            End If

            SQL = "SELECT "
            SQL &= "* "
            SQL &= "FROM  "
            SQL &= "M_DOCTOR "
            SQL &= "WHERE "
            SQL &= "ISACTIVE = '1' "

            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "M_DOCTOR")

            grdDPJPUtama.Properties.DataSource = ds.Tables("M_DOCTOR")
            grdDPJPUtama.Properties.ValueMember = "KDDOCTOR"
            grdDPJPUtama.Properties.DisplayMember = "NAME_DISPLAY"

            If oConn.State = ConnectionState.Open Then
                oConn.Close()
            End If

            Dim dsDepartment = oDoctor.GetDataByIDUser(sUserID)
            If dsDepartment IsNot Nothing Then
                grdDPJPUtama.Text = dsDepartment.KDDOCTOR
            End If
        Catch oErr As Exception
            MsgBox("Load Dokter Data : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub

    Private Sub SimpleButton2_Click(sender As Object, e As EventArgs) Handles SimpleButton2.Click
        sRemarks = ""
        Me.Close()
    End Sub
End Class