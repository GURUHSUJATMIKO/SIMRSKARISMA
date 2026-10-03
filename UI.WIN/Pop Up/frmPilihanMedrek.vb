Public Class frmPilihanMedrek
    Public Sub LoadMe(ByVal sDiagnosa As Boolean, ByVal sResep As Boolean, ByVal sOrder As Boolean, ByVal sRencanaKontrol As Boolean, ByVal sLab As Boolean, ByVal sRad As Boolean)
        sSIMPAN = False
        chkDiagnosa.Checked = sDiagnosa
        chkResep.Checked = sResep
        chkTINDAKAN.Checked = sOrder
        chkRencanaKontol.Checked = sRencanaKontrol
        chkLab.Checked = sLab
        chkRad.Checked = sRad
        SimpleButton1.Focus()
    End Sub
    Private Sub SimpleButton2_Click(sender As Object, e As EventArgs) Handles SimpleButton2.Click
        sSIMPAN = False
        Me.Close()
    End Sub
    Private Sub SimpleButton1_Click(sender As Object, e As EventArgs) Handles SimpleButton1.Click
        sSIMPAN = True
        Me.Close()
    End Sub
End Class