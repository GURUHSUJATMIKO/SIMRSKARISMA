Imports DataAccess
Imports UI.WIN.MAIN.My.Resources
Imports System.Linq

Public Class frmDiagnosaPerawat
#Region "Declaration"
    Private oFormMode As FORM_MODE = FORM_MODE.FORM_MODE_VIEW
    Private sNoId As String
    Private isLoad As Boolean = False
    Private oDiagnosaPerawat As New Digital.clsDiagnosaPerawat
    Private sKDPENDAFTARAN As String = String.Empty
    Private sKDCUSTOMER As String = String.Empty
    Private sUSERPERAWAT As String = String.Empty

#End Region
#Region "Function"
    Public Sub LoadMe(ByVal FormMode As Integer, ByVal KDPENDAFTARAN As String, ByVal KDCUSTOMER As String, ByVal USERPERAWAT As String, Optional ByVal NoId As String = "")
        oFormMode = FormMode
        sNoId = NoId
        sKDPENDAFTARAN = KDPENDAFTARAN
        sKDCUSTOMER = KDCUSTOMER
        sUSERPERAWAT = USERPERAWAT
    End Sub
    Private Sub Form_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        fn_ChangeFormState()
        fn_LoadLanguage()
        isLoad = True
    End Sub
    Public Sub fn_LoadLanguage()
        Try
            Me.Text = "Diagnosa dan Rencana Asuhan Keperawatan"

            btnSaveNew.Caption = Caption.FormSaveNew
            btnSaveClose.Caption = Caption.FormSaveClose
            btnClose.Caption = Caption.FormClose
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub Form_Closing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        sCode = txtCODE.Text.Trim.ToUpper
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

        deDATE.Properties.ReadOnly = Status
        txtDESCRIPTION.Properties.ReadOnly = Status
        txtKRITERIA_TEXT.Properties.ReadOnly = Status
        txtDS.Properties.ReadOnly = Status
        If oFormMode = FORM_MODE.FORM_MODE_ADD Then
            grdKDITEMDIAGNOSAPERAWAT.Properties.ReadOnly = False
            SimpleButton2.Visible = True
        Else
            grdKDITEMDIAGNOSAPERAWAT.Properties.ReadOnly = True
            SimpleButton2.Visible = False
        End If

        'If oFormMode <> FORM_MODE.FORM_MODE_ADD Then
        '    grvDetail_1.OptionsBehavior.ReadOnly = True
        '    grvDetail_2.OptionsBehavior.ReadOnly = True
        '    grvDetail_3.OptionsBehavior.ReadOnly = True
        '    grvDetail_4.OptionsBehavior.ReadOnly = True
        '    grvDetail_5.OptionsBehavior.ReadOnly = True
        'End If
    End Sub
    Private Sub fn_EmptyMe()
        txtCODE.Text = "<--- AUTO --->"
        deDATE.DateTime = Now
        txtDESCRIPTION.ResetText()
        grdKDITEMDIAGNOSAPERAWAT.ResetText()
        txtKRITERIA_TEXT.Text = "setelah dilakukan perawatan dalam waktu .... x 24 jam"
        txtDS.Text = "DS :"
    End Sub
    Private Sub fn_LoadData()
        Try
            ' ***** HEADER *****
            Dim ds = oDiagnosaPerawat.GetData(sNoId)

            With ds
                txtCODE.Text = .KDDIAGNOSAPERAWAT
                grdKDITEMDIAGNOSAPERAWAT.Text = .KDITEMDIAGNOSAPERAWAT
                deDATE.DateTime = .DATE
                txtDESCRIPTION.Text = .DESCRIPTION
                txtKRITERIA_TEXT.Text = .KRITERIA_TEXT
                txtDS.Text = .DIAGNOSASUBJEKTIF
                cboKategori.Text = .M_ITEM_DIAGNOSA_PERAWAT_H.KATEGORI

                BindingSource1.DataSource = oDiagnosaPerawat.GetDataDetail1(sNoId).OrderBy(Function(x) x.SEQ).ToList()
                grdDetail_1.DataSource = BindingSource1

                BindingSource2.DataSource = oDiagnosaPerawat.GetDataDetail2(sNoId).OrderBy(Function(x) x.SEQ).ToList()
                grdDetail_2.DataSource = BindingSource2

                BindingSource3.DataSource = oDiagnosaPerawat.GetDataDetail3(sNoId).OrderBy(Function(x) x.SEQ).ToList()
                grdDetail_3.DataSource = BindingSource3

                BindingSource4.DataSource = oDiagnosaPerawat.GetDataDetail4(sNoId).OrderBy(Function(x) x.SEQ).ToList()
                grdDetail_4.DataSource = BindingSource4

                BindingSource5.DataSource = oDiagnosaPerawat.GetDataDetail5(sNoId).OrderBy(Function(x) x.SEQ).ToList()
                grdDetail_5.DataSource = BindingSource5

                BindingSource6.DataSource = oDiagnosaPerawat.GetDataDetail6(sNoId).OrderBy(Function(x) x.SEQ).ToList()
                grdDetail_6.DataSource = BindingSource6
            End With
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Function fn_Validate() As Boolean
        Try
            fn_Validate = True

            If grdKDITEMDIAGNOSAPERAWAT.Text = String.Empty Then
                grdKDITEMDIAGNOSAPERAWAT.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                grdKDITEMDIAGNOSAPERAWAT.ErrorText = Statement.ErrorRequired

                grdKDITEMDIAGNOSAPERAWAT.Focus()
                fn_Validate = False
                Exit Function
            End If

            If sKDPENDAFTARAN = String.Empty Then
                MsgBox("Kode Kunjungan Kosong", MsgBoxStyle.Exclamation, Me.Text)
                fn_Validate = False
                Exit Function
            End If

            grvDetail_1.UpdateCurrentRow()
            grvDetail_2.UpdateCurrentRow()
            grvDetail_3.UpdateCurrentRow()
            grvDetail_4.UpdateCurrentRow()
            grvDetail_5.UpdateCurrentRow()
            grvDetail_6.UpdateCurrentRow()

            'If grvDetail_1.RowCount < 2 Then
            '    MsgBox(Statement.ErrorDetail, MsgBoxStyle.Exclamation, Me.Text)
            '    fn_Validate = False
            '    Exit Function
            'End If
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Function
    Private Function fn_Save() As Boolean
        Try
            ' ***** HEADER *****
            Dim ds = oDiagnosaPerawat.GetStructureHeader
            With ds
                Try
                    .DATECREATED = oDiagnosaPerawat.GetData(sNoId).DATECREATED
                Catch oErr As Exception
                    .DATECREATED = Now
                End Try
                .DATEUPDATED = Now
                .KDDIAGNOSAPERAWAT = sNoId
                .KDITEMDIAGNOSAPERAWAT = grdKDITEMDIAGNOSAPERAWAT.EditValue
                .DATE = deDATE.DateTime
                .KDPENDAFTARAN = sKDPENDAFTARAN
                .KDCUSTOMER = sKDCUSTOMER
                Try
                    .ISACTIVE = oDiagnosaPerawat.GetData(sNoId).ISACTIVE
                Catch ex As Exception
                    .ISACTIVE = False
                End Try
                .DESCRIPTION = txtDESCRIPTION.Text.Trim
                .DIAGNOSASUBJEKTIF = txtDS.Text.Trim
                Try
                    .KDUSER = oDiagnosaPerawat.GetData(sNoId).KDUSER
                Catch ex As Exception
                    .KDUSER = sUSERPERAWAT
                End Try
                .KRITERIA_TEXT = txtKRITERIA_TEXT.Text
            End With

            ' ***** DETIL 1 *****
            Dim arrDetail1 = oDiagnosaPerawat.GetStructureDetailList1
            For i As Integer = 0 To grvDetail_1.RowCount - 2
                Dim dsDetail1 = oDiagnosaPerawat.GetStructureDetail1
                With dsDetail1
                    .SEQ = i
                    .KDDIAGNOSAPERAWAT = ds.KDDIAGNOSAPERAWAT
                    .BERHUBUNGANDENGAN = IIf(String.IsNullOrEmpty(grvDetail_1.GetRowCellValue(i, colBERHUBUNGANDENGAN)), "", grvDetail_1.GetRowCellValue(i, colBERHUBUNGANDENGAN))
                    .BERHUBUNGANDENGAN_ISCHEKED = grvDetail_1.GetRowCellValue(i, colBERHUBUNGANDENGAN_ISCHEKED)
                End With
                arrDetail1.Add(dsDetail1)
            Next

            ' ***** DETIL 2 *****
            Dim arrDetail2 = oDiagnosaPerawat.GetStructureDetailList2
            For i As Integer = 0 To grvDetail_2.RowCount - 2
                Dim dsDetail2 = oDiagnosaPerawat.GetStructureDetail2
                With dsDetail2
                    .SEQ = i
                    .KDDIAGNOSAPERAWAT = ds.KDDIAGNOSAPERAWAT
                    .DITANDAIDENGAN = IIf(String.IsNullOrEmpty(grvDetail_2.GetRowCellValue(i, colDITANDAIDENGAN)), "", grvDetail_2.GetRowCellValue(i, colDITANDAIDENGAN))
                    .DITANDAIDENGAN_ISCHEKED = grvDetail_2.GetRowCellValue(i, colDITANDAIDENGAN_ISCHEKED)
                End With
                arrDetail2.Add(dsDetail2)
            Next

            ' ***** DETIL 3 *****
            Dim arrDetail3 = oDiagnosaPerawat.GetStructureDetailList3
            For i As Integer = 0 To grvDetail_3.RowCount - 2
                Dim dsDetail3 = oDiagnosaPerawat.GetStructureDetail3
                With dsDetail3
                    .SEQ = i
                    .KDDIAGNOSAPERAWAT = ds.KDDIAGNOSAPERAWAT
                    .TUJUAN = IIf(String.IsNullOrEmpty(grvDetail_3.GetRowCellValue(i, colTUJUAN)), "", grvDetail_3.GetRowCellValue(i, colTUJUAN))
                    .TUJUAN_ISCHEKED = grvDetail_3.GetRowCellValue(i, colTUJUAN_ISCHEKED)
                End With
                arrDetail3.Add(dsDetail3)
            Next


            ' ***** DETIL 4 *****
            Dim arrDetail4 = oDiagnosaPerawat.GetStructureDetailList4
            For i As Integer = 0 To grvDetail_4.RowCount - 2
                Dim dsDetail4 = oDiagnosaPerawat.GetStructureDetail4
                With dsDetail4
                    .SEQ = i
                    .KDDIAGNOSAPERAWAT = ds.KDDIAGNOSAPERAWAT
                    .KRITERIA = IIf(String.IsNullOrEmpty(grvDetail_4.GetRowCellValue(i, colKRITERIA)), "", grvDetail_4.GetRowCellValue(i, colKRITERIA))
                    .KRITERIA_ISCHEKED = grvDetail_4.GetRowCellValue(i, colKRITERIA_ISCHEKED)
                End With
                arrDetail4.Add(dsDetail4)
            Next


            ' ***** DETIL 5 *****
            Dim arrDetail5 = oDiagnosaPerawat.GetStructureDetailList5
            For i As Integer = 0 To grvDetail_5.RowCount - 2
                Dim dsDetail5 = oDiagnosaPerawat.GetStructureDetail5
                With dsDetail5
                    .SEQ = i
                    .KDDIAGNOSAPERAWAT = ds.KDDIAGNOSAPERAWAT
                    .INTERVENSI = IIf(String.IsNullOrEmpty(grvDetail_5.GetRowCellValue(i, colINTERVENSI)), "", grvDetail_5.GetRowCellValue(i, colINTERVENSI))
                    .INTERVENSI_ISCHEKED = grvDetail_5.GetRowCellValue(i, colINTERVENSI_ISCHEKED)
                End With
                arrDetail5.Add(dsDetail5)
            Next


            ' ***** DETIL 6 *****
            Dim arrDetail6 = oDiagnosaPerawat.GetStructureDetailList6
            For i As Integer = 0 To grvDetail_6.RowCount - 2
                Dim dsDetail6 = oDiagnosaPerawat.GetStructureDetail6
                With dsDetail6
                    .TANGGAL = IIf(String.IsNullOrEmpty(grvDetail_6.GetRowCellValue(i, colTANGGAL)), "", grvDetail_6.GetRowCellValue(i, colTANGGAL))
                    .SEQ = i
                    .KDDIAGNOSAPERAWAT = ds.KDDIAGNOSAPERAWAT
                    .IMPLEMENTASI = IIf(String.IsNullOrEmpty(grvDetail_6.GetRowCellValue(i, colIMPLEMENTASI)), "", grvDetail_6.GetRowCellValue(i, colIMPLEMENTASI))
                    .IMPLEMENTASI_ISCHEKED = grvDetail_6.GetRowCellValue(i, colIMPLEMENTASI_ISCHEKED)
                    .KDUSER = IIf(String.IsNullOrEmpty(grvDetail_6.GetRowCellValue(i, colKDUSER)), "", grvDetail_6.GetRowCellValue(i, colKDUSER))
                    .EVALUASI = IIf(String.IsNullOrEmpty(grvDetail_6.GetRowCellValue(i, colEVALUASI)), "", grvDetail_6.GetRowCellValue(i, colEVALUASI))
                End With
                arrDetail6.Add(dsDetail6)
            Next

            If oFormMode = FORM_MODE.FORM_MODE_ADD Then
                Try
                    fn_Save = oDiagnosaPerawat.InsertData(ds, arrDetail1, arrDetail2, arrDetail3, arrDetail4, arrDetail5, arrDetail6)
                Catch oErr As Exception
                    MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            ElseIf oFormMode = FORM_MODE.FORM_MODE_EDIT Then
                Try
                    fn_Save = oDiagnosaPerawat.UpdateData(ds, arrDetail1, arrDetail2, arrDetail3, arrDetail4, arrDetail5, arrDetail6)
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
    Private Sub grvDetail_6_RowStyle(sender As System.Object, e As DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs) Handles grvDetail_6.RowStyle
        If grvDetail_6.IsFilterRow(e.RowHandle) Then Exit Sub

        If grvDetail_6.GetRowCellValue(e.RowHandle, "IMPLEMENTASI") = "SHIFT 1" Then
            e.Appearance.BackColor = Color.LightGreen
        ElseIf grvDetail_6.GetRowCellValue(e.RowHandle, "IMPLEMENTASI") = "SHIFT 2" Then
            e.Appearance.BackColor = Color.LightGreen
        ElseIf grvDetail_6.GetRowCellValue(e.RowHandle, "IMPLEMENTASI") = "SHIFT 3" Then
            e.Appearance.BackColor = Color.LightGreen
        End If

    End Sub
    Private Sub DeleteToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If oFormMode = FORM_MODE.FORM_MODE_VIEW Then Exit Sub
        grvDetail_1.DeleteSelectedRows()
    End Sub
    Private Sub grvDetail_6_CellValueChanged(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs) Handles grvDetail_6.CellValueChanged
        If isLoad = True Then
            If e.Column.Name = colIMPLEMENTASI_ISCHEKED.Name Then
                If grvDetail_6.GetFocusedRowCellValue(colIMPLEMENTASI_ISCHEKED) = False Then
                    grvDetail_6.SetFocusedRowCellValue(colTANGGAL, "")
                    grvDetail_6.SetFocusedRowCellValue(colKDUSER, "")
                Else
                    grvDetail_6.SetFocusedRowCellValue(colTANGGAL, Now.ToString("dd/MM/yyyy HH:mm"))
                    grvDetail_6.SetFocusedRowCellValue(colKDUSER, sUSERPERAWAT)
                End If
            End If
        End If
    End Sub
