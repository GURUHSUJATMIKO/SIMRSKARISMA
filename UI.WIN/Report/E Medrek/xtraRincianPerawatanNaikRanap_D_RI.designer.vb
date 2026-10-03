<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Public Class xtraRincianPerawatanNaikRanap_D_RI
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
        Me.XrLabel57 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel56 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel55 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel54 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel53 = New DevExpress.XtraReports.UI.XRLabel()
        Me.TopMargin = New DevExpress.XtraReports.UI.TopMarginBand()
        Me.BottomMargin = New DevExpress.XtraReports.UI.BottomMarginBand()
        Me.XrControlStyle1 = New DevExpress.XtraReports.UI.XRControlStyle()
        Me.sNAMA = New DevExpress.XtraReports.UI.CalculatedField()
        Me.sKategori = New DevExpress.XtraReports.UI.CalculatedField()
        Me.sSisa = New DevExpress.XtraReports.UI.CalculatedField()
        Me.XrLabel58 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel59 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel60 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel61 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel62 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel63 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel64 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel65 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel73 = New DevExpress.XtraReports.UI.XRLabel()
        Me.sKategori_D_RI = New DevExpress.XtraReports.UI.CalculatedField()
        Me.sKategori_D = New DevExpress.XtraReports.UI.CalculatedField()
        Me.ReportHeader = New DevExpress.XtraReports.UI.ReportHeaderBand()
        Me.GroupHeader3 = New DevExpress.XtraReports.UI.GroupHeaderBand()
        Me.GroupHeader2 = New DevExpress.XtraReports.UI.GroupHeaderBand()
        Me.GroupFooter2 = New DevExpress.XtraReports.UI.GroupFooterBand()
        Me.XrLabel1 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel2 = New DevExpress.XtraReports.UI.XRLabel()
        Me.bindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.ReportFooter = New DevExpress.XtraReports.UI.ReportFooterBand()
        CType(Me.bindingSource,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me,System.ComponentModel.ISupportInitialize).BeginInit
        '
        'Detail
        '
        Me.Detail.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.XrLabel57, Me.XrLabel56, Me.XrLabel55, Me.XrLabel54, Me.XrLabel53})
        Me.Detail.HeightF = 18.47222!
        Me.Detail.Name = "Detail"
        Me.Detail.Padding = New DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100!)
        Me.Detail.SortFields.AddRange(New DevExpress.XtraReports.UI.GroupField() {New DevExpress.XtraReports.UI.GroupField("TANGGAL", DevExpress.XtraReports.UI.XRColumnSortOrder.Ascending)})
        Me.Detail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft
        '
        'XrLabel57
        '
        Me.XrLabel57.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "TANGGAL", "{0:dd/MM/yyyy}")})
        Me.XrLabel57.Font = New System.Drawing.Font("Arial", 8!)
        Me.XrLabel57.LocationFloat = New DevExpress.Utils.PointFloat(0.0001271566!, 0!)
        Me.XrLabel57.Name = "XrLabel57"
        Me.XrLabel57.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100!)
        Me.XrLabel57.SizeF = New System.Drawing.SizeF(84.55886!, 18.47222!)
        Me.XrLabel57.StylePriority.UseFont = false
        Me.XrLabel57.StylePriority.UseTextAlignment = false
        Me.XrLabel57.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft
        '
        'XrLabel56
        '
        Me.XrLabel56.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "NAMATARIF")})
        Me.XrLabel56.Font = New System.Drawing.Font("Arial", 8!)
        Me.XrLabel56.LocationFloat = New DevExpress.Utils.PointFloat(94.97566!, 0!)
        Me.XrLabel56.Name = "XrLabel56"
        Me.XrLabel56.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100!)
        Me.XrLabel56.SizeF = New System.Drawing.SizeF(373.7672!, 18.47222!)
        Me.XrLabel56.StylePriority.UseFont = false
        Me.XrLabel56.StylePriority.UseTextAlignment = false
        Me.XrLabel56.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft
        '
        'XrLabel55
        '
        Me.XrLabel55.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "QTY", "{0:n2}")})
        Me.XrLabel55.Font = New System.Drawing.Font("Arial", 8!)
        Me.XrLabel55.LocationFloat = New DevExpress.Utils.PointFloat(468.7429!, 0!)
        Me.XrLabel55.Name = "XrLabel55"
        Me.XrLabel55.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100!)
        Me.XrLabel55.SizeF = New System.Drawing.SizeF(69.59995!, 18.47222!)
        Me.XrLabel55.StylePriority.UseFont = false
        Me.XrLabel55.StylePriority.UseTextAlignment = false
        Me.XrLabel55.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight
        '
        'XrLabel54
        '
        Me.XrLabel54.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "PRICE", "{0:n0}")})
        Me.XrLabel54.Font = New System.Drawing.Font("Arial", 8!)
        Me.XrLabel54.LocationFloat = New DevExpress.Utils.PointFloat(538.3429!, 0!)
        Me.XrLabel54.Name = "XrLabel54"
        Me.XrLabel54.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100!)
        Me.XrLabel54.SizeF = New System.Drawing.SizeF(100.275!, 18.47222!)
        Me.XrLabel54.StylePriority.UseFont = false
        Me.XrLabel54.StylePriority.UseTextAlignment = false
        Me.XrLabel54.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight
        '
        'XrLabel53
        '
        Me.XrLabel53.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "SUBTOTAL", "{0:n0}")})
        Me.XrLabel53.Font = New System.Drawing.Font("Arial", 8!)
        Me.XrLabel53.LocationFloat = New DevExpress.Utils.PointFloat(649.0419!, 0!)
        Me.XrLabel53.Name = "XrLabel53"
        Me.XrLabel53.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100!)
        Me.XrLabel53.SizeF = New System.Drawing.SizeF(127.9584!, 18.47222!)
        Me.XrLabel53.StylePriority.UseFont = false
        Me.XrLabel53.StylePriority.UseTextAlignment = false
        Me.XrLabel53.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight
        '
        'TopMargin
        '
        Me.TopMargin.Font = New System.Drawing.Font("Times New Roman", 9.75!)
        Me.TopMargin.HeightF = 0!
        Me.TopMargin.Name = "TopMargin"
        Me.TopMargin.Padding = New DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100!)
        Me.TopMargin.StylePriority.UseFont = false
        Me.TopMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft
        '
        'BottomMargin
        '
        Me.BottomMargin.HeightF = 0!
        Me.BottomMargin.Name = "BottomMargin"
        Me.BottomMargin.Padding = New DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100!)
        Me.BottomMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft
        '
        'XrControlStyle1
        '
        Me.XrControlStyle1.Name = "XrControlStyle1"
        Me.XrControlStyle1.Padding = New DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100!)
        '
        'sNAMA
        '
        Me.sNAMA.Expression = "[NAMATARIF] + '(' + [NAMADOKTER] + ')'"
        Me.sNAMA.FieldType = DevExpress.XtraReports.UI.FieldType.[String]
        Me.sNAMA.Name = "sNAMA"
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
        'XrLabel58
        '
        Me.XrLabel58.Font = New System.Drawing.Font("Arial", 8!)
        Me.XrLabel58.LocationFloat = New DevExpress.Utils.PointFloat(649.0347!, 0!)
        Me.XrLabel58.Name = "XrLabel58"
        Me.XrLabel58.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100!)
        Me.XrLabel58.SizeF = New System.Drawing.SizeF(127.9584!, 18.47222!)
        Me.XrLabel58.StylePriority.UseFont = false
        Me.XrLabel58.StylePriority.UseTextAlignment = false
        Me.XrLabel58.Text = "Total (Rp.)"
        Me.XrLabel58.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft
        '
        'XrLabel59
        '
        Me.XrLabel59.Font = New System.Drawing.Font("Arial", 8!)
        Me.XrLabel59.LocationFloat = New DevExpress.Utils.PointFloat(538.3358!, 0!)
        Me.XrLabel59.Name = "XrLabel59"
        Me.XrLabel59.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100!)
        Me.XrLabel59.SizeF = New System.Drawing.SizeF(100.275!, 18.47222!)
        Me.XrLabel59.StylePriority.UseFont = false
        Me.XrLabel59.StylePriority.UseTextAlignment = false
        Me.XrLabel59.Text = "Tarif"
        Me.XrLabel59.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft
        '
        'XrLabel60
        '
        Me.XrLabel60.Font = New System.Drawing.Font("Arial", 8!)
        Me.XrLabel60.LocationFloat = New DevExpress.Utils.PointFloat(468.7357!, 0!)
        Me.XrLabel60.Name = "XrLabel60"
        Me.XrLabel60.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100!)
        Me.XrLabel60.SizeF = New System.Drawing.SizeF(69.59995!, 18.47222!)
        Me.XrLabel60.StylePriority.UseFont = false
        Me.XrLabel60.StylePriority.UseTextAlignment = false
        Me.XrLabel60.Text = "Jumlah"
        Me.XrLabel60.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft
        '
        'XrLabel61
        '
        Me.XrLabel61.Font = New System.Drawing.Font("Arial", 8!)
        Me.XrLabel61.LocationFloat = New DevExpress.Utils.PointFloat(94.96848!, 0!)
        Me.XrLabel61.Name = "XrLabel61"
        Me.XrLabel61.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100!)
        Me.XrLabel61.SizeF = New System.Drawing.SizeF(373.7672!, 18.47222!)
        Me.XrLabel61.StylePriority.UseFont = false
        Me.XrLabel61.StylePriority.UseTextAlignment = false
        Me.XrLabel61.Text = "Keterangan"
        Me.XrLabel61.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft
        '
        'XrLabel62
        '
        Me.XrLabel62.Font = New System.Drawing.Font("Arial", 8!)
        Me.XrLabel62.LocationFloat = New DevExpress.Utils.PointFloat(0.0001271566!, 0!)
        Me.XrLabel62.Name = "XrLabel62"
        Me.XrLabel62.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100!)
        Me.XrLabel62.SizeF = New System.Drawing.SizeF(84.5517!, 18.47222!)
        Me.XrLabel62.StylePriority.UseFont = false
        Me.XrLabel62.StylePriority.UseTextAlignment = false
        Me.XrLabel62.Text = "Tanggal"
        Me.XrLabel62.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft
        '
        'XrLabel63
        '
        Me.XrLabel63.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "KATEGORI")})
        Me.XrLabel63.Font = New System.Drawing.Font("Arial", 8!, System.Drawing.FontStyle.Bold)
        Me.XrLabel63.LocationFloat = New DevExpress.Utils.PointFloat(0!, 0!)
        Me.XrLabel63.Name = "XrLabel63"
        Me.XrLabel63.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100!)
        Me.XrLabel63.SizeF = New System.Drawing.SizeF(776.9931!, 19.16666!)
        Me.XrLabel63.StylePriority.UseFont = false
        Me.XrLabel63.StylePriority.UseTextAlignment = false
        Me.XrLabel63.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft
        '
        'XrLabel64
        '
        Me.XrLabel64.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "sKategori")})
        Me.XrLabel64.Font = New System.Drawing.Font("Arial", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.XrLabel64.LocationFloat = New DevExpress.Utils.PointFloat(361.1571!, 0!)
        Me.XrLabel64.Name = "XrLabel64"
        Me.XrLabel64.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100!)
        Me.XrLabel64.SizeF = New System.Drawing.SizeF(277.4533!, 18.47222!)
        Me.XrLabel64.StylePriority.UseFont = false
        Me.XrLabel64.StylePriority.UseTextAlignment = false
        Me.XrLabel64.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight
        '
        'XrLabel65
        '
        Me.XrLabel65.Borders = DevExpress.XtraPrinting.BorderSide.Top
        Me.XrLabel65.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "SUBTOTAL")})
        Me.XrLabel65.Font = New System.Drawing.Font("Arial", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.XrLabel65.LocationFloat = New DevExpress.Utils.PointFloat(649.0347!, 0!)
        Me.XrLabel65.Name = "XrLabel65"
        Me.XrLabel65.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100!)
        Me.XrLabel65.SizeF = New System.Drawing.SizeF(127.9584!, 18.47222!)
        Me.XrLabel65.StylePriority.UseBorders = false
        Me.XrLabel65.StylePriority.UseFont = false
        Me.XrLabel65.StylePriority.UseTextAlignment = false
        XrSummary1.FormatString = "{0:n0}"
        XrSummary1.Running = DevExpress.XtraReports.UI.SummaryRunning.Group
        Me.XrLabel65.Summary = XrSummary1
        Me.XrLabel65.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight
        '
        'XrLabel73
        '
        Me.XrLabel73.Font = New System.Drawing.Font("Arial", 8.5!, System.Drawing.FontStyle.Bold)
        Me.XrLabel73.LocationFloat = New DevExpress.Utils.PointFloat(0!, 4.861132!)
        Me.XrLabel73.Name = "XrLabel73"
        Me.XrLabel73.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100!)
        Me.XrLabel73.SizeF = New System.Drawing.SizeF(476.1631!, 18.4722!)
        Me.XrLabel73.StylePriority.UseFont = false
        Me.XrLabel73.StylePriority.UseTextAlignment = false
        Me.XrLabel73.Text = "Data Instalasi Rawat Inap"
        Me.XrLabel73.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft
        '
        'sKategori_D_RI
        '
        Me.sKategori_D_RI.DataMember = "R_RINCIAN_NAIKRANAP_D_RIs"
        Me.sKategori_D_RI.Expression = "'TOTAL ' + [KATEGORI] + ' Rp.'"
        Me.sKategori_D_RI.Name = "sKategori_D_RI"
        '
        'sKategori_D
        '
        Me.sKategori_D.DataMember = "R_RINCIAN_NAIKRANAP_Ds"
        Me.sKategori_D.Expression = "'TOTAL ' + [KATEGORI] + ' Rp.'"
        Me.sKategori_D.Name = "sKategori_D"
        '
        'ReportHeader
        '
        Me.ReportHeader.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.XrLabel73})
        Me.ReportHeader.HeightF = 28.125!
        Me.ReportHeader.Name = "ReportHeader"
        '
        'GroupHeader3
        '
        Me.GroupHeader3.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.XrLabel63})
        Me.GroupHeader3.GroupFields.AddRange(New DevExpress.XtraReports.UI.GroupField() {New DevExpress.XtraReports.UI.GroupField("KATEGORI", DevExpress.XtraReports.UI.XRColumnSortOrder.Ascending)})
        Me.GroupHeader3.HeightF = 19.16666!
        Me.GroupHeader3.Level = 1
        Me.GroupHeader3.Name = "GroupHeader3"
        '
        'GroupHeader2
        '
        Me.GroupHeader2.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.XrLabel58, Me.XrLabel61, Me.XrLabel60, Me.XrLabel59, Me.XrLabel62})
        Me.GroupHeader2.HeightF = 18.47222!
        Me.GroupHeader2.Name = "GroupHeader2"
        '
        'GroupFooter2
        '
        Me.GroupFooter2.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.XrLabel65, Me.XrLabel64})
        Me.GroupFooter2.HeightF = 18.47222!
        Me.GroupFooter2.Name = "GroupFooter2"
        '
        'XrLabel1
        '
        Me.XrLabel1.Font = New System.Drawing.Font("Arial", 8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.XrLabel1.LocationFloat = New DevExpress.Utils.PointFloat(361.1571!, 10.00001!)
        Me.XrLabel1.Name = "XrLabel1"
        Me.XrLabel1.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100!)
        Me.XrLabel1.SizeF = New System.Drawing.SizeF(277.4608!, 18.47222!)
        Me.XrLabel1.StylePriority.UseFont = false
        Me.XrLabel1.StylePriority.UseTextAlignment = false
        Me.XrLabel1.Text = "Total Rawat Inap :"
        Me.XrLabel1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight
        '
        'XrLabel2
        '
        Me.XrLabel2.Borders = DevExpress.XtraPrinting.BorderSide.Top
        Me.XrLabel2.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "SUBTOTAL")})
        Me.XrLabel2.Font = New System.Drawing.Font("Arial", 8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.XrLabel2.LocationFloat = New DevExpress.Utils.PointFloat(649.0419!, 10.00001!)
        Me.XrLabel2.Name = "XrLabel2"
        Me.XrLabel2.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100!)
        Me.XrLabel2.SizeF = New System.Drawing.SizeF(127.9584!, 18.47222!)
        Me.XrLabel2.StylePriority.UseBorders = false
        Me.XrLabel2.StylePriority.UseFont = false
        Me.XrLabel2.StylePriority.UseTextAlignment = false
        XrSummary2.FormatString = "{0:n0}"
        XrSummary2.Running = DevExpress.XtraReports.UI.SummaryRunning.Report
        Me.XrLabel2.Summary = XrSummary2
        Me.XrLabel2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight
        '
        'bindingSource
        '
        Me.bindingSource.DataSource = GetType(DataAccess.R_RINCIAN_NAIKRANAP_D_RI)
        '
        'ReportFooter
        '
        Me.ReportFooter.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.XrLabel2, Me.XrLabel1})
        Me.ReportFooter.HeightF = 28.47223!
        Me.ReportFooter.Name = "ReportFooter"
        Me.ReportFooter.PrintAtBottom = true
        '
        'xtraRincianPerawatanNaikRanap_D_RI
        '
        Me.Bands.AddRange(New DevExpress.XtraReports.UI.Band() {Me.Detail, Me.TopMargin, Me.BottomMargin, Me.ReportHeader, Me.GroupHeader3, Me.GroupHeader2, Me.GroupFooter2, Me.ReportFooter})
        Me.CalculatedFields.AddRange(New DevExpress.XtraReports.UI.CalculatedField() {Me.sNAMA, Me.sKategori, Me.sSisa, Me.sKategori_D_RI, Me.sKategori_D})
        Me.DataSource = Me.bindingSource
        Me.Margins = New System.Drawing.Printing.Margins(22, 22, 0, 0)
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
    Friend WithEvents sKategori As DevExpress.XtraReports.UI.CalculatedField
    Friend WithEvents sSisa As DevExpress.XtraReports.UI.CalculatedField
    Friend WithEvents XrLabel53 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel54 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel55 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel56 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel57 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel58 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel59 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel60 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel61 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel62 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel63 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel64 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel65 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel73 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents sKategori_D_RI As DevExpress.XtraReports.UI.CalculatedField
    Friend WithEvents sKategori_D As DevExpress.XtraReports.UI.CalculatedField
    Friend WithEvents ReportHeader As DevExpress.XtraReports.UI.ReportHeaderBand
    Friend WithEvents GroupHeader3 As DevExpress.XtraReports.UI.GroupHeaderBand
    Friend WithEvents GroupFooter2 As DevExpress.XtraReports.UI.GroupFooterBand
    Friend WithEvents GroupHeader2 As DevExpress.XtraReports.UI.GroupHeaderBand
    Friend WithEvents XrLabel1 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel2 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents ReportFooter As DevExpress.XtraReports.UI.ReportFooterBand
End Class
