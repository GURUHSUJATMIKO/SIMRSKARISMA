<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmItemDiagnosaPerawatList
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmItemDiagnosaPerawatList))
        Me.splitContainerControl = New DevExpress.XtraEditors.SplitContainerControl()
        Me.grd = New DevExpress.XtraGrid.GridControl()
        Me.grv = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.PanelControl1 = New DevExpress.XtraEditors.PanelControl()
        Me.LabelControl6 = New DevExpress.XtraEditors.LabelControl()
        Me.picAdd = New DevExpress.XtraEditors.PictureEdit()
        Me.LabelControl7 = New DevExpress.XtraEditors.LabelControl()
        Me.picRefresh = New DevExpress.XtraEditors.PictureEdit()
        Me.LabelControl4 = New DevExpress.XtraEditors.LabelControl()
        Me.picUpdate = New DevExpress.XtraEditors.PictureEdit()
        Me.LabelControl3 = New DevExpress.XtraEditors.LabelControl()
        Me.picDelete = New DevExpress.XtraEditors.PictureEdit()
        Me.LabelControl1 = New DevExpress.XtraEditors.LabelControl()
        Me.picPrint = New DevExpress.XtraEditors.PictureEdit()
        Me.SplitterControl1 = New DevExpress.XtraEditors.SplitterControl()
        Me.groupControlInfo = New DevExpress.XtraEditors.GroupControl()
        Me.lc = New DevExpress.XtraLayout.LayoutControl()
        Me.btnView = New DevExpress.XtraEditors.SimpleButton()
        Me.txtDESCRIPTION = New DevExpress.XtraEditors.TextEdit()
        Me.LayoutControlGroup1 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.lcName_Display = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem11 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem2 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem1 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.BarButtonItem1 = New DevExpress.XtraBars.BarButtonItem()
        Me.BarButtonItem2 = New DevExpress.XtraBars.BarButtonItem()
        Me.BarButtonItem3 = New DevExpress.XtraBars.BarButtonItem()
        CType(Me.splitContainerControl, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.splitContainerControl.SuspendLayout()
        CType(Me.grd, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grv, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PanelControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelControl1.SuspendLayout()
        CType(Me.picAdd.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.picRefresh.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.picUpdate.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.picDelete.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.picPrint.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.groupControlInfo, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.groupControlInfo.SuspendLayout()
        CType(Me.lc, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.lc.SuspendLayout()
        CType(Me.txtDESCRIPTION.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lcName_Display, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem11, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'splitContainerControl
        '
        Me.splitContainerControl.CollapsePanel = DevExpress.XtraEditors.SplitCollapsePanel.Panel1
        Me.splitContainerControl.Dock = System.Windows.Forms.DockStyle.Fill
        Me.splitContainerControl.Location = New System.Drawing.Point(0, 0)
        Me.splitContainerControl.Name = "splitContainerControl"
        Me.splitContainerControl.Panel1.Controls.Add(Me.grd)
        Me.splitContainerControl.Panel1.Controls.Add(Me.PanelControl1)
        Me.splitContainerControl.Panel1.Text = "Panel1"
        Me.splitContainerControl.Panel2.Controls.Add(Me.SplitterControl1)
        Me.splitContainerControl.Panel2.Controls.Add(Me.groupControlInfo)
        Me.splitContainerControl.Panel2.Text = "Panel2"
        Me.splitContainerControl.Size = New System.Drawing.Size(792, 573)
        Me.splitContainerControl.SplitterPosition = 500
        Me.splitContainerControl.TabIndex = 0
        '
        'grd
        '
        Me.grd.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grd.EmbeddedNavigator.Buttons.Append.Visible = False
        Me.grd.EmbeddedNavigator.Buttons.CancelEdit.Visible = False
        Me.grd.EmbeddedNavigator.Buttons.Edit.Visible = False
        Me.grd.EmbeddedNavigator.Buttons.EndEdit.Visible = False
        Me.grd.EmbeddedNavigator.Buttons.Remove.Visible = False
        Me.grd.Location = New System.Drawing.Point(0, 82)
        Me.grd.MainView = Me.grv
        Me.grd.Name = "grd"
        Me.grd.Size = New System.Drawing.Size(500, 491)
        Me.grd.TabIndex = 0
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
        Me.grv.OptionsView.ShowGroupPanel = False
        '
        'PanelControl1
        '
        Me.PanelControl1.Controls.Add(Me.LabelControl6)
        Me.PanelControl1.Controls.Add(Me.picAdd)
        Me.PanelControl1.Controls.Add(Me.LabelControl7)
        Me.PanelControl1.Controls.Add(Me.picRefresh)
        Me.PanelControl1.Controls.Add(Me.LabelControl4)
        Me.PanelControl1.Controls.Add(Me.picUpdate)
        Me.PanelControl1.Controls.Add(Me.LabelControl3)
        Me.PanelControl1.Controls.Add(Me.picDelete)
        Me.PanelControl1.Controls.Add(Me.LabelControl1)
        Me.PanelControl1.Controls.Add(Me.picPrint)
        Me.PanelControl1.Dock = System.Windows.Forms.DockStyle.Top
        Me.PanelControl1.Location = New System.Drawing.Point(0, 0)
        Me.PanelControl1.Name = "PanelControl1"
        Me.PanelControl1.Size = New System.Drawing.Size(500, 82)
        Me.PanelControl1.TabIndex = 1
        '
        'LabelControl6
        '
        Me.LabelControl6.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.LabelControl6.Location = New System.Drawing.Point(236, 60)
        Me.LabelControl6.Name = "LabelControl6"
        Me.LabelControl6.Size = New System.Drawing.Size(44, 13)
        Me.LabelControl6.TabIndex = 13
        Me.LabelControl6.Text = "&Refresh"
        '
        'picAdd
        '
        Me.picAdd.EditValue = CType(resources.GetObject("picAdd.EditValue"), Object)
        Me.picAdd.Location = New System.Drawing.Point(18, 10)
        Me.picAdd.Name = "picAdd"
        Me.picAdd.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.picAdd.Properties.Appearance.Options.UseBackColor = True
        Me.picAdd.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.picAdd.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Zoom
        Me.picAdd.Size = New System.Drawing.Size(48, 48)
        Me.picAdd.TabIndex = 8
        '
        'LabelControl7
        '
        Me.LabelControl7.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.LabelControl7.Location = New System.Drawing.Point(191, 60)
        Me.LabelControl7.Name = "LabelControl7"
        Me.LabelControl7.Size = New System.Drawing.Size(27, 13)
        Me.LabelControl7.TabIndex = 10
        Me.LabelControl7.Text = "&Print"
        '
        'picRefresh
        '
        Me.picRefresh.EditValue = CType(resources.GetObject("picRefresh.EditValue"), Object)
        Me.picRefresh.Location = New System.Drawing.Point(234, 10)
        Me.picRefresh.Name = "picRefresh"
        Me.picRefresh.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.picRefresh.Properties.Appearance.Options.UseBackColor = True
        Me.picRefresh.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.picRefresh.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Zoom
        Me.picRefresh.Size = New System.Drawing.Size(48, 48)
        Me.picRefresh.TabIndex = 12
        '
        'LabelControl4
        '
        Me.LabelControl4.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.LabelControl4.Location = New System.Drawing.Point(132, 60)
        Me.LabelControl4.Name = "LabelControl4"
        Me.LabelControl4.Size = New System.Drawing.Size(37, 13)
        Me.LabelControl4.TabIndex = 11
        Me.LabelControl4.Text = "&Delete"
        '
        'picUpdate
        '
        Me.picUpdate.EditValue = CType(resources.GetObject("picUpdate.EditValue"), Object)
        Me.picUpdate.Location = New System.Drawing.Point(72, 10)
        Me.picUpdate.Name = "picUpdate"
        Me.picUpdate.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.picUpdate.Properties.Appearance.Options.UseBackColor = True
        Me.picUpdate.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.picUpdate.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Zoom
        Me.picUpdate.Size = New System.Drawing.Size(48, 48)
        Me.picUpdate.TabIndex = 6
        '
        'LabelControl3
        '
        Me.LabelControl3.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.LabelControl3.Location = New System.Drawing.Point(86, 60)
        Me.LabelControl3.Name = "LabelControl3"
        Me.LabelControl3.Size = New System.Drawing.Size(21, 13)
        Me.LabelControl3.TabIndex = 9
        Me.LabelControl3.Text = "&Edit"
        '
        'picDelete
        '
        Me.picDelete.EditValue = CType(resources.GetObject("picDelete.EditValue"), Object)
        Me.picDelete.Location = New System.Drawing.Point(126, 10)
        Me.picDelete.Name = "picDelete"
        Me.picDelete.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.picDelete.Properties.Appearance.Options.UseBackColor = True
        Me.picDelete.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.picDelete.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Zoom
        Me.picDelete.Size = New System.Drawing.Size(48, 48)
        Me.picDelete.TabIndex = 7
        '
        'LabelControl1
        '
        Me.LabelControl1.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.LabelControl1.Location = New System.Drawing.Point(31, 60)
        Me.LabelControl1.Name = "LabelControl1"
        Me.LabelControl1.Size = New System.Drawing.Size(22, 13)
        Me.LabelControl1.TabIndex = 5
        Me.LabelControl1.Text = "&Add"
        '
        'picPrint
        '
        Me.picPrint.EditValue = CType(resources.GetObject("picPrint.EditValue"), Object)
        Me.picPrint.Location = New System.Drawing.Point(180, 10)
        Me.picPrint.Name = "picPrint"
        Me.picPrint.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.picPrint.Properties.Appearance.Options.UseBackColor = True
        Me.picPrint.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.picPrint.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Zoom
        Me.picPrint.Size = New System.Drawing.Size(48, 48)
        Me.picPrint.TabIndex = 4
        '
        'SplitterControl1
        '
        Me.SplitterControl1.Dock = System.Windows.Forms.DockStyle.Top
        Me.SplitterControl1.Location = New System.Drawing.Point(0, 310)
        Me.SplitterControl1.Name = "SplitterControl1"
        Me.SplitterControl1.Size = New System.Drawing.Size(287, 5)
        Me.SplitterControl1.TabIndex = 1
        Me.SplitterControl1.TabStop = False
        '
        'groupControlInfo
        '
        Me.groupControlInfo.Controls.Add(Me.lc)
        Me.groupControlInfo.Dock = System.Windows.Forms.DockStyle.Top
        Me.groupControlInfo.Location = New System.Drawing.Point(0, 0)
        Me.groupControlInfo.Name = "groupControlInfo"
        Me.groupControlInfo.Size = New System.Drawing.Size(287, 310)
        Me.groupControlInfo.TabIndex = 0
        Me.groupControlInfo.Text = "Information"
        '
        'lc
        '
        Me.lc.Controls.Add(Me.btnView)
        Me.lc.Controls.Add(Me.txtDESCRIPTION)
        Me.lc.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lc.Location = New System.Drawing.Point(2, 20)
        Me.lc.Name = "lc"
        Me.lc.Root = Me.LayoutControlGroup1
        Me.lc.Size = New System.Drawing.Size(283, 288)
        Me.lc.TabIndex = 0
        Me.lc.Text = "LayoutControl1"
        '
        'btnView
        '
        Me.btnView.Location = New System.Drawing.Point(12, 36)
        Me.btnView.Name = "btnView"
        Me.btnView.Size = New System.Drawing.Size(259, 22)
        Me.btnView.StyleController = Me.lc
        Me.btnView.TabIndex = 13
        Me.btnView.TabStop = False
        Me.btnView.Text = "View"
        '
        'txtDESCRIPTION
        '
        Me.txtDESCRIPTION.EnterMoveNextControl = True
        Me.txtDESCRIPTION.Location = New System.Drawing.Point(117, 12)
        Me.txtDESCRIPTION.Name = "txtDESCRIPTION"
        Me.txtDESCRIPTION.Properties.ReadOnly = True
        Me.txtDESCRIPTION.Size = New System.Drawing.Size(154, 20)
        Me.txtDESCRIPTION.StyleController = Me.lc
        Me.txtDESCRIPTION.TabIndex = 4
        Me.txtDESCRIPTION.TabStop = False
        '
        'LayoutControlGroup1
        '
        Me.LayoutControlGroup1.CustomizationFormText = "LayoutControlGroup1"
        Me.LayoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.LayoutControlGroup1.GroupBordersVisible = False
        Me.LayoutControlGroup1.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.lcName_Display, Me.LayoutControlItem11})
        Me.LayoutControlGroup1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlGroup1.Name = "LayoutControlGroup1"
        Me.LayoutControlGroup1.Size = New System.Drawing.Size(283, 288)
        Me.LayoutControlGroup1.TextVisible = False
        '
        'lcName_Display
        '
        Me.lcName_Display.AppearanceItemCaption.Options.UseTextOptions = True
        Me.lcName_Display.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.lcName_Display.Control = Me.txtDESCRIPTION
        Me.lcName_Display.CustomizationFormText = "Tampilan Nama :"
        Me.lcName_Display.Location = New System.Drawing.Point(0, 0)
        Me.lcName_Display.Name = "lcName_Display"
        Me.lcName_Display.Size = New System.Drawing.Size(263, 24)
        Me.lcName_Display.Text = "Tampilan Nama :"
        Me.lcName_Display.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.lcName_Display.TextSize = New System.Drawing.Size(100, 20)
        Me.lcName_Display.TextToControlDistance = 5
        '
        'LayoutControlItem11
        '
        Me.LayoutControlItem11.Control = Me.btnView
        Me.LayoutControlItem11.CustomizationFormText = "LayoutControlItem11"
        Me.LayoutControlItem11.Location = New System.Drawing.Point(0, 24)
        Me.LayoutControlItem11.Name = "LayoutControlItem11"
        Me.LayoutControlItem11.Size = New System.Drawing.Size(263, 244)
        Me.LayoutControlItem11.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem11.TextVisible = False
        '
        'LayoutControlItem2
        '
        Me.LayoutControlItem2.AppearanceItemCaption.Options.UseTextOptions = True
        Me.LayoutControlItem2.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.LayoutControlItem2.Control = Me.txtDESCRIPTION
        Me.LayoutControlItem2.CustomizationFormText = "Tampilan Nama :"
        Me.LayoutControlItem2.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlItem2.Name = "LayoutControlItem1"
        Me.LayoutControlItem2.Size = New System.Drawing.Size(463, 208)
        Me.LayoutControlItem2.Text = "Tampilan Nama :"
        Me.LayoutControlItem2.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.LayoutControlItem2.TextSize = New System.Drawing.Size(100, 20)
        Me.LayoutControlItem2.TextToControlDistance = 5
        '
        'LayoutControlItem1
        '
        Me.LayoutControlItem1.AppearanceItemCaption.Options.UseTextOptions = True
        Me.LayoutControlItem1.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.LayoutControlItem1.Control = Me.txtDESCRIPTION
        Me.LayoutControlItem1.CustomizationFormText = "Tampilan Nama :"
        Me.LayoutControlItem1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlItem1.Name = "lcName_Display"
        Me.LayoutControlItem1.Size = New System.Drawing.Size(463, 208)
        Me.LayoutControlItem1.Text = "Tampilan Nama :"
        Me.LayoutControlItem1.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.LayoutControlItem1.TextSize = New System.Drawing.Size(100, 20)
        Me.LayoutControlItem1.TextToControlDistance = 5
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
        'frmItemDiagnosaPerawatList
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(792, 573)
        Me.Controls.Add(Me.splitContainerControl)
        Me.KeyPreview = True
        Me.Name = "frmItemDiagnosaPerawatList"
        Me.ShowIcon = False
        Me.Text = "Item Operasi - List Form"
        CType(Me.splitContainerControl, System.ComponentModel.ISupportInitialize).EndInit()
        Me.splitContainerControl.ResumeLayout(False)
        CType(Me.grd, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grv, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PanelControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelControl1.ResumeLayout(False)
        Me.PanelControl1.PerformLayout()
        CType(Me.picAdd.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.picRefresh.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.picUpdate.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.picDelete.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.picPrint.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.groupControlInfo, System.ComponentModel.ISupportInitialize).EndInit()
        Me.groupControlInfo.ResumeLayout(False)
        CType(Me.lc, System.ComponentModel.ISupportInitialize).EndInit()
        Me.lc.ResumeLayout(False)
        CType(Me.txtDESCRIPTION.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lcName_Display, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem11, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents splitContainerControl As DevExpress.XtraEditors.SplitContainerControl
    Friend WithEvents grd As DevExpress.XtraGrid.GridControl
    Friend WithEvents grv As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents groupControlInfo As DevExpress.XtraEditors.GroupControl
    Friend WithEvents SplitterControl1 As DevExpress.XtraEditors.SplitterControl
    Friend WithEvents lc As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents LayoutControlGroup1 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents txtDESCRIPTION As DevExpress.XtraEditors.TextEdit
    Friend WithEvents lcName_Display As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem2 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem1 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents BarButtonItem1 As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents BarButtonItem2 As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents BarButtonItem3 As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents PanelControl1 As DevExpress.XtraEditors.PanelControl
    Friend WithEvents LabelControl6 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents picRefresh As DevExpress.XtraEditors.PictureEdit
    Friend WithEvents LabelControl7 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl4 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl3 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl1 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents picPrint As DevExpress.XtraEditors.PictureEdit
    Friend WithEvents picDelete As DevExpress.XtraEditors.PictureEdit
    Friend WithEvents picUpdate As DevExpress.XtraEditors.PictureEdit
    Friend WithEvents picAdd As DevExpress.XtraEditors.PictureEdit
    Friend WithEvents btnView As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents LayoutControlItem11 As DevExpress.XtraLayout.LayoutControlItem
End Class
