Imports DataAccess
Imports UI.WIN.MAIN.My.Resources
Imports System.Linq
Imports System.Data.SqlClient

Public Class frmBrowsePurchaseInvoice
    Private oKDITEM As String = String.Empty

    Public Sub fn_LoadMe(ByVal FindKDITEM As String)
        oKDITEM = FindKDITEM
    End Sub
    Private Sub Form_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        fn_LoadGrid()
        fn_LoadLanguage()
    End Sub
    Public Sub fn_LoadLanguage()
        Try
            Me.Text = "Browse Pembelian"

        Catch oErr As Exception
            MsgBox("Load Data : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub Form_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Return And e.Shift = 0 Then
            cmdSelect_Click(sender, e)
        ElseIf e.KeyCode = Keys.Escape Then
            sPricePembelian = 0
            Me.Close()
        End If
    End Sub
    Private Sub cmdSelect_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSelect.Click
        sPricePembelian = grv.GetFocusedRowCellDisplayText(colHarga)
        Me.Close()
    End Sub
    Private Sub grv_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles grv.DoubleClick
        cmdSelect_Click(sender, e)
    End Sub

    Private Sub cmdClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdClose.Click
        Me.Close()
    End Sub
    Private Sub fn_LoadGrid()
        Try
            Dim listPricePembelian As New List(Of DataAccess.R_PRICE_PEMBELIAN)

            Dim oConn_1 As New SqlConnection
            Dim oComm_1 As New SqlCommand
            Dim da_1 As SqlDataAdapter
            Dim ds_1 As New DataSet
            Dim SQL_1 As String

            oConn_1 = New SqlConnection(sConnOld)

            If oConn_1.State = ConnectionState.Closed Then
                oConn_1.Open()
            End If

            SQL_1 = "SELECT "
            SQL_1 &= "NoPembelian = A.KDPI "
            SQL_1 &= ",Tanggal = C.DATE "
            SQL_1 &= ",NoFaktur = C.KDFAKTUR "
            SQL_1 &= ",KDVENDOR = E.NAME_DISPLAY "
            SQL_1 &= ",PRICE = A.PRICE "
            SQL_1 &= "FROM P_PI_D A "
            SQL_1 &= "INNER JOIN M_ITEM B "
            SQL_1 &= "ON A.KDITEM = B.KDITEM "
            SQL_1 &= "INNER JOIN P_PI_H C "
            SQL_1 &= "ON C.KDPI = A.KDPI "
            SQL_1 &= "INNER JOIN M_UOM D "
            SQL_1 &= "ON A.KDUOM = D.KDUOM "
            SQL_1 &= "INNER JOIN M_VENDOR E "
            SQL_1 &= "ON C.KDVENDOR = E.KDVENDOR "

            SQL_1 &= "WHERE A.KDITEM = '" & oKDITEM & "' "

            oComm_1.Connection = oConn_1
            oComm_1.CommandText = SQL_1
            oComm_1.CommandTimeout = 120
            oComm_1.CommandType = CommandType.Text

            da_1 = New SqlDataAdapter(oComm_1)
            da_1.Fill(ds_1, "P_PI_D")

            For iLoop As Integer = 0 To ds_1.Tables("P_PI_D").Rows.Count - 1
                Dim dsRekap As New DataAccess.R_PRICE_PEMBELIAN
                With ds_1.Tables("P_PI_D")
                    dsRekap.NoPembelian = .Rows(iLoop)("NoPembelian")
                    dsRekap.Tanggal = .Rows(iLoop)("Tanggal")
                    dsRekap.NoFaktur = .Rows(iLoop)("NoFaktur")
                    dsRekap.KDVENDOR = .Rows(iLoop)("KDVENDOR")
                    dsRekap.PRICE = .Rows(iLoop)("PRICE")

                    listPricePembelian.Add(dsRekap)
                End With
            Next


            Dim oConn_2 As New SqlConnection
            Dim oComm_2 As New SqlCommand
            Dim da_2 As SqlDataAdapter
            Dim ds_2 As New DataSet
            Dim SQL_2 As String
            Dim sConn_2 As String = Decrypt(My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Software\SIMRS\SW\", "Database", "").ToString())

            oConn_2 = New SqlConnection(sConn_2)

            If oConn_2.State = ConnectionState.Closed Then
                oConn_2.Open()
            End If

            SQL_2 = "SELECT "
            SQL_2 &= "NoPembelian = A.KDPI "
            SQL_2 &= ",Tanggal = C.DATE "
            SQL_2 &= ",NoFaktur = C.KDFAKTUR "
            SQL_2 &= ",KDVENDOR = E.NAME_DISPLAY "
            SQL_2 &= ",PRICE = A.PRICE "
            SQL_2 &= "FROM P_PI_D A "
            SQL_2 &= "INNER JOIN M_ITEM B "
            SQL_2 &= "ON A.KDITEM = B.KDITEM "
            SQL_2 &= "INNER JOIN P_PI_H C "
            SQL_2 &= "ON C.KDPI = A.KDPI "
            SQL_2 &= "INNER JOIN M_UOM D "
            SQL_2 &= "ON A.KDUOM = D.KDUOM "
            SQL_2 &= "INNER JOIN M_VENDOR E "
            SQL_2 &= "ON C.KDVENDOR = E.KDVENDOR "

            SQL_2 &= "WHERE A.KDITEM = '" & oKDITEM & "' "

            oComm_2.Connection = oConn_2
            oComm_2.CommandText = SQL_2
            oComm_2.CommandTimeout = 120
            oComm_2.CommandType = CommandType.Text

            da_2 = New SqlDataAdapter(oComm_2)
            da_2.Fill(ds_2, "P_PI_D")

            For iLoop As Integer = 0 To ds_2.Tables("P_PI_D").Rows.Count - 1
                Dim dsRekap As New DataAccess.R_PRICE_PEMBELIAN
                With ds_2.Tables("P_PI_D")
                    dsRekap.NoPembelian = .Rows(iLoop)("NoPembelian")
                    dsRekap.Tanggal = .Rows(iLoop)("Tanggal")
                    dsRekap.NoFaktur = .Rows(iLoop)("NoFaktur")
                    dsRekap.KDVENDOR = .Rows(iLoop)("KDVENDOR")
                    dsRekap.PRICE = .Rows(iLoop)("PRICE")

                    listPricePembelian.Add(dsRekap)
                End With
            Next

            grd.DataSource = listPricePembelian
            grd.ForceInitialize()

            fn_SetFormat()

            If oConn_1.State = ConnectionState.Open Then
                oConn_1.Close()
            End If

            If oConn_2.State = ConnectionState.Open Then
                oConn_2.Close()
            End If
        Catch oErr As Exception
            MsgBox("Preview Data : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_SetFormat()
        grv.Columns("Tanggal").DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        grv.Columns("Tanggal").DisplayFormat.FormatString = "{0:dd/MM/yyyy HH:mm}"

        grv.Columns("PRICE").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        grv.Columns("PRICE").DisplayFormat.FormatString = "{0:n2}"
        grv.Columns("PRICE").AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        grv.ExpandAllGroups()

    End Sub
End Class