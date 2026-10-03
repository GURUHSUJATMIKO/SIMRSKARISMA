Imports DataAccess
Imports UI.WIN.MAIN.My.Resources
Imports System.Linq
Imports System.Data.SqlClient

Public Class frmKonsulDokter
    Public Sub fn_LoadMe(ByVal sRegister As String, ByVal KDDOCTOR As String, ByVal KDDOCTORKEPADA As String, ByVal SEQ As String, ByVal ISIKONSUL As String, ByVal JAWABKOSNUL As String, ByVal tanggal_isi As String, ByVal tanggal_jawab As String)
        txtKDREG.Text = sRegister
        grdDPJPUtama.Text = KDDOCTOR
        grdDPJP.Text = KDDOCTORKEPADA
        txtKONSUL.Text = ISIKONSUL
        txtJAWABKONSUL.Text = JAWABKOSNUL
        cboSEQ.Text = SEQ

        If tanggal_isi = "" Then
            deDATEISI.DateTime = Now
        Else
            deDATEISI.DateTime = tanggal_isi
        End If
        If tanggal_jawab = "" Then
            deDATEJAWAB.DateTime = Now
        Else
            deDATEJAWAB.DateTime = tanggal_jawab
        End If
    End Sub
    Private Sub Form_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        fn_LoadDokter()
        sKONSULTASI = ""
    End Sub
    Private Sub Form_Closing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        Dispose()
        'sCode = txtDescription.Text.Trim.ToUpper
    End Sub
    Private Overloads Sub Dispose()
        MyBase.Dispose()
        Me.Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
