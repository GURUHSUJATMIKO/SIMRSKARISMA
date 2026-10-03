<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Public Class xtraLabelGelang_New_04
    Inherits DevExpress.XtraReports.UI.XtraReport

    'XtraReport overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Designer
    'It can be modified using the Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim Code128Generator1 As DevExpress.XtraPrinting.BarCode.Code128Generator = New DevExpress.XtraPrinting.BarCode.Code128Generator()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(xtraLabelGelang_New_04))
        Me.Detail = New DevExpress.XtraReports.UI.DetailBand()
        Me.TopMargin = New DevExpress.XtraReports.UI.TopMarginBand()
        Me.txtRM6 = New DevExpress.XtraReports.UI.XRLabel()
        Me.txtRM5 = New DevExpress.XtraReports.UI.XRLabel()
        Me.txtRM4 = New DevExpress.XtraReports.UI.XRLabel()
        Me.txtRM3 = New DevExpress.XtraReports.UI.XRLabel()
        Me.txtRM2 = New DevExpress.XtraReports.UI.XRLabel()
        Me.txtRM1 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrBarCode1 = New DevExpress.XtraReports.UI.XRBarCode()
        Me.BottomMargin = New DevExpress.XtraReports.UI.BottomMarginBand()
        Me.XrControlStyle1 = New DevExpress.XtraReports.UI.XRControlStyle()
        Me.sNAME = New DevExpress.XtraReports.UI.CalculatedField()
        Me.sDEPARTMENT_DOCTOR = New DevExpress.XtraReports.UI.CalculatedField()
        Me.sLAHIR = New DevExpress.XtraReports.UI.CalculatedField()
        Me.CalculatedField1 = New DevExpress.XtraReports.UI.CalculatedField()
        Me.bindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.XrLabel1 = New DevExpress.XtraReports.UI.XRLabel()
        CType(Me.bindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        '
        'Detail
        '
        Me.Detail.Dpi = 254.0!
        Me.Detail.HeightF = 0!
        Me.Detail.Name = "Detail"
        Me.Detail.Padding = New DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 254.0!)
        Me.Detail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft
        '
        'TopMargin
        '
        Me.TopMargin.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.XrLabel1, Me.txtRM6, Me.txtRM5, Me.txtRM4, Me.txtRM3, Me.txtRM2, Me.txtRM1, Me.XrBarCode1})
        Me.TopMargin.Dpi = 254.0!
        Me.TopMargin.Font = New System.Drawing.Font("Times New Roman", 9.75!)
        Me.TopMargin.HeightF = 261.0!
        Me.TopMargin.Name = "TopMargin"
        Me.TopMargin.Padding = New DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 254.0!)
        Me.TopMargin.SnapLinePadding = New DevExpress.XtraPrinting.PaddingInfo(10, 10, 10, 10, 254.0!)
        Me.TopMargin.StylePriority.UseFont = False
        Me.TopMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft
        '
        'txtRM6
        '
        Me.txtRM6.CanGrow = False
        Me.txtRM6.Dpi = 254.0!
        Me.txtRM6.Font = New System.Drawing.Font("Arial Rounded MT Bold", 36.0!)
        Me.txtRM6.LocationFloat = New DevExpress.Utils.PointFloat(603.6959!, 22.54166!)
        Me.txtRM6.Name = "txtRM6"
        Me.txtRM6.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254.0!)
        Me.txtRM6.SizeF = New System.Drawing.SizeF(104.5021!, 106.7423!)
        Me.txtRM6.StylePriority.UseFont = False
        Me.txtRM6.StylePriority.UseTextAlignment = False
        Me.txtRM6.Text = "6"
        Me.txtRM6.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter
        '
        'txtRM5
        '
        Me.txtRM5.CanGrow = False
        Me.txtRM5.Dpi = 254.0!
        Me.txtRM5.Font = New System.Drawing.Font("Arial Rounded MT Bold", 36.0!)
        Me.txtRM5.LocationFloat = New DevExpress.Utils.PointFloat(499.1938!, 22.54166!)
        Me.txtRM5.Name = "txtRM5"
        Me.txtRM5.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254.0!)
        Me.txtRM5.SizeF = New System.Drawing.SizeF(104.5021!, 106.7423!)
        Me.txtRM5.StylePriority.UseFont = False
        Me.txtRM5.StylePriority.UseTextAlignment = False
        Me.txtRM5.Text = "5"
        Me.txtRM5.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter
        '
        'txtRM4
        '
        Me.txtRM4.CanGrow = False
        Me.txtRM4.Dpi = 254.0!
        Me.txtRM4.Font = New System.Drawing.Font("Arial Rounded MT Bold", 36.0!)
        Me.txtRM4.LocationFloat = New DevExpress.Utils.PointFloat(394.6917!, 22.54166!)
        Me.txtRM4.Name = "txtRM4"
        Me.txtRM4.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254.0!)
        Me.txtRM4.SizeF = New System.Drawing.SizeF(104.5021!, 106.7423!)
        Me.txtRM4.StylePriority.UseFont = False
        Me.txtRM4.StylePriority.UseTextAlignment = False
        Me.txtRM4.Text = "4"
        Me.txtRM4.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter
        '
        'txtRM3
        '
        Me.txtRM3.CanGrow = False
        Me.txtRM3.Dpi = 254.0!
        Me.txtRM3.Font = New System.Drawing.Font("Arial Rounded MT Bold", 36.0!)
        Me.txtRM3.LocationFloat = New DevExpress.Utils.PointFloat(290.1896!, 22.54166!)
        Me.txtRM3.Name = "txtRM3"
        Me.txtRM3.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254.0!)
        Me.txtRM3.SizeF = New System.Drawing.SizeF(104.5021!, 106.7423!)
        Me.txtRM3.StylePriority.UseFont = False
        Me.txtRM3.StylePriority.UseTextAlignment = False
        Me.txtRM3.Text = "3"
        Me.txtRM3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter
        '
        'txtRM2
        '
        Me.txtRM2.CanGrow = False
        Me.txtRM2.Dpi = 254.0!
        Me.txtRM2.Font = New System.Drawing.Font("Arial Rounded MT Bold", 36.0!)
        Me.txtRM2.LocationFloat = New DevExpress.Utils.PointFloat(185.6875!, 22.54166!)
        Me.txtRM2.Name = "txtRM2"
        Me.txtRM2.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254.0!)
        Me.txtRM2.SizeF = New System.Drawing.SizeF(104.5021!, 106.7423!)
        Me.txtRM2.StylePriority.UseFont = False
        Me.txtRM2.StylePriority.UseTextAlignment = False
        Me.txtRM2.Text = "2"
        Me.txtRM2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter
        '
        'txtRM1
        '
        Me.txtRM1.CanGrow = False
        Me.txtRM1.Dpi = 254.0!
        Me.txtRM1.Font = New System.Drawing.Font("Arial Rounded MT Bold", 36.0!)
        Me.txtRM1.LocationFloat = New DevExpress.Utils.PointFloat(81.18546!, 22.54166!)
        Me.txtRM1.Name = "txtRM1"
        Me.txtRM1.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254.0!)
        Me.txtRM1.SizeF = New System.Drawing.SizeF(104.5021!, 106.7423!)
        Me.txtRM1.StylePriority.UseFont = False
        Me.txtRM1.StylePriority.UseTextAlignment = False
        Me.txtRM1.Text = "1"
        Me.txtRM1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter
        '
        'XrBarCode1
        '
        Me.XrBarCode1.AutoModule = True
        Me.XrBarCode1.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "KDCUSTOMER")})
        Me.XrBarCode1.Dpi = 254.0!
        Me.XrBarCode1.Font = New System.Drawing.Font("Arial Rounded MT Bold", 6.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.XrBarCode1.ForeColor = System.Drawing.Color.Black
        Me.XrBarCode1.LocationFloat = New DevExpress.Utils.PointFloat(44.14378!, 129.2839!)
        Me.XrBarCode1.Module = 5.082016!
        Me.XrBarCode1.Name = "XrBarCode1"
        Me.XrBarCode1.Padding = New DevExpress.XtraPrinting.PaddingInfo(25, 25, 0, 0, 254.0!)
        Me.XrBarCode1.ShowText = False
        Me.XrBarCode1.SizeF = New System.Drawing.SizeF(697.1688!, 71.52551!)
        Me.XrBarCode1.StylePriority.UseFont = False
        Me.XrBarCode1.StylePriority.UseForeColor = False
        Me.XrBarCode1.StylePriority.UsePadding = False
        Me.XrBarCode1.StylePriority.UseTextAlignment = False
        Me.XrBarCode1.Symbology = Code128Generator1
        Me.XrBarCode1.Text = "AFAF"
        Me.XrBarCode1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter
        '
        'BottomMargin
        '
        Me.BottomMargin.Dpi = 254.0!
        Me.BottomMargin.HeightF = 0!
        Me.BottomMargin.Name = "BottomMargin"
        Me.BottomMargin.Padding = New DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 254.0!)
        Me.BottomMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft
        '
        'XrControlStyle1
        '
        Me.XrControlStyle1.Name = "XrControlStyle1"
        Me.XrControlStyle1.Padding = New DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 254.0!)
        '
        'sNAME
        '
        Me.sNAME.Expression = "[M_CUSTOMER.NAME_DISPLAY]  + '  ' + Iif(IsNullOrEmpty([M_CUSTOMER.FRONT_TITLE]) =" &
    " True,' ' ,[M_CUSTOMER.FRONT_TITLE] + ' ') + [M_CUSTOMER.BACK_TITLE]"
        Me.sNAME.FieldType = DevExpress.XtraReports.UI.FieldType.[String]
        Me.sNAME.Name = "sNAME"
        '
        'sDEPARTMENT_DOCTOR
        '
        Me.sDEPARTMENT_DOCTOR.Expression = "[M_DOCTOR.M_DEPARTMENT.NAME_DISPLAY]  +  ' / ' + [M_DOCTOR.NAME_DISPLAY]"
        Me.sDEPARTMENT_DOCTOR.FieldType = DevExpress.XtraReports.UI.FieldType.[String]
        Me.sDEPARTMENT_DOCTOR.Name = "sDEPARTMENT_DOCTOR"
        '
        'sLAHIR
        '
        Me.sLAHIR.Expression = resources.GetString("sLAHIR.Expression")
        Me.sLAHIR.Name = "sLAHIR"
        '
        'CalculatedField1
        '
        Me.CalculatedField1.Expression = "GetDay(Now()) + '/' + GetMonth(Now()) + '/' + GetYear(Now())"
        Me.CalculatedField1.Name = "CalculatedField1"
        '
        'bindingSource
        '
        ' Me.bindingSource.DataSource = GetType(iPOS.DA.dcEntity.S_PENDAFTARAN_H)
        '
        'XrLabel1
        '
        Me.XrLabel1.CanGrow = False
        Me.XrLabel1.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "M_CUSTOMER.NAME_DISPLAY")})
        Me.XrLabel1.Dpi = 254.0!
        Me.XrLabel1.Font = New System.Drawing.Font("Arial Rounded MT Bold", 12.0!)
        Me.XrLabel1.LocationFloat = New DevExpress.Utils.PointFloat(44.14378!, 200.8094!)
        Me.XrLabel1.Name = "XrLabel1"
        Me.XrLabel1.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254.0!)
        Me.XrLabel1.SizeF = New System.Drawing.SizeF(697.1688!, 48.53395!)
        Me.XrLabel1.StylePriority.UseFont = False
        Me.XrLabel1.StylePriority.UseTextAlignment = False
        Me.XrLabel1.Text = "1"
        Me.XrLabel1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft
        '
        'xtraLabelGelang_New_04
        '
        Me.Bands.AddRange(New DevExpress.XtraReports.UI.Band() {Me.Detail, Me.TopMargin, Me.BottomMargin})
        Me.CalculatedFields.AddRange(New DevExpress.XtraReports.UI.CalculatedField() {Me.sNAME, Me.sDEPARTMENT_DOCTOR, Me.sLAHIR, Me.CalculatedField1})
        Me.DataSource = Me.bindingSource
        Me.Dpi = 254.0!
        Me.Margins = New System.Drawing.Printing.Margins(2, 0, 261, 0)
        Me.PageHeight = 259
        Me.PageWidth = 800
        Me.PaperKind = System.Drawing.Printing.PaperKind.Custom
        Me.ReportUnit = DevExpress.XtraReports.UI.ReportUnit.TenthsOfAMillimeter
        Me.ShowPrintMarginsWarning = false
        Me.StyleSheet.AddRange(New DevExpress.XtraReports.UI.XRControlStyle() {Me.XrControlStyle1})
        Me.Version = "15.1"
        'CType(Me.bindingSource,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me,System.ComponentModel.ISupportInitialize).EndInit

End Sub
    Friend WithEvents Detail As DevExpress.XtraReports.UI.DetailBand
    Friend WithEvents TopMargin As DevExpress.XtraReports.UI.TopMarginBand
    Friend WithEvents bindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents XrControlStyle1 As DevExpress.XtraReports.UI.XRControlStyle
    Friend WithEvents sNAME As DevExpress.XtraReports.UI.CalculatedField
    Friend WithEvents sDEPARTMENT_DOCTOR As DevExpress.XtraReports.UI.CalculatedField
    Friend WithEvents BottomMargin As DevExpress.XtraReports.UI.BottomMarginBand
    Friend WithEvents sLAHIR As DevExpress.XtraReports.UI.CalculatedField
    Friend WithEvents CalculatedField1 As DevExpress.XtraReports.UI.CalculatedField
    Friend WithEvents XrBarCode1 As DevExpress.XtraReports.UI.XRBarCode
    Friend WithEvents txtRM1 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents txtRM6 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents txtRM5 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents txtRM4 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents txtRM3 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents txtRM2 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel1 As DevExpress.XtraReports.UI.XRLabel
End Class
