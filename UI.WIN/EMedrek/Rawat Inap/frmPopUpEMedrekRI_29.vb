
Imports System.Linq
Imports System.Data
Imports System.Data.SqlClient

Public Class frmPopUpEMedrekRI_29
    Private oTgl As String = ""
    Private oTempat As String = ""
    Private oUmur As String = ""
    Private oJenis As String = ""
    Private oPenolong As String = ""
    Private oPenyulit As String = ""
    Private oBB As String = ""
    Private oPB As String = ""
    Private oKeadaan As String = ""

    Public Sub fn_LoadMe(ByVal sDATE As String, ByVal sTempat As String, ByVal sUmur As String, ByVal sJenis As String, ByVal sPenolong As String, ByVal sPenyulit As String, ByVal sBB As String, ByVal sPB As String, ByVal sKeadaan As String)
        oTgl = sDATE
        oTempat = sTempat
        oUmur = sUmur
        oJenis = sJenis
        oPenolong = sPenolong
        oPenyulit = sPenyulit
        oBB = sBB
        oPB = sPB
        oKeadaan = sKeadaan
    End Sub
    Private Sub Form_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        fn_Load()
        fn_LoadLanguage()
    End Sub
    Private Sub cmdSelect_Click()
        If txtJENIS.Text = String.Empty Then
            MsgBox("Dibutuhkan Jenis Persalinan", MsgBoxStyle.Exclamation, Me.Text)
            txtJENIS.Focus()
            Exit Sub
        End If

        If txtTGL.Text = "" Then
            MsgBox("Dibutuhkan Tanggal", MsgBoxStyle.Exclamation, Me.Text)
            Exit Sub
        End If

        sFind1 = txtTGL.Text
        sFind2 = txtTEMPAT.Text
        sFind3 = txtUMUR.Text
        sFind4 = txtJENIS.Text
        sFind5 = txtPENOLONG.Text
        sFind6 = txtPENYULIT.Text
        sFind7 = txtBB.Text
        sFind8 = txtPB.Text
        sFind9 = txtKEADAANANAK.Text

        Me.Close()
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
    Public Sub fn_LoadLanguage()

    End Sub
    Private Sub fn_Load()
        txtTGL.Text = oTgl
        txtTEMPAT.Text = oTempat
        txtUMUR.Text = oUmur
        txtJENIS.Text = oJenis
        txtPENOLONG.Text = oPenolong
        txtPENYULIT.Text = oPenyulit
        txtBB.Text = oBB
        txtPB.Text = oPB
        txtKEADAANANAK.Text = oKeadaan
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

        sFind1 = String.Empty
        sFind2 = String.Empty
        sFind3 = String.Empty
        sFind4 = String.Empty
        sFind5 = String.Empty
        sFind6 = String.Empty
        sFind7 = String.Empty
        sFind8 = String.Empty
        sFind9 = String.Empty


        Me.Close()
    End Sub
    Private Sub btnSaveClose_Click() Handles btnSaveClose.ItemClick
        cmdSelect_Click()
    End Sub
#End Region
End Class