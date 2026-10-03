Imports DataAccess
Imports UI.WIN.MAIN.My.Resources
Imports System.Linq
Imports System.Data.SqlClient

Public Class frmDigital_MPP
#Region "Declaration"
    Private oFormMode As FORM_MODE = FORM_MODE.FORM_MODE_VIEW
    Private sNoid As String
    Private sRM As String
    Private sNAMA As String
    Private sJENISKELAMIN As String
    Private sTANGGALLAHIR As DateTime
    Private isLoad As Boolean = False
    Private oDigital_MPP As New Digital.clsDigital_MPP
#End Region
#Region "Function"
    Public Sub LoadMe(ByVal FormMode As Integer, ByVal RM As String, ByVal NAMA As String, ByVal JK As String, ByVal TANGGALLAHIR As DateTime, ByVal NoId As String)
        oFormMode = FormMode
        sNoid = NoId
        sRM = RM
        sNAMA = NAMA
        sJENISKELAMIN = JK
        sTANGGALLAHIR = TANGGALLAHIR
    End Sub
    Private Sub Form_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        fn_ChangeFormState()
        fn_LoadLanguage()
        isLoad = True
    End Sub
    Public Sub fn_LoadLanguage()
        Try
            Me.Text = "Formulir MPP"

            btnSaveNew.Caption = Caption.FormSaveNew
            btnSaveClose.Caption = Caption.FormSaveClose
            btnClose.Caption = Caption.FormClose
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub Form_Closing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        'sCode = txtKDDigital_MPP.Text.Trim.ToUpper
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
        grvDetail.OptionsBehavior.ReadOnly = Status
    End Sub
    Private Sub fn_EmptyMe()
        deDATE.DateTime = Now
        'grvDetail.OptionsSelection.MultiSelect = True
        'grvDetail.SelectAll()
        'grvDetail.DeleteSelectedRows()
        'grvDetail.OptionsSelection.MultiSelect = False
        fn_LoadKDITEM()
    End Sub
    Private Sub fn_LoadData()
        Try
            ' ***** HEADER *****
            Dim ds = oDigital_MPP.GetData(sNoId)

            With ds
                bindingSource.DataSource = oDigital_MPP.GetDataDetail(sNoid).OrderBy(Function(x) x.SEQ).ToList()
                grdDetail.DataSource = bindingSource
            End With
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Function fn_Validate() As Boolean
        Try
            fn_Validate = True

            If sNoid = String.Empty Then
                deDATE.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                deDATE.ErrorText = Statement.ErrorRequired

                deDATE.Focus()
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
            Dim ds = oDigital_MPP.GetStructureHeader
            With ds
                .KDPENDAFTARAN = sNoid
                .KDCUSTOMER = sRM
                .NAMAPASIEN = sNAMA
                .JENISKELAMIN = sJENISKELAMIN
                .TANGGALLAHIR = sTANGGALLAHIR
                .KDUSER = sUserID
                .DATECREATED = Now
                Try
                    .DATEUPDATED = oDigital_MPP.GetData(sNoid).DATEUPDATED
                Catch ex As Exception
                    .DATEUPDATED = Now
                End Try
                .DATE = deDATE.DateTime
            End With

            ' ***** DETIL *****
            Dim arrDetail = oDigital_MPP.GetStructureDetailList
            For i As Integer = 0 To grvDetail.RowCount - 2
                Dim dsDetail = oDigital_MPP.GetStructureDetail
                With dsDetail
                    .KDPENDAFTARAN = ds.KDPENDAFTARAN
                    .SEQ = i
                    .HEADER_NO = grvDetail.GetRowCellValue(i, colHEADER_NO)
                    .HEADER = grvDetail.GetRowCellValue(i, colHEADER)
                    .MEMO = IIf(String.IsNullOrEmpty(grvDetail.GetRowCellValue(i, colMEMO)), "", grvDetail.GetRowCellValue(i, colMEMO))
                    .CHEKLIS = IIf(String.IsNullOrEmpty(grvDetail.GetRowCellValue(i, colCHEKLIS)), False, grvDetail.GetRowCellValue(i, colCHEKLIS))
                End With
                arrDetail.Add(dsDetail)
            Next

            If oFormMode = FORM_MODE.FORM_MODE_ADD Then
                Try
                    fn_Save = oDigital_MPP.InsertData(ds, arrDetail)
                Catch oErr As Exception
                    MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            ElseIf oFormMode = FORM_MODE.FORM_MODE_EDIT Then
                Try
                    fn_Save = oDigital_MPP.UpdateData(ds, arrDetail)
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
    Private Sub DeleteToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DeleteToolStripMenuItem.Click
        If oFormMode = FORM_MODE.FORM_MODE_VIEW Then Exit Sub
        grvDetail.DeleteSelectedRows()
    End Sub
