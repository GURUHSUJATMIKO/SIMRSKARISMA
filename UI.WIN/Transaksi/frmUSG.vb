Imports DataAccess
Imports UI.WIN.MAIN.My.Resources
Imports System.Linq

Public Class frmUSG
#Region "Declaration"
    Private oFormMode As FORM_MODE = FORM_MODE.FORM_MODE_VIEW
    Private sNoId As Integer
    Private isLoad As Boolean = False
    Private oUSG As New Transaksi.clsUSG

#End Region
#Region "Function"
    Public Sub LoadMe(ByVal FormMode As Integer, Optional ByVal NoId As Integer = 0)
        oFormMode = FormMode
        sNoId = NoId
    End Sub
    Private Sub Form_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        fn_ChangeFormState()
        fn_LoadLanguage()
        isLoad = True
    End Sub
    Public Sub fn_LoadLanguage()
        Try
            Me.Text = USG.TITLE

            btnSaveNew.Caption = Caption.FormSaveNew
            btnSaveClose.Caption = Caption.FormSaveClose
            btnClose.Caption = Caption.FormClose
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub Form_Closing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        sCode = txtKDUSG.Text.Trim.ToUpper
    End Sub
    Private Sub fn_ChangeFormState()
        fn_LoadDoctor()
        fn_LoadPenjamin()

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
        btnPending.Enabled = Not Status

        deDATE.Properties.ReadOnly = Status
        txtKDTARIF.Properties.ReadOnly = Status
        txtGEJALA.Properties.ReadOnly = Status
        txtDESCRIPTION.Properties.ReadOnly = Status
    End Sub
    Private Sub fn_EmptyMe()
        txtKDUSG.Text = "<--- AUTO --->"
        deDATE.DateTime = Now
        txtKDKUNJUNGAN.ResetText()
        txtKDPENDAFTARAN.ResetText()
        grdKDDOCTOR_H.ResetText()
        grdKDPENJAMIN.ResetText()
        txtKDTARIF.ResetText()
        txtGEJALA.ResetText()
        txtDESCRIPTION.ResetText()
    End Sub
    Private Sub fn_LoadData()
        Try
            ' ***** HEADER *****
            Dim ds = oUSG.GetData(sNoId)

            With ds
                txtKDUSG.Text = .KDUSG
                deDATE.DateTime = .DATE
                txtKDPENDAFTARAN.Text = .KDPENDAFTARAN
                grdKDPENJAMIN.Text = .KDPENJAMIN
                txtKDKUNJUNGAN.Text = .KDKUNJUNGAN
                fn_LoadDataRegister(.KDKUNJUNGAN)
                txtKDTARIF.Text = .KDTARIF
                txtGEJALA.Text = .GEJALA
                txtDESCRIPTION.Text = .DESCRIPTION
            End With
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Function fn_Validate() As Boolean
        Try
            fn_Validate = True

            If txtKDPENDAFTARAN.Text = String.Empty Then
                txtKDPENDAFTARAN.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                txtKDPENDAFTARAN.ErrorText = Statement.ErrorRequired

                txtKDPENDAFTARAN.Focus()
                fn_Validate = False
                Exit Function
            End If
            If grdKDPENJAMIN.Text = String.Empty Then
                grdKDPENJAMIN.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                grdKDPENJAMIN.ErrorText = Statement.ErrorRequired

                grdKDPENJAMIN.Focus()
                fn_Validate = False
                Exit Function
            End If
            If txtKDKUNJUNGAN.Text = String.Empty Then
                txtKDKUNJUNGAN.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                txtKDKUNJUNGAN.ErrorText = Statement.ErrorRequired

                txtKDKUNJUNGAN.Focus()
                fn_Validate = False
                Exit Function
            End If
            If grdKDDOCTOR_H.Text = String.Empty Then
                grdKDDOCTOR_H.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                grdKDDOCTOR_H.ErrorText = Statement.ErrorRequired

                grdKDDOCTOR_H.Focus()
                fn_Validate = False
                Exit Function
            End If
            If txtDESCRIPTION.Text = String.Empty Then
                txtDESCRIPTION.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                txtDESCRIPTION.ErrorText = Statement.ErrorRequired

                txtDESCRIPTION.Focus()
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
            Dim ds = oUSG.GetStructureHeader
            With ds
                Try
                    .DATECREATED = oUSG.GetData(sNoId).DATECREATED
                Catch oErr As Exception
                    .DATECREATED = Now
                End Try
                .DATEUPDATED = Now
                .DATE = deDATE.DateTime
                .KDUSG = sNoId
                .KDPENDAFTARAN = txtKDPENDAFTARAN.Text
                .KDKUNJUNGAN = txtKDKUNJUNGAN.Text
                .KDPENJAMIN = grdKDPENJAMIN.EditValue
                .KDTARIF = txtKDTARIF.Text.ToString.Trim
                .GEJALA = txtGEJALA.Text.ToString.Trim
                .DESCRIPTION = txtDESCRIPTION.Text.ToString.Trim
                .KDUSER = sUserID
            End With

            If oFormMode = FORM_MODE.FORM_MODE_ADD Then
                Try
                    fn_Save = oUSG.InsertData(ds)
                Catch oErr As Exception
                    MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            ElseIf oFormMode = FORM_MODE.FORM_MODE_EDIT Then
                Try
                    fn_Save = oUSG.UpdateData(ds)
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
#Region "Grid Method"

