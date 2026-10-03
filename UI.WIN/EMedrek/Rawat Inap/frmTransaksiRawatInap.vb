Imports DataAccess
Imports System.Linq
Imports System.Data.SqlClient

Public Class frmTransaksiRawatInap
#Region "Declaration"
    Private oFormMode As FORM_MODE = FORM_MODE.FORM_MODE_VIEW
    Private isLoad As Boolean = False
    Private oCPPT As New Transaksi.clsCPPT
    Private sNoId As String
    Private sNoRegister As String = String.Empty
    Private sKDCUSTOMER As String = String.Empty
    Private sNAMAPASIEN As String = String.Empty
    Private sJENISKELAMIN As String = String.Empty
    Private sPENJAMIN As String = String.Empty
    Private sTANGGALLAHIR As DateTime = Now
    Private sUMUR As String = String.Empty
    Private sKodeCopy As String = String.Empty
    Private sTUJUAN As String = String.Empty
    Private sKDDOCTOR As String = String.Empty
#End Region
#Region "Function"
    Public Sub LoadMe(ByVal FormMode As Integer, ByVal isProfesiDokter As Boolean, ByVal CopyKode As String, ByVal KDREG As String, ByVal KDCUSTOMER As String, ByVal NAMAPASIEN As String, ByVal TANGGALLAHIR As DateTime, ByVal UMUR As String, ByVal JK As String, ByVal PENJAMIN As String, ByVal TUJUAN As String, ByVal kddoctor As String, ByVal NoId As String)
        oFormMode = FormMode
        sNoId = NoId
        sNoRegister = KDREG
        sKDCUSTOMER = KDCUSTOMER
        sNAMAPASIEN = NAMAPASIEN
        sJENISKELAMIN = JK
        sPENJAMIN = PENJAMIN
        sTANGGALLAHIR = TANGGALLAHIR
        sUMUR = UMUR
        sKodeCopy = CopyKode
        sTUJUAN = TUJUAN
        sKDDOCTOR = kddoctor

        If isProfesiDokter = True Then
            cboProfesi.SelectedIndex = 0
            cboProfesi.Properties.ReadOnly = True
        Else
            cboProfesi.ResetText()
            cboProfesi.Properties.ReadOnly = False
        End If
    End Sub
    Private Sub Form_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        fn_ChangeFormState()
        isLoad = True
    End Sub
    Private Sub Form_Closing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        Dispose()
    End Sub
    Private Overloads Sub Dispose()
        MyBase.Dispose()
        Me.Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
    Private Sub fn_ChangeFormState()
        fn_LoadITEM()
        fn_LoadUOM()
        fn_LoadSIGNA()
        fn_LoadCARAPAKAI()

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

        txtRegister.Properties.ReadOnly = True
        txtRUANGAN.Properties.ReadOnly = Status
        deDATE.Properties.ReadOnly = Status
        txtSUBJEKTIF.Properties.ReadOnly = Status
        chkALERGI_TIDAK.Properties.ReadOnly = Status
        chkALERGI_YA.Properties.ReadOnly = Status
        txtALERGI_TEXT.Properties.ReadOnly = Status
        txtOBJEKTIF_BERATBADAN.Properties.ReadOnly = Status
        txtOBJEKTIF_NADI.Properties.ReadOnly = Status
        txtOBJEKTIF_RESPIRASI.Properties.ReadOnly = Status
        txtOBJEKTIF_SATURASIOKSIGEN.Properties.ReadOnly = Status
        txtOBJEKTIF_SUHU.Properties.ReadOnly = Status
        txtOBJEKTIF_TEKANANDARAH.Properties.ReadOnly = Status
        txtOBJEKTIF_TINGGIBADAN.Properties.ReadOnly = Status
        txtPEMERIKSAANLAIN.Properties.ReadOnly = Status
        txtDIAGNOOSA.Properties.ReadOnly = Status
        grvDetailResep.OptionsBehavior.ReadOnly = Status
        grvPenunjang.OptionsBehavior.ReadOnly = Status
        grvTindakanPoli.OptionsBehavior.ReadOnly = Status
        txtINTRUKSILAIN.Properties.ReadOnly = Status
    End Sub
    Private Sub fn_EmptyMe()
        deDATE.DateTime = Now
        'cboProfesi.ResetText()
        txtRUANGAN.ResetText()
        txtSUBJEKTIF.ResetText()
        txtALERGI_TEXT.ResetText()
        txtOBJEKTIF_BERATBADAN.ResetText()
        txtOBJEKTIF_NADI.ResetText()
        txtOBJEKTIF_RESPIRASI.ResetText()
        txtOBJEKTIF_SATURASIOKSIGEN.ResetText()
        txtOBJEKTIF_SUHU.ResetText()
        txtOBJEKTIF_TEKANANDARAH.ResetText()
        txtOBJEKTIF_TINGGIBADAN.ResetText()
        txtPEMERIKSAANLAIN.ResetText()
        txtDIAGNOOSA.ResetText()
        txtINTRUKSILAIN.ResetText()

        txtRegister.Text = sNoRegister
        txtRUANGAN.Text = sTUJUAN

        fn_LoadDataCopy(sKodeCopy)
    End Sub
    Private Sub fn_LoadDataCopy(ByVal Parameter As String)
        Try
            If Parameter = "" Then Exit Sub

            ' ***** HEADER *****
            Dim dsLainnya = oCPPT.GetDataLainnya(Parameter)

            With dsLainnya
                chkALERGI_TIDAK.Checked = .ALERGI_TIDAK
                chkALERGI_YA.Checked = .ALERGI_YA
                txtALERGI_TEXT.Text = .ALERGI_TEXT
                txtOBJEKTIF_BERATBADAN.Text = .OBJEKTIF_BERATBADAN
                txtOBJEKTIF_NADI.Text = .OBJEKTIF_NADI
                txtOBJEKTIF_RESPIRASI.Text = .OBJEKTIF_RESPIRASI
                txtOBJEKTIF_SATURASIOKSIGEN.Text = .OBJEKTIF_SATURASIOKSIGEN
                txtOBJEKTIF_SUHU.Text = .OBJEKTIF_SUHU
                txtOBJEKTIF_TEKANANDARAH.Text = .OBJEKTIF_TEKANANDARAH
                txtOBJEKTIF_TINGGIBADAN.Text = .OBJEKTIF_TINGGIBADAN
                txtINTRUKSILAIN.Text = .INTRUKSILAIN
            End With

            Dim ds = oCPPT.GetData(Parameter)

            With ds
                txtRegister.Text = .KDPENDAFTARAN
                deDATE.DateTime = .DATE
                cboProfesi.Text = .PROFESI
                txtSUBJEKTIF.Text = .SUBJEKTIF
                txtPEMERIKSAANLAIN.Text = .ALAMAT
                txtDIAGNOOSA.Text = .ASSEMENT

                BindingSourceTindakan.DataSource = oCPPT.GetDataDetail(Parameter).OrderBy(Function(x) x.SEQ).ToList()
                grdTindakanPoli.DataSource = BindingSourceTindakan

                BindingSourcePenunjang.DataSource = oCPPT.GetDataDetail_BHP(Parameter).OrderBy(Function(x) x.SEQ).ToList()
                grdPenunjang.DataSource = BindingSourcePenunjang

                BindingSourceFarmasi.DataSource = oCPPT.GetDataDetailFarmasi(Parameter).OrderBy(Function(x) x.SEQ).ToList()
                grdDetailResep.DataSource = BindingSourceFarmasi

            End With

            Dim Total As Decimal = 0
            For Each xloop In oCPPT.GetDataDetailFarmasi(Parameter)
                Total += xloop.GRANDTOTAL
            Next

            txtGRANDTOTAL.Text = Total

        Catch oErr As Exception
            MsgBox("Load List Data : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadData()
        Try
            ' ***** HEADER *****
            Dim dsLainnya = oCPPT.GetDataLainnya(sNoId)

            With dsLainnya
                chkALERGI_TIDAK.Checked = .ALERGI_TIDAK
                chkALERGI_YA.Checked = .ALERGI_YA
                txtALERGI_TEXT.Text = .ALERGI_TEXT
                txtOBJEKTIF_BERATBADAN.Text = .OBJEKTIF_BERATBADAN
                txtOBJEKTIF_NADI.Text = .OBJEKTIF_NADI
                txtOBJEKTIF_RESPIRASI.Text = .OBJEKTIF_RESPIRASI
                txtOBJEKTIF_SATURASIOKSIGEN.Text = .OBJEKTIF_SATURASIOKSIGEN
                txtOBJEKTIF_SUHU.Text = .OBJEKTIF_SUHU
                txtOBJEKTIF_TEKANANDARAH.Text = .OBJEKTIF_TEKANANDARAH
                txtOBJEKTIF_TINGGIBADAN.Text = .OBJEKTIF_TINGGIBADAN
                txtINTRUKSILAIN.Text = .INTRUKSILAIN
            End With

            Dim ds = oCPPT.GetData(sNoId)

            With ds
                txtRegister.Text = .KDPENDAFTARAN
                deDATE.DateTime = .DATE
                cboProfesi.Text = .PROFESI
                txtSUBJEKTIF.Text = .SUBJEKTIF
                txtPEMERIKSAANLAIN.Text = .ALAMAT
                txtDIAGNOOSA.Text = .ASSEMENT
                txtRUANGAN.Text = .TEMPATLAHIR

                BindingSourceTindakan.DataSource = oCPPT.GetDataDetail(sNoId).OrderBy(Function(x) x.SEQ).ToList()
                grdTindakanPoli.DataSource = BindingSourceTindakan

                BindingSourcePenunjang.DataSource = oCPPT.GetDataDetail_BHP(sNoId).OrderBy(Function(x) x.SEQ).ToList()
                grdPenunjang.DataSource = BindingSourcePenunjang

                BindingSourceFarmasi.DataSource = oCPPT.GetDataDetailFarmasi(sNoId).OrderBy(Function(x) x.SEQ).ToList()
                grdDetailResep.DataSource = BindingSourceFarmasi

            End With

            Dim Total As Decimal = 0
            For Each xloop In oCPPT.GetDataDetailFarmasi(sNoId)
                Total += xloop.GRANDTOTAL
            Next

            txtGRANDTOTAL.Text = Total

        Catch oErr As Exception
            MsgBox("Load List Data : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Function ByteArrayToImage(ByVal byteArrayIn() As Byte) As Image
        Using ms As New System.IO.MemoryStream(byteArrayIn)
            Dim returnImage = Image.FromStream(ms)
            Return returnImage
        End Using
    End Function
    Private Function fn_Validate() As Boolean
        Try
            fn_Validate = True
            If txtRegister.Text = String.Empty Then
                MsgBox("Dibutuhkan Register", MsgBoxStyle.Exclamation, Me.Text)
                txtRegister.Focus()
                fn_Validate = False
                Exit Function
            End If
            If cboProfesi.Text = String.Empty Then
                MsgBox("Dibutuhkan Profesi", MsgBoxStyle.Exclamation, Me.Text)
                cboProfesi.Focus()
                fn_Validate = False
                Exit Function
            End If
            If txtRUANGAN.Text = String.Empty Then
                MsgBox("Dibutuhkan Ruangan", MsgBoxStyle.Exclamation, Me.Text)
                txtRUANGAN.Focus()
                fn_Validate = False
                Exit Function
            End If
        Catch oErr As Exception
            MsgBox("Validate Data : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Function
    Private Function fn_Save() As Boolean
        Try
            ' ***** HEADER *****

            Dim ds = oCPPT.GetStructureHeader
            With ds
                Try
                    .DATECREATED = oCPPT.GetData(sNoId).DATECREATED
                Catch ex As Exception
                    .DATECREATED = Now
                End Try
                .DATEUPDATED = Now
                .DATE = deDATE.DateTime
                .KDCPPT = sNoId
                .KDCUSTOMER = sKDCUSTOMER
                .KDPENDAFTARAN = txtRegister.Text
                .PROFESI = cboProfesi.Text
                .NAMAPASIEN = sNAMAPASIEN
                .JK = sJENISKELAMIN
                .NIK = ""
                .TEMPATLAHIR = txtRUANGAN.Text
                .TANGGALLAHIR = sTANGGALLAHIR
                .AGAMA = ""
                .PENJAMIN = sPENJAMIN
                .NOTELEPON = ""
                .SUKU = ""
                .ALAMAT = txtPEMERIKSAANLAIN.Text
                .SUBJEKTIF = txtSUBJEKTIF.Text

                Dim listSUBOBJEKTIF As New List(Of String)

                listSUBOBJEKTIF.Add("Umur : " & sUMUR)
                listSUBOBJEKTIF.Add("BB : " & txtOBJEKTIF_BERATBADAN.Text & " kg")
                listSUBOBJEKTIF.Add("TT : " & txtOBJEKTIF_TINGGIBADAN.Text & " cm")
                listSUBOBJEKTIF.Add("Tekanan Darah : " & txtOBJEKTIF_TEKANANDARAH.Text & " mmHg")
                listSUBOBJEKTIF.Add("Nadi : " & txtOBJEKTIF_NADI.Text & " x/mnt")
                listSUBOBJEKTIF.Add("Respirasi : " & txtOBJEKTIF_RESPIRASI.Text & " x/mnt")
                listSUBOBJEKTIF.Add("Saturasi Oksigen : " & txtOBJEKTIF_SATURASIOKSIGEN.Text & " %")
                listSUBOBJEKTIF.Add("Suhu : " & txtOBJEKTIF_SUHU.Text & " oC")
                listSUBOBJEKTIF.Add("Pemeriksaan : " & txtPEMERIKSAANLAIN.Text)

                .OBJEKTIF = String.Join(vbCrLf, listSUBOBJEKTIF.ToArray)
                .ASSEMENT = txtDIAGNOOSA.Text

                Dim listTindakanPoli As New List(Of String)
                Dim TindakanPoli As String = String.Empty
                For i As Integer = 0 To grvTindakanPoli.RowCount - 2
                    listTindakanPoli.Add(grvTindakanPoli.GetRowCellValue(i, colTINDAKAN))
                Next

                If listTindakanPoli.Count > 0 Then
                    TindakanPoli = "Tindakan di Poli : " & String.Join(vbCrLf, listTindakanPoli.ToArray)
                End If

                Dim listPemeriksaanPenunjang As New List(Of String)
                Dim PemeriksaanPenunjang As String = String.Empty
                For i As Integer = 0 To grvPenunjang.RowCount - 2
                    listPemeriksaanPenunjang.Add(grvPenunjang.GetRowCellValue(i, colPENUNJANG))
                Next

                If listPemeriksaanPenunjang.Count > 0 Then
                    PemeriksaanPenunjang = "Pemeriksaan Penunjang : " & String.Join(vbCrLf, listPemeriksaanPenunjang.ToArray)
                End If

                Dim listPemeriksaanObat As New List(Of String)
                Dim PemeriksaanObat As String = String.Empty
                For i As Integer = 0 To grvDetailResep.RowCount - 2
                    listPemeriksaanObat.Add(fn_LoadITEM(grvDetailResep.GetRowCellValue(i, colKDITEM)) & " " & IIf(String.IsNullOrEmpty(grvDetailResep.GetRowCellValue(i, colKDSIGNA)), "", fn_LoadSIGNA(grvDetailResep.GetRowCellValue(i, colKDSIGNA))) & " " & IIf(String.IsNullOrEmpty(grvDetailResep.GetRowCellValue(i, colKDCARAPAKAI)), "", fn_LoadCARAPAKAI(grvDetailResep.GetRowCellValue(i, colKDCARAPAKAI))))
                Next

                If listPemeriksaanObat.Count > 0 Then
                    PemeriksaanObat = "Medikamentosa : " & String.Join(vbCrLf, listPemeriksaanObat.ToArray)
                End If

                .PLANNING = TindakanPoli & vbCrLf & vbCrLf & PemeriksaanPenunjang & vbCrLf & vbCrLf & PemeriksaanObat & vbCrLf & vbCrLf & txtINTRUKSILAIN.Text
                .KDUSER = sUserID
                .ISCHEKED = False
            End With

            Dim dsLainnya = oCPPT.GetStructureHeaderlainnya
            With dsLainnya
                .KDCPPT = ds.KDCPPT
                .ALERGI_TIDAK = chkALERGI_TIDAK.Checked
                .ALERGI_YA = chkALERGI_YA.Checked
                .ALERGI_TEXT = txtALERGI_TEXT.Text
                .OBJEKTIF_JENISKELAMIN = sJENISKELAMIN
                .OBJEKTIF_UMUR = sUMUR

                .OBJEKTIF_BERATBADAN = txtOBJEKTIF_BERATBADAN.Text
                .OBJEKTIF_NADI = txtOBJEKTIF_NADI.Text
                .OBJEKTIF_RESPIRASI = txtOBJEKTIF_RESPIRASI.Text
                .OBJEKTIF_SATURASIOKSIGEN = txtOBJEKTIF_SATURASIOKSIGEN.Text
                .OBJEKTIF_SUHU = txtOBJEKTIF_SUHU.Text
                .OBJEKTIF_TEKANANDARAH = txtOBJEKTIF_TEKANANDARAH.Text
                .OBJEKTIF_TINGGIBADAN = txtOBJEKTIF_TINGGIBADAN.Text
                .INTRUKSILAIN = txtINTRUKSILAIN.Text
            End With

            Dim arrDetail = oCPPT.GetStructureDetailFarmasiList
            For i As Integer = 0 To grvDetailResep.RowCount - 2
                Dim dsDetail = oCPPT.GetStructureDetailFarmasi
                With dsDetail
                    .SEQ = i
                    .KDCPPT = ds.KDCPPT
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
                End With

                arrDetail.Add(dsDetail)
            Next

            Dim arrDetailTindakanPoli = oCPPT.GetStructureDetailList
            For i As Integer = 0 To grvTindakanPoli.RowCount - 2
                Dim dsDetail = oCPPT.GetStructureDetail
                With dsDetail
                    .SEQ = i
                    .KDCPPT = ds.KDCPPT
                    .TINDAKAN = grvTindakanPoli.GetRowCellValue(i, colTINDAKAN)
                End With
                arrDetailTindakanPoli.Add(dsDetail)
            Next

            Dim arrDetailPenunjang = oCPPT.GetStructureDetailBHPList
            For i As Integer = 0 To grvPenunjang.RowCount - 2
                Dim dsDetail = oCPPT.GetStructureDetailBHP
                With dsDetail
                    .SEQ = i
                    .KDCPPT = ds.KDCPPT
                    .PENUNJANG = grvPenunjang.GetRowCellValue(i, colPENUNJANG)
                End With
                arrDetailPenunjang.Add(dsDetail)
            Next

            If oFormMode = FORM_MODE.FORM_MODE_ADD Then
                Try
                    fn_Save = oCPPT.InsertData(ds, dsLainnya, arrDetailTindakanPoli, arrDetail, arrDetailPenunjang)

                    fn_SimpanCPPTSelesai(ds.DATE.ToString("yyyyMMdd"), ds.KDCPPT, ds.KDPENDAFTARAN, sKDDOCTOR, cboProfesi.Text)
                Catch ex As Exception
                    MsgBox("Simpan Data : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            ElseIf oFormMode = FORM_MODE.FORM_MODE_EDIT Then
                Try
                    fn_Save = oCPPT.UpdateData(ds, dsLainnya, arrDetailTindakanPoli, arrDetail, arrDetailPenunjang)
                Catch ex As Exception
                    MsgBox("Simpan Data : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            End If
        Catch oErr As Exception
            MsgBox("Simpan Data : " & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            fn_Save = False
        End Try
    End Function
    Private Sub fn_SimpanCPPTSelesai(ByVal TANGGAL As String, ByVal KDCPPT As String, ByVal KDREG As String, ByVal KDDCOTOR As String, ByVal DESCRIPTION As String)
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

            SQL = "INSERT INTO I_TRACKING_CPPT (TANGGAL, KDCPPT, KDREG, KDDOCTOR, DESCRIPTION) "
            SQL &= "VALUES ('" & TANGGAL & "', '" & KDCPPT & "', '" & KDREG & "', '" & KDDCOTOR & "','" & DESCRIPTION & "')"

            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "INSERT_I_TRACKING_CPPT")

            If oConn.State = ConnectionState.Open Then
                oConn.Close()
            End If
        Catch oErr As Exception
            MsgBox("Insert I_TRACKING_CPPT : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
#End Region
#Region "Command Button"
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
            SQL &= ",HARGA = B.PRICEPURCHASESTANDARD "
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

            If oConn.State = ConnectionState.Open Then
                oConn.Close()
            End If
        Catch oErr As Exception
            MsgBox("Load Obat Data : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
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

            If oConn.State = ConnectionState.Open Then
                oConn.Close()
            End If
        Catch oErr As Exception
            MsgBox("Load Satuan Data : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
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
    Private Function fn_LoadITEM(ByVal KDITEM As String) As String
        Try
            fn_LoadITEM = ""

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
                    fn_LoadUOMKDUOMHARGA = .Rows(iLoop)("PRICESALESSTANDARD")
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
    Private Sub OnValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles grvDetailResep.FocusedRowChanged
        If isLoad Then
            Calculate()
        End If
    End Sub
    Private Sub Calculate()
        Dim sSubTotal As Decimal = 0
        Dim list As New List(Of String)

        For i As Integer = 0 To grvDetailResep.RowCount - 2
            sSubTotal += CDec(grvDetailResep.GetRowCellValue(i, colGRANDTOTAL))
        Next

        txtGRANDTOTAL.Text = sSubTotal
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
            grvDetailResep.SetFocusedRowCellValue(colPRICE, fn_LoadUOMKDUOMHARGA(grvDetailResep.GetFocusedRowCellValue(colKDITEM), grvDetailResep.GetFocusedRowCellValue(colKDUOM)))
        ElseIf e.Column.Name = colQTY.Name Or e.Column.Name = colPRICE.Name Then
            Dim sSubTotal As Decimal = CDec(grvDetailResep.GetFocusedRowCellValue(colQTY)) * CDec(grvDetailResep.GetFocusedRowCellValue(colPRICE))
            grvDetailResep.SetFocusedRowCellValue(colGRANDTOTAL, sSubTotal)
        End If
    End Sub
    Private Sub frmItem_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        Select Case e.KeyCode
            Case Keys.F12
                btnClose_Click()
            Case Keys.F3
                If btnSaveClose.Enabled = True Then
                    btnSaveClose_Click()
                End If
        End Select
    End Sub
    Private Sub btnSaveClose_Click() Handles btnSaveClose.ItemClick
        If fn_Validate() = False Then Exit Sub
        If MsgBox("Save?", MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, Me.Text) = MsgBoxResult.No Then Exit Sub
        If fn_Save() = False Then
            MsgBox("Save gagal! Tolong kontak admin anda!", MsgBoxStyle.Exclamation, Me.Text)
        Else
            MsgBox("Save success!", MsgBoxStyle.Information, Me.Text)
            Me.Close()
        End If
    End Sub
    Private Sub btnClose_Click() Handles btnClose.ItemClick
        Me.Close()
    End Sub
#End Region
#Region "Lookup / Event"
    Private Sub DeleteToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DeleteToolStripMenuItemTindakan.Click
        grvTindakanPoli.DeleteSelectedRows()
    End Sub
    Private Sub ToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItemPenunjang.Click
        grvPenunjang.DeleteSelectedRows()
    End Sub
    Private Sub ToolStripMenuItem2_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItemFarmasi.Click
        grvDetailResep.DeleteSelectedRows()
    End Sub
#End Region
End Class