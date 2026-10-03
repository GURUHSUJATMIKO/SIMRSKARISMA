<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmBillingFarmasi
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
        Me.grdKDDOCTOR_H = New DevExpress.XtraEditors.GridLookUpEdit()
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
        Me.GridView1 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn10 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.grdKDWAREHOUSE = New DevExpress.XtraEditors.GridLookUpEdit()
        Me.grvKDWAREHOUSE = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn9 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.txtKDPENDAFTARAN = New DevExpress.XtraEditors.TextEdit()
        Me.grdKDPENJAMIN = New DevExpress.XtraEditors.GridLookUpEdit()
        Me.grvKDPENJAMIN = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn8 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.deDATE = New DevExpress.XtraEditors.DateEdit()
        Me.txtGRANDTOTAL = New DevExpress.XtraEditors.TextEdit()
        Me.grdCARIKDREGAWAL = New DevExpress.XtraEditors.GridLookUpEdit()
        Me.grvCARIKDPENDAFTARAN_AWAL = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.cboCARI = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.txtCARI = New DevExpress.XtraEditors.TextEdit()
        Me.txtKDKUNJUNGAN = New DevExpress.XtraEditors.TextEdit()
        Me.tabControl = New DevExpress.XtraTab.XtraTabControl()
        Me.tab1 = New DevExpress.XtraTab.XtraTabPage()
        Me.grdDetail = New DevExpress.XtraGrid.GridControl()
        Me.mnuStrip = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.DeleteToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.BindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.grvDetail = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.colKDITEM = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.grdKDITEM = New DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit()
        Me.grvKDITEM = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn2 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn7 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colKDUOM = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.grdKDUOM = New DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit()
        Me.grvKDUOM = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn4 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colKDSIGNA = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.grdKDSIGNA = New DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit()
        Me.grvKDSIGNA = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn3 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colKDCARAPAKAI = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.grdKDCARAPAKAI = New DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit()
        Me.grvKDCARAPAKAI = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn12 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colKDITEM_L2 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.grdKDITEM_L2 = New DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit()
        Me.grvKDITEM_L2 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn11 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colHARI = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colISPROLANIS = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.chkISCHEKED = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.colQTY = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colPRICE = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colPPN_PERSEN = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colPPN = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colSUBTOTAL = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colISTUSLAH = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colTUSLAH = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colGRANDTOTAL = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colDATE_EXPIRE = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.deDATE_EXPIRE = New DevExpress.XtraEditors.Repository.RepositoryItemDateEdit()
        Me.colREMARKS = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.txtREMARKS = New DevExpress.XtraEditors.Repository.RepositoryItemMemoExEdit()
        Me.colKDITEM_L1 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colKDITEM_L3 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colKDITEM_L4 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colKDITEM_L5 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colKDITEM_L6 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.tab2 = New DevExpress.XtraTab.XtraTabPage()
        Me.txtMEMO = New DevExpress.XtraEditors.MemoEdit()
        Me.txtKDBILLING = New DevExpress.XtraEditors.TextEdit()
        Me.LayoutControlGroup1 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.LayoutControlItem5 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.lKDKUNJUNGAN_POLI = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlGroup2 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.LayoutControlItem1 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem2 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem4 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.EmptySpaceItem3 = New DevExpress.XtraLayout.EmptySpaceItem()
        Me.lGRANDTOTAL = New DevExpress.XtraLayout.LayoutControlItem()
        Me.EmptySpaceItem2 = New DevExpress.XtraLayout.EmptySpaceItem()
        Me.lDATE = New DevExpress.XtraLayout.LayoutControlItem()
        Me.lKDBILLING_POLI = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem8 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem9 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem7 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.EmptySpaceItem1 = New DevExpress.XtraLayout.EmptySpaceItem()
        Me.LayoutControlItem3 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.GridColumn6 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.txtSUBTOTAL = New DevExpress.XtraEditors.TextEdit()
        Me.LayoutControlItem10 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.txtTUSLAH = New DevExpress.XtraEditors.TextEdit()
        Me.lTUSLAH = New DevExpress.XtraLayout.LayoutControlItem()
        Me.txtPPN = New DevExpress.XtraEditors.TextEdit()
        Me.lPPN = New DevExpress.XtraLayout.LayoutControlItem()
        Me.btnPending = New DevExpress.XtraBars.BarButtonItem()
        CType(Me.layoutControl, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.layoutControl.SuspendLayout()
        CType(Me.grdKDDOCTOR_H.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.barManager, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.progressBarSave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.progressSave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdKDWAREHOUSE.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grvKDWAREHOUSE, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtKDPENDAFTARAN.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdKDPENJAMIN.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grvKDPENJAMIN, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.deDATE.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.deDATE.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtGRANDTOTAL.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdCARIKDREGAWAL.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grvCARIKDPENDAFTARAN_AWAL, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cboCARI.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCARI.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtKDKUNJUNGAN.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.tabControl, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tabControl.SuspendLayout()
        Me.tab1.SuspendLayout()
        CType(Me.grdDetail, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.mnuStrip.SuspendLayout()
        CType(Me.BindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grvDetail, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdKDITEM, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grvKDITEM, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdKDUOM, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grvKDUOM, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdKDSIGNA, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grvKDSIGNA, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdKDCARAPAKAI, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grvKDCARAPAKAI, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdKDITEM_L2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grvKDITEM_L2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkISCHEKED, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.deDATE_EXPIRE, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.deDATE_EXPIRE.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtREMARKS, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tab2.SuspendLayout()
        CType(Me.txtMEMO.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtKDBILLING.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lKDKUNJUNGAN_POLI, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.EmptySpaceItem3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lGRANDTOTAL, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.EmptySpaceItem2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lDATE, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lKDBILLING_POLI, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem8, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem9, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem7, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.EmptySpaceItem1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtSUBTOTAL.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem10, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtTUSLAH.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lTUSLAH, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtPPN.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lPPN, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'layoutControl
        '
        Me.layoutControl.Controls.Add(Me.txtPPN)
        Me.layoutControl.Controls.Add(Me.txtTUSLAH)
        Me.layoutControl.Controls.Add(Me.txtSUBTOTAL)
        Me.layoutControl.Controls.Add(Me.grdKDDOCTOR_H)
        Me.layoutControl.Controls.Add(Me.grdKDWAREHOUSE)
        Me.layoutControl.Controls.Add(Me.txtKDPENDAFTARAN)
        Me.layoutControl.Controls.Add(Me.grdKDPENJAMIN)
        Me.layoutControl.Controls.Add(Me.deDATE)
        Me.layoutControl.Controls.Add(Me.txtGRANDTOTAL)
        Me.layoutControl.Controls.Add(Me.grdCARIKDREGAWAL)
        Me.layoutControl.Controls.Add(Me.cboCARI)
        Me.layoutControl.Controls.Add(Me.txtCARI)
        Me.layoutControl.Controls.Add(Me.txtKDKUNJUNGAN)
        Me.layoutControl.Controls.Add(Me.tabControl)
        Me.layoutControl.Controls.Add(Me.txtKDBILLING)
        Me.layoutControl.Dock = System.Windows.Forms.DockStyle.Fill
        Me.layoutControl.Location = New System.Drawing.Point(0, 0)
        Me.layoutControl.Name = "layoutControl"
        Me.layoutControl.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = New System.Drawing.Rectangle(652, 156, 250, 350)
        Me.layoutControl.Root = Me.LayoutControlGroup1
        Me.layoutControl.Size = New System.Drawing.Size(790, 549)
        Me.layoutControl.TabIndex = 0
        Me.layoutControl.Text = "LayoutControl1"
        '
        'grdKDDOCTOR_H
        '
        Me.grdKDDOCTOR_H.EnterMoveNextControl = True
        Me.grdKDDOCTOR_H.Location = New System.Drawing.Point(167, 222)
        Me.grdKDDOCTOR_H.MenuManager = Me.barManager
        Me.grdKDDOCTOR_H.Name = "grdKDDOCTOR_H"
        Me.grdKDDOCTOR_H.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.grdKDDOCTOR_H.Properties.NullText = ""
        Me.grdKDDOCTOR_H.Properties.PopupFormMinSize = New System.Drawing.Size(600, 300)
        Me.grdKDDOCTOR_H.Properties.ReadOnly = True
        Me.grdKDDOCTOR_H.Properties.View = Me.GridView1
        Me.grdKDDOCTOR_H.Size = New System.Drawing.Size(282, 20)
        Me.grdKDDOCTOR_H.StyleController = Me.layoutControl
        Me.grdKDDOCTOR_H.TabIndex = 37
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
        Me.barManager.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.btnSaveNew, Me.btnClose, Me.btnSaveClose, Me.btnPending})
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
        Me.barTop.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.btnSaveNew), New DevExpress.XtraBars.LinkPersistInfo(Me.btnSaveClose), New DevExpress.XtraBars.LinkPersistInfo(Me.btnPending), New DevExpress.XtraBars.LinkPersistInfo(Me.btnClose)})
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
        'GridView1
        '
        Me.GridView1.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn10})
        Me.GridView1.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.GridView1.Name = "GridView1"
        Me.GridView1.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.GridView1.OptionsView.ShowAutoFilterRow = True
        Me.GridView1.OptionsView.ShowGroupPanel = False
        '
        'GridColumn10
        '
        Me.GridColumn10.Caption = "Display Name"
        Me.GridColumn10.FieldName = "MEMO"
        Me.GridColumn10.Name = "GridColumn10"
        Me.GridColumn10.Visible = True
        Me.GridColumn10.VisibleIndex = 0
        '
        'grdKDWAREHOUSE
        '
        Me.grdKDWAREHOUSE.EnterMoveNextControl = True
        Me.grdKDWAREHOUSE.Location = New System.Drawing.Point(167, 246)
        Me.grdKDWAREHOUSE.MenuManager = Me.barManager
        Me.grdKDWAREHOUSE.Name = "grdKDWAREHOUSE"
        Me.grdKDWAREHOUSE.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.grdKDWAREHOUSE.Properties.NullText = ""
        Me.grdKDWAREHOUSE.Properties.PopupFormMinSize = New System.Drawing.Size(600, 300)
        Me.grdKDWAREHOUSE.Properties.View = Me.grvKDWAREHOUSE
        Me.grdKDWAREHOUSE.Size = New System.Drawing.Size(282, 20)
        Me.grdKDWAREHOUSE.StyleController = Me.layoutControl
        Me.grdKDWAREHOUSE.TabIndex = 36
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
        'txtKDPENDAFTARAN
        '
        Me.txtKDPENDAFTARAN.EditValue = ""
        Me.txtKDPENDAFTARAN.EnterMoveNextControl = True
        Me.txtKDPENDAFTARAN.Location = New System.Drawing.Point(167, 150)
        Me.txtKDPENDAFTARAN.Name = "txtKDPENDAFTARAN"
        Me.txtKDPENDAFTARAN.Properties.ReadOnly = True
        Me.txtKDPENDAFTARAN.Size = New System.Drawing.Size(282, 20)
        Me.txtKDPENDAFTARAN.StyleController = Me.layoutControl
        Me.txtKDPENDAFTARAN.TabIndex = 34
        Me.txtKDPENDAFTARAN.TabStop = False
        '
        'grdKDPENJAMIN
        '
        Me.grdKDPENJAMIN.EnterMoveNextControl = True
        Me.grdKDPENJAMIN.Location = New System.Drawing.Point(167, 174)
        Me.grdKDPENJAMIN.MenuManager = Me.barManager
        Me.grdKDPENJAMIN.Name = "grdKDPENJAMIN"
        Me.grdKDPENJAMIN.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.grdKDPENJAMIN.Properties.NullText = ""
        Me.grdKDPENJAMIN.Properties.PopupFormMinSize = New System.Drawing.Size(600, 300)
        Me.grdKDPENJAMIN.Properties.ReadOnly = True
        Me.grdKDPENJAMIN.Properties.View = Me.grvKDPENJAMIN
        Me.grdKDPENJAMIN.Size = New System.Drawing.Size(282, 20)
        Me.grdKDPENJAMIN.StyleController = Me.layoutControl
        Me.grdKDPENJAMIN.TabIndex = 35
        '
        'grvKDPENJAMIN
        '
        Me.grvKDPENJAMIN.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn8})
        Me.grvKDPENJAMIN.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.grvKDPENJAMIN.Name = "grvKDPENJAMIN"
        Me.grvKDPENJAMIN.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.grvKDPENJAMIN.OptionsView.ShowAutoFilterRow = True
        Me.grvKDPENJAMIN.OptionsView.ShowGroupPanel = False
        '
        'GridColumn8
        '
        Me.GridColumn8.Caption = "Display Name"
        Me.GridColumn8.FieldName = "MEMO"
        Me.GridColumn8.Name = "GridColumn8"
        Me.GridColumn8.Visible = True
        Me.GridColumn8.VisibleIndex = 0
        '
        'deDATE
        '
        Me.deDATE.EditValue = Nothing
        Me.deDATE.EnterMoveNextControl = True
        Me.deDATE.Location = New System.Drawing.Point(167, 126)
        Me.deDATE.MenuManager = Me.barManager
        Me.deDATE.Name = "deDATE"
        Me.deDATE.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.deDATE.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
        Me.deDATE.Properties.Mask.EditMask = "dd/MM/yyyy"
        Me.deDATE.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.deDATE.Size = New System.Drawing.Size(282, 20)
        Me.deDATE.StyleController = Me.layoutControl
        Me.deDATE.TabIndex = 23
        '
        'txtGRANDTOTAL
        '
        Me.txtGRANDTOTAL.EditValue = "0"
        Me.txtGRANDTOTAL.Location = New System.Drawing.Point(613, 517)
        Me.txtGRANDTOTAL.MenuManager = Me.barManager
        Me.txtGRANDTOTAL.Name = "txtGRANDTOTAL"
        Me.txtGRANDTOTAL.Properties.Appearance.Options.UseTextOptions = True
        Me.txtGRANDTOTAL.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.txtGRANDTOTAL.Properties.Mask.EditMask = "n0"
        Me.txtGRANDTOTAL.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        Me.txtGRANDTOTAL.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.txtGRANDTOTAL.Properties.ReadOnly = True
        Me.txtGRANDTOTAL.Size = New System.Drawing.Size(165, 20)
        Me.txtGRANDTOTAL.StyleController = Me.layoutControl
        Me.txtGRANDTOTAL.TabIndex = 50
        '
        'grdCARIKDREGAWAL
        '
        Me.grdCARIKDREGAWAL.EnterMoveNextControl = True
        Me.grdCARIKDREGAWAL.Location = New System.Drawing.Point(169, 66)
        Me.grdCARIKDREGAWAL.MenuManager = Me.barManager
        Me.grdCARIKDREGAWAL.Name = "grdCARIKDREGAWAL"
        Me.grdCARIKDREGAWAL.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.grdCARIKDREGAWAL.Properties.NullText = ""
        Me.grdCARIKDREGAWAL.Properties.PopupFormMinSize = New System.Drawing.Size(900, 300)
        Me.grdCARIKDREGAWAL.Properties.View = Me.grvCARIKDPENDAFTARAN_AWAL
        Me.grdCARIKDREGAWAL.Size = New System.Drawing.Size(280, 20)
        Me.grdCARIKDREGAWAL.StyleController = Me.layoutControl
        Me.grdCARIKDREGAWAL.TabIndex = 42
        '
        'grvCARIKDPENDAFTARAN_AWAL
        '
        Me.grvCARIKDPENDAFTARAN_AWAL.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.grvCARIKDPENDAFTARAN_AWAL.Name = "grvCARIKDPENDAFTARAN_AWAL"
        Me.grvCARIKDPENDAFTARAN_AWAL.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.grvCARIKDPENDAFTARAN_AWAL.OptionsView.ShowAutoFilterRow = True
        Me.grvCARIKDPENDAFTARAN_AWAL.OptionsView.ShowGroupPanel = False
        '
        'cboCARI
        '
        Me.cboCARI.EditValue = "No. Rekam Medis"
        Me.cboCARI.Location = New System.Drawing.Point(24, 42)
        Me.cboCARI.MenuManager = Me.barManager
        Me.cboCARI.Name = "cboCARI"
        Me.cboCARI.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cboCARI.Properties.Items.AddRange(New Object() {"No. Rekam Medis", "Nama Pasien"})
        Me.cboCARI.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
        Me.cboCARI.Size = New System.Drawing.Size(141, 20)
        Me.cboCARI.StyleController = Me.layoutControl
        Me.cboCARI.TabIndex = 46
        '
        'txtCARI
        '
        Me.txtCARI.Location = New System.Drawing.Point(169, 42)
        Me.txtCARI.MenuManager = Me.barManager
        Me.txtCARI.Name = "txtCARI"
        Me.txtCARI.Size = New System.Drawing.Size(280, 20)
        Me.txtCARI.StyleController = Me.layoutControl
        Me.txtCARI.TabIndex = 45
        '
        'txtKDKUNJUNGAN
        '
        Me.txtKDKUNJUNGAN.EditValue = ""
        Me.txtKDKUNJUNGAN.EnterMoveNextControl = True
        Me.txtKDKUNJUNGAN.Location = New System.Drawing.Point(167, 198)
        Me.txtKDKUNJUNGAN.Name = "txtKDKUNJUNGAN"
        Me.txtKDKUNJUNGAN.Properties.ReadOnly = True
        Me.txtKDKUNJUNGAN.Size = New System.Drawing.Size(282, 20)
        Me.txtKDKUNJUNGAN.StyleController = Me.layoutControl
        Me.txtKDKUNJUNGAN.TabIndex = 10
        Me.txtKDKUNJUNGAN.TabStop = False
        '
        'tabControl
        '
        Me.tabControl.Location = New System.Drawing.Point(12, 270)
        Me.tabControl.Name = "tabControl"
        Me.tabControl.SelectedTabPage = Me.tab1
        Me.tabControl.Size = New System.Drawing.Size(766, 171)
        Me.tabControl.TabIndex = 18
        Me.tabControl.TabPages.AddRange(New DevExpress.XtraTab.XtraTabPage() {Me.tab1, Me.tab2})
        '
        'tab1
        '
        Me.tab1.Controls.Add(Me.grdDetail)
        Me.tab1.Name = "tab1"
        Me.tab1.Size = New System.Drawing.Size(760, 143)
        Me.tab1.Text = "Detail Information"
        '
        'grdDetail
        '
        Me.grdDetail.ContextMenuStrip = Me.mnuStrip
        Me.grdDetail.DataSource = Me.BindingSource
        Me.grdDetail.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grdDetail.Location = New System.Drawing.Point(0, 0)
        Me.grdDetail.MainView = Me.grvDetail
        Me.grdDetail.MenuManager = Me.barManager
        Me.grdDetail.Name = "grdDetail"
        Me.grdDetail.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.grdKDITEM, Me.grdKDUOM, Me.txtREMARKS, Me.grdKDSIGNA, Me.grdKDCARAPAKAI, Me.deDATE_EXPIRE, Me.grdKDITEM_L2, Me.chkISCHEKED})
        Me.grdDetail.Size = New System.Drawing.Size(760, 143)
        Me.grdDetail.TabIndex = 18
        Me.grdDetail.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.grvDetail})
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
        'BindingSource
        '
        Me.BindingSource.DataSource = GetType(DataAccess.S_BILLING_FARMASI)
        '
        'grvDetail
        '
        Me.grvDetail.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.colKDITEM, Me.colKDUOM, Me.colKDSIGNA, Me.colKDCARAPAKAI, Me.colKDITEM_L2, Me.colHARI, Me.colISPROLANIS, Me.colQTY, Me.colPRICE, Me.colSUBTOTAL, Me.colPPN_PERSEN, Me.colPPN, Me.colISTUSLAH, Me.colTUSLAH, Me.colGRANDTOTAL, Me.colDATE_EXPIRE, Me.colREMARKS, Me.colKDITEM_L1, Me.colKDITEM_L3, Me.colKDITEM_L4, Me.colKDITEM_L5, Me.colKDITEM_L6})
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
        Me.colKDITEM.Caption = "Obat"
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
        Me.grvKDITEM.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn2, Me.GridColumn7})
        Me.grvKDITEM.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.grvKDITEM.Name = "grvKDITEM"
        Me.grvKDITEM.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.grvKDITEM.OptionsView.ShowAutoFilterRow = True
        Me.grvKDITEM.OptionsView.ShowGroupPanel = False
        '
        'GridColumn2
        '
        Me.GridColumn2.Caption = "Obat"
        Me.GridColumn2.FieldName = "NMITEM2"
        Me.GridColumn2.Name = "GridColumn2"
        Me.GridColumn2.Visible = True
        Me.GridColumn2.VisibleIndex = 0
        '
        'GridColumn7
        '
        Me.GridColumn7.Caption = "Stok"
        Me.GridColumn7.DisplayFormat.FormatString = "{0:n0}"
        Me.GridColumn7.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.GridColumn7.FieldName = "STOK"
        Me.GridColumn7.Name = "GridColumn7"
        Me.GridColumn7.Visible = True
        Me.GridColumn7.VisibleIndex = 1
        '
        'colKDUOM
        '
        Me.colKDUOM.Caption = "Satuan"
        Me.colKDUOM.ColumnEdit = Me.grdKDUOM
        Me.colKDUOM.FieldName = "KDUOM"
        Me.colKDUOM.Name = "colKDUOM"
        Me.colKDUOM.Visible = True
        Me.colKDUOM.VisibleIndex = 1
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
        'colKDSIGNA
        '
        Me.colKDSIGNA.Caption = "Signa"
        Me.colKDSIGNA.ColumnEdit = Me.grdKDSIGNA
        Me.colKDSIGNA.FieldName = "KDSIGNA"
        Me.colKDSIGNA.Name = "colKDSIGNA"
        Me.colKDSIGNA.Visible = True
        Me.colKDSIGNA.VisibleIndex = 2
        '
        'grdKDSIGNA
        '
        Me.grdKDSIGNA.AutoHeight = False
        Me.grdKDSIGNA.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.grdKDSIGNA.Name = "grdKDSIGNA"
        Me.grdKDSIGNA.NullText = ""
        Me.grdKDSIGNA.View = Me.grvKDSIGNA
        '
        'grvKDSIGNA
        '
        Me.grvKDSIGNA.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn3})
        Me.grvKDSIGNA.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.grvKDSIGNA.Name = "grvKDSIGNA"
        Me.grvKDSIGNA.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.grvKDSIGNA.OptionsView.ShowGroupPanel = False
        '
        'GridColumn3
        '
        Me.GridColumn3.Caption = "Nama"
        Me.GridColumn3.FieldName = "MEMO"
        Me.GridColumn3.Name = "GridColumn3"
        Me.GridColumn3.Visible = True
        Me.GridColumn3.VisibleIndex = 0
        '
        'colKDCARAPAKAI
        '
        Me.colKDCARAPAKAI.Caption = "Cara Pakai"
        Me.colKDCARAPAKAI.ColumnEdit = Me.grdKDCARAPAKAI
        Me.colKDCARAPAKAI.FieldName = "KDCARAPAKAI"
        Me.colKDCARAPAKAI.Name = "colKDCARAPAKAI"
        Me.colKDCARAPAKAI.Visible = True
        Me.colKDCARAPAKAI.VisibleIndex = 3
        '
        'grdKDCARAPAKAI
        '
        Me.grdKDCARAPAKAI.AutoHeight = False
        Me.grdKDCARAPAKAI.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.grdKDCARAPAKAI.Name = "grdKDCARAPAKAI"
        Me.grdKDCARAPAKAI.NullText = ""
        Me.grdKDCARAPAKAI.View = Me.grvKDCARAPAKAI
        '
        'grvKDCARAPAKAI
        '
        Me.grvKDCARAPAKAI.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn12})
        Me.grvKDCARAPAKAI.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.grvKDCARAPAKAI.Name = "grvKDCARAPAKAI"
        Me.grvKDCARAPAKAI.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.grvKDCARAPAKAI.OptionsView.ShowGroupPanel = False
        '
        'GridColumn12
        '
        Me.GridColumn12.Caption = "Nama"
        Me.GridColumn12.FieldName = "MEMO"
        Me.GridColumn12.Name = "GridColumn12"
        Me.GridColumn12.Visible = True
        Me.GridColumn12.VisibleIndex = 0
        '
        'colKDITEM_L2
        '
        Me.colKDITEM_L2.Caption = "Kode2"
        Me.colKDITEM_L2.ColumnEdit = Me.grdKDITEM_L2
        Me.colKDITEM_L2.FieldName = "KDITEM_L2"
        Me.colKDITEM_L2.Name = "colKDITEM_L2"
        Me.colKDITEM_L2.Visible = True
        Me.colKDITEM_L2.VisibleIndex = 4
        '
        'grdKDITEM_L2
        '
        Me.grdKDITEM_L2.AutoHeight = False
        Me.grdKDITEM_L2.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.grdKDITEM_L2.Name = "grdKDITEM_L2"
        Me.grdKDITEM_L2.NullText = ""
        Me.grdKDITEM_L2.View = Me.grvKDITEM_L2
        '
        'grvKDITEM_L2
        '
        Me.grvKDITEM_L2.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn11})
        Me.grvKDITEM_L2.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.grvKDITEM_L2.Name = "grvKDITEM_L2"
        Me.grvKDITEM_L2.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.grvKDITEM_L2.OptionsView.ShowGroupPanel = False
        '
        'GridColumn11
        '
        Me.GridColumn11.Caption = "Nama"
        Me.GridColumn11.FieldName = "MEMO"
        Me.GridColumn11.Name = "GridColumn11"
        Me.GridColumn11.Visible = True
        Me.GridColumn11.VisibleIndex = 0
        '
        'colHARI
        '
        Me.colHARI.Caption = "Hari"
        Me.colHARI.DisplayFormat.FormatString = "{0:n2}"
        Me.colHARI.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.colHARI.FieldName = "HARI"
        Me.colHARI.Name = "colHARI"
        Me.colHARI.Visible = True
        Me.colHARI.VisibleIndex = 5
        '
        'colISPROLANIS
        '
        Me.colISPROLANIS.Caption = "23 ?"
        Me.colISPROLANIS.ColumnEdit = Me.chkISCHEKED
        Me.colISPROLANIS.FieldName = "ISPROLANIS"
        Me.colISPROLANIS.Name = "colISPROLANIS"
        Me.colISPROLANIS.Visible = True
        Me.colISPROLANIS.VisibleIndex = 6
        '
        'chkISCHEKED
        '
        Me.chkISCHEKED.AutoHeight = False
        Me.chkISCHEKED.Name = "chkISCHEKED"
        '
        'colQTY
        '
        Me.colQTY.Caption = "Jumlah"
        Me.colQTY.DisplayFormat.FormatString = "{0:n2}"
        Me.colQTY.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.colQTY.FieldName = "QTY"
        Me.colQTY.Name = "colQTY"
        Me.colQTY.Visible = True
        Me.colQTY.VisibleIndex = 7
        '
        'colPRICE
        '
        Me.colPRICE.Caption = "Harga Beli"
        Me.colPRICE.DisplayFormat.FormatString = "{0:n2}"
        Me.colPRICE.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.colPRICE.FieldName = "PRICE"
        Me.colPRICE.Name = "colPRICE"
        Me.colPRICE.OptionsColumn.AllowEdit = False
        Me.colPRICE.OptionsColumn.AllowFocus = False
        Me.colPRICE.OptionsColumn.ReadOnly = True
        Me.colPRICE.OptionsColumn.TabStop = False
        Me.colPRICE.Visible = True
        Me.colPRICE.VisibleIndex = 8
        '
        'colPPN_PERSEN
        '
        Me.colPPN_PERSEN.Caption = "PPn (%)"
        Me.colPPN_PERSEN.DisplayFormat.FormatString = "{0:n0}"
        Me.colPPN_PERSEN.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.colPPN_PERSEN.FieldName = "PPN_PERSEN"
        Me.colPPN_PERSEN.Name = "colPPN_PERSEN"
        Me.colPPN_PERSEN.Visible = True
        Me.colPPN_PERSEN.VisibleIndex = 10
        '
        'colPPN
        '
        Me.colPPN.Caption = "Total PPn"
        Me.colPPN.DisplayFormat.FormatString = "{0:n2}"
        Me.colPPN.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.colPPN.FieldName = "PPN"
        Me.colPPN.Name = "colPPN"
        Me.colPPN.OptionsColumn.AllowEdit = False
        Me.colPPN.OptionsColumn.AllowFocus = False
        Me.colPPN.OptionsColumn.ReadOnly = True
        Me.colPPN.OptionsColumn.TabStop = False
        Me.colPPN.Visible = True
        Me.colPPN.VisibleIndex = 11
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
        Me.colSUBTOTAL.VisibleIndex = 9
        '
        'colISTUSLAH
        '
        Me.colISTUSLAH.Caption = "Is Tuslah"
        Me.colISTUSLAH.ColumnEdit = Me.chkISCHEKED
        Me.colISTUSLAH.FieldName = "ISTUSLAH"
        Me.colISTUSLAH.Name = "colISTUSLAH"
        Me.colISTUSLAH.Visible = True
        Me.colISTUSLAH.VisibleIndex = 12
        '
        'colTUSLAH
        '
        Me.colTUSLAH.Caption = "Total Tuslah"
        Me.colTUSLAH.DisplayFormat.FormatString = "{0:n2}"
        Me.colTUSLAH.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.colTUSLAH.FieldName = "TUSLAH"
        Me.colTUSLAH.Name = "colTUSLAH"
        Me.colTUSLAH.OptionsColumn.AllowEdit = False
        Me.colTUSLAH.OptionsColumn.AllowFocus = False
        Me.colTUSLAH.OptionsColumn.ReadOnly = True
        Me.colTUSLAH.OptionsColumn.TabStop = False
        Me.colTUSLAH.Visible = True
        Me.colTUSLAH.VisibleIndex = 13
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
        Me.colGRANDTOTAL.VisibleIndex = 14
        '
        'colDATE_EXPIRE
        '
        Me.colDATE_EXPIRE.Caption = "Tanggal Expire"
        Me.colDATE_EXPIRE.ColumnEdit = Me.deDATE_EXPIRE
        Me.colDATE_EXPIRE.FieldName = "DATE_EXPIRE"
        Me.colDATE_EXPIRE.Name = "colDATE_EXPIRE"
        Me.colDATE_EXPIRE.Visible = True
        Me.colDATE_EXPIRE.VisibleIndex = 15
        '
        'deDATE_EXPIRE
        '
        Me.deDATE_EXPIRE.AutoHeight = False
        Me.deDATE_EXPIRE.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.deDATE_EXPIRE.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.deDATE_EXPIRE.DisplayFormat.FormatString = "dd-MM-yyyy"
        Me.deDATE_EXPIRE.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.deDATE_EXPIRE.EditFormat.FormatString = "dd-MM-yyyy"
        Me.deDATE_EXPIRE.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.deDATE_EXPIRE.Mask.EditMask = "dd-MM-yyyy"
        Me.deDATE_EXPIRE.Name = "deDATE_EXPIRE"
        '
        'colREMARKS
        '
        Me.colREMARKS.Caption = "Catatan"
        Me.colREMARKS.ColumnEdit = Me.txtREMARKS
        Me.colREMARKS.FieldName = "REMARKS"
        Me.colREMARKS.Name = "colREMARKS"
        Me.colREMARKS.Visible = True
        Me.colREMARKS.VisibleIndex = 16
        '
        'txtREMARKS
        '
        Me.txtREMARKS.AutoHeight = False
        Me.txtREMARKS.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.txtREMARKS.Name = "txtREMARKS"
        '
        'colKDITEM_L1
        '
        Me.colKDITEM_L1.Caption = "Kode1"
        Me.colKDITEM_L1.FieldName = "KDITEM_L1"
        Me.colKDITEM_L1.Name = "colKDITEM_L1"
        '
        'colKDITEM_L3
        '
        Me.colKDITEM_L3.Caption = "Kode3"
        Me.colKDITEM_L3.FieldName = "KDITEM_L3"
        Me.colKDITEM_L3.Name = "colKDITEM_L3"
        '
        'colKDITEM_L4
        '
        Me.colKDITEM_L4.Caption = "Kode4"
        Me.colKDITEM_L4.FieldName = "KDITEM_L4"
        Me.colKDITEM_L4.Name = "colKDITEM_L4"
        '
        'colKDITEM_L5
        '
        Me.colKDITEM_L5.Caption = "Kode5"
        Me.colKDITEM_L5.FieldName = "KDITEM_L5"
        Me.colKDITEM_L5.Name = "colKDITEM_L5"
        '
        'colKDITEM_L6
        '
        Me.colKDITEM_L6.Caption = "Kode6"
        Me.colKDITEM_L6.FieldName = "KDITEM_L6"
        Me.colKDITEM_L6.Name = "colKDITEM_L6"
        '
        'tab2
        '
        Me.tab2.Controls.Add(Me.txtMEMO)
        Me.tab2.Name = "tab2"
        Me.tab2.Size = New System.Drawing.Size(760, 215)
        Me.tab2.Text = "Keterangan"
        '
        'txtMEMO
        '
        Me.txtMEMO.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtMEMO.Location = New System.Drawing.Point(0, 0)
        Me.txtMEMO.MenuManager = Me.barManager
        Me.txtMEMO.Name = "txtMEMO"
        Me.txtMEMO.Size = New System.Drawing.Size(760, 215)
        Me.txtMEMO.TabIndex = 0
        '
        'txtKDBILLING
        '
        Me.txtKDBILLING.EditValue = ""
        Me.txtKDBILLING.EnterMoveNextControl = True
        Me.txtKDBILLING.Location = New System.Drawing.Point(167, 102)
        Me.txtKDBILLING.Name = "txtKDBILLING"
        Me.txtKDBILLING.Properties.ReadOnly = True
        Me.txtKDBILLING.Size = New System.Drawing.Size(282, 20)
        Me.txtKDBILLING.StyleController = Me.layoutControl
        Me.txtKDBILLING.TabIndex = 9
        Me.txtKDBILLING.TabStop = False
        '
        'LayoutControlGroup1
        '
        Me.LayoutControlGroup1.CustomizationFormText = "LayoutControlGroup1"
        Me.LayoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.LayoutControlGroup1.GroupBordersVisible = False
        Me.LayoutControlGroup1.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlItem5, Me.lKDKUNJUNGAN_POLI, Me.LayoutControlGroup2, Me.lGRANDTOTAL, Me.EmptySpaceItem2, Me.lDATE, Me.lKDBILLING_POLI, Me.LayoutControlItem8, Me.LayoutControlItem9, Me.LayoutControlItem7, Me.EmptySpaceItem1, Me.LayoutControlItem3, Me.LayoutControlItem10, Me.lTUSLAH, Me.lPPN})
        Me.LayoutControlGroup1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlGroup1.Name = "Root"
        Me.LayoutControlGroup1.Size = New System.Drawing.Size(790, 549)
        Me.LayoutControlGroup1.TextVisible = False
        '
        'LayoutControlItem5
        '
        Me.LayoutControlItem5.Control = Me.tabControl
        Me.LayoutControlItem5.CustomizationFormText = "LayoutControlItem5"
        Me.LayoutControlItem5.Location = New System.Drawing.Point(0, 258)
        Me.LayoutControlItem5.Name = "LayoutControlItem5"
        Me.LayoutControlItem5.Size = New System.Drawing.Size(770, 175)
        Me.LayoutControlItem5.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem5.TextVisible = False
        '
        'lKDKUNJUNGAN_POLI
        '
        Me.lKDKUNJUNGAN_POLI.AppearanceItemCaption.Options.UseTextOptions = True
        Me.lKDKUNJUNGAN_POLI.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.lKDKUNJUNGAN_POLI.Control = Me.txtKDKUNJUNGAN
        Me.lKDKUNJUNGAN_POLI.Location = New System.Drawing.Point(0, 186)
        Me.lKDKUNJUNGAN_POLI.Name = "lKDKUNJUNGAN_POLI"
        Me.lKDKUNJUNGAN_POLI.Size = New System.Drawing.Size(441, 24)
        Me.lKDKUNJUNGAN_POLI.Text = "No. Kunjungan"
        Me.lKDKUNJUNGAN_POLI.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.lKDKUNJUNGAN_POLI.TextSize = New System.Drawing.Size(150, 20)
        Me.lKDKUNJUNGAN_POLI.TextToControlDistance = 5
        '
        'LayoutControlGroup2
        '
        Me.LayoutControlGroup2.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlItem1, Me.LayoutControlItem2, Me.LayoutControlItem4, Me.EmptySpaceItem3})
        Me.LayoutControlGroup2.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlGroup2.Name = "LayoutControlGroup2"
        Me.LayoutControlGroup2.Size = New System.Drawing.Size(770, 90)
        Me.LayoutControlGroup2.Text = "PENCARIAN"
        '
        'LayoutControlItem1
        '
        Me.LayoutControlItem1.Control = Me.cboCARI
        Me.LayoutControlItem1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlItem1.Name = "LayoutControlItem1"
        Me.LayoutControlItem1.Size = New System.Drawing.Size(145, 48)
        Me.LayoutControlItem1.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem1.TextVisible = False
        '
        'LayoutControlItem2
        '
        Me.LayoutControlItem2.Control = Me.txtCARI
        Me.LayoutControlItem2.Location = New System.Drawing.Point(145, 0)
        Me.LayoutControlItem2.Name = "LayoutControlItem2"
        Me.LayoutControlItem2.Size = New System.Drawing.Size(284, 24)
        Me.LayoutControlItem2.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem2.TextVisible = False
        '
        'LayoutControlItem4
        '
        Me.LayoutControlItem4.Control = Me.grdCARIKDREGAWAL
        Me.LayoutControlItem4.Location = New System.Drawing.Point(145, 24)
        Me.LayoutControlItem4.Name = "LayoutControlItem4"
        Me.LayoutControlItem4.Size = New System.Drawing.Size(284, 24)
        Me.LayoutControlItem4.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem4.TextVisible = False
        '
        'EmptySpaceItem3
        '
        Me.EmptySpaceItem3.AllowHotTrack = False
        Me.EmptySpaceItem3.Location = New System.Drawing.Point(429, 0)
        Me.EmptySpaceItem3.Name = "EmptySpaceItem3"
        Me.EmptySpaceItem3.Size = New System.Drawing.Size(317, 48)
        Me.EmptySpaceItem3.TextSize = New System.Drawing.Size(0, 0)
        '
        'lGRANDTOTAL
        '
        Me.lGRANDTOTAL.AppearanceItemCaption.Options.UseTextOptions = True
        Me.lGRANDTOTAL.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.lGRANDTOTAL.Control = Me.txtGRANDTOTAL
        Me.lGRANDTOTAL.Location = New System.Drawing.Point(496, 505)
        Me.lGRANDTOTAL.Name = "lGRANDTOTAL"
        Me.lGRANDTOTAL.Size = New System.Drawing.Size(274, 24)
        Me.lGRANDTOTAL.Text = "Grand Total :"
        Me.lGRANDTOTAL.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.lGRANDTOTAL.TextSize = New System.Drawing.Size(100, 20)
        Me.lGRANDTOTAL.TextToControlDistance = 5
        '
        'EmptySpaceItem2
        '
        Me.EmptySpaceItem2.AllowHotTrack = False
        Me.EmptySpaceItem2.Location = New System.Drawing.Point(0, 433)
        Me.EmptySpaceItem2.Name = "EmptySpaceItem2"
        Me.EmptySpaceItem2.Size = New System.Drawing.Size(496, 96)
        Me.EmptySpaceItem2.TextSize = New System.Drawing.Size(0, 0)
        '
        'lDATE
        '
        Me.lDATE.AppearanceItemCaption.Options.UseTextOptions = True
        Me.lDATE.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.lDATE.Control = Me.deDATE
        Me.lDATE.Location = New System.Drawing.Point(0, 114)
        Me.lDATE.Name = "lDATE"
        Me.lDATE.Size = New System.Drawing.Size(441, 24)
        Me.lDATE.Text = "Tanggal"
        Me.lDATE.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.lDATE.TextSize = New System.Drawing.Size(150, 20)
        Me.lDATE.TextToControlDistance = 5
        '
        'lKDBILLING_POLI
        '
        Me.lKDBILLING_POLI.AppearanceItemCaption.Options.UseTextOptions = True
        Me.lKDBILLING_POLI.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.lKDBILLING_POLI.Control = Me.txtKDBILLING
        Me.lKDBILLING_POLI.CustomizationFormText = "lKDBILLING_POLI"
        Me.lKDBILLING_POLI.Location = New System.Drawing.Point(0, 90)
        Me.lKDBILLING_POLI.Name = "lKDBILLING_POLI"
        Me.lKDBILLING_POLI.Size = New System.Drawing.Size(441, 24)
        Me.lKDBILLING_POLI.Text = "Kode"
        Me.lKDBILLING_POLI.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.lKDBILLING_POLI.TextSize = New System.Drawing.Size(150, 20)
        Me.lKDBILLING_POLI.TextToControlDistance = 5
        '
        'LayoutControlItem8
        '
        Me.LayoutControlItem8.AppearanceItemCaption.Options.UseTextOptions = True
        Me.LayoutControlItem8.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.LayoutControlItem8.Control = Me.txtKDPENDAFTARAN
        Me.LayoutControlItem8.Location = New System.Drawing.Point(0, 138)
        Me.LayoutControlItem8.Name = "LayoutControlItem8"
        Me.LayoutControlItem8.Size = New System.Drawing.Size(441, 24)
        Me.LayoutControlItem8.Text = "No. Pendaftaran"
        Me.LayoutControlItem8.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.LayoutControlItem8.TextSize = New System.Drawing.Size(150, 20)
        Me.LayoutControlItem8.TextToControlDistance = 5
        '
        'LayoutControlItem9
        '
        Me.LayoutControlItem9.AppearanceItemCaption.Options.UseTextOptions = True
        Me.LayoutControlItem9.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.LayoutControlItem9.Control = Me.grdKDPENJAMIN
        Me.LayoutControlItem9.Location = New System.Drawing.Point(0, 162)
        Me.LayoutControlItem9.Name = "LayoutControlItem9"
        Me.LayoutControlItem9.Size = New System.Drawing.Size(441, 24)
        Me.LayoutControlItem9.Text = "Penjamin"
        Me.LayoutControlItem9.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.LayoutControlItem9.TextSize = New System.Drawing.Size(150, 20)
        Me.LayoutControlItem9.TextToControlDistance = 5
        '
        'LayoutControlItem7
        '
        Me.LayoutControlItem7.AppearanceItemCaption.Options.UseTextOptions = True
        Me.LayoutControlItem7.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.LayoutControlItem7.Control = Me.grdKDDOCTOR_H
        Me.LayoutControlItem7.Location = New System.Drawing.Point(0, 210)
        Me.LayoutControlItem7.Name = "LayoutControlItem7"
        Me.LayoutControlItem7.Size = New System.Drawing.Size(441, 24)
        Me.LayoutControlItem7.Text = "Dokter"
        Me.LayoutControlItem7.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.LayoutControlItem7.TextSize = New System.Drawing.Size(150, 20)
        Me.LayoutControlItem7.TextToControlDistance = 5
        '
        'EmptySpaceItem1
        '
        Me.EmptySpaceItem1.AllowHotTrack = False
        Me.EmptySpaceItem1.Location = New System.Drawing.Point(441, 90)
        Me.EmptySpaceItem1.Name = "EmptySpaceItem1"
        Me.EmptySpaceItem1.Size = New System.Drawing.Size(329, 168)
        Me.EmptySpaceItem1.TextSize = New System.Drawing.Size(0, 0)
        '
        'LayoutControlItem3
        '
        Me.LayoutControlItem3.AppearanceItemCaption.Options.UseTextOptions = True
        Me.LayoutControlItem3.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.LayoutControlItem3.Control = Me.grdKDWAREHOUSE
        Me.LayoutControlItem3.Location = New System.Drawing.Point(0, 234)
        Me.LayoutControlItem3.Name = "LayoutControlItem3"
        Me.LayoutControlItem3.Size = New System.Drawing.Size(441, 24)
        Me.LayoutControlItem3.Text = "Depo :"
        Me.LayoutControlItem3.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.LayoutControlItem3.TextSize = New System.Drawing.Size(150, 20)
        Me.LayoutControlItem3.TextToControlDistance = 5
        '
        'GridColumn6
        '
        Me.GridColumn6.Caption = "Number"
        Me.GridColumn6.FieldName = "KDSO"
        Me.GridColumn6.Name = "GridColumn6"
        Me.GridColumn6.Visible = True
        Me.GridColumn6.VisibleIndex = 0
        '
        'txtSUBTOTAL
        '
        Me.txtSUBTOTAL.EditValue = "0"
        Me.txtSUBTOTAL.Location = New System.Drawing.Point(613, 445)
        Me.txtSUBTOTAL.MenuManager = Me.barManager
        Me.txtSUBTOTAL.Name = "txtSUBTOTAL"
        Me.txtSUBTOTAL.Properties.Appearance.Options.UseTextOptions = True
        Me.txtSUBTOTAL.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.txtSUBTOTAL.Properties.Mask.EditMask = "n0"
        Me.txtSUBTOTAL.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        Me.txtSUBTOTAL.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.txtSUBTOTAL.Properties.ReadOnly = True
        Me.txtSUBTOTAL.Size = New System.Drawing.Size(165, 20)
        Me.txtSUBTOTAL.StyleController = Me.layoutControl
        Me.txtSUBTOTAL.TabIndex = 51
        '
        'LayoutControlItem10
        '
        Me.LayoutControlItem10.AppearanceItemCaption.Options.UseTextOptions = True
        Me.LayoutControlItem10.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.LayoutControlItem10.Control = Me.txtSUBTOTAL
        Me.LayoutControlItem10.Location = New System.Drawing.Point(496, 433)
        Me.LayoutControlItem10.Name = "LayoutControlItem10"
        Me.LayoutControlItem10.Size = New System.Drawing.Size(274, 24)
        Me.LayoutControlItem10.Text = "Sub Total :"
        Me.LayoutControlItem10.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.LayoutControlItem10.TextSize = New System.Drawing.Size(100, 20)
        Me.LayoutControlItem10.TextToControlDistance = 5
        '
        'txtTUSLAH
        '
        Me.txtTUSLAH.EditValue = "0"
        Me.txtTUSLAH.Location = New System.Drawing.Point(613, 493)
        Me.txtTUSLAH.MenuManager = Me.barManager
        Me.txtTUSLAH.Name = "txtTUSLAH"
        Me.txtTUSLAH.Properties.Appearance.Options.UseTextOptions = True
        Me.txtTUSLAH.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.txtTUSLAH.Properties.Mask.EditMask = "n0"
        Me.txtTUSLAH.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        Me.txtTUSLAH.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.txtTUSLAH.Properties.ReadOnly = True
        Me.txtTUSLAH.Size = New System.Drawing.Size(165, 20)
        Me.txtTUSLAH.StyleController = Me.layoutControl
        Me.txtTUSLAH.TabIndex = 52
        '
        'lTUSLAH
        '
        Me.lTUSLAH.AppearanceItemCaption.Options.UseTextOptions = True
        Me.lTUSLAH.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.lTUSLAH.Control = Me.txtTUSLAH
        Me.lTUSLAH.Location = New System.Drawing.Point(496, 481)
        Me.lTUSLAH.Name = "lTUSLAH"
        Me.lTUSLAH.Size = New System.Drawing.Size(274, 24)
        Me.lTUSLAH.Text = "Tuslah :"
        Me.lTUSLAH.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.lTUSLAH.TextSize = New System.Drawing.Size(100, 20)
        Me.lTUSLAH.TextToControlDistance = 5
        '
        'txtPPN
        '
        Me.txtPPN.EditValue = "0"
        Me.txtPPN.Location = New System.Drawing.Point(613, 469)
        Me.txtPPN.MenuManager = Me.barManager
        Me.txtPPN.Name = "txtPPN"
        Me.txtPPN.Properties.Appearance.Options.UseTextOptions = True
        Me.txtPPN.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.txtPPN.Properties.Mask.EditMask = "n0"
        Me.txtPPN.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        Me.txtPPN.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.txtPPN.Properties.ReadOnly = True
        Me.txtPPN.Size = New System.Drawing.Size(165, 20)
        Me.txtPPN.StyleController = Me.layoutControl
        Me.txtPPN.TabIndex = 52
        '
        'lPPN
        '
        Me.lPPN.AppearanceItemCaption.Options.UseTextOptions = True
        Me.lPPN.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.lPPN.Control = Me.txtPPN
        Me.lPPN.Location = New System.Drawing.Point(496, 457)
        Me.lPPN.Name = "lPPN"
        Me.lPPN.Size = New System.Drawing.Size(274, 24)
        Me.lPPN.Text = "PPN :"
        Me.lPPN.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.lPPN.TextSize = New System.Drawing.Size(100, 20)
        Me.lPPN.TextToControlDistance = 5
        '
        'btnPending
        '
        Me.btnPending.Caption = "F7 - Pending"
        Me.btnPending.Id = 8
        Me.btnPending.Name = "btnPending"
        '
        'frmBillingFarmasi
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
        Me.Name = "frmBillingFarmasi"
        Me.ShowIcon = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        CType(Me.layoutControl, System.ComponentModel.ISupportInitialize).EndInit()
        Me.layoutControl.ResumeLayout(False)
        CType(Me.grdKDDOCTOR_H.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.barManager, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.progressBarSave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.progressSave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdKDWAREHOUSE.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grvKDWAREHOUSE, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtKDPENDAFTARAN.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdKDPENJAMIN.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grvKDPENJAMIN, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.deDATE.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.deDATE.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtGRANDTOTAL.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdCARIKDREGAWAL.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grvCARIKDPENDAFTARAN_AWAL, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cboCARI.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCARI.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtKDKUNJUNGAN.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.tabControl, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tabControl.ResumeLayout(False)
        Me.tab1.ResumeLayout(False)
        CType(Me.grdDetail, System.ComponentModel.ISupportInitialize).EndInit()
        Me.mnuStrip.ResumeLayout(False)
        CType(Me.BindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grvDetail, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdKDITEM, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grvKDITEM, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdKDUOM, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grvKDUOM, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdKDSIGNA, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grvKDSIGNA, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdKDCARAPAKAI, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grvKDCARAPAKAI, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdKDITEM_L2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grvKDITEM_L2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkISCHEKED, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.deDATE_EXPIRE.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.deDATE_EXPIRE, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtREMARKS, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tab2.ResumeLayout(False)
        CType(Me.txtMEMO.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtKDBILLING.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lKDKUNJUNGAN_POLI, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.EmptySpaceItem3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lGRANDTOTAL, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.EmptySpaceItem2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lDATE, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lKDBILLING_POLI, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem8, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem9, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem7, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.EmptySpaceItem1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtSUBTOTAL.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem10, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtTUSLAH.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lTUSLAH, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtPPN.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lPPN, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents layoutControl As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents LayoutControlGroup1 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents lKDBILLING_POLI As DevExpress.XtraLayout.LayoutControlItem
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
    Friend WithEvents txtKDBILLING As DevExpress.XtraEditors.TextEdit
    Friend WithEvents tabControl As DevExpress.XtraTab.XtraTabControl
    Friend WithEvents tab1 As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents LayoutControlItem5 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents grdDetail As DevExpress.XtraGrid.GridControl
    Friend WithEvents grvDetail As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents grdKDITEM As DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit
    Friend WithEvents grvKDITEM As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents grdKDUOM As DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit
    Friend WithEvents grvKDUOM As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridColumn4 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents mnuStrip As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents DeleteToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents txtREMARKS As DevExpress.XtraEditors.Repository.RepositoryItemMemoExEdit
    Friend WithEvents GridColumn2 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn6 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn7 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents txtKDKUNJUNGAN As DevExpress.XtraEditors.TextEdit
    Friend WithEvents lKDKUNJUNGAN_POLI As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents cboCARI As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents txtCARI As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LayoutControlItem1 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem2 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlGroup2 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents grdCARIKDREGAWAL As DevExpress.XtraEditors.GridLookUpEdit
    Friend WithEvents grvCARIKDPENDAFTARAN_AWAL As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents LayoutControlItem4 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents txtGRANDTOTAL As DevExpress.XtraEditors.TextEdit
    Friend WithEvents lGRANDTOTAL As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents EmptySpaceItem2 As DevExpress.XtraLayout.EmptySpaceItem
    Friend WithEvents deDATE As DevExpress.XtraEditors.DateEdit
    Friend WithEvents lDATE As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents tab2 As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents txtMEMO As DevExpress.XtraEditors.MemoEdit
    Friend WithEvents colKDITEM As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colKDUOM As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colQTY As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colPRICE As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colGRANDTOTAL As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colREMARKS As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents txtKDPENDAFTARAN As DevExpress.XtraEditors.TextEdit
    Friend WithEvents grdKDPENJAMIN As DevExpress.XtraEditors.GridLookUpEdit
    Friend WithEvents grvKDPENJAMIN As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridColumn8 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents LayoutControlItem8 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem9 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents colKDITEM_L1 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colKDITEM_L2 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colKDITEM_L3 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colKDITEM_L4 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colKDITEM_L5 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colKDITEM_L6 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents grdKDWAREHOUSE As DevExpress.XtraEditors.GridLookUpEdit
    Friend WithEvents grvKDWAREHOUSE As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridColumn9 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents LayoutControlItem3 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents colKDSIGNA As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colKDCARAPAKAI As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents grdKDSIGNA As DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit
    Friend WithEvents grvKDSIGNA As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridColumn3 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents grdKDCARAPAKAI As DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit
    Friend WithEvents grvKDCARAPAKAI As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridColumn12 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents deDATE_EXPIRE As DevExpress.XtraEditors.Repository.RepositoryItemDateEdit
    Friend WithEvents BindingSource As BindingSource
    Friend WithEvents colDATE_EXPIRE As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colHARI As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents grdKDDOCTOR_H As DevExpress.XtraEditors.GridLookUpEdit
    Friend WithEvents GridView1 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridColumn10 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents LayoutControlItem7 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents grdKDITEM_L2 As DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit
    Friend WithEvents grvKDITEM_L2 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridColumn11 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colISPROLANIS As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents chkISCHEKED As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents colSUBTOTAL As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colPPN_PERSEN As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colPPN As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colISTUSLAH As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colTUSLAH As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents EmptySpaceItem3 As DevExpress.XtraLayout.EmptySpaceItem
    Friend WithEvents EmptySpaceItem1 As DevExpress.XtraLayout.EmptySpaceItem
    Friend WithEvents txtSUBTOTAL As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LayoutControlItem10 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents txtPPN As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txtTUSLAH As DevExpress.XtraEditors.TextEdit
    Friend WithEvents lTUSLAH As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents lPPN As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents btnPending As DevExpress.XtraBars.BarButtonItem
End Class
