Imports DataAccess
Imports UI.WIN.MAIN.My.Resources
Imports System.Linq
Imports System.Data.SqlClient

Public Class frmMonitoringEvaluasiGizi
#Region "Declaration"
    Private oFormMode As FORM_MODE = FORM_MODE.FORM_MODE_VIEW
    Private isLoad As Boolean = False
    Private oS_DIGITAL_MONITORINGEVALUASIGIZI As New Digital.clsMonitoringEvaluasiGizi
    Private sNoId As String
    Private sUSERPERAWAT As String = String.Empty

#End Region
#Region "Function"
    Public Sub LoadMe(ByVal FormMode As Integer, ByVal KDREG As String, ByVal KDCUSTOMER As String, ByVal NAMAPASIEN As String, ByVal UMUR As String, ByVal KDUSER_PERAWAT As String, ByVal NoId As String)
        oFormMode = FormMode

        sNoId = NoId
        txtNOREG.Text = KDREG
        txtNAMA.Text = NAMAPASIEN
        txtNOMORRM.Text = KDCUSTOMER
        txtDIET.Text = UMUR
        sUSERPERAWAT = KDUSER_PERAWAT
    End Sub
    Private Sub Form_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        fn_ChangeFormState()
        isLoad = True
    End Sub
    Private Sub Form_Closing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        Dispose()
        sCode = txtKODE.Text.Trim.ToUpper
    End Sub
    Private Overloads Sub Dispose()
        MyBase.Dispose()
        Me.Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
    Private Sub fn_ChangeFormState()
        'fn_LoadAhliGizi()

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

        txtDIET.Properties.ReadOnly = Status
        txtDIAGNOSA.Properties.ReadOnly = Status
    End Sub
    Private Sub fn_EmptyMe()
        txtKODE.Text = "<--AUTO-->"
        txtDIET.ResetText()
        txtDIAGNOSA.ResetText()
        deDATE.DateTime = Now
    End Sub
    Private Sub fn_LoadData()
        Try
            ' ***** HEADER *****
            Dim ds = oS_DIGITAL_MONITORINGEVALUASIGIZI.GetData(sNoId)

            With ds
                txtKODE.Text = .KDMONEVG
                txtDIET.Text = .DIET
                txtDIAGNOSA.Text = .DIAGNOSA
                deDATE.DateTime = .DATE
                BindingSource1.DataSource = oS_DIGITAL_MONITORINGEVALUASIGIZI.GetDataDetail(sNoId)
                grdDetail.DataSource = BindingSource1
            End With
        Catch oErr As Exception
            MsgBox("Load List Data : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Function fn_Validate() As Boolean
        Try
            fn_Validate = True
            If txtDIET.Text = String.Empty Then
                MsgBox("Dibutuhkan data Diet", MsgBoxStyle.Exclamation, Me.Text)
                txtDIET.Focus()
                fn_Validate = False
                Exit Function
            End If

            If txtDIAGNOSA.Text = String.Empty Then
                MsgBox("Dibutuhkan Diagnosa", MsgBoxStyle.Exclamation, Me.Text)
                txtDIAGNOSA.Focus()
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
            Dim ds = oS_DIGITAL_MONITORINGEVALUASIGIZI.GetStructureHeader
            With ds
                .KDMONEVG = sNoId
                .KDREG = txtNOREG.Text
                .KDCUSTOMER = txtNOMORRM.Text
                Try
                    .DATECREATED = oS_DIGITAL_MONITORINGEVALUASIGIZI.GetData(txtKODE.Text).DATECREATED
                Catch ex As Exception
                    .DATECREATED = Now
                End Try
                .DATEUPDATED = Now
                .DATE = deDATE.DateTime
                
                .DIET = txtDIET.Text 
                .DIAGNOSA = txtDIAGNOSA.Text

                Try
                    .CETAK = oS_DIGITAL_MONITORINGEVALUASIGIZI.GetData(txtKODE.Text).CETAK
                Catch ex As Exception
                    .CETAK = 0
                End Try
            End With

            ' ***** DETIL *****
            Dim arrDetail = oS_DIGITAL_MONITORINGEVALUASIGIZI.GetStructureDetailList ''
            For i As Integer = 0 To grvDetail.RowCount - 2
                Dim dsDetail = oS_DIGITAL_MONITORINGEVALUASIGIZI.GetStructureDetail
                With dsDetail
                    .SEQ = i
                    .KDMONEVG = ds.KDMONEVG
                    .KDKUNJUNGAN = ds.KDREG
                    .DATE = CDate(grvDetail.GetRowCellValue(i, colTanggal))
                    .DIET = grvDetail.GetRowCellValue(i, colDietIntake)
                    .FISIKKLINIS = grvDetail.GetRowCellValue(i, colFisikKlinis)
                    .RENCANA = grvDetail.GetRowCellValue(i,colRencana)
                    .CATATAN = grvDetail.GetRowCellValue(i,colCatatan)
                    .KDUSER = sUserID
                    .KDUSER_SIGNATURE = sUSERPERAWAT
                End With
                arrDetail.Add(dsDetail)
            Next

            If oFormMode = FORM_MODE.FORM_MODE_ADD Then
                Try
                    fn_Save = oS_DIGITAL_MONITORINGEVALUASIGIZI.InsertData(ds,arrDetail)
                Catch ex As Exception
                    MsgBox("Simpan Data : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            ElseIf oFormMode = FORM_MODE.FORM_MODE_EDIT Then
                Try
                    fn_Save = oS_DIGITAL_MONITORINGEVALUASIGIZI.UpdateData(ds.KDMONEVG,ds,arrDetail)
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
        If MsgBox("Save " & txtKODE.Text.Trim.ToUpper & "?", MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, Me.Text) = MsgBoxResult.No Then Exit Sub
        If fn_Save() = False Then
            MsgBox("Save gagal! Tolong kontak admin anda!", MsgBoxStyle.Exclamation, Me.Text)
        Else
            MsgBox("Save " & txtKODE.Text.Trim.ToUpper & " success!", MsgBoxStyle.Information, Me.Text)
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