Imports DataAccess
Imports UI.WIN.MAIN.My.Resources
Imports System.Linq

Public Class frmCashIn
#Region "Declaration"
    Private oFormMode As FORM_MODE = FORM_MODE.FORM_MODE_VIEW
    Private sNoId As String
    Private isLoad As Boolean = False
    Private oCashIn As New Finance.clsCashIn
    Private sKDKUNJUNGAN As String = String.Empty

#End Region
#Region "Function"
    Public Sub fn_LoadKunjungan(ByVal KDKUNJUNGAN As String)
        sKDKUNJUNGAN = KDKUNJUNGAN
    End Sub
    Public Sub LoadMe(ByVal FormMode As Integer, Optional ByVal NoId As String = "")
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
            Me.Text = CashIn.TITLE

            lKDCASH.Text = CashIn.KDCASHIN
            lDATE.Text = CashIn.TANGGAL
            lKDPENDAFTARAN.Text = CashIn.KDPENDAFTARAN & " *"
            lKDPENJAMIN.Text = CashIn.KDPENJAMIN & " *"
            lKDPAYMENTTYPE.Text = CashIn.KDPAYMENTYPE & " *"

            lSUBTOTAL.Text = CashIn.SUBTOTAL
            'lADMIN.Text = CashIn.ADMIN
            lROUND.Text = CashIn.ROUND
            lGRANDTOTAL.Text = CashIn.GRANDTOTAL
            lDEPOSIT.Text = CashIn.DEPOSIT
            lDISCOUNT_PERSEN.Text = CashIn.DISCOUNT_PERSEN
            lCHARGE_PERSEN.Text = CashIn.CHARGE_PERSEN

            btnSaveNew.Caption = Caption.FormSaveNew
            btnSaveClose.Caption = Caption.FormSaveClose
            btnClose.Caption = Caption.FormClose
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub Form_Closing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        sCode = txtKDCASHIN.Text.Trim.ToUpper
    End Sub
    Private Sub fn_ChangeFormState()
        fn_LoadKDPAYMENTTYPE()
        fn_LoadKDPENJAMIN()
        fn_LoadKDUNITKASIR()

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
        txtKDPENDAFTARAN.Properties.ReadOnly = True
        txtKDPENDAFTARAN_AWAL.Properties.ReadOnly = True
        grdKDPENJAMIN.Properties.ReadOnly = True
        If oFormMode = FORM_MODE.FORM_MODE_ADD Then
            grdKDUNITKASIR.Properties.ReadOnly = False
        Else
            grdKDUNITKASIR.Properties.ReadOnly = True
        End If
        grdKDPAYMENTTYPE.Properties.ReadOnly = Status
        txtSUBTOTAL.Properties.ReadOnly = True
        'txtADMIN.Properties.ReadOnly = Status
        txtROUND.Properties.ReadOnly = Status
        txtDEPOSIT.Properties.ReadOnly = True
        txtDISCOUNT.Properties.ReadOnly = True
        txtDISCOUNT_PERSEN.Properties.ReadOnly = Status
        txtCHARGE_PERSEN.Properties.ReadOnly = Status
        txtCHARGE.Properties.ReadOnly = True
        txtGRANDTOTAL.Properties.ReadOnly = Status
        txtMEMO.Properties.ReadOnly = Status

        grvDetail.OptionsBehavior.ReadOnly = Status
    End Sub
    Private Sub fn_EmptyMe()
        txtKDCASHIN.Text = "<--- AUTO --->"
        deDATE.DateTime = Now
        txtKDPENDAFTARAN.ResetText()
        txtKDPENDAFTARAN_AWAL.ResetText()
        If sKDUNITKASIR = String.Empty Then
            grdKDUNITKASIR.Text = oCashIn.UnitKasir_Default
        Else
            grdKDUNITKASIR.Text = sKDUNITKASIR
        End If
        grdKDPENJAMIN.ResetText()
        grdKDPAYMENTTYPE.Text = oCashIn.TypePembayaran_Default
        txtMEMO.ResetText()
        txtSUBTOTAL.ResetText()
        'txtADMIN.ResetText()
        txtROUND.ResetText()
        txtDEPOSIT.ResetText()
        txtDISCOUNT.ResetText()
        txtDISCOUNT_PERSEN.ResetText()
        txtCHARGE_PERSEN.ResetText()
        txtCHARGE.ResetText()
        txtGRANDTOTAL.ResetText()

        If sKDKUNJUNGAN <> "" Then
            fn_LoadKunjunganTransakasi(sKDKUNJUNGAN)
            Calculate()
        End If
    End Sub
    Private Sub fn_LoadData()
        Try
            ' ***** HEADER *****
            Dim ds = oCashIn.GetData(sNoId)

            With ds
                txtKDCASHIN.Text = .KDCASHIN
                deDATE.DateTime = .DATE
                txtKDPENDAFTARAN.Text = .KDPENDAFTARAN
                txtKDPENDAFTARAN_AWAL.Text = .KDPENDAFTARAN_AWAL
                grdKDPENJAMIN.Text = .KDPENJAMIN
                grdKDUNITKASIR.Text = .KDUNITKASIR
                grdKDPAYMENTTYPE.Text = .KDPAYMENTTYPE
                txtSUBTOTAL.Text = .SUBTOTAL
                'txtADMIN.Text = .ADMIN
                txtROUND.Text = .ROUND
                txtDEPOSIT.Text = .DEPOSIT
                txtDISCOUNT.Text = .DISCOUNT
                txtDISCOUNT_PERSEN.Text = .DISCOUNT_PERSEN
                txtCHARGE.Text = .CHARGE
                txtCHARGE_PERSEN.Text = .CHARGE_PERSEN
                txtGRANDTOTAL.Text = .GRANDTOTAL
                txtMEMO.Text = .MEMO

                bindingSource.DataSource = oCashIn.GetDataDetail.Where(Function(x) x.KDCASHIN = sNoId).OrderBy(Function(x) x.SEQ).ToList()
                grdDetail.DataSource = bindingSource

            End With
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Function fn_Validate() As Boolean
        Try
            fn_Validate = True

            If grdKDPAYMENTTYPE.Text = String.Empty Then
                grdKDPAYMENTTYPE.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                grdKDPAYMENTTYPE.ErrorText = Statement.ErrorRequired

                grdKDPAYMENTTYPE.Focus()
                fn_Validate = False
                Exit Function
            End If
            If txtKDPENDAFTARAN.Text = String.Empty Then
                txtKDPENDAFTARAN.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                txtKDPENDAFTARAN.ErrorText = Statement.ErrorRequired

                txtKDPENDAFTARAN.Focus()
                fn_Validate = False
                Exit Function
            End If
            If grdKDUNITKASIR.Text = String.Empty Then
                grdKDUNITKASIR.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                grdKDUNITKASIR.ErrorText = Statement.ErrorRequired

                grdKDUNITKASIR.Focus()
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

            grvDetail.UpdateCurrentRow()
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Function
    Private Function fn_Save() As Boolean
        Try
            ' ***** HEADER *****
            Dim ds = oCashIn.GetStructureHeader
            With ds
                Try
                    .DATECREATED = oCashIn.GetData(sNoId).DATECREATED
                Catch oErr As Exception
                    .DATECREATED = Now
                End Try
                .DATEUPDATED = Now

                .KDCASHIN = sNoId
                .DATE = deDATE.DateTime
                .KDPENDAFTARAN = txtKDPENDAFTARAN.Text
                .KDPENDAFTARAN_AWAL = txtKDPENDAFTARAN_AWAL.Text
                .KDPENJAMIN = grdKDPENJAMIN.EditValue
                .KDUNITKASIR = grdKDUNITKASIR.EditValue
                .KDKUNJUNGAN = ""
                .KDPAYMENTTYPE = grdKDPAYMENTTYPE.EditValue
                .MEMO = "INVOICE NO. : "
                .SUBTOTAL = CDec(txtSUBTOTAL.Text)
                .ADMIN = CDec(0)
                .ROUND = CDec(txtROUND.Text)
                .DEPOSIT = CDec(txtDEPOSIT.Text)
                .DISCOUNT = CDec(txtDISCOUNT.Text)
                .DISCOUNT_PERSEN = CDec(txtDISCOUNT_PERSEN.Text)
                .CHARGE = CDec(txtCHARGE.Text)
                .CHARGE_PERSEN = CDec(txtCHARGE_PERSEN.Text)
                .GRANDTOTAL = CDec(txtGRANDTOTAL.Text)
                .KDUSER = sUserID
                Try
                    .MODUL = oCashIn.GetData(sNoId).MODUL
                Catch ex As Exception
                    Dim oUnitKasir As New Reference.clsUnitKasir
                    Dim dsUnitKasir = oUnitKasir.GetData(grdKDUNITKASIR.EditValue)
                    If dsUnitKasir IsNot Nothing Then
                        .MODUL = dsUnitKasir.MODUL
                    Else
                        .MODUL = "CI"
                    End If
                End Try
                Try
                    .ISCHEKED = oCashIn.GetData(sNoId).ISCHEKED
                Catch ex As Exception
                    .ISCHEKED = False
                End Try
            End With

            ' ***** DETIL *****
            Dim arrDetail = oCashIn.GetStructureDetailList
            For i As Integer = 0 To grvDetail.RowCount - 2
                Dim dsDetail = oCashIn.GetStructureDetail
                With dsDetail
                    Try
                        .DATECREATED = oCashIn.GetData(sNoId).DATECREATED
                    Catch oErr As Exception
                        .DATECREATED = Now
                    End Try
                    .DATEUPDATED = Now

                    .SEQ = i
                    .KDCASHIN = ds.KDCASHIN
                    .NOINVOICE = grvDetail.GetRowCellValue(i, colNOINVOICE)
                    ds.MEMO &= .NOINVOICE & ", "
                    .AMOUNTPAYMENT = CDec(grvDetail.GetRowCellValue(i, colAMOUNTPAYMENT))
                    .REMARKS = IIf(String.IsNullOrEmpty(grvDetail.GetRowCellValue(i, colREMARKS)), "-", grvDetail.GetRowCellValue(i, colREMARKS))
                End With
                arrDetail.Add(dsDetail)
            Next

            ds.MEMO &= grdKDPAYMENTTYPE.Text & IIf(txtMEMO.Text.Trim.ToUpper = String.Empty, "", " - " & txtMEMO.Text.Trim.ToUpper)

            If oFormMode = FORM_MODE.FORM_MODE_ADD Then
                Try
                    txtKDCASHIN.Text = oCashIn.InsertData(ds, arrDetail)

                    If txtKDCASHIN.Text = "" Then
                        fn_Save = False
                    Else
                        fn_Save = True
                    End If

                Catch oErr As Exception
                    MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            ElseIf oFormMode = FORM_MODE.FORM_MODE_EDIT Then
                Try
                    fn_Save = oCashIn.UpdateData(ds, arrDetail)
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
    Private Sub OnValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtROUND.EditValueChanged, txtCHARGE.EditValueChanged, txtDISCOUNT.EditValueChanged, txtDEPOSIT.EditValueChanged, grvDetail.FocusedRowChanged
        If isLoad Then
            Calculate()
        End If
    End Sub
    Private Sub Calculate()
        Dim sSubTotal = 0
        For i As Integer = 0 To grvDetail.RowCount - 2
            sSubTotal += CDec(grvDetail.GetRowCellValue(i, colAMOUNTPAYMENT))
        Next

        txtSUBTOTAL.Text = sSubTotal

        txtGRANDTOTAL.Text = CDec(txtSUBTOTAL.Text) + CDec(txtROUND.Text) + CDec(txtCHARGE.Text) - CDec(txtDISCOUNT.Text) - CDec(txtDEPOSIT.Text)
    End Sub
