Imports DataAccess
Imports UI.WIN.MAIN.My.Resources
Imports System.Linq
Imports Newtonsoft.Json.Linq
Imports System.Data.SqlClient

Public Class frmDIGITAL_IGD_01_AWAL
#Region "Declaration"
    Private oFormMode As FORM_MODE = FORM_MODE.FORM_MODE_VIEW
    Private sNoId As String
    Private isLoad As Boolean = False
    Private oDIGITAL_IGD_01_AWAL As New Transaksi.clsDIGITAL_IGD_01_AWAL

#End Region
#Region "Function"
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
            Me.Text = "Reload Data Igd"

            btnSaveNew.Caption = Caption.FormSaveNew
            btnSaveClose.Caption = Caption.FormSaveClose
            btnClose.Caption = Caption.FormClose
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub Form_Closing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        sCode = txtKDAWALASESMENIGD.Text.Trim.ToUpper
    End Sub
    Private Sub fn_ChangeFormState()
        fn_LoadKDUSER()
        fn_LoadITEM()
        fn_LoadUOM()
        fn_LoadSIGNA()
        fn_LoadCARAPAKAI()
        fn_LoadTEMPLATE()

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

        deDATE.Properties.ReadOnly = Status
        txtNAMAPASIEN.Properties.ReadOnly = Status
        txtRIWAYAT.Properties.ReadOnly = Status
        txtRIWAYAT_ALERGI.Properties.ReadOnly = Status
        txtANAMNESIS.Properties.ReadOnly = Status
        cboTINGKATKESADARAN.Properties.ReadOnly = Status
        cboE.Properties.ReadOnly = Status
        cboM.Properties.ReadOnly = Status
        cboV.Properties.ReadOnly = Status
        txtKEADAANUMUM.Properties.ReadOnly = Status
        txtBERATBADAN.Properties.ReadOnly = Status
        txtTINGGIBADAN.Properties.ReadOnly = Status
        txtBP.Properties.ReadOnly = Status
        txtHR.Properties.ReadOnly = Status
        txtRR.Properties.ReadOnly = Status
        txtT.Properties.ReadOnly = Status
        txtSPO2.Properties.ReadOnly = Status
        chkKENDARAAN_1.Properties.ReadOnly = Status
        chkKENDARAAN_2.Properties.ReadOnly = Status
        chkKENDARAAN_3.Properties.ReadOnly = Status
        txtKENDARAAN_KETERANGAN.Properties.ReadOnly = Status
        chkASALPASIEN_1.Properties.ReadOnly = Status
        chkASALPASIEN_2.Properties.ReadOnly = Status
        txtASALPASIEN_KETERANGAN.Properties.ReadOnly = Status
        chkTRIAGE_1.Properties.ReadOnly = Status
        chkTRIAGE_2.Properties.ReadOnly = Status
        chkTRIAGE_3.Properties.ReadOnly = Status
        chkTRIAGE_4.Properties.ReadOnly = Status

        cboTRIAGE_AIRWAY.Properties.ReadOnly = Status
        cboTRIAGE_BREATHING.Properties.ReadOnly = Status
        cboTRIAGE_CIRCULATION.Properties.ReadOnly = Status
        grdKDUSER.Properties.ReadOnly = Status
        cboBad.Properties.ReadOnly = Status
    End Sub
    Private Sub fn_EmptyMe()
        txtKDAWALASESMENIGD.Text = "<--- AUTO --->"
        deDATE.DateTime = Now
        txtNAMAPASIEN.ResetText()
        txtRIWAYAT.ResetText()
        txtRIWAYAT_ALERGI.ResetText()
        txtANAMNESIS.ResetText()
        cboTINGKATKESADARAN.ResetText()
        txtGCS.ResetText()
        cboE.ResetText()
        cboM.ResetText()
        cboV.ResetText()
        txtKEADAANUMUM.ResetText()
        txtBERATBADAN.ResetText()
        txtTINGGIBADAN.ResetText()
        txtBP.ResetText()
        txtHR.ResetText()
        txtRR.ResetText()
        txtT.ResetText()
        txtSPO2.ResetText()
        txtKENDARAAN_KETERANGAN.ResetText()
        txtASALPASIEN_KETERANGAN.ResetText()
        grdKDUSER.ResetText()
        cboBad.ResetText()
    End Sub
    Private Sub fn_LoadData()
        Try
            ' ***** HEADER *****
            Dim ds = oDIGITAL_IGD_01_AWAL.GetData(sNoId)

            With ds
                txtKDAWALASESMENIGD.Text = .KDAWALASESMENIGD
                deDATE.DateTime = .DATE
                txtNAMAPASIEN.Text = .NAMAPASIEN
                txtRIWAYAT.Text = .RIWAYAT
                txtRIWAYAT_ALERGI.Text = .ALERGI
                txtANAMNESIS.Text = .ANAMNESIS
                cboTINGKATKESADARAN.Text = .TINGKATKESADARAN
                txtGCS.Text = .GCS
                cboE.Text = .GCS_E
                cboM.Text = .GCS_M
                cboV.Text = .GCS_V
                txtKEADAANUMUM.Text = .KEADAANUMUM
                txtBERATBADAN.Text = .BERATBADAN
                txtTINGGIBADAN.Text = .TINGGIBADAN
                txtBP.Text = .BP
                txtHR.Text = .HR
                txtRR.Text = .RR
                txtT.Text = .T
                txtSPO2.Text = .SP02
                chkKENDARAAN_1.Checked = .KENDARAAN_1
                chkKENDARAAN_2.Checked = .KENDARAAN_2
                chkKENDARAAN_3.Checked = .KENDARAAN_3
                txtKENDARAAN_KETERANGAN.Text = .KENDARAAN_TEX
                chkASALPASIEN_1.Checked = .DATANG_1
                chkASALPASIEN_2.Checked = .DATANG_2
                txtASALPASIEN_KETERANGAN.Text = .DATANG_TEXT
                chkTRIAGE_1.Checked = .TRIAGE_1
                chkTRIAGE_2.Checked = .TRIAGE_2
                chkTRIAGE_3.Checked = .TRIAGE_3
                chkTRIAGE_4.Checked = .TRIAGE_4

                cboTRIAGE_AIRWAY.Text = .TRIAGE_AIRWAY
                cboTRIAGE_BREATHING.Text = .TRIAGE_BREATHING
                cboTRIAGE_CIRCULATION.Text = .TRIAGE_CIRCULATION

                Dim oUnit As New Reference.clsUnit
                Dim dsUnit = oUnit.GetDataByName(.KDUSER)
                If dsUnit IsNot Nothing Then
                    grdKDUSER.Text = dsUnit.KDUNIT
                Else
                    grdKDUSER.Text = .KDUSER
                End If

                cboBad.Text = .BAD

                BindingSource.DataSource = oDIGITAL_IGD_01_AWAL.GetDataDetail(sNoId).OrderBy(Function(x) x.SEQ).ToList()
                grdDetail.DataSource = BindingSource

                BindingSource1.DataSource = oDIGITAL_IGD_01_AWAL.GetDataDetail_P(sNoId).OrderBy(Function(x) x.SEQ).ToList()
                grdDetail_P.DataSource = BindingSource1

                BindingSource2.DataSource = oDIGITAL_IGD_01_AWAL.GetDataDetail_ResepPulang(sNoId).OrderBy(Function(x) x.SEQ).ToList()
                grdDetailResep.DataSource = BindingSource2
            End With
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Function fn_Validate() As Boolean
        Try
            fn_Validate = True

            If txtNAMAPASIEN.Text = String.Empty Then
                txtNAMAPASIEN.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                txtNAMAPASIEN.ErrorText = Statement.ErrorRequired

                txtNAMAPASIEN.Focus()
                fn_Validate = False
                Exit Function
            End If

            If grdKDUSER.Text = String.Empty Then
                grdKDUSER.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                grdKDUSER.ErrorText = Statement.ErrorRequired

                grdKDUSER.Focus()
                fn_Validate = False
                Exit Function
            End If
            If cboBad.Text = String.Empty Then
                cboBad.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                cboBad.ErrorText = Statement.ErrorRequired

                cboBad.Focus()
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
            Dim ds = oDIGITAL_IGD_01_AWAL.GetStructureHeader
            With ds
                Try
                    .DATECREATED = oDIGITAL_IGD_01_AWAL.GetData(sNoId).DATECREATED
                Catch oErr As Exception
                    .DATECREATED = Now
                End Try
                .DATEUPDATED = Now
                .DATE = deDATE.DateTime
                .KDAWALASESMENIGD = sNoId
                Try
                    .KDPENDAFTARAN = oDIGITAL_IGD_01_AWAL.GetData(sNoId).KDPENDAFTARAN
                Catch ex As Exception
                    .KDPENDAFTARAN = ""
                End Try
                .NAMAPASIEN = txtNAMAPASIEN.Text
                .RIWAYAT = txtRIWAYAT.Text
                .ALERGI = txtRIWAYAT_ALERGI.Text
                .ANAMNESIS = txtANAMNESIS.Text
                .TINGKATKESADARAN = cboTINGKATKESADARAN.Text
                .GCS = txtGCS.Text
                .GCS_E = cboE.Text
                .GCS_M = cboM.Text
                .GCS_V = cboV.Text
                .KEADAANUMUM = txtKEADAANUMUM.Text
                .BERATBADAN = txtBERATBADAN.Text
                .TINGGIBADAN = txtTINGGIBADAN.Text
                .BP = txtBP.Text
                .HR = txtHR.Text
                .RR = txtRR.Text
                .T = txtT.Text
                .SP02 = txtSPO2.Text
                .KDUSER = grdKDUSER.Text

                .KENDARAAN_1 = chkKENDARAAN_1.Checked
                .KENDARAAN_2 = chkKENDARAAN_2.Checked
                .KENDARAAN_3 = chkKENDARAAN_3.Checked
                .KENDARAAN_TEX = txtKENDARAAN_KETERANGAN.Text
                .DATANG_1 = chkASALPASIEN_1.Checked
                .DATANG_2 = chkASALPASIEN_2.Checked
                .DATANG_TEXT = txtASALPASIEN_KETERANGAN.Text
                .TRIAGE_1 = chkTRIAGE_1.Checked
                .TRIAGE_2 = chkTRIAGE_2.Checked
                .TRIAGE_3 = chkTRIAGE_3.Checked
                .TRIAGE_4 = chkTRIAGE_4.Checked

                .TRIAGE_AIRWAY = cboTRIAGE_AIRWAY.Text
                .TRIAGE_BREATHING = cboTRIAGE_BREATHING.Text
                .TRIAGE_CIRCULATION = cboTRIAGE_CIRCULATION.Text
                .BAD = cboBad.Text
            End With

            ' ***** DETIL *****
            Dim arrDetail = oDIGITAL_IGD_01_AWAL.GetStructureDetailList
            For i As Integer = 0 To grvDetail.RowCount - 2
                Dim dsDetail = oDIGITAL_IGD_01_AWAL.GetStructureDetail
                With dsDetail
                    .SEQ = i
                    .KDAWALASESMENIGD = ds.KDAWALASESMENIGD
                    .PENANGANAN = grvDetail.GetRowCellValue(i, colPENANGANAN)
                End With
                arrDetail.Add(dsDetail)
            Next

            Dim arrDetail_P = oDIGITAL_IGD_01_AWAL.GetStructureDetailList_P
            For i As Integer = 0 To grvDetail_P.RowCount - 2
                Dim dsDetail = oDIGITAL_IGD_01_AWAL.GetStructureDetail_P
                With dsDetail
                    .SEQ = i
                    .KDAWALASESMENIGD = ds.KDAWALASESMENIGD
                    .PENANGANAN = grvDetail_P.GetRowCellValue(i, colPENANGANAN_P)
                End With
                arrDetail_P.Add(dsDetail)
            Next

            Dim arrDetail_ResepPulang = oDIGITAL_IGD_01_AWAL.GetStructureDetailList_ResepPulang
            For i As Integer = 0 To grvDetailResep.RowCount - 2
                Dim dsDetail = oDIGITAL_IGD_01_AWAL.GetStructureDetail_ResepPulang
                With dsDetail
                    .SEQ = i
                    .KDAWALASESMENIGD = ds.KDAWALASESMENIGD
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
                    .REMARKS_FARMASI = "OBAT PULANG"
                End With

                If grvDetailResep.GetRowCellValue(i, colKDITEM) IsNot Nothing Then
                    arrDetail_ResepPulang.Add(dsDetail)
                End If
            Next

            If oFormMode = FORM_MODE.FORM_MODE_ADD Then
                Try
                    fn_Save = oDIGITAL_IGD_01_AWAL.InsertData(ds, arrDetail, arrDetail_P, arrDetail_ResepPulang)
                Catch oErr As Exception
                    MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            ElseIf oFormMode = FORM_MODE.FORM_MODE_EDIT Then
                Try
                    fn_Save = oDIGITAL_IGD_01_AWAL.UpdateData(ds, arrDetail, arrDetail_P, arrDetail_ResepPulang)
                Catch oErr As Exception
                    MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            End If

            If fn_Save = True Then
                fn_SaveBad(ds.KDAWALASESMENIGD)
            End If
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            fn_Save = False
        End Try
    End Function
    Private Sub fn_SaveBad(ByVal KDPENDAFTARAN As String)
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
            SQL &= "FROM "
            SQL &= "S_PENDAFTARAN_BED A "
            SQL &= "WHERE A.KDREG = '" & KDPENDAFTARAN & "' "

            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "SELECT_S_PENDAFTARAN_BED")


            Dim isUPDATE As Boolean = False

            For iLoop As Integer = 0 To ds.Tables("SELECT_S_PENDAFTARAN_BED").Rows.Count - 1
                isUPDATE = True
            Next

            If isUPDATE = False Then
                SQL = "INSERT "
                SQL &= "INTO "
                SQL &= "S_PENDAFTARAN_BED "
                SQL &= "( "
                SQL &= "KDREG "
                SQL &= ",KET_1 "
                SQL &= ",KET_2 "
                SQL &= ",KET_3 "
                SQL &= ") "
                SQL &= "VALUES "
                SQL &= "( "
                SQL &= "'" & KDPENDAFTARAN & "' "
                SQL &= ",'" & cboBad.Text & "' "
                SQL &= ",'" & "" & "' "
                SQL &= ",'" & "" & "' "
                SQL &= ") "

                oComm.Connection = oConn
                oComm.CommandText = SQL
                oComm.CommandTimeout = 120
                oComm.CommandType = CommandType.Text

                da = New SqlDataAdapter(oComm)
                da.Fill(ds, "INSERT_S_PENDAFTARAN_BED")
            Else
                SQL = "UPDATE S_PENDAFTARAN_BED SET KET_1 = '" & cboBad.Text & "' WHERE KDREG = '" & KDPENDAFTARAN & "' "

                oComm.Connection = oConn
                oComm.CommandText = SQL
                oComm.CommandTimeout = 120
                oComm.CommandType = CommandType.Text

                da = New SqlDataAdapter(oComm)
                da.Fill(ds, "UPDATE_S_PENDAFTARAN_BED")
            End If


            If oConn.State = ConnectionState.Open Then
                oConn.Close()
            End If
        Catch oErr As Exception
            MsgBox("S_PENDAFTARAN_BED : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Public Function fn_LoadITEM(ByVal KDITEM As String) As String
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

#End Region
#Region "Grid Method"
    Private Sub DeleteToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DeleteToolStripMenuItem.Click
        grvDetail.DeleteSelectedRows()
    End Sub
    Private Sub DeleteToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles DeleteToolStripMenuItem1.Click
        grvDetail_P.DeleteSelectedRows()
    End Sub
    Private Sub DeleteToolStripMenuItem2_Click(sender As Object, e As EventArgs) Handles DeleteToolStripMenuItem2.Click
        grvDetailResep.DeleteSelectedRows()
    End Sub
    Private Sub cboE_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboE.SelectedIndexChanged, cboM.SelectedIndexChanged, cboV.SelectedIndexChanged
        If isLoad = True Then
            Dim sE As Decimal = 0
            Dim sM As Decimal = 0
            Dim sV As Decimal = 0

            If cboE.Text = "-" Or cboM.Text = "-" Or cboV.Text = "-" Then
                txtGCS.Text = "-"
            Else
                If cboE.Text <> String.Empty Then
                    sE = CDec(cboE.Text)
                End If

                If cboM.Text <> String.Empty Then
                    sM = CDec(cboM.Text)
                End If

                If cboV.Text <> String.Empty Then
                    sV = CDec(cboV.Text)
                End If

                txtGCS.Text = sE + sM + sV
            End If
        End If
    End Sub
#End Region
#Region "Command Button"
    Private Sub grdTEMPLATE_EditValueChanged(sender As Object, e As EventArgs) Handles grdTEMPLATE.EditValueChanged
        If isLoad = True Then
            If grdTEMPLATE.Text <> "" Then
                Dim oTemplate As New Reference.clsTemplateResep

                Dim dsTemplate = oTemplate.GetDataDetail(grdTEMPLATE.EditValue)

                For Each iLoop In dsTemplate.OrderBy(Function(x) x.SEQ)
                    'txtDIAGNOOSA.Text = iLoop.S_REQ_RECIPE_TEMPLATE_H.DESCRIPTION
                    grvDetailResep.Focus()
                    grvDetailResep.AddNewRow()
                    grvDetailResep.SetFocusedRowCellValue(colKDITEM, iLoop.KDITEM)
                    grvDetailResep.SetFocusedRowCellValue(colKDUOM, iLoop.KDUOM)
                    grvDetailResep.SetFocusedRowCellValue(colKDSIGNA, iLoop.KDSIGNA)
                    grvDetailResep.SetFocusedRowCellValue(colKDCARAPAKAI, iLoop.KDCARAPAKAI)
                    grvDetailResep.SetFocusedRowCellValue(colQTY, iLoop.QTY)
                    grvDetailResep.SetFocusedRowCellValue(colREMARKS_DOKTER, iLoop.REMARKS_DOKTER)
                    grvDetailResep.UpdateCurrentRow()


                    '.NAMAOBAT = fn_LoadITEM(grvDetailResep.GetRowCellValue(i, colKDITEM))
                    '.SATUAN = fn_LoadUOMDESCRIPTION(grvDetailResep.GetRowCellValue(i, colKDUOM))
                    '.SIGNA = IIf(String.IsNullOrEmpty(grvDetailResep.GetRowCellValue(i, colKDSIGNA)), "", fn_LoadSIGNA(grvDetailResep.GetRowCellValue(i, colKDSIGNA)))
                    '.CARAPAKAI = IIf(String.IsNullOrEmpty(grvDetailResep.GetRowCellValue(i, colKDCARAPAKAI)), "", fn_LoadCARAPAKAI(grvDetailResep.GetRowCellValue(i, colKDCARAPAKAI)))
                    '.QTY = CDec(grvDetailResep.GetRowCellValue(i, colQTY))
                    '.PRICE = CDec(grvDetailResep.GetRowCellValue(i, colPRICE))
                    '.GRANDTOTAL = CDec(grvDetailResep.GetRowCellValue(i, colGRANDTOTAL))
                    '.ROMAWI = IntegerToRoman(CInt(grvDetailResep.GetFocusedRowCellValue(colQTY)))
                    '.REMARKS_DOKTER = IIf(String.IsNullOrEmpty(grvDetailResep.GetRowCellValue(i, colREMARKS_DOKTER)), "", grvDetailResep.GetRowCellValue(i, colREMARKS_DOKTER))
                    '.KDITEM = grvDetailResep.GetRowCellValue(i, colKDITEM)
                    '.KDUOM = grvDetailResep.GetRowCellValue(i, colKDUOM)
                    '.KDSIGNA = IIf(String.IsNullOrEmpty(grvDetailResep.GetRowCellValue(i, colKDSIGNA)), "1286", grvDetailResep.GetRowCellValue(i, colKDSIGNA))
                    '.KDCARAPAKAI = IIf(String.IsNullOrEmpty(grvDetailResep.GetRowCellValue(i, colKDCARAPAKAI)), "10", grvDetailResep.GetRowCellValue(i, colKDCARAPAKAI))
                    '.QTY_PERUBAHAN = grvDetailResep.GetRowCellValue(i, colQTY)
                    '.REMARKS_FARMASI = ""
                Next
            End If
        End If
    End Sub
    Private Sub frmDIGITAL_IGD_01_AWAL_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
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
        End Select
    End Sub
    Private Sub btnSaveNew_Click() Handles btnSaveNew.ItemClick
        If fn_Validate() = False Then Exit Sub
        If MsgBox(Statement.SaveQuestion, MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, Me.Text) = MsgBoxResult.No Then Exit Sub
        If fn_Save() = False Then
            MsgBox(Statement.SaveFail, MsgBoxStyle.Exclamation, Me.Text)
        Else
            MsgBox(Statement.SaveSuccess, MsgBoxStyle.Information, Me.Text)
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
            MsgBox(Statement.SaveSuccess, MsgBoxStyle.Information, Me.Text)
            Me.Close()
        End If
    End Sub
    Private Sub btnClose_Click() Handles btnClose.ItemClick
        Me.Close()
    End Sub
#End Region
#Region "Lookup / Event"
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
    Private Sub fn_LoadKDUSER()
        Dim oTemplate As New Reference.clsUnit
        Try
            grdKDUSER.Properties.DataSource = oTemplate.GetData.Where(Function(x) x.ISACTIVE = True).ToList()
            grdKDUSER.Properties.ValueMember = "KDUNIT"
            grdKDUSER.Properties.DisplayMember = "MEMO"
        Catch oErr As Exception
            MsgBox("Load Template Unit Data : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
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

            grdITEM.DataSource = ds.Tables("ITEM")
            grdITEM.ValueMember = "KDITEM"
            grdITEM.DisplayMember = "NMITEM2"

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
#End Region
End Class