#Region "Command Button"
    Private Sub frmItem_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        Select Case e.KeyCode
            Case Keys.F12
                btnClose_Click()
            Case Keys.F3
                If btnSaveClose.Enabled = True Then
                    btnSaveClose_Click()
                End If
            Case Keys.Escape
                btnClose_Click()
        End Select
    End Sub
    Private Sub btnClose_Click() Handles btnClose.ItemClick
        Me.Close()
    End Sub
    Private Sub btnSaveClose_Click() Handles btnSaveClose.ItemClick
        If fn_Validate() = False Then Exit Sub
        If MsgBox("Save?", MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, Me.Text) = MsgBoxResult.No Then Exit Sub

        If fn_CariSeq() = False Then
            fn_SimpanCPPTSelesai()
            sKONSULTASI = grdDPJP.Text
        Else
            fn_UpdateCPPTSelesai()
        End If

        MsgBox("Save success!", MsgBoxStyle.Information, Me.Text)
        Me.Close()
    End Sub
    Private Function fn_Validate() As Boolean
        Try
            fn_Validate = True

            If grdDPJPUtama.Text = "" Then
                MsgBox("Dokter Kosong", MsgBoxStyle.Information, Me.Text)
                fn_Validate = False
                Exit Function
            End If

            If grdDPJP.Text = "" Then
                MsgBox("Dokter Kosong", MsgBoxStyle.Information, Me.Text)
                fn_Validate = False
                Exit Function
            End If

            If cboSEQ.Text = "" Then
                MsgBox("Ke Kosong", MsgBoxStyle.Information, Me.Text)
                fn_Validate = False
                Exit Function
            End If

            'Dim oConn As New SqlConnection
            'Dim oComm As New SqlCommand
            'Dim da As SqlDataAdapter
            'Dim ds As New DataSet
            'Dim SQL As String

            'oConn = New SqlConnection(sConnOld)

            'If oConn.State = ConnectionState.Closed Then
            '    oConn.Open()
            'End If

            'SQL = "SELECT "
            'SQL &= "* "
            'SQL &= "FROM "
            'SQL &= "I_TRACKING_KDDOCTOR "
            'SQL &= "WHERE "
            'SQL &= "KDREG = '" & txtKDREG.Text & "' "
            'SQL &= "AND KDDOCTOR_KEPADA = '" & grdDPJP.EditValue & "' "

            'oComm.Connection = oConn
            'oComm.CommandText = SQL
            'oComm.CommandTimeout = 120
            'oComm.CommandType = CommandType.Text

            'da = New SqlDataAdapter(oComm)
            'da.Fill(ds, "SELECT_I_TRACKING_CPPT")

            'If oConn.State = ConnectionState.Open Then
            '    oConn.Close()
            'End If

            'For iLoop As Integer = 0 To ds.Tables("SELECT_I_TRACKING_CPPT").Rows.Count - 1
            '    fn_Validate = False
            'Next

            'If cboSEQ.Text = "" Then
            '    If fn_Validate = False Then
            '        MsgBox("Dokter Sudah di Input", MsgBoxStyle.Information, Me.Text)
            '    End If
            'Else
            '    fn_Validate = True
            'End If

        Catch oErr As Exception
            fn_Validate = False
            MsgBox("Select I_TRACKING_CPPT : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Function
    Private Function fn_CariSeq() As Boolean
        Try
            fn_CariSeq = False

            Dim oConn As New SqlConnection
            Dim oComm As New SqlCommand
            Dim da As SqlDataAdapter
            Dim ds As New DataSet
            Dim SQL As String

            oConn = New SqlConnection(sConnOld)

            If oConn.State = ConnectionState.Closed Then
                oConn.Open()
            End If

            SQL = "SELECT "
            SQL &= "* "
            SQL &= "FROM "
            SQL &= "I_TRACKING_KDDOCTOR "
            SQL &= "WHERE "
            SQL &= "KDREG = '" & txtKDREG.Text & "' "
            SQL &= "AND SEQ = " & cboSEQ.Text & " "

            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "SEQSELECT_I_TRACKING_CPPT")

            If oConn.State = ConnectionState.Open Then
                oConn.Close()
            End If

            For iLoop As Integer = 0 To ds.Tables("SEQSELECT_I_TRACKING_CPPT").Rows.Count - 1
                fn_CariSeq = True
            Next

        Catch oErr As Exception
            MsgBox("Select I_TRACKING_CPPT : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Function
    Private Sub fn_SimpanCPPTSelesai()
        Try
            Dim oConn As New SqlConnection
            Dim oComm As New SqlCommand
            Dim da As SqlDataAdapter
            Dim ds As New DataSet
            Dim SQL As String

            oConn = New SqlConnection(sConnOld)

            If oConn.State = ConnectionState.Closed Then
                oConn.Open()
            End If

            SQL = "INSERT INTO I_TRACKING_KDDOCTOR (KDREG, SEQ, KDDOCTOR_DARI, KDDOCTOR_KEPADA, ISIKONSUL, JAWABKONSUL, TANGGAL_ISIKONSUL, TANGGAL_JAWABKONSUL) "
            SQL &= "VALUES ('" & txtKDREG.Text & "','" & cboSEQ.Text & "', '" & grdDPJPUtama.EditValue & "', '" & grdDPJP.EditValue & "', '" & txtKONSUL.Text & "','" & txtJAWABKONSUL.Text & "','" & deDATEISI.DateTime.ToString("yyyy-MM-dd HH:mm:ss") & "','" & IIf(txtJAWABKONSUL.Text = "", "", deDATEJAWAB.DateTime.ToString("yyyy-MM-dd HH:mm:ss")) & "')"

            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "INSERT_I_TRACKING_KDDOCTOR")

            If oConn.State = ConnectionState.Open Then
                oConn.Close()
            End If
        Catch oErr As Exception
            MsgBox("Insert I_TRACKING_KDDOCTOR : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_UpdateCPPTSelesai()
        Try
            Dim oConn As New SqlConnection
            Dim oComm As New SqlCommand
            Dim da As SqlDataAdapter
            Dim ds As New DataSet
            Dim SQL As String

            oConn = New SqlConnection(sConnOld)

            If oConn.State = ConnectionState.Closed Then
                oConn.Open()
            End If

            SQL = "UPDATE I_TRACKING_KDDOCTOR SET "
            SQL &= "  KDREG = '" & txtKDREG.Text & "' "
            SQL &= ", SEQ = '" & cboSEQ.Text & "' "
            SQL &= ", KDDOCTOR_DARI = '" & grdDPJPUtama.EditValue & "' "
            SQL &= ", KDDOCTOR_KEPADA = '" & grdDPJP.EditValue & "' "
            SQL &= ", ISIKONSUL = '" & txtKONSUL.Text & "' "
            SQL &= ", JAWABKONSUL = '" & txtJAWABKONSUL.Text & "' "
            SQL &= ", TANGGAL_ISIKONSUL = '" & deDATEISI.DateTime.ToString("yyyy-MM-dd HH:mm:ss") & "' "
            SQL &= ", TANGGAL_JAWABKONSUL = '" & IIf(txtJAWABKONSUL.Text = "", "", deDATEJAWAB.DateTime.ToString("yyyy-MM-dd HH:mm:ss")) & "' "
            SQL &= "WHERE "
            SQL &= "KDREG = '" & txtKDREG.Text & "' "
            SQL &= "AND SEQ = '" & cboSEQ.Text & "' "

            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "INSERT_I_TRACKING_KDDOCTOR")

            If oConn.State = ConnectionState.Open Then
                oConn.Close()
            End If
        Catch oErr As Exception
            MsgBox("Update I_TRACKING_KDDOCTOR : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadDokter()
        Try
            'Dim oDoctor As New Reference.clsDoctor

            'Dim dsDoctor = From x In oDoctor.GetData
            '               Where x.ISACTIVE = True
            '               Select x.KDDOCTOR, x.NAME_DISPLAY

            'grdDPJPUtama.Properties.DataSource = dsDoctor.ToList()
            'grdDPJPUtama.Properties.ValueMember = "KDDOCTOR"
            'grdDPJPUtama.Properties.DisplayMember = "NAME_DISPLAY"

            'grdDPJP.Properties.DataSource = dsDoctor.ToList()
            'grdDPJP.Properties.ValueMember = "KDDOCTOR"
            'grdDPJP.Properties.DisplayMember = "NAME_DISPLAY"

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
#End Region
End Class