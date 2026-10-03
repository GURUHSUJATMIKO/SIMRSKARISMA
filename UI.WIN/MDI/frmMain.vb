Imports DataAccess
Imports UI.WIN.MAIN.My.Resources
Imports System.Linq

Public Class frmMain
#Region "Declaration"
    Private isRestart As Boolean = False

#End Region
#Region "Function"
    Private Sub frmMain_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        'frmDashboard.MdiParent = Me
        'frmDashboard.Show()

        frmDashboard_.MdiParent = Me
        frmDashboard_.Show()

        fn_LoadLanguage("id")
        fn_LoadLogin()
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
    Private Sub fn_LoadLogin()
        frmLogin.ShowDialog()

        statusKDUSER.Caption = Caption.User & " : " & sUserID & " Version 267"
        statusDATE.Caption = Caption.Tanggal & " : " & Now.ToString("dd/MM/yyyy")

        'If isMedrek = False Then
        '    mnuEMedrek.Visibility = DevExpress.XtraBars.BarItemVisibility.Never

        '    mnuReference.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
        '    mnuAdmission.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
        '    mnuRekamMedis.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
        '    mnuTransaksi.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
        '    mnuPenunjang.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
        '    mnuBillingIstalasiFarmasi.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
        '    mnuKasir.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
        '    mnuEklaim.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
        '    mnuSetting.Visibility = DevExpress.XtraBars.BarItemVisibility.Always

        'Else
        '    mnuEMedrek.Visibility = DevExpress.XtraBars.BarItemVisibility.Always

        '    mnuReference.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
        '    mnuAdmission.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
        '    mnuRekamMedis.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
        '    mnuTransaksi.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
        '    mnuPenunjang.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
        '    mnuBillingIstalasiFarmasi.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
        '    mnuKasir.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
        '    mnuEklaim.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
        '    mnuSetting.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
        'End If
    End Sub
    Private Sub fn_LoadLanguage(ByVal sCulture As String)
        Try
            If sCulture = "id" Then
                'System.Threading.Thread.CurrentThread.CurrentUICulture = System.Globalization.CultureInfo.GetCultureInfo("id-ID")
                System.Threading.Thread.CurrentThread.CurrentUICulture = New System.Globalization.CultureInfo("id-ID")

                'statusLANGUAGE.Caption = "Bahasa : Indonesia"
            Else
                System.Threading.Thread.CurrentThread.CurrentUICulture = System.Globalization.CultureInfo.InstalledUICulture

                'statusLANGUAGE.Caption = "Language : English"
            End If

            statusLANGUAGE.Caption = IIf(sCPPT_QRRJ = False, "No QR", "QR")

            My.Settings.Save()

            For Each iLoop In Me.MdiChildren
                Dim child As ILanguage = TryCast(iLoop, ILanguage)

                If child IsNot Nothing Then
                    child.fn_LoadLanguage()
                End If
            Next
        Catch oErr As Exception

        End Try

        mnuFileLanguage.Caption = Caption.FileLanguage

        'File
        mnuFileLanguage.Caption = Caption.FileLanguage
        mnuFileLanguageIndonesian.Caption = Caption.FileLanguageIndonesian
        mnuFileLanguageEnglish.Caption = Caption.FileLanguageEnglish
        mnuFileExit.Caption = Caption.FileExit
        mnuFileChangePassword.Caption = Caption.FileChangePassword

        'Reference
        mnuReference.Caption = Caption.Reference
        mnuReferenceDepartment.Caption = Caption.ReferenceDepartment
        mnuReferenceDiagnosa.Caption = Caption.ReferenceDiagnosa
        mnuReferenceProsedur.Caption = Caption.ReferenceProcedure
        mnuReferenceDokter.Caption = Caption.ReferenceDoctor
        mnuReferencePPK.Caption = Caption.ReferencePPK
        mnuReferenceSpesialistik.Caption = Caption.ReferenceSpesialistik
        mnuReferenceKelasRawat.Caption = Caption.ReferenceKelasRawat
        mnuReferencePascaPulang.Caption = Caption.ReferencePascaPulang
        mnuReferenceCaraKeluar.Caption = Caption.ReferenceCaraKeluar
        mnuReferencePropinsi.Caption = Caption.ReferencePropinsi
        mnuReferenceKabupaten.Caption = Caption.ReferenceKabupaten
        mnuReferenceKecamatan.Caption = Caption.ReferenceKecamatan
        mnuReferenceKelurahan.Caption = Caption.ReferenceKelurahan
        mnuReferenceAgama.Caption = Caption.ReferenceAgama
        mnuReferenceSuku.Caption = Caption.ReferenceSuku
        mnuReferenceCustomer.Caption = Caption.ReferenceCustomer
        mnuReferencePenjamin.Caption = Caption.ReferencePenjamin
        mnuReferenceDaftar_1.Caption = sDaftar_L1
        mnuReferenceDaftar_2.Caption = sDaftar_L2
        mnuReferenceDaftar_3.Caption = sDaftar_L3
        mnuReferenceDaftar_4.Caption = sDaftar_L4
        mnuReferenceDaftar_5.Caption = sDaftar_L5
        mnuReferenceDaftar_6.Caption = sDaftar_L6
        mnuReferenceItemNew_L1.Caption = sItem_L1
        mnuReferenceItemNew_2.Caption = sItem_L2
        mnuReferenceItemNew_3.Caption = sItem_L3
        mnuReferenceItemNew_4.Caption = sItem_L4
        mnuReferenceItemNew_5.Caption = sItem_L5
        mnuReferenceItemNew_6.Caption = sItem_L6
        mnuReferenceItemNew_7.Caption = sItem_L7
        mnuReferenceDiagnosa_PRB.Caption = Caption.ReferenceDiagnosa_PRB
        mnuReferenceRuangRawat.Caption = Caption.ReferenceRuangRawat

        'Purchasing
        mnuPurchasing.Caption = Caption.Purchasing
        mnuPurchasingPurchaseOrder.Caption = Caption.PurchasingPurchaseOrder
        mnuPurchasingPurchaseInvoice.Caption = Caption.PurchasingPurchaseInvoice
        mnuPurchasingPurchaseReturn.Caption = Caption.PurchasingPurchaseReturn
        mnuPurchasingReport.Caption = Caption.PurchasingReport
        mnuPurchasingReportPurchaseOrder.Caption = Caption.PurchasingPurchaseOrder
        mnuPurchasingReportPurchaseInvoice.Caption = Caption.PurchasingPurchaseInvoice
        mnuPurchasingReportPurchaseReturn.Caption = Caption.PurchasingPurchaseReturn

        'Inventory
        mnuInventory.Caption = Caption.Inventory
        mnuInventoryMutation.Caption = Caption.InventoryMutation
        mnuInventoryOpname.Caption = Caption.InventoryOpname
        mnuInventoryReport.Caption = Caption.InventoryReport
        mnuInventoryReportMutation.Caption = Caption.InventoryMutation
        mnuInventoryReportOpname.Caption = Caption.InventoryOpname
        mnuInventoryReportMonitoringItem.Caption = Caption.InventoryReportMonitoringItem

        'Admissioon
        mnuAdmission.Caption = Caption.Admission
        mnuAdmissionPendaftaran.Caption = Caption.AdmissionPendaftaran
        mnuAdmissionSKDP.Caption = Caption.AdmissionSKD
        mnuAdmissionApprovalPenajminSEP.Caption = Caption.AdmissionApprovalPenjaminanSEP
        mnuAdmissionUpdateTanggalPulang.Caption = Caption.AdmissionUpdateTanggalPulang
        mnuAdmissionRujukan.Caption = Caption.AdmissionRujukan
        mnuAdmissionKunjunganPoli.Caption = Caption.AdmissionPendaftaranKunjunganPoli
        mnuAdmissionKunjunganRawat.Caption = Caption.AdmissionPendaftaranKunjunganRuangan

    End Sub
    Private Sub mnuFileLogOut_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuFileLogOut.ItemClick
        For Each iLoop In Me.MdiChildren
            iLoop.Close()
        Next

        'frmDashboard.MdiParent = Me
        'frmDashboard.Show()

        frmDashboard_.MdiParent = Me
        frmDashboard_.Show()
        fn_LoadLogin()
    End Sub
#End Region
#Region "File"
    Private Sub mnuFileLanguageIndonesian_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuFileLanguageIndonesian.ItemClick
        fn_LoadLanguage("id")
    End Sub
    Private Sub mnuFileLanguageEnglish_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuFileLanguageEnglish.ItemClick
        fn_LoadLanguage("en")
    End Sub
    Private Sub mnuFileExit_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuFileExit.ItemClick
        isRestart = False
        Application.Exit()
    End Sub
