Imports DataAccess
Imports UI.WIN.MAIN.My.Resources
Imports System.Linq
Imports System.Data.SqlClient

Public Class frmAlihDPJP
#Region "Declaration"
    Private oFormMode As FORM_MODE = FORM_MODE.FORM_MODE_VIEW
    Private sNoId As String
    Private isLoad As Boolean = False
    Private oAlihDPJP As New Digital.clsAlihDPJP
    Private sKDCUSTOMER As String = String.Empty
    Private sNAMA As String = String.Empty
    Private sTUJUAN As String = String.Empty
#End Region
#Region "Function"
    Public Sub LoadMe(ByVal KDCUSTOMER As String, ByVal NAMA As String, ByVal KDPENDAFTARAN As String, ByVal TUJUAN As String)

        Dim dsCek = oAlihDPJP.GetData(KDPENDAFTARAN)
        If dsCek Is Nothing Then
            oFormMode = FORM_MODE.FORM_MODE_ADD
        Else
            oFormMode = FORM_MODE.FORM_MODE_EDIT
        End If

        sKDCUSTOMER = KDCUSTOMER
        sNAMA = NAMA
        sTUJUAN = TUJUAN
        sNoId = KDPENDAFTARAN
        txtKDPENDAFTARAN.Text = sNoId
    End Sub
    Private Sub Form_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        fn_ChangeFormState()
        fn_LoadLanguage()
        isLoad = True
    End Sub
    Public Sub fn_LoadLanguage()
        Try
            Me.Text = "Alih DPJP"

            'lCODE.Text = AlihDPJP.CODE
            'lMEMO.Text = AlihDPJP.MEMO & " *"
            'chkSEQ.Text = AlihDPJP.ISACTIVE
            'chkISDEFAULT.Text = AlihDPJP.ISDEFAULT

            btnSaveNew.Caption = Caption.FormSaveNew
            btnSaveClose.Caption = Caption.FormSaveClose
            btnClose.Caption = Caption.FormClose
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub Form_Closing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        sCode = txtMEMO.Text.Trim.ToUpper
    End Sub
    Private Sub fn_ChangeFormState()
        fn_LoadOperator()

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

        txtMEMO.Properties.ReadOnly = Status
        deDATE_KONSUL.Properties.ReadOnly = Status
        deDATEJAWABKONSUL.Properties.ReadOnly = Status
        grdKDDOCTOR_DARI.Properties.ReadOnly = Status
        grdKDDOCTOR_KEPADA.Properties.ReadOnly = Status
        txtMEMO_KONSUL.Properties.ReadOnly = Status
        txtMEMO_JAWABKONSUL.Properties.ReadOnly = Status
    End Sub
    Private Sub fn_EmptyMe()
        txtCODE.Text = sNoId
        txtMEMO.ResetText()
        deDATE_KONSUL.DateTime = Now
        deDATEJAWABKONSUL.DateTime = Now
        grdKDDOCTOR_DARI.ResetText()
        grdKDDOCTOR_KEPADA.ResetText()
        txtMEMO_KONSUL.ResetText()
        txtMEMO_JAWABKONSUL.ResetText()
    End Sub
    Private Sub fn_LoadData()
        Try
            Dim ds = oAlihDPJP.GetData(sNoId)

            With ds
                txtCODE.Text = sNoId
                txtMEMO.Text = .MEMO
                deDATE_KONSUL.DateTime = .DATE_KONSUL
                deDATEJAWABKONSUL.DateTime = .DATE_JAWABKONSUL
                grdKDDOCTOR_DARI.Text = .KDDOCTOR_DARI
                grdKDDOCTOR_KEPADA.Text = .KDDOCTOR_KEPADA
                txtMEMO_KONSUL.Text = .MEMO_KONSUL
                txtMEMO_JAWABKONSUL.Text = .MEMO_JAWABKONSUL
            End With
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Function fn_Validate() As Boolean
        Try
            fn_Validate = True
            If grdKDDOCTOR_DARI.Text = String.Empty Then
                grdKDDOCTOR_DARI.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                grdKDDOCTOR_DARI.ErrorText = Statement.ErrorRequired

                grdKDDOCTOR_DARI.Focus()
                fn_Validate = False
                Exit Function
            End If
            If sNoId = String.Empty Then
                txtKDPENDAFTARAN.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                txtKDPENDAFTARAN.ErrorText = Statement.ErrorRequired

                txtKDPENDAFTARAN.Focus()
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
            Dim ds = oAlihDPJP.GetStructureHeader
            With ds
                Try
                    .DATECREATED = oAlihDPJP.GetData(sNoId).DATECREATED
                Catch oErr As Exception
                    .DATECREATED = Now
                End Try
                .DATEUPDATED = Now
                .DATE_KONSUL = deDATE_KONSUL.DateTime
                .DATE_JAWABKONSUL = deDATEJAWABKONSUL.DateTime
                .KDALIHDPJP = sNoId
                .KDCUSTOMER = sKDCUSTOMER
                .NAMAPASIEN = sNAMA
                .KDPENDAFTARAN = sNoId
                .TUJUAN = sTUJUAN
                .SEQ = 0
                .KDDOCTOR_DARI = grdKDDOCTOR_DARI.EditValue
                .NAMADOKTER_DARI = grdKDDOCTOR_DARI.Text
                .KDDOCTOR_KEPADA = grdKDDOCTOR_KEPADA.EditValue
                .NAMADOKTER_KEPADA = grdKDDOCTOR_KEPADA.Text
                .MEMO_KONSUL = txtMEMO_KONSUL.Text
                .MEMO_JAWABKONSUL = txtMEMO_JAWABKONSUL.Text
                .MEMO = txtMEMO.Text
                .KDUSER = sUserID
            End With

            If oFormMode = FORM_MODE.FORM_MODE_ADD Then
                Try
                    fn_Save = oAlihDPJP.InsertData(ds)
                Catch oErr As Exception
                    MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            ElseIf oFormMode = FORM_MODE.FORM_MODE_EDIT Then
                Try
                    fn_Save = oAlihDPJP.UpdateData(ds)
                Catch oErr As Exception
                    MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            End If
            If fn_Save = True Then
                fn_UpdaetDPJP()
                fn_LoadDPJPSemua()
            End If
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            fn_Save = False
        End Try
    End Function
    Private Sub fn_UpdaetDPJP()
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

            SQL = "UPDATE "
            SQL &= "S_PENDAFTARAN_H "
            SQL &= "SET KDDOCTOR = '" & grdKDDOCTOR_KEPADA.EditValue & "' "
            SQL &= "WHERE KDREG = '" & sNoId & "' "

            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "S_PENDAFTARAN_H")

            If oConn.State = ConnectionState.Open Then
                oConn.Close()
            End If
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadDPJPSemua()
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
            SQL &= "A.* "
            SQL &= "FROM "
            SQL &= "I_TRACKING_KDDOCTOR A "
            SQL &= "WHERE KDREG = '" & sNoId & "' "

            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "I_TRACKING_KDDOCTOR")

            For iLoop As Integer = 0 To ds.Tables("I_TRACKING_KDDOCTOR").Rows.Count - 1
                With ds.Tables("I_TRACKING_KDDOCTOR")
                    If .Rows(iLoop)("KDDOCTOR_KEPADA") = grdKDDOCTOR_KEPADA.EditValue Then
                        Dim sSeq As Integer = .Rows(iLoop)("SEQ")
                        Dim sISIKONSUL As String = .Rows(iLoop)("ISIKONSUL")
                        fn_UpdaetDPJPI_TRACKING_KDDOCTOR(sSeq, "ALIH DPJP" & vbCrLf & sISIKONSUL)
                    End If
                End With
            Next

            If oConn.State = ConnectionState.Open Then
                oConn.Close()
            End If
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_UpdaetDPJPI_TRACKING_KDDOCTOR(ByVal sEQ As Integer, ByVal sISIKONSUL As String)
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

            SQL = "UPDATE "
            SQL &= "I_TRACKING_KDDOCTOR "
            SQL &= "SET SEQ = " & sEQ & " "
            If sISIKONSUL <> "" Then
                SQL &= ",ISIKONSUL = '" & sISIKONSUL & "' "
                SQL &= ",KDDOCTOR_KEPADA = '" & grdKDDOCTOR_DARI.EditValue & "' "
            End If
            SQL &= "WHERE KDREG = '" & sNoId & "' "
            SQL &= "AND KDDOCTOR_KEPADA = '" & grdKDDOCTOR_KEPADA.EditValue & "' "

            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "I_TRACKING_KDDOCTOR_UPDATE")

            If oConn.State = ConnectionState.Open Then
                oConn.Close()
            End If
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
#End Region
#Region "Command Button"
    Private Sub frmItem_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
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
    Private Sub fn_LoadOperator()
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
            SQL &= "FROM "
            SQL &= "M_DOCTOR A "
            SQL &= "WHERE "
            SQL &= "A.ISACTIVE = 1 "
            SQL &= "AND A.CATEGORY = 1 "
            SQL &= "ORDER BY A.NAME_DISPLAY "

            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "DOKTER")

            grdKDDOCTOR_DARI.Properties.DataSource = ds.Tables("DOKTER")
            grdKDDOCTOR_DARI.Properties.ValueMember = "KDDOCTOR"
            grdKDDOCTOR_DARI.Properties.DisplayMember = "NAME_DISPLAY"

            grdKDDOCTOR_KEPADA.Properties.DataSource = ds.Tables("DOKTER")
            grdKDDOCTOR_KEPADA.Properties.ValueMember = "KDDOCTOR"
            grdKDDOCTOR_KEPADA.Properties.DisplayMember = "NAME_DISPLAY"

            If oConn.State = ConnectionState.Open Then
                oConn.Close()
            End If
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
#End Region
End Class