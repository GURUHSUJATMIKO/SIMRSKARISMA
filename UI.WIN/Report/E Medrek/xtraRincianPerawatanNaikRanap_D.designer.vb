<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Public Class xtraRincianPerawatanNaikRanap_D
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
        Me.XrLabel40 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel45 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel42 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel41 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel46 = New DevExpress.XtraReports.UI.XRLabel()
        Me.TopMargin = New DevExpress.XtraReports.UI.TopMarginBand()
        Me.BottomMargin = New DevExpress.XtraReports.UI.BottomMarginBand()
        Me.XrControlStyle1 = New DevExpress.XtraReports.UI.XRControlStyle()
        Me.sNAMA = New DevExpress.XtraReports.UI.CalculatedField()
        Me.sKategori = New DevExpress.XtraReports.UI.CalculatedField()
        Me.sSisa = New DevExpress.XtraReports.UI.CalculatedField()
        Me.XrLabel22 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel27 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel37 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel38 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel39 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel44 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel10 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel70 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel71 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel72 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel67 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel68 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel69 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel66 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel29 = New DevExpress.XtraReports.UI.XRLabel()
        Me.sKategori_D_RI = New DevExpress.XtraReports.UI.CalculatedField()
        Me.sKategori_D = New DevExpress.XtraReports.UI.CalculatedField()
        Me.GroupHeader2 = New DevExpress.XtraReports.UI.GroupHeaderBand()
        Me.GroupFooter1 = New DevExpress.XtraReports.UI.GroupFooterBand()
        Me.ReportHeader = New DevExpress.XtraReports.UI.ReportHeaderBand()
        Me.XrLabel1 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel2 = New DevExpress.XtraReports.UI.XRLabel()
        Me.bindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.ReportFooter = New DevExpress.XtraReports.UI.ReportFooterBand()
        Me.GroupHeader3 = New DevExpress.XtraReports.UI.GroupHeaderBand()
        CType(Me.bindingSource,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me,System.ComponentModel.ISupportInitialize).BeginInit
        '
        'Detail
        '
        Me.Detail.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.XrLabel40, Me.XrLabel45, Me.XrLabel42, Me.XrLabel41, Me.XrLabel46})
        Me.Detail.HeightF = 18.47222!
        Me.Detail.Name = "Detail"
        Me.Detail.Padding = New DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100!)
        Me.Detail.SortFields.AddRange(New DevExpress.XtraReports.UI.GroupField() {New DevExpress.XtraReports.UI.GroupField("TANGGAL", DevExpress.XtraReports.UI.XRColumnSortOrder.Ascending)})
        Me.Detail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft
        '
        'XrLabel40
        '
        Me.XrLabel40.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "TANGGAL", "{0:dd/MM/yyyy}")})
        Me.XrLabel40.Font = New System.Drawing.Font("Arial", 8!)
        Me.XrLabel40.LocationFloat = New DevExpress.Utils.PointFloat(3.007038!, 0!)
        Me.XrLabel40.Name = "XrLabel40"
        Me.XrLabel40.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100!)
        Me.XrLabel40.SizeF = New System.Drawing.SizeF(84.55886!, 18.47222!)
        Me.XrLabel40.StylePriority.UseFont = false
        Me.XrLabel40.StylePriority.UseTextAlignment = false
        Me.XrLabel40.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft
        '
        'XrLabel45
        '
        Me.XrLabel45.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "PRICE", "{0:n0}")})
        Me.XrLabel45.Font = New System.Drawing.Font("Arial", 8!)
        Me.XrLabel45.LocationFloat = New DevExpress.Utils.PointFloat(541.3498!, 0!)
        Me.XrLabel45.Name = "XrLabel45"
        Me.XrLabel45.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100!)
        Me.XrLabel45.SizeF = New System.Drawing.SizeF(100.275!, 18.47222!)
        Me.XrLabel45.StylePriority.UseFont = false
        Me.XrLabel45.StylePriority.UseTextAlignment = false
        Me.XrLabel45.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight
        '
        'XrLabel42
        '
        Me.XrLabel42.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "QTY", "{0:n2}")})
        Me.XrLabel42.Font = New System.Drawing.Font("Arial", 8!)
        Me.XrLabel42.LocationFloat = New DevExpress.Utils.PointFloat(471.7498!, 0!)
        Me.XrLabel42.Name = "XrLabel42"
        Me.XrLabel42.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100!)
        Me.XrLabel42.SizeF = New System.Drawing.SizeF(69.59995!, 18.47222!)
        Me.XrLabel42.StylePriority.UseFont = false
        Me.XrLabel42.StylePriority.UseTextAlignment = false
        Me.XrLabel42.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight
        '
        'XrLabel41
        '
        Me.XrLabel41.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "NAMATARIF")})
        Me.XrLabel41.Font = New System.Drawing.Font("Arial", 8!)
        Me.XrLabel41.LocationFloat = New DevExpress.Utils.PointFloat(97.98257!, 0!)
        Me.XrLabel41.Name = "XrLabel41"
        Me.XrLabel41.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100!)
        Me.XrLabel41.SizeF = New System.Drawing.SizeF(373.7672!, 18.47222!)
        Me.XrLabel41.StylePriority.UseFont = false
        Me.XrLabel41.StylePriority.UseTextAlignment = false
        Me.XrLabel41.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft
        '
        'XrLabel46
        '
        Me.XrLabel46.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "SUBTOTAL", "{0:n0}")})
        Me.XrLabel46.Font = New System.Drawing.Font("Arial", 8!)
        Me.XrLabel46.LocationFloat = New DevExpress.Utils.PointFloat(652.0489!, 0!)
        Me.XrLabel46.Name = "XrLabel46"
        Me.XrLabel46.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100!)
        Me.XrLabel46.SizeF = New System.Drawing.SizeF(127.9584!, 18.47222!)
        Me.XrLabel46.StylePriority.UseFont = false
        Me.XrLabel46.StylePriority.UseTextAlignment = false
        Me.XrLabel46.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight
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
        'XrLabel22
        '
        Me.XrLabel22.Font = New System.Drawing.Font("Arial", 8!)
        Me.XrLabel22.LocationFloat = New DevExpress.Utils.PointFloat(3.007038!, 0!)
        Me.XrLabel22.Name = "XrLabel22"
        Me.XrLabel22.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100!)
        Me.XrLabel22.SizeF = New System.Drawing.SizeF(84.5517!, 18.47222!)
        Me.XrLabel22.StylePriority.UseFont = false
        Me.XrLabel22.StylePriority.UseTextAlignment = false
        Me.XrLabel22.Text = "Tanggal"
        Me.XrLabel22.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft
        '
        'XrLabel27
        '
        Me.XrLabel27.Font = New System.Drawing.Font("Arial", 8!)
        Me.XrLabel27.LocationFloat = New DevExpress.Utils.PointFloat(97.97541!, 0!)
        Me.XrLabel27.Name = "XrLabel27"
        Me.XrLabel27.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100!)
        Me.XrLabel27.SizeF = New System.Drawing.SizeF(373.7672!, 18.47222!)
        Me.XrLabel27.StylePriority.UseFont = false
        Me.XrLabel27.StylePriority.UseTextAlignment = false
        Me.XrLabel27.Text = "Keterangan"
        Me.XrLabel27.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft
        '
        'XrLabel37
        '
        Me.XrLabel37.Font = New System.Drawing.Font("Arial", 8!)
        Me.XrLabel37.LocationFloat = New DevExpress.Utils.PointFloat(471.7426!, 0!)
        Me.XrLabel37.Name = "XrLabel37"
        Me.XrLabel37.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100!)
        Me.XrLabel37.SizeF = New System.Drawing.SizeF(69.59995!, 18.47222!)
        Me.XrLabel37.StylePriority.UseFont = false
        Me.XrLabel37.StylePriority.UseTextAlignment = false
        Me.XrLabel37.Text = "Jumlah"
        Me.XrLabel37.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft
        '
        'XrLabel38
        '
        Me.XrLabel38.Font = New System.Drawing.Font("Arial", 8!)
        Me.XrLabel38.LocationFloat = New DevExpress.Utils.PointFloat(541.3426!, 0!)
        Me.XrLabel38.Name = "XrLabel38"
        Me.XrLabel38.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100!)
        Me.XrLabel38.SizeF = New System.Drawing.SizeF(100.275!, 18.47222!)
        Me.XrLabel38.StylePriority.UseFont = false
        Me.XrLabel38.StylePriority.UseTextAlignment = false
        Me.XrLabel38.Text = "Tarif"
        Me.XrLabel38.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft
        '
        'XrLabel39
        '
        Me.XrLabel39.Font = New System.Drawing.Font("Arial", 8!)
        Me.XrLabel39.LocationFloat = New DevExpress.Utils.PointFloat(652.0417!, 0!)
        Me.XrLabel39.Name = "XrLabel39"
        Me.XrLabel39.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100!)
        Me.XrLabel39.SizeF = New System.Drawing.SizeF(127.9584!, 18.47222!)
        Me.XrLabel39.StylePriority.UseFont = false
        Me.XrLabel39.StylePriority.UseTextAlignment = false
        Me.XrLabel39.Text = "Total (Rp.)"
        Me.XrLabel39.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft
        '
        'XrLabel44
        '
        Me.XrLabel44.Borders = DevExpress.XtraPrinting.BorderSide.Top
        Me.XrLabel44.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "SUBTOTAL")})
        Me.XrLabel44.Font = New System.Drawing.Font("Arial", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.XrLabel44.LocationFloat = New DevExpress.Utils.PointFloat(655.0416!, 0!)
        Me.XrLabel44.Name = "XrLabel44"
        Me.XrLabel44.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100!)
        Me.XrLabel44.SizeF = New System.Drawing.SizeF(127.9584!, 18.47222!)
        Me.XrLabel44.StylePriority.UseBorders = false
        Me.XrLabel44.StylePriority.UseFont = false
        Me.XrLabel44.StylePriority.UseTextAlignment = false
        XrSummary1.FormatString = "{0:n0}"
        XrSummary1.Running = DevExpress.XtraReports.UI.SummaryRunning.Group
        Me.XrLabel44.Summary = XrSummary1
        Me.XrLabel44.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight
        '
        'XrLabel10
        '
        Me.XrLabel10.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "sKategori")})
        Me.XrLabel10.Font = New System.Drawing.Font("Arial", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.XrLabel10.LocationFloat = New DevExpress.Utils.PointFloat(367.164!, 0!)
        Me.XrLabel10.Name = "XrLabel10"
        Me.XrLabel10.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100!)
        Me.XrLabel10.SizeF = New System.Drawing.SizeF(277.4533!, 18.47222!)
        Me.XrLabel10.StylePriority.UseFont = false
        Me.XrLabel10.StylePriority.UseTextAlignment = false
        Me.XrLabel10.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight
        '
        'XrLabel70
        '
        Me.XrLabel70.Font = New System.Drawing.Font("Arial", 8.5!)
        Me.XrLabel70.LocationFloat = New DevExpress.Utils.PointFloat(496.684!, 36.11115!)
        Me.XrLabel70.Name = "XrLabel70"
        Me.XrLabel70.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100!)
        Me.XrLabel70.SizeF = New System.Drawing.SizeF(19.78999!, 18.47226!)
        Me.XrLabel70.StylePriority.UseFont = false
        Me.XrLabel70.StylePriority.UseTextAlignment = false
        Me.XrLabel70.Text = ":"
        Me.XrLabel70.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft
        '
        'XrLabel71
        '
        Me.XrLabel71.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "POLIAWAL")})
        Me.XrLabel71.Font = New System.Drawing.Font("Arial", 8.5!, System.Drawing.FontStyle.Bold)
        Me.XrLabel71.LocationFloat = New DevExpress.Utils.PointFloat(516.4739!, 36.11115!)
        Me.XrLabel71.Name = "XrLabel71"
        Me.XrLabel71.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100!)
        Me.XrLabel71.SizeF = New System.Drawing.SizeF(256.5261!, 18.47222!)
        Me.XrLabel71.StylePriority.UseFont = false
        Me.XrLabel71.StylePriority.UseTextAlignment = false
        Me.XrLabel71.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft
        '
        'XrLabel72
        '
        Me.XrLabel72.Font = New System.Drawing.Font("Arial", 8.5!)
        Me.XrLabel72.LocationFloat = New DevExpress.Utils.PointFloat(459.5304!, 36.11115!)
        Me.XrLabel72.Name = "XrLabel72"
        Me.XrLabel72.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100!)
        Me.XrLabel72.SizeF = New System.Drawing.SizeF(37.15005!, 18.47222!)
        Me.XrLabel72.StylePriority.UseFont = false
        Me.XrLabel72.StylePriority.UseTextAlignment = false
        Me.XrLabel72.Text = "Poli"
        Me.XrLabel72.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft
        '
        'XrLabel67
        '
        Me.XrLabel67.Font = New System.Drawing.Font("Arial", 8.5!)
        Me.XrLabel67.LocationFloat = New DevExpress.Utils.PointFloat(2.99991!, 36.11115!)
        Me.XrLabel67.Name = "XrLabel67"
        Me.XrLabel67.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100!)
        Me.XrLabel67.SizeF = New System.Drawing.SizeF(115.8089!, 18.47222!)
        Me.XrLabel67.StylePriority.UseFont = false
        Me.XrLabel67.StylePriority.UseTextAlignment = false
        Me.XrLabel67.Text = "No. Registrasi"
        Me.XrLabel67.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft
        '
        'XrLabel68
        '
        Me.XrLabel68.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "KDREGAWAL")})
        Me.XrLabel68.Font = New System.Drawing.Font("Arial", 8.5!, System.Drawing.FontStyle.Bold)
        Me.XrLabel68.LocationFloat = New DevExpress.Utils.PointFloat(138.5988!, 36.11115!)
        Me.XrLabel68.Name = "XrLabel68"
        Me.XrLabel68.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100!)
        Me.XrLabel68.SizeF = New System.Drawing.SizeF(144.5951!, 18.47222!)
        Me.XrLabel68.StylePriority.UseFont = false
        Me.XrLabel68.StylePriority.UseTextAlignment = false
        Me.XrLabel68.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft
        '
        'XrLabel69
        '
        Me.XrLabel69.Font = New System.Drawing.Font("Arial", 8.5!)
        Me.XrLabel69.LocationFloat = New DevExpress.Utils.PointFloat(118.8088!, 36.1111!)
        Me.XrLabel69.Name = "XrLabel69"
        Me.XrLabel69.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100!)
        Me.XrLabel69.SizeF = New System.Drawing.SizeF(19.78999!, 18.47226!)
        Me.XrLabel69.StylePriority.UseFont = false
        Me.XrLabel69.StylePriority.UseTextAlignment = false
        Me.XrLabel69.Text = ":"
        Me.XrLabel69.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft
        '
        'XrLabel66
        '
        Me.XrLabel66.Font = New System.Drawing.Font("Arial", 8.5!, System.Drawing.FontStyle.Bold)
        Me.XrLabel66.LocationFloat = New DevExpress.Utils.PointFloat(2.999878!, 10.00001!)
        Me.XrLabel66.Name = "XrLabel66"
        Me.XrLabel66.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100!)
        Me.XrLabel66.SizeF = New System.Drawing.SizeF(476.1631!, 18.4722!)
        Me.XrLabel66.StylePriority.UseFont = false
        Me.XrLabel66.StylePriority.UseTextAlignment = false
        Me.XrLabel66.Text = "Data Instalasi Gawat Darurat / Instalasi Rawat Jalan"
        Me.XrLabel66.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft
        '
        'XrLabel29
        '
        Me.XrLabel29.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "KATEGORI")})
        Me.XrLabel29.Font = New System.Drawing.Font("Arial", 8!, System.Drawing.FontStyle.Bold)
        Me.XrLabel29.LocationFloat = New DevExpress.Utils.PointFloat(0!, 0!)
        Me.XrLabel29.Name = "XrLabel29"
        Me.XrLabel29.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100!)
        Me.XrLabel29.SizeF = New System.Drawing.SizeF(776.9931!, 19.16666!)
        Me.XrLabel29.StylePriority.UseFont = false
        Me.XrLabel29.StylePriority.UseTextAlignment = false
        Me.XrLabel29.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft
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
        'GroupHeader2
        '
        Me.GroupHeader2.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.XrLabel22, Me.XrLabel38, Me.XrLabel37, Me.XrLabel27, Me.XrLabel39})
        Me.GroupHeader2.HeightF = 18.47222!
        Me.GroupHeader2.Name = "GroupHeader2"
        '
        'GroupFooter1
        '
        Me.GroupFooter1.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.XrLabel44, Me.XrLabel10})
        Me.GroupFooter1.HeightF = 18.47222!
        Me.GroupFooter1.Name = "GroupFooter1"
        '
        'ReportHeader
        '
        Me.ReportHeader.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.XrLabel66, Me.XrLabel69, Me.XrLabel68, Me.XrLabel67, Me.XrLabel72, Me.XrLabel71, Me.XrLabel70})
        Me.ReportHeader.HeightF = 61.87507!
        Me.ReportHeader.Name = "ReportHeader"
        '
        'XrLabel1
        '
        Me.XrLabel1.Font = New System.Drawing.Font("Arial", 8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.XrLabel1.LocationFloat = New DevExpress.Utils.PointFloat(367.164!, 10.00001!)
        Me.XrLabel1.Name = "XrLabel1"
        Me.XrLabel1.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100!)
        Me.XrLabel1.SizeF = New System.Drawing.SizeF(277.4533!, 18.47222!)
        Me.XrLabel1.StylePriority.UseFont = false
        Me.XrLabel1.StylePriority.UseTextAlignment = false
        Me.XrLabel1.Text = "Total IGD / Rawat Jalan :"
        Me.XrLabel1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight
        '
        'XrLabel2
        '
        Me.XrLabel2.Borders = DevExpress.XtraPrinting.BorderSide.Top
        Me.XrLabel2.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "SUBTOTAL")})
        Me.XrLabel2.Font = New System.Drawing.Font("Arial", 8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.XrLabel2.LocationFloat = New DevExpress.Utils.PointFloat(655.0416!, 10.00001!)
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
        Me.bindingSource.DataSource = GetType(DataAccess.R_RINCIAN_NAIKRANAP_D)
        '
        'ReportFooter
        '
        Me.ReportFooter.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.XrLabel2, Me.XrLabel1})
        Me.ReportFooter.HeightF = 28.47223!
        Me.ReportFooter.Name = "ReportFooter"
        Me.ReportFooter.PrintAtBottom = true
        '
        'GroupHeader3
        '
        Me.GroupHeader3.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.XrLabel29})
        Me.GroupHeader3.GroupFields.AddRange(New DevExpress.XtraReports.UI.GroupField() {New DevExpress.XtraReports.UI.GroupField("KATEGORI", DevExpress.XtraReports.UI.XRColumnSortOrder.Ascending)})
        Me.GroupHeader3.HeightF = 19.16666!
        Me.GroupHeader3.Level = 1
        Me.GroupHeader3.Name = "GroupHeader3"
        '
        'xtraRincianPerawatanNaikRanap_D
        '
        Me.Bands.AddRange(New DevExpress.XtraReports.UI.Band() {Me.Detail, Me.TopMargin, Me.BottomMargin, Me.GroupHeader2, Me.GroupFooter1, Me.ReportHeader, Me.ReportFooter, Me.GroupHeader3})
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
    Friend WithEvents XrLabel46 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel45 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel42 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel41 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel40 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel39 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel38 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel37 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel27 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel22 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel29 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel10 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel44 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel67 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel68 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel69 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel66 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel70 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel71 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel72 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents sKategori_D_RI As DevExpress.XtraReports.UI.CalculatedField
    Friend WithEvents sKategori_D As DevExpress.XtraReports.UI.CalculatedField
    Friend WithEvents GroupHeader2 As DevExpress.XtraReports.UI.GroupHeaderBand
    Friend WithEvents GroupFooter1 As DevExpress.XtraReports.UI.GroupFooterBand
    Friend WithEvents ReportHeader As DevExpress.XtraReports.UI.ReportHeaderBand
    Friend WithEvents XrLabel1 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel2 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents ReportFooter As DevExpress.XtraReports.UI.ReportFooterBand
    Friend WithEvents GroupHeader3 As DevExpress.XtraReports.UI.GroupHeaderBand
End Class
