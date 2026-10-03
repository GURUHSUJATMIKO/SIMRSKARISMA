Public Class frmRemarksRawatInap
    Private Sub Me_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        txtRuangan.Text = sRemarks_Ruangan
        txtRencanaPembedahan.Text = sRemarks_RencanaPembedahan
        txtIntruksiDokter.Text = sRemarks_IntruksiDokter
    End Sub
    Private Sub SimpleButton1_Click(sender As Object, e As EventArgs) Handles SimpleButton1.Click
        If txtIndikasi.Text = "" Then
            MsgBox("Silahkan Isi Indikasi", MsgBoxStyle.Exclamation, Me.Text)
        Else
            sRemarks_Ruangan = txtRuangan.Text
            sRemarks_RencanaPembedahan = txtRencanaPembedahan.Text
            sRemarks_IntruksiDokter = txtIntruksiDokter.Text & vbCrLf & "Indikasi " & txtIndikasi.Text
            Me.Close()
        End If
    End Sub
End Class