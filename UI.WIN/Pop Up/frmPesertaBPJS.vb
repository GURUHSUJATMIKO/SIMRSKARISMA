Public Class frmPesertaBPJS
    Private Sub Form_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'sDelete = False
    End Sub
    Private Sub Form_Closing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        Dispose()
        'sCode = txtCODE.Text.Trim.ToUpper
    End Sub
    Public Sub LoadMe(NoRMBPJS As String, ByVal Message As String)
        lblME.Text = IIf(NoRMBPJS <> "", "PASIEN LAMA", "PASIEN BARU")
        'txtPasien.Text = Pasien
        'txtJenisPeserta.Text = JenisPeserta
        'txtKARTUBPJS.Text = NoKartuBPJS
        'txtNoRMRS.Text = NoRMRS
        'txtNoRMBPJS.Text = NoRMBPJS
        'txtJenisKelamin.Text = JenisKelamin
        'txtTanggalLahir.Text = TanggalLahir
        'txtUmur.Text = Umur
        'txtFaskes.Text = Faskes
        'txtMEMO.Text = Message
        'txtPasien.Focus()

        'If NoRMRS = "000000000" Then
        '    txtNoRMRS.ResetText()
        'End If
        txtMEMO.Text = Message
    End Sub
    Private Sub btnOK_Click(sender As Object, e As EventArgs) Handles btnOK.Click
        Me.Close()
    End Sub
    Private Sub txtPasien_KeyPress(sender As Object, e As KeyPressEventArgs)
        If Asc(e.KeyChar) = 13 Then
            Me.Close()
        End If
    End Sub
End Class