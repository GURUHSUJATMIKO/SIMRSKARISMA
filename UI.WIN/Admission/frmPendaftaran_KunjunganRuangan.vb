Imports DataAccess
Imports UI.WIN.MAIN.My.Resources
Imports System.Linq

Public Class frmPendaftaran_KunjunganRuangan
#Region "Declaration"
    Private oFormMode As FORM_MODE = FORM_MODE.FORM_MODE_VIEW
    Private sNoId As String
    Private isLoad As Boolean = False
    Private oPendaftaran_KunjunganRuangan As New Admission.clsPendaftaran_KunjunganRuangan
    Private PopUP As Boolean = False

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
            Me.Text = Pendaftaran_KunjunganRuangan.TITLE

            lKDKUNJUNGAN_POLI.Text = Pendaftaran_KunjunganRuangan.KDKUNJUNGAN_POLI
            lKDPENDAFATRAN.Text = Pendaftaran.KDPENDAFTARAN & " *"
            lDATE_MASUK.Text = Pendaftaran_KunjunganRuangan.DATE_MASUK
            lDATE_KELUAR.Text = Pendaftaran_KunjunganRuangan.DATE_KELUAR
            lKDDEPARTMENT.Text = Department.NAME_DISPLAY
            lPENJAMIN.Text = Penjamin.MEMO
            lDOCTOR.Text = Pendaftaran.KDDOCTOR

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
        fn_LoadKDDEPARTMENT()
        fn_LoadKDPENJAMIN()
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
        btnSaveNew.Enabled = Not Status
        btnSaveClose.Enabled = Not Status

        grdKDPENDAFTARAN.Properties.ReadOnly = Status
        deDATE_MASUK.Properties.ReadOnly = Status
        deDATE_KELUAR.Properties.ReadOnly = Status
        grdKDRUANGRAWAT.Properties.ReadOnly = Status
        grdPENJAMIN.Properties.ReadOnly = Status
        grdDOCTOR.Properties.ReadOnly = Status
        grdTempatTidur.Properties.ReadOnly = Status
    End Sub
    Private Sub fn_EmptyMe()
        txtCODE.Text = "<--- AUTO --->"
        grdKDPENDAFTARAN.ResetText()
        deDATE_MASUK.DateTime = Now
        deDATE_KELUAR.DateTime = Now
        grdKDRUANGRAWAT.ResetText()
        grdPENJAMIN.ResetText()
        grdDOCTOR.ResetText()
        grdTempatTidur.ResetText()
    End Sub
    Private Sub fn_LoadData()
        Try
            Dim ds = oPendaftaran_KunjunganRuangan.GetData(sNoId)

            With ds
                txtCODE.Text = sNoId
                cboCARI.SelectedIndex = 2
                fn_LoadKDPENDAFTARAN(.KDPENDAFTARAN)
                grdKDPENDAFTARAN.Text = .KDPENDAFTARAN
                deDATE_MASUK.DateTime = .DATE_MASUK
                deDATE_KELUAR.DateTime = .DATE_KELUAR
                grdKDRUANGRAWAT.Text = .KDRUANGRAWAT
                grdPENJAMIN.Text = .KDPENJAMIN
                grdDOCTOR.Text = .KDDOCTOR

                Dim oRuangRawat As New Reference.clsRuangRawat
                fn_LoadTempatTidur(sNoId)
                grdTempatTidur.EditValue = oRuangRawat.GetDataDetail(sNoId, .SEQ).SEQ

            End With
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Function fn_Validate() As Boolean
        Try
            fn_Validate = True
            'If txtCATATAN.Text = String.Empty Then
            '    txtCATATAN.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
            '    txtCATATAN.ErrorText = Statement.ErrorRequired

            '    txtCATATAN.Focus()
            '    fn_Validate = False
            '    Exit Function
            'End If

            If grdTempatTidur.Text = String.Empty Then
                grdTempatTidur.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                grdTempatTidur.ErrorText = Statement.ErrorRequired
                MsgBox("Tempat Tidur Masih Kosong", MsgBoxStyle.Information, Me.Text)

                grdTempatTidur.Focus()
                fn_Validate = False
                Exit Function
            End If
            If grdKDPENDAFTARAN.Text = String.Empty Then
                grdKDPENDAFTARAN.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                grdKDPENDAFTARAN.ErrorText = Statement.ErrorRequired

                grdKDPENDAFTARAN.Focus()
                fn_Validate = False
                Exit Function
            End If
            If grdPENJAMIN.Text = String.Empty Then
                grdPENJAMIN.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                grdPENJAMIN.ErrorText = Statement.ErrorRequired

                grdPENJAMIN.Focus()
                fn_Validate = False
                Exit Function
            End If
            If grdDOCTOR.Text = String.Empty Then
                grdDOCTOR.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                grdDOCTOR.ErrorText = Statement.ErrorRequired

                grdDOCTOR.Focus()
                fn_Validate = False
                Exit Function
            End If
            If grdKDRUANGRAWAT.Text = String.Empty Then
                grdKDRUANGRAWAT.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                grdKDRUANGRAWAT.ErrorText = Statement.ErrorRequired

                grdKDRUANGRAWAT.Focus()
                fn_Validate = False
                Exit Function
            End If

            Dim dsPendaftaran = oPendaftaran_KunjunganRuangan.GetDataPendaftaran(grdKDPENDAFTARAN.EditValue)

            If dsPendaftaran IsNot Nothing Then
                If dsPendaftaran.DATE.ToString("yyyyMMdd") < deDATE_MASUK.DateTime.ToString("yyyyMMdd") Then
                    MsgBox("Tanggal Sampai harus lebih besar dari Tanggal Dari!", MsgBoxStyle.OkOnly, Me.Text)
                    fn_Validate = False
                    Exit Function
                End If
            End If
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Function
    Private Function fn_Save() As Boolean
        Try
            ' ***** HEADER *****
            Dim ds = oPendaftaran_KunjunganRuangan.GetStructureHeader
            With ds
                Try
                    .DATECREATED = oPendaftaran_KunjunganRuangan.GetData(sNoId).DATECREATED
                Catch oErr As Exception
                    .DATECREATED = Now
                End Try
                .DATEUPDATED = Now
                .KDKUNJUNGAN_RUANGAN = sNoId
                .KDPENDAFTARAN = grdKDPENDAFTARAN.EditValue
                .KDRUANGRAWAT = grdKDRUANGRAWAT.EditValue
                .SEQ = grdTempatTidur.EditValue
                .DATE_MASUK = deDATE_MASUK.DateTime
                .DATE_KELUAR = deDATE_KELUAR.DateTime
                Try
                    .ISCHEKED = oPendaftaran_KunjunganRuangan.GetData(sNoId).ISCHEKED
                Catch ex As Exception
                    .ISCHEKED = False
                End Try
                Try
                    .MEMO = oPendaftaran_KunjunganRuangan.GetData(sNoId).MEMO
                Catch ex As Exception
                    .MEMO = "RUANGAN SELANJUTNYA"
                End Try
                .KDUSER = sUserID
                .KDPENJAMIN = grdPENJAMIN.EditValue
                .KDDOCTOR = grdDOCTOR.EditValue
                .KDPERUSAHAAN = oPendaftaran_KunjunganRuangan.GetDataRuangan1(grdKDPENDAFTARAN.EditValue).KDPERUSAHAAN
            End With

            If oFormMode = FORM_MODE.FORM_MODE_ADD Then
                Try
                    txtCODE.Text = oPendaftaran_KunjunganRuangan.InsertData(ds, grdKDRUANGRAWAT.EditValue)
                    If txtCODE.Text = "" Then
                        fn_Save = False
                    Else
                        fn_Save = True
                        CetakRegister(txtCODE.Text)
                    End If
                Catch oErr As Exception
                    MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            ElseIf oFormMode = FORM_MODE.FORM_MODE_EDIT Then
                Try
                    fn_Save = oPendaftaran_KunjunganRuangan.UpdateData(ds)
                    CetakRegister(txtCODE.Text)
                Catch oErr As Exception
                    MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            End If
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            fn_Save = False
        End Try
    End Function
    Private Sub CetakRegister(ByVal KDPendaftaran_KunjunganRuangan As String)
        'Dim rpt As New xtraPendaftaran_KunjunganRuangan

        'Dim ds = oPendaftaran_KunjunganRuangan.GetData(KDPendaftaran_KunjunganRuangan)
        'rpt.bindingSource.DataSource = ds
        'Dim printTool As New DevExpress.XtraReports.UI.ReportPrintTool(rpt)
        'printTool.PrintDialog()
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
    Private Sub fn_LoadKDDEPARTMENT()
        Dim oDEPARTMENT As New Reference.clsRuangRawat
        Try
            grdKDRUANGRAWAT.Properties.DataSource = oDEPARTMENT.GetData.Where(Function(x) x.ISACTIVE = True).ToList()
            grdKDRUANGRAWAT.Properties.ValueMember = "KDRUANGRAWAT"
            grdKDRUANGRAWAT.Properties.DisplayMember = "NAME_DISPLAY"

        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadKDPENJAMIN()
        Dim oPENJAMIN As New Reference.clsPENJAMIN
        Try
            grdPENJAMIN.Properties.DataSource = oPENJAMIN.GetData.Where(Function(x) x.ISACTIVE = True).ToList()
            grdPENJAMIN.Properties.ValueMember = "KDDEPARTMENT"
            grdPENJAMIN.Properties.DisplayMember = "MEMO"

        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadKDDOCTOR()
        Dim oDOCTOR As New Reference.clsDoctor
        Try
            grdDOCTOR.Properties.DataSource = oDOCTOR.GetData.Where(Function(x) x.ISACTIVE = True).ToList()
            grdDOCTOR.Properties.ValueMember = "KDDEPARTMENT"
            grdDOCTOR.Properties.DisplayMember = "NAME_DISPLAY"

        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadTempatTidur(ByVal Parameter As String)
        Dim oRuangRawat As New Reference.clsRuangRawat
        Try
            grdTempatTidur.Properties.DataSource = oRuangRawat.GetDataDetail(Parameter).Where(Function(x) x.ISACTIVE = True).ToList()
            grdTempatTidur.Properties.ValueMember = "SEQ"
            grdTempatTidur.Properties.DisplayMember = "MEMO"
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub txtCARI_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtCARI.KeyPress
        If Asc(e.KeyChar) = 13 Then
            PopUP = True
            fn_LoadKDPENDAFTARAN(txtCARI.Text)
            txtCARI.ResetText()
        End If
    End Sub
    Private Sub fn_LoadKDPENDAFTARAN(ByVal Parameter As String)
        Dim oDEPARTMENT As New Reference.clsDepartment
        Try
            Dim oPendaftaran As New Admission.clsPendaftaran

            Dim dsPendaftaran = (From x In oPendaftaran.GetDataBySKD(Parameter, cboCARI.SelectedIndex)
                                 Where x.CATEGORY = 1
                                 Select x.KDPENDAFTARAN, x.CATEGORY, x.KDCUSTOMER, x.M_CUSTOMER.NAME_DISPLAY, TUJUAN = x.M_DEPARTMENT.NAME_DISPLAY, DOKTER = x.M_DOCTOR.NAME_DISPLAY, x.DATE).OrderByDescending(Function(x) x.DATE)

            grdKDPENDAFTARAN.Properties.DataSource = dsPendaftaran.ToList()
            grdKDPENDAFTARAN.Properties.ValueMember = "KDPENDAFTARAN"
            grdKDPENDAFTARAN.Properties.DisplayMember = "KDPENDAFTARAN"

            If PopUP = True Then
                grdKDPENDAFTARAN.ShowPopup()
            End If

        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub grdKDPENDAFTARAN_KeyPress(sender As Object, e As KeyPressEventArgs) Handles grdKDPENDAFTARAN.KeyPress
        If Asc(e.KeyChar) = 13 Then
            Dim oPendaftaran As New Admission.clsPendaftaran
            Dim dsPendaftaran = oPendaftaran.GetData(grdKDPENDAFTARAN.EditValue)
            If dsPendaftaran IsNot Nothing Then
                deDATE_MASUK.DateTime = dsPendaftaran.DATE
            End If
        End If
    End Sub
    Private Sub grdKDRUANGRAWAT_KeyPress(sender As Object, e As KeyPressEventArgs) Handles grdKDRUANGRAWAT.KeyPress
        If Asc(e.KeyChar) = 13 Then
            fn_LoadTempatTidur(grdKDRUANGRAWAT.EditValue)
            grdTempatTidur.ShowPopup()
        End If
    End Sub
    Private Sub grdTempatTidur_EditValueChanged(sender As Object, e As EventArgs) Handles grdTempatTidur.EditValueChanged
        If grdKDRUANGRAWAT.Text = String.Empty Then Exit Sub

        Dim oRuangan As New Reference.clsRuangRawat
        Dim dsCekRuangan = oRuangan.GetDataDetail(grdKDRUANGRAWAT.EditValue, grdTempatTidur.EditValue)
        If dsCekRuangan IsNot Nothing Then
            If dsCekRuangan.ISTERISI = True Then
                MsgBox("Bad Sudah Terisi", MsgBoxStyle.Exclamation, Me.Text)
                grdTempatTidur.ResetText()
            End If
        End If
    End Sub
#End Region
End Class