Imports System.Linq
Imports DataAccess
Imports System.Data.SqlClient

Public Class frmEMedrekRI_16
#Region "Declaration"
    Private oFormMode As FORM_MODE = FORM_MODE.FORM_MODE_VIEW
    Private isLoad As Boolean = False
    Private oDigital As New EMedrek.clsS_DIGITAL_RI_16
    Private sKDDOCTOR As String = String.Empty

#End Region
#Region "Function"
    Public Sub LoadMe(ByVal FormMode As Integer, ByVal KDREG As String, ByVal KDCUSTOMER As String, ByVal NAMAPASIEN As String, ByVal JENISKELAMIN As String, ByVal DPJP As String, ByVal KDDPJP As String)
        oFormMode = FormMode
        txtKDREG.Text = KDREG
        txtNOPASIEN.Text = KDCUSTOMER
        txtNAMAPASIEN.Text = NAMAPASIEN
        sKDDOCTOR = KDDPJP
    End Sub
    Private Sub Form_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        fn_ChangeFormState()
        isLoad = True
    End Sub
    Private Sub Form_Closing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        sCode = txtKDREG.Text
        sLoadAsesmenAwal = False
    End Sub
    Private Overloads Sub Dispose()
        MyBase.Dispose()
        Me.Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
    Private Sub fn_ChangeFormState()
        fn_Doctor2()

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

        txtDIAGNOSAMEDIS.Properties.ReadOnly = Status
        txtSTATUS.Properties.ReadOnly = Status
        txtLAINLAIN.Properties.ReadOnly = Status
        grdDOCTOR.Properties.ReadOnly = Status
        grdTIM1.Properties.ReadOnly = Status
        grdTIM2.Properties.ReadOnly = Status
        grdDPJPUTAMA.Properties.ReadOnly = Status
        deDPJPUTAMA.Properties.ReadOnly = Status
        grdDPJP1.Properties.ReadOnly = Status
        deDPJP1.Properties.ReadOnly = Status
        grdDPJP2.Properties.ReadOnly = Status
        deDPJP2.Properties.ReadOnly = Status
        grdDPJP3.Properties.ReadOnly = Status
        deDPJP3.Properties.ReadOnly = Status
        grdDPJP4.Properties.ReadOnly = Status
        deDPJP4.Properties.ReadOnly = Status
        grdDPJPPeralihan.Properties.ReadOnly = Status
        deDATEPERALIHAN.Properties.ReadOnly = Status
        txtALASANPERALIHAN.Properties.ReadOnly = Status
        txtPERALIHANDPJPUTAMA.Properties.ReadOnly = Status
        grdKDDOCTOR2.Properties.ReadOnly = Status
    End Sub
    Private Sub fn_EmptyMe()
        'txtDIAGNOSAMEDIS.ResetText()
        txtSTATUS.ResetText()
        txtLAINLAIN.ResetText()
        grdDOCTOR.Text = sKDDOCTOR
        grdTIM1.ResetText()
        grdTIM2.ResetText()
        grdDPJPUTAMA.ResetText()
        deDPJPUTAMA.ResetText()
        grdDPJP1.ResetText()
        deDPJP1.ResetText()
        grdDPJP2.ResetText()
        deDPJP2.ResetText()
        grdDPJP3.ResetText()
        deDPJP3.ResetText()
        grdDPJP4.ResetText()
        deDPJP4.ResetText()
        grdDPJPPeralihan.ResetText()
        deDATEPERALIHAN.ResetText()
        txtALASANPERALIHAN.ResetText()
        txtPERALIHANDPJPUTAMA.ResetText()
        grdKDDOCTOR2.ResetText()
        deDATE.DateTime = Now
    End Sub
    Private Sub fn_LoadData()
        Try
            Dim dsDetail_1 = oDigital.GetData(txtKDREG.Text)
            With dsDetail_1
                txtKDREG.Text = .KDPENDAFTARAN
                txtDIAGNOSAMEDIS.Text = .DIAGNOSA_MEDIS
                txtSTATUS.Text = .STATUS
                txtLAINLAIN.Text = .LAIN_LAIN
                grdDOCTOR.Text = .DOKTER_KODE
                grdTIM1.Text = .TIM_DPJP_1
                grdTIM2.Text = .TIM_DPJP_2
                grdDPJPUTAMA.Text = .DPJP_UTAMA_KODE
                grdDPJP1.Text = .DPJP_1_KODE
                grdDPJP2.Text = .DPJP_2_KODE
                grdDPJP3.Text = .DPJP_3_KODE
                grdDPJP4.Text = .DPJP_4_KODE
                deDPJPUTAMA.Text = .DATEDPJP_UTAMA
                deDPJP1.Text = .DATEDPJP_1
                deDPJP2.Text = .DATEDPJP_2
                deDPJP3.Text = .DATEDPJP_3
                deDPJP4.Text = .DATEDPJP_4
                grdDPJPPeralihan.Text = .DPJP_PERALIHAN_KODE
                deDATEPERALIHAN.Text = .DATEDPJP_PERALIHAN
                grdKDDOCTOR2.Text = .DOKTER2_KODE
                txtALASANPERALIHAN.Text = .ALASAN_PERALIHAN
                txtPERALIHANDPJPUTAMA.Text = .PERALIHAN_DPJP_UTAMA_KODE
                deDATE.DateTime = .DATEUPDATED
            End With

        Catch oErr As Exception
            MsgBox("Load List Data : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Function fn_Validate() As Boolean
        Try
            fn_Validate = True
            If txtKDREG.Text = String.Empty Then
                MsgBox("Dibutuhkan KDREG", MsgBoxStyle.Exclamation, Me.Text)
                txtKDREG.Focus()
                fn_Validate = False
                Exit Function
            End If
        Catch oErr As Exception
            MsgBox("Validate Data : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Function
    Private Function fn_Save() As Boolean
        Try

            Dim ds_2 = oDigital.GetStructureHeader

            With ds_2
                .KDPENDAFTARAN = txtKDREG.Text
                .KDCUSTOMER = txtNOPASIEN.Text
                Try
                    .DATECREATED = oDigital.GetData(txtKDREG.Text).DATECREATED
                Catch ex As Exception
                    .DATECREATED = Now
                End Try
                .DATEUPDATED = deDATE.DateTime
                .NOPASIEN = txtNOPASIEN.Text
                .STATUS = txtSTATUS.Text
                .DIAGNOSA_MEDIS = txtDIAGNOSAMEDIS.Text
                .LAIN_LAIN = txtLAINLAIN.Text
                .DOKTER_KODE = grdDOCTOR.EditValue
                .DOKTER_NAMEDISPLAY = grdDOCTOR.Text
                .TIM_DPJP_1 = grdTIM1.Text
                .TIM_DPJP_2 = grdTIM2.Text
                .DPJP_UTAMA_KODE = grdDPJPUTAMA.EditValue
                .DPJP_UTAMA_NAMEDISPLAY = grdDPJPUTAMA.Text
                .DPJP_1_KODE = grdDPJP1.EditValue
                .DPJP_1_NAMEDISPLAY = grdDPJP1.Text
                .DPJP_2_KODE = IIf(grdDPJP2.Text = "", "", grdDPJP2.EditValue)
                .DPJP_2_NAMEDISPLAY = IIf(grdDPJP2.Text = "", "", grdDPJP2.Text)
                .DPJP_3_KODE = IIf(grdDPJP3.Text = "", "", grdDPJP3.EditValue)
                .DPJP_3_NAMEDISPLAY = IIf(grdDPJP3.Text = "", "", grdDPJP3.Text)
                .DPJP_4_KODE = IIf(grdDPJP4.Text = "", "", grdDPJP4.EditValue)
                .DPJP_4_NAMEDISPLAY = IIf(grdDPJP4.Text = "", "", grdDPJP4.Text)
                .DATEDPJP_UTAMA = deDPJPUTAMA.Text
                .DATEDPJP_1 = deDPJP1.Text
                .DATEDPJP_2 = deDPJP2.Text
                .DATEDPJP_3 = deDPJP3.Text
                .DATEDPJP_4 = deDPJP4.Text
                .DPJP_PERALIHAN_KODE = grdDPJPPeralihan.EditValue
                .DPJP_PERALIHAN_NAMEDISPLAY = grdDPJPPeralihan.Text
                .DATEDPJP_PERALIHAN = deDATEPERALIHAN.Text
                .ALASAN_PERALIHAN = txtALASANPERALIHAN.Text
                .PERALIHAN_DPJP_UTAMA_KODE = txtPERALIHANDPJPUTAMA.EditValue
                .PERALIHAN_DPJP_UTAMA_NAMEDISPLAY = txtPERALIHANDPJPUTAMA.Text
                .DOKTER2_KODE = grdKDDOCTOR2.EditValue
                .DOKTER2_NAMEDISPLAY = grdKDDOCTOR2.Text
                .KDUSER = sUserID
                .KDUSER_SIGNATURE = ""
                .KDSIGANTURE_DPJP = ""
                .KDSIGANTURE_DPJP_SIGNATURE = ""
                .KDSIGANTURE_DPJPPERALIHAN = ""
                .KDSIGANTURE_DPJPPERALIHAN_SIGNATURE = ""
                .KDSIGANTURE_PPJP = ""
                .KDSIGANTURE_PPJP_SIGNATURE = ""
                Try
                    .CETAK = oDigital.GetData(txtKDREG.Text).CETAK
                Catch ex As Exception
                    .CETAK = 0
                End Try

            End With

            If oFormMode = FORM_MODE.FORM_MODE_ADD Then
                Try
                    fn_Save = oDigital.InsertData(ds_2)
                Catch ex As Exception
                    MsgBox("Simpan Data : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            ElseIf oFormMode = FORM_MODE.FORM_MODE_EDIT Then
                Try
                    fn_Save = oDigital.UpdateData(ds_2)
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
        If MsgBox("Save " & txtKDREG.Text.Trim.ToUpper & "?", MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, Me.Text) = MsgBoxResult.No Then Exit Sub
        If fn_Save() = False Then
            MsgBox("Save gagal! Tolong kontak admin anda!", MsgBoxStyle.Exclamation, Me.Text)
        Else
            MsgBox("Save " & txtKDREG.Text.Trim.ToUpper & " success!", MsgBoxStyle.Information, Me.Text)
            Me.Close()
        End If
    End Sub
    Private Sub btnClose_Click() Handles btnClose.ItemClick
        Me.Close()
    End Sub
#End Region
#Region "Lookup / Event"
    Private Sub fn_Doctor2()
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
            SQL &= "KDSTAFF = A.KDDOCTOR "
            SQL &= ",A.NAME_DISPLAY "
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
            grdDOCTOR.Properties.ValueMember = "KDSTAFF"
            grdDOCTOR.Properties.DisplayMember = "NAME_DISPLAY"

            grdTIM1.Properties.DataSource = ds.Tables("DOKTER")
            grdTIM1.Properties.ValueMember = "KDSTAFF"
            grdTIM1.Properties.DisplayMember = "NAME_DISPLAY"

            grdTIM2.Properties.DataSource = ds.Tables("DOKTER")
            grdTIM2.Properties.ValueMember = "KDSTAFF"
            grdTIM2.Properties.DisplayMember = "NAME_DISPLAY"

            grdDPJPUTAMA.Properties.DataSource = ds.Tables("DOKTER")
            grdDPJPUTAMA.Properties.ValueMember = "KDSTAFF"
            grdDPJPUTAMA.Properties.DisplayMember = "NAME_DISPLAY"

            grdDPJP1.Properties.DataSource = ds.Tables("DOKTER")
            grdDPJP1.Properties.ValueMember = "KDSTAFF"
            grdDPJP1.Properties.DisplayMember = "NAME_DISPLAY"

            grdDPJP2.Properties.DataSource = ds.Tables("DOKTER")
            grdDPJP2.Properties.ValueMember = "KDSTAFF"
            grdDPJP2.Properties.DisplayMember = "NAME_DISPLAY"

            grdDPJP3.Properties.DataSource = ds.Tables("DOKTER")
            grdDPJP3.Properties.ValueMember = "KDSTAFF"
            grdDPJP3.Properties.DisplayMember = "NAME_DISPLAY"

            grdDPJP4.Properties.DataSource = ds.Tables("DOKTER")
            grdDPJP4.Properties.ValueMember = "KDSTAFF"
            grdDPJP4.Properties.DisplayMember = "NAME_DISPLAY"

            grdDPJPPeralihan.Properties.DataSource = ds.Tables("DOKTER")
            grdDPJPPeralihan.Properties.ValueMember = "KDSTAFF"
            grdDPJPPeralihan.Properties.DisplayMember = "NAME_DISPLAY"

            grdKDDOCTOR2.Properties.DataSource = ds.Tables("DOKTER")
            grdKDDOCTOR2.Properties.ValueMember = "KDSTAFF"
            grdKDDOCTOR2.Properties.DisplayMember = "NAME_DISPLAY"


            grdDOCTOR.Text = "9"
            grdTIM1.Text = "9"
            grdTIM2.Text = "9"
            grdDPJPUTAMA.Text = "9"
            grdDPJP1.Text = "9"
            grdDPJP2.Text = "9"
            grdDPJP3.Text = "9"
            grdDPJP4.Text = "9"
            grdDPJPPeralihan.Text = "9"
            grdKDDOCTOR2.Text = "9"

            If oConn.State = ConnectionState.Open Then
                oConn.Close()
            End If
        Catch oErr As Exception
            MsgBox("Load Data : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub grdDOCTOR_EditValueChanged(sender As Object, e As EventArgs) Handles grdDOCTOR.EditValueChanged
        'If isLoad Then
        '    Dim sDpjp As String = grdDOCTOR.Text
        '    If sDpjp = "" Or sDpjp = "[EditValue is null]" Then Exit Sub
        '    Dim oSignature As New Master.clsSignature
        '    Dim dsSignature = oSignature.GetDataByDOCTOR(grdDOCTOR.EditValue)
        '    If dsSignature IsNot Nothing Then
        '        txtSIGANTUREDPJP.Text = dsSignature.KDSIGNATURE
        '    Else
        '        MsgBox("Kode Digital Tidak ditemukan, silahkan didaftarkan terlebih dahulu", MsgBoxStyle.Information, Me.Text)
        '        txtSIGANTUREDPJP.ResetText()
        '    End If
        'End If
    End Sub
    Private Sub grdDPJPPeralihan_EditValueChanged(sender As Object, e As EventArgs) Handles grdDPJPPeralihan.EditValueChanged
        'If isLoad Then
        '    Dim sDpjp As String = grdDPJPPeralihan.Text
        '    If sDpjp = "" Then Exit Sub
        '    Dim oSignature As New Master.clsSignature
        '    Dim dsSignature = oSignature.GetDataByDOCTOR(grdDPJPPeralihan.EditValue)
        '    If dsSignature IsNot Nothing Then
        '        txtSIGANTUREDPJPPersalinan.Text = dsSignature.KDSIGNATURE
        '    Else
        '        MsgBox("Kode Digital Tidak ditemukan, silahkan didaftarkan terlebih dahulu", MsgBoxStyle.Information, Me.Text)
        '        txtSIGANTUREDPJPPersalinan.ResetText()
        '    End If
        'End If
    End Sub
    Private Sub grdKDDOCTOR2_EditValueChanged(sender As Object, e As EventArgs) Handles grdKDDOCTOR2.EditValueChanged
        'If isLoad Then
        '    If grdKDDOCTOR2.Text = "" Then Exit Sub
        '    Dim oSignature As New Master.clsSignature
        '    Dim dsSignature = oSignature.GetDataByDOCTOR2(grdKDDOCTOR2.EditValue)
        '    If dsSignature IsNot Nothing Then
        '        txtSIGANTUREPPJP.Text = dsSignature.KDSIGNATURE
        '    Else
        '        MsgBox("Kode Digital Tidak ditemukan, silahkan didaftarkan terlebih dahulu", MsgBoxStyle.Information, Me.Text)
        '        txtSIGANTUREPPJP.ResetText()
        '    End If
        'End If
    End Sub
    'Private Sub btnTambah_Click(sender As Object, e As EventArgs)
    '    txtDIAGNOSAMEDIS.Text = IIf(txtDIAGNOSAMEDIS.Text = "", txtDIAGNOSAMEDIS.Text, txtDIAGNOSAMEDIS.Text & ", ") & grdKDDIAGNOSA.Text
    'End Sub
#End Region
End Class