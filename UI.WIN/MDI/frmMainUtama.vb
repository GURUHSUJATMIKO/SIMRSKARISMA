Imports DataAccess
Imports UI.WIN.MAIN.My.Resources

Public Class frmMainUtama
#Region "Declaration"
    Private slidingMenu As String = "close"
#End Region
#Region "Function"
    Private Sub frmMain_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        setTextMenuList()
        fn_LoadLogin()
        fn_LoadNameModueldanKoneksi()
    End Sub
    Private Sub frmMain_FormClosing(sender As Object, e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        If (MessageBox.Show(Statement.ExitApplication, Caption.Title, MessageBoxButtons.YesNo, MessageBoxIcon.Question)) = Windows.Forms.DialogResult.Yes Then
            For Each iLoop In Me.MdiChildren
                iLoop.Close()
            Next

            Environment.Exit(1)
        Else
            e.Cancel = True
        End If
    End Sub
    Private Sub clearTextMenuList()
        'File
        Button1.ResetText()
        Button2.ResetText()
        Button3.ResetText()
        Button4.ResetText()
        Button5.ResetText()
        Button6.ResetText()

        'Admissi
        Button7.ResetText()
        Button8.ResetText()
        Button9.ResetText()
        Button10.ResetText()
        Button11.ResetText()
        Button12.ResetText()
        Button13.ResetText()
        Button14.ResetText()
        Button15.ResetText()
        Button16.ResetText()

        'Dokter
        Button17.ResetText()
        Button18.ResetText()
        Button19.ResetText()
        Button20.ResetText()
        Button21.ResetText()
        Button22.ResetText()
        Button23.ResetText()
        Button24.ResetText()
        Button25.ResetText()
        Button26.ResetText()
        Button27.ResetText()
        Button28.ResetText()
        Button29.ResetText()
        Button30.ResetText()
    End Sub
    Private Sub setTextMenuList()
        'File
        Button1.Text = "File >"
        Button2.Text = "Ubah Kata Sandi"
        Button3.Text = "Log Out"
        Button4.Text = "Exit"
        Button5.Text = ""
        Button6.Text = ""

        'Admissi
        Button7.Text = "Admission >"
        Button8.Text = "Pendaftaran"
        Button9.Text = "SPRI"
        Button10.ResetText()
        Button11.ResetText()
        Button12.ResetText()
        Button13.ResetText()
        Button14.ResetText()
        Button15.ResetText()
        Button16.ResetText()
        Button17.ResetText()

        'Dokter
        Button18.Text = "Dokter >"
        Button19.Text = "IGD"
        Button20.Text = "Rawat Jalan"
        Button21.Text = "Rawat Inap"


        Button22.ResetText()
        Button23.ResetText()
        Button24.ResetText()
        Button25.ResetText()
        Button26.ResetText()
        Button27.ResetText()
        Button28.ResetText()
        Button29.ResetText()
        Button30.ResetText()
    End Sub
    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        If slidingMenu = "open" Then
            SlidingPanel.Width += 25
            ImagePanel.Width += 25
            ImagePanel.Height += 25

            If SlidingPanel.Width >= 250 Then
                setTextMenuList()
                ImagePanel.Visible = True
                slidingMenu = "close"
                Timer1.Stop()
            End If
        Else
            SlidingPanel.Width -= 25
            ImagePanel.Width -= 25
            ImagePanel.Height -= 25

            If SlidingPanel.Width <= 50 Then
                clearTextMenuList()
                ImagePanel.Visible = True
                slidingMenu = "open"
                Timer1.Stop()
            End If
        End If
    End Sub
    Private Sub btnSlidingPanel_Click(sender As Object, e As EventArgs) Handles btnSlidingPanel.Click
        Timer1.Start()
    End Sub
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Button6.Visible = False
        Button5.Visible = False
        Button4.Visible = False
        Button3.Visible = False
        Button2.Visible = False

        If Button1.Text.Contains(">") Then
            Button1.Text = "File v"

            Button4.Visible = True
            Button3.Visible = True
            Button2.Visible = True
        Else
            Button1.Text = "File >"

            Button4.Visible = False
            Button3.Visible = False
            Button2.Visible = False
        End If
    End Sub
    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        Button8.Visible = False
        Button9.Visible = False
        Button10.Visible = False
        Button11.Visible = False
        Button12.Visible = False
        Button13.Visible = False
        Button14.Visible = False
        Button15.Visible = False
        Button16.Visible = False
        Button17.Visible = False

        If Button7.Text.Contains(">") Then
            Button7.Text = "Admission v"

            Button9.Visible = True
            Button8.Visible = True
        Else
            Button7.Text = "Admission >"

            Button9.Visible = False
            Button8.Visible = False
        End If
    End Sub
    Private Sub Button18_Click(sender As Object, e As EventArgs) Handles Button18.Click
        Button19.Visible = False
        Button20.Visible = False
        Button21.Visible = False

        If Button18.Text.Contains(">") Then
            Button18.Text = "Dokter v"

            Button21.Visible = True
            Button20.Visible = True
            Button19.Visible = True
        Else
            Button18.Text = "Dokter >"

            Button21.Visible = False
            Button20.Visible = False
            Button19.Visible = False
        End If
    End Sub
#End Region
#Region "File"
    Private Sub fn_LoadLogin()
        frmLogin.ShowDialog()

        'statusKDUSER.Caption = Caption.User & " : " & sUserID
        'statusDATE.Caption = Caption.Tanggal & " : " & Now.ToString("dd/MM/yyyy")
    End Sub
    Private Sub fn_LoadNameModueldanKoneksi()
        'Try
        '    Dim oNameModul As New Setting.clsModul

        '    Dim ds = oNameModul.GetData("DEMO")

        '    If ds IsNot Nothing Then
        '        sDaftar_L1 = ds.KDDAFTAR_L1
        '        sDaftar_L2 = ds.KDDAFTAR_L2
        '        sDaftar_L2_1 = ds.KDDAFTAR_L2_1
        '        sDaftar_L2_2 = ds.KDDAFTAR_L2_2
        '        sDaftar_L3 = ds.KDDAFTAR_L3
        '        sDaftar_L4 = ds.KDDAFTAR_L4
        '        sDaftar_L5 = ds.KDDAFTAR_L5
        '        sDaftar_L6 = ds.KDDAFTAR_L6

        '        sItem_L1 = ds.KDITEM_L1
        '        sItem_L2 = ds.KDITEM_L2
        '        sItem_L3 = ds.KDITEM_L3
        '        sItem_L4 = ds.KDITEM_L4
        '        sItem_L5 = ds.KDITEM_L5
        '        sItem_L6 = ds.KDITEM_L6
        '        sItem_L7 = ds.KDITEM_L7
        '    End If

        '    Dim oSetkoneksi As New Setting.clsBPJSKoneksi
        '    Dim dsSetKoneksiVClaim = oSetkoneksi.GetData("VCLAIM")

        '    If dsSetKoneksiVClaim IsNot Nothing Then
        '        sPPK_Pelayanan = dsSetKoneksiVClaim.PPKPELAYANAN
        '        sUrlVclaim = dsSetKoneksiVClaim.ALAMATWEB
        '        sConsidVclaim = dsSetKoneksiVClaim.CONSID
        '        sSecreateKeyVclaim = dsSetKoneksiVClaim.SECREATKEY
        '        sUserKeyVclaim = dsSetKoneksiVClaim.REMARKS
        '    End If

        '    Dim dsSetKoneksiAntrean = oSetkoneksi.GetData("ANTREAN")

        '    If dsSetKoneksiAntrean IsNot Nothing Then
        '        sUrlAntrean = dsSetKoneksiAntrean.ALAMATWEB
        '        sConsidAntrean = dsSetKoneksiAntrean.CONSID
        '        sSecreateKeyAntrean = dsSetKoneksiAntrean.SECREATKEY
        '        sUserKeyAntrean = dsSetKoneksiAntrean.REMARKS
        '    End If

        '    Dim dsSetKoneksiAplicare = oSetkoneksi.GetData("APLICARE")

        '    If dsSetKoneksiAplicare IsNot Nothing Then
        '        sUrlAplicare = dsSetKoneksiAplicare.ALAMATWEB
        '        sConsidAplicare = dsSetKoneksiAplicare.CONSID
        '        sSecreateKeyAplicare = dsSetKoneksiAplicare.SECREATKEY
        '        sUserKeyAplicare = dsSetKoneksiAplicare.REMARKS
        '    End If

        '    Dim dsSetKoneksiSIMRSOLD = oSetkoneksi.GetData("SIMRS_OLD")

        '    If dsSetKoneksiSIMRSOLD IsNot Nothing Then
        '        sAlamatSuara = dsSetKoneksiSIMRSOLD.REMARKS
        '    End If

        '    Dim dsSetKoneksiTTDDokter = oSetkoneksi.GetData("FOLDER_TTDDOKTER")

        '    If dsSetKoneksiTTDDokter IsNot Nothing Then
        '        sAlamatTandaTanganDokter = dsSetKoneksiTTDDokter.ALAMATWEB
        '    End If

        '    Dim dsSetKoneksiTTDPerawat = oSetkoneksi.GetData("FOLDER_TTDPERAWAT")

        '    If dsSetKoneksiTTDPerawat IsNot Nothing Then
        '        sAlamatTandaTanganPerawat = dsSetKoneksiTTDPerawat.ALAMATWEB
        '    End If

        '    Dim dsSetKoneksiPDFLaboratorium = oSetkoneksi.GetData("FOLDER_LABORATORIUM")

        '    If dsSetKoneksiPDFLaboratorium IsNot Nothing Then
        '        sAlamatSimpanPDFLaboratorium = dsSetKoneksiPDFLaboratorium.ALAMATWEB
        '    End If

        '    Dim dsSetKoneksiEclaim = oSetkoneksi.GetData("ECLAIM")

        '    If dsSetKoneksiEclaim IsNot Nothing Then
        '        sKodeTarifEClaim = dsSetKoneksiEclaim.PPKPELAYANAN

        '    End If

        '    Dim dsSetKoneksiRadiologi = oSetkoneksi.GetData("BRIGGING_RADIOLOGI")

        '    If dsSetKoneksiRadiologi IsNot Nothing Then
        '        sAlamatBriggingRadiologi = dsSetKoneksiRadiologi.ALAMATWEB
        '    End If

        '    Dim dsSetKoneksiAsesmenAwalIGD = oSetkoneksi.GetData("FOLDER_ASESMENIGD")

        '    If dsSetKoneksiAsesmenAwalIGD IsNot Nothing Then
        '        sAlamatSimpanAsesmenIGD = dsSetKoneksiAsesmenAwalIGD.ALAMATWEB
        '    End If

        '    Dim dsSetKoneksiAsesmenAwalIGDSharing = oSetkoneksi.GetData("SHARING_ASESMENMEDISIGD")

        '    If dsSetKoneksiAsesmenAwalIGDSharing IsNot Nothing Then
        '        sAlamatSimpanPDFAsesmenIGD = dsSetKoneksiAsesmenAwalIGDSharing.ALAMATWEB
        '    End If

        '    Dim dsSetKoneksiSimpanGambar = oSetkoneksi.GetData("SIMPANGAMBAR")

        '    If dsSetKoneksiSimpanGambar IsNot Nothing Then
        '        sSimpanGambar = dsSetKoneksiSimpanGambar.ALAMATWEB
        '    End If

        'Catch ex As Exception

        'End Try
    End Sub
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        frmChangePassword.ShowDialog()
    End Sub
    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        fn_LoadLogin()
    End Sub
    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Application.Exit()
    End Sub

#End Region
#Region "Admission"
    Private Sub Button19_Click(sender As Object, e As EventArgs) Handles Button19.Click
        'Dim frmMedrekRawatNewJalanList As New frmMedrekRawatNewJalanList
        'Try
        '    frmMedrekRawatNewJalanList.fn_LoadMe(True)
        '    frmMedrekRawatNewJalanList.ShowDialog(Me)
        'Catch ex As Exception
        '    MsgBox("Load Form Detail : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
        'Finally
        '    If Not frmEMedrekIDG_03 Is Nothing Then frmTransferInternalList.Dispose()
        '    frmTransferInternalList = Nothing

        'End Try
    End Sub
#End Region
#Region "Dokter"

#End Region
#Region "Validasi"
#End Region
End Class