#End Region
    '#Region "Reference"
    Private Sub mnuReferenceDepartment_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuReferenceDepartment.ItemClick
        For Each iLoop In Me.MdiChildren
            If iLoop.Name = frmDepartmentList.Name Then
                iLoop.Activate()
                Exit Sub
            End If
        Next

        frmDepartmentList.MdiParent = Me
        frmDepartmentList.Show()
    End Sub
    '    Private Sub mnuReferenceDiagnosa_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuReferenceDiagnosa.ItemClick
    '        For Each iLoop In Me.MdiChildren
    '            If iLoop.Name = frmDiagnosaList.Name Then
    '                iLoop.Activate()
    '                Exit Sub
    '            End If
    '        Next

    '        frmDiagnosaList.MdiParent = Me
    '        frmDiagnosaList.Show()
    '    End Sub
    '    Private Sub mnuReferenceProsedur_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuReferenceProsedur.ItemClick
    '        For Each iLoop In Me.MdiChildren
    '            If iLoop.Name = frmProsedurList.Name Then
    '                iLoop.Activate()
    '                Exit Sub
    '            End If
    '        Next

    '        frmProsedurList.MdiParent = Me
    '        frmProsedurList.Show()
    '    End Sub
    '    Private Sub mnuReferenceSpesialistik_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuReferenceSpesialistik.ItemClick
    '        For Each iLoop In Me.MdiChildren
    '            If iLoop.Name = frmSpesialistikList.Name Then
    '                iLoop.Activate()
    '                Exit Sub
    '            End If
    '        Next

    '        frmSpesialistikList.MdiParent = Me
    '        frmSpesialistikList.Show()
    '    End Sub
    '    Private Sub mnuReferenceRuangRawat_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuReferenceRuangRawat.ItemClick
    '        For Each iLoop In Me.MdiChildren
    '            If iLoop.Name = frmRuangRawatList.Name Then
    '                iLoop.Activate()
    '                Exit Sub
    '            End If
    '        Next

    '        frmRuangRawatList.MdiParent = Me
    '        frmRuangRawatList.Show()
    '    End Sub
    Private Sub mnuReferenceDokter_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuReferenceDokter.ItemClick
        For Each iLoop In Me.MdiChildren
            If iLoop.Name = frmDoctorList.Name Then
                iLoop.Activate()
                Exit Sub
            End If
        Next

        frmDoctorList.MdiParent = Me
        frmDoctorList.Show()
    End Sub
    '    Private Sub mnuReferenceDokterLuar_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuReferenceDokterLuar.ItemClick
    '        For Each iLoop In Me.MdiChildren
    '            If iLoop.Name = frmDoctorLuarList.Name Then
    '                iLoop.Activate()
    '                Exit Sub
    '            End If
    '        Next

    '        frmDoctorLuarList.MdiParent = Me
    '        frmDoctorLuarList.Show()
    '    End Sub
    '    Private Sub mnuReferencePPK_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuReferencePPK.ItemClick
    '        For Each iLoop In Me.MdiChildren
    '            If iLoop.Name = frmPPKList.Name Then
    '                iLoop.Activate()
    '                Exit Sub
    '            End If
    '        Next

    '        frmPPKList.MdiParent = Me
    '        frmPPKList.Show()
    '    End Sub
    '    Private Sub mnuReferenceKelasRawat_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuReferenceKelasRawat.ItemClick
    '        For Each iLoop In Me.MdiChildren
    '            If iLoop.Name = frmKelasRawatList.Name Then
    '                iLoop.Activate()
    '                Exit Sub
    '            End If
    '        Next

    '        frmKelasRawatList.MdiParent = Me
    '        frmKelasRawatList.Show()
    '    End Sub
    '    Private Sub mnuReferencePascaPulang_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuReferencePascaPulang.ItemClick
    '        For Each iLoop In Me.MdiChildren
    '            If iLoop.Name = frmPascaPulangList.Name Then
    '                iLoop.Activate()
    '                Exit Sub
    '            End If
    '        Next

    '        frmPascaPulangList.MdiParent = Me
    '        frmPascaPulangList.Show()
    '    End Sub
    '    Private Sub mnuReferenceCaraKeluar_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuReferenceCaraKeluar.ItemClick
    '        For Each iLoop In Me.MdiChildren
    '            If iLoop.Name = frmCaraKeluarList.Name Then
    '                iLoop.Activate()
    '                Exit Sub
    '            End If
    '        Next

    '        frmCaraKeluarList.MdiParent = Me
    '        frmCaraKeluarList.Show()
    '    End Sub
    '    Private Sub mnuReferencePropinsi_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuReferencePropinsi.ItemClick
    '        For Each iLoop In Me.MdiChildren
    '            If iLoop.Name = frmPropinsiList.Name Then
    '                iLoop.Activate()
    '                Exit Sub
    '            End If
    '        Next

    '        frmPropinsiList.MdiParent = Me
    '        frmPropinsiList.Show()
    '    End Sub
    '    Private Sub mnuReferenceKabupaten_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuReferenceKabupaten.ItemClick
    '        For Each iLoop In Me.MdiChildren
    '            If iLoop.Name = frmKabupatenList.Name Then
    '                iLoop.Activate()
    '                Exit Sub
    '            End If
    '        Next

    '        frmKabupatenList.MdiParent = Me
    '        frmKabupatenList.Show()
    '    End Sub
    '    Private Sub mnuReferenceKecamatan_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuReferenceKecamatan.ItemClick
    '        For Each iLoop In Me.MdiChildren
    '            If iLoop.Name = frmKecamatanList.Name Then
    '                iLoop.Activate()
    '                Exit Sub
    '            End If
    '        Next

    '        frmKecamatanList.MdiParent = Me
    '        frmKecamatanList.Show()
    '    End Sub
    '    Private Sub mnuReferenceKelurahan_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuReferenceKelurahan.ItemClick
    '        For Each iLoop In Me.MdiChildren
    '            If iLoop.Name = frmKelurahanList.Name Then
    '                iLoop.Activate()
    '                Exit Sub
    '            End If
    '        Next

    '        frmKelurahanList.MdiParent = Me
    '        frmKelurahanList.Show()
    '    End Sub
    '    Private Sub mnuReferenceAgama_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuReferenceAgama.ItemClick
    '        For Each iLoop In Me.MdiChildren
    '            If iLoop.Name = frmAgamaList.Name Then
    '                iLoop.Activate()
    '                Exit Sub
    '            End If
    '        Next

    '        frmAgamaList.MdiParent = Me
    '        frmAgamaList.Show()
    '    End Sub
    '    Private Sub mnuReferenceSuku_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuReferenceSuku.ItemClick
    '        For Each iLoop In Me.MdiChildren
    '            If iLoop.Name = frmSukuList.Name Then
    '                iLoop.Activate()
    '                Exit Sub
    '            End If
    '        Next

    '        frmSukuList.MdiParent = Me
    '        frmSukuList.Show()
    '    End Sub
    '    Private Sub mnuReferenceCustomer_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuReferenceCustomer.ItemClick
    '        'For Each iLoop In Me.MdiChildren
    '        '    If iLoop.Name = frmCustomerList.Name Then
    '        '        iLoop.Activate()
    '        '        Exit Sub
    '        '    End If
    '        'Next

    '        'frmCustomerList.MdiParent = Me
    '        'frmCustomerList.Show()
    '    End Sub
    '    Private Sub mnuReferenceCustomerUnit_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuReferenceCustomerUnit.ItemClick
    '        For Each iLoop In Me.MdiChildren
    '            If iLoop.Name = frmCustomerUnitList.Name Then
    '                iLoop.Activate()
    '                Exit Sub
    '            End If
    '        Next

    '        frmCustomerUnitList.MdiParent = Me
    '        frmCustomerUnitList.Show()
    '    End Sub
    '    Private Sub mnuReferenceHubungan_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuReferenceHubungan.ItemClick
    '        For Each iLoop In Me.MdiChildren
    '            If iLoop.Name = frmHubunganList.Name Then
    '                iLoop.Activate()
    '                Exit Sub
    '            End If
    '        Next

    '        frmHubunganList.MdiParent = Me
    '        frmHubunganList.Show()
    '    End Sub
    '    Private Sub mnuPenjamin_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuPenjamin.ItemClick
    '        For Each iLoop In Me.MdiChildren
    '            If iLoop.Name = frmPenjaminList.Name Then
    '                iLoop.Activate()
    '                Exit Sub
    '            End If
    '        Next

    '        frmPenjaminList.MdiParent = Me
    '        frmPenjaminList.Show()
    '    End Sub
    '    Private Sub mnuReferenceDaftar_1_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuReferenceDaftar_1.ItemClick
    '        For Each iLoop In Me.MdiChildren
    '            If iLoop.Name = frmDaftar_L1List.Name Then
    '                iLoop.Activate()
    '                Exit Sub
    '            End If
    '        Next

    '        frmDaftar_L1List.MdiParent = Me
    '        frmDaftar_L1List.Show()
    '    End Sub
    '    Private Sub mnuReferenceDaftar_2_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuReferenceDaftar_2.ItemClick
    '        For Each iLoop In Me.MdiChildren
    '            If iLoop.Name = frmDaftar_L2List.Name Then
    '                iLoop.Activate()
    '                Exit Sub
    '            End If
    '        Next

    '        frmDaftar_L2List.MdiParent = Me
    '        frmDaftar_L2List.Show()
    '    End Sub
    '    Private Sub mnuReferenceDaftar_3_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuReferenceDaftar_3.ItemClick
    '        For Each iLoop In Me.MdiChildren
    '            If iLoop.Name = frmDaftar_L3List.Name Then
    '                iLoop.Activate()
    '                Exit Sub
    '            End If
    '        Next

    '        frmDaftar_L3List.MdiParent = Me
    '        frmDaftar_L3List.Show()
    '    End Sub
    '    Private Sub mnuReferenceDaftar_4_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuReferenceDaftar_4.ItemClick
    '        For Each iLoop In Me.MdiChildren
    '            If iLoop.Name = frmDaftar_L4List.Name Then
    '                iLoop.Activate()
    '                Exit Sub
    '            End If
    '        Next

    '        frmDaftar_L4List.MdiParent = Me
    '        frmDaftar_L4List.Show()
    '    End Sub
    '    Private Sub mnuReferenceDaftar_5_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuReferenceDaftar_5.ItemClick
    '        For Each iLoop In Me.MdiChildren
    '            If iLoop.Name = frmDaftar_L5List.Name Then
    '                iLoop.Activate()
    '                Exit Sub
    '            End If
    '        Next

    '        frmDaftar_L5List.MdiParent = Me
    '        frmDaftar_L5List.Show()
    '    End Sub
    '    Private Sub mnuReferenceDaftar_6_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuReferenceDaftar_6.ItemClick
    '        For Each iLoop In Me.MdiChildren
    '            If iLoop.Name = frmDaftar_L6List.Name Then
    '                iLoop.Activate()
    '                Exit Sub
    '            End If
    '        Next

    '        frmDaftar_L6List.MdiParent = Me
    '        frmDaftar_L6List.Show()
    '    End Sub
    '    Private Sub mnuReferencePerusahaan__ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuReferencePerusahaan_.ItemClick
    '        For Each iLoop In Me.MdiChildren
    '            If iLoop.Name = frmPerusaanList.Name Then
    '                iLoop.Activate()
    '                Exit Sub
    '            End If
    '        Next

    '        frmPerusaanList.MdiParent = Me
    '        frmPerusaanList.Show()
    '    End Sub
    '    Private Sub mnuReferenceItemTarif_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuReferenceItemTarif.ItemClick
    '        For Each iLoop In Me.MdiChildren
    '            If iLoop.Name = frmItemList.Name Then
    '                iLoop.Close()
    '            End If
    '        Next
    '        For Each iLoop In Me.MdiChildren
    '            If iLoop.Name = frmItemList.Name Then
    '                iLoop.Activate()
    '                Exit Sub
    '            End If
    '        Next

    '        frmItemList.fn_LoadType("TARIF")
    '        frmItemList.MdiParent = Me
    '        frmItemList.Show()
    '    End Sub
    '    Private Sub mnuReferenceObat_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuReferenceObat.ItemClick
    '        For Each iLoop In Me.MdiChildren
    '            If iLoop.Name = frmItemList.Name Then
    '                iLoop.Close()
    '            End If
    '        Next
    '        For Each iLoop In Me.MdiChildren
    '            If iLoop.Name = frmItemList.Name Then
    '                iLoop.Activate()
    '                Exit Sub
    '            End If
    '        Next

    '        frmItemList.fn_LoadType("OBAT")
    '        frmItemList.MdiParent = Me
    '        frmItemList.Show()
    '    End Sub
    '    Private Sub mnuReferenceSatuan_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuReferenceSatuan.ItemClick
    '        For Each iLoop In Me.MdiChildren
    '            If iLoop.Name = frmUOMList.Name Then
    '                iLoop.Activate()
    '                Exit Sub
    '            End If
    '        Next

    '        frmUOMList.MdiParent = Me
    '        frmUOMList.Show()
    '    End Sub
    '    Private Sub mnuReferenceSigna_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuReferenceSigna.ItemClick
    '        For Each iLoop In Me.MdiChildren
    '            If iLoop.Name = frmSignaList.Name Then
    '                iLoop.Activate()
    '                Exit Sub
    '            End If
    '        Next

    '        frmSignaList.MdiParent = Me
    '        frmSignaList.Show()
    '    End Sub
    '    Private Sub mnuReferenceCaraPakai_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuReferenceCaraPakai.ItemClick
    '        For Each iLoop In Me.MdiChildren
    '            If iLoop.Name = frmCaraPakaiList.Name Then
    '                iLoop.Activate()
    '                Exit Sub
    '            End If
    '        Next

    '        frmCaraPakaiList.MdiParent = Me
    '        frmCaraPakaiList.Show()
    '    End Sub
    '    Private Sub mnuReferenceItemNew_L1_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuReferenceItemNew_L1.ItemClick
    '        For Each iLoop In Me.MdiChildren
    '            If iLoop.Name = frmItem_L1List.Name Then
    '                iLoop.Activate()
    '                Exit Sub
    '            End If
    '        Next

    '        frmItem_L1List.MdiParent = Me
    '        frmItem_L1List.Show()
    '    End Sub
    '    Private Sub mnuReferenceItemNew_2_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuReferenceItemNew_2.ItemClick
    '        For Each iLoop In Me.MdiChildren
    '            If iLoop.Name = frmItem_L2List.Name Then
    '                iLoop.Activate()
    '                Exit Sub
    '            End If
    '        Next

    '        frmItem_L2List.MdiParent = Me
    '        frmItem_L2List.Show()
    '    End Sub
    '    Private Sub mnuReferenceItemNew_3_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuReferenceItemNew_3.ItemClick
    '        For Each iLoop In Me.MdiChildren
    '            If iLoop.Name = frmItem_L3List.Name Then
    '                iLoop.Activate()
    '                Exit Sub
    '            End If
    '        Next

    '        frmItem_L3List.MdiParent = Me
    '        frmItem_L3List.Show()
    '    End Sub
    '    Private Sub mnuReferenceItemNew_4_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuReferenceItemNew_4.ItemClick
    '        For Each iLoop In Me.MdiChildren
    '            If iLoop.Name = frmItem_L4List.Name Then
    '                iLoop.Activate()
    '                Exit Sub
    '            End If
    '        Next

    '        frmItem_L4List.MdiParent = Me
    '        frmItem_L4List.Show()
    '    End Sub
    '    Private Sub mnuReferenceItemNew_5_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuReferenceItemNew_5.ItemClick
    '        For Each iLoop In Me.MdiChildren
    '            If iLoop.Name = frmItem_L5List.Name Then
    '                iLoop.Activate()
    '                Exit Sub
    '            End If
    '        Next

    '        frmItem_L5List.MdiParent = Me
    '        frmItem_L5List.Show()
    '    End Sub
    '    Private Sub mnuReferenceItemNew_6_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuReferenceItemNew_6.ItemClick
    '        For Each iLoop In Me.MdiChildren
    '            If iLoop.Name = frmItem_L6List.Name Then
    '                iLoop.Activate()
    '                Exit Sub
    '            End If
    '        Next

    '        frmItem_L6List.MdiParent = Me
    '        frmItem_L6List.Show()
    '    End Sub
    '    Private Sub mnuReferenceItemNew_7_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuReferenceItemNew_7.ItemClick
    '        For Each iLoop In Me.MdiChildren
    '            If iLoop.Name = frmItem_L7List.Name Then
    '                iLoop.Activate()
    '                Exit Sub
    '            End If
    '        Next

    '        frmItem_L7List.MdiParent = Me
    '        frmItem_L7List.Show()
    '    End Sub
    '    Private Sub mnuReferenceCaraBayar_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuReferenceCaraBayar.ItemClick
    '        For Each iLoop In Me.MdiChildren
    '            If iLoop.Name = frmCaraBayarList.Name Then
    '                iLoop.Activate()
    '                Exit Sub
    '            End If
    '        Next

    '        frmCaraBayarList.MdiParent = Me
    '        frmCaraBayarList.Show()
    '    End Sub
    '    Private Sub mnuReferenceTypePembayaran_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuReferenceTypePembayaran.ItemClick
    '        For Each iLoop In Me.MdiChildren
    '            If iLoop.Name = frmPaymentTypeList.Name Then
    '                iLoop.Activate()
    '                Exit Sub
    '            End If
    '        Next

    '        frmPaymentTypeList.MdiParent = Me
    '        frmPaymentTypeList.Show()
    '    End Sub
    '    Private Sub mnuReferenceDiagnosa_PRB_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuReferenceDiagnosa_PRB.ItemClick
    '        For Each iLoop In Me.MdiChildren
    '            If iLoop.Name = frmDiagnosa_PRBList.Name Then
    '                iLoop.Activate()
    '                Exit Sub
    '            End If
    '        Next

    '        frmDiagnosa_PRBList.MdiParent = Me
    '        frmDiagnosa_PRBList.Show()
    '    End Sub
    '    Private Sub mnuReferenceObatGenerikPRB_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuReferenceObatGenerikPRB.ItemClick
    '        For Each iLoop In Me.MdiChildren
    '            If iLoop.Name = frmObatGenerikProgramPRB.Name Then
    '                iLoop.Activate()
    '                Exit Sub
    '            End If
    '        Next

    '        frmObatGenerikProgramPRB.MdiParent = Me
    '        frmObatGenerikProgramPRB.Show()
    '    End Sub
    '    Private Sub btnReferencePabrik_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btnReferencePabrik.ItemClick
    '        For Each iLoop In Me.MdiChildren
    '            If iLoop.Name = frmProducerList.Name Then
    '                iLoop.Activate()
    '                Exit Sub
    '            End If
    '        Next

    '        frmProducerList.MdiParent = Me
    '        frmProducerList.Show()
    '    End Sub
    '    Private Sub mnuReferenceVendor_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuReferenceVendor.ItemClick
    '        For Each iLoop In Me.MdiChildren
    '            If iLoop.Name = frmVendorList.Name Then
    '                iLoop.Activate()
    '                Exit Sub
    '            End If
    '        Next

    '        frmVendorList.MdiParent = Me
    '        frmVendorList.Show()
    '    End Sub
    '    Private Sub mnuReferenceWarehouse_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuReferenceWarehouse.ItemClick
    '        For Each iLoop In Me.MdiChildren
    '            If iLoop.Name = frmWarehouseList.Name Then
    '                iLoop.Activate()
    '                Exit Sub
    '            End If
    '        Next

    '        frmWarehouseList.MdiParent = Me
    '        frmWarehouseList.Show()
    '    End Sub
    Private Sub mnuReferenceUnit__ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuReferenceUnit_.ItemClick
        For Each iLoop In Me.MdiChildren
            If iLoop.Name = frmUnitList.Name Then
                iLoop.Activate()
                Exit Sub
            End If
        Next

        frmUnitList.MdiParent = Me
        frmUnitList.Show()
    End Sub
    '    Private Sub mnuReferenceUnitKasir_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuReferenceUnitKasir.ItemClick
    '        For Each iLoop In Me.MdiChildren
    '            If iLoop.Name = frmUnitKasirList.Name Then
    '                iLoop.Activate()
    '                Exit Sub
    '            End If
    '        Next

    '        frmUnitKasirList.MdiParent = Me
    '        frmUnitKasirList.Show()
    '    End Sub
    '#End Region
    '#Region "Admission"
    '    Private Sub mnuAdmissionPendaftaran_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuAdmissionPendaftaran.ItemClick
    '        For Each iLoop In Me.MdiChildren
    '            If iLoop.Name = frmPendaftaranList.Name Then
    '                iLoop.Activate()
    '                Exit Sub
    '            End If
    '        Next

    '        frmPendaftaranList.MdiParent = Me
    '        frmPendaftaranList.Show()
    '    End Sub
    '    Private Sub mnuAdmissionKunjunganPoli_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuAdmissionKunjunganPoli.ItemClick
    '        For Each iLoop In Me.MdiChildren
    '            If iLoop.Name = frmPendaftaran_KunjunganPoliList.Name Then
    '                iLoop.Activate()
    '                Exit Sub
    '            End If
    '        Next

    '        frmPendaftaran_KunjunganPoliList.MdiParent = Me
    '        frmPendaftaran_KunjunganPoliList.Show()
    '    End Sub
    '    Private Sub mnuAdmissionKunjunganRawat_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuAdmissionKunjunganRawat.ItemClick
    '        For Each iLoop In Me.MdiChildren
    '            If iLoop.Name = frmPendaftaran_KunjunganRuanganList.Name Then
    '                iLoop.Activate()
    '                Exit Sub
    '            End If
    '        Next

    '        frmPendaftaran_KunjunganRuanganList.MdiParent = Me
    '        frmPendaftaran_KunjunganRuanganList.Show()
    '    End Sub
    '    Private Sub mnuAdmissionSKDP_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuAdmissionSKDP.ItemClick
    '        For Each iLoop In Me.MdiChildren
    '            If iLoop.Name = frmSKDList.Name Then
    '                iLoop.Activate()
    '                Exit Sub
    '            End If
    '        Next

    '        frmSKDList.MdiParent = Me
    '        frmSKDList.Show()
    '    End Sub
    '    Private Sub mnuAdmissionPRB_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuAdmissionPRB.ItemClick
    '        For Each iLoop In Me.MdiChildren
    '            If iLoop.Name = frmPendaftaran_PRBList.Name Then
    '                iLoop.Activate()
    '                Exit Sub
    '            End If
    '        Next

    '        frmPendaftaran_PRBList.MdiParent = Me
    '        frmPendaftaran_PRBList.Show()
    '    End Sub
    '    Private Sub mnuAdmissionApprovalPenajminSEP_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuAdmissionApprovalPenajminSEP.ItemClick
    '        For Each iLoop In Me.MdiChildren
    '            If iLoop.Name = frmApproval_Penjaminan_SEPList.Name Then
    '                iLoop.Activate()
    '                Exit Sub
    '            End If
    '        Next

    '        frmApproval_Penjaminan_SEPList.MdiParent = Me
    '        frmApproval_Penjaminan_SEPList.Show()
    '    End Sub
    '    Private Sub mnuAdmissionUpdateTanggalPulang_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuAdmissionUpdateTanggalPulang.ItemClick
    '        For Each iLoop In Me.MdiChildren
    '            If iLoop.Name = frmUpdate_Tanggal_PulangList.Name Then
    '                iLoop.Activate()
    '                Exit Sub
    '            End If
    '        Next

    '        frmUpdate_Tanggal_PulangList.MdiParent = Me
    '        frmUpdate_Tanggal_PulangList.Show()
    '    End Sub
    '    Private Sub mnuAdmissionRujukan_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuAdmissionRujukan.ItemClick
    '        For Each iLoop In Me.MdiChildren
    '            If iLoop.Name = frmRujukanList.Name Then
    '                iLoop.Activate()
    '                Exit Sub
    '            End If
    '        Next

    '        frmRujukanList.MdiParent = Me
    '        frmRujukanList.Show()
    '    End Sub
    '    Private Sub mnuAdmissionLembarPengajuanKlaim_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuAdmissionLembarPengajuanKlaim.ItemClick
    '        For Each iLoop In Me.MdiChildren
    '            If iLoop.Name = frmLPKList.Name Then
    '                iLoop.Activate()
    '                Exit Sub
    '            End If
    '        Next

    '        frmLPKList.MdiParent = Me
    '        frmLPKList.Show()
    '    End Sub
    '    Private Sub mnuAdmissionReportPendaftaran_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuAdmissionReportPendaftaran.ItemClick
    '        For Each iLoop In Me.MdiChildren
    '            If iLoop.Name = frmReportAdmission.Name Then
    '                iLoop.Activate()
    '                Exit Sub
    '            End If
    '        Next

    '        frmReportAdmission.MdiParent = Me
    '        frmReportAdmission.Show()
    '    End Sub
    '    Private Sub mnuAdmissionReportPendaftaranRI_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuAdmissionReportPendaftaranRI.ItemClick
    '        For Each iLoop In Me.MdiChildren
    '            If iLoop.Name = frmReportAdmissionRI.Name Then
    '                iLoop.Activate()
    '                Exit Sub
    '            End If
    '        Next

    '        frmReportAdmissionRI.MdiParent = Me
    '        frmReportAdmissionRI.Show()
    '    End Sub
    '    Private Sub mnuAdmissionReportDataKunjunganBPJS_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuAdmissionReportDataKunjunganBPJS.ItemClick
    '        For Each iLoop In Me.MdiChildren
    '            If iLoop.Name = frmMonitoringBPJS.Name Then
    '                iLoop.Activate()
    '                Exit Sub
    '            End If
    '        Next

    '        frmMonitoringBPJS.MdiParent = Me
    '        frmMonitoringBPJS.Show()
    '    End Sub
    '    Private Sub mnuAdmissionReportIntegrasiSEPInacbg_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuAdmissionReportIntegrasiSEPInacbg.ItemClick
    '        For Each iLoop In Me.MdiChildren
    '            If iLoop.Name = frmIntegrasiSEPdenganInacbg.Name Then
    '                iLoop.Activate()
    '                Exit Sub
    '            End If
    '        Next

    '        frmIntegrasiSEPdenganInacbg.MdiParent = Me
    '        frmIntegrasiSEPdenganInacbg.Show()
    '    End Sub
    '    Private Sub mnuAdmissionLaporanPRB_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuAdmissionLaporanPRB.ItemClick
    '        For Each iLoop In Me.MdiChildren
    '            If iLoop.Name = frmReportPRB.Name Then
    '                iLoop.Activate()
    '                Exit Sub
    '            End If
    '        Next

    '        frmReportPRB.MdiParent = Me
    '        frmReportPRB.Show()
    '    End Sub
    '    Private Sub mnuAdmissionLaporanSKDSPRI_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuAdmissionLaporanSKDSPRI.ItemClick
    '        For Each iLoop In Me.MdiChildren
    '            If iLoop.Name = frmReportRencaKontrol.Name Then
    '                iLoop.Activate()
    '                Exit Sub
    '            End If
    '        Next

    '        frmReportRencaKontrol.MdiParent = Me
    '        frmReportRencaKontrol.Show()
    '    End Sub
    '    Private Sub mnuAdmissionLaporanLPK_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuAdmissionLaporanLPK.ItemClick
    '        For Each iLoop In Me.MdiChildren
    '            If iLoop.Name = frmReportDataLembarPengajuanKlaim.Name Then
    '                iLoop.Activate()
    '                Exit Sub
    '            End If
    '        Next

    '        frmReportDataLembarPengajuanKlaim.MdiParent = Me
    '        frmReportDataLembarPengajuanKlaim.Show()
    '    End Sub

    '#End Region
