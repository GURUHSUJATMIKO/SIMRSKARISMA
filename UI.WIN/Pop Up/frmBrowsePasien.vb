Imports DataAccess
Imports System.Data.SqlClient

Public Class frmBrowsePasien
    Private Sub Form_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        sCode = String.Empty
    End Sub
    Private Sub Form_Closing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        'sCode = grv.GetFocusedRowCellValue("NoRM")
    End Sub
    Private Sub fn_LoadGrid(ByVal Filter As Integer, ByVal islama As Boolean)
        If islama = False Then
            Try
                grv.Columns.Clear()
                grd.DataSource = Nothing

                Dim oConn As New SqlConnection
                Dim oComm As New SqlCommand
                Dim da As SqlDataAdapter
                Dim ds As New DataSet
                Dim SQL_2 As String

                Dim sConn As String = Decrypt(My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Software\SIMRS\SW\", "Database", "").ToString())

                oConn = New SqlConnection(sConn)
                If oConn.State = ConnectionState.Closed Then
                    oConn.Open()
                End If

                SQL_2 = "SELECT "
                SQL_2 &= "NoRM = A.KDCUSTOMER "
                SQL_2 &= ",Pasien = A.NAME_DISPLAY "
                SQL_2 &= ",TanggalLahir = A.TANGGALLAHIR "
                SQL_2 &= ",KTP = A.KTP "
                SQL_2 &= ",KartuBPJS = A.KARTUBPJS "
                SQL_2 &= ",Alamat = A.ALAMATPASIEN_SIMRS "
                SQL_2 &= "FROM M_CUSTOMER A "
                SQL_2 &= "WHERE "

                If Filter = 0 Then
                    SQL_2 &= "A.KDCUSTOMER = '" & txtFILTER.Text & "' "
                ElseIf Filter = 1 Then
                    SQL_2 &= "A.NAME_DISPLAY LIKE '%" & txtFILTER.Text & "%' "
                ElseIf Filter = 2 Then
                    SQL_2 &= "A.KTP = '" & txtFILTER.Text & "' "
                ElseIf Filter = 3 Then
                    SQL_2 &= "A.KARTUBPJS = '" & txtFILTER.Text & "' "
                ElseIf Filter = 4 Then
                    SQL_2 &= "A.ALAMATPASIEN_SIMRS LIKE '%" & txtFILTER.Text & "%' "
                End If

                oComm.Connection = oConn
                oComm.CommandText = SQL_2
                oComm.CommandTimeout = 120
                oComm.CommandType = CommandType.Text

                da = New SqlDataAdapter(oComm)
                da.Fill(ds, "ALL")

                grd.MainView = grv
                grd.DataSource = ds.Tables("ALL")
                grd.ForceInitialize()

                fn_SetFormat()

                If oConn.State = ConnectionState.Open Then
                    oConn.Close()
                End If
            Catch oErr As Exception
                MsgBox("Priview Data" & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            End Try
        Else
            Try
                grv.Columns.Clear()
                grd.DataSource = Nothing

                Dim oConn As New SqlConnection
                Dim oComm As New SqlCommand
                Dim da As SqlDataAdapter
                Dim ds As New DataSet
                Dim SQL_2 As String

                Dim sConn As String = sConnOld

                oConn = New SqlConnection(sConn)

                If oConn.State = ConnectionState.Closed Then
                    oConn.Open()
                End If

                SQL_2 = "SELECT "
                SQL_2 &= "NoRM = A.KDCUSTOMER "
                SQL_2 &= ",Pasien = (SELECT CASE A.FRONT_TITLE WHEN '' THEN '' ELSE A.FRONT_TITLE + ' '  END) + A.NAME_DISPLAY + ' ' + (SELECT CASE A.BACK_TITLE WHEN '' THEN '' ELSE A.BACK_TITLE + ' '  END) "
                SQL_2 &= ",TanggalLahir = A.TANGGALLAHIR "
                SQL_2 &= ",KTP = A.NIK "
                SQL_2 &= "FROM M_CUSTOMER A "
                SQL_2 &= "WHERE "

                If Filter = 0 Then
                    SQL_2 &= "A.KDCUSTOMER = '" & txtFILTER.Text & "' "
                ElseIf Filter = 1 Then
                    SQL_2 &= "A.NAME_DISPLAY LIKE '%" & txtFILTER.Text & "%' "
                ElseIf Filter = 2 Then
                    SQL_2 &= "A.NIK = '" & txtFILTER.Text & "' "
                ElseIf Filter = 3 Then
                    SQL_2 &= "A.KARTUBPJS = '" & txtFILTER.Text & "' "
                ElseIf Filter = 4 Then
                    SQL_2 &= "A.ALAMATPASIEN_SIMRS LIKE '%" & txtFILTER.Text & "%' "
                End If

                oComm.Connection = oConn
                oComm.CommandText = SQL_2
                oComm.CommandTimeout = 120
                oComm.CommandType = CommandType.Text

                da = New SqlDataAdapter(oComm)
                da.Fill(ds, "ALL")

                grd.MainView = grv
                grd.DataSource = ds.Tables("ALL")
                grd.ForceInitialize()

                fn_SetFormat()

                If oConn.State = ConnectionState.Open Then
                    oConn.Close()
                End If
            Catch oErr As Exception
                MsgBox("Priview Data" & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            End Try
        End If

    End Sub
    Private Sub fn_SetFormat()
        For iLoop As Integer = 0 To grv.Columns.Count - 1
            If grv.Columns(iLoop).ColumnType.Name = "Decimal" Then
                grv.Columns(iLoop).DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                grv.Columns(iLoop).DisplayFormat.FormatString = "{0:n2}"
                grv.Columns(iLoop).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
            ElseIf grv.Columns(iLoop).ColumnType.Name = "DateTime" Then
                grv.Columns(iLoop).DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
                grv.Columns(iLoop).DisplayFormat.FormatString = "{0:dd/MM/yyyy HH:mm:ss}"
            End If
        Next

        grv.BestFitColumns()
    End Sub
    Private Sub btnPILIH_Click(sender As Object, e As EventArgs) Handles btnPILIH.Click
        sCode = grv.GetFocusedRowCellValue("NoRM")
        Me.Close()
    End Sub
    Private Sub btnBATAL_Click(sender As Object, e As EventArgs) Handles btnBATAL.Click
        Me.Close()
    End Sub
    Private Sub txtFILTER_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtFILTER.KeyPress
        If Asc(e.KeyChar) = 13 Then
            fn_LoadGrid(cboFilter.SelectedIndex, chkISLAMA.Checked)
        End If
    End Sub
End Class