Imports DataAccess
Imports UI.WIN.MAIN.My.Resources
Imports System.Linq
Imports Newtonsoft.Json.Linq
Imports System.Data.SqlClient

Public Class frmLaporanTindakan
#Region "Declaration"
    Private oFormMode As FORM_MODE = FORM_MODE.FORM_MODE_VIEW
    Private sNoId As String
    Private isLoad As Boolean = False
    Private oDigital_LaporanTindakan As New EMedrek.clsS_DIGITAL_OK_LAPORANTINDAKAN
    Private oBrigging As New Brigging.clsSetKoneksi
    Private sKDDOCTOR_INPUT As String = String.Empty
    Private sKDDOCTOR_DPJPUTAMA As String = String.Empty
    Private sRM As String = String.Empty
    Private sRegister As String = String.Empty
    Private sAutoClose As String = String.Empty
    Private sNamaPerawat As String = String.Empty

#End Region
#Region "Function"
    Public Sub LoadMe(ByVal oAutoClose As Boolean, ByVal FormMode As Integer, ByVal NoId As String, ByVal kddoctorinput As String, ByVal kddpjputama As String, ByVal RM As String, ByVal Register As String, ByVal NamaPerawat As String)
        oFormMode = FormMode
        sNoId = NoId
        sKDDOCTOR_INPUT = kddoctorinput
        sKDDOCTOR_DPJPUTAMA = kddpjputama
        sRM = RM
        sRegister = Register
        sAutoClose = oAutoClose
        sNamaPerawat = NamaPerawat
    End Sub
    Private Sub Form_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        fn_ChangeFormState()
        fn_LoadLanguage()
        isLoad = True
    End Sub
    Public Sub fn_LoadLanguage()
        Try
            Me.Text = "Laporan Tindakan"

            'btnSaveNew.Caption = Caption.FormSaveNew
            'btnSaveClose.Caption = Caption.FormSaveClose
            'btnClose.Caption = Caption.FormClose
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub Form_Closing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        'sCode = txtMEMO.Text.Trim.ToUpper
    End Sub
    Private Sub fn_ChangeFormState()
        fn_JENISOPERASI()
        fn_LoadOperator()
        fn_LoadPERAWAT()

        Select Case oFormMode
            Case FORM_MODE.FORM_MODE_VIEW
                fn_ViewMode(True)
                fn_LoadData()
            Case FORM_MODE.FORM_MODE_ADD
                fn_ViewMode(False)
                fn_EmptyMe()
            Case FORM_MODE.FORM_MODE_EDIT
                fn_ViewMode(False)
                fn_LoadData()
            Case Else
                fn_ViewMode(True)
        End Select
    End Sub
    Private Sub fn_ViewMode(ByVal Status As Boolean)
        'btnSaveNew.Enabled = False
        'btnSaveClose.Enabled = Not Status

        btnSimpanLaporanOperasi.Enabled = Not Status

        'TableLayoutPanel9.Anchor = AnchorStyles.Top
        'TableLayoutPanel9.Anchor = AnchorStyles.Left
        'TableLayoutPanel9.Anchor = AnchorStyles.Right

        'GroupControl9.Anchor = AnchorStyles.Top
        'GroupControl9.Anchor = AnchorStyles.Left
        'GroupControl9.Anchor = AnchorStyles.Right
    End Sub
    Private Sub fn_EmptyMe()
        DateEdit1.DateTime = Now
        grdOperator_LO.ResetText()
        'grdAnestesi_LO.ResetText()
        grdAsisten1_LO.ResetText()
        'grdAsisten2_LO.ResetText()
        'TextEdit10.ResetText()
        TextEdit11.ResetText()
        TextEdit12.ResetText()
        TimeEdit13.Time = Now.ToString("yyyy-MM-dd") & " 00:00"
        TimeEdit14.Time = Now.ToString("yyyy-MM-dd") & " 00:00"
        TimeEdit15.Time = Now.ToString("yyyy-MM-dd") & " 00:00"
        txtLamaOperasi.Text = "00:00"
        TextEdit17.ResetText()
        'TextEdit18.Text = sUserID
        TextEdit19.ResetText()
        'grdPerawatAnestesiLO.ResetText()
        MemoEdit4.ResetText()
        MemoEdit5.ResetText()
        MemoEdit6.ResetText()
        'CheckEdit1.Checked = False
        'CheckEdit2.Checked = False
        'CheckEdit3.Checked = False
        'CheckEdit4.Checked = False
        'CheckEdit5.Checked = False
        'CheckEdit6.Checked = False
        txtKAMARTINDAKAN.ResetText()
        CheckEdit7.Checked = False
        CheckEdit8.Checked = False
        CheckEdit9.Checked = False
        CheckEdit10.Checked = False
        CheckEdit11.Checked = False
        CheckEdit12.Checked = False
        CheckEdit13.Checked = False
        CheckEdit14.Checked = False
        CheckEdit15.Checked = False
        CheckEdit16.Checked = False
        CheckEdit17.Checked = False
        CheckEdit18.Checked = False
        CheckEdit19.Checked = False
        CheckEdit20.Checked = False
        cboJenisOperasi.ResetText()
        chkPemasanganImpan_Tidak.Checked = False
        chkPemasanganImpan_Ya.Checked = False
        grdPerawatInstrumen.ResetText()

        MemoEdit1.ResetText()
        MemoEdit2.ResetText()
        MemoEdit3.ResetText()

        TextEdit19.Text = sKDDOCTOR_INPUT

        If sNamaPerawat = "" Then
            txtPembuatLaporan.Text = TextEdit19.Text
        Else
            txtPembuatLaporan.Text = sNamaPerawat
        End If

        grdOperator_LO.Text = sKDDOCTOR_INPUT

        grvLODiagnosaPre.OptionsSelection.MultiSelect = True
        grvLODiagnosaPre.SelectAll()
        grvLODiagnosaPre.DeleteSelectedRows()
        grvLODiagnosaPre.OptionsSelection.MultiSelect = False

        'grvLODiagnosa2.OptionsSelection.MultiSelect = True
        'grvLODiagnosa2.SelectAll()
        'grvLODiagnosa2.DeleteSelectedRows()
        'grvLODiagnosa2.OptionsSelection.MultiSelect = False

        grvLOProsedur.OptionsSelection.MultiSelect = True
        grvLOProsedur.SelectAll()
        grvLOProsedur.DeleteSelectedRows()
        grvLOProsedur.OptionsSelection.MultiSelect = False

        grdCariDiganosaLO1.ResetText()
        'grdCariDiganosaLO2.ResetText()
        grdCariProsedur_LO.ResetText()
    End Sub
    Private Sub fn_LoadData()
        Try
            ' ***** HEADER *****
            Dim ds = oDigital_LaporanTindakan.GetData(sNoId)
            With ds
                DateEdit1.DateTime = .DATE
                grdOperator_LO.EditValue = .DOKTER_KODE
                'grdAnestesi_LO.EditValue = .TextEdit3
                TextEdit19.EditValue = .TextEdit19

                grdAsisten1_LO.EditValue = .TextEdit5
                'grdAsisten2_LO.Text = .TextEdit6
                grdPerawatInstrumen.Text = .TextEdit7
                'grdPerawatAnestesiLO.Text = .TextEdit9

                'TextEdit10.Text = .TextEdit10
                TextEdit11.Text = .TextEdit11
                TextEdit12.Text = .TextEdit12
                TimeEdit13.Time = .TextEdit13
                TimeEdit14.Time = .TextEdit14
                TimeEdit15.Time = .TextEdit15
                txtLamaOperasi.Text = .TextEdit16
                TextEdit17.Text = .TextEdit17
                'TextEdit18.Text = .TextEdit18

                MemoEdit4.Text = .MemoEdit4
                MemoEdit5.Text = .MemoEdit5
                MemoEdit6.Text = .MemoEdit6
                'CheckEdit1.Checked = .CheckEdit1
                'CheckEdit2.Checked = .CheckEdit2
                'CheckEdit3.Checked = .CheckEdit3
                'CheckEdit4.Checked = .CheckEdit4
                'CheckEdit5.Checked = .CheckEdit5
                'CheckEdit6.Checked = .CheckEdit6
                txtKAMARTINDAKAN.Text = .TextEdit10
                CheckEdit7.Checked = .CheckEdit7
                CheckEdit8.Checked = .CheckEdit8
                CheckEdit9.Checked = .CheckEdit9
                CheckEdit10.Checked = .CheckEdit10
                CheckEdit11.Checked = .CheckEdit11
                CheckEdit12.Checked = .CheckEdit12
                CheckEdit13.Checked = .CheckEdit13
                CheckEdit14.Checked = .CheckEdit14
                CheckEdit15.Checked = .CheckEdit15
                CheckEdit16.Checked = .CheckEdit16
                CheckEdit17.Checked = .CheckEdit17
                CheckEdit18.Checked = .CheckEdit18
                CheckEdit19.Checked = .CheckEdit19
                CheckEdit20.Checked = .CheckEdit20
                cboJenisOperasi.EditValue = .JENIS_OPERASI
                'txtDokter_LO.EditValue = .DOKTER_KODE

                'Pembuat Laporan
                txtPembuatLaporan.Text = .TextEdit4

                MemoEdit1.Text = .MemoEdit1
                MemoEdit2.Text = .MemoEdit2
                MemoEdit3.Text = .MemoEdit3

                chkPemasanganImpan_Tidak.Checked = .CheckEdit1
                chkPemasanganImpan_Ya.Checked = .CheckEdit2

                BindingSource1.DataSource = oDigital_LaporanTindakan.GetDataDetailDiagnosa(.KDLAPORANTINDAKAN).OrderBy(Function(x) x.SEQ).ToList()
                grdLODiagnosaPre.DataSource = BindingSource1

                'BindingSource2.DataSource = oDigital_LaporanTindakan.GetDataDetailDiagnosa2(.KDLAPORANTINDAKAN).OrderBy(Function(x) x.SEQ).ToList()
                'grdLODiagnosa2.DataSource = BindingSource2

                BindingSource3.DataSource = oDigital_LaporanTindakan.GetDataDetailProsedur(.KDLAPORANTINDAKAN).OrderBy(Function(x) x.SEQ).ToList()
                grdLOProsedur.DataSource = BindingSource3
            End With
        Catch oErr As Exception
            MsgBox("Load List Data Laporan Operasi: " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Function fn_Validate() As Boolean
        Try
            fn_Validate = True
            If cboJenisOperasi.Text = String.Empty Then
                MsgBox("Dibutuhkan Laporan Jenis Tindakan", MsgBoxStyle.Exclamation, Me.Text)
                cboJenisOperasi.Focus()
                fn_Validate = False
                Exit Function
            End If
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Function
    Private Function fn_Save() As Boolean
        Try
            ' ***** HEADER *****

            Dim ds = oDigital_LaporanTindakan.GetStructureHeader
            With ds
                .KDLAPORANTINDAKAN = sNoId
                .KDPENDAFTARAN = sRegister
                .KDCUSTOMER = sRM
                Try
                    .DATECREATED = oDigital_LaporanTindakan.GetData(sNoId).DATECREATED
                Catch ex As Exception
                    .DATECREATED = Now
                End Try
                .DATEUPDATED = Now
                .JENIS_OPERASI = cboJenisOperasi.EditValue
                .DATE = DateEdit1.DateTime

                .TextEdit1 = grdOperator_LO.EditValue
                .TextEdit2 = grdOperator_LO.Text
                .TextEdit19 = TextEdit19.EditValue
                .TextEdit8 = TextEdit19.Text
                .TextEdit3 = ""
                .TextEdit4 = txtPembuatLaporan.Text

                .TextEdit5 = grdAsisten1_LO.Text
                .TextEdit6 = ""
                .TextEdit7 = grdPerawatInstrumen.Text
                .TextEdit9 = ""
                .TextEdit10 = txtKAMARTINDAKAN.Text
                .TextEdit11 = TextEdit11.Text
                .TextEdit12 = TextEdit12.Text
                .TextEdit13 = TimeEdit13.Time
                .TextEdit14 = TimeEdit14.Time
                .TextEdit15 = TimeEdit15.Time
                .TextEdit16 = txtLamaOperasi.Text
                .TextEdit17 = TextEdit17.Text
                .TextEdit18 = sUserID
                .MemoEdit1 = MemoEdit1.Text
                .MemoEdit2 = MemoEdit2.Text
                .MemoEdit3 = MemoEdit3.Text
                .BARCODEALAT = ""
                .MemoEdit7 = ""
                .MemoEdit8 = ""

                'Dim listdiagnosa1 As New List(Of String)
                'For i As Integer = 0 To grvLODiagnosaPre.RowCount - 2
                '    listdiagnosa1.Add(grvLODiagnosaPre.GetRowCellValue(i, colNAMADIAGNOSA_LO) & grvLODiagnosaPre.GetRowCellValue(i, colKDDIAGNOSA_LO) & " (" & grvLODiagnosaPre.GetRowCellValue(i, colKATEGORI_LO) & ")")
                'Next

                Dim listdiagnosa1 As New List(Of String)
                For i As Integer = 0 To grvLODiagnosaPre.RowCount - 2
                    listdiagnosa1.Add(grvLODiagnosaPre.GetRowCellValue(i, colNAMADIAGNOSA_LO))
                Next

                .TXTDIAGNOSAPOSTOP2 = String.Join(vbCrLf, listdiagnosa1.ToArray)

                'Dim listdiagnosa2 As New List(Of String)
                'For i As Integer = 0 To grvLODiagnosa2.RowCount - 2
                '    listdiagnosa2.Add(grvLODiagnosa2.GetRowCellValue(i, colNAMADIAGNOSA_LO2))
                'Next

                .TXTDIAGNOSAPOSTOP3 = ""

                Dim listPrsedur As New List(Of String)
                For i As Integer = 0 To grvLOProsedur.RowCount - 2
                    listPrsedur.Add(grvLOProsedur.GetRowCellValue(i, colNAMAPROSEDUR_LO))
                Next

                .PROSEDUR2 = String.Join(vbCrLf, listPrsedur.ToArray)
                .PROSEDUR3 = ""
                .MemoEdit4 = MemoEdit4.Text
                .MemoEdit5 = MemoEdit5.Text
                .MemoEdit6 = MemoEdit6.Text
                .CheckEdit1 = chkPemasanganImpan_Tidak.Checked
                .CheckEdit2 = chkPemasanganImpan_Ya.Checked
                .CheckEdit3 = False
                .CheckEdit4 = False
                .CheckEdit5 = False
                .CheckEdit6 = False
                .CheckEdit7 = CheckEdit7.Checked
                .CheckEdit8 = CheckEdit8.Checked
                .CheckEdit9 = CheckEdit9.Checked
                .CheckEdit10 = CheckEdit10.Checked
                .CheckEdit11 = CheckEdit11.Checked
                .CheckEdit12 = CheckEdit12.Checked
                .CheckEdit13 = CheckEdit13.Checked
                .CheckEdit14 = CheckEdit14.Checked
                .CheckEdit15 = CheckEdit15.Checked
                .CheckEdit16 = CheckEdit16.Checked
                .CheckEdit17 = CheckEdit17.Checked
                .CheckEdit18 = CheckEdit18.Checked
                .CheckEdit19 = CheckEdit19.Checked
                .CheckEdit20 = CheckEdit20.Checked
                .KDDOCTOR = sKDDOCTOR_DPJPUTAMA
                .BARCODEALAT = ""
                .DOKTER_KODE = ""
                .DOKTER_NAMEDISPLAY = ""

                .DOKTER_KODE = grdOperator_LO.EditValue
                .DOKTER_NAMEDISPLAY = grdOperator_LO.Text

                .SEQ = 0
                Try
                    .CETAK = oDigital_LaporanTindakan.GetData(sNoId).CETAK
                Catch ex As Exception
                    .CETAK = 0
                End Try
                .KDUSER = sUserID
                .KDUSER_SIGNATURE = ""
                .DATEDELETE = Now
                Try
                    .ISDELETE = oDigital_LaporanTindakan.GetData(sNoId).ISDELETE
                Catch ex As Exception
                    .ISDELETE = 0
                End Try


            End With

            'DIAGNOSA
            Dim arrDetailDiagnosa = oDigital_LaporanTindakan.GetStructureDetailDiagnosaList
            For i As Integer = 0 To grvLODiagnosaPre.RowCount - 2
                Dim dsDetail = oDigital_LaporanTindakan.GetStructureDetailDiagnosa
                With dsDetail
                    .SEQ = i
                    .KDLAPORANTINDAKAN = ds.KDLAPORANTINDAKAN
                    .KATEGORI = IIf(i = 0, "Primer", "Sekunder")
                    .NAMADIAGNOSA = IIf(String.IsNullOrEmpty(grvLODiagnosaPre.GetRowCellValue(i, colNAMADIAGNOSA_LO)), "", grvLODiagnosaPre.GetRowCellValue(i, colNAMADIAGNOSA_LO))
                    .KDDIAGNOSA = IIf(String.IsNullOrEmpty(grvLODiagnosaPre.GetRowCellValue(i, colKDDIAGNOSA_LO)), "", grvLODiagnosaPre.GetRowCellValue(i, colKDDIAGNOSA_LO))
                End With
                arrDetailDiagnosa.Add(dsDetail)
            Next

            'Dim arrDetailDiagnosa2 = oDigital_LaporanTindakan.GetStructureDetailDiagnosa2List
            'For i As Integer = 0 To grvLODiagnosa2.RowCount - 2
            '    Dim dsDetail = oDigital_LaporanTindakan.GetStructureDetailDiagnosa2
            '    With dsDetail
            '        .SEQ = i
            '        .KDLAPORANTINDAKAN = ds.KDLAPORANTINDAKAN
            '        .KATEGORI = IIf(i = 0, "Primer", "Sekunder")
            '        .NAMADIAGNOSA = IIf(String.IsNullOrEmpty(grvLODiagnosa2.GetRowCellValue(i, colNAMADIAGNOSA_LO2)), "", grvLODiagnosa2.GetRowCellValue(i, colNAMADIAGNOSA_LO2))
            '        .KDDIAGNOSA = IIf(String.IsNullOrEmpty(grvLODiagnosa2.GetRowCellValue(i, colKDDIAGNOSA_LO2)), "", grvLODiagnosa2.GetRowCellValue(i, colKDDIAGNOSA_LO2))
            '    End With
            '    arrDetailDiagnosa2.Add(dsDetail)
            'Next

            'PROSEDUR
            Dim arrDetailProsedur = oDigital_LaporanTindakan.GetStructureDetailProsedurList
            For i As Integer = 0 To grvLOProsedur.RowCount - 2
                Dim dsDetail = oDigital_LaporanTindakan.GetStructureDetailProsedur
                With dsDetail
                    .SEQ = i
                    .KDLAPORANTINDAKAN = ds.KDLAPORANTINDAKAN
                    .KATEGORI = ""
                    .NAMAPROSEDUR = IIf(String.IsNullOrEmpty(grvLOProsedur.GetRowCellValue(i, colNAMAPROSEDUR_LO)), "", grvLOProsedur.GetRowCellValue(i, colNAMAPROSEDUR_LO))
                    .KDPROSEDUR = IIf(String.IsNullOrEmpty(grvLOProsedur.GetRowCellValue(i, colKDPROSEDUR_LO)), "", grvLOProsedur.GetRowCellValue(i, colKDPROSEDUR_LO))
                End With
                arrDetailProsedur.Add(dsDetail)
            Next

            If oFormMode = FORM_MODE.FORM_MODE_ADD Then
                Try
                    fn_Save = oDigital_LaporanTindakan.InsertData(ds, arrDetailDiagnosa, Nothing, arrDetailProsedur)

                    sNoId = ds.KDLAPORANTINDAKAN
                Catch ex As Exception
                    MsgBox("Simpan Data Laporan Operasi: " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            Else
                Try
                    fn_Save = oDigital_LaporanTindakan.UpdateData(ds, arrDetailDiagnosa, Nothing, arrDetailProsedur)
                Catch ex As Exception
                    MsgBox("Simpan Data Laporan Operasi: " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            End If
        Catch oErr As Exception
            MsgBox("Simpan Data Laporan Operasi: " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            fn_Save = False
        End Try
    End Function
    Private Sub fn_LoadDataLaporanTindakan(ByVal Kode As String)
        Try
            ' ***** HEADER *****
            Dim ds = oDigital_LaporanTindakan.GetDataTemplate(Kode)
            If ds Is Nothing Then Exit Sub

            With ds
                'DateEdit1.DateTime = .DATE
                grdOperator_LO.EditValue = .DOKTER_KODE
                'grdAnestesi_LO.EditValue = .TextEdit3
                TextEdit19.EditValue = .TextEdit19

                grdAsisten1_LO.EditValue = .TextEdit5
                'grdAsisten2_LO.Text = .TextEdit6
                grdPerawatInstrumen.Text = .TextEdit7
                'grdPerawatAnestesiLO.Text = .TextEdit9

                'TextEdit10.Text = .TextEdit10
                TextEdit11.Text = .TextEdit11
                TextEdit12.Text = .TextEdit12
                TimeEdit13.Time = .TextEdit13
                TimeEdit14.Time = .TextEdit14
                TimeEdit15.Time = .TextEdit15
                txtLamaOperasi.Text = .TextEdit16
                TextEdit17.Text = .TextEdit17
                'TextEdit18.Text = .TextEdit18

                MemoEdit4.Text = .MemoEdit4
                MemoEdit5.Text = .MemoEdit5
                MemoEdit6.Text = .MemoEdit6
                'CheckEdit1.Checked = .CheckEdit1
                'CheckEdit2.Checked = .CheckEdit2
                'CheckEdit3.Checked = .CheckEdit3
                'CheckEdit4.Checked = .CheckEdit4
                'CheckEdit5.Checked = .CheckEdit5
                'CheckEdit6.Checked = .CheckEdit6
                txtKAMARTINDAKAN.Text = .TextEdit10
                CheckEdit7.Checked = .CheckEdit7
                CheckEdit8.Checked = .CheckEdit8
                CheckEdit9.Checked = .CheckEdit9
                CheckEdit10.Checked = .CheckEdit10
                CheckEdit11.Checked = .CheckEdit11
                CheckEdit12.Checked = .CheckEdit12
                CheckEdit13.Checked = .CheckEdit13
                CheckEdit14.Checked = .CheckEdit14
                CheckEdit15.Checked = .CheckEdit15
                CheckEdit16.Checked = .CheckEdit16
                CheckEdit17.Checked = .CheckEdit17
                CheckEdit18.Checked = .CheckEdit18
                CheckEdit19.Checked = .CheckEdit19
                CheckEdit20.Checked = .CheckEdit20
                cboJenisOperasi.EditValue = .JENIS_OPERASI
                'txtDokter_LO.EditValue = .DOKTER_KODE

                MemoEdit1.Text = .MemoEdit1
                MemoEdit2.Text = .MemoEdit2
                MemoEdit3.Text = .MemoEdit3

                chkPemasanganImpan_Tidak.Checked = .CheckEdit1
                chkPemasanganImpan_Ya.Checked = .CheckEdit2

                BindingSource1.DataSource = oDigital_LaporanTindakan.GetDataDetailDiagnosaTemplate(.KDJUDUL).OrderBy(Function(x) x.SEQ).ToList()
                grdLODiagnosaPre.DataSource = BindingSource1

                'BindingSource2.DataSource = oDigital_LaporanTindakan.GetDataDetailDiagnosa2Template(.KDJUDUL).OrderBy(Function(x) x.SEQ).ToList()
                'grdLODiagnosa2.DataSource = BindingSource2

                BindingSource3.DataSource = oDigital_LaporanTindakan.GetDataDetailProsedurTemplate(.KDJUDUL).OrderBy(Function(x) x.SEQ).ToList()
                grdLOProsedur.DataSource = BindingSource3
            End With
        Catch oErr As Exception
            MsgBox("Load List Data Laporan Operasi: " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Function fn_SaveLaporanTindakan() As Boolean
        Try
            ' ***** HEADER *****

            Dim ds = oDigital_LaporanTindakan.GetStructureHeaderTemplate
            With ds
                .KDJUDUL = sKODEASESMENCOPY
                .KDPENDAFTARAN = sRegister
                .KDCUSTOMER = sRM
                Try
                    .DATECREATED = oDigital_LaporanTindakan.GetData(sNoId).DATECREATED
                Catch ex As Exception
                    .DATECREATED = Now
                End Try
                .DATEUPDATED = Now
                .JENIS_OPERASI = cboJenisOperasi.EditValue
                .DATE = DateEdit1.DateTime

                .TextEdit1 = grdOperator_LO.EditValue
                .TextEdit2 = grdOperator_LO.Text
                .TextEdit19 = TextEdit19.EditValue
                .TextEdit8 = TextEdit19.Text
                .TextEdit3 = ""
                .TextEdit4 = ""

                .TextEdit5 = grdAsisten1_LO.Text
                .TextEdit6 = ""
                .TextEdit7 = grdPerawatInstrumen.Text
                .TextEdit9 = ""
                .TextEdit10 = txtKAMARTINDAKAN.Text
                .TextEdit11 = TextEdit11.Text
                .TextEdit12 = TextEdit12.Text
                .TextEdit13 = TimeEdit13.Time
                .TextEdit14 = TimeEdit14.Time
                .TextEdit15 = TimeEdit15.Time
                .TextEdit16 = txtLamaOperasi.Text
                .TextEdit17 = TextEdit17.Text
                .TextEdit18 = sUserID
                .MemoEdit1 = MemoEdit1.Text
                .MemoEdit2 = MemoEdit2.Text
                .MemoEdit3 = MemoEdit3.Text
                .BARCODEALAT = ""
                .MemoEdit7 = ""
                .MemoEdit8 = ""

                'Dim listdiagnosa1 As New List(Of String)
                'For i As Integer = 0 To grvLODiagnosaPre.RowCount - 2
                '    listdiagnosa1.Add(grvLODiagnosaPre.GetRowCellValue(i, colNAMADIAGNOSA_LO) & grvLODiagnosaPre.GetRowCellValue(i, colKDDIAGNOSA_LO) & " (" & grvLODiagnosaPre.GetRowCellValue(i, colKATEGORI_LO) & ")")
                'Next

                Dim listdiagnosa1 As New List(Of String)
                For i As Integer = 0 To grvLODiagnosaPre.RowCount - 2
                    listdiagnosa1.Add(grvLODiagnosaPre.GetRowCellValue(i, colNAMADIAGNOSA_LO))
                Next

                .TXTDIAGNOSAPOSTOP2 = String.Join(vbCrLf, listdiagnosa1.ToArray)

                'Dim listdiagnosa2 As New List(Of String)
                'For i As Integer = 0 To grvLODiagnosa2.RowCount - 2
                '    listdiagnosa2.Add(grvLODiagnosa2.GetRowCellValue(i, colNAMADIAGNOSA_LO2))
                'Next

                .TXTDIAGNOSAPOSTOP3 = ""

                Dim listPrsedur As New List(Of String)
                For i As Integer = 0 To grvLOProsedur.RowCount - 2
                    listPrsedur.Add(grvLOProsedur.GetRowCellValue(i, colNAMAPROSEDUR_LO))
                Next

                .PROSEDUR2 = String.Join(vbCrLf, listPrsedur.ToArray)
                .PROSEDUR3 = ""
                .MemoEdit4 = MemoEdit4.Text
                .MemoEdit5 = MemoEdit5.Text
                .MemoEdit6 = MemoEdit6.Text
                .CheckEdit1 = chkPemasanganImpan_Tidak.Checked
                .CheckEdit2 = chkPemasanganImpan_Ya.Checked
                .CheckEdit3 = False
                .CheckEdit4 = False
                .CheckEdit5 = False
                .CheckEdit6 = False
                .CheckEdit7 = CheckEdit7.Checked
                .CheckEdit8 = CheckEdit8.Checked
                .CheckEdit9 = CheckEdit9.Checked
                .CheckEdit10 = CheckEdit10.Checked
                .CheckEdit11 = CheckEdit11.Checked
                .CheckEdit12 = CheckEdit12.Checked
                .CheckEdit13 = CheckEdit13.Checked
                .CheckEdit14 = CheckEdit14.Checked
                .CheckEdit15 = CheckEdit15.Checked
                .CheckEdit16 = CheckEdit16.Checked
                .CheckEdit17 = CheckEdit17.Checked
                .CheckEdit18 = CheckEdit18.Checked
                .CheckEdit19 = CheckEdit19.Checked
                .CheckEdit20 = CheckEdit20.Checked
                .KDDOCTOR = sKDDOCTOR_DPJPUTAMA
                .BARCODEALAT = ""
                .DOKTER_KODE = ""
                .DOKTER_NAMEDISPLAY = ""

                .DOKTER_KODE = grdOperator_LO.EditValue
                .DOKTER_NAMEDISPLAY = grdOperator_LO.Text

                .SEQ = 0
                Try
                    .CETAK = oDigital_LaporanTindakan.GetData(sNoId).CETAK
                Catch ex As Exception
                    .CETAK = 0
                End Try
                .KDUSER = sUserID
                .KDUSER_SIGNATURE = ""
                .DATEDELETE = Now
                Try
                    .ISDELETE = oDigital_LaporanTindakan.GetData(sNoId).ISDELETE
                Catch ex As Exception
                    .ISDELETE = 0
                End Try
            End With

            'DIAGNOSA
            Dim arrDetailDiagnosa = oDigital_LaporanTindakan.GetStructureDetailDiagnosaTemplateList
            For i As Integer = 0 To grvLODiagnosaPre.RowCount - 2
                Dim dsDetail = oDigital_LaporanTindakan.GetStructureDetailDiagnosaTemplate
                With dsDetail
                    .SEQ = i
                    .KDJUDUL = ds.KDJUDUL
                    .KATEGORI = IIf(i = 0, "Primer", "Sekunder")
                    .NAMADIAGNOSA = IIf(String.IsNullOrEmpty(grvLODiagnosaPre.GetRowCellValue(i, colNAMADIAGNOSA_LO)), "", grvLODiagnosaPre.GetRowCellValue(i, colNAMADIAGNOSA_LO))
                    .KDDIAGNOSA = IIf(String.IsNullOrEmpty(grvLODiagnosaPre.GetRowCellValue(i, colKDDIAGNOSA_LO)), "", grvLODiagnosaPre.GetRowCellValue(i, colKDDIAGNOSA_LO))
                End With
                arrDetailDiagnosa.Add(dsDetail)
            Next

            'Dim arrDetailDiagnosa2 = oDigital_LaporanTindakan.GetStructureDetailDiagnosa2TemplateList
            'For i As Integer = 0 To grvLODiagnosa2.RowCount - 2
            '    Dim dsDetail = oDigital_LaporanTindakan.GetStructureDetailDiagnosa2Template
            '    With dsDetail
            '        .SEQ = i
            '        .KDJUDUL = ds.KDJUDUL
            '        .KATEGORI = IIf(i = 0, "Primer", "Sekunder")
            '        .NAMADIAGNOSA = IIf(String.IsNullOrEmpty(grvLODiagnosa2.GetRowCellValue(i, colNAMADIAGNOSA_LO2)), "", grvLODiagnosa2.GetRowCellValue(i, colNAMADIAGNOSA_LO2))
            '        .KDDIAGNOSA = IIf(String.IsNullOrEmpty(grvLODiagnosa2.GetRowCellValue(i, colKDDIAGNOSA_LO2)), "", grvLODiagnosa2.GetRowCellValue(i, colKDDIAGNOSA_LO2))
            '    End With
            '    arrDetailDiagnosa2.Add(dsDetail)
            'Next

            'PROSEDUR
            Dim arrDetailProsedur = oDigital_LaporanTindakan.GetStructureDetailProsedurTemplateList
            For i As Integer = 0 To grvLOProsedur.RowCount - 2
                Dim dsDetail = oDigital_LaporanTindakan.GetStructureDetailProsedurTemplate
                With dsDetail
                    .SEQ = i
                    .KDJUDUL = ds.KDJUDUL
                    .KATEGORI = ""
                    .NAMAPROSEDUR = IIf(String.IsNullOrEmpty(grvLOProsedur.GetRowCellValue(i, colNAMAPROSEDUR_LO)), "", grvLOProsedur.GetRowCellValue(i, colNAMAPROSEDUR_LO))
                    .KDPROSEDUR = IIf(String.IsNullOrEmpty(grvLOProsedur.GetRowCellValue(i, colKDPROSEDUR_LO)), "", grvLOProsedur.GetRowCellValue(i, colKDPROSEDUR_LO))
                End With
                arrDetailProsedur.Add(dsDetail)
            Next

            Dim dsCek = oDigital_LaporanTindakan.GetDataTemplate(sKODEASESMENCOPY)

            If dsCek Is Nothing Then
                Try
                    fn_SaveLaporanTindakan = oDigital_LaporanTindakan.InsertDataTemplate(ds, arrDetailDiagnosa, Nothing, arrDetailProsedur)
                Catch ex As Exception
                    MsgBox("Simpan Data Laporan Operasi: " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            Else
                Try
                    fn_SaveLaporanTindakan = oDigital_LaporanTindakan.UpdateDataTemplate(ds, arrDetailDiagnosa, Nothing, arrDetailProsedur)
                Catch ex As Exception
                    MsgBox("Simpan Data Laporan Operasi: " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            End If
        Catch oErr As Exception
            MsgBox("Simpan Data Laporan Operasi: " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            fn_SaveLaporanTindakan = False
        End Try
    End Function
#End Region
#Region "Command Button"
    Private Sub frmItem_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        'Select Case e.KeyCode
        '    Case Keys.F12
        '        btnClose_Click()
        '    Case Keys.F2
        '        If btnSaveNew.Enabled = True Then
        '            btnSaveNew_Click()
        '        End If
        '    Case Keys.F3
        '        If btnSaveClose.Enabled = True Then
        '            btnSaveClose_Click()
        '        End If
        'End Select
    End Sub
    'Private Sub btnSaveNew_Click() Handles btnSaveNew.ItemClick
    '    If fn_Validate() = False Then Exit Sub
    '    If MsgBox(Statement.SaveQuestion, MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, Me.Text) = MsgBoxResult.No Then Exit Sub
    '    If fn_Save() = False Then
    '        MsgBox(Statement.SaveFail, MsgBoxStyle.Exclamation, Me.Text)
    '    Else
    '        MsgBox(Statement.SaveSuccess, MsgBoxStyle.Information, Me.Text)
    '        sStatusSave = "NEW"
    '        Me.Close()
    '    End If
    'End Sub
    'Private Sub btnSaveClose_Click() Handles btnSaveClose.ItemClick
    '    If fn_Validate() = False Then Exit Sub
    '    If MsgBox(Statement.SaveQuestion, MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, Me.Text) = MsgBoxResult.No Then Exit Sub
    '    If fn_Save() = False Then
    '        MsgBox(Statement.SaveFail, MsgBoxStyle.Exclamation, Me.Text)
    '    Else
    '        MsgBox(Statement.SaveSuccess, MsgBoxStyle.Information, Me.Text)
    '        Me.Close()
    '    End If
    'End Sub
    'Private Sub btnClose_Click() Handles btnClose.ItemClick
    '    Me.Close()
    'End Sub
#End Region
#Region "Lookup / Event"
    Private Sub txtLamaOperasi_PreviewKeyDown(sender As Object, e As PreviewKeyDownEventArgs) Handles txtLamaOperasi.PreviewKeyDown
        If e.KeyCode = Keys.Tab Then
            MemoEdit3.Focus()
        End If
    End Sub
    Private Sub DeleteToolStripLODiagnosa_Click(sender As Object, e As EventArgs) Handles DeleteToolStripMenuItem.Click
        grvLODiagnosaPre.DeleteSelectedRows()
    End Sub
    Private Sub DeleteToolStripLODiagnosa_2_Click(sender As Object, e As EventArgs) Handles DeleteToolStripMenuItem1.Click
        'grvLODiagnosa2.DeleteSelectedRows()
    End Sub
    Private Sub DeleteToolStripLOProsedur_Click(sender As Object, e As EventArgs) Handles DeleteToolStripMenuItem2.Click
        grvLOProsedur.DeleteSelectedRows()
    End Sub
    Private Sub TimeEdit15_EditValueChanging(sender As Object, e As EventArgs) Handles TimeEdit15.EditValueChanged
        If isLoad = True Then
            ' Ambil nilai waktu dari TimeEdit
            Dim waktuAwal As DateTime = TimeEdit14.EditValue
            Dim waktuAkhir As DateTime = TimeEdit15.EditValue

            If waktuAkhir >= waktuAwal Then
                Dim selisih As TimeSpan = waktuAkhir - waktuAwal
                txtLamaOperasi.Text = selisih.Hours & " Jam " & selisih.Minutes & " Menit"
            Else
                txtLamaOperasi.Text = "00 Jam 00 Menit"
            End If
        End If
    End Sub
    Private Sub txtCariDiagnosaLO1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtCariDiagnosaLO1.KeyPress
        If Asc(e.KeyChar) = 13 Then
            If txtCariDiagnosaLO1.Text.Length < 3 Then
                MsgBox("Minimal 3 Huruf !!!", MsgBoxStyle.Exclamation, Me.Text)
                Exit Sub
            End If
            Try
                Dim jsonDecode = JObject.Parse(oBrigging.fn_Pencariandiagnosa(txtCariDiagnosaLO1.Text))
                Dim sDataDuplicate As String = String.Empty
                Dim smessage As String = String.Empty

                sDataDuplicate = jsonDecode("metadata")("code").ToString
                smessage = jsonDecode("metadata")("message").ToString

                If sDataDuplicate = "200" Then
                    Dim table As DataTable

                    table = New DataTable("M_DIAGNOSA")
                    table.Columns.Add("nama")
                    table.Columns.Add("kode")

                    For Each item In jsonDecode("response")("data")
                        table.Rows.Add(New String() {item(0), item(1)})
                    Next

                    grdCariDiganosaLO1.Properties.DataSource = table
                    grdCariDiganosaLO1.Properties.ValueMember = "kode"
                    grdCariDiganosaLO1.Properties.DisplayMember = "nama"

                    grdCariDiganosaLO1.ShowPopup()

                    txtCariDiagnosaLO1.ResetText()
                Else
                    MsgBox("Pencarian Diagnosa" & vbCrLf & sDataDuplicate & "-" & smessage, MsgBoxStyle.Information, Me.Text)
                End If
            Catch oErr As Exception
                MsgBox("Pencarian Diagnosa: " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            End Try
        End If
    End Sub
    Private Sub grdCariDiganosaLO1_EditValueChanged(sender As Object, e As EventArgs) Handles grdCariDiganosaLO1.EditValueChanged
        If isLoad = False Then Exit Sub

        If grdCariDiganosaLO1.Text <> "" Then
            Dim sCek As Integer = 0

            For i As Integer = 0 To grvLODiagnosaPre.RowCount - 2
                sCek += 1
            Next

            grvLODiagnosaPre.Focus()
            grvLODiagnosaPre.AddNewRow()
            'grvLODiagnosaPre.SetFocusedRowCellValue(colKATEGORI_LO, IIf(sCek = 0, "Primer", "Sekunder"))
            grvLODiagnosaPre.SetFocusedRowCellValue(colKDDIAGNOSA_LO, grdCariDiganosaLO1.EditValue)
            grvLODiagnosaPre.SetFocusedRowCellValue(colNAMADIAGNOSA_LO, grdCariDiganosaLO1.Text)
            grvLODiagnosaPre.UpdateCurrentRow()

            grvLODiagnosaPre.Focus()
        End If
    End Sub
    Private Sub txtCariDiagnosaLO2_KeyPress(sender As Object, e As KeyPressEventArgs)
        'If Asc(e.KeyChar) = 13 Then
        '    If txtCariDiagnosaLO2.Text.Length < 3 Then
        '        MsgBox("Minimal 3 Huruf !!!", MsgBoxStyle.Exclamation, Me.Text)
        '        Exit Sub
        '    End If
        '    Try
        '        Dim jsonDecode = JObject.Parse(oBrigging.fn_Pencariandiagnosa(txtCariDiagnosaLO2.Text))
        '        Dim sDataDuplicate As String = String.Empty
        '        Dim smessage As String = String.Empty

        '        sDataDuplicate = jsonDecode("metadata")("code").ToString
        '        smessage = jsonDecode("metadata")("message").ToString

        '        If sDataDuplicate = "200" Then
        '            Dim table As DataTable

        '            table = New DataTable("M_DIAGNOSA")
        '            table.Columns.Add("nama")
        '            table.Columns.Add("kode")

        '            For Each item In jsonDecode("response")("data")
        '                table.Rows.Add(New String() {item(0), item(1)})
        '            Next

        '            grdCariDiganosaLO2.Properties.DataSource = table
        '            grdCariDiganosaLO2.Properties.ValueMember = "kode"
        '            grdCariDiganosaLO2.Properties.DisplayMember = "nama"

        '            grdCariDiganosaLO2.ShowPopup()

        '            txtCariDiagnosaLO2.ResetText()
        '        Else
        '            MsgBox("Pencarian Diagnosa" & vbCrLf & sDataDuplicate & "-" & smessage, MsgBoxStyle.Information, Me.Text)
        '        End If
        '    Catch oErr As Exception
        '        MsgBox("Pencarian Diagnosa: " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        '    End Try
        'End If
    End Sub
    Private Sub grdCariDiganosaLO2_EditValueChanged(sender As Object, e As EventArgs)
        'If isLoad = False Then Exit Sub

        'If grdCariDiganosaLO2.Text <> "" Then
        '    Dim sCek As Integer = 0

        '    For i As Integer = 0 To grvLODiagnosa2.RowCount - 2
        '        sCek += 1
        '    Next

        '    grvLODiagnosa2.Focus()
        '    grvLODiagnosa2.AddNewRow()
        '    'grvLODiagnosa2.SetFocusedRowCellValue(colKATEGORI_LO2, IIf(sCek = 0, "Primer", "Sekunder"))
        '    grvLODiagnosa2.SetFocusedRowCellValue(colKDDIAGNOSA_LO2, grdCariDiganosaLO2.EditValue)
        '    grvLODiagnosa2.SetFocusedRowCellValue(colNAMADIAGNOSA_LO2, grdCariDiganosaLO2.Text)
        '    grvLODiagnosa2.UpdateCurrentRow()

        '    grvLODiagnosa2.Focus()
        'End If
    End Sub
    Private Sub txtCariProsedur_LO_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtCariProsedur_LO.KeyPress
        If Asc(e.KeyChar) = 13 Then
            Try
                If txtCariProsedur_LO.Text.Length < 3 Then
                    MsgBox("Minimal 3 Huruf !!!", MsgBoxStyle.Exclamation, Me.Text)
                    Exit Sub
                End If

                Try
                    Dim jsonDecode = JObject.Parse(oBrigging.fn_PencarianProsedur("VCLAIM", txtCariProsedur_LO.Text))
                    Dim sDataDuplicate As String = String.Empty
                    Dim smessage As String = String.Empty

                    sDataDuplicate = jsonDecode("metadata")("code").ToString
                    smessage = jsonDecode("metadata")("message").ToString

                    If sDataDuplicate = "200" Then
                        Dim table As DataTable

                        table = New DataTable("M_PROSEDUR")
                        table.Columns.Add("nama")
                        table.Columns.Add("kode")

                        For Each item In jsonDecode("response")("data")
                            table.Rows.Add(New String() {item(0), item(1)})
                        Next

                        grdCariProsedur_LO.Properties.DataSource = table
                        grdCariProsedur_LO.Properties.ValueMember = "kode"
                        grdCariProsedur_LO.Properties.DisplayMember = "nama"

                        grdCariProsedur_LO.ShowPopup()

                        txtCariProsedur_LO.ResetText()
                    Else
                        MsgBox("Pencarian Prosedur" & vbCrLf & sDataDuplicate & "-" & smessage, MsgBoxStyle.Information, Me.Text)
                    End If
                Catch oErr As Exception
                    MsgBox("Pencarian Prosedur: " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            Catch oErr As Exception
                MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            End Try
        End If
    End Sub
    Private Sub grdCariProsedur_LO_EditValueChanged(sender As Object, e As EventArgs) Handles grdCariProsedur_LO.EditValueChanged
        If isLoad = False Then Exit Sub

        If grdCariProsedur_LO.Text <> "" Then
            grvLOProsedur.Focus()
            grvLOProsedur.AddNewRow()
            grvLOProsedur.SetFocusedRowCellValue(colKDPROSEDUR_LO, grdCariProsedur_LO.EditValue)
            grvLOProsedur.SetFocusedRowCellValue(colNAMAPROSEDUR_LO, grdCariProsedur_LO.Text)
            grvLOProsedur.UpdateCurrentRow()

            txtCariProsedur_LO.Focus()
        End If
    End Sub
    Private Sub fn_LoadOperator()
        Try
            Dim oConn As New SqlConnection
            Dim oComm As New SqlCommand
            Dim da As SqlDataAdapter
            Dim ds As New DataSet
            Dim SQL As String
            Dim sConn As String = sConnOld
            oConn = New SqlConnection(sConn)

            If oConn.State = ConnectionState.Closed Then
                oConn.Open()
            End If

            SQL = "SELECT "
            SQL &= "A.KDDOCTOR "
            SQL &= ",NAME_DISPLAY = (SELECT CASE A.FRONT_TITLE WHEN '' THEN '' ELSE A.FRONT_TITLE + ' ' END) + A.NAME_DISPLAY + A.BACK_TITLE "
            SQL &= "FROM "
            SQL &= "M_DOCTOR A "
            SQL &= "WHERE "
            SQL &= "A.ISACTIVE = 1 "
            SQL &= "AND A.CATEGORY = 1 "
            SQL &= "ORDER BY A.NAME_DISPLAY "

            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "DOKTER")

            grdOperator_LO.Properties.DataSource = ds.Tables("DOKTER")
            grdOperator_LO.Properties.ValueMember = "KDDOCTOR"
            grdOperator_LO.Properties.DisplayMember = "NAME_DISPLAY"

            TextEdit19.Properties.DataSource = ds.Tables("DOKTER")
            TextEdit19.Properties.ValueMember = "KDDOCTOR"
            TextEdit19.Properties.DisplayMember = "NAME_DISPLAY"

            'grdAnestesi_LO.Properties.DataSource = ds.Tables("DOKTER")
            'grdAnestesi_LO.Properties.ValueMember = "KDDOCTOR"
            'grdAnestesi_LO.Properties.DisplayMember = "NAME_DISPLAY"

            'txtDokter_LO.Properties.DataSource = ds.Tables("DOKTER")
            'txtDokter_LO.Properties.ValueMember = "KDDOCTOR"
            'txtDokter_LO.Properties.DisplayMember = "NAME_DISPLAY"

            If oConn.State = ConnectionState.Open Then
                oConn.Close()
            End If
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadPERAWAT()
        Dim oTemplate As New Reference.clsUnit
        Try
            Dim ds = oTemplate.GetData.Where(Function(x) x.ISACTIVE = True).ToList()

            grdAsisten1_LO.Properties.DataSource = ds
            grdAsisten1_LO.Properties.ValueMember = "MEMO"
            grdAsisten1_LO.Properties.DisplayMember = "MEMO"

            'grdAsisten2_LO.Properties.DataSource = ds
            'grdAsisten2_LO.Properties.ValueMember = "MEMO"
            'grdAsisten2_LO.Properties.DisplayMember = "MEMO"

            grdPerawatInstrumen.Properties.DataSource = ds
            grdPerawatInstrumen.Properties.ValueMember = "MEMO"
            grdPerawatInstrumen.Properties.DisplayMember = "MEMO"

            'grdPerawatAnestesiLO.Properties.DataSource = ds
            'grdPerawatAnestesiLO.Properties.ValueMember = "MEMO"
            'grdPerawatAnestesiLO.Properties.DisplayMember = "MEMO"
        Catch oErr As Exception
            MsgBox("Load Perawat : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_JENISOPERASI()
        Try
            Dim oConn As New SqlConnection
            Dim oComm As New SqlCommand
            Dim da As SqlDataAdapter
            Dim ds As New DataSet
            Dim SQL As String
            Dim sConn As String = Decrypt(My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Software\SIMRS\SW\", "Database", "").ToString())
            oConn = New SqlConnection(sConn)

            If oConn.State = ConnectionState.Closed Then
                oConn.Open()
            End If

            SQL = "SELECT "
            SQL &= "* "
            SQL &= "FROM "
            SQL &= "M_LAPORAN_TINDAKAN A "
            SQL &= "WHERE "
            SQL &= "A.ISACTIVE = 1 "

            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "JENISLAPORAN")

            cboJenisOperasi.Properties.DataSource = ds.Tables("JENISLAPORAN")
            cboJenisOperasi.Properties.ValueMember = "NAMALAPORAN"
            cboJenisOperasi.Properties.DisplayMember = "NAMALAPORAN"

            If oConn.State = ConnectionState.Open Then
                oConn.Close()
            End If

        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
#End Region
#Region "Handlle"
    'Private Sub chKamarOperasi_Click(sender As Object, e As EventArgs) Handles CheckEdit1.Click, CheckEdit2.Click, CheckEdit3.Click, CheckEdit4.Click, CheckEdit5.Click, CheckEdit6.Click
    '    CheckEdit1.Checked = False
    '    CheckEdit2.Checked = False
    '    CheckEdit3.Checked = False
    '    CheckEdit4.Checked = False
    '    CheckEdit5.Checked = False
    '    CheckEdit6.Checked = False
    'End Sub
    Private Sub chJenisAnestesi_Click(sender As Object, e As EventArgs) Handles CheckEdit7.Click, CheckEdit8.Click, CheckEdit9.Click, CheckEdit10.Click
        CheckEdit7.Checked = False
        CheckEdit8.Checked = False
        CheckEdit9.Checked = False
        CheckEdit10.Checked = False
    End Sub
    Private Sub chKlasifikasi_Click(sender As Object, e As EventArgs) Handles CheckEdit11.Click, CheckEdit12.Click
        CheckEdit11.Checked = False
        CheckEdit12.Checked = False
    End Sub
    Private Sub chJenisOperasi_Click(sender As Object, e As EventArgs) Handles CheckEdit13.Click, CheckEdit14.Click, CheckEdit15.Click, CheckEdit16.Click
        CheckEdit13.Checked = False
        CheckEdit14.Checked = False
        CheckEdit15.Checked = False
        CheckEdit16.Checked = False
    End Sub
    Private Sub chPemeriksaanPA_Click(sender As Object, e As EventArgs) Handles CheckEdit17.Click, CheckEdit18.Click
        CheckEdit17.Checked = False
        CheckEdit18.Checked = False
    End Sub
    Private Sub chPemeriksaanCairan_Click(sender As Object, e As EventArgs) Handles CheckEdit19.Click, CheckEdit20.Click
        CheckEdit19.Checked = False
        CheckEdit20.Checked = False
    End Sub
    Private Sub chPemasanganImplan_Click(sender As Object, e As EventArgs) Handles chkPemasanganImpan_Tidak.Click, chkPemasanganImpan_Ya.Click
        chkPemasanganImpan_Tidak.Checked = False
        chkPemasanganImpan_Ya.Checked = False
    End Sub
    Private Sub btnSimpanLaporanOperasi_Click(sender As Object, e As EventArgs) Handles btnSimpanLaporanOperasi.Click
        If btnSimpanLaporanOperasi.Enabled = False Then Exit Sub

        If fn_Validate() = False Then Exit Sub
        'If MsgBox(Statement.SaveQuestion, MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, Me.Text) = MsgBoxResult.No Then Exit Sub
        If fn_Save() = False Then
            MsgBox(Statement.SaveFail, MsgBoxStyle.Exclamation, Me.Text)
        Else
            MsgBox(Statement.SaveSuccess, MsgBoxStyle.Information, Me.Text)
            If sAutoClose = True Then
                Me.Close()
            Else
                oFormMode = FORM_MODE.FORM_MODE_EDIT
            End If
        End If
    End Sub
    Private Sub btnListLaporanOperasi_Click(sender As Object, e As EventArgs)
        Me.Close()
    End Sub
    'Private Sub Form1_MouseWheel(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Me.MouseWheel
    '    If e.Delta > 0 Then
    '        Trace.WriteLine("Scrolled up!")
    '        fn_ScrollPage(True)
    '    Else
    '        Trace.WriteLine("Scrolled down!")
    '        fn_ScrollPage(False)
    '    End If
    'End Sub
    'Private Sub fn_ScrollPage(ByVal isUp As Boolean)
    '    Dim myView As Point = Me.Panel7.AutoScrollPosition

    '    Dim scrollchange As Integer = 50

    '    If isUp Then
    '        'up
    '        myView.X = -myView.X
    '        myView.Y = -scrollchange - myView.Y

    '    Else
    '        'down
    '        myView.X = -myView.X
    '        myView.Y = scrollchange - myView.Y

    '    End If

    '    Me.Panel7.AutoScrollPosition = myView

    'End Sub
    Private Sub btnLoadDataLaporanOperasi_Click(sender As Object, e As EventArgs) Handles btnLoadData.Click
        Dim frmListTemplate As New frmListTemplate
        Try
            sKODEASESMENCOPY = String.Empty

            frmListTemplate.fn_LoadKategori(5, "")
            frmListTemplate.ShowDialog(Me)

            If sKODEASESMENCOPY <> "" Then
                fn_LoadDataLaporanTindakan(sKODEASESMENCOPY)
            End If
        Catch oErr As Exception
            MsgBox("Load Data Template" & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub btnSaveLaporanOperasi_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        If sKODEASESMENCOPY = "" Then
            MsgBox("Silahkan Load Data Terlebih Dahulu", MsgBoxStyle.Exclamation, Me.Text)
            Exit Sub
        End If

        If MsgBox("Save " & sKODEASESMENCOPY & "?", MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, Me.Text) = MsgBoxResult.No Then Exit Sub
        If fn_SaveLaporanTindakan() = False Then
            MsgBox("Save gagal! Tolong kontak admin anda!", MsgBoxStyle.Exclamation, Me.Text)
        Else
            MsgBox("Save " & sKODEASESMENCOPY & " success!", MsgBoxStyle.Information, Me.Text)
        End If
    End Sub
    Private Sub btnSaveAsLaporanOperasi_Click(sender As Object, e As EventArgs) Handles btnSaveAs.Click
        sRemarks = String.Empty
        frmJudulTemplate.ShowDialog(Me)

        If sRemarks = "" Then
            MsgBox("Judul Template Masih Kosong", MsgBoxStyle.Exclamation, Me.Text)
        Else
            sKODEASESMENCOPY = sRemarks

            Dim oS_DIGITAL_OK_LAPORANOPERASITEMPLATE As New Transaksi.clsS_DIGITAL_OK_LAPORANOPERASITemplate

            Dim ds = oS_DIGITAL_OK_LAPORANOPERASITEMPLATE.GetData(sKODEASESMENCOPY)
            If ds IsNot Nothing Then
                MsgBox("Judul Sudah Ada", MsgBoxStyle.Exclamation, Me.Text)
                Exit Sub
            End If

            If MsgBox("Save As " & sKODEASESMENCOPY & "?", MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, Me.Text) = MsgBoxResult.No Then Exit Sub
            If fn_SaveLaporanTindakan() = False Then
                sKODEASESMENCOPY = ""
                MsgBox("Save As gagal! Tolong kontak admin anda!", MsgBoxStyle.Exclamation, Me.Text)
            Else
                MsgBox("Save As " & sKODEASESMENCOPY & " success!", MsgBoxStyle.Information, Me.Text)
            End If
        End If

        sRemarks = String.Empty

    End Sub
#End Region
End Class