#Region "Setting"
    Private Sub mnuSettingDatabaseBackup_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuSettingDatabaseBackup.ItemClick
        Dim oConnection As New Setting.clsConnectionMain

        Try
            Dim arrMain() As String = Decrypt(My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Software\SIMRS\SW\", "Database", "").ToString()).Split(";")

            For iLoop As Integer = 0 To arrMain.Length - 1
                Dim arrMainResult() As String = arrMain(iLoop).Split("=")

                For xLoop As Integer = 0 To arrMainResult.Length - 1
                    If arrMainResult(xLoop) = "Initial Catalog" Then
                        If fileDialogSave.ShowDialog() = Windows.Forms.DialogResult.Cancel Then

                        Else
                            If fileDialogSave.FileName <> String.Empty Then
                                Try
                                    If oConnection.BackupData(arrMainResult(xLoop + 1), fileDialogSave.FileName) = True Then
                                        MsgBox(Statement.BackupSuccess, MsgBoxStyle.Information, Caption.Title)
                                    Else
                                        MsgBox(Statement.BackupFail, MsgBoxStyle.Exclamation, Caption.Title)
                                    End If
                                Catch oErr As Exception
                                    MsgBox(Statement.BackupFail, MsgBoxStyle.Exclamation, Caption.Title)
                                End Try
                            End If
                        End If
                    End If
                Next
            Next
        Catch oErr As Exception
            MsgBox(Statement.BackupFail, MsgBoxStyle.Exclamation, Caption.Title)
        End Try
    End Sub
    Private Sub mnuSettingDatabaseRestore_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuSettingDatabaseRestore.ItemClick
        Dim oConnection As New Setting.clsConnectionMain

        Try
            Dim arrMain() As String = Decrypt(My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Software\SIMRS\SW\", "Database", "").ToString()).Split(";")

            For iLoop As Integer = 0 To arrMain.Length - 1
                Dim arrMainResult() As String = arrMain(iLoop).Split("=")

                For xLoop As Integer = 0 To arrMainResult.Length - 1
                    If arrMainResult(xLoop) = "Initial Catalog" Then
                        If fileDialogOpen.ShowDialog() = Windows.Forms.DialogResult.Cancel Then

                        Else
                            If fileDialogOpen.FileName <> String.Empty Then
                                Try
                                    If oConnection.RestoreData(arrMainResult(xLoop + 1), fileDialogOpen.FileName) = True Then
                                        MsgBox(Statement.RestoreSuccess, MsgBoxStyle.Information, Caption.Title)

                                        Application.Exit()
                                    Else
                                        MsgBox(Statement.RestoreFail, MsgBoxStyle.Exclamation, Caption.Title)
                                    End If
                                Catch oErr As Exception
                                    MsgBox(Statement.RestoreFail, MsgBoxStyle.Exclamation, Caption.Title)
                                End Try
                            End If
                        End If
                    End If
                Next
            Next
        Catch oErr As Exception
            MsgBox(Statement.RestoreFail, MsgBoxStyle.Exclamation, Caption.Title)
        End Try
    End Sub
    Private Sub mnuSettingDatabaseConnectionUser_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuSettingDatabaseConnectionUser.ItemClick
        frmDatabaseUser.ShowDialog(Me)
    End Sub
    Private Sub mnuSettingUserOtority_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuSettingUserOtority.ItemClick
        For Each iLoop In Me.MdiChildren
            If iLoop.Name = frmOtorityList.Name Then
                iLoop.Activate()
                Exit Sub
            End If
        Next

        frmOtorityList.MdiParent = Me
        frmOtorityList.Show()
    End Sub
    Private Sub mnuSettingUserUser_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuSettingUserUser.ItemClick
        For Each iLoop In Me.MdiChildren
            If iLoop.Name = frmUserList.Name Then
                iLoop.Activate()
                Exit Sub
            End If
        Next

        frmUserList.MdiParent = Me
        frmUserList.Show()
    End Sub
    Private Sub mnuFileChangePassword_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuFileChangePassword.ItemClick
        frmChangePassword.ShowDialog()
    End Sub
    Private Sub mnuSettingUserKoneksi_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuSettingUserKoneksi.ItemClick
        'For Each iLoop In Me.MdiChildren
        '    If iLoop.Name = frmSetKoneksiList.Name Then
        '        iLoop.Activate()
        '        Exit Sub
        '    End If
        'Next

        'frmSetKoneksiList.MdiParent = Me
        'frmSetKoneksiList.Show()
    End Sub
    Private Sub BarButtonItem49_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem49.ItemClick
        For Each iLoop In Me.MdiChildren
            If iLoop.Name = frmMedrekRawatNew2JalanList.Name Then
                iLoop.Close()
            End If
        Next
        For Each iLoop In Me.MdiChildren
            If iLoop.Name = frmMedrekRawatNew2JalanList.Name Then
                iLoop.Activate()
                Exit Sub
            End If
        Next

        frmMedrekRawatNew2JalanList.fn_LoadMe(True)
        frmMedrekRawatNew2JalanList.MdiParent = Me
        frmMedrekRawatNew2JalanList.Show()
    End Sub
    Private Sub mnuEMedrekRawatJalan_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuEMedrekRawatJalan.ItemClick
        'For Each iLoop In Me.MdiChildren
        '    If iLoop.Name = frmMedrekRawatJalanList.Name Then
        '        iLoop.Close()
        '    End If
        'Next
        'For Each iLoop In Me.MdiChildren
        '    If iLoop.Name = frmMedrekRawatJalanList.Name Then
        '        iLoop.Activate()
        '        Exit Sub
        '    End If
        'Next

        'frmMedrekRawatJalanList.fn_LoadMe(True)
        'frmMedrekRawatJalanList.MdiParent = Me
        'frmMedrekRawatJalanList.Show()

        For Each iLoop In Me.MdiChildren
            If iLoop.Name = frmMedrekRawatNewJalanList.Name Then
                iLoop.Close()
            End If
        Next
        For Each iLoop In Me.MdiChildren
            If iLoop.Name = frmMedrekRawatNewJalanList.Name Then
                iLoop.Activate()
                Exit Sub
            End If
        Next

        frmMedrekRawatNewJalanList.fn_LoadMe(True, 0)
        frmMedrekRawatNewJalanList.MdiParent = Me
        frmMedrekRawatNewJalanList.Show()

        'Dim frmMDISalesMedrek As New frmMDISalesMedrek
        'frmMDISalesMedrek.fn_LoadCategoryDokter(True)
        'frmMDISalesMedrek.WindowState = FormWindowState.Maximized
        'frmMDISalesMedrek.ShowDialog()

        'frmMedrekRawatJalanList.fn_LoadMe(True)
        'frmMedrekRawatJalanList.MdiParent = Me
        'frmMedrekRawatJalanList.Show()

        'For Each iLoop In Me.MdiChildren
        '    If iLoop.Name = frmMedrekRawatNew2JalanList.Name Then
        '        iLoop.Close()
        '    End If
        'Next
        'For Each iLoop In Me.MdiChildren
        '    If iLoop.Name = frmMedrekRawatNew2JalanList.Name Then
        '        iLoop.Activate()
        '        Exit Sub
        '    End If
        'Next

        'frmMedrekRawatNew2JalanList.fn_LoadMe(True)
        'frmMedrekRawatNew2JalanList.MdiParent = Me
        'frmMedrekRawatNew2JalanList.Show()
    End Sub
    Private Sub mnuPenunjangLaboratoriumBilling_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuPenunjangLaboratoriumBilling.ItemClick

    End Sub
    Private Sub BarButtonItem46_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem46.ItemClick
        'For Each iLoop In Me.MdiChildren
        '    If iLoop.Name = frmMedrekRawatJalanList.Name Then
        '        iLoop.Close()
        '    End If
        'Next
        'For Each iLoop In Me.MdiChildren
        '    If iLoop.Name = frmMedrekRawatJalanList.Name Then
        '        iLoop.Activate()
        '        Exit Sub
        '    End If
        'Next

        'frmMedrekRawatJalanList.fn_LoadMe(False)
        'frmMedrekRawatJalanList.MdiParent = Me
        'frmMedrekRawatJalanList.Show()

        For Each iLoop In Me.MdiChildren
            If iLoop.Name = frmMedrekRawatNewJalanList.Name Then
                iLoop.Close()
            End If
        Next
        For Each iLoop In Me.MdiChildren
            If iLoop.Name = frmMedrekRawatNewJalanList.Name Then
                iLoop.Activate()
                Exit Sub
            End If
        Next

        frmMedrekRawatNewJalanList.fn_LoadMe(False, 0)
        frmMedrekRawatNewJalanList.MdiParent = Me
        frmMedrekRawatNewJalanList.Show()

        'For Each iLoop In Me.MdiChildren
        '    If iLoop.Name = frmMedrekRawatNew2JalanList.Name Then
        '        iLoop.Close()
        '    End If
        'Next
        'For Each iLoop In Me.MdiChildren
        '    If iLoop.Name = frmMedrekRawatNew2JalanList.Name Then
        '        iLoop.Activate()
        '        Exit Sub
        '    End If
        'Next

        'frmMedrekRawatNew2JalanList.fn_LoadMe(False)
        'frmMedrekRawatNew2JalanList.MdiParent = Me
        'frmMedrekRawatNew2JalanList.Show()
    End Sub
    Private Sub BarButtonItem47_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem47.ItemClick
        For Each iLoop In Me.MdiChildren
            If iLoop.Name = frmEmedrekReportList.Name Then
                iLoop.Activate()
                Exit Sub
            End If
        Next

        frmEmedrekReportList.MdiParent = Me
        frmEmedrekReportList.Show()
    End Sub
    Private Sub BarButtonItem50_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem50.ItemClick
        For Each iLoop In Me.MdiChildren
            If iLoop.Name = frmTemplateReseptList.Name Then
                iLoop.Activate()
                Exit Sub
            End If
        Next

        frmTemplateReseptList.MdiParent = Me
        frmTemplateReseptList.Show()
    End Sub
    Private Sub mnuAntrianLayarPoli_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuAntrianLayarPoli01.ItemClick
        Dim frmLayarAntrianPoli_01 As New frmLayarAntrianPoli_01
        Try
            sLAYARPOLI = "POLI 1"
            frmLayarAntrianPoli_01.Location = Screen.AllScreens(UBound(Screen.AllScreens)).Bounds.Location + New Point(100, 100)
            frmLayarAntrianPoli_01.Show()
        Catch ex As Exception
            MsgBox("Load Form Detail : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub BarButtonItem51_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem51.ItemClick
        Dim frmLayarAntrianPoli_01 As New frmLayarAntrianPoli_01
        Try
            sLAYARPOLI = "POLI 2"
            frmLayarAntrianPoli_01.Location = Screen.AllScreens(UBound(Screen.AllScreens)).Bounds.Location + New Point(100, 100)
            frmLayarAntrianPoli_01.Show()
        Catch ex As Exception
            MsgBox("Load Form Detail : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub BarButtonItem52_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem52.ItemClick
        Dim frmLayarAntrianPoli_01 As New frmLayarAntrianPoli_01
        Try
            sLAYARPOLI = "POLI 3"
            frmLayarAntrianPoli_01.Location = Screen.AllScreens(UBound(Screen.AllScreens)).Bounds.Location + New Point(100, 100)
            frmLayarAntrianPoli_01.Show()
        Catch ex As Exception
            MsgBox("Load Form Detail : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub BarButtonItem53_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem53.ItemClick
        Dim frmLayarAntrianPoli_01 As New frmLayarAntrianPoli_01
        Try
            sLAYARPOLI = "POLI 4"
            frmLayarAntrianPoli_01.Location = Screen.AllScreens(UBound(Screen.AllScreens)).Bounds.Location + New Point(100, 100)
            frmLayarAntrianPoli_01.Show()
        Catch ex As Exception
            MsgBox("Load Form Detail : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub BarButtonItem54_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem54.ItemClick
        Dim frmLayarAntrianPoli_01 As New frmLayarAntrianPoli_01
        Try
            sLAYARPOLI = "POLI 5"
            frmLayarAntrianPoli_01.Location = Screen.AllScreens(UBound(Screen.AllScreens)).Bounds.Location + New Point(100, 100)
            frmLayarAntrianPoli_01.Show()
        Catch ex As Exception
            MsgBox("Load Form Detail : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub BarButtonItem55_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem55.ItemClick
        Dim frmLayarAntrianPoli_01 As New frmLayarAntrianPoli_01
        Try
            sLAYARPOLI = "POLI 6"
            frmLayarAntrianPoli_01.Location = Screen.AllScreens(UBound(Screen.AllScreens)).Bounds.Location + New Point(100, 100)
            frmLayarAntrianPoli_01.Show()
        Catch ex As Exception
            MsgBox("Load Form Detail : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub BarButtonItem56_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem56.ItemClick
        Dim frmLayarAntrianPoli_01 As New frmLayarAntrianPoli_01
        Try
            sLAYARPOLI = "POLI 7"
            frmLayarAntrianPoli_01.Location = Screen.AllScreens(UBound(Screen.AllScreens)).Bounds.Location + New Point(100, 100)
            frmLayarAntrianPoli_01.Show()
        Catch ex As Exception
            MsgBox("Load Form Detail : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub BarButtonItem57_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem57.ItemClick
        Dim frmLayarAntrianPoli_01 As New frmLayarAntrianPoli_01
        Try
            sLAYARPOLI = "POLI 8"
            frmLayarAntrianPoli_01.Location = Screen.AllScreens(UBound(Screen.AllScreens)).Bounds.Location + New Point(100, 100)
            frmLayarAntrianPoli_01.Show()
        Catch ex As Exception
            MsgBox("Load Form Detail : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub BarButtonItem58_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem58.ItemClick
        Dim frmLayarAntrianPoli_01 As New frmLayarAntrianPoli_01
        Try
            sLAYARPOLI = "POLI 9"
            frmLayarAntrianPoli_01.Location = Screen.AllScreens(UBound(Screen.AllScreens)).Bounds.Location + New Point(100, 100)
            frmLayarAntrianPoli_01.Show()
        Catch ex As Exception
            MsgBox("Load Form Detail : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub BarButtonItem59_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem59.ItemClick
        Dim frmLayarAntrianPoli_01 As New frmLayarAntrianPoli_01
        Try
            sLAYARPOLI = "POLI 10"
            frmLayarAntrianPoli_01.Location = Screen.AllScreens(UBound(Screen.AllScreens)).Bounds.Location + New Point(100, 100)
            frmLayarAntrianPoli_01.Show()
        Catch ex As Exception
            MsgBox("Load Form Detail : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub

    Private Sub BarButtonItem60_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem60.ItemClick
        Dim frmLayarAntrianPoli_01 As New frmLayarAntrianPoli_01
        Try
            sLAYARPOLI = "POLI 11"
            frmLayarAntrianPoli_01.Location = Screen.AllScreens(UBound(Screen.AllScreens)).Bounds.Location + New Point(100, 100)
            frmLayarAntrianPoli_01.Show()
        Catch ex As Exception
            MsgBox("Load Form Detail : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub

    Private Sub BarButtonItem61_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem61.ItemClick
        Dim frmLayarAntrianPoli_01 As New frmLayarAntrianPoli_01
        Try
            sLAYARPOLI = "POLI 12"
            frmLayarAntrianPoli_01.Location = Screen.AllScreens(UBound(Screen.AllScreens)).Bounds.Location + New Point(100, 100)
            frmLayarAntrianPoli_01.Show()
        Catch ex As Exception
            MsgBox("Load Form Detail : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub

    Private Sub BarButtonItem62_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem62.ItemClick
        Dim frmLayarAntrianPoli_01 As New frmLayarAntrianPoli_01
        Try
            sLAYARPOLI = "POLI 13"
            frmLayarAntrianPoli_01.Location = Screen.AllScreens(UBound(Screen.AllScreens)).Bounds.Location + New Point(100, 100)
            frmLayarAntrianPoli_01.Show()
        Catch ex As Exception
            MsgBox("Load Form Detail : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub

    Private Sub BarButtonItem63_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem63.ItemClick
        Dim frmLayarAntrianPoli_01 As New frmLayarAntrianPoli_01
        Try
            sLAYARPOLI = "POLI 14"
            frmLayarAntrianPoli_01.Location = Screen.AllScreens(UBound(Screen.AllScreens)).Bounds.Location + New Point(100, 100)
            frmLayarAntrianPoli_01.Show()
        Catch ex As Exception
            MsgBox("Load Form Detail : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub

    Private Sub BarButtonItem64_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem64.ItemClick
        Dim frmLayarAntrianPoli_01 As New frmLayarAntrianPoli_01
        Try
            sLAYARPOLI = "POLI 15"
            frmLayarAntrianPoli_01.Location = Screen.AllScreens(UBound(Screen.AllScreens)).Bounds.Location + New Point(100, 100)
            frmLayarAntrianPoli_01.Show()
        Catch ex As Exception
            MsgBox("Load Form Detail : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub

    Private Sub BarButtonItem65_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem65.ItemClick
        Dim frmLayarAntrianPoli_01 As New frmLayarAntrianPoli_01
        Try
            sLAYARPOLI = "POLI 16"
            frmLayarAntrianPoli_01.Location = Screen.AllScreens(UBound(Screen.AllScreens)).Bounds.Location + New Point(100, 100)
            frmLayarAntrianPoli_01.Show()
        Catch ex As Exception
            MsgBox("Load Form Detail : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub

    Private Sub BarButtonItem66_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem66.ItemClick
        Dim frmLayarAntrianPoli_01 As New frmLayarAntrianPoli_01
        Try
            sLAYARPOLI = "POLI 17"
            frmLayarAntrianPoli_01.Location = Screen.AllScreens(UBound(Screen.AllScreens)).Bounds.Location + New Point(100, 100)
            frmLayarAntrianPoli_01.Show()
        Catch ex As Exception
            MsgBox("Load Form Detail : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub

    Private Sub BarButtonItem67_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem67.ItemClick
        Dim frmLayarAntrianPoli_01 As New frmLayarAntrianPoli_01
        Try
            sLAYARPOLI = "POLI 18"
            frmLayarAntrianPoli_01.Location = Screen.AllScreens(UBound(Screen.AllScreens)).Bounds.Location + New Point(100, 100)
            frmLayarAntrianPoli_01.Show()
        Catch ex As Exception
            MsgBox("Load Form Detail : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub

    Private Sub BarButtonItem68_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem68.ItemClick
        Dim frmLayarAntrianPoli_01 As New frmLayarAntrianPoli_01
        Try
            sLAYARPOLI = "POLI 19"
            frmLayarAntrianPoli_01.Location = Screen.AllScreens(UBound(Screen.AllScreens)).Bounds.Location + New Point(100, 100)
            frmLayarAntrianPoli_01.Show()
        Catch ex As Exception
            MsgBox("Load Form Detail : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub

    Private Sub BarButtonItem69_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem69.ItemClick
        Dim frmLayarAntrianPoli_01 As New frmLayarAntrianPoli_01
        Try
            sLAYARPOLI = "POLI 20"
            frmLayarAntrianPoli_01.Location = Screen.AllScreens(UBound(Screen.AllScreens)).Bounds.Location + New Point(100, 100)
            frmLayarAntrianPoli_01.Show()
        Catch ex As Exception
            MsgBox("Load Form Detail : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub

    Private Sub mnReloadDataIGD_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnReloadDataIGD.ItemClick
        For Each iLoop In Me.MdiChildren
            If iLoop.Name = frmDIGITAL_IGD_01_AWALList.Name Then
                iLoop.Activate()
                Exit Sub
            End If
        Next

        frmDIGITAL_IGD_01_AWALList.MdiParent = Me
        frmDIGITAL_IGD_01_AWALList.Show()
    End Sub
    Private Sub BarButtonItem70_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem70.ItemClick
        'For Each iLoop In Me.MdiChildren
        '    If iLoop.Name = frmMedrekRawatNewJalanList.Name Then
        '        iLoop.Close()
        '    End If
        'Next
        'For Each iLoop In Me.MdiChildren
        '    If iLoop.Name = frmMedrekRawatNewJalanList.Name Then
        '        iLoop.Activate()
        '        Exit Sub
        '    End If
        'Next

        'frmMedrekRawatNewJalanList.fn_LoadMe(True, 1)
        'frmMedrekRawatNewJalanList.MdiParent = Me
        'frmMedrekRawatNewJalanList.Show()

        For Each iLoop In Me.MdiChildren
            If iLoop.Name = frmReportMedicalRoom.Name Then
                iLoop.Close()
            End If
        Next
        For Each iLoop In Me.MdiChildren
            If iLoop.Name = frmReportMedicalRoom.Name Then
                iLoop.Activate()
                Exit Sub
            End If
        Next

        frmReportMedicalRoom.fn_LoadMe(True, 1)
        frmReportMedicalRoom.MdiParent = Me
        frmReportMedicalRoom.Show()
    End Sub
    Private Sub BarButtonItem71_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem71.ItemClick
        'For Each iLoop In Me.MdiChildren
        '    If iLoop.Name = frmMedrekRawatNewJalanList.Name Then
        '        iLoop.Close()
        '    End If
        'Next
        'For Each iLoop In Me.MdiChildren
        '    If iLoop.Name = frmMedrekRawatNewJalanList.Name Then
        '        iLoop.Activate()
        '        Exit Sub
        '    End If
        'Next

        'frmMedrekRawatNewJalanList.fn_LoadMe(False, 1)
        'frmMedrekRawatNewJalanList.MdiParent = Me
        'frmMedrekRawatNewJalanList.Show()

        For Each iLoop In Me.MdiChildren
            If iLoop.Name = frmReportMedicalRoom.Name Then
                iLoop.Close()
            End If
        Next
        For Each iLoop In Me.MdiChildren
            If iLoop.Name = frmReportMedicalRoom.Name Then
                iLoop.Activate()
                Exit Sub
            End If
        Next

        frmReportMedicalRoom.fn_LoadMe(False, 1)
        frmReportMedicalRoom.MdiParent = Me
        frmReportMedicalRoom.Show()
    End Sub

    Private Sub BarButtonItem72_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem72.ItemClick
        For Each iLoop In Me.MdiChildren
            If iLoop.Name = frmItemDiagnosaPerawatList.Name Then
                iLoop.Activate()
                Exit Sub
            End If
        Next

        frmItemDiagnosaPerawatList.MdiParent = Me
        frmItemDiagnosaPerawatList.Show()
    End Sub
    Private Sub btnDownloadTTE_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btnDownloadTTE.ItemClick
        'Try
        '    If Not IO.Directory.Exists(CopyLocal) Then
        '        IO.Directory.CreateDirectory(CopyLocal)
        '    Else
        '        DeleteDirectory(CopyLocal)
        '        IO.Directory.CreateDirectory(CopyLocal)
        '    End If

        '    Dim Folder As New IO.DirectoryInfo(sDownloadAlamatTandaTanganDokter)

        '    For Each mFile As IO.FileInfo In Folder.GetFiles("*.*", IO.SearchOption.AllDirectories)
        '        Dim SaveImage As New Bitmap(GetImageFromURL(mFile.FullName))
        '        SaveImage.Save(CopyLocal & mFile.Name, Imaging.ImageFormat.Png)
        '        SaveImage.Dispose()
        '        'arrfname.Add(mFile.FullName)
        '    Next

        'Catch ex As Exception

        'End Try
    End Sub
    Private Function ByteArrayToImage(ByVal byteArrayIn() As Byte) As Image
        Using ms As New System.IO.MemoryStream(byteArrayIn)
            Dim returnImage = Image.FromStream(ms)
            Return returnImage
        End Using
    End Function
    Private Function GetImageFromURL(ByVal url As String) As Image
        Dim retVal As Image = Nothing

        If Not String.IsNullOrWhiteSpace(url) Then
            Dim req As System.Net.WebRequest = System.Net.WebRequest.Create(url.Trim)

            Using request As System.Net.WebResponse = req.GetResponse
                Using stream As System.IO.Stream = request.GetResponseStream
                    retVal = New Bitmap(System.Drawing.Image.FromStream(stream))
                End Using
            End Using
        End If

        Return retVal

    End Function
    Private Sub DeleteDirectory(path As String)
        If IO.Directory.Exists(path) Then
            If IO.Directory.Exists(path) Then
                'Delete all files from the Directory
                For Each filepath As String In IO.Directory.GetFiles(path)
                    IO.File.Delete(filepath)
                Next
                'Delete all child Directories
                For Each dir As String In IO.Directory.GetDirectories(path)
                    DeleteDirectory(dir)
                Next
                'Delete a Directory
                IO.Directory.Delete(path)
            End If

        End If
    End Sub
    Private Sub BarButtonItem73_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem73.ItemClick
        sCPPT_QRRJ = Not sCPPT_QRRJ

        statusLANGUAGE.Caption = IIf(sCPPT_QRRJ = False, "No QR", "QR")
    End Sub

    Private Sub BarButtonItem76_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem76.ItemClick
        For Each iLoop In Me.MdiChildren
            If iLoop.Name = frmJenisLaporanOperasiList.Name Then
                iLoop.Activate()
                Exit Sub
            End If
        Next

        frmJenisLaporanOperasiList.MdiParent = Me
        frmJenisLaporanOperasiList.Show()
    End Sub

    Private Sub BarButtonItem77_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem77.ItemClick
        For Each iLoop In Me.MdiChildren
            If iLoop.Name = frmJenisLaporanTindakanList.Name Then
                iLoop.Activate()
                Exit Sub
            End If
        Next

        frmJenisLaporanTindakanList.MdiParent = Me
        frmJenisLaporanTindakanList.Show()
    End Sub

    Private Sub BarButtonItem78_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem78.ItemClick
        For Each iLoop In Me.MdiChildren
            If iLoop.Name = frmADokumenMasterList.Name Then
                iLoop.Activate()
                Exit Sub
            End If
        Next

        frmADokumenMasterList.MdiParent = Me
        frmADokumenMasterList.Show()
    End Sub

    Private Sub BarButtonItem79_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem79.ItemClick
        For Each iLoop In Me.MdiChildren
            If iLoop.Name = frmReportRME.Name Then
                iLoop.Activate()
                Exit Sub
            End If
        Next

        frmReportRME.MdiParent = Me
        frmReportRME.Show()
    End Sub

#End Region
    '#Region "Billing"
    '    Private Sub mnuTransaksiBillingRawatJalan_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuTransaksiBillingRawatJalan.ItemClick
    '        For Each iLoop In Me.MdiChildren
    '            If iLoop.Name = frmBillingList.Name Then
    '                iLoop.Close()
    '            End If
    '        Next
    '        For Each iLoop In Me.MdiChildren
    '            If iLoop.Name = frmBillingList.Name Then
    '                iLoop.Activate()
    '                Exit Sub
    '            End If
    '        Next

    '        frmBillingList.fn_LoadCategory(0)
    '        frmBillingList.MdiParent = Me
    '        frmBillingList.Show()
    '    End Sub
    '    Private Sub mnuTransaksiBillingRawatInap_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuTransaksiBillingRawatInap.ItemClick
    '        For Each iLoop In Me.MdiChildren
    '            If iLoop.Name = frmBillingList.Name Then
    '                iLoop.Close()
    '            End If
    '        Next
    '        For Each iLoop In Me.MdiChildren
    '            If iLoop.Name = frmBillingList.Name Then
    '                iLoop.Activate()
    '                Exit Sub
    '            End If
    '        Next

    '        frmBillingList.fn_LoadCategory(1)
    '        frmBillingList.MdiParent = Me
    '        frmBillingList.Show()
    '    End Sub
    '    Private Sub mnuPenunjangLaboratoriumBilling_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuPenunjangLaboratoriumBilling.ItemClick
    '        For Each iLoop In Me.MdiChildren
    '            If iLoop.Name = frmBillingList.Name Then
    '                iLoop.Close()
    '            End If
    '        Next
    '        For Each iLoop In Me.MdiChildren
    '            If iLoop.Name = frmBillingList.Name Then
    '                iLoop.Activate()
    '                Exit Sub
    '            End If
    '        Next

    '        frmBillingList.fn_LoadCategory(2)
    '        frmBillingList.MdiParent = Me
    '        frmBillingList.Show()
    '    End Sub
    '    Private Sub mnuPenunjangRadiologiBilling_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuPenunjangRadiologiBilling.ItemClick
    '        For Each iLoop In Me.MdiChildren
    '            If iLoop.Name = frmBillingList.Name Then
    '                iLoop.Close()
    '            End If
    '        Next
    '        For Each iLoop In Me.MdiChildren
    '            If iLoop.Name = frmBillingList.Name Then
    '                iLoop.Activate()
    '                Exit Sub
    '            End If
    '        Next

    '        frmBillingList.fn_LoadCategory(3)
    '        frmBillingList.MdiParent = Me
    '        frmBillingList.Show()
    '    End Sub
    '    Private Sub mnuPenunjangRadiologiUSG_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuPenunjangRadiologiUSG.ItemClick
    '        For Each iLoop In Me.MdiChildren
    '            If iLoop.Name = frmUSGList.Name Then
    '                iLoop.Activate()
    '                Exit Sub
    '            End If
    '        Next

    '        frmUSGList.MdiParent = Me
    '        frmUSGList.Show()
    '    End Sub
    '    Private Sub mnuPenunjangRadiologiRontgen_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuPenunjangRadiologiRontgen.ItemClick
    '        For Each iLoop In Me.MdiChildren
    '            If iLoop.Name = frmRadiologiList.Name Then
    '                iLoop.Activate()
    '                Exit Sub
    '            End If
    '        Next

    '        frmRadiologiList.MdiParent = Me
    '        frmRadiologiList.Show()
    '    End Sub
    '    Private Sub mnuPenunjangRadiologiItemRadiologi_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuPenunjangRadiologiItemRadiologi.ItemClick
    '        For Each iLoop In Me.MdiChildren
    '            If iLoop.Name = frmItemRontgenList.Name Then
    '                iLoop.Activate()
    '                Exit Sub
    '            End If
    '        Next

    '        frmItemRontgenList.MdiParent = Me
    '        frmItemRontgenList.Show()
    '    End Sub
    '    Private Sub mnuFarmasiPenjualanResep_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuFarmasiPenjualanResep.ItemClick
    '        For Each iLoop In Me.MdiChildren
    '            If iLoop.Name = frmBillingList.Name Then
    '                iLoop.Close()
    '            End If
    '        Next

    '        For Each iLoop In Me.MdiChildren
    '            If iLoop.Name = frmBillingList.Name Then
    '                iLoop.Activate()
    '                Exit Sub
    '            End If
    '        Next

    '        frmBillingList.fn_LoadCategory(4)
    '        frmBillingList.MdiParent = Me
    '        frmBillingList.Show()
    '    End Sub
    '    Private Sub mnuTransaksiBillingUnit_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuTransaksiBillingUnit.ItemClick
    '        For Each iLoop In Me.MdiChildren
    '            If iLoop.Name = frmBillingUnitList.Name Then
    '                iLoop.Activate()
    '                Exit Sub
    '            End If
    '        Next

    '        frmBillingUnitList.MdiParent = Me
    '        frmBillingUnitList.Show()
    '    End Sub
    '#End Region
    '#Region "Kasir"
    '    Private Sub mnuKasirPembayaran_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuKasirPembayaran.ItemClick
    '        For Each iLoop In Me.MdiChildren
    '            If iLoop.Name = frmCashInList.Name Then
    '                iLoop.Activate()
    '                Exit Sub
    '            End If
    '        Next

    '        frmCashInList.MdiParent = Me
    '        frmCashInList.Show()
    '    End Sub
    '#End Region
    '#Region "E Klaim"
    '    Private Sub mnuEKlaimGrouper_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuEKlaimGrouper.ItemClick
    '        For Each iLoop In Me.MdiChildren
    '            If iLoop.Name = frmGrouperList.Name Then
    '                iLoop.Activate()
    '                Exit Sub
    '            End If
    '        Next

    '        frmGrouperList.MdiParent = Me
    '        frmGrouperList.Show()
    '    End Sub
    '#End Region
    '#Region "Instalasi Farmasi"
    '    Private Sub mnuPurchasingPurchaseOrder_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuPurchasingPurchaseOrder.ItemClick
    '        For Each iLoop In Me.MdiChildren
    '            If iLoop.Name = frmPurchaseOrderList.Name Then
    '                iLoop.Activate()
    '                Exit Sub
    '            End If
    '        Next

    '        frmPurchaseOrderList.MdiParent = Me
    '        frmPurchaseOrderList.Show()
    '    End Sub
    '    Private Sub mnuPurchasingPurchaseInvoice_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuPurchasingPurchaseInvoice.ItemClick
    '        For Each iLoop In Me.MdiChildren
    '            If iLoop.Name = frmPurchaseInvoiceList.Name Then
    '                iLoop.Activate()
    '                Exit Sub
    '            End If
    '        Next

    '        frmPurchaseInvoiceList.MdiParent = Me
    '        frmPurchaseInvoiceList.Show()
    '    End Sub
    '    Private Sub mnuPurchasingPurchaseReturn_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuPurchasingPurchaseReturn.ItemClick
    '        For Each iLoop In Me.MdiChildren
    '            If iLoop.Name = frmPurchaseReturnList.Name Then
    '                iLoop.Activate()
    '                Exit Sub
    '            End If
    '        Next

    '        frmPurchaseReturnList.MdiParent = Me
    '        frmPurchaseReturnList.Show()
    '    End Sub
    '    Private Sub mnuInventoryReportMonitoringItem_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuInventoryReportMonitoringItem.ItemClick
    '        For Each iLoop In Me.MdiChildren
    '            If iLoop.Name = frmReportMonitoringItem.Name Then
    '                iLoop.Activate()
    '                Exit Sub
    '            End If
    '        Next

    '        frmReportMonitoringItem.MdiParent = Me
    '        frmReportMonitoringItem.Show()
    '    End Sub
    '    Private Sub mnuInventoryMutation_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuInventoryMutation.ItemClick
    '        For Each iLoop In Me.MdiChildren
    '            If iLoop.Name = frmMutationList.Name Then
    '                iLoop.Activate()
    '                Exit Sub
    '            End If
    '        Next

    '        frmMutationList.MdiParent = Me
    '        frmMutationList.Show()
    '    End Sub
    '    Private Sub mnuInventoryMutationBHP_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuInventoryMutationBHP.ItemClick
    '        For Each iLoop In Me.MdiChildren
    '            If iLoop.Name = frmMutationBHPList.Name Then
    '                iLoop.Activate()
    '                Exit Sub
    '            End If
    '        Next

    '        frmMutationBHPList.MdiParent = Me
    '        frmMutationBHPList.Show()
    '    End Sub
    '    Private Sub mnuInventoryOpname_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuInventoryOpname.ItemClick
    '        For Each iLoop In Me.MdiChildren
    '            If iLoop.Name = frmOpnameList.Name Then
    '                iLoop.Activate()
    '                Exit Sub
    '            End If
    '        Next

    '        frmOpnameList.MdiParent = Me
    '        frmOpnameList.Show()
    '    End Sub
    '    Private Sub mnuInventoryAdjusment_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuInventoryAdjusment.ItemClick
    '        For Each iLoop In Me.MdiChildren
    '            If iLoop.Name = frmAdjustmnetList.Name Then
    '                iLoop.Activate()
    '                Exit Sub
    '            End If
    '        Next

    '        frmAdjustmnetList.MdiParent = Me
    '        frmAdjustmnetList.Show()
    '    End Sub
    '    Private Sub mnuPurchasingReportPurchaseOrder_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuPurchasingReportPurchaseOrder.ItemClick
    '        For Each iLoop In Me.MdiChildren
    '            If iLoop.Name = frmReportPurchaseOrder.Name Then
    '                iLoop.Activate()
    '                Exit Sub
    '            End If
    '        Next

    '        frmReportPurchaseOrder.MdiParent = Me
    '        frmReportPurchaseOrder.Show()
    '    End Sub
    '    Private Sub mnuPurchasingReportPurchaseInvoice_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuPurchasingReportPurchaseInvoice.ItemClick
    '        For Each iLoop In Me.MdiChildren
    '            If iLoop.Name = frmReportPurchaseInvoice.Name Then
    '                iLoop.Activate()
    '                Exit Sub
    '            End If
    '        Next

    '        frmReportPurchaseInvoice.MdiParent = Me
    '        frmReportPurchaseInvoice.Show()
    '    End Sub
    '    Private Sub mnuPurchasingReportPurchaseReturn_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuPurchasingReportPurchaseReturn.ItemClick
    '        For Each iLoop In Me.MdiChildren
    '            If iLoop.Name = frmReportPurchaseReturn.Name Then
    '                iLoop.Activate()
    '                Exit Sub
    '            End If
    '        Next

    '        frmReportPurchaseReturn.MdiParent = Me
    '        frmReportPurchaseReturn.Show()
    '    End Sub
    '    Private Sub mnuInventoryReportMutation_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuInventoryReportMutation.ItemClick
    '        For Each iLoop In Me.MdiChildren
    '            If iLoop.Name = frmReportMutation.Name Then
    '                iLoop.Activate()
    '                Exit Sub
    '            End If
    '        Next

    '        frmReportMutation.MdiParent = Me
    '        frmReportMutation.Show()
    '    End Sub
    '    Private Sub mnuInventoryReportMutationBHP_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuInventoryReportMutationBHP.ItemClick
    '        For Each iLoop In Me.MdiChildren
    '            If iLoop.Name = frmReportMutationBHP.Name Then
    '                iLoop.Activate()
    '                Exit Sub
    '            End If
    '        Next

    '        frmReportMutationBHP.MdiParent = Me
    '        frmReportMutationBHP.Show()
    '    End Sub
    '    Private Sub mnuInventoryReportOpname_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuInventoryReportOpname.ItemClick
    '        For Each iLoop In Me.MdiChildren
    '            If iLoop.Name = frmReportOpname.Name Then
    '                iLoop.Activate()
    '                Exit Sub
    '            End If
    '        Next

    '        frmReportOpname.MdiParent = Me
    '        frmReportOpname.Show()
    '    End Sub
    '    Private Sub mnuInventoryReportAdjustment_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuInventoryReportAdjustment.ItemClick
    '        For Each iLoop In Me.MdiChildren
    '            If iLoop.Name = frmReportAdjustment.Name Then
    '                iLoop.Activate()
    '                Exit Sub
    '            End If
    '        Next

    '        frmReportAdjustment.MdiParent = Me
    '        frmReportAdjustment.Show()
    '    End Sub
    '    Private Sub mnuFarmasiPenjualanTanpaResep_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuFarmasiPenjualanTanpaResep.ItemClick
    '        For Each iLoop In Me.MdiChildren
    '            If iLoop.Name = frmBillingTanpaResepList.Name Then
    '                iLoop.Activate()
    '                Exit Sub
    '            End If
    '        Next

    '        frmBillingTanpaResepList.MdiParent = Me
    '        frmBillingTanpaResepList.Show()
    '    End Sub
    '#End Region
    '#Region "Rekam Medis"
    '    Private Sub mnuRekamMedisTracking_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuRekamMedisTracking.ItemClick
    '        For Each iLoop In Me.MdiChildren
    '            If iLoop.Name = frmTrackingList.Name Then
    '                iLoop.Activate()
    '                Exit Sub
    '            End If
    '        Next

    '        frmTrackingList.MdiParent = Me
    '        frmTrackingList.Show()
    '    End Sub
    '    Private Sub mnuRekamMedisLaporanTracking_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuRekamMedisLaporanTracking.ItemClick
    '        For Each iLoop In Me.MdiChildren
    '            If iLoop.Name = frmReportTracking.Name Then
    '                iLoop.Activate()
    '                Exit Sub
    '            End If
    '        Next

    '        frmReportTracking.MdiParent = Me
    '        frmReportTracking.Show()
    '    End Sub
    '    Private Sub mnuTransaksiLaporanBilling_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuTransaksiLaporanBillinRJ.ItemClick
    '        For Each iLoop In Me.MdiChildren
    '            If iLoop.Name = frmReportBilling.Name Then
    '                iLoop.Close()
    '            End If
    '        Next
    '        For Each iLoop In Me.MdiChildren
    '            If iLoop.Name = frmReportBilling.Name Then
    '                iLoop.Activate()
    '                Exit Sub
    '            End If
    '        Next

    '        frmReportBilling.fn_LoadCategory(0)
    '        frmReportBilling.MdiParent = Me
    '        frmReportBilling.Show()
    '    End Sub
    '    Private Sub mnuTransaksiLaporanBillinRI_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuTransaksiLaporanBillinRI.ItemClick
    '        For Each iLoop In Me.MdiChildren
    '            If iLoop.Name = frmReportBilling.Name Then
    '                iLoop.Close()
    '            End If
    '        Next
    '        For Each iLoop In Me.MdiChildren
    '            If iLoop.Name = frmReportBilling.Name Then
    '                iLoop.Activate()
    '                Exit Sub
    '            End If
    '        Next

    '        frmReportBilling.fn_LoadCategory(1)
    '        frmReportBilling.MdiParent = Me
    '        frmReportBilling.Show()
    '    End Sub
    '    Private Sub BarButtonItem42_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem42.ItemClick
    '        For Each iLoop In Me.MdiChildren
    '            If iLoop.Name = frmReportBilling.Name Then
    '                iLoop.Close()
    '            End If
    '        Next
    '        For Each iLoop In Me.MdiChildren
    '            If iLoop.Name = frmReportBilling.Name Then
    '                iLoop.Activate()
    '                Exit Sub
    '            End If
    '        Next

    '        frmReportBilling.fn_LoadCategory(2)
    '        frmReportBilling.MdiParent = Me
    '        frmReportBilling.Show()
    '    End Sub
    '    Private Sub BarButtonItem40_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem40.ItemClick
    '        For Each iLoop In Me.MdiChildren
    '            If iLoop.Name = frmReportBilling.Name Then
    '                iLoop.Close()
    '            End If
    '        Next
    '        For Each iLoop In Me.MdiChildren
    '            If iLoop.Name = frmReportBilling.Name Then
    '                iLoop.Activate()
    '                Exit Sub
    '            End If
    '        Next

    '        frmReportBilling.fn_LoadCategory(3)
    '        frmReportBilling.MdiParent = Me
    '        frmReportBilling.Show()
    '    End Sub
    '    Private Sub BarButtonItem43_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem43.ItemClick
    '        For Each iLoop In Me.MdiChildren
    '            If iLoop.Name = frmReportBillingFarmasi.Name Then
    '                iLoop.Close()
    '            End If
    '        Next
    '        For Each iLoop In Me.MdiChildren
    '            If iLoop.Name = frmReportBillingFarmasi.Name Then
    '                iLoop.Activate()
    '                Exit Sub
    '            End If
    '        Next

    '        frmReportBillingFarmasi.fn_LoadCategory(4)
    '        frmReportBillingFarmasi.MdiParent = Me
    '        frmReportBillingFarmasi.Show()
    '    End Sub

    '    Private Sub BarButtonItem44_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem44.ItemClick
    '        For Each iLoop In Me.MdiChildren
    '            If iLoop.Name = frmReportBillingTanpaResep.Name Then
    '                iLoop.Close()
    '            End If
    '        Next
    '        For Each iLoop In Me.MdiChildren
    '            If iLoop.Name = frmReportBillingTanpaResep.Name Then
    '                iLoop.Activate()
    '                Exit Sub
    '            End If
    '        Next

    '        frmReportBillingTanpaResep.fn_LoadCategory(4)
    '        frmReportBillingTanpaResep.MdiParent = Me
    '        frmReportBillingTanpaResep.Show()
    '    End Sub
    '    Private Sub BarButtonItem45_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem45.ItemClick
    '        For Each iLoop In Me.MdiChildren
    '            If iLoop.Name = frmPendaftaran_KunjunganPoliList.Name Then
    '                iLoop.Activate()
    '                Exit Sub
    '            End If
    '        Next

    '        frmPendaftaran_KunjunganPoliList.MdiParent = Me
    '        frmPendaftaran_KunjunganPoliList.Show()
    '    End Sub
    '#End Region
End Class