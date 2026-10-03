Imports DataAccess
Imports UI.WIN.MAIN.My.Resources
Imports System.Linq
Imports Newtonsoft.Json.Linq

Public Class frmDoctor
#Region "Declaration"
    Private oFormMode As FORM_MODE = FORM_MODE.FORM_MODE_VIEW
    Private sNoId As String
    Private isLoad As Boolean = False
    Private oDoctor As New Reference.clsDoctor
#End Region
#Region "Function"
    Public Sub LoadMe(ByVal FormMode As Integer, Optional ByVal NoId As String = "")
        deDATESIP.DateTime = Now
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
            Me.Text = Doctor.TITLE

            lCODE.Text = Doctor.KDDOCTOR
            lNAME_DISPLAY.Text = Doctor.NAME_DISPLAY & " *"
            chkISACTIVE.Text = Doctor.ISACTIVE
            lSPESIALISTIK.Text = Doctor.KDSPESIALISTIK
            lPHONE.Text = Doctor.PHONE
            lFAX.Text = Doctor.FAX
            lMOBILE.Text = Doctor.MOBILE
            lOTHER.Text = Doctor.OTHER
            lEMAIL.Text = Doctor.EMAIL
            lWEBSITE.Text = Doctor.WEBSITE
            lBILL_STREET.Text = Doctor.BILL_STREET
            lBILL_CITY.Text = Doctor.BILL_CITY
            lBILL_STATE.Text = Doctor.BILL_STATE
            lBILL_ZIP.Text = Doctor.BILL_ZIP
            lBILL_COUNTRY.Text = Doctor.BILL_COUNTRY
            lSUBSPESIALIS.Text = Doctor.SUBSPESIALIS
            lSIP.Text = Doctor.SIP
            lDATESIP.Text = Doctor.DATESIP
            lVCLAIM_KDDPJP.Text = Doctor.VCLAIM_KDDPJP
            lVCLAIM_KDDOCTOR.Text = Doctor.VCLAIM_KDDOCTOR
            lKDDEPARTMENT.Text = Doctor.KDDEPARTMENT
            lJENISPELAYANAN.Text = Doctor.JENISPELAYANAN
            tab1.Text = Doctor.TAB_CONTACT
            tab2.Text = Doctor.TAB_BILL
            tab4.Text = Doctor.TAB_OTHER

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
        fn_LoadKDUSER()
        fn_LoadKDSPESIALISTIK()
        fn_LoadKDDEPARTMENT()

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

        If oFormMode = FORM_MODE.FORM_MODE_ADD Then
            txtCODE.Properties.ReadOnly = False
        Else
            txtCODE.Properties.ReadOnly = True
        End If
        rbJENISPELAYANAN.Properties.ReadOnly = Status
        txtVCLAIM_KDDPJP.Properties.ReadOnly = True
        txtVCLAIM_KDDOCTOR.Properties.ReadOnly = Status
        grdKDDEPARTMENT.Properties.ReadOnly = Status
        grdKDSPESIALISTIK.Properties.ReadOnly = Status
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
        rbCATEGORY.Properties.ReadOnly = Status
        txtSIP.Properties.ReadOnly = Status
        deDATESIP.Properties.ReadOnly = Status
        grvDetail.OptionsBehavior.ReadOnly = Status

        txtFINGGER.Properties.ReadOnly = Status
        txtNOKARTUPEGAWAI.Properties.ReadOnly = Status
        txtKDCUSTOMER.Properties.ReadOnly = Status
        grdKDUSER.Properties.ReadOnly = Status
    End Sub
    Private Sub fn_EmptyMe()
        txtCODE.Text = "<---AUTO--->"
        txtNAME_DISPLAY.ResetText()
        grdKDSPESIALISTIK.ResetText()
        grdKDDEPARTMENT.ResetText()
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
        txtSIP.ResetText()
        txtVCLAIM_KDDPJP.ResetText()
        txtVCLAIM_KDDOCTOR.ResetText()
        Try
            grdKDSPESIALISTIK.Text = oDoctor.Spesialistik_Default()
        Catch ex As Exception

        End Try
        txtFINGGER.ResetText()
        txtNOKARTUPEGAWAI.ResetText()
        txtKDCUSTOMER.ResetText()
        grdKDUSER.ResetText()
    End Sub
    Private Sub fn_LoadData()
        Try
            Dim ds = oDoctor.GetData(sNoId)

            With ds
                txtCODE.Text = .KDDOCTOR
                grdKDSPESIALISTIK.Text = .KDSPESIALISTIK
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
                rbCATEGORY.SelectedIndex = .SUBSPESIALIS
                txtSIP.Text = .SIP
                deDATESIP.DateTime = .DATESIP
                txtVCLAIM_KDDPJP.Text = .VCLAIM_KDDPJP
                txtVCLAIM_KDDOCTOR.Text = .VCLAIM_KDDOCTOR
                grdKDDEPARTMENT.Text = .KDDEPARTMENT
                rbJENISPELAYANAN.Text = .VCLAIM_JENISPELAYANAN
                txtFINGGER.Text = .FINGGER
                txtNOKARTUPEGAWAI.Text = .NOKARTUPEGAWAI
                txtKDCUSTOMER.Text = .KDCUSTOMER
                grdKDUSER.Text = .KDUSER

                'IMAGE
                Try
                    'Dim img = (From x In oDoctor.GetData
                    '           Where x.KDDOCTOR = sNoId
                    '           Select x.ATTACHMENT).Single

                    picGAMBAR.Image = ByteArrayToImage(.ATTACHMENT.ToArray())
                Catch oErr As Exception
                    MsgBox("Load Image : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try

                BindingSource.DataSource = oDoctor.GetDataDetail(sNoId).OrderBy(Function(x) x.SEQ).ToList()
                grdDetail.DataSource = BindingSource

                tabControl.SelectedTabPage = tab4
                tabControl.SelectedTabPage = tab2
                tabControl.SelectedTabPage = tab1
            End With
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Function fn_Validate() As Boolean
        Try
            fn_Validate = True
            If grdKDSPESIALISTIK.Text = String.Empty Then
                grdKDSPESIALISTIK.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                grdKDSPESIALISTIK.ErrorText = Statement.ErrorRequired

                grdKDSPESIALISTIK.Focus()
                fn_Validate = False
                Exit Function
            End If
            If txtNAME_DISPLAY.Text = String.Empty Then
                txtNAME_DISPLAY.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                txtNAME_DISPLAY.ErrorText = Statement.ErrorRequired

                txtNAME_DISPLAY.Focus()
                fn_Validate = False
                Exit Function
            End If
            'If oFormMode = FORM_MODE.FORM_MODE_ADD Then
            '    If oDoctor.IsExist(txtNAME_DISPLAY.Text.ToUpper.Trim) = True Then
            '        txtNAME_DISPLAY.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
            '        txtNAME_DISPLAY.ErrorText = Statement.ErrorRegistered

            '        txtNAME_DISPLAY.Focus()
            '        fn_Validate = False
            '        Exit Function
            '    End If
            'Else
            '    If txtNAME_DISPLAY.Text.ToUpper.Trim <> oDoctor.GetData(sNoId).NAME_DISPLAY Then
            '        If oDoctor.IsExist(txtNAME_DISPLAY.Text.ToUpper.Trim) = True Then
            '            txtNAME_DISPLAY.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
            '            txtNAME_DISPLAY.ErrorText = Statement.ErrorRegistered

            '            txtNAME_DISPLAY.Focus()
            '            fn_Validate = False
            '            Exit Function
            '        End If
            '    End If
            'End If
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Function
    Private Function fn_Save() As Boolean
        Try
            ' ***** HEADER *****
            Dim ds = oDoctor.GetStructureHeader
            With ds
                Try
                    .DATECREATED = oDoctor.GetData(sNoId).DATECREATED
                Catch oErr As Exception
                    .DATECREATED = Now
                End Try
                .DATEUPDATED = Now
                .KDDOCTOR = IIf(txtCODE.Text = "<---AUTO--->", sNoId, txtCODE.Text)
                .NAME_DISPLAY = txtNAME_DISPLAY.Text.Trim
                .KDSPESIALISTIK = grdKDSPESIALISTIK.EditValue.ToString.Trim
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
                .SUBSPESIALIS = rbCATEGORY.SelectedIndex
                .SIP = txtSIP.Text.Trim.ToUpper
                .DATESIP = deDATESIP.DateTime
                .VCLAIM_KDDPJP = txtVCLAIM_KDDPJP.Text
                .VCLAIM_KDDOCTOR = txtVCLAIM_KDDOCTOR.Text
                .KDDEPARTMENT = grdKDDEPARTMENT.EditValue
                .VCLAIM_JENISPELAYANAN = rbJENISPELAYANAN.SelectedIndex
                .FINGGER = txtFINGGER.Text
                .NOKARTUPEGAWAI = txtNOKARTUPEGAWAI.Text
                .KDCUSTOMER = txtKDCUSTOMER.Text
                .KDUSER = grdKDUSER.EditValue

                Try
                    Dim ms As New IO.MemoryStream()
                    picGAMBAR.Image.Save(ms, picGAMBAR.Image.RawFormat)

                    Dim data As Byte() = ms.GetBuffer()

                    .ATTACHMENT = data
                    .ATTACHMENT_TTD = data

                    '.ATTACHMENT = oDoctor.GetData(sNoId).ATTACHMENT
                    '.ATTACHMENT_TTD = oDoctor.GetData(sNoId).ATTACHMENT_TTD
                Catch oErr As Exception
                    Try
                        .ATTACHMENT = oDoctor.GetData(sNoId).ATTACHMENT
                        .ATTACHMENT_TTD = oDoctor.GetData(sNoId).ATTACHMENT_TTD
                    Catch ex As Exception

                    End Try
                End Try
            End With

            ' ***** DETIL *****
            Dim arrDetail = oDoctor.GetStructureDetailList
            For i As Integer = 0 To grvDetail.RowCount - 2
                Dim dsDetail = oDoctor.GetStructureDetail
                With dsDetail
                    .KDDOCTOR = ds.KDDOCTOR
                    .SEQ = i
                    .HARI = grvDetail.GetRowCellValue(i, colHARI)
                    .KAPASITASPASIEN_TOTAL = grvDetail.GetRowCellValue(i, colKAPASITASPASIEN_TOTAL)
                    .KAPASITASPASIEN_JKN = grvDetail.GetRowCellValue(i, colKAPASITASPASIEN_JKN)
                    .KAPASITASPASIEN_NONJKN = grvDetail.GetRowCellValue(i, colKAPASITASPASIEN_NONJKN)
                    .LIBUR = False
                    .BUKA = grvDetail.GetRowCellValue(i, colBUKA)
                    .TUTUP = grvDetail.GetRowCellValue(i, colTUTUP)
                    .DESCRIPTION = IIf(String.IsNullOrEmpty(grvDetail.GetRowCellValue(i, colDESCRIPTION)), "-", grvDetail.GetRowCellValue(i, colDESCRIPTION))
                End With
                arrDetail.Add(dsDetail)
            Next

            If oFormMode = FORM_MODE.FORM_MODE_ADD Then
                Try
                    fn_Save = oDoctor.InsertData(ds, arrDetail)
                Catch oErr As Exception
                    MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            ElseIf oFormMode = FORM_MODE.FORM_MODE_EDIT Then
                Try
                    fn_Save = oDoctor.UpdateData(ds, arrDetail)
                Catch oErr As Exception
                    MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            End If
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            fn_Save = False
        End Try
    End Function
    Private Function ByteArrayToImage(ByVal byteArrayIn() As Byte) As Image
        Using ms As New System.IO.MemoryStream(byteArrayIn)
            Dim returnImage = Image.FromStream(ms)
            Return returnImage
        End Using
    End Function
    Private Function ImageToByteArray(ByVal ImageIn As System.Drawing.Image) As Byte()

        Using ms As New System.IO.MemoryStream
            ImageIn.Save(ms, System.Drawing.Imaging.ImageFormat.Gif)
            Return ms.ToArray
        End Using
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
    Private Sub picGAMBAR_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles picGAMBAR.Click
        If OpenFileDialog.ShowDialog = Windows.Forms.DialogResult.OK Then
            Try
                'Dim img As System.Drawing.Image = System.Drawing.Image.FromFile(OpenFileDialog.FileName)
                'Dim bytes As Byte()
                'Using ms As New IO.MemoryStream()
                '    img.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg)
                '    bytes = ms.ToArray()
                'End Using

                'picGAMBAR.Image = ByteArrayToImage(bytes)

                Dim bmp As New Bitmap(OpenFileDialog.FileName)
                If Not IsNothing(picGAMBAR.Image) Then picGAMBAR.Image.Dispose() 'Optional if you want to destroy the previously loaded image
                picGAMBAR.Image = bmp
            Catch oErr As Exception
                MsgBox("Load Image : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            End Try
        End If
    End Sub
    Private Sub fn_LoadKDUSER()
        Dim oUser As New Setting.clsUser
        Try
            grdKDUSER.Properties.DataSource = oUser.GetData.Where(Function(x) x.ISACTIVE = True).ToList()
            grdKDUSER.Properties.ValueMember = "KDUSER"
            grdKDUSER.Properties.DisplayMember = "KDUSER"
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadKDSPESIALISTIK()
        Dim oSpesialistik As New Reference.clsSpesialistik
        Try
            grdKDSPESIALISTIK.Properties.DataSource = oSpesialistik.GetData.Where(Function(x) x.ISACTIVE = True).ToList()
            grdKDSPESIALISTIK.Properties.ValueMember = "KDSPESIALISTIK"
            grdKDSPESIALISTIK.Properties.DisplayMember = "MEMO"
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadKDDEPARTMENT()
        Dim oDepartment As New Reference.clsDepartment
        Try
            grdKDDEPARTMENT.Properties.DataSource = oDepartment.GetData.Where(Function(x) x.ISACTIVE = True).ToList()
            grdKDDEPARTMENT.Properties.ValueMember = "KDDEPARTMENT"
            grdKDDEPARTMENT.Properties.DisplayMember = "NAME_DISPLAY"
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub grdKDDEPARTMENT_Click_1(sender As Object, e As KeyPressEventArgs) Handles grdKDDEPARTMENT.KeyPress
        If Asc(e.KeyChar) = 13 Then
            Try
                If grdKDDEPARTMENT.Text = String.Empty Then
                    Exit Sub
                End If

                Dim oDepartment As New Reference.clsDepartment
                Dim oSetKoneksi As New Brigging.clsSetKoneksi
                Dim oSetKoneksiNew As New Setting.clsBPJSKoneksi
                Dim dsDataSetKoneksi = oSetKoneksiNew.GetData("VCLAIM")
                Dim uTime As Integer = 0

                If dsDataSetKoneksi IsNot Nothing Then
                    uTime = (DateTime.UtcNow - New DateTime(1970, 1, 1, 0, 0, 0)).TotalSeconds
                    Dim dsSetKoneksi = oSetKoneksi.GetDataVClaimReferensiDokterDPJP(dsDataSetKoneksi.ALAMATWEB, dsDataSetKoneksi.CONSID, dsDataSetKoneksi.SECREATKEY, dsDataSetKoneksi.REMARKS, uTime, IIf(rbJENISPELAYANAN.SelectedIndex = 0, 2, 1), Now.ToString("yyyy-MM-dd"), oDepartment.GetData(grdKDDEPARTMENT.EditValue).KDDEPARTMENT_BPJS)

                    If dsSetKoneksi <> "" Then
                        Try
                            Dim allData = JObject.Parse(dsSetKoneksi)

                            Dim table As DataTable

                            table = New DataTable("M_TABEL")
                            table.Columns.Add("kode")
                            table.Columns.Add("nama")

                            Dim dsData = oSetKoneksi.Decrypt(allData("response"), dsDataSetKoneksi.CONSID & dsDataSetKoneksi.SECREATKEY & uTime)
                            Dim ds = JObject.Parse(dsData)

                            For Each item In ds("list")
                                table.Rows.Add(New String() {item("kode"), item("nama")})
                            Next

                            grdCARI.Properties.DataSource = table
                            grdCARI.Properties.ValueMember = "kode"
                            grdCARI.Properties.DisplayMember = "nama"

                            grdCARI.ShowPopup()

                        Catch oErr As Exception
                            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message & vbCrLf & vbCrLf & "Result : " & vbCrLf & dsSetKoneksi, MsgBoxStyle.Exclamation, Me.Text)
                        End Try
                    End If
                Else
                    MsgBox("Koneksi Tidak ditemukan", MsgBoxStyle.Exclamation, Me.Text)
                End If

            Catch oErr As Exception
                MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            End Try
        End If
    End Sub
    Private Sub grdCARI_KeyPress(sender As Object, e As KeyPressEventArgs) Handles grdCARI.KeyPress
        If Asc(e.KeyChar) = 13 Then
            txtVCLAIM_KDDPJP.Text = grdCARI.EditValue
            txtNAME_DISPLAY.Text = grdCARI.Text
        End If
    End Sub
    Private Sub txtCARI_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtVCLAIM_KDDOCTOR.KeyPress
        If Asc(e.KeyChar) = 13 Then
            Try
                Dim oSetKoneksi As New Brigging.clsSetKoneksi
                Dim oSetKoneksiNew As New Setting.clsBPJSKoneksi
                Dim dsDataSetKoneksi = oSetKoneksiNew.GetData("VCLAIM")
                Dim uTime As Integer = 0

                If dsDataSetKoneksi IsNot Nothing Then
                    uTime = (DateTime.UtcNow - New DateTime(1970, 1, 1, 0, 0, 0)).TotalSeconds
                    Dim dsSetKoneksi = oSetKoneksi.GetDataVClaimReferensiDokter(dsDataSetKoneksi.ALAMATWEB, dsDataSetKoneksi.CONSID, dsDataSetKoneksi.SECREATKEY, dsDataSetKoneksi.REMARKS, uTime, txtVCLAIM_KDDOCTOR.Text)

                    If dsSetKoneksi <> "" Then
                        Dim allData = JObject.Parse(dsSetKoneksi)

                        Dim CodeResponse As String = String.Empty
                        Dim messageResponse As String = String.Empty

                        CodeResponse = IIf(IsDBNull(allData.Item("metaData").Item("code")) = True, "", allData.Item("metaData").Item("code"))
                        messageResponse = allData("metaData")("message").ToString

                        If CodeResponse = "200" Then
                            txtMEMO.Text = oSetKoneksi.Decrypt(allData("response"), dsDataSetKoneksi.CONSID & dsDataSetKoneksi.SECREATKEY & uTime)
                        Else
                            txtMEMO.ResetText()
                            MsgBox(CodeResponse & " - " & messageResponse, MsgBoxStyle.Exclamation, Me.Text)
                        End If
                    Else
                        MsgBox("Koneksi Referensi Dokter Kosong", MsgBoxStyle.Exclamation, Me.Text)
                    End If
                Else
                    MsgBox("Koneksi Tidak ditemukan", MsgBoxStyle.Exclamation, Me.Text)
                End If

                txtVCLAIM_KDDOCTOR.ResetText()

            Catch oErr As Exception
                MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            End Try

        End If
    End Sub
#End Region
End Class