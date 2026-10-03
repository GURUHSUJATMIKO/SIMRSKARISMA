Imports DataAccess
Imports UI.WIN.MAIN.My.Resources
Imports System.Linq
Imports System.Data.SqlClient

Public Class frmReqAwalPemeriksaan
#Region "Declaration"
    Private oFormMode As FORM_MODE = FORM_MODE.FORM_MODE_VIEW
    Private sNoId As String
    Private sNama As String
    Private sTanggalLahir As DateTime
    Private sNoRekamMedis As String
    Private sJK As String
    Private sKodeDokter As String
    Private isLoad As Boolean = False
    Private oReqAwalPemeriksaan As New Transaksi.clsReqAwalPemeriksaan

#End Region
#Region "Function"
    Public Sub LoadMe(ByVal FormMode As Integer, ByVal Nama As String, ByVal TanggalLahir As DateTime, ByVal NoRemamMedis As String, ByVal JK As String, ByVal NoId As String, ByVal kodedokter As String)
        oFormMode = FormMode
        sNoId = NoId
        sNama = Nama
        sTanggalLahir = TanggalLahir
        sNoRekamMedis = NoRemamMedis
        sJK = IIf(JK = "1", "Laki-laki", "Perempuan")
        sKodeDokter = kodedokter
        Dim ds = oReqAwalPemeriksaan.GetData(sNoId)
        If ds IsNot Nothing Then
            oFormMode = FORM_MODE.FORM_MODE_EDIT
        End If
    End Sub
    Private Sub Form_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        fn_ChangeFormState()
        fn_LoadLanguage()
        isLoad = True
    End Sub
    Public Sub fn_LoadLanguage()
        Try
            Me.Text = "Pemeriksaan Awal"

            btnSaveNew.Caption = Caption.FormSaveNew
            btnSaveClose.Caption = Caption.FormSaveClose
            btnClose.Caption = Caption.FormClose
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub Form_Closing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        sCode = txtKDAWALPEMERIKSAAN.Text.Trim.ToUpper
    End Sub
    Private Sub fn_ChangeFormState()
        cboProfesi.Properties.Items.Add("PERAWAT")
        cboProfesi.Properties.Items.Add("BIDAN")
        cboProfesi.Properties.Items.Add("FISIOTERAPI")
        cboProfesi.Properties.Items.Add("APOTEKER")
        cboProfesi.Properties.Items.Add("ANALIS")
        cboProfesi.Properties.Items.Add("RADIOGRAFER")
        cboProfesi.Properties.Items.Add("PETUGAS KHUSUS LAINNYA")
        cboProfesi.Properties.Items.Add("PSIKOLOG")
        cboProfesi.Properties.Items.Add("REFRAKSIONIS OPTISIEN")
        cboProfesi.Properties.Items.Add("TERAPI WICARA")
        cboProfesi.Properties.Items.Add("DOKTER RADIOLOGI")
        cboProfesi.Properties.Items.Add("PERAWAT RADIOLOGI")
        cboProfesi.Properties.Items.Add("TERAPIS GIGI DAN MULUT")

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
        chkALERGI_YA.Properties.ReadOnly = Status
        chkALERGI_TIDAK.Properties.ReadOnly = Status
        txtALERGI_TEXT.Properties.ReadOnly = Status
        txtNAMAPASIEN.Properties.ReadOnly = True
        deDATETANGGALLAHIR.Properties.ReadOnly = True
        txtKDCUSTOMER.Properties.ReadOnly = True
        txtSUBJEKTIF.Properties.ReadOnly = Status
        txtOBJEKTIF_JENISKELAMIN.Properties.ReadOnly = True
        txtOBJEKTIF_UMUR.Properties.ReadOnly = True
        txtOBJEKTIF_BERATBADAN.Properties.ReadOnly = Status
        txtOBJEKTIF_TINGGIBADAN.Properties.ReadOnly = Status
        txtOBJEKTIF_TEKANANDARAH.Properties.ReadOnly = Status
        txtOBJEKTIF_NADI.Properties.ReadOnly = Status
        txtOBJEKTIF_RESPIRASI.Properties.ReadOnly = Status
        txtOBJEKTIF_SATURASIOKSIGEN.Properties.ReadOnly = Status
        txtOBJEKTIF_SUHU.Properties.ReadOnly = Status
        txtDESKRIPSI.Properties.ReadOnly = Status
        txtANTRIANDOKTER.Properties.ReadOnly = Status
        cboProfesi.Properties.ReadOnly = Status
        txtAssesment.Properties.ReadOnly = Status
        txtPlanning.Properties.ReadOnly = Status
    End Sub
    Private Sub fn_EmptyMe()
        cboProfesi.SelectedIndex = 0
        txtAssesment.ResetText()
        txtPlanning.ResetText()
        'txtKDAWALPEMERIKSAAN.Text = "<--- AUTO --->"
        txtKDAWALPEMERIKSAAN.Text = sNoId
        deDATE.DateTime = Now
        chkALERGI_YA.Checked = False
        chkALERGI_TIDAK.Checked = False
        txtALERGI_TEXT.ResetText()
        txtNAMAPASIEN.ResetText()
        deDATETANGGALLAHIR.ResetText()
        txtKDCUSTOMER.ResetText()
        txtSUBJEKTIF.ResetText()
        txtOBJEKTIF_JENISKELAMIN.ResetText()
        txtOBJEKTIF_UMUR.ResetText()
        txtOBJEKTIF_BERATBADAN.ResetText()
        txtOBJEKTIF_TINGGIBADAN.ResetText()
        txtOBJEKTIF_TEKANANDARAH.ResetText()
        txtOBJEKTIF_NADI.ResetText()
        txtOBJEKTIF_RESPIRASI.ResetText()
        txtOBJEKTIF_SATURASIOKSIGEN.ResetText()
        txtOBJEKTIF_SUHU.ResetText()
        txtNAMAPASIEN.Text = sNama
        deDATETANGGALLAHIR.DateTime = sTanggalLahir
        txtKDCUSTOMER.Text = sNoRekamMedis
        txtOBJEKTIF_UMUR.Text = GetUmurPasien(Now, sTanggalLahir)
        txtOBJEKTIF_JENISKELAMIN.Text = sJK
        txtDESKRIPSI.ResetText()

        'Dim oCounter As New Setting.clsCounter
        'Dim sMODUL As String = sKodeDokter
        'Dim sLASTNUMBER As Integer = oCounter.GetLastNumberdDay(sMODUL, deDATE.DateTime)
        'If sLASTNUMBER = 0 Then
        '    Try
        '        oCounter.InsertData(sMODUL, deDATE.DateTime)
        '        sLASTNUMBER = oCounter.GetLastNumber(sMODUL, deDATE.DateTime)
        '    Catch ex As Exception
        '        sLASTNUMBER = 0
        '    End Try

        '    txtANTRIANDOKTER.Text = sLASTNUMBER + 1
        '    oCounter.UpdateData(sMODUL, sLASTNUMBER + 1, CInt(deDATE.DateTime.ToString("dd")), Month(deDATE.DateTime), Year(deDATE.DateTime))
        'Else
        '    txtANTRIANDOKTER.Text = sLASTNUMBER + 1
        '    oCounter.UpdateData(sMODUL, sLASTNUMBER + 1, CInt(deDATE.DateTime.ToString("dd")), Month(deDATE.DateTime), Year(deDATE.DateTime))
        'End If

        txtANTRIANDOKTER.Text = fn_LoadNoAntrianDokter(sNoId)

        Try
            ' ***** HEADER *****
            Dim ds = oReqAwalPemeriksaan.GetDataLast(txtKDCUSTOMER.Text)

            If ds IsNot Nothing Then
                If MsgBox("Apakah akan di load data pasien?", MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, Me.Text) = MsgBoxResult.No Then Exit Sub

                With ds
                    chkALERGI_YA.Checked = .ALERGI_YA
                    chkALERGI_TIDAK.Checked = .ALERGI_TIDAK
                    txtALERGI_TEXT.Text = .ALERGI_TEXT
                    txtOBJEKTIF_BERATBADAN.Text = IIf(CDec(.OBJEKTIF_BERATBADAN) = 0, "", .OBJEKTIF_BERATBADAN)
                    txtOBJEKTIF_TINGGIBADAN.Text = IIf(CDec(.OBJEKTIF_TINGGIBADAN) = 0, "", .OBJEKTIF_TINGGIBADAN)
                    txtOBJEKTIF_TEKANANDARAH.Text = .OBJEKTIF_TEKANANDARAH
                End With
            End If
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Public Function GetUmurPasien(ByVal dateNow As Date, ByVal tgllahir As Date) As String
        Dim years As Long
        Dim months As Long
        Dim days As Long
        Dim yearWord As String
        Dim monthWord As String
        Dim dayWord As String

        ' menghitung tahun
        years = DateDiff("yyyy", tgllahir, dateNow)
        If Month(tgllahir) > Month(dateNow) Then
            years = years - 1
        ElseIf Month(tgllahir) = Month(dateNow) And tgllahir.Day > dateNow.Day Then
            years = years - 1
        ElseIf Month(tgllahir) = Month(dateNow) And tgllahir.Day = dateNow.Day Then
            'GoTo Finish ' jika bulan dan tanggal sama maka perhitungan selesai
        End If
        ' menghitung bulan
        tgllahir = DateAdd("yyyy", years, tgllahir)
        months = DateDiff("m", tgllahir, dateNow)
        If tgllahir.Day > dateNow.Day Then
            months = months - 1
        ElseIf Month(tgllahir) = Month(dateNow) And tgllahir.Day >= dateNow.Day Then
            months = months - 1
        End If
        tgllahir = DateAdd("m", months, tgllahir)
        ' menghitung hari
        days = DateDiff("d", tgllahir, dateNow)

        yearWord = IIf(years = 0, "", years & " Tahun ")
        monthWord = IIf(months = 0, "", months & " Bulan ")
        dayWord = IIf(days = 0, "", days & " Hari ")
        'calculateAge = yearWord & monthWord & dayWord
        'calculateAge = Trim(calculateAge)

        'GetUmurPasien = yearWord & " " & monthWord & " " & dayWord
        GetUmurPasien = yearWord
    End Function
    Private Sub fn_LoadData()
        Try
            ' ***** HEADER *****
            Dim ds = oReqAwalPemeriksaan.GetData(sNoId)

            With ds
                txtKDAWALPEMERIKSAAN.Text = .KDAWALPEMERIKSAAN
                deDATE.DateTime = .DATE
                chkALERGI_YA.Checked = .ALERGI_YA
                chkALERGI_TIDAK.Checked = .ALERGI_TIDAK
                txtALERGI_TEXT.Text = .ALERGI_TEXT
                txtNAMAPASIEN.Text = .NAMAPASIEN
                deDATETANGGALLAHIR.DateTime = .TANGGALLAHIR
                txtKDCUSTOMER.Text = .KDCUSTOMER
                txtSUBJEKTIF.Text = .SUBJEKTIF
                txtOBJEKTIF_JENISKELAMIN.Text = .OBJEKTIF_JENISKELAMIN
                txtOBJEKTIF_UMUR.Text = .OBJEKTIF_UMUR
                txtOBJEKTIF_BERATBADAN.Text = IIf(CDec(.OBJEKTIF_BERATBADAN) = 0, "", .OBJEKTIF_BERATBADAN)
                txtOBJEKTIF_TINGGIBADAN.Text = IIf(CDec(.OBJEKTIF_TINGGIBADAN) = 0, "", .OBJEKTIF_TINGGIBADAN)
                txtOBJEKTIF_TEKANANDARAH.Text = .OBJEKTIF_TEKANANDARAH
                txtOBJEKTIF_NADI.Text = .OBJEKTIF_NADI
                txtOBJEKTIF_RESPIRASI.Text = .OBJEKTIF_RESPIRASI
                txtOBJEKTIF_SATURASIOKSIGEN.Text = .OBJEKTIF_SATURASIOKSIGEN
                txtOBJEKTIF_SUHU.Text = .OBJEKTIF_SUHU
                txtDESKRIPSI.Text = .DESKRIPSI
                txtANTRIANDOKTER.Text = .ANTRIANDOKTER
                cboProfesi.Text = .PROPESI
                txtAssesment.Text = .ASESMENT
                txtPlanning.Text = .PLANNING

                bindingSource.DataSource = oReqAwalPemeriksaan.GetDataDetail(sNoId).OrderBy(Function(x) x.SEQ).ToList()
                grdTindakanPoli.DataSource = bindingSource
            End With
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Function fn_Validate() As Boolean
        Try
            fn_Validate = True


            Try
                If txtOBJEKTIF_BERATBADAN.Text <> "" Then
                    Dim TES = CDec(txtOBJEKTIF_BERATBADAN.Text)
                End If
            Catch ex As Exception
                MsgBox("Berat Badan Harus Angka" & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
                fn_Validate = False
                Exit Function
            End Try

            Try
                If txtOBJEKTIF_TINGGIBADAN.Text <> "" Then
                    Dim TES = CDec(txtOBJEKTIF_TINGGIBADAN.Text)
                End If
            Catch ex As Exception
                MsgBox("Tinggi Badan Harus Angka" & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
                fn_Validate = False
                Exit Function
            End Try


            If txtSUBJEKTIF.Text = String.Empty Then
                txtSUBJEKTIF.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                txtSUBJEKTIF.ErrorText = Statement.ErrorRequired

                txtSUBJEKTIF.Focus()
                fn_Validate = False
                Exit Function
            End If

            If cboProfesi.Text = String.Empty Then
                cboProfesi.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                cboProfesi.ErrorText = Statement.ErrorRequired

                cboProfesi.Focus()
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
            Dim ds = oReqAwalPemeriksaan.GetStructureHeader
            With ds
                Try
                    .DATECREATED = oReqAwalPemeriksaan.GetData(sNoId).DATECREATED
                Catch oErr As Exception
                    .DATECREATED = Now
                End Try
                .DATEUPDATED = Now
                .DATE = deDATE.DateTime
                .KDAWALPEMERIKSAAN = sNoId
                .KDPENDAFTARAN = sNoId
                .ALERGI_YA = chkALERGI_YA.Checked
                .ALERGI_TIDAK = chkALERGI_TIDAK.Checked
                .ALERGI_TEXT = txtALERGI_TEXT.Text
                .NAMAPASIEN = txtNAMAPASIEN.Text
                .TANGGALLAHIR = deDATETANGGALLAHIR.DateTime
                .KDCUSTOMER = txtKDCUSTOMER.Text
                .SUBJEKTIF = txtSUBJEKTIF.Text
                .OBJEKTIF_JENISKELAMIN = txtOBJEKTIF_JENISKELAMIN.Text
                .OBJEKTIF_UMUR = txtOBJEKTIF_UMUR.Text
                .OBJEKTIF_TINGGIBADAN = IIf(txtOBJEKTIF_TINGGIBADAN.Text = "", 0, txtOBJEKTIF_TINGGIBADAN.Text.Replace(",", "."))
                .OBJEKTIF_BERATBADAN = IIf(txtOBJEKTIF_BERATBADAN.Text = "", 0, txtOBJEKTIF_BERATBADAN.Text.Replace(",", "."))
                .OBJEKTIF_TEKANANDARAH = txtOBJEKTIF_TEKANANDARAH.Text
                .OBJEKTIF_NADI = txtOBJEKTIF_NADI.Text
                .OBJEKTIF_RESPIRASI = txtOBJEKTIF_RESPIRASI.Text
                .OBJEKTIF_SATURASIOKSIGEN = txtOBJEKTIF_SATURASIOKSIGEN.Text
                .OBJEKTIF_SUHU = txtOBJEKTIF_SUHU.Text
                .KDUSER = sUserID
                .DESKRIPSI = txtDESKRIPSI.Text
                .ANTRIANDOKTER = txtANTRIANDOKTER.Text
                .ASESMENT = txtAssesment.Text
                .PLANNING = txtPlanning.Text
                .PROPESI = cboProfesi.Text
            End With

            Dim arrDetail = oReqAwalPemeriksaan.GetStructureDetailList
            For i As Integer = 0 To grvTindakanPoli.RowCount - 2
                Dim dsDetail = oReqAwalPemeriksaan.GetStructureDetail
                With dsDetail
                    .SEQ = i
                    .KDAWALPEMERIKSAAN = ds.KDAWALPEMERIKSAAN
                    .KETERANGAN = grvTindakanPoli.GetRowCellValue(i, colKETERANGAN_)
                End With
                arrDetail.Add(dsDetail)
            Next

            Dim dsTambahan = oReqAwalPemeriksaan.GetStructureHeaderTambahan

            With dsTambahan
                .KDAWALPEMERIKSAAN = ds.KDAWALPEMERIKSAAN
                .TAMBAH1 = txtKESADARAN_TAMBAH1.Text
                .TAMBAH2 = txtIMT_TAMBAH2.Text
                .TAMBAH3 = txtBBIDEAL_TAMBAH3.Text
                .TAMBAH4 = txtBBTURUN_TAMBAH4.Text
                .TAMBAH5 = ""
                .TAMBAH6 = ""
                .TAMBAH7 = ""
                .TAMBAH8 = ""
                .TAMBAH9 = ""
                .TAMBAH10 = ""
            End With

            If oFormMode = FORM_MODE.FORM_MODE_ADD Then
                Try
                    fn_Save = oReqAwalPemeriksaan.InsertData(ds, arrDetail, dsTambahan)
                Catch oErr As Exception
                    MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            ElseIf oFormMode = FORM_MODE.FORM_MODE_EDIT Then
                Try
                    fn_Save = oReqAwalPemeriksaan.UpdateData(ds, arrDetail, dsTambahan)
                Catch oErr As Exception
                    MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            End If

            Try
                fn_UpdateNoAntrianPerawat(sNoId)
            Catch ex As Exception

            End Try

            If cboProfesi.Text = "FISIOTERAPI" Then
                Dim oCPPT As New Transaksi.clsReqCPPT

                Dim dsCPPT = oCPPT.GetStructureHeader
                With dsCPPT
                    .DATECREATED = ds.DATECREATED
                    .DATEUPDATED = ds.DATEUPDATED
                    .DATE = ds.DATE
                    .KDCPPT = "CPPT" & ds.KDAWALPEMERIKSAAN
                    .KDCUSTOMER = ds.KDCUSTOMER
                    .KDPENDAFTARAN = ds.KDPENDAFTARAN
                    .PROFESI = cboProfesi.Text
                    .NAMAPASIEN = ds.NAMAPASIEN
                    .JK = ds.OBJEKTIF_JENISKELAMIN
                    .NIK = ""
                    .TEMPATLAHIR = ""
                    .TANGGALLAHIR = ds.TANGGALLAHIR
                    .AGAMA = ""
                    .PENJAMIN = "AUTO"
                    .NOTELEPON = ""
                    .SUKU = ""
                    .ALAMAT = ""
                    .SUBJEKTIF = ds.SUBJEKTIF

                    Dim listSUBOBJEKTIF As New List(Of String)
                    'If entityPemeriksaan.OBJEKTIF_JENISKELAMIN <> "" Then
                    '    listSUBJEKTIF.Add("Jenis Kelamin : " & entityPemeriksaan.OBJEKTIF_JENISKELAMIN)
                    'End If
                    If ds.OBJEKTIF_UMUR <> "" Then
                        listSUBOBJEKTIF.Add("Umur : " & ds.OBJEKTIF_UMUR)
                    End If
                    If ds.OBJEKTIF_BERATBADAN <> "" Then
                        If CDec(ds.OBJEKTIF_BERATBADAN) <> 0 Then
                            listSUBOBJEKTIF.Add("BB : " & ds.OBJEKTIF_BERATBADAN & " kg")
                            '0.00
                        End If
                    End If
                    If ds.OBJEKTIF_TINGGIBADAN <> "" Then
                        If CDec(ds.OBJEKTIF_TINGGIBADAN) <> 0 Then
                            listSUBOBJEKTIF.Add("TT : " & ds.OBJEKTIF_TINGGIBADAN & " cm")
                        End If
                    End If
                    If ds.OBJEKTIF_TEKANANDARAH <> "" Then
                        listSUBOBJEKTIF.Add("Tekanan Darah : " & ds.OBJEKTIF_TEKANANDARAH & " mmHg")
                    End If
                    If ds.OBJEKTIF_NADI <> "" Then
                        listSUBOBJEKTIF.Add("Nadi : " & ds.OBJEKTIF_NADI & " x/mnt")
                    End If
                    If ds.OBJEKTIF_RESPIRASI <> "" Then
                        listSUBOBJEKTIF.Add("Respirasi : " & ds.OBJEKTIF_RESPIRASI & " x/mnt")
                    End If
                    If ds.OBJEKTIF_SATURASIOKSIGEN <> "" Then
                        listSUBOBJEKTIF.Add("Saturasi Oksigen : " & ds.OBJEKTIF_SATURASIOKSIGEN & " %")
                    End If
                    If ds.OBJEKTIF_SUHU <> "" Then
                        listSUBOBJEKTIF.Add("Suhu : " & ds.OBJEKTIF_SUHU & " oC")
                    End If
                    If ds.DESKRIPSI <> "" Then
                        listSUBOBJEKTIF.Add("Pemeriksaan : " & ds.DESKRIPSI)
                    End If

                    .OBJEKTIF = String.Join(vbCrLf, listSUBOBJEKTIF.ToArray)
                    .ASSEMENT = txtAssesment.Text
                    .PLANNING = txtPlanning.Text
                    .KDUSER = ds.KDUSER
                    .ISCHEKED = False

                    .KETERANGAN_1 = ""
                    .KETERANGAN_2 = ""
                    .KETERANGAN_3 = ""

                    .HANDOVERPEMBERI_TANGGAL = ""
                    .HANDOVERPEMBERI_KDUSER = ""
                    .HANDOVERPENERIMA_TANGGAL = ""
                    .HANDOVERPENERIMA_KDUSER = ""

                    .NILAIKRITISPEMBERI_TANGGAL = ""
                    .NILAIKRITISPEMBERI_KDUSER = ""
                    .NILAIKRITISPENERIMA_TANGGAL = ""
                    .NILAIKRITISPENERIMA_KDUSER = ""

                    .SBARPEMBERI_TANGGAL = ""
                    .SBARPEMBERI_KDUSER = ""
                    .SBARPENERIMA_TANGGAL = ""
                    .SBARPENERIMA_KDUSER = ""
                End With

                Dim dsCPPTCek = oCPPT.GetData("CPPT" & ds.KDAWALPEMERIKSAAN)

                If dsCPPTCek Is Nothing Then
                    Try
                        oCPPT.InsertData(dsCPPT, Nothing)
                    Catch oErr As Exception
                        MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
                    End Try
                Else
                    Try
                        oCPPT.UpdateData(dsCPPT, Nothing)
                    Catch oErr As Exception
                        MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
                    End Try
                End If
            End If
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            fn_Save = False
        End Try
    End Function
    Private Function fn_LoadNoAntrianDokter(ByVal KDREG As String) As Integer
        Try
            fn_LoadNoAntrianDokter = 0

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

            SQL = "SELECT * FROM S_PENDAFTARAN_H "
            SQL &= "WHERE KDREG = '" & KDREG & "' "

            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "ANTRIAN_DOKTER")

            If oConn.State = ConnectionState.Open Then
                oConn.Close()
            End If

            For xLoop As Integer = 0 To ds.Tables("ANTRIAN_DOKTER").Rows.Count - 1
                With ds.Tables("ANTRIAN_DOKTER")
                    fn_LoadNoAntrianDokter = .Rows(xLoop)("ANTRIANDOKTER")
                End With
            Next

        Catch oErr As Exception
            fn_LoadNoAntrianDokter = 0
            MsgBox("Update Antrian Pasien Data : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Function
    Public Sub fn_UpdateNoAntrianPerawat(ByVal KDREG As String)
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

            SQL = "UPDATE S_PENDAFTARAN_H "
            SQL &= "SET ISPERAWAT = 1 "
            SQL &= "WHERE KDREG = '" & KDREG & "' "

            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "updateantrian")

            If oConn.State = ConnectionState.Open Then
                oConn.Close()
            End If

        Catch oErr As Exception
            MsgBox("Update Antrian Pasien Data : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub DeleteToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DeleteToolStripMenuItem.Click
        grvTindakanPoli.DeleteSelectedRows()
    End Sub
#End Region
#Region "Grid Method"

#End Region
#Region "Command Button"
    Private Sub frmReqAwalPemeriksaan_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
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
    Private Sub chkALERGI_YA_CheckedChanged(sender As Object, e As EventArgs) Handles chkALERGI_YA.CheckedChanged
        If isLoad = True Then
            'If chkALERGI_YA.Checked = False Then
            '    chkALERGI_TIDAK.Checked = True
            'End If
        End If
    End Sub
    Private Sub btnHitung_Click(sender As Object, e As EventArgs) Handles btnHitung.Click
        Try
            Dim berat As Double = CDec(txtOBJEKTIF_BERATBADAN.Text)
            Dim tinggi As Double = CDec(txtOBJEKTIF_TINGGIBADAN.Text) / 100
            Dim imt As Double = berat / (tinggi * tinggi)

            txtIMT_TAMBAH2.Text = FormatNumber(imt, 2)

            txtBBIDEAL_TAMBAH3.Text = FormatNumber((berat / imt) * 18.5, 2) & " - " & FormatNumber((berat / imt) * 25, 2)

        Catch oErr As Exception
            MsgBox("Hitung : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub

#End Region
#Region "Lookup / Event"

#End Region
End Class