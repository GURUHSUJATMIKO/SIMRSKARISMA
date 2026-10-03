Imports DataAccess
Imports UI.WIN.MAIN.My.Resources
Imports System.Linq
Imports DevExpress.XtraPrinting
Imports System.Data
Imports System.Data.SqlClient
Imports DevExpress.XtraSplashScreen
Imports System.Threading

Public Class frmReportMonitoringItem
    Implements ILanguage

#Region "Function"
    Private Sub Me_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.Text = Item.TITLE_REPORT

        lTYPE.Text = Report.FILTER_TYPE
        cboTYPE.Properties.Items.Clear()
        cboTYPE.Properties.Items.Add(Report.FILTER_ITEM_TYPE_01)
        cboTYPE.Properties.Items.Add("Kartu Stok")
        cboTYPE.SelectedIndex = 0
    End Sub
    Private Sub Me_FormClosed(sender As System.Object, e As System.Windows.Forms.FormClosedEventArgs) Handles MyBase.FormClosed
        Try
            grv.SaveLayoutToRegistry("HKEY_CURRENT_USER\Software\SIMRS\SW\" & sUserID & "\" & Me.Text)
        Catch oErr As Exception

        End Try
    End Sub
    Public Sub fn_LoadLanguage() Implements ILanguage.fn_LoadLanguage
        Try
            Me.Text = Item.TITLE_REPORT

            lTYPE.Text = Report.FILTER_TYPE

            cboTYPE.Properties.Items.Clear()
            cboTYPE.Properties.Items.Add(Report.FILTER_ITEM_TYPE_01)
            cboTYPE.Properties.Items.Add("Kartu Stok")

            If cboTYPE.SelectedIndex = 0 Then
                fn_LoadDataCurrent()
            End If
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadSecurity()
        Try
            Dim oOtority As New Setting.clsOtority
            Dim oUser As New Setting.clsUser

            Dim ds = (From x In oOtority.GetDataDetail _
                     Join y In oUser.GetData _
                     On x.KDOTORITY Equals y.KDOTORITY _
                     Where x.MODUL = "ITEM_R" _
                     And y.KDUSER = sUserID _
                     Select x.ISADD, x.ISDELETE, x.ISUPDATE, x.ISPRINT, x.ISVIEW).FirstOrDefault

            Try
                picPrint.Enabled = ds.ISPRINT
                picRefresh.Enabled = ds.ISVIEW

                If ds.ISVIEW = True Then
                    fn_Preview()
                    fn_LoadLanguage()
                End If
            Catch oErr As Exception
                MsgBox(Statement.SecurityNotInstalled, MsgBoxStyle.Exclamation, Me.Text)

                picPrint.Enabled = False
                picRefresh.Enabled = False
            End Try
        Catch oErr As Exception
            MsgBox(Statement.SecurityNotInstalled, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_Print()
        Try
            printableComponentLink.Landscape = True
            printableComponentLink.PaperKind = Printing.PaperKind.A4

            Dim phf As PageHeaderFooter = _
        TryCast(printableComponentLink.PageHeaderFooter, PageHeaderFooter)
            phf.Header.Content.Clear()
            phf.Header.Font = New Font("Times New Roman", 14, FontStyle.Bold)
            phf.Header.LineAlignment = BrickAlignment.Center
            phf.Footer.Font = New Font("Times New Roman", 9.75)
            phf.Footer.LineAlignment = BrickAlignment.Far
            phf.Footer.Content.AddRange(New String() _
        {sWATERMARK, "", Report.REPORT_PAGE & " : [Page # of Pages #]"})

            phf.Header.Content.AddRange(New String() _
{cboTYPE.Text, Item.TITLE_REPORT, ""})

            printableComponentLink.Component = grd
            printableComponentLink.CreateDocument()
            printableComponentLink.ShowPreviewDialog(Me)
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_Preview()
        Try
            grv.Columns.Clear()
            grd.DataSource = Nothing
            grv.GroupSummary.Clear()
            grv.OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways
            grv1.OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways

            If cboTYPE.SelectedIndex = 0 Then
                fn_LoadDataCurrent()
            End If
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
#Region "Current"
    Private Sub fn_LoadDataCurrent()
        Try
            Dim oConn As New SqlConnection
            Dim oComm As New SqlCommand
            Dim da As SqlDataAdapter
            Dim ds As New DataSet
            Dim SQL As String

            Dim sConn As String = Decrypt(My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Software\SIMRS\SW\", "Database", "").ToString())

            oConn = New SqlConnection(sConn)
            If oConn.State = ConnectionState.Closed Then
                oConn.Open()
            End If

            SQL = "SELECT "
            SQL &= "WAREHOUSE = D.NAME_DISPLAY "
            SQL &= ",B.NMITEM2 "
            SQL &= ",AMOUNT = A.AMOUNT * K.RATE "
            SQL &= "FROM M_ITEM_WAREHOUSE A "
            SQL &= "INNER JOIN M_ITEM B "
            SQL &= "ON A.KDITEM = B.KDITEM "
            SQL &= "INNER JOIN M_UOM C "
            SQL &= "ON A.KDUOM = C.KDUOM "
            SQL &= "INNER JOIN M_WAREHOUSE D "
            SQL &= "ON A.KDWAREHOUSE = D.KDWAREHOUSE "
            SQL &= "INNER JOIN M_ITEM_UOM K "
            SQL &= "ON A.KDITEM = K.KDITEM AND A.KDUOM = K.KDUOM "
            SQL &= "INNER JOIN M_ITEM_L1 L "
            SQL &= "ON B.KDITEM_L1 = L.KDITEM_L1 "

            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "ITEM")


            grd.MainView = grv
            grd.DataSource = ds.Tables("ITEM")
            grd.ForceInitialize()

            fn_LoadFormatDataCurrent()
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadFormatDataCurrent_()
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
    End Sub
    Private Sub fn_LoadFormatDataCurrent()
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

        grv.Columns("WAREHOUSE").Group()
        grv.ExpandAllGroups()
    End Sub
#End Region
    Private Sub MasterColumnChooserToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles MasterColumnChooserToolStripMenuItem.Click
        grv.ShowCustomization()
    End Sub
#End Region
#Region "Command Button"
    Private Sub frmMember_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        Select Case e.KeyCode
            Case Keys.Escape
                Me.Close()
            Case Keys.P
                If e.Alt = True And picPrint.Enabled = True Then
                    picPrint_Click()
                End If
            Case Keys.R
                If e.Alt = True And picRefresh.Enabled = True Then
                    picRefresh_Click()
                End If
        End Select
    End Sub
    Private Sub picPrint_Click() Handles picPrint.Click
        Try
            fn_Print()
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub picRefresh_Click() Handles picRefresh.Click
        fn_LoadSecurity()

        Try
            grv.RestoreLayoutFromRegistry("HKEY_CURRENT_USER\Software\SIMRS\SW\" & sUserID & "\" & Me.Text)
        Catch ex As Exception

        End Try
    End Sub
    Private Sub UpdateToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles UpdateToolStripMenuItem.Click
        If cboTYPE.SelectedIndex = 2 Then
            If MessageBox.Show("Recalculate Stok?", Me.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Information) = DialogResult.Yes Then
                Try
                    Dim oItem As New Reference.clsItem

                    SplashScreenManager.ShowForm(Me, GetType(frmLoadingPanel), True, True, False)

                    'If list.Count > 0 Then
                    '    For Each xloop In list
                    '        oItem.UpdateStok(xloop.KDITEM, xloop.KDUOM, xloop.KDWAREHOUSE, xloop.AMOUNT)
                    '    Next
                    'End If

                    For iLoop As Integer = 0 To grv.RowCount - 1
                        If grv.IsRowSelected(iLoop) = True Then
                            'oItem.UpdateStok(grv.GetRowCellValue(iLoop, "KDITEM"), grv.GetRowCellValue(iLoop, "KDUOM"), grv.GetRowCellValue(iLoop, "KDWAREHOUSE"), grv.GetRowCellValue(iLoop, "STOK"))
                        End If
                    Next

                Catch ex As Exception

                Finally
                    Thread.Sleep(100)
                    SplashScreenManager.CloseForm(False)

                    MsgBox("Recalculate Stok Done!")
                End Try
            End If
        End If
    End Sub
#End Region
End Class