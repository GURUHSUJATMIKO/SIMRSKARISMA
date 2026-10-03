Imports DataAccess
Imports UI.WIN.MAIN.My.Resources
Imports System.Linq

Public Class frmItemDiagnosaPerawat
#Region "Declaration"
    Private oFormMode As FORM_MODE = FORM_MODE.FORM_MODE_VIEW
    Private sNoId As String
    Private isLoad As Boolean = False
    Private oItemDiagnosaPerawat As New Master.clsItemDiagnosaPerawat
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
        sCode = txtDESCRIPTION.Text.Trim.ToUpper
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
        If oFormMode = FORM_MODE.FORM_MODE_ADD Then
            txtKODE.Properties.ReadOnly = False
        Else
            txtKODE.Properties.ReadOnly = True
        End If
        txtDESCRIPTION.Properties.ReadOnly = Status
        chkISACTIVE.Properties.ReadOnly = Status
    End Sub
    Private Sub fn_EmptyMe()
        txtKODE.ResetText()
        txtDESCRIPTION.ResetText()
        chkISACTIVE.Checked = True
    End Sub
    Private Sub fn_LoadData()
        Try
            ' ***** HEADER *****
            Dim ds = oItemDiagnosaPerawat.GetData(sNoId)

            With ds
                txtKODE.Text = ds.KDITEMDIAGNOSAPERAWAT
                txtDESCRIPTION.Text = .DESCRIPTION
                cboKategori.EditValue = .KATEGORI
                chkISACTIVE.Checked = .ISACTIVE

                MITEMDIAGNOSAPERAWATDBindingSource.DataSource = oItemDiagnosaPerawat.GetDataDetail.Where(Function(x) x.KDITEMDIAGNOSAPERAWAT = sNoId).OrderBy(Function(x) x.SEQ).ToList()
                grdDetail_ITEM.DataSource = MITEMDIAGNOSAPERAWATDBindingSource

                tabControl.SelectedTabPage = tab1
            End With
        Catch oErr As Exception
            MsgBox("Load List Data : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Function fn_Validate() As Boolean
        Try
            fn_Validate = True
            If txtKODE.Text = String.Empty Then
                MsgBox("Dibutuhkan Kode", MsgBoxStyle.Exclamation, Me.Text)
                txtKODE.Focus()
                fn_Validate = False
                Exit Function
            End If
            If txtDESCRIPTION.Text = String.Empty Then
                MsgBox("Dibutuhkan Tampilan Nama", MsgBoxStyle.Exclamation, Me.Text)
                txtDESCRIPTION.Focus()
                fn_Validate = False
                Exit Function
            End If
            If cboKategori.Text = String.Empty Then
                MsgBox("Dibutuhkan Kategori", MsgBoxStyle.Exclamation, Me.Text)
                cboKategori.Focus()
                fn_Validate = False
                Exit Function
            End If
            'If oFormMode = FORM_MODE.FORM_MODE_ADD Then
            '    If oItemDiagnosaPerawat.IsExist(txtNAME_DISPLAY.Text.ToUpper.Trim) = True Then
            '        MsgBox(txtNAME_DISPLAY.Text.ToUpper.Trim & " sudah terdaftar!", MsgBoxStyle.Exclamation, Me.Text)
            '        txtNAME_DISPLAY.Focus()
            '        fn_Validate = False
            '        Exit Function
            '    End If
            'Else
            '    If txtNAME_DISPLAY.Text.Trim.ToUpper <> oItemDiagnosaPerawat.GetData(sNoId).NAME_DISPLAY Then
            '        If oItemDiagnosaPerawat.IsExist(txtNAME_DISPLAY.Text.ToUpper.Trim) = True Then
            '            MsgBox(txtNAME_DISPLAY.Text.ToUpper.Trim & " sudah terdaftar!", MsgBoxStyle.Exclamation, Me.Text)
            '            txtNAME_DISPLAY.Focus()
            '            fn_Validate = False
            '            Exit Function
            '        End If
            '    End If
            'End If

            grvDetail_Item.UpdateCurrentRow()
        Catch oErr As Exception
            MsgBox("Validate Data : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Function
    Private Function fn_Save() As Boolean
        Try
            ' ***** HEADER *****
            Dim ds = oItemDiagnosaPerawat.GetStructureHeader
            With ds
                .KDITEMDIAGNOSAPERAWAT = txtKODE.Text
                Try
                    .DATECREATED = oItemDiagnosaPerawat.GetData(sNoId).DATECREATED
                Catch ex As Exception
                    .DATECREATED = Now
                End Try
                .DATEUPDATED = Now
                .DESCRIPTION = txtDESCRIPTION.Text.Trim.ToUpper
                .ISACTIVE = chkISACTIVE.Checked
                .KATEGORI = cboKategori.Text
            End With

            ' ***** ITEM *****
            Dim arrDetail_ITEM = oItemDiagnosaPerawat.GetStructureDetailList
            For i As Integer = 0 To grvDetail_Item.RowCount - 2
                Dim dsDetail_ITEM = oItemDiagnosaPerawat.GetStructureDetail
                With dsDetail_ITEM
                    .KDITEMDIAGNOSAPERAWAT = ds.KDITEMDIAGNOSAPERAWAT
                    .SEQ = i
                    .BERHUBUNGANDENGAN = grvDetail_Item.GetRowCellValue(i, colBERHUBUNGANDENGAN)
                    .DITANDAIDENGAN = grvDetail_Item.GetRowCellValue(i, colDITANDAIDENGAN)
                    .TUJUAN = grvDetail_Item.GetRowCellValue(i, colTujuan)
                    .EVALUASI = grvDetail_Item.GetRowCellValue(i, colEVALUASI)
                    .INTERVENSI = grvDetail_Item.GetRowCellValue(i, colINTERVENSI)
                    .IMPLEMENTASI = grvDetail_Item.GetRowCellValue(i, colIMPLEMENTASI)
                    .KRITERIA = grvDetail_Item.GetRowCellValue(i, colKRITERIA)
                    .ISCHEKED = grvDetail_Item.GetRowCellValue(i, colISCHEKED)
                End With
                arrDetail_ITEM.Add(dsDetail_ITEM)
            Next

            If oFormMode = FORM_MODE.FORM_MODE_ADD Then
                Try
                    fn_Save = oItemDiagnosaPerawat.InsertData(ds, arrDetail_ITEM)
                Catch ex As Exception
                    MsgBox("Simpan Data : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            ElseIf oFormMode = FORM_MODE.FORM_MODE_EDIT Then
                Try
                    fn_Save = oItemDiagnosaPerawat.UpdateData(ds, arrDetail_ITEM)
                Catch ex As Exception
                    MsgBox("Simpan Data : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            End If
        Catch oErr As Exception
            MsgBox("Simpan Data : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            fn_Save = False
        End Try
    End Function
    Private Sub DeleteItemToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DeleteItemToolStripMenuItem.Click
        If oFormMode = FORM_MODE.FORM_MODE_VIEW Then Exit Sub
        grvDetail_Item.DeleteSelectedRows()
    End Sub
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
        If MsgBox("Save " & txtDESCRIPTION.Text.Trim.ToUpper & "?", MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, Me.Text) = MsgBoxResult.No Then Exit Sub
        If fn_Save() = False Then
            MsgBox("Save gagal! Tolong kontak admin anda!", MsgBoxStyle.Exclamation, Me.Text)
        Else
            MsgBox("Save " & txtDESCRIPTION.Text.Trim.ToUpper & " success!", MsgBoxStyle.Information, Me.Text)
            Me.Close()
        End If
    End Sub
    Private Sub btnClose_Click() Handles btnClose.ItemClick
        Me.Close()
    End Sub



#End Region
#Region "Lookup / Event"
    Private Sub cboKategori_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboKategori.SelectedIndexChanged
        If cboKategori.Text = "RISIKO" Or cboKategori.Text = "PROMKES" Then
            grvDetail_Item.Columns("BERHUBUNGANDENGAN").Caption = "Dibuktikan dengan"
        Else
            grvDetail_Item.Columns("BERHUBUNGANDENGAN").Caption = "Berhubungan dengan"
        End If

        If cboKategori.Text = "RISIKO" Then
            grvDetail_Item.Columns("DITANDAIDENGAN").Caption = "Faktor risiko"
        ElseIf cboKategori.Text = "PROMKES" Then
            grvDetail_Item.Columns("DITANDAIDENGAN").Caption = "Tanda dan gejala"
        Else
            grvDetail_Item.Columns("DITANDAIDENGAN").Caption = "Ditandai dengan"
        End If
    End Sub
#End Region
End Class