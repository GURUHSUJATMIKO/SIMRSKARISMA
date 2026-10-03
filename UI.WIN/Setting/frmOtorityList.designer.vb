<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmOtorityList
    Inherits DevExpress.XtraEditors.XtraForm

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
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
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim GridLevelNode1 As DevExpress.XtraGrid.GridLevelNode = New DevExpress.XtraGrid.GridLevelNode()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmOtorityList))
        Me.grv1 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.grd = New DevExpress.XtraGrid.GridControl()
        Me.mnuStrip = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.MasterColumnChooserToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.DetailColumnChooserToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.grv = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.PanelControl1 = New DevExpress.XtraEditors.PanelControl()
        Me.LabelControl6 = New DevExpress.XtraEditors.LabelControl()
        Me.picAdd = New DevExpress.XtraEditors.PictureEdit()
        Me.picRefresh = New DevExpress.XtraEditors.PictureEdit()
        Me.LabelControl4 = New DevExpress.XtraEditors.LabelControl()
        Me.picUpdate = New DevExpress.XtraEditors.PictureEdit()
        Me.LabelControl3 = New DevExpress.XtraEditors.LabelControl()
        Me.picDelete = New DevExpress.XtraEditors.PictureEdit()
        Me.LabelControl1 = New DevExpress.XtraEditors.LabelControl()
        Me.BarButtonItem1 = New DevExpress.XtraBars.BarButtonItem()
        Me.BarButtonItem2 = New DevExpress.XtraBars.BarButtonItem()
        Me.BarButtonItem3 = New DevExpress.XtraBars.BarButtonItem()
        CType(Me.grv1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grd, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.mnuStrip.SuspendLayout()
        CType(Me.grv, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PanelControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelControl1.SuspendLayout()
        CType(Me.picAdd.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.picRefresh.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.picUpdate.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.picDelete.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'grv1
        '
        Me.grv1.GridControl = Me.grd
        Me.grv1.Name = "grv1"
        Me.grv1.OptionsView.ShowAutoFilterRow = True
        Me.grv1.OptionsView.ShowIndicator = False
        '
        'grd
        '
        Me.grd.ContextMenuStrip = Me.mnuStrip
        Me.grd.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grd.EmbeddedNavigator.Buttons.Append.Visible = False
        Me.grd.EmbeddedNavigator.Buttons.CancelEdit.Visible = False
        Me.grd.EmbeddedNavigator.Buttons.Edit.Visible = False
        Me.grd.EmbeddedNavigator.Buttons.EndEdit.Visible = False
        Me.grd.EmbeddedNavigator.Buttons.Remove.Visible = False
        GridLevelNode1.LevelTemplate = Me.grv1
        GridLevelNode1.RelationName = "Level1"
        Me.grd.LevelTree.Nodes.AddRange(New DevExpress.XtraGrid.GridLevelNode() {GridLevelNode1})
        Me.grd.Location = New System.Drawing.Point(0, 82)
        Me.grd.MainView = Me.grv
        Me.grd.Name = "grd"
        Me.grd.Size = New System.Drawing.Size(792, 491)
        Me.grd.TabIndex = 2
        Me.grd.UseEmbeddedNavigator = True
        Me.grd.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.grv, Me.grv1})
        '
        'mnuStrip
        '
        Me.mnuStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.MasterColumnChooserToolStripMenuItem, Me.DetailColumnChooserToolStripMenuItem})
        Me.mnuStrip.Name = "mnuStrip"
        Me.mnuStrip.Size = New System.Drawing.Size(189, 48)
        '
        'MasterColumnChooserToolStripMenuItem
        '
        Me.MasterColumnChooserToolStripMenuItem.Name = "MasterColumnChooserToolStripMenuItem"
        Me.MasterColumnChooserToolStripMenuItem.Size = New System.Drawing.Size(188, 22)
        Me.MasterColumnChooserToolStripMenuItem.Text = "Master Column Chooser"
        '
        'DetailColumnChooserToolStripMenuItem
        '
        Me.DetailColumnChooserToolStripMenuItem.Name = "DetailColumnChooserToolStripMenuItem"
        Me.DetailColumnChooserToolStripMenuItem.Size = New System.Drawing.Size(188, 22)
        Me.DetailColumnChooserToolStripMenuItem.Text = "Detail Column Chooser"
        '
        'grv
        '
        Me.grv.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.grv.GridControl = Me.grd
        Me.grv.Name = "grv"
        Me.grv.OptionsBehavior.Editable = False
        Me.grv.OptionsBehavior.ReadOnly = True
        Me.grv.OptionsFind.AlwaysVisible = True
        Me.grv.OptionsFind.ShowFindButton = False
        Me.grv.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.grv.OptionsView.ShowAutoFilterRow = True
        Me.grv.OptionsView.ShowFooter = True
        Me.grv.OptionsView.ShowGroupedColumns = True
        '
        'PanelControl1
        '
        Me.PanelControl1.Controls.Add(Me.LabelControl6)
        Me.PanelControl1.Controls.Add(Me.picAdd)
        Me.PanelControl1.Controls.Add(Me.picRefresh)
        Me.PanelControl1.Controls.Add(Me.LabelControl4)
        Me.PanelControl1.Controls.Add(Me.picUpdate)
        Me.PanelControl1.Controls.Add(Me.LabelControl3)
        Me.PanelControl1.Controls.Add(Me.picDelete)
        Me.PanelControl1.Controls.Add(Me.LabelControl1)
        Me.PanelControl1.Dock = System.Windows.Forms.DockStyle.Top
        Me.PanelControl1.Location = New System.Drawing.Point(0, 0)
        Me.PanelControl1.Name = "PanelControl1"
        Me.PanelControl1.Size = New System.Drawing.Size(792, 82)
        Me.PanelControl1.TabIndex = 1
        '
        'LabelControl6
        '
        Me.LabelControl6.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.LabelControl6.Location = New System.Drawing.Point(182, 60)
        Me.LabelControl6.Name = "LabelControl6"
        Me.LabelControl6.Size = New System.Drawing.Size(44, 13)
        Me.LabelControl6.TabIndex = 13
        Me.LabelControl6.Text = "&Refresh"
        '
        'picAdd
        '
        Me.picAdd.EditValue = CType(resources.GetObject("picAdd.EditValue"), Object)
        Me.picAdd.Location = New System.Drawing.Point(18, 10)
        Me.picAdd.Name = "picAdd"
        Me.picAdd.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.picAdd.Properties.Appearance.Options.UseBackColor = True
        Me.picAdd.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.picAdd.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Zoom
        Me.picAdd.Size = New System.Drawing.Size(48, 48)
        Me.picAdd.TabIndex = 8
        '
        'picRefresh
        '
        Me.picRefresh.EditValue = CType(resources.GetObject("picRefresh.EditValue"), Object)
        Me.picRefresh.Location = New System.Drawing.Point(180, 10)
        Me.picRefresh.Name = "picRefresh"
        Me.picRefresh.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.picRefresh.Properties.Appearance.Options.UseBackColor = True
        Me.picRefresh.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.picRefresh.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Zoom
        Me.picRefresh.Size = New System.Drawing.Size(48, 48)
        Me.picRefresh.TabIndex = 12
        '
        'LabelControl4
        '
        Me.LabelControl4.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.LabelControl4.Location = New System.Drawing.Point(132, 60)
        Me.LabelControl4.Name = "LabelControl4"
        Me.LabelControl4.Size = New System.Drawing.Size(37, 13)
        Me.LabelControl4.TabIndex = 11
        Me.LabelControl4.Text = "&Delete"
        '
        'picUpdate
        '
        Me.picUpdate.EditValue = CType(resources.GetObject("picUpdate.EditValue"), Object)
        Me.picUpdate.Location = New System.Drawing.Point(72, 10)
        Me.picUpdate.Name = "picUpdate"
        Me.picUpdate.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.picUpdate.Properties.Appearance.Options.UseBackColor = True
        Me.picUpdate.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.picUpdate.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Zoom
        Me.picUpdate.Size = New System.Drawing.Size(48, 48)
        Me.picUpdate.TabIndex = 6
        '
        'LabelControl3
        '
        Me.LabelControl3.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.LabelControl3.Location = New System.Drawing.Point(86, 60)
        Me.LabelControl3.Name = "LabelControl3"
        Me.LabelControl3.Size = New System.Drawing.Size(21, 13)
        Me.LabelControl3.TabIndex = 9
        Me.LabelControl3.Text = "&Edit"
        '
        'picDelete
        '
        Me.picDelete.EditValue = CType(resources.GetObject("picDelete.EditValue"), Object)
        Me.picDelete.Location = New System.Drawing.Point(126, 10)
        Me.picDelete.Name = "picDelete"
        Me.picDelete.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.picDelete.Properties.Appearance.Options.UseBackColor = True
        Me.picDelete.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.picDelete.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Zoom
        Me.picDelete.Size = New System.Drawing.Size(48, 48)
        Me.picDelete.TabIndex = 7
        '
        'LabelControl1
        '
        Me.LabelControl1.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.LabelControl1.Location = New System.Drawing.Point(31, 60)
        Me.LabelControl1.Name = "LabelControl1"
        Me.LabelControl1.Size = New System.Drawing.Size(22, 13)
        Me.LabelControl1.TabIndex = 5
        Me.LabelControl1.Text = "&Add"
        '
        'BarButtonItem1
        '
        Me.BarButtonItem1.Caption = "New"
        Me.BarButtonItem1.Id = 0
        Me.BarButtonItem1.Name = "BarButtonItem1"
        '
        'BarButtonItem2
        '
        Me.BarButtonItem2.Caption = "Edit"
        Me.BarButtonItem2.Id = 1
        Me.BarButtonItem2.Name = "BarButtonItem2"
        '
        'BarButtonItem3
        '
        Me.BarButtonItem3.Caption = "Delete"
        Me.BarButtonItem3.Id = 2
        Me.BarButtonItem3.Name = "BarButtonItem3"
        '
        'frmOtorityList
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(792, 573)
        Me.Controls.Add(Me.grd)
        Me.Controls.Add(Me.PanelControl1)
        Me.KeyPreview = True
        Me.Name = "frmOtorityList"
        Me.ShowIcon = False
        CType(Me.grv1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grd, System.ComponentModel.ISupportInitialize).EndInit()
        Me.mnuStrip.ResumeLayout(False)
        CType(Me.grv, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PanelControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelControl1.ResumeLayout(False)
        Me.PanelControl1.PerformLayout()
        CType(Me.picAdd.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.picRefresh.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.picUpdate.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.picDelete.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents BarButtonItem1 As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents BarButtonItem2 As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents BarButtonItem3 As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents PanelControl1 As DevExpress.XtraEditors.PanelControl
    Friend WithEvents LabelControl6 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents picRefresh As DevExpress.XtraEditors.PictureEdit
    Friend WithEvents LabelControl4 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl3 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl1 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents picDelete As DevExpress.XtraEditors.PictureEdit
    Friend WithEvents picUpdate As DevExpress.XtraEditors.PictureEdit
    Friend WithEvents picAdd As DevExpress.XtraEditors.PictureEdit
    Friend WithEvents mnuStrip As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents MasterColumnChooserToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents DetailColumnChooserToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents grv1 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents grd As DevExpress.XtraGrid.GridControl
    Friend WithEvents grv As DevExpress.XtraGrid.Views.Grid.GridView
End Class
