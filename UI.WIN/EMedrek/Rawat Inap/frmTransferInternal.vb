Imports DataAccess
Imports System.Linq
Imports System.Data.SqlClient

Public Class frmTransferInternal
#Region "Declaration"
    Private oFormMode As FORM_MODE = FORM_MODE.FORM_MODE_VIEW
    Private sNoId As String
    Private isLoad As Boolean = False
    Private oTransferInternal As New Transaksi.clsTransferInternal
    Private sKDDOCTOR As String = String.Empty
    Private sCopyKode As String = String.Empty
    Private sPerawat As String = String.Empty

#End Region
#Region "Function"
    Public Sub LoadMe(ByVal FormMode As Integer, ByVal CopyKode As String, ByVal KDREG As String, ByVal KDDOCTOR As String, ByVal KDCUSTOMER As String, ByVal NAMAPASIEN As String, ByVal NoId As String, ByVal kdperawatuser As String)
        oFormMode = FormMode
        sNoId = NoId

        txtNAMA.Text = NAMAPASIEN
        txtNoRegister.Text = KDREG
        txtKDCUSTOMER.Text = KDCUSTOMER

        sKDDOCTOR = KDDOCTOR
        sCopyKode = CopyKode
        sPerawat = kdperawatuser
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
        fn_LoadRoom()
        fn_LoadUser()

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

        txtKDTRANSFERINTERNAL.Properties.ReadOnly = True
        txtNoRegister.Properties.ReadOnly = True
        txtKDCUSTOMER.Properties.ReadOnly = True
        txtNAMA.Properties.ReadOnly = True
        deDATE.Properties.ReadOnly = Status
        grdDPJP.Properties.ReadOnly = Status
        TextEdit1.Properties.ReadOnly = Status
        grdAsalRuangan.Properties.ReadOnly = Status
        grdRuanganTujuan.Properties.ReadOnly = Status
        TextEdit4.Properties.ReadOnly = Status
        TextEdit5.Properties.ReadOnly = Status
        TextEdit6.Properties.ReadOnly = Status
        TextEdit7.Properties.ReadOnly = Status
        TextEdit8.Properties.ReadOnly = Status
        cboE.Properties.ReadOnly = Status
        cboM.Properties.ReadOnly = Status
        cboV.Properties.ReadOnly = Status
        TextEdit12.Properties.ReadOnly = Status
        TextEdit13.Properties.ReadOnly = Status
        TextEdit14.Properties.ReadOnly = Status
        TextEdit15.Properties.ReadOnly = Status
        TextEdit16.Properties.ReadOnly = Status
        TextEdit17.Properties.ReadOnly = Status
        TextEdit18.Properties.ReadOnly = Status
        TextEdit19.Properties.ReadOnly = Status
        TextEdit20.Properties.ReadOnly = Status
        TextEdit21.Properties.ReadOnly = Status
        TextEdit22.Properties.ReadOnly = Status
        TextEdit23.Properties.ReadOnly = Status
        TextEdit24.Properties.ReadOnly = Status
        TextEdit25.Properties.ReadOnly = Status
        TextEdit26.Properties.ReadOnly = Status
        TextEdit27.Properties.ReadOnly = Status
        TextEdit28.Properties.ReadOnly = Status
        TextEdit29.Properties.ReadOnly = Status
        TextEdit30.Properties.ReadOnly = Status
        TextEdit31.Properties.ReadOnly = Status
        TextEdit32.Properties.ReadOnly = Status
        TextEdit33.Properties.ReadOnly = Status
        TextEdit34.Properties.ReadOnly = Status
        TextEdit35.Properties.ReadOnly = Status
        TextEdit36.Properties.ReadOnly = Status
        TextEdit37.Properties.ReadOnly = Status
        TextEdit38.Properties.ReadOnly = Status
        TextEdit39.Properties.ReadOnly = Status
        TextEdit40.Properties.ReadOnly = Status
        TextEdit41.Properties.ReadOnly = Status
        TextEdit42.Properties.ReadOnly = Status
        TextEdit43.Properties.ReadOnly = Status
        grdUSER1.Properties.ReadOnly = Status
        grdUSER2.Properties.ReadOnly = Status
        CheckEdit1.Properties.ReadOnly = Status
        CheckEdit2.Properties.ReadOnly = Status
        CheckEdit3.Properties.ReadOnly = Status
        CheckEdit4.Properties.ReadOnly = Status
        CheckEdit5.Properties.ReadOnly = Status
        CheckEdit6.Properties.ReadOnly = Status
        CheckEdit7.Properties.ReadOnly = Status
        CheckEdit8.Properties.ReadOnly = Status
        CheckEdit9.Properties.ReadOnly = Status
        CheckEdit10.Properties.ReadOnly = Status
        CheckEdit11.Properties.ReadOnly = Status
        CheckEdit12.Properties.ReadOnly = Status
        CheckEdit13.Properties.ReadOnly = Status
        CheckEdit14.Properties.ReadOnly = Status
        CheckEdit15.Properties.ReadOnly = Status
        CheckEdit16.Properties.ReadOnly = Status
        CheckEdit17.Properties.ReadOnly = Status
        CheckEdit18.Properties.ReadOnly = Status
        CheckEdit19.Properties.ReadOnly = Status
        CheckEdit20.Properties.ReadOnly = Status
        CheckEdit21.Properties.ReadOnly = Status
        CheckEdit22.Properties.ReadOnly = Status
        CheckEdit23.Properties.ReadOnly = Status
        CheckEdit24.Properties.ReadOnly = Status
        CheckEdit25.Properties.ReadOnly = Status
        CheckEdit26.Properties.ReadOnly = Status
        CheckEdit27.Properties.ReadOnly = Status
        CheckEdit28.Properties.ReadOnly = Status
        CheckEdit29.Properties.ReadOnly = Status
        CheckEdit30.Properties.ReadOnly = Status
        CheckEdit31.Properties.ReadOnly = Status
        CheckEdit32.Properties.ReadOnly = Status
        CheckEdit33.Properties.ReadOnly = Status
        CheckEdit34.Properties.ReadOnly = Status
        ComboBoxEdit1.Properties.ReadOnly = Status
        ComboBoxEdit2.Properties.ReadOnly = Status
    End Sub
    Private Sub fn_EmptyMe()
        txtKDTRANSFERINTERNAL.Text = "<--AUTO-->"
        deDATE.DateTime = Now
        grdDPJP.Text = sKDDOCTOR
        TextEdit1.ResetText()
        grdAsalRuangan.ResetText()
        grdRuanganTujuan.ResetText()
        TextEdit4.ResetText()
        TextEdit5.ResetText()
        TextEdit6.ResetText()
        TextEdit7.ResetText()
        TextEdit8.ResetText()
        cboE.ResetText()
        cboM.ResetText()
        cboV.ResetText()
        TextEdit12.ResetText()
        TextEdit13.ResetText()
        TextEdit14.ResetText()
        TextEdit15.ResetText()
        TextEdit16.ResetText()
        TextEdit17.ResetText()
        TextEdit18.ResetText()
        TextEdit19.ResetText()
        TextEdit20.ResetText()
        TextEdit21.ResetText()
        TextEdit22.ResetText()
        TextEdit23.ResetText()
        TextEdit24.ResetText()
        TextEdit25.ResetText()
        TextEdit26.ResetText()
        TextEdit27.ResetText()
        TextEdit28.ResetText()
        TextEdit29.ResetText()
        TextEdit30.ResetText()
        TextEdit31.ResetText()
        TextEdit32.ResetText()
        TextEdit33.ResetText()
        TextEdit34.ResetText()
        TextEdit35.ResetText()
        TextEdit36.ResetText()
        TextEdit37.ResetText()
        TextEdit38.ResetText()
        TextEdit39.ResetText()
        TextEdit40.ResetText()
        TextEdit41.ResetText()
        TextEdit42.ResetText()
        TextEdit43.ResetText()
        grdUSER1.ResetText()
        grdUSER2.ResetText()
        CheckEdit1.Checked = False
        CheckEdit2.Checked = False
        CheckEdit3.Checked = False
        CheckEdit4.Checked = False
        CheckEdit5.Checked = False
        CheckEdit6.Checked = False
        CheckEdit7.Checked = False
        CheckEdit8.Checked = False
        CheckEdit9.Checked = False
        CheckEdit10.Checked = False
        CheckEdit11.Checked = False
        CheckEdit12.Checked = False
        CheckEdit13.Checked = False
        CheckEdit14.Checked = False
        CheckEdit15.Checked = False
        CheckEdit16.Checked = False
        CheckEdit17.Checked = False
        CheckEdit18.Checked = False
        CheckEdit19.Checked = False
        CheckEdit20.Checked = False
        CheckEdit21.Checked = False
        CheckEdit22.Checked = False
        CheckEdit23.Checked = False
        CheckEdit24.Checked = False
        CheckEdit25.Checked = False
        CheckEdit26.Checked = False
        CheckEdit27.Checked = False
        CheckEdit28.Checked = False
        CheckEdit29.Checked = False
        CheckEdit30.Checked = False
        CheckEdit31.Checked = False
        CheckEdit32.Checked = False
        CheckEdit33.Checked = False
        CheckEdit34.Checked = False

        grdUSER1.Text = sPerawat
        grdUSER2.Text = sPerawat

        If sCopyKode <> "" Then
            fn_LoadDataCopy(sCopyKode)
        End If
    End Sub
    Private Sub fn_LoadDataCopy(ByVal Paramater As String)
        Try
            ' ***** HEADER *****
            Dim ds = oTransferInternal.GetData(Paramater)

            With ds
                'txtKDTRANSFERINTERNAL.Text = sNoId
                txtNoRegister.Text = .KDREG
                deDATE.DateTime = .DATE
                grdDPJP.Text = .DESCRIPTION
                txtKDCUSTOMER.Text = .KDCUSTOMER
                TextEdit1.Text = .TEXT1
                grdAsalRuangan.Text = .TEXT48
                grdRuanganTujuan.Text = .TEXT49
                TextEdit4.Text = .TEXT4
                TextEdit5.Text = .TEXT5
                TextEdit6.Text = .TEXT6
                TextEdit7.Text = .TEXT7
                TextEdit8.Text = .TEXT8
                cboE.Text = .TEXT9
                cboM.Text = .TEXT10
                cboV.Text = .TEXT11
                TextEdit12.Text = .TEXT12
                TextEdit13.Text = .TEXT13
                TextEdit14.Text = .TEXT14
                TextEdit15.Text = .TEXT15
                TextEdit16.Text = .TEXT16
                TextEdit17.Text = .TEXT17
                TextEdit18.Text = .TEXT18
                TextEdit19.Text = .TEXT19
                TextEdit20.Text = .TEXT20
                TextEdit21.Text = .TEXT21
                TextEdit22.Text = .TEXT22
                TextEdit23.Text = .TEXT23
                TextEdit24.Text = .TEXT24
                TextEdit25.Text = .TEXT25
                TextEdit26.Text = .TEXT26
                TextEdit27.Text = .TEXT27
                TextEdit28.Text = .TEXT28
                TextEdit29.Text = .TEXT29
                TextEdit30.Text = .TEXT30
                TextEdit31.Text = .TEXT31
                TextEdit32.Text = .TEXT32
                TextEdit33.Text = .TEXT33
                TextEdit34.Text = .TEXT34
                TextEdit35.Text = .TEXT35
                TextEdit36.Text = .TEXT36
                TextEdit37.Text = .TEXT37
                TextEdit38.Text = .TEXT38
                TextEdit39.Text = .TEXT39
                TextEdit40.Text = .TEXT40
                TextEdit41.Text = .TEXT41
                TextEdit42.Text = .TEXT42
                TextEdit43.Text = .TEXT43
                grdUSER1.Text = .TEXT44
                grdUSER2.Text = .TEXT45
                CheckEdit1.Checked = .CEKLIS_1
                CheckEdit2.Checked = .CEKLIS_2
                CheckEdit3.Checked = .CEKLIS_3
                CheckEdit4.Checked = .CEKLIS_4
                CheckEdit5.Checked = .CEKLIS_5
                CheckEdit6.Checked = .CEKLIS_6
                CheckEdit7.Checked = .CEKLIS_7
                CheckEdit8.Checked = .CEKLIS_8
                CheckEdit9.Checked = .CEKLIS_9
                CheckEdit10.Checked = .CEKLIS_10
                CheckEdit11.Checked = .CEKLIS_11
                CheckEdit12.Checked = .CEKLIS_12
                CheckEdit13.Checked = .CEKLIS_13
                CheckEdit14.Checked = .CEKLIS_14
                CheckEdit15.Checked = .CEKLIS_15
                CheckEdit16.Checked = .CEKLIS_16
                CheckEdit17.Checked = .CEKLIS_17
                CheckEdit18.Checked = .CEKLIS_18
                CheckEdit19.Checked = .CEKLIS_19
                CheckEdit20.Checked = .CEKLIS_20
                CheckEdit21.Checked = .CEKLIS_21
                CheckEdit22.Checked = .CEKLIS_22
                CheckEdit23.Checked = .CEKLIS_23
                CheckEdit24.Checked = .CEKLIS_24
                CheckEdit25.Checked = .CEKLIS_25
                CheckEdit26.Checked = .CEKLIS_26
                CheckEdit27.Checked = .CEKLIS_27
                CheckEdit28.Checked = .CEKLIS_28
                CheckEdit29.Checked = .CEKLIS_29
                CheckEdit30.Checked = .CEKLIS_30
                CheckEdit31.Checked = .CEKLIS_31
                CheckEdit32.Checked = .CEKLIS_32
                CheckEdit33.Checked = .CEKLIS_33
                CheckEdit34.Checked = .CEKLIS_33
                ComboBoxEdit1.Text = .TEXT46
                ComboBoxEdit2.Text = .TEXT47
            End With
        Catch oErr As Exception
            MsgBox("Load List Data : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadData()
        Try
            ' ***** HEADER *****
            Dim ds = oTransferInternal.GetData(sNoId)

            With ds
                txtKDTRANSFERINTERNAL.Text = sNoId
                txtNoRegister.Text = .KDREG
                deDATE.DateTime = .DATE
                grdDPJP.Text = .DESCRIPTION
                txtKDCUSTOMER.Text = .KDCUSTOMER
                TextEdit1.Text = .TEXT1
                grdAsalRuangan.Text = .TEXT48
                grdRuanganTujuan.Text = .TEXT49
                TextEdit4.Text = .TEXT4
                TextEdit5.Text = .TEXT5
                TextEdit6.Text = .TEXT6
                TextEdit7.Text = .TEXT7
                TextEdit8.Text = .TEXT8
                cboE.Text = .TEXT9
                cboM.Text = .TEXT10
                cboV.Text = .TEXT11
                TextEdit12.Text = .TEXT12
                TextEdit13.Text = .TEXT13
                TextEdit14.Text = .TEXT14
                TextEdit15.Text = .TEXT15
                TextEdit16.Text = .TEXT16
                TextEdit17.Text = .TEXT17
                TextEdit18.Text = .TEXT18
                TextEdit19.Text = .TEXT19
                TextEdit20.Text = .TEXT20
                TextEdit21.Text = .TEXT21
                TextEdit22.Text = .TEXT22
                TextEdit23.Text = .TEXT23
                TextEdit24.Text = .TEXT24
                TextEdit25.Text = .TEXT25
                TextEdit26.Text = .TEXT26
                TextEdit27.Text = .TEXT27
                TextEdit28.Text = .TEXT28
                TextEdit29.Text = .TEXT29
                TextEdit30.Text = .TEXT30
                TextEdit31.Text = .TEXT31
                TextEdit32.Text = .TEXT32
                TextEdit33.Text = .TEXT33
                TextEdit34.Text = .TEXT34
                TextEdit35.Text = .TEXT35
                TextEdit36.Text = .TEXT36
                TextEdit37.Text = .TEXT37
                TextEdit38.Text = .TEXT38
                TextEdit39.Text = .TEXT39
                TextEdit40.Text = .TEXT40
                TextEdit41.Text = .TEXT41
                TextEdit42.Text = .TEXT42
                TextEdit43.Text = .TEXT43
                grdUSER1.Text = .TEXT44
                grdUSER2.Text = .TEXT45
                CheckEdit1.Checked = .CEKLIS_1
                CheckEdit2.Checked = .CEKLIS_2
                CheckEdit3.Checked = .CEKLIS_3
                CheckEdit4.Checked = .CEKLIS_4
                CheckEdit5.Checked = .CEKLIS_5
                CheckEdit6.Checked = .CEKLIS_6
                CheckEdit7.Checked = .CEKLIS_7
                CheckEdit8.Checked = .CEKLIS_8
                CheckEdit9.Checked = .CEKLIS_9
                CheckEdit10.Checked = .CEKLIS_10
                CheckEdit11.Checked = .CEKLIS_11
                CheckEdit12.Checked = .CEKLIS_12
                CheckEdit13.Checked = .CEKLIS_13
                CheckEdit14.Checked = .CEKLIS_14
                CheckEdit15.Checked = .CEKLIS_15
                CheckEdit16.Checked = .CEKLIS_16
                CheckEdit17.Checked = .CEKLIS_17
                CheckEdit18.Checked = .CEKLIS_18
                CheckEdit19.Checked = .CEKLIS_19
                CheckEdit20.Checked = .CEKLIS_20
                CheckEdit21.Checked = .CEKLIS_21
                CheckEdit22.Checked = .CEKLIS_22
                CheckEdit23.Checked = .CEKLIS_23
                CheckEdit24.Checked = .CEKLIS_24
                CheckEdit25.Checked = .CEKLIS_25
                CheckEdit26.Checked = .CEKLIS_26
                CheckEdit27.Checked = .CEKLIS_27
                CheckEdit28.Checked = .CEKLIS_28
                CheckEdit29.Checked = .CEKLIS_29
                CheckEdit30.Checked = .CEKLIS_30
                CheckEdit31.Checked = .CEKLIS_31
                CheckEdit32.Checked = .CEKLIS_32
                CheckEdit33.Checked = .CEKLIS_33
                CheckEdit34.Checked = .CEKLIS_33
                ComboBoxEdit1.Text = .TEXT46
                ComboBoxEdit2.Text = .TEXT47

                Dim sE As Decimal = 0
                Dim sM As Decimal = 0
                Dim sV As Decimal = 0

                If cboE.Text <> String.Empty Then
                    Try
                        sE = CDec(cboE.Text)
                    Catch ex As Exception

                    End Try
                End If

                If cboM.Text <> String.Empty Then
                    Try
                        sM = CDec(cboM.Text)
                    Catch ex As Exception

                    End Try
                End If

                If cboV.Text <> String.Empty Then
                    Try
                        sV = CDec(cboV.Text)
                    Catch ex As Exception

                    End Try
                End If

                lblGCS.Text = "GCS : " & sE + sM + sV
            End With
        Catch oErr As Exception
            MsgBox("Load List Data : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Function fn_Validate() As Boolean
        Try
            fn_Validate = True
            If txtNoRegister.Text = String.Empty Then
                MsgBox("Dibutuhkan Register", MsgBoxStyle.Exclamation, Me.Text)
                txtNoRegister.Focus()
                fn_Validate = False
                Exit Function
            End If
            If txtKDCUSTOMER.Text = String.Empty Then
                MsgBox("Dibutuhkan No RM", MsgBoxStyle.Exclamation, Me.Text)
                txtKDCUSTOMER.Focus()
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
            Dim ds = oTransferInternal.GetStructureHeader
            With ds
                .KDTRANSFERINTERNAL = sNoId
                .KDREG = txtNoRegister.Text
                Try
                    .DATECREATED = oTransferInternal.GetData(txtNoRegister.Text).DATECREATED
                Catch ex As Exception
                    .DATECREATED = Now
                End Try
                .DATEUPDATED = Now
                .DATE = deDATE.DateTime
                .KDCUSTOMER = txtKDCUSTOMER.Text
                .DESCRIPTION = grdDPJP.EditValue
                .KDUSER = sUserID
                .CEKLIS_1 = CheckEdit1.Checked
                .CEKLIS_2 = CheckEdit2.Checked
                .CEKLIS_3 = CheckEdit3.Checked
                .CEKLIS_4 = CheckEdit4.Checked
                .CEKLIS_5 = CheckEdit5.Checked
                .CEKLIS_6 = CheckEdit6.Checked
                .CEKLIS_7 = CheckEdit7.Checked
                .CEKLIS_8 = CheckEdit8.Checked
                .CEKLIS_9 = CheckEdit9.Checked
                .CEKLIS_10 = CheckEdit10.Checked
                .CEKLIS_11 = CheckEdit11.Checked
                .CEKLIS_12 = CheckEdit12.Checked
                .CEKLIS_13 = CheckEdit13.Checked
                .CEKLIS_14 = CheckEdit14.Checked
                .CEKLIS_15 = CheckEdit15.Checked
                .CEKLIS_16 = CheckEdit16.Checked
                .CEKLIS_17 = CheckEdit17.Checked
                .CEKLIS_18 = CheckEdit18.Checked
                .CEKLIS_19 = CheckEdit19.Checked
                .CEKLIS_20 = CheckEdit20.Checked
                .CEKLIS_21 = CheckEdit21.Checked
                .CEKLIS_22 = CheckEdit22.Checked
                .CEKLIS_23 = CheckEdit23.Checked
                .CEKLIS_24 = CheckEdit24.Checked
                .CEKLIS_25 = CheckEdit25.Checked
                .CEKLIS_26 = CheckEdit26.Checked
                .CEKLIS_27 = CheckEdit27.Checked
                .CEKLIS_28 = CheckEdit28.Checked
                .CEKLIS_29 = CheckEdit29.Checked
                .CEKLIS_30 = CheckEdit30.Checked
                .CEKLIS_31 = CheckEdit31.Checked
                .CEKLIS_32 = CheckEdit32.Checked
                .CEKLIS_33 = CheckEdit33.Checked
                .CEKLIS_34 = CheckEdit34.Checked
                .TEXT1 = TextEdit1.Text
                .TEXT2 = grdAsalRuangan.Text
                .TEXT3 = grdRuanganTujuan.Text
                .TEXT4 = TextEdit4.Text
                .TEXT5 = TextEdit5.Text
                .TEXT6 = TextEdit6.Text
                .TEXT7 = TextEdit7.Text
                .TEXT8 = TextEdit8.Text
                .TEXT9 = cboE.Text
                .TEXT10 = cboM.Text
                .TEXT11 = cboV.Text
                .TEXT12 = TextEdit12.Text
                .TEXT13 = TextEdit13.Text
                .TEXT14 = TextEdit14.Text
                .TEXT15 = TextEdit15.Text
                .TEXT16 = TextEdit16.Text
                .TEXT17 = TextEdit17.Text
                .TEXT18 = TextEdit18.Text
                .TEXT19 = TextEdit19.Text
                .TEXT20 = TextEdit20.Text
                .TEXT21 = TextEdit21.Text
                .TEXT22 = TextEdit22.Text
                .TEXT23 = TextEdit23.Text
                .TEXT24 = TextEdit24.Text
                .TEXT25 = TextEdit25.Text
                .TEXT26 = TextEdit26.Text
                .TEXT27 = TextEdit27.Text
                .TEXT28 = TextEdit28.Text
                .TEXT29 = TextEdit29.Text
                .TEXT30 = TextEdit30.Text
                .TEXT31 = TextEdit31.Text
                .TEXT32 = TextEdit32.Text
                .TEXT33 = TextEdit33.Text
                .TEXT34 = TextEdit34.Text
                .TEXT35 = TextEdit35.Text
                .TEXT36 = TextEdit36.Text
                .TEXT37 = TextEdit37.Text
                .TEXT38 = TextEdit38.Text
                .TEXT39 = TextEdit39.Text
                .TEXT40 = TextEdit40.Text
                .TEXT41 = TextEdit41.Text
                .TEXT42 = TextEdit42.Text
                .TEXT43 = TextEdit43.Text
                .TEXT44 = grdUSER1.Text
                .TEXT45 = grdUSER2.Text
                .TEXT46 = ComboBoxEdit1.Text
                .TEXT47 = ComboBoxEdit2.Text
                .TEXT48 = grdAsalRuangan.EditValue
                .TEXT49 = grdRuanganTujuan.EditValue

                Dim sE As Decimal = 0
                Dim sM As Decimal = 0
                Dim sV As Decimal = 0

                Try
                    sE = CDec(cboE.Text)
                Catch ex As Exception

                End Try
                Try
                    sM = CDec(cboM.Text)
                Catch ex As Exception

                End Try
                Try
                    sV = CDec(cboV.Text)
                Catch ex As Exception

                End Try

                .TEXT50 = sE + sM + sV
                .TEXT51 = ""
                .TEXT52 = ""
                .TEXT53 = ""
                .TEXT54 = ""
                .TEXT55 = ""
                .TEXT56 = ""
                .TEXT57 = ""
                .TEXT58 = ""
                .TEXT59 = txtNAMA.Text
                .TEXT60 = grdDPJP.Text
            End With

            If oFormMode = FORM_MODE.FORM_MODE_ADD Then
                Try
                    fn_Save = oTransferInternal.InsertData(ds)
                Catch ex As Exception
                    MsgBox("Simpan Data : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            ElseIf oFormMode = FORM_MODE.FORM_MODE_EDIT Then
                Try
                    fn_Save = oTransferInternal.UpdateData(ds)
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
    Private Sub btnSaveNew_Click() Handles btnSaveNew.ItemClick
        If fn_Validate() = False Then Exit Sub
        If MsgBox("Save " & txtNoRegister.Text.Trim.ToUpper & "?", MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, Me.Text) = MsgBoxResult.No Then Exit Sub
        If fn_Save() = False Then
            MsgBox("Save gagal! Tolong kontak admin anda!", MsgBoxStyle.Exclamation, Me.Text)
        Else
            MsgBox("Save " & txtNoRegister.Text.Trim.ToUpper & " success!", MsgBoxStyle.Information, Me.Text)
            'sStatusSave = "NEW"
            Me.Close()
        End If
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
    Private Sub cboE_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboE.SelectedIndexChanged, cboM.SelectedIndexChanged, cboV.SelectedIndexChanged
        If isLoad = True Then
            Dim sE As Decimal = 0
            Dim sM As Decimal = 0
            Dim sV As Decimal = 0

            If cboE.Text = "" Or cboM.Text = "" Or cboV.Text = "" Then
                lblGCS.Text = "GCS : "
            Else
                If cboE.Text <> String.Empty Then
                    Try
                        sE = CDec(cboE.Text)
                    Catch ex As Exception

                    End Try
                End If

                If cboM.Text <> String.Empty Then
                    Try
                        sM = CDec(cboM.Text)
                    Catch ex As Exception

                    End Try
                End If

                If cboV.Text <> String.Empty Then
                    Try
                        sV = CDec(cboV.Text)
                    Catch ex As Exception

                    End Try
                End If

                lblGCS.Text = "GCS : " & sE + sM + sV
            End If
        End If
    End Sub
#End Region
#Region "Lookup / Event"
    Private Sub fn_LoadDoctorDPJP()
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
    Private Sub fn_LoadRoom()
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
            SQL &= "KDMROOM "
            SQL &= ",NAME_DISPLAY "
            SQL &= "FROM  "
            SQL &= "M_MEDICAL_ROOM "
            SQL &= "WHERE "
            SQL &= "ISACTIVE = '1' "

            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "M_MEDICAL_ROOM")

            grdAsalRuangan.Properties.DataSource = ds.Tables("M_MEDICAL_ROOM")
            grdAsalRuangan.Properties.ValueMember = "KDMROOM"
            grdAsalRuangan.Properties.DisplayMember = "NAME_DISPLAY"

            grdRuanganTujuan.Properties.DataSource = ds.Tables("M_MEDICAL_ROOM")
            grdRuanganTujuan.Properties.ValueMember = "KDMROOM"
            grdRuanganTujuan.Properties.DisplayMember = "NAME_DISPLAY"

            If oConn.State = ConnectionState.Open Then
                oConn.Close()
            End If
        Catch oErr As Exception
            MsgBox("Load Ruangan Data : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadUser()
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
            SQL &= "MEMO = (SELECT CASE A.FRONT_TITLE WHEN '' THEN '' ELSE A.FRONT_TITLE + ' ' END) + A.NAME_DISPLAY + A.BACK_TITLE "
            SQL &= "FROM  "
            SQL &= "M_DOCTOR A "
            SQL &= "WHERE "
            SQL &= "ISACTIVE = '1' "
            SQL &= "AND CATEGORY = 1 "

            SQL &= "UNION "

            SQL &= "SELECT "
            SQL &= "A.MEMO COLLATE DATABASE_DEFAULT "
            SQL &= "FROM  "
            SQL &= "RSKC_NEW..M_UNIT A "
            SQL &= "WHERE "
            SQL &= "ISACTIVE = '1' "

            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "M_USER")

            grdUSER1.Properties.DataSource = ds.Tables("M_USER")
            grdUSER1.Properties.ValueMember = "MEMO"
            grdUSER1.Properties.DisplayMember = "MEMO"

            grdUSER2.Properties.DataSource = ds.Tables("M_USER")
            grdUSER2.Properties.ValueMember = "MEMO"
            grdUSER2.Properties.DisplayMember = "MEMO"

            If oConn.State = ConnectionState.Open Then
                oConn.Close()
            End If
        Catch oErr As Exception
            MsgBox("Load Dokter Data : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
#End Region
End Class