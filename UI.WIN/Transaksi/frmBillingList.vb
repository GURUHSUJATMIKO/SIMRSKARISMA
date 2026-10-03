Imports DataAccess
Imports UI.WIN.MAIN.My.Resources
Imports System.Linq
Imports System.Data.SqlClient

Public Class frmBillingList
    Implements ILanguage

    Private oFormMode As FORM_MODE = FORM_MODE.FORM_MODE_VIEW
    Private oBilling As New Transaksi.clsBilling
    Private sCategory As Integer = 0
    Private isLoad As Boolean = False

#Region "Function"
    Public Sub fn_LoadCategory(ByVal Category As Integer)
        sCategory = Category
    End Sub
    Private Sub Me_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.Text = "Billing "

        deDATEFrom.DateTime = Now
        deDATETo.DateTime = Now

        Dim oSetting As New Setting.clsUser
        Dim dsKdwarehouse = oSetting.GetDataDetail(sUserID).FirstOrDefault(Function(x) x.ISDEFAULT = True)
        If dsKdwarehouse IsNot Nothing Then
            grdKDWAREHOUSE.Text = dsKdwarehouse.KDWAREHOUSE
            sKDWAREHOUSEAUTO = dsKdwarehouse.KDWAREHOUSE
        Else
            sKDWAREHOUSEAUTO = String.Empty
        End If

        If sKDWAREHOUSEAUTO <> String.Empty Then
            lblWAREHOUSE.Visible = True
            grdKDWAREHOUSE.Visible = True
            fn_LoadWarehouse()
        Else
            lblWAREHOUSE.Visible = False
            grdKDWAREHOUSE.Visible = False
        End If

        fn_LoadSecurity()

        isLoad = True

        Try
            grv.RestoreLayoutFromRegistry("HKEY_CURRENT_USER\Software\SIMRS\SW\" & sUserID & "\" & Me.Text)
        Catch ex As Exception

        End Try
    End Sub
    Private Sub Me_FormClosed(sender As System.Object, e As System.Windows.Forms.FormClosedEventArgs) Handles MyBase.FormClosed
        Try
            grv.SaveLayoutToRegistry("HKEY_CURRENT_USER\Software\SIMRS\SW\" & sUserID & "\" & Me.Text)
        Catch ex As Exception

        End Try
    End Sub
    Private Sub fn_LoadWarehouse()
        Dim oWarehouse As New Reference.clsWarehouse
        Dim oUser As New Setting.clsUser

        Try
            Dim ds = From x In oWarehouse.GetData
                     Join y In oUser.GetDataDetail(sUserID)
                     On x.KDWAREHOUSE Equals y.KDWAREHOUSE
                     Select x.KDWAREHOUSE, x.NAME_DISPLAY, x.ISACTIVE

            grdKDWAREHOUSE.Properties.DataSource = ds.Where(Function(x) x.ISACTIVE = True).ToList()
            grdKDWAREHOUSE.Properties.ValueMember = "KDWAREHOUSE"
            grdKDWAREHOUSE.Properties.DisplayMember = "NAME_DISPLAY"
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadSecurity()
        Try
            Dim oOtority As New Setting.clsOtority
            Dim oUser As New Setting.clsUser

            If isLoad = False Then
                Dim dsCashin = (From x In oOtority.GetDataDetail
                                Join y In oUser.GetData
                                On x.KDOTORITY Equals y.KDOTORITY
                                Where x.MODUL = "CASHIN" _
                                And y.KDUSER = sUserID
                                Select x.ISADD, x.ISDELETE, x.ISUPDATE, x.ISPRINT, x.ISVIEW).FirstOrDefault

                Try
                    mnuStrip.Visible = dsCashin.ISADD
                Catch ex As Exception
                    mnuStrip.Enabled = False
                End Try

            End If

            Dim ds = (From x In oOtority.GetDataDetail
                      Join y In oUser.GetData
                      On x.KDOTORITY Equals y.KDOTORITY
                      Where x.MODUL = "BILLING" & sCategory _
                      And y.KDUSER = sUserID
                      Select x.ISADD, x.ISDELETE, x.ISUPDATE, x.ISPRINT, x.ISVIEW).FirstOrDefault

            Try
                picAdd.Enabled = ds.ISADD
                picDelete.Enabled = ds.ISDELETE
                picUpdate.Enabled = ds.ISUPDATE
                picPrint.Enabled = ds.ISPRINT
                picRefresh.Enabled = ds.ISVIEW

                If ds.ISVIEW = True Then
                    If sKDWAREHOUSEAUTO <> String.Empty Then
                        If grdKDWAREHOUSE.Text <> String.Empty Then
                            fn_LoadData()
                            fn_LoadLanguage()
                        End If
                    Else
                        fn_LoadData()
                        fn_LoadLanguage()
                    End If

                    Try
                        grv.RestoreLayoutFromRegistry("HKEY_CURRENT_USER\Software\SIMRS\SW\" & sUserID & "\" & Me.Text)
                    Catch ex As Exception

                    End Try
                End If
            Catch oErr As Exception
                MsgBox(Statement.SecurityNotInstalled, MsgBoxStyle.Exclamation, Me.Text)

                picAdd.Enabled = False
                picDelete.Enabled = False
                picUpdate.Enabled = False
                picPrint.Enabled = False
                picRefresh.Enabled = False
            End Try
        Catch oErr As Exception
            MsgBox(Statement.SecurityNotInstalled, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Public Sub fn_LoadLanguage() Implements ILanguage.fn_LoadLanguage
        Try
            If sCategory = 0 Then
                Me.Text = "Billing " & "Rawat Jalan"
            ElseIf sCategory = 1 Then
                Me.Text = "Billing " & "Rawat Inap"
            ElseIf sCategory = 2 Then
                Me.Text = "Billing " & "Laboratorium"
            ElseIf sCategory = 3 Then
                Me.Text = "Billing " & "Radiologi"
            ElseIf sCategory = 4 Then
                Me.Text = "Billing " & "Farmasi"
            End If

            grv.Columns("TANGGAL").Caption = Billing.TANGGAL
            grv.Columns("KDBILLING").Caption = Billing.KDBILLING_POLI
            grv.Columns("KDPENDAFTARAN").Caption = Billing.KDPENDAFTARAN
            grv.Columns("KDKUNJUNGAN").Caption = Billing.KDKUNJUNGAN_POLI
            grv.Columns("TUJUAN").Caption = Billing.TUJUAN
            grv.Columns("DOKTER").Caption = "Dokter"
            grv.Columns("PENJAMIN").Caption = Billing.PENJAMIN
            grv.Columns("REKAMMEDIS").Caption = Billing.REKAMMEDIS
            grv.Columns("NAMA").Caption = Billing.NAMA
            grv.Columns("SUDAHBAYAR").Caption = Billing.SUDAHBAYAR
            grv.Columns("KDUSER").Caption = Billing.KDUSER

            fn_LoadLanguageMaster()
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Public Sub fn_LoadLanguageMaster()
        Try
            'grv.Columns("KDBILLING").Caption = Billing.KDBilling
        Catch oErr As Exception

        End Try
    End Sub
    Private Sub fn_LoadData()
        Try
            grv.Columns.Clear()
            grd.DataSource = Nothing

            Dim oConn As New SqlConnection
            Dim oComm As New SqlCommand
            Dim da As SqlDataAdapter
            Dim ds As New DataSet
            Dim SQL As String

            Dim sConn As String = Decrypt(My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Software\SIMRS\SW\", "Database", "").ToString())

            oConn = New SqlConnection(sConn)
            If oConn.State = ConnectionState.Closed Then
                oConn.Open()
            End If

            SQL = "SELECT "
            SQL &= "TANGGAL = A.DATE "
            SQL &= ",A.KDBILLING "
            SQL &= ",A.KDPENDAFTARAN "
            SQL &= ",A.KDKUNJUNGAN "
            SQL &= ",PENJAMIN = D.MEMO "
            SQL &= ",TUJUAN = ISNULL((SELECT (SELECT BB.NAME_DISPLAY FROM M_DEPARTMENT BB WHERE AA.KDDEPARTMENT = BB.KDDEPARTMENT) FROM S_PENDAFTARAN_KUNJUNGANPOLI AA WHERE A.KDKUNJUNGAN = AA.KDKUNJUNGAN_POLI), (SELECT (SELECT BB.NAME_DISPLAY FROM M_RUANGRAWAT BB WHERE AA.KDRUANGRAWAT = BB.KDRUANGRAWAT) FROM S_PENDAFTARAN_KUNJUNGANRUANGAN AA WHERE A.KDKUNJUNGAN = AA.KDKUNJUNGAN_RUANGAN)) "
            SQL &= ",DOKTER = ISNULL((SELECT (SELECT BB.NAME_DISPLAY FROM M_DOCTOR BB WHERE AA.KDDOCTOR = BB.KDDOCTOR) FROM S_PENDAFTARAN_KUNJUNGANPOLI AA WHERE A.KDKUNJUNGAN = AA.KDKUNJUNGAN_POLI), (SELECT (SELECT BB.NAME_DISPLAY FROM M_DOCTOR BB WHERE AA.KDDOCTOR = BB.KDDOCTOR) FROM S_PENDAFTARAN_KUNJUNGANRUANGAN AA WHERE A.KDKUNJUNGAN = AA.KDKUNJUNGAN_RUANGAN)) "
            SQL &= ",REKAMMEDIS = B.KDCUSTOMER "
            SQL &= ",NAMA = C.NAME_DISPLAY "
            SQL &= ",A.KDUSER "
            SQL &= ",SUDAHBAYAR = (SELECT CASE A.PAYAMOUNT WHEN 0 THEN CONVERT(bit, 0) ELSE CONVERT(bit, 1) END) "
            SQL &= "FROM "
            SQL &= "S_BILLING_H A "
            SQL &= "INNER JOIN S_PENDAFTARAN_H B "
            SQL &= "ON A.KDPENDAFTARAN = B.KDPENDAFTARAN "
            SQL &= "INNER JOIN M_CUSTOMER C "
            SQL &= "ON B.KDCUSTOMER = C.KDCUSTOMER "
            SQL &= "INNER JOIN M_PENJAMIN D "
            SQL &= "ON A.KDPENJAMIN = D.KDPENJAMIN "
            SQL &= "WHERE CONVERT(VARCHAR(8), A.DATE, 112) >= '" & deDATEFrom.DateTime.ToString("yyyyMMdd") & "' AND CONVERT(VARCHAR(8), A.DATE, 112) <= '" & deDATETo.DateTime.ToString("yyyyMMdd") & "' "
            SQL &= "AND A.CATEGORY = " & sCategory & " "
            If sCategory = 4 Then
                SQL &= "AND A.KDWAREHOUSE = '" & grdKDWAREHOUSE.EditValue & "' "
            End If
            SQL &= "ORDER BY A.DATE DESC "

            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "ALL")

            grd.MainView = grv
            grd.DataSource = ds.Tables("ALL")
            grd.ForceInitialize()

            fn_LoadFormatData()

            If oConn.State = ConnectionState.Open Then
                oConn.Close()
            End If
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadFormatData()
        For iLoop As Integer = 0 To grv.Columns.Count - 1
            If grv.Columns(iLoop).ColumnType.Name = "Decimal" Then
                grv.Columns(iLoop).DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                grv.Columns(iLoop).DisplayFormat.FormatString = "{0:n2}"
                grv.Columns(iLoop).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
            ElseIf grv.Columns(iLoop).ColumnType.Name = "DateTime" Then
                grv.Columns(iLoop).DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
                grv.Columns(iLoop).DisplayFormat.FormatString = "{0:dd/MM/yyyy HH:mm:ss}"
            End If
        Next
        grv.BestFitColumns()
    End Sub
    'Private Sub MasterColumnChooserToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles MasterColumnChooserToolStripMenuItem.Click
    '    grv.ShowCustomization()
    'End Sub
    Private Sub grv_FocusedRowChanged(ByVal sender As System.Object, ByVal e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs)
        If grv.GetFocusedRowCellValue("KDBILLING") Is Nothing Then
            fn_LoadSecurity()
            Exit Sub
        End If
    End Sub
#End Region
#Region "Command Button"
    Private Sub frmMember_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        Select Case e.KeyCode
            Case Keys.A
                If e.Alt = True And picAdd.Enabled = True Then
                    picAdd_Click()
                End If
            Case Keys.E
                If e.Alt = True And picUpdate.Enabled = True Then
                    picUpdate_Click()
                End If
            Case Keys.D
                If e.Alt = True And picDelete.Enabled = True Then
                    picDelete_Click()
                End If
            Case Keys.P
                If e.Alt = True And picPrint.Enabled = True Then
                    picPrint_Click()
                End If
            Case Keys.R
                If e.Alt = True And picRefresh.Enabled = True Then
                    picRefresh_Click()
                End If
        End Select
    End Sub
    Private Sub grv_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles grv.DoubleClick
        If grv.GetFocusedRowCellValue("KDBILLING") Is Nothing Then
            Exit Sub
        End If

        If sCategory = 4 Then
            Dim frmBillingFarmasi As New frmBillingFarmasi
            Try
                frmBillingFarmasi.LoadMe(FORM_MODE.FORM_MODE_VIEW, sCategory, True, grv.GetFocusedRowCellValue("KDBILLING"))
                frmBillingFarmasi.ShowDialog(Me)
            Catch oErr As Exception
                MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            End Try
        Else
            Dim frmBilling As New frmBilling
            Try
                frmBilling.LoadMe(FORM_MODE.FORM_MODE_VIEW, sCategory, grv.GetFocusedRowCellValue("KDBILLING"))
                frmBilling.ShowDialog(Me)
            Catch oErr As Exception
                MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            End Try
        End If
    End Sub
    Private Sub picAdd_Click() Handles picAdd.Click
        sKDWAREHOUSEAUTO = grdKDWAREHOUSE.EditValue

        If sCategory = 4 Or sCategory = 3 Or sCategory = 2 Then
            Dim frmMDISales As New frmMDISales

            frmMDISales.fn_LoadCategory(sCategory)
            frmMDISales.WindowState = FormWindowState.Maximized
            frmMDISales.ShowDialog()
            fn_LoadSecurity()
        Else
            Dim frmBilling As New frmBilling
            Try
                frmBilling.LoadMe(FORM_MODE.FORM_MODE_ADD, sCategory)
                frmBilling.ShowDialog(Me)

                If sStatusSave <> "NEW" Then
                    fn_LoadSecurity()
                End If

            Catch oErr As Exception
                MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            Finally
                If Not frmBilling Is Nothing Then frmBilling.Dispose()
                frmBilling = Nothing

                Dim rowHandle As Integer = grv.LocateByValue(rowHandle, grv.Columns("KDBILLING"), sCode)
                If rowHandle > 0 Then grv.FocusedRowHandle = rowHandle

                If sStatusSave = "NEW" Then
                    sStatusSave = "NONE"
                    picAdd_Click()
                End If
            End Try
        End If
    End Sub
    Private Sub picUpdate_Click() Handles picUpdate.Click
        If grv.GetFocusedRowCellValue("KDBILLING") Is Nothing Then
            Exit Sub
        End If

        Dim ds = oBilling.GetData(grv.GetFocusedRowCellValue("KDBILLING"))
        If ds.PAYAMOUNT > 0 Then
            MsgBox("Sudah Bayar", MsgBoxStyle.Exclamation, Me.Text)
            Exit Sub
        End If

        If sCategory = 4 Then
            Dim frmBillingFarmasi As New frmBillingFarmasi
            Try
                frmBillingFarmasi.LoadMe(FORM_MODE.FORM_MODE_EDIT, sCategory, True, grv.GetFocusedRowCellValue("KDBILLING"))
                frmBillingFarmasi.ShowDialog(Me)
                fn_LoadSecurity()
            Catch oErr As Exception
                MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            Finally
                If Not frmBillingFarmasi Is Nothing Then frmBillingFarmasi.Dispose()
                frmBillingFarmasi = Nothing

                Dim rowHandle As Integer = grv.LocateByValue(rowHandle, grv.Columns("KDBILLING"), sCode)
                If rowHandle > 0 Then grv.FocusedRowHandle = rowHandle

                If sStatusSave = "NEW" Then
                    sStatusSave = "NONE"
                    picAdd_Click()
                End If
            End Try
        Else
            Dim frmBilling As New frmBilling
            Try
                frmBilling.LoadMe(FORM_MODE.FORM_MODE_EDIT, sCategory, grv.GetFocusedRowCellValue("KDBILLING"))
                frmBilling.ShowDialog(Me)
                fn_LoadSecurity()
            Catch oErr As Exception
                MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            Finally
                If Not frmBilling Is Nothing Then frmBilling.Dispose()
                frmBilling = Nothing

                Dim rowHandle As Integer = grv.LocateByValue(rowHandle, grv.Columns("KDBILLING"), sCode)
                If rowHandle > 0 Then grv.FocusedRowHandle = rowHandle

                If sStatusSave = "NEW" Then
                    sStatusSave = "NONE"
                    picAdd_Click()
                End If
            End Try
        End If
    End Sub
    Private Sub picDelete_Click() Handles picDelete.Click
        If grv.GetFocusedRowCellValue("KDBILLING") Is Nothing Then
            Exit Sub
        End If

        Dim KDBilling As String = grv.GetFocusedRowCellValue("KDBILLING")

        Dim ds = oBilling.GetData(grv.GetFocusedRowCellValue("KDBILLING"))
        If ds.PAYAMOUNT > 0 Then
            MsgBox("Sudah Bayar", MsgBoxStyle.Exclamation, Me.Text)
            Exit Sub
        End If

        If MsgBox(Statement.DeleteQuestion, MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, Me.Text) = MsgBoxResult.No Then Exit Sub

        Dim frmPesanDelete As New frmPesanDelete
        frmPesanDelete.ShowDialog(Me)

        If sDeletePesan <> "" Then
            Dim oDelete As New Setting.clsDelete

            oDelete.InsertData("BILLING", "DELETE", grv.GetFocusedRowCellValue("KDBILLING") & "Tanggal " & Now & " Oleh " & sUserID & " Alasan " & sDeletePesan, KDBilling)

            oBilling.DeleteData(grv.GetFocusedRowCellValue("KDBILLING"))
            fn_LoadSecurity()

            MsgBox(Statement.DeleteSuccess, MsgBoxStyle.Information, Me.Text)

        End If
    End Sub
    Private Sub picPrint_Click() Handles picPrint.Click
        'Try
        '    If grv.GetFocusedRowCellValue("KDBILLING") = String.Empty Then Exit Sub
        '    Dim ds = oBilling.GetData(grv.GetFocusedRowCellValue("KDBILLING"))

        '    sCetakSEP = False

        '    If fn_CariSEP() = "ADA" Then
        '        Dim rpt As New xtraSEP
        '        rpt.bindingSource.DataSource = ds
        '        Dim printTool As New DevExpress.XtraReports.UI.ReportPrintTool(rpt)
        '        printTool.ShowPreviewDialog(DevExpress.LookAndFeel.UserLookAndFeel.Default)
        '    Else
        '        Dim rpt As New xtraUmum
        '        rpt.bindingSource.DataSource = ds
        '        Dim printTool As New DevExpress.XtraReports.UI.ReportPrintTool(rpt)
        '        printTool.ShowPreviewDialog(DevExpress.LookAndFeel.UserLookAndFeel.Default)
        '    End If

        '    If sCetakSEP = True Then
        '        oBilling.UpdateCetak(ds.KDBilling)
        '    End If
        'Catch oErr As Exception
        '    MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        'End Try
    End Sub
    Private Sub picRefresh_Click() Handles picRefresh.Click
        fn_LoadSecurity()
    End Sub
    Private Sub PembayaranToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PembayaranToolStripMenuItem.Click
        If grv.GetFocusedRowCellValue("KDKUNJUNGAN") Is Nothing Then
            Exit Sub
        End If

        Dim ds = oBilling.GetData(grv.GetFocusedRowCellValue("KDBILLING"))
        If ds.PAYAMOUNT > 0 Then
            MsgBox("Sudah Bayar", MsgBoxStyle.Exclamation, Me.Text)
            Exit Sub
        End If

        Dim frmCashIn As New frmCashIn
        Try
            frmCashIn.fn_LoadKunjungan(grv.GetFocusedRowCellValue("KDKUNJUNGAN"))
            frmCashIn.LoadMe(FORM_MODE.FORM_MODE_ADD)
            frmCashIn.ShowDialog(Me)
            fn_LoadSecurity()
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        Finally
            If Not frmCashIn Is Nothing Then frmCashIn.Dispose()
            frmCashIn = Nothing

            Dim rowHandle As Integer = grv.LocateByValue(rowHandle, grv.Columns("KDCASHIN"), sCode)
            If rowHandle > 0 Then grv.FocusedRowHandle = rowHandle

            If sStatusSave = "NEW" Then
                sStatusSave = "NONE"
                picAdd_Click()
            End If
        End Try
    End Sub
#End Region
End Class