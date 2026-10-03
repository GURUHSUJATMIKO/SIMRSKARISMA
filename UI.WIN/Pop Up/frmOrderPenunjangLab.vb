Imports DataAccess
Imports UI.WIN.MAIN.My.Resources
Imports System.Linq
Imports System.Data.SqlClient

Public Class frmOrderPenunjangLab
    Private oFormMode As FORM_MODE = FORM_MODE.FORM_MODE_VIEW
    Private oOrder As New Order.clsOrderPenunjang
    Private sKATEGORI As String
    Private sdpjp As String
    Private sKODEAWAL As String = String.Empty
    Private sREGISTER As String = String.Empty
    Private sKDCUSTOMER As String = String.Empty
    Private sNAMA As String = String.Empty
    Private sTUJUAN As String = String.Empty
    Private sUserSimpan As String = String.Empty
#Region "Declaration"

#End Region
#Region "Function"
    Public Sub LoadMe(ByVal usersimpan As String, ByVal DIAGNOSA As String, ByVal KATEGORI As String, ByVal dpjp As String, ByVal BB As String, ByVal TB As String, ByVal kode As String, ByVal KDREG As String, ByVal KDCUSTOMER As String, ByVal NAMA As String, ByVal TUJUAN As String, ByVal FormMode As Integer)
        oFormMode = FormMode
        sKODEAWAL = kode
        sREGISTER = KDREG
        sKDCUSTOMER = KDCUSTOMER
        sNAMA = NAMA
        sTUJUAN = TUJUAN
        sUserSimpan = usersimpan

        sDatePemeriksaan = Now
        sKATEGORI = KATEGORI
        sdpjp = dpjp
        txtBB.Text = BB
        txtTB.Text = TB
        fn_Doctor()
        fn_LoadData()
        txtDIAGNOSA.Text = DIAGNOSA

        If sKATEGORI = "LABORATORIUM" Then
            lblIndikasiMedis.Text = "Indikasi Pemeriksaan Laboratorium : "
        Else
            lblIndikasiMedis.Text = "Indikasi Pemeriksaan Radiologi : "
        End If
    End Sub
    Private Sub Form_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.Text = IIf(sKATEGORI = "LABORATORIUM", "Order Laboratorium", "Order Radiologi")
        btnOrder.Text = IIf(sKATEGORI = "LABORATORIUM", "Order Laboratorium", "Order Radiologi")
    End Sub
    Private Sub Form_Closing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        'sCode = txtKDMUTATION.Text.Trim.ToUpper
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
        btnOrder.Enabled = Not Status
        btnBatal.Enabled = Not Status

        grvDetail.OptionsBehavior.ReadOnly = Status
    End Sub
    Private Sub fn_EmptyMe()
        txtINDIKASIMEDIS.ResetText()
    End Sub
    Private Sub fn_LoadData()
        Try
            If sKATEGORI = "LABORATORIUM" Then
                ' ***** HEADER *****
                BindingSource.DataSource = oOrder.GetDataDetailLisLabAwal(sKODEAWAL).ToList()
                grdDetail.DataSource = BindingSource
            Else
                ' ***** HEADER *****
                BindingSource.DataSource = oOrder.GetDataDetailLisRadAwal(sKODEAWAL).ToList()
                grdDetail.DataSource = BindingSource
            End If
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Function fn_Validate() As Boolean
        Try
            fn_Validate = True

            If txtINDIKASIMEDIS.Text = String.Empty Then
                MsgBox("Di butuhkan Indikasi Medis", MsgBoxStyle.Exclamation, Me.Text)
                fn_Validate = False
                Exit Function
            End If

            grvDetail.UpdateCurrentRow()

            If grvDetail.RowCount < 2 Then
                MsgBox("Order Masih Kosong ", MsgBoxStyle.Exclamation, Me.Text)
                fn_Validate = False
                Exit Function
            End If
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Function
    Private Function fn_SaveSuara() As Boolean
        Try
            '***** DETIL *****
            Dim ds = oOrder.GetStructureSuara
            With ds
                .KODE = Now.ToString("yyyyMMddHHmmss")
                .CATATAN = sKATEGORI
                .NAMAORDER = "ADA PEMERIKSAAN ATAS NAMA " & sNAMA.Replace("'", "") & " DARI RUANGAN " & sTUJUAN
            End With

            fn_SaveSuara = oOrder.InsertDataSuara(ds)

        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            fn_SaveSuara = False
        End Try
    End Function
    Private Function fn_Save() As Boolean
        Try
            Dim oDoctor As New Reference.clsDoctor

            Dim seq As Integer = 0
            If sKATEGORI = "LABORATORIUM" Then
                seq = 0
            Else
                seq = 100
            End If

            '***** DETIL *****
            Dim arrDetail = oOrder.GetStructureDetailAwalList
            For i As Integer = 0 To grvDetail.RowCount - 2
                Dim dsDetail = oOrder.GetStructureDetailAwal
                With dsDetail
                    If oFormMode = FORM_MODE.FORM_MODE_ADD Then
                        .SEQ = seq
                        seq += 1
                    Else
                        .SEQ = IIf(String.IsNullOrEmpty(grvDetail.GetRowCellValue(i, colSEQ)), 0, grvDetail.GetRowCellValue(i, colSEQ))
                    End If
                    .KODE = sKODEAWAL
                    .DATECREATED = Now
                    .DATEUPDATED = Now
                    .DATE = IIf(String.IsNullOrEmpty(grvDetail.GetRowCellValue(i, colDATE)), Now, grvDetail.GetRowCellValue(i, colDATE))
                    .KDCPPT = ""
                    .KDREG = sREGISTER
                    .KDCUSTOMER = sKDCUSTOMER
                    .PASIEN = sNAMA
                    .TUJUAN = sTUJUAN
                    .DOKTER = IIf(String.IsNullOrEmpty(grvDetail.GetRowCellValue(i, colDOKTER)), "", grvDetail.GetRowCellValue(i, colDOKTER))
                    .KATEGORI = sKATEGORI
                    .NAMAORDER = IIf(String.IsNullOrEmpty(grvDetail.GetRowCellValue(i, colNAMAORDER)), "", grvDetail.GetRowCellValue(i, colNAMAORDER))
                    .DIAGNOSA = txtDIAGNOSA.Text
                    .MEMO = sUserSimpan
                    .ISAPPROVE = False
                    .INDIKASIMEDIS = txtINDIKASIMEDIS.Text
                    .TINGGIBADAN = txtTB.Text
                    .BERATBADAN = txtBB.Text

                    Dim namadokter As String = String.Empty
                    Dim dsDoctor = oDoctor.GetData(IIf(String.IsNullOrEmpty(grvDetail.GetRowCellValue(i, colDOKTER)), "", grvDetail.GetRowCellValue(i, colDOKTER)))

                    If dsDoctor IsNot Nothing Then
                        namadokter = dsDoctor.NAME_DISPLAY
                    End If

                    .NAMADOKTER = namadokter
                End With
                arrDetail.Add(dsDetail)
            Next

            If oFormMode = FORM_MODE.FORM_MODE_ADD Then
                fn_Save = oOrder.InsertDataAwal(arrDetail)
            Else
                fn_Save = oOrder.UpdateDataAwal(arrDetail)
            End If

            If fn_Save = True Then
                fn_SaveSuara()
            End If
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            fn_Save = False
        End Try
    End Function
    Private Sub DeleteToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DeleteToolStripMenuItem.Click
        If oFormMode = FORM_MODE.FORM_MODE_VIEW Then Exit Sub
        grvDetail.DeleteSelectedRows()
    End Sub
