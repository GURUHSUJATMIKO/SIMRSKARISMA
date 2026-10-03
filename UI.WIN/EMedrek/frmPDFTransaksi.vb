Imports DataAccess
Imports UI.WIN.MAIN.My.Resources
Imports System.Linq
Imports System.Data.SqlClient

Public Class frmPDFTransaksi
#Region "Declaration"
    Private oFormMode As FORM_MODE = FORM_MODE.FORM_MODE_VIEW
    Private sNoId As String
    Private isLoad As Boolean = False
    Private oPDFTransaksi As New Sales.clsPDFTransaksi
    Private sRegister As String = String.Empty
    Private sRekamMedis As String = String.Empty

#End Region
#Region "Function"
    Public Sub fn_LoadMe(ByVal Register As String, ByVal RekaMedis As String)
        sRegister = Register
        sRekamMedis = RekaMedis
    End Sub
    Public Sub LoadMe(ByVal FormMode As Integer, Optional ByVal NoId As String = "")
        oFormMode = FormMode
        sNoId = NoId
    End Sub
    Private Sub Form_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        fn_ChangeFormState()
        isLoad = True
    End Sub
    Private Sub Form_Closing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        sCode = txtCODE.Text.Trim.ToUpper
    End Sub
    Private Overloads Sub Dispose()
        MyBase.Dispose()
        Me.Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
    Private Sub fn_ChangeFormState()
        fn_LoadPDF()

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

        grvDetail.OptionsBehavior.ReadOnly = Status
    End Sub
    Private Sub fn_EmptyMe()
        txtCODE.Text = "<--- AUTO --->"
        txtKDREG.ResetText()
        deDATE.DateTime = Now
        txtDESCRIPTION.ResetText()
        txtKDREG.Text = sRegister
    End Sub
    Private Sub fn_LoadData()
        Try
            ' ***** HEADER *****
            Dim ds = oPDFTransaksi.GetData(sNoId)

            With ds
                txtCODE.Text = .KDPDFTRANSAKSI
                txtKDREG.Text = .KDREG
                deDATE.DateTime = .DATE
                txtDESCRIPTION.Text = .DESCRIPTION

                bindingSource.DataSource = oPDFTransaksi.GetDataDetail(sNoId).OrderBy(Function(x) x.SEQ).ToList()
                grdDetail.DataSource = bindingSource
            End With
        Catch oErr As Exception
            MsgBox("Load List Data : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Function fn_Validate() As Boolean
        Try
            fn_Validate = True
            If txtKDREG.Text = String.Empty Then
                MsgBox("Dibutuhkan Kode Register", MsgBoxStyle.Exclamation, Me.Text)
                txtKDREG.Focus()
                fn_Validate = False
                Exit Function
            End If

            grvDetail.UpdateCurrentRow()

            If grvDetail.RowCount < 2 Then
                MsgBox("Dibutuhkan Detil", MsgBoxStyle.Exclamation, Me.Text)
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
            Dim ds = oPDFTransaksi.GetStructureHeader
            With ds
                Try
                    .DATECREATED = oPDFTransaksi.GetData(sNoId).DATE
                Catch ex As Exception
                    .DATECREATED = Now
                End Try
                .DATEUPDATED = Now
                .KDPDFTRANSAKSI = sNoId
                .KDREG = txtKDREG.Text
                .DATE = deDATE.DateTime
                .DESCRIPTION = txtDESCRIPTION.Text.ToString.Trim
                .NOIDUSER = sUserID
            End With

            ' ***** DETIL *****
            Dim arrDetail = oPDFTransaksi.GetStructureDetailList
            For i As Integer = 0 To grvDetail.RowCount - 2
                Dim dsDetail = oPDFTransaksi.GetStructureDetail
                With dsDetail
                    .SEQ = i
                    .KDPDFTRANSAKSI = ds.KDPDFTRANSAKSI
                    .KDPDF = grvDetail.GetRowCellValue(i, colKDPDF)
                    .TYPE = grvDetail.GetRowCellValue(i, colTYPE)
                    .ALAMATAWAL_PDF = grvDetail.GetRowCellValue(i, colALAMATAWAL_PDF)
                    .ALAMATAKHIR_PDF = grvDetail.GetRowCellValue(i, colALAMATAKHIR_PDF)
                    .MEMO = grvDetail.GetRowCellValue(i, colMEMO).ToString.Trim.ToUpper
                End With
                arrDetail.Add(dsDetail)
            Next

            If oFormMode = FORM_MODE.FORM_MODE_ADD Then
                Try
                    fn_Save = oPDFTransaksi.InsertData(ds, arrDetail, sRekamMedis)
                Catch ex As Exception
                    MsgBox("Simpan Data : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            ElseIf oFormMode = FORM_MODE.FORM_MODE_EDIT Then
                Try
                    fn_Save = oPDFTransaksi.UpdateData(ds, arrDetail, sRekamMedis)
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
#Region "Grid Method"
    Private Sub grvDetail_CellValueChanged(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs) Handles grvDetail.CellValueChanged
        If e.Column.Name = colKDPDF.Name Then
            Try
                If grvDetail.GetFocusedRowCellValue(colKDPDF) IsNot Nothing Then
                    Dim Alamat As String = fn_LoadFile()
                    grvDetail.SetFocusedRowCellValue(colALAMATAWAL_PDF, Alamat)
                    Dim fileName As String = Alamat
                    Dim fi As New IO.FileInfo(fileName)
                    Dim extn As String = fi.Extension
                    grvDetail.SetFocusedRowCellValue(colTYPE, extn)
                    grvDetail.SetFocusedRowCellValue(colALAMATAKHIR_PDF, "")
                    grvDetail.SetFocusedRowCellValue(colMEMO, "")
                End If
            Catch oErr As Exception
                MsgBox("Load Detail : " & vbCrLf & oErr.Message, MsgBoxStyle.Critical, Me.Text)
            End Try
        End If
    End Sub
    Private Sub DeleteToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DeleteToolStripMenuItem.Click
        If oFormMode = FORM_MODE.FORM_MODE_VIEW Then Exit Sub

        'If grvDetail.GetFocusedRowCellValue(colALAMATAKHIR_PDF) IsNot Nothing Then
        '    If System.IO.File.Exists(xloop) = True Then
        '        My.Computer.FileSystem.DeleteFile(xloop, Microsoft.VisualBasic.FileIO.UIOption.AllDialogs, Microsoft.VisualBasic.FileIO.RecycleOption.DeletePermanently, Microsoft.VisualBasic.FileIO.UICancelOption.DoNothing)
        '    End If
        'End If

        'grvDetail.DeleteSelectedRows()
    End Sub
    Private Function fn_LoadFile() As String
        fn_LoadFile = ""

        Dim arrfname As New List(Of String)()
        Dim LastDir As String = String.Empty
        Dim sCountarrname = arrfname.Count
        arrfname.RemoveRange(0, sCountarrname)

        If LastDir = "" Then Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory)
        Dim fBrowse As New OpenFileDialog
        With fBrowse
            '"xls files(*.xls)|*.xls|pdf files(*.pdf)|*.pdf|doc files(*.doc)|*.doc|image files(*.jpg) |*.jpg|all files(*.*)|*.xls;*.pdf;*.doc;*.jpg"
            .Filter = "pdf files(*.pdf)|*.pdf|image files(*.jpg)|*.jpg|image files(*.png)|*.png|all files(*.*)|*.pdf;*.jpg;*.png"
            .FilterIndex = 1
            .Title = "Import data from pdf file"
            .InitialDirectory = LastDir ' Directory appear when you open your dialog.
            .Multiselect = True 'allow user to select multiple items (files)
        End With
        If fBrowse.ShowDialog() = Windows.Forms.DialogResult.OK Then
            fn_LoadFile = fBrowse.FileName
        End If
    End Function
#End Region
#Region "Command Button"
    Private Sub frm_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        Select Case e.KeyCode
            Case Keys.F12
                btnClose_Click()
            Case Keys.F9
                btnFocus_Click()
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
        If MsgBox("Save " & txtCODE.Text.Trim.ToUpper & "?", MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, Me.Text) = MsgBoxResult.No Then Exit Sub
        If fn_Save() = False Then
            MsgBox("Save gagal! Tolong kontak admin anda!", MsgBoxStyle.Exclamation, Me.Text)
        Else
            MsgBox("Save " & txtCODE.Text.Trim.ToUpper & " success!", MsgBoxStyle.Information, Me.Text)
            sStatusSave = "NEW"
            Me.Close()
        End If
    End Sub
    Private Sub btnSaveClose_Click() Handles btnSaveClose.ItemClick
        If fn_Validate() = False Then Exit Sub
        If MsgBox("Save " & txtCODE.Text.Trim.ToUpper & "?", MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, Me.Text) = MsgBoxResult.No Then Exit Sub
        If fn_Save() = False Then
            MsgBox("Save gagal! Tolong kontak admin anda!", MsgBoxStyle.Exclamation, Me.Text)
        Else
            MsgBox("Save " & txtCODE.Text.Trim.ToUpper & " success!", MsgBoxStyle.Information, Me.Text)
            Me.Close()
        End If
    End Sub
    Private Sub btnClose_Click() Handles btnClose.ItemClick
        Me.Close()
    End Sub
    Private Sub btnFocus_Click() Handles btnFocus.ItemClick
        grvDetail.Focus()
    End Sub
#End Region
#Region "Lookup / Event"
    Private Sub fn_LoadPDF()
        Try
            grdPDF.DataSource = oPDFTransaksi.GetDataPDF.Where(Function(x) x.ISACTIVE = True).ToList()
            grdPDF.ValueMember = "KDPDF"
            grdPDF.DisplayMember = "DESCRIPTION"
        Catch oErr As Exception
            MsgBox("Load PBF Data : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
#End Region
End Class