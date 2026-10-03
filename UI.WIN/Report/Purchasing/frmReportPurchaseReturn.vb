Imports DataAccess
Imports UI.WIN.MAIN.My.Resources
Imports System.Linq

Imports DevExpress.XtraPrinting

Imports System.Data
Imports System.Data.SqlClient


Public Class frmReportPurchaseReturn
    Implements ILanguage

#Region "Function"
    Private Sub Me_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.Text = PurchaseReturn.TITLE_REPORT

        lTYPE.Text = Report.FILTER_TYPE
        lDATEFROM.Text = Report.FILTER_DATEFROM
        lDATETO.Text = Report.FILTER_DATETO

        cboTYPE.Properties.Items.Clear()
        cboTYPE.Properties.Items.Add(Report.FILTER_TYPE_01)
        cboTYPE.Properties.Items.Add(Report.FILTER_TYPE_02)
        cboTYPE.Properties.Items.Add(Report.FILTER_TYPE_03)
        cboTYPE.Properties.Items.Add(Report.FILTER_TYPE_04)
        cboTYPE.SelectedIndex = 0

        deDATEFrom.DateTime = Now.AddDays((-Now.Day) + 1)
        deDATETo.DateTime = Now
    End Sub
    Private Sub Me_FormClosed(sender As System.Object, e As System.Windows.Forms.FormClosedEventArgs) Handles MyBase.FormClosed
        Try
            grv.SaveLayoutToRegistry("HKEY_CURRENT_USER\Software\SIMRS\SW\" & sUserID & "\" & Me.Text & "\" & cboTYPE.Text)
        Catch oErr As Exception

        End Try
    End Sub
    Public Sub fn_LoadLanguage() Implements ILanguage.fn_LoadLanguage
        Try
            Me.Text = PurchaseReturn.TITLE_REPORT

            lTYPE.Text = Report.FILTER_TYPE
            lDATEFROM.Text = Report.FILTER_DATEFROM
            lDATETO.Text = Report.FILTER_DATETO

            cboTYPE.Properties.Items.Clear()
            cboTYPE.Properties.Items.Add(Report.FILTER_TYPE_01)
            cboTYPE.Properties.Items.Add(Report.FILTER_TYPE_02)
            cboTYPE.Properties.Items.Add(Report.FILTER_TYPE_03)
            cboTYPE.Properties.Items.Add(Report.FILTER_TYPE_04)

            If cboTYPE.SelectedIndex = 3 Then
                fn_LoadLanguageAll()
            Else
                fn_LoadLanguageMaster()
                fn_LoadLanguageDetail()
            End If
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadSecurity()
        Try
            Dim oOtority As New Setting.clsOtority
            Dim oUser As New Setting.clsUser

            Dim ds = (From x In oOtority.GetDataDetail
                      Join y In oUser.GetData
                     On x.KDOTORITY Equals y.KDOTORITY
                      Where x.MODUL = "PR_R" _
                     And y.KDUSER = sUserID
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

            Dim phf As PageHeaderFooter =
        TryCast(printableComponentLink.PageHeaderFooter, PageHeaderFooter)
            phf.Header.Content.Clear()
            phf.Header.Font = New Font("Times New Roman", 14, FontStyle.Bold)
            phf.Header.LineAlignment = BrickAlignment.Center
            phf.Footer.Font = New Font("Times New Roman", 9.75)
            phf.Footer.LineAlignment = BrickAlignment.Far
            phf.Footer.Content.AddRange(New String() _
        {sWATERMARK, "", Report.REPORT_PAGE & " : [Page # of Pages #]"})

            phf.Header.Content.AddRange(New String() _
{"", PurchaseReturn.TITLE_REPORT & vbCrLf & deDATEFrom.DateTime.ToString("dd/MM/yyyy") & " - " & deDATETo.DateTime.ToString("dd/MM/yyyy"), ""})

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
            grv.OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways
            grv1.OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways

            If cboTYPE.SelectedIndex = 3 Then
                fn_LoadDataAll()
            Else
                fn_LoadData()
            End If

        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub

#Region "Master Detail"
    Private Sub fn_LoadData()
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
            SQL &= "A.KDPR "
            SQL &= ",A.DATE "
            SQL &= ",KDVENDOR = B.NAME_DISPLAY "
            SQL &= ",KDWAREHOUSE = C.NAME_DISPLAY "
            SQL &= ",A.MEMO "

            SQL &= ",A.KDUSER "
            SQL &= ",A.SUBTOTAL "
            SQL &= ",A.DISCOUNT "
            SQL &= ",A.TAX "
            SQL &= ",A.GRANDTOTAL "
            SQL &= "FROM P_PR_H A "
            SQL &= "INNER JOIN M_VENDOR B "
            SQL &= "ON A.KDVENDOR = B.KDVENDOR "
            SQL &= "INNER JOIN M_WAREHOUSE C "
            SQL &= "ON A.KDWAREHOUSE = C.KDWAREHOUSE "
            SQL &= "WHERE CONVERT(VARCHAR(8), A.DATE, 112) >= '" & deDATEFrom.DateTime.ToString("yyyyMMdd") & "' AND CONVERT(VARCHAR(8), A.DATE, 112) <= '" & deDATETo.DateTime.ToString("yyyyMMdd") & "' "
            SQL &= "ORDER BY DATE, KDPR DESC"

            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "P_PR_H")

            SQL = "SELECT "
            SQL &= "B.KDPR "
            SQL &= ",C.NMITEM1 "
            SQL &= ",C.NMITEM2 "
            SQL &= ",C.NMITEM3 "
            SQL &= ",B.QTY "
            SQL &= ",KDUOM = D.MEMO "
            SQL &= ",B.PRICE "
            SQL &= ",B.SUBTOTAL "
            SQL &= ",B.DISCOUNT "
            SQL &= ",B.GRANDTOTAL "
            SQL &= ",B.REMARKS "
            SQL &= "FROM P_PR_H A "
            SQL &= "INNER JOIN P_PR_D B "
            SQL &= "ON A.KDPR = B.KDPR "
            SQL &= "INNER JOIN M_ITEM C "
            SQL &= "ON B.KDITEM = C.KDITEM "
            SQL &= "INNER JOIN M_UOM D "
            SQL &= "ON B.KDUOM = D.KDUOM "
            SQL &= "WHERE CONVERT(VARCHAR(8), A.DATE, 112) >= '" & deDATEFrom.DateTime.ToString("yyyyMMdd") & "' AND CONVERT(VARCHAR(8), A.DATE, 112) <= '" & deDATETo.DateTime.ToString("yyyyMMdd") & "' "

            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "P_PR_D")

            If cboTYPE.SelectedIndex = 0 Then
                Dim keyColumn As DataColumn = ds.Tables("P_PR_H").Columns("KDPR")
                Dim foreignKeyColumn As DataColumn = ds.Tables("P_PR_D").Columns("KDPR")
                ds.Relations.Add("FK_RELATION", keyColumn, foreignKeyColumn)

                grd.MainView = grv
                grd.DataSource = ds.Tables("P_PR_H")
                grd.ForceInitialize()

                grd.LevelTree.Nodes.Add("FK_RELATION", grv1)
                grv1.ViewCaption = "Details"

                grv1.PopulateColumns(ds.Tables("P_PR_D"))
                grv1.Columns("KDPR").VisibleIndex = -1

                fn_LoadFormatData()
                fn_LoadFormatDataDetail()
            ElseIf cboTYPE.SelectedIndex = 1 Then
                grd.MainView = grv
                grd.DataSource = ds.Tables("P_PR_H")
                grd.ForceInitialize()

                fn_LoadFormatData()
            ElseIf cboTYPE.SelectedIndex = 2 Then
                grd.MainView = grv1
                grd.DataSource = ds.Tables("P_PR_D")
                grd.ForceInitialize()

                fn_LoadFormatDataDetail()
            End If
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadFormatData()
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
    Private Sub fn_LoadFormatDataDetail()
        For iLoop As Integer = 0 To grv1.Columns.Count - 1
            If grv1.Columns(iLoop).FieldName = "QTY" Or grv1.Columns(iLoop).FieldName = "PRICE" Or grv1.Columns(iLoop).FieldName = "SUBTOTAL" Or grv1.Columns(iLoop).FieldName = "DISCOUNT" Or grv1.Columns(iLoop).FieldName = "GRANDTOTAL" Then
                grv1.Columns(iLoop).DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                grv1.Columns(iLoop).DisplayFormat.FormatString = "{0:n2}"
                grv1.Columns(iLoop).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
            End If
        Next
    End Sub
    Public Sub fn_LoadLanguageMaster()
        Try
            grv.Columns("KDPR").Caption = PurchaseReturn.KDPR
            grv.Columns("DATE").Caption = PurchaseReturn.TANGGAL
            grv.Columns("KDVENDOR").Caption = PurchaseReturn.KDVENDOR
            grv.Columns("KDWAREHOUSE").Caption = PurchaseReturn.KDWAREHOUSE
            grv.Columns("MEMO").Caption = PurchaseReturn.MEMO
            grv.Columns("SUBTOTAL").Caption = PurchaseReturn.SUBTOTAL
            grv.Columns("DISCOUNT").Caption = PurchaseReturn.DISCOUNT
            grv.Columns("TAX").Caption = PurchaseReturn.TAX
            grv.Columns("GRANDTOTAL").Caption = PurchaseReturn.GRANDTOTAL
            grv.Columns("KDUSER").Caption = Caption.User

            MasterColumnChooserToolStripMenuItem.Text = Caption.ColumnChooserMaster
            DetailColumnChooserToolStripMenuItem.Text = Caption.ColumnChooserDetail
        Catch oErr As Exception

        End Try
    End Sub
    Public Sub fn_LoadLanguageDetail()
        Try
            grv1.Columns("QTY").Caption = PurchaseReturn.DETAIL_QTY
            grv1.Columns("PRICE").Caption = PurchaseReturn.DETAIL_PRICE
            grv1.Columns("SUBTOTAL").Caption = PurchaseReturn.DETAIL_SUBTOTAL
            grv1.Columns("DISCOUNT").Caption = PurchaseReturn.DETAIL_DISCOUNT
            grv1.Columns("GRANDTOTAL").Caption = PurchaseReturn.DETAIL_GRANDTOTAL
            grv1.Columns("REMARKS").Caption = PurchaseReturn.DETAIL_REMARKS

            grv1.Columns("NMITEM1").Caption = Item.NMITEM1
            grv1.Columns("NMITEM2").Caption = Item.NMITEM2
            grv1.Columns("NMITEM3").Caption = Item.NMITEM3
            grv1.Columns("KDUOM").Caption = PurchaseReturn.DETAIL_KDUOM
        Catch oErr As Exception

        End Try
    End Sub
