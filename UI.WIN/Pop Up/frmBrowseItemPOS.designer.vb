<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmBrowseItemPOS
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmBrowseItemPOS))
        Me.grd = New DevExpress.XtraGrid.GridControl()
        Me.grv = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.colNMITEM2 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colKDUOM = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colKDITEM = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ckISOBATBPJS = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.Bawah = New DevExpress.XtraEditors.PanelControl()
        Me.cmdHelp = New System.Windows.Forms.ToolStripButton()
        Me.cmdSelect = New System.Windows.Forms.ToolStripButton()
        Me.EQMenu = New System.Windows.Forms.ToolStrip()
        Me.cmdClose = New System.Windows.Forms.ToolStripButton()
        Me.GridColumn1 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepositoryItemCheckEdit1 = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        CType(Me.grd, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grv, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ckISOBATBPJS, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Bawah, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.EQMenu.SuspendLayout()
        CType(Me.RepositoryItemCheckEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'grd
        '
        Me.grd.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grd.EmbeddedNavigator.Buttons.Append.Enabled = False
        Me.grd.EmbeddedNavigator.Buttons.Append.Visible = False
        Me.grd.EmbeddedNavigator.Buttons.CancelEdit.Enabled = False
        Me.grd.EmbeddedNavigator.Buttons.CancelEdit.Visible = False
        Me.grd.EmbeddedNavigator.Buttons.Edit.Enabled = False
        Me.grd.EmbeddedNavigator.Buttons.Edit.Visible = False
        Me.grd.EmbeddedNavigator.Buttons.EndEdit.Visible = False
        Me.grd.EmbeddedNavigator.Buttons.Remove.Visible = False
        Me.grd.Location = New System.Drawing.Point(0, 45)
        Me.grd.MainView = Me.grv
        Me.grd.Name = "grd"
        Me.grd.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.ckISOBATBPJS, Me.RepositoryItemCheckEdit1})
        Me.grd.Size = New System.Drawing.Size(494, 502)
        Me.grd.TabIndex = 0
        Me.grd.UseEmbeddedNavigator = True
        Me.grd.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.grv})
        '
        'grv
        '
        Me.grv.Appearance.ColumnFilterButton.BackColor = System.Drawing.Color.Silver
        Me.grv.Appearance.ColumnFilterButton.BackColor2 = System.Drawing.Color.FromArgb(CType(CType(212, Byte), Integer), CType(CType(212, Byte), Integer), CType(CType(212, Byte), Integer))
        Me.grv.Appearance.ColumnFilterButton.BorderColor = System.Drawing.Color.Silver
        Me.grv.Appearance.ColumnFilterButton.ForeColor = System.Drawing.Color.Gray
        Me.grv.Appearance.ColumnFilterButton.Options.UseBackColor = True
        Me.grv.Appearance.ColumnFilterButton.Options.UseBorderColor = True
        Me.grv.Appearance.ColumnFilterButton.Options.UseForeColor = True
        Me.grv.Appearance.ColumnFilterButtonActive.BackColor = System.Drawing.Color.FromArgb(CType(CType(212, Byte), Integer), CType(CType(212, Byte), Integer), CType(CType(212, Byte), Integer))
        Me.grv.Appearance.ColumnFilterButtonActive.BackColor2 = System.Drawing.Color.FromArgb(CType(CType(223, Byte), Integer), CType(CType(223, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.grv.Appearance.ColumnFilterButtonActive.BorderColor = System.Drawing.Color.FromArgb(CType(CType(212, Byte), Integer), CType(CType(212, Byte), Integer), CType(CType(212, Byte), Integer))
        Me.grv.Appearance.ColumnFilterButtonActive.ForeColor = System.Drawing.Color.Blue
        Me.grv.Appearance.ColumnFilterButtonActive.Options.UseBackColor = True
        Me.grv.Appearance.ColumnFilterButtonActive.Options.UseBorderColor = True
        Me.grv.Appearance.ColumnFilterButtonActive.Options.UseForeColor = True
        Me.grv.Appearance.Empty.BackColor = System.Drawing.Color.FromArgb(CType(CType(243, Byte), Integer), CType(CType(243, Byte), Integer), CType(CType(243, Byte), Integer))
        Me.grv.Appearance.Empty.Options.UseBackColor = True
        Me.grv.Appearance.EvenRow.BackColor = System.Drawing.Color.FromArgb(CType(CType(223, Byte), Integer), CType(CType(223, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.grv.Appearance.EvenRow.BackColor2 = System.Drawing.Color.GhostWhite
        Me.grv.Appearance.EvenRow.Font = New System.Drawing.Font("Tahoma", 10.0!)
        Me.grv.Appearance.EvenRow.ForeColor = System.Drawing.Color.Black
        Me.grv.Appearance.EvenRow.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.ForwardDiagonal
        Me.grv.Appearance.EvenRow.Options.UseBackColor = True
        Me.grv.Appearance.EvenRow.Options.UseFont = True
        Me.grv.Appearance.EvenRow.Options.UseForeColor = True
        Me.grv.Appearance.FilterCloseButton.BackColor = System.Drawing.Color.FromArgb(CType(CType(212, Byte), Integer), CType(CType(208, Byte), Integer), CType(CType(200, Byte), Integer))
        Me.grv.Appearance.FilterCloseButton.BackColor2 = System.Drawing.Color.FromArgb(CType(CType(118, Byte), Integer), CType(CType(170, Byte), Integer), CType(CType(225, Byte), Integer))
        Me.grv.Appearance.FilterCloseButton.BorderColor = System.Drawing.Color.FromArgb(CType(CType(212, Byte), Integer), CType(CType(208, Byte), Integer), CType(CType(200, Byte), Integer))
        Me.grv.Appearance.FilterCloseButton.ForeColor = System.Drawing.Color.Black
        Me.grv.Appearance.FilterCloseButton.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.ForwardDiagonal
        Me.grv.Appearance.FilterCloseButton.Options.UseBackColor = True
        Me.grv.Appearance.FilterCloseButton.Options.UseBorderColor = True
        Me.grv.Appearance.FilterCloseButton.Options.UseForeColor = True
        Me.grv.Appearance.FilterPanel.BackColor = System.Drawing.Color.FromArgb(CType(CType(28, Byte), Integer), CType(CType(80, Byte), Integer), CType(CType(135, Byte), Integer))
        Me.grv.Appearance.FilterPanel.BackColor2 = System.Drawing.Color.FromArgb(CType(CType(212, Byte), Integer), CType(CType(208, Byte), Integer), CType(CType(200, Byte), Integer))
        Me.grv.Appearance.FilterPanel.ForeColor = System.Drawing.Color.White
        Me.grv.Appearance.FilterPanel.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.ForwardDiagonal
        Me.grv.Appearance.FilterPanel.Options.UseBackColor = True
        Me.grv.Appearance.FilterPanel.Options.UseForeColor = True
        Me.grv.Appearance.FixedLine.BackColor = System.Drawing.Color.FromArgb(CType(CType(58, Byte), Integer), CType(CType(58, Byte), Integer), CType(CType(58, Byte), Integer))
        Me.grv.Appearance.FixedLine.Options.UseBackColor = True
        Me.grv.Appearance.FocusedCell.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(225, Byte), Integer))
        Me.grv.Appearance.FocusedCell.Font = New System.Drawing.Font("Tahoma", 10.0!)
        Me.grv.Appearance.FocusedCell.ForeColor = System.Drawing.Color.Black
        Me.grv.Appearance.FocusedCell.Options.UseBackColor = True
        Me.grv.Appearance.FocusedCell.Options.UseFont = True
        Me.grv.Appearance.FocusedCell.Options.UseForeColor = True
        Me.grv.Appearance.FocusedRow.BackColor = System.Drawing.Color.Navy
        Me.grv.Appearance.FocusedRow.BackColor2 = System.Drawing.Color.FromArgb(CType(CType(50, Byte), Integer), CType(CType(50, Byte), Integer), CType(CType(178, Byte), Integer))
        Me.grv.Appearance.FocusedRow.Font = New System.Drawing.Font("Tahoma", 10.0!)
        Me.grv.Appearance.FocusedRow.ForeColor = System.Drawing.Color.White
        Me.grv.Appearance.FocusedRow.Options.UseBackColor = True
        Me.grv.Appearance.FocusedRow.Options.UseFont = True
        Me.grv.Appearance.FocusedRow.Options.UseForeColor = True
        Me.grv.Appearance.FooterPanel.BackColor = System.Drawing.Color.Silver
        Me.grv.Appearance.FooterPanel.BorderColor = System.Drawing.Color.Silver
        Me.grv.Appearance.FooterPanel.Font = New System.Drawing.Font("Tahoma", 10.0!)
        Me.grv.Appearance.FooterPanel.ForeColor = System.Drawing.Color.Black
        Me.grv.Appearance.FooterPanel.Options.UseBackColor = True
        Me.grv.Appearance.FooterPanel.Options.UseBorderColor = True
        Me.grv.Appearance.FooterPanel.Options.UseFont = True
        Me.grv.Appearance.FooterPanel.Options.UseForeColor = True
        Me.grv.Appearance.GroupButton.BackColor = System.Drawing.Color.Silver
        Me.grv.Appearance.GroupButton.BorderColor = System.Drawing.Color.Silver
        Me.grv.Appearance.GroupButton.ForeColor = System.Drawing.Color.Black
        Me.grv.Appearance.GroupButton.Options.UseBackColor = True
        Me.grv.Appearance.GroupButton.Options.UseBorderColor = True
        Me.grv.Appearance.GroupButton.Options.UseForeColor = True
        Me.grv.Appearance.GroupFooter.BackColor = System.Drawing.Color.FromArgb(CType(CType(202, Byte), Integer), CType(CType(202, Byte), Integer), CType(CType(202, Byte), Integer))
        Me.grv.Appearance.GroupFooter.BorderColor = System.Drawing.Color.FromArgb(CType(CType(202, Byte), Integer), CType(CType(202, Byte), Integer), CType(CType(202, Byte), Integer))
        Me.grv.Appearance.GroupFooter.ForeColor = System.Drawing.Color.Black
        Me.grv.Appearance.GroupFooter.Options.UseBackColor = True
        Me.grv.Appearance.GroupFooter.Options.UseBorderColor = True
        Me.grv.Appearance.GroupFooter.Options.UseForeColor = True
        Me.grv.Appearance.GroupPanel.BackColor = System.Drawing.Color.FromArgb(CType(CType(58, Byte), Integer), CType(CType(110, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.grv.Appearance.GroupPanel.BackColor2 = System.Drawing.Color.White
        Me.grv.Appearance.GroupPanel.Font = New System.Drawing.Font("Tahoma", 10.0!, System.Drawing.FontStyle.Bold)
        Me.grv.Appearance.GroupPanel.ForeColor = System.Drawing.Color.White
        Me.grv.Appearance.GroupPanel.Options.UseBackColor = True
        Me.grv.Appearance.GroupPanel.Options.UseFont = True
        Me.grv.Appearance.GroupPanel.Options.UseForeColor = True
        Me.grv.Appearance.GroupRow.BackColor = System.Drawing.Color.Gray
        Me.grv.Appearance.GroupRow.Font = New System.Drawing.Font("Tahoma", 10.0!)
        Me.grv.Appearance.GroupRow.ForeColor = System.Drawing.Color.Silver
        Me.grv.Appearance.GroupRow.Options.UseBackColor = True
        Me.grv.Appearance.GroupRow.Options.UseFont = True
        Me.grv.Appearance.GroupRow.Options.UseForeColor = True
        Me.grv.Appearance.HeaderPanel.BackColor = System.Drawing.Color.Silver
        Me.grv.Appearance.HeaderPanel.BorderColor = System.Drawing.Color.Silver
        Me.grv.Appearance.HeaderPanel.Font = New System.Drawing.Font("Tahoma", 10.0!, System.Drawing.FontStyle.Bold)
        Me.grv.Appearance.HeaderPanel.ForeColor = System.Drawing.Color.Black
        Me.grv.Appearance.HeaderPanel.Options.UseBackColor = True
        Me.grv.Appearance.HeaderPanel.Options.UseBorderColor = True
        Me.grv.Appearance.HeaderPanel.Options.UseFont = True
        Me.grv.Appearance.HeaderPanel.Options.UseForeColor = True
        Me.grv.Appearance.HideSelectionRow.BackColor = System.Drawing.Color.Gray
        Me.grv.Appearance.HideSelectionRow.Font = New System.Drawing.Font("Tahoma", 10.0!)
        Me.grv.Appearance.HideSelectionRow.ForeColor = System.Drawing.Color.FromArgb(CType(CType(212, Byte), Integer), CType(CType(208, Byte), Integer), CType(CType(200, Byte), Integer))
        Me.grv.Appearance.HideSelectionRow.Options.UseBackColor = True
        Me.grv.Appearance.HideSelectionRow.Options.UseFont = True
        Me.grv.Appearance.HideSelectionRow.Options.UseForeColor = True
        Me.grv.Appearance.HorzLine.BackColor = System.Drawing.Color.Silver
        Me.grv.Appearance.HorzLine.Options.UseBackColor = True
        Me.grv.Appearance.OddRow.BackColor = System.Drawing.Color.White
        Me.grv.Appearance.OddRow.BackColor2 = System.Drawing.Color.White
        Me.grv.Appearance.OddRow.Font = New System.Drawing.Font("Tahoma", 10.0!)
        Me.grv.Appearance.OddRow.ForeColor = System.Drawing.Color.Black
        Me.grv.Appearance.OddRow.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.BackwardDiagonal
        Me.grv.Appearance.OddRow.Options.UseBackColor = True
        Me.grv.Appearance.OddRow.Options.UseFont = True
        Me.grv.Appearance.OddRow.Options.UseForeColor = True
        Me.grv.Appearance.Preview.BackColor = System.Drawing.Color.White
        Me.grv.Appearance.Preview.Font = New System.Drawing.Font("Tahoma", 10.0!)
        Me.grv.Appearance.Preview.ForeColor = System.Drawing.Color.Navy
        Me.grv.Appearance.Preview.Options.UseBackColor = True
        Me.grv.Appearance.Preview.Options.UseFont = True
        Me.grv.Appearance.Preview.Options.UseForeColor = True
        Me.grv.Appearance.Row.BackColor = System.Drawing.Color.White
        Me.grv.Appearance.Row.Font = New System.Drawing.Font("Tahoma", 10.0!)
        Me.grv.Appearance.Row.ForeColor = System.Drawing.Color.Black
        Me.grv.Appearance.Row.Options.UseBackColor = True
        Me.grv.Appearance.Row.Options.UseFont = True
        Me.grv.Appearance.Row.Options.UseForeColor = True
        Me.grv.Appearance.RowSeparator.BackColor = System.Drawing.Color.White
        Me.grv.Appearance.RowSeparator.BackColor2 = System.Drawing.Color.FromArgb(CType(CType(243, Byte), Integer), CType(CType(243, Byte), Integer), CType(CType(243, Byte), Integer))
        Me.grv.Appearance.RowSeparator.Options.UseBackColor = True
        Me.grv.Appearance.SelectedRow.BackColor = System.Drawing.Color.FromArgb(CType(CType(10, Byte), Integer), CType(CType(10, Byte), Integer), CType(CType(138, Byte), Integer))
        Me.grv.Appearance.SelectedRow.Font = New System.Drawing.Font("Tahoma", 10.0!)
        Me.grv.Appearance.SelectedRow.ForeColor = System.Drawing.Color.White
        Me.grv.Appearance.SelectedRow.Options.UseBackColor = True
        Me.grv.Appearance.SelectedRow.Options.UseFont = True
        Me.grv.Appearance.SelectedRow.Options.UseForeColor = True
        Me.grv.Appearance.TopNewRow.Font = New System.Drawing.Font("Tahoma", 10.0!)
        Me.grv.Appearance.TopNewRow.Options.UseFont = True
        Me.grv.Appearance.VertLine.BackColor = System.Drawing.Color.Silver
        Me.grv.Appearance.VertLine.Options.UseBackColor = True
        Me.grv.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.colNMITEM2, Me.colKDUOM, Me.colKDITEM, Me.GridColumn1})
        Me.grv.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.grv.GridControl = Me.grd
        Me.grv.Name = "grv"
        Me.grv.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.[False]
        Me.grv.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.[False]
        Me.grv.OptionsBehavior.AllowIncrementalSearch = True
        Me.grv.OptionsBehavior.AutoExpandAllGroups = True
        Me.grv.OptionsBehavior.Editable = False
        Me.grv.OptionsDetail.EnableMasterViewMode = False
        Me.grv.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.grv.OptionsView.EnableAppearanceEvenRow = True
        Me.grv.OptionsView.EnableAppearanceOddRow = True
        Me.grv.OptionsView.ShowAutoFilterRow = True
        Me.grv.OptionsView.ShowChildrenInGroupPanel = True
        Me.grv.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.ShowAlways
        '
        'colNMITEM2
        '
        Me.colNMITEM2.AppearanceCell.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.colNMITEM2.AppearanceCell.Options.UseFont = True
        Me.colNMITEM2.AppearanceHeader.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.colNMITEM2.AppearanceHeader.Options.UseFont = True
        Me.colNMITEM2.AppearanceHeader.Options.UseTextOptions = True
        Me.colNMITEM2.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.colNMITEM2.Caption = "Paten"
        Me.colNMITEM2.FieldName = "NMITEM2"
        Me.colNMITEM2.Name = "colNMITEM2"
        Me.colNMITEM2.Visible = True
        Me.colNMITEM2.VisibleIndex = 0
        Me.colNMITEM2.Width = 246
        '
        'colKDUOM
        '
        Me.colKDUOM.Caption = "KDUOM"
        Me.colKDUOM.FieldName = "KDUOM"
        Me.colKDUOM.Name = "colKDUOM"
        '
        'colKDITEM
        '
        Me.colKDITEM.Caption = "GridColumn1"
        Me.colKDITEM.FieldName = "KDITEM"
        Me.colKDITEM.Name = "colKDITEM"
        '
        'ckISOBATBPJS
        '
        Me.ckISOBATBPJS.AutoHeight = False
        Me.ckISOBATBPJS.Name = "ckISOBATBPJS"
        '
        'Bawah
        '
        Me.Bawah.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Bawah.Location = New System.Drawing.Point(0, 547)
        Me.Bawah.Margin = New System.Windows.Forms.Padding(4)
        Me.Bawah.Name = "Bawah"
        Me.Bawah.Size = New System.Drawing.Size(494, 25)
        Me.Bawah.TabIndex = 29
        '
        'cmdHelp
        '
        Me.cmdHelp.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right
        Me.cmdHelp.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdHelp.ForeColor = System.Drawing.Color.White
        Me.cmdHelp.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.cmdHelp.Margin = New System.Windows.Forms.Padding(0, 1, 2, 2)
        Me.cmdHelp.Name = "cmdHelp"
        Me.cmdHelp.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdHelp.Size = New System.Drawing.Size(38, 42)
        Me.cmdHelp.Text = "Help"
        Me.cmdHelp.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'cmdSelect
        '
        Me.cmdSelect.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSelect.ForeColor = System.Drawing.Color.White
        Me.cmdSelect.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.cmdSelect.Margin = New System.Windows.Forms.Padding(0, 1, 2, 2)
        Me.cmdSelect.Name = "cmdSelect"
        Me.cmdSelect.Size = New System.Drawing.Size(48, 42)
        Me.cmdSelect.Text = "&Select"
        Me.cmdSelect.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.cmdSelect.ToolTipText = " Print Item List "
        '
        'EQMenu
        '
        Me.EQMenu.BackgroundImage = CType(resources.GetObject("EQMenu.BackgroundImage"), System.Drawing.Image)
        Me.EQMenu.Font = New System.Drawing.Font("Tahoma", 9.75!)
        Me.EQMenu.ImageScalingSize = New System.Drawing.Size(20, 20)
        Me.EQMenu.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.cmdSelect, Me.cmdClose, Me.cmdHelp})
        Me.EQMenu.Location = New System.Drawing.Point(0, 0)
        Me.EQMenu.Margin = New System.Windows.Forms.Padding(2)
        Me.EQMenu.MinimumSize = New System.Drawing.Size(0, 45)
        Me.EQMenu.Name = "EQMenu"
        Me.EQMenu.Size = New System.Drawing.Size(494, 45)
        Me.EQMenu.Stretch = True
        Me.EQMenu.TabIndex = 28
        Me.EQMenu.Text = "Main Menu"
        '
        'cmdClose
        '
        Me.cmdClose.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdClose.ForeColor = System.Drawing.Color.White
        Me.cmdClose.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.cmdClose.Margin = New System.Windows.Forms.Padding(0, 1, 2, 2)
        Me.cmdClose.Name = "cmdClose"
        Me.cmdClose.Size = New System.Drawing.Size(43, 42)
        Me.cmdClose.Text = "&Close"
        Me.cmdClose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.cmdClose.ToolTipText = " Close this Form "
        '
        'GridColumn1
        '
        Me.GridColumn1.Caption = "Kronis"
        Me.GridColumn1.ColumnEdit = Me.RepositoryItemCheckEdit1
        Me.GridColumn1.FieldName = "ISKRONIS"
        Me.GridColumn1.Name = "GridColumn1"
        Me.GridColumn1.Visible = True
        Me.GridColumn1.VisibleIndex = 1
        '
        'RepositoryItemCheckEdit1
        '
        Me.RepositoryItemCheckEdit1.AutoHeight = False
        Me.RepositoryItemCheckEdit1.Name = "RepositoryItemCheckEdit1"
        Me.RepositoryItemCheckEdit1.NullStyle = DevExpress.XtraEditors.Controls.StyleIndeterminate.Unchecked
        '
        'frmBrowseItemPOS
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(494, 572)
        Me.Controls.Add(Me.grd)
        Me.Controls.Add(Me.Bawah)
        Me.Controls.Add(Me.EQMenu)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmBrowseItemPOS"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Browse Item"
        CType(Me.grd, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grv, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ckISOBATBPJS, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Bawah, System.ComponentModel.ISupportInitialize).EndInit()
        Me.EQMenu.ResumeLayout(False)
        Me.EQMenu.PerformLayout()
        CType(Me.RepositoryItemCheckEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents grd As DevExpress.XtraGrid.GridControl
    Friend WithEvents grv As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents colNMITEM2 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents Bawah As DevExpress.XtraEditors.PanelControl
    Friend WithEvents cmdHelp As System.Windows.Forms.ToolStripButton
    Friend WithEvents cmdSelect As System.Windows.Forms.ToolStripButton
    Friend WithEvents EQMenu As System.Windows.Forms.ToolStrip
    Friend WithEvents cmdClose As System.Windows.Forms.ToolStripButton
    Friend WithEvents ckISOBATBPJS As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents colKDUOM As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colKDITEM As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn1 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents RepositoryItemCheckEdit1 As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
End Class
