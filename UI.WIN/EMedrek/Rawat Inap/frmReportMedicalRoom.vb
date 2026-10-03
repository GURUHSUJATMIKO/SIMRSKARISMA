Imports DataAccess
Imports UI.WIN.MAIN.My.Resources
Imports System.Linq
Imports System.Data
Imports System.Data.SqlClient

Public Class frmReportMedicalRoom
    Private oFormMode As FORM_MODE = FORM_MODE.FORM_MODE_VIEW
    Private sIsDOkter As Boolean = False
    Private sJenisRawat As Integer = 0
    Private sCasmix As Boolean = False
    Private sTYPE As Integer = 0
#Region "Function"
    Public Sub fn_LoadMe(ByVal isDOKTER As Boolean, ByVal isJenisrawat As Integer)
        deDATEFROM.DateTime = Now
        deDATETO.DateTime = Now

        sIsDOkter = isDOKTER
        If isDOKTER = False Then
            chkAll.Checked = True
            fn_LoadKDUSER()
        End If

        sJenisRawat = isJenisrawat

        fn_LoadDokter()
    End Sub
    Private Sub fn_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.Text = "PEMETAAN RUANGAN - " & IIf(sIsDOkter = False, "PERAWAT", "DOKTER")
        fn_LoadFilter()
        'fn_LoadSecurity()
    End Sub
    Private Sub fn_LoadSecurity()
        Try
            Dim oOtority As New Setting.clsOtority
            Dim oUser As New Setting.clsUser

            Dim ds = (From x In oOtority.GetDataDetail
                      Join y In oUser.GetData
                      On x.KDOTORITY Equals y.KDOTORITY
                      Where x.MODUL = "MEDREK_RJ" _
                      And y.KDUSER = sUserID
                      Select x.ISADD, x.ISDELETE, x.ISUPDATE, x.ISPRINT, x.ISVIEW).FirstOrDefault

            Try
                If ds.ISVIEW = True Then
                    fn_Preview()
                End If

            Catch ex As Exception
                MsgBox("Keamanan belum dipasang, tolong kontak admin anda!", MsgBoxStyle.Exclamation, Me.Text)

                'picAdd.Enabled = False
                'picDelete.Enabled = False
                'picUpdate.Enabled = False
                'picPrint.Enabled = False
                'picRefresh.Enabled = False
            End Try

            Dim dsCasemix = (From x In oOtority.GetDataDetail
                             Join y In oUser.GetData
                             On x.KDOTORITY Equals y.KDOTORITY
                             Where x.MODUL = "CASEMIX" _
                             And y.KDUSER = sUserID
                             Select x.ISADD, x.ISDELETE, x.ISUPDATE, x.ISPRINT, x.ISVIEW).FirstOrDefault

            If dsCasemix IsNot Nothing Then
                sCasmix = True
            Else
                sCasmix = False
            End If

        Catch ex As Exception
            MsgBox("Load Security : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadFilter()
        If sIsDOkter = False Then
            lblPERAWAT.Visible = True
            grdUSERHADNOVER.Visible = True
        Else
            lblPERAWAT.Visible = False
            grdUSERHADNOVER.Visible = False
        End If
    End Sub
    Private Function fn_Validate() As Boolean
        fn_Validate = True

        'If deDATETo.DateTime.ToString("yyyyMMdd") < deDATEFrom.DateTime.ToString("yyyyMMdd") Then
        '    MsgBox("Tanggal Sampai harus lebih besar dari Tanggal Dari!", MsgBoxStyle.OkOnly, Me.Text)
        '    fn_Validate = False
        '    Exit Function
        'End If
    End Function
    Private Sub fn_Print()
        'If fn_Validate() Then
        '    Try
        '        PrintableComponentLink.Landscape = True
        '        PrintableComponentLink.PaperKind = Printing.PaperKind.A4

        '        Dim phf As PageHeaderFooter =
        '    TryCast(PrintableComponentLink.PageHeaderFooter, PageHeaderFooter)
        '        phf.Header.Content.Clear()
        '        phf.Header.Font = New Font("Times New Roman", 14, FontStyle.Bold)
        '        phf.Header.LineAlignment = BrickAlignment.Center
        '        phf.Footer.Font = New Font("Times New Roman", 9.75)
        '        phf.Footer.LineAlignment = BrickAlignment.Far
        '        phf.Footer.Content.AddRange(New String() _
        '    {"", "", "Halaman: [Page # of Pages #]"})

        '        Select Case cboType.SelectedIndex
        '            Case 0
        '                phf.Header.Content.AddRange(New String() _
        '    {"", "LAPORAN MEDICAL ROOM (REKAP)", ""})

        '        End Select

        '        PrintableComponentLink.CreateDocument()
        '        PrintableComponentLink.ShowPreview()
        '    Catch ex As Exception
        '        MsgBox("Print Data : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
        '    End Try
        'End If
    End Sub
    Private Sub fn_Preview()
        If fn_Validate() Then
            Try
                grv.Columns.Clear()
                grd.DataSource = Nothing
                grv.OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleIfExpanded

                fn_LoadData01(chkAll.Checked)

            Catch oErr As Exception
                MsgBox("Preview Data : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            End Try
        End If
    End Sub
    Private Sub fn_LoadData01(ByVal isAll As Boolean)
        Try
            If grdDPJPUtama.Text = "" Then Exit Sub

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
            SQL &= "* FROM ( "
            SQL &= "SELECT "
            SQL &= "A.KDMROOM "
            SQL &= ",NamaKamar = A.LOKASI + '-' + A.NAME_DISPLAY "
            SQL &= ",NamaKamarBed = A.LOKASI + '-' + A.NAME_DISPLAY + ' Bed ' + CONVERT(NVARCHAR(50), C.SEQ) "
            SQL &= ",NomorBed = C.SEQ "
            SQL &= ",TanggalMasuk = ISNULL((SELECT FORMAT(DATE, 'dd/MM/yyyy HH:mm:ss') FROM S_PENDAFTARAN_H WHERE C.KDREG = KDREG), '') "
            SQL &= ",TANGGALMASUK_ = ISNULL((SELECT DATE FROM S_PENDAFTARAN_H WHERE C.KDREG = KDREG), '') "
            SQL &= ",ISPULANG = ISNULL((SELECT ISUPDATEPULANG FROM S_PENDAFTARAN_H WHERE C.KDREG = KDREG), '') "
            SQL &= ",DATEPULANG = ISNULL((SELECT DATEPULANG FROM S_PENDAFTARAN_H WHERE C.KDREG = KDREG), '') "
            SQL &= ",KelasRawat = B.NAME_DISPLAY "
            SQL &= ",NoTransaksi = C.KDREG "
            SQL &= ",NoTransaksi_Poli = ISNULL((SELECT KDREGAWAL FROM S_PENDAFTARAN_H WHERE C.KDREG = KDREG), '') "
            SQL &= ",NoTransaksi_PoliNama = ISNULL((SELECT BB.NAME_DISPLAY FROM S_PENDAFTARAN_H AA INNER JOIN M_DEPARTMENT BB ON AA.KDDEPARTMENT = BB.KDDEPARTMENT WHERE C.KDREG = AA.KDREG), '') "
            SQL &= ",NoRM = ISNULL((SELECT KDCUSTOMER FROM S_PENDAFTARAN_H WHERE C.KDREG = KDREG), '') "
            SQL &= ",KARTUBPJS = ISNULL((SELECT KARTUBPJS FROM S_PENDAFTARAN_H WHERE C.KDREG = KDREG), '') "
            SQL &= ",NOMORSEP = ISNULL((SELECT NOMORSEP FROM S_PENDAFTARAN_H WHERE C.KDREG = KDREG), '') "
            SQL &= ",KDKELASRAWAT = ISNULL((SELECT BB.DESCRIPTION FROM S_PENDAFTARAN_H AA INNER JOIN M_KELASRAWAT BB ON AA.KDKELASRAWAT = BB.KDKELASRAWAT WHERE C.KDREG = AA.KDREG ), '') "
            SQL &= ",NamaPasien = RTRIM(ISNULL((SELECT BB.NAME_DISPLAY FROM S_PENDAFTARAN_H AA INNER JOIN M_CUSTOMER BB ON AA.KDCUSTOMER = BB.KDCUSTOMER WHERE C.KDREG = AA.KDREG), '')) "
            SQL &= ",JenisKelamin = ISNULL((SELECT CASE BB.JK WHEN '1' THEN 'Laki-laki' ELSE 'Perempuan' END FROM S_PENDAFTARAN_H AA INNER JOIN M_CUSTOMER BB ON AA.KDCUSTOMER = BB.KDCUSTOMER WHERE C.KDREG = AA.KDREG), '') "
            SQL &= ",TanggalLahir = ISNULL((SELECT BB.TANGGALLAHIR FROM S_PENDAFTARAN_H AA INNER JOIN M_CUSTOMER BB ON AA.KDCUSTOMER = BB.KDCUSTOMER WHERE C.KDREG = AA.KDREG), '') "
            SQL &= ",Penjamin = ISNULL((SELECT BB.NAME_DISPLAY FROM S_PENDAFTARAN_H AA INNER JOIN M_DEBTOR BB ON AA.KDDEBTOR = BB.KDDEBTOR WHERE C.KDREG = AA.KDREG), '') "
            SQL &= ",KDDOCTOR_DPJPUTAMA = ISNULL((SELECT AA.KDDOCTOR FROM S_PENDAFTARAN_H AA WHERE C.KDREG = AA.KDREG), '') "
            SQL &= ",KDDOCTOR_DUA = ISNULL((SELECT AA.KDDOCTOR_KEPADA FROM I_TRACKING_KDDOCTOR AA WHERE C.KDREG = AA.KDREG AND AA.SEQ = 2), '') "
            SQL &= ",KDDOCTOR_TIGA = ISNULL((SELECT AA.KDDOCTOR_KEPADA FROM I_TRACKING_KDDOCTOR AA WHERE C.KDREG = AA.KDREG AND AA.SEQ = 3), '') "
            SQL &= ",KDDOCTOR_EMPAT = ISNULL((SELECT AA.KDDOCTOR_KEPADA FROM I_TRACKING_KDDOCTOR AA WHERE C.KDREG = AA.KDREG AND AA.SEQ = 4), '') "
            SQL &= ",KDDOCTOR_LIMA = ISNULL((SELECT AA.KDDOCTOR_KEPADA FROM I_TRACKING_KDDOCTOR AA WHERE C.KDREG = AA.KDREG AND AA.SEQ = 5), '') "
            SQL &= ",DPJPUtama = ISNULL((SELECT BB.FRONT_TITLE + ' ' + BB.NAME_DISPLAY FROM S_PENDAFTARAN_H AA INNER JOIN M_DOCTOR BB ON AA.KDDOCTOR = BB.KDDOCTOR WHERE C.KDREG = AA.KDREG), '') "
            SQL &= ",DPJPKeDua = ISNULL((SELECT BB.FRONT_TITLE + ' ' + BB.NAME_DISPLAY FROM I_TRACKING_KDDOCTOR AA INNER JOIN M_DOCTOR BB ON AA.KDDOCTOR_KEPADA = BB.KDDOCTOR WHERE C.KDREG = AA.KDREG AND AA.SEQ = 2), '') "
            SQL &= ",DPJPKeTiga = ISNULL((SELECT BB.FRONT_TITLE + ' ' + BB.NAME_DISPLAY FROM I_TRACKING_KDDOCTOR AA INNER JOIN M_DOCTOR BB ON AA.KDDOCTOR_KEPADA = BB.KDDOCTOR WHERE C.KDREG = AA.KDREG AND AA.SEQ = 3), '') "
            SQL &= ",DPJPKeEmpat = ISNULL((SELECT BB.FRONT_TITLE + ' ' + BB.NAME_DISPLAY FROM I_TRACKING_KDDOCTOR AA INNER JOIN M_DOCTOR BB ON AA.KDDOCTOR_KEPADA = BB.KDDOCTOR WHERE C.KDREG = AA.KDREG AND AA.SEQ = 4), '') "
            SQL &= ",DPJPKeLima = ISNULL((SELECT BB.FRONT_TITLE + ' ' + BB.NAME_DISPLAY FROM I_TRACKING_KDDOCTOR AA INNER JOIN M_DOCTOR BB ON AA.KDDOCTOR_KEPADA = BB.KDDOCTOR WHERE C.KDREG = AA.KDREG AND AA.SEQ = 5), '') "
            SQL &= ",Tersedia = C.JENIS_TERSEDIA "
            SQL &= ",Terisi =  C.ISTERISI "
            SQL &= ",Keterangan = C.DESCRIPTION "
            SQL &= ",KDCPPT = ISNULL((SELECT TOP 1 KDCPPT FROM I_TRACKING_CPPT WHERE TANGGAL = '" & Now.ToString("yyyyMMdd") & "' AND C.KDREG = KDREG AND KDDOCTOR = '" & grdDPJPUtama.EditValue & "'), '') "
            SQL &= ",KDCPPT_PERAWAT = ISNULL((SELECT TOP 1 KDCPPT FROM I_TRACKING_CPPT WHERE TANGGAL = '" & Now.ToString("yyyyMMdd") & "' AND C.KDREG = KDREG AND DESCRIPTION = 'PERAWAT'), '') "
            SQL &= ",KDCPPT_DOKTERAKHIR = ISNULL((SELECT TOP 1 KDCPPT FROM I_TRACKING_CPPT WHERE C.KDREG = KDREG AND KDDOCTOR = '" & grdDPJPUtama.EditValue & "' ORDER BY KDCPPT DESC), '') "
            SQL &= ",KDCPPT_PERAWATAKHIR = ISNULL((SELECT TOP 1 KDCPPT FROM I_TRACKING_CPPT WHERE C.KDREG = KDREG AND DESCRIPTION = 'PERAWAT' ORDER BY KDCPPT DESC), '') "
            SQL &= ",KETERANGAN_TINDAKLANJUT = ISNULL((SELECT KETERANGAN_TINDAKLANJUT FROM S_PENDAFTARAN_H WHERE C.KDREG = KDREG), '') "
            SQL &= ",UPDATECPPT = ISNULL((SELECT 'ADA' FROM S_REQ_CPPT_UPDATE WHERE C.KDREG = KDREG AND CONVERT(VARCHAR(8), DATECREATED, 112) = '" & Now.ToString("yyyyMMdd") & "'), '') "
            SQL &= "FROM "
            SQL &= "M_MEDICAL_ROOM A "
            SQL &= "INNER JOIN M_TIPE_KAMAR B "
            SQL &= "ON A.KDKAMAR = B.KDKAMAR "
            SQL &= "INNER JOIN M_MEDICAL_ROOM_DETIL C "
            SQL &= "ON A.KDMROOM = C.KDMROOM "
            SQL &= "WHERE A.ISACTIVE = 1 "

            SQL &= "UNION "

            SQL &= "SELECT  "
            SQL &= "KDMROOM = 0 "
            SQL &= " ,NamaKamar = A.DESCRIPTION  "
            SQL &= " ,NamaKamarBed = A.DESCRIPTION "
            SQL &= " ,NomorBed = 0  "
            SQL &= " ,TanggalMasuk = ISNULL((SELECT FORMAT(DATE, 'dd/MM/yyyy HH:mm:ss') FROM S_PENDAFTARAN_H WHERE C.KDREG = KDREG), '')  "
            SQL &= " ,TANGGALMASUK_ = ISNULL((SELECT DATE FROM S_PENDAFTARAN_H WHERE C.KDREG = KDREG), '')  "
            SQL &= " ,ISPULANG = 1 "
            SQL &= " ,DATEPULANG = GETDATE() "
            SQL &= " ,KelasRawat = '-'  "
            SQL &= " ,NoTransaksi = C.KDREG  "
            SQL &= " ,NoTransaksi_Poli = ISNULL((SELECT KDREGAWAL FROM S_PENDAFTARAN_H WHERE C.KDREG = KDREG), '')  "
            SQL &= " ,NoTransaksi_PoliNama = ISNULL((SELECT BB.NAME_DISPLAY FROM S_PENDAFTARAN_H AA INNER JOIN M_DEPARTMENT BB ON AA.KDDEPARTMENT = BB.KDDEPARTMENT WHERE C.KDREG = AA.KDREG), '')  "
            SQL &= " ,NoRM = ISNULL((SELECT KDCUSTOMER FROM S_PENDAFTARAN_H WHERE C.KDREG = KDREG), '')  "
            SQL &= " ,KARTUBPJS = ISNULL((SELECT KARTUBPJS FROM S_PENDAFTARAN_H WHERE C.KDREG = KDREG), '')  "
            SQL &= " ,NOMORSEP = ISNULL((SELECT NOMORSEP FROM S_PENDAFTARAN_H WHERE C.KDREG = KDREG), '')  "
            SQL &= " ,KDKELASRAWAT = ISNULL((SELECT BB.DESCRIPTION FROM S_PENDAFTARAN_H AA INNER JOIN M_KELASRAWAT BB ON AA.KDKELASRAWAT = BB.KDKELASRAWAT WHERE C.KDREG = AA.KDREG ), '')  "
            SQL &= " ,NamaPasien = ISNULL((SELECT BB.NAME_DISPLAY + ' ' + IIf(BB.FRONT_TITLE IS NULL, '', BB.FRONT_TITLE + ' ') + BB.BACK_TITLE FROM S_PENDAFTARAN_H AA INNER JOIN M_CUSTOMER BB ON AA.KDCUSTOMER = BB.KDCUSTOMER WHERE C.KDREG = AA.KDREG), '')  "
            SQL &= " ,JenisKelamin = ISNULL((SELECT CASE BB.JK WHEN '1' THEN 'Laki-laki' ELSE 'Perempuan' END FROM S_PENDAFTARAN_H AA INNER JOIN M_CUSTOMER BB ON AA.KDCUSTOMER = BB.KDCUSTOMER WHERE C.KDREG = AA.KDREG), '')  "
            SQL &= " ,TanggalLahir = ISNULL((SELECT BB.TANGGALLAHIR FROM S_PENDAFTARAN_H AA INNER JOIN M_CUSTOMER BB ON AA.KDCUSTOMER = BB.KDCUSTOMER WHERE C.KDREG = AA.KDREG), '')  "
            SQL &= " ,Penjamin = ISNULL((SELECT BB.NAME_DISPLAY FROM S_PENDAFTARAN_H AA INNER JOIN M_DEBTOR BB ON AA.KDDEBTOR = BB.KDDEBTOR WHERE C.KDREG = AA.KDREG), '')  "
            SQL &= " ,KDDOCTOR_DPJPUTAMA = ISNULL((SELECT AA.KDDOCTOR FROM S_PENDAFTARAN_H AA WHERE C.KDREG = AA.KDREG), '')  "
            SQL &= " ,KDDOCTOR_DUA = ISNULL((SELECT AA.KDDOCTOR_KEPADA FROM I_TRACKING_KDDOCTOR AA WHERE C.KDREG = AA.KDREG AND AA.SEQ = 2), '') "
            SQL &= " ,KDDOCTOR_TIGA = ISNULL((SELECT AA.KDDOCTOR_KEPADA FROM I_TRACKING_KDDOCTOR AA WHERE C.KDREG = AA.KDREG AND AA.SEQ = 3), '') "
            SQL &= " ,KDDOCTOR_EMPAT = ISNULL((SELECT AA.KDDOCTOR_KEPADA FROM I_TRACKING_KDDOCTOR AA WHERE C.KDREG = AA.KDREG AND AA.SEQ = 4), '') "
            SQL &= " ,KDDOCTOR_LIMA = ISNULL((SELECT AA.KDDOCTOR_KEPADA FROM I_TRACKING_KDDOCTOR AA WHERE C.KDREG = AA.KDREG AND AA.SEQ = 5), '') "
            SQL &= " ,DPJPUtama = ISNULL((SELECT BB.FRONT_TITLE + ' ' + BB.NAME_DISPLAY FROM S_PENDAFTARAN_H AA INNER JOIN M_DOCTOR BB ON AA.KDDOCTOR = BB.KDDOCTOR WHERE C.KDREG = AA.KDREG), '') "
            SQL &= " ,DPJPKeDua = ISNULL((SELECT BB.FRONT_TITLE + ' ' + BB.NAME_DISPLAY FROM I_TRACKING_KDDOCTOR AA INNER JOIN M_DOCTOR BB ON AA.KDDOCTOR_KEPADA = BB.KDDOCTOR WHERE C.KDREG = AA.KDREG AND AA.SEQ = 2), '')  "
            SQL &= " ,DPJPKeTiga = ISNULL((SELECT BB.FRONT_TITLE + ' ' + BB.NAME_DISPLAY FROM I_TRACKING_KDDOCTOR AA INNER JOIN M_DOCTOR BB ON AA.KDDOCTOR_KEPADA = BB.KDDOCTOR WHERE C.KDREG = AA.KDREG AND AA.SEQ = 3), '')  "
            SQL &= " ,DPJPKeEmpat = ISNULL((SELECT BB.FRONT_TITLE + ' ' + BB.NAME_DISPLAY FROM I_TRACKING_KDDOCTOR AA INNER JOIN M_DOCTOR BB ON AA.KDDOCTOR_KEPADA = BB.KDDOCTOR WHERE C.KDREG = AA.KDREG AND AA.SEQ = 4), '')  "
            SQL &= " ,DPJPKeLima = ISNULL((SELECT BB.FRONT_TITLE + ' ' + BB.NAME_DISPLAY FROM I_TRACKING_KDDOCTOR AA INNER JOIN M_DOCTOR BB ON AA.KDDOCTOR_KEPADA = BB.KDDOCTOR WHERE C.KDREG = AA.KDREG AND AA.SEQ = 5), '')  "
            SQL &= " ,Tersedia = 'PRIA WANITA'  "
            SQL &= " ,Terisi =  'TERISI'  "
            SQL &= " ,Keterangan = C.DESCRIPTION  "
            SQL &= ",KDCPPT = ISNULL((SELECT TOP 1 KDCPPT FROM I_TRACKING_CPPT WHERE TANGGAL = '" & Now.ToString("yyyyMMdd") & "' AND C.KDREG = KDREG AND KDDOCTOR = '" & grdDPJPUtama.EditValue & "'), '') "
            SQL &= ",KDCPPT_PERAWAT = ISNULL((SELECT TOP 1 KDCPPT FROM I_TRACKING_CPPT WHERE TANGGAL = '" & Now.ToString("yyyyMMdd") & "' AND C.KDREG = KDREG AND DESCRIPTION = 'PERAWAT'), '') "
            SQL &= ",KDCPPT_DOKTERAKHIR = ISNULL((SELECT TOP 1 KDCPPT FROM I_TRACKING_CPPT WHERE C.KDREG = KDREG AND KDDOCTOR = '" & grdDPJPUtama.EditValue & "' ORDER BY KDCPPT DESC), '') "
            SQL &= ",KDCPPT_PERAWATAKHIR = ISNULL((SELECT TOP 1 KDCPPT FROM I_TRACKING_CPPT WHERE C.KDREG = KDREG AND DESCRIPTION = 'PERAWAT' ORDER BY KDCPPT DESC), '') "
            SQL &= ",KETERANGAN_TINDAKLANJUT = '' "
            SQL &= ",UPDATECPPT = '' "
            SQL &= " FROM S_PENDAFTARAN_NORUANGAN C INNER JOIN M_NONRUANGAN A ON A.KDNONRUANGAN = C.KDNONRUANGAN "
            SQL &= " WHERE C.ISKELUAR = 0 "
            SQL &= " AND A.DESCRIPTION <> '-' "
            SQL &= ") XX "

            If isAll = False Then
                SQL &= "WHERE XX.KDDOCTOR_DPJPUTAMA = " & grdDPJPUtama.EditValue & " "
                SQL &= "OR XX.KDDOCTOR_DUA = " & grdDPJPUtama.EditValue & " "
                SQL &= "OR XX.KDDOCTOR_TIGA = " & grdDPJPUtama.EditValue & " "
                SQL &= "OR XX.KDDOCTOR_EMPAT = " & grdDPJPUtama.EditValue & " "
                SQL &= "OR XX.KDDOCTOR_LIMA = " & grdDPJPUtama.EditValue & " "
            End If

            SQL &= "ORDER BY XX.KDMROOM, XX.NomorBed ASC "


            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)

            da.Fill(ds, "M_MEDICALROOM")

            grd.DataSource = ds.Tables("M_MEDICALROOM")
            grd.ForceInitialize()

            fn_SetFormat()

            If oConn.State = ConnectionState.Open Then
                oConn.Close()
            End If
        Catch ex As Exception
            MsgBox("Preview Data : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadData0Byrm(ByVal KDCUSTOMER As String)
        Try
            If grdDPJPUtama.Text = "" Then Exit Sub

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

            SQL = " SELECT  "
            SQL &= " C.KDMROOM  "
            SQL &= " ,TanggalMasuk = FORMAT(C.DATE, 'dd/MM/yyyy HH:mm:ss')  "
            SQL &= " ,TANGGALMASUK_ = C.DATE  "
            SQL &= " ,KelasRawat = ISNULL((SELECT BB.NAME_DISPLAY FROM M_MEDICAL_ROOM AA INNER JOIN M_TIPE_KAMAR BB ON AA.KDKAMAR = BB.KDKAMAR WHERE C.KDMROOM = AA.KDMROOM), '')  "
            SQL &= " ,NoTransaksi = C.KDREG  "
            SQL &= " ,NoTransaksi_Poli = C.KDREGAWAL  "
            SQL &= " ,NoTransaksi_PoliNama = D.NAME_DISPLAY  "
            SQL &= " ,NoRM = C.KDCUSTOMER "
            SQL &= " ,KARTUBPJS = C.KARTUBPJS "
            SQL &= " ,NOMORSEP = C.NOMORSEP "
            SQL &= " ,KDKELASRAWAT = E.DESCRIPTION  "
            SQL &= " ,NamaPasien = RTRIM(F.NAME_DISPLAY + ' ' + IIf(F.FRONT_TITLE IS NULL, '', F.FRONT_TITLE + ' ') + F.BACK_TITLE)  "
            SQL &= " ,JenisKelamin = CASE F.JK WHEN '1' THEN 'Laki-laki' ELSE 'Perempuan' END  "
            SQL &= " ,TanggalLahir = F.TANGGALLAHIR "
            SQL &= " ,Penjamin = G.NAME_DISPLAY  "
            SQL &= " ,KDDOCTOR_DPJPUTAMA = C.KDDOCTOR  "
            SQL &= " ,DPJPUtama = H.FRONT_TITLE + ' ' + H.NAME_DISPLAY "
            SQL &= " ,DPJPKeDua = ISNULL((SELECT BB.FRONT_TITLE + ' ' + BB.NAME_DISPLAY FROM I_TRACKING_KDDOCTOR AA INNER JOIN M_DOCTOR BB ON AA.KDDOCTOR_KEPADA = BB.KDDOCTOR WHERE C.KDREG = AA.KDREG AND AA.SEQ = 2), '')  "
            SQL &= " ,DPJPKeTiga = ISNULL((SELECT BB.FRONT_TITLE + ' ' + BB.NAME_DISPLAY FROM I_TRACKING_KDDOCTOR AA INNER JOIN M_DOCTOR BB ON AA.KDDOCTOR_KEPADA = BB.KDDOCTOR WHERE C.KDREG = AA.KDREG AND AA.SEQ = 3), '') "
            SQL &= " ,DPJPKeEmpat = ISNULL((SELECT BB.FRONT_TITLE + ' ' + BB.NAME_DISPLAY FROM I_TRACKING_KDDOCTOR AA INNER JOIN M_DOCTOR BB ON AA.KDDOCTOR_KEPADA = BB.KDDOCTOR WHERE C.KDREG = AA.KDREG AND AA.SEQ = 4), '')  "
            SQL &= " ,DPJPKeLima = ISNULL((SELECT BB.FRONT_TITLE + ' ' + BB.NAME_DISPLAY FROM I_TRACKING_KDDOCTOR AA INNER JOIN M_DOCTOR BB ON AA.KDDOCTOR_KEPADA = BB.KDDOCTOR WHERE C.KDREG = AA.KDREG AND AA.SEQ = 5), '')  "
            SQL &= " ,Tersedia =  'X' "
            SQL &= " ,Terisi =  'X' "
            SQL &= " ,Keterangan = 'X' "
            SQL &= " ,KDCPPT = ISNULL((SELECT TOP 1 KDCPPT FROM I_TRACKING_CPPT WHERE TANGGAL = '20240412' AND C.KDREG = KDREG AND KDDOCTOR = '19'), '')  "
            SQL &= " ,KDCPPT_PERAWAT = ISNULL((SELECT TOP 1 KDCPPT FROM I_TRACKING_CPPT WHERE TANGGAL = '20240412' AND C.KDREG = KDREG AND DESCRIPTION = 'PERAWAT'), '')  "
            SQL &= " ,KDCPPT_DOKTERAKHIR = ISNULL((SELECT TOP 1 KDCPPT FROM I_TRACKING_CPPT WHERE C.KDREG = KDREG AND KDDOCTOR = '19' ORDER BY KDCPPT DESC), '')  "
            SQL &= " ,KDCPPT_PERAWATAKHIR = ISNULL((SELECT TOP 1 KDCPPT FROM I_TRACKING_CPPT WHERE C.KDREG = KDREG AND DESCRIPTION = 'PERAWAT' ORDER BY KDCPPT DESC), '')  "
            SQL &= " ,C.KETERANGAN_TINDAKLANJUT "
            SQL &= " FROM  "
            SQL &= " S_PENDAFTARAN_H C  "
            SQL &= " INNER JOIN M_DEPARTMENT D "
            SQL &= " ON C.KDDEPARTMENT = D.KDDEPARTMENT "
            SQL &= " INNER JOIN M_KELASRAWAT E "
            SQL &= " ON C.KDKELASRAWAT = E.KDKELASRAWAT "
            SQL &= " INNER JOIN M_CUSTOMER F "
            SQL &= " ON C.KDCUSTOMER = F.KDCUSTOMER "
            SQL &= " INNER JOIN M_DEBTOR G "
            SQL &= " ON C.KDDEBTOR = G.KDDEBTOR "
            SQL &= " INNER JOIN M_DOCTOR H "
            SQL &= " ON C.KDDOCTOR = H.KDDOCTOR "
            If cboFILTER.SelectedIndex = 0 Then
                SQL &= "WHERE C.KDCUSTOMER = '" & KDCUSTOMER.PadLeft(9, "0") & "' "
            ElseIf cboFILTER.SelectedIndex = 1 Then
                SQL &= "WHERE F.NAME_DISPLAY LIKE '%" & KDCUSTOMER & "%' "
            Else
                SQL &= "WHERE CONVERT(VARCHAR(8), C.DATE, 112) >= '" & deDATEFROM.DateTime.ToString("yyyyMMdd") & "' AND CONVERT(VARCHAR(8), C.DATE, 112) <= '" & deDATETO.DateTime.ToString("yyyyMMdd") & "' "
                If chkAll.Checked = False Then
                    If grdDPJPUtama.Text <> "" Then
                        SQL &= "AND C.KDDOCTOR = " & grdDPJPUtama.EditValue & " "
                    End If
                End If
            End If
            SQL &= " AND C.CATEGORY = '2' "
            SQL &= " ORDER BY C.DATE DESC "

            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)

            da.Fill(ds, "M_MEDICALROOM_X")

            grd.DataSource = ds.Tables("M_MEDICALROOM_X")
            grd.ForceInitialize()

            fn_SetFormat()

            If oConn.State = ConnectionState.Open Then
                oConn.Close()
            End If
        Catch ex As Exception
            MsgBox("Preview Data : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_SetFormat()
        grv.Columns("NamaKamar").Group()
        grv.ExpandAllGroups()

        For iLoop As Integer = 0 To grv.Columns.Count - 1
            If grv.Columns(iLoop).ColumnType.Name = "Decimal" Then
                grv.Columns(iLoop).DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                grv.Columns(iLoop).DisplayFormat.FormatString = "{0:n2}"
                grv.Columns(iLoop).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
                grv.GroupSummary.Add(DevExpress.Data.SummaryItemType.Sum, grv.Columns(iLoop).FieldName, grv.Columns(iLoop),
                                             "{0:n2}")
                grv.Columns(iLoop).SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                grv.Columns(iLoop).SummaryItem.DisplayFormat = "{0:n2}"
            ElseIf grv.Columns(iLoop).ColumnType.Name = "DateTime" Then
                grv.Columns(iLoop).DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
                grv.Columns(iLoop).DisplayFormat.FormatString = "{0:dd/MM/yyyy HH:mm:ss}"
            End If
        Next


        grv.Columns("KDDOCTOR_DUA").VisibleIndex = -1
        grv.Columns("KDDOCTOR_TIGA").VisibleIndex = -1
        grv.Columns("KDDOCTOR_EMPAT").VisibleIndex = -1
        grv.Columns("KDDOCTOR_LIMA").VisibleIndex = -1

        grv.Columns("UPDATECPPT").VisibleIndex = -1
        grv.Columns("NamaKamarBed").VisibleIndex = -1
        grv.Columns("KETERANGAN_TINDAKLANJUT").VisibleIndex = -1
        grv.Columns("KDMROOM").VisibleIndex = -1
        grv.Columns("NoTransaksi").VisibleIndex = -1
        grv.Columns("TANGGALMASUK_").VisibleIndex = -1
        grv.Columns("NoTransaksi_Poli").VisibleIndex = -1
        grv.Columns("Keterangan").VisibleIndex = -1
        grv.Columns("KDDOCTOR_DPJPUTAMA").VisibleIndex = -1
        grv.Columns("DATEPULANG").VisibleIndex = -1
        grv.Columns("ISPULANG").VisibleIndex = -1
        'grv.Columns("KDDOCTOR_DUA").VisibleIndex = -1
        'grv.Columns("KDDOCTOR_TIGA").VisibleIndex = -1
        'grv.Columns("KDDOCTOR_EMPAT").VisibleIndex = -1
        'grv.Columns("KDDOCTOR_LIMA").VisibleIndex = -1
        grv.Columns("KDCPPT").VisibleIndex = -1
        grv.Columns("KDCPPT_PERAWAT").VisibleIndex = -1
        grv.Columns("TanggalLahir").VisibleIndex = -1
        grv.Columns("JenisKelamin").VisibleIndex = -1
        grv.Columns("NoTransaksi_Poli").VisibleIndex = -1
        grv.Columns("NoTransaksi_PoliNama").VisibleIndex = -1
        grv.Columns("KDCPPT_DOKTERAKHIR").VisibleIndex = -1
        grv.Columns("KDCPPT_PERAWATAKHIR").VisibleIndex = -1
        grv.Columns("KARTUBPJS").VisibleIndex = -1
        grv.Columns("NOMORSEP").VisibleIndex = -1
        grv.Columns("KDKELASRAWAT").VisibleIndex = -1

        grv.Columns("Terisi").Caption = "Status"
        grv.Columns("DPJPKeDua").Caption = "DPJP Ke-2"
        grv.Columns("DPJPKeTiga").Caption = "DPJP Ke-3"
        grv.Columns("DPJPKeEmpat").Caption = "DPJP Ke-4"
        grv.Columns("DPJPKeLima").Caption = "DPJP Ke-5"
    End Sub
    Private Sub grv_RowStyle(sender As System.Object, e As DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs) Handles grv.RowStyle
        If grv.IsFilterRow(e.RowHandle) Then Exit Sub

        If grv.GetRowCellValue(e.RowHandle, "UPDATECPPT") <> "" Then
            e.Appearance.BackColor = Color.LightPink
        Else
            If sTYPE = 0 Then
                If sIsDOkter = False Then
                    If grv.GetRowCellValue(e.RowHandle, "KDCPPT_PERAWAT") <> "" Then
                        e.Appearance.BackColor = Color.LightGreen
                    Else
                        If grv.GetRowCellValue(e.RowHandle, "Terisi") = "TERISI" Then
                            e.Appearance.BackColor = Color.LightSeaGreen
                        ElseIf grv.GetRowCellValue(e.RowHandle, "Terisi") = "BOOKING" Then
                            e.Appearance.BackColor = Color.Yellow
                        ElseIf grv.GetRowCellValue(e.RowHandle, "Terisi") = "RENCANA PULANG" Then
                            e.Appearance.BackColor = Color.Yellow
                        End If
                    End If
                Else
                    If grv.GetRowCellValue(e.RowHandle, "KDCPPT") <> "" Then
                        e.Appearance.BackColor = Color.LightGreen
                    Else
                        If grv.GetRowCellValue(e.RowHandle, "Terisi") = "TERISI" Then
                            e.Appearance.BackColor = Color.LightSeaGreen
                        ElseIf grv.GetRowCellValue(e.RowHandle, "Terisi") = "BOOKING" Then
                            e.Appearance.BackColor = Color.Yellow
                        ElseIf grv.GetRowCellValue(e.RowHandle, "Terisi") = "RENCANA PULANG" Then
                            e.Appearance.BackColor = Color.Yellow
                        End If
                    End If
                End If
            Else
                If grv.GetRowCellValue(e.RowHandle, "KETERANGAN_TINDAKLANJUT") = "RESUME" Then
                    e.Appearance.BackColor = Color.Red
                ElseIf grv.GetRowCellValue(e.RowHandle, "KETERANGAN_TINDAKLANJUT") = "FINISH RESUME" Then
                    e.Appearance.BackColor = Color.Aqua
                End If
            End If
        End If
    End Sub
    Private Sub fn_LoadKDUSER()
        Dim oTemplate As New Reference.clsUnit
        Try
            grdUSERHADNOVER.Properties.DataSource = oTemplate.GetData.Where(Function(x) x.ISACTIVE = True).ToList()
            grdUSERHADNOVER.Properties.ValueMember = "KDUNIT"
            grdUSERHADNOVER.Properties.DisplayMember = "MEMO"
        Catch oErr As Exception
            MsgBox("Load User : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadDokter()
        Try
            Dim oDoctor As New Reference.clsDoctor

            'Dim dsDoctor = From x In oDoctor.GetData
            '               Where x.ISACTIVE = True
            '               Select x.KDDOCTOR, x.NAME_DISPLAY

            'grdDPJPUtama.Properties.DataSource = dsDoctor.ToList()
            'grdDPJPUtama.Properties.ValueMember = "KDDOCTOR"
            'grdDPJPUtama.Properties.DisplayMember = "NAME_DISPLAY"

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
            SQL &= "A.KDDOCTOR "
            SQL &= ",NAME_DISPLAY = (SELECT CASE A.FRONT_TITLE WHEN '' THEN '' ELSE A.FRONT_TITLE + ' ' END) + A.NAME_DISPLAY + A.BACK_TITLE "
            SQL &= "FROM  "
            SQL &= "M_DOCTOR A "
            SQL &= "WHERE "
            SQL &= "ISACTIVE = '1' "
            SQL &= "AND CATEGORY = 1 "
            SQL &= "ORDER BY NAME_DISPLAY "

            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "M_DOCTOR")

            grdDPJPUtama.Properties.DataSource = ds.Tables("M_DOCTOR")
            grdDPJPUtama.Properties.ValueMember = "KDDOCTOR"
            grdDPJPUtama.Properties.DisplayMember = "NAME_DISPLAY"

            If oConn.State = ConnectionState.Open Then
                oConn.Close()
            End If

            If sIsDOkter = True Then
                Dim dsDepartment = oDoctor.GetDataByIDUser(sUserID)
                If dsDepartment IsNot Nothing Then
                    grdDPJPUtama.Text = dsDepartment.KDDOCTOR
                End If
            End If
        Catch oErr As Exception
            MsgBox("Load Dokter Data : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
#End Region
#Region "Command Button"
    Private Sub frmMember_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        Select Case e.KeyCode
            Case Keys.Escape
                Me.Close()
            Case Keys.R
                If e.Alt = True And picRefresh.Enabled = True Then
                    picRefresh_Click()
                End If
        End Select
    End Sub
    Private Sub picRefresh_Click() Handles picRefresh.Click
        sTYPE = 0
        fn_LoadSecurity()
    End Sub
    Private Sub UpdateListToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles UpdateListToolStripMenuItem.Click
        If grv.GetFocusedRowCellValue("NoTransaksi") Is Nothing Then
            Exit Sub
        End If
        If grv.GetFocusedRowCellValue("NoRM") = "" Then
            Exit Sub
        End If

        If grdDPJPUtama.Text = "" Then
            MsgBox("Silahkan Pilih dokter yang akan di Input", MsgBoxStyle.Exclamation, Me.Text)
            grdDPJPUtama.ShowPopup()
            Exit Sub
        End If

        If sIsDOkter = False Then
            If grdUSERHADNOVER.Text = "" Then
                MsgBox("User Perawat Kosong", MsgBoxStyle.Exclamation, Me.Text)
                grdUSERHADNOVER.Focus()
                Exit Sub
            End If
        Else
            grdUSERHADNOVER.ResetText()
        End If

        'If sIsDOkter = True Then
        '    Dim cekdokter As Boolean = False

        '    If grv.GetFocusedRowCellValue("DPJPUtama").ToString.Contains(grdDPJPUtama.Text) Then
        '        cekdokter = True
        '    End If
        '    If grv.GetFocusedRowCellValue("DPJPKeDua").ToString.Contains(grdDPJPUtama.Text) Then
        '        cekdokter = True
        '    End If
        '    If grv.GetFocusedRowCellValue("DPJPKeTiga").ToString.Contains(grdDPJPUtama.Text) Then
        '        cekdokter = True
        '    End If
        '    If grv.GetFocusedRowCellValue("DPJPKeEmpat").ToString.Contains(grdDPJPUtama.Text) Then
        '        cekdokter = True
        '    End If
        '    If grv.GetFocusedRowCellValue("DPJPKeLima").ToString.Contains(grdDPJPUtama.Text) Then
        '        cekdokter = True
        '    End If

        '    If cekdokter = False Then
        '        MsgBox("Dokter ", MsgBoxStyle.Exclamation, Me.Text)
        '        grdDPJPUtama.Focus()
        '    End If
        'End If

        Try
            Dim tanggalpulang As DateTime = Now
            If CBool(grv.GetFocusedRowCellValue("ISPULANG")) = True Then
                tanggalpulang = grv.GetFocusedRowCellValue("DATEPULANG")
            End If
            Dim TANGGALMASUK As DateTime = grv.GetFocusedRowCellValue("TANGGALMASUK_")
            Dim frmRawatInapList As New frmRawatInapList
            frmRawatInapList.fn_LoadMe(sCasmix, sIsDOkter, grv.GetFocusedRowCellValue("KDKELASRAWAT"), grv.GetFocusedRowCellValue("KARTUBPJS"), grv.GetFocusedRowCellValue("NOMORSEP"), grv.GetFocusedRowCellValue("NoTransaksi"), grdDPJPUtama.EditValue, grv.GetFocusedRowCellValue("NamaKamarBed"), grv.GetFocusedRowCellValue("Penjamin"), grv.GetFocusedRowCellValue("NoRM"), grv.GetFocusedRowCellValue("NamaPasien"), grv.GetFocusedRowCellValue("JenisKelamin"), grv.GetFocusedRowCellValue("TanggalLahir"), grv.GetFocusedRowCellValue("KDCPPT_PERAWATAKHIR"), TANGGALMASUK, grv.GetFocusedRowCellValue("DPJPUtama"), grv.GetFocusedRowCellValue("DPJPKeDua"), grv.GetFocusedRowCellValue("DPJPKeTiga"), grv.GetFocusedRowCellValue("DPJPKeEmpat"), grv.GetFocusedRowCellValue("DPJPKeLima"), grdDPJPUtama.Text, grv.GetFocusedRowCellValue("NoTransaksi_Poli"), grdUSERHADNOVER.Text, grv.GetFocusedRowCellValue("KDDOCTOR_DPJPUTAMA"), tanggalpulang)
            frmRawatInapList.ShowDialog(Me)

            If sIsDOkter = False Then
                grv.SetFocusedRowCellValue("KDCPPT_PERAWAT", Load_SimpanCPPTSelesai(grv.GetFocusedRowCellValue("NoTransaksi")))
            Else
                grv.SetFocusedRowCellValue("KDCPPT", Load_SimpanCPPTSelesai(grv.GetFocusedRowCellValue("NoTransaksi")))
            End If
        Catch ex As Exception
            MsgBox("Load Form Detail : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
        Finally
            If Not frmEMedrekIDG_03 Is Nothing Then frmRawatInapList.Dispose()
            frmRawatInapList = Nothing
        End Try
    End Sub
    Private Sub grdDPJPUtama_EditValueChanged(sender As Object, e As EventArgs) Handles grdDPJPUtama.EditValueChanged
        fn_LoadSecurity()
    End Sub
    Private Function Load_SimpanCPPTSelesai(ByVal KDREG As String) As String
        Load_SimpanCPPTSelesai = ""

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
            SQL &= "I_TRACKING_CPPT A "
            SQL &= "WHERE "
            SQL &= "A.TANGGAL = '" & Now.ToString("yyyyMMdd") & "' "
            SQL &= "AND A.KDREG = '" & KDREG & "' "

            If sIsDOkter = False Then
                SQL &= "AND A.DESCRIPTION = 'PERAWAT' "
            Else
                SQL &= "AND A.KDDOCTOR = '" & grdDPJPUtama.EditValue & "' "
            End If

            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "I_TRACKING_CPPT")

            'For iLoop As Integer = 0 To ds.Tables("I_TRACKING_CPPT").Rows.Count - 1
            '    With ds.Tables("I_TRACKING_CPPT")
            '        Load_SimpanCPPTSelesai = "Y"
            '    End With
            'Next

            For xloop As Integer = 0 To ds.Tables("I_TRACKING_CPPT").Rows.Count - 1
                Load_SimpanCPPTSelesai = ds.Tables("I_TRACKING_CPPT").Rows(xloop)("KDCPPT")
            Next

            If oConn.State = ConnectionState.Open Then
                oConn.Close()
            End If

        Catch oErr As Exception
            MsgBox("I_TRACKING_CPPT : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Function
    Private Sub btnCari_Click(sender As Object, e As EventArgs) Handles btnCari.Click
        sTYPE = 1
        fn_LoadData0Byrm(txtPARAMETER.Text)
    End Sub
    Private Sub cboFILTER_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboFILTER.SelectedIndexChanged
        If cboFILTER.SelectedIndex = 2 Then
            deDATEFROM.Visible = True
            deDATETO.Visible = True
            txtPARAMETER.Visible = False
            txtPARAMETER.ResetText()
        Else
            deDATEFROM.Visible = False
            deDATETO.Visible = False
            txtPARAMETER.Visible = True
            txtPARAMETER.ResetText()
        End If
    End Sub
    Private Sub AlihDPJPToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AlihDPJPToolStripMenuItem.Click
        If grv.GetFocusedRowCellValue("NoTransaksi") Is Nothing Then
            Exit Sub
        End If
        If grv.GetFocusedRowCellValue("NoRM") = "" Then
            Exit Sub
        End If

        Dim frmAlihDPJP As New frmAlihDPJP
        Try
            frmAlihDPJP.LoadMe(grv.GetFocusedRowCellValue("NoRM"), grv.GetFocusedRowCellValue("NamaPasien"), grv.GetFocusedRowCellValue("NoTransaksi"), grv.GetFocusedRowCellValue("NamaKamar"))
            frmAlihDPJP.ShowDialog(Me)

        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        Finally
            picRefresh_Click()

            If Not frmAlihDPJP Is Nothing Then frmAlihDPJP.Dispose()
            frmAlihDPJP = Nothing
        End Try
    End Sub
#End Region
End Class