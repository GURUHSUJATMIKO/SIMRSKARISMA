Public Class frmMDISalesMedrek
    Sub New()
        InitializeComponent()
    End Sub
    Dim sParamater As Boolean = False

    Public Sub fn_LoadCategoryDokter(ByVal Paramater As Boolean)
        sParamater = Paramater
    End Sub
    Private Sub frmMDISales_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim frmMedrekRawatNewJalanList As New frmMedrekRawatNewJalanList
        Try
            frmMedrekRawatNewJalanList.MdiParent = Me
            frmMedrekRawatNewJalanList.fn_LoadMe(sParamater, 0)
            frmMedrekRawatNewJalanList.Show()
            frmMedrekRawatNewJalanList.WindowState = FormWindowState.Maximized
        Catch ex As Exception
            MsgBox("Load Form Detail : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try

        'If xtraTab.Pages.Count < 1 Then
        '    If sCategory = 4 Then
        '        Dim frmBillingFarmasi As New frmBillingFarmasi
        '        Try
        '            frmBillingFarmasi.LoadMe(FORM_MODE.FORM_MODE_ADD, sCategory, True)
        '            frmBillingFarmasi.MdiParent = Me
        '            frmBillingFarmasi.Show()
        '            frmBillingFarmasi.WindowState = FormWindowState.Maximized
        '        Catch ex As Exception
        '            MsgBox("Load Form Detail : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
        '        End Try
        '    Else
        '        Dim frmBilling As New frmBilling
        '        Try
        '            frmBilling.LoadMe(FORM_MODE.FORM_MODE_ADD, sCategory)
        '            frmBilling.MdiParent = Me
        '            frmBilling.Show()
        '            frmBilling.WindowState = FormWindowState.Maximized
        '        Catch ex As Exception
        '            MsgBox("Load Form Detail : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
        '        End Try
        '    End If
        'End If
    End Sub
    Private Sub xtraTab_PageRemoved(sender As System.Object, e As DevExpress.XtraTabbedMdi.MdiTabPageEventArgs) Handles xtraTab.PageRemoved
        'If xtraTab.Pages.Count < 1 Then
        '    If sCategory = 4 Then
        '        Dim frmBillingFarmasi As New frmBillingFarmasi
        '        Try
        '            frmBillingFarmasi.LoadMe(FORM_MODE.FORM_MODE_ADD, sCategory, True)
        '            frmBillingFarmasi.MdiParent = Me
        '            frmBillingFarmasi.Show()
        '            frmBillingFarmasi.WindowState = FormWindowState.Maximized
        '        Catch ex As Exception
        '            MsgBox("Load Form Detail : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
        '        End Try
        '    Else
        '        Dim frmBilling As New frmBilling
        '        Try
        '            frmBilling.LoadMe(FORM_MODE.FORM_MODE_ADD, sCategory)
        '            frmBilling.MdiParent = Me
        '            frmBilling.Show()
        '            frmBilling.WindowState = FormWindowState.Maximized
        '        Catch ex As Exception
        '            MsgBox("Load Form Detail : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
        '        End Try
        '    End If
        'End If
    End Sub
End Class
