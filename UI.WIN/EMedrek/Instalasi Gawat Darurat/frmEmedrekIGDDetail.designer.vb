<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmEmedrekIGDDetail
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
        Me.layoutControl = New DevExpress.XtraLayout.LayoutControl()
        Me.tabControl = New DevExpress.XtraTab.XtraTabControl()
        Me.tab1 = New DevExpress.XtraTab.XtraTabPage()
        Me.grdDetail = New DevExpress.XtraGrid.GridControl()
        Me.BindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.grvDetail = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.colPUKUL = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.dePUKUL = New DevExpress.XtraEditors.Repository.RepositoryItemDateEdit()
        Me.colPENANGANAN = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepositoryItemMemoEdit1 = New DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit()
        Me.colNAMA = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.grdUSER = New DevExpress.XtraEditors.Repository.RepositoryItemSearchLookUpEdit()
        Me.RepositoryItemSearchLookUpEdit1View = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn1 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colPARAF = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.barManager = New DevExpress.XtraBars.BarManager(Me.components)
        Me.barTop = New DevExpress.XtraBars.Bar()
        Me.btnSaveClose = New DevExpress.XtraBars.BarButtonItem()
        Me.btnClose = New DevExpress.XtraBars.BarButtonItem()
        Me.barDockControlTop = New DevExpress.XtraBars.BarDockControl()
        Me.barDockControlBottom = New DevExpress.XtraBars.BarDockControl()
        Me.barDockControlLeft = New DevExpress.XtraBars.BarDockControl()
        Me.barDockControlRight = New DevExpress.XtraBars.BarDockControl()
        Me.btnSaveNew = New DevExpress.XtraBars.BarButtonItem()
        Me.progressBarSave = New DevExpress.XtraEditors.Repository.RepositoryItemMarqueeProgressBar()
        Me.progressSave = New DevExpress.XtraEditors.Repository.RepositoryItemMarqueeProgressBar()
        Me.mePenanganandanPenilaian = New DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit()
        Me.LayoutControlGroup1 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.LayoutControlItem5 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.mnuStrip = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.DeleteToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.GridColumn6 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn3 = New DevExpress.XtraGrid.Columns.GridColumn()
        CType(Me.layoutControl, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.layoutControl.SuspendLayout()
        CType(Me.tabControl, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tabControl.SuspendLayout()
        Me.tab1.SuspendLayout()
        CType(Me.grdDetail, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.BindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grvDetail, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dePUKUL, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dePUKUL.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemMemoEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdUSER, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemSearchLookUpEdit1View, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.barManager, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.progressBarSave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.progressSave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.mePenanganandanPenilaian, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem5, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.mnuStrip.SuspendLayout()
        Me.SuspendLayout()
        '
        'layoutControl
        '
        Me.layoutControl.Controls.Add(Me.tabControl)
        Me.layoutControl.Dock = System.Windows.Forms.DockStyle.Fill
        Me.layoutControl.Location = New System.Drawing.Point(0, 0)
        Me.layoutControl.Name = "layoutControl"
        Me.layoutControl.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = New System.Drawing.Rectangle(652, 156, 250, 350)
        Me.layoutControl.Root = Me.LayoutControlGroup1
        Me.layoutControl.Size = New System.Drawing.Size(790, 549)
        Me.layoutControl.TabIndex = 0
        Me.layoutControl.Text = "LayoutControl1"
        '
        'tabControl
        '
        Me.tabControl.Location = New System.Drawing.Point(12, 12)
        Me.tabControl.Name = "tabControl"
        Me.tabControl.SelectedTabPage = Me.tab1
        Me.tabControl.Size = New System.Drawing.Size(766, 525)
        Me.tabControl.TabIndex = 18
        Me.tabControl.TabPages.AddRange(New DevExpress.XtraTab.XtraTabPage() {Me.tab1})
        '
        'tab1
        '
        Me.tab1.Controls.Add(Me.grdDetail)
        Me.tab1.Name = "tab1"
        Me.tab1.Size = New System.Drawing.Size(760, 497)
        Me.tab1.Text = "Detail Information"
        '
        'grdDetail
        '
        Me.grdDetail.DataSource = Me.BindingSource
        Me.grdDetail.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grdDetail.Location = New System.Drawing.Point(0, 0)
        Me.grdDetail.MainView = Me.grvDetail
        Me.grdDetail.MenuManager = Me.barManager
        Me.grdDetail.Name = "grdDetail"
        Me.grdDetail.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.dePUKUL, Me.mePenanganandanPenilaian, Me.RepositoryItemMemoEdit1, Me.grdUSER})
        Me.grdDetail.Size = New System.Drawing.Size(760, 497)
        Me.grdDetail.TabIndex = 20
        Me.grdDetail.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.grvDetail})
        '
        'BindingSource
        '
        Me.BindingSource.DataSource = GetType(DataAccess.S_DIGITAL_IGD_01_DETIL)
        '
        'grvDetail
        '
        Me.grvDetail.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.colPUKUL, Me.colPENANGANAN, Me.colNAMA, Me.colPARAF})
        Me.grvDetail.GridControl = Me.grdDetail
        Me.grvDetail.Name = "grvDetail"
        Me.grvDetail.OptionsDetail.EnableMasterViewMode = False
        Me.grvDetail.OptionsNavigation.AutoFocusNewRow = True
        Me.grvDetail.OptionsNavigation.EnterMoveNextColumn = True
        Me.grvDetail.OptionsView.EnableAppearanceEvenRow = True
        Me.grvDetail.OptionsView.EnableAppearanceOddRow = True
        Me.grvDetail.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Bottom
        Me.grvDetail.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never
        Me.grvDetail.OptionsView.ShowFooter = True
        Me.grvDetail.OptionsView.ShowGroupPanel = False
        '
        'colPUKUL
        '
        Me.colPUKUL.Caption = "PUKUL"
        Me.colPUKUL.ColumnEdit = Me.dePUKUL
        Me.colPUKUL.DisplayFormat.FormatString = "dd-MM-yyyy HH:mm:ss"
        Me.colPUKUL.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.colPUKUL.FieldName = "PUKUL"
        Me.colPUKUL.Name = "colPUKUL"
        Me.colPUKUL.Visible = True
        Me.colPUKUL.VisibleIndex = 0
        Me.colPUKUL.Width = 93
        '
        'dePUKUL
        '
        Me.dePUKUL.AutoHeight = False
        Me.dePUKUL.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.dePUKUL.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
        Me.dePUKUL.Mask.EditMask = "dd-MM-yyyy HH:mm:ss"
        Me.dePUKUL.Mask.UseMaskAsDisplayFormat = True
        Me.dePUKUL.Name = "dePUKUL"
        '
        'colPENANGANAN
        '
        Me.colPENANGANAN.Caption = "PENANGANAN DAN PENILAIAN ULANG"
        Me.colPENANGANAN.ColumnEdit = Me.RepositoryItemMemoEdit1
        Me.colPENANGANAN.FieldName = "PENANGANAN"
        Me.colPENANGANAN.Name = "colPENANGANAN"
        Me.colPENANGANAN.OptionsColumn.AllowEdit = False
        Me.colPENANGANAN.OptionsColumn.AllowFocus = False
        Me.colPENANGANAN.OptionsColumn.ReadOnly = True
        Me.colPENANGANAN.OptionsColumn.TabStop = False
        Me.colPENANGANAN.Visible = True
        Me.colPENANGANAN.VisibleIndex = 1
        Me.colPENANGANAN.Width = 557
        '
        'RepositoryItemMemoEdit1
        '
        Me.RepositoryItemMemoEdit1.Name = "RepositoryItemMemoEdit1"
        '
        'colNAMA
        '
        Me.colNAMA.Caption = "NAMA"
        Me.colNAMA.ColumnEdit = Me.grdUSER
        Me.colNAMA.FieldName = "NAMA"
        Me.colNAMA.Name = "colNAMA"
        Me.colNAMA.Visible = True
        Me.colNAMA.VisibleIndex = 2
        Me.colNAMA.Width = 193
        '
        'grdUSER
        '
        Me.grdUSER.AutoHeight = False
        Me.grdUSER.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.grdUSER.Name = "grdUSER"
        Me.grdUSER.NullText = ""
        Me.grdUSER.View = Me.RepositoryItemSearchLookUpEdit1View
        '
        'RepositoryItemSearchLookUpEdit1View
        '
        Me.RepositoryItemSearchLookUpEdit1View.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn1})
        Me.RepositoryItemSearchLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.RepositoryItemSearchLookUpEdit1View.Name = "RepositoryItemSearchLookUpEdit1View"
        Me.RepositoryItemSearchLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.RepositoryItemSearchLookUpEdit1View.OptionsView.ShowGroupPanel = False
        '
        'GridColumn1
        '
        Me.GridColumn1.Caption = "User"
        Me.GridColumn1.FieldName = "KDUSER"
        Me.GridColumn1.Name = "GridColumn1"
        Me.GridColumn1.Visible = True
        Me.GridColumn1.VisibleIndex = 0
        '
        'colPARAF
        '
        Me.colPARAF.Caption = "Paraf"
        Me.colPARAF.FieldName = "PARAF"
        Me.colPARAF.Name = "colPARAF"
        Me.colPARAF.Width = 73
        '
        'barManager
        '
        Me.barManager.AllowQuickCustomization = False
        Me.barManager.Bars.AddRange(New DevExpress.XtraBars.Bar() {Me.barTop})
        Me.barManager.DockControls.Add(Me.barDockControlTop)
        Me.barManager.DockControls.Add(Me.barDockControlBottom)
        Me.barManager.DockControls.Add(Me.barDockControlLeft)
        Me.barManager.DockControls.Add(Me.barDockControlRight)
        Me.barManager.Form = Me
        Me.barManager.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.btnSaveNew, Me.btnClose, Me.btnSaveClose})
        Me.barManager.MainMenu = Me.barTop
        Me.barManager.MaxItemId = 8
        Me.barManager.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.progressBarSave, Me.progressSave})
        '
        'barTop
        '
        Me.barTop.BarName = "Main menu"
        Me.barTop.DockCol = 0
        Me.barTop.DockRow = 0
        Me.barTop.DockStyle = DevExpress.XtraBars.BarDockStyle.Bottom
        Me.barTop.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.btnSaveClose), New DevExpress.XtraBars.LinkPersistInfo(Me.btnClose)})
        Me.barTop.OptionsBar.DrawDragBorder = False
        Me.barTop.OptionsBar.MultiLine = True
        Me.barTop.OptionsBar.UseWholeRow = True
        Me.barTop.Text = "Main menu"
        '
        'btnSaveClose
        '
        Me.btnSaveClose.Caption = "F3 - Save && Close"
        Me.btnSaveClose.Id = 5
        Me.btnSaveClose.Name = "btnSaveClose"
        '
        'btnClose
        '
        Me.btnClose.Caption = "F12 - Close"
        Me.btnClose.Id = 3
        Me.btnClose.Name = "btnClose"
        '
        'barDockControlTop
        '
        Me.barDockControlTop.CausesValidation = False
        Me.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top
        Me.barDockControlTop.Location = New System.Drawing.Point(0, 0)
        Me.barDockControlTop.Size = New System.Drawing.Size(790, 0)
        '
        'barDockControlBottom
        '
        Me.barDockControlBottom.CausesValidation = False
        Me.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.barDockControlBottom.Location = New System.Drawing.Point(0, 549)
        Me.barDockControlBottom.Size = New System.Drawing.Size(790, 22)
        '
        'barDockControlLeft
        '
        Me.barDockControlLeft.CausesValidation = False
        Me.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left
        Me.barDockControlLeft.Location = New System.Drawing.Point(0, 0)
        Me.barDockControlLeft.Size = New System.Drawing.Size(0, 549)
        '
        'barDockControlRight
        '
        Me.barDockControlRight.CausesValidation = False
        Me.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right
        Me.barDockControlRight.Location = New System.Drawing.Point(790, 0)
        Me.barDockControlRight.Size = New System.Drawing.Size(0, 549)
        '
        'btnSaveNew
        '
        Me.btnSaveNew.Caption = "F2 - Save && New"
        Me.btnSaveNew.Id = 2
        Me.btnSaveNew.Name = "btnSaveNew"
        '
        'progressBarSave
        '
        Me.progressBarSave.Name = "progressBarSave"
        Me.progressBarSave.Stopped = True
        '
        'progressSave
        '
        Me.progressSave.Name = "progressSave"
        Me.progressSave.Paused = True
        '
        'mePenanganandanPenilaian
        '
        Me.mePenanganandanPenilaian.Name = "mePenanganandanPenilaian"
        '
        'LayoutControlGroup1
        '
        Me.LayoutControlGroup1.CustomizationFormText = "LayoutControlGroup1"
        Me.LayoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.LayoutControlGroup1.GroupBordersVisible = False
        Me.LayoutControlGroup1.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlItem5})
        Me.LayoutControlGroup1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlGroup1.Name = "Root"
        Me.LayoutControlGroup1.Size = New System.Drawing.Size(790, 549)
        Me.LayoutControlGroup1.TextVisible = False
        '
        'LayoutControlItem5
        '
        Me.LayoutControlItem5.Control = Me.tabControl
        Me.LayoutControlItem5.CustomizationFormText = "LayoutControlItem5"
        Me.LayoutControlItem5.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlItem5.Name = "LayoutControlItem5"
        Me.LayoutControlItem5.Size = New System.Drawing.Size(770, 529)
        Me.LayoutControlItem5.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem5.TextVisible = False
        '
        'mnuStrip
        '
        Me.mnuStrip.ImageScalingSize = New System.Drawing.Size(20, 20)
        Me.mnuStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.DeleteToolStripMenuItem})
        Me.mnuStrip.Name = "mnuStrip"
        Me.mnuStrip.Size = New System.Drawing.Size(108, 26)
        '
        'DeleteToolStripMenuItem
        '
        Me.DeleteToolStripMenuItem.Name = "DeleteToolStripMenuItem"
        Me.DeleteToolStripMenuItem.Size = New System.Drawing.Size(107, 22)
        Me.DeleteToolStripMenuItem.Text = "Delete"
        '
        'GridColumn6
        '
        Me.GridColumn6.Caption = "Number"
        Me.GridColumn6.FieldName = "KDSO"
        Me.GridColumn6.Name = "GridColumn6"
        Me.GridColumn6.Visible = True
        Me.GridColumn6.VisibleIndex = 0
        '
        'GridColumn3
        '
        Me.GridColumn3.Caption = "Name Display"
        Me.GridColumn3.FieldName = "NAME_DISPLAY"
        Me.GridColumn3.Name = "GridColumn3"
        Me.GridColumn3.Visible = True
        Me.GridColumn3.VisibleIndex = 0
        '
        'frmEmedrekIGDDetail
        '
        Me.Appearance.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(236, Byte), Integer), CType(CType(239, Byte), Integer))
        Me.Appearance.Options.UseBackColor = True
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(790, 571)
        Me.Controls.Add(Me.layoutControl)
        Me.Controls.Add(Me.barDockControlLeft)
        Me.Controls.Add(Me.barDockControlRight)
        Me.Controls.Add(Me.barDockControlBottom)
        Me.Controls.Add(Me.barDockControlTop)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
        Me.KeyPreview = True
        Me.Name = "frmEmedrekIGDDetail"
        Me.ShowIcon = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        CType(Me.layoutControl, System.ComponentModel.ISupportInitialize).EndInit()
        Me.layoutControl.ResumeLayout(False)
        CType(Me.tabControl, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tabControl.ResumeLayout(False)
        Me.tab1.ResumeLayout(False)
        CType(Me.grdDetail, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.BindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grvDetail, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dePUKUL.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dePUKUL, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemMemoEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdUSER, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemSearchLookUpEdit1View, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.barManager, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.progressBarSave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.progressSave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.mePenanganandanPenilaian, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem5, System.ComponentModel.ISupportInitialize).EndInit()
        Me.mnuStrip.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout

End Sub
    Friend WithEvents layoutControl As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents LayoutControlGroup1 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents barManager As DevExpress.XtraBars.BarManager
    Friend WithEvents barTop As DevExpress.XtraBars.Bar
    Friend WithEvents barDockControlTop As DevExpress.XtraBars.BarDockControl
    Friend WithEvents barDockControlBottom As DevExpress.XtraBars.BarDockControl
    Friend WithEvents barDockControlLeft As DevExpress.XtraBars.BarDockControl
    Friend WithEvents barDockControlRight As DevExpress.XtraBars.BarDockControl
    Friend WithEvents btnSaveNew As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents btnClose As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents btnSaveClose As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents progressBarSave As DevExpress.XtraEditors.Repository.RepositoryItemMarqueeProgressBar
    Friend WithEvents progressSave As DevExpress.XtraEditors.Repository.RepositoryItemMarqueeProgressBar
    Friend WithEvents tabControl As DevExpress.XtraTab.XtraTabControl
    Friend WithEvents tab1 As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents LayoutControlItem5 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents mnuStrip As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents DeleteToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents GridColumn6 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn3 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents grdDetail As DevExpress.XtraGrid.GridControl
    Friend WithEvents grvDetail As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents colPUKUL As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents dePUKUL As DevExpress.XtraEditors.Repository.RepositoryItemDateEdit
    Friend WithEvents colPENANGANAN As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents RepositoryItemMemoEdit1 As DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit
    Friend WithEvents colNAMA As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents grdUSER As DevExpress.XtraEditors.Repository.RepositoryItemSearchLookUpEdit
    Friend WithEvents RepositoryItemSearchLookUpEdit1View As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridColumn1 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colPARAF As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents mePenanganandanPenilaian As DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit
    Friend WithEvents BindingSource As BindingSource
End Class
