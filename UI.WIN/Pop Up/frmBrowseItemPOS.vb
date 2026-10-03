Imports DataAccess
Imports UI.WIN.MAIN.My.Resources
Imports System.Linq
Imports System.Data.SqlClient

Public Class frmBrowseItemPOS
    Private Sub Form_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        fn_LoadGrid()
    End Sub
    Private Sub Form_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Return And e.Shift = 0 Then
            cmdSelect_Click(sender, e)
        ElseIf e.KeyCode = Keys.Escape Then
            sKDITEM_PILIH = String.Empty
            sKDUOM_PILIH = String.Empty
            Me.Close()
        End If
    End Sub
    Private Sub cmdSelect_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSelect.Click
        Try
            sKDITEM_PILIH = grv.GetFocusedRowCellDisplayText(colKDITEM)
            sKDUOM_PILIH = grv.GetFocusedRowCellDisplayText(colKDUOM)
        Catch ex As Exception
            sKDITEM_PILIH = String.Empty
            sKDUOM_PILIH = String.Empty
        End Try
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
            Dim oConn As New SqlConnection
            Dim oComm As New SqlCommand
            Dim da As SqlDataAdapter
            Dim ds As New DataSet
            Dim SQL As String
            'Dim sConn As String = Decrypt(My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Software\SIMRS\SW\", "Database", "").ToString())

            oConn = New SqlConnection(sConnOld)

            If oConn.State = ConnectionState.Closed Then
                oConn.Open()
            End If

            SQL = "SELECT "
            SQL &= "A.KDITEM "
            'SQL &= ",NMITEM2 = (SELECT CASE B.ISFRONAS WHEN 1 THEN REPLACE(B.NMITEM2, '(EC) ', '') ELSE B.NMITEM2 END) "
            SQL &= ",B.NMITEM2 "
            SQL &= ",A.KDUOM "
            SQL &= ",ISKRONIS = B.ISFRONAS "
            SQL &= "FROM M_ITEM_UOM A "
            SQL &= "INNER JOIN M_ITEM B "
            SQL &= "ON A.KDITEM = B.KDITEM "
            'SQL &= "INNER JOIN M_ITEM_L1 C "
            'SQL &= "ON B.KDITEM_L1 = C.KDITEM_L1 "
            SQL &= "WHERE "
            SQL &= "A.RATE = 1 "
            SQL &= "AND B.ISACTIVE = 1 "
            'SQL &= "AND C.MEMO = 'OBAT' "

            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "ITEM")

            grd.DataSource = ds.Tables("ITEM")
            grd.RefreshDataSource()

            If oConn.State = ConnectionState.Open Then
                oConn.Close()
            End If

        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub

End Class