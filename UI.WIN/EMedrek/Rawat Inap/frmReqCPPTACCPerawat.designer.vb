<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmReqCPPTACCPerawat
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
        Me.grdKDUSER = New DevExpress.XtraEditors.GridLookUpEdit()
        Me.grvWAREHOUSE = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn32 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cboSEQ = New DevExpress.XtraEditors.ComboBoxEdit()
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
        Me.txtMEMO = New DevExpress.XtraEditors.MemoEdit()
        Me.LayoutControlGroup1 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.lCODE = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem4 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.lMEMO = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem2 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.TextEdit1 = New DevExpress.XtraEditors.TextEdit()
        Me.LayoutControlItem1 = New DevExpress.XtraLayout.LayoutControlItem()
        CType(Me.layoutControl, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.layoutControl.SuspendLayout()
        CType(Me.grdKDUSER.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grvWAREHOUSE, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cboSEQ.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.barManager, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.progressBarSave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.progressSave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCODE.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtMEMO.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lCODE, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lMEMO, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TextEdit1.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'layoutControl
        '
        Me.layoutControl.Controls.Add(Me.TextEdit1)
        Me.layoutControl.Controls.Add(Me.grdKDUSER)
        Me.layoutControl.Controls.Add(Me.cboSEQ)
        Me.layoutControl.Controls.Add(Me.txtCODE)
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
        'grdKDUSER
        '
        Me.grdKDUSER.EnterMoveNextControl = True
        Me.grdKDUSER.Location = New System.Drawing.Point(117, 84)
        Me.grdKDUSER.Name = "grdKDUSER"
        Me.grdKDUSER.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.grdKDUSER.Properties.NullText = ""
        Me.grdKDUSER.Properties.PopupFormMinSize = New System.Drawing.Size(600, 300)
        Me.grdKDUSER.Properties.View = Me.grvWAREHOUSE
        Me.grdKDUSER.Size = New System.Drawing.Size(461, 20)
        Me.grdKDUSER.StyleController = Me.layoutControl
        Me.grdKDUSER.TabIndex = 62
        '
        'grvWAREHOUSE
        '
        Me.grvWAREHOUSE.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn32})
        Me.grvWAREHOUSE.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.grvWAREHOUSE.Name = "grvWAREHOUSE"
        Me.grvWAREHOUSE.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.grvWAREHOUSE.OptionsView.ShowAutoFilterRow = True
        Me.grvWAREHOUSE.OptionsView.ShowGroupPanel = False
        '
        'GridColumn32
        '
        Me.GridColumn32.Caption = "Tampilan Nama"
        Me.GridColumn32.FieldName = "MEMO"
        Me.GridColumn32.Name = "GridColumn32"
        Me.GridColumn32.Visible = True
        Me.GridColumn32.VisibleIndex = 0
        '
        'cboSEQ
        '
        Me.cboSEQ.EditValue = "HAND OVER"
        Me.cboSEQ.Location = New System.Drawing.Point(117, 60)
        Me.cboSEQ.MenuManager = Me.barManager
        Me.cboSEQ.Name = "cboSEQ"
        Me.cboSEQ.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cboSEQ.Properties.Items.AddRange(New Object() {"HAND OVER", "PELAPORAN NILAI KRITIS", "SBAR / TBAK"})
        Me.cboSEQ.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
        Me.cboSEQ.Size = New System.Drawing.Size(461, 20)
        Me.cboSEQ.StyleController = Me.layoutControl
        Me.cboSEQ.TabIndex = 19
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
        'txtCODE
        '
        Me.txtCODE.Location = New System.Drawing.Point(117, 12)
        Me.txtCODE.MenuManager = Me.barManager
        Me.txtCODE.Name = "txtCODE"
        Me.txtCODE.Size = New System.Drawing.Size(461, 20)
        Me.txtCODE.StyleController = Me.layoutControl
        Me.txtCODE.TabIndex = 17
        '
        'txtMEMO
        '
        Me.txtMEMO.EditValue = ""
        Me.txtMEMO.Location = New System.Drawing.Point(117, 108)
        Me.txtMEMO.Name = "txtMEMO"
        Me.txtMEMO.Size = New System.Drawing.Size(461, 281)
        Me.txtMEMO.StyleController = Me.layoutControl
        Me.txtMEMO.TabIndex = 9
        '
        'LayoutControlGroup1
        '
        Me.LayoutControlGroup1.CustomizationFormText = "LayoutControlGroup1"
        Me.LayoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.LayoutControlGroup1.GroupBordersVisible = False
        Me.LayoutControlGroup1.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.lCODE, Me.LayoutControlItem4, Me.lMEMO, Me.LayoutControlItem2, Me.LayoutControlItem1})
        Me.LayoutControlGroup1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlGroup1.Name = "LayoutControlGroup1"
        Me.LayoutControlGroup1.Size = New System.Drawing.Size(590, 401)
        Me.LayoutControlGroup1.TextVisible = False
        '
        'lCODE
        '
        Me.lCODE.AppearanceItemCaption.Options.UseTextOptions = True
        Me.lCODE.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.lCODE.Control = Me.txtCODE
        Me.lCODE.Location = New System.Drawing.Point(0, 0)
        Me.lCODE.Name = "lCODE"
        Me.lCODE.Size = New System.Drawing.Size(570, 24)
        Me.lCODE.Text = "Kode :"
        Me.lCODE.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.lCODE.TextSize = New System.Drawing.Size(100, 20)
        Me.lCODE.TextToControlDistance = 5
        '
        'LayoutControlItem4
        '
        Me.LayoutControlItem4.AppearanceItemCaption.Options.UseTextOptions = True
        Me.LayoutControlItem4.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.LayoutControlItem4.Control = Me.cboSEQ
        Me.LayoutControlItem4.Location = New System.Drawing.Point(0, 48)
        Me.LayoutControlItem4.Name = "LayoutControlItem4"
        Me.LayoutControlItem4.Size = New System.Drawing.Size(570, 24)
        Me.LayoutControlItem4.Text = "Status :"
        Me.LayoutControlItem4.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.LayoutControlItem4.TextSize = New System.Drawing.Size(100, 20)
        Me.LayoutControlItem4.TextToControlDistance = 5
        '
        'lMEMO
        '
        Me.lMEMO.AppearanceItemCaption.Options.UseTextOptions = True
        Me.lMEMO.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.lMEMO.AppearanceItemCaption.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Top
        Me.lMEMO.Control = Me.txtMEMO
        Me.lMEMO.CustomizationFormText = "Display Name * :"
        Me.lMEMO.Location = New System.Drawing.Point(0, 96)
        Me.lMEMO.Name = "lMEMO"
        Me.lMEMO.Size = New System.Drawing.Size(570, 285)
        Me.lMEMO.Text = "Deskripsi :"
        Me.lMEMO.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.lMEMO.TextSize = New System.Drawing.Size(100, 20)
        Me.lMEMO.TextToControlDistance = 5
        '
        'LayoutControlItem2
        '
        Me.LayoutControlItem2.AppearanceItemCaption.Options.UseTextOptions = True
        Me.LayoutControlItem2.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.LayoutControlItem2.Control = Me.grdKDUSER
        Me.LayoutControlItem2.Location = New System.Drawing.Point(0, 72)
        Me.LayoutControlItem2.Name = "LayoutControlItem2"
        Me.LayoutControlItem2.Size = New System.Drawing.Size(570, 24)
        Me.LayoutControlItem2.Text = "User :"
        Me.LayoutControlItem2.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.LayoutControlItem2.TextSize = New System.Drawing.Size(100, 20)
        Me.LayoutControlItem2.TextToControlDistance = 5
        '
        'TextEdit1
        '
        Me.TextEdit1.Location = New System.Drawing.Point(117, 36)
        Me.TextEdit1.MenuManager = Me.barManager
        Me.TextEdit1.Name = "TextEdit1"
        Me.TextEdit1.Size = New System.Drawing.Size(461, 20)
        Me.TextEdit1.StyleController = Me.layoutControl
        Me.TextEdit1.TabIndex = 18
        '
        'LayoutControlItem1
        '
        Me.LayoutControlItem1.AppearanceItemCaption.Options.UseTextOptions = True
        Me.LayoutControlItem1.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.LayoutControlItem1.Control = Me.TextEdit1
        Me.LayoutControlItem1.Location = New System.Drawing.Point(0, 24)
        Me.LayoutControlItem1.Name = "LayoutControlItem1"
        Me.LayoutControlItem1.Size = New System.Drawing.Size(570, 24)
        Me.LayoutControlItem1.Text = "Kode CPPT :"
        Me.LayoutControlItem1.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.LayoutControlItem1.TextSize = New System.Drawing.Size(100, 20)
        Me.LayoutControlItem1.TextToControlDistance = 5
        '
        'frmReqCPPTACCPerawat
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
        Me.Name = "frmReqCPPTACCPerawat"
        Me.ShowIcon = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        CType(Me.layoutControl, System.ComponentModel.ISupportInitialize).EndInit()
        Me.layoutControl.ResumeLayout(False)
        CType(Me.grdKDUSER.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grvWAREHOUSE, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cboSEQ.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.barManager, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.progressBarSave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.progressSave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCODE.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtMEMO.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lCODE, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lMEMO, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TextEdit1.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).EndInit()
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
    Friend WithEvents lCODE As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents cboSEQ As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents txtMEMO As DevExpress.XtraEditors.MemoEdit
    Friend WithEvents LayoutControlItem4 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents lMEMO As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents grdKDUSER As DevExpress.XtraEditors.GridLookUpEdit
    Friend WithEvents grvWAREHOUSE As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridColumn32 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents LayoutControlItem2 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents TextEdit1 As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LayoutControlItem1 As DevExpress.XtraLayout.LayoutControlItem
End Class
