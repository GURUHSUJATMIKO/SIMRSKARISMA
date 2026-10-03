<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmGrouperList
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmGrouperList))
        Me.grd = New DevExpress.XtraGrid.GridControl()
        Me.mnuStrip = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.ColumnChooserToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.grv = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.PanelControl1 = New DevExpress.XtraEditors.PanelControl()
        Me.LabelControl6 = New DevExpress.XtraEditors.LabelControl()
        Me.picAdd = New DevExpress.XtraEditors.PictureEdit()
        Me.LabelControl7 = New DevExpress.XtraEditors.LabelControl()
        Me.picRefresh = New DevExpress.XtraEditors.PictureEdit()
        Me.LabelControl4 = New DevExpress.XtraEditors.LabelControl()
        Me.picUpdate = New DevExpress.XtraEditors.PictureEdit()
        Me.LabelControl3 = New DevExpress.XtraEditors.LabelControl()
        Me.picDelete = New DevExpress.XtraEditors.PictureEdit()
        Me.LabelControl1 = New DevExpress.XtraEditors.LabelControl()
        Me.picPrint = New DevExpress.XtraEditors.PictureEdit()
        Me.BarButtonItem1 = New DevExpress.XtraBars.BarButtonItem()
        Me.BarButtonItem2 = New DevExpress.XtraBars.BarButtonItem()
        Me.BarButtonItem3 = New DevExpress.XtraBars.BarButtonItem()
        CType(Me.grd, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.mnuStrip.SuspendLayout()
        CType(Me.grv, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PanelControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelControl1.SuspendLayout()
        CType(Me.picAdd.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.picRefresh.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.picUpdate.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.picDelete.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.picPrint.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
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
        Me.grd.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.grd.Location = New System.Drawing.Point(0, 101)
        Me.grd.MainView = Me.grv
        Me.grd.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.grd.Name = "grd"
        Me.grd.Size = New System.Drawing.Size(924, 604)
        Me.grd.TabIndex = 0
        Me.grd.UseEmbeddedNavigator = True
        Me.grd.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.grv})
        '
        'mnuStrip
        '
        Me.mnuStrip.ImageScalingSize = New System.Drawing.Size(20, 20)
        Me.mnuStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ColumnChooserToolStripMenuItem})
        Me.mnuStrip.Name = "mnuStrip"
        Me.mnuStrip.Size = New System.Drawing.Size(188, 28)
        '
        'ColumnChooserToolStripMenuItem
        '
        Me.ColumnChooserToolStripMenuItem.Name = "ColumnChooserToolStripMenuItem"
        Me.ColumnChooserToolStripMenuItem.Size = New System.Drawing.Size(187, 24)
        Me.ColumnChooserToolStripMenuItem.Text = "Column Chooser"
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
        Me.grv.OptionsView.ShowGroupPanel = False
        '
        'PanelControl1
        '
        Me.PanelControl1.Controls.Add(Me.LabelControl6)
        Me.PanelControl1.Controls.Add(Me.picAdd)
        Me.PanelControl1.Controls.Add(Me.LabelControl7)
        Me.PanelControl1.Controls.Add(Me.picRefresh)
        Me.PanelControl1.Controls.Add(Me.LabelControl4)
        Me.PanelControl1.Controls.Add(Me.picUpdate)
        Me.PanelControl1.Controls.Add(Me.LabelControl3)
        Me.PanelControl1.Controls.Add(Me.picDelete)
        Me.PanelControl1.Controls.Add(Me.LabelControl1)
        Me.PanelControl1.Controls.Add(Me.picPrint)
        Me.PanelControl1.Dock = System.Windows.Forms.DockStyle.Top
        Me.PanelControl1.Location = New System.Drawing.Point(0, 0)
        Me.PanelControl1.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.PanelControl1.Name = "PanelControl1"
        Me.PanelControl1.Size = New System.Drawing.Size(924, 101)
        Me.PanelControl1.TabIndex = 1
        '
        'LabelControl6
        '
        Me.LabelControl6.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.LabelControl6.Location = New System.Drawing.Point(275, 74)
        Me.LabelControl6.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.LabelControl6.Name = "LabelControl6"
        Me.LabelControl6.Size = New System.Drawing.Size(53, 17)
        Me.LabelControl6.TabIndex = 13
        Me.LabelControl6.Text = "&Refresh"
        '
        'picAdd
        '
        Me.picAdd.EditValue = CType(resources.GetObject("picAdd.EditValue"), Object)
        Me.picAdd.Location = New System.Drawing.Point(21, 12)
        Me.picAdd.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.picAdd.Name = "picAdd"
        Me.picAdd.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.picAdd.Properties.Appearance.Options.UseBackColor = True
        Me.picAdd.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.picAdd.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Zoom
        Me.picAdd.Size = New System.Drawing.Size(56, 59)
        Me.picAdd.TabIndex = 8
        '
        'LabelControl7
        '
        Me.LabelControl7.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.LabelControl7.Location = New System.Drawing.Point(223, 74)
        Me.LabelControl7.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.LabelControl7.Name = "LabelControl7"
        Me.LabelControl7.Size = New System.Drawing.Size(33, 17)
        Me.LabelControl7.TabIndex = 10
        Me.LabelControl7.Text = "&Print"
        '
        'picRefresh
        '
        Me.picRefresh.EditValue = CType(resources.GetObject("picRefresh.EditValue"), Object)
        Me.picRefresh.Location = New System.Drawing.Point(273, 12)
        Me.picRefresh.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.picRefresh.Name = "picRefresh"
        Me.picRefresh.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.picRefresh.Properties.Appearance.Options.UseBackColor = True
        Me.picRefresh.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.picRefresh.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Zoom
        Me.picRefresh.Size = New System.Drawing.Size(56, 59)
        Me.picRefresh.TabIndex = 12
        '
        'LabelControl4
        '
        Me.LabelControl4.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.LabelControl4.Location = New System.Drawing.Point(154, 74)
        Me.LabelControl4.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.LabelControl4.Name = "LabelControl4"
        Me.LabelControl4.Size = New System.Drawing.Size(43, 17)
        Me.LabelControl4.TabIndex = 11
        Me.LabelControl4.Text = "&Delete"
        '
        'picUpdate
        '
        Me.picUpdate.EditValue = CType(resources.GetObject("picUpdate.EditValue"), Object)
        Me.picUpdate.Location = New System.Drawing.Point(84, 12)
        Me.picUpdate.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.picUpdate.Name = "picUpdate"
        Me.picUpdate.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.picUpdate.Properties.Appearance.Options.UseBackColor = True
        Me.picUpdate.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.picUpdate.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Zoom
        Me.picUpdate.Size = New System.Drawing.Size(56, 59)
        Me.picUpdate.TabIndex = 6
        '
        'LabelControl3
        '
        Me.LabelControl3.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.LabelControl3.Location = New System.Drawing.Point(100, 74)
        Me.LabelControl3.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.LabelControl3.Name = "LabelControl3"
        Me.LabelControl3.Size = New System.Drawing.Size(27, 17)
        Me.LabelControl3.TabIndex = 9
        Me.LabelControl3.Text = "&Edit"
        '
        'picDelete
        '
        Me.picDelete.EditValue = CType(resources.GetObject("picDelete.EditValue"), Object)
        Me.picDelete.Location = New System.Drawing.Point(147, 12)
        Me.picDelete.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.picDelete.Name = "picDelete"
        Me.picDelete.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.picDelete.Properties.Appearance.Options.UseBackColor = True
        Me.picDelete.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.picDelete.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Zoom
        Me.picDelete.Size = New System.Drawing.Size(56, 59)
        Me.picDelete.TabIndex = 7
        '
        'LabelControl1
        '
        Me.LabelControl1.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.LabelControl1.Location = New System.Drawing.Point(36, 74)
        Me.LabelControl1.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.LabelControl1.Name = "LabelControl1"
        Me.LabelControl1.Size = New System.Drawing.Size(28, 17)
        Me.LabelControl1.TabIndex = 5
        Me.LabelControl1.Text = "&Add"
        '
        'picPrint
        '
        Me.picPrint.EditValue = CType(resources.GetObject("picPrint.EditValue"), Object)
        Me.picPrint.Location = New System.Drawing.Point(210, 12)
        Me.picPrint.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.picPrint.Name = "picPrint"
        Me.picPrint.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.picPrint.Properties.Appearance.Options.UseBackColor = True
        Me.picPrint.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.picPrint.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Zoom
        Me.picPrint.Size = New System.Drawing.Size(56, 59)
        Me.picPrint.TabIndex = 4
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
        'frmGrouperList
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(924, 705)
        Me.Controls.Add(Me.grd)
        Me.Controls.Add(Me.PanelControl1)
        Me.KeyPreview = True
        Me.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Name = "frmGrouperList"
        Me.ShowIcon = False
        Me.Text = "Grouper - List Form"
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
        CType(Me.picPrint.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents grd As DevExpress.XtraGrid.GridControl
    Friend WithEvents grv As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents BarButtonItem1 As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents BarButtonItem2 As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents BarButtonItem3 As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents PanelControl1 As DevExpress.XtraEditors.PanelControl
    Friend WithEvents LabelControl6 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents picRefresh As DevExpress.XtraEditors.PictureEdit
    Friend WithEvents LabelControl7 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl4 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl3 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl1 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents picPrint As DevExpress.XtraEditors.PictureEdit
    Friend WithEvents picDelete As DevExpress.XtraEditors.PictureEdit
    Friend WithEvents picUpdate As DevExpress.XtraEditors.PictureEdit
    Friend WithEvents picAdd As DevExpress.XtraEditors.PictureEdit
    Friend WithEvents mnuStrip As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents ColumnChooserToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
End Class
