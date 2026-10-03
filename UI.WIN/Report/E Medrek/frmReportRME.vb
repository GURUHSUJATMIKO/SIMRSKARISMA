Imports DataAccess
Imports UI.WIN.MAIN.My.Resources
Imports System.Linq
Imports DevExpress.XtraPrinting
Imports System.Data
Imports System.Data.SqlClient
Imports System
Imports System.Text
Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq
Imports System.Globalization
Imports DevExpress.XtraSplashScreen
'Imports System.Threading.Tasks

Public Class frmReportRME
#Region "Function"
    Private oCustomer As New Reference.clsCustomerBPJSRME
    Private oDoctor As New Reference.clsDoctorBPJSRME
    Private oOrganization As New Reference.clsOrganizationBPJSRME
    Private oDepartment As New Reference.clsDepartmentBPJSRME
    Private oPendaftaranBPJS As New Reference.clsPendaftaranBPJSRME
    'Private oGrouper As New Grouper.clsR_Identitas_Grouper_Data
    Private oSet_Antrian As New Pendaftaran2.clsPendaftaranWaktuIGD
    'Private oGrouperDataCppt As New Grouper.clsR_CPPT
    Private oKoneksi As New Brigging.clsSetKoneksi
    Private sRMEBPJS_Url As String = "https://apijkn.bpjs-kesehatan.go.id/medicalrecord/"
    Private sRMEBPJS_ConsId As String = "16694"
    Private sRMEBPJS_SecreatKey As String = sSecreateKeyVclaim
    Private sRMEBPJS_UserKey As String = "db16cba02244a6bcb769ba2f735cf1d8b2cfdb8daab50e1a51775248cf111a11"
    Private sPPKPELAYANAN As String = "0464R005"

    Private Sub Me_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.Text = "Laporan Kirim RME BPJS"

        lTYPE.Text = Report.FILTER_TYPE
        lDATEFROM.Text = Report.FILTER_DATEFROM
        lDATETO.Text = Report.FILTER_DATETO

        cboTYPE.Properties.Items.Clear()
        cboTYPE.Properties.Items.Add("Rekap Rawat Jalan")
        cboTYPE.SelectedIndex = 0

        deDATEFrom.DateTime = Now.AddDays((-Now.Day) + 1)
        deDATETo.DateTime = Now

        txtKodeBPJS.Text = "0464R005"
        txtKodeKemenkes.Text = "3217005"
        txtNamaOrganization.Text = " Rumah Sakit Karisma Cimareme"
    End Sub
    Private Sub fn_LoadSecurity()
        Try
            Dim oOtority As New Setting.clsOtority
            Dim oUser As New Setting.clsUser

            Dim ds = (From x In oOtority.GetDataDetail
                      Join y In oUser.GetData
                      On x.KDOTORITY Equals y.KDOTORITY
                      Where x.MODUL = "RMEBPJSKESEHATAN" _
                      And y.KDUSER = sUserID
                      Select x.ISADD, x.ISDELETE, x.ISUPDATE, x.ISPRINT, x.ISVIEW).FirstOrDefault

            Try
                picPrint.Enabled = ds.ISPRINT
                picRefresh.Enabled = ds.ISVIEW

                If ds.ISVIEW = True Then
                    fn_Preview()
                End If
            Catch oErr As Exception
                MsgBox(Statement.SecurityNotInstalled, MsgBoxStyle.Exclamation, Me.Text)

                picPrint.Enabled = False
                picRefresh.Enabled = False
            End Try
        Catch oErr As Exception
            MsgBox(Statement.SecurityNotInstalled, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_Print()
        Try
            printableComponentLink.Landscape = True
            printableComponentLink.PaperKind = Printing.PaperKind.A4

            Dim phf As PageHeaderFooter =
        TryCast(printableComponentLink.PageHeaderFooter, PageHeaderFooter)
            phf.Header.Content.Clear()
            phf.Header.Font = New Font("Times New Roman", 14, FontStyle.Bold)
            phf.Header.LineAlignment = BrickAlignment.Center
            phf.Footer.Font = New Font("Times New Roman", 9.75)
            phf.Footer.LineAlignment = BrickAlignment.Far
            phf.Footer.Content.AddRange(New String() _
        {sWATERMARK, "", Report.REPORT_PAGE & " : [Page # of Pages #]"})

            phf.Header.Content.AddRange(New String() _
{"", "Laporan Kirim RME BPJS" & vbCrLf & "Periode Tanggal : " & deDATEFrom.DateTime.ToString("dd/MM/yyyy") & " - " & deDATETo.DateTime.ToString("dd/MM/yyyy"), ""})

            printableComponentLink.Component = grd
            printableComponentLink.CreateDocument()
            printableComponentLink.ShowPreviewDialog(Me)
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_Preview()
        Try
            grv.Columns.Clear()
            grd.DataSource = Nothing
            grv.OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways

            Select Case cboTYPE.SelectedIndex
                Case 0
                    fn_LoadDataPatient()
            End Select

        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
#Region "Load Data"
    Private Sub fn_LoadDataPatient()
        Try
            Dim oConn As New SqlConnection
            Dim oComm As New SqlCommand
            Dim da As SqlDataAdapter
            Dim ds As New DataSet
            Dim SQL As String

            Dim sConn As String = sConnOld

            'DUSTIRA
            'Dim sConn As String = Decrypt(My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Software\SIMRS\EMEDREK\", "Database", "").ToString())

            oConn = New SqlConnection(sConn)
            If oConn.State = ConnectionState.Closed Then
                oConn.Open()
            End If

            SQL = "SELECT "
            SQL &= "KDPENDAFTARAN = A.KDREG "
            SQL &= ",A.NOMORSEP "
            SQL &= ",TANGGALDATANG = A.DATE "
            SQL &= ",NORM = A.KDCUSTOMER "
            SQL &= ",NAMAPASIEN = B.NAME_DISPLAY "
            SQL &= ",POLIKLINIK = C.NAME_DISPLAY "
            SQL &= ",DOKTER = D.NAME_DISPLAY "
            SQL &= ",CEKDATAGROUPER = 'Terkirim' "
            SQL &= ",CEKDATA = ISNULL((SELECT RESPON FROM BUDIJAYAGROUP_RSKC..S_PENDAFTARAN_BPJSRME WHERE A.KDREG = KDREG), '') "
            SQL &= "FROM BUDIJAYAGROUP_RSKC..S_PENDAFTARAN_H A "
            SQL &= "INNER JOIN BUDIJAYAGROUP_RSKC..M_CUSTOMER B "
            SQL &= "ON A.KDCUSTOMER = B.KDCUSTOMER "
            SQL &= "INNER JOIN BUDIJAYAGROUP_RSKC..M_DEPARTMENT C "
            SQL &= "ON A.KDDEPARTMENT = C.KDDEPARTMENT "
            SQL &= "INNER JOIN BUDIJAYAGROUP_RSKC..M_DOCTOR D "
            SQL &= "ON A.KDDOCTOR = D.KDDOCTOR "
            SQL &= "WHERE CONVERT(VARCHAR(8), A.DATE, 112) >= '" & deDATEFrom.DateTime.ToString("yyyyMMdd") & "' AND CONVERT(VARCHAR(8), A.DATE, 112) <= '" & deDATETo.DateTime.ToString("yyyyMMdd") & "' "
            SQL &= "AND A.NOMORSEP <> '' "
            SQL &= "AND A.CATEGORY = 1 "

            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "DATAPASIEN")

            grd.DataSource = ds.Tables("DATAPASIEN")
            grd.ForceInitialize()

            If oConn.State = ConnectionState.Open Then
                oConn.Close()
            End If

            fn_LoadFormatData()
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadFormatData()
        For iLoop As Integer = 0 To grv.Columns.Count - 1
            If grv.Columns(iLoop).ColumnType.Name = "Decimal" Then
                grv.Columns(iLoop).DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                grv.Columns(iLoop).DisplayFormat.FormatString = "{0:n2}"
                grv.Columns(iLoop).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
            ElseIf grv.Columns(iLoop).ColumnType.Name = "DateTime" Then
                grv.Columns(iLoop).DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
                grv.Columns(iLoop).DisplayFormat.FormatString = "{0:dd/MM/yyyy}"
            End If
        Next
    End Sub
    Private Function fn_LoadWaktuServer() As DateTime
        Try
            fn_LoadWaktuServer = Now

            Dim oConn As New SqlConnection
            Dim oComm As New SqlCommand
            Dim da As SqlDataAdapter
            Dim ds As New DataSet
            Dim SQL As String

            Dim sConn As String = sConnOld

            'RSDUSTIRA
            'Dim sConn As String = Decrypt(My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Software\SIMRS\EMEDREK\", "Database", "").ToString())

            oConn = New SqlConnection(sConn)
            If oConn.State = ConnectionState.Closed Then
                oConn.Open()
            End If

            SQL = "SELECT GETDATE() AS CurrentDateTime "

            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "WAKTUSERVER")

            If oConn.State = ConnectionState.Open Then
                oConn.Close()
            End If

            For iLoop As Integer = 0 To ds.Tables("WAKTUSERVER").Rows.Count - 1
                With ds.Tables("WAKTUSERVER")
                    fn_LoadWaktuServer = .Rows(iLoop)("CurrentDateTime")
                End With
            Next
        Catch oErr As Exception
            fn_LoadWaktuServer = Now
            Throw oErr
        End Try
    End Function
