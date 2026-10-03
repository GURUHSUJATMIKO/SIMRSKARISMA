Imports DataAccess
Imports UI.WIN.MAIN.My.Resources
Imports System.Linq
Imports Newtonsoft.Json.Linq

Public Class frmKelasAplicare
#Region "Declaration"
    Private oFormMode As FORM_MODE = FORM_MODE.FORM_MODE_VIEW
    Private sNoId As String
    Private isLoad As Boolean = False
    Private oKelasAplicare As New Reference.clsKelasAplicare
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
            Me.Text = KelasAplicare.TITLE

            lKDKelasAplicare.Text = KelasAplicare.KDKELASAPLICARE & " *"
            lMEMO.Text = KelasAplicare.MEMO & " *"
            chkISACTIVE.Text = KelasAplicare.ISACTIVE
            chkISDEFAULT.Text = KelasAplicare.ISDEFAULT

            btnSaveNew.Caption = Caption.FormSaveNew
            btnSaveClose.Caption = Caption.FormSaveClose
            btnClose.Caption = Caption.FormClose
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub Form_Closing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        sCode = txtMEMO.Text.Trim.ToUpper
    End Sub
    Private Sub fn_ChangeFormState()
        fn_LoadKelasAplicare()
        fn_LoadKDRoom()

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

        txtKDKELASAPLICARE.Properties.ReadOnly = True
        txtMEMO.Properties.ReadOnly = True
        chkISACTIVE.Properties.ReadOnly = Status
        chkISDEFAULT.Properties.ReadOnly = Status
    End Sub
    Private Sub fn_EmptyMe()
        txtKDKELASAPLICARE.ResetText()
        txtMEMO.ResetText()

        chkISACTIVE.Checked = True
        chkISDEFAULT.Checked = False

        txtMEMO.ResetText()
    End Sub
    Private Sub fn_LoadData()
        Try
            Dim ds = oKelasAplicare.GetData(sNoId)

            With ds
                txtKDKELASAPLICARE.Text = sNoId
                txtMEMO.Text = .MEMO
                chkISACTIVE.Checked = .ISACTIVE
                chkISDEFAULT.Checked = .ISDEFAULT

                BindingSource.DataSource = oKelasAplicare.GetDataDetail_UOM.Where(Function(x) x.KDKELAS = sNoId).OrderBy(Function(x) x.M_DEPARTMENT.NAME_DISPLAY).ToList()
                grdDetail_UOM.DataSource = BindingSource

            End With
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Function fn_Validate() As Boolean
        Try
            fn_Validate = True
            If txtKDKELASAPLICARE.Text = String.Empty Then
                txtKDKELASAPLICARE.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                txtKDKELASAPLICARE.ErrorText = Statement.ErrorRequired

                txtKDKELASAPLICARE.Focus()
                fn_Validate = False
                Exit Function
            End If
            If txtMEMO.Text = String.Empty Then
                txtMEMO.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                txtMEMO.ErrorText = Statement.ErrorRequired

                txtMEMO.Focus()
                fn_Validate = False
                Exit Function
            End If
            If oFormMode = FORM_MODE.FORM_MODE_ADD Then
                If oKelasAplicare.IsExist(txtMEMO.Text.ToUpper.Trim) = True Then
                    txtMEMO.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                    txtMEMO.ErrorText = Statement.ErrorRegistered

                    txtMEMO.Focus()
                    fn_Validate = False
                    Exit Function
                End If
            Else
                If txtMEMO.Text.Trim.ToUpper <> oKelasAplicare.GetData(sNoId).MEMO Then
                    If oKelasAplicare.IsExist(txtMEMO.Text.ToUpper.Trim) = True Then
                        txtMEMO.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                        txtMEMO.ErrorText = Statement.ErrorRegistered

                        txtMEMO.Focus()
                        fn_Validate = False
                        Exit Function
                    End If
                End If
            End If

            If grvDetail_UOM.RowCount < 2 Then
                MsgBox("Dibutuhkan Detil", MsgBoxStyle.Exclamation, Me.Text)
                fn_Validate = False
                Exit Function
            End If

        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Function
    Private Function fn_Save() As Boolean
        'Try
        '    ' ***** HEADER *****
        '    Dim ds = oKelasAplicare.GetStructureHeader
        '    With ds
        '        Try
        '            .DATECREATED = oKelasAplicare.GetData(sNoId).DATECREATED
        '        Catch oErr As Exception
        '            .DATECREATED = Now
        '        End Try
        '        .DATEUPDATED = Now

        '        .KDKELAS = txtKDKELASAPLICARE.Text.ToString.Trim.ToUpper
        '        .MEMO = txtMEMO.Text.Trim.ToUpper
        '        .ISACTIVE = chkISACTIVE.Checked
        '        .ISDEFAULT = chkISDEFAULT.Checked

        '    End With

        '    ' ***** Satuan *****
        '    Dim arrDetail_UOM = oKelasAplicare.GetStructureDetail_UOMList
        '    For i As Integer = 0 To grvDetail_UOM.RowCount - 2
        '        Dim dsDetail_UOM = oKelasAplicare.GetStructureDetail_UOM
        '        With dsDetail_UOM
        '            Try
        '                .DATECREATED = oKelasAplicare.GetData(sNoId).DATECREATED
        '            Catch oErr As Exception
        '                .DATECREATED = Now
        '            End Try
        '            .DATEUPDATED = Now
        '            .KDUPDATE_APLICARE = txtKDKELASAPLICARE.Text.ToString.Trim.ToUpper & grvDetail_UOM.GetRowCellValue(i, colKDUOM)
        '            .KDKELASAPLICARE = txtKDKELASAPLICARE.Text.ToString.Trim.ToUpper
        '            .KDDEPARTMENT = grvDetail_UOM.GetRowCellValue(i, colKDUOM)
        '            .KAPASITAS = grvDetail_UOM.GetRowCellValue(i, colKAPASITAS)
        '            .TERSEDIA = grvDetail_UOM.GetRowCellValue(i, colTERSEDIA)
        '            .TERSEDIA_LAKI = grvDetail_UOM.GetRowCellValue(i, colTERSEDIA_LAKI)
        '            .TERSEDIA_PEREMPUAN = grvDetail_UOM.GetRowCellValue(i, colTERSEDIA_PEREMPUAN)
        '            .TERSEDIA_LAKIPEREMPUAN = grvDetail_UOM.GetRowCellValue(i, colTERSEDIA_LAKIPEREMPUAN)

        '            Try
        '                .ISAPLICARE = oKelasAplicare.GetDataDetail_UOM(txtKDKELASAPLICARE.Text, grvDetail_UOM.GetRowCellValue(i, colKDUOM)).ISAPLICARE
        '            Catch ex As Exception
        '                .ISAPLICARE = False
        '            End Try

        '        End With
        '        arrDetail_UOM.Add(dsDetail_UOM)
        '    Next

        '    If oFormMode = FORM_MODE.FORM_MODE_ADD Then
        '        Try
        '            fn_Save = oKelasAplicare.InsertData(ds, arrDetail_UOM)
        '        Catch oErr As Exception
        '            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        '        End Try
        '    ElseIf oFormMode = FORM_MODE.FORM_MODE_EDIT Then
        '        Try
        '            fn_Save = oKelasAplicare.UpdateData(ds, arrDetail_UOM)
        '            fn_UpdateKetersediaan()
        '        Catch oErr As Exception
        '            MsgBox("Update Ketersedian Tempat Tidur gagal", MsgBoxStyle.Exclamation, Me.Text)
        '        End Try
        '    End If
        'Catch oErr As Exception
        '    MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        '    fn_Save = False
        'End Try
    End Function
    Private Sub fn_RuanganBaru()
        Try
            Dim jsonRequest As String = String.Empty

            jsonRequest = "{ "
            jsonRequest &= """kodekelas"": """ & txtKDKELASAPLICARE.Text & ""","
            jsonRequest &= """koderuang"": """ & grvDetail_UOM.GetFocusedRowCellValue(colKDUOM) & ""","
            jsonRequest &= """namaruang"": """ & oKelasAplicare.GetDataByRoom(grvDetail_UOM.GetFocusedRowCellValue(colKDUOM)).NAME_DISPLAY & ""","
            jsonRequest &= """kapasitas"": """ & grvDetail_UOM.GetFocusedRowCellValue(colKAPASITAS) & ""","
            jsonRequest &= """tersedia"": """ & grvDetail_UOM.GetFocusedRowCellValue(colTERSEDIA) & ""","
            jsonRequest &= """tersediapria"": """ & grvDetail_UOM.GetFocusedRowCellValue(colTERSEDIA_LAKI) & ""","
            jsonRequest &= """tersediawanita"": """ & grvDetail_UOM.GetFocusedRowCellValue(colTERSEDIA_PEREMPUAN) & ""","
            jsonRequest &= """tersediapriawanita"": """ & grvDetail_UOM.GetFocusedRowCellValue(colTERSEDIA_LAKIPEREMPUAN) & """"
            jsonRequest &= "} "

            Dim oSetKoneksi As New Brigging.clsSetKoneksi
            Dim dsSetKoneksi = oSetKoneksi.RuanganBaru("APLICARE", jsonRequest)

            If dsSetKoneksi <> "" Then
                Dim allData = JObject.Parse(dsSetKoneksi)
                Dim CodeResponse As String = String.Empty
                Dim messageResponse As String = String.Empty

                CodeResponse = allData("metadata")("code").ToString
                messageResponse = allData("metadata")("message").ToString

                If CodeResponse = 1 Then
                    grvDetail_UOM.SetFocusedRowCellValue(colIAPLICARE, True)
                    MsgBox(CodeResponse & " - " & messageResponse, MsgBoxStyle.Information, Me.Text)
                Else
                    MsgBox(CodeResponse & " - " & messageResponse, MsgBoxStyle.Exclamation, Me.Text)
                End If
            Else
                MsgBox("Kosong set Koneksi", Me.Text)
            End If

        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, "Update Aplicare Gagagl !!!")
        End Try
    End Sub
    Private Sub fn_HapusBaru()
        Try
            Dim jsonRequest As String = String.Empty

            jsonRequest = "{ "
            jsonRequest &= """kodekelas"": """ & txtKDKELASAPLICARE.Text & ""","
            jsonRequest &= """koderuang"": """ & grvDetail_UOM.GetFocusedRowCellValue(colKDUOM) & """"
            jsonRequest &= "} "

            Dim oSetKoneksi As New Brigging.clsSetKoneksi
            Dim dsSetKoneksi = oSetKoneksi.RuanganHapus("APLICARE", jsonRequest)

            If dsSetKoneksi <> "" Then
                Dim allData = JObject.Parse(dsSetKoneksi)
                Dim CodeResponse As String = String.Empty
                Dim messageResponse As String = String.Empty

                CodeResponse = allData("metadata")("code").ToString
                messageResponse = allData("metadata")("message").ToString

                If CodeResponse = 1 Then
                    grvDetail_UOM.SetFocusedRowCellValue(colIAPLICARE, False)
                    MsgBox(CodeResponse & " - " & messageResponse, MsgBoxStyle.Information, Me.Text)
                Else
                    MsgBox(CodeResponse & " - " & messageResponse, MsgBoxStyle.Exclamation, Me.Text)
                End If
            Else
                MsgBox("Kosong set Koneksi", Me.Text)
            End If

        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, "Update Aplicare Gagagl !!!")
        End Try
    End Sub
    Private Sub fn_UpdateKetersediaan()
        'For i As Integer = 0 To grvDetail_UOM.RowCount - 2
        '    If grvDetail_UOM.GetRowCellValue(i, colIAPLICARE) = True Then
        '        Try
        '            Dim jsonRequest As String = String.Empty

        '            jsonRequest = "{ "
        '            jsonRequest &= """kodekelas"": """ & txtKDKELASAPLICARE.Text & ""","
        '            jsonRequest &= """koderuang"": """ & grvDetail_UOM.GetRowCellValue(i, colKDUOM) & ""","
        '            'jsonRequest &= """namaruang"": """ & oKelasAplicare.GetDataByRoom(grvDetail_UOM.GetRowCellValue(i, colKDUOM)).NAME_DISPLAY & ""","
        '            jsonRequest &= """kapasitas"": """ & grvDetail_UOM.GetRowCellValue(i, colKAPASITAS) & ""","
        '            jsonRequest &= """tersedia"": """ & grvDetail_UOM.GetRowCellValue(i, colTERSEDIA) & ""","
        '            jsonRequest &= """tersediapria"": """ & grvDetail_UOM.GetRowCellValue(i, colTERSEDIA_LAKI) & ""","
        '            jsonRequest &= """tersediawanita"": """ & grvDetail_UOM.GetRowCellValue(i, colTERSEDIA_PEREMPUAN) & ""","
        '            jsonRequest &= """tersediapriawanita"": """ & grvDetail_UOM.GetRowCellValue(i, colTERSEDIA_LAKIPEREMPUAN) & """"
        '            jsonRequest &= "} "

        '            Dim oSetKoneksi As New Brigging.clsSetKoneksi
        '            Dim dsSetKoneksi = oSetKoneksi.UpdateKetersediaanTempatTidur("APLICARE", jsonRequest)

        '            If dsSetKoneksi <> "" Then
        '                Dim allData = JObject.Parse(dsSetKoneksi)
        '                Dim CodeResponse As String = String.Empty
        '                Dim messageResponse As String = String.Empty

        '                CodeResponse = allData("metadata")("code").ToString
        '                messageResponse = allData("metadata")("message").ToString

        '                If CodeResponse = 1 Then
        '                    MsgBox(CodeResponse & " - " & messageResponse, MsgBoxStyle.Information, Me.Text)

        '                    Dim oUpdateAplicare As New Transaction.clsUpdateAplicare

        '                    ' ***** HEADER *****
        '                    Dim ds = oUpdateAplicare.GetStructureHeader
        '                    With ds
        '                        .DATECREATED = Now
        '                        .DATEUPDATED = Now
        '                        .KDUPDATE_APLICARE = 0
        '                        .REQUEST = jsonRequest
        '                        .RESPON = dsSetKoneksi
        '                        .KDUSER = sUserID
        '                    End With
        '                Else
        '                    MsgBox(CodeResponse & " - " & messageResponse, MsgBoxStyle.Exclamation, Me.Text)
        '                End If
        '            Else
        '                MsgBox("Kosong set Koneksi", Me.Text)
        '            End If

        '        Catch oErr As Exception
        '            MsgBox("Looping Aplicare" & Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, "Update Aplicare Gagagl !!!")
        '        End Try
        '    End If
        'Next
    End Sub
    Private Sub DeleteToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DeleteToolStripMenuItem.Click
        If oFormMode = FORM_MODE.FORM_MODE_VIEW Then Exit Sub
        If grvDetail_UOM.GetFocusedRowCellValue(colIAPLICARE) = False Then
            grvDetail_UOM.DeleteSelectedRows()
        End If
    End Sub
    Private Sub RuanganBaruToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles RuanganBaruToolStripMenuItem.Click
        If grvDetail_UOM.GetFocusedRowCellValue(colKDUOM) IsNot Nothing Then
            If oKelasAplicare.GetDataDetail_UOM(txtKDKELASAPLICARE.Text, grvDetail_UOM.GetFocusedRowCellValue(colKDUOM)) IsNot Nothing Then
                Dim sResult = MsgBox("Apakah Yakin Ruangan akan di Simpan Aplicare?", MsgBoxStyle.YesNo, Me.Text)

                If sResult = Windows.Forms.DialogResult.Yes Then
                    fn_RuanganBaru()
                End If
            Else
                MsgBox("Data Rumah Sakit tidak ditemukan, Silahkan Simpan Ketersedian terlebih dahulu", MsgBoxStyle.Exclamation, Me.Text)
            End If
        Else
            MsgBox("Silahkan Pilih Ruangan terlebih dahulu", MsgBoxStyle.Exclamation, Me.Text)
        End If
    End Sub
    Private Sub HapusRuanganToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles HapusRuanganToolStripMenuItem.Click
        If grvDetail_UOM.GetFocusedRowCellValue(colKDUOM) IsNot Nothing Then
            If oKelasAplicare.GetDataDetail_UOM(txtKDKELASAPLICARE.Text, grvDetail_UOM.GetFocusedRowCellValue(colKDUOM)) IsNot Nothing Then
                Dim sResult = MsgBox("Apakah Yakin Ruangan akan di Hapus Aplicare?", MsgBoxStyle.YesNo, Me.Text)

                If sResult = Windows.Forms.DialogResult.Yes Then
                    fn_HapusBaru()
                End If
            Else
                MsgBox("Data Rumah Sakit tidak ditemukan, Silahkan Simpan Ketersedian terlebih dahulu", MsgBoxStyle.Exclamation, Me.Text)
            End If
        Else
            MsgBox("Silahkan Pilih Ruangan terlebih dahulu", MsgBoxStyle.Exclamation, Me.Text)
        End If
    End Sub
