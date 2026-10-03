Imports DataAccess
Imports UI.WIN.MAIN.My.Resources
Imports System.Linq
Imports System.Data.SqlClient

Public Class frmDigital_IGD_01_GERD
#Region "Declaration"
    Private oFormMode As FORM_MODE = FORM_MODE.FORM_MODE_VIEW
    Private sNoId As String
    Private isLoad As Boolean = False
    Private oDigital_IGD_01_GERD As New Inventory.clsDigital_IGD_01_GERD
    Private sRegister As String = String.Empty
    Private sNAMA As String = String.Empty
    Private sRM As String = String.Empty
#End Region
#Region "Function"
    Public Sub LoadMe(ByVal FormMode As Integer, ByVal KDREG As String, ByVal NAMA As String, ByVal NORM As String, Optional ByVal NoId As String = "")
        oFormMode = FormMode
        sNoId = NoId
        sRegister = KDREG
        sNAMA = NAMA
        sRM = NORM
    End Sub
    Private Sub Form_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        fn_ChangeFormState()
        fn_LoadLanguage()
        isLoad = True
    End Sub
    Public Sub fn_LoadLanguage()
        Try
            Me.Text = "Tabel Diagnosis GERD-Q"

            'lKDDigital_IGD_01_GERD.Text = Digital_IGD_01_GERD.KDDigital_IGD_01_GERD
            'lDATE.Text = Digital_IGD_01_GERD.TANGGAL
            'lKDWAREHOUSE.Text = Digital_IGD_01_GERD.KDWAREHOUSE & " *"

            'tab1.Text = Digital_IGD_01_GERD.TAB_DETAIL
            'tab2.Text = Digital_IGD_01_GERD.TAB_MEMO

            'grvDetail.Columns("KDITEM").Caption = Digital_IGD_01_GERD.DETAIL_KDITEM
            'grvDetail.Columns("KDUOM").Caption = Digital_IGD_01_GERD.DETAIL_KDUOM
            'grvDetail.Columns("QTY").Caption = Digital_IGD_01_GERD.DETAIL_QTY
            'grvDetail.Columns("REMARKS").Caption = Digital_IGD_01_GERD.DETAIL_REMARKS

            'grvKDWAREHOUSE.Columns("NAME_DISPLAY").Caption = Warehouse.NAME_DISPLAY

            ''grvKDITEM.Columns("NMITEM1").Caption = Item.NMITEM1
            'grvKDITEM.Columns("NMITEM2").Caption = Item.NMITEM2
            ''grvKDITEM.Columns("NMITEM3").Caption = Item.NMITEM3

            'grvKDUOM.Columns("MEMO").Caption = UOM.MEMO

            btnSaveNew.Caption = Caption.FormSaveNew
            btnSaveClose.Caption = Caption.FormSaveClose
            btnClose.Caption = Caption.FormClose
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub Form_Closing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        sCode = txtKDGERD.Text.Trim.ToUpper
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
        txtRUANGRAWAT.Properties.ReadOnly = Status
        txtDIAGNOSA.Properties.ReadOnly = Status

        grvDetail.OptionsBehavior.ReadOnly = Status
    End Sub
    Private Sub fn_EmptyMe()
        txtKDGERD.Text = "<--- AUTO --->"
        deDATE.DateTime = Now
        txtKDPENDAFTARAN.ResetText()
        txtNAMAPASIEN.ResetText()
        txtNOREKAMMEDIS.ResetText()
        txtRUANGRAWAT.ResetText()
        txtDIAGNOSA.ResetText()

        txtKDPENDAFTARAN.Text = sRegister
        txtNAMAPASIEN.Text = sNAMA
        txtNOREKAMMEDIS.Text = sRM

        'grvDetail.AddNewRow()
        'grvDetail.SetFocusedRowCellValue(colPERTANYAAN, "Seberapa sering Anda mengalami perasaan terbakar di bagian belakang tulang dada (heartburn)?")
        'grvDetail.SetFocusedRowCellValue(colFREKUENSI_1, 0)
        'grvDetail.SetFocusedRowCellValue(colFREKUENSI_2, 1)
        'grvDetail.SetFocusedRowCellValue(colFREKUENSI_3, 2)
        'grvDetail.SetFocusedRowCellValue(colFREKUENSI_4, 3)
        'grvDetail.SetFocusedRowCellValue(colFREKUENSI_4, 0)

        'grvDetail.AddNewRow()
        'grvDetail.SetFocusedRowCellValue(colPERTANYAAN, "Seberapa sering Anda mengalami naiknya isi lambung ke arah tenggorokan atau mulut (regurgitasi)?")
        'grvDetail.SetFocusedRowCellValue(colFREKUENSI_1, 0)
        'grvDetail.SetFocusedRowCellValue(colFREKUENSI_2, 1)
        'grvDetail.SetFocusedRowCellValue(colFREKUENSI_3, 2)
        'grvDetail.SetFocusedRowCellValue(colFREKUENSI_4, 3)
        'grvDetail.SetFocusedRowCellValue(colFREKUENSI_4, 0)

        'grvDetail.AddNewRow()
        'grvDetail.SetFocusedRowCellValue(colPERTANYAAN, "Seberapa sering Anda mengalami nyeri ulu hati?")
        'grvDetail.SetFocusedRowCellValue(colFREKUENSI_1, 3)
        'grvDetail.SetFocusedRowCellValue(colFREKUENSI_2, 2)
        'grvDetail.SetFocusedRowCellValue(colFREKUENSI_3, 1)
        'grvDetail.SetFocusedRowCellValue(colFREKUENSI_4, 0)
        'grvDetail.SetFocusedRowCellValue(colFREKUENSI_4, 0)

        'grvDetail.AddNewRow()
        'grvDetail.SetFocusedRowCellValue(colPERTANYAAN, "Seberapa sering Anda mengalami mual?")
        'grvDetail.SetFocusedRowCellValue(colFREKUENSI_1, 3)
        'grvDetail.SetFocusedRowCellValue(colFREKUENSI_2, 2)
        'grvDetail.SetFocusedRowCellValue(colFREKUENSI_3, 1)
        'grvDetail.SetFocusedRowCellValue(colFREKUENSI_4, 0)
        'grvDetail.SetFocusedRowCellValue(colFREKUENSI_4, 0)

        'grvDetail.AddNewRow()
        'grvDetail.SetFocusedRowCellValue(colPERTANYAAN, "Seberapa sering Anda mengalami kesulitan tidur malam oleh karena rasa terbakar di dada atau naiknya isi perut?")
        'grvDetail.SetFocusedRowCellValue(colFREKUENSI_1, 0)
        'grvDetail.SetFocusedRowCellValue(colFREKUENSI_2, 1)
        'grvDetail.SetFocusedRowCellValue(colFREKUENSI_3, 2)
        'grvDetail.SetFocusedRowCellValue(colFREKUENSI_4, 3)
        'grvDetail.SetFocusedRowCellValue(colFREKUENSI_4, 0)

        'grvDetail.AddNewRow()
        'grvDetail.SetFocusedRowCellValue(colPERTANYAAN, "Seberapa sering Anda meminum obat tambahan untuk rasa terbakar di dada dan/baiknya isi perut, selain yang di berikan oleh dokter Anda? (seperti obat maag yang di jual bebas)")
        'grvDetail.SetFocusedRowCellValue(colFREKUENSI_1, 0)
        'grvDetail.SetFocusedRowCellValue(colFREKUENSI_2, 1)
        'grvDetail.SetFocusedRowCellValue(colFREKUENSI_3, 2)
        'grvDetail.SetFocusedRowCellValue(colFREKUENSI_4, 3)
        'grvDetail.SetFocusedRowCellValue(colFREKUENSI_4, 0)


        grvDetail.AddNewRow()
        grvDetail.SetFocusedRowCellValue(colPERTANYAAN, "Seberapa sering Anda mengalami perasaan terbakar di bagian belakang tulang dada (heartburn) ?")
        grvDetail.SetFocusedRowCellValue(colFREKUENSI_1, 0)
        grvDetail.SetFocusedRowCellValue(colFREKUENSI_2, 1)
        grvDetail.SetFocusedRowCellValue(colFREKUENSI_3, 2)
        grvDetail.SetFocusedRowCellValue(colFREKUENSI_4, 3)
        'grvDetail.SetFocusedRowCellValue(colFREKUENSI_4, 0)

        grvDetail.AddNewRow()
        grvDetail.SetFocusedRowCellValue(colPERTANYAAN, "Seberapa sering Anda merasa isi lambung (cairan atau makanan) naik ke arah kerongkongan atau mulut (Regurgitasi) ?")
        grvDetail.SetFocusedRowCellValue(colFREKUENSI_1, 0)
        grvDetail.SetFocusedRowCellValue(colFREKUENSI_2, 1)
        grvDetail.SetFocusedRowCellValue(colFREKUENSI_3, 2)
        grvDetail.SetFocusedRowCellValue(colFREKUENSI_4, 3)
        'grvDetail.SetFocusedRowCellValue(colFREKUENSI_4, 0)

        grvDetail.AddNewRow()
        grvDetail.SetFocusedRowCellValue(colPERTANYAAN, "Seberapa sering Anda merasa nyeri pada bagian tengah perut atas ?")
        grvDetail.SetFocusedRowCellValue(colFREKUENSI_1, 3)
        grvDetail.SetFocusedRowCellValue(colFREKUENSI_2, 2)
        grvDetail.SetFocusedRowCellValue(colFREKUENSI_3, 1)
        grvDetail.SetFocusedRowCellValue(colFREKUENSI_4, 3)
        'grvDetail.SetFocusedRowCellValue(colFREKUENSI_4, 0)

        grvDetail.AddNewRow()
        grvDetail.SetFocusedRowCellValue(colPERTANYAAN, "Seberapa sering Anda merasa mual ?")
        grvDetail.SetFocusedRowCellValue(colFREKUENSI_1, 3)
        grvDetail.SetFocusedRowCellValue(colFREKUENSI_2, 2)
        grvDetail.SetFocusedRowCellValue(colFREKUENSI_3, 1)
        grvDetail.SetFocusedRowCellValue(colFREKUENSI_4, 0)
        'grvDetail.SetFocusedRowCellValue(colFREKUENSI_4, 0)

        grvDetail.AddNewRow()
        grvDetail.SetFocusedRowCellValue(colPERTANYAAN, "Seberapa sering kenyamanan tidur malam Anda terganggu oleh heartburn atau regurgitasi yang Anda alami ? ")
        grvDetail.SetFocusedRowCellValue(colFREKUENSI_1, 0)
        grvDetail.SetFocusedRowCellValue(colFREKUENSI_2, 1)
        grvDetail.SetFocusedRowCellValue(colFREKUENSI_3, 2)
        grvDetail.SetFocusedRowCellValue(colFREKUENSI_4, 3)
        'grvDetail.SetFocusedRowCellValue(colFREKUENSI_4, 0)

        grvDetail.AddNewRow()
        grvDetail.SetFocusedRowCellValue(colPERTANYAAN, "Seberapa sering Anda meminum obat tambahan untuk heartburn dan atau regurgitasi yang Anda alami selain dari apa yang telah dianjurkan oleh dokter (seperti obat maag yang dijual bebas)")
        grvDetail.SetFocusedRowCellValue(colFREKUENSI_1, 0)
        grvDetail.SetFocusedRowCellValue(colFREKUENSI_2, 1)
        grvDetail.SetFocusedRowCellValue(colFREKUENSI_3, 2)
        grvDetail.SetFocusedRowCellValue(colFREKUENSI_4, 3)
        'grvDetail.SetFocusedRowCellValue(colFREKUENSI_4, 0)

        grvDetail.UpdateCurrentRow()

    End Sub
    Private Sub fn_LoadData()
        Try
            ' ***** HEADER *****
            Dim ds = oDigital_IGD_01_GERD.GetData(sNoId)

            With ds
                txtKDGERD.Text = .KDGERD
                deDATE.DateTime = .DATE

                txtKDPENDAFTARAN.Text = .KDPENDAFTARAN
                txtNAMAPASIEN.Text = .NAMAPASIEN
                txtNOREKAMMEDIS.Text = .NOREKAMMEDIS
                txtRUANGRAWAT.Text = .RUANGRAWAT
                txtDIAGNOSA.Text = .DIAGNOSA
                txtHASIL.Text = .HASIL

                bindingSource.DataSource = oDigital_IGD_01_GERD.GetDataDetail.Where(Function(x) x.KDGERD = sNoId).OrderBy(Function(x) x.SEQ).ToList()
                grdDetail.DataSource = bindingSource
            End With
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Function fn_Validate() As Boolean
        Try
            fn_Validate = True

            If txtKDPENDAFTARAN.Text = String.Empty Then
                txtKDPENDAFTARAN.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                txtKDPENDAFTARAN.ErrorText = Statement.ErrorRequired

                txtKDPENDAFTARAN.Focus()
                fn_Validate = False
                Exit Function
            End If
            If txtNAMAPASIEN.Text = String.Empty Then
                txtNAMAPASIEN.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                txtNAMAPASIEN.ErrorText = Statement.ErrorRequired

                txtNAMAPASIEN.Focus()
                fn_Validate = False
                Exit Function
            End If
            If txtNOREKAMMEDIS.Text = String.Empty Then
                txtNOREKAMMEDIS.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                txtNOREKAMMEDIS.ErrorText = Statement.ErrorRequired

                txtNOREKAMMEDIS.Focus()
                fn_Validate = False
                Exit Function
            End If
            If txtDIAGNOSA.Text = String.Empty Then
                txtDIAGNOSA.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                txtDIAGNOSA.ErrorText = Statement.ErrorRequired

                txtDIAGNOSA.Focus()
                fn_Validate = False
                Exit Function
            End If
            If txtRUANGRAWAT.Text = String.Empty Then
                txtRUANGRAWAT.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                txtRUANGRAWAT.ErrorText = Statement.ErrorRequired

                txtRUANGRAWAT.Focus()
                fn_Validate = False
                Exit Function
            End If

            grvDetail.UpdateCurrentRow()

            If grvDetail.RowCount < 2 Then
                MsgBox(Statement.ErrorDetail, MsgBoxStyle.Exclamation, Me.Text)
                fn_Validate = False
                Exit Function
            End If
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Function
    Private Function fn_Save() As Boolean
        Try
            ' ***** HEADER *****
            Dim ds = oDigital_IGD_01_GERD.GetStructureHeader
            With ds
                Try
                    .DATECREATED = oDigital_IGD_01_GERD.GetData(sNoId).DATECREATED
                Catch oErr As Exception
                    .DATECREATED = Now
                End Try
                .DATEUPDATED = Now

                .KDGERD = sNoId
                .DATE = deDATE.DateTime
                .KDPENDAFTARAN = txtKDPENDAFTARAN.Text
                .NAMAPASIEN = txtNAMAPASIEN.Text.Trim.ToUpper
                .NOREKAMMEDIS = txtNOREKAMMEDIS.Text.Trim.ToUpper
                .RUANGRAWAT = txtRUANGRAWAT.Text.Trim.ToUpper
                .DIAGNOSA = txtDIAGNOSA.Text.Trim.ToUpper
                .HASIL = CInt(txtHASIL.Text)
                Try
                    .KDUSER = oDigital_IGD_01_GERD.GetData(sNoId).KDUSER
                Catch ex As Exception
                    .KDUSER = sUserID
                End Try
            End With

            ' ***** DETIL *****
            Dim arrDetail = oDigital_IGD_01_GERD.GetStructureDetailList
            For i As Integer = 0 To grvDetail.RowCount - 2
                Dim dsDetail = oDigital_IGD_01_GERD.GetStructureDetail
                With dsDetail
                    .SEQ = i
                    .KDGERD = ds.KDGERD
                    .PERTANYAAN = IIf(String.IsNullOrEmpty(grvDetail.GetRowCellValue(i, colPERTANYAAN)), "", grvDetail.GetRowCellValue(i, colPERTANYAAN))
                    .FREKUENSI_1 = CInt(grvDetail.GetRowCellValue(i, colFREKUENSI_1))
                    .FREKUENSI_2 = CInt(grvDetail.GetRowCellValue(i, colFREKUENSI_2))
                    .FREKUENSI_3 = CInt(grvDetail.GetRowCellValue(i, colFREKUENSI_3))
                    .FREKUENSI_4 = CInt(grvDetail.GetRowCellValue(i, colFREKUENSI_4))
                    .FREKUENSI_SKOR = CInt(grvDetail.GetRowCellValue(i, colFREKUENSI_SKOR))
                    .REMARSK = IIf(String.IsNullOrEmpty(grvDetail.GetRowCellValue(i, colREMARKS)), "-", grvDetail.GetRowCellValue(i, colREMARKS))
                End With
                arrDetail.Add(dsDetail)
            Next

            If oFormMode = FORM_MODE.FORM_MODE_ADD Then
                Try
                    fn_Save = oDigital_IGD_01_GERD.InsertData(ds, arrDetail)
                Catch oErr As Exception
                    MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            ElseIf oFormMode = FORM_MODE.FORM_MODE_EDIT Then
                Try
                    fn_Save = oDigital_IGD_01_GERD.UpdateData(ds, arrDetail)
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
    Private Sub OnValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles grvDetail.FocusedRowChanged
        If isLoad Then
            Calculate()
        End If
    End Sub
    Private Sub Calculate()
        Dim sSubTotal As Integer = 0
        For i As Integer = 0 To grvDetail.RowCount - 2
            sSubTotal += CDec(grvDetail.GetRowCellValue(i, colFREKUENSI_SKOR))
        Next

        txtHASIL.Text = sSubTotal

    End Sub
    'Private Sub grvDetail_CellValueChanged(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs) Handles grvDetail.CellValueChanged
    '    If e.Column.Name = colPERTANYAAN.Name Then
    '        Dim oItem As New Reference.clsItem
    '        Try
    '            If grvDetail.GetFocusedRowCellValue(colKDITEM) IsNot Nothing Then
    '                Dim ds = oItem.GetDataDetail_UOM(grvDetail.GetFocusedRowCellValue(colKDITEM))

    '                If ds IsNot Nothing Then
    '                    grvDetail.SetFocusedRowCellValue(colKDUOM, ds.FirstOrDefault(Function(x) x.RATE = 1).KDUOM)
    '                Else
    '                    MsgBox(Statement.ErrorUOM, MsgBoxStyle.Exclamation, Me.Text)

    '                    grvDetail.CancelUpdateCurrentRow()
    '                End If
    '            End If
    '        Catch oErr As Exception
    '            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
    '        End Try
    '    ElseIf e.Column.Name = colKDUOM.Name Then
    '        Dim oItem As New Reference.clsItem
    '        Try
    '            If grvDetail.GetFocusedRowCellValue(colKDITEM) IsNot Nothing And grvDetail.GetFocusedRowCellValue(colKDUOM) IsNot Nothing Then
    '                Dim ds = oItem.GetDataDetail_UOM(grvDetail.GetFocusedRowCellValue(colKDITEM), grvDetail.GetFocusedRowCellValue(colKDUOM))

    '                If ds IsNot Nothing Then

    '                Else
    '                    MsgBox(Statement.ErrorUOM, MsgBoxStyle.Exclamation, Me.Text)

    '                    Dim sItem = grvDetail.GetFocusedRowCellValue(colKDITEM)
    '                    grvDetail.CancelUpdateCurrentRow()

    '                    grvDetail.AddNewRow()
    '                    grvDetail.SetFocusedRowCellValue(colKDITEM, sItem)
    '                End If
    '            End If
    '        Catch oErr As Exception
    '            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
    '        End Try
    '    ElseIf e.Column.Name = colQTY.Name Then
    '        If CDec(grvDetail.GetFocusedRowCellValue(colQTY)) = 0 Then
    '            grvDetail.SetFocusedRowCellValue(colQTY, 1)
    '        End If
    '    End If
    'End Sub
    Private Sub DeleteToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DeleteToolStripMenuItem.Click
        If oFormMode = FORM_MODE.FORM_MODE_VIEW Then Exit Sub
        grvDetail.DeleteSelectedRows()
    End Sub
#End Region
#Region "Command Button"
    Private Sub frmDigital_IGD_01_GERD_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
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
#End Region
#Region "Lookup / Event"

#End Region
End Class