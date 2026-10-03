Imports DataAccess
Imports UI.WIN.MAIN.My.Resources
Imports System.Data.SqlClient

Public Class frmEMedrekRI_17
#Region "Declaration"
    Private oFormMode As FORM_MODE = FORM_MODE.FORM_MODE_VIEW
    Private isLoad As Boolean = False
    Private oS_DIGITAL_RI_17 As New Digital.clsDigital_RI_17
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

        deDATE.Properties.ReadOnly = Status
        deDATE_MASUK.Properties.ReadOnly = Status
        deDATE_DATA.Properties.ReadOnly = Status
        txtIDENTITAS_01.Properties.ReadOnly = Status
        chkIDENTITAS_02.Properties.ReadOnly = Status
        chkIDENTITAS_03.Properties.ReadOnly = Status
        chkIDENTITAS_04.Properties.ReadOnly = Status
        chkIDENTITAS_05.Properties.ReadOnly = Status
        txtIDENTITAS_06.Properties.ReadOnly = Status
        txtIDENTITAS_07.Properties.ReadOnly = Status
        txtIDENTITAS_08.Properties.ReadOnly = Status
        txtIDENTITAS_09.Properties.ReadOnly = Status
        txtIDENTITAS_10.Properties.ReadOnly = Status
        txtIDENTITAS_11.Properties.ReadOnly = Status
        txtIDENTITAS_12.Properties.ReadOnly = Status
        txtIDENTITAS_13.Properties.ReadOnly = Status
        txtIDENTITAS_14.Properties.ReadOnly = Status
        txtIDENTITAS_15.Properties.ReadOnly = Status
        txtIDENTITAS_16.Properties.ReadOnly = Status
        txtIDENTITAS_17.Properties.ReadOnly = Status
        txtIDENTITAS_18.Properties.ReadOnly = Status
        chkALATBANTU_01.Properties.ReadOnly = Status
        chkALATBANTU_02.Properties.ReadOnly = Status
        chkALATBANTU_03.Properties.ReadOnly = Status
        chkALATBANTU_04.Properties.ReadOnly = Status
        txtKEADAANFISIK_01.Properties.ReadOnly = Status
        txtKEADAANFISIK_02.Properties.ReadOnly = Status
        txtKEADAANFISIK_03.Properties.ReadOnly = Status
        txtKEADAANFISIK_04.Properties.ReadOnly = Status
        chkKEADAANFISIK_05.Properties.ReadOnly = Status
        chkKEADAANFISIK_06.Properties.ReadOnly = Status
        chkKEADAANFISIK_07.Properties.ReadOnly = Status
        chkKEADAANFISIK_08.Properties.ReadOnly = Status
        txtKEADAANFISIK_09.Properties.ReadOnly = Status
        chkKEADAANFISIK_10.Properties.ReadOnly = Status
        chkKEADAANFISIK_11.Properties.ReadOnly = Status
        chkKEADAANFISIK_12.Properties.ReadOnly = Status
        chkKEADAANFISIK_13.Properties.ReadOnly = Status
        chkKEADAANFISIK_14.Properties.ReadOnly = Status
        txtKEADAANFISIK_15.Properties.ReadOnly = Status
        chkKEADAANFISIK_16.Properties.ReadOnly = Status
        chkKEADAANFISIK_17.Properties.ReadOnly = Status
        chkKEADAANFISIK_18.Properties.ReadOnly = Status
        chkKEADAANFISIK_19.Properties.ReadOnly = Status
        chkKEADAANFISIK_20.Properties.ReadOnly = Status
        txtKEADAANFISIK_21.Properties.ReadOnly = Status
        chkKEADAANFISIK_22.Properties.ReadOnly = Status
        txtKEADAANFISIK_23.Properties.ReadOnly = Status
        chkKEADAANFISIK_24.Properties.ReadOnly = Status
        txtKEADAANFISIK_25.Properties.ReadOnly = Status
        chkKEADAANFISIK_26.Properties.ReadOnly = Status
        txtKEADAANFISIK_27.Properties.ReadOnly = Status
        chkKEADAANFISIK_28.Properties.ReadOnly = Status
        txtKEADAANFISIK_29.Properties.ReadOnly = Status
        chkKEADAANFISIK_30.Properties.ReadOnly = Status
        txtKEADAANFISIK_31.Properties.ReadOnly = Status
        chkKEADAANFISIK_32.Properties.ReadOnly = Status
        chkKEADAANFISIK_33.Properties.ReadOnly = Status
        chkKEADAANFISIK_34.Properties.ReadOnly = Status
        chkKEADAANFISIK_35.Properties.ReadOnly = Status
        txtKEADAANFISIK_36.Properties.ReadOnly = Status
        chkKEADAANFISIK_37.Properties.ReadOnly = Status
        chkKEADAANFISIK_38.Properties.ReadOnly = Status
        chkKEADAANFISIK_39.Properties.ReadOnly = Status
        chkKEADAANFISIK_40.Properties.ReadOnly = Status
        txtKEADAANFISIK_41.Properties.ReadOnly = Status
        chkKEADAANFISIK_42.Properties.ReadOnly = Status
        chkKEADAANFISIK_43.Properties.ReadOnly = Status
        chkKEADAANFISIK_44.Properties.ReadOnly = Status
        chkKEADAANFISIK_45.Properties.ReadOnly = Status
        txtKEADAANFISIK_46.Properties.ReadOnly = Status
        chkKEADAANFISIK_47.Properties.ReadOnly = Status
        txtKEADAANFISIK_48.Properties.ReadOnly = Status
        chkKEADAANFISIK_49.Properties.ReadOnly = Status
        txtKEADAANFISIK_50.Properties.ReadOnly = Status
        chkKEADAANFISIK_51.Properties.ReadOnly = Status
        txtKEADAANFISIK_52.Properties.ReadOnly = Status
        chkKEADAANFISIK_53.Properties.ReadOnly = Status
        txtKEADAANFISIK_54.Properties.ReadOnly = Status
        chkKEADAANFISIK_55.Properties.ReadOnly = Status
        chkKEADAANFISIK_56.Properties.ReadOnly = Status
        chkKEADAANFISIK_57.Properties.ReadOnly = Status
        chkKEADAANFISIK_58.Properties.ReadOnly = Status
        chkKEADAANFISIK_59.Properties.ReadOnly = Status
        chkKEADAANFISIK_60.Properties.ReadOnly = Status
        txtKEADAANFISIK_61.Properties.ReadOnly = Status
        chkKEADAANFISIK_62.Properties.ReadOnly = Status
        txtKEADAANFISIK_63.Properties.ReadOnly = Status
        chkKEADAANFISIK_64.Properties.ReadOnly = Status
        chkKEADAANFISIK_65.Properties.ReadOnly = Status
        chkKEADAANFISIK_66.Properties.ReadOnly = Status
        chkKEADAANFISIK_67.Properties.ReadOnly = Status
        chkKEADAANFISIK_68.Properties.ReadOnly = Status
        chkKEADAANFISIK_69.Properties.ReadOnly = Status
        chkKEADAANFISIK_70.Properties.ReadOnly = Status
        chkKEADAANFISIK_71.Properties.ReadOnly = Status
        chkKEADAANFISIK_72.Properties.ReadOnly = Status
        chkKEADAANFISIK_73.Properties.ReadOnly = Status
        txtKEADAANFISIK_74.Properties.ReadOnly = Status
        chkKEADAANFISIK_75.Properties.ReadOnly = Status
        chkKEADAANFISIK_76.Properties.ReadOnly = Status
        chkKEADAANFISIK_77.Properties.ReadOnly = Status
        chkKEADAANFISIK_78.Properties.ReadOnly = Status
        chkKEADAANFISIK_79.Properties.ReadOnly = Status
        chkKEADAANFISIK_80.Properties.ReadOnly = Status
        txtKEADAANFISIK_81.Properties.ReadOnly = Status
        chkJANTUNGPARU_01.Properties.ReadOnly = Status
        txtJANTUNGPARU_02.Properties.ReadOnly = Status
        chkJANTUNGPARU_03.Properties.ReadOnly = Status
        chkJANTUNGPARU_04.Properties.ReadOnly = Status
        chkJANTUNGPARU_05.Properties.ReadOnly = Status
        chkJANTUNGPARU_06.Properties.ReadOnly = Status
        txtJANTUNGPARU_07.Properties.ReadOnly = Status
        chkJANTUNGPARU_08.Properties.ReadOnly = Status
        chkJANTUNGPARU_09.Properties.ReadOnly = Status
        chkJANTUNGPARU_10.Properties.ReadOnly = Status
        chkJANTUNGPARU_11.Properties.ReadOnly = Status
        chkJANTUNGPARU_12.Properties.ReadOnly = Status
        txtJANTUNGPARU_13.Properties.ReadOnly = Status
        chkJANTUNGPARU_14.Properties.ReadOnly = Status
        chkJANTUNGPARU_15.Properties.ReadOnly = Status
        txtJANTUNGPARU_16.Properties.ReadOnly = Status
        chkJANTUNGPARU_17.Properties.ReadOnly = Status
        chkJANTUNGPARU_18.Properties.ReadOnly = Status
        chkJANTUNGPARU_19.Properties.ReadOnly = Status
        chkJANTUNGPARU_20.Properties.ReadOnly = Status
        chkJANTUNGPARU_21.Properties.ReadOnly = Status
        chkJANTUNGPARU_22.Properties.ReadOnly = Status
        chkJANTUNGPARU_23.Properties.ReadOnly = Status
        chkJANTUNGPARU_24.Properties.ReadOnly = Status
        chkAKTIVITAS_01.Properties.ReadOnly = Status
        chkAKTIVITAS_02.Properties.ReadOnly = Status
        txtAKTIVITAS_03.Properties.ReadOnly = Status
        chkAKTIVITAS_04.Properties.ReadOnly = Status
        chkAKTIVITAS_05.Properties.ReadOnly = Status
        txtAKTIVITAS_06.Properties.ReadOnly = Status
        chkAKTIVITAS_07.Properties.ReadOnly = Status
        chkAKTIVITAS_08.Properties.ReadOnly = Status
        txtAKTIVITAS_09.Properties.ReadOnly = Status
        txtAKTIVITAS_10.Properties.ReadOnly = Status
        txtAKTIVITAS_11.Properties.ReadOnly = Status
        txtAKTIVITAS_12.Properties.ReadOnly = Status
        txtAKTIVITAS_13.Properties.ReadOnly = Status
        txtAKTIVITAS_14.Properties.ReadOnly = Status
        txtAKTIVITAS_15.Properties.ReadOnly = Status
        txtAKTIVITAS_16.Properties.ReadOnly = Status
        txtAKTIVITAS_17.Properties.ReadOnly = Status
        txtAKTIVITAS_18.Properties.ReadOnly = Status
        txtAKTIVITAS_19.Properties.ReadOnly = Status
        txtAKTIVITAS_20.Properties.ReadOnly = Status
        txtAKTIVITAS_21.Properties.ReadOnly = Status
        txtAKTIVITAS_22.Properties.ReadOnly = Status
        chkAKTIVITAS_23.Properties.ReadOnly = Status
        chkAKTIVITAS_24.Properties.ReadOnly = Status
        txtAKTIVITAS_25.Properties.ReadOnly = Status
        chkAKTIVITAS_26.Properties.ReadOnly = Status
        chkAKTIVITAS_27.Properties.ReadOnly = Status
        chkAKTIVITAS_28.Properties.ReadOnly = Status
        chkAKTIVITAS_29.Properties.ReadOnly = Status
        txtAKTIVITAS_30.Properties.ReadOnly = Status
        chkRIWAYATKESEHATAN_01.Properties.ReadOnly = Status
        chkRIWAYATKESEHATAN_02.Properties.ReadOnly = Status
        txtRIWAYATKESEHATAN_03.Properties.ReadOnly = Status
        chkRIWAYATKESEHATAN_04.Properties.ReadOnly = Status
        chkRIWAYATKESEHATAN_05.Properties.ReadOnly = Status
        txtRIWAYATKESEHATAN_06.Properties.ReadOnly = Status
        chkRIWAYATPSIKOSOSIAL_01.Properties.ReadOnly = Status
        chkRIWAYATPSIKOSOSIAL_02.Properties.ReadOnly = Status
        chkRIWAYATPSIKOSOSIAL_03.Properties.ReadOnly = Status
        chkRIWAYATPSIKOSOSIAL_04.Properties.ReadOnly = Status
        chkRIWAYATPSIKOSOSIAL_05.Properties.ReadOnly = Status
        chkRIWAYATPSIKOSOSIAL_06.Properties.ReadOnly = Status
        chkRIWAYATPSIKOSOSIAL_07.Properties.ReadOnly = Status
        chkRIWAYATPSIKOSOSIAL_08.Properties.ReadOnly = Status
        txtRIWAYATPSIKOSOSIAL_09.Properties.ReadOnly = Status
        chkRIWAYATPSIKOSOSIAL_10.Properties.ReadOnly = Status
        chkRIWAYATPSIKOSOSIAL_11.Properties.ReadOnly = Status
        chkRIWAYATPSIKOSOSIAL_12.Properties.ReadOnly = Status
        chkRIWAYATPSIKOSOSIAL_13.Properties.ReadOnly = Status
        txtHASILPENUNJANG_01.Properties.ReadOnly = Status
        txtPENGOBATAN_01.Properties.ReadOnly = Status
        chkDISCHARGE_01.Properties.ReadOnly = Status
        chkDISCHARGE_02.Properties.ReadOnly = Status
        txtDISCHARGE_03.Properties.ReadOnly = Status
        chkDISCHARGE_04.Properties.ReadOnly = Status
        chkDISCHARGE_05.Properties.ReadOnly = Status
        txtDISCHARGE_06.Properties.ReadOnly = Status
        chkDISCHARGE_07.Properties.ReadOnly = Status
        chkDISCHARGE_08.Properties.ReadOnly = Status
        txtDISCHARGE_09.Properties.ReadOnly = Status
        chkDISCHARGE_10.Properties.ReadOnly = Status
        chkDISCHARGE_11.Properties.ReadOnly = Status
        txtDISCHARGE_12.Properties.ReadOnly = Status
        chkDISCHARGE_13.Properties.ReadOnly = Status
        chkDISCHARGE_14.Properties.ReadOnly = Status
        txtDISCHARGE_15.Properties.ReadOnly = Status
        chkDISCHARGE_16.Properties.ReadOnly = Status
        chkDISCHARGE_17.Properties.ReadOnly = Status
        txtDISCHARGE_18.Properties.ReadOnly = Status
        chkDISCHARGE_19.Properties.ReadOnly = Status
        chkDISCHARGE_20.Properties.ReadOnly = Status
        txtDISCHARGE_21.Properties.ReadOnly = Status
        chkDISCHARGE_22.Properties.ReadOnly = Status
        chkDISCHARGE_23.Properties.ReadOnly = Status
        txtDISCHARGE_24.Properties.ReadOnly = Status
    End Sub
    Private Sub fn_EmptyMe()
        'txtCODE.Text = "<--- AUTO --->"

        deDATE.DateTime = Now
        deDATE_MASUK.DateTime = Now
        deDATE_DATA.DateTime = Now
        txtIDENTITAS_01.ResetText()
        chkIDENTITAS_02.Checked = False
        chkIDENTITAS_03.Checked = False
        chkIDENTITAS_04.Checked = False
        chkIDENTITAS_05.Checked = False
        txtIDENTITAS_06.ResetText()
        txtIDENTITAS_07.ResetText()
        txtIDENTITAS_08.ResetText()
        txtIDENTITAS_09.ResetText()
        txtIDENTITAS_10.ResetText()
        txtIDENTITAS_11.ResetText()
        txtIDENTITAS_12.ResetText()
        txtIDENTITAS_13.ResetText()
        txtIDENTITAS_14.ResetText()
        txtIDENTITAS_15.ResetText()
        txtIDENTITAS_16.ResetText()
        txtIDENTITAS_17.ResetText()
        txtIDENTITAS_18.ResetText()
        chkALATBANTU_01.Checked = False
        chkALATBANTU_02.Checked = False
        chkALATBANTU_03.Checked = False
        chkALATBANTU_04.Checked = False
        txtKEADAANFISIK_01.ResetText()
        txtKEADAANFISIK_02.ResetText()
        txtKEADAANFISIK_03.ResetText()
        txtKEADAANFISIK_04.ResetText()
        chkKEADAANFISIK_05.Checked = False
        chkKEADAANFISIK_06.Checked = False
        chkKEADAANFISIK_07.Checked = False
        chkKEADAANFISIK_08.Checked = False
        txtKEADAANFISIK_09.ResetText()
        chkKEADAANFISIK_10.Checked = False
        chkKEADAANFISIK_11.Checked = False
        chkKEADAANFISIK_12.Checked = False
        chkKEADAANFISIK_13.Checked = False
        chkKEADAANFISIK_14.Checked = False
        txtKEADAANFISIK_15.ResetText()
        chkKEADAANFISIK_16.Checked = False
        chkKEADAANFISIK_17.Checked = False
        chkKEADAANFISIK_18.Checked = False
        chkKEADAANFISIK_19.Checked = False
        chkKEADAANFISIK_20.Checked = False
        txtKEADAANFISIK_21.ResetText()
        chkKEADAANFISIK_22.Checked = False
        txtKEADAANFISIK_23.ResetText()
        chkKEADAANFISIK_24.Checked = False
        txtKEADAANFISIK_25.ResetText()
        chkKEADAANFISIK_26.Checked = False
        txtKEADAANFISIK_27.ResetText()
        chkKEADAANFISIK_28.Checked = False
        txtKEADAANFISIK_29.ResetText()
        chkKEADAANFISIK_30.Checked = False
        txtKEADAANFISIK_31.ResetText()
        chkKEADAANFISIK_32.Checked = False
        chkKEADAANFISIK_33.Checked = False
        chkKEADAANFISIK_34.Checked = False
        chkKEADAANFISIK_35.Checked = False
        txtKEADAANFISIK_36.ResetText()
        chkKEADAANFISIK_37.Checked = False
        chkKEADAANFISIK_38.Checked = False
        chkKEADAANFISIK_39.Checked = False
        chkKEADAANFISIK_40.Checked = False
        txtKEADAANFISIK_41.ResetText()
        chkKEADAANFISIK_42.Checked = False
        chkKEADAANFISIK_43.Checked = False
        chkKEADAANFISIK_44.Checked = False
        chkKEADAANFISIK_45.Checked = False
        txtKEADAANFISIK_46.ResetText()
        chkKEADAANFISIK_47.Checked = False
        txtKEADAANFISIK_48.ResetText()
        chkKEADAANFISIK_49.Checked = False
        txtKEADAANFISIK_50.ResetText()
        chkKEADAANFISIK_51.Checked = False
        txtKEADAANFISIK_52.ResetText()
        chkKEADAANFISIK_53.Checked = False
        txtKEADAANFISIK_54.ResetText()
        chkKEADAANFISIK_55.Checked = False
        chkKEADAANFISIK_56.Checked = False
        chkKEADAANFISIK_57.Checked = False
        chkKEADAANFISIK_58.Checked = False
        chkKEADAANFISIK_59.Checked = False
        chkKEADAANFISIK_60.Checked = False
        txtKEADAANFISIK_61.ResetText()
        chkKEADAANFISIK_62.Checked = False
        txtKEADAANFISIK_63.ResetText()
        chkKEADAANFISIK_64.Checked = False
        chkKEADAANFISIK_65.Checked = False
        chkKEADAANFISIK_66.Checked = False
        chkKEADAANFISIK_67.Checked = False
        chkKEADAANFISIK_68.Checked = False
        chkKEADAANFISIK_69.Checked = False
        chkKEADAANFISIK_70.Checked = False
        chkKEADAANFISIK_71.Checked = False
        chkKEADAANFISIK_72.Checked = False
        chkKEADAANFISIK_73.Checked = False
        txtKEADAANFISIK_74.ResetText()
        chkKEADAANFISIK_75.Checked = False
        chkKEADAANFISIK_76.Checked = False
        chkKEADAANFISIK_77.Checked = False
        chkKEADAANFISIK_78.Checked = False
        chkKEADAANFISIK_79.Checked = False
        chkKEADAANFISIK_80.Checked = False
        txtKEADAANFISIK_81.ResetText()
        chkJANTUNGPARU_01.Checked = False
        txtJANTUNGPARU_02.ResetText()
        chkJANTUNGPARU_03.Checked = False
        chkJANTUNGPARU_04.Checked = False
        chkJANTUNGPARU_05.Checked = False
        chkJANTUNGPARU_06.Checked = False
        txtJANTUNGPARU_07.ResetText()
        chkJANTUNGPARU_08.Checked = False
        chkJANTUNGPARU_09.Checked = False
        chkJANTUNGPARU_10.Checked = False
        chkJANTUNGPARU_11.Checked = False
        chkJANTUNGPARU_12.Checked = False
        txtJANTUNGPARU_13.ResetText()
        chkJANTUNGPARU_14.Checked = False
        chkJANTUNGPARU_15.Checked = False
        txtJANTUNGPARU_16.ResetText()
        chkJANTUNGPARU_17.Checked = False
        chkJANTUNGPARU_18.Checked = False
        chkJANTUNGPARU_19.Checked = False
        chkJANTUNGPARU_20.Checked = False
        chkJANTUNGPARU_21.Checked = False
        chkJANTUNGPARU_22.Checked = False
        chkJANTUNGPARU_23.Checked = False
        chkJANTUNGPARU_24.Checked = False
        chkAKTIVITAS_01.Checked = False
        chkAKTIVITAS_02.Checked = False
        txtAKTIVITAS_03.ResetText()
        chkAKTIVITAS_04.Checked = False
        chkAKTIVITAS_05.Checked = False
        txtAKTIVITAS_06.ResetText()
        chkAKTIVITAS_07.Checked = False
        chkAKTIVITAS_08.Checked = False
        txtAKTIVITAS_09.ResetText()
        txtAKTIVITAS_10.ResetText()
        txtAKTIVITAS_11.ResetText()
        txtAKTIVITAS_12.ResetText()
        txtAKTIVITAS_13.ResetText()
        txtAKTIVITAS_14.ResetText()
        txtAKTIVITAS_15.ResetText()
        txtAKTIVITAS_16.ResetText()
        txtAKTIVITAS_17.ResetText()
        txtAKTIVITAS_18.ResetText()
        txtAKTIVITAS_19.ResetText()
        txtAKTIVITAS_20.ResetText()
        txtAKTIVITAS_21.ResetText()
        txtAKTIVITAS_22.ResetText()
        chkAKTIVITAS_23.Checked = False
        chkAKTIVITAS_24.Checked = False
        txtAKTIVITAS_25.ResetText()
        chkAKTIVITAS_26.Checked = False
        chkAKTIVITAS_27.Checked = False
        chkAKTIVITAS_28.Checked = False
        chkAKTIVITAS_29.Checked = False
        txtAKTIVITAS_30.ResetText()
        chkRIWAYATKESEHATAN_01.Checked = False
        chkRIWAYATKESEHATAN_02.Checked = False
        txtRIWAYATKESEHATAN_03.ResetText()
        chkRIWAYATKESEHATAN_04.Checked = False
        chkRIWAYATKESEHATAN_05.Checked = False
        txtRIWAYATKESEHATAN_06.ResetText()
        chkRIWAYATPSIKOSOSIAL_01.Checked = False
        chkRIWAYATPSIKOSOSIAL_02.Checked = False
        chkRIWAYATPSIKOSOSIAL_03.Checked = False
        chkRIWAYATPSIKOSOSIAL_04.Checked = False
        chkRIWAYATPSIKOSOSIAL_05.Checked = False
        chkRIWAYATPSIKOSOSIAL_06.Checked = False
        chkRIWAYATPSIKOSOSIAL_07.Checked = False
        chkRIWAYATPSIKOSOSIAL_08.Checked = False
        txtRIWAYATPSIKOSOSIAL_09.ResetText()
        chkRIWAYATPSIKOSOSIAL_10.Checked = False
        chkRIWAYATPSIKOSOSIAL_11.Checked = False
        chkRIWAYATPSIKOSOSIAL_12.Checked = False
        chkRIWAYATPSIKOSOSIAL_13.Checked = False
        txtHASILPENUNJANG_01.ResetText()
        txtPENGOBATAN_01.ResetText()
        chkDISCHARGE_01.Checked = False
        chkDISCHARGE_02.Checked = False
        txtDISCHARGE_03.ResetText()
        chkDISCHARGE_04.Checked = False
        chkDISCHARGE_05.Checked = False
        txtDISCHARGE_06.ResetText()
        chkDISCHARGE_07.Checked = False
        chkDISCHARGE_08.Checked = False
        txtDISCHARGE_09.ResetText()
        chkDISCHARGE_10.Checked = False
        chkDISCHARGE_11.Checked = False
        txtDISCHARGE_12.ResetText()
        chkDISCHARGE_13.Checked = False
        chkDISCHARGE_14.Checked = False
        txtDISCHARGE_15.ResetText()
        chkDISCHARGE_16.Checked = False
        chkDISCHARGE_17.Checked = False
        txtDISCHARGE_18.ResetText()
        chkDISCHARGE_19.Checked = False
        chkDISCHARGE_20.Checked = False
        txtDISCHARGE_21.ResetText()
        chkDISCHARGE_22.Checked = False
        chkDISCHARGE_23.Checked = False
        txtDISCHARGE_24.ResetText()
        'fn_LoadAsessmenRawatJalan(txtNoRegister.Text)

    End Sub
    Private Sub fn_LoadData()
        Try
            ' ***** HEADER *****
            Dim ds = oS_DIGITAL_RI_17.GetData(txtNoRegister.Text)

            With ds
                deDATE.DateTime = .DATE
                deDATE_MASUK.DateTime = .DATE_MASUK
                deDATE_DATA.DateTime = .DATE_DATA
                txtIDENTITAS_01.Text = .IDENTITAS_01
                chkIDENTITAS_02.Checked = .IDENTITAS_02
                chkIDENTITAS_03.Checked = .IDENTITAS_03
                chkIDENTITAS_04.Checked = .IDENTITAS_04
                chkIDENTITAS_05.Checked = .IDENTITAS_05
                txtIDENTITAS_06.Text = .IDENTITAS_06
                txtIDENTITAS_07.Text = .IDENTITAS_07
                txtIDENTITAS_08.Text = .IDENTITAS_08
                txtIDENTITAS_09.Text = .IDENTITAS_09
                txtIDENTITAS_10.Text = .IDENTITAS_10
                txtIDENTITAS_11.Text = .IDENTITAS_11
                txtIDENTITAS_12.Text = .IDENTITAS_12
                txtIDENTITAS_13.Text = .IDENTITAS_13
                txtIDENTITAS_14.Text = .IDENTITAS_14
                txtIDENTITAS_15.Text = .IDENTITAS_15
                txtIDENTITAS_16.Text = .IDENTITAS_16
                txtIDENTITAS_17.Text = .IDENTITAS_17
                txtIDENTITAS_18.Text = .IDENTITAS_18
                chkALATBANTU_01.Checked = .ALATBANTU_01
                chkALATBANTU_02.Checked = .ALATBANTU_02
                chkALATBANTU_03.Checked = .ALATBANTU_03
                chkALATBANTU_04.Checked = .ALATBANTU_04
                txtKEADAANFISIK_01.Text = .KEADAANFISIK_01
                txtKEADAANFISIK_02.Text = .KEADAANFISIK_02
                txtKEADAANFISIK_03.Text = .KEADAANFISIK_03
                txtKEADAANFISIK_04.Text = .KEADAANFISIK_04
                chkKEADAANFISIK_05.Checked = .KEADAANFISIK_05
                chkKEADAANFISIK_06.Checked = .KEADAANFISIK_06
                chkKEADAANFISIK_07.Checked = .KEADAANFISIK_07
                chkKEADAANFISIK_08.Checked = .KEADAANFISIK_08
                txtKEADAANFISIK_09.Text = .KEADAANFISIK_09
                chkKEADAANFISIK_10.Checked = .KEADAANFISIK_10
                chkKEADAANFISIK_11.Checked = .KEADAANFISIK_11
                chkKEADAANFISIK_12.Checked = .KEADAANFISIK_12
                chkKEADAANFISIK_13.Checked = .KEADAANFISIK_13
                chkKEADAANFISIK_14.Checked = .KEADAANFISIK_14
                txtKEADAANFISIK_15.Text = .KEADAANFISIK_15
                chkKEADAANFISIK_16.Checked = .KEADAANFISIK_16
                chkKEADAANFISIK_17.Checked = .KEADAANFISIK_17
                chkKEADAANFISIK_18.Checked = .KEADAANFISIK_18
                chkKEADAANFISIK_19.Checked = .KEADAANFISIK_19
                chkKEADAANFISIK_20.Checked = .KEADAANFISIK_20
                txtKEADAANFISIK_21.Text = .KEADAANFISIK_21
                chkKEADAANFISIK_22.Checked = .KEADAANFISIK_22
                txtKEADAANFISIK_23.Text = .KEADAANFISIK_23
                chkKEADAANFISIK_24.Checked = .KEADAANFISIK_24
                txtKEADAANFISIK_25.Text = .KEADAANFISIK_25
                chkKEADAANFISIK_26.Checked = .KEADAANFISIK_26
                txtKEADAANFISIK_27.Text = .KEADAANFISIK_27
                chkKEADAANFISIK_28.Checked = .KEADAANFISIK_28
                txtKEADAANFISIK_29.Text = .KEADAANFISIK_29
                chkKEADAANFISIK_30.Checked = .KEADAANFISIK_30
                txtKEADAANFISIK_31.Text = .KEADAANFISIK_31
                chkKEADAANFISIK_32.Checked = .KEADAANFISIK_32
                chkKEADAANFISIK_33.Checked = .KEADAANFISIK_33
                chkKEADAANFISIK_34.Checked = .KEADAANFISIK_34
                chkKEADAANFISIK_35.Checked = .KEADAANFISIK_35
                txtKEADAANFISIK_36.Text = .KEADAANFISIK_36
                chkKEADAANFISIK_37.Checked = .KEADAANFISIK_37
                chkKEADAANFISIK_38.Checked = .KEADAANFISIK_38
                chkKEADAANFISIK_39.Checked = .KEADAANFISIK_39
                chkKEADAANFISIK_40.Checked = .KEADAANFISIK_40
                txtKEADAANFISIK_41.Text = .KEADAANFISIK_41
                chkKEADAANFISIK_42.Checked = .KEADAANFISIK_42
                chkKEADAANFISIK_43.Checked = .KEADAANFISIK_43
                chkKEADAANFISIK_44.Checked = .KEADAANFISIK_44
                chkKEADAANFISIK_45.Checked = .KEADAANFISIK_45
                txtKEADAANFISIK_46.Text = .KEADAANFISIK_46
                chkKEADAANFISIK_47.Checked = .KEADAANFISIK_47
                txtKEADAANFISIK_48.Text = .KEADAANFISIK_48
                chkKEADAANFISIK_49.Checked = .KEADAANFISIK_49
                txtKEADAANFISIK_50.Text = .KEADAANFISIK_50
                chkKEADAANFISIK_51.Checked = .KEADAANFISIK_51
                txtKEADAANFISIK_52.Text = .KEADAANFISIK_52
                chkKEADAANFISIK_53.Checked = .KEADAANFISIK_53
                txtKEADAANFISIK_54.Text = .KEADAANFISIK_54
                chkKEADAANFISIK_55.Checked = .KEADAANFISIK_55
                chkKEADAANFISIK_56.Checked = .KEADAANFISIK_56
                chkKEADAANFISIK_57.Checked = .KEADAANFISIK_57
                chkKEADAANFISIK_58.Checked = .KEADAANFISIK_58
                chkKEADAANFISIK_59.Checked = .KEADAANFISIK_59
                chkKEADAANFISIK_60.Checked = .KEADAANFISIK_60
                txtKEADAANFISIK_61.Text = .KEADAANFISIK_61
                chkKEADAANFISIK_62.Checked = .KEADAANFISIK_62
                txtKEADAANFISIK_63.Text = .KEADAANFISIK_63
                chkKEADAANFISIK_64.Checked = .KEADAANFISIK_64
                chkKEADAANFISIK_65.Checked = .KEADAANFISIK_65
                chkKEADAANFISIK_66.Checked = .KEADAANFISIK_66
                chkKEADAANFISIK_67.Checked = .KEADAANFISIK_67
                chkKEADAANFISIK_68.Checked = .KEADAANFISIK_68
                chkKEADAANFISIK_69.Checked = .KEADAANFISIK_69
                chkKEADAANFISIK_70.Checked = .KEADAANFISIK_70
                chkKEADAANFISIK_71.Checked = .KEADAANFISIK_71
                chkKEADAANFISIK_72.Checked = .KEADAANFISIK_72
                chkKEADAANFISIK_73.Checked = .KEADAANFISIK_73
                txtKEADAANFISIK_74.Text = .KEADAANFISIK_74
                chkKEADAANFISIK_75.Checked = .KEADAANFISIK_75
                chkKEADAANFISIK_76.Checked = .KEADAANFISIK_76
                chkKEADAANFISIK_77.Checked = .KEADAANFISIK_77
                chkKEADAANFISIK_78.Checked = .KEADAANFISIK_78
                chkKEADAANFISIK_79.Checked = .KEADAANFISIK_79
                chkKEADAANFISIK_80.Checked = .KEADAANFISIK_80
                txtKEADAANFISIK_81.Text = .KEADAANFISIK_81
                chkJANTUNGPARU_01.Checked = .JANTUNGPARU_01
                txtJANTUNGPARU_02.Text = .JANTUNGPARU_02
                chkJANTUNGPARU_03.Checked = .JANTUNGPARU_03
                chkJANTUNGPARU_04.Checked = .JANTUNGPARU_04
                chkJANTUNGPARU_05.Checked = .JANTUNGPARU_05
                chkJANTUNGPARU_06.Checked = .JANTUNGPARU_06
                txtJANTUNGPARU_07.Text = .JANTUNGPARU_07
                chkJANTUNGPARU_08.Checked = .JANTUNGPARU_08
                chkJANTUNGPARU_09.Checked = .JANTUNGPARU_09
                chkJANTUNGPARU_10.Checked = .JANTUNGPARU_10
                chkJANTUNGPARU_11.Checked = .JANTUNGPARU_11
                chkJANTUNGPARU_12.Checked = .JANTUNGPARU_12
                txtJANTUNGPARU_13.Text = .JANTUNGPARU_13
                chkJANTUNGPARU_14.Checked = .JANTUNGPARU_14
                chkJANTUNGPARU_15.Checked = .JANTUNGPARU_15
                txtJANTUNGPARU_16.Text = .JANTUNGPARU_16
                chkJANTUNGPARU_17.Checked = .JANTUNGPARU_17
                chkJANTUNGPARU_18.Checked = .JANTUNGPARU_18
                chkJANTUNGPARU_19.Checked = .JANTUNGPARU_19
                chkJANTUNGPARU_20.Checked = .JANTUNGPARU_20
                chkJANTUNGPARU_21.Checked = .JANTUNGPARU_21
                chkJANTUNGPARU_22.Checked = .JANTUNGPARU_22
                chkJANTUNGPARU_23.Checked = .JANTUNGPARU_23
                chkJANTUNGPARU_24.Checked = .JANTUNGPARU_24
                chkAKTIVITAS_01.Checked = .AKTIVITAS_01
                chkAKTIVITAS_02.Checked = .AKTIVITAS_02
                txtAKTIVITAS_03.Text = .AKTIVITAS_03
                chkAKTIVITAS_04.Checked = .AKTIVITAS_04
                chkAKTIVITAS_05.Checked = .AKTIVITAS_05
                txtAKTIVITAS_06.Text = .AKTIVITAS_06
                chkAKTIVITAS_07.Checked = .AKTIVITAS_07
                chkAKTIVITAS_08.Checked = .AKTIVITAS_08
                txtAKTIVITAS_09.Text = .AKTIVITAS_09
                txtAKTIVITAS_10.Text = .AKTIVITAS_10
                txtAKTIVITAS_11.Text = .AKTIVITAS_11
                txtAKTIVITAS_12.Text = .AKTIVITAS_12
                txtAKTIVITAS_13.Text = .AKTIVITAS_13
                txtAKTIVITAS_14.Text = .AKTIVITAS_14
                txtAKTIVITAS_15.Text = .AKTIVITAS_15
                txtAKTIVITAS_16.Text = .AKTIVITAS_16
                txtAKTIVITAS_17.Text = .AKTIVITAS_17
                txtAKTIVITAS_18.Text = .AKTIVITAS_18
                txtAKTIVITAS_19.Text = .AKTIVITAS_19
                txtAKTIVITAS_20.Text = .AKTIVITAS_20
                txtAKTIVITAS_21.Text = .AKTIVITAS_21
                txtAKTIVITAS_22.Text = .AKTIVITAS_22
                chkAKTIVITAS_23.Checked = .AKTIVITAS_23
                chkAKTIVITAS_24.Checked = .AKTIVITAS_24
                txtAKTIVITAS_25.Text = .AKTIVITAS_25
                chkAKTIVITAS_26.Checked = .AKTIVITAS_26
                chkAKTIVITAS_27.Checked = .AKTIVITAS_27
                chkAKTIVITAS_28.Checked = .AKTIVITAS_28
                chkAKTIVITAS_29.Checked = .AKTIVITAS_29
                txtAKTIVITAS_30.Text = .AKTIVITAS_30
                chkRIWAYATKESEHATAN_01.Checked = .RIWAYATKESEHATAN_01
                chkRIWAYATKESEHATAN_02.Checked = .RIWAYATKESEHATAN_02
                txtRIWAYATKESEHATAN_03.Text = .RIWAYATKESEHATAN_03
                chkRIWAYATKESEHATAN_04.Checked = .RIWAYATKESEHATAN_04
                chkRIWAYATKESEHATAN_05.Checked = .RIWAYATKESEHATAN_05
                txtRIWAYATKESEHATAN_06.Text = .RIWAYATKESEHATAN_06
                chkRIWAYATPSIKOSOSIAL_01.Checked = .RIWAYATPSIKOSOSIAL_01
                chkRIWAYATPSIKOSOSIAL_02.Checked = .RIWAYATPSIKOSOSIAL_02
                chkRIWAYATPSIKOSOSIAL_03.Checked = .RIWAYATPSIKOSOSIAL_03
                chkRIWAYATPSIKOSOSIAL_04.Checked = .RIWAYATPSIKOSOSIAL_04
                chkRIWAYATPSIKOSOSIAL_05.Checked = .RIWAYATPSIKOSOSIAL_05
                chkRIWAYATPSIKOSOSIAL_06.Checked = .RIWAYATPSIKOSOSIAL_06
                chkRIWAYATPSIKOSOSIAL_07.Checked = .RIWAYATPSIKOSOSIAL_07
                chkRIWAYATPSIKOSOSIAL_08.Checked = .RIWAYATPSIKOSOSIAL_08
                txtRIWAYATPSIKOSOSIAL_09.Text = .RIWAYATPSIKOSOSIAL_09
                chkRIWAYATPSIKOSOSIAL_10.Checked = .RIWAYATPSIKOSOSIAL_10
                chkRIWAYATPSIKOSOSIAL_11.Checked = .RIWAYATPSIKOSOSIAL_11
                chkRIWAYATPSIKOSOSIAL_12.Checked = .RIWAYATPSIKOSOSIAL_12
                chkRIWAYATPSIKOSOSIAL_13.Checked = .RIWAYATPSIKOSOSIAL_13
                txtHASILPENUNJANG_01.Text = .HASILPENUNJANG_01
                txtPENGOBATAN_01.Text = .PENGOBATAN_01
                chkDISCHARGE_01.Checked = .DISCHARGE_01
                chkDISCHARGE_02.Checked = .DISCHARGE_02
                txtDISCHARGE_03.Text = .DISCHARGE_03
                chkDISCHARGE_04.Checked = .DISCHARGE_04
                chkDISCHARGE_05.Checked = .DISCHARGE_05
                txtDISCHARGE_06.Text = .DISCHARGE_06
                chkDISCHARGE_07.Checked = .DISCHARGE_07
                chkDISCHARGE_08.Checked = .DISCHARGE_08
                txtDISCHARGE_09.Text = .DISCHARGE_09
                chkDISCHARGE_10.Checked = .DISCHARGE_10
                chkDISCHARGE_11.Checked = .DISCHARGE_11
                txtDISCHARGE_12.Text = .DISCHARGE_12
                chkDISCHARGE_13.Checked = .DISCHARGE_13
                chkDISCHARGE_14.Checked = .DISCHARGE_14
                txtDISCHARGE_15.Text = .DISCHARGE_15
                chkDISCHARGE_16.Checked = .DISCHARGE_16
                chkDISCHARGE_17.Checked = .DISCHARGE_17
                txtDISCHARGE_18.Text = .DISCHARGE_18
                chkDISCHARGE_19.Checked = .DISCHARGE_19
                chkDISCHARGE_20.Checked = .DISCHARGE_20
                txtDISCHARGE_21.Text = .DISCHARGE_21
                chkDISCHARGE_22.Checked = .DISCHARGE_22
                chkDISCHARGE_23.Checked = .DISCHARGE_23
                txtDISCHARGE_24.Text = .DISCHARGE_24

            End With
        Catch oErr As Exception
            MsgBox("Load List Data : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    'Private Sub fn_LoadAsessmenRawatJalan(ByVal Parameter As String)
    '    Dim oAsessmenRawatJalan As New EMedrek.clsS_DIGITAL_RJ_08
    '    Dim dsAsessmenRawatJalan = oAsessmenRawatJalan.GetData(Parameter)

    '    If dsAsessmenRawatJalan IsNot Nothing Then
    '        'txtSOAP_O.Text = "Tensi : " & dsAsessmenRawatJalan.TANDA_VITAL_01 & " mmHg" & vbCrLf & vbCrLf & "Nadi : " & dsAsessmenRawatJalan.TANDA_VITAL_02 & " x/m" & vbCrLf & vbCrLf & "Respirasi : " & dsAsessmenRawatJalan.TANDA_VITAL_04 & " x/m" & vbCrLf & vbCrLf & "Suhu : " & dsAsessmenRawatJalan.TANDA_VITAL_03 & " °C" & vbCrLf & vbCrLf & "Berat Badan : " & dsAsessmenRawatJalan.TANDA_VITAL_05 & " Kg" & vbCrLf & vbCrLf & "Tinggi Badan : " & dsAsessmenRawatJalan.TANDA_VITAL_06 & " Cm"
    '        txtIDENTITAS_09.Text = dsAsessmenRawatJalan.KELUHAN_UTAMA
    '        txtIDENTITAS_12.Text = dsAsessmenRawatJalan.TANDA_VITAL_01
    '        txtIDENTITAS_11.text = dsAsessmenRawatJalan.TANDA_VITAL_02
    '        txtIDENTITAS_10.text = dsAsessmenRawatJalan.TANDA_VITAL_03
    '        txtIDENTITAS_13.text = dsAsessmenRawatJalan.TANDA_VITAL_04
    '        txtIDENTITAS_14.Text = dsAsessmenRawatJalan.KESADARAN_UMUM
    '        txtIDENTITAS_15.Text = dsAsessmenRawatJalan.TANDA_VITAL_06
    '        txtIDENTITAS_16.Text = dsAsessmenRawatJalan.TANDA_VITAL_05
    '        txtIDENTITAS_17.Text = dsAsessmenRawatJalan.S_PENDAFTARAN_H.MASTER_PASIEN.kodedarah.Trim
    '        txtIDENTITAS_18.Text = dsAsessmenRawatJalan.ALERGI_02_TEXT

    '    Else
    '        MsgBox("Assemen Awal Keperawatan rawat jalan belum di input", MsgBoxStyle.Exclamation, Me.Text)
    '    End If
    'End Sub
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

    Private Function chk_val(ByVal chkbox As Boolean) As String
        If chkbox = True Then
            Return "1"
        Else
            Return "0"
        End If
    End Function

    Private Function fn_Save() As Boolean
        Try
            ' ***** HEADER *****
            Dim ds = oS_DIGITAL_RI_17.GetStructureHeader
            With ds
                .KDPENDAFTARAN = txtNoRegister.Text
                .KDCUSTOMER = txtNoPasien.Text
                Try
                    .DATECREATED = oS_DIGITAL_RI_17.GetData(txtNoRegister.Text).DATECREATED
                Catch ex As Exception
                    .DATECREATED = Now
                End Try
                .DATEUPDATED = Now

                .DATE = deDATE.DateTime
                .DOKTER2_KODE = ""
                .DOKTER2_NAMEDISPLAY = txtDPJP.Text

                .DATE_MASUK = deDATE_MASUK.DateTime
                .DATE_DATA = deDATE_DATA.DateTime
                .IDENTITAS_01 = txtIDENTITAS_01.Text
                .IDENTITAS_02 = chk_val(chkIDENTITAS_02.Checked)
                .IDENTITAS_03 = chk_val(chkIDENTITAS_03.Checked)
                .IDENTITAS_04 = chk_val(chkIDENTITAS_04.Checked)
                .IDENTITAS_05 = chk_val(chkIDENTITAS_05.Checked)
                .IDENTITAS_06 = txtIDENTITAS_06.Text
                .IDENTITAS_07 = txtIDENTITAS_07.Text
                .IDENTITAS_08 = txtIDENTITAS_08.Text
                .IDENTITAS_09 = txtIDENTITAS_09.Text
                .IDENTITAS_10 = txtIDENTITAS_10.Text
                .IDENTITAS_11 = txtIDENTITAS_11.Text
                .IDENTITAS_12 = txtIDENTITAS_12.Text
                .IDENTITAS_13 = txtIDENTITAS_13.Text
                .IDENTITAS_14 = txtIDENTITAS_14.Text
                .IDENTITAS_15 = txtIDENTITAS_15.Text
                .IDENTITAS_16 = txtIDENTITAS_16.Text
                .IDENTITAS_17 = txtIDENTITAS_17.Text
                .IDENTITAS_18 = txtIDENTITAS_18.Text
                .ALATBANTU_01 = chk_val(chkALATBANTU_01.Checked)
                .ALATBANTU_02 = chk_val(chkALATBANTU_02.Checked)
                .ALATBANTU_03 = chk_val(chkALATBANTU_03.Checked)
                .ALATBANTU_04 = chk_val(chkALATBANTU_04.Checked)
                .KEADAANFISIK_01 = txtKEADAANFISIK_01.Text
                .KEADAANFISIK_02 = txtKEADAANFISIK_02.Text
                .KEADAANFISIK_03 = txtKEADAANFISIK_03.Text
                .KEADAANFISIK_04 = txtKEADAANFISIK_04.Text
                .KEADAANFISIK_05 = chk_val(chkKEADAANFISIK_05.Checked)
                .KEADAANFISIK_06 = chk_val(chkKEADAANFISIK_06.Checked)
                .KEADAANFISIK_07 = chk_val(chkKEADAANFISIK_07.Checked)
                .KEADAANFISIK_08 = chk_val(chkKEADAANFISIK_08.Checked)
                .KEADAANFISIK_09 = txtKEADAANFISIK_09.Text
                .KEADAANFISIK_10 = chk_val(chkKEADAANFISIK_10.Checked)
                .KEADAANFISIK_11 = chk_val(chkKEADAANFISIK_11.Checked)
                .KEADAANFISIK_12 = chk_val(chkKEADAANFISIK_12.Checked)
                .KEADAANFISIK_13 = chk_val(chkKEADAANFISIK_13.Checked)
                .KEADAANFISIK_14 = chk_val(chkKEADAANFISIK_14.Checked)
                .KEADAANFISIK_15 = txtKEADAANFISIK_15.Text
                .KEADAANFISIK_16 = chk_val(chkKEADAANFISIK_16.Checked)
                .KEADAANFISIK_17 = chk_val(chkKEADAANFISIK_17.Checked)
                .KEADAANFISIK_18 = chk_val(chkKEADAANFISIK_18.Checked)
                .KEADAANFISIK_19 = chk_val(chkKEADAANFISIK_19.Checked)
                .KEADAANFISIK_20 = chk_val(chkKEADAANFISIK_20.Checked)
                .KEADAANFISIK_21 = txtKEADAANFISIK_21.Text
                .KEADAANFISIK_22 = chk_val(chkKEADAANFISIK_22.Checked)
                .KEADAANFISIK_23 = txtKEADAANFISIK_23.Text
                .KEADAANFISIK_24 = chk_val(chkKEADAANFISIK_24.Checked)
                .KEADAANFISIK_25 = txtKEADAANFISIK_25.Text
                .KEADAANFISIK_26 = chk_val(chkKEADAANFISIK_26.Checked)
                .KEADAANFISIK_27 = txtKEADAANFISIK_27.Text
                .KEADAANFISIK_28 = chk_val(chkKEADAANFISIK_28.Checked)
                .KEADAANFISIK_29 = txtKEADAANFISIK_29.Text
                .KEADAANFISIK_30 = chk_val(chkKEADAANFISIK_30.Checked)
                .KEADAANFISIK_31 = txtKEADAANFISIK_31.Text
                .KEADAANFISIK_32 = chk_val(chkKEADAANFISIK_32.Checked)
                .KEADAANFISIK_33 = chk_val(chkKEADAANFISIK_33.Checked)
                .KEADAANFISIK_34 = chk_val(chkKEADAANFISIK_34.Checked)
                .KEADAANFISIK_35 = chk_val(chkKEADAANFISIK_35.Checked)
                .KEADAANFISIK_36 = txtKEADAANFISIK_36.Text
                .KEADAANFISIK_37 = chk_val(chkKEADAANFISIK_37.Checked)
                .KEADAANFISIK_38 = chk_val(chkKEADAANFISIK_38.Checked)
                .KEADAANFISIK_39 = chk_val(chkKEADAANFISIK_39.Checked)
                .KEADAANFISIK_40 = chk_val(chkKEADAANFISIK_40.Checked)
                .KEADAANFISIK_41 = txtKEADAANFISIK_41.Text
                .KEADAANFISIK_42 = chk_val(chkKEADAANFISIK_42.Checked)
                .KEADAANFISIK_43 = chk_val(chkKEADAANFISIK_43.Checked)
                .KEADAANFISIK_44 = chk_val(chkKEADAANFISIK_44.Checked)
                .KEADAANFISIK_45 = chk_val(chkKEADAANFISIK_45.Checked)
                .KEADAANFISIK_46 = txtKEADAANFISIK_46.Text
                .KEADAANFISIK_47 = chk_val(chkKEADAANFISIK_47.Checked)
                .KEADAANFISIK_48 = txtKEADAANFISIK_48.Text
                .KEADAANFISIK_49 = chk_val(chkKEADAANFISIK_49.Checked)
                .KEADAANFISIK_50 = txtKEADAANFISIK_50.Text
                .KEADAANFISIK_51 = chk_val(chkKEADAANFISIK_51.Checked)
                .KEADAANFISIK_52 = txtKEADAANFISIK_52.Text
                .KEADAANFISIK_53 = chk_val(chkKEADAANFISIK_53.Checked)
                .KEADAANFISIK_54 = txtKEADAANFISIK_54.Text
                .KEADAANFISIK_55 = chk_val(chkKEADAANFISIK_55.Checked)
                .KEADAANFISIK_56 = chk_val(chkKEADAANFISIK_56.Checked)
                .KEADAANFISIK_57 = chk_val(chkKEADAANFISIK_57.Checked)
                .KEADAANFISIK_58 = chk_val(chkKEADAANFISIK_58.Checked)
                .KEADAANFISIK_59 = chk_val(chkKEADAANFISIK_59.Checked)
                .KEADAANFISIK_60 = chk_val(chkKEADAANFISIK_60.Checked)
                .KEADAANFISIK_61 = txtKEADAANFISIK_61.Text
                .KEADAANFISIK_62 = chk_val(chkKEADAANFISIK_62.Checked)
                .KEADAANFISIK_63 = txtKEADAANFISIK_63.Text
                .KEADAANFISIK_64 = chk_val(chkKEADAANFISIK_64.Checked)
                .KEADAANFISIK_65 = chk_val(chkKEADAANFISIK_65.Checked)
                .KEADAANFISIK_66 = chk_val(chkKEADAANFISIK_66.Checked)
                .KEADAANFISIK_67 = chk_val(chkKEADAANFISIK_67.Checked)
                .KEADAANFISIK_68 = chk_val(chkKEADAANFISIK_68.Checked)
                .KEADAANFISIK_69 = chk_val(chkKEADAANFISIK_69.Checked)
                .KEADAANFISIK_70 = chk_val(chkKEADAANFISIK_70.Checked)
                .KEADAANFISIK_71 = chk_val(chkKEADAANFISIK_71.Checked)
                .KEADAANFISIK_72 = chk_val(chkKEADAANFISIK_72.Checked)
                .KEADAANFISIK_73 = chk_val(chkKEADAANFISIK_73.Checked)
                .KEADAANFISIK_74 = txtKEADAANFISIK_74.Text
                .KEADAANFISIK_75 = chk_val(chkKEADAANFISIK_75.Checked)
                .KEADAANFISIK_76 = chk_val(chkKEADAANFISIK_76.Checked)
                .KEADAANFISIK_77 = chk_val(chkKEADAANFISIK_77.Checked)
                .KEADAANFISIK_78 = chk_val(chkKEADAANFISIK_78.Checked)
                .KEADAANFISIK_79 = chk_val(chkKEADAANFISIK_79.Checked)
                .KEADAANFISIK_80 = chk_val(chkKEADAANFISIK_80.Checked)
                .KEADAANFISIK_81 = txtKEADAANFISIK_81.Text
                .JANTUNGPARU_01 = chk_val(chkJANTUNGPARU_01.Checked)
                .JANTUNGPARU_02 = txtJANTUNGPARU_02.Text
                .JANTUNGPARU_03 = chk_val(chkJANTUNGPARU_03.Checked)
                .JANTUNGPARU_04 = chk_val(chkJANTUNGPARU_04.Checked)
                .JANTUNGPARU_05 = chk_val(chkJANTUNGPARU_05.Checked)
                .JANTUNGPARU_06 = chk_val(chkJANTUNGPARU_06.Checked)
                .JANTUNGPARU_07 = txtJANTUNGPARU_07.Text
                .JANTUNGPARU_08 = chk_val(chkJANTUNGPARU_08.Checked)
                .JANTUNGPARU_09 = chk_val(chkJANTUNGPARU_09.Checked)
                .JANTUNGPARU_10 = chk_val(chkJANTUNGPARU_10.Checked)
                .JANTUNGPARU_11 = chk_val(chkJANTUNGPARU_11.Checked)
                .JANTUNGPARU_12 = chk_val(chkJANTUNGPARU_12.Checked)
                .JANTUNGPARU_13 = txtJANTUNGPARU_13.Text
                .JANTUNGPARU_14 = chk_val(chkJANTUNGPARU_14.Checked)
                .JANTUNGPARU_15 = chk_val(chkJANTUNGPARU_15.Checked)
                .JANTUNGPARU_16 = txtJANTUNGPARU_16.Text
                .JANTUNGPARU_17 = chk_val(chkJANTUNGPARU_17.Checked)
                .JANTUNGPARU_18 = chk_val(chkJANTUNGPARU_18.Checked)
                .JANTUNGPARU_19 = chk_val(chkJANTUNGPARU_19.Checked)
                .JANTUNGPARU_20 = chk_val(chkJANTUNGPARU_20.Checked)
                .JANTUNGPARU_21 = chk_val(chkJANTUNGPARU_21.Checked)
                .JANTUNGPARU_22 = chk_val(chkJANTUNGPARU_22.Checked)
                .JANTUNGPARU_23 = chk_val(chkJANTUNGPARU_23.Checked)
                .JANTUNGPARU_24 = chk_val(chkJANTUNGPARU_24.Checked)
                .AKTIVITAS_01 = chk_val(chkAKTIVITAS_01.Checked)
                .AKTIVITAS_02 = chk_val(chkAKTIVITAS_02.Checked)
                .AKTIVITAS_03 = txtAKTIVITAS_03.Text
                .AKTIVITAS_04 = chk_val(chkAKTIVITAS_04.Checked)
                .AKTIVITAS_05 = chk_val(chkAKTIVITAS_05.Checked)
                .AKTIVITAS_06 = txtAKTIVITAS_06.Text
                .AKTIVITAS_07 = chk_val(chkAKTIVITAS_07.Checked)
                .AKTIVITAS_08 = chk_val(chkAKTIVITAS_08.Checked)
                .AKTIVITAS_09 = txtAKTIVITAS_09.Text
                .AKTIVITAS_10 = txtAKTIVITAS_10.Text
                .AKTIVITAS_11 = txtAKTIVITAS_11.Text
                .AKTIVITAS_12 = txtAKTIVITAS_12.Text
                .AKTIVITAS_13 = txtAKTIVITAS_13.Text
                .AKTIVITAS_14 = txtAKTIVITAS_14.Text
                .AKTIVITAS_15 = txtAKTIVITAS_15.Text
                .AKTIVITAS_16 = txtAKTIVITAS_16.Text
                .AKTIVITAS_17 = txtAKTIVITAS_17.Text
                .AKTIVITAS_18 = txtAKTIVITAS_18.Text
                .AKTIVITAS_19 = txtAKTIVITAS_19.Text
                .AKTIVITAS_20 = txtAKTIVITAS_20.Text
                .AKTIVITAS_21 = txtAKTIVITAS_21.Text
                .AKTIVITAS_22 = txtAKTIVITAS_22.Text
                .AKTIVITAS_23 = chk_val(chkAKTIVITAS_23.Checked)
                .AKTIVITAS_24 = chk_val(chkAKTIVITAS_24.Checked)
                .AKTIVITAS_25 = txtAKTIVITAS_25.Text
                .AKTIVITAS_26 = chk_val(chkAKTIVITAS_26.Checked)
                .AKTIVITAS_27 = chk_val(chkAKTIVITAS_27.Checked)
                .AKTIVITAS_28 = chk_val(chkAKTIVITAS_28.Checked)
                .AKTIVITAS_29 = chk_val(chkAKTIVITAS_29.Checked)
                .AKTIVITAS_30 = txtAKTIVITAS_30.Text
                .RIWAYATKESEHATAN_01 = chk_val(chkRIWAYATKESEHATAN_01.Checked)
                .RIWAYATKESEHATAN_02 = chk_val(chkRIWAYATKESEHATAN_02.Checked)
                .RIWAYATKESEHATAN_03 = txtRIWAYATKESEHATAN_03.Text
                .RIWAYATKESEHATAN_04 = chk_val(chkRIWAYATKESEHATAN_04.Checked)
                .RIWAYATKESEHATAN_05 = chk_val(chkRIWAYATKESEHATAN_05.Checked)
                .RIWAYATKESEHATAN_06 = txtRIWAYATKESEHATAN_06.Text
                .RIWAYATPSIKOSOSIAL_01 = chk_val(chkRIWAYATPSIKOSOSIAL_01.Checked)
                .RIWAYATPSIKOSOSIAL_02 = chk_val(chkRIWAYATPSIKOSOSIAL_02.Checked)
                .RIWAYATPSIKOSOSIAL_03 = chk_val(chkRIWAYATPSIKOSOSIAL_03.Checked)
                .RIWAYATPSIKOSOSIAL_04 = chk_val(chkRIWAYATPSIKOSOSIAL_04.Checked)
                .RIWAYATPSIKOSOSIAL_05 = chk_val(chkRIWAYATPSIKOSOSIAL_05.Checked)
                .RIWAYATPSIKOSOSIAL_06 = chk_val(chkRIWAYATPSIKOSOSIAL_06.Checked)
                .RIWAYATPSIKOSOSIAL_07 = chk_val(chkRIWAYATPSIKOSOSIAL_07.Checked)
                .RIWAYATPSIKOSOSIAL_08 = chk_val(chkRIWAYATPSIKOSOSIAL_08.Checked)
                .RIWAYATPSIKOSOSIAL_09 = txtRIWAYATPSIKOSOSIAL_09.Text
                .RIWAYATPSIKOSOSIAL_10 = chk_val(chkRIWAYATPSIKOSOSIAL_10.Checked)
                .RIWAYATPSIKOSOSIAL_11 = chk_val(chkRIWAYATPSIKOSOSIAL_11.Checked)
                .RIWAYATPSIKOSOSIAL_12 = chk_val(chkRIWAYATPSIKOSOSIAL_12.Checked)
                .RIWAYATPSIKOSOSIAL_13 = chk_val(chkRIWAYATPSIKOSOSIAL_13.Checked)
                .HASILPENUNJANG_01 = txtHASILPENUNJANG_01.Text
                .PENGOBATAN_01 = txtPENGOBATAN_01.Text
                .DISCHARGE_01 = chk_val(chkDISCHARGE_01.Checked)
                .DISCHARGE_02 = chk_val(chkDISCHARGE_02.Checked)
                .DISCHARGE_03 = txtDISCHARGE_03.Text
                .DISCHARGE_04 = chk_val(chkDISCHARGE_04.Checked)
                .DISCHARGE_05 = chk_val(chkDISCHARGE_05.Checked)
                .DISCHARGE_06 = txtDISCHARGE_06.Text
                .DISCHARGE_07 = chk_val(chkDISCHARGE_07.Checked)
                .DISCHARGE_08 = chk_val(chkDISCHARGE_08.Checked)
                .DISCHARGE_09 = txtDISCHARGE_09.Text
                .DISCHARGE_10 = chk_val(chkDISCHARGE_10.Checked)
                .DISCHARGE_11 = chk_val(chkDISCHARGE_11.Checked)
                .DISCHARGE_12 = txtDISCHARGE_12.Text
                .DISCHARGE_13 = chk_val(chkDISCHARGE_13.Checked)
                .DISCHARGE_14 = chk_val(chkDISCHARGE_14.Checked)
                .DISCHARGE_15 = txtDISCHARGE_15.Text
                .DISCHARGE_16 = chk_val(chkDISCHARGE_16.Checked)
                .DISCHARGE_17 = chk_val(chkDISCHARGE_17.Checked)
                .DISCHARGE_18 = txtDISCHARGE_18.Text
                .DISCHARGE_19 = chk_val(chkDISCHARGE_19.Checked)
                .DISCHARGE_20 = chk_val(chkDISCHARGE_20.Checked)
                .DISCHARGE_21 = txtDISCHARGE_21.Text
                .DISCHARGE_22 = chk_val(chkDISCHARGE_22.Checked)
                .DISCHARGE_23 = chk_val(chkDISCHARGE_23.Checked)
                .DISCHARGE_24 = txtDISCHARGE_24.Text
                Try
                    .CETAK = oS_DIGITAL_RI_17.GetData(txtNoRegister.Text).CETAK
                Catch ex As Exception
                    .CETAK = 0
                End Try

                .KDUSER = sKDUSER_PERAWAT
                .KDUSER_SIGNATURE = sUserID
            End With

            If oFormMode = FORM_MODE.FORM_MODE_ADD Then
                Try
                    fn_Save = oS_DIGITAL_RI_17.InsertData(ds)
                Catch ex As Exception
                    MsgBox("Simpan Data : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            ElseIf oFormMode = FORM_MODE.FORM_MODE_EDIT Then
                Try
                    fn_Save = oS_DIGITAL_RI_17.UpdateData(ds)
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
    Private Sub frmEMedrekRI_17_MouseWheel(sender As Object, e As MouseEventArgs) Handles MyBase.MouseWheel
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