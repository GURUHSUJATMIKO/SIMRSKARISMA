<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmBrowseUpload_01
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
        Me.lblRM = New DevExpress.XtraEditors.LabelControl()
        Me.LayoutControl1 = New DevExpress.XtraLayout.LayoutControl()
        Me.PdfViewer1 = New DevExpress.XtraPdfViewer.PdfViewer()
        Me.lblJenisKelamin = New DevExpress.XtraEditors.LabelControl()
        Me.lblTanggalLahir = New DevExpress.XtraEditors.LabelControl()
        Me.lblNama = New DevExpress.XtraEditors.LabelControl()
        Me.LayoutControlGroup1 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.LayoutControlItem2 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem3 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem4 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem5 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.lPDF = New DevExpress.XtraLayout.LayoutControlItem()
        Me.lblkdreg = New DevExpress.XtraEditors.LabelControl()
        Me.LayoutControlItem1 = New DevExpress.XtraLayout.LayoutControlItem()
        CType(Me.LayoutControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.LayoutControl1.SuspendLayout()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lPDF, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'lblRM
        '
        Me.lblRM.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblRM.Appearance.BackColor = System.Drawing.Color.Yellow
        Me.lblRM.Appearance.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblRM.Appearance.ForeColor = System.Drawing.Color.DarkOliveGreen
        Me.lblRM.Location = New System.Drawing.Point(2, 22)
        Me.lblRM.Name = "lblRM"
        Me.lblRM.Size = New System.Drawing.Size(89, 16)
        Me.lblRM.StyleController = Me.LayoutControl1
        Me.lblRM.TabIndex = 34
        Me.lblRM.Text = "LabelControl1"
        '
        'LayoutControl1
        '
        Me.LayoutControl1.Controls.Add(Me.lblkdreg)
        Me.LayoutControl1.Controls.Add(Me.PdfViewer1)
        Me.LayoutControl1.Controls.Add(Me.lblJenisKelamin)
        Me.LayoutControl1.Controls.Add(Me.lblRM)
        Me.LayoutControl1.Controls.Add(Me.lblTanggalLahir)
        Me.LayoutControl1.Controls.Add(Me.lblNama)
        Me.LayoutControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LayoutControl1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControl1.Name = "LayoutControl1"
        Me.LayoutControl1.Root = Me.LayoutControlGroup1
        Me.LayoutControl1.Size = New System.Drawing.Size(801, 617)
        Me.LayoutControl1.TabIndex = 38
        Me.LayoutControl1.Text = "LayoutControl1"
        '
        'PdfViewer1
        '
        Me.PdfViewer1.Location = New System.Drawing.Point(2, 102)
        Me.PdfViewer1.Name = "PdfViewer1"
        Me.PdfViewer1.Size = New System.Drawing.Size(797, 513)
        Me.PdfViewer1.TabIndex = 38
        '
        'lblJenisKelamin
        '
        Me.lblJenisKelamin.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblJenisKelamin.Appearance.BackColor = System.Drawing.Color.Yellow
        Me.lblJenisKelamin.Appearance.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblJenisKelamin.Appearance.ForeColor = System.Drawing.Color.DarkOliveGreen
        Me.lblJenisKelamin.Location = New System.Drawing.Point(2, 82)
        Me.lblJenisKelamin.Name = "lblJenisKelamin"
        Me.lblJenisKelamin.Size = New System.Drawing.Size(89, 16)
        Me.lblJenisKelamin.StyleController = Me.LayoutControl1
        Me.lblJenisKelamin.TabIndex = 37
        Me.lblJenisKelamin.Text = "LabelControl4"
        '
        'lblTanggalLahir
        '
        Me.lblTanggalLahir.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblTanggalLahir.Appearance.BackColor = System.Drawing.Color.Yellow
        Me.lblTanggalLahir.Appearance.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTanggalLahir.Appearance.ForeColor = System.Drawing.Color.DarkOliveGreen
        Me.lblTanggalLahir.Location = New System.Drawing.Point(2, 62)
        Me.lblTanggalLahir.Name = "lblTanggalLahir"
        Me.lblTanggalLahir.Size = New System.Drawing.Size(89, 16)
        Me.lblTanggalLahir.StyleController = Me.LayoutControl1
        Me.lblTanggalLahir.TabIndex = 36
        Me.lblTanggalLahir.Text = "LabelControl3"
        '
        'lblNama
        '
        Me.lblNama.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblNama.Appearance.BackColor = System.Drawing.Color.Yellow
        Me.lblNama.Appearance.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblNama.Appearance.ForeColor = System.Drawing.Color.DarkOliveGreen
        Me.lblNama.Location = New System.Drawing.Point(2, 42)
        Me.lblNama.Name = "lblNama"
        Me.lblNama.Size = New System.Drawing.Size(89, 16)
        Me.lblNama.StyleController = Me.LayoutControl1
        Me.lblNama.TabIndex = 35
        Me.lblNama.Text = "LabelControl2"
        '
        'LayoutControlGroup1
        '
        Me.LayoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.LayoutControlGroup1.GroupBordersVisible = False
        Me.LayoutControlGroup1.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlItem2, Me.LayoutControlItem3, Me.LayoutControlItem4, Me.LayoutControlItem5, Me.lPDF, Me.LayoutControlItem1})
        Me.LayoutControlGroup1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlGroup1.Name = "LayoutControlGroup1"
        Me.LayoutControlGroup1.Padding = New DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0)
        Me.LayoutControlGroup1.Size = New System.Drawing.Size(801, 617)
        Me.LayoutControlGroup1.TextVisible = False
        '
        'LayoutControlItem2
        '
        Me.LayoutControlItem2.Control = Me.lblRM
        Me.LayoutControlItem2.Location = New System.Drawing.Point(0, 20)
        Me.LayoutControlItem2.Name = "LayoutControlItem2"
        Me.LayoutControlItem2.Size = New System.Drawing.Size(801, 20)
        Me.LayoutControlItem2.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem2.TextVisible = False
        '
        'LayoutControlItem3
        '
        Me.LayoutControlItem3.Control = Me.lblNama
        Me.LayoutControlItem3.Location = New System.Drawing.Point(0, 40)
        Me.LayoutControlItem3.Name = "LayoutControlItem3"
        Me.LayoutControlItem3.Size = New System.Drawing.Size(801, 20)
        Me.LayoutControlItem3.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem3.TextVisible = False
        '
        'LayoutControlItem4
        '
        Me.LayoutControlItem4.Control = Me.lblTanggalLahir
        Me.LayoutControlItem4.Location = New System.Drawing.Point(0, 60)
        Me.LayoutControlItem4.Name = "LayoutControlItem4"
        Me.LayoutControlItem4.Size = New System.Drawing.Size(801, 20)
        Me.LayoutControlItem4.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem4.TextVisible = False
        '
        'LayoutControlItem5
        '
        Me.LayoutControlItem5.Control = Me.lblJenisKelamin
        Me.LayoutControlItem5.Location = New System.Drawing.Point(0, 80)
        Me.LayoutControlItem5.Name = "LayoutControlItem5"
        Me.LayoutControlItem5.Size = New System.Drawing.Size(801, 20)
        Me.LayoutControlItem5.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem5.TextVisible = False
        '
        'lPDF
        '
        Me.lPDF.Control = Me.PdfViewer1
        Me.lPDF.Location = New System.Drawing.Point(0, 100)
        Me.lPDF.Name = "lPDF"
        Me.lPDF.Size = New System.Drawing.Size(801, 517)
        Me.lPDF.TextSize = New System.Drawing.Size(0, 0)
        Me.lPDF.TextVisible = False
        '
        'lblkdreg
        '
        Me.lblkdreg.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblkdreg.Appearance.BackColor = System.Drawing.Color.Yellow
        Me.lblkdreg.Appearance.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblkdreg.Appearance.ForeColor = System.Drawing.Color.DarkOliveGreen
        Me.lblkdreg.Location = New System.Drawing.Point(2, 2)
        Me.lblkdreg.Name = "lblkdreg"
        Me.lblkdreg.Size = New System.Drawing.Size(89, 16)
        Me.lblkdreg.StyleController = Me.LayoutControl1
        Me.lblkdreg.TabIndex = 39
        Me.lblkdreg.Text = "LabelControl1"
        '
        'LayoutControlItem1
        '
        Me.LayoutControlItem1.Control = Me.lblkdreg
        Me.LayoutControlItem1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlItem1.Name = "LayoutControlItem1"
        Me.LayoutControlItem1.Size = New System.Drawing.Size(801, 20)
        Me.LayoutControlItem1.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem1.TextVisible = False
        '
        'frmBrowseUpload_01
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(801, 617)
        Me.Controls.Add(Me.LayoutControl1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.KeyPreview = True
        Me.Name = "frmBrowseUpload_01"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Browse"
        CType(Me.LayoutControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.LayoutControl1.ResumeLayout(False)
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lPDF, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents lblRM As DevExpress.XtraEditors.LabelControl
    Friend WithEvents lblNama As DevExpress.XtraEditors.LabelControl
    Friend WithEvents lblTanggalLahir As DevExpress.XtraEditors.LabelControl
    Friend WithEvents lblJenisKelamin As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LayoutControl1 As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents LayoutControlGroup1 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents LayoutControlItem2 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem3 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem4 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem5 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents PdfViewer1 As DevExpress.XtraPdfViewer.PdfViewer
    Friend WithEvents lPDF As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents lblkdreg As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LayoutControlItem1 As DevExpress.XtraLayout.LayoutControlItem
End Class
