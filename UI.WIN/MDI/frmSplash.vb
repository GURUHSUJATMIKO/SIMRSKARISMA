Public Class frmSplash
    Sub New
        InitializeComponent()
    End Sub

    Public Overrides Sub ProcessCommand(ByVal cmd As System.Enum, ByVal arg As Object)
        MyBase.ProcessCommand(cmd, arg)
    End Sub

    Public Enum SplashScreenCommand
        SomeCommandId
    End Enum

    Private Sub frmSplash_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        Me.TopMost = True

        Dim GridControl As New DevExpress.XtraGrid.GridControl
        Dim GridView As New DevExpress.XtraGrid.Views.Grid.GridView

        Dim SplitterControl As New DevExpress.XtraEditors.SplitterControl
        Dim GroupControl As New DevExpress.XtraEditors.GroupControl

        Dim LayoutControl As New DevExpress.XtraLayout.LayoutControl
        Dim LayoutControlGroup As New DevExpress.XtraLayout.LayoutControlGroup
        Dim LayoutControlItem = New DevExpress.XtraLayout.LayoutControlItem

        Dim LabelControl As New DevExpress.XtraEditors.LabelControl
        Dim PictureEdit As New DevExpress.XtraEditors.PictureEdit
        Dim MemoEdit As New DevExpress.XtraEditors.MemoEdit
        Dim SimpleButton As New DevExpress.XtraEditors.SimpleButton
        Dim TextEdit As New DevExpress.XtraEditors.TextEdit
        Dim DateEdit As New DevExpress.XtraEditors.DateEdit

        Dim BarButtonItem As New DevExpress.XtraBars.BarButtonItem

        Dim XtraReport As New DevExpress.XtraReports.UI.XtraReport
        Dim DetailBand As New DevExpress.XtraReports.UI.DetailBand
        Dim XRLabel As New DevExpress.XtraReports.UI.XRLabel
        Dim TopMarginBand As New DevExpress.XtraReports.UI.TopMarginBand
        Dim BottomMarginBand As New DevExpress.XtraReports.UI.BottomMarginBand
        Dim DetailReportBand As New DevExpress.XtraReports.UI.DetailReportBand
        Dim XRControlStyle As New DevExpress.XtraReports.UI.XRControlStyle
        Dim PageHeaderBand As New DevExpress.XtraReports.UI.PageHeaderBand
        Dim PageFooterBand As New DevExpress.XtraReports.UI.PageFooterBand
        Dim CalculatedField As New DevExpress.XtraReports.UI.CalculatedField

        Dim BindingSource As New System.Windows.Forms.BindingSource

        Dim ContextMenuStrip As New System.Windows.Forms.ContextMenuStrip
        Dim ToolStripMenuItem As New System.Windows.Forms.ToolStripMenuItem

        System.Threading.Thread.Sleep(1000)
    End Sub
End Class
