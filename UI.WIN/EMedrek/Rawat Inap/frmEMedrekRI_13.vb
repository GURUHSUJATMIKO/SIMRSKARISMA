Imports System.Linq
Imports DataAccess
Imports System.Data.SqlClient

Public Class frmEMedrekRI_13
#Region "Declaration"
    Private oFormMode As FORM_MODE = FORM_MODE.FORM_MODE_VIEW
    Private isLoad As Boolean = False
    Private oS_DIGITAL_RI_13 As New Transaksi.clsDigital_DischargePlanning
    Private oS_DIGITAL_RI_13_Template As New Transaksi.clsDigital_DischargePlanningTemplate
    Private sKDDOCTOR As String = ""
    Private sKDREG_RJ As String = String.Empty

#End Region
#Region "Function"
    Public Sub LoadMe(ByVal FormMode As Integer, ByVal KDREG As String, ByVal KDREG_RJ As String, ByVal KDCUSTOMER As String, ByVal NAMAPASIEN As String, ByVal JENISKELAMIN As String, ByVal DPJP As String, ByVal KDDPJP As String)
        oFormMode = FormMode
        txtNoRegister.Text = KDREG
        txtNamaPasien.Text = NAMAPASIEN
        txtNoPasien.Text = KDCUSTOMER
        sKDDOCTOR = KDDPJP
        sKDREG_RJ = KDREG_RJ
    End Sub
    Private Sub Form_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        sKODETEMPLATE = ""
        fn_ChangeFormState()
        isLoad = True
    End Sub
    Private Sub Form_Closing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        sCode = txtNoRegister.Text.Trim.ToUpper
        sLoadAsesmenAwal = False
        sKODEASESMENCOPY = ""
        Dispose()
    End Sub
    Private Overloads Sub Dispose()
        MyBase.Dispose()
        Me.Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
    Private Sub fn_ChangeFormState()
        fn_NOIDUSER()
        fn_Doctor()

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
        'txtJAM.Properties.ReadOnly = Status
        grdDPJP.Properties.ReadOnly = Status
        txtANAMNESIS_01.Properties.ReadOnly = Status
        txtANAMNESIS_02.Properties.ReadOnly = Status
        txtANAMNESIS_03.Properties.ReadOnly = Status
        txtANAMNESIS_04.Properties.ReadOnly = Status
        txtANAMNESIS_05.Properties.ReadOnly = Status
        txtANAMNESIS_06.Properties.ReadOnly = Status
        txtANAMNESIS_07.Properties.ReadOnly = Status
        txtANAMNESIS_08.Properties.ReadOnly = Status
        txtANAMNESIS_09.Properties.ReadOnly = Status
        txtANAMNESIS_10.Properties.ReadOnly = Status
        txtANAMNESIS_11.Properties.ReadOnly = Status
        txtANAMNESIS_12.Properties.ReadOnly = Status
        txtANAMNESIS_13.Properties.ReadOnly = Status
        txtANAMNESIS_14.Properties.ReadOnly = Status
        txtANAMNESIS_15.Properties.ReadOnly = Status
        txtANAMNESIS_16.Properties.ReadOnly = Status
        txtANAMNESIS_17.Properties.ReadOnly = Status
        txtANAMNESIS_18.Properties.ReadOnly = Status
        txtANAMNESIS_19.Properties.ReadOnly = Status
        txtANAMNESIS_20.Properties.ReadOnly = Status
        'txtANAMNESIS_21.Properties.ReadOnly = Status
        'txtANAMNESIS_22.Properties.ReadOnly = Status
        'txtANAMNESIS_23.Properties.ReadOnly = Status
        'txtANAMNESIS_24.Properties.ReadOnly = Status
        'txtANAMNESIS_25.Properties.ReadOnly = Status
        'txtANAMNESIS_26.Properties.ReadOnly = Status
        'txtANAMNESIS_27.Properties.ReadOnly = Status
        'txtANAMNESIS_28.Properties.ReadOnly = Status
        txtANAMNESIS_29.Properties.ReadOnly = Status
        txtANAMNESIS_30.Properties.ReadOnly = Status
        txtANAMNESIS_31.Properties.ReadOnly = Status
        txtANAMNESIS_32.Properties.ReadOnly = Status
        txtANAMNESIS_33.Properties.ReadOnly = Status
        txtANAMNESIS_34.Properties.ReadOnly = Status
        txtANAMNESIS_35.Properties.ReadOnly = Status
        txtANAMNESIS_36.Properties.ReadOnly = Status
        txtANAMNESIS_37.Properties.ReadOnly = Status
        txtANAMNESIS_38.Properties.ReadOnly = Status
        cboRencanaPerawatan.Properties.ReadOnly = Status

        chkANAMNESIS_15_1_CHEK.Properties.ReadOnly = Status
        chkANAMNESIS_16_1_CHEK.Properties.ReadOnly = Status
        chkANAMNESIS_17_1_CHEK.Properties.ReadOnly = Status
        chkANAMNESIS_18_1_CHEK.Properties.ReadOnly = Status
        chkANAMNESIS_19_1_CHEK.Properties.ReadOnly = Status
        chkANAMNESIS_20_1_CHEK.Properties.ReadOnly = Status
        chkANAMNESIS_15_2_CHEK.Properties.ReadOnly = Status
        chkANAMNESIS_16_2_CHEK.Properties.ReadOnly = Status
        chkANAMNESIS_17_2_CHEK.Properties.ReadOnly = Status
        chkANAMNESIS_18_2_CHEK.Properties.ReadOnly = Status
        chkANAMNESIS_19_2_CHEK.Properties.ReadOnly = Status
        chkANAMNESIS_20_2_CHEK.Properties.ReadOnly = Status
    End Sub
    Private Sub fn_EmptyMe()
        'txtCODE.Text = "<--- AUTO --->"

        deDATE.DateTime = Now
        'txtJAM.Text = Now.ToString("HH:mm")
        'grdDOCTOR2.ResetText()
        txtANAMNESIS_01.ResetText()
        txtANAMNESIS_02.ResetText()
        txtANAMNESIS_03.ResetText()
        txtANAMNESIS_04.ResetText()
        txtANAMNESIS_05.ResetText()
        txtANAMNESIS_06.ResetText()
        txtANAMNESIS_07.ResetText()
        txtANAMNESIS_08.ResetText()
        txtANAMNESIS_09.ResetText()
        txtANAMNESIS_10.ResetText()
        txtANAMNESIS_11.ResetText()
        txtANAMNESIS_12.ResetText()
        txtANAMNESIS_13.ResetText()
        txtANAMNESIS_14.ResetText()
        txtANAMNESIS_15.ResetText()
        txtANAMNESIS_16.ResetText()
        txtANAMNESIS_17.ResetText()
        txtANAMNESIS_18.ResetText()
        txtANAMNESIS_19.ResetText()
        txtANAMNESIS_20.ResetText()
        'txtANAMNESIS_21.ResetText()
        'txtANAMNESIS_22.ResetText()
        'txtANAMNESIS_23.ResetText()
        'txtANAMNESIS_24.ResetText()
        'txtANAMNESIS_25.ResetText()
        'txtANAMNESIS_26.ResetText()
        'txtANAMNESIS_27.ResetText()
        'txtANAMNESIS_28.ResetText()
        txtANAMNESIS_29.ResetText()
        txtANAMNESIS_30.ResetText()
        txtANAMNESIS_31.ResetText()
        txtANAMNESIS_32.ResetText()
        txtANAMNESIS_33.ResetText()
        txtANAMNESIS_34.ResetText()
        txtANAMNESIS_35.ResetText()
        txtANAMNESIS_36.ResetText()
        txtANAMNESIS_37.ResetText()
        txtANAMNESIS_38.ResetText()
        cboRencanaPerawatan.ResetText()
        grdDPJP.Text = sKDDOCTOR

        chkANAMNESIS_15_1_CHEK.Checked = False
        chkANAMNESIS_16_1_CHEK.Checked = False
        chkANAMNESIS_17_1_CHEK.Checked = False
        chkANAMNESIS_18_1_CHEK.Checked = False
        chkANAMNESIS_19_1_CHEK.Checked = False
        chkANAMNESIS_20_1_CHEK.Checked = False
        chkANAMNESIS_15_2_CHEK.Checked = False
        chkANAMNESIS_16_2_CHEK.Checked = False
        chkANAMNESIS_17_2_CHEK.Checked = False
        chkANAMNESIS_18_2_CHEK.Checked = False
        chkANAMNESIS_19_2_CHEK.Checked = False
        chkANAMNESIS_20_2_CHEK.Checked = False
    End Sub
    Private Sub fn_LoadData()
        Try
            ' ***** HEADER *****
            Dim ds = oS_DIGITAL_RI_13.GetData(txtNoRegister.Text)

            With ds
                deDATE.DateTime = .DATE
                'txtJAM.Text = .JAM
                grdDPJP.Text = .DOCTOR_KODE
                txtANAMNESIS_01.Text = .ANAMNESIS_01
                txtANAMNESIS_02.Text = .ANAMNESIS_02
                txtANAMNESIS_03.Text = .ANAMNESIS_03
                txtANAMNESIS_04.Text = .ANAMNESIS_04
                txtANAMNESIS_05.Text = .ANAMNESIS_05
                txtANAMNESIS_06.Text = .ANAMNESIS_06
                txtANAMNESIS_07.Text = .ANAMNESIS_07
                txtANAMNESIS_08.Text = .ANAMNESIS_08
                txtANAMNESIS_09.Text = .ANAMNESIS_09
                txtANAMNESIS_10.Text = .ANAMNESIS_10
                txtANAMNESIS_11.Text = .ANAMNESIS_11
                txtANAMNESIS_12.Text = .ANAMNESIS_12
                txtANAMNESIS_13.Text = .ANAMNESIS_13
                txtANAMNESIS_14.Text = .ANAMNESIS_14
                txtANAMNESIS_15.Text = .ANAMNESIS_15
                txtANAMNESIS_16.Text = .ANAMNESIS_16
                txtANAMNESIS_17.Text = .ANAMNESIS_17
                txtANAMNESIS_18.Text = .ANAMNESIS_18
                txtANAMNESIS_19.Text = .ANAMNESIS_19
                txtANAMNESIS_20.Text = .ANAMNESIS_20
                'txtANAMNESIS_21.Text = .ANAMNESIS_21
                'txtANAMNESIS_22.Text = .ANAMNESIS_22
                'txtANAMNESIS_23.Text = .ANAMNESIS_23
                'txtANAMNESIS_24.Text = .ANAMNESIS_24
                'txtANAMNESIS_25.Text = .ANAMNESIS_25
                'txtANAMNESIS_26.Text = .ANAMNESIS_26
                'txtANAMNESIS_27.Text = .ANAMNESIS_27
                'txtANAMNESIS_28.Text = .ANAMNESIS_28
                txtANAMNESIS_29.Text = .ANAMNESIS_29
                txtANAMNESIS_30.Text = .ANAMNESIS_30
                txtANAMNESIS_31.Text = .ANAMNESIS_31
                txtANAMNESIS_32.Text = .ANAMNESIS_32
                txtANAMNESIS_33.Text = .ANAMNESIS_33
                txtANAMNESIS_34.Text = .ANAMNESIS_34
                txtANAMNESIS_35.Text = .ANAMNESIS_35
                txtANAMNESIS_36.Text = .ANAMNESIS_36
                txtANAMNESIS_37.Text = .ANAMNESIS_37
                txtANAMNESIS_38.Text = .ANAMNESIS_38
                cboRencanaPerawatan.Text = .HARI

                chkANAMNESIS_15_1_CHEK.Checked = .ANAMNESIS_15_1_CHEK
                chkANAMNESIS_16_1_CHEK.Checked = .ANAMNESIS_16_1_CHEK
                chkANAMNESIS_17_1_CHEK.Checked = .ANAMNESIS_17_1_CHEK
                chkANAMNESIS_18_1_CHEK.Checked = .ANAMNESIS_18_1_CHEK
                chkANAMNESIS_19_1_CHEK.Checked = .ANAMNESIS_19_1_CHEK
                chkANAMNESIS_20_1_CHEK.Checked = .ANAMNESIS_20_1_CHEK
                chkANAMNESIS_15_2_CHEK.Checked = .ANAMNESIS_15_2_CHEK
                chkANAMNESIS_16_2_CHEK.Checked = .ANAMNESIS_16_2_CHEK
                chkANAMNESIS_17_2_CHEK.Checked = .ANAMNESIS_17_2_CHEK
                chkANAMNESIS_18_2_CHEK.Checked = .ANAMNESIS_18_2_CHEK
                chkANAMNESIS_19_2_CHEK.Checked = .ANAMNESIS_19_2_CHEK
                chkANAMNESIS_20_2_CHEK.Checked = .ANAMNESIS_20_2_CHEK
            End With
        Catch oErr As Exception
            MsgBox("Load List Data : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Function fn_Validate() As Boolean
        Try
            fn_Validate = True
            If txtNoRegister.Text = String.Empty Then
                MsgBox("Dibutuhkan Register", MsgBoxStyle.Exclamation, Me.Text)
                txtNoRegister.Focus()
                fn_Validate = False
                Exit Function
            End If
            If cboRencanaPerawatan.Text = String.Empty Then
                MsgBox("Dibutuhkan Rencana Perawatan", MsgBoxStyle.Exclamation, Me.Text)
                cboRencanaPerawatan.Focus()
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
            Dim ds = oS_DIGITAL_RI_13.GetStructureHeader
            With ds
                .KDREG = txtNoRegister.Text
                .KDCUSTOMER = txtNoPasien.Text
                Try
                    .DATECREATED = oS_DIGITAL_RI_13.GetData(txtNoRegister.Text).DATECREATED
                Catch ex As Exception
                    .DATECREATED = Now
                End Try
                .DATEUPDATED = Now

                .DATE = deDATE.DateTime
                .JAM = deDATE.DateTime.ToString("HH:mm")
                .DOCTOR_KODE = grdDPJP.EditValue
                .DOCTOR_NAME_DISPLAY = grdDPJP.Text
                .ANAMNESIS_01 = txtANAMNESIS_01.Text
                .ANAMNESIS_02 = txtANAMNESIS_02.Text
                .ANAMNESIS_03 = txtANAMNESIS_03.Text
                .ANAMNESIS_04 = txtANAMNESIS_04.Text
                .ANAMNESIS_05 = txtANAMNESIS_05.Text
                .ANAMNESIS_06 = txtANAMNESIS_06.Text
                .ANAMNESIS_07 = txtANAMNESIS_07.Text
                .ANAMNESIS_08 = txtANAMNESIS_08.Text
                .ANAMNESIS_09 = txtANAMNESIS_09.Text
                .ANAMNESIS_10 = txtANAMNESIS_10.Text
                .ANAMNESIS_11 = txtANAMNESIS_11.Text
                .ANAMNESIS_12 = txtANAMNESIS_12.Text
                .ANAMNESIS_13 = txtANAMNESIS_13.Text
                .ANAMNESIS_14 = txtANAMNESIS_14.Text
                .ANAMNESIS_15 = txtANAMNESIS_15.Text
                .ANAMNESIS_16 = txtANAMNESIS_16.Text
                .ANAMNESIS_17 = txtANAMNESIS_17.Text
                .ANAMNESIS_18 = txtANAMNESIS_18.Text
                .ANAMNESIS_19 = txtANAMNESIS_19.Text
                .ANAMNESIS_20 = txtANAMNESIS_20.Text
                Try
                    Dim getdata = oS_DIGITAL_RI_13.GetData(txtNoRegister.Text)
                    .ANAMNESIS_21 = getdata.ANAMNESIS_15 & ", " & getdata.ANAMNESIS_16 & ", " & getdata.ANAMNESIS_17 & ", " & getdata.ANAMNESIS_18 & ", " & getdata.ANAMNESIS_19 & ", " & getdata.ANAMNESIS_20
                Catch ex As Exception
                    .ANAMNESIS_21 = ""
                End Try
                .ANAMNESIS_22 = ""
                .ANAMNESIS_23 = ""
                .ANAMNESIS_24 = ""
                .ANAMNESIS_25 = ""
                .ANAMNESIS_26 = ""
                .ANAMNESIS_27 = ""
                .ANAMNESIS_28 = ""
                .ANAMNESIS_29 = txtANAMNESIS_29.Text
                .ANAMNESIS_30 = txtANAMNESIS_30.Text
                .ANAMNESIS_31 = txtANAMNESIS_31.Text
                .ANAMNESIS_32 = txtANAMNESIS_32.Text
                .ANAMNESIS_33 = txtANAMNESIS_33.Text
                .ANAMNESIS_34 = txtANAMNESIS_34.Text
                .ANAMNESIS_35 = txtANAMNESIS_35.Text
                .ANAMNESIS_36 = txtANAMNESIS_36.Text
                .ANAMNESIS_37 = txtANAMNESIS_37.Text
                .ANAMNESIS_38 = txtANAMNESIS_38.Text
                Try
                    .CETAK = oS_DIGITAL_RI_13.GetData(txtNoRegister.Text).CETAK
                Catch ex As Exception
                    .CETAK = 0
                End Try
                .KDUSER = sUserID
                .KDUSER_SIGNATURE = ""
                .HARI = cboRencanaPerawatan.Text
                .ANAMNESIS_15_1_CHEK = chkANAMNESIS_15_1_CHEK.Checked
                .ANAMNESIS_16_1_CHEK = chkANAMNESIS_16_1_CHEK.Checked
                .ANAMNESIS_17_1_CHEK = chkANAMNESIS_17_1_CHEK.Checked
                .ANAMNESIS_18_1_CHEK = chkANAMNESIS_18_1_CHEK.Checked
                .ANAMNESIS_19_1_CHEK = chkANAMNESIS_19_1_CHEK.Checked
                .ANAMNESIS_20_1_CHEK = chkANAMNESIS_20_1_CHEK.Checked
                .ANAMNESIS_15_2_CHEK = chkANAMNESIS_15_2_CHEK.Checked
                .ANAMNESIS_16_2_CHEK = chkANAMNESIS_16_2_CHEK.Checked
                .ANAMNESIS_17_2_CHEK = chkANAMNESIS_17_2_CHEK.Checked
                .ANAMNESIS_18_2_CHEK = chkANAMNESIS_18_2_CHEK.Checked
                .ANAMNESIS_19_2_CHEK = chkANAMNESIS_19_2_CHEK.Checked
                .ANAMNESIS_20_2_CHEK = chkANAMNESIS_20_2_CHEK.Checked
            End With

            If oFormMode = FORM_MODE.FORM_MODE_ADD Then
                Try
                    fn_Save = oS_DIGITAL_RI_13.InsertData(ds, Nothing)
                Catch ex As Exception
                    MsgBox("Simpan Data : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            ElseIf oFormMode = FORM_MODE.FORM_MODE_EDIT Then
                Try
                    fn_Save = oS_DIGITAL_RI_13.UpdateData(ds, Nothing)
                Catch ex As Exception
                    MsgBox("Simpan Data : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            End If
        Catch oErr As Exception
            MsgBox("Simpan Data : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            fn_Save = False
        End Try
    End Function
    Private Sub fn_LoadDataTemplate(ByVal kode As String)
        Try
            ' ***** HEADER *****
            Dim ds = oS_DIGITAL_RI_13_Template.GetData(kode)

            With ds
                deDATE.DateTime = .DATE
                'grdDPJP.Text = .DOCTOR_KODE
                txtANAMNESIS_01.Text = .ANAMNESIS_01
                txtANAMNESIS_02.Text = .ANAMNESIS_02
                txtANAMNESIS_03.Text = .ANAMNESIS_03
                txtANAMNESIS_04.Text = .ANAMNESIS_04
                txtANAMNESIS_05.Text = .ANAMNESIS_05
                txtANAMNESIS_06.Text = .ANAMNESIS_06
                txtANAMNESIS_07.Text = .ANAMNESIS_07
                txtANAMNESIS_08.Text = .ANAMNESIS_08
                txtANAMNESIS_09.Text = .ANAMNESIS_09
                txtANAMNESIS_10.Text = .ANAMNESIS_10
                txtANAMNESIS_11.Text = .ANAMNESIS_11
                txtANAMNESIS_12.Text = .ANAMNESIS_12
                txtANAMNESIS_13.Text = .ANAMNESIS_13
                txtANAMNESIS_14.Text = .ANAMNESIS_14
                txtANAMNESIS_15.Text = .ANAMNESIS_15
                txtANAMNESIS_16.Text = .ANAMNESIS_16
                txtANAMNESIS_17.Text = .ANAMNESIS_17
                txtANAMNESIS_18.Text = .ANAMNESIS_18
                txtANAMNESIS_19.Text = .ANAMNESIS_19
                txtANAMNESIS_20.Text = .ANAMNESIS_20
                'txtANAMNESIS_21.Text = .ANAMNESIS_21
                'txtANAMNESIS_22.Text = .ANAMNESIS_22
                'txtANAMNESIS_23.Text = .ANAMNESIS_23
                'txtANAMNESIS_24.Text = .ANAMNESIS_24
                'txtANAMNESIS_25.Text = .ANAMNESIS_25
                'txtANAMNESIS_26.Text = .ANAMNESIS_26
                'txtANAMNESIS_27.Text = .ANAMNESIS_27
                'txtANAMNESIS_28.Text = .ANAMNESIS_28
                txtANAMNESIS_29.Text = .ANAMNESIS_29
                txtANAMNESIS_30.Text = .ANAMNESIS_30
                txtANAMNESIS_31.Text = .ANAMNESIS_31
                txtANAMNESIS_32.Text = .ANAMNESIS_32
                txtANAMNESIS_33.Text = .ANAMNESIS_33
                txtANAMNESIS_34.Text = .ANAMNESIS_34
                txtANAMNESIS_35.Text = .ANAMNESIS_35
                txtANAMNESIS_36.Text = .ANAMNESIS_36
                txtANAMNESIS_37.Text = .ANAMNESIS_37
                txtANAMNESIS_38.Text = .ANAMNESIS_38
                cboRencanaPerawatan.Text = .HARI

                chkANAMNESIS_15_1_CHEK.Checked = .ANAMNESIS_15_1_CHEK
                chkANAMNESIS_16_1_CHEK.Checked = .ANAMNESIS_16_1_CHEK
                chkANAMNESIS_17_1_CHEK.Checked = .ANAMNESIS_17_1_CHEK
                chkANAMNESIS_18_1_CHEK.Checked = .ANAMNESIS_18_1_CHEK
                chkANAMNESIS_19_1_CHEK.Checked = .ANAMNESIS_19_1_CHEK
                chkANAMNESIS_20_1_CHEK.Checked = .ANAMNESIS_20_1_CHEK
                chkANAMNESIS_15_2_CHEK.Checked = .ANAMNESIS_15_2_CHEK
                chkANAMNESIS_16_2_CHEK.Checked = .ANAMNESIS_16_2_CHEK
                chkANAMNESIS_17_2_CHEK.Checked = .ANAMNESIS_17_2_CHEK
                chkANAMNESIS_18_2_CHEK.Checked = .ANAMNESIS_18_2_CHEK
                chkANAMNESIS_19_2_CHEK.Checked = .ANAMNESIS_19_2_CHEK
                chkANAMNESIS_20_2_CHEK.Checked = .ANAMNESIS_20_2_CHEK
            End With
        Catch oErr As Exception
            MsgBox("Load List Data : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Function fn_SaveTemplate() As Boolean
        Try
            Dim oAdd As Boolean = True

            ' ***** HEADER *****
            Dim ds = oS_DIGITAL_RI_13_Template.GetStructureHeader
            With ds
                .KDJUDUL = sKODEASESMENCOPY
                Try
                    .DATECREATED = oS_DIGITAL_RI_13_Template.GetData(sKODEASESMENCOPY).DATECREATED
                Catch ex As Exception
                    oAdd = False
                    .DATECREATED = Now
                End Try
                .DATEUPDATED = Now

                .DATE = deDATE.DateTime
                .JAM = deDATE.DateTime.ToString("HH:mm")
                .DOCTOR_KODE = ""
                .DOCTOR_NAME_DISPLAY = ""
                .ANAMNESIS_01 = txtANAMNESIS_01.Text
                .ANAMNESIS_02 = txtANAMNESIS_02.Text
                .ANAMNESIS_03 = txtANAMNESIS_03.Text
                .ANAMNESIS_04 = txtANAMNESIS_04.Text
                .ANAMNESIS_05 = txtANAMNESIS_05.Text
                .ANAMNESIS_06 = txtANAMNESIS_06.Text
                .ANAMNESIS_07 = txtANAMNESIS_07.Text
                .ANAMNESIS_08 = txtANAMNESIS_08.Text
                .ANAMNESIS_09 = txtANAMNESIS_09.Text
                .ANAMNESIS_10 = txtANAMNESIS_10.Text
                .ANAMNESIS_11 = txtANAMNESIS_11.Text
                .ANAMNESIS_12 = txtANAMNESIS_12.Text
                .ANAMNESIS_13 = txtANAMNESIS_13.Text
                .ANAMNESIS_14 = txtANAMNESIS_14.Text
                .ANAMNESIS_15 = txtANAMNESIS_15.Text
                .ANAMNESIS_16 = txtANAMNESIS_16.Text
                .ANAMNESIS_17 = txtANAMNESIS_17.Text
                .ANAMNESIS_18 = txtANAMNESIS_18.Text
                .ANAMNESIS_19 = txtANAMNESIS_19.Text
                .ANAMNESIS_20 = txtANAMNESIS_20.Text
                Try
                    Dim getdata = oS_DIGITAL_RI_13_Template.GetData(sKODEASESMENCOPY)
                    .ANAMNESIS_21 = getdata.ANAMNESIS_15 & ", " & getdata.ANAMNESIS_16 & ", " & getdata.ANAMNESIS_17 & ", " & getdata.ANAMNESIS_18 & ", " & getdata.ANAMNESIS_19 & ", " & getdata.ANAMNESIS_20
                Catch ex As Exception
                    .ANAMNESIS_21 = ""
                End Try
                .ANAMNESIS_22 = ""
                .ANAMNESIS_23 = ""
                .ANAMNESIS_24 = ""
                .ANAMNESIS_25 = ""
                .ANAMNESIS_26 = ""
                .ANAMNESIS_27 = ""
                .ANAMNESIS_28 = ""
                .ANAMNESIS_29 = txtANAMNESIS_29.Text
                .ANAMNESIS_30 = txtANAMNESIS_30.Text
                .ANAMNESIS_31 = txtANAMNESIS_31.Text
                .ANAMNESIS_32 = txtANAMNESIS_32.Text
                .ANAMNESIS_33 = txtANAMNESIS_33.Text
                .ANAMNESIS_34 = txtANAMNESIS_34.Text
                .ANAMNESIS_35 = txtANAMNESIS_35.Text
                .ANAMNESIS_36 = txtANAMNESIS_36.Text
                .ANAMNESIS_37 = txtANAMNESIS_37.Text
                .ANAMNESIS_38 = txtANAMNESIS_38.Text
                Try
                    .CETAK = oS_DIGITAL_RI_13_Template.GetData(sKODEASESMENCOPY).CETAK
                Catch ex As Exception
                    .CETAK = 0
                End Try
                .KDUSER = sUserID
                .KDUSER_SIGNATURE = ""
                .HARI = cboRencanaPerawatan.Text
                .ANAMNESIS_15_1_CHEK = chkANAMNESIS_15_1_CHEK.Checked
                .ANAMNESIS_16_1_CHEK = chkANAMNESIS_16_1_CHEK.Checked
                .ANAMNESIS_17_1_CHEK = chkANAMNESIS_17_1_CHEK.Checked
                .ANAMNESIS_18_1_CHEK = chkANAMNESIS_18_1_CHEK.Checked
                .ANAMNESIS_19_1_CHEK = chkANAMNESIS_19_1_CHEK.Checked
                .ANAMNESIS_20_1_CHEK = chkANAMNESIS_20_1_CHEK.Checked
                .ANAMNESIS_15_2_CHEK = chkANAMNESIS_15_2_CHEK.Checked
                .ANAMNESIS_16_2_CHEK = chkANAMNESIS_16_2_CHEK.Checked
                .ANAMNESIS_17_2_CHEK = chkANAMNESIS_17_2_CHEK.Checked
                .ANAMNESIS_18_2_CHEK = chkANAMNESIS_18_2_CHEK.Checked
                .ANAMNESIS_19_2_CHEK = chkANAMNESIS_19_2_CHEK.Checked
                .ANAMNESIS_20_2_CHEK = chkANAMNESIS_20_2_CHEK.Checked
            End With

            If oAdd = False Then
                Try
                    sKODEASESMENCOPY = oS_DIGITAL_RI_13_Template.InsertData(ds)

                    If sKODEASESMENCOPY = "" Then
                        fn_SaveTemplate = False
                    Else
                        fn_SaveTemplate = True
                    End If
                Catch ex As Exception
                    MsgBox("Simpan Data : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            Else
                Try
                    fn_SaveTemplate = oS_DIGITAL_RI_13_Template.UpdateData(ds)
                Catch ex As Exception
                    MsgBox("Simpan Data : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            End If
        Catch oErr As Exception
            MsgBox("Simpan Data : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            fn_SaveTemplate = False
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
    Private Sub chkNORMAL_KEPALA_CheckedChanged(sender As Object, e As EventArgs) Handles chkANAMNESIS_15_1_CHEK.Click, chkANAMNESIS_15_2_CHEK.Click
        chkANAMNESIS_15_1_CHEK.Checked = False
        chkANAMNESIS_15_2_CHEK.Checked = False
    End Sub
    Private Sub chkNORMAL_MATA_CheckedChanged(sender As Object, e As EventArgs) Handles chkANAMNESIS_16_1_CHEK.Click, chkANAMNESIS_16_2_CHEK.Click
        chkANAMNESIS_16_1_CHEK.Checked = False
        chkANAMNESIS_16_2_CHEK.Checked = False
    End Sub
    Private Sub chkNORMAL_LEHER_CheckedChanged(sender As Object, e As EventArgs) Handles chkANAMNESIS_17_1_CHEK.Click, chkANAMNESIS_17_2_CHEK.Click
        chkANAMNESIS_17_1_CHEK.Checked = False
        chkANAMNESIS_17_2_CHEK.Checked = False
    End Sub
    Private Sub chkNORMAL_DADA_CheckedChanged(sender As Object, e As EventArgs) Handles chkANAMNESIS_18_1_CHEK.Click, chkANAMNESIS_18_2_CHEK.Click
        chkANAMNESIS_18_1_CHEK.Checked = False
        chkANAMNESIS_18_2_CHEK.Checked = False
    End Sub
    Private Sub chkNORMAL_PERUT_CheckedChanged(sender As Object, e As EventArgs) Handles chkANAMNESIS_19_1_CHEK.Click, chkANAMNESIS_19_2_CHEK.Click
        chkANAMNESIS_19_1_CHEK.Checked = False
        chkANAMNESIS_19_2_CHEK.Checked = False
    End Sub
    Private Sub chkNORMAL_ALATGERAK_CheckedChanged(sender As Object, e As EventArgs) Handles chkANAMNESIS_20_1_CHEK.Click, chkANAMNESIS_20_2_CHEK.Click
        chkANAMNESIS_20_1_CHEK.Checked = False
        chkANAMNESIS_20_2_CHEK.Checked = False
    End Sub
#End Region
#Region "Lookup / Event"
    Private Sub fn_NOIDUSER()
        'Dim oUser As New Setting.clsUser
        'Try
        '    grdNOIDUSER.Properties.DataSource = oUser.GetData.Where(Function(x) x.ISACTIVE = True).ToList()
        '    grdNOIDUSER.Properties.ValueMember = "NOIDUSER"
        '    grdNOIDUSER.Properties.DisplayMember = "NOIDUSER"
        'Catch oErr As Exception
        '    MsgBox("Load Sub Spesialis Data : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        'End Try
    End Sub
    Private Sub fn_Doctor()
        Try
            Dim oDoctor As New Reference.clsDoctor

            'Dim dsDoctor = From x In oDoctor.GetData
            '               Where x.ISACTIVE = True
            '               Select x.KDDOCTOR, x.NAME_DISPLAY

            'grdDPJPUtama.Properties.DataSource = dsDoctor.ToList()
            'grdDPJPUtama.Properties.ValueMember = "KDDOCTOR"
            'grdDPJPUtama.Properties.DisplayMember = "NAME_DISPLAY"

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

            grdDPJP.Properties.DataSource = ds.Tables("M_DOCTOR")
            grdDPJP.Properties.ValueMember = "KDDOCTOR"
            grdDPJP.Properties.DisplayMember = "NAME_DISPLAY"

            If oConn.State = ConnectionState.Open Then
                oConn.Close()
            End If

        Catch oErr As Exception
            MsgBox("Load Dokter Data : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub frmEMedrekRI_13_MouseWheel(sender As Object, e As MouseEventArgs) Handles MyBase.MouseWheel
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
    Private Sub btnCPPT_Click(sender As Object, e As EventArgs) Handles btnCPPT.Click
        Dim frmBrowseLoadCPPT As New frmBrowseLoadCPPT
        Try
            frmBrowseLoadCPPT.fn_LoadMe(txtNoRegister.Text)
            frmBrowseLoadCPPT.ShowDialog(Me)

            LoadCPPT(sKDCPPT)
        Catch oErr As Exception
            MsgBox("Form Browse Load CPPT" & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub SimpleButton1_Click(sender As Object, e As EventArgs) Handles SimpleButton1.Click
        LoadAsesmenIGD(sKDREG_RJ)
    End Sub
    Private Sub LoadCPPT(ByVal Kode As String)
        Try
            Dim oCPPT As New Transaksi.clsCPPT

            Dim ds = oCPPT.GetData(Kode)
            If ds IsNot Nothing Then
                txtANAMNESIS_01.Text = ds.SUBJEKTIF

                Dim dsLainnya = oCPPT.GetDataLainnya(ds.KDCPPT)
                If dsLainnya IsNot Nothing Then
                    txtANAMNESIS_08.Text = dsLainnya.OBJEKTIF_BERATBADAN
                    txtANAMNESIS_09.Text = dsLainnya.OBJEKTIF_TINGGIBADAN
                    txtANAMNESIS_11.Text = dsLainnya.OBJEKTIF_NADI
                    txtANAMNESIS_12.Text = dsLainnya.OBJEKTIF_TEKANANDARAH
                    txtANAMNESIS_13.Text = dsLainnya.OBJEKTIF_SUHU
                    txtANAMNESIS_14.Text = dsLainnya.OBJEKTIF_RESPIRASI
                End If

                txtANAMNESIS_35.Text = ds.ASSEMENT
                txtANAMNESIS_36.Text = ds.PLANNING
            End If
        Catch oErr As Exception
            MsgBox("Form Browse Load CPPT" & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub LoadAsesmenIGD(ByVal Kode As String)
        Dim oS_DIGITAL_IGD_01 As New Transaksi.clsDigital_IGD_01
        Try
            Dim ds = oS_DIGITAL_IGD_01.GetData(Kode)
            If ds IsNot Nothing Then
                txtANAMNESIS_01.Text = ds.RIWAYAT_PENYAKITDAHULU
                txtANAMNESIS_02.Text = ds.RIWAYAT
                txtANAMNESIS_05.Text = ds.KEADAANUMUM
                txtANAMNESIS_06.Text = ds.TINGKATKESADARAN
                txtANAMNESIS_08.Text = ds.BERATBADAN
                txtANAMNESIS_09.Text = ds.TINGGIBADAN
                txtANAMNESIS_11.Text = ds.HR
                txtANAMNESIS_12.Text = ds.BP
                txtANAMNESIS_13.Text = ds.T
                txtANAMNESIS_14.Text = ds.RR

                txtANAMNESIS_35.Text = ds.KDDIAGNOSA

                txtANAMNESIS_36.Text = ds.OBATSAATPULANG

                Dim list As New List(Of String)

                For Each xloop In oS_DIGITAL_IGD_01.GetDataDetailPenunjang(ds.KDPENDAFTARAN)
                    list.Add(xloop.PENANGANAN)
                Next

                txtANAMNESIS_33.Text = String.Join(", ", list.ToArray)
            End If

        Catch oErr As Exception
            MsgBox("Form Browse Load Assemen Medis Awal IGD" & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub btnLoadData_Click(sender As Object, e As EventArgs) Handles btnLoadData.Click
        Dim frmListTemplate As New frmListTemplate
        Try
            sKODEASESMENCOPY = String.Empty

            frmListTemplate.fn_LoadKategori(3, "")
            frmListTemplate.ShowDialog(Me)

            If sKODEASESMENCOPY <> "" Then
                fn_LoadDataTemplate(sKODEASESMENCOPY)
            End If
        Catch oErr As Exception
            MsgBox("Load Data Template" & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        sRemarks = String.Empty
        frmJudulTemplate.ShowDialog(Me)

        If sRemarks = "" Then
            MsgBox("Judul Template Masih Kosong", MsgBoxStyle.Exclamation, Me.Text)
        Else
            sKODEASESMENCOPY = sRemarks

            If MsgBox("Save " & sKODEASESMENCOPY & "?", MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, Me.Text) = MsgBoxResult.No Then Exit Sub
            If fn_SaveTemplate() = False Then
                sKODEASESMENCOPY = ""
                MsgBox("Save gagal! Tolong kontak admin anda!", MsgBoxStyle.Exclamation, Me.Text)
            Else
                MsgBox("Save " & sKODEASESMENCOPY & " success!", MsgBoxStyle.Information, Me.Text)
            End If
        End If
    End Sub
    Private Sub btnSaveAs_Click(sender As Object, e As EventArgs) Handles btnSaveAs.Click
        If sKODEASESMENCOPY = "" Then
            MsgBox("Silahkan Load Data Terlebih Dahulu", MsgBoxStyle.Exclamation, Me.Text)
            Exit Sub
        End If

        If MsgBox("Save As " & sKODEASESMENCOPY & "?", MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, Me.Text) = MsgBoxResult.No Then Exit Sub
        If fn_SaveTemplate() = False Then
            MsgBox("Save As gagal! Tolong kontak admin anda!", MsgBoxStyle.Exclamation, Me.Text)
        Else
            MsgBox("Save As " & sKODEASESMENCOPY & " success!", MsgBoxStyle.Information, Me.Text)
        End If
    End Sub
#End Region
End Class