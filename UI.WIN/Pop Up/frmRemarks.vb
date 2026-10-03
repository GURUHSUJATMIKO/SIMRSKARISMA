Public Class frmRemarks
    Private Sub SimpleButton1_Click(sender As Object, e As EventArgs) Handles SimpleButton1.Click
        sRemarks = sRemarks & vbCrLf & MemoEdit1.Text
        Me.Close()
    End Sub
End Class