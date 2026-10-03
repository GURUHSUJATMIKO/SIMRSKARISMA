Imports DataAccess
Imports UI.WIN.MAIN.My.Resources
Imports System.Linq
Imports System.Data.SqlClient

Public Class frmDigital_MPPA
#Region "Declaration"
    Private oFormMode As FORM_MODE = FORM_MODE.FORM_MODE_VIEW
    Private sNoid As String
    Private sRM As String
    Private sNAMA As String
    Private sJENISKELAMIN As String
    Private sTANGGALLAHIR As DateTime
    Private sRegister As String
    Private sRuangan As String
    Private isLoad As Boolean = False
    Private oDigital_MPP As New Digital.clsDigital_MPPA
#End Region
#Region "Function"
    Private Sub Form1_MouseWheel(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Me.MouseWheel
        If e.Delta > 0 Then
            Trace.WriteLine("Scrolled up!")
            fn_ScrollPage(True)
        Else
            Trace.WriteLine("Scrolled down!")
            fn_ScrollPage(False)
        End If
    End Sub
    Private Sub fn_ScrollPage(ByVal isUp As Boolean)
        Dim myView As Point = Me.Panel1.AutoScrollPosition

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

        Me.Panel1.AutoScrollPosition = myView
    End Sub
    Public Sub LoadMe(ByVal FormMode As Integer, ByVal Register As String, ByVal Ruangan As String, ByVal RM As String, ByVal NAMA As String, ByVal JK As String, ByVal TANGGALLAHIR As DateTime, ByVal NoId As String)
        oFormMode = FormMode
        sNoid = NoId
        sRM = RM
        sNAMA = NAMA
        sJENISKELAMIN = JK
        sTANGGALLAHIR = TANGGALLAHIR
        sRegister = Register
        sRuangan = Ruangan
        txtNAMAPASIEN.Text = NAMA
        txtNOREKAMMEDIS.Text = RM
    End Sub
    Private Sub Form_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        fn_ChangeFormState()
        fn_LoadLanguage()
        isLoad = True
    End Sub
    Public Sub fn_LoadLanguage()
        Try
            Me.Text = "Form A – Evaluasi Awal Manajer Pelayanan Pasien"

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
        fn_LoadDokter
        fn_LoadKDUSER()

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

        LoadDaftarg(sRegister)

    End Sub
    Private Sub LoadDaftarg(ByVal kdreg As String)
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
            SQL &= "A.DATE  "
            SQL &= "FROM "
            SQL &= "S_PENDAFTARAN_H A "
            SQL &= "WHERE "
            SQL &= "A.KDREG = '" & kdreg & "' "

            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "S_PENDAFTARAN_H")

            For iLoop As Integer = 0 To ds.Tables("S_PENDAFTARAN_H").Rows.Count - 1
                With ds.Tables("S_PENDAFTARAN_H")
                    deTGLMASUK.DateTime = .Rows(iLoop)("DATE")
                End With
            Next

            If oConn.State = ConnectionState.Open Then
                oConn.Close()
            End If

        Catch oErr As Exception
            MsgBox("Load Rincian: " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_ViewMode(ByVal Status As Boolean)
        btnSaveNew.Enabled = Not Status
        btnSaveClose.Enabled = Not Status

        deDATE.Properties.ReadOnly = Status
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
        CheckEdit35.Properties.ReadOnly = Status
        CheckEdit36.Properties.ReadOnly = Status
        CheckEdit37.Properties.ReadOnly = Status
        CheckEdit38.Properties.ReadOnly = Status
        CheckEdit39.Properties.ReadOnly = Status
        CheckEdit40.Properties.ReadOnly = Status
        CheckEdit41.Properties.ReadOnly = Status
        CheckEdit42.Properties.ReadOnly = Status
        CheckEdit43.Properties.ReadOnly = Status
        CheckEdit44.Properties.ReadOnly = Status
        CheckEdit45.Properties.ReadOnly = Status
        CheckEdit46.Properties.ReadOnly = Status
        CheckEdit47.Properties.ReadOnly = Status
        CheckEdit48.Properties.ReadOnly = Status
        CheckEdit49.Properties.ReadOnly = Status
        CheckEdit50.Properties.ReadOnly = Status
        CheckEdit51.Properties.ReadOnly = Status
        CheckEdit52.Properties.ReadOnly = Status
        CheckEdit53.Properties.ReadOnly = Status
        CheckEdit54.Properties.ReadOnly = Status
        CheckEdit55.Properties.ReadOnly = Status
        CheckEdit56.Properties.ReadOnly = Status
        CheckEdit57.Properties.ReadOnly = Status
        CheckEdit58.Properties.ReadOnly = Status
        CheckEdit59.Properties.ReadOnly = Status
        CheckEdit60.Properties.ReadOnly = Status
        CheckEdit61.Properties.ReadOnly = Status
        CheckEdit62.Properties.ReadOnly = Status
        CheckEdit63.Properties.ReadOnly = Status
        CheckEdit64.Properties.ReadOnly = Status

        TextEdit1.Properties.ReadOnly = Status
        TextEdit2.Properties.ReadOnly = Status
        grdDPJPUtama.Properties.ReadOnly = Status
        grdUSERHADNOVER.Properties.ReadOnly = Status
        MemoEdit1.Properties.ReadOnly = Status
    End Sub
    Private Sub fn_EmptyMe()
        deDATE.DateTime = Now
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
        CheckEdit35.Checked = False
        CheckEdit36.Checked = False
        CheckEdit37.Checked = False
        CheckEdit38.Checked = False
        CheckEdit39.Checked = False
        CheckEdit40.Checked = False
        CheckEdit41.Checked = False
        CheckEdit42.Checked = False
        CheckEdit43.Checked = False
        CheckEdit44.Checked = False
        CheckEdit45.Checked = False
        CheckEdit46.Checked = False
        CheckEdit47.Checked = False
        CheckEdit48.Checked = False
        CheckEdit49.Checked = False
        CheckEdit50.Checked = False
        CheckEdit51.Checked = False
        CheckEdit52.Checked = False
        CheckEdit53.Checked = False
        CheckEdit54.Checked = False
        CheckEdit55.Checked = False
        CheckEdit56.Checked = False
        CheckEdit57.Checked = False
        CheckEdit58.Checked = False
        CheckEdit59.Checked = False
        CheckEdit60.Checked = False
        CheckEdit61.Checked = False
        CheckEdit62.Checked = False
        CheckEdit63.Checked = False
        CheckEdit64.Checked = False
        TextEdit1.ResetText()
        TextEdit2.ResetText()
        MemoEdit1.ResetText()
        grdDPJPUtama.ResetText()
        grdUSERHADNOVER.resetText()
    End Sub
    Private Sub fn_LoadData()
        Try
            ' ***** HEADER *****
            Dim ds = oDigital_MPP.GetData(sNoid)

            With ds
                deDATE.DateTime = .DATE
                CheckEdit1.Checked = .CHEKLIS_1
                CheckEdit2.Checked = .CHEKLIS_2
                CheckEdit3.Checked = .CHEKLIS_3
                CheckEdit4.Checked = .CHEKLIS_4
                CheckEdit5.Checked = .CHEKLIS_5
                CheckEdit6.Checked = .CHEKLIS_6
                CheckEdit7.Checked = .CHEKLIS_7
                CheckEdit8.Checked = .CHEKLIS_8
                CheckEdit9.Checked = .CHEKLIS_9
                CheckEdit10.Checked = .CHEKLIS_10
                CheckEdit11.Checked = .CHEKLIS_11
                CheckEdit12.Checked = .CHEKLIS_12
                CheckEdit13.Checked = .CHEKLIS_13
                CheckEdit14.Checked = .CHEKLIS_14
                CheckEdit15.Checked = .CHEKLIS_15
                CheckEdit16.Checked = .CHEKLIS_16
                CheckEdit17.Checked = .CHEKLIS_17
                CheckEdit18.Checked = .CHEKLIS_18
                CheckEdit19.Checked = .CHEKLIS_19
                CheckEdit20.Checked = .CHEKLIS_20
                CheckEdit21.Checked = .CHEKLIS_21
                CheckEdit22.Checked = .CHEKLIS_22
                CheckEdit23.Checked = .CHEKLIS_23
                CheckEdit24.Checked = .CHEKLIS_24
                CheckEdit25.Checked = .CHEKLIS_25
                CheckEdit26.Checked = .CHEKLIS_26
                CheckEdit27.Checked = .CHEKLIS_27
                CheckEdit28.Checked = .CHEKLIS_28
                CheckEdit29.Checked = .CHEKLIS_29
                CheckEdit30.Checked = .CHEKLIS_30
                CheckEdit31.Checked = .CHEKLIS_31
                CheckEdit32.Checked = .CHEKLIS_32
                CheckEdit33.Checked = .CHEKLIS_33
                CheckEdit34.Checked = .CHEKLIS_34
                CheckEdit35.Checked = .CHEKLIS_35
                CheckEdit36.Checked = .CHEKLIS_36
                CheckEdit37.Checked = .CHEKLIS_37
                CheckEdit38.Checked = .CHEKLIS_38
                CheckEdit39.Checked = .CHEKLIS_39
                CheckEdit40.Checked = .CHEKLIS_40
                CheckEdit41.Checked = .CHEKLIS_41
                CheckEdit42.Checked = .CHEKLIS_42
                CheckEdit43.Checked = .CHEKLIS_43
                CheckEdit44.Checked = .CHEKLIS_44
                CheckEdit45.Checked = .CHEKLIS_45
                CheckEdit46.Checked = .CHEKLIS_46
                CheckEdit47.Checked = .CHEKLIS_47
                CheckEdit48.Checked = .CHEKLIS_48
                CheckEdit49.Checked = .CHEKLIS_49
                CheckEdit50.Checked = .CHEKLIS_50
                CheckEdit51.Checked = .CHEKLIS_51
                CheckEdit52.Checked = .CHEKLIS_52
                CheckEdit53.Checked = .CHEKLIS_53
                CheckEdit54.Checked = .CHEKLIS_54
                CheckEdit55.Checked = .CHEKLIS_55
                CheckEdit56.Checked = .CHEKLIS_56
                CheckEdit57.Checked = .CHEKLIS_57
                CheckEdit58.Checked = .CHEKLIS_58
                CheckEdit59.Checked = .CHEKLIS_59
                CheckEdit60.Checked = .CHEKLIS_60
                CheckEdit61.Checked = .CHEKLIS_61
                CheckEdit62.Checked = .CHEKLIS_62
                CheckEdit63.Checked = .CHEKLIS_63
                CheckEdit64.Checked = .CHEKLIS_64

                TextEdit1.Text = .KETERANGAN_1
                TextEdit2.Text = .KETERANGAN_2
                grdDPJPUtama.Text = .KETERANGAN_3
                grdUSERHADNOVER.Text = .KETERANGAN_4

                MemoEdit1.Text = .KETERANGAN_7
            End With
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Function fn_Validate() As Boolean
        Try
            fn_Validate = True

            If sRegister = String.Empty Then
                MsgBox("Nomor Register Kosong", MsgBoxStyle.Exclamation, Me.Text)
                deDATE.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                deDATE.ErrorText = Statement.ErrorRequired

                deDATE.Focus()
                fn_Validate = False
                Exit Function
            End If
            If grdDPJPUtama.Text = String.Empty Then
                grdDPJPUtama.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                grdDPJPUtama.ErrorText = Statement.ErrorRequired

                grdDPJPUtama.Focus()
                fn_Validate = False
                Exit Function
            End If
            If grdUSERHADNOVER.Text = String.Empty Then
                grdUSERHADNOVER.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                grdUSERHADNOVER.ErrorText = Statement.ErrorRequired

                grdUSERHADNOVER.Focus()
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
                .KDMPPA = sNoid
                .KDPENDAFTARAN = sRegister
                .KDCUSTOMER = sRM
                .NAMAPASIEN = sNAMA
                .JENISKELAMIN = sJENISKELAMIN
                .TANGGALLAHIR = sTANGGALLAHIR
                .KDUSER = sUserID
                .DATECREATED = Now
                .RUANGAN = sRuangan
                Try
                    .DATEUPDATED = oDigital_MPP.GetData(sNoid).DATEUPDATED
                Catch ex As Exception
                    .DATEUPDATED = Now
                End Try
                .DATE = deDATE.DateTime

                .CHEKLIS_1 = CheckEdit1.Checked
                .CHEKLIS_2 = CheckEdit2.Checked
                .CHEKLIS_3 = CheckEdit3.Checked
                .CHEKLIS_4 = CheckEdit4.Checked
                .CHEKLIS_5 = CheckEdit5.Checked
                .CHEKLIS_6 = CheckEdit6.Checked
                .CHEKLIS_7 = CheckEdit7.Checked
                .CHEKLIS_8 = CheckEdit8.Checked
                .CHEKLIS_9 = CheckEdit9.Checked
                .CHEKLIS_10 = CheckEdit10.Checked
                .CHEKLIS_11 = CheckEdit11.Checked
                .CHEKLIS_12 = CheckEdit12.Checked
                .CHEKLIS_13 = CheckEdit13.Checked
                .CHEKLIS_14 = CheckEdit14.Checked
                .CHEKLIS_15 = CheckEdit15.Checked
                .CHEKLIS_16 = CheckEdit16.Checked
                .CHEKLIS_17 = CheckEdit17.Checked
                .CHEKLIS_18 = CheckEdit18.Checked
                .CHEKLIS_19 = CheckEdit19.Checked
                .CHEKLIS_20 = CheckEdit20.Checked
                .CHEKLIS_21 = CheckEdit21.Checked
                .CHEKLIS_22 = CheckEdit22.Checked
                .CHEKLIS_23 = CheckEdit23.Checked
                .CHEKLIS_24 = CheckEdit24.Checked
                .CHEKLIS_25 = CheckEdit25.Checked
                .CHEKLIS_26 = CheckEdit26.Checked
                .CHEKLIS_27 = CheckEdit27.Checked
                .CHEKLIS_28 = CheckEdit28.Checked
                .CHEKLIS_29 = CheckEdit29.Checked
                .CHEKLIS_30 = CheckEdit30.Checked
                .CHEKLIS_31 = CheckEdit31.Checked
                .CHEKLIS_32 = CheckEdit32.Checked
                .CHEKLIS_33 = CheckEdit33.Checked
                .CHEKLIS_34 = CheckEdit34.Checked
                .CHEKLIS_35 = CheckEdit35.Checked
                .CHEKLIS_36 = CheckEdit36.Checked
                .CHEKLIS_37 = CheckEdit37.Checked
                .CHEKLIS_38 = CheckEdit38.Checked
                .CHEKLIS_39 = CheckEdit39.Checked
                .CHEKLIS_40 = CheckEdit40.Checked
                .CHEKLIS_41 = CheckEdit41.Checked
                .CHEKLIS_42 = CheckEdit42.Checked
                .CHEKLIS_43 = CheckEdit43.Checked
                .CHEKLIS_44 = CheckEdit44.Checked
                .CHEKLIS_45 = CheckEdit45.Checked
                .CHEKLIS_46 = CheckEdit46.Checked
                .CHEKLIS_47 = CheckEdit47.Checked
                .CHEKLIS_48 = CheckEdit48.Checked
                .CHEKLIS_49 = CheckEdit49.Checked
                .CHEKLIS_50 = CheckEdit50.Checked
                .CHEKLIS_51 = CheckEdit51.Checked
                .CHEKLIS_52 = CheckEdit52.Checked
                .CHEKLIS_53 = CheckEdit53.Checked
                .CHEKLIS_54 = CheckEdit54.Checked
                .CHEKLIS_55 = CheckEdit55.Checked
                .CHEKLIS_56 = CheckEdit56.Checked
                .CHEKLIS_57 = CheckEdit57.Checked
                .CHEKLIS_58 = CheckEdit58.Checked
                .CHEKLIS_59 = CheckEdit59.Checked
                .CHEKLIS_60 = CheckEdit60.Checked
                .CHEKLIS_61 = CheckEdit61.Checked
                .CHEKLIS_62 = CheckEdit62.Checked
                .CHEKLIS_63 = CheckEdit63.Checked
                .CHEKLIS_64 = CheckEdit64.Checked
                .CHEKLIS_65 = False
                .CHEKLIS_66 = False
                .CHEKLIS_67 = False
                .CHEKLIS_68 = False
                .CHEKLIS_69 = False
                .CHEKLIS_70 = False
                .CHEKLIS_71 = False
                .CHEKLIS_72 = False
                .CHEKLIS_73 = False
                .CHEKLIS_74 = False
                .CHEKLIS_75 = False
                .CHEKLIS_76 = False
                .CHEKLIS_77 = False
                .CHEKLIS_78 = False
                .CHEKLIS_79 = False
                .CHEKLIS_80 = False
                .KETERANGAN_1 = TextEdit1.Text
                .KETERANGAN_2 = TextEdit2.Text
                .KETERANGAN_3 = grdDPJPUtama.EditValue
                .KETERANGAN_4 = grdUSERHADNOVER.EditValue
                .KETERANGAN_5 = grdDPJPUtama.Text 
                .KETERANGAN_6 = grdUSERHADNOVER.Text
                .KETERANGAN_7 = MemoEdit1.Text
                .KETERANGAN_8 = ""
                .KETERANGAN_9 = ""
                .KETERANGAN_10 = ""
            End With

            If oFormMode = FORM_MODE.FORM_MODE_ADD Then
                Try
                    fn_Save = oDigital_MPP.InsertData(ds)
                Catch oErr As Exception
                    MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            ElseIf oFormMode = FORM_MODE.FORM_MODE_EDIT Then
                Try
                    fn_Save = oDigital_MPP.UpdateData(ds)
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
    'Private Sub DeleteToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DeleteToolStripMenuItem.Click
    '    If oFormMode = FORM_MODE.FORM_MODE_VIEW Then Exit Sub
    '    grvDetail.DeleteSelectedRows()
    'End Sub
#End Region
#Region "Command Button"
    Private Sub frmDigital_MPP_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
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
    Private Sub fn_LoadDokter()
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
            SQL &= "A.KDDOCTOR "
            SQL &= ",NAME_DISPLAY = (SELECT CASE A.FRONT_TITLE WHEN '' THEN '' ELSE A.FRONT_TITLE + ' ' END) + A.NAME_DISPLAY + A.BACK_TITLE "
            SQL &= "FROM  "
            SQL &= "M_DOCTOR A "
            SQL &= "WHERE "
            SQL &= "ISACTIVE = '1' "
            SQL &= "AND CATEGORY = 1 "
            SQL &= "ORDER BY NAME_DISPLAY "

            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "M_DOCTOR")

            grdDPJPUtama.Properties.DataSource = ds.Tables("M_DOCTOR")
            grdDPJPUtama.Properties.ValueMember = "KDDOCTOR"
            grdDPJPUtama.Properties.DisplayMember = "NAME_DISPLAY"

            If oConn.State = ConnectionState.Open Then
                oConn.Close()
            End If
        Catch oErr As Exception
            MsgBox("Load Dokter Data : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadKDUSER()
        Dim oTemplate As New Reference.clsUnit
        Try
            grdUSERHADNOVER.Properties.DataSource = oTemplate.GetData.Where(Function(x) x.ISACTIVE = True).ToList()
            grdUSERHADNOVER.Properties.ValueMember = "KDUNIT"
            grdUSERHADNOVER.Properties.DisplayMember = "MEMO"
        Catch oErr As Exception
            MsgBox("Load User : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
#End Region
End Class