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

        Dim frmMain As New frmMain
        frmMain.WindowState = FormWindowState.Maximized

        Application.Run(frmMain)
    End Sub
End Class
