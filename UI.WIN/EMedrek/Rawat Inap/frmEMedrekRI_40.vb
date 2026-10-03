Imports DataAccess
Imports UI.WIN.MAIN.My.Resources
Imports System.Linq
Imports System.Data.SqlClient

Public Class frmEMedrekRI_40
#Region "Declaration"
    Private oFormMode As FORM_MODE = FORM_MODE.FORM_MODE_VIEW
    Private isLoad As Boolean = False
    Private oS_DIGITAL_RI_40 As New Digital.clsDigital_RI_40
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
        txtDPJP.Text = DPJP
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

        chkASESMEN_01.Properties.ReadOnly = Status
        chkASESMEN_02.Properties.ReadOnly = Status
        txtASESMEN_03.Properties.ReadOnly = Status
        txtASESMEN_04.Properties.ReadOnly = Status
        txtASESMEN_05.Properties.ReadOnly = Status
        txtASESMEN_06.Properties.ReadOnly = Status
        chkASESMEN_07.Properties.ReadOnly = Status
        txtASESMEN_08.Properties.ReadOnly = Status
        txtASESMEN_09.Properties.ReadOnly = Status
        chkASESMEN_10.Properties.ReadOnly = Status
        chkASESMEN_11.Properties.ReadOnly = Status
        chkASESMEN_12.Properties.ReadOnly = Status
        txtASESMEN_13.Properties.ReadOnly = Status
        txtASESMEN_14.Properties.ReadOnly = Status
        txtASESMEN_15.Properties.ReadOnly = Status
        txtASESMEN_16.Properties.ReadOnly = Status
        txtASESMEN_17.Properties.ReadOnly = Status
        txtASESMEN_18.Properties.ReadOnly = Status
        txtASESMEN_19.Properties.ReadOnly = Status
        txtASESMEN_20.Properties.ReadOnly = Status
        chkASESMEN_21.Properties.ReadOnly = Status
        chkASESMEN_22.Properties.ReadOnly = Status
        chkASESMEN_23.Properties.ReadOnly = Status
        txtASESMEN_24.Properties.ReadOnly = Status
        txtASESMEN_25.Properties.ReadOnly = Status
        txtASESMEN_26.Properties.ReadOnly = Status
        chkASESMEN_27.Properties.ReadOnly = Status
        chkASESMEN_28.Properties.ReadOnly = Status
        chkASESMEN_29.Properties.ReadOnly = Status
        txtASESMEN_30.Properties.ReadOnly = Status
        chkASESMEN_31.Properties.ReadOnly = Status
        chkASESMEN_32.Properties.ReadOnly = Status
        chkASESMEN_33.Properties.ReadOnly = Status
        chkASESMEN_34.Properties.ReadOnly = Status
        txtASESMEN_35.Properties.ReadOnly = Status
        chkASESMEN_36.Properties.ReadOnly = Status
        chkASESMEN_37.Properties.ReadOnly = Status
        txtASESMEN_38.Properties.ReadOnly = Status
        txtASESMEN_39.Properties.ReadOnly = Status
        txtASESMEN_40.Properties.ReadOnly = Status
        txtASESMEN_41.Properties.ReadOnly = Status
        chkASESMEN_42.Properties.ReadOnly = Status
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
        txtASESMEN_54.Properties.ReadOnly = Status
        chkASESMEN_55.Properties.ReadOnly = Status
        txtASESMEN_56.Properties.ReadOnly = Status
        chkASESMEN_57.Properties.ReadOnly = Status
        chkASESMEN_58.Properties.ReadOnly = Status
        txtASESMEN_59.Properties.ReadOnly = Status
        chkASESMEN_60.Properties.ReadOnly = Status
        chkASESMEN_61.Properties.ReadOnly = Status
        chkASESMEN_62.Properties.ReadOnly = Status
        chkASESMEN_63.Properties.ReadOnly = Status
        chkASESMEN_64.Properties.ReadOnly = Status
        chkASESMEN_65.Properties.ReadOnly = Status
        chkASESMEN_66.Properties.ReadOnly = Status
        chkASESMEN_67.Properties.ReadOnly = Status
        txtASESMEN_68.Properties.ReadOnly = Status
        txtASESMEN_69.Properties.ReadOnly = Status
        txtASESMEN_70.Properties.ReadOnly = Status
        chkASESMEN_71.Properties.ReadOnly = Status
        chkASESMEN_72.Properties.ReadOnly = Status
        chkASESMEN_73.Properties.ReadOnly = Status
        chkASESMEN_74.Properties.ReadOnly = Status
        chkASESMEN_75.Properties.ReadOnly = Status
        txtASESMEN_76.Properties.ReadOnly = Status
        chkASESMEN_77.Properties.ReadOnly = Status
        chkASESMEN_78.Properties.ReadOnly = Status
        txtASESMEN_79.Properties.ReadOnly = Status
        txtASESMEN_80.Properties.ReadOnly = Status
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
        chkASESMEN_91.Properties.ReadOnly = Status
        chkASESMEN_92.Properties.ReadOnly = Status
        chkASESMEN_93.Properties.ReadOnly = Status
        chkASESMEN_94.Properties.ReadOnly = Status
        chkASESMEN_95.Properties.ReadOnly = Status
        chkASESMEN_96.Properties.ReadOnly = Status
        chkASESMEN_97.Properties.ReadOnly = Status
        txtASESMEN_98.Properties.ReadOnly = Status
        chkASESMEN_99.Properties.ReadOnly = Status
        chkASESMEN_100.Properties.ReadOnly = Status
        txtASESMEN_101.Properties.ReadOnly = Status
        txtASESMEN_102.Properties.ReadOnly = Status
        'txtASESMEN_103.Properties.ReadOnly = Status
        chkASESMEN_104.Properties.ReadOnly = Status
        txtASESMEN_105.Properties.ReadOnly = Status
        chkASESMEN_106.Properties.ReadOnly = Status
        'chkASESMEN_107.Properties.ReadOnly = Status
        'chkASESMEN_108.Properties.ReadOnly = Status
        'chkASESMEN_109.Properties.ReadOnly = Status
        'chkASESMEN_110.Properties.ReadOnly = Status
        'chkASESMEN_111.Properties.ReadOnly = Status
        'txtASESMEN_112.Properties.ReadOnly = Status
        'chkASESMEN_113.Properties.ReadOnly = Status
        'chkASESMEN_114.Properties.ReadOnly = Status
        'chkASESMEN_115.Properties.ReadOnly = Status
        'chkASESMEN_116.Properties.ReadOnly = Status
        'chkASESMEN_117.Properties.ReadOnly = Status
        'chkASESMEN_118.Properties.ReadOnly = Status
        'chkASESMEN_119.Properties.ReadOnly = Status
        'txtASESMEN_120.Properties.ReadOnly = Status
        'txtASESMEN_121.Properties.ReadOnly = Status
        'txtASESMEN_122.Properties.ReadOnly = Status
        'txtASESMEN_123.Properties.ReadOnly = Status
        'chkASESMEN_124.Properties.ReadOnly = Status
        'chkASESMEN_125.Properties.ReadOnly = Status
        'chkASESMEN_126.Properties.ReadOnly = Status
        'chkASESMEN_127.Properties.ReadOnly = Status
        'txtASESMEN_128.Properties.ReadOnly = Status
        'txtASESMEN_129.Properties.ReadOnly = Status
        'chkASESMEN_130.Properties.ReadOnly = Status
        'txtASESMEN_131.Properties.ReadOnly = Status
        'chkASESMEN_132.Properties.ReadOnly = Status
        'chkASESMEN_133.Properties.ReadOnly = Status
        'txtASESMEN_134.Properties.ReadOnly = Status
        'chkASESMEN_135.Properties.ReadOnly = Status
        'txtASESMEN_136.Properties.ReadOnly = Status
        'chkASESMEN_137.Properties.ReadOnly = Status
        'txtASESMEN_138.Properties.ReadOnly = Status
        'txtASESMEN_139.Properties.ReadOnly = Status
        'chkASESMEN_140.Properties.ReadOnly = Status
        'chkASESMEN_141.Properties.ReadOnly = Status
        'chkASESMEN_142.Properties.ReadOnly = Status
        'txtASESMEN_143.Properties.ReadOnly = Status
        'chkASESMEN_144.Properties.ReadOnly = Status
        'txtASESMEN_145.Properties.ReadOnly = Status
        'chkASESMEN_146.Properties.ReadOnly = Status
        'txtASESMEN_147.Properties.ReadOnly = Status
        'txtASESMEN_148.Properties.ReadOnly = Status
        'chkASESMEN_149.Properties.ReadOnly = Status
        'chkASESMEN_150.Properties.ReadOnly = Status
        'chkASESMEN_151.Properties.ReadOnly = Status
        'chkASESMEN_152.Properties.ReadOnly = Status
        'txtASESMEN_153.Properties.ReadOnly = Status
        'chkASESMEN_154.Properties.ReadOnly = Status
        'chkASESMEN_155.Properties.ReadOnly = Status
        'chkASESMEN_156.Properties.ReadOnly = Status
        'chkASESMEN_157.Properties.ReadOnly = Status
        'chkASESMEN_158.Properties.ReadOnly = Status
        'chkASESMEN_159.Properties.ReadOnly = Status
        'chkASESMEN_160.Properties.ReadOnly = Status
        'chkASESMEN_161.Properties.ReadOnly = Status
        'chkASESMEN_162.Properties.ReadOnly = Status
        'chkASESMEN_163.Properties.ReadOnly = Status
        'chkASESMEN_164.Properties.ReadOnly = Status
        'chkASESMEN_165.Properties.ReadOnly = Status
        'chkASESMEN_166.Properties.ReadOnly = Status
        'chkASESMEN_167.Properties.ReadOnly = Status
        'txtASESMEN_168.Properties.ReadOnly = Status
        'chkASESMEN_169.Properties.ReadOnly = Status
        'chkASESMEN_170.Properties.ReadOnly = Status
        'txtASESMEN_171.Properties.ReadOnly = Status
        'chkASESMEN_172.Properties.ReadOnly = Status
        'chkASESMEN_173.Properties.ReadOnly = Status
        'chkASESMEN_174.Properties.ReadOnly = Status
        'chkASESMEN_175.Properties.ReadOnly = Status
        'chkASESMEN_176.Properties.ReadOnly = Status
        'chkASESMEN_177.Properties.ReadOnly = Status
        'chkASESMEN_178.Properties.ReadOnly = Status
        'txtASESMEN_179.Properties.ReadOnly = Status
        'chkASESMEN_180.Properties.ReadOnly = Status
        'txtASESMEN_181.Properties.ReadOnly = Status
        'chkASESMEN_182.Properties.ReadOnly = Status
        'txtASESMEN_183.Properties.ReadOnly = Status
        'chkASESMEN_184.Properties.ReadOnly = Status
        'txtASESMEN_185.Properties.ReadOnly = Status
        'chkASESMEN_186.Properties.ReadOnly = Status
        'chkASESMEN_187.Properties.ReadOnly = Status
        'txtASESMEN_188.Properties.ReadOnly = Status
        'chkASESMEN_189.Properties.ReadOnly = Status
        'chkASESMEN_190.Properties.ReadOnly = Status
        'txtASESMEN_191.Properties.ReadOnly = Status
        'txtASESMEN_192.Properties.ReadOnly = Status
        'txtASESMEN_193.Properties.ReadOnly = Status
        'txtASESMEN_194.Properties.ReadOnly = Status
        'chkASESMEN_195.Properties.ReadOnly = Status
        'chkASESMEN_196.Properties.ReadOnly = Status
        txtASESMEN_197.Properties.ReadOnly = Status
        txtASESMEN_198.Properties.ReadOnly = Status
        txtASESMEN_199.Properties.ReadOnly = Status
        txtASESMEN_200.Properties.ReadOnly = Status
        txtASESMEN_201.Properties.ReadOnly = Status
        txtASESMEN_202.Properties.ReadOnly = Status
        txtASESMEN_203.Properties.ReadOnly = Status
        'txtASESMEN_204.Properties.ReadOnly = Status
        chkASESMEN_205.Properties.ReadOnly = Status
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
        chkASESMEN_218.Properties.ReadOnly = Status
        chkASESMEN_219.Properties.ReadOnly = Status
        chkASESMEN_220.Properties.ReadOnly = Status
        chkASESMEN_221.Properties.ReadOnly = Status
        chkASESMEN_222.Properties.ReadOnly = Status
        chkASESMEN_223.Properties.ReadOnly = Status
        txtASESMEN_224.Properties.ReadOnly = Status
        txtASESMEN_225.Properties.ReadOnly = Status
        txtASESMEN_226.Properties.ReadOnly = Status
        txtASESMEN_227.Properties.ReadOnly = Status
        txtASESMEN_228.Properties.ReadOnly = Status
        'txtASESMEN_229.Properties.ReadOnly = Status
        chkASESMEN_230.Properties.ReadOnly = Status
        chkASESMEN_231.Properties.ReadOnly = Status
        chkASESMEN_232.Properties.ReadOnly = Status
        txtASESMEN_233.Properties.ReadOnly = Status
        txtASESMEN_234.Properties.ReadOnly = Status
        txtASESMEN_235.Properties.ReadOnly = Status
        txtASESMEN_236.Properties.ReadOnly = Status
        txtASESMEN_237.Properties.ReadOnly = Status
        txtASESMEN_238.Properties.ReadOnly = Status
        txtASESMEN_239.Properties.ReadOnly = Status
        txtASESMEN_240.Properties.ReadOnly = Status
        txtASESMEN_241.Properties.ReadOnly = Status
        txtASESMEN_242.Properties.ReadOnly = Status
        txtASESMEN_243.Properties.ReadOnly = Status
        txtASESMEN_244.Properties.ReadOnly = Status
        txtASESMEN_245.Properties.ReadOnly = Status
        txtASESMEN_246.Properties.ReadOnly = Status
        txtASESMEN_247.Properties.ReadOnly = Status
        txtASESMEN_248.Properties.ReadOnly = Status
        txtASESMEN_249.Properties.ReadOnly = Status
        txtASESMEN_250.Properties.ReadOnly = Status
        txtASESMEN_251.Properties.ReadOnly = Status
        txtASESMEN_252.Properties.ReadOnly = Status
        txtASESMEN_253.Properties.ReadOnly = Status
        txtASESMEN_254.Properties.ReadOnly = Status
        txtASESMEN_255.Properties.ReadOnly = Status
        txtASESMEN_256.Properties.ReadOnly = Status
        txtASESMEN_257.Properties.ReadOnly = Status
        txtASESMEN_258.Properties.ReadOnly = Status
        txtASESMEN_259.Properties.ReadOnly = Status
        txtASESMEN_260.Properties.ReadOnly = Status
        txtASESMEN_261.Properties.ReadOnly = Status
        txtASESMEN_262.Properties.ReadOnly = Status
        txtASESMEN_263.Properties.ReadOnly = Status
        txtASESMEN_264.Properties.ReadOnly = Status
        txtASESMEN_265.Properties.ReadOnly = Status
        txtASESMEN_266.Properties.ReadOnly = Status
        txtASESMEN_267.Properties.ReadOnly = Status
        txtASESMEN_268.Properties.ReadOnly = Status
        txtASESMEN_269.Properties.ReadOnly = Status
        txtASESMEN_270.Properties.ReadOnly = Status
        txtASESMEN_271.Properties.ReadOnly = Status
        txtASESMEN_272.Properties.ReadOnly = Status
        txtASESMEN_273.Properties.ReadOnly = Status
        txtASESMEN_274.Properties.ReadOnly = Status
        txtASESMEN_275.Properties.ReadOnly = Status
        txtASESMEN_276.Properties.ReadOnly = Status
        txtASESMEN_277.Properties.ReadOnly = Status
        txtASESMEN_278.Properties.ReadOnly = Status
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
        txtASESMEN_293.Properties.ReadOnly = Status
        txtASESMEN_294.Properties.ReadOnly = Status
        txtASESMEN_295.Properties.ReadOnly = Status
        txtASESMEN_296.Properties.ReadOnly = Status
        txtASESMEN_297.Properties.ReadOnly = Status
        txtASESMEN_298.Properties.ReadOnly = Status
        txtASESMEN_299.Properties.ReadOnly = Status
        txtASESMEN_300.Properties.ReadOnly = Status
        txtASESMEN_301.Properties.ReadOnly = Status
        txtASESMEN_302.Properties.ReadOnly = Status
        txtASESMEN_303.Properties.ReadOnly = Status
        txtASESMEN_304.Properties.ReadOnly = Status
        txtASESMEN_305.Properties.ReadOnly = Status
        txtASESMEN_306.Properties.ReadOnly = Status
        txtASESMEN_307.Properties.ReadOnly = Status
        txtASESMEN_308.Properties.ReadOnly = Status
        txtASESMEN_309.Properties.ReadOnly = Status
        txtASESMEN_310.Properties.ReadOnly = Status
        txtASESMEN_311.Properties.ReadOnly = Status
        txtASESMEN_312.Properties.ReadOnly = Status
        txtASESMEN_313.Properties.ReadOnly = Status
        txtASESMEN_314.Properties.ReadOnly = Status
        txtASESMEN_315.Properties.ReadOnly = Status
        txtASESMEN_316.Properties.ReadOnly = Status
        txtASESMEN_317.Properties.ReadOnly = Status
        txtASESMEN_318.Properties.ReadOnly = Status
        txtASESMEN_319.Properties.ReadOnly = Status
        txtASESMEN_320.Properties.ReadOnly = Status
        txtASESMEN_321.Properties.ReadOnly = Status
        txtASESMEN_322.Properties.ReadOnly = Status
        txtASESMEN_323.Properties.ReadOnly = Status
        txtASESMEN_324.Properties.ReadOnly = Status
        txtASESMEN_325.Properties.ReadOnly = Status
        txtASESMEN_326.Properties.ReadOnly = Status
        txtASESMEN_327.Properties.ReadOnly = Status
        txtASESMEN_328.Properties.ReadOnly = Status
        txtASESMEN_329.Properties.ReadOnly = Status
        txtASESMEN_330.Properties.ReadOnly = Status
        txtASESMEN_331.Properties.ReadOnly = Status
        txtASESMEN_332.Properties.ReadOnly = Status
        txtASESMEN_333.Properties.ReadOnly = Status
        txtASESMEN_334.Properties.ReadOnly = Status
        txtASESMEN_335.Properties.ReadOnly = Status
        txtASESMEN_336.Properties.ReadOnly = Status
        txtASESMEN_337.Properties.ReadOnly = Status
        txtASESMEN_338.Properties.ReadOnly = Status
        txtASESMEN_339.Properties.ReadOnly = Status
        txtASESMEN_340.Properties.ReadOnly = Status
        txtASESMEN_341.Properties.ReadOnly = Status
        txtASESMEN_342.Properties.ReadOnly = Status
        txtASESMEN_343.Properties.ReadOnly = Status
        txtASESMEN_344.Properties.ReadOnly = Status
        txtASESMEN_345.Properties.ReadOnly = Status
        txtASESMEN_346.Properties.ReadOnly = Status
        txtASESMEN_347.Properties.ReadOnly = Status
        txtASESMEN_348.Properties.ReadOnly = Status
        txtASESMEN_349.Properties.ReadOnly = Status
        txtASESMEN_350.Properties.ReadOnly = Status
        txtASESMEN_351.Properties.ReadOnly = Status
        txtASESMEN_352.Properties.ReadOnly = Status
        txtASESMEN_353.Properties.ReadOnly = Status
        txtASESMEN_354.Properties.ReadOnly = Status
        txtASESMEN_355.Properties.ReadOnly = Status
        txtASESMEN_356.Properties.ReadOnly = Status
        txtASESMEN_357.Properties.ReadOnly = Status
        txtASESMEN_358.Properties.ReadOnly = Status
        txtASESMEN_359.Properties.ReadOnly = Status
        txtASESMEN_360.Properties.ReadOnly = Status
        txtASESMEN_361.Properties.ReadOnly = Status
        txtASESMEN_362.Properties.ReadOnly = Status
        txtASESMEN_363.Properties.ReadOnly = Status
        txtASESMEN_364.Properties.ReadOnly = Status
        txtASESMEN_365.Properties.ReadOnly = Status
        txtASESMEN_366.Properties.ReadOnly = Status
        txtASESMEN_367.Properties.ReadOnly = Status
        txtASESMEN_368.Properties.ReadOnly = Status
        txtASESMEN_369.Properties.ReadOnly = Status
        txtASESMEN_370.Properties.ReadOnly = Status
        txtASESMEN_371.Properties.ReadOnly = Status
        txtASESMEN_372.Properties.ReadOnly = Status
        chkASESMEN_373.Properties.ReadOnly = Status
        chkASESMEN_374.Properties.ReadOnly = Status
        chkASESMEN_375.Properties.ReadOnly = Status
        txtASESMEN_376.Properties.ReadOnly = Status
        chkASESMEN_377.Properties.ReadOnly = Status
        chkASESMEN_378.Properties.ReadOnly = Status
        chkASESMEN_379.Properties.ReadOnly = Status
        chkASESMEN_380.Properties.ReadOnly = Status
        chkASESMEN_381.Properties.ReadOnly = Status
        chkASESMEN_382.Properties.ReadOnly = Status
        txtASESMEN_383.Properties.ReadOnly = Status
        txtASESMEN_384.Properties.ReadOnly = Status
        txtASESMEN_385.Properties.ReadOnly = Status
        txtASESMEN_386.Properties.ReadOnly = Status
        txtASESMEN_387.Properties.ReadOnly = Status
        chkASESMEN_388.Properties.ReadOnly = Status
        chkASESMEN_389.Properties.ReadOnly = Status
        chkASESMEN_390.Properties.ReadOnly = Status
        chkASESMEN_391.Properties.ReadOnly = Status
        chkASESMEN_392.Properties.ReadOnly = Status
        chkASESMEN_393.Properties.ReadOnly = Status
        chkASESMEN_394.Properties.ReadOnly = Status
        chkASESMEN_395.Properties.ReadOnly = Status
        chkASESMEN_396.Properties.ReadOnly = Status
        chkASESMEN_397.Properties.ReadOnly = Status
        chkASESMEN_398.Properties.ReadOnly = Status
        txtASESMEN_399.Properties.ReadOnly = Status
        chkASESMEN_400.Properties.ReadOnly = Status
        chkASESMEN_401.Properties.ReadOnly = Status
        chkASESMEN_402.Properties.ReadOnly = Status
        chkASESMEN_403.Properties.ReadOnly = Status
        txtASESMEN_404.Properties.ReadOnly = Status
        chkASESMEN_405.Properties.ReadOnly = Status
        chkASESMEN_406.Properties.ReadOnly = Status
        chkASESMEN_407.Properties.ReadOnly = Status
        chkASESMEN_408.Properties.ReadOnly = Status
        chkASESMEN_409.Properties.ReadOnly = Status
        chkASESMEN_410.Properties.ReadOnly = Status
        chkASESMEN_411.Properties.ReadOnly = Status
        chkASESMEN_412.Properties.ReadOnly = Status
        chkASESMEN_413.Properties.ReadOnly = Status
        chkASESMEN_414.Properties.ReadOnly = Status
        chkASESMEN_415.Properties.ReadOnly = Status
        chkASESMEN_416.Properties.ReadOnly = Status
        chkASESMEN_417.Properties.ReadOnly = Status
        chkASESMEN_418.Properties.ReadOnly = Status
        chkASESMEN_419.Properties.ReadOnly = Status
        chkASESMEN_420.Properties.ReadOnly = Status
        chkASESMEN_421.Properties.ReadOnly = Status
        chkASESMEN_422.Properties.ReadOnly = Status
        chkASESMEN_423.Properties.ReadOnly = Status
        chkASESMEN_424.Properties.ReadOnly = Status
        chkASESMEN_425.Properties.ReadOnly = Status
        chkASESMEN_426.Properties.ReadOnly = Status
        chkASESMEN_427.Properties.ReadOnly = Status
        chkASESMEN_428.Properties.ReadOnly = Status
        chkASESMEN_429.Properties.ReadOnly = Status
        chkASESMEN_430.Properties.ReadOnly = Status
        chkASESMEN_431.Properties.ReadOnly = Status
        chkASESMEN_432.Properties.ReadOnly = Status
        chkASESMEN_433.Properties.ReadOnly = Status
        chkASESMEN_434.Properties.ReadOnly = Status
        chkASESMEN_435.Properties.ReadOnly = Status
        chkASESMEN_436.Properties.ReadOnly = Status
        chkASESMEN_437.Properties.ReadOnly = Status
        chkASESMEN_438.Properties.ReadOnly = Status
        txtASESMEN_439.Properties.ReadOnly = Status
        chkASESMEN_440.Properties.ReadOnly = Status
        txtJAM.Properties.ReadOnly = Status
        deDATE.Properties.ReadOnly = Status
        'txtPerawat.Properties.ReadOnly = Status

        deTANGGALSHD1.Properties.ReadOnly = Status
        deTANGGALSHD2.Properties.ReadOnly = Status
        deTANGGALSHD3.Properties.ReadOnly = Status
        deTANGGALSHD4.Properties.ReadOnly = Status
        deTANGGALSHD5.Properties.ReadOnly = Status
        deTANGGALSHD6.Properties.ReadOnly = Status
        deTANGGALSHD7.Properties.ReadOnly = Status

        txtSKORSHD1.Properties.ReadOnly = Status
        txtSKORSHD2.Properties.ReadOnly = Status
        txtSKORSHD3.Properties.ReadOnly = Status
        txtSKORSHD4.Properties.ReadOnly = Status
        txtSKORSHD5.Properties.ReadOnly = Status
        txtSKORSHD6.Properties.ReadOnly = Status
        txtSKORSHD7.Properties.ReadOnly = Status

        txtSKORSHD8.Properties.ReadOnly = Status
        txtSKORSHD9.Properties.ReadOnly = Status
        txtSKORSHD10.Properties.ReadOnly = Status
        txtSKORSHD11.Properties.ReadOnly = Status
        txtSKORSHD12.Properties.ReadOnly = Status
        txtSKORSHD13.Properties.ReadOnly = Status
        txtSKORSHD14.Properties.ReadOnly = Status

        txtSKORSHD15.Properties.ReadOnly = Status
        txtSKORSHD16.Properties.ReadOnly = Status
        txtSKORSHD17.Properties.ReadOnly = Status
        txtSKORSHD18.Properties.ReadOnly = Status
        txtSKORSHD19.Properties.ReadOnly = Status
        txtSKORSHD20.Properties.ReadOnly = Status
        txtSKORSHD21.Properties.ReadOnly = Status

        txtSKORSHD22.Properties.ReadOnly = Status
        txtSKORSHD23.Properties.ReadOnly = Status
        txtSKORSHD24.Properties.ReadOnly = Status
        txtSKORSHD25.Properties.ReadOnly = Status
        txtSKORSHD26.Properties.ReadOnly = Status
        txtSKORSHD27.Properties.ReadOnly = Status
        txtSKORSHD28.Properties.ReadOnly = Status

        txtSKORSHD29.Properties.ReadOnly = Status
        txtSKORSHD30.Properties.ReadOnly = Status
        txtSKORSHD31.Properties.ReadOnly = Status
        txtSKORSHD32.Properties.ReadOnly = Status
        txtSKORSHD33.Properties.ReadOnly = Status
        txtSKORSHD34.Properties.ReadOnly = Status
        txtSKORSHD35.Properties.ReadOnly = Status

        txtSKORSHD36.Properties.ReadOnly = Status
        txtSKORSHD37.Properties.ReadOnly = Status
        txtSKORSHD38.Properties.ReadOnly = Status
        txtSKORSHD39.Properties.ReadOnly = Status
        txtSKORSHD40.Properties.ReadOnly = Status
        txtSKORSHD41.Properties.ReadOnly = Status
        txtSKORSHD42.Properties.ReadOnly = Status

        'txtASESMEN_204
        txtTOTALSKOR2.Properties.ReadOnly = Status
        txtTOTALSKOR3.Properties.ReadOnly = Status
        txtTOTALSKOR4.Properties.ReadOnly = Status
        txtTOTALSKOR5.Properties.ReadOnly = Status
        txtTOTALSKOR6.Properties.ReadOnly = Status
        txtTOTALSKOR7.Properties.ReadOnly = Status

        'monitorresiko
        deTANGGALMORR_1.Properties.ReadOnly = Status
        deTANGGALMORR_2.Properties.ReadOnly = Status
        deTANGGALMORR_3.Properties.ReadOnly = Status
        deTANGGALMORR_4.Properties.ReadOnly = Status
        deTANGGALMORR_5.Properties.ReadOnly = Status
        deTANGGALMORR_6.Properties.ReadOnly = Status
        deTANGGALMORR_7.Properties.ReadOnly = Status

        chkMORR_1.Properties.ReadOnly = Status
        chkMORR_2.Properties.ReadOnly = Status
        chkMORR_3.Properties.ReadOnly = Status
        chkMORR_4.Properties.ReadOnly = Status
        chkMORR_5.Properties.ReadOnly = Status
        chkMORR_6.Properties.ReadOnly = Status
        chkMORR_7.Properties.ReadOnly = Status
        chkMORR_8.Properties.ReadOnly = Status
        chkMORR_9.Properties.ReadOnly = Status

        chkMORR_10.Properties.ReadOnly = Status
        chkMORR_11.Properties.ReadOnly = Status
        chkMORR_12.Properties.ReadOnly = Status
        chkMORR_13.Properties.ReadOnly = Status
        chkMORR_14.Properties.ReadOnly = Status
        chkMORR_15.Properties.ReadOnly = Status
        chkMORR_16.Properties.ReadOnly = Status
        chkMORR_17.Properties.ReadOnly = Status
        chkMORR_18.Properties.ReadOnly = Status

        chkMORR_19.Properties.ReadOnly = Status
        chkMORR_20.Properties.ReadOnly = Status
        chkMORR_21.Properties.ReadOnly = Status
        chkMORR_22.Properties.ReadOnly = Status
        chkMORR_23.Properties.ReadOnly = Status
        chkMORR_24.Properties.ReadOnly = Status
        chkMORR_25.Properties.ReadOnly = Status
        chkMORR_26.Properties.ReadOnly = Status
        chkMORR_27.Properties.ReadOnly = Status

        chkMORR_28.Properties.ReadOnly = Status
        chkMORR_29.Properties.ReadOnly = Status
        chkMORR_30.Properties.ReadOnly = Status
        chkMORR_31.Properties.ReadOnly = Status
        chkMORR_32.Properties.ReadOnly = Status
        chkMORR_33.Properties.ReadOnly = Status
        chkMORR_34.Properties.ReadOnly = Status
        chkMORR_35.Properties.ReadOnly = Status
        chkMORR_36.Properties.ReadOnly = Status

        chkMORR_37.Properties.ReadOnly = Status
        chkMORR_38.Properties.ReadOnly = Status
        chkMORR_39.Properties.ReadOnly = Status
        chkMORR_40.Properties.ReadOnly = Status
        chkMORR_41.Properties.ReadOnly = Status
        chkMORR_42.Properties.ReadOnly = Status
        chkMORR_43.Properties.ReadOnly = Status
        chkMORR_44.Properties.ReadOnly = Status
        chkMORR_45.Properties.ReadOnly = Status

        chkMORR_46.Properties.ReadOnly = Status
        chkMORR_47.Properties.ReadOnly = Status
        chkMORR_48.Properties.ReadOnly = Status
        chkMORR_49.Properties.ReadOnly = Status
        chkMORR_50.Properties.ReadOnly = Status
        chkMORR_51.Properties.ReadOnly = Status
        chkMORR_52.Properties.ReadOnly = Status
        chkMORR_53.Properties.ReadOnly = Status
        chkMORR_54.Properties.ReadOnly = Status

        txtPETUGASMORR_1.Properties.ReadOnly = Status
        txtPETUGASMORR_2.Properties.ReadOnly = Status
        txtPETUGASMORR_3.Properties.ReadOnly = Status
        txtPETUGASMORR_4.Properties.ReadOnly = Status
        txtPETUGASMORR_5.Properties.ReadOnly = Status
        txtPETUGASMORR_6.Properties.ReadOnly = Status
        txtPETUGASMORR_7.Properties.ReadOnly = Status


        deTANGGALMORT_1.Properties.ReadOnly = Status
        deTANGGALMORT_2.Properties.ReadOnly = Status
        deTANGGALMORT_3.Properties.ReadOnly = Status
        deTANGGALMORT_4.Properties.ReadOnly = Status
        deTANGGALMORT_5.Properties.ReadOnly = Status
        deTANGGALMORT_6.Properties.ReadOnly = Status
        deTANGGALMORT_7.Properties.ReadOnly = Status

        chkMORT_1.Properties.ReadOnly = Status
        chkMORT_2.Properties.ReadOnly = Status
        chkMORT_3.Properties.ReadOnly = Status
        chkMORT_4.Properties.ReadOnly = Status
        chkMORT_5.Properties.ReadOnly = Status
        chkMORT_6.Properties.ReadOnly = Status
        chkMORT_7.Properties.ReadOnly = Status
        chkMORT_8.Properties.ReadOnly = Status

        chkMORT_9.Properties.ReadOnly = Status
        chkMORT_10.Properties.ReadOnly = Status
        chkMORT_11.Properties.ReadOnly = Status
        chkMORT_12.Properties.ReadOnly = Status
        chkMORT_13.Properties.ReadOnly = Status
        chkMORT_14.Properties.ReadOnly = Status
        chkMORT_15.Properties.ReadOnly = Status
        chkMORT_16.Properties.ReadOnly = Status

        chkMORT_17.Properties.ReadOnly = Status
        chkMORT_18.Properties.ReadOnly = Status
        chkMORT_19.Properties.ReadOnly = Status
        chkMORT_20.Properties.ReadOnly = Status
        chkMORT_21.Properties.ReadOnly = Status
        chkMORT_22.Properties.ReadOnly = Status
        chkMORT_23.Properties.ReadOnly = Status
        chkMORT_24.Properties.ReadOnly = Status

        chkMORT_25.Properties.ReadOnly = Status
        chkMORT_26.Properties.ReadOnly = Status
        chkMORT_27.Properties.ReadOnly = Status
        chkMORT_28.Properties.ReadOnly = Status
        chkMORT_29.Properties.ReadOnly = Status
        chkMORT_30.Properties.ReadOnly = Status
        chkMORT_31.Properties.ReadOnly = Status
        chkMORT_32.Properties.ReadOnly = Status

        chkMORT_33.Properties.ReadOnly = Status
        chkMORT_34.Properties.ReadOnly = Status
        chkMORT_35.Properties.ReadOnly = Status
        chkMORT_36.Properties.ReadOnly = Status
        chkMORT_37.Properties.ReadOnly = Status
        chkMORT_38.Properties.ReadOnly = Status
        chkMORT_39.Properties.ReadOnly = Status
        chkMORT_40.Properties.ReadOnly = Status

        chkMORT_41.Properties.ReadOnly = Status
        chkMORT_42.Properties.ReadOnly = Status
        chkMORT_43.Properties.ReadOnly = Status
        chkMORT_44.Properties.ReadOnly = Status
        chkMORT_45.Properties.ReadOnly = Status
        chkMORT_46.Properties.ReadOnly = Status
        chkMORT_47.Properties.ReadOnly = Status
        chkMORT_48.Properties.ReadOnly = Status

        txtPETUGASMORT_1.Properties.ReadOnly = Status
        txtPETUGASMORT_2.Properties.ReadOnly = Status
        txtPETUGASMORT_3.Properties.ReadOnly = Status
        txtPETUGASMORT_4.Properties.ReadOnly = Status
        txtPETUGASMORT_5.Properties.ReadOnly = Status
        txtPETUGASMORT_6.Properties.ReadOnly = Status
        txtPETUGASMORT_7.Properties.ReadOnly = Status

        chkMASALAHKEP_1.Properties.ReadOnly = Status
        chkMASALAHKEP_2.Properties.ReadOnly = Status
        chkMASALAHKEP_3.Properties.ReadOnly = Status
        chkMASALAHKEP_4.Properties.ReadOnly = Status
        chkMASALAHKEP_5.Properties.ReadOnly = Status
        chkMASALAHKEP_6.Properties.ReadOnly = Status
        chkMASALAHKEP_7.Properties.ReadOnly = Status
        chkMASALAHKEP_8.Properties.ReadOnly = Status
        chkMASALAHKEP_9.Properties.ReadOnly = Status
        chkMASALAHKEP_10.Properties.ReadOnly = Status
        chkMASALAHKEP_11.Properties.ReadOnly = Status
        chkMASALAHKEP_12.Properties.ReadOnly = Status
        chkMASALAHKEP_13.Properties.ReadOnly = Status
        chkMASALAHKEP_14.Properties.ReadOnly = Status
        chkMASALAHKEP_15.Properties.ReadOnly = Status
        chkMASALAHKEP_16.Properties.ReadOnly = Status
        chkMASALAHKEP_17.Properties.ReadOnly = Status
        chkMASALAHKEP_18.Properties.ReadOnly = Status
        chkMASALAHKEP_19.Properties.ReadOnly = Status
        chkMASALAHKEP_20.Properties.ReadOnly = Status
        chkMASALAHKEP_21.Properties.ReadOnly = Status
        chkMASALAHKEP_22.Properties.ReadOnly = Status
        chkMASALAHKEP_23.Properties.ReadOnly = Status
        chkMASALAHKEP_24.Properties.ReadOnly = Status
        chkMASALAHKEP_25.Properties.ReadOnly = Status
        chkMASALAHKEP_26.Properties.ReadOnly = Status
        chkMASALAHKEP_27.Properties.ReadOnly = Status
        chkMASALAHKEP_28.Properties.ReadOnly = Status
        chkMASALAHKEP_29.Properties.ReadOnly = Status
        chkMASALAHKEP_30.Properties.ReadOnly = Status
        chkMASALAHKEP_31.Properties.ReadOnly = Status
        chkMASALAHKEP_32.Properties.ReadOnly = Status
        chkMASALAHKEP_33.Properties.ReadOnly = Status
        chkMASALAHKEP_34.Properties.ReadOnly = Status
        chkMASALAHKEP_35.Properties.ReadOnly = Status
        chkMASALAHKEP_36.Properties.ReadOnly = Status
        chkMASALAHKEP_37.Properties.ReadOnly = Status
        chkMASALAHKEP_38.Properties.ReadOnly = Status
        chkMASALAHKEP_39.Properties.ReadOnly = Status
        chkMASALAHKEP_40.Properties.ReadOnly = Status
        chkMASALAHKEP_41.Properties.ReadOnly = Status
        chkMASALAHKEP_42.Properties.ReadOnly = Status
        chkMASALAHKEP_43.Properties.ReadOnly = Status
        chkMASALAHKEP_44.Properties.ReadOnly = Status
        txtMASALAHKEP_44_TEXT.Properties.ReadOnly = Status


        TextEdit1.Properties.ReadOnly = Status
        TextEdit2.Properties.ReadOnly = Status
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

        CheckEdit1.Properties.ReadOnly = Status
        CheckEdit2.Properties.ReadOnly = Status
        CheckEdit3.Properties.ReadOnly = Status
        CheckEdit4.Properties.ReadOnly = Status
        CheckEdit5.Properties.ReadOnly = Status
        CheckEdit6.Properties.ReadOnly = Status
        CheckEdit7.Properties.ReadOnly = Status
        CheckEdit8.Properties.ReadOnly = Status
        CheckEdit9.Properties.ReadOnly = Status
        CheckEdit10.Properties.ReadOnly = Status
        CheckEdit11.Properties.ReadOnly = Status
        CheckEdit12.Properties.ReadOnly = Status
        CheckEdit13.Properties.ReadOnly = Status
        CheckEdit14.Properties.ReadOnly = Status
        CheckEdit15.Properties.ReadOnly = Status
        CheckEdit16.Properties.ReadOnly = Status
        CheckEdit17.Properties.ReadOnly = Status
        CheckEdit18.Properties.ReadOnly = Status
        CheckEdit19.Properties.ReadOnly = Status
        CheckEdit20.Properties.ReadOnly = Status
        txtSuku.Properties.ReadOnly = Status
    End Sub
    Private Sub fn_EmptyMe()
        chkASESMEN_01.Checked = False
        chkASESMEN_02.Checked = False
        txtASESMEN_03.ResetText()
        txtASESMEN_04.ResetText()
        txtASESMEN_05.ResetText()
        txtASESMEN_06.ResetText()
        chkASESMEN_07.Checked = False
        txtASESMEN_08.ResetText()
        txtASESMEN_09.ResetText()
        chkASESMEN_10.Checked = False
        chkASESMEN_11.Checked = False
        chkASESMEN_12.Checked = False
        txtASESMEN_13.ResetText()
        txtASESMEN_14.ResetText()
        txtASESMEN_15.ResetText()
        txtASESMEN_16.ResetText()
        txtASESMEN_17.ResetText()
        txtASESMEN_18.ResetText()
        txtASESMEN_19.ResetText()
        txtASESMEN_20.ResetText()
        chkASESMEN_21.Checked = False
        chkASESMEN_22.Checked = False
        chkASESMEN_23.Checked = False
        txtASESMEN_24.ResetText()
        txtASESMEN_25.ResetText()
        txtASESMEN_26.ResetText()
        chkASESMEN_27.Checked = False
        chkASESMEN_28.Checked = False
        chkASESMEN_29.Checked = False
        txtASESMEN_30.ResetText()
        chkASESMEN_31.Checked = False
        chkASESMEN_32.Checked = False
        chkASESMEN_33.Checked = False
        chkASESMEN_34.Checked = False
        txtASESMEN_35.ResetText()
        chkASESMEN_36.Checked = False
        chkASESMEN_37.Checked = False
        txtASESMEN_38.ResetText()
        txtASESMEN_39.ResetText()
        txtASESMEN_40.ResetText()
        txtASESMEN_41.ResetText()
        chkASESMEN_42.Checked = False
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
        txtASESMEN_54.ResetText()
        chkASESMEN_55.Checked = False
        txtASESMEN_56.ResetText()
        chkASESMEN_57.Checked = False
        chkASESMEN_58.Checked = False
        txtASESMEN_59.ResetText()
        chkASESMEN_60.Checked = False
        chkASESMEN_61.Checked = False
        chkASESMEN_62.Checked = False
        chkASESMEN_63.Checked = False
        chkASESMEN_64.Checked = False
        chkASESMEN_65.Checked = False
        chkASESMEN_66.Checked = False
        chkASESMEN_67.Checked = False
        txtASESMEN_68.ResetText()
        txtASESMEN_69.ResetText()
        txtASESMEN_70.ResetText()
        chkASESMEN_71.Checked = False
        chkASESMEN_72.Checked = False
        chkASESMEN_73.Checked = False
        chkASESMEN_74.Checked = False
        chkASESMEN_75.Checked = False
        txtASESMEN_76.ResetText()
        chkASESMEN_77.Checked = False
        chkASESMEN_78.Checked = False
        txtASESMEN_79.ResetText()
        txtASESMEN_80.ResetText()
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
        chkASESMEN_91.Checked = False
        chkASESMEN_92.Checked = False
        chkASESMEN_93.Checked = False
        chkASESMEN_94.Checked = False
        chkASESMEN_95.Checked = False
        chkASESMEN_96.Checked = False
        chkASESMEN_97.Checked = False
        txtASESMEN_98.ResetText()
        chkASESMEN_99.Checked = False
        chkASESMEN_100.Checked = False
        txtASESMEN_101.ResetText()
        txtASESMEN_102.ResetText()
        txtASESMEN_103.ResetText()
        chkASESMEN_104.Checked = False
        txtASESMEN_105.ResetText()
        chkASESMEN_106.Checked = False
        'chkASESMEN_107.Checked = False
        'chkASESMEN_108.Checked = False
        'chkASESMEN_109.Checked = False
        'chkASESMEN_110.Checked = False
        'chkASESMEN_111.Checked = False
        'txtASESMEN_112.ResetText()
        'chkASESMEN_113.Checked = False
        'chkASESMEN_114.Checked = False
        'chkASESMEN_115.Checked = False
        'chkASESMEN_116.Checked = False
        'chkASESMEN_117.Checked = False
        'chkASESMEN_118.Checked = False
        'chkASESMEN_119.Checked = False
        'txtASESMEN_120.ResetText()
        'txtASESMEN_121.ResetText()
        'txtASESMEN_122.ResetText()
        'txtASESMEN_123.ResetText()
        'chkASESMEN_124.Checked = False
        'chkASESMEN_125.Checked = False
        'chkASESMEN_126.Checked = False
        'chkASESMEN_127.Checked = False
        'txtASESMEN_128.ResetText()
        'txtASESMEN_129.ResetText()
        'chkASESMEN_130.Checked = False
        'txtASESMEN_131.ResetText()
        'chkASESMEN_132.Checked = False
        'chkASESMEN_133.Checked = False
        'txtASESMEN_134.ResetText()
        'chkASESMEN_135.Checked = False
        'txtASESMEN_136.ResetText()
        'chkASESMEN_137.Checked = False
        'txtASESMEN_138.ResetText()
        'txtASESMEN_139.ResetText()
        'chkASESMEN_140.Checked = False
        'chkASESMEN_141.Checked = False
        'chkASESMEN_142.Checked = False
        'txtASESMEN_143.ResetText()
        'chkASESMEN_144.Checked = False
        'txtASESMEN_145.ResetText()
        'chkASESMEN_146.Checked = False
        'txtASESMEN_147.ResetText()
        'txtASESMEN_148.ResetText()
        'chkASESMEN_149.Checked = False
        'chkASESMEN_150.Checked = False
        'chkASESMEN_151.Checked = False
        'chkASESMEN_152.Checked = False
        'txtASESMEN_153.ResetText()
        'chkASESMEN_154.Checked = False
        'chkASESMEN_155.Checked = False
        'chkASESMEN_156.Checked = False
        'chkASESMEN_157.Checked = False
        'chkASESMEN_158.Checked = False
        'chkASESMEN_159.Checked = False
        'chkASESMEN_160.Checked = False
        'chkASESMEN_161.Checked = False
        'chkASESMEN_162.Checked = False
        'chkASESMEN_163.Checked = False
        'chkASESMEN_164.Checked = False
        'chkASESMEN_165.Checked = False
        'chkASESMEN_166.Checked = False
        'chkASESMEN_167.Checked = False
        'txtASESMEN_168.ResetText()
        'chkASESMEN_169.Checked = False
        'chkASESMEN_170.Checked = False
        'txtASESMEN_171.ResetText()
        'chkASESMEN_172.Checked = False
        'chkASESMEN_173.Checked = False
        'chkASESMEN_174.Checked = False
        'chkASESMEN_175.Checked = False
        'chkASESMEN_176.Checked = False
        'chkASESMEN_177.Checked = False
        'chkASESMEN_178.Checked = False
        'txtASESMEN_179.ResetText()
        'chkASESMEN_180.Checked = False
        'txtASESMEN_181.ResetText()
        'chkASESMEN_182.Checked = False
        'txtASESMEN_183.ResetText()
        'chkASESMEN_184.Checked = False
        'txtASESMEN_185.ResetText()
        'chkASESMEN_186.Checked = False
        'chkASESMEN_187.Checked = False
        'txtASESMEN_188.ResetText()
        'chkASESMEN_189.Checked = False
        'chkASESMEN_190.Checked = False
        'txtASESMEN_191.ResetText()
        'txtASESMEN_192.ResetText()
        'txtASESMEN_193.ResetText()
        'txtASESMEN_194.ResetText()
        'chkASESMEN_195.Checked = False
        'chkASESMEN_196.Checked = False
        txtASESMEN_197.ResetText()
        txtASESMEN_198.ResetText()
        txtASESMEN_199.ResetText()
        txtASESMEN_200.ResetText()
        txtASESMEN_201.ResetText()
        txtASESMEN_202.ResetText()
        txtASESMEN_203.ResetText()
        txtASESMEN_204.ResetText()
        chkASESMEN_205.Checked = False
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
        chkASESMEN_218.Checked = False
        chkASESMEN_219.Checked = False
        chkASESMEN_220.Checked = False
        chkASESMEN_221.Checked = False
        chkASESMEN_222.Checked = False
        chkASESMEN_223.Checked = False
        txtASESMEN_224.ResetText()
        txtASESMEN_225.ResetText()
        txtASESMEN_226.ResetText()
        txtASESMEN_227.ResetText()
        txtASESMEN_228.ResetText()
        txtASESMEN_229.ResetText()
        chkASESMEN_230.Checked = False
        chkASESMEN_231.Checked = False
        chkASESMEN_232.Checked = False
        txtASESMEN_233.ResetText()
        txtASESMEN_234.ResetText()
        txtASESMEN_235.ResetText()
        txtASESMEN_236.ResetText()
        txtASESMEN_237.ResetText()
        txtASESMEN_238.ResetText()
        txtASESMEN_239.ResetText()
        txtASESMEN_240.ResetText()
        txtASESMEN_241.ResetText()
        txtASESMEN_242.ResetText()
        txtASESMEN_243.ResetText()
        txtASESMEN_244.ResetText()
        txtASESMEN_245.ResetText()
        txtASESMEN_246.ResetText()
        txtASESMEN_247.ResetText()
        txtASESMEN_248.ResetText()
        txtASESMEN_249.ResetText()
        txtASESMEN_250.ResetText()
        txtASESMEN_251.ResetText()
        txtASESMEN_252.ResetText()
        txtASESMEN_253.ResetText()
        txtASESMEN_254.ResetText()
        txtASESMEN_255.ResetText()
        txtASESMEN_256.ResetText()
        txtASESMEN_257.ResetText()
        txtASESMEN_258.ResetText()
        txtASESMEN_259.ResetText()
        txtASESMEN_260.ResetText()
        txtASESMEN_261.ResetText()
        txtASESMEN_262.ResetText()
        txtASESMEN_263.ResetText()
        txtASESMEN_264.ResetText()
        txtASESMEN_265.ResetText()
        txtASESMEN_266.ResetText()
        txtASESMEN_267.ResetText()
        txtASESMEN_268.ResetText()
        txtASESMEN_269.ResetText()
        txtASESMEN_270.ResetText()
        txtASESMEN_271.ResetText()
        txtASESMEN_272.ResetText()
        txtASESMEN_273.ResetText()
        txtASESMEN_274.ResetText()
        txtASESMEN_275.ResetText()
        txtASESMEN_276.ResetText()
        txtASESMEN_277.ResetText()
        txtASESMEN_278.ResetText()
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
        txtASESMEN_293.ResetText()
        txtASESMEN_294.ResetText()
        txtASESMEN_295.ResetText()
        txtASESMEN_296.ResetText()
        txtASESMEN_297.ResetText()
        txtASESMEN_298.ResetText()
        txtASESMEN_299.ResetText()
        txtASESMEN_300.ResetText()
        txtASESMEN_301.ResetText()
        txtASESMEN_302.ResetText()
        txtASESMEN_303.ResetText()
        txtASESMEN_304.ResetText()
        txtASESMEN_305.ResetText()
        txtASESMEN_306.ResetText()
        txtASESMEN_307.ResetText()
        txtASESMEN_308.ResetText()
        txtASESMEN_309.ResetText()
        txtASESMEN_310.ResetText()
        txtASESMEN_311.ResetText()
        txtASESMEN_312.ResetText()
        txtASESMEN_313.ResetText()
        txtASESMEN_314.ResetText()
        txtASESMEN_315.ResetText()
        txtASESMEN_316.ResetText()
        txtASESMEN_317.ResetText()
        txtASESMEN_318.ResetText()
        txtASESMEN_319.ResetText()
        txtASESMEN_320.ResetText()
        txtASESMEN_321.ResetText()
        txtASESMEN_322.ResetText()
        txtASESMEN_323.ResetText()
        txtASESMEN_324.ResetText()
        txtASESMEN_325.ResetText()
        txtASESMEN_326.ResetText()
        txtASESMEN_327.ResetText()
        txtASESMEN_328.ResetText()
        txtASESMEN_329.ResetText()
        txtASESMEN_330.ResetText()
        txtASESMEN_331.ResetText()
        txtASESMEN_332.ResetText()
        txtASESMEN_333.ResetText()
        txtASESMEN_334.ResetText()
        txtASESMEN_335.ResetText()
        txtASESMEN_336.ResetText()
        txtASESMEN_337.ResetText()
        txtASESMEN_338.ResetText()
        txtASESMEN_339.ResetText()
        txtASESMEN_340.ResetText()
        txtASESMEN_341.ResetText()
        txtASESMEN_342.ResetText()
        txtASESMEN_343.ResetText()
        txtASESMEN_344.ResetText()
        txtASESMEN_345.ResetText()
        txtASESMEN_346.ResetText()
        txtASESMEN_347.ResetText()
        txtASESMEN_348.ResetText()
        txtASESMEN_349.ResetText()
        txtASESMEN_350.ResetText()
        txtASESMEN_351.ResetText()
        txtASESMEN_352.ResetText()
        txtASESMEN_353.ResetText()
        txtASESMEN_354.ResetText()
        txtASESMEN_355.ResetText()
        txtASESMEN_356.ResetText()
        txtASESMEN_357.ResetText()
        txtASESMEN_358.ResetText()
        txtASESMEN_359.ResetText()
        txtASESMEN_360.ResetText()
        txtASESMEN_361.ResetText()
        txtASESMEN_362.ResetText()
        txtASESMEN_363.ResetText()
        txtASESMEN_364.ResetText()
        txtASESMEN_365.ResetText()
        txtASESMEN_366.ResetText()
        txtASESMEN_367.ResetText()
        txtASESMEN_368.ResetText()
        txtASESMEN_369.ResetText()
        txtASESMEN_370.ResetText()
        txtASESMEN_371.ResetText()
        txtASESMEN_372.ResetText()
        chkASESMEN_373.Checked = False
        chkASESMEN_374.Checked = False
        chkASESMEN_375.Checked = False
        txtASESMEN_376.ResetText()
        chkASESMEN_377.Checked = False
        chkASESMEN_378.Checked = False
        chkASESMEN_379.Checked = False
        chkASESMEN_380.Checked = False
        chkASESMEN_381.Checked = False
        chkASESMEN_382.Checked = False
        txtASESMEN_383.ResetText()
        txtASESMEN_384.ResetText()
        txtASESMEN_385.ResetText()
        txtASESMEN_386.ResetText()
        txtASESMEN_387.ResetText()
        chkASESMEN_388.Checked = False
        chkASESMEN_389.Checked = False
        chkASESMEN_390.Checked = False
        chkASESMEN_391.Checked = False
        chkASESMEN_392.Checked = False
        chkASESMEN_393.Checked = False
        chkASESMEN_394.Checked = False
        chkASESMEN_395.Checked = False
        chkASESMEN_396.Checked = False
        chkASESMEN_397.Checked = False
        chkASESMEN_398.Checked = False
        txtASESMEN_399.ResetText()
        chkASESMEN_400.Checked = False
        chkASESMEN_401.Checked = False
        chkASESMEN_402.Checked = False
        chkASESMEN_403.Checked = False
        txtASESMEN_404.ResetText()
        chkASESMEN_405.Checked = False
        chkASESMEN_406.Checked = False
        chkASESMEN_407.Checked = False
        chkASESMEN_408.Checked = False
        chkASESMEN_409.Checked = False
        chkASESMEN_410.Checked = False
        chkASESMEN_411.Checked = False
        chkASESMEN_412.Checked = False
        chkASESMEN_413.Checked = False
        chkASESMEN_414.Checked = False
        chkASESMEN_415.Checked = False
        chkASESMEN_416.Checked = False
        chkASESMEN_417.Checked = False
        chkASESMEN_418.Checked = False
        chkASESMEN_419.Checked = False
        chkASESMEN_420.Checked = False
        chkASESMEN_421.Checked = False
        chkASESMEN_422.Checked = False
        chkASESMEN_423.Checked = False
        chkASESMEN_424.Checked = False
        chkASESMEN_425.Checked = False
        chkASESMEN_426.Checked = False
        chkASESMEN_427.Checked = False
        chkASESMEN_428.Checked = False
        chkASESMEN_429.Checked = False
        chkASESMEN_430.Checked = False
        chkASESMEN_431.Checked = False
        chkASESMEN_432.Checked = False
        chkASESMEN_433.Checked = False
        chkASESMEN_434.Checked = False
        chkASESMEN_435.Checked = False
        chkASESMEN_436.Checked = False
        chkASESMEN_437.Checked = False
        chkASESMEN_438.Checked = False
        txtASESMEN_439.ResetText()
        chkASESMEN_440.Checked = False
        txtJAM.Text = Now.ToString("HH:mm")
        deDATE.DateTime = Now
        txtPerawat.ResetText()

        'tambahan
        deTANGGALSHD1.ResetText()
        deTANGGALSHD2.ResetText()
        deTANGGALSHD3.ResetText()
        deTANGGALSHD4.ResetText()
        deTANGGALSHD5.ResetText()
        deTANGGALSHD6.ResetText()
        deTANGGALSHD7.ResetText()

        txtSKORSHD1.ResetText()
        txtSKORSHD2.ResetText()
        txtSKORSHD3.ResetText()
        txtSKORSHD4.ResetText()
        txtSKORSHD5.ResetText()
        txtSKORSHD6.ResetText()
        txtSKORSHD7.ResetText()

        txtSKORSHD8.ResetText()
        txtSKORSHD9.ResetText()
        txtSKORSHD10.ResetText()
        txtSKORSHD11.ResetText()
        txtSKORSHD12.ResetText()
        txtSKORSHD13.ResetText()
        txtSKORSHD14.ResetText()

        txtSKORSHD15.ResetText()
        txtSKORSHD16.ResetText()
        txtSKORSHD17.ResetText()
        txtSKORSHD18.ResetText()
        txtSKORSHD19.ResetText()
        txtSKORSHD20.ResetText()
        txtSKORSHD21.ResetText()

        txtSKORSHD22.ResetText()
        txtSKORSHD23.ResetText()
        txtSKORSHD24.ResetText()
        txtSKORSHD25.ResetText()
        txtSKORSHD26.ResetText()
        txtSKORSHD27.ResetText()
        txtSKORSHD28.ResetText()

        txtSKORSHD29.ResetText()
        txtSKORSHD30.ResetText()
        txtSKORSHD31.ResetText()
        txtSKORSHD32.ResetText()
        txtSKORSHD33.ResetText()
        txtSKORSHD34.ResetText()
        txtSKORSHD35.ResetText()

        txtSKORSHD36.ResetText()
        txtSKORSHD37.ResetText()
        txtSKORSHD38.ResetText()
        txtSKORSHD39.ResetText()
        txtSKORSHD40.ResetText()
        txtSKORSHD41.ResetText()
        txtSKORSHD42.ResetText()

        txtTOTALSKOR2.ResetText()
        txtTOTALSKOR3.ResetText()
        txtTOTALSKOR4.ResetText()
        txtTOTALSKOR5.ResetText()
        txtTOTALSKOR6.ResetText()
        txtTOTALSKOR7.ResetText()

        deTANGGALMORR_1.ResetText()
        deTANGGALMORR_2.ResetText()
        deTANGGALMORR_3.ResetText()
        deTANGGALMORR_4.ResetText()
        deTANGGALMORR_5.ResetText()
        deTANGGALMORR_6.ResetText()
        deTANGGALMORR_7.ResetText()

        chkMORR_1.Checked = False
        chkMORR_2.Checked = False
        chkMORR_3.Checked = False
        chkMORR_4.Checked = False
        chkMORR_5.Checked = False
        chkMORR_6.Checked = False
        chkMORR_7.Checked = False
        chkMORR_8.Checked = False
        chkMORR_9.Checked = False
        chkMORR_10.Checked = False
        chkMORR_11.Checked = False
        chkMORR_12.Checked = False
        chkMORR_13.Checked = False
        chkMORR_14.Checked = False
        chkMORR_15.Checked = False
        chkMORR_16.Checked = False
        chkMORR_17.Checked = False
        chkMORR_18.Checked = False
        chkMORR_19.Checked = False
        chkMORR_20.Checked = False
        chkMORR_21.Checked = False
        chkMORR_22.Checked = False
        chkMORR_23.Checked = False
        chkMORR_24.Checked = False
        chkMORR_25.Checked = False
        chkMORR_26.Checked = False
        chkMORR_27.Checked = False
        chkMORR_28.Checked = False
        chkMORR_29.Checked = False
        chkMORR_30.Checked = False
        chkMORR_31.Checked = False
        chkMORR_32.Checked = False
        chkMORR_33.Checked = False
        chkMORR_34.Checked = False
        chkMORR_35.Checked = False
        chkMORR_36.Checked = False
        chkMORR_37.Checked = False
        chkMORR_38.Checked = False
        chkMORR_39.Checked = False
        chkMORR_40.Checked = False
        chkMORR_41.Checked = False
        chkMORR_42.Checked = False
        chkMORR_43.Checked = False
        chkMORR_44.Checked = False
        chkMORR_45.Checked = False
        chkMORR_46.Checked = False
        chkMORR_47.Checked = False
        chkMORR_48.Checked = False
        chkMORR_49.Checked = False
        chkMORR_50.Checked = False
        chkMORR_51.Checked = False
        chkMORR_52.Checked = False
        chkMORR_53.Checked = False
        chkMORR_54.Checked = False

        txtPETUGASMORR_1.ResetText()
        txtPETUGASMORR_2.ResetText()
        txtPETUGASMORR_3.ResetText()
        txtPETUGASMORR_4.ResetText()
        txtPETUGASMORR_5.ResetText()
        txtPETUGASMORR_6.ResetText()
        txtPETUGASMORR_7.ResetText()

        deTANGGALMORT_1.ResetText()
        deTANGGALMORT_2.ResetText()
        deTANGGALMORT_3.ResetText()
        deTANGGALMORT_4.ResetText()
        deTANGGALMORT_5.ResetText()
        deTANGGALMORT_6.ResetText()
        deTANGGALMORT_7.ResetText()

        chkMORT_1.Checked = False
        chkMORT_2.Checked = False
        chkMORT_3.Checked = False
        chkMORT_4.Checked = False
        chkMORT_5.Checked = False
        chkMORT_6.Checked = False
        chkMORT_7.Checked = False
        chkMORT_8.Checked = False
        chkMORT_9.Checked = False
        chkMORT_10.Checked = False
        chkMORT_11.Checked = False
        chkMORT_12.Checked = False
        chkMORT_13.Checked = False
        chkMORT_14.Checked = False
        chkMORT_15.Checked = False
        chkMORT_16.Checked = False
        chkMORT_17.Checked = False
        chkMORT_18.Checked = False
        chkMORT_19.Checked = False
        chkMORT_20.Checked = False
        chkMORT_21.Checked = False
        chkMORT_22.Checked = False
        chkMORT_23.Checked = False
        chkMORT_24.Checked = False
        chkMORT_25.Checked = False
        chkMORT_26.Checked = False
        chkMORT_27.Checked = False
        chkMORT_28.Checked = False
        chkMORT_29.Checked = False
        chkMORT_30.Checked = False
        chkMORT_31.Checked = False
        chkMORT_32.Checked = False
        chkMORT_33.Checked = False
        chkMORT_34.Checked = False
        chkMORT_35.Checked = False
        chkMORT_36.Checked = False
        chkMORT_37.Checked = False
        chkMORT_38.Checked = False
        chkMORT_39.Checked = False
        chkMORT_40.Checked = False
        chkMORT_41.Checked = False
        chkMORT_42.Checked = False
        chkMORT_43.Checked = False
        chkMORT_44.Checked = False
        chkMORT_45.Checked = False
        chkMORT_46.Checked = False
        chkMORT_47.Checked = False
        chkMORT_48.Checked = False

        txtPETUGASMORT_1.ResetText()
        txtPETUGASMORT_2.ResetText()
        txtPETUGASMORT_3.ResetText()
        txtPETUGASMORT_4.ResetText()
        txtPETUGASMORT_5.ResetText()
        txtPETUGASMORT_6.ResetText()
        txtPETUGASMORT_7.ResetText()

        chkMASALAHKEP_1.Checked = False
        chkMASALAHKEP_2.Checked = False
        chkMASALAHKEP_3.Checked = False
        chkMASALAHKEP_4.Checked = False
        chkMASALAHKEP_5.Checked = False
        chkMASALAHKEP_6.Checked = False
        chkMASALAHKEP_7.Checked = False
        chkMASALAHKEP_8.Checked = False
        chkMASALAHKEP_9.Checked = False
        chkMASALAHKEP_10.Checked = False
        chkMASALAHKEP_11.Checked = False
        chkMASALAHKEP_12.Checked = False
        chkMASALAHKEP_13.Checked = False
        chkMASALAHKEP_14.Checked = False
        chkMASALAHKEP_15.Checked = False
        chkMASALAHKEP_16.Checked = False
        chkMASALAHKEP_17.Checked = False
        chkMASALAHKEP_18.Checked = False
        chkMASALAHKEP_19.Checked = False
        chkMASALAHKEP_20.Checked = False
        chkMASALAHKEP_21.Checked = False
        chkMASALAHKEP_22.Checked = False
        chkMASALAHKEP_23.Checked = False
        chkMASALAHKEP_24.Checked = False
        chkMASALAHKEP_25.Checked = False
        chkMASALAHKEP_26.Checked = False
        chkMASALAHKEP_27.Checked = False
        chkMASALAHKEP_28.Checked = False
        chkMASALAHKEP_29.Checked = False
        chkMASALAHKEP_30.Checked = False
        chkMASALAHKEP_31.Checked = False
        chkMASALAHKEP_32.Checked = False
        chkMASALAHKEP_33.Checked = False
        chkMASALAHKEP_34.Checked = False
        chkMASALAHKEP_35.Checked = False
        chkMASALAHKEP_36.Checked = False
        chkMASALAHKEP_37.Checked = False
        chkMASALAHKEP_38.Checked = False
        chkMASALAHKEP_39.Checked = False
        chkMASALAHKEP_40.Checked = False
        chkMASALAHKEP_41.Checked = False
        chkMASALAHKEP_42.Checked = False
        chkMASALAHKEP_43.Checked = False
        chkMASALAHKEP_44.Checked = False
        txtMASALAHKEP_44_TEXT.ResetText()


        TextEdit1.ResetText()
        TextEdit2.ResetText()
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

        CheckEdit1.Checked = False
        CheckEdit2.Checked = False
        CheckEdit3.Checked = False
        CheckEdit4.Checked = False
        CheckEdit5.Checked = False
        CheckEdit6.Checked = False
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
        txtSuku.ResetText()
    End Sub
    Private Sub fn_LoadData()
        Try
            ' ***** HEADER *****
            Dim ds = oS_DIGITAL_RI_40.GetData(txtNoRegister.Text)

            With ds
                chkASESMEN_01.Checked = .ASESMEN_01
                chkASESMEN_02.Checked = .ASESMEN_02
                txtASESMEN_03.Text = .ASESMEN_03
                txtASESMEN_04.Text = .ASESMEN_04
                txtASESMEN_05.Text = .ASESMEN_05
                txtASESMEN_06.Text = .ASESMEN_06
                chkASESMEN_07.Checked = .ASESMEN_07
                txtASESMEN_08.Text = .ASESMEN_08
                txtASESMEN_09.Text = .ASESMEN_09
                chkASESMEN_10.Checked = .ASESMEN_10
                chkASESMEN_11.Checked = .ASESMEN_11
                chkASESMEN_12.Checked = .ASESMEN_12
                txtASESMEN_13.Text = .ASESMEN_13
                txtASESMEN_14.Text = .ASESMEN_14
                txtASESMEN_15.Text = .ASESMEN_15
                txtASESMEN_16.Text = .ASESMEN_16
                txtASESMEN_17.Text = .ASESMEN_17
                txtASESMEN_18.Text = .ASESMEN_18
                txtASESMEN_19.Text = .ASESMEN_19
                txtASESMEN_20.Text = .ASESMEN_20
                chkASESMEN_21.Checked = .ASESMEN_21
                chkASESMEN_22.Checked = .ASESMEN_22
                chkASESMEN_23.Checked = .ASESMEN_23
                txtASESMEN_24.Text = .ASESMEN_24
                txtASESMEN_25.Text = .ASESMEN_25
                txtASESMEN_26.Text = .ASESMEN_26
                chkASESMEN_27.Checked = .ASESMEN_27
                chkASESMEN_28.Checked = .ASESMEN_28
                chkASESMEN_29.Checked = .ASESMEN_29
                txtASESMEN_30.Text = .ASESMEN_30
                chkASESMEN_31.Checked = .ASESMEN_31
                chkASESMEN_32.Checked = .ASESMEN_32
                chkASESMEN_33.Checked = .ASESMEN_33
                chkASESMEN_34.Checked = .ASESMEN_34
                txtASESMEN_35.Text = .ASESMEN_35
                chkASESMEN_36.Checked = .ASESMEN_36
                chkASESMEN_37.Checked = .ASESMEN_37
                txtASESMEN_38.Text = .ASESMEN_38
                txtASESMEN_39.Text = .ASESMEN_39
                txtASESMEN_40.Text = .ASESMEN_40
                txtASESMEN_41.Text = .ASESMEN_41
                chkASESMEN_42.Checked = .ASESMEN_42
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
                txtASESMEN_54.Text = .ASESMEN_54
                chkASESMEN_55.Checked = .ASESMEN_55
                txtASESMEN_56.Text = .ASESMEN_56
                chkASESMEN_57.Checked = .ASESMEN_57
                chkASESMEN_58.Checked = .ASESMEN_58
                txtASESMEN_59.Text = .ASESMEN_59
                chkASESMEN_60.Checked = .ASESMEN_60
                chkASESMEN_61.Checked = .ASESMEN_61
                chkASESMEN_62.Checked = .ASESMEN_62
                chkASESMEN_63.Checked = .ASESMEN_63
                chkASESMEN_64.Checked = .ASESMEN_64
                chkASESMEN_65.Checked = .ASESMEN_65
                chkASESMEN_66.Checked = .ASESMEN_66
                chkASESMEN_67.Checked = .ASESMEN_67
                txtASESMEN_68.Text = .ASESMEN_68
                txtASESMEN_69.Text = .ASESMEN_69
                txtASESMEN_70.Text = .ASESMEN_70
                chkASESMEN_71.Checked = .ASESMEN_71
                chkASESMEN_72.Checked = .ASESMEN_72
                chkASESMEN_73.Checked = .ASESMEN_73
                chkASESMEN_74.Checked = .ASESMEN_74
                chkASESMEN_75.Checked = .ASESMEN_75
                txtASESMEN_76.Text = .ASESMEN_76
                chkASESMEN_77.Checked = .ASESMEN_77
                chkASESMEN_78.Checked = .ASESMEN_78
                txtASESMEN_79.Text = .ASESMEN_79
                txtASESMEN_80.Text = .ASESMEN_80
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
                chkASESMEN_91.Checked = .ASESMEN_91
                chkASESMEN_92.Checked = .ASESMEN_92
                chkASESMEN_93.Checked = .ASESMEN_93
                chkASESMEN_94.Checked = .ASESMEN_94
                chkASESMEN_95.Checked = .ASESMEN_95
                chkASESMEN_96.Checked = .ASESMEN_96
                chkASESMEN_97.Checked = .ASESMEN_97
                txtASESMEN_98.Text = .ASESMEN_98
                chkASESMEN_99.Checked = .ASESMEN_99
                chkASESMEN_100.Checked = .ASESMEN_100
                txtASESMEN_101.Text = .ASESMEN_101
                txtASESMEN_102.Text = .ASESMEN_102
                txtASESMEN_103.Text = .ASESMEN_103
                chkASESMEN_104.Checked = .ASESMEN_104
                txtASESMEN_105.Text = .ASESMEN_105
                chkASESMEN_106.Checked = .ASESMEN_106
                'chkASESMEN_107.Checked = .ASESMEN_107
                'chkASESMEN_108.Checked = .ASESMEN_108
                'chkASESMEN_109.Checked = .ASESMEN_109
                'chkASESMEN_110.Checked = .ASESMEN_110
                'chkASESMEN_111.Checked = .ASESMEN_111
                'txtASESMEN_112.Text = .ASESMEN_112
                'chkASESMEN_113.Checked = .ASESMEN_113
                'chkASESMEN_114.Checked = .ASESMEN_114
                'chkASESMEN_115.Checked = .ASESMEN_115
                'chkASESMEN_116.Checked = .ASESMEN_116
                'chkASESMEN_117.Checked = .ASESMEN_117
                'chkASESMEN_118.Checked = .ASESMEN_118
                'chkASESMEN_119.Checked = .ASESMEN_119
                'txtASESMEN_120.Text = .ASESMEN_120
                'txtASESMEN_121.Text = .ASESMEN_121
                'txtASESMEN_122.Text = .ASESMEN_122
                'txtASESMEN_123.Text = .ASESMEN_123
                'chkASESMEN_124.Checked = .ASESMEN_124
                'chkASESMEN_125.Checked = .ASESMEN_125
                'chkASESMEN_126.Checked = .ASESMEN_126
                'chkASESMEN_127.Checked = .ASESMEN_127
                'txtASESMEN_128.Text = .ASESMEN_128
                'txtASESMEN_129.Text = .ASESMEN_129
                'chkASESMEN_130.Checked = .ASESMEN_130
                'txtASESMEN_131.Text = .ASESMEN_131
                'chkASESMEN_132.Checked = .ASESMEN_132
                'chkASESMEN_133.Checked = .ASESMEN_133
                'txtASESMEN_134.Text = .ASESMEN_134
                'chkASESMEN_135.Checked = .ASESMEN_135
                'txtASESMEN_136.Text = .ASESMEN_136
                'chkASESMEN_137.Checked = .ASESMEN_137
                'txtASESMEN_138.Text = .ASESMEN_138
                'txtASESMEN_139.Text = .ASESMEN_139
                'chkASESMEN_140.Checked = .ASESMEN_140
                'chkASESMEN_141.Checked = .ASESMEN_141
                'chkASESMEN_142.Checked = .ASESMEN_142
                'txtASESMEN_143.Text = .ASESMEN_143
                'chkASESMEN_144.Checked = .ASESMEN_144
                'txtASESMEN_145.Text = .ASESMEN_145
                'chkASESMEN_146.Checked = .ASESMEN_146
                'txtASESMEN_147.Text = .ASESMEN_147
                'txtASESMEN_148.Text = .ASESMEN_148
                'chkASESMEN_149.Checked = .ASESMEN_149
                'chkASESMEN_150.Checked = .ASESMEN_150
                'chkASESMEN_151.Checked = .ASESMEN_151
                'chkASESMEN_152.Checked = .ASESMEN_152
                'txtASESMEN_153.Text = .ASESMEN_153
                'chkASESMEN_154.Checked = .ASESMEN_154
                'chkASESMEN_155.Checked = .ASESMEN_155
                'chkASESMEN_156.Checked = .ASESMEN_156
                'chkASESMEN_157.Checked = .ASESMEN_157
                'chkASESMEN_158.Checked = .ASESMEN_158
                'chkASESMEN_159.Checked = .ASESMEN_159
                'chkASESMEN_160.Checked = .ASESMEN_160
                'chkASESMEN_161.Checked = .ASESMEN_161
                'chkASESMEN_162.Checked = .ASESMEN_162
                'chkASESMEN_163.Checked = .ASESMEN_163
                'chkASESMEN_164.Checked = .ASESMEN_164
                'chkASESMEN_165.Checked = .ASESMEN_165
                'chkASESMEN_166.Checked = .ASESMEN_166
                'chkASESMEN_167.Checked = .ASESMEN_167
                'txtASESMEN_168.Text = .ASESMEN_168
                'chkASESMEN_169.Checked = .ASESMEN_169
                'chkASESMEN_170.Checked = .ASESMEN_170
                'txtASESMEN_171.Text = .ASESMEN_171
                'chkASESMEN_172.Checked = .ASESMEN_172
                'chkASESMEN_173.Checked = .ASESMEN_173
                'chkASESMEN_174.Checked = .ASESMEN_174
                'chkASESMEN_175.Checked = .ASESMEN_175
                'chkASESMEN_176.Checked = .ASESMEN_176
                'chkASESMEN_177.Checked = .ASESMEN_177
                'chkASESMEN_178.Checked = .ASESMEN_178
                'txtASESMEN_179.Text = .ASESMEN_179
                'chkASESMEN_180.Checked = .ASESMEN_180
                'txtASESMEN_181.Text = .ASESMEN_181
                'chkASESMEN_182.Checked = .ASESMEN_182
                'txtASESMEN_183.Text = .ASESMEN_183
                'chkASESMEN_184.Checked = .ASESMEN_184
                'txtASESMEN_185.Text = .ASESMEN_185
                'chkASESMEN_186.Checked = .ASESMEN_186
                'chkASESMEN_187.Checked = .ASESMEN_187
                'txtASESMEN_188.Text = .ASESMEN_188
                'chkASESMEN_189.Checked = .ASESMEN_189
                'chkASESMEN_190.Checked = .ASESMEN_190
                'txtASESMEN_191.Text = .ASESMEN_191
                'txtASESMEN_192.Text = .ASESMEN_192
                'txtASESMEN_193.Text = .ASESMEN_193
                'txtASESMEN_194.Text = .ASESMEN_194
                'chkASESMEN_195.Checked = .ASESMEN_195
                'chkASESMEN_196.Checked = .ASESMEN_196
                txtASESMEN_197.Text = .ASESMEN_197
                txtASESMEN_198.Text = .ASESMEN_198
                txtASESMEN_199.Text = .ASESMEN_199
                txtASESMEN_200.Text = .ASESMEN_200
                txtASESMEN_201.Text = .ASESMEN_201
                txtASESMEN_202.Text = .ASESMEN_202
                txtASESMEN_203.Text = .ASESMEN_203
                txtASESMEN_204.Text = .ASESMEN_204
                chkASESMEN_205.Checked = .ASESMEN_205
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
                chkASESMEN_218.Checked = .ASESMEN_218
                chkASESMEN_219.Checked = .ASESMEN_219
                chkASESMEN_220.Checked = .ASESMEN_220
                chkASESMEN_221.Checked = .ASESMEN_221
                chkASESMEN_222.Checked = .ASESMEN_222
                chkASESMEN_223.Checked = .ASESMEN_223
                txtASESMEN_224.Text = .ASESMEN_224
                txtASESMEN_225.Text = .ASESMEN_225
                txtASESMEN_226.Text = .ASESMEN_226
                txtASESMEN_227.Text = .ASESMEN_227
                txtASESMEN_228.Text = .ASESMEN_228
                txtASESMEN_229.Text = .ASESMEN_229
                chkASESMEN_230.Checked = .ASESMEN_230
                chkASESMEN_231.Checked = .ASESMEN_231
                chkASESMEN_232.Checked = .ASESMEN_232
                txtASESMEN_233.Text = .ASESMEN_233
                txtASESMEN_234.Text = .ASESMEN_234
                txtASESMEN_235.Text = .ASESMEN_235
                txtASESMEN_236.Text = .ASESMEN_236
                txtASESMEN_237.Text = .ASESMEN_237
                txtASESMEN_238.Text = .ASESMEN_238
                txtASESMEN_239.Text = .ASESMEN_239
                txtASESMEN_240.Text = .ASESMEN_240
                txtASESMEN_241.Text = .ASESMEN_241
                txtASESMEN_242.Text = .ASESMEN_242
                txtASESMEN_243.Text = .ASESMEN_243
                txtASESMEN_244.Text = .ASESMEN_244
                txtASESMEN_245.Text = .ASESMEN_245
                txtASESMEN_246.Text = .ASESMEN_246
                txtASESMEN_247.Text = .ASESMEN_247
                txtASESMEN_248.Text = .ASESMEN_248
                txtASESMEN_249.Text = .ASESMEN_249
                txtASESMEN_250.Text = .ASESMEN_250
                txtASESMEN_251.Text = .ASESMEN_251
                txtASESMEN_252.Text = .ASESMEN_252
                txtASESMEN_253.Text = .ASESMEN_253
                txtASESMEN_254.Text = .ASESMEN_254
                txtASESMEN_255.Text = .ASESMEN_255
                txtASESMEN_256.Text = .ASESMEN_256
                txtASESMEN_257.Text = .ASESMEN_257
                txtASESMEN_258.Text = .ASESMEN_258
                txtASESMEN_259.Text = .ASESMEN_259
                txtASESMEN_260.Text = .ASESMEN_260
                txtASESMEN_261.Text = .ASESMEN_261
                txtASESMEN_262.Text = .ASESMEN_262
                txtASESMEN_263.Text = .ASESMEN_263
                txtASESMEN_264.Text = .ASESMEN_264
                txtASESMEN_265.Text = .ASESMEN_265
                txtASESMEN_266.Text = .ASESMEN_266
                txtASESMEN_267.Text = .ASESMEN_267
                txtASESMEN_268.Text = .ASESMEN_268
                txtASESMEN_269.Text = .ASESMEN_269
                txtASESMEN_270.Text = .ASESMEN_270
                txtASESMEN_271.Text = .ASESMEN_271
                txtASESMEN_272.Text = .ASESMEN_272
                txtASESMEN_273.Text = .ASESMEN_273
                txtASESMEN_274.Text = .ASESMEN_274
                txtASESMEN_275.Text = .ASESMEN_275
                txtASESMEN_276.Text = .ASESMEN_276
                txtASESMEN_277.Text = .ASESMEN_277
                txtASESMEN_278.Text = .ASESMEN_278
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
                txtASESMEN_293.Text = .ASESMEN_293
                txtASESMEN_294.Text = .ASESMEN_294
                txtASESMEN_295.Text = .ASESMEN_295
                txtASESMEN_296.Text = .ASESMEN_296
                txtASESMEN_297.Text = .ASESMEN_297
                txtASESMEN_298.Text = .ASESMEN_298
                txtASESMEN_299.Text = .ASESMEN_299
                txtASESMEN_300.Text = .ASESMEN_300
                txtASESMEN_301.Text = .ASESMEN_301
                txtASESMEN_302.Text = .ASESMEN_302
                txtASESMEN_303.Text = .ASESMEN_303
                txtASESMEN_304.Text = .ASESMEN_304
                txtASESMEN_305.Text = .ASESMEN_305
                txtASESMEN_306.Text = .ASESMEN_306
                txtASESMEN_307.Text = .ASESMEN_307
                txtASESMEN_308.Text = .ASESMEN_308
                txtASESMEN_309.Text = .ASESMEN_309
                txtASESMEN_310.Text = .ASESMEN_310
                txtASESMEN_311.Text = .ASESMEN_311
                txtASESMEN_312.Text = .ASESMEN_312
                txtASESMEN_313.Text = .ASESMEN_313
                txtASESMEN_314.Text = .ASESMEN_314
                txtASESMEN_315.Text = .ASESMEN_315
                txtASESMEN_316.Text = .ASESMEN_316
                txtASESMEN_317.Text = .ASESMEN_317
                txtASESMEN_318.Text = .ASESMEN_318
                txtASESMEN_319.Text = .ASESMEN_319
                txtASESMEN_320.Text = .ASESMEN_320
                txtASESMEN_321.Text = .ASESMEN_321
                txtASESMEN_322.Text = .ASESMEN_322
                txtASESMEN_323.Text = .ASESMEN_323
                txtASESMEN_324.Text = .ASESMEN_324
                txtASESMEN_325.Text = .ASESMEN_325
                txtASESMEN_326.Text = .ASESMEN_326
                txtASESMEN_327.Text = .ASESMEN_327
                txtASESMEN_328.Text = .ASESMEN_328
                txtASESMEN_329.Text = .ASESMEN_329
                txtASESMEN_330.Text = .ASESMEN_330
                txtASESMEN_331.Text = .ASESMEN_331
                txtASESMEN_332.Text = .ASESMEN_332
                txtASESMEN_333.Text = .ASESMEN_333
                txtASESMEN_334.Text = .ASESMEN_334
                txtASESMEN_335.Text = .ASESMEN_335
                txtASESMEN_336.Text = .ASESMEN_336
                txtASESMEN_337.Text = .ASESMEN_337
                txtASESMEN_338.Text = .ASESMEN_338
                txtASESMEN_339.Text = .ASESMEN_339
                txtASESMEN_340.Text = .ASESMEN_340
                txtASESMEN_341.Text = .ASESMEN_341
                txtASESMEN_342.Text = .ASESMEN_342
                txtASESMEN_343.Text = .ASESMEN_343
                txtASESMEN_344.Text = .ASESMEN_344
                txtASESMEN_345.Text = .ASESMEN_345
                txtASESMEN_346.Text = .ASESMEN_346
                txtASESMEN_347.Text = .ASESMEN_347
                txtASESMEN_348.Text = .ASESMEN_348
                txtASESMEN_349.Text = .ASESMEN_349
                txtASESMEN_350.Text = .ASESMEN_350
                txtASESMEN_351.Text = .ASESMEN_351
                txtASESMEN_352.Text = .ASESMEN_352
                txtASESMEN_353.Text = .ASESMEN_353
                txtASESMEN_354.Text = .ASESMEN_354
                txtASESMEN_355.Text = .ASESMEN_355
                txtASESMEN_356.Text = .ASESMEN_356
                txtASESMEN_357.Text = .ASESMEN_357
                txtASESMEN_358.Text = .ASESMEN_358
                txtASESMEN_359.Text = .ASESMEN_359
                txtASESMEN_360.Text = .ASESMEN_360
                txtASESMEN_361.Text = .ASESMEN_361
                txtASESMEN_362.Text = .ASESMEN_362
                txtASESMEN_363.Text = .ASESMEN_363
                txtASESMEN_364.Text = .ASESMEN_364
                txtASESMEN_365.Text = .ASESMEN_365
                txtASESMEN_366.Text = .ASESMEN_366
                txtASESMEN_367.Text = .ASESMEN_367
                txtASESMEN_368.Text = .ASESMEN_368
                txtASESMEN_369.Text = .ASESMEN_369
                txtASESMEN_370.Text = .ASESMEN_370
                txtASESMEN_371.Text = .ASESMEN_371
                txtASESMEN_372.Text = .ASESMEN_372
                chkASESMEN_373.Checked = .ASESMEN_373
                chkASESMEN_374.Checked = .ASESMEN_374
                chkASESMEN_375.Checked = .ASESMEN_375
                txtASESMEN_376.Text = .ASESMEN_376
                chkASESMEN_377.Checked = .ASESMEN_377
                chkASESMEN_378.Checked = .ASESMEN_378
                chkASESMEN_379.Checked = .ASESMEN_379
                chkASESMEN_380.Checked = .ASESMEN_380
                chkASESMEN_381.Checked = .ASESMEN_381
                chkASESMEN_382.Checked = .ASESMEN_382
                txtASESMEN_383.Text = .ASESMEN_383
                txtASESMEN_384.Text = .ASESMEN_384
                txtASESMEN_385.Text = .ASESMEN_385
                txtASESMEN_386.Text = .ASESMEN_386
                txtASESMEN_387.Text = .ASESMEN_387
                chkASESMEN_388.Checked = .ASESMEN_388
                chkASESMEN_389.Checked = .ASESMEN_389
                chkASESMEN_390.Checked = .ASESMEN_390
                chkASESMEN_391.Checked = .ASESMEN_391
                chkASESMEN_392.Checked = .ASESMEN_392
                chkASESMEN_393.Checked = .ASESMEN_393
                chkASESMEN_394.Checked = .ASESMEN_394
                chkASESMEN_395.Checked = .ASESMEN_395
                chkASESMEN_396.Checked = .ASESMEN_396
                chkASESMEN_397.Checked = .ASESMEN_397
                chkASESMEN_398.Checked = .ASESMEN_398
                txtASESMEN_399.Text = .ASESMEN_399
                chkASESMEN_400.Checked = .ASESMEN_400
                chkASESMEN_401.Checked = .ASESMEN_401
                chkASESMEN_402.Checked = .ASESMEN_402
                chkASESMEN_403.Checked = .ASESMEN_403
                txtASESMEN_404.Text = .ASESMEN_404
                chkASESMEN_405.Checked = .ASESMEN_405
                chkASESMEN_406.Checked = .ASESMEN_406
                chkASESMEN_407.Checked = .ASESMEN_407
                chkASESMEN_408.Checked = .ASESMEN_408
                chkASESMEN_409.Checked = .ASESMEN_409
                chkASESMEN_410.Checked = .ASESMEN_410
                chkASESMEN_411.Checked = .ASESMEN_411
                chkASESMEN_412.Checked = .ASESMEN_412
                chkASESMEN_413.Checked = .ASESMEN_413
                chkASESMEN_414.Checked = .ASESMEN_414
                chkASESMEN_415.Checked = .ASESMEN_415
                chkASESMEN_416.Checked = .ASESMEN_416
                chkASESMEN_417.Checked = .ASESMEN_417
                chkASESMEN_418.Checked = .ASESMEN_418
                chkASESMEN_419.Checked = .ASESMEN_419
                chkASESMEN_420.Checked = .ASESMEN_420
                chkASESMEN_421.Checked = .ASESMEN_421
                chkASESMEN_422.Checked = .ASESMEN_422
                chkASESMEN_423.Checked = .ASESMEN_423
                chkASESMEN_424.Checked = .ASESMEN_424
                chkASESMEN_425.Checked = .ASESMEN_425
                chkASESMEN_426.Checked = .ASESMEN_426
                chkASESMEN_427.Checked = .ASESMEN_427
                chkASESMEN_428.Checked = .ASESMEN_428
                chkASESMEN_429.Checked = .ASESMEN_429
                chkASESMEN_430.Checked = .ASESMEN_430
                chkASESMEN_431.Checked = .ASESMEN_431
                chkASESMEN_432.Checked = .ASESMEN_432
                chkASESMEN_433.Checked = .ASESMEN_433
                chkASESMEN_434.Checked = .ASESMEN_434
                chkASESMEN_435.Checked = .ASESMEN_435
                chkASESMEN_436.Checked = .ASESMEN_436
                chkASESMEN_437.Checked = .ASESMEN_437
                chkASESMEN_438.Checked = .ASESMEN_438
                txtASESMEN_439.Text = .ASESMEN_439
                chkASESMEN_440.Checked = .ASESMEN_440
                txtJAM.Text = .JAM
                deDATE.DateTime = .DATE
                txtPerawat.Text = .KDUSER
                grdPerawat.Text = .KDUSER

                'tambahan
                deTANGGALSHD1.Text = .TANGGALSHD1
                deTANGGALSHD2.Text = .TANGGALSHD2
                deTANGGALSHD3.Text = .TANGGALSHD3
                deTANGGALSHD4.Text = .TANGGALSHD4
                deTANGGALSHD5.Text = .TANGGALSHD5
                deTANGGALSHD6.Text = .TANGGALSHD6
                deTANGGALSHD7.Text = .TANGGALSHD7

                txtSKORSHD1.Text = .SKORSHD1
                txtSKORSHD2.Text = .SKORSHD2
                txtSKORSHD3.Text = .SKORSHD3
                txtSKORSHD4.Text = .SKORSHD4
                txtSKORSHD5.Text = .SKORSHD5
                txtSKORSHD6.Text = .SKORSHD6
                txtSKORSHD7.Text = .SKORSHD7

                txtSKORSHD8.Text = .SKORSHD8
                txtSKORSHD9.Text = .SKORSHD9
                txtSKORSHD10.Text = .SKORSHD10
                txtSKORSHD11.Text = .SKORSHD11
                txtSKORSHD12.Text = .SKORSHD12
                txtSKORSHD13.Text = .SKORSHD13
                txtSKORSHD14.Text = .SKORSHD14

                txtSKORSHD15.Text = .SKORSHD15
                txtSKORSHD16.Text = .SKORSHD16
                txtSKORSHD17.Text = .SKORSHD17
                txtSKORSHD18.Text = .SKORSHD18
                txtSKORSHD19.Text = .SKORSHD19
                txtSKORSHD20.Text = .SKORSHD20
                txtSKORSHD21.Text = .SKORSHD21

                txtSKORSHD22.Text = .SKORSHD22
                txtSKORSHD23.Text = .SKORSHD23
                txtSKORSHD24.Text = .SKORSHD24
                txtSKORSHD25.Text = .SKORSHD25
                txtSKORSHD26.Text = .SKORSHD26
                txtSKORSHD27.Text = .SKORSHD27
                txtSKORSHD28.Text = .SKORSHD28

                txtSKORSHD29.Text = .SKORSHD29
                txtSKORSHD30.Text = .SKORSHD30
                txtSKORSHD31.Text = .SKORSHD31
                txtSKORSHD32.Text = .SKORSHD32
                txtSKORSHD33.Text = .SKORSHD33
                txtSKORSHD34.Text = .SKORSHD34
                txtSKORSHD35.Text = .SKORSHD35

                txtSKORSHD36.Text = .SKORSHD36
                txtSKORSHD37.Text = .SKORSHD37
                txtSKORSHD38.Text = .SKORSHD38
                txtSKORSHD39.Text = .SKORSHD39
                txtSKORSHD40.Text = .SKORSHD40
                txtSKORSHD41.Text = .SKORSHD41
                txtSKORSHD42.Text = .SKORSHD42

                txtTOTALSKOR2.Text = .TOTALSKOR2
                txtTOTALSKOR3.Text = .TOTALSKOR3
                txtTOTALSKOR4.Text = .TOTALSKOR4
                txtTOTALSKOR5.Text = .TOTALSKOR5
                txtTOTALSKOR6.Text = .TOTALSKOR6
                txtTOTALSKOR7.Text = .TOTALSKOR7

                deTANGGALMORR_1.Text = .TANGGALMORR_1
                deTANGGALMORR_2.Text = .TANGGALMORR_2
                deTANGGALMORR_3.Text = .TANGGALMORR_3
                deTANGGALMORR_4.Text = .TANGGALMORR_4
                deTANGGALMORR_5.Text = .TANGGALMORR_5
                deTANGGALMORR_6.Text = .TANGGALMORR_6
                deTANGGALMORR_7.Text = .TANGGALMORR_7

                chkMORR_1.Checked = .MORR_1
                chkMORR_2.Checked = .MORR_2
                chkMORR_3.Checked = .MORR_3
                chkMORR_4.Checked = .MORR_4
                chkMORR_5.Checked = .MORR_5
                chkMORR_6.Checked = .MORR_6
                chkMORR_7.Checked = .MORR_7
                chkMORR_8.Checked = .MORR_8
                chkMORR_9.Checked = .MORR_9
                chkMORR_10.Checked = .MORR_10
                chkMORR_11.Checked = .MORR_11
                chkMORR_12.Checked = .MORR_12
                chkMORR_13.Checked = .MORR_13
                chkMORR_14.Checked = .MORR_14
                chkMORR_15.Checked = .MORR_15
                chkMORR_16.Checked = .MORR_16
                chkMORR_17.Checked = .MORR_17
                chkMORR_18.Checked = .MORR_18
                chkMORR_19.Checked = .MORR_19
                chkMORR_20.Checked = .MORR_20
                chkMORR_21.Checked = .MORR_21
                chkMORR_22.Checked = .MORR_22
                chkMORR_23.Checked = .MORR_23
                chkMORR_24.Checked = .MORR_24
                chkMORR_25.Checked = .MORR_25
                chkMORR_26.Checked = .MORR_26
                chkMORR_27.Checked = .MORR_27
                chkMORR_28.Checked = .MORR_28
                chkMORR_29.Checked = .MORR_29
                chkMORR_30.Checked = .MORR_30
                chkMORR_31.Checked = .MORR_31
                chkMORR_32.Checked = .MORR_32
                chkMORR_33.Checked = .MORR_33
                chkMORR_34.Checked = .MORR_34
                chkMORR_35.Checked = .MORR_35
                chkMORR_36.Checked = .MORR_36
                chkMORR_37.Checked = .MORR_37
                chkMORR_38.Checked = .MORR_38
                chkMORR_39.Checked = .MORR_39
                chkMORR_40.Checked = .MORR_40
                chkMORR_41.Checked = .MORR_41
                chkMORR_42.Checked = .MORR_42
                chkMORR_43.Checked = .MORR_43
                chkMORR_44.Checked = .MORR_44
                chkMORR_45.Checked = .MORR_45
                chkMORR_46.Checked = .MORR_46
                chkMORR_47.Checked = .MORR_47
                chkMORR_48.Checked = .MORR_48
                chkMORR_49.Checked = .MORR_49
                chkMORR_50.Checked = .MORR_50
                chkMORR_51.Checked = .MORR_51
                chkMORR_52.Checked = .MORR_52
                chkMORR_53.Checked = .MORR_53
                chkMORR_54.Checked = .MORR_54

                txtPETUGASMORR_1.Text = .PETUGASMORR_1
                txtPETUGASMORR_2.Text = .PETUGASMORR_2
                txtPETUGASMORR_3.Text = .PETUGASMORR_3
                txtPETUGASMORR_4.Text = .PETUGASMORR_4
                txtPETUGASMORR_5.Text = .PETUGASMORR_5
                txtPETUGASMORR_6.Text = .PETUGASMORR_6
                txtPETUGASMORR_7.Text = .PETUGASMORR_7

                deTANGGALMORT_1.Text = .TANGGALMORT_1
                deTANGGALMORT_2.Text = .TANGGALMORT_2
                deTANGGALMORT_3.Text = .TANGGALMORT_3
                deTANGGALMORT_4.Text = .TANGGALMORT_4
                deTANGGALMORT_5.Text = .TANGGALMORT_5
                deTANGGALMORT_6.Text = .TANGGALMORT_6
                deTANGGALMORT_7.Text = .TANGGALMORT_7

                chkMORT_1.Checked = .MORT_1
                chkMORT_2.Checked = .MORT_2
                chkMORT_3.Checked = .MORT_3
                chkMORT_4.Checked = .MORT_4
                chkMORT_5.Checked = .MORT_5
                chkMORT_6.Checked = .MORT_6
                chkMORT_7.Checked = .MORT_7
                chkMORT_8.Checked = .MORT_8
                chkMORT_9.Checked = .MORT_9
                chkMORT_10.Checked = .MORT_10
                chkMORT_11.Checked = .MORT_11
                chkMORT_12.Checked = .MORT_12
                chkMORT_13.Checked = .MORT_13
                chkMORT_14.Checked = .MORT_14
                chkMORT_15.Checked = .MORT_15
                chkMORT_16.Checked = .MORT_16
                chkMORT_17.Checked = .MORT_17
                chkMORT_18.Checked = .MORT_18
                chkMORT_19.Checked = .MORT_19
                chkMORT_20.Checked = .MORT_20
                chkMORT_21.Checked = .MORT_21
                chkMORT_22.Checked = .MORT_22
                chkMORT_23.Checked = .MORT_23
                chkMORT_24.Checked = .MORT_24
                chkMORT_25.Checked = .MORT_25
                chkMORT_26.Checked = .MORT_26
                chkMORT_27.Checked = .MORT_27
                chkMORT_28.Checked = .MORT_28
                chkMORT_29.Checked = .MORT_29
                chkMORT_30.Checked = .MORT_30
                chkMORT_31.Checked = .MORT_31
                chkMORT_32.Checked = .MORT_32
                chkMORT_33.Checked = .MORT_33
                chkMORT_34.Checked = .MORT_34
                chkMORT_35.Checked = .MORT_35
                chkMORT_36.Checked = .MORT_36
                chkMORT_37.Checked = .MORT_37
                chkMORT_38.Checked = .MORT_38
                chkMORT_39.Checked = .MORT_39
                chkMORT_40.Checked = .MORT_40
                chkMORT_41.Checked = .MORT_41
                chkMORT_42.Checked = .MORT_42
                chkMORT_43.Checked = .MORT_43
                chkMORT_44.Checked = .MORT_44
                chkMORT_45.Checked = .MORT_45
                chkMORT_46.Checked = .MORT_46
                chkMORT_47.Checked = .MORT_47
                chkMORT_48.Checked = .MORT_48

                txtPETUGASMORT_1.Text = .PETUGASMORT_1
                txtPETUGASMORT_2.Text = .PETUGASMORT_2
                txtPETUGASMORT_3.Text = .PETUGASMORT_3
                txtPETUGASMORT_4.Text = .PETUGASMORT_4
                txtPETUGASMORT_5.Text = .PETUGASMORT_5
                txtPETUGASMORT_6.Text = .PETUGASMORT_6
                txtPETUGASMORT_7.Text = .PETUGASMORT_7

                chkMASALAHKEP_1.Checked = .MASALAHKEP_1
                chkMASALAHKEP_2.Checked = .MASALAHKEP_2
                chkMASALAHKEP_3.Checked = .MASALAHKEP_3
                chkMASALAHKEP_4.Checked = .MASALAHKEP_4
                chkMASALAHKEP_5.Checked = .MASALAHKEP_5
                chkMASALAHKEP_6.Checked = .MASALAHKEP_6
                chkMASALAHKEP_7.Checked = .MASALAHKEP_7
                chkMASALAHKEP_8.Checked = .MASALAHKEP_8
                chkMASALAHKEP_9.Checked = .MASALAHKEP_9
                chkMASALAHKEP_10.Checked = .MASALAHKEP_10
                chkMASALAHKEP_11.Checked = .MASALAHKEP_11
                chkMASALAHKEP_12.Checked = .MASALAHKEP_12
                chkMASALAHKEP_13.Checked = .MASALAHKEP_13
                chkMASALAHKEP_14.Checked = .MASALAHKEP_14
                chkMASALAHKEP_15.Checked = .MASALAHKEP_15
                chkMASALAHKEP_16.Checked = .MASALAHKEP_16
                chkMASALAHKEP_17.Checked = .MASALAHKEP_17
                chkMASALAHKEP_18.Checked = .MASALAHKEP_18
                chkMASALAHKEP_19.Checked = .MASALAHKEP_19
                chkMASALAHKEP_20.Checked = .MASALAHKEP_20
                chkMASALAHKEP_21.Checked = .MASALAHKEP_21
                chkMASALAHKEP_22.Checked = .MASALAHKEP_22
                chkMASALAHKEP_23.Checked = .MASALAHKEP_23
                chkMASALAHKEP_24.Checked = .MASALAHKEP_24
                chkMASALAHKEP_25.Checked = .MASALAHKEP_25
                chkMASALAHKEP_26.Checked = .MASALAHKEP_26
                chkMASALAHKEP_27.Checked = .MASALAHKEP_27
                chkMASALAHKEP_28.Checked = .MASALAHKEP_28
                chkMASALAHKEP_29.Checked = .MASALAHKEP_29
                chkMASALAHKEP_30.Checked = .MASALAHKEP_30
                chkMASALAHKEP_31.Checked = .MASALAHKEP_31
                chkMASALAHKEP_32.Checked = .MASALAHKEP_32
                chkMASALAHKEP_33.Checked = .MASALAHKEP_33
                chkMASALAHKEP_34.Checked = .MASALAHKEP_34
                chkMASALAHKEP_35.Checked = .MASALAHKEP_35
                chkMASALAHKEP_36.Checked = .MASALAHKEP_36
                chkMASALAHKEP_37.Checked = .MASALAHKEP_37
                chkMASALAHKEP_38.Checked = .MASALAHKEP_38
                chkMASALAHKEP_39.Checked = .MASALAHKEP_39
                chkMASALAHKEP_40.Checked = .MASALAHKEP_40
                chkMASALAHKEP_41.Checked = .MASALAHKEP_41
                chkMASALAHKEP_42.Checked = .MASALAHKEP_42
                chkMASALAHKEP_43.Checked = .MASALAHKEP_43
                chkMASALAHKEP_44.Checked = .MASALAHKEP_44
                txtMASALAHKEP_44_TEXT.Text = .MASALAHKEP_44_TEXT

                TextEdit1.Text = .NEW_01
                TextEdit2.Text = .NEW_02
                TextEdit3.Text = .NEW_03
                TextEdit4.Text = .NEW_04
                TextEdit5.Text = .NEW_05
                TextEdit6.Text = .NEW_06
                TextEdit7.Text = .NEW_07
                TextEdit8.Text = .NEW_08
                TextEdit9.Text = .NEW_09
                TextEdit10.Text = .NEW_10
                TextEdit11.Text = .NEW_11
                TextEdit12.Text = .NEW_12

                CheckEdit1.Checked = .NEW_01_BIT
                CheckEdit2.Checked = .NEW_02_BIT
                CheckEdit3.Checked = .NEW_03_BIT
                CheckEdit4.Checked = .NEW_04_BIT
                CheckEdit5.Checked = .NEW_05_BIT
                CheckEdit6.Checked = .NEW_06_BIT
                CheckEdit7.Checked = .NEW_07_BIT
                CheckEdit8.Checked = .NEW_08_BIT
                CheckEdit9.Checked = .NEW_09_BIT
                CheckEdit10.Checked = .NEW_10_BIT
                CheckEdit11.Checked = .NEW_11_BIT
                CheckEdit12.Checked = .NEW_12_BIT
                CheckEdit13.Checked = .NEW_13_BIT
                CheckEdit14.Checked = .NEW_14_BIT
                CheckEdit15.Checked = .NEW_15_BIT
                CheckEdit16.Checked = .NEW_16_BIT
                CheckEdit17.Checked = .NEW_17_BIT
                CheckEdit18.Checked = .NEW_18_BIT
                CheckEdit19.Checked = .NEW_19_BIT
                CheckEdit20.Checked = .NEW_20_BIT
                txtSuku.Text = .ASESMEN_112
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
            Dim ds = oS_DIGITAL_RI_40.GetStructureHeader
            With ds
                .KDPENDAFTARAN = txtNoRegister.Text
                .KDCUSTOMER = txtNoPasien.Text

                Try
                    .DATECREATED = oS_DIGITAL_RI_40.GetData(txtNoRegister.Text).DATECREATED
                Catch ex As Exception
                    .DATECREATED = Now
                End Try
                .DATEUPDATED = Now
                .ASESMEN_01 = chkASESMEN_01.Checked
                .ASESMEN_02 = chkASESMEN_02.Checked
                .ASESMEN_03 = txtASESMEN_03.Text
                .ASESMEN_04 = txtASESMEN_04.Text
                .ASESMEN_05 = txtASESMEN_05.Text
                .ASESMEN_06 = txtASESMEN_06.Text
                .ASESMEN_07 = chkASESMEN_07.Checked
                .ASESMEN_08 = txtASESMEN_08.Text
                .ASESMEN_09 = txtASESMEN_09.Text
                .ASESMEN_10 = chkASESMEN_10.Checked
                .ASESMEN_11 = chkASESMEN_11.Checked
                .ASESMEN_12 = chkASESMEN_12.Checked
                .ASESMEN_13 = txtASESMEN_13.Text
                .ASESMEN_14 = txtASESMEN_14.Text
                .ASESMEN_15 = txtASESMEN_15.Text
                .ASESMEN_16 = txtASESMEN_16.Text
                .ASESMEN_17 = txtASESMEN_17.Text
                .ASESMEN_18 = txtASESMEN_18.Text
                .ASESMEN_19 = txtASESMEN_19.Text
                .ASESMEN_20 = txtASESMEN_20.Text
                .ASESMEN_21 = chkASESMEN_21.Checked
                .ASESMEN_22 = chkASESMEN_22.Checked
                .ASESMEN_23 = chkASESMEN_23.Checked
                .ASESMEN_24 = txtASESMEN_24.Text
                .ASESMEN_25 = txtASESMEN_25.Text
                .ASESMEN_26 = txtASESMEN_26.Text
                .ASESMEN_27 = chkASESMEN_27.Checked
                .ASESMEN_28 = chkASESMEN_28.Checked
                .ASESMEN_29 = chkASESMEN_29.Checked
                .ASESMEN_30 = txtASESMEN_30.Text
                .ASESMEN_31 = chkASESMEN_31.Checked
                .ASESMEN_32 = chkASESMEN_32.Checked
                .ASESMEN_33 = chkASESMEN_33.Checked
                .ASESMEN_34 = chkASESMEN_34.Checked
                .ASESMEN_35 = txtASESMEN_35.Text
                .ASESMEN_36 = chkASESMEN_36.Checked
                .ASESMEN_37 = chkASESMEN_37.Checked
                .ASESMEN_38 = txtASESMEN_38.Text
                .ASESMEN_39 = txtASESMEN_39.Text
                .ASESMEN_40 = txtASESMEN_40.Text
                .ASESMEN_41 = txtASESMEN_41.Text
                .ASESMEN_42 = chkASESMEN_42.Checked
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
                .ASESMEN_54 = txtASESMEN_54.Text
                .ASESMEN_55 = chkASESMEN_55.Checked
                .ASESMEN_56 = txtASESMEN_56.Text
                .ASESMEN_57 = chkASESMEN_57.Checked
                .ASESMEN_58 = chkASESMEN_58.Checked
                .ASESMEN_59 = txtASESMEN_59.Text
                .ASESMEN_60 = chkASESMEN_60.Checked
                .ASESMEN_61 = chkASESMEN_61.Checked
                .ASESMEN_62 = chkASESMEN_62.Checked
                .ASESMEN_63 = chkASESMEN_63.Checked
                .ASESMEN_64 = chkASESMEN_64.Checked
                .ASESMEN_65 = chkASESMEN_65.Checked
                .ASESMEN_66 = chkASESMEN_66.Checked
                .ASESMEN_67 = chkASESMEN_67.Checked
                .ASESMEN_68 = txtASESMEN_68.Text
                .ASESMEN_69 = txtASESMEN_69.Text
                .ASESMEN_70 = txtASESMEN_70.Text
                .ASESMEN_71 = chkASESMEN_71.Checked
                .ASESMEN_72 = chkASESMEN_72.Checked
                .ASESMEN_73 = chkASESMEN_73.Checked
                .ASESMEN_74 = chkASESMEN_74.Checked
                .ASESMEN_75 = chkASESMEN_75.Checked
                .ASESMEN_76 = txtASESMEN_76.Text
                .ASESMEN_77 = chkASESMEN_77.Checked
                .ASESMEN_78 = chkASESMEN_78.Checked
                .ASESMEN_79 = txtASESMEN_79.Text
                .ASESMEN_80 = txtASESMEN_80.Text
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
                .ASESMEN_91 = chkASESMEN_91.Checked
                .ASESMEN_92 = chkASESMEN_92.Checked
                .ASESMEN_93 = chkASESMEN_93.Checked
                .ASESMEN_94 = chkASESMEN_94.Checked
                .ASESMEN_95 = chkASESMEN_95.Checked
                .ASESMEN_96 = chkASESMEN_96.Checked
                .ASESMEN_97 = chkASESMEN_97.Checked
                .ASESMEN_98 = txtASESMEN_98.Text
                .ASESMEN_99 = chkASESMEN_99.Checked
                .ASESMEN_100 = chkASESMEN_100.Checked
                .ASESMEN_101 = txtASESMEN_101.Text
                .ASESMEN_102 = txtASESMEN_102.Text
                .ASESMEN_103 = txtASESMEN_103.Text
                .ASESMEN_104 = chkASESMEN_104.Checked
                .ASESMEN_105 = txtASESMEN_105.Text
                .ASESMEN_106 = chkASESMEN_106.Checked
                '.ASESMEN_107 = chkASESMEN_107.Checked
                '.ASESMEN_108 = chkASESMEN_108.Checked
                '.ASESMEN_109 = chkASESMEN_109.Checked
                '.ASESMEN_110 = chkASESMEN_110.Checked
                '.ASESMEN_111 = chkASESMEN_111.Checked
                '.ASESMEN_112 = txtASESMEN_112.Text
                '.ASESMEN_113 = chkASESMEN_113.Checked
                '.ASESMEN_114 = chkASESMEN_114.Checked
                '.ASESMEN_115 = chkASESMEN_115.Checked
                '.ASESMEN_116 = chkASESMEN_116.Checked
                '.ASESMEN_117 = chkASESMEN_117.Checked
                '.ASESMEN_118 = chkASESMEN_118.Checked
                '.ASESMEN_119 = chkASESMEN_119.Checked
                '.ASESMEN_120 = txtASESMEN_120.Text
                '.ASESMEN_121 = txtASESMEN_121.Text
                '.ASESMEN_122 = txtASESMEN_122.Text
                '.ASESMEN_123 = txtASESMEN_123.Text
                '.ASESMEN_124 = chkASESMEN_124.Checked
                '.ASESMEN_125 = chkASESMEN_125.Checked
                '.ASESMEN_126 = chkASESMEN_126.Checked
                '.ASESMEN_127 = chkASESMEN_127.Checked
                '.ASESMEN_128 = txtASESMEN_128.Text
                '.ASESMEN_129 = txtASESMEN_129.Text
                '.ASESMEN_130 = chkASESMEN_130.Checked
                '.ASESMEN_131 = txtASESMEN_131.Text
                '.ASESMEN_132 = chkASESMEN_132.Checked
                '.ASESMEN_133 = chkASESMEN_133.Checked
                '.ASESMEN_134 = txtASESMEN_134.Text
                '.ASESMEN_135 = chkASESMEN_135.Checked
                '.ASESMEN_136 = txtASESMEN_136.Text
                '.ASESMEN_137 = chkASESMEN_137.Checked
                '.ASESMEN_138 = txtASESMEN_138.Text
                '.ASESMEN_139 = txtASESMEN_139.Text
                '.ASESMEN_140 = chkASESMEN_140.Checked
                '.ASESMEN_141 = chkASESMEN_141.Checked
                '.ASESMEN_142 = chkASESMEN_142.Checked
                '.ASESMEN_143 = txtASESMEN_143.Text
                '.ASESMEN_144 = chkASESMEN_144.Checked
                '.ASESMEN_145 = txtASESMEN_145.Text
                '.ASESMEN_146 = chkASESMEN_146.Checked
                '.ASESMEN_147 = txtASESMEN_147.Text
                '.ASESMEN_148 = txtASESMEN_148.Text
                '.ASESMEN_149 = chkASESMEN_149.Checked
                '.ASESMEN_150 = chkASESMEN_150.Checked
                '.ASESMEN_151 = chkASESMEN_151.Checked
                '.ASESMEN_152 = chkASESMEN_152.Checked
                '.ASESMEN_153 = txtASESMEN_153.Text
                '.ASESMEN_154 = chkASESMEN_154.Checked
                '.ASESMEN_155 = chkASESMEN_155.Checked
                '.ASESMEN_156 = chkASESMEN_156.Checked
                '.ASESMEN_157 = chkASESMEN_157.Checked
                '.ASESMEN_158 = chkASESMEN_158.Checked
                '.ASESMEN_159 = chkASESMEN_159.Checked
                '.ASESMEN_160 = chkASESMEN_160.Checked
                '.ASESMEN_161 = chkASESMEN_161.Checked
                '.ASESMEN_162 = chkASESMEN_162.Checked
                '.ASESMEN_163 = chkASESMEN_163.Checked
                '.ASESMEN_164 = chkASESMEN_164.Checked
                '.ASESMEN_165 = chkASESMEN_165.Checked
                '.ASESMEN_166 = chkASESMEN_166.Checked
                '.ASESMEN_167 = chkASESMEN_167.Checked
                '.ASESMEN_168 = txtASESMEN_168.Text
                '.ASESMEN_169 = chkASESMEN_169.Checked
                '.ASESMEN_170 = chkASESMEN_170.Checked
                '.ASESMEN_171 = txtASESMEN_171.Text
                '.ASESMEN_172 = chkASESMEN_172.Checked
                '.ASESMEN_173 = chkASESMEN_173.Checked
                '.ASESMEN_174 = chkASESMEN_174.Checked
                '.ASESMEN_175 = chkASESMEN_175.Checked
                '.ASESMEN_176 = chkASESMEN_176.Checked
                '.ASESMEN_177 = chkASESMEN_177.Checked
                '.ASESMEN_178 = chkASESMEN_178.Checked
                '.ASESMEN_179 = txtASESMEN_179.Text
                '.ASESMEN_180 = chkASESMEN_180.Checked
                '.ASESMEN_181 = txtASESMEN_181.Text
                '.ASESMEN_182 = chkASESMEN_182.Checked
                '.ASESMEN_183 = txtASESMEN_183.Text
                '.ASESMEN_184 = chkASESMEN_184.Checked
                '.ASESMEN_185 = txtASESMEN_185.Text
                '.ASESMEN_186 = chkASESMEN_186.Checked
                '.ASESMEN_187 = chkASESMEN_187.Checked
                '.ASESMEN_188 = txtASESMEN_188.Text
                '.ASESMEN_189 = chkASESMEN_189.Checked
                '.ASESMEN_190 = chkASESMEN_190.Checked
                '.ASESMEN_191 = txtASESMEN_191.Text
                '.ASESMEN_192 = txtASESMEN_192.Text
                '.ASESMEN_193 = txtASESMEN_193.Text
                '.ASESMEN_194 = txtASESMEN_194.Text
                '.ASESMEN_195 = chkASESMEN_195.Checked
                '.ASESMEN_196 = chkASESMEN_196.Checked

                .ASESMEN_107 = False
                .ASESMEN_108 = False
                .ASESMEN_109 = False
                .ASESMEN_110 = False
                .ASESMEN_111 = False
                .ASESMEN_112 = txtSuku.Text
                .ASESMEN_113 = False
                .ASESMEN_114 = False
                .ASESMEN_115 = False
                .ASESMEN_116 = False
                .ASESMEN_117 = False
                .ASESMEN_118 = False
                .ASESMEN_119 = False
                .ASESMEN_120 = ""
                .ASESMEN_121 = ""
                .ASESMEN_122 = ""
                .ASESMEN_123 = ""
                .ASESMEN_124 = False
                .ASESMEN_125 = False
                .ASESMEN_126 = False
                .ASESMEN_127 = False
                .ASESMEN_128 = ""
                .ASESMEN_129 = ""
                .ASESMEN_130 = False
                .ASESMEN_131 = ""
                .ASESMEN_132 = False
                .ASESMEN_133 = False
                .ASESMEN_134 = ""
                .ASESMEN_135 = False
                .ASESMEN_136 = ""
                .ASESMEN_137 = False
                .ASESMEN_138 = ""
                .ASESMEN_139 = ""
                .ASESMEN_140 = False
                .ASESMEN_141 = False
                .ASESMEN_142 = False
                .ASESMEN_143 = ""
                .ASESMEN_144 = False
                .ASESMEN_145 = ""
                .ASESMEN_146 = False
                .ASESMEN_147 = ""
                .ASESMEN_148 = ""
                .ASESMEN_149 = False
                .ASESMEN_150 = False
                .ASESMEN_151 = False
                .ASESMEN_152 = False
                .ASESMEN_153 = ""
                .ASESMEN_154 = False
                .ASESMEN_155 = False
                .ASESMEN_156 = False
                .ASESMEN_157 = False
                .ASESMEN_158 = False
                .ASESMEN_159 = False
                .ASESMEN_160 = False
                .ASESMEN_161 = False
                .ASESMEN_162 = False
                .ASESMEN_163 = False
                .ASESMEN_164 = False
                .ASESMEN_165 = False
                .ASESMEN_166 = False
                .ASESMEN_167 = False
                .ASESMEN_168 = ""
                .ASESMEN_169 = False
                .ASESMEN_170 = False
                .ASESMEN_171 = ""
                .ASESMEN_172 = False
                .ASESMEN_173 = False
                .ASESMEN_174 = False
                .ASESMEN_175 = False
                .ASESMEN_176 = False
                .ASESMEN_177 = False
                .ASESMEN_178 = False
                .ASESMEN_179 = ""
                .ASESMEN_180 = False
                .ASESMEN_181 = ""
                .ASESMEN_182 = False
                .ASESMEN_183 = ""
                .ASESMEN_184 = False
                .ASESMEN_185 = ""
                .ASESMEN_186 = False
                .ASESMEN_187 = False
                .ASESMEN_188 = ""
                .ASESMEN_189 = False
                .ASESMEN_190 = False
                .ASESMEN_191 = ""
                .ASESMEN_192 = ""
                .ASESMEN_193 = ""
                .ASESMEN_194 = ""
                .ASESMEN_195 = False
                .ASESMEN_196 = False

                .ASESMEN_197 = txtASESMEN_197.Text
                .ASESMEN_198 = txtASESMEN_198.Text
                .ASESMEN_199 = txtASESMEN_199.Text
                .ASESMEN_200 = txtASESMEN_200.Text
                .ASESMEN_201 = txtASESMEN_201.Text
                .ASESMEN_202 = txtASESMEN_202.Text
                .ASESMEN_203 = txtASESMEN_203.Text
                .ASESMEN_204 = txtASESMEN_204.Text
                .ASESMEN_205 = chkASESMEN_205.Checked
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
                .ASESMEN_218 = chkASESMEN_218.Checked
                .ASESMEN_219 = chkASESMEN_219.Checked
                .ASESMEN_220 = chkASESMEN_220.Checked
                .ASESMEN_221 = chkASESMEN_221.Checked
                .ASESMEN_222 = chkASESMEN_222.Checked
                .ASESMEN_223 = chkASESMEN_223.Checked
                .ASESMEN_224 = txtASESMEN_224.Text
                .ASESMEN_225 = txtASESMEN_225.Text
                .ASESMEN_226 = txtASESMEN_226.Text
                .ASESMEN_227 = txtASESMEN_227.Text
                .ASESMEN_228 = txtASESMEN_228.Text
                .ASESMEN_229 = txtASESMEN_229.Text
                .ASESMEN_230 = chkASESMEN_230.Checked
                .ASESMEN_231 = chkASESMEN_231.Checked
                .ASESMEN_232 = chkASESMEN_232.Checked
                .ASESMEN_233 = txtASESMEN_233.Text
                .ASESMEN_234 = txtASESMEN_234.Text
                .ASESMEN_235 = txtASESMEN_235.Text
                .ASESMEN_236 = txtASESMEN_236.Text
                .ASESMEN_237 = txtASESMEN_237.Text
                .ASESMEN_238 = txtASESMEN_238.Text
                .ASESMEN_239 = txtASESMEN_239.Text
                .ASESMEN_240 = txtASESMEN_240.Text
                .ASESMEN_241 = txtASESMEN_241.Text
                .ASESMEN_242 = txtASESMEN_242.Text
                .ASESMEN_243 = txtASESMEN_243.Text
                .ASESMEN_244 = txtASESMEN_244.Text
                .ASESMEN_245 = txtASESMEN_245.Text
                .ASESMEN_246 = txtASESMEN_246.Text
                .ASESMEN_247 = txtASESMEN_247.Text
                .ASESMEN_248 = txtASESMEN_248.Text
                .ASESMEN_249 = txtASESMEN_249.Text
                .ASESMEN_250 = txtASESMEN_250.Text
                .ASESMEN_251 = txtASESMEN_251.Text
                .ASESMEN_252 = txtASESMEN_252.Text
                .ASESMEN_253 = txtASESMEN_253.Text
                .ASESMEN_254 = txtASESMEN_254.Text
                .ASESMEN_255 = txtASESMEN_255.Text
                .ASESMEN_256 = txtASESMEN_256.Text
                .ASESMEN_257 = txtASESMEN_257.Text
                .ASESMEN_258 = txtASESMEN_258.Text
                .ASESMEN_259 = txtASESMEN_259.Text
                .ASESMEN_260 = txtASESMEN_260.Text
                .ASESMEN_261 = txtASESMEN_261.Text
                .ASESMEN_262 = txtASESMEN_262.Text
                .ASESMEN_263 = txtASESMEN_263.Text
                .ASESMEN_264 = txtASESMEN_264.Text
                .ASESMEN_265 = txtASESMEN_265.Text
                .ASESMEN_266 = txtASESMEN_266.Text
                .ASESMEN_267 = txtASESMEN_267.Text
                .ASESMEN_268 = txtASESMEN_268.Text
                .ASESMEN_269 = txtASESMEN_269.Text
                .ASESMEN_270 = txtASESMEN_270.Text
                .ASESMEN_271 = txtASESMEN_271.Text
                .ASESMEN_272 = txtASESMEN_272.Text
                .ASESMEN_273 = txtASESMEN_273.Text
                .ASESMEN_274 = txtASESMEN_274.Text
                .ASESMEN_275 = txtASESMEN_275.Text
                .ASESMEN_276 = txtASESMEN_276.Text
                .ASESMEN_277 = txtASESMEN_277.Text
                .ASESMEN_278 = txtASESMEN_278.Text
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
                .ASESMEN_293 = txtASESMEN_293.Text
                .ASESMEN_294 = txtASESMEN_294.Text
                .ASESMEN_295 = txtASESMEN_295.Text
                .ASESMEN_296 = txtASESMEN_296.Text
                .ASESMEN_297 = txtASESMEN_297.Text
                .ASESMEN_298 = txtASESMEN_298.Text
                .ASESMEN_299 = txtASESMEN_299.Text
                .ASESMEN_300 = txtASESMEN_300.Text
                .ASESMEN_301 = txtASESMEN_301.Text
                .ASESMEN_302 = txtASESMEN_302.Text
                .ASESMEN_303 = txtASESMEN_303.Text
                .ASESMEN_304 = txtASESMEN_304.Text
                .ASESMEN_305 = txtASESMEN_305.Text
                .ASESMEN_306 = txtASESMEN_306.Text
                .ASESMEN_307 = txtASESMEN_307.Text
                .ASESMEN_308 = txtASESMEN_308.Text
                .ASESMEN_309 = txtASESMEN_309.Text
                .ASESMEN_310 = txtASESMEN_310.Text
                .ASESMEN_311 = txtASESMEN_311.Text
                .ASESMEN_312 = txtASESMEN_312.Text
                .ASESMEN_313 = txtASESMEN_313.Text
                .ASESMEN_314 = txtASESMEN_314.Text
                .ASESMEN_315 = txtASESMEN_315.Text
                .ASESMEN_316 = txtASESMEN_316.Text
                .ASESMEN_317 = txtASESMEN_317.Text
                .ASESMEN_318 = txtASESMEN_318.Text
                .ASESMEN_319 = txtASESMEN_319.Text
                .ASESMEN_320 = txtASESMEN_320.Text
                .ASESMEN_321 = txtASESMEN_321.Text
                .ASESMEN_322 = txtASESMEN_322.Text
                .ASESMEN_323 = txtASESMEN_323.Text
                .ASESMEN_324 = txtASESMEN_324.Text
                .ASESMEN_325 = txtASESMEN_325.Text
                .ASESMEN_326 = txtASESMEN_326.Text
                .ASESMEN_327 = txtASESMEN_327.Text
                .ASESMEN_328 = txtASESMEN_328.Text
                .ASESMEN_329 = txtASESMEN_329.Text
                .ASESMEN_330 = txtASESMEN_330.Text
                .ASESMEN_331 = txtASESMEN_331.Text
                .ASESMEN_332 = txtASESMEN_332.Text
                .ASESMEN_333 = txtASESMEN_333.Text
                .ASESMEN_334 = txtASESMEN_334.Text
                .ASESMEN_335 = txtASESMEN_335.Text
                .ASESMEN_336 = txtASESMEN_336.Text
                .ASESMEN_337 = txtASESMEN_337.Text
                .ASESMEN_338 = txtASESMEN_338.Text
                .ASESMEN_339 = txtASESMEN_339.Text
                .ASESMEN_340 = txtASESMEN_340.Text
                .ASESMEN_341 = txtASESMEN_341.Text
                .ASESMEN_342 = txtASESMEN_342.Text
                .ASESMEN_343 = txtASESMEN_343.Text
                .ASESMEN_344 = txtASESMEN_344.Text
                .ASESMEN_345 = txtASESMEN_345.Text
                .ASESMEN_346 = txtASESMEN_346.Text
                .ASESMEN_347 = txtASESMEN_347.Text
                .ASESMEN_348 = txtASESMEN_348.Text
                .ASESMEN_349 = txtASESMEN_349.Text
                .ASESMEN_350 = txtASESMEN_350.Text
                .ASESMEN_351 = txtASESMEN_351.Text
                .ASESMEN_352 = txtASESMEN_352.Text
                .ASESMEN_353 = txtASESMEN_353.Text
                .ASESMEN_354 = txtASESMEN_354.Text
                .ASESMEN_355 = txtASESMEN_355.Text
                .ASESMEN_356 = txtASESMEN_356.Text
                .ASESMEN_357 = txtASESMEN_357.Text
                .ASESMEN_358 = txtASESMEN_358.Text
                .ASESMEN_359 = txtASESMEN_359.Text
                .ASESMEN_360 = txtASESMEN_360.Text
                .ASESMEN_361 = txtASESMEN_361.Text
                .ASESMEN_362 = txtASESMEN_362.Text
                .ASESMEN_363 = txtASESMEN_363.Text
                .ASESMEN_364 = txtASESMEN_364.Text
                .ASESMEN_365 = txtASESMEN_365.Text
                .ASESMEN_366 = txtASESMEN_366.Text
                .ASESMEN_367 = txtASESMEN_367.Text
                .ASESMEN_368 = txtASESMEN_368.Text
                .ASESMEN_369 = txtASESMEN_369.Text
                .ASESMEN_370 = txtASESMEN_370.Text
                .ASESMEN_371 = txtASESMEN_371.Text
                .ASESMEN_372 = txtASESMEN_372.Text
                .ASESMEN_373 = chkASESMEN_373.Checked
                .ASESMEN_374 = chkASESMEN_374.Checked
                .ASESMEN_375 = chkASESMEN_375.Checked
                .ASESMEN_376 = txtASESMEN_376.Text
                .ASESMEN_377 = chkASESMEN_377.Checked
                .ASESMEN_378 = chkASESMEN_378.Checked
                .ASESMEN_379 = chkASESMEN_379.Checked
                .ASESMEN_380 = chkASESMEN_380.Checked
                .ASESMEN_381 = chkASESMEN_381.Checked
                .ASESMEN_382 = chkASESMEN_382.Checked
                .ASESMEN_383 = txtASESMEN_383.Text
                .ASESMEN_384 = txtASESMEN_384.Text
                .ASESMEN_385 = txtASESMEN_385.Text
                .ASESMEN_386 = txtASESMEN_386.Text
                .ASESMEN_387 = txtASESMEN_387.Text
                .ASESMEN_388 = chkASESMEN_388.Checked
                .ASESMEN_389 = chkASESMEN_389.Checked
                .ASESMEN_390 = chkASESMEN_390.Checked
                .ASESMEN_391 = chkASESMEN_391.Checked
                .ASESMEN_392 = chkASESMEN_392.Checked
                .ASESMEN_393 = chkASESMEN_393.Checked
                .ASESMEN_394 = chkASESMEN_394.Checked
                .ASESMEN_395 = chkASESMEN_395.Checked
                .ASESMEN_396 = chkASESMEN_396.Checked
                .ASESMEN_397 = chkASESMEN_397.Checked
                .ASESMEN_398 = chkASESMEN_398.Checked
                .ASESMEN_399 = txtASESMEN_399.Text
                .ASESMEN_400 = chkASESMEN_400.Checked
                .ASESMEN_401 = chkASESMEN_401.Checked
                .ASESMEN_402 = chkASESMEN_402.Checked
                .ASESMEN_403 = chkASESMEN_403.Checked
                .ASESMEN_404 = txtASESMEN_404.Text
                .ASESMEN_405 = chkASESMEN_405.Checked
                .ASESMEN_406 = chkASESMEN_406.Checked
                .ASESMEN_407 = chkASESMEN_407.Checked
                .ASESMEN_408 = chkASESMEN_408.Checked
                .ASESMEN_409 = chkASESMEN_409.Checked
                .ASESMEN_410 = chkASESMEN_410.Checked
                .ASESMEN_411 = chkASESMEN_411.Checked
                .ASESMEN_412 = chkASESMEN_412.Checked
                .ASESMEN_413 = chkASESMEN_413.Checked
                .ASESMEN_414 = chkASESMEN_414.Checked
                .ASESMEN_415 = chkASESMEN_415.Checked
                .ASESMEN_416 = chkASESMEN_416.Checked
                .ASESMEN_417 = chkASESMEN_417.Checked
                .ASESMEN_418 = chkASESMEN_418.Checked
                .ASESMEN_419 = chkASESMEN_419.Checked
                .ASESMEN_420 = chkASESMEN_420.Checked
                .ASESMEN_421 = chkASESMEN_421.Checked
                .ASESMEN_422 = chkASESMEN_422.Checked
                .ASESMEN_423 = chkASESMEN_423.Checked
                .ASESMEN_424 = chkASESMEN_424.Checked
                .ASESMEN_425 = chkASESMEN_425.Checked
                .ASESMEN_426 = chkASESMEN_426.Checked
                .ASESMEN_427 = chkASESMEN_427.Checked
                .ASESMEN_428 = chkASESMEN_428.Checked
                .ASESMEN_429 = chkASESMEN_429.Checked
                .ASESMEN_430 = chkASESMEN_430.Checked
                .ASESMEN_431 = chkASESMEN_431.Checked
                .ASESMEN_432 = chkASESMEN_432.Checked
                .ASESMEN_433 = chkASESMEN_433.Checked
                .ASESMEN_434 = chkASESMEN_434.Checked
                .ASESMEN_435 = chkASESMEN_435.Checked
                .ASESMEN_436 = chkASESMEN_436.Checked
                .ASESMEN_437 = chkASESMEN_437.Checked
                .ASESMEN_438 = chkASESMEN_438.Checked
                .ASESMEN_439 = txtASESMEN_439.Text
                .ASESMEN_440 = chkASESMEN_440.Checked
                .JAM = txtJAM.Text
                .DATE = deDATE.DateTime
                .DOKTER_KODE = ""
                .DOKTER_NAMEDISPLAY = txtDPJP.Text

                Try
                    .CETAK = oS_DIGITAL_RI_40.GetData(txtNoRegister.Text).CETAK
                Catch ex As Exception
                    .CETAK = 0
                End Try

                .KDUSER = sKDUSER_PERAWAT
                .KDUSER_SIGNATURE = sUserID

                'tambahan
                .TANGGALSHD1 = deTANGGALSHD1.Text
                .TANGGALSHD2 = deTANGGALSHD2.Text
                .TANGGALSHD3 = deTANGGALSHD3.Text
                .TANGGALSHD4 = deTANGGALSHD4.Text
                .TANGGALSHD5 = deTANGGALSHD5.Text
                .TANGGALSHD6 = deTANGGALSHD6.Text
                .TANGGALSHD7 = deTANGGALSHD7.Text

                .SKORSHD1 = txtSKORSHD1.Text
                .SKORSHD2 = txtSKORSHD2.Text
                .SKORSHD3 = txtSKORSHD3.Text
                .SKORSHD4 = txtSKORSHD4.Text
                .SKORSHD5 = txtSKORSHD5.Text
                .SKORSHD6 = txtSKORSHD6.Text
                .SKORSHD7 = txtSKORSHD7.Text

                .SKORSHD8 = txtSKORSHD8.Text
                .SKORSHD9 = txtSKORSHD9.Text
                .SKORSHD10 = txtSKORSHD10.Text
                .SKORSHD11 = txtSKORSHD11.Text
                .SKORSHD12 = txtSKORSHD12.Text
                .SKORSHD13 = txtSKORSHD13.Text
                .SKORSHD14 = txtSKORSHD14.Text

                .SKORSHD15 = txtSKORSHD15.Text
                .SKORSHD16 = txtSKORSHD16.Text
                .SKORSHD17 = txtSKORSHD17.Text
                .SKORSHD18 = txtSKORSHD18.Text
                .SKORSHD19 = txtSKORSHD19.Text
                .SKORSHD20 = txtSKORSHD20.Text
                .SKORSHD21 = txtSKORSHD21.Text

                .SKORSHD22 = txtSKORSHD22.Text
                .SKORSHD23 = txtSKORSHD23.Text
                .SKORSHD24 = txtSKORSHD24.Text
                .SKORSHD25 = txtSKORSHD25.Text
                .SKORSHD26 = txtSKORSHD26.Text
                .SKORSHD27 = txtSKORSHD27.Text
                .SKORSHD28 = txtSKORSHD28.Text

                .SKORSHD29 = txtSKORSHD29.Text
                .SKORSHD30 = txtSKORSHD30.Text
                .SKORSHD31 = txtSKORSHD31.Text
                .SKORSHD32 = txtSKORSHD32.Text
                .SKORSHD33 = txtSKORSHD33.Text
                .SKORSHD34 = txtSKORSHD34.Text
                .SKORSHD35 = txtSKORSHD35.Text

                .SKORSHD36 = txtSKORSHD36.Text
                .SKORSHD37 = txtSKORSHD37.Text
                .SKORSHD38 = txtSKORSHD38.Text
                .SKORSHD39 = txtSKORSHD39.Text
                .SKORSHD40 = txtSKORSHD40.Text
                .SKORSHD41 = txtSKORSHD41.Text
                .SKORSHD42 = txtSKORSHD42.Text

                .TOTALSKOR2 = txtTOTALSKOR2.Text
                .TOTALSKOR3 = txtTOTALSKOR3.Text
                .TOTALSKOR4 = txtTOTALSKOR4.Text
                .TOTALSKOR5 = txtTOTALSKOR5.Text
                .TOTALSKOR6 = txtTOTALSKOR6.Text
                .TOTALSKOR7 = txtTOTALSKOR7.Text

                .TANGGALMORR_1 = deTANGGALMORR_1.Text
                .TANGGALMORR_2 = deTANGGALMORR_2.Text
                .TANGGALMORR_3 = deTANGGALMORR_3.Text
                .TANGGALMORR_4 = deTANGGALMORR_4.Text
                .TANGGALMORR_5 = deTANGGALMORR_5.Text
                .TANGGALMORR_6 = deTANGGALMORR_6.Text
                .TANGGALMORR_7 = deTANGGALMORR_7.Text

                .MORR_1 = chkMORR_1.Checked
                .MORR_2 = chkMORR_2.Checked
                .MORR_3 = chkMORR_3.Checked
                .MORR_4 = chkMORR_4.Checked
                .MORR_5 = chkMORR_5.Checked
                .MORR_6 = chkMORR_6.Checked
                .MORR_7 = chkMORR_7.Checked
                .MORR_8 = chkMORR_8.Checked
                .MORR_9 = chkMORR_9.Checked
                .MORR_10 = chkMORR_10.Checked
                .MORR_11 = chkMORR_11.Checked
                .MORR_12 = chkMORR_12.Checked
                .MORR_13 = chkMORR_13.Checked
                .MORR_14 = chkMORR_14.Checked
                .MORR_15 = chkMORR_15.Checked
                .MORR_16 = chkMORR_16.Checked
                .MORR_17 = chkMORR_17.Checked
                .MORR_18 = chkMORR_18.Checked
                .MORR_19 = chkMORR_19.Checked
                .MORR_20 = chkMORR_20.Checked
                .MORR_21 = chkMORR_21.Checked
                .MORR_22 = chkMORR_22.Checked
                .MORR_23 = chkMORR_23.Checked
                .MORR_24 = chkMORR_24.Checked
                .MORR_25 = chkMORR_25.Checked
                .MORR_26 = chkMORR_26.Checked
                .MORR_27 = chkMORR_27.Checked
                .MORR_28 = chkMORR_28.Checked
                .MORR_29 = chkMORR_29.Checked
                .MORR_30 = chkMORR_30.Checked
                .MORR_31 = chkMORR_31.Checked
                .MORR_32 = chkMORR_32.Checked
                .MORR_33 = chkMORR_33.Checked
                .MORR_34 = chkMORR_34.Checked
                .MORR_35 = chkMORR_35.Checked
                .MORR_36 = chkMORR_36.Checked
                .MORR_37 = chkMORR_37.Checked
                .MORR_38 = chkMORR_38.Checked
                .MORR_39 = chkMORR_39.Checked
                .MORR_40 = chkMORR_40.Checked
                .MORR_41 = chkMORR_41.Checked
                .MORR_42 = chkMORR_42.Checked
                .MORR_43 = chkMORR_43.Checked
                .MORR_44 = chkMORR_44.Checked
                .MORR_45 = chkMORR_45.Checked
                .MORR_46 = chkMORR_46.Checked
                .MORR_47 = chkMORR_47.Checked
                .MORR_48 = chkMORR_48.Checked
                .MORR_49 = chkMORR_49.Checked
                .MORR_50 = chkMORR_50.Checked
                .MORR_51 = chkMORR_51.Checked
                .MORR_52 = chkMORR_52.Checked
                .MORR_53 = chkMORR_53.Checked
                .MORR_54 = chkMORR_54.Checked

                .PETUGASMORR_1 = txtPETUGASMORR_1.Text
                .PETUGASMORR_2 = txtPETUGASMORR_2.Text
                .PETUGASMORR_3 = txtPETUGASMORR_3.Text
                .PETUGASMORR_4 = txtPETUGASMORR_4.Text
                .PETUGASMORR_5 = txtPETUGASMORR_5.Text
                .PETUGASMORR_6 = txtPETUGASMORR_6.Text
                .PETUGASMORR_7 = txtPETUGASMORR_7.Text

                .TANGGALMORT_1 = deTANGGALMORT_1.Text
                .TANGGALMORT_2 = deTANGGALMORT_2.Text
                .TANGGALMORT_3 = deTANGGALMORT_3.Text
                .TANGGALMORT_4 = deTANGGALMORT_4.Text
                .TANGGALMORT_5 = deTANGGALMORT_5.Text
                .TANGGALMORT_6 = deTANGGALMORT_6.Text
                .TANGGALMORT_7 = deTANGGALMORT_7.Text

                .MORT_1 = chkMORT_1.Checked
                .MORT_2 = chkMORT_2.Checked
                .MORT_3 = chkMORT_3.Checked
                .MORT_4 = chkMORT_4.Checked
                .MORT_5 = chkMORT_5.Checked
                .MORT_6 = chkMORT_6.Checked
                .MORT_7 = chkMORT_7.Checked
                .MORT_8 = chkMORT_8.Checked
                .MORT_9 = chkMORT_9.Checked
                .MORT_10 = chkMORT_10.Checked
                .MORT_11 = chkMORT_11.Checked
                .MORT_12 = chkMORT_12.Checked
                .MORT_13 = chkMORT_13.Checked
                .MORT_14 = chkMORT_14.Checked
                .MORT_15 = chkMORT_15.Checked
                .MORT_16 = chkMORT_16.Checked
                .MORT_17 = chkMORT_17.Checked
                .MORT_18 = chkMORT_18.Checked
                .MORT_19 = chkMORT_19.Checked
                .MORT_20 = chkMORT_20.Checked
                .MORT_21 = chkMORT_21.Checked
                .MORT_22 = chkMORT_22.Checked
                .MORT_23 = chkMORT_23.Checked
                .MORT_24 = chkMORT_24.Checked
                .MORT_25 = chkMORT_25.Checked
                .MORT_26 = chkMORT_26.Checked
                .MORT_27 = chkMORT_27.Checked
                .MORT_28 = chkMORT_28.Checked
                .MORT_29 = chkMORT_29.Checked
                .MORT_30 = chkMORT_30.Checked
                .MORT_31 = chkMORT_31.Checked
                .MORT_32 = chkMORT_32.Checked
                .MORT_33 = chkMORT_33.Checked
                .MORT_34 = chkMORT_34.Checked
                .MORT_35 = chkMORT_35.Checked
                .MORT_36 = chkMORT_36.Checked
                .MORT_37 = chkMORT_37.Checked
                .MORT_38 = chkMORT_38.Checked
                .MORT_39 = chkMORT_39.Checked
                .MORT_40 = chkMORT_40.Checked
                .MORT_41 = chkMORT_41.Checked
                .MORT_42 = chkMORT_42.Checked
                .MORT_43 = chkMORT_43.Checked
                .MORT_44 = chkMORT_44.Checked
                .MORT_45 = chkMORT_45.Checked
                .MORT_46 = chkMORT_46.Checked
                .MORT_47 = chkMORT_47.Checked
                .MORT_48 = chkMORT_48.Checked

                .PETUGASMORT_1 = txtPETUGASMORT_1.Text
                .PETUGASMORT_2 = txtPETUGASMORT_2.Text
                .PETUGASMORT_3 = txtPETUGASMORT_3.Text
                .PETUGASMORT_4 = txtPETUGASMORT_4.Text
                .PETUGASMORT_5 = txtPETUGASMORT_5.Text
                .PETUGASMORT_6 = txtPETUGASMORT_6.Text
                .PETUGASMORT_7 = txtPETUGASMORT_7.Text
                
                .MASALAHKEP_1 = chkMASALAHKEP_1.Checked
                .MASALAHKEP_2 = chkMASALAHKEP_2.Checked
                .MASALAHKEP_3 = chkMASALAHKEP_3.Checked
                .MASALAHKEP_4 = chkMASALAHKEP_4.Checked
                .MASALAHKEP_5 = chkMASALAHKEP_5.Checked
                .MASALAHKEP_6 = chkMASALAHKEP_6.Checked
                .MASALAHKEP_7 = chkMASALAHKEP_7.Checked
                .MASALAHKEP_8 = chkMASALAHKEP_8.Checked
                .MASALAHKEP_9 = chkMASALAHKEP_9.Checked
                .MASALAHKEP_10 = chkMASALAHKEP_10.Checked
                .MASALAHKEP_11 = chkMASALAHKEP_11.Checked
                .MASALAHKEP_12 = chkMASALAHKEP_12.Checked
                .MASALAHKEP_13 = chkMASALAHKEP_13.Checked
                .MASALAHKEP_14 = chkMASALAHKEP_14.Checked
                .MASALAHKEP_15 = chkMASALAHKEP_15.Checked
                .MASALAHKEP_16 = chkMASALAHKEP_16.Checked
                .MASALAHKEP_17 = chkMASALAHKEP_17.Checked
                .MASALAHKEP_18 = chkMASALAHKEP_18.Checked
                .MASALAHKEP_19 = chkMASALAHKEP_19.Checked
                .MASALAHKEP_20 = chkMASALAHKEP_20.Checked
                .MASALAHKEP_21 = chkMASALAHKEP_21.Checked
                .MASALAHKEP_22 = chkMASALAHKEP_22.Checked
                .MASALAHKEP_23 = chkMASALAHKEP_23.Checked
                .MASALAHKEP_24 = chkMASALAHKEP_24.Checked
                .MASALAHKEP_25 = chkMASALAHKEP_25.Checked
                .MASALAHKEP_26 = chkMASALAHKEP_26.Checked
                .MASALAHKEP_27 = chkMASALAHKEP_27.Checked
                .MASALAHKEP_28 = chkMASALAHKEP_28.Checked
                .MASALAHKEP_29 = chkMASALAHKEP_29.Checked
                .MASALAHKEP_30 = chkMASALAHKEP_30.Checked
                .MASALAHKEP_31 = chkMASALAHKEP_31.Checked
                .MASALAHKEP_32 = chkMASALAHKEP_32.Checked
                .MASALAHKEP_33 = chkMASALAHKEP_33.Checked
                .MASALAHKEP_34 = chkMASALAHKEP_34.Checked
                .MASALAHKEP_35 = chkMASALAHKEP_35.Checked
                .MASALAHKEP_36 = chkMASALAHKEP_36.Checked
                .MASALAHKEP_37 = chkMASALAHKEP_37.Checked
                .MASALAHKEP_38 = chkMASALAHKEP_38.Checked
                .MASALAHKEP_39 = chkMASALAHKEP_39.Checked
                .MASALAHKEP_40 = chkMASALAHKEP_40.Checked
                .MASALAHKEP_41 = chkMASALAHKEP_41.Checked
                .MASALAHKEP_42 = chkMASALAHKEP_42.Checked
                .MASALAHKEP_43 = chkMASALAHKEP_43.Checked
                .MASALAHKEP_44 = chkMASALAHKEP_44.Checked
                .MASALAHKEP_44_TEXT = txtMASALAHKEP_44_TEXT.Text


                .NEW_01 = TextEdit1.Text
                .NEW_02 = TextEdit2.Text
                .NEW_03 = TextEdit3.Text
                .NEW_04 = TextEdit4.Text
                .NEW_05 = TextEdit5.Text
                .NEW_06 = TextEdit6.Text
                .NEW_07 = TextEdit7.Text
                .NEW_08 = TextEdit8.Text
                .NEW_09 = TextEdit9.Text
                .NEW_10 = TextEdit10.Text
                .NEW_11 = TextEdit11.Text
                .NEW_12 = TextEdit12.Text

                .NEW_01_BIT = CheckEdit1.Checked
                .NEW_02_BIT = CheckEdit2.Checked
                .NEW_03_BIT = CheckEdit3.Checked
                .NEW_04_BIT = CheckEdit4.Checked
                .NEW_05_BIT = CheckEdit5.Checked
                .NEW_06_BIT = CheckEdit6.Checked
                .NEW_07_BIT = CheckEdit7.Checked
                .NEW_08_BIT = CheckEdit8.Checked
                .NEW_09_BIT = CheckEdit9.Checked
                .NEW_10_BIT = CheckEdit10.Checked
                .NEW_11_BIT = CheckEdit11.Checked
                .NEW_12_BIT = CheckEdit12.Checked
                .NEW_13_BIT = CheckEdit13.Checked
                .NEW_14_BIT = CheckEdit14.Checked
                .NEW_15_BIT = CheckEdit15.Checked
                .NEW_16_BIT = CheckEdit16.Checked
                .NEW_17_BIT = CheckEdit17.Checked
                .NEW_18_BIT = CheckEdit18.Checked
                .NEW_19_BIT = CheckEdit19.Checked
                .NEW_20_BIT = CheckEdit20.Checked
            End With

            If oFormMode = FORM_MODE.FORM_MODE_ADD Then
                Try
                    fn_Save = oS_DIGITAL_RI_40.InsertData(ds)
                Catch ex As Exception
                    MsgBox("Simpan Data : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            ElseIf oFormMode = FORM_MODE.FORM_MODE_EDIT Then
                Try
                    fn_Save = oS_DIGITAL_RI_40.UpdateData(ds)
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
    Private Sub txtASESMEN_80_EditValueChanged(sender As Object, e As EventArgs) Handles txtASESMEN_80.EditValueChanged, txtASESMEN_98.EditValueChanged, txtASESMEN_101.EditValueChanged, txtASESMEN_102.EditValueChanged
        Dim sAsesmen80 As Decimal = 0
        sAsesmen80 = CDec(txtASESMEN_80.Text)

        Dim sAsesmen98 As Decimal = 0
        sAsesmen98 = CDec(txtASESMEN_98.Text)

        Dim sAsesmen101 As Decimal = 0
        sAsesmen101 = CDec(txtASESMEN_101.Text)

        Dim sAsesmen102 As Decimal = 0
        sAsesmen102 = CDec(txtASESMEN_102.Text)

        txtASESMEN_103.Text = sAsesmen80 + sAsesmen98 + sAsesmen101 + sAsesmen102
    End Sub

    Private Sub txtASESMEN_197_EditValueChanged(sender As Object, e As EventArgs) Handles txtASESMEN_197.EditValueChanged, txtASESMEN_198.EditValueChanged, txtASESMEN_199.EditValueChanged, txtASESMEN_200.EditValueChanged, txtASESMEN_201.EditValueChanged, txtASESMEN_202.EditValueChanged, txtASESMEN_203.EditValueChanged
        Dim sAsesmen197 As Decimal = 0
        sAsesmen197 = CDec(txtASESMEN_197.Text)

        Dim sAsesmen198 As Decimal = 0
        sAsesmen198 = CDec(txtASESMEN_198.Text)

        Dim sAsesmen199 As Decimal = 0
        sAsesmen199 = CDec(txtASESMEN_199.Text)

        Dim sAsesmen200 As Decimal = 0
        sAsesmen200 = CDec(txtASESMEN_200.Text)

        Dim sAsesmen201 As Decimal = 0
        sAsesmen201 = CDec(txtASESMEN_201.Text)

        Dim sAsesmen202 As Decimal = 0
        sAsesmen202 = CDec(txtASESMEN_202.Text)

        Dim sAsesmen203 As Decimal = 0
        sAsesmen203 = CDec(txtASESMEN_203.Text)

        txtASESMEN_204.Text = sAsesmen197 + sAsesmen198 + sAsesmen199 + sAsesmen200 + sAsesmen201 + sAsesmen202 + sAsesmen203
    End Sub

    Private Sub txtASESMEN_224_EditValueChanged(sender As Object, e As EventArgs) Handles txtASESMEN_224.EditValueChanged, txtASESMEN_225.EditValueChanged, txtASESMEN_226.EditValueChanged, txtASESMEN_227.EditValueChanged, txtASESMEN_228.EditValueChanged
        Dim sAsesmen224 As Decimal = 0
        sAsesmen224 = CDec(txtASESMEN_224.Text)

        Dim sAsesmen225 As Decimal = 0
        sAsesmen225 = CDec(txtASESMEN_225.Text)

        Dim sAsesmen226 As Decimal = 0
        sAsesmen226 = CDec(txtASESMEN_226.Text)

        Dim sAsesmen227 As Decimal = 0
        sAsesmen227 = CDec(txtASESMEN_227.Text)

        Dim sAsesmen228 As Decimal = 0
        sAsesmen228 = CDec(txtASESMEN_228.Text)

        txtASESMEN_229.Text = sAsesmen224 + sAsesmen225 + sAsesmen226 + sAsesmen227 + sAsesmen228
    End Sub

    'tambahan
    Private Sub txtSKORSHD1_EditValueChanged(sender As Object, e As EventArgs) Handles txtSKORSHD1.EditValueChanged,txtSKORSHD2.EditValueChanged,txtSKORSHD3.EditValueChanged,txtSKORSHD4.EditValueChanged,txtSKORSHD5.EditValueChanged,txtSKORSHD6.EditValueChanged,txtSKORSHD7.EditValueChanged
        Dim sSkorHD1 As Decimal = CDec(txtSKORSHD1.Text)
        Dim sSkorHD2 As Decimal = CDec(txtSKORSHD2.Text)
        Dim sSkorHD3 As Decimal = CDec(txtSKORSHD3.Text)
        Dim sSkorHD4 As Decimal = CDec(txtSKORSHD4.Text)
        Dim sSkorHD5 As Decimal = CDec(txtSKORSHD5.Text)
        Dim sSkorHD6 As Decimal = CDec(txtSKORSHD6.Text)
        Dim sSkorHD7 As Decimal = CDec(txtSKORSHD7.Text)

        txtTOTALSKOR2.Text = sSkorHD1 + sSkorHD2 + sSkorHD3 + sSkorHD4 + sSkorHD5 + sSkorHD6 + sSkorHD7
    End Sub

    Private Sub txtSKORSHD8_EditValueChanged(sender As Object, e As EventArgs) Handles txtSKORSHD8.EditValueChanged,txtSKORSHD9.EditValueChanged,txtSKORSHD10.EditValueChanged,txtSKORSHD11.EditValueChanged,txtSKORSHD12.EditValueChanged,txtSKORSHD13.EditValueChanged,txtSKORSHD14.EditValueChanged
        Dim sSkorHD8 As Decimal = CDec(txtSKORSHD8.Text)
        Dim sSkorHD9 As Decimal = CDec(txtSKORSHD9.Text)
        Dim sSkorHD10 As Decimal = CDec(txtSKORSHD10.Text)
        Dim sSkorHD11 As Decimal = CDec(txtSKORSHD11.Text)
        Dim sSkorHD12 As Decimal = CDec(txtSKORSHD12.Text)
        Dim sSkorHD13 As Decimal = CDec(txtSKORSHD13.Text)
        Dim sSkorHD14 As Decimal = CDec(txtSKORSHD14.Text)

        txtTOTALSKOR3.Text = sSkorHD8 + sSkorHD9 + sSkorHD10 + sSkorHD11 + sSkorHD12 + sSkorHD13 + sSkorHD14
    End Sub

    Private Sub txtSKORSHD15_EditValueChanged(sender As Object, e As EventArgs) Handles txtSKORSHD15.EditValueChanged,txtSKORSHD16.EditValueChanged,txtSKORSHD17.EditValueChanged,txtSKORSHD18.EditValueChanged,txtSKORSHD19.EditValueChanged,txtSKORSHD20.EditValueChanged,txtSKORSHD21.EditValueChanged
        Dim sSkorHD15 As Decimal = CDec(txtSKORSHD15.Text)
        Dim sSkorHD16 As Decimal = CDec(txtSKORSHD16.Text)
        Dim sSkorHD17 As Decimal = CDec(txtSKORSHD17.Text)
        Dim sSkorHD18 As Decimal = CDec(txtSKORSHD18.Text)
        Dim sSkorHD19 As Decimal = CDec(txtSKORSHD19.Text)
        Dim sSkorHD20 As Decimal = CDec(txtSKORSHD20.Text)
        Dim sSkorHD21 As Decimal = CDec(txtSKORSHD21.Text)

        txtTOTALSKOR4.Text = sSkorHD15 + sSkorHD16 + sSkorHD17 + sSkorHD18 + sSkorHD19 + sSkorHD20 + sSkorHD21
    End Sub

    Private Sub txtSKORSHD22_EditValueChanged(sender As Object, e As EventArgs) Handles txtSKORSHD22.EditValueChanged,txtSKORSHD23.EditValueChanged,txtSKORSHD24.EditValueChanged,txtSKORSHD25.EditValueChanged,txtSKORSHD26.EditValueChanged,txtSKORSHD27.EditValueChanged,txtSKORSHD28.EditValueChanged
        Dim sSkorHD22 As Decimal = CDec(txtSKORSHD22.Text)
        Dim sSkorHD23 As Decimal = CDec(txtSKORSHD23.Text)
        Dim sSkorHD24 As Decimal = CDec(txtSKORSHD24.Text)
        Dim sSkorHD25 As Decimal = CDec(txtSKORSHD25.Text)
        Dim sSkorHD26 As Decimal = CDec(txtSKORSHD26.Text)
        Dim sSkorHD27 As Decimal = CDec(txtSKORSHD27.Text)
        Dim sSkorHD28 As Decimal = CDec(txtSKORSHD28.Text)

        txtTOTALSKOR5.Text = sSkorHD22 + sSkorHD23 + sSkorHD24 + sSkorHD25 + sSkorHD26 + sSkorHD27 + sSkorHD28
    End Sub

    Private Sub txtSKORSHD29_EditValueChanged(sender As Object, e As EventArgs) Handles txtSKORSHD29.EditValueChanged,txtSKORSHD30.EditValueChanged,txtSKORSHD31.EditValueChanged,txtSKORSHD32.EditValueChanged,txtSKORSHD33.EditValueChanged,txtSKORSHD34.EditValueChanged,txtSKORSHD35.EditValueChanged
        Dim sSkorHD29 As Decimal = CDec(txtSKORSHD29.Text)
        Dim sSkorHD30 As Decimal = CDec(txtSKORSHD30.Text)
        Dim sSkorHD31 As Decimal = CDec(txtSKORSHD31.Text)
        Dim sSkorHD32 As Decimal = CDec(txtSKORSHD32.Text)
        Dim sSkorHD33 As Decimal = CDec(txtSKORSHD33.Text)
        Dim sSkorHD34 As Decimal = CDec(txtSKORSHD34.Text)
        Dim sSkorHD35 As Decimal = CDec(txtSKORSHD35.Text)

        txtTOTALSKOR6.Text = sSkorHD29 + sSkorHD30 + sSkorHD31 + sSkorHD32 + sSkorHD33 + sSkorHD34 + sSkorHD35
    End Sub

    Private Sub txtSKORSHD36_EditValueChanged(sender As Object, e As EventArgs) Handles txtSKORSHD36.EditValueChanged,txtSKORSHD37.EditValueChanged,txtSKORSHD38.EditValueChanged,txtSKORSHD39.EditValueChanged,txtSKORSHD40.EditValueChanged,txtSKORSHD41.EditValueChanged,txtSKORSHD42.EditValueChanged
        Dim sSkorHD36 As Decimal = CDec(txtSKORSHD36.Text)
        Dim sSkorHD37 As Decimal = CDec(txtSKORSHD37.Text)
        Dim sSkorHD38 As Decimal = CDec(txtSKORSHD38.Text)
        Dim sSkorHD39 As Decimal = CDec(txtSKORSHD39.Text)
        Dim sSkorHD40 As Decimal = CDec(txtSKORSHD40.Text)
        Dim sSkorHD41 As Decimal = CDec(txtSKORSHD41.Text)
        Dim sSkorHD42 As Decimal = CDec(txtSKORSHD42.Text)

        txtTOTALSKOR6.Text = sSkorHD36 + sSkorHD37 + sSkorHD38 + sSkorHD39 + sSkorHD40 + sSkorHD41 + sSkorHD42
    End Sub

    Private Sub frmEMedrekRI_40_MouseWheel(sender As Object, e As MouseEventArgs) Handles MyBase.MouseWheel
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
    Private Sub grdPerawat_EditValueChanged(sender As Object, e As EventArgs) Handles grdPerawat.EditValueChanged
        If grdPerawat.Text <> "" Then
            txtPerawat.Text = grdPerawat.Text
        End If
    End Sub

#End Region
End Class