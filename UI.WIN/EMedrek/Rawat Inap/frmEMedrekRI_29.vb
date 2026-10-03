Imports DataAccess
Imports UI.WIN.MAIN.My.Resources

Public Class frmEMedrekRI_29
#Region "Declaration"
    Private oFormMode As FORM_MODE = FORM_MODE.FORM_MODE_VIEW
    Private isLoad As Boolean = False
    Private oS_DIGITAL_RI_29 As New Digital.clsS_DIGITAL_RI_29
    Private down As Boolean = False
    Private sKDUSER_PERAWAT As String = String.Empty

#End Region
#Region "Function"
    Public Sub LoadMe(ByVal FormMode As Integer, ByVal KDREG As String, ByVal KDCUSTOMER As String, ByVal NAMAPASIEN As String, ByVal JENISKELAMIN As String, ByVal KDUSER_PERAWAT As String, ByVal DPJP As String)
        oFormMode = FormMode

        txtNoRegister.Text = KDREG
        txtNoPasien.Text = KDCUSTOMER
        txtNamaPasien.Text = NAMAPASIEN
        txtUmur.Text = JENISKELAMIN
        txtDokter.Text = DPJP
        sKDUSER_PERAWAT = KDUSER_PERAWAT
    End Sub
    Private Sub Form_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        fn_ChangeFormState()
        isLoad = True
    End Sub
    Private Sub Form_Closing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        Dispose()
        sCode = txtNoRegister.Text.Trim.ToUpper
    End Sub
    Private Overloads Sub Dispose()
        MyBase.Dispose()
        Me.Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
    Private Sub fn_ChangeFormState()
        fn_NOIDUSER()

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
        deDATE.Properties.ReadOnly = Status
        'txtMANAJER.Properties.ReadOnly = Status

        chkASESMEN_01.Properties.ReadOnly = Status
        chkASESMEN_02.Properties.ReadOnly = Status
        chkASESMEN_03.Properties.ReadOnly = Status
        chkASESMEN_04.Properties.ReadOnly = Status
        chkASESMEN_05.Properties.ReadOnly = Status
        txtASESMEN_06.Properties.ReadOnly = Status
        txtASESMEN_07.Properties.ReadOnly = Status
        txtASESMEN_08.Properties.ReadOnly = Status
        txtASESMEN_09.Properties.ReadOnly = Status
        txtASESMEN_10.Properties.ReadOnly = Status
        chkASESMEN_11.Properties.ReadOnly = Status
        chkASESMEN_12.Properties.ReadOnly = Status
        chkASESMEN_13.Properties.ReadOnly = Status
        chkASESMEN_14.Properties.ReadOnly = Status
        chkASESMEN_15.Properties.ReadOnly = Status
        chkASESMEN_16.Properties.ReadOnly = Status
        txtASESMEN_17.Properties.ReadOnly = Status
        txtASESMEN_18.Properties.ReadOnly = Status
        txtASESMEN_19.Properties.ReadOnly = Status
        txtASESMEN_20.Properties.ReadOnly = Status
        txtASESMEN_21.Properties.ReadOnly = Status
        txtASESMEN_22.Properties.ReadOnly = Status
        txtASESMEN_23.Properties.ReadOnly = Status
        txtASESMEN_24.Properties.ReadOnly = Status
        chkASESMEN_25.Properties.ReadOnly = Status
        chkASESMEN_26.Properties.ReadOnly = Status
        chkASESMEN_27.Properties.ReadOnly = Status
        chkASESMEN_28.Properties.ReadOnly = Status
        txtASESMEN_29.Properties.ReadOnly = Status
        chkASESMEN_30.Properties.ReadOnly = Status
        chkASESMEN_31.Properties.ReadOnly = Status
        chkASESMEN_32.Properties.ReadOnly = Status
        chkASESMEN_33.Properties.ReadOnly = Status
        chkASESMEN_34.Properties.ReadOnly = Status
        chkASESMEN_35.Properties.ReadOnly = Status
        txtASESMEN_36.Properties.ReadOnly = Status
        chkASESMEN_37.Properties.ReadOnly = Status
        chkASESMEN_38.Properties.ReadOnly = Status
        txtASESMEN_39.Properties.ReadOnly = Status
        chkASESMEN_40.Properties.ReadOnly = Status
        chkASESMEN_41.Properties.ReadOnly = Status
        txtASESMEN_42.Properties.ReadOnly = Status
        chkASESMEN_43.Properties.ReadOnly = Status
        chkASESMEN_44.Properties.ReadOnly = Status
        chkASESMEN_45.Properties.ReadOnly = Status
        chkASESMEN_46.Properties.ReadOnly = Status
        chkASESMEN_47.Properties.ReadOnly = Status
        chkASESMEN_48.Properties.ReadOnly = Status
        chkASESMEN_49.Properties.ReadOnly = Status
        chkASESMEN_50.Properties.ReadOnly = Status
        chkASESMEN_51.Properties.ReadOnly = Status
        chkASESMEN_52.Properties.ReadOnly = Status
        chkASESMEN_53.Properties.ReadOnly = Status
        chkASESMEN_54.Properties.ReadOnly = Status
        chkASESMEN_55.Properties.ReadOnly = Status
        txtASESMEN_56.Properties.ReadOnly = Status
        txtASESMEN_57.Properties.ReadOnly = Status
        chkASESMEN_58.Properties.ReadOnly = Status
        txtASESMEN_59.Properties.ReadOnly = Status
        txtASESMEN_60.Properties.ReadOnly = Status
        chkASESMEN_61.Properties.ReadOnly = Status
        txtASESMEN_62.Properties.ReadOnly = Status
        txtASESMEN_63.Properties.ReadOnly = Status
        chkASESMEN_64.Properties.ReadOnly = Status
        chkASESMEN_65.Properties.ReadOnly = Status
        chkASESMEN_66.Properties.ReadOnly = Status
        chkASESMEN_67.Properties.ReadOnly = Status
        chkASESMEN_68.Properties.ReadOnly = Status
        chkASESMEN_69.Properties.ReadOnly = Status
        chkASESMEN_70.Properties.ReadOnly = Status
        chkASESMEN_71.Properties.ReadOnly = Status
        chkASESMEN_72.Properties.ReadOnly = Status
        chkASESMEN_73.Properties.ReadOnly = Status
        chkASESMEN_74.Properties.ReadOnly = Status
        chkASESMEN_75.Properties.ReadOnly = Status
        chkASESMEN_76.Properties.ReadOnly = Status
        chkASESMEN_77.Properties.ReadOnly = Status
        chkASESMEN_78.Properties.ReadOnly = Status
        chkASESMEN_79.Properties.ReadOnly = Status
        chkASESMEN_80.Properties.ReadOnly = Status
        chkASESMEN_81.Properties.ReadOnly = Status
        chkASESMEN_82.Properties.ReadOnly = Status
        chkASESMEN_83.Properties.ReadOnly = Status
        chkASESMEN_84.Properties.ReadOnly = Status
        chkASESMEN_85.Properties.ReadOnly = Status
        chkASESMEN_86.Properties.ReadOnly = Status
        chkASESMEN_87.Properties.ReadOnly = Status
        chkASESMEN_88.Properties.ReadOnly = Status
        chkASESMEN_89.Properties.ReadOnly = Status
        txtASESMEN_90.Properties.ReadOnly = Status
        txtASESMEN_91.Properties.ReadOnly = Status
        chkASESMEN_92.Properties.ReadOnly = Status
        chkASESMEN_93.Properties.ReadOnly = Status
        chkASESMEN_94.Properties.ReadOnly = Status
        chkASESMEN_95.Properties.ReadOnly = Status
        chkASESMEN_96.Properties.ReadOnly = Status
        chkASESMEN_97.Properties.ReadOnly = Status
        chkASESMEN_98.Properties.ReadOnly = Status
        chkASESMEN_99.Properties.ReadOnly = Status
        chkASESMEN_100.Properties.ReadOnly = Status
        chkASESMEN_101.Properties.ReadOnly = Status
        chkASESMEN_102.Properties.ReadOnly = Status
        chkASESMEN_103.Properties.ReadOnly = Status
        chkASESMEN_104.Properties.ReadOnly = Status
        chkASESMEN_105.Properties.ReadOnly = Status
        chkASESMEN_106.Properties.ReadOnly = Status
        txtASESMEN_107.Properties.ReadOnly = Status
        chkASESMEN_108.Properties.ReadOnly = Status
        chkASESMEN_109.Properties.ReadOnly = Status
        txtASESMEN_110.Properties.ReadOnly = Status
        chkASESMEN_111.Properties.ReadOnly = Status
        chkASESMEN_112.Properties.ReadOnly = Status
        chkASESMEN_113.Properties.ReadOnly = Status
        txtASESMEN_114.Properties.ReadOnly = Status
        chkASESMEN_115.Properties.ReadOnly = Status
        chkASESMEN_116.Properties.ReadOnly = Status
        chkASESMEN_117.Properties.ReadOnly = Status
        chkASESMEN_118.Properties.ReadOnly = Status
        txtASESMEN_119.Properties.ReadOnly = Status
        chkASESMEN_120.Properties.ReadOnly = Status
        chkASESMEN_121.Properties.ReadOnly = Status
        chkASESMEN_122.Properties.ReadOnly = Status
        chkASESMEN_123.Properties.ReadOnly = Status
        chkASESMEN_124.Properties.ReadOnly = Status
        chkASESMEN_125.Properties.ReadOnly = Status
        chkASESMEN_126.Properties.ReadOnly = Status
        chkASESMEN_127.Properties.ReadOnly = Status
        chkASESMEN_128.Properties.ReadOnly = Status
        chkASESMEN_129.Properties.ReadOnly = Status
        chkASESMEN_130.Properties.ReadOnly = Status
        chkASESMEN_131.Properties.ReadOnly = Status
        chkASESMEN_132.Properties.ReadOnly = Status
        chkASESMEN_133.Properties.ReadOnly = Status
        txtASESMEN_134.Properties.ReadOnly = Status
        chkASESMEN_135.Properties.ReadOnly = Status
        chkASESMEN_136.Properties.ReadOnly = Status
        chkASESMEN_137.Properties.ReadOnly = Status
        chkASESMEN_138.Properties.ReadOnly = Status
        txtASESMEN_139.Properties.ReadOnly = Status
        txtASESMEN_140.Properties.ReadOnly = Status
        txtASESMEN_141.Properties.ReadOnly = Status
        txtASESMEN_142.Properties.ReadOnly = Status
        txtASESMEN_143.Properties.ReadOnly = Status
        txtASESMEN_144.Properties.ReadOnly = Status
        txtASESMEN_145.Properties.ReadOnly = Status
        txtASESMEN_146.Properties.ReadOnly = Status
        chkASESMEN_147.Properties.ReadOnly = Status
        chkASESMEN_148.Properties.ReadOnly = Status
        chkASESMEN_149.Properties.ReadOnly = Status
        chkASESMEN_150.Properties.ReadOnly = Status
        chkASESMEN_151.Properties.ReadOnly = Status
        chkASESMEN_152.Properties.ReadOnly = Status
        chkASESMEN_153.Properties.ReadOnly = Status
        chkASESMEN_154.Properties.ReadOnly = Status
        chkASESMEN_155.Properties.ReadOnly = Status
        chkASESMEN_156.Properties.ReadOnly = Status
        chkASESMEN_157.Properties.ReadOnly = Status
        chkASESMEN_158.Properties.ReadOnly = Status
        chkASESMEN_159.Properties.ReadOnly = Status
        chkASESMEN_160.Properties.ReadOnly = Status
        chkASESMEN_161.Properties.ReadOnly = Status
        chkASESMEN_162.Properties.ReadOnly = Status
        chkASESMEN_163.Properties.ReadOnly = Status
        chkASESMEN_164.Properties.ReadOnly = Status
        chkASESMEN_165.Properties.ReadOnly = Status
        chkASESMEN_166.Properties.ReadOnly = Status
        chkASESMEN_167.Properties.ReadOnly = Status
        chkASESMEN_168.Properties.ReadOnly = Status
        chkASESMEN_169.Properties.ReadOnly = Status
        chkASESMEN_170.Properties.ReadOnly = Status
        chkASESMEN_171.Properties.ReadOnly = Status
        chkASESMEN_172.Properties.ReadOnly = Status
        chkASESMEN_173.Properties.ReadOnly = Status
        txtASESMEN_174.Properties.ReadOnly = Status
        txtASESMEN_175.Properties.ReadOnly = Status
        txtASESMEN_176.Properties.ReadOnly = Status
        txtASESMEN_177.Properties.ReadOnly = Status
        txtASESMEN_178.Properties.ReadOnly = Status
        txtASESMEN_179.Properties.ReadOnly = Status
        txtASESMEN_180.Properties.ReadOnly = Status
        chkASESMEN_181.Properties.ReadOnly = Status
        chkASESMEN_182.Properties.ReadOnly = Status
        txtASESMEN_183.Properties.ReadOnly = Status
        txtASESMEN_184.Properties.ReadOnly = Status
        chkASESMEN_185.Properties.ReadOnly = Status
        chkASESMEN_186.Properties.ReadOnly = Status
        txtASESMEN_187.Properties.ReadOnly = Status
        txtASESMEN_188.Properties.ReadOnly = Status
        chkASESMEN_189.Properties.ReadOnly = Status
        chkASESMEN_190.Properties.ReadOnly = Status
        txtASESMEN_191.Properties.ReadOnly = Status
        txtASESMEN_192.Properties.ReadOnly = Status
        txtASESMEN_193.Properties.ReadOnly = Status
        txtASESMEN_194.Properties.ReadOnly = Status
        txtASESMEN_195.Properties.ReadOnly = Status
        txtASESMEN_196.Properties.ReadOnly = Status
        txtASESMEN_197.Properties.ReadOnly = Status
        txtASESMEN_198.Properties.ReadOnly = Status
        txtASESMEN_199.Properties.ReadOnly = Status
        chkASESMEN_200.Properties.ReadOnly = Status
        chkASESMEN_201.Properties.ReadOnly = Status
        chkASESMEN_202.Properties.ReadOnly = Status
        txtASESMEN_203.Properties.ReadOnly = Status
        txtASESMEN_204.Properties.ReadOnly = Status
        txtASESMEN_205.Properties.ReadOnly = Status
        chkASESMEN_206.Properties.ReadOnly = Status
        chkASESMEN_207.Properties.ReadOnly = Status
        chkASESMEN_208.Properties.ReadOnly = Status
        chkASESMEN_209.Properties.ReadOnly = Status
        chkASESMEN_210.Properties.ReadOnly = Status
        chkASESMEN_211.Properties.ReadOnly = Status
        chkASESMEN_212.Properties.ReadOnly = Status
        chkASESMEN_213.Properties.ReadOnly = Status
        chkASESMEN_214.Properties.ReadOnly = Status
        chkASESMEN_215.Properties.ReadOnly = Status
        chkASESMEN_216.Properties.ReadOnly = Status
        chkASESMEN_217.Properties.ReadOnly = Status
        txtASESMEN_218.Properties.ReadOnly = Status
        txtASESMEN_219.Properties.ReadOnly = Status
        txtASESMEN_220.Properties.ReadOnly = Status
        txtASESMEN_221.Properties.ReadOnly = Status
        txtASESMEN_222.Properties.ReadOnly = Status
        txtASESMEN_223.Properties.ReadOnly = Status
        txtASESMEN_224.Properties.ReadOnly = Status
        txtASESMEN_225.Properties.ReadOnly = Status
        txtASESMEN_226.Properties.ReadOnly = Status
        txtASESMEN_227.Properties.ReadOnly = Status
        txtASESMEN_228.Properties.ReadOnly = Status
        txtASESMEN_229.Properties.ReadOnly = Status
        txtASESMEN_230.Properties.ReadOnly = Status
        chkASESMEN_231.Properties.ReadOnly = Status
        chkASESMEN_232.Properties.ReadOnly = Status
        chkASESMEN_233.Properties.ReadOnly = Status
        txtASESMEN_234.Properties.ReadOnly = Status
        chkASESMEN_235.Properties.ReadOnly = Status
        chkASESMEN_236.Properties.ReadOnly = Status
        chkASESMEN_237.Properties.ReadOnly = Status
        chkASESMEN_238.Properties.ReadOnly = Status
        chkASESMEN_239.Properties.ReadOnly = Status
        chkASESMEN_240.Properties.ReadOnly = Status
        chkASESMEN_241.Properties.ReadOnly = Status
        cboASESMEN_242.Properties.ReadOnly = Status
        cboASESMEN_243.Properties.ReadOnly = Status
        txtASESMEN_244.Properties.ReadOnly = Status
        txtASESMEN_245.Properties.ReadOnly = Status
        chkASESMEN_246.Properties.ReadOnly = Status
        chkASESMEN_247.Properties.ReadOnly = Status
        chkASESMEN_248.Properties.ReadOnly = Status
        chkASESMEN_249.Properties.ReadOnly = Status
        chkASESMEN_250.Properties.ReadOnly = Status
        chkASESMEN_251.Properties.ReadOnly = Status
        txtASESMEN_252.Properties.ReadOnly = Status
        cboASESMEN_253.Properties.ReadOnly = Status
        txtASESMEN_254.Properties.ReadOnly = Status
        txtASESMEN_255.Properties.ReadOnly = Status
        txtASESMEN_256.Properties.ReadOnly = Status
        txtASESMEN_257.Properties.ReadOnly = Status
        cboASESMEN_258.Properties.ReadOnly = Status
        txtASESMEN_259.Properties.ReadOnly = Status
        txtASESMEN_260.Properties.ReadOnly = Status
        txtASESMEN_261.Properties.ReadOnly = Status
        txtASESMEN_262.Properties.ReadOnly = Status
        cboASESMEN_263.Properties.ReadOnly = Status
        txtASESMEN_264.Properties.ReadOnly = Status
        txtASESMEN_265.Properties.ReadOnly = Status
        txtASESMEN_266.Properties.ReadOnly = Status
        txtASESMEN_267.Properties.ReadOnly = Status
        cboASESMEN_268.Properties.ReadOnly = Status
        txtASESMEN_269.Properties.ReadOnly = Status
        txtASESMEN_270.Properties.ReadOnly = Status
        txtASESMEN_271.Properties.ReadOnly = Status
        txtASESMEN_272.Properties.ReadOnly = Status
        cboASESMEN_273.Properties.ReadOnly = Status
        txtASESMEN_274.Properties.ReadOnly = Status
        txtASESMEN_275.Properties.ReadOnly = Status
        txtASESMEN_276.Properties.ReadOnly = Status
        txtASESMEN_277.Properties.ReadOnly = Status
        cboASESMEN_278.Properties.ReadOnly = Status
        txtASESMEN_279.Properties.ReadOnly = Status
        txtASESMEN_280.Properties.ReadOnly = Status
        txtASESMEN_281.Properties.ReadOnly = Status
        txtASESMEN_282.Properties.ReadOnly = Status
        txtASESMEN_283.Properties.ReadOnly = Status
        txtASESMEN_284.Properties.ReadOnly = Status
        txtASESMEN_285.Properties.ReadOnly = Status
        txtASESMEN_286.Properties.ReadOnly = Status
        txtASESMEN_287.Properties.ReadOnly = Status
        txtASESMEN_288.Properties.ReadOnly = Status
        txtASESMEN_289.Properties.ReadOnly = Status
        txtASESMEN_290.Properties.ReadOnly = Status
        txtASESMEN_291.Properties.ReadOnly = Status
        txtASESMEN_292.Properties.ReadOnly = Status
        chkASESMEN_293.Properties.ReadOnly = Status
        chkASESMEN_294.Properties.ReadOnly = Status
        chkASESMEN_295.Properties.ReadOnly = Status
        chkASESMEN_296.Properties.ReadOnly = Status
        chkASESMEN_297.Properties.ReadOnly = Status
        chkASESMEN_298.Properties.ReadOnly = Status
        chkASESMEN_299.Properties.ReadOnly = Status
        chkASESMEN_300.Properties.ReadOnly = Status
        chkASESMEN_301.Properties.ReadOnly = Status
        txtASESMEN_302.Properties.ReadOnly = Status
        chkASESMEN_303.Properties.ReadOnly = Status
        chkASESMEN_304.Properties.ReadOnly = Status
        chkASESMEN_305.Properties.ReadOnly = Status
        txtASESMEN_306.Properties.ReadOnly = Status
        chkASESMEN_307.Properties.ReadOnly = Status
        chkASESMEN_308.Properties.ReadOnly = Status
        chkASESMEN_309.Properties.ReadOnly = Status
        chkASESMEN_310.Properties.ReadOnly = Status
        chkASESMEN_311.Properties.ReadOnly = Status
        chkASESMEN_312.Properties.ReadOnly = Status
        chkASESMEN_313.Properties.ReadOnly = Status
        chkASESMEN_314.Properties.ReadOnly = Status
        chkASESMEN_315.Properties.ReadOnly = Status
        chkASESMEN_316.Properties.ReadOnly = Status
        chkASESMEN_317.Properties.ReadOnly = Status
        chkASESMEN_318.Properties.ReadOnly = Status
        txtASESMEN_319.Properties.ReadOnly = Status
        chkASESMEN_320.Properties.ReadOnly = Status
        chkASESMEN_321.Properties.ReadOnly = Status
        chkASESMEN_322.Properties.ReadOnly = Status
        chkASESMEN_323.Properties.ReadOnly = Status
        chkASESMEN_324.Properties.ReadOnly = Status
        chkASESMEN_325.Properties.ReadOnly = Status
        chkASESMEN_326.Properties.ReadOnly = Status
        txtASESMEN_327.Properties.ReadOnly = Status
        txtASESMEN_328.Properties.ReadOnly = Status
        chkASESMEN_329.Properties.ReadOnly = Status
        txtASESMEN_330.Properties.ReadOnly = Status
        chkASESMEN_331.Properties.ReadOnly = Status
        chkASESMEN_332.Properties.ReadOnly = Status
        chkASESMEN_333.Properties.ReadOnly = Status
        chkASESMEN_334.Properties.ReadOnly = Status
        chkASESMEN_335.Properties.ReadOnly = Status
        chkASESMEN_336.Properties.ReadOnly = Status
        chkASESMEN_337.Properties.ReadOnly = Status
        chkASESMEN_338.Properties.ReadOnly = Status
        chkASESMEN_339.Properties.ReadOnly = Status
        chkASESMEN_340.Properties.ReadOnly = Status
        chkASESMEN_341.Properties.ReadOnly = Status
        chkASESMEN_342.Properties.ReadOnly = Status
        chkASESMEN_343.Properties.ReadOnly = Status
        chkASESMEN_344.Properties.ReadOnly = Status
        chkASESMEN_345.Properties.ReadOnly = Status
        chkASESMEN_346.Properties.ReadOnly = Status
        txtASESMEN_347.Properties.ReadOnly = Status
        txtASESMEN_348.Properties.ReadOnly = Status
        MemoEdit1.Properties.ReadOnly = Status
        MemoEdit2.Properties.ReadOnly = Status


        chkCHKKETTIDAK_01.Properties.ReadOnly = Status
        chkCHKKETTIDAK_02.Properties.ReadOnly = Status
        chkCHKKETTIDAK_03.Properties.ReadOnly = Status
        chkCHKKETTIDAK_04.Properties.ReadOnly = Status
        chkCHKKETTIDAK_05.Properties.ReadOnly = Status
        chkCHKKETTIDAK_06.Properties.ReadOnly = Status

        deTGLSKALAMORSE_01.Properties.ReadOnly = Status
        deTGLSKALAMORSE_02.Properties.ReadOnly = Status
        deTGLSKALAMORSE_03.Properties.ReadOnly = Status
        deTGLSKALAMORSE_04.Properties.ReadOnly = Status
        deTGLSKALAMORSE_05.Properties.ReadOnly = Status

        txtTotalSkorJawabanYa.ReadOnly = Status
        txtTGLdanJAM.Properties.ReadOnly = Status

        deMRJ_TGL01.Properties.ReadOnly = Status
        deMRJ_TGL02.Properties.ReadOnly = Status
        deMRJ_TGL03.Properties.ReadOnly = Status
        deMRJ_TGL04.Properties.ReadOnly = Status
        deMRJ_TGL05.Properties.ReadOnly = Status
        deMRJ_TGL06.Properties.ReadOnly = Status
        deMRJ_TGL07.Properties.ReadOnly = Status

        chkMRJ_CHK_A2.Properties.ReadOnly = Status
        chkMRJ_CHK_A3.Properties.ReadOnly = Status
        chkMRJ_CHK_A4.Properties.ReadOnly = Status
        chkMRJ_CHK_A5.Properties.ReadOnly = Status
        chkMRJ_CHK_A6.Properties.ReadOnly = Status
        chkMRJ_CHK_A7.Properties.ReadOnly = Status

        chkMRJ_CHK_B2.Properties.ReadOnly = Status
        chkMRJ_CHK_B3.Properties.ReadOnly = Status
        chkMRJ_CHK_B4.Properties.ReadOnly = Status
        chkMRJ_CHK_B5.Properties.ReadOnly = Status
        chkMRJ_CHK_B6.Properties.ReadOnly = Status
        chkMRJ_CHK_B7.Properties.ReadOnly = Status

        chkMRJ_CHK_C2.Properties.ReadOnly = Status
        chkMRJ_CHK_C3.Properties.ReadOnly = Status
        chkMRJ_CHK_C4.Properties.ReadOnly = Status
        chkMRJ_CHK_C5.Properties.ReadOnly = Status
        chkMRJ_CHK_C6.Properties.ReadOnly = Status
        chkMRJ_CHK_C7.Properties.ReadOnly = Status

        chkMRJ_CHK_D2.Properties.ReadOnly = Status
        chkMRJ_CHK_D3.Properties.ReadOnly = Status
        chkMRJ_CHK_D4.Properties.ReadOnly = Status
        chkMRJ_CHK_D5.Properties.ReadOnly = Status
        chkMRJ_CHK_D6.Properties.ReadOnly = Status
        chkMRJ_CHK_D7.Properties.ReadOnly = Status

        chkMRJ_CHK_E2.Properties.ReadOnly = Status
        chkMRJ_CHK_E3.Properties.ReadOnly = Status
        chkMRJ_CHK_E4.Properties.ReadOnly = Status
        chkMRJ_CHK_E5.Properties.ReadOnly = Status
        chkMRJ_CHK_E6.Properties.ReadOnly = Status
        chkMRJ_CHK_E7.Properties.ReadOnly = Status

        chkMRJ_CHK_F2.Properties.ReadOnly = Status
        chkMRJ_CHK_F3.Properties.ReadOnly = Status
        chkMRJ_CHK_F4.Properties.ReadOnly = Status
        chkMRJ_CHK_F5.Properties.ReadOnly = Status
        chkMRJ_CHK_F6.Properties.ReadOnly = Status
        chkMRJ_CHK_F7.Properties.ReadOnly = Status

        chkMRJ_CHK_G2.Properties.ReadOnly = Status
        chkMRJ_CHK_G3.Properties.ReadOnly = Status
        chkMRJ_CHK_G4.Properties.ReadOnly = Status
        chkMRJ_CHK_G5.Properties.ReadOnly = Status
        chkMRJ_CHK_G6.Properties.ReadOnly = Status
        chkMRJ_CHK_G7.Properties.ReadOnly = Status

        txtMRJ_TTD01.Properties.ReadOnly = Status
        txtMRJ_TTD02.Properties.ReadOnly = Status
        txtMRJ_TTD03.Properties.ReadOnly = Status
        txtMRJ_TTD04.Properties.ReadOnly = Status
        txtMRJ_TTD05.Properties.ReadOnly = Status
        txtMRJ_TTD06.Properties.ReadOnly = Status
        txtMRJ_TTD07.Properties.ReadOnly = Status
        'txtMRJ_TTD08.Properties.ReadOnly = Status

        TextEdit1.Properties.ReadOnly = Status
        TextEdit2.Properties.ReadOnly = Status
        TextEdit3.Properties.ReadOnly = Status
        '.TextEdit4 = ""
        '.TextEdit5 = ""
        chkSOSIAL_01_1.Properties.ReadOnly = Status
        chkSOSIAL_01_2.Properties.ReadOnly = Status
        chkSOSIAL_02_1.Properties.ReadOnly = Status
        chkSOSIAL_02_2.Properties.ReadOnly = Status
        chkSOSIAL_02_3.Properties.ReadOnly = Status
        chkSOSIAL_02_4.Properties.ReadOnly = Status

        grvDetail.OptionsBehavior.ReadOnly = Status

        txtSOSIAL_03_1_TEXT.Properties.ReadOnly = Status
        txtSOSIAL_03_2_TEXT.Properties.ReadOnly = Status
        txtSOSIAL_03_3_TEXT.Properties.ReadOnly = Status
        txtSTATUS_SPIRITUAL_01_TEXT.Properties.ReadOnly = Status
        txtSTATUS_SPIRITUAL_02_TEXT.Properties.ReadOnly = Status

    End Sub
    Private Sub fn_EmptyMe()
        'txtCODE.Text = "<--- AUTO --->"
        deDATE.DateTime = Now
        'txtMANAJER.ResetText()

        MemoEdit1.ResetText()
        MemoEdit2.ResetText()
        chkASESMEN_01.Checked = False
        chkASESMEN_02.Checked = False
        chkASESMEN_03.Checked = False
        chkASESMEN_04.Checked = False
        chkASESMEN_05.Checked = False
        txtASESMEN_06.ResetText()
        txtASESMEN_07.ResetText()
        txtASESMEN_08.ResetText()
        txtASESMEN_09.ResetText()
        txtASESMEN_10.ResetText()
        chkASESMEN_11.Checked = False
        chkASESMEN_12.Checked = False
        chkASESMEN_13.Checked = False
        chkASESMEN_14.Checked = False
        chkASESMEN_15.Checked = False
        chkASESMEN_16.Checked = False
        txtASESMEN_17.ResetText()
        txtASESMEN_18.ResetText()
        txtASESMEN_19.ResetText()
        txtASESMEN_20.ResetText()
        txtASESMEN_21.ResetText()
        txtASESMEN_22.ResetText()
        txtASESMEN_23.ResetText()
        txtASESMEN_24.ResetText()
        chkASESMEN_25.Checked = False
        chkASESMEN_26.Checked = False
        chkASESMEN_27.Checked = False
        chkASESMEN_28.Checked = False
        txtASESMEN_29.ResetText()
        chkASESMEN_30.Checked = False
        chkASESMEN_31.Checked = False
        chkASESMEN_32.Checked = False
        chkASESMEN_33.Checked = False
        chkASESMEN_34.Checked = False
        chkASESMEN_35.Checked = False
        txtASESMEN_36.ResetText()
        chkASESMEN_37.Checked = False
        chkASESMEN_38.Checked = False
        txtASESMEN_39.ResetText()
        chkASESMEN_40.Checked = False
        chkASESMEN_41.Checked = False
        txtASESMEN_42.ResetText()
        chkASESMEN_43.Checked = False
        chkASESMEN_44.Checked = False
        chkASESMEN_45.Checked = False
        chkASESMEN_46.Checked = False
        chkASESMEN_47.Checked = False
        chkASESMEN_48.Checked = False
        chkASESMEN_49.Checked = False
        chkASESMEN_50.Checked = False
        chkASESMEN_51.Checked = False
        chkASESMEN_52.Checked = False
        chkASESMEN_53.Checked = False
        chkASESMEN_54.Checked = False
        chkASESMEN_55.Checked = False
        txtASESMEN_56.ResetText()
        txtASESMEN_57.ResetText()
        chkASESMEN_58.Checked = False
        txtASESMEN_59.ResetText()
        txtASESMEN_60.ResetText()
        chkASESMEN_61.Checked = False
        txtASESMEN_62.ResetText()
        txtASESMEN_63.ResetText()
        chkASESMEN_64.Checked = False
        chkASESMEN_65.Checked = False
        chkASESMEN_66.Checked = False
        chkASESMEN_67.Checked = False
        chkASESMEN_68.Checked = False
        chkASESMEN_69.Checked = False
        chkASESMEN_70.Checked = False
        chkASESMEN_71.Checked = False
        chkASESMEN_72.Checked = False
        chkASESMEN_73.Checked = False
        chkASESMEN_74.Checked = False
        chkASESMEN_75.Checked = False
        chkASESMEN_76.Checked = False
        chkASESMEN_77.Checked = False
        chkASESMEN_78.Checked = False
        chkASESMEN_79.Checked = False
        chkASESMEN_80.Checked = False
        chkASESMEN_81.Checked = False
        chkASESMEN_82.Checked = False
        chkASESMEN_83.Checked = False
        chkASESMEN_84.Checked = False
        chkASESMEN_85.Checked = False
        chkASESMEN_86.Checked = False
        chkASESMEN_87.Checked = False
        chkASESMEN_88.Checked = False
        chkASESMEN_89.Checked = False
        txtASESMEN_90.ResetText()
        txtASESMEN_91.ResetText()
        chkASESMEN_92.Checked = False
        chkASESMEN_93.Checked = False
        chkASESMEN_94.Checked = False
        chkASESMEN_95.Checked = False
        chkASESMEN_96.Checked = False
        chkASESMEN_97.Checked = False
        chkASESMEN_98.Checked = False
        chkASESMEN_99.Checked = False
        chkASESMEN_100.Checked = False
        chkASESMEN_101.Checked = False
        chkASESMEN_102.Checked = False
        chkASESMEN_103.Checked = False
        chkASESMEN_104.Checked = False
        chkASESMEN_105.Checked = False
        chkASESMEN_106.Checked = False
        txtASESMEN_107.ResetText()
        chkASESMEN_108.Checked = False
        chkASESMEN_109.Checked = False
        txtASESMEN_110.ResetText()
        chkASESMEN_111.Checked = False
        chkASESMEN_112.Checked = False
        chkASESMEN_113.Checked = False
        txtASESMEN_114.ResetText()
        chkASESMEN_115.Checked = False
        chkASESMEN_116.Checked = False
        chkASESMEN_117.Checked = False
        chkASESMEN_118.Checked = False
        txtASESMEN_119.ResetText()
        chkASESMEN_120.Checked = False
        chkASESMEN_121.Checked = False
        chkASESMEN_122.Checked = False
        chkASESMEN_123.Checked = False
        chkASESMEN_124.Checked = False
        chkASESMEN_125.Checked = False
        chkASESMEN_126.Checked = False
        chkASESMEN_127.Checked = False
        chkASESMEN_128.Checked = False
        chkASESMEN_129.Checked = False
        chkASESMEN_130.Checked = False
        chkASESMEN_131.Checked = False
        chkASESMEN_132.Checked = False
        chkASESMEN_133.Checked = False
        txtASESMEN_134.ResetText()
        chkASESMEN_135.Checked = False
        chkASESMEN_136.Checked = False
        chkASESMEN_137.Checked = False
        chkASESMEN_138.Checked = False
        txtASESMEN_139.ResetText()
        txtASESMEN_140.ResetText()
        txtASESMEN_141.ResetText()
        txtASESMEN_142.ResetText()
        txtASESMEN_143.ResetText()
        txtASESMEN_144.ResetText()
        txtASESMEN_145.ResetText()
        txtASESMEN_146.ResetText()
        chkASESMEN_147.Checked = False
        chkASESMEN_148.Checked = False
        chkASESMEN_149.Checked = False
        chkASESMEN_150.Checked = False
        chkASESMEN_151.Checked = False
        chkASESMEN_152.Checked = False
        chkASESMEN_153.Checked = False
        chkASESMEN_154.Checked = False
        chkASESMEN_155.Checked = False
        chkASESMEN_156.Checked = False
        chkASESMEN_157.Checked = False
        chkASESMEN_158.Checked = False
        chkASESMEN_159.Checked = False
        chkASESMEN_160.Checked = False
        chkASESMEN_161.Checked = False
        chkASESMEN_162.Checked = False
        chkASESMEN_163.Checked = False
        chkASESMEN_164.Checked = False
        chkASESMEN_165.Checked = False
        chkASESMEN_166.Checked = False
        chkASESMEN_167.Checked = False
        chkASESMEN_168.Checked = False
        chkASESMEN_169.Checked = False
        chkASESMEN_170.Checked = False
        chkASESMEN_171.Checked = False
        chkASESMEN_172.Checked = False
        chkASESMEN_173.Checked = False
        txtASESMEN_174.ResetText()
        txtASESMEN_175.ResetText()
        txtASESMEN_176.ResetText()
        txtASESMEN_177.ResetText()
        txtASESMEN_178.ResetText()
        txtASESMEN_179.ResetText()
        txtASESMEN_180.ResetText()
        chkASESMEN_181.Checked = False
        chkASESMEN_182.Checked = False
        txtASESMEN_183.ResetText()
        txtASESMEN_184.ResetText()
        chkASESMEN_185.Checked = False
        chkASESMEN_186.Checked = False
        txtASESMEN_187.ResetText()
        txtASESMEN_188.ResetText()
        chkASESMEN_189.Checked = False
        chkASESMEN_190.Checked = False
        txtASESMEN_191.ResetText()
        txtASESMEN_192.ResetText()
        txtASESMEN_193.ResetText()
        txtASESMEN_194.ResetText()
        txtASESMEN_195.ResetText()
        txtASESMEN_196.ResetText()
        txtASESMEN_197.ResetText()
        txtASESMEN_198.ResetText()
        txtASESMEN_199.ResetText()
        chkASESMEN_200.Checked = False
        chkASESMEN_201.Checked = False
        chkASESMEN_202.Checked = False
        txtASESMEN_203.ResetText()
        txtASESMEN_204.ResetText()
        txtASESMEN_205.ResetText()
        chkASESMEN_206.Checked = False
        chkASESMEN_207.Checked = False
        chkASESMEN_208.Checked = False
        chkASESMEN_209.Checked = False
        chkASESMEN_210.Checked = False
        chkASESMEN_211.Checked = False
        chkASESMEN_212.Checked = False
        chkASESMEN_213.Checked = False
        chkASESMEN_214.Checked = False
        chkASESMEN_215.Checked = False
        chkASESMEN_216.Checked = False
        chkASESMEN_217.Checked = False
        txtASESMEN_218.ResetText()
        txtASESMEN_219.ResetText()
        txtASESMEN_220.ResetText()
        txtASESMEN_221.ResetText()
        txtASESMEN_222.ResetText()
        txtASESMEN_223.ResetText()
        txtASESMEN_224.ResetText()
        txtASESMEN_225.ResetText()
        txtASESMEN_226.ResetText()
        txtASESMEN_227.ResetText()
        txtASESMEN_228.ResetText()
        txtASESMEN_229.ResetText()
        txtASESMEN_230.ResetText()
        chkASESMEN_231.Checked = False
        chkASESMEN_232.Checked = False
        chkASESMEN_233.Checked = False
        txtASESMEN_234.ResetText()
        chkASESMEN_235.Checked = False
        chkASESMEN_236.Checked = False
        chkASESMEN_237.Checked = False
        chkASESMEN_238.Checked = False
        chkASESMEN_239.Checked = False
        chkASESMEN_240.Checked = False
        chkASESMEN_241.Checked = False
        cboASESMEN_242.ResetText()
        cboASESMEN_243.ResetText()
        txtASESMEN_244.ResetText()
        txtASESMEN_245.ResetText()
        chkASESMEN_246.Checked = False
        chkASESMEN_247.Checked = False
        chkASESMEN_248.Checked = False
        chkASESMEN_249.Checked = False
        chkASESMEN_250.Checked = False
        chkASESMEN_251.Checked = False
        txtASESMEN_252.ResetText()
        cboASESMEN_253.ResetText()
        txtASESMEN_254.ResetText()
        txtASESMEN_255.ResetText()
        txtASESMEN_256.ResetText()
        txtASESMEN_257.ResetText()
        cboASESMEN_258.ResetText()
        txtASESMEN_259.ResetText()
        txtASESMEN_260.ResetText()
        txtASESMEN_261.ResetText()
        txtASESMEN_262.ResetText()
        cboASESMEN_263.ResetText()
        txtASESMEN_264.ResetText()
        txtASESMEN_265.ResetText()
        txtASESMEN_266.ResetText()
        txtASESMEN_267.ResetText()
        cboASESMEN_268.ResetText()
        txtASESMEN_269.ResetText()
        txtASESMEN_270.ResetText()
        txtASESMEN_271.ResetText()
        txtASESMEN_272.ResetText()
        cboASESMEN_273.ResetText()
        txtASESMEN_274.ResetText()
        txtASESMEN_275.ResetText()
        txtASESMEN_276.ResetText()
        txtASESMEN_277.ResetText()
        cboASESMEN_278.ResetText()
        txtASESMEN_279.ResetText()
        txtASESMEN_280.ResetText()
        txtASESMEN_281.ResetText()
        txtASESMEN_282.ResetText()
        txtASESMEN_283.ResetText()
        txtASESMEN_284.ResetText()
        txtASESMEN_285.ResetText()
        txtASESMEN_286.ResetText()
        txtASESMEN_287.ResetText()
        txtASESMEN_288.ResetText()
        txtASESMEN_289.ResetText()
        txtASESMEN_290.ResetText()
        txtASESMEN_291.ResetText()
        txtASESMEN_292.ResetText()
        chkASESMEN_293.Checked = False
        chkASESMEN_294.Checked = False
        chkASESMEN_295.Checked = False
        chkASESMEN_296.Checked = False
        chkASESMEN_297.Checked = False
        chkASESMEN_298.Checked = False
        chkASESMEN_299.Checked = False
        chkASESMEN_300.Checked = False
        chkASESMEN_301.Checked = False
        txtASESMEN_302.ResetText()
        chkASESMEN_303.Checked = False
        chkASESMEN_304.Checked = False
        chkASESMEN_305.Checked = False
        txtASESMEN_306.ResetText()
        chkASESMEN_307.Checked = False
        chkASESMEN_308.Checked = False
        chkASESMEN_309.Checked = False
        chkASESMEN_310.Checked = False
        chkASESMEN_311.Checked = False
        chkASESMEN_312.Checked = False
        chkASESMEN_313.Checked = False
        chkASESMEN_314.Checked = False
        chkASESMEN_315.Checked = False
        chkASESMEN_316.Checked = False
        chkASESMEN_317.Checked = False
        chkASESMEN_318.Checked = False
        txtASESMEN_319.ResetText()
        chkASESMEN_320.Checked = False
        chkASESMEN_321.Checked = False
        chkASESMEN_322.Checked = False
        chkASESMEN_323.Checked = False
        chkASESMEN_324.Checked = False
        chkASESMEN_325.Checked = False
        chkASESMEN_326.Checked = False
        txtASESMEN_327.ResetText()
        txtASESMEN_328.ResetText()
        chkASESMEN_329.Checked = False
        txtASESMEN_330.ResetText()
        chkASESMEN_331.Checked = False
        chkASESMEN_332.Checked = False
        chkASESMEN_333.Checked = False
        chkASESMEN_334.Checked = False
        chkASESMEN_335.Checked = False
        chkASESMEN_336.Checked = False
        chkASESMEN_337.Checked = False
        chkASESMEN_338.Checked = False
        chkASESMEN_339.Checked = False
        chkASESMEN_340.Checked = False
        chkASESMEN_341.Checked = False
        chkASESMEN_342.Checked = False
        chkASESMEN_343.Checked = False
        chkASESMEN_344.Checked = False
        chkASESMEN_345.Checked = False
        chkASESMEN_346.Checked = False
        txtASESMEN_347.ResetText()
        txtASESMEN_348.ResetText()

        chkCHKKETTIDAK_01.Checked = False
        chkCHKKETTIDAK_02.Checked = False
        chkCHKKETTIDAK_03.Checked = False
        chkCHKKETTIDAK_04.Checked = False
        chkCHKKETTIDAK_05.Checked = False
        chkCHKKETTIDAK_06.Checked = False

        deTGLSKALAMORSE_01.ResetText()
        deTGLSKALAMORSE_02.ResetText()
        deTGLSKALAMORSE_03.ResetText()
        deTGLSKALAMORSE_04.ResetText()
        deTGLSKALAMORSE_05.ResetText()

        txtTotalSkorJawabanYa.ResetText()
        txtTGLdanJAM.ResetText()

        deMRJ_TGL01.ResetText()
        deMRJ_TGL02.ResetText()
        deMRJ_TGL03.ResetText()
        deMRJ_TGL04.ResetText()
        deMRJ_TGL05.ResetText()
        deMRJ_TGL06.ResetText()
        deMRJ_TGL07.ResetText()

        chkMRJ_CHK_A2.Checked = False
        chkMRJ_CHK_A3.Checked = False
        chkMRJ_CHK_A4.Checked = False
        chkMRJ_CHK_A5.Checked = False
        chkMRJ_CHK_A6.Checked = False
        chkMRJ_CHK_A7.Checked = False

        chkMRJ_CHK_B2.Checked = False
        chkMRJ_CHK_B3.Checked = False
        chkMRJ_CHK_B4.Checked = False
        chkMRJ_CHK_B5.Checked = False
        chkMRJ_CHK_B6.Checked = False
        chkMRJ_CHK_B7.Checked = False

        chkMRJ_CHK_C2.Checked = False
        chkMRJ_CHK_C3.Checked = False
        chkMRJ_CHK_C4.Checked = False
        chkMRJ_CHK_C5.Checked = False
        chkMRJ_CHK_C6.Checked = False
        chkMRJ_CHK_C7.Checked = False

        chkMRJ_CHK_D2.Checked = False
        chkMRJ_CHK_D3.Checked = False
        chkMRJ_CHK_D4.Checked = False
        chkMRJ_CHK_D5.Checked = False
        chkMRJ_CHK_D6.Checked = False
        chkMRJ_CHK_D7.Checked = False

        chkMRJ_CHK_E2.Checked = False
        chkMRJ_CHK_E3.Checked = False
        chkMRJ_CHK_E4.Checked = False
        chkMRJ_CHK_E5.Checked = False
        chkMRJ_CHK_E6.Checked = False
        chkMRJ_CHK_E7.Checked = False

        chkMRJ_CHK_F2.Checked = False
        chkMRJ_CHK_F3.Checked = False
        chkMRJ_CHK_F4.Checked = False
        chkMRJ_CHK_F5.Checked = False
        chkMRJ_CHK_F6.Checked = False
        chkMRJ_CHK_F7.Checked = False

        chkMRJ_CHK_G2.Checked = False
        chkMRJ_CHK_G3.Checked = False
        chkMRJ_CHK_G4.Checked = False
        chkMRJ_CHK_G5.Checked = False
        chkMRJ_CHK_G6.Checked = False
        chkMRJ_CHK_G7.Checked = False

        txtMRJ_TTD01.ResetText()
        txtMRJ_TTD02.ResetText()
        txtMRJ_TTD03.ResetText()
        txtMRJ_TTD04.ResetText()
        txtMRJ_TTD05.ResetText()
        txtMRJ_TTD06.ResetText()
        txtMRJ_TTD07.ResetText()

        TextEdit1.ResetText()
        TextEdit2.ResetText()
        TextEdit3.ResetText()
        '.TextEdit4 = ""
        '.TextEdit5 = ""
        chkSOSIAL_01_1.Checked = False
        chkSOSIAL_01_2.Checked = False
        chkSOSIAL_02_1.Checked = False
        chkSOSIAL_02_2.Checked = False
        chkSOSIAL_02_3.Checked = False
        chkSOSIAL_02_4.Checked = False

        txtSOSIAL_03_1_TEXT.ResetText()
        txtSOSIAL_03_2_TEXT.ResetText()
        txtSOSIAL_03_3_TEXT.ResetText()
        txtSTATUS_SPIRITUAL_01_TEXT.ResetText()
        txtSTATUS_SPIRITUAL_02_TEXT.ResetText()
        'txtMRJ_TTD08.Properties.ReadOnly = Status
    End Sub
    Private Sub fn_LoadData()
        Try
            ' ***** HEADER *****
            Dim ds = oS_DIGITAL_RI_29.GetData(txtNoRegister.Text)

            With ds
                deDATE.DateTime = .DATE
                'txtMANAJER.Text = .MANAGER_PELAYANAN

                chkASESMEN_01.Checked = .ASESMEN_1
                chkASESMEN_02.Checked = .ASESMEN_2
                chkASESMEN_03.Checked = .ASESMEN_3
                chkASESMEN_04.Checked = .ASESMEN_4
                chkASESMEN_05.Checked = .ASESMEN_5
                txtASESMEN_06.Text = .ASESMEN_6
                txtASESMEN_07.Text = .ASESMEN_7
                txtASESMEN_08.Text = .ASESMEN_8
                txtASESMEN_09.Text = .ASESMEN_9
                txtASESMEN_10.Text = .ASESMEN_10
                chkASESMEN_11.Checked = .ASESMEN_11
                chkASESMEN_12.Checked = .ASESMEN_12
                chkASESMEN_13.Checked = .ASESMEN_13
                chkASESMEN_14.Checked = .ASESMEN_14
                chkASESMEN_15.Checked = .ASESMEN_15
                chkASESMEN_16.Checked = .ASESMEN_16
                txtASESMEN_17.Text = .ASESMEN_17
                txtASESMEN_18.Text = .ASESMEN_18
                txtASESMEN_19.Text = .ASESMEN_19
                txtASESMEN_20.Text = .ASESMEN_20
                txtASESMEN_21.Text = .ASESMEN_21
                txtASESMEN_22.Text = .ASESMEN_22
                txtASESMEN_23.Text = .ASESMEN_23
                txtASESMEN_24.Text = .ASESMEN_24
                chkASESMEN_25.Checked = .ASESMEN_25
                chkASESMEN_26.Checked = .ASESMEN_26
                chkASESMEN_27.Checked = .ASESMEN_27
                chkASESMEN_28.Checked = .ASESMEN_28
                txtASESMEN_29.Text = .ASESMEN_29
                chkASESMEN_30.Checked = .ASESMEN_30
                chkASESMEN_31.Checked = .ASESMEN_31
                chkASESMEN_32.Checked = .ASESMEN_32
                chkASESMEN_33.Checked = .ASESMEN_33
                chkASESMEN_34.Checked = .ASESMEN_34
                chkASESMEN_35.Checked = .ASESMEN_35
                txtASESMEN_36.Text = .ASESMEN_36
                chkASESMEN_37.Checked = .ASESMEN_37
                chkASESMEN_38.Checked = .ASESMEN_38
                txtASESMEN_39.Text = .ASESMEN_39
                chkASESMEN_40.Checked = .ASESMEN_40
                chkASESMEN_41.Checked = .ASESMEN_41
                txtASESMEN_42.Text = .ASESMEN_42
                chkASESMEN_43.Checked = .ASESMEN_43
                chkASESMEN_44.Checked = .ASESMEN_44
                chkASESMEN_45.Checked = .ASESMEN_45
                chkASESMEN_46.Checked = .ASESMEN_46
                chkASESMEN_47.Checked = .ASESMEN_47
                chkASESMEN_48.Checked = .ASESMEN_48
                chkASESMEN_49.Checked = .ASESMEN_49
                chkASESMEN_50.Checked = .ASESMEN_50
                chkASESMEN_51.Checked = .ASESMEN_51
                chkASESMEN_52.Checked = .ASESMEN_52
                chkASESMEN_53.Checked = .ASESMEN_53
                chkASESMEN_54.Checked = .ASESMEN_54
                chkASESMEN_55.Checked = .ASESMEN_55
                txtASESMEN_56.Text = .ASESMEN_56
                txtASESMEN_57.Text = .ASESMEN_57
                chkASESMEN_58.Checked = .ASESMEN_58
                txtASESMEN_59.Text = .ASESMEN_59
                txtASESMEN_60.Text = .ASESMEN_60
                chkASESMEN_61.Checked = .ASESMEN_61
                txtASESMEN_62.Text = .ASESMEN_62
                txtASESMEN_63.Text = .ASESMEN_63
                chkASESMEN_64.Checked = .ASESMEN_64
                chkASESMEN_65.Checked = .ASESMEN_65
                chkASESMEN_66.Checked = .ASESMEN_66
                chkASESMEN_67.Checked = .ASESMEN_67
                chkASESMEN_68.Checked = .ASESMEN_68
                chkASESMEN_69.Checked = .ASESMEN_69
                chkASESMEN_70.Checked = .ASESMEN_70
                chkASESMEN_71.Checked = .ASESMEN_71
                chkASESMEN_72.Checked = .ASESMEN_72
                chkASESMEN_73.Checked = .ASESMEN_73
                chkASESMEN_74.Checked = .ASESMEN_74
                chkASESMEN_75.Checked = .ASESMEN_75
                chkASESMEN_76.Checked = .ASESMEN_76
                chkASESMEN_77.Checked = .ASESMEN_77
                chkASESMEN_78.Checked = .ASESMEN_78
                chkASESMEN_79.Checked = .ASESMEN_79
                chkASESMEN_80.Checked = .ASESMEN_80
                chkASESMEN_81.Checked = .ASESMEN_81
                chkASESMEN_82.Checked = .ASESMEN_82
                chkASESMEN_83.Checked = .ASESMEN_83
                chkASESMEN_84.Checked = .ASESMEN_84
                chkASESMEN_85.Checked = .ASESMEN_85
                chkASESMEN_86.Checked = .ASESMEN_86
                chkASESMEN_87.Checked = .ASESMEN_87
                chkASESMEN_88.Checked = .ASESMEN_88
                chkASESMEN_89.Checked = .ASESMEN_89
                txtASESMEN_90.Text = .ASESMEN_90
                txtASESMEN_91.Text = .ASESMEN_91
                chkASESMEN_92.Checked = .ASESMEN_92
                chkASESMEN_93.Checked = .ASESMEN_93
                chkASESMEN_94.Checked = .ASESMEN_94
                chkASESMEN_95.Checked = .ASESMEN_95
                chkASESMEN_96.Checked = .ASESMEN_96
                chkASESMEN_97.Checked = .ASESMEN_97
                chkASESMEN_98.Checked = .ASESMEN_98
                chkASESMEN_99.Checked = .ASESMEN_99
                chkASESMEN_100.Checked = .ASESMEN_100
                chkASESMEN_101.Checked = .ASESMEN_101
                chkASESMEN_102.Checked = .ASESMEN_102
                chkASESMEN_103.Checked = .ASESMEN_103
                chkASESMEN_104.Checked = .ASESMEN_104
                chkASESMEN_105.Checked = .ASESMEN_105
                chkASESMEN_106.Checked = .ASESMEN_106
                txtASESMEN_107.Text = .ASESMEN_107
                chkASESMEN_108.Checked = .ASESMEN_108
                chkASESMEN_109.Checked = .ASESMEN_109
                txtASESMEN_110.Text = .ASESMEN_110
                chkASESMEN_111.Checked = .ASESMEN_111
                chkASESMEN_112.Checked = .ASESMEN_112
                chkASESMEN_113.Checked = .ASESMEN_113
                txtASESMEN_114.Text = .ASESMEN_114
                chkASESMEN_115.Checked = .ASESMEN_115
                chkASESMEN_116.Checked = .ASESMEN_116
                chkASESMEN_117.Checked = .ASESMEN_117
                chkASESMEN_118.Checked = .ASESMEN_118
                txtASESMEN_119.Text = .ASESMEN_119
                chkASESMEN_120.Checked = .ASESMEN_120
                chkASESMEN_121.Checked = .ASESMEN_121
                chkASESMEN_122.Checked = .ASESMEN_122
                chkASESMEN_123.Checked = .ASESMEN_123
                chkASESMEN_124.Checked = .ASESMEN_124
                chkASESMEN_125.Checked = .ASESMEN_125
                chkASESMEN_126.Checked = .ASESMEN_126
                chkASESMEN_127.Checked = .ASESMEN_127
                chkASESMEN_128.Checked = .ASESMEN_128
                chkASESMEN_129.Checked = .ASESMEN_129
                chkASESMEN_130.Checked = .ASESMEN_130
                chkASESMEN_131.Checked = .ASESMEN_131
                chkASESMEN_132.Checked = .ASESMEN_132
                chkASESMEN_133.Checked = .ASESMEN_133
                txtASESMEN_134.Text = .ASESMEN_134
                chkASESMEN_135.Checked = .ASESMEN_135
                chkASESMEN_136.Checked = .ASESMEN_136
                chkASESMEN_137.Checked = .ASESMEN_137
                chkASESMEN_138.Checked = .ASESMEN_138
                txtASESMEN_139.Text = .ASESMEN_139
                txtASESMEN_140.Text = .ASESMEN_140
                txtASESMEN_141.Text = .ASESMEN_141
                txtASESMEN_142.Text = .ASESMEN_142
                txtASESMEN_143.Text = .ASESMEN_143
                txtASESMEN_144.Text = .ASESMEN_144
                txtASESMEN_145.Text = .ASESMEN_145
                txtASESMEN_146.Text = .ASESMEN_146
                chkASESMEN_147.Checked = .ASESMEN_147
                chkASESMEN_148.Checked = .ASESMEN_148
                chkASESMEN_149.Checked = .ASESMEN_149
                chkASESMEN_150.Checked = .ASESMEN_150
                chkASESMEN_151.Checked = .ASESMEN_151
                chkASESMEN_152.Checked = .ASESMEN_152
                chkASESMEN_153.Checked = .ASESMEN_153
                chkASESMEN_154.Checked = .ASESMEN_154
                chkASESMEN_155.Checked = .ASESMEN_155
                chkASESMEN_156.Checked = .ASESMEN_156
                chkASESMEN_157.Checked = .ASESMEN_157
                chkASESMEN_158.Checked = .ASESMEN_158
                chkASESMEN_159.Checked = .ASESMEN_159
                chkASESMEN_160.Checked = .ASESMEN_160
                chkASESMEN_161.Checked = .ASESMEN_161
                chkASESMEN_162.Checked = .ASESMEN_162
                chkASESMEN_163.Checked = .ASESMEN_163
                chkASESMEN_164.Checked = .ASESMEN_164
                chkASESMEN_165.Checked = .ASESMEN_165
                chkASESMEN_166.Checked = .ASESMEN_166
                chkASESMEN_167.Checked = .ASESMEN_167
                chkASESMEN_168.Checked = .ASESMEN_168
                chkASESMEN_169.Checked = .ASESMEN_169
                chkASESMEN_170.Checked = .ASESMEN_170
                chkASESMEN_171.Checked = .ASESMEN_171
                chkASESMEN_172.Checked = .ASESMEN_172
                chkASESMEN_173.Checked = .ASESMEN_173
                txtASESMEN_174.Text = .ASESMEN_174
                txtASESMEN_175.Text = .ASESMEN_175
                txtASESMEN_176.Text = .ASESMEN_176
                txtASESMEN_177.Text = .ASESMEN_177
                txtASESMEN_178.Text = .ASESMEN_178
                txtASESMEN_179.Text = .ASESMEN_179
                txtASESMEN_180.Text = .ASESMEN_180
                chkASESMEN_181.Checked = .ASESMEN_181
                chkASESMEN_182.Checked = .ASESMEN_182
                txtASESMEN_183.Text = .ASESMEN_183
                txtASESMEN_184.Text = .ASESMEN_184
                chkASESMEN_185.Checked = .ASESMEN_185
                chkASESMEN_186.Checked = .ASESMEN_186
                txtASESMEN_187.Text = .ASESMEN_187
                txtASESMEN_188.Text = .ASESMEN_188
                chkASESMEN_189.Checked = .ASESMEN_189
                chkASESMEN_190.Checked = .ASESMEN_190
                txtASESMEN_191.Text = .ASESMEN_191
                txtASESMEN_192.Text = .ASESMEN_192
                txtASESMEN_193.Text = .ASESMEN_193
                txtASESMEN_194.Text = .ASESMEN_194
                txtASESMEN_195.Text = .ASESMEN_195
                txtASESMEN_196.Text = .ASESMEN_196
                txtASESMEN_197.Text = .ASESMEN_197
                txtASESMEN_198.Text = .ASESMEN_198
                txtASESMEN_199.Text = .ASESMEN_199
                chkASESMEN_200.Checked = .ASESMEN_200
                chkASESMEN_201.Checked = .ASESMEN_201
                chkASESMEN_202.Checked = .ASESMEN_202
                txtASESMEN_203.Text = .ASESMEN_203
                txtASESMEN_204.Text = .ASESMEN_204
                txtASESMEN_205.Text = .ASESMEN_205
                chkASESMEN_206.Checked = .ASESMEN_206
                chkASESMEN_207.Checked = .ASESMEN_207
                chkASESMEN_208.Checked = .ASESMEN_208
                chkASESMEN_209.Checked = .ASESMEN_209
                chkASESMEN_210.Checked = .ASESMEN_210
                chkASESMEN_211.Checked = .ASESMEN_211
                chkASESMEN_212.Checked = .ASESMEN_212
                chkASESMEN_213.Checked = .ASESMEN_213
                chkASESMEN_214.Checked = .ASESMEN_214
                chkASESMEN_215.Checked = .ASESMEN_215
                chkASESMEN_216.Checked = .ASESMEN_216
                chkASESMEN_217.Checked = .ASESMEN_217
                txtASESMEN_218.Text = .ASESMEN_218
                txtASESMEN_219.Text = .ASESMEN_219
                txtASESMEN_220.Text = .ASESMEN_220
                txtASESMEN_221.Text = .ASESMEN_221
                txtASESMEN_222.Text = .ASESMEN_222
                txtASESMEN_223.Text = .ASESMEN_223
                txtASESMEN_224.Text = .ASESMEN_224
                txtASESMEN_225.Text = .ASESMEN_225
                txtASESMEN_226.Text = .ASESMEN_226
                txtASESMEN_227.Text = .ASESMEN_227
                txtASESMEN_228.Text = .ASESMEN_228
                txtASESMEN_229.Text = .ASESMEN_229
                txtASESMEN_230.Text = .ASESMEN_230
                chkASESMEN_231.Checked = .ASESMEN_231
                chkASESMEN_232.Checked = .ASESMEN_232
                chkASESMEN_233.Checked = .ASESMEN_233
                txtASESMEN_234.Text = .ASESMEN_234
                chkASESMEN_235.Checked = .ASESMEN_235
                chkASESMEN_236.Checked = .ASESMEN_236
                chkASESMEN_237.Checked = .ASESMEN_237
                chkASESMEN_238.Checked = .ASESMEN_238
                chkASESMEN_239.Checked = .ASESMEN_239
                chkASESMEN_240.Checked = .ASESMEN_240
                chkASESMEN_241.Checked = .ASESMEN_241
                cboASESMEN_242.Text = .ASESMEN_242
                cboASESMEN_243.Text = .ASESMEN_243
                txtASESMEN_244.Text = .ASESMEN_244
                txtASESMEN_245.Text = .ASESMEN_245
                chkASESMEN_246.Checked = .ASESMEN_246
                chkASESMEN_247.Checked = .ASESMEN_247
                chkASESMEN_248.Checked = .ASESMEN_248
                chkASESMEN_249.Checked = .ASESMEN_249
                chkASESMEN_250.Checked = .ASESMEN_250
                chkASESMEN_251.Checked = .ASESMEN_251
                txtASESMEN_252.Text = .ASESMEN_252
                cboASESMEN_253.Text = .ASESMEN_253
                txtASESMEN_254.Text = .ASESMEN_254
                txtASESMEN_255.Text = .ASESMEN_255
                txtASESMEN_256.Text = .ASESMEN_256
                txtASESMEN_257.Text = .ASESMEN_257
                cboASESMEN_258.Text = .ASESMEN_258
                txtASESMEN_259.Text = .ASESMEN_259
                txtASESMEN_260.Text = .ASESMEN_260
                txtASESMEN_261.Text = .ASESMEN_261
                txtASESMEN_262.Text = .ASESMEN_262
                cboASESMEN_263.Text = .ASESMEN_263
                txtASESMEN_264.Text = .ASESMEN_264
                txtASESMEN_265.Text = .ASESMEN_265
                txtASESMEN_266.Text = .ASESMEN_266
                txtASESMEN_267.Text = .ASESMEN_267
                cboASESMEN_268.Text = .ASESMEN_268
                txtASESMEN_269.Text = .ASESMEN_269
                txtASESMEN_270.Text = .ASESMEN_270
                txtASESMEN_271.Text = .ASESMEN_271
                txtASESMEN_272.Text = .ASESMEN_272
                cboASESMEN_273.Text = .ASESMEN_273
                txtASESMEN_274.Text = .ASESMEN_274
                txtASESMEN_275.Text = .ASESMEN_275
                txtASESMEN_276.Text = .ASESMEN_276
                txtASESMEN_277.Text = .ASESMEN_277
                cboASESMEN_278.Text = .ASESMEN_278
                txtASESMEN_279.Text = .ASESMEN_279
                txtASESMEN_280.Text = .ASESMEN_280
                txtASESMEN_281.Text = .ASESMEN_281
                txtASESMEN_282.Text = .ASESMEN_282
                txtASESMEN_283.Text = .ASESMEN_283
                txtASESMEN_284.Text = .ASESMEN_284
                txtASESMEN_285.Text = .ASESMEN_285
                txtASESMEN_286.Text = .ASESMEN_286
                txtASESMEN_287.Text = .ASESMEN_287
                txtASESMEN_288.Text = .ASESMEN_288
                txtASESMEN_289.Text = .ASESMEN_289
                txtASESMEN_290.Text = .ASESMEN_290
                txtASESMEN_291.Text = .ASESMEN_291
                txtASESMEN_292.Text = .ASESMEN_292
                chkASESMEN_293.Checked = .ASESMEN_293
                chkASESMEN_294.Checked = .ASESMEN_294
                chkASESMEN_295.Checked = .ASESMEN_295
                chkASESMEN_296.Checked = .ASESMEN_296
                chkASESMEN_297.Checked = .ASESMEN_297
                chkASESMEN_298.Checked = .ASESMEN_298
                chkASESMEN_299.Checked = .ASESMEN_299
                chkASESMEN_300.Checked = .ASESMEN_300
                chkASESMEN_301.Checked = .ASESMEN_301
                txtASESMEN_302.Text = .ASESMEN_302
                chkASESMEN_303.Checked = .ASESMEN_303
                chkASESMEN_304.Checked = .ASESMEN_304
                chkASESMEN_305.Checked = .ASESMEN_305
                txtASESMEN_306.Text = .ASESMEN_306
                chkASESMEN_307.Checked = .ASESMEN_307
                chkASESMEN_308.Checked = .ASESMEN_308
                chkASESMEN_309.Checked = .ASESMEN_309
                chkASESMEN_310.Checked = .ASESMEN_310
                chkASESMEN_311.Checked = .ASESMEN_311
                chkASESMEN_312.Checked = .ASESMEN_312
                chkASESMEN_313.Checked = .ASESMEN_313
                chkASESMEN_314.Checked = .ASESMEN_314
                chkASESMEN_315.Checked = .ASESMEN_315
                chkASESMEN_316.Checked = .ASESMEN_316
                chkASESMEN_317.Checked = .ASESMEN_317
                chkASESMEN_318.Checked = .ASESMEN_318
                txtASESMEN_319.Text = .ASESMEN_319
                chkASESMEN_320.Checked = .ASESMEN_320
                chkASESMEN_321.Checked = .ASESMEN_321
                chkASESMEN_322.Checked = .ASESMEN_322
                chkASESMEN_323.Checked = .ASESMEN_323
                chkASESMEN_324.Checked = .ASESMEN_324
                chkASESMEN_325.Checked = .ASESMEN_325
                chkASESMEN_326.Checked = .ASESMEN_326
                txtASESMEN_327.Text = .ASESMEN_327
                txtASESMEN_328.Text = .ASESMEN_328
                chkASESMEN_329.Checked = .ASESMEN_329
                txtASESMEN_330.Text = .ASESMEN_330
                chkASESMEN_331.Checked = .ASESMEN_331
                chkASESMEN_332.Checked = .ASESMEN_332
                chkASESMEN_333.Checked = .ASESMEN_333
                chkASESMEN_334.Checked = .ASESMEN_334
                chkASESMEN_335.Checked = .ASESMEN_335
                chkASESMEN_336.Checked = .ASESMEN_336
                chkASESMEN_337.Checked = .ASESMEN_337
                chkASESMEN_338.Checked = .ASESMEN_338
                chkASESMEN_339.Checked = .ASESMEN_339
                chkASESMEN_340.Checked = .ASESMEN_340
                chkASESMEN_341.Checked = .ASESMEN_341
                chkASESMEN_342.Checked = .ASESMEN_342
                chkASESMEN_343.Checked = .ASESMEN_343
                chkASESMEN_344.Checked = .ASESMEN_344
                chkASESMEN_345.Checked = .ASESMEN_345
                chkASESMEN_346.Checked = .ASESMEN_346
                txtASESMEN_347.Text = .ASESMEN_347
                txtASESMEN_348.Text = .ASESMEN_348

                chkCHKKETTIDAK_01.Checked = .CHKKETTIDAK_01
                chkCHKKETTIDAK_02.Checked = .CHKKETTIDAK_02
                chkCHKKETTIDAK_03.Checked = .CHKKETTIDAK_03
                chkCHKKETTIDAK_04.Checked = .CHKKETTIDAK_04
                chkCHKKETTIDAK_05.Checked = .CHKKETTIDAK_05
                chkCHKKETTIDAK_06.Checked = .CHKKETTIDAK_06

                deTGLSKALAMORSE_01.Text = .TGLSKALAMORSE_01
                deTGLSKALAMORSE_02.Text = .TGLSKALAMORSE_02
                deTGLSKALAMORSE_03.Text = .TGLSKALAMORSE_03
                deTGLSKALAMORSE_04.Text = .TGLSKALAMORSE_04
                deTGLSKALAMORSE_05.Text = .TGLSKALAMORSE_05

                txtTotalSkorJawabanYa.Text = .TOTALSKORYA
                txtTGLdanJAM.Text = .TGLDANJAM


                If .MRJ_TGL01 <> "" Then
                    deMRJ_TGL01.DateTime = .MRJ_TGL01
                End If
                
                If .MRJ_TGL02 <> "" Then
                    deMRJ_TGL02.DateTime = .MRJ_TGL02
                End If
                
                If .MRJ_TGL03 <> "" Then
                    deMRJ_TGL03.DateTime = .MRJ_TGL03
                End If
                
                If .MRJ_TGL04 <> "" Then
                    deMRJ_TGL04.DateTime = .MRJ_TGL04
                End If
                
                If .MRJ_TGL05 <> "" Then
                    deMRJ_TGL05.DateTime = .MRJ_TGL05
                End If
                
                If .MRJ_TGL06 <> "" Then
                    deMRJ_TGL06.DateTime = .MRJ_TGL06
                End If
                
                If .MRJ_TGL07 <> "" Then
                    deMRJ_TGL07.DateTime = .MRJ_TGL07
                End If
                

                chkMRJ_CHK_A2.Checked = .MRJ_CHK_A2
                chkMRJ_CHK_A3.Checked = .MRJ_CHK_A3
                chkMRJ_CHK_A4.Checked = .MRJ_CHK_A4
                chkMRJ_CHK_A5.Checked = .MRJ_CHK_A5
                chkMRJ_CHK_A6.Checked = .MRJ_CHK_A6
                chkMRJ_CHK_A7.Checked = .MRJ_CHK_A7

                chkMRJ_CHK_B2.Checked = .MRJ_CHK_B2
                chkMRJ_CHK_B3.Checked = .MRJ_CHK_B3
                chkMRJ_CHK_B4.Checked = .MRJ_CHK_B4
                chkMRJ_CHK_B5.Checked = .MRJ_CHK_B5
                chkMRJ_CHK_B6.Checked = .MRJ_CHK_B6
                chkMRJ_CHK_B7.Checked = .MRJ_CHK_B7

                chkMRJ_CHK_C2.Checked = .MRJ_CHK_C2
                chkMRJ_CHK_C3.Checked = .MRJ_CHK_C3
                chkMRJ_CHK_C4.Checked = .MRJ_CHK_C4
                chkMRJ_CHK_C5.Checked = .MRJ_CHK_C5
                chkMRJ_CHK_C6.Checked = .MRJ_CHK_C6
                chkMRJ_CHK_C7.Checked = .MRJ_CHK_C7

                chkMRJ_CHK_D2.Checked = .MRJ_CHK_D2
                chkMRJ_CHK_D3.Checked = .MRJ_CHK_D3
                chkMRJ_CHK_D4.Checked = .MRJ_CHK_D4
                chkMRJ_CHK_D5.Checked = .MRJ_CHK_D5
                chkMRJ_CHK_D6.Checked = .MRJ_CHK_D6
                chkMRJ_CHK_D7.Checked = .MRJ_CHK_D7

                chkMRJ_CHK_E2.Checked = .MRJ_CHK_E2
                chkMRJ_CHK_E3.Checked = .MRJ_CHK_E3
                chkMRJ_CHK_E4.Checked = .MRJ_CHK_E4
                chkMRJ_CHK_E5.Checked = .MRJ_CHK_E5
                chkMRJ_CHK_E6.Checked = .MRJ_CHK_E6
                chkMRJ_CHK_E7.Checked = .MRJ_CHK_E7

                chkMRJ_CHK_F2.Checked = .MRJ_CHK_F2
                chkMRJ_CHK_F3.Checked = .MRJ_CHK_F3
                chkMRJ_CHK_F4.Checked = .MRJ_CHK_F4
                chkMRJ_CHK_F5.Checked = .MRJ_CHK_F5
                chkMRJ_CHK_F6.Checked = .MRJ_CHK_F6
                chkMRJ_CHK_F7.Checked = .MRJ_CHK_F7

                chkMRJ_CHK_G2.Checked = .MRJ_CHK_G2
                chkMRJ_CHK_G3.Checked = .MRJ_CHK_G3
                chkMRJ_CHK_G4.Checked = .MRJ_CHK_G4
                chkMRJ_CHK_G5.Checked = .MRJ_CHK_G5
                chkMRJ_CHK_G6.Checked = .MRJ_CHK_G6
                chkMRJ_CHK_G7.Checked = .MRJ_CHK_G7

                txtMRJ_TTD01.Text = .MRJ_TTD01
                txtMRJ_TTD02.Text = .MRJ_TTD02
                txtMRJ_TTD03.Text = .MRJ_TTD03
                txtMRJ_TTD04.Text = .MRJ_TTD04
                txtMRJ_TTD05.Text = .MRJ_TTD05
                txtMRJ_TTD06.Text = .MRJ_TTD06
                txtMRJ_TTD07.Text = .MRJ_TTD07


                TextEdit1.Text = .TextEdit1
                TextEdit2.Text = .TextEdit2
                TextEdit3.Text = .TextEdit3
                MemoEdit1.Text = .TextEdit4
                MemoEdit2.Text = .TextEdit5
                chkSOSIAL_01_1.Checked = .chkSOSIAL_01_1
                chkSOSIAL_01_2.Checked = .chkSOSIAL_01_2
                chkSOSIAL_02_1.Checked = .chkSOSIAL_02_1
                chkSOSIAL_02_2.Checked = .chkSOSIAL_02_2
                chkSOSIAL_02_3.Checked = .chkSOSIAL_02_3
                chkSOSIAL_02_4.Checked = .chkSOSIAL_02_4


                txtSOSIAL_03_1_TEXT.Text = .txtSOSIAL_03_1_TEXT
                txtSOSIAL_03_2_TEXT.Text = .txtSOSIAL_03_2_TEXT
                txtSOSIAL_03_3_TEXT.Text = .txtSOSIAL_03_3_TEXT
                txtSTATUS_SPIRITUAL_01_TEXT.Text = .txtSTATUS_SPIRITUAL_01_TEXT
                txtSTATUS_SPIRITUAL_02_TEXT.Text = .txtSTATUS_SPIRITUAL_02_TEXT


                BindingSource.DataSource = oS_DIGITAL_RI_29.GetDataDetail(txtNoRegister.Text)
                grdDetail.DataSource = BindingSource
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
                MsgBox("Dibutuhkan Register", MsgBoxStyle.Exclamation, Me.Text)
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
            Dim ds = oS_DIGITAL_RI_29.GetStructureHeader
            With ds
                .KDPENDAFTARAN = txtNoRegister.Text
                .KDCUSTOMER = txtNoPasien.Text
                Try
                    .DATECREATED = oS_DIGITAL_RI_29.GetData(txtNoRegister.Text).DATECREATED
                Catch ex As Exception
                    .DATECREATED = Now
                End Try
                .DATEUPDATED = Now
                .DATE = deDATE.DateTime
                '.MANAGER_PELAYANAN = txtMANAJER.Text
                .JAM = deDATE.DateTime
                .DOKTER_KODE = ""
                .DOKTER_NAMEDISPLAY = txtDokter.Text
                .ASESMEN_1 = chkASESMEN_01.Checked
                .ASESMEN_2 = chkASESMEN_02.Checked
                .ASESMEN_3 = chkASESMEN_03.Checked
                .ASESMEN_4 = chkASESMEN_04.Checked
                .ASESMEN_5 = chkASESMEN_05.Checked
                .ASESMEN_6 = txtASESMEN_06.Text
                .ASESMEN_7 = txtASESMEN_07.Text
                .ASESMEN_8 = txtASESMEN_08.Text
                .ASESMEN_9 = txtASESMEN_09.Text
                .ASESMEN_10 = txtASESMEN_10.Text
                .ASESMEN_11 = chkASESMEN_11.Checked
                .ASESMEN_12 = chkASESMEN_12.Checked
                .ASESMEN_13 = chkASESMEN_13.Checked
                .ASESMEN_14 = chkASESMEN_14.Checked
                .ASESMEN_15 = chkASESMEN_15.Checked
                .ASESMEN_16 = chkASESMEN_16.Checked
                .ASESMEN_17 = txtASESMEN_17.Text
                .ASESMEN_18 = txtASESMEN_18.Text
                .ASESMEN_19 = txtASESMEN_19.Text
                .ASESMEN_20 = txtASESMEN_20.Text
                .ASESMEN_21 = txtASESMEN_21.Text
                .ASESMEN_22 = txtASESMEN_22.Text
                .ASESMEN_23 = txtASESMEN_23.Text
                .ASESMEN_24 = txtASESMEN_24.Text
                .ASESMEN_25 = chkASESMEN_25.Checked
                .ASESMEN_26 = chkASESMEN_26.Checked
                .ASESMEN_27 = chkASESMEN_27.Checked
                .ASESMEN_28 = chkASESMEN_28.Checked
                .ASESMEN_29 = txtASESMEN_29.Text
                .ASESMEN_30 = chkASESMEN_30.Checked
                .ASESMEN_31 = chkASESMEN_31.Checked
                .ASESMEN_32 = chkASESMEN_32.Checked
                .ASESMEN_33 = chkASESMEN_33.Checked
                .ASESMEN_34 = chkASESMEN_34.Checked
                .ASESMEN_35 = chkASESMEN_35.Checked
                .ASESMEN_36 = txtASESMEN_36.Text
                .ASESMEN_37 = chkASESMEN_37.Checked
                .ASESMEN_38 = chkASESMEN_38.Checked
                .ASESMEN_39 = txtASESMEN_39.Text
                .ASESMEN_40 = chkASESMEN_40.Checked
                .ASESMEN_41 = chkASESMEN_41.Checked
                .ASESMEN_42 = txtASESMEN_42.Text
                .ASESMEN_43 = chkASESMEN_43.Checked
                .ASESMEN_44 = chkASESMEN_44.Checked
                .ASESMEN_45 = chkASESMEN_45.Checked
                .ASESMEN_46 = chkASESMEN_46.Checked
                .ASESMEN_47 = chkASESMEN_47.Checked
                .ASESMEN_48 = chkASESMEN_48.Checked
                .ASESMEN_49 = chkASESMEN_49.Checked
                .ASESMEN_50 = chkASESMEN_50.Checked
                .ASESMEN_51 = chkASESMEN_51.Checked
                .ASESMEN_52 = chkASESMEN_52.Checked
                .ASESMEN_53 = chkASESMEN_53.Checked
                .ASESMEN_54 = chkASESMEN_54.Checked
                .ASESMEN_55 = chkASESMEN_55.Checked
                .ASESMEN_56 = txtASESMEN_56.Text
                .ASESMEN_57 = txtASESMEN_57.Text
                .ASESMEN_58 = chkASESMEN_58.Checked
                .ASESMEN_59 = txtASESMEN_59.Text
                .ASESMEN_60 = txtASESMEN_60.Text
                .ASESMEN_61 = chkASESMEN_61.Checked
                .ASESMEN_62 = txtASESMEN_62.Text
                .ASESMEN_63 = txtASESMEN_63.Text
                .ASESMEN_64 = chkASESMEN_64.Checked
                .ASESMEN_65 = chkASESMEN_65.Checked
                .ASESMEN_66 = chkASESMEN_66.Checked
                .ASESMEN_67 = chkASESMEN_67.Checked
                .ASESMEN_68 = chkASESMEN_68.Checked
                .ASESMEN_69 = chkASESMEN_69.Checked
                .ASESMEN_70 = chkASESMEN_70.Checked
                .ASESMEN_71 = chkASESMEN_71.Checked
                .ASESMEN_72 = chkASESMEN_72.Checked
                .ASESMEN_73 = chkASESMEN_73.Checked
                .ASESMEN_74 = chkASESMEN_74.Checked
                .ASESMEN_75 = chkASESMEN_75.Checked
                .ASESMEN_76 = chkASESMEN_76.Checked
                .ASESMEN_77 = chkASESMEN_77.Checked
                .ASESMEN_78 = chkASESMEN_78.Checked
                .ASESMEN_79 = chkASESMEN_79.Checked
                .ASESMEN_80 = chkASESMEN_80.Checked
                .ASESMEN_81 = chkASESMEN_81.Checked
                .ASESMEN_82 = chkASESMEN_82.Checked
                .ASESMEN_83 = chkASESMEN_83.Checked
                .ASESMEN_84 = chkASESMEN_84.Checked
                .ASESMEN_85 = chkASESMEN_85.Checked
                .ASESMEN_86 = chkASESMEN_86.Checked
                .ASESMEN_87 = chkASESMEN_87.Checked
                .ASESMEN_88 = chkASESMEN_88.Checked
                .ASESMEN_89 = chkASESMEN_89.Checked
                .ASESMEN_90 = txtASESMEN_90.Text
                .ASESMEN_91 = txtASESMEN_91.Text
                .ASESMEN_92 = chkASESMEN_92.Checked
                .ASESMEN_93 = chkASESMEN_93.Checked
                .ASESMEN_94 = chkASESMEN_94.Checked
                .ASESMEN_95 = chkASESMEN_95.Checked
                .ASESMEN_96 = chkASESMEN_96.Checked
                .ASESMEN_97 = chkASESMEN_97.Checked
                .ASESMEN_98 = chkASESMEN_98.Checked
                .ASESMEN_99 = chkASESMEN_99.Checked
                .ASESMEN_100 = chkASESMEN_100.Checked
                .ASESMEN_101 = chkASESMEN_101.Checked
                .ASESMEN_102 = chkASESMEN_102.Checked
                .ASESMEN_103 = chkASESMEN_103.Checked
                .ASESMEN_104 = chkASESMEN_104.Checked
                .ASESMEN_105 = chkASESMEN_105.Checked
                .ASESMEN_106 = chkASESMEN_106.Checked
                .ASESMEN_107 = txtASESMEN_107.Text
                .ASESMEN_108 = chkASESMEN_108.Checked
                .ASESMEN_109 = chkASESMEN_109.Checked
                .ASESMEN_110 = txtASESMEN_110.Text
                .ASESMEN_111 = chkASESMEN_111.Checked
                .ASESMEN_112 = chkASESMEN_112.Checked
                .ASESMEN_113 = chkASESMEN_113.Checked
                .ASESMEN_114 = txtASESMEN_114.Text
                .ASESMEN_115 = chkASESMEN_115.Checked
                .ASESMEN_116 = chkASESMEN_116.Checked
                .ASESMEN_117 = chkASESMEN_117.Checked
                .ASESMEN_118 = chkASESMEN_118.Checked
                .ASESMEN_119 = txtASESMEN_119.Text
                .ASESMEN_120 = chkASESMEN_120.Checked
                .ASESMEN_121 = chkASESMEN_121.Checked
                .ASESMEN_122 = chkASESMEN_122.Checked
                .ASESMEN_123 = chkASESMEN_123.Checked
                .ASESMEN_124 = chkASESMEN_124.Checked
                .ASESMEN_125 = chkASESMEN_125.Checked
                .ASESMEN_126 = chkASESMEN_126.Checked
                .ASESMEN_127 = chkASESMEN_127.Checked
                .ASESMEN_128 = chkASESMEN_128.Checked
                .ASESMEN_129 = chkASESMEN_129.Checked
                .ASESMEN_130 = chkASESMEN_130.Checked
                .ASESMEN_131 = chkASESMEN_131.Checked
                .ASESMEN_132 = chkASESMEN_132.Checked
                .ASESMEN_133 = chkASESMEN_133.Checked
                .ASESMEN_134 = txtASESMEN_134.Text
                .ASESMEN_135 = chkASESMEN_135.Checked
                .ASESMEN_136 = chkASESMEN_136.Checked
                .ASESMEN_137 = chkASESMEN_137.Checked
                .ASESMEN_138 = chkASESMEN_138.Checked
                .ASESMEN_139 = txtASESMEN_139.Text
                .ASESMEN_140 = txtASESMEN_140.Text
                .ASESMEN_141 = txtASESMEN_141.Text
                .ASESMEN_142 = txtASESMEN_142.Text
                .ASESMEN_143 = txtASESMEN_143.Text
                .ASESMEN_144 = txtASESMEN_144.Text
                .ASESMEN_145 = txtASESMEN_145.Text
                .ASESMEN_146 = txtASESMEN_146.Text
                .ASESMEN_147 = chkASESMEN_147.Checked
                .ASESMEN_148 = chkASESMEN_148.Checked
                .ASESMEN_149 = chkASESMEN_149.Checked
                .ASESMEN_150 = chkASESMEN_150.Checked
                .ASESMEN_151 = chkASESMEN_151.Checked
                .ASESMEN_152 = chkASESMEN_152.Checked
                .ASESMEN_153 = chkASESMEN_153.Checked
                .ASESMEN_154 = chkASESMEN_154.Checked
                .ASESMEN_155 = chkASESMEN_155.Checked
                .ASESMEN_156 = chkASESMEN_156.Checked
                .ASESMEN_157 = chkASESMEN_157.Checked
                .ASESMEN_158 = chkASESMEN_158.Checked
                .ASESMEN_159 = chkASESMEN_159.Checked
                .ASESMEN_160 = chkASESMEN_160.Checked
                .ASESMEN_161 = chkASESMEN_161.Checked
                .ASESMEN_162 = chkASESMEN_162.Checked
                .ASESMEN_163 = chkASESMEN_163.Checked
                .ASESMEN_164 = chkASESMEN_164.Checked
                .ASESMEN_165 = chkASESMEN_165.Checked
                .ASESMEN_166 = chkASESMEN_166.Checked
                .ASESMEN_167 = chkASESMEN_167.Checked
                .ASESMEN_168 = chkASESMEN_168.Checked
                .ASESMEN_169 = chkASESMEN_169.Checked
                .ASESMEN_170 = chkASESMEN_170.Checked
                .ASESMEN_171 = chkASESMEN_171.Checked
                .ASESMEN_172 = chkASESMEN_172.Checked
                .ASESMEN_173 = chkASESMEN_173.Checked
                .ASESMEN_174 = txtASESMEN_174.Text
                .ASESMEN_175 = txtASESMEN_175.Text
                .ASESMEN_176 = txtASESMEN_176.Text
                .ASESMEN_177 = txtASESMEN_177.Text
                .ASESMEN_178 = txtASESMEN_178.Text
                .ASESMEN_179 = txtASESMEN_179.Text
                .ASESMEN_180 = txtASESMEN_180.Text
                .ASESMEN_181 = chkASESMEN_181.Checked
                .ASESMEN_182 = chkASESMEN_182.Checked
                .ASESMEN_183 = txtASESMEN_183.Text
                .ASESMEN_184 = txtASESMEN_184.Text
                .ASESMEN_185 = chkASESMEN_185.Checked
                .ASESMEN_186 = chkASESMEN_186.Checked
                .ASESMEN_187 = txtASESMEN_187.Text
                .ASESMEN_188 = txtASESMEN_188.Text
                .ASESMEN_189 = chkASESMEN_189.Checked
                .ASESMEN_190 = chkASESMEN_190.Checked
                .ASESMEN_191 = txtASESMEN_191.Text
                .ASESMEN_192 = txtASESMEN_192.Text
                .ASESMEN_193 = txtASESMEN_193.Text
                .ASESMEN_194 = txtASESMEN_194.Text
                .ASESMEN_195 = txtASESMEN_195.Text
                .ASESMEN_196 = txtASESMEN_196.Text
                .ASESMEN_197 = txtASESMEN_197.Text
                .ASESMEN_198 = txtASESMEN_198.Text
                .ASESMEN_199 = txtASESMEN_199.Text
                .ASESMEN_200 = chkASESMEN_200.Checked
                .ASESMEN_201 = chkASESMEN_201.Checked
                .ASESMEN_202 = chkASESMEN_202.Checked
                .ASESMEN_203 = txtASESMEN_203.Text
                .ASESMEN_204 = txtASESMEN_204.Text
                .ASESMEN_205 = txtASESMEN_205.Text
                .ASESMEN_206 = chkASESMEN_206.Checked
                .ASESMEN_207 = chkASESMEN_207.Checked
                .ASESMEN_208 = chkASESMEN_208.Checked
                .ASESMEN_209 = chkASESMEN_209.Checked
                .ASESMEN_210 = chkASESMEN_210.Checked
                .ASESMEN_211 = chkASESMEN_211.Checked
                .ASESMEN_212 = chkASESMEN_212.Checked
                .ASESMEN_213 = chkASESMEN_213.Checked
                .ASESMEN_214 = chkASESMEN_214.Checked
                .ASESMEN_215 = chkASESMEN_215.Checked
                .ASESMEN_216 = chkASESMEN_216.Checked
                .ASESMEN_217 = chkASESMEN_217.Checked
                .ASESMEN_218 = txtASESMEN_218.Text
                .ASESMEN_219 = txtASESMEN_219.Text
                .ASESMEN_220 = txtASESMEN_220.Text
                .ASESMEN_221 = txtASESMEN_221.Text
                .ASESMEN_222 = txtASESMEN_222.Text
                .ASESMEN_223 = txtASESMEN_223.Text
                .ASESMEN_224 = txtASESMEN_224.Text
                .ASESMEN_225 = txtASESMEN_225.Text
                .ASESMEN_226 = txtASESMEN_226.Text
                .ASESMEN_227 = txtASESMEN_227.Text
                .ASESMEN_228 = txtASESMEN_228.Text
                .ASESMEN_229 = txtASESMEN_229.Text
                .ASESMEN_230 = txtASESMEN_230.Text
                .ASESMEN_231 = chkASESMEN_231.Checked
                .ASESMEN_232 = chkASESMEN_232.Checked
                .ASESMEN_233 = chkASESMEN_233.Checked
                .ASESMEN_234 = txtASESMEN_234.Text
                .ASESMEN_235 = chkASESMEN_235.Checked
                .ASESMEN_236 = chkASESMEN_236.Checked
                .ASESMEN_237 = chkASESMEN_237.Checked
                .ASESMEN_238 = chkASESMEN_238.Checked
                .ASESMEN_239 = chkASESMEN_239.Checked
                .ASESMEN_240 = chkASESMEN_240.Checked
                .ASESMEN_241 = chkASESMEN_241.Checked
                .ASESMEN_242 = cboASESMEN_242.Text
                .ASESMEN_243 = cboASESMEN_243.Text
                .ASESMEN_244 = txtASESMEN_244.Text
                .ASESMEN_245 = txtASESMEN_245.Text
                .ASESMEN_246 = chkASESMEN_246.Checked
                .ASESMEN_247 = chkASESMEN_247.Checked
                .ASESMEN_248 = chkASESMEN_248.Checked
                .ASESMEN_249 = chkASESMEN_249.Checked
                .ASESMEN_250 = chkASESMEN_250.Checked
                .ASESMEN_251 = chkASESMEN_251.Checked
                .ASESMEN_252 = txtASESMEN_252.Text
                .ASESMEN_253 = cboASESMEN_253.Text
                .ASESMEN_254 = txtASESMEN_254.Text
                .ASESMEN_255 = txtASESMEN_255.Text
                .ASESMEN_256 = txtASESMEN_256.Text
                .ASESMEN_257 = txtASESMEN_257.Text
                .ASESMEN_258 = cboASESMEN_258.text
                .ASESMEN_259 = txtASESMEN_259.Text
                .ASESMEN_260 = txtASESMEN_260.Text
                .ASESMEN_261 = txtASESMEN_261.Text
                .ASESMEN_262 = txtASESMEN_262.Text
                .ASESMEN_263 = cboASESMEN_263.Text
                .ASESMEN_264 = txtASESMEN_264.Text
                .ASESMEN_265 = txtASESMEN_265.Text
                .ASESMEN_266 = txtASESMEN_266.Text
                .ASESMEN_267 = txtASESMEN_267.Text
                .ASESMEN_268 = cboASESMEN_268.Text
                .ASESMEN_269 = txtASESMEN_269.Text
                .ASESMEN_270 = txtASESMEN_270.Text
                .ASESMEN_271 = txtASESMEN_271.Text
                .ASESMEN_272 = txtASESMEN_272.Text
                .ASESMEN_273 = cboASESMEN_273.Text
                .ASESMEN_274 = txtASESMEN_274.Text
                .ASESMEN_275 = txtASESMEN_275.Text
                .ASESMEN_276 = txtASESMEN_276.Text
                .ASESMEN_277 = txtASESMEN_277.Text
                .ASESMEN_278 = cboASESMEN_278.Text
                .ASESMEN_279 = txtASESMEN_279.Text
                .ASESMEN_280 = txtASESMEN_280.Text
                .ASESMEN_281 = txtASESMEN_281.Text
                .ASESMEN_282 = txtASESMEN_282.Text
                .ASESMEN_283 = txtASESMEN_283.Text
                .ASESMEN_284 = txtASESMEN_284.Text
                .ASESMEN_285 = txtASESMEN_285.Text
                .ASESMEN_286 = txtASESMEN_286.Text
                .ASESMEN_287 = txtASESMEN_287.Text
                .ASESMEN_288 = txtASESMEN_288.Text
                .ASESMEN_289 = txtASESMEN_289.Text
                .ASESMEN_290 = txtASESMEN_290.Text
                .ASESMEN_291 = txtASESMEN_291.Text
                .ASESMEN_292 = txtASESMEN_292.Text
                .ASESMEN_293 = chkASESMEN_293.Checked
                .ASESMEN_294 = chkASESMEN_294.Checked
                .ASESMEN_295 = chkASESMEN_295.Checked
                .ASESMEN_296 = chkASESMEN_296.Checked
                .ASESMEN_297 = chkASESMEN_297.Checked
                .ASESMEN_298 = chkASESMEN_298.Checked
                .ASESMEN_299 = chkASESMEN_299.Checked
                .ASESMEN_300 = chkASESMEN_300.Checked
                .ASESMEN_301 = chkASESMEN_301.Checked
                .ASESMEN_302 = txtASESMEN_302.Text
                .ASESMEN_303 = chkASESMEN_303.Checked
                .ASESMEN_304 = chkASESMEN_304.Checked
                .ASESMEN_305 = chkASESMEN_305.Checked
                .ASESMEN_306 = txtASESMEN_306.Text
                .ASESMEN_307 = chkASESMEN_307.Checked
                .ASESMEN_308 = chkASESMEN_308.Checked
                .ASESMEN_309 = chkASESMEN_309.Checked
                .ASESMEN_310 = chkASESMEN_310.Checked
                .ASESMEN_311 = chkASESMEN_311.Checked
                .ASESMEN_312 = chkASESMEN_312.Checked
                .ASESMEN_313 = chkASESMEN_313.Checked
                .ASESMEN_314 = chkASESMEN_314.Checked
                .ASESMEN_315 = chkASESMEN_315.Checked
                .ASESMEN_316 = chkASESMEN_316.Checked
                .ASESMEN_317 = chkASESMEN_317.Checked
                .ASESMEN_318 = chkASESMEN_318.Checked
                .ASESMEN_319 = txtASESMEN_319.Text
                .ASESMEN_320 = chkASESMEN_320.Checked
                .ASESMEN_321 = chkASESMEN_321.Checked
                .ASESMEN_322 = chkASESMEN_322.Checked
                .ASESMEN_323 = chkASESMEN_323.Checked
                .ASESMEN_324 = chkASESMEN_324.Checked
                .ASESMEN_325 = chkASESMEN_325.Checked
                .ASESMEN_326 = chkASESMEN_326.Checked
                .ASESMEN_327 = txtASESMEN_327.Text
                .ASESMEN_328 = txtASESMEN_328.Text
                .ASESMEN_329 = chkASESMEN_329.Checked
                .ASESMEN_330 = txtASESMEN_330.Text
                .ASESMEN_331 = chkASESMEN_331.Checked
                .ASESMEN_332 = chkASESMEN_332.Checked
                .ASESMEN_333 = chkASESMEN_333.Checked
                .ASESMEN_334 = chkASESMEN_334.Checked
                .ASESMEN_335 = chkASESMEN_335.Checked
                .ASESMEN_336 = chkASESMEN_336.Checked
                .ASESMEN_337 = chkASESMEN_337.Checked
                .ASESMEN_338 = chkASESMEN_338.Checked
                .ASESMEN_339 = chkASESMEN_339.Checked
                .ASESMEN_340 = chkASESMEN_340.Checked
                .ASESMEN_341 = chkASESMEN_341.Checked
                .ASESMEN_342 = chkASESMEN_342.Checked
                .ASESMEN_343 = chkASESMEN_343.Checked
                .ASESMEN_344 = chkASESMEN_344.Checked
                .ASESMEN_345 = chkASESMEN_345.Checked
                .ASESMEN_346 = chkASESMEN_346.Checked
                .ASESMEN_347 = txtASESMEN_347.Text
                .ASESMEN_348 = txtASESMEN_348.Text

                Try
                    .CETAK = oS_DIGITAL_RI_29.GetData(txtNoRegister.Text).CETAK
                Catch ex As Exception
                    .CETAK = 0
                End Try
                .KDUSER = sKDUSER_PERAWAT
                .KDUSER_SIGNATURE = sUserID


                .CHKKETTIDAK_01 = chkCHKKETTIDAK_01.Checked
                .CHKKETTIDAK_02 = chkCHKKETTIDAK_02.Checked
                .CHKKETTIDAK_03 = chkCHKKETTIDAK_03.Checked
                .CHKKETTIDAK_04 = chkCHKKETTIDAK_04.Checked
                .CHKKETTIDAK_05 = chkCHKKETTIDAK_05.Checked
                .CHKKETTIDAK_06 = chkCHKKETTIDAK_06.Checked

                .TGLSKALAMORSE_01 = deTGLSKALAMORSE_01.Text
                .TGLSKALAMORSE_02 = deTGLSKALAMORSE_02.Text
                .TGLSKALAMORSE_03 = deTGLSKALAMORSE_03.Text
                .TGLSKALAMORSE_04 = deTGLSKALAMORSE_04.Text
                .TGLSKALAMORSE_05 = deTGLSKALAMORSE_05.Text

                .TOTALSKORYA = txtTotalSkorJawabanYa.Text
                .TGLDANJAM = txtTGLdanJAM.Text


                .MRJ_TGL01 = deMRJ_TGL01.DateTime
                .MRJ_TGL02 = deMRJ_TGL02.DateTime
                .MRJ_TGL03 = deMRJ_TGL03.DateTime
                .MRJ_TGL04 = deMRJ_TGL04.DateTime
                .MRJ_TGL05 = deMRJ_TGL05.DateTime
                .MRJ_TGL06 = deMRJ_TGL06.DateTime
                .MRJ_TGL07 = deMRJ_TGL07.DateTime

                .MRJ_CHK_A2 = chkMRJ_CHK_A2.Checked
                .MRJ_CHK_A3 = chkMRJ_CHK_A3.Checked
                .MRJ_CHK_A4 = chkMRJ_CHK_A4.Checked
                .MRJ_CHK_A5 = chkMRJ_CHK_A5.Checked
                .MRJ_CHK_A6 = chkMRJ_CHK_A6.Checked
                .MRJ_CHK_A7 = chkMRJ_CHK_A7.Checked

                .MRJ_CHK_B2 = chkMRJ_CHK_B2.Checked
                .MRJ_CHK_B3 = chkMRJ_CHK_B3.Checked
                .MRJ_CHK_B4 = chkMRJ_CHK_B4.Checked
                .MRJ_CHK_B5 = chkMRJ_CHK_B5.Checked
                .MRJ_CHK_B6 = chkMRJ_CHK_B6.Checked
                .MRJ_CHK_B7 = chkMRJ_CHK_B7.Checked

                .MRJ_CHK_C2 = chkMRJ_CHK_C2.Checked
                .MRJ_CHK_C3 = chkMRJ_CHK_C3.Checked
                .MRJ_CHK_C4 = chkMRJ_CHK_C4.Checked
                .MRJ_CHK_C5 = chkMRJ_CHK_C5.Checked
                .MRJ_CHK_C6 = chkMRJ_CHK_C6.Checked
                .MRJ_CHK_C7 = chkMRJ_CHK_C7.Checked

                .MRJ_CHK_D2 = chkMRJ_CHK_D2.Checked 
                .MRJ_CHK_D3 = chkMRJ_CHK_D3.Checked 
                 .MRJ_CHK_D4 = chkMRJ_CHK_D4.Checked 
                .MRJ_CHK_D5 = chkMRJ_CHK_D5.Checked 
                .MRJ_CHK_D6 = chkMRJ_CHK_D6.Checked 
                .MRJ_CHK_D7 = chkMRJ_CHK_D7.Checked 

                .MRJ_CHK_E2 = chkMRJ_CHK_E2.Checked 
                .MRJ_CHK_E3 = chkMRJ_CHK_E3.Checked 
                .MRJ_CHK_E4 = chkMRJ_CHK_E4.Checked 
                .MRJ_CHK_E5 = chkMRJ_CHK_E5.Checked 
                .MRJ_CHK_E6 = chkMRJ_CHK_E6.Checked 
                .MRJ_CHK_E7 = chkMRJ_CHK_E7.Checked 

                .MRJ_CHK_F2 = chkMRJ_CHK_F2.Checked 
                .MRJ_CHK_F3 = chkMRJ_CHK_F3.Checked 
                .MRJ_CHK_F4 = chkMRJ_CHK_F4.Checked 
                .MRJ_CHK_F5 = chkMRJ_CHK_F5.Checked 
                .MRJ_CHK_F6 = chkMRJ_CHK_F6.Checked 
                .MRJ_CHK_F7 = chkMRJ_CHK_F7.Checked 

                .MRJ_CHK_G2 = chkMRJ_CHK_G2.Checked 
                .MRJ_CHK_G3 = chkMRJ_CHK_G3.Checked 
                .MRJ_CHK_G4 = chkMRJ_CHK_G4.Checked 
                .MRJ_CHK_G5 = chkMRJ_CHK_G5.Checked 
                .MRJ_CHK_G6 = chkMRJ_CHK_G6.Checked 
                .MRJ_CHK_G7 = chkMRJ_CHK_G7.Checked 

                .MRJ_TTD01 = txtMRJ_TTD01.Text 
                .MRJ_TTD02 = txtMRJ_TTD02.Text 
                .MRJ_TTD03 = txtMRJ_TTD03.Text 
                .MRJ_TTD04 = txtMRJ_TTD04.Text 
                .MRJ_TTD05 = txtMRJ_TTD05.Text 
                .MRJ_TTD06 = txtMRJ_TTD06.Text 
                .MRJ_TTD07 = txtMRJ_TTD07.Text
                .MRJ_TTD08 = ""

                .TextEdit1 = TextEdit1.Text
                .TextEdit2 = TextEdit2.Text
                .TextEdit3 = TextEdit3.Text
                .TextEdit4 = MemoEdit1.Text
                .TextEdit5 = MemoEdit2.Text
                .chkSOSIAL_01_1 = chkSOSIAL_01_1.Checked
                .chkSOSIAL_01_2 = chkSOSIAL_01_2.Checked
                .chkSOSIAL_02_1 = chkSOSIAL_02_1.Checked
                .chkSOSIAL_02_2 = chkSOSIAL_02_2.Checked
                .chkSOSIAL_02_3 = chkSOSIAL_02_3.Checked
                .chkSOSIAL_02_4 = chkSOSIAL_02_4.Checked

                .txtSOSIAL_03_1_TEXT = txtSOSIAL_03_1_TEXT.Text
                .txtSOSIAL_03_2_TEXT = txtSOSIAL_03_2_TEXT.Text
                .txtSOSIAL_03_3_TEXT = txtSOSIAL_03_3_TEXT.Text
                .txtSTATUS_SPIRITUAL_01_TEXT = txtSTATUS_SPIRITUAL_01_TEXT.Text
                .txtSTATUS_SPIRITUAL_02_TEXT = txtSTATUS_SPIRITUAL_02_TEXT.Text
            End With

            ' ***** DETIL *****
            Dim arrDetail = oS_DIGITAL_RI_29.GetStructureDetailList ''
            For i As Integer = 0 To grvDetail.RowCount - 2
                Dim dsDetail = oS_DIGITAL_RI_29.GetStructureDetail
                With dsDetail
                    .SEQ = i
                    .KDPENDAFTARAN = ds.KDPENDAFTARAN
                    .TANGGAL = CDate(grvDetail.GetRowCellValue(i, colDATE))
                    .TEMPAT = grvDetail.GetRowCellValue(i, colTEMPAT)
                    .UMUR = grvDetail.GetRowCellValue(i, colUMUR)
                    .JENISPERSALINAN = grvDetail.GetRowCellValue(i, colJENIS)
                    .PENOLONG = grvDetail.GetRowCellValue(i, colPENOLONG)
                    .PENYULIT = grvDetail.GetRowCellValue(i, colPENYULIT)
                    .BB = grvDetail.GetRowCellValue(i, colBB)
                    .PB = grvDetail.GetRowCellValue(i, colPB)
                    .KEADAANANAK = grvDetail.GetRowCellValue(i, colKEADAAN)
                End With
                arrDetail.Add(dsDetail)
            Next

            If oFormMode = FORM_MODE.FORM_MODE_ADD Then
                Try
                    fn_Save = oS_DIGITAL_RI_29.InsertData(ds, arrDetail)
                Catch ex As Exception
                    MsgBox("Simpan Data : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            ElseIf oFormMode = FORM_MODE.FORM_MODE_EDIT Then
                Try
                    fn_Save = oS_DIGITAL_RI_29.UpdateData(ds, arrDetail)
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
            Case Keys.PageUp
	            fn_ScrollPage(True)
            Case Keys.PageDown
	            fn_ScrollPage(False)
        End Select
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
    Private Sub btnClose_Click() Handles btnClose.ItemClick
        Me.Close()
    End Sub
#End Region
#Region "Lookup / Event"
    Private Sub fn_NOIDUSER()
        Dim oUser As New Setting.clsUser
        Try
            'grdNOIDUSER.Properties.DataSource = oUser.GetData.Where(Function(x) x.ISACTIVE = True).ToList()
            'grdNOIDUSER.Properties.ValueMember = "NOIDUSER"
            'grdNOIDUSER.Properties.DisplayMember = "NOIDUSER"
        Catch oErr As Exception
            MsgBox("Load Sub Spesialis Data : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub btnTambah_Click(sender As Object, e As EventArgs) Handles btnTAMBAH.Click
        Dim frmPopUpEMedrekRI_29 As New frmPopUpEMedrekRI_29
        frmPopUpEMedrekRI_29.fn_LoadMe(Now, "", "", "", "", "", "", "", "")
        frmPopUpEMedrekRI_29.ShowDialog()

        If sFind1 <> String.Empty Then
            grvDetail.Focus()
            grvDetail.AddNewRow()
            grvDetail.SetFocusedRowCellValue(colDATE, sFind1)
            grvDetail.SetFocusedRowCellValue(colTEMPAT, sFind2)
            grvDetail.SetFocusedRowCellValue(colUMUR, sFind3)
            grvDetail.SetFocusedRowCellValue(colJENIS, sFind4)
            grvDetail.SetFocusedRowCellValue(colPENOLONG, sFind5)
            grvDetail.SetFocusedRowCellValue(colPENYULIT, sFind6)
            grvDetail.SetFocusedRowCellValue(colBB, sFind7)
            grvDetail.SetFocusedRowCellValue(colPB, sFind8)
            grvDetail.SetFocusedRowCellValue(colKEADAAN, sFind9)
            grvDetail.UpdateCurrentRow()
        End If

        sFind1 = String.Empty
        sFind2 = String.Empty
        sFind3 = String.Empty

        grdDetail.Focus()
    End Sub
    Private Sub EditToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles EditToolStripMenuItem.Click
        Dim frmPopUpEMedrekRI_29 As New frmPopUpEMedrekRI_29
        frmPopUpEMedrekRI_29.fn_LoadMe(grvDetail.GetFocusedRowCellValue(colDATE), grvDetail.GetFocusedRowCellValue(colTEMPAT), grvDetail.GetFocusedRowCellValue(colUMUR), grvDetail.GetFocusedRowCellValue(colJENIS), grvDetail.GetFocusedRowCellValue(colPENOLONG), grvDetail.GetFocusedRowCellValue(colPENYULIT), grvDetail.GetFocusedRowCellValue(colBB), grvDetail.GetFocusedRowCellValue(colPB), grvDetail.GetFocusedRowCellValue(colKEADAAN))
        frmPopUpEMedrekRI_29.ShowDialog()

        If sFind1 <> String.Empty Then
            grvDetail.SetFocusedRowCellValue(colDATE, sFind1)
            grvDetail.SetFocusedRowCellValue(colTEMPAT, sFind2)
            grvDetail.SetFocusedRowCellValue(colUMUR, sFind3)
            grvDetail.SetFocusedRowCellValue(colJENIS, sFind4)
            grvDetail.SetFocusedRowCellValue(colPENOLONG, sFind5)
            grvDetail.SetFocusedRowCellValue(colPENYULIT, sFind6)
            grvDetail.SetFocusedRowCellValue(colBB, sFind7)
            grvDetail.SetFocusedRowCellValue(colPB, sFind8)
            grvDetail.SetFocusedRowCellValue(colKEADAAN, sFind9)

            grvDetail.UpdateCurrentRow()
        End If

        sFind1 = String.Empty
        sFind2 = String.Empty
        sFind3 = String.Empty
        sFind4 = String.Empty
        sFind5 = String.Empty
        sFind6 = String.Empty
        sFind7 = String.Empty
        sFind8 = String.Empty
        sFind9 = String.Empty

        grdDetail.Focus()
    End Sub

    Private Sub DeleteToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DeleteToolStripMenuItem.Click
        grvDetail.DeleteSelectedRows()
    End Sub

    Private Sub frmEMedrekRI_29_MouseWheel(sender As Object, e As MouseEventArgs) Handles MyBase.MouseWheel
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
#End Region
End Class