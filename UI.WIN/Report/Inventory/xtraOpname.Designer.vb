<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Public Class xtraOpname
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
        Dim XrSummary1 As DevExpress.XtraReports.UI.XRSummary = New DevExpress.XtraReports.UI.XRSummary()
        Dim XrSummary2 As DevExpress.XtraReports.UI.XRSummary = New DevExpress.XtraReports.UI.XRSummary()
        Me.Detail = New DevExpress.XtraReports.UI.DetailBand()
        Me.TopMargin = New DevExpress.XtraReports.UI.TopMarginBand()
        Me.XrPageInfo2 = New DevExpress.XtraReports.UI.XRPageInfo()
        Me.lCOMPANY = New DevExpress.XtraReports.UI.XRLabel()
        Me.BottomMargin = New DevExpress.XtraReports.UI.BottomMarginBand()
        Me.lblREPORT = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel1 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel2 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel3 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel5 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel6 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel7 = New DevExpress.XtraReports.UI.XRLabel()
        Me.bindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.XrLabel9 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel10 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel11 = New DevExpress.XtraReports.UI.XRLabel()
        Me.sSEQ = New DevExpress.XtraReports.UI.CalculatedField()
        Me.XrLabel13 = New DevExpress.XtraReports.UI.XRLabel()
        Me.DetailReport = New DevExpress.XtraReports.UI.DetailReportBand()
        Me.Detail1 = New DevExpress.XtraReports.UI.DetailBand()
        Me.XrLabel19 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel31 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel23 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel22 = New DevExpress.XtraReports.UI.XRLabel()
        Me.GroupHeader1 = New DevExpress.XtraReports.UI.GroupHeaderBand()
        Me.XrLabel18 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel17 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel16 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel15 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel14 = New DevExpress.XtraReports.UI.XRLabel()
        Me.GroupFooter1 = New DevExpress.XtraReports.UI.GroupFooterBand()
        Me.XrLabel48 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrPageInfo1 = New DevExpress.XtraReports.UI.XRPageInfo()
        Me.XrLabel47 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel44 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel29 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel43 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel46 = New DevExpress.XtraReports.UI.XRLabel()
        Me.ReportFooter = New DevExpress.XtraReports.UI.ReportFooterBand()
        Me.ReportHeader = New DevExpress.XtraReports.UI.ReportHeaderBand()
        Me.lNPWP = New DevExpress.XtraReports.UI.XRLabel()
        Me.lPHONE = New DevExpress.XtraReports.UI.XRLabel()
        Me.lADDRESS = New DevExpress.XtraReports.UI.XRLabel()
        CType(Me.bindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        '
        'Detail
        '
        Me.Detail.HeightF = 0.0!
        Me.Detail.Name = "Detail"
        Me.Detail.Padding = New DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100.0!)
        Me.Detail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft
        '
        'TopMargin
        '
        Me.TopMargin.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.XrPageInfo2})
        Me.TopMargin.HeightF = 50.0!
        Me.TopMargin.Name = "TopMargin"
        Me.TopMargin.Padding = New DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100.0!)
        Me.TopMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft
        '
        'XrPageInfo2
        '
        Me.XrPageInfo2.Format = "HAL : {0} / {1}"
        Me.XrPageInfo2.LocationFloat = New DevExpress.Utils.PointFloat(576.4584!, 10.00001!)
        Me.XrPageInfo2.Name = "XrPageInfo2"
        Me.XrPageInfo2.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrPageInfo2.SizeF = New System.Drawing.SizeF(163.5416!, 23.0!)
        Me.XrPageInfo2.StylePriority.UseTextAlignment = False
        Me.XrPageInfo2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight
        '
        'lCOMPANY
        '
        Me.lCOMPANY.CanShrink = True
        Me.lCOMPANY.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Bold)
        Me.lCOMPANY.LocationFloat = New DevExpress.Utils.PointFloat(10.00023!, 10.00001!)
        Me.lCOMPANY.Name = "lCOMPANY"
        Me.lCOMPANY.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.lCOMPANY.SizeF = New System.Drawing.SizeF(350.6248!, 25.0!)
        Me.lCOMPANY.StylePriority.UseFont = False
        Me.lCOMPANY.Text = "lCOMPANY"
        '
        'BottomMargin
        '
        Me.BottomMargin.HeightF = 50.0!
        Me.BottomMargin.Name = "BottomMargin"
        Me.BottomMargin.Padding = New DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100.0!)
        Me.BottomMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft
        '
        'lblREPORT
        '
        Me.lblREPORT.Font = New System.Drawing.Font("Arial", 12.0!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Underline), System.Drawing.FontStyle))
        Me.lblREPORT.LocationFloat = New DevExpress.Utils.PointFloat(383.333!, 10.00001!)
        Me.lblREPORT.Name = "lblREPORT"
        Me.lblREPORT.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.lblREPORT.SizeF = New System.Drawing.SizeF(356.67!, 25.0!)
        Me.lblREPORT.StylePriority.UseFont = False
        Me.lblREPORT.Text = "STOK OPNAME"
        '
        'XrLabel1
        '
        Me.XrLabel1.Font = New System.Drawing.Font("Arial", 9.0!)
        Me.XrLabel1.LocationFloat = New DevExpress.Utils.PointFloat(383.3333!, 35.00001!)
        Me.XrLabel1.Name = "XrLabel1"
        Me.XrLabel1.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLabel1.SizeF = New System.Drawing.SizeF(79.17!, 18.0!)
        Me.XrLabel1.StylePriority.UseFont = False
        Me.XrLabel1.Text = "NOMOR"
        '
        'XrLabel2
        '
        Me.XrLabel2.Font = New System.Drawing.Font("Arial", 9.0!)
        Me.XrLabel2.LocationFloat = New DevExpress.Utils.PointFloat(383.3366!, 53.0!)
        Me.XrLabel2.Name = "XrLabel2"
        Me.XrLabel2.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLabel2.SizeF = New System.Drawing.SizeF(79.16669!, 18.0!)
        Me.XrLabel2.StylePriority.UseFont = False
        Me.XrLabel2.Text = "TANGGAL"
        '
        'XrLabel3
        '
        Me.XrLabel3.Font = New System.Drawing.Font("Arial", 9.0!)
        Me.XrLabel3.LocationFloat = New DevExpress.Utils.PointFloat(383.3333!, 71.0!)
        Me.XrLabel3.Name = "XrLabel3"
        Me.XrLabel3.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLabel3.SizeF = New System.Drawing.SizeF(79.16663!, 18.0!)
        Me.XrLabel3.StylePriority.UseFont = False
        Me.XrLabel3.Text = "GUDANG"
        '
        'XrLabel5
        '
        Me.XrLabel5.Font = New System.Drawing.Font("Arial", 9.0!)
        Me.XrLabel5.LocationFloat = New DevExpress.Utils.PointFloat(462.5032!, 35.00001!)
        Me.XrLabel5.Name = "XrLabel5"
        Me.XrLabel5.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLabel5.SizeF = New System.Drawing.SizeF(11.46164!, 18.0!)
        Me.XrLabel5.StylePriority.UseFont = False
        Me.XrLabel5.Text = ":"
        '
        'XrLabel6
        '
        Me.XrLabel6.Font = New System.Drawing.Font("Arial", 9.0!)
        Me.XrLabel6.LocationFloat = New DevExpress.Utils.PointFloat(462.5032!, 53.0!)
        Me.XrLabel6.Name = "XrLabel6"
        Me.XrLabel6.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLabel6.SizeF = New System.Drawing.SizeF(11.46164!, 18.0!)
        Me.XrLabel6.StylePriority.UseFont = False
        Me.XrLabel6.Text = ":"
        '
        'XrLabel7
        '
        Me.XrLabel7.Font = New System.Drawing.Font("Arial", 9.0!)
        Me.XrLabel7.LocationFloat = New DevExpress.Utils.PointFloat(462.5032!, 71.0!)
        Me.XrLabel7.Name = "XrLabel7"
        Me.XrLabel7.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLabel7.SizeF = New System.Drawing.SizeF(11.46164!, 18.0!)
        Me.XrLabel7.StylePriority.UseFont = False
        Me.XrLabel7.Text = ":"
        '
        'bindingSource
        '
        Me.bindingSource.DataSource = GetType(DataAccess.I_OPNAME_H)
        '
        'XrLabel9
        '
        Me.XrLabel9.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "KDOPNAME")})
        Me.XrLabel9.Font = New System.Drawing.Font("Arial", 9.0!)
        Me.XrLabel9.LocationFloat = New DevExpress.Utils.PointFloat(473.9649!, 35.00001!)
        Me.XrLabel9.Name = "XrLabel9"
        Me.XrLabel9.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLabel9.SizeF = New System.Drawing.SizeF(266.0348!, 18.0!)
        Me.XrLabel9.StylePriority.UseFont = False
        Me.XrLabel9.Text = "XrLabel9"
        '
        'XrLabel10
        '
        Me.XrLabel10.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "DATE", "{0:dd/MM/yyyy}")})
        Me.XrLabel10.Font = New System.Drawing.Font("Arial", 9.0!)
        Me.XrLabel10.LocationFloat = New DevExpress.Utils.PointFloat(473.965!, 53.0!)
        Me.XrLabel10.Name = "XrLabel10"
        Me.XrLabel10.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLabel10.SizeF = New System.Drawing.SizeF(266.0348!, 18.0!)
        Me.XrLabel10.StylePriority.UseFont = False
        Me.XrLabel10.Text = "XrLabel10"
        '
        'XrLabel11
        '
        Me.XrLabel11.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "M_WAREHOUSE.NAME_DISPLAY")})
        Me.XrLabel11.Font = New System.Drawing.Font("Arial", 9.0!)
        Me.XrLabel11.LocationFloat = New DevExpress.Utils.PointFloat(473.965!, 71.0!)
        Me.XrLabel11.Name = "XrLabel11"
        Me.XrLabel11.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLabel11.SizeF = New System.Drawing.SizeF(266.0348!, 18.0!)
        Me.XrLabel11.StylePriority.UseFont = False
        Me.XrLabel11.Text = "XrLabel11"
        '
        'sSEQ
        '
        Me.sSEQ.DataMember = "I_OPNAME_Ds"
        Me.sSEQ.Expression = "[SEQ]  +  1"
        Me.sSEQ.FieldType = DevExpress.XtraReports.UI.FieldType.[String]
        Me.sSEQ.Name = "sSEQ"
        '
        'XrLabel13
        '
        Me.XrLabel13.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "I_OPNAME_Ds.sSEQ")})
        Me.XrLabel13.Font = New System.Drawing.Font("Arial", 9.0!)
        Me.XrLabel13.LocationFloat = New DevExpress.Utils.PointFloat(10.00001!, 0.0!)
        Me.XrLabel13.Name = "XrLabel13"
        Me.XrLabel13.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLabel13.SizeF = New System.Drawing.SizeF(36.45833!, 18.0!)
        Me.XrLabel13.StylePriority.UseFont = False
        Me.XrLabel13.StylePriority.UseTextAlignment = False
        Me.XrLabel13.Text = "XrLabel13"
        Me.XrLabel13.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight
        '
        'DetailReport
        '
        Me.DetailReport.Bands.AddRange(New DevExpress.XtraReports.UI.Band() {Me.Detail1, Me.GroupHeader1, Me.GroupFooter1})
        Me.DetailReport.DataMember = "I_OPNAME_Ds"
        Me.DetailReport.DataSource = Me.bindingSource
        Me.DetailReport.Level = 0
        Me.DetailReport.Name = "DetailReport"
        '
        'Detail1
        '
        Me.Detail1.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.XrLabel19, Me.XrLabel31, Me.XrLabel23, Me.XrLabel13, Me.XrLabel22})
        Me.Detail1.HeightF = 18.0!
        Me.Detail1.Name = "Detail1"
        Me.Detail1.SortFields.AddRange(New DevExpress.XtraReports.UI.GroupField() {New DevExpress.XtraReports.UI.GroupField("SEQ", DevExpress.XtraReports.UI.XRColumnSortOrder.Ascending)})
        '
        'XrLabel19
        '
        Me.XrLabel19.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "I_OPNAME_Ds.REMARKS")})
        Me.XrLabel19.Font = New System.Drawing.Font("Arial", 9.0!)
        Me.XrLabel19.LocationFloat = New DevExpress.Utils.PointFloat(530.2117!, 0.0!)
        Me.XrLabel19.Name = "XrLabel19"
        Me.XrLabel19.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLabel19.SizeF = New System.Drawing.SizeF(209.788!, 18.0!)
        Me.XrLabel19.StylePriority.UseFont = False
        Me.XrLabel19.StylePriority.UseTextAlignment = False
        Me.XrLabel19.Text = "XrLabel19"
        Me.XrLabel19.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft
        '
        'XrLabel31
        '
        Me.XrLabel31.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "I_OPNAME_Ds.M_UOM.MEMO")})
        Me.XrLabel31.Font = New System.Drawing.Font("Arial", 9.0!)
        Me.XrLabel31.LocationFloat = New DevExpress.Utils.PointFloat(460.2118!, 0.0!)
        Me.XrLabel31.Name = "XrLabel31"
        Me.XrLabel31.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLabel31.SizeF = New System.Drawing.SizeF(69.99982!, 18.0!)
        Me.XrLabel31.StylePriority.UseFont = False
        Me.XrLabel31.StylePriority.UseTextAlignment = False
        Me.XrLabel31.Text = "XrLabel31"
        Me.XrLabel31.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft
        '
        'XrLabel23
        '
        Me.XrLabel23.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "I_OPNAME_Ds.QTY", "{0:n2}")})
        Me.XrLabel23.Font = New System.Drawing.Font("Arial", 9.0!)
        Me.XrLabel23.LocationFloat = New DevExpress.Utils.PointFloat(380.2116!, 0.0!)
        Me.XrLabel23.Name = "XrLabel23"
        Me.XrLabel23.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLabel23.SizeF = New System.Drawing.SizeF(80.00018!, 18.0!)
        Me.XrLabel23.StylePriority.UseFont = False
        Me.XrLabel23.StylePriority.UseTextAlignment = False
        Me.XrLabel23.Text = "XrLabel23"
        Me.XrLabel23.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight
        '
        'XrLabel22
        '
        Me.XrLabel22.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "I_OPNAME_Ds.M_ITEM.NMITEM2")})
        Me.XrLabel22.Font = New System.Drawing.Font("Arial", 9.0!)
        Me.XrLabel22.LocationFloat = New DevExpress.Utils.PointFloat(46.45834!, 0.0!)
        Me.XrLabel22.Name = "XrLabel22"
        Me.XrLabel22.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLabel22.SizeF = New System.Drawing.SizeF(333.7533!, 18.0!)
        Me.XrLabel22.StylePriority.UseFont = False
        Me.XrLabel22.Text = "XrLabel22"
        '
        'GroupHeader1
        '
        Me.GroupHeader1.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.XrLabel18, Me.XrLabel17, Me.XrLabel16, Me.XrLabel15, Me.XrLabel14})
        Me.GroupHeader1.HeightF = 18.0!
        Me.GroupHeader1.Name = "GroupHeader1"
        '
        'XrLabel18
        '
        Me.XrLabel18.Borders = CType(((DevExpress.XtraPrinting.BorderSide.Left Or DevExpress.XtraPrinting.BorderSide.Top) _
            Or DevExpress.XtraPrinting.BorderSide.Bottom), DevExpress.XtraPrinting.BorderSide)
        Me.XrLabel18.Font = New System.Drawing.Font("Arial", 9.0!)
        Me.XrLabel18.LocationFloat = New DevExpress.Utils.PointFloat(530.2115!, 0.0!)
        Me.XrLabel18.Name = "XrLabel18"
        Me.XrLabel18.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLabel18.SizeF = New System.Drawing.SizeF(209.7881!, 18.0!)
        Me.XrLabel18.StylePriority.UseBorders = False
        Me.XrLabel18.StylePriority.UseFont = False
        Me.XrLabel18.StylePriority.UseTextAlignment = False
        Me.XrLabel18.Text = "CATATAN"
        Me.XrLabel18.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter
        '
        'XrLabel17
        '
        Me.XrLabel17.Borders = CType(((DevExpress.XtraPrinting.BorderSide.Left Or DevExpress.XtraPrinting.BorderSide.Top) _
            Or DevExpress.XtraPrinting.BorderSide.Bottom), DevExpress.XtraPrinting.BorderSide)
        Me.XrLabel17.Font = New System.Drawing.Font("Arial", 9.0!)
        Me.XrLabel17.LocationFloat = New DevExpress.Utils.PointFloat(460.2118!, 0.0!)
        Me.XrLabel17.Name = "XrLabel17"
        Me.XrLabel17.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLabel17.SizeF = New System.Drawing.SizeF(69.99966!, 18.0!)
        Me.XrLabel17.StylePriority.UseBorders = False
        Me.XrLabel17.StylePriority.UseFont = False
        Me.XrLabel17.StylePriority.UseTextAlignment = False
        Me.XrLabel17.Text = "STN"
        Me.XrLabel17.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter
        '
        'XrLabel16
        '
        Me.XrLabel16.Borders = CType(((DevExpress.XtraPrinting.BorderSide.Left Or DevExpress.XtraPrinting.BorderSide.Top) _
            Or DevExpress.XtraPrinting.BorderSide.Bottom), DevExpress.XtraPrinting.BorderSide)
        Me.XrLabel16.Font = New System.Drawing.Font("Arial", 9.0!)
        Me.XrLabel16.LocationFloat = New DevExpress.Utils.PointFloat(380.2116!, 0.0!)
        Me.XrLabel16.Name = "XrLabel16"
        Me.XrLabel16.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLabel16.SizeF = New System.Drawing.SizeF(80.00012!, 18.0!)
        Me.XrLabel16.StylePriority.UseBorders = False
        Me.XrLabel16.StylePriority.UseFont = False
        Me.XrLabel16.StylePriority.UseTextAlignment = False
        Me.XrLabel16.Text = "JUMLAH"
        Me.XrLabel16.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter
        '
        'XrLabel15
        '
        Me.XrLabel15.Borders = CType(((DevExpress.XtraPrinting.BorderSide.Left Or DevExpress.XtraPrinting.BorderSide.Top) _
            Or DevExpress.XtraPrinting.BorderSide.Bottom), DevExpress.XtraPrinting.BorderSide)
        Me.XrLabel15.Font = New System.Drawing.Font("Arial", 9.0!)
        Me.XrLabel15.LocationFloat = New DevExpress.Utils.PointFloat(46.45834!, 0.0!)
        Me.XrLabel15.Name = "XrLabel15"
        Me.XrLabel15.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLabel15.SizeF = New System.Drawing.SizeF(333.7533!, 18.0!)
        Me.XrLabel15.StylePriority.UseBorders = False
        Me.XrLabel15.StylePriority.UseFont = False
        Me.XrLabel15.StylePriority.UseTextAlignment = False
        Me.XrLabel15.Text = "NAMA BARANG"
        Me.XrLabel15.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter
        '
        'XrLabel14
        '
        Me.XrLabel14.Borders = CType(((DevExpress.XtraPrinting.BorderSide.Left Or DevExpress.XtraPrinting.BorderSide.Top) _
            Or DevExpress.XtraPrinting.BorderSide.Bottom), DevExpress.XtraPrinting.BorderSide)
        Me.XrLabel14.Font = New System.Drawing.Font("Arial", 9.0!)
        Me.XrLabel14.LocationFloat = New DevExpress.Utils.PointFloat(10.00001!, 0.0!)
        Me.XrLabel14.Name = "XrLabel14"
        Me.XrLabel14.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLabel14.SizeF = New System.Drawing.SizeF(36.45833!, 18.0!)
        Me.XrLabel14.StylePriority.UseBorders = False
        Me.XrLabel14.StylePriority.UseFont = False
        Me.XrLabel14.StylePriority.UseTextAlignment = False
        Me.XrLabel14.Text = "NO."
        Me.XrLabel14.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter
        '
        'GroupFooter1
        '
        Me.GroupFooter1.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.XrLabel48, Me.XrPageInfo1, Me.XrLabel47})
        Me.GroupFooter1.HeightF = 24.20832!
        Me.GroupFooter1.KeepTogether = True
        Me.GroupFooter1.Name = "GroupFooter1"
        Me.GroupFooter1.PrintAtBottom = True
        Me.GroupFooter1.RepeatEveryPage = True
        '
        'XrLabel48
        '
        Me.XrLabel48.Borders = CType((DevExpress.XtraPrinting.BorderSide.Top Or DevExpress.XtraPrinting.BorderSide.Bottom), DevExpress.XtraPrinting.BorderSide)
        Me.XrLabel48.Font = New System.Drawing.Font("Arial", 9.0!)
        Me.XrLabel48.LocationFloat = New DevExpress.Utils.PointFloat(460.2118!, 0.0!)
        Me.XrLabel48.Name = "XrLabel48"
        Me.XrLabel48.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLabel48.SizeF = New System.Drawing.SizeF(279.7879!, 19.0!)
        Me.XrLabel48.StylePriority.UseBorders = False
        Me.XrLabel48.StylePriority.UseFont = False
        Me.XrLabel48.StylePriority.UseTextAlignment = False
        XrSummary1.Func = DevExpress.XtraReports.UI.SummaryFunc.Custom
        Me.XrLabel48.Summary = XrSummary1
        Me.XrLabel48.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight
        '
        'XrPageInfo1
        '
        Me.XrPageInfo1.Borders = CType((DevExpress.XtraPrinting.BorderSide.Top Or DevExpress.XtraPrinting.BorderSide.Bottom), DevExpress.XtraPrinting.BorderSide)
        Me.XrPageInfo1.Font = New System.Drawing.Font("Arial", 9.0!)
        Me.XrPageInfo1.Format = "SUBTOTAL {0}"
        Me.XrPageInfo1.LocationFloat = New DevExpress.Utils.PointFloat(10.00005!, 0.0!)
        Me.XrPageInfo1.Name = "XrPageInfo1"
        Me.XrPageInfo1.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrPageInfo1.PageInfo = DevExpress.XtraPrinting.PageInfo.Number
        Me.XrPageInfo1.SizeF = New System.Drawing.SizeF(370.2115!, 19.0!)
        Me.XrPageInfo1.StylePriority.UseBorders = False
        Me.XrPageInfo1.StylePriority.UseFont = False
        Me.XrPageInfo1.StylePriority.UseTextAlignment = False
        Me.XrPageInfo1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight
        '
        'XrLabel47
        '
        Me.XrLabel47.Borders = CType((DevExpress.XtraPrinting.BorderSide.Top Or DevExpress.XtraPrinting.BorderSide.Bottom), DevExpress.XtraPrinting.BorderSide)
        Me.XrLabel47.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "I_OPNAME_Ds.QTY")})
        Me.XrLabel47.Font = New System.Drawing.Font("Arial", 9.0!)
        Me.XrLabel47.LocationFloat = New DevExpress.Utils.PointFloat(380.2116!, 0.0!)
        Me.XrLabel47.Name = "XrLabel47"
        Me.XrLabel47.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLabel47.SizeF = New System.Drawing.SizeF(80.00018!, 18.0!)
        Me.XrLabel47.StylePriority.UseBorders = False
        Me.XrLabel47.StylePriority.UseFont = False
        Me.XrLabel47.StylePriority.UseTextAlignment = False
        XrSummary2.FormatString = "{0:n2}"
        XrSummary2.Running = DevExpress.XtraReports.UI.SummaryRunning.Page
        Me.XrLabel47.Summary = XrSummary2
        Me.XrLabel47.Text = "XrLabel47"
        Me.XrLabel47.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight
        '
        'XrLabel44
        '
        Me.XrLabel44.Borders = CType((((DevExpress.XtraPrinting.BorderSide.Left Or DevExpress.XtraPrinting.BorderSide.Top) _
            Or DevExpress.XtraPrinting.BorderSide.Right) _
            Or DevExpress.XtraPrinting.BorderSide.Bottom), DevExpress.XtraPrinting.BorderSide)
        Me.XrLabel44.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "KDUSER")})
        Me.XrLabel44.Font = New System.Drawing.Font("Arial", 9.0!)
        Me.XrLabel44.LocationFloat = New DevExpress.Utils.PointFloat(10.00002!, 24.58331!)
        Me.XrLabel44.Name = "XrLabel44"
        Me.XrLabel44.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLabel44.SizeF = New System.Drawing.SizeF(100.0!, 47.41669!)
        Me.XrLabel44.StylePriority.UseBorders = False
        Me.XrLabel44.StylePriority.UseFont = False
        Me.XrLabel44.StylePriority.UseTextAlignment = False
        Me.XrLabel44.Text = "XrLabel44"
        Me.XrLabel44.TextAlignment = DevExpress.XtraPrinting.TextAlignment.BottomCenter
        '
        'XrLabel29
        '
        Me.XrLabel29.Borders = CType(((DevExpress.XtraPrinting.BorderSide.Top Or DevExpress.XtraPrinting.BorderSide.Right) _
            Or DevExpress.XtraPrinting.BorderSide.Bottom), DevExpress.XtraPrinting.BorderSide)
        Me.XrLabel29.Font = New System.Drawing.Font("Arial", 9.0!)
        Me.XrLabel29.LocationFloat = New DevExpress.Utils.PointFloat(110.0!, 24.58331!)
        Me.XrLabel29.Name = "XrLabel29"
        Me.XrLabel29.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLabel29.SizeF = New System.Drawing.SizeF(100.0!, 47.41669!)
        Me.XrLabel29.StylePriority.UseBorders = False
        Me.XrLabel29.StylePriority.UseFont = False
        Me.XrLabel29.StylePriority.UseTextAlignment = False
        Me.XrLabel29.Text = "GUDANG"
        Me.XrLabel29.TextAlignment = DevExpress.XtraPrinting.TextAlignment.BottomCenter
        '
        'XrLabel43
        '
        Me.XrLabel43.Borders = DevExpress.XtraPrinting.BorderSide.None
        Me.XrLabel43.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold)
        Me.XrLabel43.LocationFloat = New DevExpress.Utils.PointFloat(10.00002!, 0.0!)
        Me.XrLabel43.Name = "XrLabel43"
        Me.XrLabel43.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLabel43.SizeF = New System.Drawing.SizeF(50.0!, 18.0!)
        Me.XrLabel43.StylePriority.UseBorders = False
        Me.XrLabel43.StylePriority.UseFont = False
        Me.XrLabel43.StylePriority.UseTextAlignment = False
        Me.XrLabel43.Text = "KET :"
        Me.XrLabel43.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft
        '
        'XrLabel46
        '
        Me.XrLabel46.CanShrink = True
        Me.XrLabel46.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "MEMO")})
        Me.XrLabel46.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold)
        Me.XrLabel46.LocationFloat = New DevExpress.Utils.PointFloat(60.00004!, 0.0!)
        Me.XrLabel46.Multiline = True
        Me.XrLabel46.Name = "XrLabel46"
        Me.XrLabel46.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLabel46.SizeF = New System.Drawing.SizeF(530.0002!, 18.0!)
        Me.XrLabel46.StylePriority.UseFont = False
        Me.XrLabel46.Text = "XrLabel46"
        '
        'ReportFooter
        '
        Me.ReportFooter.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.XrLabel43, Me.XrLabel44, Me.XrLabel29, Me.XrLabel46})
        Me.ReportFooter.HeightF = 80.91666!
        Me.ReportFooter.KeepTogether = True
        Me.ReportFooter.Name = "ReportFooter"
        Me.ReportFooter.PrintAtBottom = True
        '
        'ReportHeader
        '
        Me.ReportHeader.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.lNPWP, Me.lPHONE, Me.lADDRESS, Me.lblREPORT, Me.XrLabel1, Me.XrLabel2, Me.XrLabel3, Me.XrLabel5, Me.XrLabel6, Me.XrLabel7, Me.XrLabel9, Me.XrLabel10, Me.XrLabel11, Me.lCOMPANY})
        Me.ReportHeader.HeightF = 96.87497!
        Me.ReportHeader.Name = "ReportHeader"
        '
        'lNPWP
        '
        Me.lNPWP.CanShrink = True
        Me.lNPWP.Font = New System.Drawing.Font("Arial", 9.0!)
        Me.lNPWP.LocationFloat = New DevExpress.Utils.PointFloat(10.00001!, 71.0!)
        Me.lNPWP.Name = "lNPWP"
        Me.lNPWP.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.lNPWP.SizeF = New System.Drawing.SizeF(350.6248!, 18.0!)
        Me.lNPWP.StylePriority.UseFont = False
        Me.lNPWP.Text = "lNPWP"
        '
        'lPHONE
        '
        Me.lPHONE.CanShrink = True
        Me.lPHONE.Font = New System.Drawing.Font("Arial", 9.0!)
        Me.lPHONE.LocationFloat = New DevExpress.Utils.PointFloat(10.00023!, 53.0!)
        Me.lPHONE.Name = "lPHONE"
        Me.lPHONE.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.lPHONE.SizeF = New System.Drawing.SizeF(350.6248!, 18.0!)
        Me.lPHONE.StylePriority.UseFont = False
        Me.lPHONE.Text = "lPHONE"
        '
        'lADDRESS
        '
        Me.lADDRESS.CanShrink = True
        Me.lADDRESS.Font = New System.Drawing.Font("Arial", 9.0!)
        Me.lADDRESS.LocationFloat = New DevExpress.Utils.PointFloat(10.00001!, 35.00001!)
        Me.lADDRESS.Name = "lADDRESS"
        Me.lADDRESS.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.lADDRESS.SizeF = New System.Drawing.SizeF(350.6248!, 18.0!)
        Me.lADDRESS.StylePriority.UseFont = False
        Me.lADDRESS.Text = "lADDRESS"
        '
        'xtraOpname
        '
        Me.Bands.AddRange(New DevExpress.XtraReports.UI.Band() {Me.Detail, Me.TopMargin, Me.BottomMargin, Me.DetailReport, Me.ReportFooter, Me.ReportHeader})
        Me.CalculatedFields.AddRange(New DevExpress.XtraReports.UI.CalculatedField() {Me.sSEQ})
        Me.DataSource = Me.bindingSource
        Me.Margins = New System.Drawing.Printing.Margins(50, 50, 50, 50)
        Me.PageHeight = 550
        Me.PaperKind = System.Drawing.Printing.PaperKind.Custom
        Me.Version = "12.1"
        CType(Me.bindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()

    End Sub
    Friend WithEvents Detail As DevExpress.XtraReports.UI.DetailBand
    Friend WithEvents TopMargin As DevExpress.XtraReports.UI.TopMarginBand
    Friend WithEvents BottomMargin As DevExpress.XtraReports.UI.BottomMarginBand
    Friend WithEvents lCOMPANY As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents lblREPORT As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel2 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel1 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel3 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel7 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel6 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel5 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents bindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents XrLabel9 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel10 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel11 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents sSEQ As DevExpress.XtraReports.UI.CalculatedField
    Friend WithEvents XrLabel13 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents DetailReport As DevExpress.XtraReports.UI.DetailReportBand
    Friend WithEvents Detail1 As DevExpress.XtraReports.UI.DetailBand
    Friend WithEvents GroupHeader1 As DevExpress.XtraReports.UI.GroupHeaderBand
    Friend WithEvents XrLabel14 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel15 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel16 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel17 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel22 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel23 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel29 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel44 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel31 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel43 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel46 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrPageInfo1 As DevExpress.XtraReports.UI.XRPageInfo
    Friend WithEvents ReportFooter As DevExpress.XtraReports.UI.ReportFooterBand
    Friend WithEvents GroupFooter1 As DevExpress.XtraReports.UI.GroupFooterBand
    Friend WithEvents ReportHeader As DevExpress.XtraReports.UI.ReportHeaderBand
    Friend WithEvents XrLabel47 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel48 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrPageInfo2 As DevExpress.XtraReports.UI.XRPageInfo
    Friend WithEvents lADDRESS As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents lNPWP As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents lPHONE As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel18 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel19 As DevExpress.XtraReports.UI.XRLabel
End Class
