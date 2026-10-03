Imports iPOS.GLB.Globals
Imports iPOS.DA
Imports System.Linq
Imports System.Data
Imports System.Data.SqlClient

Public Class frmKodingList
    Public Noreg As String
    Private oFormMode As FORM_MODE = FORM_MODE.FORM_MODE_VIEW
    Private oPendaftaran As New Pendaftaran.clsPendaftaran

#Region "Function"
    Private Sub Me_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        deDATEFrom.DateTime = Now
        deDATETo.DateTime = Now
        fn_LoadFilter()
        fn_LoadSecurity()

        Me.Text = "Koding - List Edit Form"

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
                     On x.NOIDOTORITY Equals y.NOIDOTORITY
                      Where x.NOIDMODUL = "KODING" _
                     And y.NOIDUSER = sUserID
                      Select x.ISADD, x.ISDELETE, x.ISUPDATE, x.ISPRINT, x.ISVIEW).FirstOrDefault

            Try
                picAdd.Enabled = ds.ISADD
                picUpdate.Enabled = ds.ISUPDATE
                picPrint.Enabled = ds.ISPRINT
                picRefresh.Enabled = ds.ISVIEW

                If ds.ISVIEW = True Then
                    fn_LoadData()
                End If
            Catch ex As Exception
                MsgBox("Keamanan belum dipasang, tolong kontak admin anda!", MsgBoxStyle.Exclamation, Me.Text)

                picAdd.Enabled = False
                ' picDelete.Enabled = False
                picUpdate.Enabled = False
                picPrint.Enabled = False
                picRefresh.Enabled = False

            End Try
        Catch ex As Exception
            MsgBox("Load Security : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadFilter()
        cboKATEGORI.Properties.Items.Add(sKATEGORI_STATUS_JALAN)
        cboKATEGORI.Properties.Items.Add(sKATEGORI_STATUS_INAP)

        cboKATEGORI.SelectedIndex = 0
    End Sub
    Private Sub fn_LoadData()
        Try
            Dim oConn As New SqlConnection
            Dim oComm As New SqlCommand
            Dim da As SqlDataAdapter
            Dim ds As New DataSet
            Dim SQL As String

            Dim sConn As String = iPOS.GLB.Globals.Decrypt(My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Software\BUDIJAYAGROUP\BPJS\" & sDatabase & "\", "Database", "").ToString())
            '
            oConn = New SqlConnection(sConn)
            If oConn.State = ConnectionState.Closed Then
                oConn.Open()
            End If

            SQL = "SELECT "
            SQL &= "Jenis = CASE A.CATEGORY WHEN '1' THEN 'RJ' WHEN '2' THEN 'RI' END , NoRegister = A.KDREG, TanggalDatang = A.DATE, NoRM = A.KDCUSTOMER "
            SQL &= ",NamaPasien = B.NAME_DISPLAY + ' ' + IIf(B.FRONT_TITLE IS NULL, '', B.FRONT_TITLE + ' ') + B.BACK_TITLE  "
            SQL &= ",StatusBayar = CASE WHEN A.GRANDTOTAL = 0 THEN 'BELUM LUNAS' WHEN A.GRANDTOTAL > A.PAYAMOUNT THEN 'BELUM LUNAS' WHEN A.GRANDTOTAL <= A.PAYAMOUNT THEN 'LUNAS' END "
            SQL &= ",Poli = C.NAME_DISPLAY "
            SQL &= ",NamaDokter = IIf(D.FRONT_TITLE IS NULL, '', D.FRONT_TITLE + ' ') + D.NAME_DISPLAY  + ' ' + D.BACK_TITLE "
            SQL &= ",StatusBayar = CASE WHEN A.GRANDTOTAL = 0 THEN 'BELUM LUNAS' WHEN A.GRANDTOTAL > A.PAYAMOUNT THEN 'BELUM LUNAS' WHEN A.GRANDTOTAL <= A.PAYAMOUNT THEN 'LUNAS' END "
            SQL &= ",StatusDaftar = CASE A.STATUSDAFTAR WHEN '1' THEN 'DALAM ANTRIAN' WHEN '2' THEN 'SELESAI' WHEN '3' THEN 'BATAL' END "

            SQL &= "FROM S_PENDAFTARAN_H A "
            SQL &= "INNER JOIN M_CUSTOMER B "
            SQL &= "ON A.KDCUSTOMER = B.KDCUSTOMER "
            SQL &= "INNER JOIN M_DEPARTMENT C "
            SQL &= "ON A.KDDEPARTMENT = C.KDDEPARTMENT "
            SQL &= "INNER JOIN M_DOCTOR D "
            SQL &= "ON A.KDDOCTOR = D.KDDOCTOR "
            If cboKATEGORI.SelectedIndex = 0 Then
                SQL &= "WHERE CONVERT(VARCHAR(8), A.DATE, 112) >= '" & deDATEFrom.DateTime.ToString("yyyyMMdd") & "' AND CONVERT(VARCHAR(8), A.DATE, 112) <= '" & deDATETo.DateTime.ToString("yyyyMMdd") & "' AND A.STATUSDAFTAR <> 3 AND A.CATEGORY = 1 "
            Else
                SQL &= "WHERE CONVERT(VARCHAR(8), A.DATE, 112) >= '" & deDATEFrom.DateTime.ToString("yyyyMMdd") & "' AND CONVERT(VARCHAR(8), A.DATE, 112) <= '" & deDATETo.DateTime.ToString("yyyyMMdd") & "' AND A.STATUSDAFTAR <> 3 AND A.CATEGORY = 2 "
            End If
            SQL &= "ORDER BY A.KDREG DESC "

            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "S_PENDAFTARAN")

            BindingSource.DataSource = ds.Tables("S_PENDAFTARAN")

            grd.DataSource = BindingSource
            grd.ForceInitialize()

            fn_LoadFormatData()

            fn_LoadDetail()

            If oConn.State = ConnectionState.Open Then
                oConn.Close()
            End If

        Catch ex As Exception
            MsgBox("Load Data : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadFormatData()
        grv.Columns("TanggalDatang").DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        grv.Columns("TanggalDatang").DisplayFormat.FormatString = "{0:dd/MM/yyyy}"

    End Sub
    Private Sub fn_LoadDetail()
        Try
            'fn_SEPGABUNG()

            Dim oConn As New SqlConnection
            Dim oComm As New SqlCommand
            Dim da As SqlDataAdapter
            Dim ds As New DataSet
            Dim SQL As String

            Dim sConn As String = iPOS.GLB.Globals.Decrypt(My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Software\BUDIJAYAGROUP\BPJS\" & sDatabase & "\", "Database", "").ToString())

            oConn = New SqlConnection(sConn)

            If oConn.State = ConnectionState.Closed Then
                oConn.Open()
            End If

            grvBilling.OptionsSelection.MultiSelect = True
            grvBilling.SelectAll()
            grvBilling.DeleteSelectedRows()
            grvBilling.OptionsSelection.MultiSelect = False

            SQL = "SELECT A.* "
            SQL &= "FROM ( "
            SQL &= "(SELECT "
            SQL &= "NoKoding = A.KDKODING "
            SQL &= ",Tgl = A.DATE "
            SQL &= ",Kode = B.KDDIAGNOSA "
            SQL &= ",Diagnosa = C.DESCRIPTION "
            SQL &= ",Ket = A.DESCRIPTION "
            SQL &= ",[User] = A.NOIDUSER "
            SQL &= ",Seq = B.SEQ "

            SQL &= "FROM S_KODING_H A "
            SQL &= "INNER JOIN S_KODING_DX B "
            SQL &= "ON A.KDKODING = B.KDKODING "
            SQL &= "INNER JOIN M_DIAGNOSAICD10 C "
            SQL &= "ON B.KDDIAGNOSA = C.KDDIAGNOSA "

            SQL &= "WHERE A.KDREG = '" & grv.GetFocusedRowCellValue("NoRegister") & "' "
            SQL &= ") "
            SQL &= ") AS A "
            'SQL &= "ORDER BY A.NoKoding ASC "
            SQL &= "ORDER BY A.Seq ASC "

            oComm.Connection = oConn

            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "S_PENDAFTARAN_DETIL")

            BindingSource1.DataSource = ds.Tables("S_PENDAFTARAN_DETIL")

            grdBilling.DataSource = BindingSource1
            grdBilling.ForceInitialize()

            grvBilling.BestFitColumns()

            fn_SetFormat()

            If oConn.State = ConnectionState.Open Then
                oConn.Close()
            End If

        Catch ex As Exception
            MsgBox("Load Detail : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_EmptyMe()

    End Sub
    Private Sub fn_SetFormat()
        Select Case cboType.SelectedIndex
            Case 0

                For iLoop As Integer = 0 To grvBilling.Columns.Count - 1
                    If grvBilling.Columns(iLoop).ColumnType.Name = "Decimal" Then

                    ElseIf grvBilling.Columns(iLoop).ColumnType.Name = "DateTime" Then
                        grvBilling.Columns(iLoop).DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
                        grvBilling.Columns(iLoop).DisplayFormat.FormatString = "{0:dd/MM/yyyy}"

                    End If

                Next

        End Select

    End Sub
    Private Function fn_DeleteData(ByVal sKDKODING As String) As Boolean
        Try
            Dim oTransaksi As New Koding.clsKoding

            oTransaksi.DeleteData(sKDKODING)
            fn_DeleteData = True
        Catch oErr As Exception
            fn_DeleteData = False
            MsgBox("Hapus Data : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Function
    Private Function fn_ValidasiSEP() As Boolean

    End Function

#End Region
#Region "Command Button"
    Private Sub frmMember_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        Select Case e.KeyCode
            Case Keys.A
                If e.Alt = True And picAdd.Enabled = True Then
                    picAdd_Click()
                End If
            Case Keys.E
                If e.Alt = True And picUpdate.Enabled = True Then
                    picUpdate_Click()
                End If
            Case Keys.P
                If e.Alt = True And picPrint.Enabled = True Then
                    'picPrint_Click()
                End If
            Case Keys.R
                If e.Alt = True And picRefresh.Enabled = True Then
                    picRefresh_Click()
                End If
        End Select
    End Sub
    Private Sub grv_FocusedRowChanged(ByVal sender As System.Object, ByVal e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs) Handles grv.FocusedRowChanged
        Try
            If grv.GetFocusedRowCellValue("NoRegister") Is Nothing Then
                fn_EmptyMe()
                Exit Sub
            End If

            fn_LoadDetail()
        Catch ex As Exception

        End Try
    End Sub
    'Private Sub grv_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles grv.DoubleClick
    '    Try
    '        If grv.GetFocusedRowCellValue("NoRegister") Is Nothing Then
    '            fn_EmptyMe()
    '            Exit Sub
    '        End If

    '        fn_LoadDetail()

    '    Catch ex As Exception

    '    End Try
    'End Sub
    Private Sub picAdd_Click() Handles picAdd.Click
        If grv.GetFocusedRowCellValue("NoRegister") = String.Empty Then Exit Sub

        Dim frmKoding As New frmKoding
        Try
            frmKoding.LoadMe(FORM_MODE.FORM_MODE_ADD, grv.GetFocusedRowCellValue("NoRegister"), grv.GetFocusedRowCellValue("Cetak"))
            frmKoding.ShowDialog(Me)
            fn_LoadSecurity()
        Catch ex As Exception
            MsgBox("Load Form Detail : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
        Finally
            If Not frmKoding Is Nothing Then frmKoding.Dispose()
            frmKoding = Nothing

            Dim rowHandle As Integer = grv.LocateByValue(rowHandle, grv.Columns("NoRegister"), sCode)
            If rowHandle > 0 Then grv.FocusedRowHandle = rowHandle

            If sStatusSave = "NEW" Then
                sStatusSave = "NONE"
                picAdd_Click()
            End If
        End Try
    End Sub
    Private Sub picUpdate_Click() Handles picUpdate.Click
        If grvBilling.GetFocusedRowCellValue("NoKoding") Is Nothing Then
            fn_EmptyMe()
            Exit Sub
        End If


        Dim frmKoding As New frmKoding
        Try
            frmKoding.LoadMe(FORM_MODE.FORM_MODE_EDIT, grv.GetFocusedRowCellValue("NoRegister"), grvBilling.GetFocusedRowCellValue("NoKoding"))
            frmKoding.ShowDialog(Me)
            fn_LoadSecurity()
        Catch ex As Exception
            MsgBox("Load Form Detail : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
        Finally
            If Not frmKoding Is Nothing Then frmKoding.Dispose()
            frmKoding = Nothing

            Dim rowHandle As Integer = grv.LocateByValue(rowHandle, grv.Columns("NoRegister"), sCode)
            If rowHandle > 0 Then grv.FocusedRowHandle = rowHandle

            If sStatusSave = "NEW" Then
                sStatusSave = "NONE"
                picAdd_Click()
            End If
        End Try
    End Sub
    Private Sub DeleteToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DeleteToolStripMenuItem.Click
        If grvBilling.GetFocusedRowCellValue("NoKoding") Is Nothing Then
            fn_EmptyMe()
            Exit Sub
        End If

        If MsgBox("Delete " & grvBilling.GetFocusedRowCellValue("NoKoding") & "?", MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, Me.Text) = MsgBoxResult.No Then Exit Sub
        If fn_DeleteData(grvBilling.GetFocusedRowCellValue("NoKoding")) = False Then
            MsgBox("Hapus gagal! tolong kontak admin anda!", MsgBoxStyle.Exclamation, Me.Text)
            Exit Sub
        End If
        MsgBox("Delete " & grvBilling.GetFocusedRowCellValue("NoKoding") & " success!", MsgBoxStyle.Information, Me.Text)
        grvBilling.DeleteSelectedRows()
        fn_LoadSecurity()

    End Sub
    Private Function fn_PrintStruk(ByVal sCode As String) As Boolean
        If sKodeBillingTranskasi = 1 Then
            'If txtKDSO.Text = String.Empty Then Exit Function

            Dim oConn As New SqlConnection
            Dim oComm As New SqlCommand
            Dim da As SqlDataAdapter
            Dim ds As New DataSet
            Dim SQL As String

            Dim sConn As String = iPOS.GLB.Globals.Decrypt(My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Software\BUDIJAYAGROUP\BPJS\" & sDatabase & "\", "Database", "").ToString())

            oConn = New SqlConnection(sConn)
            If oConn.State = ConnectionState.Closed Then
                oConn.Open()
            End If

            SQL = "SELECT  "
            'SQL &= "RUANG = (SELECT BB.NAME_DISPLAY FROM S_PENDAFTARAN_H AS AA INNER JOIN M_DEPARTMENT AS BB ON AA.KDDEPARTMENT = BB.KDDEPARTMENT WHERE AA.KDREG = '" & txtKDSO.Text & "') "
            SQL &= ",C.KDREG "
            'SQL &= ",JENIS = (SELECT BB.DESCRIPTION FROM S_PENDAFTARAN_H AS AA INNER JOIN M_JENISPESERTA AS BB ON AA.KDPESERTA = BB.KDPESERTA WHERE AA.KDREG = '" & txtKDSO.Text & "') "
            'SQL &= ",NOPASIEN = (SELECT AA.NOPASIEN FROM S_PENDAFTARAN_H AS AA WHERE AA.KDREG = '" & txtKDSO.Text & "') "
            'SQL &= ",NAMAPASIEN = (SELECT BB.NAMAPASIEN FROM S_PENDAFTARAN_H AS AA INNER JOIN MASTER_PASIEN AS BB ON AA.NOPASIEN = BB.NOPASIEN WHERE AA.KDREG = '" & txtKDSO.Text & "') "
            'SQL &= ",ALAMATPASIEN = (SELECT BB.ALM1PASIEN FROM S_PENDAFTARAN_H AS AA INNER JOIN MASTER_PASIEN AS BB ON AA.NOPASIEN = BB.NOPASIEN WHERE AA.KDREG = '" & txtKDSO.Text & "') "
            ' SQL &= ",TANGGALMASUK = (SELECT AA.DATE FROM S_PENDAFTARAN_H AS AA WHERE AA.KDREG = '" & txtKDSO.Text & "') "
            'SQL &= ",TANGGALKELUAR = (SELECT AA.DATEPULANG FROM S_PENDAFTARAN_H AS AA WHERE AA.KDREG = '" & txtKDSO.Text & "') "
            'SQL &= ",KELAS = (SELECT BB.DESCRIPTION FROM S_PENDAFTARAN_H AS AA INNER JOIN M_KELASRAWAT AS BB ON AA.KDKELASRAWAT = BB.KDKELASRAWAT WHERE AA.KDREG = '" & txtKDSO.Text & "') "
            'SQL &= ",CATEGORY = (SELECT AA.CATEGORY FROM S_PENDAFTARAN_H AS AA WHERE AA.KDREG = '" & txtKDSO.Text & "') "
            SQL &= ",GROUPTARIF = D.DESCRIPTION "
            SQL &= ",NAMATARIF = B.TARIFKT "
            SQL &= ",QTY = SUM(A.QTY) "
            SQL &= ",A.PRICE "
            SQL &= ",GRANDTOTAL = SUM(A.GRANDTOTAL) "
            SQL &= ",NAMADOKTER = (SELECT CASE WHEN A.KDDOCTOR IS NULL THEN '-' ELSE (SELECT NAME_DISPLAY FROM M_DOCTOR WHERE KDDOCTOR = A.KDDOCTOR ) END ) "
            SQL &= ",C.KDBILLING "
            SQL &= ",C.KDREG "
            SQL &= "FROM S_TRANSAKSILAB_D A "
            SQL &= "INNER JOIN M_TARIFRS AS B "
            SQL &= "ON A.KDITEMTARIF = B.KDITEMTARIF "
            SQL &= "INNER JOIN S_TRANSAKSILAB_H AS C "
            SQL &= "ON A.KDTRANSAKSILAB = C.KDTRANSAKSILAB "
            SQL &= "INNER JOIN M_KODETARIF AS D "
            SQL &= "ON A.KODEUNITTARIF = D.KODEUNITTARIF "
            'SQL &= "WHERE C.KDREG = '" & txtKDSO.Text & "' OR C.KDREG = '" & txtNotaRawatJalan.Text & "' "
            SQL &= "GROUP BY D.DESCRIPTION, B.TARIFKT, A.PRICE, C.KDBILLING, A.KDDOCTOR, C.KDREG  "
            ' SQL &= "ORDER BY C.KDREG DESC "

            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "S_PENDAFTARAN_DETIL")

            sPrintGrandTotal = 0

            For xloop As Integer = 0 To ds.Tables("S_PENDAFTARAN_DETIL").Rows.Count - 1
                sPrintGrandTotal += ds.Tables("S_PENDAFTARAN_DETIL").Rows(xloop)("GRANDTOTAL")
            Next


            Dim listPendaftaranH As New List(Of DA.dcEntity.R_RINCIAN_RAWATINAP)


            For iLoop As Integer = 0 To ds.Tables("S_PENDAFTARAN_DETIL").Rows.Count - 1
                Dim dsPendaftaranH As New DA.dcEntity.R_RINCIAN_RAWATINAP
                With ds.Tables("S_PENDAFTARAN_DETIL")

                    'dsPendaftaranH.RUANG = .Rows(iLoop)("RUANG").ToString
                    'dsPendaftaranH.JENIS = .Rows(iLoop)("JENIS").ToString
                    'dsPendaftaranH.NOPASIEN = .Rows(iLoop)("NOPASIEN").ToString
                    'dsPendaftaranH.NAMAPASIEN = .Rows(iLoop)("NAMAPASIEN").ToString
                    'dsPendaftaranH.ALAMATPASIEN = .Rows(iLoop)("ALAMATPASIEN").ToString
                    'dsPendaftaranH.TANGGALMASUK = .Rows(iLoop)("TANGGALMASUK").ToString
                    'dsPendaftaranH.TANGGALKELUAR = .Rows(iLoop)("TANGGALKELUAR").ToString
                    'dsPendaftaranH.KELAS = .Rows(iLoop)("KELAS").ToString
                    'dsPendaftaranH.CATEGORY = .Rows(iLoop)("CATEGORY").ToString

                    'dsPendaftaranH.KDREG = .Rows(iLoop)("KDREG").ToString
                    'dsPendaftaranH.NAMADOKTER = .Rows(iLoop)("NAMADOKTER").ToString
                    'dsPendaftaranH.GROUPTARIF = .Rows(iLoop)("GROUPTARIF").ToString
                    'dsPendaftaranH.NAMATARIF = .Rows(iLoop)("NAMATARIF").ToString
                    'dsPendaftaranH.QTY = .Rows(iLoop)("QTY").ToString
                    'dsPendaftaranH.PRICE = .Rows(iLoop)("PRICE").ToString
                    'dsPendaftaranH.GRANDTOTAL = .Rows(iLoop)("GRANDTOTAL").ToString
                    'dsPendaftaranH.KDBILLING = .Rows(iLoop)("KDBILLING").ToString

                    listPendaftaranH.Add(dsPendaftaranH)

                End With

            Next

            SQL = "SELECT NAMAOBAT = B.NMITEM2, QTY = SUM(A.QTY), A.PRICE "
            SQL &= ",TOTALOBAT = SUM(A.GRANDTOTAL) "
            SQL &= ",GROUPTARIF = CASE A.ALKES WHEN 0 THEN 'XX TARIF OBAT' WHEN 1 THEN 'ZZ TARIF ALKES' END "
            SQL &= ",D.ISOUT "
            SQL &= "FROM S_SO_D As A "
            SQL &= "INNER JOIN M_ITEM As B "
            SQL &= "On A.KDITEM = B.KDITEM "
            SQL &= "INNER JOIN S_SO_H As C "
            SQL &= "On A.KDSO = C.KDSO "
            SQL &= "INNER JOIN S_REG_H As D "
            SQL &= "On C.KDREG = D.KDREG "
            If grv.GetFocusedRowCellValue("NoRegister2") <> String.Empty Then
                SQL &= "WHERE A.KDREG = '" & grv.GetFocusedRowCellValue("NoRegister2") & "' AND A.DUATIGA = 0 AND A.GRANDTOTAL <> 0 AND C.DESCRIPTION <> 'DIRGANTARA INDONESIA' "
                SQL &= "AND D.KATEGORI = 1 "
            Else
                SQL &= "WHERE A.KDREG = 'KOSONG' AND A.DUATIGA = 0 AND A.GRANDTOTAL <> 0 AND C.DESCRIPTION <> 'DIRGANTARA INDONESIA' "
                SQL &= "AND D.KATEGORI = 1 "
            End If
            SQL &= "GROUP BY B.NMITEM2, A.PRICE, A.ALKES, D.ISOUT "

            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "R_OBAT")

            If ds.Tables("R_OBAT").Rows.Count < 1 Then
                MsgBox("Data Obat Kosong")

                'Exit Function

            Else
                For xloop As Integer = 0 To ds.Tables("R_OBAT").Rows.Count - 1

                    If ds.Tables("R_OBAT").Rows(xloop)("ISOUT") = 0 Then
                        MsgBox("Obat Belum di tutup Oleh Farmasi")

                        Exit Function

                    End If

                    sPrintGrandTotal += ds.Tables("R_OBAT").Rows(xloop)("TOTALOBAT")
                Next

                For iLoop As Integer = 0 To ds.Tables("R_OBAT").Rows.Count - 1
                    Dim dsPendaftaranH As New DA.dcEntity.R_RINCIAN_RAWATINAP
                    With ds.Tables("R_OBAT")

                        'dsPendaftaranH.RUANG = oPendaftaran.GetData(txtKDSO.Text).M_DEPARTMENT.NAME_DISPLAY
                        'dsPendaftaranH.NOPASIEN = oPendaftaran.GetData(txtKDSO.Text).KDCUSTOMER
                        'dsPendaftaranH.KDREG = txtKDSO.Text
                        'dsPendaftaranH.NAMAPASIEN = oPendaftaran.GetData(txtKDSO.Text).M_CUSTOMER.NAME_DISPLAY
                        'dsPendaftaranH.ALAMATPASIEN = oPendaftaran.GetData(txtKDSO.Text).ADDRESS_STREET
                        'dsPendaftaranH.TANGGALMASUK = oPendaftaran.GetData(txtKDSO.Text).DATE
                        'dsPendaftaranH.KELAS = oPendaftaran.GetData(txtKDSO.Text).KELASRAWAT
                        'dsPendaftaranH.CATEGORY = oPendaftaran.GetData(txtKDSO.Text).CATEGORY

                        'dsPendaftaranH.NAMADOKTER = "-"
                        'dsPendaftaranH.GROUPTARIF = .Rows(iLoop)("GROUPTARIF").ToString
                        'dsPendaftaranH.NAMATARIF = .Rows(iLoop)("NAMAOBAT").ToString
                        'dsPendaftaranH.QTY = .Rows(iLoop)("QTY").ToString
                        'dsPendaftaranH.PRICE = .Rows(iLoop)("PRICE").ToString
                        'dsPendaftaranH.GRANDTOTAL = .Rows(iLoop)("TOTALOBAT").ToString
                        'dsPendaftaranH.KDBILLING = 7

                        listPendaftaranH.Add(dsPendaftaranH)

                    End With

                Next
            End If

            'Dim rpt As New xtraRekapBiayaRawatInap

            'sNota1 = txtKDSO.Text
            'sNota2 = txtNotaRawatJalan.Text
            sNomorSEPRincian = grv.GetFocusedRowCellValue("NOMORSEP")

            'Dim oGrouper As New Sales.clsGrouper

            'Dim dsGrouper = oGrouper.GetDataReg(grv.GetFocusedRowCellValue("NoRegister"))

            'If dsGrouper IsNot Nothing Then
            '    sTotalCBGRincian = oGrouper.GetDataReg(grv.GetFocusedRowCellValue("NoRegister")).HASILGROUPER

            '    sNamaCBGRincian = oGrouper.GetDataReg(grv.GetFocusedRowCellValue("NoRegister")).KDHASILGROUPER

            'Else
            '    sTotalCBGRincian = 0
            '    sNamaCBGRincian = ""
            'End If


            'rpt.bindingSource.DataSource = listPendaftaranH

            sPrintDokterDPJP = grv.GetFocusedRowCellValue("NamaDokter")

            'Dim printTool As New DevExpress.XtraReports.UI.ReportPrintTool(rpt)
            'printTool.ShowPreviewDialog()

            If oConn.State = ConnectionState.Open Then
                oConn.Close()
            End If

        End If

    End Function
    'Private Function fn_PrintStrukGabungBayi(ByVal sCode As String) As Boolean
    '    If sKodeBillingTranskasi = 1 Then
    '        If txtKDSO.Text = String.Empty Then Exit Function

    '        Dim sCOde1 As String = String.Empty
    '        Dim sCOde2 As String = String.Empty
    '        Dim sCOde3 As String = String.Empty
    '        Dim sCOde4 As String = String.Empty
    '        Dim sCOde5 As String = String.Empty

    '        Dim oConn As New SqlConnection
    '        Dim oComm As New SqlCommand
    '        Dim da As SqlDataAdapter
    '        Dim ds As New DataSet
    '        Dim SQL As String

    '        Dim sConn As String = iPOS.GLB.Globals.Decrypt(My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Software\BUDIJAYAGROUP\BPJS\" & sDatabase & "\", "Database", "").ToString())

    '        oConn = New SqlConnection(sConn)
    '        If oConn.State = ConnectionState.Closed Then
    '            oConn.Open()
    '        End If

    '        sCOde1 = txtKDSO.Text

    '        Try
    '            sCOde5 = oPendaftaran.GetData(txtKDSO.Text).KDREGAWAL
    '        Catch ex As Exception
    '            sCOde5 = String.Empty
    '            MsgBox("Kode Reg Awal Tidak Ditemukan Tidak dapat terhubung dengan Farmasi", MsgBoxStyle.Exclamation, Me.Text)
    '        End Try

    '        sCOde2 = txtNotaRawatJalan.Text

    '        Dim oTransaksi As New Sales.clsTransaksi

    '        Try
    '            Dim RuangGabung As String = String.Empty
    '            sCOde3 = oPendaftaran.GetDataByPERINA(grv.GetFocusedRowCellValue("NOMORSEP")).KDREG

    '            Try
    '                sCOde4 = oTransaksi.GetDataByKDREG(sCOde3).DESCRIPTION
    '            Catch ex As Exception
    '                sCOde4 = String.Empty
    '            End Try

    '        Catch ex As Exception
    '            sCOde3 = String.Empty
    '            sCOde4 = String.Empty
    '        End Try

    '        SQL = "SELECT  "
    '        SQL &= "RUANG = (SELECT BB.NAME_DISPLAY FROM S_PENDAFTARAN_H AS AA INNER JOIN M_DEPARTMENT AS BB ON AA.KDDEPARTMENT = BB.KDDEPARTMENT WHERE AA.KDREG = '" & txtKDSO.Text & "') "
    '        SQL &= ",C.KDREG "
    '        SQL &= ",JENIS = (SELECT BB.DESCRIPTION FROM S_PENDAFTARAN_H AS AA INNER JOIN M_JENISPESERTA AS BB ON AA.KDPESERTA = BB.KDPESERTA WHERE AA.KDREG = '" & txtKDSO.Text & "') "
    '        SQL &= ",NOPASIEN = (SELECT AA.NOPASIEN FROM S_PENDAFTARAN_H AS AA WHERE AA.KDREG = '" & txtKDSO.Text & "') "
    '        SQL &= ",NAMAPASIEN = (SELECT BB.NAMAPASIEN FROM S_PENDAFTARAN_H AS AA INNER JOIN MASTER_PASIEN AS BB ON AA.NOPASIEN = BB.NOPASIEN WHERE AA.KDREG = '" & txtKDSO.Text & "') "
    '        SQL &= ",ALAMATPASIEN = (SELECT BB.ALM1PASIEN FROM S_PENDAFTARAN_H AS AA INNER JOIN MASTER_PASIEN AS BB ON AA.NOPASIEN = BB.NOPASIEN WHERE AA.KDREG = '" & txtKDSO.Text & "') "
    '        SQL &= ",TANGGALMASUK = (SELECT AA.DATE FROM S_PENDAFTARAN_H AS AA WHERE AA.KDREG = '" & txtKDSO.Text & "') "
    '        SQL &= ",TANGGALKELUAR = (SELECT AA.DATEPULANG FROM S_PENDAFTARAN_H AS AA WHERE AA.KDREG = '" & txtKDSO.Text & "') "
    '        SQL &= ",KELAS = (SELECT BB.DESCRIPTION FROM S_PENDAFTARAN_H AS AA INNER JOIN M_KELASRAWAT AS BB ON AA.KDKELASRAWAT = BB.KDKELASRAWAT WHERE AA.KDREG = '" & txtKDSO.Text & "') "
    '        SQL &= ",CATEGORY = (SELECT AA.CATEGORY FROM S_PENDAFTARAN_H AS AA WHERE AA.KDREG = '" & txtKDSO.Text & "') "
    '        SQL &= ",GROUPTARIF = D.DESCRIPTION "
    '        SQL &= ",NAMATARIF = B.TARIFKT "
    '        SQL &= ",QTY = SUM(A.QTY) "
    '        SQL &= ",A.PRICE "
    '        SQL &= ",GRANDTOTAL = SUM(A.GRANDTOTAL) "
    '        SQL &= ",NAMADOKTER = (SELECT CASE WHEN A.KDDOCTOR IS NULL THEN '-' ELSE (SELECT NAME_DISPLAY FROM M_DOCTOR WHERE KDDOCTOR = A.KDDOCTOR ) END ) "
    '        SQL &= ",C.KDBILLING "
    '        SQL &= ",C.KDREG "
    '        SQL &= "FROM S_TRANSAKSILAB_D A "
    '        SQL &= "INNER JOIN M_TARIFRS AS B "
    '        SQL &= "ON A.KDITEMTARIF = B.KDITEMTARIF "
    '        SQL &= "INNER JOIN S_TRANSAKSILAB_H AS C "
    '        SQL &= "ON A.KDTRANSAKSILAB = C.KDTRANSAKSILAB "
    '        SQL &= "INNER JOIN M_KODETARIF AS D "
    '        SQL &= "ON A.KODEUNITTARIF = D.KODEUNITTARIF "
    '        SQL &= "WHERE C.KDREG = '" & sCOde1 & "' OR C.KDREG = '" & sCOde2 & "' OR C.KDREG = '" & sCOde3 & "' OR C.KDREG = '" & sCOde4 & "' "
    '        SQL &= "GROUP BY D.DESCRIPTION, B.TARIFKT, A.PRICE, C.KDBILLING, A.KDDOCTOR, C.KDREG  "

    '        oComm.Connection = oConn
    '        oComm.CommandText = SQL
    '        oComm.CommandTimeout = 120
    '        oComm.CommandType = CommandType.Text

    '        da = New SqlDataAdapter(oComm)
    '        da.Fill(ds, "S_PENDAFTARAN_DETIL")

    '        sPrintGrandTotal = 0

    '        For xloop As Integer = 0 To ds.Tables("S_PENDAFTARAN_DETIL").Rows.Count - 1
    '            sPrintGrandTotal += ds.Tables("S_PENDAFTARAN_DETIL").Rows(xloop)("GRANDTOTAL")
    '        Next


    '        Dim listPendaftaranH As New List(Of DA.dcEntity.R_RINCIAN_RAWATINAP)


    '        For iLoop As Integer = 0 To ds.Tables("S_PENDAFTARAN_DETIL").Rows.Count - 1
    '            Dim dsPendaftaranH As New DA.dcEntity.R_RINCIAN_RAWATINAP
    '            With ds.Tables("S_PENDAFTARAN_DETIL")

    '                dsPendaftaranH.RUANG = .Rows(iLoop)("RUANG").ToString
    '                dsPendaftaranH.JENIS = .Rows(iLoop)("JENIS").ToString
    '                dsPendaftaranH.NOPASIEN = .Rows(iLoop)("NOPASIEN").ToString
    '                dsPendaftaranH.NAMAPASIEN = .Rows(iLoop)("NAMAPASIEN").ToString
    '                dsPendaftaranH.ALAMATPASIEN = .Rows(iLoop)("ALAMATPASIEN").ToString
    '                dsPendaftaranH.TANGGALMASUK = .Rows(iLoop)("TANGGALMASUK").ToString
    '                dsPendaftaranH.TANGGALKELUAR = .Rows(iLoop)("TANGGALKELUAR").ToString
    '                dsPendaftaranH.KELAS = .Rows(iLoop)("KELAS").ToString
    '                dsPendaftaranH.CATEGORY = .Rows(iLoop)("CATEGORY").ToString

    '                dsPendaftaranH.KDREG = .Rows(iLoop)("KDREG").ToString
    '                dsPendaftaranH.NAMADOKTER = .Rows(iLoop)("NAMADOKTER").ToString
    '                dsPendaftaranH.GROUPTARIF = .Rows(iLoop)("GROUPTARIF").ToString
    '                dsPendaftaranH.NAMATARIF = .Rows(iLoop)("NAMATARIF").ToString
    '                dsPendaftaranH.QTY = .Rows(iLoop)("QTY").ToString
    '                dsPendaftaranH.PRICE = .Rows(iLoop)("PRICE").ToString
    '                dsPendaftaranH.GRANDTOTAL = .Rows(iLoop)("GRANDTOTAL").ToString
    '                dsPendaftaranH.KDBILLING = .Rows(iLoop)("KDBILLING").ToString

    '                listPendaftaranH.Add(dsPendaftaranH)

    '            End With

    '        Next

    '        SQL = "SELECT NAMAOBAT = B.NMITEM2, QTY = SUM(A.QTY), A.PRICE "
    '        SQL &= ",TOTALOBAT = SUM(A.GRANDTOTAL) "
    '        SQL &= ",GROUPTARIF = CASE A.ALKES WHEN 0 THEN 'XX TARIF OBAT' WHEN 1 THEN 'ZZ TARIF ALKES' END "
    '        SQL &= ",D.ISOUT "
    '        SQL &= "FROM S_SO_D As A "
    '        SQL &= "INNER JOIN M_ITEM As B "
    '        SQL &= "On A.KDITEM = B.KDITEM "
    '        SQL &= "INNER JOIN S_SO_H As C "
    '        SQL &= "On A.KDSO = C.KDSO "
    '        SQL &= "INNER JOIN S_REG_H As D "
    '        SQL &= "On C.KDREG = D.KDREG "
    '        If grv.GetFocusedRowCellValue("NoRegister2") <> String.Empty Then
    '            SQL &= "WHERE A.KDREG = '" & grv.GetFocusedRowCellValue("NoRegister2") & "' AND A.DUATIGA = 0 AND A.GRANDTOTAL <> 0 AND C.DESCRIPTION <> 'DIRGANTARA INDONESIA' "
    '            SQL &= "AND D.KATEGORI = 1 "
    '        Else
    '            SQL &= "WHERE A.KDREG = 'KOSONG' AND A.DUATIGA = 0 AND A.GRANDTOTAL <> 0 AND C.DESCRIPTION <> 'DIRGANTARA INDONESIA' "
    '            SQL &= "AND D.KATEGORI = 1 "
    '        End If
    '        SQL &= "GROUP BY B.NMITEM2, A.PRICE, A.ALKES, D.ISOUT "

    '        oComm.Connection = oConn
    '        oComm.CommandText = SQL
    '        oComm.CommandTimeout = 120
    '        oComm.CommandType = CommandType.Text

    '        da = New SqlDataAdapter(oComm)
    '        da.Fill(ds, "R_OBAT")

    '        If ds.Tables("R_OBAT").Rows.Count < 1 Then
    '            MsgBox("Data Obat Kosong")

    '            'Exit Function

    '        Else
    '            For xloop As Integer = 0 To ds.Tables("R_OBAT").Rows.Count - 1

    '                If ds.Tables("R_OBAT").Rows(xloop)("ISOUT") = 0 Then
    '                    MsgBox("Obat Belum di tutup Oleh Farmasi")

    '                    Exit Function

    '                End If

    '                sPrintGrandTotal += ds.Tables("R_OBAT").Rows(xloop)("TOTALOBAT")
    '            Next

    '            For iLoop As Integer = 0 To ds.Tables("R_OBAT").Rows.Count - 1
    '                Dim dsPendaftaranH As New DA.dcEntity.R_RINCIAN_RAWATINAP
    '                With ds.Tables("R_OBAT")

    '                    dsPendaftaranH.RUANG = oPendaftaran.GetData(txtKDSO.Text).M_DEPARTMENT.NAME_DISPLAY
    '                    dsPendaftaranH.JENIS = oPendaftaran.GetData(txtKDSO.Text).M_JENISPESERTA.DESCRIPTION
    '                    dsPendaftaranH.NOPASIEN = oPendaftaran.GetData(txtKDSO.Text).NOPASIEN
    '                    dsPendaftaranH.KDREG = txtKDSO.Text
    '                    dsPendaftaranH.NAMAPASIEN = oPendaftaran.GetData(txtKDSO.Text).MASTER_PASIEN.namapasien
    '                    dsPendaftaranH.ALAMATPASIEN = oPendaftaran.GetData(txtKDSO.Text).MASTER_PASIEN.alm1pasien
    '                    dsPendaftaranH.TANGGALMASUK = oPendaftaran.GetData(txtKDSO.Text).DATE
    '                    dsPendaftaranH.TANGGALKELUAR = oPendaftaran.GetData(txtKDSO.Text).DATEPULANG
    '                    dsPendaftaranH.KELAS = oPendaftaran.GetData(txtKDSO.Text).M_KELASRAWAT.DESCRIPTION
    '                    dsPendaftaranH.CATEGORY = oPendaftaran.GetData(txtKDSO.Text).CATEGORY

    '                    dsPendaftaranH.NAMADOKTER = "-"
    '                    dsPendaftaranH.GROUPTARIF = .Rows(iLoop)("GROUPTARIF").ToString
    '                    dsPendaftaranH.NAMATARIF = .Rows(iLoop)("NAMAOBAT").ToString
    '                    dsPendaftaranH.QTY = .Rows(iLoop)("QTY").ToString
    '                    dsPendaftaranH.PRICE = .Rows(iLoop)("PRICE").ToString
    '                    dsPendaftaranH.GRANDTOTAL = .Rows(iLoop)("TOTALOBAT").ToString
    '                    dsPendaftaranH.KDBILLING = 9

    '                    listPendaftaranH.Add(dsPendaftaranH)

    '                End With

    '            Next
    '        End If

    '        Dim rpt As New xtraRekapBiayaRawatInap

    '        sNota1 = txtKDSO.Text
    '        sNota2 = txtNotaRawatJalan.Text

    '        rpt.bindingSource.DataSource = listPendaftaranH

    '        sPrintDokterDPJP = grv.GetFocusedRowCellValue("NamaDokter")

    '        Dim printTool As New DevExpress.XtraReports.UI.ReportPrintTool(rpt)
    '        printTool.ShowPreviewDialog()

    '        If oConn.State = ConnectionState.Open Then
    '            oConn.Close()
    '        End If

    '    End If

    'End Function
    'Private Function fn_PrintStrukNCC(ByVal sCode As String) As Boolean
    '    If txtKDSO.Text = String.Empty Then Exit Function

    '    Dim oConn As New SqlConnection
    '    Dim oComm As New SqlCommand
    '    Dim da As SqlDataAdapter
    '    Dim ds As New DataSet
    '    Dim SQL As String

    '    Dim sConn As String = iPOS.GLB.Globals.Decrypt(My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Software\BUDIJAYAGROUP\BPJS\" & sDatabase & "\", "Database", "").ToString())

    '    oConn = New SqlConnection(sConn)
    '    If oConn.State = ConnectionState.Closed Then
    '        oConn.Open()
    '    End If


    '    SQL = "SELECT A.KDTRANSAKSILAB, A.KDBILLING, jumlah = B.GRANDTOTAL, C.TARIFKT "
    '    SQL &= ",KODE = (SELECT AA.KODE FROM konektor_cbg_inap AS AA WHERE C.TARIFKT + ' ' + D.DESCRIPTION = AA.TARIFKT)   "
    '    SQL &= "FROM  "
    '    SQL &= "S_TRANSAKSILAB_H AS A "
    '    SQL &= "INNER JOIN S_TRANSAKSILAB_D AS B "
    '    SQL &= "ON A.KDTRANSAKSILAB = B.KDTRANSAKSILAB "
    '    SQL &= "INNER JOIN M_TARIFRS AS C "
    '    SQL &= "ON B.KDITEMTARIF = C.KDITEMTARIF "
    '    SQL &= "INNER JOIN M_KODETARIF AS D "
    '    SQL &= "ON B.KODEUNITTARIF = D.KODEUNITTARIF "
    '    SQL &= "WHERE A.KDREG = '" & txtKDSO.Text & "' OR A.KDREG = '" & txtNotaRawatJalan.Text & "' "

    '    'SQL &= ",NAMADOKTER = (SELECT CASE WHEN A.KDDOCTOR IS NULL THEN '-' ELSE (SELECT NAME_DISPLAY FROM M_DOCTOR WHERE KDDOCTOR = A.KDDOCTOR ) END ) "

    '    oComm.Connection = oConn
    '    oComm.CommandText = SQL
    '    oComm.CommandTimeout = 120
    '    oComm.CommandType = CommandType.Text

    '    da = New SqlDataAdapter(oComm)
    '    da.Fill(ds, "S_PENDAFTARAN_D")

    '    Dim TarifNonPBedah As Integer = 0
    '    Dim TarifPBedah As Integer = 0
    '    Dim TarifKonsultasi As Integer = 0
    '    Dim TarifTenagaAhli As Integer = 0
    '    Dim TarifKeperawatan As Integer = 0
    '    Dim TarifPenunjang As Integer = 0
    '    Dim TarifRadiologi As Integer = 0
    '    Dim TarifLaboratorium As Integer = 0
    '    Dim TarifPelayananDarah As Integer = 0
    '    Dim TarifRehabilitas As Integer = 0
    '    Dim TarifKamarAkomodasi As Integer = 0
    '    Dim TarifRawatInsentif As Integer = 0
    '    Dim TarifBMHP As Integer = 0
    '    Dim TarifAlatMedis As Integer = 0
    '    Dim TarifPoliEksekutif As Integer = 0
    '    Dim TarifRumahSakit As Integer = 0
    '    Dim TarifObat As Integer = 0
    '    Dim TarifAlkes As Integer = 0


    '    For xloop As Integer = 0 To ds.Tables("S_PENDAFTARAN_D").Rows.Count - 1
    '        If IsDBNull(ds.Tables("S_PENDAFTARAN_D").Rows(xloop)("KODE")) And ds.Tables("S_PENDAFTARAN_D").Rows(xloop)("KDBILLING") = 2 Then
    '            TarifRadiologi += ds.Tables("S_PENDAFTARAN_D").Rows(xloop)("jumlah")
    '        ElseIf IsDBNull(ds.Tables("S_PENDAFTARAN_D").Rows(xloop)("KODE")) And ds.Tables("S_PENDAFTARAN_D").Rows(xloop)("KDBILLING") = 3 Then
    '            TarifLaboratorium += ds.Tables("S_PENDAFTARAN_D").Rows(xloop)("jumlah")
    '        ElseIf IsDBNull(ds.Tables("S_PENDAFTARAN_D").Rows(xloop)("KODE")) And ds.Tables("S_PENDAFTARAN_D").Rows(xloop)("KDBILLING") = 6 Then
    '            TarifLaboratorium += ds.Tables("S_PENDAFTARAN_D").Rows(xloop)("jumlah")
    '        ElseIf IsDBNull(ds.Tables("S_PENDAFTARAN_D").Rows(xloop)("KODE")) And ds.Tables("S_PENDAFTARAN_D").Rows(xloop)("KDBILLING") = 4 Then
    '            TarifBMHP += ds.Tables("S_PENDAFTARAN_D").Rows(xloop)("jumlah")
    '        ElseIf IsDBNull(ds.Tables("S_PENDAFTARAN_D").Rows(xloop)("KODE")) And ds.Tables("S_PENDAFTARAN_D").Rows(xloop)("KDBILLING") = 1 Then
    '            'TarifBMHP += ds.Tables("S_PENDAFTARAN_D").Rows(xloop)("jumlah")
    '            MsgBox("Nama Tarif " & ds.Tables("S_PENDAFTARAN_D").Rows(xloop)("TARIFKT") & " Belum dikelompokkan Ke Kelompok Tarif E-Klaim")
    '            Exit Function
    '        Else
    '            If ds.Tables("S_PENDAFTARAN_D").Rows(xloop)("KODE") = "001" Then
    '                TarifNonPBedah += ds.Tables("S_PENDAFTARAN_D").Rows(xloop)("jumlah")
    '            ElseIf ds.Tables("S_PENDAFTARAN_D").Rows(xloop)("KODE") = "002" Then
    '                TarifPBedah += ds.Tables("S_PENDAFTARAN_D").Rows(xloop)("jumlah")
    '            ElseIf ds.Tables("S_PENDAFTARAN_D").Rows(xloop)("KODE") = "003" Then
    '                TarifKonsultasi += ds.Tables("S_PENDAFTARAN_D").Rows(xloop)("jumlah")
    '            ElseIf ds.Tables("S_PENDAFTARAN_D").Rows(xloop)("KODE") = "004" Then
    '                TarifTenagaAhli += ds.Tables("S_PENDAFTARAN_D").Rows(xloop)("jumlah")
    '            ElseIf ds.Tables("S_PENDAFTARAN_D").Rows(xloop)("KODE") = "005" Then
    '                TarifKeperawatan += ds.Tables("S_PENDAFTARAN_D").Rows(xloop)("jumlah")
    '            ElseIf ds.Tables("S_PENDAFTARAN_D").Rows(xloop)("KODE") = "006" Then
    '                TarifPenunjang += ds.Tables("S_PENDAFTARAN_D").Rows(xloop)("jumlah")
    '            ElseIf ds.Tables("S_PENDAFTARAN_D").Rows(xloop)("KODE") = "007" Then
    '                TarifRadiologi += ds.Tables("S_PENDAFTARAN_D").Rows(xloop)("jumlah")
    '            ElseIf ds.Tables("S_PENDAFTARAN_D").Rows(xloop)("KODE") = "008" Then
    '                TarifLaboratorium += ds.Tables("S_PENDAFTARAN_D").Rows(xloop)("jumlah")
    '            ElseIf ds.Tables("S_PENDAFTARAN_D").Rows(xloop)("KODE") = "009" Then
    '                TarifPelayananDarah += ds.Tables("S_PENDAFTARAN_D").Rows(xloop)("jumlah")
    '            ElseIf ds.Tables("S_PENDAFTARAN_D").Rows(xloop)("KODE") = "010" Then
    '                TarifRehabilitas += ds.Tables("S_PENDAFTARAN_D").Rows(xloop)("jumlah")
    '            ElseIf ds.Tables("S_PENDAFTARAN_D").Rows(xloop)("KODE") = "011" Then
    '                TarifKamarAkomodasi += ds.Tables("S_PENDAFTARAN_D").Rows(xloop)("jumlah")
    '            ElseIf ds.Tables("S_PENDAFTARAN_D").Rows(xloop)("KODE") = "012" Then
    '                TarifRawatInsentif += ds.Tables("S_PENDAFTARAN_D").Rows(xloop)("jumlah")
    '            ElseIf ds.Tables("S_PENDAFTARAN_D").Rows(xloop)("KODE") = "013" Then
    '                'TarifObat += ds.Tables("S_PENDAFTARAN_D").Rows(xloop)("jumlah")
    '            ElseIf ds.Tables("S_PENDAFTARAN_D").Rows(xloop)("KODE") = "014" Then
    '                TarifAlkes += ds.Tables("S_PENDAFTARAN_D").Rows(xloop)("jumlah")
    '            ElseIf ds.Tables("S_PENDAFTARAN_D").Rows(xloop)("KODE") = "015" Then
    '                TarifBMHP += ds.Tables("S_PENDAFTARAN_D").Rows(xloop)("jumlah")
    '            ElseIf ds.Tables("S_PENDAFTARAN_D").Rows(xloop)("KODE") = "016" Then
    '                TarifAlatMedis += ds.Tables("S_PENDAFTARAN_D").Rows(xloop)("jumlah")
    '            ElseIf ds.Tables("S_PENDAFTARAN_D").Rows(xloop)("KODE") = "017" Then
    '                TarifPoliEksekutif += ds.Tables("S_PENDAFTARAN_D").Rows(xloop)("jumlah")

    '            End If
    '        End If

    '    Next

    '    Dim sTanggalLahir As DateTime = Now
    '    Dim sTanggal1 As DateTime = Now
    '    Dim sTanggal2 As DateTime = Now

    '    sTanggalLahir = oPendaftaran.GetData(txtKDSO.Text).M_CUSTOMER.TANGGALLAHIR
    '    sTanggal1 = grv.GetFocusedRowCellValue("TanggalDatang")
    '    sTanggal2 = grv.GetFocusedRowCellValue("TanggalPulang")

    '    HitungUmur(sTanggalLahir)

    '    LOS = String.Empty

    '    LOS = DateDiff(DateInterval.Day, sTanggal1, sTanggal2) + 1

    '    SQL = "Select "
    '    SQL &= "TOTALOBAT = SUM(A.GRANDTOTAL)"
    '    SQL &= "FROM S_SO_D As A "
    '    SQL &= "INNER JOIN M_ITEM As B "
    '    SQL &= "On A.KDITEM = b.KDITEM "
    '    SQL &= "INNER JOIN S_SO_H As C "
    '    SQL &= "On A.KDSO = C.KDSO "
    '    If grv.GetFocusedRowCellValue("NoRegister2") <> String.Empty Then
    '        SQL &= "WHERE A.KDREG = '" & grv.GetFocusedRowCellValue("NoRegister2") & "' AND A.DUATIGA = 0 AND A.ALKES = 0 AND A.GRANDTOTAL <> 0 AND C.DESCRIPTION <> 'DIRGANTARA INDONESIA' "
    '    Else
    '        SQL &= "WHERE A.KDREG = 'KOSONG' AND A.DUATIGA = 0 AND A.ALKES = 0 AND A.GRANDTOTAL <> 0 AND C.DESCRIPTION <> 'DIRGANTARA INDONESIA' "
    '    End If
    '    SQL &= "GROUP BY A.KDREG "

    '    oComm.Connection = oConn
    '    oComm.CommandText = SQL
    '    oComm.CommandTimeout = 120
    '    oComm.CommandType = CommandType.Text

    '    da = New SqlDataAdapter(oComm)
    '    da.Fill(ds, "R_OBAT")

    '    For xloop As Integer = 0 To ds.Tables("R_OBAT").Rows.Count - 1
    '        TarifObat = ds.Tables("R_OBAT").Rows(xloop)("TOTALOBAT")
    '    Next

    '    SQL = "SELECT  "
    '    SQL &= "TOTALALKES = SUM(A.GRANDTOTAL)"
    '    SQL &= "FROM S_SO_D AS A "
    '    SQL &= "INNER JOIN M_ITEM AS B "
    '    SQL &= "ON A.KDITEM = B.KDITEM "
    '    SQL &= "INNER JOIN S_SO_H AS C "
    '    SQL &= "ON A.KDSO = C.KDSO "
    '    If grv.GetFocusedRowCellValue("NoRegister2") <> String.Empty Then
    '        SQL &= "WHERE A.KDREG = '" & grv.GetFocusedRowCellValue("NoRegister2") & "' AND A.DUATIGA = 0 AND A.ALKES = 1 AND A.GRANDTOTAL <> 0 AND C.DESCRIPTION <> 'DIRGANTARA INDONESIA' "
    '    Else
    '        SQL &= "WHERE A.KDREG = 'KOSONG' AND A.DUATIGA = 0 AND A.ALKES = 1 AND A.GRANDTOTAL <> 0 AND C.DESCRIPTION <> 'DIRGANTARA INDONESIA' "
    '    End If
    '    SQL &= "GROUP BY A.KDREG "

    '    oComm.Connection = oConn
    '    oComm.CommandText = SQL
    '    oComm.CommandTimeout = 120
    '    oComm.CommandType = CommandType.Text

    '    da = New SqlDataAdapter(oComm)
    '    da.Fill(ds, "R_ALKES")

    '    For xloop As Integer = 0 To ds.Tables("R_ALKES").Rows.Count - 1
    '        TarifAlkes += ds.Tables("R_ALKES").Rows(xloop)("TOTALALKES")
    '    Next


    '    Dim listNCC As New List(Of DA.dcEntity.R_NCCDUSTIRA)
    '    Dim dsPendaftaranH As New DA.dcEntity.R_NCCDUSTIRA

    '    TarifRumahSakit = TarifNonPBedah + TarifPBedah + TarifKonsultasi + TarifTenagaAhli _
    '        + TarifKeperawatan + TarifPenunjang + TarifRadiologi + TarifLaboratorium _
    '        + TarifPelayananDarah + TarifRehabilitas + TarifKamarAkomodasi + TarifRawatInsentif _
    '        + TarifBMHP + TarifAlatMedis + TarifPoliEksekutif + TarifObat + TarifAlkes


    '    dsPendaftaranH.KDREGISTER = txtKDSO.Text
    '    dsPendaftaranH.NORM = grv.GetFocusedRowCellValue("NoRM")
    '    dsPendaftaranH.NAMAPASIEN = grv.GetFocusedRowCellValue("NamaPasien")
    '    dsPendaftaranH.JENISKELAMIN = oPendaftaran.GetData(txtKDSO.Text).MASTER_PASIEN.jnskelamin
    '    dsPendaftaranH.TANGGALLAHIR = oPendaftaran.GetData(txtKDSO.Text).MASTER_PASIEN.tgllahir
    '    dsPendaftaranH.CARABAYAR = "JKN"
    '    dsPendaftaranH.NOKARTUBPJS = oPendaftaran.GetData(txtKDSO.Text).MASTER_PASIEN.noaskes
    '    dsPendaftaranH.NOMORSEP = grv.GetFocusedRowCellValue("NOMORSEP")
    '    dsPendaftaranH.COB = ""
    '    dsPendaftaranH.JENISRAWAT = oPendaftaran.GetData(txtKDSO.Text).CATEGORY
    '    dsPendaftaranH.KELASRAWAT = oPendaftaran.GetData(txtKDSO.Text).M_KELASRAWAT.DESCRIPTION
    '    dsPendaftaranH.TANGGALRAWAT = oPendaftaran.GetData(txtKDSO.Text).DATE
    '    dsPendaftaranH.TANGGALPULANG = oPendaftaran.GetData(txtKDSO.Text).DATEPULANG

    '    dsPendaftaranH.UMUR = UMUR
    '    dsPendaftaranH.LOS = LOS
    '    dsPendaftaranH.BERATLAHIR = "0"
    '    dsPendaftaranH.ADLSCORE = 0
    '    dsPendaftaranH.CARAPULANG = ""
    '    dsPendaftaranH.DPJP = oPendaftaran.GetData(txtKDSO.Text).M_DOCTOR.NAME_DISPLAY
    '    dsPendaftaranH.TARIFRUMAHSAKIT = TarifRumahSakit
    '    dsPendaftaranH.JENISTARIF = "Tarif RS Kelas B Pemerintah"

    '    dsPendaftaranH.PNONBEDAH = TarifNonPBedah
    '    dsPendaftaranH.PBEDAH = TarifPBedah
    '    dsPendaftaranH.KONSULTASI = TarifKonsultasi
    '    dsPendaftaranH.TENAGAAHLI = TarifTenagaAhli
    '    dsPendaftaranH.KEPERAWATAN = TarifKeperawatan
    '    dsPendaftaranH.PENUNJANG = TarifPenunjang
    '    dsPendaftaranH.RADIOLOGI = TarifRadiologi
    '    dsPendaftaranH.LABORATORIUM = TarifLaboratorium
    '    dsPendaftaranH.PELAYANANDARAH = TarifPelayananDarah
    '    dsPendaftaranH.REHABILITASI = TarifRehabilitas
    '    dsPendaftaranH.AKOMODASI = TarifKamarAkomodasi
    '    dsPendaftaranH.RAWAINSENTIF = TarifPoliEksekutif
    '    dsPendaftaranH.OBAT = TarifObat
    '    dsPendaftaranH.ALKES = TarifAlkes
    '    dsPendaftaranH.BMHP = TarifBMHP
    '    dsPendaftaranH.SEWAALAT = TarifAlatMedis
    '    dsPendaftaranH.TOTALTARIF = TarifRumahSakit

    '    Dim ADMINISTRASI As Integer = 0
    '    ADMINISTRASI = oPendaftaran.GetData(txtKDSO.Text).ADMINISTRASI

    '    If ADMINISTRASI = 0 Then
    '        dsPendaftaranH.CARAPULANG = "-".ToString.ToUpper
    '    ElseIf ADMINISTRASI = 1 Then
    '        dsPendaftaranH.CARAPULANG = "Atas persetujuan dokter".ToString.ToUpper
    '    ElseIf ADMINISTRASI = 2 Then
    '        dsPendaftaranH.CARAPULANG = "Dirujuk".ToString.ToUpper
    '    ElseIf ADMINISTRASI = 3 Then
    '        dsPendaftaranH.CARAPULANG = "Atas permintaan sendiri".ToString.ToUpper
    '    ElseIf ADMINISTRASI = 4 Then
    '        dsPendaftaranH.CARAPULANG = "Meninggal".ToString.ToUpper
    '    ElseIf ADMINISTRASI = 5 Then
    '        dsPendaftaranH.CARAPULANG = "Lain-Lain".ToString.ToUpper
    '    End If

    '    listNCC.Add(dsPendaftaranH)

    '    Dim rpt As New xtraNCCDUSTIRA

    '    rpt.bindingSource.DataSource = listNCC

    '    Dim printTool As New DevExpress.XtraReports.UI.ReportPrintTool(rpt)
    '    printTool.ShowPreviewDialog()

    '    If oConn.State = ConnectionState.Open Then
    '        oConn.Close()
    '    End If

    'End Function
    'Private Function fn_PrintStrukNCCGabungBayi(ByVal sCode As String) As Boolean
    '    If txtKDSO.Text = String.Empty Then Exit Function

    '    Dim sCOde1 As String = String.Empty
    '    Dim sCOde2 As String = String.Empty
    '    Dim sCOde3 As String = String.Empty
    '    Dim sCOde4 As String = String.Empty
    '    Dim sCOde5 As String = String.Empty


    '    Dim oConn As New SqlConnection
    '    Dim oComm As New SqlCommand
    '    Dim da As SqlDataAdapter
    '    Dim ds As New DataSet
    '    Dim SQL As String

    '    Dim sConn As String = iPOS.GLB.Globals.Decrypt(My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Software\BUDIJAYAGROUP\BPJS\" & sDatabase & "\", "Database", "").ToString())

    '    oConn = New SqlConnection(sConn)
    '    If oConn.State = ConnectionState.Closed Then
    '        oConn.Open()
    '    End If

    '    sCOde1 = txtKDSO.Text

    '    Try
    '        sCOde5 = oPendaftaran.GetData(txtKDSO.Text).KDREGAWAL
    '    Catch ex As Exception
    '        sCOde5 = String.Empty
    '        MsgBox("Kode Reg Awal Tidak Ditemukan Tidak dapat terhubung dengan Farmasi", MsgBoxStyle.Exclamation, Me.Text)
    '    End Try

    '    sCOde2 = txtNotaRawatJalan.Text

    '    Dim oTransaksi As New Sales.clsTransaksi

    '    Try
    '        Dim RuangGabung As String = String.Empty
    '        sCOde3 = oPendaftaran.GetDataByPERINA(grv.GetFocusedRowCellValue("NOMORSEP")).KDREG

    '        Try
    '            sCOde4 = oTransaksi.GetDataByKDREG(sCOde3).DESCRIPTION
    '        Catch ex As Exception
    '            sCOde4 = String.Empty
    '        End Try

    '    Catch ex As Exception
    '        sCOde3 = String.Empty
    '        sCOde4 = String.Empty
    '    End Try


    '    SQL = "SELECT A.KDTRANSAKSILAB, A.KDBILLING, jumlah = B.GRANDTOTAL, C.TARIFKT "
    '    SQL &= ",KODE = (SELECT AA.KODE FROM konektor_cbg_inap AS AA WHERE C.TARIFKT + ' ' + D.DESCRIPTION = AA.TARIFKT)   "
    '    SQL &= "FROM  "
    '    SQL &= "S_TRANSAKSILAB_H AS A "
    '    SQL &= "INNER JOIN S_TRANSAKSILAB_D AS B "
    '    SQL &= "ON A.KDTRANSAKSILAB = B.KDTRANSAKSILAB "
    '    SQL &= "INNER JOIN M_TARIFRS AS C "
    '    SQL &= "ON B.KDITEMTARIF = C.KDITEMTARIF "
    '    SQL &= "INNER JOIN M_KODETARIF AS D "
    '    SQL &= "ON B.KODEUNITTARIF = D.KODEUNITTARIF "
    '    SQL &= "WHERE A.KDREG = '" & sCOde1 & "' OR A.KDREG = '" & sCOde2 & "' OR A.KDREG = '" & sCOde3 & "' OR A.KDREG = '" & sCOde4 & "' "

    '    'SQL &= ",NAMADOKTER = (SELECT CASE WHEN A.KDDOCTOR IS NULL THEN '-' ELSE (SELECT NAME_DISPLAY FROM M_DOCTOR WHERE KDDOCTOR = A.KDDOCTOR ) END ) "

    '    oComm.Connection = oConn
    '    oComm.CommandText = SQL
    '    oComm.CommandTimeout = 120
    '    oComm.CommandType = CommandType.Text

    '    da = New SqlDataAdapter(oComm)
    '    da.Fill(ds, "S_PENDAFTARAN_D")

    '    Dim TarifNonPBedah As Integer = 0
    '    Dim TarifPBedah As Integer = 0
    '    Dim TarifKonsultasi As Integer = 0
    '    Dim TarifTenagaAhli As Integer = 0
    '    Dim TarifKeperawatan As Integer = 0
    '    Dim TarifPenunjang As Integer = 0
    '    Dim TarifRadiologi As Integer = 0
    '    Dim TarifLaboratorium As Integer = 0
    '    Dim TarifPelayananDarah As Integer = 0
    '    Dim TarifRehabilitas As Integer = 0
    '    Dim TarifKamarAkomodasi As Integer = 0
    '    Dim TarifRawatInsentif As Integer = 0
    '    Dim TarifBMHP As Integer = 0
    '    Dim TarifAlatMedis As Integer = 0
    '    Dim TarifPoliEksekutif As Integer = 0
    '    Dim TarifRumahSakit As Integer = 0
    '    Dim TarifObat As Integer = 0
    '    Dim TarifAlkes As Integer = 0


    '    For xloop As Integer = 0 To ds.Tables("S_PENDAFTARAN_D").Rows.Count - 1
    '        If IsDBNull(ds.Tables("S_PENDAFTARAN_D").Rows(xloop)("KODE")) And ds.Tables("S_PENDAFTARAN_D").Rows(xloop)("KDBILLING") = 2 Then
    '            TarifRadiologi += ds.Tables("S_PENDAFTARAN_D").Rows(xloop)("jumlah")
    '        ElseIf IsDBNull(ds.Tables("S_PENDAFTARAN_D").Rows(xloop)("KODE")) And ds.Tables("S_PENDAFTARAN_D").Rows(xloop)("KDBILLING") = 3 Then
    '            TarifLaboratorium += ds.Tables("S_PENDAFTARAN_D").Rows(xloop)("jumlah")
    '        ElseIf IsDBNull(ds.Tables("S_PENDAFTARAN_D").Rows(xloop)("KODE")) And ds.Tables("S_PENDAFTARAN_D").Rows(xloop)("KDBILLING") = 4 Then
    '            TarifBMHP += ds.Tables("S_PENDAFTARAN_D").Rows(xloop)("jumlah")
    '        ElseIf IsDBNull(ds.Tables("S_PENDAFTARAN_D").Rows(xloop)("KODE")) And ds.Tables("S_PENDAFTARAN_D").Rows(xloop)("KDBILLING") = 1 Then
    '            'TarifBMHP += ds.Tables("S_PENDAFTARAN_D").Rows(xloop)("jumlah")
    '            MsgBox("Nama Tarif " & ds.Tables("S_PENDAFTARAN_D").Rows(xloop)("TARIFKT") & " Belum dikelompokkan Ke Kelompok Tarif E-Klaim")
    '            Exit Function
    '        Else
    '            If ds.Tables("S_PENDAFTARAN_D").Rows(xloop)("KODE") = "001" Then
    '                TarifNonPBedah += ds.Tables("S_PENDAFTARAN_D").Rows(xloop)("jumlah")
    '            ElseIf ds.Tables("S_PENDAFTARAN_D").Rows(xloop)("KODE") = "002" Then
    '                TarifPBedah += ds.Tables("S_PENDAFTARAN_D").Rows(xloop)("jumlah")
    '            ElseIf ds.Tables("S_PENDAFTARAN_D").Rows(xloop)("KODE") = "003" Then
    '                TarifKonsultasi += ds.Tables("S_PENDAFTARAN_D").Rows(xloop)("jumlah")
    '            ElseIf ds.Tables("S_PENDAFTARAN_D").Rows(xloop)("KODE") = "004" Then
    '                TarifTenagaAhli += ds.Tables("S_PENDAFTARAN_D").Rows(xloop)("jumlah")
    '            ElseIf ds.Tables("S_PENDAFTARAN_D").Rows(xloop)("KODE") = "005" Then
    '                TarifKeperawatan += ds.Tables("S_PENDAFTARAN_D").Rows(xloop)("jumlah")
    '            ElseIf ds.Tables("S_PENDAFTARAN_D").Rows(xloop)("KODE") = "006" Then
    '                TarifPenunjang += ds.Tables("S_PENDAFTARAN_D").Rows(xloop)("jumlah")
    '            ElseIf ds.Tables("S_PENDAFTARAN_D").Rows(xloop)("KODE") = "007" Then
    '                TarifRadiologi += ds.Tables("S_PENDAFTARAN_D").Rows(xloop)("jumlah")
    '            ElseIf ds.Tables("S_PENDAFTARAN_D").Rows(xloop)("KODE") = "008" Then
    '                TarifLaboratorium += ds.Tables("S_PENDAFTARAN_D").Rows(xloop)("jumlah")
    '            ElseIf ds.Tables("S_PENDAFTARAN_D").Rows(xloop)("KODE") = "009" Then
    '                TarifPelayananDarah += ds.Tables("S_PENDAFTARAN_D").Rows(xloop)("jumlah")
    '            ElseIf ds.Tables("S_PENDAFTARAN_D").Rows(xloop)("KODE") = "010" Then
    '                TarifRehabilitas += ds.Tables("S_PENDAFTARAN_D").Rows(xloop)("jumlah")
    '            ElseIf ds.Tables("S_PENDAFTARAN_D").Rows(xloop)("KODE") = "011" Then
    '                TarifKamarAkomodasi += ds.Tables("S_PENDAFTARAN_D").Rows(xloop)("jumlah")
    '            ElseIf ds.Tables("S_PENDAFTARAN_D").Rows(xloop)("KODE") = "012" Then
    '                TarifRawatInsentif += ds.Tables("S_PENDAFTARAN_D").Rows(xloop)("jumlah")
    '            ElseIf ds.Tables("S_PENDAFTARAN_D").Rows(xloop)("KODE") = "013" Then
    '                'TarifObat += ds.Tables("S_PENDAFTARAN_D").Rows(xloop)("jumlah")
    '            ElseIf ds.Tables("S_PENDAFTARAN_D").Rows(xloop)("KODE") = "014" Then
    '                TarifAlkes += ds.Tables("S_PENDAFTARAN_D").Rows(xloop)("jumlah")
    '            ElseIf ds.Tables("S_PENDAFTARAN_D").Rows(xloop)("KODE") = "015" Then
    '                TarifBMHP += ds.Tables("S_PENDAFTARAN_D").Rows(xloop)("jumlah")
    '            ElseIf ds.Tables("S_PENDAFTARAN_D").Rows(xloop)("KODE") = "016" Then
    '                TarifAlatMedis += ds.Tables("S_PENDAFTARAN_D").Rows(xloop)("jumlah")
    '            ElseIf ds.Tables("S_PENDAFTARAN_D").Rows(xloop)("KODE") = "017" Then
    '                TarifPoliEksekutif += ds.Tables("S_PENDAFTARAN_D").Rows(xloop)("jumlah")

    '            End If
    '        End If

    '    Next

    '    Dim sTanggalLahir As DateTime = Now
    '    Dim sTanggal1 As DateTime = Now
    '    Dim sTanggal2 As DateTime = Now

    '    sTanggalLahir = oPendaftaran.GetData(txtKDSO.Text).MASTER_PASIEN.tgllahir
    '    sTanggal1 = grv.GetFocusedRowCellValue("TanggalDatang")
    '    sTanggal2 = grv.GetFocusedRowCellValue("TanggalPulang")

    '    HitungUmur(sTanggalLahir)

    '    LOS = String.Empty

    '    LOS = DateDiff(DateInterval.Day, sTanggal1, sTanggal2) + 1

    '    SQL = "Select "
    '    SQL &= "TOTALOBAT = SUM(A.GRANDTOTAL)"
    '    SQL &= "FROM S_SO_D As A "
    '    SQL &= "INNER JOIN M_ITEM As B "
    '    SQL &= "On A.KDITEM = b.KDITEM "
    '    SQL &= "INNER JOIN S_SO_H As C "
    '    SQL &= "On A.KDSO = C.KDSO "
    '    If grv.GetFocusedRowCellValue("NoRegister2") <> String.Empty Then
    '        SQL &= "WHERE A.KDREG = '" & grv.GetFocusedRowCellValue("NoRegister2") & "' AND A.DUATIGA = 0 AND A.ALKES = 0 AND A.GRANDTOTAL <> 0 AND C.DESCRIPTION <> 'DIRGANTARA INDONESIA' "
    '    Else
    '        SQL &= "WHERE A.KDREG = 'KOSONG' AND A.DUATIGA = 0 AND A.ALKES = 0 AND A.GRANDTOTAL <> 0 AND C.DESCRIPTION <> 'DIRGANTARA INDONESIA' "
    '    End If
    '    SQL &= "GROUP BY A.KDREG "

    '    oComm.Connection = oConn
    '    oComm.CommandText = SQL
    '    oComm.CommandTimeout = 120
    '    oComm.CommandType = CommandType.Text

    '    da = New SqlDataAdapter(oComm)
    '    da.Fill(ds, "R_OBAT")

    '    For xloop As Integer = 0 To ds.Tables("R_OBAT").Rows.Count - 1
    '        TarifObat = ds.Tables("R_OBAT").Rows(xloop)("TOTALOBAT")
    '    Next

    '    SQL = "SELECT  "
    '    SQL &= "TOTALALKES = SUM(A.GRANDTOTAL)"
    '    SQL &= "FROM S_SO_D AS A "
    '    SQL &= "INNER JOIN M_ITEM AS B "
    '    SQL &= "ON A.KDITEM = B.KDITEM "
    '    SQL &= "INNER JOIN S_SO_H AS C "
    '    SQL &= "ON A.KDSO = C.KDSO "
    '    If grv.GetFocusedRowCellValue("NoRegister2") <> String.Empty Then
    '        SQL &= "WHERE A.KDREG = '" & grv.GetFocusedRowCellValue("NoRegister2") & "' AND A.DUATIGA = 0 AND A.ALKES = 1 AND A.GRANDTOTAL <> 0 AND C.DESCRIPTION <> 'DIRGANTARA INDONESIA' "
    '    Else
    '        SQL &= "WHERE A.KDREG = 'KOSONG' AND A.DUATIGA = 0 AND A.ALKES = 1 AND A.GRANDTOTAL <> 0 AND C.DESCRIPTION <> 'DIRGANTARA INDONESIA' "
    '    End If
    '    SQL &= "GROUP BY A.KDREG "

    '    oComm.Connection = oConn
    '    oComm.CommandText = SQL
    '    oComm.CommandTimeout = 120
    '    oComm.CommandType = CommandType.Text

    '    da = New SqlDataAdapter(oComm)
    '    da.Fill(ds, "R_ALKES")

    '    For xloop As Integer = 0 To ds.Tables("R_ALKES").Rows.Count - 1
    '        TarifAlkes += ds.Tables("R_ALKES").Rows(xloop)("TOTALALKES")
    '    Next


    '    Dim listNCC As New List(Of DA.dcEntity.R_NCCDUSTIRA)
    '    Dim dsPendaftaranH As New DA.dcEntity.R_NCCDUSTIRA

    '    TarifRumahSakit = TarifNonPBedah + TarifPBedah + TarifKonsultasi + TarifTenagaAhli _
    '        + TarifKeperawatan + TarifPenunjang + TarifRadiologi + TarifLaboratorium _
    '        + TarifPelayananDarah + TarifRehabilitas + TarifKamarAkomodasi + TarifRawatInsentif _
    '        + TarifBMHP + TarifAlatMedis + TarifPoliEksekutif + TarifObat + TarifAlkes


    '    dsPendaftaranH.KDREGISTER = txtKDSO.Text
    '    dsPendaftaranH.NORM = grv.GetFocusedRowCellValue("NoRM")
    '    dsPendaftaranH.NAMAPASIEN = grv.GetFocusedRowCellValue("NamaPasien")
    '    dsPendaftaranH.JENISKELAMIN = oPendaftaran.GetData(txtKDSO.Text).MASTER_PASIEN.jnskelamin
    '    dsPendaftaranH.TANGGALLAHIR = oPendaftaran.GetData(txtKDSO.Text).MASTER_PASIEN.tgllahir
    '    dsPendaftaranH.CARABAYAR = "JKN"
    '    dsPendaftaranH.NOKARTUBPJS = oPendaftaran.GetData(txtKDSO.Text).MASTER_PASIEN.noaskes
    '    dsPendaftaranH.NOMORSEP = grv.GetFocusedRowCellValue("NOMORSEP")
    '    dsPendaftaranH.COB = ""
    '    dsPendaftaranH.JENISRAWAT = oPendaftaran.GetData(txtKDSO.Text).CATEGORY
    '    dsPendaftaranH.KELASRAWAT = oPendaftaran.GetData(txtKDSO.Text).M_KELASRAWAT.DESCRIPTION
    '    dsPendaftaranH.TANGGALRAWAT = oPendaftaran.GetData(txtKDSO.Text).DATE
    '    dsPendaftaranH.TANGGALPULANG = oPendaftaran.GetData(txtKDSO.Text).DATEPULANG

    '    dsPendaftaranH.UMUR = UMUR
    '    dsPendaftaranH.LOS = LOS
    '    dsPendaftaranH.BERATLAHIR = "0"
    '    dsPendaftaranH.ADLSCORE = 0
    '    dsPendaftaranH.CARAPULANG = ""
    '    dsPendaftaranH.DPJP = oPendaftaran.GetData(txtKDSO.Text).M_DOCTOR.NAME_DISPLAY
    '    dsPendaftaranH.TARIFRUMAHSAKIT = TarifRumahSakit
    '    dsPendaftaranH.JENISTARIF = "Tarif RS Kelas B Pemerintah"

    '    dsPendaftaranH.PNONBEDAH = TarifNonPBedah
    '    dsPendaftaranH.PBEDAH = TarifPBedah
    '    dsPendaftaranH.KONSULTASI = TarifKonsultasi
    '    dsPendaftaranH.TENAGAAHLI = TarifTenagaAhli
    '    dsPendaftaranH.KEPERAWATAN = TarifKeperawatan
    '    dsPendaftaranH.PENUNJANG = TarifPenunjang
    '    dsPendaftaranH.RADIOLOGI = TarifRadiologi
    '    dsPendaftaranH.LABORATORIUM = TarifLaboratorium
    '    dsPendaftaranH.PELAYANANDARAH = TarifPelayananDarah
    '    dsPendaftaranH.REHABILITASI = TarifRehabilitas
    '    dsPendaftaranH.AKOMODASI = TarifKamarAkomodasi
    '    dsPendaftaranH.RAWAINSENTIF = TarifPoliEksekutif
    '    dsPendaftaranH.OBAT = TarifObat
    '    dsPendaftaranH.ALKES = TarifAlkes
    '    dsPendaftaranH.BMHP = TarifBMHP
    '    dsPendaftaranH.SEWAALAT = TarifAlatMedis
    '    dsPendaftaranH.TOTALTARIF = TarifRumahSakit

    '    Dim ADMINISTRASI As Integer = 0
    '    ADMINISTRASI = oPendaftaran.GetData(txtKDSO.Text).ADMINISTRASI

    '    If ADMINISTRASI = 0 Then
    '        dsPendaftaranH.CARAPULANG = "-".ToString.ToUpper
    '    ElseIf ADMINISTRASI = 1 Then
    '        dsPendaftaranH.CARAPULANG = "Atas persetujuan dokter".ToString.ToUpper
    '    ElseIf ADMINISTRASI = 2 Then
    '        dsPendaftaranH.CARAPULANG = "Dirujuk".ToString.ToUpper
    '    ElseIf ADMINISTRASI = 3 Then
    '        dsPendaftaranH.CARAPULANG = "Atas permintaan sendiri".ToString.ToUpper
    '    ElseIf ADMINISTRASI = 4 Then
    '        dsPendaftaranH.CARAPULANG = "Meninggal".ToString.ToUpper
    '    ElseIf ADMINISTRASI = 5 Then
    '        dsPendaftaranH.CARAPULANG = "Lain-Lain".ToString.ToUpper
    '    End If

    '    listNCC.Add(dsPendaftaranH)

    '    Dim rpt As New xtraNCCDUSTIRA

    '    rpt.bindingSource.DataSource = listNCC

    '    Dim printTool As New DevExpress.XtraReports.UI.ReportPrintTool(rpt)
    '    printTool.ShowPreviewDialog()

    '    If oConn.State = ConnectionState.Open Then
    '        oConn.Close()
    '    End If

    'End Function
    Private Sub picRefresh_Click() Handles picRefresh.Click
        fn_LoadSecurity()
    End Sub
    Private Sub UpdateToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles UpdateToolStripMenuItem.Click
        picUpdate_Click()
    End Sub
    Private Sub CetakUlangToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CetakUlangToolStripMenuItem.Click
        'If grvBilling.GetFocusedRowCellValue("NoKoding") Is Nothing Then
        '    fn_EmptyMe()
        '    Exit Sub
        'ElseIf grvBilling.GetFocusedRowCellValue("KodeBilling") <> sKodeBillingTranskasi Then
        '    fn_EmptyMe()
        '    MsgBox("Tidak Dapat Cetak " & grvBilling.GetFocusedRowCellValue("NoKoding"), MsgBoxStyle.Information, Me.Text)

        '    Exit Sub
        'End If
        'If sKodeBillingTranskasi = 5 Then
        '    Try
        '        If grvBilling.GetFocusedRowCellValue("NoKoding") Is Nothing Then
        '            fn_EmptyMe()
        '            Exit Sub
        '        End If

        '        Dim oTransaksi As New Sales.clsTransaksi
        '        sNamaPasienLuar = String.Empty

        '        Dim ds = oTransaksi.GetDataDetail(grvBilling.GetFocusedRowCellValue("NoKoding"))
        '        sDepartmentDustira = grvBilling.GetFocusedRowCellValue("Tujuan")


        '        Dim rpt As New xtraPasienLuar1

        '        rpt.bindingSource.DataSource = ds
        '        sNamaPasienLuar = grv.GetFocusedRowCellValue("NamaPasien")
        '        Dim printTool As New DevExpress.XtraReports.UI.ReportPrintTool(rpt)

        '        Try

        '            printTool.PrintDialog()

        '        Catch ex As Exception
        '            printTool.PrintDialog()
        '        End Try

        '    Catch ex As Exception
        '        MsgBox("Load Printer : " & vbCrLf & ex.Message, MsgBoxStyle.Critical, Me.Text)

        '    End Try
        'Else
        '    Try
        '        If grvBilling.GetFocusedRowCellValue("NoKoding") Is Nothing Then
        '            fn_EmptyMe()
        '            Exit Sub
        '        End If

        '        Dim oTransaksi As New Sales.clsTransaksi

        '        Dim ds = oTransaksi.GetDataDetail(grvBilling.GetFocusedRowCellValue("NoKoding"))
        '        sDepartmentDustira = grvBilling.GetFocusedRowCellValue("Tujuan")

        '        Dim Radiologi As Integer = 0
        '        Radiologi = oTransaksi.GetData(grvBilling.GetFocusedRowCellValue("NoKoding")).KDBILLING
        '        If Radiologi <> 2 Then
        '            Dim rpt As New xtraRawatJalan
        '            rpt.bindingSource.DataSource = ds

        '            Dim printTool As New DevExpress.XtraReports.UI.ReportPrintTool(rpt)

        '            Try

        '                printTool.PrintDialog()

        '            Catch ex As Exception
        '                printTool.PrintDialog()
        '            End Try
        '        Else
        '            Dim rpt As New xtraRawatJalanRadiologi
        '            rpt.bindingSource.DataSource = ds

        '            Dim printTool As New DevExpress.XtraReports.UI.ReportPrintTool(rpt)

        '            Try

        '                printTool.PrintDialog()

        '            Catch ex As Exception
        '                printTool.PrintDialog()
        '            End Try
        '        End If

        '    Catch ex As Exception
        '        MsgBox("Load Printer : " & vbCrLf & ex.Message, MsgBoxStyle.Critical, Me.Text)

        '    End Try
        'End If

    End Sub
    Private Sub RincianToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles RincianToolStripMenuItem.Click
        Try
            If grv.GetFocusedRowCellValue("NoRegister") Is Nothing Then Exit Sub

            fn_PrintStruk(grv.GetFocusedRowCellValue("NoRegister"))

        Catch oErr As Exception
            MsgBox("Print Data : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub NCCToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles NCCToolStripMenuItem.Click
        Try
            If grv.GetFocusedRowCellValue("NoRegister") Is Nothing Then Exit Sub

            'fn_PrintStrukNCC(grv.GetFocusedRowCellValue("NoRegister"))

        Catch oErr As Exception
            MsgBox("Print Data : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub CetakGabungRawatJalanToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CetakGabungRawatJalanToolStripMenuItem.Click
        If sCetakRincianAuto = True Then
            sCetakRincianAuto = False
            CetakGabungRawatJalanToolStripMenuItem.Text = "Cetak Auto Rawat Jalan (NO)"
        ElseIf sCetakRincianAuto = False Then
            sCetakRincianAuto = True
            CetakGabungRawatJalanToolStripMenuItem.Text = "Cetak Auto Rawat Jalan (YA)"
        End If

    End Sub
    Public Function HitungUmur(ByVal tanggllahir As Date) As String
        Dim y, m, d As Integer
        d = Now.Day - tanggllahir.Day
        m = Now.Month - tanggllahir.Month
        y = Now.Year - tanggllahir.Year
        If Math.Sign(d) = -1 Then
            d = 30 - Math.Abs(m)
            m -= 1
        End If
        If Math.Sign(m) = -1 Then
            m = 12 - Math.Abs(m)
            y -= 1

        End If

        'UMUR = y & " Tahun, " & m & " bulan, " & d & " hari"
        sUMURKARTU = y & " Tahun, " & m & " bulan, " & d & " hari"

        Return y & " Tahun, " & m & " bulan, " & d & " hari"

    End Function
    Private Sub MenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles picPrint.Click
        If grv.GetFocusedRowCellValue("NoKoding") Is Nothing Then Exit Sub
        mnuStrip1.Show(picPrint.Location.X, picPrint.Location.Y + 125)
    End Sub
    Private Sub btnTambah_Click(sender As Object, e As EventArgs)
        Try
            Dim oConn As New SqlConnection
            Dim oComm As New SqlCommand
            Dim da As SqlDataAdapter
            Dim ds As New DataSet
            Dim SQL As String
            Dim sConn As String = iPOS.GLB.Globals.Decrypt(My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Software\BUDIJAYAGROUP\BPJS\" & sDatabase & "\", "Database", "").ToString())

            oConn = New SqlConnection(sConn)
            If oConn.State = ConnectionState.Closed Then
                oConn.Open()
            End If

            SQL = "SELECT TOP 1 KDREG FROM S_PENDAFTARAN_H "
            SQL &= "WHERE NOPASIEN = '" & grv.GetFocusedRowCellValue("NoRM") & "' AND CATEGORY = 0 "
            'SQL &= "CONVERT(VARCHAR(8), A.DATE, 112) >= '" & grv.GetFocusedRowCellValue("NoRM") & "' "
            SQL &= "ORDER BY DATE DESC "

            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "S_PENDAFTARAN_H1")

            For xloop As Integer = 0 To ds.Tables("S_PENDAFTARAN_H1").Rows.Count - 1
                ' txtNotaRawatJalan.Text = ds.Tables("S_PENDAFTARAN_H1").Rows(xloop)("KDREG")
            Next

            If sKodeBillingTranskasi = 1 Then
                'SQL = "UPDATE S_TRANSAKSILAB_H SET DESCRIPTION = '" & txtNotaRawatJalan.Text.ToString.Trim & "' "
                ' SQL &= "WHERE KDREG = '" & txtKDSO.Text & "' "
                '
                oComm.Connection = oConn
                oComm.CommandText = SQL
                oComm.CommandTimeout = 120
                oComm.CommandType = CommandType.Text

                da = New SqlDataAdapter(oComm)
                da.Fill(ds, "UPDATE")
            End If


            If oConn.State = ConnectionState.Open Then
                oConn.Close()
            End If

        Catch ex As Exception
            MsgBox("Jaringan Error : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)

        End Try
    End Sub
    Private Sub RincianNaikKelasToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles RincianNaikKelasToolStripMenuItem.Click
        fn_NAIKKelasRawat()
    End Sub
    Private Sub fn_NAIKKelasRawat()
        'Try
        '    Dim oConn As New SqlConnection
        '    Dim oComm As New SqlCommand
        '    Dim da As SqlDataAdapter
        '    Dim ds As New DataSet
        '    Dim SQL As String
        '    Dim NaikKELAS As String = String.Empty

        '    Dim sConn As String = iPOS.GLB.Globals.Decrypt(My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Software\BUDIJAYAGROUP\BPJS\" & sDatabase & "\", "Database", "").ToString())

        '    oConn = New SqlConnection(sConn)
        '    If oConn.State = ConnectionState.Closed Then
        '        oConn.Open()
        '    End If

        '    SQL = "SELECT  "
        '    SQL &= "A.RUANG, A.JENIS, A.NOPASIEN, A.KDREG, A.NAMAPASIEN, A.ALAMATPASIEN, A.TANGGALMASUK, A.TANGGALKELUAR, A.KELAS, A.NAIKKELAS, A.DOKTERDPJP "
        '    SQL &= ",A.NAMADOKTERDETIL, A.GROUPTARIF, A.NAMATARIF, QTY = SUM(A.QTY), A.PRICEHAKKELAS, PRICENAIKKELAS = (Select Case When A.PRICENAIKELAS < A.PRICEHAKKELAS Then A.PRICEHAKKELAS Else A.PRICENAIKELAS End) "
        '    SQL &= ",TOTALKELAS = SUM(A.TOTALKELAS) "
        '    SQL &= ",TOTALNAIKKELAS = ((Select Case When A.PRICENAIKELAS < A.PRICEHAKKELAS Then A.PRICEHAKKELAS Else A.PRICENAIKELAS End)) * SUM(A.QTY)"
        '    SQL &= "FROM ( "
        '    SQL &= "Select  "
        '    SQL &= "RUANG = (Select BB.NAME_DISPLAY FROM S_PENDAFTARAN_H As AA INNER JOIN M_DEPARTMENT As BB On AA.KDDEPARTMENT = BB.KDDEPARTMENT WHERE AA.KDREG = '" & txtKDSO.Text & "') "
        '    SQL &= ",JENIS = (SELECT BB.DESCRIPTION FROM S_PENDAFTARAN_H AS AA INNER JOIN M_JENISPESERTA AS BB ON AA.KDPESERTA = BB.KDPESERTA WHERE AA.KDREG = '" & txtKDSO.Text & "') "
        '    SQL &= ",NOPASIEN = (SELECT AA.NOPASIEN FROM S_PENDAFTARAN_H AS AA WHERE AA.KDREG = '" & txtKDSO.Text & "') "
        '    SQL &= ",NAMAPASIEN = (SELECT BB.NAMAPASIEN FROM S_PENDAFTARAN_H AS AA INNER JOIN MASTER_PASIEN AS BB ON AA.NOPASIEN = BB.NOPASIEN WHERE AA.KDREG = '" & txtKDSO.Text & "') "
        '    SQL &= ",ALAMATPASIEN = (SELECT BB.ALM1PASIEN FROM S_PENDAFTARAN_H AS AA INNER JOIN MASTER_PASIEN AS BB ON AA.NOPASIEN = BB.NOPASIEN WHERE AA.KDREG = '" & txtKDSO.Text & "') "
        '    SQL &= ",TANGGALMASUK = E.DATE "
        '    SQL &= ",TANGGALKELUAR = E.DATEPULANG "
        '    SQL &= ",KELAS = (SELECT BB.DESCRIPTION FROM S_PENDAFTARAN_H AS AA INNER JOIN M_KELASRAWAT AS BB ON AA.KDKELASRAWAT = BB.KDKELASRAWAT WHERE AA.KDREG = '" & txtKDSO.Text & "') "
        '    SQL &= ",NAIKKELAS = (SELECT BB.DESCRIPTION FROM S_PENDAFTARAN_H AS AA INNER JOIN M_KELASRAWAT AS BB ON AA.NAIKKELAS = BB.KDKELASRAWAT WHERE AA.KDREG = '" & txtKDSO.Text & "') "
        '    SQL &= ",NAMADOKTERDETIL = (SELECT CASE WHEN A.KDDOCTOR IS NULL THEN '-' ELSE (SELECT NAME_DISPLAY FROM M_DOCTOR WHERE KDDOCTOR = A.KDDOCTOR ) END ) "
        '    SQL &= ",GROUPTARIF = D.DESCRIPTION "
        '    SQL &= ",NAMATARIF = B.TARIFKT "
        '    SQL &= ",QTY = A.QTY "
        '    SQL &= ",PRICEHAKKELAS = A.PRICE "
        '    SQL &= ",TOTALKELAS = A.GRANDTOTAL "
        '    SQL &= ",CATEGORY = (SELECT AA.CATEGORY FROM S_PENDAFTARAN_H AS AA WHERE AA.KDREG = '" & txtKDSO.Text & "') "
        '    SQL &= ",PRICENAIKELAS = (SELECT CASE E.CATEGORY WHEN 1 THEN  "
        '    SQL &= "(SELECT CASE E.NAIKKELAS WHEN 1 THEN B.KLSI WHEN 2 THEN B.KLSII WHEN 3 THEN B.KLSIII WHEN 7 THEN B.VIP WHEN 8 THEN B.UTAMA WHEN NULL THEN 0 WHEN 11 THEN B.TARIF END) "
        '    SQL &= "Else "
        '    SQL &= "0 "
        '    SQL &= "End) "
        '    SQL &= ",DOKTERDPJP = (SELECT AA.DATEPULANG FROM S_PENDAFTARAN_H AS AA INNER JOIN M_DOCTOR AS BB ON AA.KDDOCTOR = BB.KDDOCTOR WHERE AA.KDREG = '" & txtKDSO.Text & "') "
        '    SQL &= ",C.KDREG "
        '    SQL &= "FROM S_TRANSAKSILAB_D A "
        '    SQL &= "INNER JOIN M_TARIFRS AS B "
        '    SQL &= "ON A.KDITEMTARIF = B.KDITEMTARIF "
        '    SQL &= "INNER JOIN S_TRANSAKSILAB_H AS C "
        '    SQL &= "ON A.KDTRANSAKSILAB = C.KDTRANSAKSILAB "
        '    SQL &= "INNER JOIN M_KODETARIF AS D "
        '    SQL &= "ON A.KODEUNITTARIF = D.KODEUNITTARIF "
        '    SQL &= "INNER JOIN S_PENDAFTARAN_H AS E "
        '    SQL &= "ON C.KDREG = E.KDREG "
        '    SQL &= "WHERE C.KDREG = '" & txtKDSO.Text & "' OR C.KDREG = '" & txtNotaRawatJalan.Text & "' "
        '    SQL &= ") AS A "
        '    SQL &= "GROUP BY A.RUANG, A.JENIS, A.NOPASIEN, A.KDREG, A.NAMAPASIEN, A.ALAMATPASIEN, A.TANGGALMASUK, A.TANGGALKELUAR, A.KELAS, A.NAIKKELAS, A.DOKTERDPJP, A.NAMADOKTERDETIL, A.GROUPTARIF, A.NAMATARIF, A.PRICEHAKKELAS, A.PRICENAIKELAS "

        '    oComm.Connection = oConn
        '    oComm.CommandText = SQL
        '    oComm.CommandTimeout = 120
        '    oComm.CommandType = CommandType.Text

        '    da = New SqlDataAdapter(oComm)
        '    da.Fill(ds, "M_NAIKKELAS")

        '    Dim listPendaftaranH As New List(Of DA.dcEntity.R_RINCIAN_NAIKKELA)

        '    For iLoop As Integer = 0 To ds.Tables("M_NAIKKELAS").Rows.Count - 1
        '        Dim dsPendaftaranH As New DA.dcEntity.R_RINCIAN_NAIKKELA
        '        With ds.Tables("M_NAIKKELAS")

        '            dsPendaftaranH.RUANG = .Rows(iLoop)("RUANG").ToString
        '            dsPendaftaranH.JENIS = .Rows(iLoop)("JENIS").ToString
        '            dsPendaftaranH.NOPASIEN = .Rows(iLoop)("NOPASIEN").ToString
        '            dsPendaftaranH.NAMAPASIEN = .Rows(iLoop)("NAMAPASIEN").ToString
        '            dsPendaftaranH.ALAMATPASIEN = .Rows(iLoop)("ALAMATPASIEN").ToString
        '            dsPendaftaranH.KELAS = .Rows(iLoop)("KELAS").ToString
        '            dsPendaftaranH.NAIKKELAS = .Rows(iLoop)("NAIKKELAS").ToString
        '            dsPendaftaranH.DOKTERDPJP = .Rows(iLoop)("DOKTERDPJP").ToString
        '            dsPendaftaranH.TANGGALMASUK = CDate(.Rows(iLoop)("TANGGALMASUK"))
        '            dsPendaftaranH.TANGGALKELUAR = CDate(.Rows(iLoop)("TANGGALKELUAR"))
        '            dsPendaftaranH.KDREG = .Rows(iLoop)("KDREG").ToString
        '            dsPendaftaranH.NAMADOKTERDETIL = .Rows(iLoop)("NAMADOKTERDETIL").ToString
        '            dsPendaftaranH.GROUPTARIF = .Rows(iLoop)("GROUPTARIF").ToString
        '            dsPendaftaranH.NAMATARIF = .Rows(iLoop)("NAMATARIF").ToString
        '            dsPendaftaranH.QTY = .Rows(iLoop)("QTY").ToString
        '            dsPendaftaranH.PRICE = .Rows(iLoop)("PRICEHAKKELAS").ToString
        '            dsPendaftaranH.PRICENAIKKELAS = .Rows(iLoop)("PRICENAIKKELAS").ToString

        '            dsPendaftaranH.TOTALNAIKKELAS = .Rows(iLoop)("TOTALNAIKKELAS").ToString
        '            dsPendaftaranH.TOTALKELAS = .Rows(iLoop)("TOTALKELAS").ToString

        '            listPendaftaranH.Add(dsPendaftaranH)

        '        End With

        '    Next


        '    SQL = "SELECT NAMAOBAT = B.NMITEM2, QTY = SUM(A.QTY), A.PRICE "
        '    SQL &= ",TOTALOBAT = SUM(A.GRANDTOTAL) "
        '    SQL &= "FROM S_SO_D As A "
        '    SQL &= "INNER JOIN M_ITEM As B "
        '    SQL &= "On A.KDITEM = B.KDITEM "
        '    SQL &= "INNER JOIN S_SO_H As C "
        '    SQL &= "On A.KDSO = C.KDSO "
        '    SQL &= "INNER JOIN S_REG_H As D "
        '    SQL &= "On C.KDREG = D.KDREG "
        '    If grv.GetFocusedRowCellValue("NoRegister2") <> String.Empty Then
        '        SQL &= "WHERE A.KDREG = '" & grv.GetFocusedRowCellValue("NoRegister2") & "' AND A.DUATIGA = 0 AND A.ALKES = 0 AND A.GRANDTOTAL <> 0 AND C.DESCRIPTION <> 'DIRGANTARA INDONESIA' "
        '    Else
        '        SQL &= "WHERE A.KDREG = 'KOSONG' AND A.DUATIGA = 0 AND A.ALKES = 0 AND A.GRANDTOTAL <> 0 AND C.DESCRIPTION <> 'DIRGANTARA INDONESIA' "
        '    End If
        '    SQL &= "GROUP BY B.NMITEM2, A.PRICE "

        '    oComm.Connection = oConn
        '    oComm.CommandText = SQL
        '    oComm.CommandTimeout = 120
        '    oComm.CommandType = CommandType.Text

        '    da = New SqlDataAdapter(oComm)
        '    da.Fill(ds, "R_OBAT")


        '    For iLoop As Integer = 0 To ds.Tables("R_OBAT").Rows.Count - 1
        '        Dim dsPendaftaranH As New DA.dcEntity.R_RINCIAN_NAIKKELA
        '        With ds.Tables("R_OBAT")

        '            dsPendaftaranH.TANGGALMASUK = Date.MinValue
        '            dsPendaftaranH.TANGGALKELUAR = Date.MinValue

        '            dsPendaftaranH.RUANG = oPendaftaran.GetData(txtKDSO.Text).M_DEPARTMENT.NAME_DISPLAY
        '            dsPendaftaranH.JENIS = oPendaftaran.GetData(txtKDSO.Text).M_JENISPESERTA.DESCRIPTION
        '            dsPendaftaranH.NOPASIEN = oPendaftaran.GetData(txtKDSO.Text).NOPASIEN
        '            dsPendaftaranH.NAMAPASIEN = oPendaftaran.GetData(txtKDSO.Text).MASTER_PASIEN.namapasien
        '            dsPendaftaranH.ALAMATPASIEN = oPendaftaran.GetData(txtKDSO.Text).MASTER_PASIEN.alm1pasien
        '            dsPendaftaranH.KELAS = oPendaftaran.GetData(txtKDSO.Text).M_KELASRAWAT.DESCRIPTION
        '            dsPendaftaranH.NAIKKELAS = oPendaftaran.GetData(txtKDSO.Text).NAIKKELAS
        '            dsPendaftaranH.DOKTERDPJP = oPendaftaran.GetData(txtKDSO.Text).M_DOCTOR.NAME_DISPLAY

        '            dsPendaftaranH.NAMADOKTERDETIL = ""
        '            dsPendaftaranH.GROUPTARIF = "XX OBAT FARMASI"
        '            dsPendaftaranH.NAMATARIF = .Rows(iLoop)("NAMAOBAT").ToString
        '            dsPendaftaranH.QTY = .Rows(iLoop)("QTY").ToString
        '            dsPendaftaranH.PRICENAIKKELAS = .Rows(iLoop)("PRICE").ToString
        '            dsPendaftaranH.TOTALNAIKKELAS = .Rows(iLoop)("TOTALOBAT").ToString
        '            dsPendaftaranH.TOTALKELAS = .Rows(iLoop)("TOTALOBAT").ToString

        '            listPendaftaranH.Add(dsPendaftaranH)
        '        End With

        '    Next


        '    SQL = "SELECT NAMAOBAT = B.NMITEM2, QTY = SUM(A.QTY), A.PRICE "
        '    SQL &= ",TOTALOBAT = SUM(A.GRANDTOTAL) "
        '    SQL &= "FROM S_SO_D As A "
        '    SQL &= "INNER JOIN M_ITEM As B "
        '    SQL &= "On A.KDITEM = B.KDITEM "
        '    SQL &= "INNER JOIN S_SO_H As C "
        '    SQL &= "On A.KDSO = C.KDSO "
        '    SQL &= "INNER JOIN S_REG_H As D "
        '    SQL &= "On C.KDREG = D.KDREG "
        '    If grv.GetFocusedRowCellValue("NoRegister2") <> String.Empty Then
        '        SQL &= "WHERE A.KDREG = '" & grv.GetFocusedRowCellValue("NoRegister2") & "' AND A.DUATIGA = 0 AND A.ALKES = 1 AND A.GRANDTOTAL <> 0 AND C.DESCRIPTION <> 'DIRGANTARA INDONESIA' "
        '    Else
        '        SQL &= "WHERE A.KDREG = 'KOSONG' AND A.DUATIGA = 0 AND A.ALKES = 1 AND A.GRANDTOTAL <> 0 AND C.DESCRIPTION <> 'DIRGANTARA INDONESIA' "
        '    End If
        '    SQL &= "GROUP BY B.NMITEM2, A.PRICE "

        '    oComm.Connection = oConn
        '    oComm.CommandText = SQL
        '    oComm.CommandTimeout = 120
        '    oComm.CommandType = CommandType.Text

        '    da = New SqlDataAdapter(oComm)
        '    da.Fill(ds, "R_ALKES")

        '    If ds.Tables("R_ALKES").Rows.Count < 1 Then

        '    Else
        '        For iLoop As Integer = 0 To ds.Tables("R_ALKES").Rows.Count - 1
        '            Dim dsPendaftaranH As New DA.dcEntity.R_RINCIAN_NAIKKELA
        '            With ds.Tables("R_ALKES")

        '                dsPendaftaranH.TANGGALMASUK = Date.MinValue
        '                dsPendaftaranH.TANGGALKELUAR = Date.MinValue

        '                dsPendaftaranH.RUANG = oPendaftaran.GetData(txtKDSO.Text).M_DEPARTMENT.NAME_DISPLAY
        '                dsPendaftaranH.JENIS = oPendaftaran.GetData(txtKDSO.Text).M_JENISPESERTA.DESCRIPTION
        '                dsPendaftaranH.NOPASIEN = oPendaftaran.GetData(txtKDSO.Text).NOPASIEN
        '                dsPendaftaranH.NAMAPASIEN = oPendaftaran.GetData(txtKDSO.Text).MASTER_PASIEN.namapasien
        '                dsPendaftaranH.ALAMATPASIEN = oPendaftaran.GetData(txtKDSO.Text).MASTER_PASIEN.alm1pasien
        '                dsPendaftaranH.KELAS = oPendaftaran.GetData(txtKDSO.Text).M_KELASRAWAT.DESCRIPTION
        '                dsPendaftaranH.NAIKKELAS = oPendaftaran.GetData(txtKDSO.Text).NAIKKELAS
        '                dsPendaftaranH.DOKTERDPJP = oPendaftaran.GetData(txtKDSO.Text).M_DOCTOR.NAME_DISPLAY


        '                dsPendaftaranH.NAMADOKTERDETIL = ""
        '                dsPendaftaranH.GROUPTARIF = "ZZ ALKES FARMASI"
        '                dsPendaftaranH.NAMATARIF = .Rows(iLoop)("NAMAOBAT").ToString
        '                dsPendaftaranH.QTY = .Rows(iLoop)("QTY").ToString
        '                dsPendaftaranH.PRICENAIKKELAS = .Rows(iLoop)("PRICE").ToString
        '                dsPendaftaranH.TOTALNAIKKELAS = .Rows(iLoop)("TOTALOBAT").ToString
        '                dsPendaftaranH.TOTALKELAS = .Rows(iLoop)("TOTALOBAT").ToString

        '                listPendaftaranH.Add(dsPendaftaranH)

        '            End With

        '        Next

        '    End If

        '    Dim rpt As New xtraRekapBiayaRawatInapNaikKelasR1

        '    sNota1 = txtKDSO.Text
        '    sNota2 = txtNotaRawatJalan.Text

        '    rpt.bindingSource.DataSource = listPendaftaranH
        '    sPrintJudulRuangan = grv.GetFocusedRowCellValue("Poli")
        '    sTanggalPulang = grv.GetFocusedRowCellValue("TanggalPulang")
        '    sPrintDokterDPJP = grv.GetFocusedRowCellValue("NamaDokter")

        '    Dim printTool As New DevExpress.XtraReports.UI.ReportPrintTool(rpt)
        '    printTool.ShowPreviewDialog()

        '    If oConn.State = ConnectionState.Open Then
        '        oConn.Close()
        '    End If

        'Catch ex As Exception
        '    MsgBox("Pencarian Naik Kelas Bermasalah")
        'End Try
    End Sub
    Private Sub CetakUlangFotoRadiologiToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CetakUlangFotoRadiologiToolStripMenuItem.Click
        fn_PrintStrukFoto(grvBilling.GetFocusedRowCellValue("NoKoding"))
        'fn_PrintStrukFoto(grvBilling.GetFocusedRowCellValue("NoKoding"))

    End Sub
    Private Function fn_PrintStrukFoto(ByVal sCode As String) As Boolean
        'Try
        '    Dim oTransaksi As New Sales.clsTransaksi

        '    Dim ds = oTransaksi.GetDataDetail(sCode)
        '    Dim oMasterPasien As New Master.clsCustomer_L3
        '    Dim TanggalLahir As DateTime = Now

        '    TanggalLahir = oMasterPasien.GetData(grv.GetFocusedRowCellValue("NoRM")).tgllahir

        '    HitungUmur(TanggalLahir)


        '    Dim rpt As New xtraRadiologi

        '    rpt.bindingSource.DataSource = ds

        '    Dim printTool As New DevExpress.XtraReports.UI.ReportPrintTool(rpt)
        '    printTool.Print()

        'Catch ex As Exception
        '    MsgBox("Load Printer Foto: " & vbCrLf & ex.Message, MsgBoxStyle.Critical, Me.Text)

        'End Try
    End Function
    Private Sub RincianToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles RincianToolStripMenuItem1.Click
        Try
            If grv.GetFocusedRowCellValue("NoRegister") Is Nothing Then Exit Sub

            'fn_PrintStrukGabungBayi(grv.GetFocusedRowCellValue("NoRegister"))

        Catch oErr As Exception
            MsgBox("Print Data : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub NCCToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles NCCToolStripMenuItem1.Click
        Try
            If grv.GetFocusedRowCellValue("NoRegister") Is Nothing Then Exit Sub

            'fn_PrintStrukNCCGabungBayi(grv.GetFocusedRowCellValue("NoRegister"))

        Catch oErr As Exception
            MsgBox("Print Data : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub CetakToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CetakToolStripMenuItem.Click
        Try
            If grvBilling.GetFocusedRowCellValue("NoKoding") Is Nothing Then
                fn_EmptyMe()
                Exit Sub
            End If

            Dim dsTandatangan = oPendaftaran.GetDataTandaTangan(grv.GetFocusedRowCellValue("NoRegister"))

            If dsTandatangan IsNot Nothing Then
                DownloadIamge1 = dsTandatangan.ALAMATTANDATANGAN
            Else
                DownloadIamge1 = ""
            End If

            Dim oKoding As New Koding.clsKoding
            Dim ds = oKoding.GetData(grvBilling.GetFocusedRowCellValue("NoKoding"))

            Dim rpt As New xtraKoding

            rpt.bindingSource.DataSource = ds

            KDDOCTOR = ds.S_PENDAFTARAN_H.KDDOCTOR
            sUserIDTandaTangan = ds.NOIDUSER

            Dim printTool As New DevExpress.XtraReports.UI.ReportPrintTool(rpt)
            printTool.ShowPreviewDialog()

        Catch ex As Exception
            MsgBox("Load Printer Foto: " & vbCrLf & ex.Message, MsgBoxStyle.Critical, Me.Text)

        End Try
    End Sub


#End Region

End Class