#End Region
#Region "Command Button"
    Private Sub frmCashIn_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
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
    Private Sub DeleteToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DeleteToolStripMenuItem.Click
        If oFormMode = FORM_MODE.FORM_MODE_VIEW Then Exit Sub
        grvDetail.DeleteSelectedRows()
    End Sub
    Private Sub fn_LoadKDUNITKASIR()
        Dim oUNITKASIR As New Reference.clsUnitKasir
        Try
            grdKDUNITKASIR.Properties.DataSource = oUNITKASIR.GetData.Where(Function(x) x.ISACTIVE = True).ToList()
            grdKDUNITKASIR.Properties.ValueMember = "KDUNITKASIR"
            grdKDUNITKASIR.Properties.DisplayMember = "MEMO"
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadKDPENJAMIN()
        Dim oPENJAMIN As New Reference.clsPENJAMIN
        Try
            grdKDPENJAMIN.Properties.DataSource = oPENJAMIN.GetData.Where(Function(x) x.ISACTIVE = True).ToList()
            grdKDPENJAMIN.Properties.ValueMember = "KDPENJAMIN"
            grdKDPENJAMIN.Properties.DisplayMember = "MEMO"
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadKDPAYMENTTYPE()
        Dim oPAYMENTTYPE As New Reference.clsPaymentType
        Try
            grdKDPAYMENTTYPE.Properties.DataSource = oPAYMENTTYPE.GetData.Where(Function(x) x.ISACTIVE = True).ToList()
            grdKDPAYMENTTYPE.Properties.ValueMember = "KDPAYMENTTYPE"
            grdKDPAYMENTTYPE.Properties.DisplayMember = "MEMO"
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub grdKDPAYMENTTYPE_KeyDown(sender As System.Object, e As System.Windows.Forms.KeyEventArgs) Handles grdKDPAYMENTTYPE.KeyDown
        If e.KeyCode = Keys.Delete Then
            grdKDPAYMENTTYPE.ResetText()
        End If
    End Sub
    Private Sub txtCARI_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtCARI.KeyPress
        If Asc(e.KeyChar) = 13 Then
            fn_LoadPendaftaranRekamMedisList(txtCARI.Text)
        End If
    End Sub
    Private Sub fn_LoadPendaftaranRekamMedisList(ByVal sParameter As String)
        Try
            If sParameter = String.Empty Then Exit Sub

            If cboFilter.SelectedIndex = 0 Then
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
                                           Select Kode = x.KDKUNJUNGAN_POLI, TanggalDatang = x.S_PENDAFTARAN_H.DATE.ToString("dd-MM-yyyy"), Tujuan = x.M_DEPARTMENT.NAME_DISPLAY, Penjamin = x.M_PENJAMIN.MEMO, NoSEP = x.S_PENDAFTARAN_H.NOMORSEP, NoKartuBPJS = x.S_PENDAFTARAN_H.KARTUBPJS, Pasien = x.S_PENDAFTARAN_H.M_CUSTOMER.NAME_DISPLAY, TanggalLahir = x.S_PENDAFTARAN_H.M_CUSTOMER.TANGGALLAHIR.ToString("dd-MM-yyyy")

                    Dim dsCustomerRIList = From x In oPendaftaran_KunjunganRuangan.GetDataByRekamNama(sParameter)
                                           Select Kode = x.KDKUNJUNGAN_RUANGAN, TanggalDatang = x.S_PENDAFTARAN_H.DATE.ToString("dd-MM-yyyy"), Tujuan = x.M_RUANGRAWAT.NAME_DISPLAY, Penjamin = x.M_PENJAMIN.MEMO, NoSEP = x.S_PENDAFTARAN_H.NOMORSEP, NoKartuBPJS = x.S_PENDAFTARAN_H.KARTUBPJS, Pasien = x.S_PENDAFTARAN_H.M_CUSTOMER.NAME_DISPLAY, TanggalLahir = x.S_PENDAFTARAN_H.M_CUSTOMER.TANGGALLAHIR.ToString("dd-MM-yyyy")

                    Dim dsCustomerList = dsCustomerRJList.Union(dsCustomerRIList)

                    grdCARIKDREGAWAL.Properties.DataSource = dsCustomerList.ToList()
                    grdCARIKDREGAWAL.Properties.ValueMember = "Kode"
                    grdCARIKDREGAWAL.Properties.DisplayMember = "Pasien"

                End If
            ElseIf cboFilter.SelectedIndex = 1 Then

                If cboCARI.SelectedIndex = 0 Then
                    Dim oPendaftaran_TanpaResep As New Transaksi.clsBillingTanpaResep

                    Dim dsCustomerPasienLuarList = From x In oPendaftaran_TanpaResep.GetDataBYRM(sParameter)
                                                   Select Kode = x.KDBILLING_TANPARESEP, TanggalDatang = x.DATE.ToString("dd-MM-yyyy"), Tujuan = "Resep Luar", Penjamin = x.M_PENJAMIN.MEMO, Pasien = x.M_CUSTOMER_UNIT.NAME_DISPLAY

                    grdCARIKDREGAWAL.Properties.DataSource = dsCustomerPasienLuarList.ToList()
                    grdCARIKDREGAWAL.Properties.ValueMember = "Kode"
                    grdCARIKDREGAWAL.Properties.DisplayMember = "Pasien"
                Else
                    Dim oPendaftaran_TanpaResep As New Transaksi.clsBillingTanpaResep

                    Dim dsCustomerPasienLuarList = From x In oPendaftaran_TanpaResep.GetDataBYNama(sParameter)
                                                   Select Kode = x.KDBILLING_TANPARESEP, TanggalDatang = x.DATE.ToString("dd-MM-yyyy"), Tujuan = "Resep Luar", Penjamin = x.M_PENJAMIN.MEMO, Pasien = x.M_CUSTOMER_UNIT.NAME_DISPLAY

                    grdCARIKDREGAWAL.Properties.DataSource = dsCustomerPasienLuarList.ToList()
                    grdCARIKDREGAWAL.Properties.ValueMember = "Kode"
                    grdCARIKDREGAWAL.Properties.DisplayMember = "Pasien"
                End If

            Else
                If cboCARI.SelectedIndex = 0 Then
                    Dim oPendaftaran_Unit As New Transaksi.clsBillingUnit

                    Dim dsCustomerPasienLuarList = From x In oPendaftaran_Unit.GetDataBYRM(sParameter)
                                                   Select Kode = x.KDBILLING_UNIT, TanggalDatang = x.DATE.ToString("dd-MM-yyyy"), Tujuan = "Resep Luar", Penjamin = x.M_PENJAMIN.MEMO, Pasien = x.M_CUSTOMER_UNIT.NAME_DISPLAY

                    grdCARIKDREGAWAL.Properties.DataSource = dsCustomerPasienLuarList.ToList()
                    grdCARIKDREGAWAL.Properties.ValueMember = "Kode"
                    grdCARIKDREGAWAL.Properties.DisplayMember = "Pasien"
                Else
                    Dim oPendaftaran_Unit As New Transaksi.clsBillingUnit

                    Dim dsCustomerPasienLuarList = From x In oPendaftaran_Unit.GetDataBYNama(sParameter)
                                                   Select Kode = x.KDBILLING_UNIT, TanggalDatang = x.DATE.ToString("dd-MM-yyyy"), Tujuan = "Resep Luar", Penjamin = x.M_PENJAMIN.MEMO, Pasien = x.M_CUSTOMER_UNIT.NAME_DISPLAY

                    grdCARIKDREGAWAL.Properties.DataSource = dsCustomerPasienLuarList.ToList()
                    grdCARIKDREGAWAL.Properties.ValueMember = "Kode"
                    grdCARIKDREGAWAL.Properties.DisplayMember = "Pasien"
                End If
            End If

            txtCARI.ResetText()
            grdCARIKDREGAWAL.ShowPopup()
            fn_LoadFormatData()
            grvCARIKDPENDAFTARAN_AWAL.BestFitColumns()
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadFormatData()
        For iLoop As Integer = 0 To grvCARIKDPENDAFTARAN_AWAL.Columns.Count - 1
            If grvCARIKDPENDAFTARAN_AWAL.Columns(iLoop).ColumnType.Name = "Decimal" Then
                grvCARIKDPENDAFTARAN_AWAL.Columns(iLoop).DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                grvCARIKDPENDAFTARAN_AWAL.Columns(iLoop).DisplayFormat.FormatString = "{0:n2}"
                grvCARIKDPENDAFTARAN_AWAL.Columns(iLoop).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
            ElseIf grvCARIKDPENDAFTARAN_AWAL.Columns(iLoop).ColumnType.Name = "DateTime" Then
                grvCARIKDPENDAFTARAN_AWAL.Columns(iLoop).DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
                grvCARIKDPENDAFTARAN_AWAL.Columns(iLoop).DisplayFormat.FormatString = "{0:dd/MM/yyyy HH:mm:ss}"
            End If
        Next
    End Sub
    Private Sub fn_LoadKunjunganTransakasi(ByVal Paramater As String)
        Dim oPendafataran_KunjunganPoli As New Admission.clsPendaftaran_KunjunganPoli
        Dim dsRJ = oPendafataran_KunjunganPoli.GetData(Paramater)
        If dsRJ IsNot Nothing Then
            txtKDPENDAFTARAN.Text = dsRJ.KDPENDAFTARAN
            txtKDPENDAFTARAN_AWAL.Text = ""
            grdKDPENJAMIN.Text = dsRJ.KDPENJAMIN
        Else
            Dim oPendafataran_KunjunganRuangan As New Admission.clsPendaftaran_KunjunganRuangan
            Dim dsRI = oPendafataran_KunjunganRuangan.GetData(Paramater)
            If dsRI IsNot Nothing Then
                txtKDPENDAFTARAN.Text = dsRI.KDPENDAFTARAN
                txtKDPENDAFTARAN_AWAL.Text = dsRI.S_PENDAFTARAN_H.KDPENDAFTARAN_AWAL
                grdKDPENJAMIN.Text = dsRI.KDPENJAMIN
            Else
                fn_EmptyMe()
                Exit Sub
            End If
        End If

        Dim oBilling As New Transaksi.clsBilling

        Dim ds = oBilling.GetDataBykdregpenjamin(txtKDPENDAFTARAN.Text, grdKDPENJAMIN.EditValue)

        For Each xloop In ds
            grvDetail.Focus()
            grvDetail.AddNewRow()
            grvDetail.SetFocusedRowCellValue(colNOINVOICE, xloop.KDBILLING)
            'grvDetail.SetFocusedRowCellValue(colAMOUNTPAYMENT, xloop.GRANDTOTAL)
            grvDetail.SetFocusedRowCellValue(colAMOUNTPAYMENT, Math.Ceiling(xloop.GRANDTOTAL / 100) * 100)
            grvDetail.SetFocusedRowCellValue(colREMARKS, "-")
            grvDetail.UpdateCurrentRow()
        Next

        Dim dsAwal = oBilling.GetDataBykdregpenjamin(txtKDPENDAFTARAN_AWAL.Text, grdKDPENJAMIN.EditValue)

        For Each xloop In dsAwal
            grvDetail.Focus()
            grvDetail.AddNewRow()
            grvDetail.SetFocusedRowCellValue(colNOINVOICE, xloop.KDBILLING)
            'grvDetail.SetFocusedRowCellValue(colAMOUNTPAYMENT, xloop.GRANDTOTAL)
            grvDetail.SetFocusedRowCellValue(colAMOUNTPAYMENT, Math.Ceiling(xloop.GRANDTOTAL / 100) * 100)
            grvDetail.SetFocusedRowCellValue(colREMARKS, "-")
            grvDetail.UpdateCurrentRow()
        Next
    End Sub
    Private Sub grdCARIKDREGAWAL_KeyPress(sender As Object, e As KeyPressEventArgs) Handles grdCARIKDREGAWAL.KeyPress
        If Asc(e.KeyChar) = 13 Then
            If cboFilter.SelectedIndex = 0 Then
                fn_LoadKunjunganTransakasi(grdCARIKDREGAWAL.EditValue)
            ElseIf cboFilter.SelectedIndex = 1 Then
                Dim oTanpaResep As New Transaksi.clsBillingTanpaResep
                Dim dsTanpaResep = oTanpaResep.GetData(grdCARIKDREGAWAL.EditValue)
                If dsTanpaResep IsNot Nothing Then
                    txtKDPENDAFTARAN.Text = dsTanpaResep.KDBILLING_TANPARESEP
                    txtKDPENDAFTARAN_AWAL.Text = ""
                    grdKDPENJAMIN.Text = dsTanpaResep.KDPENJAMIN

                    Dim ds = oTanpaResep.GetDataByKodeDeatil(txtKDPENDAFTARAN.Text)

                    For Each xloop In ds
                        grvDetail.Focus()
                        grvDetail.AddNewRow()
                        grvDetail.SetFocusedRowCellValue(colNOINVOICE, xloop.KDBILLING_TANPARESEP)
                        'grvDetail.SetFocusedRowCellValue(colAMOUNTPAYMENT, xloop.GRANDTOTAL)
                        grvDetail.SetFocusedRowCellValue(colAMOUNTPAYMENT, Math.Ceiling(xloop.GRANDTOTAL / 100) * 100)
                        grvDetail.SetFocusedRowCellValue(colREMARKS, "-")
                        grvDetail.UpdateCurrentRow()
                    Next
                Else
                    fn_EmptyMe()
                    Exit Sub
                End If
            Else
                Dim oBillingUnit As New Transaksi.clsBillingUnit
                Dim dsBilingUnit = oBillingUnit.GetData(grdCARIKDREGAWAL.EditValue)
                If dsBilingUnit IsNot Nothing Then
                    txtKDPENDAFTARAN.Text = dsBilingUnit.KDBILLING_UNIT
                    txtKDPENDAFTARAN_AWAL.Text = ""
                    grdKDPENJAMIN.Text = dsBilingUnit.KDPENJAMIN

                    Dim ds = oBillingUnit.GetDataByKodeDeatil(txtKDPENDAFTARAN.Text)

                    For Each xloop In ds
                        grvDetail.Focus()
                        grvDetail.AddNewRow()
                        grvDetail.SetFocusedRowCellValue(colNOINVOICE, xloop.KDBILLING_UNIT)
                        'grvDetail.SetFocusedRowCellValue(colAMOUNTPAYMENT, xloop.GRANDTOTAL)
                        grvDetail.SetFocusedRowCellValue(colAMOUNTPAYMENT, Math.Ceiling(xloop.GRANDTOTAL / 100) * 100)
                        grvDetail.SetFocusedRowCellValue(colREMARKS, "-")
                        grvDetail.UpdateCurrentRow()
                    Next
                Else
                    fn_EmptyMe()
                    Exit Sub
                End If
            End If
        End If
    End Sub
    Private Sub txtDISCOUNT_PERSEN_EditValueChanged(sender As Object, e As EventArgs) Handles txtDISCOUNT_PERSEN.EditValueChanged
        If isLoad = True Then
            txtDISCOUNT.Text = CDec(txtSUBTOTAL.Text) * (CDec(txtDISCOUNT_PERSEN.Text) / 100)
        End If
    End Sub
    Private Sub txtCHARGE_PERSEN_EditValueChanged(sender As Object, e As EventArgs) Handles txtCHARGE_PERSEN.EditValueChanged
        If isLoad = True Then
            Dim sCharge As Decimal = 0

            sCharge = (CDec(txtSUBTOTAL.Text) - CDec(txtDISCOUNT.Text)) * (CDec(txtCHARGE_PERSEN.Text) / 100)

            txtCHARGE.Text = sCharge
        End If
    End Sub
    Private Sub grdKDUNITKASIR_EditValueChanged(sender As Object, e As EventArgs) Handles grdKDUNITKASIR.EditValueChanged
        If isLoad = True Then
            sKDUNITKASIR = grdKDUNITKASIR.EditValue
        End If
    End Sub
    Private Sub grvDetail_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles grvDetail.DoubleClick
        If grvDetail.GetFocusedRowCellValue("NOINVOICE") Is Nothing Then
            Exit Sub
        End If

        Dim oBilling As New Transaksi.clsBilling

        Dim dsBilling = oBilling.GetData(grvDetail.GetFocusedRowCellValue("NOINVOICE"))
        If dsBilling IsNot Nothing Then
            If dsBilling.CATEGORY = 4 Then
                Dim frmBillingFarmasi As New frmBillingFarmasi
                Try
                    frmBillingFarmasi.LoadMe(FORM_MODE.FORM_MODE_VIEW, dsBilling.CATEGORY, False, dsBilling.KDBILLING)
                    frmBillingFarmasi.ShowDialog(Me)
                Catch oErr As Exception
                    MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            Else
                Dim frmBilling As New frmBilling
                Try
                    frmBilling.LoadMe(FORM_MODE.FORM_MODE_VIEW, dsBilling.CATEGORY, dsBilling.KDBILLING)
                    frmBilling.ShowDialog(Me)
                Catch oErr As Exception
                    MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            End If
        End If

    End Sub
#End Region
End Class