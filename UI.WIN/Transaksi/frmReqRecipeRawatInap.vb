Imports DataAccess
Imports UI.WIN.MAIN.My.Resources
Imports System.Linq
Imports System.Data.SqlClient
Imports Newtonsoft.Json.Linq

Public Class frmReqRecipeRawatInap
#Region "Declaration"
    Private oFormMode As FORM_MODE = FORM_MODE.FORM_MODE_VIEW
    Private sNoId As String
    Private isLoad As Boolean = False
    Private oReqRecipeRawatInap As New Transaksi.clsReqRecipeRawatInap
    Private sPENJAMIN As String = String.Empty
    Private sKDPENDAFTARAN As String = String.Empty
    Private sKDCUSTOMER As String = String.Empty
    Private sPASIEN As String = String.Empty
    Private sKDDOKTER As String = String.Empty
    Private sRUANGAN As String = String.Empty
    Private sDIAGNOSA As String = String.Empty
    Private sBERATBADAN As String = String.Empty
    Private sUSERPERAWAT As String = String.Empty
    Private sDokter As Boolean = False
#End Region
#Region "Function"
    Public Sub fn_LoaDataPasien(ByVal PULANG As Boolean, ByVal Dokter As Boolean, ByVal PENJAMIN As String, ByVal KDPENDAFTARAN As String, ByVal KDCUSTOMER As String, ByVal PASIEN As String, ByVal KDDOKTER As String, ByVal RUANGAN As String, ByVal DIAGNOSA As String, ByVal BB As String, ByVal userperawat As String)
        sPENJAMIN = PENJAMIN
        sKDPENDAFTARAN = KDPENDAFTARAN
        sKDCUSTOMER = KDCUSTOMER
        sPASIEN = PASIEN
        sKDDOKTER = KDDOKTER
        sRUANGAN = RUANGAN
        sDIAGNOSA = DIAGNOSA
        sBERATBADAN = BB
        sDokter = Dokter
        sUSERPERAWAT = userperawat
        chkISPULANG.Checked = PULANG
    End Sub
    Public Sub LoadMe(ByVal FormMode As Integer, Optional ByVal NoId As String = "")
        oFormMode = FormMode
        sNoId = NoId
    End Sub
    Private Sub Form_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        fn_ChangeFormState()
        fn_LoadLanguage()
        isLoad = True
    End Sub
    Public Sub fn_LoadLanguage()
        Try
            Me.Text = "Request Obat"

            'lKDREQRECIPE_RI.Text = ReqRecipeRawatInap.KDREQRECIPE_RI
            'lDATE.Text = ReqRecipeRawatInap.TANGGAL

            'tab1.Text = ReqRecipeRawatInap.TAB_DETAIL
            'tab2.Text = ReqRecipeRawatInap.TAB_MEMO

            'grvDetail.Columns("KDITEM").Caption = ReqRecipeRawatInap.DETAIL_KDITEM
            'grvDetail.Columns("KDUOM").Caption = ReqRecipeRawatInap.DETAIL_KDUOM
            'grvDetail.Columns("QTY").Caption = ReqRecipeRawatInap.DETAIL_QTY
            'grvDetail.Columns("REMARKS").Caption = ReqRecipeRawatInap.DETAIL_REMARKS

            ''grvKDITEM.Columns("NMITEM1").Caption = Item.NMITEM1
            'grvKDITEM.Columns("NMITEM2").Caption = Item.NMITEM2
            ''grvKDITEM.Columns("NMITEM3").Caption = Item.NMITEM3

            'grvKDUOM.Columns("MEMO").Caption = UOM.MEMO

            'btnSaveNew.Caption = Caption.FormSaveNew
            btnSaveClose.Caption = Caption.FormSaveClose
            btnClose.Caption = Caption.FormClose
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub Form_Closing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        sCode = txtKDREQRECIPE_RI.Text.Trim.ToUpper
    End Sub
    Private Sub fn_ChangeFormState()
        fn_LoadITEM()
        fn_LoadUOM()
        fn_LoadSIGNA()
        fn_LoadCARAPAKAI()
        fn_LoadDokter()

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
        'btnSaveNew.Enabled = Not Status
        btnSaveClose.Enabled = Not Status

        txtKDPENDAFTARAN.Properties.ReadOnly = True
        txtPENJAMIN.Properties.ReadOnly = True
        txtRUANGAN.Properties.ReadOnly = Status
        txtKDCUSTOMER.Properties.ReadOnly = True
        txtPASIEN.Properties.ReadOnly = True
        txtOBJEKTIF_BERATBADAN.Properties.ReadOnly = Status
        txtDIAGNOSA.Properties.ReadOnly = Status
        grdDPJP.Properties.ReadOnly = Status
        deDATE.Properties.ReadOnly = Status
        chkISPULANG.Properties.ReadOnly = True

        grvDetailResep.OptionsBehavior.ReadOnly = Status
    End Sub
    Private Sub fn_EmptyMe()
        txtKDREQRECIPE_RI.Text = "<--- AUTO --->"
        txtKDPENDAFTARAN.Text = sKDPENDAFTARAN
        txtPENJAMIN.Text = sPENJAMIN
        txtRUANGAN.Text = sRUANGAN
        txtKDCUSTOMER.Text = sKDCUSTOMER
        txtPASIEN.Text = sPASIEN

        txtOBJEKTIF_BERATBADAN.Text = sBERATBADAN
        txtDIAGNOSA.Text = sDIAGNOSA
        grdDPJP.Text = sKDDOKTER
        deDATE.DateTime = Now

    End Sub
    Private Sub fn_LoadData()
        Try
            ' ***** HEADER *****
            Dim ds = oReqRecipeRawatInap.GetData(sNoId)

            With ds
                txtKDREQRECIPE_RI.Text = .KDREQRECIPE_RI
                deDATE.DateTime = .DATE
                chkALERGI_TIDAK.Checked = .ISPERUBAHANRESEP
                chkALERGI_YA.Checked = .ISAPPROVAL
                txtKDPENDAFTARAN.Text = .KDPENDAFTARAN
                txtPENJAMIN.Text = .PENJAMIN
                txtRUANGAN.Text = .TUJUAN
                txtKDCUSTOMER.Text = .KDCUSTOMER
                txtPASIEN.Text = .PASIEN
                txtOBJEKTIF_BERATBADAN.Text = .BERATBADAN
                txtDIAGNOSA.Text = .DIAGNOSA
                grdDPJP.Text = .ALAMAT
                chkISPULANG.Checked = .ISPULANG

                bindingSource.DataSource = oReqRecipeRawatInap.GetDataDetail(sNoId).OrderBy(Function(x) x.SEQ).ToList()
                grdDetailResep.DataSource = bindingSource
            End With
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Function fn_Validate() As Boolean
        Try
            fn_Validate = True

            grvDetailResep.UpdateCurrentRow()
            If txtKDPENDAFTARAN.Text = String.Empty Then
                txtKDPENDAFTARAN.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                txtKDPENDAFTARAN.ErrorText = Statement.ErrorRequired

                txtKDPENDAFTARAN.Focus()
                fn_Validate = False
                Exit Function
            End If
            If txtKDCUSTOMER.Text = String.Empty Then
                txtKDCUSTOMER.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                txtKDCUSTOMER.ErrorText = Statement.ErrorRequired

                txtKDCUSTOMER.Focus()
                fn_Validate = False
                Exit Function
            End If
            If sUSERPERAWAT = "" Then
                'txtKDCUSTOMER.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                'txtKDCUSTOMER.ErrorText = Statement.ErrorRequired

                'txtKDCUSTOMER.Focus()

                MsgBox("User Kosong", MsgBoxStyle.Exclamation, Me.Text)
                fn_Validate = False
                Exit Function
            End If
            If grvDetailResep.RowCount < 2 Then
                MsgBox(Statement.ErrorDetail, MsgBoxStyle.Exclamation, Me.Text)
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
            Dim ds = oReqRecipeRawatInap.GetStructureHeader
            With ds
                Try
                    .DATECREATED = oReqRecipeRawatInap.GetData(sNoId).DATECREATED
                Catch ex As Exception
                    .DATECREATED = Now
                End Try
                .DATEUPDATED = Now
                .DATE = deDATE.DateTime
                .KDREQRECIPE_RI = sNoId
                .NOANTRIAN = String.Empty
                .KDPENDAFTARAN = txtKDPENDAFTARAN.Text
                .KDWAREHOUSE = 1
                .ALERGIOBAT = IIf(txtALERGIOBAT.Text.ToString.Trim = String.Empty, "-", txtALERGIOBAT.Text)
                .BERATBADAN = txtOBJEKTIF_BERATBADAN.Text
                Try
                    .DESCRIPTION = oReqRecipeRawatInap.GetData(sNoId).DESCRIPTION
                Catch ex As Exception
                    .DESCRIPTION = deDATE.DateTime.ToString("ddMMyyyy")
                End Try
                Try
                    .ISCHEKED = oReqRecipeRawatInap.GetData(sNoId).ISCHEKED
                Catch ex As Exception
                    .ISCHEKED = False
                End Try
                Try
                    .KONFIRMASIRESEP = oReqRecipeRawatInap.GetData(sNoId).KONFIRMASIRESEP
                Catch ex As Exception
                    .KONFIRMASIRESEP = "Resep Belum Diterima"
                End Try
                .SUBTOTAL_RINCIAN = CDec(0)
                .SUBTOTAL_PAKET = CDec(0)
                .SUBTOTAL_KRONIS = CDec(0)
                .GRANDTOTAL = CDec(0)

                .NOIDUSER = sUSERPERAWAT

                .ISPERUBAHANRESEP = chkALERGI_TIDAK.Checked
                .ISAPPROVAL = chkALERGI_YA.Checked
                Try
                    .NOIDUSER_FARMASI = oReqRecipeRawatInap.GetData(sNoId).NOIDUSER_FARMASI
                Catch ex As Exception
                    .NOIDUSER_FARMASI = String.Empty
                End Try
                Try
                    .DESCRIPTION_PERUBAHAN = oReqRecipeRawatInap.GetData(sNoId).DESCRIPTION_PERUBAHAN
                Catch ex As Exception
                    .DESCRIPTION_PERUBAHAN = ""
                End Try

                .PENJAMIN = txtPenjamin.Text
                .KDCUSTOMER = txtKDCUSTOMER.Text
                .PASIEN = txtPASIEN.Text
                .ALAMAT = grdDPJP.EditValue
                .DOKTER = grdDPJP.Text
                .TUJUAN = txtRUANGAN.Text
                .DIAGNOSA = txtDIAGNOSA.Text

                Dim oDoctor As New Reference.clsDoctor
                Dim dsDoctor = oDoctor.GetData(grdDPJP.EditValue)
                If dsDoctor IsNot Nothing Then
                    .SIPDOKTER = dsDoctor.SIP
                Else
                    .SIPDOKTER = ""
                End If
                Try
                    .KDRECIPE = oReqRecipeRawatInap.GetData(sNoId).KDRECIPE
                Catch ex As Exception
                    .KDRECIPE = ""
                End Try

                .ISPULANG = chkISPULANG.Checked
            End With

            ' ***** DETIL *****
            Dim arrDetail = oReqRecipeRawatInap.GetStructureDetailList
            For i As Integer = 0 To grvDetailResep.RowCount - 2
                Dim dsDetail = oReqRecipeRawatInap.GetStructureDetail
                With dsDetail
                    .SEQ = i
                    .KDREQRECIPE_RI = ds.KDREQRECIPE_RI
                    .NAMAOBAT = fn_LoadITEM(grvDetailResep.GetRowCellValue(i, colKDITEM))
                    .SATUAN = fn_LoadUOMDESCRIPTION(grvDetailResep.GetRowCellValue(i, colKDUOM))
                    .SIGNA = IIf(String.IsNullOrEmpty(grvDetailResep.GetRowCellValue(i, colKDSIGNA)), "", fn_LoadSIGNA(grvDetailResep.GetRowCellValue(i, colKDSIGNA)))
                    .CARAPAKAI = IIf(String.IsNullOrEmpty(grvDetailResep.GetRowCellValue(i, colKDCARAPAKAI)), "", fn_LoadCARAPAKAI(grvDetailResep.GetRowCellValue(i, colKDCARAPAKAI)))
                    .QTY = CDec(grvDetailResep.GetRowCellValue(i, colQTY))
                    .PRICE = CDec(grvDetailResep.GetRowCellValue(i, colPRICE))
                    .GRANDTOTAL = CDec(0)
                    .ROMAWI = IntegerToRoman(CInt(grvDetailResep.GetRowCellValue(i, colQTY)))
                    .REMARKS_DOKTER = IIf(String.IsNullOrEmpty(grvDetailResep.GetRowCellValue(i, colREMARKS_DOKTER)), "", grvDetailResep.GetRowCellValue(i, colREMARKS_DOKTER))
                    .KDITEM = grvDetailResep.GetRowCellValue(i, colKDITEM)
                    .KDUOM = grvDetailResep.GetRowCellValue(i, colKDUOM)
                    .KDSIGNA = IIf(String.IsNullOrEmpty(grvDetailResep.GetRowCellValue(i, colKDSIGNA)), "1286", grvDetailResep.GetRowCellValue(i, colKDSIGNA))
                    .KDCARAPAKAI = IIf(String.IsNullOrEmpty(grvDetailResep.GetRowCellValue(i, colKDCARAPAKAI)), "10", grvDetailResep.GetRowCellValue(i, colKDCARAPAKAI))
                    .QTY_PERUBAHAN = grvDetailResep.GetRowCellValue(i, colQTY)
                    Try
                        .REMARKS_FARMASI = oReqRecipeRawatInap.GetDataObatBySeq(ds.KDREQRECIPE_RI, i).REMARKS_FARMASI
                    Catch ex As Exception
                        .REMARKS_FARMASI = ""
                    End Try
                    .DOSIS = IIf(String.IsNullOrEmpty(grvDetailResep.GetRowCellValue(i, colDOSIS)), "", grvDetailResep.GetRowCellValue(i, colDOSIS))
                End With

                If grvDetailResep.GetRowCellValue(i, colKDITEM) IsNot Nothing Then
                    arrDetail.Add(dsDetail)
                End If
            Next

            If oFormMode = FORM_MODE.FORM_MODE_ADD Then
                Try
                    fn_Save = oReqRecipeRawatInap.InsertData(ds, arrDetail)
                Catch oErr As Exception
                    MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            ElseIf oFormMode = FORM_MODE.FORM_MODE_EDIT Then
                Try
                    fn_Save = oReqRecipeRawatInap.UpdateData(ds, arrDetail)
                Catch oErr As Exception
                    MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            End If

        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            fn_Save = False
        End Try
    End Function
