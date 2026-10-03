<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmDatabaseUser
    Inherits DevExpress.XtraEditors.XtraForm

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
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
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.LabelControl2 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl1 = New DevExpress.XtraEditors.LabelControl()
        Me.txtMainServer = New DevExpress.XtraEditors.TextEdit()
        Me.btnSave = New DevExpress.XtraEditors.SimpleButton()
        Me.btnCancel = New DevExpress.XtraEditors.SimpleButton()
        Me.txtMainDatabase = New DevExpress.XtraEditors.TextEdit()
        Me.LabelControl3 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl4 = New DevExpress.XtraEditors.LabelControl()
        Me.txtID = New DevExpress.XtraEditors.TextEdit()
        Me.txtPassword = New DevExpress.XtraEditors.TextEdit()
        CType(Me.txtMainServer.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtMainDatabase.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtID.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtPassword.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'LabelControl2
        '
        Me.LabelControl2.Location = New System.Drawing.Point(41, 45)
        Me.LabelControl2.Name = "LabelControl2"
        Me.LabelControl2.Size = New System.Drawing.Size(53, 13)
        Me.LabelControl2.TabIndex = 1
        Me.LabelControl2.Text = "Database :"
        '
        'LabelControl1
        '
        Me.LabelControl1.Location = New System.Drawing.Point(55, 19)
        Me.LabelControl1.Name = "LabelControl1"
        Me.LabelControl1.Size = New System.Drawing.Size(39, 13)
        Me.LabelControl1.TabIndex = 1
        Me.LabelControl1.Text = "Server :"
        '
        'txtMainServer
        '
        Me.txtMainServer.EnterMoveNextControl = True
        Me.txtMainServer.Location = New System.Drawing.Point(104, 16)
        Me.txtMainServer.Name = "txtMainServer"
        Me.txtMainServer.Size = New System.Drawing.Size(160, 20)
        Me.txtMainServer.TabIndex = 0
        '
        'btnSave
        '
        Me.btnSave.Location = New System.Drawing.Point(108, 129)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(75, 23)
        Me.btnSave.TabIndex = 5
        Me.btnSave.Text = "&Save"
        '
        'btnCancel
        '
        Me.btnCancel.Location = New System.Drawing.Point(189, 129)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(75, 23)
        Me.btnCancel.TabIndex = 6
        Me.btnCancel.Text = "&Cancel"
        '
        'txtMainDatabase
        '
        Me.txtMainDatabase.EditValue = "USER_DATABASE"
        Me.txtMainDatabase.EnterMoveNextControl = True
        Me.txtMainDatabase.Location = New System.Drawing.Point(104, 42)
        Me.txtMainDatabase.Name = "txtMainDatabase"
        Me.txtMainDatabase.Size = New System.Drawing.Size(160, 20)
        Me.txtMainDatabase.TabIndex = 1
        '
        'LabelControl3
        '
        Me.LabelControl3.Location = New System.Drawing.Point(41, 99)
        Me.LabelControl3.Name = "LabelControl3"
        Me.LabelControl3.Size = New System.Drawing.Size(53, 13)
        Me.LabelControl3.TabIndex = 7
        Me.LabelControl3.Text = "Password :"
        '
        'LabelControl4
        '
        Me.LabelControl4.Location = New System.Drawing.Point(76, 73)
        Me.LabelControl4.Name = "LabelControl4"
        Me.LabelControl4.Size = New System.Drawing.Size(18, 13)
        Me.LabelControl4.TabIndex = 8
        Me.LabelControl4.Text = "ID :"
        '
        'txtID
        '
        Me.txtID.EditValue = "sa"
        Me.txtID.EnterMoveNextControl = True
        Me.txtID.Location = New System.Drawing.Point(104, 70)
        Me.txtID.Name = "txtID"
        Me.txtID.Size = New System.Drawing.Size(160, 20)
        Me.txtID.TabIndex = 3
        '
        'txtPassword
        '
        Me.txtPassword.EditValue = ""
        Me.txtPassword.EnterMoveNextControl = True
        Me.txtPassword.Location = New System.Drawing.Point(104, 96)
        Me.txtPassword.Name = "txtPassword"
        Me.txtPassword.Properties.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.txtPassword.Size = New System.Drawing.Size(160, 20)
        Me.txtPassword.TabIndex = 4
        '
        'frmDatabaseUser
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(305, 166)
        Me.Controls.Add(Me.LabelControl3)
        Me.Controls.Add(Me.LabelControl4)
        Me.Controls.Add(Me.txtID)
        Me.Controls.Add(Me.txtPassword)
        Me.Controls.Add(Me.btnCancel)
        Me.Controls.Add(Me.LabelControl2)
        Me.Controls.Add(Me.btnSave)
        Me.Controls.Add(Me.LabelControl1)
        Me.Controls.Add(Me.txtMainServer)
        Me.Controls.Add(Me.txtMainDatabase)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Name = "frmDatabaseUser"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Setting - Database"
        CType(Me.txtMainServer.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtMainDatabase.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtID.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtPassword.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents txtMainServer As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LabelControl2 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl1 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents btnSave As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnCancel As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents txtMainDatabase As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LabelControl3 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl4 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents txtID As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txtPassword As DevExpress.XtraEditors.TextEdit
End Class
