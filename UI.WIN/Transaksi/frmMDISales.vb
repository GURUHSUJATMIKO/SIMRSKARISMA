Public Class frmMDISales
    Sub New()
        InitializeComponent()
    End Sub
    Private sCategory As Integer = 0

    Public Sub fn_LoadCategory(ByVal Category As Integer)
        sCategory = Category

        If sCategory = 0 Then
            Me.Text = "Dasboard Billing " & "Rawat Jalan"
        ElseIf sCategory = 1 Then
            Me.Text = "Dasboard Billing " & "Rawat Inap"
        ElseIf sCategory = 2 Then
            Me.Text = "Dasboard Billing " & "Laboratorium"
        ElseIf sCategory = 3 Then
            Me.Text = "Dasboard Billing " & "Radiologi"
        ElseIf sCategory = 4 Then
            Me.Text = "Dasboard Billing " & "Farmasi"
        End If
    End Sub
    Private Sub frmMDISales_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
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
