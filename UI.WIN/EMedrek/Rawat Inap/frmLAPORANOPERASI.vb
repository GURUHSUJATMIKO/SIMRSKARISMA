Imports DataAccess
Imports System.Data.SqlClient
Imports UI.WIN.MAIN.My.Resources
Imports System.Linq
Imports System.IO

Public Class frmLAPORANOPERASI
#Region "Declaration"
    Private oFormMode As FORM_MODE = FORM_MODE.FORM_MODE_VIEW
    Private isLoad As Boolean = False
    Private oS_DIGITAL_OK_LAPORANOPERASI As New EMedrek.clsS_DIGITAL_OK_LAPORANOPERASI
    Private oS_DIGITAL_OK_LAPORANOPERASITEMPLATE As New Transaksi.clsS_DIGITAL_OK_LAPORANOPERASITemplate
    Private sKODEDOKTER As String
    Private sSEQ As Integer = 0
    Private sKDDOKTER_NAME As String = String.Empty
#End Region
#Region "Function"
    Public Sub LoadMe(ByVal FormMode As Integer, ByVal KDREG As String, ByVal sequence As Integer, ByVal KDCUSTOMER As String, ByVal NAMAPASIEN As String, ByVal JENISKELAMIN As String, ByVal DPJP As String, ByVal KDDPJP As String)
        oFormMode = FormMode

        txtNoRegister.Text = KDREG
        txtNamaPasien.Text = NAMAPASIEN
        txtJK.Text = JENISKELAMIN
        txtNoPasien.Text = KDCUSTOMER
        sKODEDOKTER = KDDPJP
        sKDDOKTER_NAME = DPJP
    End Sub
    Private Sub Form_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'fn_LoadDataTemplate()
        fn_JENISOPERASI()
        fn_LoadOperator()
        fn_LoadAsisten()
        fn_LoadOperator2()
        fn_DIAGNOSA()
        fn_PROSEDUR()

        fn_ChangeFormState()
        isLoad = True
    End Sub
    Private Sub Form_Closing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        Dispose()
        sCode = txtNoRegister.Text.Trim.ToUpper
        sLoadLaporanOperasi = False
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

        'txtPERAWAT_3.Properties.ReadOnly = False
        DateEdit1.Properties.ReadOnly = False
        TextEdit1.Properties.ReadOnly = False
        TextEdit2.Properties.ReadOnly = False
        TextEdit3.Properties.ReadOnly = False
        TextEdit4.Properties.ReadOnly = False
        TextEdit5.Properties.ReadOnly = False
        TextEdit6.Properties.ReadOnly = False
        TextEdit7.Properties.ReadOnly = False
        TextEdit8.Properties.ReadOnly = False
        TextEdit9.Properties.ReadOnly = False
        TextEdit10.Properties.ReadOnly = False
        TextEdit11.Properties.ReadOnly = False
        TextEdit12.Properties.ReadOnly = False
        TextEdit13.Properties.ReadOnly = False
        TextEdit14.Properties.ReadOnly = False
        TextEdit15.Properties.ReadOnly = False
        TextEdit16.Properties.ReadOnly = False
        TextEdit17.Properties.ReadOnly = False
        TextEdit18.Properties.ReadOnly = False
        TextEdit19.Properties.ReadOnly = False
        MemoEdit4.Properties.ReadOnly = False
        MemoEdit5.Properties.ReadOnly = False
        MemoEdit6.Properties.ReadOnly = False
        CheckEdit1.Properties.ReadOnly = False
        CheckEdit2.Properties.ReadOnly = False
        CheckEdit3.Properties.ReadOnly = False
        CheckEdit4.Properties.ReadOnly = False
        CheckEdit5.Properties.ReadOnly = False
        CheckEdit6.Properties.ReadOnly = False
        CheckEdit7.Properties.ReadOnly = False
        CheckEdit8.Properties.ReadOnly = False
        CheckEdit9.Properties.ReadOnly = False
        CheckEdit10.Properties.ReadOnly = False
        CheckEdit11.Properties.ReadOnly = False
        CheckEdit12.Properties.ReadOnly = False
        CheckEdit13.Properties.ReadOnly = False
        CheckEdit14.Properties.ReadOnly = False
        CheckEdit15.Properties.ReadOnly = False
        CheckEdit16.Properties.ReadOnly = False
        CheckEdit17.Properties.ReadOnly = False
        CheckEdit18.Properties.ReadOnly = False
        CheckEdit19.Properties.ReadOnly = False
        CheckEdit20.Properties.ReadOnly = False

        'cboJenisOperasi

    End Sub
    Private Sub fn_EmptyMe()
        'txtCODE.Text = "<--- AUTO --->"

        'chkDIAGNOSA_1.Checked = False
        DateEdit1.EditValue = Now

        TextEdit3.ResetText()
        TextEdit4.ResetText()
        TextEdit5.ResetText()

        TextEdit8.ResetText()
        TextEdit9.ResetText()
        TextEdit10.ResetText()
        TextEdit11.ResetText()
        TextEdit12.ResetText()
        TextEdit13.Text = Now.ToString("HH:mm")
        TextEdit14.Text = Now.ToString("HH:mm")
        TextEdit15.Text = Now.ToString("HH:mm")
        TextEdit16.Text = "00:00"
        TextEdit17.ResetText()
        TextEdit18.ResetText()
        TextEdit19.ResetText()
        MemoEdit4.ResetText()
        MemoEdit5.ResetText()
        MemoEdit6.ResetText()
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

        'cboJenisOperasi.SelectedIndex = 0
        'strDiagnosa = ""
        'strDiagnosa2 = ""
        'strProsedur = ""
    End Sub
    Private Sub fn_LoadData()
        Try
            ' ***** HEADER *****
            Dim ds = oS_DIGITAL_OK_LAPORANOPERASI.GetDataByKDPENDFATRAN(txtNoRegister.Text)
            With ds

                DateEdit1.DateTime = .DATE
                TextEdit1.EditValue = .TextEdit1
                TextEdit2.EditValue = .TextEdit2
                TextEdit3.Text = .TextEdit3
                TextEdit4.Text = .TextEdit4
                TextEdit5.Text = .TextEdit5
                TextEdit6.EditValue = .TextEdit6
                TextEdit7.EditValue = .TextEdit7
                TextEdit8.Text = .TextEdit8
                TextEdit9.Text = .TextEdit9
                TextEdit10.Text = .TextEdit10
                TextEdit11.Text = .TextEdit11
                TextEdit12.Text = .TextEdit12
                'TextEdit13.EditValue = CDate(.TextEdit13)
                'TextEdit14.EditValue = CDate(.TextEdit14)
                'TextEdit15.EditValue = CDate(.TextEdit15)
                'TextEdit16.EditValue = CDate(.TextEdit16)
                TextEdit13.Text = .TextEdit13
                TextEdit14.Text = .TextEdit14
                TextEdit15.Text = .TextEdit15
                TextEdit16.Text = .TextEdit16
                TextEdit17.Text = .TextEdit17
                TextEdit18.Text = .TextEdit18
                TextEdit19.EditValue = .TextEdit19
                MemoEdit4.Text = .MemoEdit4
                MemoEdit5.Text = .MemoEdit5
                MemoEdit6.Text = .MemoEdit6
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
                cboJenisOperasi.EditValue = .JENIS_OPERASI
                TextEdit20.Text = .BARCODEALAT

                Try
                    txtDiagnosaPreOP.Text = .MemoEdit1 + vbCrLf + .MemoEdit7 + vbCrLf + .MemoEdit8
                Catch ex As Exception
                    txtDiagnosaPreOP.Text = .MemoEdit1
                End Try

                Try
                    txtDiagnosaPostOP.Text = .MemoEdit2 + vbCrLf + .TXTDIAGNOSAPOSTOP2 + vbCrLf + .TXTDIAGNOSAPOSTOP3
                Catch ex As Exception
                    txtDiagnosaPostOP.Text = .MemoEdit2
                End Try

                Try
                    txtProsedur.Text = .MemoEdit3 + vbCrLf + .PROSEDUR2 + vbCrLf + .PROSEDUR3
                Catch ex As Exception
                    txtProsedur.Text = .MemoEdit3
                End Try


                txtDokter.EditValue = .DOKTER_KODE
            End With
        Catch oErr As Exception
            MsgBox("Load List Data : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadDataTemplate(ByVal Kode As String)
        Try
            ' ***** HEADER *****
            Dim ds = oS_DIGITAL_OK_LAPORANOPERASITEMPLATE.getdata(Kode)
            With ds
                'DateEdit1.DateTime = Now
                TextEdit1.EditValue = .TextEdit1
                TextEdit2.EditValue = .TextEdit2
                TextEdit3.Text = .TextEdit3
                TextEdit4.Text = .TextEdit4
                TextEdit5.Text = .TextEdit5
                TextEdit6.EditValue = .TextEdit6
                TextEdit7.EditValue = .TextEdit7
                TextEdit8.Text = .TextEdit8
                TextEdit9.Text = .TextEdit9
                TextEdit10.Text = .TextEdit10
                TextEdit11.Text = .TextEdit11
                TextEdit12.Text = .TextEdit12
                'TextEdit13.EditValue = CDate(.TextEdit13)
                'TextEdit14.EditValue = CDate(.TextEdit14)
                'TextEdit15.EditValue = CDate(.TextEdit15)
                'TextEdit16.EditValue = CDate(.TextEdit16)
                TextEdit13.Text = .TextEdit13
                TextEdit14.Text = .TextEdit14
                TextEdit15.Text = .TextEdit15
                TextEdit16.Text = .TextEdit16
                TextEdit17.Text = .TextEdit17
                TextEdit18.Text = .TextEdit18
                TextEdit19.EditValue = .TextEdit19
                MemoEdit4.Text = .MemoEdit4
                MemoEdit5.Text = .MemoEdit5
                MemoEdit6.Text = .MemoEdit6
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
                cboJenisOperasi.EditValue = .JENIS_OPERASI
                TextEdit20.Text = .BARCODEALAT

                Try
                    txtDiagnosaPreOP.Text = .MemoEdit1 + vbCrLf + .MemoEdit7 + vbCrLf + .MemoEdit8
                Catch ex As Exception
                    txtDiagnosaPreOP.Text = .MemoEdit1
                End Try

                Try
                    txtDiagnosaPostOP.Text = .MemoEdit2 + vbCrLf + .TXTDIAGNOSAPOSTOP2 + vbCrLf + .TXTDIAGNOSAPOSTOP3
                Catch ex As Exception
                    txtDiagnosaPostOP.Text = .MemoEdit2
                End Try

                Try
                    txtProsedur.Text = .MemoEdit3 + vbCrLf + .PROSEDUR2 + vbCrLf + .PROSEDUR3
                Catch ex As Exception
                    txtProsedur.Text = .MemoEdit3
                End Try


                txtDokter.EditValue = .DOKTER_KODE
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
            If txtDokter.Text = String.Empty Then
                MsgBox("Dokter Merawat Belum di Isi", MsgBoxStyle.Exclamation, Me.Text)
                txtDokter.Focus()
                fn_Validate = False
                Exit Function
            End If
            If TextEdit1.Text = String.Empty Then
                MsgBox("Operator Belum Di Isi", MsgBoxStyle.Exclamation, Me.Text)
                TextEdit1.Focus()
                fn_Validate = False
                Exit Function
            End If
            If TextEdit2.Text = String.Empty Then
                MsgBox("Asisten I Belum Di Isi", MsgBoxStyle.Exclamation, Me.Text)
                TextEdit2.Focus()
                fn_Validate = False
                Exit Function
            End If
            If TextEdit7.Text = String.Empty Then
                MsgBox("Asisten II Belum Di Isi", MsgBoxStyle.Exclamation, Me.Text)
                TextEdit7.Focus()
                fn_Validate = False
                Exit Function
            End If
            If TextEdit6.Text = String.Empty Then
                'MsgBox("Anestesi Belum Di Isi", MsgBoxStyle.Exclamation, Me.Text)
                TextEdit6.Text = "Tidak Ada"
                'fn_Validate = False
                'Exit Function
            End If
            If TextEdit5.Text = String.Empty Then
                MsgBox("Perawat Instrumen Belum Di Isi", MsgBoxStyle.Exclamation, Me.Text)
                TextEdit5.Focus()
                fn_Validate = False
                Exit Function
            End If
            If txtDiagnosaPreOP.Text = String.Empty Then
                MsgBox("Diagnosa Pre Operatif Belum Di Isi", MsgBoxStyle.Exclamation, Me.Text)
                grdKDDIAGNOSAPREOP1.Focus()
                fn_Validate = False
                Exit Function
            End If
            If txtDiagnosaPostOP.Text = String.Empty Then
                MsgBox("Diagnosa Post Operatif Belum Di Isi", MsgBoxStyle.Exclamation, Me.Text)
                grdKDDIAGNOSAPOSTOP1.Focus()
                fn_Validate = False
                Exit Function
            End If
            If txtProsedur.Text = String.Empty Then
                MsgBox("Tindakan Operasi Belum Di Isi", MsgBoxStyle.Exclamation, Me.Text)
                grdKDPROSEDUR1.Focus()
                fn_Validate = False
                Exit Function
            End If
            If TextEdit10.Text = String.Empty Then
                MsgBox("Jaringan yang di Eksisi/insisi Belum Di Isi", MsgBoxStyle.Exclamation, Me.Text)
                TextEdit10.Focus()
                fn_Validate = False
                Exit Function
            End If
            If TextEdit11.Text = String.Empty Then
                MsgBox("Jenis Jaringan Belum Di Isi", MsgBoxStyle.Exclamation, Me.Text)
                TextEdit11.Focus()
                fn_Validate = False
                Exit Function
            End If
            If TextEdit12.Text = String.Empty Then
                MsgBox("Jenis Pemeriksaan Belum Di Isi", MsgBoxStyle.Exclamation, Me.Text)
                TextEdit12.Focus()
                fn_Validate = False
                Exit Function
            End If
            If MemoEdit4.Text = String.Empty Then
                MsgBox("Laporan Operasi Belum Di Isi", MsgBoxStyle.Exclamation, Me.Text)
                MemoEdit4.Focus()
                fn_Validate = False
                Exit Function
            End If
            If TextEdit17.Text = String.Empty Then
                MsgBox("Pendarahan Belum Di Isi.", MsgBoxStyle.Exclamation, Me.Text)
                TextEdit17.Focus()
                fn_Validate = False
                Exit Function
            End If
            If MemoEdit5.Text = String.Empty Then
                MsgBox("Komplikasi Belum Di Isi.", MsgBoxStyle.Exclamation, Me.Text)
                MemoEdit5.Focus()
                fn_Validate = False
                Exit Function
            End If
            If MemoEdit6.Text = String.Empty Then
                MsgBox("Instruksi Pasca Bedah Belum Di Isi.", MsgBoxStyle.Exclamation, Me.Text)
                MemoEdit6.Focus()
                fn_Validate = False
                Exit Function
            End If
            If TextEdit18.Text = String.Empty Then
                MsgBox("Pembuat Laporan Belum Di Isi.", MsgBoxStyle.Exclamation, Me.Text)
                TextEdit18.Focus()
                fn_Validate = False
                Exit Function
            End If
            If TextEdit19.Text = String.Empty Then
                MsgBox("Dokter Ahli Bedah Belum Di Isi.", MsgBoxStyle.Exclamation, Me.Text)
                TextEdit19.Focus()
                fn_Validate = False
                Exit Function
            End If
            'If grdNOIDUSER.Text = String.Empty Then
            '    MsgBox("Dibutuhkan Petugas Triage", MsgBoxStyle.Exclamation, Me.Text)
            '    grdNOIDUSER.Focus()
            '    fn_Validate = False
            '    Exit Function
            'End If
        Catch oErr As Exception
            MsgBox("Validate Data : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Function
    Private Function fn_Save() As Boolean
        Try
            ' ***** HEADER *****
            If TextEdit20.Text <> "" Then
                If IO.File.Exists(TextEdit20.Text) Then
                Else
                    MsgBox("file barcode implan corrupt", MsgBoxStyle.Exclamation, Me.Text)
                    Return 0
                End If
            Else
            End If

            Dim ds = oS_DIGITAL_OK_LAPORANOPERASI.GetStructureHeader
            With ds
                .KDPENDAFTARAN = txtNoRegister.Text
                .KDCUSTOMER = txtNoPasien.Text
                Try
                    .DATECREATED = oS_DIGITAL_OK_LAPORANOPERASI.GetDataByKDPENDFATRAN(txtNoRegister.Text).DATECREATED
                Catch ex As Exception
                    .DATECREATED = Now
                End Try
                .DATEUPDATED = Now
                .JENIS_OPERASI = cboJenisOperasi.EditValue
                .DATE = DateEdit1.Text
                .TextEdit1 = TextEdit1.EditValue
                .TextEdit2 = TextEdit2.Text
                .TextEdit3 = TextEdit3.Text
                .TextEdit4 = TextEdit4.Text
                .TextEdit5 = TextEdit5.Text
                .TextEdit6 = TextEdit6.Text
                .TextEdit7 = TextEdit7.Text
                .TextEdit8 = TextEdit8.Text
                .TextEdit9 = TextEdit9.Text
                .TextEdit10 = TextEdit10.Text
                .TextEdit11 = TextEdit11.Text
                .TextEdit12 = TextEdit12.Text
                '.TextEdit13 = DateTime.Parse(TextEdit13.EditValue).ToString("dd/MM/yyyy HH:mm:ss")
                '.TextEdit14 = DateTime.Parse(TextEdit14.EditValue).ToString("dd/MM/yyyy HH:mm:ss")
                '.TextEdit15 = DateTime.Parse(TextEdit15.EditValue).ToString("dd/MM/yyyy HH:mm:ss")
                '.TextEdit16 = DateTime.Parse(TextEdit16.EditValue).ToString("dd/MM/yyyy HH:mm:ss")
                .TextEdit13 = TextEdit13.Text
                .TextEdit14 = TextEdit14.Text
                .TextEdit15 = TextEdit15.Text
                .TextEdit16 = TextEdit16.Text
                .TextEdit17 = TextEdit17.Text
                .TextEdit18 = TextEdit18.Text
                .TextEdit19 = TextEdit19.EditValue

                .MemoEdit1 = txtDiagnosaPreOP.Text
                .MemoEdit2 = txtDiagnosaPostOP.Text
                .MemoEdit3 = txtProsedur.Text
                .BARCODEALAT = TextEdit20.Text


                'If grdKDDIAGNOSAPREOP2.EditValue Is Nothing Then
                .MemoEdit7 = " "
                'Else
                '    .MemoEdit7 = grdKDDIAGNOSAPREOP2.EditValue
                'End If
                'If grdKDDIAGNOSAPREOP3.EditValue Is Nothing Then
                .MemoEdit8 = " "
                'Else
                '    .MemoEdit8 = grdKDDIAGNOSAPREOP3.EditValue
                'End If



                'If grdKDDIAGNOSAPOSTOP2.EditValue Is Nothing Then
                .TXTDIAGNOSAPOSTOP2 = " "
                'Else
                '    .TXTDIAGNOSAPOSTOP2 = grdKDDIAGNOSAPOSTOP2.EditValue
                'End If
                'If grdKDDIAGNOSAPOSTOP3.EditValue Is Nothing Then
                .TXTDIAGNOSAPOSTOP3 = " "
                'Else
                '    .TXTDIAGNOSAPOSTOP3 = grdKDDIAGNOSAPOSTOP3.EditValue
                'End If


                'If grdKDPROSEDUR2.EditValue Is Nothing Then
                .PROSEDUR2 = " "
                'Else
                '    .PROSEDUR2 = grdKDPROSEDUR2.EditValue
                'End If
                'If grdKDPROSEDUR3.EditValue Is Nothing Then
                .PROSEDUR3 = " "
                'Else
                '    .PROSEDUR3 = grdKDPROSEDUR3.EditValue
                'End If

                .MemoEdit4 = MemoEdit4.Text
                .MemoEdit5 = MemoEdit5.Text
                .MemoEdit6 = MemoEdit6.Text
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

                .KDDOCTOR = sKODEDOKTER
                .BARCODEALAT = TextEdit20.Text


                .DOKTER_KODE = txtDokter.EditValue
                .DOKTER_NAMEDISPLAY = txtDokter.Text

                Try
                    If oFormMode = FORM_MODE.FORM_MODE_EDIT Then
                        .SEQ = sSEQ
                    ElseIf oFormMode = FORM_MODE.FORM_MODE_ADD Then
                        .SEQ = oS_DIGITAL_OK_LAPORANOPERASI.GetSequence(txtNoRegister.Text) + 1
                    End If
                Catch ex As Exception
                    .SEQ = sSEQ
                End Try

                Try
                    .CETAK = oS_DIGITAL_OK_LAPORANOPERASI.GetDataByKDPENDFATRAN(txtNoRegister.Text).CETAK
                Catch ex As Exception
                    .CETAK = 0
                End Try
                .KDUSER = sUserID
                .KDUSER_SIGNATURE = ""
            End With

            'oFormMode = 1
            If oFormMode = FORM_MODE.FORM_MODE_ADD Then
                Try
                    fn_Save = oS_DIGITAL_OK_LAPORANOPERASI.InsertData(ds, Nothing, Nothing, Nothing)
                Catch ex As Exception
                    MsgBox("Simpan Data : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            ElseIf oFormMode = FORM_MODE.FORM_MODE_EDIT Then
                Try
                    fn_Save = oS_DIGITAL_OK_LAPORANOPERASI.UpdateData(ds, Nothing, Nothing, Nothing)
                Catch ex As Exception
                    MsgBox("Simpan Data : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            End If
        Catch oErr As Exception
            MsgBox("Simpan Data : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            fn_Save = False
        End Try
    End Function
    Private Function fn_SaveTemplate() As Boolean
        Try
            ' ***** HEADER *****
            If TextEdit20.Text <> "" Then
                If IO.File.Exists(TextEdit20.Text) Then
                Else
                    MsgBox("file barcode implan corrupt", MsgBoxStyle.Exclamation, Me.Text)
                    Return 0
                End If
            Else
            End If

            Dim ds = oS_DIGITAL_OK_LAPORANOPERASITEMPLATE.GetStructureHeader
            With ds
                .KDJUDUL = sKODEASESMENCOPY
                Try
                    .DATECREATED = oS_DIGITAL_OK_LAPORANOPERASI.GetDataByKDPENDFATRAN(sKODEASESMENCOPY).DATECREATED
                Catch ex As Exception
                    .DATECREATED = Now
                End Try
                .DATEUPDATED = Now
                .JENIS_OPERASI = cboJenisOperasi.EditValue
                .TextEdit1 = TextEdit1.EditValue
                .TextEdit2 = TextEdit2.Text
                .TextEdit3 = TextEdit3.Text
                .TextEdit4 = TextEdit4.Text
                .TextEdit5 = TextEdit5.Text
                .TextEdit6 = TextEdit6.Text
                .TextEdit7 = TextEdit7.Text
                .TextEdit8 = TextEdit8.Text
                .TextEdit9 = TextEdit9.Text
                .TextEdit10 = TextEdit10.Text
                .TextEdit11 = TextEdit11.Text
                .TextEdit12 = TextEdit12.Text
                '.TextEdit13 = DateTime.Parse(TextEdit13.EditValue).ToString("dd/MM/yyyy HH:mm:ss")
                '.TextEdit14 = DateTime.Parse(TextEdit14.EditValue).ToString("dd/MM/yyyy HH:mm:ss")
                '.TextEdit15 = DateTime.Parse(TextEdit15.EditValue).ToString("dd/MM/yyyy HH:mm:ss")
                '.TextEdit16 = DateTime.Parse(TextEdit16.EditValue).ToString("dd/MM/yyyy HH:mm:ss")
                .TextEdit13 = TextEdit13.Text
                .TextEdit14 = TextEdit14.Text
                .TextEdit15 = TextEdit15.Text
                .TextEdit16 = TextEdit16.Text
                .TextEdit17 = TextEdit17.Text
                .TextEdit18 = TextEdit18.Text
                .TextEdit19 = TextEdit19.EditValue

                .MemoEdit1 = txtDiagnosaPreOP.Text
                .MemoEdit2 = txtDiagnosaPostOP.Text
                .MemoEdit3 = txtProsedur.Text
                .BARCODEALAT = TextEdit20.Text


                'If grdKDDIAGNOSAPREOP2.EditValue Is Nothing Then
                .MemoEdit7 = " "
                'Else
                '    .MemoEdit7 = grdKDDIAGNOSAPREOP2.EditValue
                'End If
                'If grdKDDIAGNOSAPREOP3.EditValue Is Nothing Then
                .MemoEdit8 = " "
                'Else
                '    .MemoEdit8 = grdKDDIAGNOSAPREOP3.EditValue
                'End If



                'If grdKDDIAGNOSAPOSTOP2.EditValue Is Nothing Then
                .TXTDIAGNOSAPOSTOP2 = " "
                'Else
                '    .TXTDIAGNOSAPOSTOP2 = grdKDDIAGNOSAPOSTOP2.EditValue
                'End If
                'If grdKDDIAGNOSAPOSTOP3.EditValue Is Nothing Then
                .TXTDIAGNOSAPOSTOP3 = " "
                'Else
                '    .TXTDIAGNOSAPOSTOP3 = grdKDDIAGNOSAPOSTOP3.EditValue
                'End If


                'If grdKDPROSEDUR2.EditValue Is Nothing Then
                .PROSEDUR2 = " "
                'Else
                '    .PROSEDUR2 = grdKDPROSEDUR2.EditValue
                'End If
                'If grdKDPROSEDUR3.EditValue Is Nothing Then
                .PROSEDUR3 = " "
                'Else
                '    .PROSEDUR3 = grdKDPROSEDUR3.EditValue
                'End If

                .MemoEdit4 = MemoEdit4.Text
                .MemoEdit5 = MemoEdit5.Text
                .MemoEdit6 = MemoEdit6.Text
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

                .KDDOCTOR = sKODEDOKTER
                .BARCODEALAT = TextEdit20.Text


                .DOKTER_KODE = txtDokter.EditValue
                .DOKTER_NAMEDISPLAY = txtDokter.Text


                Try
                    .CETAK = oS_DIGITAL_OK_LAPORANOPERASI.GetDataByKDPENDFATRAN(sKODEASESMENCOPY).CETAK
                Catch ex As Exception
                    .CETAK = 0
                End Try
                .KDUSER = sUserID
                .KDUSER_SIGNATURE = ""
            End With

            'oFormMode = 1
            'If oFormMode = FORM_MODE.FORM_MODE_ADD Then
            '    Try
            '        sKODEASESMENCOPY = fn_SaveTemplate = oS_DIGITAL_OK_LAPORANOPERASITEMPLATE.InsertData(ds)

            '        If sKODEASESMENCOPY = "" Then
            '            fn_SaveTemplate = False
            '        Else
            '            fn_SaveTemplate = True
            '        End If
            '    Catch ex As Exception
            '        MsgBox("Simpan Data : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
            '    End Try
            'ElseIf oFormMode = FORM_MODE.FORM_MODE_EDIT Then
            '    Try
            '        fn_SaveTemplate = oS_DIGITAL_OK_LAPORANOPERASITEMPLATE.UpdateData(ds)
            '    Catch ex As Exception
            '        MsgBox("Simpan Data : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
            '    End Try
            'End If
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
            Case Keys.F2
                If btnSaveNew.Enabled = True Then
                    btnSaveNew_Click()
                End If
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

    Private Sub fn_LoadOperator()
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

            TextEdit1.Properties.DataSource = ds.Tables("DOKTER")
            TextEdit1.Properties.ValueMember = "NAME_DISPLAY"
            TextEdit1.Properties.DisplayMember = "NAME_DISPLAY"

            TextEdit19.Properties.DataSource = ds.Tables("DOKTER")
            TextEdit19.Properties.ValueMember = "NAME_DISPLAY"
            TextEdit19.Properties.DisplayMember = "NAME_DISPLAY"

            TextEdit2.Properties.DataSource = ds.Tables("DOKTER")
            TextEdit2.Properties.ValueMember = "NAME_DISPLAY"
            TextEdit2.Properties.DisplayMember = "NAME_DISPLAY"

            TextEdit6.Properties.DataSource = ds.Tables("DOKTER")
            TextEdit6.Properties.ValueMember = "NAME_DISPLAY"
            TextEdit6.Properties.DisplayMember = "NAME_DISPLAY"

            TextEdit7.Properties.DataSource = ds.Tables("DOKTER")
            TextEdit7.Properties.ValueMember = "NAME_DISPLAY"
            TextEdit7.Properties.DisplayMember = "NAME_DISPLAY"

            txtDokter.Properties.DataSource = ds.Tables("DOKTER")
            txtDokter.Properties.ValueMember = "KDSTAFF"
            txtDokter.Properties.DisplayMember = "NAME_DISPLAY"


            If oConn.State = ConnectionState.Open Then
                oConn.Close()
            End If
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try

        'Try
        '    Dim oConn As New SqlConnection
        '    Dim oComm As New SqlCommand
        '    Dim da As SqlDataAdapter
        '    Dim ds As New DataSet
        '    Dim SQL As String
        '    Dim sConn As String = sKoneksi
        '    oConn = New SqlConnection(sConn)

        '    If oConn.State = ConnectionState.Closed Then
        '        oConn.Open()
        '    End If

        '    SQL = "SELECT "
        '    SQL &= "* "
        '    SQL &= "FROM "
        '    SQL &= "M_DOCTOR A "
        '    SQL &= "WHERE "
        '    SQL &= "A.ISACTIVE = 1 "
        '    'SQL &= "And A.KDDOCTORSUB = 8 "

        '    oComm.Connection = oConn
        '    oComm.CommandText = SQL
        '    oComm.CommandTimeout = 120
        '    oComm.CommandType = CommandType.Text

        '    da = New SqlDataAdapter(oComm)
        '    da.Fill(ds, "DOCTOR")

        '    TextEdit1.Properties.DataSource = ds.Tables("DOCTOR")
        '    TextEdit1.Properties.ValueMember = "NAME_DISPLAY"
        '    TextEdit1.Properties.DisplayMember = "NAME_DISPLAY"

        '    TextEdit19.Properties.DataSource = ds.Tables("DOCTOR")
        '    TextEdit19.Properties.ValueMember = "NAME_DISPLAY"
        '    TextEdit19.Properties.DisplayMember = "NAME_DISPLAY"

        '    If oConn.State = ConnectionState.Open Then
        '        oConn.Close()
        '    End If
        'Catch oErr As Exception
        '    MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        'End Try
    End Sub

    Private Sub fn_LoadOperator2()
        'Try
        '    Dim oConn As New SqlConnection
        '    Dim oComm As New SqlCommand
        '    Dim da As SqlDataAdapter
        '    Dim ds As New DataSet
        '    Dim SQL As String
        '    Dim sConn As String = sKoneksi
        '    oConn = New SqlConnection(sConn)

        '    If oConn.State = ConnectionState.Closed Then
        '        oConn.Open()
        '    End If

        '    SQL = "SELECT "
        '    SQL &= "* "
        '    SQL &= "FROM "
        '    SQL &= "M_DOCTOR A "
        '    SQL &= "WHERE "
        '    SQL &= "A.ISACTIVE = 1 "
        '    'SQL &= "And A.KDDOCTORSUB = 16 "

        '    oComm.Connection = oConn
        '    oComm.CommandText = SQL
        '    oComm.CommandTimeout = 120
        '    oComm.CommandType = CommandType.Text

        '    da = New SqlDataAdapter(oComm)
        '    da.Fill(ds, "DOCTOR")

        '    TextEdit6.Properties.DataSource = ds.Tables("DOCTOR")
        '    TextEdit6.Properties.ValueMember = "NAME_DISPLAY"
        '    TextEdit6.Properties.DisplayMember = "NAME_DISPLAY"

        '    If oConn.State = ConnectionState.Open Then
        '        oConn.Close()
        '    End If
        'Catch oErr As Exception
        '    MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        'End Try
    End Sub

    Private Sub fn_LoadAsisten()
        'Try
        '    Dim oConn As New SqlConnection
        '    Dim oComm As New SqlCommand
        '    Dim da As SqlDataAdapter
        '    Dim ds As New DataSet
        '    Dim SQL As String
        '    Dim sConn As String = sKoneksi
        '    oConn = New SqlConnection(sConn)

        '    If oConn.State = ConnectionState.Closed Then
        '        oConn.Open()
        '    End If

        '    SQL = "SELECT "
        '    SQL &= "* "
        '    SQL &= "FROM "
        '    SQL &= "M_DOCTOR A "
        '    SQL &= "WHERE "
        '    SQL &= "A.ISACTIVE = 1 "

        '    oComm.Connection = oConn
        '    oComm.CommandText = SQL
        '    oComm.CommandTimeout = 120
        '    oComm.CommandType = CommandType.Text

        '    da = New SqlDataAdapter(oComm)
        '    da.Fill(ds, "DOCTOR")

        '    TextEdit2.Properties.DataSource = ds.Tables("DOCTOR")
        '    TextEdit2.Properties.ValueMember = "NAME_DISPLAY"
        '    TextEdit2.Properties.DisplayMember = "NAME_DISPLAY"

        '    TextEdit7.Properties.DataSource = ds.Tables("DOCTOR")
        '    TextEdit7.Properties.ValueMember = "NAME_DISPLAY"
        '    TextEdit7.Properties.DisplayMember = "NAME_DISPLAY"

        '    If oConn.State = ConnectionState.Open Then
        '        oConn.Close()
        '    End If
        'Catch oErr As Exception
        '    MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        'End Try
    End Sub

    'Private Sub TextEdit15_EditValueChanged(sender As Object, e As EventArgs) Handles TextEdit15.EditValueChanged
    '    TextEdit16.Text = (TextEdit15.EditValue - TextEdit14.EditValue).ToString
    'End Sub

    Private Sub fn_DIAGNOSA()
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
            SQL &= "* "
            SQL &= "FROM "
            SQL &= "M_DIAGNOSA A "
            SQL &= "WHERE "
            SQL &= "A.ISACTIVE = 1 "

            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "DIAGNOSA")

            grdKDDIAGNOSAPREOP1.Properties.DataSource = ds.Tables("DIAGNOSA")
            grdKDDIAGNOSAPREOP1.Properties.ValueMember = "MEMO"   'KDDIAGNOSA
            grdKDDIAGNOSAPREOP1.Properties.DisplayMember = "MEMO"

            'grdKDDIAGNOSAPREOP2.Properties.DataSource = ds.Tables("DIAGNOSA")
            'grdKDDIAGNOSAPREOP2.Properties.ValueMember = "MEMO"   'KDDIAGNOSA
            'grdKDDIAGNOSAPREOP2.Properties.DisplayMember = "MEMO"

            'grdKDDIAGNOSAPREOP3.Properties.DataSource = ds.Tables("DIAGNOSA")
            'grdKDDIAGNOSAPREOP3.Properties.ValueMember = "MEMO"   'KDDIAGNOSA
            'grdKDDIAGNOSAPREOP3.Properties.DisplayMember = "MEMO"

            grdKDDIAGNOSAPOSTOP1.Properties.DataSource = ds.Tables("DIAGNOSA")
            grdKDDIAGNOSAPOSTOP1.Properties.ValueMember = "MEMO"
            grdKDDIAGNOSAPOSTOP1.Properties.DisplayMember = "MEMO"

            'grdKDDIAGNOSAPOSTOP2.Properties.DataSource = ds.Tables("DIAGNOSA")
            'grdKDDIAGNOSAPOSTOP2.Properties.ValueMember = "MEMO"
            'grdKDDIAGNOSAPOSTOP2.Properties.DisplayMember = "MEMO"

            'grdKDDIAGNOSAPOSTOP3.Properties.DataSource = ds.Tables("DIAGNOSA")
            'grdKDDIAGNOSAPOSTOP3.Properties.ValueMember = "MEMO"
            'grdKDDIAGNOSAPOSTOP3.Properties.DisplayMember = "MEMO"

            If oConn.State = ConnectionState.Open Then
                oConn.Close()
            End If
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_PROSEDUR()
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
            SQL &= "* "
            SQL &= "FROM "
            SQL &= "M_PROSEDUR A "
            SQL &= "WHERE "
            SQL &= "A.ISACTIVE = 1 "

            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "PROSEDUR")

            grdKDPROSEDUR1.Properties.DataSource = ds.Tables("PROSEDUR")
            grdKDPROSEDUR1.Properties.ValueMember = "MEMO"
            grdKDPROSEDUR1.Properties.DisplayMember = "MEMO"

            'grdKDPROSEDUR2.Properties.DataSource = ds.Tables("PROSEDUR")
            'grdKDPROSEDUR2.Properties.ValueMember = "MEMO"
            'grdKDPROSEDUR2.Properties.DisplayMember = "MEMO"

            'grdKDPROSEDUR3.Properties.DataSource = ds.Tables("PROSEDUR")
            'grdKDPROSEDUR3.Properties.ValueMember = "MEMO"
            'grdKDPROSEDUR3.Properties.DisplayMember = "MEMO"

            If oConn.State = ConnectionState.Open Then
                oConn.Close()
            End If
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub

    Private Sub fn_JENISOPERASI(Optional ByVal sSpesialis As String = "")
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
            SQL &= "* "
            SQL &= "FROM "
            SQL &= "M_LAPORAN_OPERASI A "
            SQL &= "WHERE "
            SQL &= "A.ISACTIVE = 1 "

            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "JENISLAPORAN")

            cboJenisOperasi.Properties.DataSource = ds.Tables("JENISLAPORAN")
            cboJenisOperasi.Properties.ValueMember = "NAMALAPORAN"
            cboJenisOperasi.Properties.DisplayMember = "NAMALAPORAN"

            If oConn.State = ConnectionState.Open Then
                oConn.Close()
            End If

            If sSpesialis <> "" Then
                For i As Integer = 0 To ds.Tables("JENISLAPORAN").Rows.Count -1
                    If ds.Tables("JENISLAPORAN").Rows(i)("LAMPIRAN").ToString.Contains(sSpesialis) Then
                        cboJenisOperasi.EditValue = ds.Tables("JENISLAPORAN").Rows(i)("NAMALAPORAN").ToString()
                    End If
                Next
            End If
            

        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub

#End Region

#Region "Handlle"
    Private Sub chKamarOperasi_Click(sender As Object, e As EventArgs) Handles CheckEdit1.Click, CheckEdit2.Click, CheckEdit3.Click, CheckEdit4.Click, CheckEdit5.Click, CheckEdit6.Click
        CheckEdit1.Checked = False
        CheckEdit2.Checked = False
        CheckEdit3.Checked = False
        CheckEdit4.Checked = False
        CheckEdit5.Checked = False
        CheckEdit6.Checked = False
    End Sub
    Private Sub chJenisAnestesi_Click(sender As Object, e As EventArgs) Handles CheckEdit7.Click, CheckEdit8.Click, CheckEdit9.Click, CheckEdit10.Click
        CheckEdit7.Checked = False
        CheckEdit8.Checked = False
        CheckEdit9.Checked = False
        CheckEdit10.Checked = False
    End Sub
    Private Sub chKlasifikasi_Click(sender As Object, e As EventArgs) Handles CheckEdit11.Click, CheckEdit12.Click
        CheckEdit11.Checked = False
        CheckEdit12.Checked = False
    End Sub
    Private Sub chJenisOperasi_Click(sender As Object, e As EventArgs) Handles CheckEdit13.Click, CheckEdit14.Click, CheckEdit15.Click, CheckEdit16.Click
        CheckEdit13.Checked = False
        CheckEdit14.Checked = False
        CheckEdit15.Checked = False
        CheckEdit16.Checked = False
    End Sub
    Private Sub chPemeriksaanPA_Click(sender As Object, e As EventArgs) Handles CheckEdit17.Click, CheckEdit18.Click
        CheckEdit17.Checked = False
        CheckEdit18.Checked = False
    End Sub
    Private Sub chPemeriksaanCairan_Click(sender As Object, e As EventArgs) Handles CheckEdit19.Click, CheckEdit20.Click
        CheckEdit19.Checked = False
        CheckEdit20.Checked = False
    End Sub


    Private Sub grdKDDIAGNOSAPREOP1_EditValueChanged(sender As Object, e As EventArgs) Handles grdKDDIAGNOSAPREOP1.EditValueChanged
        txtDiagnosaPreOP.Text = txtDiagnosaPreOP.Text + grdKDDIAGNOSAPREOP1.EditValue + " "
    End Sub

    Private Sub grdKDDIAGNOSAPOSTOP1_EditValueChanged(sender As Object, e As EventArgs) Handles grdKDDIAGNOSAPOSTOP1.EditValueChanged
        txtDiagnosaPostOP.Text = txtDiagnosaPostOP.Text + grdKDDIAGNOSAPOSTOP1.EditValue + " "
    End Sub

    Private Sub grdKDPROSEDUR1_EditValueChanged(sender As Object, e As EventArgs) Handles grdKDPROSEDUR1.EditValueChanged
        txtProsedur.Text = txtProsedur.Text + grdKDPROSEDUR1.EditValue + " "
    End Sub

    Private Sub LabelControl57_Click(sender As Object, e As EventArgs) Handles LabelControl57.Click

    End Sub

    Private Sub SimpleButton2_Click(sender As Object, e As EventArgs) Handles SimpleButton2.Click
        'Shell("explorer ""\\172.165.115.250\barcode""", vbNormalFocus)
        Dim fBrowse As New OpenFileDialog
        With fBrowse
            .Filter = "image jpg files(*.jpg)|*.jpg"
            .Title = "Upload Barcode Implan"
            .Multiselect = False
        End With

        If fBrowse.ShowDialog() = Windows.Forms.DialogResult.OK Then
            Dim src = fBrowse.FileName
            Dim fileName = Path.GetFileNameWithoutExtension(src)
            Dim fileNameExt = Path.GetExtension(src)
            Dim oDate As String = DateTime.Now.ToString("yyyyMMddHHmmss")

            Dim dest = "\\172.165.115.250\barcode\Uploaded\" & fileName & "_" & oDate & fileNameExt
            TextEdit20.Text = dest
            System.IO.File.Copy(src, dest)
        End If

    End Sub

    Private Sub txtDokter_EditValueChanged(sender As Object, e As EventArgs) Handles txtDokter.EditValueChanged
        If txtDokter.Text IsNot Nothing Then
            Try
                Dim index = txtDokter.Text.ToUpper.IndexOf(" SP")
                If index > -1 Then
                    Dim str As String = ""

                    Try
                        str = txtDokter.Text.ToString.ToUpper.Substring(index,6)
                    Catch ex As Exception
                        str = txtDokter.Text.ToString.ToUpper.Substring(index,5)
                    End Try

                    fn_JENISOPERASI(str.Trim())
                Else
                    fn_JENISOPERASI()
                End If
            Catch ex As Exception
                Exit Sub
            End Try
        End If
    End Sub

    Private Sub frmLAPORANOPERASI_MouseWheel(sender As Object, e As MouseEventArgs) Handles MyBase.MouseWheel
	    If e.Delta > 0 Then
		    'up
		    fn_ScrollPage(True)
	    Else
		    'down
		    fn_ScrollPage(False)
	    End If
    End Sub

    Private Sub fn_ScrollPage(ByVal isUp As Boolean)
	    Dim myView As Point = Me.Panel1.AutoScrollPosition
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

	    Me.Panel1.AutoScrollPosition = myView
    End Sub
    Private Sub btnLoadData_Click(sender As Object, e As EventArgs) Handles btnLoadData.Click
        Dim frmListTemplate As New frmListTemplate
        Try
            sKODEASESMENCOPY = String.Empty

            frmListTemplate.fn_LoadKategori(4, "")
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
    'Private Sub fn_LoadDataTemplate()
    '    Try
    '        Dim oTemplateLaporanOperasi As New Master.clsTemplateLaporanOperasi
    '        Dim ds = From x In oTemplateLaporanOperasi.GetData
    '                 Select x.KDTLO, x.JUDUL, x.JENIS_OPERASI

    '        grdTemplateLO.Properties.DataSource = ds.ToList
    '        grdTemplateLO.Properties.ValueMember = "KDTLO"
    '        grdTemplateLO.Properties.DisplayMember = "JUDUL"

    '    Catch ex As Exception
    '        MsgBox(Statement.ErrorStatement & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
    '    End Try
    'End Sub

    'Private Sub grdTemplateLO_KeyPress(sender As Object, e As KeyPressEventArgs)
    '    If Asc(e.KeyChar) = 13 And String.IsNullOrWhiteSpace(grdTemplateLO.EditValue) = False Then
    '        Dim oTLO As New Master.clsTemplateLaporanOperasi
    '        Dim ds = oTLO.GetData(grdTemplateLO.EditValue)
    '        If ds IsNot Nothing Then
    '            TextEdit1.EditValue = ds.TextEdit1
    '            TextEdit2.EditValue = ds.TextEdit2
    '            TextEdit3.Text = ds.TextEdit3
    '            TextEdit4.Text = ds.TextEdit4
    '            TextEdit5.Text = ds.TextEdit5
    '            TextEdit6.EditValue = ds.TextEdit6
    '            TextEdit7.EditValue = ds.TextEdit7
    '            TextEdit8.Text = ds.TextEdit8
    '            TextEdit9.Text = ds.TextEdit9
    '            TextEdit10.Text = ds.TextEdit10
    '            TextEdit11.Text = ds.TextEdit11
    '            TextEdit12.Text = ds.TextEdit12
    '            'TextEdit13.EditValue = CDate(.TextEdit13)
    '            'TextEdit14.EditValue = CDate(.TextEdit14)
    '            'TextEdit15.EditValue = CDate(.TextEdit15)
    '            'TextEdit16.EditValue = CDate(.TextEdit16)
    '            TextEdit13.Text = ds.TextEdit13
    '            TextEdit14.Text = ds.TextEdit14
    '            TextEdit15.Text = ds.TextEdit15
    '            TextEdit16.Text = ds.TextEdit16
    '            TextEdit17.Text = ds.TextEdit17
    '            TextEdit18.Text = ds.TextEdit18
    '            TextEdit19.EditValue = ds.TextEdit19
    '            MemoEdit4.Text = ds.MemoEdit4
    '            MemoEdit5.Text = ds.MemoEdit5
    '            MemoEdit6.Text = ds.MemoEdit6
    '            CheckEdit1.Checked = ds.CheckEdit1
    '            CheckEdit2.Checked = ds.CheckEdit2
    '            CheckEdit3.Checked = ds.CheckEdit3
    '            CheckEdit4.Checked = ds.CheckEdit4
    '            CheckEdit5.Checked = ds.CheckEdit5
    '            CheckEdit6.Checked = ds.CheckEdit6
    '            CheckEdit7.Checked = ds.CheckEdit7
    '            CheckEdit8.Checked = ds.CheckEdit8
    '            CheckEdit9.Checked = ds.CheckEdit9
    '            CheckEdit10.Checked = ds.CheckEdit10
    '            CheckEdit11.Checked = ds.CheckEdit11
    '            CheckEdit12.Checked = ds.CheckEdit12
    '            CheckEdit13.Checked = ds.CheckEdit13
    '            CheckEdit14.Checked = ds.CheckEdit14
    '            CheckEdit15.Checked = ds.CheckEdit15
    '            CheckEdit16.Checked = ds.CheckEdit16
    '            CheckEdit17.Checked = ds.CheckEdit17
    '            CheckEdit18.Checked = ds.CheckEdit18
    '            CheckEdit19.Checked = ds.CheckEdit19
    '            CheckEdit20.Checked = ds.CheckEdit20
    '            cboJenisOperasi.EditValue = ds.JENIS_OPERASI

    '            Try
    '                txtDiagnosaPreOP.Text = ds.MemoEdit1 + vbCrLf + ds.MemoEdit7 + vbCrLf + ds.MemoEdit8
    '                If String.IsNullOrWhiteSpace(txtDiagnosaPreOP.Text) Then
    '                    txtDiagnosaPreOP.ResetText()
    '                End If
    '            Catch ex As Exception
    '                txtDiagnosaPreOP.Text = ds.MemoEdit1
    '            End Try

    '            Try
    '                txtDiagnosaPostOP.Text = ds.MemoEdit2 + vbCrLf + ds.TXTDIAGNOSAPOSTOP2 + vbCrLf + ds.TXTDIAGNOSAPOSTOP3
    '                If String.IsNullOrWhiteSpace(txtDiagnosaPostOP.Text) Then
    '                    txtDiagnosaPostOP.ResetText()
    '                End If
    '            Catch ex As Exception
    '                txtDiagnosaPostOP.Text = ds.MemoEdit2
    '            End Try

    '            Try
    '                txtProsedur.Text = ds.MemoEdit3 + vbCrLf + ds.PROSEDUR2 + vbCrLf + ds.PROSEDUR3
    '                If String.IsNullOrWhiteSpace(txtProsedur.Text) Then
    '                    txtProsedur.ResetText()
    '                End If
    '            Catch ex As Exception
    '                txtProsedur.Text = ds.MemoEdit3
    '            End Try

    '            txtDokter.EditValue = ds.DOKTER_KODE
    '        End If
    '    End If
    'End Sub

#End Region
End Class