<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmTracking
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
        Me.txtCODE = New DevExpress.XtraEditors.TextEdit()
        Me.grdCARIREKAMMEDIS = New DevExpress.XtraEditors.GridLookUpEdit()
        Me.GridView1 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn3 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn4 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cboCARI = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.txtCARI = New DevExpress.XtraEditors.TextEdit()
        Me.deDATE_KIRIM = New DevExpress.XtraEditors.DateEdit()
        Me.txtMEMO = New DevExpress.XtraEditors.MemoEdit()
        Me.LayoutControlGroup1 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.lDESCRIPTION = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem3 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem4 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.lKDPENDAFATRAN = New DevExpress.XtraLayout.LayoutControlItem()
        Me.lDATE = New DevExpress.XtraLayout.LayoutControlItem()
        Me.lKDSKD = New DevExpress.XtraLayout.LayoutControlItem()
        Me.txtKDCUSTOMER = New DevExpress.XtraEditors.TextEdit()
        Me.LayoutControlItem1 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.txtTUJUAN_SEKARANG = New DevExpress.XtraEditors.TextEdit()
        Me.LayoutControlItem2 = New DevExpress.XtraLayout.LayoutControlItem()
        CType(Me.layoutControl, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.layoutControl.SuspendLayout()
        CType(Me.barManager, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.progressBarSave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.progressSave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCODE.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdCARIREKAMMEDIS.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cboCARI.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCARI.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.deDATE_KIRIM.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.deDATE_KIRIM.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtMEMO.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lDESCRIPTION, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lKDPENDAFATRAN, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lDATE, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lKDSKD, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtKDCUSTOMER.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtTUJUAN_SEKARANG.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'layoutControl
        '
        Me.layoutControl.Controls.Add(Me.txtTUJUAN_SEKARANG)
        Me.layoutControl.Controls.Add(Me.txtKDCUSTOMER)
        Me.layoutControl.Controls.Add(Me.txtCODE)
        Me.layoutControl.Controls.Add(Me.grdCARIREKAMMEDIS)
        Me.layoutControl.Controls.Add(Me.cboCARI)
        Me.layoutControl.Controls.Add(Me.txtCARI)
        Me.layoutControl.Controls.Add(Me.deDATE_KIRIM)
        Me.layoutControl.Controls.Add(Me.txtMEMO)
        Me.layoutControl.Dock = System.Windows.Forms.DockStyle.Fill
        Me.layoutControl.Location = New System.Drawing.Point(0, 0)
        Me.layoutControl.Name = "layoutControl"
        Me.layoutControl.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = New System.Drawing.Rectangle(774, 238, 250, 350)
        Me.layoutControl.Root = Me.LayoutControlGroup1
        Me.layoutControl.Size = New System.Drawing.Size(656, 313)
        Me.layoutControl.TabIndex = 0
        Me.layoutControl.Text = "LayoutControl1"
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
        Me.barDockControlTop.Size = New System.Drawing.Size(656, 0)
        '
        'barDockControlBottom
        '
        Me.barDockControlBottom.CausesValidation = False
        Me.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.barDockControlBottom.Location = New System.Drawing.Point(0, 313)
        Me.barDockControlBottom.Size = New System.Drawing.Size(656, 22)
        '
        'barDockControlLeft
        '
        Me.barDockControlLeft.CausesValidation = False
        Me.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left
        Me.barDockControlLeft.Location = New System.Drawing.Point(0, 0)
        Me.barDockControlLeft.Size = New System.Drawing.Size(0, 313)
        '
        'barDockControlRight
        '
        Me.barDockControlRight.CausesValidation = False
        Me.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right
        Me.barDockControlRight.Location = New System.Drawing.Point(656, 0)
        Me.barDockControlRight.Size = New System.Drawing.Size(0, 313)
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
        'txtCODE
        '
        Me.txtCODE.Location = New System.Drawing.Point(167, 12)
        Me.txtCODE.MenuManager = Me.barManager
        Me.txtCODE.Name = "txtCODE"
        Me.txtCODE.Properties.ReadOnly = True
        Me.txtCODE.Size = New System.Drawing.Size(477, 20)
        Me.txtCODE.StyleController = Me.layoutControl
        Me.txtCODE.TabIndex = 47
        '
        'grdCARIREKAMMEDIS
        '
        Me.grdCARIREKAMMEDIS.EnterMoveNextControl = True
        Me.grdCARIREKAMMEDIS.Location = New System.Drawing.Point(167, 60)
        Me.grdCARIREKAMMEDIS.MenuManager = Me.barManager
        Me.grdCARIREKAMMEDIS.Name = "grdCARIREKAMMEDIS"
        Me.grdCARIREKAMMEDIS.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.grdCARIREKAMMEDIS.Properties.NullText = ""
        Me.grdCARIREKAMMEDIS.Properties.PopupFormMinSize = New System.Drawing.Size(600, 300)
        Me.grdCARIREKAMMEDIS.Properties.View = Me.GridView1
        Me.grdCARIREKAMMEDIS.Size = New System.Drawing.Size(477, 20)
        Me.grdCARIREKAMMEDIS.StyleController = Me.layoutControl
        Me.grdCARIREKAMMEDIS.TabIndex = 40
        '
        'GridView1
        '
        Me.GridView1.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn3, Me.GridColumn4})
        Me.GridView1.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.GridView1.Name = "GridView1"
        Me.GridView1.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.GridView1.OptionsView.ShowAutoFilterRow = True
        Me.GridView1.OptionsView.ShowGroupPanel = False
        '
        'GridColumn3
        '
        Me.GridColumn3.Caption = "No RM"
        Me.GridColumn3.FieldName = "KDCUSTOMER"
        Me.GridColumn3.Name = "GridColumn3"
        Me.GridColumn3.Visible = True
        Me.GridColumn3.VisibleIndex = 2
        '
        'GridColumn4
        '
        Me.GridColumn4.Caption = "Pasien"
        Me.GridColumn4.FieldName = "NAME_DISPLAY"
        Me.GridColumn4.Name = "GridColumn4"
        Me.GridColumn4.Visible = True
        Me.GridColumn4.VisibleIndex = 3
        '
        'cboCARI
        '
        Me.cboCARI.EditValue = "REKAM MEDIS"
        Me.cboCARI.Location = New System.Drawing.Point(167, 36)
        Me.cboCARI.MenuManager = Me.barManager
        Me.cboCARI.Name = "cboCARI"
        Me.cboCARI.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cboCARI.Properties.Items.AddRange(New Object() {"REKAM MEDIS", "NAMA PASIEN"})
        Me.cboCARI.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
        Me.cboCARI.Size = New System.Drawing.Size(158, 20)
        Me.cboCARI.StyleController = Me.layoutControl
        Me.cboCARI.TabIndex = 46
        '
        'txtCARI
        '
        Me.txtCARI.Location = New System.Drawing.Point(329, 36)
        Me.txtCARI.MenuManager = Me.barManager
        Me.txtCARI.Name = "txtCARI"
        Me.txtCARI.Size = New System.Drawing.Size(315, 20)
        Me.txtCARI.StyleController = Me.layoutControl
        Me.txtCARI.TabIndex = 45
        '
        'deDATE_KIRIM
        '
        Me.deDATE_KIRIM.EditValue = Nothing
        Me.deDATE_KIRIM.EnterMoveNextControl = True
        Me.deDATE_KIRIM.Location = New System.Drawing.Point(167, 132)
        Me.deDATE_KIRIM.MenuManager = Me.barManager
        Me.deDATE_KIRIM.Name = "deDATE_KIRIM"
        Me.deDATE_KIRIM.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.deDATE_KIRIM.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
        Me.deDATE_KIRIM.Properties.Mask.EditMask = "dd/MM/yyyy"
        Me.deDATE_KIRIM.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.deDATE_KIRIM.Size = New System.Drawing.Size(477, 20)
        Me.deDATE_KIRIM.StyleController = Me.layoutControl
        Me.deDATE_KIRIM.TabIndex = 22
        '
        'txtMEMO
        '
        Me.txtMEMO.EditValue = ""
        Me.txtMEMO.Location = New System.Drawing.Point(167, 156)
        Me.txtMEMO.Name = "txtMEMO"
        Me.txtMEMO.Size = New System.Drawing.Size(477, 145)
        Me.txtMEMO.StyleController = Me.layoutControl
        Me.txtMEMO.TabIndex = 9
        '
        'LayoutControlGroup1
        '
        Me.LayoutControlGroup1.CustomizationFormText = "LayoutControlGroup1"
        Me.LayoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.LayoutControlGroup1.GroupBordersVisible = False
        Me.LayoutControlGroup1.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.lDESCRIPTION, Me.LayoutControlItem3, Me.LayoutControlItem4, Me.lKDPENDAFATRAN, Me.lKDSKD, Me.LayoutControlItem1, Me.LayoutControlItem2, Me.lDATE})
        Me.LayoutControlGroup1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlGroup1.Name = "LayoutControlGroup1"
        Me.LayoutControlGroup1.Size = New System.Drawing.Size(656, 313)
        Me.LayoutControlGroup1.TextVisible = False
        '
        'lDESCRIPTION
        '
        Me.lDESCRIPTION.AppearanceItemCaption.Options.UseTextOptions = True
        Me.lDESCRIPTION.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.lDESCRIPTION.AppearanceItemCaption.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Top
        Me.lDESCRIPTION.Control = Me.txtMEMO
        Me.lDESCRIPTION.CustomizationFormText = "Display Name * :"
        Me.lDESCRIPTION.Location = New System.Drawing.Point(0, 144)
        Me.lDESCRIPTION.Name = "lDESCRIPTION"
        Me.lDESCRIPTION.Size = New System.Drawing.Size(636, 149)
        Me.lDESCRIPTION.Text = "Description * :"
        Me.lDESCRIPTION.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.lDESCRIPTION.TextSize = New System.Drawing.Size(150, 20)
        Me.lDESCRIPTION.TextToControlDistance = 5
        '
        'LayoutControlItem3
        '
        Me.LayoutControlItem3.Control = Me.txtCARI
        Me.LayoutControlItem3.Location = New System.Drawing.Point(317, 24)
        Me.LayoutControlItem3.Name = "LayoutControlItem3"
        Me.LayoutControlItem3.Size = New System.Drawing.Size(319, 24)
        Me.LayoutControlItem3.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem3.TextVisible = False
        '
        'LayoutControlItem4
        '
        Me.LayoutControlItem4.AppearanceItemCaption.Options.UseTextOptions = True
        Me.LayoutControlItem4.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.LayoutControlItem4.Control = Me.cboCARI
        Me.LayoutControlItem4.Location = New System.Drawing.Point(0, 24)
        Me.LayoutControlItem4.Name = "LayoutControlItem4"
        Me.LayoutControlItem4.Size = New System.Drawing.Size(317, 24)
        Me.LayoutControlItem4.Text = "Cari"
        Me.LayoutControlItem4.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.LayoutControlItem4.TextSize = New System.Drawing.Size(150, 20)
        Me.LayoutControlItem4.TextToControlDistance = 5
        '
        'lKDPENDAFATRAN
        '
        Me.lKDPENDAFATRAN.AppearanceItemCaption.Options.UseTextOptions = True
        Me.lKDPENDAFATRAN.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.lKDPENDAFATRAN.Control = Me.grdCARIREKAMMEDIS
        Me.lKDPENDAFATRAN.Location = New System.Drawing.Point(0, 48)
        Me.lKDPENDAFATRAN.Name = "lKDPENDAFATRAN"
        Me.lKDPENDAFATRAN.Size = New System.Drawing.Size(636, 24)
        Me.lKDPENDAFATRAN.Text = "Pasien"
        Me.lKDPENDAFATRAN.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.lKDPENDAFATRAN.TextSize = New System.Drawing.Size(150, 20)
        Me.lKDPENDAFATRAN.TextToControlDistance = 5
        '
        'lDATE
        '
        Me.lDATE.AppearanceItemCaption.Options.UseTextOptions = True
        Me.lDATE.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.lDATE.Control = Me.deDATE_KIRIM
        Me.lDATE.Location = New System.Drawing.Point(0, 120)
        Me.lDATE.Name = "lDATE"
        Me.lDATE.Size = New System.Drawing.Size(636, 24)
        Me.lDATE.Text = "Tanggal Kirim"
        Me.lDATE.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.lDATE.TextSize = New System.Drawing.Size(150, 20)
        Me.lDATE.TextToControlDistance = 5
        '
        'lKDSKD
        '
        Me.lKDSKD.AppearanceItemCaption.Options.UseTextOptions = True
        Me.lKDSKD.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.lKDSKD.Control = Me.txtCODE
        Me.lKDSKD.Location = New System.Drawing.Point(0, 0)
        Me.lKDSKD.Name = "lKDSKD"
        Me.lKDSKD.Size = New System.Drawing.Size(636, 24)
        Me.lKDSKD.Text = "Kode"
        Me.lKDSKD.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.lKDSKD.TextSize = New System.Drawing.Size(150, 20)
        Me.lKDSKD.TextToControlDistance = 5
        '
        'txtKDCUSTOMER
        '
        Me.txtKDCUSTOMER.Location = New System.Drawing.Point(167, 84)
        Me.txtKDCUSTOMER.MenuManager = Me.barManager
        Me.txtKDCUSTOMER.Name = "txtKDCUSTOMER"
        Me.txtKDCUSTOMER.Size = New System.Drawing.Size(477, 20)
        Me.txtKDCUSTOMER.StyleController = Me.layoutControl
        Me.txtKDCUSTOMER.TabIndex = 46
        '
        'LayoutControlItem1
        '
        Me.LayoutControlItem1.AppearanceItemCaption.Options.UseTextOptions = True
        Me.LayoutControlItem1.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.LayoutControlItem1.Control = Me.txtKDCUSTOMER
        Me.LayoutControlItem1.Location = New System.Drawing.Point(0, 72)
        Me.LayoutControlItem1.Name = "LayoutControlItem1"
        Me.LayoutControlItem1.Size = New System.Drawing.Size(636, 24)
        Me.LayoutControlItem1.Text = "No Rekam Medis"
        Me.LayoutControlItem1.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.LayoutControlItem1.TextSize = New System.Drawing.Size(150, 20)
        Me.LayoutControlItem1.TextToControlDistance = 5
        '
        'txtTUJUAN_SEKARANG
        '
        Me.txtTUJUAN_SEKARANG.Location = New System.Drawing.Point(167, 108)
        Me.txtTUJUAN_SEKARANG.MenuManager = Me.barManager
        Me.txtTUJUAN_SEKARANG.Name = "txtTUJUAN_SEKARANG"
        Me.txtTUJUAN_SEKARANG.Size = New System.Drawing.Size(477, 20)
        Me.txtTUJUAN_SEKARANG.StyleController = Me.layoutControl
        Me.txtTUJUAN_SEKARANG.TabIndex = 46
        '
        'LayoutControlItem2
        '
        Me.LayoutControlItem2.AppearanceItemCaption.Options.UseTextOptions = True
        Me.LayoutControlItem2.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.LayoutControlItem2.Control = Me.txtTUJUAN_SEKARANG
        Me.LayoutControlItem2.Location = New System.Drawing.Point(0, 96)
        Me.LayoutControlItem2.Name = "LayoutControlItem2"
        Me.LayoutControlItem2.Size = New System.Drawing.Size(636, 24)
        Me.LayoutControlItem2.Text = "Tujuan"
        Me.LayoutControlItem2.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.LayoutControlItem2.TextSize = New System.Drawing.Size(150, 20)
        Me.LayoutControlItem2.TextToControlDistance = 5
        '
        'frmTracking
        '
        Me.Appearance.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(236, Byte), Integer), CType(CType(239, Byte), Integer))
        Me.Appearance.Options.UseBackColor = True
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(656, 335)
        Me.Controls.Add(Me.layoutControl)
        Me.Controls.Add(Me.barDockControlLeft)
        Me.Controls.Add(Me.barDockControlRight)
        Me.Controls.Add(Me.barDockControlBottom)
        Me.Controls.Add(Me.barDockControlTop)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
        Me.KeyPreview = True
        Me.Name = "frmTracking"
        Me.ShowIcon = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        CType(Me.layoutControl, System.ComponentModel.ISupportInitialize).EndInit()
        Me.layoutControl.ResumeLayout(False)
        CType(Me.barManager, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.progressBarSave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.progressSave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCODE.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdCARIREKAMMEDIS.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cboCARI.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCARI.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.deDATE_KIRIM.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.deDATE_KIRIM.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtMEMO.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lDESCRIPTION, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lKDPENDAFATRAN, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lDATE, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lKDSKD, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtKDCUSTOMER.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtTUJUAN_SEKARANG.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents layoutControl As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents LayoutControlGroup1 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents lDESCRIPTION As DevExpress.XtraLayout.LayoutControlItem
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
    Friend WithEvents txtMEMO As DevExpress.XtraEditors.MemoEdit
    Friend WithEvents deDATE_KIRIM As DevExpress.XtraEditors.DateEdit
    Friend WithEvents lDATE As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents cboCARI As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents txtCARI As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LayoutControlItem3 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem4 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents grdCARIREKAMMEDIS As DevExpress.XtraEditors.GridLookUpEdit
    Friend WithEvents GridView1 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents lKDPENDAFATRAN As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents txtCODE As DevExpress.XtraEditors.TextEdit
    Friend WithEvents lKDSKD As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents GridColumn3 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn4 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents txtTUJUAN_SEKARANG As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txtKDCUSTOMER As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LayoutControlItem1 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem2 As DevExpress.XtraLayout.LayoutControlItem
End Class
