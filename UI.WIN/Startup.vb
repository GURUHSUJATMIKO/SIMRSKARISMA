Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Windows.Forms
Imports DevExpress.Skins
Imports DevExpress.XtraSplashScreen
Imports DevExpress.Utils
Imports System.Drawing
Imports DevExpress.Skins.Info
Imports System.Globalization
Imports DevExpress.LookAndFeel

Friend NotInheritable Class Startup
    ''' <summary>
    ''' The main entry point for the application.
    ''' </summary>
    Private Sub New()
    End Sub

    Private Shared ReadOnly FormsWithGridPolicy As New HashSet(Of Form)

    <STAThread()> _
    Shared Sub Main()
        Try
            SplashScreenManager.ShowForm(GetType(frmSplash))
        Catch ex As Exception

        Finally
            SplashScreenManager.CloseForm(False)
        End Try

        DevExpress.Skins.SkinManager.EnableFormSkins()
        'DevExpress.UserSkins.BonusSkins.Register()
        'UserLookAndFeel.Default.SetSkinStyle("VS2010")
        UserLookAndFeel.Default.SetSkinStyle("Blue")

        Dim demoCI As CultureInfo = CType(Application.CurrentCulture.Clone(), CultureInfo)
        demoCI.NumberFormat.CurrencySymbol = "$"
        Application.CurrentCulture = demoCI

        AppearanceObject.DefaultFont = New Font("Arial", 10)
        Application.EnableVisualStyles()
        Application.SetCompatibleTextRenderingDefault(False)

        AddHandler Application.Idle, AddressOf ApplyGridPolicyToOpenForms

        Dim frmMain As New frmMain
        frmMain.WindowState = FormWindowState.Maximized

        Application.Run(frmMain)
    End Sub

    Private Shared Sub ApplyGridPolicyToOpenForms(ByVal sender As Object, ByVal e As EventArgs)
        For Each openForm As Form In Application.OpenForms
            If Not FormsWithGridPolicy.Contains(openForm) Then
                FormsWithGridPolicy.Add(openForm)
                AddHandler openForm.FormClosed, AddressOf GridPolicyFormClosed
                WatchGridPolicyControls(openForm)
            End If
        Next
    End Sub

    Private Shared Sub GridPolicyFormClosed(ByVal sender As Object, ByVal e As FormClosedEventArgs)
        FormsWithGridPolicy.Remove(DirectCast(sender, Form))
    End Sub

    Private Shared Sub GridPolicyControlAdded(ByVal sender As Object, ByVal e As ControlEventArgs)
        WatchGridPolicyControls(e.Control)
    End Sub

    Private Shared Sub WatchGridPolicyControls(ByVal control As Control)
        AddHandler control.ControlAdded, AddressOf GridPolicyControlAdded

        Dim gridControl As DevExpress.XtraGrid.GridControl = TryCast(control, DevExpress.XtraGrid.GridControl)
        If gridControl IsNot Nothing Then
            For Each view As DevExpress.XtraGrid.Views.Base.BaseView In gridControl.ViewCollection
                ApplyGridPolicyToView(view)
            Next

            AddHandler gridControl.ViewRegistered, AddressOf GridPolicyViewRegistered
        End If

        For Each childControl As Control In control.Controls
            WatchGridPolicyControls(childControl)
        Next
    End Sub

    Private Shared Sub GridPolicyViewRegistered(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.ViewOperationEventArgs)
        ApplyGridPolicyToView(e.View)
    End Sub

    Private Shared Sub ApplyGridPolicyToView(ByVal view As DevExpress.XtraGrid.Views.Base.BaseView)
        Dim gridView As DevExpress.XtraGrid.Views.Grid.GridView = TryCast(view, DevExpress.XtraGrid.Views.Grid.GridView)
        If gridView IsNot Nothing Then
            gridView.OptionsCustomization.AllowQuickHideColumns = False
        End If
    End Sub
End Class
