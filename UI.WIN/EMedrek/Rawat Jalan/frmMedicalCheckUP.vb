Imports DataAccess
Imports UI.WIN.MAIN.My.Resources
Imports System.Linq
Imports System.Data.SqlClient

Public Class frmMedicalCheckUP
#Region "Declaration"
    Private oFormMode As FORM_MODE = FORM_MODE.FORM_MODE_VIEW
    Private sNoId As String
    Private isLoad As Boolean = False
    Private oMedicalCheckUp As New Digital.clsDigital_MedicalChekUp
    Private sKDCUSTOMER As String
    Private sNAMAPASIEN As String
    Private sKDPENDAFTARAN As String
    Private sKDDOCTOR As String
    Private sTANGGALLAHIR As String
    Private sJENISKELAMIN As String
    Private sUMUR As String
#End Region
#Region "Function"
    Public Sub fn_LoadDataMaster(ByVal tanggaldaftar As String, ByVal KDCUSTOMER As String, ByVal NAMAPASIEN As String, ByVal KDPENDAFTARAN As String, ByVal KDDOCTOR As String, ByVal tanggalahir As String, ByVal jeniskelamin As String, ByVal umur As String)
        sKDCUSTOMER = KDCUSTOMER
        sNAMAPASIEN = NAMAPASIEN
        sKDPENDAFTARAN = KDPENDAFTARAN
        sKDDOCTOR = KDDOCTOR
        sTANGGALLAHIR = tanggalahir
        sJENISKELAMIN = jeniskelamin
        sUMUR = umur

        txtRM.Text = sKDCUSTOMER
        txtNAMA.Text = sNAMAPASIEN
        txtJENISKELAMIN.Text = sJENISKELAMIN
        txtTANGGALLAHIR.Text = sTANGGALLAHIR
        txtUMUR.Text = sUMUR
        txtTANGGALMASUK.Text = tanggaldaftar
    End Sub
    Public Sub LoadMe(ByVal FormMode As Integer, Optional ByVal NoId As String = "")
        oFormMode = FormMode
        sNoId = NoId
        sPicture = Nothing
    End Sub
    Private Sub Form_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        fn_ChangeFormState()
        fn_LoadLanguage()
        isLoad = True
    End Sub
    Public Sub fn_LoadLanguage()
        Try
            Me.Text = "MEDICAL CHECK UP"

            lCODE.Text = "Kode :"

            btnSaveNew.Caption = Caption.FormSaveNew
            btnSaveClose.Caption = Caption.FormSaveClose
            btnClose.Caption = Caption.FormClose
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub Form_Closing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        'sCode = txtMEMO.Text.Trim.ToUpper
    End Sub
    Private Sub fn_ChangeFormState()
        fn_LoadDokter()

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

        txtCODE.Properties.ReadOnly = True
        grdDPJP.Properties.ReadOnly = Status
        deDATE.Properties.ReadOnly = Status
        MemoEdit1.Properties.ReadOnly = Status
        MemoEdit2.Properties.ReadOnly = Status
        MemoEdit3.Properties.ReadOnly = Status
        MemoEdit4.Properties.ReadOnly = Status
        MemoEdit5.Properties.ReadOnly = Status
        MemoEdit6.Properties.ReadOnly = Status
        MemoEdit7.Properties.ReadOnly = Status
        MemoEdit8.Properties.ReadOnly = Status
        MemoEdit9.Properties.ReadOnly = Status
        MemoEdit10.Properties.ReadOnly = Status
        MemoEdit11.Properties.ReadOnly = Status
        MemoEdit12.Properties.ReadOnly = Status
        MemoEdit13.Properties.ReadOnly = Status
        MemoEdit14.Properties.ReadOnly = Status
        'MemoEdit15.Properties.ReadOnly = Status
        'MemoEdit16.Properties.ReadOnly = Status
        'MemoEdit17.Properties.ReadOnly = Status
        'MemoEdit18.Properties.ReadOnly = Status
        'MemoEdit19.Properties.ReadOnly = Status
        'MemoEdit20.Properties.ReadOnly = Status
        'MemoEdit21.Properties.ReadOnly = Status
        'MemoEdit22.Properties.ReadOnly = Status
        'MemoEdit23.Properties.ReadOnly = Status
        'MemoEdit24.Properties.ReadOnly = Status
        'MemoEdit25.Properties.ReadOnly = Status
        MemoEdit26.Properties.ReadOnly = Status
        MemoEdit27.Properties.ReadOnly = Status
        MemoEdit28.Properties.ReadOnly = Status
        MemoEdit29.Properties.ReadOnly = Status
        'MemoEdit30.Properties.ReadOnly = Status
        'MemoEdit31.Properties.ReadOnly = Status
        'MemoEdit32.Properties.ReadOnly = Status
        'MemoEdit33.Properties.ReadOnly = Status
        'MemoEdit34.Properties.ReadOnly = Status
        'MemoEdit35.Properties.ReadOnly = Status
        'MemoEdit36.Properties.ReadOnly = Status
        TextEdit1.Properties.ReadOnly = Status
        TextEdit2.Properties.ReadOnly = Status
        TextEdit3.Properties.ReadOnly = Status
        TextEdit4.Properties.ReadOnly = Status

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
        CheckEdit21.Properties.ReadOnly = Status
        CheckEdit22.Properties.ReadOnly = Status
        CheckEdit23.Properties.ReadOnly = Status
        CheckEdit24.Properties.ReadOnly = Status
        CheckEdit25.Properties.ReadOnly = Status
        CheckEdit26.Properties.ReadOnly = Status
        CheckEdit27.Properties.ReadOnly = Status
        CheckEdit28.Properties.ReadOnly = Status
        CheckEdit29.Properties.ReadOnly = Status
        CheckEdit30.Properties.ReadOnly = Status
        CheckEdit31.Properties.ReadOnly = Status
        CheckEdit32.Properties.ReadOnly = Status
        CheckEdit33.Properties.ReadOnly = Status
        CheckEdit34.Properties.ReadOnly = Status
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
        CheckEdit50.Properties.ReadOnly = Status
        CheckEdit51.Properties.ReadOnly = Status
        CheckEdit52.Properties.ReadOnly = Status
        CheckEdit53.Properties.ReadOnly = Status
        CheckEdit54.Properties.ReadOnly = Status
        CheckEdit55.Properties.ReadOnly = Status
        CheckEdit56.Properties.ReadOnly = Status
        CheckEdit57.Properties.ReadOnly = Status
        CheckEdit58.Properties.ReadOnly = Status
        CheckEdit59.Properties.ReadOnly = Status
        CheckEdit60.Properties.ReadOnly = Status
        CheckEdit61.Properties.ReadOnly = Status
        CheckEdit62.Properties.ReadOnly = Status
        CheckEdit63.Properties.ReadOnly = Status
        CheckEdit64.Properties.ReadOnly = Status
        CheckEdit65.Properties.ReadOnly = Status
        CheckEdit66.Properties.ReadOnly = Status
        CheckEdit67.Properties.ReadOnly = Status
        CheckEdit68.Properties.ReadOnly = Status
        CheckEdit69.Properties.ReadOnly = Status
        CheckEdit70.Properties.ReadOnly = Status
        CheckEdit71.Properties.ReadOnly = Status
        CheckEdit72.Properties.ReadOnly = Status
        CheckEdit73.Properties.ReadOnly = Status
        CheckEdit74.Properties.ReadOnly = Status
        CheckEdit75.Properties.ReadOnly = Status
        CheckEdit76.Properties.ReadOnly = Status
        CheckEdit77.Properties.ReadOnly = Status
        CheckEdit78.Properties.ReadOnly = Status
        CheckEdit79.Properties.ReadOnly = Status
        CheckEdit80.Properties.ReadOnly = Status
        CheckEdit81.Properties.ReadOnly = Status
        CheckEdit82.Properties.ReadOnly = Status
        CheckEdit83.Properties.ReadOnly = Status
        CheckEdit84.Properties.ReadOnly = Status
        CheckEdit85.Properties.ReadOnly = Status
        CheckEdit86.Properties.ReadOnly = Status
        CheckEdit87.Properties.ReadOnly = Status
        CheckEdit88.Properties.ReadOnly = Status
        CheckEdit89.Properties.ReadOnly = Status
        CheckEdit90.Properties.ReadOnly = Status
        CheckEdit91.Properties.ReadOnly = Status
        CheckEdit92.Properties.ReadOnly = Status
        CheckEdit93.Properties.ReadOnly = Status
        CheckEdit94.Properties.ReadOnly = Status
        CheckEdit95.Properties.ReadOnly = Status
        CheckEdit96.Properties.ReadOnly = Status
        CheckEdit97.Properties.ReadOnly = Status
    End Sub
    Private Sub fn_EmptyMe()
        txtCODE.Text = "<---AUTO--->"
        grdDPJP.Text = sKDDOCTOR
        deDATE.DateTime = Now
        MemoEdit1.ResetText()
        MemoEdit2.ResetText()
        MemoEdit3.ResetText()
        MemoEdit4.ResetText()
        MemoEdit5.ResetText()
        MemoEdit6.ResetText()
        MemoEdit7.ResetText()
        MemoEdit8.ResetText()
        MemoEdit9.ResetText()
        MemoEdit10.ResetText()
        MemoEdit11.ResetText()
        MemoEdit12.ResetText()
        MemoEdit13.ResetText()
        MemoEdit14.ResetText()
        'MemoEdit15.ResetText()
        'MemoEdit16.ResetText()
        'MemoEdit17.ResetText()
        'MemoEdit18.ResetText()
        'MemoEdit19.ResetText()
        'MemoEdit20.ResetText()
        'MemoEdit21.ResetText()
        'MemoEdit22.ResetText()
        'MemoEdit23.ResetText()
        'MemoEdit24.ResetText()
        'MemoEdit25.ResetText()
        MemoEdit26.ResetText()
        MemoEdit27.ResetText()
        MemoEdit28.ResetText()
        MemoEdit29.ResetText()
        'MemoEdit30.ResetText()
        'MemoEdit31.ResetText()
        'MemoEdit32.ResetText()
        'MemoEdit33.ResetText()
        'MemoEdit34.ResetText()
        'MemoEdit35.ResetText()
        'MemoEdit36.ResetText()
        TextEdit1.ResetText()
        TextEdit2.ResetText()
        TextEdit3.ResetText()
        TextEdit4.ResetText()

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
        CheckEdit23.Checked = False
        CheckEdit24.Checked = False
        CheckEdit25.Checked = False
        CheckEdit26.Checked = False
        CheckEdit27.Checked = False
        CheckEdit28.Checked = False
        CheckEdit29.Checked = False
        CheckEdit30.Checked = False
        CheckEdit31.Checked = False
        CheckEdit32.Checked = False
        CheckEdit33.Checked = False
        CheckEdit34.Checked = False
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
        CheckEdit50.Checked = False
        CheckEdit51.Checked = False
        CheckEdit52.Checked = False
        CheckEdit53.Checked = False
        CheckEdit54.Checked = False
        CheckEdit55.Checked = False
        CheckEdit56.Checked = False
        CheckEdit57.Checked = False
        CheckEdit58.Checked = False
        CheckEdit59.Checked = False
        CheckEdit60.Checked = False
        CheckEdit61.Checked = False
        CheckEdit62.Checked = False
        CheckEdit63.Checked = False
        CheckEdit64.Checked = False
        CheckEdit65.Checked = False
        CheckEdit66.Checked = False
        CheckEdit67.Checked = False
        CheckEdit68.Checked = False
        CheckEdit69.Checked = False
        CheckEdit70.Checked = False
        CheckEdit71.Checked = False
        CheckEdit72.Checked = False
        CheckEdit73.Checked = False
        CheckEdit74.Checked = False
        CheckEdit75.Checked = False
        CheckEdit76.Checked = False
        CheckEdit77.Checked = False
        CheckEdit78.Checked = False
        CheckEdit79.Checked = False
        CheckEdit80.Checked = False
        CheckEdit81.Checked = False
        CheckEdit82.Checked = False
        CheckEdit83.Checked = False
        CheckEdit84.Checked = False
        CheckEdit85.Checked = False
        CheckEdit86.Checked = False
        CheckEdit87.Checked = False
        CheckEdit88.Checked = False
        CheckEdit89.Checked = False
        CheckEdit90.Checked = False
        CheckEdit91.Checked = False
        CheckEdit92.Checked = False
        CheckEdit93.Checked = False
        CheckEdit94.Checked = False
        CheckEdit95.Checked = False
        CheckEdit96.Checked = False
        CheckEdit97.Checked = False
    End Sub
    Private Sub fn_LoadData()
        Try
            Dim ds = oMedicalCheckUp.GetData(sNoId)

            With ds
                txtCODE.Text = sNoId
                grdDPJP.Text = .KDDOCTOR
                deDATE.DateTime = .DATE
                MemoEdit1.Text = .MemoEdit1
                MemoEdit2.Text = .MemoEdit2
                MemoEdit3.Text = .MemoEdit3
                MemoEdit4.Text = .MemoEdit4
                MemoEdit5.Text = .MemoEdit5
                MemoEdit6.Text = .MemoEdit6
                MemoEdit7.Text = .MemoEdit7
                MemoEdit8.Text = .MemoEdit8
                MemoEdit9.Text = .MemoEdit9
                MemoEdit10.Text = .MemoEdit10
                MemoEdit11.Text = .MemoEdit11
                MemoEdit12.Text = .MemoEdit12
                MemoEdit13.Text = .MemoEdit13
                MemoEdit14.Text = .MemoEdit14
                'MemoEdit15.Text = .MemoEdit15
                'MemoEdit16.Text = .MemoEdit16
                'MemoEdit17.Text = .MemoEdit17
                'MemoEdit18.Text = .MemoEdit18
                'MemoEdit19.Text = .MemoEdit19
                'MemoEdit20.Text = .MemoEdit20
                'MemoEdit21.Text = .MemoEdit21
                'MemoEdit22.Text = .MemoEdit22
                'MemoEdit23.Text = .MemoEdit23
                'MemoEdit24.Text = .MemoEdit24
                'MemoEdit25.Text = .MemoEdit25
                MemoEdit26.Text = .MemoEdit26
                MemoEdit27.Text = .MemoEdit27
                MemoEdit28.Text = .MemoEdit28
                MemoEdit29.Text = .MemoEdit29
                'MemoEdit30.Text = .MemoEdit30
                'MemoEdit31.Text = .MemoEdit31
                'MemoEdit32.Text = .MemoEdit32
                'MemoEdit33.Text = .MemoEdit33
                'MemoEdit34.Text = .MemoEdit34
                'MemoEdit35.Text = .MemoEdit35
                'MemoEdit36.Text = .MemoEdit36
                TextEdit1.Text = .TEXTEDIT1
                TextEdit2.Text = .TEXTEDIT2
                TextEdit3.Text = .TEXTEDIT3
                TextEdit4.Text = .TEXTEDIT4

                CheckEdit1.Checked = .CheckEdit1
                CheckEdit2.Checked = .CheckEdit2
                CheckEdit3.Checked = .CheckEdit3
                CheckEdit4.Checked = .CheckEdit4
                CheckEdit5.Checked = .CheckEdit5
                CheckEdit6.Checked = .CheckEdit6
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
                CheckEdit21.Checked = .CheckEdit21
                CheckEdit22.Checked = .CheckEdit22
                CheckEdit23.Checked = .CheckEdit23
                CheckEdit24.Checked = .CheckEdit24
                CheckEdit25.Checked = .CheckEdit25
                CheckEdit26.Checked = .CheckEdit26
                CheckEdit27.Checked = .CheckEdit27
                CheckEdit28.Checked = .CheckEdit28
                CheckEdit29.Checked = .CheckEdit29
                CheckEdit30.Checked = .CheckEdit30
                CheckEdit31.Checked = .CheckEdit31
                CheckEdit32.Checked = .CheckEdit32
                CheckEdit33.Checked = .CheckEdit33
                CheckEdit34.Checked = .CheckEdit34
                CheckEdit35.Checked = .CheckEdit35
                CheckEdit36.Checked = .CheckEdit36
                CheckEdit37.Checked = .CheckEdit37
                CheckEdit38.Checked = .CheckEdit38
                CheckEdit39.Checked = .CheckEdit39
                CheckEdit40.Checked = .CheckEdit40
                CheckEdit41.Checked = .CheckEdit41
                CheckEdit42.Checked = .CheckEdit42
                CheckEdit43.Checked = .CheckEdit43
                CheckEdit44.Checked = .CheckEdit44
                CheckEdit45.Checked = .CheckEdit45
                CheckEdit46.Checked = .CheckEdit46
                CheckEdit47.Checked = .CheckEdit47
                CheckEdit48.Checked = .CheckEdit48
                CheckEdit49.Checked = .CheckEdit49
                CheckEdit50.Checked = .CheckEdit50
                CheckEdit51.Checked = .CheckEdit51
                CheckEdit52.Checked = .CheckEdit52
                CheckEdit53.Checked = .CheckEdit53
                CheckEdit54.Checked = .CheckEdit54
                CheckEdit55.Checked = .CheckEdit55
                CheckEdit56.Checked = .CheckEdit56
                CheckEdit57.Checked = .CheckEdit57
                CheckEdit58.Checked = .CheckEdit58
                CheckEdit59.Checked = .CheckEdit59
                CheckEdit60.Checked = .CheckEdit60
                CheckEdit61.Checked = .CheckEdit61
                CheckEdit62.Checked = .CheckEdit62
                CheckEdit63.Checked = .CheckEdit63
                CheckEdit64.Checked = .CheckEdit64
                CheckEdit65.Checked = .CheckEdit65
                CheckEdit66.Checked = .CheckEdit66
                CheckEdit67.Checked = .CheckEdit67
                CheckEdit68.Checked = .CheckEdit68
                CheckEdit69.Checked = .CheckEdit69
                CheckEdit70.Checked = .CheckEdit70
                CheckEdit71.Checked = .CheckEdit71
                CheckEdit72.Checked = .CheckEdit72
                CheckEdit73.Checked = .CheckEdit73
                CheckEdit74.Checked = .CheckEdit74
                CheckEdit75.Checked = .CheckEdit75
                CheckEdit76.Checked = .CheckEdit76
                CheckEdit77.Checked = .CheckEdit77
                CheckEdit78.Checked = .CheckEdit78
                CheckEdit79.Checked = .CheckEdit79
                CheckEdit80.Checked = .CheckEdit80
                CheckEdit81.Checked = .CheckEdit81
                CheckEdit82.Checked = .CheckEdit82
                CheckEdit83.Checked = .CheckEdit83
                CheckEdit84.Checked = .CheckEdit84
                CheckEdit85.Checked = .CheckEdit85
                CheckEdit86.Checked = .CheckEdit86
                CheckEdit87.Checked = .CheckEdit87
                CheckEdit88.Checked = .CheckEdit88
                CheckEdit89.Checked = .CheckEdit89
                CheckEdit90.Checked = .CheckEdit90
                CheckEdit91.Checked = .CheckEdit91
                CheckEdit92.Checked = .CheckEdit92
                CheckEdit93.Checked = .CheckEdit93
                CheckEdit94.Checked = .CheckEdit94
                CheckEdit95.Checked = .CheckEdit95
                CheckEdit96.Checked = .CheckEdit96
                CheckEdit97.Checked = .CheckEdit97
                Try
                    If .MemoEdit37 <> "" Then
                        'picGAMBAR2.Image = Image.FromFile(txtALAMATGAMBAR.Text)

                        Dim img As System.Drawing.Image = System.Drawing.Image.FromFile(.MemoEdit37)
                        picGAMBAR2.Image = img
                        sPicture = picGAMBAR2.Image
                    Else
                        sPicture = Nothing
                    End If
                Catch ex As Exception
                    sPicture = Nothing
                End Try

            End With

            Dim oReqAwalPemeriksaan As New Transaksi.clsReqAwalPemeriksaan
            Dim dsReqAwalPemeriksaan = oReqAwalPemeriksaan.GetData(sKDPENDAFTARAN)
            If dsReqAwalPemeriksaan IsNot Nothing Then
                'chkALERGI_YA.Checked = dsReqAwalPemeriksaan.ALERGI_YA
                'chkALERGI_TIDAK.Checked = dsReqAwalPemeriksaan.ALERGI_TIDAK
                'txtALERGIOBAT.Text = dsReqAwalPemeriksaan.ALERGI_TEXT
                'txtSUBJEKTIF.Text = dsReqAwalPemeriksaan.SUBJEKTIF
                'txtOBJEKTIF_UMUR.Text = dsReqAwalPemeriksaan.OBJEKTIF_UMUR
                MemoEdit5.Text = IIf(CDec(dsReqAwalPemeriksaan.OBJEKTIF_BERATBADAN) = 0, "", dsReqAwalPemeriksaan.OBJEKTIF_BERATBADAN)
                MemoEdit7.Text = IIf(CDec(dsReqAwalPemeriksaan.OBJEKTIF_TINGGIBADAN) = 0, "", dsReqAwalPemeriksaan.OBJEKTIF_TINGGIBADAN)
                MemoEdit8.Text = dsReqAwalPemeriksaan.OBJEKTIF_TEKANANDARAH
                MemoEdit10.Text = dsReqAwalPemeriksaan.OBJEKTIF_NADI
                MemoEdit12.Text = dsReqAwalPemeriksaan.OBJEKTIF_RESPIRASI
                'txtOBJEKTIF_SATURASIOKSIGEN.Text = dsReqAwalPemeriksaan.OBJEKTIF_SATURASIOKSIGEN
                MemoEdit11.Text = dsReqAwalPemeriksaan.OBJEKTIF_SUHU
                'txtDESKRIPSI.Text = dsReqAwalPemeriksaan.DESKRIPSI

                'For Each xloop In oReqAwalPemeriksaan.GetDataDetail(txtKDPENDAFTARAN.Text)
                '    grvTindakanPoli.Focus()
                '    grvTindakanPoli.AddNewRow()
                '    grvTindakanPoli.SetFocusedRowCellValue(colKETERANGAN_, xloop.KETERANGAN)
                '    grvTindakanPoli.UpdateCurrentRow()
                'Next
            End If
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Function fn_Validate() As Boolean
        Try
            fn_Validate = True
            If grdDPJP.Text = String.Empty Then
                grdDPJP.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                grdDPJP.ErrorText = Statement.ErrorRequired

                grdDPJP.Focus()
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
            Dim ds = oMedicalCheckUp.GetStructureHeader
            With ds
                Try
                    .DATECREATED = oMedicalCheckUp.GetData(sNoId).DATECREATED
                Catch oErr As Exception
                    .DATECREATED = Now
                End Try
                .DATEUPDATED = Now
                .KDMEDICALCHECKUP = sNoId
                .DATE = deDATE.DateTime
                .KDCUSTOMER = sKDCUSTOMER
                .NAMAPASIEN = sNAMAPASIEN
                .KDPENDAFTARAN = sKDPENDAFTARAN
                .KDDOCTOR = grdDPJP.EditValue
                .NAMADOKTER = grdDPJP.Text
                .MemoEdit1 = MemoEdit1.Text
                .MemoEdit2 = MemoEdit2.Text
                .MemoEdit3 = MemoEdit3.Text
                .MemoEdit4 = MemoEdit4.Text
                .MemoEdit5 = MemoEdit5.Text
                .MemoEdit6 = MemoEdit6.Text
                .MemoEdit7 = MemoEdit7.Text
                .MemoEdit8 = MemoEdit8.Text
                .MemoEdit9 = MemoEdit9.Text
                .MemoEdit10 = MemoEdit10.Text
                .MemoEdit11 = MemoEdit11.Text
                .MemoEdit12 = MemoEdit12.Text
                .MemoEdit13 = MemoEdit13.Text
                .MemoEdit14 = MemoEdit14.Text
                .MemoEdit15 = ""
                .MemoEdit16 = ""
                .MemoEdit17 = ""
                .MemoEdit18 = ""
                .MemoEdit19 = ""
                .MemoEdit20 = ""
                .MemoEdit21 = ""
                .MemoEdit22 = ""
                .MemoEdit23 = ""
                .MemoEdit24 = ""
                .MemoEdit25 = ""
                .MemoEdit26 = MemoEdit26.Text
                .MemoEdit27 = MemoEdit27.Text
                .MemoEdit28 = MemoEdit28.Text
                .MemoEdit29 = MemoEdit29.Text
                .MemoEdit30 = ""
                .MemoEdit31 = ""
                .MemoEdit32 = ""
                .MemoEdit33 = ""
                .MemoEdit34 = ""
                .MemoEdit35 = ""
                .MemoEdit36 = ""
                If sPicture Is Nothing Then
                    Try
                        .MemoEdit37 = oMedicalCheckUp.GetData(sNoid).MemoEdit37
                    Catch ex As Exception
                        .MemoEdit37 = ""
                    End Try
                Else
                    Dim oReqAwalPemeriksaan As New Transaksi.clsReqAwalPemeriksaan
                    Dim sALAMATSIMPAN As String = String.Empty
                    Dim dsAlamat = oReqAwalPemeriksaan.GetDataAlamatSimpan()
                    If dsAlamat IsNot Nothing Then
                        sALAMATSIMPAN = dsAlamat.ALAMAT_SERVER
                    End If

                    Dim Alamat As String = sALAMATSIMPAN & Now.ToString("ddMMyyyyHHmm") & ".MCU" & sKDPENDAFTARAN & ".jpg"
                    picGAMBAR2.Image.Save(Alamat, System.Drawing.Imaging.ImageFormat.Jpeg)
                    .MemoEdit37 = Alamat
                End If

                .MemoEdit38 = sTANGGALLAHIR
                .MemoEdit39 = sJENISKELAMIN
                .MemoEdit40 = sUMUR
                .TEXTEDIT1 = TextEdit1.Text
                .TEXTEDIT2 = TextEdit2.Text
                .TEXTEDIT3 = TextEdit3.Text
                .TEXTEDIT4 = TextEdit4.Text

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
                .CheckEdit21 = CheckEdit21.Checked
                .CheckEdit22 = CheckEdit22.Checked
                .CheckEdit23 = CheckEdit23.Checked
                .CheckEdit24 = CheckEdit24.Checked
                .CheckEdit25 = CheckEdit25.Checked
                .CheckEdit26 = CheckEdit26.Checked
                .CheckEdit27 = CheckEdit27.Checked
                .CheckEdit28 = CheckEdit28.Checked
                .CheckEdit29 = CheckEdit29.Checked
                .CheckEdit30 = CheckEdit30.Checked
                .CheckEdit31 = CheckEdit31.Checked
                .CheckEdit32 = CheckEdit32.Checked
                .CheckEdit33 = CheckEdit33.Checked
                .CheckEdit34 = CheckEdit34.Checked
                .CheckEdit35 = CheckEdit35.Checked
                .CheckEdit36 = CheckEdit36.Checked
                .CheckEdit37 = CheckEdit37.Checked
                .CheckEdit38 = CheckEdit38.Checked
                .CheckEdit39 = CheckEdit39.Checked
                .CheckEdit40 = CheckEdit40.Checked
                .CheckEdit41 = CheckEdit41.Checked
                .CheckEdit42 = CheckEdit42.Checked
                .CheckEdit43 = CheckEdit43.Checked
                .CheckEdit44 = CheckEdit44.Checked
                .CheckEdit45 = CheckEdit45.Checked
                .CheckEdit46 = CheckEdit46.Checked
                .CheckEdit47 = CheckEdit47.Checked
                .CheckEdit48 = CheckEdit48.Checked
                .CheckEdit49 = CheckEdit49.Checked
                .CheckEdit50 = CheckEdit50.Checked
                .CheckEdit51 = CheckEdit51.Checked
                .CheckEdit52 = CheckEdit52.Checked
                .CheckEdit53 = CheckEdit53.Checked
                .CheckEdit54 = CheckEdit54.Checked
                .CheckEdit55 = CheckEdit55.Checked
                .CheckEdit56 = CheckEdit56.Checked
                .CheckEdit57 = CheckEdit57.Checked
                .CheckEdit58 = CheckEdit58.Checked
                .CheckEdit59 = CheckEdit59.Checked
                .CheckEdit60 = CheckEdit60.Checked
                .CheckEdit61 = CheckEdit61.Checked
                .CheckEdit62 = CheckEdit62.Checked
                .CheckEdit63 = CheckEdit63.Checked
                .CheckEdit64 = CheckEdit64.Checked
                .CheckEdit65 = CheckEdit65.Checked
                .CheckEdit66 = CheckEdit66.Checked
                .CheckEdit67 = CheckEdit67.Checked
                .CheckEdit68 = CheckEdit68.Checked
                .CheckEdit69 = CheckEdit69.Checked
                .CheckEdit70 = CheckEdit70.Checked
                .CheckEdit71 = CheckEdit71.Checked
                .CheckEdit72 = CheckEdit72.Checked
                .CheckEdit73 = CheckEdit73.Checked
                .CheckEdit74 = CheckEdit74.Checked
                .CheckEdit75 = CheckEdit75.Checked
                .CheckEdit76 = CheckEdit76.Checked
                .CheckEdit77 = CheckEdit77.Checked
                .CheckEdit78 = CheckEdit78.Checked
                .CheckEdit79 = CheckEdit79.Checked
                .CheckEdit80 = CheckEdit80.Checked
                .CheckEdit81 = CheckEdit81.Checked
                .CheckEdit82 = CheckEdit82.Checked
                .CheckEdit83 = CheckEdit83.Checked
                .CheckEdit84 = CheckEdit84.Checked
                .CheckEdit85 = CheckEdit85.Checked
                .CheckEdit86 = CheckEdit86.Checked
                .CheckEdit87 = CheckEdit87.Checked
                .CheckEdit88 = CheckEdit88.Checked
                .CheckEdit89 = CheckEdit89.Checked
                .CheckEdit90 = CheckEdit90.Checked
                .CheckEdit91 = CheckEdit91.Checked
                .CheckEdit92 = CheckEdit92.Checked
                .CheckEdit93 = CheckEdit93.Checked
                .CheckEdit94 = CheckEdit94.Checked
                .CheckEdit95 = CheckEdit95.Checked
                .CheckEdit96 = CheckEdit96.Checked
                .CheckEdit97 = CheckEdit97.Checked
                .CheckEdit98 = False
                .CheckEdit99 = False
                .CheckEdit100 = False
                .CheckEdit101 = False
                .CheckEdit102 = False
                .CheckEdit103 = False
                .CheckEdit104 = False
                .CheckEdit105 = False
                .CheckEdit106 = False
                .CheckEdit107 = False
                .CheckEdit108 = False
                .CheckEdit109 = False
                .CheckEdit110 = False
                .KDUSER = sUserID
                .MEMO = ""
            End With

            If oFormMode = FORM_MODE.FORM_MODE_ADD Then
                Try
                    fn_Save = oMedicalCheckUp.InsertData(ds)
                Catch oErr As Exception
                    MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            ElseIf oFormMode = FORM_MODE.FORM_MODE_EDIT Then
                Try
                    fn_Save = oMedicalCheckUp.UpdateData(ds)
                Catch oErr As Exception
                    MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            End If
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            fn_Save = False
        End Try
    End Function
