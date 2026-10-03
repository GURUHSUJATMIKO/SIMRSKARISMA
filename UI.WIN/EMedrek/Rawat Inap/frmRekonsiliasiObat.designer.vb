<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmRekonsiliasiObat
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
        Me.GroupControl1 = New DevExpress.XtraEditors.GroupControl()
        Me.LayoutControl1 = New DevExpress.XtraLayout.LayoutControl()
        Me.txtTujuan = New DevExpress.XtraEditors.TextEdit()
        Me.txtNoRegister = New DevExpress.XtraEditors.TextEdit()
        Me.txtNamaPasien = New DevExpress.XtraEditors.TextEdit()
        Me.txtNoPasien = New DevExpress.XtraEditors.TextEdit()
        Me.LayoutControlGroup1 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.LayoutControlItem1 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem5 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem6 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem2 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.ContextMenuStrip1 = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.DeleteToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.LayoutControl3 = New DevExpress.XtraLayout.LayoutControl()
        Me.txtApoteker = New DevExpress.XtraEditors.TextEdit()
        Me.txtPerawat = New DevExpress.XtraEditors.TextEdit()
        Me.chkObatBawa_TIDAK = New DevExpress.XtraEditors.CheckEdit()
        Me.chkObatBawa_YA = New DevExpress.XtraEditors.CheckEdit()
        Me.chkAlergi_TIDAK = New DevExpress.XtraEditors.CheckEdit()
        Me.chkAlergi_YA = New DevExpress.XtraEditors.CheckEdit()
        Me.deDATE = New DevExpress.XtraEditors.DateEdit()
        Me.grdDokter = New DevExpress.XtraEditors.SearchLookUpEdit()
        Me.SearchLookUpEdit1View = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.colNAMA = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.tabControl = New DevExpress.XtraTab.XtraTabControl()
        Me.tab1 = New DevExpress.XtraTab.XtraTabPage()
        Me.grdDetail = New DevExpress.XtraGrid.GridControl()
        Me.BindingSource1 = New System.Windows.Forms.BindingSource(Me.components)
        Me.grvDetail = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.colITEMOBAT = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colREAKSIALERGI = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.txtREAKSIALERGI = New DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit()
        Me.colTingkatAlergi = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.grdITEM = New DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit()
        Me.grvITEM = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn3 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.grdUOM = New DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit()
        Me.grvUOM = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn4 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.deDATEEXPIRE = New DevExpress.XtraEditors.Repository.RepositoryItemDateEdit()
        Me.txtTINGKATALERGI = New DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit()
        Me.grdKDDOCTOR = New DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit()
        Me.grvKDDOCTOR = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn2 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cboTINGKATALERGI = New DevExpress.XtraEditors.Repository.RepositoryItemComboBox()
        Me.tab2 = New DevExpress.XtraTab.XtraTabPage()
        Me.grdDetail2 = New DevExpress.XtraGrid.GridControl()
        Me.ContextMenuStrip2 = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.DeleteToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.BindingSource2 = New System.Windows.Forms.BindingSource(Me.components)
        Me.grvDetail2 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.colITEMOBAT2 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colDOSIS = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.txtDOSIS = New DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit()
        Me.colFREKUENSI = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colTANGGAL_MULAI = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.deTANGGAL_MULAI = New DevExpress.XtraEditors.Repository.RepositoryItemDateEdit()
        Me.colTANGGAL_STOP = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.deTANGGAL_STOP = New DevExpress.XtraEditors.Repository.RepositoryItemDateEdit()
        Me.colTANGGAL_STOP_STR = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colISOBATDILANJUTKAN_1 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.chkISOBATDILANJUTKAN_1 = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.colISOBATDILANJUTKAN_2 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.chkISOBATDILANJUTKAN_2 = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.grdITEM2 = New DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit()
        Me.grvITEM2 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn6 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.grdKDUOM2 = New DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit()
        Me.GridView3 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn9 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.txtFREKUENSI = New DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit()
        Me.grdKDDOCTOR2 = New DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit()
        Me.grvKDDOCTOR2 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn13 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.txtALASANMAKAN_OBAT = New DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit()
        Me.txtKETERANGAN = New DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit()
        Me.grdSIGNA = New DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit()
        Me.grvSIGNA = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.colSigna = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.tab3 = New DevExpress.XtraTab.XtraTabPage()
        Me.txtKET = New DevExpress.XtraEditors.MemoEdit()
        Me.LayoutControlGroup2 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.LayoutControlItem9 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem10 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem16 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem17 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem18 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem19 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem20 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem7 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem11 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.fileDialog = New System.Windows.Forms.OpenFileDialog()
        CType(Me.barManager, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.progressBarSave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.progressSave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GroupControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl1.SuspendLayout()
        CType(Me.LayoutControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.LayoutControl1.SuspendLayout()
        CType(Me.txtTujuan.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtNoRegister.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtNamaPasien.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtNoPasien.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ContextMenuStrip1.SuspendLayout()
        Me.Panel3.SuspendLayout()
        CType(Me.LayoutControl3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.LayoutControl3.SuspendLayout()
        CType(Me.txtApoteker.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtPerawat.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkObatBawa_TIDAK.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkObatBawa_YA.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkAlergi_TIDAK.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkAlergi_YA.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.deDATE.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.deDATE.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdDokter.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SearchLookUpEdit1View, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.tabControl, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tabControl.SuspendLayout()
        Me.tab1.SuspendLayout()
        CType(Me.grdDetail, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.BindingSource1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grvDetail, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtREAKSIALERGI, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdITEM, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grvITEM, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdUOM, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grvUOM, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.deDATEEXPIRE, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.deDATEEXPIRE.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtTINGKATALERGI, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdKDDOCTOR, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grvKDDOCTOR, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cboTINGKATALERGI, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tab2.SuspendLayout()
        CType(Me.grdDetail2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ContextMenuStrip2.SuspendLayout()
        CType(Me.BindingSource2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grvDetail2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDOSIS, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.deTANGGAL_MULAI, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.deTANGGAL_MULAI.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.deTANGGAL_STOP, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.deTANGGAL_STOP.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkISOBATDILANJUTKAN_1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkISOBATDILANJUTKAN_2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdITEM2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grvITEM2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdKDUOM2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtFREKUENSI, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdKDDOCTOR2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grvKDDOCTOR2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtALASANMAKAN_OBAT, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtKETERANGAN, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdSIGNA, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grvSIGNA, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tab3.SuspendLayout()
        CType(Me.txtKET.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem9, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem10, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem16, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem17, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem18, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem19, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem20, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem7, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem11, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
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
        Me.barDockControlTop.Size = New System.Drawing.Size(834, 0)
        '
        'barDockControlBottom
        '
        Me.barDockControlBottom.CausesValidation = False
        Me.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.barDockControlBottom.Location = New System.Drawing.Point(0, 635)
        Me.barDockControlBottom.Size = New System.Drawing.Size(834, 22)
        '
        'barDockControlLeft
        '
        Me.barDockControlLeft.CausesValidation = False
        Me.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left
        Me.barDockControlLeft.Location = New System.Drawing.Point(0, 0)
        Me.barDockControlLeft.Size = New System.Drawing.Size(0, 635)
        '
        'barDockControlRight
        '
        Me.barDockControlRight.CausesValidation = False
        Me.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right
        Me.barDockControlRight.Location = New System.Drawing.Point(834, 0)
        Me.barDockControlRight.Size = New System.Drawing.Size(0, 635)
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
        'GroupControl1
        '
        Me.GroupControl1.Controls.Add(Me.LayoutControl1)
        Me.GroupControl1.Dock = System.Windows.Forms.DockStyle.Top
        Me.GroupControl1.Location = New System.Drawing.Point(0, 0)
        Me.GroupControl1.Name = "GroupControl1"
        Me.GroupControl1.Size = New System.Drawing.Size(834, 95)
        Me.GroupControl1.TabIndex = 4
        Me.GroupControl1.Text = "IDENTITAS"
        '
        'LayoutControl1
        '
        Me.LayoutControl1.Controls.Add(Me.txtTujuan)
        Me.LayoutControl1.Controls.Add(Me.txtNoRegister)
        Me.LayoutControl1.Controls.Add(Me.txtNamaPasien)
        Me.LayoutControl1.Controls.Add(Me.txtNoPasien)
        Me.LayoutControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LayoutControl1.Location = New System.Drawing.Point(2, 20)
        Me.LayoutControl1.Name = "LayoutControl1"
        Me.LayoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = New System.Drawing.Rectangle(446, 162, 250, 350)
        Me.LayoutControl1.Root = Me.LayoutControlGroup1
        Me.LayoutControl1.Size = New System.Drawing.Size(830, 73)
        Me.LayoutControl1.TabIndex = 0
        Me.LayoutControl1.Text = "LayoutControl1"
        '
        'txtTujuan
        '
        Me.txtTujuan.Location = New System.Drawing.Point(433, 36)
        Me.txtTujuan.MenuManager = Me.barManager
        Me.txtTujuan.Name = "txtTujuan"
        Me.txtTujuan.Size = New System.Drawing.Size(385, 20)
        Me.txtTujuan.StyleController = Me.LayoutControl1
        Me.txtTujuan.TabIndex = 6
        '
        'txtNoRegister
        '
        Me.txtNoRegister.Location = New System.Drawing.Point(433, 12)
        Me.txtNoRegister.MenuManager = Me.barManager
        Me.txtNoRegister.Name = "txtNoRegister"
        Me.txtNoRegister.Properties.ReadOnly = True
        Me.txtNoRegister.Size = New System.Drawing.Size(385, 20)
        Me.txtNoRegister.StyleController = Me.LayoutControl1
        Me.txtNoRegister.TabIndex = 6
        '
        'txtNamaPasien
        '
        Me.txtNamaPasien.Location = New System.Drawing.Point(117, 36)
        Me.txtNamaPasien.MenuManager = Me.barManager
        Me.txtNamaPasien.Name = "txtNamaPasien"
        Me.txtNamaPasien.Properties.ReadOnly = True
        Me.txtNamaPasien.Size = New System.Drawing.Size(207, 20)
        Me.txtNamaPasien.StyleController = Me.LayoutControl1
        Me.txtNamaPasien.TabIndex = 5
        '
        'txtNoPasien
        '
        Me.txtNoPasien.Location = New System.Drawing.Point(117, 12)
        Me.txtNoPasien.MenuManager = Me.barManager
        Me.txtNoPasien.Name = "txtNoPasien"
        Me.txtNoPasien.Properties.ReadOnly = True
        Me.txtNoPasien.Size = New System.Drawing.Size(207, 20)
        Me.txtNoPasien.StyleController = Me.LayoutControl1
        Me.txtNoPasien.TabIndex = 4
        '
        'LayoutControlGroup1
        '
        Me.LayoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.LayoutControlGroup1.GroupBordersVisible = False
        Me.LayoutControlGroup1.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlItem1, Me.LayoutControlItem5, Me.LayoutControlItem6, Me.LayoutControlItem2})
        Me.LayoutControlGroup1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlGroup1.Name = "Root"
        Me.LayoutControlGroup1.Size = New System.Drawing.Size(830, 73)
        Me.LayoutControlGroup1.TextVisible = False
        '
        'LayoutControlItem1
        '
        Me.LayoutControlItem1.AppearanceItemCaption.Options.UseTextOptions = True
        Me.LayoutControlItem1.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.LayoutControlItem1.Control = Me.txtNoPasien
        Me.LayoutControlItem1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlItem1.Name = "LayoutControlItem1"
        Me.LayoutControlItem1.Size = New System.Drawing.Size(316, 24)
        Me.LayoutControlItem1.Text = "No. Pasien :"
        Me.LayoutControlItem1.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.LayoutControlItem1.TextSize = New System.Drawing.Size(100, 20)
        Me.LayoutControlItem1.TextToControlDistance = 5
        '
        'LayoutControlItem5
        '
        Me.LayoutControlItem5.AppearanceItemCaption.Options.UseTextOptions = True
        Me.LayoutControlItem5.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.LayoutControlItem5.Control = Me.txtNoRegister
        Me.LayoutControlItem5.Location = New System.Drawing.Point(316, 0)
        Me.LayoutControlItem5.Name = "LayoutControlItem5"
        Me.LayoutControlItem5.Size = New System.Drawing.Size(494, 24)
        Me.LayoutControlItem5.Text = "No. Register :"
        Me.LayoutControlItem5.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.LayoutControlItem5.TextSize = New System.Drawing.Size(100, 20)
        Me.LayoutControlItem5.TextToControlDistance = 5
        '
        'LayoutControlItem6
        '
        Me.LayoutControlItem6.AppearanceItemCaption.Options.UseTextOptions = True
        Me.LayoutControlItem6.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.LayoutControlItem6.Control = Me.txtTujuan
        Me.LayoutControlItem6.Location = New System.Drawing.Point(316, 24)
        Me.LayoutControlItem6.Name = "LayoutControlItem6"
        Me.LayoutControlItem6.Size = New System.Drawing.Size(494, 29)
        Me.LayoutControlItem6.Text = "Poli / Ruangan :"
        Me.LayoutControlItem6.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.LayoutControlItem6.TextSize = New System.Drawing.Size(100, 20)
        Me.LayoutControlItem6.TextToControlDistance = 5
        '
        'LayoutControlItem2
        '
        Me.LayoutControlItem2.AppearanceItemCaption.Options.UseTextOptions = True
        Me.LayoutControlItem2.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.LayoutControlItem2.Control = Me.txtNamaPasien
        Me.LayoutControlItem2.Location = New System.Drawing.Point(0, 24)
        Me.LayoutControlItem2.Name = "LayoutControlItem2"
        Me.LayoutControlItem2.Size = New System.Drawing.Size(316, 29)
        Me.LayoutControlItem2.Text = "Nama Pasien :"
        Me.LayoutControlItem2.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.LayoutControlItem2.TextSize = New System.Drawing.Size(100, 20)
        Me.LayoutControlItem2.TextToControlDistance = 5
        '
        'ContextMenuStrip1
        '
        Me.ContextMenuStrip1.ImageScalingSize = New System.Drawing.Size(20, 20)
        Me.ContextMenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.DeleteToolStripMenuItem})
        Me.ContextMenuStrip1.Name = "ContextMenuStrip1"
        Me.ContextMenuStrip1.Size = New System.Drawing.Size(108, 26)
        '
        'DeleteToolStripMenuItem
        '
        Me.DeleteToolStripMenuItem.Name = "DeleteToolStripMenuItem"
        Me.DeleteToolStripMenuItem.Size = New System.Drawing.Size(107, 22)
        Me.DeleteToolStripMenuItem.Text = "Delete"
        '
        'Panel3
        '
        Me.Panel3.AutoScroll = True
        Me.Panel3.Controls.Add(Me.LayoutControl3)
        Me.Panel3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel3.Location = New System.Drawing.Point(0, 95)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(834, 540)
        Me.Panel3.TabIndex = 30
        '
        'LayoutControl3
        '
        Me.LayoutControl3.Controls.Add(Me.txtApoteker)
        Me.LayoutControl3.Controls.Add(Me.txtPerawat)
        Me.LayoutControl3.Controls.Add(Me.chkObatBawa_TIDAK)
        Me.LayoutControl3.Controls.Add(Me.chkObatBawa_YA)
        Me.LayoutControl3.Controls.Add(Me.chkAlergi_TIDAK)
        Me.LayoutControl3.Controls.Add(Me.chkAlergi_YA)
        Me.LayoutControl3.Controls.Add(Me.deDATE)
        Me.LayoutControl3.Controls.Add(Me.grdDokter)
        Me.LayoutControl3.Controls.Add(Me.tabControl)
        Me.LayoutControl3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LayoutControl3.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControl3.Name = "LayoutControl3"
        Me.LayoutControl3.Root = Me.LayoutControlGroup2
        Me.LayoutControl3.Size = New System.Drawing.Size(834, 540)
        Me.LayoutControl3.TabIndex = 20
        Me.LayoutControl3.Text = "LayoutControl3"
        '
        'txtApoteker
        '
        Me.txtApoteker.Location = New System.Drawing.Point(117, 508)
        Me.txtApoteker.MenuManager = Me.barManager
        Me.txtApoteker.Name = "txtApoteker"
        Me.txtApoteker.Size = New System.Drawing.Size(330, 20)
        Me.txtApoteker.StyleController = Me.LayoutControl3
        Me.txtApoteker.TabIndex = 35
        '
        'txtPerawat
        '
        Me.txtPerawat.Location = New System.Drawing.Point(117, 484)
        Me.txtPerawat.MenuManager = Me.barManager
        Me.txtPerawat.Name = "txtPerawat"
        Me.txtPerawat.Size = New System.Drawing.Size(330, 20)
        Me.txtPerawat.StyleController = Me.LayoutControl3
        Me.txtPerawat.TabIndex = 35
        '
        'chkObatBawa_TIDAK
        '
        Me.chkObatBawa_TIDAK.Location = New System.Drawing.Point(182, 36)
        Me.chkObatBawa_TIDAK.MenuManager = Me.barManager
        Me.chkObatBawa_TIDAK.Name = "chkObatBawa_TIDAK"
        Me.chkObatBawa_TIDAK.Properties.Caption = "Tidak"
        Me.chkObatBawa_TIDAK.Size = New System.Drawing.Size(640, 19)
        Me.chkObatBawa_TIDAK.StyleController = Me.LayoutControl3
        Me.chkObatBawa_TIDAK.TabIndex = 27
        '
        'chkObatBawa_YA
        '
        Me.chkObatBawa_YA.Location = New System.Drawing.Point(137, 36)
        Me.chkObatBawa_YA.MenuManager = Me.barManager
        Me.chkObatBawa_YA.Name = "chkObatBawa_YA"
        Me.chkObatBawa_YA.Properties.Caption = "Ada"
        Me.chkObatBawa_YA.Size = New System.Drawing.Size(41, 19)
        Me.chkObatBawa_YA.StyleController = Me.LayoutControl3
        Me.chkObatBawa_YA.TabIndex = 26
        '
        'chkAlergi_TIDAK
        '
        Me.chkAlergi_TIDAK.Location = New System.Drawing.Point(182, 12)
        Me.chkAlergi_TIDAK.MenuManager = Me.barManager
        Me.chkAlergi_TIDAK.Name = "chkAlergi_TIDAK"
        Me.chkAlergi_TIDAK.Properties.Caption = "Tidak"
        Me.chkAlergi_TIDAK.Size = New System.Drawing.Size(640, 19)
        Me.chkAlergi_TIDAK.StyleController = Me.LayoutControl3
        Me.chkAlergi_TIDAK.TabIndex = 25
        '
        'chkAlergi_YA
        '
        Me.chkAlergi_YA.Location = New System.Drawing.Point(137, 12)
        Me.chkAlergi_YA.MenuManager = Me.barManager
        Me.chkAlergi_YA.Name = "chkAlergi_YA"
        Me.chkAlergi_YA.Properties.Caption = "Ya"
        Me.chkAlergi_YA.Size = New System.Drawing.Size(41, 19)
        Me.chkAlergi_YA.StyleController = Me.LayoutControl3
        Me.chkAlergi_YA.TabIndex = 24
        '
        'deDATE
        '
        Me.deDATE.EditValue = Nothing
        Me.deDATE.Location = New System.Drawing.Point(556, 460)
        Me.deDATE.MenuManager = Me.barManager
        Me.deDATE.Name = "deDATE"
        Me.deDATE.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.deDATE.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.deDATE.Size = New System.Drawing.Size(266, 20)
        Me.deDATE.StyleController = Me.LayoutControl3
        Me.deDATE.TabIndex = 23
        '
        'grdDokter
        '
        Me.grdDokter.EditValue = ""
        Me.grdDokter.Location = New System.Drawing.Point(117, 460)
        Me.grdDokter.MenuManager = Me.barManager
        Me.grdDokter.Name = "grdDokter"
        Me.grdDokter.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.grdDokter.Properties.NullText = ""
        Me.grdDokter.Properties.View = Me.SearchLookUpEdit1View
        Me.grdDokter.Size = New System.Drawing.Size(330, 20)
        Me.grdDokter.StyleController = Me.LayoutControl3
        Me.grdDokter.TabIndex = 20
        '
        'SearchLookUpEdit1View
        '
        Me.SearchLookUpEdit1View.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.colNAMA})
        Me.SearchLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.SearchLookUpEdit1View.Name = "SearchLookUpEdit1View"
        Me.SearchLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.SearchLookUpEdit1View.OptionsView.ShowGroupPanel = False
        '
        'colNAMA
        '
        Me.colNAMA.Caption = "Nama"
        Me.colNAMA.FieldName = "NAME_DISPLAY"
        Me.colNAMA.Name = "colNAMA"
        Me.colNAMA.Visible = True
        Me.colNAMA.VisibleIndex = 0
        '
        'tabControl
        '
        Me.tabControl.Location = New System.Drawing.Point(12, 60)
        Me.tabControl.Name = "tabControl"
        Me.tabControl.SelectedTabPage = Me.tab1
        Me.tabControl.Size = New System.Drawing.Size(810, 396)
        Me.tabControl.TabIndex = 19
        Me.tabControl.TabPages.AddRange(New DevExpress.XtraTab.XtraTabPage() {Me.tab1, Me.tab2, Me.tab3})
        '
        'tab1
        '
        Me.tab1.Controls.Add(Me.grdDetail)
        Me.tab1.Name = "tab1"
        Me.tab1.Size = New System.Drawing.Size(804, 368)
        Me.tab1.Text = "Daftar obat yang menimbulkan alergi"
        '
        'grdDetail
        '
        Me.grdDetail.ContextMenuStrip = Me.ContextMenuStrip1
        Me.grdDetail.DataSource = Me.BindingSource1
        Me.grdDetail.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grdDetail.Location = New System.Drawing.Point(0, 0)
        Me.grdDetail.MainView = Me.grvDetail
        Me.grdDetail.MenuManager = Me.barManager
        Me.grdDetail.Name = "grdDetail"
        Me.grdDetail.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.grdITEM, Me.grdUOM, Me.deDATEEXPIRE, Me.txtTINGKATALERGI, Me.txtREAKSIALERGI, Me.grdKDDOCTOR, Me.cboTINGKATALERGI})
        Me.grdDetail.Size = New System.Drawing.Size(804, 368)
        Me.grdDetail.TabIndex = 19
        Me.grdDetail.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.grvDetail})
        '
        'BindingSource1
        '
        Me.BindingSource1.DataSource = GetType(DataAccess.S_DIGITAL_FARMASI_REKONSILIASIOBAT_D1)
        '
        'grvDetail
        '
        Me.grvDetail.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.colITEMOBAT, Me.colREAKSIALERGI, Me.colTingkatAlergi})
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
        'colITEMOBAT
        '
        Me.colITEMOBAT.Caption = "Obat"
        Me.colITEMOBAT.FieldName = "ITEMOBAT"
        Me.colITEMOBAT.Name = "colITEMOBAT"
        Me.colITEMOBAT.Visible = True
        Me.colITEMOBAT.VisibleIndex = 0
        '
        'colREAKSIALERGI
        '
        Me.colREAKSIALERGI.Caption = "Reaksi Alergi"
        Me.colREAKSIALERGI.ColumnEdit = Me.txtREAKSIALERGI
        Me.colREAKSIALERGI.FieldName = "REAKSIALERGI"
        Me.colREAKSIALERGI.Name = "colREAKSIALERGI"
        Me.colREAKSIALERGI.Visible = True
        Me.colREAKSIALERGI.VisibleIndex = 1
        '
        'txtREAKSIALERGI
        '
        Me.txtREAKSIALERGI.Name = "txtREAKSIALERGI"
        '
        'colTingkatAlergi
        '
        Me.colTingkatAlergi.Caption = "Tingkat Alergi"
        Me.colTingkatAlergi.FieldName = "TINGKATALERGI"
        Me.colTingkatAlergi.Name = "colTingkatAlergi"
        '
        'grdITEM
        '
        Me.grdITEM.AutoHeight = False
        Me.grdITEM.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.grdITEM.Name = "grdITEM"
        Me.grdITEM.NullText = ""
        Me.grdITEM.PopupFormMinSize = New System.Drawing.Size(600, 300)
        Me.grdITEM.View = Me.grvITEM
        '
        'grvITEM
        '
        Me.grvITEM.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn3})
        Me.grvITEM.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.grvITEM.Name = "grvITEM"
        Me.grvITEM.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.grvITEM.OptionsView.ShowAutoFilterRow = True
        Me.grvITEM.OptionsView.ShowGroupPanel = False
        '
        'GridColumn3
        '
        Me.GridColumn3.Caption = "Nama Dagang"
        Me.GridColumn3.FieldName = "NMITEM2"
        Me.GridColumn3.Name = "GridColumn3"
        Me.GridColumn3.Visible = True
        Me.GridColumn3.VisibleIndex = 0
        '
        'grdUOM
        '
        Me.grdUOM.AutoHeight = False
        Me.grdUOM.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.grdUOM.Name = "grdUOM"
        Me.grdUOM.NullText = ""
        Me.grdUOM.PopupFormMinSize = New System.Drawing.Size(600, 300)
        Me.grdUOM.View = Me.grvUOM
        '
        'grvUOM
        '
        Me.grvUOM.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn4})
        Me.grvUOM.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.grvUOM.Name = "grvUOM"
        Me.grvUOM.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.grvUOM.OptionsView.ShowAutoFilterRow = True
        Me.grvUOM.OptionsView.ShowGroupPanel = False
        '
        'GridColumn4
        '
        Me.GridColumn4.Caption = "Keterangan"
        Me.GridColumn4.FieldName = "DESCRIPTION"
        Me.GridColumn4.Name = "GridColumn4"
        Me.GridColumn4.Visible = True
        Me.GridColumn4.VisibleIndex = 0
        '
        'deDATEEXPIRE
        '
        Me.deDATEEXPIRE.AutoHeight = False
        Me.deDATEEXPIRE.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.deDATEEXPIRE.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
        Me.deDATEEXPIRE.Mask.EditMask = "dd/MM/yyyy"
        Me.deDATEEXPIRE.Mask.UseMaskAsDisplayFormat = True
        Me.deDATEEXPIRE.Name = "deDATEEXPIRE"
        '
        'txtTINGKATALERGI
        '
        Me.txtTINGKATALERGI.Name = "txtTINGKATALERGI"
        '
        'grdKDDOCTOR
        '
        Me.grdKDDOCTOR.AutoHeight = False
        Me.grdKDDOCTOR.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.grdKDDOCTOR.Name = "grdKDDOCTOR"
        Me.grdKDDOCTOR.NullText = ""
        Me.grdKDDOCTOR.View = Me.grvKDDOCTOR
        '
        'grvKDDOCTOR
        '
        Me.grvKDDOCTOR.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn2})
        Me.grvKDDOCTOR.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.grvKDDOCTOR.Name = "grvKDDOCTOR"
        Me.grvKDDOCTOR.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.grvKDDOCTOR.OptionsView.ShowGroupPanel = False
        '
        'GridColumn2
        '
        Me.GridColumn2.Caption = "Tampilan Nama"
        Me.GridColumn2.FieldName = "NAME_DISPLAY"
        Me.GridColumn2.Name = "GridColumn2"
        Me.GridColumn2.Visible = True
        Me.GridColumn2.VisibleIndex = 0
        '
        'cboTINGKATALERGI
        '
        Me.cboTINGKATALERGI.AutoHeight = False
        Me.cboTINGKATALERGI.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cboTINGKATALERGI.Items.AddRange(New Object() {"Ringan", "Sedang", "Berat"})
        Me.cboTINGKATALERGI.Name = "cboTINGKATALERGI"
        '
        'tab2
        '
        Me.tab2.Controls.Add(Me.grdDetail2)
        Me.tab2.Name = "tab2"
        Me.tab2.Size = New System.Drawing.Size(804, 368)
        Me.tab2.Text = "Semua jenis obat ( Obat resep, bebas, herbal )"
        '
        'grdDetail2
        '
        Me.grdDetail2.ContextMenuStrip = Me.ContextMenuStrip2
        Me.grdDetail2.DataSource = Me.BindingSource2
        Me.grdDetail2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grdDetail2.Location = New System.Drawing.Point(0, 0)
        Me.grdDetail2.MainView = Me.grvDetail2
        Me.grdDetail2.MenuManager = Me.barManager
        Me.grdDetail2.Name = "grdDetail2"
        Me.grdDetail2.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.grdITEM2, Me.grdKDUOM2, Me.deTANGGAL_MULAI, Me.txtDOSIS, Me.txtFREKUENSI, Me.grdKDDOCTOR2, Me.deTANGGAL_STOP, Me.txtALASANMAKAN_OBAT, Me.chkISOBATDILANJUTKAN_1, Me.chkISOBATDILANJUTKAN_2, Me.txtKETERANGAN, Me.grdSIGNA})
        Me.grdDetail2.Size = New System.Drawing.Size(804, 368)
        Me.grdDetail2.TabIndex = 19
        Me.grdDetail2.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.grvDetail2})
        '
        'ContextMenuStrip2
        '
        Me.ContextMenuStrip2.ImageScalingSize = New System.Drawing.Size(20, 20)
        Me.ContextMenuStrip2.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.DeleteToolStripMenuItem1})
        Me.ContextMenuStrip2.Name = "ContextMenuStrip2"
        Me.ContextMenuStrip2.Size = New System.Drawing.Size(108, 26)
        '
        'DeleteToolStripMenuItem1
        '
        Me.DeleteToolStripMenuItem1.Name = "DeleteToolStripMenuItem1"
        Me.DeleteToolStripMenuItem1.Size = New System.Drawing.Size(107, 22)
        Me.DeleteToolStripMenuItem1.Text = "Delete"
        '
        'BindingSource2
        '
        Me.BindingSource2.DataSource = GetType(DataAccess.S_DIGITAL_FARMASI_REKONSILIASIOBAT_D2)
        '
        'grvDetail2
        '
        Me.grvDetail2.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.colITEMOBAT2, Me.colDOSIS, Me.colFREKUENSI, Me.colTANGGAL_MULAI, Me.colTANGGAL_STOP, Me.colTANGGAL_STOP_STR, Me.colISOBATDILANJUTKAN_1, Me.colISOBATDILANJUTKAN_2})
        Me.grvDetail2.GridControl = Me.grdDetail2
        Me.grvDetail2.Name = "grvDetail2"
        Me.grvDetail2.OptionsDetail.EnableMasterViewMode = False
        Me.grvDetail2.OptionsNavigation.AutoFocusNewRow = True
        Me.grvDetail2.OptionsNavigation.EnterMoveNextColumn = True
        Me.grvDetail2.OptionsView.EnableAppearanceEvenRow = True
        Me.grvDetail2.OptionsView.EnableAppearanceOddRow = True
        Me.grvDetail2.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Bottom
        Me.grvDetail2.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never
        Me.grvDetail2.OptionsView.ShowFooter = True
        Me.grvDetail2.OptionsView.ShowGroupPanel = False
        '
        'colITEMOBAT2
        '
        Me.colITEMOBAT2.Caption = "Obat"
        Me.colITEMOBAT2.FieldName = "ITEMOBAT"
        Me.colITEMOBAT2.Name = "colITEMOBAT2"
        Me.colITEMOBAT2.Visible = True
        Me.colITEMOBAT2.VisibleIndex = 0
        Me.colITEMOBAT2.Width = 67
        '
        'colDOSIS
        '
        Me.colDOSIS.Caption = "Dosis"
        Me.colDOSIS.ColumnEdit = Me.txtDOSIS
        Me.colDOSIS.FieldName = "DOSIS"
        Me.colDOSIS.Name = "colDOSIS"
        Me.colDOSIS.Visible = True
        Me.colDOSIS.VisibleIndex = 1
        Me.colDOSIS.Width = 67
        '
        'txtDOSIS
        '
        Me.txtDOSIS.Name = "txtDOSIS"
        '
        'colFREKUENSI
        '
        Me.colFREKUENSI.Caption = "Frekuensi"
        Me.colFREKUENSI.FieldName = "FREKUENSI"
        Me.colFREKUENSI.Name = "colFREKUENSI"
        Me.colFREKUENSI.Visible = True
        Me.colFREKUENSI.VisibleIndex = 2
        Me.colFREKUENSI.Width = 67
        '
        'colTANGGAL_MULAI
        '
        Me.colTANGGAL_MULAI.Caption = "Tgl Mulai"
        Me.colTANGGAL_MULAI.ColumnEdit = Me.deTANGGAL_MULAI
        Me.colTANGGAL_MULAI.DisplayFormat.FormatString = "dd-MM-yyyy"
        Me.colTANGGAL_MULAI.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.colTANGGAL_MULAI.FieldName = "TANGGAL_MULAI"
        Me.colTANGGAL_MULAI.Name = "colTANGGAL_MULAI"
        Me.colTANGGAL_MULAI.Visible = True
        Me.colTANGGAL_MULAI.VisibleIndex = 3
        Me.colTANGGAL_MULAI.Width = 67
        '
        'deTANGGAL_MULAI
        '
        Me.deTANGGAL_MULAI.AutoHeight = False
        Me.deTANGGAL_MULAI.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.deTANGGAL_MULAI.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
        Me.deTANGGAL_MULAI.Mask.EditMask = "dd-MM-yyyy"
        Me.deTANGGAL_MULAI.Mask.UseMaskAsDisplayFormat = True
        Me.deTANGGAL_MULAI.Name = "deTANGGAL_MULAI"
        '
        'colTANGGAL_STOP
        '
        Me.colTANGGAL_STOP.Caption = "Tgl Stop"
        Me.colTANGGAL_STOP.ColumnEdit = Me.deTANGGAL_STOP
        Me.colTANGGAL_STOP.DisplayFormat.FormatString = "dd-MM-yyyy"
        Me.colTANGGAL_STOP.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.colTANGGAL_STOP.FieldName = "TANGGAL_STOP"
        Me.colTANGGAL_STOP.Name = "colTANGGAL_STOP"
        Me.colTANGGAL_STOP.Width = 67
        '
        'deTANGGAL_STOP
        '
        Me.deTANGGAL_STOP.AutoHeight = False
        Me.deTANGGAL_STOP.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.deTANGGAL_STOP.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.deTANGGAL_STOP.Mask.EditMask = "dd-MM-yyyy"
        Me.deTANGGAL_STOP.Name = "deTANGGAL_STOP"
        '
        'colTANGGAL_STOP_STR
        '
        Me.colTANGGAL_STOP_STR.Caption = "Tgl Stop"
        Me.colTANGGAL_STOP_STR.FieldName = "TANGGAL_STOP_STR"
        Me.colTANGGAL_STOP_STR.Name = "colTANGGAL_STOP_STR"
        Me.colTANGGAL_STOP_STR.Visible = True
        Me.colTANGGAL_STOP_STR.VisibleIndex = 4
        '
        'colISOBATDILANJUTKAN_1
        '
        Me.colISOBATDILANJUTKAN_1.Caption = "Obat dilanjutkan"
        Me.colISOBATDILANJUTKAN_1.ColumnEdit = Me.chkISOBATDILANJUTKAN_1
        Me.colISOBATDILANJUTKAN_1.FieldName = "ISOBATDILANJUTKAN_1"
        Me.colISOBATDILANJUTKAN_1.Name = "colISOBATDILANJUTKAN_1"
        Me.colISOBATDILANJUTKAN_1.Visible = True
        Me.colISOBATDILANJUTKAN_1.VisibleIndex = 5
        Me.colISOBATDILANJUTKAN_1.Width = 93
        '
        'chkISOBATDILANJUTKAN_1
        '
        Me.chkISOBATDILANJUTKAN_1.AutoHeight = False
        Me.chkISOBATDILANJUTKAN_1.Name = "chkISOBATDILANJUTKAN_1"
        Me.chkISOBATDILANJUTKAN_1.NullStyle = DevExpress.XtraEditors.Controls.StyleIndeterminate.Unchecked
        '
        'colISOBATDILANJUTKAN_2
        '
        Me.colISOBATDILANJUTKAN_2.Caption = "Obat tidak dilanjutkan"
        Me.colISOBATDILANJUTKAN_2.ColumnEdit = Me.chkISOBATDILANJUTKAN_2
        Me.colISOBATDILANJUTKAN_2.FieldName = "ISOBATDILANJUTKAN_2"
        Me.colISOBATDILANJUTKAN_2.Name = "colISOBATDILANJUTKAN_2"
        Me.colISOBATDILANJUTKAN_2.Visible = True
        Me.colISOBATDILANJUTKAN_2.VisibleIndex = 6
        Me.colISOBATDILANJUTKAN_2.Width = 59
        '
        'chkISOBATDILANJUTKAN_2
        '
        Me.chkISOBATDILANJUTKAN_2.AutoHeight = False
        Me.chkISOBATDILANJUTKAN_2.Name = "chkISOBATDILANJUTKAN_2"
        Me.chkISOBATDILANJUTKAN_2.NullStyle = DevExpress.XtraEditors.Controls.StyleIndeterminate.Unchecked
        '
        'grdITEM2
        '
        Me.grdITEM2.AutoHeight = False
        Me.grdITEM2.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.grdITEM2.Name = "grdITEM2"
        Me.grdITEM2.NullText = ""
        Me.grdITEM2.PopupFormMinSize = New System.Drawing.Size(600, 300)
        Me.grdITEM2.View = Me.grvITEM2
        '
        'grvITEM2
        '
        Me.grvITEM2.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn6})
        Me.grvITEM2.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.grvITEM2.Name = "grvITEM2"
        Me.grvITEM2.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.grvITEM2.OptionsView.ShowAutoFilterRow = True
        Me.grvITEM2.OptionsView.ShowGroupPanel = False
        '
        'GridColumn6
        '
        Me.GridColumn6.Caption = "Nama Dagang"
        Me.GridColumn6.FieldName = "NMITEM2"
        Me.GridColumn6.Name = "GridColumn6"
        Me.GridColumn6.Visible = True
        Me.GridColumn6.VisibleIndex = 0
        '
        'grdKDUOM2
        '
        Me.grdKDUOM2.AutoHeight = False
        Me.grdKDUOM2.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.grdKDUOM2.Name = "grdKDUOM2"
        Me.grdKDUOM2.NullText = ""
        Me.grdKDUOM2.PopupFormMinSize = New System.Drawing.Size(600, 300)
        Me.grdKDUOM2.View = Me.GridView3
        '
        'GridView3
        '
        Me.GridView3.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn9})
        Me.GridView3.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.GridView3.Name = "GridView3"
        Me.GridView3.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.GridView3.OptionsView.ShowAutoFilterRow = True
        Me.GridView3.OptionsView.ShowGroupPanel = False
        '
        'GridColumn9
        '
        Me.GridColumn9.Caption = "Keterangan"
        Me.GridColumn9.FieldName = "DESCRIPTION"
        Me.GridColumn9.Name = "GridColumn9"
        Me.GridColumn9.Visible = True
        Me.GridColumn9.VisibleIndex = 0
        '
        'txtFREKUENSI
        '
        Me.txtFREKUENSI.Name = "txtFREKUENSI"
        '
        'grdKDDOCTOR2
        '
        Me.grdKDDOCTOR2.AutoHeight = False
        Me.grdKDDOCTOR2.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.grdKDDOCTOR2.Name = "grdKDDOCTOR2"
        Me.grdKDDOCTOR2.NullText = ""
        Me.grdKDDOCTOR2.View = Me.grvKDDOCTOR2
        '
        'grvKDDOCTOR2
        '
        Me.grvKDDOCTOR2.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn13})
        Me.grvKDDOCTOR2.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.grvKDDOCTOR2.Name = "grvKDDOCTOR2"
        Me.grvKDDOCTOR2.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.grvKDDOCTOR2.OptionsView.ShowGroupPanel = False
        '
        'GridColumn13
        '
        Me.GridColumn13.Caption = "Tampilan Nama"
        Me.GridColumn13.FieldName = "NAME_DISPLAY"
        Me.GridColumn13.Name = "GridColumn13"
        Me.GridColumn13.Visible = True
        Me.GridColumn13.VisibleIndex = 0
        '
        'txtALASANMAKAN_OBAT
        '
        Me.txtALASANMAKAN_OBAT.Name = "txtALASANMAKAN_OBAT"
        '
        'txtKETERANGAN
        '
        Me.txtKETERANGAN.Name = "txtKETERANGAN"
        '
        'grdSIGNA
        '
        Me.grdSIGNA.AutoHeight = False
        Me.grdSIGNA.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.grdSIGNA.Name = "grdSIGNA"
        Me.grdSIGNA.NullText = ""
        Me.grdSIGNA.View = Me.grvSIGNA
        '
        'grvSIGNA
        '
        Me.grvSIGNA.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.colSigna})
        Me.grvSIGNA.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.grvSIGNA.Name = "grvSIGNA"
        Me.grvSIGNA.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.grvSIGNA.OptionsView.ShowGroupPanel = False
        '
        'colSigna
        '
        Me.colSigna.Caption = "Frekuensi"
        Me.colSigna.FieldName = "MEMO"
        Me.colSigna.Name = "colSigna"
        Me.colSigna.Visible = True
        Me.colSigna.VisibleIndex = 0
        '
        'tab3
        '
        Me.tab3.Controls.Add(Me.txtKET)
        Me.tab3.Name = "tab3"
        Me.tab3.Size = New System.Drawing.Size(804, 368)
        Me.tab3.Text = "Keterangan"
        '
        'txtKET
        '
        Me.txtKET.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtKET.Location = New System.Drawing.Point(0, 0)
        Me.txtKET.MenuManager = Me.barManager
        Me.txtKET.Name = "txtKET"
        Me.txtKET.Size = New System.Drawing.Size(804, 368)
        Me.txtKET.TabIndex = 0
        '
        'LayoutControlGroup2
        '
        Me.LayoutControlGroup2.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.LayoutControlGroup2.GroupBordersVisible = False
        Me.LayoutControlGroup2.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlItem9, Me.LayoutControlItem10, Me.LayoutControlItem16, Me.LayoutControlItem17, Me.LayoutControlItem18, Me.LayoutControlItem19, Me.LayoutControlItem20, Me.LayoutControlItem7, Me.LayoutControlItem11})
        Me.LayoutControlGroup2.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlGroup2.Name = "LayoutControlGroup2"
        Me.LayoutControlGroup2.Size = New System.Drawing.Size(834, 540)
        Me.LayoutControlGroup2.TextVisible = False
        '
        'LayoutControlItem9
        '
        Me.LayoutControlItem9.Control = Me.tabControl
        Me.LayoutControlItem9.Location = New System.Drawing.Point(0, 48)
        Me.LayoutControlItem9.Name = "LayoutControlItem9"
        Me.LayoutControlItem9.Size = New System.Drawing.Size(814, 400)
        Me.LayoutControlItem9.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem9.TextVisible = False
        '
        'LayoutControlItem10
        '
        Me.LayoutControlItem10.AppearanceItemCaption.Options.UseTextOptions = True
        Me.LayoutControlItem10.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.LayoutControlItem10.Control = Me.grdDokter
        Me.LayoutControlItem10.Location = New System.Drawing.Point(0, 448)
        Me.LayoutControlItem10.Name = "LayoutControlItem10"
        Me.LayoutControlItem10.Size = New System.Drawing.Size(439, 24)
        Me.LayoutControlItem10.Text = "Dokter :"
        Me.LayoutControlItem10.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.LayoutControlItem10.TextSize = New System.Drawing.Size(100, 20)
        Me.LayoutControlItem10.TextToControlDistance = 5
        '
        'LayoutControlItem16
        '
        Me.LayoutControlItem16.AppearanceItemCaption.Options.UseTextOptions = True
        Me.LayoutControlItem16.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.LayoutControlItem16.Control = Me.deDATE
        Me.LayoutControlItem16.Location = New System.Drawing.Point(439, 448)
        Me.LayoutControlItem16.Name = "LayoutControlItem16"
        Me.LayoutControlItem16.Size = New System.Drawing.Size(375, 72)
        Me.LayoutControlItem16.Text = "Tanggal :"
        Me.LayoutControlItem16.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.LayoutControlItem16.TextSize = New System.Drawing.Size(100, 13)
        Me.LayoutControlItem16.TextToControlDistance = 5
        '
        'LayoutControlItem17
        '
        Me.LayoutControlItem17.AppearanceItemCaption.Options.UseTextOptions = True
        Me.LayoutControlItem17.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.LayoutControlItem17.Control = Me.chkAlergi_YA
        Me.LayoutControlItem17.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlItem17.Name = "LayoutControlItem17"
        Me.LayoutControlItem17.Size = New System.Drawing.Size(170, 24)
        Me.LayoutControlItem17.Text = "Alergi :"
        Me.LayoutControlItem17.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.LayoutControlItem17.TextSize = New System.Drawing.Size(120, 20)
        Me.LayoutControlItem17.TextToControlDistance = 5
        '
        'LayoutControlItem18
        '
        Me.LayoutControlItem18.Control = Me.chkAlergi_TIDAK
        Me.LayoutControlItem18.Location = New System.Drawing.Point(170, 0)
        Me.LayoutControlItem18.Name = "LayoutControlItem18"
        Me.LayoutControlItem18.Size = New System.Drawing.Size(644, 24)
        Me.LayoutControlItem18.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem18.TextVisible = False
        '
        'LayoutControlItem19
        '
        Me.LayoutControlItem19.AppearanceItemCaption.Options.UseTextOptions = True
        Me.LayoutControlItem19.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.LayoutControlItem19.Control = Me.chkObatBawa_YA
        Me.LayoutControlItem19.Location = New System.Drawing.Point(0, 24)
        Me.LayoutControlItem19.Name = "LayoutControlItem19"
        Me.LayoutControlItem19.Size = New System.Drawing.Size(170, 24)
        Me.LayoutControlItem19.Text = "Obat yang dibawa :"
        Me.LayoutControlItem19.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.LayoutControlItem19.TextSize = New System.Drawing.Size(120, 20)
        Me.LayoutControlItem19.TextToControlDistance = 5
        '
        'LayoutControlItem20
        '
        Me.LayoutControlItem20.Control = Me.chkObatBawa_TIDAK
        Me.LayoutControlItem20.Location = New System.Drawing.Point(170, 24)
        Me.LayoutControlItem20.Name = "LayoutControlItem20"
        Me.LayoutControlItem20.Size = New System.Drawing.Size(644, 24)
        Me.LayoutControlItem20.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem20.TextVisible = False
        '
        'LayoutControlItem7
        '
        Me.LayoutControlItem7.AppearanceItemCaption.Options.UseTextOptions = True
        Me.LayoutControlItem7.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.LayoutControlItem7.Control = Me.txtPerawat
        Me.LayoutControlItem7.Location = New System.Drawing.Point(0, 472)
        Me.LayoutControlItem7.Name = "LayoutControlItem7"
        Me.LayoutControlItem7.Size = New System.Drawing.Size(439, 24)
        Me.LayoutControlItem7.Text = "Perawat :"
        Me.LayoutControlItem7.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.LayoutControlItem7.TextSize = New System.Drawing.Size(100, 20)
        Me.LayoutControlItem7.TextToControlDistance = 5
        '
        'LayoutControlItem11
        '
        Me.LayoutControlItem11.AppearanceItemCaption.Options.UseTextOptions = True
        Me.LayoutControlItem11.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.LayoutControlItem11.Control = Me.txtApoteker
        Me.LayoutControlItem11.Location = New System.Drawing.Point(0, 496)
        Me.LayoutControlItem11.Name = "LayoutControlItem11"
        Me.LayoutControlItem11.Size = New System.Drawing.Size(439, 24)
        Me.LayoutControlItem11.Text = "Apoteker :"
        Me.LayoutControlItem11.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.LayoutControlItem11.TextSize = New System.Drawing.Size(100, 20)
        Me.LayoutControlItem11.TextToControlDistance = 5
        '
        'fileDialog
        '
        Me.fileDialog.FileName = "OpenFileDialog1"
        '
        'frmRekonsiliasiObat
        '
        Me.Appearance.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(236, Byte), Integer), CType(CType(239, Byte), Integer))
        Me.Appearance.Options.UseBackColor = True
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(834, 657)
        Me.Controls.Add(Me.Panel3)
        Me.Controls.Add(Me.GroupControl1)
        Me.Controls.Add(Me.barDockControlLeft)
        Me.Controls.Add(Me.barDockControlRight)
        Me.Controls.Add(Me.barDockControlBottom)
        Me.Controls.Add(Me.barDockControlTop)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
        Me.KeyPreview = True
        Me.Name = "frmRekonsiliasiObat"
        Me.ShowIcon = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "FORMULIR REKONSILIASI OBAT"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        CType(Me.barManager, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.progressBarSave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.progressSave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GroupControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl1.ResumeLayout(False)
        CType(Me.LayoutControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.LayoutControl1.ResumeLayout(False)
        CType(Me.txtTujuan.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtNoRegister.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtNamaPasien.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtNoPasien.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ContextMenuStrip1.ResumeLayout(False)
        Me.Panel3.ResumeLayout(False)
        CType(Me.LayoutControl3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.LayoutControl3.ResumeLayout(False)
        CType(Me.txtApoteker.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtPerawat.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkObatBawa_TIDAK.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkObatBawa_YA.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkAlergi_TIDAK.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkAlergi_YA.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.deDATE.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.deDATE.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdDokter.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.SearchLookUpEdit1View, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.tabControl, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tabControl.ResumeLayout(False)
        Me.tab1.ResumeLayout(False)
        CType(Me.grdDetail, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.BindingSource1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grvDetail, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtREAKSIALERGI, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdITEM, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grvITEM, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdUOM, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grvUOM, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.deDATEEXPIRE.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.deDATEEXPIRE, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtTINGKATALERGI, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdKDDOCTOR, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grvKDDOCTOR, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cboTINGKATALERGI, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tab2.ResumeLayout(False)
        CType(Me.grdDetail2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ContextMenuStrip2.ResumeLayout(False)
        CType(Me.BindingSource2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grvDetail2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDOSIS, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.deTANGGAL_MULAI.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.deTANGGAL_MULAI, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.deTANGGAL_STOP.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.deTANGGAL_STOP, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkISOBATDILANJUTKAN_1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkISOBATDILANJUTKAN_2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdITEM2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grvITEM2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdKDUOM2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtFREKUENSI, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdKDDOCTOR2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grvKDDOCTOR2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtALASANMAKAN_OBAT, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtKETERANGAN, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdSIGNA, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grvSIGNA, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tab3.ResumeLayout(False)
        CType(Me.txtKET.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem9, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem10, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem16, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem17, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem18, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem19, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem20, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem7, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem11, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout

End Sub
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
    Friend WithEvents GroupControl1 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents LayoutControl1 As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents txtTujuan As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txtNoRegister As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txtNamaPasien As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txtNoPasien As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LayoutControlGroup1 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents LayoutControlItem1 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem2 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem5 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem6 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents ContextMenuStrip1 As ContextMenuStrip
    Friend WithEvents Panel3 As Panel
    Friend WithEvents fileDialog As OpenFileDialog
    Friend WithEvents tabControl As DevExpress.XtraTab.XtraTabControl
    Friend WithEvents tab1 As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents tab2 As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents grdDetail2 As DevExpress.XtraGrid.GridControl
    Friend WithEvents grvDetail2 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents colITEMOBAT2 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents grdITEM2 As DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit
    Friend WithEvents grvITEM2 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridColumn6 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents grdKDUOM2 As DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit
    Friend WithEvents GridView3 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridColumn9 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colDOSIS As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents txtDOSIS As DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit
    Friend WithEvents colFREKUENSI As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents txtFREKUENSI As DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit
    Friend WithEvents colTANGGAL_MULAI As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents deTANGGAL_MULAI As DevExpress.XtraEditors.Repository.RepositoryItemDateEdit
    Friend WithEvents colTANGGAL_STOP As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents deTANGGAL_STOP As DevExpress.XtraEditors.Repository.RepositoryItemDateEdit
    Friend WithEvents txtALASANMAKAN_OBAT As DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit
    Friend WithEvents colISOBATDILANJUTKAN_1 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents chkISOBATDILANJUTKAN_1 As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents colISOBATDILANJUTKAN_2 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents chkISOBATDILANJUTKAN_2 As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents txtKETERANGAN As DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit
    Friend WithEvents grdKDDOCTOR2 As DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit
    Friend WithEvents grvKDDOCTOR2 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridColumn13 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents LayoutControl3 As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents grdDokter As DevExpress.XtraEditors.SearchLookUpEdit
    Friend WithEvents SearchLookUpEdit1View As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents LayoutControlGroup2 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents LayoutControlItem9 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem10 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents deDATE As DevExpress.XtraEditors.DateEdit
    Friend WithEvents LayoutControlItem16 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents BindingSource1 As BindingSource
    Friend WithEvents BindingSource2 As BindingSource
    Friend WithEvents tab3 As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents txtKET As DevExpress.XtraEditors.MemoEdit
    Friend WithEvents colNAMA As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents DeleteToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ContextMenuStrip2 As ContextMenuStrip
    Friend WithEvents DeleteToolStripMenuItem1 As ToolStripMenuItem
    Friend WithEvents chkObatBawa_TIDAK As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents chkObatBawa_YA As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents chkAlergi_TIDAK As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents chkAlergi_YA As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents LayoutControlItem17 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem18 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem19 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem20 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents grdSIGNA As DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit
    Friend WithEvents grvSIGNA As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents colSigna As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents grdDetail As DevExpress.XtraGrid.GridControl
    Friend WithEvents grvDetail As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents colITEMOBAT As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cboTINGKATALERGI As DevExpress.XtraEditors.Repository.RepositoryItemComboBox
    Friend WithEvents colREAKSIALERGI As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents txtREAKSIALERGI As DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit
    Friend WithEvents grdITEM As DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit
    Friend WithEvents grvITEM As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridColumn3 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents grdUOM As DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit
    Friend WithEvents grvUOM As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridColumn4 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents deDATEEXPIRE As DevExpress.XtraEditors.Repository.RepositoryItemDateEdit
    Friend WithEvents txtTINGKATALERGI As DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit
    Friend WithEvents grdKDDOCTOR As DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit
    Friend WithEvents grvKDDOCTOR As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridColumn2 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colTingkatAlergi As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colTANGGAL_STOP_STR As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents txtApoteker As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txtPerawat As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LayoutControlItem7 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem11 As DevExpress.XtraLayout.LayoutControlItem
End Class
