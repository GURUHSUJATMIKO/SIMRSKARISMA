<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Public Class xtraLabelGelang_New_05
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(xtraLabelGelang_New_05))
        Me.Detail = New DevExpress.XtraReports.UI.DetailBand()
        Me.TopMargin = New DevExpress.XtraReports.UI.TopMarginBand()
        Me.XrBarCode1 = New DevExpress.XtraReports.UI.XRBarCode()
        Me.XrLabel2 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel7 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel1 = New DevExpress.XtraReports.UI.XRLabel()
        Me.BottomMargin = New DevExpress.XtraReports.UI.BottomMarginBand()
        Me.XrControlStyle1 = New DevExpress.XtraReports.UI.XRControlStyle()
        Me.sNAME = New DevExpress.XtraReports.UI.CalculatedField()
        Me.sDEPARTMENT_DOCTOR = New DevExpress.XtraReports.UI.CalculatedField()
        Me.sLAHIR = New DevExpress.XtraReports.UI.CalculatedField()
        Me.sUMUR = New DevExpress.XtraReports.UI.CalculatedField()
        Me.XrLabel3 = New DevExpress.XtraReports.UI.XRLabel()
        Me.sANTRIAN = New DevExpress.XtraReports.UI.CalculatedField()
        Me.bindingSource = New System.Windows.Forms.BindingSource(Me.components)
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
        Me.TopMargin.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.XrLabel3, Me.XrBarCode1, Me.XrLabel2, Me.XrLabel7, Me.XrLabel1})
        Me.TopMargin.Dpi = 254.0!
        Me.TopMargin.Font = New System.Drawing.Font("Times New Roman", 9.75!)
        Me.TopMargin.HeightF = 261.0!
        Me.TopMargin.Name = "TopMargin"
        Me.TopMargin.Padding = New DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 254.0!)
        Me.TopMargin.SnapLinePadding = New DevExpress.XtraPrinting.PaddingInfo(10, 10, 10, 10, 254.0!)
        Me.TopMargin.StylePriority.UseFont = False
        Me.TopMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft
        '
        'XrBarCode1
        '
        Me.XrBarCode1.AutoModule = True
        Me.XrBarCode1.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "KDCUSTOMER")})
        Me.XrBarCode1.Dpi = 254.0!
        Me.XrBarCode1.Font = New System.Drawing.Font("Arial Rounded MT Bold", 6.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.XrBarCode1.LocationFloat = New DevExpress.Utils.PointFloat(26.74102!, 113.4089!)
        Me.XrBarCode1.Module = 5.082016!
        Me.XrBarCode1.Name = "XrBarCode1"
        Me.XrBarCode1.Padding = New DevExpress.XtraPrinting.PaddingInfo(25, 25, 0, 0, 254.0!)
        Me.XrBarCode1.ShowText = False
        Me.XrBarCode1.SizeF = New System.Drawing.SizeF(485.502!, 95.33795!)
        Me.XrBarCode1.StylePriority.UseFont = False
        Me.XrBarCode1.StylePriority.UsePadding = False
        Me.XrBarCode1.Symbology = Code128Generator1
        '
        'XrLabel2
        '
        Me.XrLabel2.CanGrow = False
        Me.XrLabel2.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "KDCUSTOMER")})
        Me.XrLabel2.Dpi = 254.0!
        Me.XrLabel2.Font = New System.Drawing.Font("Arial Rounded MT Bold", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.XrLabel2.LocationFloat = New DevExpress.Utils.PointFloat(25.0!, 208.7469!)
        Me.XrLabel2.Name = "XrLabel2"
        Me.XrLabel2.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254.0!)
        Me.XrLabel2.SizeF = New System.Drawing.SizeF(754.259!, 35.56!)
        Me.XrLabel2.StylePriority.UseFont = False
        Me.XrLabel2.StylePriority.UseTextAlignment = False
        Me.XrLabel2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter
        '
        'XrLabel7
        '
        Me.XrLabel7.CanGrow = False
        Me.XrLabel7.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "sUMUR")})
        Me.XrLabel7.Dpi = 254.0!
        Me.XrLabel7.Font = New System.Drawing.Font("Arial Rounded MT Bold", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.XrLabel7.LocationFloat = New DevExpress.Utils.PointFloat(26.74102!, 71.14335!)
        Me.XrLabel7.Name = "XrLabel7"
        Me.XrLabel7.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254.0!)
        Me.XrLabel7.SizeF = New System.Drawing.SizeF(754.2589!, 42.26559!)
        Me.XrLabel7.StylePriority.UseFont = False
        Me.XrLabel7.StylePriority.UseTextAlignment = False
        Me.XrLabel7.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter
        '
        'XrLabel1
        '
        Me.XrLabel1.CanGrow = False
        Me.XrLabel1.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "sNAME")})
        Me.XrLabel1.Dpi = 254.0!
        Me.XrLabel1.Font = New System.Drawing.Font("Arial Rounded MT Bold", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.XrLabel1.LocationFloat = New DevExpress.Utils.PointFloat(26.74098!, 35.58335!)
        Me.XrLabel1.Name = "XrLabel1"
        Me.XrLabel1.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254.0!)
        Me.XrLabel1.SizeF = New System.Drawing.SizeF(754.259!, 35.56!)
        Me.XrLabel1.StylePriority.UseFont = False
        Me.XrLabel1.StylePriority.UseTextAlignment = False
        Me.XrLabel1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter
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
        'sUMUR
        '
        Me.sUMUR.Expression = "GetDay([M_CUSTOMER.TANGGALLAHIR]) + '/' + GetMonth([M_CUSTOMER.TANGGALLAHIR]) + '" &
    "/' + GetYear([M_CUSTOMER.TANGGALLAHIR]) + '         ' + Iif([M_CUSTOMER.JK]= 1, " &
    "'(L)' , '(P)')"
        Me.sUMUR.Name = "sUMUR"
        '
        'XrLabel3
        '
        Me.XrLabel3.CanGrow = False
        Me.XrLabel3.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "sANTRIAN")})
        Me.XrLabel3.Dpi = 254.0!
        Me.XrLabel3.Font = New System.Drawing.Font("Arial Rounded MT Bold", 12.0!, System.Drawing.FontStyle.Bold)
        Me.XrLabel3.LocationFloat = New DevExpress.Utils.PointFloat(512.2431!, 113.4089!)
        Me.XrLabel3.Name = "XrLabel3"
        Me.XrLabel3.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254.0!)
        Me.XrLabel3.SizeF = New System.Drawing.SizeF(267.0159!, 95.33795!)
        Me.XrLabel3.StylePriority.UseFont = False
        Me.XrLabel3.StylePriority.UseTextAlignment = False
        Me.XrLabel3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter
        '
        'sANTRIAN
        '
        Me.sANTRIAN.Expression = "Substring([KDANTRIANONLINE], 8)"
        Me.sANTRIAN.Name = "sANTRIAN"
        '
        'bindingSource
        '
        'Me.bindingSource.DataSource = GetType(iPOS.DA.dcEntity.S_ANTRIAN_ONLINE_H)
        '
        'xtraLabelGelang_New_05
        '
        Me.Bands.AddRange(New DevExpress.XtraReports.UI.Band() {Me.Detail, Me.TopMargin, Me.BottomMargin})
        Me.CalculatedFields.AddRange(New DevExpress.XtraReports.UI.CalculatedField() {Me.sNAME, Me.sDEPARTMENT_DOCTOR, Me.sLAHIR, Me.sUMUR, Me.sANTRIAN})
        Me.DataSource = Me.bindingSource
        Me.Dpi = 254.0!
        Me.Margins = New System.Drawing.Printing.Margins(2, 0, 261, 0)
        Me.PageHeight = 259
        Me.PageWidth = 800
        Me.PaperKind = System.Drawing.Printing.PaperKind.Custom
        Me.ReportUnit = DevExpress.XtraReports.UI.ReportUnit.TenthsOfAMillimeter
        Me.ShowPrintMarginsWarning = False
        Me.StyleSheet.AddRange(New DevExpress.XtraReports.UI.XRControlStyle() {Me.XrControlStyle1})
        Me.Version = "15.1"
        'CType(Me.bindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()

    End Sub
    Friend WithEvents Detail As DevExpress.XtraReports.UI.DetailBand
    Friend WithEvents TopMargin As DevExpress.XtraReports.UI.TopMarginBand
    Friend WithEvents bindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents XrControlStyle1 As DevExpress.XtraReports.UI.XRControlStyle
    Friend WithEvents sNAME As DevExpress.XtraReports.UI.CalculatedField
    Friend WithEvents sDEPARTMENT_DOCTOR As DevExpress.XtraReports.UI.CalculatedField
    Friend WithEvents BottomMargin As DevExpress.XtraReports.UI.BottomMarginBand
    Friend WithEvents XrLabel1 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents sLAHIR As DevExpress.XtraReports.UI.CalculatedField
    Friend WithEvents sUMUR As DevExpress.XtraReports.UI.CalculatedField
    Friend WithEvents XrLabel7 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel2 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrBarCode1 As DevExpress.XtraReports.UI.XRBarCode
    Friend WithEvents XrLabel3 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents sANTRIAN As DevExpress.XtraReports.UI.CalculatedField
End Class
