Imports DataAccess
Imports UI.WIN.MAIN.My.Resources
Imports System.Linq
Imports System.Data.SqlClient

Public Class frmResepIGD_LoadAwal
#Region "Declaration"
    Private oFormMode As FORM_MODE = FORM_MODE.FORM_MODE_VIEW
    Private isLoad As Boolean = False
    Private oS_DIGITAL_IGD_01 As New Transaksi.clsDigital_IGD_01
    Private sNoId As String
#End Region
#Region "Function"
    Public Sub LoadMe(ByVal FormMode As Integer, Optional ByVal NoId As String = "")
        oFormMode = FormMode
        sNoId = NoId
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
        'fn_NOIDUSER()
        fn_LoadKDDOCTOR()
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
    End Sub
    Private Sub fn_EmptyMe()
        txtCODE.Text = "<--- AUTO --->"

    End Sub
    Private Sub fn_LoadData()
        Try
            ' ***** HEADER *****
            Dim ds = oS_DIGITAL_IGD_01.GetData(sNoId)

            With ds
                deDATE.DateTime = .DATE

                'BindingSource.DataSource = oS_DIGITAL_IGD_01.GetDataDetailResepObatPulang("IGD_" & sKDREG).OrderBy(Function(x) x.SEQ).ToList()
                'grdDetailResep.DataSource = BindingSource

                'BindingSource1.DataSource = oS_DIGITAL_IGD_01.GetDataDetailResepSelamaIGD("IGD_" & sKDREG).OrderBy(Function(x) x.SEQ).ToList()
                'grdObatIGD.DataSource = BindingSource1

                'BindingSource2.DataSource = oS_DIGITAL_IGD_01.GetDataDetailResepRanap("IGD_" & sKDREG).OrderBy(Function(x) x.SEQ).ToList()
                'grdObatRanap.DataSource = BindingSource2

                'BindingSourceTindakanPoli.DataSource = oS_DIGITAL_IGD_01.GetDataDetailTindakanPoli(sKDREG).OrderBy(Function(x) x.SEQ).ToList()
                'grdTindakanPoli.DataSource = BindingSourceTindakanPoli

                'BindingSourcePenunjang.DataSource = oS_DIGITAL_IGD_01.GetDataDetailPenunjang(sKDREG).OrderBy(Function(x) x.SEQ).ToList()
                'grdTindakan.DataSource = BindingSourcePenunjang
            End With

            'Dim oReq_Recipe As New Transaksi.clsReq_Recipe
            'Dim dsResep = oReq_Recipe.GetData("IGD_" & sKDREG)
            'If dsResep IsNot Nothing Then
            '    txtGRANDTOTAL.Text = dsResep.GRANDTOTAL
            'End If
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
            'If sKDREG = String.Empty Then
            '    MsgBox("Dibutuhkan Register", MsgBoxStyle.Exclamation, Me.Text)
            '    fn_Validate = False
            '    Exit Function
            'End If
            'If txtPETUGAS_TRIAGE.Text = String.Empty Then
            '    MsgBox("Dibutuhkan Petugas Triage", MsgBoxStyle.Exclamation, Me.Text)
            '    txtPETUGAS_TRIAGE.Focus()
            '    fn_Validate = False
            '    Exit Function
            'End If
            'If txtPERAWAT.Text = String.Empty Then
            '    If txtPETUGAS_TRIAGE.Text = String.Empty Then
            '        MsgBox("Dibutuhkan Perawat", MsgBoxStyle.Exclamation, Me.Text)
            '        txtPERAWAT.Focus()
            '        fn_Validate = False
            '        Exit Function
            '    Else
            '        txtPERAWAT.Text = txtPETUGAS_TRIAGE.Text
            '    End If
            'End If
            'If grdKDDOCTOR.Text = String.Empty Then
            '    MsgBox("Dibutuhkan Dokter", MsgBoxStyle.Exclamation, Me.Text)
            '    grdKDDOCTOR.Focus()
            '    fn_Validate = False
            '    Exit Function
            'End If

        Catch oErr As Exception
            MsgBox("Validate Data : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Function
    Private Function fn_Save() As Boolean
        'Try
        '    Dim sTindakan As Integer = 0
        '    Dim sTerapi As Integer = 0

        '    Dim listObatPulang As New List(Of String)
        '    Dim listObatIGD As New List(Of String)
        '    Dim listObatRanap As New List(Of String)

        '    For i As Integer = 0 To grvDetailResep.RowCount - 2
        '        listObatPulang.Add(fn_LoadITEM(grvDetailResep.GetRowCellValue(i, colKDITEM)) & " No " & IntegerToRoman(CInt(grvDetailResep.GetRowCellValue(i, colQTY))) & " " & fn_LoadUOMDESCRIPTION(grvDetailResep.GetRowCellValue(i, colKDUOM)) & " " & IIf(String.IsNullOrEmpty(grvDetailResep.GetRowCellValue(i, colKDSIGNA)), "", fn_LoadSIGNA(grvDetailResep.GetRowCellValue(i, colKDSIGNA))) & " " & IIf(String.IsNullOrEmpty(grvDetailResep.GetRowCellValue(i, colKDCARAPAKAI)), "", fn_LoadCARAPAKAI(grvDetailResep.GetRowCellValue(i, colKDCARAPAKAI))) & " " & IIf(String.IsNullOrEmpty(grvDetailResep.GetRowCellValue(i, colREMARKS_DOKTER)), "", grvDetailResep.GetRowCellValue(i, colREMARKS_DOKTER)))
        '    Next
        '    For i As Integer = 0 To grvObatIGD.RowCount - 2
        '        listObatIGD.Add(fn_LoadITEM(grvObatIGD.GetRowCellValue(i, colKDITEM_ObatIGD)) & " No " & IntegerToRoman(CInt(grvObatIGD.GetRowCellValue(i, colQTY_ObatIGD))) & " " & fn_LoadUOMDESCRIPTION(grvObatIGD.GetRowCellValue(i, colKDUOM_ObatIGD)) & " " & IIf(String.IsNullOrEmpty(grvObatIGD.GetRowCellValue(i, colKDSIGNA_ObatIGD)), "", fn_LoadSIGNA(grvObatIGD.GetRowCellValue(i, colKDSIGNA_ObatIGD))) & " " & IIf(String.IsNullOrEmpty(grvObatIGD.GetRowCellValue(i, colKDCARAPAKAI_ObatIGD)), "", fn_LoadCARAPAKAI(grvObatIGD.GetRowCellValue(i, colKDCARAPAKAI_ObatIGD))) & " " & IIf(String.IsNullOrEmpty(grvObatIGD.GetRowCellValue(i, colREMARKS_DOKTER_ObatIGD)), "", grvObatIGD.GetRowCellValue(i, colREMARKS_DOKTER_ObatIGD)))
        '    Next
        '    For i As Integer = 0 To grvObatRanap.RowCount - 2
        '        listObatRanap.Add(fn_LoadITEM(grvObatRanap.GetRowCellValue(i, colKDITEM_ObatRanap)) & " No " & IntegerToRoman(CInt(grvObatRanap.GetRowCellValue(i, colQTY_ObatRanap))) & " " & fn_LoadUOMDESCRIPTION(grvObatRanap.GetRowCellValue(i, colKDUOM_ObatRanap)) & " " & IIf(String.IsNullOrEmpty(grvObatRanap.GetRowCellValue(i, colKDSIGNA_ObatRanap)), "", fn_LoadSIGNA(grvObatRanap.GetRowCellValue(i, colKDSIGNA_ObatRanap))) & " " & IIf(String.IsNullOrEmpty(grvObatRanap.GetRowCellValue(i, colKDCARAPAKAI_ObatRanap)), "", fn_LoadCARAPAKAI(grvObatRanap.GetRowCellValue(i, colKDCARAPAKAI_ObatRanap))) & " " & IIf(String.IsNullOrEmpty(grvObatRanap.GetRowCellValue(i, colREMARKS_DOKTER_ObatRanap)), "", grvObatRanap.GetRowCellValue(i, colREMARKS_DOKTER_ObatRanap)))
        '    Next

        '    ' ***** HEADER *****
        '    Dim ds = oS_DIGITAL_IGD_01.GetStructureHeader
        '    With ds
        '        .DATE = deDATE.DateTime
        '        .KDUSER_SIGNATURE = ""
        '    End With

        '    Dim oReq_Recipe As New Transaksi.clsReq_Recipe
        '    '' ***** HEADER *****
        '    'Dim dsResepH = oReq_Recipe.GetStructureHeader
        '    'With dsResepH
        '    '    Try
        '    '        .DATECREATED = oReq_Recipe.GetData(sNoId).DATECREATED
        '    '    Catch ex As Exception
        '    '        .DATECREATED = Now
        '    '    End Try
        '    '    .DATEUPDATED = Now
        '    '    .DATE = deDATE.DateTime
        '    '    .KDREQRECIPE = sNoId
        '    '    .NOANTRIAN = String.Empty
        '    '    .KDPENDAFTARAN = ""
        '    '    .KDWAREHOUSE = 1
        '    '    Try
        '    '        .ALERGIOBAT = oReq_Recipe.GetData(sNoId).ALERGIOBAT
        '    '    Catch ex As Exception
        '    '        .ALERGIOBAT = ""
        '    '    End Try
        '    '    Try
        '    '        .BERATBADAN = oReq_Recipe.GetData("IGD_" & sKDREG).BERATBADAN
        '    '    Catch ex As Exception
        '    '        .BERATBADAN = ""
        '    '    End Try
        '    '    Try
        '    '        .DESCRIPTION = oReq_Recipe.GetData("IGD_" & sKDREG).DESCRIPTION
        '    '    Catch ex As Exception
        '    '        .DESCRIPTION = ""
        '    '    End Try
        '    '    Try
        '    '        .ISCHEKED = oReq_Recipe.GetData("IGD_" & sKDREG).ISCHEKED
        '    '    Catch ex As Exception
        '    '        .ISCHEKED = False
        '    '    End Try
        '    '    Try
        '    '        .KONFIRMASIRESEP = oReq_Recipe.GetData("IGD_" & sKDREG).KONFIRMASIRESEP
        '    '    Catch ex As Exception
        '    '        .KONFIRMASIRESEP = "Resep Belum Diterima"
        '    '    End Try
        '    '    .SUBTOTAL_RINCIAN = CDec(0)
        '    '    .SUBTOTAL_PAKET = CDec(0)
        '    '    .SUBTOTAL_KRONIS = CDec(0)

        '    '    Calculate()

        '    '    .GRANDTOTAL = CDec(txtGRANDTOTAL.Text)
        '    '    .NOIDUSER = sUserID
        '    '    .ISPERUBAHANRESEP = False
        '    '    .ISAPPROVAL = False
        '    '    .NOIDUSER_FARMASI = String.Empty
        '    '    .DESCRIPTION_PERUBAHAN = String.Empty
        '    '    .PENJAMIN = sPENJAMIN
        '    '    .KDCUSTOMER = sKDCUSTOMER
        '    '    .PASIEN = sNAMAPASIEN
        '    '    .ALAMAT = ""
        '    '    .DOKTER = grdKDDOCTOR.Text
        '    '    .TUJUAN = sTUJUAN
        '    '    Try
        '    '        .DIAGNOSA = oReq_Recipe.GetData("IGD_" & sKDREG).DIAGNOSA
        '    '    Catch ex As Exception
        '    '        .DIAGNOSA = ""
        '    '    End Try
        '    '    .SIPDOKTER = sSIPDOKTER
        '    '    Try
        '    '        .KDRECIPE = oReq_Recipe.GetData("IGD_" & sKDREG).KDRECIPE
        '    '    Catch ex As Exception
        '    '        .KDRECIPE = ""
        '    '    End Try
        '    'End With

        '    Dim arrDetail = oReq_Recipe.GetStructureDetailList
        '    For i As Integer = 0 To grvDetailResep.RowCount - 2
        '        Dim dsDetail = oReq_Recipe.GetStructureDetail
        '        With dsDetail
        '            sTerapi += 1

        '            .SEQ = i
        '            .KDREQRECIPE = dsResepH.KDREQRECIPE
        '            .NAMAOBAT = fn_LoadITEM(grvDetailResep.GetRowCellValue(i, colKDITEM))
        '            .SATUAN = fn_LoadUOMDESCRIPTION(grvDetailResep.GetRowCellValue(i, colKDUOM))
        '            .SIGNA = IIf(String.IsNullOrEmpty(grvDetailResep.GetRowCellValue(i, colKDSIGNA)), "", fn_LoadSIGNA(grvDetailResep.GetRowCellValue(i, colKDSIGNA)))
        '            .CARAPAKAI = IIf(String.IsNullOrEmpty(grvDetailResep.GetRowCellValue(i, colKDCARAPAKAI)), "", fn_LoadCARAPAKAI(grvDetailResep.GetRowCellValue(i, colKDCARAPAKAI)))
        '            .QTY = CDec(grvDetailResep.GetRowCellValue(i, colQTY))
        '            .PRICE = CDec(grvDetailResep.GetRowCellValue(i, colPRICE))
        '            .GRANDTOTAL = CDec(grvDetailResep.GetRowCellValue(i, colGRANDTOTAL))
        '            .ROMAWI = IntegerToRoman(CInt(grvDetailResep.GetFocusedRowCellValue(colQTY)))
        '            .REMARKS_DOKTER = IIf(String.IsNullOrEmpty(grvDetailResep.GetRowCellValue(i, colREMARKS_DOKTER)), "", grvDetailResep.GetRowCellValue(i, colREMARKS_DOKTER))
        '            .KDITEM = grvDetailResep.GetRowCellValue(i, colKDITEM)
        '            .KDUOM = grvDetailResep.GetRowCellValue(i, colKDUOM)
        '            .KDSIGNA = IIf(String.IsNullOrEmpty(grvDetailResep.GetRowCellValue(i, colKDSIGNA)), "1286", grvDetailResep.GetRowCellValue(i, colKDSIGNA))
        '            .KDCARAPAKAI = IIf(String.IsNullOrEmpty(grvDetailResep.GetRowCellValue(i, colKDCARAPAKAI)), "10", grvDetailResep.GetRowCellValue(i, colKDCARAPAKAI))
        '            .QTY_PERUBAHAN = grvDetailResep.GetRowCellValue(i, colQTY)
        '            .REMARKS_FARMASI = "OBAT PULANG"
        '        End With

        '        If grvDetailResep.GetRowCellValue(i, colKDITEM) IsNot Nothing Then
        '            arrDetail.Add(dsDetail)
        '        End If
        '    Next

        '    Dim i_ObatIGD As Integer = 100
        '    For i As Integer = 0 To grvObatIGD.RowCount - 2
        '        Dim dsDetail = oReq_Recipe.GetStructureDetail
        '        With dsDetail
        '            sTerapi += 1

        '            .SEQ = i_ObatIGD
        '            .KDREQRECIPE = dsResepH.KDREQRECIPE
        '            .NAMAOBAT = fn_LoadITEM(grvObatIGD.GetRowCellValue(i, colKDITEM_ObatIGD))
        '            .SATUAN = fn_LoadUOMDESCRIPTION(grvObatIGD.GetRowCellValue(i, colKDUOM_ObatIGD))
        '            .SIGNA = IIf(String.IsNullOrEmpty(grvObatIGD.GetRowCellValue(i, colKDSIGNA_ObatIGD)), "", fn_LoadSIGNA(grvObatIGD.GetRowCellValue(i, colKDSIGNA_ObatIGD)))
        '            .CARAPAKAI = IIf(String.IsNullOrEmpty(grvObatIGD.GetRowCellValue(i, colKDCARAPAKAI_ObatIGD)), "", fn_LoadCARAPAKAI(grvObatIGD.GetRowCellValue(i, colKDCARAPAKAI_ObatIGD)))
        '            .QTY = CDec(grvObatIGD.GetRowCellValue(i, colQTY_ObatIGD))
        '            .PRICE = CDec(grvObatIGD.GetRowCellValue(i, colPRICE_ObatIGD))
        '            .GRANDTOTAL = CDec(grvObatIGD.GetRowCellValue(i, colGRANDTOTAL_ObatIGD))
        '            .ROMAWI = IntegerToRoman(CInt(grvObatIGD.GetFocusedRowCellValue(colQTY_ObatIGD)))
        '            .REMARKS_DOKTER = IIf(String.IsNullOrEmpty(grvObatIGD.GetRowCellValue(i, colREMARKS_DOKTER_ObatIGD)), "", grvObatIGD.GetRowCellValue(i, colREMARKS_DOKTER_ObatIGD))
        '            .KDITEM = grvObatIGD.GetRowCellValue(i, colKDITEM_ObatIGD)
        '            .KDUOM = grvObatIGD.GetRowCellValue(i, colKDUOM_ObatIGD)
        '            .KDSIGNA = IIf(String.IsNullOrEmpty(grvObatIGD.GetRowCellValue(i, colKDSIGNA_ObatIGD)), "1286", grvObatIGD.GetRowCellValue(i, colKDSIGNA_ObatIGD))
        '            .KDCARAPAKAI = IIf(String.IsNullOrEmpty(grvObatIGD.GetRowCellValue(i, colKDCARAPAKAI_ObatIGD)), "10", grvObatIGD.GetRowCellValue(i, colKDCARAPAKAI_ObatIGD))
        '            .QTY_PERUBAHAN = grvObatIGD.GetRowCellValue(i, colQTY_ObatIGD)
        '            .REMARKS_FARMASI = "OBAT IGD"

        '            i_ObatIGD += 1
        '        End With

        '        If grvObatIGD.GetRowCellValue(i, colKDITEM_ObatIGD) IsNot Nothing Then
        '            arrDetail.Add(dsDetail)
        '        End If
        '    Next

        '    Dim i_ObatRanap As Integer = 200
        '    For i As Integer = 0 To grvObatRanap.RowCount - 2
        '        Dim dsDetail = oReq_Recipe.GetStructureDetail
        '        With dsDetail
        '            sTerapi += 1

        '            .SEQ = i_ObatRanap
        '            .KDREQRECIPE = dsResepH.KDREQRECIPE
        '            .NAMAOBAT = fn_LoadITEM(grvObatRanap.GetRowCellValue(i, colKDITEM_ObatRanap))
        '            .SATUAN = fn_LoadUOMDESCRIPTION(grvObatRanap.GetRowCellValue(i, colKDUOM_ObatRanap))
        '            .SIGNA = IIf(String.IsNullOrEmpty(grvObatRanap.GetRowCellValue(i, colKDSIGNA_ObatRanap)), "", fn_LoadSIGNA(grvObatRanap.GetRowCellValue(i, colKDSIGNA_ObatRanap)))
        '            .CARAPAKAI = IIf(String.IsNullOrEmpty(grvObatRanap.GetRowCellValue(i, colKDCARAPAKAI_ObatRanap)), "", fn_LoadCARAPAKAI(grvObatRanap.GetRowCellValue(i, colKDCARAPAKAI_ObatRanap)))
        '            .QTY = CDec(grvObatRanap.GetRowCellValue(i, colQTY_ObatRanap))
        '            .PRICE = CDec(grvObatRanap.GetRowCellValue(i, colPRICE_ObatRanap))
        '            .GRANDTOTAL = CDec(grvObatRanap.GetRowCellValue(i, colGRANDTOTAL_ObatRanap))
        '            .ROMAWI = IntegerToRoman(CInt(grvObatRanap.GetFocusedRowCellValue(colQTY_ObatRanap)))
        '            .REMARKS_DOKTER = IIf(String.IsNullOrEmpty(grvObatRanap.GetRowCellValue(i, colREMARKS_DOKTER_ObatRanap)), "", grvObatRanap.GetRowCellValue(i, colREMARKS_DOKTER_ObatRanap))
        '            .KDITEM = grvObatRanap.GetRowCellValue(i, colKDITEM_ObatRanap)
        '            .KDUOM = grvObatRanap.GetRowCellValue(i, colKDUOM_ObatRanap)
        '            .KDSIGNA = IIf(String.IsNullOrEmpty(grvObatRanap.GetRowCellValue(i, colKDSIGNA_ObatRanap)), "1286", grvObatRanap.GetRowCellValue(i, colKDSIGNA_ObatRanap))
        '            .KDCARAPAKAI = IIf(String.IsNullOrEmpty(grvObatRanap.GetRowCellValue(i, colKDCARAPAKAI_ObatRanap)), "10", grvObatRanap.GetRowCellValue(i, colKDCARAPAKAI_ObatRanap))
        '            .QTY_PERUBAHAN = grvObatRanap.GetRowCellValue(i, colQTY_ObatRanap)
        '            .REMARKS_FARMASI = "OBAT RANAP"

        '            i_ObatRanap += 1
        '        End With

        '        If grvObatRanap.GetRowCellValue(i, colKDITEM_ObatRanap) IsNot Nothing Then
        '            arrDetail.Add(dsDetail)
        '        End If
        '    Next


        '    Dim arrDetailTindakanPoli = oReq_Recipe.GetStructureDetailTindakanPoliList
        '    For i As Integer = 0 To grvTindakanPoli.RowCount - 2
        '        Dim dsDetail = oReq_Recipe.GetStructureDetailTindakanPoli
        '        With dsDetail
        '            .SEQ = i
        '            .KDPENDAFTARAN = ds.KDPENDAFTARAN
        '            .PENANGANAN = grvTindakanPoli.GetRowCellValue(i, colPENANGANAN)
        '        End With
        '        arrDetailTindakanPoli.Add(dsDetail)
        '    Next

        '    Dim arrDetailPenunjang = oReq_Recipe.GetStructureDetailPenunjangList
        '    For i As Integer = 0 To grvTindakan.RowCount - 2
        '        Dim dsDetail = oReq_Recipe.GetStructureDetailPenunjang
        '        With dsDetail
        '            .SEQ = i
        '            .KDPENDAFTARAN = ds.KDPENDAFTARAN
        '            .PENANGANAN = grvTindakan.GetRowCellValue(i, colPENANGANAN_)
        '        End With
        '        arrDetailPenunjang.Add(dsDetail)
        '    Next


        '    If oFormMode = FORM_MODE.FORM_MODE_ADD Then
        '        Try
        '            fn_Save = oS_DIGITAL_IGD_01.InsertData(ds, dsResepH, arrDetail, dsTelaah, dsTelaahObat1, dsTelaahObat2, arrDetailTindakanPoli, arrDetailPenunjang, dsKoding, dsKodingUtama, arrDetailDiagnosaPenyertaTindakan, arrDetailTindakan)
        '        Catch ex As Exception
        '            MsgBox("Simpan Data : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
        '        End Try
        '    ElseIf oFormMode = FORM_MODE.FORM_MODE_EDIT Then
        '        Try
        '            fn_Save = oS_DIGITAL_IGD_01.UpdateData(ds, dsResepH, arrDetail, arrDetailTindakanPoli, arrDetailPenunjang, dsKoding, dsKodingUtama, arrDetailDiagnosaPenyertaTindakan, arrDetailTindakan)
        '        Catch ex As Exception
        '            MsgBox("Simpan Data : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
        '        End Try
        '    End If

        'Catch oErr As Exception
        '    MsgBox("Simpan Data : " & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        '    fn_Save = False
        'End Try
    End Function
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

            grdCARAPAKAI_ObatIGD.DataSource = ds.Tables("CARAPAKAI")
            grdCARAPAKAI_ObatIGD.ValueMember = "KDCP"
            grdCARAPAKAI_ObatIGD.DisplayMember = "DESCRIPTION"

            grdKDCARAPAKAI_ObatRanap.DataSource = ds.Tables("CARAPAKAI")
            grdKDCARAPAKAI_ObatRanap.ValueMember = "KDCP"
            grdKDCARAPAKAI_ObatRanap.DisplayMember = "DESCRIPTION"

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

            grdKDSIGNA_ObatIGD.DataSource = ds.Tables("SIGNA")
            grdKDSIGNA_ObatIGD.ValueMember = "KDSIGNA"
            grdKDSIGNA_ObatIGD.DisplayMember = "DESCRIPTION"

            grdKDSIGNA_ObatRanap.DataSource = ds.Tables("SIGNA")
            grdKDSIGNA_ObatRanap.ValueMember = "KDSIGNA"
            grdKDSIGNA_ObatRanap.DisplayMember = "DESCRIPTION"

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

            grdKDITEM_ObatIGD.DataSource = ds.Tables("ITEM")
            grdKDITEM_ObatIGD.ValueMember = "KDITEM"
            grdKDITEM_ObatIGD.DisplayMember = "NMITEM2"

            grdKDITEM_ObatRanap.DataSource = ds.Tables("ITEM")
            grdKDITEM_ObatRanap.ValueMember = "KDITEM"
            grdKDITEM_ObatRanap.DisplayMember = "NMITEM2"

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

            grdKDUOM_ObatIGD.DataSource = ds.Tables("UOM")
            grdKDUOM_ObatIGD.ValueMember = "KDUOM"
            grdKDUOM_ObatIGD.DisplayMember = "DESCRIPTION"

            grdKDUOM_ObatRanap.DataSource = ds.Tables("UOM")
            grdKDUOM_ObatRanap.ValueMember = "KDUOM"
            grdKDUOM_ObatRanap.DisplayMember = "DESCRIPTION"

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
    Private Sub OnValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles grvDetailResep.FocusedRowChanged, grvObatIGD.FocusedRowChanged, grvObatRanap.FocusedRowChanged
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
        For i As Integer = 0 To grvObatIGD.RowCount - 2
            sSubTotal += CDec(grvObatIGD.GetRowCellValue(i, colGRANDTOTAL_ObatIGD))
        Next
        For i As Integer = 0 To grvObatRanap.RowCount - 2
            sSubTotal += CDec(grvObatRanap.GetRowCellValue(i, colGRANDTOTAL_ObatRanap))
        Next

        txtGRANDTOTAL.Text = sSubTotal
        'txtOBATSAATPULANG.Text = String.Join(", ", list.ToArray)
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
    Private Sub grvObatIGD_CellValueChanged(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs) Handles grvObatIGD.CellValueChanged
        If e.Column.Name = colKDITEM_ObatIGD.Name Then
            Try
                If grvObatIGD.GetFocusedRowCellValue(colKDITEM_ObatIGD) IsNot Nothing Then
                    grvObatIGD.SetFocusedRowCellValue(colKDUOM, fn_LoadUOMKDUOM(grvObatIGD.GetFocusedRowCellValue(colKDITEM_ObatIGD)))
                    grvObatIGD.SetFocusedRowCellValue(colREMARKS_DOKTER_ObatIGD, "")
                    grvObatIGD.SetFocusedRowCellValue(colQTY_ObatIGD, 0)
                End If
            Catch oErr As Exception
                MsgBox("Load Detail : " & vbCrLf & oErr.Message, MsgBoxStyle.Critical, Me.Text)
            End Try
        ElseIf e.Column.Name = colREMARKS_DOKTER_ObatIGD.Name Then
            'grvTindakan.Focus()
            'grvTindakan.AddNewRow()
            'grvTindakan.SetFocusedRowCellValue(colTERAPI, fn_LoadITEM(grvObatIGD.GetFocusedRowCellValue(colKDITEM_ObatIGD)) & " " & fn_LoadSIGNA(grvObatIGD.GetFocusedRowCellValue(colKDSIGNA)))
            'grvTindakan.SetFocusedRowCellValue(colTINDAKAN, "")
            'grvTindakan.UpdateCurrentRow()
        ElseIf e.Column.Name = colKDUOM_ObatIGD.Name Then
            grvObatIGD.SetFocusedRowCellValue(colPRICE_ObatIGD, fn_LoadUOMKDUOMHARGA(grvObatIGD.GetFocusedRowCellValue(colKDITEM_ObatIGD), grvObatIGD.GetFocusedRowCellValue(colKDUOM_ObatIGD)))
        ElseIf e.Column.Name = colQTY_ObatIGD.Name Or e.Column.Name = colPRICE_ObatIGD.Name Then
            Dim sSubTotal As Decimal = CDec(grvObatIGD.GetFocusedRowCellValue(colQTY_ObatIGD)) * CDec(grvObatIGD.GetFocusedRowCellValue(colPRICE_ObatIGD))
            grvObatIGD.SetFocusedRowCellValue(colGRANDTOTAL_ObatIGD, sSubTotal)
        End If
    End Sub
    Private Sub grvObatRanap_CellValueChanged(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs) Handles grvObatRanap.CellValueChanged
        If e.Column.Name = colKDITEM_ObatRanap.Name Then
            Try
                If grvObatRanap.GetFocusedRowCellValue(colKDITEM_ObatRanap) IsNot Nothing Then
                    grvObatRanap.SetFocusedRowCellValue(colKDUOM, fn_LoadUOMKDUOM(grvObatRanap.GetFocusedRowCellValue(colKDITEM_ObatRanap)))
                    grvObatRanap.SetFocusedRowCellValue(colREMARKS_DOKTER_ObatRanap, "")
                    grvObatRanap.SetFocusedRowCellValue(colQTY_ObatRanap, 0)
                End If
            Catch oErr As Exception
                MsgBox("Load Detail : " & vbCrLf & oErr.Message, MsgBoxStyle.Critical, Me.Text)
            End Try
        ElseIf e.Column.Name = colREMARKS_DOKTER_ObatRanap.Name Then
            'grvTindakan.Focus()
            'grvTindakan.AddNewRow()
            'grvTindakan.SetFocusedRowCellValue(colTERAPI, fn_LoadITEM(grvObatRanap.GetFocusedRowCellValue(colKDITEM_ObatIGD)) & " " & fn_LoadSIGNA(grvObatRanap.GetFocusedRowCellValue(colKDSIGNA)))
            'grvTindakan.SetFocusedRowCellValue(colTINDAKAN, "")
            'grvTindakan.UpdateCurrentRow()
        ElseIf e.Column.Name = colKDUOM_ObatRanap.Name Then
            grvObatRanap.SetFocusedRowCellValue(colPRICE_ObatRanap, fn_LoadUOMKDUOMHARGA(grvObatRanap.GetFocusedRowCellValue(colKDITEM_ObatRanap), grvObatRanap.GetFocusedRowCellValue(colKDUOM_ObatRanap)))
        ElseIf e.Column.Name = colQTY_ObatRanap.Name Or e.Column.Name = colPRICE_ObatRanap.Name Then
            Dim sSubTotal As Decimal = CDec(grvObatRanap.GetFocusedRowCellValue(colQTY_ObatRanap)) * CDec(grvObatRanap.GetFocusedRowCellValue(colPRICE_ObatRanap))
            grvObatRanap.SetFocusedRowCellValue(colGRANDTOTAL_ObatRanap, sSubTotal)
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
    Private Sub fn_LoadKDDOCTOR()
        Dim oDoctor As New Reference.clsDoctor
        Try
            grdKDDOCTOR.Properties.DataSource = oDoctor.GetData.Where(Function(x) x.ISACTIVE = True).ToList()
            grdKDDOCTOR.Properties.ValueMember = "KDDOCTOR"
            grdKDDOCTOR.Properties.DisplayMember = "NAME_DISPLAY"

        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub DeleteToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DeleteToolStripMenuItem.Click
        grvDetailResep.DeleteSelectedRows()
    End Sub
    Private Sub ToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem1.Click
        grvObatIGD.DeleteSelectedRows()
    End Sub
    Private Sub ToolStripMenuItem2_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem2.Click
        grvObatRanap.DeleteSelectedRows()
    End Sub
#End Region
End Class