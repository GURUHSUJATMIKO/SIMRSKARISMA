<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmPesanDelete
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
        Me.btnHapus = New DevExpress.XtraEditors.SimpleButton()
        Me.btnBatal = New DevExpress.XtraEditors.SimpleButton()
        Me.LabelControl1 = New DevExpress.XtraEditors.LabelControl()
        Me.txtHapus = New DevExpress.XtraEditors.MemoEdit()
        Me.LabelControl2 = New DevExpress.XtraEditors.LabelControl()
        CType(Me.txtHapus.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'btnHapus
        '
        Me.btnHapus.Appearance.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnHapus.Appearance.Options.UseFont = True
        Me.btnHapus.Location = New System.Drawing.Point(23, 136)
        Me.btnHapus.Name = "btnHapus"
        Me.btnHapus.Size = New System.Drawing.Size(195, 41)
        Me.btnHapus.TabIndex = 0
        Me.btnHapus.Text = "HAPUS"
        '
        'btnBatal
        '
        Me.btnBatal.Appearance.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnBatal.Appearance.Options.UseFont = True
        Me.btnBatal.Location = New System.Drawing.Point(224, 136)
        Me.btnBatal.Name = "btnBatal"
        Me.btnBatal.Size = New System.Drawing.Size(195, 41)
        Me.btnBatal.TabIndex = 1
        Me.btnBatal.Text = "BATAL"
        '
        'LabelControl1
        '
        Me.LabelControl1.Appearance.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelControl1.Location = New System.Drawing.Point(23, 12)
        Me.LabelControl1.Name = "LabelControl1"
        Me.LabelControl1.Size = New System.Drawing.Size(319, 19)
        Me.LabelControl1.TabIndex = 2
        Me.LabelControl1.Text = "APAKAH TRANSAKSI AKAN DIHAPUS ?"
        '
        'txtHapus
        '
        Me.txtHapus.Location = New System.Drawing.Point(23, 63)
        Me.txtHapus.Name = "txtHapus"
        Me.txtHapus.Size = New System.Drawing.Size(396, 67)
        Me.txtHapus.TabIndex = 3
        '
        'LabelControl2
        '
        Me.LabelControl2.Location = New System.Drawing.Point(23, 44)
        Me.LabelControl2.Name = "LabelControl2"
        Me.LabelControl2.Size = New System.Drawing.Size(72, 13)
        Me.LabelControl2.TabIndex = 4
        Me.LabelControl2.Text = "Alasan Hapus :"
        '
        'frmPesanDelete
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(442, 195)
        Me.Controls.Add(Me.LabelControl2)
        Me.Controls.Add(Me.txtHapus)
        Me.Controls.Add(Me.LabelControl1)
        Me.Controls.Add(Me.btnBatal)
        Me.Controls.Add(Me.btnHapus)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmPesanDelete"
        Me.ShowIcon = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        CType(Me.txtHapus.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents btnHapus As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnBatal As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents LabelControl1 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents txtHapus As DevExpress.XtraEditors.MemoEdit
    Friend WithEvents LabelControl2 As DevExpress.XtraEditors.LabelControl
End Class
