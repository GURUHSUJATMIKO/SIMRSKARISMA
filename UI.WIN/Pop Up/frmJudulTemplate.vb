Public Class frmJudulTemplate
    Private Sub Me_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        MemoEdit1.ResetText()
    End Sub
    Private Sub SimpleButton1_Click(sender As Object, e As EventArgs) Handles SimpleButton1.Click
        sRemarks = MemoEdit1.Text
        Me.Close()
    End Sub
End Class