<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmRiwayatResep
    Inherits DevExpress.XtraEditors.XtraForm

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim GridLevelNode1 As DevExpress.XtraGrid.GridLevelNode = New DevExpress.XtraGrid.GridLevelNode()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmRiwayatResep))
        Me.grv1 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.grd = New DevExpress.XtraGrid.GridControl()
        Me.mnuStrip = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.AmbilTransaksiToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.grv = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GroupControl1 = New DevExpress.XtraEditors.GroupControl()
        Me.panelMenu = New DevExpress.XtraEditors.PanelControl()
        Me.LabelControl6 = New DevExpress.XtraEditors.LabelControl()
        Me.picRefresh = New DevExpress.XtraEditors.PictureEdit()
        Me.printSystem = New DevExpress.XtraPrinting.PrintingSystem(Me.components)
        Me.printableComponentLink = New DevExpress.XtraPrinting.PrintableComponentLink(Me.components)
        CType(Me.grv1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grd, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.mnuStrip.SuspendLayout()
        CType(Me.grv, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GroupControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl1.SuspendLayout()
        CType(Me.panelMenu, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.panelMenu.SuspendLayout()
        CType(Me.picRefresh.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.printSystem, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'grv1
        '
        Me.grv1.GridControl = Me.grd
        Me.grv1.Name = "grv1"
        Me.grv1.OptionsBehavior.Editable = False
        Me.grv1.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.grv1.OptionsView.ShowAutoFilterRow = True
        '
        'grd
        '
        Me.grd.ContextMenuStrip = Me.mnuStrip
        Me.grd.Dock = System.Windows.Forms.DockStyle.Fill
        GridLevelNode1.LevelTemplate = Me.grv1
        GridLevelNode1.RelationName = "Level1"
        Me.grd.LevelTree.Nodes.AddRange(New DevExpress.XtraGrid.GridLevelNode() {GridLevelNode1})
        Me.grd.Location = New System.Drawing.Point(2, 20)
        Me.grd.MainView = Me.grv
        Me.grd.Name = "grd"
        Me.grd.ShowOnlyPredefinedDetails = True
        Me.grd.Size = New System.Drawing.Size(956, 526)
        Me.grd.TabIndex = 0
        Me.grd.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.grv, Me.grv1})
        '
        'mnuStrip
        '
        Me.mnuStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.AmbilTransaksiToolStripMenuItem})
        Me.mnuStrip.Name = "mnuStrip"
        Me.mnuStrip.Size = New System.Drawing.Size(178, 26)
        '
        'AmbilTransaksiToolStripMenuItem
        '
        Me.AmbilTransaksiToolStripMenuItem.Name = "AmbilTransaksiToolStripMenuItem"
        Me.AmbilTransaksiToolStripMenuItem.Size = New System.Drawing.Size(177, 22)
        Me.AmbilTransaksiToolStripMenuItem.Text = "Salin Riwayat Resep"
        '
        'grv
        '
        Me.grv.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.grv.GridControl = Me.grd
        Me.grv.Name = "grv"
        Me.grv.OptionsBehavior.Editable = False
        Me.grv.OptionsBehavior.ReadOnly = True
        Me.grv.OptionsDetail.SmartDetailHeight = True
        Me.grv.OptionsPrint.EnableAppearanceEvenRow = True
        Me.grv.OptionsPrint.EnableAppearanceOddRow = True
        Me.grv.OptionsPrint.ExpandAllDetails = True
        Me.grv.OptionsPrint.PrintDetails = True
        Me.grv.OptionsPrint.PrintFilterInfo = True
        Me.grv.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.grv.OptionsView.ShowAutoFilterRow = True
        Me.grv.OptionsView.ShowFooter = True
        '
        'GroupControl1
        '
        Me.GroupControl1.Controls.Add(Me.grd)
        Me.GroupControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupControl1.Location = New System.Drawing.Point(0, 109)
        Me.GroupControl1.Name = "GroupControl1"
        Me.GroupControl1.Size = New System.Drawing.Size(960, 548)
        Me.GroupControl1.TabIndex = 4
        Me.GroupControl1.Text = "Preview"
        '
        'panelMenu
        '
        Me.panelMenu.Appearance.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.panelMenu.Appearance.BackColor2 = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.panelMenu.Appearance.Options.UseBackColor = True
        Me.panelMenu.Controls.Add(Me.LabelControl6)
        Me.panelMenu.Controls.Add(Me.picRefresh)
        Me.panelMenu.Dock = System.Windows.Forms.DockStyle.Top
        Me.panelMenu.Location = New System.Drawing.Point(0, 0)
        Me.panelMenu.Name = "panelMenu"
        Me.panelMenu.Size = New System.Drawing.Size(960, 109)
        Me.panelMenu.TabIndex = 7
        '
        'LabelControl6
        '
        Me.LabelControl6.Appearance.Font = New System.Drawing.Font("Tahoma", 12.0!)
        Me.LabelControl6.Location = New System.Drawing.Point(16, 78)
        Me.LabelControl6.Name = "LabelControl6"
        Me.LabelControl6.Size = New System.Drawing.Size(53, 19)
        Me.LabelControl6.TabIndex = 3
        Me.LabelControl6.Text = "&Refresh"
        '
        'picRefresh
        '
        Me.picRefresh.EditValue = CType(resources.GetObject("picRefresh.EditValue"), Object)
        Me.picRefresh.Location = New System.Drawing.Point(12, 12)
        Me.picRefresh.Name = "picRefresh"
        Me.picRefresh.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.picRefresh.Properties.Appearance.Options.UseBackColor = True
        Me.picRefresh.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.picRefresh.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Zoom
        Me.picRefresh.Size = New System.Drawing.Size(60, 60)
        Me.picRefresh.TabIndex = 2
        '
        'printSystem
        '
        Me.printSystem.Links.AddRange(New Object() {Me.printableComponentLink})
        '
        'printableComponentLink
        '
        Me.printableComponentLink.Component = Me.grd
        Me.printableComponentLink.Landscape = True
        Me.printableComponentLink.PaperKind = System.Drawing.Printing.PaperKind.Custom
        Me.printableComponentLink.PrintingSystemBase = Me.printSystem
        '
        'frmRiwayatResep
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(960, 657)
        Me.Controls.Add(Me.GroupControl1)
        Me.Controls.Add(Me.panelMenu)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmRiwayatResep"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Riwayat Resep"
        CType(Me.grv1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grd, System.ComponentModel.ISupportInitialize).EndInit()
        Me.mnuStrip.ResumeLayout(False)
        CType(Me.grv, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GroupControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl1.ResumeLayout(False)
        CType(Me.panelMenu, System.ComponentModel.ISupportInitialize).EndInit()
        Me.panelMenu.ResumeLayout(False)
        Me.panelMenu.PerformLayout()
        CType(Me.picRefresh.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.printSystem, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(false)

End Sub
    Friend WithEvents GroupControl1 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents grd As DevExpress.XtraGrid.GridControl
    Friend WithEvents grv As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents panelMenu As DevExpress.XtraEditors.PanelControl
    Friend WithEvents LabelControl6 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents picRefresh As DevExpress.XtraEditors.PictureEdit
    Friend WithEvents grv1 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents printSystem As DevExpress.XtraPrinting.PrintingSystem
    Friend WithEvents printableComponentLink As DevExpress.XtraPrinting.PrintableComponentLink
    Friend WithEvents mnuStrip As ContextMenuStrip
    Friend WithEvents AmbilTransaksiToolStripMenuItem As ToolStripMenuItem
End Class
