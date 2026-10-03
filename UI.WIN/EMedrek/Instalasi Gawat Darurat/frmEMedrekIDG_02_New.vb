Imports DataAccess
Imports UI.WIN.MAIN.My.Resources
Imports System.Linq
Imports System.Data.SqlClient

Public Class frmEMedrekIDG_02_New
#Region "Declaration"
    Private oFormMode As FORM_MODE = FORM_MODE.FORM_MODE_VIEW
    Private sNoId As String
    Private isLoad As Boolean = False
    Private oS_DIGITAL_IGD_02_NEW As New Transaksi.clsDigital_IGD_02
    Private down As Boolean = False
    Private sJENISKELAMIN As String = String.Empty
    Private sTANGGALLAHIR As String = String.Empty
#End Region
#Region "Function"
    Public Sub LoadRegister(ByVal KDREG As String, ByVal KDCUSTOMER As String, ByVal NAMAPASIEN As String, ByVal JENISKELAMIN As String, ByVal TANGGALLAHIR As String, ByVal TANGGALDATANG As String, ByVal Usia As String)
        txtNoRegister.Text = KDREG
        txtNoPasien.Text = KDCUSTOMER
        txtNamaPasien.Text = NAMAPASIEN
        txtUmur.Text = TANGGALLAHIR
        txtTanggalDaftar.Text = TANGGALDATANG
        'txtDokter.Text = KDDOCTOR
        sJENISKELAMIN = JENISKELAMIN
        sTANGGALLAHIR = TANGGALLAHIR
        sNoId = KDREG
        txtUSIA.Text = Usia
    End Sub
    'Public Sub LoadMe(ByVal NoId As String)
    '    Dim ds = oS_DIGITAL_IGD_02_NEW.GetData(NoId)
    '    If ds Is Nothing Then
    '        oFormMode = FORM_MODE.FORM_MODE_ADD
    '    Else
    '        oFormMode = FORM_MODE.FORM_MODE_EDIT
    '    End If
    '    'oFormMode = FormMode
    '    sNoId = NoId
    'End Sub
    Private Sub Form_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'Me.Text = EMedrekIGD_02.TITLE
        fn_ChangeFormState()
        isLoad = True
    End Sub
    Private Sub Form_Closing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        sCode = txtNoRegister.Text.Trim.ToUpper
    End Sub
    Private Overloads Sub Dispose()
        MyBase.Dispose()
        Me.Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
    Private Sub fn_ChangeFormState()
        sPicture = Nothing

        If sNoId = "" Then
            MsgBox("Pasien Belum dipilih, Silahkan kembali ke List Pasien " & sNoId, MsgBoxStyle.Exclamation, Me.Text)
            Me.Close()
        End If
        If sNoId = "Nul" Then
            MsgBox("Pasien Belum dipilih, Silahkan kembali ke List Pasien" & sNoId, MsgBoxStyle.Exclamation, Me.Text)
            Me.Close()
        End If

        Dim ds = oS_DIGITAL_IGD_02_NEW.GetData(sNoId)

        If ds Is Nothing Then
            oFormMode = FORM_MODE.FORM_MODE_ADD
        Else
            oFormMode = FORM_MODE.FORM_MODE_EDIT
        End If

        fn_LoadKDUSER()

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

        txtCATATANGAMBAR.Properties.ReadOnly = Status
        chkMedrek_1.Properties.ReadOnly = Status
        chkMedrek_2.Properties.ReadOnly = Status
        chkMedrek_3.Properties.ReadOnly = Status
        chkMedrek_4.Properties.ReadOnly = Status
        chkMedrek_5.Properties.ReadOnly = Status
        chkMedrek_6.Properties.ReadOnly = Status
        chkMedrek_7.Properties.ReadOnly = Status
        chkMedrek_8.Properties.ReadOnly = Status
        chkMedrek_9.Properties.ReadOnly = Status
        chkMedrek_10.Properties.ReadOnly = Status
        chkMedrek_11.Properties.ReadOnly = Status
        chkMedrek_12.Properties.ReadOnly = Status
        chkMedrek_13.Properties.ReadOnly = Status
        chkMedrek_14.Properties.ReadOnly = Status
        chkMedrek_15.Properties.ReadOnly = Status
        chkMedrek_16.Properties.ReadOnly = Status
        chkMedrek_17.Properties.ReadOnly = Status
        chkMedrek_18.Properties.ReadOnly = Status
        chkMedrek_19.Properties.ReadOnly = Status
        chkMedrek_20.Properties.ReadOnly = Status
        chkMedrek_21.Properties.ReadOnly = Status
        chkMedrek_22.Properties.ReadOnly = Status
        chkMedrek_23.Properties.ReadOnly = Status
        chkMedrek_24.Properties.ReadOnly = Status
        chkMedrek_25.Properties.ReadOnly = Status
        chkMedrek_26.Properties.ReadOnly = Status
        chkMedrek_27.Properties.ReadOnly = Status
        chkMedrek_28.Properties.ReadOnly = Status
        chkMedrek_29.Properties.ReadOnly = Status
        chkMedrek_30.Properties.ReadOnly = Status
        chkMedrek_31.Properties.ReadOnly = Status
        chkMedrek_32.Properties.ReadOnly = Status
        chkMedrek_33.Properties.ReadOnly = Status
        chkMedrek_34.Properties.ReadOnly = Status
        chkMedrek_35.Properties.ReadOnly = Status
        chkMedrek_36.Properties.ReadOnly = Status
        chkMedrek_37.Properties.ReadOnly = Status
        chkMedrek_38.Properties.ReadOnly = Status
        chkMedrek_39.Properties.ReadOnly = Status
        chkMedrek_40.Properties.ReadOnly = Status
        chkMedrek_41.Properties.ReadOnly = Status
        chkMedrek_42.Properties.ReadOnly = Status
        chkMedrek_43.Properties.ReadOnly = Status
        chkMedrek_44.Properties.ReadOnly = Status
        chkMedrek_45.Properties.ReadOnly = Status
        chkMedrek_46.Properties.ReadOnly = Status
        chkMedrek_47.Properties.ReadOnly = Status
        chkMedrek_48.Properties.ReadOnly = Status
        chkMedrek_49.Properties.ReadOnly = Status
        chkMedrek_50.Properties.ReadOnly = Status
        chkMedrek_51.Properties.ReadOnly = Status
        chkMedrek_52.Properties.ReadOnly = Status
        chkMedrek_53.Properties.ReadOnly = Status
        chkMedrek_54.Properties.ReadOnly = Status
        chkMedrek_55.Properties.ReadOnly = Status
        chkMedrek_56.Properties.ReadOnly = Status
        chkMedrek_57.Properties.ReadOnly = Status
        chkMedrek_58.Properties.ReadOnly = Status
        chkMedrek_59.Properties.ReadOnly = Status
        chkMedrek_60.Properties.ReadOnly = Status
        chkMedrek_61.Properties.ReadOnly = Status
        chkMedrek_62.Properties.ReadOnly = Status
        chkMedrek_63.Properties.ReadOnly = Status
        chkMedrek_64.Properties.ReadOnly = Status
        chkMedrek_65.Properties.ReadOnly = Status
        chkMedrek_66.Properties.ReadOnly = Status
        chkMedrek_67.Properties.ReadOnly = Status
        chkMedrek_68.Properties.ReadOnly = Status
        chkMedrek_69.Properties.ReadOnly = Status
        chkMedrek_70.Properties.ReadOnly = Status
        chkMedrek_71.Properties.ReadOnly = Status
        chkMedrek_72.Properties.ReadOnly = Status
        chkMedrek_73.Properties.ReadOnly = Status
        chkMedrek_74.Properties.ReadOnly = Status
        chkMedrek_75.Properties.ReadOnly = Status
        chkMedrek_76.Properties.ReadOnly = Status
        chkMedrek_77.Properties.ReadOnly = Status
        chkMedrek_78.Properties.ReadOnly = Status
        chkMedrek_79.Properties.ReadOnly = Status
        chkMedrek_80.Properties.ReadOnly = Status
        chkMedrek_81.Properties.ReadOnly = Status
        chkMedrek_82.Properties.ReadOnly = Status
        chkMedrek_83.Properties.ReadOnly = Status
        chkMedrek_84.Properties.ReadOnly = Status
        chkMedrek_85.Properties.ReadOnly = Status
        chkMedrek_86.Properties.ReadOnly = Status
        chkMedrek_87.Properties.ReadOnly = Status
        chkMedrek_88.Properties.ReadOnly = Status
        chkMedrek_89.Properties.ReadOnly = Status
        chkMedrek_90.Properties.ReadOnly = Status
        chkMedrek_91.Properties.ReadOnly = Status
        chkMedrek_92.Properties.ReadOnly = Status
        chkMedrek_93.Properties.ReadOnly = Status
        chkMedrek_94.Properties.ReadOnly = Status
        chkMedrek_95.Properties.ReadOnly = Status
        chkMedrek_96.Properties.ReadOnly = Status
        chkMedrek_97.Properties.ReadOnly = Status
        chkMedrek_98.Properties.ReadOnly = Status
        chkMedrek_99.Properties.ReadOnly = Status
        chkMedrek_100.Properties.ReadOnly = Status
        chkMedrek_101.Properties.ReadOnly = Status
        chkMedrek_102.Properties.ReadOnly = Status
        chkMedrek_103.Properties.ReadOnly = Status
        chkMedrek_104.Properties.ReadOnly = Status
        chkMedrek_105.Properties.ReadOnly = Status
        chkMedrek_106.Properties.ReadOnly = Status
        chkMedrek_107.Properties.ReadOnly = Status
        chkMedrek_108.Properties.ReadOnly = Status
        chkMedrek_109.Properties.ReadOnly = Status
        chkMedrek_110.Properties.ReadOnly = Status
        chkMedrek_111.Properties.ReadOnly = Status
        chkMedrek_112.Properties.ReadOnly = Status
        chkMedrek_113.Properties.ReadOnly = Status
        chkMedrek_114.Properties.ReadOnly = Status
        chkMedrek_115.Properties.ReadOnly = Status
        chkMedrek_116.Properties.ReadOnly = Status
        chkMedrek_117.Properties.ReadOnly = Status
        chkMedrek_118.Properties.ReadOnly = Status
        chkMedrek_119.Properties.ReadOnly = Status
        chkMedrek_120.Properties.ReadOnly = Status
        chkMedrek_121.Properties.ReadOnly = Status
        chkMedrek_122.Properties.ReadOnly = Status
        chkMedrek_123.Properties.ReadOnly = Status
        chkMedrek_124.Properties.ReadOnly = Status
        chkMedrek_125.Properties.ReadOnly = Status
        chkMedrek_126.Properties.ReadOnly = Status
        chkMedrek_127.Properties.ReadOnly = Status
        chkMedrek_128.Properties.ReadOnly = Status
        chkMedrek_129.Properties.ReadOnly = Status
        chkMedrek_130.Properties.ReadOnly = Status
        chkMedrek_131.Properties.ReadOnly = Status
        chkMedrek_132.Properties.ReadOnly = Status
        chkMedrek_133.Properties.ReadOnly = Status
        chkMedrek_134.Properties.ReadOnly = Status
        chkMedrek_135.Properties.ReadOnly = Status
        chkMedrek_136.Properties.ReadOnly = Status
        chkMedrek_137.Properties.ReadOnly = Status
        chkMedrek_138.Properties.ReadOnly = Status
        chkMedrek_139.Properties.ReadOnly = Status
        chkMedrek_140.Properties.ReadOnly = Status
        chkMedrek_141.Properties.ReadOnly = Status
        chkMedrek_142.Properties.ReadOnly = Status
        chkMedrek_143.Properties.ReadOnly = Status
        chkMedrek_144.Properties.ReadOnly = Status
        chkMedrek_145.Properties.ReadOnly = Status
        chkMedrek_146.Properties.ReadOnly = Status
        chkMedrek_147.Properties.ReadOnly = Status
        chkMedrek_148.Properties.ReadOnly = Status
        chkMedrek_149.Properties.ReadOnly = Status
        chkMedrek_150.Properties.ReadOnly = Status
        chkMedrek_151.Properties.ReadOnly = Status
        chkMedrek_152.Properties.ReadOnly = Status
        chkMedrek_153.Properties.ReadOnly = Status
        chkMedrek_154.Properties.ReadOnly = Status
        chkMedrek_155.Properties.ReadOnly = Status
        chkMedrek_156.Properties.ReadOnly = Status
        chkMedrek_157.Properties.ReadOnly = Status
        chkMedrek_158.Properties.ReadOnly = Status
        chkMedrek_159.Properties.ReadOnly = Status
        chkMedrek_160.Properties.ReadOnly = Status
        chkMedrek_161.Properties.ReadOnly = Status
        chkMedrek_162.Properties.ReadOnly = Status
        chkMedrek_163.Properties.ReadOnly = Status
        chkMedrek_164.Properties.ReadOnly = Status
        chkMedrek_165.Properties.ReadOnly = Status
        chkMedrek_166.Properties.ReadOnly = Status
        chkMedrek_167.Properties.ReadOnly = Status
        chkMedrek_168.Properties.ReadOnly = Status
        chkMedrek_169.Properties.ReadOnly = Status
        chkMedrek_170.Properties.ReadOnly = Status
        chkMedrek_171.Properties.ReadOnly = Status
        chkMedrek_172.Properties.ReadOnly = Status
        chkMedrek_173.Properties.ReadOnly = Status
        chkMedrek_174.Properties.ReadOnly = Status
        chkMedrek_175.Properties.ReadOnly = Status
        chkMedrek_176.Properties.ReadOnly = Status
        chkMedrek_177.Properties.ReadOnly = Status
        chkMedrek_178.Properties.ReadOnly = Status
        chkMedrek_179.Properties.ReadOnly = Status
        chkMedrek_180.Properties.ReadOnly = Status
        chkMedrek_181.Properties.ReadOnly = Status
        chkMedrek_182.Properties.ReadOnly = Status
        chkMedrek_183.Properties.ReadOnly = Status
        chkMedrek_184.Properties.ReadOnly = Status
        chkMedrek_185.Properties.ReadOnly = Status
        chkMedrek_186.Properties.ReadOnly = Status
        chkMedrek_187.Properties.ReadOnly = Status
        chkMedrek_188.Properties.ReadOnly = Status
        chkMedrek_189.Properties.ReadOnly = Status
        chkMedrek_190.Properties.ReadOnly = Status
        chkMedrek_191.Properties.ReadOnly = Status
        chkMedrek_192.Properties.ReadOnly = Status
        chkMedrek_193.Properties.ReadOnly = Status
        chkMedrek_194.Properties.ReadOnly = Status
        chkMedrek_195.Properties.ReadOnly = Status
        chkMedrek_196.Properties.ReadOnly = Status
        chkMedrek_197.Properties.ReadOnly = Status
        chkMedrek_198.Properties.ReadOnly = Status
        chkMedrek_199.Properties.ReadOnly = Status
        chkMedrek_200.Properties.ReadOnly = Status
        chkMedrek_201.Properties.ReadOnly = Status
        chkMedrek_202.Properties.ReadOnly = Status
        chkMedrek_203.Properties.ReadOnly = Status
        chkMedrek_204.Properties.ReadOnly = Status
        chkMedrek_205.Properties.ReadOnly = Status
        chkMedrek_206.Properties.ReadOnly = Status
        chkMedrek_207.Properties.ReadOnly = Status
        chkMedrek_208.Properties.ReadOnly = Status
        chkMedrek_209.Properties.ReadOnly = Status
        chkMedrek_210.Properties.ReadOnly = Status
        chkMedrek_211.Properties.ReadOnly = Status
        chkMedrek_212.Properties.ReadOnly = Status
        chkMedrek_213.Properties.ReadOnly = Status
        chkMedrek_214.Properties.ReadOnly = Status
        chkMedrek_215.Properties.ReadOnly = Status
        chkMedrek_216.Properties.ReadOnly = Status
        chkMedrek_217.Properties.ReadOnly = Status
        chkMedrek_218.Properties.ReadOnly = Status
        chkMedrek_219.Properties.ReadOnly = Status
        chkMedrek_220.Properties.ReadOnly = Status
        chkMedrek_221.Properties.ReadOnly = Status
        chkMedrek_222.Properties.ReadOnly = Status
        chkMedrek_223.Properties.ReadOnly = Status
        chkMedrek_224.Properties.ReadOnly = Status
        chkMedrek_225.Properties.ReadOnly = Status
        chkMedrek_226.Properties.ReadOnly = Status
        chkMedrek_227.Properties.ReadOnly = Status
        chkMedrek_228.Properties.ReadOnly = Status
        chkMedrek_229.Properties.ReadOnly = Status
        chkMedrek_230.Properties.ReadOnly = Status
        chkMedrek_231.Properties.ReadOnly = Status
        chkMedrek_232.Properties.ReadOnly = Status
        chkMedrek_233.Properties.ReadOnly = Status
        chkMedrek_234.Properties.ReadOnly = Status
        chkMedrek_235.Properties.ReadOnly = Status
        chkMedrek_236.Properties.ReadOnly = Status
        chkMedrek_237.Properties.ReadOnly = Status
        chkMedrek_238.Properties.ReadOnly = Status
        chkMedrek_239.Properties.ReadOnly = Status
        chkMedrek_240.Properties.ReadOnly = Status
        chkMedrek_241.Properties.ReadOnly = Status
        chkMedrek_242.Properties.ReadOnly = Status
        chkMedrek_243.Properties.ReadOnly = Status
        chkMedrek_244.Properties.ReadOnly = Status
        chkMedrek_245.Properties.ReadOnly = Status
        chkMedrek_246.Properties.ReadOnly = Status
        chkMedrek_247.Properties.ReadOnly = Status
        chkMedrek_248.Properties.ReadOnly = Status
        chkMedrek_249.Properties.ReadOnly = Status
        chkMedrek_250.Properties.ReadOnly = Status
        chkMedrek_251.Properties.ReadOnly = Status
        chkMedrek_252.Properties.ReadOnly = Status
        chkMedrek_253.Properties.ReadOnly = Status
        chkMedrek_254.Properties.ReadOnly = Status
        chkMedrek_255.Properties.ReadOnly = Status
        chkMedrek_256.Properties.ReadOnly = Status
        chkMedrek_257.Properties.ReadOnly = Status
        chkMedrek_258.Properties.ReadOnly = Status
        txtMedrek_1.Properties.ReadOnly = Status
        txtMedrek_2.Properties.ReadOnly = Status
        cboTRIAGE_AIRWAY.Properties.ReadOnly = Status
        cboTRIAGE_BREATHING.Properties.ReadOnly = Status
        cboTRIAGE_CIRCULATION.Properties.ReadOnly = Status
        txtMedrek_6.Properties.ReadOnly = Status
        TimeEdit1.Properties.ReadOnly = Status
        TimeEdit2.Properties.ReadOnly = Status
        TimeEdit3.Properties.ReadOnly = Status
        txtMedrek_10.Properties.ReadOnly = Status
        txtMedrek_11.Properties.ReadOnly = Status
        txtMedrek_12.Properties.ReadOnly = Status
        txtMedrek_13.Properties.ReadOnly = Status
        txtMedrek_14.Properties.ReadOnly = Status
        txtMedrek_15.Properties.ReadOnly = Status
        TimeEdit4.Properties.ReadOnly = Status
        TimeEdit5.Properties.ReadOnly = Status
        TimeEdit6.Properties.ReadOnly = Status
        txtMedrek_19.Properties.ReadOnly = Status
        txtMedrek_20.Properties.ReadOnly = Status
        txtMedrek_21.Properties.ReadOnly = Status
        txtMedrek_22.Properties.ReadOnly = Status
        txtMedrek_23.Properties.ReadOnly = Status
        txtMedrek_24.Properties.ReadOnly = Status
        txtMedrek_25.Properties.ReadOnly = Status
        txtMedrek_26.Properties.ReadOnly = Status
        txtMedrek_27.Properties.ReadOnly = Status
        txtMedrek_28.Properties.ReadOnly = Status
        txtMedrek_29.Properties.ReadOnly = Status
        TimeEdit7.Properties.ReadOnly = Status
        TimeEdit8.Properties.ReadOnly = Status
        TimeEdit9.Properties.ReadOnly = Status
        txtMedrek_33.Properties.ReadOnly = Status
        txtMedrek_34.Properties.ReadOnly = Status
        txtMedrek_35.Properties.ReadOnly = Status
        txtMedrek_36.Properties.ReadOnly = Status
        txtMedrek_37.Properties.ReadOnly = Status
        txtMedrek_38.Properties.ReadOnly = Status
        txtMedrek_39.Properties.ReadOnly = Status
        txtMedrek_40.Properties.ReadOnly = Status
        txtMedrek_41.Properties.ReadOnly = Status
        txtMedrek_42.Properties.ReadOnly = Status
        txtMedrek_43.Properties.ReadOnly = Status
        txtMedrek_44.Properties.ReadOnly = Status
        txtMedrek_45.Properties.ReadOnly = Status
        txtMedrek_46.Properties.ReadOnly = Status
        txtMedrek_47.Properties.ReadOnly = Status
        txtMedrek_48.Properties.ReadOnly = Status
        txtMedrek_49.Properties.ReadOnly = Status
        txtMedrek_50.Properties.ReadOnly = Status
        txtMedrek_51.Properties.ReadOnly = Status
        txtMedrek_52.Properties.ReadOnly = Status
        txtMedrek_53.Properties.ReadOnly = Status
        txtMedrek_54.Properties.ReadOnly = Status
        txtMedrek_55.Properties.ReadOnly = Status
        txtMedrek_56.Properties.ReadOnly = Status
        txtMedrek_57.Properties.ReadOnly = Status
        txtMedrek_58.Properties.ReadOnly = Status
        txtMedrek_59.Properties.ReadOnly = Status
        TimeEdit10.Properties.ReadOnly = Status
        TimeEdit11.Properties.ReadOnly = Status
        TimeEdit12.Properties.ReadOnly = Status
        txt_GCS.Properties.ReadOnly = Status
        cbo_E.Properties.ReadOnly = Status
        cbo_M.Properties.ReadOnly = Status
        cbo_V.Properties.ReadOnly = Status
        txtMedrek_67.Properties.ReadOnly = Status
        txtMedrek_68.Properties.ReadOnly = Status
        txtMedrek_69.Properties.ReadOnly = Status
        txtMedrek_70.Properties.ReadOnly = Status
        txtMedrek_71.Properties.ReadOnly = Status
        txtMedrek_72.Properties.ReadOnly = Status
        txtMedrek_73.Properties.ReadOnly = Status
        txtMedrek_74.Properties.ReadOnly = Status
        txtMedrek_75.Properties.ReadOnly = Status
        txtMedrek_76.Properties.ReadOnly = Status
        txtMedrek_77.Properties.ReadOnly = Status
        txtMedrek_78.Properties.ReadOnly = Status
        TimeEdit13.Properties.ReadOnly = Status
        TimeEdit14.Properties.ReadOnly = Status
        TimeEdit15.Properties.ReadOnly = Status
        txtMedrek_82.Properties.ReadOnly = Status
        txtMedrek_83.Properties.ReadOnly = Status
        cboSkalaNyeri.Properties.ReadOnly = Status
        txtMedrek_85.Properties.ReadOnly = Status
        txtMedrek_86.Properties.ReadOnly = Status
        txtMedrek_87.Properties.ReadOnly = Status
        txtMedrek_88.Properties.ReadOnly = Status
        txtMedrek_89.Properties.ReadOnly = Status
        TimeEdit16.Properties.ReadOnly = Status
        TimeEdit17.Properties.ReadOnly = Status
        TimeEdit18.Properties.ReadOnly = Status
        txtMedrek_93.Properties.ReadOnly = Status
        txtMedrek_94.Properties.ReadOnly = Status
        txtMedrek_95.Properties.ReadOnly = Status
        txtMedrek_96.Properties.ReadOnly = Status
        txtMedrek_97.Properties.ReadOnly = Status
        txtMedrek_98.Properties.ReadOnly = Status
        txtMedrek_99.Properties.ReadOnly = Status
        txtMedrek_100.Properties.ReadOnly = Status
        txtMedrek_101.Properties.ReadOnly = Status
        txtMedrek_102.Properties.ReadOnly = Status
        txtMedrek_103.Properties.ReadOnly = Status
        txtMedrek_104.Properties.ReadOnly = Status
        txtMedrek_105.Properties.ReadOnly = Status
        txtMedrek_106.Properties.ReadOnly = Status
        txtMedrek_107.Properties.ReadOnly = Status
        txtMedrek_108.Properties.ReadOnly = Status
        'txtMedrek_109.Properties.ReadOnly = Status
        'txtMedrek_110.Properties.ReadOnly = Status
        txtSIMPANGAMBAR_1.Properties.ReadOnly = True


        txtKEADAAN.Properties.ReadOnly = Status
        cboTINGKATKESADARAN.Properties.ReadOnly = Status
        txtTANDA_VITAL_01.Properties.ReadOnly = Status
        txtTANDA_VITAL_02.Properties.ReadOnly = Status
        txtTANDA_VITAL_03.Properties.ReadOnly = Status
        txtTANDA_VITAL_04.Properties.ReadOnly = Status
        txtTANDA_VITAL_05.Properties.ReadOnly = Status
        txtTANDA_VITAL_06.Properties.ReadOnly = Status
        txtTANDA_VITAL_07.Properties.ReadOnly = Status
        txtTANDA_VITAL_08.Properties.ReadOnly = Status
        txtTANDA_VITAL_09.Properties.ReadOnly = Status
        chkTANDA_VITAL_REGULER.Properties.ReadOnly = Status
        chkTANDA_VITAL_IREGULER.Properties.ReadOnly = Status
        chkSTATUS_PSIKOLOGIS_01.Properties.ReadOnly = Status
        chkSTATUS_PSIKOLOGIS_02.Properties.ReadOnly = Status
        chkSTATUS_PSIKOLOGIS_03.Properties.ReadOnly = Status
        chkSTATUS_PSIKOLOGIS_04.Properties.ReadOnly = Status
        chkSTATUS_PSIKOLOGIS_05.Properties.ReadOnly = Status
        chkSTATUS_PSIKOLOGIS_06.Properties.ReadOnly = Status
        txtSTATUS_PSIKOLOGIS_06_TEXT.Properties.ReadOnly = Status
        chkSTATUS_MENTAL_01.Properties.ReadOnly = Status
        chkSTATUS_MENTAL_02.Properties.ReadOnly = Status
        txtSTATUS_MENTAL_02_TEXT.Properties.ReadOnly = Status
        chkSTATUS_MENTAL_03.Properties.ReadOnly = Status
        txtSTATUS_MENTAL_03_TEXT.Properties.ReadOnly = Status
        chkSOSIAL_01_1.Properties.ReadOnly = Status
        chkSOSIAL_01_2.Properties.ReadOnly = Status
        chkSOSIAL_02_1.Properties.ReadOnly = Status
        chkSOSIAL_02_2.Properties.ReadOnly = Status
        chkSOSIAL_02_3.Properties.ReadOnly = Status
        chkSOSIAL_02_4.Properties.ReadOnly = Status
        txtSOSIAL_02_4_TEXT.Properties.ReadOnly = Status
        txtSOSIAL_03_1_TEXT.Properties.ReadOnly = Status
        txtSOSIAL_03_2_TEXT.Properties.ReadOnly = Status
        txtSOSIAL_03_3_TEXT.Properties.ReadOnly = Status
        txtSTATUS_SPIRITUAL_01_TEXT.Properties.ReadOnly = Status
        txtSTATUS_SPIRITUAL_02_TEXT.Properties.ReadOnly = Status
        grdKDUSER.Properties.ReadOnly = Status


        TextEdit1.ReadOnly = Status
        TextEdit2.ReadOnly = Status
        'Dim oSetUser As New Setting.clsUser
        'Dim dsUser = oSetUser.GetData(sUserID)
        'If dsUser IsNot Nothing Then
        '    If dsUser.ISOTORTY = True Then
        '        lTanggal.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        '        sIsOtority = True
        '    Else
        '        lTanggal.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        '        sIsOtority = False
        '    End If
        'End If
    End Sub
    Private Sub fn_EmptyMe()
        txtRELOAD.ResetText()
        txtCATATANGAMBAR.ResetText()
        chkMedrek_1.Checked = False
        chkMedrek_2.Checked = False
        chkMedrek_3.Checked = False
        chkMedrek_4.Checked = False
        chkMedrek_5.Checked = False
        chkMedrek_6.Checked = False
        chkMedrek_7.Checked = False
        chkMedrek_8.Checked = False
        chkMedrek_9.Checked = False
        chkMedrek_10.Checked = False
        chkMedrek_11.Checked = False
        chkMedrek_12.Checked = False
        chkMedrek_13.Checked = False
        chkMedrek_14.Checked = False
        chkMedrek_15.Checked = False
        chkMedrek_16.Checked = False
        chkMedrek_17.Checked = False
        chkMedrek_18.Checked = False
        chkMedrek_19.Checked = False
        chkMedrek_20.Checked = False
        chkMedrek_21.Checked = False
        chkMedrek_22.Checked = False
        chkMedrek_23.Checked = False
        chkMedrek_24.Checked = False
        chkMedrek_25.Checked = False
        chkMedrek_26.Checked = False
        chkMedrek_27.Checked = False
        chkMedrek_28.Checked = False
        chkMedrek_29.Checked = False
        chkMedrek_30.Checked = False
        chkMedrek_31.Checked = False
        chkMedrek_32.Checked = False
        chkMedrek_33.Checked = False
        chkMedrek_34.Checked = False
        chkMedrek_35.Checked = False
        chkMedrek_36.Checked = False
        chkMedrek_37.Checked = False
        chkMedrek_38.Checked = False
        chkMedrek_39.Checked = False
        chkMedrek_40.Checked = False
        chkMedrek_41.Checked = False
        chkMedrek_42.Checked = False
        chkMedrek_43.Checked = False
        chkMedrek_44.Checked = False
        chkMedrek_45.Checked = False
        chkMedrek_46.Checked = False
        chkMedrek_47.Checked = False
        chkMedrek_48.Checked = False
        chkMedrek_49.Checked = False
        chkMedrek_50.Checked = False
        chkMedrek_51.Checked = False
        chkMedrek_52.Checked = False
        chkMedrek_53.Checked = False
        chkMedrek_54.Checked = False
        chkMedrek_55.Checked = False
        chkMedrek_56.Checked = False
        chkMedrek_57.Checked = False
        chkMedrek_58.Checked = False
        chkMedrek_59.Checked = False
        chkMedrek_60.Checked = False
        chkMedrek_61.Checked = False
        chkMedrek_62.Checked = False
        chkMedrek_63.Checked = False
        chkMedrek_64.Checked = False
        chkMedrek_65.Checked = False
        chkMedrek_66.Checked = False
        chkMedrek_67.Checked = False
        chkMedrek_68.Checked = False
        chkMedrek_69.Checked = False
        chkMedrek_70.Checked = False
        chkMedrek_71.Checked = False
        chkMedrek_72.Checked = False
        chkMedrek_73.Checked = False
        chkMedrek_74.Checked = False
        chkMedrek_75.Checked = False
        chkMedrek_76.Checked = False
        chkMedrek_77.Checked = False
        chkMedrek_78.Checked = False
        chkMedrek_79.Checked = False
        chkMedrek_80.Checked = False
        chkMedrek_81.Checked = False
        chkMedrek_82.Checked = False
        chkMedrek_83.Checked = False
        chkMedrek_84.Checked = False
        chkMedrek_85.Checked = False
        chkMedrek_86.Checked = False
        chkMedrek_87.Checked = False
        chkMedrek_88.Checked = False
        chkMedrek_89.Checked = False
        chkMedrek_90.Checked = False
        chkMedrek_91.Checked = False
        chkMedrek_92.Checked = False
        chkMedrek_93.Checked = False
        chkMedrek_94.Checked = False
        chkMedrek_95.Checked = False
        chkMedrek_96.Checked = False
        chkMedrek_97.Checked = False
        chkMedrek_98.Checked = False
        chkMedrek_99.Checked = False
        chkMedrek_100.Checked = False
        chkMedrek_101.Checked = False
        chkMedrek_102.Checked = False
        chkMedrek_103.Checked = False
        chkMedrek_104.Checked = False
        chkMedrek_105.Checked = False
        chkMedrek_106.Checked = False
        chkMedrek_107.Checked = False
        chkMedrek_108.Checked = False
        chkMedrek_109.Checked = False
        chkMedrek_110.Checked = False
        chkMedrek_111.Checked = False
        chkMedrek_112.Checked = False
        chkMedrek_113.Checked = False
        chkMedrek_114.Checked = False
        chkMedrek_115.Checked = False
        chkMedrek_116.Checked = False
        chkMedrek_117.Checked = False
        chkMedrek_118.Checked = False
        chkMedrek_119.Checked = False
        chkMedrek_120.Checked = False
        chkMedrek_121.Checked = False
        chkMedrek_122.Checked = False
        chkMedrek_123.Checked = False
        chkMedrek_124.Checked = False
        chkMedrek_125.Checked = False
        chkMedrek_126.Checked = False
        chkMedrek_127.Checked = False
        chkMedrek_128.Checked = False
        chkMedrek_129.Checked = False
        chkMedrek_130.Checked = False
        chkMedrek_131.Checked = False
        chkMedrek_132.Checked = False
        chkMedrek_133.Checked = False
        chkMedrek_134.Checked = False
        chkMedrek_135.Checked = False
        chkMedrek_136.Checked = False
        chkMedrek_137.Checked = False
        chkMedrek_138.Checked = False
        chkMedrek_139.Checked = False
        chkMedrek_140.Checked = False
        chkMedrek_141.Checked = False
        chkMedrek_142.Checked = False
        chkMedrek_143.Checked = False
        chkMedrek_144.Checked = False
        chkMedrek_145.Checked = False
        chkMedrek_146.Checked = False
        chkMedrek_147.Checked = False
        chkMedrek_148.Checked = False
        chkMedrek_149.Checked = False
        chkMedrek_150.Checked = False
        chkMedrek_151.Checked = False
        chkMedrek_152.Checked = False
        chkMedrek_153.Checked = False
        chkMedrek_154.Checked = False
        chkMedrek_155.Checked = False
        chkMedrek_156.Checked = False
        chkMedrek_157.Checked = False
        chkMedrek_158.Checked = False
        chkMedrek_159.Checked = False
        chkMedrek_160.Checked = False
        chkMedrek_161.Checked = False
        chkMedrek_162.Checked = False
        chkMedrek_163.Checked = False
        chkMedrek_164.Checked = False
        chkMedrek_165.Checked = False
        chkMedrek_166.Checked = False
        chkMedrek_167.Checked = False
        chkMedrek_168.Checked = False
        chkMedrek_169.Checked = False
        chkMedrek_170.Checked = False
        chkMedrek_171.Checked = False
        chkMedrek_172.Checked = False
        chkMedrek_173.Checked = False
        chkMedrek_174.Checked = False
        chkMedrek_175.Checked = False
        chkMedrek_176.Checked = False
        chkMedrek_177.Checked = False
        chkMedrek_178.Checked = False
        chkMedrek_179.Checked = False
        chkMedrek_180.Checked = False
        chkMedrek_181.Checked = False
        chkMedrek_182.Checked = False
        chkMedrek_183.Checked = False
        chkMedrek_184.Checked = False
        chkMedrek_185.Checked = False
        chkMedrek_186.Checked = False
        chkMedrek_187.Checked = False
        chkMedrek_188.Checked = False
        chkMedrek_189.Checked = False
        chkMedrek_190.Checked = False
        chkMedrek_191.Checked = False
        chkMedrek_192.Checked = False
        chkMedrek_193.Checked = False
        chkMedrek_194.Checked = False
        chkMedrek_195.Checked = False
        chkMedrek_196.Checked = False
        chkMedrek_197.Checked = False
        chkMedrek_198.Checked = False
        chkMedrek_199.Checked = False
        chkMedrek_200.Checked = False
        chkMedrek_201.Checked = False
        chkMedrek_202.Checked = False
        chkMedrek_203.Checked = False
        chkMedrek_204.Checked = False
        chkMedrek_205.Checked = False
        chkMedrek_206.Checked = False
        chkMedrek_207.Checked = False
        chkMedrek_208.Checked = False
        chkMedrek_209.Checked = False
        chkMedrek_210.Checked = False
        chkMedrek_211.Checked = False
        chkMedrek_212.Checked = False
        chkMedrek_213.Checked = False
        chkMedrek_214.Checked = False
        chkMedrek_215.Checked = False
        chkMedrek_216.Checked = False
        chkMedrek_217.Checked = False
        chkMedrek_218.Checked = False
        chkMedrek_219.Checked = False
        chkMedrek_220.Checked = False
        chkMedrek_221.Checked = False
        chkMedrek_222.Checked = False
        chkMedrek_223.Checked = False
        chkMedrek_224.Checked = False
        chkMedrek_225.Checked = False
        chkMedrek_226.Checked = False
        chkMedrek_227.Checked = False
        chkMedrek_228.Checked = False
        chkMedrek_229.Checked = False
        chkMedrek_230.Checked = False
        chkMedrek_231.Checked = False
        chkMedrek_232.Checked = False
        chkMedrek_233.Checked = False
        chkMedrek_234.Checked = False
        chkMedrek_235.Checked = False
        chkMedrek_236.Checked = False
        chkMedrek_237.Checked = False
        chkMedrek_238.Checked = False
        chkMedrek_239.Checked = False
        chkMedrek_240.Checked = False
        chkMedrek_241.Checked = False
        chkMedrek_242.Checked = False
        chkMedrek_243.Checked = False
        chkMedrek_244.Checked = False
        chkMedrek_245.Checked = False
        chkMedrek_246.Checked = False
        chkMedrek_247.Checked = False
        chkMedrek_248.Checked = False
        chkMedrek_249.Checked = False
        chkMedrek_250.Checked = False
        chkMedrek_251.Checked = False
        chkMedrek_252.Checked = False
        chkMedrek_253.Checked = False
        chkMedrek_254.Checked = False
        chkMedrek_255.Checked = False
        chkMedrek_256.Checked = False
        chkMedrek_257.Checked = False
        chkMedrek_258.Checked = False
        txtMedrek_1.ResetText()
        txtMedrek_2.ResetText()
        cboTRIAGE_AIRWAY.ResetText()
        cboTRIAGE_BREATHING.ResetText()
        cboTRIAGE_CIRCULATION.ResetText()
        txtMedrek_6.ResetText()
        TimeEdit1.Time = Now
        TimeEdit2.Time = Now
        TimeEdit3.Time = Now
        txtMedrek_10.ResetText()
        txtMedrek_11.ResetText()
        txtMedrek_12.ResetText()
        txtMedrek_13.ResetText()
        txtMedrek_14.ResetText()
        txtMedrek_15.ResetText()
        TimeEdit4.Time = Now
        TimeEdit5.Time = Now
        TimeEdit6.Time = Now
        txtMedrek_19.ResetText()
        txtMedrek_20.ResetText()
        txtMedrek_21.ResetText()
        txtMedrek_22.ResetText()
        txtMedrek_23.ResetText()
        txtMedrek_24.ResetText()
        txtMedrek_25.ResetText()
        txtMedrek_26.ResetText()
        txtMedrek_27.ResetText()
        txtMedrek_28.ResetText()
        txtMedrek_29.ResetText()
        TimeEdit7.Time = Now
        TimeEdit8.Time = Now
        TimeEdit9.Time = Now
        txtMedrek_33.ResetText()
        txtMedrek_34.ResetText()
        txtMedrek_35.ResetText()
        txtMedrek_36.ResetText()
        txtMedrek_37.ResetText()
        txtMedrek_38.ResetText()
        txtMedrek_39.ResetText()
        txtMedrek_40.ResetText()
        txtMedrek_41.ResetText()
        txtMedrek_42.ResetText()
        txtMedrek_43.ResetText()
        txtMedrek_44.ResetText()
        txtMedrek_45.ResetText()
        txtMedrek_46.ResetText()
        txtMedrek_47.ResetText()
        txtMedrek_48.ResetText()
        txtMedrek_49.ResetText()
        txtMedrek_50.ResetText()
        txtMedrek_51.ResetText()
        txtMedrek_52.ResetText()
        txtMedrek_53.ResetText()
        txtMedrek_54.ResetText()
        txtMedrek_55.ResetText()
        txtMedrek_56.ResetText()
        txtMedrek_57.ResetText()
        txtMedrek_58.ResetText()
        txtMedrek_59.ResetText()
        TimeEdit10.Time = Now
        TimeEdit11.Time = Now
        TimeEdit12.Time = Now
        txt_GCS.ResetText()
        cbo_E.ResetText()
        cbo_M.ResetText()
        cbo_V.ResetText()
        txtMedrek_67.ResetText()
        txtMedrek_68.ResetText()
        txtMedrek_69.ResetText()
        txtMedrek_70.ResetText()
        txtMedrek_71.ResetText()
        txtMedrek_72.ResetText()
        txtMedrek_73.ResetText()
        txtMedrek_74.ResetText()
        txtMedrek_75.ResetText()
        txtMedrek_76.ResetText()
        txtMedrek_77.ResetText()
        txtMedrek_78.ResetText()
        TimeEdit13.Time = Now
        TimeEdit14.Time = Now
        TimeEdit15.Time = Now
        txtMedrek_82.ResetText()
        txtMedrek_83.ResetText()
        cboSkalaNyeri.ResetText()
        txtMedrek_85.ResetText()
        txtMedrek_86.ResetText()
        txtMedrek_87.ResetText()
        txtMedrek_88.ResetText()
        txtMedrek_89.ResetText()
        TimeEdit16.Time = Now
        TimeEdit17.Time = Now
        TimeEdit18.Time = Now
        txtMedrek_93.ResetText()
        txtMedrek_94.ResetText()
        txtMedrek_95.ResetText()
        txtMedrek_96.ResetText()
        txtMedrek_97.ResetText()
        txtMedrek_98.ResetText()
        txtMedrek_99.ResetText()
        txtMedrek_100.ResetText()
        txtMedrek_101.ResetText()
        txtMedrek_102.ResetText()
        txtMedrek_103.ResetText()
        txtMedrek_104.ResetText()
        txtMedrek_105.ResetText()
        txtMedrek_106.ResetText()
        txtMedrek_107.ResetText()
        txtMedrek_108.ResetText()

        deDATE.DateTime = Now

        txtKEADAAN.ResetText()
        cboTINGKATKESADARAN.ResetText()
        txtTANDA_VITAL_01.ResetText()
        txtTANDA_VITAL_02.ResetText()
        txtTANDA_VITAL_03.ResetText()
        txtTANDA_VITAL_04.ResetText()
        txtTANDA_VITAL_05.ResetText()
        txtTANDA_VITAL_06.ResetText()
        txtTANDA_VITAL_07.ResetText()
        txtTANDA_VITAL_08.ResetText()
        txtTANDA_VITAL_09.ResetText()
        chkTANDA_VITAL_REGULER.Checked = False
        chkTANDA_VITAL_IREGULER.Checked = False
        chkSTATUS_PSIKOLOGIS_01.Checked = False
        chkSTATUS_PSIKOLOGIS_02.Checked = False
        chkSTATUS_PSIKOLOGIS_03.Checked = False
        chkSTATUS_PSIKOLOGIS_04.Checked = False
        chkSTATUS_PSIKOLOGIS_05.Checked = False
        chkSTATUS_PSIKOLOGIS_06.Checked = False
        txtSTATUS_PSIKOLOGIS_06_TEXT.ResetText()
        chkSTATUS_MENTAL_01.Checked = False
        chkSTATUS_MENTAL_02.Checked = False
        txtSTATUS_MENTAL_02_TEXT.ResetText()
        chkSTATUS_MENTAL_03.Checked = False
        txtSTATUS_MENTAL_03_TEXT.ResetText()
        chkSOSIAL_01_1.Checked = False
        chkSOSIAL_01_2.Checked = False
        chkSOSIAL_02_1.Checked = False
        chkSOSIAL_02_2.Checked = False
        chkSOSIAL_02_3.Checked = False
        chkSOSIAL_02_4.Checked = False
        txtSOSIAL_02_4_TEXT.ResetText()
        txtSOSIAL_03_1_TEXT.ResetText()
        txtSOSIAL_03_2_TEXT.ResetText()
        txtSOSIAL_03_3_TEXT.ResetText()
        txtSTATUS_SPIRITUAL_01_TEXT.ResetText()
        txtSTATUS_SPIRITUAL_02_TEXT.ResetText()
        grdKDUSER.ResetText()


        TextEdit1.ResetText()
        TextEdit2.ResetText()

        txtSIMPANGAMBAR_1.Text = sAlamatSimpanAsesmenIGD

        Try
            If Not IO.Directory.Exists(txtSIMPANGAMBAR_1.Text) Then
                IO.Directory.CreateDirectory(txtSIMPANGAMBAR_1.Text)
            End If
        Catch ex As Exception
            MsgBox("Simpan Folder Asesmen IGD", MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadData()
        Try
            ' ***** HEADER *****
            Dim ds = oS_DIGITAL_IGD_02_NEW.GetData(sNoId)

            With ds
                .ALAMAT = txtCATATANGAMBAR.Text
                chkMedrek_1.Checked = .BIT_1
                chkMedrek_2.Checked = .BIT_2
                chkMedrek_3.Checked = .BIT_3
                chkMedrek_4.Checked = .BIT_4
                chkMedrek_5.Checked = .BIT_5
                chkMedrek_6.Checked = .BIT_6
                chkMedrek_7.Checked = .BIT_7
                chkMedrek_8.Checked = .BIT_8
                chkMedrek_9.Checked = .BIT_9
                chkMedrek_10.Checked = .BIT_10
                chkMedrek_11.Checked = .BIT_11
                chkMedrek_12.Checked = .BIT_12
                chkMedrek_13.Checked = .BIT_13
                chkMedrek_14.Checked = .BIT_14
                chkMedrek_15.Checked = .BIT_15
                chkMedrek_16.Checked = .BIT_16
                chkMedrek_17.Checked = .BIT_17
                chkMedrek_18.Checked = .BIT_18
                chkMedrek_19.Checked = .BIT_19
                chkMedrek_20.Checked = .BIT_20
                chkMedrek_21.Checked = .BIT_21
                chkMedrek_22.Checked = .BIT_22
                chkMedrek_23.Checked = .BIT_23
                chkMedrek_24.Checked = .BIT_24
                chkMedrek_25.Checked = .BIT_25
                chkMedrek_26.Checked = .BIT_26
                chkMedrek_27.Checked = .BIT_27
                chkMedrek_28.Checked = .BIT_28
                chkMedrek_29.Checked = .BIT_29
                chkMedrek_30.Checked = .BIT_30
                chkMedrek_31.Checked = .BIT_31
                chkMedrek_32.Checked = .BIT_32
                chkMedrek_33.Checked = .BIT_33
                chkMedrek_34.Checked = .BIT_34
                chkMedrek_35.Checked = .BIT_35
                chkMedrek_36.Checked = .BIT_36
                chkMedrek_37.Checked = .BIT_37
                chkMedrek_38.Checked = .BIT_38
                chkMedrek_39.Checked = .BIT_39
                chkMedrek_40.Checked = .BIT_40
                chkMedrek_41.Checked = .BIT_41
                chkMedrek_42.Checked = .BIT_42
                chkMedrek_43.Checked = .BIT_43
                chkMedrek_44.Checked = .BIT_44
                chkMedrek_45.Checked = .BIT_45
                chkMedrek_46.Checked = .BIT_46
                chkMedrek_47.Checked = .BIT_47
                chkMedrek_48.Checked = .BIT_48
                chkMedrek_49.Checked = .BIT_49
                chkMedrek_50.Checked = .BIT_50
                chkMedrek_51.Checked = .BIT_51
                chkMedrek_52.Checked = .BIT_52
                chkMedrek_53.Checked = .BIT_53
                chkMedrek_54.Checked = .BIT_54
                chkMedrek_55.Checked = .BIT_55
                chkMedrek_56.Checked = .BIT_56
                chkMedrek_57.Checked = .BIT_57
                chkMedrek_58.Checked = .BIT_58
                chkMedrek_59.Checked = .BIT_59
                chkMedrek_60.Checked = .BIT_60
                chkMedrek_61.Checked = .BIT_61
                chkMedrek_62.Checked = .BIT_62
                chkMedrek_63.Checked = .BIT_63
                chkMedrek_64.Checked = .BIT_64
                chkMedrek_65.Checked = .BIT_65
                chkMedrek_66.Checked = .BIT_66
                chkMedrek_67.Checked = .BIT_67
                chkMedrek_68.Checked = .BIT_68
                chkMedrek_69.Checked = .BIT_69
                chkMedrek_70.Checked = .BIT_70
                chkMedrek_71.Checked = .BIT_71
                chkMedrek_72.Checked = .BIT_72
                chkMedrek_73.Checked = .BIT_73
                chkMedrek_74.Checked = .BIT_74
                chkMedrek_75.Checked = .BIT_75
                chkMedrek_76.Checked = .BIT_76
                chkMedrek_77.Checked = .BIT_77
                chkMedrek_78.Checked = .BIT_78
                chkMedrek_79.Checked = .BIT_79
                chkMedrek_80.Checked = .BIT_80
                chkMedrek_81.Checked = .BIT_81
                chkMedrek_82.Checked = .BIT_82
                chkMedrek_83.Checked = .BIT_83
                chkMedrek_84.Checked = .BIT_84
                chkMedrek_85.Checked = .BIT_85
                chkMedrek_86.Checked = .BIT_86
                chkMedrek_87.Checked = .BIT_87
                chkMedrek_88.Checked = .BIT_88
                chkMedrek_89.Checked = .BIT_89
                chkMedrek_90.Checked = .BIT_90
                chkMedrek_91.Checked = .BIT_91
                chkMedrek_92.Checked = .BIT_92
                chkMedrek_93.Checked = .BIT_93
                chkMedrek_94.Checked = .BIT_94
                chkMedrek_95.Checked = .BIT_95
                chkMedrek_96.Checked = .BIT_96
                chkMedrek_97.Checked = .BIT_97
                chkMedrek_98.Checked = .BIT_98
                chkMedrek_99.Checked = .BIT_99
                chkMedrek_100.Checked = .BIT_100
                chkMedrek_101.Checked = .BIT_101
                chkMedrek_102.Checked = .BIT_102
                chkMedrek_103.Checked = .BIT_103
                chkMedrek_104.Checked = .BIT_104
                chkMedrek_105.Checked = .BIT_105
                chkMedrek_106.Checked = .BIT_106
                chkMedrek_107.Checked = .BIT_107
                chkMedrek_108.Checked = .BIT_108
                chkMedrek_109.Checked = .BIT_109
                chkMedrek_110.Checked = .BIT_110
                chkMedrek_111.Checked = .BIT_111
                chkMedrek_112.Checked = .BIT_112
                chkMedrek_113.Checked = .BIT_113
                chkMedrek_114.Checked = .BIT_114
                chkMedrek_115.Checked = .BIT_115
                chkMedrek_116.Checked = .BIT_116
                chkMedrek_117.Checked = .BIT_117
                chkMedrek_118.Checked = .BIT_118
                chkMedrek_119.Checked = .BIT_119
                chkMedrek_120.Checked = .BIT_120
                chkMedrek_121.Checked = .BIT_121
                chkMedrek_122.Checked = .BIT_122
                chkMedrek_123.Checked = .BIT_123
                chkMedrek_124.Checked = .BIT_124
                chkMedrek_125.Checked = .BIT_125
                chkMedrek_126.Checked = .BIT_126
                chkMedrek_127.Checked = .BIT_127
                chkMedrek_128.Checked = .BIT_128
                chkMedrek_129.Checked = .BIT_129
                chkMedrek_130.Checked = .BIT_130
                chkMedrek_131.Checked = .BIT_131
                chkMedrek_132.Checked = .BIT_132
                chkMedrek_133.Checked = .BIT_133
                chkMedrek_134.Checked = .BIT_134
                chkMedrek_135.Checked = .BIT_135
                chkMedrek_136.Checked = .BIT_136
                chkMedrek_137.Checked = .BIT_137
                chkMedrek_138.Checked = .BIT_138
                chkMedrek_139.Checked = .BIT_139
                chkMedrek_140.Checked = .BIT_140
                chkMedrek_141.Checked = .BIT_141
                chkMedrek_142.Checked = .BIT_142
                chkMedrek_143.Checked = .BIT_143
                chkMedrek_144.Checked = .BIT_144
                chkMedrek_145.Checked = .BIT_145
                chkMedrek_146.Checked = .BIT_146
                chkMedrek_147.Checked = .BIT_147
                chkMedrek_148.Checked = .BIT_148
                chkMedrek_149.Checked = .BIT_149
                chkMedrek_150.Checked = .BIT_150
                chkMedrek_151.Checked = .BIT_151
                chkMedrek_152.Checked = .BIT_152
                chkMedrek_153.Checked = .BIT_153
                chkMedrek_154.Checked = .BIT_154
                chkMedrek_155.Checked = .BIT_155
                chkMedrek_156.Checked = .BIT_156
                chkMedrek_157.Checked = .BIT_157
                chkMedrek_158.Checked = .BIT_158
                chkMedrek_159.Checked = .BIT_159
                chkMedrek_160.Checked = .BIT_160
                chkMedrek_161.Checked = .BIT_161
                chkMedrek_162.Checked = .BIT_162
                chkMedrek_163.Checked = .BIT_163
                chkMedrek_164.Checked = .BIT_164
                chkMedrek_165.Checked = .BIT_165
                chkMedrek_166.Checked = .BIT_166
                chkMedrek_167.Checked = .BIT_167
                chkMedrek_168.Checked = .BIT_168
                chkMedrek_169.Checked = .BIT_169
                chkMedrek_170.Checked = .BIT_170
                chkMedrek_171.Checked = .BIT_171
                chkMedrek_172.Checked = .BIT_172
                chkMedrek_173.Checked = .BIT_173
                chkMedrek_174.Checked = .BIT_174
                chkMedrek_175.Checked = .BIT_175
                chkMedrek_176.Checked = .BIT_176
                chkMedrek_177.Checked = .BIT_177
                chkMedrek_178.Checked = .BIT_178
                chkMedrek_179.Checked = .BIT_179
                chkMedrek_180.Checked = .BIT_180
                chkMedrek_181.Checked = .BIT_181
                chkMedrek_182.Checked = .BIT_182
                chkMedrek_183.Checked = .BIT_183
                chkMedrek_184.Checked = .BIT_184
                chkMedrek_185.Checked = .BIT_185
                chkMedrek_186.Checked = .BIT_186
                chkMedrek_187.Checked = .BIT_187
                chkMedrek_188.Checked = .BIT_188
                chkMedrek_189.Checked = .BIT_189
                chkMedrek_190.Checked = .BIT_190
                chkMedrek_191.Checked = .BIT_191
                chkMedrek_192.Checked = .BIT_192
                chkMedrek_193.Checked = .BIT_193
                chkMedrek_194.Checked = .BIT_194
                chkMedrek_195.Checked = .BIT_195
                chkMedrek_196.Checked = .BIT_196
                chkMedrek_197.Checked = .BIT_197
                chkMedrek_198.Checked = .BIT_198
                chkMedrek_199.Checked = .BIT_199
                chkMedrek_200.Checked = .BIT_200
                chkMedrek_201.Checked = .BIT_201
                chkMedrek_202.Checked = .BIT_202
                chkMedrek_203.Checked = .BIT_203
                chkMedrek_204.Checked = .BIT_204
                chkMedrek_205.Checked = .BIT_205
                chkMedrek_206.Checked = .BIT_206
                chkMedrek_207.Checked = .BIT_207
                chkMedrek_208.Checked = .BIT_208
                chkMedrek_209.Checked = .BIT_209
                chkMedrek_210.Checked = .BIT_210
                chkMedrek_211.Checked = .BIT_211
                chkMedrek_212.Checked = .BIT_212
                chkMedrek_213.Checked = .BIT_213
                chkMedrek_214.Checked = .BIT_214
                chkMedrek_215.Checked = .BIT_215
                chkMedrek_216.Checked = .BIT_216
                chkMedrek_217.Checked = .BIT_217
                chkMedrek_218.Checked = .BIT_218
                chkMedrek_219.Checked = .BIT_219
                chkMedrek_220.Checked = .BIT_220
                chkMedrek_221.Checked = .BIT_221
                chkMedrek_222.Checked = .BIT_222
                chkMedrek_223.Checked = .BIT_223
                chkMedrek_224.Checked = .BIT_224
                chkMedrek_225.Checked = .BIT_225
                chkMedrek_226.Checked = .BIT_226
                chkMedrek_227.Checked = .BIT_227
                chkMedrek_228.Checked = .BIT_228
                chkMedrek_229.Checked = .BIT_229
                chkMedrek_230.Checked = .BIT_230
                chkMedrek_231.Checked = .BIT_231
                chkMedrek_232.Checked = .BIT_232
                chkMedrek_233.Checked = .BIT_233
                chkMedrek_234.Checked = .BIT_234
                chkMedrek_235.Checked = .BIT_235
                chkMedrek_236.Checked = .BIT_236
                chkMedrek_237.Checked = .BIT_237
                chkMedrek_238.Checked = .BIT_238
                chkMedrek_239.Checked = .BIT_239
                chkMedrek_240.Checked = .BIT_240
                chkMedrek_241.Checked = .BIT_241
                chkMedrek_242.Checked = .BIT_242
                chkMedrek_243.Checked = .BIT_243
                chkMedrek_244.Checked = .BIT_244
                chkMedrek_245.Checked = .BIT_245
                chkMedrek_246.Checked = .BIT_246
                chkMedrek_247.Checked = .BIT_247
                chkMedrek_248.Checked = .BIT_248
                chkMedrek_249.Checked = .BIT_249
                chkMedrek_250.Checked = .BIT_250
                chkMedrek_251.Checked = .BIT_251
                chkMedrek_252.Checked = .BIT_252
                chkMedrek_253.Checked = .BIT_253
                chkMedrek_254.Checked = .BIT_254
                chkMedrek_255.Checked = .BIT_255
                chkMedrek_256.Checked = .BIT_256
                chkMedrek_257.Checked = .BIT_257
                chkMedrek_258.Checked = .BIT_258
                txtMedrek_1.Text = .TEXT_1
                txtMedrek_2.Text = .TEXT_2
                cboTRIAGE_AIRWAY.Text = .TEXT_3
                cboTRIAGE_BREATHING.Text = .TEXT_4
                cboTRIAGE_CIRCULATION.Text = .TEXT_5
                txtMedrek_6.Text = .TEXT_6
                TimeEdit1.Time = .TEXT_7
                TimeEdit2.Time = .TEXT_8
                TimeEdit3.Time = .TEXT_9
                txtMedrek_10.Text = .TEXT_10
                txtMedrek_11.Text = .TEXT_11
                txtMedrek_12.Text = .TEXT_12
                txtMedrek_13.Text = .TEXT_13
                txtMedrek_14.Text = .TEXT_14
                txtMedrek_15.Text = .TEXT_15
                TimeEdit4.Time = .TEXT_16
                TimeEdit5.Time = .TEXT_17
                TimeEdit6.Time = .TEXT_18
                txtMedrek_19.Text = .TEXT_19
                txtMedrek_20.Text = .TEXT_20
                txtMedrek_21.Text = .TEXT_21
                txtMedrek_22.Text = .TEXT_22
                txtMedrek_23.Text = .TEXT_23
                txtMedrek_24.Text = .TEXT_24
                txtMedrek_25.Text = .TEXT_25
                txtMedrek_26.Text = .TEXT_26
                txtMedrek_27.Text = .TEXT_27
                txtMedrek_28.Text = .TEXT_28
                txtMedrek_29.Text = .TEXT_29
                TimeEdit7.Time = .TEXT_30
                TimeEdit8.Time = .TEXT_31
                TimeEdit9.Time = .TEXT_32
                txtMedrek_33.Text = .TEXT_33
                txtMedrek_34.Text = .TEXT_34
                txtMedrek_35.Text = .TEXT_35
                txtMedrek_36.Text = .TEXT_36
                txtMedrek_37.Text = .TEXT_37
                txtMedrek_38.Text = .TEXT_38
                txtMedrek_39.Text = .TEXT_39
                txtMedrek_40.Text = .TEXT_40
                txtMedrek_41.Text = .TEXT_41
                txtMedrek_42.Text = .TEXT_42
                txtMedrek_43.Text = .TEXT_43
                txtMedrek_44.Text = .TEXT_44
                txtMedrek_45.Text = .TEXT_45
                txtMedrek_46.Text = .TEXT_46
                txtMedrek_47.Text = .TEXT_47
                txtMedrek_48.Text = .TEXT_48
                txtMedrek_49.Text = .TEXT_49
                txtMedrek_50.Text = .TEXT_50
                txtMedrek_51.Text = .TEXT_51
                txtMedrek_52.Text = .TEXT_52
                txtMedrek_53.Text = .TEXT_53
                txtMedrek_54.Text = .TEXT_54
                txtMedrek_55.Text = .TEXT_55
                txtMedrek_56.Text = .TEXT_56
                txtMedrek_57.Text = .TEXT_57
                txtMedrek_58.Text = .TEXT_58
                txtMedrek_59.Text = .TEXT_59
                TimeEdit10.Time = .TEXT_60
                TimeEdit11.Time = .TEXT_61
                TimeEdit12.Time = .TEXT_62
                txt_GCS.Text = .TEXT_63
                cbo_E.Text = .TEXT_64
                cbo_M.Text = .TEXT_65
                cbo_V.Text = .TEXT_66
                txtMedrek_67.Text = .TEXT_67
                txtMedrek_68.Text = .TEXT_68
                txtMedrek_69.Text = .TEXT_69
                txtMedrek_70.Text = .TEXT_70
                txtMedrek_71.Text = .TEXT_71
                txtMedrek_72.Text = .TEXT_72
                txtMedrek_73.Text = .TEXT_73
                txtMedrek_74.Text = .TEXT_74
                txtMedrek_75.Text = .TEXT_75
                txtMedrek_76.Text = .TEXT_76
                txtMedrek_77.Text = .TEXT_77
                txtMedrek_78.Text = .TEXT_78
                TimeEdit13.Time = .TEXT_79
                TimeEdit14.Time = .TEXT_80
                TimeEdit15.Time = .TEXT_81
                txtMedrek_82.Text = .TEXT_82
                txtMedrek_83.Text = .TEXT_83
                cboSkalaNyeri.Text = .TEXT_84
                txtMedrek_85.Text = .TEXT_85
                txtMedrek_86.Text = .TEXT_86
                txtMedrek_87.Text = .TEXT_87
                txtMedrek_88.Text = .TEXT_88
                txtMedrek_89.Text = .TEXT_89
                TimeEdit16.Time = .TEXT_90
                TimeEdit17.Time = .TEXT_91
                TimeEdit18.Time = .TEXT_92
                txtMedrek_93.Text = .TEXT_93
                txtMedrek_94.Text = .TEXT_94
                txtMedrek_95.Text = .TEXT_95
                txtMedrek_96.Text = .TEXT_96
                txtMedrek_97.Text = .TEXT_97
                txtMedrek_98.Text = .TEXT_98
                txtMedrek_99.Text = .TEXT_99
                txtMedrek_100.Text = .TEXT_100
                txtMedrek_101.Text = .TEXT_101
                txtMedrek_102.Text = .TEXT_102
                txtMedrek_103.Text = .TEXT_103
                txtMedrek_104.Text = .TEXT_104
                txtMedrek_105.Text = .TEXT_105
                txtMedrek_106.Text = .TEXT_106
                txtMedrek_107.Text = .TEXT_107
                txtMedrek_108.Text = .TEXT_108

                txtKEADAAN.Text = .KEADAAN
                cboTINGKATKESADARAN.Text = .KESADARAN_UMUM
                txtTANDA_VITAL_01.Text = .TANDA_VITAL_01
                txtTANDA_VITAL_02.Text = .TANDA_VITAL_02
                txtTANDA_VITAL_03.Text = .TANDA_VITAL_03
                txtTANDA_VITAL_04.Text = .TANDA_VITAL_04
                txtTANDA_VITAL_05.Text = .TANDA_VITAL_05
                txtTANDA_VITAL_06.Text = .TANDA_VITAL_06
                txtTANDA_VITAL_07.Text = .TANDA_VITAL_07
                txtTANDA_VITAL_08.Text = .TANDA_VITAL_08
                txtTANDA_VITAL_09.Text = .TANDA_VITAL_09
                chkTANDA_VITAL_REGULER.Checked = .TANDA_VITAL_REGULER
                chkTANDA_VITAL_IREGULER.Checked = .TANDA_VITAL_IREGULER
                chkSTATUS_PSIKOLOGIS_01.Checked = .STATUS_PSIKOLOGIS_01
                chkSTATUS_PSIKOLOGIS_02.Checked = .STATUS_PSIKOLOGIS_02
                chkSTATUS_PSIKOLOGIS_03.Checked = .STATUS_PSIKOLOGIS_03
                chkSTATUS_PSIKOLOGIS_04.Checked = .STATUS_PSIKOLOGIS_04
                chkSTATUS_PSIKOLOGIS_05.Checked = .STATUS_PSIKOLOGIS_05
                chkSTATUS_PSIKOLOGIS_06.Checked = .STATUS_PSIKOLOGIS_06
                txtSTATUS_PSIKOLOGIS_06_TEXT.Text = .STATUS_PSIKOLOGIS_06_TEXT
                chkSTATUS_MENTAL_01.Checked = .STATUS_MENTAL_01
                chkSTATUS_MENTAL_02.Checked = .STATUS_MENTAL_02
                txtSTATUS_MENTAL_02_TEXT.Text = .STATUS_MENTAL_02_TEXT
                chkSTATUS_MENTAL_03.Checked = .STATUS_MENTAL_03
                txtSTATUS_MENTAL_03_TEXT.Text = .STATUS_MENTAL_03_TEXT
                chkSOSIAL_01_1.Checked = .SOSIAL_01_1
                chkSOSIAL_01_2.Checked = .SOSIAL_01_2
                chkSOSIAL_02_1.Checked = .SOSIAL_02_1
                chkSOSIAL_02_2.Checked = .SOSIAL_02_2
                chkSOSIAL_02_3.Checked = .SOSIAL_02_3
                chkSOSIAL_02_4.Checked = .SOSIAL_02_4
                txtSOSIAL_02_4_TEXT.Text = .SOSIAL_02_4_TEXT
                txtSOSIAL_03_1_TEXT.Text = .SOSIAL_03_1_TEXT
                txtSOSIAL_03_2_TEXT.Text = .SOSIAL_03_2_TEXT
                txtSOSIAL_03_3_TEXT.Text = .SOSIAL_03_3_TEXT
                txtSTATUS_SPIRITUAL_01_TEXT.Text = .STATUS_SPIRITUAL_01_TEXT
                txtSTATUS_SPIRITUAL_02_TEXT.Text = .STATUS_SPIRITUAL_02_TEXT
                'suku
                TextEdit1.Text = .TEXT_109
                'agama
                TextEdit2.Text = .TEXT_110

                Dim oUnit As New Reference.clsUnit
                Dim dsUnit = oUnit.GetDataByName(.KDUSER)
                If dsUnit IsNot Nothing Then
                    grdKDUSER.Text = dsUnit.KDUNIT
                Else
                    grdKDUSER.Text = .KDUSER
                End If

                txtRELOAD.Text = .KDUSER_SIGNATURE
                'Try
                '    'Dim img = (From x In oS_DIGITAL_IGD_01.GetData
                '    '           Where x.KDREG = txtNoRegister.Text
                '    '           Select x.ATTACHMENT_2).Single

                '    Dim img = oS_DIGITAL_IGD_02_NEW.GetData(sNoId).SIMPANGAMBAR_1

                '    picGambar2.Image = ByteArrayToImage(img.ToArray())
                'Catch oErr As Exception
                'End Try

                Try
                    txtSIMPANGAMBAR_1.Text = .SIMPANGAMBAR_1
                    Dim ImagePath As String = txtSIMPANGAMBAR_1.Text
                    Dim img1 As Bitmap
                    Dim newImage As Image = Image.FromFile(txtSIMPANGAMBAR_1.Text)

                    img1 = New Bitmap(ImagePath)
                    picGAMBAR2.ImageLocation = ImagePath

                    picGAMBAR2.Image = newImage
                Catch ex As Exception
                    sPicture = Nothing
                    MsgBox("Load List Data Gambar tidak ditemukan dialamat : " & .SIMPANGAMBAR_1, MsgBoxStyle.Exclamation, Me.Text)
                End Try

                deDATE.DateTime = .DATE

                BindingSource1.DataSource = oS_DIGITAL_IGD_02_NEW.GetDataDetail(sNoId).OrderBy(Function(x) x.SEQ).ToList()
                grdDetail.DataSource = BindingSource1
            End With
        Catch oErr As Exception
            MsgBox("Load List Data : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
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
            If txtNoRegister.Text = String.Empty Then
                MsgBox("Dibutuhkan Nomor Register", MsgBoxStyle.Exclamation, Me.Text)
                txtNoRegister.Focus()
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
            Dim ds = oS_DIGITAL_IGD_02_NEW.GetStructureHeader
            With ds
                Try
                    .DATECREATED = oS_DIGITAL_IGD_02_NEW.GetData(sNoId).DATECREATED
                Catch ex As Exception
                    .DATECREATED = Now
                End Try
                .DATEUPDATED = Now
                .DATE = deDATE.DateTime
                .KDPENDAFTARAN = txtNoRegister.Text
                .KDCUSTOMER = txtNoPasien.Text
                .NAMAPASIEN = txtNamaPasien.Text
                .JENISKELAMIN = sJENISKELAMIN
                .TANGGALLAHIR = sTANGGALLAHIR
                .ALAMAT = txtCATATANGAMBAR.Text
                .BIT_1 = chkMedrek_1.Checked
                .BIT_2 = chkMedrek_2.Checked
                .BIT_3 = chkMedrek_3.Checked
                .BIT_4 = chkMedrek_4.Checked
                .BIT_5 = chkMedrek_5.Checked
                .BIT_6 = chkMedrek_6.Checked
                .BIT_7 = chkMedrek_7.Checked
                .BIT_8 = chkMedrek_8.Checked
                .BIT_9 = chkMedrek_9.Checked
                .BIT_10 = chkMedrek_10.Checked
                .BIT_11 = chkMedrek_11.Checked
                .BIT_12 = chkMedrek_12.Checked
                .BIT_13 = chkMedrek_13.Checked
                .BIT_14 = chkMedrek_14.Checked
                .BIT_15 = chkMedrek_15.Checked
                .BIT_16 = chkMedrek_16.Checked
                .BIT_17 = chkMedrek_17.Checked
                .BIT_18 = chkMedrek_18.Checked
                .BIT_19 = chkMedrek_19.Checked
                .BIT_20 = chkMedrek_20.Checked
                .BIT_21 = chkMedrek_21.Checked
                .BIT_22 = chkMedrek_22.Checked
                .BIT_23 = chkMedrek_23.Checked
                .BIT_24 = chkMedrek_24.Checked
                .BIT_25 = chkMedrek_25.Checked
                .BIT_26 = chkMedrek_26.Checked
                .BIT_27 = chkMedrek_27.Checked
                .BIT_28 = chkMedrek_28.Checked
                .BIT_29 = chkMedrek_29.Checked
                .BIT_30 = chkMedrek_30.Checked
                .BIT_31 = chkMedrek_31.Checked
                .BIT_32 = chkMedrek_32.Checked
                .BIT_33 = chkMedrek_33.Checked
                .BIT_34 = chkMedrek_34.Checked
                .BIT_35 = chkMedrek_35.Checked
                .BIT_36 = chkMedrek_36.Checked
                .BIT_37 = chkMedrek_37.Checked
                .BIT_38 = chkMedrek_38.Checked
                .BIT_39 = chkMedrek_39.Checked
                .BIT_40 = chkMedrek_40.Checked
                .BIT_41 = chkMedrek_41.Checked
                .BIT_42 = chkMedrek_42.Checked
                .BIT_43 = chkMedrek_43.Checked
                .BIT_44 = chkMedrek_44.Checked
                .BIT_45 = chkMedrek_45.Checked
                .BIT_46 = chkMedrek_46.Checked
                .BIT_47 = chkMedrek_47.Checked
                .BIT_48 = chkMedrek_48.Checked
                .BIT_49 = chkMedrek_49.Checked
                .BIT_50 = chkMedrek_50.Checked
                .BIT_51 = chkMedrek_51.Checked
                .BIT_52 = chkMedrek_52.Checked
                .BIT_53 = chkMedrek_53.Checked
                .BIT_54 = chkMedrek_54.Checked
                .BIT_55 = chkMedrek_55.Checked
                .BIT_56 = chkMedrek_56.Checked
                .BIT_57 = chkMedrek_57.Checked
                .BIT_58 = chkMedrek_58.Checked
                .BIT_59 = chkMedrek_59.Checked
                .BIT_60 = chkMedrek_60.Checked
                .BIT_61 = chkMedrek_61.Checked
                .BIT_62 = chkMedrek_62.Checked
                .BIT_63 = chkMedrek_63.Checked
                .BIT_64 = chkMedrek_64.Checked
                .BIT_65 = chkMedrek_65.Checked
                .BIT_66 = chkMedrek_66.Checked
                .BIT_67 = chkMedrek_67.Checked
                .BIT_68 = chkMedrek_68.Checked
                .BIT_69 = chkMedrek_69.Checked
                .BIT_70 = chkMedrek_70.Checked
                .BIT_71 = chkMedrek_71.Checked
                .BIT_72 = chkMedrek_72.Checked
                .BIT_73 = chkMedrek_73.Checked
                .BIT_74 = chkMedrek_74.Checked
                .BIT_75 = chkMedrek_75.Checked
                .BIT_76 = chkMedrek_76.Checked
                .BIT_77 = chkMedrek_77.Checked
                .BIT_78 = chkMedrek_78.Checked
                .BIT_79 = chkMedrek_79.Checked
                .BIT_80 = chkMedrek_80.Checked
                .BIT_81 = chkMedrek_81.Checked
                .BIT_82 = chkMedrek_82.Checked
                .BIT_83 = chkMedrek_83.Checked
                .BIT_84 = chkMedrek_84.Checked
                .BIT_85 = chkMedrek_85.Checked
                .BIT_86 = chkMedrek_86.Checked
                .BIT_87 = chkMedrek_87.Checked
                .BIT_88 = chkMedrek_88.Checked
                .BIT_89 = chkMedrek_89.Checked
                .BIT_90 = chkMedrek_90.Checked
                .BIT_91 = chkMedrek_91.Checked
                .BIT_92 = chkMedrek_92.Checked
                .BIT_93 = chkMedrek_93.Checked
                .BIT_94 = chkMedrek_94.Checked
                .BIT_95 = chkMedrek_95.Checked
                .BIT_96 = chkMedrek_96.Checked
                .BIT_97 = chkMedrek_97.Checked
                .BIT_98 = chkMedrek_98.Checked
                .BIT_99 = chkMedrek_99.Checked
                .BIT_100 = chkMedrek_100.Checked
                .BIT_101 = chkMedrek_101.Checked
                .BIT_102 = chkMedrek_102.Checked
                .BIT_103 = chkMedrek_103.Checked
                .BIT_104 = chkMedrek_104.Checked
                .BIT_105 = chkMedrek_105.Checked
                .BIT_106 = chkMedrek_106.Checked
                .BIT_107 = chkMedrek_107.Checked
                .BIT_108 = chkMedrek_108.Checked
                .BIT_109 = chkMedrek_109.Checked
                .BIT_110 = chkMedrek_110.Checked
                .BIT_111 = chkMedrek_111.Checked
                .BIT_112 = chkMedrek_112.Checked
                .BIT_113 = chkMedrek_113.Checked
                .BIT_114 = chkMedrek_114.Checked
                .BIT_115 = chkMedrek_115.Checked
                .BIT_116 = chkMedrek_116.Checked
                .BIT_117 = chkMedrek_117.Checked
                .BIT_118 = chkMedrek_118.Checked
                .BIT_119 = chkMedrek_119.Checked
                .BIT_120 = chkMedrek_120.Checked
                .BIT_121 = chkMedrek_121.Checked
                .BIT_122 = chkMedrek_122.Checked
                .BIT_123 = chkMedrek_123.Checked
                .BIT_124 = chkMedrek_124.Checked
                .BIT_125 = chkMedrek_125.Checked
                .BIT_126 = chkMedrek_126.Checked
                .BIT_127 = chkMedrek_127.Checked
                .BIT_128 = chkMedrek_128.Checked
                .BIT_129 = chkMedrek_129.Checked
                .BIT_130 = chkMedrek_130.Checked
                .BIT_131 = chkMedrek_131.Checked
                .BIT_132 = chkMedrek_132.Checked
                .BIT_133 = chkMedrek_133.Checked
                .BIT_134 = chkMedrek_134.Checked
                .BIT_135 = chkMedrek_135.Checked
                .BIT_136 = chkMedrek_136.Checked
                .BIT_137 = chkMedrek_137.Checked
                .BIT_138 = chkMedrek_138.Checked
                .BIT_139 = chkMedrek_139.Checked
                .BIT_140 = chkMedrek_140.Checked
                .BIT_141 = chkMedrek_141.Checked
                .BIT_142 = chkMedrek_142.Checked
                .BIT_143 = chkMedrek_143.Checked
                .BIT_144 = chkMedrek_144.Checked
                .BIT_145 = chkMedrek_145.Checked
                .BIT_146 = chkMedrek_146.Checked
                .BIT_147 = chkMedrek_147.Checked
                .BIT_148 = chkMedrek_148.Checked
                .BIT_149 = chkMedrek_149.Checked
                .BIT_150 = chkMedrek_150.Checked
                .BIT_151 = chkMedrek_151.Checked
                .BIT_152 = chkMedrek_152.Checked
                .BIT_153 = chkMedrek_153.Checked
                .BIT_154 = chkMedrek_154.Checked
                .BIT_155 = chkMedrek_155.Checked
                .BIT_156 = chkMedrek_156.Checked
                .BIT_157 = chkMedrek_157.Checked
                .BIT_158 = chkMedrek_158.Checked
                .BIT_159 = chkMedrek_159.Checked
                .BIT_160 = chkMedrek_160.Checked
                .BIT_161 = chkMedrek_161.Checked
                .BIT_162 = chkMedrek_162.Checked
                .BIT_163 = chkMedrek_163.Checked
                .BIT_164 = chkMedrek_164.Checked
                .BIT_165 = chkMedrek_165.Checked
                .BIT_166 = chkMedrek_166.Checked
                .BIT_167 = chkMedrek_167.Checked
                .BIT_168 = chkMedrek_168.Checked
                .BIT_169 = chkMedrek_169.Checked
                .BIT_170 = chkMedrek_170.Checked
                .BIT_171 = chkMedrek_171.Checked
                .BIT_172 = chkMedrek_172.Checked
                .BIT_173 = chkMedrek_173.Checked
                .BIT_174 = chkMedrek_174.Checked
                .BIT_175 = chkMedrek_175.Checked
                .BIT_176 = chkMedrek_176.Checked
                .BIT_177 = chkMedrek_177.Checked
                .BIT_178 = chkMedrek_178.Checked
                .BIT_179 = chkMedrek_179.Checked
                .BIT_180 = chkMedrek_180.Checked
                .BIT_181 = chkMedrek_181.Checked
                .BIT_182 = chkMedrek_182.Checked
                .BIT_183 = chkMedrek_183.Checked
                .BIT_184 = chkMedrek_184.Checked
                .BIT_185 = chkMedrek_185.Checked
                .BIT_186 = chkMedrek_186.Checked
                .BIT_187 = chkMedrek_187.Checked
                .BIT_188 = chkMedrek_188.Checked
                .BIT_189 = chkMedrek_189.Checked
                .BIT_190 = chkMedrek_190.Checked
                .BIT_191 = chkMedrek_191.Checked
                .BIT_192 = chkMedrek_192.Checked
                .BIT_193 = chkMedrek_193.Checked
                .BIT_194 = chkMedrek_194.Checked
                .BIT_195 = chkMedrek_195.Checked
                .BIT_196 = chkMedrek_196.Checked
                .BIT_197 = chkMedrek_197.Checked
                .BIT_198 = chkMedrek_198.Checked
                .BIT_199 = chkMedrek_199.Checked
                .BIT_200 = chkMedrek_200.Checked
                .BIT_201 = chkMedrek_201.Checked
                .BIT_202 = chkMedrek_202.Checked
                .BIT_203 = chkMedrek_203.Checked
                .BIT_204 = chkMedrek_204.Checked
                .BIT_205 = chkMedrek_205.Checked
                .BIT_206 = chkMedrek_206.Checked
                .BIT_207 = chkMedrek_207.Checked
                .BIT_208 = chkMedrek_208.Checked
                .BIT_209 = chkMedrek_209.Checked
                .BIT_210 = chkMedrek_210.Checked
                .BIT_211 = chkMedrek_211.Checked
                .BIT_212 = chkMedrek_212.Checked
                .BIT_213 = chkMedrek_213.Checked
                .BIT_214 = chkMedrek_214.Checked
                .BIT_215 = chkMedrek_215.Checked
                .BIT_216 = chkMedrek_216.Checked
                .BIT_217 = chkMedrek_217.Checked
                .BIT_218 = chkMedrek_218.Checked
                .BIT_219 = chkMedrek_219.Checked
                .BIT_220 = chkMedrek_220.Checked
                .BIT_221 = chkMedrek_221.Checked
                .BIT_222 = chkMedrek_222.Checked
                .BIT_223 = chkMedrek_223.Checked
                .BIT_224 = chkMedrek_224.Checked
                .BIT_225 = chkMedrek_225.Checked
                .BIT_226 = chkMedrek_226.Checked
                .BIT_227 = chkMedrek_227.Checked
                .BIT_228 = chkMedrek_228.Checked
                .BIT_229 = chkMedrek_229.Checked
                .BIT_230 = chkMedrek_230.Checked
                .BIT_231 = chkMedrek_231.Checked
                .BIT_232 = chkMedrek_232.Checked
                .BIT_233 = chkMedrek_233.Checked
                .BIT_234 = chkMedrek_234.Checked
                .BIT_235 = chkMedrek_235.Checked
                .BIT_236 = chkMedrek_236.Checked
                .BIT_237 = chkMedrek_237.Checked
                .BIT_238 = chkMedrek_238.Checked
                .BIT_239 = chkMedrek_239.Checked
                .BIT_240 = chkMedrek_240.Checked
                .BIT_241 = chkMedrek_241.Checked
                .BIT_242 = chkMedrek_242.Checked
                .BIT_243 = chkMedrek_243.Checked
                .BIT_244 = chkMedrek_244.Checked
                .BIT_245 = chkMedrek_245.Checked
                .BIT_246 = chkMedrek_246.Checked
                .BIT_247 = chkMedrek_247.Checked
                .BIT_248 = chkMedrek_248.Checked
                .BIT_249 = chkMedrek_249.Checked
                .BIT_250 = chkMedrek_250.Checked
                .BIT_251 = chkMedrek_251.Checked
                .BIT_252 = chkMedrek_252.Checked
                .BIT_253 = chkMedrek_253.Checked
                .BIT_254 = chkMedrek_254.Checked
                .BIT_255 = chkMedrek_255.Checked
                .BIT_256 = chkMedrek_256.Checked
                .BIT_257 = chkMedrek_257.Checked
                .BIT_258 = chkMedrek_258.Checked
                .TEXT_1 = txtMedrek_1.Text.ToString.ToUpper.Trim
                .TEXT_2 = txtMedrek_2.Text.ToString.ToUpper.Trim
                .TEXT_3 = cboTRIAGE_AIRWAY.Text.ToString.ToUpper.Trim
                .TEXT_4 = cboTRIAGE_BREATHING.Text.ToString.ToUpper.Trim
                .TEXT_5 = cboTRIAGE_CIRCULATION.Text.ToString.ToUpper.Trim
                .TEXT_6 = txtMedrek_6.Text.ToString.ToUpper.Trim
                .TEXT_7 = TimeEdit1.Time
                .TEXT_8 = TimeEdit2.Time
                .TEXT_9 = TimeEdit3.Time
                .TEXT_10 = txtMedrek_10.Text.ToString.ToUpper.Trim
                .TEXT_11 = txtMedrek_11.Text.ToString.ToUpper.Trim
                .TEXT_12 = txtMedrek_12.Text.ToString.ToUpper.Trim
                .TEXT_13 = txtMedrek_13.Text.ToString.ToUpper.Trim
                .TEXT_14 = txtMedrek_14.Text.ToString.ToUpper.Trim
                .TEXT_15 = txtMedrek_15.Text.ToString.ToUpper.Trim
                .TEXT_16 = TimeEdit4.Time
                .TEXT_17 = TimeEdit5.Time
                .TEXT_18 = TimeEdit6.Time
                .TEXT_19 = txtMedrek_19.Text.ToString.ToUpper.Trim
                .TEXT_20 = txtMedrek_20.Text.ToString.ToUpper.Trim
                .TEXT_21 = txtMedrek_21.Text.ToString.ToUpper.Trim
                .TEXT_22 = txtMedrek_22.Text.ToString.ToUpper.Trim
                .TEXT_23 = txtMedrek_23.Text.ToString.ToUpper.Trim
                .TEXT_24 = txtMedrek_24.Text.ToString.ToUpper.Trim
                .TEXT_25 = txtMedrek_25.Text.ToString.ToUpper.Trim
                .TEXT_26 = txtMedrek_26.Text.ToString.ToUpper.Trim
                .TEXT_27 = txtMedrek_27.Text.ToString.ToUpper.Trim
                .TEXT_28 = txtMedrek_28.Text.ToString.ToUpper.Trim
                .TEXT_29 = txtMedrek_29.Text.ToString.ToUpper.Trim
                .TEXT_30 = TimeEdit7.Time
                .TEXT_31 = TimeEdit8.Time
                .TEXT_32 = TimeEdit9.Time
                .TEXT_33 = txtMedrek_33.Text.ToString.ToUpper.Trim
                .TEXT_34 = txtMedrek_34.Text.ToString.ToUpper.Trim
                .TEXT_35 = txtMedrek_35.Text.ToString.ToUpper.Trim
                .TEXT_36 = txtMedrek_36.Text.ToString.ToUpper.Trim
                .TEXT_37 = txtMedrek_37.Text.ToString.ToUpper.Trim
                .TEXT_38 = txtMedrek_38.Text.ToString.ToUpper.Trim
                .TEXT_39 = txtMedrek_39.Text.ToString.ToUpper.Trim
                .TEXT_40 = txtMedrek_40.Text.ToString.ToUpper.Trim
                .TEXT_41 = txtMedrek_41.Text.ToString.ToUpper.Trim
                .TEXT_42 = txtMedrek_42.Text.ToString.ToUpper.Trim
                .TEXT_43 = txtMedrek_43.Text.ToString.ToUpper.Trim
                .TEXT_44 = txtMedrek_44.Text.ToString.ToUpper.Trim
                .TEXT_45 = txtMedrek_45.Text.ToString.ToUpper.Trim
                .TEXT_46 = txtMedrek_46.Text.ToString.ToUpper.Trim
                .TEXT_47 = txtMedrek_47.Text.ToString.ToUpper.Trim
                .TEXT_48 = txtMedrek_48.Text.ToString.ToUpper.Trim
                .TEXT_49 = txtMedrek_49.Text.ToString.ToUpper.Trim
                .TEXT_50 = txtMedrek_50.Text.ToString.ToUpper.Trim
                .TEXT_51 = txtMedrek_51.Text.ToString.ToUpper.Trim
                .TEXT_52 = txtMedrek_52.Text.ToString.ToUpper.Trim
                .TEXT_53 = txtMedrek_53.Text.ToString.ToUpper.Trim
                .TEXT_54 = txtMedrek_54.Text.ToString.ToUpper.Trim
                .TEXT_55 = txtMedrek_55.Text.ToString.ToUpper.Trim
                .TEXT_56 = txtMedrek_56.Text.ToString.ToUpper.Trim
                .TEXT_57 = txtMedrek_57.Text.ToString.ToUpper.Trim
                .TEXT_58 = txtMedrek_58.Text.ToString.ToUpper.Trim
                .TEXT_59 = txtMedrek_59.Text.ToString.ToUpper.Trim
                .TEXT_60 = TimeEdit10.Time
                .TEXT_61 = TimeEdit11.Time
                .TEXT_62 = TimeEdit12.Time
                .TEXT_63 = txt_GCS.Text.ToString.ToUpper.Trim
                .TEXT_64 = cbo_E.Text.ToString.ToUpper.Trim
                .TEXT_65 = cbo_M.Text.ToString.ToUpper.Trim
                .TEXT_66 = cbo_V.Text.ToString.ToUpper.Trim
                .TEXT_67 = txtMedrek_67.Text.ToString.ToUpper.Trim
                .TEXT_68 = txtMedrek_68.Text.ToString.ToUpper.Trim
                .TEXT_69 = txtMedrek_69.Text.ToString.ToUpper.Trim
                .TEXT_70 = txtMedrek_70.Text.ToString.ToUpper.Trim
                .TEXT_71 = txtMedrek_71.Text.ToString.ToUpper.Trim
                .TEXT_72 = txtMedrek_72.Text.ToString.ToUpper.Trim
                .TEXT_73 = txtMedrek_73.Text.ToString.ToUpper.Trim
                .TEXT_74 = txtMedrek_74.Text.ToString.ToUpper.Trim
                .TEXT_75 = txtMedrek_75.Text.ToString.ToUpper.Trim
                .TEXT_76 = txtMedrek_76.Text.ToString.ToUpper.Trim
                .TEXT_77 = txtMedrek_77.Text.ToString.ToUpper.Trim
                .TEXT_78 = txtMedrek_78.Text.ToString.ToUpper.Trim
                .TEXT_79 = TimeEdit13.Time
                .TEXT_80 = TimeEdit14.Time
                .TEXT_81 = TimeEdit15.Time
                .TEXT_82 = txtMedrek_82.Text.ToString.ToUpper.Trim
                .TEXT_83 = txtMedrek_83.Text.ToString.ToUpper.Trim
                .TEXT_84 = cboSkalaNyeri.Text
                .TEXT_85 = txtMedrek_85.Text.ToString.ToUpper.Trim
                .TEXT_86 = txtMedrek_86.Text.ToString.ToUpper.Trim
                .TEXT_87 = txtMedrek_87.Text.ToString.ToUpper.Trim
                .TEXT_88 = txtMedrek_88.Text.ToString.ToUpper.Trim
                .TEXT_89 = txtMedrek_89.Text.ToString.ToUpper.Trim
                .TEXT_90 = TimeEdit16.Time
                .TEXT_91 = TimeEdit17.Time
                .TEXT_92 = TimeEdit18.Time
                .TEXT_93 = txtMedrek_93.Text.ToString.ToUpper.Trim
                .TEXT_94 = txtMedrek_94.Text.ToString.ToUpper.Trim
                .TEXT_95 = txtMedrek_95.Text.ToString.ToUpper.Trim
                .TEXT_96 = txtMedrek_96.Text.ToString.ToUpper.Trim
                .TEXT_97 = txtMedrek_97.Text.ToString.ToUpper.Trim
                .TEXT_98 = txtMedrek_98.Text.ToString.ToUpper.Trim
                .TEXT_99 = txtMedrek_99.Text.ToString.ToUpper.Trim
                .TEXT_100 = txtMedrek_100.Text.ToString.ToUpper.Trim
                .TEXT_101 = txtMedrek_101.Text.ToString.ToUpper.Trim
                .TEXT_102 = txtMedrek_102.Text.ToString.ToUpper.Trim
                .TEXT_103 = txtMedrek_103.Text.ToString.ToUpper.Trim
                .TEXT_104 = txtMedrek_104.Text.ToString.ToUpper.Trim
                .TEXT_105 = txtMedrek_105.Text.ToString.ToUpper.Trim
                .TEXT_106 = txtMedrek_106.Text.ToString.ToUpper.Trim
                .TEXT_107 = txtMedrek_107.Text.ToString.ToUpper.Trim
                .TEXT_108 = txtMedrek_108.Text.ToString.ToUpper.Trim
                .TEXT_109 = TextEdit1.Text
                .TEXT_110 = TextEdit2.Text

                .KEADAAN = txtKEADAAN.Text
                .KESADARAN_UMUM = cboTINGKATKESADARAN.Text
                .TANDA_VITAL_01 = txtTANDA_VITAL_01.Text
                .TANDA_VITAL_02 = txtTANDA_VITAL_02.Text
                .TANDA_VITAL_03 = txtTANDA_VITAL_03.Text
                .TANDA_VITAL_04 = txtTANDA_VITAL_04.Text
                .TANDA_VITAL_05 = txtTANDA_VITAL_05.Text
                .TANDA_VITAL_06 = txtTANDA_VITAL_06.Text
                .TANDA_VITAL_07 = txtTANDA_VITAL_07.Text
                .TANDA_VITAL_08 = txtTANDA_VITAL_08.Text
                .TANDA_VITAL_09 = txtTANDA_VITAL_09.Text
                .TANDA_VITAL_REGULER = chkTANDA_VITAL_REGULER.Checked
                .TANDA_VITAL_IREGULER = chkTANDA_VITAL_IREGULER.Checked
                .STATUS_PSIKOLOGIS_01 = chkSTATUS_PSIKOLOGIS_01.Checked
                .STATUS_PSIKOLOGIS_02 = chkSTATUS_PSIKOLOGIS_02.Checked
                .STATUS_PSIKOLOGIS_03 = chkSTATUS_PSIKOLOGIS_03.Checked
                .STATUS_PSIKOLOGIS_04 = chkSTATUS_PSIKOLOGIS_04.Checked
                .STATUS_PSIKOLOGIS_05 = chkSTATUS_PSIKOLOGIS_05.Checked
                .STATUS_PSIKOLOGIS_06 = chkSTATUS_PSIKOLOGIS_06.Checked
                .STATUS_PSIKOLOGIS_06_TEXT = txtSTATUS_PSIKOLOGIS_06_TEXT.Text
                .STATUS_MENTAL_01 = chkSTATUS_MENTAL_01.Checked
                .STATUS_MENTAL_02 = chkSTATUS_MENTAL_02.Checked
                .STATUS_MENTAL_02_TEXT = txtSTATUS_MENTAL_02_TEXT.Text
                .STATUS_MENTAL_03 = chkSTATUS_MENTAL_03.Checked
                .STATUS_MENTAL_03_TEXT = txtSTATUS_MENTAL_03_TEXT.Text
                .SOSIAL_01_1 = chkSOSIAL_01_1.Checked
                .SOSIAL_01_2 = chkSOSIAL_01_2.Checked
                .SOSIAL_02_1 = chkSOSIAL_02_1.Checked
                .SOSIAL_02_2 = chkSOSIAL_02_2.Checked
                .SOSIAL_02_3 = chkSOSIAL_02_3.Checked
                .SOSIAL_02_4 = chkSOSIAL_02_4.Checked
                .SOSIAL_02_4_TEXT = txtSOSIAL_02_4_TEXT.Text
                .SOSIAL_03_1_TEXT = txtSOSIAL_03_1_TEXT.Text
                .SOSIAL_03_2_TEXT = txtSOSIAL_03_2_TEXT.Text
                .SOSIAL_03_3_TEXT = txtSOSIAL_03_3_TEXT.Text
                .STATUS_SPIRITUAL_01_TEXT = txtSTATUS_SPIRITUAL_01_TEXT.Text
                .STATUS_SPIRITUAL_02_TEXT = txtSTATUS_SPIRITUAL_02_TEXT.Text

                'If txtSIMPANGAMBAR_1.Text <> "" Then
                '    If My.Computer.FileSystem.FileExists(txtSIMPANGAMBAR_1.Text) Then
                '        Dim SeqString As String = Microsoft.VisualBasic.Right(txtSIMPANGAMBAR_1.Text, 8)
                '        Dim Seq As String = CInt(Microsoft.VisualBasic.Right(txtSIMPANGAMBAR_1.Text.Replace(".jpg", ""), 4)) + 1

                '        picGAMBAR2.Image.Save(txtSIMPANGAMBAR_1.Text.Replace(SeqString, "") & Seq.PadLeft(4, "0") & ".jpg")

                '        .SIMPANGAMBAR_1 = txtSIMPANGAMBAR_1.Text.Replace(SeqString, "") & Seq.PadLeft(4, "0") & ".jpg"
                '    Else
                '        picGAMBAR2.Image.Save(txtSIMPANGAMBAR_1.Text & "IGD_" & txtNoRegister.Text & "0000.jpg")
                '        .SIMPANGAMBAR_1 = txtSIMPANGAMBAR_1.Text & "IGD_" & txtNoRegister.Text & "0000.jpg"
                '    End If
                'Else
                '    .SIMPANGAMBAR_1 = ""
                'End If

                If sPicture Is Nothing Then
                    Try
                        .SIMPANGAMBAR_1 = oS_DIGITAL_IGD_02_NEW.GetData(sNoId).SIMPANGAMBAR_1
                    Catch ex As Exception
                        .SIMPANGAMBAR_1 = ""
                    End Try
                Else
                    Dim sALAMATSIMPAN As String = String.Empty
                    Dim dsAlamat = oS_DIGITAL_IGD_02_NEW.GetDataAlamatSimpan()
                    If dsAlamat IsNot Nothing Then
                        sALAMATSIMPAN = dsAlamat.ALAMAT_SERVER
                    End If

                    Dim Alamat As String = sALAMATSIMPAN & Now.ToString("ddMMyyyyHHmm") & "." & txtNoRegister.Text & ".jpg"
                    picGAMBAR2.Image.Save(Alamat, System.Drawing.Imaging.ImageFormat.Jpeg)
                    .SIMPANGAMBAR_1 = Alamat
                End If

                'Try
                '    Dim ms As New IO.MemoryStream()
                '    picGambar2.Image.Save(ms, picGambar2.Image.RawFormat)

                '    Dim data As Byte() = ms.GetBuffer()

                '    .SIMPANGAMBAR_1 = data
                'Catch oErr As Exception
                '    Try
                '        .SIMPANGAMBAR_1 = oS_DIGITAL_IGD_01.GetData(txtNoRegister.Text).ATTACHMENT_2
                '    Catch ex As Exception

                '    End Try
                'End Try
                Try
                    .CETAK = oS_DIGITAL_IGD_02_NEW.GetData(sNoId).CETAK
                Catch ex As Exception
                    .CETAK = 0
                End Try

                .KDUSER = grdKDUSER.Text
                .KDUSER_SIGNATURE = txtRELOAD.Text
            End With

            ' ***** DETIL *****
            Dim arrDetail = oS_DIGITAL_IGD_02_NEW.GetStructureDetailList
            For i As Integer = 0 To grvDetail.RowCount - 2
                Dim dsDetail = oS_DIGITAL_IGD_02_NEW.GetStructureDetail
                With dsDetail
                    .SEQ = i
                    .KDPENDAFTARAN = ds.KDPENDAFTARAN
                    .PUKUL = CDate(grvDetail.GetRowCellValue(i, colPUKUL))
                    .PENANGANAN = grvDetail.GetRowCellValue(i, colPENANGANAN)
                    .NAMA = IIf(String.IsNullOrEmpty(grvDetail.GetRowCellValue(i, colNAMA)), "-", grvDetail.GetRowCellValue(i, colNAMA))
                    .PARAF = IIf(String.IsNullOrEmpty(grvDetail.GetRowCellValue(i, colPARAF)), "-", grvDetail.GetRowCellValue(i, colPARAF))
                End With
                arrDetail.Add(dsDetail)
            Next

            If oFormMode = FORM_MODE.FORM_MODE_ADD Then
                Try
                    fn_Save = oS_DIGITAL_IGD_02_NEW.InsertData(ds, arrDetail)
                    If fn_Save = True Then
                        fn_UpdateNoAntrianPerawat(ds.KDPENDAFTARAN)
                    End If
                Catch ex As Exception
                    MsgBox("Simpan Data : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            ElseIf oFormMode = FORM_MODE.FORM_MODE_EDIT Then
                Try
                    fn_Save = oS_DIGITAL_IGD_02_NEW.UpdateData(ds, arrDetail)
                Catch ex As Exception
                    MsgBox("Simpan Data : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            End If

            If fn_Save = True Then
                frmReqAwalPemeriksaan.fn_UpdateNoAntrianPerawat(ds.KDPENDAFTARAN)
                If txtRELOAD.Text <> "" Then
                    frmEMedrekIDG_01.fn_LoadUpdateBed(ds.KDPENDAFTARAN, txtRELOAD.Text)
                End If
            End If
        Catch oErr As Exception
            MsgBox("Simpan Data : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            fn_Save = False
        End Try
    End Function
    Private Sub fn_UpdateNoAntrianPerawat(ByVal KDREG As String)
        Try
            Dim sANTRIANIGD As String = String.Empty
            Dim oCounter As New Setting.clsCounter
            Dim sMODUL As String = "IGDANTRIAN"
            Dim sLASTNUMBER As Integer = oCounter.GetLastNumberdDay(sMODUL, deDATE.DateTime)
            If sLASTNUMBER = 0 Then
                Try
                    oCounter.InsertData(sMODUL, deDATE.DateTime)
                    sLASTNUMBER = oCounter.GetLastNumber(sMODUL, deDATE.DateTime)
                Catch ex As Exception
                    sLASTNUMBER = 0
                End Try

                sANTRIANIGD = sLASTNUMBER + 1
                oCounter.UpdateData(sMODUL, sLASTNUMBER + 1, CInt(deDATE.DateTime.ToString("dd")), Month(deDATE.DateTime), Year(deDATE.DateTime))
            Else
                sANTRIANIGD = sLASTNUMBER + 1
                oCounter.UpdateData(sMODUL, sLASTNUMBER + 1, CInt(deDATE.DateTime.ToString("dd")), Month(deDATE.DateTime), Year(deDATE.DateTime))
            End If

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

            SQL = "UPDATE S_PENDAFTARAN_H "
            SQL &= "SET ANTRIANDOKTER = '" & sANTRIANIGD & "' "
            SQL &= "WHERE KDREG = '" & KDREG & "' "

            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "updateantrian")

            If oConn.State = ConnectionState.Open Then
                oConn.Close()
            End If

        Catch oErr As Exception
            MsgBox("Update Antrian Pasien Data : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
#End Region
#Region "Grid Method"
    Private Sub picGAMBAR2_MouseDown(sender As Object, e As MouseEventArgs)
        down = True
    End Sub
    Private Sub picGAMBAR2_MouseUp(sender As Object, e As MouseEventArgs)
        down = False
    End Sub
    Private Sub picGAMBAR2_MouseMove(sender As Object, e As MouseEventArgs)
        If down = True Then
            Dim ImageToDrawOn As Image
            Dim g As Graphics
            Dim Brush1 As New SolidBrush(Color.Black)
            g = picGAMBAR2.CreateGraphics()
            ImageToDrawOn = picGAMBAR2.Image
            g = Graphics.FromImage(ImageToDrawOn)
            g.FillEllipse(Brush1, e.X, e.Y, 5, 5)
            picGAMBAR2.Image = ImageToDrawOn
            g.Dispose()
            Brush1.Dispose()
        End If

    End Sub
    Private Sub ResetGambarToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ResetGambarToolStripMenuItem.Click
        picGAMBAR2.Image = CType(My.Resources.ResourceManager.GetObject("gambar"), Image)
    End Sub
    Private Sub DeleteToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DeleteToolStripMenuItem.Click
        grvDetail.DeleteSelectedRows()
    End Sub
#End Region
#Region "Command Button"
    Private Sub frm_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        Select Case e.KeyCode
            Case Keys.F12
                btnClose_Click()
            Case Keys.F3
                If btnSaveClose.Enabled = True Then
                    btnSaveClose_Click()
                End If
        End Select
    End Sub
    Private Sub btnClose_Click() Handles btnClose.ItemClick
        Me.Close()
    End Sub
    Private Sub btnSaveClose_Click() Handles btnSaveClose.ItemClick
        If fn_Validate() = False Then Exit Sub
        If MsgBox("Save " & txtNoRegister.Text.Trim.ToUpper & "?", MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, Me.Text) = MsgBoxResult.No Then Exit Sub
        If fn_Save() = False Then
            MsgBox("Save gagal! Tolong kontak admin anda!", MsgBoxStyle.Exclamation, Me.Text)
        Else
            MsgBox("Save " & txtNoRegister.Text.Trim.ToUpper & " success!", MsgBoxStyle.Information, Me.Text)
            Me.Close()
        End If
    End Sub

#End Region
#Region "Lookup / Event"
    Private Sub fn_LoadKDUSER()
        Dim oTemplate As New Reference.clsUnit
        Try
            grdKDUSER.Properties.DataSource = oTemplate.GetData.Where(Function(x) x.ISACTIVE = True).ToList()
            grdKDUSER.Properties.ValueMember = "KDUNIT"
            grdKDUSER.Properties.DisplayMember = "MEMO"
        Catch oErr As Exception
            MsgBox("Load Template Unit Data : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    'Private Sub fn_LoadWAREHOUSE()
    '    Dim oWarehouse As New Master.clsWarehouse
    '    Try
    '        If isIntern = True Then
    '            grdWAREHOUSE.Properties.DataSource = oWarehouse.GetData.Where(Function(x) x.ISACTIVE = True).ToList()
    '        Else
    '            grdWAREHOUSE.Properties.DataSource = oWarehouse.GetData.Where(Function(x) x.ISACTIVE = True).ToList()
    '        End If
    '        grdWAREHOUSE.Properties.ValueMember = "KDWAREHOUSE"
    '        grdWAREHOUSE.Properties.DisplayMember = "NAME_DISPLAY"
    '    Catch oErr As Exception
    '        MsgBox("Load Gudang Data : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
    '    End Try
    'End Sub
    Private Sub cboSAATPULANG_E_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbo_E.SelectedIndexChanged, cbo_M.SelectedIndexChanged, cbo_V.SelectedIndexChanged
        If isLoad = True Then
            Dim sE As Decimal = 0
            Dim sM As Decimal = 0
            Dim sV As Decimal = 0

            If cbo_E.Text <> String.Empty Then
                sE = CDec(cbo_E.Text)
            End If

            If cbo_M.Text <> String.Empty Then
                sM = CDec(cbo_M.Text)
            End If

            If cbo_V.Text <> String.Empty Then
                sV = CDec(cbo_V.Text)
            End If

            txt_GCS.Text = sE + sM + sV

        End If
    End Sub
#End Region
#Region "Handles"
    Private Sub chkKENDARAAN_Click(sender As Object, e As EventArgs) Handles chkMedrek_1.Click, chkMedrek_2.Click, chkMedrek_3.Click
        chkMedrek_1.Checked = False
        chkMedrek_2.Checked = False
        chkMedrek_3.Checked = False
    End Sub
    Private Sub chkTRIAGE_Click(sender As Object, e As EventArgs) Handles chkMedrek_6.Click, chkMedrek_7.Click, chkMedrek_8.Click, chkMedrek_9.Click
        chkMedrek_6.Checked = False
        chkMedrek_7.Checked = False
        chkMedrek_8.Checked = False
        chkMedrek_9.Checked = False
    End Sub
    Private Sub chkAIRWAY_JALANAFAS_Click(sender As Object, e As EventArgs) Handles chkMedrek_10.Click, chkMedrek_11.Click
        chkMedrek_10.Checked = False
        chkMedrek_11.Checked = False
    End Sub
    Private Sub chkAIRWAY_SUARANAFAS_Click(sender As Object, e As EventArgs) Handles chkMedrek_17.Click, chkMedrek_18.Click
        chkMedrek_17.Checked = False
        chkMedrek_18.Checked = False
    End Sub
    Private Sub chkBREATING_SUARANAFAS_Click(sender As Object, e As EventArgs) Handles chkMedrek_43.Click, chkMedrek_44.Click, chkMedrek_45.Click
        chkMedrek_43.Checked = False
        chkMedrek_44.Checked = False
        chkMedrek_45.Checked = False
    End Sub
    Private Sub chkBREATING_DISTRESPERNAFASAN_Click(sender As Object, e As EventArgs) Handles chkMedrek_46.Click, chkMedrek_47.Click, chkMedrek_48.Click
        chkMedrek_46.Checked = False
        chkMedrek_47.Checked = False
        chkMedrek_48.Checked = False
    End Sub
    Private Sub chkBREATING_JENISPERNAFASAN_Click(sender As Object, e As EventArgs) Handles chkMedrek_46.Click, chkMedrek_47.Click, chkMedrek_48.Click
        chkMedrek_49.Checked = False
        chkMedrek_50.Checked = False
        chkMedrek_51.Checked = False
    End Sub
    Private Sub chkCIRCULATION_AKRAL_Click(sender As Object, e As EventArgs) Handles chkMedrek_87.Click, chkMedrek_88.Click
        chkMedrek_87.Checked = False
        chkMedrek_88.Checked = False
    End Sub
    Private Sub chkCIRCULATION_PUCAT_Click(sender As Object, e As EventArgs) Handles chkMedrek_89.Click, chkMedrek_90.Click
        chkMedrek_89.Checked = False
        chkMedrek_90.Checked = False
    End Sub
    Private Sub chkCIRCULATION_KEBIRUAN_Click(sender As Object, e As EventArgs) Handles chkMedrek_91.Click, chkMedrek_92.Click
        chkMedrek_91.Checked = False
        chkMedrek_92.Checked = False
    End Sub
    Private Sub chkCIRCULATION_PENGISIANKAPILER_Click(sender As Object, e As EventArgs) Handles chkMedrek_93.Click, chkMedrek_94.Click
        chkMedrek_93.Checked = False
        chkMedrek_94.Checked = False
    End Sub
    Private Sub chkCIRCULATION_NADI_Click(sender As Object, e As EventArgs) Handles chkMedrek_95.Click, chkMedrek_96.Click
        chkMedrek_95.Checked = False
        chkMedrek_96.Checked = False
    End Sub
    Private Sub chkCIRCULATION_IRAMANADI_Click(sender As Object, e As EventArgs) Handles chkMedrek_97.Click, chkMedrek_98.Click
        chkMedrek_97.Checked = False
        chkMedrek_98.Checked = False
    End Sub
    Private Sub chkCIRCULATION_KEKUATAN_Click(sender As Object, e As EventArgs) Handles chkMedrek_99.Click, chkMedrek_100.Click
        chkMedrek_99.Checked = False
        chkMedrek_100.Checked = False
    End Sub
    Private Sub chkCIRCULATION_KELEMBABANKULIT_Click(sender As Object, e As EventArgs) Handles chkMedrek_101.Click, chkMedrek_102.Click
        chkMedrek_101.Checked = False
        chkMedrek_102.Checked = False
    End Sub
    Private Sub chkCIRCULATION_TURGORKULIT_Click(sender As Object, e As EventArgs) Handles chkMedrek_103.Click, chkMedrek_104.Click
        chkMedrek_103.Checked = False
        chkMedrek_104.Checked = False
    End Sub
    Private Sub chkDISBILITY_KESADARAN_Click(sender As Object, e As EventArgs) Handles chkMedrek_1.Click, chkMedrek_2.Click, chkMedrek_3.Click, chkMedrek_4.Click
        chkMedrek_1.Checked = False
        chkMedrek_2.Checked = False
        chkMedrek_3.Checked = False
        chkMedrek_4.Checked = False
    End Sub
    Private Sub chkDISBILITY_PUPIL_Click(sender As Object, e As EventArgs) Handles chkMedrek_1.Click, chkMedrek_2.Click
        chkMedrek_1.Checked = False
        chkMedrek_2.Checked = False
    End Sub
    Private Sub chkDISBILITY_REFLEKCAHAYA_Click(sender As Object, e As EventArgs) Handles chkMedrek_1.Click, chkMedrek_2.Click
        chkMedrek_1.Checked = False
        chkMedrek_2.Checked = False
    End Sub
    Private Sub chkDISBILITY_MOTORIK_Click(sender As Object, e As EventArgs) Handles chkMedrek_1.Click, chkMedrek_2.Click
        chkMedrek_1.Checked = False
        chkMedrek_2.Checked = False
    End Sub
    Private Sub chkDISBILITY_SENSORIK_Click(sender As Object, e As EventArgs) Handles chkMedrek_1.Click, chkMedrek_2.Click
        chkMedrek_1.Checked = False
        chkMedrek_2.Checked = False
    End Sub
    Private Sub chkEKSPOSUR_TRAUMALUKA_Click(sender As Object, e As EventArgs) Handles chkMedrek_1.Click, chkMedrek_2.Click
        chkMedrek_1.Checked = False
        chkMedrek_2.Checked = False
    End Sub
    Private Sub chkEKSPOSUR_NYERI_Click(sender As Object, e As EventArgs) Handles chkMedrek_1.Click, chkMedrek_2.Click
        chkMedrek_1.Checked = False
        chkMedrek_2.Checked = False
    End Sub
    Private Sub chkRESIKO_PASIENJATUH_Click(sender As Object, e As EventArgs) Handles chkMedrek_1.Click, chkMedrek_2.Click
        chkMedrek_1.Checked = False
        chkMedrek_2.Checked = False
    End Sub
    Private Sub SimpleButton2_Click(sender As Object, e As EventArgs)
        'Dim oSignature As New Master.clsSignature
        'Dim dsSignature = oSignature.GetDataByNOIDUSER(sUserID)
        'If dsSignature IsNot Nothing Then
        '    Dim oDoctor2 As New Master.clsDoctor2
        '    Dim dsDoctor2 = oDoctor2.GetData(dsSignature.KDDOCTOR2)
        '    If dsDoctor2 Is Nothing Then
        '        MsgBox("Tidak terdaftar sebagai Perawat", MsgBoxStyle.Information, Me.Text)
        '    Else
        '        txtMedrek_110.Text = dsSignature.KDSIGNATURE
        '        txtMedrek_109.Text = dsSignature.NAMA
        '    End If
        'Else
        '    MsgBox("Kode Digital Tidak ditemukan, silahkan didaftarkan terlebih dahulu", MsgBoxStyle.Information, Me.Text)
        'End If
    End Sub
    Private Sub btnEDITIMAGE_Click(sender As Object, e As EventArgs) Handles btnEDITIMAGE.Click
        Dim frmPopUp_Image As New frmPopUp_img
        frmPopUp_Image.fn_LoadMe(picGAMBAR2.Image)
        frmPopUp_Image.ShowDialog()

        If sPicture IsNot Nothing Then
            picGAMBAR2.Image = sPicture
        End If

        picGAMBAR2.Focus()
    End Sub
    Private Sub fn_LoadDataReloadIGD()
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

            Dim Tanggal As DateTime = deDATE.DateTime.AddDays(-1)

            SQL = "SELECT "
            SQL &= "A.KDAWALASESMENIGD "
            SQL &= ",A.NAMAPASIEN "
            SQL &= ",A.KDUSER "
            SQL &= "FROM "
            SQL &= "S_DIGITAL_IGD_01_AWAL A "
            SQL &= "WHERE CONVERT(VARCHAR(8), A.DATE, 112) >= '" & Tanggal.ToString("yyyyMMdd") & "' "
            'SQL &= "AND A.KDPENDAFTARAN = '' "
            SQL &= "ORDER BY A.DATE DESC "

            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "ALL")

            grdReloadIGD.Properties.DataSource = ds.Tables("ALL")
            grdReloadIGD.Properties.ValueMember = "KDAWALASESMENIGD"
            grdReloadIGD.Properties.DisplayMember = "NAMAPASIEN"

            If oConn.State = ConnectionState.Open Then
                oConn.Close()
            End If
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub SimpleButton2_Click_1(sender As Object, e As EventArgs) Handles SimpleButton2.Click
        fn_LoadDataReloadIGD()
        grdReloadIGD.ShowPopup()
    End Sub
    Private Sub grdReloadIGD_EditValueChanged(sender As Object, e As EventArgs) Handles grdReloadIGD.EditValueChanged
        If grdReloadIGD.Text <> "" Then
            Try
                txtRELOAD.Text = grdReloadIGD.EditValue

                Dim oDIGITAL_IGD_01_AWAL As New Transaksi.clsDIGITAL_IGD_01_AWAL
                ' ***** HEADER *****
                Dim ds = oDIGITAL_IGD_01_AWAL.GetData(grdReloadIGD.EditValue)

                With ds
                    deDATE.DateTime = .DATE
                    txtNamaPasien.Text = .NAMAPASIEN
                    txtMedrek_97.Text = .RIWAYAT
                    'txtRIWAYAT_ALERGI.Text = .ALERGI
                    txtMedrek_6.Text = .ANAMNESIS
                    cboTINGKATKESADARAN.Text = .TINGKATKESADARAN
                    'txtGCS.Text = .GCS
                    'cboE.Text = .GCS_E
                    'cboM.Text = .GCS_M
                    'cboV.Text = .GCS_V
                    txtKEADAAN.Text = .KEADAANUMUM
                    txtTANDA_VITAL_05.Text = .BERATBADAN
                    txtTANDA_VITAL_06.Text = .TINGGIBADAN
                    txtTANDA_VITAL_01.Text = .BP
                    txtTANDA_VITAL_02.Text = .HR
                    txtTANDA_VITAL_04.Text = .RR
                    txtTANDA_VITAL_03.Text = .T
                    txtTANDA_VITAL_09.Text = .SP02

                    chkMedrek_1.Checked = .KENDARAAN_1
                    chkMedrek_2.Checked = .KENDARAAN_2
                    chkMedrek_3.Checked = .KENDARAAN_3
                    chkMedrek_4.Checked = .DATANG_1
                    chkMedrek_5.Checked = .DATANG_2

                    txtMedrek_1.Text = .KENDARAAN_TEX
                    txtMedrek_2.Text = .DATANG_TEXT

                    chkMedrek_6.Checked = .TRIAGE_1
                    chkMedrek_7.Checked = .TRIAGE_2
                    chkMedrek_8.Checked = .TRIAGE_3
                    chkMedrek_9.Checked = .TRIAGE_4

                    cboTRIAGE_AIRWAY.Text = .TRIAGE_AIRWAY
                    cboTRIAGE_BREATHING.Text = .TRIAGE_BREATHING
                    cboTRIAGE_CIRCULATION.Text = .TRIAGE_CIRCULATION

                    'BindingSourceTindakanPoli.DataSource = oDIGITAL_IGD_01_AWAL.GetDataDetail(.KDAWALASESMENIGD).OrderBy(Function(x) x.SEQ).ToList()
                    'grdTindakanPoli.DataSource = BindingSourceTindakanPoli

                    'BindingSourcePenunjang.DataSource = oDIGITAL_IGD_01_AWAL.GetDataDetail_P(.KDAWALASESMENIGD).OrderBy(Function(x) x.SEQ).ToList()
                    'grdTindakan.DataSource = BindingSourcePenunjang
                End With
            Catch oErr As Exception
                MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            End Try
        End If
    End Sub
#End Region
End Class