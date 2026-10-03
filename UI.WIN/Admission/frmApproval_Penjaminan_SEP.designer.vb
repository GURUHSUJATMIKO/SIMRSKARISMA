<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmApproval_Penjaminan_SEP
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
        Me.cboJENISPENGAJUAN = New DevExpress.XtraEditors.ComboBoxEdit()
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
        Me.rbJENISRAWAT = New DevExpress.XtraEditors.RadioGroup()
        Me.txtCODE = New DevExpress.XtraEditors.TextEdit()
        Me.rbCATEGORY = New DevExpress.XtraEditors.RadioGroup()
        Me.grdKDCUSTOMER = New DevExpress.XtraEditors.GridLookUpEdit()
        Me.grvKDCUSTOMER = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn12 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn1 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn2 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cboCARI = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.txtCARI = New DevExpress.XtraEditors.TextEdit()
        Me.deDATE = New DevExpress.XtraEditors.DateEdit()
        Me.txtDESCRIPTION = New DevExpress.XtraEditors.MemoEdit()
        Me.LayoutControlGroup1 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.lDESCRIPTION = New DevExpress.XtraLayout.LayoutControlItem()
        Me.lKDCUSTOMER = New DevExpress.XtraLayout.LayoutControlItem()
        Me.lDATE = New DevExpress.XtraLayout.LayoutControlItem()
        Me.lKDAPPROVAL = New DevExpress.XtraLayout.LayoutControlItem()
        Me.lJENISRAWAT = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem4 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.lCATEGORY = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem3 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.lJENISPENGAJUAN = New DevExpress.XtraLayout.LayoutControlItem()
        CType(Me.layoutControl, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.layoutControl.SuspendLayout()
        CType(Me.cboJENISPENGAJUAN.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.barManager, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.progressBarSave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.progressSave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rbJENISRAWAT.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCODE.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rbCATEGORY.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdKDCUSTOMER.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grvKDCUSTOMER, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cboCARI.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCARI.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.deDATE.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.deDATE.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDESCRIPTION.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lDESCRIPTION, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lKDCUSTOMER, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lDATE, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lKDAPPROVAL, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lJENISRAWAT, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lCATEGORY, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lJENISPENGAJUAN, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'layoutControl
        '
        Me.layoutControl.Controls.Add(Me.cboJENISPENGAJUAN)
        Me.layoutControl.Controls.Add(Me.rbJENISRAWAT)
        Me.layoutControl.Controls.Add(Me.txtCODE)
        Me.layoutControl.Controls.Add(Me.rbCATEGORY)
        Me.layoutControl.Controls.Add(Me.grdKDCUSTOMER)
        Me.layoutControl.Controls.Add(Me.cboCARI)
        Me.layoutControl.Controls.Add(Me.txtCARI)
        Me.layoutControl.Controls.Add(Me.deDATE)
        Me.layoutControl.Controls.Add(Me.txtDESCRIPTION)
        Me.layoutControl.Dock = System.Windows.Forms.DockStyle.Fill
        Me.layoutControl.Location = New System.Drawing.Point(0, 0)
        Me.layoutControl.Name = "layoutControl"
        Me.layoutControl.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = New System.Drawing.Rectangle(774, 238, 250, 350)
        Me.layoutControl.Root = Me.LayoutControlGroup1
        Me.layoutControl.Size = New System.Drawing.Size(537, 304)
        Me.layoutControl.TabIndex = 0
        Me.layoutControl.Text = "LayoutControl1"
        '
        'cboJENISPENGAJUAN
        '
        Me.cboJENISPENGAJUAN.EditValue = "pengajuan backdate"
        Me.cboJENISPENGAJUAN.Location = New System.Drawing.Point(137, 166)
        Me.cboJENISPENGAJUAN.MenuManager = Me.barManager
        Me.cboJENISPENGAJUAN.Name = "cboJENISPENGAJUAN"
        Me.cboJENISPENGAJUAN.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cboJENISPENGAJUAN.Properties.Items.AddRange(New Object() {"pengajuan backdate", "pengajuan finger print"})
        Me.cboJENISPENGAJUAN.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
        Me.cboJENISPENGAJUAN.Size = New System.Drawing.Size(388, 20)
        Me.cboJENISPENGAJUAN.StyleController = Me.layoutControl
        Me.cboJENISPENGAJUAN.TabIndex = 47
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
        Me.barDockControlTop.Size = New System.Drawing.Size(537, 0)
        '
        'barDockControlBottom
        '
        Me.barDockControlBottom.CausesValidation = False
        Me.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.barDockControlBottom.Location = New System.Drawing.Point(0, 304)
        Me.barDockControlBottom.Size = New System.Drawing.Size(537, 22)
        '
        'barDockControlLeft
        '
        Me.barDockControlLeft.CausesValidation = False
        Me.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left
        Me.barDockControlLeft.Location = New System.Drawing.Point(0, 0)
        Me.barDockControlLeft.Size = New System.Drawing.Size(0, 304)
        '
        'barDockControlRight
        '
        Me.barDockControlRight.CausesValidation = False
        Me.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right
        Me.barDockControlRight.Location = New System.Drawing.Point(537, 0)
        Me.barDockControlRight.Size = New System.Drawing.Size(0, 304)
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
        'rbJENISRAWAT
        '
        Me.rbJENISRAWAT.Location = New System.Drawing.Point(137, 65)
        Me.rbJENISRAWAT.MenuManager = Me.barManager
        Me.rbJENISRAWAT.Name = "rbJENISRAWAT"
        Me.rbJENISRAWAT.Properties.Items.AddRange(New DevExpress.XtraEditors.Controls.RadioGroupItem() {New DevExpress.XtraEditors.Controls.RadioGroupItem(Nothing, "Rawat Jalan"), New DevExpress.XtraEditors.Controls.RadioGroupItem(Nothing, "Rawat Inap")})
        Me.rbJENISRAWAT.Size = New System.Drawing.Size(388, 25)
        Me.rbJENISRAWAT.StyleController = Me.layoutControl
        Me.rbJENISRAWAT.TabIndex = 28
        '
        'txtCODE
        '
        Me.txtCODE.Location = New System.Drawing.Point(137, 12)
        Me.txtCODE.MenuManager = Me.barManager
        Me.txtCODE.Name = "txtCODE"
        Me.txtCODE.Properties.ReadOnly = True
        Me.txtCODE.Size = New System.Drawing.Size(388, 20)
        Me.txtCODE.StyleController = Me.layoutControl
        Me.txtCODE.TabIndex = 47
        '
        'rbCATEGORY
        '
        Me.rbCATEGORY.Location = New System.Drawing.Point(137, 36)
        Me.rbCATEGORY.MenuManager = Me.barManager
        Me.rbCATEGORY.Name = "rbCATEGORY"
        Me.rbCATEGORY.Properties.Items.AddRange(New DevExpress.XtraEditors.Controls.RadioGroupItem() {New DevExpress.XtraEditors.Controls.RadioGroupItem(Nothing, "Pengajuan"), New DevExpress.XtraEditors.Controls.RadioGroupItem(Nothing, "Approval Penjaminan SEP")})
        Me.rbCATEGORY.Size = New System.Drawing.Size(388, 25)
        Me.rbCATEGORY.StyleController = Me.layoutControl
        Me.rbCATEGORY.TabIndex = 27
        '
        'grdKDCUSTOMER
        '
        Me.grdKDCUSTOMER.EnterMoveNextControl = True
        Me.grdKDCUSTOMER.Location = New System.Drawing.Point(137, 118)
        Me.grdKDCUSTOMER.MenuManager = Me.barManager
        Me.grdKDCUSTOMER.Name = "grdKDCUSTOMER"
        Me.grdKDCUSTOMER.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.grdKDCUSTOMER.Properties.NullText = ""
        Me.grdKDCUSTOMER.Properties.PopupFormMinSize = New System.Drawing.Size(600, 300)
        Me.grdKDCUSTOMER.Properties.View = Me.grvKDCUSTOMER
        Me.grdKDCUSTOMER.Size = New System.Drawing.Size(388, 20)
        Me.grdKDCUSTOMER.StyleController = Me.layoutControl
        Me.grdKDCUSTOMER.TabIndex = 40
        '
        'grvKDCUSTOMER
        '
        Me.grvKDCUSTOMER.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn12, Me.GridColumn1, Me.GridColumn2})
        Me.grvKDCUSTOMER.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.grvKDCUSTOMER.Name = "grvKDCUSTOMER"
        Me.grvKDCUSTOMER.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.grvKDCUSTOMER.OptionsView.ShowAutoFilterRow = True
        Me.grvKDCUSTOMER.OptionsView.ShowGroupPanel = False
        '
        'GridColumn12
        '
        Me.GridColumn12.Caption = "Pasien"
        Me.GridColumn12.FieldName = "NAME_DISPLAY"
        Me.GridColumn12.Name = "GridColumn12"
        Me.GridColumn12.Visible = True
        Me.GridColumn12.VisibleIndex = 0
        '
        'GridColumn1
        '
        Me.GridColumn1.Caption = "Rekam Medis"
        Me.GridColumn1.FieldName = "KDCUSTOMER"
        Me.GridColumn1.Name = "GridColumn1"
        Me.GridColumn1.Visible = True
        Me.GridColumn1.VisibleIndex = 1
        '
        'GridColumn2
        '
        Me.GridColumn2.Caption = "No BPJS"
        Me.GridColumn2.FieldName = "KARTUBPJS"
        Me.GridColumn2.Name = "GridColumn2"
        Me.GridColumn2.Visible = True
        Me.GridColumn2.VisibleIndex = 2
        '
        'cboCARI
        '
        Me.cboCARI.EditValue = "REKAM MEDIS"
        Me.cboCARI.Location = New System.Drawing.Point(137, 94)
        Me.cboCARI.MenuManager = Me.barManager
        Me.cboCARI.Name = "cboCARI"
        Me.cboCARI.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cboCARI.Properties.Items.AddRange(New Object() {"REKAM MEDIS", "NAMA PASIEN", "NO KARTU BPJS"})
        Me.cboCARI.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
        Me.cboCARI.Size = New System.Drawing.Size(129, 20)
        Me.cboCARI.StyleController = Me.layoutControl
        Me.cboCARI.TabIndex = 46
        '
        'txtCARI
        '
        Me.txtCARI.Location = New System.Drawing.Point(270, 94)
        Me.txtCARI.MenuManager = Me.barManager
        Me.txtCARI.Name = "txtCARI"
        Me.txtCARI.Size = New System.Drawing.Size(255, 20)
        Me.txtCARI.StyleController = Me.layoutControl
        Me.txtCARI.TabIndex = 45
        '
        'deDATE
        '
        Me.deDATE.EditValue = Nothing
        Me.deDATE.EnterMoveNextControl = True
        Me.deDATE.Location = New System.Drawing.Point(137, 142)
        Me.deDATE.MenuManager = Me.barManager
        Me.deDATE.Name = "deDATE"
        Me.deDATE.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.deDATE.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
        Me.deDATE.Properties.Mask.EditMask = "dd/MM/yyyy"
        Me.deDATE.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.deDATE.Size = New System.Drawing.Size(388, 20)
        Me.deDATE.StyleController = Me.layoutControl
        Me.deDATE.TabIndex = 22
        '
        'txtDESCRIPTION
        '
        Me.txtDESCRIPTION.EditValue = ""
        Me.txtDESCRIPTION.Location = New System.Drawing.Point(137, 190)
        Me.txtDESCRIPTION.Name = "txtDESCRIPTION"
        Me.txtDESCRIPTION.Size = New System.Drawing.Size(388, 102)
        Me.txtDESCRIPTION.StyleController = Me.layoutControl
        Me.txtDESCRIPTION.TabIndex = 9
        '
        'LayoutControlGroup1
        '
        Me.LayoutControlGroup1.CustomizationFormText = "LayoutControlGroup1"
        Me.LayoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.LayoutControlGroup1.GroupBordersVisible = False
        Me.LayoutControlGroup1.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.lDESCRIPTION, Me.lKDCUSTOMER, Me.lDATE, Me.lKDAPPROVAL, Me.lJENISRAWAT, Me.LayoutControlItem4, Me.lCATEGORY, Me.LayoutControlItem3, Me.lJENISPENGAJUAN})
        Me.LayoutControlGroup1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlGroup1.Name = "LayoutControlGroup1"
        Me.LayoutControlGroup1.Size = New System.Drawing.Size(537, 304)
        Me.LayoutControlGroup1.TextVisible = False
        '
        'lDESCRIPTION
        '
        Me.lDESCRIPTION.AppearanceItemCaption.Options.UseTextOptions = True
        Me.lDESCRIPTION.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.lDESCRIPTION.AppearanceItemCaption.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Top
        Me.lDESCRIPTION.Control = Me.txtDESCRIPTION
        Me.lDESCRIPTION.CustomizationFormText = "Display Name * :"
        Me.lDESCRIPTION.Location = New System.Drawing.Point(0, 178)
        Me.lDESCRIPTION.Name = "lDESCRIPTION"
        Me.lDESCRIPTION.Size = New System.Drawing.Size(517, 106)
        Me.lDESCRIPTION.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.lDESCRIPTION.TextSize = New System.Drawing.Size(120, 20)
        Me.lDESCRIPTION.TextToControlDistance = 5
        '
        'lKDCUSTOMER
        '
        Me.lKDCUSTOMER.AppearanceItemCaption.Options.UseTextOptions = True
        Me.lKDCUSTOMER.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.lKDCUSTOMER.Control = Me.grdKDCUSTOMER
        Me.lKDCUSTOMER.Location = New System.Drawing.Point(0, 106)
        Me.lKDCUSTOMER.Name = "lKDCUSTOMER"
        Me.lKDCUSTOMER.Size = New System.Drawing.Size(517, 24)
        Me.lKDCUSTOMER.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.lKDCUSTOMER.TextSize = New System.Drawing.Size(120, 20)
        Me.lKDCUSTOMER.TextToControlDistance = 5
        '
        'lDATE
        '
        Me.lDATE.AppearanceItemCaption.Options.UseTextOptions = True
        Me.lDATE.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.lDATE.Control = Me.deDATE
        Me.lDATE.Location = New System.Drawing.Point(0, 130)
        Me.lDATE.Name = "lDATE"
        Me.lDATE.Size = New System.Drawing.Size(517, 24)
        Me.lDATE.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.lDATE.TextSize = New System.Drawing.Size(120, 20)
        Me.lDATE.TextToControlDistance = 5
        '
        'lKDAPPROVAL
        '
        Me.lKDAPPROVAL.AppearanceItemCaption.Options.UseTextOptions = True
        Me.lKDAPPROVAL.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.lKDAPPROVAL.Control = Me.txtCODE
        Me.lKDAPPROVAL.Location = New System.Drawing.Point(0, 0)
        Me.lKDAPPROVAL.Name = "lKDAPPROVAL"
        Me.lKDAPPROVAL.Size = New System.Drawing.Size(517, 24)
        Me.lKDAPPROVAL.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.lKDAPPROVAL.TextSize = New System.Drawing.Size(120, 20)
        Me.lKDAPPROVAL.TextToControlDistance = 5
        '
        'lJENISRAWAT
        '
        Me.lJENISRAWAT.AppearanceItemCaption.Options.UseTextOptions = True
        Me.lJENISRAWAT.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.lJENISRAWAT.Control = Me.rbJENISRAWAT
        Me.lJENISRAWAT.Location = New System.Drawing.Point(0, 53)
        Me.lJENISRAWAT.Name = "lJENISRAWAT"
        Me.lJENISRAWAT.Size = New System.Drawing.Size(517, 29)
        Me.lJENISRAWAT.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.lJENISRAWAT.TextSize = New System.Drawing.Size(120, 20)
        Me.lJENISRAWAT.TextToControlDistance = 5
        '
        'LayoutControlItem4
        '
        Me.LayoutControlItem4.AppearanceItemCaption.Options.UseTextOptions = True
        Me.LayoutControlItem4.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.LayoutControlItem4.Control = Me.cboCARI
        Me.LayoutControlItem4.Location = New System.Drawing.Point(0, 82)
        Me.LayoutControlItem4.Name = "LayoutControlItem4"
        Me.LayoutControlItem4.Size = New System.Drawing.Size(258, 24)
        Me.LayoutControlItem4.Text = "Cari :"
        Me.LayoutControlItem4.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.LayoutControlItem4.TextSize = New System.Drawing.Size(120, 20)
        Me.LayoutControlItem4.TextToControlDistance = 5
        '
        'lCATEGORY
        '
        Me.lCATEGORY.AppearanceItemCaption.Options.UseTextOptions = True
        Me.lCATEGORY.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.lCATEGORY.Control = Me.rbCATEGORY
        Me.lCATEGORY.Location = New System.Drawing.Point(0, 24)
        Me.lCATEGORY.Name = "lCATEGORY"
        Me.lCATEGORY.Size = New System.Drawing.Size(517, 29)
        Me.lCATEGORY.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.lCATEGORY.TextSize = New System.Drawing.Size(120, 20)
        Me.lCATEGORY.TextToControlDistance = 5
        '
        'LayoutControlItem3
        '
        Me.LayoutControlItem3.Control = Me.txtCARI
        Me.LayoutControlItem3.Location = New System.Drawing.Point(258, 82)
        Me.LayoutControlItem3.Name = "LayoutControlItem3"
        Me.LayoutControlItem3.Size = New System.Drawing.Size(259, 24)
        Me.LayoutControlItem3.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem3.TextVisible = False
        '
        'lJENISPENGAJUAN
        '
        Me.lJENISPENGAJUAN.AppearanceItemCaption.Options.UseTextOptions = True
        Me.lJENISPENGAJUAN.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.lJENISPENGAJUAN.Control = Me.cboJENISPENGAJUAN
        Me.lJENISPENGAJUAN.Location = New System.Drawing.Point(0, 154)
        Me.lJENISPENGAJUAN.Name = "lJENISPENGAJUAN"
        Me.lJENISPENGAJUAN.Size = New System.Drawing.Size(517, 24)
        Me.lJENISPENGAJUAN.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.lJENISPENGAJUAN.TextSize = New System.Drawing.Size(120, 20)
        Me.lJENISPENGAJUAN.TextToControlDistance = 5
        '
        'frmApproval_Penjaminan_SEP
        '
        Me.Appearance.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(236, Byte), Integer), CType(CType(239, Byte), Integer))
        Me.Appearance.Options.UseBackColor = True
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(537, 326)
        Me.Controls.Add(Me.layoutControl)
        Me.Controls.Add(Me.barDockControlLeft)
        Me.Controls.Add(Me.barDockControlRight)
        Me.Controls.Add(Me.barDockControlBottom)
        Me.Controls.Add(Me.barDockControlTop)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
        Me.KeyPreview = True
        Me.Name = "frmApproval_Penjaminan_SEP"
        Me.ShowIcon = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        CType(Me.layoutControl, System.ComponentModel.ISupportInitialize).EndInit()
        Me.layoutControl.ResumeLayout(False)
        CType(Me.cboJENISPENGAJUAN.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.barManager, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.progressBarSave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.progressSave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rbJENISRAWAT.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCODE.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rbCATEGORY.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdKDCUSTOMER.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grvKDCUSTOMER, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cboCARI.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCARI.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.deDATE.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.deDATE.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDESCRIPTION.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lDESCRIPTION, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lKDCUSTOMER, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lDATE, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lKDAPPROVAL, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lJENISRAWAT, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lCATEGORY, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lJENISPENGAJUAN, System.ComponentModel.ISupportInitialize).EndInit()
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
    Friend WithEvents txtDESCRIPTION As DevExpress.XtraEditors.MemoEdit
    Friend WithEvents deDATE As DevExpress.XtraEditors.DateEdit
    Friend WithEvents lDATE As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents cboCARI As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents txtCARI As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LayoutControlItem3 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem4 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents grdKDCUSTOMER As DevExpress.XtraEditors.GridLookUpEdit
    Friend WithEvents grvKDCUSTOMER As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridColumn12 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents lKDCUSTOMER As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents rbCATEGORY As DevExpress.XtraEditors.RadioGroup
    Friend WithEvents lCATEGORY As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents txtCODE As DevExpress.XtraEditors.TextEdit
    Friend WithEvents lKDAPPROVAL As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents rbJENISRAWAT As DevExpress.XtraEditors.RadioGroup
    Friend WithEvents lJENISRAWAT As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents GridColumn1 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn2 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cboJENISPENGAJUAN As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents lJENISPENGAJUAN As DevExpress.XtraLayout.LayoutControlItem
End Class
