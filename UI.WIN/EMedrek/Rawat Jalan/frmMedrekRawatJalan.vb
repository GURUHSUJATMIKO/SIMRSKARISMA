Imports DataAccess
Imports System.Linq
Imports System.Data.SqlClient
Imports Newtonsoft.Json.Linq

Public Class frmMedrekRawatJalan
#Region "Declaration"
    Private oFormMode As FORM_MODE = FORM_MODE.FORM_MODE_VIEW
    Private isLoad As Boolean = False
    Private sNoid As String = String.Empty
    Private ssKODEBOOKING As String = String.Empty
    Private sKDDEPARTMENT As String = String.Empty
    Private sKDDOCTOR As String = String.Empty
    Private sNOMORSEP As String = String.Empty
    Private sKARTUBPJS As String = String.Empty
    Private sTANGGALLAHIR As DateTime = Now
    Private sDIAGNOSA As String = String.Empty
    Private sALAMATSIMPAN As String = String.Empty
    Private oReqAwalPemeriksaan As New Transaksi.clsReqAwalPemeriksaan
    Private sJK As String = String.Empty
    Private sRegister As String = String.Empty
    Private sPenjamin As String = String.Empty
    Private sDPJP As String = String.Empty
    Private sTUJUAN As String = String.Empty
    Private sSIPDOKTER As String = String.Empty