#End Region
#Region "Command Button"
    Private Sub frmItem_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        Select Case e.KeyCode
            Case Keys.F12
                btnClose_Click()
            Case Keys.F2
                If btnSaveNew.Enabled = True Then
                    btnSaveNew_Click()
                End If
            Case Keys.F3
                If btnSaveClose.Enabled = True Then
                    btnSaveClose_Click()
                End If
        End Select
    End Sub
    Private Sub btnSaveNew_Click() Handles btnSaveNew.ItemClick
        If fn_Validate() = False Then Exit Sub
        If MsgBox(Statement.SaveQuestion, MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, Me.Text) = MsgBoxResult.No Then Exit Sub
        If fn_Save() = False Then
            MsgBox(Statement.SaveFail, MsgBoxStyle.Exclamation, Me.Text)
        Else
            MsgBox(Statement.SaveSuccess, MsgBoxStyle.Information, Me.Text)
            sStatusSave = "NEW"
            Me.Close()
        End If
    End Sub
    Private Sub btnSaveClose_Click() Handles btnSaveClose.ItemClick
        If fn_Validate() = False Then Exit Sub
        If MsgBox(Statement.SaveQuestion, MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, Me.Text) = MsgBoxResult.No Then Exit Sub
        If fn_Save() = False Then
            MsgBox(Statement.SaveFail, MsgBoxStyle.Exclamation, Me.Text)
        Else
            MsgBox(Statement.SaveSuccess, MsgBoxStyle.Information, Me.Text)
            Me.Close()
        End If
    End Sub
    Private Sub btnClose_Click() Handles btnClose.ItemClick
        Me.Close()
    End Sub
    Private Sub SimpleButton1_Click(sender As Object, e As EventArgs) Handles SimpleButton1.Click
        Dim frmPopUp_Image As New frmPopUp_img
        frmPopUp_Image.fn_LoadMe(picGAMBAR2.Image)
        frmPopUp_Image.ShowDialog()

        If sPicture IsNot Nothing Then
            picGAMBAR2.Image = sPicture
        End If

        picGAMBAR2.Focus()
    End Sub
#End Region
#Region "Lookup / Event"
    Private Sub fn_LoadDokter()
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
            SQL &= "KDDOCTOR, NAME_DISPLAY "
            SQL &= "FROM  "
            SQL &= "M_DOCTOR "
            SQL &= "WHERE "
            SQL &= "ISACTIVE = '1' "
            SQL &= "ORDER BY NAME_DISPLAY "

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
#End Region
End Class