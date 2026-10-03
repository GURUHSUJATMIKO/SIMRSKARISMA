<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmChangePassword
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmChangePassword))
        Me.txtCurrent = New DevExpress.XtraEditors.TextEdit
        Me.txtNew = New DevExpress.XtraEditors.TextEdit
        Me.lblUsername = New DevExpress.XtraEditors.LabelControl
        Me.lblPassword = New DevExpress.XtraEditors.LabelControl
        Me.btnCancel = New DevExpress.XtraEditors.SimpleButton
        Me.PictureEdit1 = New DevExpress.XtraEditors.PictureEdit
        Me.txtConfirm = New DevExpress.XtraEditors.TextEdit
        Me.LabelControl1 = New DevExpress.XtraEditors.LabelControl
        Me.btnSave = New DevExpress.XtraEditors.SimpleButton
        CType(Me.txtCurrent.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtNew.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureEdit1.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtConfirm.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'txtCurrent
        '
        Me.txtCurrent.AllowHtmlTextInToolTip = DevExpress.Utils.DefaultBoolean.[Default]
        Me.txtCurrent.EnterMoveNextControl = True
        Me.txtCurrent.Location = New System.Drawing.Point(260, 16)
        Me.txtCurrent.Name = "txtCurrent"
        Me.txtCurrent.Properties.AccessibleRole = System.Windows.Forms.AccessibleRole.[Default]
        Me.txtCurrent.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.[Default]
        Me.txtCurrent.Properties.Appearance.Font = New System.Drawing.Font("Tahoma", 12.0!)
        Me.txtCurrent.Properties.Appearance.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal
        Me.txtCurrent.Properties.Appearance.Options.UseFont = True
        Me.txtCurrent.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.[Default]
        Me.txtCurrent.Properties.Appearance.TextOptions.HotkeyPrefix = DevExpress.Utils.HKeyPrefix.[Default]
        Me.txtCurrent.Properties.Appearance.TextOptions.Trimming = DevExpress.Utils.Trimming.[Default]
        Me.txtCurrent.Properties.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.[Default]
        Me.txtCurrent.Properties.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.[Default]
        Me.txtCurrent.Properties.AppearanceDisabled.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal
        Me.txtCurrent.Properties.AppearanceDisabled.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.[Default]
        Me.txtCurrent.Properties.AppearanceDisabled.TextOptions.HotkeyPrefix = DevExpress.Utils.HKeyPrefix.[Default]
        Me.txtCurrent.Properties.AppearanceDisabled.TextOptions.Trimming = DevExpress.Utils.Trimming.[Default]
        Me.txtCurrent.Properties.AppearanceDisabled.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.[Default]
        Me.txtCurrent.Properties.AppearanceDisabled.TextOptions.WordWrap = DevExpress.Utils.WordWrap.[Default]
        Me.txtCurrent.Properties.AppearanceFocused.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal
        Me.txtCurrent.Properties.AppearanceFocused.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.[Default]
        Me.txtCurrent.Properties.AppearanceFocused.TextOptions.HotkeyPrefix = DevExpress.Utils.HKeyPrefix.[Default]
        Me.txtCurrent.Properties.AppearanceFocused.TextOptions.Trimming = DevExpress.Utils.Trimming.[Default]
        Me.txtCurrent.Properties.AppearanceFocused.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.[Default]
        Me.txtCurrent.Properties.AppearanceFocused.TextOptions.WordWrap = DevExpress.Utils.WordWrap.[Default]
        Me.txtCurrent.Properties.AppearanceReadOnly.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal
        Me.txtCurrent.Properties.AppearanceReadOnly.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.[Default]
        Me.txtCurrent.Properties.AppearanceReadOnly.TextOptions.HotkeyPrefix = DevExpress.Utils.HKeyPrefix.[Default]
        Me.txtCurrent.Properties.AppearanceReadOnly.TextOptions.Trimming = DevExpress.Utils.Trimming.[Default]
        Me.txtCurrent.Properties.AppearanceReadOnly.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.[Default]
        Me.txtCurrent.Properties.AppearanceReadOnly.TextOptions.WordWrap = DevExpress.Utils.WordWrap.[Default]
        Me.txtCurrent.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.[Default]
        Me.txtCurrent.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.txtCurrent.Properties.EditValueChangedFiringMode = DevExpress.XtraEditors.Controls.EditValueChangedFiringMode.[Default]
        Me.txtCurrent.Properties.ExportMode = DevExpress.XtraEditors.Repository.ExportMode.[Default]
        Me.txtCurrent.Properties.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Skin
        Me.txtCurrent.Properties.Mask.AutoComplete = DevExpress.XtraEditors.Mask.AutoCompleteType.[Default]
        Me.txtCurrent.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.None
        Me.txtCurrent.Properties.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.txtCurrent.Size = New System.Drawing.Size(144, 26)
        Me.txtCurrent.TabIndex = 0
        Me.txtCurrent.ToolTipIconType = DevExpress.Utils.ToolTipIconType.None
        '
        'txtNew
        '
        Me.txtNew.AllowHtmlTextInToolTip = DevExpress.Utils.DefaultBoolean.[Default]
        Me.txtNew.EnterMoveNextControl = True
        Me.txtNew.Location = New System.Drawing.Point(260, 48)
        Me.txtNew.Name = "txtNew"
        Me.txtNew.Properties.AccessibleRole = System.Windows.Forms.AccessibleRole.[Default]
        Me.txtNew.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.[Default]
        Me.txtNew.Properties.Appearance.Font = New System.Drawing.Font("Tahoma", 12.0!)
        Me.txtNew.Properties.Appearance.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal
        Me.txtNew.Properties.Appearance.Options.UseFont = True
        Me.txtNew.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.[Default]
        Me.txtNew.Properties.Appearance.TextOptions.HotkeyPrefix = DevExpress.Utils.HKeyPrefix.[Default]
        Me.txtNew.Properties.Appearance.TextOptions.Trimming = DevExpress.Utils.Trimming.[Default]
        Me.txtNew.Properties.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.[Default]
        Me.txtNew.Properties.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.[Default]
        Me.txtNew.Properties.AppearanceDisabled.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal
        Me.txtNew.Properties.AppearanceDisabled.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.[Default]
        Me.txtNew.Properties.AppearanceDisabled.TextOptions.HotkeyPrefix = DevExpress.Utils.HKeyPrefix.[Default]
        Me.txtNew.Properties.AppearanceDisabled.TextOptions.Trimming = DevExpress.Utils.Trimming.[Default]
        Me.txtNew.Properties.AppearanceDisabled.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.[Default]
        Me.txtNew.Properties.AppearanceDisabled.TextOptions.WordWrap = DevExpress.Utils.WordWrap.[Default]
        Me.txtNew.Properties.AppearanceFocused.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal
        Me.txtNew.Properties.AppearanceFocused.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.[Default]
        Me.txtNew.Properties.AppearanceFocused.TextOptions.HotkeyPrefix = DevExpress.Utils.HKeyPrefix.[Default]
        Me.txtNew.Properties.AppearanceFocused.TextOptions.Trimming = DevExpress.Utils.Trimming.[Default]
        Me.txtNew.Properties.AppearanceFocused.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.[Default]
        Me.txtNew.Properties.AppearanceFocused.TextOptions.WordWrap = DevExpress.Utils.WordWrap.[Default]
        Me.txtNew.Properties.AppearanceReadOnly.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal
        Me.txtNew.Properties.AppearanceReadOnly.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.[Default]
        Me.txtNew.Properties.AppearanceReadOnly.TextOptions.HotkeyPrefix = DevExpress.Utils.HKeyPrefix.[Default]
        Me.txtNew.Properties.AppearanceReadOnly.TextOptions.Trimming = DevExpress.Utils.Trimming.[Default]
        Me.txtNew.Properties.AppearanceReadOnly.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.[Default]
        Me.txtNew.Properties.AppearanceReadOnly.TextOptions.WordWrap = DevExpress.Utils.WordWrap.[Default]
        Me.txtNew.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.[Default]
        Me.txtNew.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.txtNew.Properties.EditValueChangedFiringMode = DevExpress.XtraEditors.Controls.EditValueChangedFiringMode.[Default]
        Me.txtNew.Properties.ExportMode = DevExpress.XtraEditors.Repository.ExportMode.[Default]
        Me.txtNew.Properties.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Skin
        Me.txtNew.Properties.Mask.AutoComplete = DevExpress.XtraEditors.Mask.AutoCompleteType.[Default]
        Me.txtNew.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.None
        Me.txtNew.Properties.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.txtNew.Size = New System.Drawing.Size(144, 26)
        Me.txtNew.TabIndex = 1
        Me.txtNew.ToolTipIconType = DevExpress.Utils.ToolTipIconType.None
        '
        'lblUsername
        '
        Me.lblUsername.AllowHtmlTextInToolTip = DevExpress.Utils.DefaultBoolean.[Default]
        Me.lblUsername.Appearance.Font = New System.Drawing.Font("Tahoma", 12.0!)
        Me.lblUsername.Appearance.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal
        Me.lblUsername.Appearance.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.lblUsername.Appearance.Options.UseFont = True
        Me.lblUsername.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.[Default]
        Me.lblUsername.Appearance.TextOptions.HotkeyPrefix = DevExpress.Utils.HKeyPrefix.[Default]
        Me.lblUsername.Appearance.TextOptions.Trimming = DevExpress.Utils.Trimming.[Default]
        Me.lblUsername.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.[Default]
        Me.lblUsername.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.[Default]
        Me.lblUsername.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.[Default]
        Me.lblUsername.ImageAlignToText = DevExpress.XtraEditors.ImageAlignToText.None
        Me.lblUsername.LineLocation = DevExpress.XtraEditors.LineLocation.[Default]
        Me.lblUsername.LineOrientation = DevExpress.XtraEditors.LabelLineOrientation.[Default]
        Me.lblUsername.Location = New System.Drawing.Point(118, 19)
        Me.lblUsername.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Skin
        Me.lblUsername.Name = "lblUsername"
        Me.lblUsername.Size = New System.Drawing.Size(136, 19)
        Me.lblUsername.TabIndex = 1
        Me.lblUsername.Text = "Current Password :"
        Me.lblUsername.ToolTipIconType = DevExpress.Utils.ToolTipIconType.None
        '
        'lblPassword
        '
        Me.lblPassword.AllowHtmlTextInToolTip = DevExpress.Utils.DefaultBoolean.[Default]
        Me.lblPassword.Appearance.Font = New System.Drawing.Font("Tahoma", 12.0!)
        Me.lblPassword.Appearance.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal
        Me.lblPassword.Appearance.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.lblPassword.Appearance.Options.UseFont = True
        Me.lblPassword.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.[Default]
        Me.lblPassword.Appearance.TextOptions.HotkeyPrefix = DevExpress.Utils.HKeyPrefix.[Default]
        Me.lblPassword.Appearance.TextOptions.Trimming = DevExpress.Utils.Trimming.[Default]
        Me.lblPassword.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.[Default]
        Me.lblPassword.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.[Default]
        Me.lblPassword.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.[Default]
        Me.lblPassword.ImageAlignToText = DevExpress.XtraEditors.ImageAlignToText.None
        Me.lblPassword.LineLocation = DevExpress.XtraEditors.LineLocation.[Default]
        Me.lblPassword.LineOrientation = DevExpress.XtraEditors.LabelLineOrientation.[Default]
        Me.lblPassword.Location = New System.Drawing.Point(140, 51)
        Me.lblPassword.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Skin
        Me.lblPassword.Name = "lblPassword"
        Me.lblPassword.Size = New System.Drawing.Size(114, 19)
        Me.lblPassword.TabIndex = 1
        Me.lblPassword.Text = "New Password :"
        Me.lblPassword.ToolTipIconType = DevExpress.Utils.ToolTipIconType.None
        '
        'btnCancel
        '
        Me.btnCancel.AllowHtmlTextInToolTip = DevExpress.Utils.DefaultBoolean.[Default]
        Me.btnCancel.Appearance.Font = New System.Drawing.Font("Tahoma", 12.0!)
        Me.btnCancel.Appearance.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal
        Me.btnCancel.Appearance.Options.UseFont = True
        Me.btnCancel.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.[Default]
        Me.btnCancel.Appearance.TextOptions.HotkeyPrefix = DevExpress.Utils.HKeyPrefix.[Default]
        Me.btnCancel.Appearance.TextOptions.Trimming = DevExpress.Utils.Trimming.[Default]
        Me.btnCancel.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.[Default]
        Me.btnCancel.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.[Default]
        Me.btnCancel.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.[Default]
        Me.btnCancel.DialogResult = System.Windows.Forms.DialogResult.None
        Me.btnCancel.ImageLocation = DevExpress.XtraEditors.ImageLocation.[Default]
        Me.btnCancel.Location = New System.Drawing.Point(329, 113)
        Me.btnCancel.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Skin
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(75, 35)
        Me.btnCancel.TabIndex = 4
        Me.btnCancel.Text = "&Cancel"
        Me.btnCancel.ToolTipIconType = DevExpress.Utils.ToolTipIconType.None
        '
        'PictureEdit1
        '
        Me.PictureEdit1.AllowHtmlTextInToolTip = DevExpress.Utils.DefaultBoolean.[Default]
        Me.PictureEdit1.EditValue = CType(resources.GetObject("PictureEdit1.EditValue"), Object)
        Me.PictureEdit1.Location = New System.Drawing.Point(12, 13)
        Me.PictureEdit1.Name = "PictureEdit1"
        Me.PictureEdit1.Properties.AccessibleRole = System.Windows.Forms.AccessibleRole.[Default]
        Me.PictureEdit1.Properties.Appearance.BackColor = System.Drawing.Color.Silver
        Me.PictureEdit1.Properties.Appearance.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal
        Me.PictureEdit1.Properties.Appearance.Options.UseBackColor = True
        Me.PictureEdit1.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.[Default]
        Me.PictureEdit1.Properties.Appearance.TextOptions.HotkeyPrefix = DevExpress.Utils.HKeyPrefix.[Default]
        Me.PictureEdit1.Properties.Appearance.TextOptions.Trimming = DevExpress.Utils.Trimming.[Default]
        Me.PictureEdit1.Properties.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.[Default]
        Me.PictureEdit1.Properties.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.[Default]
        Me.PictureEdit1.Properties.AppearanceDisabled.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal
        Me.PictureEdit1.Properties.AppearanceDisabled.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.[Default]
        Me.PictureEdit1.Properties.AppearanceDisabled.TextOptions.HotkeyPrefix = DevExpress.Utils.HKeyPrefix.[Default]
        Me.PictureEdit1.Properties.AppearanceDisabled.TextOptions.Trimming = DevExpress.Utils.Trimming.[Default]
        Me.PictureEdit1.Properties.AppearanceDisabled.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.[Default]
        Me.PictureEdit1.Properties.AppearanceDisabled.TextOptions.WordWrap = DevExpress.Utils.WordWrap.[Default]
        Me.PictureEdit1.Properties.AppearanceFocused.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal
        Me.PictureEdit1.Properties.AppearanceFocused.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.[Default]
        Me.PictureEdit1.Properties.AppearanceFocused.TextOptions.HotkeyPrefix = DevExpress.Utils.HKeyPrefix.[Default]
        Me.PictureEdit1.Properties.AppearanceFocused.TextOptions.Trimming = DevExpress.Utils.Trimming.[Default]
        Me.PictureEdit1.Properties.AppearanceFocused.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.[Default]
        Me.PictureEdit1.Properties.AppearanceFocused.TextOptions.WordWrap = DevExpress.Utils.WordWrap.[Default]
        Me.PictureEdit1.Properties.AppearanceReadOnly.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal
        Me.PictureEdit1.Properties.AppearanceReadOnly.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.[Default]
        Me.PictureEdit1.Properties.AppearanceReadOnly.TextOptions.HotkeyPrefix = DevExpress.Utils.HKeyPrefix.[Default]
        Me.PictureEdit1.Properties.AppearanceReadOnly.TextOptions.Trimming = DevExpress.Utils.Trimming.[Default]
        Me.PictureEdit1.Properties.AppearanceReadOnly.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.[Default]
        Me.PictureEdit1.Properties.AppearanceReadOnly.TextOptions.WordWrap = DevExpress.Utils.WordWrap.[Default]
        Me.PictureEdit1.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.PictureEdit1.Properties.EditValueChangedFiringMode = DevExpress.XtraEditors.Controls.EditValueChangedFiringMode.[Default]
        Me.PictureEdit1.Properties.ExportMode = DevExpress.XtraEditors.Repository.ExportMode.[Default]
        Me.PictureEdit1.Properties.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Skin
        Me.PictureEdit1.Properties.PictureAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.PictureEdit1.Properties.PictureStoreMode = DevExpress.XtraEditors.Controls.PictureStoreMode.[Default]
        Me.PictureEdit1.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Zoom
        Me.PictureEdit1.Size = New System.Drawing.Size(100, 96)
        Me.PictureEdit1.TabIndex = 4
        Me.PictureEdit1.ToolTipIconType = DevExpress.Utils.ToolTipIconType.None
        '
        'txtConfirm
        '
        Me.txtConfirm.AllowHtmlTextInToolTip = DevExpress.Utils.DefaultBoolean.[Default]
        Me.txtConfirm.EnterMoveNextControl = True
        Me.txtConfirm.Location = New System.Drawing.Point(260, 80)
        Me.txtConfirm.Name = "txtConfirm"
        Me.txtConfirm.Properties.AccessibleRole = System.Windows.Forms.AccessibleRole.[Default]
        Me.txtConfirm.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.[Default]
        Me.txtConfirm.Properties.Appearance.Font = New System.Drawing.Font("Tahoma", 12.0!)
        Me.txtConfirm.Properties.Appearance.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal
        Me.txtConfirm.Properties.Appearance.Options.UseFont = True
        Me.txtConfirm.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.[Default]
        Me.txtConfirm.Properties.Appearance.TextOptions.HotkeyPrefix = DevExpress.Utils.HKeyPrefix.[Default]
        Me.txtConfirm.Properties.Appearance.TextOptions.Trimming = DevExpress.Utils.Trimming.[Default]
        Me.txtConfirm.Properties.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.[Default]
        Me.txtConfirm.Properties.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.[Default]
        Me.txtConfirm.Properties.AppearanceDisabled.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal
        Me.txtConfirm.Properties.AppearanceDisabled.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.[Default]
        Me.txtConfirm.Properties.AppearanceDisabled.TextOptions.HotkeyPrefix = DevExpress.Utils.HKeyPrefix.[Default]
        Me.txtConfirm.Properties.AppearanceDisabled.TextOptions.Trimming = DevExpress.Utils.Trimming.[Default]
        Me.txtConfirm.Properties.AppearanceDisabled.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.[Default]
        Me.txtConfirm.Properties.AppearanceDisabled.TextOptions.WordWrap = DevExpress.Utils.WordWrap.[Default]
        Me.txtConfirm.Properties.AppearanceFocused.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal
        Me.txtConfirm.Properties.AppearanceFocused.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.[Default]
        Me.txtConfirm.Properties.AppearanceFocused.TextOptions.HotkeyPrefix = DevExpress.Utils.HKeyPrefix.[Default]
        Me.txtConfirm.Properties.AppearanceFocused.TextOptions.Trimming = DevExpress.Utils.Trimming.[Default]
        Me.txtConfirm.Properties.AppearanceFocused.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.[Default]
        Me.txtConfirm.Properties.AppearanceFocused.TextOptions.WordWrap = DevExpress.Utils.WordWrap.[Default]
        Me.txtConfirm.Properties.AppearanceReadOnly.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal
        Me.txtConfirm.Properties.AppearanceReadOnly.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.[Default]
        Me.txtConfirm.Properties.AppearanceReadOnly.TextOptions.HotkeyPrefix = DevExpress.Utils.HKeyPrefix.[Default]
        Me.txtConfirm.Properties.AppearanceReadOnly.TextOptions.Trimming = DevExpress.Utils.Trimming.[Default]
        Me.txtConfirm.Properties.AppearanceReadOnly.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.[Default]
        Me.txtConfirm.Properties.AppearanceReadOnly.TextOptions.WordWrap = DevExpress.Utils.WordWrap.[Default]
        Me.txtConfirm.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.[Default]
        Me.txtConfirm.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.txtConfirm.Properties.EditValueChangedFiringMode = DevExpress.XtraEditors.Controls.EditValueChangedFiringMode.[Default]
        Me.txtConfirm.Properties.ExportMode = DevExpress.XtraEditors.Repository.ExportMode.[Default]
        Me.txtConfirm.Properties.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Skin
        Me.txtConfirm.Properties.Mask.AutoComplete = DevExpress.XtraEditors.Mask.AutoCompleteType.[Default]
        Me.txtConfirm.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.None
        Me.txtConfirm.Properties.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.txtConfirm.Size = New System.Drawing.Size(144, 26)
        Me.txtConfirm.TabIndex = 2
        Me.txtConfirm.ToolTipIconType = DevExpress.Utils.ToolTipIconType.None
        '
        'LabelControl1
        '
        Me.LabelControl1.AllowHtmlTextInToolTip = DevExpress.Utils.DefaultBoolean.[Default]
        Me.LabelControl1.Appearance.Font = New System.Drawing.Font("Tahoma", 12.0!)
        Me.LabelControl1.Appearance.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal
        Me.LabelControl1.Appearance.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.LabelControl1.Appearance.Options.UseFont = True
        Me.LabelControl1.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.[Default]
        Me.LabelControl1.Appearance.TextOptions.HotkeyPrefix = DevExpress.Utils.HKeyPrefix.[Default]
        Me.LabelControl1.Appearance.TextOptions.Trimming = DevExpress.Utils.Trimming.[Default]
        Me.LabelControl1.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.[Default]
        Me.LabelControl1.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.[Default]
        Me.LabelControl1.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.[Default]
        Me.LabelControl1.ImageAlignToText = DevExpress.XtraEditors.ImageAlignToText.None
        Me.LabelControl1.LineLocation = DevExpress.XtraEditors.LineLocation.[Default]
        Me.LabelControl1.LineOrientation = DevExpress.XtraEditors.LabelLineOrientation.[Default]
        Me.LabelControl1.Location = New System.Drawing.Point(114, 83)
        Me.LabelControl1.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Skin
        Me.LabelControl1.Name = "LabelControl1"
        Me.LabelControl1.Size = New System.Drawing.Size(140, 19)
        Me.LabelControl1.TabIndex = 1
        Me.LabelControl1.Text = "Confirm Password :"
        Me.LabelControl1.ToolTipIconType = DevExpress.Utils.ToolTipIconType.None
        '
        'btnSave
        '
        Me.btnSave.AllowHtmlTextInToolTip = DevExpress.Utils.DefaultBoolean.[Default]
        Me.btnSave.Appearance.Font = New System.Drawing.Font("Tahoma", 12.0!)
        Me.btnSave.Appearance.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal
        Me.btnSave.Appearance.Options.UseFont = True
        Me.btnSave.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.[Default]
        Me.btnSave.Appearance.TextOptions.HotkeyPrefix = DevExpress.Utils.HKeyPrefix.[Default]
        Me.btnSave.Appearance.TextOptions.Trimming = DevExpress.Utils.Trimming.[Default]
        Me.btnSave.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.[Default]
        Me.btnSave.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.[Default]
        Me.btnSave.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.[Default]
        Me.btnSave.DialogResult = System.Windows.Forms.DialogResult.None
        Me.btnSave.ImageLocation = DevExpress.XtraEditors.ImageLocation.[Default]
        Me.btnSave.Location = New System.Drawing.Point(248, 113)
        Me.btnSave.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Skin
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(75, 35)
        Me.btnSave.TabIndex = 3
        Me.btnSave.Text = "&Save"
        Me.btnSave.ToolTipIconType = DevExpress.Utils.ToolTipIconType.None
        '
        'frmChangePassword
        '
        Me.Appearance.BackColor = System.Drawing.Color.Silver
        Me.Appearance.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal
        Me.Appearance.Options.UseBackColor = True
        Me.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.[Default]
        Me.Appearance.TextOptions.HotkeyPrefix = DevExpress.Utils.HKeyPrefix.[Default]
        Me.Appearance.TextOptions.Trimming = DevExpress.Utils.Trimming.[Default]
        Me.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.[Default]
        Me.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.[Default]
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(426, 160)
        Me.Controls.Add(Me.PictureEdit1)
        Me.Controls.Add(Me.btnSave)
        Me.Controls.Add(Me.btnCancel)
        Me.Controls.Add(Me.LabelControl1)
        Me.Controls.Add(Me.lblPassword)
        Me.Controls.Add(Me.lblUsername)
        Me.Controls.Add(Me.txtConfirm)
        Me.Controls.Add(Me.txtNew)
        Me.Controls.Add(Me.txtCurrent)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Skin
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmChangePassword"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Change Password"
        Me.TopMost = True
        CType(Me.txtCurrent.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtNew.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureEdit1.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtConfirm.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents txtCurrent As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txtNew As DevExpress.XtraEditors.TextEdit
    Friend WithEvents lblUsername As DevExpress.XtraEditors.LabelControl
    Friend WithEvents lblPassword As DevExpress.XtraEditors.LabelControl
    Friend WithEvents btnCancel As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents PictureEdit1 As DevExpress.XtraEditors.PictureEdit
    Friend WithEvents txtConfirm As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LabelControl1 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents btnSave As DevExpress.XtraEditors.SimpleButton
End Class