#End Region
#Region "Command Button"
    Private Sub frmItem_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
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
    Private Sub grdCARI_KeyPress(sender As Object, e As KeyPressEventArgs) Handles grdCARI.KeyPress
        If Asc(e.KeyChar) = 13 Then
            txtKDKELASAPLICARE.Text = grdCARI.EditValue
            txtMEMO.Text = grdCARI.Text
        End If
    End Sub
#End Region
#Region "Grid Method"
    Private Sub grdDetail_UOM_CellValueChanged(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs) Handles grvDetail_UOM.CellValueChanged

    End Sub
#End Region
#Region "Lookup / Event"
    Private Sub fn_LoadKDRoom()
        'Dim oRoom As New Reference.clsRoom
        'Try
        '    grdUOM.DataSource = oRoom.GetData.Where(Function(x) x.ISACTIVE = True).ToList()
        '    grdUOM.ValueMember = "KDDEPARTMENT"
        '    grdUOM.DisplayMember = "NAME_DISPLAY"
        'Catch oErr As Exception
        '    MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        'End Try
    End Sub
    Private Sub fn_LoadKelasAplicare()
        Try
            Dim oSetKoneksi As New Brigging.clsSetKoneksi

            Dim dsSetKoneksi = oSetKoneksi.GetDataAplicareReferensiKelasRawat("APLICARE")

            If dsSetKoneksi <> "" Then
                Try
                    Dim allData = JObject.Parse(dsSetKoneksi)

                    Dim table As DataTable

                    table = New DataTable("M_KELAS")
                    table.Columns.Add("kode")
                    table.Columns.Add("nama")

                    For Each item In allData("response")("list")
                        table.Rows.Add(New String() {item("kodekelas"), item("namakelas")})
                    Next

                    grdCARI.Properties.DataSource = table

                    grdCARI.Properties.ValueMember = "kode"
                    grdCARI.Properties.DisplayMember = "nama"

                    grdCARI.ShowPopup()

                Catch oErr As Exception
                    MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message & vbCrLf & vbCrLf & "Result : " & vbCrLf & dsSetKoneksi, MsgBoxStyle.Exclamation, Me.Text)
                End Try

            End If

        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub

#End Region
End Class