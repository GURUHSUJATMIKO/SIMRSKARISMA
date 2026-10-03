Imports DataAccess
Imports System.Linq
Imports System.Data.SqlClient

Public Class frmObatFarmasi
    Private Sub Form_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        sQTY_PILIH = 0
        sKDSIGNA_PILIH = String.Empty
        sKDCARAPAKAI_PILIH = String.Empty
        sREMARKS_PILIH = String.Empty
        fn_LoadSIGNA()
        fn_LoadCARAPAKAI()
    End Sub
    Private Sub Form_Closing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        Dispose()

        sQTY_PILIH = txtQTY.Text
        sKDSIGNA_PILIH = grdKDSIGNA.EditValue
        sKDCARAPAKAI_PILIH = grdKDCARAPAKAI.EditValue
        sREMARKS_PILIH = txtREMARKS_DOKTER.Text
    End Sub
    Private Sub btnBayar_Click(sender As Object, e As EventArgs) Handles btnHapus.Click
        If grdKDSIGNA.Text = String.Empty Then
            MsgBox("Signa Belum dipilih", MsgBoxStyle.Exclamation, Me.Text)
            Exit Sub
        End If
        If grdKDCARAPAKAI.Text = String.Empty Then
            MsgBox("Cara Pakai Belum dipilih", MsgBoxStyle.Exclamation, Me.Text)
            Exit Sub
        End If
        If CDec(txtQTY.Text) <= 0 Then
            MsgBox("QTY Masih 0", MsgBoxStyle.Exclamation, Me.Text)
            Exit Sub
        End If

        Me.Close()
    End Sub
    Private Sub btnBayarTidak_Click(sender As Object, e As EventArgs) Handles btnBatal.Click
        sKDITEM_PILIH = ""
        sQTY_PILIH = 0
        sKDSIGNA_PILIH = String.Empty
        sKDCARAPAKAI_PILIH = String.Empty
        sREMARKS_PILIH = String.Empty
        Me.Close()
    End Sub
    Private Sub fn_LoadSIGNA()
        Try
            Dim oConn As New SqlConnection
            Dim oComm As New SqlCommand
            Dim da As SqlDataAdapter
            Dim ds As New DataSet
            Dim SQL As String

            oConn = New SqlConnection(sConnOld)

            If oConn.State = ConnectionState.Closed Then
                oConn.Open()
            End If

            SQL = "SELECT * "
            SQL &= "FROM M_SIGNA "
            SQL &= "WHERE ISACTIVE = 1 "

            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "SIGNA")

            grdKDSIGNA.Properties.DataSource = ds.Tables("SIGNA")
            grdKDSIGNA.Properties.ValueMember = "KDSIGNA"
            grdKDSIGNA.Properties.DisplayMember = "DESCRIPTION"

            If oConn.State = ConnectionState.Open Then
                oConn.Close()
            End If

        Catch oErr As Exception
            MsgBox("Load Diagnosa Data : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadCARAPAKAI()
        Try
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
            SQL &= "M_CARAPAKAI A "
            SQL &= "WHERE ISACTIVE = 1 "

            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "CARAPAKAI")

            grdKDCARAPAKAI.Properties.DataSource = ds.Tables("CARAPAKAI")
            grdKDCARAPAKAI.Properties.ValueMember = "KDCP"
            grdKDCARAPAKAI.Properties.DisplayMember = "DESCRIPTION"

            If oConn.State = ConnectionState.Open Then
                oConn.Close()
            End If

        Catch oErr As Exception
            MsgBox("Load Diagnosa Data : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub frm_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        Select Case e.KeyCode
            Case Keys.F12
                sKDITEM_PILIH = ""
                Me.Close()
            Case Keys.F3
                If grdKDSIGNA.Text = String.Empty Then
                    MsgBox("Signa Belum dipilih", MsgBoxStyle.Exclamation, Me.Text)
                    Exit Sub
                End If
                If grdKDCARAPAKAI.Text = String.Empty Then
                    MsgBox("Cara Pakai Belum dipilih", MsgBoxStyle.Exclamation, Me.Text)
                    Exit Sub
                End If
                If CDec(txtQTY.Text) <= 0 Then
                    MsgBox("QTY Masih 0", MsgBoxStyle.Exclamation, Me.Text)
                    Exit Sub
                End If

                Me.Close()
        End Select
    End Sub
End Class