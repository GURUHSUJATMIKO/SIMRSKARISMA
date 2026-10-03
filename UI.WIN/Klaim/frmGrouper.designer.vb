<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmGrouper
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
        Me.components = New System.ComponentModel.Container()
        Dim GridLevelNode1 As DevExpress.XtraGrid.GridLevelNode = New DevExpress.XtraGrid.GridLevelNode()
        Me.grv1 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn1 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn7 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn9 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn10 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn11 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn15 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.grd = New DevExpress.XtraGrid.GridControl()
        Me.grv = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.layoutControl = New DevExpress.XtraLayout.LayoutControl()
        Me.txtVENTILATOR = New DevExpress.XtraEditors.TextEdit()
        Me.barManager = New DevExpress.XtraBars.BarManager(Me.components)
        Me.barTop = New DevExpress.XtraBars.Bar()
        Me.btnSaveNew = New DevExpress.XtraBars.BarButtonItem()
        Me.btnSaveClose = New DevExpress.XtraBars.BarButtonItem()
        Me.btnAddCustomer = New DevExpress.XtraBars.BarButtonItem()
        Me.btnUpdatePasien = New DevExpress.XtraBars.BarButtonItem()
        Me.btnClose = New DevExpress.XtraBars.BarButtonItem()
        Me.barDockControlTop = New DevExpress.XtraBars.BarDockControl()
        Me.barDockControlBottom = New DevExpress.XtraBars.BarDockControl()
        Me.barDockControlLeft = New DevExpress.XtraBars.BarDockControl()
        Me.barDockControlRight = New DevExpress.XtraBars.BarDockControl()
        Me.btnICD = New DevExpress.XtraBars.BarButtonItem()
        Me.btnProsedur = New DevExpress.XtraBars.BarButtonItem()
        Me.progressBarSave = New DevExpress.XtraEditors.Repository.RepositoryItemMarqueeProgressBar()
        Me.progressSave = New DevExpress.XtraEditors.Repository.RepositoryItemMarqueeProgressBar()
        Me.LabelControl18 = New DevExpress.XtraEditors.LabelControl()
        Me.txtRAWATINTENSIF_HARI = New DevExpress.XtraEditors.TextEdit()
        Me.LabelControl17 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl12 = New DevExpress.XtraEditors.LabelControl()
        Me.txtLAMA = New DevExpress.XtraEditors.TextEdit()
        Me.LabelControl10 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl16 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl15 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl14 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl13 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl11 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl9 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl8 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl7 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl6 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl5 = New DevExpress.XtraEditors.LabelControl()
        Me.rbKELASPELAYANAN = New DevExpress.XtraEditors.RadioGroup()
        Me.chkNaikKelas = New DevExpress.XtraEditors.CheckEdit()
        Me.chkAdaRawat = New DevExpress.XtraEditors.CheckEdit()
        Me.rbKELASHAK = New DevExpress.XtraEditors.RadioGroup()
        Me.rbCategory = New DevExpress.XtraEditors.RadioGroup()
        Me.grdICD_IX = New DevExpress.XtraEditors.GridLookUpEdit()
        Me.GridView5 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn19 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.grdICD_X = New DevExpress.XtraEditors.GridLookUpEdit()
        Me.GridView3 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn18 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.grdJenisTarif = New DevExpress.XtraEditors.GridLookUpEdit()
        Me.GridView1 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn8 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.txtHAKKELAS = New DevExpress.XtraEditors.TextEdit()
        Me.txtTarifEksekutif = New DevExpress.XtraEditors.TextEdit()
        Me.txtICCD_IX = New DevExpress.XtraEditors.TextEdit()
        Me.txtICD_X = New DevExpress.XtraEditors.TextEdit()
        Me.txtSewaAlat = New DevExpress.XtraEditors.TextEdit()
        Me.txtObatKemoTerapi = New DevExpress.XtraEditors.TextEdit()
        Me.txtRawatIntensif = New DevExpress.XtraEditors.TextEdit()
        Me.txtPelayananDarah = New DevExpress.XtraEditors.TextEdit()
        Me.txtPenunjang = New DevExpress.XtraEditors.TextEdit()
        Me.txtKonsultasi = New DevExpress.XtraEditors.TextEdit()
        Me.txtBMHP = New DevExpress.XtraEditors.TextEdit()
        Me.txtObatKronis = New DevExpress.XtraEditors.TextEdit()
        Me.txtKamarAkomodasi = New DevExpress.XtraEditors.TextEdit()
        Me.txtLaboratorium = New DevExpress.XtraEditors.TextEdit()
        Me.txtKeperawatan = New DevExpress.XtraEditors.TextEdit()
        Me.txtProsedurBedah = New DevExpress.XtraEditors.TextEdit()
        Me.txtAlkes = New DevExpress.XtraEditors.TextEdit()
        Me.txtObat = New DevExpress.XtraEditors.TextEdit()
        Me.txtRehabilitasi = New DevExpress.XtraEditors.TextEdit()
        Me.txtRadiologi = New DevExpress.XtraEditors.TextEdit()
        Me.txtTenagaAhli = New DevExpress.XtraEditors.TextEdit()
        Me.txtProsedurNonBedah = New DevExpress.XtraEditors.TextEdit()
        Me.txttarifRumahSakit = New DevExpress.XtraEditors.TextEdit()
        Me.grdCaraKeluar = New DevExpress.XtraEditors.GridLookUpEdit()
        Me.grvCaraKeluar = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn6 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.txtBeratBadan = New DevExpress.XtraEditors.TextEdit()
        Me.txtUmur = New DevExpress.XtraEditors.TextEdit()
        Me.txtADLScore_Chronic = New DevExpress.XtraEditors.TextEdit()
        Me.txtADLScore_SubAcute = New DevExpress.XtraEditors.TextEdit()
        Me.txtLOS = New DevExpress.XtraEditors.TextEdit()
        Me.chkKelasEksekutif = New DevExpress.XtraEditors.CheckEdit()
        Me.LabelControl4 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl3 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl2 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl1 = New DevExpress.XtraEditors.LabelControl()
        Me.grdCOB = New DevExpress.XtraEditors.GridLookUpEdit()
        Me.GridView2 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn5 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.txtNoSEP = New DevExpress.XtraEditors.TextEdit()
        Me.txtNoPeserta = New DevExpress.XtraEditors.TextEdit()
        Me.cboCaraBayar = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.txtCODE = New DevExpress.XtraEditors.TextEdit()
        Me.grdDPJP = New DevExpress.XtraEditors.GridLookUpEdit()
        Me.GridView4 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn2 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.grdKDCASHIN = New DevExpress.XtraEditors.GridLookUpEdit()
        Me.grvKDCASHIN = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn12 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn14 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn3 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn4 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn13 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn16 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn17 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn20 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn21 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn22 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn23 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn24 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cboCARI = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.txtCARI = New DevExpress.XtraEditors.TextEdit()
        Me.deDATEPULANG = New DevExpress.XtraEditors.DateEdit()
        Me.deDATEMASUK = New DevExpress.XtraEditors.DateEdit()
        Me.LayoutControlGroup1 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.LayoutControlItem4 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.lKDSKD = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlGroup2 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.LayoutControlItem1 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem2 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem5 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem6 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem7 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem8 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem9 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem10 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlGroup3 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.lKDDOCTOR = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem13 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem14 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem12 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.lDATE = New DevExpress.XtraLayout.LayoutControlItem()
        Me.lDATE_KONTROL = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LKELASPELAYANAN_RB = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem20 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.lKELASPELAYANAN = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem50 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem51 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem52 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.lNAIKKELAS = New DevExpress.XtraLayout.LayoutControlItem()
        Me.lADARAWAT = New DevExpress.XtraLayout.LayoutControlItem()
        Me.lRAWATINTENSIF_HARI = New DevExpress.XtraLayout.LayoutControlItem()
        Me.lRAWATINTENSIF_HARI_TEXT = New DevExpress.XtraLayout.LayoutControlItem()
        Me.lKELASEKSEKUTIF = New DevExpress.XtraLayout.LayoutControlItem()
        Me.lKELASHAKRJ_LBL = New DevExpress.XtraLayout.LayoutControlItem()
        Me.lKELASHAKRI = New DevExpress.XtraLayout.LayoutControlItem()
        Me.lKELASHAKRJ = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem25 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem15 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.lLAMA_LBL = New DevExpress.XtraLayout.LayoutControlItem()
        Me.lLAMA = New DevExpress.XtraLayout.LayoutControlItem()
        Me.lVENTILATOR = New DevExpress.XtraLayout.LayoutControlItem()
        Me.lVENTILATOR_TEXT = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem26 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem16 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem49 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem18 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem54 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem17 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.lTARIFEKSEKUTIF = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlGroup5 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.LayoutControlItem29 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem30 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem31 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem32 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem33 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem34 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem35 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem36 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem37 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem38 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem39 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem40 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem41 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem42 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem43 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem44 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem45 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem46 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem28 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.EmptySpaceItem1 = New DevExpress.XtraLayout.EmptySpaceItem()
        Me.LayoutControlItem47 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem48 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem23 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem11 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem21 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem3 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.lKDPENDAFATRAN = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem22 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem27 = New DevExpress.XtraLayout.LayoutControlItem()
        CType(Me.grv1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grd, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grv, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.layoutControl, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.layoutControl.SuspendLayout()
        CType(Me.txtVENTILATOR.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.barManager, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.progressBarSave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.progressSave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtRAWATINTENSIF_HARI.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtLAMA.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rbKELASPELAYANAN.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkNaikKelas.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkAdaRawat.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rbKELASHAK.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rbCategory.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdICD_IX.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdICD_X.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdJenisTarif.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtHAKKELAS.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtTarifEksekutif.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtICCD_IX.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtICD_X.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtSewaAlat.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtObatKemoTerapi.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtRawatIntensif.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtPelayananDarah.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtPenunjang.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtKonsultasi.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtBMHP.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtObatKronis.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtKamarAkomodasi.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtLaboratorium.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtKeperawatan.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtProsedurBedah.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtAlkes.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtObat.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtRehabilitasi.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtRadiologi.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtTenagaAhli.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtProsedurNonBedah.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txttarifRumahSakit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdCaraKeluar.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grvCaraKeluar, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtBeratBadan.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtUmur.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtADLScore_Chronic.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtADLScore_SubAcute.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtLOS.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkKelasEksekutif.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdCOB.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtNoSEP.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtNoPeserta.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cboCaraBayar.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCODE.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdDPJP.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdKDCASHIN.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grvKDCASHIN, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cboCARI.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCARI.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.deDATEPULANG.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.deDATEPULANG.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.deDATEMASUK.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.deDATEMASUK.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lKDSKD, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem7, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem8, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem9, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem10, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lKDDOCTOR, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem13, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem14, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem12, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lDATE, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lDATE_KONTROL, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LKELASPELAYANAN_RB, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem20, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lKELASPELAYANAN, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem50, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem51, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem52, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lNAIKKELAS, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lADARAWAT, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lRAWATINTENSIF_HARI, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lRAWATINTENSIF_HARI_TEXT, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lKELASEKSEKUTIF, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lKELASHAKRJ_LBL, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lKELASHAKRI, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lKELASHAKRJ, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem25, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem15, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lLAMA_LBL, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lLAMA, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lVENTILATOR, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lVENTILATOR_TEXT, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem26, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem16, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem49, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem18, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem54, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem17, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lTARIFEKSEKUTIF, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem29, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem30, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem31, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem32, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem33, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem34, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem35, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem36, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem37, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem38, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem39, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem40, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem41, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem42, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem43, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem44, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem45, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem46, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem28, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.EmptySpaceItem1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem47, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem48, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem23, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem11, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem21, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lKDPENDAFATRAN, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem22, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem27, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'grv1
        '
        Me.grv1.AppearancePrint.EvenRow.BackColor = System.Drawing.Color.Transparent
        Me.grv1.AppearancePrint.EvenRow.BackColor2 = System.Drawing.Color.Transparent
        Me.grv1.AppearancePrint.EvenRow.BorderColor = System.Drawing.Color.Transparent
        Me.grv1.AppearancePrint.EvenRow.Options.UseBackColor = True
        Me.grv1.AppearancePrint.EvenRow.Options.UseBorderColor = True
        Me.grv1.AppearancePrint.FilterPanel.BackColor = System.Drawing.Color.Transparent
        Me.grv1.AppearancePrint.FilterPanel.BackColor2 = System.Drawing.Color.Transparent
        Me.grv1.AppearancePrint.FilterPanel.BorderColor = System.Drawing.Color.Transparent
        Me.grv1.AppearancePrint.FilterPanel.Options.UseBackColor = True
        Me.grv1.AppearancePrint.FilterPanel.Options.UseBorderColor = True
        Me.grv1.AppearancePrint.FooterPanel.BackColor = System.Drawing.Color.Transparent
        Me.grv1.AppearancePrint.FooterPanel.BackColor2 = System.Drawing.Color.Transparent
        Me.grv1.AppearancePrint.FooterPanel.BorderColor = System.Drawing.Color.Transparent
        Me.grv1.AppearancePrint.FooterPanel.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.grv1.AppearancePrint.FooterPanel.Options.UseBackColor = True
        Me.grv1.AppearancePrint.FooterPanel.Options.UseBorderColor = True
        Me.grv1.AppearancePrint.FooterPanel.Options.UseFont = True
        Me.grv1.AppearancePrint.GroupFooter.BackColor = System.Drawing.Color.Transparent
        Me.grv1.AppearancePrint.GroupFooter.BackColor2 = System.Drawing.Color.Transparent
        Me.grv1.AppearancePrint.GroupFooter.BorderColor = System.Drawing.Color.Transparent
        Me.grv1.AppearancePrint.GroupFooter.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.grv1.AppearancePrint.GroupFooter.Options.UseBackColor = True
        Me.grv1.AppearancePrint.GroupFooter.Options.UseBorderColor = True
        Me.grv1.AppearancePrint.GroupFooter.Options.UseFont = True
        Me.grv1.AppearancePrint.GroupRow.BackColor = System.Drawing.Color.Transparent
        Me.grv1.AppearancePrint.GroupRow.BackColor2 = System.Drawing.Color.Transparent
        Me.grv1.AppearancePrint.GroupRow.BorderColor = System.Drawing.Color.Transparent
        Me.grv1.AppearancePrint.GroupRow.Options.UseBackColor = True
        Me.grv1.AppearancePrint.GroupRow.Options.UseBorderColor = True
        Me.grv1.AppearancePrint.HeaderPanel.BackColor = System.Drawing.Color.Transparent
        Me.grv1.AppearancePrint.HeaderPanel.BackColor2 = System.Drawing.Color.Transparent
        Me.grv1.AppearancePrint.HeaderPanel.BorderColor = System.Drawing.Color.Transparent
        Me.grv1.AppearancePrint.HeaderPanel.Font = New System.Drawing.Font("Tahoma", 8.25!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Underline), System.Drawing.FontStyle))
        Me.grv1.AppearancePrint.HeaderPanel.Options.UseBackColor = True
        Me.grv1.AppearancePrint.HeaderPanel.Options.UseBorderColor = True
        Me.grv1.AppearancePrint.HeaderPanel.Options.UseFont = True
        Me.grv1.AppearancePrint.HeaderPanel.Options.UseTextOptions = True
        Me.grv1.AppearancePrint.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near
        Me.grv1.AppearancePrint.Lines.BackColor = System.Drawing.Color.Transparent
        Me.grv1.AppearancePrint.Lines.BackColor2 = System.Drawing.Color.Transparent
        Me.grv1.AppearancePrint.Lines.BorderColor = System.Drawing.Color.Transparent
        Me.grv1.AppearancePrint.Lines.Options.UseBackColor = True
        Me.grv1.AppearancePrint.Lines.Options.UseBorderColor = True
        Me.grv1.AppearancePrint.OddRow.BackColor = System.Drawing.Color.Transparent
        Me.grv1.AppearancePrint.OddRow.BackColor2 = System.Drawing.Color.Transparent
        Me.grv1.AppearancePrint.OddRow.BorderColor = System.Drawing.Color.Transparent
        Me.grv1.AppearancePrint.OddRow.Options.UseBackColor = True
        Me.grv1.AppearancePrint.OddRow.Options.UseBorderColor = True
        Me.grv1.AppearancePrint.Preview.BackColor = System.Drawing.Color.Transparent
        Me.grv1.AppearancePrint.Preview.BackColor2 = System.Drawing.Color.Transparent
        Me.grv1.AppearancePrint.Preview.BorderColor = System.Drawing.Color.Transparent
        Me.grv1.AppearancePrint.Preview.Options.UseBackColor = True
        Me.grv1.AppearancePrint.Preview.Options.UseBorderColor = True
        Me.grv1.AppearancePrint.Row.BackColor = System.Drawing.Color.Transparent
        Me.grv1.AppearancePrint.Row.BackColor2 = System.Drawing.Color.Transparent
        Me.grv1.AppearancePrint.Row.BorderColor = System.Drawing.Color.Transparent
        Me.grv1.AppearancePrint.Row.Options.UseBackColor = True
        Me.grv1.AppearancePrint.Row.Options.UseBorderColor = True
        Me.grv1.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn1, Me.GridColumn7, Me.GridColumn9, Me.GridColumn10, Me.GridColumn11, Me.GridColumn15})
        Me.grv1.GridControl = Me.grd
        Me.grv1.Name = "grv1"
        Me.grv1.OptionsBehavior.Editable = False
        Me.grv1.OptionsPrint.EnableAppearanceEvenRow = True
        Me.grv1.OptionsPrint.EnableAppearanceOddRow = True
        Me.grv1.OptionsPrint.PrintDetails = True
        Me.grv1.OptionsPrint.PrintFilterInfo = True
        Me.grv1.OptionsPrint.PrintHorzLines = False
        Me.grv1.OptionsPrint.PrintVertLines = False
        Me.grv1.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.grv1.OptionsView.ShowAutoFilterRow = True
        Me.grv1.OptionsView.ShowFooter = True
        Me.grv1.OptionsView.ShowGroupPanel = False
        '
        'GridColumn1
        '
        Me.GridColumn1.Caption = "Transaksi"
        Me.GridColumn1.FieldName = "Transaksi"
        Me.GridColumn1.Name = "GridColumn1"
        Me.GridColumn1.Visible = True
        Me.GridColumn1.VisibleIndex = 0
        '
        'GridColumn7
        '
        Me.GridColumn7.Caption = "No_Reg"
        Me.GridColumn7.FieldName = "No_Reg"
        Me.GridColumn7.Name = "GridColumn7"
        Me.GridColumn7.Visible = True
        Me.GridColumn7.VisibleIndex = 1
        '
        'GridColumn9
        '
        Me.GridColumn9.Caption = "Tgl_Tindakan"
        Me.GridColumn9.DisplayFormat.FormatString = "dd-MM-yyyy"
        Me.GridColumn9.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.GridColumn9.FieldName = "Tgl_Tindakan"
        Me.GridColumn9.Name = "GridColumn9"
        Me.GridColumn9.Visible = True
        Me.GridColumn9.VisibleIndex = 2
        '
        'GridColumn10
        '
        Me.GridColumn10.Caption = "Nama Tindakan"
        Me.GridColumn10.FieldName = "NamaTindakan"
        Me.GridColumn10.Name = "GridColumn10"
        Me.GridColumn10.Visible = True
        Me.GridColumn10.VisibleIndex = 3
        '
        'GridColumn11
        '
        Me.GridColumn11.Caption = "Total Tarif Margin"
        Me.GridColumn11.DisplayFormat.FormatString = "{0:n2}"
        Me.GridColumn11.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.GridColumn11.FieldName = "TotalTarifMargin"
        Me.GridColumn11.Name = "GridColumn11"
        Me.GridColumn11.Visible = True
        Me.GridColumn11.VisibleIndex = 4
        '
        'GridColumn15
        '
        Me.GridColumn15.Caption = "Kategori"
        Me.GridColumn15.FieldName = "Kategori"
        Me.GridColumn15.Name = "GridColumn15"
        Me.GridColumn15.Visible = True
        Me.GridColumn15.VisibleIndex = 5
        '
        'grd
        '
        Me.grd.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        GridLevelNode1.LevelTemplate = Me.grv1
        GridLevelNode1.RelationName = "Level1"
        Me.grd.LevelTree.Nodes.AddRange(New DevExpress.XtraGrid.GridLevelNode() {GridLevelNode1})
        Me.grd.Location = New System.Drawing.Point(969, 16)
        Me.grd.MainView = Me.grv
        Me.grd.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.grd.Name = "grd"
        Me.grd.ShowOnlyPredefinedDetails = True
        Me.grd.Size = New System.Drawing.Size(493, 683)
        Me.grd.TabIndex = 5
        Me.grd.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.grv, Me.grv1})
        '
        'grv
        '
        Me.grv.AppearancePrint.EvenRow.BackColor = System.Drawing.Color.Transparent
        Me.grv.AppearancePrint.EvenRow.BackColor2 = System.Drawing.Color.Transparent
        Me.grv.AppearancePrint.EvenRow.BorderColor = System.Drawing.Color.Transparent
        Me.grv.AppearancePrint.EvenRow.Options.UseBackColor = True
        Me.grv.AppearancePrint.EvenRow.Options.UseBorderColor = True
        Me.grv.AppearancePrint.FilterPanel.BackColor = System.Drawing.Color.Transparent
        Me.grv.AppearancePrint.FilterPanel.BackColor2 = System.Drawing.Color.Transparent
        Me.grv.AppearancePrint.FilterPanel.BorderColor = System.Drawing.Color.Transparent
        Me.grv.AppearancePrint.FilterPanel.Options.UseBackColor = True
        Me.grv.AppearancePrint.FilterPanel.Options.UseBorderColor = True
        Me.grv.AppearancePrint.FooterPanel.BackColor = System.Drawing.Color.Transparent
        Me.grv.AppearancePrint.FooterPanel.BackColor2 = System.Drawing.Color.Transparent
        Me.grv.AppearancePrint.FooterPanel.BorderColor = System.Drawing.Color.Transparent
        Me.grv.AppearancePrint.FooterPanel.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.grv.AppearancePrint.FooterPanel.Options.UseBackColor = True
        Me.grv.AppearancePrint.FooterPanel.Options.UseBorderColor = True
        Me.grv.AppearancePrint.FooterPanel.Options.UseFont = True
        Me.grv.AppearancePrint.GroupFooter.BackColor = System.Drawing.Color.Transparent
        Me.grv.AppearancePrint.GroupFooter.BackColor2 = System.Drawing.Color.Transparent
        Me.grv.AppearancePrint.GroupFooter.BorderColor = System.Drawing.Color.Transparent
        Me.grv.AppearancePrint.GroupFooter.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.grv.AppearancePrint.GroupFooter.Options.UseBackColor = True
        Me.grv.AppearancePrint.GroupFooter.Options.UseBorderColor = True
        Me.grv.AppearancePrint.GroupFooter.Options.UseFont = True
        Me.grv.AppearancePrint.GroupRow.BackColor = System.Drawing.Color.Transparent
        Me.grv.AppearancePrint.GroupRow.BackColor2 = System.Drawing.Color.Transparent
        Me.grv.AppearancePrint.GroupRow.BorderColor = System.Drawing.Color.Transparent
        Me.grv.AppearancePrint.GroupRow.Options.UseBackColor = True
        Me.grv.AppearancePrint.GroupRow.Options.UseBorderColor = True
        Me.grv.AppearancePrint.HeaderPanel.BackColor = System.Drawing.Color.Transparent
        Me.grv.AppearancePrint.HeaderPanel.BackColor2 = System.Drawing.Color.Transparent
        Me.grv.AppearancePrint.HeaderPanel.BorderColor = System.Drawing.Color.Transparent
        Me.grv.AppearancePrint.HeaderPanel.Font = New System.Drawing.Font("Tahoma", 8.25!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Underline), System.Drawing.FontStyle))
        Me.grv.AppearancePrint.HeaderPanel.Options.UseBackColor = True
        Me.grv.AppearancePrint.HeaderPanel.Options.UseBorderColor = True
        Me.grv.AppearancePrint.HeaderPanel.Options.UseFont = True
        Me.grv.AppearancePrint.HeaderPanel.Options.UseTextOptions = True
        Me.grv.AppearancePrint.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near
        Me.grv.AppearancePrint.Lines.BackColor = System.Drawing.Color.Transparent
        Me.grv.AppearancePrint.Lines.BackColor2 = System.Drawing.Color.Transparent
        Me.grv.AppearancePrint.Lines.BorderColor = System.Drawing.Color.Transparent
        Me.grv.AppearancePrint.Lines.Options.UseBackColor = True
        Me.grv.AppearancePrint.Lines.Options.UseBorderColor = True
        Me.grv.AppearancePrint.OddRow.BackColor = System.Drawing.Color.Transparent
        Me.grv.AppearancePrint.OddRow.BackColor2 = System.Drawing.Color.Transparent
        Me.grv.AppearancePrint.OddRow.BorderColor = System.Drawing.Color.Transparent
        Me.grv.AppearancePrint.OddRow.Options.UseBackColor = True
        Me.grv.AppearancePrint.OddRow.Options.UseBorderColor = True
        Me.grv.AppearancePrint.Preview.BackColor = System.Drawing.Color.Transparent
        Me.grv.AppearancePrint.Preview.BackColor2 = System.Drawing.Color.Transparent
        Me.grv.AppearancePrint.Preview.BorderColor = System.Drawing.Color.Transparent
        Me.grv.AppearancePrint.Preview.Options.UseBackColor = True
        Me.grv.AppearancePrint.Preview.Options.UseBorderColor = True
        Me.grv.AppearancePrint.Row.BackColor = System.Drawing.Color.Transparent
        Me.grv.AppearancePrint.Row.BackColor2 = System.Drawing.Color.Transparent
        Me.grv.AppearancePrint.Row.BorderColor = System.Drawing.Color.Transparent
        Me.grv.AppearancePrint.Row.Options.UseBackColor = True
        Me.grv.AppearancePrint.Row.Options.UseBorderColor = True
        Me.grv.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.grv.GridControl = Me.grd
        Me.grv.Name = "grv"
        Me.grv.OptionsBehavior.Editable = False
        Me.grv.OptionsBehavior.ReadOnly = True
        Me.grv.OptionsDetail.SmartDetailHeight = True
        Me.grv.OptionsPrint.EnableAppearanceEvenRow = True
        Me.grv.OptionsPrint.EnableAppearanceOddRow = True
        Me.grv.OptionsPrint.ExpandAllDetails = True
        Me.grv.OptionsPrint.PrintDetails = True
        Me.grv.OptionsPrint.PrintFilterInfo = True
        Me.grv.OptionsPrint.PrintHorzLines = False
        Me.grv.OptionsPrint.PrintVertLines = False
        Me.grv.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.grv.OptionsView.ShowFooter = True
        Me.grv.OptionsView.ShowGroupPanel = False
        '
        'layoutControl
        '
        Me.layoutControl.Controls.Add(Me.txtVENTILATOR)
        Me.layoutControl.Controls.Add(Me.LabelControl18)
        Me.layoutControl.Controls.Add(Me.txtRAWATINTENSIF_HARI)
        Me.layoutControl.Controls.Add(Me.LabelControl17)
        Me.layoutControl.Controls.Add(Me.LabelControl12)
        Me.layoutControl.Controls.Add(Me.txtLAMA)
        Me.layoutControl.Controls.Add(Me.LabelControl10)
        Me.layoutControl.Controls.Add(Me.LabelControl16)
        Me.layoutControl.Controls.Add(Me.LabelControl15)
        Me.layoutControl.Controls.Add(Me.LabelControl14)
        Me.layoutControl.Controls.Add(Me.LabelControl13)
        Me.layoutControl.Controls.Add(Me.LabelControl11)
        Me.layoutControl.Controls.Add(Me.LabelControl9)
        Me.layoutControl.Controls.Add(Me.LabelControl8)
        Me.layoutControl.Controls.Add(Me.LabelControl7)
        Me.layoutControl.Controls.Add(Me.LabelControl6)
        Me.layoutControl.Controls.Add(Me.LabelControl5)
        Me.layoutControl.Controls.Add(Me.rbKELASPELAYANAN)
        Me.layoutControl.Controls.Add(Me.chkNaikKelas)
        Me.layoutControl.Controls.Add(Me.chkAdaRawat)
        Me.layoutControl.Controls.Add(Me.rbKELASHAK)
        Me.layoutControl.Controls.Add(Me.rbCategory)
        Me.layoutControl.Controls.Add(Me.grdICD_IX)
        Me.layoutControl.Controls.Add(Me.grdICD_X)
        Me.layoutControl.Controls.Add(Me.grd)
        Me.layoutControl.Controls.Add(Me.grdJenisTarif)
        Me.layoutControl.Controls.Add(Me.txtHAKKELAS)
        Me.layoutControl.Controls.Add(Me.txtTarifEksekutif)
        Me.layoutControl.Controls.Add(Me.txtICCD_IX)
        Me.layoutControl.Controls.Add(Me.txtICD_X)
        Me.layoutControl.Controls.Add(Me.txtSewaAlat)
        Me.layoutControl.Controls.Add(Me.txtObatKemoTerapi)
        Me.layoutControl.Controls.Add(Me.txtRawatIntensif)
        Me.layoutControl.Controls.Add(Me.txtPelayananDarah)
        Me.layoutControl.Controls.Add(Me.txtPenunjang)
        Me.layoutControl.Controls.Add(Me.txtKonsultasi)
        Me.layoutControl.Controls.Add(Me.txtBMHP)
        Me.layoutControl.Controls.Add(Me.txtObatKronis)
        Me.layoutControl.Controls.Add(Me.txtKamarAkomodasi)
        Me.layoutControl.Controls.Add(Me.txtLaboratorium)
        Me.layoutControl.Controls.Add(Me.txtKeperawatan)
        Me.layoutControl.Controls.Add(Me.txtProsedurBedah)
        Me.layoutControl.Controls.Add(Me.txtAlkes)
        Me.layoutControl.Controls.Add(Me.txtObat)
        Me.layoutControl.Controls.Add(Me.txtRehabilitasi)
        Me.layoutControl.Controls.Add(Me.txtRadiologi)
        Me.layoutControl.Controls.Add(Me.txtTenagaAhli)
        Me.layoutControl.Controls.Add(Me.txtProsedurNonBedah)
        Me.layoutControl.Controls.Add(Me.txttarifRumahSakit)
        Me.layoutControl.Controls.Add(Me.grdCaraKeluar)
        Me.layoutControl.Controls.Add(Me.txtBeratBadan)
        Me.layoutControl.Controls.Add(Me.txtUmur)
        Me.layoutControl.Controls.Add(Me.txtADLScore_Chronic)
        Me.layoutControl.Controls.Add(Me.txtADLScore_SubAcute)
        Me.layoutControl.Controls.Add(Me.txtLOS)
        Me.layoutControl.Controls.Add(Me.chkKelasEksekutif)
        Me.layoutControl.Controls.Add(Me.LabelControl4)
        Me.layoutControl.Controls.Add(Me.LabelControl3)
        Me.layoutControl.Controls.Add(Me.LabelControl2)
        Me.layoutControl.Controls.Add(Me.LabelControl1)
        Me.layoutControl.Controls.Add(Me.grdCOB)
        Me.layoutControl.Controls.Add(Me.txtNoSEP)
        Me.layoutControl.Controls.Add(Me.txtNoPeserta)
        Me.layoutControl.Controls.Add(Me.cboCaraBayar)
        Me.layoutControl.Controls.Add(Me.txtCODE)
        Me.layoutControl.Controls.Add(Me.grdDPJP)
        Me.layoutControl.Controls.Add(Me.grdKDCASHIN)
        Me.layoutControl.Controls.Add(Me.cboCARI)
        Me.layoutControl.Controls.Add(Me.txtCARI)
        Me.layoutControl.Controls.Add(Me.deDATEPULANG)
        Me.layoutControl.Controls.Add(Me.deDATEMASUK)
        Me.layoutControl.Dock = System.Windows.Forms.DockStyle.Fill
        Me.layoutControl.Location = New System.Drawing.Point(0, 0)
        Me.layoutControl.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.layoutControl.Name = "layoutControl"
        Me.layoutControl.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = New System.Drawing.Rectangle(442, 268, 250, 350)
        Me.layoutControl.Root = Me.LayoutControlGroup1
        Me.layoutControl.Size = New System.Drawing.Size(1478, 715)
        Me.layoutControl.TabIndex = 0
        Me.layoutControl.Text = "LayoutControl1"
        '
        'txtVENTILATOR
        '
        Me.txtVENTILATOR.EditValue = "0"
        Me.txtVENTILATOR.Location = New System.Drawing.Point(760, 276)
        Me.txtVENTILATOR.MenuManager = Me.barManager
        Me.txtVENTILATOR.Name = "txtVENTILATOR"
        Me.txtVENTILATOR.Properties.Mask.EditMask = "n0"
        Me.txtVENTILATOR.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        Me.txtVENTILATOR.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.txtVENTILATOR.Size = New System.Drawing.Size(199, 22)
        Me.txtVENTILATOR.StyleController = Me.layoutControl
        Me.txtVENTILATOR.TabIndex = 51
        '
        'barManager
        '
        Me.barManager.AllowQuickCustomization = False
        Me.barManager.Bars.AddRange(New DevExpress.XtraBars.Bar() {Me.barTop})
        Me.barManager.DockControls.Add(Me.barDockControlTop)
        Me.barManager.DockControls.Add(Me.barDockControlBottom)
        Me.barManager.DockControls.Add(Me.barDockControlLeft)
        Me.barManager.DockControls.Add(Me.barDockControlRight)
        Me.barManager.Form = Me
        Me.barManager.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.btnSaveNew, Me.btnClose, Me.btnSaveClose, Me.btnICD, Me.btnProsedur, Me.btnUpdatePasien, Me.btnAddCustomer})
        Me.barManager.MainMenu = Me.barTop
        Me.barManager.MaxItemId = 12
        Me.barManager.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.progressBarSave, Me.progressSave})
        '
        'barTop
        '
        Me.barTop.BarName = "Main menu"
        Me.barTop.DockCol = 0
        Me.barTop.DockRow = 0
        Me.barTop.DockStyle = DevExpress.XtraBars.BarDockStyle.Bottom
        Me.barTop.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.btnSaveNew), New DevExpress.XtraBars.LinkPersistInfo(Me.btnSaveClose), New DevExpress.XtraBars.LinkPersistInfo(Me.btnAddCustomer), New DevExpress.XtraBars.LinkPersistInfo(Me.btnUpdatePasien), New DevExpress.XtraBars.LinkPersistInfo(Me.btnClose)})
        Me.barTop.OptionsBar.DrawDragBorder = False
        Me.barTop.OptionsBar.MultiLine = True
        Me.barTop.OptionsBar.UseWholeRow = True
        Me.barTop.Text = "Main menu"
        '
        'btnSaveNew
        '
        Me.btnSaveNew.Caption = "F2 - Save && New"
        Me.btnSaveNew.Id = 2
        Me.btnSaveNew.Name = "btnSaveNew"
        '
        'btnSaveClose
        '
        Me.btnSaveClose.Caption = "F3 - Save && Close"
        Me.btnSaveClose.Id = 5
        Me.btnSaveClose.Name = "btnSaveClose"
        '
        'btnAddCustomer
        '
        Me.btnAddCustomer.Caption = "F5 - Master Pasien"
        Me.btnAddCustomer.Id = 11
        Me.btnAddCustomer.Name = "btnAddCustomer"
        '
        'btnUpdatePasien
        '
        Me.btnUpdatePasien.Caption = "F6 - Update Pasien E Klaim"
        Me.btnUpdatePasien.Id = 10
        Me.btnUpdatePasien.Name = "btnUpdatePasien"
        '
        'btnClose
        '
        Me.btnClose.Caption = "F12 - Close"
        Me.btnClose.Id = 3
        Me.btnClose.Name = "btnClose"
        '
        'barDockControlTop
        '
        Me.barDockControlTop.CausesValidation = False
        Me.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top
        Me.barDockControlTop.Location = New System.Drawing.Point(0, 0)
        Me.barDockControlTop.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.barDockControlTop.Size = New System.Drawing.Size(1478, 0)
        '
        'barDockControlBottom
        '
        Me.barDockControlBottom.CausesValidation = False
        Me.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.barDockControlBottom.Location = New System.Drawing.Point(0, 715)
        Me.barDockControlBottom.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.barDockControlBottom.Size = New System.Drawing.Size(1478, 29)
        '
        'barDockControlLeft
        '
        Me.barDockControlLeft.CausesValidation = False
        Me.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left
        Me.barDockControlLeft.Location = New System.Drawing.Point(0, 0)
        Me.barDockControlLeft.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.barDockControlLeft.Size = New System.Drawing.Size(0, 715)
        '
        'barDockControlRight
        '
        Me.barDockControlRight.CausesValidation = False
        Me.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right
        Me.barDockControlRight.Location = New System.Drawing.Point(1478, 0)
        Me.barDockControlRight.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.barDockControlRight.Size = New System.Drawing.Size(0, 715)
        '
        'btnICD
        '
        Me.btnICD.Caption = "F5 - Diagnosa"
        Me.btnICD.Id = 8
        Me.btnICD.Name = "btnICD"
        '
        'btnProsedur
        '
        Me.btnProsedur.Caption = "F6 - Prosedur (ICD-9-CM)"
        Me.btnProsedur.Id = 9
        Me.btnProsedur.Name = "btnProsedur"
        '
        'progressBarSave
        '
        Me.progressBarSave.Name = "progressBarSave"
        Me.progressBarSave.Stopped = True
        '
        'progressSave
        '
        Me.progressSave.Name = "progressSave"
        Me.progressSave.Paused = True
        '
        'LabelControl18
        '
        Me.LabelControl18.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.LabelControl18.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.LabelControl18.Location = New System.Drawing.Point(631, 276)
        Me.LabelControl18.Name = "LabelControl18"
        Me.LabelControl18.Size = New System.Drawing.Size(123, 16)
        Me.LabelControl18.StyleController = Me.layoutControl
        Me.LabelControl18.TabIndex = 63
        Me.LabelControl18.Text = "Ventilator (jam) :"
        '
        'txtRAWATINTENSIF_HARI
        '
        Me.txtRAWATINTENSIF_HARI.EditValue = "0"
        Me.txtRAWATINTENSIF_HARI.Location = New System.Drawing.Point(167, 276)
        Me.txtRAWATINTENSIF_HARI.MenuManager = Me.barManager
        Me.txtRAWATINTENSIF_HARI.Name = "txtRAWATINTENSIF_HARI"
        Me.txtRAWATINTENSIF_HARI.Properties.Mask.EditMask = "n0"
        Me.txtRAWATINTENSIF_HARI.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        Me.txtRAWATINTENSIF_HARI.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.txtRAWATINTENSIF_HARI.Size = New System.Drawing.Size(458, 22)
        Me.txtRAWATINTENSIF_HARI.StyleController = Me.layoutControl
        Me.txtRAWATINTENSIF_HARI.TabIndex = 50
        '
        'LabelControl17
        '
        Me.LabelControl17.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.LabelControl17.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.LabelControl17.Location = New System.Drawing.Point(20, 276)
        Me.LabelControl17.Name = "LabelControl17"
        Me.LabelControl17.Size = New System.Drawing.Size(141, 16)
        Me.LabelControl17.StyleController = Me.layoutControl
        Me.LabelControl17.TabIndex = 62
        Me.LabelControl17.Text = "Rawat Intensif (hari) :"
        '
        'LabelControl12
        '
        Me.LabelControl12.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.LabelControl12.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.LabelControl12.Location = New System.Drawing.Point(16, 420)
        Me.LabelControl12.Name = "LabelControl12"
        Me.LabelControl12.Size = New System.Drawing.Size(455, 16)
        Me.LabelControl12.StyleController = Me.layoutControl
        Me.LabelControl12.TabIndex = 63
        Me.LabelControl12.Text = "Tarif Rumah sakit (Rp.) :"
        '
        'txtLAMA
        '
        Me.txtLAMA.EditValue = "0"
        Me.txtLAMA.Location = New System.Drawing.Point(760, 244)
        Me.txtLAMA.MenuManager = Me.barManager
        Me.txtLAMA.Name = "txtLAMA"
        Me.txtLAMA.Properties.Mask.EditMask = "n0"
        Me.txtLAMA.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        Me.txtLAMA.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.txtLAMA.Size = New System.Drawing.Size(199, 22)
        Me.txtLAMA.StyleController = Me.layoutControl
        Me.txtLAMA.TabIndex = 49
        '
        'LabelControl10
        '
        Me.LabelControl10.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.LabelControl10.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.LabelControl10.Location = New System.Drawing.Point(631, 244)
        Me.LabelControl10.Name = "LabelControl10"
        Me.LabelControl10.Size = New System.Drawing.Size(123, 16)
        Me.LabelControl10.StyleController = Me.layoutControl
        Me.LabelControl10.TabIndex = 63
        Me.LabelControl10.Text = "Lama (Hari) :"
        '
        'LabelControl16
        '
        Me.LabelControl16.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.LabelControl16.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.LabelControl16.Location = New System.Drawing.Point(631, 360)
        Me.LabelControl16.Name = "LabelControl16"
        Me.LabelControl16.Size = New System.Drawing.Size(123, 16)
        Me.LabelControl16.StyleController = Me.layoutControl
        Me.LabelControl16.TabIndex = 62
        Me.LabelControl16.Text = "Jenis Tarif :"
        '
        'LabelControl15
        '
        Me.LabelControl15.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.LabelControl15.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.LabelControl15.Location = New System.Drawing.Point(631, 332)
        Me.LabelControl15.Name = "LabelControl15"
        Me.LabelControl15.Size = New System.Drawing.Size(123, 16)
        Me.LabelControl15.StyleController = Me.layoutControl
        Me.LabelControl15.TabIndex = 62
        Me.LabelControl15.Text = "Cara Pulang :"
        '
        'LabelControl14
        '
        Me.LabelControl14.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.LabelControl14.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.LabelControl14.Location = New System.Drawing.Point(631, 304)
        Me.LabelControl14.Name = "LabelControl14"
        Me.LabelControl14.Size = New System.Drawing.Size(123, 16)
        Me.LabelControl14.StyleController = Me.layoutControl
        Me.LabelControl14.TabIndex = 62
        Me.LabelControl14.Text = "Berat Lahir (gram) :"
        '
        'LabelControl13
        '
        Me.LabelControl13.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.LabelControl13.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.LabelControl13.Location = New System.Drawing.Point(631, 216)
        Me.LabelControl13.Name = "LabelControl13"
        Me.LabelControl13.Size = New System.Drawing.Size(123, 16)
        Me.LabelControl13.StyleController = Me.layoutControl
        Me.LabelControl13.TabIndex = 62
        Me.LabelControl13.Text = "Umur :"
        '
        'LabelControl11
        '
        Me.LabelControl11.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.LabelControl11.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.LabelControl11.Location = New System.Drawing.Point(631, 184)
        Me.LabelControl11.Name = "LabelControl11"
        Me.LabelControl11.Size = New System.Drawing.Size(123, 16)
        Me.LabelControl11.StyleController = Me.layoutControl
        Me.LabelControl11.TabIndex = 61
        Me.LabelControl11.Text = "Kelas Hak :"
        '
        'LabelControl9
        '
        Me.LabelControl9.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.LabelControl9.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.LabelControl9.Location = New System.Drawing.Point(20, 360)
        Me.LabelControl9.Name = "LabelControl9"
        Me.LabelControl9.Size = New System.Drawing.Size(141, 16)
        Me.LabelControl9.StyleController = Me.layoutControl
        Me.LabelControl9.TabIndex = 61
        Me.LabelControl9.Text = "DPJP :"
        '
        'LabelControl8
        '
        Me.LabelControl8.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.LabelControl8.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.LabelControl8.Location = New System.Drawing.Point(20, 332)
        Me.LabelControl8.Name = "LabelControl8"
        Me.LabelControl8.Size = New System.Drawing.Size(141, 16)
        Me.LabelControl8.StyleController = Me.layoutControl
        Me.LabelControl8.TabIndex = 61
        Me.LabelControl8.Text = "ADL Score :"
        '
        'LabelControl7
        '
        Me.LabelControl7.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.LabelControl7.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.LabelControl7.Location = New System.Drawing.Point(20, 304)
        Me.LabelControl7.Name = "LabelControl7"
        Me.LabelControl7.Size = New System.Drawing.Size(141, 16)
        Me.LabelControl7.StyleController = Me.layoutControl
        Me.LabelControl7.TabIndex = 61
        Me.LabelControl7.Text = "LOS (Hari) :"
        '
        'LabelControl6
        '
        Me.LabelControl6.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.LabelControl6.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.LabelControl6.Location = New System.Drawing.Point(20, 244)
        Me.LabelControl6.Name = "LabelControl6"
        Me.LabelControl6.Size = New System.Drawing.Size(141, 16)
        Me.LabelControl6.StyleController = Me.layoutControl
        Me.LabelControl6.TabIndex = 61
        Me.LabelControl6.Text = "Kelas Pelayanan :"
        '
        'LabelControl5
        '
        Me.LabelControl5.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.LabelControl5.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.LabelControl5.Location = New System.Drawing.Point(20, 216)
        Me.LabelControl5.Name = "LabelControl5"
        Me.LabelControl5.Size = New System.Drawing.Size(141, 16)
        Me.LabelControl5.StyleController = Me.layoutControl
        Me.LabelControl5.TabIndex = 60
        Me.LabelControl5.Text = "Tanggal Rawat :"
        '
        'rbKELASPELAYANAN
        '
        Me.rbKELASPELAYANAN.Location = New System.Drawing.Point(167, 244)
        Me.rbKELASPELAYANAN.MenuManager = Me.barManager
        Me.rbKELASPELAYANAN.Name = "rbKELASPELAYANAN"
        Me.rbKELASPELAYANAN.Properties.Items.AddRange(New DevExpress.XtraEditors.Controls.RadioGroupItem() {New DevExpress.XtraEditors.Controls.RadioGroupItem(Nothing, "Kelas 3"), New DevExpress.XtraEditors.Controls.RadioGroupItem(Nothing, "Kelas 2"), New DevExpress.XtraEditors.Controls.RadioGroupItem(Nothing, "Kelas 1"), New DevExpress.XtraEditors.Controls.RadioGroupItem(Nothing, "Kelas VIP"), New DevExpress.XtraEditors.Controls.RadioGroupItem(Nothing, "Kelas VVIP")})
        Me.rbKELASPELAYANAN.Size = New System.Drawing.Size(458, 26)
        Me.rbKELASPELAYANAN.StyleController = Me.layoutControl
        Me.rbKELASPELAYANAN.TabIndex = 58
        '
        'chkNaikKelas
        '
        Me.chkNaikKelas.Location = New System.Drawing.Point(20, 184)
        Me.chkNaikKelas.MenuManager = Me.barManager
        Me.chkNaikKelas.Name = "chkNaikKelas"
        Me.chkNaikKelas.Properties.Caption = "Naik/Turun Kelas"
        Me.chkNaikKelas.Size = New System.Drawing.Size(121, 20)
        Me.chkNaikKelas.StyleController = Me.layoutControl
        Me.chkNaikKelas.TabIndex = 59
        '
        'chkAdaRawat
        '
        Me.chkAdaRawat.Location = New System.Drawing.Point(147, 184)
        Me.chkAdaRawat.MenuManager = Me.barManager
        Me.chkAdaRawat.Name = "chkAdaRawat"
        Me.chkAdaRawat.Properties.Caption = "Ada Rawat Intensif"
        Me.chkAdaRawat.Size = New System.Drawing.Size(131, 20)
        Me.chkAdaRawat.StyleController = Me.layoutControl
        Me.chkAdaRawat.TabIndex = 58
        '
        'rbKELASHAK
        '
        Me.rbKELASHAK.Location = New System.Drawing.Point(760, 184)
        Me.rbKELASHAK.MenuManager = Me.barManager
        Me.rbKELASHAK.Name = "rbKELASHAK"
        Me.rbKELASHAK.Properties.Items.AddRange(New DevExpress.XtraEditors.Controls.RadioGroupItem() {New DevExpress.XtraEditors.Controls.RadioGroupItem(Nothing, "Kelas 3"), New DevExpress.XtraEditors.Controls.RadioGroupItem(Nothing, "Kelas 2"), New DevExpress.XtraEditors.Controls.RadioGroupItem(Nothing, "Kelas 1")})
        Me.rbKELASHAK.Size = New System.Drawing.Size(143, 26)
        Me.rbKELASHAK.StyleController = Me.layoutControl
        Me.rbKELASHAK.TabIndex = 57
        '
        'rbCategory
        '
        Me.rbCategory.Location = New System.Drawing.Point(298, 16)
        Me.rbCategory.MenuManager = Me.barManager
        Me.rbCategory.Name = "rbCategory"
        Me.rbCategory.Properties.Items.AddRange(New DevExpress.XtraEditors.Controls.RadioGroupItem() {New DevExpress.XtraEditors.Controls.RadioGroupItem(Nothing, "Rawat Jalan"), New DevExpress.XtraEditors.Controls.RadioGroupItem(Nothing, "Rawat Inap")})
        Me.rbCategory.Size = New System.Drawing.Size(665, 26)
        Me.rbCategory.StyleController = Me.layoutControl
        Me.rbCategory.TabIndex = 56
        '
        'grdICD_IX
        '
        Me.grdICD_IX.EnterMoveNextControl = True
        Me.grdICD_IX.Location = New System.Drawing.Point(632, 647)
        Me.grdICD_IX.MenuManager = Me.barManager
        Me.grdICD_IX.Name = "grdICD_IX"
        Me.grdICD_IX.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.grdICD_IX.Properties.NullText = ""
        Me.grdICD_IX.Properties.PopupFormMinSize = New System.Drawing.Size(600, 300)
        Me.grdICD_IX.Properties.View = Me.GridView5
        Me.grdICD_IX.Size = New System.Drawing.Size(331, 22)
        Me.grdICD_IX.StyleController = Me.layoutControl
        Me.grdICD_IX.TabIndex = 45
        '
        'GridView5
        '
        Me.GridView5.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn19})
        Me.GridView5.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.GridView5.Name = "GridView5"
        Me.GridView5.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.GridView5.OptionsView.ShowAutoFilterRow = True
        Me.GridView5.OptionsView.ShowGroupPanel = False
        '
        'GridColumn19
        '
        Me.GridColumn19.Caption = "Name Display"
        Me.GridColumn19.FieldName = "MEMO"
        Me.GridColumn19.Name = "GridColumn19"
        Me.GridColumn19.Visible = True
        Me.GridColumn19.VisibleIndex = 0
        '
        'grdICD_X
        '
        Me.grdICD_X.EnterMoveNextControl = True
        Me.grdICD_X.Location = New System.Drawing.Point(171, 647)
        Me.grdICD_X.MenuManager = Me.barManager
        Me.grdICD_X.Name = "grdICD_X"
        Me.grdICD_X.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.grdICD_X.Properties.NullText = ""
        Me.grdICD_X.Properties.PopupFormMinSize = New System.Drawing.Size(600, 300)
        Me.grdICD_X.Properties.View = Me.GridView3
        Me.grdICD_X.Size = New System.Drawing.Size(300, 22)
        Me.grdICD_X.StyleController = Me.layoutControl
        Me.grdICD_X.TabIndex = 45
        '
        'GridView3
        '
        Me.GridView3.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn18})
        Me.GridView3.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.GridView3.Name = "GridView3"
        Me.GridView3.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.GridView3.OptionsView.ShowAutoFilterRow = True
        Me.GridView3.OptionsView.ShowGroupPanel = False
        '
        'GridColumn18
        '
        Me.GridColumn18.Caption = "Name Display"
        Me.GridColumn18.FieldName = "MEMO"
        Me.GridColumn18.Name = "GridColumn18"
        Me.GridColumn18.Visible = True
        Me.GridColumn18.VisibleIndex = 0
        '
        'grdJenisTarif
        '
        Me.grdJenisTarif.EnterMoveNextControl = True
        Me.grdJenisTarif.Location = New System.Drawing.Point(760, 360)
        Me.grdJenisTarif.MenuManager = Me.barManager
        Me.grdJenisTarif.Name = "grdJenisTarif"
        Me.grdJenisTarif.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.grdJenisTarif.Properties.NullText = ""
        Me.grdJenisTarif.Properties.PopupFormMinSize = New System.Drawing.Size(600, 300)
        Me.grdJenisTarif.Properties.View = Me.GridView1
        Me.grdJenisTarif.Size = New System.Drawing.Size(199, 22)
        Me.grdJenisTarif.StyleController = Me.layoutControl
        Me.grdJenisTarif.TabIndex = 45
        '
        'GridView1
        '
        Me.GridView1.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn8})
        Me.GridView1.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.GridView1.Name = "GridView1"
        Me.GridView1.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.GridView1.OptionsView.ShowAutoFilterRow = True
        Me.GridView1.OptionsView.ShowGroupPanel = False
        '
        'GridColumn8
        '
        Me.GridColumn8.Caption = "Name Display"
        Me.GridColumn8.FieldName = "MEMO"
        Me.GridColumn8.Name = "GridColumn8"
        Me.GridColumn8.Visible = True
        Me.GridColumn8.VisibleIndex = 0
        '
        'txtHAKKELAS
        '
        Me.txtHAKKELAS.EditValue = "-"
        Me.txtHAKKELAS.Location = New System.Drawing.Point(909, 184)
        Me.txtHAKKELAS.MenuManager = Me.barManager
        Me.txtHAKKELAS.Name = "txtHAKKELAS"
        Me.txtHAKKELAS.Properties.ReadOnly = True
        Me.txtHAKKELAS.Size = New System.Drawing.Size(50, 22)
        Me.txtHAKKELAS.StyleController = Me.layoutControl
        Me.txtHAKKELAS.TabIndex = 49
        '
        'txtTarifEksekutif
        '
        Me.txtTarifEksekutif.EditValue = "0"
        Me.txtTarifEksekutif.Location = New System.Drawing.Point(885, 388)
        Me.txtTarifEksekutif.MenuManager = Me.barManager
        Me.txtTarifEksekutif.Name = "txtTarifEksekutif"
        Me.txtTarifEksekutif.Properties.Mask.EditMask = "n0"
        Me.txtTarifEksekutif.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        Me.txtTarifEksekutif.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.txtTarifEksekutif.Size = New System.Drawing.Size(74, 22)
        Me.txtTarifEksekutif.StyleController = Me.layoutControl
        Me.txtTarifEksekutif.TabIndex = 50
        '
        'txtICCD_IX
        '
        Me.txtICCD_IX.Location = New System.Drawing.Point(632, 675)
        Me.txtICCD_IX.MenuManager = Me.barManager
        Me.txtICCD_IX.Name = "txtICCD_IX"
        Me.txtICCD_IX.Size = New System.Drawing.Size(331, 22)
        Me.txtICCD_IX.StyleController = Me.layoutControl
        Me.txtICCD_IX.TabIndex = 54
        '
        'txtICD_X
        '
        Me.txtICD_X.Location = New System.Drawing.Point(171, 675)
        Me.txtICD_X.MenuManager = Me.barManager
        Me.txtICD_X.Name = "txtICD_X"
        Me.txtICD_X.Size = New System.Drawing.Size(300, 22)
        Me.txtICD_X.StyleController = Me.layoutControl
        Me.txtICD_X.TabIndex = 53
        '
        'txtSewaAlat
        '
        Me.txtSewaAlat.EditValue = "0"
        Me.txtSewaAlat.Location = New System.Drawing.Point(780, 615)
        Me.txtSewaAlat.MenuManager = Me.barManager
        Me.txtSewaAlat.Name = "txtSewaAlat"
        Me.txtSewaAlat.Properties.Appearance.Options.UseTextOptions = True
        Me.txtSewaAlat.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.txtSewaAlat.Properties.Mask.EditMask = "n0"
        Me.txtSewaAlat.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        Me.txtSewaAlat.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.txtSewaAlat.Size = New System.Drawing.Size(179, 22)
        Me.txtSewaAlat.StyleController = Me.layoutControl
        Me.txtSewaAlat.TabIndex = 49
        '
        'txtObatKemoTerapi
        '
        Me.txtObatKemoTerapi.EditValue = "0"
        Me.txtObatKemoTerapi.Location = New System.Drawing.Point(780, 587)
        Me.txtObatKemoTerapi.MenuManager = Me.barManager
        Me.txtObatKemoTerapi.Name = "txtObatKemoTerapi"
        Me.txtObatKemoTerapi.Properties.Appearance.Options.UseTextOptions = True
        Me.txtObatKemoTerapi.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.txtObatKemoTerapi.Properties.Mask.EditMask = "n0"
        Me.txtObatKemoTerapi.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        Me.txtObatKemoTerapi.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.txtObatKemoTerapi.Size = New System.Drawing.Size(179, 22)
        Me.txtObatKemoTerapi.StyleController = Me.layoutControl
        Me.txtObatKemoTerapi.TabIndex = 49
        '
        'txtRawatIntensif
        '
        Me.txtRawatIntensif.EditValue = "0"
        Me.txtRawatIntensif.Location = New System.Drawing.Point(780, 559)
        Me.txtRawatIntensif.MenuManager = Me.barManager
        Me.txtRawatIntensif.Name = "txtRawatIntensif"
        Me.txtRawatIntensif.Properties.Appearance.Options.UseTextOptions = True
        Me.txtRawatIntensif.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.txtRawatIntensif.Properties.Mask.EditMask = "n0"
        Me.txtRawatIntensif.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        Me.txtRawatIntensif.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.txtRawatIntensif.Size = New System.Drawing.Size(179, 22)
        Me.txtRawatIntensif.StyleController = Me.layoutControl
        Me.txtRawatIntensif.TabIndex = 49
        '
        'txtPelayananDarah
        '
        Me.txtPelayananDarah.EditValue = "0"
        Me.txtPelayananDarah.Location = New System.Drawing.Point(780, 531)
        Me.txtPelayananDarah.MenuManager = Me.barManager
        Me.txtPelayananDarah.Name = "txtPelayananDarah"
        Me.txtPelayananDarah.Properties.Appearance.Options.UseTextOptions = True
        Me.txtPelayananDarah.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.txtPelayananDarah.Properties.Mask.EditMask = "n0"
        Me.txtPelayananDarah.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        Me.txtPelayananDarah.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.txtPelayananDarah.Size = New System.Drawing.Size(179, 22)
        Me.txtPelayananDarah.StyleController = Me.layoutControl
        Me.txtPelayananDarah.TabIndex = 49
        '
        'txtPenunjang
        '
        Me.txtPenunjang.EditValue = "0"
        Me.txtPenunjang.Location = New System.Drawing.Point(780, 503)
        Me.txtPenunjang.MenuManager = Me.barManager
        Me.txtPenunjang.Name = "txtPenunjang"
        Me.txtPenunjang.Properties.Appearance.Options.UseTextOptions = True
        Me.txtPenunjang.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.txtPenunjang.Properties.Mask.EditMask = "n0"
        Me.txtPenunjang.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        Me.txtPenunjang.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.txtPenunjang.Size = New System.Drawing.Size(179, 22)
        Me.txtPenunjang.StyleController = Me.layoutControl
        Me.txtPenunjang.TabIndex = 49
        '
        'txtKonsultasi
        '
        Me.txtKonsultasi.EditValue = "0"
        Me.txtKonsultasi.Location = New System.Drawing.Point(780, 475)
        Me.txtKonsultasi.MenuManager = Me.barManager
        Me.txtKonsultasi.Name = "txtKonsultasi"
        Me.txtKonsultasi.Properties.Appearance.Options.UseTextOptions = True
        Me.txtKonsultasi.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.txtKonsultasi.Properties.Mask.EditMask = "n0"
        Me.txtKonsultasi.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        Me.txtKonsultasi.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.txtKonsultasi.Size = New System.Drawing.Size(179, 22)
        Me.txtKonsultasi.StyleController = Me.layoutControl
        Me.txtKonsultasi.TabIndex = 49
        '
        'txtBMHP
        '
        Me.txtBMHP.EditValue = "0"
        Me.txtBMHP.Location = New System.Drawing.Point(476, 615)
        Me.txtBMHP.MenuManager = Me.barManager
        Me.txtBMHP.Name = "txtBMHP"
        Me.txtBMHP.Properties.Appearance.Options.UseTextOptions = True
        Me.txtBMHP.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.txtBMHP.Properties.Mask.EditMask = "n0"
        Me.txtBMHP.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        Me.txtBMHP.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.txtBMHP.Size = New System.Drawing.Size(163, 22)
        Me.txtBMHP.StyleController = Me.layoutControl
        Me.txtBMHP.TabIndex = 49
        '
        'txtObatKronis
        '
        Me.txtObatKronis.EditValue = "0"
        Me.txtObatKronis.Location = New System.Drawing.Point(476, 587)
        Me.txtObatKronis.MenuManager = Me.barManager
        Me.txtObatKronis.Name = "txtObatKronis"
        Me.txtObatKronis.Properties.Appearance.Options.UseTextOptions = True
        Me.txtObatKronis.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.txtObatKronis.Properties.Mask.EditMask = "n0"
        Me.txtObatKronis.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        Me.txtObatKronis.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.txtObatKronis.Size = New System.Drawing.Size(163, 22)
        Me.txtObatKronis.StyleController = Me.layoutControl
        Me.txtObatKronis.TabIndex = 49
        '
        'txtKamarAkomodasi
        '
        Me.txtKamarAkomodasi.EditValue = "0"
        Me.txtKamarAkomodasi.Location = New System.Drawing.Point(476, 559)
        Me.txtKamarAkomodasi.MenuManager = Me.barManager
        Me.txtKamarAkomodasi.Name = "txtKamarAkomodasi"
        Me.txtKamarAkomodasi.Properties.Appearance.Options.UseTextOptions = True
        Me.txtKamarAkomodasi.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.txtKamarAkomodasi.Properties.Mask.EditMask = "n0"
        Me.txtKamarAkomodasi.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        Me.txtKamarAkomodasi.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.txtKamarAkomodasi.Size = New System.Drawing.Size(163, 22)
        Me.txtKamarAkomodasi.StyleController = Me.layoutControl
        Me.txtKamarAkomodasi.TabIndex = 49
        '
        'txtLaboratorium
        '
        Me.txtLaboratorium.EditValue = "0"
        Me.txtLaboratorium.Location = New System.Drawing.Point(476, 531)
        Me.txtLaboratorium.MenuManager = Me.barManager
        Me.txtLaboratorium.Name = "txtLaboratorium"
        Me.txtLaboratorium.Properties.Appearance.Options.UseTextOptions = True
        Me.txtLaboratorium.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.txtLaboratorium.Properties.Mask.EditMask = "n0"
        Me.txtLaboratorium.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        Me.txtLaboratorium.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.txtLaboratorium.Size = New System.Drawing.Size(163, 22)
        Me.txtLaboratorium.StyleController = Me.layoutControl
        Me.txtLaboratorium.TabIndex = 49
        '
        'txtKeperawatan
        '
        Me.txtKeperawatan.EditValue = "0"
        Me.txtKeperawatan.Location = New System.Drawing.Point(476, 503)
        Me.txtKeperawatan.MenuManager = Me.barManager
        Me.txtKeperawatan.Name = "txtKeperawatan"
        Me.txtKeperawatan.Properties.Appearance.Options.UseTextOptions = True
        Me.txtKeperawatan.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.txtKeperawatan.Properties.Mask.EditMask = "n0"
        Me.txtKeperawatan.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        Me.txtKeperawatan.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.txtKeperawatan.Size = New System.Drawing.Size(163, 22)
        Me.txtKeperawatan.StyleController = Me.layoutControl
        Me.txtKeperawatan.TabIndex = 49
        '
        'txtProsedurBedah
        '
        Me.txtProsedurBedah.EditValue = "0"
        Me.txtProsedurBedah.Location = New System.Drawing.Point(476, 475)
        Me.txtProsedurBedah.MenuManager = Me.barManager
        Me.txtProsedurBedah.Name = "txtProsedurBedah"
        Me.txtProsedurBedah.Properties.Appearance.Options.UseTextOptions = True
        Me.txtProsedurBedah.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.txtProsedurBedah.Properties.Mask.EditMask = "n0"
        Me.txtProsedurBedah.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        Me.txtProsedurBedah.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.txtProsedurBedah.Size = New System.Drawing.Size(163, 22)
        Me.txtProsedurBedah.StyleController = Me.layoutControl
        Me.txtProsedurBedah.TabIndex = 49
        '
        'txtAlkes
        '
        Me.txtAlkes.EditValue = "0"
        Me.txtAlkes.Location = New System.Drawing.Point(155, 615)
        Me.txtAlkes.MenuManager = Me.barManager
        Me.txtAlkes.Name = "txtAlkes"
        Me.txtAlkes.Properties.Appearance.Options.UseTextOptions = True
        Me.txtAlkes.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.txtAlkes.Properties.Mask.EditMask = "n0"
        Me.txtAlkes.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        Me.txtAlkes.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.txtAlkes.Size = New System.Drawing.Size(180, 22)
        Me.txtAlkes.StyleController = Me.layoutControl
        Me.txtAlkes.TabIndex = 49
        '
        'txtObat
        '
        Me.txtObat.EditValue = "0"
        Me.txtObat.Location = New System.Drawing.Point(155, 587)
        Me.txtObat.MenuManager = Me.barManager
        Me.txtObat.Name = "txtObat"
        Me.txtObat.Properties.Appearance.Options.UseTextOptions = True
        Me.txtObat.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.txtObat.Properties.Mask.EditMask = "n0"
        Me.txtObat.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        Me.txtObat.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.txtObat.Size = New System.Drawing.Size(180, 22)
        Me.txtObat.StyleController = Me.layoutControl
        Me.txtObat.TabIndex = 49
        '
        'txtRehabilitasi
        '
        Me.txtRehabilitasi.EditValue = "0"
        Me.txtRehabilitasi.Location = New System.Drawing.Point(155, 559)
        Me.txtRehabilitasi.MenuManager = Me.barManager
        Me.txtRehabilitasi.Name = "txtRehabilitasi"
        Me.txtRehabilitasi.Properties.Appearance.Options.UseTextOptions = True
        Me.txtRehabilitasi.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.txtRehabilitasi.Properties.Mask.EditMask = "n0"
        Me.txtRehabilitasi.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        Me.txtRehabilitasi.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.txtRehabilitasi.Size = New System.Drawing.Size(180, 22)
        Me.txtRehabilitasi.StyleController = Me.layoutControl
        Me.txtRehabilitasi.TabIndex = 49
        '
        'txtRadiologi
        '
        Me.txtRadiologi.EditValue = "0"
        Me.txtRadiologi.Location = New System.Drawing.Point(155, 531)
        Me.txtRadiologi.MenuManager = Me.barManager
        Me.txtRadiologi.Name = "txtRadiologi"
        Me.txtRadiologi.Properties.Appearance.Options.UseTextOptions = True
        Me.txtRadiologi.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.txtRadiologi.Properties.Mask.EditMask = "n0"
        Me.txtRadiologi.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        Me.txtRadiologi.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.txtRadiologi.Size = New System.Drawing.Size(180, 22)
        Me.txtRadiologi.StyleController = Me.layoutControl
        Me.txtRadiologi.TabIndex = 49
        '
        'txtTenagaAhli
        '
        Me.txtTenagaAhli.EditValue = "0"
        Me.txtTenagaAhli.Location = New System.Drawing.Point(155, 503)
        Me.txtTenagaAhli.MenuManager = Me.barManager
        Me.txtTenagaAhli.Name = "txtTenagaAhli"
        Me.txtTenagaAhli.Properties.Appearance.Options.UseTextOptions = True
        Me.txtTenagaAhli.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.txtTenagaAhli.Properties.Mask.EditMask = "n0"
        Me.txtTenagaAhli.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        Me.txtTenagaAhli.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.txtTenagaAhli.Size = New System.Drawing.Size(180, 22)
        Me.txtTenagaAhli.StyleController = Me.layoutControl
        Me.txtTenagaAhli.TabIndex = 49
        '
        'txtProsedurNonBedah
        '
        Me.txtProsedurNonBedah.EditValue = "0"
        Me.txtProsedurNonBedah.Location = New System.Drawing.Point(155, 475)
        Me.txtProsedurNonBedah.MenuManager = Me.barManager
        Me.txtProsedurNonBedah.Name = "txtProsedurNonBedah"
        Me.txtProsedurNonBedah.Properties.Appearance.Options.UseTextOptions = True
        Me.txtProsedurNonBedah.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.txtProsedurNonBedah.Properties.Mask.EditMask = "n0"
        Me.txtProsedurNonBedah.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        Me.txtProsedurNonBedah.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.txtProsedurNonBedah.Size = New System.Drawing.Size(180, 22)
        Me.txtProsedurNonBedah.StyleController = Me.layoutControl
        Me.txtProsedurNonBedah.TabIndex = 49
        '
        'txttarifRumahSakit
        '
        Me.txttarifRumahSakit.EditValue = "0"
        Me.txttarifRumahSakit.Location = New System.Drawing.Point(477, 420)
        Me.txttarifRumahSakit.MenuManager = Me.barManager
        Me.txttarifRumahSakit.Name = "txttarifRumahSakit"
        Me.txttarifRumahSakit.Properties.Appearance.Options.UseTextOptions = True
        Me.txttarifRumahSakit.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.txttarifRumahSakit.Properties.Mask.EditMask = "n0"
        Me.txttarifRumahSakit.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        Me.txttarifRumahSakit.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.txttarifRumahSakit.Size = New System.Drawing.Size(158, 22)
        Me.txttarifRumahSakit.StyleController = Me.layoutControl
        Me.txttarifRumahSakit.TabIndex = 48
        '
        'grdCaraKeluar
        '
        Me.grdCaraKeluar.EnterMoveNextControl = True
        Me.grdCaraKeluar.Location = New System.Drawing.Point(760, 332)
        Me.grdCaraKeluar.MenuManager = Me.barManager
        Me.grdCaraKeluar.Name = "grdCaraKeluar"
        Me.grdCaraKeluar.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.grdCaraKeluar.Properties.NullText = ""
        Me.grdCaraKeluar.Properties.PopupFormMinSize = New System.Drawing.Size(600, 300)
        Me.grdCaraKeluar.Properties.View = Me.grvCaraKeluar
        Me.grdCaraKeluar.Size = New System.Drawing.Size(199, 22)
        Me.grdCaraKeluar.StyleController = Me.layoutControl
        Me.grdCaraKeluar.TabIndex = 41
        '
        'grvCaraKeluar
        '
        Me.grvCaraKeluar.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn6})
        Me.grvCaraKeluar.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.grvCaraKeluar.Name = "grvCaraKeluar"
        Me.grvCaraKeluar.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.grvCaraKeluar.OptionsView.ShowAutoFilterRow = True
        Me.grvCaraKeluar.OptionsView.ShowGroupPanel = False
        '
        'GridColumn6
        '
        Me.GridColumn6.Caption = "Name Display"
        Me.GridColumn6.FieldName = "MEMO"
        Me.GridColumn6.Name = "GridColumn6"
        Me.GridColumn6.Visible = True
        Me.GridColumn6.VisibleIndex = 0
        '
        'txtBeratBadan
        '
        Me.txtBeratBadan.EditValue = "0"
        Me.txtBeratBadan.Location = New System.Drawing.Point(760, 304)
        Me.txtBeratBadan.MenuManager = Me.barManager
        Me.txtBeratBadan.Name = "txtBeratBadan"
        Me.txtBeratBadan.Properties.Mask.EditMask = "n0"
        Me.txtBeratBadan.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        Me.txtBeratBadan.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.txtBeratBadan.Size = New System.Drawing.Size(199, 22)
        Me.txtBeratBadan.StyleController = Me.layoutControl
        Me.txtBeratBadan.TabIndex = 48
        '
        'txtUmur
        '
        Me.txtUmur.EditValue = "0 hari"
        Me.txtUmur.Location = New System.Drawing.Point(760, 216)
        Me.txtUmur.MenuManager = Me.barManager
        Me.txtUmur.Name = "txtUmur"
        Me.txtUmur.Size = New System.Drawing.Size(199, 22)
        Me.txtUmur.StyleController = Me.layoutControl
        Me.txtUmur.TabIndex = 48
        '
        'txtADLScore_Chronic
        '
        Me.txtADLScore_Chronic.EditValue = "-"
        Me.txtADLScore_Chronic.Location = New System.Drawing.Point(422, 332)
        Me.txtADLScore_Chronic.MenuManager = Me.barManager
        Me.txtADLScore_Chronic.Name = "txtADLScore_Chronic"
        Me.txtADLScore_Chronic.Size = New System.Drawing.Size(203, 22)
        Me.txtADLScore_Chronic.StyleController = Me.layoutControl
        Me.txtADLScore_Chronic.TabIndex = 47
        '
        'txtADLScore_SubAcute
        '
        Me.txtADLScore_SubAcute.EditValue = "-"
        Me.txtADLScore_SubAcute.Location = New System.Drawing.Point(239, 332)
        Me.txtADLScore_SubAcute.MenuManager = Me.barManager
        Me.txtADLScore_SubAcute.Name = "txtADLScore_SubAcute"
        Me.txtADLScore_SubAcute.Size = New System.Drawing.Size(120, 22)
        Me.txtADLScore_SubAcute.StyleController = Me.layoutControl
        Me.txtADLScore_SubAcute.TabIndex = 47
        '
        'txtLOS
        '
        Me.txtLOS.EditValue = "1"
        Me.txtLOS.Location = New System.Drawing.Point(167, 304)
        Me.txtLOS.MenuManager = Me.barManager
        Me.txtLOS.Name = "txtLOS"
        Me.txtLOS.Properties.Mask.EditMask = "n0"
        Me.txtLOS.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        Me.txtLOS.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.txtLOS.Size = New System.Drawing.Size(458, 22)
        Me.txtLOS.StyleController = Me.layoutControl
        Me.txtLOS.TabIndex = 47
        '
        'chkKelasEksekutif
        '
        Me.chkKelasEksekutif.Location = New System.Drawing.Point(284, 184)
        Me.chkKelasEksekutif.MenuManager = Me.barManager
        Me.chkKelasEksekutif.Name = "chkKelasEksekutif"
        Me.chkKelasEksekutif.Properties.Caption = "Kelas Eksekutif"
        Me.chkKelasEksekutif.Size = New System.Drawing.Size(341, 20)
        Me.chkKelasEksekutif.StyleController = Me.layoutControl
        Me.chkKelasEksekutif.TabIndex = 50
        '
        'LabelControl4
        '
        Me.LabelControl4.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.LabelControl4.Location = New System.Drawing.Point(714, 103)
        Me.LabelControl4.Name = "LabelControl4"
        Me.LabelControl4.Size = New System.Drawing.Size(245, 16)
        Me.LabelControl4.StyleController = Me.layoutControl
        Me.LabelControl4.TabIndex = 49
        Me.LabelControl4.Text = "COB "
        '
        'LabelControl3
        '
        Me.LabelControl3.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.LabelControl3.Location = New System.Drawing.Point(438, 103)
        Me.LabelControl3.Name = "LabelControl3"
        Me.LabelControl3.Size = New System.Drawing.Size(270, 16)
        Me.LabelControl3.StyleController = Me.layoutControl
        Me.LabelControl3.TabIndex = 49
        Me.LabelControl3.Text = "No. SEP"
        '
        'LabelControl2
        '
        Me.LabelControl2.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.LabelControl2.Location = New System.Drawing.Point(218, 103)
        Me.LabelControl2.Name = "LabelControl2"
        Me.LabelControl2.Size = New System.Drawing.Size(214, 16)
        Me.LabelControl2.StyleController = Me.layoutControl
        Me.LabelControl2.TabIndex = 49
        Me.LabelControl2.Text = "No. Peserta"
        '
        'LabelControl1
        '
        Me.LabelControl1.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.LabelControl1.Location = New System.Drawing.Point(20, 103)
        Me.LabelControl1.Name = "LabelControl1"
        Me.LabelControl1.Size = New System.Drawing.Size(192, 16)
        Me.LabelControl1.StyleController = Me.layoutControl
        Me.LabelControl1.TabIndex = 48
        Me.LabelControl1.Text = "Jaminan / Cara Bayar"
        '
        'grdCOB
        '
        Me.grdCOB.EnterMoveNextControl = True
        Me.grdCOB.Location = New System.Drawing.Point(714, 125)
        Me.grdCOB.MenuManager = Me.barManager
        Me.grdCOB.Name = "grdCOB"
        Me.grdCOB.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.grdCOB.Properties.NullText = ""
        Me.grdCOB.Properties.PopupFormMinSize = New System.Drawing.Size(600, 300)
        Me.grdCOB.Properties.View = Me.GridView2
        Me.grdCOB.Size = New System.Drawing.Size(245, 22)
        Me.grdCOB.StyleController = Me.layoutControl
        Me.grdCOB.TabIndex = 44
        '
        'GridView2
        '
        Me.GridView2.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn5})
        Me.GridView2.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.GridView2.Name = "GridView2"
        Me.GridView2.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.GridView2.OptionsView.ShowAutoFilterRow = True
        Me.GridView2.OptionsView.ShowGroupPanel = False
        '
        'GridColumn5
        '
        Me.GridColumn5.Caption = "Name Display"
        Me.GridColumn5.FieldName = "MEMO"
        Me.GridColumn5.Name = "GridColumn5"
        Me.GridColumn5.Visible = True
        Me.GridColumn5.VisibleIndex = 0
        '
        'txtNoSEP
        '
        Me.txtNoSEP.Location = New System.Drawing.Point(438, 125)
        Me.txtNoSEP.MenuManager = Me.barManager
        Me.txtNoSEP.Name = "txtNoSEP"
        Me.txtNoSEP.Size = New System.Drawing.Size(270, 22)
        Me.txtNoSEP.StyleController = Me.layoutControl
        Me.txtNoSEP.TabIndex = 46
        '
        'txtNoPeserta
        '
        Me.txtNoPeserta.Location = New System.Drawing.Point(218, 125)
        Me.txtNoPeserta.MenuManager = Me.barManager
        Me.txtNoPeserta.Name = "txtNoPeserta"
        Me.txtNoPeserta.Size = New System.Drawing.Size(214, 22)
        Me.txtNoPeserta.StyleController = Me.layoutControl
        Me.txtNoPeserta.TabIndex = 46
        '
        'cboCaraBayar
        '
        Me.cboCaraBayar.EditValue = "JKN"
        Me.cboCaraBayar.Location = New System.Drawing.Point(20, 125)
        Me.cboCaraBayar.MenuManager = Me.barManager
        Me.cboCaraBayar.Name = "cboCaraBayar"
        Me.cboCaraBayar.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cboCaraBayar.Properties.Items.AddRange(New Object() {"JKN", "JAMKESDA", "JAMKESOS", "PASIEN BAYAR"})
        Me.cboCaraBayar.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
        Me.cboCaraBayar.Size = New System.Drawing.Size(192, 22)
        Me.cboCaraBayar.StyleController = Me.layoutControl
        Me.cboCaraBayar.TabIndex = 47
        '
        'txtCODE
        '
        Me.txtCODE.Location = New System.Drawing.Point(141, 16)
        Me.txtCODE.MenuManager = Me.barManager
        Me.txtCODE.Name = "txtCODE"
        Me.txtCODE.Properties.ReadOnly = True
        Me.txtCODE.Size = New System.Drawing.Size(151, 22)
        Me.txtCODE.StyleController = Me.layoutControl
        Me.txtCODE.TabIndex = 47
        '
        'grdDPJP
        '
        Me.grdDPJP.EnterMoveNextControl = True
        Me.grdDPJP.Location = New System.Drawing.Point(167, 360)
        Me.grdDPJP.MenuManager = Me.barManager
        Me.grdDPJP.Name = "grdDPJP"
        Me.grdDPJP.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.grdDPJP.Properties.NullText = ""
        Me.grdDPJP.Properties.PopupFormMinSize = New System.Drawing.Size(600, 300)
        Me.grdDPJP.Properties.View = Me.GridView4
        Me.grdDPJP.Size = New System.Drawing.Size(458, 22)
        Me.grdDPJP.StyleController = Me.layoutControl
        Me.grdDPJP.TabIndex = 40
        '
        'GridView4
        '
        Me.GridView4.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn2})
        Me.GridView4.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.GridView4.Name = "GridView4"
        Me.GridView4.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.GridView4.OptionsView.ShowAutoFilterRow = True
        Me.GridView4.OptionsView.ShowGroupPanel = False
        '
        'GridColumn2
        '
        Me.GridColumn2.Caption = "Name Display"
        Me.GridColumn2.FieldName = "NAME_DISPLAY"
        Me.GridColumn2.Name = "GridColumn2"
        Me.GridColumn2.Visible = True
        Me.GridColumn2.VisibleIndex = 0
        '
        'grdKDCASHIN
        '
        Me.grdKDCASHIN.EnterMoveNextControl = True
        Me.grdKDCASHIN.Location = New System.Drawing.Point(499, 48)
        Me.grdKDCASHIN.MenuManager = Me.barManager
        Me.grdKDCASHIN.Name = "grdKDCASHIN"
        Me.grdKDCASHIN.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.grdKDCASHIN.Properties.NullText = ""
        Me.grdKDCASHIN.Properties.PopupFormMinSize = New System.Drawing.Size(600, 300)
        Me.grdKDCASHIN.Properties.View = Me.grvKDCASHIN
        Me.grdKDCASHIN.Size = New System.Drawing.Size(464, 22)
        Me.grdKDCASHIN.StyleController = Me.layoutControl
        Me.grdKDCASHIN.TabIndex = 40
        '
        'grvKDCASHIN
        '
        Me.grvKDCASHIN.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn12, Me.GridColumn14, Me.GridColumn3, Me.GridColumn4, Me.GridColumn13, Me.GridColumn16, Me.GridColumn17, Me.GridColumn20, Me.GridColumn21, Me.GridColumn22, Me.GridColumn23, Me.GridColumn24})
        Me.grvKDCASHIN.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.grvKDCASHIN.Name = "grvKDCASHIN"
        Me.grvKDCASHIN.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.grvKDCASHIN.OptionsView.ShowAutoFilterRow = True
        Me.grvKDCASHIN.OptionsView.ShowGroupPanel = False
        '
        'GridColumn12
        '
        Me.GridColumn12.Caption = "Nomor Pendaftaran"
        Me.GridColumn12.FieldName = "KDPENDAFTARAN"
        Me.GridColumn12.Name = "GridColumn12"
        Me.GridColumn12.Visible = True
        Me.GridColumn12.VisibleIndex = 0
        '
        'GridColumn14
        '
        Me.GridColumn14.Caption = "Tanggal Daftar"
        Me.GridColumn14.DisplayFormat.FormatString = "dd-MM-yyyy"
        Me.GridColumn14.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.GridColumn14.FieldName = "TANGGAL"
        Me.GridColumn14.Name = "GridColumn14"
        Me.GridColumn14.Visible = True
        Me.GridColumn14.VisibleIndex = 1
        '
        'GridColumn3
        '
        Me.GridColumn3.Caption = "No Rekam Medis"
        Me.GridColumn3.FieldName = "KDCUSTOMER"
        Me.GridColumn3.Name = "GridColumn3"
        Me.GridColumn3.Visible = True
        Me.GridColumn3.VisibleIndex = 2
        '
        'GridColumn4
        '
        Me.GridColumn4.Caption = "Nama Pasien"
        Me.GridColumn4.FieldName = "PASIEN"
        Me.GridColumn4.Name = "GridColumn4"
        Me.GridColumn4.Visible = True
        Me.GridColumn4.VisibleIndex = 3
        '
        'GridColumn13
        '
        Me.GridColumn13.Caption = "Poli/Ruangan"
        Me.GridColumn13.FieldName = "TUJUAN"
        Me.GridColumn13.Name = "GridColumn13"
        Me.GridColumn13.Visible = True
        Me.GridColumn13.VisibleIndex = 4
        '
        'GridColumn16
        '
        Me.GridColumn16.Caption = "NoID_Petugas"
        Me.GridColumn16.FieldName = "NoID_Petugas"
        Me.GridColumn16.Name = "GridColumn16"
        '
        'GridColumn17
        '
        Me.GridColumn17.Caption = "Tgl_Lahir"
        Me.GridColumn17.FieldName = "Tgl_Lahir"
        Me.GridColumn17.Name = "GridColumn17"
        '
        'GridColumn20
        '
        Me.GridColumn20.Caption = "No_BPJS"
        Me.GridColumn20.FieldName = "No_BPJS"
        Me.GridColumn20.Name = "GridColumn20"
        '
        'GridColumn21
        '
        Me.GridColumn21.Caption = "NIK"
        Me.GridColumn21.FieldName = "NIK"
        Me.GridColumn21.Name = "GridColumn21"
        '
        'GridColumn22
        '
        Me.GridColumn22.Caption = "Kode_Klinik"
        Me.GridColumn22.FieldName = "Kode_Klinik"
        Me.GridColumn22.Name = "GridColumn22"
        '
        'GridColumn23
        '
        Me.GridColumn23.Caption = "Lama"
        Me.GridColumn23.FieldName = "Lama"
        Me.GridColumn23.Name = "GridColumn23"
        '
        'GridColumn24
        '
        Me.GridColumn24.Caption = "Tgl_Pulang"
        Me.GridColumn24.FieldName = "Tgl_Pulang"
        Me.GridColumn24.Name = "GridColumn24"
        '
        'cboCARI
        '
        Me.cboCARI.EditValue = "No Register"
        Me.cboCARI.Location = New System.Drawing.Point(141, 48)
        Me.cboCARI.MenuManager = Me.barManager
        Me.cboCARI.Name = "cboCARI"
        Me.cboCARI.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cboCARI.Properties.Items.AddRange(New Object() {"No Register", "Rekam Medis"})
        Me.cboCARI.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
        Me.cboCARI.Size = New System.Drawing.Size(151, 22)
        Me.cboCARI.StyleController = Me.layoutControl
        Me.cboCARI.TabIndex = 46
        '
        'txtCARI
        '
        Me.txtCARI.Location = New System.Drawing.Point(298, 48)
        Me.txtCARI.MenuManager = Me.barManager
        Me.txtCARI.Name = "txtCARI"
        Me.txtCARI.Size = New System.Drawing.Size(195, 22)
        Me.txtCARI.StyleController = Me.layoutControl
        Me.txtCARI.TabIndex = 45
        '
        'deDATEPULANG
        '
        Me.deDATEPULANG.EditValue = Nothing
        Me.deDATEPULANG.EnterMoveNextControl = True
        Me.deDATEPULANG.Location = New System.Drawing.Point(377, 216)
        Me.deDATEPULANG.MenuManager = Me.barManager
        Me.deDATEPULANG.Name = "deDATEPULANG"
        Me.deDATEPULANG.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.deDATEPULANG.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
        Me.deDATEPULANG.Properties.Mask.EditMask = "dd/MM/yyyy"
        Me.deDATEPULANG.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.deDATEPULANG.Size = New System.Drawing.Size(248, 22)
        Me.deDATEPULANG.StyleController = Me.layoutControl
        Me.deDATEPULANG.TabIndex = 22
        '
        'deDATEMASUK
        '
        Me.deDATEMASUK.EditValue = Nothing
        Me.deDATEMASUK.EnterMoveNextControl = True
        Me.deDATEMASUK.Location = New System.Drawing.Point(209, 216)
        Me.deDATEMASUK.MenuManager = Me.barManager
        Me.deDATEMASUK.Name = "deDATEMASUK"
        Me.deDATEMASUK.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.deDATEMASUK.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
        Me.deDATEMASUK.Properties.Mask.EditMask = "dd/MM/yyyy"
        Me.deDATEMASUK.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.deDATEMASUK.Size = New System.Drawing.Size(119, 22)
        Me.deDATEMASUK.StyleController = Me.layoutControl
        Me.deDATEMASUK.TabIndex = 22
        '
        'LayoutControlGroup1
        '
        Me.LayoutControlGroup1.CustomizationFormText = "LayoutControlGroup1"
        Me.LayoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.LayoutControlGroup1.GroupBordersVisible = False
        Me.LayoutControlGroup1.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlItem4, Me.lKDSKD, Me.LayoutControlGroup2, Me.LayoutControlGroup3, Me.LayoutControlGroup5, Me.LayoutControlItem28, Me.EmptySpaceItem1, Me.LayoutControlItem47, Me.LayoutControlItem48, Me.LayoutControlItem23, Me.LayoutControlItem11, Me.LayoutControlItem21, Me.LayoutControlItem3, Me.lKDPENDAFATRAN, Me.LayoutControlItem22, Me.LayoutControlItem27})
        Me.LayoutControlGroup1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlGroup1.Name = "Root"
        Me.LayoutControlGroup1.Size = New System.Drawing.Size(1478, 715)
        Me.LayoutControlGroup1.TextVisible = False
        '
        'LayoutControlItem4
        '
        Me.LayoutControlItem4.AppearanceItemCaption.Options.UseTextOptions = True
        Me.LayoutControlItem4.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.LayoutControlItem4.Control = Me.cboCARI
        Me.LayoutControlItem4.Location = New System.Drawing.Point(0, 32)
        Me.LayoutControlItem4.Name = "LayoutControlItem4"
        Me.LayoutControlItem4.Size = New System.Drawing.Size(282, 28)
        Me.LayoutControlItem4.Text = "Cari :"
        Me.LayoutControlItem4.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.LayoutControlItem4.TextSize = New System.Drawing.Size(120, 20)
        Me.LayoutControlItem4.TextToControlDistance = 5
        '
        'lKDSKD
        '
        Me.lKDSKD.AppearanceItemCaption.Options.UseTextOptions = True
        Me.lKDSKD.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.lKDSKD.Control = Me.txtCODE
        Me.lKDSKD.Location = New System.Drawing.Point(0, 0)
        Me.lKDSKD.Name = "lKDSKD"
        Me.lKDSKD.Size = New System.Drawing.Size(282, 32)
        Me.lKDSKD.Text = "No Transaksi :"
        Me.lKDSKD.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.lKDSKD.TextSize = New System.Drawing.Size(120, 20)
        Me.lKDSKD.TextToControlDistance = 5
        '
        'LayoutControlGroup2
        '
        Me.LayoutControlGroup2.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlItem1, Me.LayoutControlItem2, Me.LayoutControlItem5, Me.LayoutControlItem6, Me.LayoutControlItem7, Me.LayoutControlItem8, Me.LayoutControlItem9, Me.LayoutControlItem10})
        Me.LayoutControlGroup2.Location = New System.Drawing.Point(0, 60)
        Me.LayoutControlGroup2.Name = "LayoutControlGroup2"
        Me.LayoutControlGroup2.Padding = New DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0)
        Me.LayoutControlGroup2.Size = New System.Drawing.Size(953, 81)
        Me.LayoutControlGroup2.Text = " "
        '
        'LayoutControlItem1
        '
        Me.LayoutControlItem1.AppearanceItemCaption.Options.UseTextOptions = True
        Me.LayoutControlItem1.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.LayoutControlItem1.Control = Me.cboCaraBayar
        Me.LayoutControlItem1.Location = New System.Drawing.Point(0, 22)
        Me.LayoutControlItem1.Name = "LayoutControlItem1"
        Me.LayoutControlItem1.Size = New System.Drawing.Size(198, 28)
        Me.LayoutControlItem1.Text = "Jaminan / Cara Bayar :"
        Me.LayoutControlItem1.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.LayoutControlItem1.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem1.TextToControlDistance = 0
        Me.LayoutControlItem1.TextVisible = False
        '
        'LayoutControlItem2
        '
        Me.LayoutControlItem2.AppearanceItemCaption.Options.UseTextOptions = True
        Me.LayoutControlItem2.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.LayoutControlItem2.Control = Me.txtNoPeserta
        Me.LayoutControlItem2.Location = New System.Drawing.Point(198, 22)
        Me.LayoutControlItem2.Name = "LayoutControlItem2"
        Me.LayoutControlItem2.Size = New System.Drawing.Size(220, 28)
        Me.LayoutControlItem2.Text = "No. Peserta :"
        Me.LayoutControlItem2.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.LayoutControlItem2.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem2.TextToControlDistance = 0
        Me.LayoutControlItem2.TextVisible = False
        '
        'LayoutControlItem5
        '
        Me.LayoutControlItem5.AppearanceItemCaption.Options.UseTextOptions = True
        Me.LayoutControlItem5.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.LayoutControlItem5.Control = Me.txtNoSEP
        Me.LayoutControlItem5.Location = New System.Drawing.Point(418, 22)
        Me.LayoutControlItem5.Name = "LayoutControlItem5"
        Me.LayoutControlItem5.Size = New System.Drawing.Size(276, 28)
        Me.LayoutControlItem5.Text = "Nomor SEP :"
        Me.LayoutControlItem5.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.LayoutControlItem5.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem5.TextToControlDistance = 0
        Me.LayoutControlItem5.TextVisible = False
        '
        'LayoutControlItem6
        '
        Me.LayoutControlItem6.AppearanceItemCaption.Options.UseTextOptions = True
        Me.LayoutControlItem6.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.LayoutControlItem6.Control = Me.grdCOB
        Me.LayoutControlItem6.Location = New System.Drawing.Point(694, 22)
        Me.LayoutControlItem6.Name = "LayoutControlItem6"
        Me.LayoutControlItem6.Size = New System.Drawing.Size(251, 28)
        Me.LayoutControlItem6.Text = "COB :"
        Me.LayoutControlItem6.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.LayoutControlItem6.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem6.TextToControlDistance = 0
        Me.LayoutControlItem6.TextVisible = False
        '
        'LayoutControlItem7
        '
        Me.LayoutControlItem7.Control = Me.LabelControl1
        Me.LayoutControlItem7.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlItem7.Name = "LayoutControlItem7"
        Me.LayoutControlItem7.Size = New System.Drawing.Size(198, 22)
        Me.LayoutControlItem7.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem7.TextVisible = False
        '
        'LayoutControlItem8
        '
        Me.LayoutControlItem8.Control = Me.LabelControl2
        Me.LayoutControlItem8.Location = New System.Drawing.Point(198, 0)
        Me.LayoutControlItem8.Name = "LayoutControlItem8"
        Me.LayoutControlItem8.Size = New System.Drawing.Size(220, 22)
        Me.LayoutControlItem8.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem8.TextVisible = False
        '
        'LayoutControlItem9
        '
        Me.LayoutControlItem9.Control = Me.LabelControl3
        Me.LayoutControlItem9.Location = New System.Drawing.Point(418, 0)
        Me.LayoutControlItem9.Name = "LayoutControlItem9"
        Me.LayoutControlItem9.Size = New System.Drawing.Size(276, 22)
        Me.LayoutControlItem9.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem9.TextVisible = False
        '
        'LayoutControlItem10
        '
        Me.LayoutControlItem10.Control = Me.LabelControl4
        Me.LayoutControlItem10.Location = New System.Drawing.Point(694, 0)
        Me.LayoutControlItem10.Name = "LayoutControlItem10"
        Me.LayoutControlItem10.Size = New System.Drawing.Size(251, 22)
        Me.LayoutControlItem10.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem10.TextVisible = False
        '
        'LayoutControlGroup3
        '
        Me.LayoutControlGroup3.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.lKDDOCTOR, Me.LayoutControlItem13, Me.LayoutControlItem14, Me.LayoutControlItem12, Me.lDATE, Me.lDATE_KONTROL, Me.LKELASPELAYANAN_RB, Me.LayoutControlItem20, Me.lKELASPELAYANAN, Me.LayoutControlItem50, Me.LayoutControlItem51, Me.LayoutControlItem52, Me.lNAIKKELAS, Me.lADARAWAT, Me.lRAWATINTENSIF_HARI, Me.lRAWATINTENSIF_HARI_TEXT, Me.lKELASEKSEKUTIF, Me.lKELASHAKRJ_LBL, Me.lKELASHAKRI, Me.lKELASHAKRJ, Me.LayoutControlItem25, Me.LayoutControlItem15, Me.lLAMA_LBL, Me.lLAMA, Me.lVENTILATOR, Me.lVENTILATOR_TEXT, Me.LayoutControlItem26, Me.LayoutControlItem16, Me.LayoutControlItem49, Me.LayoutControlItem18, Me.LayoutControlItem54, Me.LayoutControlItem17, Me.lTARIFEKSEKUTIF})
        Me.LayoutControlGroup3.Location = New System.Drawing.Point(0, 141)
        Me.LayoutControlGroup3.Name = "LayoutControlGroup3"
        Me.LayoutControlGroup3.Padding = New DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0)
        Me.LayoutControlGroup3.Size = New System.Drawing.Size(953, 263)
        Me.LayoutControlGroup3.Text = " "
        '
        'lKDDOCTOR
        '
        Me.lKDDOCTOR.AppearanceItemCaption.Options.UseTextOptions = True
        Me.lKDDOCTOR.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.lKDDOCTOR.Control = Me.grdDPJP
        Me.lKDDOCTOR.Location = New System.Drawing.Point(147, 176)
        Me.lKDDOCTOR.Name = "lKDDOCTOR"
        Me.lKDDOCTOR.Size = New System.Drawing.Size(464, 56)
        Me.lKDDOCTOR.Text = "DPJP :"
        Me.lKDDOCTOR.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.lKDDOCTOR.TextSize = New System.Drawing.Size(0, 0)
        Me.lKDDOCTOR.TextToControlDistance = 0
        Me.lKDDOCTOR.TextVisible = False
        '
        'LayoutControlItem13
        '
        Me.LayoutControlItem13.AppearanceItemCaption.Options.UseTextOptions = True
        Me.LayoutControlItem13.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.LayoutControlItem13.Control = Me.txtADLScore_SubAcute
        Me.LayoutControlItem13.Location = New System.Drawing.Point(147, 148)
        Me.LayoutControlItem13.Name = "LayoutControlItem13"
        Me.LayoutControlItem13.Size = New System.Drawing.Size(198, 28)
        Me.LayoutControlItem13.Text = "Sub Acute :"
        Me.LayoutControlItem13.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.AutoSize
        Me.LayoutControlItem13.TextSize = New System.Drawing.Size(67, 16)
        Me.LayoutControlItem13.TextToControlDistance = 5
        '
        'LayoutControlItem14
        '
        Me.LayoutControlItem14.AppearanceItemCaption.Options.UseTextOptions = True
        Me.LayoutControlItem14.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.LayoutControlItem14.Control = Me.txtADLScore_Chronic
        Me.LayoutControlItem14.Location = New System.Drawing.Point(345, 148)
        Me.LayoutControlItem14.Name = "LayoutControlItem14"
        Me.LayoutControlItem14.Size = New System.Drawing.Size(266, 28)
        Me.LayoutControlItem14.Text = "Chronic :"
        Me.LayoutControlItem14.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.AutoSize
        Me.LayoutControlItem14.TextSize = New System.Drawing.Size(52, 16)
        Me.LayoutControlItem14.TextToControlDistance = 5
        '
        'LayoutControlItem12
        '
        Me.LayoutControlItem12.AppearanceItemCaption.Options.UseTextOptions = True
        Me.LayoutControlItem12.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.LayoutControlItem12.Control = Me.txtLOS
        Me.LayoutControlItem12.Location = New System.Drawing.Point(147, 120)
        Me.LayoutControlItem12.Name = "LayoutControlItem12"
        Me.LayoutControlItem12.Size = New System.Drawing.Size(464, 28)
        Me.LayoutControlItem12.Text = "LOS (Hari) :"
        Me.LayoutControlItem12.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.LayoutControlItem12.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem12.TextToControlDistance = 0
        Me.LayoutControlItem12.TextVisible = False
        '
        'lDATE
        '
        Me.lDATE.AppearanceItemCaption.Options.UseTextOptions = True
        Me.lDATE.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.lDATE.Control = Me.deDATEMASUK
        Me.lDATE.Location = New System.Drawing.Point(147, 32)
        Me.lDATE.Name = "lDATE"
        Me.lDATE.Size = New System.Drawing.Size(167, 28)
        Me.lDATE.Text = "Masuk"
        Me.lDATE.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.AutoSize
        Me.lDATE.TextSize = New System.Drawing.Size(37, 16)
        Me.lDATE.TextToControlDistance = 5
        '
        'lDATE_KONTROL
        '
        Me.lDATE_KONTROL.AppearanceItemCaption.Options.UseTextOptions = True
        Me.lDATE_KONTROL.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.lDATE_KONTROL.Control = Me.deDATEPULANG
        Me.lDATE_KONTROL.Location = New System.Drawing.Point(314, 32)
        Me.lDATE_KONTROL.Name = "lDATE_KONTROL"
        Me.lDATE_KONTROL.Size = New System.Drawing.Size(297, 28)
        Me.lDATE_KONTROL.Text = "Pulang"
        Me.lDATE_KONTROL.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.AutoSize
        Me.lDATE_KONTROL.TextSize = New System.Drawing.Size(38, 16)
        Me.lDATE_KONTROL.TextToControlDistance = 5
        '
        'LKELASPELAYANAN_RB
        '
        Me.LKELASPELAYANAN_RB.AppearanceItemCaption.Options.UseTextOptions = True
        Me.LKELASPELAYANAN_RB.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.LKELASPELAYANAN_RB.Control = Me.rbKELASPELAYANAN
        Me.LKELASPELAYANAN_RB.Location = New System.Drawing.Point(147, 60)
        Me.LKELASPELAYANAN_RB.Name = "LKELASPELAYANAN_RB"
        Me.LKELASPELAYANAN_RB.Size = New System.Drawing.Size(464, 32)
        Me.LKELASPELAYANAN_RB.Text = "Kelas Pelayanan :"
        Me.LKELASPELAYANAN_RB.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.LKELASPELAYANAN_RB.TextSize = New System.Drawing.Size(0, 0)
        Me.LKELASPELAYANAN_RB.TextToControlDistance = 0
        Me.LKELASPELAYANAN_RB.TextVisible = False
        '
        'LayoutControlItem20
        '
        Me.LayoutControlItem20.Control = Me.LabelControl5
        Me.LayoutControlItem20.Location = New System.Drawing.Point(0, 32)
        Me.LayoutControlItem20.Name = "LayoutControlItem20"
        Me.LayoutControlItem20.Size = New System.Drawing.Size(147, 28)
        Me.LayoutControlItem20.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem20.TextVisible = False
        '
        'lKELASPELAYANAN
        '
        Me.lKELASPELAYANAN.Control = Me.LabelControl6
        Me.lKELASPELAYANAN.Location = New System.Drawing.Point(0, 60)
        Me.lKELASPELAYANAN.Name = "lKELASPELAYANAN"
        Me.lKELASPELAYANAN.Size = New System.Drawing.Size(147, 32)
        Me.lKELASPELAYANAN.TextSize = New System.Drawing.Size(0, 0)
        Me.lKELASPELAYANAN.TextVisible = False
        '
        'LayoutControlItem50
        '
        Me.LayoutControlItem50.Control = Me.LabelControl7
        Me.LayoutControlItem50.Location = New System.Drawing.Point(0, 120)
        Me.LayoutControlItem50.Name = "LayoutControlItem50"
        Me.LayoutControlItem50.Size = New System.Drawing.Size(147, 28)
        Me.LayoutControlItem50.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem50.TextVisible = False
        '
        'LayoutControlItem51
        '
        Me.LayoutControlItem51.Control = Me.LabelControl8
        Me.LayoutControlItem51.Location = New System.Drawing.Point(0, 148)
        Me.LayoutControlItem51.Name = "LayoutControlItem51"
        Me.LayoutControlItem51.Size = New System.Drawing.Size(147, 28)
        Me.LayoutControlItem51.Text = "ADL Score"
        Me.LayoutControlItem51.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem51.TextVisible = False
        '
        'LayoutControlItem52
        '
        Me.LayoutControlItem52.Control = Me.LabelControl9
        Me.LayoutControlItem52.Location = New System.Drawing.Point(0, 176)
        Me.LayoutControlItem52.Name = "LayoutControlItem52"
        Me.LayoutControlItem52.Size = New System.Drawing.Size(147, 56)
        Me.LayoutControlItem52.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem52.TextVisible = False
        '
        'lNAIKKELAS
        '
        Me.lNAIKKELAS.Control = Me.chkNaikKelas
        Me.lNAIKKELAS.Location = New System.Drawing.Point(0, 0)
        Me.lNAIKKELAS.Name = "lNAIKKELAS"
        Me.lNAIKKELAS.Size = New System.Drawing.Size(127, 32)
        Me.lNAIKKELAS.TextSize = New System.Drawing.Size(0, 0)
        Me.lNAIKKELAS.TextVisible = False
        '
        'lADARAWAT
        '
        Me.lADARAWAT.Control = Me.chkAdaRawat
        Me.lADARAWAT.Location = New System.Drawing.Point(127, 0)
        Me.lADARAWAT.Name = "lADARAWAT"
        Me.lADARAWAT.Size = New System.Drawing.Size(137, 32)
        Me.lADARAWAT.TextSize = New System.Drawing.Size(0, 0)
        Me.lADARAWAT.TextVisible = False
        '
        'lRAWATINTENSIF_HARI
        '
        Me.lRAWATINTENSIF_HARI.Control = Me.LabelControl17
        Me.lRAWATINTENSIF_HARI.Location = New System.Drawing.Point(0, 92)
        Me.lRAWATINTENSIF_HARI.Name = "lRAWATINTENSIF_HARI"
        Me.lRAWATINTENSIF_HARI.Size = New System.Drawing.Size(147, 28)
        Me.lRAWATINTENSIF_HARI.TextSize = New System.Drawing.Size(0, 0)
        Me.lRAWATINTENSIF_HARI.TextVisible = False
        '
        'lRAWATINTENSIF_HARI_TEXT
        '
        Me.lRAWATINTENSIF_HARI_TEXT.Control = Me.txtRAWATINTENSIF_HARI
        Me.lRAWATINTENSIF_HARI_TEXT.Location = New System.Drawing.Point(147, 92)
        Me.lRAWATINTENSIF_HARI_TEXT.Name = "lRAWATINTENSIF_HARI_TEXT"
        Me.lRAWATINTENSIF_HARI_TEXT.Size = New System.Drawing.Size(464, 28)
        Me.lRAWATINTENSIF_HARI_TEXT.TextSize = New System.Drawing.Size(0, 0)
        Me.lRAWATINTENSIF_HARI_TEXT.TextVisible = False
        '
        'lKELASEKSEKUTIF
        '
        Me.lKELASEKSEKUTIF.Control = Me.chkKelasEksekutif
        Me.lKELASEKSEKUTIF.Location = New System.Drawing.Point(264, 0)
        Me.lKELASEKSEKUTIF.Name = "lKELASEKSEKUTIF"
        Me.lKELASEKSEKUTIF.Size = New System.Drawing.Size(347, 32)
        Me.lKELASEKSEKUTIF.TextSize = New System.Drawing.Size(0, 0)
        Me.lKELASEKSEKUTIF.TextVisible = False
        '
        'lKELASHAKRJ_LBL
        '
        Me.lKELASHAKRJ_LBL.Control = Me.LabelControl11
        Me.lKELASHAKRJ_LBL.Location = New System.Drawing.Point(611, 0)
        Me.lKELASHAKRJ_LBL.Name = "lKELASHAKRJ_LBL"
        Me.lKELASHAKRJ_LBL.Size = New System.Drawing.Size(129, 32)
        Me.lKELASHAKRJ_LBL.TextSize = New System.Drawing.Size(0, 0)
        Me.lKELASHAKRJ_LBL.TextVisible = False
        '
        'lKELASHAKRI
        '
        Me.lKELASHAKRI.AppearanceItemCaption.Options.UseTextOptions = True
        Me.lKELASHAKRI.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.lKELASHAKRI.Control = Me.rbKELASHAK
        Me.lKELASHAKRI.Location = New System.Drawing.Point(740, 0)
        Me.lKELASHAKRI.Name = "lKELASHAKRI"
        Me.lKELASHAKRI.Size = New System.Drawing.Size(149, 32)
        Me.lKELASHAKRI.Text = "Kelas Hak :"
        Me.lKELASHAKRI.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.lKELASHAKRI.TextSize = New System.Drawing.Size(0, 0)
        Me.lKELASHAKRI.TextToControlDistance = 0
        Me.lKELASHAKRI.TextVisible = False
        '
        'lKELASHAKRJ
        '
        Me.lKELASHAKRJ.AppearanceItemCaption.Options.UseTextOptions = True
        Me.lKELASHAKRJ.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.lKELASHAKRJ.Control = Me.txtHAKKELAS
        Me.lKELASHAKRJ.Location = New System.Drawing.Point(889, 0)
        Me.lKELASHAKRJ.Name = "lKELASHAKRJ"
        Me.lKELASHAKRJ.Size = New System.Drawing.Size(56, 32)
        Me.lKELASHAKRJ.Text = "Kelas Hak :"
        Me.lKELASHAKRJ.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.lKELASHAKRJ.TextSize = New System.Drawing.Size(0, 0)
        Me.lKELASHAKRJ.TextToControlDistance = 0
        Me.lKELASHAKRJ.TextVisible = False
        '
        'LayoutControlItem25
        '
        Me.LayoutControlItem25.Control = Me.LabelControl13
        Me.LayoutControlItem25.Location = New System.Drawing.Point(611, 32)
        Me.LayoutControlItem25.Name = "LayoutControlItem25"
        Me.LayoutControlItem25.Size = New System.Drawing.Size(129, 28)
        Me.LayoutControlItem25.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem25.TextVisible = False
        '
        'LayoutControlItem15
        '
        Me.LayoutControlItem15.AppearanceItemCaption.Options.UseTextOptions = True
        Me.LayoutControlItem15.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.LayoutControlItem15.Control = Me.txtUmur
        Me.LayoutControlItem15.Location = New System.Drawing.Point(740, 32)
        Me.LayoutControlItem15.Name = "LayoutControlItem15"
        Me.LayoutControlItem15.Size = New System.Drawing.Size(205, 28)
        Me.LayoutControlItem15.Text = "Umur :"
        Me.LayoutControlItem15.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.LayoutControlItem15.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem15.TextToControlDistance = 0
        Me.LayoutControlItem15.TextVisible = False
        '
        'lLAMA_LBL
        '
        Me.lLAMA_LBL.Control = Me.LabelControl10
        Me.lLAMA_LBL.Location = New System.Drawing.Point(611, 60)
        Me.lLAMA_LBL.Name = "lLAMA_LBL"
        Me.lLAMA_LBL.Size = New System.Drawing.Size(129, 32)
        Me.lLAMA_LBL.TextSize = New System.Drawing.Size(0, 0)
        Me.lLAMA_LBL.TextVisible = False
        '
        'lLAMA
        '
        Me.lLAMA.Control = Me.txtLAMA
        Me.lLAMA.Location = New System.Drawing.Point(740, 60)
        Me.lLAMA.Name = "lLAMA"
        Me.lLAMA.Size = New System.Drawing.Size(205, 32)
        Me.lLAMA.TextSize = New System.Drawing.Size(0, 0)
        Me.lLAMA.TextVisible = False
        '
        'lVENTILATOR
        '
        Me.lVENTILATOR.Control = Me.LabelControl18
        Me.lVENTILATOR.Location = New System.Drawing.Point(611, 92)
        Me.lVENTILATOR.Name = "lVENTILATOR"
        Me.lVENTILATOR.Size = New System.Drawing.Size(129, 28)
        Me.lVENTILATOR.TextSize = New System.Drawing.Size(0, 0)
        Me.lVENTILATOR.TextVisible = False
        '
        'lVENTILATOR_TEXT
        '
        Me.lVENTILATOR_TEXT.Control = Me.txtVENTILATOR
        Me.lVENTILATOR_TEXT.Location = New System.Drawing.Point(740, 92)
        Me.lVENTILATOR_TEXT.Name = "lVENTILATOR_TEXT"
        Me.lVENTILATOR_TEXT.Size = New System.Drawing.Size(205, 28)
        Me.lVENTILATOR_TEXT.TextSize = New System.Drawing.Size(0, 0)
        Me.lVENTILATOR_TEXT.TextVisible = False
        '
        'LayoutControlItem26
        '
        Me.LayoutControlItem26.Control = Me.LabelControl14
        Me.LayoutControlItem26.Location = New System.Drawing.Point(611, 120)
        Me.LayoutControlItem26.Name = "LayoutControlItem26"
        Me.LayoutControlItem26.Size = New System.Drawing.Size(129, 28)
        Me.LayoutControlItem26.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem26.TextVisible = False
        '
        'LayoutControlItem16
        '
        Me.LayoutControlItem16.AppearanceItemCaption.Options.UseTextOptions = True
        Me.LayoutControlItem16.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.LayoutControlItem16.Control = Me.txtBeratBadan
        Me.LayoutControlItem16.Location = New System.Drawing.Point(740, 120)
        Me.LayoutControlItem16.Name = "LayoutControlItem16"
        Me.LayoutControlItem16.Size = New System.Drawing.Size(205, 28)
        Me.LayoutControlItem16.Text = "Berat Lahir (gram) :"
        Me.LayoutControlItem16.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.LayoutControlItem16.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem16.TextToControlDistance = 0
        Me.LayoutControlItem16.TextVisible = False
        '
        'LayoutControlItem49
        '
        Me.LayoutControlItem49.Control = Me.LabelControl15
        Me.LayoutControlItem49.Location = New System.Drawing.Point(611, 148)
        Me.LayoutControlItem49.Name = "LayoutControlItem49"
        Me.LayoutControlItem49.Size = New System.Drawing.Size(129, 28)
        Me.LayoutControlItem49.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem49.TextVisible = False
        '
        'LayoutControlItem18
        '
        Me.LayoutControlItem18.AppearanceItemCaption.Options.UseTextOptions = True
        Me.LayoutControlItem18.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.LayoutControlItem18.Control = Me.grdCaraKeluar
        Me.LayoutControlItem18.Location = New System.Drawing.Point(740, 148)
        Me.LayoutControlItem18.Name = "LayoutControlItem18"
        Me.LayoutControlItem18.Size = New System.Drawing.Size(205, 28)
        Me.LayoutControlItem18.Text = "Cara Pulang :"
        Me.LayoutControlItem18.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.LayoutControlItem18.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem18.TextToControlDistance = 0
        Me.LayoutControlItem18.TextVisible = False
        '
        'LayoutControlItem54
        '
        Me.LayoutControlItem54.Control = Me.LabelControl16
        Me.LayoutControlItem54.Location = New System.Drawing.Point(611, 176)
        Me.LayoutControlItem54.Name = "LayoutControlItem54"
        Me.LayoutControlItem54.Size = New System.Drawing.Size(129, 56)
        Me.LayoutControlItem54.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem54.TextVisible = False
        '
        'LayoutControlItem17
        '
        Me.LayoutControlItem17.AppearanceItemCaption.Options.UseTextOptions = True
        Me.LayoutControlItem17.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.LayoutControlItem17.Control = Me.grdJenisTarif
        Me.LayoutControlItem17.Location = New System.Drawing.Point(740, 176)
        Me.LayoutControlItem17.Name = "LayoutControlItem17"
        Me.LayoutControlItem17.Size = New System.Drawing.Size(205, 28)
        Me.LayoutControlItem17.Text = "Jenis Tarif :"
        Me.LayoutControlItem17.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.LayoutControlItem17.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem17.TextToControlDistance = 0
        Me.LayoutControlItem17.TextVisible = False
        '
        'lTARIFEKSEKUTIF
        '
        Me.lTARIFEKSEKUTIF.AppearanceItemCaption.Options.UseTextOptions = True
        Me.lTARIFEKSEKUTIF.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.lTARIFEKSEKUTIF.Control = Me.txtTarifEksekutif
        Me.lTARIFEKSEKUTIF.Location = New System.Drawing.Point(740, 204)
        Me.lTARIFEKSEKUTIF.Name = "lTARIFEKSEKUTIF"
        Me.lTARIFEKSEKUTIF.Size = New System.Drawing.Size(205, 28)
        Me.lTARIFEKSEKUTIF.Text = "Tarif Eksekutif :"
        Me.lTARIFEKSEKUTIF.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.lTARIFEKSEKUTIF.TextSize = New System.Drawing.Size(120, 20)
        Me.lTARIFEKSEKUTIF.TextToControlDistance = 5
        '
        'LayoutControlGroup5
        '
        Me.LayoutControlGroup5.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlItem29, Me.LayoutControlItem30, Me.LayoutControlItem31, Me.LayoutControlItem32, Me.LayoutControlItem33, Me.LayoutControlItem34, Me.LayoutControlItem35, Me.LayoutControlItem36, Me.LayoutControlItem37, Me.LayoutControlItem38, Me.LayoutControlItem39, Me.LayoutControlItem40, Me.LayoutControlItem41, Me.LayoutControlItem42, Me.LayoutControlItem43, Me.LayoutControlItem44, Me.LayoutControlItem45, Me.LayoutControlItem46})
        Me.LayoutControlGroup5.Location = New System.Drawing.Point(0, 432)
        Me.LayoutControlGroup5.Name = "LayoutControlGroup5"
        Me.LayoutControlGroup5.Padding = New DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0)
        Me.LayoutControlGroup5.Size = New System.Drawing.Size(953, 199)
        Me.LayoutControlGroup5.Text = " "
        '
        'LayoutControlItem29
        '
        Me.LayoutControlItem29.AppearanceItemCaption.Options.UseTextOptions = True
        Me.LayoutControlItem29.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.LayoutControlItem29.Control = Me.txtProsedurNonBedah
        Me.LayoutControlItem29.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlItem29.Name = "LayoutControlItem29"
        Me.LayoutControlItem29.Size = New System.Drawing.Size(321, 28)
        Me.LayoutControlItem29.Text = "Prosedur Non Bedah :"
        Me.LayoutControlItem29.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.LayoutControlItem29.TextSize = New System.Drawing.Size(130, 20)
        Me.LayoutControlItem29.TextToControlDistance = 5
        '
        'LayoutControlItem30
        '
        Me.LayoutControlItem30.AppearanceItemCaption.Options.UseTextOptions = True
        Me.LayoutControlItem30.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.LayoutControlItem30.Control = Me.txtTenagaAhli
        Me.LayoutControlItem30.Location = New System.Drawing.Point(0, 28)
        Me.LayoutControlItem30.Name = "LayoutControlItem30"
        Me.LayoutControlItem30.Size = New System.Drawing.Size(321, 28)
        Me.LayoutControlItem30.Text = "Tenaga Ahli :"
        Me.LayoutControlItem30.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.LayoutControlItem30.TextSize = New System.Drawing.Size(130, 20)
        Me.LayoutControlItem30.TextToControlDistance = 5
        '
        'LayoutControlItem31
        '
        Me.LayoutControlItem31.AppearanceItemCaption.Options.UseTextOptions = True
        Me.LayoutControlItem31.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.LayoutControlItem31.Control = Me.txtRadiologi
        Me.LayoutControlItem31.Location = New System.Drawing.Point(0, 56)
        Me.LayoutControlItem31.Name = "LayoutControlItem31"
        Me.LayoutControlItem31.Size = New System.Drawing.Size(321, 28)
        Me.LayoutControlItem31.Text = "Radiologi :"
        Me.LayoutControlItem31.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.LayoutControlItem31.TextSize = New System.Drawing.Size(130, 20)
        Me.LayoutControlItem31.TextToControlDistance = 5
        '
        'LayoutControlItem32
        '
        Me.LayoutControlItem32.AppearanceItemCaption.Options.UseTextOptions = True
        Me.LayoutControlItem32.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.LayoutControlItem32.Control = Me.txtRehabilitasi
        Me.LayoutControlItem32.Location = New System.Drawing.Point(0, 84)
        Me.LayoutControlItem32.Name = "LayoutControlItem32"
        Me.LayoutControlItem32.Size = New System.Drawing.Size(321, 28)
        Me.LayoutControlItem32.Text = "Rehabilitasi :"
        Me.LayoutControlItem32.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.LayoutControlItem32.TextSize = New System.Drawing.Size(130, 20)
        Me.LayoutControlItem32.TextToControlDistance = 5
        '
        'LayoutControlItem33
        '
        Me.LayoutControlItem33.AppearanceItemCaption.Options.UseTextOptions = True
        Me.LayoutControlItem33.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.LayoutControlItem33.Control = Me.txtObat
        Me.LayoutControlItem33.Location = New System.Drawing.Point(0, 112)
        Me.LayoutControlItem33.Name = "LayoutControlItem33"
        Me.LayoutControlItem33.Size = New System.Drawing.Size(321, 28)
        Me.LayoutControlItem33.Text = "Obat :"
        Me.LayoutControlItem33.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.LayoutControlItem33.TextSize = New System.Drawing.Size(130, 20)
        Me.LayoutControlItem33.TextToControlDistance = 5
        '
        'LayoutControlItem34
        '
        Me.LayoutControlItem34.AppearanceItemCaption.Options.UseTextOptions = True
        Me.LayoutControlItem34.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.LayoutControlItem34.Control = Me.txtAlkes
        Me.LayoutControlItem34.Location = New System.Drawing.Point(0, 140)
        Me.LayoutControlItem34.Name = "LayoutControlItem34"
        Me.LayoutControlItem34.Size = New System.Drawing.Size(321, 28)
        Me.LayoutControlItem34.Text = "Alkes :"
        Me.LayoutControlItem34.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.LayoutControlItem34.TextSize = New System.Drawing.Size(130, 20)
        Me.LayoutControlItem34.TextToControlDistance = 5
        '
        'LayoutControlItem35
        '
        Me.LayoutControlItem35.AppearanceItemCaption.Options.UseTextOptions = True
        Me.LayoutControlItem35.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.LayoutControlItem35.Control = Me.txtProsedurBedah
        Me.LayoutControlItem35.Location = New System.Drawing.Point(321, 0)
        Me.LayoutControlItem35.Name = "LayoutControlItem35"
        Me.LayoutControlItem35.Size = New System.Drawing.Size(304, 28)
        Me.LayoutControlItem35.Text = "Prosedur Bedah :"
        Me.LayoutControlItem35.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.LayoutControlItem35.TextSize = New System.Drawing.Size(130, 20)
        Me.LayoutControlItem35.TextToControlDistance = 5
        '
        'LayoutControlItem36
        '
        Me.LayoutControlItem36.AppearanceItemCaption.Options.UseTextOptions = True
        Me.LayoutControlItem36.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.LayoutControlItem36.Control = Me.txtKeperawatan
        Me.LayoutControlItem36.Location = New System.Drawing.Point(321, 28)
        Me.LayoutControlItem36.Name = "LayoutControlItem36"
        Me.LayoutControlItem36.Size = New System.Drawing.Size(304, 28)
        Me.LayoutControlItem36.Text = "Keperawatan :"
        Me.LayoutControlItem36.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.LayoutControlItem36.TextSize = New System.Drawing.Size(130, 20)
        Me.LayoutControlItem36.TextToControlDistance = 5
        '
        'LayoutControlItem37
        '
        Me.LayoutControlItem37.AppearanceItemCaption.Options.UseTextOptions = True
        Me.LayoutControlItem37.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.LayoutControlItem37.Control = Me.txtLaboratorium
        Me.LayoutControlItem37.Location = New System.Drawing.Point(321, 56)
        Me.LayoutControlItem37.Name = "LayoutControlItem37"
        Me.LayoutControlItem37.Size = New System.Drawing.Size(304, 28)
        Me.LayoutControlItem37.Text = "Laboratorium :"
        Me.LayoutControlItem37.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.LayoutControlItem37.TextSize = New System.Drawing.Size(130, 20)
        Me.LayoutControlItem37.TextToControlDistance = 5
        '
        'LayoutControlItem38
        '
        Me.LayoutControlItem38.AppearanceItemCaption.Options.UseTextOptions = True
        Me.LayoutControlItem38.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.LayoutControlItem38.Control = Me.txtKamarAkomodasi
        Me.LayoutControlItem38.Location = New System.Drawing.Point(321, 84)
        Me.LayoutControlItem38.Name = "LayoutControlItem38"
        Me.LayoutControlItem38.Size = New System.Drawing.Size(304, 28)
        Me.LayoutControlItem38.Text = "Kamar / Akomodasi :"
        Me.LayoutControlItem38.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.LayoutControlItem38.TextSize = New System.Drawing.Size(130, 20)
        Me.LayoutControlItem38.TextToControlDistance = 5
        '
        'LayoutControlItem39
        '
        Me.LayoutControlItem39.AppearanceItemCaption.Options.UseTextOptions = True
        Me.LayoutControlItem39.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.LayoutControlItem39.Control = Me.txtObatKronis
        Me.LayoutControlItem39.Location = New System.Drawing.Point(321, 112)
        Me.LayoutControlItem39.Name = "LayoutControlItem39"
        Me.LayoutControlItem39.Size = New System.Drawing.Size(304, 28)
        Me.LayoutControlItem39.Text = "Obat Kronis :"
        Me.LayoutControlItem39.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.LayoutControlItem39.TextSize = New System.Drawing.Size(130, 20)
        Me.LayoutControlItem39.TextToControlDistance = 5
        '
        'LayoutControlItem40
        '
        Me.LayoutControlItem40.AppearanceItemCaption.Options.UseTextOptions = True
        Me.LayoutControlItem40.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.LayoutControlItem40.Control = Me.txtBMHP
        Me.LayoutControlItem40.Location = New System.Drawing.Point(321, 140)
        Me.LayoutControlItem40.Name = "LayoutControlItem40"
        Me.LayoutControlItem40.Size = New System.Drawing.Size(304, 28)
        Me.LayoutControlItem40.Text = "BMHP :"
        Me.LayoutControlItem40.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.LayoutControlItem40.TextSize = New System.Drawing.Size(130, 20)
        Me.LayoutControlItem40.TextToControlDistance = 5
        '
        'LayoutControlItem41
        '
        Me.LayoutControlItem41.AppearanceItemCaption.Options.UseTextOptions = True
        Me.LayoutControlItem41.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.LayoutControlItem41.Control = Me.txtKonsultasi
        Me.LayoutControlItem41.Location = New System.Drawing.Point(625, 0)
        Me.LayoutControlItem41.Name = "LayoutControlItem41"
        Me.LayoutControlItem41.Size = New System.Drawing.Size(320, 28)
        Me.LayoutControlItem41.Text = "Konsultasi :"
        Me.LayoutControlItem41.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.LayoutControlItem41.TextSize = New System.Drawing.Size(130, 20)
        Me.LayoutControlItem41.TextToControlDistance = 5
        '
        'LayoutControlItem42
        '
        Me.LayoutControlItem42.AppearanceItemCaption.Options.UseTextOptions = True
        Me.LayoutControlItem42.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.LayoutControlItem42.Control = Me.txtPenunjang
        Me.LayoutControlItem42.Location = New System.Drawing.Point(625, 28)
        Me.LayoutControlItem42.Name = "LayoutControlItem42"
        Me.LayoutControlItem42.Size = New System.Drawing.Size(320, 28)
        Me.LayoutControlItem42.Text = "Penunjang :"
        Me.LayoutControlItem42.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.LayoutControlItem42.TextSize = New System.Drawing.Size(130, 20)
        Me.LayoutControlItem42.TextToControlDistance = 5
        '
        'LayoutControlItem43
        '
        Me.LayoutControlItem43.AppearanceItemCaption.Options.UseTextOptions = True
        Me.LayoutControlItem43.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.LayoutControlItem43.Control = Me.txtPelayananDarah
        Me.LayoutControlItem43.Location = New System.Drawing.Point(625, 56)
        Me.LayoutControlItem43.Name = "LayoutControlItem43"
        Me.LayoutControlItem43.Size = New System.Drawing.Size(320, 28)
        Me.LayoutControlItem43.Text = "Pelayanan Darah :"
        Me.LayoutControlItem43.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.LayoutControlItem43.TextSize = New System.Drawing.Size(130, 20)
        Me.LayoutControlItem43.TextToControlDistance = 5
        '
        'LayoutControlItem44
        '
        Me.LayoutControlItem44.AppearanceItemCaption.Options.UseTextOptions = True
        Me.LayoutControlItem44.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.LayoutControlItem44.Control = Me.txtRawatIntensif
        Me.LayoutControlItem44.Location = New System.Drawing.Point(625, 84)
        Me.LayoutControlItem44.Name = "LayoutControlItem44"
        Me.LayoutControlItem44.Size = New System.Drawing.Size(320, 28)
        Me.LayoutControlItem44.Text = "Rawat Intensif :"
        Me.LayoutControlItem44.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.LayoutControlItem44.TextSize = New System.Drawing.Size(130, 20)
        Me.LayoutControlItem44.TextToControlDistance = 5
        '
        'LayoutControlItem45
        '
        Me.LayoutControlItem45.AppearanceItemCaption.Options.UseTextOptions = True
        Me.LayoutControlItem45.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.LayoutControlItem45.Control = Me.txtObatKemoTerapi
        Me.LayoutControlItem45.Location = New System.Drawing.Point(625, 112)
        Me.LayoutControlItem45.Name = "LayoutControlItem45"
        Me.LayoutControlItem45.Size = New System.Drawing.Size(320, 28)
        Me.LayoutControlItem45.Text = "Obat Kemoterapi :"
        Me.LayoutControlItem45.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.LayoutControlItem45.TextSize = New System.Drawing.Size(130, 20)
        Me.LayoutControlItem45.TextToControlDistance = 5
        '
        'LayoutControlItem46
        '
        Me.LayoutControlItem46.AppearanceItemCaption.Options.UseTextOptions = True
        Me.LayoutControlItem46.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.LayoutControlItem46.Control = Me.txtSewaAlat
        Me.LayoutControlItem46.Location = New System.Drawing.Point(625, 140)
        Me.LayoutControlItem46.Name = "LayoutControlItem46"
        Me.LayoutControlItem46.Size = New System.Drawing.Size(320, 28)
        Me.LayoutControlItem46.Text = "Sewa Alat :"
        Me.LayoutControlItem46.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.LayoutControlItem46.TextSize = New System.Drawing.Size(130, 20)
        Me.LayoutControlItem46.TextToControlDistance = 5
        '
        'LayoutControlItem28
        '
        Me.LayoutControlItem28.AppearanceItemCaption.Options.UseTextOptions = True
        Me.LayoutControlItem28.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.LayoutControlItem28.Control = Me.txttarifRumahSakit
        Me.LayoutControlItem28.Location = New System.Drawing.Point(461, 404)
        Me.LayoutControlItem28.Name = "LayoutControlItem28"
        Me.LayoutControlItem28.Size = New System.Drawing.Size(164, 28)
        Me.LayoutControlItem28.Text = "Tarif Rumah sakit :"
        Me.LayoutControlItem28.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.LayoutControlItem28.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem28.TextToControlDistance = 0
        Me.LayoutControlItem28.TextVisible = False
        '
        'EmptySpaceItem1
        '
        Me.EmptySpaceItem1.AllowHotTrack = False
        Me.EmptySpaceItem1.Location = New System.Drawing.Point(625, 404)
        Me.EmptySpaceItem1.Name = "EmptySpaceItem1"
        Me.EmptySpaceItem1.Size = New System.Drawing.Size(328, 28)
        Me.EmptySpaceItem1.TextSize = New System.Drawing.Size(0, 0)
        '
        'LayoutControlItem47
        '
        Me.LayoutControlItem47.AppearanceItemCaption.Options.UseTextOptions = True
        Me.LayoutControlItem47.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.LayoutControlItem47.Control = Me.txtICD_X
        Me.LayoutControlItem47.Location = New System.Drawing.Point(0, 659)
        Me.LayoutControlItem47.Name = "LayoutControlItem47"
        Me.LayoutControlItem47.Size = New System.Drawing.Size(461, 30)
        Me.LayoutControlItem47.Text = "Diagnosa (ICD-10) :"
        Me.LayoutControlItem47.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.LayoutControlItem47.TextSize = New System.Drawing.Size(150, 20)
        Me.LayoutControlItem47.TextToControlDistance = 5
        '
        'LayoutControlItem48
        '
        Me.LayoutControlItem48.AppearanceItemCaption.Options.UseTextOptions = True
        Me.LayoutControlItem48.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.LayoutControlItem48.Control = Me.txtICCD_IX
        Me.LayoutControlItem48.Location = New System.Drawing.Point(461, 659)
        Me.LayoutControlItem48.Name = "LayoutControlItem48"
        Me.LayoutControlItem48.Size = New System.Drawing.Size(492, 30)
        Me.LayoutControlItem48.Text = "Prosedur (ICD-9-CM) :"
        Me.LayoutControlItem48.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.LayoutControlItem48.TextSize = New System.Drawing.Size(150, 20)
        Me.LayoutControlItem48.TextToControlDistance = 5
        '
        'LayoutControlItem23
        '
        Me.LayoutControlItem23.Control = Me.grd
        Me.LayoutControlItem23.Location = New System.Drawing.Point(953, 0)
        Me.LayoutControlItem23.Name = "LayoutControlItem23"
        Me.LayoutControlItem23.Size = New System.Drawing.Size(499, 689)
        Me.LayoutControlItem23.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem23.TextVisible = False
        '
        'LayoutControlItem11
        '
        Me.LayoutControlItem11.AppearanceItemCaption.Options.UseTextOptions = True
        Me.LayoutControlItem11.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.LayoutControlItem11.Control = Me.grdICD_X
        Me.LayoutControlItem11.Location = New System.Drawing.Point(0, 631)
        Me.LayoutControlItem11.Name = "LayoutControlItem11"
        Me.LayoutControlItem11.Size = New System.Drawing.Size(461, 28)
        Me.LayoutControlItem11.Text = "Referensi ICD X :"
        Me.LayoutControlItem11.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.LayoutControlItem11.TextSize = New System.Drawing.Size(150, 20)
        Me.LayoutControlItem11.TextToControlDistance = 5
        '
        'LayoutControlItem21
        '
        Me.LayoutControlItem21.AppearanceItemCaption.Options.UseTextOptions = True
        Me.LayoutControlItem21.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.LayoutControlItem21.Control = Me.grdICD_IX
        Me.LayoutControlItem21.Location = New System.Drawing.Point(461, 631)
        Me.LayoutControlItem21.Name = "LayoutControlItem21"
        Me.LayoutControlItem21.Size = New System.Drawing.Size(492, 28)
        Me.LayoutControlItem21.Text = "Refefensi ICD IX :"
        Me.LayoutControlItem21.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.LayoutControlItem21.TextSize = New System.Drawing.Size(150, 20)
        Me.LayoutControlItem21.TextToControlDistance = 5
        '
        'LayoutControlItem3
        '
        Me.LayoutControlItem3.Control = Me.txtCARI
        Me.LayoutControlItem3.Location = New System.Drawing.Point(282, 32)
        Me.LayoutControlItem3.Name = "LayoutControlItem3"
        Me.LayoutControlItem3.Size = New System.Drawing.Size(201, 28)
        Me.LayoutControlItem3.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem3.TextVisible = False
        '
        'lKDPENDAFATRAN
        '
        Me.lKDPENDAFATRAN.AppearanceItemCaption.Options.UseTextOptions = True
        Me.lKDPENDAFATRAN.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.lKDPENDAFATRAN.Control = Me.grdKDCASHIN
        Me.lKDPENDAFATRAN.Location = New System.Drawing.Point(483, 32)
        Me.lKDPENDAFATRAN.Name = "lKDPENDAFATRAN"
        Me.lKDPENDAFATRAN.Size = New System.Drawing.Size(470, 28)
        Me.lKDPENDAFATRAN.Text = "No Register :"
        Me.lKDPENDAFATRAN.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.lKDPENDAFATRAN.TextSize = New System.Drawing.Size(0, 0)
        Me.lKDPENDAFATRAN.TextToControlDistance = 0
        Me.lKDPENDAFATRAN.TextVisible = False
        '
        'LayoutControlItem22
        '
        Me.LayoutControlItem22.AppearanceItemCaption.Options.UseTextOptions = True
        Me.LayoutControlItem22.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.LayoutControlItem22.Control = Me.rbCategory
        Me.LayoutControlItem22.Location = New System.Drawing.Point(282, 0)
        Me.LayoutControlItem22.Name = "LayoutControlItem22"
        Me.LayoutControlItem22.Size = New System.Drawing.Size(671, 32)
        Me.LayoutControlItem22.Text = "Jenis Rawat :"
        Me.LayoutControlItem22.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.LayoutControlItem22.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem22.TextToControlDistance = 0
        Me.LayoutControlItem22.TextVisible = False
        '
        'LayoutControlItem27
        '
        Me.LayoutControlItem27.Control = Me.LabelControl12
        Me.LayoutControlItem27.Location = New System.Drawing.Point(0, 404)
        Me.LayoutControlItem27.Name = "LayoutControlItem27"
        Me.LayoutControlItem27.Size = New System.Drawing.Size(461, 28)
        Me.LayoutControlItem27.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem27.TextVisible = False
        '
        'frmGrouper
        '
        Me.Appearance.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(236, Byte), Integer), CType(CType(239, Byte), Integer))
        Me.Appearance.Options.UseBackColor = True
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1478, 744)
        Me.Controls.Add(Me.layoutControl)
        Me.Controls.Add(Me.barDockControlLeft)
        Me.Controls.Add(Me.barDockControlRight)
        Me.Controls.Add(Me.barDockControlBottom)
        Me.Controls.Add(Me.barDockControlTop)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
        Me.KeyPreview = True
        Me.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Name = "frmGrouper"
        Me.ShowIcon = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Grouper - Edit Form"
        CType(Me.grv1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grd, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grv, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.layoutControl, System.ComponentModel.ISupportInitialize).EndInit()
        Me.layoutControl.ResumeLayout(False)
        CType(Me.txtVENTILATOR.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.barManager, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.progressBarSave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.progressSave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtRAWATINTENSIF_HARI.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtLAMA.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rbKELASPELAYANAN.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkNaikKelas.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkAdaRawat.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rbKELASHAK.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rbCategory.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdICD_IX.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdICD_X.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdJenisTarif.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtHAKKELAS.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtTarifEksekutif.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtICCD_IX.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtICD_X.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtSewaAlat.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtObatKemoTerapi.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtRawatIntensif.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtPelayananDarah.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtPenunjang.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtKonsultasi.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtBMHP.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtObatKronis.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtKamarAkomodasi.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtLaboratorium.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtKeperawatan.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtProsedurBedah.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtAlkes.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtObat.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtRehabilitasi.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtRadiologi.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtTenagaAhli.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtProsedurNonBedah.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txttarifRumahSakit.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdCaraKeluar.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grvCaraKeluar, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtBeratBadan.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtUmur.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtADLScore_Chronic.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtADLScore_SubAcute.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtLOS.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkKelasEksekutif.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdCOB.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtNoSEP.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtNoPeserta.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cboCaraBayar.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCODE.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdDPJP.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdKDCASHIN.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grvKDCASHIN, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cboCARI.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCARI.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.deDATEPULANG.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.deDATEPULANG.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.deDATEMASUK.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.deDATEMASUK.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lKDSKD, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem7, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem8, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem9, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem10, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lKDDOCTOR, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem13, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem14, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem12, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lDATE, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lDATE_KONTROL, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LKELASPELAYANAN_RB, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem20, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lKELASPELAYANAN, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem50, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem51, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem52, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lNAIKKELAS, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lADARAWAT, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lRAWATINTENSIF_HARI, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lRAWATINTENSIF_HARI_TEXT, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lKELASEKSEKUTIF, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lKELASHAKRJ_LBL, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lKELASHAKRI, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lKELASHAKRJ, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem25, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem15, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lLAMA_LBL, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lLAMA, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lVENTILATOR, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lVENTILATOR_TEXT, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem26, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem16, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem49, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem18, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem54, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem17, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lTARIFEKSEKUTIF, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem29, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem30, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem31, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem32, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem33, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem34, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem35, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem36, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem37, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem38, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem39, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem40, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem41, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem42, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem43, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem44, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem45, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem46, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem28, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.EmptySpaceItem1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem47, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem48, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem23, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem11, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem21, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lKDPENDAFATRAN, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem22, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem27, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents layoutControl As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents LayoutControlGroup1 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents barManager As DevExpress.XtraBars.BarManager
    Friend WithEvents barTop As DevExpress.XtraBars.Bar
    Friend WithEvents barDockControlTop As DevExpress.XtraBars.BarDockControl
    Friend WithEvents barDockControlBottom As DevExpress.XtraBars.BarDockControl
    Friend WithEvents barDockControlLeft As DevExpress.XtraBars.BarDockControl
    Friend WithEvents barDockControlRight As DevExpress.XtraBars.BarDockControl
    Friend WithEvents btnSaveNew As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents btnClose As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents btnSaveClose As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents progressBarSave As DevExpress.XtraEditors.Repository.RepositoryItemMarqueeProgressBar
    Friend WithEvents progressSave As DevExpress.XtraEditors.Repository.RepositoryItemMarqueeProgressBar
    Friend WithEvents deDATEPULANG As DevExpress.XtraEditors.DateEdit
    Friend WithEvents deDATEMASUK As DevExpress.XtraEditors.DateEdit
    Friend WithEvents lDATE As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents lDATE_KONTROL As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents cboCARI As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents txtCARI As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LayoutControlItem3 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem4 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents grdKDCASHIN As DevExpress.XtraEditors.GridLookUpEdit
    Friend WithEvents grvKDCASHIN As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridColumn12 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn13 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn14 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents lKDPENDAFATRAN As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents grdDPJP As DevExpress.XtraEditors.GridLookUpEdit
    Friend WithEvents GridView4 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridColumn2 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents lKDDOCTOR As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents txtCODE As DevExpress.XtraEditors.TextEdit
    Friend WithEvents lKDSKD As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents GridColumn3 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn4 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents txtSewaAlat As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txtObatKemoTerapi As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txtRawatIntensif As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txtPelayananDarah As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txtPenunjang As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txtKonsultasi As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txtBMHP As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txtObatKronis As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txtKamarAkomodasi As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txtLaboratorium As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txtKeperawatan As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txtProsedurBedah As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txtAlkes As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txtObat As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txtRehabilitasi As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txtRadiologi As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txtTenagaAhli As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txtProsedurNonBedah As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txttarifRumahSakit As DevExpress.XtraEditors.TextEdit
    Friend WithEvents grdCaraKeluar As DevExpress.XtraEditors.GridLookUpEdit
    Friend WithEvents grvCaraKeluar As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridColumn6 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents txtBeratBadan As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txtUmur As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txtADLScore_Chronic As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txtADLScore_SubAcute As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txtLOS As DevExpress.XtraEditors.TextEdit
    Friend WithEvents chkKelasEksekutif As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents LabelControl4 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl3 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl2 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl1 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents grdCOB As DevExpress.XtraEditors.GridLookUpEdit
    Friend WithEvents GridView2 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridColumn5 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents txtNoSEP As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txtNoPeserta As DevExpress.XtraEditors.TextEdit
    Friend WithEvents cboCaraBayar As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents LayoutControlGroup2 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents LayoutControlItem1 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem2 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem5 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem6 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem7 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem8 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem9 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem10 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlGroup3 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents LayoutControlItem13 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem14 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem12 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents lKELASEKSEKUTIF As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem16 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem15 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem18 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlGroup5 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents LayoutControlItem29 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem30 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem31 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem32 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem33 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem34 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem35 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem36 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem37 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem38 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem39 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem40 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem41 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem42 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem43 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem44 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem45 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem46 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem28 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents btnICD As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents txtICCD_IX As DevExpress.XtraEditors.TextEdit
    Friend WithEvents btnProsedur As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents txtICD_X As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LayoutControlItem47 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem48 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents txtTarifEksekutif As DevExpress.XtraEditors.TextEdit
    Friend WithEvents lTARIFEKSEKUTIF As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents txtHAKKELAS As DevExpress.XtraEditors.TextEdit
    Friend WithEvents lKELASHAKRJ As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents EmptySpaceItem1 As DevExpress.XtraLayout.EmptySpaceItem
    Friend WithEvents btnUpdatePasien As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents grdJenisTarif As DevExpress.XtraEditors.GridLookUpEdit
    Friend WithEvents GridView1 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridColumn8 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents LayoutControlItem17 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents grd As DevExpress.XtraGrid.GridControl
    Friend WithEvents grv1 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents grv As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents LayoutControlItem23 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents GridColumn1 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn7 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn9 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn10 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn11 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn15 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn16 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn17 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents grdICD_IX As DevExpress.XtraEditors.GridLookUpEdit
    Friend WithEvents GridView5 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridColumn19 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents grdICD_X As DevExpress.XtraEditors.GridLookUpEdit
    Friend WithEvents GridView3 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridColumn18 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents LayoutControlItem11 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem21 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents GridColumn20 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn21 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents btnAddCustomer As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents GridColumn22 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn23 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents rbCategory As DevExpress.XtraEditors.RadioGroup
    Friend WithEvents LayoutControlItem22 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents rbKELASHAK As DevExpress.XtraEditors.RadioGroup
    Friend WithEvents lKELASHAKRI As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents chkNaikKelas As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents chkAdaRawat As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents lNAIKKELAS As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents lADARAWAT As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LabelControl9 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl8 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl7 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl6 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl5 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents rbKELASPELAYANAN As DevExpress.XtraEditors.RadioGroup
    Friend WithEvents LKELASPELAYANAN_RB As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem20 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents lKELASPELAYANAN As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem50 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem51 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem52 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LabelControl16 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl15 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl14 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl13 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl11 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents lKELASHAKRJ_LBL As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem25 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem26 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem49 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem54 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LabelControl12 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents txtLAMA As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LabelControl10 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents lLAMA_LBL As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents lLAMA As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem27 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents txtVENTILATOR As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LabelControl18 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents txtRAWATINTENSIF_HARI As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LabelControl17 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents lRAWATINTENSIF_HARI As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents lRAWATINTENSIF_HARI_TEXT As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents lVENTILATOR As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents lVENTILATOR_TEXT As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents GridColumn24 As DevExpress.XtraGrid.Columns.GridColumn
End Class