#End Region
#Region "All"
    Private Sub fn_LoadDataAll()
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
            SQL &= "A.* "
            SQL &= ",B.* "
            SQL &= "FROM "
            SQL &= "( "
            SQL &= "SELECT "
            SQL &= "A.KDPR "
            SQL &= ",A.DATE "
            SQL &= ",KDVENDOR = B.NAME_DISPLAY "
            SQL &= ",KDWAREHOUSE = C.NAME_DISPLAY "
            SQL &= ",A.MEMO "

            SQL &= ",A.KDUSER "
            SQL &= ",A.SUBTOTAL "
            SQL &= ",A.DISCOUNT "
            SQL &= ",A.TAX "
            SQL &= ",A.GRANDTOTAL "
            SQL &= "FROM P_PR_H A "
            SQL &= "INNER JOIN M_VENDOR B "
            SQL &= "ON A.KDVENDOR = B.KDVENDOR "
            SQL &= "INNER JOIN M_WAREHOUSE C "
            SQL &= "ON A.KDWAREHOUSE = C.KDWAREHOUSE "
            SQL &= "WHERE CONVERT(VARCHAR(8), A.DATE, 112) >= '" & deDATEFrom.DateTime.ToString("yyyyMMdd") & "' AND CONVERT(VARCHAR(8), A.DATE, 112) <= '" & deDATETo.DateTime.ToString("yyyyMMdd") & "' "
            SQL &= ") AS A "

            SQL &= "INNER JOIN "
            SQL &= "( "
            SQL &= "SELECT "
            SQL &= "B.KDPR "
            SQL &= ",C.NMITEM1 "
            SQL &= ",C.NMITEM2 "
            SQL &= ",C.NMITEM3 "
            SQL &= ",B.QTY "
            SQL &= ",KDUOM = D.MEMO "
            SQL &= ",B.PRICE "
            SQL &= ",B.SUBTOTAL "
            SQL &= ",B.DISCOUNT "
            SQL &= ",B.GRANDTOTAL "
            SQL &= ",B.REMARKS "
            SQL &= "FROM P_PR_H A "
            SQL &= "INNER JOIN P_PR_D B "
            SQL &= "ON A.KDPR = B.KDPR "
            SQL &= "INNER JOIN M_ITEM C "
            SQL &= "ON B.KDITEM = C.KDITEM "
            SQL &= "INNER JOIN M_UOM D "
            SQL &= "ON B.KDUOM = D.KDUOM "
            SQL &= "WHERE CONVERT(VARCHAR(8), A.DATE, 112) >= '" & deDATEFrom.DateTime.ToString("yyyyMMdd") & "' AND CONVERT(VARCHAR(8), A.DATE, 112) <= '" & deDATETo.DateTime.ToString("yyyyMMdd") & "' "
            SQL &= ") AS B "

            SQL &= "ON A.KDPR = B.KDPR "
            SQL &= "ORDER BY A.KDPR DESC "

            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "ALL")

            grd.MainView = grv
            grd.DataSource = ds.Tables("ALL")
            grd.ForceInitialize()

            fn_LoadFormatDataAll()
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadFormatDataAll()
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

        grv.Columns("KDPR1").Visible = False
        grv.Columns("KDPR1").OptionsColumn.ShowInCustomizationForm = False
    End Sub
    Public Sub fn_LoadLanguageAll()
        Try
            grv.Columns("KDPR").Caption = PurchaseReturn.KDPR
            grv.Columns("DATE").Caption = PurchaseReturn.TANGGAL
            grv.Columns("KDVENDOR").Caption = PurchaseReturn.KDVENDOR
            grv.Columns("KDWAREHOUSE").Caption = PurchaseReturn.KDWAREHOUSE
            grv.Columns("MEMO").Caption = PurchaseReturn.MEMO
            grv.Columns("SUBTOTAL").Caption = PurchaseReturn.SUBTOTAL
            grv.Columns("DISCOUNT").Caption = PurchaseReturn.DISCOUNT
            grv.Columns("TAX").Caption = PurchaseReturn.TAX
            grv.Columns("GRANDTOTAL").Caption = PurchaseReturn.GRANDTOTAL
            grv.Columns("KDUSER").Caption = Caption.User

            grv.Columns("QTY").Caption = PurchaseReturn.DETAIL_QTY
            grv.Columns("PRICE").Caption = PurchaseReturn.DETAIL_PRICE
            grv.Columns("SUBTOTAL1").Caption = PurchaseReturn.DETAIL_SUBTOTAL
            grv.Columns("DISCOUNT1").Caption = PurchaseReturn.DETAIL_DISCOUNT
            grv.Columns("GRANDTOTAL1").Caption = PurchaseReturn.DETAIL_GRANDTOTAL
            grv.Columns("REMARKS").Caption = PurchaseReturn.DETAIL_REMARKS

            grv.Columns("NMITEM1").Caption = Item.NMITEM1
            grv.Columns("NMITEM2").Caption = Item.NMITEM2
            grv.Columns("NMITEM3").Caption = Item.NMITEM3
            grv.Columns("KDUOM").Caption = PurchaseReturn.DETAIL_KDUOM

            MasterColumnChooserToolStripMenuItem.Text = Caption.ColumnChooserMaster
            DetailColumnChooserToolStripMenuItem.Text = Caption.ColumnChooserDetail
        Catch oErr As Exception

        End Try
    End Sub
#End Region

    Private Sub MasterColumnChooserToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles MasterColumnChooserToolStripMenuItem.Click
        grv.ShowCustomization()
    End Sub
    Private Sub DetailColumnChooserToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles DetailColumnChooserToolStripMenuItem.Click
        grv1.ShowCustomization()
    End Sub
    Private Sub cboTYPE_SelectedIndexChanged() Handles cboTYPE.SelectedIndexChanged
        fn_LoadSecurity()

        Try
            grv.RestoreLayoutFromRegistry("HKEY_CURRENT_USER\Software\SIMRS\SW\" & sUserID & "\" & Me.Text & "\" & cboTYPE.Text)
        Catch ex As Exception

        End Try
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
            grv.RestoreLayoutFromRegistry("HKEY_CURRENT_USER\Software\SIMRS\SW\" & sUserID & "\" & Me.Text & "\" & cboTYPE.Text)
        Catch ex As Exception

        End Try
    End Sub
#End Region
End Class