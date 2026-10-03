Imports DataAccess
Imports UI.WIN.MAIN.My.Resources
Imports System.Linq
Imports Newtonsoft.Json.Linq
Imports System.Data.SqlClient

Public Class frmAsesmenMedisRawatJalan
#Region "Declaration"
    Private oFormMode As FORM_MODE = FORM_MODE.FORM_MODE_VIEW
    Private oAsesmenMedisRawatJalan As New EMedrek.clsDigital_AsesmenMedisRawatJalan
    Private sNoid As String
    Private sKodeIdentitas As Integer
    Private sKDDOCTOR As String
    Private isLoad As Boolean = False
#End Region
#Region "Function"
    Public Sub LoadMe(ByVal FormMode As Integer, ByVal KDDOCTOR As String, ByVal KodeIdentitas As Integer, ByVal NoId As String)
        oFormMode = FormMode

        sKodeIdentitas = KodeIdentitas
        sNoId = NoId
        grdDOCTOR.Text = KDDOCTOR
    End Sub
    Private Sub Form_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        fn_ChangeFormState()
        fn_LoadLanguage()
        isLoad = True
    End Sub
    Public Sub fn_LoadLanguage()
        Try
            Me.Text = "ASESMEN AWAL MEDIS RAWAT JALAN"

            btnSaveClosee.Caption = "Simpan"
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub Form_Closing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        'sCode = txtMEMO.Text.Trim.ToUpper
    End Sub
    Private Sub fn_ChangeFormState()
        fn_LoadKDDOCTOR

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
        btnSaveClosee.Enabled = Not Status
        deDATE.Properties.ReadOnly = Status
        MemoEdit1.Properties.ReadOnly = Status
        MemoEdit2.Properties.ReadOnly = Status
        MemoEdit3.Properties.ReadOnly = Status
        MemoEdit4.Properties.ReadOnly = Status
        MemoEdit5.Properties.ReadOnly = Status
        MemoEdit6.Properties.ReadOnly = Status
        MemoEdit7.Properties.ReadOnly = Status
        MemoEdit8.Properties.ReadOnly = Status
        MemoEdit9.Properties.ReadOnly = Status
        MemoEdit10.Properties.ReadOnly = Status
        MemoEdit11.Properties.ReadOnly = Status
        MemoEdit12.Properties.ReadOnly = Status
        MemoEdit13.Properties.ReadOnly = Status
        MemoEdit14.Properties.ReadOnly = Status
        MemoEdit15.Properties.ReadOnly = Status
        MemoEdit16.Properties.ReadOnly = Status
        MemoEdit17.Properties.ReadOnly = Status
        MemoEdit18.Properties.ReadOnly = Status
        MemoEdit19.Properties.ReadOnly = Status
        MemoEdit20.Properties.ReadOnly = Status
        MemoEdit21.Properties.ReadOnly = Status
        MemoEdit22.Properties.ReadOnly = Status
        MemoEdit23.Properties.ReadOnly = Status
        MemoEdit24.Properties.ReadOnly = Status
        MemoEdit25.Properties.ReadOnly = Status
        MemoEdit26.Properties.ReadOnly = Status
        MemoEdit27.Properties.ReadOnly = Status
        MemoEdit28.Properties.ReadOnly = Status
        MemoEdit29.Properties.ReadOnly = Status
        MemoEdit30.Properties.ReadOnly = Status
        MemoEdit31.Properties.ReadOnly = Status
        MemoEdit32.Properties.ReadOnly = Status
        MemoEdit33.Properties.ReadOnly = Status
        MemoEdit34.Properties.ReadOnly = Status
        MemoEdit35.Properties.ReadOnly = Status
        MemoEdit36.Properties.ReadOnly = Status
        MemoEdit37.Properties.ReadOnly = Status
        MemoEdit38.Properties.ReadOnly = Status
        MemoEdit39.Properties.ReadOnly = Status
        MemoEdit40.Properties.ReadOnly = Status
        CheckEdit1.Properties.ReadOnly = Status
        CheckEdit2.Properties.ReadOnly = Status
        CheckEdit3.Properties.ReadOnly = Status
        CheckEdit4.Properties.ReadOnly = Status
        CheckEdit5.Properties.ReadOnly = Status
        CheckEdit6.Properties.ReadOnly = Status
        CheckEdit7.Properties.ReadOnly = Status
    End Sub
    Private Sub fn_EmptyMe()
        deDATE.DateTime = Now
        MemoEdit1.ResetText()
        MemoEdit2.ResetText()
        MemoEdit3.ResetText()
        MemoEdit4.ResetText()
        MemoEdit5.ResetText()
        MemoEdit6.ResetText()
        MemoEdit7.ResetText()
        MemoEdit8.ResetText()
        MemoEdit9.ResetText()
        MemoEdit10.ResetText()
        MemoEdit11.ResetText()
        MemoEdit12.ResetText()
        MemoEdit13.ResetText()
        MemoEdit14.ResetText()
        MemoEdit15.ResetText()
        MemoEdit16.ResetText()
        MemoEdit17.ResetText()
        MemoEdit18.ResetText()
        MemoEdit19.ResetText()
        MemoEdit20.ResetText()
        MemoEdit21.ResetText()
        MemoEdit22.ResetText()
        MemoEdit23.ResetText()
        MemoEdit24.ResetText()
        MemoEdit25.ResetText()
        MemoEdit26.ResetText()
        MemoEdit27.ResetText()
        MemoEdit28.ResetText()
        MemoEdit29.ResetText()
        MemoEdit30.ResetText()
        MemoEdit31.ResetText()
        MemoEdit32.ResetText()
        MemoEdit33.ResetText()
        MemoEdit34.ResetText()
        MemoEdit35.ResetText()
        MemoEdit36.ResetText()
        MemoEdit37.ResetText()
        MemoEdit38.ResetText()
        MemoEdit39.ResetText()
        MemoEdit40.ResetText()
        CheckEdit1.Checked = False
        CheckEdit2.Checked = False
        CheckEdit3.Checked = False
        CheckEdit4.Checked = False
        CheckEdit5.Checked = False
        CheckEdit6.Checked = False
        CheckEdit7.Checked = False
    End Sub
    Private Sub fn_LoadData()
        Try
            ' ***** HEADER *****
            Dim ds = oAsesmenMedisRawatJalan.GetData(sNoid)
            With ds
                deDATE.DateTime = .DATE
                MemoEdit1.Text = .MemoEdit1
                MemoEdit2.Text = .MemoEdit2
                MemoEdit3.Text = .MemoEdit3
                MemoEdit4.Text = .MemoEdit4
                MemoEdit5.Text = .MemoEdit5
                MemoEdit6.Text = .MemoEdit6
                MemoEdit7.Text = .MemoEdit7
                MemoEdit8.Text = .MemoEdit8
                MemoEdit9.Text = .MemoEdit9
                MemoEdit10.Text = .MemoEdit10
                MemoEdit11.Text = .MemoEdit11
                MemoEdit12.Text = .MemoEdit12
                MemoEdit13.Text = .MemoEdit13
                MemoEdit14.Text = .MemoEdit14
                MemoEdit15.Text = .MemoEdit15
                MemoEdit16.Text = .MemoEdit16
                MemoEdit17.Text = .MemoEdit17
                MemoEdit18.Text = .MemoEdit18
                MemoEdit19.Text = .MemoEdit19
                MemoEdit20.Text = .MemoEdit20
                MemoEdit21.Text = .MemoEdit21
                MemoEdit22.Text = .MemoEdit22
                MemoEdit23.Text = .MemoEdit23
                MemoEdit24.Text = .MemoEdit24
                MemoEdit25.Text = .MemoEdit25
                MemoEdit26.Text = .MemoEdit26
                MemoEdit27.Text = .MemoEdit27
                MemoEdit28.Text = .MemoEdit28
                MemoEdit29.Text = .MemoEdit29
                MemoEdit30.Text = .MemoEdit30
                MemoEdit31.Text = .MemoEdit31
                MemoEdit32.Text = .MemoEdit32
                MemoEdit33.Text = .MemoEdit33
                MemoEdit34.Text = .MemoEdit34
                MemoEdit35.Text = .MemoEdit35
                MemoEdit36.Text = .MemoEdit36
                MemoEdit37.Text = .MemoEdit37
                MemoEdit38.Text = .MemoEdit38
                MemoEdit39.Text = .MemoEdit39
                MemoEdit40.Text = .MemoEdit40
                CheckEdit1.Checked = .CheckEdit1
                CheckEdit2.Checked = .CheckEdit2
                CheckEdit3.Checked = .CheckEdit3
                CheckEdit4.Checked = .CheckEdit4
                CheckEdit5.Checked = .CheckEdit5
                CheckEdit6.Checked = .CheckEdit6
                CheckEdit7.Checked = .CheckEdit7
            End With
        Catch oErr As Exception
            MsgBox("Load List Data Laporan Operasi: " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Function fn_Validate() As Boolean
        fn_Validate = True

        If sNoid = String.Empty Then
            MsgBox("Dibutuhkan Register", MsgBoxStyle.Exclamation, Me.Text)
            deDATE.Focus()
            fn_Validate = False
            Exit Function
        End If
    End Function
    Private Function fn_Save() As Boolean
        Try
            ' ***** HEADER *****

            Dim ds = oAsesmenMedisRawatJalan.GetStructureHeader
            With ds
                .KDKUNJUNGAN = sNoid
                .KDIDENTITAS = sKodeIdentitas
                .DATECREATED = Now
                .DATEUPDATED = Now
                .DATE = deDATE.DateTime
                .MemoEdit1 = MemoEdit1.Text.Trim
                .MemoEdit2 = MemoEdit2.Text.Trim
                .MemoEdit3 = MemoEdit3.Text.Trim
                .MemoEdit4 = MemoEdit4.Text.Trim
                .MemoEdit5 = MemoEdit5.Text.Trim
                .MemoEdit6 = MemoEdit6.Text.Trim
                .MemoEdit7 = MemoEdit7.Text.Trim
                .MemoEdit8 = MemoEdit8.Text.Trim
                .MemoEdit9 = MemoEdit9.Text.Trim
                .MemoEdit10 = MemoEdit10.Text.Trim
                .MemoEdit11 = MemoEdit11.Text.Trim
                .MemoEdit12 = MemoEdit12.Text.Trim
                .MemoEdit13 = MemoEdit13.Text.Trim
                .MemoEdit14 = MemoEdit14.Text.Trim
                .MemoEdit15 = MemoEdit15.Text.Trim
                .MemoEdit16 = MemoEdit16.Text.Trim
                .MemoEdit17 = MemoEdit17.Text.Trim
                .MemoEdit18 = MemoEdit18.Text.Trim
                .MemoEdit19 = MemoEdit19.Text.Trim
                .MemoEdit20 = MemoEdit20.Text.Trim
                .MemoEdit21 = MemoEdit21.Text.Trim
                .MemoEdit22 = MemoEdit22.Text.Trim
                .MemoEdit23 = MemoEdit23.Text.Trim
                .MemoEdit24 = MemoEdit24.Text.Trim
                .MemoEdit25 = MemoEdit25.Text.Trim
                .MemoEdit26 = MemoEdit26.Text.Trim
                .MemoEdit27 = MemoEdit27.Text.Trim
                .MemoEdit28 = MemoEdit28.Text.Trim
                .MemoEdit29 = MemoEdit29.Text.Trim
                .MemoEdit30 = MemoEdit30.Text.Trim
                .MemoEdit31 = MemoEdit31.Text.Trim
                .MemoEdit32 = MemoEdit32.Text.Trim
                .MemoEdit33 = MemoEdit33.Text.Trim
                .MemoEdit34 = MemoEdit34.Text.Trim
                .MemoEdit35 = MemoEdit35.Text.Trim
                .MemoEdit36 = MemoEdit36.Text.Trim
                .MemoEdit37 = MemoEdit37.Text.Trim
                .MemoEdit38 = MemoEdit38.Text.Trim
                .MemoEdit39 = MemoEdit39.Text.Trim
                .MemoEdit40 = MemoEdit40.Text.Trim
                .CheckEdit1 = CheckEdit1.Checked
                .CheckEdit2 = CheckEdit2.Checked
                .CheckEdit3 = CheckEdit3.Checked
                .CheckEdit4 = CheckEdit4.Checked
                .CheckEdit5 = CheckEdit5.Checked
                .CheckEdit6 = CheckEdit6.Checked
                .CheckEdit7 = CheckEdit7.Checked
                .KDUSER = sUserID
            End With

            If oFormMode = FORM_MODE.FORM_MODE_ADD Then
                Try
                    fn_Save = oAsesmenMedisRawatJalan.InsertData(ds)
                Catch ex As Exception
                    MsgBox("Simpan Data Laporan Tindakan: " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            Else
                Try
                    fn_Save = oAsesmenMedisRawatJalan.UpdateData(ds)
                Catch ex As Exception
                    MsgBox("Simpan Data Laporan Tindakan: " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            End If
        Catch oErr As Exception
            MsgBox("Simpan Data Laporan Tindakan: " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            fn_Save = False
        End Try
    End Function

#End Region
#Region "Command Button"
    Private Sub btnSaveClosee_Click() Handles btnSaveClosee.ItemClick
        If fn_Validate() = False Then Exit Sub
        If MsgBox(Statement.SaveQuestion, MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, Me.Text) = MsgBoxResult.No Then Exit Sub
        If fn_Save() = False Then
            MsgBox(Statement.SaveFail, MsgBoxStyle.Exclamation, Me.Text)
        Else
            oFormMode = FORM_MODE.FORM_MODE_EDIT
            MsgBox(Statement.SaveSuccess, MsgBoxStyle.Information, Me.Text)
        End If
    End Sub
    Private Sub frmItem_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        Select Case e.KeyCode
            Case Keys.PageUp
                fn_ScrollPage(True)
            Case Keys.PageDown
                fn_ScrollPage(False)
        End Select
    End Sub
    Private Sub frmAsesmenMedisRawatJalan_MouseWheel(sender As Object, e As MouseEventArgs) Handles MyBase.MouseWheel
        If e.Delta > 0 Then
            'up
            fn_ScrollPage(True)
        Else
            'down
            fn_ScrollPage(False)
        End If
    End Sub
    Private Sub fn_ScrollPage(ByVal isUp As Boolean)
        Dim myView As Point = Me.Panel2.AutoScrollPosition
        Dim scrollchange As Integer = 50

        If isUp Then
            'up
            myView.X = -myView.X
            myView.Y = -scrollchange - myView.Y
        Else
            'down
            myView.X = -myView.X
            myView.Y = scrollchange - myView.Y
        End If

        Me.Panel2.AutoScrollPosition = myView
    End Sub
#End Region
#Region "Lookup / Event"
    Private Sub fn_LoadKDDOCTOR()
        Dim oDPJP As New Reference.clsDoctor
        Try
            grdDOCTOR.Properties.DataSource = oDPJP.GetData.Where(Function(x) x.ISACTIVE = True).ToList()
            grdDOCTOR.Properties.ValueMember = "KDDOCTOR"
            grdDOCTOR.Properties.DisplayMember = "NAME_DISPLAY"

        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
#End Region
End Class