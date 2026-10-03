Imports DataAccess
Imports System.Linq
Imports System.Data.SqlClient
Imports Newtonsoft.Json.Linq
Imports System.Drawing.Text
Imports System.IO
Imports System.Net
Imports System.Text

Public Class frmMedrekRawatJalanList
    Private sLoadOrder As Boolean = False
    Private sClearList As Boolean = False
    Private oDoctor As New Reference.clsDoctor
    Private sKODEBOOKING As String = String.Empty
    Private sTANGGALLAHIR As DateTime = Now
    Private sKARTUBPJS As String = String.Empty
    Private sUMUPASIEN As String = String.Empty
    Private sJK As String = String.Empty
    Private sisDOkter As Boolean = False
    Private isLoad As Boolean = False

#Region "Function"
    Public Sub fn_LoadMe(ByVal isDOKTER As Boolean)
        sisDOkter = isDOKTER
        If isDOKTER = False Then
            AddToolStripMenuItem.Visible = False
            PemeriksaanAwalToolStripMenuItem.Visible = True
            XtraTabPage1.PageVisible = False
            XtraTabPage4.PageVisible = False
        Else
            AddToolStripMenuItem.Visible = True
            PemeriksaanAwalToolStripMenuItem.Visible = False
            XtraTabPage1.PageVisible = True
            XtraTabPage4.PageVisible = True
        End If
    End Sub
    Private Sub Me_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        deDATE.DateTime = Now
        fn_LoadSecurity()
        fn_EmptyMe()

        Dim dsDepartment = oDoctor.GetDataByIDUser(sUserID)
        If dsDepartment IsNot Nothing Then
            grdKDDEPARTMENT.EditValue = dsDepartment.KDDEPARTMENT
            fn_LoadDokter(dsDepartment.KDDOCTOR)
            If grdKDDEPARTMENT.Text <> "" Then
                fn_LoadData()
            End If
        End If
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
                'picAdd.Enabled = ds.ISADD
                'picDelete.Enabled = ds.ISDELETE
                'picUpdate.Enabled = ds.ISUPDATE
                'picPrint.Enabled = ds.ISPRINT
                'picRefresh.Enabled = ds.ISVIEW

                If ds.ISVIEW = True Then
                    fn_LoadView()
                    'tabControl_SelectedPageChanged()
                    XtraTabControl1.SelectedTabPageIndex = 0
                    XtraTabControl1.SelectedTabPageIndex = 4
                    XtraTabControl1_SelectedPageChanged()
                End If
            Catch ex As Exception
                MsgBox("Keamanan belum dipasang, tolong kontak admin anda!", MsgBoxStyle.Exclamation, Me.Text)

                'picAdd.Enabled = False
                'picDelete.Enabled = False
                'picUpdate.Enabled = False
                'picPrint.Enabled = False
                'picRefresh.Enabled = False
            End Try
        Catch ex As Exception
            MsgBox("Load Security : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadView()
        fn_LoadDEPARTMENT()
    End Sub
    Private Sub fn_EmptyMe()
        lblNamaPasien.Text = "Nul"
        lblTanggalLahirUsia.Text = "Nul"
        lblRM.Text = "Nul"
        lblRegister.Text = "Nul"
        lblDPJP.Text = "Nul"
        lblPenjamin.Text = "Nul"
        lblSIP.Text = "Nul"
        lblNOMORSEP.Text = "Nul"
        sKODEBOOKING = ""

        'grvListPasien.OptionsSelection.MultiSelect = True
        'grvListPasien.SelectAll()
        'grvListPasien.DeleteSelectedRows()
        'grvListPasien.OptionsSelection.MultiSelect = False

        'grvResep.OptionsSelection.MultiSelect = True
        'grvResep.SelectAll()
        'grvResep.DeleteSelectedRows()
        'grvResep.OptionsSelection.MultiSelect = False

        grvBillingTambahan.OptionsSelection.MultiSelect = True
        grvBillingTambahan.SelectAll()
        grvBillingTambahan.DeleteSelectedRows()
        grvBillingTambahan.OptionsSelection.MultiSelect = False

        'grvResume.OptionsSelection.MultiSelect = True
        'grvResume.SelectAll()
        'grvResume.DeleteSelectedRows()
        'grvResume.OptionsSelection.MultiSelect = False

        'grvSKD.OptionsSelection.MultiSelect = True
        'grvSKD.SelectAll()
        'grvSKD.DeleteSelectedRows()
        'grvSKD.OptionsSelection.MultiSelect = False

        grvHystori.OptionsSelection.MultiSelect = True
        grvHystori.SelectAll()
        grvHystori.DeleteSelectedRows()
        grvHystori.OptionsSelection.MultiSelect = False

        PdfViewerResep.CloseDocument()
        PdfViewerBillingTambahan.CloseDocument()
        PdfViewerResume.CloseDocument()
        PdfViewerSKD.CloseDocument()
        PdfViewerCPPTRawatJalan.CloseDocument()
    End Sub
    Private Sub grvListPasien_FocusedRowChanged(ByVal sender As System.Object, ByVal e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs) Handles grvListPasien.FocusedRowChanged
        If tabControl.SelectedTabPageIndex = 0 Then
            If grvListPasien.GetFocusedRowCellValue("NoRegister") Is Nothing Then
                fn_EmptyMe()
                Exit Sub
            End If
        End If
        If sisDOkter = False Then
            XtraTabControl1_SelectedPageChanged()
        Else
            XtraTabControl1.SelectedTabPageIndex = 0
        End If


        'XtraTabControl1_SelectedPageChanged()

        lblNamaPasien.Text = grvListPasien.GetFocusedRowCellValue("Pasien")
        lblRM.Text = grvListPasien.GetFocusedRowCellValue("KDCUSTOMER")
        'Dim TANGGALLAHIR As DateTime = grvListPasien.GetFocusedRowCellValue("TANGGALLAHIR")
        sTANGGALLAHIR = grvListPasien.GetFocusedRowCellValue("TANGGALLAHIR")
        lblTanggalLahirUsia.Text = sTANGGALLAHIR.ToString("dd-MM-yyyy") & " - " & grvListPasien.GetFocusedRowCellValue("USIA")
        txtDIAGNOSAUTAMA.Text = grvListPasien.GetFocusedRowCellValue("DIAGNOSA")
        deDATE_MASUK.DateTime = CDate(grvListPasien.GetFocusedRowCellValue("DATE"))
        lblRegister.Text = grvListPasien.GetFocusedRowCellValue("NoRegister")
        lblPenjamin.Text = grvListPasien.GetFocusedRowCellValue("PENJAMIN")
        lblDPJP.Text = grvListPasien.GetFocusedRowCellValue("Dokter")
        lblSIP.Text = grvListPasien.GetFocusedRowCellValue("SIP")
        lblNOMORSEP.Text = grvListPasien.GetFocusedRowCellValue("NOMORSEP")
        sKODEBOOKING = grvListPasien.GetFocusedRowCellValue("Antrian")
        sKARTUBPJS = grvListPasien.GetFocusedRowCellValue("KARTUBPJS")
        sJK = grvListPasien.GetFocusedRowCellValue("JK")
        sUMUPASIEN = grvListPasien.GetFocusedRowCellValue("USIA")


        Dim oReqAwalPemeriksaan As New Transaksi.clsReqAwalPemeriksaan
        Dim dsReqAwalPemeriksaan = oReqAwalPemeriksaan.GetData(lblRegister.Text)
        If dsReqAwalPemeriksaan IsNot Nothing Then
            txtBERATBADAN.Text = dsReqAwalPemeriksaan.OBJEKTIF_BERATBADAN
            txtTB.Text = dsReqAwalPemeriksaan.OBJEKTIF_BERATBADAN
        Else
            Dim oCustomer_Detil As New Reference.clsCustomerDetil
            Dim dsCustomer_Detil = oCustomer_Detil.GetData(lblRM.Text)
            If dsCustomer_Detil IsNot Nothing Then
                txtBERATBADAN.Text = dsCustomer_Detil.BB
                txtTB.Text = dsCustomer_Detil.TT
                txtALERGIOBAT.Text = dsCustomer_Detil.ALERGI
                txtDIAGNOSAUTAMA.Text = dsCustomer_Detil.DIAGNOSA
            End If
        End If

        Dim oCustomer_Detil_ As New Reference.clsCustomerDetil
        Dim dsCustomer_Detil_ = oCustomer_Detil_.GetData(lblRM.Text)
        If dsCustomer_Detil_ IsNot Nothing Then
            txtALERGIOBAT.Text = dsCustomer_Detil_.ALERGI
            txtDIAGNOSAUTAMA.Text = dsCustomer_Detil_.DIAGNOSA
        End If

        fn_LoadHystori(lblRM.Text)

        'If lblRegister.Text = "" Then
        '    MsgBox("Pasien Belum dipilih, Silahkan kembali ke List Pasien", MsgBoxStyle.Exclamation, Me.Text)
        '    fn_EmptyMe()
        '    Exit Sub
        'End If

        'Dim oKoding As New Admission.clsKoding
        'Dim dsKoding = oKoding.GetDataByKDPENDAFTARAN(lblRegister.Text)
        'If dsKoding IsNot Nothing Then
        '    Dim frmMedrekRawatJalan As New frmMedrekRawatJalan
        '    Try
        '        frmMedrekRawatJalan.fn_LoadIdentias(txtBERATBADAN.Text, txtTB.Text, txtALERGIOBAT.Text, lblRegister.Text, lblTanggalLahirUsia.Text, lblNamaPasien.Text, lblRM.Text, lblDPJP.Text, grdKDDEPARTMENT.Text, lblSIP.Text, lblPenjamin.Text, sKODEBOOKING, grdKDDEPARTMENT.EditValue, grdDPJP.EditValue, lblNOMORSEP.Text, sKARTUBPJS, txtDIAGNOSAUTAMA.Text, sTANGGALLAHIR)
        '        frmMedrekRawatJalan.LoadMe(FORM_MODE.FORM_MODE_EDIT, dsKoding.KDKODING)
        '        frmMedrekRawatJalan.ShowDialog(Me)
        '        fn_LoadSecurity()
        '    Catch ex As Exception
        '        MsgBox("Load Form Detail : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
        '    Finally
        '        If Not frmMedrekRawatJalan Is Nothing Then frmMedrekRawatJalan.Dispose()
        '        frmMedrekRawatJalan = Nothing

        '        Dim rowHandle As Integer = grvListPasien.LocateByValue(rowHandle, grvListPasien.Columns("NoRegister"), sCode)
        '        If rowHandle > 0 Then grvListPasien.FocusedRowHandle = rowHandle

        '        If sStatusSave = "NEW" Then
        '            sStatusSave = "NONE"
        '        End If
        '    End Try
        'Else
        '    Dim frmMedrekRawatJalan As New frmMedrekRawatJalan
        '    Try
        '        frmMedrekRawatJalan.fn_LoadIdentias(txtBERATBADAN.Text, txtTB.Text, txtALERGIOBAT.Text, lblRegister.Text, lblTanggalLahirUsia.Text, lblNamaPasien.Text, lblRM.Text, lblDPJP.Text, grdKDDEPARTMENT.Text, lblSIP.Text, lblPenjamin.Text, sKODEBOOKING, grdKDDEPARTMENT.EditValue, grdDPJP.EditValue, lblNOMORSEP.Text, sKARTUBPJS, txtDIAGNOSAUTAMA.Text, sTANGGALLAHIR)
        '        frmMedrekRawatJalan.LoadMe(FORM_MODE.FORM_MODE_ADD)
        '        frmMedrekRawatJalan.ShowDialog(Me)
        '        fn_LoadSecurity()
        '    Catch ex As Exception
        '        MsgBox("Load Form Detail : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
        '    Finally
        '        If Not frmMedrekRawatJalan Is Nothing Then frmMedrekRawatJalan.Dispose()
        '        frmMedrekRawatJalan = Nothing

        '        Dim rowHandle As Integer = grvListPasien.LocateByValue(rowHandle, grvListPasien.Columns("NoRegister"), sCode)
        '        If rowHandle > 0 Then grvListPasien.FocusedRowHandle = rowHandle

        '        If sStatusSave = "NEW" Then
        '            sStatusSave = "NONE"
        '        End If
        '    End Try
        'End If
    End Sub
    Private Sub fn_LoadData()
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
            SQL &= "NoRegister = B.KDREG "
            SQL &= ",AntrianDokter = (SELECT CASE B.ISRESUME WHEN 0 THEN (SELECT CASE B.ANTRIANDOKTER WHEN 0 THEN 'X' ELSE 'A' + RIGHT('000' + CONVERT(NVARCHAR(50), B.ANTRIANDOKTER), 3) END) ELSE 'Y' END) "
            SQL &= ",Antrian = B.NOMORANTRIAN "
            SQL &= ",Pasien = D.NAME_DISPLAY "
            SQL &= ",Dokter = C.NAME_DISPLAY "
            SQL &= ",SIP = C.SIP "
            SQL &= ",B.KDCUSTOMER "
            SQL &= ",B.USIA "
            SQL &= ",D.TANGGALLAHIR "
            SQL &= ",DIAGNOSA = E.KDDIAGNOSA + ' - ' + E.DESCRIPTION "
            SQL &= ",B.DATE "
            SQL &= ",PENJAMIN = F.NAME_DISPLAY "
            SQL &= ",B.NOMORSEP "
            SQL &= ",B.KARTUBPJS "
            SQL &= ",Cek = B.ISRESUME "
            SQL &= ",C.KDDOCTOR "
            SQL &= ",D.JK "
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
            SQL &= "WHERE CONVERT(VARCHAR(8), B.DATE, 112) = '" & deDATE.DateTime.ToString("yyyyMMdd") & "' "
            SQL &= "AND B.KDDEPARTMENT = " & grdKDDEPARTMENT.EditValue & " "
            SQL &= "AND B.KDDOCTOR = " & grdDPJP.EditValue & " "
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

            fn_LoadFormatData()

            grvListPasien.BestFitColumns()

        Catch oErr As Exception
            MsgBox("Load List Pasien Data : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadFormatData()
        For iLoop As Integer = 0 To grvListPasien.Columns.Count - 1
            If grvListPasien.Columns(iLoop).ColumnType.Name = "Decimal" Then
                grvListPasien.Columns(iLoop).DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                grvListPasien.Columns(iLoop).DisplayFormat.FormatString = "{0:n2}"
                grvListPasien.Columns(iLoop).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
                grvListPasien.GroupSummary.Add(DevExpress.Data.SummaryItemType.Sum, grvListPasien.Columns(iLoop).FieldName, grvListPasien.Columns(iLoop),
                                     "{0:n2}")
                grvListPasien.Columns(iLoop).SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                grvListPasien.Columns(iLoop).SummaryItem.DisplayFormat = "{0:n2}"
            ElseIf grvListPasien.Columns(iLoop).ColumnType.Name = "DateTime" Then
                grvListPasien.Columns(iLoop).DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
                grvListPasien.Columns(iLoop).DisplayFormat.FormatString = "{0:HH:mm:ss}"
            End If
        Next

        grvListPasien.Columns("PENJAMIN").Caption = "Penjamin"
        grvListPasien.Columns("DATE").Caption = "Jam Pendaftaran"

        grvListPasien.Columns("Antrian").VisibleIndex = -1
        grvListPasien.Columns("NoRegister").VisibleIndex = -1
        grvListPasien.Columns("Dokter").VisibleIndex = -1
        grvListPasien.Columns("SIP").VisibleIndex = -1
        grvListPasien.Columns("KDCUSTOMER").VisibleIndex = -1
        grvListPasien.Columns("USIA").VisibleIndex = -1
        grvListPasien.Columns("TANGGALLAHIR").VisibleIndex = -1
        grvListPasien.Columns("DIAGNOSA").VisibleIndex = -1
        'grvListPasien.Columns("DATE").VisibleIndex = -1
        'grvListPasien.Columns("PENJAMIN").VisibleIndex = -1
        grvListPasien.Columns("NOMORSEP").VisibleIndex = -1
        grvListPasien.Columns("KARTUBPJS").VisibleIndex = -1
        grvListPasien.Columns("Cek").VisibleIndex = -1
        grvListPasien.Columns("KDDOCTOR").VisibleIndex = -1
        grvListPasien.Columns("JK").VisibleIndex = -1
    End Sub
    Private Sub fn_LoadDataResep(ByVal KDREG As String)
        'Try
        '    If grdKDDEPARTMENT.Text = "" Then Exit Sub

        '    Dim oReq_Recipe As New Transaksi.clsReq_Recipe

        '    Dim ds = From x In oReq_Recipe.GetDataByRekamMedis(Parameter)
        '             Select NoRegister = x.KDPENDAFTARAN, NoKode = x.KDREQRECIPE, Tanggal = x.DATE, Tujuan = x.TUJUAN, DPJP = x.DOKTER

        '    grdResep.DataSource = ds.ToList()

        '    fn_LoadFormatResep()

        '    grvResep.BestFitColumns()
        'Catch oErr As Exception
        '    MsgBox("Load Resep Data : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        'End Try
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
    'Private Sub fn_LoadFormatResep()
    '    For iLoop As Integer = 0 To grvResep.Columns.Count - 1
    '        If grvResep.Columns(iLoop).ColumnType.Name = "Decimal" Then
    '            grvResep.Columns(iLoop).DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
    '            grvResep.Columns(iLoop).DisplayFormat.FormatString = "{0:n2}"
    '            grvResep.Columns(iLoop).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    '            grvResep.GroupSummary.Add(DevExpress.Data.SummaryItemType.Sum, grvResep.Columns(iLoop).FieldName, grvResep.Columns(iLoop),
    '                                 "{0:n2}")
    '            grvResep.Columns(iLoop).SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
    '            grvResep.Columns(iLoop).SummaryItem.DisplayFormat = "{0:n2}"
    '        ElseIf grvResep.Columns(iLoop).ColumnType.Name = "DateTime" Then
    '            grvResep.Columns(iLoop).DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
    '            grvResep.Columns(iLoop).DisplayFormat.FormatString = "{0:dd/MM/yyyy HH:mm:ss}"
    '        End If
    '    Next

    '    grvResep.Columns("NoRegister").VisibleIndex = -1
    '    grvResep.Columns("NoKode").VisibleIndex = -1

    'End Sub
    Private Sub fn_LoadDataBillingTambahan(ByVal Parameter As String)
        Try
            If grdKDDEPARTMENT.Text = "" Then Exit Sub

            Dim oReq_BHP As New Transaksi.clsREQ_BHP

            Dim ds = From x In oReq_BHP.GetDataByRekamMedis(Parameter)
                     Select NoRegister = x.KDPENDAFTARAN, Tanggal = x.DATE, NoKode = x.KDREQBHP, Tujuan = x.M_DEPARTMENT.NAME_DISPLAY, DPJP = x.M_DOCTOR.NAME_DISPLAY

            grdBillingTambahan.DataSource = ds.ToList()

            fn_LoadFormatBillinTambahan()

            grvBillingTambahan.BestFitColumns()
        Catch oErr As Exception
            MsgBox("Load Billing Tambahan Data : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadFormatBillinTambahan()
        For iLoop As Integer = 0 To grvBillingTambahan.Columns.Count - 1
            If grvBillingTambahan.Columns(iLoop).ColumnType.Name = "Decimal" Then
                grvBillingTambahan.Columns(iLoop).DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                grvBillingTambahan.Columns(iLoop).DisplayFormat.FormatString = "{0:n2}"
                grvBillingTambahan.Columns(iLoop).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
                grvBillingTambahan.GroupSummary.Add(DevExpress.Data.SummaryItemType.Sum, grvBillingTambahan.Columns(iLoop).FieldName, grvBillingTambahan.Columns(iLoop),
                                     "{0:n2}")
                grvBillingTambahan.Columns(iLoop).SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                grvBillingTambahan.Columns(iLoop).SummaryItem.DisplayFormat = "{0:n2}"
            ElseIf grvBillingTambahan.Columns(iLoop).ColumnType.Name = "DateTime" Then
                grvBillingTambahan.Columns(iLoop).DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
                grvBillingTambahan.Columns(iLoop).DisplayFormat.FormatString = "{0:dd/MM/yyyy HH:mm:ss}"
            End If
        Next

        grvBillingTambahan.Columns("NoRegister").VisibleIndex = -1
        grvBillingTambahan.Columns("NoKode").VisibleIndex = -1
    End Sub
    Private Sub fn_LoadDataResume(ByVal Parameter As String)
        'Try
        '    If grdKDDEPARTMENT.Text = "" Then Exit Sub

        '    Dim oKoding As New Admission.clsKoding

        '    Dim ds = From x In oKoding.GetDataByRekamMedis(Parameter)
        '             Select NoRegister = x.KDPENDAFTARAN, NoKode = x.KDKODING, Tanggal = x.DATE, Tujuan = x.TUJUAN, DPJP = x.DOKTER

        '    grdResume.DataSource = ds.ToList()

        '    fn_LoadFormatResume()

        '    grvResume.BestFitColumns()
        'Catch oErr As Exception
        '    MsgBox("Load Resume Data : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        'End Try
    End Sub
    Private Sub fn_LoadFormatResume()
        'For iLoop As Integer = 0 To grvResume.Columns.Count - 1
        '    If grvResume.Columns(iLoop).ColumnType.Name = "Decimal" Then
        '        grvResume.Columns(iLoop).DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        '        grvResume.Columns(iLoop).DisplayFormat.FormatString = "{0:n2}"
        '        grvResume.Columns(iLoop).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        '        grvResume.GroupSummary.Add(DevExpress.Data.SummaryItemType.Sum, grvResume.Columns(iLoop).FieldName, grvResume.Columns(iLoop),
        '                             "{0:n2}")
        '        grvResume.Columns(iLoop).SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
        '        grvResume.Columns(iLoop).SummaryItem.DisplayFormat = "{0:n2}"
        '    ElseIf grvResume.Columns(iLoop).ColumnType.Name = "DateTime" Then
        '        grvResume.Columns(iLoop).DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        '        grvResume.Columns(iLoop).DisplayFormat.FormatString = "{0:dd/MM/yyyy HH:mm:ss}"
        '    End If
        'Next

        'grvResume.Columns("NoRegister").VisibleIndex = -1
        'grvResume.Columns("NoKode").VisibleIndex = -1
    End Sub
    Private Sub fn_LoadDataSKD(ByVal Parameter As String)
        'Try
        '    If grdKDDEPARTMENT.Text = "" Then Exit Sub

        '    Dim oReq_SKD As New Admission.clsSKD

        '    Dim ds = From x In oReq_SKD.GetDataByRMList(Parameter)
        '             Select NoRegister = x.KDPENDAFTARAN, NoKode = x.KDSKD, Tanggal = x.DATE, Tujuan = x.M_DEPARTMENT.NAME_DISPLAY, DPJP = x.M_DOCTOR.NAME_DISPLAY

        '    grdSKD.DataSource = ds.ToList()

        '    fn_LoadFormatSKD()

        '    grvSKD.BestFitColumns()
        'Catch oErr As Exception
        '    MsgBox("Load SKD Data : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        'End Try
    End Sub
    Private Sub fn_LoadFormatSKD()
        'For iLoop As Integer = 0 To grvSKD.Columns.Count - 1
        '    If grvSKD.Columns(iLoop).ColumnType.Name = "Decimal" Then
        '        grvSKD.Columns(iLoop).DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        '        grvSKD.Columns(iLoop).DisplayFormat.FormatString = "{0:n2}"
        '        grvSKD.Columns(iLoop).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        '        grvSKD.GroupSummary.Add(DevExpress.Data.SummaryItemType.Sum, grvSKD.Columns(iLoop).FieldName, grvSKD.Columns(iLoop),
        '                             "{0:n2}")
        '        grvSKD.Columns(iLoop).SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
        '        grvSKD.Columns(iLoop).SummaryItem.DisplayFormat = "{0:n2}"
        '    ElseIf grvSKD.Columns(iLoop).ColumnType.Name = "DateTime" Then
        '        grvSKD.Columns(iLoop).DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        '        grvSKD.Columns(iLoop).DisplayFormat.FormatString = "{0:dd/MM/yyyy HH:mm:ss}"
        '    End If
        'Next

        'grvSKD.Columns("NoRegister").VisibleIndex = -1
        'grvSKD.Columns("NoKode").VisibleIndex = -1
    End Sub
    Private Sub fn_LoadHystori(ByVal nopasien As String)
        Try
            grvHystori.OptionsSelection.MultiSelect = True
            grvHystori.SelectAll()
            grvHystori.DeleteSelectedRows()
            grvHystori.OptionsSelection.MultiSelect = False

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

            'SQL = "SELECT "
            'SQL &= "* "
            'SQL &= "FROM ("
            'SQL &= "SELECT "
            'SQL &= "Kategori = 'ERESEP' "
            'SQL &= ",NoRegister = A.KDPENDAFTARAN "
            'SQL &= ",NoTransaksi = A.KDREQRECIPE "
            'SQL &= ",Tanggal = A.DATE "
            'SQL &= ",Tujuan = A.TUJUAN "
            'SQL &= ",DPJP = A.DOKTER "
            'SQL &= "FROM "
            'SQL &= "S_REQ_RECIPE_H A "
            'SQL &= "WHERE A.KDCUSTOMER = '" & nopasien & "' "

            'SQL &= "UNION "

            'SQL &= "SELECT "
            'SQL &= "Kategori = 'BILLING TAMBAHAN' "
            'SQL &= ",NoRegister = A.KDPENDAFTARAN "
            'SQL &= ",NoTransaksi = A.KDREQBHP "
            'SQL &= ",Tanggal = A.DATE "
            'SQL &= ",Tujuan = B.NAME_DISPLAY "
            'SQL &= ",DPJP = D.NAME_DISPLAY "
            'SQL &= "FROM "
            'SQL &= "S_REQ_BHP_H A "
            'SQL &= "INNER JOIN M_DEPARTMENT B "
            'SQL &= "ON A.KDDEPARTMENT = B.KDDEPARTMENT "
            'SQL &= "INNER JOIN S_PENDAFTARAN_H C "
            'SQL &= "ON A.KDPENDAFTARAN = C.KDPENDAFTARAN "
            'SQL &= "INNER JOIN M_DOCTOR D "
            'SQL &= "ON C.KDDOCTOR = D.KDDOCTOR "
            'SQL &= "WHERE C.KDCUSTOMER = '" & nopasien & "' "

            'SQL &= "UNION "

            'SQL &= "SELECT "
            'SQL &= "Kategori = 'SURAT KONTROL DOKTER' "
            'SQL &= ",NoRegister = A.KDPENDAFTARAN "
            'SQL &= ",NoTransaksi = A.KDSKD "
            'SQL &= ",Tanggal = A.DATE "
            'SQL &= ",Tujuan = B.NAME_DISPLAY "
            'SQL &= ",DPJP = D.NAME_DISPLAY "
            'SQL &= "FROM "
            'SQL &= "S_PENDAFTARAN_SKD A "
            'SQL &= "INNER JOIN M_DEPARTMENT B "
            'SQL &= "ON A.KDDEPARTMENT = B.KDDEPARTMENT "
            'SQL &= "INNER JOIN M_DOCTOR D "
            'SQL &= "ON A.KDDOCTOR = D.KDDOCTOR "
            'SQL &= "WHERE A.KDCUSTOMER = '" & nopasien & "' "

            'SQL &= "UNION "

            'SQL &= "SELECT "
            'SQL &= "Kategori = 'RESUME RAWAT JALAN' "
            'SQL &= ",NoRegister = A.KDPENDAFTARAN "
            'SQL &= ",NoTransaksi = A.KDKODING "
            'SQL &= ",Tanggal = A.DATE "
            'SQL &= ",Tujuan = A.TUJUAN "
            'SQL &= ",DPJP = A.DOKTER "
            'SQL &= "FROM "
            'SQL &= "S_KODING_H A "
            'SQL &= "WHERE A.KDCUSTOMER = '" & nopasien & "' "

            'SQL &= ") Z "
            'SQL &= "ORDER BY Z.Tanggal DESC "

            SQL = "SELECT * FROM ( "
            SQL &= "SELECT "
            SQL &= "Kategori = CONVERT(VARCHAR(8), A.DATE, 103) + ', ' + A.TUJUAN + ', ' + A.DOKTER "
            SQL &= ",Tipe ='DIAGNOSA UTAMA' "
            SQL &= ",NoRegister = A.KDPENDAFTARAN "
            SQL &= ",NoTransaksi = A.KDREQRECIPE "
            SQL &= ",Deskripsi = A.DIAGNOSA "
            SQL &= ",PILIH = CONVERT(BIT, 0) "
            SQL &= ",KDITEM = '' "
            SQL &= ",KDUOM = '' "
            SQL &= "FROM "
            SQL &= "S_REQ_RECIPE_H A "
            SQL &= "WHERE A.KDCUSTOMER = '" & nopasien & "' "

            SQL &= "UNION "

            SQL &= "SELECT "
            SQL &= "Kategori = CONVERT(VARCHAR(8), A.DATE, 103) + ', ' + A.TUJUAN + ', ' + A.DOKTER "
            SQL &= ",Tipe ='XRESEP' "
            SQL &= ",NoRegister = A.KDPENDAFTARAN "
            SQL &= ",NoTransaksi = A.KDREQRECIPE "
            SQL &= ",Deskripsi = B.NAMAOBAT + ', ' + B.SATUAN + ', ' + CONVERT(NVARCHAR(5), B.QTY) + ', ' + B.REMARKS_DOKTER + ', ' + B.SIGNA + ', ' + B.CARAPAKAI "
            SQL &= ",PILIH = CONVERT(BIT, 0) "
            SQL &= ",B.KDITEM "
            SQL &= ",B.KDUOM "
            SQL &= "FROM "
            SQL &= "S_REQ_RECIPE_H A "
            SQL &= "INNER JOIN S_REQ_RECIPE_D B "
            SQL &= "ON A.KDREQRECIPE = B.KDREQRECIPE "
            SQL &= "WHERE A.KDCUSTOMER = '" & nopasien & "' "

            'SQL &= "UNION "

            'SQL &= "SELECT "
            'SQL &= "Kategori = CONVERT(VARCHAR(8), B.DATE, 103) + ', ' + E.NAME_DISPLAY + ', ' + F.NAME_DISPLAY "
            'SQL &= ",Tipe ='UPLOAD' "
            'SQL &= ",NoRegister = B.KDPENDAFTARAN "
            'SQL &= ",NoTransaksi = A.KDPDFTRANSAKSI "
            'SQL &= ",Deskripsi = C.ALAMATAKHIR_PDF "
            'SQL &= ",PILIH = CONVERT(BIT, 0) "
            'SQL &= ",KDITEM = '' "
            'SQL &= ",KDUOM = '' "
            'SQL &= "FROM "
            'SQL &= "S_PDF_H A "
            'SQL &= "INNER JOIN S_PENDAFTARAN_H B "
            'SQL &= "ON A.KDREG = B.KDREG "
            'SQL &= "INNER JOIN S_PDF_D C "
            'SQL &= "ON A.KDPDFTRANSAKSI = C.KDPDFTRANSAKSI "
            'SQL &= "INNER JOIN M_PDF D "
            'SQL &= "ON C.KDPDF = D.KDPDF "
            'SQL &= "INNER JOIN M_DEPARTMENT E "
            'SQL &= "ON B.KDDEPARTMENT = E.KDDEPARTMENT "
            'SQL &= "INNER JOIN M_DOCTOR F "
            'SQL &= "ON B.KDDOCTOR = F.KDDOCTOR "
            'SQL &= "WHERE B.KDCUSTOMER = '" & nopasien & "' "

            SQL &= ") Z "
            SQL &= "ORDER BY Z.Kategori DESC "

            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "S_HISTORY")

            'grdHystori.DataSource = ds.Tables("S_HISTORY")
            'grdHystori.ForceInitialize()

            BindingSource.DataSource = ds.Tables("S_HISTORY")

            grdHystori.DataSource = BindingSource
            grdHystori.ForceInitialize()

            If oConn.State = ConnectionState.Open Then
                oConn.Close()
            End If

            fn_SetFormatHystori()

            grvHystori.BestFitColumns()

        Catch oErr As Exception
            MsgBox("Load History Data : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadDataUpload()
        Try
            grvUpload.OptionsSelection.MultiSelect = True
            grvUpload.SelectAll()
            grvUpload.DeleteSelectedRows()
            grvUpload.OptionsSelection.MultiSelect = False

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
            SQL &= "Kategori = CONVERT(VARCHAR(8), B.DATE, 103) + ', ' + E.NAME_DISPLAY + ', ' + F.NAME_DISPLAY "
            SQL &= ",NamaUpload = D.DESCRIPTION "
            SQL &= ",Catatan = C.MEMO "
            SQL &= ",AlamatUpload = C.ALAMATAKHIR_PDF "
            SQL &= ",Type = C.TYPE "
            SQL &= "FROM "
            SQL &= "S_PDF_H A "
            SQL &= "INNER JOIN S_PENDAFTARAN_H B "
            SQL &= "ON A.KDREG = B.KDREG "
            SQL &= "INNER JOIN S_PDF_D C "
            SQL &= "ON A.KDPDFTRANSAKSI = C.KDPDFTRANSAKSI "
            SQL &= "INNER JOIN M_PDF D "
            SQL &= "ON C.KDPDF = D.KDPDF "
            SQL &= "INNER JOIN M_DEPARTMENT E "
            SQL &= "ON B.KDDEPARTMENT = E.KDDEPARTMENT "
            SQL &= "INNER JOIN M_DOCTOR F "
            SQL &= "ON B.KDDOCTOR = F.KDDOCTOR "
            SQL &= "WHERE B.KDCUSTOMER = '" & lblRM.Text & "' "

            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "S_upload")

            grdUpload.DataSource = ds.Tables("S_upload")
            grdUpload.ForceInitialize()

            If oConn.State = ConnectionState.Open Then
                oConn.Close()
            End If

            fn_LoadFormatDataUpload()

            grvUpload.Columns("Kategori").Group()
            'grvUpload.Columns("NamaUpload").Group()
            grvUpload.ExpandAllGroups()
            grvUpload.Columns("Kategori").VisibleIndex = -1
            grvUpload.Columns("AlamatUpload").VisibleIndex = -1
            grvUpload.Columns("Type").VisibleIndex = -1

            grvUpload.BestFitColumns()

        Catch oErr As Exception
            MsgBox("Load List Pasien Data : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadFormatDataUpload()
        For iLoop As Integer = 0 To grvUpload.Columns.Count - 1
            If grvUpload.Columns(iLoop).ColumnType.Name = "Decimal" Then
                grvUpload.Columns(iLoop).DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                grvUpload.Columns(iLoop).DisplayFormat.FormatString = "{0:n2}"
                grvUpload.Columns(iLoop).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
            ElseIf grvUpload.Columns(iLoop).ColumnType.Name = "DateTime" Then
                grvUpload.Columns(iLoop).DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
                grvUpload.Columns(iLoop).DisplayFormat.FormatString = "{0:dd/MM/yyyy}"
            End If
        Next

    End Sub
    Private Sub grvUpload_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles grvUpload.DoubleClick
        If grvUpload.GetFocusedRowCellValue("AlamatUpload") Is Nothing Then
            Exit Sub
        End If

        If lblRM.Text = "" Then
            Exit Sub
        End If

        'frmBrowseUpload.fn_LoadMe(grvUpload.GetFocusedRowCellValue("Type"), grvUpload.GetFocusedRowCellValue("AlamatUpload"), lblRM.Text, lblNamaPasien.Text, sTANGGALLAHIR.ToString("dd-MM-yyyy"), IIf(sJK = 1, "Laki-laki", "Perempuan"))
        'frmBrowseUpload.Show()
        'frmBrowseUpload.BringToFront()
        'frmBrowseUpload.WindowState = FormWindowState.Normal
    End Sub
    Private Sub fn_SetFormatHystori()
        For iLoop As Integer = 0 To grvHystori.Columns.Count - 1
            If grvHystori.Columns(iLoop).ColumnType.Name = "Decimal" Then
                grvHystori.Columns(iLoop).DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                grvHystori.Columns(iLoop).DisplayFormat.FormatString = "{0:n2}"
                grvHystori.Columns(iLoop).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
                'grvHystori.GroupSummary.Add(DevExpress.Data.SummaryItemType.Sum, grvHystori.Columns(iLoop).FieldName, grvHystori.Columns(iLoop),
                '                     "{0:n2}")
                'grvHystori.Columns(iLoop).SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                'grvHystori.Columns(iLoop).SummaryItem.DisplayFormat = "{0:n2}"
            ElseIf grvHystori.Columns(iLoop).ColumnType.Name = "DateTime" Then
                grvHystori.Columns(iLoop).DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
                grvHystori.Columns(iLoop).DisplayFormat.FormatString = "{0:dd/MM/yyyy HH:mm:ss}"
            End If
        Next


        grvHystori.Columns("NoRegister").VisibleIndex = -1
        grvHystori.Columns("NoTransaksi").VisibleIndex = -1
        grvHystori.Columns("KDITEM").VisibleIndex = -1
        grvHystori.Columns("KDUOM").VisibleIndex = -1
        'grvHystori.Columns("SATUAN").VisibleIndex = -1
        'grvHystori.Columns("JUMLAH").VisibleIndex = -1
        'grvHystori.Columns("CARAPAKAI").VisibleIndex = -1
        'grvHystori.Columns("KET1").VisibleIndex = -1
        'grvHystori.Columns("KET2").VisibleIndex = -1

        grvHystori.Columns("Kategori").Group()
        grvHystori.Columns("Tipe").Group()
        grvHystori.ExpandAllGroups()
    End Sub
    Private Function fn_SaveCustomer_Detil() As Boolean
        Try
            fn_SaveCustomer_Detil = True

            Dim oCustomer_Detil As New Reference.clsCustomerDetil
            ' ***** HEADER *****
            Dim ds = oCustomer_Detil.GetStructureHeader
            With ds
                Try
                    .DATECREATED = oCustomer_Detil.GetData(lblRegister.Text).DATECREATED
                Catch ex As Exception
                    .DATECREATED = Now
                End Try
                .DATEUPDATED = Now
                .KDCUSTOMER = lblRM.Text
                .BB = txtBERATBADAN.Text
                .TT = txtTB.Text
                .ALERGI = txtALERGIOBAT.Text
                .DIAGNOSA = txtDIAGNOSAUTAMA.Text
            End With

            Dim dsCsutomer_Detil = oCustomer_Detil.GetData(lblRM.Text)

            If dsCsutomer_Detil Is Nothing Then
                oCustomer_Detil.InsertData(ds)
            Else
                oCustomer_Detil.UpdateData(ds)
            End If
        Catch oErr As Exception
            MsgBox("Simpan Data : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            fn_SaveCustomer_Detil = False
        End Try
    End Function
#End Region
#Region "Command Button"
    Private Sub fn_PrintStrukResep(ByVal NoTransaksi As String, ByVal isPDF As Boolean)
        Try
            Dim listPendaftaran As New List(Of DataAccess.R_RESEP)

            Dim oConn_1 As New SqlConnection
            Dim oComm_1 As New SqlCommand
            Dim da_1 As SqlDataAdapter
            Dim ds_1 As New DataSet
            Dim SQL_1 As String

            oConn_1 = New SqlConnection(Decrypt(My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Software\SIMRS\SW\", "Database", "").ToString()))

            If oConn_1.State = ConnectionState.Closed Then
                oConn_1.Open()
            End If

            SQL_1 = "SELECT "
            SQL_1 &= "* "
            SQL_1 &= "FROM S_REQ_RECIPE_H A "
            SQL_1 &= "WHERE A.KDREQRECIPE ='" & NoTransaksi & "' "

            oComm_1.Connection = oConn_1
            oComm_1.CommandText = SQL_1
            oComm_1.CommandTimeout = 120
            oComm_1.CommandType = CommandType.Text

            da_1 = New SqlDataAdapter(oComm_1)
            da_1.Fill(ds_1, "S_REQ_RECIPE_H")

            If oConn_1.State = ConnectionState.Open Then
                oConn_1.Close()
            End If

            For iLoop As Integer = 0 To ds_1.Tables("S_REQ_RECIPE_H").Rows.Count - 1
                Dim dsRekap As New DataAccess.R_RESEP
                With ds_1.Tables("S_REQ_RECIPE_H")
                    dsRekap.KDREQRECIPE = .Rows(iLoop)("KDREQRECIPE")
                    dsRekap.POLI = .Rows(iLoop)("TUJUAN")
                    dsRekap.DOKTER = .Rows(iLoop)("DOKTER")
                    dsRekap.SIP = .Rows(iLoop)("SIPDOKTER")
                    dsRekap.TANGGAL = .Rows(iLoop)("DATE")
                    dsRekap.BERATBADAN = .Rows(iLoop)("BERATBADAN")
                    dsRekap.RIWAYAT_YA = ""
                    dsRekap.RIWAYAT_TIDAK = ""
                    dsRekap.ALERGIOBAT = .Rows(iLoop)("ALERGIOBAT")

                    Dim oConn_2 As New SqlConnection
                    Dim oComm_2 As New SqlCommand
                    Dim da_2 As SqlDataAdapter
                    Dim ds_2 As New DataSet
                    Dim SQL_2 As String

                    oConn_2 = New SqlConnection(Decrypt(My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Software\SIMRS\SW\", "Database", "").ToString()))

                    If oConn_2.State = ConnectionState.Closed Then
                        oConn_2.Open()
                    End If

                    SQL_2 = "SELECT "
                    SQL_2 &= "* "
                    SQL_2 &= "FROM "
                    SQL_2 &= "S_REQ_RECIPE_D A  "
                    SQL_2 &= "WHERE A.KDREQRECIPE ='" & .Rows(iLoop)("KDREQRECIPE") & "' "

                    oComm_2.Connection = oConn_2
                    oComm_2.CommandText = SQL_2
                    oComm_2.CommandTimeout = 120
                    oComm_2.CommandType = CommandType.Text

                    da_2 = New SqlDataAdapter(oComm_2)
                    da_2.Fill(ds_2, "S_REQ_RECIPE_D")

                    If oConn_2.State = ConnectionState.Open Then
                        oConn_2.Close()
                    End If

                    Dim list As New List(Of String)

                    For xLoop As Integer = 0 To ds_2.Tables("S_REQ_RECIPE_D").Rows.Count - 1
                        With ds_2.Tables("S_REQ_RECIPE_D")
                            list.Add("R/ " & .Rows(xLoop)("NAMAOBAT") & " No " & IntegerToRoman(.Rows(xLoop)("QTY")) & vbCrLf & "ʃ " & .Rows(xLoop)("SIGNA") & " " & .Rows(xLoop)("CARAPAKAI") & " " & .Rows(xLoop)("REMARKS_DOKTER"))
                        End With
                    Next


                    dsRekap.DESCRIPTION = String.Join(vbCrLf & "-----------------------------------" & vbCrLf, list.ToArray) & vbCr & "-----------------------------------"

                    dsRekap.NAMAPASIEN = .Rows(iLoop)("PASIEN")
                    dsRekap.RM = .Rows(iLoop)("KDCUSTOMER")
                    dsRekap.TANGGALLAHIR = sTANGGALLAHIR
                    dsRekap.ALAMAT = .Rows(iLoop)("ALAMAT")

                    Dim oConn_3 As New SqlConnection
                    Dim oComm_3 As New SqlCommand
                    Dim da_3 As SqlDataAdapter
                    Dim ds_3 As New DataSet
                    Dim SQL_3 As String

                    oConn_3 = New SqlConnection(Decrypt(My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Software\SIMRS\SW\", "Database", "").ToString()))

                    If oConn_3.State = ConnectionState.Closed Then
                        oConn_3.Open()
                    End If

                    SQL_3 = "SELECT "
                    SQL_3 &= "* "
                    SQL_3 &= "FROM "
                    SQL_3 &= "S_REQ_RECIPE_TELAAHRESEP A  "
                    SQL_3 &= "WHERE A.KDREQRECIPE ='" & .Rows(iLoop)("KDREQRECIPE") & "' "

                    oComm_3.Connection = oConn_3
                    oComm_3.CommandText = SQL_3
                    oComm_3.CommandTimeout = 120
                    oComm_3.CommandType = CommandType.Text

                    da_3 = New SqlDataAdapter(oComm_3)
                    da_3.Fill(ds_3, "S_REQ_RECIPE_TELAAHRESEP")

                    If oConn_3.State = ConnectionState.Open Then
                        oConn_3.Close()
                    End If

                    For xLoop As Integer = 0 To ds_3.Tables("S_REQ_RECIPE_TELAAHRESEP").Rows.Count - 1
                        With ds_3.Tables("S_REQ_RECIPE_TELAAHRESEP")
                            dsRekap.TELAAH_01_01_01 = .Rows(xLoop)("TELAAH_01")
                            dsRekap.TELAAH_01_01_02 = .Rows(xLoop)("TELAAH_02")
                            dsRekap.TELAAH_01_01_03 = .Rows(xLoop)("TELAAH_03")
                            dsRekap.TELAAH_01_01_04 = .Rows(xLoop)("TELAAH_04")
                            dsRekap.TELAAH_01_01_05 = .Rows(xLoop)("TELAAH_05")
                            dsRekap.TELAAH_01_01_06 = .Rows(xLoop)("TELAAH_06")
                            dsRekap.TELAAH_01_01_07 = .Rows(xLoop)("TELAAH_07")
                            dsRekap.TELAAH_01_01_08 = .Rows(xLoop)("TELAAH_08")
                            dsRekap.TELAAH_01_01_09 = .Rows(xLoop)("TELAAH_09")
                            dsRekap.TELAAH_01_01_10 = .Rows(xLoop)("TELAAH_10")
                            dsRekap.TELAAH_01_01_11 = .Rows(xLoop)("TELAAH_11")
                            dsRekap.TELAAH_01_01_12 = .Rows(xLoop)("TELAAH_12")
                        End With
                    Next

                    Dim oConn_4 As New SqlConnection
                    Dim oComm_4 As New SqlCommand
                    Dim da_4 As SqlDataAdapter
                    Dim ds_4 As New DataSet
                    Dim SQL_4 As String

                    oConn_4 = New SqlConnection(Decrypt(My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Software\SIMRS\SW\", "Database", "").ToString()))

                    If oConn_4.State = ConnectionState.Closed Then
                        oConn_4.Open()
                    End If

                    SQL_4 = "SELECT "
                    SQL_4 &= "* "
                    SQL_4 &= "FROM "
                    SQL_4 &= "S_REQ_RECIPE_TELAAHOBAT1 A  "
                    SQL_4 &= "WHERE A.KDREQRECIPE ='" & .Rows(iLoop)("KDREQRECIPE") & "' "

                    oComm_4.Connection = oConn_4
                    oComm_4.CommandText = SQL_4
                    oComm_4.CommandTimeout = 120
                    oComm_4.CommandType = CommandType.Text

                    da_4 = New SqlDataAdapter(oComm_4)
                    da_4.Fill(ds_4, "S_REQ_RECIPE_TELAAHOBAT1")

                    If oConn_4.State = ConnectionState.Open Then
                        oConn_4.Close()
                    End If

                    For xLoop As Integer = 0 To ds_4.Tables("S_REQ_RECIPE_TELAAHOBAT1").Rows.Count - 1
                        With ds_4.Tables("S_REQ_RECIPE_TELAAHOBAT1")
                            dsRekap.TELAAH_02_01_01 = .Rows(xLoop)("TELAAH_01")
                            dsRekap.TELAAH_02_01_02 = .Rows(xLoop)("TELAAH_02")
                            dsRekap.TELAAH_02_01_03 = .Rows(xLoop)("TELAAH_03")
                            dsRekap.TELAAH_02_01_04 = .Rows(xLoop)("TELAAH_04")
                            dsRekap.TELAAH_02_01_05 = .Rows(xLoop)("TELAAH_05")
                        End With
                    Next

                    Dim oConn_5 As New SqlConnection
                    Dim oComm_5 As New SqlCommand
                    Dim da_5 As SqlDataAdapter
                    Dim ds_5 As New DataSet
                    Dim SQL_5 As String

                    oConn_5 = New SqlConnection(Decrypt(My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Software\SIMRS\SW\", "Database", "").ToString()))

                    If oConn_5.State = ConnectionState.Closed Then
                        oConn_5.Open()
                    End If

                    SQL_5 = "SELECT "
                    SQL_5 &= "* "
                    SQL_5 &= "FROM "
                    SQL_5 &= "S_REQ_RECIPE_TELAAHOBAT2 A  "
                    SQL_5 &= "WHERE A.KDREQRECIPE ='" & .Rows(iLoop)("KDREQRECIPE") & "' "

                    oComm_5.Connection = oConn_5
                    oComm_5.CommandText = SQL_5
                    oComm_5.CommandTimeout = 120
                    oComm_5.CommandType = CommandType.Text

                    da_5 = New SqlDataAdapter(oComm_5)
                    da_5.Fill(ds_5, "S_REQ_RECIPE_TELAAHOBAT2")

                    If oConn_5.State = ConnectionState.Open Then
                        oConn_5.Close()
                    End If

                    For xLoop As Integer = 0 To ds_5.Tables("S_REQ_RECIPE_TELAAHOBAT2").Rows.Count - 1
                        With ds_5.Tables("S_REQ_RECIPE_TELAAHOBAT2")
                            dsRekap.TELAAH_03_01_01 = .Rows(xLoop)("TELAAH_01")
                            dsRekap.TELAAH_03_01_02 = .Rows(xLoop)("TELAAH_02")
                            dsRekap.TELAAH_03_01_03 = .Rows(xLoop)("TELAAH_03")
                            dsRekap.TELAAH_03_01_04 = .Rows(xLoop)("TELAAH_04")
                            dsRekap.TELAAH_03_01_05 = .Rows(xLoop)("TELAAH_05")
                        End With
                    Next

                    listPendaftaran.Add(dsRekap)
                End With
            Next

            Dim oKoding As New Transaksi.clsReq_Recipe
            Dim dsKoding = oKoding.GetDataKoding(NoTransaksi)
            If dsKoding IsNot Nothing Then
                sUserIDTandaTangan = dsKoding.KDDOCTOR
            End If

            Dim rpt As New xtraEResepNonRacikanNew

            If listPendaftaran.Count > 0 Then
                rpt.BindingSource1.DataSource = listPendaftaran

                If isPDF = True Then
                    rpt.ExportToPdf("C:/farmasi/resep/A.pdf")
                    PdfViewerResep.LoadDocument("C:/farmasi/resep/A.pdf")
                Else
                    Dim printTool As New DevExpress.XtraReports.UI.ReportPrintTool(rpt)
                    printTool.ShowPreviewDialog(DevExpress.LookAndFeel.UserLookAndFeel.Default)
                End If

            End If
        Catch oErr As Exception
            'MsgBox("Print Data : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_PrintStrukResumeRawatJalan(ByVal NoTransaksi As String, ByVal isPDF As Boolean)
        Try
            Dim oKoding As New Transaksi.clsReq_Recipe

            Dim dsKoding = oKoding.GetDataKoding(NoTransaksi)
            If dsKoding IsNot Nothing Then
                Dim rpt As New xtraKoding

                rpt.bindingSource.DataSource = dsKoding

                DownloadIamge1 = AlamatDownloadIamge1 & lblRM.Text & "/" & lblRM.Text & ".png"

                'KDDOCTOR = dsKoding.S_PENDAFTARAN_H.KDDOCTOR
                sUserIDTandaTangan = dsKoding.KDDOCTOR

                If isPDF = True Then
                    rpt.bindingSource.DataSource = dsKoding
                    rpt.ExportToPdf("C:/farmasi/resep/B.pdf")
                    PdfViewerResume.LoadDocument("C:/farmasi/resep/B.pdf")
                Else
                    Dim printTool As New DevExpress.XtraReports.UI.ReportPrintTool(rpt)
                    printTool.ShowPreviewDialog(DevExpress.LookAndFeel.UserLookAndFeel.Default)
                End If
            End If
        Catch oErr As Exception
            'MsgBox("Print Data : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
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
    Private Function fn_DeleteResep(ByVal NoKode As String) As Boolean
        Try
            Dim oReq_Recipe As New Transaksi.clsReq_Recipe
            oReq_Recipe.DeleteData(NoKode)
            fn_DeleteResep = True
        Catch oErr As Exception
            fn_DeleteResep = False
            MsgBox("Hapus Data Resep : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Function
    Private Sub grvBillingTambahan_FocusedRowChanged(ByVal sender As System.Object, ByVal e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs) Handles grvBillingTambahan.FocusedRowChanged
        If grvBillingTambahan.GetFocusedRowCellValue("NoKode") Is Nothing Then
            Exit Sub
        End If

        If Not IO.Directory.Exists("C:/Farmasi/Resep") Then
            IO.Directory.CreateDirectory("C:/Farmasi/Resep")
        Else
            PdfViewerBillingTambahan.CloseDocument()
        End If

        'fn_PrintStrukbhp(grvBillingTambahan.GetFocusedRowCellValue("NoKode"), True, False)
    End Sub
    Private Sub grvBillingTambahan_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles grvBillingTambahan.DoubleClick
        'If grvBillingTambahan.GetFocusedRowCellValue("NoKode") Is Nothing Then
        '    Exit Sub
        'End If

        'Dim frmReq_BHP As New frmReq_BHP
        'Try
        '    PdfViewerBillingTambahan.CloseDocument()
        '    frmReq_BHP.LoadMe(FORM_MODE.FORM_MODE_VIEW, lblRegister.Text, grvBillingTambahan.GetFocusedRowCellValue("NoKode"))
        '    frmReq_BHP.ShowDialog(Me)
        'Catch ex As Exception
        '    MsgBox("Load Form Detail : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
        'End Try
    End Sub
    Private Sub grvHystori_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles grvHystori.DoubleClick

    End Sub
    Private Sub picAddBillingTambahan_Click() Handles picAddBillingTambahan.Click
        If lblRegister.Text <> "-" Then
            'Dim oPendaftaran As New Admission.clsPendaftaran
            'Dim dsPendaftaran = oPendaftaran.GetData(lblRegister.Text)
            'If dsPendaftaran IsNot Nothing Then
            '    fn_HidenOrder(False)
            'End If
            fn_HidenOrder(False)
        Else
            MsgBox("Register Kosong", MsgBoxStyle.Exclamation, Me.Text)
        End If
    End Sub
    Private Sub picPrintBillingTambahan_Click() Handles picPrintBillingTambahan.Click
        Try
            If grvBillingTambahan.GetFocusedRowCellValue("NoKode") Is Nothing Then
                Exit Sub
            End If

            'fn_PrintStrukbhp(grvBillingTambahan.GetFocusedRowCellValue("NoKode"), False, False)
        Catch oErr As Exception
            MsgBox("Print Data : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub picDeleteBillingTambahan_Click() Handles picDeleteBillingTambahan.Click
        If grvBillingTambahan.GetFocusedRowCellValue("NoKode") Is Nothing Then
            Exit Sub
        End If

        Dim oReq_BHP As New Transaksi.clsREQ_BHP
        Dim dsReq = oReq_BHP.GetData(grvBillingTambahan.GetFocusedRowCellValue("NoKode"))

        If dsReq IsNot Nothing Then
            If MsgBox("Delete " & grvBillingTambahan.GetFocusedRowCellValue("NoKode") & "?", MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, Me.Text) = MsgBoxResult.No Then Exit Sub
            If fn_DeleteBillingTambahan(grvBillingTambahan.GetFocusedRowCellValue("NoKode")) = False Then
                MsgBox("Hapus gagal! tolong kontak admin anda!", MsgBoxStyle.Exclamation, Me.Text)
                Exit Sub
            End If
            MsgBox("Delete " & grvBillingTambahan.GetFocusedRowCellValue("NoKode") & " success!", MsgBoxStyle.Information, Me.Text)
            fn_LoadSecurity()
        End If
    End Sub
    Private Sub picSalinBillingTambahan_Resep_Click() Handles picSalinBillingTambahan.Click
        Dim oReq_BHP As New Transaksi.clsREQ_BHP

        If grvBillingTambahan.GetFocusedRowCellValue("NoKode") Is Nothing Then
            Exit Sub
        End If

        If lblRegister.Text <> "-" Then
            fn_HidenOrder(False)

            'Dim oPendaftaran As New Admission.clsPendaftaran
            'Dim dsPendaftaran = oPendaftaran.GetData(lblRegister.Text)
            'If dsPendaftaran IsNot Nothing Then
            '    fn_HidenOrder(False)
            'End If

            'BindingSourceOrder.DataSource = oReq_BHP.GetDataDetail(grvBillingTambahan.GetFocusedRowCellValue("NoKode")).OrderBy(Function(x) x.SEQ).ToList()
            'grdBillingTamabahanT.DataSource = BindingSourceOrder
        End If
    End Sub
    Private Sub picRefreshBillingTambahan_Click() Handles picRefreshBillingTambahan.Click
        fn_LoadSecurity()
    End Sub
    Private Function fn_DeleteBillingTambahan(ByVal NoKode As String) As Boolean
        Try
            Dim oReq_BHP As New Transaksi.clsREQ_BHP
            oReq_BHP.DeleteData(NoKode)
            fn_DeleteBillingTambahan = True
        Catch oErr As Exception
            fn_DeleteBillingTambahan = False
            MsgBox("Hapus Data Billing Tambahan : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Function
    Private Function fn_DeleteResume(ByVal NoKode As String) As Boolean
        Try
            Dim oResume As New Admission.clsKoding
            oResume.DeleteData(NoKode)
            fn_DeleteResume = True
        Catch oErr As Exception
            fn_DeleteResume = False
            MsgBox("Hapus Data Billing Tambahan : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Function
    Private Function fn_DeleteSKD(ByVal NoKode As String) As Boolean
        Try
            Dim oSKD As New Admission.clsSKD
            oSKD.DeleteData(NoKode)
            fn_DeleteSKD = True
        Catch oErr As Exception
            fn_DeleteSKD = False
            MsgBox("Hapus Data Billing Tambahan : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Function
#End Region
#Region "Lookup / Event"
    Private Sub fn_LoadDEPARTMENT()
        Dim oDepartment As New Reference.clsDepartment
        Try
            grdKDDEPARTMENT.Properties.DataSource = oDepartment.GetData.Where(Function(x) x.ISACTIVE = True And x.KDDEPARTMENT_BPJS <> "").ToList()
            grdKDDEPARTMENT.Properties.ValueMember = "KDDEPARTMENT"
            grdKDDEPARTMENT.Properties.DisplayMember = "NAME_DISPLAY"
        Catch oErr As Exception
            MsgBox("Load Poli Data : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub tabControl_SelectedPageChanged() Handles tabControl.SelectedPageChanged
        If lblRegister.Text = "-" Then
            MsgBox("Register Kosong", MsgBoxStyle.Exclamation, Me.Text)

            tabControl.SelectedTabPageIndex = 0

            Exit Sub
        End If

        If tabControl.SelectedTabPageIndex = 0 Then
            sClearList = True
            fn_HidenOrder(True)

            If grdKDDEPARTMENT.Text <> "" Then
                fn_LoadData()
            End If
        ElseIf tabControl.SelectedTabPageIndex = 1 Then
            If sClearList = True Then
                sClearList = False
            End If
            If lblRM.Text <> "-" Then
                fn_LoadDataResep(lblRM.Text)
            End If
        ElseIf tabControl.SelectedTabPageIndex = 2 Then
            If sClearList = True Then
                fn_HidenOrder(True)
                sClearList = False
            End If
            If lblRM.Text <> "-" Then
                fn_LoadDataBillingTambahan(lblRM.Text)
            End If
        ElseIf tabControl.SelectedTabPageIndex = 3 Then
            If sClearList = True Then
                sClearList = False
            End If
            If lblRM.Text <> "-" Then
                fn_LoadDataSKD(lblRM.Text)
            End If
        ElseIf tabControl.SelectedTabPageIndex = 4 Then
            If sClearList = True Then
                sClearList = False
            End If
            If lblRM.Text <> "-" Then
                fn_LoadDataResume(lblRM.Text)
            End If
        End If
    End Sub
    Private Sub XtraTabControl1_SelectedPageChanged() Handles XtraTabControl1.SelectedPageChanged
        If lblRegister.Text = "-" Then
            MsgBox("Register Kosong", MsgBoxStyle.Exclamation, Me.Text)

            tabControl.SelectedTabPageIndex = 0

            Exit Sub
        End If
        PdfViewerCPPTRawatJalan.CloseDocument()
        PdfViewerResume.CloseDocument()

        If XtraTabControl1.SelectedTabPageIndex = 0 Then
            sClearList = True
            fn_HidenOrder(True)

            If grdKDDEPARTMENT.Text <> "" Then
                fn_LoadData()
            End If
        ElseIf XtraTabControl1.SelectedTabPageIndex = 1 Then
            If sClearList = True Then
                sClearList = False
            End If
            If grvHystori.GetFocusedRowCellValue("NoRegister") IsNot Nothing Then
                Dim oReq_Recipe As New Transaksi.clsReq_Recipe
                Dim dsReq = oReq_Recipe.GetDatakdreg(grvHystori.GetFocusedRowCellValue("NoRegister"))
                If dsReq IsNot Nothing Then
                    fn_PrintStrukResep(dsReq.KDREQRECIPE, True)
                End If
            Else
                MsgBox("Load Resep : " & vbCrLf & "Silahkan Pilih Register", MsgBoxStyle.Exclamation, Me.Text)
                XtraTabControl1.SelectedTabPageIndex = 0
            End If
            'If lblRegister.Text <> "" Then
            '    Dim oReq_Recipe As New Transaksi.clsReq_Recipe
            '    Dim dsReq = oReq_Recipe.GetDatakdreg(lblRegister.Text)
            '    If dsReq IsNot Nothing Then
            '        fn_PrintStrukResep(dsReq.KDREQRECIPE, True)
            '    End If
            'End If
        ElseIf XtraTabControl1.SelectedTabPageIndex = 2 Then
            If sClearList = True Then
                sClearList = False
            End If
            If lblRM.Text <> "-" Then
                fn_LoadDataSKD(lblRM.Text)
            End If
        ElseIf XtraTabControl1.SelectedTabPageIndex = 3 Then
            If sClearList = True Then
                sClearList = False
            End If

            Dim oKoding As New Admission.clsKoding
            Dim dsReq = oKoding.GetDataByKDPENDAFTARAN(lblRegister.Text)
            If dsReq IsNot Nothing Then
                fn_PrintStrukResumeRawatJalan(dsReq.KDKODING, True)
            End If
            'If lblRegister.Text <> "" Then
            '    Dim oKoding As New Admission.clsKoding
            '    Dim dsReq = oKoding.GetDataByKDPENDAFTARAN(lblRegister.Text)
            '    If dsReq IsNot Nothing Then
            '        fn_PrintStrukResumeRawatJalan(dsReq.KDKODING, True)
            '    End If
            'End If
        ElseIf XtraTabControl1.SelectedTabPageIndex = 4 Then
            Try
                If lblRM.Text = String.Empty Then Exit Sub
                Dim oReqCPPT As New Transaksi.clsReqCPPT

                Dim rpt As New xtraDigital_CPPT_01

                rpt.ShowPrintMarginsWarning = False
                rpt.Watermark.Text = sWATERMARK
                Dim ds = oReqCPPT.GetDataByRM(lblRM.Text)
                rpt.bindingSource.DataSource = ds

                If Not IO.Directory.Exists("C:/SIMRS/CPPT") Then
                    IO.Directory.CreateDirectory("C:/SIMRS/CPPT")
                End If

                rpt.ExportToPdf("C:/SIMRS/CPPT/" & lblRM.Text & ".pdf")
                PdfViewerCPPTRawatJalan.LoadDocument("C:/SIMRS/CPPT/" & lblRM.Text & ".pdf")

                'Dim printTool As New DevExpress.XtraReports.UI.ReportPrintTool(rpt)
                'printTool.ShowPreviewDialog(DevExpress.LookAndFeel.UserLookAndFeel.Default)
            Catch oErr As Exception
                MsgBox("Cetak CPPT" & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            End Try
        ElseIf XtraTabControl1.SelectedTabPageIndex = 5 Then
            fn_LoadDataUpload()
        End If
    End Sub
    Private Sub picRefreshListPasien_Click() Handles picRefreshListPasien.Click
        If grdKDDEPARTMENT.Text <> "" Then
            fn_LoadData()
        End If
    End Sub
#End Region
#Region "Form"
    Private Sub fn_LoadDokter(ByVal Paramater As String)
        Try
            If grdKDDEPARTMENT.Text = String.Empty Then Exit Sub

            Dim dsDoctor = From x In oDoctor.GetData
                           Where x.ISACTIVE = True And x.KDDEPARTMENT = grdKDDEPARTMENT.EditValue And x.VCLAIM_KDDPJP <> ""
                           Select x.KDDOCTOR, x.NAME_DISPLAY

            grdDPJP.Properties.DataSource = dsDoctor.ToList()
            grdDPJP.Properties.ValueMember = "KDDOCTOR"
            grdDPJP.Properties.DisplayMember = "NAME_DISPLAY"

            grdDPJP.Text = Paramater
        Catch oErr As Exception
            MsgBox("Load Dokter Data : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
#Region "Order"
    Private Sub grvBillingTamabahanT_CellValueChanged(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs) Handles grvBillingTamabahanT.CellValueChanged
        If e.Column.Name = colRINCIAN.Name Then
            If sLoadOrder = True Then Exit Sub
            If grvBillingTamabahanT.GetFocusedRowCellValue(colRINCIAN) IsNot Nothing Then
                grvBillingTamabahanT.SetFocusedRowCellValue(colQTYORDER, 1)
                grvBillingTamabahanT.SetFocusedRowCellValue(colDATEPEMERIKSAAN, Now)
                grvBillingTamabahanT.SetFocusedRowCellValue(colKETERANGANORDER, "")
                grvBillingTamabahanT.SetFocusedRowCellValue(colKETERANGAN_PENUNJANG, "LAIN")
            End If
        End If
    End Sub
    Private Sub fn_HidenOrder(ByVal Hidden As Boolean)
        grvBillingTamabahanT.OptionsSelection.MultiSelect = True
        grvBillingTamabahanT.SelectAll()
        grvBillingTamabahanT.DeleteSelectedRows()
        grvBillingTamabahanT.OptionsSelection.MultiSelect = False

        grvBillingTamabahanT.Columns("KETERANGAN_PENUNJANG").Group()
        grvBillingTamabahanT.ExpandAllGroups()

        If Hidden = False Then
            lBillingTambahan.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            lSIMPANORDER.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            lADDPENUNJANG.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            lEMPRTY.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always

            lListOrder.Height = 100
        Else
            lBillingTambahan.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            lSIMPANORDER.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            lADDPENUNJANG.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            lEMPRTY.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never

            lListOrder.Height = 320
        End If
    End Sub
    Private Sub btnSimpanOrder_Click(sender As Object, e As EventArgs) Handles btnSimpanOrder.Click
        If grvBillingTamabahanT.RowCount < 2 Then
            MsgBox("Dibutuhkan Minimal Satu Diagnosa Utama", MsgBoxStyle.Exclamation, Me.Text)
            Exit Sub
        End If
        If MsgBox("Save Order Register " & lblRegister.Text.Trim.ToUpper & "?", MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, Me.Text) = MsgBoxResult.No Then Exit Sub
        fn_SaveOrder("")
        fn_LoadSecurity()
        fn_HidenOrder(True)
    End Sub
    Private Function fn_SaveOrder(ByVal sNoid As String) As Boolean
        Try
            ' ***** HEADER *****
            Dim oReq_BHP As New Transaksi.clsREQ_BHP
            Dim ds = oReq_BHP.GetStructureHeader
            With ds
                Try
                    .DATECREATED = oReq_BHP.GetData(sNoid).DATECREATED
                Catch ex As Exception
                    .DATECREATED = Now
                End Try
                .DATEUPDATE = Now
                .KDREQBHP = sNoid
                .KDDOCTOR = grdDPJP.EditValue
                .KDDEPARTMENT = grdKDDEPARTMENT.EditValue
                .DATE = deDATE.DateTime
                .KDPENDAFTARAN = lblRegister.Text
                .NOIDUSER = sUserID
                .TINDAKAN = ""
                .ISLAB = False
                .LAB = ""
                .ISRONTGEN = False
                .RONTGEN = ""
                .ISUSG = False
                .USG = ""
                .ISRONTGEN = False
                .RONTGEN = ""
                .ISLAIN = False
                .LAIN = txtDIAGNOSAUTAMA.Text
                .DESCRIPTION = ""
            End With

            Dim arrDetail = oReq_BHP.GetStructureDetailList
            For i As Integer = 0 To grvBillingTamabahanT.RowCount - 1
                Dim dsDetail = oReq_BHP.GetStructureDetail
                With dsDetail
                    .SEQ = i
                    .KDREQBHP = ds.KDREQBHP
                    .RINCIAN = grvBillingTamabahanT.GetRowCellValue(i, colRINCIAN)
                    .QTY = CDec(grvBillingTamabahanT.GetRowCellValue(i, colQTYORDER))
                    .DATE_PERMINTAAN = CDate(grvBillingTamabahanT.GetRowCellValue(i, colDATEPEMERIKSAAN))
                    .KETERANGAN = IIf(String.IsNullOrEmpty(grvBillingTamabahanT.GetRowCellValue(i, colKETERANGANORDER)), "", grvBillingTamabahanT.GetRowCellValue(i, colKETERANGANORDER))
                    .KETERANGAN_PENUNJANG = IIf(String.IsNullOrEmpty(grvBillingTamabahanT.GetRowCellValue(i, colKETERANGAN_PENUNJANG)), "LAIN", grvBillingTamabahanT.GetRowCellValue(i, colKETERANGAN_PENUNJANG))
                    .GROUP = ""
                    .ISCHEKED = False
                    .KDTARIF = IIf(String.IsNullOrEmpty(grvBillingTamabahanT.GetRowCellValue(i, colKDTARIF)), 0, grvBillingTamabahanT.GetRowCellValue(i, colKDTARIF))
                    .ISBACA = False
                End With

                If dsDetail.RINCIAN <> "" Then
                    arrDetail.Add(dsDetail)
                End If
            Next

            If sNoid = "" Then
                Try
                    Dim KDREQBHP As String = oReq_BHP.InsertData(ds, arrDetail)
                    If KDREQBHP <> "" Then
                        fn_SaveOrder = True
                    Else
                        fn_SaveOrder = False
                    End If
                Catch ex As Exception
                    MsgBox("Simpan Data : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            Else
                Try
                    fn_SaveOrder = oReq_BHP.UpdateData(ds, arrDetail)
                Catch ex As Exception
                    MsgBox("Simpan Data : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            End If
        Catch oErr As Exception
            MsgBox("Simpan Data : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            fn_SaveOrder = False
        End Try
    End Function
    Private Sub ToolStripMenuItem2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem2.Click
        grvBillingTamabahanT.DeleteSelectedRows()
    End Sub
#End Region
#Region "Surat Kontrol Dokter"
    Private Function fn_SaveAntrian(ByVal sDATE As DateTime, ByVal KODEANTRIAN As String) As Integer
        Try
            ' ***** HEADER *****
            Dim oSet_Antrian_Simpan As New Antrian.clsSet_Antrian_Simpan

            Dim ds = oSet_Antrian_Simpan.GetStructureHeader
            With ds
                .DATECREATED = sDATE
                .KDANTRIANMANUAL = 0
                .KODE = KODEANTRIAN
                .NOMORANTRIAN = 0
                .ISPANGGIL = False
            End With

            fn_SaveAntrian = oSet_Antrian_Simpan.InsertData(ds)

        Catch oErr As Exception
            fn_SaveAntrian = 0
        End Try
    End Function
    Private Sub txtBERATBADAN_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles txtBERATBADAN.Validating
        If lblRegister.Text <> "-" Then
            fn_SaveCustomer_Detil()
        End If
    End Sub
    Private Sub txtTB_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles txtTB.Validating
        If lblRegister.Text <> "-" Then
            fn_SaveCustomer_Detil()
        End If
    End Sub
    Private Sub txtALERGIOBAT_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles txtALERGIOBAT.Validating
        If lblRegister.Text <> "-" Then
            fn_SaveCustomer_Detil()
        End If
    End Sub
    Private Sub grdKDDIAGNOSA_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs)
        If lblRegister.Text <> "-" Then
            fn_SaveCustomer_Detil()
        End If
    End Sub
    Private Sub AddToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AddToolStripMenuItem.Click
        If lblRegister.Text = "" Then
            MsgBox("Pasien Belum dipilih, Silahkan kembali ke List Pasien", MsgBoxStyle.Exclamation, Me.Text)
            fn_EmptyMe()
            Exit Sub
        End If

        If grvListPasien.GetFocusedRowCellValue("Antrian") <> "" Then
            Dim uTime As Integer = (DateTime.UtcNow - New DateTime(1970, 1, 1, 0, 0, 0)).TotalSeconds
            Dim JsonRequest As String = fn_RequestUpdateWaktuAntrean(grvListPasien.GetFocusedRowCellValue("Antrian"), 4, uTime)
            If JsonRequest <> "" Then
                MsgBox(fn_UpdateWaktuAntrean(JsonRequest, uTime), MsgBoxStyle.Information, Me.Text)
            End If
        End If

        Dim oKoding As New Admission.clsKoding
        Dim dsKoding = oKoding.GetDataByKDPENDAFTARAN(lblRegister.Text)
        If dsKoding IsNot Nothing Then
            Dim frmMedrekRawatJalan As New frmMedrekRawatJalan
            Try
                frmMedrekRawatJalan.fn_LoadIdentias(txtBERATBADAN.Text, txtTB.Text, txtALERGIOBAT.Text, lblRegister.Text, sUMUPASIEN, lblNamaPasien.Text, lblRM.Text, lblDPJP.Text, grdKDDEPARTMENT.Text, lblSIP.Text, lblPenjamin.Text, sKODEBOOKING, grdKDDEPARTMENT.EditValue, grdDPJP.EditValue, lblNOMORSEP.Text, sKARTUBPJS, txtDIAGNOSAUTAMA.Text, sTANGGALLAHIR, sJK, grvListPasien.GetFocusedRowCellValue("AntrianDokter"))
                frmMedrekRawatJalan.LoadMe(FORM_MODE.FORM_MODE_EDIT, dsKoding.KDKODING)
                frmMedrekRawatJalan.ShowDialog(Me)
                fn_LoadSecurity()
            Catch ex As Exception
                MsgBox("Load Form Detail : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
            Finally
                If Not frmMedrekRawatJalan Is Nothing Then frmMedrekRawatJalan.Dispose()
                frmMedrekRawatJalan = Nothing

                Dim rowHandle As Integer = grvListPasien.LocateByValue(rowHandle, grvListPasien.Columns("NoRegister"), sCode)
                If rowHandle > 0 Then grvListPasien.FocusedRowHandle = rowHandle

                If sStatusSave = "NEW" Then
                    sStatusSave = "NONE"
                End If
            End Try
        Else
            Dim frmMedrekRawatJalan As New frmMedrekRawatJalan
            Try
                frmMedrekRawatJalan.fn_LoadIdentias(txtBERATBADAN.Text, txtTB.Text, txtALERGIOBAT.Text, lblRegister.Text, sUMUPASIEN, lblNamaPasien.Text, lblRM.Text, lblDPJP.Text, grdKDDEPARTMENT.Text, lblSIP.Text, lblPenjamin.Text, sKODEBOOKING, grdKDDEPARTMENT.EditValue, grdDPJP.EditValue, lblNOMORSEP.Text, sKARTUBPJS, txtDIAGNOSAUTAMA.Text, sTANGGALLAHIR, sJK, grvListPasien.GetFocusedRowCellValue("AntrianDokter"))
                frmMedrekRawatJalan.LoadMe(FORM_MODE.FORM_MODE_ADD)
                frmMedrekRawatJalan.ShowDialog(Me)
                fn_LoadSecurity()
            Catch ex As Exception
                MsgBox("Load Form Detail : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
            Finally
                If Not frmMedrekRawatJalan Is Nothing Then frmMedrekRawatJalan.Dispose()
                frmMedrekRawatJalan = Nothing

                Dim rowHandle As Integer = grvListPasien.LocateByValue(rowHandle, grvListPasien.Columns("NoRegister"), sCode)
                If rowHandle > 0 Then grvListPasien.FocusedRowHandle = rowHandle

                If sStatusSave = "NEW" Then
                    sStatusSave = "NONE"
                End If
            End Try
        End If

    End Sub
    Private Sub btnMasukPoli_Click(sender As Object, e As EventArgs) Handles btnMasukPoli.Click
        Try
            If grvListPasien.GetFocusedRowCellValue("Antrian") Is Nothing Then
                Exit Sub
            End If

            If grvListPasien.GetFocusedRowCellValue("Antrian") <> "" Then
                Dim uTime As Integer = (DateTime.UtcNow - New DateTime(1970, 1, 1, 0, 0, 0)).TotalSeconds
                Dim JsonRequest As String = fn_RequestUpdateWaktuAntrean(grvListPasien.GetFocusedRowCellValue("Antrian"), 4, uTime)
                If JsonRequest <> "" Then
                    MsgBox(fn_UpdateWaktuAntrean(JsonRequest, uTime), MsgBoxStyle.Information, Me.Text)
                End If
            End If
        Catch oErr As Exception
            MsgBox("Print Data : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
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
            Dim dsDataSetKoneksi = oSetKoneksi.GetData().FirstOrDefault(Function(x) x.ISACTIVE = True And x.NAME_DISPLAY = "ANTREAN")

            Dim dsSetKoneksi = oSetKoneksi.UpdateWaktuAntrean("https://apijkn.bpjs-kesehatan.go.id/antreanrs/", "16694", "9kODD4D793", "26db4b5c5610878ef22c8113992891c7", uTime, jsonRequest)

            Dim allData = JObject.Parse(dsSetKoneksi)

            Dim CodeResponse As String = String.Empty
            Dim messageResponse As String = String.Empty

            CodeResponse = IIf(IsDBNull(allData.Item("metadata").Item("code")) = True, "", allData.Item("metadata").Item("code"))
            messageResponse = allData("metadata")("message").ToString

            If CodeResponse = "200" Then
                fn_UpdateWaktuAntrean = CodeResponse
            Else
                fn_UpdateWaktuAntrean = CodeResponse & "-" & messageResponse
            End If

        Catch oErr As Exception
            fn_UpdateWaktuAntrean = oErr.Message
        End Try
    End Function
    Private Sub CopyToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CopyToolStripMenuItem.Click
        listCopy.Clear()
        Dim oReq_Recipe As New Transaksi.clsReq_Recipe

        For iLoop As Integer = 0 To grvHystori.RowCount - 1
            If CBool(grvHystori.GetRowCellValue(iLoop, "PILIH")) = True Then
                If grvHystori.GetRowCellValue(iLoop, "KDITEM") <> "" Then
                    Dim dsDetail = oReq_Recipe.GetStructureDetail
                    With dsDetail
                        .SEQ = i
                        .KDREQRECIPE = grvHystori.GetRowCellValue(iLoop, "KDREQRECIPE")
                        .NAMAOBAT = grvHystori.GetRowCellValue(iLoop, "NAMAOBAT")
                        .SATUAN = grvHystori.GetRowCellValue(iLoop, "SATUAN")
                        .SIGNA = ""
                        .CARAPAKAI = ""
                        .QTY = grvHystori.GetRowCellValue(iLoop, "JUMLAH")
                        .PRICE = CDec(0)
                        .GRANDTOTAL = CDec(0)
                        .ROMAWI = ""
                        .REMARKS_DOKTER = grvHystori.GetRowCellValue(iLoop, "CARAPAKAI")
                        .KDITEM = grvHystori.GetRowCellValue(iLoop, "KDITEM")
                        .KDUOM = grvHystori.GetRowCellValue(iLoop, "KDUOM")
                        .KDSIGNA = grvHystori.GetRowCellValue(iLoop, "KET1")
                        .KDCARAPAKAI = grvHystori.GetRowCellValue(iLoop, "KET2")
                        .QTY_PERUBAHAN = 0
                        .REMARKS_FARMASI = ""
                    End With
                    listCopy.Add(dsDetail)
                End If
            End If
        Next

        fn_LoadHystori(lblRM.Text)
    End Sub
#End Region
    Private Sub frmItem_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If sisDOkter = False Then Exit Sub
        Select Case e.KeyCode
            Case Keys.F3
                If lblRegister.Text = "" Then
                    MsgBox("Pasien Belum dipilih, Silahkan kembali ke List Pasien", MsgBoxStyle.Exclamation, Me.Text)
                    fn_EmptyMe()
                    Exit Sub
                End If

                If grvListPasien.GetFocusedRowCellValue("Antrian") <> "" Then
                    Dim uTime As Integer = (DateTime.UtcNow - New DateTime(1970, 1, 1, 0, 0, 0)).TotalSeconds
                    Dim JsonRequest As String = fn_RequestUpdateWaktuAntrean(grvListPasien.GetFocusedRowCellValue("Antrian"), 4, uTime)
                    If JsonRequest <> "" Then
                        MsgBox(fn_UpdateWaktuAntrean(JsonRequest, uTime), MsgBoxStyle.Information, Me.Text)
                    End If
                End If

                Dim oKoding As New Admission.clsKoding
                Dim dsKoding = oKoding.GetDataByKDPENDAFTARAN(lblRegister.Text)
                If dsKoding IsNot Nothing Then
                    Dim frmMedrekRawatJalan As New frmMedrekRawatJalan
                    Try
                        frmMedrekRawatJalan.fn_LoadIdentias(txtBERATBADAN.Text, txtTB.Text, txtALERGIOBAT.Text, lblRegister.Text, sUMUPASIEN, lblNamaPasien.Text, lblRM.Text, lblDPJP.Text, grdKDDEPARTMENT.Text, lblSIP.Text, lblPenjamin.Text, sKODEBOOKING, grdKDDEPARTMENT.EditValue, grdDPJP.EditValue, lblNOMORSEP.Text, sKARTUBPJS, txtDIAGNOSAUTAMA.Text, sTANGGALLAHIR, sJK,grvListPasien.GetFocusedRowCellValue("AntrianDokter"))
                        frmMedrekRawatJalan.LoadMe(FORM_MODE.FORM_MODE_EDIT, dsKoding.KDKODING)
                        frmMedrekRawatJalan.ShowDialog(Me)
                        fn_LoadSecurity()
                    Catch ex As Exception
                        MsgBox("Load Form Detail : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
                    Finally
                        If Not frmMedrekRawatJalan Is Nothing Then frmMedrekRawatJalan.Dispose()
                        frmMedrekRawatJalan = Nothing

                        Dim rowHandle As Integer = grvListPasien.LocateByValue(rowHandle, grvListPasien.Columns("NoRegister"), sCode)
                        If rowHandle > 0 Then grvListPasien.FocusedRowHandle = rowHandle

                        If sStatusSave = "NEW" Then
                            sStatusSave = "NONE"
                        End If
                    End Try
                Else
                    Dim frmMedrekRawatJalan As New frmMedrekRawatJalan
                    Try
                        frmMedrekRawatJalan.fn_LoadIdentias(txtBERATBADAN.Text, txtTB.Text, txtALERGIOBAT.Text, lblRegister.Text, sUMUPASIEN, lblNamaPasien.Text, lblRM.Text, lblDPJP.Text, grdKDDEPARTMENT.Text, lblSIP.Text, lblPenjamin.Text, sKODEBOOKING, grdKDDEPARTMENT.EditValue, grdDPJP.EditValue, lblNOMORSEP.Text, sKARTUBPJS, txtDIAGNOSAUTAMA.Text, sTANGGALLAHIR, sJK, grvListPasien.GetFocusedRowCellValue("AntrianDokter"))
                        frmMedrekRawatJalan.LoadMe(FORM_MODE.FORM_MODE_ADD)
                        frmMedrekRawatJalan.ShowDialog(Me)
                        fn_LoadSecurity()
                    Catch ex As Exception
                        MsgBox("Load Form Detail : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
                    Finally
                        If Not frmMedrekRawatJalan Is Nothing Then frmMedrekRawatJalan.Dispose()
                        frmMedrekRawatJalan = Nothing

                        Dim rowHandle As Integer = grvListPasien.LocateByValue(rowHandle, grvListPasien.Columns("NoRegister"), sCode)
                        If rowHandle > 0 Then grvListPasien.FocusedRowHandle = rowHandle

                        If sStatusSave = "NEW" Then
                            sStatusSave = "NONE"
                        End If
                    End Try
                End If
        End Select
    End Sub
    Private Sub grdKDDEPARTMENT_EditValueChanged(sender As Object, e As EventArgs) Handles grdKDDEPARTMENT.EditValueChanged
        If isLoad = True Then
            fn_LoadDokter(grdKDDEPARTMENT.EditValue)
        End If
    End Sub
    Private Sub PemeriksaanAwalToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PemeriksaanAwalToolStripMenuItem.Click
        If lblRegister.Text <> "" Then
            Dim frmReqAwalPemeriksaan As New frmReqAwalPemeriksaan
            Try
                frmReqAwalPemeriksaan.LoadMe(FORM_MODE.FORM_MODE_ADD, lblNamaPasien.Text, sTANGGALLAHIR, lblRM.Text, sUMUPASIEN, sJK, lblRegister.Text, grdDPJP.EditValue)
                frmReqAwalPemeriksaan.ShowDialog(Me)
                fn_LoadData()
            Catch ex As Exception
                MsgBox("Load Form Detail : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
            Finally
                If Not frmReqAwalPemeriksaan Is Nothing Then frmReqAwalPemeriksaan.Dispose()
                frmReqAwalPemeriksaan = Nothing

                Dim rowHandle As Integer = grvListPasien.LocateByValue(rowHandle, grvListPasien.Columns("NoRegister"), sCode)
                If rowHandle > 0 Then grvListPasien.FocusedRowHandle = rowHandle

                If sStatusSave = "NEW" Then
                    sStatusSave = "NONE"
                End If
            End Try
        End If
    End Sub
#End Region
End Class