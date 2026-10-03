Imports DataAccess
Imports UI.WIN.MAIN.My.Resources
Imports System.Linq
Imports System.Data.SqlClient

Public Class frmEMedrekRJ_34
#Region "Declaration"
    Private oFormMode As FORM_MODE = FORM_MODE.FORM_MODE_VIEW
    Private isLoad As Boolean = False
    Private oS_DIGITAL_RJ_34 As New Digital.clsDigital_RJ_34
    Private down As Boolean = False
    Private sKDDOKTER As String = String.Empty
    Private sRUANGAN As String = String.Empty
#End Region
#Region "Function"
    Public Sub LoadMe(ByVal FormMode As Integer, ByVal KDREG As String, ByVal KDCUSTOMER As String, ByVal NAMAPASIEN As String, ByVal JENISKELAMIN As String, ByVal DPJP As String, ByVal KDDPJP As String, ByVal RUANGAN As String)
        oFormMode = FormMode

        txtNOREG.Text = KDREG
        txtNAMA.Text = NAMAPASIEN
        txtJK.Text = JENISKELAMIN
        txtNOMORRM.Text = KDCUSTOMER
        sKDDOKTER = KDDPJP
        sRUANGAN = RUANGAN
    End Sub
    Private Sub Form_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        fn_ChangeFormState()
        isLoad = True
    End Sub
    Private Sub Form_Closing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        Dispose()
        sCode = txtNOREG.Text.Trim.ToUpper
        sLoadAsesmenAwal = False
    End Sub
    Private Overloads Sub Dispose()
        MyBase.Dispose()
        Me.Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
    Private Sub fn_ChangeFormState()
        fn_DOCTOR()

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
        btnReload.Enabled = Not Status
        btnDiagnosa.Enabled = Not Status

        txt1.Properties.ReadOnly = Status
        txt2.Properties.ReadOnly = Status
        txt3.Properties.ReadOnly = Status
        txt4.Properties.ReadOnly = Status
        txt5.Properties.ReadOnly = Status
        txt6.Properties.ReadOnly = Status
        txt7.Properties.ReadOnly = Status
        txt8.Properties.ReadOnly = Status
        txt9.Properties.ReadOnly = Status
        TextEdit4.Properties.ReadOnly = Status
        TextEdit5.Properties.ReadOnly = Status
        TextEdit6.Properties.ReadOnly = Status
        TextEdit7.Properties.ReadOnly = Status
        TextEdit2.Properties.ReadOnly = Status
        TextEdit1.Properties.ReadOnly = Status
        TextEdit3.Properties.ReadOnly = Status
        TextEdit8.Properties.ReadOnly = Status
        TextEdit9.Properties.ReadOnly = Status
        TextEdit10.Properties.ReadOnly = Status
        TextEdit11.Properties.ReadOnly = Status
        TextEdit12.Properties.ReadOnly = Status
        TextEdit13.Properties.ReadOnly = Status
        TextEdit14.Properties.ReadOnly = Status
        TextEdit15.Properties.ReadOnly = Status
        TextEdit16.Properties.ReadOnly = Status
        TextEdit17.Properties.ReadOnly = Status
        MemoEdit7.Properties.ReadOnly = Status
        MemoEdit4.Properties.ReadOnly = Status
        MemoEdit2.Properties.ReadOnly = Status
        MemoEdit3.Properties.ReadOnly = Status
        MemoEdit5.Properties.ReadOnly = Status
        MemoEdit6.Properties.ReadOnly = Status
        txtSDIGITALRJ34_27_TEXT.Properties.ReadOnly = Status
        txtTHORAX.Properties.ReadOnly = Status

        'Dim oSetUser As New Setting.clsUser
        'Dim dsUser = oSetUser.GetData(sUserID)
        'If dsUser IsNot Nothing Then
        '    If dsUser.ISOTORTY = True Then
        '        lTanggal.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        '        sIsOtority = True
        '    Else
        '        lTanggal.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        '        sIsOtority = False
        '    End If
        'End If
    End Sub
    Private Sub fn_EmptyMe()
        'txtCODE.Text = "<--- AUTO --->"
        txt1.ResetText()
        txt2.ResetText()
        txt3.ResetText()
        txt4.ResetText()
        txt5.ResetText()
        txt6.ResetText()
        txt7.ResetText()
        txt8.ResetText()
        txt9.ResetText()
        TextEdit4.ResetText()
        TextEdit5.ResetText()
        TextEdit6.ResetText()
        TextEdit7.ResetText()
        TextEdit2.ResetText()
        TextEdit1.ResetText()
        TextEdit3.ResetText()
        TextEdit8.ResetText()
        TextEdit9.ResetText()
        TextEdit10.ResetText()
        TextEdit11.ResetText()
        TextEdit12.ResetText()
        TextEdit13.ResetText()
        TextEdit14.ResetText()
        TextEdit15.ResetText()
        TextEdit16.ResetText()
        TextEdit17.ResetText()
        MemoEdit7.ResetText()
        MemoEdit4.ResetText()
        MemoEdit2.ResetText()
        MemoEdit3.ResetText()
        MemoEdit5.ResetText()
        MemoEdit6.ResetText()
        txtSDIGITALRJ34_27_TEXT.ResetText()
        txtTHORAX.ResetText()
        grdDOCTOR.Text = sKDDOKTER
        'fn_LoadAsessmenRawatJalan(txtNOREG.Text)
        txtWAKTU.Text = Now.ToString("dd-MM-yyyy HH:mm")
    End Sub
    Private Sub fn_LoadData()
        Try
            ' ***** HEADER *****
            Dim ds = oS_DIGITAL_RJ_34.GetData(txtNOREG.Text)

            With ds
                grdDOCTOR.EditValue = .KDDOCTOR_KODE
                txt1.Text = .SDIGITALRJ34_1
                txt2.Text = .SDIGITALRJ34_2
                txt3.Text = .SDIGITALRJ34_3
                txt4.Text = .SDIGITALRJ34_4
                txt5.Text = .SDIGITALRJ34_5
                txt6.Text = .SDIGITALRJ34_6
                txt7.Text = .SDIGITALRJ34_7
                txt8.Text = .SDIGITALRJ34_8
                txt9.Text = .SDIGITALRJ34_9
                TextEdit4.Text = .SDIGITALRJ34_10
                TextEdit5.Text = .SDIGITALRJ34_11
                TextEdit6.Text = .SDIGITALRJ34_12
                TextEdit7.Text = .SDIGITALRJ34_13
                TextEdit2.Text = .SDIGITALRJ34_14
                TextEdit1.Text = .SDIGITALRJ34_15
                TextEdit3.Text = .SDIGITALRJ34_16
                TextEdit8.Text = .SDIGITALRJ34_17
                TextEdit9.Text = .SDIGITALRJ34_18
                TextEdit10.Text = .SDIGITALRJ34_19
                TextEdit11.Text = .SDIGITALRJ34_20
                TextEdit12.Text = .SDIGITALRJ34_21
                TextEdit13.Text = .SDIGITALRJ34_22
                TextEdit14.Text = .SDIGITALRJ34_23
                TextEdit15.Text = .SDIGITALRJ34_24
                TextEdit16.Text = .SDIGITALRJ34_25
                TextEdit17.Text = .SDIGITALRJ34_26
                Try
                    Dim img = oS_DIGITAL_RJ_34.GetData(txtNOREG.Text).SDIGITALRJ34_27

                    picGAMBAR2.Image = ByteArrayToImage(img.ToArray())
                Catch oErr As Exception
                End Try
                MemoEdit7.Text = .SDIGITALRJ34_28
                MemoEdit4.Text = .SDIGITALRJ34_29
                MemoEdit2.Text = .SDIGITALRJ34_30
                MemoEdit3.Text = .SDIGITALRJ34_31
                MemoEdit5.Text = .SDIGITALRJ34_32
                MemoEdit6.Text = .SDIGITALRJ34_33
                txtSDIGITALRJ34_27_TEXT.Text = .SDIGITALRJ34_27_TEXT
                txtTHORAX.Text = .SDIGITALRJ34_THORAX

            End With
        Catch oErr As Exception
            MsgBox("Load List Data : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    'Private Sub fn_LoadAsessmenRawatJalan(ByVal Parameter As String)
    '    Dim oAsessmenRawatJalan As New Digital.clsDigital_RJ_08
    '    Dim dsAsessmenRawatJalan = oAsessmenRawatJalan.GetData(Parameter)

    '    Dim oAsessmenRawatJalan2 As New Digital.clsS_DIGITAL_ASKEP_RAWATJALAN
    '    Dim dsAsessmenRawatJalan2 = oAsessmenRawatJalan2.GetDatabyKDKUNJUNGAN(Parameter)

    '    If dsAsessmenRawatJalan IsNot Nothing Then
    '        'txtSOAP_O.Text = "Tensi : " & dsAsessmenRawatJalan.TANDA_VITAL_01 & " mmHg" & vbCrLf & vbCrLf & "Nadi : " & dsAsessmenRawatJalan.TANDA_VITAL_02 & " x/m" & vbCrLf & vbCrLf & "Respirasi : " & dsAsessmenRawatJalan.TANDA_VITAL_04 & " x/m" & vbCrLf & vbCrLf & "Suhu : " & dsAsessmenRawatJalan.TANDA_VITAL_03 & " °C" & vbCrLf & vbCrLf & "Berat Badan : " & dsAsessmenRawatJalan.TANDA_VITAL_05 & " Kg" & vbCrLf & vbCrLf & "Tinggi Badan : " & dsAsessmenRawatJalan.TANDA_VITAL_06 & " Cm"
    '        txt1.Text = dsAsessmenRawatJalan.KELUHAN_UTAMA
    '        txt7.Text = dsAsessmenRawatJalan.TANDA_VITAL_05
    '        txt8.Text = dsAsessmenRawatJalan.TANDA_VITAL_06
    '        TextEdit4.Text = dsAsessmenRawatJalan.TANDA_VITAL_02
    '        TextEdit5.Text = dsAsessmenRawatJalan.TANDA_VITAL_01
    '        TextEdit6.Text = dsAsessmenRawatJalan.TANDA_VITAL_03
    '        TextEdit7.Text = dsAsessmenRawatJalan.TANDA_VITAL_04
    '        txt5.Text = dsAsessmenRawatJalan.KESADARAN_UMUM
    '        txt4.Text = dsAsessmenRawatJalan.ALERGI_02_02_TEXT
    '    ElseIf dsAsessmenRawatJalan2 IsNot Nothing Then
    '        txt1.Text = dsAsessmenRawatJalan2.KELUHAN_UTAMA
    '        txt7.Text = dsAsessmenRawatJalan2.TANDA_VITAL_BB
    '        txt8.Text = dsAsessmenRawatJalan2.TANDA_VITAL_TB
    '        TextEdit4.Text = dsAsessmenRawatJalan2.TANDA_VITAL_NADI
    '        TextEdit5.Text = dsAsessmenRawatJalan2.TANDA_VITAL_TD
    '        TextEdit6.Text = dsAsessmenRawatJalan2.TANDA_VITAL_SUHU
    '        TextEdit7.Text = dsAsessmenRawatJalan2.TANDA_VITAL_R
    '        txt5.Text = dsAsessmenRawatJalan2.KESADARAN_UMUM
    '        txt4.Text = dsAsessmenRawatJalan2.KEADAAN
    '    Else
    '        Exit Sub
    '    End If
    'End Sub
    Private Function ByteArrayToImage(ByVal byteArrayIn() As Byte) As Image
        Using ms As New System.IO.MemoryStream(byteArrayIn)
            Dim returnImage = Image.FromStream(ms)
            Return returnImage
        End Using
    End Function
    Private Function fn_Validate() As Boolean
        Try
            fn_Validate = True
            If txtNOREG.Text = String.Empty Then
                MsgBox("Dibutuhkan Register", MsgBoxStyle.Exclamation, Me.Text)
                txtNOREG.Focus()
                fn_Validate = False
                Exit Function
            End If
            If grdDOCTOR.Text = String.Empty Then
                MsgBox("Dibutuhkan Dokter", MsgBoxStyle.Exclamation, Me.Text)
                grdDOCTOR.Focus()
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
            Dim ds = oS_DIGITAL_RJ_34.GetStructureHeader
            With ds
                .KDPENDAFTARAN = txtNOREG.Text
                .KDCUSTOMER = txtNOMORRM.Text
                Try
                    .DATECREATED = Now
                Catch ex As Exception
                    .DATECREATED = Now
                End Try
                .DATE = Now
                .DATEUPDATED = Now
                .KDDOCTOR_KODE = grdDOCTOR.EditValue
                .KDDOCTOR_NAME_DISPLAY = grdDOCTOR.Text
                .DEPARTMENT_KODE = ""
                .PERAWAT = ""
                .DEPARTMENT_NAME_DISPLAY = sRUANGAN
                .SDIGITALRJ34_1 = txt1.Text
                .SDIGITALRJ34_2 = txt2.Text
                .SDIGITALRJ34_3 = txt3.Text
                .SDIGITALRJ34_4 = txt4.Text
                .SDIGITALRJ34_5 = txt5.Text
                .SDIGITALRJ34_6 = txt6.Text
                .SDIGITALRJ34_7 = txt7.Text
                .SDIGITALRJ34_8 = txt8.Text
                .SDIGITALRJ34_9 = txt9.Text
                .SDIGITALRJ34_10 = TextEdit4.Text
                .SDIGITALRJ34_11 = TextEdit5.Text
                .SDIGITALRJ34_12 = TextEdit6.Text
                .SDIGITALRJ34_13 = TextEdit7.Text
                .SDIGITALRJ34_14 = TextEdit2.Text
                .SDIGITALRJ34_15 = TextEdit1.Text
                .SDIGITALRJ34_16 = TextEdit3.Text
                .SDIGITALRJ34_17 = TextEdit8.Text
                .SDIGITALRJ34_18 = TextEdit9.Text
                .SDIGITALRJ34_19 = TextEdit10.Text
                .SDIGITALRJ34_20 = TextEdit11.Text
                .SDIGITALRJ34_21 = TextEdit12.Text
                .SDIGITALRJ34_22 = TextEdit13.Text
                .SDIGITALRJ34_23 = TextEdit14.Text
                .SDIGITALRJ34_24 = TextEdit15.Text
                .SDIGITALRJ34_25 = TextEdit16.Text
                .SDIGITALRJ34_26 = TextEdit17.Text
                Try
                    Dim ms As New IO.MemoryStream()
                    picGAMBAR2.Image.Save(ms, picGAMBAR2.Image.RawFormat)

                    Dim data As Byte() = ms.GetBuffer()

                    .SDIGITALRJ34_27 = data
                Catch oErr As Exception
                    Try
                        .SDIGITALRJ34_27 = oS_DIGITAL_RJ_34.GetData(txtNOREG.Text).SDIGITALRJ34_27
                    Catch ex As Exception

                    End Try
                End Try
                .SDIGITALRJ34_27_TEXT = txtSDIGITALRJ34_27_TEXT.Text
                .SDIGITALRJ34_28 = MemoEdit7.Text
                .SDIGITALRJ34_29 = MemoEdit4.Text
                .SDIGITALRJ34_30 = MemoEdit2.Text
                .SDIGITALRJ34_31 = MemoEdit3.Text
                .SDIGITALRJ34_32 = MemoEdit5.Text
                .SDIGITALRJ34_33 = MemoEdit6.Text
                .SDIGITALRJ34_THORAX = txtTHORAX.Text
                Try
                    .CETAK = oS_DIGITAL_RJ_34.GetData(txtNOREG.Text).CETAK
                Catch ex As Exception
                    .CETAK = 0
                End Try
                .KDUSER = sUserID
                .KDUSE_SIGNATURE = ""
            End With

            If oFormMode = FORM_MODE.FORM_MODE_ADD Then
                Try
                    fn_Save = oS_DIGITAL_RJ_34.InsertData(ds)
                Catch ex As Exception
                    MsgBox("Simpan Data : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            ElseIf oFormMode = FORM_MODE.FORM_MODE_EDIT Then
                Try
                    fn_Save = oS_DIGITAL_RJ_34.UpdateData(ds)
                Catch ex As Exception
                    MsgBox("Simpan Data : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            End If
        Catch oErr As Exception
            MsgBox("Simpan Data : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            fn_Save = False
        End Try
    End Function
#End Region
#Region "Command Button"
    Private Sub frmItem_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        Select Case e.KeyCode
            Case Keys.F12
                btnClose_Click()
            Case Keys.F3
                If btnSaveClose.Enabled = True Then
                    btnSaveClose_Click()
                End If
            Case Keys.PageUp
                fn_ScrollPage(True)
            Case Keys.PageDown
	            fn_ScrollPage(False)
        End Select
    End Sub
    'Private Sub btnDiagnosa_Click() Handles btnDiagnosa.ItemClick
    '    Dim oMasterDiagnosa As New Diagnosa.clsMasterDiagnosa
    '    Dim dsMasterDiagnosa = oMasterDiagnosa.GetData(txtNOREG.Text)
    '    If dsMasterDiagnosa IsNot Nothing Then
    '        Dim frmMasterDiagnosa As New frmMasterDiagnosa
    '        frmMasterDiagnosa.LoadMe(FORM_MODE.FORM_MODE_EDIT, txtNOREG.Text)
    '        frmMasterDiagnosa.ShowDialog(Me)
    '    Else
    '        Dim frmMasterDiagnosa As New frmMasterDiagnosa
    '        frmMasterDiagnosa.LoadMe(FORM_MODE.FORM_MODE_ADD, txtNOREG.Text)
    '        frmMasterDiagnosa.ShowDialog(Me)
    '    End If

    '    If sCode = "Berhasil" Then
    '        Dim listDiagnosa As New List(Of String)
    '        For Each xloop In oMasterDiagnosa.GetDataDetail(txtNOREG.Text)
    '            listDiagnosa.Add(xloop.KATEGORI & " : " & xloop.REMARKS)
    '        Next

    '        Dim listProsedur As New List(Of String)
    '        For Each xloop In oMasterDiagnosa.GetDataDetail_(txtNOREG.Text)
    '            listProsedur.Add(xloop.SEQ + 1 & ". " & xloop.REMARKS)
    '        Next
    '        MemoEdit4.Text = String.Join(vbCrLf, listProsedur.ToArray)
    '        MemoEdit2.Text = String.Join(vbCrLf, listDiagnosa.ToArray)
    '    End If
    'End Sub
    'Private Sub btnReload_Click() Handles btnReload.ItemClick
    '    Dim dsKunjungan = oS_DIGITAL_RJ_34.GetDataByKunjungan(txtNOREG.Text)

    '    Dim listPenunjang As New List(Of String)
    '    Dim listTindakanPengobatan As New List(Of String)

    '    If dsKunjungan IsNot Nothing Then
    '        Dim oOrderTindakan As New Inventory.clsOrderTindakan
    '        Dim oOrderLab As New Inventory.clsOrderLab
    '        Dim oOrderRad As New Inventory.clsOrderRad
    '        Dim oKonsul As New Digital.clsKonsul
    '        Dim oKonsulJawab As New Digital.clsJawabKonsul

    '        Dim dsTindakan = From x In oOrderTindakan.GetDataPendaftaranList(dsKunjungan.KDPENDAFTARAN)
    '                         Join y In oOrderTindakan.GetDataDetail()
    '                         On x.KDORDERTINDAKAN Equals y.KDORDERTINDAKAN
    '                         Select TANGGAL = x.DATE, ITEM = "Tindakan : " & y.TARIFKT & " " & y.REMARKS & ", "

    '        Dim dsLab = From x In oOrderLab.GetDataPendaftaranList(dsKunjungan.KDPENDAFTARAN)
    '                    Join y In oOrderLab.GetDataDetail()
    '                    On x.KDORDERLAB Equals y.KDORDERLAB
    '                    Select TANGGAL = x.DATE, ITEM = "Lab : " & y.TARIFKT & " " & y.REMARKS & ", "

    '        Dim dsRad = From x In oOrderRad.GetDataPendaftaranList(dsKunjungan.KDPENDAFTARAN)
    '                    Join y In oOrderRad.GetDataDetail()
    '                    On x.KDORDERRAD Equals y.KDORDERRAD
    '                    Select TANGGAL = x.DATE, ITEM = "Rad : " & y.TARIFKT & " " & y.REMARKS & ", "

    '        Dim dsKonsul = From x In oKonsul.GetDataPendaftaranList(dsKunjungan.KDPENDAFTARAN)
    '                       Select TANGGAL = x.DATE, ITEM = "Konsul : " & "Dari " & x.DOKTER_DARI & " Kepada " & x.DOKTER_KEPADA & " Isi " & x.MEMO & ", "

    '        Dim dsJawabKonsul = From x In oKonsulJawab.GetDataPendaftaranList(dsKunjungan.KDPENDAFTARAN)
    '                            Select TANGGAL = x.DATE, ITEM = "Jawab Konsul : " & "Dari " & x.DOKTER_DARI & " Kepada " & x.DOKTER_KEPADA & " Isi " & x.MEMO & ", "

    '        Dim dsUnionPenunjang = dsLab.Union(dsRad)

    '        For Each xloop In dsUnionPenunjang.OrderBy(Function(x) x.TANGGAL)
    '            listPenunjang.Add("*) " & xloop.TANGGAL.ToString("dd-MM-yyy HH:mm:ss") & " " & xloop.ITEM)
    '        Next

    '        Dim dsUnionTindakanKonsul = dsTindakan.Union(dsKonsul).Union(dsJawabKonsul)

    '        For Each xloop In dsUnionTindakanKonsul.OrderBy(Function(x) x.TANGGAL)
    '            listTindakanPengobatan.Add("*) " & xloop.TANGGAL.ToString("dd-MM-yyy HH:mm:ss") & " " & xloop.ITEM)
    '        Next

    '        Dim oResep As New Inventory.clsOrderResep

    '        Dim dsResep = oResep.GetDataPendaftaranList(dsKunjungan.KDPENDAFTARAN)

    '        For Each xloop In dsResep
    '            For Each yloop In oResep.GetDataDetail(xloop.KDORDERRESEP)
    '                listTindakanPengobatan.Add(yloop.TARIFKT & " " & yloop.SIGNA)
    '            Next
    '        Next

    '        MemoEdit7.Text = String.Join(", ", listPenunjang.ToArray)
    '        MemoEdit3.Text = String.Join(", ", listTindakanPengobatan.ToArray)
    '    End If
    'End Sub
    Private Sub btnSaveClose_Click() Handles btnSaveClose.ItemClick
        If fn_Validate() = False Then Exit Sub
        If MsgBox("Save " & txtNOREG.Text.Trim.ToUpper & "?", MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, Me.Text) = MsgBoxResult.No Then Exit Sub
        If fn_Save() = False Then
            MsgBox("Save gagal! Tolong kontak admin anda!", MsgBoxStyle.Exclamation, Me.Text)
        Else
            MsgBox("Save " & txtNOREG.Text.Trim.ToUpper & " success!", MsgBoxStyle.Information, Me.Text)
            Me.Close()
        End If
    End Sub
    Private Sub btnClose_Click() Handles btnClose.ItemClick
        Me.Close()
    End Sub
#End Region
#Region "Lookup / Event"
    Private Sub fn_NOIDUSER()
        'Dim oUser As New Setting.clsUser
        'Try
        '    grdNOIDUSER.Properties.DataSource = oUser.GetData.Where(Function(x) x.ISACTIVE = True).ToList()
        '    grdNOIDUSER.Properties.ValueMember = "NOIDUSER"
        '    grdNOIDUSER.Properties.DisplayMember = "NOIDUSER"
        'Catch oErr As Exception
        '    MsgBox("Load Sub Spesialis Data : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        'End Try
    End Sub
    Private Sub fn_DOCTOR()
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
            SQL &= "M_DOCTOR A "
            SQL &= "WHERE "
            SQL &= "A.ISACTIVE = 1 "

            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "DOKTER")

            grdDOCTOR.Properties.DataSource = ds.Tables("DOKTER")
            grdDOCTOR.Properties.ValueMember = "KDDOCTOR"
            grdDOCTOR.Properties.DisplayMember = "NAME_DISPLAY"

            If oConn.State = ConnectionState.Open Then
                oConn.Close()
            End If
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub btnEDITIMAGE_Click(sender As Object, e As EventArgs) Handles btnEDITIMAGE.Click
        Dim frmPopUp_Image As New frmPopUp_img1
        frmPopUp_Image.fn_LoadMe(picGAMBAR2.Image)
        frmPopUp_Image.ShowDialog()

        If sPicture IsNot Nothing Then
            picGAMBAR2.Image = sPicture
        End If

        picGAMBAR2.Focus()
        sFind10 = String.Empty
        sPicture = Nothing
    End Sub

    Private Sub SimpleButton1_Click(sender As Object, e As EventArgs) Handles SimpleButton1.Click
        TextEdit2.Text = "konjungtiva tidak anemis, sklera tidak ikterik"
        TextEdit1.Text = "dalam batas normal"
        TextEdit3.Text = "dalam batas normal"
        TextEdit8.Text = "dalam batas normal"
        TextEdit9.Text = "dalam batas normal"
        TextEdit10.Text = "kgb tidak teraba membesar"
        TextEdit11.Text = "dalam batas normal"
        TextEdit12.Text = "dalam batas normal"
        TextEdit13.Text = "dalam batas normal"
        TextEdit14.Text = "dalam batas normal"
        TextEdit15.Text = "dalam batas normal"
        TextEdit16.Text = "dalam batas normal"
        TextEdit17.Text = "dalam batas normal"
    End Sub
    Private Sub SimpleButton2_Click(sender As Object, e As EventArgs) Handles SimpleButton2.Click
        TextEdit13.Text = "dalam batas normal"
        TextEdit14.Text = "tidak membesar"
        TextEdit15.Text = "tidak membesar"
        TextEdit16.Text = "dalam batas normal"
        TextEdit17.Text = "dalam batas normal"
    End Sub

    Private Sub SimpleButton3_Click(sender As Object, e As EventArgs) Handles SimpleButton3.Click
        TextEdit11.Text = "VBS (+)/(+)"
    End Sub

    Private Sub SimpleButton4_Click(sender As Object, e As EventArgs) Handles SimpleButton4.Click
        TextEdit11.Text = "ronchi (-)/(-)"
    End Sub

    Private Sub SimpleButton6_Click(sender As Object, e As EventArgs) Handles SimpleButton6.Click
        TextEdit11.Text = "Weezing (-)/(-)"
    End Sub

    Private Sub SimpleButton5_Click(sender As Object, e As EventArgs) Handles SimpleButton5.Click
        txtTHORAX.Text = "bentuk dan gerak simetris"
    End Sub

    Private Sub SimpleButton7_Click(sender As Object, e As EventArgs) Handles SimpleButton7.Click
        TextEdit12.Text = "BJ 1"
    End Sub

    Private Sub SimpleButton8_Click(sender As Object, e As EventArgs) Handles SimpleButton8.Click
        TextEdit12.Text = "BJ 2 dalam batas normal"
    End Sub

    Private Sub SimpleButton9_Click(sender As Object, e As EventArgs) Handles SimpleButton9.Click
        TextEdit12.Text = "murmur (-)"
    End Sub

    Private Sub frmEMedrekRJ_34_MouseWheel(sender As Object, e As MouseEventArgs) Handles MyBase.MouseWheel
	    If e.Delta > 0 Then
		    'up
		    fn_ScrollPage(True)
	    Else
		    'down
		    fn_ScrollPage(False)
	    End If
    End Sub

    Private Sub fn_ScrollPage(ByVal isUp As Boolean)
	    Dim myView As Point = Me.Panel3.AutoScrollPosition
	    Dim scrollchange As Integer = 50

	    If isUp Then
		    'up
		    myView.X = -myView.X
		    myView.y = -scrollchange - myView.Y
	    Else
		    'down
		    myView.X = -myView.X
		    myView.y = scrollchange - myView.Y
	    End If

	    Me.Panel3.AutoScrollPosition = myView
    End Sub
#End Region
End Class