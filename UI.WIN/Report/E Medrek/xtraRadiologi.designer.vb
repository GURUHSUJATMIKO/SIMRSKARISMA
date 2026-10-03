<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Public Class xtraRadiologi
    Inherits DevExpress.XtraReports.UI.XtraReport

    'XtraReport overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
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
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(xtraRadiologi))
        Me.Detail = New DevExpress.XtraReports.UI.DetailBand()
        Me.TopMargin = New DevExpress.XtraReports.UI.TopMarginBand()
        Me.BottomMargin = New DevExpress.XtraReports.UI.BottomMarginBand()
        Me.XrLabel43 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrPageInfo1 = New DevExpress.XtraReports.UI.XRPageInfo()
        Me.XrLabel1 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrControlStyle1 = New DevExpress.XtraReports.UI.XRControlStyle()
        Me.sNAMA = New DevExpress.XtraReports.UI.CalculatedField()
        Me.ReportFooter = New DevExpress.XtraReports.UI.ReportFooterBand()
        Me.picTTD = New DevExpress.XtraReports.UI.XRPictureBox()
        Me.XrLabel21 = New DevExpress.XtraReports.UI.XRLabel()
        Me.lblDOKTER = New DevExpress.XtraReports.UI.XRLabel()
        Me.PageHeader = New DevExpress.XtraReports.UI.PageHeaderBand()
        Me.sKategori = New DevExpress.XtraReports.UI.CalculatedField()
        Me.sSisa = New DevExpress.XtraReports.UI.CalculatedField()
        Me.ReportHeader = New DevExpress.XtraReports.UI.ReportHeaderBand()
        Me.XrLabel4 = New DevExpress.XtraReports.UI.XRLabel()
        Me.lblDIAGNOSA = New DevExpress.XtraReports.UI.XRLabel()
        Me.lblDOCTOR = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel3 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel15 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel13 = New DevExpress.XtraReports.UI.XRLabel()
        Me.lblEXAMDESC = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel16 = New DevExpress.XtraReports.UI.XRLabel()
        Me.lblRM = New DevExpress.XtraReports.UI.XRLabel()
        Me.lblTANGGAL = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel34 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel8 = New DevExpress.XtraReports.UI.XRLabel()
        Me.lblNAMA = New DevExpress.XtraReports.UI.XRLabel()
        Me.lblDEPARTMENT = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel11 = New DevExpress.XtraReports.UI.XRLabel()
        Me.lblREPORTDATE = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrPictureBox2 = New DevExpress.XtraReports.UI.XRPictureBox()
        Me.lblDESCRIPTION = New DevExpress.XtraReports.UI.XRLabel()
        Me.bindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.sTDT = New DevExpress.XtraReports.UI.CalculatedField()
        Me.sDoctor = New DevExpress.XtraReports.UI.CalculatedField()
        CType(Me.bindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        '
        'Detail
        '
        Me.Detail.Expanded = False
        Me.Detail.HeightF = 0!
        Me.Detail.Name = "Detail"
        Me.Detail.Padding = New DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100.0!)
        Me.Detail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft
        '
        'TopMargin
        '
        Me.TopMargin.Font = New System.Drawing.Font("Times New Roman", 9.75!)
        Me.TopMargin.HeightF = 20.0!
        Me.TopMargin.Name = "TopMargin"
        Me.TopMargin.Padding = New DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100.0!)
        Me.TopMargin.StylePriority.UseFont = False
        Me.TopMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft
        '
        'BottomMargin
        '
        Me.BottomMargin.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.XrLabel43, Me.XrPageInfo1})
        Me.BottomMargin.HeightF = 24.0!
        Me.BottomMargin.Name = "BottomMargin"
        Me.BottomMargin.Padding = New DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100.0!)
        Me.BottomMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft
        '
        'XrLabel43
        '
        Me.XrLabel43.Font = New System.Drawing.Font("Arial", 8.0!)
        Me.XrLabel43.LocationFloat = New DevExpress.Utils.PointFloat(681.3121!, 3.693898!)
        Me.XrLabel43.Name = "XrLabel43"
        Me.XrLabel43.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLabel43.SizeF = New System.Drawing.SizeF(40.16461!, 19.99998!)
        Me.XrLabel43.StylePriority.UseFont = False
        Me.XrLabel43.StylePriority.UseTextAlignment = False
        Me.XrLabel43.Text = "HAL :"
        Me.XrLabel43.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter
        Me.XrLabel43.Visible = False
        '
        'XrPageInfo1
        '
        Me.XrPageInfo1.Font = New System.Drawing.Font("Arial", 8.0!)
        Me.XrPageInfo1.LocationFloat = New DevExpress.Utils.PointFloat(721.4767!, 3.693898!)
        Me.XrPageInfo1.Name = "XrPageInfo1"
        Me.XrPageInfo1.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrPageInfo1.SizeF = New System.Drawing.SizeF(31.25!, 19.99998!)
        Me.XrPageInfo1.StylePriority.UseFont = False
        Me.XrPageInfo1.StylePriority.UseTextAlignment = False
        Me.XrPageInfo1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight
        Me.XrPageInfo1.Visible = False
        '
        'XrLabel1
        '
        Me.XrLabel1.Font = New System.Drawing.Font("Arial", 8.5!)
        Me.XrLabel1.LocationFloat = New DevExpress.Utils.PointFloat(29.18994!, 124.1182!)
        Me.XrLabel1.Name = "XrLabel1"
        Me.XrLabel1.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLabel1.SizeF = New System.Drawing.SizeF(603.7452!, 18.47221!)
        Me.XrLabel1.StylePriority.UseFont = False
        Me.XrLabel1.StylePriority.UseTextAlignment = False
        Me.XrLabel1.Text = "This document is digitally signed and hence no manual signature is required"
        Me.XrLabel1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft
        '
        'XrControlStyle1
        '
        Me.XrControlStyle1.Name = "XrControlStyle1"
        Me.XrControlStyle1.Padding = New DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100.0!)
        '
        'sNAMA
        '
        Me.sNAMA.Expression = "[NAMATARIF] + '(' + [NAMADOKTER] + ')'"
        Me.sNAMA.FieldType = DevExpress.XtraReports.UI.FieldType.[String]
        Me.sNAMA.Name = "sNAMA"
        '
        'ReportFooter
        '
        Me.ReportFooter.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.picTTD, Me.XrLabel21, Me.lblDOKTER, Me.XrLabel1})
        Me.ReportFooter.HeightF = 142.5904!
        Me.ReportFooter.KeepTogether = True
        Me.ReportFooter.Name = "ReportFooter"
        '
        'picTTD
        '
        Me.picTTD.Borders = DevExpress.XtraPrinting.BorderSide.None
        Me.picTTD.LocationFloat = New DevExpress.Utils.PointFloat(29.18968!, 28.47223!)
        Me.picTTD.Name = "picTTD"
        Me.picTTD.SizeF = New System.Drawing.SizeF(193.752!, 71.68756!)
        Me.picTTD.Sizing = DevExpress.XtraPrinting.ImageSizeMode.StretchImage
        Me.picTTD.StylePriority.UseBorders = False
        '
        'XrLabel21
        '
        Me.XrLabel21.Font = New System.Drawing.Font("Arial", 8.5!)
        Me.XrLabel21.LocationFloat = New DevExpress.Utils.PointFloat(29.18994!, 10.00001!)
        Me.XrLabel21.Name = "XrLabel21"
        Me.XrLabel21.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLabel21.SizeF = New System.Drawing.SizeF(193.7517!, 18.47221!)
        Me.XrLabel21.StylePriority.UseFont = False
        Me.XrLabel21.StylePriority.UseTextAlignment = False
        Me.XrLabel21.Text = "Best Regards,"
        Me.XrLabel21.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft
        '
        'lblDOKTER
        '
        Me.lblDOKTER.Font = New System.Drawing.Font("Arial", 8.5!)
        Me.lblDOKTER.LocationFloat = New DevExpress.Utils.PointFloat(29.18994!, 100.1598!)
        Me.lblDOKTER.Name = "lblDOKTER"
        Me.lblDOKTER.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.lblDOKTER.SizeF = New System.Drawing.SizeF(266.7513!, 18.47221!)
        Me.lblDOKTER.StylePriority.UseFont = False
        Me.lblDOKTER.StylePriority.UseTextAlignment = False
        Me.lblDOKTER.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft
        '
        'PageHeader
        '
        Me.PageHeader.Expanded = False
        Me.PageHeader.HeightF = 0!
        Me.PageHeader.Name = "PageHeader"
        '
        'sKategori
        '
        Me.sKategori.Expression = "'TOTAL ' + [KATEGORI] + ' Rp.'"
        Me.sKategori.Name = "sKategori"
        '
        'sSisa
        '
        Me.sSisa.Expression = "Sum([SUBTOTAL]) - [DEPOSIT]"
        Me.sSisa.Name = "sSisa"
        '
        'ReportHeader
        '
        Me.ReportHeader.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.XrLabel4, Me.lblDIAGNOSA, Me.lblDOCTOR, Me.XrLabel3, Me.XrLabel15, Me.XrLabel13, Me.lblEXAMDESC, Me.XrLabel16, Me.lblRM, Me.lblTANGGAL, Me.XrLabel34, Me.XrLabel8, Me.lblNAMA, Me.lblDEPARTMENT, Me.XrLabel11, Me.lblREPORTDATE, Me.XrPictureBox2, Me.lblDESCRIPTION})
        Me.ReportHeader.HeightF = 217.3956!
        Me.ReportHeader.Name = "ReportHeader"
        '
        'XrLabel4
        '
        Me.XrLabel4.Borders = CType(((DevExpress.XtraPrinting.BorderSide.Left Or DevExpress.XtraPrinting.BorderSide.Top) _
            Or DevExpress.XtraPrinting.BorderSide.Bottom), DevExpress.XtraPrinting.BorderSide)
        Me.XrLabel4.Font = New System.Drawing.Font("Arial", 8.5!)
        Me.XrLabel4.LocationFloat = New DevExpress.Utils.PointFloat(29.18968!, 155.1734!)
        Me.XrLabel4.Name = "XrLabel4"
        Me.XrLabel4.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLabel4.SizeF = New System.Drawing.SizeF(98.20041!, 18.47221!)
        Me.XrLabel4.StylePriority.UseBorders = False
        Me.XrLabel4.StylePriority.UseFont = False
        Me.XrLabel4.StylePriority.UseTextAlignment = False
        Me.XrLabel4.Text = "Diagnosa"
        Me.XrLabel4.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft
        '
        'lblDIAGNOSA
        '
        Me.lblDIAGNOSA.Borders = CType((((DevExpress.XtraPrinting.BorderSide.Left Or DevExpress.XtraPrinting.BorderSide.Top) _
            Or DevExpress.XtraPrinting.BorderSide.Right) _
            Or DevExpress.XtraPrinting.BorderSide.Bottom), DevExpress.XtraPrinting.BorderSide)
        Me.lblDIAGNOSA.Font = New System.Drawing.Font("Arial", 8.5!)
        Me.lblDIAGNOSA.LocationFloat = New DevExpress.Utils.PointFloat(127.3901!, 155.1734!)
        Me.lblDIAGNOSA.Name = "lblDIAGNOSA"
        Me.lblDIAGNOSA.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.lblDIAGNOSA.SizeF = New System.Drawing.SizeF(625.3366!, 18.47221!)
        Me.lblDIAGNOSA.StylePriority.UseBorders = False
        Me.lblDIAGNOSA.StylePriority.UseFont = False
        Me.lblDIAGNOSA.StylePriority.UseTextAlignment = False
        Me.lblDIAGNOSA.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter
        '
        'lblDOCTOR
        '
        Me.lblDOCTOR.Borders = CType(((DevExpress.XtraPrinting.BorderSide.Left Or DevExpress.XtraPrinting.BorderSide.Top) _
            Or DevExpress.XtraPrinting.BorderSide.Right), DevExpress.XtraPrinting.BorderSide)
        Me.lblDOCTOR.Font = New System.Drawing.Font("Arial", 8.5!)
        Me.lblDOCTOR.LocationFloat = New DevExpress.Utils.PointFloat(127.3903!, 136.7012!)
        Me.lblDOCTOR.Name = "lblDOCTOR"
        Me.lblDOCTOR.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.lblDOCTOR.SizeF = New System.Drawing.SizeF(625.3366!, 18.47221!)
        Me.lblDOCTOR.StylePriority.UseBorders = False
        Me.lblDOCTOR.StylePriority.UseFont = False
        Me.lblDOCTOR.StylePriority.UseTextAlignment = False
        Me.lblDOCTOR.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter
        '
        'XrLabel3
        '
        Me.XrLabel3.Borders = CType((DevExpress.XtraPrinting.BorderSide.Left Or DevExpress.XtraPrinting.BorderSide.Top), DevExpress.XtraPrinting.BorderSide)
        Me.XrLabel3.Font = New System.Drawing.Font("Arial", 8.5!)
        Me.XrLabel3.LocationFloat = New DevExpress.Utils.PointFloat(29.18992!, 136.7012!)
        Me.XrLabel3.Name = "XrLabel3"
        Me.XrLabel3.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLabel3.SizeF = New System.Drawing.SizeF(98.20041!, 18.47221!)
        Me.XrLabel3.StylePriority.UseBorders = False
        Me.XrLabel3.StylePriority.UseFont = False
        Me.XrLabel3.StylePriority.UseTextAlignment = False
        Me.XrLabel3.Text = "Dokter Pengirim"
        Me.XrLabel3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft
        '
        'XrLabel15
        '
        Me.XrLabel15.Borders = DevExpress.XtraPrinting.BorderSide.None
        Me.XrLabel15.Font = New System.Drawing.Font("Arial", 10.0!)
        Me.XrLabel15.LocationFloat = New DevExpress.Utils.PointFloat(29.18994!, 180.4512!)
        Me.XrLabel15.Name = "XrLabel15"
        Me.XrLabel15.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLabel15.SizeF = New System.Drawing.SizeF(723.5368!, 18.47221!)
        Me.XrLabel15.StylePriority.UseBorders = False
        Me.XrLabel15.StylePriority.UseFont = False
        Me.XrLabel15.StylePriority.UseTextAlignment = False
        Me.XrLabel15.Text = "(RESULT)"
        Me.XrLabel15.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter
        '
        'XrLabel13
        '
        Me.XrLabel13.Borders = CType((DevExpress.XtraPrinting.BorderSide.Left Or DevExpress.XtraPrinting.BorderSide.Top), DevExpress.XtraPrinting.BorderSide)
        Me.XrLabel13.Font = New System.Drawing.Font("Arial", 8.5!)
        Me.XrLabel13.LocationFloat = New DevExpress.Utils.PointFloat(29.18992!, 118.229!)
        Me.XrLabel13.Name = "XrLabel13"
        Me.XrLabel13.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLabel13.SizeF = New System.Drawing.SizeF(98.20039!, 18.47219!)
        Me.XrLabel13.StylePriority.UseBorders = False
        Me.XrLabel13.StylePriority.UseFont = False
        Me.XrLabel13.StylePriority.UseTextAlignment = False
        Me.XrLabel13.Text = "Pemeriksaan"
        Me.XrLabel13.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft
        '
        'lblEXAMDESC
        '
        Me.lblEXAMDESC.Borders = CType(((DevExpress.XtraPrinting.BorderSide.Left Or DevExpress.XtraPrinting.BorderSide.Top) _
            Or DevExpress.XtraPrinting.BorderSide.Right), DevExpress.XtraPrinting.BorderSide)
        Me.lblEXAMDESC.Font = New System.Drawing.Font("Arial", 8.5!)
        Me.lblEXAMDESC.LocationFloat = New DevExpress.Utils.PointFloat(127.3903!, 118.229!)
        Me.lblEXAMDESC.Name = "lblEXAMDESC"
        Me.lblEXAMDESC.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.lblEXAMDESC.SizeF = New System.Drawing.SizeF(625.3365!, 18.47219!)
        Me.lblEXAMDESC.StylePriority.UseBorders = False
        Me.lblEXAMDESC.StylePriority.UseFont = False
        Me.lblEXAMDESC.StylePriority.UseTextAlignment = False
        Me.lblEXAMDESC.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter
        '
        'XrLabel16
        '
        Me.XrLabel16.Borders = CType((DevExpress.XtraPrinting.BorderSide.Left Or DevExpress.XtraPrinting.BorderSide.Top), DevExpress.XtraPrinting.BorderSide)
        Me.XrLabel16.Font = New System.Drawing.Font("Arial", 8.5!)
        Me.XrLabel16.LocationFloat = New DevExpress.Utils.PointFloat(29.18987!, 81.28452!)
        Me.XrLabel16.Name = "XrLabel16"
        Me.XrLabel16.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLabel16.SizeF = New System.Drawing.SizeF(98.20043!, 18.47222!)
        Me.XrLabel16.StylePriority.UseBorders = False
        Me.XrLabel16.StylePriority.UseFont = False
        Me.XrLabel16.StylePriority.UseTextAlignment = False
        Me.XrLabel16.Text = "No Rekam Medis"
        Me.XrLabel16.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft
        '
        'lblRM
        '
        Me.lblRM.Borders = CType((DevExpress.XtraPrinting.BorderSide.Left Or DevExpress.XtraPrinting.BorderSide.Top), DevExpress.XtraPrinting.BorderSide)
        Me.lblRM.Font = New System.Drawing.Font("Arial", 8.5!)
        Me.lblRM.LocationFloat = New DevExpress.Utils.PointFloat(127.3903!, 81.28452!)
        Me.lblRM.Name = "lblRM"
        Me.lblRM.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.lblRM.SizeF = New System.Drawing.SizeF(138.4878!, 18.47223!)
        Me.lblRM.StylePriority.UseBorders = False
        Me.lblRM.StylePriority.UseFont = False
        Me.lblRM.StylePriority.UseTextAlignment = False
        Me.lblRM.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter
        '
        'lblTANGGAL
        '
        Me.lblTANGGAL.Borders = CType((DevExpress.XtraPrinting.BorderSide.Left Or DevExpress.XtraPrinting.BorderSide.Top), DevExpress.XtraPrinting.BorderSide)
        Me.lblTANGGAL.Font = New System.Drawing.Font("Arial", 8.5!)
        Me.lblTANGGAL.LocationFloat = New DevExpress.Utils.PointFloat(596.0865!, 99.75673!)
        Me.lblTANGGAL.Name = "lblTANGGAL"
        Me.lblTANGGAL.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.lblTANGGAL.SizeF = New System.Drawing.SizeF(156.6401!, 18.47221!)
        Me.lblTANGGAL.StylePriority.UseBorders = False
        Me.lblTANGGAL.StylePriority.UseFont = False
        Me.lblTANGGAL.StylePriority.UseTextAlignment = False
        Me.lblTANGGAL.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter
        '
        'XrLabel34
        '
        Me.XrLabel34.Borders = CType((DevExpress.XtraPrinting.BorderSide.Left Or DevExpress.XtraPrinting.BorderSide.Top), DevExpress.XtraPrinting.BorderSide)
        Me.XrLabel34.Font = New System.Drawing.Font("Arial", 8.5!)
        Me.XrLabel34.LocationFloat = New DevExpress.Utils.PointFloat(29.18987!, 99.75681!)
        Me.XrLabel34.Name = "XrLabel34"
        Me.XrLabel34.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLabel34.SizeF = New System.Drawing.SizeF(98.20044!, 18.47221!)
        Me.XrLabel34.StylePriority.UseBorders = False
        Me.XrLabel34.StylePriority.UseFont = False
        Me.XrLabel34.StylePriority.UseTextAlignment = False
        Me.XrLabel34.Text = "Tanggal"
        Me.XrLabel34.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft
        '
        'XrLabel8
        '
        Me.XrLabel8.Borders = CType((DevExpress.XtraPrinting.BorderSide.Left Or DevExpress.XtraPrinting.BorderSide.Top), DevExpress.XtraPrinting.BorderSide)
        Me.XrLabel8.Font = New System.Drawing.Font("Arial", 8.5!)
        Me.XrLabel8.LocationFloat = New DevExpress.Utils.PointFloat(265.8781!, 99.75681!)
        Me.XrLabel8.Name = "XrLabel8"
        Me.XrLabel8.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLabel8.SizeF = New System.Drawing.SizeF(47.04605!, 18.47222!)
        Me.XrLabel8.StylePriority.UseBorders = False
        Me.XrLabel8.StylePriority.UseFont = False
        Me.XrLabel8.StylePriority.UseTextAlignment = False
        Me.XrLabel8.Text = "Dept"
        Me.XrLabel8.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft
        '
        'lblNAMA
        '
        Me.lblNAMA.Borders = CType(((DevExpress.XtraPrinting.BorderSide.Left Or DevExpress.XtraPrinting.BorderSide.Top) _
            Or DevExpress.XtraPrinting.BorderSide.Right), DevExpress.XtraPrinting.BorderSide)
        Me.lblNAMA.Font = New System.Drawing.Font("Arial", 8.5!)
        Me.lblNAMA.LocationFloat = New DevExpress.Utils.PointFloat(265.8781!, 81.28452!)
        Me.lblNAMA.Name = "lblNAMA"
        Me.lblNAMA.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.lblNAMA.SizeF = New System.Drawing.SizeF(486.8487!, 18.47221!)
        Me.lblNAMA.StylePriority.UseBorders = False
        Me.lblNAMA.StylePriority.UseFont = False
        Me.lblNAMA.StylePriority.UseTextAlignment = False
        Me.lblNAMA.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter
        '
        'lblDEPARTMENT
        '
        Me.lblDEPARTMENT.Borders = CType((DevExpress.XtraPrinting.BorderSide.Left Or DevExpress.XtraPrinting.BorderSide.Top), DevExpress.XtraPrinting.BorderSide)
        Me.lblDEPARTMENT.Font = New System.Drawing.Font("Arial", 8.5!)
        Me.lblDEPARTMENT.LocationFloat = New DevExpress.Utils.PointFloat(312.9242!, 99.75681!)
        Me.lblDEPARTMENT.Name = "lblDEPARTMENT"
        Me.lblDEPARTMENT.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.lblDEPARTMENT.SizeF = New System.Drawing.SizeF(203.7121!, 18.47221!)
        Me.lblDEPARTMENT.StylePriority.UseBorders = False
        Me.lblDEPARTMENT.StylePriority.UseFont = False
        Me.lblDEPARTMENT.StylePriority.UseTextAlignment = False
        Me.lblDEPARTMENT.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter
        '
        'XrLabel11
        '
        Me.XrLabel11.Borders = CType((DevExpress.XtraPrinting.BorderSide.Left Or DevExpress.XtraPrinting.BorderSide.Top), DevExpress.XtraPrinting.BorderSide)
        Me.XrLabel11.Font = New System.Drawing.Font("Arial", 8.5!)
        Me.XrLabel11.LocationFloat = New DevExpress.Utils.PointFloat(516.6363!, 99.75681!)
        Me.XrLabel11.Name = "XrLabel11"
        Me.XrLabel11.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLabel11.SizeF = New System.Drawing.SizeF(79.45041!, 18.47221!)
        Me.XrLabel11.StylePriority.UseBorders = False
        Me.XrLabel11.StylePriority.UseFont = False
        Me.XrLabel11.StylePriority.UseTextAlignment = False
        Me.XrLabel11.Text = "Report Date"
        Me.XrLabel11.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft
        '
        'lblREPORTDATE
        '
        Me.lblREPORTDATE.Borders = CType(((DevExpress.XtraPrinting.BorderSide.Left Or DevExpress.XtraPrinting.BorderSide.Top) _
            Or DevExpress.XtraPrinting.BorderSide.Right), DevExpress.XtraPrinting.BorderSide)
        Me.lblREPORTDATE.Font = New System.Drawing.Font("Arial", 8.5!)
        Me.lblREPORTDATE.LocationFloat = New DevExpress.Utils.PointFloat(127.3901!, 99.75683!)
        Me.lblREPORTDATE.Name = "lblREPORTDATE"
        Me.lblREPORTDATE.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.lblREPORTDATE.SizeF = New System.Drawing.SizeF(138.488!, 18.47221!)
        Me.lblREPORTDATE.StylePriority.UseBorders = False
        Me.lblREPORTDATE.StylePriority.UseFont = False
        Me.lblREPORTDATE.StylePriority.UseTextAlignment = False
        Me.lblREPORTDATE.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter
        '
        'XrPictureBox2
        '
        Me.XrPictureBox2.Image = CType(resources.GetObject("XrPictureBox2.Image"), System.Drawing.Image)
        Me.XrPictureBox2.LocationFloat = New DevExpress.Utils.PointFloat(205.9164!, 10.00001!)
        Me.XrPictureBox2.Name = "XrPictureBox2"
        Me.XrPictureBox2.SizeF = New System.Drawing.SizeF(361.5579!, 60.34718!)
        Me.XrPictureBox2.Sizing = DevExpress.XtraPrinting.ImageSizeMode.StretchImage
        '
        'lblDESCRIPTION
        '
        Me.lblDESCRIPTION.Borders = CType((((DevExpress.XtraPrinting.BorderSide.Left Or DevExpress.XtraPrinting.BorderSide.Top) _
            Or DevExpress.XtraPrinting.BorderSide.Right) _
            Or DevExpress.XtraPrinting.BorderSide.Bottom), DevExpress.XtraPrinting.BorderSide)
        Me.lblDESCRIPTION.Font = New System.Drawing.Font("Arial", 8.0!)
        Me.lblDESCRIPTION.LocationFloat = New DevExpress.Utils.PointFloat(29.18987!, 198.9234!)
        Me.lblDESCRIPTION.Multiline = True
        Me.lblDESCRIPTION.Name = "lblDESCRIPTION"
        Me.lblDESCRIPTION.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.lblDESCRIPTION.SizeF = New System.Drawing.SizeF(723.5369!, 18.47215!)
        Me.lblDESCRIPTION.StylePriority.UseBorders = False
        Me.lblDESCRIPTION.StylePriority.UseFont = False
        Me.lblDESCRIPTION.StylePriority.UseTextAlignment = False
        Me.lblDESCRIPTION.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft
        '
        'sTDT
        '
        Me.sTDT.Expression = "DateDiffMinute([TANGGALINPUT], [TANGGALHASIL])"
        Me.sTDT.Name = "sTDT"
        '
        'sDoctor
        '
        Me.sDoctor.Expression = "IIF(IsNullOrEmpty([S_PENDAFTARAN_H.M_DOCTOR.FRONT_TITLE]), '',  [S_PENDAFTARAN_H." &
    "M_DOCTOR.FRONT_TITLE] + ' ') + [S_PENDAFTARAN_H.M_DOCTOR.NAME_DISPLAY] + ' ' + [" &
    "S_PENDAFTARAN_H.M_DOCTOR.BACK_TITLE]"
        Me.sDoctor.Name = "sDoctor"
        '
        'xtraRadiologi
        '
        Me.Bands.AddRange(New DevExpress.XtraReports.UI.Band() {Me.Detail, Me.TopMargin, Me.BottomMargin, Me.PageHeader, Me.ReportFooter, Me.ReportHeader})
        Me.CalculatedFields.AddRange(New DevExpress.XtraReports.UI.CalculatedField() {Me.sNAMA, Me.sKategori, Me.sSisa, Me.sTDT, Me.sDoctor})
        Me.DataSource = Me.bindingSource
        Me.Margins = New System.Drawing.Printing.Margins(22, 21, 20, 24)
        Me.PageHeight = 1169
        Me.PageWidth = 827
        Me.PaperKind = System.Drawing.Printing.PaperKind.A4
        Me.ShowPrintMarginsWarning = false
        Me.StyleSheet.AddRange(New DevExpress.XtraReports.UI.XRControlStyle() {Me.XrControlStyle1})
        Me.Version = "15.1"
        CType(Me.bindingSource,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me,System.ComponentModel.ISupportInitialize).EndInit

