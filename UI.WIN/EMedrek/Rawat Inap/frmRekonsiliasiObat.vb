Imports System.Data.SqlClient
Imports DataAccess
Imports UI.WIN.MAIN.My.Resources
Imports System.Linq

Public Class frmRekonsiliasiObat
#Region "Declaration"
    Private oFormMode As FORM_MODE = FORM_MODE.FORM_MODE_VIEW
    Private isLoad As Boolean = False
    Private oS_DIGITAL_FARMASI_REKONSILIASIOBAT_H As New Transaksi.clsRekonsiliasiObat
    Private sNoId As String
    Private sNoRegister As String = String.Empty
    Private sKDCUSTOMER As String = String.Empty
    Private sNAMAPASIEN As String = String.Empty
    Private sJENISKELAMIN As String = String.Empty
    Private sPENJAMIN As String = String.Empty
    Private sTANGGALLAHIR As DateTime = Now
    Private sUMUR As String = String.Empty
    Private sKodeCopy As String = String.Empty
    Private sTUJUAN As String = String.Empty
    Private sKDDOCTOR As String = String.Empty
#End Region
#Region "Function"
    Public Sub LoadMe(ByVal FormMode As Integer, ByVal CopyKode As String, ByVal KDREG As String, ByVal KDCUSTOMER As String, ByVal NAMAPASIEN As String, ByVal TANGGALLAHIR As DateTime, ByVal UMUR As String, ByVal JK As String, ByVal PENJAMIN As String, ByVal TUJUAN As String, ByVal KDDOCTOR As String, ByVal NoId As String)
        oFormMode = FormMode
        sNoId = NoId
        sNoRegister = KDREG
        sKDCUSTOMER = KDCUSTOMER
        sNAMAPASIEN = NAMAPASIEN
        sJENISKELAMIN = JK
        sPENJAMIN = PENJAMIN
        sTANGGALLAHIR = TANGGALLAHIR
        sUMUR = UMUR
        sKodeCopy = CopyKode
        sTUJUAN = TUJUAN
        sKDDOCTOR = KDDOCTOR

        txtNamaPasien.Text = NAMAPASIEN
        txtNoPasien.Text = KDCUSTOMER
    End Sub
    Private Sub Form_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        fn_ChangeFormState()
        isLoad = True
    End Sub
    Private Sub Form_Closing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        Dispose()
        sCode = txtNoRegister.Text.Trim.ToUpper
    End Sub
    Private Overloads Sub Dispose()
        MyBase.Dispose()
        Me.Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
    Private Sub fn_ChangeFormState()
        fn_LoadDoctorDPJP()
        'fn_LoadKDSIGNA()

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

        grdDokter.Properties.ReadOnly = Status
        txtPerawat.Properties.ReadOnly = Status
        txtApoteker.Properties.ReadOnly = Status
        deDATE.Properties.ReadOnly = Status
    End Sub
    Private Sub fn_EmptyMe()
        grdDokter.ResetText()
        txtPerawat.ResetText()
        txtApoteker.ResetText()
        deDATE.DateTime = Now
        txtKET.ResetText()
        chkAlergi_YA.Checked = True
        chkAlergi_TIDAK.Checked = False
        chkObatBawa_YA.Checked = True
        chkObatBawa_TIDAK.Checked = False

        txtTujuan.Text = sTUJUAN
        grdDokter.Text = sKDDOCTOR
        txtNoRegister.Text = sNoRegister
    End Sub
    Private Sub fn_LoadData()
        Try
            ' ***** HEADER *****
            Dim ds = oS_DIGITAL_FARMASI_REKONSILIASIOBAT_H.GetData(sNoid)

            With ds
                txtNoRegister.Text = .KDPENDAFTARAN
                deDATE.DateTime = CDate(.DATE)
                txtKET.Text = .KETERANGAN
                chkAlergi_YA.Checked = .ALERGI_YA
                chkAlergi_TIDAK.Checked = .ALERGI_TIDAK
                chkObatBawa_YA.Checked = .OBATBAWA_YA
                chkObatBawa_TIDAK.Checked = .OBATBAWA_TIDAK
                txtTujuan.Text = .KDUSER_SIGNATURE

                BindingSource1.DataSource = oS_DIGITAL_FARMASI_REKONSILIASIOBAT_H.GetDataDetail1(sNoId)
                grdDetail.DataSource = BindingSource1

                BindingSource2.DataSource = oS_DIGITAL_FARMASI_REKONSILIASIOBAT_H.GetDataDetail2(sNoId)
                grdDetail2.DataSource = BindingSource2

                grdDokter.EditValue = .KODEDOKTER
                txtPerawat.Text = .NAMAPERAWAT
                txtApoteker.EditValue = .NAMAAPOTEKER

                tabControl.SelectedTabPage = tab2
                tabControl.SelectedTabPage = tab1

            End With
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
            If txtNoRegister.Text = String.Empty Then
                MsgBox("Dibutuhkan Register", MsgBoxStyle.Exclamation, Me.Text)
                txtNoRegister.Focus()
                fn_Validate = False
                Exit Function
            End If
            If txtTujuan.Text = String.Empty Then
                MsgBox("Dibutuhkan Tujuan", MsgBoxStyle.Exclamation, Me.Text)
                txtTujuan.Focus()
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
            Dim ds = oS_DIGITAL_FARMASI_REKONSILIASIOBAT_H.GetStructureHeader
            With ds

                .KDREKONSILIASI = sNoId
                .KDPENDAFTARAN = txtNoRegister.Text
                .KDCUSTOMER = sKDCUSTOMER

                Try
                    .DATECREATED = oS_DIGITAL_FARMASI_REKONSILIASIOBAT_H.GetData(sNoId).DATECREATED
                Catch ex As Exception
                    .DATECREATED = Now
                End Try
                .DATEUPDATED = Now

                .DATE = deDATE.DateTime

                .ALERGI_YA = chkAlergi_YA.Checked
                .ALERGI_TIDAK = chkAlergi_TIDAK.Checked
                .OBATBAWA_YA = chkObatBawa_YA.Checked
                .OBATBAWA_TIDAK = chkObatBawa_TIDAK.Checked

                .KETERANGAN = txtKET.Text

                Try
                    .CETAK = oS_DIGITAL_FARMASI_REKONSILIASIOBAT_H.GetData(sNoId).CETAK
                Catch ex As Exception
                    .CETAK = 0
                End Try

                .KODEDOKTER = grdDokter.EditValue
                .NAMADOKTER = grdDokter.Text

                .KODEAPOTEKER = ""
                .NAMAAPOTEKER = txtApoteker.Text

                .KODEPERAWAT = ""
                .NAMAPERAWAT = txtPerawat.Text

                .KDUSER = sUserID
                .KDUSER_SIGNATURE = txtTujuan.Text
            End With

            ' ***** DETIL *****
            Dim arrDetail = oS_DIGITAL_FARMASI_REKONSILIASIOBAT_H.GetStructureDetail1List
            For i As Integer = 0 To grvDetail.RowCount - 2
                Dim dsDetail = oS_DIGITAL_FARMASI_REKONSILIASIOBAT_H.GetStructureDetail1
                With dsDetail
                    .SEQ = i
                    .KDREKONSILIASI = ds.KDREKONSILIASI
                    .ITEMOBAT = grvDetail.GetRowCellValue(i, colITEMOBAT)
                    .TINGKATALERGI = IIf(String.IsNullOrEmpty(grvDetail.GetRowCellValue(i, colTingkatAlergi)), "", grvDetail.GetRowCellValue(i, colTingkatAlergi))
                    .REAKSIALERGI =  IIf(String.IsNullOrEmpty(grvDetail.GetRowCellValue(i, colREAKSIALERGI)), "", grvDetail.GetRowCellValue(i, colREAKSIALERGI))
                End With
                arrDetail.Add(dsDetail)
            Next

            ' ***** DETIL 2 *****
            Dim arrDetail2 = oS_DIGITAL_FARMASI_REKONSILIASIOBAT_H.GetStructureDetail2List
            For i As Integer = 0 To grvDetail2.RowCount - 2
                Dim dsDetail2 = oS_DIGITAL_FARMASI_REKONSILIASIOBAT_H.GetStructureDetail2
                With dsDetail2
                    .SEQ = i
                    .KDREKONSILIASI = ds.KDREKONSILIASI
                    .ITEMOBAT = grvDetail2.GetRowCellValue(i, colITEMOBAT)
                    .DOSIS = grvDetail2.GetRowCellValue(i, colDOSIS)
                    .FREKUENSI = grvDetail2.GetRowCellValue(i, colFREKUENSI)
                    .TANGGAL_MULAI = CDate(grvDetail2.GetRowCellValue(i, colTANGGAL_MULAI))
                    .TANGGAL_STOP = CDate(grvDetail2.GetRowCellValue(i, colTANGGAL_STOP))
                    .ISOBATDILANJUTKAN_1 = grvDetail2.GetRowCellValue(i, colISOBATDILANJUTKAN_1)
                    .ISOBATDILANJUTKAN_2 = grvDetail2.GetRowCellValue(i, colISOBATDILANJUTKAN_2)
                    .TANGGAL_STOP_STR = grvDetail2.GetRowCellValue(i, colTANGGAL_STOP_STR)
                End With
                arrDetail2.Add(dsDetail2)
            Next

            If oFormMode = FORM_MODE.FORM_MODE_ADD Then
                Try
                    fn_Save = oS_DIGITAL_FARMASI_REKONSILIASIOBAT_H.InsertData(ds,arrDetail,arrDetail2)
                Catch ex As Exception
                    MsgBox("Simpan Data : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            ElseIf oFormMode = FORM_MODE.FORM_MODE_EDIT Then
                Try
                    fn_Save = oS_DIGITAL_FARMASI_REKONSILIASIOBAT_H.UpdateData(ds, arrDetail, arrDetail2)
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
        End Select
    End Sub

    Private Sub btnSaveClose_Click() Handles btnSaveClose.ItemClick
        If fn_Validate() = False Then Exit Sub
        If MsgBox("Save " & txtNoRegister.Text.Trim.ToUpper & "?", MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, Me.Text) = MsgBoxResult.No Then Exit Sub
        If fn_Save() = False Then
            MsgBox("Save gagal! Tolong kontak admin anda!", MsgBoxStyle.Exclamation, Me.Text)
        Else
            MsgBox("Save " & txtNoRegister.Text.Trim.ToUpper & " success!", MsgBoxStyle.Information, Me.Text)
            Me.Close()
        End If
    End Sub

    Private Sub btnClose_Click() Handles btnClose.ItemClick
        Me.Close()
    End Sub

#End Region
#Region "Lookup / Event"
    Private Sub DeleteToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DeleteToolStripMenuItem.Click
        If oFormMode = FORM_MODE.FORM_MODE_VIEW Then Exit Sub
        grvDetail.DeleteSelectedRows()
    End Sub

    Private Sub DeleteToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles DeleteToolStripMenuItem1.Click, grdDetail2.EmbeddedNavigator.ContextMenuStripChanged
        If oFormMode = FORM_MODE.FORM_MODE_VIEW Then Exit Sub
        grvDetail2.DeleteSelectedRows()
    End Sub

    Private Sub chkAlergi_YA_CheckedChanged(sender As Object, e As EventArgs) Handles chkAlergi_YA.CheckedChanged
        If chkAlergi_YA.Checked Then
            chkAlergi_TIDAK.Checked = False
        Else
            chkAlergi_TIDAK.Checked = True
        End If
        fn_TabObat()
    End Sub

    Private Sub chkAlergi_TIDAK_CheckedChanged(sender As Object, e As EventArgs) Handles chkAlergi_TIDAK.CheckedChanged
        If chkAlergi_TIDAK.Checked Then
            chkAlergi_YA.Checked = False
        Else
            chkAlergi_YA.Checked = True
        End If
        fn_TabObat()
    End Sub

    Private Sub chkObatBawa_YA_CheckedChanged(sender As Object, e As EventArgs) Handles chkObatBawa_YA.CheckedChanged
        If chkObatBawa_YA.Checked Then
            chkObatBawa_TIDAK.Checked = False
        Else
            chkObatBawa_TIDAK.Checked = True
        End If
        fn_TabObat()
    End Sub

    Private Sub chkObatBawa_TIDAK_CheckedChanged(sender As Object, e As EventArgs) Handles chkObatBawa_TIDAK.CheckedChanged
        If chkObatBawa_TIDAK.Checked Then
            chkObatBawa_YA.Checked = False
        Else
            chkObatBawa_YA.Checked = True
        End If
        fn_TabObat()
    End Sub

    Private sub fn_TabObat()
        If chkAlergi_YA.Checked And chkAlergi_TIDAK.Checked = False Then
            tab1.PageVisible = True
        Else
            tab1.PageVisible = False
        End If

        If chkObatBawa_YA.Checked And chkObatBawa_TIDAK.Checked = False Then
            tab2.PageVisible = True
        Else
            tab2.PageVisible = False
        End If
    End Sub

    Private Sub grvDetail2_CellValueChanged(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs) Handles grvDetail2.CellValueChanged
        If e.Column.Name = colITEMOBAT2.Name Then
            If grvDetail2.GetFocusedRowCellValue(colITEMOBAT2) IsNot Nothing Then
                grvDetail2.SetFocusedRowCellValue(colTANGGAL_MULAI, Now)
                grvDetail2.SetFocusedRowCellValue(colTANGGAL_STOP, Now)
                grvDetail2.SetFocusedRowCellValue(colTANGGAL_STOP_STR, "-")
            End If
        End If

        If e.Column.Name = colISOBATDILANJUTKAN_1.Name Then
            If CBool(grvDetail2.GetFocusedRowCellValue(colISOBATDILANJUTKAN_1)) = True Then
                grvDetail2.SetFocusedRowCellValue(colTANGGAL_STOP, Now)
                grvDetail2.SetFocusedRowCellValue(colTANGGAL_STOP_STR, "-")
                grvDetail2.SetFocusedRowCellValue(colISOBATDILANJUTKAN_2, False)
            End If
        End If

        If e.Column.Name = colISOBATDILANJUTKAN_2.Name Then
            If CBool(grvDetail2.GetFocusedRowCellValue(colISOBATDILANJUTKAN_2)) = True Then
                grvDetail2.SetFocusedRowCellValue(colTANGGAL_STOP, Now)
                grvDetail2.SetFocusedRowCellValue(colTANGGAL_STOP_STR, Now.ToString())
                grvDetail2.SetFocusedRowCellValue(colISOBATDILANJUTKAN_1, False)
            End If
        End If
    End Sub
    Private Sub fn_LoadDoctorDPJP()
        Try
            Dim oDoctor As New Reference.clsDoctor

            Dim dsDoctorList = From x In oDoctor.GetData()
                               Where x.ISACTIVE = True
                               Select x.KDDOCTOR, x.NAME_DISPLAY

            grdDokter.Properties.DataSource = dsDoctorList.ToList()
            grdDokter.Properties.ValueMember = "KDDOCTOR"
            grdDokter.Properties.DisplayMember = "NAME_DISPLAY"
        Catch oErr As Exception
            MsgBox("Load Dokter Data : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    'Private Sub fn_LoadKDSIGNA()
    '    Try
    '        Dim oConn As New SqlConnection
    '        Dim oComm As New SqlCommand
    '        Dim da As SqlDataAdapter
    '        Dim ds As New DataSet
    '        Dim SQL As String
    '        Dim sConn As String = sConnOld
    '        oConn = New SqlConnection(sConn)

    '        If oConn.State = ConnectionState.Closed Then
    '            oConn.Open()
    '        End If

    '        SQL = "SELECT "
    '        SQL &= "* "
    '        SQL &= "FROM "
    '        SQL &= "M_SIGNA A "
    '        SQL &= "WHERE "
    '        SQL &= "A.ISACTIVE = 1 "

    '        oComm.Connection = oConn
    '        oComm.CommandText = SQL
    '        oComm.CommandTimeout = 120
    '        oComm.CommandType = CommandType.Text

    '        da = New SqlDataAdapter(oComm)
    '        da.Fill(ds, "SIGNA")

    '        grdSIGNA.DataSource = ds.Tables("SIGNA")
    '        grdSIGNA.ValueMember = "MEMO"
    '        grdSIGNA.DisplayMember = "MEMO"

    '        If oConn.State = ConnectionState.Open Then
    '            oConn.Close()
    '        End If
    '    Catch oErr As Exception
    '        MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
    '    End Try
    'End Sub
#End Region
End Class