#End Region
#Region "Command Button"
    Private Sub frmUSG_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
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
#End Region
#Region "Lookup / Event"
    Private Sub fn_LoadPenjamin()
        Dim oPenjamin As New Reference.clsPENJAMIN
        Try
            grdKDPENJAMIN.Properties.DataSource = oPenjamin.GetData.Where(Function(x) x.ISACTIVE = True).ToList()
            grdKDPENJAMIN.Properties.ValueMember = "KDPENJAMIN"
            grdKDPENJAMIN.Properties.DisplayMember = "MEMO"
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadDoctor()
        Dim oDoctor As New Reference.clsDoctor
        Try
            grdKDDOCTOR_H.Properties.DataSource = oDoctor.GetData.Where(Function(x) x.ISACTIVE = True).ToList()
            grdKDDOCTOR_H.Properties.ValueMember = "KDDOCTOR"
            grdKDDOCTOR_H.Properties.DisplayMember = "NAME_DISPLAY"
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub txtCARI_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtCARI.KeyPress
        If Asc(e.KeyChar) = 13 Then
            fn_LoadPendaftaranRekamMedisList(txtCARI.Text)
        End If
    End Sub
    Private Sub fn_LoadPendaftaranRekamMedisList(ByVal sParameter As String)
        Try
            If sParameter = String.Empty Then Exit Sub

            Dim oPendaftaran_KunjunganPoli As New Admission.clsPendaftaran_KunjunganPoli
            Dim oPendaftaran_KunjunganRuangan As New Admission.clsPendaftaran_KunjunganRuangan

            If cboCARI.SelectedIndex = 0 Then
                Dim dsCustomerRJList = From x In oPendaftaran_KunjunganPoli.GetDataByRekamMedis(sParameter)
                                       Select Kode = x.KDKUNJUNGAN_POLI, TanggalDatang = x.S_PENDAFTARAN_H.DATE.ToString("dd-MM-yyyy"), Tujuan = x.M_DEPARTMENT.NAME_DISPLAY, Penjamin = x.M_PENJAMIN.MEMO, NoSEP = x.S_PENDAFTARAN_H.NOMORSEP, NoKartuBPJS = x.S_PENDAFTARAN_H.KARTUBPJS, Pasien = x.S_PENDAFTARAN_H.M_CUSTOMER.NAME_DISPLAY, TanggalLahir = x.S_PENDAFTARAN_H.M_CUSTOMER.TANGGALLAHIR.ToString("dd-MM-yyyy")

                Dim dsCustomerRIList = From x In oPendaftaran_KunjunganRuangan.GetDataByRekamMedis(sParameter)
                                       Select Kode = x.KDKUNJUNGAN_RUANGAN, TanggalDatang = x.S_PENDAFTARAN_H.DATE.ToString("dd-MM-yyyy"), Tujuan = x.M_RUANGRAWAT.NAME_DISPLAY, Penjamin = x.M_PENJAMIN.MEMO, NoSEP = x.S_PENDAFTARAN_H.NOMORSEP, NoKartuBPJS = x.S_PENDAFTARAN_H.KARTUBPJS, Pasien = x.S_PENDAFTARAN_H.M_CUSTOMER.NAME_DISPLAY, TanggalLahir = x.S_PENDAFTARAN_H.M_CUSTOMER.TANGGALLAHIR.ToString("dd-MM-yyyy")

                Dim dsCustomerList = dsCustomerRJList.Union(dsCustomerRIList)

                grdCARIKDREGAWAL.Properties.DataSource = dsCustomerList.ToList()
                grdCARIKDREGAWAL.Properties.ValueMember = "Kode"
                grdCARIKDREGAWAL.Properties.DisplayMember = "Pasien"

            Else
                Dim dsCustomerRJList = From x In oPendaftaran_KunjunganPoli.GetDataByRekamNama(sParameter)
                                       Select Kode = x.KDKUNJUNGAN_POLI, Tujuan = x.M_DEPARTMENT.NAME_DISPLAY, Penjamin = x.M_PENJAMIN.MEMO, NoSEP = x.S_PENDAFTARAN_H.NOMORSEP, NoKartuBPJS = x.S_PENDAFTARAN_H.KARTUBPJS, Pasien = x.S_PENDAFTARAN_H.M_CUSTOMER.NAME_DISPLAY, TanggalLahir = x.S_PENDAFTARAN_H.M_CUSTOMER.TANGGALLAHIR.ToString("dd-MM-yyyy")

                Dim dsCustomerRIList = From x In oPendaftaran_KunjunganRuangan.GetDataByRekamNama(sParameter)
                                       Select Kode = x.KDKUNJUNGAN_RUANGAN, Tujuan = x.M_RUANGRAWAT.NAME_DISPLAY, Penjamin = x.M_PENJAMIN.MEMO, NoSEP = x.S_PENDAFTARAN_H.NOMORSEP, NoKartuBPJS = x.S_PENDAFTARAN_H.KARTUBPJS, Pasien = x.S_PENDAFTARAN_H.M_CUSTOMER.NAME_DISPLAY, TanggalLahir = x.S_PENDAFTARAN_H.M_CUSTOMER.TANGGALLAHIR.ToString("dd-MM-yyyy")

                Dim dsCustomerList = dsCustomerRJList.Union(dsCustomerRIList)

                grdCARIKDREGAWAL.Properties.DataSource = dsCustomerList.ToList()
                grdCARIKDREGAWAL.Properties.ValueMember = "Kode"
                grdCARIKDREGAWAL.Properties.DisplayMember = "Pasien"

            End If

            grdCARIKDREGAWAL.ShowPopup()
            grvCARIKDPENDAFTARAN_AWAL.BestFitColumns()
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub grdCARIKDREGAWAL_KeyPress(sender As Object, e As KeyPressEventArgs) Handles grdCARIKDREGAWAL.KeyPress
        fn_LoadDataRegister(grdCARIKDREGAWAL.EditValue)
    End Sub
    Private Sub fn_LoadDataRegister(ByVal Parameter As String)
        Dim oPendafataran_KunjunganPoli As New Admission.clsPendaftaran_KunjunganPoli
        Dim dsRJ = oPendafataran_KunjunganPoli.GetData(Parameter)
        If dsRJ IsNot Nothing Then
            txtKDKUNJUNGAN.Text = dsRJ.KDKUNJUNGAN_POLI
            grdKDDOCTOR_H.Text = dsRJ.KDDOCTOR
            txtKDPENDAFTARAN.Text = dsRJ.KDPENDAFTARAN
            grdKDPENJAMIN.Text = dsRJ.KDPENJAMIN
            txtTUJUAN.Text = dsRJ.M_DEPARTMENT.NAME_DISPLAY
        Else
            Dim oPendafataran_KunjunganRuangan As New Admission.clsPendaftaran_KunjunganRuangan
            Dim dsRI = oPendafataran_KunjunganRuangan.GetData(Parameter)
            If dsRI IsNot Nothing Then
                txtKDKUNJUNGAN.Text = dsRI.KDKUNJUNGAN_RUANGAN
                grdKDDOCTOR_H.Text = dsRI.KDDOCTOR
                txtKDPENDAFTARAN.Text = dsRI.KDPENDAFTARAN
                grdKDPENJAMIN.Text = dsRI.KDPENJAMIN
                txtTUJUAN.Text = dsRI.M_RUANGRAWAT.NAME_DISPLAY
            Else
                fn_EmptyMe()
            End If
        End If
    End Sub
#End Region
End Class