<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmDIGITAL_IGD_01_AWALList
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmDIGITAL_IGD_01_AWALList))
        Me.mnuStrip = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.MasterColumnChooserToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.BarButtonItem1 = New DevExpress.XtraBars.BarButtonItem()
        Me.BarButtonItem2 = New DevExpress.XtraBars.BarButtonItem()
        Me.BarButtonItem3 = New DevExpress.XtraBars.BarButtonItem()
        Me.printSystem = New DevExpress.XtraPrinting.PrintingSystem(Me.components)
        Me.PrintableComponentLink = New DevExpress.XtraPrinting.PrintableComponentLink(Me.components)
        Me.PanelControl2 = New DevExpress.XtraEditors.PanelControl()
        Me.picAdd = New DevExpress.XtraEditors.PictureEdit()
        Me.picPrint = New DevExpress.XtraEditors.PictureEdit()
        Me.deDATETo = New DevExpress.XtraEditors.DateEdit()
        Me.LabelControl1 = New DevExpress.XtraEditors.LabelControl()
        Me.picDelete = New DevExpress.XtraEditors.PictureEdit()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.LabelControl3 = New DevExpress.XtraEditors.LabelControl()
        Me.picUpdate = New DevExpress.XtraEditors.PictureEdit()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.LabelControl4 = New DevExpress.XtraEditors.LabelControl()
        Me.deDATEFrom = New DevExpress.XtraEditors.DateEdit()
        Me.picRefresh = New DevExpress.XtraEditors.PictureEdit()
        Me.LabelControl6 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl7 = New DevExpress.XtraEditors.LabelControl()
        Me.grd = New DevExpress.XtraGrid.GridControl()
        Me.grv = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.mnuStrip.SuspendLayout()
        CType(Me.printSystem, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PanelControl2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelControl2.SuspendLayout()
        CType(Me.picAdd.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.picPrint.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.deDATETo.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.deDATETo.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.picDelete.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.picUpdate.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.deDATEFrom.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.deDATEFrom.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.picRefresh.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grd, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grv, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'mnuStrip
        '
        Me.mnuStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.MasterColumnChooserToolStripMenuItem})
        Me.mnuStrip.Name = "mnuStrip"
        Me.mnuStrip.Size = New System.Drawing.Size(204, 26)
        '
        'MasterColumnChooserToolStripMenuItem
        '
        Me.MasterColumnChooserToolStripMenuItem.Name = "MasterColumnChooserToolStripMenuItem"
        Me.MasterColumnChooserToolStripMenuItem.Size = New System.Drawing.Size(203, 22)
        Me.MasterColumnChooserToolStripMenuItem.Text = "Master Column Chooser"
        '
        'BarButtonItem1
        '
        Me.BarButtonItem1.Caption = "New"
        Me.BarButtonItem1.Id = 0
        Me.BarButtonItem1.Name = "BarButtonItem1"
        '
        'BarButtonItem2
        '
        Me.BarButtonItem2.Caption = "Edit"
        Me.BarButtonItem2.Id = 1
        Me.BarButtonItem2.Name = "BarButtonItem2"
        '
        'BarButtonItem3
        '
        Me.BarButtonItem3.Caption = "Delete"
        Me.BarButtonItem3.Id = 2
        Me.BarButtonItem3.Name = "BarButtonItem3"
        '
        'printSystem
        '
        Me.printSystem.Links.AddRange(New Object() {Me.PrintableComponentLink})
        '
        'PrintableComponentLink
        '
        Me.PrintableComponentLink.PaperKind = System.Drawing.Printing.PaperKind.A4
        Me.PrintableComponentLink.PrintingSystemBase = Me.printSystem
        '
        'PanelControl2
        '
        Me.PanelControl2.Controls.Add(Me.picAdd)
        Me.PanelControl2.Controls.Add(Me.picPrint)
        Me.PanelControl2.Controls.Add(Me.deDATETo)
        Me.PanelControl2.Controls.Add(Me.LabelControl1)
        Me.PanelControl2.Controls.Add(Me.picDelete)
        Me.PanelControl2.Controls.Add(Me.Label2)
        Me.PanelControl2.Controls.Add(Me.LabelControl3)
        Me.PanelControl2.Controls.Add(Me.picUpdate)
        Me.PanelControl2.Controls.Add(Me.Label1)
        Me.PanelControl2.Controls.Add(Me.LabelControl4)
        Me.PanelControl2.Controls.Add(Me.deDATEFrom)
        Me.PanelControl2.Controls.Add(Me.picRefresh)
        Me.PanelControl2.Controls.Add(Me.LabelControl6)
        Me.PanelControl2.Controls.Add(Me.LabelControl7)
        Me.PanelControl2.Dock = System.Windows.Forms.DockStyle.Top
        Me.PanelControl2.Location = New System.Drawing.Point(0, 0)
        Me.PanelControl2.Name = "PanelControl2"
        Me.PanelControl2.Size = New System.Drawing.Size(792, 140)
        Me.PanelControl2.TabIndex = 18
        '
        'picAdd
        '
        Me.picAdd.EditValue = CType(resources.GetObject("picAdd.EditValue"), Object)
        Me.picAdd.Location = New System.Drawing.Point(12, 12)
        Me.picAdd.Name = "picAdd"
        Me.picAdd.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.picAdd.Properties.Appearance.Options.UseBackColor = True
        Me.picAdd.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.picAdd.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Zoom
        Me.picAdd.Size = New System.Drawing.Size(48, 48)
        Me.picAdd.TabIndex = 8
        '
        'picPrint
        '
        Me.picPrint.EditValue = CType(resources.GetObject("picPrint.EditValue"), Object)
        Me.picPrint.Location = New System.Drawing.Point(174, 12)
        Me.picPrint.Name = "picPrint"
        Me.picPrint.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.picPrint.Properties.Appearance.Options.UseBackColor = True
        Me.picPrint.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.picPrint.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Zoom
        Me.picPrint.Size = New System.Drawing.Size(48, 48)
        Me.picPrint.TabIndex = 4
        '
        'deDATETo
        '
        Me.deDATETo.EditValue = Nothing
        Me.deDATETo.EnterMoveNextControl = True
        Me.deDATETo.Location = New System.Drawing.Point(117, 112)
        Me.deDATETo.Name = "deDATETo"
        Me.deDATETo.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.deDATETo.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
        Me.deDATETo.Properties.Mask.EditMask = "dd/MM/yyyy"
        Me.deDATETo.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.deDATETo.Size = New System.Drawing.Size(113, 20)
        Me.deDATETo.TabIndex = 3
        '
        'LabelControl1
        '
        Me.LabelControl1.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.LabelControl1.Location = New System.Drawing.Point(25, 62)
        Me.LabelControl1.Name = "LabelControl1"
        Me.LabelControl1.Size = New System.Drawing.Size(22, 13)
        Me.LabelControl1.TabIndex = 5
        Me.LabelControl1.Text = "&Add"
        '
        'picDelete
        '
        Me.picDelete.EditValue = CType(resources.GetObject("picDelete.EditValue"), Object)
        Me.picDelete.Location = New System.Drawing.Point(120, 12)
        Me.picDelete.Name = "picDelete"
        Me.picDelete.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.picDelete.Properties.Appearance.Options.UseBackColor = True
        Me.picDelete.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.picDelete.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Zoom
        Me.picDelete.Size = New System.Drawing.Size(48, 48)
        Me.picDelete.TabIndex = 7
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(18, 115)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(89, 13)
        Me.Label2.TabIndex = 15
        Me.Label2.Text = "Sampai Tanggal :"
        '
        'LabelControl3
        '
        Me.LabelControl3.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.LabelControl3.Location = New System.Drawing.Point(80, 62)
        Me.LabelControl3.Name = "LabelControl3"
        Me.LabelControl3.Size = New System.Drawing.Size(21, 13)
        Me.LabelControl3.TabIndex = 9
        Me.LabelControl3.Text = "&Edit"
        '
        'picUpdate
        '
        Me.picUpdate.EditValue = CType(resources.GetObject("picUpdate.EditValue"), Object)
        Me.picUpdate.Location = New System.Drawing.Point(66, 12)
        Me.picUpdate.Name = "picUpdate"
        Me.picUpdate.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.picUpdate.Properties.Appearance.Options.UseBackColor = True
        Me.picUpdate.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.picUpdate.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Zoom
        Me.picUpdate.Size = New System.Drawing.Size(48, 48)
        Me.picUpdate.TabIndex = 6
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(33, 89)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(74, 13)
        Me.Label1.TabIndex = 14
        Me.Label1.Text = "Dari Tanggal :"
        '
        'LabelControl4
        '
        Me.LabelControl4.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.LabelControl4.Location = New System.Drawing.Point(126, 62)
        Me.LabelControl4.Name = "LabelControl4"
        Me.LabelControl4.Size = New System.Drawing.Size(37, 13)
        Me.LabelControl4.TabIndex = 11
        Me.LabelControl4.Text = "&Delete"
        '
        'deDATEFrom
        '
        Me.deDATEFrom.EditValue = Nothing
        Me.deDATEFrom.EnterMoveNextControl = True
        Me.deDATEFrom.Location = New System.Drawing.Point(117, 86)
        Me.deDATEFrom.Name = "deDATEFrom"
        Me.deDATEFrom.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.deDATEFrom.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
        Me.deDATEFrom.Properties.Mask.EditMask = "dd/MM/yyyy"
        Me.deDATEFrom.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.deDATEFrom.Size = New System.Drawing.Size(113, 20)
        Me.deDATEFrom.TabIndex = 6
        '
        'picRefresh
        '
        Me.picRefresh.EditValue = CType(resources.GetObject("picRefresh.EditValue"), Object)
        Me.picRefresh.Location = New System.Drawing.Point(228, 12)
        Me.picRefresh.Name = "picRefresh"
        Me.picRefresh.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.picRefresh.Properties.Appearance.Options.UseBackColor = True
        Me.picRefresh.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.picRefresh.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Zoom
        Me.picRefresh.Size = New System.Drawing.Size(48, 48)
        Me.picRefresh.TabIndex = 12
        '
        'LabelControl6
        '
        Me.LabelControl6.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.LabelControl6.Location = New System.Drawing.Point(230, 62)
        Me.LabelControl6.Name = "LabelControl6"
        Me.LabelControl6.Size = New System.Drawing.Size(44, 13)
        Me.LabelControl6.TabIndex = 13
        Me.LabelControl6.Text = "&Refresh"
        '
        'LabelControl7
        '
        Me.LabelControl7.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.LabelControl7.Location = New System.Drawing.Point(185, 62)
        Me.LabelControl7.Name = "LabelControl7"
        Me.LabelControl7.Size = New System.Drawing.Size(27, 13)
        Me.LabelControl7.TabIndex = 10
        Me.LabelControl7.Text = "&Print"
        '
        'grd
        '
        Me.grd.ContextMenuStrip = Me.mnuStrip
        Me.grd.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grd.EmbeddedNavigator.Buttons.Append.Visible = False
        Me.grd.EmbeddedNavigator.Buttons.CancelEdit.Visible = False
        Me.grd.EmbeddedNavigator.Buttons.Edit.Visible = False
        Me.grd.EmbeddedNavigator.Buttons.EndEdit.Visible = False
        Me.grd.EmbeddedNavigator.Buttons.Remove.Visible = False
        Me.grd.Location = New System.Drawing.Point(0, 140)
        Me.grd.MainView = Me.grv
        Me.grd.Name = "grd"
        Me.grd.Size = New System.Drawing.Size(792, 433)
        Me.grd.TabIndex = 19
        Me.grd.UseEmbeddedNavigator = True
        Me.grd.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.grv})
        '
        'grv
        '
        Me.grv.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.grv.GridControl = Me.grd
        Me.grv.Name = "grv"
        Me.grv.OptionsBehavior.Editable = False
        Me.grv.OptionsBehavior.ReadOnly = True
        Me.grv.OptionsFind.AlwaysVisible = True
        Me.grv.OptionsFind.ShowFindButton = False
        Me.grv.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.grv.OptionsView.ShowAutoFilterRow = True
        Me.grv.OptionsView.ShowGroupPanel = False
        '
        'frmDIGITAL_IGD_01_AWALList
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(792, 573)
        Me.Controls.Add(Me.grd)
        Me.Controls.Add(Me.PanelControl2)
        Me.KeyPreview = True
        Me.Name = "frmDIGITAL_IGD_01_AWALList"
        Me.ShowIcon = False
        Me.mnuStrip.ResumeLayout(False)
        CType(Me.printSystem, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PanelControl2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelControl2.ResumeLayout(False)
        Me.PanelControl2.PerformLayout()
        CType(Me.picAdd.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.picPrint.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.deDATETo.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.deDATETo.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.picDelete.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.picUpdate.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.deDATEFrom.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.deDATEFrom.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.picRefresh.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grd, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grv, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents BarButtonItem1 As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents BarButtonItem2 As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents BarButtonItem3 As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents mnuStrip As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents MasterColumnChooserToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents printSystem As DevExpress.XtraPrinting.PrintingSystem
    Friend WithEvents PrintableComponentLink As DevExpress.XtraPrinting.PrintableComponentLink
    Friend WithEvents PanelControl2 As DevExpress.XtraEditors.PanelControl
    Friend WithEvents picAdd As DevExpress.XtraEditors.PictureEdit
    Friend WithEvents picPrint As DevExpress.XtraEditors.PictureEdit
    Friend WithEvents deDATETo As DevExpress.XtraEditors.DateEdit
    Friend WithEvents LabelControl1 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents picDelete As DevExpress.XtraEditors.PictureEdit
    Friend WithEvents Label2 As Label
    Friend WithEvents LabelControl3 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents picUpdate As DevExpress.XtraEditors.PictureEdit
    Friend WithEvents Label1 As Label
    Friend WithEvents LabelControl4 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents deDATEFrom As DevExpress.XtraEditors.DateEdit
    Friend WithEvents picRefresh As DevExpress.XtraEditors.PictureEdit
    Friend WithEvents LabelControl6 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl7 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents grd As DevExpress.XtraGrid.GridControl
    Friend WithEvents grv As DevExpress.XtraGrid.Views.Grid.GridView
End Class