#End Region
#Region "Grid Method"
    Private Sub grvDetail_CellValueChanged(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs) Handles grvDetail.CellValueChanged
        If e.Column.Name = colNAMAORDER.Name Then
            Try
                If grvDetail.GetFocusedRowCellValue(colNAMAORDER) IsNot Nothing Then
                    grvDetail.SetFocusedRowCellValue(colDATE, sDatePemeriksaan)
                    grvDetail.SetFocusedRowCellValue(colDOKTER, sdpjp)
                End If
            Catch oErr As Exception
                MsgBox("Load Detail : " & vbCrLf & oErr.Message, MsgBoxStyle.Critical, Me.Text)
            End Try
        End If
    End Sub
#End Region
#Region "Command Button"
    Private Sub btnOrder_Click(sender As Object, e As EventArgs) Handles btnOrder.Click
        If fn_Validate() = False Then Exit Sub
        'If MsgBox(Statement.SaveQuestion, MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, Me.Text) = MsgBoxResult.No Then Exit Sub
        If fn_Save() = False Then
            MsgBox(Statement.SaveFail, MsgBoxStyle.Exclamation, Me.Text)
        Else
            'MsgBox(sKDCPPT, MsgBoxStyle.Information, Me.Text)
            Me.Close()
        End If

        'Try
        '    Dim CEK As Boolean = False
        '    Dim Berhasil As Boolean = False

        '    For i As Integer = 0 To grvDetail.RowCount - 2
        '        CEK = True

        '        If sKATEGORI = "LABORATORIUM" Then
        '            listOrderLab.Add(grvDetail.GetRowCellValue(i, colNAMAORDER))
        '        Else
        '            listOrderRad.Add(grvDetail.GetRowCellValue(i, colNAMAORDER))
        '        End If
        '    Next

        '    Dim arrDetailOrderX As New List(Of DataAccess.S_REQ_ORDER_PENUNJANG)

        '    For i As Integer = 0 To grvDetail.RowCount - 2
        '        Dim dsDetail As New DataAccess.S_REQ_ORDER_PENUNJANG
        '        With dsDetail
        '            .SEQ = i
        '            Try
        '                .DATECREATED = oOrder.GetDataDetail(sKDCPPT, i).DATECREATED
        '            Catch oErr As Exception
        '                .DATECREATED = Now
        '            End Try
        '            .DATEUPDATED = Now
        '            .DATE = IIf(String.IsNullOrEmpty(grvDetail.GetRowCellValue(i, colDATE)), Now, grvDetail.GetRowCellValue(i, colDATE))
        '            .KDCPPT = sKDCPPT
        '            .KDREG = ""
        '            .KDCUSTOMER = ""
        '            .PASIEN = ""
        '            .TUJUAN = ""
        '            .DOKTER = fn_Doctor(IIf(String.IsNullOrEmpty(grvDetail.GetRowCellValue(i, colDOKTER)), "", grvDetail.GetRowCellValue(i, colDOKTER)))
        '            .KATEGORI = sKATEGORI
        '            .NAMAORDER = IIf(String.IsNullOrEmpty(grvDetail.GetRowCellValue(i, colNAMAORDER)), "", grvDetail.GetRowCellValue(i, colNAMAORDER))
        '            .DIAGNOSA = ""
        '            .MEMO = ""
        '            .ISAPPROVE = False
        '            .INDIKASIMEDIS = txtINDIKASIMEDIS.Text
        '        End With

        '        arrDetailOrderX.Add(dsDetail)

        '        'arrDetailOrder.Add(dsDetail)

        '        Berhasil = True
        '    Next


        '    'For iLoop As Integer = 0 To ds.Tables("HISTORY").Rows.Count - 1
        '    '    Dim dsRekap As New DataAccess.R_RIWAYAT_DAFTAR
        '    '    With ds.Tables("HISTORY")
        '    '        dsRekap.NomorPendaftaran = .Rows(iLoop)("NomorPendaftaran")
        '    '        dsRekap.Tanggal = .Rows(iLoop)("Tanggal")
        '    '        dsRekap.Tujuan = .Rows(iLoop)("Tujuan")
        '    '        dsRekap.Dokter = .Rows(iLoop)("Dokter")
        '    '        dsRekap.KDKUNJUNGAN = .Rows(iLoop)("KDKUNJUNGAN")
        '    '        dsRekap.NoKontrol = .Rows(iLoop)("NoKontrol")
        '    '        dsRekap.ISDAFTAR = True
        '    '        listPendaftaran.Add(dsRekap)
        '    '    End With
        '    'Next

        '    sIndikasiMedis = txtINDIKASIMEDIS.Text

        '    If CEK = False Then
        '        MsgBox("Order Masih Kosong", MsgBoxStyle.Exclamation, Me.Text)
        '    Else
        '        If Berhasil = True Then
        '            Me.Close()
        '        End If
        '    End If
        'Catch ex As Exception
        '    MsgBox("Load Form Detail Penunjang: " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
        'End Try
    End Sub
    Private Sub btnBatal_Click(sender As Object, e As EventArgs) Handles btnBatal.Click
        'skodeorderlab = String.Empty
        'skodeorderRad = String.Empty
        'listOrderLab.Clear()
        'sIndikasiMedis = String.Empty
        Me.Close()
    End Sub
    Private Sub btnCeklisOrder_Click(sender As Object, e As EventArgs) Handles btnCeklisOrder.Click
        Dim frmReportOrderPenunjang As New frmReportOrderPenunjang
        Try
            frmReportOrderPenunjang.fn_LoadKategori(sKATEGORI)
            frmReportOrderPenunjang.WindowState = FormWindowState.Maximized
            frmReportOrderPenunjang.ShowDialog(Me)
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try

        Try
            For Each xloop In sListRincianLab
                grvDetail.Focus()
                grvDetail.AddNewRow()
                grvDetail.SetFocusedRowCellValue(colDATE, sDatePemeriksaan)
                grvDetail.SetFocusedRowCellValue(colNAMAORDER, xloop)
                grvDetail.UpdateCurrentRow()
            Next

            For Each xloop In sListRincianRad
                grvDetail.Focus()
                grvDetail.AddNewRow()
                grvDetail.SetFocusedRowCellValue(colDATE, sDatePemeriksaan)
                grvDetail.SetFocusedRowCellValue(colNAMAORDER, xloop)
                grvDetail.UpdateCurrentRow()
            Next
        Catch ex As Exception
            MsgBox("Load Form Detail Penunjang: " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
#End Region
#Region "Lookup / Event"
    Private Sub fn_Doctor()
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
            SQL &= "A.KDDOCTOR "
            SQL &= ",NAME_DISPLAY = (SELECT CASE A.FRONT_TITLE WHEN '' THEN '' ELSE A.FRONT_TITLE + ' ' END) + A.NAME_DISPLAY + A.BACK_TITLE "
            SQL &= "FROM  "
            SQL &= "M_DOCTOR A "
            SQL &= "WHERE "
            SQL &= "A.ISACTIVE = '1' "
            SQL &= "AND A.CATEGORY = 1 "
            SQL &= "ORDER BY A.NAME_DISPLAY "

            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "M_DOCTOR")

            grdKDDOCTOR.DataSource = ds.Tables("M_DOCTOR")
            grdKDDOCTOR.ValueMember = "KDDOCTOR"
            grdKDDOCTOR.DisplayMember = "NAME_DISPLAY"

            If oConn.State = ConnectionState.Open Then
                oConn.Close()
            End If

        Catch oErr As Exception
            MsgBox("Load Dokter Data : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Function fn_Doctor(ByVal kddoctor As String) As String
        Try
            fn_Doctor = ""

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
            SQL &= "A.KDDOCTOR = '" & kddoctor & "' "

            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "M_DOCTORGET")

            For iLoop As Integer = 0 To ds.Tables("M_DOCTORGET").Rows.Count - 1
                With ds.Tables("M_DOCTORGET")
                    fn_Doctor = .Rows(iLoop)("NAME_DISPLAY")
                End With
            Next

            If oConn.State = ConnectionState.Open Then
                oConn.Close()
            End If

        Catch oErr As Exception
            fn_Doctor = ""
            MsgBox("Load Dokter Data : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Function
#End Region
End Class