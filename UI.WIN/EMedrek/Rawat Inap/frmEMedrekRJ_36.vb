Imports DataAccess
Imports UI.WIN.MAIN.My.Resources
Imports System.Linq
Imports System.Data.SqlClient

Public Class frmEMedrekRJ_36
#Region "Declaration"
    Private oFormMode As FORM_MODE = FORM_MODE.FORM_MODE_VIEW
    Private isLoad As Boolean = False
    Private oS_DIGITAL_RJ_36 As New Digital.clsDigital_RJ_36
    Private down As Boolean = False
    Private sRUANGAN As String = String.Empty

#End Region
#Region "Function"
    Public Sub LoadMe(ByVal FormMode As Integer, ByVal KDREG As String, ByVal KDCUSTOMER As String, ByVal NAMAPASIEN As String, ByVal JENISKELAMIN As String, ByVal DPJP As String, ByVal RUANGAN As String)
        oFormMode = FormMode

        txtNOREG.Text = KDREG
        txtNOMORRM.Text = KDCUSTOMER
        txtNAMA.Text = NAMAPASIEN
        txtDOCTOR.Text = DPJP
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
        fn_NOIDUSER()
        fn_DOCTOR()
        fn_DEPARTMENT()

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
        TextEdit1.Properties.ReadOnly = Status
        TextEdit17.Properties.ReadOnly = Status
        TextEdit2.Properties.ReadOnly = Status
        TextEdit3.Properties.ReadOnly = Status
        TextEdit8.Properties.ReadOnly = Status
        TextEdit9.Properties.ReadOnly = Status
        TextEdit11.Properties.ReadOnly = Status
        TextEdit10.Properties.ReadOnly = Status
        TextEdit12.Properties.ReadOnly = Status
        MemoEdit4.Properties.ReadOnly = Status
        MemoEdit2.Properties.ReadOnly = Status
        MemoEdit3.Properties.ReadOnly = Status
        MemoEdit5.Properties.ReadOnly = Status
        MemoEdit6.Properties.ReadOnly = Status
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
        TextEdit1.ResetText()
        TextEdit17.ResetText()
        TextEdit2.ResetText()
        TextEdit3.ResetText()
        TextEdit8.ResetText()
        TextEdit9.ResetText()
        TextEdit11.ResetText()
        TextEdit10.ResetText()
        TextEdit12.ResetText()
        MemoEdit4.ResetText()
        MemoEdit2.ResetText()
        MemoEdit3.ResetText()
        MemoEdit5.ResetText()
        MemoEdit6.ResetText()

        deDATE.DateTime = Now

        fn_LoadAsessmenRawatJalan(txtNOREG.Text)
    End Sub
    Private Sub fn_LoadData()
        Try
            ' ***** HEADER *****
            Dim ds = oS_DIGITAL_RJ_36.GetData(txtNOREG.Text)

            With ds
                txtDOCTOR.Text = .DOKTER_NAMEDISPLAY
                'grdDEPARTMENT.Text = ""
                txt1.Text = .SDIGITALRJ36_1
                txt2.Text = .SDIGITALRJ36_2
                txt3.Text = .SDIGITALRJ36_3
                txt4.Text = .SDIGITALRJ36_4
                txt5.Text = .SDIGITALRJ36_5
                txt6.Text = .SDIGITALRJ36_6
                txt7.Text = .SDIGITALRJ36_7
                txt8.Text = .SDIGITALRJ36_8
                txt9.Text = .SDIGITALRJ36_9
                TextEdit4.Text = .SDIGITALRJ36_10
                TextEdit5.Text = .SDIGITALRJ36_11
                TextEdit6.Text = .SDIGITALRJ36_12
                TextEdit7.Text = .SDIGITALRJ36_13
                CheckEdit5.Checked = .SDIGITALRJ36_14
                TextEdit1.Text = .SDIGITALRJ36_15
                CheckEdit1.Checked = .SDIGITALRJ36_16
                TextEdit17.Text = .SDIGITALRJ36_17
                CheckEdit2.Checked = .SDIGITALRJ36_18
                TextEdit2.Text = .SDIGITALRJ36_19
                CheckEdit3.Checked = .SDIGITALRJ36_20
                CheckEdit4.Checked = .SDIGITALRJ36_21
                CheckEdit7.Checked = .SDIGITALRJ36_22
                CheckEdit6.Checked = .SDIGITALRJ36_23
                TextEdit3.Text = .SDIGITALRJ36_24
                Try
                    Dim img = oS_DIGITAL_RJ_36.GetData(txtNOREG.Text).SDIGITALRJ36_25

                    picGAMBAR2.Image = ByteArrayToImage(img.ToArray())
                Catch oErr As Exception
                End Try
                CheckEdit9.Checked = .SDIGITALRJ36_26
                CheckEdit8.Checked = .SDIGITALRJ36_27
                CheckEdit10.Checked = .SDIGITALRJ36_28
                TextEdit8.Text = .SDIGITALRJ36_29
                TextEdit9.Text = .SDIGITALRJ36_30
                CheckEdit13.Checked = .SDIGITALRJ36_31
                TextEdit11.Text = .SDIGITALRJ36_32
                CheckEdit12.Checked = .SDIGITALRJ36_33
                TextEdit10.Text = .SDIGITALRJ36_34
                CheckEdit11.Checked = .SDIGITALRJ36_35
                TextEdit12.Text = .SDIGITALRJ36_36
                MemoEdit4.Text = .SDIGITALRJ36_37
                MemoEdit2.Text = .SDIGITALRJ36_38
                MemoEdit3.Text = .SDIGITALRJ36_39
                MemoEdit5.Text = .SDIGITALRJ36_40
                MemoEdit6.Text = .SDIGITALRJ36_41

                deDATE.DateTime = .DATE
            End With
        Catch oErr As Exception
            MsgBox("Load List Data : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadAsessmenRawatJalan(ByVal Parameter As String)
        'Dim oAsessmenRawatJalan As New Digital.clsDigital_RJ_08
        'Dim dsAsessmenRawatJalan = oAsessmenRawatJalan.GetData(Parameter)

        'Dim oAsessmenRawatJalan2 As New Digital.clsS_DIGITAL_ASKEP_RAWATJALAN
        'Dim dsAsessmenRawatJalan2 = oAsessmenRawatJalan2.GetDatabyKDKUNJUNGAN(Parameter)

        'If dsAsessmenRawatJalan IsNot Nothing Then
        '    'txtSOAP_O.Text = "Tensi : " & dsAsessmenRawatJalan.TANDA_VITAL_01 & " mmHg" & vbCrLf & vbCrLf & "Nadi : " & dsAsessmenRawatJalan.TANDA_VITAL_02 & " x/m" & vbCrLf & vbCrLf & "Respirasi : " & dsAsessmenRawatJalan.TANDA_VITAL_04 & " x/m" & vbCrLf & vbCrLf & "Suhu : " & dsAsessmenRawatJalan.TANDA_VITAL_03 & " °C" & vbCrLf & vbCrLf & "Berat Badan : " & dsAsessmenRawatJalan.TANDA_VITAL_05 & " Kg" & vbCrLf & vbCrLf & "Tinggi Badan : " & dsAsessmenRawatJalan.TANDA_VITAL_06 & " Cm"
        '    txt1.Text = dsAsessmenRawatJalan.KELUHAN_UTAMA
        '    txt7.Text = dsAsessmenRawatJalan.TANDA_VITAL_05
        '    txt8.Text = dsAsessmenRawatJalan.TANDA_VITAL_06
        '    TextEdit4.Text = dsAsessmenRawatJalan.TANDA_VITAL_02
        '    TextEdit5.Text = dsAsessmenRawatJalan.TANDA_VITAL_01
        '    TextEdit6.Text = dsAsessmenRawatJalan.TANDA_VITAL_03
        '    TextEdit7.Text = dsAsessmenRawatJalan.TANDA_VITAL_04
        '    txt5.Text = dsAsessmenRawatJalan.KESADARAN_UMUM
        '    txt4.Text = dsAsessmenRawatJalan.ALERGI_02_02_TEXT
        'ElseIf dsAsessmenRawatJalan2 IsNot Nothing Then
        '    txt1.Text = dsAsessmenRawatJalan2.KELUHAN_UTAMA
        '    txt7.Text = dsAsessmenRawatJalan2.TANDA_VITAL_BB
        '    txt8.Text = dsAsessmenRawatJalan2.TANDA_VITAL_TB
        '    TextEdit4.Text = dsAsessmenRawatJalan2.TANDA_VITAL_NADI
        '    TextEdit5.Text = dsAsessmenRawatJalan2.TANDA_VITAL_TD
        '    TextEdit6.Text = dsAsessmenRawatJalan2.TANDA_VITAL_SUHU
        '    TextEdit7.Text = dsAsessmenRawatJalan2.TANDA_VITAL_R
        '    txt5.Text = dsAsessmenRawatJalan2.KESADARAN_UMUM
        '    txt4.Text = dsAsessmenRawatJalan2.KEADAAN
        'Else
        '    Exit Sub
        'End If

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
            If txtNOREG.Text = String.Empty Then
                MsgBox("Dibutuhkan Register", MsgBoxStyle.Exclamation, Me.Text)
                txtNOREG.Focus()
                fn_Validate = False
                Exit Function
            End If
            If txtDOCTOR.Text = String.Empty Then
                MsgBox("Dibutuhkan Dokter", MsgBoxStyle.Exclamation, Me.Text)
                txtDOCTOR.Focus()
                fn_Validate = False
                Exit Function
            End If
            'If grdDEPARTMENT.Text = String.Empty Then
            '    MsgBox("Dibutuhkan Poliklinik", MsgBoxStyle.Exclamation, Me.Text)
            '    grdDEPARTMENT.Focus()
            '    fn_Validate = False
            '    Exit Function
            'End If
        Catch oErr As Exception
            MsgBox("Validate Data : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Function
    Private Function fn_Save() As Boolean
        Try
            ' ***** HEADER *****
            Dim ds = oS_DIGITAL_RJ_36.GetStructureHeader
            With ds
                .KDPENDAFTARAN = txtNOREG.Text
                .KDCUSTOMER = txtNOMORRM.Text
                Try
                    .DATECREATED = oS_DIGITAL_RJ_36.GetData(txtNOREG.Text).DATECREATED
                Catch ex As Exception
                    .DATECREATED = Now
                End Try
                .DATEUPDATED = Now
                .DATE = deDATE.DateTime
                .DOKTER_KODE = sRUANGAN
                .DOKTER_NAMEDISPLAY = txtDOCTOR.Text
                .SDIGITALRJ36_1 = txt1.Text
                .SDIGITALRJ36_2 = txt2.Text
                .SDIGITALRJ36_3 = txt3.Text
                .SDIGITALRJ36_4 = txt4.Text
                .SDIGITALRJ36_5 = txt5.Text
                .SDIGITALRJ36_6 = txt6.Text
                .SDIGITALRJ36_7 = txt7.Text
                .SDIGITALRJ36_8 = txt8.Text
                .SDIGITALRJ36_9 = txt9.Text
                .SDIGITALRJ36_10 = TextEdit4.Text
                .SDIGITALRJ36_11 = TextEdit5.Text
                .SDIGITALRJ36_12 = TextEdit6.Text
                .SDIGITALRJ36_13 = TextEdit7.Text
                .SDIGITALRJ36_14 = CheckEdit5.Checked
                .SDIGITALRJ36_15 = TextEdit1.Text
                .SDIGITALRJ36_16 = CheckEdit1.Checked
                .SDIGITALRJ36_17 = TextEdit17.Text
                .SDIGITALRJ36_18 = CheckEdit2.Checked
                .SDIGITALRJ36_19 = TextEdit2.Text
                .SDIGITALRJ36_20 = CheckEdit3.Checked
                .SDIGITALRJ36_21 = CheckEdit4.Checked
                .SDIGITALRJ36_22 = CheckEdit7.Checked
                .SDIGITALRJ36_23 = CheckEdit6.Checked
                .SDIGITALRJ36_24 = TextEdit3.Text
                Try
                    Dim ms As New IO.MemoryStream()
                    picGAMBAR2.Image.Save(ms, picGAMBAR2.Image.RawFormat)

                    Dim data As Byte() = ms.GetBuffer()

                    .SDIGITALRJ36_25 = data
                Catch oErr As Exception
                    Try
                        .SDIGITALRJ36_25 = oS_DIGITAL_RJ_36.GetData(txtNOREG.Text).SDIGITALRJ36_25
                    Catch ex As Exception

                    End Try
                End Try
                .SDIGITALRJ36_26 = CheckEdit9.Checked
                .SDIGITALRJ36_27 = CheckEdit8.Checked
                .SDIGITALRJ36_28 = CheckEdit10.Checked
                .SDIGITALRJ36_29 = TextEdit8.Text
                .SDIGITALRJ36_30 = TextEdit9.Text
                .SDIGITALRJ36_31 = CheckEdit13.Checked
                .SDIGITALRJ36_32 = TextEdit11.Text
                .SDIGITALRJ36_33 = CheckEdit12.Checked
                .SDIGITALRJ36_34 = TextEdit10.Text
                .SDIGITALRJ36_35 = CheckEdit11.Checked
                .SDIGITALRJ36_36 = TextEdit12.Text
                .SDIGITALRJ36_37 = MemoEdit4.Text
                .SDIGITALRJ36_38 = MemoEdit2.Text
                .SDIGITALRJ36_39 = MemoEdit3.Text
                .SDIGITALRJ36_40 = MemoEdit5.Text
                .SDIGITALRJ36_41 = MemoEdit6.Text

                .DOKTER_KODE = txtDOCTOR.EditValue
                .DOKTER_NAMEDISPLAY = txtDOCTOR.Text
                Try
                    .CETAK = oS_DIGITAL_RJ_36.GetData(txtNOREG.Text).CETAK
                Catch ex As Exception
                    .CETAK = 0
                End Try

                .KDUSER = sUserID
                .KDUSER_SIGNATURE = ""
            End With

            If oFormMode = FORM_MODE.FORM_MODE_ADD Then
                Try
                    fn_Save = oS_DIGITAL_RJ_36.InsertData(ds)
                Catch ex As Exception
                    MsgBox("Simpan Data : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            ElseIf oFormMode = FORM_MODE.FORM_MODE_EDIT Then
                Try
                    fn_Save = oS_DIGITAL_RJ_36.UpdateData(ds)
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
            Case Keys.F5
                If btnReload.Enabled = True Then
                    btnReload_Click()
                End If
            Case Keys.F6
                If btnReload.Enabled = True Then
                    btnReload_Click()
                End If
            Case Keys.PageUp
	            fn_ScrollPage(True)
            Case Keys.PageDown
	            fn_ScrollPage(False)
        End Select
    End Sub
    Private Sub btnDiagnosa_Click() Handles btnDiagnosa.ItemClick
        'Dim oMasterDiagnosa As New Diagnosa.clsMasterDiagnosa
        'Dim dsMasterDiagnosa = oMasterDiagnosa.GetData(txtNOREG.Text)
        'If dsMasterDiagnosa IsNot Nothing Then
        '    Dim frmMasterDiagnosa As New frmMasterDiagnosa
        '    frmMasterDiagnosa.LoadMe(FORM_MODE.FORM_MODE_EDIT, txtNOREG.Text)
        '    frmMasterDiagnosa.ShowDialog(Me)
        'Else
        '    Dim frmMasterDiagnosa As New frmMasterDiagnosa
        '    frmMasterDiagnosa.LoadMe(FORM_MODE.FORM_MODE_ADD, txtNOREG.Text)
        '    frmMasterDiagnosa.ShowDialog(Me)
        'End If

        'If sCode = "Berhasil" Then
        '    Dim listDiagnosa As New List(Of String)
        '    For Each xloop In oMasterDiagnosa.GetDataDetail(txtNOREG.Text)
        '        listDiagnosa.Add(xloop.KATEGORI & " : " & xloop.REMARKS)
        '    Next

        '    Dim listProsedur As New List(Of String)
        '    For Each xloop In oMasterDiagnosa.GetDataDetail_(txtNOREG.Text)
        '        listProsedur.Add(xloop.SEQ + 1 & ". " & xloop.REMARKS)
        '    Next
        '    MemoEdit4.Text = String.Join(vbCrLf, listProsedur.ToArray)
        '    MemoEdit2.Text = String.Join(vbCrLf, listDiagnosa.ToArray)
        'End If
    End Sub
    Private Sub btnReload_Click() Handles btnReload.ItemClick
        'Dim dsKunjungan = oS_DIGITAL_RJ_36.GetDataByKunjungan(txtNOREG.Text)

        'Dim listPenunjang As New List(Of String)
        'Dim listTindakanPengobatan As New List(Of String)

        'If dsKunjungan IsNot Nothing Then
        '    Dim oOrderTindakan As New Inventory.clsOrderTindakan
        '    Dim oOrderLab As New Inventory.clsOrderLab
        '    Dim oOrderRad As New Inventory.clsOrderRad
        '    Dim oKonsul As New Digital.clsKonsul
        '    Dim oKonsulJawab As New Digital.clsJawabKonsul

        '    Dim dsTindakan = From x In oOrderTindakan.GetDataPendaftaranList(dsKunjungan.KDPENDAFTARAN)
        '                     Join y In oOrderTindakan.GetDataDetail()
        '                     On x.KDORDERTINDAKAN Equals y.KDORDERTINDAKAN
        '                     Select TANGGAL = x.DATE, ITEM = "Tindakan : " & y.TARIFKT & " " & y.REMARKS & ", "

        '    Dim dsLab = From x In oOrderLab.GetDataPendaftaranList(dsKunjungan.KDPENDAFTARAN)
        '                Join y In oOrderLab.GetDataDetail()
        '                On x.KDORDERLAB Equals y.KDORDERLAB
        '                Select TANGGAL = x.DATE, ITEM = "Lab : " & y.TARIFKT & " " & y.REMARKS & ", "

        '    Dim dsRad = From x In oOrderRad.GetDataPendaftaranList(dsKunjungan.KDPENDAFTARAN)
        '                Join y In oOrderRad.GetDataDetail()
        '                On x.KDORDERRAD Equals y.KDORDERRAD
        '                Select TANGGAL = x.DATE, ITEM = "Rad : " & y.TARIFKT & " " & y.REMARKS & ", "

        '    Dim dsKonsul = From x In oKonsul.GetDataPendaftaranList(dsKunjungan.KDPENDAFTARAN)
        '                   Select TANGGAL = x.DATE, ITEM = "Konsul : " & "Dari " & x.DOKTER_DARI & " Kepada " & x.DOKTER_KEPADA & " Isi " & x.MEMO & ", "

        '    Dim dsJawabKonsul = From x In oKonsulJawab.GetDataPendaftaranList(dsKunjungan.KDPENDAFTARAN)
        '                        Select TANGGAL = x.DATE, ITEM = "Jawab Konsul : " & "Dari " & x.DOKTER_DARI & " Kepada " & x.DOKTER_KEPADA & " Isi " & x.MEMO & ", "

        '    Dim dsUnionPenunjang = dsLab.Union(dsRad)

        '    For Each xloop In dsUnionPenunjang.OrderBy(Function(x) x.TANGGAL)
        '        listPenunjang.Add("*) " & xloop.TANGGAL.ToString("dd-MM-yyy HH:mm:ss") & " " & xloop.ITEM)
        '    Next

        '    Dim dsUnionTindakanKonsul = dsTindakan.Union(dsKonsul).Union(dsJawabKonsul)

        '    For Each xloop In dsUnionTindakanKonsul.OrderBy(Function(x) x.TANGGAL)
        '        listTindakanPengobatan.Add("*) " & xloop.TANGGAL.ToString("dd-MM-yyy HH:mm:ss") & " " & xloop.ITEM)
        '    Next

        '    Dim oResep As New Inventory.clsOrderResep

        '    Dim dsResep = oResep.GetDataPendaftaranList(dsKunjungan.KDPENDAFTARAN)

        '    For Each xloop In dsResep
        '        For Each yloop In oResep.GetDataDetail(xloop.KDORDERRESEP)
        '            listTindakanPengobatan.Add(yloop.TARIFKT & " " & yloop.SIGNA)
        '        Next
        '    Next

        '    'MemoEdit7.Text = String.Join(", ", listPenunjang.ToArray)
        '    MemoEdit3.Text = String.Join(", ", listTindakanPengobatan.ToArray)
        'End If
    End Sub
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
        'Dim oDOCTOR As New Master.clsDoctor
        'Try
        '    grdDOCTOR.Properties.DataSource = oDOCTOR.GetData.Where(Function(x) x.ISACTIVE = True And x.ADDRESS_COUNTRY <> "").ToList()
        '    grdDOCTOR.Properties.ValueMember = "KDDOCTOR"
        '    grdDOCTOR.Properties.DisplayMember = "NAME_DISPLAY"
        'Catch oErr As Exception
        '    MsgBox("Load Dokter DPJP Data : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        'End Try
    End Sub
    Private Sub fn_DEPARTMENT()
        'Dim oDEPARTMENT As New Master.clsDepartment
        'Try
        '    grdDEPARTMENT.Properties.DataSource = oDEPARTMENT.GetData.Where(Function(x) x.ISACTIVE = True).ToList()
        '    grdDEPARTMENT.Properties.ValueMember = "KDDEPARTMENT"
        '    grdDEPARTMENT.Properties.DisplayMember = "NAME_DISPLAY"
        'Catch oErr As Exception
        '    MsgBox("Load Department Data : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        'End Try
    End Sub

    Private Sub btnEDITIMAGE_Click(sender As Object, e As EventArgs) Handles btnEDITIMAGE.Click
        Dim frmPopUp_Image As New frmPopUp_img2
        frmPopUp_Image.fn_LoadMe(picGAMBAR2.Image)
        frmPopUp_Image.ShowDialog()

        If sPicture IsNot Nothing Then
            picGAMBAR2.Image = sPicture
        End If

        picGAMBAR2.Focus()
        sFind10 = String.Empty
        sPicture = Nothing
    End Sub

    Private Sub frmEMedrekRJ_36_MouseWheel(sender As Object, e As MouseEventArgs) Handles MyBase.MouseWheel
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