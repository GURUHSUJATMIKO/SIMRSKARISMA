Public Class xtraTrackingPasien
    Private Sub xtra_BeforePrint(ByVal sender As System.Object, ByVal e As System.Drawing.Printing.PrintEventArgs) Handles MyBase.BeforePrint
        'lblLabel_1.Text = sLabel_1
        'lblLabel_2.Text = sLabel_2

        lHeaderTracking.Text = sHeaderJudulTracking

    End Sub
    Private Sub xtraTrackingPasien_PrintProgress(sender As System.Object, e As DevExpress.XtraPrinting.PrintProgressEventArgs) Handles MyBase.PrintProgress
        If e.PrintAction = Printing.PrintAction.PrintToFile Or e.PrintAction = Printing.PrintAction.PrintToPrinter Then
            sPrintRegister = True
        End If
    End Sub
End Class