End Sub
    Friend WithEvents Detail As DevExpress.XtraReports.UI.DetailBand
    Friend WithEvents TopMargin As DevExpress.XtraReports.UI.TopMarginBand
    Friend WithEvents BottomMargin As DevExpress.XtraReports.UI.BottomMarginBand
    Friend WithEvents bindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents XrControlStyle1 As DevExpress.XtraReports.UI.XRControlStyle
    Friend WithEvents sNAMA As DevExpress.XtraReports.UI.CalculatedField
    Friend WithEvents ReportFooter As DevExpress.XtraReports.UI.ReportFooterBand
    Friend WithEvents PageHeader As DevExpress.XtraReports.UI.PageHeaderBand
    Friend WithEvents sKategori As DevExpress.XtraReports.UI.CalculatedField
    Friend WithEvents sSisa As DevExpress.XtraReports.UI.CalculatedField
    Friend WithEvents ReportHeader As DevExpress.XtraReports.UI.ReportHeaderBand
    Friend WithEvents lblDESCRIPTION As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents lblDOKTER As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel21 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents sTDT As DevExpress.XtraReports.UI.CalculatedField
    Friend WithEvents XrPictureBox2 As DevExpress.XtraReports.UI.XRPictureBox
    Friend WithEvents lblDEPARTMENT As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel11 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents lblREPORTDATE As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents lblNAMA As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel8 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel34 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents lblTANGGAL As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents lblRM As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel16 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel13 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents lblEXAMDESC As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel15 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel1 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel43 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrPageInfo1 As DevExpress.XtraReports.UI.XRPageInfo
    Friend WithEvents lblDOCTOR As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel3 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel4 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents lblDIAGNOSA As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents sDoctor As DevExpress.XtraReports.UI.CalculatedField
    Friend WithEvents picTTD As DevExpress.XtraReports.UI.XRPictureBox
End Class
