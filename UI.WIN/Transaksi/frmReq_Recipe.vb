Imports DataAccess
Imports System.Linq
Imports System.Data.SqlClient
Imports Newtonsoft.Json.Linq

Public Class frmReq_Recipe
#Region "Declaration"
    Private oFormMode As FORM_MODE = FORM_MODE.FORM_MODE_VIEW
    Private sNoId As String
    Private isLoad As Boolean = False
    Private oReq_Recipe As New Transaksi.clsReq_Recipe
    Private ssKODEBOOKING As String = String.Empty
#End Region
#Region "Function"
    Public Sub fn_LoadIdentias(ByVal BERATBADAN As Decimal, ByVal TINGGIBADAN As Decimal, ByVal ALERGI As String, ByVal KDPENDAFTRAN As String, ByVal usia As String, ByVal sPASIEN As String, ByVal sKDCUSTOMER As String, ByVal sDOKTER As String, ByVal sTUJUAN As String, ByVal sSIPDOKTER As String, ByVal sPENJAMIN As String, ByVal sKODEBOOKING As String)
        txtBERATBADAN.Text = BERATBADAN
        txtTB.Text = TINGGIBADAN
        txtALERGIOBAT.Text = ALERGI
        lblNamaPasien.Text = sPASIEN
        lblTanggalLahirUsia.Text = usia
        lblRM.Text = sKDCUSTOMER
        'lblJenisRawat.Text = slblJenisRawat
        lblRegister.Text = KDPENDAFTRAN
        lblDPJP.Text = sDOKTER
        lblTUJUAN.Text = sTUJUAN
        lblSIPDOKTER.Text = sSIPDOKTER
        lblPenjamin.Text = sPENJAMIN
        'lblAdmisi.Text = slblAdmisi
        ssKODEBOOKING = sKODEBOOKING
    End Sub
    Public Sub LoadMe(ByVal FormMode As Integer, ByVal isFarmasi As Boolean, Optional ByVal NoId As String = "")
        oFormMode = FormMode
        sNoId = NoId
    End Sub
    Private Sub Form_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        fn_ChangeFormState()
        isLoad = True
        isSave = False
    End Sub
    Private Sub Form_Closing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        sCode = txtKDREQRECIPE.Text.Trim.ToUpper
    End Sub
    Private Overloads Sub Dispose()
        MyBase.Dispose()
        Me.Dispose(True)
        GC.SuppressFinalize(Me)
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
        btnSaveNew.Enabled = Not Status
        btnSaveClose.Enabled = Not Status
        btnCariObat.Enabled = Not Status
        btnRiwayatResep.Enabled = Not Status
        btnSKD.Enabled = Not Status
        btnBHP.Enabled = Not Status
        btnResumeRJ.Enabled = Not Status

        deDATE.Properties.ReadOnly = Status
        txtALERGIOBAT.Properties.ReadOnly = Status
        grvDetailResep.OptionsBehavior.Editable = Not Status
    End Sub
    Private Sub fn_EmptyMe()

    End Sub
    Private Sub fn_LoadData()
        Try
            ' ***** HEADER *****
            Dim ds = oReq_Recipe.GetData(sNoId)

            With ds
                txtKDREQRECIPE.Text = .KDREQRECIPE
                deDATE.DateTime = .DATE
                txtALERGIOBAT.Text = .ALERGIOBAT
                txtDIAGNOOSA.Text = .DIAGNOSA
                BindingSource.DataSource = oReq_Recipe.GetDataDetail(sNoId).OrderBy(Function(x) x.SEQ).ToList()
                grdDetailResep.DataSource = BindingSource

            End With
        Catch oErr As Exception
            MsgBox("Load List Data : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Function fn_Validate() As Boolean
        Try
            fn_Validate = True

            If lblRegister.Text = String.Empty Then
                MsgBox("Dibutuhkan No Register", MsgBoxStyle.Exclamation, Me.Text)
                lblRegister.Focus()
                fn_Validate = False
                Exit Function
            End If

            grvDetailResep.UpdateCurrentRow()

            If grvDetailResep.RowCount < 2 Then
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
            Dim oReq_Recipe As New Transaksi.clsReq_Recipe
            ' ***** HEADER *****
            Dim ds = oReq_Recipe.GetStructureHeader
            With ds
                Try
                    .DATECREATED = oReq_Recipe.GetData(sNoId).DATECREATED
                Catch ex As Exception
                    .DATECREATED = Now
                End Try
                .DATEUPDATED = Now
                .DATE = deDATE.DateTime
                .KDREQRECIPE = sNoId
                .NOANTRIAN = String.Empty
                .KDPENDAFTARAN = lblRegister.Text
                .KDWAREHOUSE = 1
                .ALERGIOBAT = IIf(txtALERGIOBAT.Text.ToString.Trim = String.Empty, "-", txtALERGIOBAT.Text)
                .BERATBADAN = txtBERATBADAN.Text
                Try
                    .DESCRIPTION = oReq_Recipe.GetData(sNoId).DESCRIPTION
                Catch ex As Exception
                    .DESCRIPTION = ""
                End Try
                Try
                    .ISCHEKED = oReq_Recipe.GetData(sNoId).ISCHEKED
                Catch ex As Exception
                    .ISCHEKED = False
                End Try
                Try
                    .KONFIRMASIRESEP = oReq_Recipe.GetData(sNoId).KONFIRMASIRESEP
                Catch ex As Exception
                    .KONFIRMASIRESEP = "Resep Belum Diterima"
                End Try
                .SUBTOTAL_RINCIAN = CDec(0)
                .SUBTOTAL_PAKET = CDec(0)
                .SUBTOTAL_KRONIS = CDec(0)
                .GRANDTOTAL = CDec(0)
                .NOIDUSER = sUserID
                .ISPERUBAHANRESEP = False
                .ISAPPROVAL = False
                .NOIDUSER_FARMASI = String.Empty
                .DESCRIPTION_PERUBAHAN = String.Empty
                .PENJAMIN = lblPenjamin.Text
                .KDCUSTOMER = lblRM.Text
                .PASIEN = lblNamaPasien.Text
                .ALAMAT = ""
                .DOKTER = lblDPJP.Text
                .TUJUAN = lblTUJUAN.Text
                .DIAGNOSA = txtDIAGNOOSA.Text
                .SIPDOKTER = lblSIPDOKTER.Text
                Try
                    .KDRECIPE = oReq_Recipe.GetData(sNoId).KDRECIPE
                Catch ex As Exception
                    .KDRECIPE = ""
                End Try
            End With

            Dim oObat As New Reference.clsItem
            Dim oSigna As New Reference.clsSigna
            Dim oCaraPakai As New Reference.clsCaraPakai

            Dim arrDetail = oReq_Recipe.GetStructureDetailList
            For i As Integer = 0 To grvDetailResep.RowCount - 2
                Dim dsDetail = oReq_Recipe.GetStructureDetail
                With dsDetail
                    .SEQ = i
                    .KDREQRECIPE = ds.KDREQRECIPE
                    .NAMAOBAT = IIf(String.IsNullOrEmpty(grvDetailResep.GetRowCellValue(i, colNAMAOBAT)), "", grvDetailResep.GetRowCellValue(i, colNAMAOBAT))
                    .SATUAN = IIf(String.IsNullOrEmpty(grvDetailResep.GetRowCellValue(i, colSATUAN)), "", grvDetailResep.GetRowCellValue(i, colSATUAN))
                    .SIGNA = IIf(String.IsNullOrEmpty(grvDetailResep.GetRowCellValue(i, colSIGNA)), "", grvDetailResep.GetRowCellValue(i, colSIGNA))
                    .CARAPAKAI = IIf(String.IsNullOrEmpty(grvDetailResep.GetRowCellValue(i, colCARAPAKAI)), "", grvDetailResep.GetRowCellValue(i, colCARAPAKAI))
                    .QTY = CDec(grvDetailResep.GetRowCellValue(i, colQTY))
                    .PRICE = CDec(0)
                    .GRANDTOTAL = CDec(0)
                    .ROMAWI = IntegerToRoman(CInt(grvDetailResep.GetFocusedRowCellValue(colQTY)))
                    .REMARKS_DOKTER = IIf(String.IsNullOrEmpty(grvDetailResep.GetRowCellValue(i, colREMARKS_DOKTER)), "", grvDetailResep.GetRowCellValue(i, colREMARKS_DOKTER))
                    .KDITEM = IIf(String.IsNullOrEmpty(grvDetailResep.GetRowCellValue(i, colKDITEM)), "18770", grvDetailResep.GetRowCellValue(i, colKDITEM))
                    .KDSIGNA = IIf(String.IsNullOrEmpty(grvDetailResep.GetRowCellValue(i, colKDSIGNA)), oSigna.DefaultSigna, grvDetailResep.GetRowCellValue(i, colKDSIGNA))
                    .KDUOM = IIf(String.IsNullOrEmpty(grvDetailResep.GetRowCellValue(i, colKDUOM)), oObat.GetDataDetail_UOM(oObat.GetDataByName("-").KDITEM).FirstOrDefault(Function(x) x.RATE = 1).KDUOM, grvDetailResep.GetRowCellValue(i, colKDUOM))
                    .KDCARAPAKAI = IIf(String.IsNullOrEmpty(grvDetailResep.GetRowCellValue(i, colKDCARAPAKAI)), oCaraPakai.DefaultCaraPakai, grvDetailResep.GetRowCellValue(i, colKDCARAPAKAI))
                    .QTY_PERUBAHAN = grvDetailResep.GetRowCellValue(i, colQTY)
                    .REMARKS_FARMASI = IIf(String.IsNullOrEmpty(grvDetailResep.GetRowCellValue(i, colREMARKS_FARMASI)), "", grvDetailResep.GetRowCellValue(i, colREMARKS_DOKTER))
                End With
                arrDetail.Add(dsDetail)
            Next

            Dim dsTelaah = oReq_Recipe.GetStructureTelaah

            With dsTelaah
                .DATECREATED = Now
                .DATEUPDATED = Now
                .KDREQRECIPE = ds.KDREQRECIPE
                .TELAAH_01 = False
                .TELAAH_02 = False
                .TELAAH_03 = False
                .TELAAH_04 = False
                .TELAAH_05 = False
                .TELAAH_06 = False
                .TELAAH_07 = False
                .TELAAH_08 = False
                .TELAAH_09 = False
                .TELAAH_10 = False
                .TELAAH_11 = False
                .TELAAH_12 = False
                .TELAAH_13 = False
                .TELAAH_14 = False
                .TELAAH_15 = False
                .TELAAH_16 = False
                .TELAAH_17 = False
                .TELAAH_18 = False
                .TELAAH_19 = False
                .TELAAH_20 = False
                .TELAAH_21 = False
                .TELAAH_22 = False
                .TELAAH_23 = False
                .TELAAH_24 = False
                .NOIDUSER = ""
                .REMARKS = ""
            End With

            Dim dsTelaahObat1 = oReq_Recipe.GetStructureTelaahObat1

            With dsTelaahObat1
                .DATECREATED = Now
                .DATEUPDATED = Now
                .TELAAH_01 = False
                .TELAAH_02 = False
                .TELAAH_03 = False
                .TELAAH_04 = False
                .TELAAH_05 = False
                .NOIDUSER = ""
                .REMARKS = ""
            End With

            Dim dsTelaahObat2 = oReq_Recipe.GetStructureTelaahObat2

            With dsTelaahObat2
                .DATECREATED = Now
                .DATEUPDATED = Now
                .TELAAH_01 = False
                .TELAAH_02 = False
                .TELAAH_03 = False
                .TELAAH_04 = False
                .TELAAH_05 = False
                .NOIDUSER = ""
                .REMARKS = ""
            End With

            If oFormMode = FORM_MODE.FORM_MODE_ADD Then
                Dim KDREQRECIPE As String = oReq_Recipe.InsertData(ds, arrDetail, dsTelaah, dsTelaahObat1, dsTelaahObat2)
                If KDREQRECIPE <> "" Then
                    fn_Save = True
                Else
                    fn_Save = False
                End If
            Else
                fn_Save = oReq_Recipe.UpdateData(ds, arrDetail)
            End If

        Catch oErr As Exception
            MsgBox("Simpan Data Resep : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            fn_Save = False
        End Try
    End Function
    Public Function IntegerToRoman(IntNumberValue As Integer) As String
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
    Private Sub OnValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles grvDetailResep.FocusedRowChanged
        Calculate()
    End Sub
    Private Sub Calculate()
        'Dim sTotal = 0

        'For i As Integer = 0 To grvDetailResep.RowCount - 2
        '    sTotal += CDec(grvDetailResep.GetRowCellValue(i, colGRANDTOTAL))
        'Next
        'txtGRANDTOTAL.Text = sTotal
    End Sub
    Private Sub grvDetail_CellValueChanged(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs)
        If e.Column.Name = colKDITEM.Name Then
            'Dim oItem As New Reference.clsItem
            Try
                'If grvDetailResep.GetFocusedRowCellValue(colKDITEM) IsNot Nothing Then
                '    Dim ds = oItem.GetDataDetail_UOM(grvDetailResep.GetFocusedRowCellValue(colKDITEM))

                '    If ds IsNot Nothing Then
                '        grvDetailResep.SetFocusedRowCellValue(colKDUOM, ds.FirstOrDefault(Function(x) x.RATE = 1).KDUOM)
                '        grvDetailResep.SetFocusedRowCellValue(colQTY_PERUBAHAN, 1)
                '        grvDetailResep.SetFocusedRowCellValue(colREMARKS_DOKTER, "")
                '    Else
                '        MsgBox("Satuan tidak terdaftar untuk obat ini, tolong kontak admin anda!", MsgBoxStyle.Critical, Me.Text)
                '        grvDetailResep.CancelUpdateCurrentRow()
                '    End If
                'End If
                grvDetailResep.SetFocusedRowCellValue(colQTY_PERUBAHAN, 1)
                grvDetailResep.SetFocusedRowCellValue(colREMARKS_DOKTER, "")
            Catch oErr As Exception
                MsgBox("Load Detail : " & vbCrLf & oErr.Message, MsgBoxStyle.Critical, Me.Text)
            End Try
        ElseIf e.Column.Name = colNAMAOBAT.Name Then
            If grvDetailResep.GetFocusedRowCellValue(colQTY) = 0 Then
                grvDetailResep.SetFocusedRowCellValue(colQTY, 1)
            End If
        ElseIf e.Column.Name = colQTY.Name Then
            Try
                If CDec(grvDetailResep.GetFocusedRowCellValue(colQTY)) > 0 Then
                    grvDetailResep.SetFocusedRowCellValue(colROMAWI, IntegerToRoman(CInt(grvDetailResep.GetFocusedRowCellValue(colQTY))))
                Else
                    grvDetailResep.SetFocusedRowCellValue(colROMAWI, "")
                End If
            Catch oErr As Exception
                MsgBox("Load Romawi : " & vbCrLf & oErr.Message, MsgBoxStyle.Critical, Me.Text)
                grvDetailResep.SetFocusedRowCellValue(colROMAWI, "")
            End Try
        End If
    End Sub
    Private Sub DeleteToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DeleteToolStripMenuItem.Click
        If oFormMode = FORM_MODE.FORM_MODE_VIEW Then Exit Sub
        grvDetailResep.DeleteSelectedRows()
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
            Case Keys.F6
                If btnCariObat.Enabled = True Then
                    btnCariObat_Click()
                End If
            Case Keys.F9
                btnFocus_Click()
            Case Keys.F12
                btnClose_Click()
        End Select
    End Sub
    Private Sub btnCariObat_Click() Handles btnCariObat.ItemClick
        Dim frmBrowseItemPOS As New frmBrowseItemPOS
        Try
            frmBrowseItemPOS.ShowDialog()
            If sKDITEM_PILIH <> "" Then
                frmObatFarmasi.ShowDialog(Me)

                grvDetailResep.Focus()
                grvDetailResep.AddNewRow()

                grvDetailResep.SetFocusedRowCellValue(colNAMAOBAT, fn_LoadITEM(sKDITEM_PILIH))
                grvDetailResep.SetFocusedRowCellValue(colSATUAN, fn_LoadUOM(sKDUOM_PILIH))
                grvDetailResep.SetFocusedRowCellValue(colSIGNA, fn_LoadSIGNA(sKDSIGNA_PILIH))
                grvDetailResep.SetFocusedRowCellValue(colCARAPAKAI, fn_LoadCARAPAKAI(sKDCARAPAKAI_PILIH))
                grvDetailResep.SetFocusedRowCellValue(colQTY, sQTY_PILIH)
                grvDetailResep.SetFocusedRowCellValue(colREMARKS_DOKTER, sREMARKS_PILIH)

                grvDetailResep.SetFocusedRowCellValue(colKDITEM, sKDITEM_PILIH)
                grvDetailResep.SetFocusedRowCellValue(colKDUOM, sKDUOM_PILIH)
                grvDetailResep.SetFocusedRowCellValue(colKDSIGNA, sKDSIGNA_PILIH)
                grvDetailResep.SetFocusedRowCellValue(colKDCARAPAKAI, sKDCARAPAKAI_PILIH)
                grvDetailResep.SetFocusedRowCellValue(colQTY_PERUBAHAN, sQTY_PILIH)
                grvDetailResep.SetFocusedRowCellValue(colREMARKS_FARMASI, sREMARKS_PILIH)
                grvDetailResep.UpdateCurrentRow()
            End If
        Catch ex As Exception
            MsgBox("Load Form Detail : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
        Finally
            If Not frmBrowseItemPOS Is Nothing Then frmBrowseItemPOS.Dispose()
            frmBrowseItemPOS = Nothing
        End Try
    End Sub
    Private Sub btnFocus_Click() Handles btnFocus.ItemClick
        grvDetailResep.Focus()
    End Sub
    Private Sub btnSaveNew_Click() Handles btnSaveNew.ItemClick
        If fn_Validate() = False Then Exit Sub
        If MsgBox("Save " & txtKDREQRECIPE.Text.Trim.ToUpper & "?", MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, Me.Text) = MsgBoxResult.No Then Exit Sub
        If fn_Save() = False Then
            MsgBox("Save gagal! Tolong kontak admin anda!", MsgBoxStyle.Exclamation, Me.Text)
        Else
            MsgBox("Save " & txtKDREQRECIPE.Text.Trim.ToUpper & " success!", MsgBoxStyle.Information, Me.Text)
            'sStatusSave = "NEW"
            'Me.Close()
            'fn_ChangeFormState()
            'oFormMode = FORM_MODE.FORM_MODE_EDIT

            Try
                If ssKODEBOOKING <> "" Then

                    Dim uTime As Integer = (DateTime.UtcNow - New DateTime(1970, 1, 1, 0, 0, 0)).TotalSeconds
                    Dim JsonRequest As String = fn_RequestUpdateWaktuAntrean(ssKODEBOOKING, 4, uTime)
                    If JsonRequest <> "" Then
                        fn_UpdateWaktuAntrean(JsonRequest, uTime)
                        'MsgBox(fn_UpdateWaktuAntrean(JsonRequest, uTime), MsgBoxStyle.Information, Me.Text)
                    End If
                End If
            Catch oErr As Exception
                MsgBox("Update BPJS Waktu Tunggu : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            End Try
        End If
    End Sub
    Private Sub btnSaveClose_Click() Handles btnSaveClose.ItemClick
        If fn_Validate() = False Then Exit Sub
        If MsgBox("Save " & txtKDREQRECIPE.Text.Trim.ToUpper & "?", MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, Me.Text) = MsgBoxResult.No Then Exit Sub
        If fn_Save() = False Then
            MsgBox("Save gagal! Tolong kontak admin anda!", MsgBoxStyle.Exclamation, Me.Text)
        Else
            MsgBox("Save " & txtKDREQRECIPE.Text.Trim.ToUpper & " success!", MsgBoxStyle.Information, Me.Text)
            Try
                If ssKODEBOOKING <> "" Then

                    Dim uTime As Integer = (DateTime.UtcNow - New DateTime(1970, 1, 1, 0, 0, 0)).TotalSeconds
                    Dim JsonRequest As String = fn_RequestUpdateWaktuAntrean(ssKODEBOOKING, 4, uTime)
                    If JsonRequest <> "" Then
                        fn_UpdateWaktuAntrean(JsonRequest, uTime)
                        'MsgBox(fn_UpdateWaktuAntrean(JsonRequest, uTime), MsgBoxStyle.Information, Me.Text)
                    End If
                End If
            Catch oErr As Exception
                MsgBox("Update BPJS Waktu Tunggu : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            End Try
            Me.Close()
        End If
    End Sub
    Private Sub btnClose_Click() Handles btnClose.ItemClick
        Me.Close()
    End Sub
    Private Function fn_RequestUpdateWaktuAntrean(ByVal KODEBOOKING As String, ByVal ISPANGGIL As Integer, ByVal uTime As Integer) As String
        Try
            Dim jsonRequest As String = String.Empty

            jsonRequest = " { "
            jsonRequest &= """kodebooking"": """ & KODEBOOKING & ""","
            jsonRequest &= """taskid"": """ & ISPANGGIL & """, "
            jsonRequest &= """waktu"": """ & uTime & """ "
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
                MsgBox(fn_UpdateWaktuAntrean, MsgBoxStyle.Information, Me.Text)
            End If

        Catch oErr As Exception
            fn_UpdateWaktuAntrean = oErr.Message
        End Try
    End Function
#End Region
#Region "Lookup / Event"
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
    Private Function fn_LoadUOM(ByVal KDUOM As String) As String
        Try
            fn_LoadUOM = ""

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
                    fn_LoadUOM = .Rows(iLoop)("DESCRIPTION")
                End With
            Next

            If oConn.State = ConnectionState.Open Then
                oConn.Close()
            End If
        Catch oErr As Exception
            fn_LoadUOM = ""
            MsgBox("Load Satuan" & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
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
#End Region
End Class