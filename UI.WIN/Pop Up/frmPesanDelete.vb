Public Class frmPesanDelete
    Private Sub Form_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        sDeletePesan = ""
    End Sub
    Private Sub Form_Closing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        Dispose()
        sDeletePesan = txtHapus.Text.Trim.ToUpper
    End Sub
    Private Sub btnBayar_Click(sender As Object, e As EventArgs) Handles btnHapus.Click
        If txtHapus.Text = "" Then
            MsgBox("Alasan Kosong", MsgBoxStyle.Exclamation, Me.Text)
        Else
            Me.Close()
        End If
    End Sub
    Private Sub btnBayarTidak_Click(sender As Object, e As EventArgs) Handles btnBatal.Click
        sDeletePesan = ""
        Me.Close()
    End Sub
End Class