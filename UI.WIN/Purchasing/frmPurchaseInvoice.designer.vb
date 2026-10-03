<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmPurchaseInvoice
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
        Me.layoutControl = New DevExpress.XtraLayout.LayoutControl()
        Me.txtNOFAKTUR = New DevExpress.XtraEditors.TextEdit()
        Me.txtMATERAI = New DevExpress.XtraEditors.TextEdit()
        Me.barManager = New DevExpress.XtraBars.BarManager(Me.components)
        Me.barTop = New DevExpress.XtraBars.Bar()
        Me.btnSaveNew = New DevExpress.XtraBars.BarButtonItem()
        Me.btnSaveClose = New DevExpress.XtraBars.BarButtonItem()
        Me.btnClose = New DevExpress.XtraBars.BarButtonItem()
        Me.barDockControlTop = New DevExpress.XtraBars.BarDockControl()
        Me.barDockControlBottom = New DevExpress.XtraBars.BarDockControl()
        Me.barDockControlLeft = New DevExpress.XtraBars.BarDockControl()
        Me.barDockControlRight = New DevExpress.XtraBars.BarDockControl()
        Me.btnHistory = New DevExpress.XtraBars.BarButtonItem()
        Me.progressBarSave = New DevExpress.XtraEditors.Repository.RepositoryItemMarqueeProgressBar()
        Me.progressSave = New DevExpress.XtraEditors.Repository.RepositoryItemMarqueeProgressBar()
        Me.txtPPN_PERSEN = New DevExpress.XtraEditors.TextEdit()
        Me.txtDISCOUNT_PERSEN = New DevExpress.XtraEditors.TextEdit()
        Me.deDATECREATED = New DevExpress.XtraEditors.DateEdit()
        Me.deDATEDUE = New DevExpress.XtraEditors.DateEdit()
        Me.grdKDWAREHOUSE = New DevExpress.XtraEditors.GridLookUpEdit()
        Me.grvKDWAREHOUSE = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn9 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.txtGRANDTOTAL = New DevExpress.XtraEditors.TextEdit()
        Me.txtPPN = New DevExpress.XtraEditors.TextEdit()
        Me.txtDISCOUNT = New DevExpress.XtraEditors.TextEdit()
        Me.txtSUBTOTAL = New DevExpress.XtraEditors.TextEdit()
        Me.grdKDVENDOR = New DevExpress.XtraEditors.GridLookUpEdit()
        Me.grvKDVENDOR = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn3 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.LayoutControl5 = New DevExpress.XtraLayout.LayoutControl()
        Me.LayoutControlGroup5 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.EmptySpaceItem1 = New DevExpress.XtraLayout.EmptySpaceItem()
        Me.deDATE = New DevExpress.XtraEditors.DateEdit()
        Me.tabControl = New DevExpress.XtraTab.XtraTabControl()
        Me.tab1 = New DevExpress.XtraTab.XtraTabPage()
        Me.grdDetail = New DevExpress.XtraGrid.GridControl()
        Me.mnuStrip = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.DeleteToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.PrintBarcodeToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.bindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.grvDetail = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.colKDITEM = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.grdKDITEM = New DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit()
        Me.grvKDITEM = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn2 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn1 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colQTY = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colKDUOM = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.grdKDUOM = New DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit()
        Me.grvKDUOM = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn4 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colPRICE = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.betxtPrice = New DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit()
        Me.colSUBPRICE = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colSUBTOTAL = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colDISCOUNT_PERSEN = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colDISCOUNT = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colPPN_PERSEN = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colPPN = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colGRANDTOTAL = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colBATCH = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colDATE_EXPIRED = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colISUPDATEHARGA = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colREMARKS = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.txtREMARKS = New DevExpress.XtraEditors.Repository.RepositoryItemMemoExEdit()
        Me.tab2 = New DevExpress.XtraTab.XtraTabPage()
        Me.LayoutControl1 = New DevExpress.XtraLayout.LayoutControl()
        Me.txtMEMO = New DevExpress.XtraEditors.MemoEdit()
        Me.s = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.LayoutControlItem7 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.txtKDPI = New DevExpress.XtraEditors.TextEdit()
        Me.LayoutControlGroup1 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.lKDPI = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem5 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.lDATE = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem3 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.lKDVENDOR = New DevExpress.XtraLayout.LayoutControlItem()
        Me.EmptySpaceItem2 = New DevExpress.XtraLayout.EmptySpaceItem()
        Me.lSUBTOTAL = New DevExpress.XtraLayout.LayoutControlItem()
        Me.lDISCOUNT = New DevExpress.XtraLayout.LayoutControlItem()
        Me.lTAX = New DevExpress.XtraLayout.LayoutControlItem()
        Me.lGRANDTOTAL = New DevExpress.XtraLayout.LayoutControlItem()
        Me.lKDWAREHOUSE = New DevExpress.XtraLayout.LayoutControlItem()
        Me.lDATEDUE = New DevExpress.XtraLayout.LayoutControlItem()
        Me.lDATE_CREATED = New DevExpress.XtraLayout.LayoutControlItem()
        Me.lMATERAI = New DevExpress.XtraLayout.LayoutControlItem()
        Me.lNOFAKTUR = New DevExpress.XtraLayout.LayoutControlItem()
        Me.lDISCOUNT_PERSEN = New DevExpress.XtraLayout.LayoutControlItem()
        Me.lPPN_PERSEN = New DevExpress.XtraLayout.LayoutControlItem()
        Me.GridColumn6 = New DevExpress.XtraGrid.Columns.GridColumn()
        CType(Me.layoutControl, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.layoutControl.SuspendLayout()
        CType(Me.txtNOFAKTUR.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtMATERAI.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.barManager, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.progressBarSave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.progressSave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtPPN_PERSEN.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDISCOUNT_PERSEN.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.deDATECREATED.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.deDATECREATED.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.deDATEDUE.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.deDATEDUE.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdKDWAREHOUSE.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grvKDWAREHOUSE, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtGRANDTOTAL.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtPPN.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDISCOUNT.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtSUBTOTAL.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdKDVENDOR.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grvKDVENDOR, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControl5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.EmptySpaceItem1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.deDATE.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.deDATE.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.tabControl, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tabControl.SuspendLayout()
        Me.tab1.SuspendLayout()
        CType(Me.grdDetail, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.mnuStrip.SuspendLayout()
        CType(Me.bindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grvDetail, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdKDITEM, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grvKDITEM, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdKDUOM, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grvKDUOM, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.betxtPrice, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtREMARKS, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tab2.SuspendLayout()
        CType(Me.LayoutControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.LayoutControl1.SuspendLayout()
        CType(Me.txtMEMO.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.s, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem7, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtKDPI.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lKDPI, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lDATE, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lKDVENDOR, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.EmptySpaceItem2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lSUBTOTAL, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lDISCOUNT, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lTAX, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lGRANDTOTAL, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lKDWAREHOUSE, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lDATEDUE, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lDATE_CREATED, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lMATERAI, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lNOFAKTUR, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lDISCOUNT_PERSEN, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lPPN_PERSEN, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'layoutControl
        '
        Me.layoutControl.Controls.Add(Me.txtNOFAKTUR)
        Me.layoutControl.Controls.Add(Me.txtMATERAI)
        Me.layoutControl.Controls.Add(Me.txtPPN_PERSEN)
        Me.layoutControl.Controls.Add(Me.txtDISCOUNT_PERSEN)
        Me.layoutControl.Controls.Add(Me.deDATECREATED)
        Me.layoutControl.Controls.Add(Me.deDATEDUE)
        Me.layoutControl.Controls.Add(Me.grdKDWAREHOUSE)
        Me.layoutControl.Controls.Add(Me.txtGRANDTOTAL)
        Me.layoutControl.Controls.Add(Me.txtPPN)
        Me.layoutControl.Controls.Add(Me.txtDISCOUNT)
        Me.layoutControl.Controls.Add(Me.txtSUBTOTAL)
        Me.layoutControl.Controls.Add(Me.grdKDVENDOR)
        Me.layoutControl.Controls.Add(Me.LayoutControl5)
        Me.layoutControl.Controls.Add(Me.deDATE)
        Me.layoutControl.Controls.Add(Me.tabControl)
        Me.layoutControl.Controls.Add(Me.txtKDPI)
        Me.layoutControl.Dock = System.Windows.Forms.DockStyle.Fill
        Me.layoutControl.Location = New System.Drawing.Point(0, 0)
        Me.layoutControl.Name = "layoutControl"
        Me.layoutControl.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = New System.Drawing.Rectangle(652, 156, 250, 350)
        Me.layoutControl.Root = Me.LayoutControlGroup1
        Me.layoutControl.Size = New System.Drawing.Size(790, 549)
        Me.layoutControl.TabIndex = 0
        Me.layoutControl.Text = "LayoutControl1"
        '
        'txtNOFAKTUR
        '
        Me.txtNOFAKTUR.EditValue = ""
        Me.txtNOFAKTUR.EnterMoveNextControl = True
        Me.txtNOFAKTUR.Location = New System.Drawing.Point(117, 84)
        Me.txtNOFAKTUR.Name = "txtNOFAKTUR"
        Me.txtNOFAKTUR.Size = New System.Drawing.Size(375, 20)
        Me.txtNOFAKTUR.StyleController = Me.layoutControl
        Me.txtNOFAKTUR.TabIndex = 10
        '
        'txtMATERAI
        '
        Me.txtMATERAI.EnterMoveNextControl = True
        Me.txtMATERAI.Location = New System.Drawing.Point(575, 493)
        Me.txtMATERAI.MenuManager = Me.barManager
        Me.txtMATERAI.Name = "txtMATERAI"
        Me.txtMATERAI.Properties.Appearance.Options.UseTextOptions = True
        Me.txtMATERAI.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.txtMATERAI.Properties.Mask.EditMask = "n2"
        Me.txtMATERAI.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        Me.txtMATERAI.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.txtMATERAI.Properties.NullText = "0.00"
        Me.txtMATERAI.Size = New System.Drawing.Size(203, 20)
        Me.txtMATERAI.StyleController = Me.layoutControl
        Me.txtMATERAI.TabIndex = 30
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
        Me.barManager.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.btnSaveNew, Me.btnClose, Me.btnSaveClose, Me.btnHistory})
        Me.barManager.MainMenu = Me.barTop
        Me.barManager.MaxItemId = 9
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
        'btnHistory
        '
        Me.btnHistory.Caption = "F11 - History"
        Me.btnHistory.Id = 8
        Me.btnHistory.Name = "btnHistory"
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
        'txtPPN_PERSEN
        '
        Me.txtPPN_PERSEN.EnterMoveNextControl = True
        Me.txtPPN_PERSEN.Location = New System.Drawing.Point(575, 469)
        Me.txtPPN_PERSEN.MenuManager = Me.barManager
        Me.txtPPN_PERSEN.Name = "txtPPN_PERSEN"
        Me.txtPPN_PERSEN.Properties.Appearance.Options.UseTextOptions = True
        Me.txtPPN_PERSEN.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.txtPPN_PERSEN.Properties.Mask.EditMask = "n2"
        Me.txtPPN_PERSEN.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        Me.txtPPN_PERSEN.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.txtPPN_PERSEN.Properties.NullText = "0.00"
        Me.txtPPN_PERSEN.Size = New System.Drawing.Size(75, 20)
        Me.txtPPN_PERSEN.StyleController = Me.layoutControl
        Me.txtPPN_PERSEN.TabIndex = 30
        '
        'txtDISCOUNT_PERSEN
        '
        Me.txtDISCOUNT_PERSEN.EnterMoveNextControl = True
        Me.txtDISCOUNT_PERSEN.Location = New System.Drawing.Point(575, 445)
        Me.txtDISCOUNT_PERSEN.MenuManager = Me.barManager
        Me.txtDISCOUNT_PERSEN.Name = "txtDISCOUNT_PERSEN"
        Me.txtDISCOUNT_PERSEN.Properties.Appearance.Options.UseTextOptions = True
        Me.txtDISCOUNT_PERSEN.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.txtDISCOUNT_PERSEN.Properties.Mask.EditMask = "n2"
        Me.txtDISCOUNT_PERSEN.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        Me.txtDISCOUNT_PERSEN.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.txtDISCOUNT_PERSEN.Properties.NullText = "0.00"
        Me.txtDISCOUNT_PERSEN.Size = New System.Drawing.Size(75, 20)
        Me.txtDISCOUNT_PERSEN.StyleController = Me.layoutControl
        Me.txtDISCOUNT_PERSEN.TabIndex = 30
        '
        'deDATECREATED
        '
        Me.deDATECREATED.EditValue = Nothing
        Me.deDATECREATED.EnterMoveNextControl = True
        Me.deDATECREATED.Location = New System.Drawing.Point(117, 36)
        Me.deDATECREATED.MenuManager = Me.barManager
        Me.deDATECREATED.Name = "deDATECREATED"
        Me.deDATECREATED.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.deDATECREATED.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
        Me.deDATECREATED.Properties.Mask.EditMask = "dd/MM/yyyy"
        Me.deDATECREATED.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.deDATECREATED.Size = New System.Drawing.Size(375, 20)
        Me.deDATECREATED.StyleController = Me.layoutControl
        Me.deDATECREATED.TabIndex = 21
        '
        'deDATEDUE
        '
        Me.deDATEDUE.EditValue = Nothing
        Me.deDATEDUE.Location = New System.Drawing.Point(117, 108)
        Me.deDATEDUE.MenuManager = Me.barManager
        Me.deDATEDUE.Name = "deDATEDUE"
        Me.deDATEDUE.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.deDATEDUE.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
        Me.deDATEDUE.Properties.Mask.EditMask = "dd/MM/yyyy"
        Me.deDATEDUE.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.deDATEDUE.Size = New System.Drawing.Size(375, 20)
        Me.deDATEDUE.StyleController = Me.layoutControl
        Me.deDATEDUE.TabIndex = 32
        '
        'grdKDWAREHOUSE
        '
        Me.grdKDWAREHOUSE.EnterMoveNextControl = True
        Me.grdKDWAREHOUSE.Location = New System.Drawing.Point(117, 156)
        Me.grdKDWAREHOUSE.MenuManager = Me.barManager
        Me.grdKDWAREHOUSE.Name = "grdKDWAREHOUSE"
        Me.grdKDWAREHOUSE.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.grdKDWAREHOUSE.Properties.NullText = ""
        Me.grdKDWAREHOUSE.Properties.PopupFormMinSize = New System.Drawing.Size(600, 300)
        Me.grdKDWAREHOUSE.Properties.View = Me.grvKDWAREHOUSE
        Me.grdKDWAREHOUSE.Size = New System.Drawing.Size(375, 20)
        Me.grdKDWAREHOUSE.StyleController = Me.layoutControl
        Me.grdKDWAREHOUSE.TabIndex = 31
        '
        'grvKDWAREHOUSE
        '
        Me.grvKDWAREHOUSE.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn9})
        Me.grvKDWAREHOUSE.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.grvKDWAREHOUSE.Name = "grvKDWAREHOUSE"
        Me.grvKDWAREHOUSE.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.grvKDWAREHOUSE.OptionsView.ShowAutoFilterRow = True
        Me.grvKDWAREHOUSE.OptionsView.ShowGroupPanel = False
        '
        'GridColumn9
        '
        Me.GridColumn9.Caption = "Display Name"
        Me.GridColumn9.FieldName = "NAME_DISPLAY"
        Me.GridColumn9.Name = "GridColumn9"
        Me.GridColumn9.Visible = True
        Me.GridColumn9.VisibleIndex = 0
        '
        'txtGRANDTOTAL
        '
        Me.txtGRANDTOTAL.EnterMoveNextControl = True
        Me.txtGRANDTOTAL.Location = New System.Drawing.Point(575, 517)
        Me.txtGRANDTOTAL.MenuManager = Me.barManager
        Me.txtGRANDTOTAL.Name = "txtGRANDTOTAL"
        Me.txtGRANDTOTAL.Properties.Appearance.Options.UseTextOptions = True
        Me.txtGRANDTOTAL.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.txtGRANDTOTAL.Properties.Mask.EditMask = "n2"
        Me.txtGRANDTOTAL.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        Me.txtGRANDTOTAL.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.txtGRANDTOTAL.Properties.NullText = "0.00"
        Me.txtGRANDTOTAL.Properties.ReadOnly = True
        Me.txtGRANDTOTAL.Size = New System.Drawing.Size(203, 20)
        Me.txtGRANDTOTAL.StyleController = Me.layoutControl
        Me.txtGRANDTOTAL.TabIndex = 30
        Me.txtGRANDTOTAL.TabStop = False
        '
        'txtPPN
        '
        Me.txtPPN.EnterMoveNextControl = True
        Me.txtPPN.Location = New System.Drawing.Point(654, 469)
        Me.txtPPN.MenuManager = Me.barManager
        Me.txtPPN.Name = "txtPPN"
        Me.txtPPN.Properties.Appearance.Options.UseTextOptions = True
        Me.txtPPN.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.txtPPN.Properties.Mask.EditMask = "n2"
        Me.txtPPN.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        Me.txtPPN.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.txtPPN.Properties.NullText = "0.00"
        Me.txtPPN.Size = New System.Drawing.Size(124, 20)
        Me.txtPPN.StyleController = Me.layoutControl
        Me.txtPPN.TabIndex = 29
        '
        'txtDISCOUNT
        '
        Me.txtDISCOUNT.EnterMoveNextControl = True
        Me.txtDISCOUNT.Location = New System.Drawing.Point(654, 445)
        Me.txtDISCOUNT.MenuManager = Me.barManager
        Me.txtDISCOUNT.Name = "txtDISCOUNT"
        Me.txtDISCOUNT.Properties.Appearance.Options.UseTextOptions = True
        Me.txtDISCOUNT.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.txtDISCOUNT.Properties.Mask.EditMask = "n2"
        Me.txtDISCOUNT.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        Me.txtDISCOUNT.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.txtDISCOUNT.Properties.NullText = "0.00"
        Me.txtDISCOUNT.Size = New System.Drawing.Size(124, 20)
        Me.txtDISCOUNT.StyleController = Me.layoutControl
        Me.txtDISCOUNT.TabIndex = 28
        '
        'txtSUBTOTAL
        '
        Me.txtSUBTOTAL.EnterMoveNextControl = True
        Me.txtSUBTOTAL.Location = New System.Drawing.Point(575, 421)
        Me.txtSUBTOTAL.MenuManager = Me.barManager
        Me.txtSUBTOTAL.Name = "txtSUBTOTAL"
        Me.txtSUBTOTAL.Properties.Appearance.Options.UseTextOptions = True
        Me.txtSUBTOTAL.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.txtSUBTOTAL.Properties.Mask.EditMask = "n2"
        Me.txtSUBTOTAL.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        Me.txtSUBTOTAL.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.txtSUBTOTAL.Properties.NullText = "0.00"
        Me.txtSUBTOTAL.Properties.ReadOnly = True
        Me.txtSUBTOTAL.Size = New System.Drawing.Size(203, 20)
        Me.txtSUBTOTAL.StyleController = Me.layoutControl
        Me.txtSUBTOTAL.TabIndex = 27
        Me.txtSUBTOTAL.TabStop = False
        '
        'grdKDVENDOR
        '
        Me.grdKDVENDOR.EditValue = ""
        Me.grdKDVENDOR.EnterMoveNextControl = True
        Me.grdKDVENDOR.Location = New System.Drawing.Point(117, 132)
        Me.grdKDVENDOR.MenuManager = Me.barManager
        Me.grdKDVENDOR.Name = "grdKDVENDOR"
        Me.grdKDVENDOR.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.grdKDVENDOR.Properties.NullText = ""
        Me.grdKDVENDOR.Properties.PopupFormMinSize = New System.Drawing.Size(600, 300)
        Me.grdKDVENDOR.Properties.View = Me.grvKDVENDOR
        Me.grdKDVENDOR.Size = New System.Drawing.Size(375, 20)
        Me.grdKDVENDOR.StyleController = Me.layoutControl
        Me.grdKDVENDOR.TabIndex = 22
        '
        'grvKDVENDOR
        '
        Me.grvKDVENDOR.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn3})
        Me.grvKDVENDOR.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.grvKDVENDOR.Name = "grvKDVENDOR"
        Me.grvKDVENDOR.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.grvKDVENDOR.OptionsView.ShowAutoFilterRow = True
        Me.grvKDVENDOR.OptionsView.ShowGroupPanel = False
        '
        'GridColumn3
        '
        Me.GridColumn3.Caption = "Name Display"
        Me.GridColumn3.FieldName = "NAME_DISPLAY"
        Me.GridColumn3.Name = "GridColumn3"
        Me.GridColumn3.Visible = True
        Me.GridColumn3.VisibleIndex = 0
        '
        'LayoutControl5
        '
        Me.LayoutControl5.Location = New System.Drawing.Point(496, 12)
        Me.LayoutControl5.Name = "LayoutControl5"
        Me.LayoutControl5.Root = Me.LayoutControlGroup5
        Me.LayoutControl5.Size = New System.Drawing.Size(282, 164)
        Me.LayoutControl5.TabIndex = 21
        Me.LayoutControl5.Text = "LayoutControl5"
        '
        'LayoutControlGroup5
        '
        Me.LayoutControlGroup5.CustomizationFormText = "LayoutControlGroup5"
        Me.LayoutControlGroup5.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.LayoutControlGroup5.GroupBordersVisible = False
        Me.LayoutControlGroup5.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.EmptySpaceItem1})
        Me.LayoutControlGroup5.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlGroup5.Name = "LayoutControlGroup5"
        Me.LayoutControlGroup5.Size = New System.Drawing.Size(282, 164)
        Me.LayoutControlGroup5.TextVisible = False
        '
        'EmptySpaceItem1
        '
        Me.EmptySpaceItem1.AllowHotTrack = False
        Me.EmptySpaceItem1.CustomizationFormText = "EmptySpaceItem1"
        Me.EmptySpaceItem1.Location = New System.Drawing.Point(0, 0)
        Me.EmptySpaceItem1.Name = "EmptySpaceItem1"
        Me.EmptySpaceItem1.Size = New System.Drawing.Size(262, 144)
        Me.EmptySpaceItem1.TextSize = New System.Drawing.Size(0, 0)
        '
        'deDATE
        '
        Me.deDATE.EditValue = Nothing
        Me.deDATE.EnterMoveNextControl = True
        Me.deDATE.Location = New System.Drawing.Point(117, 60)
        Me.deDATE.MenuManager = Me.barManager
        Me.deDATE.Name = "deDATE"
        Me.deDATE.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.deDATE.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
        Me.deDATE.Properties.Mask.EditMask = "dd/MM/yyyy"
        Me.deDATE.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.deDATE.Size = New System.Drawing.Size(375, 20)
        Me.deDATE.StyleController = Me.layoutControl
        Me.deDATE.TabIndex = 20
        '
        'tabControl
        '
        Me.tabControl.Location = New System.Drawing.Point(12, 180)
        Me.tabControl.Name = "tabControl"
        Me.tabControl.SelectedTabPage = Me.tab1
        Me.tabControl.Size = New System.Drawing.Size(766, 237)
        Me.tabControl.TabIndex = 18
        Me.tabControl.TabPages.AddRange(New DevExpress.XtraTab.XtraTabPage() {Me.tab1, Me.tab2})
        '
        'tab1
        '
        Me.tab1.Controls.Add(Me.grdDetail)
        Me.tab1.Name = "tab1"
        Me.tab1.Size = New System.Drawing.Size(760, 209)
        Me.tab1.Text = "Detail Information"
        '
        'grdDetail
        '
        Me.grdDetail.ContextMenuStrip = Me.mnuStrip
        Me.grdDetail.DataSource = Me.bindingSource
        Me.grdDetail.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grdDetail.Location = New System.Drawing.Point(0, 0)
        Me.grdDetail.MainView = Me.grvDetail
        Me.grdDetail.MenuManager = Me.barManager
        Me.grdDetail.Name = "grdDetail"
        Me.grdDetail.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.grdKDITEM, Me.grdKDUOM, Me.txtREMARKS, Me.betxtPrice})
        Me.grdDetail.Size = New System.Drawing.Size(760, 209)
        Me.grdDetail.TabIndex = 18
        Me.grdDetail.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.grvDetail})
        '
        'mnuStrip
        '
        Me.mnuStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.DeleteToolStripMenuItem, Me.PrintBarcodeToolStripMenuItem})
        Me.mnuStrip.Name = "mnuStrip"
        Me.mnuStrip.Size = New System.Drawing.Size(146, 48)
        '
        'DeleteToolStripMenuItem
        '
        Me.DeleteToolStripMenuItem.Name = "DeleteToolStripMenuItem"
        Me.DeleteToolStripMenuItem.Size = New System.Drawing.Size(145, 22)
        Me.DeleteToolStripMenuItem.Text = "Delete"
        '
        'PrintBarcodeToolStripMenuItem
        '
        Me.PrintBarcodeToolStripMenuItem.Name = "PrintBarcodeToolStripMenuItem"
        Me.PrintBarcodeToolStripMenuItem.Size = New System.Drawing.Size(145, 22)
        Me.PrintBarcodeToolStripMenuItem.Text = "Print Barcode"
        '
        'bindingSource
        '
        Me.bindingSource.DataSource = GetType(DataAccess.P_PI_D)
        '
        'grvDetail
        '
        Me.grvDetail.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.colKDITEM, Me.colQTY, Me.colKDUOM, Me.colPRICE, Me.colSUBPRICE, Me.colSUBTOTAL, Me.colDISCOUNT_PERSEN, Me.colDISCOUNT, Me.colPPN_PERSEN, Me.colPPN, Me.colGRANDTOTAL, Me.colBATCH, Me.colDATE_EXPIRED, Me.colISUPDATEHARGA, Me.colREMARKS})
        Me.grvDetail.GridControl = Me.grdDetail
        Me.grvDetail.Name = "grvDetail"
        Me.grvDetail.OptionsCustomization.AllowColumnMoving = False
        Me.grvDetail.OptionsCustomization.AllowFilter = False
        Me.grvDetail.OptionsCustomization.AllowGroup = False
        Me.grvDetail.OptionsCustomization.AllowQuickHideColumns = False
        Me.grvDetail.OptionsCustomization.AllowSort = False
        Me.grvDetail.OptionsDetail.EnableMasterViewMode = False
        Me.grvDetail.OptionsFind.AllowFindPanel = False
        Me.grvDetail.OptionsLayout.StoreAllOptions = True
        Me.grvDetail.OptionsLayout.StoreAppearance = True
        Me.grvDetail.OptionsMenu.EnableColumnMenu = False
        Me.grvDetail.OptionsNavigation.AutoFocusNewRow = True
        Me.grvDetail.OptionsNavigation.EnterMoveNextColumn = True
        Me.grvDetail.OptionsView.EnableAppearanceEvenRow = True
        Me.grvDetail.OptionsView.EnableAppearanceOddRow = True
        Me.grvDetail.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Bottom
        Me.grvDetail.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never
        Me.grvDetail.OptionsView.ShowFooter = True
        Me.grvDetail.OptionsView.ShowGroupPanel = False
        '
        'colKDITEM
        '
        Me.colKDITEM.Caption = "Item"
        Me.colKDITEM.ColumnEdit = Me.grdKDITEM
        Me.colKDITEM.FieldName = "KDITEM"
        Me.colKDITEM.Name = "colKDITEM"
        Me.colKDITEM.Visible = True
        Me.colKDITEM.VisibleIndex = 0
        '
        'grdKDITEM
        '
        Me.grdKDITEM.AutoHeight = False
        Me.grdKDITEM.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.grdKDITEM.Name = "grdKDITEM"
        Me.grdKDITEM.NullText = ""
        Me.grdKDITEM.PopupFormMinSize = New System.Drawing.Size(600, 300)
        Me.grdKDITEM.View = Me.grvKDITEM
        '
        'grvKDITEM
        '
        Me.grvKDITEM.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn2, Me.GridColumn1})
        Me.grvKDITEM.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.grvKDITEM.Name = "grvKDITEM"
        Me.grvKDITEM.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.grvKDITEM.OptionsView.ShowAutoFilterRow = True
        Me.grvKDITEM.OptionsView.ShowGroupPanel = False
        '
        'GridColumn2
        '
        Me.GridColumn2.Caption = "Item Name #2"
        Me.GridColumn2.FieldName = "NMITEM2"
        Me.GridColumn2.Name = "GridColumn2"
        Me.GridColumn2.Visible = True
        Me.GridColumn2.VisibleIndex = 0
        '
        'GridColumn1
        '
        Me.GridColumn1.Caption = "Stok"
        Me.GridColumn1.DisplayFormat.FormatString = "{0:n0}"
        Me.GridColumn1.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.GridColumn1.FieldName = "STOK"
        Me.GridColumn1.Name = "GridColumn1"
        Me.GridColumn1.Visible = True
        Me.GridColumn1.VisibleIndex = 1
        '
        'colQTY
        '
        Me.colQTY.AppearanceCell.Options.UseTextOptions = True
        Me.colQTY.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.colQTY.Caption = "Qty"
        Me.colQTY.DisplayFormat.FormatString = "{0:n2}"
        Me.colQTY.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.colQTY.FieldName = "QTY"
        Me.colQTY.Name = "colQTY"
        Me.colQTY.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "QTY", "{0:n2}")})
        Me.colQTY.Visible = True
        Me.colQTY.VisibleIndex = 1
        '
        'colKDUOM
        '
        Me.colKDUOM.Caption = "Unit"
        Me.colKDUOM.ColumnEdit = Me.grdKDUOM
        Me.colKDUOM.FieldName = "KDUOM"
        Me.colKDUOM.Name = "colKDUOM"
        Me.colKDUOM.OptionsColumn.TabStop = False
        Me.colKDUOM.Visible = True
        Me.colKDUOM.VisibleIndex = 2
        '
        'grdKDUOM
        '
        Me.grdKDUOM.AutoHeight = False
        Me.grdKDUOM.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.grdKDUOM.Name = "grdKDUOM"
        Me.grdKDUOM.NullText = ""
        Me.grdKDUOM.PopupFormMinSize = New System.Drawing.Size(600, 300)
        Me.grdKDUOM.View = Me.grvKDUOM
        '
        'grvKDUOM
        '
        Me.grvKDUOM.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn4})
        Me.grvKDUOM.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.grvKDUOM.Name = "grvKDUOM"
        Me.grvKDUOM.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.grvKDUOM.OptionsView.ShowAutoFilterRow = True
        Me.grvKDUOM.OptionsView.ShowGroupPanel = False
        '
        'GridColumn4
        '
        Me.GridColumn4.Caption = "Description"
        Me.GridColumn4.FieldName = "MEMO"
        Me.GridColumn4.Name = "GridColumn4"
        Me.GridColumn4.Visible = True
        Me.GridColumn4.VisibleIndex = 0
        '
        'colPRICE
        '
        Me.colPRICE.Caption = "Price"
        Me.colPRICE.ColumnEdit = Me.betxtPrice
        Me.colPRICE.DisplayFormat.FormatString = "{0:n2}"
        Me.colPRICE.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.colPRICE.FieldName = "PRICE"
        Me.colPRICE.Name = "colPRICE"
        Me.colPRICE.Visible = True
        Me.colPRICE.VisibleIndex = 3
        '
        'betxtPrice
        '
        Me.betxtPrice.AutoHeight = False
        Me.betxtPrice.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
        Me.betxtPrice.Name = "betxtPrice"
        '
        'colSUBPRICE
        '
        Me.colSUBPRICE.Caption = "Sub Harga"
        Me.colSUBPRICE.DisplayFormat.FormatString = "{0:n2}"
        Me.colSUBPRICE.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.colSUBPRICE.FieldName = "SUBPRICE"
        Me.colSUBPRICE.Name = "colSUBPRICE"
        Me.colSUBPRICE.Visible = True
        Me.colSUBPRICE.VisibleIndex = 4
        '
        'colSUBTOTAL
        '
        Me.colSUBTOTAL.Caption = "Sub Total"
        Me.colSUBTOTAL.DisplayFormat.FormatString = "{0:n2}"
        Me.colSUBTOTAL.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.colSUBTOTAL.FieldName = "SUBTOTAL"
        Me.colSUBTOTAL.Name = "colSUBTOTAL"
        Me.colSUBTOTAL.OptionsColumn.AllowEdit = False
        Me.colSUBTOTAL.OptionsColumn.AllowFocus = False
        Me.colSUBTOTAL.OptionsColumn.ReadOnly = True
        Me.colSUBTOTAL.OptionsColumn.TabStop = False
        Me.colSUBTOTAL.Visible = True
        Me.colSUBTOTAL.VisibleIndex = 5
        '
        'colDISCOUNT_PERSEN
        '
        Me.colDISCOUNT_PERSEN.Caption = "Discount (%)"
        Me.colDISCOUNT_PERSEN.DisplayFormat.FormatString = "{0:n2}"
        Me.colDISCOUNT_PERSEN.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.colDISCOUNT_PERSEN.FieldName = "DISCOUNT_PERSEN"
        Me.colDISCOUNT_PERSEN.Name = "colDISCOUNT_PERSEN"
        Me.colDISCOUNT_PERSEN.Visible = True
        Me.colDISCOUNT_PERSEN.VisibleIndex = 6
        '
        'colDISCOUNT
        '
        Me.colDISCOUNT.Caption = "Discount"
        Me.colDISCOUNT.DisplayFormat.FormatString = "{0:n2}"
        Me.colDISCOUNT.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.colDISCOUNT.FieldName = "DISCOUNT"
        Me.colDISCOUNT.Name = "colDISCOUNT"
        '
        'colPPN_PERSEN
        '
        Me.colPPN_PERSEN.Caption = "PPn (%)"
        Me.colPPN_PERSEN.DisplayFormat.FormatString = "{0:n2}"
        Me.colPPN_PERSEN.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.colPPN_PERSEN.FieldName = "PPN_PERSEN"
        Me.colPPN_PERSEN.Name = "colPPN_PERSEN"
        Me.colPPN_PERSEN.Visible = True
        Me.colPPN_PERSEN.VisibleIndex = 7
        '
        'colPPN
        '
        Me.colPPN.Caption = "PPn"
        Me.colPPN.DisplayFormat.FormatString = "{0:n2}"
        Me.colPPN.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.colPPN.FieldName = "PPN"
        Me.colPPN.Name = "colPPN"
        '
        'colGRANDTOTAL
        '
        Me.colGRANDTOTAL.Caption = "Grand Total"
        Me.colGRANDTOTAL.DisplayFormat.FormatString = "{0:n2}"
        Me.colGRANDTOTAL.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.colGRANDTOTAL.FieldName = "GRANDTOTAL"
        Me.colGRANDTOTAL.Name = "colGRANDTOTAL"
        Me.colGRANDTOTAL.OptionsColumn.AllowEdit = False
        Me.colGRANDTOTAL.OptionsColumn.AllowFocus = False
        Me.colGRANDTOTAL.OptionsColumn.ReadOnly = True
        Me.colGRANDTOTAL.OptionsColumn.TabStop = False
        Me.colGRANDTOTAL.Visible = True
        Me.colGRANDTOTAL.VisibleIndex = 8
        '
        'colBATCH
        '
        Me.colBATCH.Caption = "Batch"
        Me.colBATCH.FieldName = "BATCH"
        Me.colBATCH.Name = "colBATCH"
        Me.colBATCH.Visible = True
        Me.colBATCH.VisibleIndex = 9
        '
        'colDATE_EXPIRED
        '
        Me.colDATE_EXPIRED.Caption = "Expire"
        Me.colDATE_EXPIRED.DisplayFormat.FormatString = "dd-MM-yyyy"
        Me.colDATE_EXPIRED.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.colDATE_EXPIRED.FieldName = "DATE_EXPIRED"
        Me.colDATE_EXPIRED.Name = "colDATE_EXPIRED"
        Me.colDATE_EXPIRED.Visible = True
        Me.colDATE_EXPIRED.VisibleIndex = 10
        '
        'colISUPDATEHARGA
        '
        Me.colISUPDATEHARGA.Caption = "Update"
        Me.colISUPDATEHARGA.FieldName = "ISUPDATEHARGA"
        Me.colISUPDATEHARGA.Name = "colISUPDATEHARGA"
        Me.colISUPDATEHARGA.Visible = True
        Me.colISUPDATEHARGA.VisibleIndex = 11
        '
        'colREMARKS
        '
        Me.colREMARKS.AppearanceCell.Options.UseTextOptions = True
        Me.colREMARKS.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.colREMARKS.Caption = "Remarks"
        Me.colREMARKS.ColumnEdit = Me.txtREMARKS
        Me.colREMARKS.FieldName = "REMARKS"
        Me.colREMARKS.Name = "colREMARKS"
        Me.colREMARKS.Visible = True
        Me.colREMARKS.VisibleIndex = 12
        '
        'txtREMARKS
        '
        Me.txtREMARKS.AutoHeight = False
        Me.txtREMARKS.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.txtREMARKS.Name = "txtREMARKS"
        '
        'tab2
        '
        Me.tab2.Controls.Add(Me.LayoutControl1)
        Me.tab2.Name = "tab2"
        Me.tab2.Size = New System.Drawing.Size(760, 209)
        Me.tab2.Text = "Memo Information"
        '
        'LayoutControl1
        '
        Me.LayoutControl1.Controls.Add(Me.txtMEMO)
        Me.LayoutControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LayoutControl1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControl1.Name = "LayoutControl1"
        Me.LayoutControl1.Root = Me.s
        Me.LayoutControl1.Size = New System.Drawing.Size(760, 209)
        Me.LayoutControl1.TabIndex = 0
        Me.LayoutControl1.Text = "LayoutControl1"
        '
        'txtMEMO
        '
        Me.txtMEMO.EnterMoveNextControl = True
        Me.txtMEMO.Location = New System.Drawing.Point(12, 12)
        Me.txtMEMO.MenuManager = Me.barManager
        Me.txtMEMO.Name = "txtMEMO"
        Me.txtMEMO.Size = New System.Drawing.Size(736, 185)
        Me.txtMEMO.StyleController = Me.LayoutControl1
        Me.txtMEMO.TabIndex = 4
        '
        's
        '
        Me.s.CustomizationFormText = "s"
        Me.s.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.s.GroupBordersVisible = False
        Me.s.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlItem7})
        Me.s.Location = New System.Drawing.Point(0, 0)
        Me.s.Name = "s"
        Me.s.Size = New System.Drawing.Size(760, 209)
        Me.s.TextVisible = False
        '
        'LayoutControlItem7
        '
        Me.LayoutControlItem7.Control = Me.txtMEMO
        Me.LayoutControlItem7.CustomizationFormText = "LayoutControlItem7"
        Me.LayoutControlItem7.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlItem7.Name = "LayoutControlItem7"
        Me.LayoutControlItem7.Size = New System.Drawing.Size(740, 189)
        Me.LayoutControlItem7.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem7.TextVisible = False
        '
        'txtKDPI
        '
        Me.txtKDPI.EditValue = ""
        Me.txtKDPI.EnterMoveNextControl = True
        Me.txtKDPI.Location = New System.Drawing.Point(117, 12)
        Me.txtKDPI.Name = "txtKDPI"
        Me.txtKDPI.Properties.ReadOnly = True
        Me.txtKDPI.Size = New System.Drawing.Size(375, 20)
        Me.txtKDPI.StyleController = Me.layoutControl
        Me.txtKDPI.TabIndex = 9
        '
        'LayoutControlGroup1
        '
        Me.LayoutControlGroup1.CustomizationFormText = "LayoutControlGroup1"
        Me.LayoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.LayoutControlGroup1.GroupBordersVisible = False
        Me.LayoutControlGroup1.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.lKDPI, Me.LayoutControlItem5, Me.lDATE, Me.LayoutControlItem3, Me.lKDVENDOR, Me.EmptySpaceItem2, Me.lSUBTOTAL, Me.lDISCOUNT, Me.lTAX, Me.lGRANDTOTAL, Me.lKDWAREHOUSE, Me.lDATEDUE, Me.lDATE_CREATED, Me.lMATERAI, Me.lNOFAKTUR, Me.lDISCOUNT_PERSEN, Me.lPPN_PERSEN})
        Me.LayoutControlGroup1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlGroup1.Name = "Root"
        Me.LayoutControlGroup1.Size = New System.Drawing.Size(790, 549)
        Me.LayoutControlGroup1.TextVisible = False
        '
        'lKDPI
        '
        Me.lKDPI.AppearanceItemCaption.Options.UseTextOptions = True
        Me.lKDPI.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.lKDPI.Control = Me.txtKDPI
        Me.lKDPI.CustomizationFormText = "Display Name * :"
        Me.lKDPI.Location = New System.Drawing.Point(0, 0)
        Me.lKDPI.Name = "lKDPI"
        Me.lKDPI.Size = New System.Drawing.Size(484, 24)
        Me.lKDPI.Text = "Number * :"
        Me.lKDPI.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.lKDPI.TextSize = New System.Drawing.Size(100, 20)
        Me.lKDPI.TextToControlDistance = 5
        '
        'LayoutControlItem5
        '
        Me.LayoutControlItem5.Control = Me.tabControl
        Me.LayoutControlItem5.CustomizationFormText = "LayoutControlItem5"
        Me.LayoutControlItem5.Location = New System.Drawing.Point(0, 168)
        Me.LayoutControlItem5.Name = "LayoutControlItem5"
        Me.LayoutControlItem5.Size = New System.Drawing.Size(770, 241)
        Me.LayoutControlItem5.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem5.TextVisible = False
        '
        'lDATE
        '
        Me.lDATE.AppearanceItemCaption.Options.UseTextOptions = True
        Me.lDATE.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.lDATE.Control = Me.deDATE
        Me.lDATE.CustomizationFormText = "Date :"
        Me.lDATE.Location = New System.Drawing.Point(0, 48)
        Me.lDATE.Name = "lDATE"
        Me.lDATE.Size = New System.Drawing.Size(484, 24)
        Me.lDATE.Text = "Date :"
        Me.lDATE.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.lDATE.TextSize = New System.Drawing.Size(100, 20)
        Me.lDATE.TextToControlDistance = 5
        '
        'LayoutControlItem3
        '
        Me.LayoutControlItem3.Control = Me.LayoutControl5
        Me.LayoutControlItem3.CustomizationFormText = "LayoutControlItem3"
        Me.LayoutControlItem3.Location = New System.Drawing.Point(484, 0)
        Me.LayoutControlItem3.Name = "LayoutControlItem3"
        Me.LayoutControlItem3.Size = New System.Drawing.Size(286, 168)
        Me.LayoutControlItem3.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem3.TextVisible = False
        '
        'lKDVENDOR
        '
        Me.lKDVENDOR.AppearanceItemCaption.Options.UseTextOptions = True
        Me.lKDVENDOR.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.lKDVENDOR.Control = Me.grdKDVENDOR
        Me.lKDVENDOR.CustomizationFormText = "Vendor * :"
        Me.lKDVENDOR.Location = New System.Drawing.Point(0, 120)
        Me.lKDVENDOR.Name = "lKDVENDOR"
        Me.lKDVENDOR.Size = New System.Drawing.Size(484, 24)
        Me.lKDVENDOR.Text = "Vendor * :"
        Me.lKDVENDOR.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.lKDVENDOR.TextSize = New System.Drawing.Size(100, 20)
        Me.lKDVENDOR.TextToControlDistance = 5
        '
        'EmptySpaceItem2
        '
        Me.EmptySpaceItem2.AllowHotTrack = False
        Me.EmptySpaceItem2.CustomizationFormText = "EmptySpaceItem2"
        Me.EmptySpaceItem2.Location = New System.Drawing.Point(0, 409)
        Me.EmptySpaceItem2.Name = "EmptySpaceItem2"
        Me.EmptySpaceItem2.Size = New System.Drawing.Size(458, 120)
        Me.EmptySpaceItem2.TextSize = New System.Drawing.Size(0, 0)
        '
        'lSUBTOTAL
        '
        Me.lSUBTOTAL.AppearanceItemCaption.Options.UseTextOptions = True
        Me.lSUBTOTAL.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.lSUBTOTAL.Control = Me.txtSUBTOTAL
        Me.lSUBTOTAL.CustomizationFormText = "Sub Total :"
        Me.lSUBTOTAL.Location = New System.Drawing.Point(458, 409)
        Me.lSUBTOTAL.Name = "lSUBTOTAL"
        Me.lSUBTOTAL.Size = New System.Drawing.Size(312, 24)
        Me.lSUBTOTAL.Text = "Sub Total :"
        Me.lSUBTOTAL.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.lSUBTOTAL.TextSize = New System.Drawing.Size(100, 20)
        Me.lSUBTOTAL.TextToControlDistance = 5
        '
        'lDISCOUNT
        '
        Me.lDISCOUNT.AppearanceItemCaption.Options.UseTextOptions = True
        Me.lDISCOUNT.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.lDISCOUNT.Control = Me.txtDISCOUNT
        Me.lDISCOUNT.CustomizationFormText = "Discount :"
        Me.lDISCOUNT.Location = New System.Drawing.Point(642, 433)
        Me.lDISCOUNT.Name = "lDISCOUNT"
        Me.lDISCOUNT.Size = New System.Drawing.Size(128, 24)
        Me.lDISCOUNT.Text = "Discount :"
        Me.lDISCOUNT.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.lDISCOUNT.TextSize = New System.Drawing.Size(0, 0)
        Me.lDISCOUNT.TextToControlDistance = 0
        Me.lDISCOUNT.TextVisible = False
        '
        'lTAX
        '
        Me.lTAX.AppearanceItemCaption.Options.UseTextOptions = True
        Me.lTAX.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.lTAX.Control = Me.txtPPN
        Me.lTAX.CustomizationFormText = "Tax :"
        Me.lTAX.Location = New System.Drawing.Point(642, 457)
        Me.lTAX.Name = "lTAX"
        Me.lTAX.Size = New System.Drawing.Size(128, 24)
        Me.lTAX.Text = "Tax :"
        Me.lTAX.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.lTAX.TextSize = New System.Drawing.Size(0, 0)
        Me.lTAX.TextToControlDistance = 0
        Me.lTAX.TextVisible = False
        '
        'lGRANDTOTAL
        '
        Me.lGRANDTOTAL.AppearanceItemCaption.Options.UseTextOptions = True
        Me.lGRANDTOTAL.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.lGRANDTOTAL.Control = Me.txtGRANDTOTAL
        Me.lGRANDTOTAL.CustomizationFormText = "Grand Total :"
        Me.lGRANDTOTAL.Location = New System.Drawing.Point(458, 505)
        Me.lGRANDTOTAL.Name = "lGRANDTOTAL"
        Me.lGRANDTOTAL.Size = New System.Drawing.Size(312, 24)
        Me.lGRANDTOTAL.Text = "Grand Total :"
        Me.lGRANDTOTAL.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.lGRANDTOTAL.TextSize = New System.Drawing.Size(100, 20)
        Me.lGRANDTOTAL.TextToControlDistance = 5
        '
        'lKDWAREHOUSE
        '
        Me.lKDWAREHOUSE.AppearanceItemCaption.Options.UseTextOptions = True
        Me.lKDWAREHOUSE.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.lKDWAREHOUSE.Control = Me.grdKDWAREHOUSE
        Me.lKDWAREHOUSE.CustomizationFormText = "Warehouse * :"
        Me.lKDWAREHOUSE.Location = New System.Drawing.Point(0, 144)
        Me.lKDWAREHOUSE.Name = "lKDWAREHOUSE"
        Me.lKDWAREHOUSE.Size = New System.Drawing.Size(484, 24)
        Me.lKDWAREHOUSE.Text = "Warehouse * :"
        Me.lKDWAREHOUSE.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.lKDWAREHOUSE.TextSize = New System.Drawing.Size(100, 20)
        Me.lKDWAREHOUSE.TextToControlDistance = 5
        '
        'lDATEDUE
        '
        Me.lDATEDUE.AppearanceItemCaption.Options.UseTextOptions = True
        Me.lDATEDUE.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.lDATEDUE.Control = Me.deDATEDUE
        Me.lDATEDUE.CustomizationFormText = "Due Date :"
        Me.lDATEDUE.Location = New System.Drawing.Point(0, 96)
        Me.lDATEDUE.Name = "lDATEDUE"
        Me.lDATEDUE.Size = New System.Drawing.Size(484, 24)
        Me.lDATEDUE.Text = "Due Date :"
        Me.lDATEDUE.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.lDATEDUE.TextSize = New System.Drawing.Size(100, 20)
        Me.lDATEDUE.TextToControlDistance = 5
        '
        'lDATE_CREATED
        '
        Me.lDATE_CREATED.AppearanceItemCaption.Options.UseTextOptions = True
        Me.lDATE_CREATED.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.lDATE_CREATED.Control = Me.deDATECREATED
        Me.lDATE_CREATED.Location = New System.Drawing.Point(0, 24)
        Me.lDATE_CREATED.Name = "lDATE_CREATED"
        Me.lDATE_CREATED.Size = New System.Drawing.Size(484, 24)
        Me.lDATE_CREATED.Text = "Tanggal Entri :"
        Me.lDATE_CREATED.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.lDATE_CREATED.TextSize = New System.Drawing.Size(100, 20)
        Me.lDATE_CREATED.TextToControlDistance = 5
        '
        'lMATERAI
        '
        Me.lMATERAI.AppearanceItemCaption.Options.UseTextOptions = True
        Me.lMATERAI.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.lMATERAI.Control = Me.txtMATERAI
        Me.lMATERAI.Location = New System.Drawing.Point(458, 481)
        Me.lMATERAI.Name = "lMATERAI"
        Me.lMATERAI.Size = New System.Drawing.Size(312, 24)
        Me.lMATERAI.Text = "Materai :"
        Me.lMATERAI.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.lMATERAI.TextSize = New System.Drawing.Size(100, 13)
        Me.lMATERAI.TextToControlDistance = 5
        '
        'lNOFAKTUR
        '
        Me.lNOFAKTUR.AppearanceItemCaption.Options.UseTextOptions = True
        Me.lNOFAKTUR.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.lNOFAKTUR.Control = Me.txtNOFAKTUR
        Me.lNOFAKTUR.Location = New System.Drawing.Point(0, 72)
        Me.lNOFAKTUR.Name = "lNOFAKTUR"
        Me.lNOFAKTUR.Size = New System.Drawing.Size(484, 24)
        Me.lNOFAKTUR.Text = "No Faktur :"
        Me.lNOFAKTUR.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.lNOFAKTUR.TextSize = New System.Drawing.Size(100, 20)
        Me.lNOFAKTUR.TextToControlDistance = 5
        '
        'lDISCOUNT_PERSEN
        '
        Me.lDISCOUNT_PERSEN.AppearanceItemCaption.Options.UseTextOptions = True
        Me.lDISCOUNT_PERSEN.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.lDISCOUNT_PERSEN.Control = Me.txtDISCOUNT_PERSEN
        Me.lDISCOUNT_PERSEN.Location = New System.Drawing.Point(458, 433)
        Me.lDISCOUNT_PERSEN.Name = "lDISCOUNT_PERSEN"
        Me.lDISCOUNT_PERSEN.Size = New System.Drawing.Size(184, 24)
        Me.lDISCOUNT_PERSEN.Text = "Diskon :"
        Me.lDISCOUNT_PERSEN.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.lDISCOUNT_PERSEN.TextSize = New System.Drawing.Size(100, 13)
        Me.lDISCOUNT_PERSEN.TextToControlDistance = 5
        '
        'lPPN_PERSEN
        '
        Me.lPPN_PERSEN.AppearanceItemCaption.Options.UseTextOptions = True
        Me.lPPN_PERSEN.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.lPPN_PERSEN.Control = Me.txtPPN_PERSEN
        Me.lPPN_PERSEN.Location = New System.Drawing.Point(458, 457)
        Me.lPPN_PERSEN.Name = "lPPN_PERSEN"
        Me.lPPN_PERSEN.Size = New System.Drawing.Size(184, 24)
        Me.lPPN_PERSEN.Tag = ""
        Me.lPPN_PERSEN.Text = "PPn :"
        Me.lPPN_PERSEN.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.lPPN_PERSEN.TextSize = New System.Drawing.Size(100, 13)
        Me.lPPN_PERSEN.TextToControlDistance = 5
        '
        'GridColumn6
        '
        Me.GridColumn6.Caption = "Number"
        Me.GridColumn6.FieldName = "KDSO"
        Me.GridColumn6.Name = "GridColumn6"
        Me.GridColumn6.Visible = True
        Me.GridColumn6.VisibleIndex = 0
        '
        'frmPurchaseInvoice
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
        Me.Name = "frmPurchaseInvoice"
        Me.ShowIcon = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        CType(Me.layoutControl, System.ComponentModel.ISupportInitialize).EndInit()
        Me.layoutControl.ResumeLayout(False)
        CType(Me.txtNOFAKTUR.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtMATERAI.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.barManager, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.progressBarSave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.progressSave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtPPN_PERSEN.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDISCOUNT_PERSEN.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.deDATECREATED.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.deDATECREATED.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.deDATEDUE.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.deDATEDUE.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdKDWAREHOUSE.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grvKDWAREHOUSE, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtGRANDTOTAL.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtPPN.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDISCOUNT.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtSUBTOTAL.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdKDVENDOR.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grvKDVENDOR, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControl5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.EmptySpaceItem1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.deDATE.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.deDATE.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.tabControl, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tabControl.ResumeLayout(False)
        Me.tab1.ResumeLayout(False)
        CType(Me.grdDetail, System.ComponentModel.ISupportInitialize).EndInit()
        Me.mnuStrip.ResumeLayout(False)
        CType(Me.bindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grvDetail, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdKDITEM, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grvKDITEM, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdKDUOM, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grvKDUOM, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.betxtPrice, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtREMARKS, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tab2.ResumeLayout(False)
        CType(Me.LayoutControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.LayoutControl1.ResumeLayout(False)
        CType(Me.txtMEMO.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.s, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem7, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtKDPI.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lKDPI, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lDATE, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lKDVENDOR, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.EmptySpaceItem2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lSUBTOTAL, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lDISCOUNT, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lTAX, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lGRANDTOTAL, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lKDWAREHOUSE, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lDATEDUE, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lDATE_CREATED, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lMATERAI, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lNOFAKTUR, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lDISCOUNT_PERSEN, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lPPN_PERSEN, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents layoutControl As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents LayoutControlGroup1 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents lKDPI As DevExpress.XtraLayout.LayoutControlItem
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
    Friend WithEvents txtKDPI As DevExpress.XtraEditors.TextEdit
    Friend WithEvents bindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents tabControl As DevExpress.XtraTab.XtraTabControl
    Friend WithEvents tab1 As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents LayoutControlItem5 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents grdDetail As DevExpress.XtraGrid.GridControl
    Friend WithEvents grvDetail As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents deDATE As DevExpress.XtraEditors.DateEdit
    Friend WithEvents lDATE As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControl5 As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents LayoutControlGroup5 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents LayoutControlItem3 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents tab2 As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents LayoutControl1 As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents s As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents txtMEMO As DevExpress.XtraEditors.MemoEdit
    Friend WithEvents LayoutControlItem7 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents colKDITEM As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colQTY As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colREMARKS As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents grdKDITEM As DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit
    Friend WithEvents grvKDITEM As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents grdKDUOM As DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit
    Friend WithEvents grvKDUOM As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridColumn4 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents mnuStrip As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents DeleteToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents EmptySpaceItem1 As DevExpress.XtraLayout.EmptySpaceItem
    Friend WithEvents colKDUOM As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents txtREMARKS As DevExpress.XtraEditors.Repository.RepositoryItemMemoExEdit
    Friend WithEvents grdKDVENDOR As DevExpress.XtraEditors.GridLookUpEdit
    Friend WithEvents grvKDVENDOR As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents lKDVENDOR As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents GridColumn3 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn2 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn6 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents EmptySpaceItem2 As DevExpress.XtraLayout.EmptySpaceItem
    Friend WithEvents txtGRANDTOTAL As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txtPPN As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txtDISCOUNT As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txtSUBTOTAL As DevExpress.XtraEditors.TextEdit
    Friend WithEvents lSUBTOTAL As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents lDISCOUNT As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents lTAX As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents lGRANDTOTAL As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents colPRICE As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colSUBTOTAL As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colDISCOUNT As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colGRANDTOTAL As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents grdKDWAREHOUSE As DevExpress.XtraEditors.GridLookUpEdit
    Friend WithEvents grvKDWAREHOUSE As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridColumn9 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents lKDWAREHOUSE As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents deDATEDUE As DevExpress.XtraEditors.DateEdit
    Friend WithEvents lDATEDUE As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents PrintBarcodeToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents btnHistory As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents GridColumn1 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents deDATECREATED As DevExpress.XtraEditors.DateEdit
    Friend WithEvents lDATE_CREATED As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents txtMATERAI As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txtPPN_PERSEN As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txtDISCOUNT_PERSEN As DevExpress.XtraEditors.TextEdit
    Friend WithEvents lDISCOUNT_PERSEN As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents lPPN_PERSEN As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents lMATERAI As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents txtNOFAKTUR As DevExpress.XtraEditors.TextEdit
    Friend WithEvents lNOFAKTUR As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents colSUBPRICE As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colDISCOUNT_PERSEN As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colBATCH As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colDATE_EXPIRED As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colISUPDATEHARGA As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colPPN_PERSEN As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colPPN As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents betxtPrice As DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit
End Class
