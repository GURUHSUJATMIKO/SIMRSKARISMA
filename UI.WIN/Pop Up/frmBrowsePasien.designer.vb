<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmBrowsePasien
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
        Me.grd = New DevExpress.XtraGrid.GridControl()
        Me.grv = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.Bawah = New DevExpress.XtraEditors.PanelControl()
        Me.cboFilter = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.LabelControl1 = New DevExpress.XtraEditors.LabelControl()
        Me.txtFILTER = New DevExpress.XtraEditors.TextEdit()
        Me.btnBATAL = New DevExpress.XtraEditors.SimpleButton()
        Me.btnPILIH = New DevExpress.XtraEditors.SimpleButton()
        Me.chkISLAMA = New DevExpress.XtraEditors.CheckEdit()
        CType(Me.grd, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grv, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Bawah, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Bawah.SuspendLayout()
        CType(Me.cboFilter.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtFILTER.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkISLAMA.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'grd
        '
        Me.grd.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grd.EmbeddedNavigator.Buttons.Append.Enabled = False
        Me.grd.EmbeddedNavigator.Buttons.Append.Visible = False
        Me.grd.EmbeddedNavigator.Buttons.CancelEdit.Enabled = False
        Me.grd.EmbeddedNavigator.Buttons.CancelEdit.Visible = False
        Me.grd.EmbeddedNavigator.Buttons.Edit.Enabled = False
        Me.grd.EmbeddedNavigator.Buttons.Edit.Visible = False
        Me.grd.EmbeddedNavigator.Buttons.EndEdit.Visible = False
        Me.grd.EmbeddedNavigator.Buttons.Remove.Visible = False
        Me.grd.Location = New System.Drawing.Point(0, 0)
        Me.grd.MainView = Me.grv
        Me.grd.Name = "grd"
        Me.grd.Size = New System.Drawing.Size(1034, 530)
        Me.grd.TabIndex = 0
        Me.grd.UseEmbeddedNavigator = True
        Me.grd.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.grv})
        '
        'grv
        '
        Me.grv.Appearance.ColumnFilterButton.BackColor = System.Drawing.Color.Silver
        Me.grv.Appearance.ColumnFilterButton.BackColor2 = System.Drawing.Color.FromArgb(CType(CType(212, Byte), Integer), CType(CType(212, Byte), Integer), CType(CType(212, Byte), Integer))
        Me.grv.Appearance.ColumnFilterButton.BorderColor = System.Drawing.Color.Silver
        Me.grv.Appearance.ColumnFilterButton.ForeColor = System.Drawing.Color.Gray
        Me.grv.Appearance.ColumnFilterButton.Options.UseBackColor = True
        Me.grv.Appearance.ColumnFilterButton.Options.UseBorderColor = True
        Me.grv.Appearance.ColumnFilterButton.Options.UseForeColor = True
        Me.grv.Appearance.ColumnFilterButtonActive.BackColor = System.Drawing.Color.FromArgb(CType(CType(212, Byte), Integer), CType(CType(212, Byte), Integer), CType(CType(212, Byte), Integer))
        Me.grv.Appearance.ColumnFilterButtonActive.BackColor2 = System.Drawing.Color.FromArgb(CType(CType(223, Byte), Integer), CType(CType(223, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.grv.Appearance.ColumnFilterButtonActive.BorderColor = System.Drawing.Color.FromArgb(CType(CType(212, Byte), Integer), CType(CType(212, Byte), Integer), CType(CType(212, Byte), Integer))
        Me.grv.Appearance.ColumnFilterButtonActive.ForeColor = System.Drawing.Color.Blue
        Me.grv.Appearance.ColumnFilterButtonActive.Options.UseBackColor = True
        Me.grv.Appearance.ColumnFilterButtonActive.Options.UseBorderColor = True
        Me.grv.Appearance.ColumnFilterButtonActive.Options.UseForeColor = True
        Me.grv.Appearance.Empty.BackColor = System.Drawing.Color.FromArgb(CType(CType(243, Byte), Integer), CType(CType(243, Byte), Integer), CType(CType(243, Byte), Integer))
        Me.grv.Appearance.Empty.Options.UseBackColor = True
        Me.grv.Appearance.EvenRow.BackColor = System.Drawing.Color.FromArgb(CType(CType(223, Byte), Integer), CType(CType(223, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.grv.Appearance.EvenRow.BackColor2 = System.Drawing.Color.GhostWhite
        Me.grv.Appearance.EvenRow.Font = New System.Drawing.Font("Tahoma", 10.0!)
        Me.grv.Appearance.EvenRow.ForeColor = System.Drawing.Color.Black
        Me.grv.Appearance.EvenRow.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.ForwardDiagonal
        Me.grv.Appearance.EvenRow.Options.UseBackColor = True
        Me.grv.Appearance.EvenRow.Options.UseFont = True
        Me.grv.Appearance.EvenRow.Options.UseForeColor = True
        Me.grv.Appearance.FilterCloseButton.BackColor = System.Drawing.Color.FromArgb(CType(CType(212, Byte), Integer), CType(CType(208, Byte), Integer), CType(CType(200, Byte), Integer))
        Me.grv.Appearance.FilterCloseButton.BackColor2 = System.Drawing.Color.FromArgb(CType(CType(118, Byte), Integer), CType(CType(170, Byte), Integer), CType(CType(225, Byte), Integer))
        Me.grv.Appearance.FilterCloseButton.BorderColor = System.Drawing.Color.FromArgb(CType(CType(212, Byte), Integer), CType(CType(208, Byte), Integer), CType(CType(200, Byte), Integer))
        Me.grv.Appearance.FilterCloseButton.ForeColor = System.Drawing.Color.Black
        Me.grv.Appearance.FilterCloseButton.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.ForwardDiagonal
        Me.grv.Appearance.FilterCloseButton.Options.UseBackColor = True
        Me.grv.Appearance.FilterCloseButton.Options.UseBorderColor = True
        Me.grv.Appearance.FilterCloseButton.Options.UseForeColor = True
        Me.grv.Appearance.FilterPanel.BackColor = System.Drawing.Color.FromArgb(CType(CType(28, Byte), Integer), CType(CType(80, Byte), Integer), CType(CType(135, Byte), Integer))
        Me.grv.Appearance.FilterPanel.BackColor2 = System.Drawing.Color.FromArgb(CType(CType(212, Byte), Integer), CType(CType(208, Byte), Integer), CType(CType(200, Byte), Integer))
        Me.grv.Appearance.FilterPanel.ForeColor = System.Drawing.Color.White
        Me.grv.Appearance.FilterPanel.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.ForwardDiagonal
        Me.grv.Appearance.FilterPanel.Options.UseBackColor = True
        Me.grv.Appearance.FilterPanel.Options.UseForeColor = True
        Me.grv.Appearance.FixedLine.BackColor = System.Drawing.Color.FromArgb(CType(CType(58, Byte), Integer), CType(CType(58, Byte), Integer), CType(CType(58, Byte), Integer))
        Me.grv.Appearance.FixedLine.Options.UseBackColor = True
        Me.grv.Appearance.FocusedCell.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(225, Byte), Integer))
        Me.grv.Appearance.FocusedCell.Font = New System.Drawing.Font("Tahoma", 10.0!)
        Me.grv.Appearance.FocusedCell.ForeColor = System.Drawing.Color.Black
        Me.grv.Appearance.FocusedCell.Options.UseBackColor = True
        Me.grv.Appearance.FocusedCell.Options.UseFont = True
        Me.grv.Appearance.FocusedCell.Options.UseForeColor = True
        Me.grv.Appearance.FocusedRow.BackColor = System.Drawing.Color.Navy
        Me.grv.Appearance.FocusedRow.BackColor2 = System.Drawing.Color.FromArgb(CType(CType(50, Byte), Integer), CType(CType(50, Byte), Integer), CType(CType(178, Byte), Integer))
        Me.grv.Appearance.FocusedRow.Font = New System.Drawing.Font("Tahoma", 10.0!)
        Me.grv.Appearance.FocusedRow.ForeColor = System.Drawing.Color.White
        Me.grv.Appearance.FocusedRow.Options.UseBackColor = True
        Me.grv.Appearance.FocusedRow.Options.UseFont = True
        Me.grv.Appearance.FocusedRow.Options.UseForeColor = True
        Me.grv.Appearance.FooterPanel.BackColor = System.Drawing.Color.Silver
        Me.grv.Appearance.FooterPanel.BorderColor = System.Drawing.Color.Silver
        Me.grv.Appearance.FooterPanel.Font = New System.Drawing.Font("Tahoma", 10.0!)
        Me.grv.Appearance.FooterPanel.ForeColor = System.Drawing.Color.Black
        Me.grv.Appearance.FooterPanel.Options.UseBackColor = True
        Me.grv.Appearance.FooterPanel.Options.UseBorderColor = True
        Me.grv.Appearance.FooterPanel.Options.UseFont = True
        Me.grv.Appearance.FooterPanel.Options.UseForeColor = True
        Me.grv.Appearance.GroupButton.BackColor = System.Drawing.Color.Silver
        Me.grv.Appearance.GroupButton.BorderColor = System.Drawing.Color.Silver
        Me.grv.Appearance.GroupButton.ForeColor = System.Drawing.Color.Black
        Me.grv.Appearance.GroupButton.Options.UseBackColor = True
        Me.grv.Appearance.GroupButton.Options.UseBorderColor = True
        Me.grv.Appearance.GroupButton.Options.UseForeColor = True
        Me.grv.Appearance.GroupFooter.BackColor = System.Drawing.Color.FromArgb(CType(CType(202, Byte), Integer), CType(CType(202, Byte), Integer), CType(CType(202, Byte), Integer))
        Me.grv.Appearance.GroupFooter.BorderColor = System.Drawing.Color.FromArgb(CType(CType(202, Byte), Integer), CType(CType(202, Byte), Integer), CType(CType(202, Byte), Integer))
        Me.grv.Appearance.GroupFooter.ForeColor = System.Drawing.Color.Black
        Me.grv.Appearance.GroupFooter.Options.UseBackColor = True
        Me.grv.Appearance.GroupFooter.Options.UseBorderColor = True
        Me.grv.Appearance.GroupFooter.Options.UseForeColor = True
        Me.grv.Appearance.GroupPanel.BackColor = System.Drawing.Color.FromArgb(CType(CType(58, Byte), Integer), CType(CType(110, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.grv.Appearance.GroupPanel.BackColor2 = System.Drawing.Color.White
        Me.grv.Appearance.GroupPanel.Font = New System.Drawing.Font("Tahoma", 10.0!, System.Drawing.FontStyle.Bold)
        Me.grv.Appearance.GroupPanel.ForeColor = System.Drawing.Color.White
        Me.grv.Appearance.GroupPanel.Options.UseBackColor = True
        Me.grv.Appearance.GroupPanel.Options.UseFont = True
        Me.grv.Appearance.GroupPanel.Options.UseForeColor = True
        Me.grv.Appearance.GroupRow.BackColor = System.Drawing.Color.Gray
        Me.grv.Appearance.GroupRow.Font = New System.Drawing.Font("Tahoma", 10.0!)
        Me.grv.Appearance.GroupRow.ForeColor = System.Drawing.Color.Silver
        Me.grv.Appearance.GroupRow.Options.UseBackColor = True
        Me.grv.Appearance.GroupRow.Options.UseFont = True
        Me.grv.Appearance.GroupRow.Options.UseForeColor = True
        Me.grv.Appearance.HeaderPanel.BackColor = System.Drawing.Color.Silver
        Me.grv.Appearance.HeaderPanel.BorderColor = System.Drawing.Color.Silver
        Me.grv.Appearance.HeaderPanel.Font = New System.Drawing.Font("Tahoma", 10.0!, System.Drawing.FontStyle.Bold)
        Me.grv.Appearance.HeaderPanel.ForeColor = System.Drawing.Color.Black
        Me.grv.Appearance.HeaderPanel.Options.UseBackColor = True
        Me.grv.Appearance.HeaderPanel.Options.UseBorderColor = True
        Me.grv.Appearance.HeaderPanel.Options.UseFont = True
        Me.grv.Appearance.HeaderPanel.Options.UseForeColor = True
        Me.grv.Appearance.HideSelectionRow.BackColor = System.Drawing.Color.Gray
        Me.grv.Appearance.HideSelectionRow.Font = New System.Drawing.Font("Tahoma", 10.0!)
        Me.grv.Appearance.HideSelectionRow.ForeColor = System.Drawing.Color.FromArgb(CType(CType(212, Byte), Integer), CType(CType(208, Byte), Integer), CType(CType(200, Byte), Integer))
        Me.grv.Appearance.HideSelectionRow.Options.UseBackColor = True
        Me.grv.Appearance.HideSelectionRow.Options.UseFont = True
        Me.grv.Appearance.HideSelectionRow.Options.UseForeColor = True
        Me.grv.Appearance.HorzLine.BackColor = System.Drawing.Color.Silver
        Me.grv.Appearance.HorzLine.Options.UseBackColor = True
        Me.grv.Appearance.OddRow.BackColor = System.Drawing.Color.White
        Me.grv.Appearance.OddRow.BackColor2 = System.Drawing.Color.White
        Me.grv.Appearance.OddRow.Font = New System.Drawing.Font("Tahoma", 10.0!)
        Me.grv.Appearance.OddRow.ForeColor = System.Drawing.Color.Black
        Me.grv.Appearance.OddRow.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.BackwardDiagonal
        Me.grv.Appearance.OddRow.Options.UseBackColor = True
        Me.grv.Appearance.OddRow.Options.UseFont = True
        Me.grv.Appearance.OddRow.Options.UseForeColor = True
        Me.grv.Appearance.Preview.BackColor = System.Drawing.Color.White
        Me.grv.Appearance.Preview.Font = New System.Drawing.Font("Tahoma", 10.0!)
        Me.grv.Appearance.Preview.ForeColor = System.Drawing.Color.Navy
        Me.grv.Appearance.Preview.Options.UseBackColor = True
        Me.grv.Appearance.Preview.Options.UseFont = True
        Me.grv.Appearance.Preview.Options.UseForeColor = True
        Me.grv.Appearance.Row.BackColor = System.Drawing.Color.White
        Me.grv.Appearance.Row.Font = New System.Drawing.Font("Tahoma", 10.0!)
        Me.grv.Appearance.Row.ForeColor = System.Drawing.Color.Black
        Me.grv.Appearance.Row.Options.UseBackColor = True
        Me.grv.Appearance.Row.Options.UseFont = True
        Me.grv.Appearance.Row.Options.UseForeColor = True
        Me.grv.Appearance.RowSeparator.BackColor = System.Drawing.Color.White
        Me.grv.Appearance.RowSeparator.BackColor2 = System.Drawing.Color.FromArgb(CType(CType(243, Byte), Integer), CType(CType(243, Byte), Integer), CType(CType(243, Byte), Integer))
        Me.grv.Appearance.RowSeparator.Options.UseBackColor = True
        Me.grv.Appearance.SelectedRow.BackColor = System.Drawing.Color.FromArgb(CType(CType(10, Byte), Integer), CType(CType(10, Byte), Integer), CType(CType(138, Byte), Integer))
        Me.grv.Appearance.SelectedRow.Font = New System.Drawing.Font("Tahoma", 10.0!)
        Me.grv.Appearance.SelectedRow.ForeColor = System.Drawing.Color.White
        Me.grv.Appearance.SelectedRow.Options.UseBackColor = True
        Me.grv.Appearance.SelectedRow.Options.UseFont = True
        Me.grv.Appearance.SelectedRow.Options.UseForeColor = True
        Me.grv.Appearance.TopNewRow.Font = New System.Drawing.Font("Tahoma", 10.0!)
        Me.grv.Appearance.TopNewRow.Options.UseFont = True
        Me.grv.Appearance.VertLine.BackColor = System.Drawing.Color.Silver
        Me.grv.Appearance.VertLine.Options.UseBackColor = True
        Me.grv.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.grv.GridControl = Me.grd
        Me.grv.Name = "grv"
        Me.grv.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.[False]
        Me.grv.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.[False]
        Me.grv.OptionsBehavior.AllowIncrementalSearch = True
        Me.grv.OptionsBehavior.AutoExpandAllGroups = True
        Me.grv.OptionsBehavior.Editable = False
        Me.grv.OptionsDetail.EnableMasterViewMode = False
        Me.grv.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.grv.OptionsView.EnableAppearanceEvenRow = True
        Me.grv.OptionsView.EnableAppearanceOddRow = True
        Me.grv.OptionsView.ShowAutoFilterRow = True
        Me.grv.OptionsView.ShowChildrenInGroupPanel = True
        Me.grv.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.ShowAlways
        Me.grv.OptionsView.ShowGroupPanel = False
        '
        'Bawah
        '
        Me.Bawah.Controls.Add(Me.chkISLAMA)
        Me.Bawah.Controls.Add(Me.cboFilter)
        Me.Bawah.Controls.Add(Me.LabelControl1)
        Me.Bawah.Controls.Add(Me.txtFILTER)
        Me.Bawah.Controls.Add(Me.btnBATAL)
        Me.Bawah.Controls.Add(Me.btnPILIH)
        Me.Bawah.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Bawah.Location = New System.Drawing.Point(0, 530)
        Me.Bawah.Margin = New System.Windows.Forms.Padding(4)
        Me.Bawah.Name = "Bawah"
        Me.Bawah.Size = New System.Drawing.Size(1034, 68)
        Me.Bawah.TabIndex = 29
        '
        'cboFilter
        '
        Me.cboFilter.EditValue = "Rekam Medis"
        Me.cboFilter.Location = New System.Drawing.Point(61, 11)
        Me.cboFilter.Name = "cboFilter"
        Me.cboFilter.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cboFilter.Properties.Items.AddRange(New Object() {"Rekam Medis", "Nama Pasien", "KTP"})
        Me.cboFilter.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
        Me.cboFilter.Size = New System.Drawing.Size(162, 20)
        Me.cboFilter.TabIndex = 51
        '
        'LabelControl1
        '
        Me.LabelControl1.Location = New System.Drawing.Point(12, 14)
        Me.LabelControl1.Name = "LabelControl1"
        Me.LabelControl1.Size = New System.Drawing.Size(31, 13)
        Me.LabelControl1.TabIndex = 50
        Me.LabelControl1.Text = "Filter :"
        '
        'txtFILTER
        '
        Me.txtFILTER.Location = New System.Drawing.Point(229, 11)
        Me.txtFILTER.Name = "txtFILTER"
        Me.txtFILTER.Size = New System.Drawing.Size(385, 20)
        Me.txtFILTER.TabIndex = 49
        '
        'btnBATAL
        '
        Me.btnBATAL.Location = New System.Drawing.Point(539, 37)
        Me.btnBATAL.Name = "btnBATAL"
        Me.btnBATAL.Size = New System.Drawing.Size(75, 23)
        Me.btnBATAL.TabIndex = 46
        Me.btnBATAL.Text = "BATAL"
        '
        'btnPILIH
        '
        Me.btnPILIH.Location = New System.Drawing.Point(458, 37)
        Me.btnPILIH.Name = "btnPILIH"
        Me.btnPILIH.Size = New System.Drawing.Size(75, 23)
        Me.btnPILIH.TabIndex = 45
        Me.btnPILIH.Text = "PILIH"
        '
        'chkISLAMA
        '
        Me.chkISLAMA.Location = New System.Drawing.Point(61, 37)
        Me.chkISLAMA.Name = "chkISLAMA"
        Me.chkISLAMA.Properties.Caption = "SIMRS Lama ?"
        Me.chkISLAMA.Size = New System.Drawing.Size(103, 19)
        Me.chkISLAMA.TabIndex = 52
        '
        'frmBrowsePasien
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1034, 598)
        Me.Controls.Add(Me.grd)
        Me.Controls.Add(Me.Bawah)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.KeyPreview = True
        Me.Name = "frmBrowsePasien"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Browse Order"
        CType(Me.grd, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grv, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Bawah, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Bawah.ResumeLayout(False)
        Me.Bawah.PerformLayout()
        CType(Me.cboFilter.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtFILTER.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkISLAMA.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents grd As DevExpress.XtraGrid.GridControl
    Friend WithEvents grv As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents Bawah As DevExpress.XtraEditors.PanelControl
    Friend WithEvents btnBATAL As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnPILIH As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents LabelControl1 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents txtFILTER As DevExpress.XtraEditors.TextEdit
    Friend WithEvents cboFilter As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents chkISLAMA As DevExpress.XtraEditors.CheckEdit
End Class
