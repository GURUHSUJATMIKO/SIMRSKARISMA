<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmReportRME
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmReportRME))
        Me.deDATEFrom = New DevExpress.XtraEditors.DateEdit()
        Me.LayoutControl1 = New DevExpress.XtraLayout.LayoutControl()
        Me.txtNamaOrganization = New DevExpress.XtraEditors.TextEdit()
        Me.txtKodeKemenkes = New DevExpress.XtraEditors.TextEdit()
        Me.txtKodeBPJS = New DevExpress.XtraEditors.TextEdit()
        Me.cboTYPE = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.deDATETo = New DevExpress.XtraEditors.DateEdit()
        Me.LayoutControlGroup1 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.lDATEFROM = New DevExpress.XtraLayout.LayoutControlItem()
        Me.lDATETO = New DevExpress.XtraLayout.LayoutControlItem()
        Me.lTYPE = New DevExpress.XtraLayout.LayoutControlItem()
        Me.EmptySpaceItem1 = New DevExpress.XtraLayout.EmptySpaceItem()
        Me.LayoutControlItem1 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem2 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem3 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.GroupControl1 = New DevExpress.XtraEditors.GroupControl()
        Me.grd = New DevExpress.XtraGrid.GridControl()
        Me.ContextMenuStrip1 = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.CekKirimSatuRegisterToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.KirimSemuaDiGridToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.grv = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.panelMenu = New DevExpress.XtraEditors.PanelControl()
        Me.LabelControl1 = New DevExpress.XtraEditors.LabelControl()
        Me.picKirimBPJS = New DevExpress.XtraEditors.PictureEdit()
        Me.LabelControl4 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl3 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl2 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl6 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl7 = New DevExpress.XtraEditors.LabelControl()
        Me.picRefresh = New DevExpress.XtraEditors.PictureEdit()
        Me.picPrint = New DevExpress.XtraEditors.PictureEdit()
        Me.GroupControl2 = New DevExpress.XtraEditors.GroupControl()
        Me.printSystem = New DevExpress.XtraPrinting.PrintingSystem(Me.components)
        Me.printableComponentLink = New DevExpress.XtraPrinting.PrintableComponentLink(Me.components)
        CType(Me.deDATEFrom.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.deDATEFrom.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.LayoutControl1.SuspendLayout()
        CType(Me.txtNamaOrganization.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtKodeKemenkes.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtKodeBPJS.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cboTYPE.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.deDATETo.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.deDATETo.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lDATEFROM, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lDATETO, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lTYPE, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.EmptySpaceItem1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GroupControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl1.SuspendLayout()
        CType(Me.grd, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ContextMenuStrip1.SuspendLayout()
        CType(Me.grv, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.panelMenu, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.panelMenu.SuspendLayout()
        CType(Me.picKirimBPJS.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.picRefresh.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.picPrint.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GroupControl2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl2.SuspendLayout()
        CType(Me.printSystem, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'deDATEFrom
        '
        Me.deDATEFrom.EditValue = Nothing
        Me.deDATEFrom.EnterMoveNextControl = True
        Me.deDATEFrom.Location = New System.Drawing.Point(117, 36)
        Me.deDATEFrom.Name = "deDATEFrom"
        Me.deDATEFrom.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.deDATEFrom.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
        Me.deDATEFrom.Properties.Mask.EditMask = "dd/MM/yyyy"
        Me.deDATEFrom.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.deDATEFrom.Size = New System.Drawing.Size(101, 20)
        Me.deDATEFrom.StyleController = Me.LayoutControl1
        Me.deDATEFrom.TabIndex = 3
        '
        'LayoutControl1
        '
        Me.LayoutControl1.Controls.Add(Me.txtNamaOrganization)
        Me.LayoutControl1.Controls.Add(Me.txtKodeKemenkes)
        Me.LayoutControl1.Controls.Add(Me.txtKodeBPJS)
        Me.LayoutControl1.Controls.Add(Me.cboTYPE)
        Me.LayoutControl1.Controls.Add(Me.deDATEFrom)
        Me.LayoutControl1.Controls.Add(Me.deDATETo)
        Me.LayoutControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LayoutControl1.Location = New System.Drawing.Point(2, 20)
        Me.LayoutControl1.Name = "LayoutControl1"
        Me.LayoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = New System.Drawing.Rectangle(1938, 260, 250, 350)
        Me.LayoutControl1.Root = Me.LayoutControlGroup1
        Me.LayoutControl1.Size = New System.Drawing.Size(640, 96)
        Me.LayoutControl1.TabIndex = 2
        Me.LayoutControl1.Text = "LayoutControl1"
        '
        'txtNamaOrganization
        '
        Me.txtNamaOrganization.Location = New System.Drawing.Point(387, 60)
        Me.txtNamaOrganization.Name = "txtNamaOrganization"
        Me.txtNamaOrganization.Size = New System.Drawing.Size(241, 20)
        Me.txtNamaOrganization.StyleController = Me.LayoutControl1
        Me.txtNamaOrganization.TabIndex = 7
        '
        'txtKodeKemenkes
        '
        Me.txtKodeKemenkes.Location = New System.Drawing.Point(387, 36)
        Me.txtKodeKemenkes.Name = "txtKodeKemenkes"
        Me.txtKodeKemenkes.Size = New System.Drawing.Size(241, 20)
        Me.txtKodeKemenkes.StyleController = Me.LayoutControl1
        Me.txtKodeKemenkes.TabIndex = 6
        '
        'txtKodeBPJS
        '
        Me.txtKodeBPJS.Location = New System.Drawing.Point(387, 12)
        Me.txtKodeBPJS.Name = "txtKodeBPJS"
        Me.txtKodeBPJS.Size = New System.Drawing.Size(241, 20)
        Me.txtKodeBPJS.StyleController = Me.LayoutControl1
        Me.txtKodeBPJS.TabIndex = 5
        '
        'cboTYPE
        '
        Me.cboTYPE.Location = New System.Drawing.Point(117, 12)
        Me.cboTYPE.Name = "cboTYPE"
        Me.cboTYPE.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cboTYPE.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
        Me.cboTYPE.Size = New System.Drawing.Size(101, 20)
        Me.cboTYPE.StyleController = Me.LayoutControl1
        Me.cboTYPE.TabIndex = 0
        '
        'deDATETo
        '
        Me.deDATETo.EditValue = Nothing
        Me.deDATETo.EnterMoveNextControl = True
        Me.deDATETo.Location = New System.Drawing.Point(117, 60)
        Me.deDATETo.Name = "deDATETo"
        Me.deDATETo.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.deDATETo.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
        Me.deDATETo.Properties.Mask.EditMask = "dd/MM/yyyy"
        Me.deDATETo.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.deDATETo.Size = New System.Drawing.Size(101, 20)
        Me.deDATETo.StyleController = Me.LayoutControl1
        Me.deDATETo.TabIndex = 4
        '
        'LayoutControlGroup1
        '
        Me.LayoutControlGroup1.CustomizationFormText = "LayoutControlGroup1"
        Me.LayoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.LayoutControlGroup1.GroupBordersVisible = False
        Me.LayoutControlGroup1.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.lDATEFROM, Me.lDATETO, Me.lTYPE, Me.EmptySpaceItem1, Me.LayoutControlItem1, Me.LayoutControlItem2, Me.LayoutControlItem3})
        Me.LayoutControlGroup1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlGroup1.Name = "Root"
        Me.LayoutControlGroup1.Size = New System.Drawing.Size(640, 96)
        Me.LayoutControlGroup1.TextVisible = False
        '
        'lDATEFROM
        '
        Me.lDATEFROM.AppearanceItemCaption.Options.UseTextOptions = True
        Me.lDATEFROM.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.lDATEFROM.Control = Me.deDATEFrom
        Me.lDATEFROM.CustomizationFormText = "From Date :"
        Me.lDATEFROM.Location = New System.Drawing.Point(0, 24)
        Me.lDATEFROM.Name = "lDATEFROM"
        Me.lDATEFROM.Size = New System.Drawing.Size(210, 24)
        Me.lDATEFROM.Text = "From Date :"
        Me.lDATEFROM.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.lDATEFROM.TextSize = New System.Drawing.Size(100, 20)
        Me.lDATEFROM.TextToControlDistance = 5
        '
        'lDATETO
        '
        Me.lDATETO.AppearanceItemCaption.Options.UseTextOptions = True
        Me.lDATETO.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.lDATETO.Control = Me.deDATETo
        Me.lDATETO.CustomizationFormText = "To Date :"
        Me.lDATETO.Location = New System.Drawing.Point(0, 48)
        Me.lDATETO.Name = "lDATETO"
        Me.lDATETO.Size = New System.Drawing.Size(210, 28)
        Me.lDATETO.Text = "To Date :"
        Me.lDATETO.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.lDATETO.TextSize = New System.Drawing.Size(100, 20)
        Me.lDATETO.TextToControlDistance = 5
        '
        'lTYPE
        '
        Me.lTYPE.AppearanceItemCaption.Options.UseTextOptions = True
        Me.lTYPE.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.lTYPE.Control = Me.cboTYPE
        Me.lTYPE.CustomizationFormText = "Type :"
        Me.lTYPE.Location = New System.Drawing.Point(0, 0)
        Me.lTYPE.Name = "lTYPE"
        Me.lTYPE.Size = New System.Drawing.Size(210, 24)
        Me.lTYPE.Text = "Type :"
        Me.lTYPE.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.lTYPE.TextSize = New System.Drawing.Size(100, 20)
        Me.lTYPE.TextToControlDistance = 5
        '
        'EmptySpaceItem1
        '
        Me.EmptySpaceItem1.AllowHotTrack = False
        Me.EmptySpaceItem1.Location = New System.Drawing.Point(210, 0)
        Me.EmptySpaceItem1.Name = "EmptySpaceItem1"
        Me.EmptySpaceItem1.Size = New System.Drawing.Size(10, 76)
        Me.EmptySpaceItem1.TextSize = New System.Drawing.Size(0, 0)
        '
        'LayoutControlItem1
        '
        Me.LayoutControlItem1.AppearanceItemCaption.Options.UseTextOptions = True
        Me.LayoutControlItem1.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.LayoutControlItem1.Control = Me.txtKodeBPJS
        Me.LayoutControlItem1.Location = New System.Drawing.Point(220, 0)
        Me.LayoutControlItem1.Name = "LayoutControlItem1"
        Me.LayoutControlItem1.Size = New System.Drawing.Size(400, 24)
        Me.LayoutControlItem1.Text = "Kode BPJS :"
        Me.LayoutControlItem1.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.LayoutControlItem1.TextSize = New System.Drawing.Size(150, 20)
        Me.LayoutControlItem1.TextToControlDistance = 5
        '
        'LayoutControlItem2
        '
        Me.LayoutControlItem2.AppearanceItemCaption.Options.UseTextOptions = True
        Me.LayoutControlItem2.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.LayoutControlItem2.Control = Me.txtKodeKemenkes
        Me.LayoutControlItem2.Location = New System.Drawing.Point(220, 24)
        Me.LayoutControlItem2.Name = "LayoutControlItem2"
        Me.LayoutControlItem2.Size = New System.Drawing.Size(400, 24)
        Me.LayoutControlItem2.Text = "Kode Kemenkes :"
        Me.LayoutControlItem2.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.LayoutControlItem2.TextSize = New System.Drawing.Size(150, 20)
        Me.LayoutControlItem2.TextToControlDistance = 5
        '
        'LayoutControlItem3
        '
        Me.LayoutControlItem3.AppearanceItemCaption.Options.UseTextOptions = True
        Me.LayoutControlItem3.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.LayoutControlItem3.Control = Me.txtNamaOrganization
        Me.LayoutControlItem3.Location = New System.Drawing.Point(220, 48)
        Me.LayoutControlItem3.Name = "LayoutControlItem3"
        Me.LayoutControlItem3.Size = New System.Drawing.Size(400, 28)
        Me.LayoutControlItem3.Text = "Nama Organization :"
        Me.LayoutControlItem3.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.LayoutControlItem3.TextSize = New System.Drawing.Size(150, 20)
        Me.LayoutControlItem3.TextToControlDistance = 5
        '
        'GroupControl1
        '
        Me.GroupControl1.Controls.Add(Me.grd)
        Me.GroupControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupControl1.Location = New System.Drawing.Point(0, 208)
        Me.GroupControl1.Name = "GroupControl1"
        Me.GroupControl1.Size = New System.Drawing.Size(644, 364)
        Me.GroupControl1.TabIndex = 4
        Me.GroupControl1.Text = "Preview"
        '
        'grd
        '
        Me.grd.ContextMenuStrip = Me.ContextMenuStrip1
        Me.grd.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grd.EmbeddedNavigator.Buttons.Append.Visible = False
        Me.grd.EmbeddedNavigator.Buttons.CancelEdit.Visible = False
        Me.grd.EmbeddedNavigator.Buttons.Edit.Visible = False
        Me.grd.EmbeddedNavigator.Buttons.EndEdit.Visible = False
        Me.grd.EmbeddedNavigator.Buttons.Remove.Visible = False
        Me.grd.Location = New System.Drawing.Point(2, 20)
        Me.grd.MainView = Me.grv
        Me.grd.Name = "grd"
        Me.grd.Size = New System.Drawing.Size(640, 342)
        Me.grd.TabIndex = 21
        Me.grd.UseEmbeddedNavigator = True
        Me.grd.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.grv})
        '
        'ContextMenuStrip1
        '
        Me.ContextMenuStrip1.ImageScalingSize = New System.Drawing.Size(24, 24)
        Me.ContextMenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.CekKirimSatuRegisterToolStripMenuItem, Me.KirimSemuaDiGridToolStripMenuItem})
        Me.ContextMenuStrip1.Name = "ContextMenuStrip1"
        Me.ContextMenuStrip1.Size = New System.Drawing.Size(369, 48)
        '
        'CekKirimSatuRegisterToolStripMenuItem
        '
        Me.CekKirimSatuRegisterToolStripMenuItem.Name = "CekKirimSatuRegisterToolStripMenuItem"
        Me.CekKirimSatuRegisterToolStripMenuItem.Size = New System.Drawing.Size(368, 22)
        Me.CekKirimSatuRegisterToolStripMenuItem.Text = "Kirim Satu Register Warna Kuning / Update Warna Hijau"
        '
        'KirimSemuaDiGridToolStripMenuItem
        '
        Me.KirimSemuaDiGridToolStripMenuItem.Name = "KirimSemuaDiGridToolStripMenuItem"
        Me.KirimSemuaDiGridToolStripMenuItem.Size = New System.Drawing.Size(368, 22)
        Me.KirimSemuaDiGridToolStripMenuItem.Text = "Kirim Semua Register Warna Kuning"
        '
        'grv
        '
        Me.grv.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.grv.GridControl = Me.grd
        Me.grv.Name = "grv"
        Me.grv.OptionsBehavior.Editable = False
        Me.grv.OptionsBehavior.ReadOnly = True
        Me.grv.OptionsFind.ShowFindButton = False
        Me.grv.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.grv.OptionsView.ShowAutoFilterRow = True
        Me.grv.OptionsView.ShowGroupPanel = False
        '
        'panelMenu
        '
        Me.panelMenu.Appearance.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.panelMenu.Appearance.BackColor2 = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.panelMenu.Appearance.Options.UseBackColor = True
        Me.panelMenu.Controls.Add(Me.LabelControl1)
        Me.panelMenu.Controls.Add(Me.picKirimBPJS)
        Me.panelMenu.Controls.Add(Me.LabelControl4)
        Me.panelMenu.Controls.Add(Me.LabelControl3)
        Me.panelMenu.Controls.Add(Me.LabelControl2)
        Me.panelMenu.Controls.Add(Me.LabelControl6)
        Me.panelMenu.Controls.Add(Me.LabelControl7)
        Me.panelMenu.Controls.Add(Me.picRefresh)
        Me.panelMenu.Controls.Add(Me.picPrint)
        Me.panelMenu.Dock = System.Windows.Forms.DockStyle.Top
        Me.panelMenu.Location = New System.Drawing.Point(0, 0)
        Me.panelMenu.Name = "panelMenu"
        Me.panelMenu.Size = New System.Drawing.Size(644, 90)
        Me.panelMenu.TabIndex = 7
        '
        'LabelControl1
        '
        Me.LabelControl1.Appearance.BackColor = System.Drawing.Color.HotPink
        Me.LabelControl1.Location = New System.Drawing.Point(197, 66)
        Me.LabelControl1.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.LabelControl1.Name = "LabelControl1"
        Me.LabelControl1.Size = New System.Drawing.Size(237, 13)
        Me.LabelControl1.TabIndex = 20
        Me.LabelControl1.Text = "Warna Pink Sudah Kirim Ke BPJS Kesehatan Gagal"
        '
        'picKirimBPJS
        '
        Me.picKirimBPJS.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.picKirimBPJS.EditValue = CType(resources.GetObject("picKirimBPJS.EditValue"), Object)
        Me.picKirimBPJS.Location = New System.Drawing.Point(582, 12)
        Me.picKirimBPJS.Name = "picKirimBPJS"
        Me.picKirimBPJS.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.picKirimBPJS.Properties.Appearance.Options.UseBackColor = True
        Me.picKirimBPJS.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.picKirimBPJS.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Zoom
        Me.picKirimBPJS.Size = New System.Drawing.Size(48, 48)
        Me.picKirimBPJS.TabIndex = 19
        '
        'LabelControl4
        '
        Me.LabelControl4.Appearance.BackColor = System.Drawing.Color.Yellow
        Me.LabelControl4.Location = New System.Drawing.Point(197, 31)
        Me.LabelControl4.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.LabelControl4.Name = "LabelControl4"
        Me.LabelControl4.Size = New System.Drawing.Size(210, 13)
        Me.LabelControl4.TabIndex = 18
        Me.LabelControl4.Text = "Warna Kuning Siap Kirim Ke BPJS Kesehatan"
        '
        'LabelControl3
        '
        Me.LabelControl3.Appearance.BackColor = System.Drawing.Color.Green
        Me.LabelControl3.Location = New System.Drawing.Point(197, 49)
        Me.LabelControl3.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.LabelControl3.Name = "LabelControl3"
        Me.LabelControl3.Size = New System.Drawing.Size(248, 13)
        Me.LabelControl3.TabIndex = 18
        Me.LabelControl3.Text = "Warna Hijau Sudah Kirim Ke BPJS Kesehatan Sukses"
        '
        'LabelControl2
        '
        Me.LabelControl2.Location = New System.Drawing.Point(147, 31)
        Me.LabelControl2.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.LabelControl2.Name = "LabelControl2"
        Me.LabelControl2.Size = New System.Drawing.Size(30, 13)
        Me.LabelControl2.TabIndex = 18
        Me.LabelControl2.Text = "Note :"
        '
        'LabelControl6
        '
        Me.LabelControl6.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.LabelControl6.Location = New System.Drawing.Point(68, 62)
        Me.LabelControl6.Name = "LabelControl6"
        Me.LabelControl6.Size = New System.Drawing.Size(44, 13)
        Me.LabelControl6.TabIndex = 17
        Me.LabelControl6.Text = "&Refresh"
        '
        'LabelControl7
        '
        Me.LabelControl7.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.LabelControl7.Location = New System.Drawing.Point(23, 62)
        Me.LabelControl7.Name = "LabelControl7"
        Me.LabelControl7.Size = New System.Drawing.Size(27, 13)
        Me.LabelControl7.TabIndex = 15
        Me.LabelControl7.Text = "&Print"
        '
        'picRefresh
        '
        Me.picRefresh.EditValue = CType(resources.GetObject("picRefresh.EditValue"), Object)
        Me.picRefresh.Location = New System.Drawing.Point(66, 12)
        Me.picRefresh.Name = "picRefresh"
        Me.picRefresh.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.picRefresh.Properties.Appearance.Options.UseBackColor = True
        Me.picRefresh.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.picRefresh.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Zoom
        Me.picRefresh.Size = New System.Drawing.Size(48, 48)
        Me.picRefresh.TabIndex = 16
        '
        'picPrint
        '
        Me.picPrint.EditValue = CType(resources.GetObject("picPrint.EditValue"), Object)
        Me.picPrint.Location = New System.Drawing.Point(12, 12)
        Me.picPrint.Name = "picPrint"
        Me.picPrint.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.picPrint.Properties.Appearance.Options.UseBackColor = True
        Me.picPrint.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.picPrint.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Zoom
        Me.picPrint.Size = New System.Drawing.Size(48, 48)
        Me.picPrint.TabIndex = 14
        '
        'GroupControl2
        '
        Me.GroupControl2.Controls.Add(Me.LayoutControl1)
        Me.GroupControl2.Dock = System.Windows.Forms.DockStyle.Top
        Me.GroupControl2.Location = New System.Drawing.Point(0, 90)
        Me.GroupControl2.Name = "GroupControl2"
        Me.GroupControl2.Size = New System.Drawing.Size(644, 118)
        Me.GroupControl2.TabIndex = 4
        Me.GroupControl2.Text = "Filter"
        '
        'printSystem
        '
        Me.printSystem.Links.AddRange(New Object() {Me.printableComponentLink})
        '
        'printableComponentLink
        '
        Me.printableComponentLink.Landscape = True
        Me.printableComponentLink.PaperKind = System.Drawing.Printing.PaperKind.Custom
        Me.printableComponentLink.PrintingSystemBase = Me.printSystem
        '
        'frmReportRME
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(644, 572)
        Me.Controls.Add(Me.GroupControl1)
        Me.Controls.Add(Me.GroupControl2)
        Me.Controls.Add(Me.panelMenu)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmReportRME"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        CType(Me.deDATEFrom.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.deDATEFrom.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.LayoutControl1.ResumeLayout(False)
        CType(Me.txtNamaOrganization.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtKodeKemenkes.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtKodeBPJS.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cboTYPE.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.deDATETo.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.deDATETo.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lDATEFROM, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lDATETO, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lTYPE, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.EmptySpaceItem1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GroupControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl1.ResumeLayout(False)
        CType(Me.grd, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ContextMenuStrip1.ResumeLayout(False)
        CType(Me.grv, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.panelMenu, System.ComponentModel.ISupportInitialize).EndInit()
        Me.panelMenu.ResumeLayout(False)
        Me.panelMenu.PerformLayout()
        CType(Me.picKirimBPJS.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.picRefresh.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.picPrint.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GroupControl2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl2.ResumeLayout(False)
        CType(Me.printSystem, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents deDATEFrom As DevExpress.XtraEditors.DateEdit
    Friend WithEvents deDATETo As DevExpress.XtraEditors.DateEdit
    Friend WithEvents GroupControl1 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents panelMenu As DevExpress.XtraEditors.PanelControl
    Friend WithEvents GroupControl2 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents printSystem As DevExpress.XtraPrinting.PrintingSystem
    Friend WithEvents printableComponentLink As DevExpress.XtraPrinting.PrintableComponentLink
    Friend WithEvents LabelControl6 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl7 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents picRefresh As DevExpress.XtraEditors.PictureEdit
    Friend WithEvents picPrint As DevExpress.XtraEditors.PictureEdit
    Friend WithEvents LayoutControl1 As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents LayoutControlGroup1 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents lDATEFROM As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents lDATETO As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents cboTYPE As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents lTYPE As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents grd As DevExpress.XtraGrid.GridControl
    Friend WithEvents grv As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents LabelControl4 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl3 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl2 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents ContextMenuStrip1 As ContextMenuStrip
    Friend WithEvents CekKirimSatuRegisterToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents KirimSemuaDiGridToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents txtNamaOrganization As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txtKodeKemenkes As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txtKodeBPJS As DevExpress.XtraEditors.TextEdit
    Friend WithEvents EmptySpaceItem1 As DevExpress.XtraLayout.EmptySpaceItem
    Friend WithEvents LayoutControlItem1 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem2 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem3 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents picKirimBPJS As DevExpress.XtraEditors.PictureEdit
    Friend WithEvents LabelControl1 As DevExpress.XtraEditors.LabelControl
End Class
