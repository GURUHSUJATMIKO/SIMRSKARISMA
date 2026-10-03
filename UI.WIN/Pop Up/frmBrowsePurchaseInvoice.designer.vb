<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmBrowsePurchaseInvoice
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmBrowsePurchaseInvoice))
        Me.grd = New DevExpress.XtraGrid.GridControl()
        Me.grv = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.colKDPI = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colDATE = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colVENDOR = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colHarga = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepositoryItemDateEdit1 = New DevExpress.XtraEditors.Repository.RepositoryItemDateEdit()
        Me.Bawah = New DevExpress.XtraEditors.PanelControl()
        Me.cmdHelp = New System.Windows.Forms.ToolStripButton()
        Me.cmdSelect = New System.Windows.Forms.ToolStripButton()
        Me.EQMenu = New System.Windows.Forms.ToolStrip()
        Me.cmdClose = New System.Windows.Forms.ToolStripButton()
        Me.colNOFAKTUR = New DevExpress.XtraGrid.Columns.GridColumn()
        CType(Me.grd,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.grv,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.RepositoryItemDateEdit1,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.RepositoryItemDateEdit1.CalendarTimeProperties,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.Bawah,System.ComponentModel.ISupportInitialize).BeginInit
        Me.EQMenu.SuspendLayout
        Me.SuspendLayout
        '
        'grd
        '
        Me.grd.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grd.EmbeddedNavigator.Buttons.Append.Enabled = false
        Me.grd.EmbeddedNavigator.Buttons.Append.Visible = false
        Me.grd.EmbeddedNavigator.Buttons.CancelEdit.Enabled = false
        Me.grd.EmbeddedNavigator.Buttons.CancelEdit.Visible = false
        Me.grd.EmbeddedNavigator.Buttons.Edit.Enabled = false
        Me.grd.EmbeddedNavigator.Buttons.Edit.Visible = false
        Me.grd.EmbeddedNavigator.Buttons.EndEdit.Visible = false
        Me.grd.EmbeddedNavigator.Buttons.Remove.Visible = false
        Me.grd.Location = New System.Drawing.Point(0, 45)
        Me.grd.MainView = Me.grv
        Me.grd.Name = "grd"
        Me.grd.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.RepositoryItemDateEdit1})
        Me.grd.Size = New System.Drawing.Size(618, 502)
        Me.grd.TabIndex = 0
        Me.grd.UseEmbeddedNavigator = true
        Me.grd.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.grv})
        '
        'grv
        '
        Me.grv.Appearance.ColumnFilterButton.BackColor = System.Drawing.Color.Silver
        Me.grv.Appearance.ColumnFilterButton.BackColor2 = System.Drawing.Color.FromArgb(CType(CType(212,Byte),Integer), CType(CType(212,Byte),Integer), CType(CType(212,Byte),Integer))
        Me.grv.Appearance.ColumnFilterButton.BorderColor = System.Drawing.Color.Silver
        Me.grv.Appearance.ColumnFilterButton.ForeColor = System.Drawing.Color.Gray
        Me.grv.Appearance.ColumnFilterButton.Options.UseBackColor = true
        Me.grv.Appearance.ColumnFilterButton.Options.UseBorderColor = true
        Me.grv.Appearance.ColumnFilterButton.Options.UseForeColor = true
        Me.grv.Appearance.ColumnFilterButtonActive.BackColor = System.Drawing.Color.FromArgb(CType(CType(212,Byte),Integer), CType(CType(212,Byte),Integer), CType(CType(212,Byte),Integer))
        Me.grv.Appearance.ColumnFilterButtonActive.BackColor2 = System.Drawing.Color.FromArgb(CType(CType(223,Byte),Integer), CType(CType(223,Byte),Integer), CType(CType(223,Byte),Integer))
        Me.grv.Appearance.ColumnFilterButtonActive.BorderColor = System.Drawing.Color.FromArgb(CType(CType(212,Byte),Integer), CType(CType(212,Byte),Integer), CType(CType(212,Byte),Integer))
        Me.grv.Appearance.ColumnFilterButtonActive.ForeColor = System.Drawing.Color.Blue
        Me.grv.Appearance.ColumnFilterButtonActive.Options.UseBackColor = true
        Me.grv.Appearance.ColumnFilterButtonActive.Options.UseBorderColor = true
        Me.grv.Appearance.ColumnFilterButtonActive.Options.UseForeColor = true
        Me.grv.Appearance.Empty.BackColor = System.Drawing.Color.FromArgb(CType(CType(243,Byte),Integer), CType(CType(243,Byte),Integer), CType(CType(243,Byte),Integer))
        Me.grv.Appearance.Empty.Options.UseBackColor = true
        Me.grv.Appearance.EvenRow.BackColor = System.Drawing.Color.FromArgb(CType(CType(223,Byte),Integer), CType(CType(223,Byte),Integer), CType(CType(223,Byte),Integer))
        Me.grv.Appearance.EvenRow.BackColor2 = System.Drawing.Color.GhostWhite
        Me.grv.Appearance.EvenRow.Font = New System.Drawing.Font("Tahoma", 10!)
        Me.grv.Appearance.EvenRow.ForeColor = System.Drawing.Color.Black
        Me.grv.Appearance.EvenRow.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.ForwardDiagonal
        Me.grv.Appearance.EvenRow.Options.UseBackColor = true
        Me.grv.Appearance.EvenRow.Options.UseFont = true
        Me.grv.Appearance.EvenRow.Options.UseForeColor = true
        Me.grv.Appearance.FilterCloseButton.BackColor = System.Drawing.Color.FromArgb(CType(CType(212,Byte),Integer), CType(CType(208,Byte),Integer), CType(CType(200,Byte),Integer))
        Me.grv.Appearance.FilterCloseButton.BackColor2 = System.Drawing.Color.FromArgb(CType(CType(118,Byte),Integer), CType(CType(170,Byte),Integer), CType(CType(225,Byte),Integer))
        Me.grv.Appearance.FilterCloseButton.BorderColor = System.Drawing.Color.FromArgb(CType(CType(212,Byte),Integer), CType(CType(208,Byte),Integer), CType(CType(200,Byte),Integer))
        Me.grv.Appearance.FilterCloseButton.ForeColor = System.Drawing.Color.Black
        Me.grv.Appearance.FilterCloseButton.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.ForwardDiagonal
        Me.grv.Appearance.FilterCloseButton.Options.UseBackColor = true
        Me.grv.Appearance.FilterCloseButton.Options.UseBorderColor = true
        Me.grv.Appearance.FilterCloseButton.Options.UseForeColor = true
        Me.grv.Appearance.FilterPanel.BackColor = System.Drawing.Color.FromArgb(CType(CType(28,Byte),Integer), CType(CType(80,Byte),Integer), CType(CType(135,Byte),Integer))
        Me.grv.Appearance.FilterPanel.BackColor2 = System.Drawing.Color.FromArgb(CType(CType(212,Byte),Integer), CType(CType(208,Byte),Integer), CType(CType(200,Byte),Integer))
        Me.grv.Appearance.FilterPanel.ForeColor = System.Drawing.Color.White
        Me.grv.Appearance.FilterPanel.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.ForwardDiagonal
        Me.grv.Appearance.FilterPanel.Options.UseBackColor = true
        Me.grv.Appearance.FilterPanel.Options.UseForeColor = true
        Me.grv.Appearance.FixedLine.BackColor = System.Drawing.Color.FromArgb(CType(CType(58,Byte),Integer), CType(CType(58,Byte),Integer), CType(CType(58,Byte),Integer))
        Me.grv.Appearance.FixedLine.Options.UseBackColor = true
        Me.grv.Appearance.FocusedCell.BackColor = System.Drawing.Color.FromArgb(CType(CType(255,Byte),Integer), CType(CType(255,Byte),Integer), CType(CType(225,Byte),Integer))
        Me.grv.Appearance.FocusedCell.Font = New System.Drawing.Font("Tahoma", 10!)
        Me.grv.Appearance.FocusedCell.ForeColor = System.Drawing.Color.Black
        Me.grv.Appearance.FocusedCell.Options.UseBackColor = true
        Me.grv.Appearance.FocusedCell.Options.UseFont = true
        Me.grv.Appearance.FocusedCell.Options.UseForeColor = true
        Me.grv.Appearance.FocusedRow.BackColor = System.Drawing.Color.Navy
        Me.grv.Appearance.FocusedRow.BackColor2 = System.Drawing.Color.FromArgb(CType(CType(50,Byte),Integer), CType(CType(50,Byte),Integer), CType(CType(178,Byte),Integer))
        Me.grv.Appearance.FocusedRow.Font = New System.Drawing.Font("Tahoma", 10!)
        Me.grv.Appearance.FocusedRow.ForeColor = System.Drawing.Color.White
        Me.grv.Appearance.FocusedRow.Options.UseBackColor = true
        Me.grv.Appearance.FocusedRow.Options.UseFont = true
        Me.grv.Appearance.FocusedRow.Options.UseForeColor = true
        Me.grv.Appearance.FooterPanel.BackColor = System.Drawing.Color.Silver
        Me.grv.Appearance.FooterPanel.BorderColor = System.Drawing.Color.Silver
        Me.grv.Appearance.FooterPanel.Font = New System.Drawing.Font("Tahoma", 10!)
        Me.grv.Appearance.FooterPanel.ForeColor = System.Drawing.Color.Black
        Me.grv.Appearance.FooterPanel.Options.UseBackColor = true
        Me.grv.Appearance.FooterPanel.Options.UseBorderColor = true
        Me.grv.Appearance.FooterPanel.Options.UseFont = true
        Me.grv.Appearance.FooterPanel.Options.UseForeColor = true
        Me.grv.Appearance.GroupButton.BackColor = System.Drawing.Color.Silver
        Me.grv.Appearance.GroupButton.BorderColor = System.Drawing.Color.Silver
        Me.grv.Appearance.GroupButton.ForeColor = System.Drawing.Color.Black
        Me.grv.Appearance.GroupButton.Options.UseBackColor = true
        Me.grv.Appearance.GroupButton.Options.UseBorderColor = true
        Me.grv.Appearance.GroupButton.Options.UseForeColor = true
        Me.grv.Appearance.GroupFooter.BackColor = System.Drawing.Color.FromArgb(CType(CType(202,Byte),Integer), CType(CType(202,Byte),Integer), CType(CType(202,Byte),Integer))
        Me.grv.Appearance.GroupFooter.BorderColor = System.Drawing.Color.FromArgb(CType(CType(202,Byte),Integer), CType(CType(202,Byte),Integer), CType(CType(202,Byte),Integer))
        Me.grv.Appearance.GroupFooter.ForeColor = System.Drawing.Color.Black
        Me.grv.Appearance.GroupFooter.Options.UseBackColor = true
        Me.grv.Appearance.GroupFooter.Options.UseBorderColor = true
        Me.grv.Appearance.GroupFooter.Options.UseForeColor = true
        Me.grv.Appearance.GroupPanel.BackColor = System.Drawing.Color.FromArgb(CType(CType(58,Byte),Integer), CType(CType(110,Byte),Integer), CType(CType(165,Byte),Integer))
        Me.grv.Appearance.GroupPanel.BackColor2 = System.Drawing.Color.White
        Me.grv.Appearance.GroupPanel.Font = New System.Drawing.Font("Tahoma", 10!, System.Drawing.FontStyle.Bold)
        Me.grv.Appearance.GroupPanel.ForeColor = System.Drawing.Color.White
        Me.grv.Appearance.GroupPanel.Options.UseBackColor = true
        Me.grv.Appearance.GroupPanel.Options.UseFont = true
        Me.grv.Appearance.GroupPanel.Options.UseForeColor = true
        Me.grv.Appearance.GroupRow.BackColor = System.Drawing.Color.Gray
        Me.grv.Appearance.GroupRow.Font = New System.Drawing.Font("Tahoma", 10!)
        Me.grv.Appearance.GroupRow.ForeColor = System.Drawing.Color.Silver
        Me.grv.Appearance.GroupRow.Options.UseBackColor = true
        Me.grv.Appearance.GroupRow.Options.UseFont = true
        Me.grv.Appearance.GroupRow.Options.UseForeColor = true
        Me.grv.Appearance.HeaderPanel.BackColor = System.Drawing.Color.Silver
        Me.grv.Appearance.HeaderPanel.BorderColor = System.Drawing.Color.Silver
        Me.grv.Appearance.HeaderPanel.Font = New System.Drawing.Font("Tahoma", 10!, System.Drawing.FontStyle.Bold)
        Me.grv.Appearance.HeaderPanel.ForeColor = System.Drawing.Color.Black
        Me.grv.Appearance.HeaderPanel.Options.UseBackColor = true
        Me.grv.Appearance.HeaderPanel.Options.UseBorderColor = true
        Me.grv.Appearance.HeaderPanel.Options.UseFont = true
        Me.grv.Appearance.HeaderPanel.Options.UseForeColor = true
        Me.grv.Appearance.HideSelectionRow.BackColor = System.Drawing.Color.Gray
        Me.grv.Appearance.HideSelectionRow.Font = New System.Drawing.Font("Tahoma", 10!)
        Me.grv.Appearance.HideSelectionRow.ForeColor = System.Drawing.Color.FromArgb(CType(CType(212,Byte),Integer), CType(CType(208,Byte),Integer), CType(CType(200,Byte),Integer))
        Me.grv.Appearance.HideSelectionRow.Options.UseBackColor = true
        Me.grv.Appearance.HideSelectionRow.Options.UseFont = true
        Me.grv.Appearance.HideSelectionRow.Options.UseForeColor = true
        Me.grv.Appearance.HorzLine.BackColor = System.Drawing.Color.Silver
        Me.grv.Appearance.HorzLine.Options.UseBackColor = true
        Me.grv.Appearance.OddRow.BackColor = System.Drawing.Color.White
        Me.grv.Appearance.OddRow.BackColor2 = System.Drawing.Color.White
        Me.grv.Appearance.OddRow.Font = New System.Drawing.Font("Tahoma", 10!)
        Me.grv.Appearance.OddRow.ForeColor = System.Drawing.Color.Black
        Me.grv.Appearance.OddRow.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.BackwardDiagonal
        Me.grv.Appearance.OddRow.Options.UseBackColor = true
        Me.grv.Appearance.OddRow.Options.UseFont = true
        Me.grv.Appearance.OddRow.Options.UseForeColor = true
        Me.grv.Appearance.Preview.BackColor = System.Drawing.Color.White
        Me.grv.Appearance.Preview.Font = New System.Drawing.Font("Tahoma", 10!)
        Me.grv.Appearance.Preview.ForeColor = System.Drawing.Color.Navy
        Me.grv.Appearance.Preview.Options.UseBackColor = true
        Me.grv.Appearance.Preview.Options.UseFont = true
        Me.grv.Appearance.Preview.Options.UseForeColor = true
        Me.grv.Appearance.Row.BackColor = System.Drawing.Color.White
        Me.grv.Appearance.Row.Font = New System.Drawing.Font("Tahoma", 10!)
        Me.grv.Appearance.Row.ForeColor = System.Drawing.Color.Black
        Me.grv.Appearance.Row.Options.UseBackColor = true
        Me.grv.Appearance.Row.Options.UseFont = true
        Me.grv.Appearance.Row.Options.UseForeColor = true
        Me.grv.Appearance.RowSeparator.BackColor = System.Drawing.Color.White
        Me.grv.Appearance.RowSeparator.BackColor2 = System.Drawing.Color.FromArgb(CType(CType(243,Byte),Integer), CType(CType(243,Byte),Integer), CType(CType(243,Byte),Integer))
        Me.grv.Appearance.RowSeparator.Options.UseBackColor = true
        Me.grv.Appearance.SelectedRow.BackColor = System.Drawing.Color.FromArgb(CType(CType(10,Byte),Integer), CType(CType(10,Byte),Integer), CType(CType(138,Byte),Integer))
        Me.grv.Appearance.SelectedRow.Font = New System.Drawing.Font("Tahoma", 10!)
        Me.grv.Appearance.SelectedRow.ForeColor = System.Drawing.Color.White
        Me.grv.Appearance.SelectedRow.Options.UseBackColor = true
        Me.grv.Appearance.SelectedRow.Options.UseFont = true
        Me.grv.Appearance.SelectedRow.Options.UseForeColor = true
        Me.grv.Appearance.TopNewRow.Font = New System.Drawing.Font("Tahoma", 10!)
        Me.grv.Appearance.TopNewRow.Options.UseFont = true
        Me.grv.Appearance.VertLine.BackColor = System.Drawing.Color.Silver
        Me.grv.Appearance.VertLine.Options.UseBackColor = true
        Me.grv.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.colKDPI, Me.colDATE, Me.colNOFAKTUR, Me.colVENDOR, Me.colHarga})
        Me.grv.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.grv.GridControl = Me.grd
        Me.grv.Name = "grv"
        Me.grv.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.[False]
        Me.grv.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.[False]
        Me.grv.OptionsBehavior.AllowIncrementalSearch = true
        Me.grv.OptionsBehavior.AutoExpandAllGroups = true
        Me.grv.OptionsBehavior.Editable = false
        Me.grv.OptionsDetail.EnableMasterViewMode = false
        Me.grv.OptionsSelection.EnableAppearanceFocusedCell = false
        Me.grv.OptionsView.EnableAppearanceEvenRow = true
        Me.grv.OptionsView.EnableAppearanceOddRow = true
        Me.grv.OptionsView.ShowAutoFilterRow = true
        Me.grv.OptionsView.ShowChildrenInGroupPanel = true
        Me.grv.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.ShowAlways
        '
        'colKDPI
        '
        Me.colKDPI.AppearanceCell.Font = New System.Drawing.Font("Tahoma", 9!)
        Me.colKDPI.AppearanceCell.Options.UseFont = true
        Me.colKDPI.AppearanceHeader.Font = New System.Drawing.Font("Tahoma", 9!)
        Me.colKDPI.AppearanceHeader.Options.UseFont = true
        Me.colKDPI.AppearanceHeader.Options.UseTextOptions = true
        Me.colKDPI.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.colKDPI.Caption = "No Pembelian"
        Me.colKDPI.FieldName = "NoPembelian"
        Me.colKDPI.Name = "colKDPI"
        Me.colKDPI.Visible = true
        Me.colKDPI.VisibleIndex = 0
        Me.colKDPI.Width = 166
        '
        'colDATE
        '
        Me.colDATE.AppearanceCell.Font = New System.Drawing.Font("Tahoma", 9!)
        Me.colDATE.AppearanceCell.Options.UseFont = true
        Me.colDATE.AppearanceHeader.Font = New System.Drawing.Font("Tahoma", 9!)
        Me.colDATE.AppearanceHeader.Options.UseFont = true
        Me.colDATE.AppearanceHeader.Options.UseTextOptions = true
        Me.colDATE.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.colDATE.Caption = "Tanggal"
        Me.colDATE.FieldName = "Tanggal"
        Me.colDATE.Name = "colDATE"
        Me.colDATE.Visible = true
        Me.colDATE.VisibleIndex = 1
        Me.colDATE.Width = 171
        '
        'colVENDOR
        '
        Me.colVENDOR.AppearanceCell.Font = New System.Drawing.Font("Tahoma", 9!)
        Me.colVENDOR.AppearanceCell.Options.UseFont = true
        Me.colVENDOR.AppearanceHeader.Font = New System.Drawing.Font("Tahoma", 9!)
        Me.colVENDOR.AppearanceHeader.Options.UseFont = true
        Me.colVENDOR.AppearanceHeader.Options.UseTextOptions = true
        Me.colVENDOR.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.colVENDOR.Caption = "PBF"
        Me.colVENDOR.FieldName = "KDVENDOR"
        Me.colVENDOR.Name = "colVENDOR"
        Me.colVENDOR.Visible = true
        Me.colVENDOR.VisibleIndex = 3
        Me.colVENDOR.Width = 364
        '
        'colHarga
        '
        Me.colHarga.AppearanceCell.Font = New System.Drawing.Font("Tahoma", 9!)
        Me.colHarga.AppearanceCell.Options.UseFont = true
        Me.colHarga.AppearanceCell.Options.UseTextOptions = true
        Me.colHarga.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.colHarga.AppearanceHeader.Font = New System.Drawing.Font("Tahoma", 9!)
        Me.colHarga.AppearanceHeader.Options.UseFont = true
        Me.colHarga.AppearanceHeader.Options.UseTextOptions = true
        Me.colHarga.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.colHarga.Caption = "Harga"
        Me.colHarga.FieldName = "PRICE"
        Me.colHarga.Name = "colHarga"
        Me.colHarga.Visible = true
        Me.colHarga.VisibleIndex = 4
        Me.colHarga.Width = 187
        '
        'RepositoryItemDateEdit1
        '
        Me.RepositoryItemDateEdit1.AutoHeight = false
        Me.RepositoryItemDateEdit1.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepositoryItemDateEdit1.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepositoryItemDateEdit1.Mask.EditMask = "dd/MM/yyyy"
        Me.RepositoryItemDateEdit1.Mask.UseMaskAsDisplayFormat = true
        Me.RepositoryItemDateEdit1.Name = "RepositoryItemDateEdit1"
        '
        'Bawah
        '
        Me.Bawah.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Bawah.Location = New System.Drawing.Point(0, 547)
        Me.Bawah.Margin = New System.Windows.Forms.Padding(4)
        Me.Bawah.Name = "Bawah"
        Me.Bawah.Size = New System.Drawing.Size(618, 25)
        Me.Bawah.TabIndex = 29
        '
        'cmdHelp
        '
        Me.cmdHelp.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right
        Me.cmdHelp.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
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
        Me.cmdSelect.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.cmdSelect.ForeColor = System.Drawing.Color.White
        Me.cmdSelect.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.cmdSelect.Margin = New System.Windows.Forms.Padding(0, 1, 2, 2)
        Me.cmdSelect.Name = "cmdSelect"
        Me.cmdSelect.Size = New System.Drawing.Size(48, 42)
        Me.cmdSelect.Text = "&Select"
        Me.cmdSelect.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.cmdSelect.ToolTipText = " Print PaymentType List "
        '
        'EQMenu
        '
        Me.EQMenu.BackgroundImage = CType(resources.GetObject("EQMenu.BackgroundImage"),System.Drawing.Image)
        Me.EQMenu.Font = New System.Drawing.Font("Tahoma", 9.75!)
        Me.EQMenu.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.cmdSelect, Me.cmdClose, Me.cmdHelp})
        Me.EQMenu.Location = New System.Drawing.Point(0, 0)
        Me.EQMenu.Margin = New System.Windows.Forms.Padding(2)
        Me.EQMenu.MinimumSize = New System.Drawing.Size(0, 45)
        Me.EQMenu.Name = "EQMenu"
        Me.EQMenu.Size = New System.Drawing.Size(618, 45)
        Me.EQMenu.Stretch = true
        Me.EQMenu.TabIndex = 28
        Me.EQMenu.Text = "Main Menu"
        '
        'cmdClose
        '
        Me.cmdClose.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.cmdClose.ForeColor = System.Drawing.Color.White
        Me.cmdClose.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.cmdClose.Margin = New System.Windows.Forms.Padding(0, 1, 2, 2)
        Me.cmdClose.Name = "cmdClose"
        Me.cmdClose.Size = New System.Drawing.Size(43, 42)
        Me.cmdClose.Text = "&Close"
        Me.cmdClose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.cmdClose.ToolTipText = " Close this Form "
        '
        'colNOFAKTUR
        '
        Me.colNOFAKTUR.AppearanceCell.Font = New System.Drawing.Font("Tahoma", 9!)
        Me.colNOFAKTUR.AppearanceCell.Options.UseFont = true
        Me.colNOFAKTUR.AppearanceHeader.Font = New System.Drawing.Font("Tahoma", 9!)
        Me.colNOFAKTUR.AppearanceHeader.Options.UseFont = true
        Me.colNOFAKTUR.AppearanceHeader.Options.UseTextOptions = true
        Me.colNOFAKTUR.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.colNOFAKTUR.Caption = "No Faktur"
        Me.colNOFAKTUR.FieldName = "NoFaktur"
        Me.colNOFAKTUR.Name = "colNOFAKTUR"
        Me.colNOFAKTUR.Visible = true
        Me.colNOFAKTUR.VisibleIndex = 2
        Me.colNOFAKTUR.Width = 190
        '
        'frmBrowsePurchaseInvoice
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6!, 13!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(618, 572)
        Me.Controls.Add(Me.grd)
        Me.Controls.Add(Me.Bawah)
        Me.Controls.Add(Me.EQMenu)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.KeyPreview = true
        Me.Name = "frmBrowsePurchaseInvoice"
        Me.ShowIcon = false
        Me.ShowInTaskbar = false
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Browse Pembelian"
        CType(Me.grd,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.grv,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.RepositoryItemDateEdit1.CalendarTimeProperties,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.RepositoryItemDateEdit1,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.Bawah,System.ComponentModel.ISupportInitialize).EndInit
        Me.EQMenu.ResumeLayout(false)
        Me.EQMenu.PerformLayout
        Me.ResumeLayout(false)
        Me.PerformLayout

End Sub
    Friend WithEvents grd As DevExpress.XtraGrid.GridControl
    Friend WithEvents grv As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents colDATE As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents Bawah As DevExpress.XtraEditors.PanelControl
    Friend WithEvents cmdHelp As System.Windows.Forms.ToolStripButton
    Friend WithEvents cmdSelect As System.Windows.Forms.ToolStripButton
    Friend WithEvents EQMenu As System.Windows.Forms.ToolStrip
    Friend WithEvents cmdClose As System.Windows.Forms.ToolStripButton
    Friend WithEvents RepositoryItemDateEdit1 As DevExpress.XtraEditors.Repository.RepositoryItemDateEdit
    Friend WithEvents colVENDOR As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colHarga As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colKDPI As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colNOFAKTUR As DevExpress.XtraGrid.Columns.GridColumn
End Class