#End Region
#Region "Grid Method"
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
            grvDetailResep.SetFocusedRowCellValue(colPRICE, hargajualawal)
        ElseIf e.Column.Name = colQTY.Name Or e.Column.Name = colPRICE.Name Then
            Dim sSubTotal As Decimal = CDec(grvDetailResep.GetFocusedRowCellValue(colQTY)) * CDec(grvDetailResep.GetFocusedRowCellValue(colPRICE))
            grvDetailResep.SetFocusedRowCellValue(colGRANDTOTAL, sSubTotal)
        End If
    End Sub
    Private Sub DeleteToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DeleteToolStripMenuItem.Click
        If oFormMode = FORM_MODE.FORM_MODE_VIEW Then Exit Sub
        grvDetailResep.DeleteSelectedRows()
    End Sub
#End Region
#Region "Command Button"
    Private Sub frmReqRecipeRawatInap_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        Select Case e.KeyCode
            Case Keys.F12
                btnClose_Click()
            'Case Keys.F2
            '    If btnSaveNew.Enabled = True Then
            '        btnSaveNew_Click()
            '    End If
            Case Keys.F3
                If btnSaveClose.Enabled = True Then
                    btnSaveClose_Click()
                End If
        End Select
    End Sub
    'Private Sub btnSaveNew_Click() Handles btnSaveNew.ItemClick
    '    If fn_Validate() = False Then Exit Sub
    '    If MsgBox(Statement.SaveQuestion, MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, Me.Text) = MsgBoxResult.No Then Exit Sub
    '    If fn_Save() = False Then
    '        MsgBox(Statement.SaveFail, MsgBoxStyle.Exclamation, Me.Text)
    '    Else
    '        MsgBox(Statement.SaveSuccess, MsgBoxStyle.Information, Me.Text)
    '        sStatusSave = "NEW"
    '        Me.Close()
    '    End If
    'End Sub
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
#Region "Lookup / Event"
    Public Function fn_LoadUOMKDUOMHARGA(ByVal KDITEM As String, ByVal kduom As String) As Decimal
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
    Public Function fn_LoadUOMKDUOM(ByVal KDITEM As String) As String
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
    Private Sub fn_LoadDokter()
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

            grdDPJP.Properties.DataSource = ds.Tables("M_DOCTOR")
            grdDPJP.Properties.ValueMember = "KDDOCTOR"
            grdDPJP.Properties.DisplayMember = "NAME_DISPLAY"

            If oConn.State = ConnectionState.Open Then
                oConn.Close()
            End If

        Catch oErr As Exception
            MsgBox("Load Dokter Data : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
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

            'ds.PRICEPURCHASESTANDARD + (ds.PRICEPURCHASESTANDARD * (ds.MARGIN / 100))

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
            'If sDokter = False Then
            '    SQL &= "AND "
            '    SQL &= "KDKATEGORI_BPJS IN ('15','16', '17') "
            'Else
            '    SQL &= "AND "
            '    SQL &= "KDKATEGORI_BPJS NOT IN ('15','16', '17') "
            'End If

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
    Public Function fn_LoadITEM(ByVal KDITEM As String) As String
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
    Public Function fn_LoadUOMDESCRIPTION(ByVal KDUOM As String) As String
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
    Public Function fn_LoadSIGNA(ByVal KDSIGNA As String) As String
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
    Public Function fn_LoadCARAPAKAI(ByVal KDCP As String) As String
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
    Public Function IntegerToRoman(IntNumberValue As Integer) As String
        Try
            IntegerToRoman = ""

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

            IntegerToRoman = result
        Catch ex As Exception
            IntegerToRoman = ""
        End Try
    End Function
    Public Function fn_LoadDokter(ByVal KDDCTOR As Integer) As String
        Try
            fn_LoadDokter = ""

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
            SQL &= "A.NAME_DISPLAY "
            SQL &= "FROM "
            SQL &= "M_DOCTOR A "
            SQL &= "WHERE "
            SQL &= "A.KDDOCTOR = " & KDDCTOR & " "

            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "SEARCHDOKTER")

            For iLoop As Integer = 0 To ds.Tables("SEARCHDOKTER").Rows.Count - 1
                With ds.Tables("SEARCHDOKTER")
                    fn_LoadDokter = .Rows(iLoop)("NAME_DISPLAY")
                End With
            Next

            If oConn.State = ConnectionState.Open Then
                oConn.Close()
            End If
        Catch oErr As Exception
            fn_LoadDokter = ""
            MsgBox("Load Dokter" & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Function
#End Region
End Class