#End Region
#Region "Command Button"
    Private Sub frmDigital_MPP_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
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
    'Private Sub fn_LoadKDWAREHOUSE()
    '    Dim oWAREHOUSE As New Reference.clsWarehouse
    '    Try
    '        grdKDWAREHOUSE.Properties.DataSource = oWAREHOUSE.GetData.Where(Function(x) x.ISACTIVE = True).ToList()
    '        grdKDWAREHOUSE.Properties.ValueMember = "KDWAREHOUSE"
    '        grdKDWAREHOUSE.Properties.DisplayMember = "NAME_DISPLAY"
    '    Catch oErr As Exception
    '        MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
    '    End Try
    'End Sub
    'Private Sub grdKDWAREHOUSE_KeyDown(sender As System.Object, e As System.Windows.Forms.KeyEventArgs)
    '    If e.KeyCode = Keys.Delete Then
    '        grdKDWAREHOUSE.ResetText()
    '    End If
    'End Sub
    Private Sub fn_LoadKDITEM()
        Try
            Dim oConn As New SqlConnection
            Dim oComm As New SqlCommand
            Dim da As SqlDataAdapter
            Dim ds As New DataSet
            Dim SQL As String

            Dim sConn As String = Decrypt(My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Software\SIMRS\SW\", "Database", "").ToString())

            oConn = New SqlConnection(sConn)
            If oConn.State = ConnectionState.Closed Then
                oConn.Open()
            End If

            SQL = "SELECT "
            SQL &= "A.* "
            SQL &= "FROM "
            SQL &= "S_DIGITAL_MPP_D_AMBILDATA A "
            SQL &= "ORDER BY A.SEQ "

            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "S_DIGITAL_MPP_D_AMBILDATA")

            For i As Integer = 0 To ds.Tables("S_DIGITAL_MPP_D_AMBILDATA").Rows.Count - 1
                grvDetail.Focus()
                grvDetail.AddNewRow()
                grvDetail.SetFocusedRowCellValue(colHEADER_NO, ds.Tables("S_DIGITAL_MPP_D_AMBILDATA").Rows(i)("HEADER_NO").ToString())
                grvDetail.SetFocusedRowCellValue(colHEADER, ds.Tables("S_DIGITAL_MPP_D_AMBILDATA").Rows(i)("HEADER").ToString())
                grvDetail.SetFocusedRowCellValue(colMEMO, ds.Tables("S_DIGITAL_MPP_D_AMBILDATA").Rows(i)("MEMO").ToString())
                grvDetail.SetFocusedRowCellValue(colCHEKLIS, ds.Tables("S_DIGITAL_MPP_D_AMBILDATA").Rows(i)("CHEKLIS").ToString())
                grvDetail.UpdateCurrentRow()
            Next

            If oConn.State = ConnectionState.Open Then
                oConn.Close()
            End If

        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    'Private Sub fn_LoadKDUOM()
    '    Dim oUOM As New Reference.clsUOM
    '    Try
    '        grdKDUOM.DataSource = oUOM.GetData.Where(Function(x) x.ISACTIVE = True).ToList()
    '        grdKDUOM.ValueMember = "KDUOM"
    '        grdKDUOM.DisplayMember = "MEMO"
    '    Catch oErr As Exception
    '        MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
    '    End Try
    'End Sub
    'Private Sub grdKDWAREHOUSE_EditValueChanged(sender As Object, e As EventArgs)
    '    fn_LoadKDITEM()
    'End Sub
#End Region
End Class