#End Region
#Region "Function"
    Public Sub fn_LoadIdentias(ByVal BERATBADAN As Decimal, ByVal TINGGIBADAN As Decimal, ByVal ALERGI As String, ByVal KDPENDAFTRAN As String, ByVal usia As String, ByVal sPASIEN As String, ByVal sKDCUSTOMER As String, ByVal DOKTER As String, ByVal TUJUAN As String, ByVal SIPDOKTER As String, ByVal PENJAMIN As String, ByVal KODEBOOKING As String, ByVal KDDEPARTMENT As String, ByVal KDDOCTOR As String, ByVal NOMORSEP As String, ByVal KARTUBPJS As String, ByVal DIAGNOSA As String, ByVal TANGGALLAHIR As DateTime, ByVal JK As String)
        'lblNMOR.Text = NOMORANTRIANDOKTER
        txtOBJEKTIF_BERATBADAN.Text = BERATBADAN
        txtOBJEKTIF_TINGGIBADAN.Text = TINGGIBADAN
        txtALERGIOBAT.Text = ALERGI
        txtNAMAPASIEN.Text = sPASIEN
        txtOBJEKTIF_UMUR.Text = GetUmurPasien(Now, TANGGALLAHIR)
        sJK = IIf(JK = 1, "Laki-laki", "Perempuan")
        Dim ds = oReqAwalPemeriksaan.GetData(sNoid)
        If ds IsNot Nothing Then
            oFormMode = FORM_MODE.FORM_MODE_EDIT
        End If
        txtKDCUSTOMER.Text = sKDCUSTOMER
        'lblRegister.Text = KDPENDAFTRAN
        'lblDPJP.Text = sDOKTER
        'lblTUJUAN.Text = sTUJUAN
        'lblSIPDOKTER.Text = sSIPDOKTER
        'lblPenjamin.Text = sPENJAMIN
        ssKODEBOOKING = KODEBOOKING
        sKDDEPARTMENT = KDDEPARTMENT
        sKDDOCTOR = KDDOCTOR
        sNOMORSEP = NOMORSEP
        sKARTUBPJS = KARTUBPJS
        sTANGGALLAHIR = TANGGALLAHIR
        sDIAGNOSA = DIAGNOSA

        sRegister = KDPENDAFTRAN
        sPenjamin = PENJAMIN
        sDPJP = DOKTER
        sTUJUAN = TUJUAN
        sSIPDOKTER = SIPDOKTER
    End Sub
    Private Function GetUmurPasien(ByVal dateNow As Date, ByVal tgllahir As Date) As String
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
    Public Sub LoadMe(ByVal FormMode As Integer, Optional ByVal NoId As String = "")
        oFormMode = FormMode
        sNoid = NoId
        Me.Text = "Medrek Rawat Jalan - " & IIf(oFormMode = FORM_MODE.FORM_MODE_ADD, "Add", "Update") & " Form"
        Dim dsAlamat = oReqAwalPemeriksaan.GetDataAlamatSimpan()
        If dsAlamat IsNot Nothing Then
            sALAMATSIMPAN = dsAlamat.ALAMAT_SERVER
        Else
            MsgBox("Load Data Data : " & vbCrLf & "Alamat simpan gambar tidak ada", MsgBoxStyle.Exclamation, Me.Text)
            Me.Close()
        End If
    End Sub
    Private Sub Form_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        fn_ChangeFormState()
        isLoad = True
        'grdDetailResep.Focus()
        'sPRB = ""
        txtDIAGNOOSA.Focus()
    End Sub
    Private Sub Form_Closing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        'sCode = txtCODE.Text.Trim.ToUpper
    End Sub
    Private Overloads Sub Dispose()
        MyBase.Dispose()
        Me.Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
    Private Sub fn_LoadDataList()
        Try
            grvListPasien.OptionsSelection.MultiSelect = True
            grvListPasien.SelectAll()
            grvListPasien.DeleteSelectedRows()
            grvListPasien.OptionsSelection.MultiSelect = False

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

            'fn_LoadIdentitas(grvListPasien.GetFocusedRowCellValue("NoRegister"), grvListPasien.GetFocusedRowCellValue("Antrian"), grvListPasien.GetFocusedRowCellValue("CATEGORY"), grvListPasien.GetFocusedRowCellValue("NoRegister"), grvListPasien.GetFocusedRowCellValue("NoRegister"), grvListPasien.GetFocusedRowCellValue("NoRegister"), grvListPasien.GetFocusedRowCellValue("NoRegister"), grvListPasien.GetFocusedRowCellValue("NoRegister"), grvListPasien.GetFocusedRowCellValue("NoRegister"), grvListPasien.GetFocusedRowCellValue("NoRegister"), grvListPasien.GetFocusedRowCellValue("NoRegister"))

            SQL = "SELECT "
            SQL &= "* "
            SQL &= "FROM ( "
            SQL &= "SELECT "
            'SQL &= "NoRegister = B.KDREG "
            SQL &= "AntrianDokter = (SELECT CASE B.ISRESUME WHEN 0 THEN (SELECT CASE B.ANTRIANDOKTER WHEN 0 THEN 'X' ELSE 'A' + RIGHT('000' + CONVERT(NVARCHAR(50), B.ANTRIANDOKTER), 3) END) ELSE 'Y' END) "
            'SQL &= ",Antrian = B.NOMORANTRIAN "
            SQL &= ",Pasien = D.NAME_DISPLAY "
            'SQL &= ",Dokter = C.NAME_DISPLAY "
            'SQL &= ",SIP = C.SIP "
            'SQL &= ",B.KDCUSTOMER "
            'SQL &= ",B.USIA "
            'SQL &= ",D.TANGGALLAHIR "
            'SQL &= ",DIAGNOSA = E.KDDIAGNOSA + ' - ' + E.DESCRIPTION "
            'SQL &= ",B.DATE "
            'SQL &= ",PENJAMIN = F.NAME_DISPLAY "
            'SQL &= ",B.NOMORSEP "
            'SQL &= ",B.KARTUBPJS "
            'SQL &= ",Cek = B.ISRESUME "
            'SQL &= ",C.KDDOCTOR "
            'SQL &= ",D.JK "
            SQL &= "FROM "
            SQL &= "S_PENDAFTARAN_H B "
            SQL &= "INNER JOIN M_DOCTOR C "
            SQL &= "ON B.KDDOCTOR = C.KDDOCTOR "
            SQL &= "INNER JOIN M_CUSTOMER D "
            SQL &= "ON B.KDCUSTOMER = D.KDCUSTOMER "
            SQL &= "INNER JOIN M_DIAGNOSAICD10 E "
            SQL &= "ON B.KDDIAGNOSA = E.KDDIAGNOSA "
            SQL &= "INNER JOIN M_DEBTOR F "
            SQL &= "ON B.KDDEBTOR = F.KDDEBTOR "
            SQL &= "WHERE CONVERT(VARCHAR(8), B.DATE, 112) = '" & Now.ToString("yyyyMMdd") & "' "
            SQL &= "AND B.KDDEPARTMENT = " & sKDDEPARTMENT & " "
            SQL &= "AND B.KDDOCTOR = " & sKDDOCTOR & " "
            SQL &= ") Z "
            SQL &= "ORDER BY Z.ANTRIANDOKTER ASC "

            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "S_LISTPASIEN")

            grdListPasien.DataSource = ds.Tables("S_LISTPASIEN")
            grdListPasien.ForceInitialize()

            If oConn.State = ConnectionState.Open Then
                oConn.Close()
            End If

            'fn_LoadFormatData()

            grvListPasien.BestFitColumns()

        Catch oErr As Exception
            MsgBox("Load List Pasien Data : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub grvListPasien_RowStyle(sender As System.Object, e As DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs) Handles grvListPasien.RowStyle
        If grvListPasien.IsFilterRow(e.RowHandle) Then Exit Sub
        If grvListPasien.GetRowCellValue(e.RowHandle, "Cek") = True Then
            e.Appearance.BackColor = Color.LawnGreen
            'Else
            '    e.Appearance.BackColor = Color.Red
        Else
            If grvListPasien.GetRowCellValue(e.RowHandle, "AntrianDokter") <> "X" Then
                e.Appearance.BackColor = Color.Orange
            End If
        End If
    End Sub
    Private Sub fn_ChangeFormState()
        fn_LoadTEMPLATE()
        fn_LoadITEM()
        fn_LoadUOM()
        fn_LoadSIGNA()
        fn_LoadCARAPAKAI()
        fn_LoadDataList()

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

        txtDIAGNOOSA.Properties.ReadOnly = Status
        txtTINDAKLANJUT.ReadOnly = Status

        deDATE.Properties.ReadOnly = Status
        chkALERGI_YA.Properties.ReadOnly = Status
        chkALERGI_TIDAK.Properties.ReadOnly = Status
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
    End Sub
    Private Sub fn_EmptyMe()
        'txtCODE.Text = "<--- AUTO --->"
        sPicture = Nothing
        chkALERGI_YA.Checked = False
        chkALERGI_TIDAK.Checked = False
        deDATE.DateTime = Now
        txtDIAGNOOSA.Text = sDIAGNOSA
        'txtNAMAPASIEN.ResetText()
        deDATETANGGALLAHIR.DateTime = sTANGGALLAHIR
        'txtKDCUSTOMER.ResetText()
        'txtSUBJEKTIF.ResetText()
        txtOBJEKTIF_JENISKELAMIN.Text = sJK
        'txtOBJEKTIF_UMUR.ResetText()
        'txtOBJEKTIF_BERATBADAN.ResetText()
        'txtOBJEKTIF_TINGGIBADAN.ResetText()
        'txtOBJEKTIF_TEKANANDARAH.ResetText()
        'txtOBJEKTIF_NADI.ResetText()
        'txtOBJEKTIF_RESPIRASI.ResetText()
        'txtOBJEKTIF_SATURASIOKSIGEN.ResetText()
        'txtOBJEKTIF_SUHU.ResetText()
        'txtDESKRIPSI.ResetText()
        txtALAMATGAMBAR.ResetText()

        Dim dsReqAwalPemeriksaan = oReqAwalPemeriksaan.GetData(sRegister)

        If dsReqAwalPemeriksaan IsNot Nothing Then
            chkALERGI_YA.Checked = dsReqAwalPemeriksaan.ALERGI_YA
            chkALERGI_TIDAK.Checked = dsReqAwalPemeriksaan.ALERGI_TIDAK
            txtALERGIOBAT.Text = dsReqAwalPemeriksaan.ALERGI_TEXT
            txtNAMAPASIEN.Text = dsReqAwalPemeriksaan.NAMAPASIEN
            deDATETANGGALLAHIR.DateTime = dsReqAwalPemeriksaan.TANGGALLAHIR
            txtKDCUSTOMER.Text = dsReqAwalPemeriksaan.KDCUSTOMER
            txtSUBJEKTIF.Text = dsReqAwalPemeriksaan.SUBJEKTIF
            txtOBJEKTIF_JENISKELAMIN.Text = dsReqAwalPemeriksaan.OBJEKTIF_JENISKELAMIN
            txtOBJEKTIF_UMUR.Text = dsReqAwalPemeriksaan.OBJEKTIF_UMUR
            txtOBJEKTIF_BERATBADAN.Text = IIf(CDec(dsReqAwalPemeriksaan.OBJEKTIF_BERATBADAN) = 0, "", dsReqAwalPemeriksaan.OBJEKTIF_BERATBADAN)
            txtOBJEKTIF_TINGGIBADAN.Text = IIf(CDec(dsReqAwalPemeriksaan.OBJEKTIF_TINGGIBADAN) = 0, "", dsReqAwalPemeriksaan.OBJEKTIF_TINGGIBADAN)
            txtOBJEKTIF_TEKANANDARAH.Text = dsReqAwalPemeriksaan.OBJEKTIF_TEKANANDARAH
            txtOBJEKTIF_NADI.Text = dsReqAwalPemeriksaan.OBJEKTIF_NADI
            txtOBJEKTIF_RESPIRASI.Text = dsReqAwalPemeriksaan.OBJEKTIF_RESPIRASI
            txtOBJEKTIF_SATURASIOKSIGEN.Text = dsReqAwalPemeriksaan.OBJEKTIF_SATURASIOKSIGEN
            txtOBJEKTIF_SUHU.Text = dsReqAwalPemeriksaan.OBJEKTIF_SUHU
            txtDESKRIPSI.Text = dsReqAwalPemeriksaan.DESKRIPSI

            For Each xloop In oReqAwalPemeriksaan.GetDataDetail(sRegister)
                grvTindakanPoli.Focus()
                grvTindakanPoli.AddNewRow()
                grvTindakanPoli.SetFocusedRowCellValue(colKETERANGAN_, xloop.KETERANGAN)
                grvTindakanPoli.UpdateCurrentRow()
            Next
        End If

        'grvTindakanPoli.Focus()
        grvTindakanPoli.AddNewRow()
        grvTindakanPoli.SetFocusedRowCellValue(colKETERANGAN_, "Konsultasi Medis")
        grvTindakanPoli.UpdateCurrentRow()
    End Sub
    Private Sub fn_LoadData()
        Try
            Dim oReq_Recipe As New Transaksi.clsReq_Recipe
            Dim oKoding As New Admission.clsKoding
            Dim oReqAkhirPemeriksaan As New Transaksi.clsReqAkhirPemeriksaan

            ' ***** HEADER *****
            Dim ds = oReq_Recipe.GetData(sNoid)

            With ds
                txtDIAGNOOSA.Text = .DIAGNOSA
                deDATE.DateTime = .DATE

                Dim dsKoding = oKoding.GetData(sNoid)
                If dsKoding IsNot Nothing Then
                    Try
                        txtTINDAKLANJUT.Text = dsKoding.DESCRIPTION
                        'txtTINDAKLANJUT.Text = txtTINDAKLANJUT.Rtf
                    Catch ex As Exception

                    End Try
                End If

                txtGRANDTOTAL.Text = .GRANDTOTAL

                BindingSource.DataSource = oReq_Recipe.GetDataDetail(sNoid).Where(Function(x) x.KDITEM <> "").OrderBy(Function(x) x.SEQ).ToList()
                grdDetailResep.DataSource = BindingSource

                BindingSource1.DataSource = oReq_Recipe.GetDataDetailDiagnosaPenyerta(sNoid).OrderBy(Function(x) x.SEQ).ToList()
                grdDiagnosaPenyerta.DataSource = BindingSource1

                BindingSource2.DataSource = oReq_Recipe.GetDataDetailTindakan(sNoid).Where(Function(x) x.TINDAKAN <> "").OrderBy(Function(x) x.SEQ).ToList()
                grdTindakan.DataSource = BindingSource2

                BindingSource3.DataSource = oReq_Recipe.GetDataDetailTindakan_(sNoid).Where(Function(x) x.KETERANGAN <> "").OrderBy(Function(x) x.SEQ).ToList()
                grdTindakanPoli.DataSource = BindingSource3

                BindingSource4.DataSource = oReq_Recipe.GetDataDetailRacikan(sNoid).OrderBy(Function(x) x.SEQ).ToList()
                grdOBATRACIKAN.DataSource = BindingSource4
            End With

            Dim dsAkhir = oReqAkhirPemeriksaan.GetData(sNoid)
            If dsAkhir IsNot Nothing Then
                With dsAkhir
                    chkALERGI_YA.Checked = .ALERGI_YA
                    chkALERGI_TIDAK.Checked = .ALERGI_TIDAK
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
                    txtALAMATGAMBAR.Text = .ALAMATGAMBAR

                    If .ALAMATGAMBAR <> "" Then
                        picGAMBAR2.Image = Image.FromFile(txtALAMATGAMBAR.Text)
                        sPicture = picGAMBAR2.Image
                    Else
                        sPicture = Nothing
                    End If
                End With
            Else
                deDATETANGGALLAHIR.DateTime = sTANGGALLAHIR
            End If

        Catch oErr As Exception
            MsgBox("Load Data Data : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
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

            If sRegister = String.Empty Then
                MsgBox("Dibutuhkan Register", MsgBoxStyle.Exclamation, Me.Text)
                'lblRegister.Focus()
                fn_Validate = False
                Exit Function
            End If

            If txtDIAGNOOSA.Text = "----" Then
                MsgBox("Dibutuhkan Diagnosa", MsgBoxStyle.Exclamation, Me.Text)
                txtDIAGNOOSA.Focus()
                fn_Validate = False
                Exit Function
            End If
            If txtDIAGNOOSA.Text = "- - -" Then
                MsgBox("Dibutuhkan Diagnosa", MsgBoxStyle.Exclamation, Me.Text)
                txtDIAGNOOSA.Focus()
                fn_Validate = False
                Exit Function
            End If
            'If sKDDPJP = 0 Then
            '    MsgBox("Dibutuhkan DPJP", MsgBoxStyle.Exclamation, Me.Text)
            '    fn_Validate = False
            '    Exit Function
            'End If
            'If sKDDEPARTMENT = 0 Then
            '    MsgBox("Dibutuhkan Poli", MsgBoxStyle.Exclamation, Me.Text)
            '    fn_Validate = False
            '    Exit Function
            'End If
            If sRegister = String.Empty Then
                MsgBox("Dibutuhkan Register", MsgBoxStyle.Exclamation, Me.Text)
                'lblRegister.Focus()
                fn_Validate = False
                Exit Function
            End If
            If txtDIAGNOOSA.Text = String.Empty Then
                MsgBox("Dibutuhkan Diagnosa", MsgBoxStyle.Exclamation, Me.Text)
                txtDIAGNOOSA.Focus()
                fn_Validate = False
                Exit Function
            End If
            'grvDetail_DX.UpdateCurrentRow()

            'If grvDetail_DX.RowCount <2 Then
            '    MsgBox("Dibutuhkan Diagnosa", MsgBoxStyle.Exclamation, Me.Text)
            '    fn_Validate = False
            '    Exit Function
            'End If

            'If chkIsRencanaKontrol.Checked = True Then
            '    If sPenjamin = "BPJS KESEHATAN" Then
            '        Dim oSKD As New Admission.clsSKD

            '        Dim dsSKD = oSKD.GetDataLastCustomer(sNOMORRUJUKAN, sDATE)

            '        If dsSKD IsNot Nothing Then
            '            MsgBox("Sudah Pernah di Input dengan Hari yang sama, Silahkan perbaiki data Surat Kontrol Dokter ", MsgBoxStyle.Exclamation, Me.Text)
            '            fn_Validate = False
            '            Exit FunctionV         
            '        End If

            '        Dim Bulan As Integer = 0

            '        Bulan = DateDiff(DateInterval.Month, sDATERUJUKAN, deDATEKONTROL.DateTime)

            '        If Bulan > 3 Then
            '            MsgBox("Nomor Rujukan sudah lebih dari 3 bulan", MsgBoxStyle.Exclamation, Me.Text)
            '            fn_Validate = False
            '            Exit Function
            '        End If

            '        Dim sTanggalHariIni As String = String.Empty
            '        sTanggalHariIni = Now.ToString("yyyyMMdd")

            '        If deDATEKONTROL.DateTime.ToString("yyyyMMdd") = sTanggalHariIni Or sDATE.ToString("yyyyMMdd") = deDATEKONTROL.DateTime.ToString("yyyyMMdd") Then
            '            MsgBox("Tanggal Kontrol Harap Rubah !!!", MsgBoxStyle.Exclamation, Me.Text)
            '            deDATEKONTROL.Focus()
            '            fn_Validate = False
            '            Exit Function
            '        End If
            '    End If
            'End If
        Catch oErr As Exception
            MsgBox("Validate Data : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Function
    Private Function fn_SaveCustomer_Detil() As Boolean
        Try
            fn_SaveCustomer_Detil = True

            Dim oCustomer_Detil As New Reference.clsCustomerDetil
            ' ***** HEADER *****
            Dim ds = oCustomer_Detil.GetStructureHeader
            With ds
                .DATECREATED = Now
                .DATEUPDATED = Now
                .KDCUSTOMER = txtKDCUSTOMER.Text
                .BB = IIf(txtOBJEKTIF_BERATBADAN.Text = "", 0, txtOBJEKTIF_BERATBADAN.Text.Replace(",", "."))
                If txtOBJEKTIF_TINGGIBADAN.Text = "" Then
                    .TT = 0
                Else
                    .TT = txtOBJEKTIF_TINGGIBADAN.Text.Replace(",", ".")
                End If

                .ALERGI = txtALERGIOBAT.Text
                .DIAGNOSA = txtDIAGNOOSA.Text
            End With

            Dim dsCsutomer_Detil = oCustomer_Detil.GetData(txtKDCUSTOMER.Text)

            If dsCsutomer_Detil Is Nothing Then
                oCustomer_Detil.InsertData(ds)
            Else
                oCustomer_Detil.UpdateData(ds)
            End If

        Catch oErr As Exception
            MsgBox("Simpan Data Save Customer : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            fn_SaveCustomer_Detil = False
        End Try
    End Function
    Private Function fn_Save() As Boolean
        Try
            Dim sTindakan As Integer = 0
            Dim sTerapi As Integer = 0

            Dim oReq_Recipe As New Transaksi.clsReq_Recipe
            ' ***** HEADER *****
            Dim ds = oReq_Recipe.GetStructureHeader
            With ds
                Try
                    .DATECREATED = oReq_Recipe.GetData(sNoid).DATECREATED
                Catch ex As Exception
                    .DATECREATED = Now
                End Try
                .DATEUPDATED = Now
                .DATE = deDATE.DateTime
                .KDREQRECIPE = sNoid
                .NOANTRIAN = String.Empty
                .KDPENDAFTARAN = sRegister
                .KDWAREHOUSE = 1
                .ALERGIOBAT = IIf(txtALERGIOBAT.Text.ToString.Trim = String.Empty, "-", txtALERGIOBAT.Text)
                .BERATBADAN = txtOBJEKTIF_BERATBADAN.Text
                Try
                    .DESCRIPTION = oReq_Recipe.GetData(sNoid).DESCRIPTION
                Catch ex As Exception
                    .DESCRIPTION = ""
                End Try
                Try
                    .ISCHEKED = oReq_Recipe.GetData(sNoid).ISCHEKED
                Catch ex As Exception
                    .ISCHEKED = False
                End Try
                Try
                    .KONFIRMASIRESEP = oReq_Recipe.GetData(sNoid).KONFIRMASIRESEP
                Catch ex As Exception
                    .KONFIRMASIRESEP = "Resep Belum Diterima"
                End Try
                .SUBTOTAL_RINCIAN = CDec(0)
                .SUBTOTAL_PAKET = CDec(0)
                .SUBTOTAL_KRONIS = CDec(0)

                Calculate()

                .GRANDTOTAL = CDec(txtGRANDTOTAL.Text)
                .NOIDUSER = sUserID
                .ISPERUBAHANRESEP = False
                .ISAPPROVAL = False
                .NOIDUSER_FARMASI = String.Empty
                .DESCRIPTION_PERUBAHAN = String.Empty
                .PENJAMIN = sPenjamin
                .KDCUSTOMER = txtKDCUSTOMER.Text
                .PASIEN = txtNAMAPASIEN.Text
                .ALAMAT = ""
                .DOKTER = sDPJP
                .TUJUAN = sTUJUAN
                .DIAGNOSA = txtDIAGNOOSA.Text
                .SIPDOKTER = sSIPDOKTER
                Try
                    .KDRECIPE = oReq_Recipe.GetData(sNoid).KDRECIPE
                Catch ex As Exception
                    .KDRECIPE = ""
                End Try
            End With

            Dim arrDetail = oReq_Recipe.GetStructureDetailList
            For i As Integer = 0 To grvDetailResep.RowCount - 2
                Dim dsDetail = oReq_Recipe.GetStructureDetail
                With dsDetail
                    sTerapi += 1

                    .SEQ = i
                    .KDREQRECIPE = ds.KDREQRECIPE
                    .NAMAOBAT = fn_LoadITEM(grvDetailResep.GetRowCellValue(i, colKDITEM))
                    .SATUAN = fn_LoadUOMDESCRIPTION(grvDetailResep.GetRowCellValue(i, colKDUOM))
                    .SIGNA = IIf(String.IsNullOrEmpty(grvDetailResep.GetRowCellValue(i, colKDSIGNA)), "", fn_LoadSIGNA(grvDetailResep.GetRowCellValue(i, colKDSIGNA)))
                    .CARAPAKAI = IIf(String.IsNullOrEmpty(grvDetailResep.GetRowCellValue(i, colKDCARAPAKAI)), "", fn_LoadCARAPAKAI(grvDetailResep.GetRowCellValue(i, colKDCARAPAKAI)))
                    .QTY = CDec(grvDetailResep.GetRowCellValue(i, colQTY))
                    .PRICE = CDec(grvDetailResep.GetRowCellValue(i, colPRICE))
                    .GRANDTOTAL = CDec(grvDetailResep.GetRowCellValue(i, colGRANDTOTAL))
                    .ROMAWI = IntegerToRoman(CInt(grvDetailResep.GetFocusedRowCellValue(colQTY)))
                    .REMARKS_DOKTER = IIf(String.IsNullOrEmpty(grvDetailResep.GetRowCellValue(i, colREMARKS_DOKTER)), "", grvDetailResep.GetRowCellValue(i, colREMARKS_DOKTER))
                    .KDITEM = grvDetailResep.GetRowCellValue(i, colKDITEM)
                    .KDUOM = grvDetailResep.GetRowCellValue(i, colKDUOM)
                    .KDSIGNA = IIf(String.IsNullOrEmpty(grvDetailResep.GetRowCellValue(i, colKDSIGNA)), "1286", grvDetailResep.GetRowCellValue(i, colKDSIGNA))
                    .KDCARAPAKAI = IIf(String.IsNullOrEmpty(grvDetailResep.GetRowCellValue(i, colKDCARAPAKAI)), "10", grvDetailResep.GetRowCellValue(i, colKDCARAPAKAI))
                    .QTY_PERUBAHAN = grvDetailResep.GetRowCellValue(i, colQTY)
                    .REMARKS_FARMASI = ""
                    .DOSIS = ""
                End With

                If grvDetailResep.GetRowCellValue(i, colKDITEM) IsNot Nothing Then
                    arrDetail.Add(dsDetail)
                End If
            Next

            Dim dsTelaah = oReq_Recipe.GetStructureTelaah

            With dsTelaah
                .DATECREATED = Now
                .DATEUPDATED = Now
                .KDREQRECIPE = ds.KDREQRECIPE
                .TELAAH_01 = False
                .TELAAH_02 = False
                .TELAAH_03 = False
                .TELAAH_04 = False
                .TELAAH_05 = False
                .TELAAH_06 = False
                .TELAAH_07 = False
                .TELAAH_08 = False
                .TELAAH_09 = False
                .TELAAH_10 = False
                .TELAAH_11 = False
                .TELAAH_12 = False
                .TELAAH_13 = False
                .TELAAH_14 = False
                .TELAAH_15 = False
                .TELAAH_16 = False
                .TELAAH_17 = False
                .TELAAH_18 = False
                .TELAAH_19 = False
                .TELAAH_20 = False
                .TELAAH_21 = False
                .TELAAH_22 = False
                .TELAAH_23 = False
                .TELAAH_24 = False
                .NOIDUSER = ""
                .REMARKS = ""
            End With

            Dim dsTelaahObat1 = oReq_Recipe.GetStructureTelaahObat1

            With dsTelaahObat1
                .DATECREATED = Now
                .DATEUPDATED = Now
                .TELAAH_01 = False
                .TELAAH_02 = False
                .TELAAH_03 = False
                .TELAAH_04 = False
                .TELAAH_05 = False
                .NOIDUSER = ""
                .REMARKS = ""
            End With

            Dim dsTelaahObat2 = oReq_Recipe.GetStructureTelaahObat2

            With dsTelaahObat2
                .DATECREATED = Now
                .DATEUPDATED = Now
                .TELAAH_01 = False
                .TELAAH_02 = False
                .TELAAH_03 = False
                .TELAAH_04 = False
                .TELAAH_05 = False
                .NOIDUSER = ""
                .REMARKS = ""
            End With

            ' ***** KODING *****
            Dim oKoding As New Admission.clsKoding
            Dim dsKoding = oKoding.GetStructureHeader
            With dsKoding
                Try
                    .DATECREATED = oKoding.GetData(sNoid).DATECREATED
                Catch ex As Exception
                    .DATECREATED = Now
                End Try
                .DATEUPDATED = Now
                .KDKODING = sNoid
                .KDPENDAFTARAN = sRegister.Trim.ToUpper
                .DATE = deDATE.DateTime
                ''new Font("Tahoma", 12, FontStyle.Bold)
                ''txtTINDAKLANJUT.Font = New Font("Times New Roman", 9, FontStyle.Regular)
                'txtTINDAKLANJUT.Text = txtTINDAKLANJUT.Text
                .DESCRIPTION = txtTINDAKLANJUT.Text
                .NOIDUSER = sUserID
                .DOKTER = sDPJP
                .TUJUAN = sTUJUAN
                .KDCUSTOMER = txtKDCUSTOMER.Text
                .NAMAPASIEN = txtNAMAPASIEN.Text
                .KARTUBPJS = sKARTUBPJS
                .TANGGALLAHIR = sTANGGALLAHIR
                .KDDOCTOR = sKDDOCTOR
            End With

            Dim arrDetailDiagnosaPenyerta = oKoding.GetStructureDetail_DianosisPenyertaList
            For i As Integer = 0 To grvDiagnosaPenyerta.RowCount - 2
                Dim dsDetail = oKoding.GetStructureDetail_DianosisPenyerta
                With dsDetail
                    .SEQ = i
                    .KDKODING = ds.KDREQRECIPE
                    .KDDIAGNOSA = "-"
                    .KETERANGAN = grvDiagnosaPenyerta.GetRowCellValue(i, colKETERANGAN)
                End With
                arrDetailDiagnosaPenyerta.Add(dsDetail)
            Next

            For i As Integer = 0 To grvTindakan.RowCount - 2
                sTindakan += 1
            Next

            Dim arrDetailDiagnosaPenyertaTindakan = oKoding.GetStructureDetail_Terapi_TindakanList

            If sTerapi >= sTindakan Then
                For i As Integer = 0 To grvDetailResep.RowCount - 2
                    Dim dsDetail = oKoding.GetStructureDetail_Terapi_Tindakan
                    With dsDetail
                        Dim TINDAKAN As String = String.Empty

                        .SEQ = i
                        .KDKODING = ds.KDREQRECIPE
                        .TERAPI = fn_LoadITEM(grvDetailResep.GetRowCellValue(i, colKDITEM)) & " " & grvDetailResep.GetRowCellValue(i, colREMARKS_DOKTER)
                        For y As Integer = 0 To grvTindakan.RowCount - 2
                            If i = y Then
                                TINDAKAN = grvTindakan.GetRowCellValue(i, colTINDAKAN)
                                Exit For
                            End If
                        Next
                        .TINDAKAN = TINDAKAN
                    End With
                    arrDetailDiagnosaPenyertaTindakan.Add(dsDetail)
                Next
            Else
                For i As Integer = 0 To grvTindakan.RowCount - 2
                    Dim dsDetail = oKoding.GetStructureDetail_Terapi_Tindakan
                    With dsDetail
                        Dim TERAPI As String = String.Empty

                        .SEQ = i
                        .KDKODING = ds.KDREQRECIPE
                        For y As Integer = 0 To grvDetailResep.RowCount - 2
                            If i = y Then
                                TERAPI = fn_LoadITEM(grvDetailResep.GetRowCellValue(i, colKDITEM)) & " " & grvDetailResep.GetRowCellValue(i, colREMARKS_DOKTER)
                                Exit For
                            End If
                        Next
                        .TERAPI = TERAPI
                        .TINDAKAN = grvTindakan.GetRowCellValue(i, colTINDAKAN)
                    End With
                    arrDetailDiagnosaPenyertaTindakan.Add(dsDetail)
                Next
            End If

            Dim arrDetailTindakan = oKoding.GetStructureDetail_TindakanList
            For i As Integer = 0 To grvTindakanPoli.RowCount - 2
                Dim dsDetail = oKoding.GetStructureDetail_Tindakan
                With dsDetail
                    .SEQ = i
                    .KDKODING = ds.KDREQRECIPE
                    .KETERANGAN = grvTindakanPoli.GetRowCellValue(i, colKETERANGAN_)
                End With
                arrDetailTindakan.Add(dsDetail)
            Next

            Dim oReqAkhirPemeriksaan As New Transaksi.clsReqAkhirPemeriksaan

            Dim dsPemeriksaan = oReqAkhirPemeriksaan.GetStructureHeader
            With dsPemeriksaan
                Try
                    .DATECREATED = oReqAkhirPemeriksaan.GetData(sNoid).DATECREATED
                Catch oErr As Exception
                    .DATECREATED = Now
                End Try
                .DATEUPDATED = Now
                .DATE = deDATE.DateTime
                .KDAKHIRPEMERIKSAAN = sNoid
                .KDPENDAFTARAN = sRegister
                .ALERGI_YA = chkALERGI_YA.Checked
                .ALERGI_TIDAK = chkALERGI_TIDAK.Checked
                .ALERGI_TEXT = txtALERGIOBAT.Text
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
                If sPicture Is Nothing Then
                    Try
                        .ALAMATGAMBAR = oReqAkhirPemeriksaan.GetData(sNoid).ALAMATGAMBAR
                    Catch ex As Exception
                        .ALAMATGAMBAR = ""
                    End Try
                Else
                    Dim Alamat As String = sALAMATSIMPAN & Now.ToString("ddMMyyyyHHmm") & "." & sRegister & ".jpg"
                    picGAMBAR2.Image.Save(Alamat, System.Drawing.Imaging.ImageFormat.Jpeg)
                    .ALAMATGAMBAR = Alamat
                End If
            End With

            Dim arrDetailObatRacikan = oKoding.GetStructureDetail_ObatRacikanList
            For i As Integer = 0 To grvOBATRACIKAN.RowCount - 2
                Dim dsDetail = oKoding.GetStructureDetail_ObatRacikan
                With dsDetail
                    .KDREQRECIPE = ds.KDREQRECIPE
                    .SEQ = i
                    .KDITEM = grvOBATRACIKAN.GetRowCellValue(i, colKDITEMOBATRACIKAN)
                    .NAMAOBAT = fn_LoadITEM(grvOBATRACIKAN.GetRowCellValue(i, colKDITEMOBATRACIKAN))
                    .KDUOM = grvOBATRACIKAN.GetRowCellValue(i, colKDUOMOBATRACIKAN)
                    .SATUAN = fn_LoadUOMDESCRIPTION(grvOBATRACIKAN.GetRowCellValue(i, colKDUOMOBATRACIKAN))
                    .SIGNA = grvOBATRACIKAN.GetRowCellValue(i, colSIGNAOBATRACIKAN)
                    .PERMINTAAN = grvOBATRACIKAN.GetRowCellValue(i, colPERMINTAANOBATRACIKAN)
                    .QTY = grvOBATRACIKAN.GetRowCellValue(i, colQTYOBATRACIKAN)
                    .REMARKS = grvOBATRACIKAN.GetRowCellValue(i, colREMARKSRACIKAN)
                End With
                arrDetailObatRacikan.Add(dsDetail)
            Next

            If oFormMode = FORM_MODE.FORM_MODE_ADD Then
                Dim KDREQRECIPE As String = oReq_Recipe.InsertData(False,ds, arrDetail, dsTelaah, dsTelaahObat1, dsTelaahObat2, dsKoding, arrDetailDiagnosaPenyerta, arrDetailDiagnosaPenyertaTindakan, arrDetailTindakan, dsPemeriksaan, sConnOld, arrDetailObatRacikan)

                If KDREQRECIPE <> "" Then
                    fn_Save = True
                Else
                    fn_Save = False
                End If
            ElseIf oFormMode = FORM_MODE.FORM_MODE_EDIT Then
                fn_Save = oReq_Recipe.UpdateData(False,ds, arrDetail, dsKoding, arrDetailDiagnosaPenyerta, arrDetailDiagnosaPenyertaTindakan, arrDetailTindakan, dsPemeriksaan, arrDetailObatRacikan)
            End If

            fn_LoadUpdateSudah(sRegister)
            fn_LoadDataUpdateAntian()
        Catch oErr As Exception
            MsgBox("Simpan Data Resep : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            fn_Save = False
        End Try
    End Function
#End Region
#Region "Grid Method"
    Private Sub OnValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles grvDetailResep.FocusedRowChanged
        If isLoad Then
            Calculate()
        End If
    End Sub
    Private Sub Calculate()
        Dim sSubTotal = 0
        For i As Integer = 0 To grvDetailResep.RowCount - 2
            sSubTotal += CDec(grvDetailResep.GetRowCellValue(i, colGRANDTOTAL))
        Next

        txtGRANDTOTAL.Text = sSubTotal
    End Sub
    Private Sub grvDiagnosaPenyerta_RowStyle(sender As System.Object, e As DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs) Handles grvDiagnosaPenyerta.RowStyle
        If grvDiagnosaPenyerta.IsFilterRow(e.RowHandle) Then Exit Sub
        e.Appearance.BackColor = Color.GreenYellow
    End Sub
    Private Sub grvTindakanPoli_RowStyle(sender As System.Object, e As DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs) Handles grvTindakanPoli.RowStyle
        If grvTindakanPoli.IsFilterRow(e.RowHandle) Then Exit Sub
        If grvTindakanPoli.GetRowCellValue(e.RowHandle, "KETERANGAN") <> "" Then
            e.Appearance.BackColor = Color.GreenYellow
        End If
    End Sub
    Private Sub grvTindakan_RowStyle(sender As System.Object, e As DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs) Handles grvTindakan.RowStyle
        If grvTindakan.IsFilterRow(e.RowHandle) Then Exit Sub
        If grvTindakan.GetRowCellValue(e.RowHandle, "TINDAKAN") <> "" Then
            e.Appearance.BackColor = Color.GreenYellow
        End If
    End Sub
    Private Sub grvDetailResep_RowStyle(sender As System.Object, e As DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs) Handles grvDetailResep.RowStyle
        If grvTindakanPoli.IsFilterRow(e.RowHandle) Then Exit Sub
        e.Appearance.BackColor = Color.GreenYellow
    End Sub
    Private Sub grvDetailResep_CellValueChanged(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs) Handles grvDetailResep.CellValueChanged
        If e.Column.Name = colKDITEM.Name Then
            Try
                If grvDetailResep.GetFocusedRowCellValue(colKDITEM) IsNot Nothing Then
                    grvDetailResep.SetFocusedRowCellValue(colKDUOM, fn_LoadUOMKDUOM(grvDetailResep.GetFocusedRowCellValue(colKDITEM)))
                    grvDetailResep.SetFocusedRowCellValue(colREMARKS_DOKTER, "")
                    grvDetailResep.SetFocusedRowCellValue(colQTY, 0)
                End If
            Catch oErr As Exception
                MsgBox("Load Detail : " & vbCrLf & oErr.Message, MsgBoxStyle.Critical, Me.Text)
            End Try
        ElseIf e.Column.Name = colREMARKS_DOKTER.Name Then
            'grvTindakan.Focus()
            'grvTindakan.AddNewRow()
            'grvTindakan.SetFocusedRowCellValue(colTERAPI, fn_LoadITEM(grvDetailResep.GetFocusedRowCellValue(colKDITEM)) & " " & fn_LoadSIGNA(grvDetailResep.GetFocusedRowCellValue(colKDSIGNA)))
            'grvTindakan.SetFocusedRowCellValue(colTINDAKAN, "")
            'grvTindakan.UpdateCurrentRow()
        ElseIf e.Column.Name = colKDUOM.Name Then
            Dim hargajualawal As Decimal = fn_LoadUOMKDUOMHARGA(grvDetailResep.GetFocusedRowCellValue(colKDITEM), grvDetailResep.GetFocusedRowCellValue(colKDUOM))
            Dim margin As Decimal = fn_LoadUOMKDUOMMARGIN(grvDetailResep.GetFocusedRowCellValue(colKDITEM), grvDetailResep.GetFocusedRowCellValue(colKDUOM))

            Dim sPriceSales As Decimal = hargajualawal + (hargajualawal * (margin / 100))

            grvDetailResep.SetFocusedRowCellValue(colPRICE, sPriceSales)

            'grvDetailResep.SetFocusedRowCellValue(colPRICE, fn_LoadUOMKDUOMHARGA(grvDetailResep.GetFocusedRowCellValue(colKDITEM), grvDetailResep.GetFocusedRowCellValue(colKDUOM)))
        ElseIf e.Column.Name = colQTY.Name Or e.Column.Name = colPRICE.Name Then
            Dim sSubTotal As Decimal = CDec(grvDetailResep.GetFocusedRowCellValue(colQTY)) * CDec(grvDetailResep.GetFocusedRowCellValue(colPRICE))
            grvDetailResep.SetFocusedRowCellValue(colGRANDTOTAL, sSubTotal)
        End If
    End Sub
    Private Sub grvOBATRACIKAN_CellValueChanged(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs) Handles grvOBATRACIKAN.CellValueChanged
        If e.Column.Name = colKDITEMOBATRACIKAN.Name Then
            Try
                If grvOBATRACIKAN.GetFocusedRowCellValue(colKDITEMOBATRACIKAN) IsNot Nothing Then
                    Dim Tes = fn_LoadUOMKDUOM(grvOBATRACIKAN.GetFocusedRowCellValue(colKDITEMOBATRACIKAN))

                    grvOBATRACIKAN.SetFocusedRowCellValue(colKDUOMOBATRACIKAN, IIf(Tes = "-", 99, Tes))
                    'grvOBATRACIKAN.SetFocusedRowCellValue(colREMARKS_DOKTER, "")
                    grvOBATRACIKAN.SetFocusedRowCellValue(colQTY, 0)
                    grvOBATRACIKAN.SetFocusedRowCellValue(colREMARKSRACIKAN, "-")
                End If
            Catch oErr As Exception
                MsgBox("Load Detail : " & vbCrLf & oErr.Message, MsgBoxStyle.Critical, Me.Text)
            End Try
            'ElseIf e.Column.Name = colREMARKS_DOKTER.Name Then
            '    'grvTindakan.Focus()
            '    'grvTindakan.AddNewRow()
            '    'grvTindakan.SetFocusedRowCellValue(colTERAPI, fn_LoadITEM(grvOBATRACIKAN.GetFocusedRowCellValue(colKDITEM)) & " " & fn_LoadSIGNA(grvOBATRACIKAN.GetFocusedRowCellValue(colKDSIGNA)))
            '    'grvTindakan.SetFocusedRowCellValue(colTINDAKAN, "")
            '    'grvTindakan.UpdateCurrentRow()
            'ElseIf e.Column.Name = colKDUOM.Name Then
            '    grvOBATRACIKAN.SetFocusedRowCellValue(colPRICE, fn_LoadUOMKDUOMHARGA(grvOBATRACIKAN.GetFocusedRowCellValue(colKDITEM), grvOBATRACIKAN.GetFocusedRowCellValue(colKDUOM)))
            'ElseIf e.Column.Name = colQTY.Name Or e.Column.Name = colPRICE.Name Then
            '    Dim sSubTotal As Decimal = CDec(grvOBATRACIKAN.GetFocusedRowCellValue(colQTY)) * CDec(grvOBATRACIKAN.GetFocusedRowCellValue(colPRICE))
            '    grvOBATRACIKAN.SetFocusedRowCellValue(colGRANDTOTAL, sSubTotal)
        End If
    End Sub
    'Private Sub grvRACIKAN_CellValueChanged(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs) Handles grvRacikan.CellValueChanged
    '    If e.Column.Name = colKDITEM_R.Name Then
    '        Try
    '            If grvRacikan.GetFocusedRowCellValue(colKDITEM_R) IsNot Nothing Then
    '                If oBrigging.GETITEM_NMITEM2(grvRacikan.GetFocusedRowCellValue(colKDITEM_R), sKoneksi) = "RACIKAN_1" Or oBrigging.GETITEM_NMITEM2(grvRacikan.GetFocusedRowCellValue(colKDITEM_R), sKoneksi) = "RACIKAN_2" Or oBrigging.GETITEM_NMITEM2(grvRacikan.GetFocusedRowCellValue(colKDITEM_R), sKoneksi) = "RACIKAN_3" Then
    '                    grvRacikan.SetFocusedRowCellValue(colHEADER, True)
    '                Else
    '                    grvRacikan.SetFocusedRowCellValue(colHEADER, False)
    '                End If
    '            End If
    '        Catch oErr As Exception
    '            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
    '        End Try
    '    ElseIf e.Column.Name = colKDUOM_R.Name Then
    '        Try
    '            If grvRacikan.GetFocusedRowCellValue(colKDITEM_R) IsNot Nothing And grvRacikan.GetFocusedRowCellValue(colKDUOM_R) IsNot Nothing Then
    '                Dim sPRICEPURCHASESTANDARD As Decimal = oBrigging.GETDATAUOM(grvRacikan.GetFocusedRowCellValue(colKDITEM_R), grvRacikan.GetFocusedRowCellValue(colKDUOM_R), sKoneksi)
    '                Dim sMARGIN As Decimal = oBrigging.GETDATAMARGIN(grvRacikan.GetFocusedRowCellValue(colKDITEM_R), grvRacikan.GetFocusedRowCellValue(colKDUOM_R), sKoneksi)

    '                If oBrigging.GETDATASTANDARISASI(grvRacikan.GetFocusedRowCellValue(colKDITEM_R), sKoneksi) = "STANDARISASI" Then
    '                    If txtPENJAMIN.Text = "BPJS KESEHATAN" Or txtPENJAMIN.Text = "UMUM" Or txtPENJAMIN.Text = "BON UMUM" Then
    '                        grvRacikan.SetFocusedRowCellValue(colPRICE_R, sPRICEPURCHASESTANDARD)
    '                    Else
    '                        grvRacikan.SetFocusedRowCellValue(colPRICE_R, (sPRICEPURCHASESTANDARD + (sPRICEPURCHASESTANDARD * (10 / 100))) + ((sPRICEPURCHASESTANDARD + (sPRICEPURCHASESTANDARD * (10 / 100))) * (25 / 100)))
    '                    End If
    '                Else
    '                    If txtPENJAMIN.Text = "BPJS KESEHATAN" Or txtPENJAMIN.Text = "UMUM" Or txtPENJAMIN.Text = "BON UMUM" Then
    '                        grvRacikan.SetFocusedRowCellValue(colPRICE_R, (sPRICEPURCHASESTANDARD + (sPRICEPURCHASESTANDARD * (sMARGIN / 100))))
    '                    Else
    '                        grvRacikan.SetFocusedRowCellValue(colPRICE_R, (sPRICEPURCHASESTANDARD + (sPRICEPURCHASESTANDARD * (10 / 100))) + ((sPRICEPURCHASESTANDARD + (sPRICEPURCHASESTANDARD * (10 / 100))) * (25 / 100)))
    '                    End If
    '                End If
    '            End If
    '        Catch oErr As Exception
    '            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
    '        End Try
    '    ElseIf e.Column.Name = colQTYHARI_R.Name Or e.Column.Name = colQTYSEDIAAN_R.Name Or e.Column.Name = colQTYDOSIS_R.Name Then
    '        If grvRacikan.GetFocusedRowCellValue(colQTYSEDIAAN_R) <> 0 Then
    '            Dim QTY As Decimal = 0

    '            QTY = (grvRacikan.GetFocusedRowCellValue(colQTYHARI_R) * grvRacikan.GetFocusedRowCellValue(colQTYDOSIS_R)) / grvRacikan.GetFocusedRowCellValue(colQTYSEDIAAN_R)

    '            If sCATEGORY = 0 Then
    '                If grdPOLIRUANGAN.Text = "IGD" Then
    '                    grvRacikan.SetFocusedRowCellValue(colQTY_7_R, QTY)
    '                    grvRacikan.SetFocusedRowCellValue(colQTY_23_R, 0)
    '                Else
    '                    If txtPENJAMIN.Text = "BPJS KESEHATAN" Or txtPENJAMIN.Text = "UMUM" Or txtPENJAMIN.Text = "BON UMUM" Then
    '                        If oBrigging.GETITEM_23(grvRacikan.GetFocusedRowCellValue(colKDITEM_R), sKoneksi) = True Then
    '                            If QTY = 15 Then
    '                                grvRacikan.SetFocusedRowCellValue(colQTY_7_R, 4)
    '                                grvRacikan.SetFocusedRowCellValue(colQTY_23_R, 11)
    '                            ElseIf QTY = 30 Then
    '                                grvRacikan.SetFocusedRowCellValue(colQTY_7_R, 7)
    '                                grvRacikan.SetFocusedRowCellValue(colQTY_23_R, 23)
    '                            ElseIf QTY = 60 Then
    '                                grvRacikan.SetFocusedRowCellValue(colQTY_7_R, 14)
    '                                grvRacikan.SetFocusedRowCellValue(colQTY_23_R, 46)
    '                            ElseIf QTY = 90 Then
    '                                grvRacikan.SetFocusedRowCellValue(colQTY_7_R, 21)
    '                                grvRacikan.SetFocusedRowCellValue(colQTY_23_R, 69)
    '                            ElseIf QTY = 120 Then
    '                                grvRacikan.SetFocusedRowCellValue(colQTY_7_R, 28)
    '                                grvRacikan.SetFocusedRowCellValue(colQTY_23_R, 92)
    '                            ElseIf QTY = 180 Then
    '                                grvRacikan.SetFocusedRowCellValue(colQTY_7_R, 42)
    '                                grvRacikan.SetFocusedRowCellValue(colQTY_23_R, 138)
    '                            Else
    '                                grvRacikan.SetFocusedRowCellValue(colQTY_7_R, QTY)
    '                                grvRacikan.SetFocusedRowCellValue(colQTY_23_R, 0)
    '                            End If
    '                        Else
    '                            grvRacikan.SetFocusedRowCellValue(colQTY_7_R, QTY)
    '                            grvRacikan.SetFocusedRowCellValue(colQTY_23_R, 0)
    '                        End If
    '                    Else
    '                        grvRacikan.SetFocusedRowCellValue(colQTY_7_R, QTY)
    '                        grvRacikan.SetFocusedRowCellValue(colQTY_23_R, 0)
    '                    End If
    '                End If
    '            Else
    '                grvRacikan.SetFocusedRowCellValue(colQTY_7_R, QTY)
    '                grvRacikan.SetFocusedRowCellValue(colQTY_23_R, 0)
    '            End If
    '        End If
    '    ElseIf e.Column.Name = colQTY_7_R.Name Or e.Column.Name = colQTY_23_R.Name Or e.Column.Name = colPRICE_R.Name Then
    '        Dim sSubTotal_7 As Decimal = CDec(grvRacikan.GetFocusedRowCellValue(colQTY_7_R)) * (CDec(grvRacikan.GetFocusedRowCellValue(colPRICE_R)))
    '        Dim sSubTotal_23 As Decimal = CDec(grvRacikan.GetFocusedRowCellValue(colQTY_23_R)) * (CDec(grvRacikan.GetFocusedRowCellValue(colPRICE_R)))

    '        grvRacikan.SetFocusedRowCellValue(colSUBTOTAL7_R, sSubTotal_7)
    '        grvRacikan.SetFocusedRowCellValue(colSUBTOTAL_23_R, sSubTotal_23)
    '    ElseIf e.Column.Name = colSUBTOTAL7_R.Name Or e.Column.Name = colSUBTOTAL_23_R.Name Then
    '        grvRacikan.SetFocusedRowCellValue(colGRANDTOTAL, grvRacikan.GetFocusedRowCellValue(colSUBTOTAL7_R) + grvRacikan.GetFocusedRowCellValue(colSUBTOTAL_23_R))
    '    End If
    'End Sub
    Private Sub chkALERGI_TIDAK_CheckedChanged(sender As Object, e As EventArgs) Handles chkALERGI_TIDAK.CheckedChanged
        If isLoad = True Then
            If chkALERGI_TIDAK.Checked = True Then
                txtALERGIOBAT.ResetText()
                chkALERGI_YA.Checked = False
            End If
        End If
    End Sub
    Private Sub chkALERGI_YA_CheckedChanged(sender As Object, e As EventArgs) Handles chkALERGI_YA.CheckedChanged
        If isLoad = True Then
            If chkALERGI_YA.Checked = True Then
                chkALERGI_TIDAK.Checked = False
            End If
        End If
    End Sub
#End Region
#Region "Command Button"
    Private Sub frm_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        Select Case e.KeyCode
            Case Keys.F12
                btnClose_Click()
            Case Keys.F3
                If btnSaveClose.Enabled = True Then
                    btnSaveClose_Click()
                End If
        End Select
    End Sub
    Private Sub picSimpan_Click() Handles picSimpan.Click
        If fn_Validate() = False Then Exit Sub
        Dim sOrder As Boolean = False
        Dim sLab As Boolean = False
        Dim sRad As Boolean = False

        For i As Integer = 0 To grvTindakan.RowCount - 2
            If grvTindakan.GetRowCellValue(i, colTINDAKAN).ToString.Contains("LABORATORIUM") Then
                sLab = True
            Else
                sOrder = True
            End If
            If grvTindakan.GetRowCellValue(i, colTINDAKAN).ToString.Contains("RADIOLOGI") Then
                sRad = True
            Else
                sOrder = True
            End If
        Next

        Dim frmPilihanMedrek As New frmPilihanMedrek
        frmPilihanMedrek.LoadMe(True, True, sOrder, IIf(txtTINDAKLANJUT.Text = "", False, True), sLab, sRad)
        frmPilihanMedrek.ShowDialog(Me)

        If sSIMPAN = False Then Exit Sub

        If fn_SaveCustomer_Detil() = False Then
            MsgBox("Save gagal! Tolong kontak admin anda!", MsgBoxStyle.Exclamation, Me.Text)
        Else
            If fn_Save() = False Then
                MsgBox("Save gagal! Tolong kontak admin anda!", MsgBoxStyle.Exclamation, Me.Text)
                Exit Sub
            Else
                MsgBox("Save " & sRegister.Trim.ToUpper & " success!", MsgBoxStyle.Information, Me.Text)

                Try
                    If ssKODEBOOKING <> "" Then

                        Dim uTime As Integer = (DateTime.UtcNow - New DateTime(1970, 1, 1, 0, 0, 0)).TotalSeconds
                        Dim JsonRequest As String = fn_RequestUpdateWaktuAntrean(ssKODEBOOKING, 4, uTime)
                        If JsonRequest <> "" Then
                            fn_UpdateWaktuAntrean(JsonRequest, uTime)
                            'MsgBox(fn_UpdateWaktuAntrean(JsonRequest, uTime), MsgBoxStyle.Information, Me.Text)
                        End If
                    End If
                Catch oErr As Exception
                    MsgBox("Update BPJS Waktu Tunggu : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try

                Dim oNameModul As New Setting.clsCounter
                oNameModul.UpdateDataCekSuara()

                Me.Close()
            End If
        End If
    End Sub
    Private Sub btnSaveClose_Click() Handles btnSaveClose.ItemClick
        If fn_Validate() = False Then Exit Sub
        Dim sOrder As Boolean = False
        Dim sLab As Boolean = False
        Dim sRad As Boolean = False

        For i As Integer = 0 To grvTindakan.RowCount - 2
            If grvTindakan.GetRowCellValue(i, colTINDAKAN).ToString.Contains("LABORATORIUM") Then
                sLab = True
            Else
                sOrder = True
            End If
            If grvTindakan.GetRowCellValue(i, colTINDAKAN).ToString.Contains("RADIOLOGI") Then
                sRad = True
            Else
                sOrder = True
            End If
        Next

        Dim frmPilihanMedrek As New frmPilihanMedrek
        frmPilihanMedrek.LoadMe(True, True, sOrder, IIf(txtTINDAKLANJUT.Text = "", False, True), sLab, sRad)
        frmPilihanMedrek.ShowDialog(Me)

        If sSIMPAN = False Then Exit Sub

        If fn_SaveCustomer_Detil() = False Then
            MsgBox("Save gagal! Tolong kontak admin anda!", MsgBoxStyle.Exclamation, Me.Text)
        Else
            If fn_Save() = False Then
                MsgBox("Save gagal! Tolong kontak admin anda!", MsgBoxStyle.Exclamation, Me.Text)
                Exit Sub
            Else
                fn_subcekPoli(ssKODEBOOKING)

                MsgBox("Save " & sRegister.Trim.ToUpper & " success!", MsgBoxStyle.Information, Me.Text)

                'Try
                '    If ssKODEBOOKING <> "" Then

                '        Dim uTime As Integer = (DateTime.UtcNow - New DateTime(1970, 1, 1, 0, 0, 0)).TotalSeconds
                '        Dim JsonRequest As String = fn_RequestUpdateWaktuAntrean(ssKODEBOOKING, 5, uTime)
                '        If JsonRequest <> "" Then
                '            fn_UpdateWaktuAntrean(JsonRequest, uTime)
                '            'MsgBox(fn_UpdateWaktuAntrean(JsonRequest, uTime), MsgBoxStyle.Information, Me.Text)
                '        End If
                '    End If
                'Catch oErr As Exception
                '    MsgBox("Update BPJS Waktu Tunggu : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
                'End Try

                Dim oNameModul As New Setting.clsCounter
                oNameModul.UpdateDataCekSuara()

                Me.Close()
            End If
        End If

    End Sub
    Public Sub fn_subcekPoli(ByVal Antrian As String)
        Try
            Dim oConn As New SqlConnection
            Dim oComm As New SqlCommand
            Dim da As SqlDataAdapter
            Dim ds As New DataSet
            Dim SQL As String
            oConn = New SqlConnection(sConnOld)

            If oConn.State = ConnectionState.Closed Then
                oConn.Open()
            End If

            SQL = "SELECT "
            SQL &= "* "
            SQL &= "FROM "
            SQL &= "Z_ANTRIAN_POLI A "
            SQL &= "INNER JOIN Z_ANTRIAN_H B "
            SQL &= "ON A.KODEBOOKING = B.KODEBOOKING "
            SQL &= "WHERE "
            SQL &= "A.KODEBOOKING = '" & Antrian & "' "

            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "Z_ANTRIAN_POLI")

            Dim cek As Boolean = False
            Dim Ada As Boolean = False

            For iLoop As Integer = 0 To ds.Tables("Z_ANTRIAN_POLI").Rows.Count - 1
                Ada = True

                With ds.Tables("Z_ANTRIAN_POLI")
                    If .Rows(iLoop)("TAKSID") = "5" Then
                        cek = True
                    End If

                    'fn_SaveTransaksi(.Rows(iLoop)("KDPOLIBPJS"), .Rows(iLoop)("KODEANTRIAN"), .Rows(iLoop)("ANTRIANDOKTER"), "")
                    'fn_LoadUOMKDUOM = .Rows(iLoop)("KDUOM")
                End With
            Next

            If oConn.State = ConnectionState.Open Then
                oConn.Close()
            End If

            If Ada = True Then
                If cek = False Then
                    fn_SaveTaksIDPoli(Antrian)
                End If
            End If

        Catch oErr As Exception
            MsgBox("Print Data : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Function fn_LoadDateSever() As Date
        Try
            Dim oConn As New SqlConnection
            Dim oComm As New SqlCommand
            Dim da As SqlDataAdapter
            Dim ds As New DataSet
            Dim SQL As String
            oConn = New SqlConnection(sConnOld)

            If oConn.State = ConnectionState.Closed Then
                oConn.Open()
            End If

            SQL = " Select TANGGAL = GetDate() "

            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "TANGGAL")

            fn_LoadDateSever = Now

            For iLoop As Integer = 0 To ds.Tables("TANGGAL").Rows.Count - 1
                With ds.Tables("TANGGAL")
                    fn_LoadDateSever = .Rows(iLoop)("TANGGAL")
                End With
            Next

            If oConn.State = ConnectionState.Connecting Then
                oConn.Close()
            End If
        Catch oErr As Exception
            fn_LoadDateSever = Now
            MsgBox("Load Date : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Function
    Private  Sub fn_SaveTaksIDPoli(ByVal sKODEBOOKING As String)
        Try
            Dim oConn As New SqlConnection
            Dim oComm As New SqlCommand
            Dim da As SqlDataAdapter
            Dim ds As New DataSet
            Dim SQL As String

            oConn = New SqlConnection(sConnOld)

            If oConn.State = ConnectionState.Closed Then
                oConn.Open()
            End If

            SQL = "INSERT INTO "
            SQL &= "Z_ANTRIAN_POLI "
            SQL &= "( "
            SQL &= "KODEBOOKING "
            SQL &= ",TAKSID "
            SQL &= ",WAKTU "
            SQL &= ",WAKTU_TEXT "
            SQL &= ",KODE_BPJS "
            SQL &= ",REQUEST "
            SQL &= ",RESPONS "
            SQL &= ") "
            SQL &= "VALUES "
            SQL &= "( "
            SQL &= "'" & sKODEBOOKING & "' "
            SQL &= ",5 "
            SQL &= ",'" & fn_LoadDateSever().ToString("yyyy-MM-dd HH:mm:ss") & "' "
            SQL &= ",'" & fn_LoadDateSever().ToString("yyyyMMdd") & "' "
            SQL &= ",'0' "
            SQL &= ",'' "
            SQL &= ",'' "
            SQL &= ") "

            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "INSERTZ_ANTRIAN_POLI")

            If oConn.State = ConnectionState.Connecting Then
                oConn.Close()
            End If
        Catch oErr As Exception
            MsgBox("Simpan Taks Id Gagal: " & vbCrLf & oErr.Message)
        End Try
    End Sub
    Private Sub btnClose_Click() Handles btnClose.ItemClick
        Me.Close()
    End Sub
    Private Function fn_RequestUpdateWaktuAntrean(ByVal KODEBOOKING As String, ByVal ISPANGGIL As Integer, ByVal uTime As Integer) As String
        Try
            Dim jsonRequest As String = String.Empty

            jsonRequest = " { "
            jsonRequest &= """kodebooking"": """ & KODEBOOKING & ""","
            jsonRequest &= """taskid"": """ & ISPANGGIL & """, "
            jsonRequest &= """waktu"": """ & uTime & "000" & """ "
            jsonRequest &= "}  "

            fn_RequestUpdateWaktuAntrean = jsonRequest

        Catch oErr As Exception
            fn_RequestUpdateWaktuAntrean = ""
        End Try
    End Function
    Public Function fn_UpdateWaktuAntrean(ByVal jsonRequest As String, ByVal uTime As Integer) As String
        Try
            Dim oSetKoneksi As New Brigging.clsSetKoneksi

            Dim dsSetKoneksi = oSetKoneksi.UpdateWaktuAntrean(sUrlAntrean, sConsidAntrean, sSecreateKeyAntrean, sUserKeyAntrean, uTime, jsonRequest)

            Dim allData = JObject.Parse(dsSetKoneksi)

            Dim CodeResponse As String = String.Empty
            Dim messageResponse As String = String.Empty

            CodeResponse = IIf(IsDBNull(allData.Item("metadata").Item("code")) = True, "", allData.Item("metadata").Item("code"))
            messageResponse = allData("metadata")("message").ToString

            If CodeResponse = "200" Then
                fn_UpdateWaktuAntrean = CodeResponse
            Else
                fn_UpdateWaktuAntrean = CodeResponse & "-" & messageResponse
                MsgBox(fn_UpdateWaktuAntrean, MsgBoxStyle.Information, Me.Text)
            End If

        Catch oErr As Exception
            fn_UpdateWaktuAntrean = oErr.Message
        End Try
    End Function
#End Region
#Region "Lookup / Event"
    Private Sub fn_LoadUpdateSudah(ByVal kdreg As String)
        Try
            Dim oConn As New SqlConnection
            Dim oComm As New SqlCommand
            Dim da As SqlDataAdapter
            Dim ds As New DataSet
            Dim SQL As String
            'Dim sConn As String = Decrypt(My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Software\SIMRS\SW\", "Database", "").ToString())

            oConn = New SqlConnection(sConnOld)

            If oConn.State = ConnectionState.Closed Then
                oConn.Open()
            End If

            SQL = "UPDATE "
            SQL &= "S_PENDAFTARAN_H "
            SQL &= "SET ISRESUME = 1 "
            SQL &= ",KETERANGAN_TINDAKLANJUT = '" & txtTINDAKLANJUT.Text & "' "
            SQL &= "WHERE "
            SQL &= "KDREG = '" & kdreg & "' "

            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "UPDATEPENDAFTARAN")

            If oConn.State = ConnectionState.Open Then
                oConn.Close()
            End If
        Catch oErr As Exception
            MsgBox("Load Obat Data : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadDataUpdateAntian()
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
            SQL &= ",KELOMPOK = ISNULL((SELECT BB.KELOMPOK FROM Z_COUNTER_ANTRIAN_D AA INNER JOIN Z_COUNTER_ANTRIAN_H BB ON AA.KDCOUNTER = BB.KDCOUNTER  WHERE A.KDANTRIANNEW = AA.KDCOUNTER + CONVERT(nvarchar(50),AA.SEQ) + AA.ANTRIAN) , '1')  "
            SQL &= "FROM "
            SQL &= "S_PENDAFTARAN_H A "
            SQL &= "WHERE CONVERT(VARCHAR(8), A.DATE, 112) = '" & deDATE.DateTime.ToString("yyyyMMdd") & "' "
            SQL &= "AND A.KDDEPARTMENT = " & sKDDEPARTMENT & " "
            SQL &= "AND A.KDDOCTOR = " & sKDDOCTOR & " "

            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "S_PENDAFTARAN_H")

            Dim TotalKelompok1 As Integer = 0
            Dim TotalKelompok2 As Integer = 0
            Dim SudahTotalKelompok1 As Integer = 0
            Dim SudahTotalKelompok2 As Integer = 0

            For iLoop As Integer = 0 To ds.Tables("S_PENDAFTARAN_H").Rows.Count - 1
                With ds.Tables("S_PENDAFTARAN_H")
                    If .Rows(iLoop)("KELOMPOK") = "1" Then
                        TotalKelompok1 += 1

                        If .Rows(iLoop)("ISRESUME") = "1" Then
                            SudahTotalKelompok1 += 1
                        End If
                    Else
                        TotalKelompok2 += 1

                        If .Rows(iLoop)("ISRESUME") = "1" Then
                            SudahTotalKelompok2 += 1
                        End If
                    End If
                End With
            Next

            If oConn.State = ConnectionState.Open Then
                oConn.Close()
            End If

            fn_UpdateAntian(TotalKelompok1, SudahTotalKelompok1, "1")
            fn_UpdateAntian(TotalKelompok2, SudahTotalKelompok2, "2")

        Catch oErr As Exception
            MsgBox("Load List Pasien Data : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation)
        End Try
    End Sub
    Private Sub fn_UpdateAntian(ByVal Total As Integer, ByVal Sisa As Integer, ByVal KELOMPOK As String)
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

            SQL = "UPDATE "
            SQL &= "Z_MASTER_ATRIANSEMUAPOLI "
            SQL &= "SET "
            SQL &= "KETERANGAN = '" & Sisa.ToString.PadLeft(3, "0") & "/" & Total.ToString.PadLeft(3, "0") & "' "
            SQL &= "WHERE "
            SQL &= "KDDEPARTMENT = " & sKDDEPARTMENT & " "
            SQL &= "AND KDDOCTOR = " & sKDDOCTOR & " "
            SQL &= "AND KELOMPOK = '" & KELOMPOK & "' "

            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "UPDATE_ S_PENDAFTARAN_H")

            If oConn.State = ConnectionState.Open Then
                oConn.Close()
            End If

        Catch oErr As Exception
            MsgBox("Load List Pasien Data : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation)
        End Try
    End Sub
    Private Sub fn_LoadITEM()
        Try
            Dim oConn As New SqlConnection
            Dim oComm As New SqlCommand
            Dim da As SqlDataAdapter
            Dim ds As New DataSet
            Dim SQL As String
            'Dim sConn As String = Decrypt(My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Software\SIMRS\SW\", "Database", "").ToString())

            oConn = New SqlConnection(sConnOld)

            If oConn.State = ConnectionState.Closed Then
                oConn.Open()
            End If

            SQL = "SELECT "
            SQL &= "* "
            SQL &= "FROM ( "
            SQL &= "SELECT "
            SQL &= "A.KDITEM "
            SQL &= ",A.NMITEM1 "
            SQL &= ",A.NMITEM2 "
            'SQL &= ",HARGA = B.PRICEPURCHASESTANDARD "
            SQL &= ",HARGA = B.PRICEPURCHASESTANDARD + (B.PRICEPURCHASESTANDARD * (B.MARGIN / 100)) "
            SQL &= ",SATUAN = C.DESCRIPTION "
            SQL &= ",STOK = ISNULL((SELECT SUM(AMOUNT) FROM M_ITEM_WAREHOUSE WHERE B.KDITEM = KDITEM AND B.KDUOM = KDUOM GROUP BY KDITEM) ,0) "
            SQL &= "FROM M_ITEM A "
            SQL &= "INNER JOIN M_ITEM_UOM B "
            SQL &= "ON A.KDITEM = B.KDITEM "
            SQL &= "INNER JOIN M_UOM C "
            SQL &= "ON B.KDUOM = C.KDUOM "
            SQL &= "WHERE "
            SQL &= "A.ISACTIVE = 1 "
            SQL &= "AND B.RATE = 1 "

            SQL &= "UNION "

            SQL &= "SELECT "
            SQL &= "KDITEM = 999999 "
            SQL &= ",NMITEM1 = A.DESCRIPTION "
            SQL &= ",NMITEM2 = A.DESCRIPTION "
            SQL &= ",HARGA = 0 "
            SQL &= ",SATUAN = '-' "
            SQL &= ",STOK = 0 "
            SQL &= "FROM M_ITEM_RACIK A "
            SQL &= ") X "
            SQL &= "ORDER BY "
            SQL &= "X.HARGA "

            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "ITEM")

            grdKDITEM.DataSource = ds.Tables("ITEM")
            grdKDITEM.ValueMember = "KDITEM"
            grdKDITEM.DisplayMember = "NMITEM2"

            grdKDITEMOBATRACIKAN.DataSource = ds.Tables("ITEM")
            grdKDITEMOBATRACIKAN.ValueMember = "KDITEM"
            grdKDITEMOBATRACIKAN.DisplayMember = "NMITEM2"

            If oConn.State = ConnectionState.Open Then
                oConn.Close()
            End If
        Catch oErr As Exception
            MsgBox("Load Obat Data : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Function fn_LoadITEM(ByVal KDITEM As String) As String
        Try
            fn_LoadITEM = "RACIKAN"

            Dim oConn As New SqlConnection
            Dim oComm As New SqlCommand
            Dim da As SqlDataAdapter
            Dim ds As New DataSet
            Dim SQL As String
            oConn = New SqlConnection(sConnOld)

            If oConn.State = ConnectionState.Closed Then
                oConn.Open()
            End If

            SQL = "SELECT "
            SQL &= "* "
            SQL &= "FROM "
            SQL &= "M_ITEM A "
            SQL &= "WHERE "
            SQL &= "A.KDITEM = '" & KDITEM & "' "

            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "SEARCHITEM")

            For iLoop As Integer = 0 To ds.Tables("SEARCHITEM").Rows.Count - 1
                With ds.Tables("SEARCHITEM")
                    fn_LoadITEM = .Rows(iLoop)("NMITEM2")
                End With
            Next

            If oConn.State = ConnectionState.Open Then
                oConn.Close()
            End If
        Catch oErr As Exception
            fn_LoadITEM = ""
            MsgBox("Load Item" & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Function
    Private Sub fn_LoadUOM()
        Try
            Dim oConn As New SqlConnection
            Dim oComm As New SqlCommand
            Dim da As SqlDataAdapter
            Dim ds As New DataSet
            Dim SQL As String

            oConn = New SqlConnection(sConnOld)

            If oConn.State = ConnectionState.Closed Then
                oConn.Open()
            End If

            SQL = "SELECT "
            SQL &= "A.KDUOM "
            SQL &= ",A.DESCRIPTION "
            SQL &= "FROM M_UOM A "
            SQL &= "WHERE "
            SQL &= "A.ISACTIVE = 1 "

            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "UOM")

            grdUOM.DataSource = ds.Tables("UOM")
            grdUOM.ValueMember = "KDUOM"
            grdUOM.DisplayMember = "DESCRIPTION"

            grdKDUOMRACIKAN.DataSource = ds.Tables("UOM")
            grdKDUOMRACIKAN.ValueMember = "KDUOM"
            grdKDUOMRACIKAN.DisplayMember = "DESCRIPTION"

            If oConn.State = ConnectionState.Open Then
                oConn.Close()
            End If
        Catch oErr As Exception
            MsgBox("Load Satuan Data : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Function fn_LoadUOMKDUOM(ByVal KDITEM As String) As String
        Try
            fn_LoadUOMKDUOM = "-"

            Dim oConn As New SqlConnection
            Dim oComm As New SqlCommand
            Dim da As SqlDataAdapter
            Dim ds As New DataSet
            Dim SQL As String
            oConn = New SqlConnection(sConnOld)

            If oConn.State = ConnectionState.Closed Then
                oConn.Open()
            End If

            SQL = "SELECT "
            SQL &= "TOP 1 * "
            SQL &= "FROM "
            SQL &= "M_ITEM_UOM A "
            SQL &= "WHERE "
            SQL &= "A.KDITEM = '" & KDITEM & "' "
            SQL &= "AND A.RATE = 1 "

            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "SEARCHKDUOM")

            For iLoop As Integer = 0 To ds.Tables("SEARCHKDUOM").Rows.Count - 1
                With ds.Tables("SEARCHKDUOM")
                    fn_LoadUOMKDUOM = .Rows(iLoop)("KDUOM")
                End With
            Next

            If oConn.State = ConnectionState.Open Then
                oConn.Close()
            End If
        Catch oErr As Exception
            fn_LoadUOMKDUOM = ""
            MsgBox("Load Satuan" & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Function
    Private Function fn_LoadUOMKDUOMHARGA(ByVal KDITEM As String, ByVal kduom As String) As Decimal
        Try
            fn_LoadUOMKDUOMHARGA = 0

            Dim oConn As New SqlConnection
            Dim oComm As New SqlCommand
            Dim da As SqlDataAdapter
            Dim ds As New DataSet
            Dim SQL As String
            oConn = New SqlConnection(sConnOld)

            If oConn.State = ConnectionState.Closed Then
                oConn.Open()
            End If

            SQL = "SELECT "
            SQL &= "TOP 1 * "
            SQL &= "FROM "
            SQL &= "M_ITEM_UOM A "
            SQL &= "WHERE "
            SQL &= "A.KDITEM = '" & KDITEM & "' "
            SQL &= "AND A.KDUOM = '" & kduom & "' "
            SQL &= "AND A.RATE = 1 "

            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "SEARCHKDUOMHARGA")

            For iLoop As Integer = 0 To ds.Tables("SEARCHKDUOMHARGA").Rows.Count - 1
                With ds.Tables("SEARCHKDUOMHARGA")
                    'fn_LoadUOMKDUOMHARGA = .Rows(iLoop)("PRICESALESSTANDARD")
                    fn_LoadUOMKDUOMHARGA = .Rows(iLoop)("PRICEPURCHASESTANDARD")
                End With
            Next

            If oConn.State = ConnectionState.Open Then
                oConn.Close()
            End If
        Catch oErr As Exception
            fn_LoadUOMKDUOMHARGA = 0
            MsgBox("Load Satuan" & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Function
    Private Function fn_LoadUOMKDUOMMARGIN(ByVal KDITEM As String, ByVal kduom As String) As Decimal
        Try
            fn_LoadUOMKDUOMMARGIN = 0

            Dim oConn As New SqlConnection
            Dim oComm As New SqlCommand
            Dim da As SqlDataAdapter
            Dim ds As New DataSet
            Dim SQL As String
            oConn = New SqlConnection(sConnOld)

            If oConn.State = ConnectionState.Closed Then
                oConn.Open()
            End If

            SQL = "SELECT "
            SQL &= "TOP 1 * "
            SQL &= "FROM "
            SQL &= "M_ITEM_UOM A "
            SQL &= "WHERE "
            SQL &= "A.KDITEM = '" & KDITEM & "' "
            SQL &= "AND A.KDUOM = '" & kduom & "' "
            SQL &= "AND A.RATE = 1 "

            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "SEARCHKDUOMHARGA")

            For iLoop As Integer = 0 To ds.Tables("SEARCHKDUOMHARGA").Rows.Count - 1
                With ds.Tables("SEARCHKDUOMHARGA")
                    'fn_LoadUOMKDUOMHARGA = .Rows(iLoop)("PRICESALESSTANDARD")
                    fn_LoadUOMKDUOMMARGIN = .Rows(iLoop)("MARGIN")
                End With
            Next

            If oConn.State = ConnectionState.Open Then
                oConn.Close()
            End If
        Catch oErr As Exception
            fn_LoadUOMKDUOMMARGIN = 0
            MsgBox("Load Margin" & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Function
    Private Function fn_LoadUOMDESCRIPTION(ByVal KDUOM As String) As String
        Try
            fn_LoadUOMDESCRIPTION = ""

            Dim oConn As New SqlConnection
            Dim oComm As New SqlCommand
            Dim da As SqlDataAdapter
            Dim ds As New DataSet
            Dim SQL As String
            oConn = New SqlConnection(sConnOld)

            If oConn.State = ConnectionState.Closed Then
                oConn.Open()
            End If

            SQL = "SELECT "
            SQL &= "* "
            SQL &= "FROM "
            SQL &= "M_UOM A "
            SQL &= "WHERE "
            SQL &= "A.KDUOM = '" & KDUOM & "' "

            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "SEARCHUOM")

            For iLoop As Integer = 0 To ds.Tables("SEARCHUOM").Rows.Count - 1
                With ds.Tables("SEARCHUOM")
                    fn_LoadUOMDESCRIPTION = .Rows(iLoop)("DESCRIPTION")
                End With
            Next

            If oConn.State = ConnectionState.Open Then
                oConn.Close()
            End If
        Catch oErr As Exception
            fn_LoadUOMDESCRIPTION = ""
            MsgBox("Load Satuan" & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Function
    Private Sub fn_LoadSIGNA()
        Try
            Dim oConn As New SqlConnection
            Dim oComm As New SqlCommand
            Dim da As SqlDataAdapter
            Dim ds As New DataSet
            Dim SQL As String

            oConn = New SqlConnection(sConnOld)

            If oConn.State = ConnectionState.Closed Then
                oConn.Open()
            End If

            SQL = "SELECT "
            SQL &= "A.KDSIGNA "
            SQL &= ",A.DESCRIPTION "
            SQL &= "FROM M_SIGNA A "
            SQL &= "WHERE "
            SQL &= "A.ISACTIVE = 1 "

            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "SIGNA")

            grdKDSIGNA.DataSource = ds.Tables("SIGNA")
            grdKDSIGNA.ValueMember = "KDSIGNA"
            grdKDSIGNA.DisplayMember = "DESCRIPTION"

            If oConn.State = ConnectionState.Open Then
                oConn.Close()
            End If
        Catch oErr As Exception
            MsgBox("Load Signa Data : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Function fn_LoadSIGNA(ByVal KDSIGNA As String) As String
        Try
            fn_LoadSIGNA = ""

            Dim oConn As New SqlConnection
            Dim oComm As New SqlCommand
            Dim da As SqlDataAdapter
            Dim ds As New DataSet
            Dim SQL As String
            oConn = New SqlConnection(sConnOld)

            If oConn.State = ConnectionState.Closed Then
                oConn.Open()
            End If

            SQL = "SELECT "
            SQL &= "* "
            SQL &= "FROM "
            SQL &= "M_SIGNA A "
            SQL &= "WHERE "
            SQL &= "A.KDSIGNA = '" & KDSIGNA & "' "

            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "SEARCHSIGNA")

            For iLoop As Integer = 0 To ds.Tables("SEARCHSIGNA").Rows.Count - 1
                With ds.Tables("SEARCHSIGNA")
                    fn_LoadSIGNA = .Rows(iLoop)("DESCRIPTION")
                End With
            Next

            If oConn.State = ConnectionState.Open Then
                oConn.Close()
            End If
        Catch oErr As Exception
            fn_LoadSIGNA = ""
            MsgBox("Load Signa" & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Function
    Private Sub fn_LoadCARAPAKAI()
        Try
            Dim oConn As New SqlConnection
            Dim oComm As New SqlCommand
            Dim da As SqlDataAdapter
            Dim ds As New DataSet
            Dim SQL As String

            oConn = New SqlConnection(sConnOld)

            If oConn.State = ConnectionState.Closed Then
                oConn.Open()
            End If

            SQL = "SELECT "
            SQL &= "A.KDCP "
            SQL &= ",A.DESCRIPTION "
            SQL &= "FROM M_CARAPAKAI A "
            SQL &= "WHERE "
            SQL &= "A.ISACTIVE = 1 "

            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "CARAPAKAI")

            grdCARAPAKAI.DataSource = ds.Tables("CARAPAKAI")
            grdCARAPAKAI.ValueMember = "KDCP"
            grdCARAPAKAI.DisplayMember = "DESCRIPTION"

            If oConn.State = ConnectionState.Open Then
                oConn.Close()
            End If
        Catch oErr As Exception
            MsgBox("Load Signa Data : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Function fn_LoadCARAPAKAI(ByVal KDCP As String) As String
        Try
            fn_LoadCARAPAKAI = ""

            Dim oConn As New SqlConnection
            Dim oComm As New SqlCommand
            Dim da As SqlDataAdapter
            Dim ds As New DataSet
            Dim SQL As String
            oConn = New SqlConnection(sConnOld)

            If oConn.State = ConnectionState.Closed Then
                oConn.Open()
            End If

            SQL = "SELECT "
            SQL &= "* "
            SQL &= "FROM "
            SQL &= "M_CARAPAKAI A "
            SQL &= "WHERE "
            SQL &= "A.KDCP = '" & KDCP & "' "

            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "SEARCHCARAPAKAI")

            For iLoop As Integer = 0 To ds.Tables("SEARCHCARAPAKAI").Rows.Count - 1
                With ds.Tables("SEARCHCARAPAKAI")
                    fn_LoadCARAPAKAI = .Rows(iLoop)("DESCRIPTION")
                End With
            Next

            If oConn.State = ConnectionState.Open Then
                oConn.Close()
            End If
        Catch oErr As Exception
            fn_LoadCARAPAKAI = ""
            MsgBox("Load Cara Pakai" & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Function
    Private Sub fn_LoadTEMPLATE()
        Dim oTemplate As New Reference.clsTemplateResep
        Try
            grdTEMPLATE.Properties.DataSource = oTemplate.GetData.Where(Function(x) x.NOIDUSER = sUserID).ToList()
            grdTEMPLATE.Properties.ValueMember = "KDTEMPLATE"
            grdTEMPLATE.Properties.DisplayMember = "DESCRIPTION"
        Catch oErr As Exception
            MsgBox("Load Template Data : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub btnRiwayatPemberianObat_Click(sender As Object, e As EventArgs)
        If txtKDCUSTOMER.Text <> "" Then
            Dim frmRiwayatResep As New frmRiwayatResep
            Try
                frmRiwayatResep.LoadMe(txtKDCUSTOMER.Text, 1)
                frmRiwayatResep.ShowDialog(Me)

                If sNoTransaksi <> String.Empty Then
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
                    SQL &= "A.KDITEM "
                    SQL &= ",A.KDUOM "
                    SQL &= ",A.KDSIGNA "
                    SQL &= ",A.KDCP "
                    SQL &= ",QTY = SUM(A.QTY) "
                    SQL &= "FROM S_RECIPE_D AS A "
                    SQL &= "WHERE A.KDRECIPE = '" & sNoTransaksi & "' "
                    SQL &= "GROUP BY "
                    SQL &= "A.KDITEM "
                    SQL &= ",A.KDUOM "
                    SQL &= ",A.KDSIGNA "
                    SQL &= ",A.KDCP "
                    'SQL &= "ORDER BY A.SEQ "

                    oComm.Connection = oConn
                    oComm.CommandText = SQL
                    oComm.CommandTimeout = 120
                    oComm.CommandType = CommandType.Text

                    da = New SqlDataAdapter(oComm)
                    da.Fill(ds, "S_LIS_D")

                    If oConn.State = ConnectionState.Open Then
                        oConn.Close()
                    End If

                    For iLoop As Integer = 0 To ds.Tables("S_LIS_D").Rows.Count - 1
                        With ds.Tables("S_LIS_D")
                            grvDetailResep.Focus()
                            grvDetailResep.AddNewRow()

                            grvDetailResep.SetFocusedRowCellValue(colKDITEM, .Rows(iLoop)("KDITEM"))
                            'grvDetailResep.SetFocusedRowCellValue(colSATUAN, fn_LoadUOM(.Rows(iLoop)("KDITEM"), .Rows(iLoop)("KDUOM")))
                            'grvDetailResep.SetFocusedRowCellValue(colSIGNA, fn_LoadSIGNA(sKDSIGNA_PILIH))
                            'grvDetailResep.SetFocusedRowCellValue(colCARAPAKAI, fn_LoadCARAPAKAI(sKDCARAPAKAI_PILIH))
                            grvDetailResep.SetFocusedRowCellValue(colQTY, .Rows(iLoop)("QTY"))

                            grvDetailResep.SetFocusedRowCellValue(colKDITEM, .Rows(iLoop)("KDITEM"))
                            grvDetailResep.SetFocusedRowCellValue(colKDUOM, .Rows(iLoop)("KDUOM"))
                            grvDetailResep.SetFocusedRowCellValue(colKDSIGNA, .Rows(iLoop)("KDSIGNA"))
                            grvDetailResep.SetFocusedRowCellValue(colKDCARAPAKAI, .Rows(iLoop)("KDCP"))
                            'grvDetailResep.SetFocusedRowCellValue(colQTY_PERUBAHAN, .Rows(iLoop)("QTY"))
                            grvDetailResep.UpdateCurrentRow()
                        End With
                    Next
                End If
            Catch ex As Exception
                MsgBox("Load Form Riwayat Resep : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
            End Try
        Else
            MsgBox("Nomor Rekam Medis Kosong!!!", MsgBoxStyle.Exclamation, Me.Text)
        End If
    End Sub
    Private Sub btnRiwayatPemberianResep_Click(sender As Object, e As EventArgs) Handles btnRiwayatPemberianResep.Click
        If txtKDCUSTOMER.Text <> "" Then
            Dim frmRiwayatResep As New frmRiwayatResep
            Try
                frmRiwayatResep.LoadMe(txtKDCUSTOMER.Text, 0)
                frmRiwayatResep.ShowDialog(Me)

                If sNoTransaksi <> String.Empty Then
                    Dim oReq_Recipe As New Transaksi.clsReq_Recipe

                    Dim dsTemplate = oReq_Recipe.GetDataDetail(sNoTransaksi)

                    For Each iLoop In dsTemplate.OrderBy(Function(x) x.SEQ)
                        grvDetailResep.Focus()
                        grvDetailResep.AddNewRow()

                        grvDetailResep.SetFocusedRowCellValue(colKDITEM, iLoop.KDITEM)
                        'grvDetailResep.SetFocusedRowCellValue(colKDUOM, iLoop.M_UOM.MEMO)
                        'grvDetailResep.SetFocusedRowCellValue(colKDSIGNA, iLoop.M_SIGNA.MEMO)
                        'grvDetailResep.SetFocusedRowCellValue(colKDCARAPAKAI, iLoop.M_CARAPAKAI.MEMO)
                        grvDetailResep.SetFocusedRowCellValue(colQTY, iLoop.QTY)
                        grvDetailResep.SetFocusedRowCellValue(colREMARKS_DOKTER, iLoop.REMARKS_DOKTER)

                        'grvDetailResep.SetFocusedRowCellValue(colKDITEM, iLoop.KDITEM)
                        grvDetailResep.SetFocusedRowCellValue(colKDUOM, iLoop.KDUOM)
                        grvDetailResep.SetFocusedRowCellValue(colKDSIGNA, iLoop.KDSIGNA)
                        grvDetailResep.SetFocusedRowCellValue(colKDCARAPAKAI, iLoop.KDCARAPAKAI)
                        'grvDetailResep.SetFocusedRowCellValue(colQTY_PERUBAHAN, iLoop.QTY)
                        'grvDetailResep.SetFocusedRowCellValue(colREMARKS_FARMASI, iLoop.REMARKS_FARMASI)
                        grvDetailResep.UpdateCurrentRow()
                    Next
                End If

            Catch ex As Exception
                MsgBox("Load Form Riwayat Resep : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
            End Try
        Else
            MsgBox("Nomor Rekam Medis Kosong!!!", MsgBoxStyle.Exclamation, Me.Text)
        End If
    End Sub
    Private Function IntegerToRoman(IntNumberValue As Integer) As String
        Dim RomanNumbers As New Dictionary(Of String, Integer)()
        RomanNumbers.Add("M", 1000)
        RomanNumbers.Add("CM", 900)
        RomanNumbers.Add("D", 500)
        RomanNumbers.Add("CD", 400)
        RomanNumbers.Add("C", 100)
        RomanNumbers.Add("XC", 90)
        RomanNumbers.Add("L", 50)
        RomanNumbers.Add("XL", 40)
        RomanNumbers.Add("X", 10)
        RomanNumbers.Add("IX", 9)
        RomanNumbers.Add("V", 5)
        RomanNumbers.Add("IV", 4)
        RomanNumbers.Add("I", 1)

        Dim result As String = ""

        For Each pair As KeyValuePair(Of String, Integer) In RomanNumbers
            While IntNumberValue >= pair.Value
                IntNumberValue -= pair.Value
                result += pair.Key
            End While
        Next
        Return result
    End Function
    Private Sub DeleteToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DeleteToolStripMenuItem.Click
        grvDetailResep.DeleteSelectedRows()
    End Sub
    Private Sub DeleteAllToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DeleteAllToolStripMenuItem.Click
        grvDetailResep.OptionsSelection.MultiSelect = True
        grvDetailResep.SelectAll()
        grvDetailResep.DeleteSelectedRows()
        grvDetailResep.OptionsSelection.MultiSelect = False
    End Sub
    Private Sub ToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem1.Click
        grvDiagnosaPenyerta.DeleteSelectedRows()
    End Sub
    Private Sub DeleteAllToolStripMenuItem2_Click(sender As Object, e As EventArgs) Handles DeleteAllToolStripMenuItem2.Click
        grvDiagnosaPenyerta.OptionsSelection.MultiSelect = True
        grvDiagnosaPenyerta.SelectAll()
        grvDiagnosaPenyerta.DeleteSelectedRows()
        grvDiagnosaPenyerta.OptionsSelection.MultiSelect = False
    End Sub
    Private Sub ToolStripMenuItem2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem2.Click
        grvTindakan.DeleteSelectedRows()
    End Sub
    Private Sub DeleteAllToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles DeleteAllToolStripMenuItem1.Click
        grvTindakan.OptionsSelection.MultiSelect = True
        grvTindakan.SelectAll()
        grvTindakan.DeleteSelectedRows()
        grvTindakan.OptionsSelection.MultiSelect = False
    End Sub
    Private Sub ToolStripMenuItem3_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem3.Click
        grvTindakanPoli.DeleteSelectedRows()
    End Sub
    Private Sub ToolStripMenuItem4_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem4.Click
        grvTindakanPoli.OptionsSelection.MultiSelect = True
        grvTindakanPoli.SelectAll()
        grvTindakanPoli.DeleteSelectedRows()
        grvTindakanPoli.OptionsSelection.MultiSelect = False
    End Sub
    Private Sub ToolStripMenuItem5_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem5.Click
        grvOBATRACIKAN.DeleteSelectedRows()
    End Sub
    Private Sub ToolStripMenuItem6_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem6.Click
        grvOBATRACIKAN.OptionsSelection.MultiSelect = True
        grvOBATRACIKAN.SelectAll()
        grvOBATRACIKAN.DeleteSelectedRows()
        grvOBATRACIKAN.OptionsSelection.MultiSelect = False
    End Sub
    Private Sub grdTEMPLATE_EditValueChanged(sender As Object, e As EventArgs) Handles grdTEMPLATE.EditValueChanged
        If grdTEMPLATE.Text <> "" Then
            Dim oTemplate As New Reference.clsTemplateResep

            Dim dsTemplate = oTemplate.GetDataDetail(grdTEMPLATE.EditValue)

            For Each iLoop In dsTemplate.OrderBy(Function(x) x.SEQ)
                txtDIAGNOOSA.Text = iLoop.S_REQ_RECIPE_TEMPLATE_H.DESCRIPTION
                grvDetailResep.Focus()
                grvDetailResep.AddNewRow()
                grvDetailResep.SetFocusedRowCellValue(colKDITEM, iLoop.KDITEM)
                grvDetailResep.SetFocusedRowCellValue(colKDUOM, iLoop.KDUOM)
                grvDetailResep.SetFocusedRowCellValue(colKDSIGNA, iLoop.KDSIGNA)
                grvDetailResep.SetFocusedRowCellValue(colKDCARAPAKAI, iLoop.KDCARAPAKAI)
                grvDetailResep.SetFocusedRowCellValue(colQTY, iLoop.QTY)
                grvDetailResep.SetFocusedRowCellValue(colREMARKS_DOKTER, iLoop.REMARKS_DOKTER)
                grvDetailResep.UpdateCurrentRow()
            Next
        End If
    End Sub
    Private Sub SimpleButton1_Click(sender As Object, e As EventArgs) Handles SimpleButton1.Click
        Dim frmReportOrderPenunjang As New frmReportOrderPenunjang
        Try
            frmReportOrderPenunjang.ShowDialog(Me)

            For Each xloop In sListRincianLab
                grvTindakan.Focus()
                grvTindakan.AddNewRow()
                grvTindakan.SetFocusedRowCellValue(colTERAPI, "")
                grvTindakan.SetFocusedRowCellValue(colTINDAKAN, "LABORATORIUM " & xloop)
                grvTindakan.UpdateCurrentRow()
            Next

            For Each xloop In sListRincianRad
                grvTindakan.Focus()
                grvTindakan.AddNewRow()
                grvTindakan.SetFocusedRowCellValue(colTERAPI, "")
                grvTindakan.SetFocusedRowCellValue(colTINDAKAN, "RADIOLOGI " & xloop)
                grvTindakan.UpdateCurrentRow()
            Next
        Catch ex As Exception
            MsgBox("Load Form Detail Penunjang: " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub grvDetailResep_KeyPress(sender As Object, e As KeyPressEventArgs) Handles grvDetailResep.KeyPress
        If Asc(e.KeyChar) = Keys.Tab Then
            SimpleButton1.Focus()
        End If
    End Sub
    Private Sub grvDiagnosaPenyerta_KeyPress(sender As Object, e As KeyPressEventArgs) Handles grvDiagnosaPenyerta.KeyPress
        If Asc(e.KeyChar) = Keys.Tab Then
            grdDetailResep.Focus()
        End If
    End Sub
    Private Sub grvTindakan_KeyPress(sender As Object, e As KeyPressEventArgs) Handles grvTindakan.KeyPress
        If Asc(e.KeyChar) = Keys.Tab Then
            btnSKD.Focus()
        End If
    End Sub
    Private Sub btnSKD_TabIndexChanged(sender As Object, e As EventArgs) Handles btnSKD.TabIndexChanged
        If txtTINDAKLANJUT.Text <> "" Then
            btnSaveClose_Click()
        End If
    End Sub
    Private Sub PasteToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PasteToolStripMenuItem.Click
        For Each iLoop In listCopy
            grvDetailResep.Focus()
            grvDetailResep.AddNewRow()

            grvDetailResep.SetFocusedRowCellValue(colKDITEM, iLoop.KDITEM)
            grvDetailResep.SetFocusedRowCellValue(colKDUOM, iLoop.KDUOM)
            grvDetailResep.SetFocusedRowCellValue(colKDSIGNA, iLoop.KDSIGNA)
            grvDetailResep.SetFocusedRowCellValue(colKDCARAPAKAI, iLoop.KDCARAPAKAI)
            grvDetailResep.SetFocusedRowCellValue(colQTY, iLoop.QTY)
            grvDetailResep.SetFocusedRowCellValue(colREMARKS_DOKTER, iLoop.REMARKS_DOKTER)
            grvDetailResep.UpdateCurrentRow()
        Next
    End Sub
    Private Sub fn_LoadTindakLanjut(ByVal KDREG As String)
        Dim oSkd As New Admission.clsSKD
        txtTINDAKLANJUT.ResetText()

        For Each xloop In oSkd.GetDataByKdregList(KDREG)
            Dim dsSKD = oSkd.GetData(xloop.KDSKD)
            If dsSKD IsNot Nothing Then
                If dsSKD.ALASAN = "KONTROL" Then
                    Dim sTindakLanjut As String = "Tanggal Kontrol : " & dsSKD.DATEKONTROL.ToString("dd-MM-yyyy") & vbCrLf & "Alasan : " & dsSKD.ALASAN.ToString.Trim & vbCrLf & "Tindak Lanjut : " & dsSKD.TINDAKLANJUT.ToString.Trim & vbCrLf & "Rencana Pemeriksaan : " & dsSKD.DESCRIPTION.ToString.Trim
                    txtTINDAKLANJUT.Text = IIf(txtTINDAKLANJUT.Text = "", "", txtTINDAKLANJUT.Text & vbCrLf & vbCrLf) & sTindakLanjut
                End If
                If dsSKD.ALASAN = "RUJUKAN EKSTERNAL" Then
                    Dim sTindakLanjut As String = "Tanggal Kontrol : " & dsSKD.DATEKONTROL.ToString("dd-MM-yyyy") & vbCrLf & "Alasan : " & dsSKD.ALASAN.ToString.Trim & vbCrLf & "Alasan di rujuk : " & dsSKD.DESCRIPTION.ToString.Trim
                    txtTINDAKLANJUT.Text = IIf(txtTINDAKLANJUT.Text = "", "", txtTINDAKLANJUT.Text & vbCrLf & vbCrLf) & sTindakLanjut
                End If
                If dsSKD.ALASAN = "RUJUKAN HABIS" Then
                    Dim sTindakLanjut As String = "Tanggal Kontrol : " & dsSKD.DATEKONTROL.ToString("dd-MM-yyyy") & vbCrLf & "Alasan : " & dsSKD.ALASAN.ToString.Trim & vbCrLf & "Tindak Lanjut : " & dsSKD.TINDAKLANJUT.ToString.Trim & vbCrLf & "Rencana Pemeriksaan : " & dsSKD.DESCRIPTION.ToString.Trim
                    txtTINDAKLANJUT.Text = IIf(txtTINDAKLANJUT.Text = "", "", txtTINDAKLANJUT.Text & vbCrLf & vbCrLf) & sTindakLanjut
                End If
                If dsSKD.ALASAN = "RUJUKAN INTERNAL" Then
                    Dim sTindakLanjut As String = "Tanggal Kontrol : " & dsSKD.DATEKONTROL.ToString("dd-MM-yyyy") & vbCrLf & "Alasan : " & dsSKD.ALASAN.ToString.Trim & vbCrLf & "Tujuan Poli : " & dsSKD.M_DEPARTMENT.NAME_DISPLAY.ToString.Trim & vbCrLf & "Dokter yang di tuju: " & dsSKD.M_DOCTOR.NAME_DISPLAY.ToString.Trim & vbCrLf & "Alasan di rujuk : " & dsSKD.DESCRIPTION.ToString.Trim
                    txtTINDAKLANJUT.Text = IIf(txtTINDAKLANJUT.Text = "", "", txtTINDAKLANJUT.Text & vbCrLf & vbCrLf) & sTindakLanjut
                End If
                If dsSKD.ALASAN = "PRB" Then
                    Dim sTindakLanjut As String = "Tanggal Kontrol : " & dsSKD.DATEKONTROL.ToString("dd-MM-yyyy") & vbCrLf & "Alasan : " & dsSKD.ALASAN.ToString.Trim & vbCrLf & "Alasan di rujuk : " & dsSKD.DESCRIPTION.ToString.Trim
                    txtTINDAKLANJUT.Text = IIf(txtTINDAKLANJUT.Text = "", "", txtTINDAKLANJUT.Text & vbCrLf & vbCrLf) & sTindakLanjut
                End If
                If dsSKD.ALASAN = "RUJUK BALIK" Then
                    Dim sTindakLanjut As String = "Tanggal Kontrol : " & dsSKD.DATEKONTROL.ToString("dd-MM-yyyy") & vbCrLf & "Alasan : " & dsSKD.ALASAN.ToString.Trim & vbCrLf & "Alasan di rujuk : " & dsSKD.DESCRIPTION.ToString.Trim
                    txtTINDAKLANJUT.Text = IIf(txtTINDAKLANJUT.Text = "", "", txtTINDAKLANJUT.Text & vbCrLf & vbCrLf) & sTindakLanjut
                End If
                If dsSKD.ALASAN = "SELESAI PENGOBATAN" Then
                    Dim sTindakLanjut As String = "Keterangan : " & dsSKD.ALASAN.ToString.Trim
                    txtTINDAKLANJUT.Text = IIf(txtTINDAKLANJUT.Text = "", "", txtTINDAKLANJUT.Text & vbCrLf & vbCrLf) & sTindakLanjut
                End If
                If dsSKD.ALASAN = "RAWAT INAP" Then
                    Dim sTindakLanjut As String = "Keterangan : " & dsSKD.ALASAN.ToString.Trim
                    txtTINDAKLANJUT.Text = IIf(txtTINDAKLANJUT.Text = "", "", txtTINDAKLANJUT.Text & vbCrLf & vbCrLf) & sTindakLanjut
                End If
                If dsSKD.ALASAN = "PENJADWALAN OPERASI" Then
                    Dim sTindakLanjut As String = "Keterangan : " & dsSKD.ALASAN.ToString.Trim
                    txtTINDAKLANJUT.Text = IIf(txtTINDAKLANJUT.Text = "", "", txtTINDAKLANJUT.Text & vbCrLf & vbCrLf) & sTindakLanjut & " " & sRemarks
                End If
            End If
        Next
    End Sub
    Private Sub btnSKD_Click(sender As Object, e As EventArgs) Handles btnSKD.Click
        If sRegister Is Nothing Then
            MsgBox("Register Kosong", MsgBoxStyle.Exclamation, Me.Text)
            Exit Sub
        End If

        Dim oSKD As New Admission.clsSKD
        Dim ds = oSKD.GetDataPendaftaranalasan(sRegister, "KONTROL")

        If ds IsNot Nothing Then
            Dim frmSKD As New frmSKD
            Try
                frmSKD.fn_LoadNoPendaftaran(sRegister, txtKDCUSTOMER.Text, sKDDEPARTMENT, sKDDOCTOR, sNOMORSEP, sPenjamin, txtNAMAPASIEN.Text, txtDIAGNOOSA.Text)
                frmSKD.fn_LoadKategori(0)
                frmSKD.LoadMe(FORM_MODE.FORM_MODE_EDIT, ds.KDSKD)
                frmSKD.ShowDialog(Me)
            Catch ex As Exception
                MsgBox("Load Form Detail : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
            Finally
                If Not frmSKD Is Nothing Then frmSKD.Dispose()
                frmSKD = Nothing
            End Try
        Else
            Dim frmSKD As New frmSKD
            Try
                frmSKD.fn_LoadNoPendaftaran(sRegister, txtKDCUSTOMER.Text, sKDDEPARTMENT, sKDDOCTOR, sNOMORSEP, sPenjamin, txtNAMAPASIEN.Text, txtDIAGNOOSA.Text)
                frmSKD.fn_LoadKategori(0)
                frmSKD.LoadMe(FORM_MODE.FORM_MODE_ADD)
                frmSKD.ShowDialog(Me)
            Catch ex As Exception
                MsgBox("Load Form Detail : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
            End Try
        End If

        fn_LoadTindakLanjut(sRegister)

        btnSaveClose_Click()

    End Sub
    Private Sub SimpleButton2_Click(sender As Object, e As EventArgs) Handles btnEksternal.Click
        If sRegister Is Nothing Then
            MsgBox("Register Kosong", MsgBoxStyle.Exclamation, Me.Text)
            Exit Sub
        End If

        Dim oSKD As New Admission.clsSKD
        Dim ds = oSKD.GetDataPendaftaranalasan(sRegister, "RUJUK")

        If ds IsNot Nothing Then
            Dim frmSKD As New frmSKD
            Try
                frmSKD.fn_LoadKategori(1)
                frmSKD.LoadMe(FORM_MODE.FORM_MODE_EDIT, ds.KDSKD)
                frmSKD.ShowDialog(Me)
            Catch ex As Exception
                MsgBox("Load Form Detail : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
            Finally
                If Not frmSKD Is Nothing Then frmSKD.Dispose()
                frmSKD = Nothing
            End Try
        Else
            Dim frmSKD As New frmSKD
            Try
                frmSKD.fn_LoadNoPendaftaran(sRegister, txtKDCUSTOMER.Text, sKDDEPARTMENT, sKDDOCTOR, sNOMORSEP, sPenjamin, txtNAMAPASIEN.Text, txtDIAGNOOSA.Text)
                frmSKD.fn_LoadKategori(1)
                frmSKD.LoadMe(FORM_MODE.FORM_MODE_ADD)
                frmSKD.ShowDialog(Me)
            Catch ex As Exception
                MsgBox("Load Form Detail : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
            End Try
        End If

        fn_LoadTindakLanjut(sRegister)

        btnSaveClose_Click()
    End Sub
    Private Sub SimpleButton4_Click(sender As Object, e As EventArgs) Handles btnRujukanHabis.Click
        If sRegister Is Nothing Then
            MsgBox("Register Kosong", MsgBoxStyle.Exclamation, Me.Text)
            Exit Sub
        End If

        Dim oSKD As New Admission.clsSKD
        Dim ds = oSKD.GetDataPendaftaranalasan(sRegister, "RUJUKAN HABIS")

        If ds IsNot Nothing Then
            Dim frmSKD As New frmSKD
            Try
                frmSKD.fn_LoadNoPendaftaran(sRegister, txtKDCUSTOMER.Text, sKDDEPARTMENT, sKDDOCTOR, sNOMORSEP, sPenjamin, txtNAMAPASIEN.Text, txtDIAGNOOSA.Text)
                frmSKD.fn_LoadKategori(2)
                frmSKD.LoadMe(FORM_MODE.FORM_MODE_EDIT, ds.KDSKD)
                frmSKD.ShowDialog(Me)
            Catch ex As Exception
                MsgBox("Load Form Detail : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
            Finally
                If Not frmSKD Is Nothing Then frmSKD.Dispose()
                frmSKD = Nothing
            End Try
        Else
            Dim frmSKD As New frmSKD
            Try
                frmSKD.fn_LoadNoPendaftaran(sRegister, txtKDCUSTOMER.Text, sKDDEPARTMENT, sKDDOCTOR, sNOMORSEP, sPenjamin, txtNAMAPASIEN.Text, txtDIAGNOOSA.Text)
                frmSKD.fn_LoadKategori(2)
                frmSKD.LoadMe(FORM_MODE.FORM_MODE_ADD)
                frmSKD.ShowDialog(Me)
            Catch ex As Exception
                MsgBox("Load Form Detail : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
            End Try
        End If

        fn_LoadTindakLanjut(sRegister)

        btnSaveClose_Click()
    End Sub
    Private Sub SimpleButton5_Click(sender As Object, e As EventArgs) Handles btnInternal.Click
        If sRegister Is Nothing Then
            MsgBox("Register Kosong", MsgBoxStyle.Exclamation, Me.Text)
            Exit Sub
        End If

        Dim oSKD As New Admission.clsSKD
        Dim ds = oSKD.GetDataPendaftaranalasan(sRegister, "RUJUK INTERNAL")

        If ds IsNot Nothing Then
            Dim frmSKD As New frmSKD
            Try
                frmSKD.fn_LoadKategori(3)
                frmSKD.LoadMe(FORM_MODE.FORM_MODE_EDIT, ds.KDSKD)
                frmSKD.ShowDialog(Me)
            Catch ex As Exception
                MsgBox("Load Form Detail : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
            Finally
                If Not frmSKD Is Nothing Then frmSKD.Dispose()
                frmSKD = Nothing
            End Try
        Else
            Dim frmSKD As New frmSKD
            Try
                frmSKD.fn_LoadNoPendaftaran(sRegister, txtKDCUSTOMER.Text, sKDDEPARTMENT, sKDDOCTOR, sNOMORSEP, sPenjamin, txtNAMAPASIEN.Text, txtDIAGNOOSA.Text)
                frmSKD.fn_LoadKategori(3)
                frmSKD.LoadMe(FORM_MODE.FORM_MODE_ADD)
                frmSKD.ShowDialog(Me)
            Catch ex As Exception
                MsgBox("Load Form Detail : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
            End Try
        End If

        fn_LoadTindakLanjut(sRegister)

        btnSaveClose_Click()
    End Sub
    Private Sub SimpleButton3_Click(sender As Object, e As EventArgs) Handles btnPRB.Click
        If sRegister Is Nothing Then
            MsgBox("Register Kosong", MsgBoxStyle.Exclamation, Me.Text)
            Exit Sub
        End If

        Dim oSKD As New Admission.clsSKD
        Dim ds = oSKD.GetDataPendaftaranalasan(sRegister, "PRB")

        If ds IsNot Nothing Then
            Dim frmSKD As New frmSKD
            Try
                frmSKD.fn_LoadKategori(4)
                frmSKD.LoadMe(FORM_MODE.FORM_MODE_EDIT, ds.KDSKD)
                frmSKD.ShowDialog(Me)
            Catch ex As Exception
                MsgBox("Load Form Detail : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
            Finally
                If Not frmSKD Is Nothing Then frmSKD.Dispose()
                frmSKD = Nothing
            End Try
        Else
            Dim frmSKD As New frmSKD
            Try
                frmSKD.fn_LoadNoPendaftaran(sRegister, txtKDCUSTOMER.Text, sKDDEPARTMENT, sKDDOCTOR, sNOMORSEP, sPenjamin, txtNAMAPASIEN.Text, txtDIAGNOOSA.Text)
                frmSKD.fn_LoadKategori(4)
                frmSKD.LoadMe(FORM_MODE.FORM_MODE_ADD)
                frmSKD.ShowDialog(Me)
            Catch ex As Exception
                MsgBox("Load Form Detail : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
            End Try
        End If

        fn_LoadTindakLanjut(sRegister)

        btnSaveClose_Click()
    End Sub
    Private Sub btnRujukBalik_Click(sender As Object, e As EventArgs) Handles btnRujukBalik.Click
        If sRegister Is Nothing Then
            MsgBox("Register Kosong", MsgBoxStyle.Exclamation, Me.Text)
            Exit Sub
        End If

        Dim oSKD As New Admission.clsSKD
        Dim ds = oSKD.GetDataPendaftaranalasan(sRegister, "RUJUK BALIK")

        If ds IsNot Nothing Then
            Dim frmSKD As New frmSKD
            Try
                frmSKD.fn_LoadKategori(5)
                frmSKD.LoadMe(FORM_MODE.FORM_MODE_EDIT, ds.KDSKD)
                frmSKD.ShowDialog(Me)
            Catch ex As Exception
                MsgBox("Load Form Detail : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
            Finally
                If Not frmSKD Is Nothing Then frmSKD.Dispose()
                frmSKD = Nothing
            End Try
        Else
            Dim frmSKD As New frmSKD
            Try
                frmSKD.fn_LoadNoPendaftaran(sRegister, txtKDCUSTOMER.Text, sKDDEPARTMENT, sKDDOCTOR, sNOMORSEP, sPenjamin, txtNAMAPASIEN.Text, txtDIAGNOOSA.Text)
                frmSKD.fn_LoadKategori(5)
                frmSKD.LoadMe(FORM_MODE.FORM_MODE_ADD)
                frmSKD.ShowDialog(Me)
            Catch ex As Exception
                MsgBox("Load Form Detail : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
            End Try
        End If

        fn_LoadTindakLanjut(sRegister)

        btnSaveClose_Click()
    End Sub
    Private Sub btnSelesaiPengobatan_Click(sender As Object, e As EventArgs) Handles btnSelesaiPengobatan.Click
        If sRegister Is Nothing Then
            MsgBox("Register Kosong", MsgBoxStyle.Exclamation, Me.Text)
            Exit Sub
        End If

        If MsgBox("Apakah Selesai Pengobatan?", MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, Me.Text) = MsgBoxResult.No Then Exit Sub

        Dim oSKD As New Admission.clsSKD
        Dim ds = oSKD.GetDataPendaftaranalasan(sRegister, "SELESAI PENGOBATAN")

        If ds IsNot Nothing Then
            fn_SaveSKD(False, ds.KDSKD, "SELESAI PENGOBATAN")
        Else
            fn_SaveSKD(True, "", "SELESAI PENGOBATAN")
        End If

        fn_LoadTindakLanjut(sRegister)

        btnSaveClose_Click()
    End Sub
    Private Sub btnRawatInap_Click(sender As Object, e As EventArgs) Handles btnRawatInap.Click
        If sRegister Is Nothing Then
            MsgBox("Register Kosong", MsgBoxStyle.Exclamation, Me.Text)
            Exit Sub
        End If

        If MsgBox("Apakah Rawat Inap?", MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, Me.Text) = MsgBoxResult.No Then Exit Sub

        Dim oSKD As New Admission.clsSKD
        Dim ds = oSKD.GetDataPendaftaranalasan(sRegister, "RAWAT INAP")

        If ds IsNot Nothing Then
            fn_SaveSKD(False, ds.KDSKD, "RAWAT INAP")
        Else
            fn_SaveSKD(True, "", "RAWAT INAP")
        End If

        Dim frmRemarks As New frmRemarks
        Try
            sRemarks = ""
            frmRemarks.ShowDialog(Me)
        Catch ex As Exception
            MsgBox("Load Form Detail : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try

        fn_LoadTindakLanjut(sRegister)

        btnSaveClose_Click()
    End Sub
    Private Function fn_SaveSKD(ByVal isAdd As Boolean, ByVal NOIID As String, ByVal ALASAN As String) As Boolean
        Try
            ' ***** HEADER *****
            Dim oSKD As New Admission.clsSKD
            Dim ds = oSKD.GetStructureHeader
            With ds
                Try
                    .DATECREATED = oSKD.GetData(NOIID).DATECREATED
                Catch oErr As Exception
                    .DATECREATED = Now
                End Try

                .DATEUPDATED = Now
                .KDSKD = NOIID
                .KDANTRIANMANUAL = 0
                Try
                    .KDPENDAFTARAN = oSKD.GetData(NOIID).KDPENDAFTARAN
                Catch oErr As Exception
                    .KDPENDAFTARAN = sRegister
                End Try
                .DATE = deDATE.DateTime
                .ISCATEGORY = 0
                Try
                    .KDDIAGNOSA = oSKD.GetData(NOIID).KDDIAGNOSA
                Catch oErr As Exception
                    .KDDIAGNOSA = "-"
                End Try
                .KDDEPARTMENT = sKDDEPARTMENT
                .KDDOCTOR = sKDDOCTOR
                .NOMORRUJUKAN = ""
                .DESCRIPTION = ""
                Try
                    .ISCHEKED = oSKD.GetData(NOIID).ISCHEKED
                Catch oErr As Exception
                    .ISCHEKED = False
                End Try
                .KDUSER = sUserID
                .DATEKONTROL = Now
                .ALASAN = ALASAN
                .TINDAKLANJUT = ""
                .TANGGALPERIKSA_TEXT = Now.ToString("ddMMyyyy")
                .KDJADWALDOKTER = 0
                .SEQ = 0
                .REQUEST = ""
                .RESPONSE = ""
                .NOMORSEP = ""
                .SEARCH = Now.ToString("ddMMyyyy")
                Try
                    .KDCUSTOMER = oSKD.GetData(NOIID).KDCUSTOMER
                Catch oErr As Exception
                    .KDCUSTOMER = txtKDCUSTOMER.Text
                End Try
                .ORDERPENUNJANG = ""
                .DATEKONTROL_2 = Now
            End With

            If isAdd = True Then
                Dim KDSKD As String = oSKD.InsertData(ds, NOIID)

                If KDSKD <> "" Then
                    fn_SaveSKD = True
                Else
                    fn_SaveSKD = False
                End If
            Else
                fn_SaveSKD = oSKD.UpdateData(ds)
            End If
        Catch oErr As Exception
            MsgBox("Save SKD" & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            fn_SaveSKD = False
        End Try
    End Function
    Private Sub btnPenjadwalanOperasi_Click(sender As Object, e As EventArgs) Handles btnPenjadwalanOperasi.Click
        If sRegister Is Nothing Then
            MsgBox("Register Kosong", MsgBoxStyle.Exclamation, Me.Text)
            Exit Sub
        End If

        If MsgBox("Apakah Penjadwalan Operasi?", MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, Me.Text) = MsgBoxResult.No Then Exit Sub

        Dim oSKD As New Admission.clsSKD
        Dim ds = oSKD.GetDataPendaftaranalasan(sRegister, "PENJADWALAN OPERASI")

        If ds IsNot Nothing Then
            fn_SaveSKD(False, ds.KDSKD, "PENJADWALAN OPERASI")
        Else
            fn_SaveSKD(True, "", "PENJADWALAN OPERASI")
        End If

        fn_LoadTindakLanjut(sRegister)

        btnSaveClose_Click()
    End Sub
    Private Sub SimpleButton2_Click_1(sender As Object, e As EventArgs) Handles SimpleButton2.Click
        Dim frmPopUp_Image As New frmPopUp_img
        frmPopUp_Image.fn_LoadMe(picGAMBAR2.Image)
        frmPopUp_Image.ShowDialog()

        If sPicture IsNot Nothing Then
            picGAMBAR2.Image = sPicture
        End If

        picGAMBAR2.Focus()
        'sPicture = Nothing
    End Sub
    Private Sub SimpleButton3_Click_1(sender As Object, e As EventArgs) Handles SimpleButton3.Click
        picGAMBAR2.Image = CType(My.Resources.ResourceManager.GetObject("gambar"), Image)
        sPicture = Nothing
    End Sub
    Private Sub cboPOLIMATA_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboPOLIMATA.SelectedIndexChanged
        grvTindakanPoli.Focus()
        grvTindakanPoli.AddNewRow()
        grvTindakanPoli.SetFocusedRowCellValue(colKETERANGAN, cboPOLIMATA.Text)
        grvTindakanPoli.UpdateCurrentRow()
    End Sub
    Private Sub SimpleButton4_Click_1(sender As Object, e As EventArgs) Handles SimpleButton4.Click
        If txtKDCUSTOMER.Text <> "" Then
            Dim frmRiwayatResep As New frmRiwayatResep
            Try
                frmRiwayatResep.LoadMe(txtKDCUSTOMER.Text, 1)
                frmRiwayatResep.ShowDialog(Me)

                If sNoTransaksi <> String.Empty Then
                    Dim oConn_1 As New SqlConnection
                    Dim oComm_1 As New SqlCommand
                    Dim da_1 As SqlDataAdapter
                    Dim ds_1 As New DataSet
                    Dim SQL_1 As String

                    oConn_1 = New SqlConnection(sConnOld)

                    If oConn_1.State = ConnectionState.Closed Then
                        oConn_1.Open()
                    End If

                    SQL_1 = "SELECT "
                    SQL_1 &= "* "
                    SQL_1 &= "FROM S_RECIPE_D AS A "
                    SQL_1 &= "WHERE A.KDRECIPE = '" & sNoTransaksi & "' "


                    oComm_1.Connection = oConn_1
                    oComm_1.CommandText = SQL_1
                    oComm_1.CommandTimeout = 120
                    oComm_1.CommandType = CommandType.Text

                    da_1 = New SqlDataAdapter(oComm_1)
                    da_1.Fill(ds_1, "S_RECIPE_D")

                    If oConn_1.State = ConnectionState.Open Then
                        oConn_1.Close()
                    End If

                    For xLoop As Integer = 0 To ds_1.Tables("S_RECIPE_D").Rows.Count - 1
                        With ds_1.Tables("S_RECIPE_D")
                            grvDetailResep.Focus()
                            grvDetailResep.AddNewRow()

                            grvDetailResep.SetFocusedRowCellValue(colKDITEM, .Rows(xLoop)("KDITEM"))
                            grvDetailResep.SetFocusedRowCellValue(colQTY, .Rows(xLoop)("QTY"))

                            Dim Tes As String = "-"
                            Try
                                Tes = .Rows(xLoop)("DESCRIPTION")
                            Catch ex As Exception

                            End Try
                            grvDetailResep.SetFocusedRowCellValue(colREMARKS_DOKTER, Tes)
                            grvDetailResep.SetFocusedRowCellValue(colKDUOM, .Rows(xLoop)("KDUOM"))
                            grvDetailResep.SetFocusedRowCellValue(colKDSIGNA, FormatNumber(.Rows(xLoop)("SIGNA_1"), 0) & "x" & FormatNumber(.Rows(xLoop)("SIGNA_2"), 0))
                            grvDetailResep.SetFocusedRowCellValue(colKDCARAPAKAI, Fn_carpakai(.Rows(xLoop)("KDCP")))
                            grvDetailResep.UpdateCurrentRow()

                        End With
                    Next
                End If

            Catch ex As Exception
                MsgBox("Load Form Riwayat Resep : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
            End Try
        Else
            MsgBox("Nomor Rekam Medis Kosong!!!", MsgBoxStyle.Exclamation, Me.Text)
        End If
    End Sub
    Private Function Fn_carpakai(ByVal kdcp As Integer) As String
        Try
            Fn_carpakai = ""

            Dim oConn_1 As New SqlConnection
            Dim oComm_1 As New SqlCommand
            Dim da_1 As SqlDataAdapter
            Dim ds_1 As New DataSet
            Dim SQL_1 As String

            oConn_1 = New SqlConnection(sConnOld)

            If oConn_1.State = ConnectionState.Closed Then
                oConn_1.Open()
            End If

            SQL_1 = "SELECT "
            SQL_1 &= "* "
            SQL_1 &= "FROM M_CARAPAKAI AS A "
            SQL_1 &= "WHERE A.KDCP = '" & kdcp & "' "

            oComm_1.Connection = oConn_1
            oComm_1.CommandText = SQL_1
            oComm_1.CommandTimeout = 120
            oComm_1.CommandType = CommandType.Text

            da_1 = New SqlDataAdapter(oComm_1)
            da_1.Fill(ds_1, "M_CARAPAKAI")

            If oConn_1.State = ConnectionState.Open Then
                oConn_1.Close()
            End If

            For xLoop As Integer = 0 To ds_1.Tables("M_CARAPAKAI").Rows.Count - 1
                With ds_1.Tables("M_CARAPAKAI")
                    Fn_carpakai = .Rows(xLoop)("DESCRIPTION")
                End With
            Next

        Catch ex As Exception
            Fn_carpakai = ""
        End Try
    End Function
#End Region
End Class