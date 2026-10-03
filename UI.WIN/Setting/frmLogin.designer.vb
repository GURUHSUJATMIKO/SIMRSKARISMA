<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmLogin
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmLogin))
        Me.txtUsername = New DevExpress.XtraEditors.TextEdit()
        Me.txtPassword = New DevExpress.XtraEditors.TextEdit()
        Me.lblUsername = New DevExpress.XtraEditors.LabelControl()
        Me.lblPassword = New DevExpress.XtraEditors.LabelControl()
        Me.btnLogin = New DevExpress.XtraEditors.SimpleButton()
        Me.btnClose = New DevExpress.XtraEditors.SimpleButton()
        Me.PictureEdit1 = New DevExpress.XtraEditors.PictureEdit()
        CType(Me.txtUsername.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtPassword.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureEdit1.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'txtUsername
        '
        Me.txtUsername.EnterMoveNextControl = True
        Me.txtUsername.Location = New System.Drawing.Point(206, 15)
        Me.txtUsername.Name = "txtUsername"
        Me.txtUsername.Properties.Appearance.Font = New System.Drawing.Font("Tahoma", 12.0!)
        Me.txtUsername.Properties.Appearance.Options.UseFont = True
        Me.txtUsername.Size = New System.Drawing.Size(156, 26)
        Me.txtUsername.TabIndex = 0
        '
        'txtPassword
        '
        Me.txtPassword.EnterMoveNextControl = True
        Me.txtPassword.Location = New System.Drawing.Point(206, 47)
        Me.txtPassword.Name = "txtPassword"
        Me.txtPassword.Properties.Appearance.Font = New System.Drawing.Font("Tahoma", 12.0!)
        Me.txtPassword.Properties.Appearance.Options.UseFont = True
        Me.txtPassword.Properties.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.txtPassword.Size = New System.Drawing.Size(156, 26)
        Me.txtPassword.TabIndex = 1
        '
        'lblUsername
        '
        Me.lblUsername.Appearance.Font = New System.Drawing.Font("Tahoma", 12.0!)
        Me.lblUsername.Location = New System.Drawing.Point(118, 19)
        Me.lblUsername.Name = "lblUsername"
        Me.lblUsername.Size = New System.Drawing.Size(82, 19)
        Me.lblUsername.TabIndex = 1
        Me.lblUsername.Text = "Username :"
        '
        'lblPassword
        '
        Me.lblPassword.Appearance.Font = New System.Drawing.Font("Tahoma", 12.0!)
        Me.lblPassword.Location = New System.Drawing.Point(122, 50)
        Me.lblPassword.Name = "lblPassword"
        Me.lblPassword.Size = New System.Drawing.Size(78, 19)
        Me.lblPassword.TabIndex = 1
        Me.lblPassword.Text = "Password :"
        '
        'btnLogin
        '
        Me.btnLogin.Appearance.Font = New System.Drawing.Font("Tahoma", 12.0!)
        Me.btnLogin.Appearance.Options.UseFont = True
        Me.btnLogin.Location = New System.Drawing.Point(206, 88)
        Me.btnLogin.Name = "btnLogin"
        Me.btnLogin.Size = New System.Drawing.Size(75, 35)
        Me.btnLogin.TabIndex = 2
        Me.btnLogin.Text = "Login"
        '
        'btnClose
        '
        Me.btnClose.Appearance.Font = New System.Drawing.Font("Tahoma", 12.0!)
        Me.btnClose.Appearance.Options.UseFont = True
        Me.btnClose.Location = New System.Drawing.Point(287, 88)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(75, 35)
        Me.btnClose.TabIndex = 3
        Me.btnClose.Text = "Close"
        '
        'PictureEdit1
        '
        Me.PictureEdit1.EditValue = CType(resources.GetObject("PictureEdit1.EditValue"), Object)
        Me.PictureEdit1.Location = New System.Drawing.Point(12, 13)
        Me.PictureEdit1.Name = "PictureEdit1"
        Me.PictureEdit1.Properties.Appearance.BackColor = System.Drawing.Color.Silver
        Me.PictureEdit1.Properties.Appearance.Options.UseBackColor = True
        Me.PictureEdit1.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.PictureEdit1.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Zoom
        Me.PictureEdit1.Size = New System.Drawing.Size(100, 96)
        Me.PictureEdit1.TabIndex = 4
        '
        'frmLogin
        '
        Me.Appearance.BackColor = System.Drawing.Color.Silver
        Me.Appearance.Options.UseBackColor = True
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(379, 136)
        Me.Controls.Add(Me.PictureEdit1)
        Me.Controls.Add(Me.btnClose)
        Me.Controls.Add(Me.btnLogin)
        Me.Controls.Add(Me.lblPassword)
        Me.Controls.Add(Me.lblUsername)
        Me.Controls.Add(Me.txtPassword)
        Me.Controls.Add(Me.txtUsername)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "frmLogin"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Login"
        Me.TopMost = True
        CType(Me.txtUsername.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtPassword.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureEdit1.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents txtUsername As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txtPassword As DevExpress.XtraEditors.TextEdit
    Friend WithEvents lblUsername As DevExpress.XtraEditors.LabelControl
    Friend WithEvents lblPassword As DevExpress.XtraEditors.LabelControl
    Friend WithEvents btnLogin As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnClose As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents PictureEdit1 As DevExpress.XtraEditors.PictureEdit
End Class
