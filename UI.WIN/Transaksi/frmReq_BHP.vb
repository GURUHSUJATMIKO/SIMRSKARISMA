Imports DataAccess
Imports UI.WIN.MAIN.My.Resources
Imports System.Linq
Imports System.Data.SqlClient
Imports Newtonsoft.Json.Linq

Public Class frmReq_BHP
#Region "Declaration"
    Private oFormMode As FORM_MODE = FORM_MODE.FORM_MODE_VIEW
    Private sNoId As String
    Private isLoad As Boolean = False
    Private oReq_BHP As New Transaksi.clsREQ_BHP
    Private sReq As String = String.Empty
    Private sDepartmentAuto As String = String.Empty

#End Region
#Region "Function"
    Public Sub fn_LoadRegister(ByVal sKREQ As String)
        sReq = sKREQ
    End Sub
    Public Sub LoadMe(ByVal FormMode As Integer, ByVal KDREG As String, Optional ByVal NoId As String = "")
        oFormMode = FormMode
        sNoId = NoId

        Dim oPendaftaran As New Admission.clsPendaftaran
        Dim dsPendaftaran = oPendaftaran.GetData(KDREG)

        If dsPendaftaran IsNot Nothing Then
            txtKDREG.Text = dsPendaftaran.KDPENDAFTARAN
            sDepartmentAuto = dsPendaftaran.KDDEPARTMENT
        Else
            fn_EmptyMe()
        End If
    End Sub
    Private Sub Form_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        fn_ChangeFormState()
        isLoad = True
        isSave = False
    End Sub
    Private Sub Form_Closing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        sCode = txtKDREQBHP.Text.Trim.ToUpper
    End Sub
    Private Overloads Sub Dispose()
        MyBase.Dispose()
        Me.Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
    Private Sub fn_ChangeFormState()
        fn_LoadDEPARTMENT()
        fn_LoadDOCTOR()
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
        btnRiwayatFarmasi.Enabled = Not Status
        btnRiwayatResep.Enabled = Not Status
        btnSKD.Enabled = Not Status
        btnAddPenunjang.Enabled = Status

        deDATE.Properties.ReadOnly = Status
        grdKDDEPARTMENT.Properties.ReadOnly = Status
        grdKDDOCTOR.Properties.ReadOnly = Status
        txtTINDAKAN.Properties.ReadOnly = Status
        'chkLAB.Properties.ReadOnly = Status
        txtLAB.Properties.ReadOnly = Status
        ' chkRONTGEN.Properties.ReadOnly = Status
        txtRONTGEN.Properties.ReadOnly = Status
        'chkUSG.Properties.ReadOnly = Status
        txtUSG.Properties.ReadOnly = Status
        'chkLAIN.Properties.ReadOnly = Status
        txtLAIN.Properties.ReadOnly = Status

        grvBillingTambahan.OptionsBehavior.Editable = Not Status
    End Sub
    Private Sub fn_EmptyMe()
        txtKDREQBHP.Text = "<---AUTO--->"
        deDATETANGGALDAFTAR.DateTime = Now
        deDATE.DateTime = Now
        'txtKDREG.ResetText()
        'grdKDDOCTOR.ResetText()
        grdKDDEPARTMENT.EditValue = sDepartmentAuto
        txtTINDAKAN.Text = "-"
        'chkLAB.Checked = False
        txtLAB.ResetText()
        'chkRONTGEN.Checked = False
        txtRONTGEN.ResetText()
        'chkUSG.Checked = False
        txtUSG.ResetText()
        'chkLAIN.Checked = False
        txtLAIN.ResetText()

        If sReq <> "" Then
            Dim ds = oReq_BHP.GetData(sReq)

            With ds
                txtKDREQBHP.Text = .KDREQBHP
                deDATE.DateTime = .DATE
                txtKDREG.Text = .KDPENDAFTARAN
                grdKDDOCTOR.Text = .KDDOCTOR
                grdKDDEPARTMENT.Text = .KDDEPARTMENT
                txtTINDAKAN.Text = .TINDAKAN
                'chkLAB.Checked = .ISLAB
                txtLAB.Text = .LAB
                'chkRONTGEN.Checked = .ISRONTGEN
                txtRONTGEN.Text = .RONTGEN
                'chkUSG.Checked = .ISUSG
                txtUSG.Text = .USG
                'chkLAIN.Checked = .ISLAIN
                txtLAIN.Text = .LAIN

                BindingSource.DataSource = oReq_BHP.GetDataDetail(sReq).OrderBy(Function(x) x.SEQ).ToList()
                grdBillingTambahan.DataSource = BindingSource

            End With

            sReq = String.Empty
        End If
    End Sub
    Private Sub fn_LoadData()
        Try
            ' ***** HEADER *****
            Dim ds = oReq_BHP.GetData(sNoId)

            With ds
                txtKDREQBHP.Text = .KDREQBHP
                deDATE.DateTime = .DATE
                txtKDREG.Text = .KDPENDAFTARAN
                grdKDDOCTOR.Text = .KDDOCTOR
                grdKDDEPARTMENT.Text = .KDDEPARTMENT
                txtTINDAKAN.Text = .TINDAKAN
                'chkLAB.Checked = .ISLAB
                txtLAB.Text = .LAB
                'chkRONTGEN.Checked = .ISRONTGEN
                txtRONTGEN.Text = .RONTGEN
                'chkUSG.Checked = .ISUSG
                txtUSG.Text = .USG
                'chkLAIN.Checked = .ISLAIN
                txtLAIN.Text = .LAIN

                BindingSource.DataSource = oReq_BHP.GetDataDetail(sNoId).OrderBy(Function(x) x.SEQ).ToList()
                grdBillingTambahan.DataSource = BindingSource

            End With
        Catch oErr As Exception
            MsgBox("Load List Data : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Function fn_Validate() As Boolean
        Try
            fn_Validate = True

            If txtKDREG.Text = String.Empty Then
                MsgBox("Dibutuhkan No Register", MsgBoxStyle.Exclamation, Me.Text)
                txtKDREG.Focus()
                fn_Validate = False
                Exit Function
            End If
            If grdKDDOCTOR.Text = String.Empty Then
                MsgBox("Dibutuhkan Operator", MsgBoxStyle.Exclamation, Me.Text)
                grdKDDOCTOR.Focus()
                fn_Validate = False
                Exit Function
            End If
            If grdKDDEPARTMENT.Text = String.Empty Then
                MsgBox("Dibutuhkan Poli/Ruangan", MsgBoxStyle.Exclamation, Me.Text)
                grdKDDOCTOR.Focus()
                fn_Validate = False
                Exit Function
            End If

            grvBillingTambahan.UpdateCurrentRow()

            If grvBillingTambahan.RowCount < 2 Then
                MsgBox("Dibutuhkan Detail Obat", MsgBoxStyle.Exclamation, Me.Text)
                tabControl.SelectedTabPageIndex = 0
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
            Dim ds = oReq_BHP.GetStructureHeader
            With ds
                Try
                    .DATECREATED = oReq_BHP.GetData(sNoId).DATECREATED
                Catch ex As Exception
                    .DATECREATED = Now
                End Try
                .DATEUPDATE = Now
                .KDREQBHP = sNoId
                .KDDOCTOR = grdKDDOCTOR.EditValue
                .KDDEPARTMENT = grdKDDEPARTMENT.EditValue
                .DATE = deDATE.DateTime
                .KDPENDAFTARAN = txtKDREG.Text
                .NOIDUSER = sUserID
                .TINDAKAN = txtTINDAKAN.Text
                .ISLAB = False
                .LAB = txtLAB.Text
                .ISRONTGEN = False
                .RONTGEN = txtRONTGEN.Text
                .ISUSG = False
                .USG = txtUSG.Text
                .ISRONTGEN = False
                .RONTGEN = txtUSG.Text
                .ISLAIN = False
                .LAIN = txtLAIN.Text
                .DESCRIPTION = ""

            End With

            Dim arrDetail = oReq_BHP.GetStructureDetailList
            For i As Integer = 0 To grvBillingTambahan.RowCount - 2
                Dim dsDetail = oReq_BHP.GetStructureDetail
                With dsDetail
                    .SEQ = i
                    .KDREQBHP = ds.KDREQBHP
                    .RINCIAN = grvBillingTambahan.GetRowCellValue(i, colRINCIAN)
                    .QTY = CDec(grvBillingTambahan.GetRowCellValue(i, colQTY))
                    .DATE_PERMINTAAN = CDate(grvBillingTambahan.GetRowCellValue(i, colDATE_PERMINTAAN))
                    .KETERANGAN = IIf(String.IsNullOrEmpty(grvBillingTambahan.GetRowCellValue(i, colKETERANGAN)), "", grvBillingTambahan.GetRowCellValue(i, colKETERANGAN))
                    .KETERANGAN_PENUNJANG = IIf(String.IsNullOrEmpty(grvBillingTambahan.GetRowCellValue(i, colKETERANGAN_PENUNJANG)), "LAIN", grvBillingTambahan.GetRowCellValue(i, colKETERANGAN_PENUNJANG))
                    .GROUP = grvBillingTambahan.GetRowCellValue(i, colGROUP)
                    .ISCHEKED = grvBillingTambahan.GetRowCellValue(i, colISCHEKED)
                    .KDTARIF = grvBillingTambahan.GetRowCellValue(i, colKDTARIF)
                    .ISBACA = grvBillingTambahan.GetRowCellValue(i, colISBACA)
                End With
                arrDetail.Add(dsDetail)
            Next

            If oFormMode = FORM_MODE.FORM_MODE_ADD Then
                Try
                    Dim KDREQBHP As String = oReq_BHP.InsertData(ds, arrDetail)
                    If KDREQBHP <> "" Then
                        fn_Save = True
                        txtKDREQBHP.Text = KDREQBHP
                        isSave = True
                    Else
                        fn_Save = False
                        isSave = False
                    End If
                Catch ex As Exception
                    MsgBox("Simpan Data : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            ElseIf oFormMode = FORM_MODE.FORM_MODE_EDIT Then
                Try
                    fn_Save = oReq_BHP.UpdateData(ds, arrDetail)
                    isSave = True
                Catch ex As Exception
                    isSave = False
                    MsgBox("Simpan Data : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            End If
        Catch oErr As Exception
            isSave = False
            MsgBox("Simpan Data : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            fn_Save = False
        End Try
    End Function
#End Region
#Region "Grid Method"
    Private Sub OnValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles grvBillingTambahan.FocusedRowChanged
        Calculate()
    End Sub
    Private Sub Calculate()
        'Dim sTotal = 0

        'For i As Integer = 0 To grvDetail.RowCount - 2
        '    sTotal += CDec(grvDetail.GetRowCellValue(i, colGRANDTOTAL))
        'Next
        'txtGRANDTOTAL.Text = sTotal
    End Sub
    Private Sub grvDetail_CellValueChanged(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs) Handles grvBillingTambahan.CellValueChanged
        If e.Column.Name = colRINCIAN.Name Then
            If grvBillingTambahan.GetFocusedRowCellValue(colRINCIAN) IsNot Nothing Then
                grvBillingTambahan.SetFocusedRowCellValue(colQTY, 1)
            End If
        End If
    End Sub
    Private Sub DeleteToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DeleteToolStripMenuItem.Click
        If oFormMode = FORM_MODE.FORM_MODE_VIEW Then Exit Sub
        grvBillingTambahan.DeleteSelectedRows()
    End Sub
#End Region
#Region "Command Button"
    Private Sub frmItem_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        Select Case e.KeyCode
            Case Keys.F2
                If btnSaveNew.Enabled = True Then
                    btnSaveNew_Click()
                End If
            Case Keys.F3
                If btnSaveClose.Enabled = True Then
                    btnSaveClose_Click()
                End If
            Case Keys.F5
                If btnAddPenunjang.Enabled = True Then
                    btnAddPenunjang_Click()
                End If
            Case Keys.F9
                btnFocus_Click()
            Case Keys.F12
                btnClose_Click()
        End Select
    End Sub
    Private Sub btnAddPenunjang_Click() Handles btnAddPenunjang.ItemClick
        sListRincian.Clear()
        frmReportOrderPenunjang.ShowDialog(Me)

        For Each xloop In sListRincian
            grvBillingTambahan.Focus()
            grvBillingTambahan.AddNewRow()
            grvBillingTambahan.SetFocusedRowCellValue(colRINCIAN, xloop)
            grvBillingTambahan.SetFocusedRowCellValue(colQTY, 1)
            grvBillingTambahan.SetFocusedRowCellValue(colDATE_PERMINTAAN, sDatePemeriksaan)
            grvBillingTambahan.SetFocusedRowCellValue(colKETERANGAN, "")
            grvBillingTambahan.UpdateCurrentRow()
        Next
    End Sub
    Private Sub btnFocus_Click() Handles btnFocus.ItemClick
        grvBillingTambahan.Focus()
    End Sub
    Private Sub btnSaveNew_Click() Handles btnSaveNew.ItemClick
        If fn_Validate() = False Then Exit Sub
        If MsgBox("Save " & txtKDREQBHP.Text.Trim.ToUpper & "?", MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, Me.Text) = MsgBoxResult.No Then Exit Sub
        If fn_Save() = False Then
            MsgBox("Save gagal! Tolong kontak admin anda!", MsgBoxStyle.Exclamation, Me.Text)
        Else
            MsgBox("Save " & txtKDREQBHP.Text.Trim.ToUpper & " success!", MsgBoxStyle.Information, Me.Text)
            sStatusSave = "NEW"
            Me.Close()
        End If
    End Sub
    Private Sub btnSaveClose_Click() Handles btnSaveClose.ItemClick
        If fn_Validate() = False Then Exit Sub
        If MsgBox("Save " & txtKDREQBHP.Text.Trim.ToUpper & "?", MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, Me.Text) = MsgBoxResult.No Then Exit Sub
        If fn_Save() = False Then
            MsgBox("Save gagal! Tolong kontak admin anda!", MsgBoxStyle.Exclamation, Me.Text)
        Else
            MsgBox("Save " & txtKDREQBHP.Text.Trim.ToUpper & " success!", MsgBoxStyle.Information, Me.Text)
            Me.Close()
        End If
    End Sub
    Private Sub btnClose_Click() Handles btnClose.ItemClick
        Me.Close()
    End Sub
#End Region
#Region "Lookup / Event"
    Private Sub fn_LoadDEPARTMENT()
        Dim oDepartment As New Reference.clsDepartment
        Try
            grdKDDEPARTMENT.Properties.DataSource = oDepartment.GetData.Where(Function(x) x.ISACTIVE = True).ToList()
            grdKDDEPARTMENT.Properties.ValueMember = "KDDEPARTMENT"
            grdKDDEPARTMENT.Properties.DisplayMember = "NAME_DISPLAY"
        Catch oErr As Exception
            MsgBox("Load Diagnosa Data : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadDOCTOR()
        Dim oDoctor As New Reference.clsDoctor
        Try
            Dim dsDoctor = From x In oDoctor.GetData()
                           Where x.ISACTIVE = True
                           Select x.KDDOCTOR, NAME_DISPLAY = x.NAME_DISPLAY

            grdKDDOCTOR.Properties.DataSource = dsDoctor.ToList()
            grdKDDOCTOR.Properties.ValueMember = "KDDOCTOR"
            grdKDDOCTOR.Properties.DisplayMember = "NAME_DISPLAY"
        Catch oErr As Exception
            MsgBox("Load Dokter Data : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadPendaftaran(ByVal KDDEPARTMENT As Integer, ByVal CATEGORY As Integer)
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

            SQL = "SELECT * "
            SQL &= "FROM( "
            SQL &= "SELECT "
            SQL &= "A.KDREG "
            SQL &= ",PASIEN = B.NAME_DISPLAY + ' ' + IIf(B.FRONT_TITLE IS NULL, '', B.FRONT_TITLE + ' ') + B.BACK_TITLE  "
            SQL &= ",KDDEBTOR = C.NAME_DISPLAY "
            SQL &= ",KDCUSTOMER = B.KDCUSTOMER "
            SQL &= ",NMDOCTOR = IIf(D.FRONT_TITLE IS NULL, '', D.FRONT_TITLE + ' ') + D.NAME_DISPLAY  + ' ' + D.BACK_TITLE "
            SQL &= ",A.USIA "
            SQL &= ",DEPARTMENT = E.NAME_DISPLAY "
            SQL &= ",BARULAMA = (SELECT CASE CONVERT(VARCHAR(8), A.DATE, 112) WHEN CONVERT(VARCHAR(8), B.TANGGALDAFTAR, 112) THEN 'BARU' ELSE 'LAMA' END) "
            SQL &= "FROM S_PENDAFTARAN_H A "
            SQL &= "INNER JOIN M_CUSTOMER B "
            SQL &= "ON A.KDCUSTOMER = B.KDCUSTOMER "
            SQL &= "INNER JOIN M_DEBTOR C "
            SQL &= "ON A.KDDEBTOR = C.KDDEBTOR "
            SQL &= "INNER JOIN M_DOCTOR D "
            SQL &= "ON A.KDDOCTOR = D.KDDOCTOR "
            SQL &= "INNER JOIN M_DEPARTMENT E "
            SQL &= "ON A.KDDEPARTMENT = E.KDDEPARTMENT "
            If CATEGORY = 1 Then
                SQL &= "WHERE A.CATEGORY = 1 "
                SQL &= "AND CONVERT(VARCHAR(8), A.DATE, 112) = '" & deDATETANGGALDAFTAR.DateTime.ToString("yyyyMMdd") & "' "
                SQL &= "AND A.KDDEPARTMENT = " & KDDEPARTMENT & " "
            Else
                SQL &= "WHERE A.CATEGORY = 2 "
                SQL &= "AND A.ISUPDATEPULANG = 0 "
                SQL &= "AND A.KDDEPARTMENT = " & KDDEPARTMENT & " "
            End If
            SQL &= ") AS A "
            SQL &= "ORDER BY "
            SQL &= "A.KDREG "

            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "S_PENDAFTARAN")

            grd.DataSource = ds.Tables("S_PENDAFTARAN")
            grd.ForceInitialize()

            If oConn.State = ConnectionState.Open Then
                oConn.Close()
            End If

            fn_SetFormat()

        Catch oErr As Exception
            MsgBox("Load Obat Data : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_SetFormat()
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

        grv.Columns("KDREG").VisibleIndex = -1
        grv.Columns("KDREG").Caption = "No. Regsiter"
        grv.Columns("PASIEN").Caption = "Nama Pasien"
        grv.Columns("KDDEBTOR").Caption = "Asuransi"
        grv.Columns("KDCUSTOMER").Caption = "No. RM"
        grv.Columns("NMDOCTOR").Caption = "DPJP"
        grv.Columns("DEPARTMENT").Caption = "Poli"
        grv.Columns("BARULAMA").Caption = "Baru/Lama"
        grv.Columns("USIA").Caption = "Usia"

    End Sub
    Private Sub txtKDREG_EditValueChanged(sender As Object, e As EventArgs) Handles txtKDREG.EditValueChanged
        If txtKDREG.Text = "" Then Exit Sub
        Dim oPendaftaran As New Admission.clsPendaftaran
        Dim dsPendaftaran = oPendaftaran.GetData(txtKDREG.Text)
        If dsPendaftaran IsNot Nothing Then
            txtNAMAPASIEN.Text = dsPendaftaran.M_CUSTOMER.NAME_DISPLAY
            txtKDCUSTOMER.Text = dsPendaftaran.KDCUSTOMER
            grdKDDOCTOR.Text = dsPendaftaran.KDDOCTOR
        End If
    End Sub
    Private Sub grd_DoubleClick(sender As Object, e As EventArgs) Handles grd.DoubleClick
        If grv.GetFocusedRowCellValue("KDREG") Is Nothing Then
            Exit Sub
        End If

        Dim oPendaftaran As New Admission.clsPendaftaran
        Dim dsPendaftaran = oPendaftaran.GetData(grv.GetFocusedRowCellValue("KDREG"))

        If dsPendaftaran IsNot Nothing Then
            txtKDREG.Text = dsPendaftaran.KDPENDAFTARAN
        Else
            fn_EmptyMe()
        End If
    End Sub
    Private Sub grdKDDEPARTMENT_EditValueChanged(sender As Object, e As EventArgs) Handles grdKDDEPARTMENT.EditValueChanged
        If isLoad Then
            'If grdKDDEPARTMENT.Text = "" Then Exit Sub
            'Dim oDepartment As New Reference.clsDepartment

            'fn_LoadPendaftaran(grdKDDEPARTMENT.EditValue, oDepartment.GetData(grdKDDEPARTMENT.EditValue).CATEGORY)

            'sDepartmentAuto = grdKDDEPARTMENT.EditValue
        End If
    End Sub

#End Region
End Class