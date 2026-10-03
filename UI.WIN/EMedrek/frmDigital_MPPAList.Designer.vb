<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmDigital_MPPAList
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmDigital_MPPAList))
        Me.grd = New DevExpress.XtraGrid.GridControl()
        Me.grv = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.PanelControl1 = New DevExpress.XtraEditors.PanelControl()
        Me.LabelControl6 = New DevExpress.XtraEditors.LabelControl()
        Me.picAdd_A = New DevExpress.XtraEditors.PictureEdit()
        Me.LabelControl7 = New DevExpress.XtraEditors.LabelControl()
        Me.picRefresh = New DevExpress.XtraEditors.PictureEdit()
        Me.LabelControl4 = New DevExpress.XtraEditors.LabelControl()
        Me.picUpdate = New DevExpress.XtraEditors.PictureEdit()
        Me.LabelControl3 = New DevExpress.XtraEditors.LabelControl()
        Me.picDelete = New DevExpress.XtraEditors.PictureEdit()
        Me.LabelControl1 = New DevExpress.XtraEditors.LabelControl()
        Me.picPrint = New DevExpress.XtraEditors.PictureEdit()
        Me.BarButtonItem1 = New DevExpress.XtraBars.BarButtonItem()
        Me.BarButtonItem2 = New DevExpress.XtraBars.BarButtonItem()
        Me.BarButtonItem3 = New DevExpress.XtraBars.BarButtonItem()
        Me.picAdd_B = New DevExpress.XtraEditors.PictureEdit()
        Me.LabelControl2 = New DevExpress.XtraEditors.LabelControl()
        CType(Me.grd,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.grv,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.PanelControl1,System.ComponentModel.ISupportInitialize).BeginInit
        Me.PanelControl1.SuspendLayout
        CType(Me.picAdd_A.Properties,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.picRefresh.Properties,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.picUpdate.Properties,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.picDelete.Properties,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.picPrint.Properties,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.picAdd_B.Properties,System.ComponentModel.ISupportInitialize).BeginInit
        Me.SuspendLayout
        '
        'grd
        '
        Me.grd.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grd.EmbeddedNavigator.Buttons.Append.Visible = false
        Me.grd.EmbeddedNavigator.Buttons.CancelEdit.Visible = false
        Me.grd.EmbeddedNavigator.Buttons.Edit.Visible = false
        Me.grd.EmbeddedNavigator.Buttons.EndEdit.Visible = false
        Me.grd.EmbeddedNavigator.Buttons.Remove.Visible = false
        Me.grd.Location = New System.Drawing.Point(0, 93)
        Me.grd.MainView = Me.grv
        Me.grd.Name = "grd"
        Me.grd.Size = New System.Drawing.Size(792, 480)
        Me.grd.TabIndex = 0
        Me.grd.UseEmbeddedNavigator = true
        Me.grd.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.grv})
        '
        'grv
        '
        Me.grv.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.grv.GridControl = Me.grd
        Me.grv.Name = "grv"
        Me.grv.OptionsBehavior.Editable = false
        Me.grv.OptionsBehavior.ReadOnly = true
        Me.grv.OptionsFind.AlwaysVisible = true
        Me.grv.OptionsFind.ShowFindButton = false
        Me.grv.OptionsSelection.EnableAppearanceFocusedCell = false
        Me.grv.OptionsView.ShowGroupPanel = false
        '
        'PanelControl1
        '
        Me.PanelControl1.Controls.Add(Me.picAdd_B)
        Me.PanelControl1.Controls.Add(Me.LabelControl6)
        Me.PanelControl1.Controls.Add(Me.picAdd_A)
        Me.PanelControl1.Controls.Add(Me.LabelControl7)
        Me.PanelControl1.Controls.Add(Me.picRefresh)
        Me.PanelControl1.Controls.Add(Me.LabelControl4)
        Me.PanelControl1.Controls.Add(Me.picUpdate)
        Me.PanelControl1.Controls.Add(Me.LabelControl3)
        Me.PanelControl1.Controls.Add(Me.picDelete)
        Me.PanelControl1.Controls.Add(Me.LabelControl2)
        Me.PanelControl1.Controls.Add(Me.LabelControl1)
        Me.PanelControl1.Controls.Add(Me.picPrint)
        Me.PanelControl1.Dock = System.Windows.Forms.DockStyle.Top
        Me.PanelControl1.Location = New System.Drawing.Point(0, 0)
        Me.PanelControl1.Name = "PanelControl1"
        Me.PanelControl1.Size = New System.Drawing.Size(792, 93)
        Me.PanelControl1.TabIndex = 1
        '
        'LabelControl6
        '
        Me.LabelControl6.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.LabelControl6.Location = New System.Drawing.Point(290, 60)
        Me.LabelControl6.Name = "LabelControl6"
        Me.LabelControl6.Size = New System.Drawing.Size(44, 13)
        Me.LabelControl6.TabIndex = 13
        Me.LabelControl6.Text = "&Refresh"
        '
        'picAdd_A
        '
        Me.picAdd_A.EditValue = CType(resources.GetObject("picAdd_A.EditValue"),Object)
        Me.picAdd_A.Location = New System.Drawing.Point(18, 10)
        Me.picAdd_A.Name = "picAdd_A"
        Me.picAdd_A.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.picAdd_A.Properties.Appearance.Options.UseBackColor = true
        Me.picAdd_A.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.picAdd_A.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Zoom
        Me.picAdd_A.Size = New System.Drawing.Size(48, 48)
        Me.picAdd_A.TabIndex = 8
        '
        'LabelControl7
        '
        Me.LabelControl7.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.LabelControl7.Location = New System.Drawing.Point(245, 60)
        Me.LabelControl7.Name = "LabelControl7"
        Me.LabelControl7.Size = New System.Drawing.Size(27, 13)
        Me.LabelControl7.TabIndex = 10
        Me.LabelControl7.Text = "&Print"
        '
        'picRefresh
        '
        Me.picRefresh.EditValue = CType(resources.GetObject("picRefresh.EditValue"),Object)
        Me.picRefresh.Location = New System.Drawing.Point(288, 10)
        Me.picRefresh.Name = "picRefresh"
        Me.picRefresh.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.picRefresh.Properties.Appearance.Options.UseBackColor = true
        Me.picRefresh.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.picRefresh.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Zoom
        Me.picRefresh.Size = New System.Drawing.Size(48, 48)
        Me.picRefresh.TabIndex = 12
        '
        'LabelControl4
        '
        Me.LabelControl4.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.LabelControl4.Location = New System.Drawing.Point(186, 60)
        Me.LabelControl4.Name = "LabelControl4"
        Me.LabelControl4.Size = New System.Drawing.Size(37, 13)
        Me.LabelControl4.TabIndex = 11
        Me.LabelControl4.Text = "&Delete"
        '
        'picUpdate
        '
        Me.picUpdate.EditValue = CType(resources.GetObject("picUpdate.EditValue"),Object)
        Me.picUpdate.Location = New System.Drawing.Point(126, 10)
        Me.picUpdate.Name = "picUpdate"
        Me.picUpdate.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.picUpdate.Properties.Appearance.Options.UseBackColor = true
        Me.picUpdate.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.picUpdate.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Zoom
        Me.picUpdate.Size = New System.Drawing.Size(48, 48)
        Me.picUpdate.TabIndex = 6
        '
        'LabelControl3
        '
        Me.LabelControl3.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.LabelControl3.Location = New System.Drawing.Point(140, 60)
        Me.LabelControl3.Name = "LabelControl3"
        Me.LabelControl3.Size = New System.Drawing.Size(21, 13)
        Me.LabelControl3.TabIndex = 9
        Me.LabelControl3.Text = "&Edit"
        '
        'picDelete
        '
        Me.picDelete.EditValue = CType(resources.GetObject("picDelete.EditValue"),Object)
        Me.picDelete.Location = New System.Drawing.Point(180, 10)
        Me.picDelete.Name = "picDelete"
        Me.picDelete.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.picDelete.Properties.Appearance.Options.UseBackColor = true
        Me.picDelete.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.picDelete.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Zoom
        Me.picDelete.Size = New System.Drawing.Size(48, 48)
        Me.picDelete.TabIndex = 7
        '
        'LabelControl1
        '
        Me.LabelControl1.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.LabelControl1.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.LabelControl1.Location = New System.Drawing.Point(18, 60)
        Me.LabelControl1.Name = "LabelControl1"
        Me.LabelControl1.Size = New System.Drawing.Size(41, 26)
        Me.LabelControl1.TabIndex = 5
        Me.LabelControl1.Text = "Add "&Global.Microsoft.VisualBasic.ChrW(13)&Global.Microsoft.VisualBasic.ChrW(10)&"Form A"
        '
        'picPrint
        '
        Me.picPrint.EditValue = CType(resources.GetObject("picPrint.EditValue"),Object)
        Me.picPrint.Location = New System.Drawing.Point(234, 10)
        Me.picPrint.Name = "picPrint"
        Me.picPrint.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.picPrint.Properties.Appearance.Options.UseBackColor = true
        Me.picPrint.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.picPrint.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Zoom
        Me.picPrint.Size = New System.Drawing.Size(48, 48)
        Me.picPrint.TabIndex = 4
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
        'picAdd_B
        '
        Me.picAdd_B.EditValue = CType(resources.GetObject("picAdd_B.EditValue"),Object)
        Me.picAdd_B.Location = New System.Drawing.Point(72, 10)
        Me.picAdd_B.Name = "picAdd_B"
        Me.picAdd_B.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.picAdd_B.Properties.Appearance.Options.UseBackColor = true
        Me.picAdd_B.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.picAdd_B.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Zoom
        Me.picAdd_B.Size = New System.Drawing.Size(48, 48)
        Me.picAdd_B.TabIndex = 15
        '
        'LabelControl2
        '
        Me.LabelControl2.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.LabelControl2.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.LabelControl2.Location = New System.Drawing.Point(72, 60)
        Me.LabelControl2.Name = "LabelControl2"
        Me.LabelControl2.Size = New System.Drawing.Size(39, 26)
        Me.LabelControl2.TabIndex = 5
        Me.LabelControl2.Text = "Add "&Global.Microsoft.VisualBasic.ChrW(13)&Global.Microsoft.VisualBasic.ChrW(10)&"Form B"
        '
        'frmDigital_MPPAList
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6!, 13!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(792, 573)
        Me.Controls.Add(Me.grd)
        Me.Controls.Add(Me.PanelControl1)
        Me.KeyPreview = true
        Me.Name = "frmDigital_MPPAList"
        Me.ShowIcon = false
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Form MPP - List Form"
        CType(Me.grd,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.grv,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.PanelControl1,System.ComponentModel.ISupportInitialize).EndInit
        Me.PanelControl1.ResumeLayout(false)
        Me.PanelControl1.PerformLayout
        CType(Me.picAdd_A.Properties,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.picRefresh.Properties,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.picUpdate.Properties,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.picDelete.Properties,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.picPrint.Properties,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.picAdd_B.Properties,System.ComponentModel.ISupportInitialize).EndInit
        Me.ResumeLayout(false)

End Sub
    Friend WithEvents grd As DevExpress.XtraGrid.GridControl
    Friend WithEvents grv As DevExpress.XtraGrid.Views.Grid.GridView
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
    Friend WithEvents picAdd_A As DevExpress.XtraEditors.PictureEdit
    Friend WithEvents picAdd_B As DevExpress.XtraEditors.PictureEdit
    Friend WithEvents LabelControl2 As DevExpress.XtraEditors.LabelControl
End Class
