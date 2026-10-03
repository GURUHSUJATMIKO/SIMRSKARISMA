<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmReportAplicares
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmReportAplicares))
        Me.grv1 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.grd = New DevExpress.XtraGrid.GridControl()
        Me.grv = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.mnuStrip = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.MasterColumnChooserToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.DetailColumnChooserToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.LayoutControl1 = New DevExpress.XtraLayout.LayoutControl()
        Me.cboTYPE = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.LayoutControl2 = New DevExpress.XtraLayout.LayoutControl()
        Me.Root = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.LayoutControlGroup1 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.LayoutControlItem3 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.lTYPE = New DevExpress.XtraLayout.LayoutControlItem()
        Me.GroupControl1 = New DevExpress.XtraEditors.GroupControl()
        Me.panelMenu = New DevExpress.XtraEditors.PanelControl()
        Me.LabelControl6 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl7 = New DevExpress.XtraEditors.LabelControl()
        Me.picRefresh = New DevExpress.XtraEditors.PictureEdit()
        Me.picPrint = New DevExpress.XtraEditors.PictureEdit()
        Me.GroupControl2 = New DevExpress.XtraEditors.GroupControl()
        Me.printSystem = New DevExpress.XtraPrinting.PrintingSystem(Me.components)
        Me.printableComponentLink = New DevExpress.XtraPrinting.PrintableComponentLink(Me.components)
        Me.txtLimit = New DevExpress.XtraEditors.TextEdit()
        Me.LayoutControlItem1 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.txtStart = New DevExpress.XtraEditors.TextEdit()
        Me.LayoutControlItem2 = New DevExpress.XtraLayout.LayoutControlItem()
        CType(Me.grv1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grd, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grv, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.mnuStrip.SuspendLayout()
        CType(Me.LayoutControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.LayoutControl1.SuspendLayout()
        CType(Me.cboTYPE.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControl2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Root, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lTYPE, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GroupControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl1.SuspendLayout()
        CType(Me.panelMenu, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.panelMenu.SuspendLayout()
        CType(Me.picRefresh.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.picPrint.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GroupControl2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl2.SuspendLayout()
        CType(Me.printSystem, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtLimit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtStart.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem2, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.grd.Dock = System.Windows.Forms.DockStyle.Fill
        GridLevelNode1.LevelTemplate = Me.grv1
        GridLevelNode1.RelationName = "Level1"
        Me.grd.LevelTree.Nodes.AddRange(New DevExpress.XtraGrid.GridLevelNode() {GridLevelNode1})
        Me.grd.Location = New System.Drawing.Point(2, 20)
        Me.grd.MainView = Me.grv
        Me.grd.Name = "grd"
        Me.grd.ShowOnlyPredefinedDetails = True
        Me.grd.Size = New System.Drawing.Size(640, 343)
        Me.grd.TabIndex = 9
        Me.grd.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.grv, Me.grv1})
        '
        'grv
        '
        Me.grv.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.grv.GridControl = Me.grd
        Me.grv.Name = "grv"
        Me.grv.OptionsBehavior.Editable = False
        Me.grv.OptionsBehavior.ReadOnly = True
        Me.grv.OptionsDetail.SmartDetailHeight = True
        Me.grv.OptionsPrint.ExpandAllDetails = True
        Me.grv.OptionsPrint.PrintDetails = True
        Me.grv.OptionsPrint.PrintFilterInfo = True
        Me.grv.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.grv.OptionsView.AllowCellMerge = True
        Me.grv.OptionsView.ShowAutoFilterRow = True
        Me.grv.OptionsView.ShowFooter = True
        '
        'mnuStrip
        '
        Me.mnuStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.MasterColumnChooserToolStripMenuItem, Me.DetailColumnChooserToolStripMenuItem})
        Me.mnuStrip.Name = "mnuStrip"
        Me.mnuStrip.Size = New System.Drawing.Size(204, 48)
        '
        'MasterColumnChooserToolStripMenuItem
        '
        Me.MasterColumnChooserToolStripMenuItem.Name = "MasterColumnChooserToolStripMenuItem"
        Me.MasterColumnChooserToolStripMenuItem.Size = New System.Drawing.Size(203, 22)
        Me.MasterColumnChooserToolStripMenuItem.Text = "Master Column Chooser"
        '
        'DetailColumnChooserToolStripMenuItem
        '
        Me.DetailColumnChooserToolStripMenuItem.Name = "DetailColumnChooserToolStripMenuItem"
        Me.DetailColumnChooserToolStripMenuItem.Size = New System.Drawing.Size(203, 22)
        Me.DetailColumnChooserToolStripMenuItem.Text = "Detail Column Chooser"
        '
        'LayoutControl1
        '
        Me.LayoutControl1.Controls.Add(Me.txtStart)
        Me.LayoutControl1.Controls.Add(Me.txtLimit)
        Me.LayoutControl1.Controls.Add(Me.cboTYPE)
        Me.LayoutControl1.Controls.Add(Me.LayoutControl2)
        Me.LayoutControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LayoutControl1.Location = New System.Drawing.Point(2, 20)
        Me.LayoutControl1.Name = "LayoutControl1"
        Me.LayoutControl1.Root = Me.LayoutControlGroup1
        Me.LayoutControl1.Size = New System.Drawing.Size(640, 96)
        Me.LayoutControl1.TabIndex = 2
        Me.LayoutControl1.Text = "LayoutControl1"
        '
        'cboTYPE
        '
        Me.cboTYPE.Location = New System.Drawing.Point(117, 12)
        Me.cboTYPE.Name = "cboTYPE"
        Me.cboTYPE.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cboTYPE.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
        Me.cboTYPE.Size = New System.Drawing.Size(113, 20)
        Me.cboTYPE.StyleController = Me.LayoutControl1
        Me.cboTYPE.TabIndex = 5
        '
        'LayoutControl2
        '
        Me.LayoutControl2.Location = New System.Drawing.Point(234, 12)
        Me.LayoutControl2.Name = "LayoutControl2"
        Me.LayoutControl2.Root = Me.Root
        Me.LayoutControl2.Size = New System.Drawing.Size(394, 72)
        Me.LayoutControl2.TabIndex = 4
        Me.LayoutControl2.Text = "LayoutControl2"
        '
        'Root
        '
        Me.Root.CustomizationFormText = "Root"
        Me.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.Root.GroupBordersVisible = False
        Me.Root.Location = New System.Drawing.Point(0, 0)
        Me.Root.Name = "Root"
        Me.Root.Size = New System.Drawing.Size(394, 72)
        Me.Root.TextVisible = False
        '
        'LayoutControlGroup1
        '
        Me.LayoutControlGroup1.CustomizationFormText = "LayoutControlGroup1"
        Me.LayoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.LayoutControlGroup1.GroupBordersVisible = False
        Me.LayoutControlGroup1.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlItem3, Me.lTYPE, Me.LayoutControlItem1, Me.LayoutControlItem2})
        Me.LayoutControlGroup1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlGroup1.Name = "LayoutControlGroup1"
        Me.LayoutControlGroup1.Size = New System.Drawing.Size(640, 96)
        Me.LayoutControlGroup1.TextVisible = False
        '
        'LayoutControlItem3
        '
        Me.LayoutControlItem3.Control = Me.LayoutControl2
        Me.LayoutControlItem3.CustomizationFormText = "LayoutControlItem3"
        Me.LayoutControlItem3.Location = New System.Drawing.Point(222, 0)
        Me.LayoutControlItem3.Name = "LayoutControlItem3"
        Me.LayoutControlItem3.Size = New System.Drawing.Size(398, 76)
        Me.LayoutControlItem3.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem3.TextVisible = False
        '
        'lTYPE
        '
        Me.lTYPE.AppearanceItemCaption.Options.UseTextOptions = True
        Me.lTYPE.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.lTYPE.Control = Me.cboTYPE
        Me.lTYPE.CustomizationFormText = "Type :"
        Me.lTYPE.Location = New System.Drawing.Point(0, 0)
        Me.lTYPE.Name = "lTYPE"
        Me.lTYPE.Size = New System.Drawing.Size(222, 24)
        Me.lTYPE.Text = "Type :"
        Me.lTYPE.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.lTYPE.TextSize = New System.Drawing.Size(100, 20)
        Me.lTYPE.TextToControlDistance = 5
        '
        'GroupControl1
        '
        Me.GroupControl1.Controls.Add(Me.grd)
        Me.GroupControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupControl1.Location = New System.Drawing.Point(0, 207)
        Me.GroupControl1.Name = "GroupControl1"
        Me.GroupControl1.Size = New System.Drawing.Size(644, 365)
        Me.GroupControl1.TabIndex = 4
        Me.GroupControl1.Text = "Preview"
        '
        'panelMenu
        '
        Me.panelMenu.Appearance.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.panelMenu.Appearance.BackColor2 = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.panelMenu.Appearance.Options.UseBackColor = True
        Me.panelMenu.Controls.Add(Me.LabelControl6)
        Me.panelMenu.Controls.Add(Me.LabelControl7)
        Me.panelMenu.Controls.Add(Me.picRefresh)
        Me.panelMenu.Controls.Add(Me.picPrint)
        Me.panelMenu.Dock = System.Windows.Forms.DockStyle.Top
        Me.panelMenu.Location = New System.Drawing.Point(0, 0)
        Me.panelMenu.Name = "panelMenu"
        Me.panelMenu.Size = New System.Drawing.Size(644, 89)
        Me.panelMenu.TabIndex = 7
        '
        'LabelControl6
        '
        Me.LabelControl6.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.LabelControl6.Location = New System.Drawing.Point(68, 62)
        Me.LabelControl6.Name = "LabelControl6"
        Me.LabelControl6.Size = New System.Drawing.Size(44, 13)
        Me.LabelControl6.TabIndex = 17
        Me.LabelControl6.Text = "&Refresh"
        '
        'LabelControl7
        '
        Me.LabelControl7.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.LabelControl7.Location = New System.Drawing.Point(23, 62)
        Me.LabelControl7.Name = "LabelControl7"
        Me.LabelControl7.Size = New System.Drawing.Size(27, 13)
        Me.LabelControl7.TabIndex = 15
        Me.LabelControl7.Text = "&Print"
        '
        'picRefresh
        '
        Me.picRefresh.EditValue = CType(resources.GetObject("picRefresh.EditValue"), Object)
        Me.picRefresh.Location = New System.Drawing.Point(66, 12)
        Me.picRefresh.Name = "picRefresh"
        Me.picRefresh.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.picRefresh.Properties.Appearance.Options.UseBackColor = True
        Me.picRefresh.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.picRefresh.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Zoom
        Me.picRefresh.Size = New System.Drawing.Size(48, 48)
        Me.picRefresh.TabIndex = 16
        '
        'picPrint
        '
        Me.picPrint.EditValue = CType(resources.GetObject("picPrint.EditValue"), Object)
        Me.picPrint.Location = New System.Drawing.Point(12, 12)
        Me.picPrint.Name = "picPrint"
        Me.picPrint.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.picPrint.Properties.Appearance.Options.UseBackColor = True
        Me.picPrint.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.picPrint.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Zoom
        Me.picPrint.Size = New System.Drawing.Size(48, 48)
        Me.picPrint.TabIndex = 14
        '
        'GroupControl2
        '
        Me.GroupControl2.Controls.Add(Me.LayoutControl1)
        Me.GroupControl2.Dock = System.Windows.Forms.DockStyle.Top
        Me.GroupControl2.Location = New System.Drawing.Point(0, 89)
        Me.GroupControl2.Name = "GroupControl2"
        Me.GroupControl2.Size = New System.Drawing.Size(644, 118)
        Me.GroupControl2.TabIndex = 4
        Me.GroupControl2.Text = "Filter"
        '
        'printSystem
        '
        Me.printSystem.Links.AddRange(New Object() {Me.printableComponentLink})
        '
        'printableComponentLink
        '
        Me.printableComponentLink.Component = Me.grd
        Me.printableComponentLink.Landscape = True
        Me.printableComponentLink.Margins = New System.Drawing.Printing.Margins(25, 25, 100, 25)
        Me.printableComponentLink.PaperKind = System.Drawing.Printing.PaperKind.Custom
        Me.printableComponentLink.PrintingSystemBase = Me.printSystem
        '
        'txtLimit
        '
        Me.txtLimit.EditValue = "100"
        Me.txtLimit.Location = New System.Drawing.Point(117, 60)
        Me.txtLimit.Name = "txtLimit"
        Me.txtLimit.Size = New System.Drawing.Size(113, 20)
        Me.txtLimit.StyleController = Me.LayoutControl1
        Me.txtLimit.TabIndex = 6
        '
        'LayoutControlItem1
        '
        Me.LayoutControlItem1.AppearanceItemCaption.Options.UseTextOptions = True
        Me.LayoutControlItem1.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.LayoutControlItem1.Control = Me.txtLimit
        Me.LayoutControlItem1.Location = New System.Drawing.Point(0, 48)
        Me.LayoutControlItem1.Name = "LayoutControlItem1"
        Me.LayoutControlItem1.Size = New System.Drawing.Size(222, 28)
        Me.LayoutControlItem1.Text = "Limit :"
        Me.LayoutControlItem1.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.LayoutControlItem1.TextSize = New System.Drawing.Size(100, 20)
        Me.LayoutControlItem1.TextToControlDistance = 5
        '
        'txtStart
        '
        Me.txtStart.EditValue = "1"
        Me.txtStart.Location = New System.Drawing.Point(117, 36)
        Me.txtStart.Name = "txtStart"
        Me.txtStart.Size = New System.Drawing.Size(113, 20)
        Me.txtStart.StyleController = Me.LayoutControl1
        Me.txtStart.TabIndex = 7
        '
        'LayoutControlItem2
        '
        Me.LayoutControlItem2.AppearanceItemCaption.Options.UseTextOptions = True
        Me.LayoutControlItem2.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.LayoutControlItem2.Control = Me.txtStart
        Me.LayoutControlItem2.Location = New System.Drawing.Point(0, 24)
        Me.LayoutControlItem2.Name = "LayoutControlItem2"
        Me.LayoutControlItem2.Size = New System.Drawing.Size(222, 24)
        Me.LayoutControlItem2.Text = "Start :"
        Me.LayoutControlItem2.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.LayoutControlItem2.TextSize = New System.Drawing.Size(100, 20)
        Me.LayoutControlItem2.TextToControlDistance = 5
        '
        'frmReportAplicares
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(644, 572)
        Me.Controls.Add(Me.GroupControl1)
        Me.Controls.Add(Me.GroupControl2)
        Me.Controls.Add(Me.panelMenu)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmReportAplicares"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        CType(Me.grv1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grd, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grv, System.ComponentModel.ISupportInitialize).EndInit()
        Me.mnuStrip.ResumeLayout(False)
        CType(Me.LayoutControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.LayoutControl1.ResumeLayout(False)
        CType(Me.cboTYPE.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControl2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Root, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lTYPE, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GroupControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl1.ResumeLayout(False)
        CType(Me.panelMenu, System.ComponentModel.ISupportInitialize).EndInit()
        Me.panelMenu.ResumeLayout(False)
        Me.panelMenu.PerformLayout()
        CType(Me.picRefresh.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.picPrint.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GroupControl2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl2.ResumeLayout(False)
        CType(Me.printSystem, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtLimit.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtStart.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents GroupControl1 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents panelMenu As DevExpress.XtraEditors.PanelControl
    Friend WithEvents GroupControl2 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents printSystem As DevExpress.XtraPrinting.PrintingSystem
    Friend WithEvents printableComponentLink As DevExpress.XtraPrinting.PrintableComponentLink
    Friend WithEvents mnuStrip As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents MasterColumnChooserToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents DetailColumnChooserToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents LabelControl6 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl7 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents picRefresh As DevExpress.XtraEditors.PictureEdit
    Friend WithEvents picPrint As DevExpress.XtraEditors.PictureEdit
    Friend WithEvents LayoutControl1 As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents LayoutControl2 As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents Root As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents LayoutControlGroup1 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents LayoutControlItem3 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents cboTYPE As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents lTYPE As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents grd As DevExpress.XtraGrid.GridControl
    Friend WithEvents grv1 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents grv As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents txtStart As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txtLimit As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LayoutControlItem1 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem2 As DevExpress.XtraLayout.LayoutControlItem
End Class
