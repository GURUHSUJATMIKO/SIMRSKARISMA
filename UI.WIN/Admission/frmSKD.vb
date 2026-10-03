Imports DataAccess
Imports UI.WIN.MAIN.My.Resources
Imports System.Linq
Imports Newtonsoft.Json.Linq
Imports System.Data.SqlClient
Imports System.Globalization
Imports iTextSharp.text

Public Class frmSKD
#Region "Declaration"
    Private oFormMode As FORM_MODE = FORM_MODE.FORM_MODE_VIEW
    Private sNoId As String
    Private isLoad As Boolean = False
    Private oSKD As New Admission.clsSKD
    Private sKDDIAGNOSA As String = String.Empty
    Private sNAMADIAGNOSA As String = String.Empty
    Private sKDPENDAFTARAN As String = String.Empty
    Private sKDCUSTOMER As String = String.Empty
    Private sKDDEPARTMENT As String = String.Empty
    Private sKDDOCTOR As String = String.Empty
    Private sNOMORSEP As String = String.Empty
    Private sPENJAMIN As String = String.Empty
    Private sNAMAPASIEN As String = String.Empty
    Private ssAve As Boolean = False
    Private sKategori As Integer = 0
    Private oOrderPenunjang As New Order.clsOrderPenunjang

#End Region
#Region "Function"
    Public Sub fn_LoadNoPendaftaran(ByVal KDPENDAFTARAN As String, ByVal KDCUSTOMER As String, ByVal KDDEPARTMENT As String, ByVal KDDOCTOR As String, ByVal NOMORSEP As String, ByVal PENJAMIN As String, ByVal NAMAPASIEN As String, ByVal DIAGNOSA As String)
        sKDPENDAFTARAN = KDPENDAFTARAN
        sKDCUSTOMER = KDCUSTOMER
        sKDDEPARTMENT = KDDEPARTMENT
        sKDDOCTOR = KDDOCTOR
        sKDDIAGNOSA = "-"
        sNOMORSEP = NOMORSEP
        sPENJAMIN = PENJAMIN
        sNAMAPASIEN = NAMAPASIEN
        sNAMADIAGNOSA = DIAGNOSA
    End Sub
    Public Sub fn_LoadNoPendaftaranPolidanDokter(ByVal Parameter1 As String, ByVal Parameter2 As String)
        grdKDDEPARTMENT.Text = Parameter1
        grdKDDOCTOR.Text = Parameter2
    End Sub
    Public Sub fn_LoadKategori(ByVal Parameter1 As Integer)
        sKategori = Parameter1
    End Sub
    Public Sub LoadMe(ByVal FormMode As Integer, Optional ByVal NoId As String = "")
        oFormMode = FormMode
        sNoId = NoId
    End Sub
    Private Sub Form_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        fn_ChangeFormState()
        fn_LoadLanguage()
        isLoad = True
        txtCODE.Focus()
    End Sub
    Public Sub fn_LoadLanguage()
        Try
            lDATE_KONTROL_2.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never

            If sKategori = 0 Then
                Me.Text = "Rencana Kontrol Selanjutnya - Edit Form"

                lKDDEPARTMENT.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                lSEARCHTEXT.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never

                lKDSKD.Text = SKD.KDSKD
                lDATE.Text = SKD.TANGGAL
                lDATE_KONTROL.Text = SKD.TANGGAL_KONTROL
                lKDDEPARTMENT.Text = SKD.KDDEPARTMENT
                lKDDOCTOR.Text = SKD.KDDOCTOR
                lDESCRIPTION.Text = "Rencana Pemeriksaan Saat Kontrol Selanjutnya"
                lALASAN.Text = SKD.ALASAN
                lTINDAKLANJUT.Text = SKD.TINDAKLANJUT

            ElseIf sKategori = 1 Then
                Me.Text = "Rujuk Eksternal - Edit Form"

                lKDDEPARTMENT.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                lSEARCHTEXT.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always

                lKDSKD.Text = SKD.KDSKD
                lDATE.Text = SKD.TANGGAL
                lDATE_KONTROL.Text = SKD.TANGGAL_KONTROL
                lKDDEPARTMENT.Text = "Poli Tujuan"
                lKDDOCTOR.Text = SKD.KDDOCTOR
                lDESCRIPTION.Text = "Alasan di rujuk"
                lALASAN.Text = SKD.ALASAN
                lTINDAKLANJUT.Text = "Nama RS"
            ElseIf sKategori = 2 Then
                Me.Text = "Rujukan Habis - Edit Form"

                lKDDEPARTMENT.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                lSEARCHTEXT.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never

                lKDSKD.Text = SKD.KDSKD
                lDATE.Text = SKD.TANGGAL
                lDATE_KONTROL.Text = SKD.TANGGAL_KONTROL
                lKDDEPARTMENT.Text = SKD.KDDEPARTMENT
                lKDDOCTOR.Text = SKD.KDDOCTOR
                lDESCRIPTION.Text = "Rencana Pemeriksaan Saat Kontrol Selanjutnya"
                lALASAN.Text = SKD.ALASAN
                lTINDAKLANJUT.Text = SKD.TINDAKLANJUT
            ElseIf sKategori = 3 Then
                'Me.Text = "Rujuk Internal Bersama - Edit Form"

                Me.Text = "Konsul Internal - Edit Form"

                lKDDEPARTMENT.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                lSEARCHTEXT.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                lTINDAKLANJUT.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never

                lKDSKD.Text = SKD.KDSKD
                lDATE.Text = SKD.TANGGAL
                lDATE_KONTROL.Text = SKD.TANGGAL_KONTROL
                lKDDEPARTMENT.Text = SKD.KDDEPARTMENT
                lKDDOCTOR.Text = "Dokter Yang di Tuju"
                lDESCRIPTION.Text = "Alasan di Konsul"
                lALASAN.Text = SKD.ALASAN
                lTINDAKLANJUT.Text = SKD.TINDAKLANJUT
            ElseIf sKategori = 4 Then
                Me.Text = "PRB - Edit Form"

                lKDDEPARTMENT.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                lSEARCHTEXT.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never

                lKDSKD.Text = SKD.KDSKD
                lDATE.Text = SKD.TANGGAL
                lDATE_KONTROL.Text = SKD.TANGGAL_KONTROL
                lKDDEPARTMENT.Text = SKD.KDDEPARTMENT
                lKDDOCTOR.Text = SKD.KDDOCTOR
                lDESCRIPTION.Text = "Alasan di rujuk "
                lALASAN.Text = SKD.ALASAN
                lTINDAKLANJUT.Text = SKD.TINDAKLANJUT
            ElseIf sKategori = 5 Then
                Me.Text = "RUJUK BALIK - Edit Form"

                lKDDEPARTMENT.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                lSEARCHTEXT.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never

                lKDSKD.Text = SKD.KDSKD
                lDATE.Text = SKD.TANGGAL
                lDATE_KONTROL.Text = SKD.TANGGAL_KONTROL
                lKDDEPARTMENT.Text = SKD.KDDEPARTMENT
                lKDDOCTOR.Text = SKD.KDDOCTOR
                lDESCRIPTION.Text = "Alasan di rujuk"
                lALASAN.Text = SKD.ALASAN
                lTINDAKLANJUT.Text = SKD.TINDAKLANJUT
            ElseIf sKategori = 6 Then
                Me.Text = "SELESAI PENGOBATAN - Edit Form"

                lKDDEPARTMENT.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                lSEARCHTEXT.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                lDATE_KONTROL.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never

                lKDSKD.Text = SKD.KDSKD
                lDATE.Text = SKD.TANGGAL
                lDATE_KONTROL.Text = SKD.TANGGAL_KONTROL
                lKDDEPARTMENT.Text = SKD.KDDEPARTMENT
                lKDDOCTOR.Text = SKD.KDDOCTOR
                lDESCRIPTION.Text = "Keterangan"
                lALASAN.Text = SKD.ALASAN
                lTINDAKLANJUT.Text = SKD.TINDAKLANJUT
            ElseIf sKategori = 7 Then
                'NAIK RANAP
            ElseIf sKategori = 8 Then
                Me.Text = "Rujuk Internal Alih Rawat - Edit Form"

                lKDDEPARTMENT.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                lSEARCHTEXT.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                lTINDAKLANJUT.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never

                lKDSKD.Text = SKD.KDSKD
                lDATE.Text = SKD.TANGGAL
                lDATE_KONTROL.Text = SKD.TANGGAL_KONTROL
                lKDDEPARTMENT.Text = SKD.KDDEPARTMENT
                lKDDOCTOR.Text = "Dokter Yang di Tuju"
                lDESCRIPTION.Text = "Alasan di rujuk"
                lALASAN.Text = SKD.ALASAN
                lTINDAKLANJUT.Text = SKD.TINDAKLANJUT
            ElseIf sKategori = 9 Then
                Me.Text = "1 Kali Iterasi - Edit Form"

                lKDDEPARTMENT.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                lSEARCHTEXT.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                lTINDAKLANJUT.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                'lDATE_KONTROL_2.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always

                lKDSKD.Text = SKD.KDSKD
                lDATE.Text = SKD.TANGGAL
                lDATE_KONTROL.Text = "Tanggal Iterasi Ke 1 :"
                lKDDEPARTMENT.Text = "Asal Poli"
                lKDDOCTOR.Text = "Dokter Pemberi Iterasi"
                lDESCRIPTION.Text = "Alasan di Iterasi"
                lALASAN.Text = SKD.ALASAN
                lTINDAKLANJUT.Text = SKD.TINDAKLANJUT
            ElseIf sKategori = 10 Then
                Me.Text = "2 Kali Iterasi - Edit Form"

                lKDDEPARTMENT.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                lSEARCHTEXT.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                lTINDAKLANJUT.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                lDATE_KONTROL_2.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always

                lKDSKD.Text = SKD.KDSKD
                lDATE.Text = SKD.TANGGAL
                lDATE_KONTROL.Text = "Tanggal Iterasi Ke 1 :"
                lKDDEPARTMENT.Text = "Asal Poli"
                lKDDOCTOR.Text = "Dokter Pemberi Iterasi"
                lDESCRIPTION.Text = "Alasan di Iterasi"
                lALASAN.Text = SKD.ALASAN
                lTINDAKLANJUT.Text = SKD.TINDAKLANJUT
            End If

            btnSaveNew.Caption = Caption.FormSaveNew
            btnSaveClose.Caption = Caption.FormSaveClose
            btnClose.Caption = Caption.FormClose
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub Form_Closing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        sCode = IIf(txtCODE.Text = "<--- AUTO --->", "", txtCODE.Text.Trim.ToUpper)
    End Sub
    Private Sub fn_ChangeFormState()
        fn_LoadKDDEPARTMENT()
        fn_LoadKDDOCTOR()
        ssAve = False

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
        btnPenunjang.Enabled = Not Status

        If oFormMode = FORM_MODE.FORM_MODE_ADD Then
            txtCODE.Properties.ReadOnly = False
        Else
            txtCODE.Properties.ReadOnly = True
        End If
        deDATE.Properties.ReadOnly = True
        grdKDDEPARTMENT.Properties.ReadOnly = Status
        grdKDDOCTOR.Properties.ReadOnly = Status
        txtDESCRIPTION.Properties.ReadOnly = Status
        txtALASAN.Properties.ReadOnly = True
        txtTINDAKLANJUT.Properties.ReadOnly = Status
        txtNomorSepaAtauKartu.Properties.ReadOnly = Status
        txtSEARCH.Properties.ReadOnly = Status
        txtORDERPENUNJANG.Properties.ReadOnly = Status

        'If sKategori = 0 Or sKategori = 2 Then
        '    lOrderLab.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        '    lOrderRad.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        '    lOrder.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        'Else
        '    lOrderLab.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        '    lOrderRad.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        '    lOrder.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        'End If

        lOrderLab.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        lOrderRad.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        lOrder.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
    End Sub
    Private Sub fn_EmptyMe()
        txtCODE.Text = "<--- AUTO --->"

        deDATE.DateTime = Now
        deDATEKONTROL.DateTime = Now
        deDATEKONTROL_2.DateTime = Now

        txtDESCRIPTION.Text = "-"
        txtTINDAKLANJUT.Text = "-"
        txtNomorSepaAtauKartu.Text = sNOMORSEP
        grdKDDEPARTMENT.Text = sKDDEPARTMENT
        grdKDDOCTOR.Text = sKDDOCTOR
        txtSEARCH.ResetText()
        txtORDERPENUNJANG.ResetText()

        txtALASAN.SelectedIndex = sKategori

        If txtALASAN.Text = "KONTROL" Then

        ElseIf txtALASAN.Text = "RUJUKAN EKSTERNAL" Then

        ElseIf txtALASAN.Text = "RUJUKAN HABIS" Then
            txtTINDAKLANJUT.Text = "KEMBALI KE FASKES 1 UNTUK MEMPERPANJANG RUJUKAN"
        ElseIf txtALASAN.Text = "SELESAI PENGOBATAN" Then
            'txtTINDAKLANJUT.Text = "SELESAI PENGOBATAN"
        End If

        If grdKDDEPARTMENT.Text = "IGD" Then
            txtTINDAKLANJUT.Text = "Setempat"
            txtSEARCH.Text = "igd"
            txtDESCRIPTION.Text = "Rawat inap penuh"
        End If

        If txtALASAN.Text.Contains("ITERASI") Then
            txtSEARCH.Text = "FARMASI"

            Dim budaya As New CultureInfo("id-ID")

            Dim TanggalAwal As DateTime = Now.AddDays(30)
            Dim TanggalAwal2 As DateTime = Now.AddDays(60)

            Dim namaHari1 As String = TanggalAwal.ToString("dddd", budaya).ToUpper
            If namaHari1 = "SABTU" Then
                'TanggalAwal = TanggalAwal.AddDays(2)
            ElseIf namaHari1 = "MINGGU" Then
                TanggalAwal = TanggalAwal.AddDays(1)
            End If

            deDATEKONTROL.DateTime = TanggalAwal
            deDATEKONTROL_2.DateTime = TanggalAwal

            If txtALASAN.Text = "ITERASI2" Then

                Dim namaHari2 As String = TanggalAwal2.ToString("dddd", budaya).ToUpper
                If namaHari2 = "SABTU" Then
                    'TanggalAwal2 = TanggalAwal2.AddDays(2)
                ElseIf namaHari2 = "MINGGU" Then
                    TanggalAwal2 = TanggalAwal2.AddDays(1)
                End If
                deDATEKONTROL_2.DateTime = TanggalAwal2
            End If

        End If
    End Sub
    Private Sub fn_LoadData()
        Try
            Dim ds = oSKD.GetData(sNoId)

            With ds
                txtCODE.Text = sNoId
                txtDESCRIPTION.Text = .DESCRIPTION
                deDATE.DateTime = .DATE
                deDATEKONTROL.DateTime = .DATEKONTROL
                grdKDDEPARTMENT.Text = .KDDEPARTMENT
                grdKDDOCTOR.Text = .KDDOCTOR
                txtALASAN.Text = .ALASAN
                txtTINDAKLANJUT.Text = .TINDAKLANJUT
                txtNomorSepaAtauKartu.Text = .NOMORSEP
                txtSEARCH.Text = .REQUEST
                txtORDERPENUNJANG.Text = .ORDERPENUNJANG
                deDATEKONTROL_2.DateTime = .DATEKONTROL_2
            End With
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Function fn_Validate() As Boolean
        Try
            fn_Validate = True
            If oFormMode = FORM_MODE.FORM_MODE_ADD Then
                If sKDPENDAFTARAN = String.Empty Then
                    MsgBox("Nomor Pendaftaran Kosong", MsgBoxStyle.Exclamation, Me.Text)
                    fn_Validate = False
                    Exit Function
                End If
            End If

            If txtALASAN.Text = String.Empty Then
                txtALASAN.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                txtALASAN.ErrorText = Statement.ErrorRequired

                txtALASAN.Focus()
                fn_Validate = False
                Exit Function
            End If
            If txtTINDAKLANJUT.Text = String.Empty Then
                txtTINDAKLANJUT.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                txtTINDAKLANJUT.ErrorText = Statement.ErrorRequired

                txtTINDAKLANJUT.Focus()
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
            If grdKDDOCTOR.Text = String.Empty Then
                grdKDDOCTOR.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                grdKDDOCTOR.ErrorText = Statement.ErrorRequired

                grdKDDOCTOR.Focus()
                fn_Validate = False
                Exit Function
            End If

            'Dim dsPendaftaran = oSKD.GetDataByRegister(sKDPENDAFTARAN)
            'If dsPendaftaran IsNot Nothing Then
            '    MsgBox("Surat Kontrol Sudah dibuat, silahkan edit transaksi", MsgBoxStyle.Exclamation, Me.Text)
            '    fn_Validate = False
            '    Exit Function
            'End If

            If oFormMode = FORM_MODE.FORM_MODE_ADD Then
                Dim oDoctor As New Reference.clsDoctor
                Dim dsHariLibur = oDoctor.GetDataJadwalLibur(deDATEKONTROL.DateTime.ToString("yyyyMMdd"))

                If dsHariLibur IsNot Nothing Then
                    MsgBox("Tanggal " & dsHariLibur.DATE.ToString("dd-MM-yyyy") & " " & dsHariLibur.MEMO & " Silahkan Pilih Tanggal Lain", MsgBoxStyle.Exclamation, Me.Text)
                    deDATEKONTROL.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                    deDATEKONTROL.ErrorText = Statement.ErrorRequired

                    deDATEKONTROL.Focus()
                    fn_Validate = False
                    Exit Function
                End If

                If txtALASAN.Text = "ITERASI2" Then
                    Dim dsHariLibur2 = oDoctor.GetDataJadwalLibur(deDATEKONTROL_2.DateTime.ToString("yyyyMMdd"))
                    If dsHariLibur2 IsNot Nothing Then
                        MsgBox("Tanggal " & dsHariLibur2.DATE.ToString("dd-MM-yyyy") & " " & dsHariLibur2.MEMO & " Silahkan Pilih Tanggal Lain", MsgBoxStyle.Exclamation, Me.Text)
                        deDATEKONTROL_2.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                        deDATEKONTROL_2.ErrorText = Statement.ErrorRequired

                        deDATEKONTROL_2.Focus()
                        fn_Validate = False
                        Exit Function
                    End If
                End If
            End If

        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Function
    Private Function fn_RequestInsertRencanaKontrol(ByVal sNoSEP As String) As String
        Try
            Dim oDoctor As New Reference.clsDoctor
            Dim jsonRequest As String = String.Empty
            Dim oDepartment As New Reference.clsDepartment

            jsonRequest = "{ "
            jsonRequest &= " ""request"" :   { "
            jsonRequest &= " ""noSEP"" :  """ & sNoSEP & """ , "
            jsonRequest &= " ""kodeDokter"" :  """ & oDoctor.GetData(grdKDDOCTOR.EditValue).VCLAIM_KDDPJP & """ , "
            jsonRequest &= " ""poliKontrol"" :  """ & oDepartment.GetData(grdKDDEPARTMENT.EditValue).KDDEPARTMENT_BPJS & """ , "
            jsonRequest &= " ""tglRencanaKontrol"" :  """ & deDATEKONTROL.DateTime.ToString("yyyy-MM-dd") & """ , "
            jsonRequest &= " ""user"" :  """ & sUserID & """ "
            jsonRequest &= " } "
            jsonRequest &= " } "

            fn_RequestInsertRencanaKontrol = jsonRequest

        Catch oErr As Exception
            fn_RequestInsertRencanaKontrol = ""
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Function
    Private Function fn_InsertRencanaKontrol(ByVal jsonRequest As String, ByVal uTime As Integer) As String
        Try
            Dim oSetKoneksi As New Brigging.clsSetKoneksi
            If sConsidVclaim <> "" Then
                Dim dsSetKoneksi = oSetKoneksi.InsertRencanaKontrol(sUrlVclaim, sConsidVclaim, sSecreateKeyVclaim, sUserKeyVclaim, uTime, jsonRequest)

                If dsSetKoneksi <> "" Then
                    Dim allData = JObject.Parse(dsSetKoneksi)

                    Dim CodeResponse As String = String.Empty
                    Dim messageResponse As String = String.Empty

                    CodeResponse = IIf(IsDBNull(allData.Item("metaData").Item("code")) = True, "", allData.Item("metaData").Item("code"))
                    messageResponse = allData("metaData")("message").ToString

                    If CodeResponse = "200" Then
                        fn_InsertRencanaKontrol = allData("response")
                    Else
                        fn_InsertRencanaKontrol = ""
                        MsgBox(CodeResponse & " - " & messageResponse, MsgBoxStyle.Exclamation, Me.Text)
                        Exit Function
                    End If
                Else
                    fn_InsertRencanaKontrol = ""
                    MsgBox("Insert Rencana Kontrol Gagal", MsgBoxStyle.Exclamation, Me.Text)
                End If
            Else
                fn_InsertRencanaKontrol = ""
                MsgBox("Consid Kosong", MsgBoxStyle.Exclamation, Me.Text)
            End If
        Catch oErr As Exception
            fn_InsertRencanaKontrol = ""
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Function
    Private Function fn_RequestUpdateRencanaKontrol(ByVal sNoSEP As String) As String
        Try
            Dim oDoctor As New Reference.clsDoctor
            Dim jsonRequest As String = String.Empty
            Dim oDepartment As New Reference.clsDepartment

            jsonRequest = "{ "
            jsonRequest &= " ""request"" :   { "
            jsonRequest &= " ""noSuratKontrol"" :  """ & txtCODE.Text & """ , "
            jsonRequest &= " ""noSEP"" :  """ & sNoSEP & """ , "
            jsonRequest &= " ""kodeDokter"" :  """ & oDoctor.GetData(grdKDDOCTOR.EditValue).VCLAIM_KDDPJP & """ , "
            jsonRequest &= " ""poliKontrol"" :  """ & oDepartment.GetData(grdKDDEPARTMENT.EditValue).KDDEPARTMENT_BPJS & """ , "
            jsonRequest &= " ""tglRencanaKontrol"" :  """ & deDATEKONTROL.DateTime.ToString("yyyy-MM-dd") & """ , "
            jsonRequest &= " ""user"" :  """ & sUserID & """ "
            jsonRequest &= " } "
            jsonRequest &= " } "

            fn_RequestUpdateRencanaKontrol = jsonRequest

        Catch oErr As Exception
            fn_RequestUpdateRencanaKontrol = ""
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Function
    Private Function fn_UpdateRencanaKontrol(ByVal jsonRequest As String, ByVal uTime As Integer) As String
        Try
            Dim oSetKoneksi As New Brigging.clsSetKoneksi

            If sConsidVclaim <> "" Then
                Dim dsSetKoneksi = oSetKoneksi.UpdateRencanaKontrol(sUrlVclaim, sConsidVclaim, sSecreateKeyVclaim, sUserKeyVclaim, uTime, jsonRequest)

                If dsSetKoneksi <> "" Then
                    Dim allData = JObject.Parse(dsSetKoneksi)

                    Dim CodeResponse As String = String.Empty
                    Dim messageResponse As String = String.Empty

                    CodeResponse = IIf(IsDBNull(allData.Item("metaData").Item("code")) = True, "", allData.Item("metaData").Item("code"))
                    messageResponse = allData("metaData")("message").ToString

                    If CodeResponse = "200" Then
                        fn_UpdateRencanaKontrol = allData("response")
                    Else
                        fn_UpdateRencanaKontrol = ""
                        MsgBox(CodeResponse & " - " & messageResponse, MsgBoxStyle.Exclamation, Me.Text)
                    End If
                Else
                    fn_UpdateRencanaKontrol = ""
                    MsgBox("Insert Rencana Kontrol Gagal", MsgBoxStyle.Exclamation, Me.Text)
                End If
            Else
                fn_UpdateRencanaKontrol = ""
                MsgBox("Consid Kosong", MsgBoxStyle.Exclamation, Me.Text)
            End If
        Catch oErr As Exception
            fn_UpdateRencanaKontrol = ""
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Function
    Private Function fn_Save() As Boolean
        Try
            ' ******** Insert BPJS
            Dim jsonRequest As String = String.Empty
            Dim jsonResponse As String = String.Empty
            Dim oSetKoneksi As New Brigging.clsSetKoneksi

            If txtALASAN.Text = "KONTROL" Then
                Dim uTime As Integer = (DateTime.UtcNow - New DateTime(1970, 1, 1, 0, 0, 0)).TotalSeconds

                If sPENJAMIN = "BPJS KESEHATAN" Then
                    If txtNomorSepaAtauKartu.Text = "" Then
                        If oFormMode = FORM_MODE.FORM_MODE_ADD Then
                            txtCODE.ResetText()
                        End If
                    Else
                        If oFormMode = FORM_MODE.FORM_MODE_ADD Then
                            jsonRequest = fn_RequestInsertRencanaKontrol(txtNomorSepaAtauKartu.Text)

                            If jsonRequest <> "" Then
                                jsonResponse = fn_InsertRencanaKontrol(jsonRequest, uTime)
                                If jsonResponse <> "" Then
                                    Dim DataDecrypt = JObject.Parse(oSetKoneksi.Decrypt(jsonResponse, sConsidVclaim & sSecreateKeyVclaim & uTime))
                                    txtCODE.Text = DataDecrypt.Item("noSuratKontrol").ToString()
                                Else
                                    fn_Save = False
                                    Exit Function
                                End If
                            Else
                                fn_Save = False
                                Exit Function
                            End If
                        Else
                            jsonRequest = fn_RequestUpdateRencanaKontrol(txtNomorSepaAtauKartu.Text)
                            If jsonRequest <> "" Then
                                jsonResponse = fn_UpdateRencanaKontrol(jsonRequest, uTime)
                                If jsonResponse <> "" Then

                                Else
                                    fn_Save = False
                                    Exit Function
                                End If
                            Else
                                fn_Save = False
                                Exit Function
                            End If
                        End If

                        txtSEARCH.Text = jsonRequest
                    End If
                Else
                    If oFormMode = FORM_MODE.FORM_MODE_ADD Then
                        txtCODE.ResetText()
                    End If
                End If
            Else
                If oFormMode = FORM_MODE.FORM_MODE_ADD Then
                    txtCODE.ResetText()
                End If
            End If

            ' ***** HEADER *****
            Dim ds = oSKD.GetStructureHeader
            With ds
                Try
                    .DATECREATED = oSKD.GetData(sNoId).DATECREATED
                Catch oErr As Exception
                    .DATECREATED = Now
                End Try

                .DATEUPDATED = Now
                .KDSKD = txtCODE.Text
                .KDANTRIANMANUAL = 0
                Try
                    .KDPENDAFTARAN = oSKD.GetData(sNoId).KDPENDAFTARAN
                Catch oErr As Exception
                    .KDPENDAFTARAN = sKDPENDAFTARAN
                End Try
                .DATE = deDATE.DateTime
                .ISCATEGORY = 0
                Try
                    .KDDIAGNOSA = oSKD.GetData(sNoId).KDDIAGNOSA
                Catch oErr As Exception
                    .KDDIAGNOSA = sKDDIAGNOSA
                End Try
                .KDDEPARTMENT = grdKDDEPARTMENT.EditValue
                .KDDOCTOR = grdKDDOCTOR.EditValue
                .NOMORRUJUKAN = sNAMAPASIEN
                .DESCRIPTION = txtDESCRIPTION.Text.Trim
                Try
                    .ISCHEKED = oSKD.GetData(sNoId).ISCHEKED
                Catch oErr As Exception
                    .ISCHEKED = False
                End Try
                .KDUSER = sUserID
                .DATEKONTROL = deDATEKONTROL.DateTime
                .ALASAN = txtALASAN.Text.Trim.ToUpper
                .TINDAKLANJUT = txtTINDAKLANJUT.Text.Trim
                .TANGGALPERIKSA_TEXT = deDATEKONTROL.DateTime.ToString("ddMMyyyy")
                .KDJADWALDOKTER = 0
                .SEQ = 0
                .REQUEST = txtSEARCH.Text
                .RESPONSE = jsonResponse
                .NOMORSEP = txtNomorSepaAtauKartu.Text
                .SEARCH = deDATEKONTROL.DateTime.ToString("ddMMyyyy")
                Try
                    .KDCUSTOMER = oSKD.GetData(sNoId).KDCUSTOMER
                Catch oErr As Exception
                    .KDCUSTOMER = sKDCUSTOMER
                End Try
                .ORDERPENUNJANG = txtORDERPENUNJANG.Text
                .DATEKONTROL_2 = deDATEKONTROL_2.DateTime
            End With

            If oFormMode = FORM_MODE.FORM_MODE_ADD Then
                Dim KDSKD As String = oSKD.InsertData(ds, txtCODE.Text)

                If KDSKD <> "" Then
                    txtCODE.Text = KDSKD
                    fn_PrintStruk7(txtCODE.Text)
                    fn_Save = True
                    ssAve = True
                Else
                    ssAve = False
                    fn_Save = False
                End If
            ElseIf oFormMode = FORM_MODE.FORM_MODE_EDIT Then
                fn_Save = oSKD.UpdateData(ds)
                fn_PrintStruk7(txtCODE.Text)
                ssAve = True
            End If
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            fn_Save = False
            ssAve = False
        End Try
    End Function
    Private Function fn_PrintStruk7(ByVal sCode As String) As Boolean

    End Function
#End Region
#Region "Command Button"
    Private Sub btnOrderLaboratorium_Click(sender As Object, e As EventArgs) Handles btnOrderLaboratorium.Click
        'Dim frmOrderPenunjangLab As New frmOrderPenunjangLab
        'Try
        '    frmOrderPenunjangLab.LoadMe("LABORATORIUM", sNoId, grdKDDOCTOR.EditValue, 0, 0)
        '    frmOrderPenunjangLab.ShowDialog(Me)
        'Catch ex As Exception
        '    MsgBox("Load Form Detail : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
        'Finally
        '    If Not frmOrderPenunjangLab Is Nothing Then frmOrderPenunjangLab.Dispose()
        '    frmOrderPenunjangLab = Nothing

        '    If listOrderLab.Count > 0 Then
        '        txtORDERPENUNJANG.Text = "Laboratorium" & vbCrLf & String.Join(vbCrLf, listOrderLab.ToArray)
        '    End If
        '    If listOrderRad.Count > 0 Then
        '        txtORDERPENUNJANG.Text = "Laboratorium" & vbCrLf & String.Join(vbCrLf, listOrderRad.ToArray)
        '    End If
        'End Try
    End Sub
    Private Sub btnOrderRadiologi_Click(sender As Object, e As EventArgs) Handles btnOrderRadiologi.Click
        'Dim frmOrderPenunjangLab As New frmOrderPenunjangLab
        'Try
        '    frmOrderPenunjangLab.LoadMe("RADIOLOGI", sNoId, grdKDDOCTOR.EditValue, 0, 0)
        '    frmOrderPenunjangLab.ShowDialog(Me)
        'Catch ex As Exception
        '    MsgBox("Load Form Detail : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
        'Finally
        '    If Not frmOrderPenunjangLab Is Nothing Then frmOrderPenunjangLab.Dispose()
        '    frmOrderPenunjangLab = Nothing

        '    If listOrderLab.Count > 0 Then
        '        txtORDERPENUNJANG.Text = "Laboratorium" & vbCrLf & String.Join(vbCrLf, listOrderLab.ToArray)
        '    End If
        '    If listOrderRad.Count > 0 Then
        '        txtORDERPENUNJANG.Text = "Laboratorium" & vbCrLf & String.Join(vbCrLf, listOrderRad.ToArray)
        '    End If
        'End Try
    End Sub
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
            Case Keys.F5
                If btnPenunjang.Enabled = True Then
                    btnPenunjang_Click()
                End If
                'Case Keys.F10
                '    If btnCari.Enabled = True Then
                '        btnCari_Click()
                '    End If
        End Select
    End Sub
    Private Sub btnPenunjang_Click() Handles btnPenunjang.ItemClick
        Dim frmReportOrderPenunjang As New frmReportOrderPenunjang
        Try
            sListRincianLab.Clear()
            sListRincianRad.Clear()

            frmReportOrderPenunjang.ShowDialog(Me)

            Dim list As New List(Of String)

            For Each xloop In sListRincianLab
                list.Add("LABORATORIUM " & xloop)
            Next

            For Each xloop In sListRincianRad
                list.Add("RADIOLOGI " & xloop)
            Next

            txtDESCRIPTION.Text = String.Join(vbCrLf, list.ToArray)
        Catch ex As Exception
            MsgBox("Load Form Detail Penunjang: " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub picSimpan_Click() Handles picSimpan.Click
        If fn_Validate() = False Then Exit Sub
        If MsgBox(Statement.SaveQuestion, MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, Me.Text) = MsgBoxResult.No Then Exit Sub
        If fn_Save() = False Then
            MsgBox(Statement.SaveFail, MsgBoxStyle.Exclamation, Me.Text)
        Else
            MsgBox("Nomor SKD " & txtCODE.Text, MsgBoxStyle.Information, Me.Text)
            sStatusSave = "NEW"
            Me.Close()
        End If
    End Sub
    Private Sub btnCari_Click() Handles btnCari.ItemClick
        frmReportRencaKontrol.ShowDialog(Me)
    End Sub
    Private Sub btnSaveNew_Click() Handles btnSaveNew.ItemClick
        If fn_Validate() = False Then Exit Sub
        If MsgBox(Statement.SaveQuestion, MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, Me.Text) = MsgBoxResult.No Then Exit Sub
        If fn_Save() = False Then
            MsgBox(Statement.SaveFail, MsgBoxStyle.Exclamation, Me.Text)
        Else
            MsgBox("Nomor SKD " & txtCODE.Text, MsgBoxStyle.Information, Me.Text)
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
            MsgBox("Nomor SKD " & txtCODE.Text, MsgBoxStyle.Information, Me.Text)
            Me.Close()
        End If
    End Sub
    Private Sub btnClose_Click() Handles btnClose.ItemClick
        Me.Close()
    End Sub
#End Region
#Region "Lookup / Event"
    Private Sub fn_LoadKDDEPARTMENT()
        'Dim oDEPARTMENT As New Reference.clsDepartment
        'Try
        '    grdKDDEPARTMENT.Properties.DataSource = oDEPARTMENT.GetData.Where(Function(x) x.ISACTIVE = True).ToList()
        '    grdKDDEPARTMENT.Properties.ValueMember = "KDDEPARTMENT"
        '    grdKDDEPARTMENT.Properties.DisplayMember = "NAME_DISPLAY"

        'Catch oErr As Exception
        '    MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        'End Try
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
            SQL &= "FROM  "
            SQL &= "M_DEPARTMENT "
            SQL &= "WHERE "
            SQL &= "ISACTIVE = '1' "

            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "M_DEPARTMENT")

            If oConn.State = ConnectionState.Open Then
                oConn.Close()
            End If

            grdKDDEPARTMENT.Properties.DataSource = ds.Tables("M_DEPARTMENT")
            grdKDDEPARTMENT.Properties.ValueMember = "KDDEPARTMENT"
            grdKDDEPARTMENT.Properties.DisplayMember = "NAME_DISPLAY"


        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadKDDOCTOR()
        'Dim oDoctor As New Reference.clsDoctor
        Try
            'grdKDDOCTOR.Properties.DataSource = oDoctor.GetData.Where(Function(x) x.ISACTIVE = True).ToList()
            'grdKDDOCTOR.Properties.ValueMember = "KDDOCTOR"
            'grdKDDOCTOR.Properties.DisplayMember = "NAME_DISPLAY"

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
            SQL &= "FROM  "
            SQL &= "M_DOCTOR "
            SQL &= "WHERE "
            SQL &= "ISACTIVE = '1' "

            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "M_DOCTOR")


            If oConn.State = ConnectionState.Open Then
                oConn.Close()
            End If

            grdKDDOCTOR.Properties.DataSource = ds.Tables("M_DOCTOR")
            grdKDDOCTOR.Properties.ValueMember = "KDDOCTOR"
            grdKDDOCTOR.Properties.DisplayMember = "NAME_DISPLAY"


        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadKDDOCTOR2(ByVal KDDEPARTMENT As String)
        'Dim oDoctor As New Reference.clsDoctor
        Try
            'grdKDDOCTOR.Properties.DataSource = oDoctor.GetData.Where(Function(x) x.ISACTIVE = True And x.KDDEPARTMENT = KDDEPARTMENT).ToList()
            'grdKDDOCTOR.Properties.ValueMember = "KDDOCTOR"
            'grdKDDOCTOR.Properties.DisplayMember = "NAME_DISPLAY"

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
            SQL &= "FROM  "
            SQL &= "M_DOCTOR "
            SQL &= "WHERE "
            SQL &= "ISACTIVE = '1' "
            SQL &= "AND KDDEPARTMENT = '" & KDDEPARTMENT & "' "

            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "M_DOCTOR")

            If oConn.State = ConnectionState.Open Then
                oConn.Close()
            End If

            grdKDDOCTOR.Properties.DataSource = ds.Tables("M_DOCTOR")
            grdKDDOCTOR.Properties.ValueMember = "KDDOCTOR"
            grdKDDOCTOR.Properties.DisplayMember = "NAME_DISPLAY"

        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub txtDESCRIPTION_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtDESCRIPTION.KeyPress
        If Asc(e.KeyChar) = Keys.Tab Then
            btnSaveClose_Click()
        End If
    End Sub
    Private Sub txtDESCRIPTION_TabIndexChanged(sender As Object, e As EventArgs) Handles txtDESCRIPTION.TabIndexChanged
        If isLoad = True Then
            btnSaveClose_Click()
        End If
    End Sub
    Private Sub txtDESCRIPTION_TabStopChanged(sender As Object, e As EventArgs) Handles txtDESCRIPTION.TabStopChanged
        If isLoad = True Then
            btnSaveClose_Click()
        End If
    End Sub
    Private Sub grdKDDEPARTMENT_EditValueChanged(sender As Object, e As EventArgs) Handles grdKDDEPARTMENT.EditValueChanged
        If isLoad = True Then
            If sKategori = 3 Then
                If grdKDDEPARTMENT.Text <> "" Then
                    fn_LoadKDDOCTOR2(grdKDDEPARTMENT.EditValue)
                End If
            End If
        End If
    End Sub
#End Region
End Class