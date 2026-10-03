Imports DevExpress.Skins
Imports DevExpress.LookAndFeel
Imports DevExpress.UserSkins
Imports DevExpress.XtraEditors

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmMDISalesMedrek
    Inherits XtraForm

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
        Me.xtraTab = New DevExpress.XtraTabbedMdi.XtraTabbedMdiManager(Me.components)
        Me.BarButtonItem12 = New DevExpress.XtraBars.BarButtonItem()
        CType(Me.xtraTab, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'xtraTab
        '
        Me.xtraTab.MdiParent = Me
        '
        'BarButtonItem12
        '
        Me.BarButtonItem12.Caption = "Salesman"
        Me.BarButtonItem12.Id = 21
        Me.BarButtonItem12.Name = "BarButtonItem12"
        '
        'frmMDISalesMedrek
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(792, 573)
        Me.IsMdiContainer = True
        Me.KeyPreview = True
        Me.Name = "frmMDISalesMedrek"
        Me.ShowIcon = False
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        CType(Me.xtraTab, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents xtraTab As DevExpress.XtraTabbedMdi.XtraTabbedMdiManager
    Friend WithEvents BarButtonItem12 As DevExpress.XtraBars.BarButtonItem
End Class
