<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmPPK
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
        Me.txtCARI = New DevExpress.XtraEditors.TextEdit()
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
        Me.grdCARI = New DevExpress.XtraEditors.GridLookUpEdit()
        Me.grvCARI = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.cboASALRUJUKAN = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.txtKDFASKES = New DevExpress.XtraEditors.TextEdit()
        Me.chkISDEFAULT = New DevExpress.XtraEditors.CheckEdit()
        Me.chkISACTIVE = New DevExpress.XtraEditors.CheckEdit()
        Me.txtMEMO = New DevExpress.XtraEditors.MemoEdit()
        Me.LayoutControlGroup1 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.lMEMO = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem1 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem2 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.lKDFASKES = New DevExpress.XtraLayout.LayoutControlItem()
        Me.lJENISFASKES = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem4 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem3 = New DevExpress.XtraLayout.LayoutControlItem()
        CType(Me.layoutControl, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.layoutControl.SuspendLayout()
        CType(Me.txtCARI.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.barManager, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.progressBarSave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.progressSave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdCARI.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grvCARI, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cboASALRUJUKAN.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtKDFASKES.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkISDEFAULT.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkISACTIVE.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtMEMO.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lMEMO, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lKDFASKES, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lJENISFASKES, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'layoutControl
        '
        Me.layoutControl.Controls.Add(Me.txtCARI)
        Me.layoutControl.Controls.Add(Me.grdCARI)
        Me.layoutControl.Controls.Add(Me.cboASALRUJUKAN)
        Me.layoutControl.Controls.Add(Me.txtKDFASKES)
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
        'txtCARI
        '
        Me.txtCARI.Location = New System.Drawing.Point(117, 12)
        Me.txtCARI.MenuManager = Me.barManager
        Me.txtCARI.Name = "txtCARI"
        Me.txtCARI.Size = New System.Drawing.Size(133, 20)
        Me.txtCARI.StyleController = Me.layoutControl
        Me.txtCARI.TabIndex = 17
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
        'grdCARI
        '
        Me.grdCARI.EditValue = ""
        Me.grdCARI.EnterMoveNextControl = True
        Me.grdCARI.Location = New System.Drawing.Point(254, 12)
        Me.grdCARI.MenuManager = Me.barManager
        Me.grdCARI.Name = "grdCARI"
        Me.grdCARI.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.grdCARI.Properties.NullText = ""
        Me.grdCARI.Properties.PopupFormMinSize = New System.Drawing.Size(600, 300)
        Me.grdCARI.Properties.View = Me.grvCARI
        Me.grdCARI.Size = New System.Drawing.Size(222, 20)
        Me.grdCARI.StyleController = Me.layoutControl
        Me.grdCARI.TabIndex = 26
        '
        'grvCARI
        '
        Me.grvCARI.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.grvCARI.Name = "grvCARI"
        Me.grvCARI.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.grvCARI.OptionsView.ShowAutoFilterRow = True
        Me.grvCARI.OptionsView.ShowGroupPanel = False
        '
        'cboASALRUJUKAN
        '
        Me.cboASALRUJUKAN.EditValue = "Faskes 1"
        Me.cboASALRUJUKAN.Location = New System.Drawing.Point(117, 36)
        Me.cboASALRUJUKAN.MenuManager = Me.barManager
        Me.cboASALRUJUKAN.Name = "cboASALRUJUKAN"
        Me.cboASALRUJUKAN.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cboASALRUJUKAN.Properties.Items.AddRange(New Object() {"Faskes 1", "Faskes 2 (RS)"})
        Me.cboASALRUJUKAN.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
        Me.cboASALRUJUKAN.Size = New System.Drawing.Size(359, 20)
        Me.cboASALRUJUKAN.StyleController = Me.layoutControl
        Me.cboASALRUJUKAN.TabIndex = 46
        '
        'txtKDFASKES
        '
        Me.txtKDFASKES.Location = New System.Drawing.Point(117, 60)
        Me.txtKDFASKES.MenuManager = Me.barManager
        Me.txtKDFASKES.Name = "txtKDFASKES"
        Me.txtKDFASKES.Size = New System.Drawing.Size(359, 20)
        Me.txtKDFASKES.StyleController = Me.layoutControl
        Me.txtKDFASKES.TabIndex = 16
        '
        'chkISDEFAULT
        '
        Me.chkISDEFAULT.EnterMoveNextControl = True
        Me.chkISDEFAULT.Location = New System.Drawing.Point(480, 35)
        Me.chkISDEFAULT.MenuManager = Me.barManager
        Me.chkISDEFAULT.Name = "chkISDEFAULT"
        Me.chkISDEFAULT.Properties.Caption = "Default?"
        Me.chkISDEFAULT.Size = New System.Drawing.Size(98, 19)
        Me.chkISDEFAULT.StyleController = Me.layoutControl
        Me.chkISDEFAULT.TabIndex = 15
        Me.chkISDEFAULT.TabStop = False
        '
        'chkISACTIVE
        '
        Me.chkISACTIVE.EnterMoveNextControl = True
        Me.chkISACTIVE.Location = New System.Drawing.Point(480, 12)
        Me.chkISACTIVE.MenuManager = Me.barManager
        Me.chkISACTIVE.Name = "chkISACTIVE"
        Me.chkISACTIVE.Properties.Caption = "Active?"
        Me.chkISACTIVE.Properties.NullStyle = DevExpress.XtraEditors.Controls.StyleIndeterminate.Unchecked
        Me.chkISACTIVE.Size = New System.Drawing.Size(98, 19)
        Me.chkISACTIVE.StyleController = Me.layoutControl
        Me.chkISACTIVE.TabIndex = 14
        Me.chkISACTIVE.TabStop = False
        '
        'txtMEMO
        '
        Me.txtMEMO.EditValue = ""
        Me.txtMEMO.Location = New System.Drawing.Point(117, 84)
        Me.txtMEMO.Name = "txtMEMO"
        Me.txtMEMO.Size = New System.Drawing.Size(359, 305)
        Me.txtMEMO.StyleController = Me.layoutControl
        Me.txtMEMO.TabIndex = 9
        '
        'LayoutControlGroup1
        '
        Me.LayoutControlGroup1.CustomizationFormText = "LayoutControlGroup1"
        Me.LayoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.LayoutControlGroup1.GroupBordersVisible = False
        Me.LayoutControlGroup1.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.lMEMO, Me.LayoutControlItem1, Me.LayoutControlItem2, Me.lKDFASKES, Me.lJENISFASKES, Me.LayoutControlItem4, Me.LayoutControlItem3})
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
        Me.lMEMO.Location = New System.Drawing.Point(0, 72)
        Me.lMEMO.Name = "lMEMO"
        Me.lMEMO.Size = New System.Drawing.Size(468, 309)
        Me.lMEMO.Text = "Description * :"
        Me.lMEMO.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.lMEMO.TextSize = New System.Drawing.Size(100, 20)
        Me.lMEMO.TextToControlDistance = 5
        '
        'LayoutControlItem1
        '
        Me.LayoutControlItem1.Control = Me.chkISACTIVE
        Me.LayoutControlItem1.CustomizationFormText = "LayoutControlItem1"
        Me.LayoutControlItem1.Location = New System.Drawing.Point(468, 0)
        Me.LayoutControlItem1.Name = "LayoutControlItem1"
        Me.LayoutControlItem1.Size = New System.Drawing.Size(102, 23)
        Me.LayoutControlItem1.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem1.TextVisible = False
        '
        'LayoutControlItem2
        '
        Me.LayoutControlItem2.Control = Me.chkISDEFAULT
        Me.LayoutControlItem2.CustomizationFormText = "LayoutControlItem2"
        Me.LayoutControlItem2.Location = New System.Drawing.Point(468, 23)
        Me.LayoutControlItem2.Name = "LayoutControlItem2"
        Me.LayoutControlItem2.Size = New System.Drawing.Size(102, 358)
        Me.LayoutControlItem2.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem2.TextVisible = False
        '
        'lKDFASKES
        '
        Me.lKDFASKES.AppearanceItemCaption.Options.UseTextOptions = True
        Me.lKDFASKES.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.lKDFASKES.Control = Me.txtKDFASKES
        Me.lKDFASKES.Location = New System.Drawing.Point(0, 48)
        Me.lKDFASKES.Name = "lKDFASKES"
        Me.lKDFASKES.Size = New System.Drawing.Size(468, 24)
        Me.lKDFASKES.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.lKDFASKES.TextSize = New System.Drawing.Size(100, 20)
        Me.lKDFASKES.TextToControlDistance = 5
        '
        'lJENISFASKES
        '
        Me.lJENISFASKES.AppearanceItemCaption.Options.UseTextOptions = True
        Me.lJENISFASKES.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.lJENISFASKES.Control = Me.cboASALRUJUKAN
        Me.lJENISFASKES.Location = New System.Drawing.Point(0, 24)
        Me.lJENISFASKES.Name = "lJENISFASKES"
        Me.lJENISFASKES.Size = New System.Drawing.Size(468, 24)
        Me.lJENISFASKES.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.lJENISFASKES.TextSize = New System.Drawing.Size(100, 20)
        Me.lJENISFASKES.TextToControlDistance = 5
        '
        'LayoutControlItem4
        '
        Me.LayoutControlItem4.AppearanceItemCaption.Options.UseTextOptions = True
        Me.LayoutControlItem4.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.LayoutControlItem4.Control = Me.txtCARI
        Me.LayoutControlItem4.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlItem4.Name = "LayoutControlItem4"
        Me.LayoutControlItem4.Size = New System.Drawing.Size(242, 24)
        Me.LayoutControlItem4.Text = "Cari"
        Me.LayoutControlItem4.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.LayoutControlItem4.TextSize = New System.Drawing.Size(100, 20)
        Me.LayoutControlItem4.TextToControlDistance = 5
        '
        'LayoutControlItem3
        '
        Me.LayoutControlItem3.Control = Me.grdCARI
        Me.LayoutControlItem3.Location = New System.Drawing.Point(242, 0)
        Me.LayoutControlItem3.Name = "LayoutControlItem3"
        Me.LayoutControlItem3.Size = New System.Drawing.Size(226, 24)
        Me.LayoutControlItem3.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem3.TextVisible = False
        '
        'frmPPK
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
        Me.Name = "frmPPK"
        Me.ShowIcon = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        CType(Me.layoutControl, System.ComponentModel.ISupportInitialize).EndInit()
        Me.layoutControl.ResumeLayout(False)
        CType(Me.txtCARI.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.barManager, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.progressBarSave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.progressSave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdCARI.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grvCARI, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cboASALRUJUKAN.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtKDFASKES.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkISDEFAULT.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkISACTIVE.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtMEMO.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lMEMO, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lKDFASKES, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lJENISFASKES, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem3, System.ComponentModel.ISupportInitialize).EndInit()
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
    Friend WithEvents txtKDFASKES As DevExpress.XtraEditors.TextEdit
    Friend WithEvents lKDFASKES As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents cboASALRUJUKAN As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents lJENISFASKES As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents txtCARI As DevExpress.XtraEditors.TextEdit
    Friend WithEvents grdCARI As DevExpress.XtraEditors.GridLookUpEdit
    Friend WithEvents grvCARI As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents LayoutControlItem4 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem3 As DevExpress.XtraLayout.LayoutControlItem
End Class
