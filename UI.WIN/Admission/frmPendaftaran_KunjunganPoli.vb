Imports DataAccess
Imports UI.WIN.MAIN.My.Resources
Imports System.Linq
Imports System.Data.SqlClient

Public Class frmPendaftaran_KunjunganPoli
#Region "Declaration"
    Private oFormMode As FORM_MODE = FORM_MODE.FORM_MODE_VIEW
    Private sNoId As String
    Private isLoad As Boolean = False
    Private oPendaftaran_KunjunganPoli As New Admission.clsPendaftaran_KunjunganPoli
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
            Me.Text = Pendaftaran_KunjunganPoli.TITLE

            lKDKUNJUNGAN_POLI.Text = Pendaftaran_KunjunganPoli.KDKUNJUNGAN_POLI
            lKDPENDAFATRAN.Text = Pendaftaran.KDPENDAFTARAN & " *"
            lDATE_MASUK.Text = Pendaftaran_KunjunganPoli.DATE_MASUK
            lDATE_KELUAR.Text = Pendaftaran_KunjunganPoli.DATE_KELUAR
            lKDDEPARTMENT.Text = "Poli"
            lPENJAMIN.Text = "Penjamin"
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
        grdKDDEPARTMENT.Properties.ReadOnly = Status
        grdPENJAMIN.Properties.ReadOnly = Status
        grdDOCTOR.Properties.ReadOnly = Status
    End Sub
    Private Sub fn_EmptyMe()
        txtCODE.Text = "<--- AUTO --->"
        grdKDPENDAFTARAN.ResetText()
        deDATE_MASUK.DateTime = Now
        deDATE_KELUAR.DateTime = Now
        grdKDDEPARTMENT.ResetText()
        grdPENJAMIN.ResetText()
        grdDOCTOR.ResetText()
        chkISJAGA.Checked = False
    End Sub
    Private Sub fn_LoadData()
        Try
            Dim ds = oPendaftaran_KunjunganPoli.GetData(sNoId)

            With ds
                txtCODE.Text = sNoId
                cboCARI.SelectedIndex = 2
                fn_LoadKDPENDAFTARAN(.KDPENDAFTARAN)
                grdKDPENDAFTARAN.Text = .KDPENDAFTARAN
                deDATE_MASUK.DateTime = .DATE_MASUK
                deDATE_KELUAR.DateTime = .DATE_KELUAR
                grdKDDEPARTMENT.Text = .KDDEPARTMENT
                fn_LoadDoctor(grdKDDEPARTMENT.EditValue)
                grdPENJAMIN.Text = .KDPENJAMIN
                grdDOCTOR.Text = .KDDOCTOR
                chkISJAGA.Checked = .ISJAGA
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
            If grdKDDEPARTMENT.Text = String.Empty Then
                grdKDDEPARTMENT.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                grdKDDEPARTMENT.ErrorText = Statement.ErrorRequired

                grdKDDEPARTMENT.Focus()
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
            Dim ds = oPendaftaran_KunjunganPoli.GetStructureHeader
            With ds
                Try
                    .DATECREATED = oPendaftaran_KunjunganPoli.GetData(sNoId).DATECREATED
                Catch oErr As Exception
                    .DATECREATED = Now
                End Try
                .DATEUPDATED = Now
                .KDKUNJUNGAN_POLI = sNoId
                .KDPENDAFTARAN = grdKDPENDAFTARAN.EditValue
                .KDDEPARTMENT = grdKDDEPARTMENT.EditValue
                .DATE_MASUK = deDATE_MASUK.DateTime
                .DATE_KELUAR = deDATE_KELUAR.DateTime
                Try
                    .ISCHEKED = oPendaftaran_KunjunganPoli.GetData(sNoId).ISCHEKED
                Catch ex As Exception
                    .ISCHEKED = False
                End Try
                Try
                    .MEMO = oPendaftaran_KunjunganPoli.GetData(sNoId).MEMO
                Catch ex As Exception
                    .MEMO = "POLI SELANJUTNYA"
                End Try
                .KDUSER = sUserID
                .KDPENJAMIN = grdPENJAMIN.EditValue
                .KDDOCTOR = grdDOCTOR.EditValue
                .KDPERUSAHAAN = oPendaftaran_KunjunganPoli.GetDataPoli1(grdKDPENDAFTARAN.EditValue).KDPERUSAHAAN
                .ISJAGA = chkISJAGA.Checked
            End With

            If oFormMode = FORM_MODE.FORM_MODE_ADD Then
                Try
                    txtCODE.Text = oPendaftaran_KunjunganPoli.InsertData(ds, oPendaftaran_KunjunganPoli.GetDataMemoDepartment(grdKDDEPARTMENT.EditValue).MEMO & oPendaftaran_KunjunganPoli.GetDataMemoDoctor(grdDOCTOR.EditValue).MEMO & IIf(chkISJAGA.Checked = False, "P", "S" & If(grdPENJAMIN.Text = "BPJS KESEHATAN", "B", "A")))
                    If txtCODE.Text = "" Then
                        fn_Save = False
                    Else
                        fn_Save = True
                        CetakRegister(txtCODE.Text)
                        If fn_Save_simrsRJ(txtCODE.Text) = False Then
                            MsgBox("Data Belum Masuk Ke SIMRS Lama", MsgBoxStyle.Exclamation, Me.Text)
                        End If
                    End If

                Catch oErr As Exception
                    MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            ElseIf oFormMode = FORM_MODE.FORM_MODE_EDIT Then
                Try
                    fn_Save = oPendaftaran_KunjunganPoli.UpdateData(ds)
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
    Private Function fn_Save_simrsRJ(ByVal KDKUNJUNGAN_POLI As String) As Boolean
        Try
            Dim oPendaftaranKunjungan As New Admission.clsPendaftaran_KunjunganPoli

            Dim dsKunjungan = oPendaftaranKunjungan.GetData(KDKUNJUNGAN_POLI)

            If dsKunjungan IsNot Nothing Then
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

                SQL = "SELECT *  "
                SQL &= "FROM S_PENDAFTARAN_H "
                SQL &= "WHERE "
                SQL &= "KDREG = '" & dsKunjungan.KDPENDAFTARAN & "' "

                oComm.Connection = oConn
                oComm.CommandText = SQL
                oComm.CommandTimeout = 120
                oComm.CommandType = CommandType.Text

                da = New SqlDataAdapter(oComm)
                da.Fill(ds, "GETDATA_PENDAFTARAN")

                If ds.Tables("GETDATA_PENDAFTARAN").Rows.Count > 0 Then

                    fn_Save_simrsRJ = True
                    Exit Function
                End If
                SQL = "INSERT INTO "
                SQL &= "S_PENDAFTARAN_H "
                SQL &= "( "
                SQL &= "KDREG "
                SQL &= ",DATECREATED "
                SQL &= ",DATEUPDATED "
                SQL &= ",DATE "
                SQL &= ",KDDOCTOR "
                SQL &= ",PRIORITAS "
                SQL &= ",PENDAFTARANVIA "
                SQL &= ",CATATAN "
                SQL &= ",KDDEBTOR "
                SQL &= ",JENISASURANSI "
                SQL &= ",KARTUBPJS "
                SQL &= ",KDGROUPCUSTOMER "
                SQL &= ",NIK "
                SQL &= ",NAMA_PJ "
                SQL &= ",NIK_PJ "
                SQL &= ",ASALRUJUKAN "
                SQL &= ",NOMORRUJUKAN "
                SQL &= ",CATEGORY "
                SQL &= ",NOIDUSER "
                SQL &= ",RUJUKAN "
                SQL &= ",KDDEPARTMENT "
                SQL &= ",ADDRESS_STREET "
                SQL &= ",PHONE "
                SQL &= ",NAMA_WALI "
                SQL &= ",HUBUNGAN "
                SQL &= ",ADDRESS_WALI "
                SQL &= ",PHONE_WALI "
                SQL &= ",DOKTER_RUJUKAN "
                SQL &= ",DIKIRIM_MELALUI "
                SQL &= ",KDDIAGNOSA "
                SQL &= ",TANGGALMASUK "
                SQL &= ",KELASRAWAT "
                SQL &= ",KDMROOM "
                SQL &= ",JENISDIET "
                SQL &= ",KETERANGANDIET "
                SQL &= ",UANGMUKA "
                SQL &= ",KETRANAP "
                SQL &= ",BERASALDARI "
                SQL &= ",KETHUBUNGAN "
                SQL &= ",KDCUSTOMER "
                SQL &= ",ISDOKUMENRM "
                SQL &= ",STATUSDAFTAR "
                SQL &= ",GRANDTOTAL "
                SQL &= ",PAYAMOUNT "
                SQL &= ",ISUPDATEPULANG "
                SQL &= ",DATEPULANG "
                SQL &= ",USIA "
                SQL &= ",WAKTUDOKUMENRM "
                SQL &= ",ISPROLANIS "
                SQL &= ",KDSKDP "
                SQL &= ",KDKELASRAWAT "
                SQL &= ",NOMORSEP "
                SQL &= ",KDPESERTA "
                SQL &= ",KDASALRUJUKAN "
                SQL &= ",KDPPK "
                SQL &= ",DATERUJUKAN "
                SQL &= ",ISEKSEKUTIF "
                SQL &= ",ISKATARAK "
                SQL &= ",ISCOB "
                SQL &= ",ISPENJAMINKKL "
                SQL &= ",ISJASARAHARJA "
                SQL &= ",ISKETENAGAKERJAAN "
                SQL &= ",ISASABRI "
                SQL &= ",ISSUPLESI "
                SQL &= ",ISTASPEN "
                SQL &= ",TANGGALKEJADIAN "
                SQL &= ",KODEPROV "
                SQL &= ",KODEKOTA "
                SQL &= ",KODEKEC "
                SQL &= ",KETERANGANKKL "
                SQL &= ",REQUEST "
                SQL &= ",RESPONSE "
                SQL &= ",KDCOB "
                SQL &= ",NOSUPLESI "
                SQL &= ",TYPE "
                SQL &= ",STATUSBPJS "
                SQL &= ",ISOFFLINE "
                SQL &= ",INFORMASIBPJS "
                SQL &= ",CETAK "
                SQL &= ",KONSUL "
                SQL &= ",DOKTERKONSUL "
                SQL &= ",KDREGAWAL "
                SQL &= ",ISNAIKRANAP "
                SQL &= ",NOMORANTRIAN "
                SQL &= ") "
                SQL &= "VALUES "
                SQL &= "( "
                SQL &= "'" & dsKunjungan.KDPENDAFTARAN & "' "
                SQL &= ",'" & dsKunjungan.DATECREATED.ToString("yyyy-MM-dd HH:mm:ss") & "' "
                SQL &= ",'" & dsKunjungan.DATEUPDATED.ToString("yyyy-MM-dd HH:mm:ss") & "' "
                SQL &= ",'" & dsKunjungan.DATE_MASUK.ToString("yyyy-MM-dd HH:mm:ss") & "' "
                SQL &= ",'" & dsKunjungan.KDDOCTOR & "' "
                SQL &= ",'" & dsKunjungan.S_PENDAFTARAN_H.KDDAFTAR_L6 & "' "
                SQL &= ",'" & dsKunjungan.S_PENDAFTARAN_H.KDDAFTAR_L4 & "' "
                SQL &= ",'" & dsKunjungan.S_PENDAFTARAN_H.CATATAN & "' "
                SQL &= ",'" & dsKunjungan.KDPENJAMIN & "' "
                SQL &= ",'" & dsKunjungan.S_PENDAFTARAN_H.M_PERUSAHAAN.MEMO & "' "
                SQL &= ",'" & dsKunjungan.S_PENDAFTARAN_H.KARTUBPJS & "' "
                SQL &= ",'" & dsKunjungan.S_PENDAFTARAN_H.KDPERUSAHAAN & "' "
                SQL &= ",'" & dsKunjungan.S_PENDAFTARAN_H.M_CUSTOMER.KTP & "' "
                SQL &= ",'" & dsKunjungan.S_PENDAFTARAN_H.PENANGGUNGJAWAB & "' "
                SQL &= ",'' "
                SQL &= ",'" & dsKunjungan.S_PENDAFTARAN_H.M_PPK.MEMO & "' "
                SQL &= ",'" & dsKunjungan.S_PENDAFTARAN_H.NOMORRUJUKAN & "' "
                SQL &= ",'1' "
                SQL &= ",'" & dsKunjungan.S_PENDAFTARAN_H.KDUSER & "' "
                SQL &= ",'" & dsKunjungan.S_PENDAFTARAN_H.KDDAFTAR_L2 & "' "
                SQL &= ",'" & dsKunjungan.KDDEPARTMENT & "' "
                SQL &= ",'" & dsKunjungan.S_PENDAFTARAN_H.JALAN & "' "
                SQL &= ",'" & dsKunjungan.S_PENDAFTARAN_H.NOMORTELEPON & "' "
                SQL &= ",'' "
                SQL &= ",'' "
                SQL &= ",'' "
                SQL &= ",'' "
                SQL &= ",'' "
                SQL &= ",'' "
                SQL &= ",'" & dsKunjungan.S_PENDAFTARAN_H.KDDIAGNOSA & "' "
                SQL &= ",'" & dsKunjungan.DATE_MASUK.ToString("yyyy-MM-dd HH:mm:ss") & "' "
                SQL &= ",'' "
                SQL &= ",'1' "
                SQL &= ",'' "
                SQL &= ",'' "
                SQL &= ",0 "
                SQL &= ",'' "
                SQL &= ",'1' "
                SQL &= ",'' "
                SQL &= ",'" & dsKunjungan.S_PENDAFTARAN_H.KDCUSTOMER & "' "
                SQL &= ",'0' "
                SQL &= ",'1' "
                SQL &= ",0 "
                SQL &= ",0 "
                SQL &= ",'0' "
                SQL &= ",'" & dsKunjungan.DATE_KELUAR.ToString("yyyy-MM-dd HH:mm:ss") & "' "
                SQL &= ",'" & dsKunjungan.S_PENDAFTARAN_H.USIA & "' "
                SQL &= ",'' "
                SQL &= ",'0' "
                SQL &= ",'" & dsKunjungan.S_PENDAFTARAN_H.NOMORSKDP & "' "
                SQL &= ",'" & dsKunjungan.S_PENDAFTARAN_H.KDKELASRAWAT & "' "
                SQL &= ",'" & dsKunjungan.S_PENDAFTARAN_H.NOMORSEP & "' "
                SQL &= ",'-' "
                SQL &= ",'1' "
                SQL &= ",'" & dsKunjungan.S_PENDAFTARAN_H.M_PPK.KODEFASKES & "' "
                SQL &= ",'" & dsKunjungan.S_PENDAFTARAN_H.DATE_RUJUKAN & "' "
                SQL &= ",'" & dsKunjungan.S_PENDAFTARAN_H.ISEKSEKUTIF & "' "
                SQL &= ",'" & dsKunjungan.S_PENDAFTARAN_H.ISKATARAK & "' "
                SQL &= ",'" & dsKunjungan.S_PENDAFTARAN_H.ISCOB & "' "
                SQL &= ",'0' "
                SQL &= ",'0' "
                SQL &= ",'0' "
                SQL &= ",'0' "
                SQL &= ",'0' "
                SQL &= ",'0' "
                SQL &= ",'" & dsKunjungan.DATE_MASUK.ToString("yyyy-MM-dd HH:mm:ss") & "' "
                SQL &= ",'0000' "
                SQL &= ",'0000' "
                SQL &= ",'0000' "
                SQL &= ",'' "
                SQL &= ",'" & dsKunjungan.S_PENDAFTARAN_H.REQUEST & "' "
                SQL &= ",'" & dsKunjungan.S_PENDAFTARAN_H.RESPON & "' "
                SQL &= ",'' "
                SQL &= ",'" & dsKunjungan.S_PENDAFTARAN_H.JAMINAN_PENJAMIN_SUPLESI_NOSEPSUPLESI & "' "
                SQL &= ",'' "
                SQL &= ",'' "
                SQL &= ",'" & dsKunjungan.S_PENDAFTARAN_H.ISOFFLINE & "' "
                SQL &= ",'" & dsKunjungan.S_PENDAFTARAN_H.INFORMASIPRB & "' "
                SQL &= ",'1' "
                SQL &= ",'2' "
                SQL &= ",'9' "
                SQL &= ",'" & dsKunjungan.S_PENDAFTARAN_H.KDPENDAFTARAN_AWAL & "' "
                SQL &= ",'0' "
                SQL &= ",'" & dsKunjungan.KDKUNJUNGAN_POLI & "' "
                SQL &= ") "

                oComm.Connection = oConn
                oComm.CommandText = SQL
                oComm.CommandTimeout = 120
                oComm.CommandType = CommandType.Text

                da = New SqlDataAdapter(oComm)
                da.Fill(ds, "INSERTSET_PENDAFTARAN")

                fn_Save_simrsRJ = True

                If oConn.State = ConnectionState.Open Then
                    oConn.Close()
                End If
            Else
                fn_Save_simrsRJ = False
            End If
        Catch oErr As Exception
            fn_Save_simrsRJ = False
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Function
    Private Sub CetakRegister(ByVal KDPendaftaran_KunjunganPoli As String)
        'Dim rpt As New xtraPendaftaran_KunjunganPoli

        'Dim ds = oPendaftaran_KunjunganPoli.GetData(KDPendaftaran_KunjunganPoli)
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
        Dim oDEPARTMENT As New Reference.clsDepartment
        Try
            grdKDDEPARTMENT.Properties.DataSource = oDEPARTMENT.GetData.Where(Function(x) x.ISACTIVE = True).ToList()
            grdKDDEPARTMENT.Properties.ValueMember = "KDDEPARTMENT"
            grdKDDEPARTMENT.Properties.DisplayMember = "NAME_DISPLAY"

        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadKDPENJAMIN()
        Dim oPENJAMIN As New Reference.clsPENJAMIN
        Try
            grdPENJAMIN.Properties.DataSource = oPENJAMIN.GetData.Where(Function(x) x.ISACTIVE = True).ToList()
            grdPENJAMIN.Properties.ValueMember = "KDPENJAMIN"
            grdPENJAMIN.Properties.DisplayMember = "MEMO"

        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadDoctor(ByVal KDDEPARTMENT As String)
        If grdKDDEPARTMENT.Text = "" Then Exit Sub

        Dim oDoctor As New Reference.clsDoctor

        Try
            Dim dsDoctor = From x In oDoctor.GetData
                           Where x.ISACTIVE = True And x.KDDEPARTMENT = grdKDDEPARTMENT.EditValue
                           Select x.KDDOCTOR, x.NAME_DISPLAY

            grdDOCTOR.Properties.DataSource = dsDoctor.ToList()
            grdDOCTOR.Properties.ValueMember = "KDDOCTOR"
            grdDOCTOR.Properties.DisplayMember = "NAME_DISPLAY"
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
                                 Where x.CATEGORY = 0
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
    Private Sub grdKDDEPARTMENT_KeyPress(sender As Object, e As KeyPressEventArgs) Handles grdKDDEPARTMENT.KeyPress
        If Asc(e.KeyChar) = 13 Then
            fn_LoadDoctor(grdKDDEPARTMENT.EditValue)
        End If
    End Sub
#End Region
End Class