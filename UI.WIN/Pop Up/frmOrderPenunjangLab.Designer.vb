<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmOrderPenunjangLab
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
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmOrderPenunjangLab))
        Dim SerializableAppearanceObject1 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Me.PanelControl1 = New DevExpress.XtraEditors.PanelControl()
        Me.btnBatal = New DevExpress.XtraEditors.SimpleButton()
        Me.btnOrder = New DevExpress.XtraEditors.SimpleButton()
        Me.grdDetail = New DevExpress.XtraGrid.GridControl()
        Me.mnuStrip = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.DeleteToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.BindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.grvDetail = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.colDATE = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepositoryItemDateEdit1 = New DevExpress.XtraEditors.Repository.RepositoryItemDateEdit()
        Me.colNAMAORDER = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn1 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.btnCeklisOrder = New DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit()
        Me.colDOKTER = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.grdKDDOCTOR = New DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit()
        Me.grvKDDOCTOR = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn2 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.colSEQ = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.PanelControl2 = New DevExpress.XtraEditors.PanelControl()
        Me.txtDIAGNOSA = New DevExpress.XtraEditors.MemoEdit()
        Me.txtTB = New DevExpress.XtraEditors.TextEdit()
        Me.txtBB = New DevExpress.XtraEditors.TextEdit()
        Me.LabelControl5 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl4 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl3 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl2 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl1 = New DevExpress.XtraEditors.LabelControl()
        Me.lblIndikasiMedis = New DevExpress.XtraEditors.LabelControl()
        Me.txtINDIKASIMEDIS = New DevExpress.XtraEditors.MemoEdit()
        Me.PanelControl3 = New DevExpress.XtraEditors.PanelControl()
        CType(Me.PanelControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelControl1.SuspendLayout()
        CType(Me.grdDetail, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.mnuStrip.SuspendLayout()
        CType(Me.BindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grvDetail, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemDateEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemDateEdit1.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnCeklisOrder, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdKDDOCTOR, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grvKDDOCTOR, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PanelControl2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelControl2.SuspendLayout()
        CType(Me.txtDIAGNOSA.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtTB.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtBB.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtINDIKASIMEDIS.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PanelControl3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelControl3.SuspendLayout()
        Me.SuspendLayout()
        '
        'PanelControl1
        '
        Me.PanelControl1.Controls.Add(Me.btnBatal)
        Me.PanelControl1.Controls.Add(Me.btnOrder)
        Me.PanelControl1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.PanelControl1.Location = New System.Drawing.Point(0, 489)
        Me.PanelControl1.Name = "PanelControl1"
        Me.PanelControl1.Size = New System.Drawing.Size(584, 72)
        Me.PanelControl1.TabIndex = 20
        '
        'btnBatal
        '
        Me.btnBatal.Appearance.Options.UseTextOptions = True
        Me.btnBatal.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
        Me.btnBatal.Location = New System.Drawing.Point(158, 9)
        Me.btnBatal.Name = "btnBatal"
        Me.btnBatal.Size = New System.Drawing.Size(139, 51)
        Me.btnBatal.TabIndex = 1
        Me.btnBatal.Text = "Batal"
        '
        'btnOrder
        '
        Me.btnOrder.Appearance.Options.UseTextOptions = True
        Me.btnOrder.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
        Me.btnOrder.Location = New System.Drawing.Point(13, 9)
        Me.btnOrder.Name = "btnOrder"
        Me.btnOrder.Size = New System.Drawing.Size(139, 51)
        Me.btnOrder.TabIndex = 0
        Me.btnOrder.Text = "Order Laboratorium"
        '
        'grdDetail
        '
        Me.grdDetail.ContextMenuStrip = Me.mnuStrip
        Me.grdDetail.DataSource = Me.BindingSource
        Me.grdDetail.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grdDetail.Location = New System.Drawing.Point(2, 2)
        Me.grdDetail.MainView = Me.grvDetail
        Me.grdDetail.Name = "grdDetail"
        Me.grdDetail.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.btnCeklisOrder, Me.grdKDDOCTOR, Me.RepositoryItemDateEdit1})
        Me.grdDetail.Size = New System.Drawing.Size(580, 267)
        Me.grdDetail.TabIndex = 21
        Me.grdDetail.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.grvDetail})
        '
        'mnuStrip
        '
        Me.mnuStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.DeleteToolStripMenuItem})
        Me.mnuStrip.Name = "mnuStrip"
        Me.mnuStrip.Size = New System.Drawing.Size(108, 26)
        '
        'DeleteToolStripMenuItem
        '
        Me.DeleteToolStripMenuItem.Name = "DeleteToolStripMenuItem"
        Me.DeleteToolStripMenuItem.Size = New System.Drawing.Size(107, 22)
        Me.DeleteToolStripMenuItem.Text = "Delete"
        '
        'BindingSource
        '
        Me.BindingSource.DataSource = GetType(DataAccess.S_REQ_ORDER_PENUNJANG)
        '
        'grvDetail
        '
        Me.grvDetail.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.colDATE, Me.colNAMAORDER, Me.GridColumn1, Me.colDOKTER, Me.colSEQ})
        Me.grvDetail.GridControl = Me.grdDetail
        Me.grvDetail.Name = "grvDetail"
        Me.grvDetail.OptionsCustomization.AllowColumnMoving = False
        Me.grvDetail.OptionsCustomization.AllowFilter = False
        Me.grvDetail.OptionsCustomization.AllowGroup = False
        Me.grvDetail.OptionsCustomization.AllowQuickHideColumns = False
        Me.grvDetail.OptionsCustomization.AllowSort = False
        Me.grvDetail.OptionsDetail.EnableMasterViewMode = False
        Me.grvDetail.OptionsFind.AllowFindPanel = False
        Me.grvDetail.OptionsLayout.StoreAllOptions = True
        Me.grvDetail.OptionsLayout.StoreAppearance = True
        Me.grvDetail.OptionsMenu.EnableColumnMenu = False
        Me.grvDetail.OptionsNavigation.AutoFocusNewRow = True
        Me.grvDetail.OptionsNavigation.EnterMoveNextColumn = True
        Me.grvDetail.OptionsView.EnableAppearanceEvenRow = True
        Me.grvDetail.OptionsView.EnableAppearanceOddRow = True
        Me.grvDetail.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Bottom
        Me.grvDetail.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never
        Me.grvDetail.OptionsView.ShowFooter = True
        Me.grvDetail.OptionsView.ShowGroupPanel = False
        '
        'colDATE
        '
        Me.colDATE.Caption = "Tanggal Pemeriksaan"
        Me.colDATE.ColumnEdit = Me.RepositoryItemDateEdit1
        Me.colDATE.DisplayFormat.FormatString = "dd-MM-yyyy"
        Me.colDATE.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.colDATE.FieldName = "DATE"
        Me.colDATE.Name = "colDATE"
        Me.colDATE.Visible = True
        Me.colDATE.VisibleIndex = 0
        Me.colDATE.Width = 120
        '
        'RepositoryItemDateEdit1
        '
        Me.RepositoryItemDateEdit1.AutoHeight = False
        Me.RepositoryItemDateEdit1.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepositoryItemDateEdit1.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepositoryItemDateEdit1.DisplayFormat.FormatString = "dd-MM-yyyy"
        Me.RepositoryItemDateEdit1.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.RepositoryItemDateEdit1.EditFormat.FormatString = "dd-MM-yyyy"
        Me.RepositoryItemDateEdit1.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.RepositoryItemDateEdit1.Name = "RepositoryItemDateEdit1"
        '
        'colNAMAORDER
        '
        Me.colNAMAORDER.Caption = "Order By Text"
        Me.colNAMAORDER.FieldName = "NAMAORDER"
        Me.colNAMAORDER.Name = "colNAMAORDER"
        Me.colNAMAORDER.Visible = True
        Me.colNAMAORDER.VisibleIndex = 1
        Me.colNAMAORDER.Width = 222
        '
        'GridColumn1
        '
        Me.GridColumn1.Caption = "Order By Ceklis"
        Me.GridColumn1.ColumnEdit = Me.btnCeklisOrder
        Me.GridColumn1.Name = "GridColumn1"
        Me.GridColumn1.Visible = True
        Me.GridColumn1.VisibleIndex = 2
        Me.GridColumn1.Width = 115
        '
        'btnCeklisOrder
        '
        Me.btnCeklisOrder.AutoHeight = False
        Me.btnCeklisOrder.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", -1, True, True, False, DevExpress.XtraEditors.ImageLocation.MiddleCenter, CType(resources.GetObject("btnCeklisOrder.Buttons"), System.Drawing.Image), New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject1, "", Nothing, Nothing, True)})
        Me.btnCeklisOrder.Name = "btnCeklisOrder"
        Me.btnCeklisOrder.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor
        '
        'colDOKTER
        '
        Me.colDOKTER.Caption = "Dokter"
        Me.colDOKTER.ColumnEdit = Me.grdKDDOCTOR
        Me.colDOKTER.FieldName = "DOKTER"
        Me.colDOKTER.Name = "colDOKTER"
        Me.colDOKTER.Visible = True
        Me.colDOKTER.VisibleIndex = 3
        Me.colDOKTER.Width = 105
        '
        'grdKDDOCTOR
        '
        Me.grdKDDOCTOR.AutoHeight = False
        Me.grdKDDOCTOR.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.grdKDDOCTOR.Name = "grdKDDOCTOR"
        Me.grdKDDOCTOR.NullText = ""
        Me.grdKDDOCTOR.View = Me.grvKDDOCTOR
        '
        'grvKDDOCTOR
        '
        Me.grvKDDOCTOR.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn2})
        Me.grvKDDOCTOR.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.grvKDDOCTOR.Name = "grvKDDOCTOR"
        Me.grvKDDOCTOR.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.grvKDDOCTOR.OptionsView.ShowGroupPanel = False
        '
        'GridColumn2
        '
        Me.GridColumn2.Caption = "Nama"
        Me.GridColumn2.FieldName = "NAME_DISPLAY"
        Me.GridColumn2.Name = "GridColumn2"
        Me.GridColumn2.Visible = True
        Me.GridColumn2.VisibleIndex = 0
        '
        'colSEQ
        '
        Me.colSEQ.Caption = "Seq"
        Me.colSEQ.FieldName = "SEQ"
        Me.colSEQ.Name = "colSEQ"
        '
        'PanelControl2
        '
        Me.PanelControl2.Controls.Add(Me.txtDIAGNOSA)
        Me.PanelControl2.Controls.Add(Me.txtTB)
        Me.PanelControl2.Controls.Add(Me.txtBB)
        Me.PanelControl2.Controls.Add(Me.LabelControl5)
        Me.PanelControl2.Controls.Add(Me.LabelControl4)
        Me.PanelControl2.Controls.Add(Me.LabelControl3)
        Me.PanelControl2.Controls.Add(Me.LabelControl2)
        Me.PanelControl2.Controls.Add(Me.LabelControl1)
        Me.PanelControl2.Controls.Add(Me.lblIndikasiMedis)
        Me.PanelControl2.Controls.Add(Me.txtINDIKASIMEDIS)
        Me.PanelControl2.Dock = System.Windows.Forms.DockStyle.Top
        Me.PanelControl2.Location = New System.Drawing.Point(0, 0)
        Me.PanelControl2.Name = "PanelControl2"
        Me.PanelControl2.Size = New System.Drawing.Size(584, 218)
        Me.PanelControl2.TabIndex = 22
        '
        'txtDIAGNOSA
        '
        Me.txtDIAGNOSA.Location = New System.Drawing.Point(13, 32)
        Me.txtDIAGNOSA.Name = "txtDIAGNOSA"
        Me.txtDIAGNOSA.Size = New System.Drawing.Size(559, 46)
        Me.txtDIAGNOSA.TabIndex = 0
        '
        'txtTB
        '
        Me.txtTB.Location = New System.Drawing.Point(92, 111)
        Me.txtTB.Name = "txtTB"
        Me.txtTB.Properties.ReadOnly = True
        Me.txtTB.Size = New System.Drawing.Size(148, 20)
        Me.txtTB.TabIndex = 3
        '
        'txtBB
        '
        Me.txtBB.Location = New System.Drawing.Point(92, 84)
        Me.txtBB.Name = "txtBB"
        Me.txtBB.Properties.ReadOnly = True
        Me.txtBB.Size = New System.Drawing.Size(148, 20)
        Me.txtBB.TabIndex = 2
        '
        'LabelControl5
        '
        Me.LabelControl5.Location = New System.Drawing.Point(246, 114)
        Me.LabelControl5.Name = "LabelControl5"
        Me.LabelControl5.Size = New System.Drawing.Size(15, 13)
        Me.LabelControl5.TabIndex = 1
        Me.LabelControl5.Text = "Cm"
        '
        'LabelControl4
        '
        Me.LabelControl4.Location = New System.Drawing.Point(249, 87)
        Me.LabelControl4.Name = "LabelControl4"
        Me.LabelControl4.Size = New System.Drawing.Size(12, 13)
        Me.LabelControl4.TabIndex = 1
        Me.LabelControl4.Text = "Kg"
        '
        'LabelControl3
        '
        Me.LabelControl3.Location = New System.Drawing.Point(13, 115)
        Me.LabelControl3.Name = "LabelControl3"
        Me.LabelControl3.Size = New System.Drawing.Size(68, 13)
        Me.LabelControl3.TabIndex = 1
        Me.LabelControl3.Text = "Tinggi Badan :"
        '
        'LabelControl2
        '
        Me.LabelControl2.Location = New System.Drawing.Point(16, 87)
        Me.LabelControl2.Name = "LabelControl2"
        Me.LabelControl2.Size = New System.Drawing.Size(66, 13)
        Me.LabelControl2.TabIndex = 1
        Me.LabelControl2.Text = "Berat Badan :"
        '
        'LabelControl1
        '
        Me.LabelControl1.Location = New System.Drawing.Point(13, 12)
        Me.LabelControl1.Name = "LabelControl1"
        Me.LabelControl1.Size = New System.Drawing.Size(51, 13)
        Me.LabelControl1.TabIndex = 1
        Me.LabelControl1.Text = "Diagnosa :"
        '
        'lblIndikasiMedis
        '
        Me.lblIndikasiMedis.Location = New System.Drawing.Point(13, 141)
        Me.lblIndikasiMedis.Name = "lblIndikasiMedis"
        Me.lblIndikasiMedis.Size = New System.Drawing.Size(73, 13)
        Me.lblIndikasiMedis.TabIndex = 1
        Me.lblIndikasiMedis.Text = "Indikasi Medis :"
        '
        'txtINDIKASIMEDIS
        '
        Me.txtINDIKASIMEDIS.Location = New System.Drawing.Point(13, 160)
        Me.txtINDIKASIMEDIS.Name = "txtINDIKASIMEDIS"
        Me.txtINDIKASIMEDIS.Size = New System.Drawing.Size(559, 46)
        Me.txtINDIKASIMEDIS.TabIndex = 4
        '
        'PanelControl3
        '
        Me.PanelControl3.Controls.Add(Me.grdDetail)
        Me.PanelControl3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PanelControl3.Location = New System.Drawing.Point(0, 218)
        Me.PanelControl3.Name = "PanelControl3"
        Me.PanelControl3.Size = New System.Drawing.Size(584, 271)
        Me.PanelControl3.TabIndex = 23
        '
        'frmOrderPenunjangLab
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(584, 561)
        Me.Controls.Add(Me.PanelControl3)
        Me.Controls.Add(Me.PanelControl2)
        Me.Controls.Add(Me.PanelControl1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.MaximizeBox = False
        Me.Name = "frmOrderPenunjangLab"
        Me.ShowIcon = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "-"
        CType(Me.PanelControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelControl1.ResumeLayout(False)
        CType(Me.grdDetail, System.ComponentModel.ISupportInitialize).EndInit()
        Me.mnuStrip.ResumeLayout(False)
        CType(Me.BindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grvDetail, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemDateEdit1.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemDateEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnCeklisOrder, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdKDDOCTOR, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grvKDDOCTOR, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PanelControl2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelControl2.ResumeLayout(False)
        Me.PanelControl2.PerformLayout()
        CType(Me.txtDIAGNOSA.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtTB.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtBB.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtINDIKASIMEDIS.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PanelControl3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelControl3.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents PanelControl1 As DevExpress.XtraEditors.PanelControl
    Friend WithEvents btnBatal As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnOrder As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents grdDetail As DevExpress.XtraGrid.GridControl
    Friend WithEvents mnuStrip As ContextMenuStrip
    Friend WithEvents DeleteToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents BindingSource As BindingSource
    Friend WithEvents grvDetail As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents colNAMAORDER As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents PanelControl2 As DevExpress.XtraEditors.PanelControl
    Friend WithEvents lblIndikasiMedis As DevExpress.XtraEditors.LabelControl
    Friend WithEvents txtINDIKASIMEDIS As DevExpress.XtraEditors.MemoEdit
    Friend WithEvents PanelControl3 As DevExpress.XtraEditors.PanelControl
    Friend WithEvents colDATE As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents colDOKTER As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn1 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents btnCeklisOrder As DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit
    Friend WithEvents grdKDDOCTOR As DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit
    Friend WithEvents grvKDDOCTOR As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridColumn2 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents RepositoryItemDateEdit1 As DevExpress.XtraEditors.Repository.RepositoryItemDateEdit
    Friend WithEvents txtTB As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txtBB As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LabelControl5 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl4 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl3 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl2 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents colSEQ As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents txtDIAGNOSA As DevExpress.XtraEditors.MemoEdit
    Friend WithEvents LabelControl1 As DevExpress.XtraEditors.LabelControl
End Class
