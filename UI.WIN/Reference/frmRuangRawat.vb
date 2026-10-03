Imports DataAccess
Imports UI.WIN.MAIN.My.Resources
Imports System.Linq
Imports Newtonsoft.Json.Linq

Public Class frmRuangRawat
#Region "Declaration"
    Private oFormMode As FORM_MODE = FORM_MODE.FORM_MODE_VIEW
    Private sNoId As String
    Private isLoad As Boolean = False
    Private oRuangRawat As New Reference.clsRuangRawat
#End Region
#Region "Function"
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
            Me.Text = RuangRawat.TITLE

            lCODE.Text = RuangRawat.KDRuangRawat & " *"
            lKDRUANGRAWAT_BPJS.Text = RuangRawat.KDRUANGRAWAT_BPJS
            lNAME_DISPLAY.Text = RuangRawat.NAME_DISPLAY & " *"
            chkISACTIVE.Text = RuangRawat.ISACTIVE

            lPHONE.Text = RuangRawat.PHONE
            lFAX.Text = RuangRawat.FAX
            lMOBILE.Text = RuangRawat.MOBILE
            lOTHER.Text = RuangRawat.OTHER
            lEMAIL.Text = RuangRawat.EMAIL
            lWEBSITE.Text = RuangRawat.WEBSITE

            lBILL_STREET.Text = RuangRawat.BILL_STREET
            lBILL_CITY.Text = RuangRawat.BILL_CITY
            lBILL_STATE.Text = RuangRawat.BILL_STATE
            lBILL_ZIP.Text = RuangRawat.BILL_ZIP
            lBILL_COUNTRY.Text = RuangRawat.BILL_COUNTRY

            tab1.Text = RuangRawat.TAB_CONTACT
            tab2.Text = RuangRawat.TAB_BILL
            tab3.Text = RuangRawat.TAB_OTHER

            btnSaveNew.Caption = Caption.FormSaveNew
            btnSaveClose.Caption = Caption.FormSaveClose
            btnClose.Caption = Caption.FormClose
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub Form_Closing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        sCode = txtCODE.Text.Trim.ToUpper
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
        btnRuanganBaru.Enabled = Not Status
        btnHapusRuangan.Enabled = Not Status

        If oFormMode = FORM_MODE.FORM_MODE_ADD Then
            txtCODE.Properties.ReadOnly = False
        Else
            txtCODE.Properties.ReadOnly = True
        End If
        txtKDRUANGRAWAT_BPJS.Properties.ReadOnly = Status
        txtKDKELAS_APLICARE.Properties.ReadOnly = Status
        txtKDRUANG_APLICARE.Properties.ReadOnly = Status
        txtKDRUANG_SIRS.Properties.ReadOnly = Status
        txtNAME_DISPLAY.Properties.ReadOnly = Status
        chkISACTIVE.Properties.ReadOnly = Status
        txtPHONE.Properties.ReadOnly = Status
        txtFAX.Properties.ReadOnly = Status
        txtMOBILE.Properties.ReadOnly = Status
        txtEMAIL.Properties.ReadOnly = Status
        txtOTHER.Properties.ReadOnly = Status
        txtWEBSITE.Properties.ReadOnly = Status
        txtBILL_STREET.Properties.ReadOnly = Status
        txtBILL_CITY.Properties.ReadOnly = Status
        txtBILL_STATE.Properties.ReadOnly = Status
        txtBILL_ZIP.Properties.ReadOnly = Status
        txtBILL_COUNTRY.Properties.ReadOnly = Status
        txtMEMO.Properties.ReadOnly = Status
    End Sub
    Private Sub fn_EmptyMe()
        txtCODE.Text = "<---AUTO--->"
        txtKDRUANGRAWAT_BPJS.ResetText()
        txtKDKELAS_APLICARE.ResetText()
        txtKDRUANG_APLICARE.ResetText()
        txtKDRUANG_SIRS.ResetText()
        txtNAME_DISPLAY.ResetText()
        chkISACTIVE.Checked = True
        txtPHONE.ResetText()
        txtFAX.ResetText()
        txtMOBILE.ResetText()
        txtEMAIL.ResetText()
        txtOTHER.ResetText()
        txtWEBSITE.ResetText()
        txtBILL_STREET.ResetText()
        txtBILL_CITY.ResetText()
        txtBILL_STATE.ResetText()
        txtBILL_ZIP.ResetText()
        txtBILL_COUNTRY.ResetText()
        txtMEMO.ResetText()
        txtKDRUANGRAWAT_BPJS.ResetText()
    End Sub
    Private Sub fn_LoadData()
        Try
            Dim ds = oRuangRawat.GetData(sNoId)

            With ds
                txtCODE.Text = .KDRUANGRAWAT
                txtKDRUANGRAWAT_BPJS.Text = .KDRUANGRAWAT_BPJS
                txtKDKELAS_APLICARE.Text = .KDKELAS_APLICARE
                txtKDRUANG_APLICARE.Text = .KDRUANG_APLICARE
                txtKDRUANG_SIRS.Text = .KDRUANG_SIRS
                txtNAME_DISPLAY.Text = .NAME_DISPLAY
                chkISACTIVE.Checked = .ISACTIVE
                txtPHONE.Text = .PHONE
                txtFAX.Text = .FAX
                txtMOBILE.Text = .MOBILE
                txtEMAIL.Text = .EMAIL
                txtOTHER.Text = .OTHER
                txtWEBSITE.Text = .WEBSITE
                txtBILL_STREET.Text = .BILL_STREET
                txtBILL_CITY.Text = .BILL_CITY
                txtBILL_STATE.Text = .BILL_STATE
                txtBILL_ZIP.Text = .BILL_ZIP
                txtBILL_COUNTRY.Text = .BILL_COUNTRY
                txtMEMO.Text = .MEMO

            End With

            BindingSource.DataSource = oRuangRawat.GetDataDetail.Where(Function(x) x.KDRUANGRAWAT = sNoId).OrderBy(Function(x) x.SEQ).ToList()
            grdDetail.DataSource = BindingSource

            tabControl.SelectedTabPage = tab4
            tabControl.SelectedTabPage = tab3
            tabControl.SelectedTabPage = tab2
            tabControl.SelectedTabPage = tab1

        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Function fn_Validate() As Boolean
        Try
            fn_Validate = True
            If txtNAME_DISPLAY.Text = String.Empty Then
                txtNAME_DISPLAY.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                txtNAME_DISPLAY.ErrorText = Statement.ErrorRequired

                txtNAME_DISPLAY.Focus()
                fn_Validate = False
                Exit Function
            End If
            'If txtKDRUANGRAWAT_BPJS.Text = String.Empty Then
            '    txtKDRUANGRAWAT_BPJS.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
            '    txtKDRUANGRAWAT_BPJS.ErrorText = Statement.ErrorRequired

            '    txtKDRUANGRAWAT_BPJS.Focus()
            '    fn_Validate = False
            '    Exit Function
            'End If

            If oFormMode = FORM_MODE.FORM_MODE_ADD Then
                If oRuangRawat.IsExist(txtNAME_DISPLAY.Text.ToUpper.Trim) = True Then
                    txtNAME_DISPLAY.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                    txtNAME_DISPLAY.ErrorText = Statement.ErrorRegistered

                    txtNAME_DISPLAY.Focus()
                    fn_Validate = False
                    Exit Function
                End If
            Else
                If txtNAME_DISPLAY.Text.Trim <> oRuangRawat.GetData(sNoId).NAME_DISPLAY Then
                    If oRuangRawat.IsExist(txtNAME_DISPLAY.Text.ToUpper.Trim) = True Then
                        txtNAME_DISPLAY.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                        txtNAME_DISPLAY.ErrorText = Statement.ErrorRegistered

                        txtNAME_DISPLAY.Focus()
                        fn_Validate = False
                        Exit Function
                    End If
                End If
            End If

            grvDetail.UpdateCurrentRow()

            If grvDetail.RowCount < 2 Then
                MsgBox(Statement.ErrorDetail, MsgBoxStyle.Exclamation, Me.Text)
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
            Dim ds = oRuangRawat.GetStructureHeader
            With ds
                Try
                    .DATECREATED = oRuangRawat.GetData(sNoId).DATECREATED
                Catch oErr As Exception
                    .DATECREATED = Now
                End Try
                .DATEUPDATED = Now
                .KDRUANGRAWAT = IIf(txtCODE.Text = "<---AUTO--->", sNoId, txtCODE.Text)
                .KDRUANGRAWAT_BPJS = txtKDRUANGRAWAT_BPJS.Text
                .KDRUANGRAWAT_BPJS = txtKDRUANGRAWAT_BPJS.Text
                .KDKELAS_APLICARE = txtKDKELAS_APLICARE.Text
                .KDRUANG_APLICARE = txtKDRUANG_APLICARE.Text
                .KDRUANG_SIRS = txtKDRUANG_SIRS.Text
                .NAME_DISPLAY = txtNAME_DISPLAY.Text.Trim
                .PHONE = txtPHONE.Text.Trim.ToUpper
                .FAX = txtFAX.Text.Trim.ToUpper
                .MOBILE = txtMOBILE.Text.Trim.ToUpper
                .EMAIL = txtEMAIL.Text.Trim.ToUpper
                .OTHER = txtOTHER.Text.Trim.ToUpper
                .WEBSITE = txtWEBSITE.Text.Trim.ToUpper
                .BILL_STREET = txtBILL_STREET.Text.Trim.ToUpper
                .BILL_CITY = txtBILL_CITY.Text.Trim.ToUpper
                .BILL_STATE = txtBILL_STATE.Text.Trim.ToUpper
                .BILL_ZIP = txtBILL_ZIP.Text.Trim.ToUpper
                .BILL_COUNTRY = txtBILL_COUNTRY.Text.Trim.ToUpper
                .MEMO = txtMEMO.Text.Trim.ToUpper
                .ISACTIVE = chkISACTIVE.Checked
            End With

            ' ***** DETIL *****
            Dim arrDetail = oRuangRawat.GetStructureDetailList
            For i As Integer = 0 To grvDetail.RowCount - 2
                Dim dsDetail = oRuangRawat.GetStructureDetail
                With dsDetail
                    .SEQ = i
                    .KDRUANGRAWAT = ds.KDRUANGRAWAT
                    .MEMO = grvDetail.GetRowCellValue(i, colMEMO)
                    .ISTERISI = grvDetail.GetRowCellValue(i, colISTERISI)
                    .ISACTIVE = grvDetail.GetRowCellValue(i, colISACTIVE)
                    .KATEGORI = grvDetail.GetRowCellValue(i, colKATEGORI)
                End With
                arrDetail.Add(dsDetail)
            Next

            If oFormMode = FORM_MODE.FORM_MODE_ADD Then
                Try
                    fn_Save = oRuangRawat.InsertData(ds, arrDetail)
                Catch oErr As Exception
                    MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            ElseIf oFormMode = FORM_MODE.FORM_MODE_EDIT Then
                Try
                    fn_Save = oRuangRawat.UpdateData(ds, arrDetail)
                Catch oErr As Exception
                    MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            End If
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            fn_Save = False
        End Try
    End Function
    Private Sub DeleteToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DeleteToolStripMenuItem.Click
        If oFormMode = FORM_MODE.FORM_MODE_VIEW Then Exit Sub
        grvDetail.DeleteSelectedRows()
    End Sub
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
#End Region
#Region "Lookup / Event"
    Private Sub txtCODE_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtCODE.KeyPress
        If Asc(e.KeyChar) = 13 Then
            Try
                Dim oSetKoneksi As New Brigging.clsSetKoneksi

                Dim dsDataSetKoneksi = oSetKoneksi.GetData().FirstOrDefault(Function(x) x.ISACTIVE = True And x.NAME_DISPLAY = "VCLAIM")
                Dim uTime As Integer = 0

                If dsDataSetKoneksi IsNot Nothing Then
                    uTime = (DateTime.UtcNow - New DateTime(1970, 1, 1, 0, 0, 0)).TotalSeconds
                    Dim dsSetKoneksi = oSetKoneksi.GetDataVClaimReferensiRuangRawat(dsDataSetKoneksi.ALAMATWEB, dsDataSetKoneksi.CONSID, dsDataSetKoneksi.SECREATKEY, dsDataSetKoneksi.USER_KEY, uTime)
                    Dim allData = JObject.Parse(dsSetKoneksi)
                    tabControl.SelectedTabPage = tab3
                    txtMEMO.Text = oSetKoneksi.Decrypt(allData("response"), dsDataSetKoneksi.CONSID & dsDataSetKoneksi.SECREATKEY & uTime)
                Else
                    MsgBox("Koneksi Tidak ditemukan", MsgBoxStyle.Exclamation, Me.Text)
                End If
            Catch oErr As Exception
                MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            End Try
        End If
    End Sub
    Private Sub txtKDKELAS_APLICARE_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtKDKELAS_APLICARE.KeyPress
        If Asc(e.KeyChar) = 13 Then
            Try
                Dim oSetKoneksi As New Brigging.clsSetKoneksi
                Dim dsDataSetKoneksi = oSetKoneksi.GetData().FirstOrDefault(Function(x) x.ISACTIVE = True And x.NAME_DISPLAY = "APLICARE")
                Dim uTime As Integer = 0

                If dsDataSetKoneksi IsNot Nothing Then
                    uTime = (DateTime.UtcNow - New DateTime(1970, 1, 1, 0, 0, 0)).TotalSeconds
                    Dim dsSetKoneksi = oSetKoneksi.GetDataAplicareReferensiKelasRawat(dsDataSetKoneksi.ALAMATWEB, dsDataSetKoneksi.CONSID, dsDataSetKoneksi.SECREATKEY, uTime)

                    If dsSetKoneksi <> "" Then
                        Dim allData = JObject.Parse(dsSetKoneksi)

                        Dim CodeResponse As String = String.Empty
                        Dim messageResponse As String = String.Empty

                        CodeResponse = allData("metadata")("code").ToString

                        If CodeResponse = "1" Then
                            Dim table As DataTable

                            table = New DataTable("I_REPOT_REKAP")
                            table.Columns.Add("kodekelas")
                            table.Columns.Add("namakelas")

                            For Each item In allData("response")("list")
                                table.Rows.Add(New String() {item("kodekelas"), item("namakelas")})
                            Next

                            grdCARI.Properties.DataSource = table
                            grdCARI.Properties.ValueMember = "kodekelas"
                            grdCARI.Properties.DisplayMember = "namakelas"
                        Else
                            MsgBox(allData("metaData")("message").ToString, MsgBoxStyle.Exclamation, Me.Text)
                        End If
                    Else
                        MsgBox("Insert SEP Data Gagal", MsgBoxStyle.Exclamation, Me.Text)
                    End If
                Else
                    MsgBox("Cari SEP Data Gagal/Kosong", MsgBoxStyle.Exclamation, Me.Text)
                End If
            Catch oErr As Exception
                MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            End Try
        End If
    End Sub
    Private Sub grdCARI_KeyPress(sender As Object, e As KeyPressEventArgs) Handles grdCARI.KeyPress
        If Asc(e.KeyChar) = 13 Then
            txtKDKELAS_APLICARE.Text = grdCARI.EditValue
        End If
    End Sub
#End Region
End Class