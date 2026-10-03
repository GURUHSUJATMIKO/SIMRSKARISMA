Imports DataAccess
Imports UI.WIN.MAIN.My.Resources
Imports System.Linq
Imports System.Data.SqlClient
Imports Newtonsoft.Json.Linq

Public Class frmEMedrekIDG_01
#Region "Declaration"
    Private oFormMode As FORM_MODE = FORM_MODE.FORM_MODE_VIEW
    Private isLoad As Boolean = False
    Private oS_DIGITAL_IGD_01 As New Transaksi.clsDigital_IGD_01
    Private down As Boolean = False
    Private sKDDOCTOR As String = String.Empty
    Private sKDCUSTOMER As String = String.Empty
    Private sNAMAPASIEN As String = String.Empty
    Private sALAMAT As String = String.Empty
    Private sPENJAMIN As String = String.Empty
    Private sTUJUAN As String = String.Empty
    Private sSIPDOKTER As String = String.Empty
    Private sKDDEPARTMENT As String = String.Empty
    Private sNOMORSEP As String = String.Empty
    Private sKARTUBPJS As String = String.Empty
#End Region
#Region "Function"
    Public Sub LoadMe(ByVal KDREG As String, KDDEPARTMENT As String, ByVal NOMORSEP As String, ByVal KDDOCTOR As String, ByVal KDCUSTOMER As String, ByVal NAMAPASIEN As String, ByVal JENISKELAMIN As String, ByVal TANGGALLAHIR As DateTime, ByVal ALAMAT As String, ByVal PENJAMIN As String, ByVal TUJUAN As String, ByVal SIPDOKTER As String, ByVal TANGGALDATANG As DateTime, ByVal KARTUBPJS As String, ByVal KTP As String, ByVal Usia As String)
        sKARTUBPJS = KARTUBPJS
        sKDDOCTOR = KDDOCTOR
        sKDCUSTOMER = KDCUSTOMER
        sNAMAPASIEN = NAMAPASIEN
        sALAMAT = ALAMAT
        sPENJAMIN = PENJAMIN
        sTUJUAN = TUJUAN
        sSIPDOKTER = SIPDOKTER
        sKDDEPARTMENT = KDDEPARTMENT
        sNOMORSEP = NOMORSEP

        txtNoRegister.Text = KDREG
        txtNoPasien.Text = KDCUSTOMER
        txtNamaPasien.Text = NAMAPASIEN
        deDATETANGGALLAHIR.DateTime = TANGGALLAHIR
        deDATE.DateTime = TANGGALDATANG
        'txtDokter.Text = KDDOCTOR
        txtJENISKELAMIN.Text = JENISKELAMIN
        txtNIK.Text = KTP

        If KDREG = "" Then
            MsgBox("Pasien Belum dipilih, Silahkan kembali ke List Pasien !!!", MsgBoxStyle.Exclamation, Me.Text)
            Me.Close()
        End If
        If KDREG = "Nul" Then
            MsgBox("Pasien Belum dipilih, Silahkan kembali ke List Pasien !!!", MsgBoxStyle.Exclamation, Me.Text)
            Me.Close()
        End If

        Dim ds = oS_DIGITAL_IGD_01.GetData(KDREG)

        If ds Is Nothing Then
            oFormMode = FORM_MODE.FORM_MODE_ADD
        Else
            oFormMode = FORM_MODE.FORM_MODE_EDIT
        End If

        txtUSIA.Text = Usia
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
        'fn_NOIDUSER()
        fn_LoadKDDOCTOR()
        fn_LoadITEM()
        fn_LoadUOM()
        fn_LoadSIGNA()
        fn_LoadCARAPAKAI()
        fn_ReoladResep()
        fn_LoadTEMPLATE()

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
        sPicture = Nothing

        btnSaveNew.Enabled = Not Status
        btnSaveClose.Enabled = Not Status
        btnDiagnosa.Enabled = Not Status
        'btnReload.Enabled = Not Status

        deDATE.Properties.ReadOnly = Status
        chkTRIAGE_TRAUMA.Properties.ReadOnly = Status
        chkTRIAGE_NONTRAUMA.Properties.ReadOnly = Status
        chkTRIAGE_MATERNITY.Properties.ReadOnly = Status
        chkTRIAGE_RED.Properties.ReadOnly = Status
        chkTRIAGE_YELLOW.Properties.ReadOnly = Status
        chkTRIAGE_GREEN.Properties.ReadOnly = Status
        chkTRIAGE_BLACK.Properties.ReadOnly = Status
        'chkTRIAGE_AIRWAY.Properties.ReadOnly = Status
        'chkTRIAGE_BREATHING.Properties.ReadOnly = Status
        'chkTRIAGE_CIRCULATION.Properties.ReadOnly = Status
        cboTRIAGE_AIRWAY.Properties.ReadOnly = Status
        cboTRIAGE_BREATHING.Properties.ReadOnly = Status
        cboTRIAGE_CIRCULATION.Properties.ReadOnly = Status
        chkTRIAGE_DATANGSENDIRI.Properties.ReadOnly = Status
        chkTRIAGE_DIANTAR.Properties.ReadOnly = Status
        cboTRIAGE_DIANTAR.Properties.ReadOnly = Status
        chkAUTOANAMNESA.Properties.ReadOnly = Status
        chkALLOANAMNESA.Properties.ReadOnly = Status
        deDATEPUKULPERIKSA.Properties.ReadOnly = Status
        deDATEPUKULRAWAT.Properties.ReadOnly = Status
        txtPETUGAS_TRIAGE.Properties.ReadOnly = Status
        txtRIWAYAT.Properties.ReadOnly = Status
        txtRIWAYAT_ALERGI.Properties.ReadOnly = Status
        txtRIWAYAT_PENYAKITDAHULU.Properties.ReadOnly = Status
        cboTINGKATKESADARAN.Properties.ReadOnly = Status
        cboE.Properties.ReadOnly = Status
        cboM.Properties.ReadOnly = Status
        cboV.Properties.ReadOnly = Status
        txtKEADAANUMUM.Properties.ReadOnly = Status
        txtBERATBADAN.Properties.ReadOnly = Status
        txtTINGGIBADAN.Properties.ReadOnly = Status
        txtBP.Properties.ReadOnly = Status
        txtHR.Properties.ReadOnly = Status
        txtRR.Properties.ReadOnly = Status
        txtT.Properties.ReadOnly = Status
        txtSPO2.Properties.ReadOnly = Status
        chkRESIKO_1.Properties.ReadOnly = Status
        chkRESIKO_2.Properties.ReadOnly = Status
        chkFUNGSIONAL_1.Properties.ReadOnly = Status
        chkFUNGSIONAL_2.Properties.ReadOnly = Status
        chkFUNGSIONAL_3.Properties.ReadOnly = Status
        txtFUNGSIONAL_2.Properties.ReadOnly = Status
        txtFUNGSIONAL_3.Properties.ReadOnly = Status
        chkSKALANYERI_00.Properties.ReadOnly = Status
        chkSKALANYERI_01.Properties.ReadOnly = Status
        chkSKALANYERI_02.Properties.ReadOnly = Status
        chkSKALANYERI_03.Properties.ReadOnly = Status
        chkSKALANYERI_04.Properties.ReadOnly = Status
        chkSKALANYERI_05.Properties.ReadOnly = Status
        chkSKALANYERI_06.Properties.ReadOnly = Status
        chkSKALANYERI_07.Properties.ReadOnly = Status
        chkSKALANYERI_08.Properties.ReadOnly = Status
        chkSKALANYERI_09.Properties.ReadOnly = Status
        chkSKALANYERI_10.Properties.ReadOnly = Status
        chkNORMAL_KEPALA.Properties.ReadOnly = Status
        chkTIDAK_NORMAL_KEPALA.Properties.ReadOnly = Status
        txtTidakNormalKepala.Properties.ReadOnly = Status
        chkNORMAL_MATA.Properties.ReadOnly = Status
        chkTIDAK_NORMAL_MATA.Properties.ReadOnly = Status
        txtTidakNormalMata.Properties.ReadOnly = Status
        chkNORMAL_LEHER.Properties.ReadOnly = Status
        chkTIDAK_NORMAL_LEHER.Properties.ReadOnly = Status
        txtTidakNormalLeher.Properties.ReadOnly = Status
        chkNORMAL_DADA.Properties.ReadOnly = Status
        chkTIDAK_NORMAL_DADA.Properties.ReadOnly = Status
        txtTidakNormalDada.Properties.ReadOnly = Status
        chkNORMAL_PERUT.Properties.ReadOnly = Status
        chkTIDAK_NORMAL_PERUT.Properties.ReadOnly = Status
        txtTidakNormalPerut.Properties.ReadOnly = Status
        chkNORMAL_ALATGERAK.Properties.ReadOnly = Status
        chkTIDAK_NORMAL_ALATGERAK.Properties.ReadOnly = Status
        txtTidakNormalAlatGerak.Properties.ReadOnly = Status
        txtTINDAKLANJUT_TUJUAN.Properties.ReadOnly = Status
        txtPemeriksaanFisik.Properties.ReadOnly = Status

        grdKDDOCTOR.Properties.ReadOnly = Status
        txtKDDIAGNOSA.Properties.ReadOnly = Status
        chkKESIMPULAN_PERBAIKAN.Properties.ReadOnly = Status
        chkKESIMPULAN_STABIL.Properties.ReadOnly = Status
        chkKESIMPULAN_PERBURUKAN.Properties.ReadOnly = Status
        chkTINDAKLANJUT_RUJUK.Properties.ReadOnly = True
        chkTINDAKLANJUT_RAWAT.Properties.ReadOnly = True
        chkTINDAKLANJUT_PULANGPAKSA.Properties.ReadOnly = True
        chkTINDAKLANJUT_PULANG.Properties.ReadOnly = True
        txtLOKASI.Properties.ReadOnly = Status
        txtSAATPASIENPULANG_TEXT.Properties.ReadOnly = Status
        deDATEPUKUL.Properties.ReadOnly = Status
        cboSAATPULANG_TINGKATKESDARAN.Properties.ReadOnly = Status
        cboSAATPULANG_E.Properties.ReadOnly = Status
        cboSAATPULANG_M.Properties.ReadOnly = Status
        cboSAATPULANG_V.Properties.ReadOnly = Status
        txtSAATPULANG_BP.Properties.ReadOnly = Status
        txtSAATPULANG_HR.Properties.ReadOnly = Status
        txtSAATPULANG_RR.Properties.ReadOnly = Status
        txtSAATPULANG_SPO2.Properties.ReadOnly = Status
        txtSAATPULANG_T.Properties.ReadOnly = Status
        txtINSTRUKSILANJUTAN.Properties.ReadOnly = Status
        txtLOKASI.Properties.ReadOnly = Status
        txtWAKTU.Properties.ReadOnly = Status
        txtDOKTER_.Properties.ReadOnly = Status
        'txtOBATSAATPULANG.Properties.ReadOnly = True
        txtPERAWAT.Properties.ReadOnly = Status
        txtSURVEY_ALATGERAK_1.Properties.ReadOnly = Status
        CheckEdit1.Properties.ReadOnly = Status
        CheckEdit2.Properties.ReadOnly = Status
        CheckEdit3.Properties.ReadOnly = Status
        CheckEdit4.Properties.ReadOnly = Status
        CheckEdit5.Properties.ReadOnly = Status
        CheckEdit6.Properties.ReadOnly = Status
        txtSURVEY_MATA_1.Properties.ReadOnly = Status
        txtKELUHANUTAMA.Properties.ReadOnly = Status

        If oFormMode = FORM_MODE.FORM_MODE_ADD Then
            lWAKTU.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        Else
            lWAKTU.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        End If
    End Sub
    Private Sub fn_EmptyMe()
        'txtCODE.Text = "<--- AUTO --->"
        'deDATE.DateTime = Now
        chkTRIAGE_TRAUMA.Checked = False
        chkTRIAGE_NONTRAUMA.Checked = False
        chkTRIAGE_MATERNITY.Checked = False
        chkTRIAGE_RED.Checked = False
        chkTRIAGE_YELLOW.Checked = False
        chkTRIAGE_GREEN.Checked = False
        chkTRIAGE_BLACK.Checked = False
        'chkTRIAGE_AIRWAY.Checked = False
        'chkTRIAGE_BREATHING.Checked = False
        'chkTRIAGE_CIRCULATION.Checked = False
        cboTRIAGE_AIRWAY.ResetText()
        cboTRIAGE_BREATHING.ResetText()
        cboTRIAGE_CIRCULATION.ResetText()
        chkTRIAGE_DATANGSENDIRI.Checked = False
        chkTRIAGE_DIANTAR.Checked = False
        cboTRIAGE_DIANTAR.ResetText()
        chkAUTOANAMNESA.Checked = False
        chkALLOANAMNESA.Checked = False
        deDATEPUKULPERIKSA.Time = Now
        deDATEPUKULRAWAT.Time = Now
        txtPETUGAS_TRIAGE.ResetText()
        txtRIWAYAT.ResetText()
        txtRIWAYAT_ALERGI.ResetText()
        txtRIWAYAT_PENYAKITDAHULU.ResetText()
        cboTINGKATKESADARAN.ResetText()
        txtGCS.ResetText()
        cboE.ResetText()
        cboM.ResetText()
        cboV.ResetText()
        txtKEADAANUMUM.ResetText()
        txtBERATBADAN.ResetText()
        txtTINGGIBADAN.ResetText()
        txtBP.ResetText()
        txtHR.ResetText()
        txtRR.ResetText()
        txtT.ResetText()
        txtSPO2.ResetText()
        chkRESIKO_1.Checked = False
        chkRESIKO_2.Checked = False
        chkFUNGSIONAL_1.Checked = False
        chkFUNGSIONAL_2.Checked = False
        chkFUNGSIONAL_3.Checked = False
        txtFUNGSIONAL_2.ResetText()
        txtFUNGSIONAL_3.ResetText()
        chkSKALANYERI_00.Checked = False
        chkSKALANYERI_01.Checked = False
        chkSKALANYERI_02.Checked = False
        chkSKALANYERI_03.Checked = False
        chkSKALANYERI_04.Checked = False
        chkSKALANYERI_05.Checked = False
        chkSKALANYERI_06.Checked = False
        chkSKALANYERI_07.Checked = False
        chkSKALANYERI_08.Checked = False
        chkSKALANYERI_09.Checked = False
        chkSKALANYERI_10.Checked = False
        chkNORMAL_KEPALA.Checked = True
        chkTIDAK_NORMAL_KEPALA.Checked = False
        txtTidakNormalKepala.ResetText()
        chkNORMAL_MATA.Checked = True
        chkTIDAK_NORMAL_MATA.Checked = False
        txtTidakNormalMata.ResetText()
        chkNORMAL_LEHER.Checked = True
        chkTIDAK_NORMAL_LEHER.Checked = False
        txtTidakNormalLeher.ResetText()
        chkNORMAL_DADA.Checked = True
        chkTIDAK_NORMAL_DADA.Checked = False
        txtTidakNormalDada.ResetText()
        chkNORMAL_PERUT.Checked = True
        chkTIDAK_NORMAL_PERUT.Checked = False
        txtTidakNormalPerut.ResetText()
        chkNORMAL_ALATGERAK.Checked = True
        chkTIDAK_NORMAL_ALATGERAK.Checked = False
        txtTidakNormalAlatGerak.ResetText()
        txtTINDAKLANJUT_TUJUAN.ResetText()
        txtSURVEY_ALATGERAK_1.ResetText()
        txtPemeriksaanFisik.ResetText()

        grdKDDOCTOR.ResetText()
        txtKDDIAGNOSA.ResetText()
        chkKESIMPULAN_PERBAIKAN.Checked = False
        chkKESIMPULAN_STABIL.Checked = False
        chkKESIMPULAN_PERBURUKAN.Checked = False
        chkTINDAKLANJUT_RUJUK.Checked = False
        chkTINDAKLANJUT_RAWAT.Checked = False
        chkTINDAKLANJUT_PULANGPAKSA.Checked = False
        chkTINDAKLANJUT_PULANG.Checked = False
        txtLOKASI.ResetText()
        txtSAATPASIENPULANG_TEXT.ResetText()
        deDATEPUKUL.DateTime = Now
        cboSAATPULANG_TINGKATKESDARAN.ResetText()
        txtSAATPULANG_GCS.ResetText()
        cboSAATPULANG_E.ResetText()
        cboSAATPULANG_M.ResetText()
        cboSAATPULANG_V.ResetText()
        txtSAATPULANG_BP.ResetText()
        txtSAATPULANG_HR.ResetText()
        txtSAATPULANG_RR.ResetText()
        txtSAATPULANG_SPO2.ResetText()
        txtSAATPULANG_T.ResetText()
        txtINSTRUKSILANJUTAN.ResetText()
        txtLOKASI.ResetText()
        txtWAKTU.ResetText()
        txtDOKTER_.ResetText()
        'txtOBATSAATPULANG.ResetText()
        txtPERAWAT.ResetText()
        txtTINDAKLANJUT.ResetText()

        grdKDDOCTOR.Text = sKDDOCTOR

        txtRELOAD.Reset()
        CheckEdit1.Checked = False
        CheckEdit2.Checked = False
        CheckEdit3.Checked = False
        CheckEdit4.Checked = False
        CheckEdit5.Checked = False
        CheckEdit6.Checked = False

        txtSURVEY_MATA_1.ResetText()
        txtKELUHANUTAMA.ResetText()

        'fn_LoadAdsemenPerawat()
    End Sub
    Private Sub fn_LoadAdsemenPerawat()
        If isLoad = False Then Exit Sub

        Dim oDigital As New Transaksi.clsDigital_IGD_02
        Dim dsDigital = oDigital.GetData(txtNoRegister.Text)
        If dsDigital IsNot Nothing Then
            'If txtRIWAYAT_PENYAKITDAHULU.Text = "" Then
            '    txtRIWAYAT_PENYAKITDAHULU.Text = dsDigital.TEXT_6
            'End If

            cboSAATPULANG_TINGKATKESDARAN.Text = dsDigital.KESADARAN_UMUM

            chkTRIAGE_DATANGSENDIRI.Checked = dsDigital.BIT_4
            chkTRIAGE_RED.Checked = dsDigital.BIT_6
            chkTRIAGE_YELLOW.Checked = dsDigital.BIT_7
            chkTRIAGE_GREEN.Checked = dsDigital.BIT_8
            chkTRIAGE_BLACK.Checked = dsDigital.BIT_8
            txtPETUGAS_TRIAGE.Text = dsDigital.KDUSER
            txtPERAWAT.Text = dsDigital.KDUSER
            cboTRIAGE_AIRWAY.Text = dsDigital.TEXT_3
            cboTRIAGE_BREATHING.Text = dsDigital.TEXT_4
            cboTRIAGE_CIRCULATION.Text = dsDigital.TEXT_5
            'If txtRIWAYAT.Text = "" Then
            '    txtRIWAYAT.Text = dsDigital.TEXT_97
            'End If
            'txtGCS.Text = dsDigital.TEXT_63
            'If txtGCS.Text = "" Then
            '    cboE.Text = dsDigital.TEXT_64
            '    cboM.Text = dsDigital.TEXT_65
            '    cboV.Text = dsDigital.TEXT_66
            '    cboSAATPULANG_E.Text = dsDigital.TEXT_64
            '    cboSAATPULANG_M.Text = dsDigital.TEXT_65
            '    cboSAATPULANG_V.Text = dsDigital.TEXT_66
            'End If

            cboE.Text = dsDigital.TEXT_64
            cboM.Text = dsDigital.TEXT_65
            cboV.Text = dsDigital.TEXT_66

            txtBP.Text = dsDigital.TANDA_VITAL_01
            txtHR.Text = dsDigital.TANDA_VITAL_02
            txtT.Text = dsDigital.TANDA_VITAL_03
            txtRR.Text = dsDigital.TANDA_VITAL_04
            txtSPO2.Text = dsDigital.TANDA_VITAL_09

            cboSAATPULANG_E.Text = dsDigital.TEXT_64
            cboSAATPULANG_M.Text = dsDigital.TEXT_65
            cboSAATPULANG_V.Text = dsDigital.TEXT_66

            txtSAATPULANG_BP.Text = dsDigital.TANDA_VITAL_01
            txtSAATPULANG_HR.Text = dsDigital.TANDA_VITAL_02
            txtSAATPULANG_T.Text = dsDigital.TANDA_VITAL_03
            txtSAATPULANG_RR.Text = dsDigital.TANDA_VITAL_04
            txtSAATPULANG_SPO2.Text = dsDigital.TANDA_VITAL_09

            cboTINGKATKESADARAN.Text = dsDigital.KESADARAN_UMUM
            txtKEADAANUMUM.Text = dsDigital.KEADAAN
            txtBERATBADAN.Text = dsDigital.TANDA_VITAL_05
            txtTINGGIBADAN.Text = dsDigital.TANDA_VITAL_06

            If dsDigital.TEXT_84 <> "" Then
                If dsDigital.TEXT_84 = 0 Then
                    chkSKALANYERI_00.Checked = True
                ElseIf dsDigital.TEXT_84 = 1 Then
                    chkSKALANYERI_01.Checked = True
                ElseIf dsDigital.TEXT_84 = 2 Then
                    chkSKALANYERI_02.Checked = True
                ElseIf dsDigital.TEXT_84 = 3 Then
                    chkSKALANYERI_03.Checked = True
                ElseIf dsDigital.TEXT_84 = 4 Then
                    chkSKALANYERI_04.Checked = True
                ElseIf dsDigital.TEXT_84 = 5 Then
                    chkSKALANYERI_05.Checked = True
                ElseIf dsDigital.TEXT_84 = 6 Then
                    chkSKALANYERI_06.Checked = True
                ElseIf dsDigital.TEXT_84 = 7 Then
                    chkSKALANYERI_07.Checked = True
                ElseIf dsDigital.TEXT_84 = 8 Then
                    chkSKALANYERI_08.Checked = True
                ElseIf dsDigital.TEXT_84 = 9 Then
                    chkSKALANYERI_09.Checked = True
                ElseIf dsDigital.TEXT_84 = 10 Then
                    chkSKALANYERI_10.Checked = True
                End If
            End If
        Else
            MsgBox("Asesmen Perawat belum di input", MsgBoxStyle.Exclamation, Me.Text)
        End If
    End Sub
    Private Sub fn_LoadVonek()
        If isLoad = False Then Exit Sub

        Dim oDigital As New Transaksi.clsDigital_IGD_03
        Dim dsDigital = oDigital.GetData(txtNoRegister.Text)
        If dsDigital IsNot Nothing Then
            txtRIWAYAT.Text = "Riwayat Menstruasi :" & vbCrLf & "HPHT : " & dsDigital.RIWAYATMENSTRUASI_TEXT_1 & vbCrLf _
                              & "Dismenorea : " & dsDigital.RIWAYATMENSTRUASI_TEXT_2 & vbCrLf _
                              & "Siklus : " & dsDigital.RIWAYATMENSTRUASI_TEXT_3 & vbCrLf _
                              & "Banyaknya : " & dsDigital.RIWAYATMENSTRUASI_TEXT_4 & vbCrLf _
                              & IIf(dsDigital.RIWAYATMENSTRUASI_6_1 = True, "Teratur", "Tidak Teratur") & vbCrLf _
                              & "TP : " & dsDigital.RIWAYATMENSTRUASI_TEXT_7 & vbCrLf _
                              & IIf(dsDigital.RIWAYATMENSTRUASI_8_1 = True, "Menorhagia", "") & vbCrLf _
                              & IIf(dsDigital.RIWAYATMENSTRUASI_8_2 = True, "Metrorhagia", "")

            txtSURVEY_MATA_1.Text = "Pemeriksaan Dalam :" & vbCrLf & "Vulva Vagina : " & dsDigital.PEMERIKSAANDALAM_1_TEXT & vbCrLf _
                                    & "Portio : " & dsDigital.PEMERIKSAANDALAM_2_TEXT & vbCrLf _
                                    & "Ketuban : " & dsDigital.PEMERIKSAANDALAM_3_TEXT & vbCrLf _
                                    & "Pembukaan : " & dsDigital.PEMERIKSAANDALAM_4_TEXT & vbCrLf _
                                    & "Presentasi fetur : " & dsDigital.PEMERIKSAANDALAM_5_TEXT & vbCrLf _
                                    & "Hodge/Station : " & dsDigital.PEMERIKSAANDALAM_6_TEXT & vbCrLf
        Else
            MsgBox("Vonek Kebidan belum di input", MsgBoxStyle.Exclamation, Me.Text)
        End If
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
    Private Sub fn_UpdateDataReloadIGD(ByVal Parameter As String)
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

            SQL = "UPDATE S_DIGITAL_IGD_01_AWAL SET KDPENDAFTARAN = '" & txtNoRegister.Text & "' WHERE KDAWALASESMENIGD = '" & Parameter & "' "

            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "UPDATEALL")

            If oConn.State = ConnectionState.Open Then
                oConn.Close()
            End If
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadData()
        Try
            ' ***** HEADER *****
            Dim ds = oS_DIGITAL_IGD_01.GetData(txtNoRegister.Text)

            With ds
                deDATE.DateTime = .DATE
                chkTRIAGE_TRAUMA.Checked = .TRIAGE_TRAUMA
                chkTRIAGE_NONTRAUMA.Checked = .TRIAGE_NONTRAUMA
                chkTRIAGE_MATERNITY.Checked = .TRIAGE_MATERNITY
                chkTRIAGE_RED.Checked = .TRIAGE_RED
                chkTRIAGE_YELLOW.Checked = .TRIAGE_YELLOW
                chkTRIAGE_GREEN.Checked = .TRIAGE_GREEN
                chkTRIAGE_BLACK.Checked = .TRIAGE_BLACK
                'chkTRIAGE_AIRWAY.Checked = .TRIAGE_AIRWAY
                'chkTRIAGE_BREATHING.Checked = .TRIAGE_BREATHING
                'chkTRIAGE_CIRCULATION.Checked = .TRIAGE_CIRCULATION
                cboTRIAGE_AIRWAY.Text = .TRIAGE_AIRWAY_TEX
                cboTRIAGE_BREATHING.Text = .TRIAGE_BREATHING_TEXT
                cboTRIAGE_CIRCULATION.Text = .TRIAGE_CIRCULATION_TEXT
                chkTRIAGE_DATANGSENDIRI.Checked = .TRIAGE_DATANGSENDIRI
                chkTRIAGE_DIANTAR.Checked = .TRIAGE_DIANTAR
                cboTRIAGE_DIANTAR.Text = .TRIAGE_DIANTAR_TEXT
                chkAUTOANAMNESA.Checked = .AUTOANAMNESA
                chkALLOANAMNESA.Checked = .ALOANAMNESA
                deDATEPUKULPERIKSA.Time = .PUKULPERIKSA
                deDATEPUKULRAWAT.Time = .PUKULRAWAT
                txtPETUGAS_TRIAGE.Text = .PETUGASTRIAGE
                txtRIWAYAT.Text = .RIWAYAT
                txtRIWAYAT_ALERGI.Text = .RIWAYAT_ALERGI
                txtRIWAYAT_PENYAKITDAHULU.Text = .RIWAYAT_PENYAKITDAHULU
                cboTINGKATKESADARAN.Text = .TINGKATKESADARAN
                txtGCS.Text = .GCS
                cboE.Text = .E
                cboM.Text = .M
                cboV.Text = .V
                txtKEADAANUMUM.Text = .KEADAANUMUM
                txtBERATBADAN.Text = .BERATBADAN
                txtTINGGIBADAN.Text = .TINGGIBADAN
                txtBP.Text = .BP
                txtHR.Text = .HR
                txtRR.Text = .RR
                txtT.Text = .T
                txtSPO2.Text = .SP02
                chkRESIKO_1.Checked = .RESIKO_1
                chkRESIKO_2.Checked = .RESIKO_2
                chkFUNGSIONAL_1.Checked = .FUNGSIONAL_1
                chkFUNGSIONAL_2.Checked = .FUNGSIONAL_2
                chkFUNGSIONAL_3.Checked = .FUNGSIONAL_3
                txtFUNGSIONAL_2.Text = .FUNGSIONAL_2_TEXT
                txtFUNGSIONAL_3.Text = .FUNGSIONAL_3_TEXT
                chkSKALANYERI_00.Checked = .SKALANEYRI_00
                chkSKALANYERI_01.Checked = .SKALANEYRI_01
                chkSKALANYERI_02.Checked = .SKALANEYRI_02
                chkSKALANYERI_03.Checked = .SKALANEYRI_03
                chkSKALANYERI_04.Checked = .SKALANEYRI_04
                chkSKALANYERI_05.Checked = .SKALANEYRI_05
                chkSKALANYERI_06.Checked = .SKALANEYRI_06
                chkSKALANYERI_07.Checked = .SKALANEYRI_07
                chkSKALANYERI_08.Checked = .SKALANEYRI_08
                chkSKALANYERI_09.Checked = .SKALANEYRI_09
                chkSKALANYERI_10.Checked = .SKALANEYRI_10
                chkNORMAL_KEPALA.Checked = .NORMAL_KEPALA
                chkTIDAK_NORMAL_KEPALA.Checked = .TIDAK_NORMAL_KEPALA
                txtTidakNormalKepala.Text = .SURVEY_KEPALA_2
                chkNORMAL_MATA.Checked = .NORMAL_MATA
                chkTIDAK_NORMAL_MATA.Checked = .TIDAK_NORMAL_MATA
                txtTidakNormalMata.Text = .SURVEY_MATA_2
                chkNORMAL_LEHER.Checked = .NORMAL_LEHER
                chkTIDAK_NORMAL_LEHER.Checked = .TIDAK_NORMAL_LEHER
                txtTidakNormalLeher.Text = .SURVEY_LEHER_2
                chkNORMAL_DADA.Checked = .NORMAL_DADA
                chkTIDAK_NORMAL_DADA.Checked = .TIDAK_NORMAL_DADA
                txtTidakNormalDada.Text = .SURVEY_DADA_2
                chkNORMAL_PERUT.Checked = .NORMAL_PERUT
                chkTIDAK_NORMAL_PERUT.Checked = .TIDAK_NORMAL_PERUT
                txtTidakNormalPerut.Text = .SURVEY_PERUT_2
                chkNORMAL_ALATGERAK.Checked = .NORMAL_ALATGERAK
                chkTIDAK_NORMAL_ALATGERAK.Checked = .TIDAK_NORMAL_ALATGERAK
                txtTidakNormalAlatGerak.Text = .SURVEY_ALATGERAK_2
                txtTINDAKLANJUT_TUJUAN.Text = .TUJUAN
                txtSURVEY_ALATGERAK_1.Text = .SURVEY_ALATGERAK_1
                txtSURVEY_MATA_1.Text = .SURVEY_MATA_1
                txtKELUHANUTAMA.Text = .SURVEY_LEHER_1
                txtPemeriksaanFisik.Text = .SURVEY_DADA_1
                Try
                    'Dim img = (From x In oS_DIGITAL_IGD_01.GetData
                    '           Where x.KDREG = txtNoRegister.Text
                    '           Select x.ATTACHMENT_2).Single

                    Dim img = oS_DIGITAL_IGD_01.GetData(txtNoRegister.Text).ATTACHMENT_1

                    picGAMBAR2.Image = ByteArrayToImage(img.ToArray())
                Catch oErr As Exception
                End Try

                Try
                    Dim ImagePath As String = .SURVEY_PERUT_1
                    Dim img1 As Bitmap
                    Dim newImage As Image = Image.FromFile(.SURVEY_PERUT_1)

                    img1 = New Bitmap(ImagePath)
                    picGAMBAR2.ImageLocation = ImagePath

                    picGAMBAR2.Image = newImage
                Catch ex As Exception
                    sPicture = Nothing
                    'MsgBox("Load List Data Gambar tidak ditemukan dialamat : " & .SIMPANGAMBAR_1, MsgBoxStyle.Exclamation, Me.Text)
                End Try

                grdKDDOCTOR.Text = .DOCTOR_KODE
                txtKDDIAGNOSA.Text = .KDDIAGNOSA
                chkKESIMPULAN_PERBAIKAN.Checked = .KESIMPULAN_PERBAIKAN
                chkKESIMPULAN_STABIL.Checked = .KESIMPULAN_STABIL
                chkKESIMPULAN_PERBURUKAN.Checked = .KESIMPULAN_PERBURUKAN
                chkTINDAKLANJUT_RUJUK.Checked = .TINDAKLANJUT_RUJUK
                chkTINDAKLANJUT_RAWAT.Checked = .TINDAKLANJUT_RAWAT
                chkTINDAKLANJUT_PULANGPAKSA.Checked = .TINDAKLANJUT_PULANGPAKSA
                chkTINDAKLANJUT_PULANG.Checked = .TINDAKLANJUT_PULANG
                txtLOKASI.Text = .TUJUAN
                txtSAATPASIENPULANG_TEXT.Text = .SAATPASIENPULANG_TEXT
                deDATEPUKUL.DateTime = .SAATPASIENPULANG_TIME
                cboSAATPULANG_TINGKATKESDARAN.Text = .SAATPASIENPULANG_TINGKATKESADARAN
                txtSAATPULANG_GCS.Text = .SAATPASIENPULANG_GCS
                cboSAATPULANG_E.Text = .SAATPASIENPULANG_E
                cboSAATPULANG_M.Text = .SAATPASIENPULANG_M
                cboSAATPULANG_V.Text = .SAATPASIENPULANG_V
                txtSAATPULANG_BP.Text = .SAATPASIENPULANG_BP
                txtSAATPULANG_HR.Text = .SAATPASIENPULANG_HR
                txtSAATPULANG_RR.Text = .SAATPASIENPULANG_RR
                txtSAATPULANG_SPO2.Text = .SAATPASIENPULANG_SPO2
                txtSAATPULANG_T.Text = .SAATPASIENPULANG_T
                txtINSTRUKSILANJUTAN.Text = .INTRUKSILANJUTAN
                txtLOKASI.Text = .PERAWATANLANJUTAN_LOKASI
                txtWAKTU.Text = .PERAWATANLANJUTAN_WAKTU
                txtDOKTER_.Text = .PERAWATANLANJUTAN_DOKTER
                'txtOBATSAATPULANG.Text = .OBATSAATPULANG
                txtPERAWAT.Text = .PERAWATPENANGUNGJAWAB
                txtTINDAKLANJUT.Text = .SURVEY_KEPALA_1

                txtRELOAD.Text = .KDUSER_SIGNATURE

                CheckEdit1.Checked = .KRITERIA_01
                CheckEdit2.Checked = .KRITERIA_02
                CheckEdit3.Checked = .KRITERIA_03
                CheckEdit4.Checked = .KRITERIA_04
                CheckEdit5.Checked = .KRITERIA_05
                CheckEdit6.Checked = .KRITERIA_06

                BindingSource.DataSource = oS_DIGITAL_IGD_01.GetDataDetailResepPulang("IGD_" & txtNoRegister.Text).OrderBy(Function(x) x.SEQ).ToList()
                grdDetailResep.DataSource = BindingSource

                'BindingSource1.DataSource = oS_DIGITAL_IGD_01.GetDataDetailResepSelamaIGD("IGD_" & txtNoRegister.Text).OrderBy(Function(x) x.SEQ).ToList()
                'grdObatIGD.DataSource = BindingSource1

                'BindingSource2.DataSource = oS_DIGITAL_IGD_01.GetDataDetailResepRanap("IGD_" & txtNoRegister.Text).OrderBy(Function(x) x.SEQ).ToList()
                'grdObatRanap.DataSource = BindingSource2

                BindingSourceTindakanPoli.DataSource = oS_DIGITAL_IGD_01.GetDataDetailTindakanPoli(txtNoRegister.Text).OrderBy(Function(x) x.SEQ).ToList()
                grdTindakanPoli.DataSource = BindingSourceTindakanPoli

                BindingSourcePenunjang.DataSource = oS_DIGITAL_IGD_01.GetDataDetailPenunjang(txtNoRegister.Text).OrderBy(Function(x) x.SEQ).ToList()
                grdTindakan.DataSource = BindingSourcePenunjang

                BindingSourceDiagnosa.DataSource = oS_DIGITAL_IGD_01.GetDataDetail(txtNoRegister.Text).OrderBy(Function(x) x.SEQ).ToList()
                grdDIAGNOSA_CPPT.DataSource = BindingSourceDiagnosa

            End With

            Dim oReq_Recipe As New Transaksi.clsReq_Recipe
            Dim dsResep = oReq_Recipe.GetData("IGD_" & txtNoRegister.Text)
            If dsResep IsNot Nothing Then
                txtGRANDTOTAL.Text = dsResep.GRANDTOTAL
            End If
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
                fn_Validate = False
                Exit Function
            End If
            Dim sTriage As Boolean = False

            If chkTRIAGE_RED.Checked = True Then
                sTriage = True
            End If
            If chkTRIAGE_YELLOW.Checked = True Then
                sTriage = True
            End If
            If chkTRIAGE_GREEN.Checked = True Then
                sTriage = True
            End If
            If chkTRIAGE_BLACK.Checked = True Then
                sTriage = True
            End If
            If sTriage = False Then
                MsgBox("Dibutuhkan Trigae", MsgBoxStyle.Exclamation, Me.Text)
                chkTRIAGE_RED.Focus()
                fn_Validate = False
                Exit Function
            End If
            Dim Kriteria As Boolean = False

            Dim ABC As Boolean = False

            If cboTRIAGE_AIRWAY.Text = "-" Or cboTRIAGE_AIRWAY.Text = "" Then
            Else
                ABC = True
            End If

            If cboTRIAGE_BREATHING.Text = "-" Or cboTRIAGE_BREATHING.Text = "" Then
            Else
                ABC = True
            End If

            If cboTRIAGE_CIRCULATION.Text = "-" Or cboTRIAGE_CIRCULATION.Text = "" Then
            Else
                ABC = True
            End If

            If ABC = False Then
                MsgBox("Dibutuhkan Airway/Breathing/Circulation", MsgBoxStyle.Exclamation, Me.Text)
                cboTRIAGE_AIRWAY.Focus()
                fn_Validate = False
                Exit Function
            End If

            If CheckEdit1.Checked = True Then
                Kriteria = True
            End If
            If CheckEdit2.Checked = True Then
                Kriteria = True
            End If
            If CheckEdit3.Checked = True Then
                Kriteria = True
            End If
            If CheckEdit4.Checked = True Then
                Kriteria = True
            End If
            If CheckEdit5.Checked = True Then
                Kriteria = True
            End If
            If CheckEdit6.Checked = True Then
                Kriteria = True
            End If

            If Kriteria = False Then
                MsgBox("Dibutuhkan Kriteria", MsgBoxStyle.Exclamation, Me.Text)
                CheckEdit1.Focus()
                fn_Validate = False
                Exit Function
            End If

            If txtPETUGAS_TRIAGE.Text = String.Empty Then
                MsgBox("Dibutuhkan Petugas Triage", MsgBoxStyle.Exclamation, Me.Text)
                txtPETUGAS_TRIAGE.Focus()
                fn_Validate = False
                Exit Function
            End If
            If txtPERAWAT.Text = String.Empty Then
                If txtPETUGAS_TRIAGE.Text = String.Empty Then
                    MsgBox("Dibutuhkan Perawat", MsgBoxStyle.Exclamation, Me.Text)
                    txtPERAWAT.Focus()
                    fn_Validate = False
                    Exit Function
                Else
                    txtPERAWAT.Text = txtPETUGAS_TRIAGE.Text
                End If
            End If
            If grdKDDOCTOR.Text = String.Empty Then
                MsgBox("Dibutuhkan Dokter", MsgBoxStyle.Exclamation, Me.Text)
                grdKDDOCTOR.Focus()
                fn_Validate = False
                Exit Function
            End If
            'If txtRIWAYAT.Text = String.Empty Then
            '    MsgBox("Dibutuhkan Riwayat", MsgBoxStyle.Exclamation, Me.Text)
            '    txtRIWAYAT.Focus()
            '    fn_Validate = False
            '    Exit Function
            'End If
            If txtRIWAYAT_PENYAKITDAHULU.Text = String.Empty Then
                MsgBox("Dibutuhkan Anamnesis", MsgBoxStyle.Exclamation, Me.Text)
                txtRIWAYAT_PENYAKITDAHULU.Focus()
                fn_Validate = False
                Exit Function
            End If
            If txtBP.Text = String.Empty Then
                MsgBox("Dibutuhkan Tensi", MsgBoxStyle.Exclamation, Me.Text)
                txtBP.Focus()
                fn_Validate = False
                Exit Function
            End If
            If txtHR.Text = String.Empty Then
                MsgBox("Dibutuhkan Nadi", MsgBoxStyle.Exclamation, Me.Text)
                txtHR.Focus()
                fn_Validate = False
                Exit Function
            End If
            If txtRR.Text = String.Empty Then
                MsgBox("Dibutuhkan Respirasi", MsgBoxStyle.Exclamation, Me.Text)
                txtRR.Focus()
                fn_Validate = False
                Exit Function
            End If
            If txtT.Text = String.Empty Then
                MsgBox("Dibutuhkan Suhu", MsgBoxStyle.Exclamation, Me.Text)
                txtT.Focus()
                fn_Validate = False
                Exit Function
            End If
            If txtSPO2.Text = String.Empty Then
                MsgBox("Dibutuhkan SPO2", MsgBoxStyle.Exclamation, Me.Text)
                txtSPO2.Focus()
                fn_Validate = False
                Exit Function
            End If
            'If txtKDDIAGNOSA.Text = String.Empty Then
            '    MsgBox("Dibutuhkan Diagnosa", MsgBoxStyle.Exclamation, Me.Text)
            '    txtKDDIAGNOSA.Focus()
            '    fn_Validate = False
            '    Exit Function
            'End If
            ''If txtTINDAKLANJUT.Text = String.Empty Then
            ''    MsgBox("Dibutuhkan Tindak Lanjut", MsgBoxStyle.Exclamation, Me.Text)
            ''    txtTINDAKLANJUT.Focus()
            ''    fn_Validate = False
            ''    Exit Function
            ''End If

            'If txtKDDIAGNOSA.Text = String.Empty Then
            '    MsgBox("Dibutuhkan Diagnosa", MsgBoxStyle.Exclamation, Me.Text)
            '    txtKDDIAGNOSA.Focus()
            '    fn_Validate = False
            '    Exit Function
            'End If

            Dim listdiagnosa As New List(Of String)

            For i As Integer = 0 To grvDIAGNOSA_CPPT.RowCount - 2
                listdiagnosa.Add(IIf(grvDIAGNOSA_CPPT.GetRowCellValue(i, colKDDIAGNOSA_CPPT) = "", "", grvDIAGNOSA_CPPT.GetRowCellValue(i, colKDDIAGNOSA_CPPT) & "-") & grvDIAGNOSA_CPPT.GetRowCellValue(i, colNAMADIAGNOSA_CPPT))
            Next

            txtKDDIAGNOSA.Text = String.Join(", ", listdiagnosa.ToArray)

            If txtKDDIAGNOSA.Text = String.Empty Then
                MsgBox("Dibutuhkan Diagnosa", MsgBoxStyle.Exclamation, Me.Text)
                grdDIAGNOSA_CPPT.Focus()
                fn_Validate = False
                Exit Function
            End If
        Catch oErr As Exception
            MsgBox("Validate Data : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Function
    Private Function fn_Save() As Boolean
        Try
            Dim sTindakan As Integer = 0
            Dim sTerapi As Integer = 0

            Dim listObatPulang As New List(Of String)
            Dim listObatIGD As New List(Of String)
            Dim listObatRanap As New List(Of String)

            For i As Integer = 0 To grvDetailResep.RowCount - 2
                listObatPulang.Add(fn_LoadITEM(grvDetailResep.GetRowCellValue(i, colKDITEM)) & " No " & IntegerToRoman(CInt(grvDetailResep.GetRowCellValue(i, colQTY))) & " " & fn_LoadUOMDESCRIPTION(grvDetailResep.GetRowCellValue(i, colKDUOM)) & " " & IIf(String.IsNullOrEmpty(grvDetailResep.GetRowCellValue(i, colKDSIGNA)), "", fn_LoadSIGNA(grvDetailResep.GetRowCellValue(i, colKDSIGNA))) & " " & IIf(String.IsNullOrEmpty(grvDetailResep.GetRowCellValue(i, colKDCARAPAKAI)), "", fn_LoadCARAPAKAI(grvDetailResep.GetRowCellValue(i, colKDCARAPAKAI))) & " " & IIf(String.IsNullOrEmpty(grvDetailResep.GetRowCellValue(i, colREMARKS_DOKTER)), "", grvDetailResep.GetRowCellValue(i, colREMARKS_DOKTER)))
            Next
            For i As Integer = 0 To grvObatIGD.RowCount - 2
                listObatIGD.Add(fn_LoadITEM(grvObatIGD.GetRowCellValue(i, colKDITEM_ObatIGD)) & " No " & IntegerToRoman(CInt(grvObatIGD.GetRowCellValue(i, colQTY_ObatIGD))) & " " & fn_LoadUOMDESCRIPTION(grvObatIGD.GetRowCellValue(i, colKDUOM_ObatIGD)) & " " & IIf(String.IsNullOrEmpty(grvObatIGD.GetRowCellValue(i, colKDSIGNA_ObatIGD)), "", fn_LoadSIGNA(grvObatIGD.GetRowCellValue(i, colKDSIGNA_ObatIGD))) & " " & IIf(String.IsNullOrEmpty(grvObatIGD.GetRowCellValue(i, colKDCARAPAKAI_ObatIGD)), "", fn_LoadCARAPAKAI(grvObatIGD.GetRowCellValue(i, colKDCARAPAKAI_ObatIGD))) & " " & IIf(String.IsNullOrEmpty(grvObatIGD.GetRowCellValue(i, colREMARKS_DOKTER_ObatIGD)), "", grvObatIGD.GetRowCellValue(i, colREMARKS_DOKTER_ObatIGD)))
            Next
            For i As Integer = 0 To grvObatRanap.RowCount - 2
                listObatRanap.Add(fn_LoadITEM(grvObatRanap.GetRowCellValue(i, colKDITEM_ObatRanap)) & " No " & IntegerToRoman(CInt(grvObatRanap.GetRowCellValue(i, colQTY_ObatRanap))) & " " & fn_LoadUOMDESCRIPTION(grvObatRanap.GetRowCellValue(i, colKDUOM_ObatRanap)) & " " & IIf(String.IsNullOrEmpty(grvObatRanap.GetRowCellValue(i, colKDSIGNA_ObatRanap)), "", fn_LoadSIGNA(grvObatRanap.GetRowCellValue(i, colKDSIGNA_ObatRanap))) & " " & IIf(String.IsNullOrEmpty(grvObatRanap.GetRowCellValue(i, colKDCARAPAKAI_ObatRanap)), "", fn_LoadCARAPAKAI(grvObatRanap.GetRowCellValue(i, colKDCARAPAKAI_ObatRanap))) & " " & IIf(String.IsNullOrEmpty(grvObatRanap.GetRowCellValue(i, colREMARKS_DOKTER_ObatRanap)), "", grvObatRanap.GetRowCellValue(i, colREMARKS_DOKTER_ObatRanap)))
            Next

            ' ***** HEADER *****
            Dim ds = oS_DIGITAL_IGD_01.GetStructureHeader
            With ds
                .DATE = deDATE.DateTime
                .KDPENDAFTARAN = txtNoRegister.Text
                .KDCUSTOMER = sKDCUSTOMER
                .NAMAPASIEN = sNAMAPASIEN
                .JENISKELAMIN = txtJENISKELAMIN.Text
                .TANGGALLAHIR = deDATETANGGALLAHIR.DateTime.ToString("dd-MM-yyyy")
                .ALAMAT = sALAMAT
                Try
                    .DATECREATED = oS_DIGITAL_IGD_01.GetData(txtNoRegister.Text).DATECREATED
                Catch ex As Exception
                    .DATECREATED = Now
                End Try
                .DATEUPDATED = Now
                .TRIAGE_TRAUMA = chkTRIAGE_TRAUMA.Checked
                .TRIAGE_NONTRAUMA = chkTRIAGE_NONTRAUMA.Checked
                .TRIAGE_MATERNITY = chkTRIAGE_MATERNITY.Checked
                .TRIAGE_RED = chkTRIAGE_RED.Checked
                .TRIAGE_YELLOW = chkTRIAGE_YELLOW.Checked
                .TRIAGE_GREEN = chkTRIAGE_GREEN.Checked
                .TRIAGE_BLACK = chkTRIAGE_BLACK.Checked
                .TRIAGE_AIRWAY = False
                .TRIAGE_BREATHING = False
                .TRIAGE_CIRCULATION = False
                .TRIAGE_AIRWAY_TEX = cboTRIAGE_AIRWAY.Text
                .TRIAGE_BREATHING_TEXT = cboTRIAGE_BREATHING.Text
                .TRIAGE_CIRCULATION_TEXT = cboTRIAGE_CIRCULATION.Text
                .TRIAGE_DATANGSENDIRI = chkTRIAGE_DATANGSENDIRI.Checked
                .TRIAGE_DIANTAR = chkTRIAGE_DIANTAR.Checked
                .TRIAGE_DIANTAR_TEXT = cboTRIAGE_DIANTAR.Text
                .AUTOANAMNESA = chkAUTOANAMNESA.Checked
                .ALOANAMNESA = chkALLOANAMNESA.Checked
                .PUKULPERIKSA = deDATEPUKULPERIKSA.Time
                .PUKULRAWAT = deDATEPUKULRAWAT.Time
                .PETUGASTRIAGE = txtPETUGAS_TRIAGE.Text
                .RIWAYAT = txtRIWAYAT.Text
                .RIWAYAT_ALERGI = txtRIWAYAT_ALERGI.Text
                .RIWAYAT_PENYAKITDAHULU = txtRIWAYAT_PENYAKITDAHULU.Text
                .TINGKATKESADARAN = cboTINGKATKESADARAN.Text
                .GCS = txtGCS.Text
                .E = cboE.Text
                .M = cboM.Text
                .V = cboV.Text
                .KEADAANUMUM = txtKEADAANUMUM.Text
                .BERATBADAN = txtBERATBADAN.Text
                .TINGGIBADAN = txtTINGGIBADAN.Text
                .BP = txtBP.Text
                .HR = txtHR.Text
                .RR = txtRR.Text
                .T = txtT.Text
                .SP02 = txtSPO2.Text
                .RESIKO_1 = chkRESIKO_1.Checked
                .RESIKO_2 = chkRESIKO_2.Checked
                .FUNGSIONAL_1 = chkFUNGSIONAL_1.Checked
                .FUNGSIONAL_2 = chkFUNGSIONAL_2.Checked
                .FUNGSIONAL_3 = chkFUNGSIONAL_3.Checked
                .FUNGSIONAL_2_TEXT = txtFUNGSIONAL_2.Text
                .FUNGSIONAL_3_TEXT = txtFUNGSIONAL_3.Text
                .SKALANEYRI_00 = chkSKALANYERI_00.Checked
                .SKALANEYRI_01 = chkSKALANYERI_01.Checked
                .SKALANEYRI_02 = chkSKALANYERI_02.Checked
                .SKALANEYRI_03 = chkSKALANYERI_03.Checked
                .SKALANEYRI_04 = chkSKALANYERI_04.Checked
                .SKALANEYRI_05 = chkSKALANYERI_05.Checked
                .SKALANEYRI_06 = chkSKALANYERI_06.Checked
                .SKALANEYRI_07 = chkSKALANYERI_07.Checked
                .SKALANEYRI_08 = chkSKALANYERI_08.Checked
                .SKALANEYRI_09 = chkSKALANYERI_09.Checked
                .SKALANEYRI_10 = chkSKALANYERI_10.Checked
                .SURVEY_KEPALA_1 = txtTINDAKLANJUT.Text
                .NORMAL_KEPALA = chkNORMAL_KEPALA.Checked
                .TIDAK_NORMAL_KEPALA = chkTIDAK_NORMAL_KEPALA.Checked
                .SURVEY_KEPALA_2 = txtTidakNormalKepala.Text
                .SURVEY_MATA_1 = txtSURVEY_MATA_1.Text
                .NORMAL_MATA = chkNORMAL_MATA.Checked
                .TIDAK_NORMAL_MATA = chkTIDAK_NORMAL_MATA.Checked
                .SURVEY_MATA_2 = txtTidakNormalMata.Text
                .SURVEY_LEHER_1 = txtKELUHANUTAMA.Text
                .NORMAL_LEHER = chkNORMAL_LEHER.Checked
                .TIDAK_NORMAL_LEHER = chkTIDAK_NORMAL_LEHER.Checked
                .SURVEY_LEHER_2 = txtTidakNormalLeher.Text
                .SURVEY_DADA_1 = txtPemeriksaanFisik.Text
                .NORMAL_DADA = chkNORMAL_DADA.Checked
                .TIDAK_NORMAL_DADA = chkTIDAK_NORMAL_DADA.Checked
                .SURVEY_DADA_2 = txtTidakNormalDada.Text

                If sPicture Is Nothing Then
                    Try
                        .SURVEY_PERUT_1 = oS_DIGITAL_IGD_01.GetData(txtNoRegister.Text).SURVEY_PERUT_1
                    Catch ex As Exception
                        .SURVEY_PERUT_1 = ""
                    End Try
                Else
                    Dim Alamat As String = sAlamatSimpanAsesmenIGD & Now.ToString("ddMMyyyyHHmm") & "." & txtNoRegister.Text & ".jpg"
                    picGAMBAR2.Image.Save(Alamat, System.Drawing.Imaging.ImageFormat.Jpeg)
                    .SURVEY_PERUT_1 = Alamat
                End If

                .NORMAL_PERUT = chkNORMAL_PERUT.Checked
                .TIDAK_NORMAL_PERUT = chkTIDAK_NORMAL_PERUT.Checked
                .SURVEY_PERUT_2 = txtTidakNormalPerut.Text
                .SURVEY_ALATGERAK_1 = txtSURVEY_ALATGERAK_1.Text
                .NORMAL_ALATGERAK = chkNORMAL_ALATGERAK.Checked
                .TIDAK_NORMAL_ALATGERAK = chkTIDAK_NORMAL_ALATGERAK.Checked
                .SURVEY_ALATGERAK_2 = txtTidakNormalAlatGerak.Text

                Try
                    Dim ms As New IO.MemoryStream()
                    picGAMBAR2.Image.Save(ms, picGAMBAR2.Image.RawFormat)

                    Dim data As Byte() = ms.GetBuffer()

                    .ATTACHMENT_2 = data
                Catch oErr As Exception
                    Try
                        .ATTACHMENT_2 = oS_DIGITAL_IGD_01.GetData(txtNoRegister.Text).ATTACHMENT_2
                    Catch ex As Exception

                    End Try
                End Try

                .DOCTOR_KODE = grdKDDOCTOR.EditValue
                .DOCTOR_NAME_DISPLAY = grdKDDOCTOR.Text
                .KDDIAGNOSA = txtKDDIAGNOSA.Text
                .KESIMPULAN_PERBAIKAN = chkKESIMPULAN_PERBAIKAN.Checked
                .KESIMPULAN_STABIL = chkKESIMPULAN_STABIL.Checked
                .KESIMPULAN_PERBURUKAN = chkKESIMPULAN_PERBURUKAN.Checked
                .TINDAKLANJUT_RUJUK = chkTINDAKLANJUT_RUJUK.Checked
                .TINDAKLANJUT_RAWAT = chkTINDAKLANJUT_RAWAT.Checked
                .TINDAKLANJUT_PULANGPAKSA = chkTINDAKLANJUT_PULANGPAKSA.Checked
                .TINDAKLANJUT_PULANG = chkTINDAKLANJUT_PULANG.Checked
                .TUJUAN = txtTINDAKLANJUT_TUJUAN.Text
                .SAATPASIENPULANG_TEXT = txtSAATPASIENPULANG_TEXT.Text
                '.SAATPASIENPULANG_TIME = deDATEPUKUL.DateTime
                If oFormMode = FORM_MODE.FORM_MODE_ADD Then
                    .SAATPASIENPULANG_TIME = Now
                Else
                    .SAATPASIENPULANG_TIME = deDATEPUKUL.DateTime
                End If

                .SAATPASIENPULANG_TINGKATKESADARAN = cboSAATPULANG_TINGKATKESDARAN.Text
                .SAATPASIENPULANG_GCS = txtSAATPULANG_GCS.Text
                .SAATPASIENPULANG_E = cboSAATPULANG_E.Text
                .SAATPASIENPULANG_M = cboSAATPULANG_M.Text
                .SAATPASIENPULANG_V = cboSAATPULANG_V.Text
                .SAATPASIENPULANG_BP = txtSAATPULANG_BP.Text
                .SAATPASIENPULANG_HR = txtSAATPULANG_HR.Text
                .SAATPASIENPULANG_RR = txtSAATPULANG_RR.Text
                .SAATPASIENPULANG_SPO2 = txtSAATPULANG_SPO2.Text
                .SAATPASIENPULANG_T = txtSAATPULANG_T.Text
                .INTRUKSILANJUTAN = txtINSTRUKSILANJUTAN.Text
                .PERAWATANLANJUTAN_LOKASI = txtLOKASI.Text
                .PERAWATANLANJUTAN_WAKTU = txtWAKTU.Text
                .PERAWATANLANJUTAN_DOKTER = txtDOKTER_.Text

                Try
                    Dim ObatPulang As String = ""
                    Dim ObatIGD As String = ""
                    Dim Obatranap As String = ""

                    If listObatIGD.Count > 0 Then
                        ObatIGD = "OBAT SELAMA IGD :" & vbCrLf & String.Join(", ", listObatIGD.ToArray)
                    End If
                    If listObatRanap.Count > 0 Then
                        Obatranap = "OBAT RANAP :" & vbCrLf & String.Join(", ", listObatRanap.ToArray)
                    End If
                    If listObatPulang.Count > 0 Then
                        ObatPulang = "OBAT PULANG :" & vbCrLf & String.Join(", ", listObatPulang.ToArray)
                    End If

                    .OBATSAATPULANG = ObatIGD & vbCrLf & Obatranap & vbCrLf & ObatPulang
                Catch ex As Exception
                    .OBATSAATPULANG = ""
                End Try
                '.OBATSAATPULANG = txtOBATSAATPULANG.Text

                Try
                    .CETAK = oS_DIGITAL_IGD_01.GetData(txtNoRegister.Text).CETAK
                Catch ex As Exception
                    .CETAK = 0
                End Try
                .PERAWATPENANGUNGJAWAB = txtPERAWAT.Text
                .KDUSER = sUserID
                .KDUSER_SIGNATURE = txtRELOAD.Text
                .KRITERIA_01 = CheckEdit1.Checked
                .KRITERIA_02 = CheckEdit2.Checked
                .KRITERIA_03 = CheckEdit3.Checked
                .KRITERIA_04 = CheckEdit4.Checked
                .KRITERIA_05 = CheckEdit5.Checked
                .KRITERIA_06 = CheckEdit6.Checked
            End With

            '' ***** DETIL *****
            'Dim arrDetail = oS_DIGITAL_IGD_01.GetStructureDetailList
            'For i As Integer = 0 To grvDetail.RowCount - 2
            '    Dim dsDetail = oS_DIGITAL_IGD_01.GetStructureDetail
            '    With dsDetail
            '        .SEQ = i
            '        .KDPENDAFTARAN = ds.KDPENDAFTARAN
            '        .PUKUL = CDate(grvDetail.GetRowCellValue(i, colPUKUL))
            '        .PENANGANAN = grvDetail.GetRowCellValue(i, colPENANGANAN)
            '        .NAMA = IIf(String.IsNullOrEmpty(grvDetail.GetRowCellValue(i, colNAMA)), "-", grvDetail.GetRowCellValue(i, colNAMA))
            '        .PARAF = IIf(String.IsNullOrEmpty(grvDetail.GetRowCellValue(i, colPARAF)), "-", grvDetail.GetRowCellValue(i, colPARAF))
            '    End With
            '    arrDetail.Add(dsDetail)
            'Next

            Dim oReq_Recipe As New Transaksi.clsReq_Recipe
            ' ***** HEADER *****
            Dim dsResepH = oReq_Recipe.GetStructureHeader
            With dsResepH
                Try
                    .DATECREATED = oReq_Recipe.GetData("IGD_" & txtNoRegister.Text).DATECREATED
                Catch ex As Exception
                    .DATECREATED = Now
                End Try
                .DATEUPDATED = Now
                .DATE = deDATE.DateTime
                .KDREQRECIPE = "IGD_" & txtNoRegister.Text
                .NOANTRIAN = String.Empty
                .KDPENDAFTARAN = txtNoRegister.Text
                .KDWAREHOUSE = 1
                .ALERGIOBAT = IIf(txtRIWAYAT_ALERGI.Text.ToString.Trim = String.Empty, "-", txtRIWAYAT_ALERGI.Text)
                .BERATBADAN = txtBERATBADAN.Text
                Try
                    .DESCRIPTION = oReq_Recipe.GetData("IGD_" & txtNoRegister.Text).DESCRIPTION
                Catch ex As Exception
                    .DESCRIPTION = ""
                End Try
                Try
                    .ISCHEKED = oReq_Recipe.GetData("IGD_" & txtNoRegister.Text).ISCHEKED
                Catch ex As Exception
                    .ISCHEKED = False
                End Try
                Try
                    .KONFIRMASIRESEP = oReq_Recipe.GetData("IGD_" & txtNoRegister.Text).KONFIRMASIRESEP
                Catch ex As Exception
                    .KONFIRMASIRESEP = "Resep Belum Diterima"
                End Try
                .SUBTOTAL_RINCIAN = CDec(0)
                .SUBTOTAL_PAKET = CDec(0)
                .SUBTOTAL_KRONIS = CDec(0)

                Calculate()

                .GRANDTOTAL = CDec(txtGRANDTOTAL.Text)
                .NOIDUSER = sUserID
                .ISPERUBAHANRESEP = False
                .ISAPPROVAL = False
                .NOIDUSER_FARMASI = String.Empty
                .DESCRIPTION_PERUBAHAN = String.Empty
                .PENJAMIN = sPENJAMIN
                .KDCUSTOMER = sKDCUSTOMER
                .PASIEN = sNAMAPASIEN
                .ALAMAT = ""
                .DOKTER = grdKDDOCTOR.Text
                .TUJUAN = sTUJUAN
                .DIAGNOSA = txtKDDIAGNOSA.Text
                .SIPDOKTER = sSIPDOKTER
                Try
                    .KDRECIPE = oReq_Recipe.GetData("IGD_" & txtNoRegister.Text).KDRECIPE
                Catch ex As Exception
                    .KDRECIPE = ""
                End Try
            End With

            Dim arrDetail = oReq_Recipe.GetStructureDetailList
            Dim arrDetailResume = oReq_Recipe.GetStructureDetailList

            For i As Integer = 0 To grvDetailResep.RowCount - 2
                Dim dsDetail = oReq_Recipe.GetStructureDetail
                With dsDetail
                    .SEQ = i
                    .KDREQRECIPE = dsResepH.KDREQRECIPE
                    .NAMAOBAT = fn_LoadITEM(grvDetailResep.GetRowCellValue(i, colKDITEM))
                    .SATUAN = fn_LoadUOMDESCRIPTION(grvDetailResep.GetRowCellValue(i, colKDUOM))
                    .SIGNA = IIf(String.IsNullOrEmpty(grvDetailResep.GetRowCellValue(i, colKDSIGNA)), "", fn_LoadSIGNA(grvDetailResep.GetRowCellValue(i, colKDSIGNA)))
                    .CARAPAKAI = IIf(String.IsNullOrEmpty(grvDetailResep.GetRowCellValue(i, colKDCARAPAKAI)), "", fn_LoadCARAPAKAI(grvDetailResep.GetRowCellValue(i, colKDCARAPAKAI)))
                    .QTY = CDec(grvDetailResep.GetRowCellValue(i, colQTY))
                    .PRICE = CDec(grvDetailResep.GetRowCellValue(i, colPRICE))
                    .GRANDTOTAL = CDec(grvDetailResep.GetRowCellValue(i, colGRANDTOTAL))
                    .ROMAWI = IntegerToRoman(CInt(grvDetailResep.GetFocusedRowCellValue(colQTY)))
                    .REMARKS_DOKTER = IIf(String.IsNullOrEmpty(grvDetailResep.GetRowCellValue(i, colREMARKS_DOKTER)), "", grvDetailResep.GetRowCellValue(i, colREMARKS_DOKTER))
                    .KDITEM = grvDetailResep.GetRowCellValue(i, colKDITEM)
                    .KDUOM = grvDetailResep.GetRowCellValue(i, colKDUOM)
                    .KDSIGNA = IIf(String.IsNullOrEmpty(grvDetailResep.GetRowCellValue(i, colKDSIGNA)), "1286", grvDetailResep.GetRowCellValue(i, colKDSIGNA))
                    .KDCARAPAKAI = IIf(String.IsNullOrEmpty(grvDetailResep.GetRowCellValue(i, colKDCARAPAKAI)), "10", grvDetailResep.GetRowCellValue(i, colKDCARAPAKAI))
                    .QTY_PERUBAHAN = grvDetailResep.GetRowCellValue(i, colQTY)
                    .REMARKS_FARMASI = "OBAT PULANG"
                    .DOSIS = ""
                End With

                If grvDetailResep.GetRowCellValue(i, colKDITEM) IsNot Nothing Then
                    arrDetail.Add(dsDetail)
                End If
            Next

            Dim i_ObatIGD As Integer = 100
            For i As Integer = 0 To grvObatIGD.RowCount - 2
                Dim dsDetail = oReq_Recipe.GetStructureDetail
                With dsDetail
                    .SEQ = i_ObatIGD
                    .KDREQRECIPE = dsResepH.KDREQRECIPE
                    .NAMAOBAT = fn_LoadITEM(grvObatIGD.GetRowCellValue(i, colKDITEM_ObatIGD))
                    .SATUAN = fn_LoadUOMDESCRIPTION(grvObatIGD.GetRowCellValue(i, colKDUOM_ObatIGD))
                    .SIGNA = IIf(String.IsNullOrEmpty(grvObatIGD.GetRowCellValue(i, colKDSIGNA_ObatIGD)), "", fn_LoadSIGNA(grvObatIGD.GetRowCellValue(i, colKDSIGNA_ObatIGD)))
                    .CARAPAKAI = IIf(String.IsNullOrEmpty(grvObatIGD.GetRowCellValue(i, colKDCARAPAKAI_ObatIGD)), "", fn_LoadCARAPAKAI(grvObatIGD.GetRowCellValue(i, colKDCARAPAKAI_ObatIGD)))
                    .QTY = CDec(grvObatIGD.GetRowCellValue(i, colQTY_ObatIGD))
                    .PRICE = CDec(grvObatIGD.GetRowCellValue(i, colPRICE_ObatIGD))
                    .GRANDTOTAL = CDec(grvObatIGD.GetRowCellValue(i, colGRANDTOTAL_ObatIGD))
                    .ROMAWI = IntegerToRoman(CInt(grvObatIGD.GetFocusedRowCellValue(colQTY_ObatIGD)))
                    .REMARKS_DOKTER = IIf(String.IsNullOrEmpty(grvObatIGD.GetRowCellValue(i, colREMARKS_DOKTER_ObatIGD)), "", grvObatIGD.GetRowCellValue(i, colREMARKS_DOKTER_ObatIGD))
                    .KDITEM = grvObatIGD.GetRowCellValue(i, colKDITEM_ObatIGD)
                    .KDUOM = grvObatIGD.GetRowCellValue(i, colKDUOM_ObatIGD)
                    .KDSIGNA = IIf(String.IsNullOrEmpty(grvObatIGD.GetRowCellValue(i, colKDSIGNA_ObatIGD)), "1286", grvObatIGD.GetRowCellValue(i, colKDSIGNA_ObatIGD))
                    .KDCARAPAKAI = IIf(String.IsNullOrEmpty(grvObatIGD.GetRowCellValue(i, colKDCARAPAKAI_ObatIGD)), "10", grvObatIGD.GetRowCellValue(i, colKDCARAPAKAI_ObatIGD))
                    .QTY_PERUBAHAN = grvObatIGD.GetRowCellValue(i, colQTY_ObatIGD)
                    .REMARKS_FARMASI = "OBAT IGD"
                    .DOSIS = ""
                    i_ObatIGD += 1
                End With

                If grvObatIGD.GetRowCellValue(i, colKDITEM_ObatIGD) IsNot Nothing Then
                    arrDetail.Add(dsDetail)
                End If
            Next

            Dim i_ObatRanap As Integer = 200
            For i As Integer = 0 To grvObatRanap.RowCount - 2
                Dim dsDetail = oReq_Recipe.GetStructureDetail
                With dsDetail
                    .SEQ = i_ObatRanap
                    .KDREQRECIPE = dsResepH.KDREQRECIPE
                    .NAMAOBAT = fn_LoadITEM(grvObatRanap.GetRowCellValue(i, colKDITEM_ObatRanap))
                    .SATUAN = fn_LoadUOMDESCRIPTION(grvObatRanap.GetRowCellValue(i, colKDUOM_ObatRanap))
                    .SIGNA = IIf(String.IsNullOrEmpty(grvObatRanap.GetRowCellValue(i, colKDSIGNA_ObatRanap)), "", fn_LoadSIGNA(grvObatRanap.GetRowCellValue(i, colKDSIGNA_ObatRanap)))
                    .CARAPAKAI = IIf(String.IsNullOrEmpty(grvObatRanap.GetRowCellValue(i, colKDCARAPAKAI_ObatRanap)), "", fn_LoadCARAPAKAI(grvObatRanap.GetRowCellValue(i, colKDCARAPAKAI_ObatRanap)))
                    .QTY = CDec(grvObatRanap.GetRowCellValue(i, colQTY_ObatRanap))
                    .PRICE = CDec(grvObatRanap.GetRowCellValue(i, colPRICE_ObatRanap))
                    .GRANDTOTAL = CDec(grvObatRanap.GetRowCellValue(i, colGRANDTOTAL_ObatRanap))
                    .ROMAWI = IntegerToRoman(CInt(grvObatRanap.GetFocusedRowCellValue(colQTY_ObatRanap)))
                    .REMARKS_DOKTER = IIf(String.IsNullOrEmpty(grvObatRanap.GetRowCellValue(i, colREMARKS_DOKTER_ObatRanap)), "", grvObatRanap.GetRowCellValue(i, colREMARKS_DOKTER_ObatRanap))
                    .KDITEM = grvObatRanap.GetRowCellValue(i, colKDITEM_ObatRanap)
                    .KDUOM = grvObatRanap.GetRowCellValue(i, colKDUOM_ObatRanap)
                    .KDSIGNA = IIf(String.IsNullOrEmpty(grvObatRanap.GetRowCellValue(i, colKDSIGNA_ObatRanap)), "1286", grvObatRanap.GetRowCellValue(i, colKDSIGNA_ObatRanap))
                    .KDCARAPAKAI = IIf(String.IsNullOrEmpty(grvObatRanap.GetRowCellValue(i, colKDCARAPAKAI_ObatRanap)), "10", grvObatRanap.GetRowCellValue(i, colKDCARAPAKAI_ObatRanap))
                    .QTY_PERUBAHAN = grvObatRanap.GetRowCellValue(i, colQTY_ObatRanap)
                    .REMARKS_FARMASI = "OBAT RANAP"
                    .DOSIS = IIf(String.IsNullOrEmpty(grvObatRanap.GetRowCellValue(i, colDOSIS)), "", grvObatRanap.GetRowCellValue(i, colDOSIS))
                    i_ObatRanap += 1
                End With

                If grvObatRanap.GetRowCellValue(i, colKDITEM_ObatRanap) IsNot Nothing Then
                    arrDetail.Add(dsDetail)
                End If
            Next

            '--------------------------------------RESUME

            Dim sEqResume As Integer = 0

            For i As Integer = 0 To grvDetailResep.RowCount - 2
                Dim dsDetail = oReq_Recipe.GetStructureDetail
                With dsDetail
                    sTerapi += 1

                    .SEQ = sEqResume
                    .KDREQRECIPE = dsResepH.KDREQRECIPE
                    .NAMAOBAT = fn_LoadITEM(grvDetailResep.GetRowCellValue(i, colKDITEM))
                    .SATUAN = fn_LoadUOMDESCRIPTION(grvDetailResep.GetRowCellValue(i, colKDUOM))
                    .SIGNA = IIf(String.IsNullOrEmpty(grvDetailResep.GetRowCellValue(i, colKDSIGNA)), "", fn_LoadSIGNA(grvDetailResep.GetRowCellValue(i, colKDSIGNA)))
                    .CARAPAKAI = IIf(String.IsNullOrEmpty(grvDetailResep.GetRowCellValue(i, colKDCARAPAKAI)), "", fn_LoadCARAPAKAI(grvDetailResep.GetRowCellValue(i, colKDCARAPAKAI)))
                    .QTY = CDec(grvDetailResep.GetRowCellValue(i, colQTY))
                    .PRICE = CDec(grvDetailResep.GetRowCellValue(i, colPRICE))
                    .GRANDTOTAL = CDec(grvDetailResep.GetRowCellValue(i, colGRANDTOTAL))
                    .ROMAWI = IntegerToRoman(CInt(grvDetailResep.GetFocusedRowCellValue(colQTY)))
                    .REMARKS_DOKTER = IIf(String.IsNullOrEmpty(grvDetailResep.GetRowCellValue(i, colREMARKS_DOKTER)), "", grvDetailResep.GetRowCellValue(i, colREMARKS_DOKTER))
                    .KDITEM = grvDetailResep.GetRowCellValue(i, colKDITEM)
                    .KDUOM = grvDetailResep.GetRowCellValue(i, colKDUOM)
                    .KDSIGNA = IIf(String.IsNullOrEmpty(grvDetailResep.GetRowCellValue(i, colKDSIGNA)), "1286", grvDetailResep.GetRowCellValue(i, colKDSIGNA))
                    .KDCARAPAKAI = IIf(String.IsNullOrEmpty(grvDetailResep.GetRowCellValue(i, colKDCARAPAKAI)), "10", grvDetailResep.GetRowCellValue(i, colKDCARAPAKAI))
                    .QTY_PERUBAHAN = grvDetailResep.GetRowCellValue(i, colQTY)
                    .REMARKS_FARMASI = "OBAT PULANG"
                    .DOSIS = ""
                    sEqResume += 1
                End With

                If grvDetailResep.GetRowCellValue(i, colKDITEM) IsNot Nothing Then
                    arrDetailResume.Add(dsDetail)
                End If
            Next

            For i As Integer = 0 To grvObatIGD.RowCount - 2
                Dim dsDetail = oReq_Recipe.GetStructureDetail
                With dsDetail
                    sTerapi += 1

                    .SEQ = sEqResume
                    .KDREQRECIPE = dsResepH.KDREQRECIPE
                    .NAMAOBAT = fn_LoadITEM(grvObatIGD.GetRowCellValue(i, colKDITEM_ObatIGD))
                    .SATUAN = fn_LoadUOMDESCRIPTION(grvObatIGD.GetRowCellValue(i, colKDUOM_ObatIGD))
                    .SIGNA = IIf(String.IsNullOrEmpty(grvObatIGD.GetRowCellValue(i, colKDSIGNA_ObatIGD)), "", fn_LoadSIGNA(grvObatIGD.GetRowCellValue(i, colKDSIGNA_ObatIGD)))
                    .CARAPAKAI = IIf(String.IsNullOrEmpty(grvObatIGD.GetRowCellValue(i, colKDCARAPAKAI_ObatIGD)), "", fn_LoadCARAPAKAI(grvObatIGD.GetRowCellValue(i, colKDCARAPAKAI_ObatIGD)))
                    .QTY = CDec(grvObatIGD.GetRowCellValue(i, colQTY_ObatIGD))
                    .PRICE = CDec(grvObatIGD.GetRowCellValue(i, colPRICE_ObatIGD))
                    .GRANDTOTAL = CDec(grvObatIGD.GetRowCellValue(i, colGRANDTOTAL_ObatIGD))
                    .ROMAWI = IntegerToRoman(CInt(grvObatIGD.GetFocusedRowCellValue(colQTY_ObatIGD)))
                    .REMARKS_DOKTER = IIf(String.IsNullOrEmpty(grvObatIGD.GetRowCellValue(i, colREMARKS_DOKTER_ObatIGD)), "", grvObatIGD.GetRowCellValue(i, colREMARKS_DOKTER_ObatIGD))
                    .KDITEM = grvObatIGD.GetRowCellValue(i, colKDITEM_ObatIGD)
                    .KDUOM = grvObatIGD.GetRowCellValue(i, colKDUOM_ObatIGD)
                    .KDSIGNA = IIf(String.IsNullOrEmpty(grvObatIGD.GetRowCellValue(i, colKDSIGNA_ObatIGD)), "1286", grvObatIGD.GetRowCellValue(i, colKDSIGNA_ObatIGD))
                    .KDCARAPAKAI = IIf(String.IsNullOrEmpty(grvObatIGD.GetRowCellValue(i, colKDCARAPAKAI_ObatIGD)), "10", grvObatIGD.GetRowCellValue(i, colKDCARAPAKAI_ObatIGD))
                    .QTY_PERUBAHAN = grvObatIGD.GetRowCellValue(i, colQTY_ObatIGD)
                    .REMARKS_FARMASI = "OBAT IGD"
                    .DOSIS = ""

                    sEqResume += 1
                End With

                If grvObatIGD.GetRowCellValue(i, colKDITEM_ObatIGD) IsNot Nothing Then
                    arrDetailResume.Add(dsDetail)
                End If
            Next

            '--------------------------TUTUP RESUME

            Dim dsTelaah = oReq_Recipe.GetStructureTelaah

            With dsTelaah
                .DATECREATED = Now
                .DATEUPDATED = Now
                .KDREQRECIPE = dsResepH.KDREQRECIPE
                .TELAAH_01 = False
                .TELAAH_02 = False
                .TELAAH_03 = False
                .TELAAH_04 = False
                .TELAAH_05 = False
                .TELAAH_06 = False
                .TELAAH_07 = False
                .TELAAH_08 = False
                .TELAAH_09 = False
                .TELAAH_10 = False
                .TELAAH_11 = False
                .TELAAH_12 = False
                .TELAAH_13 = False
                .TELAAH_14 = False
                .TELAAH_15 = False
                .TELAAH_16 = False
                .TELAAH_17 = False
                .TELAAH_18 = False
                .TELAAH_19 = False
                .TELAAH_20 = False
                .TELAAH_21 = False
                .TELAAH_22 = False
                .TELAAH_23 = False
                .TELAAH_24 = False
                .NOIDUSER = ""
                .REMARKS = ""
            End With

            Dim dsTelaahObat1 = oReq_Recipe.GetStructureTelaahObat1

            With dsTelaahObat1
                .DATECREATED = Now
                .DATEUPDATED = Now
                .KDREQRECIPE = dsResepH.KDREQRECIPE
                .TELAAH_01 = False
                .TELAAH_02 = False
                .TELAAH_03 = False
                .TELAAH_04 = False
                .TELAAH_05 = False
                .NOIDUSER = ""
                .REMARKS = ""
            End With

            Dim dsTelaahObat2 = oReq_Recipe.GetStructureTelaahObat2

            With dsTelaahObat2
                .DATECREATED = Now
                .DATEUPDATED = Now
                .KDREQRECIPE = dsResepH.KDREQRECIPE
                .TELAAH_01 = False
                .TELAAH_02 = False
                .TELAAH_03 = False
                .TELAAH_04 = False
                .TELAAH_05 = False
                .NOIDUSER = ""
                .REMARKS = ""
            End With

            Dim arrDetailTindakanPoli = oReq_Recipe.GetStructureDetailTindakanPoliList
            For i As Integer = 0 To grvTindakanPoli.RowCount - 2
                Dim dsDetail = oReq_Recipe.GetStructureDetailTindakanPoli
                With dsDetail
                    .SEQ = i
                    .KDPENDAFTARAN = ds.KDPENDAFTARAN
                    .PENANGANAN = grvTindakanPoli.GetRowCellValue(i, colPENANGANAN)
                End With
                arrDetailTindakanPoli.Add(dsDetail)
            Next

            Dim arrDetailPenunjang = oReq_Recipe.GetStructureDetailPenunjangList
            For i As Integer = 0 To grvTindakan.RowCount - 2
                Dim dsDetail = oReq_Recipe.GetStructureDetailPenunjang
                With dsDetail
                    .SEQ = i
                    .KDPENDAFTARAN = ds.KDPENDAFTARAN
                    .PENANGANAN = grvTindakan.GetRowCellValue(i, colPENANGANAN_)
                End With
                arrDetailPenunjang.Add(dsDetail)

                sTindakan += 1
            Next

            ' ***** KODING *****
            Dim oKoding As New Admission.clsKoding
            Dim dsKoding = oKoding.GetStructureHeader
            With dsKoding
                Try
                    .DATECREATED = oKoding.GetData(txtNoRegister.Text).DATECREATED
                Catch ex As Exception
                    .DATECREATED = Now
                End Try
                .DATEUPDATED = Now
                .KDKODING = "IGD_" & txtNoRegister.Text
                .KDPENDAFTARAN = txtNoRegister.Text
                .DATE = deDATE.DateTime
                ''new Font("Tahoma", 12, FontStyle.Bold)
                ''txtTINDAKLANJUT.Font = New Font("Times New Roman", 9, FontStyle.Regular)
                'txtTINDAKLANJUT.Text = txtTINDAKLANJUT.Text
                .DESCRIPTION = txtTINDAKLANJUT.Text
                .NOIDUSER = sUserID
                .DOKTER = grdKDDOCTOR.Text
                .TUJUAN = "IGD"
                .KDCUSTOMER = txtNoPasien.Text
                .NAMAPASIEN = txtNamaPasien.Text
                .KARTUBPJS = sKARTUBPJS
                .TANGGALLAHIR = deDATETANGGALLAHIR.DateTime
                .KDDOCTOR = grdKDDOCTOR.EditValue
            End With

            'Dim oKoding As New Admission.clsKoding
            ' ***** KODING *****
            Dim dsKodingUtama = oKoding.GetStructureHeaderUtama
            With dsKodingUtama
                .KDKODING = "IGD_" & txtNoRegister.Text
                .KDDIAGNOSA = "-"
                .KETERANGAN = txtKDDIAGNOSA.Text
            End With

            'Dim listTranskasi As New List(Of DataAccess.S_KODING_TERAPI_DIAGNOSISPENYERTA)
            'Dim iSeq As Integer = 0

            'For i As Integer = 0 To grvTindakan.RowCount - 2
            '    Dim dsRekap1 As New DataAccess.S_KODING_TERAPI_DIAGNOSISPENYERTA

            '    iSeq = i

            '    dsRekap1.SEQ = iSeq
            '    dsRekap1.KDKODING = ""
            '    dsRekap1.TERAPI = ""
            '    dsRekap1.TINDAKAN = grvTindakan.GetRowCellValue(i, colPENANGANAN_)

            '    listTranskasi.Add(dsRekap1)

            'Next

            'If listTranskasi.Count > 0 Then
            '    iSeq += 1
            'End If

            'For i As Integer = 0 To grvTindakanPoli.RowCount - 2
            '    Dim dsRekap1 As New DataAccess.S_KODING_TERAPI_DIAGNOSISPENYERTA

            '    dsRekap1.SEQ = iSeq
            '    dsRekap1.KDKODING = ""
            '    dsRekap1.TERAPI = ""
            '    dsRekap1.TINDAKAN = grvTindakanPoli.GetRowCellValue(i, colPENANGANAN)

            '    iSeq += 1

            '    listTranskasi.Add(dsRekap1)
            'Next

            'For Each xloop In listTranskasi
            '    sTindakan += 1
            'Next

            Dim arrDetailDiagnosaPenyertaTindakan = oKoding.GetStructureDetail_Terapi_TindakanList

            If sTerapi >= sTindakan Then
                For Each xloop In arrDetailResume
                    Dim dsDetail = oKoding.GetStructureDetail_Terapi_Tindakan
                    With dsDetail
                        Dim TINDAKAN As String = String.Empty

                        .SEQ = xloop.SEQ
                        .KDKODING = "IGD_" & txtNoRegister.Text
                        .TERAPI = fn_LoadITEM(xloop.KDITEM) & " " & xloop.REMARKS_DOKTER
                        'For y As Integer = 0 To grvTindakan.RowCount - 2
                        '    If xloop.SEQ = y Then
                        '        TINDAKAN = grvTindakan.GetRowCellValue(y, colPENANGANAN_)
                        '        Exit For
                        '    End If
                        'Next
                        For Each xloopT In arrDetailPenunjang
                            If xloop.SEQ = xloopT.SEQ Then
                                TINDAKAN = xloopT.PENANGANAN
                                Exit For
                            End If
                        Next
                        .TINDAKAN = TINDAKAN
                    End With

                    arrDetailDiagnosaPenyertaTindakan.Add(dsDetail)
                Next
            Else
                For Each xloopT In arrDetailPenunjang
                    Dim dsDetail = oKoding.GetStructureDetail_Terapi_Tindakan
                    With dsDetail
                        Dim TERAPI As String = String.Empty

                        .SEQ = xloopT.SEQ
                        .KDKODING = "IGD_" & txtNoRegister.Text

                        For Each xloop In arrDetailResume
                            If xloopT.SEQ = xloop.SEQ Then
                                TERAPI = fn_LoadITEM(xloop.KDITEM) & " " & xloop.REMARKS_DOKTER
                                Exit For
                            End If
                        Next

                        .TERAPI = TERAPI
                        .TINDAKAN = xloopT.PENANGANAN
                    End With
                    arrDetailDiagnosaPenyertaTindakan.Add(dsDetail)
                Next

                'For i As Integer = 0 To grvTindakan.RowCount - 2
                '    Dim dsDetail = oKoding.GetStructureDetail_Terapi_Tindakan
                '    With dsDetail
                '        Dim TERAPI As String = String.Empty

                '        .SEQ = i
                '        .KDKODING = "IGD_" & txtNoRegister.Text

                '        For Each xloop In arrDetailResume
                '            If i = xloop.SEQ Then
                '                TERAPI = fn_LoadITEM(xloop.KDITEM) & " " & xloop.REMARKS_DOKTER
                '                Exit For
                '            End If
                '        Next

                '        .TERAPI = TERAPI
                '        .TINDAKAN = grvTindakan.GetRowCellValue(i, colPENANGANAN_)
                '    End With
                '    arrDetailDiagnosaPenyertaTindakan.Add(dsDetail)
                'Next
            End If

            Dim arrDetailTindakan = oKoding.GetStructureDetail_TindakanList
            For i As Integer = 0 To grvTindakanPoli.RowCount - 2
                Dim dsDetail = oKoding.GetStructureDetail_Tindakan
                With dsDetail
                    .SEQ = i
                    .KDKODING = "IGD_" & txtNoRegister.Text
                    .KETERANGAN = grvTindakanPoli.GetRowCellValue(i, colPENANGANAN)
                End With
                arrDetailTindakan.Add(dsDetail)
            Next

            'DIAGNOSA
            Dim arrDetailDiagnosa = oS_DIGITAL_IGD_01.GetStructureDetailList
            For i As Integer = 0 To grvDIAGNOSA_CPPT.RowCount - 2
                Dim dsDetail = oS_DIGITAL_IGD_01.GetStructureDetail
                With dsDetail
                    .SEQ = i
                    .KDPENDAFTARAN = ds.KDPENDAFTARAN
                    .KATEGORI = grvDIAGNOSA_CPPT.GetRowCellValue(i, colKATEGORI_CPPT)
                    .NAMADIAGNOSA = grvDIAGNOSA_CPPT.GetRowCellValue(i, colNAMADIAGNOSA_CPPT)
                    .KDDIAGNOSA = IIf(String.IsNullOrEmpty(grvDIAGNOSA_CPPT.GetRowCellValue(i, colKDDIAGNOSA_CPPT)), "", grvDIAGNOSA_CPPT.GetRowCellValue(i, colKDDIAGNOSA_CPPT))
                End With
                arrDetailDiagnosa.Add(dsDetail)
            Next

            If oFormMode = FORM_MODE.FORM_MODE_ADD Then
                Try
                    fn_Save = oS_DIGITAL_IGD_01.InsertData(ds, dsResepH, arrDetail, dsTelaah, dsTelaahObat1, dsTelaahObat2, arrDetailTindakanPoli, arrDetailPenunjang, dsKoding, dsKodingUtama, arrDetailDiagnosaPenyertaTindakan, arrDetailTindakan, arrDetailDiagnosa)
                Catch ex As Exception
                    MsgBox("Simpan Data : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            ElseIf oFormMode = FORM_MODE.FORM_MODE_EDIT Then
                Try
                    fn_Save = oS_DIGITAL_IGD_01.UpdateData(ds, dsResepH, arrDetail, arrDetailTindakanPoli, arrDetailPenunjang, dsKoding, dsKodingUtama, arrDetailDiagnosaPenyertaTindakan, arrDetailTindakan, arrDetailDiagnosa)
                Catch ex As Exception
                    MsgBox("Simpan Data : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            End If

            If fn_Save = True Then
                fn_LoadUpdateSudah(ds.KDPENDAFTARAN, txtTINDAKLANJUT.Text)
                If txtRELOAD.Text <> "" Then
                    fn_LoadUpdateBed(ds.KDPENDAFTARAN, txtRELOAD.Text)
                End If
                'fn_UpdateDataReloadIGD(txtRELOAD.Text)
            End If
        Catch oErr As Exception
            MsgBox("Simpan Data : " & vbCrLf & txtNoRegister.Text & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            fn_Save = False
        End Try
    End Function
    Public Sub fn_LoadUpdateSudah(ByVal kdreg As String, ByVal tindaklanjut As String)
        Try
            Dim oConn As New SqlConnection
            Dim oComm As New SqlCommand
            Dim da As SqlDataAdapter
            Dim ds As New DataSet
            Dim SQL As String
            'Dim sConn As String = Decrypt(My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Software\SIMRS\SW\", "Database", "").ToString())

            oConn = New SqlConnection(sConnOld)

            If oConn.State = ConnectionState.Closed Then
                oConn.Open()
            End If

            SQL = "UPDATE "
            SQL &= "S_PENDAFTARAN_H "
            SQL &= "SET ISRESUME = 1 "
            SQL &= ",KETERANGAN_TINDAKLANJUT = '" & tindaklanjut & "' "
            SQL &= "WHERE "
            SQL &= "KDREG = '" & kdreg & "' "

            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "UPDATEPENDAFTARAN")

            If oConn.State = ConnectionState.Open Then
                oConn.Close()
            End If
        Catch oErr As Exception
            MsgBox("Load Obat Data : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Public Sub fn_LoadUpdateBed(ByVal kdreg As String, ByVal KODE As String)
        Try
            Dim oConn As New SqlConnection
            Dim oComm As New SqlCommand
            Dim da As SqlDataAdapter
            Dim ds As New DataSet
            Dim SQL As String
            'Dim sConn As String = Decrypt(My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Software\SIMRS\SW\", "Database", "").ToString())

            oConn = New SqlConnection(sConnOld)

            If oConn.State = ConnectionState.Closed Then
                oConn.Open()
            End If

            SQL = "UPDATE "
            SQL &= "S_PENDAFTARAN_BED "
            SQL &= "SET KET_2 = '" & kdreg & "' "
            SQL &= "WHERE "
            SQL &= "KDREG = '" & KODE & "' "

            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "UPDATES_PENDAFTARAN_BED")

            If oConn.State = ConnectionState.Open Then
                oConn.Close()
            End If
        Catch oErr As Exception
            MsgBox("S_PENDAFTARAN_BED : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
#End Region
#Region "Command Button"
    Private Sub fn_LoadCARAPAKAI()
        Try
            Dim oConn As New SqlConnection
            Dim oComm As New SqlCommand
            Dim da As SqlDataAdapter
            Dim ds As New DataSet
            Dim SQL As String

            oConn = New SqlConnection(sConnOld)

            If oConn.State = ConnectionState.Closed Then
                oConn.Open()
            End If

            SQL = "SELECT "
            SQL &= "A.KDCP "
            SQL &= ",A.DESCRIPTION "
            SQL &= "FROM M_CARAPAKAI A "
            SQL &= "WHERE "
            SQL &= "A.ISACTIVE = 1 "

            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "CARAPAKAI")

            grdCARAPAKAI.DataSource = ds.Tables("CARAPAKAI")
            grdCARAPAKAI.ValueMember = "KDCP"
            grdCARAPAKAI.DisplayMember = "DESCRIPTION"

            grdCARAPAKAI_ObatIGD.DataSource = ds.Tables("CARAPAKAI")
            grdCARAPAKAI_ObatIGD.ValueMember = "KDCP"
            grdCARAPAKAI_ObatIGD.DisplayMember = "DESCRIPTION"

            grdKDCARAPAKAI_ObatRanap.DataSource = ds.Tables("CARAPAKAI")
            grdKDCARAPAKAI_ObatRanap.ValueMember = "KDCP"
            grdKDCARAPAKAI_ObatRanap.DisplayMember = "DESCRIPTION"

            If oConn.State = ConnectionState.Open Then
                oConn.Close()
            End If
        Catch oErr As Exception
            MsgBox("Load Signa Data : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadSIGNA()
        Try
            Dim oConn As New SqlConnection
            Dim oComm As New SqlCommand
            Dim da As SqlDataAdapter
            Dim ds As New DataSet
            Dim SQL As String

            oConn = New SqlConnection(sConnOld)

            If oConn.State = ConnectionState.Closed Then
                oConn.Open()
            End If

            SQL = "SELECT "
            SQL &= "A.KDSIGNA "
            SQL &= ",A.DESCRIPTION "
            SQL &= "FROM M_SIGNA A "
            SQL &= "WHERE "
            SQL &= "A.ISACTIVE = 1 "

            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "SIGNA")

            grdKDSIGNA.DataSource = ds.Tables("SIGNA")
            grdKDSIGNA.ValueMember = "KDSIGNA"
            grdKDSIGNA.DisplayMember = "DESCRIPTION"

            grdKDSIGNA_ObatIGD.DataSource = ds.Tables("SIGNA")
            grdKDSIGNA_ObatIGD.ValueMember = "KDSIGNA"
            grdKDSIGNA_ObatIGD.DisplayMember = "DESCRIPTION"

            grdKDSIGNA_ObatRanap.DataSource = ds.Tables("SIGNA")
            grdKDSIGNA_ObatRanap.ValueMember = "KDSIGNA"
            grdKDSIGNA_ObatRanap.DisplayMember = "DESCRIPTION"

            If oConn.State = ConnectionState.Open Then
                oConn.Close()
            End If
        Catch oErr As Exception
            MsgBox("Load Signa Data : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadITEM()
        Try
            Dim oConn As New SqlConnection
            Dim oComm As New SqlCommand
            Dim da As SqlDataAdapter
            Dim ds As New DataSet
            Dim SQL As String
            'Dim sConn As String = Decrypt(My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Software\SIMRS\SW\", "Database", "").ToString())

            oConn = New SqlConnection(sConnOld)

            If oConn.State = ConnectionState.Closed Then
                oConn.Open()
            End If

            SQL = "SELECT "
            SQL &= "* "
            SQL &= "FROM ( "
            SQL &= "SELECT "
            SQL &= "A.KDITEM "
            SQL &= ",A.NMITEM1 "
            SQL &= ",A.NMITEM2 "
            SQL &= ",HARGA = B.PRICEPURCHASESTANDARD "
            SQL &= ",SATUAN = C.DESCRIPTION "
            SQL &= ",STOK = ISNULL((SELECT SUM(AMOUNT) FROM M_ITEM_WAREHOUSE WHERE B.KDITEM = KDITEM AND B.KDUOM = KDUOM GROUP BY KDITEM) ,0) "
            SQL &= "FROM M_ITEM A "
            SQL &= "INNER JOIN M_ITEM_UOM B "
            SQL &= "ON A.KDITEM = B.KDITEM "
            SQL &= "INNER JOIN M_UOM C "
            SQL &= "ON B.KDUOM = C.KDUOM "
            SQL &= "WHERE "
            SQL &= "A.ISACTIVE = 1 "
            SQL &= "AND B.RATE = 1 "

            SQL &= "UNION "

            SQL &= "SELECT "
            SQL &= "KDITEM = 999999 "
            SQL &= ",NMITEM1 = A.DESCRIPTION "
            SQL &= ",NMITEM2 = A.DESCRIPTION "
            SQL &= ",HARGA = 0 "
            SQL &= ",SATUAN = '-' "
            SQL &= ",STOK = 0 "
            SQL &= "FROM M_ITEM_RACIK A "
            SQL &= ") X "
            SQL &= "ORDER BY "
            SQL &= "X.HARGA "

            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "ITEM")

            grdKDITEM.DataSource = ds.Tables("ITEM")
            grdKDITEM.ValueMember = "KDITEM"
            grdKDITEM.DisplayMember = "NMITEM2"

            grdKDITEM_ObatIGD.DataSource = ds.Tables("ITEM")
            grdKDITEM_ObatIGD.ValueMember = "KDITEM"
            grdKDITEM_ObatIGD.DisplayMember = "NMITEM2"

            grdKDITEM_ObatRanap.DataSource = ds.Tables("ITEM")
            grdKDITEM_ObatRanap.ValueMember = "KDITEM"
            grdKDITEM_ObatRanap.DisplayMember = "NMITEM2"

            If oConn.State = ConnectionState.Open Then
                oConn.Close()
            End If
        Catch oErr As Exception
            MsgBox("Load Obat Data : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadUOM()
        Try
            Dim oConn As New SqlConnection
            Dim oComm As New SqlCommand
            Dim da As SqlDataAdapter
            Dim ds As New DataSet
            Dim SQL As String

            oConn = New SqlConnection(sConnOld)

            If oConn.State = ConnectionState.Closed Then
                oConn.Open()
            End If

            SQL = "SELECT "
            SQL &= "A.KDUOM "
            SQL &= ",A.DESCRIPTION "
            SQL &= "FROM M_UOM A "
            SQL &= "WHERE "
            SQL &= "A.ISACTIVE = 1 "

            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "UOM")

            grdUOM.DataSource = ds.Tables("UOM")
            grdUOM.ValueMember = "KDUOM"
            grdUOM.DisplayMember = "DESCRIPTION"

            grdKDUOM_ObatIGD.DataSource = ds.Tables("UOM")
            grdKDUOM_ObatIGD.ValueMember = "KDUOM"
            grdKDUOM_ObatIGD.DisplayMember = "DESCRIPTION"

            grdKDUOM_ObatRanap.DataSource = ds.Tables("UOM")
            grdKDUOM_ObatRanap.ValueMember = "KDUOM"
            grdKDUOM_ObatRanap.DisplayMember = "DESCRIPTION"

            If oConn.State = ConnectionState.Open Then
                oConn.Close()
            End If
        Catch oErr As Exception
            MsgBox("Load Satuan Data : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Function IntegerToRoman(IntNumberValue As Integer) As String
        Dim RomanNumbers As New Dictionary(Of String, Integer)()
        RomanNumbers.Add("M", 1000)
        RomanNumbers.Add("CM", 900)
        RomanNumbers.Add("D", 500)
        RomanNumbers.Add("CD", 400)
        RomanNumbers.Add("C", 100)
        RomanNumbers.Add("XC", 90)
        RomanNumbers.Add("L", 50)
        RomanNumbers.Add("XL", 40)
        RomanNumbers.Add("X", 10)
        RomanNumbers.Add("IX", 9)
        RomanNumbers.Add("V", 5)
        RomanNumbers.Add("IV", 4)
        RomanNumbers.Add("I", 1)

        Dim result As String = ""

        For Each pair As KeyValuePair(Of String, Integer) In RomanNumbers
            While IntNumberValue >= pair.Value
                IntNumberValue -= pair.Value
                result += pair.Key
            End While
        Next
        Return result
    End Function
    Public Function fn_LoadSIGNA(ByVal KDSIGNA As String) As String
        Try
            fn_LoadSIGNA = ""

            Dim oConn As New SqlConnection
            Dim oComm As New SqlCommand
            Dim da As SqlDataAdapter
            Dim ds As New DataSet
            Dim SQL As String
            oConn = New SqlConnection(sConnOld)

            If oConn.State = ConnectionState.Closed Then
                oConn.Open()
            End If

            SQL = "SELECT "
            SQL &= "* "
            SQL &= "FROM "
            SQL &= "M_SIGNA A "
            SQL &= "WHERE "
            SQL &= "A.KDSIGNA = '" & KDSIGNA & "' "

            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "SEARCHSIGNA")

            For iLoop As Integer = 0 To ds.Tables("SEARCHSIGNA").Rows.Count - 1
                With ds.Tables("SEARCHSIGNA")
                    fn_LoadSIGNA = .Rows(iLoop)("DESCRIPTION")
                End With
            Next

            If oConn.State = ConnectionState.Open Then
                oConn.Close()
            End If
        Catch oErr As Exception
            fn_LoadSIGNA = ""
            MsgBox("Load Signa" & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Function
    Public Function fn_LoadUOMDESCRIPTION(ByVal KDUOM As String) As String
        Try
            fn_LoadUOMDESCRIPTION = ""

            Dim oConn As New SqlConnection
            Dim oComm As New SqlCommand
            Dim da As SqlDataAdapter
            Dim ds As New DataSet
            Dim SQL As String
            oConn = New SqlConnection(sConnOld)

            If oConn.State = ConnectionState.Closed Then
                oConn.Open()
            End If

            SQL = "SELECT "
            SQL &= "* "
            SQL &= "FROM "
            SQL &= "M_UOM A "
            SQL &= "WHERE "
            SQL &= "A.KDUOM = '" & KDUOM & "' "

            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "SEARCHUOM")

            For iLoop As Integer = 0 To ds.Tables("SEARCHUOM").Rows.Count - 1
                With ds.Tables("SEARCHUOM")
                    fn_LoadUOMDESCRIPTION = .Rows(iLoop)("DESCRIPTION")
                End With
            Next

            If oConn.State = ConnectionState.Open Then
                oConn.Close()
            End If
        Catch oErr As Exception
            fn_LoadUOMDESCRIPTION = ""
            MsgBox("Load Satuan" & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Function
    Public Function fn_LoadITEM(ByVal KDITEM As String) As String
        Try
            fn_LoadITEM = ""

            Dim oConn As New SqlConnection
            Dim oComm As New SqlCommand
            Dim da As SqlDataAdapter
            Dim ds As New DataSet
            Dim SQL As String
            oConn = New SqlConnection(sConnOld)

            If oConn.State = ConnectionState.Closed Then
                oConn.Open()
            End If

            SQL = "SELECT "
            SQL &= "* "
            SQL &= "FROM "
            SQL &= "M_ITEM A "
            SQL &= "WHERE "
            SQL &= "A.KDITEM = '" & KDITEM & "' "

            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "SEARCHITEM")

            For iLoop As Integer = 0 To ds.Tables("SEARCHITEM").Rows.Count - 1
                With ds.Tables("SEARCHITEM")
                    fn_LoadITEM = .Rows(iLoop)("NMITEM2")
                End With
            Next

            If oConn.State = ConnectionState.Open Then
                oConn.Close()
            End If
        Catch oErr As Exception
            fn_LoadITEM = ""
            MsgBox("Load Item" & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Function
    Private Function fn_LoadUOMKDUOM(ByVal KDITEM As String) As String
        Try
            fn_LoadUOMKDUOM = "-"

            Dim oConn As New SqlConnection
            Dim oComm As New SqlCommand
            Dim da As SqlDataAdapter
            Dim ds As New DataSet
            Dim SQL As String
            oConn = New SqlConnection(sConnOld)

            If oConn.State = ConnectionState.Closed Then
                oConn.Open()
            End If

            SQL = "SELECT "
            SQL &= "TOP 1 * "
            SQL &= "FROM "
            SQL &= "M_ITEM_UOM A "
            SQL &= "WHERE "
            SQL &= "A.KDITEM = '" & KDITEM & "' "
            SQL &= "AND A.RATE = 1 "

            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "SEARCHKDUOM")

            For iLoop As Integer = 0 To ds.Tables("SEARCHKDUOM").Rows.Count - 1
                With ds.Tables("SEARCHKDUOM")
                    fn_LoadUOMKDUOM = .Rows(iLoop)("KDUOM")
                End With
            Next

            If oConn.State = ConnectionState.Open Then
                oConn.Close()
            End If
        Catch oErr As Exception
            fn_LoadUOMKDUOM = ""
            MsgBox("Load Satuan" & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Function
    Private Function fn_LoadUOMKDUOMHARGA(ByVal KDITEM As String, ByVal kduom As String) As Decimal
        Try
            fn_LoadUOMKDUOMHARGA = 0

            Dim oConn As New SqlConnection
            Dim oComm As New SqlCommand
            Dim da As SqlDataAdapter
            Dim ds As New DataSet
            Dim SQL As String
            oConn = New SqlConnection(sConnOld)

            If oConn.State = ConnectionState.Closed Then
                oConn.Open()
            End If

            SQL = "SELECT "
            SQL &= "TOP 1 * "
            SQL &= "FROM "
            SQL &= "M_ITEM_UOM A "
            SQL &= "WHERE "
            SQL &= "A.KDITEM = '" & KDITEM & "' "
            SQL &= "AND A.KDUOM = '" & kduom & "' "
            SQL &= "AND A.RATE = 1 "

            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "SEARCHKDUOMHARGA")

            For iLoop As Integer = 0 To ds.Tables("SEARCHKDUOMHARGA").Rows.Count - 1
                With ds.Tables("SEARCHKDUOMHARGA")
                    fn_LoadUOMKDUOMHARGA = .Rows(iLoop)("PRICESALESSTANDARD")
                End With
            Next

            If oConn.State = ConnectionState.Open Then
                oConn.Close()
            End If
        Catch oErr As Exception
            fn_LoadUOMKDUOMHARGA = 0
            MsgBox("Load Satuan" & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Function
    Public Function fn_LoadCARAPAKAI(ByVal KDCP As String) As String
        Try
            fn_LoadCARAPAKAI = ""

            Dim oConn As New SqlConnection
            Dim oComm As New SqlCommand
            Dim da As SqlDataAdapter
            Dim ds As New DataSet
            Dim SQL As String
            oConn = New SqlConnection(sConnOld)

            If oConn.State = ConnectionState.Closed Then
                oConn.Open()
            End If

            SQL = "SELECT "
            SQL &= "* "
            SQL &= "FROM "
            SQL &= "M_CARAPAKAI A "
            SQL &= "WHERE "
            SQL &= "A.KDCP = '" & KDCP & "' "

            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "SEARCHCARAPAKAI")

            For iLoop As Integer = 0 To ds.Tables("SEARCHCARAPAKAI").Rows.Count - 1
                With ds.Tables("SEARCHCARAPAKAI")
                    fn_LoadCARAPAKAI = .Rows(iLoop)("DESCRIPTION")
                End With
            Next

            If oConn.State = ConnectionState.Open Then
                oConn.Close()
            End If
        Catch oErr As Exception
            fn_LoadCARAPAKAI = ""
            MsgBox("Load Cara Pakai" & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Function
    Private Sub OnValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles grvDetailResep.FocusedRowChanged, grvObatIGD.FocusedRowChanged, grvObatRanap.FocusedRowChanged
        If isLoad Then
            Calculate()
        End If
    End Sub
    Private Sub Calculate()
        Dim sSubTotal = 0
        Dim list As New List(Of String)

        For i As Integer = 0 To grvDetailResep.RowCount - 2
            sSubTotal += CDec(grvDetailResep.GetRowCellValue(i, colGRANDTOTAL))
        Next
        For i As Integer = 0 To grvObatIGD.RowCount - 2
            sSubTotal += CDec(grvObatIGD.GetRowCellValue(i, colGRANDTOTAL_ObatIGD))
        Next
        For i As Integer = 0 To grvObatRanap.RowCount - 2
            sSubTotal += CDec(grvObatRanap.GetRowCellValue(i, colGRANDTOTAL_ObatRanap))
        Next

        txtGRANDTOTAL.Text = sSubTotal
        'txtOBATSAATPULANG.Text = String.Join(", ", list.ToArray)
    End Sub
    Private Sub grvDetailResep_CellValueChanged(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs) Handles grvDetailResep.CellValueChanged
        If e.Column.Name = colKDITEM.Name Then
            Try
                If grvDetailResep.GetFocusedRowCellValue(colKDITEM) IsNot Nothing Then
                    grvDetailResep.SetFocusedRowCellValue(colKDUOM, fn_LoadUOMKDUOM(grvDetailResep.GetFocusedRowCellValue(colKDITEM)))
                    grvDetailResep.SetFocusedRowCellValue(colREMARKS_DOKTER, "")
                    grvDetailResep.SetFocusedRowCellValue(colQTY, 0)
                End If
            Catch oErr As Exception
                MsgBox("Load Detail : " & vbCrLf & oErr.Message, MsgBoxStyle.Critical, Me.Text)
            End Try
        ElseIf e.Column.Name = colREMARKS_DOKTER.Name Then
            'grvTindakan.Focus()
            'grvTindakan.AddNewRow()
            'grvTindakan.SetFocusedRowCellValue(colTERAPI, fn_LoadITEM(grvDetailResep.GetFocusedRowCellValue(colKDITEM)) & " " & fn_LoadSIGNA(grvDetailResep.GetFocusedRowCellValue(colKDSIGNA)))
            'grvTindakan.SetFocusedRowCellValue(colTINDAKAN, "")
            'grvTindakan.UpdateCurrentRow()
        ElseIf e.Column.Name = colKDUOM.Name Then
            grvDetailResep.SetFocusedRowCellValue(colPRICE, fn_LoadUOMKDUOMHARGA(grvDetailResep.GetFocusedRowCellValue(colKDITEM), grvDetailResep.GetFocusedRowCellValue(colKDUOM)))
        ElseIf e.Column.Name = colQTY.Name Or e.Column.Name = colPRICE.Name Then
            Dim sSubTotal As Decimal = CDec(grvDetailResep.GetFocusedRowCellValue(colQTY)) * CDec(grvDetailResep.GetFocusedRowCellValue(colPRICE))
            grvDetailResep.SetFocusedRowCellValue(colGRANDTOTAL, sSubTotal)
        End If
    End Sub
    Private Sub grvObatIGD_CellValueChanged(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs) Handles grvObatIGD.CellValueChanged
        If e.Column.Name = colKDITEM_ObatIGD.Name Then
            Try
                If grvObatIGD.GetFocusedRowCellValue(colKDITEM_ObatIGD) IsNot Nothing Then
                    grvObatIGD.SetFocusedRowCellValue(colKDUOM, fn_LoadUOMKDUOM(grvObatIGD.GetFocusedRowCellValue(colKDITEM_ObatIGD)))
                    grvObatIGD.SetFocusedRowCellValue(colREMARKS_DOKTER_ObatIGD, "")
                    grvObatIGD.SetFocusedRowCellValue(colQTY_ObatIGD, 0)
                End If
            Catch oErr As Exception
                MsgBox("Load Detail : " & vbCrLf & oErr.Message, MsgBoxStyle.Critical, Me.Text)
            End Try
        ElseIf e.Column.Name = colREMARKS_DOKTER_ObatIGD.Name Then
            'grvTindakan.Focus()
            'grvTindakan.AddNewRow()
            'grvTindakan.SetFocusedRowCellValue(colTERAPI, fn_LoadITEM(grvObatIGD.GetFocusedRowCellValue(colKDITEM_ObatIGD)) & " " & fn_LoadSIGNA(grvObatIGD.GetFocusedRowCellValue(colKDSIGNA)))
            'grvTindakan.SetFocusedRowCellValue(colTINDAKAN, "")
            'grvTindakan.UpdateCurrentRow()
        ElseIf e.Column.Name = colKDUOM_ObatIGD.Name Then
            grvObatIGD.SetFocusedRowCellValue(colPRICE_ObatIGD, fn_LoadUOMKDUOMHARGA(grvObatIGD.GetFocusedRowCellValue(colKDITEM_ObatIGD), grvObatIGD.GetFocusedRowCellValue(colKDUOM_ObatIGD)))
        ElseIf e.Column.Name = colQTY_ObatIGD.Name Or e.Column.Name = colPRICE_ObatIGD.Name Then
            Dim sSubTotal As Decimal = CDec(grvObatIGD.GetFocusedRowCellValue(colQTY_ObatIGD)) * CDec(grvObatIGD.GetFocusedRowCellValue(colPRICE_ObatIGD))
            grvObatIGD.SetFocusedRowCellValue(colGRANDTOTAL_ObatIGD, sSubTotal)
        End If
    End Sub
    Private Sub grvObatRanap_CellValueChanged(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs) Handles grvObatRanap.CellValueChanged
        If e.Column.Name = colKDITEM_ObatRanap.Name Then
            Try
                If grvObatRanap.GetFocusedRowCellValue(colKDITEM_ObatRanap) IsNot Nothing Then
                    grvObatRanap.SetFocusedRowCellValue(colKDUOM, fn_LoadUOMKDUOM(grvObatRanap.GetFocusedRowCellValue(colKDITEM_ObatRanap)))
                    grvObatRanap.SetFocusedRowCellValue(colREMARKS_DOKTER_ObatRanap, "")
                    grvObatRanap.SetFocusedRowCellValue(colQTY_ObatRanap, 0)
                End If
            Catch oErr As Exception
                MsgBox("Load Detail : " & vbCrLf & oErr.Message, MsgBoxStyle.Critical, Me.Text)
            End Try
        ElseIf e.Column.Name = colREMARKS_DOKTER_ObatRanap.Name Then
            'grvTindakan.Focus()
            'grvTindakan.AddNewRow()
            'grvTindakan.SetFocusedRowCellValue(colTERAPI, fn_LoadITEM(grvObatRanap.GetFocusedRowCellValue(colKDITEM_ObatIGD)) & " " & fn_LoadSIGNA(grvObatRanap.GetFocusedRowCellValue(colKDSIGNA)))
            'grvTindakan.SetFocusedRowCellValue(colTINDAKAN, "")
            'grvTindakan.UpdateCurrentRow()
        ElseIf e.Column.Name = colKDUOM_ObatRanap.Name Then
            grvObatRanap.SetFocusedRowCellValue(colPRICE_ObatRanap, fn_LoadUOMKDUOMHARGA(grvObatRanap.GetFocusedRowCellValue(colKDITEM_ObatRanap), grvObatRanap.GetFocusedRowCellValue(colKDUOM_ObatRanap)))
        ElseIf e.Column.Name = colQTY_ObatRanap.Name Or e.Column.Name = colPRICE_ObatRanap.Name Then
            Dim sSubTotal As Decimal = CDec(grvObatRanap.GetFocusedRowCellValue(colQTY_ObatRanap)) * CDec(grvObatRanap.GetFocusedRowCellValue(colPRICE_ObatRanap))
            grvObatRanap.SetFocusedRowCellValue(colGRANDTOTAL_ObatRanap, sSubTotal)
        End If
    End Sub
    Private Sub frmItem_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        Select Case e.KeyCode
            Case Keys.F12
                btnClose_Click()
            Case Keys.F3
                If btnSaveClose.Enabled = True Then
                    btnSaveClose_Click()
                End If
        End Select
    End Sub
    Private Sub btnSaveClose_Click() Handles btnSaveClose.ItemClick
        If fn_Validate() = False Then Exit Sub
        If txtTINDAKLANJUT.Text = "" Then
            If MsgBox("Save Tanpa TINDAK LANJUT dengan Register " & txtNoRegister.Text & "?", MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, Me.Text) = MsgBoxResult.No Then Exit Sub
        Else
            If MsgBox("Save " & txtNoRegister.Text & "?", MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, Me.Text) = MsgBoxResult.No Then Exit Sub
        End If
        If fn_Save() = False Then
            MsgBox("Save gagal! Tolong kontak admin anda!", MsgBoxStyle.Exclamation, Me.Text)
        Else
            MsgBox("Save " & txtNoRegister.Text & " success!", MsgBoxStyle.Information, Me.Text)
            Me.Close()
        End If
    End Sub
    Private Sub btnClose_Click() Handles btnClose.ItemClick
        Me.Close()
    End Sub
    'Private Sub chkTINDAKLANJUT_RUJUK_CheckedChanged(sender As Object, e As EventArgs) Handles chkTINDAKLANJUT_RUJUK.CheckedChanged
    '    If chkTINDAKLANJUT_RUJUK.Checked = True Then
    '        If txtNoRegister.Text Is Nothing Then
    '            MsgBox("Register Kosong", MsgBoxStyle.Exclamation, Me.Text)
    '            Exit Sub
    '        End If

    '        Dim oSKD As New Admission.clsSKD
    '        Dim ds = oSKD.GetDataPendaftaranalasan(txtNoRegister.Text, "RUJUK")

    '        If ds IsNot Nothing Then
    '            Dim frmSKD As New frmSKD
    '            Try
    '                frmSKD.fn_LoadKategori(1)
    '                frmSKD.LoadMe(FORM_MODE.FORM_MODE_EDIT, ds.KDSKD)
    '                frmSKD.ShowDialog(Me)
    '            Catch ex As Exception
    '                MsgBox("Load Form Detail : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
    '            Finally
    '                If Not frmSKD Is Nothing Then frmSKD.Dispose()
    '                frmSKD = Nothing
    '            End Try
    '        Else
    '            Dim frmSKD As New frmSKD
    '            Try
    '                frmSKD.fn_LoadNoPendaftaran(txtNoRegister.Text, sKDCUSTOMER, sKDDEPARTMENT, sKDDOCTOR, sNOMORSEP, sPENJAMIN, sNAMAPASIEN)
    '                frmSKD.fn_LoadKategori(1)
    '                frmSKD.LoadMe(FORM_MODE.FORM_MODE_ADD)
    '                frmSKD.ShowDialog(Me)
    '            Catch ex As Exception
    '                MsgBox("Load Form Detail : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
    '            End Try
    '        End If

    '        fn_LoadTindakLanjut(txtNoRegister.Text, "")
    '    End If
    'End Sub
    'Private Sub chkTINDAKLANJUT_RAWAT_CheckedChanged(sender As Object, e As EventArgs) Handles chkTINDAKLANJUT_RAWAT.CheckedChanged
    '    If chkTINDAKLANJUT_RAWAT.Checked = True Then
    '        If txtNoRegister.Text Is Nothing Then
    '            MsgBox("Register Kosong", MsgBoxStyle.Exclamation, Me.Text)
    '            Exit Sub
    '        End If

    '        If MsgBox("Apakah Rawat Inap?", MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, Me.Text) = MsgBoxResult.No Then Exit Sub

    '        Dim oSKD As New Admission.clsSKD
    '        Dim ds = oSKD.GetDataPendaftaranalasan(txtNoRegister.Text, "RAWAT INAP")

    '        If ds IsNot Nothing Then
    '            fn_SaveSKD(False, ds.KDSKD, "RAWAT INAP")
    '        Else
    '            fn_SaveSKD(True, "", "RAWAT INAP")
    '        End If

    '        Dim frmRemarks As New frmRemarks
    '        Try
    '            sRemarks = ""
    '            frmRemarks.ShowDialog(Me)
    '        Catch ex As Exception
    '            MsgBox("Load Form Detail : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
    '        End Try

    '        fn_LoadTindakLanjut(txtNoRegister.Text, sRemarks)

    '    End If
    'End Sub
    'Private Sub chkTINDAKLANJUT_PULANGPAKSA_CheckedChanged(sender As Object, e As EventArgs) Handles chkTINDAKLANJUT_PULANGPAKSA.CheckedChanged
    '    If chkTINDAKLANJUT_PULANGPAKSA.Checked = True Then
    '        If txtNoRegister.Text Is Nothing Then
    '            MsgBox("Register Kosong", MsgBoxStyle.Exclamation, Me.Text)
    '            Exit Sub
    '        End If

    '        'If MsgBox("Apakah Selesai Pengobatan?", MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, Me.Text) = MsgBoxResult.No Then Exit Sub

    '        Dim oSKD As New Admission.clsSKD
    '        Dim ds = oSKD.GetDataPendaftaranalasan(txtNoRegister.Text, "SELESAI PENGOBATAN")

    '        If ds IsNot Nothing Then
    '            fn_SaveSKD(False, ds.KDSKD, "SELESAI PENGOBATAN")
    '        Else
    '            fn_SaveSKD(True, "", "SELESAI PENGOBATAN")
    '        End If

    '        fn_LoadTindakLanjut(txtNoRegister.Text, "")

    '    End If
    'End Sub
    Private Sub chkTRIAGE_TRAUMA_CheckedChanged(sender As Object, e As EventArgs) Handles chkTRIAGE_TRAUMA.CheckedChanged
        If chkTRIAGE_TRAUMA.Checked = True Then
            chkTRIAGE_NONTRAUMA.Checked = False
            chkTRIAGE_MATERNITY.Checked = False
        End If
    End Sub
    Private Sub chkTRIAGE_NONTRAUMA_CheckedChanged(sender As Object, e As EventArgs) Handles chkTRIAGE_NONTRAUMA.CheckedChanged
        If chkTRIAGE_NONTRAUMA.Checked = True Then
            chkTRIAGE_TRAUMA.Checked = False
            chkTRIAGE_MATERNITY.Checked = False
        End If
    End Sub
    Private Sub chkTRIAGE_MATERNITY_CheckedChanged(sender As Object, e As EventArgs) Handles chkTRIAGE_MATERNITY.CheckedChanged
        If chkTRIAGE_MATERNITY.Checked = True Then
            chkTRIAGE_TRAUMA.Checked = False
            chkTRIAGE_NONTRAUMA.Checked = False
        End If
    End Sub
    Private Sub chkTRIAGE_RED_CheckedChanged(sender As Object, e As EventArgs) Handles chkTRIAGE_RED.CheckedChanged
        If chkTRIAGE_RED.Checked = True Then
            chkTRIAGE_YELLOW.Checked = False
            chkTRIAGE_GREEN.Checked = False
            chkTRIAGE_BLACK.Checked = False
        End If
    End Sub
    Private Sub chkTRIAGE_YELLOW_CheckedChanged(sender As Object, e As EventArgs) Handles chkTRIAGE_YELLOW.CheckedChanged
        If chkTRIAGE_YELLOW.Checked = True Then
            chkTRIAGE_RED.Checked = False
            chkTRIAGE_GREEN.Checked = False
            chkTRIAGE_BLACK.Checked = False
        End If
    End Sub
    Private Sub chkTRIAGE_GREEN_CheckedChanged(sender As Object, e As EventArgs) Handles chkTRIAGE_GREEN.CheckedChanged
        If chkTRIAGE_GREEN.Checked = True Then
            chkTRIAGE_RED.Checked = False
            chkTRIAGE_YELLOW.Checked = False
            chkTRIAGE_BLACK.Checked = False
        End If
    End Sub
    Private Sub chkTRIAGE_BLACK_CheckedChanged(sender As Object, e As EventArgs) Handles chkTRIAGE_BLACK.CheckedChanged
        If chkTRIAGE_BLACK.Checked = True Then
            chkTRIAGE_RED.Checked = False
            chkTRIAGE_YELLOW.Checked = False
            chkTRIAGE_GREEN.Checked = False
        End If
    End Sub
    Private Sub chkTRIAGE_DATANGSENDIRI_CheckedChanged(sender As Object, e As EventArgs) Handles chkTRIAGE_DATANGSENDIRI.CheckedChanged
        If chkTRIAGE_DATANGSENDIRI.Checked = True Then
            chkTRIAGE_DIANTAR.Checked = False
            cboTRIAGE_DIANTAR.SelectedIndex = 0
        End If
    End Sub
    Private Sub chkTRIAGE_DIANTAR_CheckedChanged(sender As Object, e As EventArgs) Handles chkTRIAGE_DIANTAR.CheckedChanged
        If chkTRIAGE_DIANTAR.Checked = True Then
            chkTRIAGE_DATANGSENDIRI.Checked = False
        End If
    End Sub
    Private Sub chkAUTOANAMNESA_CheckedChanged(sender As Object, e As EventArgs) Handles chkAUTOANAMNESA.CheckedChanged
        If chkAUTOANAMNESA.Checked = True Then
            chkALLOANAMNESA.Checked = False
        End If
    End Sub
    Private Sub chkALLOANAMNESA_CheckedChanged(sender As Object, e As EventArgs) Handles chkALLOANAMNESA.CheckedChanged
        If chkALLOANAMNESA.Checked = True Then
            chkAUTOANAMNESA.Checked = False
        End If
    End Sub
    Private Sub chkSKALANYERI_01_Click(sender As Object, e As EventArgs) Handles chkSKALANYERI_00.Click, chkSKALANYERI_01.Click, chkSKALANYERI_02.Click, chkSKALANYERI_03.Click, chkSKALANYERI_04.Click, chkSKALANYERI_05.Click, chkSKALANYERI_06.Click, chkSKALANYERI_07.Click, chkSKALANYERI_08.Click, chkSKALANYERI_09.Click, chkSKALANYERI_10.Click
        chkSKALANYERI_00.Checked = False
        chkSKALANYERI_01.Checked = False
        chkSKALANYERI_02.Checked = False
        chkSKALANYERI_03.Checked = False
        chkSKALANYERI_04.Checked = False
        chkSKALANYERI_05.Checked = False
        chkSKALANYERI_06.Checked = False
        chkSKALANYERI_07.Checked = False
        chkSKALANYERI_08.Checked = False
        chkSKALANYERI_09.Checked = False
        chkSKALANYERI_10.Checked = False
    End Sub
    Private Sub chkRISIKO_Click(sender As Object, e As EventArgs) Handles chkRESIKO_1.Click, chkRESIKO_2.Click
        chkRESIKO_1.Checked = False
        chkRESIKO_2.Checked = False
    End Sub
    Private Sub chkFUNGSIONAL_Click(sender As Object, e As EventArgs) Handles chkFUNGSIONAL_1.Click, chkFUNGSIONAL_2.Click, chkFUNGSIONAL_3.Click
        chkFUNGSIONAL_1.Checked = False
        chkFUNGSIONAL_2.Checked = False
        chkFUNGSIONAL_3.Checked = False
    End Sub
    Private Sub picGAMBAR2_MouseDown(sender As Object, e As MouseEventArgs) Handles picGAMBAR2.MouseDown
        down = True
    End Sub
    Private Sub picGAMBAR2_MouseUp(sender As Object, e As MouseEventArgs) Handles picGAMBAR2.MouseUp
        down = False
    End Sub
    Private Sub picGAMBAR2_MouseMove(sender As Object, e As MouseEventArgs) Handles picGAMBAR2.MouseMove
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
        picGAMBAR2.Image = CType(My.Resources.ResourceManager.GetObject("gambar_2"), Image)
    End Sub
#End Region
#Region "Lookup / Event"
    Private Sub grvDIAGNOSA_CPPT_CellValueChanged(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs) Handles grvDIAGNOSA_CPPT.CellValueChanged
        If e.Column.Name = colNAMADIAGNOSA_CPPT.Name Then
            Dim CEK As Boolean = False

            For i As Integer = 0 To grvDIAGNOSA_CPPT.RowCount - 2
                If grvDIAGNOSA_CPPT.GetRowCellValue(i, colKATEGORI_CPPT) = "Primer" Then
                    CEK = True
                End If
            Next

            If CEK = False Then
                grvDIAGNOSA_CPPT.SetFocusedRowCellValue(colKATEGORI_CPPT, "Primer")
            Else
                grvDIAGNOSA_CPPT.SetFocusedRowCellValue(colKATEGORI_CPPT, "Sekunder")
            End If
        End If
    End Sub
    'Private Sub fn_NOIDUSER()
    '    Dim oUser As New Setting.clsUser
    '    Try
    '        grdUSER.DataSource = oUser.GetData.Where(Function(x) x.ISACTIVE = True).ToList()
    '        grdUSER.ValueMember = "KDUSER"
    '        grdUSER.DisplayMember = "KDUSER"
    '    Catch oErr As Exception
    '        MsgBox("Load User Data : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
    '    End Try
    'End Sub
    Private Sub fn_LoadKDDOCTOR()
        'Dim oDoctor As New Reference.clsDoctor
        'Try
        '    grdKDDOCTOR.Properties.DataSource = oDoctor.GetData.Where(Function(x) x.ISACTIVE = True).ToList()
        '    grdKDDOCTOR.Properties.ValueMember = "KDDOCTOR"
        '    grdKDDOCTOR.Properties.DisplayMember = "NAME_DISPLAY"

        'Catch oErr As Exception
        '    MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        'End Try
        Try
            Dim oConn As New SqlConnection
            Dim oComm As New SqlCommand
            Dim da As SqlDataAdapter
            Dim ds As New DataSet
            Dim SQL As String
            'Dim sConn As String = Decrypt(My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Software\SIMRS\SW\", "Database", "").ToString())

            oConn = New SqlConnection(sConnOld)

            If oConn.State = ConnectionState.Closed Then
                oConn.Open()
            End If

            SQL = "SELECT "
            SQL &= "* "
            SQL &= "FROM  "
            SQL &= "M_DOCTOR "
            SQL &= "WHERE "
            SQL &= "ISACTIVE = '1' "

            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "M_DOCTOR")

            grdKDDOCTOR.Properties.DataSource = ds.Tables("M_DOCTOR")
            grdKDDOCTOR.Properties.ValueMember = "KDDOCTOR"
            grdKDDOCTOR.Properties.DisplayMember = "NAME_DISPLAY"

            If oConn.State = ConnectionState.Open Then
                oConn.Close()
            End If
        Catch oErr As Exception
            MsgBox("Load Obat Data : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub cboE_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboE.SelectedIndexChanged, cboM.SelectedIndexChanged, cboV.SelectedIndexChanged
        If isLoad = True Then
            Dim sE As Decimal = 0
            Dim sM As Decimal = 0
            Dim sV As Decimal = 0

            If cboE.Text = "-" Or cboM.Text = "-" Or cboV.Text = "-" Then
                txtGCS.Text = "-"
            Else
                If cboE.Text <> String.Empty Then
                    sE = CDec(cboE.Text)
                End If

                If cboM.Text <> String.Empty Then
                    sM = CDec(cboM.Text)
                End If

                If cboV.Text <> String.Empty Then
                    sV = CDec(cboV.Text)
                End If

                txtGCS.Text = sE + sM + sV
            End If
        End If
    End Sub
    Private Sub cboSAATPULANG_E_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboSAATPULANG_E.SelectedIndexChanged, cboSAATPULANG_M.SelectedIndexChanged, cboSAATPULANG_V.SelectedIndexChanged
        If isLoad = True Then
            Dim sE As Decimal = 0
            Dim sM As Decimal = 0
            Dim sV As Decimal = 0

            If cboSAATPULANG_E.Text = "-" Or cboSAATPULANG_M.Text = "-" Or cboSAATPULANG_V.Text = "-" Then
                txtSAATPULANG_GCS.Text = "-"
            Else
                If cboSAATPULANG_E.Text <> String.Empty Then
                    sE = CDec(cboSAATPULANG_E.Text)
                End If

                If cboSAATPULANG_M.Text <> String.Empty Then
                    sM = CDec(cboSAATPULANG_M.Text)
                End If

                If cboSAATPULANG_V.Text <> String.Empty Then
                    sV = CDec(cboSAATPULANG_V.Text)
                End If

                txtSAATPULANG_GCS.Text = sE + sM + sV
            End If

        End If
    End Sub
    Private Sub chkNORMAL_KEPALA_CheckedChanged(sender As Object, e As EventArgs) Handles chkNORMAL_KEPALA.Click, chkTIDAK_NORMAL_KEPALA.Click
        chkNORMAL_KEPALA.Checked = False
        chkTIDAK_NORMAL_KEPALA.Checked = False
    End Sub
    Private Sub chkNORMAL_MATA_CheckedChanged(sender As Object, e As EventArgs) Handles chkNORMAL_MATA.Click, chkTIDAK_NORMAL_MATA.Click
        chkNORMAL_MATA.Checked = False
        chkTIDAK_NORMAL_MATA.Checked = False
    End Sub
    Private Sub chkNORMAL_LEHER_CheckedChanged(sender As Object, e As EventArgs) Handles chkNORMAL_LEHER.Click, chkTIDAK_NORMAL_LEHER.Click
        chkNORMAL_LEHER.Checked = False
        chkTIDAK_NORMAL_LEHER.Checked = False
    End Sub
    Private Sub chkNORMAL_DADA_CheckedChanged(sender As Object, e As EventArgs) Handles chkNORMAL_DADA.Click, chkTIDAK_NORMAL_DADA.Click
        chkNORMAL_DADA.Checked = False
        chkTIDAK_NORMAL_DADA.Checked = False
    End Sub
    Private Sub chkNORMAL_PERUT_CheckedChanged(sender As Object, e As EventArgs) Handles chkNORMAL_PERUT.Click, chkTIDAK_NORMAL_PERUT.Click
        chkNORMAL_PERUT.Checked = False
        chkTIDAK_NORMAL_PERUT.Checked = False
    End Sub
    Private Sub chkNORMAL_ALATGERAK_CheckedChanged(sender As Object, e As EventArgs) Handles chkNORMAL_ALATGERAK.Click, chkTIDAK_NORMAL_ALATGERAK.Click
        chkNORMAL_ALATGERAK.Checked = False
        chkTIDAK_NORMAL_ALATGERAK.Checked = False
    End Sub
    Private Sub DeleteToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DeleteToolStripMenuItem.Click
        grvDetailResep.DeleteSelectedRows()
    End Sub
    Private Sub ToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem1.Click
        grvObatIGD.DeleteSelectedRows()
    End Sub
    Private Sub ToolStripMenuItem2_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem2.Click
        grvObatRanap.DeleteSelectedRows()
    End Sub
    Private Sub ToolStripMenuItem3_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem3.Click
        grvDIAGNOSA_CPPT.DeleteSelectedRows()
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
    Private Sub fn_LoadTindakLanjut(ByVal KDREG As String, ByVal catatan As String)
        Dim oSkd As New Admission.clsSKD
        txtTINDAKLANJUT.ResetText()

        For Each xloop In oSkd.GetDataByKdregList(KDREG)
            Dim dsSKD = oSkd.GetData(xloop.KDSKD)
            If dsSKD IsNot Nothing Then
                If dsSKD.ALASAN = "RUJUKAN EKSTERNAL" Then
                    Dim sTindakLanjut As String = "Tanggal Kontrol : " & dsSKD.DATEKONTROL.ToString("dd-MM-yyyy") & vbCrLf & "Alasan : " & dsSKD.ALASAN.ToString.Trim & vbCrLf & "Alasan di rujuk : " & dsSKD.DESCRIPTION.ToString.Trim
                    txtTINDAKLANJUT.Text = sTindakLanjut
                    oS_DIGITAL_IGD_01.UpdateTindakLanjut(txtNoRegister.Text, sTindakLanjut, True, False, False, False)
                End If
                If dsSKD.ALASAN = "SELESAI PENGOBATAN" Then
                    Dim sTindakLanjut As String = dsSKD.TINDAKLANJUT.ToString.Trim
                    txtTINDAKLANJUT.Text = sTindakLanjut
                End If
                If dsSKD.ALASAN = "MENINGGAL" Then
                    txtTINDAKLANJUT.Text = "MENINGGAL"
                End If
                'If dsSKD.ALASAN = "RAWAT INAP" Then
                '    Dim sTindakLanjut As String = dsSKD.ALASAN.ToString.Trim
                '    txtTINDAKLANJUT.Text = sTindakLanjut & vbCrLf & catatan
                'End If
                If dsSKD.ALASAN = "RAWAT INAP" Then
                    Dim sTindakLanjut As String = "Keterangan : " & dsSKD.ALASAN.ToString.Trim & vbCrLf & sRemarks_IntruksiDokter
                    txtTINDAKLANJUT.Text = IIf(txtTINDAKLANJUT.Text = "", "", txtTINDAKLANJUT.Text & vbCrLf & vbCrLf) & sTindakLanjut
                End If

                If dsSKD.ALASAN = "INTRUKSI LANJUTAN" Then
                    Dim sTindakLanjut As String = dsSKD.TINDAKLANJUT.ToString.Trim
                    txtTINDAKLANJUT.Text = sTindakLanjut
                End If
            End If
        Next
    End Sub
    Public Function fn_ValiadsiSudahAdaSKD(ByVal KDREG As String) As Boolean
        fn_ValiadsiSudahAdaSKD = False

        Try
            Dim oSkd As New Admission.clsSKD

            Dim Cek As Boolean = False
            For Each xloop In oSkd.GetDataByKdregList(KDREG)
                Cek = True
            Next
            If Cek = True Then
                If MsgBox("Apakah Yakin Akan Merubah Tindak Lanjut sebelumnya ?", MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, Me.Text) = MsgBoxResult.No Then
                    fn_ValiadsiSudahAdaSKD = False
                Else
                    fn_ValiadsiSudahAdaSKD = oSkd.DeleteDataAllSKDIGD(Decrypt(My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Software\SIMRS\SW\", "Database", "").ToString()), KDREG)
                End If
            Else
                fn_ValiadsiSudahAdaSKD = True
            End If
        Catch ex As Exception
            MsgBox("fn_ValiadsiSudahAdaSKD : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Function
    Private Sub SimpleButton2_Click(sender As Object, e As EventArgs) Handles btnEksternal.Click
        If txtNoRegister.Text Is Nothing Then
            MsgBox("Register Kosong", MsgBoxStyle.Exclamation, Me.Text)
            Exit Sub
        End If

        If fn_ValiadsiSudahAdaSKD(txtNoRegister.Text) = False Then
            Exit Sub
        End If

        chkTINDAKLANJUT_RUJUK.Checked = True
        chkTINDAKLANJUT_RAWAT.Checked = False
        chkTINDAKLANJUT_PULANGPAKSA.Checked = False
        chkTINDAKLANJUT_PULANG.Checked = False

        Dim oSKD As New Admission.clsSKD
        Dim ds = oSKD.GetDataPendaftaranalasan(txtNoRegister.Text, "RUJUKAN EKSTERNAL")

        If ds IsNot Nothing Then
            Dim frmSKD As New frmSKD
            Try
                frmSKD.fn_LoadKategori(1)
                frmSKD.LoadMe(FORM_MODE.FORM_MODE_EDIT, ds.KDSKD)
                frmSKD.ShowDialog(Me)
            Catch ex As Exception
                MsgBox("Load Form Detail : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
            Finally
                If Not frmSKD Is Nothing Then frmSKD.Dispose()
                frmSKD = Nothing
            End Try
        Else
            Dim frmSKD As New frmSKD
            Try
                frmSKD.fn_LoadNoPendaftaran(txtNoRegister.Text, sKDCUSTOMER, sKDDEPARTMENT, sKDDOCTOR, sNOMORSEP, sPENJAMIN, sNAMAPASIEN, txtKDDIAGNOSA.Text)
                frmSKD.fn_LoadKategori(1)
                frmSKD.LoadMe(FORM_MODE.FORM_MODE_ADD)
                frmSKD.ShowDialog(Me)
            Catch ex As Exception
                MsgBox("Load Form Detail : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
            End Try
        End If

        fn_LoadTindakLanjut(txtNoRegister.Text, "")

    End Sub
    Private Sub btnRawatInap_Click(sender As Object, e As EventArgs) Handles btnRawatInap.Click
        'If txtNoRegister.Text Is Nothing Then
        '    MsgBox("Register Kosong", MsgBoxStyle.Exclamation, Me.Text)
        '    Exit Sub
        'End If

        'If fn_ValiadsiSudahAdaSKD(txtNoRegister.Text) = False Then
        '    Exit Sub
        'End If

        'If MsgBox("Apakah Rawat Inap?", MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, Me.Text) = MsgBoxResult.No Then Exit Sub

        'chkTINDAKLANJUT_RUJUK.Checked = False
        'chkTINDAKLANJUT_RAWAT.Checked = True
        'chkTINDAKLANJUT_PULANGPAKSA.Checked = False
        'chkTINDAKLANJUT_PULANG.Checked = False

        'Dim oSKD As New Admission.clsSKD
        'Dim ds = oSKD.GetDataPendaftaranalasan(txtNoRegister.Text, "RAWAT INAP")

        'If ds IsNot Nothing Then
        '    fn_SaveSKD(False, ds.KDSKD, "RAWAT INAP", "")
        'Else
        '    fn_SaveSKD(True, "", "RAWAT INAP", "")
        'End If

        'Dim frmRemarks As New frmRemarks
        'Try
        '    sRemarks = ""
        '    frmRemarks.ShowDialog(Me)
        'Catch ex As Exception
        '    MsgBox("Load Form Detail : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
        'End Try

        'fn_LoadTindakLanjut(txtNoRegister.Text, sRemarks)

        'If oFormMode = FORM_MODE.FORM_MODE_EDIT Then
        '    oS_DIGITAL_IGD_01.UpdateTindakLanjut(txtNoRegister.Text, "RAWAT INAP", False, True, False, False)
        'End If

        If txtNoRegister.Text = "" Then
            MsgBox("Register Kosong", MsgBoxStyle.Exclamation, Me.Text)
            Exit Sub
        End If

        If MsgBox("Apakah Rawat Inap?", MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, Me.Text) = MsgBoxResult.No Then Exit Sub

        sRemarks_Ruangan = ""
        sRemarks_RencanaPembedahan = ""
        sRemarks_IntruksiDokter = ""

        Dim Kode As String = fn_LoadTindakLanjutAlasanCek(txtNoRegister.Text, "RAWAT INAP")

        Dim frmRemarksRawatInap As New frmRemarksRawatInap
        Try
            frmRemarksRawatInap.ShowDialog(Me)
        Catch ex As Exception
            MsgBox("Load Form Detail : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try

        If Kode <> "" Then
            fn_SaveSKD(False, Kode, "RAWAT INAP")
        Else
            fn_SaveSKD(True, "", "RAWAT INAP")
        End If

        fn_LoadTindakLanjut(txtNoRegister.Text, "")

        sRemarks = ""
        sRemarks_Ruangan = ""
        sRemarks_RencanaPembedahan = ""
        sRemarks_IntruksiDokter = ""
    End Sub
    Private Function fn_LoadTindakLanjutAlasanCek(ByVal KDREG As String, ByVal ALASAN As String) As String
        fn_LoadTindakLanjutAlasanCek = ""

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
            SQL &= "FROM S_PENDAFTARAN_SKD WHERE KDPENDAFTARAN = '" & KDREG & "' AND ALASAN = '" & ALASAN & "'"

            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "S_PENDAFTARAN_SKD")

            If oConn.State = ConnectionState.Open Then
                oConn.Close()
            End If

            For iLoop As Integer = 0 To ds.Tables("S_PENDAFTARAN_SKD").Rows.Count - 1
                With ds.Tables("S_PENDAFTARAN_SKD")
                    fn_LoadTindakLanjutAlasanCek = .Rows(iLoop)("KDSKD").ToString()
                    sRemarks_Ruangan = .Rows(iLoop)("REQUEST").ToString()
                    sRemarks_RencanaPembedahan = .Rows(iLoop)("RESPONSE").ToString()
                    sRemarks_IntruksiDokter = .Rows(iLoop)("DESCRIPTION").ToString()
                End With
            Next

        Catch ex As Exception
            MsgBox("Tindak Lanjut : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Function
    Private Function fn_SaveSKD(ByVal isAdd As Boolean, ByVal NOIID As String, ByVal ALASAN As String) As Boolean
        Try
            ' ***** HEADER *****
            Dim oSKD As New Admission.clsSKD
            Dim ds = oSKD.GetStructureHeader
            With ds
                Try
                    .DATECREATED = oSKD.GetData(NOIID).DATECREATED
                Catch oErr As Exception
                    .DATECREATED = Now
                End Try

                .DATEUPDATED = Now
                .KDSKD = NOIID
                .KDANTRIANMANUAL = 0
                Try
                    .KDPENDAFTARAN = oSKD.GetData(NOIID).KDPENDAFTARAN
                Catch oErr As Exception
                    .KDPENDAFTARAN = txtNoRegister.Text
                End Try
                .DATE = deDATE.DateTime
                .ISCATEGORY = 0
                Try
                    .KDDIAGNOSA = oSKD.GetData(NOIID).KDDIAGNOSA
                Catch oErr As Exception
                    .KDDIAGNOSA = "-"
                End Try
                .KDDEPARTMENT = sKDDEPARTMENT
                .KDDOCTOR = sKDDOCTOR
                .NOMORRUJUKAN = ""
                .DESCRIPTION = sRemarks_IntruksiDokter
                Try
                    .ISCHEKED = oSKD.GetData(NOIID).ISCHEKED
                Catch oErr As Exception
                    .ISCHEKED = False
                End Try
                .KDUSER = sUserID
                .DATEKONTROL = Now
                .ALASAN = ALASAN
                .TINDAKLANJUT = ""
                .TANGGALPERIKSA_TEXT = Now.ToString("ddMMyyyy")
                .KDJADWALDOKTER = 0
                .SEQ = 0
                .REQUEST = sRemarks_Ruangan
                .RESPONSE = sRemarks_RencanaPembedahan
                .NOMORSEP = ""
                .SEARCH = Now.ToString("ddMMyyyy")
                Try
                    .KDCUSTOMER = oSKD.GetData(NOIID).KDCUSTOMER
                Catch oErr As Exception
                    .KDCUSTOMER = txtNoPasien.Text
                End Try
                .ORDERPENUNJANG = ""
                .DATEKONTROL_2 = Now
            End With

            If isAdd = True Then
                Dim KDSKD As String = oSKD.InsertData(ds, NOIID)

                If KDSKD <> "" Then
                    fn_SaveSKD = True
                Else
                    fn_SaveSKD = False
                End If
            Else
                fn_SaveSKD = oSKD.UpdateData(ds)
            End If
        Catch oErr As Exception
            MsgBox("Save SKD" & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            fn_SaveSKD = False
        End Try
    End Function
    Private Sub btnSelesaiPengobatan_Click(sender As Object, e As EventArgs) Handles btnSelesaiPengobatan.Click
        If txtNoRegister.Text Is Nothing Then
            MsgBox("Register Kosong", MsgBoxStyle.Exclamation, Me.Text)
            Exit Sub
        End If

        If fn_ValiadsiSudahAdaSKD(txtNoRegister.Text) = False Then
            Exit Sub
        End If

        If txtSAATPASIENPULANG_TEXT.Text = "" Then
            MsgBox("Kondisi Saat Pulang Kosong", MsgBoxStyle.Exclamation, Me.Text)
            txtSAATPASIENPULANG_TEXT.Focus()
            Exit Sub
        End If
        If txtINSTRUKSILANJUTAN.Text = "" Then
            MsgBox("Intruksi Lanjutan Saat Pulang Kosong", MsgBoxStyle.Exclamation, Me.Text)
            txtINSTRUKSILANJUTAN.Focus()
            Exit Sub
        End If
        If cboSAATPULANG_TINGKATKESDARAN.Text = "" Then
            MsgBox("Tingkat Kesadaran Saat Pulang Kosong", MsgBoxStyle.Exclamation, Me.Text)
            txtINSTRUKSILANJUTAN.Focus()
            Exit Sub
        End If
        If txtSAATPULANG_GCS.Text = "" Then
            MsgBox("GCS Saat Pulang Kosong", MsgBoxStyle.Exclamation, Me.Text)
            txtINSTRUKSILANJUTAN.Focus()
            Exit Sub
        End If
        If txtSAATPULANG_BP.Text = "" Then
            MsgBox("BP Saat Pulang Kosong", MsgBoxStyle.Exclamation, Me.Text)
            txtSAATPULANG_BP.Focus()
            Exit Sub
        End If
        If txtSAATPULANG_HR.Text = "" Then
            MsgBox("HR Saat Pulang Kosong", MsgBoxStyle.Exclamation, Me.Text)
            txtSAATPULANG_HR.Focus()
            Exit Sub
        End If
        If txtSAATPULANG_RR.Text = "" Then
            MsgBox("RR Saat Pulang Kosong", MsgBoxStyle.Exclamation, Me.Text)
            txtSAATPULANG_HR.Focus()
            Exit Sub
        End If
        If txtSAATPULANG_SPO2.Text = "" Then
            MsgBox("SPO2 Saat Pulang Kosong", MsgBoxStyle.Exclamation, Me.Text)
            txtSAATPULANG_SPO2.Focus()
            Exit Sub
        End If
        If txtSAATPULANG_T.Text = "" Then
            MsgBox("T Saat Pulang Kosong", MsgBoxStyle.Exclamation, Me.Text)
            txtSAATPULANG_T.Focus()
            Exit Sub
        End If

        If MsgBox("Apakah Pulang Paksa?", MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, Me.Text) = MsgBoxResult.No Then Exit Sub

        chkTINDAKLANJUT_RUJUK.Checked = False
        chkTINDAKLANJUT_RAWAT.Checked = False
        chkTINDAKLANJUT_PULANGPAKSA.Checked = True
        chkTINDAKLANJUT_PULANG.Checked = False

        Dim oSKD As New Admission.clsSKD
        Dim ds = oSKD.GetDataPendaftaranalasan(txtNoRegister.Text, "SELESAI PENGOBATAN")

        If ds IsNot Nothing Then
            fn_SaveSKD(False, ds.KDSKD, "SELESAI PENGOBATAN", "PULANG PAKSA")
        Else
            fn_SaveSKD(True, "", "SELESAI PENGOBATAN", "PULANG PAKSA")
        End If

        fn_LoadTindakLanjut(txtNoRegister.Text, "")
        If oFormMode = FORM_MODE.FORM_MODE_EDIT Then
            oS_DIGITAL_IGD_01.UpdateTindakLanjut(txtNoRegister.Text, "PULANG PAKSAP", False, False, True, False)
        End If
    End Sub
    Private Sub SimpleButton3_Click(sender As Object, e As EventArgs) Handles SimpleButton3.Click
        If txtNoRegister.Text Is Nothing Then
            MsgBox("Register Kosong", MsgBoxStyle.Exclamation, Me.Text)
            Exit Sub
        End If

        If txtSAATPASIENPULANG_TEXT.Text = "" Then
            MsgBox("Kondisi Saat Pulang Kosong", MsgBoxStyle.Exclamation, Me.Text)
            txtSAATPASIENPULANG_TEXT.Focus()
            Exit Sub
        End If
        If txtINSTRUKSILANJUTAN.Text = "" Then
            MsgBox("Intruksi Lanjutan Saat Pulang Kosong", MsgBoxStyle.Exclamation, Me.Text)
            txtINSTRUKSILANJUTAN.Focus()
            Exit Sub
        End If
        If cboSAATPULANG_TINGKATKESDARAN.Text = "" Then
            MsgBox("Tingkat Kesadaran Saat Pulang Kosong", MsgBoxStyle.Exclamation, Me.Text)
            txtINSTRUKSILANJUTAN.Focus()
            Exit Sub
        End If
        If txtSAATPULANG_GCS.Text = "" Then
            MsgBox("GCS Saat Pulang Kosong", MsgBoxStyle.Exclamation, Me.Text)
            txtINSTRUKSILANJUTAN.Focus()
            Exit Sub
        End If
        If txtSAATPULANG_BP.Text = "" Then
            MsgBox("BP Saat Pulang Kosong", MsgBoxStyle.Exclamation, Me.Text)
            txtSAATPULANG_BP.Focus()
            Exit Sub
        End If
        If txtSAATPULANG_HR.Text = "" Then
            MsgBox("HR Saat Pulang Kosong", MsgBoxStyle.Exclamation, Me.Text)
            txtSAATPULANG_HR.Focus()
            Exit Sub
        End If
        If txtSAATPULANG_RR.Text = "" Then
            MsgBox("RR Saat Pulang Kosong", MsgBoxStyle.Exclamation, Me.Text)
            txtSAATPULANG_HR.Focus()
            Exit Sub
        End If
        If txtSAATPULANG_SPO2.Text = "" Then
            MsgBox("SPO2 Saat Pulang Kosong", MsgBoxStyle.Exclamation, Me.Text)
            txtSAATPULANG_SPO2.Focus()
            Exit Sub
        End If
        If txtSAATPULANG_T.Text = "" Then
            MsgBox("T Saat Pulang Kosong", MsgBoxStyle.Exclamation, Me.Text)
            txtSAATPULANG_T.Focus()
            Exit Sub
        End If
        If fn_ValiadsiSudahAdaSKD(txtNoRegister.Text) = False Then
            Exit Sub
        End If

        If MsgBox("Apakah Pulang ?", MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, Me.Text) = MsgBoxResult.No Then Exit Sub

        chkTINDAKLANJUT_RUJUK.Checked = False
        chkTINDAKLANJUT_RAWAT.Checked = False
        chkTINDAKLANJUT_PULANGPAKSA.Checked = False
        chkTINDAKLANJUT_PULANG.Checked = True

        Dim oSKD As New Admission.clsSKD
        Dim ds = oSKD.GetDataPendaftaranalasan(txtNoRegister.Text, "SELESAI PENGOBATAN")

        If ds IsNot Nothing Then
            fn_SaveSKD(False, ds.KDSKD, "SELESAI PENGOBATAN", "PULANG")
        Else
            fn_SaveSKD(True, "", "SELESAI PENGOBATAN", "PULANG")
        End If

        fn_LoadTindakLanjut(txtNoRegister.Text, "")
        If oFormMode = FORM_MODE.FORM_MODE_EDIT Then
            oS_DIGITAL_IGD_01.UpdateTindakLanjut(txtNoRegister.Text, "PULANG", False, False, False, True)
        End If
    End Sub
    Private Sub Meninggal_Click(sender As Object, e As EventArgs) Handles Meninggal.Click
        If txtNoRegister.Text Is Nothing Then
            MsgBox("Register Kosong", MsgBoxStyle.Exclamation, Me.Text)
            Exit Sub
        End If

        If fn_ValiadsiSudahAdaSKD(txtNoRegister.Text) = False Then
            Exit Sub
        End If

        If MsgBox("Apakah Pasien Meninggal ?", MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, Me.Text) = MsgBoxResult.No Then Exit Sub

        chkTINDAKLANJUT_RUJUK.Checked = False
        chkTINDAKLANJUT_RAWAT.Checked = False
        chkTINDAKLANJUT_PULANGPAKSA.Checked = False
        chkTINDAKLANJUT_PULANG.Checked = False

        Dim oSKD As New Admission.clsSKD
        Dim ds = oSKD.GetDataPendaftaranalasan(txtNoRegister.Text, "MENINGGAL")

        If ds IsNot Nothing Then
            fn_SaveSKD(False, ds.KDSKD, "MENINGGAL", "MENINGGAL")
        Else
            fn_SaveSKD(True, "", "MENINGGAL", "MENINGGAL")
        End If

        fn_LoadTindakLanjut(txtNoRegister.Text, "")

        If oFormMode = FORM_MODE.FORM_MODE_EDIT Then
            oS_DIGITAL_IGD_01.UpdateTindakLanjut(txtNoRegister.Text, "MENINGGAL", False, False, False, False)
        End If
    End Sub
    Private Function fn_SaveSKD(ByVal isAdd As Boolean, ByVal NOIID As String, ByVal ALASAN As String, ByVal TINDAKLANJUT As String) As Boolean
        Try
            ' ***** HEADER *****
            Dim oSKD As New Admission.clsSKD
            Dim ds = oSKD.GetStructureHeader
            With ds
                Try
                    .DATECREATED = oSKD.GetData(NOIID).DATECREATED
                Catch oErr As Exception
                    .DATECREATED = Now
                End Try

                .DATEUPDATED = Now
                .KDSKD = NOIID
                .KDANTRIANMANUAL = 0
                Try
                    .KDPENDAFTARAN = oSKD.GetData(NOIID).KDPENDAFTARAN
                Catch oErr As Exception
                    .KDPENDAFTARAN = txtNoRegister.Text
                End Try
                .DATE = deDATE.DateTime
                .ISCATEGORY = 0
                Try
                    .KDDIAGNOSA = oSKD.GetData(NOIID).KDDIAGNOSA
                Catch oErr As Exception
                    .KDDIAGNOSA = "-"
                End Try
                .KDDEPARTMENT = sKDDEPARTMENT
                .KDDOCTOR = sKDDOCTOR
                .NOMORRUJUKAN = ""
                .DESCRIPTION = ""
                Try
                    .ISCHEKED = oSKD.GetData(NOIID).ISCHEKED
                Catch oErr As Exception
                    .ISCHEKED = False
                End Try
                .KDUSER = sUserID
                .DATEKONTROL = Now
                .ALASAN = ALASAN
                .TINDAKLANJUT = TINDAKLANJUT
                .TANGGALPERIKSA_TEXT = Now.ToString("ddMMyyyy")
                .KDJADWALDOKTER = 0
                .SEQ = 0
                .REQUEST = ""
                .RESPONSE = ""
                .NOMORSEP = ""
                .SEARCH = Now.ToString("ddMMyyyy")
                Try
                    .KDCUSTOMER = oSKD.GetData(NOIID).KDCUSTOMER
                Catch oErr As Exception
                    .KDCUSTOMER = sKDCUSTOMER
                End Try
                .ORDERPENUNJANG = ""
                .DATEKONTROL_2 = Now
            End With

            If isAdd = True Then
                Dim KDSKD As String = oSKD.InsertData(ds, NOIID)

                If KDSKD <> "" Then
                    fn_SaveSKD = True
                Else
                    fn_SaveSKD = False
                End If
            Else
                fn_SaveSKD = oSKD.UpdateData(ds)
            End If
        Catch oErr As Exception
            MsgBox("Save SKD" & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            fn_SaveSKD = False
        End Try
    End Function
    Private Sub SimpleButton4_Click(sender As Object, e As EventArgs) Handles SimpleButton4.Click
        fn_LoadVonek()
    End Sub
    Private Sub SimpleButton1_Click(sender As Object, e As EventArgs) Handles SimpleButton1.Click
        fn_LoadAdsemenPerawat()
    End Sub
    Private Sub SimpleButton2_Click_1(sender As Object, e As EventArgs) Handles SimpleButton2.Click
        fn_LoadDataReloadIGD()
        grdReloadIGD.ShowPopup()
    End Sub
    Private Sub OnValueChanged(sender As Object, e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs) Handles grvDetailResep.FocusedRowChanged, grvObatIGD.FocusedRowChanged, grvObatRanap.FocusedRowChanged

    End Sub
    Private Sub fn_ReoladResep()
        Try
            grvObatIGD.OptionsSelection.MultiSelect = True
            grvObatIGD.SelectAll()
            grvObatIGD.DeleteSelectedRows()
            grvObatIGD.OptionsSelection.MultiSelect = False

            grvObatRanap.OptionsSelection.MultiSelect = True
            grvObatRanap.SelectAll()
            grvObatRanap.DeleteSelectedRows()
            grvObatRanap.OptionsSelection.MultiSelect = False

            Dim oConn As New SqlConnection
            Dim oComm As New SqlCommand
            Dim da As SqlDataAdapter
            Dim ds As New DataSet
            Dim SQL As String
            oConn = New SqlConnection(sConnOld)

            If oConn.State = ConnectionState.Closed Then
                oConn.Open()
            End If

            SQL = "SELECT "
            SQL &= "B.* "
            SQL &= ",KATEGOR_IGD = ISNULL((B.BATCH), '') "
            SQL &= "FROM "
            SQL &= "S_RECIPE_H A "
            SQL &= "INNER JOIN S_RECIPE_D B "
            SQL &= "ON A.KDRECIPE = B.KDRECIPE "
            SQL &= "WHERE "
            SQL &= "A.KDREG = '" & txtNoRegister.Text & "' "

            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "S_RECIPE_H")

            If oConn.State = ConnectionState.Open Then
                oConn.Close()
            End If

            For iLoop As Integer = 0 To ds.Tables("S_RECIPE_H").Rows.Count - 1
                With ds.Tables("S_RECIPE_H")
                    If .Rows(iLoop)("KATEGOR_IGD") = "RESEP RAWAT INAP" Then
                        grvObatRanap.Focus()
                        grvObatRanap.AddNewRow()
                        grvObatRanap.SetFocusedRowCellValue(colKDITEM_ObatRanap, .Rows(iLoop)("KDITEM"))
                        grvObatRanap.SetFocusedRowCellValue(colKDSIGNA_ObatRanap, .Rows(iLoop)("KDSIGNA"))
                        grvObatRanap.SetFocusedRowCellValue(colKDCARAPAKAI_ObatRanap, .Rows(iLoop)("KDCP"))
                        grvObatRanap.SetFocusedRowCellValue(colQTY_ObatRanap, .Rows(iLoop)("QTY"))
                        grvObatRanap.UpdateCurrentRow()
                    Else
                        grvObatIGD.Focus()
                        grvObatIGD.AddNewRow()
                        grvObatIGD.SetFocusedRowCellValue(colKDITEM_ObatIGD, .Rows(iLoop)("KDITEM"))
                        grvObatIGD.SetFocusedRowCellValue(colKDSIGNA_ObatIGD, .Rows(iLoop)("KDSIGNA"))
                        grvObatIGD.SetFocusedRowCellValue(colKDCARAPAKAI_ObatIGD, .Rows(iLoop)("KDCP"))
                        grvObatIGD.SetFocusedRowCellValue(colQTY_ObatIGD, .Rows(iLoop)("QTY"))
                        grvObatIGD.UpdateCurrentRow()
                    End If
                End With
            Next
        Catch oErr As Exception
            MsgBox("Reload Resep" & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub grdReloadIGD_EditValueChanged(sender As Object, e As EventArgs) Handles grdReloadIGD.EditValueChanged
        If grdReloadIGD.Text <> "" Then
            Try
                txtRELOAD.Text = grdReloadIGD.EditValue

                Dim oDIGITAL_IGD_01_AWAL As New Transaksi.clsDIGITAL_IGD_01_AWAL
                ' ***** HEADER *****
                Dim ds = oDIGITAL_IGD_01_AWAL.GetData(grdReloadIGD.EditValue)

                With ds
                    txtPETUGAS_TRIAGE.Text = .KDUSER
                    deDATE.DateTime = .DATE
                    txtNamaPasien.Text = .NAMAPASIEN
                    txtRIWAYAT.Text = .RIWAYAT
                    txtRIWAYAT_ALERGI.Text = .ALERGI
                    txtRIWAYAT_PENYAKITDAHULU.Text = .ANAMNESIS
                    cboTINGKATKESADARAN.Text = .TINGKATKESADARAN
                    txtGCS.Text = .GCS
                    cboE.Text = .GCS_E
                    cboM.Text = .GCS_M
                    cboV.Text = .GCS_V
                    txtKEADAANUMUM.Text = .KEADAANUMUM
                    txtBERATBADAN.Text = .BERATBADAN
                    txtTINGGIBADAN.Text = .TINGGIBADAN
                    txtBP.Text = .BP
                    txtHR.Text = .HR
                    txtRR.Text = .RR
                    txtT.Text = .T
                    txtSPO2.Text = .SP02

                    chkTRIAGE_RED.Checked = .TRIAGE_1
                    chkTRIAGE_YELLOW.Checked = .TRIAGE_2
                    chkTRIAGE_GREEN.Checked = .TRIAGE_3
                    chkTRIAGE_BLACK.Checked = .TRIAGE_4

                    txtSAATPULANG_GCS.Text = .GCS
                    cboSAATPULANG_E.Text = .GCS_E
                    cboSAATPULANG_M.Text = .GCS_M
                    cboSAATPULANG_V.Text = .GCS_V

                    cboSAATPULANG_TINGKATKESDARAN.Text = .KEADAANUMUM
                    'txtSAATPULANG_BP.Text = .BP
                    'txtSAATPULANG_HR.Text = .HR
                    'txtSAATPULANG_T.Text = .T
                    'txtSAATPULANG_RR.Text = .RR
                    'txtSAATPULANG_SPO2.Text = .SP02

                    cboTRIAGE_AIRWAY.Text = .TRIAGE_AIRWAY
                    cboTRIAGE_BREATHING.Text = .TRIAGE_BREATHING
                    cboTRIAGE_CIRCULATION.Text = .TRIAGE_CIRCULATION

                    BindingSourceTindakanPoli.DataSource = oDIGITAL_IGD_01_AWAL.GetDataDetail(.KDAWALASESMENIGD).OrderBy(Function(x) x.SEQ).ToList()
                    grdTindakanPoli.DataSource = BindingSourceTindakanPoli

                    BindingSourcePenunjang.DataSource = oDIGITAL_IGD_01_AWAL.GetDataDetail_P(.KDAWALASESMENIGD).OrderBy(Function(x) x.SEQ).ToList()
                    grdTindakan.DataSource = BindingSourcePenunjang

                    BindingSource.DataSource = oDIGITAL_IGD_01_AWAL.GetDataDetail_ResepPulang(.KDAWALASESMENIGD).OrderBy(Function(x) x.SEQ).ToList()
                    grdDetailResep.DataSource = BindingSource

                    deDATEPUKULPERIKSA.Time = .DATECREATED

                End With
            Catch oErr As Exception
                MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            End Try
        End If
    End Sub

    'Private Sub btnReload_Click(sender As Object, e As EventArgs) Handles btnReload.Click
    '    grvDetail.OptionsSelection.MultiSelect = True
    '    grvDetail.SelectAll()
    '    grvDetail.DeleteSelectedRows()
    '    grvDetail.OptionsSelection.MultiSelect = False

    '    Dim dsKunjungan = oS_DIGITAL_IGD_01.GetDataByKunjungan(txtNoRegister.Text)
    '    If dsKunjungan IsNot Nothing Then
    '        Dim oOrderTindakan As New Inventory.clsOrderTindakan
    '        Dim oOrderLab As New Inventory.clsOrderLab
    '        Dim oOrderRad As New Inventory.clsOrderRad
    '        Dim oKonsul As New Digital.clsKonsul
    '        Dim oKonsulJawab As New Digital.clsJawabKonsul

    '        Dim dsTindakan = From x In oOrderTindakan.GetDataPendaftaranList(dsKunjungan.KDPENDAFTARAN)
    '                         Join y In oOrderTindakan.GetDataDetail()
    '                         On x.KDORDERTINDAKAN Equals y.KDORDERTINDAKAN
    '                         Select TANGGAL = x.DATE, ITEM = "Tindakan : " & y.TARIFKT & ", Catatan : " & y.REMARKS

    '        Dim dsLab = From x In oOrderLab.GetDataPendaftaranList(dsKunjungan.KDPENDAFTARAN)
    '                    Join y In oOrderLab.GetDataDetail()
    '                    On x.KDORDERLAB Equals y.KDORDERLAB
    '                    Select TANGGAL = x.DATE, ITEM = "Lab : " & y.TARIFKT  & ", Catatan : " & y.REMARKS

    '        Dim dsRad = From x In oOrderRad.GetDataPendaftaranList(dsKunjungan.KDPENDAFTARAN)
    '                    Join y In oOrderRad.GetDataDetail()
    '                    On x.KDORDERRAD Equals y.KDORDERRAD
    '                    Select TANGGAL = x.DATE, ITEM = "Rad : " & y.TARIFKT & ", Catatan : " & y.REMARKS

    '        Dim dsKonsul = From x In oKonsul.GetDataPendaftaranList(dsKunjungan.KDPENDAFTARAN)
    '                       Select TANGGAL = x.DATE, ITEM = "Konsul : " & "Dari " & x.DOKTER_DARI & " Kepada " & x.DOKTER_KEPADA & " Isi " & x.MEMO & ", "

    '        Dim dsJawabKonsul = From x In oKonsulJawab.GetDataPendaftaranList(dsKunjungan.KDPENDAFTARAN)
    '                            Select TANGGAL = x.DATE, ITEM = "Jawab Konsul : " & "Dari " & x.DOKTER_DARI & " Kepada " & x.DOKTER_KEPADA & " Isi " & x.MEMO & ", "

    '        Dim dsUnionAll = dsTindakan.Union(dsLab).Union(dsRad).Union(dsKonsul).Union(dsJawabKonsul)

    '        For Each xloop In dsUnionAll.OrderBy(Function(x) x.TANGGAL)
    '            grvDetail.Focus()
    '            grvDetail.AddNewRow()
    '            grvDetail.SetFocusedRowCellValue(colPUKUL, xloop.TANGGAL)
    '            grvDetail.SetFocusedRowCellValue(colPENANGANAN, xloop.ITEM)
    '            grvDetail.SetFocusedRowCellValue(colNAMA, "")
    '            grvDetail.SetFocusedRowCellValue(colPARAF, "")

    '            grvDetail.UpdateCurrentRow()
    '        Next

    '    End If
    'End Sub
    Private Sub fn_LoadTEMPLATE()
        Dim oTemplate As New Reference.clsTemplateResep
        Try
            grdTEMPLATE.Properties.DataSource = oTemplate.GetData.Where(Function(x) x.NOIDUSER = sUserID).ToList()
            grdTEMPLATE.Properties.ValueMember = "KDTEMPLATE"
            grdTEMPLATE.Properties.DisplayMember = "DESCRIPTION"
        Catch oErr As Exception
            MsgBox("Load Template Data : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub grdTEMPLATE_EditValueChanged(sender As Object, e As EventArgs) Handles grdTEMPLATE.EditValueChanged
        If isLoad = True Then
            If grdTEMPLATE.Text <> "" Then
                Dim oTemplate As New Reference.clsTemplateResep

                Dim dsTemplate = oTemplate.GetDataDetail(grdTEMPLATE.EditValue)

                For Each iLoop In dsTemplate.OrderBy(Function(x) x.SEQ)
                    'txtDIAGNOOSA.Text = iLoop.S_REQ_RECIPE_TEMPLATE_H.DESCRIPTION
                    grvDetailResep.Focus()
                    grvDetailResep.AddNewRow()
                    grvDetailResep.SetFocusedRowCellValue(colKDITEM, iLoop.KDITEM)
                    grvDetailResep.SetFocusedRowCellValue(colKDUOM, iLoop.KDUOM)
                    grvDetailResep.SetFocusedRowCellValue(colKDSIGNA, iLoop.KDSIGNA)
                    grvDetailResep.SetFocusedRowCellValue(colKDCARAPAKAI, iLoop.KDCARAPAKAI)
                    grvDetailResep.SetFocusedRowCellValue(colQTY, iLoop.QTY)
                    grvDetailResep.SetFocusedRowCellValue(colREMARKS_DOKTER, iLoop.REMARKS_DOKTER)
                    grvDetailResep.UpdateCurrentRow()


                    '.NAMAOBAT = fn_LoadITEM(grvDetailResep.GetRowCellValue(i, colKDITEM))
                    '.SATUAN = fn_LoadUOMDESCRIPTION(grvDetailResep.GetRowCellValue(i, colKDUOM))
                    '.SIGNA = IIf(String.IsNullOrEmpty(grvDetailResep.GetRowCellValue(i, colKDSIGNA)), "", fn_LoadSIGNA(grvDetailResep.GetRowCellValue(i, colKDSIGNA)))
                    '.CARAPAKAI = IIf(String.IsNullOrEmpty(grvDetailResep.GetRowCellValue(i, colKDCARAPAKAI)), "", fn_LoadCARAPAKAI(grvDetailResep.GetRowCellValue(i, colKDCARAPAKAI)))
                    '.QTY = CDec(grvDetailResep.GetRowCellValue(i, colQTY))
                    '.PRICE = CDec(grvDetailResep.GetRowCellValue(i, colPRICE))
                    '.GRANDTOTAL = CDec(grvDetailResep.GetRowCellValue(i, colGRANDTOTAL))
                    '.ROMAWI = IntegerToRoman(CInt(grvDetailResep.GetFocusedRowCellValue(colQTY)))
                    '.REMARKS_DOKTER = IIf(String.IsNullOrEmpty(grvDetailResep.GetRowCellValue(i, colREMARKS_DOKTER)), "", grvDetailResep.GetRowCellValue(i, colREMARKS_DOKTER))
                    '.KDITEM = grvDetailResep.GetRowCellValue(i, colKDITEM)
                    '.KDUOM = grvDetailResep.GetRowCellValue(i, colKDUOM)
                    '.KDSIGNA = IIf(String.IsNullOrEmpty(grvDetailResep.GetRowCellValue(i, colKDSIGNA)), "1286", grvDetailResep.GetRowCellValue(i, colKDSIGNA))
                    '.KDCARAPAKAI = IIf(String.IsNullOrEmpty(grvDetailResep.GetRowCellValue(i, colKDCARAPAKAI)), "10", grvDetailResep.GetRowCellValue(i, colKDCARAPAKAI))
                    '.QTY_PERUBAHAN = grvDetailResep.GetRowCellValue(i, colQTY)
                    '.REMARKS_FARMASI = ""
                Next
            End If
        End If
    End Sub
    Private Sub btnTATALAKSANA_Click(sender As Object, e As EventArgs) Handles btnTATALAKSANA.Click
        If txtNoRegister.Text Is Nothing Then
            MsgBox("Register Kosong", MsgBoxStyle.Exclamation, Me.Text)
            Exit Sub
        End If

        If MsgBox("Apakah Pasien Intruksi Lanjutan?", MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, Me.Text) = MsgBoxResult.No Then Exit Sub

        If fn_ValiadsiSudahAdaSKD(txtNoRegister.Text) = False Then
            Exit Sub
        End If

        Dim oSKD As New Admission.clsSKD
        Dim ds = oSKD.GetDataPendaftaranalasan(txtNoRegister.Text, "INTRUKSI LANJUTAN")

        If ds IsNot Nothing Then
            fn_SaveSKD(False, ds.KDSKD, "INTRUKSI LANJUTAN", "Mohon Asessmen Ulang Oleh TS di FKTP")
        Else
            fn_SaveSKD(True, "", "INTRUKSI LANJUTAN", "Mohon Asessmen Ulang Oleh TS di FKTP")
        End If

        fn_LoadTindakLanjut(txtNoRegister.Text, "INTRUKSI LANJUTAN")

        If oFormMode = FORM_MODE.FORM_MODE_EDIT Then
            oS_DIGITAL_IGD_01.UpdateTindakLanjut(txtNoRegister.Text, "INTRUKSI LANJUTAN", False, False, False, False)
        End If
    End Sub
    Private Sub txtCARIDIAGNOSA_CPPT_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtCARIDIAGNOSA_CPPT.KeyPress
        If Asc(e.KeyChar) = 13 Then
            Try
                Dim oBrigging As New Brigging.clsSetKoneksi
                Dim jsonDecode = JObject.Parse(oBrigging.fn_Pencariandiagnosa(txtCARIDIAGNOSA_CPPT.Text))
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

                    grdCARIDIAGNOSA_CPPT.Properties.DataSource = table
                    grdCARIDIAGNOSA_CPPT.Properties.ValueMember = "kode"
                    grdCARIDIAGNOSA_CPPT.Properties.DisplayMember = "nama"

                    GridView11.BestFitColumns()

                    grdCARIDIAGNOSA_CPPT.ShowPopup()

                    txtCARIDIAGNOSA_CPPT.ResetText()
                Else
                    MsgBox("Pencarian Diagnosa" & vbCrLf & sDataDuplicate & "-" & smessage, MsgBoxStyle.Information, Me.Text)
                End If
            Catch oErr As Exception
                MsgBox("Pencarian Diagnosa: " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            End Try
        End If
    End Sub
    Private Sub grdCARIDIAGNOSA_CPPT_EditValueChanged(sender As Object, e As EventArgs) Handles grdCARIDIAGNOSA_CPPT.EditValueChanged
        If grdCARIDIAGNOSA_CPPT.Text <> "" Then
            Dim sCek As Integer = 0

            For i As Integer = 0 To grvDIAGNOSA_CPPT.RowCount - 2
                sCek += 1
            Next

            grvDIAGNOSA_CPPT.Focus()
            grvDIAGNOSA_CPPT.AddNewRow()
            'grvDIAGNOSA_CPPT.SetFocusedRowCellValue(colKATEGORI_CPPT, IIf(sCek = 0, "Primer", "Sekunder"))
            grvDIAGNOSA_CPPT.SetFocusedRowCellValue(colKDDIAGNOSA_CPPT, grdCARIDIAGNOSA_CPPT.EditValue)
            grvDIAGNOSA_CPPT.SetFocusedRowCellValue(colNAMADIAGNOSA_CPPT, grdCARIDIAGNOSA_CPPT.Text)
            grvDIAGNOSA_CPPT.UpdateCurrentRow()

            txtCARIDIAGNOSA_CPPT.Focus()
        End If
    End Sub
#End Region
End Class