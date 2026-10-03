<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmReportMedicalRoom
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
        Dim GridLevelNode1 As DevExpress.XtraGrid.GridLevelNode = New DevExpress.XtraGrid.GridLevelNode()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmReportMedicalRoom))
        Me.grv1 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.grd = New DevExpress.XtraGrid.GridControl()
        Me.ContextMenuStrip1 = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.UpdateListToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.AlihDPJPToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.grv = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GroupControl1 = New DevExpress.XtraEditors.GroupControl()
        Me.panelMenu = New DevExpress.XtraEditors.PanelControl()
        Me.deDATEFROM = New DevExpress.XtraEditors.DateEdit()
        Me.deDATETO = New DevExpress.XtraEditors.DateEdit()
        Me.chkAll = New DevExpress.XtraEditors.CheckEdit()
        Me.btnCari = New DevExpress.XtraEditors.SimpleButton()
        Me.txtPARAMETER = New DevExpress.XtraEditors.TextEdit()
        Me.lblHiden1 = New DevExpress.XtraEditors.LabelControl()
        Me.cboFILTER = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.lblPERAWAT = New DevExpress.XtraEditors.LabelControl()
        Me.grdUSERHADNOVER = New DevExpress.XtraEditors.GridLookUpEdit()
        Me.GridView27 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn44 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.LabelControl1 = New DevExpress.XtraEditors.LabelControl()
        Me.grdDPJPUtama = New DevExpress.XtraEditors.GridLookUpEdit()
        Me.GridView1 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn2 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn3 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.LabelControl6 = New DevExpress.XtraEditors.LabelControl()
        Me.picRefresh = New DevExpress.XtraEditors.PictureEdit()
        Me.printSystem = New DevExpress.XtraPrinting.PrintingSystem(Me.components)
        Me.printableComponentLink = New DevExpress.XtraPrinting.PrintableComponentLink(Me.components)
        Me.BindingSource = New System.Windows.Forms.BindingSource(Me.components)
        CType(Me.grv1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grd, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ContextMenuStrip1.SuspendLayout()
        CType(Me.grv, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GroupControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl1.SuspendLayout()
        CType(Me.panelMenu, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.panelMenu.SuspendLayout()
        CType(Me.deDATEFROM.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.deDATEFROM.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.deDATETO.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.deDATETO.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkAll.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtPARAMETER.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cboFILTER.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdUSERHADNOVER.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView27, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdDPJPUtama.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.picRefresh.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.printSystem, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.BindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'grv1
        '
        Me.grv1.GridControl = Me.grd
        Me.grv1.Name = "grv1"
        Me.grv1.OptionsBehavior.Editable = False
        Me.grv1.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.grv1.OptionsView.ShowAutoFilterRow = True
        '
        'grd
        '
        Me.grd.ContextMenuStrip = Me.ContextMenuStrip1
        Me.grd.Dock = System.Windows.Forms.DockStyle.Fill
        GridLevelNode1.LevelTemplate = Me.grv1
        GridLevelNode1.RelationName = "Level1"
        Me.grd.LevelTree.Nodes.AddRange(New DevExpress.XtraGrid.GridLevelNode() {GridLevelNode1})
        Me.grd.Location = New System.Drawing.Point(2, 20)
        Me.grd.MainView = Me.grv
        Me.grd.Name = "grd"
        Me.grd.ShowOnlyPredefinedDetails = True
        Me.grd.Size = New System.Drawing.Size(1014, 468)
        Me.grd.TabIndex = 0
        Me.grd.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.grv, Me.grv1})
        '
        'ContextMenuStrip1
        '
        Me.ContextMenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.UpdateListToolStripMenuItem, Me.AlihDPJPToolStripMenuItem})
        Me.ContextMenuStrip1.Name = "ContextMenuStrip1"
        Me.ContextMenuStrip1.Size = New System.Drawing.Size(124, 48)
        '
        'UpdateListToolStripMenuItem
        '
        Me.UpdateListToolStripMenuItem.Name = "UpdateListToolStripMenuItem"
        Me.UpdateListToolStripMenuItem.Size = New System.Drawing.Size(123, 22)
        Me.UpdateListToolStripMenuItem.Text = "Edit"
        '
        'AlihDPJPToolStripMenuItem
        '
        Me.AlihDPJPToolStripMenuItem.Name = "AlihDPJPToolStripMenuItem"
        Me.AlihDPJPToolStripMenuItem.Size = New System.Drawing.Size(123, 22)
        Me.AlihDPJPToolStripMenuItem.Text = "Alih DPJP"
        '
        'grv
        '
        Me.grv.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.grv.GridControl = Me.grd
        Me.grv.Name = "grv"
        Me.grv.OptionsBehavior.ReadOnly = True
        Me.grv.OptionsDetail.SmartDetailHeight = True
        Me.grv.OptionsPrint.EnableAppearanceEvenRow = True
        Me.grv.OptionsPrint.EnableAppearanceOddRow = True
        Me.grv.OptionsPrint.ExpandAllDetails = True
        Me.grv.OptionsPrint.PrintDetails = True
        Me.grv.OptionsPrint.PrintFilterInfo = True
        Me.grv.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.grv.OptionsView.ShowAutoFilterRow = True
        Me.grv.OptionsView.ShowFooter = True
        '
        'GroupControl1
        '
        Me.GroupControl1.Controls.Add(Me.grd)
        Me.GroupControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupControl1.Location = New System.Drawing.Point(0, 82)
        Me.GroupControl1.Name = "GroupControl1"
        Me.GroupControl1.Size = New System.Drawing.Size(1018, 490)
        Me.GroupControl1.TabIndex = 4
        Me.GroupControl1.Text = "Preview"
        '
        'panelMenu
        '
        Me.panelMenu.Appearance.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.panelMenu.Appearance.BackColor2 = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.panelMenu.Appearance.Options.UseBackColor = True
        Me.panelMenu.Controls.Add(Me.deDATEFROM)
        Me.panelMenu.Controls.Add(Me.deDATETO)
        Me.panelMenu.Controls.Add(Me.chkAll)
        Me.panelMenu.Controls.Add(Me.btnCari)
        Me.panelMenu.Controls.Add(Me.txtPARAMETER)
        Me.panelMenu.Controls.Add(Me.lblHiden1)
        Me.panelMenu.Controls.Add(Me.cboFILTER)
        Me.panelMenu.Controls.Add(Me.lblPERAWAT)
        Me.panelMenu.Controls.Add(Me.grdUSERHADNOVER)
        Me.panelMenu.Controls.Add(Me.LabelControl1)
        Me.panelMenu.Controls.Add(Me.grdDPJPUtama)
        Me.panelMenu.Controls.Add(Me.LabelControl6)
        Me.panelMenu.Controls.Add(Me.picRefresh)
        Me.panelMenu.Dock = System.Windows.Forms.DockStyle.Top
        Me.panelMenu.Location = New System.Drawing.Point(0, 0)
        Me.panelMenu.Name = "panelMenu"
        Me.panelMenu.Size = New System.Drawing.Size(1018, 82)
        Me.panelMenu.TabIndex = 7
        '
        'deDATEFROM
        '
        Me.deDATEFROM.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.deDATEFROM.EditValue = Nothing
        Me.deDATEFROM.EnterMoveNextControl = True
        Me.deDATEFROM.Location = New System.Drawing.Point(785, 31)
        Me.deDATEFROM.Name = "deDATEFROM"
        Me.deDATEFROM.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.deDATEFROM.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
        Me.deDATEFROM.Properties.Mask.EditMask = "dd/MM/yyyy HH:mm"
        Me.deDATEFROM.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.deDATEFROM.Size = New System.Drawing.Size(150, 20)
        Me.deDATEFROM.TabIndex = 58
        Me.deDATEFROM.Visible = False
        '
        'deDATETO
        '
        Me.deDATETO.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.deDATETO.EditValue = Nothing
        Me.deDATETO.EnterMoveNextControl = True
        Me.deDATETO.Location = New System.Drawing.Point(785, 54)
        Me.deDATETO.Name = "deDATETO"
        Me.deDATETO.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.deDATETO.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
        Me.deDATETO.Properties.Mask.EditMask = "dd/MM/yyyy HH:mm"
        Me.deDATETO.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.deDATETO.Size = New System.Drawing.Size(150, 20)
        Me.deDATETO.TabIndex = 57
        Me.deDATETO.Visible = False
        '
        'chkAll
        '
        Me.chkAll.Location = New System.Drawing.Point(78, 12)
        Me.chkAll.Name = "chkAll"
        Me.chkAll.Properties.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.chkAll.Properties.Appearance.Options.UseFont = True
        Me.chkAll.Properties.Caption = "Semua Pasien ?"
        Me.chkAll.Size = New System.Drawing.Size(130, 19)
        Me.chkAll.TabIndex = 70
        '
        'btnCari
        '
        Me.btnCari.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnCari.Location = New System.Drawing.Point(938, 25)
        Me.btnCari.Name = "btnCari"
        Me.btnCari.Size = New System.Drawing.Size(75, 48)
        Me.btnCari.TabIndex = 69
        Me.btnCari.Text = "Cari"
        '
        'txtPARAMETER
        '
        Me.txtPARAMETER.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtPARAMETER.Location = New System.Drawing.Point(785, 54)
        Me.txtPARAMETER.Name = "txtPARAMETER"
        Me.txtPARAMETER.Size = New System.Drawing.Size(150, 20)
        Me.txtPARAMETER.TabIndex = 68
        '
        'lblHiden1
        '
        Me.lblHiden1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblHiden1.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.lblHiden1.Location = New System.Drawing.Point(619, 37)
        Me.lblHiden1.Name = "lblHiden1"
        Me.lblHiden1.Size = New System.Drawing.Size(35, 13)
        Me.lblHiden1.TabIndex = 66
        Me.lblHiden1.Text = "Filter :"
        '
        'cboFILTER
        '
        Me.cboFILTER.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cboFILTER.EditValue = "Rekam Medis"
        Me.cboFILTER.Location = New System.Drawing.Point(618, 54)
        Me.cboFILTER.Name = "cboFILTER"
        Me.cboFILTER.Properties.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.cboFILTER.Properties.Appearance.Options.UseFont = True
        Me.cboFILTER.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cboFILTER.Properties.Items.AddRange(New Object() {"Rekam Medis", "Nama Pasien", "Periode"})
        Me.cboFILTER.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
        Me.cboFILTER.Size = New System.Drawing.Size(161, 20)
        Me.cboFILTER.TabIndex = 65
        '
        'lblPERAWAT
        '
        Me.lblPERAWAT.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.lblPERAWAT.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.lblPERAWAT.Location = New System.Drawing.Point(332, 33)
        Me.lblPERAWAT.Name = "lblPERAWAT"
        Me.lblPERAWAT.Size = New System.Drawing.Size(90, 13)
        Me.lblPERAWAT.TabIndex = 64
        Me.lblPERAWAT.Text = "Perawat/Bidan :"
        '
        'grdUSERHADNOVER
        '
        Me.grdUSERHADNOVER.EnterMoveNextControl = True
        Me.grdUSERHADNOVER.Location = New System.Drawing.Point(332, 55)
        Me.grdUSERHADNOVER.Name = "grdUSERHADNOVER"
        Me.grdUSERHADNOVER.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.grdUSERHADNOVER.Properties.NullText = ""
        Me.grdUSERHADNOVER.Properties.PopupFormMinSize = New System.Drawing.Size(600, 300)
        Me.grdUSERHADNOVER.Properties.View = Me.GridView27
        Me.grdUSERHADNOVER.Size = New System.Drawing.Size(204, 20)
        Me.grdUSERHADNOVER.TabIndex = 63
        '
        'GridView27
        '
        Me.GridView27.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn44})
        Me.GridView27.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.GridView27.Name = "GridView27"
        Me.GridView27.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.GridView27.OptionsView.ShowAutoFilterRow = True
        Me.GridView27.OptionsView.ShowGroupPanel = False
        '
        'GridColumn44
        '
        Me.GridColumn44.Caption = "Tampilan Nama"
        Me.GridColumn44.FieldName = "MEMO"
        Me.GridColumn44.Name = "GridColumn44"
        Me.GridColumn44.Visible = True
        Me.GridColumn44.VisibleIndex = 0
        '
        'LabelControl1
        '
        Me.LabelControl1.Appearance.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelControl1.Location = New System.Drawing.Point(78, 33)
        Me.LabelControl1.Name = "LabelControl1"
        Me.LabelControl1.Size = New System.Drawing.Size(53, 16)
        Me.LabelControl1.TabIndex = 59
        Me.LabelControl1.Text = "Dokter :"
        '
        'grdDPJPUtama
        '
        Me.grdDPJPUtama.Location = New System.Drawing.Point(78, 55)
        Me.grdDPJPUtama.Name = "grdDPJPUtama"
        Me.grdDPJPUtama.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.grdDPJPUtama.Properties.NullText = ""
        Me.grdDPJPUtama.Properties.View = Me.GridView1
        Me.grdDPJPUtama.Size = New System.Drawing.Size(248, 20)
        Me.grdDPJPUtama.TabIndex = 58
        '
        'GridView1
        '
        Me.GridView1.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn2, Me.GridColumn3})
        Me.GridView1.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.GridView1.Name = "GridView1"
        Me.GridView1.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.GridView1.OptionsView.ShowAutoFilterRow = True
        Me.GridView1.OptionsView.ShowGroupPanel = False
        '
        'GridColumn2
        '
        Me.GridColumn2.Caption = "Keterangan"
        Me.GridColumn2.FieldName = "NAME_DISPLAY"
        Me.GridColumn2.Name = "GridColumn2"
        Me.GridColumn2.Visible = True
        Me.GridColumn2.VisibleIndex = 0
        '
        'GridColumn3
        '
        Me.GridColumn3.Caption = "Kode"
        Me.GridColumn3.FieldName = "KODEANTRIAN"
        Me.GridColumn3.Name = "GridColumn3"
        '
        'LabelControl6
        '
        Me.LabelControl6.Appearance.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelControl6.Location = New System.Drawing.Point(12, 58)
        Me.LabelControl6.Name = "LabelControl6"
        Me.LabelControl6.Size = New System.Drawing.Size(44, 16)
        Me.LabelControl6.TabIndex = 3
        Me.LabelControl6.Text = "&Refresh"
        '
        'picRefresh
        '
        Me.picRefresh.EditValue = CType(resources.GetObject("picRefresh.EditValue"), Object)
        Me.picRefresh.Location = New System.Drawing.Point(12, 12)
        Me.picRefresh.Name = "picRefresh"
        Me.picRefresh.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.picRefresh.Properties.Appearance.Options.UseBackColor = True
        Me.picRefresh.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.picRefresh.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Zoom
        Me.picRefresh.Size = New System.Drawing.Size(40, 40)
        Me.picRefresh.TabIndex = 2
        '
        'printSystem
        '
        Me.printSystem.Links.AddRange(New Object() {Me.printableComponentLink})
        '
        'printableComponentLink
        '
        Me.printableComponentLink.Component = Me.grd
        Me.printableComponentLink.Landscape = True
        Me.printableComponentLink.PaperKind = System.Drawing.Printing.PaperKind.Custom
        Me.printableComponentLink.PrintingSystemBase = Me.printSystem
        '
        'frmReportMedicalRoom
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1018, 572)
        Me.Controls.Add(Me.GroupControl1)
        Me.Controls.Add(Me.panelMenu)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmReportMedicalRoom"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Pemetaan Ruangan"
        CType(Me.grv1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grd, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ContextMenuStrip1.ResumeLayout(False)
        CType(Me.grv, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GroupControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl1.ResumeLayout(False)
        CType(Me.panelMenu, System.ComponentModel.ISupportInitialize).EndInit()
        Me.panelMenu.ResumeLayout(False)
        Me.panelMenu.PerformLayout()
        CType(Me.deDATEFROM.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.deDATEFROM.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.deDATETO.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.deDATETO.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkAll.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtPARAMETER.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cboFILTER.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdUSERHADNOVER.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView27, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdDPJPUtama.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.picRefresh.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.printSystem, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.BindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents GroupControl1 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents grd As DevExpress.XtraGrid.GridControl
    Friend WithEvents grv As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents panelMenu As DevExpress.XtraEditors.PanelControl
    Friend WithEvents LabelControl6 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents picRefresh As DevExpress.XtraEditors.PictureEdit
    Friend WithEvents grv1 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents printSystem As DevExpress.XtraPrinting.PrintingSystem
    Friend WithEvents printableComponentLink As DevExpress.XtraPrinting.PrintableComponentLink
    Friend WithEvents BindingSource As BindingSource
    Friend WithEvents ContextMenuStrip1 As ContextMenuStrip
    Friend WithEvents UpdateListToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents grdDPJPUtama As DevExpress.XtraEditors.GridLookUpEdit
    Friend WithEvents GridView1 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridColumn2 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn3 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents LabelControl1 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents grdUSERHADNOVER As DevExpress.XtraEditors.GridLookUpEdit
    Friend WithEvents GridView27 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridColumn44 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents lblPERAWAT As DevExpress.XtraEditors.LabelControl
    Friend WithEvents txtPARAMETER As DevExpress.XtraEditors.TextEdit
    Friend WithEvents lblHiden1 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents cboFILTER As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents btnCari As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents chkAll As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents deDATEFROM As DevExpress.XtraEditors.DateEdit
    Friend WithEvents deDATETO As DevExpress.XtraEditors.DateEdit
    Friend WithEvents AlihDPJPToolStripMenuItem As ToolStripMenuItem
End Class
