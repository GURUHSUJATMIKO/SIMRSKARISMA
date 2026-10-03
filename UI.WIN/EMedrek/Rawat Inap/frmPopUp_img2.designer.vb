<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmPopUp_img2
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
        Me.PanelControl1 = New DevExpress.XtraEditors.PanelControl()
        Me.picIMAGE = New System.Windows.Forms.PictureBox()
        Me.ContextMenuStrip1 = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.ResetGambarToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.LayoutControlGroup1 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.LayoutControlItem1 = New DevExpress.XtraLayout.LayoutControlItem()
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
        CType(Me.layoutControl, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.layoutControl.SuspendLayout()
        CType(Me.PanelControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelControl1.SuspendLayout()
        CType(Me.picIMAGE, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ContextMenuStrip1.SuspendLayout()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.barManager, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.progressBarSave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.progressSave, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'layoutControl
        '
        Me.layoutControl.Controls.Add(Me.PanelControl1)
        Me.layoutControl.Dock = System.Windows.Forms.DockStyle.Fill
        Me.layoutControl.Location = New System.Drawing.Point(0, 0)
        Me.layoutControl.Name = "layoutControl"
        Me.layoutControl.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = New System.Drawing.Rectangle(628, 238, 250, 350)
        Me.layoutControl.Root = Me.LayoutControlGroup1
        Me.layoutControl.Size = New System.Drawing.Size(501, 308)
        Me.layoutControl.TabIndex = 0
        Me.layoutControl.Text = "LayoutControl1"
        '
        'PanelControl1
        '
        Me.PanelControl1.Controls.Add(Me.picIMAGE)
        Me.PanelControl1.Location = New System.Drawing.Point(12, 12)
        Me.PanelControl1.Name = "PanelControl1"
        Me.PanelControl1.Size = New System.Drawing.Size(477, 284)
        Me.PanelControl1.TabIndex = 4
        '
        'picIMAGE
        '
        Me.picIMAGE.ContextMenuStrip = Me.ContextMenuStrip1
        Me.picIMAGE.Dock = System.Windows.Forms.DockStyle.Fill
        Me.picIMAGE.Image = Global.UI.WIN.MAIN.My.Resources.Resources.newrologi
        Me.picIMAGE.Location = New System.Drawing.Point(2, 2)
        Me.picIMAGE.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.picIMAGE.Name = "picIMAGE"
        Me.picIMAGE.Size = New System.Drawing.Size(473, 280)
        Me.picIMAGE.TabIndex = 77
        Me.picIMAGE.TabStop = False
        '
        'ContextMenuStrip1
        '
        Me.ContextMenuStrip1.ImageScalingSize = New System.Drawing.Size(20, 20)
        Me.ContextMenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ResetGambarToolStripMenuItem})
        Me.ContextMenuStrip1.Name = "ContextMenuStrip1"
        Me.ContextMenuStrip1.Size = New System.Drawing.Size(148, 26)
        '
        'ResetGambarToolStripMenuItem
        '
        Me.ResetGambarToolStripMenuItem.Name = "ResetGambarToolStripMenuItem"
        Me.ResetGambarToolStripMenuItem.Size = New System.Drawing.Size(147, 22)
        Me.ResetGambarToolStripMenuItem.Text = "Reset Gambar"
        '
        'LayoutControlGroup1
        '
        Me.LayoutControlGroup1.CustomizationFormText = "LayoutControlGroup1"
        Me.LayoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.LayoutControlGroup1.GroupBordersVisible = False
        Me.LayoutControlGroup1.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlItem1})
        Me.LayoutControlGroup1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlGroup1.Name = "Root"
        Me.LayoutControlGroup1.Size = New System.Drawing.Size(501, 308)
        Me.LayoutControlGroup1.TextVisible = False
        '
        'LayoutControlItem1
        '
        Me.LayoutControlItem1.Control = Me.PanelControl1
        Me.LayoutControlItem1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlItem1.Name = "LayoutControlItem1"
        Me.LayoutControlItem1.Size = New System.Drawing.Size(481, 288)
        Me.LayoutControlItem1.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem1.TextVisible = False
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
        Me.btnSaveClose.Caption = "F3 - Save"
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
        Me.barDockControlTop.Size = New System.Drawing.Size(501, 0)
        '
        'barDockControlBottom
        '
        Me.barDockControlBottom.CausesValidation = False
        Me.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.barDockControlBottom.Location = New System.Drawing.Point(0, 308)
        Me.barDockControlBottom.Size = New System.Drawing.Size(501, 22)
        '
        'barDockControlLeft
        '
        Me.barDockControlLeft.CausesValidation = False
        Me.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left
        Me.barDockControlLeft.Location = New System.Drawing.Point(0, 0)
        Me.barDockControlLeft.Size = New System.Drawing.Size(0, 308)
        '
        'barDockControlRight
        '
        Me.barDockControlRight.CausesValidation = False
        Me.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right
        Me.barDockControlRight.Location = New System.Drawing.Point(501, 0)
        Me.barDockControlRight.Size = New System.Drawing.Size(0, 308)
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
        'frmPopUp_img2
        '
        Me.Appearance.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(236, Byte), Integer), CType(CType(239, Byte), Integer))
        Me.Appearance.Options.UseBackColor = True
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(501, 330)
        Me.Controls.Add(Me.layoutControl)
        Me.Controls.Add(Me.barDockControlLeft)
        Me.Controls.Add(Me.barDockControlRight)
        Me.Controls.Add(Me.barDockControlBottom)
        Me.Controls.Add(Me.barDockControlTop)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.KeyPreview = true
        Me.MaximizeBox = false
        Me.MinimizeBox = false
        Me.Name = "frmPopUp_img2"
        Me.ShowIcon = false
        Me.ShowInTaskbar = false
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Pop Up Image"
        CType(Me.layoutControl,System.ComponentModel.ISupportInitialize).EndInit
        Me.layoutControl.ResumeLayout(false)
        CType(Me.PanelControl1,System.ComponentModel.ISupportInitialize).EndInit
        Me.PanelControl1.ResumeLayout(false)
        CType(Me.picIMAGE,System.ComponentModel.ISupportInitialize).EndInit
        Me.ContextMenuStrip1.ResumeLayout(false)
        CType(Me.LayoutControlGroup1,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.LayoutControlItem1,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.barManager,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.progressBarSave,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.progressSave,System.ComponentModel.ISupportInitialize).EndInit
        Me.ResumeLayout(false)
        Me.PerformLayout

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
    Friend WithEvents PanelControl1 As DevExpress.XtraEditors.PanelControl
    Friend WithEvents LayoutControlItem1 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents picIMAGE As PictureBox
    Friend WithEvents ContextMenuStrip1 As ContextMenuStrip
    Friend WithEvents ResetGambarToolStripMenuItem As ToolStripMenuItem
End Class
