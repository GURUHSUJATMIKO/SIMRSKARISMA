Imports DataAccess
Imports UI.WIN.MAIN.My.Resources
Imports System.Linq

Public Class frmPopUpPenanganandanPenilaianUlang
    Private oDate As DateTime
    Private oPenanganan As String = ""
    Private oNama As String = ""
    Private oParaf As String = ""
    Public Sub fn_LoadMe(ByVal sDATE As DateTime, ByVal sPenanganan As String, ByVal sNama As String, ByVal sParaf As String)
        oDate = sDATE
        oPenanganan = sPenanganan
        oNama = sNama
        oParaf = sParaf
    End Sub
    Private Sub Form_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        fn_NOIDUSER()
        fn_Load()
        fn_LoadLanguage()
    End Sub
    Private Sub cmdSelect_Click()
        If txtPENANGANAN.Text = String.Empty Then

            MsgBox("Dibutuhkan Penanganan dan Penilaian Ulang", MsgBoxStyle.Exclamation, Me.Text)
            txtNama.Focus()
            Exit Sub
        End If

        sDATEFrom = deDATE.Time
        sFind1 = txtPENANGANAN.Text
        sFind2 = txtNama.Text
        sFind3 = grdUSER.EditValue

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
        deDATE.Time = oDate
        txtPENANGANAN.Text = oPenanganan
        txtNama.Text = oNama
        grdUSER.Text = oParaf
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
        sDATEFrom = Now
        sFind1 = String.Empty
        sFind2 = String.Empty
        sFind3 = String.Empty

        Me.Close()
    End Sub
    Private Sub btnSaveClose_Click() Handles btnSaveClose.ItemClick
        cmdSelect_Click()
    End Sub
    Private Sub fn_NOIDUSER()
        Dim oUser As New Setting.clsUser
        Try
            grdUSER.Properties.DataSource = oUser.GetData.Where(Function(x) x.ISACTIVE = True).ToList()
            grdUSER.Properties.ValueMember = "KDUSER"
            grdUSER.Properties.DisplayMember = "KDUSER"

        Catch oErr As Exception
            MsgBox("Load User Data : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
#End Region
End Class