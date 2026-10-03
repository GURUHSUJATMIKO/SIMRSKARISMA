Imports DataAccess
Imports UI.WIN.MAIN.My.Resources
Imports System.Linq
Imports System.Data.SqlClient

Public Class frmLembarDischargePlanning
#Region "Declaration"
    Private oFormMode As FORM_MODE = FORM_MODE.FORM_MODE_VIEW
    Private isLoad As Boolean = False
    Private oS_DIGITAL_LEMBARDISCHARGEPLANNING As New Digital.clsS_DIGITAL_LEMBARDISCHARGEPLANNING
    Private down As Boolean = False
    Private sKDUSER_PERAWAT As String = String.Empty

#End Region
#Region "Function"
    Public Sub LoadMe(ByVal FormMode As Integer, ByVal KDREG As String, ByVal KDDOCTOR As String, ByVal KDCUSTOMER As String, ByVal NAMAPASIEN As String, ByVal JENISKELAMIN As String, ByVal TUJUAN As String, ByVal KDUSER_PERAWAT As String)
        oFormMode = FormMode

        txtNoRegister.Text = KDREG
        txtNamaPasien.Text = NAMAPASIEN
        txtJenisKelamin.Text = JENISKELAMIN
        txtRuangRawat.Text = TUJUAN
        txtKDCUSTOMER.Text = KDCUSTOMER
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
       
        txtDiagnosaKeperawatan.Properties.ReadOnly = Status
        txtAturanDiet.Properties.ReadOnly = Status
        txtObatyangdiminum.Properties.ReadOnly = Status
        txtAktifitasdanIstirahat.Properties.ReadOnly = Status
        deTanggalKontrol.Properties.ReadOnly = Status
        txtTempatKontrol.Properties.ReadOnly = Status
        txtYangdibawapulang.Properties.ReadOnly = Status
        chkSembuh.Properties.ReadOnly = Status
        chkObatJalan.Properties.ReadOnly = Status
        chkPindah.Properties.ReadOnly = Status
        chkPulangPaksa.Properties.ReadOnly = Status
        chkLari.Properties.ReadOnly = Status
        chkMeninggal.Properties.ReadOnly = Status
        txtLainlain.Properties.ReadOnly = Status
        deDate.Properties.ReadOnly = Status
    End Sub
    Private Sub fn_EmptyMe()

        txtDiagnosaKeperawatan.ResetText()
        txtAturanDiet.ResetText()
        txtObatyangdiminum.ResetText()
        txtAktifitasdanIstirahat.ResetText()
        deTanggalKontrol.ResetText()
        txtTempatKontrol.ResetText()
        txtYangdibawapulang.ResetText()
        chkSembuh.Checked = False
        chkObatJalan.Checked = False
        chkPindah.Checked = False
        chkPulangPaksa.Checked = False
        chkLari.Checked = False
        chkMeninggal.Checked = False
        txtLainlain.ResetText()
        deDate.DateTime = Now
        deTanggalKRS.DateTime = Now
        deTanggalMRS.DateTime = Now
        deTanggalKontrol.DateTime = Now
    End Sub
    Private Sub fn_LoadData()
        Try
            ' ***** HEADER *****
            Dim ds = oS_DIGITAL_LEMBARDISCHARGEPLANNING.GetData(txtNoRegister.Text)

            With ds
                deDate.DateTime = .DATE
                deTanggalMRS.DateTime =.TANGGALMRS
                txtDiagnosaMRS.Text = .DIAGNOSAMRS
                deTanggalKRS.DateTime = .TANGGALKRS
                txtDiagnosaKRS.Text = .DIAGNOSAKRS
                txtDiagnosaKeperawatan.Text = .DIAGNOSAKEPERAWATAN
                txtAturanDiet.Text = .ATURANDIET
                txtObatyangdiminum.Text = .OBATYANGDIMINUM
                txtAktifitasdanIstirahat.Text = .AKTIFITAS
                deTanggalKontrol.DateTime = .TANGGALKONTROL
                txtTempatKontrol.Text = .TEMPATKONTROL
                txtYangdibawapulang.Text = .HASIL
                chkSembuh.Checked = .SEMBUH
                chkObatJalan.Checked = .OBATJALAN
                chkPindah.Checked = .PINDAH
                chkPulangPaksa.Checked = .PULANGPAKSA
                chkLari.Checked = .LARI
                chkMeninggal.Checked = .MENINGGAL
                txtLainlain.Text = .LAINLAIN

                'fn_LoadAsessmenRawatJalan(txtNoRegister.Text)

            End With
        Catch oErr As Exception
            MsgBox("Load List Data : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    'Private Sub fn_LoadAsessmenRawatJalan(ByVal Parameter As String)
    '    Dim oAsessmenRawatJalan As New Digital.clsDigital_RJ_08
    '    Dim dsAsessmenRawatJalan = oAsessmenRawatJalan.GetData(Parameter)

    '    Dim oAsessmenRawatJalan2 As New Digital.clsS_DIGITAL_ASKEP_RAWATJALAN
    '    Dim dsAsessmenRawatJalan2 = oAsessmenRawatJalan2.GetDatabyKDKUNJUNGAN(Parameter)

    '    If dsAsessmenRawatJalan IsNot Nothing Then
    '        'txtSOAP_O.Text = "Tensi : " & dsAsessmenRawatJalan.TANDA_VITAL_01 & " mmHg" & vbCrLf & vbCrLf & "Nadi : " & dsAsessmenRawatJalan.TANDA_VITAL_02 & " x/m" & vbCrLf & vbCrLf & "Respirasi : " & dsAsessmenRawatJalan.TANDA_VITAL_04 & " x/m" & vbCrLf & vbCrLf & "Suhu : " & dsAsessmenRawatJalan.TANDA_VITAL_03 & " °C" & vbCrLf & vbCrLf & "Berat Badan : " & dsAsessmenRawatJalan.TANDA_VITAL_05 & " Kg" & vbCrLf & vbCrLf & "Tinggi Badan : " & dsAsessmenRawatJalan.TANDA_VITAL_06 & " Cm"
            
    '        'keluhanutama
    '        txtRIWAYATPENYAKITTERDAHULU.Text = dsAsessmenRawatJalan2.KELUHAN_UTAMA   
    '        'keadaan umum
    '        txtSIMPULAN.Text = dsAsessmenRawatJalan.ALERGI_02_02_TEXT
    '        'kesadaran
    '        txtBB.Text = dsAsessmenRawatJalan.KESADARAN_UMUM
    '        'bb
    '        txtPB.Text = dsAsessmenRawatJalan.TANDA_VITAL_05 
    '        'tb
    '        txtIMT.Text = dsAsessmenRawatJalan.TANDA_VITAL_06  
    '        'gizi
    '        txtSTATUSGIZI.Text = dsAsessmenRawatJalan.TANDA_VITAL_06  
    '        'nadi
    '        txtPENURUNANBB.Text = dsAsessmenRawatJalan.TANDA_VITAL_02 
    '        'td
    '        txtDALAM.Text = dsAsessmenRawatJalan.TANDA_VITAL_01
    '        'suhu
    '        txtTL.Text = dsAsessmenRawatJalan.TANDA_VITAL_03  
    '        'respirasi
    '        txtLILA.Text = dsAsessmenRawatJalan.TANDA_VITAL_04  
    '    ElseIf dsAsessmenRawatJalan2 IsNot Nothing Then
    '        'keluhanutama
    '        txtRIWAYATPENYAKITTERDAHULU.Text = dsAsessmenRawatJalan2.KELUHAN_UTAMA   
    '        'keadaan umum
    '        txtSIMPULAN.Text = dsAsessmenRawatJalan2.KEADAAN
    '        'kesadaran
    '        txtBB.Text = dsAsessmenRawatJalan2.KESADARAN_UMUM 
    '        'bb
    '        txtPB.Text = dsAsessmenRawatJalan2.TANDA_VITAL_BB 
    '        'tb
    '        txtIMT.Text = dsAsessmenRawatJalan2.TANDA_VITAL_TB
    '        'gizi
    '        'txtSTATUSGIZI.Text = dsAsessmenRawatJalan2.TANDA_VITAL_06  
    '        'nadi
    '        txtPENURUNANBB.Text = dsAsessmenRawatJalan2.TANDA_VITAL_NADI 
    '        'td
    '        txtDALAM.Text = dsAsessmenRawatJalan2.TANDA_VITAL_TD
    '        'suhu
    '        txtTL.Text = dsAsessmenRawatJalan2.TANDA_VITAL_SUHU  
    '        'respirasi
    '        txtLILA.Text = dsAsessmenRawatJalan2.TANDA_VITAL_R  
    '    Else 
    '        Exit Sub
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

            Dim dsCek = oS_DIGITAL_LEMBARDISCHARGEPLANNING.GetData(txtNoRegister.Text)
            If dsCek IsNot Nothing Then
                MsgBox("Sudah di input", MsgBoxStyle.Exclamation, Me.Text)
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
            Dim ds = oS_DIGITAL_LEMBARDISCHARGEPLANNING.GetStructureHeader
            With ds
                .KDPENDAFTARAN = txtNoRegister.Text
                .KDCUSTOMER = txtKDCUSTOMER.Text
                Try
                    .DATECREATED = oS_DIGITAL_LEMBARDISCHARGEPLANNING.GetData(txtNoRegister.Text).DATECREATED
                Catch ex As Exception
                    .DATECREATED = Now
                End Try
                .DATEUPDATED = Now
                .DATE = deDATE.DateTime
                
                .TANGGALMRS = deTanggalMRS.DateTime
                .DIAGNOSAMRS = txtDiagnosaMRS.Text
                .TANGGALKRS = deTanggalKRS.DateTime
                .DIAGNOSAKRS = txtDiagnosaKRS.Text
                .DIAGNOSAKEPERAWATAN = txtDiagnosaKeperawatan.Text
                .ATURANDIET = txtAturanDiet.Text
                .OBATYANGDIMINUM = txtObatyangdiminum.Text
                .AKTIFITAS = txtAktifitasdanIstirahat.Text
                .TANGGALKONTROL = deTanggalKontrol.DateTime
                .TEMPATKONTROL = txtTempatKontrol.Text
                .HASIL = txtYangdibawapulang.Text
                .SEMBUH = chkSembuh.Checked
                .OBATJALAN = chkObatJalan.Checked
                .PINDAH = chkPindah.Checked
                .PULANGPAKSA = chkPulangPaksa.Checked
                .LARI = chkLari.Checked
                .MENINGGAL = chkMeninggal.Checked
                .LAINLAIN = txtLainlain.Text
                .PERAWAT_KODE = ""
                .PERAWAT_NAMEDISPLAY = sKDUSER_PERAWAT

                Try
                    .CETAK = oS_DIGITAL_LEMBARDISCHARGEPLANNING.GetData(txtNoRegister.Text).CETAK
                Catch ex As Exception
                    .CETAK = 0
                End Try

                .KDUSER = sKDUSER_PERAWAT
                .KDUSER_SIGNATURE = sUserID
            End With

            If oFormMode = FORM_MODE.FORM_MODE_ADD Then
                Try
                    fn_Save = oS_DIGITAL_LEMBARDISCHARGEPLANNING.InsertData(ds)
                Catch ex As Exception
                    MsgBox("Simpan Data : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            ElseIf oFormMode = FORM_MODE.FORM_MODE_EDIT Then
                Try
                    fn_Save = oS_DIGITAL_LEMBARDISCHARGEPLANNING.UpdateData(ds)
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
     Private Sub frmLembarDischargePlanning_MouseWheel(sender As Object, e As MouseEventArgs) Handles MyBase.MouseWheel
	    If e.Delta > 0 Then
		    'up
		    fn_ScrollPage(True)
	    Else
		    'down
		    fn_ScrollPage(False)
	    End If
    End Sub
    Private Sub fn_ScrollPage(ByVal isUp As Boolean)
        Dim myView As Point = Me.PanelControl1.AutoScrollPosition
        Dim scrollchange As Integer = 50

        If isUp Then
            'up
            myView.X = -myView.X
            myView.Y = -scrollchange - myView.Y

        Else
            'down
            myView.X = -myView.X
            myView.Y = scrollchange - myView.Y

        End If

        Me.Panel3.AutoScrollPosition = myView
    End Sub
#End Region
End Class