#End Region
#Region "Command Button"
    Private Sub frmDiagnosaPerawat_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        Select Case e.KeyCode
            Case Keys.F12
                btnClosee_Click()
            Case Keys.F2
                If btnSaveNew.Enabled = True Then
                    btnSaveNew_Click()
                End If
            Case Keys.F3
                If btnSaveClose.Enabled = True Then
                    btnSaveClosee_Click()
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
            Me.Close()
        End If
    End Sub
    Private Sub btnSaveClosee_Click() Handles btnSaveClosee.ItemClick
        If fn_Validate() = False Then Exit Sub
        If MsgBox(Statement.SaveQuestion, MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, Me.Text) = MsgBoxResult.No Then Exit Sub
        If fn_Save() = False Then
            MsgBox(Statement.SaveFail, MsgBoxStyle.Exclamation, Me.Text)
        Else
            MsgBox(Statement.SaveSuccess, MsgBoxStyle.Information, Me.Text)
            Me.Close()
        End If
    End Sub
    Private Sub btnClosee_Click() Handles btnClosee.ItemClick
        Me.Close()
    End Sub
#End Region
#Region "Lookup / Event"
    Private Sub fn_LoadKDITEMDIAGNOSAPERAWAT(ByVal sKategori As String)
        Try
            Dim ds = From x In oDiagnosaPerawat.GetDataItemAll()
                     Select x.DESCRIPTION, x.KDITEMDIAGNOSAPERAWAT, x.KATEGORI, x.ISACTIVE

            grdKDITEMDIAGNOSAPERAWAT.Properties.DataSource = IIf(oFormMode = FORM_MODE.FORM_MODE_ADD, ds.Where(Function(x) x.ISACTIVE = True And x.KATEGORI = sKategori).ToList(), ds.ToList())
            grdKDITEMDIAGNOSAPERAWAT.Properties.ValueMember = "KDITEMDIAGNOSAPERAWAT"
            grdKDITEMDIAGNOSAPERAWAT.Properties.DisplayMember = "DESCRIPTION"

        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub SimpleButton1_Click(sender As Object, e As EventArgs) Handles SimpleButton1.Click
        isLoad = False

        grvDetail_6.Focus()
        grvDetail_6.AddNewRow()
        grvDetail_6.SetFocusedRowCellValue(colIMPLEMENTASI, "SHIFT 1")
        grvDetail_6.SetFocusedRowCellValue(colIMPLEMENTASI_ISCHEKED, False)

        For Each xloop In oDiagnosaPerawat.GetDataItemDetil(grdKDITEMDIAGNOSAPERAWAT.EditValue)
            If xloop.IMPLEMENTASI <> "" And xloop.IMPLEMENTASI <> "...." Then
                grvDetail_6.Focus()
                grvDetail_6.AddNewRow()
                grvDetail_6.SetFocusedRowCellValue(colIMPLEMENTASI, xloop.IMPLEMENTASI)
                grvDetail_6.SetFocusedRowCellValue(colIMPLEMENTASI_ISCHEKED, xloop.ISCHEKED)
            End If
        Next

        For i As Integer = 0 To TextEdit_2.Text
            grvDetail_6.Focus()
            grvDetail_6.AddNewRow()
            grvDetail_6.SetFocusedRowCellValue(colIMPLEMENTASI, "")
            grvDetail_6.SetFocusedRowCellValue(colIMPLEMENTASI_ISCHEKED, False)
        Next

        grvDetail_6.Focus()
        grvDetail_6.AddNewRow()
        grvDetail_6.SetFocusedRowCellValue(colIMPLEMENTASI, "SHIFT 2")
        grvDetail_6.SetFocusedRowCellValue(colIMPLEMENTASI_ISCHEKED, False)

        For Each xloop In oDiagnosaPerawat.GetDataItemDetil(grdKDITEMDIAGNOSAPERAWAT.EditValue)
            If xloop.IMPLEMENTASI <> "" And xloop.IMPLEMENTASI <> "...." Then
                grvDetail_6.Focus()
                grvDetail_6.AddNewRow()
                grvDetail_6.SetFocusedRowCellValue(colIMPLEMENTASI, xloop.IMPLEMENTASI)
                grvDetail_6.SetFocusedRowCellValue(colIMPLEMENTASI_ISCHEKED, xloop.ISCHEKED)
            End If
        Next

        For i As Integer = 0 To TextEdit_2.Text
            grvDetail_6.Focus()
            grvDetail_6.AddNewRow()
            grvDetail_6.SetFocusedRowCellValue(colIMPLEMENTASI, "")
            grvDetail_6.SetFocusedRowCellValue(colIMPLEMENTASI_ISCHEKED, False)
        Next

        grvDetail_6.Focus()
        grvDetail_6.AddNewRow()
        grvDetail_6.SetFocusedRowCellValue(colIMPLEMENTASI, "SHIFT 3")
        grvDetail_6.SetFocusedRowCellValue(colIMPLEMENTASI_ISCHEKED, False)

        For Each xloop In oDiagnosaPerawat.GetDataItemDetil(grdKDITEMDIAGNOSAPERAWAT.EditValue)
            If xloop.IMPLEMENTASI <> "" And xloop.IMPLEMENTASI <> "...." Then
                grvDetail_6.Focus()
                grvDetail_6.AddNewRow()
                grvDetail_6.SetFocusedRowCellValue(colIMPLEMENTASI, xloop.IMPLEMENTASI)
                grvDetail_6.SetFocusedRowCellValue(colIMPLEMENTASI_ISCHEKED, xloop.ISCHEKED)
            End If
        Next

        For i As Integer = 0 To TextEdit_2.Text
            grvDetail_6.Focus()
            grvDetail_6.AddNewRow()
            grvDetail_6.SetFocusedRowCellValue(colIMPLEMENTASI, "")
            grvDetail_6.SetFocusedRowCellValue(colIMPLEMENTASI_ISCHEKED, False)
        Next

        isLoad = True
    End Sub
    Private Sub SimpleButton2_Click(sender As Object, e As EventArgs) Handles SimpleButton2.Click
        If grdKDITEMDIAGNOSAPERAWAT.Text = String.Empty Then
            Exit Sub
        End If

        isLoad = False

        Dim ds = oDiagnosaPerawat.GetDataKunjunganAndkditem(sKDPENDAFTARAN, grdKDITEMDIAGNOSAPERAWAT.EditValue)

        If ds IsNot Nothing Then
            MsgBox("Diagnosa Sudah Ada dengan nomor Kode " & ds.KDDIAGNOSAPERAWAT, MsgBoxStyle.Exclamation, Me.Text)

            'For Each xloop In oDiagnosaPerawat.GetDataDetail1(ds.KDDIAGNOSAPERAWAT)
            '    grvDetail_1.Focus()
            '    grvDetail_1.AddNewRow()
            '    grvDetail_1.SetFocusedRowCellValue(colBERHUBUNGANDENGAN, xloop.BERHUBUNGANDENGAN)
            '    grvDetail_1.SetFocusedRowCellValue(colBERHUBUNGANDENGAN_ISCHEKED, xloop.BERHUBUNGANDENGAN_ISCHEKED)
            'Next

            'For Each xloop In oDiagnosaPerawat.GetDataDetail2(ds.KDDIAGNOSAPERAWAT)
            '    grvDetail_2.Focus()
            '    grvDetail_2.AddNewRow()
            '    grvDetail_2.SetFocusedRowCellValue(colDITANDAIDENGAN, xloop.DITANDAIDENGAN)
            '    grvDetail_2.SetFocusedRowCellValue(colDITANDAIDENGAN_ISCHEKED, xloop.DITANDAIDENGAN_ISCHEKED)
            'Next

            'For Each xloop In oDiagnosaPerawat.GetDataDetail3(ds.KDDIAGNOSAPERAWAT)
            '    grvDetail_3.Focus()
            '    grvDetail_3.AddNewRow()
            '    grvDetail_3.SetFocusedRowCellValue(colTUJUAN, xloop.TUJUAN)
            '    grvDetail_3.SetFocusedRowCellValue(colTUJUAN_ISCHEKED, xloop.TUJUAN_ISCHEKED)
            'Next

            'For Each xloop In oDiagnosaPerawat.GetDataDetail4(ds.KDDIAGNOSAPERAWAT)
            '    grvDetail_4.Focus()
            '    grvDetail_4.AddNewRow()
            '    grvDetail_4.SetFocusedRowCellValue(colKRITERIA, xloop.KRITERIA)
            '    grvDetail_4.SetFocusedRowCellValue(colKRITERIA_ISCHEKED, xloop.KRITERIA_ISCHEKED)
            'Next

            'For Each xloop In oDiagnosaPerawat.GetDataDetail5(ds.KDDIAGNOSAPERAWAT)
            '    grvDetail_5.Focus()
            '    grvDetail_5.AddNewRow()
            '    grvDetail_5.SetFocusedRowCellValue(colINTERVENSI, xloop.INTERVENSI)
            '    grvDetail_5.SetFocusedRowCellValue(colINTERVENSI_ISCHEKED, xloop.INTERVENSI_ISCHEKED)
            'Next

            'For Each xloop In oDiagnosaPerawat.GetDataDetail6(ds.KDDIAGNOSAPERAWAT)
            '    grvDetail_6.Focus()
            '    grvDetail_6.AddNewRow()
            '    grvDetail_6.SetFocusedRowCellValue(colINTERVENSI, xloop.TANGGAL)
            '    grvDetail_6.SetFocusedRowCellValue(colINTERVENSI, xloop.IMPLEMENTASI)
            '    grvDetail_6.SetFocusedRowCellValue(colINTERVENSI_ISCHEKED, xloop.IMPLEMENTASI_ISCHEKED)
            '    grvDetail_6.SetFocusedRowCellValue(colINTERVENSI_ISCHEKED, xloop.IMPLEMENTASI_ISCHEKED)
            'Next
        Else
            grvDetail_1.OptionsSelection.MultiSelect = True
            grvDetail_1.SelectAll()
            grvDetail_1.DeleteSelectedRows()
            grvDetail_1.OptionsSelection.MultiSelect = False

            grvDetail_2.OptionsSelection.MultiSelect = True
            grvDetail_2.SelectAll()
            grvDetail_2.DeleteSelectedRows()
            grvDetail_2.OptionsSelection.MultiSelect = False

            grvDetail_3.OptionsSelection.MultiSelect = True
            grvDetail_3.SelectAll()
            grvDetail_3.DeleteSelectedRows()
            grvDetail_3.OptionsSelection.MultiSelect = False

            grvDetail_4.OptionsSelection.MultiSelect = True
            grvDetail_4.SelectAll()
            grvDetail_4.DeleteSelectedRows()
            grvDetail_4.OptionsSelection.MultiSelect = False

            grvDetail_5.OptionsSelection.MultiSelect = True
            grvDetail_5.SelectAll()
            grvDetail_5.DeleteSelectedRows()
            grvDetail_5.OptionsSelection.MultiSelect = False

            grvDetail_6.OptionsSelection.MultiSelect = True
            grvDetail_6.SelectAll()
            grvDetail_6.DeleteSelectedRows()
            grvDetail_6.OptionsSelection.MultiSelect = False

            For Each xloop In oDiagnosaPerawat.GetDataItemDetil(grdKDITEMDIAGNOSAPERAWAT.EditValue)
                If xloop.BERHUBUNGANDENGAN <> "" And xloop.BERHUBUNGANDENGAN <> "...." Then
                    grvDetail_1.Focus()
                    grvDetail_1.AddNewRow()
                    grvDetail_1.SetFocusedRowCellValue(colBERHUBUNGANDENGAN, xloop.BERHUBUNGANDENGAN)
                    grvDetail_1.SetFocusedRowCellValue(colBERHUBUNGANDENGAN_ISCHEKED, xloop.ISCHEKED)
                End If

                If xloop.DITANDAIDENGAN <> "" And xloop.DITANDAIDENGAN <> "...." Then
                    grvDetail_2.Focus()
                    grvDetail_2.AddNewRow()
                    grvDetail_2.SetFocusedRowCellValue(colDITANDAIDENGAN, xloop.DITANDAIDENGAN)
                    grvDetail_2.SetFocusedRowCellValue(colDITANDAIDENGAN_ISCHEKED, xloop.ISCHEKED)
                End If

                If xloop.TUJUAN <> "" And xloop.TUJUAN <> "...." Then
                    grvDetail_3.Focus()
                    grvDetail_3.AddNewRow()
                    grvDetail_3.SetFocusedRowCellValue(colTUJUAN, xloop.TUJUAN)
                    grvDetail_3.SetFocusedRowCellValue(colTUJUAN_ISCHEKED, xloop.ISCHEKED)
                End If

                If xloop.KRITERIA <> "" And xloop.KRITERIA <> "...." Then
                    grvDetail_4.Focus()
                    grvDetail_4.AddNewRow()
                    grvDetail_4.SetFocusedRowCellValue(colKRITERIA, xloop.KRITERIA)
                    grvDetail_4.SetFocusedRowCellValue(colKRITERIA_ISCHEKED, xloop.ISCHEKED)
                End If

                If xloop.INTERVENSI <> "" And xloop.INTERVENSI <> "...." Then
                    grvDetail_5.Focus()
                    grvDetail_5.AddNewRow()
                    grvDetail_5.SetFocusedRowCellValue(colINTERVENSI, xloop.INTERVENSI)
                    grvDetail_5.SetFocusedRowCellValue(colINTERVENSI_ISCHEKED, xloop.ISCHEKED)

                End If
            Next

            grvDetail_6.Focus()
            grvDetail_6.AddNewRow()
            grvDetail_6.SetFocusedRowCellValue(colIMPLEMENTASI, "SHIFT 1")
            grvDetail_6.SetFocusedRowCellValue(colIMPLEMENTASI_ISCHEKED, False)

            For Each xloop In oDiagnosaPerawat.GetDataItemDetil(grdKDITEMDIAGNOSAPERAWAT.EditValue)
                If xloop.IMPLEMENTASI <> "" And xloop.IMPLEMENTASI <> "...." Then
                    grvDetail_6.Focus()
                    grvDetail_6.AddNewRow()
                    grvDetail_6.SetFocusedRowCellValue(colIMPLEMENTASI, xloop.IMPLEMENTASI)
                    grvDetail_6.SetFocusedRowCellValue(colIMPLEMENTASI_ISCHEKED, xloop.ISCHEKED)
                End If
            Next

            For i As Integer = 0 To TextEdit_1.Text
                grvDetail_6.Focus()
                grvDetail_6.AddNewRow()
                grvDetail_6.SetFocusedRowCellValue(colIMPLEMENTASI, "")
                grvDetail_6.SetFocusedRowCellValue(colIMPLEMENTASI_ISCHEKED, False)
            Next

            grvDetail_6.Focus()
            grvDetail_6.AddNewRow()
            grvDetail_6.SetFocusedRowCellValue(colIMPLEMENTASI, "SHIFT 2")
            grvDetail_6.SetFocusedRowCellValue(colIMPLEMENTASI_ISCHEKED, False)

            For Each xloop In oDiagnosaPerawat.GetDataItemDetil(grdKDITEMDIAGNOSAPERAWAT.EditValue)
                If xloop.IMPLEMENTASI <> "" And xloop.IMPLEMENTASI <> "...." Then
                    grvDetail_6.Focus()
                    grvDetail_6.AddNewRow()
                    grvDetail_6.SetFocusedRowCellValue(colIMPLEMENTASI, xloop.IMPLEMENTASI)
                    grvDetail_6.SetFocusedRowCellValue(colIMPLEMENTASI_ISCHEKED, xloop.ISCHEKED)
                End If
            Next

            For i As Integer = 0 To TextEdit_1.Text
                grvDetail_6.Focus()
                grvDetail_6.AddNewRow()
                grvDetail_6.SetFocusedRowCellValue(colIMPLEMENTASI, "")
                grvDetail_6.SetFocusedRowCellValue(colIMPLEMENTASI_ISCHEKED, False)
            Next

            grvDetail_6.Focus()
            grvDetail_6.AddNewRow()
            grvDetail_6.SetFocusedRowCellValue(colIMPLEMENTASI, "SHIFT 3")
            grvDetail_6.SetFocusedRowCellValue(colIMPLEMENTASI_ISCHEKED, False)

            For Each xloop In oDiagnosaPerawat.GetDataItemDetil(grdKDITEMDIAGNOSAPERAWAT.EditValue)
                If xloop.IMPLEMENTASI <> "" And xloop.IMPLEMENTASI <> "...." Then
                    grvDetail_6.Focus()
                    grvDetail_6.AddNewRow()
                    grvDetail_6.SetFocusedRowCellValue(colIMPLEMENTASI, xloop.IMPLEMENTASI)
                    grvDetail_6.SetFocusedRowCellValue(colIMPLEMENTASI_ISCHEKED, xloop.ISCHEKED)
                End If
            Next

            For i As Integer = 0 To TextEdit_1.Text
                grvDetail_6.Focus()
                grvDetail_6.AddNewRow()
                grvDetail_6.SetFocusedRowCellValue(colIMPLEMENTASI, "")
                grvDetail_6.SetFocusedRowCellValue(colIMPLEMENTASI_ISCHEKED, False)
            Next

        End If

        isLoad = True
    End Sub
    Private Sub cboKategori_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboKategori.SelectedIndexChanged
        fn_LoadKDITEMDIAGNOSAPERAWAT(cboKategori.Text)
        If cboKategori.Text = "RISIKO" Or cboKategori.Text = "PROMKES" Then
            grvDetail_1.Columns("BERHUBUNGANDENGAN").Caption = "Dibuktikan dengan"
        Else
            grvDetail_1.Columns("BERHUBUNGANDENGAN").Caption = "Berhubungan dengan"
        End If

        If cboKategori.Text = "RISIKO" Then
            grvDetail_2.Columns("DITANDAIDENGAN").Caption = "Faktor risiko"
        ElseIf cboKategori.Text = "PROMKES" Then
            grvDetail_2.Columns("DITANDAIDENGAN").Caption = "Tanda dan gejala"
        Else
            grvDetail_2.Columns("DITANDAIDENGAN").Caption = "Ditandai dengan"
        End If
    End Sub
#End Region
End Class