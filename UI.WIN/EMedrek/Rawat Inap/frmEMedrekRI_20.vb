Imports DataAccess
Imports UI.WIN.MAIN.My.Resources
Imports System.Linq
Imports System.Data.SqlClient

Public Class frmEMedrekRI_20
#Region "Declaration"
    Private oFormMode As FORM_MODE = FORM_MODE.FORM_MODE_VIEW
    Private isLoad As Boolean = False
    Private oS_DIGITAL_RI_20 As New Digital.clsDigital_RI_20

#End Region
#Region "Function"
    Public Sub LoadMe(ByVal FormMode As Integer, ByVal KDREG As String, ByVal KDCUSTOMER As String, ByVal NAMAPASIEN As String, ByVal TANGGALLAHIR As String, ByVal KDUSER_PERAWAT As String, ByVal KDDPJP As String, ByVal RUANGAN As String)
        oFormMode = FormMode
        txtKDKREG.Text = KDREG
        txtNOPASIEN.Text = KDCUSTOMER
        txtNAMAPASIEN.Text = NAMAPASIEN
        txtTANGGALLAHIR.Text = TANGGALLAHIR
        txtRUANGRAWAT2.Text = RUANGAN
        grdDOKTER.EditValue = KDDPJP
        txtUSERPERAWAT.Text = KDUSER_PERAWAT
    End Sub
    Private Sub Form_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        fn_ChangeFormState()
        isLoad = True
    End Sub
    Private Sub Form_Closing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        sCode = txtKDKREG.Text
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
        btnSaveClose.Enabled = Not Status

    End Sub
    Private Sub fn_EmptyMe()
        deDATE.DateTime = Now
        txtDIIT.ResetText()
        txtBB.ResetText()
        txtTB.ResetText()
        txtIMT.ResetText()
        txtSTATUSGIZI.ResetText()
        txtLLA.ResetText()
        txtINDIKATOR1_SKOR1.ResetText()
        txtINDIKATOR1_SKOR2.ResetText()
        txtINDIKATOR1_SKOR3.ResetText()
        txtINDIKATOR1_SKOR4.ResetText()
        txtINDIKATOR1_SKOR5.ResetText()
        txtINDIKATOR2_SKOR1.ResetText()
        txtINDIKATOR2_SKOR2.ResetText()
        txtINDIKATOR2_SKOR3.ResetText()
        txtINDIKATOR2_SKOR4.ResetText()
        'txtMUAL.ResetText()
        'txtMUNTAH.ResetText()
        'txtDIARE.ResetText()
        'txtANOREKSIA.ResetText()
        CheckEdit27.Checked = False
        CheckEdit28.Checked = False
        CheckEdit29.Checked = False
        CheckEdit30.Checked = False
        txtINDIKATOR3_SKOR1.ResetText()
        txtINDIKATOR3_SKOR2.ResetText()
        txtINDIKATOR3_SKOR3.ResetText()
        txtINDIKATOR4_SKOR1.ResetText()
        txtINDIKATOR4_SKOR2.ResetText()
        txtINDIKATOR4_SKOR3.ResetText()
        txtINDIKATOR5_SKOR1.ResetText()
        txtINDIKATOR5_SKOR2.ResetText()
        txtINDIKATOR5_SKOR3.ResetText()
        txtINDIKATORPF1_SKOR1.ResetText()
        txtINDIKATORPF1_SKOR2.ResetText()
        txtINDIKATORPF1_SKOR3.ResetText()
        txtINDIKATORPF2_SKOR1.ResetText()
        txtINDIKATORPF2_SKOR2.ResetText()
        txtINDIKATORPF2_SKOR3.ResetText()
        txtINDIKATORPF3_SKOR1.ResetText()
        txtINDIKATORPF3_SKOR2.ResetText()
        txtINDIKATORPF3_SKOR3.ResetText()
        txtINDIKATORPF4_SKOR1.ResetText()
        txtINDIKATORPF4_SKOR2.ResetText()
        txtINDIKATORPF4_SKOR3.ResetText()
        'chkPENILAIAN_1.CheckState = False
        CheckEdit23.CheckState = False

        'txtMATERI.ResetText()
        'txtLEAFLET.ResetText()
        'txtDATA_PENTING.ResetText()
        TEXTEDIT_1.ResetText()
        TEXTEDIT_2.ResetText()
        TEXTEDIT_3.ResetText()
        TEXTEDIT_4.SelectedIndex = 0
        'TEXTEDIT_5.ResetText()
        'TEXTEDIT_6.ResetText()
        'TEXTEDIT_7.ResetText()

        chkKeseluruhanSkor_A.Checked = False
        chkKeseluruhanSkor_B.Checked = False
        chkKeseluruhanSkor_C.Checked = False
        chkKeseluruhanSkor_A_2.Checked = False
        chkKeseluruhanSkor_B_2.Checked = False
        chkKeseluruhanSkor_C_2.Checked = False

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
        CheckEdit21.Checked = False
        CheckEdit22.Checked = False

        chkCeklis_07.Checked = False
        chkCeklis_08.Checked = False
        chkCeklis_09.Checked = False
    End Sub
    Private Sub fn_LoadData()
        Try
            ' ***** HEADER *****
            Dim ds = oS_DIGITAL_RI_20.GetData(txtKDKREG.Text)
            With ds
                deDATE.DateTime = .DATE
                txtRUANGRAWAT2.Text = ds.RUANGRAWAT2
                txtDIAGNOSAKLINIS.Text = ds.DIAGNOSAKLINIS
                grdDOKTER.EditValue = ds.KDDOCTOR
                txtDIIT.Text = ds.DIIT
                txtBB.Text = ds.BB
                txtTB.Text = ds.TB
                txtIMT.Text = ds.IMT
                txtSTATUSGIZI.Text = ds.STATUS_GIZI
                txtLLA.Text = ds.LLA
                txtINDIKATOR1_SKOR1.Text = ds.INDIKATOR1_SKOR1
                txtINDIKATOR1_SKOR2.Text = ds.INDIKATOR1_SKOR2
                txtINDIKATOR1_SKOR3.Text = ds.INDIKATOR1_SKOR3
                txtINDIKATOR1_SKOR4.Text = ds.INDIKATOR1_SKOR4
                txtINDIKATOR1_SKOR5.Text = ds.INDIKATOR1_SKOR5
                txtINDIKATOR2_SKOR1.Text = ds.INDIKATOR2_SKOR1
                txtINDIKATOR2_SKOR2.Text = ds.INDIKATOR2_SKOR2
                txtINDIKATOR2_SKOR3.Text = ds.INDIKATOR2_SKOR3
                txtINDIKATOR2_SKOR4.Text = ds.INDIKATOR2_SKOR4
                'txtMUAL.Text = ds.INDIKATOR3_MUAL
                'txtMUNTAH.Text = ds.INDIKATOR3_MUNTAH
                'txtDIARE.Text = ds.INDIKATOR3_DIARE
                'txtANOREKSIA.Text = ds.INDIKATOR3_ANOREKSIA


                CheckEdit27.Checked = IIf(ds.INDIKATOR3_MUAL = "Ya", True, False)
                CheckEdit28.Checked = IIf(ds.INDIKATOR3_MUNTAH = "Ya", True, False)
                CheckEdit29.Checked = IIf(ds.INDIKATOR3_DIARE = "Ya", True, False)
                CheckEdit30.Checked = IIf(ds.INDIKATOR3_ANOREKSIA = "Ya", True, False)

                txtINDIKATOR3_SKOR1.Text = ds.INDIKATOR3_SKOR1
                txtINDIKATOR3_SKOR2.Text = ds.INDIKATOR3_SKOR2
                txtINDIKATOR3_SKOR3.Text = ds.INDIKATOR3_SKOR3
                txtINDIKATOR4_SKOR1.Text = ds.INDIKATOR4_SKOR1
                txtINDIKATOR4_SKOR2.Text = ds.INDIKATOR4_SKOR2
                txtINDIKATOR4_SKOR3.Text = ds.INDIKATOR4_SKOR3
                txtINDIKATOR5_SKOR1.Text = ds.INDIKATOR5_SKOR1
                txtINDIKATOR5_SKOR2.Text = ds.INDIKATOR5_SKOR2
                txtINDIKATOR5_SKOR3.Text = ds.INDIKATOR5_SKOR3
                txtINDIKATORPF1_SKOR1.Text = ds.INDIKATORPF1_SKOR1
                txtINDIKATORPF1_SKOR2.Text = ds.INDIKATORPF1_SKOR2
                txtINDIKATORPF1_SKOR3.Text = ds.INDIKATORPF1_SKOR3
                txtINDIKATORPF2_SKOR1.Text = ds.INDIKATORPF2_SKOR1
                txtINDIKATORPF2_SKOR2.Text = ds.INDIKATORPF2_SKOR2
                txtINDIKATORPF2_SKOR3.Text = ds.INDIKATORPF2_SKOR3
                txtINDIKATORPF3_SKOR1.Text = ds.INDIKATORPF3_SKOR1
                txtINDIKATORPF3_SKOR2.Text = ds.INDIKATORPF3_SKOR2
                txtINDIKATORPF3_SKOR3.Text = ds.INDIKATORPF3_SKOR3
                txtINDIKATORPF4_SKOR1.Text = ds.INDIKATORPF4_SKOR1
                txtINDIKATORPF4_SKOR2.Text = ds.INDIKATORPF4_SKOR2
                txtINDIKATORPF4_SKOR3.Text = ds.INDIKATORPF4_SKOR3
                'chkPENILAIAN_1.Checked = ds.PENILAIAN_1
                CheckEdit23.Checked = ds.PENILAIAN_2

                'CheckEdit24.Checked = ds.PENILAIAN_3
                'CheckEdit25.Checked = ds.PENILAIAN_4
                'CheckEdit26.Checked = ds.PENILAIAN_5


                'txtMATERI.Text = ds.MATERI
                'txtLEAFLET.Text = ds.LEAFLET
                'txtDATA_PENTING.Text = ds.DATA_PENTING
                TEXTEDIT_1.Text = ds.TEXTEDIT_1
                TEXTEDIT_2.Text = ds.TEXTEDIT_2
                TEXTEDIT_3.Text = ds.TEXTEDIT_3
                TEXTEDIT_4.Text = ds.TEXTEDIT_4
                'TEXTEDIT_5.Text = ds.TEXTEDIT_5
                'TEXTEDIT_6.Text = ds.TEXTEDIT_6
                'TEXTEDIT_7.Text = ds.TEXTEDIT_7

                'CheckEdit31.Checked = IIf(ds.TEXTEDIT_5 = "Ya", True, False)
                'CheckEdit32.Checked = IIf(ds.TEXTEDIT_6 = "Ya", True, False)
                'CheckEdit33.Checked = IIf(ds.TEXTEDIT_7 = "Ya", True, False)

                chkKeseluruhanSkor_A.Checked = ds.CEKLIS_01
                chkKeseluruhanSkor_B.Checked = ds.CEKLIS_02
                chkKeseluruhanSkor_C.Checked = ds.CEKLIS_03
                chkKeseluruhanSkor_A_2.Checked = ds.CEKLIS_04
                chkKeseluruhanSkor_B_2.Checked = ds.CEKLIS_05
                chkKeseluruhanSkor_C_2.Checked = ds.CEKLIS_06


                CheckEdit1.Checked = ds.CheckEdit1
                CheckEdit2.Checked = ds.CheckEdit2
                CheckEdit3.Checked = ds.CheckEdit3
                CheckEdit4.Checked = ds.CheckEdit4
                CheckEdit5.Checked = ds.CheckEdit5
                CheckEdit6.Checked = ds.CheckEdit6
                CheckEdit7.Checked = ds.CheckEdit7
                CheckEdit8.Checked = ds.CheckEdit8
                CheckEdit9.Checked = ds.CheckEdit9
                CheckEdit10.Checked = ds.CheckEdit10
                CheckEdit11.Checked = ds.CheckEdit11
                CheckEdit12.Checked = ds.CheckEdit12
                CheckEdit13.Checked = ds.CheckEdit13
                CheckEdit14.Checked = ds.CheckEdit14
                CheckEdit15.Checked = ds.CheckEdit15
                CheckEdit16.Checked = ds.CheckEdit16
                CheckEdit17.Checked = ds.CheckEdit17
                CheckEdit18.Checked = ds.CheckEdit18
                CheckEdit19.Checked = ds.CheckEdit19
                CheckEdit20.Checked = ds.CheckEdit20
                CheckEdit21.Checked = ds.PENILAIAN_1
                CheckEdit22.Checked = ds.PENILAIAN_2

                chkMonitoringUmum.Checked = ds.PENILAIAN_4
                chkKonseling.Checked = ds.PENILAIAN_5

                chkCeklis_07.Checked = ds.CEKLIS_07
                chkCeklis_08.Checked = ds.CEKLIS_08
                chkCeklis_09.Checked = ds.CEKLIS_09
            End With
        Catch oErr As Exception
            MsgBox("Load List Data : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Function fn_Validate() As Boolean
        Try
            fn_Validate = True
            If txtKDKREG.Text = String.Empty Then
                MsgBox("Dibutuhkan KDKUNJUNGAN", MsgBoxStyle.Exclamation, Me.Text)
                txtKDKREG.Focus()
                fn_Validate = False
                Exit Function
            End If

        Catch oErr As Exception
            MsgBox("Validate Data : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Function
    Private Function fn_Save() As Boolean
        Try
            Dim ds = oS_DIGITAL_RI_20.GetStructureHeader

            With ds
                .KDREG = txtKDKREG.Text
                .KDCUSTOMER = txtNOPASIEN.Text
                Try
                    .DATECREATED = oS_DIGITAL_RI_20.GetData(txtKDKREG.Text).DATECREATED
                Catch ex As Exception
                    .DATECREATED = Now
                End Try
                .DATEUPDATED = Now
                .DATE = deDATE.DateTime
                .RUANGRAWAT2 = txtRUANGRAWAT2.Text.Trim
                .DIAGNOSAKLINIS = txtDIAGNOSAKLINIS.Text.Trim
                .DIIT = txtDIIT.Text.Trim.ToUpper
                .KDDOCTOR = grdDOKTER.EditValue
                .NMDOCTOR = grdDOKTER.Text
                .BB = txtBB.Text.Trim.ToUpper
                .TB = txtTB.Text.Trim.ToUpper
                .IMT = txtIMT.Text.Trim.ToUpper
                .LLA = txtLLA.Text.Trim.ToUpper
                .STATUS_GIZI = txtSTATUSGIZI.Text.Trim.ToUpper

                .INDIKATOR1_SKOR1 = txtINDIKATOR1_SKOR1.Text.Trim.ToUpper
                .INDIKATOR1_SKOR2 = txtINDIKATOR1_SKOR2.Text.Trim.ToUpper
                .INDIKATOR1_SKOR3 = txtINDIKATOR1_SKOR3.Text.Trim.ToUpper
                .INDIKATOR1_SKOR4 = txtINDIKATOR1_SKOR4.Text.Trim.ToUpper
                .INDIKATOR1_SKOR5 = txtINDIKATOR1_SKOR5.Text.Trim.ToUpper

                .INDIKATOR2_SKOR1 = txtINDIKATOR2_SKOR1.Text.Trim.ToUpper
                .INDIKATOR2_SKOR2 = txtINDIKATOR2_SKOR2.Text.Trim.ToUpper
                .INDIKATOR2_SKOR3 = txtINDIKATOR2_SKOR3.Text.Trim.ToUpper
                .INDIKATOR2_SKOR4 = txtINDIKATOR2_SKOR4.Text.Trim.ToUpper

                '.INDIKATOR3_MUAL = txtMUAL.Text.Trim.ToUpper
                '.INDIKATOR3_MUNTAH = txtMUNTAH.Text.Trim.ToUpper
                '.INDIKATOR3_DIARE = txtDIARE.Text.Trim.ToUpper
                '.INDIKATOR3_ANOREKSIA = txtANOREKSIA.Text.Trim.ToUpper

                .INDIKATOR3_MUAL = IIf(CheckEdit27.Checked = True, "Ya", "")
                .INDIKATOR3_MUNTAH = IIf(CheckEdit28.Checked = True, "Ya", "")
                .INDIKATOR3_DIARE = IIf(CheckEdit29.Checked = True, "Ya", "")
                .INDIKATOR3_ANOREKSIA = IIf(CheckEdit30.Checked = True, "Ya", "")

                .INDIKATOR3_SKOR1 = txtINDIKATOR3_SKOR1.Text.Trim.ToUpper
                .INDIKATOR3_SKOR2 = txtINDIKATOR3_SKOR2.Text.Trim.ToUpper
                .INDIKATOR3_SKOR3 = txtINDIKATOR3_SKOR3.Text.Trim.ToUpper

                .INDIKATOR4_SKOR1 = txtINDIKATOR4_SKOR1.Text.Trim.ToUpper
                .INDIKATOR4_SKOR2 = txtINDIKATOR4_SKOR2.Text.Trim.ToUpper
                .INDIKATOR4_SKOR3 = txtINDIKATOR4_SKOR3.Text.Trim.ToUpper

                .INDIKATOR5_SKOR1 = txtINDIKATOR5_SKOR1.Text.Trim.ToUpper
                .INDIKATOR5_SKOR2 = txtINDIKATOR5_SKOR2.Text.Trim.ToUpper
                .INDIKATOR5_SKOR3 = txtINDIKATOR5_SKOR3.Text.Trim.ToUpper

                .INDIKATORPF1_SKOR1 = txtINDIKATORPF1_SKOR1.Text.Trim.ToUpper
                .INDIKATORPF1_SKOR2 = txtINDIKATORPF1_SKOR2.Text.Trim.ToUpper
                .INDIKATORPF1_SKOR3 = txtINDIKATORPF1_SKOR3.Text.Trim.ToUpper

                .INDIKATORPF2_SKOR1 = txtINDIKATORPF2_SKOR1.Text.Trim.ToUpper
                .INDIKATORPF2_SKOR2 = txtINDIKATORPF2_SKOR2.Text.Trim.ToUpper
                .INDIKATORPF2_SKOR3 = txtINDIKATORPF2_SKOR3.Text.Trim.ToUpper

                .INDIKATORPF3_SKOR1 = txtINDIKATORPF3_SKOR1.Text.Trim.ToUpper
                .INDIKATORPF3_SKOR2 = txtINDIKATORPF3_SKOR2.Text.Trim.ToUpper
                .INDIKATORPF3_SKOR3 = txtINDIKATORPF3_SKOR3.Text.Trim.ToUpper

                .INDIKATORPF4_SKOR1 = txtINDIKATORPF4_SKOR1.Text.Trim.ToUpper
                .INDIKATORPF4_SKOR2 = txtINDIKATORPF4_SKOR2.Text.Trim.ToUpper
                .INDIKATORPF4_SKOR3 = txtINDIKATORPF4_SKOR3.Text.Trim.ToUpper

                '.PENILAIAN_1 = False
                .PENILAIAN_2 = CheckEdit23.Checked

                .MATERI = ""
                .LEAFLET = ""
                .DATA_PENTING = ""

                Try
                    .CETAK = oS_DIGITAL_RI_20.GetData(txtKDKREG.Text).CETAK
                Catch ex As Exception
                    .CETAK = 0
                End Try

                .KDUSER = sUserID
                .KDUSER_SIGNATURE = txtUSERPERAWAT.Text

                .TEXTEDIT_1 = TEXTEDIT_1.Text
                .TEXTEDIT_2 = TEXTEDIT_2.Text
                .TEXTEDIT_3 = TEXTEDIT_3.Text
                .TEXTEDIT_4 = TEXTEDIT_4.Text
                '.TEXTEDIT_5 = TEXTEDIT_5.Text
                '.TEXTEDIT_6 = TEXTEDIT_6.Text
                '.TEXTEDIT_7 = TEXTEDIT_7.Text

                '.PENILAIAN_3 = CheckEdit24.Checked
                '.PENILAIAN_4 = CheckEdit25.Checked
                '.PENILAIAN_5 = CheckEdit26.Checked

                .PENILAIAN_3 = False
                .PENILAIAN_4 = chkMonitoringUmum.Checked
                .PENILAIAN_5 = chkKonseling.Checked

                .TEXTEDIT_5 = ""
                .TEXTEDIT_6 = ""
                .TEXTEDIT_7 = ""

                .TEXTEDIT_8 = ""
                .TEXTEDIT_9 = ""
                .TEXTEDIT_10 = ""
                .CheckEdit1 = CheckEdit1.Checked
                .CheckEdit2 = CheckEdit2.Checked
                .CheckEdit3 = CheckEdit3.Checked
                .CheckEdit4 = CheckEdit4.Checked
                .CheckEdit5 = CheckEdit5.Checked
                .CheckEdit6 = CheckEdit6.Checked
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
                .PENILAIAN_1 = CheckEdit21.Checked
                .PENILAIAN_2 = CheckEdit22.Checked


                .CEKLIS_01 = chkKeseluruhanSkor_A.Checked
                .CEKLIS_02 = chkKeseluruhanSkor_B.Checked
                .CEKLIS_03 = chkKeseluruhanSkor_C.Checked
                .CEKLIS_04 = chkKeseluruhanSkor_A_2.Checked
                .CEKLIS_05 = chkKeseluruhanSkor_B_2.Checked
                .CEKLIS_06 = chkKeseluruhanSkor_C_2.Checked

                .CEKLIS_07 = chkCeklis_07.Checked
                .CEKLIS_08 = chkCeklis_08.Checked
                .CEKLIS_09 = chkCeklis_09.Checked

                .CEKLIS_10 = False
                .CEKLIS_11 = False
                .CEKLIS_12 = False
                .CEKLIS_13 = False
                .CEKLIS_14 = False
                .CEKLIS_15 = False
                .CEKLIS_16 = False
                .CEKLIS_17 = False
                .CEKLIS_18 = False
                .CEKLIS_19 = False
                .CEKLIS_20 = False
            End With

            If oFormMode = FORM_MODE.FORM_MODE_ADD Then
                Try
                    fn_Save = oS_DIGITAL_RI_20.InsertData(ds)
                Catch ex As Exception
                    MsgBox("Simpan Data : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            ElseIf oFormMode = FORM_MODE.FORM_MODE_EDIT Then
                Try
                    fn_Save = oS_DIGITAL_RI_20.UpdateData(ds)
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
        End Select
    End Sub
    Private Sub btnSaveClose_Click() Handles btnSaveClose.ItemClick
        If fn_Validate() = False Then Exit Sub
        If MsgBox("Save " & txtKDKREG.Text.Trim.ToUpper & "?", MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, Me.Text) = MsgBoxResult.No Then Exit Sub
        If fn_Save() = False Then
            MsgBox("Save gagal! Tolong kontak admin anda!", MsgBoxStyle.Exclamation, Me.Text)
        Else
            MsgBox("Save " & txtKDKREG.Text.Trim.ToUpper & " success!", MsgBoxStyle.Information, Me.Text)
            Me.Close()
        End If
    End Sub
    Private Sub btnClose_Click() Handles btnClose.ItemClick
        Me.Close()
    End Sub
#End Region
#Region "Lookup / Event"
    'Private Sub fn_LoadDoctor()

    '    Try
    '        Dim sKoneksiOld As String = String.Empty
    '        Dim oSetKoneksi As New Setting.clsSetKoneksi
    '        Dim dsSetKoneksi = oSetKoneksi.GetData()
    '        If dsSetKoneksi IsNot Nothing Then
    '            sKoneksiOld = dsSetKoneksi.GENERATE_ECLAIM
    '        End If

    '        Dim oConn As New SqlConnection
    '        Dim oComm As New SqlCommand
    '        Dim da As SqlDataAdapter
    '        Dim ds As New DataSet
    '        Dim SQL As String
    '        Dim sConn As String = sKoneksiOld
    '        oConn = New SqlConnection(sConn)

    '        If oConn.State = ConnectionState.Closed Then
    '            oConn.Open()
    '        End If

    '        SQL = "SELECT "
    '        SQL &= "* "
    '        SQL &= "FROM "
    '        SQL &= "M_STAFF A "
    '        SQL &= "WHERE "
    '        SQL &= "A.ISACTIVE = 1 "
    '        SQL &= "AND KELOMPOKIPK = 'NAKES' "
    '        SQL &= "ORDER BY NAME_DISPLAY ASC "

    '        oComm.Connection = oConn
    '        oComm.CommandText = SQL
    '        oComm.CommandTimeout = 120
    '        oComm.CommandType = CommandType.Text

    '        da = New SqlDataAdapter(oComm)
    '        da.Fill(ds, "STAFF")

    '        grdDOKTER.Properties.DataSource = ds.Tables("STAFF")
    '        grdDOKTER.Properties.ValueMember = "KDSTAFF"
    '        grdDOKTER.Properties.DisplayMember = "NAME_DISPLAY"

    '        If oConn.State = ConnectionState.Open Then
    '            oConn.Close()
    '        End If
    '    Catch oErr As Exception
    '        MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
    '    End Try
    'End Sub

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

            grdDOKTER.Properties.DataSource = ds.Tables("DOKTER")
            grdDOKTER.Properties.ValueMember = "KDDOCTOR"
            grdDOKTER.Properties.DisplayMember = "NAME_DISPLAY"

            If oConn.State = ConnectionState.Open Then
                oConn.Close()
            End If
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
#End Region
End Class