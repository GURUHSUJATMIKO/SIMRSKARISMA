Imports DataAccess
Imports UI.WIN.MAIN.My.Resources
Imports System.Linq
Imports System.Data.SqlClient

Public Class frmBrowseLoadCPPT
    Private oKDPENDAFTARAN As String = String.Empty
    Private oCPPT As New Transaksi.clsCPPT

    Public Sub fn_LoadMe(ByVal FindKDITEM As String)
        sKDCPPT = String.Empty
        oKDPENDAFTARAN = FindKDITEM
    End Sub
    Private Sub Form_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        fn_LoadGrid()
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
        sKDCPPT = grv.GetFocusedRowCellDisplayText(colKDCPPT)
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
            grv.OptionsSelection.MultiSelect = True
            grv.SelectAll()
            grv.DeleteSelectedRows()
            grv.OptionsSelection.MultiSelect = False

            Dim ds = From x In oCPPT.GetDataByRegister(oKDPENDAFTARAN)
                     Select x.KDCPPT, SUKU = IIf(x.SUKU = "TAMBAH SBAR", "SBAR", "CPPT") & IIf(x.KDPENDAFTARAN.Contains("RJ"), " RJ", " RI"), x.DATE, x.PROFESI, x.SUBJEKTIF, x.OBJEKTIF, x.ASSEMENT, x.PLANNING, x.KDUSER

            grd.DataSource = ds.ToList

            For iLoop As Integer = 0 To grv.Columns.Count - 1
                If grv.Columns(iLoop).ColumnType.Name = "Decimal" Then
                    grv.Columns(iLoop).DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                    grv.Columns(iLoop).DisplayFormat.FormatString = "{0:n2}"
                    grv.Columns(iLoop).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
                ElseIf grv.Columns(iLoop).ColumnType.Name = "DateTime" Then
                    grv.Columns(iLoop).DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
                    grv.Columns(iLoop).DisplayFormat.FormatString = "{0:dd/MM/yyyy}"
                End If
            Next

            grv.Columns("PROFESI").Group()
            grv.ExpandAllGroups()

        Catch oErr As Exception
            MsgBox("Load Data CPPT Rawat Inap" & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_SetFormat()
        'grv.Columns("Tanggal").DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        'grv.Columns("Tanggal").DisplayFormat.FormatString = "{0:dd/MM/yyyy HH:mm}"

        'grv.Columns("PRICE").DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        'grv.Columns("PRICE").DisplayFormat.FormatString = "{0:n2}"
        'grv.Columns("PRICE").AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        'grv.ExpandAllGroups()
    End Sub
End Class