#End Region
#Region "Simpan Data"
    Private Function fn_SaveBPJSRME_Customer(ByVal KDCUSTOMER As String, ByVal ID As String, ByVal Pesan As Boolean) As Boolean
        Try
            ' ***** HEADER *****
            Dim isadd As Boolean = False

            Dim ds = oCustomer.GetStructureHeader
            With ds
                Dim WaktuServer As DateTime = fn_LoadWaktuServer()

                Dim dsCek = oCustomer.GetData(KDCUSTOMER)
                If dsCek Is Nothing Then
                    isadd = True
                    .DATECREATED = WaktuServer
                Else
                    isadd = False
                    .DATECREATED = dsCek.DATECREATED
                End If

                .DATEUPDATED = WaktuServer
                .KDCUSTOMER = KDCUSTOMER
                .ID = ID
                .REQUEST = ""
                .RESPON = ""
                .ISDEFAULT = False
                .ISACTIVE = True
            End With

            If isadd = True Then
                fn_SaveBPJSRME_Customer = oCustomer.InsertData(ds)
            Else
                fn_SaveBPJSRME_Customer = oCustomer.UpdateData(ds)
            End If
        Catch oErr As Exception
            fn_SaveBPJSRME_Customer = False
            If Pesan = True Then
                MsgBox("Gagal Simpan id Pasien" & vbCritical & oErr.Message, MsgBoxStyle.Information, Me.Text)
            End If
        End Try
    End Function
    Private Function fn_SaveBPJSRME_Doctor(ByVal KDDOCTOR As String, ByVal ID As String, ByVal Pesan As Boolean) As Boolean
        Try
            ' ***** HEADER *****
            Dim isadd As Boolean = False

            Dim ds = oDoctor.GetStructureHeader
            With ds
                Dim WaktuServer As DateTime = fn_LoadWaktuServer()

                .DATECREATED = WaktuServer
                .DATEUPDATED = WaktuServer
                .KDDOCTOR = KDDOCTOR
                .ID = ID
                .REQUEST = ""
                .RESPON = ""
                .ISDEFAULT = False
                .ISACTIVE = True
            End With

            fn_SaveBPJSRME_Doctor = oDoctor.InsertData(ds)
        Catch oErr As Exception
            fn_SaveBPJSRME_Doctor = False
            If Pesan = True Then
                MsgBox("Gagal Simpan id Dokter" & vbCritical & oErr.Message, MsgBoxStyle.Information, Me.Text)
            End If
        End Try
    End Function
    Private Function fn_SaveBPJSRME_Department(ByVal KDDEPARTMENT As String, ByVal ID As String, ByVal Pesan As Boolean) As Boolean
        Try
            ' ***** HEADER *****
            Dim isadd As Boolean = False

            Dim ds = oDepartment.GetStructureHeader
            With ds
                Dim WaktuServer As DateTime = fn_LoadWaktuServer()

                .DATECREATED = WaktuServer

                .DATEUPDATED = WaktuServer
                .KDDEPARTMENT = KDDEPARTMENT
                .ID = ID
                .REQUEST = ""
                .RESPON = ""
                .ISDEFAULT = False
                .ISACTIVE = True
            End With

            fn_SaveBPJSRME_Department = oDepartment.InsertData(ds)
        Catch oErr As Exception
            fn_SaveBPJSRME_Department = False
            If Pesan = True Then
                MsgBox("Gagal Simpan id Department" & vbCritical & oErr.Message, MsgBoxStyle.Information, Me.Text)
            End If
        End Try
    End Function
    Private Function fn_SaveBPJSRME_Organization(ByVal ID As String, ByVal Pesan As Boolean) As Boolean
        Try
            ' ***** HEADER *****
            Dim isadd As Boolean = False

            Dim ds = oOrganization.GetStructureHeader
            With ds
                Dim WaktuServer As DateTime = fn_LoadWaktuServer()

                Dim dsCek = oOrganization.GetData()
                If dsCek Is Nothing Then
                    isadd = True
                    .DATECREATED = WaktuServer
                Else
                    isadd = False
                    .DATECREATED = dsCek.DATECREATED
                End If

                .DATEUPDATED = WaktuServer
                .KDORGANIZATION = "ORGANIZATION"
                .ID = ID
                .REQUEST = ""
                .RESPON = ""
                .ISDEFAULT = False
                .ISACTIVE = True
            End With

            If isadd = True Then
                fn_SaveBPJSRME_Organization = oOrganization.InsertData(ds)
            Else
                fn_SaveBPJSRME_Organization = oOrganization.UpdateData(ds)
            End If
        Catch oErr As Exception
            fn_SaveBPJSRME_Organization = False
            If Pesan = True Then
                MsgBox("Gagal Simpan id Organization" & vbCritical & oErr.Message, MsgBoxStyle.Information, Me.Text)
            End If
        End Try
    End Function
    Private Function fn_SaveBPJSRME_Pendaftaran(ByVal KDPENDAFTARAN As String, ByVal ID1 As String, ByVal ID2 As String, ByVal ID3 As String, ByVal ID4 As String, ByVal ID5 As String, ByVal ID6 As String, ByVal ID7 As String, ByVal Request As String, ByVal Respons As String, ByVal Pesan As Boolean) As Boolean
        Try
            ' ***** HEADER *****
            Dim isadd As Boolean = False

            Dim ds = oPendaftaranBPJS.GetStructureHeader
            With ds
                Dim WaktuServer As DateTime = fn_LoadWaktuServer()

                Dim dsCek = oPendaftaranBPJS.GetData(KDPENDAFTARAN)
                If dsCek Is Nothing Then
                    isadd = True
                    .DATECREATED = WaktuServer
                Else
                    isadd = False
                    .DATECREATED = dsCek.DATECREATED
                End If

                .DATEUPDATED = WaktuServer
                .KDREG = KDPENDAFTARAN
                .ID1 = ID1
                .ID2 = ID2
                .ID3 = ID3
                .ID4 = ID4
                .ID5 = ID5
                .ID6 = ID6
                .ID7 = ID7
                .ID8 = ""
                .ID9 = ""
                .ID10 = ""
                .REQUEST = Request
                .RESPON = Respons
                .CATEGORY = IIf(KDPENDAFTARAN.Contains("RJ"), 0, 1)
                .ISDEFAULT = False
                .ISACTIVE = True
            End With

            If isadd = True Then
                fn_SaveBPJSRME_Pendaftaran = oPendaftaranBPJS.InsertData(ds)
            Else
                fn_SaveBPJSRME_Pendaftaran = oPendaftaranBPJS.UpdateData(ds)
            End If
        Catch oErr As Exception
            fn_SaveBPJSRME_Pendaftaran = False
            If Pesan = True Then
                MsgBox("Gagal Simpan id Pendaftaran" & vbCritical & oErr.Message, MsgBoxStyle.Information, Me.Text)
            End If
        End Try
    End Function
