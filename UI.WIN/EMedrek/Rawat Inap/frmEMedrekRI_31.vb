
Imports System.Data.SqlClient
Imports System.Linq
Imports DataAccess

Public Class frmEMedrekRI_31
#Region "Declaration"
    Private oFormMode As FORM_MODE = FORM_MODE.FORM_MODE_VIEW
    Private isLoad As Boolean = False
    Private oS_DIGITAL_RI_31 As New Transaksi.clsDigital_SBAR
    Private sNoid As String = String.Empty
    Private sCATEGORY As Integer = 0
    Private sKDDOCTOR As String = String.Empty
    Private sCopyKode As String = String.Empty
    Private sRegister As String = String.Empty

#End Region
#Region "Function"
    Public Sub LoadMe(ByVal FormMode As Integer, ByVal CopyKode As String, ByVal KDREG As String, ByVal KDDOCTOR As String, ByVal KDCUSTOMER As String, ByVal NAMAPASIEN As String, ByVal TANGGALLAHIR As DateTime, ByVal TUJUAN As String, ByVal TANGGALDATANG As DateTime, ByVal NoId As String)
        oFormMode = FormMode
        sNoid = NoId

        txtNoRegister.Text = KDREG
        txtNamaPasien.Text = NAMAPASIEN
        txtUmur.Text = TANGGALLAHIR.ToString("dd-MM-yyyy")
        txtNoPasien.Text = KDCUSTOMER

        sKDDOCTOR = KDDOCTOR
        sCopyKode = CopyKode
        sRegister = KDREG
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
        fn_LoadDoctor()

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

        txtDITERIMA.Properties.ReadOnly = Status
        txtDISETUJUI.Properties.ReadOnly = Status
        txtRUANGAN1.Properties.ReadOnly = Status
        txtRUANGAN2.Properties.ReadOnly = Status
        deDATE.Properties.ReadOnly = Status
        txtJAM.Properties.ReadOnly = Status
        txtDIIAGNOSA_1.Properties.ReadOnly = Status
        grdKDDOCTOR.Properties.ReadOnly = Status
        grdKDDOCTOR2.Properties.ReadOnly = Status
        grdKDDOCTOR3.Properties.ReadOnly = Status
        chkASESMEN_01.Properties.ReadOnly = Status
        chkASESMEN_02.Properties.ReadOnly = Status
        txtASESMEN_03.Properties.ReadOnly = Status
        txtASESMEN_04.Properties.ReadOnly = Status
        deASESMEN_05.Properties.ReadOnly = Status
        chkASESMEN_06.Properties.ReadOnly = Status
        txtASESMEN_07.Properties.ReadOnly = Status
        chkASESMEN_08.Properties.ReadOnly = Status
        txtASESMEN_09.Properties.ReadOnly = Status
        txtASESMEN_10.Properties.ReadOnly = Status
        txtASESMEN_11.Properties.ReadOnly = Status
        txtASESMEN_12.Properties.ReadOnly = Status
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
        txtASESMEN_25.Properties.ReadOnly = Status
        txtASESMEN_26.Properties.ReadOnly = Status
        chkASESMEN_27.Properties.ReadOnly = Status
        chkASESMEN_28.Properties.ReadOnly = Status
        chkASESMEN_29.Properties.ReadOnly = Status
        txtASESMEN_30.Properties.ReadOnly = Status
        chkASESMEN_31.Properties.ReadOnly = Status
        txtASESMEN_32.Properties.ReadOnly = Status
        chkASESMEN_33.Properties.ReadOnly = Status
        txtASESMEN_34.Properties.ReadOnly = Status
        chkASESMEN_35.Properties.ReadOnly = Status
        chkASESMEN_36.Properties.ReadOnly = Status
        chkASESMEN_37.Properties.ReadOnly = Status
        chkASESMEN_38.Properties.ReadOnly = Status
        chkASESMEN_39.Properties.ReadOnly = Status
        chkASESMEN_40.Properties.ReadOnly = Status
        txtASESMEN_41.Properties.ReadOnly = Status
        txtASESMEN_42.Properties.ReadOnly = Status
        deASESMEN_43.Properties.ReadOnly = Status
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
        chkASESMEN_56.Properties.ReadOnly = Status
        chkASESMEN_57.Properties.ReadOnly = Status
        chkASESMEN_58.Properties.ReadOnly = Status
        chkASESMEN_59.Properties.ReadOnly = Status
        chkASESMEN_60.Properties.ReadOnly = Status
        txtASESMEN_61.Properties.ReadOnly = Status
        deASESMEN_62.Properties.ReadOnly = Status
        txtASESMEN_63.Properties.ReadOnly = Status
        chkASESMEN_64.Properties.ReadOnly = Status
        chkASESMEN_65.Properties.ReadOnly = Status
        chkASESMEN_66.Properties.ReadOnly = Status
        chkASESMEN_67.Properties.ReadOnly = Status
        txtASESMEN_68.Properties.ReadOnly = Status
        txtASESMEN_69.Properties.ReadOnly = Status
        txtASESMEN_70.Properties.ReadOnly = Status
        txtASESMEN_71.Properties.ReadOnly = Status
        txtASESMEN_72.Properties.ReadOnly = Status
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
        chkASESMEN_90.Properties.ReadOnly = Status
        chkASESMEN_92.Properties.ReadOnly = Status
        grvDetail.OptionsBehavior.ReadOnly = Status
        txtDiagnosa.Properties.ReadOnly = Status

    End Sub
    Private Sub fn_EmptyMe()
        txtCODE.Text = "<--- AUTO --->"
        txtRUANGAN1.ResetText()
        txtRUANGAN2.ResetText()
        deDATE.DateTime = Now
        txtJAM.ResetText()
        txtDIIAGNOSA_1.ResetText()
        grdKDDOCTOR.ResetText()
        grdKDDOCTOR2.ResetText()
        grdKDDOCTOR3.ResetText()
        chkASESMEN_01.Checked = False
        chkASESMEN_02.Checked = False
        txtASESMEN_03.ResetText()
        txtASESMEN_04.ResetText()
        deASESMEN_05.ResetText()
        chkASESMEN_06.Checked = False
        txtASESMEN_07.ResetText()
        chkASESMEN_08.Checked = False
        txtASESMEN_09.ResetText()
        txtASESMEN_10.ResetText()
        txtASESMEN_11.ResetText()
        txtASESMEN_12.ResetText()
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
        txtASESMEN_25.ResetText()
        txtASESMEN_26.ResetText()
        chkASESMEN_27.Checked = False
        chkASESMEN_28.Checked = False
        chkASESMEN_29.Checked = False
        txtASESMEN_30.ResetText()
        chkASESMEN_31.Checked = False
        txtASESMEN_32.ResetText()
        chkASESMEN_33.Checked = False
        txtASESMEN_34.ResetText()
        chkASESMEN_35.Checked = False
        chkASESMEN_36.Checked = False
        chkASESMEN_37.Checked = False
        chkASESMEN_38.Checked = False
        chkASESMEN_39.Checked = False
        chkASESMEN_40.Checked = False
        txtASESMEN_41.ResetText()
        txtASESMEN_42.ResetText()
        deASESMEN_43.ResetText()
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
        chkASESMEN_56.Checked = False
        chkASESMEN_57.Checked = False
        chkASESMEN_58.Checked = False
        chkASESMEN_59.Checked = False
        chkASESMEN_60.Checked = False
        txtASESMEN_61.ResetText()
        deASESMEN_62.ResetText()
        txtASESMEN_63.ResetText()
        chkASESMEN_64.Checked = False
        chkASESMEN_65.Checked = False
        chkASESMEN_66.Checked = False
        chkASESMEN_67.Checked = False
        txtASESMEN_68.ResetText()
        txtASESMEN_69.ResetText()
        txtASESMEN_70.ResetText()
        txtASESMEN_71.ResetText()
        txtASESMEN_72.ResetText()
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
        chkASESMEN_90.Checked = False
        chkASESMEN_92.Checked = False
        txtDiagnosa.ResetText()
        txtDITERIMA.ResetText()
        txtDISETUJUI.ResetText()

        txtDISETUJUI.Text = txtNamaPasien.Text
        txtNoRegister.Text = sRegister
        fn_LoadDataCopy(sCopyKode)
    End Sub
    Private Sub fn_LoadDataCopy(ByVal Paramater As String)
        Try
            If Paramater = "" Then Exit Sub

            ' ***** HEADER *****
            Dim ds = oS_DIGITAL_RI_31.GetData(Paramater)

            With ds
                txtCODE.Text = .KDPINDAHAN
                txtRUANGAN1.EditValue = .DEPARTMENT_NAME_DISPLAY
                txtRUANGAN2.EditValue = .DEPARTMENT2_NAME_DISPLAY
                deDATE.Text = .DATE
                txtJAM.Text = .JAM
                txtDIIAGNOSA_1.Text = .KDDIAGNOSA
                grdKDDOCTOR.EditValue = .DOCTOR_KODE
                grdKDDOCTOR2.EditValue = .DOCTOR2_KODE
                grdKDDOCTOR3.EditValue = .DOCTOR3_KODE
                chkASESMEN_01.Checked = .ASESMEN_01
                chkASESMEN_02.Checked = .ASESMEN_02
                txtASESMEN_03.Text = .ASESMEN_03
                txtASESMEN_04.Text = .ASESMEN_04
                deASESMEN_05.Text = .ASESMEN_05
                chkASESMEN_06.Checked = .ASESMEN_06
                txtASESMEN_07.Text = .ASESMEN_07
                chkASESMEN_08.Checked = .ASESMEN_08
                txtASESMEN_09.Text = .ASESMEN_09
                txtASESMEN_10.Text = .ASESMEN_10
                txtASESMEN_11.Text = .ASESMEN_11
                txtASESMEN_12.Text = .ASESMEN_12
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
                txtASESMEN_25.Text = .ASESMEN_25
                txtASESMEN_26.Text = .ASESMEN_26
                chkASESMEN_27.Checked = .ASESMEN_27
                chkASESMEN_28.Checked = .ASESMEN_28
                chkASESMEN_29.Checked = .ASESMEN_29
                txtASESMEN_30.Text = .ASESMEN_30
                chkASESMEN_31.Checked = .ASESMEN_31
                txtASESMEN_32.Text = .ASESMEN_32
                chkASESMEN_33.Checked = .ASESMEN_33
                txtASESMEN_34.Text = .ASESMEN_34
                chkASESMEN_35.Checked = .ASESMEN_35
                chkASESMEN_36.Checked = .ASESMEN_36
                chkASESMEN_37.Checked = .ASESMEN_37
                chkASESMEN_38.Checked = .ASESMEN_38
                chkASESMEN_39.Checked = .ASESMEN_39
                chkASESMEN_40.Checked = .ASESMEN_40
                txtASESMEN_41.Text = .ASESMEN_41
                txtASESMEN_42.Text = .ASESMEN_42
                deASESMEN_43.Text = .ASESMEN_43
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
                chkASESMEN_56.Checked = .ASESMEN_56
                chkASESMEN_57.Checked = .ASESMEN_57
                chkASESMEN_58.Checked = .ASESMEN_58
                chkASESMEN_59.Checked = .ASESMEN_59
                chkASESMEN_60.Checked = .ASESMEN_60
                txtASESMEN_61.Text = .ASESMEN_61
                deASESMEN_62.Text = .ASESMEN_62
                txtASESMEN_63.Text = .ASESMEN_63
                chkASESMEN_64.Checked = .ASESMEN_64
                chkASESMEN_65.Checked = .ASESMEN_65
                chkASESMEN_66.Checked = .ASESMEN_66
                chkASESMEN_67.Checked = .ASESMEN_67
                txtASESMEN_68.Text = .ASESMEN_68
                txtASESMEN_69.Text = .ASESMEN_69
                txtASESMEN_70.Text = .ASESMEN_70
                txtASESMEN_71.Text = .ASESMEN_71
                txtASESMEN_72.Text = .ASESMEN_72
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
                chkASESMEN_90.Checked = .ASESMEN_90
                chkASESMEN_92.Checked = .ASESMEN_92
                txtDiagnosa.Text = .TXTDIAGNOSA

                BindingSource.DataSource = oS_DIGITAL_RI_31.GetDataDetail.Where(Function(x) x.KDPINDAHAN = Paramater).OrderBy(Function(x) x.SEQ).ToList()
                grdDetail.DataSource = BindingSource
            End With
        Catch oErr As Exception
            MsgBox("Load List Data : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadData()
        Try
            ' ***** HEADER *****
            Dim ds = oS_DIGITAL_RI_31.GetData(sNoid)

            With ds
                txtNoRegister.Text = .KDREG
                txtCODE.Text = .KDPINDAHAN
                txtRUANGAN1.EditValue = .DEPARTMENT_NAME_DISPLAY
                txtRUANGAN2.EditValue = .DEPARTMENT2_NAME_DISPLAY
                deDATE.Text = .DATE
                txtJAM.Text = .JAM
                txtDIIAGNOSA_1.Text = .KDDIAGNOSA
                grdKDDOCTOR.EditValue = .DOCTOR_KODE
                grdKDDOCTOR2.EditValue = .DOCTOR2_KODE
                grdKDDOCTOR3.EditValue = .DOCTOR3_KODE
                chkASESMEN_01.Checked = .ASESMEN_01
                chkASESMEN_02.Checked = .ASESMEN_02
                txtASESMEN_03.Text = .ASESMEN_03
                txtASESMEN_04.Text = .ASESMEN_04
                deASESMEN_05.Text = .ASESMEN_05
                chkASESMEN_06.Checked = .ASESMEN_06
                txtASESMEN_07.Text = .ASESMEN_07
                chkASESMEN_08.Checked = .ASESMEN_08
                txtASESMEN_09.Text = .ASESMEN_09
                txtASESMEN_10.Text = .ASESMEN_10
                txtASESMEN_11.Text = .ASESMEN_11
                txtASESMEN_12.Text = .ASESMEN_12
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
                txtASESMEN_25.Text = .ASESMEN_25
                txtASESMEN_26.Text = .ASESMEN_26
                chkASESMEN_27.Checked = .ASESMEN_27
                chkASESMEN_28.Checked = .ASESMEN_28
                chkASESMEN_29.Checked = .ASESMEN_29
                txtASESMEN_30.Text = .ASESMEN_30
                chkASESMEN_31.Checked = .ASESMEN_31
                txtASESMEN_32.Text = .ASESMEN_32
                chkASESMEN_33.Checked = .ASESMEN_33
                txtASESMEN_34.Text = .ASESMEN_34
                chkASESMEN_35.Checked = .ASESMEN_35
                chkASESMEN_36.Checked = .ASESMEN_36
                chkASESMEN_37.Checked = .ASESMEN_37
                chkASESMEN_38.Checked = .ASESMEN_38
                chkASESMEN_39.Checked = .ASESMEN_39
                chkASESMEN_40.Checked = .ASESMEN_40
                txtASESMEN_41.Text = .ASESMEN_41
                txtASESMEN_42.Text = .ASESMEN_42
                deASESMEN_43.Text = .ASESMEN_43
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
                chkASESMEN_56.Checked = .ASESMEN_56
                chkASESMEN_57.Checked = .ASESMEN_57
                chkASESMEN_58.Checked = .ASESMEN_58
                chkASESMEN_59.Checked = .ASESMEN_59
                chkASESMEN_60.Checked = .ASESMEN_60
                txtASESMEN_61.Text = .ASESMEN_61
                deASESMEN_62.Text = .ASESMEN_62
                txtASESMEN_63.Text = .ASESMEN_63
                chkASESMEN_64.Checked = .ASESMEN_64
                chkASESMEN_65.Checked = .ASESMEN_65
                chkASESMEN_66.Checked = .ASESMEN_66
                chkASESMEN_67.Checked = .ASESMEN_67
                txtASESMEN_68.Text = .ASESMEN_68
                txtASESMEN_69.Text = .ASESMEN_69
                txtASESMEN_70.Text = .ASESMEN_70
                txtASESMEN_71.Text = .ASESMEN_71
                txtASESMEN_72.Text = .ASESMEN_72
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
                chkASESMEN_90.Checked = .ASESMEN_90
                chkASESMEN_92.Checked = .ASESMEN_92
                txtDiagnosa.Text = .TXTDIAGNOSA
                txtDISETUJUI.Text = .ASESMEN_91
                txtDITERIMA.Text = .DOCTOR2_2_NAME_DISPLAY

                BindingSource.DataSource = oS_DIGITAL_RI_31.GetDataDetail.Where(Function(x) x.KDPINDAHAN = sNoid).OrderBy(Function(x) x.SEQ).ToList()
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
            Dim ds = oS_DIGITAL_RI_31.GetStructureHeader
            With ds
                .KDPINDAHAN = sNoid
                .KDREG = txtNoRegister.Text
                .KDCUSTOMER = txtNoPasien.Text
                Try
                    .DATECREATED = oS_DIGITAL_RI_31.GetData(txtNoRegister.Text).DATECREATED
                Catch ex As Exception
                    .DATECREATED = Now
                End Try
                .DATEUPDATED = Now
                .DEPARTMENT_KODE = ""
                .DEPARTMENT_NAME_DISPLAY = txtRUANGAN1.Text
                .DEPARTMENT2_KODE = ""
                .DEPARTMENT2_NAME_DISPLAY = txtRUANGAN2.Text
                .DATE = deDATE.DateTime
                .JAM = txtJAM.Text
                .KDDIAGNOSA = txtDiagnosa.Text
                .DOCTOR_KODE = grdKDDOCTOR.EditValue
                .DOCTOR_NAME_DISPLAY = grdKDDOCTOR.Text
                .DOCTOR2_KODE = grdKDDOCTOR2.EditValue
                .DOCTOR2_NAME_DISPLAY = grdKDDOCTOR2.Text
                .DOCTOR3_KODE = grdKDDOCTOR3.EditValue
                .DOCTOR3_NAME_DISPLAY = grdKDDOCTOR3.Text
                .ASESMEN_01 = chkASESMEN_01.Checked
                .ASESMEN_02 = chkASESMEN_02.Checked
                .ASESMEN_03 = txtASESMEN_03.Text
                .ASESMEN_04 = txtASESMEN_04.Text
                .ASESMEN_05 = deASESMEN_05.Text
                .ASESMEN_06 = chkASESMEN_06.Checked
                .ASESMEN_07 = txtASESMEN_07.Text
                .ASESMEN_08 = chkASESMEN_08.Checked
                .ASESMEN_09 = txtASESMEN_09.Text
                .ASESMEN_10 = txtASESMEN_10.Text
                .ASESMEN_11 = txtASESMEN_11.Text
                .ASESMEN_12 = txtASESMEN_12.Text
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
                .ASESMEN_25 = txtASESMEN_25.Text
                .ASESMEN_26 = txtASESMEN_26.Text
                .ASESMEN_27 = chkASESMEN_27.Checked
                .ASESMEN_28 = chkASESMEN_28.Checked
                .ASESMEN_29 = chkASESMEN_29.Checked
                .ASESMEN_30 = txtASESMEN_30.Text
                .ASESMEN_31 = chkASESMEN_31.Checked
                .ASESMEN_32 = txtASESMEN_32.Text
                .ASESMEN_33 = chkASESMEN_33.Checked
                .ASESMEN_34 = txtASESMEN_34.Text
                .ASESMEN_35 = chkASESMEN_35.Checked
                .ASESMEN_36 = chkASESMEN_36.Checked
                .ASESMEN_37 = chkASESMEN_37.Checked
                .ASESMEN_38 = chkASESMEN_38.Checked
                .ASESMEN_39 = chkASESMEN_39.Checked
                .ASESMEN_40 = chkASESMEN_40.Checked
                .ASESMEN_41 = txtASESMEN_41.Text
                .ASESMEN_42 = txtASESMEN_42.Text
                .ASESMEN_43 = deASESMEN_43.Text
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
                .ASESMEN_56 = chkASESMEN_56.Checked
                .ASESMEN_57 = chkASESMEN_57.Checked
                .ASESMEN_58 = chkASESMEN_58.Checked
                .ASESMEN_59 = chkASESMEN_59.Checked
                .ASESMEN_60 = chkASESMEN_60.Checked
                .ASESMEN_61 = txtASESMEN_61.Text
                .ASESMEN_62 = deASESMEN_62.Text
                .ASESMEN_63 = txtASESMEN_63.Text
                .ASESMEN_64 = chkASESMEN_64.Checked
                .ASESMEN_65 = chkASESMEN_65.Checked
                .ASESMEN_66 = chkASESMEN_66.Checked
                .ASESMEN_67 = chkASESMEN_67.Checked
                .ASESMEN_68 = txtASESMEN_68.Text
                .ASESMEN_69 = txtASESMEN_69.Text
                .ASESMEN_70 = txtASESMEN_70.Text
                .ASESMEN_71 = txtASESMEN_71.Text
                .ASESMEN_72 = txtASESMEN_72.Text
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
                .ASESMEN_90 = chkASESMEN_90.Checked
                .ASESMEN_92 = chkASESMEN_92.Checked
                .TXTDIAGNOSA = txtDiagnosa.Text

                .ASESMEN_91 = txtDISETUJUI.Text
                .DOCTOR2_2_NAME_DISPLAY = txtDITERIMA.Text
                Try
                    .DOCTOR2_1_KODE = oS_DIGITAL_RI_31.GetData(sNoid).DOCTOR2_1_KODE
                Catch ex As Exception
                    .DOCTOR2_1_KODE = ""
                End Try
                Try
                    .DOCTOR2_1_NAME_DISPLAY = oS_DIGITAL_RI_31.GetData(sNoid).DOCTOR2_1_NAME_DISPLAY
                Catch ex As Exception
                    .DOCTOR2_1_NAME_DISPLAY = ""
                End Try
                Try
                    .DOCTOR2_2_KODE = oS_DIGITAL_RI_31.GetData(sNoid).DOCTOR2_2_KODE
                Catch ex As Exception
                    .DOCTOR2_2_KODE = ""
                End Try
                Try
                    .CETAK = oS_DIGITAL_RI_31.GetData(sNoid).CETAK
                Catch ex As Exception
                    .CETAK = 0
                End Try
                Try
                    .KDUSER = oS_DIGITAL_RI_31.GetData(sNoid).KDUSER
                Catch ex As Exception
                    .KDUSER = sUserID
                End Try
                .KDUSER_SIGNATURE = ""
            End With

            ' ***** DETIL *****
            Dim arrDetail = oS_DIGITAL_RI_31.GetStructureDetailList
            For i As Integer = 0 To grvDetail.RowCount - 2
                Dim dsDetail = oS_DIGITAL_RI_31.GetStructureDetail
                With dsDetail
                    .SEQ = i
                    .KDPINDAHAN = ds.KDPINDAHAN
                    .KDREG = txtNoRegister.Text
                    .TANGGAL = grvDetail.GetRowCellValue(i, colDATE)
                    .TENSI = grvDetail.GetRowCellValue(i, colTENSI)
                    .NADI = grvDetail.GetRowCellValue(i, colNADI)
                    .SUHU = grvDetail.GetRowCellValue(i, colSUHU)
                    .RESPIRASI = grvDetail.GetRowCellValue(i, colRESPIRASI)
                    .KES = grvDetail.GetRowCellValue(i, colKES)
                    .SPO2 = grvDetail.GetRowCellValue(i, colSPO2)
                    .ORAL = grvDetail.GetRowCellValue(i, colORAL)
                    .INFUS = grvDetail.GetRowCellValue(i, colINFUS)
                    .DARAH = grvDetail.GetRowCellValue(i, colDARAH)
                    .URINE = grvDetail.GetRowCellValue(i, colURINE)
                    .DRAIN = grvDetail.GetRowCellValue(i, colDRAIN)
                    .NGT = grvDetail.GetRowCellValue(i, colNGT)
                    .CATATAN = grvDetail.GetRowCellValue(i, colCATATAN)
                End With
                arrDetail.Add(dsDetail)
            Next

            If oFormMode = FORM_MODE.FORM_MODE_ADD Then
                Try
                    fn_Save = oS_DIGITAL_RI_31.InsertData(ds, arrDetail)
                Catch ex As Exception
                    MsgBox("Simpan Data : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            ElseIf oFormMode = FORM_MODE.FORM_MODE_EDIT Then
                Try
                    fn_Save = oS_DIGITAL_RI_31.UpdateData(ds, arrDetail)
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
    Private Sub btnTambah_Click(sender As Object, e As EventArgs) Handles btnTAMBAH.Click
        Dim frmPopUpEMedrekRI_31 As New frmPopUpEMedrekRI_31
        frmPopUpEMedrekRI_31.fn_LoadMe(Now.ToString("dd/MM/yyyy HH:mm"), "", "", "", "", "", "", "", "", "", "", "", "", "")
        frmPopUpEMedrekRI_31.ShowDialog()

        If sFind1 <> String.Empty Then
            grvDetail.Focus()
            grvDetail.AddNewRow()
            grvDetail.SetFocusedRowCellValue(colDATE, sFind1)
            grvDetail.SetFocusedRowCellValue(colTENSI, sFind2)
            grvDetail.SetFocusedRowCellValue(colNADI, sFind3)
            grvDetail.SetFocusedRowCellValue(colSUHU, sFind4)
            grvDetail.SetFocusedRowCellValue(colRESPIRASI, sFind5)
            grvDetail.SetFocusedRowCellValue(colKES, sFind6)
            grvDetail.SetFocusedRowCellValue(colSPO2, sFind7)
            grvDetail.SetFocusedRowCellValue(colORAL, sFind8)
            grvDetail.SetFocusedRowCellValue(colINFUS, sFind9)
            grvDetail.SetFocusedRowCellValue(colDARAH, sFind10)
            grvDetail.SetFocusedRowCellValue(colURINE, sFind11)
            grvDetail.SetFocusedRowCellValue(colDRAIN, sFind12)
            grvDetail.SetFocusedRowCellValue(colNGT, sFind13)
            grvDetail.SetFocusedRowCellValue(colCATATAN, sFind14)
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
        sFind10 = String.Empty
        sFind11 = String.Empty
        sFind12 = String.Empty
        sFind13 = String.Empty
        sFind14 = String.Empty

        grdDetail.Focus()
    End Sub
    Private Sub EditToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles EditToolStripMenuItem.Click
        Dim frmPopUpEMedrekRI_31 As New frmPopUpEMedrekRI_31
        frmPopUpEMedrekRI_31.fn_LoadMe(grvDetail.GetFocusedRowCellValue(colDATE), grvDetail.GetFocusedRowCellValue(colTENSI), grvDetail.GetFocusedRowCellValue(colNADI), grvDetail.GetFocusedRowCellValue(colSUHU), grvDetail.GetFocusedRowCellValue(colRESPIRASI), grvDetail.GetFocusedRowCellValue(colKES), grvDetail.GetFocusedRowCellValue(colSPO2), grvDetail.GetFocusedRowCellValue(colORAL), grvDetail.GetFocusedRowCellValue(colINFUS), grvDetail.GetFocusedRowCellValue(colDARAH), grvDetail.GetFocusedRowCellValue(colURINE), grvDetail.GetFocusedRowCellValue(colDRAIN), grvDetail.GetFocusedRowCellValue(colNGT), grvDetail.GetFocusedRowCellValue(colCATATAN))
        frmPopUpEMedrekRI_31.ShowDialog()

        If sFind1 <> String.Empty Then
            grvDetail.SetFocusedRowCellValue(colDATE, sFind1)
            grvDetail.SetFocusedRowCellValue(colTENSI, sFind2)
            grvDetail.SetFocusedRowCellValue(colNADI, sFind3)
            grvDetail.SetFocusedRowCellValue(colSUHU, sFind4)
            grvDetail.SetFocusedRowCellValue(colRESPIRASI, sFind5)
            grvDetail.SetFocusedRowCellValue(colKES, sFind6)
            grvDetail.SetFocusedRowCellValue(colSPO2, sFind7)
            grvDetail.SetFocusedRowCellValue(colORAL, sFind8)
            grvDetail.SetFocusedRowCellValue(colINFUS, sFind9)
            grvDetail.SetFocusedRowCellValue(colDARAH, sFind10)
            grvDetail.SetFocusedRowCellValue(colURINE, sFind11)
            grvDetail.SetFocusedRowCellValue(colDRAIN, sFind12)
            grvDetail.SetFocusedRowCellValue(colNGT, sFind13)
            grvDetail.SetFocusedRowCellValue(colCATATAN, sFind14)

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
        sFind10 = String.Empty
        sFind11 = String.Empty
        sFind12 = String.Empty
        sFind13 = String.Empty
        sFind14 = String.Empty

        grdDetail.Focus()
    End Sub

    Private Sub DeleteToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DeleteToolStripMenuItem.Click
        grvDetail.DeleteSelectedRows()
    End Sub
    Private Sub fn_LoadDoctor()
        Try
            Dim oDoctor As New Reference.clsDoctor

            Dim dsDoctor = From x In oDoctor.GetData()
                           Where x.ISACTIVE = True
                           Select x.KDDOCTOR, x.NAME_DISPLAY

            grdKDDOCTOR.Properties.DataSource = dsDoctor.ToList()
            grdKDDOCTOR.Properties.ValueMember = "KDDOCTOR"
            grdKDDOCTOR.Properties.DisplayMember = "NAME_DISPLAY"

            grdKDDOCTOR2.Properties.DataSource = dsDoctor.ToList()
            grdKDDOCTOR2.Properties.ValueMember = "KDDOCTOR"
            grdKDDOCTOR2.Properties.DisplayMember = "NAME_DISPLAY"

            grdKDDOCTOR3.Properties.DataSource = dsDoctor.ToList()
            grdKDDOCTOR3.Properties.ValueMember = "KDDOCTOR"
            grdKDDOCTOR3.Properties.DisplayMember = "NAME_DISPLAY"
        Catch oErr As Exception
            MsgBox("Load Dokter Data : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub frmEMedrekRI_31_MouseWheel(sender As Object, e As MouseEventArgs) Handles MyBase.MouseWheel
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