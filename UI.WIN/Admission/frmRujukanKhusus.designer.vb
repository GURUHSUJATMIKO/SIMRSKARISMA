<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmRujukanKhusus
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
        Me.grdDetail_Prosedur = New DevExpress.XtraGrid.GridControl()
        Me.mnuStrip2 = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.ToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.BindingSource2 = New System.Windows.Forms.BindingSource(Me.components)
        Me.grvDetail_Prosedur = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.colKDPROSEDUR = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.grdKDPROSEDUR = New DevExpress.XtraEditors.Repository.RepositoryItemSearchLookUpEdit()
        Me.grvKDPROSEDUR = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn11 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colREMARKS_PROSEDUR = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.barManager = New DevExpress.XtraBars.BarManager(Me.components)
        Me.barTop = New DevExpress.XtraBars.Bar()
        Me.btnSaveNew = New DevExpress.XtraBars.BarButtonItem()
        Me.btnSaveClose = New DevExpress.XtraBars.BarButtonItem()
        Me.btnClose = New DevExpress.XtraBars.BarButtonItem()
        Me.barDockControlTop = New DevExpress.XtraBars.BarDockControl()
        Me.barDockControlBottom = New DevExpress.XtraBars.BarDockControl()
        Me.barDockControlLeft = New DevExpress.XtraBars.BarDockControl()
        Me.barDockControlRight = New DevExpress.XtraBars.BarDockControl()
        Me.progressBarSave = New DevExpress.XtraEditors.Repository.RepositoryItemMarqueeProgressBar()
        Me.progressSave = New DevExpress.XtraEditors.Repository.RepositoryItemMarqueeProgressBar()
        Me.RepositoryItemMemoExEdit1 = New DevExpress.XtraEditors.Repository.RepositoryItemMemoExEdit()
        Me.grdDetail_Diagnosa = New DevExpress.XtraGrid.GridControl()
        Me.mnuStrip = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.DeleteToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.BindingSource1 = New System.Windows.Forms.BindingSource(Me.components)
        Me.grvDetail_Diagnosa = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.colKDDIAGNOSA = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.grdKDDIAGNOSA = New DevExpress.XtraEditors.Repository.RepositoryItemSearchLookUpEdit()
        Me.grvKDDIAGNOSA = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn27 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colREMARKS_DIAGNOSA = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.txtREMARKS = New DevExpress.XtraEditors.Repository.RepositoryItemMemoExEdit()
        Me.txtCODE = New DevExpress.XtraEditors.TextEdit()
        Me.LayoutControlGroup1 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.lKDRUJUKAN = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem1 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem2 = New DevExpress.XtraLayout.LayoutControlItem()
        CType(Me.layoutControl, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.layoutControl.SuspendLayout()
        CType(Me.grdDetail_Prosedur, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.mnuStrip2.SuspendLayout()
        CType(Me.BindingSource2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grvDetail_Prosedur, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdKDPROSEDUR, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grvKDPROSEDUR, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.barManager, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.progressBarSave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.progressSave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemMemoExEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdDetail_Diagnosa, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.mnuStrip.SuspendLayout()
        CType(Me.BindingSource1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grvDetail_Diagnosa, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdKDDIAGNOSA, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grvKDDIAGNOSA, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtREMARKS, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCODE.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lKDRUJUKAN, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'layoutControl
        '
        Me.layoutControl.Controls.Add(Me.grdDetail_Prosedur)
        Me.layoutControl.Controls.Add(Me.grdDetail_Diagnosa)
        Me.layoutControl.Controls.Add(Me.txtCODE)
        Me.layoutControl.Dock = System.Windows.Forms.DockStyle.Fill
        Me.layoutControl.Location = New System.Drawing.Point(0, 0)
        Me.layoutControl.Name = "layoutControl"
        Me.layoutControl.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = New System.Drawing.Rectangle(774, 238, 250, 350)
        Me.layoutControl.Root = Me.LayoutControlGroup1
        Me.layoutControl.Size = New System.Drawing.Size(711, 485)
        Me.layoutControl.TabIndex = 0
        Me.layoutControl.Text = "LayoutControl1"
        '
        'grdDetail_Prosedur
        '
        Me.grdDetail_Prosedur.ContextMenuStrip = Me.mnuStrip2
        Me.grdDetail_Prosedur.DataSource = Me.BindingSource2
        Me.grdDetail_Prosedur.Location = New System.Drawing.Point(358, 36)
        Me.grdDetail_Prosedur.MainView = Me.grvDetail_Prosedur
        Me.grdDetail_Prosedur.MenuManager = Me.barManager
        Me.grdDetail_Prosedur.Name = "grdDetail_Prosedur"
        Me.grdDetail_Prosedur.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.RepositoryItemMemoExEdit1, Me.grdKDPROSEDUR})
        Me.grdDetail_Prosedur.Size = New System.Drawing.Size(341, 437)
        Me.grdDetail_Prosedur.TabIndex = 20
        Me.grdDetail_Prosedur.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.grvDetail_Prosedur})
        '
        'mnuStrip2
        '
        Me.mnuStrip2.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripMenuItem1})
        Me.mnuStrip2.Name = "mnuStrip"
        Me.mnuStrip2.Size = New System.Drawing.Size(108, 26)
        '
        'ToolStripMenuItem1
        '
        Me.ToolStripMenuItem1.Name = "ToolStripMenuItem1"
        Me.ToolStripMenuItem1.Size = New System.Drawing.Size(107, 22)
        Me.ToolStripMenuItem1.Text = "Delete"
        '
        'BindingSource2
        '
        Me.BindingSource2.DataSource = GetType(DataAccess.S_PENDAFTARAN_LPK_D2)
        '
        'grvDetail_Prosedur
        '
        Me.grvDetail_Prosedur.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.colKDPROSEDUR, Me.colREMARKS_PROSEDUR})
        Me.grvDetail_Prosedur.GridControl = Me.grdDetail_Prosedur
        Me.grvDetail_Prosedur.Name = "grvDetail_Prosedur"
        Me.grvDetail_Prosedur.OptionsCustomization.AllowColumnMoving = False
        Me.grvDetail_Prosedur.OptionsCustomization.AllowFilter = False
        Me.grvDetail_Prosedur.OptionsCustomization.AllowGroup = False
        Me.grvDetail_Prosedur.OptionsCustomization.AllowQuickHideColumns = False
        Me.grvDetail_Prosedur.OptionsCustomization.AllowSort = False
        Me.grvDetail_Prosedur.OptionsDetail.EnableMasterViewMode = False
        Me.grvDetail_Prosedur.OptionsFind.AllowFindPanel = False
        Me.grvDetail_Prosedur.OptionsLayout.StoreAllOptions = True
        Me.grvDetail_Prosedur.OptionsLayout.StoreAppearance = True
        Me.grvDetail_Prosedur.OptionsMenu.EnableColumnMenu = False
        Me.grvDetail_Prosedur.OptionsNavigation.AutoFocusNewRow = True
        Me.grvDetail_Prosedur.OptionsNavigation.EnterMoveNextColumn = True
        Me.grvDetail_Prosedur.OptionsView.EnableAppearanceEvenRow = True
        Me.grvDetail_Prosedur.OptionsView.EnableAppearanceOddRow = True
        Me.grvDetail_Prosedur.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Bottom
        Me.grvDetail_Prosedur.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never
        Me.grvDetail_Prosedur.OptionsView.ShowFooter = True
        Me.grvDetail_Prosedur.OptionsView.ShowGroupPanel = False
        '
        'colKDPROSEDUR
        '
        Me.colKDPROSEDUR.Caption = "Prosedur"
        Me.colKDPROSEDUR.ColumnEdit = Me.grdKDPROSEDUR
        Me.colKDPROSEDUR.FieldName = "KDPROSEDUR"
        Me.colKDPROSEDUR.Name = "colKDPROSEDUR"
        Me.colKDPROSEDUR.Visible = True
        Me.colKDPROSEDUR.VisibleIndex = 0
        '
        'grdKDPROSEDUR
        '
        Me.grdKDPROSEDUR.AutoHeight = False
        Me.grdKDPROSEDUR.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.grdKDPROSEDUR.Name = "grdKDPROSEDUR"
        Me.grdKDPROSEDUR.NullText = ""
        Me.grdKDPROSEDUR.View = Me.grvKDPROSEDUR
        '
        'grvKDPROSEDUR
        '
        Me.grvKDPROSEDUR.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn11})
        Me.grvKDPROSEDUR.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.grvKDPROSEDUR.Name = "grvKDPROSEDUR"
        Me.grvKDPROSEDUR.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.grvKDPROSEDUR.OptionsView.ShowGroupPanel = False
        '
        'GridColumn11
        '
        Me.GridColumn11.Caption = "Name Display"
        Me.GridColumn11.FieldName = "MEMO"
        Me.GridColumn11.Name = "GridColumn11"
        Me.GridColumn11.Visible = True
        Me.GridColumn11.VisibleIndex = 0
        '
        'colREMARKS_PROSEDUR
        '
        Me.colREMARKS_PROSEDUR.Caption = "Catatan"
        Me.colREMARKS_PROSEDUR.FieldName = "REMARKS"
        Me.colREMARKS_PROSEDUR.Name = "colREMARKS_PROSEDUR"
        Me.colREMARKS_PROSEDUR.OptionsColumn.TabStop = False
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
        Me.barTop.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.btnSaveNew), New DevExpress.XtraBars.LinkPersistInfo(Me.btnSaveClose), New DevExpress.XtraBars.LinkPersistInfo(Me.btnClose)})
        Me.barTop.OptionsBar.DrawDragBorder = False
        Me.barTop.OptionsBar.MultiLine = True
        Me.barTop.OptionsBar.UseWholeRow = True
        Me.barTop.Text = "Main menu"
        '
        'btnSaveNew
        '
        Me.btnSaveNew.Caption = "F2 - Save && New"
        Me.btnSaveNew.Id = 2
        Me.btnSaveNew.Name = "btnSaveNew"
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
        Me.barDockControlTop.Size = New System.Drawing.Size(711, 0)
        '
        'barDockControlBottom
        '
        Me.barDockControlBottom.CausesValidation = False
        Me.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.barDockControlBottom.Location = New System.Drawing.Point(0, 485)
        Me.barDockControlBottom.Size = New System.Drawing.Size(711, 22)
        '
        'barDockControlLeft
        '
        Me.barDockControlLeft.CausesValidation = False
        Me.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left
        Me.barDockControlLeft.Location = New System.Drawing.Point(0, 0)
        Me.barDockControlLeft.Size = New System.Drawing.Size(0, 485)
        '
        'barDockControlRight
        '
        Me.barDockControlRight.CausesValidation = False
        Me.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right
        Me.barDockControlRight.Location = New System.Drawing.Point(711, 0)
        Me.barDockControlRight.Size = New System.Drawing.Size(0, 485)
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
        'RepositoryItemMemoExEdit1
        '
        Me.RepositoryItemMemoExEdit1.AutoHeight = False
        Me.RepositoryItemMemoExEdit1.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepositoryItemMemoExEdit1.Name = "RepositoryItemMemoExEdit1"
        '
        'grdDetail_Diagnosa
        '
        Me.grdDetail_Diagnosa.ContextMenuStrip = Me.mnuStrip
        Me.grdDetail_Diagnosa.DataSource = Me.BindingSource1
        Me.grdDetail_Diagnosa.Location = New System.Drawing.Point(12, 36)
        Me.grdDetail_Diagnosa.MainView = Me.grvDetail_Diagnosa
        Me.grdDetail_Diagnosa.MenuManager = Me.barManager
        Me.grdDetail_Diagnosa.Name = "grdDetail_Diagnosa"
        Me.grdDetail_Diagnosa.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.txtREMARKS, Me.grdKDDIAGNOSA})
        Me.grdDetail_Diagnosa.Size = New System.Drawing.Size(342, 437)
        Me.grdDetail_Diagnosa.TabIndex = 19
        Me.grdDetail_Diagnosa.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.grvDetail_Diagnosa})
        '
        'mnuStrip
        '
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
        'BindingSource1
        '
        Me.BindingSource1.DataSource = GetType(DataAccess.S_PENDAFTARAN_LPK_D1)
        '
        'grvDetail_Diagnosa
        '
        Me.grvDetail_Diagnosa.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.colKDDIAGNOSA, Me.colREMARKS_DIAGNOSA})
        Me.grvDetail_Diagnosa.GridControl = Me.grdDetail_Diagnosa
        Me.grvDetail_Diagnosa.Name = "grvDetail_Diagnosa"
        Me.grvDetail_Diagnosa.OptionsCustomization.AllowColumnMoving = False
        Me.grvDetail_Diagnosa.OptionsCustomization.AllowFilter = False
        Me.grvDetail_Diagnosa.OptionsCustomization.AllowGroup = False
        Me.grvDetail_Diagnosa.OptionsCustomization.AllowQuickHideColumns = False
        Me.grvDetail_Diagnosa.OptionsCustomization.AllowSort = False
        Me.grvDetail_Diagnosa.OptionsDetail.EnableMasterViewMode = False
        Me.grvDetail_Diagnosa.OptionsFind.AllowFindPanel = False
        Me.grvDetail_Diagnosa.OptionsLayout.StoreAllOptions = True
        Me.grvDetail_Diagnosa.OptionsLayout.StoreAppearance = True
        Me.grvDetail_Diagnosa.OptionsMenu.EnableColumnMenu = False
        Me.grvDetail_Diagnosa.OptionsNavigation.AutoFocusNewRow = True
        Me.grvDetail_Diagnosa.OptionsNavigation.EnterMoveNextColumn = True
        Me.grvDetail_Diagnosa.OptionsView.EnableAppearanceEvenRow = True
        Me.grvDetail_Diagnosa.OptionsView.EnableAppearanceOddRow = True
        Me.grvDetail_Diagnosa.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Bottom
        Me.grvDetail_Diagnosa.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never
        Me.grvDetail_Diagnosa.OptionsView.ShowFooter = True
        Me.grvDetail_Diagnosa.OptionsView.ShowGroupPanel = False
        '
        'colKDDIAGNOSA
        '
        Me.colKDDIAGNOSA.Caption = "Diagnosa"
        Me.colKDDIAGNOSA.ColumnEdit = Me.grdKDDIAGNOSA
        Me.colKDDIAGNOSA.FieldName = "KDDIAGNOSA"
        Me.colKDDIAGNOSA.Name = "colKDDIAGNOSA"
        Me.colKDDIAGNOSA.Visible = True
        Me.colKDDIAGNOSA.VisibleIndex = 0
        '
        'grdKDDIAGNOSA
        '
        Me.grdKDDIAGNOSA.AutoHeight = False
        Me.grdKDDIAGNOSA.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.grdKDDIAGNOSA.Name = "grdKDDIAGNOSA"
        Me.grdKDDIAGNOSA.NullText = ""
        Me.grdKDDIAGNOSA.View = Me.grvKDDIAGNOSA
        '
        'grvKDDIAGNOSA
        '
        Me.grvKDDIAGNOSA.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn27})
        Me.grvKDDIAGNOSA.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.grvKDDIAGNOSA.Name = "grvKDDIAGNOSA"
        Me.grvKDDIAGNOSA.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.grvKDDIAGNOSA.OptionsView.ShowGroupPanel = False
        '
        'GridColumn27
        '
        Me.GridColumn27.Caption = "Name Display"
        Me.GridColumn27.FieldName = "MEMO"
        Me.GridColumn27.Name = "GridColumn27"
        Me.GridColumn27.Visible = True
        Me.GridColumn27.VisibleIndex = 0
        '
        'colREMARKS_DIAGNOSA
        '
        Me.colREMARKS_DIAGNOSA.Caption = "Catatan"
        Me.colREMARKS_DIAGNOSA.FieldName = "REMARKS"
        Me.colREMARKS_DIAGNOSA.Name = "colREMARKS_DIAGNOSA"
        Me.colREMARKS_DIAGNOSA.OptionsColumn.TabStop = False
        '
        'txtREMARKS
        '
        Me.txtREMARKS.AutoHeight = False
        Me.txtREMARKS.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.txtREMARKS.Name = "txtREMARKS"
        '
        'txtCODE
        '
        Me.txtCODE.Location = New System.Drawing.Point(137, 12)
        Me.txtCODE.MenuManager = Me.barManager
        Me.txtCODE.Name = "txtCODE"
        Me.txtCODE.Properties.ReadOnly = True
        Me.txtCODE.Size = New System.Drawing.Size(562, 20)
        Me.txtCODE.StyleController = Me.layoutControl
        Me.txtCODE.TabIndex = 47
        '
        'LayoutControlGroup1
        '
        Me.LayoutControlGroup1.CustomizationFormText = "LayoutControlGroup1"
        Me.LayoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.LayoutControlGroup1.GroupBordersVisible = False
        Me.LayoutControlGroup1.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.lKDRUJUKAN, Me.LayoutControlItem1, Me.LayoutControlItem2})
        Me.LayoutControlGroup1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlGroup1.Name = "LayoutControlGroup1"
        Me.LayoutControlGroup1.Size = New System.Drawing.Size(711, 485)
        Me.LayoutControlGroup1.TextVisible = False
        '
        'lKDRUJUKAN
        '
        Me.lKDRUJUKAN.AppearanceItemCaption.Options.UseTextOptions = True
        Me.lKDRUJUKAN.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.lKDRUJUKAN.Control = Me.txtCODE
        Me.lKDRUJUKAN.Location = New System.Drawing.Point(0, 0)
        Me.lKDRUJUKAN.Name = "lKDRUJUKAN"
        Me.lKDRUJUKAN.Size = New System.Drawing.Size(691, 24)
        Me.lKDRUJUKAN.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.lKDRUJUKAN.TextSize = New System.Drawing.Size(120, 20)
        Me.lKDRUJUKAN.TextToControlDistance = 5
        '
        'LayoutControlItem1
        '
        Me.LayoutControlItem1.Control = Me.grdDetail_Diagnosa
        Me.LayoutControlItem1.Location = New System.Drawing.Point(0, 24)
        Me.LayoutControlItem1.Name = "LayoutControlItem1"
        Me.LayoutControlItem1.Size = New System.Drawing.Size(346, 441)
        Me.LayoutControlItem1.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem1.TextVisible = False
        '
        'LayoutControlItem2
        '
        Me.LayoutControlItem2.Control = Me.grdDetail_Prosedur
        Me.LayoutControlItem2.Location = New System.Drawing.Point(346, 24)
        Me.LayoutControlItem2.Name = "LayoutControlItem2"
        Me.LayoutControlItem2.Size = New System.Drawing.Size(345, 441)
        Me.LayoutControlItem2.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem2.TextVisible = False
        '
        'frmRujukanKhusus
        '
        Me.Appearance.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(236, Byte), Integer), CType(CType(239, Byte), Integer))
        Me.Appearance.Options.UseBackColor = True
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(711, 507)
        Me.Controls.Add(Me.layoutControl)
        Me.Controls.Add(Me.barDockControlLeft)
        Me.Controls.Add(Me.barDockControlRight)
        Me.Controls.Add(Me.barDockControlBottom)
        Me.Controls.Add(Me.barDockControlTop)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
        Me.KeyPreview = True
        Me.Name = "frmRujukanKhusus"
        Me.ShowIcon = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        CType(Me.layoutControl, System.ComponentModel.ISupportInitialize).EndInit()
        Me.layoutControl.ResumeLayout(False)
        CType(Me.grdDetail_Prosedur, System.ComponentModel.ISupportInitialize).EndInit()
        Me.mnuStrip2.ResumeLayout(False)
        CType(Me.BindingSource2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grvDetail_Prosedur, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdKDPROSEDUR, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grvKDPROSEDUR, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.barManager, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.progressBarSave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.progressSave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemMemoExEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdDetail_Diagnosa, System.ComponentModel.ISupportInitialize).EndInit()
        Me.mnuStrip.ResumeLayout(False)
        CType(Me.BindingSource1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grvDetail_Diagnosa, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdKDDIAGNOSA, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grvKDDIAGNOSA, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtREMARKS, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCODE.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lKDRUJUKAN, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

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
    Friend WithEvents txtCODE As DevExpress.XtraEditors.TextEdit
    Friend WithEvents lKDRUJUKAN As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents grdDetail_Prosedur As DevExpress.XtraGrid.GridControl
    Friend WithEvents grvDetail_Prosedur As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents colKDPROSEDUR As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents grdKDPROSEDUR As DevExpress.XtraEditors.Repository.RepositoryItemSearchLookUpEdit
    Friend WithEvents grvKDPROSEDUR As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridColumn11 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colREMARKS_PROSEDUR As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents RepositoryItemMemoExEdit1 As DevExpress.XtraEditors.Repository.RepositoryItemMemoExEdit
    Friend WithEvents grdDetail_Diagnosa As DevExpress.XtraGrid.GridControl
    Friend WithEvents grvDetail_Diagnosa As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents colKDDIAGNOSA As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents grdKDDIAGNOSA As DevExpress.XtraEditors.Repository.RepositoryItemSearchLookUpEdit
    Friend WithEvents grvKDDIAGNOSA As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridColumn27 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colREMARKS_DIAGNOSA As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents txtREMARKS As DevExpress.XtraEditors.Repository.RepositoryItemMemoExEdit
    Friend WithEvents LayoutControlItem1 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem2 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents BindingSource2 As BindingSource
    Friend WithEvents BindingSource1 As BindingSource
    Friend WithEvents mnuStrip2 As ContextMenuStrip
    Friend WithEvents ToolStripMenuItem1 As ToolStripMenuItem
    Friend WithEvents mnuStrip As ContextMenuStrip
    Friend WithEvents DeleteToolStripMenuItem As ToolStripMenuItem
End Class
