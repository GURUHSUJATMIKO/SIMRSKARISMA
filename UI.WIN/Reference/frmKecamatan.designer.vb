<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmKecamatan
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
        Me.txtVCLAIM_KODE = New DevExpress.XtraEditors.TextEdit()
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
        Me.grdKECAMATAN = New DevExpress.XtraEditors.GridLookUpEdit()
        Me.GridView9 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.grdKDKABUPATEN = New DevExpress.XtraEditors.GridLookUpEdit()
        Me.GridView4 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn1 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn7 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.chkISDEFAULT = New DevExpress.XtraEditors.CheckEdit()
        Me.chkISACTIVE = New DevExpress.XtraEditors.CheckEdit()
        Me.txtMEMO = New DevExpress.XtraEditors.MemoEdit()
        Me.LayoutControlGroup1 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.lMEMO = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem1 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem2 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.lKABUPATEN = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem3 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.lVCLAIM_KODE = New DevExpress.XtraLayout.LayoutControlItem()
        Me.SimpleButton1 = New DevExpress.XtraEditors.SimpleButton()
        Me.LayoutControlItem4 = New DevExpress.XtraLayout.LayoutControlItem()
        CType(Me.layoutControl, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.layoutControl.SuspendLayout()
        CType(Me.txtVCLAIM_KODE.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.barManager, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.progressBarSave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.progressSave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdKECAMATAN.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView9, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdKDKABUPATEN.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkISDEFAULT.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkISACTIVE.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtMEMO.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lMEMO, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lKABUPATEN, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lVCLAIM_KODE, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem4, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'layoutControl
        '
        Me.layoutControl.Controls.Add(Me.SimpleButton1)
        Me.layoutControl.Controls.Add(Me.txtVCLAIM_KODE)
        Me.layoutControl.Controls.Add(Me.grdKECAMATAN)
        Me.layoutControl.Controls.Add(Me.grdKDKABUPATEN)
        Me.layoutControl.Controls.Add(Me.chkISDEFAULT)
        Me.layoutControl.Controls.Add(Me.chkISACTIVE)
        Me.layoutControl.Controls.Add(Me.txtMEMO)
        Me.layoutControl.Dock = System.Windows.Forms.DockStyle.Fill
        Me.layoutControl.Location = New System.Drawing.Point(0, 0)
        Me.layoutControl.Name = "layoutControl"
        Me.layoutControl.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = New System.Drawing.Rectangle(774, 238, 250, 350)
        Me.layoutControl.Root = Me.LayoutControlGroup1
        Me.layoutControl.Size = New System.Drawing.Size(590, 401)
        Me.layoutControl.TabIndex = 0
        Me.layoutControl.Text = "LayoutControl1"
        '
        'txtVCLAIM_KODE
        '
        Me.txtVCLAIM_KODE.Location = New System.Drawing.Point(117, 38)
        Me.txtVCLAIM_KODE.MenuManager = Me.barManager
        Me.txtVCLAIM_KODE.Name = "txtVCLAIM_KODE"
        Me.txtVCLAIM_KODE.Size = New System.Drawing.Size(369, 20)
        Me.txtVCLAIM_KODE.StyleController = Me.layoutControl
        Me.txtVCLAIM_KODE.TabIndex = 50
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
        Me.barDockControlTop.Size = New System.Drawing.Size(590, 0)
        '
        'barDockControlBottom
        '
        Me.barDockControlBottom.CausesValidation = False
        Me.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.barDockControlBottom.Location = New System.Drawing.Point(0, 401)
        Me.barDockControlBottom.Size = New System.Drawing.Size(590, 22)
        '
        'barDockControlLeft
        '
        Me.barDockControlLeft.CausesValidation = False
        Me.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left
        Me.barDockControlLeft.Location = New System.Drawing.Point(0, 0)
        Me.barDockControlLeft.Size = New System.Drawing.Size(0, 401)
        '
        'barDockControlRight
        '
        Me.barDockControlRight.CausesValidation = False
        Me.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right
        Me.barDockControlRight.Location = New System.Drawing.Point(590, 0)
        Me.barDockControlRight.Size = New System.Drawing.Size(0, 401)
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
        'grdKECAMATAN
        '
        Me.grdKECAMATAN.EnterMoveNextControl = True
        Me.grdKECAMATAN.Location = New System.Drawing.Point(279, 12)
        Me.grdKECAMATAN.MenuManager = Me.barManager
        Me.grdKECAMATAN.Name = "grdKECAMATAN"
        Me.grdKECAMATAN.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.grdKECAMATAN.Properties.NullText = ""
        Me.grdKECAMATAN.Properties.PopupFormMinSize = New System.Drawing.Size(600, 300)
        Me.grdKECAMATAN.Properties.View = Me.GridView9
        Me.grdKECAMATAN.Size = New System.Drawing.Size(170, 20)
        Me.grdKECAMATAN.StyleController = Me.layoutControl
        Me.grdKECAMATAN.TabIndex = 45
        '
        'GridView9
        '
        Me.GridView9.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.GridView9.Name = "GridView9"
        Me.GridView9.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.GridView9.OptionsView.ShowAutoFilterRow = True
        Me.GridView9.OptionsView.ShowGroupPanel = False
        '
        'grdKDKABUPATEN
        '
        Me.grdKDKABUPATEN.EditValue = ""
        Me.grdKDKABUPATEN.EnterMoveNextControl = True
        Me.grdKDKABUPATEN.Location = New System.Drawing.Point(117, 12)
        Me.grdKDKABUPATEN.MenuManager = Me.barManager
        Me.grdKDKABUPATEN.Name = "grdKDKABUPATEN"
        Me.grdKDKABUPATEN.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.grdKDKABUPATEN.Properties.NullText = ""
        Me.grdKDKABUPATEN.Properties.PopupFormMinSize = New System.Drawing.Size(600, 300)
        Me.grdKDKABUPATEN.Properties.View = Me.GridView4
        Me.grdKDKABUPATEN.Size = New System.Drawing.Size(158, 20)
        Me.grdKDKABUPATEN.StyleController = Me.layoutControl
        Me.grdKDKABUPATEN.TabIndex = 24
        '
        'GridView4
        '
        Me.GridView4.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn1, Me.GridColumn7})
        Me.GridView4.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.GridView4.Name = "GridView4"
        Me.GridView4.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.GridView4.OptionsView.ShowAutoFilterRow = True
        Me.GridView4.OptionsView.ShowGroupPanel = False
        '
        'GridColumn1
        '
        Me.GridColumn1.Caption = "Propinsi"
        Me.GridColumn1.FieldName = "PROPINSI"
        Me.GridColumn1.Name = "GridColumn1"
        Me.GridColumn1.Visible = True
        Me.GridColumn1.VisibleIndex = 0
        '
        'GridColumn7
        '
        Me.GridColumn7.Caption = "Kabupaten"
        Me.GridColumn7.FieldName = "MEMO"
        Me.GridColumn7.Name = "GridColumn7"
        Me.GridColumn7.Visible = True
        Me.GridColumn7.VisibleIndex = 1
        '
        'chkISDEFAULT
        '
        Me.chkISDEFAULT.EnterMoveNextControl = True
        Me.chkISDEFAULT.Location = New System.Drawing.Point(490, 35)
        Me.chkISDEFAULT.MenuManager = Me.barManager
        Me.chkISDEFAULT.Name = "chkISDEFAULT"
        Me.chkISDEFAULT.Properties.Caption = "Default?"
        Me.chkISDEFAULT.Size = New System.Drawing.Size(88, 19)
        Me.chkISDEFAULT.StyleController = Me.layoutControl
        Me.chkISDEFAULT.TabIndex = 15
        Me.chkISDEFAULT.TabStop = False
        '
        'chkISACTIVE
        '
        Me.chkISACTIVE.EnterMoveNextControl = True
        Me.chkISACTIVE.Location = New System.Drawing.Point(490, 12)
        Me.chkISACTIVE.MenuManager = Me.barManager
        Me.chkISACTIVE.Name = "chkISACTIVE"
        Me.chkISACTIVE.Properties.Caption = "Active?"
        Me.chkISACTIVE.Properties.NullStyle = DevExpress.XtraEditors.Controls.StyleIndeterminate.Unchecked
        Me.chkISACTIVE.Size = New System.Drawing.Size(88, 19)
        Me.chkISACTIVE.StyleController = Me.layoutControl
        Me.chkISACTIVE.TabIndex = 14
        Me.chkISACTIVE.TabStop = False
        '
        'txtMEMO
        '
        Me.txtMEMO.EditValue = ""
        Me.txtMEMO.Location = New System.Drawing.Point(117, 62)
        Me.txtMEMO.Name = "txtMEMO"
        Me.txtMEMO.Size = New System.Drawing.Size(369, 327)
        Me.txtMEMO.StyleController = Me.layoutControl
        Me.txtMEMO.TabIndex = 9
        '
        'LayoutControlGroup1
        '
        Me.LayoutControlGroup1.CustomizationFormText = "LayoutControlGroup1"
        Me.LayoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.LayoutControlGroup1.GroupBordersVisible = False
        Me.LayoutControlGroup1.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.lMEMO, Me.LayoutControlItem1, Me.LayoutControlItem2, Me.lKABUPATEN, Me.LayoutControlItem3, Me.lVCLAIM_KODE, Me.LayoutControlItem4})
        Me.LayoutControlGroup1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlGroup1.Name = "LayoutControlGroup1"
        Me.LayoutControlGroup1.Size = New System.Drawing.Size(590, 401)
        Me.LayoutControlGroup1.TextVisible = False
        '
        'lMEMO
        '
        Me.lMEMO.AppearanceItemCaption.Options.UseTextOptions = True
        Me.lMEMO.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.lMEMO.AppearanceItemCaption.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Top
        Me.lMEMO.Control = Me.txtMEMO
        Me.lMEMO.CustomizationFormText = "Display Name * :"
        Me.lMEMO.Location = New System.Drawing.Point(0, 50)
        Me.lMEMO.Name = "lMEMO"
        Me.lMEMO.Size = New System.Drawing.Size(478, 331)
        Me.lMEMO.Text = "Description * :"
        Me.lMEMO.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.lMEMO.TextSize = New System.Drawing.Size(100, 20)
        Me.lMEMO.TextToControlDistance = 5
        '
        'LayoutControlItem1
        '
        Me.LayoutControlItem1.Control = Me.chkISACTIVE
        Me.LayoutControlItem1.CustomizationFormText = "LayoutControlItem1"
        Me.LayoutControlItem1.Location = New System.Drawing.Point(478, 0)
        Me.LayoutControlItem1.Name = "LayoutControlItem1"
        Me.LayoutControlItem1.Size = New System.Drawing.Size(92, 23)
        Me.LayoutControlItem1.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem1.TextVisible = False
        '
        'LayoutControlItem2
        '
        Me.LayoutControlItem2.Control = Me.chkISDEFAULT
        Me.LayoutControlItem2.CustomizationFormText = "LayoutControlItem2"
        Me.LayoutControlItem2.Location = New System.Drawing.Point(478, 23)
        Me.LayoutControlItem2.Name = "LayoutControlItem2"
        Me.LayoutControlItem2.Size = New System.Drawing.Size(92, 358)
        Me.LayoutControlItem2.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem2.TextVisible = False
        '
        'lKABUPATEN
        '
        Me.lKABUPATEN.AppearanceItemCaption.Options.UseTextOptions = True
        Me.lKABUPATEN.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.lKABUPATEN.Control = Me.grdKDKABUPATEN
        Me.lKABUPATEN.Location = New System.Drawing.Point(0, 0)
        Me.lKABUPATEN.Name = "lKABUPATEN"
        Me.lKABUPATEN.Size = New System.Drawing.Size(267, 26)
        Me.lKABUPATEN.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.lKABUPATEN.TextSize = New System.Drawing.Size(100, 20)
        Me.lKABUPATEN.TextToControlDistance = 5
        '
        'LayoutControlItem3
        '
        Me.LayoutControlItem3.Control = Me.grdKECAMATAN
        Me.LayoutControlItem3.Location = New System.Drawing.Point(267, 0)
        Me.LayoutControlItem3.Name = "LayoutControlItem3"
        Me.LayoutControlItem3.Size = New System.Drawing.Size(174, 26)
        Me.LayoutControlItem3.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem3.TextVisible = False
        '
        'lVCLAIM_KODE
        '
        Me.lVCLAIM_KODE.AppearanceItemCaption.Options.UseTextOptions = True
        Me.lVCLAIM_KODE.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.lVCLAIM_KODE.Control = Me.txtVCLAIM_KODE
        Me.lVCLAIM_KODE.Location = New System.Drawing.Point(0, 26)
        Me.lVCLAIM_KODE.Name = "lVCLAIM_KODE"
        Me.lVCLAIM_KODE.Size = New System.Drawing.Size(478, 24)
        Me.lVCLAIM_KODE.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.lVCLAIM_KODE.TextSize = New System.Drawing.Size(100, 20)
        Me.lVCLAIM_KODE.TextToControlDistance = 5
        '
        'SimpleButton1
        '
        Me.SimpleButton1.Location = New System.Drawing.Point(453, 12)
        Me.SimpleButton1.Name = "SimpleButton1"
        Me.SimpleButton1.Size = New System.Drawing.Size(33, 22)
        Me.SimpleButton1.StyleController = Me.layoutControl
        Me.SimpleButton1.TabIndex = 51
        Me.SimpleButton1.Text = "+"
        '
        'LayoutControlItem4
        '
        Me.LayoutControlItem4.Control = Me.SimpleButton1
        Me.LayoutControlItem4.Location = New System.Drawing.Point(441, 0)
        Me.LayoutControlItem4.Name = "LayoutControlItem4"
        Me.LayoutControlItem4.Size = New System.Drawing.Size(37, 26)
        Me.LayoutControlItem4.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem4.TextVisible = False
        '
        'frmKecamatan
        '
        Me.Appearance.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(236, Byte), Integer), CType(CType(239, Byte), Integer))
        Me.Appearance.Options.UseBackColor = True
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(590, 423)
        Me.Controls.Add(Me.layoutControl)
        Me.Controls.Add(Me.barDockControlLeft)
        Me.Controls.Add(Me.barDockControlRight)
        Me.Controls.Add(Me.barDockControlBottom)
        Me.Controls.Add(Me.barDockControlTop)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
        Me.KeyPreview = True
        Me.Name = "frmKecamatan"
        Me.ShowIcon = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        CType(Me.layoutControl, System.ComponentModel.ISupportInitialize).EndInit()
        Me.layoutControl.ResumeLayout(False)
        CType(Me.txtVCLAIM_KODE.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.barManager, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.progressBarSave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.progressSave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdKECAMATAN.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView9, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdKDKABUPATEN.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkISDEFAULT.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkISACTIVE.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtMEMO.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lMEMO, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lKABUPATEN, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lVCLAIM_KODE, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem4, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents layoutControl As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents LayoutControlGroup1 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents lMEMO As DevExpress.XtraLayout.LayoutControlItem
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
    Friend WithEvents chkISACTIVE As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents LayoutControlItem1 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents txtMEMO As DevExpress.XtraEditors.MemoEdit
    Friend WithEvents chkISDEFAULT As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents LayoutControlItem2 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents grdKDKABUPATEN As DevExpress.XtraEditors.GridLookUpEdit
    Friend WithEvents GridView4 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridColumn7 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents lKABUPATEN As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents grdKECAMATAN As DevExpress.XtraEditors.GridLookUpEdit
    Friend WithEvents GridView9 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents LayoutControlItem3 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents txtVCLAIM_KODE As DevExpress.XtraEditors.TextEdit
    Friend WithEvents lVCLAIM_KODE As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents GridColumn1 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents SimpleButton1 As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents LayoutControlItem4 As DevExpress.XtraLayout.LayoutControlItem
End Class
