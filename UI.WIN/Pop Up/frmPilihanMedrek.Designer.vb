<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmPilihanMedrek
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
        Me.PanelControl1 = New DevExpress.XtraEditors.PanelControl()
        Me.chkRad = New DevExpress.XtraEditors.CheckEdit()
        Me.chkLab = New DevExpress.XtraEditors.CheckEdit()
        Me.LabelControl1 = New DevExpress.XtraEditors.LabelControl()
        Me.chkTINDAKAN = New DevExpress.XtraEditors.CheckEdit()
        Me.SimpleButton2 = New DevExpress.XtraEditors.SimpleButton()
        Me.SimpleButton1 = New DevExpress.XtraEditors.SimpleButton()
        Me.chkRencanaKontol = New DevExpress.XtraEditors.CheckEdit()
        Me.chkResep = New DevExpress.XtraEditors.CheckEdit()
        Me.chkDiagnosa = New DevExpress.XtraEditors.CheckEdit()
        CType(Me.PanelControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelControl1.SuspendLayout()
        CType(Me.chkRad.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkLab.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkTINDAKAN.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkRencanaKontol.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkResep.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkDiagnosa.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'PanelControl1
        '
        Me.PanelControl1.Controls.Add(Me.chkRad)
        Me.PanelControl1.Controls.Add(Me.chkLab)
        Me.PanelControl1.Controls.Add(Me.LabelControl1)
        Me.PanelControl1.Controls.Add(Me.chkTINDAKAN)
        Me.PanelControl1.Controls.Add(Me.SimpleButton2)
        Me.PanelControl1.Controls.Add(Me.SimpleButton1)
        Me.PanelControl1.Controls.Add(Me.chkRencanaKontol)
        Me.PanelControl1.Controls.Add(Me.chkResep)
        Me.PanelControl1.Controls.Add(Me.chkDiagnosa)
        Me.PanelControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PanelControl1.Location = New System.Drawing.Point(0, 0)
        Me.PanelControl1.Name = "PanelControl1"
        Me.PanelControl1.Size = New System.Drawing.Size(251, 223)
        Me.PanelControl1.TabIndex = 0
        '
        'chkRad
        '
        Me.chkRad.Location = New System.Drawing.Point(12, 106)
        Me.chkRad.Name = "chkRad"
        Me.chkRad.Properties.Caption = "Formulir Radiologi"
        Me.chkRad.Properties.ReadOnly = True
        Me.chkRad.Size = New System.Drawing.Size(225, 19)
        Me.chkRad.TabIndex = 5
        '
        'chkLab
        '
        Me.chkLab.Location = New System.Drawing.Point(12, 81)
        Me.chkLab.Name = "chkLab"
        Me.chkLab.Properties.Caption = "Formulir Laboratorium"
        Me.chkLab.Properties.ReadOnly = True
        Me.chkLab.Size = New System.Drawing.Size(225, 19)
        Me.chkLab.TabIndex = 4
        '
        'LabelControl1
        '
        Me.LabelControl1.Location = New System.Drawing.Point(12, 12)
        Me.LabelControl1.Name = "LabelControl1"
        Me.LabelControl1.Size = New System.Drawing.Size(108, 13)
        Me.LabelControl1.TabIndex = 7
        Me.LabelControl1.Text = "Anda Telah Membuat :"
        '
        'chkTINDAKAN
        '
        Me.chkTINDAKAN.Location = New System.Drawing.Point(12, 56)
        Me.chkTINDAKAN.Name = "chkTINDAKAN"
        Me.chkTINDAKAN.Properties.Caption = "Billing Tambahan"
        Me.chkTINDAKAN.Properties.ReadOnly = True
        Me.chkTINDAKAN.Size = New System.Drawing.Size(225, 19)
        Me.chkTINDAKAN.TabIndex = 3
        '
        'SimpleButton2
        '
        Me.SimpleButton2.Appearance.Font = New System.Drawing.Font("Tahoma", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SimpleButton2.Appearance.Options.UseFont = True
        Me.SimpleButton2.Location = New System.Drawing.Point(129, 181)
        Me.SimpleButton2.Name = "SimpleButton2"
        Me.SimpleButton2.Size = New System.Drawing.Size(108, 28)
        Me.SimpleButton2.TabIndex = 1
        Me.SimpleButton2.Text = "BATAL"
        '
        'SimpleButton1
        '
        Me.SimpleButton1.Appearance.Font = New System.Drawing.Font("Tahoma", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SimpleButton1.Appearance.Options.UseFont = True
        Me.SimpleButton1.Location = New System.Drawing.Point(15, 181)
        Me.SimpleButton1.Name = "SimpleButton1"
        Me.SimpleButton1.Size = New System.Drawing.Size(108, 28)
        Me.SimpleButton1.TabIndex = 0
        Me.SimpleButton1.Text = "SIMPAN"
        '
        'chkRencanaKontol
        '
        Me.chkRencanaKontol.Location = New System.Drawing.Point(12, 131)
        Me.chkRencanaKontol.Name = "chkRencanaKontol"
        Me.chkRencanaKontol.Properties.Caption = "Surat Rencana Kontrol"
        Me.chkRencanaKontol.Properties.ReadOnly = True
        Me.chkRencanaKontol.Size = New System.Drawing.Size(225, 19)
        Me.chkRencanaKontol.TabIndex = 6
        '
        'chkResep
        '
        Me.chkResep.Location = New System.Drawing.Point(12, 31)
        Me.chkResep.Name = "chkResep"
        Me.chkResep.Properties.Caption = "Resep"
        Me.chkResep.Properties.ReadOnly = True
        Me.chkResep.Size = New System.Drawing.Size(225, 19)
        Me.chkResep.TabIndex = 2
        '
        'chkDiagnosa
        '
        Me.chkDiagnosa.Location = New System.Drawing.Point(12, 156)
        Me.chkDiagnosa.Name = "chkDiagnosa"
        Me.chkDiagnosa.Properties.Caption = "Resume"
        Me.chkDiagnosa.Properties.ReadOnly = True
        Me.chkDiagnosa.Size = New System.Drawing.Size(225, 19)
        Me.chkDiagnosa.TabIndex = 7
        '
        'frmPilihanMedrek
        '
        Me.Appearance.BackColor = System.Drawing.Color.White
        Me.Appearance.Options.UseBackColor = True
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(251, 223)
        Me.Controls.Add(Me.PanelControl1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "frmPilihanMedrek"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "frmPilihanMedrek"
        CType(Me.PanelControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelControl1.ResumeLayout(False)
        Me.PanelControl1.PerformLayout()
        CType(Me.chkRad.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkLab.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkTINDAKAN.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkRencanaKontol.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkResep.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkDiagnosa.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents PanelControl1 As DevExpress.XtraEditors.PanelControl
    Friend WithEvents chkRencanaKontol As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents chkResep As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents chkDiagnosa As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents SimpleButton2 As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents SimpleButton1 As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents chkTINDAKAN As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents chkRad As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents chkLab As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents LabelControl1 As DevExpress.XtraEditors.LabelControl
End Class
