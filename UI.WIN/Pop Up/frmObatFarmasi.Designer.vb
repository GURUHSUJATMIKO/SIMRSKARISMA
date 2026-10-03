<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmObatFarmasi
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.grdKDSIGNA = New DevExpress.XtraEditors.SearchLookUpEdit()
        Me.GridView3 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.LayoutControl1 = New DevExpress.XtraLayout.LayoutControl()
        Me.txtQTY = New DevExpress.XtraEditors.TextEdit()
        Me.grdKDCARAPAKAI = New DevExpress.XtraEditors.SearchLookUpEdit()
        Me.GridView5 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.btnBatal = New DevExpress.XtraEditors.SimpleButton()
        Me.txtREMARKS_DOKTER = New DevExpress.XtraEditors.MemoEdit()
        Me.btnHapus = New DevExpress.XtraEditors.SimpleButton()
        Me.LayoutControlGroup1 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.LayoutControlItem1 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem2 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem3 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem4 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem5 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem6 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.GridColumn4 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn3 = New DevExpress.XtraGrid.Columns.GridColumn()
        CType(Me.grdKDSIGNA.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.LayoutControl1.SuspendLayout()
        CType(Me.txtQTY.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdKDCARAPAKAI.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtREMARKS_DOKTER.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem6, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'grdKDSIGNA
        '
        Me.grdKDSIGNA.Location = New System.Drawing.Point(117, 36)
        Me.grdKDSIGNA.Name = "grdKDSIGNA"
        Me.grdKDSIGNA.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.grdKDSIGNA.Properties.NullText = ""
        Me.grdKDSIGNA.Properties.PopupFormMinSize = New System.Drawing.Size(600, 300)
        Me.grdKDSIGNA.Properties.View = Me.GridView3
        Me.grdKDSIGNA.Size = New System.Drawing.Size(313, 20)
        Me.grdKDSIGNA.StyleController = Me.LayoutControl1
        Me.grdKDSIGNA.TabIndex = 46
        '
        'GridView3
        '
        Me.GridView3.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn3})
        Me.GridView3.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.GridView3.Name = "GridView3"
        Me.GridView3.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.GridView3.OptionsView.ShowAutoFilterRow = True
        Me.GridView3.OptionsView.ShowGroupPanel = False
        '
        'LayoutControl1
        '
        Me.LayoutControl1.Controls.Add(Me.txtQTY)
        Me.LayoutControl1.Controls.Add(Me.grdKDCARAPAKAI)
        Me.LayoutControl1.Controls.Add(Me.btnBatal)
        Me.LayoutControl1.Controls.Add(Me.txtREMARKS_DOKTER)
        Me.LayoutControl1.Controls.Add(Me.btnHapus)
        Me.LayoutControl1.Controls.Add(Me.grdKDSIGNA)
        Me.LayoutControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LayoutControl1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControl1.Name = "LayoutControl1"
        Me.LayoutControl1.Root = Me.LayoutControlGroup1
        Me.LayoutControl1.Size = New System.Drawing.Size(442, 392)
        Me.LayoutControl1.TabIndex = 53
        Me.LayoutControl1.Text = "LayoutControl1"
        '
        'txtQTY
        '
        Me.txtQTY.EditValue = "0"
        Me.txtQTY.Location = New System.Drawing.Point(117, 12)
        Me.txtQTY.Name = "txtQTY"
        Me.txtQTY.Properties.Appearance.Options.UseTextOptions = True
        Me.txtQTY.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.txtQTY.Properties.Mask.EditMask = "n0"
        Me.txtQTY.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        Me.txtQTY.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.txtQTY.Size = New System.Drawing.Size(313, 20)
        Me.txtQTY.StyleController = Me.LayoutControl1
        Me.txtQTY.TabIndex = 52
        '
        'grdKDCARAPAKAI
        '
        Me.grdKDCARAPAKAI.Location = New System.Drawing.Point(117, 60)
        Me.grdKDCARAPAKAI.Name = "grdKDCARAPAKAI"
        Me.grdKDCARAPAKAI.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.grdKDCARAPAKAI.Properties.NullText = ""
        Me.grdKDCARAPAKAI.Properties.PopupFormMinSize = New System.Drawing.Size(600, 300)
        Me.grdKDCARAPAKAI.Properties.View = Me.GridView5
        Me.grdKDCARAPAKAI.Size = New System.Drawing.Size(313, 20)
        Me.grdKDCARAPAKAI.StyleController = Me.LayoutControl1
        Me.grdKDCARAPAKAI.TabIndex = 48
        '
        'GridView5
        '
        Me.GridView5.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn4})
        Me.GridView5.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.GridView5.Name = "GridView5"
        Me.GridView5.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.GridView5.OptionsView.ShowAutoFilterRow = True
        Me.GridView5.OptionsView.ShowGroupPanel = False
        '
        'btnBatal
        '
        Me.btnBatal.Appearance.Font = New System.Drawing.Font("Tahoma", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnBatal.Appearance.Options.UseFont = True
        Me.btnBatal.Location = New System.Drawing.Point(242, 350)
        Me.btnBatal.Name = "btnBatal"
        Me.btnBatal.Size = New System.Drawing.Size(188, 30)
        Me.btnBatal.StyleController = Me.LayoutControl1
        Me.btnBatal.TabIndex = 1
        Me.btnBatal.Text = "F12 - BATAL"
        '
        'txtREMARKS_DOKTER
        '
        Me.txtREMARKS_DOKTER.Location = New System.Drawing.Point(117, 84)
        Me.txtREMARKS_DOKTER.Name = "txtREMARKS_DOKTER"
        Me.txtREMARKS_DOKTER.Size = New System.Drawing.Size(313, 262)
        Me.txtREMARKS_DOKTER.StyleController = Me.LayoutControl1
        Me.txtREMARKS_DOKTER.TabIndex = 50
        '
        'btnHapus
        '
        Me.btnHapus.Appearance.Font = New System.Drawing.Font("Tahoma", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnHapus.Appearance.Options.UseFont = True
        Me.btnHapus.Location = New System.Drawing.Point(12, 350)
        Me.btnHapus.Name = "btnHapus"
        Me.btnHapus.Size = New System.Drawing.Size(226, 30)
        Me.btnHapus.StyleController = Me.LayoutControl1
        Me.btnHapus.TabIndex = 0
        Me.btnHapus.Text = "F3 - PILIH"
        '
        'LayoutControlGroup1
        '
        Me.LayoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.LayoutControlGroup1.GroupBordersVisible = False
        Me.LayoutControlGroup1.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlItem1, Me.LayoutControlItem2, Me.LayoutControlItem3, Me.LayoutControlItem4, Me.LayoutControlItem5, Me.LayoutControlItem6})
        Me.LayoutControlGroup1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlGroup1.Name = "LayoutControlGroup1"
        Me.LayoutControlGroup1.Size = New System.Drawing.Size(442, 392)
        Me.LayoutControlGroup1.TextVisible = False
        '
        'LayoutControlItem1
        '
        Me.LayoutControlItem1.AppearanceItemCaption.Options.UseTextOptions = True
        Me.LayoutControlItem1.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.LayoutControlItem1.Control = Me.txtQTY
        Me.LayoutControlItem1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlItem1.Name = "LayoutControlItem1"
        Me.LayoutControlItem1.Size = New System.Drawing.Size(422, 24)
        Me.LayoutControlItem1.Text = "Jumlah :"
        Me.LayoutControlItem1.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.LayoutControlItem1.TextSize = New System.Drawing.Size(100, 13)
        Me.LayoutControlItem1.TextToControlDistance = 5
        '
        'LayoutControlItem2
        '
        Me.LayoutControlItem2.AppearanceItemCaption.Options.UseTextOptions = True
        Me.LayoutControlItem2.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.LayoutControlItem2.Control = Me.grdKDSIGNA
        Me.LayoutControlItem2.Location = New System.Drawing.Point(0, 24)
        Me.LayoutControlItem2.Name = "LayoutControlItem2"
        Me.LayoutControlItem2.Size = New System.Drawing.Size(422, 24)
        Me.LayoutControlItem2.Text = "Signa :"
        Me.LayoutControlItem2.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.LayoutControlItem2.TextSize = New System.Drawing.Size(100, 13)
        Me.LayoutControlItem2.TextToControlDistance = 5
        '
        'LayoutControlItem3
        '
        Me.LayoutControlItem3.AppearanceItemCaption.Options.UseTextOptions = True
        Me.LayoutControlItem3.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.LayoutControlItem3.Control = Me.grdKDCARAPAKAI
        Me.LayoutControlItem3.Location = New System.Drawing.Point(0, 48)
        Me.LayoutControlItem3.Name = "LayoutControlItem3"
        Me.LayoutControlItem3.Size = New System.Drawing.Size(422, 24)
        Me.LayoutControlItem3.Text = "Cara Pakai:"
        Me.LayoutControlItem3.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.LayoutControlItem3.TextSize = New System.Drawing.Size(100, 13)
        Me.LayoutControlItem3.TextToControlDistance = 5
        '
        'LayoutControlItem4
        '
        Me.LayoutControlItem4.AppearanceItemCaption.Options.UseTextOptions = True
        Me.LayoutControlItem4.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.LayoutControlItem4.Control = Me.txtREMARKS_DOKTER
        Me.LayoutControlItem4.Location = New System.Drawing.Point(0, 72)
        Me.LayoutControlItem4.Name = "LayoutControlItem4"
        Me.LayoutControlItem4.Size = New System.Drawing.Size(422, 266)
        Me.LayoutControlItem4.Text = "Catatan :"
        Me.LayoutControlItem4.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.LayoutControlItem4.TextSize = New System.Drawing.Size(100, 13)
        Me.LayoutControlItem4.TextToControlDistance = 5
        Me.LayoutControlItem4.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        '
        'LayoutControlItem5
        '
        Me.LayoutControlItem5.Control = Me.btnHapus
        Me.LayoutControlItem5.Location = New System.Drawing.Point(0, 338)
        Me.LayoutControlItem5.Name = "LayoutControlItem5"
        Me.LayoutControlItem5.Size = New System.Drawing.Size(230, 34)
        Me.LayoutControlItem5.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem5.TextVisible = False
        '
        'LayoutControlItem6
        '
        Me.LayoutControlItem6.Control = Me.btnBatal
        Me.LayoutControlItem6.Location = New System.Drawing.Point(230, 338)
        Me.LayoutControlItem6.Name = "LayoutControlItem6"
        Me.LayoutControlItem6.Size = New System.Drawing.Size(192, 34)
        Me.LayoutControlItem6.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem6.TextVisible = False
        '
        'GridColumn4
        '
        Me.GridColumn4.Caption = "Name Display"
        Me.GridColumn4.FieldName = "DESCRIPTION"
        Me.GridColumn4.Name = "GridColumn4"
        Me.GridColumn4.Visible = True
        Me.GridColumn4.VisibleIndex = 0
        '
        'GridColumn3
        '
        Me.GridColumn3.Caption = "Name Display"
        Me.GridColumn3.FieldName = "DESCRIPTION"
        Me.GridColumn3.Name = "GridColumn3"
        Me.GridColumn3.Visible = True
        Me.GridColumn3.VisibleIndex = 0
        '
        'frmObatFarmasi
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(442, 392)
        Me.Controls.Add(Me.LayoutControl1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmObatFarmasi"
        Me.ShowIcon = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        CType(Me.grdKDSIGNA.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.LayoutControl1.ResumeLayout(False)
        CType(Me.txtQTY.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdKDCARAPAKAI.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtREMARKS_DOKTER.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem6, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents grdKDSIGNA As DevExpress.XtraEditors.SearchLookUpEdit
    Friend WithEvents GridView3 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridColumn3 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents LayoutControl1 As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents txtQTY As DevExpress.XtraEditors.TextEdit
    Friend WithEvents grdKDCARAPAKAI As DevExpress.XtraEditors.SearchLookUpEdit
    Friend WithEvents GridView5 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridColumn4 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents btnBatal As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents txtREMARKS_DOKTER As DevExpress.XtraEditors.MemoEdit
    Friend WithEvents btnHapus As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents LayoutControlGroup1 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents LayoutControlItem1 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem2 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem3 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem4 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem5 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem6 As DevExpress.XtraLayout.LayoutControlItem
End Class
