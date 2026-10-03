Imports DataAccess
Imports UI.WIN.MAIN.My.Resources
Imports System.Linq
Imports System.Data.SqlClient

Public Class frmEMEDREKASMEDICU
#Region "Declaration"
    Private oFormMode As FORM_MODE = FORM_MODE.FORM_MODE_VIEW
    Private isLoad As Boolean = False
    Private oS_DIGITAL_RI_MEDIS_ICU As New Digital.clsS_DIGITAL_RI_MEDIS_ICU
    Private down As Boolean = False
    Private sKDDOCTOR As String
#End Region
#Region "Function"
    Public Sub LoadMe(ByVal FormMode As Integer, ByVal KDREG As String, ByVal KDCUSTOMER As String, ByVal NAMAPASIEN As String, ByVal JENISKELAMIN As String, ByVal DPJP As String, ByVal KDDPJP As String)
        oFormMode = FormMode

        txtNOREG.Text = KDREG
        txtNAMA.Text = NAMAPASIEN
        txtNOMORRM.Text = KDCUSTOMER
        sKDDOCTOR = KDDPJP
    End Sub
    Private Sub Form_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        fn_ChangeFormState()
        isLoad = True
    End Sub
    Private Sub Form_Closing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        Dispose()
        sCode = txtNOREG.Text.Trim.ToUpper
        sLoadAsesmenAwal = False
    End Sub
    Private Overloads Sub Dispose()
        MyBase.Dispose()
        Me.Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
    Private Sub fn_ChangeFormState()
        fn_LoadKDDOCTOR()

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
        btnSaveNew.Enabled = Not Status
        btnSaveClose.Enabled = Not Status

        txtWAKTU.Properties.ReadOnly = Status
        txtANAMNESIS.Properties.ReadOnly = Status
        TextEdit1.Properties.ReadOnly = Status
        TextEdit2.Properties.ReadOnly = Status
        CheckEdit1.Properties.ReadOnly = Status
        CheckEdit2.Properties.ReadOnly = Status
        TextEdit3.Properties.ReadOnly = Status
        TextEdit4.Properties.ReadOnly = Status
        TextEdit5.Properties.ReadOnly = Status
        TextEdit6.Properties.ReadOnly = Status
        TextEdit7.Properties.ReadOnly = Status
        TextEdit8.Properties.ReadOnly = Status
        TextEdit9.Properties.ReadOnly = Status
        TextEdit10.Properties.ReadOnly = Status
        TextEdit11.Properties.ReadOnly = Status
        TextEdit12.Properties.ReadOnly = Status
        TextEdit13.Properties.ReadOnly = Status
        TextEdit14.Properties.ReadOnly = Status
        TextEdit15.Properties.ReadOnly = Status
        CheckEdit3.Properties.ReadOnly = Status
        CheckEdit4.Properties.ReadOnly = Status
        CheckEdit5.Properties.ReadOnly = Status
        CheckEdit6.Properties.ReadOnly = Status
        CheckEdit7.Properties.ReadOnly = Status
        CheckEdit8.Properties.ReadOnly = Status
        TextEdit16.Properties.ReadOnly = Status
        CheckEdit9.Properties.ReadOnly = Status
        CheckEdit10.Properties.ReadOnly = Status
        TextEdit17.Properties.ReadOnly = Status
        CheckEdit11.Properties.ReadOnly = Status
        TextEdit18.Properties.ReadOnly = Status
        CheckEdit12.Properties.ReadOnly = Status
        CheckEdit13.Properties.ReadOnly = Status
        CheckEdit14.Properties.ReadOnly = Status
        TextEdit19.Properties.ReadOnly = Status
        CheckEdit15.Properties.ReadOnly = Status
        CheckEdit16.Properties.ReadOnly = Status
        TextEdit20.Properties.ReadOnly = Status
        TextEdit21.Properties.ReadOnly = Status
        TextEdit22.Properties.ReadOnly = Status
        TextEdit23.Properties.ReadOnly = Status
        TextEdit24.Properties.ReadOnly = Status
        CheckEdit17.Properties.ReadOnly = Status
        CheckEdit18.Properties.ReadOnly = Status
        CheckEdit19.Properties.ReadOnly = Status
        CheckEdit20.Properties.ReadOnly = Status
        CheckEdit21.Properties.ReadOnly = Status
        CheckEdit22.Properties.ReadOnly = Status
        CheckEdit23.Properties.ReadOnly = Status
        CheckEdit24.Properties.ReadOnly = Status
        CheckEdit25.Properties.ReadOnly = Status
        CheckEdit26.Properties.ReadOnly = Status
        CheckEdit27.Properties.ReadOnly = Status
        TextEdit25.Properties.ReadOnly = Status
        CheckEdit28.Properties.ReadOnly = Status
        CheckEdit29.Properties.ReadOnly = Status
        CheckEdit30.Properties.ReadOnly = Status
        CheckEdit31.Properties.ReadOnly = Status
        TextEdit26.Properties.ReadOnly = Status
        CheckEdit32.Properties.ReadOnly = Status
        CheckEdit33.Properties.ReadOnly = Status
        TextEdit27.Properties.ReadOnly = Status
        CheckEdit34.Properties.ReadOnly = Status
        TextEdit28.Properties.ReadOnly = Status
        CheckEdit35.Properties.ReadOnly = Status
        CheckEdit36.Properties.ReadOnly = Status
        CheckEdit37.Properties.ReadOnly = Status
        CheckEdit38.Properties.ReadOnly = Status
        CheckEdit39.Properties.ReadOnly = Status
        CheckEdit40.Properties.ReadOnly = Status
        CheckEdit41.Properties.ReadOnly = Status
        CheckEdit42.Properties.ReadOnly = Status
        CheckEdit43.Properties.ReadOnly = Status
        CheckEdit44.Properties.ReadOnly = Status
        CheckEdit45.Properties.ReadOnly = Status
        CheckEdit46.Properties.ReadOnly = Status
        CheckEdit47.Properties.ReadOnly = Status
        CheckEdit48.Properties.ReadOnly = Status
        CheckEdit49.Properties.ReadOnly = Status
        TextEdit29.Properties.ReadOnly = Status
        TextEdit30.Properties.ReadOnly = Status
        TextEdit31.Properties.ReadOnly = Status
        TextEdit32.Properties.ReadOnly = Status
        TextEdit33.Properties.ReadOnly = Status
        TextEdit34.Properties.ReadOnly = Status
        TextEdit35.Properties.ReadOnly = Status
        TextEdit36.Properties.ReadOnly = Status
        TextEdit37.Properties.ReadOnly = Status
        TextEdit38.Properties.ReadOnly = Status
        TextEdit39.Properties.ReadOnly = Status
        TextEdit40.Properties.ReadOnly = Status
        TextEdit41.Properties.ReadOnly = Status
        TextEdit42.Properties.ReadOnly = Status
        TextEdit43.Properties.ReadOnly = Status
        TextEdit44.Properties.ReadOnly = Status
        TextEdit45.Properties.ReadOnly = Status
        TextEdit46.Properties.ReadOnly = Status
        TextEdit47.Properties.ReadOnly = Status
        TextEdit48.Properties.ReadOnly = Status
        TextEdit49.Properties.ReadOnly = Status
        TextEdit50.Properties.ReadOnly = Status
        TextEdit51.Properties.ReadOnly = Status
        TextEdit52.Properties.ReadOnly = Status
        TextEdit53.Properties.ReadOnly = Status
        TextEdit54.Properties.ReadOnly = Status
        TextEdit55.Properties.ReadOnly = Status
        TextEdit56.Properties.ReadOnly = Status
        TextEdit57.Properties.ReadOnly = Status
        TextEdit58.Properties.ReadOnly = Status
        TextEdit59.Properties.ReadOnly = Status
        TextEdit60.Properties.ReadOnly = Status
        TextEdit61.Properties.ReadOnly = Status
        TextEdit62.Properties.ReadOnly = Status
        TextEdit63.Properties.ReadOnly = Status
        TextEdit64.Properties.ReadOnly = Status
        TextEdit65.Properties.ReadOnly = Status
        TextEdit66.Properties.ReadOnly = Status
        MemoEdit1.Properties.ReadOnly = Status
        MemoEdit2.Properties.ReadOnly = Status
        MemoEdit3.Properties.ReadOnly = Status
        MemoEdit4.Properties.ReadOnly = Status
        MemoEdit5.Properties.ReadOnly = Status
        MemoEdit6.Properties.ReadOnly = Status
        TextEdit67.Properties.ReadOnly = Status
        TextEdit68.Properties.ReadOnly = Status
        'TextEdit69.Properties.ReadOnly = Status
        MemoEdit7.Properties.ReadOnly = Status
        grdDokter.Properties.ReadOnly = Status
    End Sub
    Private Sub fn_EmptyMe()
        txtWAKTU.ResetText()
        txtANAMNESIS.ResetText()
        TextEdit1.ResetText()
        TextEdit2.ResetText()
        CheckEdit1.Checked = False
        CheckEdit2.Checked = False
        TextEdit3.ResetText()
        TextEdit4.ResetText()
        TextEdit5.ResetText()
        TextEdit6.ResetText()
        TextEdit7.ResetText()
        TextEdit8.ResetText()
        TextEdit9.ResetText()
        TextEdit10.ResetText()
        TextEdit11.ResetText()
        TextEdit12.ResetText()
        TextEdit13.ResetText()
        TextEdit14.ResetText()
        TextEdit15.ResetText()
        CheckEdit3.Checked = False
        CheckEdit4.Checked = False
        CheckEdit5.Checked = False
        CheckEdit6.Checked = False
        CheckEdit7.Checked = False
        CheckEdit8.Checked = False
        TextEdit16.ResetText()
        CheckEdit9.Checked = False
        CheckEdit10.Checked = False
        TextEdit17.ResetText()
        CheckEdit11.Checked = False
        TextEdit18.ResetText()
        CheckEdit12.Checked = False
        CheckEdit13.Checked = False
        CheckEdit14.Checked = False
        TextEdit19.ResetText()
        CheckEdit15.Checked = False
        CheckEdit16.Checked = False
        TextEdit20.ResetText()
        TextEdit21.ResetText()
        TextEdit22.ResetText()
        TextEdit23.ResetText()
        TextEdit24.ResetText()
        CheckEdit17.Checked = False
        CheckEdit18.Checked = False
        CheckEdit19.Checked = False
        CheckEdit20.Checked = False
        CheckEdit21.Checked = False
        CheckEdit22.Checked = False
        CheckEdit23.Checked = False
        CheckEdit24.Checked = False
        CheckEdit25.Checked = False
        CheckEdit26.Checked = False
        CheckEdit27.Checked = False
        TextEdit25.ResetText()
        CheckEdit28.Checked = False
        CheckEdit29.Checked = False
        CheckEdit30.Checked = False
        CheckEdit31.Checked = False
        TextEdit26.ResetText()
        CheckEdit32.Checked = False
        CheckEdit33.Checked = False
        TextEdit27.ResetText()
        CheckEdit34.Checked = False
        TextEdit28.ResetText()
        CheckEdit35.Checked = False
        CheckEdit36.Checked = False
        CheckEdit37.Checked = False
        CheckEdit38.Checked = False
        CheckEdit39.Checked = False
        CheckEdit40.Checked = False
        CheckEdit41.Checked = False
        CheckEdit42.Checked = False
        CheckEdit43.Checked = False
        CheckEdit44.Checked = False
        CheckEdit45.Checked = False
        CheckEdit46.Checked = False
        CheckEdit47.Checked = False
        CheckEdit48.Checked = False
        CheckEdit49.Checked = False
        TextEdit29.ResetText()
        TextEdit30.ResetText()
        TextEdit31.ResetText()
        TextEdit32.ResetText()
        TextEdit33.ResetText()
        TextEdit34.ResetText()
        TextEdit35.ResetText()
        TextEdit36.ResetText()
        TextEdit37.ResetText()
        TextEdit38.ResetText()
        TextEdit39.ResetText()
        TextEdit40.ResetText()
        TextEdit41.ResetText()
        TextEdit42.ResetText()
        TextEdit43.ResetText()
        TextEdit44.ResetText()
        TextEdit45.ResetText()
        TextEdit46.ResetText()
        TextEdit47.ResetText()
        TextEdit48.ResetText()
        TextEdit49.ResetText()
        TextEdit50.ResetText()
        TextEdit51.ResetText()
        TextEdit52.ResetText()
        TextEdit53.ResetText()
        TextEdit54.ResetText()
        TextEdit55.ResetText()
        TextEdit56.ResetText()
        TextEdit57.ResetText()
        TextEdit58.ResetText()
        TextEdit59.ResetText()
        TextEdit60.ResetText()
        TextEdit61.ResetText()
        TextEdit62.ResetText()
        TextEdit63.ResetText()
        TextEdit64.ResetText()
        TextEdit65.ResetText()
        TextEdit65.ResetText()
        MemoEdit1.ResetText()
        MemoEdit2.ResetText()
        MemoEdit3.ResetText()
        MemoEdit4.ResetText()
        MemoEdit5.ResetText()
        MemoEdit6.ResetText()
        TextEdit67.ResetText()
        TextEdit68.ResetText()
        'TextEdit69.ResetText()
        MemoEdit7.ResetText()
        txtWAKTU.Text = Now.ToString("dd-MM-yyyy HH:mm")
        'deDATE.DateTime = Now
        grdDokter.Text = sKDDOCTOR
    End Sub
    Private Sub fn_LoadData()
        Try
            ' ***** HEADER *****
            Dim ds = oS_DIGITAL_RI_MEDIS_ICU.GetData(txtNOREG.Text)

            With ds
                'txtUMUM.Text = .UMUM
                txtWAKTU.Text = .WAKTU_AWALASESMEN
                txtANAMNESIS.Text = .ANAMNESIS
                TextEdit1.Text = .KELUHANUTAMA
                TextEdit2.Text = .RIWAYATPENYAKIT
                CheckEdit1.Checked = .PERNAHDIRAWAT_1
                CheckEdit2.Checked = .PERNAHDIRAWAT_2
                TextEdit3.Text = .PERNAHDIRAWAT_KAPAN
                TextEdit4.Text = .PERNAHDIRAWAT_DIMANA
                TextEdit5.Text = .PERNAHDIRAWAT_DIAGNOSIS
                TextEdit6.Text = .KEADAANUMUM
                TextEdit7.Text = .KESADARAN
                TextEdit8.Text = .KESANSAKIT
                TextEdit9.Text = .BB
                TextEdit10.Text = .TB
                TextEdit11.Text = .GIZI
                TextEdit12.Text = .NADI
                TextEdit13.Text = .TENAL
                TextEdit14.Text = .SUHU
                TextEdit15.Text = .RESPIRASI
                CheckEdit3.Checked = .STATUSPSIKOLOGI_01
                CheckEdit4.Checked = .STATUSPSIKOLOGI_02
                CheckEdit5.Checked = .STATUSPSIKOLOGI_03
                CheckEdit6.Checked = .STATUSPSIKOLOGI_04
                CheckEdit7.Checked = .STATUSPSIKOLOGI_05
                CheckEdit8.Checked = .STATUSPSIKOLOGI_06
                TextEdit16.Text = .STATUSPSIKOLOGI_KET
                CheckEdit9.Checked = .STATUSMENTAL_01
                CheckEdit10.Checked = .STATUSMENTAL_02
                TextEdit17.Text = .STATUSMENTAL_02_KET
                CheckEdit11.Checked = .STATUSMENTAL_03
                TextEdit18.Text = .STATUSMENTAL_03_KET
                TextEdit70.Text = .SOSIAL_01
                CheckEdit12.Checked = .SOSIAL_01_1
                CheckEdit13.Checked = .SOSIAL_01_2
                CheckEdit14.Checked = .SOSIAL_01_3
                TextEdit19.Text = .SOSIAL_01_KET
                CheckEdit15.Checked = .SOSIAL_02
                CheckEdit16.Checked = .SOSIAL_03
                TextEdit20.Text = .SOSIAL_NAMA
                TextEdit21.Text = .SOSIAL_HUBUNGAN
                TextEdit22.Text = .SOSIAL_TELEPON
                TextEdit23.Text = .STATUSSPIRITUAL_01_KET
                TextEdit24.Text = .STATUSSPIRITUAL_02_KET
                CheckEdit17.Checked = .SKRININGGIZI_01
                CheckEdit18.Checked = .SKRININGGIZI_02
                CheckEdit19.Checked = .SKRININGGIZI_03
                CheckEdit20.Checked = .SKRININGGIZI_04
                CheckEdit21.Checked = .SKRININGGIZI_05
                CheckEdit22.Checked = .SKRININGGIZI_06
                CheckEdit23.Checked = .RESIKOCEDERA_01
                CheckEdit24.Checked = .RESIKOCEDERA_02
                CheckEdit25.Checked = .RESIKOCEDERA_03
                CheckEdit26.Checked = .KEBUTUHANPRIVASI_01
                CheckEdit27.Checked = .KEBUTUHANPRIVASI_02
                TextEdit25.Text = .KEBUTUHANPRIVASI_02_KET
                CheckEdit28.Checked = .KEBUTUHANPRIVASI_03
                CheckEdit29.Checked = .KEBUTUHANPRIVASI_04
                CheckEdit30.Checked = .KEBUTUHANPRIVASI_05
                CheckEdit31.Checked = .KEBUTUHANPRIVASI_06
                TextEdit26.Text = .KEBUTUHANPRIVASI_06_KET
                CheckEdit32.Checked = .STATUSFUNGSIONAL_01
                CheckEdit33.Checked = .STATUSFUNGSIONAL_02
                TextEdit27.Text = .STATUSFUNGSIONAL_02_KET
                CheckEdit34.Checked = .STATUSFUNGSIONAL_03
                TextEdit28.Text = .STATUSFUNGSIONAL_03_KET
                CheckEdit35.Checked = .EVALUASINYERI_01
                CheckEdit36.Checked = .EVALUASINYERI_02
                CheckEdit37.Checked = .EVALUASINYERI_03
                CheckEdit38.Checked = .EVALUASINYERI_04
                CheckEdit39.Checked = .EVALUASINYERI_05
                CheckEdit40.Checked = .EVALUASINYERI_06
                CheckEdit41.Checked = .EVALUASINYERI_07
                CheckEdit42.Checked = .EVALUASINYERI_08
                CheckEdit43.Checked = .EVALUASINYERI_09
                CheckEdit44.Checked = .EVALUASINYERI_10
                CheckEdit45.Checked = .EVALUASINYERI_11
                CheckEdit46.Checked = .EVALUASINYERI_12
                CheckEdit47.Checked = .EVALUASINYERI_13
                CheckEdit48.Checked = .EVALUASINYERI_14
                CheckEdit49.Checked = .EVALUASINYERI_15
                TextEdit29.Text = .EVALUASINYERI_TOTAL
                TextEdit30.Text = .STATUSLOKALISATA_1_01
                TextEdit31.Text = .STATUSLOKALISATA_1_02
                TextEdit32.Text = .STATUSLOKALISATA_1_03
                TextEdit33.Text = .STATUSLOKALISATA_2_01
                TextEdit34.Text = .STATUSLOKALISATA_2_02
                TextEdit35.Text = .STATUSLOKALISATA_3_01
                TextEdit36.Text = .STATUSLOKALISATA_3_02
                TextEdit37.Text = .STATUSLOKALISATA_3_03
                TextEdit38.Text = .STATUSLOKALISATA_3_04
                TextEdit39.Text = .STATUSLOKALISATA_4A_01
                TextEdit40.Text = .STATUSLOKALISATA_4A_02
                TextEdit41.Text = .STATUSLOKALISATA_4A_03
                TextEdit42.Text = .STATUSLOKALISATA_4A_04
                TextEdit43.Text = .STATUSLOKALISATA_4B_01
                TextEdit44.Text = .STATUSLOKALISATA_4B_02
                TextEdit45.Text = .STATUSLOKALISATA_4B_03
                TextEdit46.Text = .STATUSLOKALISATA_4B_04
                TextEdit47.Text = .STATUSLOKALISATA_4B_05
                TextEdit48.Text = .STATUSLOKALISATA_4B_06
                TextEdit49.Text = .STATUSLOKALISATA_4B_07
                TextEdit50.Text = .STATUSLOKALISATA_4C_01
                TextEdit51.Text = .STATUSLOKALISATA_4C_02
                TextEdit52.Text = .STATUSLOKALISATA_4C_03
                TextEdit53.Text = .STATUSLOKALISATA_4C_04
                TextEdit54.Text = .STATUSLOKALISATA_4C_05
                TextEdit55.Text = .STATUSLOKALISATA_4C_06
                TextEdit56.Text = .STATUSLOKALISATA_4C_07
                TextEdit57.Text = .STATUSLOKALISATA_4D_01
                TextEdit58.Text = .STATUSLOKALISATA_4D_02
                TextEdit59.Text = .STATUSLOKALISATA_4D_03
                TextEdit60.Text = .STATUSLOKALISATA_4D_04
                TextEdit61.Text = .STATUSLOKALISATA_5_01
                TextEdit62.Text = .STATUSLOKALISATA_5_02
                TextEdit63.Text = .STATUSLOKALISATA_5_03
                TextEdit64.Text = .STATUSLOKALISATA_6_01
                TextEdit65.Text = .STATUSLOKALISATA_7_01
                TextEdit66.Text = .STATUSLOKALISATA_8_01
                MemoEdit1.Text = .PEMERIKSAANPENUNJANG
                MemoEdit2.Text = .DIFERENSIALDIAGNOSA
                MemoEdit3.Text = .DIAGNOSAKERJA
                MemoEdit4.Text = .PENGOBATANDANTINDAKAN
                MemoEdit5.Text = .REKONSILIASIOBAT
                MemoEdit6.Text = .DISCHARGEPALNNING
                TextEdit67.Text = .WAKTU_AKHIRASESMEN
                TextEdit68.Text = .TANGGAL
                'TextEdit69.Text = .DOKTER_NAMEDISPLAY
                grdDokter.Text = .DOKTER_KODE
                MemoEdit7.Text = .RIWAYATPENYAKITTERDAHULU

                'deDATE.DateTime = .DATE

                'fn_LoadAsessmenRawatJalan(txtNOREG.Text)

            End With
        Catch oErr As Exception
            MsgBox("Load List Data : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadAsessmenRawatJalan(ByVal Parameter As String)
        'Dim oAsessmenRawatJalan As New Digital.clsDigital_RJ_08
        'Dim dsAsessmenRawatJalan = oAsessmenRawatJalan.GetData(Parameter)

        'Dim oAsessmenRawatJalan2 As New Digital.clsS_DIGITAL_ASKEP_RAWATJALAN
        'Dim dsAsessmenRawatJalan2 = oAsessmenRawatJalan2.GetDatabyKDKUNJUNGAN(Parameter)

        'If dsAsessmenRawatJalan IsNot Nothing Then
        '    'txtSOAP_O.Text = "Tensi : " & dsAsessmenRawatJalan.TANDA_VITAL_01 & " mmHg" & vbCrLf & vbCrLf & "Nadi : " & dsAsessmenRawatJalan.TANDA_VITAL_02 & " x/m" & vbCrLf & vbCrLf & "Respirasi : " & dsAsessmenRawatJalan.TANDA_VITAL_04 & " x/m" & vbCrLf & vbCrLf & "Suhu : " & dsAsessmenRawatJalan.TANDA_VITAL_03 & " °C" & vbCrLf & vbCrLf & "Berat Badan : " & dsAsessmenRawatJalan.TANDA_VITAL_05 & " Kg" & vbCrLf & vbCrLf & "Tinggi Badan : " & dsAsessmenRawatJalan.TANDA_VITAL_06 & " Cm"

        '    'keluhanutama
        '    TextEdit1.Text = dsAsessmenRawatJalan.KELUHAN_UTAMA   
        '    'keadaan umum
        '    TextEdit6.Text = dsAsessmenRawatJalan.ALERGI_02_02_TEXT
        '    'kesadaran
        '    TextEdit7.Text = dsAsessmenRawatJalan.KESADARAN_UMUM
        '    'bb
        '    TextEdit9.Text = dsAsessmenRawatJalan.TANDA_VITAL_05 
        '    'tb
        '    TextEdit10.Text = dsAsessmenRawatJalan.TANDA_VITAL_06  
        '    'gizi
        '    TextEdit11.Text = dsAsessmenRawatJalan.TANDA_VITAL_06  
        '    'nadi
        '    TextEdit12.Text = dsAsessmenRawatJalan.TANDA_VITAL_02 
        '    'td
        '    TextEdit13.Text = dsAsessmenRawatJalan.TANDA_VITAL_01
        '    'suhu
        '    TextEdit14.Text = dsAsessmenRawatJalan.TANDA_VITAL_03  
        '    'respirasi
        '    TextEdit15.Text = dsAsessmenRawatJalan.TANDA_VITAL_04  
        'ElseIf dsAsessmenRawatJalan2 IsNot Nothing Then
        '    'keluhanutama
        '    TextEdit1.Text = dsAsessmenRawatJalan2.KELUHAN_UTAMA   
        '    'keadaan umum
        '    TextEdit6.Text = dsAsessmenRawatJalan2.KEADAAN
        '    'kesadaran
        '    TextEdit7.Text = dsAsessmenRawatJalan2.KESADARAN_UMUM
        '    'bb
        '    TextEdit9.Text = dsAsessmenRawatJalan2.TANDA_VITAL_BB 
        '    'tb
        '    TextEdit10.Text = dsAsessmenRawatJalan2.TANDA_VITAL_TB  
        '    'gizi
        '    'TextEdit11.Text = dsAsessmenRawatJalan2.gizi  
        '    'nadi
        '    TextEdit12.Text = dsAsessmenRawatJalan2.TANDA_VITAL_NADI 
        '    'td
        '    TextEdit13.Text = dsAsessmenRawatJalan2.TANDA_VITAL_TD
        '    'suhu
        '    TextEdit14.Text = dsAsessmenRawatJalan2.TANDA_VITAL_SUHU  
        '    'respirasi
        '    TextEdit15.Text = dsAsessmenRawatJalan2.TANDA_VITAL_R  
        'Else
        '    Exit Sub
        'End If
    End Sub
    Private Function ByteArrayToImage(ByVal byteArrayIn() As Byte) As Image
        Using ms As New System.IO.MemoryStream(byteArrayIn)
            Dim returnImage = Image.FromStream(ms)
            Return returnImage
        End Using
    End Function
    Private Function fn_Validate() As Boolean
        Try
            fn_Validate = True
            If txtNOREG.Text = String.Empty Then
                MsgBox("Dibutuhkan Register", MsgBoxStyle.Exclamation, Me.Text)
                txtNOREG.Focus()
                fn_Validate = False
                Exit Function
            End If
        Catch oErr As Exception
            MsgBox("Validate Data : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Function
    Private Function fn_Save() As Boolean
        Try
            ' ***** HEADER *****
            Dim ds = oS_DIGITAL_RI_MEDIS_ICU.GetStructureHeader
            With ds
                .KDPENDAFTARAN = txtNOREG.Text
                .KDCUSTOMER = txtNOMORRM.Text
                Try
                    .DATECREATED = oS_DIGITAL_RI_MEDIS_ICU.GetData(txtNOREG.Text).DATECREATED
                Catch ex As Exception
                    .DATECREATED = Now
                End Try
                .DATEUPDATED = Now
                .DATE = Now
                .UMUM = ""
                .WAKTU_AWALASESMEN = txtWAKTU.Text
                .ANAMNESIS = txtANAMNESIS.Text
                .KELUHANUTAMA = TextEdit1.Text
                .RIWAYATPENYAKIT = TextEdit2.Text
                .PERNAHDIRAWAT_1 = CheckEdit1.Checked
                .PERNAHDIRAWAT_2 = CheckEdit2.Checked
                .PERNAHDIRAWAT_KAPAN = TextEdit3.Text
                .PERNAHDIRAWAT_DIMANA = TextEdit4.Text
                .PERNAHDIRAWAT_DIAGNOSIS = TextEdit5.Text
                .KEADAANUMUM = TextEdit6.Text
                .KESADARAN = TextEdit7.Text
                .KESANSAKIT = TextEdit8.Text
                .BB = TextEdit9.Text
                .TB = TextEdit10.Text
                .GIZI = TextEdit11.Text
                .NADI = TextEdit12.Text
                .TENAL = TextEdit13.Text
                .SUHU = TextEdit14.Text
                .RESPIRASI = TextEdit15.Text
                .STATUSPSIKOLOGI_01 = CheckEdit3.Checked
                .STATUSPSIKOLOGI_02 = CheckEdit4.Checked
                .STATUSPSIKOLOGI_03 = CheckEdit5.Checked
                .STATUSPSIKOLOGI_04 = CheckEdit6.Checked
                .STATUSPSIKOLOGI_05 = CheckEdit7.Checked
                .STATUSPSIKOLOGI_06 = CheckEdit8.Checked
                .STATUSPSIKOLOGI_KET = TextEdit16.Text
                .STATUSMENTAL_01 = CheckEdit9.Checked
                .STATUSMENTAL_02 = CheckEdit10.Checked
                .STATUSMENTAL_02_KET = TextEdit17.Text
                .STATUSMENTAL_03 = CheckEdit11.Checked
                .STATUSMENTAL_03_KET = TextEdit18.Text
                .SOSIAL_01 = TextEdit70.Text
                .SOSIAL_01_1 = CheckEdit12.Checked
                .SOSIAL_01_2 = CheckEdit13.Checked
                .SOSIAL_01_3 = CheckEdit14.Checked
                .SOSIAL_01_KET = TextEdit19.Text
                .SOSIAL_02 = CheckEdit15.Checked
                .SOSIAL_03 = CheckEdit16.Checked
                .SOSIAL_NAMA = TextEdit20.Text
                .SOSIAL_HUBUNGAN = TextEdit21.Text
                .SOSIAL_TELEPON = TextEdit22.Text
                .STATUSSPIRITUAL_01_KET = TextEdit23.Text
                .STATUSSPIRITUAL_02_KET = TextEdit24.Text
                .SKRININGGIZI_01 = CheckEdit17.Checked
                .SKRININGGIZI_02 = CheckEdit18.Checked
                .SKRININGGIZI_03 = CheckEdit19.Checked
                .SKRININGGIZI_04 = CheckEdit20.Checked
                .SKRININGGIZI_05 = CheckEdit21.Checked
                .SKRININGGIZI_06 = CheckEdit22.Checked
                .RESIKOCEDERA_01 = CheckEdit23.Checked
                .RESIKOCEDERA_02 = CheckEdit24.Checked
                .RESIKOCEDERA_03 = CheckEdit25.Checked
                .KEBUTUHANPRIVASI_01 = CheckEdit26.Checked
                .KEBUTUHANPRIVASI_02 = CheckEdit27.Checked
                .KEBUTUHANPRIVASI_02_KET = TextEdit25.Text
                .KEBUTUHANPRIVASI_03 = CheckEdit28.Checked
                .KEBUTUHANPRIVASI_04 = CheckEdit29.Checked
                .KEBUTUHANPRIVASI_05 = CheckEdit30.Checked
                .KEBUTUHANPRIVASI_06 = CheckEdit31.Checked
                .KEBUTUHANPRIVASI_06_KET = TextEdit26.Text
                .STATUSFUNGSIONAL_01 = CheckEdit32.Checked
                .STATUSFUNGSIONAL_02 = CheckEdit33.Checked
                .STATUSFUNGSIONAL_02_KET = TextEdit27.Text
                .STATUSFUNGSIONAL_03 = CheckEdit34.Checked
                .STATUSFUNGSIONAL_03_KET = TextEdit28.Text
                .EVALUASINYERI_01 = CheckEdit35.Checked
                .EVALUASINYERI_02 = CheckEdit36.Checked
                .EVALUASINYERI_03 = CheckEdit37.Checked
                .EVALUASINYERI_04 = CheckEdit38.Checked
                .EVALUASINYERI_05 = CheckEdit39.Checked
                .EVALUASINYERI_06 = CheckEdit40.Checked
                .EVALUASINYERI_07 = CheckEdit41.Checked
                .EVALUASINYERI_08 = CheckEdit42.Checked
                .EVALUASINYERI_09 = CheckEdit43.Checked
                .EVALUASINYERI_10 = CheckEdit44.Checked
                .EVALUASINYERI_11 = CheckEdit45.Checked
                .EVALUASINYERI_12 = CheckEdit46.Checked
                .EVALUASINYERI_13 = CheckEdit47.Checked
                .EVALUASINYERI_14 = CheckEdit48.Checked
                .EVALUASINYERI_15 = CheckEdit49.Checked
                .EVALUASINYERI_TOTAL = TextEdit29.Text
                .STATUSLOKALISATA_1_01 = TextEdit30.Text
                .STATUSLOKALISATA_1_02 = TextEdit31.Text
                .STATUSLOKALISATA_1_03 = TextEdit32.Text
                .STATUSLOKALISATA_2_01 = TextEdit33.Text
                .STATUSLOKALISATA_2_02 = TextEdit34.Text
                .STATUSLOKALISATA_3_01 = TextEdit35.Text
                .STATUSLOKALISATA_3_02 = TextEdit36.Text
                .STATUSLOKALISATA_3_03 = TextEdit37.Text
                .STATUSLOKALISATA_3_04 = TextEdit38.Text
                .STATUSLOKALISATA_4A_01 = TextEdit39.Text
                .STATUSLOKALISATA_4A_02 = TextEdit40.Text
                .STATUSLOKALISATA_4A_03 = TextEdit41.Text
                .STATUSLOKALISATA_4A_04 = TextEdit42.Text
                .STATUSLOKALISATA_4B_01 = TextEdit43.Text
                .STATUSLOKALISATA_4B_02 = TextEdit44.Text
                .STATUSLOKALISATA_4B_03 = TextEdit45.Text
                .STATUSLOKALISATA_4B_04 = TextEdit46.Text
                .STATUSLOKALISATA_4B_05 = TextEdit47.Text
                .STATUSLOKALISATA_4B_06 = TextEdit48.Text
                .STATUSLOKALISATA_4B_07 = TextEdit49.Text
                .STATUSLOKALISATA_4C_01 = TextEdit50.Text
                .STATUSLOKALISATA_4C_02 = TextEdit51.Text
                .STATUSLOKALISATA_4C_03 = TextEdit52.Text
                .STATUSLOKALISATA_4C_04 = TextEdit53.Text
                .STATUSLOKALISATA_4C_05 = TextEdit54.Text
                .STATUSLOKALISATA_4C_06 = TextEdit55.Text
                .STATUSLOKALISATA_4C_07 = TextEdit56.Text
                .STATUSLOKALISATA_4D_01 = TextEdit57.Text
                .STATUSLOKALISATA_4D_02 = TextEdit58.Text
                .STATUSLOKALISATA_4D_03 = TextEdit59.Text
                .STATUSLOKALISATA_4D_04 = TextEdit60.Text
                .STATUSLOKALISATA_5_01 = TextEdit61.Text
                .STATUSLOKALISATA_5_02 = TextEdit62.Text
                .STATUSLOKALISATA_5_03 = TextEdit63.Text
                .STATUSLOKALISATA_6_01 = TextEdit64.Text
                .STATUSLOKALISATA_7_01 = TextEdit65.Text
                .STATUSLOKALISATA_8_01 = TextEdit66.Text
                .PEMERIKSAANPENUNJANG = MemoEdit1.Text
                .DIFERENSIALDIAGNOSA = MemoEdit2.Text
                .DIAGNOSAKERJA = MemoEdit3.Text
                .PENGOBATANDANTINDAKAN = MemoEdit4.Text
                .REKONSILIASIOBAT = MemoEdit5.Text
                .DISCHARGEPALNNING = MemoEdit6.Text
                .WAKTU_AKHIRASESMEN = TextEdit67.Text
                .TANGGAL = TextEdit68.Text
                .RIWAYATPENYAKITTERDAHULU = MemoEdit7.Text
                .DOKTER_KODE = grdDokter.EditValue
                .DOKTER_NAMEDISPLAY = grdDokter.Text
                Try
                    .CETAK = oS_DIGITAL_RI_MEDIS_ICU.GetData(txtNOREG.Text).CETAK
                Catch ex As Exception
                    .CETAK = 0
                End Try
                .KDUSER = sUserID
                .KDUSER_SIGNATURE = ""
            End With

            If oFormMode = FORM_MODE.FORM_MODE_ADD Then
                Try
                    fn_Save = oS_DIGITAL_RI_MEDIS_ICU.InsertData(ds)
                Catch ex As Exception
                    MsgBox("Simpan Data : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            ElseIf oFormMode = FORM_MODE.FORM_MODE_EDIT Then
                Try
                    fn_Save = oS_DIGITAL_RI_MEDIS_ICU.UpdateData(ds)
                Catch ex As Exception
                    MsgBox("Simpan Data : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            End If
        Catch oErr As Exception
            MsgBox("Simpan Data : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            fn_Save = False
        End Try
    End Function
#End Region
#Region "Command Button"
    Private Sub frmItem_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        Select Case e.KeyCode
            Case Keys.F12
                btnClose_Click()
            Case Keys.F3
                If btnSaveClose.Enabled = True Then
                    btnSaveClose_Click()
                End If
            'Case Keys.F5
            '    If btnReload.Enabled = True Then
            '        btnReload_Click()
            '    End If
            'Case Keys.F6
            '    If btnDiagnosa.Enabled = True Then
            '        btnDiagnosa_Click()
            '    End If
            Case Keys.PageUp
	            fn_ScrollPage(True)
            Case Keys.PageDown
	            fn_ScrollPage(False)
        End Select
    End Sub
    'Private Sub btnDiagnosa_Click() Handles btnDiagnosa.ItemClick
    '    Dim oMasterDiagnosa As New Diagnosa.clsMasterDiagnosa
    '    Dim dsMasterDiagnosa = oMasterDiagnosa.GetData(txtNOREG.Text)
    '    If dsMasterDiagnosa IsNot Nothing Then
    '        Dim frmMasterDiagnosa As New frmMasterDiagnosa
    '        frmMasterDiagnosa.LoadMe(FORM_MODE.FORM_MODE_EDIT, txtNOREG.Text)
    '        frmMasterDiagnosa.ShowDialog(Me)
    '    Else
    '        Dim frmMasterDiagnosa As New frmMasterDiagnosa
    '        frmMasterDiagnosa.LoadMe(FORM_MODE.FORM_MODE_ADD, txtNOREG.Text)
    '        frmMasterDiagnosa.ShowDialog(Me)
    '    End If

    '    If sCode = "Berhasil" Then
    '        Dim listDiagnosa As New List(Of String)
    '        For Each xloop In oMasterDiagnosa.GetDataDetail(txtNOREG.Text)
    '            listDiagnosa.Add(xloop.KATEGORI & " : " & xloop.REMARKS)
    '        Next

    '        Dim listProsedur As New List(Of String)
    '        For Each xloop In oMasterDiagnosa.GetDataDetail_(txtNOREG.Text)
    '            listProsedur.Add(xloop.SEQ + 1 & ". " & xloop.REMARKS)
    '        Next
    '        MemoEdit2.Text = String.Join(vbCrLf, listProsedur.ToArray)
    '        MemoEdit3.Text = String.Join(vbCrLf, listDiagnosa.ToArray)
    '    End If
    'End Sub
    'Private Sub btnReload_Click() Handles btnReload.ItemClick
    '    Dim dsKunjungan = oS_DIGITAL_RI_MEDIS_ICU.GetDataByKunjungan(txtNOREG.Text)

    '    Dim listPenunjang As New List(Of String)
    '    Dim listTindakanPengobatan As New List(Of String)

    '    If dsKunjungan IsNot Nothing Then
    '        Dim oOrderTindakan As New Inventory.clsOrderTindakan
    '        Dim oOrderLab As New Inventory.clsOrderLab
    '        Dim oOrderRad As New Inventory.clsOrderRad
    '        Dim oKonsul As New Digital.clsKonsul
    '        Dim oKonsulJawab As New Digital.clsJawabKonsul

    '        Dim dsTindakan = From x In oOrderTindakan.GetDataPendaftaranList(dsKunjungan.KDPENDAFTARAN)
    '                         Join y In oOrderTindakan.GetDataDetail()
    '                         On x.KDORDERTINDAKAN Equals y.KDORDERTINDAKAN
    '                         Select TANGGAL = x.DATE, ITEM = "Tindakan : " & y.TARIFKT & " " & y.REMARKS & ", "

    '        Dim dsLab = From x In oOrderLab.GetDataPendaftaranList(dsKunjungan.KDPENDAFTARAN)
    '                    Join y In oOrderLab.GetDataDetail()
    '                    On x.KDORDERLAB Equals y.KDORDERLAB
    '                    Select TANGGAL = x.DATE, ITEM = "Lab : " & y.TARIFKT & " " & y.REMARKS & ", "

    '        Dim dsRad = From x In oOrderRad.GetDataPendaftaranList(dsKunjungan.KDPENDAFTARAN)
    '                    Join y In oOrderRad.GetDataDetail()
    '                    On x.KDORDERRAD Equals y.KDORDERRAD
    '                    Select TANGGAL = x.DATE, ITEM = "Rad : " & y.TARIFKT & " " & y.REMARKS & ", "

    '        Dim dsKonsul = From x In oKonsul.GetDataPendaftaranList(dsKunjungan.KDPENDAFTARAN)
    '                       Select TANGGAL = x.DATE, ITEM = "Konsul : " & "Dari " & x.DOKTER_DARI & " Kepada " & x.DOKTER_KEPADA & " Isi " & x.MEMO & ", "

    '        Dim dsJawabKonsul = From x In oKonsulJawab.GetDataPendaftaranList(dsKunjungan.KDPENDAFTARAN)
    '                            Select TANGGAL = x.DATE, ITEM = "Jawab Konsul : " & "Dari " & x.DOKTER_DARI & " Kepada " & x.DOKTER_KEPADA & " Isi " & x.MEMO & ", "

    '        Dim dsUnionPenunjang = dsLab.Union(dsRad)

    '        For Each xloop In dsUnionPenunjang.OrderBy(Function(x) x.TANGGAL)
    '            listPenunjang.Add("*) " & xloop.TANGGAL.ToString("dd-MM-yyy HH:mm:ss") & " " & xloop.ITEM)
    '        Next

    '        Dim dsUnionTindakanKonsul = dsTindakan.Union(dsKonsul).Union(dsJawabKonsul)

    '        For Each xloop In dsUnionTindakanKonsul.OrderBy(Function(x) x.TANGGAL)
    '            listTindakanPengobatan.Add("*) " & xloop.TANGGAL.ToString("dd-MM-yyy HH:mm:ss") & " " & xloop.ITEM)
    '        Next

    '        Dim oResep As New Inventory.clsOrderResep

    '        Dim dsResep = oResep.GetDataPendaftaranList(dsKunjungan.KDPENDAFTARAN)

    '        For Each xloop In dsResep
    '            For Each yloop In oResep.GetDataDetail(xloop.KDORDERRESEP)
    '                listTindakanPengobatan.Add(yloop.TARIFKT & " " & yloop.SIGNA)
    '            Next
    '        Next

    '        MemoEdit1.Text = String.Join(", ", listPenunjang.ToArray)
    '        MemoEdit4.Text = String.Join(", ", listTindakanPengobatan.ToArray)
    '    End If
    'End Sub
    Private Sub btnSaveClose_Click() Handles btnSaveClose.ItemClick
        If fn_Validate() = False Then Exit Sub
        If MsgBox("Save " & txtNOREG.Text.Trim.ToUpper & "?", MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, Me.Text) = MsgBoxResult.No Then Exit Sub
        If fn_Save() = False Then
            MsgBox("Save gagal! Tolong kontak admin anda!", MsgBoxStyle.Exclamation, Me.Text)
        Else
            MsgBox("Save " & txtNOREG.Text.Trim.ToUpper & " success!", MsgBoxStyle.Information, Me.Text)
            Me.Close()
        End If
    End Sub
    Private Sub btnClose_Click() Handles btnClose.ItemClick
        Me.Close()
    End Sub
#End Region
#Region "Lookup / Event"
    Private Sub SimpleButton1_Click(sender As Object, e As EventArgs) 
        TextEdit14.Text = "dalam batas normal"
        TextEdit19.Text = "dalam batas normal"
        TextEdit18.Text = "dalam batas normal"
        TextEdit17.Text = "dalam batas normal"
        TextEdit16.Text = "dalam batas normal"
        TextEdit15.Text = "dalam batas normal"
        TextEdit20.Text = "dalam batas normal"
        TextEdit21.Text = "dalam batas normal"
        TextEdit22.Text = "dalam batas normal"
    End Sub

    Private Sub frmEMEDREKASMEDICU_MouseWheel(sender As Object, e As MouseEventArgs) Handles MyBase.MouseWheel
	    If e.Delta > 0 Then
		    'up
		    fn_ScrollPage(True)
	    Else
		    'down
		    fn_ScrollPage(False)
	    End If
    End Sub

    Private Sub fn_ScrollPage(ByVal isUp As Boolean)
	    Dim myView As Point = Me.Panel3.AutoScrollPosition
	    Dim scrollchange As Integer = 50

	    If isUp Then
		    'up
		    myView.X = -myView.X
		    myView.y = -scrollchange - myView.Y
	    Else
		    'down
		    myView.X = -myView.X
		    myView.y = scrollchange - myView.Y
	    End If

	    Me.Panel3.AutoScrollPosition = myView
    End Sub
    Private Sub fn_LoadKDDOCTOR()
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
            SQL &= "* "
            SQL &= "FROM "
            SQL &= "M_DOCTOR A "
            SQL &= "WHERE "
            SQL &= "A.ISACTIVE = 1 "

            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "DOKTER")

            grdDokter.Properties.DataSource = ds.Tables("DOKTER")
            grdDokter.Properties.ValueMember = "KDDOCTOR"
            grdDokter.Properties.DisplayMember = "NAME_DISPLAY"

            If oConn.State = ConnectionState.Open Then
                oConn.Close()
            End If
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
#End Region
End Class