#End Region
#Region "Kirim RME BPJS Kesehatan"
    Private Function fn_UDD(ByVal Parameter1 As String, ByVal Parameter2 As String, ByVal Parameter3 As String) As String
        fn_UDD = Parameter1 & "-" & Parameter2 & "-" & Parameter3 & "-" & Guid.NewGuid().ToString()
    End Function
    Private Function BuildBundleWith6Resources(ByVal Register As String, ByVal Pesan As Boolean) As Boolean
        BuildBundleWith6Resources = False

        Dim dsPendaftaran = oPendaftaranBPJS.GetDataPendaftaranByRegister(Register)
        If dsPendaftaran IsNot Nothing Then
            Dim idCustomer As String = String.Empty
            Dim idDoctor As String = String.Empty
            Dim idDepartment As String = String.Empty
            Dim idOrganization As String = String.Empty
            Dim diagnosaUtamaKode As String = ""
            Dim diagnosaUtamaName As String = ""
            Dim diagnosaDisplay As New List(Of String)

            Threading.Thread.Sleep(1000)

            Dim dsCustomerBPJSRME = oCustomer.GetData(dsPendaftaran.KDCUSTOMER)
            If dsCustomerBPJSRME IsNot Nothing Then
                idCustomer = dsCustomerBPJSRME.ID
            Else
                idCustomer = fn_UDD(txtKodeBPJS.Text, txtKodeKemenkes.Text, IIf(dsPendaftaran.KDREG.Contains("RJ"), "2", "1"))
                If fn_SaveBPJSRME_Customer(dsPendaftaran.KDCUSTOMER, idCustomer, Pesan) = False Then
                    idCustomer = ""
                End If
            End If

            Threading.Thread.Sleep(1000)

            Dim dsDoctorBPJSRME As String = oDoctor.GetData(dsPendaftaran.KDDOCTOR, sConnOld)
            If dsDoctorBPJSRME <> "" Then
                idDoctor = dsDoctorBPJSRME
            Else
                idDoctor = fn_UDD(txtKodeBPJS.Text, txtKodeKemenkes.Text, IIf(dsPendaftaran.KDREG.Contains("RJ"), "2", "1"))
                If fn_SaveBPJSRME_Doctor(dsPendaftaran.KDDOCTOR, idDoctor, Pesan) = False Then
                    idDoctor = ""
                End If
            End If

            Threading.Thread.Sleep(1000)

            Dim dsDepartmentBPJSRME As String = oDepartment.GetData(dsPendaftaran.KDDEPARTMENT, sConnOld)
            If dsDepartmentBPJSRME <> "" Then
                idDepartment = dsDepartmentBPJSRME
            Else
                idDepartment = fn_UDD(txtKodeBPJS.Text, txtKodeKemenkes.Text, IIf(dsPendaftaran.KDREG.Contains("RJ"), "2", "1"))
                If fn_SaveBPJSRME_Department(dsPendaftaran.KDDEPARTMENT, idDepartment, Pesan) = False Then
                    idDepartment = ""
                End If
            End If

            Threading.Thread.Sleep(1000)

            Dim dsOrganizationBPJSRME = oOrganization.GetData()
            If dsOrganizationBPJSRME IsNot Nothing Then
                idOrganization = dsOrganizationBPJSRME.ID
            Else
                idOrganization = fn_UDD(txtKodeBPJS.Text, txtKodeKemenkes.Text, IIf(dsPendaftaran.KDREG.Contains("RJ"), "2", "1"))
                If fn_SaveBPJSRME_Organization(idOrganization, Pesan) = False Then
                    idOrganization = ""
                End If
            End If

            'If dsPendaftaran.M_DOCTOR.JENISKELAMIN = "" Then
            '    If Pesan = True Then
            '        MsgBox("Silahkan perbaiki jenis kelamin dan tanggal lahir dokter", MsgBoxStyle.Information, Me.Text)
            '    End If
            '    idDoctor = ""
            'End If

            'Dim kodegrouper As Integer = 0

            'Dim dsGrouper = oGrouper.GetDataByRegister(dsPendaftaran.KDREG)
            'If dsGrouper Is Nothing Then
            '    If Pesan = True Then
            '        MsgBox("Grouper Tidak di temukan dengan nomor Register", MsgBoxStyle.Information, Me.Text)
            '    End If
            '    idCustomer = ""
            'Else
            '    kodegrouper = dsGrouper.kodegrouper
            'End If

            'For Each xloop In oGrouper.GetDataDetailDiagnosa(kodegrouper)
            '    If xloop.seq = 0 Then
            '        diagnosaUtamaKode = xloop.kddiagnosa
            '        diagnosaUtamaName = xloop.memo
            '    End If

            '    diagnosaDisplay.Add(xloop.memo)
            'Next

            'If diagnosaUtamaKode = "" Then
            '    If Pesan = True Then
            '        MsgBox("Diagnosa Utama Tidak ditemukan (Seq 0)", MsgBoxStyle.Information, Me.Text)
            '    End If
            '    idCustomer = ""
            'End If

            'Dim dsCPPT = oGrouperDataCppt.GetDataByKodePendaftaranRawatJalanDokter(dsPendaftaran.KDREG)

            'If dsCPPT Is Nothing Then
            '    If Pesan = True Then
            '        MsgBox("CPPT Tidak di temukan", MsgBoxStyle.Information, Me.Text)
            '    End If
            '    idCustomer = ""
            'End If

            'If idCustomer <> "" And idDoctor <> "" And idDepartment <> "" And idOrganization <> "" Then
            '    Dim ID1_Bundle As String = ""
            '    Dim ID2_encounter As String = ""
            '    Dim ID3_Condition As String = ""
            '    Dim ID4_Medication As String = ""
            '    Dim ID5_Condition As String = ""
            '    Dim ID6_Composition As String = ""
            '    Dim ID7_Prosedur As String = ""

            '    Dim dsPendaftaranBPJSRME = oPendaftaranBPJS.GetData(dsPendaftaran.KDREG)
            '    If dsPendaftaranBPJSRME IsNot Nothing Then
            '        ID1_Bundle = dsPendaftaranBPJSRME.ID1
            '        ID2_encounter = dsPendaftaranBPJSRME.ID2
            '        ID3_Condition = dsPendaftaranBPJSRME.ID3
            '        ID4_Medication = dsPendaftaranBPJSRME.ID4
            '        ID5_Condition = dsPendaftaranBPJSRME.ID5
            '        ID6_Composition = dsPendaftaranBPJSRME.ID6
            '        ID7_Prosedur = dsPendaftaranBPJSRME.ID7
            '    Else
            '        ID1_Bundle = fn_UDD(txtKodeBPJS.Text, txtKodeKemenkes.Text, IIf(dsPendaftaran.KDREG.Contains("RJ"), "2", "1"))
            '        ID2_encounter = fn_UDD(txtKodeBPJS.Text, txtKodeKemenkes.Text, IIf(dsPendaftaran.KDREG.Contains("RJ"), "2", "1"))
            '        ID3_Condition = fn_UDD(txtKodeBPJS.Text, txtKodeKemenkes.Text, IIf(dsPendaftaran.KDREG.Contains("RJ"), "2", "1"))
            '        ID4_Medication = fn_UDD(txtKodeBPJS.Text, txtKodeKemenkes.Text, IIf(dsPendaftaran.KDREG.Contains("RJ"), "2", "1"))
            '        ID5_Condition = fn_UDD(txtKodeBPJS.Text, txtKodeKemenkes.Text, IIf(dsPendaftaran.KDREG.Contains("RJ"), "2", "1"))
            '        ID6_Composition = fn_UDD(txtKodeBPJS.Text, txtKodeKemenkes.Text, IIf(dsPendaftaran.KDREG.Contains("RJ"), "2", "1"))
            '        ID7_Prosedur = fn_UDD(txtKodeBPJS.Text, txtKodeKemenkes.Text, IIf(dsPendaftaran.KDREG.Contains("RJ"), "2", "1"))
            '    End If

            '    Dim sb As New StringBuilder()

            '    ' =====================================================
            '    ' BUNDLE AWAL
            '    ' =====================================================
            '    sb.AppendLine("{")
            '    sb.AppendLine("  ""resourceType"": ""Bundle"",")
            '    sb.AppendLine($"  ""id"": ""{ID1_Bundle}"",")
            '    sb.AppendLine("  ""meta"": {")
            '    sb.AppendLine($"    ""lastUpdated"": ""{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture)}""")
            '    sb.AppendLine("  },")
            '    sb.AppendLine("  ""identifier"": {")
            '    sb.AppendLine("    ""system"": ""sep"",")
            '    sb.AppendLine($"    ""value"": ""{dsPendaftaran.NOMORSEP}""")
            '    sb.AppendLine("  },")
            '    sb.AppendLine("  ""type"": ""document"",")
            '    sb.AppendLine("  ""entry"": [")


            '    ' =====================================================
            '    ' 1. ORGANIZATION RESOURCE
            '    ' =====================================================
            '    sb.AppendLine("    {")
            '    sb.AppendLine("      ""resource"": {")
            '    sb.AppendLine("        ""resourceType"": ""Organization"",")
            '    sb.AppendLine($"        ""id"": ""{idDepartment}"",")
            '    sb.AppendLine("        ""identifier"": [")
            '    sb.AppendLine("          {")
            '    sb.AppendLine("            ""use"": ""official"",")
            '    sb.AppendLine("            ""system"": ""urn:oid:bpjs"",")
            '    sb.AppendLine($"            ""value"": ""{EscapeJson(txtKodeBPJS.Text)}""")
            '    sb.AppendLine("          },")
            '    sb.AppendLine("          {")
            '    sb.AppendLine("            ""use"": ""official"",")
            '    sb.AppendLine("            ""system"": ""urn:oid:kemkes"",")
            '    sb.AppendLine($"            ""value"": ""{EscapeJson(txtKodeKemenkes.Text)}""")
            '    sb.AppendLine("          }")
            '    sb.AppendLine("        ],")
            '    sb.AppendLine("        ""type"": [")
            '    sb.AppendLine("          {")
            '    sb.AppendLine("            ""coding"": [")
            '    sb.AppendLine("              {")
            '    sb.AppendLine("                ""system"": ""http://hl7.org/fhir/organization-type"",")
            '    sb.AppendLine("                ""code"": ""prov"",")
            '    sb.AppendLine("                ""display"": ""Healthcare Provider""")
            '    sb.AppendLine("              }")
            '    sb.AppendLine("            ]")
            '    sb.AppendLine("          }")
            '    sb.AppendLine("        ],")
            '    sb.AppendLine($"        ""name"": ""{EscapeJson(dsPendaftaran.M_DEPARTMENT.NAME_DISPLAY)}"",")
            '    sb.AppendLine("        ""alias"": [")
            '    sb.AppendLine($"          ""{EscapeJson(txtNamaOrganization.Text)}""")
            '    sb.AppendLine("        ],")
            '    sb.AppendLine("        ""telecom"": [")
            '    sb.AppendLine("          {")
            '    sb.AppendLine("            ""system"": ""phone"",")
            '    sb.AppendLine($"            ""value"": ""{EscapeJson(dsPendaftaran.M_DEPARTMENT.PHONE)}"",")
            '    sb.AppendLine("            ""use"": ""work""")
            '    sb.AppendLine("          }")
            '    sb.AppendLine("        ],")
            '    sb.AppendLine("        ""address"": [")
            '    sb.AppendLine("          {")
            '    sb.AppendLine("            ""use"": ""work"",")
            '    sb.AppendLine($"            ""text"": ""{EscapeJson(dsPendaftaran.M_DEPARTMENT.BILL_STREET)}"",")
            '    sb.AppendLine("            ""line"": [")
            '    sb.AppendLine($"              ""{EscapeJson(dsPendaftaran.M_DEPARTMENT.BILL_STREET)}""")
            '    sb.AppendLine("            ],")
            '    sb.AppendLine($"            ""city"": ""{EscapeJson(dsPendaftaran.M_DEPARTMENT.BILL_CITY)}"",")
            '    sb.AppendLine($"            ""state"": ""{EscapeJson(dsPendaftaran.M_DEPARTMENT.BILL_STATE)}"",")
            '    sb.AppendLine($"            ""postalCode"": ""{EscapeJson(dsPendaftaran.M_DEPARTMENT.BILL_ZIP)}"",")
            '    sb.AppendLine($"            ""country"": ""{EscapeJson(dsPendaftaran.M_DEPARTMENT.BILL_COUNTRY)}""")
            '    sb.AppendLine("          }")
            '    sb.AppendLine("        ],")
            '    sb.AppendLine("        ""contact"": [")
            '    sb.AppendLine("          {")
            '    sb.AppendLine("            ""purpose"": {")
            '    sb.AppendLine("              ""coding"": [")
            '    sb.AppendLine("                {")
            '    sb.AppendLine("                  ""system"": ""http://hl7.org/fhir/contactentity-type"",")
            '    sb.AppendLine("                  ""code"": ""PATINF""")
            '    sb.AppendLine("                }")
            '    sb.AppendLine("              ]")
            '    sb.AppendLine("            },")
            '    sb.AppendLine("            ""telecom"": [")
            '    sb.AppendLine("              {")
            '    sb.AppendLine("                ""system"": ""phone"",")
            '    sb.AppendLine($"                ""value"": ""{EscapeJson(dsPendaftaran.M_DEPARTMENT.MOBILE)}""")
            '    sb.AppendLine("              }")
            '    sb.AppendLine("            ]")
            '    sb.AppendLine("          }")
            '    sb.AppendLine("        ]")
            '    sb.AppendLine("      }")
            '    sb.AppendLine("    },")

            '    ' =====================================================
            '    ' 2. PRACTITIONER RESOURCE - VERSI SEDERHANA
            '    ' =====================================================
            '    sb.AppendLine("    {")
            '    sb.AppendLine("      ""resource"": {")
            '    sb.AppendLine("        ""resourceType"": ""Practitioner"",")
            '    sb.AppendLine($"        ""id"": ""{idDoctor}"",")
            '    sb.AppendLine("        ""identifier"": [")
            '    sb.AppendLine("          {")
            '    sb.AppendLine("            ""use"": ""official"",")
            '    sb.AppendLine("            ""system"": ""urn:oid:nomor_sip"",")
            '    sb.AppendLine($"            ""value"": ""{EscapeJson(dsPendaftaran.M_DOCTOR.SIP)}""")
            '    sb.AppendLine("          },")
            '    sb.AppendLine("          {")
            '    sb.AppendLine("            ""use"": ""official"",")
            '    sb.AppendLine("            ""type"": {")
            '    sb.AppendLine("              ""coding"": [{")
            '    sb.AppendLine("                ""system"": ""http://hl7.org/fhir/v2/0203"",")
            '    sb.AppendLine("                ""code"": ""NNIDN""")
            '    sb.AppendLine("              }]")
            '    sb.AppendLine("            },")
            '    sb.AppendLine($"            ""value"": ""{EscapeJson(dsPendaftaran.M_DOCTOR.OTHER)}"",")
            '    sb.AppendLine("            ""assigner"": {")
            '    sb.AppendLine("              ""display"": ""KEMDAGRI""")
            '    sb.AppendLine("            }")
            '    sb.AppendLine("          }")
            '    sb.AppendLine("        ],")
            '    sb.AppendLine("        ""name"": [")
            '    sb.AppendLine("          {")
            '    sb.AppendLine("            ""use"": ""official"",")
            '    sb.AppendLine($"            ""text"": ""{EscapeJson(dsPendaftaran.M_DOCTOR.NAME_DISPLAY)}""")
            '    sb.AppendLine("          }")
            '    sb.AppendLine("        ],")
            '    sb.AppendLine("        ""telecom"": [")
            '    sb.AppendLine("          {")
            '    sb.AppendLine("            ""system"": ""phone"",")
            '    sb.AppendLine($"            ""value"": ""{EscapeJson(dsPendaftaran.M_DOCTOR.MOBILE)}"",")
            '    sb.AppendLine("            ""use"": ""work""")
            '    sb.AppendLine("          },")
            '    sb.AppendLine("          {")
            '    sb.AppendLine("            ""system"": ""email"",")
            '    sb.AppendLine($"            ""value"": ""{EscapeJson(dsPendaftaran.M_DOCTOR.EMAIL)}"",")
            '    sb.AppendLine("            ""use"": ""work""")
            '    sb.AppendLine("          },")
            '    sb.AppendLine("          {")
            '    sb.AppendLine("            ""system"": ""fax"",")
            '    sb.AppendLine("            ""value"": """",")
            '    sb.AppendLine("            ""use"": ""work""")
            '    sb.AppendLine("          }")
            '    sb.AppendLine("        ],")
            '    sb.AppendLine($"        ""gender"": ""{dsPendaftaran.M_DOCTOR.JENISKELAMIN}"",")
            '    sb.AppendLine($"        ""birthDate"": ""{dsPendaftaran.M_DOCTOR.TANGGALLAHIR.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture)}"",")
            '    sb.AppendLine("        ""address"": [")
            '    sb.AppendLine("          {")
            '    sb.AppendLine("            ""use"": ""home"",")
            '    sb.AppendLine($"            ""text"": ""{EscapeJson(dsPendaftaran.M_DOCTOR.BILL_STREET)}"",")
            '    sb.AppendLine("            ""line"": [")
            '    sb.AppendLine($"              ""{EscapeJson(dsPendaftaran.M_DOCTOR.BILL_STREET)}""")
            '    sb.AppendLine("            ],")
            '    sb.AppendLine($"            ""city"": ""{EscapeJson(dsPendaftaran.M_DOCTOR.BILL_CITY)}"",")
            '    sb.AppendLine($"            ""state"": ""{EscapeJson(dsPendaftaran.M_DOCTOR.BILL_STATE)}"",")
            '    sb.AppendLine($"            ""postalCode"": ""{EscapeJson(dsPendaftaran.M_DOCTOR.BILL_ZIP)}"",")
            '    sb.AppendLine($"            ""country"": ""{EscapeJson(dsPendaftaran.M_DOCTOR.BILL_COUNTRY)}""")
            '    sb.AppendLine("          }")
            '    sb.AppendLine("        ]")
            '    sb.AppendLine("      }")
            '    sb.AppendLine("    },")

            '    ' =====================================================
            '    ' 3. PATIENT RESOURCE
            '    ' =====================================================
            '    sb.AppendLine("    {")
            '    sb.AppendLine("      ""resource"": {")
            '    sb.AppendLine("        ""resourceType"": ""Patient"",")
            '    sb.AppendLine($"        ""id"": ""{idCustomer}"",")
            '    sb.AppendLine("        ""identifier"": [")
            '    sb.AppendLine("          {")
            '    sb.AppendLine("            ""use"": ""usual"",")
            '    sb.AppendLine("            ""type"": {")
            '    sb.AppendLine("              ""coding"": [{")
            '    sb.AppendLine("                ""system"": ""http://hl7.org/fhir/v2/0203"",")
            '    sb.AppendLine("                ""code"": ""MR""")
            '    sb.AppendLine("              }]")
            '    sb.AppendLine("            },")
            '    sb.AppendLine($"            ""value"": ""{EscapeJson(dsPendaftaran.KDCUSTOMER)}"",")
            '    sb.AppendLine("            ""assigner"": {")
            '    sb.AppendLine($"              ""display"": ""{EscapeJson(sUserID)}""")
            '    sb.AppendLine("            }")
            '    sb.AppendLine("          },")
            '    sb.AppendLine("          {")
            '    sb.AppendLine("            ""use"": ""official"",")
            '    sb.AppendLine("            ""type"": {")
            '    sb.AppendLine("              ""coding"": [{")
            '    sb.AppendLine("                ""system"": ""http://hl7.org/fhir/v2/0203"",")
            '    sb.AppendLine("                ""code"": ""MB""")
            '    sb.AppendLine("              }]")
            '    sb.AppendLine("            },")
            '    sb.AppendLine($"            ""value"": ""{EscapeJson(dsPendaftaran.KARTUBPJS)}"",")
            '    sb.AppendLine("            ""assigner"": {")
            '    sb.AppendLine("              ""display"": ""BPJS KESEHATAN""")
            '    sb.AppendLine("            }")
            '    sb.AppendLine("          },")
            '    sb.AppendLine("          {")
            '    sb.AppendLine("            ""use"": ""official"",")
            '    sb.AppendLine("            ""type"": {")
            '    sb.AppendLine("              ""coding"": [{")
            '    sb.AppendLine("                ""system"": ""http://hl7.org/fhir/v2/0203"",")
            '    sb.AppendLine("                ""code"": ""NNIDN""")
            '    sb.AppendLine("              }]")
            '    sb.AppendLine("            },")
            '    sb.AppendLine($"            ""value"": ""{EscapeJson(dsPendaftaran.M_CUSTOMER.KTP)}"",")
            '    sb.AppendLine("            ""assigner"": {")
            '    sb.AppendLine("              ""display"": ""KEMENDAGRI""")
            '    sb.AppendLine("            }")
            '    sb.AppendLine("          }")
            '    sb.AppendLine("        ],")
            '    sb.AppendLine("        ""active"": true,")
            '    sb.AppendLine("        ""name"": [{")
            '    sb.AppendLine("          ""use"": ""official"",")
            '    sb.AppendLine($"          ""text"": ""{EscapeJson(dsPendaftaran.M_CUSTOMER.NAME_DISPLAY)}""")
            '    sb.AppendLine("        }],")
            '    sb.AppendLine("        ""maritalStatus"": {")
            '    sb.AppendLine("          ""coding"": [{")
            '    sb.AppendLine("            ""system"": ""http://hl7.org/fhir/v3/MaritalStatus"",")
            '    sb.AppendLine("            ""code"": ""U""")
            '    sb.AppendLine("          }]")
            '    sb.AppendLine("        },")
            '    sb.AppendLine("        ""telecom"": [")
            '    sb.AppendLine("          {""system"": ""phone"", ""value"": """", ""use"": ""work""},")
            '    sb.AppendLine($"          {{""system"": ""phone"", ""value"": ""{EscapeJson(dsPendaftaran.NOMORTELEPON)}"", ""use"": ""mobile""}},")
            '    sb.AppendLine("          {""system"": ""phone"", ""value"": ""TDK ADA"", ""use"": ""home""}")
            '    sb.AppendLine("        ],")
            '    sb.AppendLine($"        ""gender"": ""{IIf(dsPendaftaran.M_CUSTOMER.KDJENISKELAMIN = "1", "male", "female")}"",")
            '    'DUSTIRA
            '    'sb.AppendLine($"        ""gender"": ""{IIf(dsPendaftaran.M_CUSTOMER.KDJENISKELAMIN = "0", "male", "female")}"",")
            '    sb.AppendLine($"        ""birthDate"": ""{dsPendaftaran.M_CUSTOMER.TANGGALLAHIR.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture)}"",")
            '    sb.AppendLine("        ""deceasedBoolean"": false,")
            '    sb.AppendLine("        ""address"": [{")
            '    sb.AppendLine("          ""line"": [")
            '    sb.AppendLine($"            ""{EscapeJson(dsPendaftaran.M_CUSTOMER.ALAMAT)}""")
            '    sb.AppendLine("          ],")
            '    sb.AppendLine($"          ""city"": ""{EscapeJson(dsPendaftaran.M_CUSTOMER.M_KELURAHAN.M_KECAMATAN.M_KABUPATEN.MEMO)}"",")
            '    sb.AppendLine($"          ""district"": ""{EscapeJson(dsPendaftaran.M_CUSTOMER.M_KELURAHAN.M_KECAMATAN.MEMO)}"",")
            '    sb.AppendLine("          ""state"": """ & dsPendaftaran.M_CUSTOMER.NEGARA & """,")
            '    sb.AppendLine("          ""postalCode"": """ & dsPendaftaran.M_CUSTOMER.KODEPOS & """,")
            '    sb.AppendLine($"          ""text"": ""{EscapeJson(dsPendaftaran.M_CUSTOMER.ALAMAT)}"",")
            '    sb.AppendLine("          ""use"": ""home"",")
            '    sb.AppendLine("          ""type"": ""both""")
            '    sb.AppendLine("        }],")
            '    sb.AppendLine("        ""managingOrganization"": {")
            '    sb.AppendLine($"          ""reference"": ""Organization/{idOrganization}"",")
            '    sb.AppendLine($"          ""display"": ""{EscapeJson(txtNamaOrganization.Text)}""")
            '    sb.AppendLine("        }")
            '    sb.AppendLine("      }")
            '    sb.AppendLine("    },")


            '    ' =====================================================
            '    ' 4. ENCOUNTER RESOURCE
            '    ' =====================================================
            '    sb.AppendLine("    {")
            '    sb.AppendLine("      ""resource"": {")
            '    sb.AppendLine("        ""resourceType"": ""Encounter"",")
            '    sb.AppendLine($"        ""id"": ""{ID2_encounter}"",")
            '    sb.AppendLine("        ""identifier"": [")
            '    sb.AppendLine("          {")
            '    sb.AppendLine("            ""system"": ""http://api.bpjs-kesehatan.go.id:8080/Vclaim-rest/SEP/"",")
            '    sb.AppendLine($"            ""value"": ""{EscapeJson(dsPendaftaran.NOMORSEP)}""")
            '    sb.AppendLine("          }")
            '    sb.AppendLine("        ],")
            '    sb.AppendLine("        ""subject"": {")
            '    sb.AppendLine($"          ""reference"": ""Patient/{idCustomer}"",")
            '    sb.AppendLine($"          ""display"": ""{EscapeJson(dsPendaftaran.M_CUSTOMER.NAME_DISPLAY)}"",")
            '    sb.AppendLine($"          ""noSep"": ""{EscapeJson(dsPendaftaran.NOMORSEP)}""")
            '    sb.AppendLine("        },")
            '    sb.AppendLine("        ""class"": {")
            '    sb.AppendLine("          ""system"": ""http://hl7.org/fhir/v3/ActCode"",")
            '    If dsPendaftaran.CATEGORY = "1" Then
            '        sb.AppendLine("          ""code"": ""IMP"",")
            '        sb.AppendLine("          ""display"": ""inpatient encounter""")
            '    Else
            '        sb.AppendLine("          ""code"": ""AMB"",")
            '        sb.AppendLine("          ""display"": ""ambulatory encounter""")
            '    End If
            '    sb.AppendLine("        },")
            '    sb.AppendLine("        ""incomingReferral"": [")
            '    sb.AppendLine("          {")
            '    sb.AppendLine("            ""identifier"": [")
            '    sb.AppendLine("              {")
            '    sb.AppendLine("                ""system"": ""nomor_rujukan_bpjs"",")
            '    sb.AppendLine($"                ""value"": ""{EscapeJson(dsPendaftaran.NOMORRUJUKAN)}""")
            '    sb.AppendLine("              },")
            '    sb.AppendLine("              {")
            '    sb.AppendLine("                ""system"": ""nomor_rujukan_internal_rs"",")
            '    sb.AppendLine($"                ""value"": ""{""}""")
            '    sb.AppendLine("              }")
            '    sb.AppendLine("            ]")
            '    sb.AppendLine("          }")
            '    sb.AppendLine("        ],")
            '    sb.AppendLine("        ""reason"": [")
            '    sb.AppendLine("          {")
            '    sb.AppendLine("            ""coding"": [")
            '    sb.AppendLine("              {")
            '    sb.AppendLine("                ""system"": ""http://hl7.org/fhir/sid/icd-10"",")
            '    sb.AppendLine($"                ""code"": ""{EscapeJson(diagnosaUtamaKode)}"",")
            '    If diagnosaUtamaName <> "" Then
            '        sb.AppendLine($"                ""display"": ""{EscapeJson(diagnosaUtamaName)}""")
            '    Else
            '        sb.AppendLine("                ""display"": null")
            '    End If
            '    sb.AppendLine("              }")
            '    sb.AppendLine("            ],")
            '    sb.AppendLine($"            ""text"": ""{EscapeJson(String.Join(", ", diagnosaDisplay.ToArray))}""")
            '    sb.AppendLine("          }")
            '    sb.AppendLine("        ],")
            '    sb.AppendLine("        ""diagnosis"": [")
            '    ' Loop untuk multiple diagnosis

            '    Dim isFirstDiagnosis As Boolean = True

            '    For Each xloop In oGrouper.GetDataDetailDiagnosa(kodegrouper)
            '        If Not isFirstDiagnosis Then
            '            sb.AppendLine(",")
            '        End If

            '        'Dim conditionId As String = fn_UDD(txtKodeBPJS.Text, txtKodeKemenkes.Text, IIf(dsPendaftaran.KDPENDAFTARAN.Contains("RJ"), "2", "1"))
            '        'Dim diagnosisCode As String = If(xloop("KD_DIAGNOSA") IsNot Nothing, xloop("KD_DIAGNOSA").ToString(), "")
            '        'Dim diagnosisName As String = If(xloop("NAMA_DIAGNOSA") IsNot Nothing, xloop("NAMA_DIAGNOSA").ToString(), "")
            '        'Dim rank As Integer = If(xloop("RANK") IsNot Nothing, Convert.ToInt32(xloop("RANK")), diagnosisData.Rows.IndexOf(xloop) + 1)

            '        sb.AppendLine("          {")
            '        sb.AppendLine("            ""condition"": {")
            '        sb.AppendLine($"              ""reference"": ""Condition/{ID3_Condition}"",")
            '        sb.AppendLine("              ""role"": {")
            '        sb.AppendLine("                ""coding"": [")
            '        sb.AppendLine("                  {")
            '        sb.AppendLine("                    ""system"": ""http://hl7.org/fhir/diagnosis-role"",")
            '        sb.AppendLine("                    ""code"": ""DD"",")
            '        sb.AppendLine("                    ""display"": ""Discharge Diagnosis""")
            '        sb.AppendLine("                  }")
            '        sb.AppendLine("                ]")
            '        sb.AppendLine("              },")
            '        sb.AppendLine($"              ""rank"": {xloop.seq + 1}")
            '        sb.AppendLine("            }")
            '        sb.Append("          }")

            '        isFirstDiagnosis = False
            '    Next

            '    sb.AppendLine("        ],")
            '    sb.AppendLine("        ""hospitalization"": {")
            '    sb.AppendLine("          ""dischargeDisposition"": [")
            '    sb.AppendLine("            {")
            '    sb.AppendLine("              ""coding"": [")
            '    sb.AppendLine("                {")
            '    sb.AppendLine("                  ""system"": ""http://hl7.org/fhir/discharge-disposition"",")
            '    'NAIK RANAP
            '    If dsPendaftaran.KDPENDAFTARAN_AWAL = "" Then
            '        sb.AppendLine("                  ""code"": ""home"",")
            '        sb.AppendLine("                  ""display"": ""Home""")
            '    ElseIf dsPendaftaran.KDPENDAFTARAN_AWAL <> "" Then
            '        sb.AppendLine("                  ""code"": ""other"",")
            '        sb.AppendLine("                  ""display"": ""Other""")
            '    Else
            '        sb.AppendLine("                  ""code"": ""unknown"",")
            '        sb.AppendLine("                  ""display"": ""Unknown""")
            '    End If
            '    sb.AppendLine("                }")
            '    sb.AppendLine("              ]")
            '    sb.AppendLine("            }")
            '    sb.AppendLine("          ]")
            '    sb.AppendLine("        },")
            '    sb.AppendLine("        ""period"": {")
            '    sb.AppendLine($"          ""start"": ""{dsPendaftaran.DATE.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture)}"",")

            '    If dsPendaftaran.KODEBOOKING <> "" Then
            '        Dim dsWaktuTungguTaskId7 = oSet_Antrian.GetDataAntrianPoli(dsPendaftaran.KODEBOOKING, 7)

            '        If dsWaktuTungguTaskId7 IsNot Nothing Then
            '            sb.AppendLine($"          ""end"": ""{dsWaktuTungguTaskId7.WAKTU.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture)}""")
            '        Else
            '            Dim dsWaktuTungguTaskId5 = oSet_Antrian.GetDataAntrianPoli(dsPendaftaran.KODEBOOKING, 5)

            '            If dsWaktuTungguTaskId5 IsNot Nothing Then
            '                sb.AppendLine($"          ""end"": ""{dsWaktuTungguTaskId5.WAKTU.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture)}""")
            '            Else
            '                Dim Rnd As New Random()
            '                sb.AppendLine($"          ""end"": ""{dsPendaftaran.DATE.AddHours(Rnd.Next(1, 3)).ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture)}""")
            '            End If
            '        End If
            '    Else
            '        Dim Rnd As New Random()
            '        sb.AppendLine($"          ""end"": ""{dsPendaftaran.DATE.AddHours(Rnd.Next(1, 3)).ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture)}""")
            '        'sb.AppendLine("          ""end"": """"")
            '    End If

            '    sb.AppendLine("        },")
            '    If dsPendaftaran.CATEGORY = "0" Or dsPendaftaran.CATEGORY = "1" Then
            '        sb.AppendLine("        ""status"": ""finished"",")
            '    Else
            '        sb.AppendLine("        ""status"": ""in-progress"",")
            '    End If
            '    sb.AppendLine("        ""text"": {")
            '    sb.AppendLine("          ""status"": ""generated"",")
            '    sb.AppendLine($"          ""div"": ""{EscapeJson("Admitted to " & dsPendaftaran.M_DEPARTMENT.NAME_DISPLAY & ", " & EscapeJson(dsPendaftaran.M_CUSTOMER.NAME_DISPLAY) & " Hospital date " & dsPendaftaran.DATE.ToString("dd MMMM yyyy HH:mm", New CultureInfo("id-ID")) & "")}""")
            '    sb.AppendLine("        }")
            '    sb.AppendLine("      }")

            '    ' =====================================================
            '    ' 5. MEDICATIONREQUEST RESOURCES
            '    ' =====================================================
            '    ' Ambil data resep dari oGrouper

            '    Dim jumlahitemobat As Integer = 0
            '    Dim listObatObatan As New List(Of String)

            '    For Each xloop In oGrouperDataCppt.GetDataDetailNonRacikan(dsCPPT.KDCPPT)
            '        listObatObatan.Add(xloop.NAMAOBAT)
            '        'medicationRefs.Add("MedicationRequest/" & Guid.NewGuid().ToString())
            '        jumlahitemobat += 1
            '    Next

            '    If jumlahitemobat > 0 Then
            '        sb.AppendLine("    },")

            '        sb.AppendLine("    {")
            '        sb.AppendLine("  ""resource"": [")

            '        Dim i As Integer = 0
            '        Dim oSigna As New Reference.clsSigna

            '        For Each xloop In oGrouperDataCppt.GetDataDetailNonRacikan(dsCPPT.KDCPPT)

            '            sb.AppendLine("    {")
            '            sb.AppendLine("      ""resourceType"": ""MedicationRequest"",")
            '            sb.AppendLine("      ""text"": {")
            '            sb.AppendLine($"        ""div"": ""{EscapeJson(xloop.NAMAOBAT)}""")
            '            sb.AppendLine("      },")
            '            sb.AppendLine("      ""identifier"": {")
            '            sb.AppendLine("        ""system"": ""id_resep_pulang"",")
            '            sb.AppendLine($"        ""value"": ""{ID4_Medication}""")
            '            sb.AppendLine("      },")
            '            sb.AppendLine("      ""subject"": {")
            '            sb.AppendLine($"        ""display"": ""{EscapeJson(dsPendaftaran.M_CUSTOMER.NAME_DISPLAY)}"",")
            '            sb.AppendLine($"        ""reference"": ""Patient/{idCustomer}""")
            '            sb.AppendLine("      },")
            '            sb.AppendLine($"      ""intent"": ""final"",")
            '            sb.AppendLine("      ""medicationCodeableConcept"": {")
            '            sb.AppendLine("        ""coding"": [")
            '            sb.AppendLine("          {")
            '            sb.AppendLine($"            ""code"": ""DRx0006657"",")
            '            sb.AppendLine("            ""system"": ""http://rscm.co.id/drug""")
            '            sb.AppendLine("          }")
            '            sb.AppendLine("        ],")
            '            sb.AppendLine($"        ""text"": ""{EscapeJson(xloop.NAMAOBAT)}""")
            '            sb.AppendLine("      },")
            '            sb.AppendLine("      ""dosageInstruction"": [")
            '            sb.AppendLine("        {")
            '            sb.AppendLine("          ""doseQuantity"": {")
            '            sb.AppendLine($"            ""code"": ""{EscapeJson(xloop.SATUAN)}"",")
            '            sb.AppendLine("            ""system"": ""http://unitsofmeasure.org"",")
            '            sb.AppendLine($"            ""unit"": ""{EscapeJson(xloop.SATUAN)}"",")
            '            sb.AppendLine($"            ""value"": ""{CInt(xloop.JUMLAH)}""")
            '            sb.AppendLine("          },")
            '            sb.AppendLine("          ""route"": {")
            '            sb.AppendLine("            ""coding"": [")
            '            sb.AppendLine("              {")
            '            sb.AppendLine("                ""system"": ""http://snomed.info/sct"",")
            '            sb.AppendLine($"                ""code"": ""001"",")
            '            sb.AppendLine($"                ""display"": ""ORAL""")
            '            sb.AppendLine("              }")
            '            sb.AppendLine("            ]")
            '            sb.AppendLine("          },")
            '            sb.AppendLine("          ""timing"": {")
            '            sb.AppendLine("            ""repeat"": {")
            '            sb.AppendLine($"              ""frequency"": {CInt(oSigna.GetData(xloop.KDSIGNA).SIGNA_1)},")
            '            sb.AppendLine($"              ""period"": {CInt(oSigna.GetData(xloop.KDSIGNA).SIGNA_2)},")
            '            sb.AppendLine("              ""periodUnit"": ""d""")
            '            sb.AppendLine("            }")
            '            sb.AppendLine("          },")
            '            sb.AppendLine("          ""additionalInstruction"": [")
            '            sb.AppendLine("            {")
            '            sb.AppendLine($"              ""text"": ""{EscapeJson(xloop.SIGNA)}""")
            '            sb.AppendLine("            }")
            '            sb.AppendLine("          ]")
            '            sb.AppendLine("        }")
            '            sb.AppendLine("      ],")
            '            sb.AppendLine("      ""reasonCode"": [")
            '            sb.AppendLine("        {")
            '            sb.AppendLine("          ""coding"": [")
            '            sb.AppendLine("            {")
            '            sb.AppendLine("              ""code"": """",")
            '            sb.AppendLine("              ""display"": """",")
            '            sb.AppendLine("              ""system"": """"")
            '            sb.AppendLine("            }")
            '            sb.AppendLine("          ],")
            '            sb.AppendLine("          ""text"": """"")
            '            sb.AppendLine("        }")
            '            sb.AppendLine("      ],")
            '            sb.AppendLine("      ""requester"": {")
            '            sb.AppendLine("        ""agent"": {")
            '            sb.AppendLine($"          ""display"": ""{EscapeJson(dsPendaftaran.M_DOCTOR.NAME_DISPLAY)}"",")
            '            sb.AppendLine($"          ""reference"": ""Practitioner/{idDoctor}""")
            '            sb.AppendLine("        },")
            '            sb.AppendLine("        ""onBehalfOf"": {")
            '            sb.AppendLine($"          ""reference"": ""Organization/{idDepartment}""")
            '            sb.AppendLine("        }")
            '            sb.AppendLine("      },")
            '            sb.AppendLine("      ""meta"": {")
            '            sb.AppendLine($"        ""lastUpdated"": ""{dsPendaftaran.DATE.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture)}""")
            '            sb.AppendLine("      }")
            '            sb.AppendLine("    }")

            '            i += 1

            '            If i < jumlahitemobat Then
            '                sb.AppendLine("    ,")
            '            End If

            '        Next

            '        sb.AppendLine("  ]")
            '        sb.AppendLine("    },")
            '    Else
            '        sb.AppendLine("    },")
            '    End If

            '    ' =====================================================
            '    ' 6. CONDITION RESOURCE
            '    ' =====================================================
            '    sb.AppendLine("    {")
            '    sb.AppendLine("      ""resource"": {")
            '    sb.AppendLine("        ""resourceType"": ""Condition"",")
            '    sb.AppendLine($"        ""id"": ""{ID5_Condition}"",")
            '    sb.AppendLine("        ""clinicalStatus"": ""active"",")
            '    sb.AppendLine("        ""verificationStatus"": ""confirmed"",")
            '    sb.AppendLine("        ""category"": [{")
            '    sb.AppendLine("          ""coding"": [{")
            '    sb.AppendLine("            ""system"": ""http://hl7.org/fhir/condition-category"",")
            '    sb.AppendLine("            ""code"": ""encounter-diagnosis"",")
            '    sb.AppendLine("            ""display"": ""Encounter Diagnosis""")
            '    sb.AppendLine("          }]")
            '    sb.AppendLine("        }],")
            '    sb.AppendLine("        ""code"": {")
            '    sb.AppendLine("          ""coding"": [{")
            '    sb.AppendLine("            ""system"": ""http://hl7.org/fhir/sid/icd-10"",")
            '    sb.AppendLine("            ""code"": """ & dsPendaftaran.KDDIAGNOSA & """,")
            '    sb.AppendLine($"            ""display"": ""{EscapeJson(dsPendaftaran.M_DIAGNOSA.MEMO.Replace(dsPendaftaran.KDDIAGNOSA & " - ", ""))}""")
            '    sb.AppendLine("          }],")
            '    sb.AppendLine($"          ""text"": ""{EscapeJson(dsPendaftaran.M_DIAGNOSA.MEMO.Replace(dsPendaftaran.KDDIAGNOSA & " - ", ""))}""")
            '    sb.AppendLine("        },")
            '    sb.AppendLine("        ""subject"": {")
            '    sb.AppendLine($"          ""reference"": ""Patient/{idCustomer}""")
            '    sb.AppendLine("        },")
            '    sb.AppendLine($"        ""onsetDateTime"": ""{dsPendaftaran.DATE.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture)}""")
            '    sb.AppendLine("      }")
            '    sb.AppendLine("    },")

            '    ' =====================================================
            '    ' 6. PROCEDURE RESOURCES
            '    ' =====================================================
            '    ' Ambil data prosedur dari oGrouper

            '    'Dim isFirstProcedure As Boolean = True

            '    'For Each xloop In oGrouper.GetDataDetailProsedur(kodegrouper)
            '    '    If Not isFirstProcedure Then
            '    '        sb.AppendLine(",")
            '    '    End If

            '    '    sb.AppendLine("    {")
            '    '    sb.AppendLine("      ""resource"": {")
            '    '    sb.AppendLine("        ""resourceType"": ""Procedure"",")
            '    '    sb.AppendLine($"        ""id"": ""{ID7_Prosedur}"",")
            '    '    sb.AppendLine("        ""text"": {")
            '    '    sb.AppendLine("          ""status"": ""generated"",")
            '    '    sb.AppendLine("          ""div"": ""Generated Narrative with Details""")
            '    '    sb.AppendLine("        },")
            '    '    sb.AppendLine("        ""status"": ""completed"",")
            '    '    sb.AppendLine("        ""code"": {")
            '    '    sb.AppendLine("          ""coding"": [")
            '    '    sb.AppendLine("            {")
            '    '    sb.AppendLine("              ""system"": ""http://snomed.info/sct"",")
            '    '    sb.AppendLine($"              ""code"": ""{EscapeJson(xloop.kdprpsedur)}"",")
            '    '    sb.AppendLine($"              ""display"": ""{EscapeJson(xloop.memo)}""")
            '    '    sb.AppendLine("            }")
            '    '    sb.AppendLine("          ]")
            '    '    sb.AppendLine("        },")
            '    '    sb.AppendLine("        ""subject"": {")
            '    '    sb.AppendLine($"          ""reference"": ""Patient/{idCustomer}"",")
            '    '    sb.AppendLine($"          ""display"": ""{EscapeJson(dsPendaftaran.M_CUSTOMER.NAME_DISPLAY)}""")
            '    '    sb.AppendLine("        },")
            '    '    sb.AppendLine("        ""context"": {")
            '    '    sb.AppendLine($"          ""reference"": ""Encounter/{ID2_encounter}"",")
            '    '    sb.AppendLine($"          ""display"": ""{EscapeJson(dsPendaftaran.M_CUSTOMER.NAME_DISPLAY)} encounter on {dsPendaftaran.DATE.ToString("dd MMMM yyyy HH:mm", New CultureInfo("id-ID"))}""")
            '    '    sb.AppendLine("        },")
            '    '    sb.AppendLine("        ""performedPeriod"": {")
            '    '    sb.AppendLine($"          ""start"": ""{dsPendaftaran.DATE.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture)}"",")

            '    '    If dsPendaftaran.KODEBOOKING <> "" Then
            '    '        Dim dsWaktuTungguTaskId7 = oSet_Antrian.GetDataBySaveWaktuTunggu(dsPendaftaran.KODEBOOKING, 7)

            '    '        If dsWaktuTungguTaskId7 IsNot Nothing Then
            '    '            sb.AppendLine($"          ""end"": ""{dsWaktuTungguTaskId7.DATE.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture)}""")
            '    '        Else
            '    '            Dim dsWaktuTungguTaskId5 = oSet_Antrian.GetDataBySaveWaktuTunggu(dsPendaftaran.KODEBOOKING, 5)

            '    '            If dsWaktuTungguTaskId5 IsNot Nothing Then
            '    '                sb.AppendLine($"          ""end"": ""{dsWaktuTungguTaskId5.DATE.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture)}""")
            '    '            Else
            '    '                Dim Rnd As New Random()
            '    '                sb.AppendLine($"          ""end"": ""{dsPendaftaran.DATE.AddHours(Rnd.Next(1, 3)).ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture)}""")
            '    '            End If
            '    '        End If
            '    '    Else
            '    '        Dim Rnd As New Random()
            '    '        sb.AppendLine($"          ""end"": ""{dsPendaftaran.DATE.AddHours(Rnd.Next(1, 3)).ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture)}""")
            '    '    End If

            '    '    sb.AppendLine("        },")
            '    '    sb.AppendLine("        ""performer"": [")
            '    '    sb.AppendLine("          {")
            '    '    sb.AppendLine("            ""role"": {")
            '    '    sb.AppendLine("              ""coding"": [")
            '    '    sb.AppendLine("                {")
            '    '    sb.AppendLine("                  ""system"": ""http://snomed.info/sct"",")
            '    '    sb.AppendLine($"                  ""code"": ""{EscapeJson(performerRoleCode)}"",")
            '    '    sb.AppendLine($"                  ""display"": ""{EscapeJson(performerRoleDisplay)}""")
            '    '    sb.AppendLine("                }")
            '    '    sb.AppendLine("              ]")
            '    '    sb.AppendLine("            },")
            '    '    sb.AppendLine("            ""actor"": {")
            '    '    sb.AppendLine($"              ""reference"": ""Practitioner/{idDoctor}"",")
            '    '    sb.AppendLine($"              ""display"": ""{EscapeJson(dsPendaftaran.M_DOCTOR.NAME_DISPLAY)}""")
            '    '    sb.AppendLine("            }")
            '    '    sb.AppendLine("          }")
            '    '    sb.AppendLine("        ],")
            '    '    sb.AppendLine("        ""reasonCode"": [")
            '    '    sb.AppendLine("          {")
            '    '    sb.AppendLine($"            ""text"": ""{EscapeJson(reasonText)}""")
            '    '    sb.AppendLine("          }")
            '    '    sb.AppendLine("        ],")
            '    '    sb.AppendLine("        ""bodySite"": [")
            '    '    sb.AppendLine("          {")
            '    '    sb.AppendLine("            ""coding"": [")
            '    '    sb.AppendLine("              {")
            '    '    sb.AppendLine("                ""system"": ""http://snomed.info/sct"",")
            '    '    sb.AppendLine($"                ""code"": ""{EscapeJson(bodySiteCode)}"",")
            '    '    sb.AppendLine($"                ""display"": ""{EscapeJson(bodySiteDisplay)}""")
            '    '    sb.AppendLine("              }")
            '    '    sb.AppendLine("            ]")
            '    '    sb.AppendLine("          }")
            '    '    sb.AppendLine("        ],")
            '    '    sb.AppendLine("        ""focalDevice"": [")
            '    '    sb.AppendLine("          {")
            '    '    sb.AppendLine("            ""action"": {")
            '    '    sb.AppendLine("              ""coding"": [")
            '    '    sb.AppendLine("                {")
            '    '    sb.AppendLine("                  ""system"": ""http://hl7.org/fhir/device-action"",")
            '    '    sb.AppendLine("                  ""code"": ""implanted""")
            '    '    sb.AppendLine("                }")
            '    '    sb.AppendLine("              ]")
            '    '    sb.AppendLine("            },")
            '    '    sb.AppendLine("            ""manipulated"": {")
            '    '    sb.AppendLine("              ""reference"": ""Device/example-pacemaker""")
            '    '    sb.AppendLine("            }")
            '    '    sb.AppendLine("          }")
            '    '    sb.AppendLine("        ],")
            '    '    sb.AppendLine("        ""note"": [")
            '    '    sb.AppendLine("          {")
            '    '    sb.AppendLine($"            ""text"": ""{EscapeJson(noteText)}""")
            '    '    sb.AppendLine("          }")
            '    '    sb.AppendLine("        ]")
            '    '    sb.AppendLine("      }")
            '    '    sb.Append("    }")

            '    '    isFirstProcedure = False
            '    'Next

            '    ' =====================================================
            '    ' 8. COMPOSITION RESOURCE (Terakhir)
            '    ' =====================================================
            '    sb.AppendLine("    {")
            '    sb.AppendLine("      ""resource"": {")
            '    sb.AppendLine("        ""resourceType"": ""Composition"",")
            '    sb.AppendLine($"        ""id"": ""{ID6_Composition}"",")
            '    sb.AppendLine("        ""status"": ""final"",")
            '    sb.AppendLine("        ""type"": {")
            '    sb.AppendLine("          ""coding"": [{""system"": ""http://loinc.org"", ""code"": ""81218-0""}],")
            '    sb.AppendLine("          ""text"": ""Discharge Summary""")
            '    sb.AppendLine("        },")
            '    sb.AppendLine("        ""subject"": {")
            '    sb.AppendLine($"          ""reference"": ""Patient/{idCustomer}"",")
            '    sb.AppendLine($"          ""display"": ""{EscapeJson(dsPendaftaran.M_CUSTOMER.NAME_DISPLAY)}""")
            '    sb.AppendLine("        },")
            '    sb.AppendLine("        ""encounter"": {")
            '    sb.AppendLine($"          ""reference"": ""Encounter/{ID2_encounter}""")
            '    sb.AppendLine("        },")
            '    sb.AppendLine($"        ""date"": ""{dsCPPT.DATE.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture)}"",")
            '    sb.AppendLine("        ""author"": [{")
            '    sb.AppendLine($"          ""reference"": ""Practitioner/{idDoctor}"",")
            '    sb.AppendLine($"          ""display"": ""{EscapeJson(dsPendaftaran.M_DOCTOR.NAME_DISPLAY)}""")
            '    sb.AppendLine("        }],")
            '    sb.AppendLine("        ""title"": ""Discharge Summary"",")
            '    sb.AppendLine("        ""confidentiality"": ""N"",")
            '    sb.AppendLine("        ""section"": {")

            '    ' Section 0 - Reason for admission
            '    sb.AppendLine("          ""0"": {")
            '    sb.AppendLine("            ""title"": ""Reason for admission"",")
            '    sb.AppendLine("            ""code"": {")
            '    sb.AppendLine("              ""coding"": [{""system"": ""http://loinc.org"", ""code"": ""29299-5"", ""display"": ""Reason for visit Narrative""}]")
            '    sb.AppendLine("            },")
            '    sb.AppendLine("            ""text"": {""status"": ""additional"", ""div"": """ & EscapeJson(IIf(dsPendaftaran.TUJUANKUNJUNGAN = "", "Normal", dsPendaftaran.TUJUANKUNJUNGAN)) & """},")
            '    sb.AppendLine("            ""entry"": []")
            '    sb.AppendLine("          },")

            '    ' Section 1 - Chief complaint
            '    sb.AppendLine("          ""1"": {")
            '    sb.AppendLine("            ""title"": ""Chief complaint"",")
            '    sb.AppendLine("            ""code"": {")
            '    sb.AppendLine("              ""coding"": [{""system"": ""http://loinc.org"", ""code"": ""10154-3"", ""display"": ""Chief complaint Narrative""}]")
            '    sb.AppendLine("            },")
            '    sb.AppendLine("            ""text"": {""status"": ""additional"", ""div"": """ & EscapeJson(dsCPPT.SUBJEKTIF_KELUHANUTAMA) & """},")
            '    sb.AppendLine("            ""entry"": []")
            '    sb.AppendLine("          },")
            '    '""{EscapeJson(section2)}"""
            '    ' Section 2 - Admission diagnosis
            '    sb.AppendLine("          ""2"": {")
            '    sb.AppendLine("            ""title"": ""Admission diagnosis"",")
            '    sb.AppendLine("            ""code"": {")
            '    sb.AppendLine("              ""coding"": [{""system"": ""http://loinc.org"", ""code"": ""42347-5"", ""display"": ""Admission diagnosis Narrative""}]")
            '    sb.AppendLine("            },")
            '    sb.AppendLine("            ""text"": {""status"": ""additional"", ""div"": """ & EscapeJson(dsPendaftaran.M_DIAGNOSA.MEMO) & """},")
            '    sb.AppendLine("            ""entry"": []")
            '    'sb.AppendLine("            ""entry"": [{""reference"": """ & conditionId & """}]")
            '    sb.AppendLine("          },")

            '    ' Section 3 - Discharge diagnosis
            '    sb.AppendLine("          ""3"": {")
            '    sb.AppendLine("            ""title"": ""Discharge diagnosis"",")
            '    sb.AppendLine("            ""code"": {")
            '    sb.AppendLine("              ""coding"": [{""system"": ""http://loinc.org"", ""code"": ""78375-3"", ""display"": ""Discharge diagnosis Narrative""}]")
            '    sb.AppendLine("            },")
            '    sb.AppendLine("            ""text"": {""status"": ""additional"", ""div"": ""<div>" & EscapeJson(diagnosaUtamaName) & "</div>""},")
            '    sb.AppendLine("            ""entry"": [{""reference"": """ & EscapeJson(String.Join(", ", diagnosaDisplay.ToArray)) & """}]")
            '    sb.AppendLine("          },")

            '    ' Section 4 - Medications on Discharge
            '    sb.AppendLine("          ""4"": {")
            '    sb.AppendLine("            ""title"": ""Medications on Discharge"",")
            '    sb.AppendLine("            ""code"": {")
            '    sb.AppendLine("              ""coding"": [{""system"": ""http://loinc.org"", ""code"": ""75311-1"", ""display"": ""Hospital discharge medications Narrative""}]")
            '    sb.AppendLine("            },")
            '    sb.AppendLine("            ""text"": {""status"": ""additional"", ""div"": """ & EscapeJson(String.Join(", ", listObatObatan.ToArray)) & """},")
            '    sb.AppendLine("            ""mode"": ""working"",")
            '    'sb.AppendLine("            ""entry"": []")
            '    sb.AppendLine("            ""entry"": [{""reference"": """ & "MedicationRequest/" & ID4_Medication & """}]")
            '    sb.AppendLine("          },")

            '    '"entry" [
            '    '      {
            '    '        "reference": "MedicationRequest/1002R005-3204086-2-ITM0002571"
            '    '      },
            '    '      {
            '    '        "reference": "MedicationRequest/1002R005-3204086-2-ITM0005231"
            '    '      }
            '    '    ]


            '    ' Section 5 - Plan of care
            '    sb.AppendLine("          ""5"": {")
            '    sb.AppendLine("            ""title"": ""Plan of care"",")
            '    sb.AppendLine("            ""code"": {")
            '    sb.AppendLine("              ""coding"": [{""system"": ""http://loinc.org"", ""code"": ""18776-5"", ""display"": ""Plan of care""}]")
            '    sb.AppendLine("            },")
            '    sb.AppendLine("            ""text"": {""status"": ""additional"", ""div"": """ & EscapeJson(dsCPPT.PLANNING_TEXT) & """},")
            '    sb.AppendLine("            ""mode"": ""working"",")
            '    sb.AppendLine("            ""entry"": []")
            '    sb.AppendLine("          },")

            '    ' Section 7 - Known allergies
            '    sb.AppendLine("          ""7"": {")
            '    sb.AppendLine("            ""title"": ""Known allergies"",")
            '    sb.AppendLine("            ""code"": {")
            '    sb.AppendLine("              ""coding"": [{""system"": ""http://loinc.org"", ""code"": ""48765-2"", ""display"": ""Allergies and adverse reactions""}]")
            '    sb.AppendLine("            },")
            '    sb.AppendLine("            ""text"": {""status"": ""additional"", ""div"": """ & EscapeJson(dsCPPT.SUBJEKTIF_ALERGI_YA_TEXT) & """},")
            '    sb.AppendLine("            ""entry"": []")
            '    'sb.AppendLine("            ""entry"": [{""reference"": """ & allergyId & """}]")
            '    sb.AppendLine("          }")
            '    sb.AppendLine("        }")
            '    sb.AppendLine("      }")
            '    sb.AppendLine("    }")


            '    sb.AppendLine("  ]")
            '    sb.AppendLine("}")


            '    Dim compressionResult As CompressionResult = FhirGzipCompressor.CompressWithStats(sb.ToString())

            '    Dim dataMR As String = oKoneksi.EncryptBPJS2(sRMEBPJS_ConsId, sRMEBPJS_SecreatKey, sPPKPELAYANAN, compressionResult.CompressedBase64)

            '    Dim jsonRequest As String = String.Empty

            '    jsonRequest = "{ "
            '    jsonRequest &= " ""request"" :   { "
            '    jsonRequest &= " ""noSep"" :  """ & dsPendaftaran.NOMORSEP & "" & "" & """ , "
            '    jsonRequest &= " ""jnsPelayanan"" :  """ & "" & IIf(dsPendaftaran.CATEGORY = 0, "2", "1") & "" & """ , "
            '    jsonRequest &= " ""bulan"" :  """ & "" & CInt(CDate(dsPendaftaran.DATE).ToString("MM")) & "" & """ , "
            '    jsonRequest &= " ""tahun"" :  """ & "" & CDate(dsPendaftaran.DATE).ToString("yyyy") & "" & """ , "
            '    jsonRequest &= " ""dataMR"" :  """ & dataMR & """ "
            '    jsonRequest &= " } "
            '    jsonRequest &= " } "

            '    Dim uTime As Integer = (DateTime.UtcNow - New DateTime(1970, 1, 1, 0, 0, 0)).TotalSeconds

            '    Dim kirim As String = oKoneksi.InsertMedicalRecord(sRMEBPJS_Url, sRMEBPJS_ConsId, sRMEBPJS_SecreatKey, sRMEBPJS_UserKey, uTime, jsonRequest)

            '    If SatusehatAuth.GetToken(kirim, "message") = "OK" Then
            '        BuildBundleWith6Resources = fn_SaveBPJSRME_Pendaftaran(dsPendaftaran.KDPENDAFTARAN, ID1_Bundle, ID2_encounter, ID3_Condition, ID4_Medication, ID5_Condition, ID6_Composition, ID7_Prosedur, sb.ToString(), kirim, Pesan)
            '    Else
            '        fn_SaveBPJSRME_Pendaftaran(dsPendaftaran.KDPENDAFTARAN, ID1_Bundle, ID2_encounter, ID3_Condition, ID4_Medication, ID5_Condition, ID6_Composition, ID7_Prosedur, sb.ToString(), kirim, Pesan)

            '        BuildBundleWith6Resources = False

            '        If Pesan = True Then
            '            MsgBox(kirim, MsgBoxStyle.Information, Me.Text)
            '        End If
            '    End If
            'End If
        End If
    End Function
    Private Function EscapeJson(text As String) As String
        If String.IsNullOrEmpty(text) Then Return ""

        With text
            Return .Replace("\", "\\") _
               .Replace("""", "\""") _
               .Replace(vbCr, "\r") _
               .Replace(vbLf, "\n") _
               .Replace(vbTab, "\t")
        End With
    End Function
#End Region
#End Region
#Region "Command Button"
    Private Sub grv_RowStyle(sender As System.Object, e As DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs) Handles grv.RowStyle
        If grv.IsFilterRow(e.RowHandle) Then Exit Sub

        If grv.GetRowCellValue(e.RowHandle, "CEKDATA") <> "" Then
            If grv.GetRowCellValue(e.RowHandle, "CEKDATA").ToString.Contains("Sukses") Then
                e.Appearance.BackColor = Color.LightGreen
            Else
                e.Appearance.BackColor = Color.HotPink
            End If
        Else
            If grv.GetRowCellValue(e.RowHandle, "CEKDATAGROUPER") = "Terkirim" Then
                e.Appearance.BackColor = Color.Yellow
            End If
        End If
    End Sub
    Private Sub frmMember_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        Select Case e.KeyCode
            Case Keys.Escape
                Me.Close()
            Case Keys.P
                If e.Alt = True And picPrint.Enabled = True Then
                    picPrint_Click()
                End If
            Case Keys.R
                If e.Alt = True And picRefresh.Enabled = True Then
                    picRefresh_Click()
                End If
        End Select
    End Sub
    Private Sub picPrint_Click() Handles picPrint.Click
        Try
            fn_Print()
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub picRefresh_Click() Handles picRefresh.Click
        fn_LoadSecurity()
    End Sub
    Private Sub CekKirimSatuRegisterToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CekKirimSatuRegisterToolStripMenuItem.Click
        Try
            If txtKodeBPJS.Text = "" And txtKodeKemenkes.Text = "" And txtNamaOrganization.Text = "" Then
                MsgBox("Data BPJS, Kemenkes dan Nama Organization Kosong", MsgBoxStyle.Information, Me.Text)
                Exit Sub
            End If

            If grv.GetFocusedRowCellValue("KDPENDAFTARAN") Is Nothing Then
                MsgBox("Register Kosong", MsgBoxStyle.Information, Me.Text)
                Exit Sub
            End If

            If MsgBox("Apakah Akan Kirim Data Ke Satu Sehat ?", MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, Me.Text) = MsgBoxResult.No Then Exit Sub

            SplashScreenManager.ShowForm(Me, GetType(frmLoadingPanel), True, True, False)

            If grv.GetFocusedRowCellValue("CEKDATAGROUPER") <> "" Then
                If BuildBundleWith6Resources(grv.GetFocusedRowCellValue("KDPENDAFTARAN"), True) = False Then
                    SplashScreenManager.CloseForm(False)
                    MsgBox("Gagal Kirim", MsgBoxStyle.Information, Me.Text)
                Else
                    SplashScreenManager.CloseForm(False)
                    MsgBox("Berhasil Kiirm", MsgBoxStyle.Information, Me.Text)
                End If
            Else
                SplashScreenManager.CloseForm(False)
                MsgBox("Data Grouper Kosong", MsgBoxStyle.Information, Me.Text)
            End If

        Catch oErr As Exception
            SplashScreenManager.CloseForm(False)
            MsgBox("Eror Looping" & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub KirimSemuaDiGridToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles KirimSemuaDiGridToolStripMenuItem.Click
        Try
            If txtKodeBPJS.Text = "" And txtKodeKemenkes.Text = "" And txtNamaOrganization.Text = "" Then
                MsgBox("Data BPJS, Kemenkes dan Nama Organization Kosong", MsgBoxStyle.Information, Me.Text)
                Exit Sub
            End If

            Dim counter As Integer = 0
            Dim counterGagal As Integer = 0
            Dim counterAll As Integer = 0

            For i As Integer = 0 To grv.RowCount - 1
                If grv.GetRowCellValue(i, "CEKDATA") = "" Then
                    If grv.GetRowCellValue(i, "CEKDATAGROUPER") <> "" Then
                        counterAll += 1
                    End If
                End If
            Next

            If MsgBox("Apakah Akan Kirim Data Ke Satu Sehat Sebanyak " & counterAll & "?", MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, Me.Text) = MsgBoxResult.No Then Exit Sub

            SplashScreenManager.ShowForm(Me, GetType(frmLoadingPanel), True, True, False)

            Dim indexProses As Integer = 0

            For i As Integer = 0 To grv.RowCount - 1
                If grv.GetRowCellValue(i, "CEKDATA") = "" Then
                    If grv.GetRowCellValue(i, "CEKDATAGROUPER") <> "" Then
                        indexProses += 1

                        SplashScreenManager.Default.SetWaitFormDescription($"Mengirim data {indexProses} dari {counterAll}...")

                        If BuildBundleWith6Resources(grv.GetRowCellValue(i, "KDPENDAFTARAN"), False) = False Then
                            counterGagal += 1
                        Else
                            counter += 1
                        End If

                        Threading.Thread.Sleep(5000)
                    End If
                End If
            Next

            SplashScreenManager.CloseForm(False)

            MsgBox("Kirim Data : " & counterAll & vbCrLf & "*Berhasil : " & counter & vbCrLf & "*Gagal : " & counterGagal, MsgBoxStyle.Information, Me.Text)
        Catch oErr As Exception
            SplashScreenManager.CloseForm(False)
            MsgBox("Eror Looping" & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
#End Region
End Class