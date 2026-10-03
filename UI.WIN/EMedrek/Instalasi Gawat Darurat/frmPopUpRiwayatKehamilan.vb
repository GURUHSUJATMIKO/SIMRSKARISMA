Public Class frmPopUpRiwayatKehamilan
    Private oDate As String = ""
    Private oTempat As String = ""
    Private oUmur As String = ""
    Private oJenis As String = ""
    Private oPenolong As String = ""
    Private oJK As String = ""
    Private oBB As String = ""
    Private oPB As String = ""
    Private oASI As String = ""
    Private oKet As String = ""

    Public Sub fn_LoadMe(ByVal sDATE As String, ByVal sTempatPersalinan As String, ByVal sUmurKehamilan As String, ByVal sJenisPersalinan As String, ByVal sPenolong As String, ByVal sAnakJK As String, ByVal sAnakBB As String, ByVal sAnakPB As String, ByVal sASI As String, ByVal sKet As String)
        oDate = sDATE
        oTempat = sTempatPersalinan
        oUmur = sUmurKehamilan
        oJenis = sJenisPersalinan
        oPenolong = sPenolong
        oJK = sAnakJK
        oBB = sAnakBB
        oPB = sAnakPB
        oASI = sASI
        oKet = sKet
    End Sub
    Private Sub Form_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        fn_Load()
        fn_LoadLanguage()
    End Sub
    Private Sub cmdSelect_Click()
        If txtTEMPATPERSALINAN.Text = String.Empty Then

            MsgBox("Dibutuhkan Tempat Persalinan", MsgBoxStyle.Exclamation, Me.Text)
            txtTEMPATPERSALINAN.Focus()
            Exit Sub
        End If

        sFind10 = txtDATE.Text
        sFind1 = txtTEMPATPERSALINAN.Text
        sFind2 = txtUMURKEHAMILAN.Text
        sFind3 = txtJENISPERSALINAN.Text
        sFind4 = txtPENOLONG.Text
        sFind5 = txtANAKJK.Text
        sFind6 = txtANAKBB.Text
        sFind7 = txtANAKPB.Text
        sFind8 = txtASIEKSLUSIF.Text
        sFind9 = txtKET.Text

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
        txtDATE.Text = oDate
        txtTEMPATPERSALINAN.Text = oTempat
        txtUMURKEHAMILAN.Text = oUmur
        txtJENISPERSALINAN.Text = oJenis
        txtPENOLONG.Text = oPenolong
        txtANAKJK.Text = oJK
        txtANAKBB.Text = oBB
        txtANAKPB.Text = oPB
        txtASIEKSLUSIF.Text = oASI
        txtKET.Text = oKet
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
        sFind10 = String.Empty
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