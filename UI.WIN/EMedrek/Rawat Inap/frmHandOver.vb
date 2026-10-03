Imports DataAccess
Imports System.Linq
Imports System.Data.SqlClient

Public Class frmHandOver
#Region "Declaration"
    Private oFormMode As FORM_MODE = FORM_MODE.FORM_MODE_VIEW
    Private sNoId As String
    Private isLoad As Boolean = False
    Private oS_DIGITAL_HANDOVER As New Transaksi.clsDigital_HandOver
    Private sKDDOCTOR As String = String.Empty
    Private sCopyKode As String = String.Empty
    Private sRegister As String = String.Empty
    Private sRuangan As String = String.Empty

#End Region
#Region "Function"
    Public Sub LoadMe(ByVal FormMode As Integer, ByVal CopyKode As String, ByVal KDREG As String, ByVal KDDOCTOR As String, ByVal KDCUSTOMER As String, ByVal NAMAPASIEN As String, ByVal TANGGALLAHIR As DateTime, ByVal TUJUAN As String, ByVal TANGGALDATANG As DateTime, ByVal NoId As String)
        oFormMode = FormMode
        sNoId = NoId

        txtNamaPasien.Text = NAMAPASIEN
        txtUmur.Text = TANGGALLAHIR.ToString("dd-MM-yyyy")
        txtNoPasien.Text = KDCUSTOMER

        sRegister = KDREG
        sRuangan = TUJUAN
        sKDDOCTOR = KDDOCTOR
        sCopyKode = CopyKode
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
        fn_LoadDoctorDPJP()

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

        cboKATEGORI.Properties.ReadOnly = Status
        txtDIAGNOSA.Properties.ReadOnly = Status
        grdDPJP.Properties.ReadOnly = Status
        txtRUANGAN.Properties.ReadOnly = Status
        txtMASALAHKEPERAWATAN.Properties.ReadOnly = Status
        txtRIWAYATALERGI.Properties.ReadOnly = Status
        txtRIWAYATREAKSI.Properties.ReadOnly = Status
        cboKESADARAN.Properties.ReadOnly = Status
        txtGCS.Properties.ReadOnly = Status
        cboE.Properties.ReadOnly = Status
        cboM.Properties.ReadOnly = Status
        cboV.Properties.ReadOnly = Status
        txtTD.Properties.ReadOnly = Status
        txtHR.Properties.ReadOnly = Status
        txtSUHU.Properties.ReadOnly = Status
        txtRR.Properties.ReadOnly = Status
        txtSPO2.Properties.ReadOnly = Status
        txtSKALANYERI.Properties.ReadOnly = Status
        txtOKSIGEN.Properties.ReadOnly = Status
        txtINFUS.Properties.ReadOnly = Status
        txtTRANSFUSI.Properties.ReadOnly = Status
        chkBAKNORMAL.Properties.ReadOnly = Status
        chkKATETER.Properties.ReadOnly = Status
        txtURINE.Properties.ReadOnly = Status
        chkBABNORMAL.Properties.ReadOnly = Status
        chkCOLOSTOMY.Properties.ReadOnly = Status
        chkORAL.Properties.ReadOnly = Status
        chkNGT.Properties.ReadOnly = Status
        chkMOBILISASI1.Properties.ReadOnly = Status
        chkMOBILISASI2.Properties.ReadOnly = Status
        chkMOBILISASI3.Properties.ReadOnly = Status
        txtSCORE.Properties.ReadOnly = Status
        txtKONSULTASI.Properties.ReadOnly = Status
        txtTHERAPY.Properties.ReadOnly = Status
        txtRENCANA1.Properties.ReadOnly = Status
        txtRENCANA2.Properties.ReadOnly = Status
        txtPEMBERIOPERAN.Properties.ReadOnly = Status
        txtPENERIMAOPERAN.Properties.ReadOnly = Status
        deDATEFrom.Properties.ReadOnly = Status
    End Sub
    Private Sub fn_EmptyMe()
        deDATEFrom.DateTime = Now
        cboKATEGORI.SelectedIndex = 0
        txtDIAGNOSA.ResetText()
        grdDPJP.Text = sKDDOCTOR
        txtRUANGAN.ResetText()
        txtMASALAHKEPERAWATAN.ResetText()
        txtRIWAYATALERGI.ResetText()
        txtRIWAYATREAKSI.ResetText()
        cboKESADARAN.ResetText()
        txtGCS.Text = 0
        cboE.SelectedIndex = 0
        cboM.SelectedIndex = 0
        cboV.SelectedIndex = 0
        txtTD.ResetText()
        txtHR.ResetText()
        txtSUHU.ResetText()
        txtRR.ResetText()
        txtSPO2.ResetText()
        txtSKALANYERI.ResetText()
        txtOKSIGEN.ResetText()
        txtTRANSFUSI.ResetText()
        txtINFUS.ResetText()
        chkBAKNORMAL.Checked = False
        chkKATETER.Checked = False
        txtURINE.ResetText()
        chkBABNORMAL.Checked = False
        chkCOLOSTOMY.Checked = False
        chkORAL.Checked = False
        chkNGT.Checked = False
        chkMOBILISASI1.Checked = False
        chkMOBILISASI2.Checked = False
        chkMOBILISASI3.Checked = False
        txtSCORE.ResetText()
        txtKONSULTASI.ResetText()
        txtTHERAPY.ResetText()
        txtRENCANA1.ResetText()
        txtRENCANA2.ResetText()
        txtPEMBERIOPERAN.ResetText()
        txtPENERIMAOPERAN.ResetText()

        txtNoRegister.Text = sRegister
        txtRUANGAN.Text = sRuangan
        fn_LoadDataCopy(sCopyKode)
    End Sub
    Private Sub fn_LoadDataCopy(ByVal Paramater As String)
        Try
            If Paramater = "" Then Exit Sub

            ' ***** HEADER *****
            Dim ds = oS_DIGITAL_HANDOVER.GetData(Paramater)

            With ds
                deDATEFrom.DateTime = Now
                cboKATEGORI.Text = .KATEGORI
                txtDIAGNOSA.Text = .DIAGNOSA
                grdDPJP.EditValue = .KDDOCTOR
                txtMASALAHKEPERAWATAN.Text = .MASALAHKEPERAWATAN
                txtRIWAYATALERGI.Text = .RIWAYATALERGI
                txtRIWAYATREAKSI.Text = .txtRIWAYATREAKSI
                cboKESADARAN.Text = .KESADARAN
                txtGCS.Text = .GCS
                cboE.Text = .E
                cboM.Text = .M
                cboV.Text = .V
                txtTD.Text = .TD
                txtHR.Text = .HR
                txtSUHU.Text = .SUHU
                txtRR.Text = .RR
                txtSPO2.Text = .SPO2
                txtSKALANYERI.Text = .SKALANYERI
                txtOKSIGEN.Text = .OKSIGEN
                txtTRANSFUSI.Text = .TRANSFUSI
                txtINFUS.Text = .INFUS
                chkBAKNORMAL.Checked = .BAKNORMAL
                chkKATETER.Checked = .KATETER
                txtURINE.Text = .URINE
                chkBABNORMAL.Checked = .BABNORMAL
                chkCOLOSTOMY.Checked = .COLOSTOMY
                chkORAL.Checked = .ORAL
                chkNGT.Checked = .NGT
                chkMOBILISASI1.Checked = .MOBILISASI1
                chkMOBILISASI2.Checked = .MOBILISASI2
                chkMOBILISASI3.Checked = .MOBILISASI3
                txtSCORE.Text = .SCORE
                txtKONSULTASI.Text = .KONSULTASI
                txtTHERAPY.Text = .THERAPY
                txtRENCANA1.Text = .RENCANA1
                txtRENCANA2.Text = .RENCANA2
                txtPEMBERIOPERAN.EditValue = .KDPEMBERIOPERAN
                txtPENERIMAOPERAN.EditValue = .KDPENERIMAOPERAN

            End With
        Catch oErr As Exception
            MsgBox("Load List Data : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadData()
        Try
            ' ***** HEADER *****
            Dim ds = oS_DIGITAL_HANDOVER.GetData(sNoId)

            With ds
                txtNoRegister.Text = .KDREG
                deDATEFrom.DateTime = .DATE
                cboKATEGORI.Text = .KATEGORI
                txtDIAGNOSA.Text = .DIAGNOSA
                grdDPJP.EditValue = .KDDOCTOR
                txtMASALAHKEPERAWATAN.Text = .MASALAHKEPERAWATAN
                txtRIWAYATALERGI.Text = .RIWAYATALERGI
                txtRIWAYATREAKSI.Text = .txtRIWAYATREAKSI
                cboKESADARAN.Text = .KESADARAN
                txtGCS.Text = .GCS
                cboE.Text = .E
                cboM.Text = .M
                cboV.Text = .V
                txtTD.Text = .TD
                txtHR.Text = .HR
                txtSUHU.Text = .SUHU
                txtRR.Text = .RR
                txtSPO2.Text = .SPO2
                txtSKALANYERI.Text = .SKALANYERI
                txtOKSIGEN.Text = .OKSIGEN
                txtTRANSFUSI.Text = .TRANSFUSI
                txtINFUS.Text = .INFUS
                chkBAKNORMAL.Checked = .BAKNORMAL
                chkKATETER.Checked = .KATETER
                txtURINE.Text = .URINE
                chkBABNORMAL.Checked = .BABNORMAL
                chkCOLOSTOMY.Checked = .COLOSTOMY
                chkORAL.Checked = .ORAL
                chkNGT.Checked = .NGT
                chkMOBILISASI1.Checked = .MOBILISASI1
                chkMOBILISASI2.Checked = .MOBILISASI2
                chkMOBILISASI3.Checked = .MOBILISASI3
                txtSCORE.Text = .SCORE
                txtKONSULTASI.Text = .KONSULTASI
                txtTHERAPY.Text = .THERAPY
                txtRENCANA1.Text = .RENCANA1
                txtRENCANA2.Text = .RENCANA2
                txtPEMBERIOPERAN.EditValue = .KDPEMBERIOPERAN
                txtPENERIMAOPERAN.EditValue = .KDPENERIMAOPERAN
                txtRUANGAN.Text = .KDUSER_SIGNATURE
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
            If txtRUANGAN.Text = String.Empty Then
                MsgBox("Dibutuhkan Ruangan", MsgBoxStyle.Exclamation, Me.Text)
                txtRUANGAN.Focus()
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
            Dim ds = oS_DIGITAL_HANDOVER.GetStructureHeader
            With ds
                .KDHO = sNoId
                .KDREG = txtNoRegister.Text
                Try
                    .DATECREATED = oS_DIGITAL_HANDOVER.GetData(txtNoRegister.Text).DATECREATED
                Catch ex As Exception
                    .DATECREATED = Now
                End Try
                .DATEUPDATED = Now
                .DATE = deDATEFrom.DateTime
                .KATEGORI = cboKATEGORI.Text
                .KDCUSTOMER = txtNoPasien.Text
                .DIAGNOSA = txtDIAGNOSA.Text
                .KDDOCTOR = grdDPJP.EditValue
                .DOKTER = grdDPJP.Text
                .MASALAHKEPERAWATAN = txtMASALAHKEPERAWATAN.Text
                .RIWAYATALERGI = txtRIWAYATALERGI.Text
                .txtRIWAYATREAKSI = txtRIWAYATREAKSI.Text
                .KESADARAN = cboKESADARAN.SelectedText
                .GCS = txtGCS.Text
                .E = cboE.Text
                .M = cboM.Text
                .V = cboV.Text
                .TD = txtTD.Text
                .HR = txtHR.Text
                .SUHU = txtSUHU.Text
                .RR = txtRR.Text
                .SPO2 = txtSPO2.Text
                .SKALANYERI = txtSKALANYERI.Text
                .OKSIGEN = txtOKSIGEN.Text
                .TRANSFUSI = txtTRANSFUSI.Text
                .INFUS = txtINFUS.Text
                .BAKNORMAL = chkBAKNORMAL.Checked
                .KATETER = chkKATETER.Checked
                .URINE = txtURINE.Text
                .BABNORMAL = chkBABNORMAL.Checked
                .COLOSTOMY = chkCOLOSTOMY.Checked
                .ORAL = chkORAL.Checked
                .NGT = chkNGT.Checked
                .MOBILISASI1 = chkMOBILISASI1.Checked
                .MOBILISASI2 = chkMOBILISASI2.Checked
                .MOBILISASI3 = chkMOBILISASI3.Checked
                .SCORE = txtSCORE.Text
                .KONSULTASI = txtKONSULTASI.Text
                .THERAPY = txtTHERAPY.Text
                .RENCANA1 = txtRENCANA1.Text
                .RENCANA2 = txtRENCANA2.Text
                .KDPEMBERIOPERAN = txtPEMBERIOPERAN.EditValue
                .PEMBERIOPERAN = txtPEMBERIOPERAN.Text
                .KDPENERIMAOPERAN = txtPENERIMAOPERAN.EditValue
                .PENERIMAOPERAN = txtPENERIMAOPERAN.Text

                Try
                    .CETAK = oS_DIGITAL_HANDOVER.GetData(txtNoRegister.Text).CETAK
                Catch ex As Exception
                    .CETAK = 0
                End Try
                .KDUSER = sUserID
                .KDUSER_SIGNATURE = txtRUANGAN.Text
            End With

            If oFormMode = FORM_MODE.FORM_MODE_ADD Then
                Try
                    fn_Save = oS_DIGITAL_HANDOVER.InsertData(ds)
                Catch ex As Exception
                    MsgBox("Simpan Data : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            ElseIf oFormMode = FORM_MODE.FORM_MODE_EDIT Then
                Try
                    fn_Save = oS_DIGITAL_HANDOVER.UpdateData(ds)
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
            'Case Keys.F2
            '    If btnSaveNew.Enabled = True Then
            '        btnSaveNew_Click()
            '    End If
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
    Private Sub btnSaveNew_Click() Handles btnSaveNew.ItemClick
        If fn_Validate() = False Then Exit Sub
        If MsgBox("Save " & txtNoRegister.Text.Trim.ToUpper & "?", MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, Me.Text) = MsgBoxResult.No Then Exit Sub
        If fn_Save() = False Then
            MsgBox("Save gagal! Tolong kontak admin anda!", MsgBoxStyle.Exclamation, Me.Text)
        Else
            MsgBox("Save " & txtNoRegister.Text.Trim.ToUpper & " success!", MsgBoxStyle.Information, Me.Text)
            'sStatusSave = "NEW"
            Me.Close()
        End If
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
    Private Sub fn_LoadDoctorDPJP()
        Try
            Dim oDoctor As New Reference.clsDoctor

            Dim dsDoctor = From x In oDoctor.GetData()
                           Where x.ISACTIVE = True
                           Select x.KDDOCTOR, x.NAME_DISPLAY

            grdDPJP.Properties.DataSource = dsDoctor.ToList()
            grdDPJP.Properties.ValueMember = "KDDOCTOR"
            grdDPJP.Properties.DisplayMember = "NAME_DISPLAY"
        Catch oErr As Exception
            MsgBox("Load Dokter Data : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub

    Private Sub cboE_EditValueChanged(sender As Object, e As EventArgs) Handles cboE.EditValueChanged
        fn_GCSValue()
    End Sub

    Private Sub cboM_EditValueChanged(sender As Object, e As EventArgs) Handles cboM.EditValueChanged
        fn_GCSValue()
    End Sub

    Private Sub cboV_EditValueChanged(sender As Object, e As EventArgs) Handles cboV.EditValueChanged
        fn_GCSValue()
    End Sub
    Private Sub fn_GCSValue()
        Try
            Dim val As Integer
            val = CInt(cboE.EditValue) + CInt(cboM.EditValue) + CInt(cboV.EditValue)
            txtGCS.Text = val.ToString()
        Catch ex As Exception
            MsgBox("Load Sub Spesialis Data : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, "Hand Over Shift")
        End Try

    End Sub

    Private Sub chkBAKNORMAL_CheckedChanged(sender As Object, e As EventArgs) Handles chkBAKNORMAL.CheckedChanged
        If chkBAKNORMAL.Checked=False Then
            chkKATETER.Checked = True
        Else
            chkKATETER.Checked = False
        End If
    End Sub
    Private Sub frmHandOver_MouseWheel(sender As Object, e As MouseEventArgs) Handles MyBase.MouseWheel
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