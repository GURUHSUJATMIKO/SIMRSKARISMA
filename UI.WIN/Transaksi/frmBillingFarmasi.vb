Imports DataAccess
Imports UI.WIN.MAIN.My.Resources
Imports System.Linq
Imports System.Data.SqlClient

Public Class frmBillingFarmasi
#Region "Declaration"
    Private oFormMode As FORM_MODE = FORM_MODE.FORM_MODE_VIEW
    Private sNoId As String
    Private isLoad As Boolean = False
    Private oBilling As New Transaksi.clsBilling
    Private sCategory As Integer = 0
    Private sFarmasi As Boolean = False
#End Region
#Region "Function"
    Public Sub LoadMe(ByVal FormMode As Integer, ByVal Category As Integer, ByVal Farmasi As Boolean, Optional ByVal NoId As String = "")
        oFormMode = FormMode
        sCategory = Category
        sFarmasi = Farmasi
        sNoId = NoId
    End Sub
    Private Sub Form_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        fn_ChangeFormState()
        fn_LoadLanguage()
        isLoad = True
    End Sub
    Public Sub fn_LoadLanguage()
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

            btnSaveNew.Caption = Caption.FormSaveNew
            btnSaveClose.Caption = Caption.FormSaveClose
            btnClose.Caption = Caption.FormClose
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub Form_Closing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        sCode = txtKDBILLING.Text.Trim.ToUpper
    End Sub
    Private Sub fn_ChangeFormState()
        fn_LoadItem_L2()
        fn_LoadItem()
        fn_LoadSatuan()
        fn_LoadSigna()
        fn_LoadCaraPakai()
        fn_LoadPenjamin()
        fn_LoadDokter()
        fn_LoadWarehouse()

        Select Case oFormMode
            Case FORM_MODE.FORM_MODE_VIEW
                fn_ViewMode(True)
                fn_LoadData()
            Case FORM_MODE.FORM_MODE_ADD
                fn_ViewMode(False)
                fn_EmptyMe()
            Case FORM_MODE.FORM_MODE_EDIT
                fn_ViewMode(False)
                fn_LoadData()
            Case Else
                fn_ViewMode(True)
        End Select
    End Sub
    Private Sub fn_ViewMode(ByVal Status As Boolean)
        btnSaveNew.Enabled = Not Status
        btnSaveClose.Enabled = Not Status
        btnPending.Enabled = Not Status

        deDATE.Properties.ReadOnly = Status
        grdKDWAREHOUSE.Properties.ReadOnly = Status
        txtKDPENDAFTARAN.Properties.ReadOnly = True
        grdKDDOCTOR_H.Properties.ReadOnly = True
        grdKDPENJAMIN.Properties.ReadOnly = True
        grvDetail.OptionsBehavior.ReadOnly = Status

        If sFarmasi = False Then
            colPPN_PERSEN.VisibleIndex = 10
            colPPN.VisibleIndex = 11
            colISTUSLAH.VisibleIndex = 12
            colTUSLAH.VisibleIndex = 13
            colGRANDTOTAL.VisibleIndex = 14
            lPPN.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            lTUSLAH.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            lGRANDTOTAL.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        Else
            colPPN_PERSEN.VisibleIndex = -1
            colPPN.VisibleIndex = -1
            colISTUSLAH.VisibleIndex = -1
            colTUSLAH.VisibleIndex = -1
            colGRANDTOTAL.VisibleIndex = -1
            lPPN.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            lTUSLAH.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            lGRANDTOTAL.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        End If

    End Sub
    Private Sub fn_EmptyMe()
        txtKDBILLING.Text = "<--- AUTO --->"
        deDATE.DateTime = Now
        txtKDKUNJUNGAN.ResetText()
        grdKDDOCTOR_H.ResetText()
        txtKDPENDAFTARAN.ResetText()
        grdKDPENJAMIN.ResetText()
        grdKDWAREHOUSE.Text = sKDWAREHOUSEAUTO
    End Sub
    Private Sub fn_LoadData()
        Try
            ' ***** HEADER *****
            Dim ds = oBilling.GetData(sNoId)

            With ds
                txtKDBILLING.Text = .KDBILLING
                deDATE.DateTime = .DATE
                txtKDPENDAFTARAN.Text = .KDPENDAFTARAN
                grdKDWAREHOUSE.Text = .KDWAREHOUSE
                grdKDDOCTOR_H.Text = .KDDOCTOR
                grdKDPENJAMIN.Text = .KDPENJAMIN
                txtKDKUNJUNGAN.Text = .KDKUNJUNGAN
                txtSUBTOTAL.Text = .SUBTOTAL
                txtPPN.Text = .TAX
                txtTUSLAH.Text = .DISCOUNT
                txtGRANDTOTAL.Text = .GRANDTOTAL
                BindingSource.DataSource = oBilling.GetDataDetailFarmasi(sNoId)
                grdDetail.DataSource = BindingSource
            End With
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Function fn_Validate() As Boolean
        Try
            fn_Validate = True

            If txtKDPENDAFTARAN.Text = String.Empty Then
                txtKDPENDAFTARAN.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                txtKDPENDAFTARAN.ErrorText = Statement.ErrorRequired

                txtKDPENDAFTARAN.Focus()
                fn_Validate = False
                Exit Function
            End If
            If grdKDPENJAMIN.Text = String.Empty Then
                grdKDPENJAMIN.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                grdKDPENJAMIN.ErrorText = Statement.ErrorRequired

                grdKDPENJAMIN.Focus()
                fn_Validate = False
                Exit Function
            End If
            If txtKDKUNJUNGAN.Text = String.Empty Then
                txtKDKUNJUNGAN.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                txtKDKUNJUNGAN.ErrorText = Statement.ErrorRequired

                txtKDKUNJUNGAN.Focus()
                fn_Validate = False
                Exit Function
            End If
            If grdKDDOCTOR_H.Text = String.Empty Then
                grdKDDOCTOR_H.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                grdKDDOCTOR_H.ErrorText = Statement.ErrorRequired

                grdKDDOCTOR_H.Focus()
                fn_Validate = False
                Exit Function
            End If
            If grdKDWAREHOUSE.Text = String.Empty Then
                grdKDWAREHOUSE.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                grdKDWAREHOUSE.ErrorText = Statement.ErrorRequired

                grdKDWAREHOUSE.Focus()
                fn_Validate = False
                Exit Function
            End If

            grvDetail.UpdateCurrentRow()

            If grvDetail.RowCount < 2 Then
                MsgBox(Statement.ErrorDetail, MsgBoxStyle.Exclamation, Me.Text)
                fn_Validate = False
                Exit Function
            End If
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Function
    Private Function fn_Save() As Boolean
        Try
            ' ***** HEADER *****
            Dim ds = oBilling.GetStructureHeader
            With ds
                Try
                    .DATECREATED = oBilling.GetData(sNoId).DATECREATED
                Catch oErr As Exception
                    .DATECREATED = Now
                End Try
                .DATEUPDATED = Now
                .CATEGORY = sCategory
                .DATE = deDATE.DateTime
                .KDDOCTOR = grdKDDOCTOR_H.EditValue
                .KDWAREHOUSE = grdKDWAREHOUSE.EditValue
                .KDBILLING = sNoId
                .KDPENDAFTARAN = txtKDPENDAFTARAN.Text
                .KDPENJAMIN = grdKDPENJAMIN.EditValue
                .KDKUNJUNGAN = txtKDKUNJUNGAN.Text
                .SUBTOTAL = CDec(txtSUBTOTAL.Text)
                .DISCOUNT = CDec(txtTUSLAH.Text)
                .TAX = CDec(txtPPN.Text)
                .GRANDTOTAL = CDec(txtGRANDTOTAL.Text)
                Try
                    .PAYAMOUNT = oBilling.GetData(sNoId).PAYAMOUNT
                Catch ex As Exception
                    .PAYAMOUNT = 0
                End Try
                .KDUSER = sUserID
                .MEMO = txtMEMO.Text
            End With

            ' ***** DETIL *****
            Dim arrDetail = oBilling.GetStructureDetailFarmasiList
            For i As Integer = 0 To grvDetail.RowCount - 2
                Dim dsDetail = oBilling.GetStructureDetailFarmasi
                With dsDetail
                    .KDBILLING = ds.KDBILLING
                    .KDDOCTOR = grdKDDOCTOR_H.EditValue
                    .KDITEM = grvDetail.GetRowCellValue(i, colKDITEM)
                    .KDITEM_L1 = grvDetail.GetRowCellValue(i, colKDITEM_L1)
                    .KDITEM_L2 = grvDetail.GetRowCellValue(i, colKDITEM_L2)
                    .KDITEM_L3 = grvDetail.GetRowCellValue(i, colKDITEM_L3)
                    .KDITEM_L4 = grvDetail.GetRowCellValue(i, colKDITEM_L4)
                    .KDITEM_L5 = grvDetail.GetRowCellValue(i, colKDITEM_L5)
                    .KDITEM_L6 = grvDetail.GetRowCellValue(i, colKDITEM_L6)
                    .KDUOM = grvDetail.GetRowCellValue(i, colKDUOM)
                    .KDCARAPAKAI = grvDetail.GetRowCellValue(i, colKDCARAPAKAI)
                    .KDSIGNA = grvDetail.GetRowCellValue(i, colKDSIGNA)
                    .ISPROLANIS = grvDetail.GetRowCellValue(i, colISPROLANIS)
                    .ISTUSLAH = grvDetail.GetRowCellValue(i, colISTUSLAH)
                    .DATE_EXPIRE = CDate(grvDetail.GetRowCellValue(i, colDATE_EXPIRE))
                    .SEQ = i
                    .HARI = grvDetail.GetRowCellValue(i, colHARI)
                    .QTY = grvDetail.GetRowCellValue(i, colQTY)
                    .PRICE = grvDetail.GetRowCellValue(i, colPRICE)
                    .SUBTOTAL = grvDetail.GetRowCellValue(i, colSUBTOTAL)
                    .PPN_PERSEN = grvDetail.GetRowCellValue(i, colPPN_PERSEN)
                    .PPN = grvDetail.GetRowCellValue(i, colPPN)
                    .TUSLAH = grvDetail.GetRowCellValue(i, colTUSLAH)
                    .GRANDTOTAL = grvDetail.GetRowCellValue(i, colGRANDTOTAL)
                    .REMARKS = grvDetail.GetRowCellValue(i, colREMARKS)
                End With
                arrDetail.Add(dsDetail)
            Next

            If oFormMode = FORM_MODE.FORM_MODE_ADD Then
                Try
                    fn_Save = oBilling.InsertData(ds, Nothing, arrDetail, Nothing)
                Catch oErr As Exception
                    MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            ElseIf oFormMode = FORM_MODE.FORM_MODE_EDIT Then
                Try
                    fn_Save = oBilling.UpdateData(ds, Nothing, arrDetail, Nothing)
                Catch oErr As Exception
                    MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            End If
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            fn_Save = False
        End Try
    End Function
#End Region
#Region "Grid Method"
    Private Sub DeleteToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DeleteToolStripMenuItem.Click
        If oFormMode = FORM_MODE.FORM_MODE_VIEW Then Exit Sub
        grvDetail.DeleteSelectedRows()
    End Sub
    Private Sub OnValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles grvDetail.FocusedRowChanged
        If isLoad Then
            Calculate()
        End If
    End Sub
    Private Sub Calculate()
        Dim sSubTotal = 0
        Dim sPPN = 0
        Dim sTUSLAH = 0

        For i As Integer = 0 To grvDetail.RowCount - 2
            sSubTotal += CDec(grvDetail.GetRowCellValue(i, colSUBTOTAL))
        Next
        For i As Integer = 0 To grvDetail.RowCount - 2
            sPPN += CDec(grvDetail.GetRowCellValue(i, colPPN))
        Next
        For i As Integer = 0 To grvDetail.RowCount - 2
            sTUSLAH += CDec(grvDetail.GetRowCellValue(i, colTUSLAH))
        Next

        txtSUBTOTAL.Text = sSubTotal
        txtPPN.Text = sPPN
        txtTUSLAH.Text = sTUSLAH
        txtGRANDTOTAL.Text = CDec(txtSUBTOTAL.Text) + CDec(txtPPN.Text) + CDec(txtTUSLAH.Text)
    End Sub
    Private Sub grvDetail_CellValueChanged(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs) Handles grvDetail.CellValueChanged
        If txtKDKUNJUNGAN.Text = "" Then
            MsgBox("Pasien Kosong", MsgBoxStyle.Exclamation, Me.Text)
            grvDetail.CancelUpdateCurrentRow()
            Exit Sub
        End If

        If e.Column.Name = colKDITEM.Name Then
            Dim oItem As New Reference.clsItem
            Try
                If grvDetail.GetFocusedRowCellValue(colKDITEM) IsNot Nothing Then
                    Dim ds = oItem.GetDataDetail_UOM(grvDetail.GetFocusedRowCellValue(colKDITEM))
                    If ds IsNot Nothing Then
                        grvDetail.SetFocusedRowCellValue(colKDUOM, ds.FirstOrDefault(Function(x) x.RATE = 1).KDUOM)
                        grvDetail.SetFocusedRowCellValue(colKDITEM_L1, oItem.GetData(grvDetail.GetFocusedRowCellValue(colKDITEM)).KDITEM_L1)
                        grvDetail.SetFocusedRowCellValue(colKDITEM_L2, oItem.GetData(grvDetail.GetFocusedRowCellValue(colKDITEM)).KDITEM_L2)
                        grvDetail.SetFocusedRowCellValue(colKDITEM_L3, oItem.GetData(grvDetail.GetFocusedRowCellValue(colKDITEM)).KDITEM_L3)
                        grvDetail.SetFocusedRowCellValue(colKDITEM_L4, oItem.GetData(grvDetail.GetFocusedRowCellValue(colKDITEM)).KDITEM_L4)
                        grvDetail.SetFocusedRowCellValue(colKDITEM_L5, oItem.GetData(grvDetail.GetFocusedRowCellValue(colKDITEM)).KDITEM_L5)
                        grvDetail.SetFocusedRowCellValue(colKDITEM_L6, oItem.GetData(grvDetail.GetFocusedRowCellValue(colKDITEM)).KDITEM_L6)
                        grvDetail.SetFocusedRowCellValue(colHARI, 1)
                        grvDetail.SetFocusedRowCellValue(colREMARKS, "-")
                        grvDetail.SetFocusedRowCellValue(colKDSIGNA, oItem.DefautlSigna)
                        grvDetail.SetFocusedRowCellValue(colKDCARAPAKAI, oItem.DefautlCaraPakai)
                        grvDetail.SetFocusedRowCellValue(colREMARKS, "-")
                        grvDetail.SetFocusedRowCellValue(colDATE_EXPIRE, oItem.GetData(grvDetail.GetFocusedRowCellValue(colKDITEM)).DATE_EXPIRE)
                        grvDetail.SetFocusedRowCellValue(colPPN_PERSEN, 10)
                        grvDetail.SetFocusedRowCellValue(colISTUSLAH, IIf(grdKDPENJAMIN.Text = "BPJS KESEHATAN", False, True))
                    Else
                        MsgBox(Statement.ErrorUOM, MsgBoxStyle.Exclamation, Me.Text)
                        grvDetail.CancelUpdateCurrentRow()
                    End If
                End If
            Catch oErr As Exception
                MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            End Try
        ElseIf e.Column.Name = colKDUOM.Name Then
            Dim oItem As New Reference.clsItem
            Try
                If grvDetail.GetFocusedRowCellValue(colKDITEM) IsNot Nothing And grvDetail.GetFocusedRowCellValue(colKDUOM) IsNot Nothing Then
                    Dim ds = oItem.GetDataDetail_UOM(grvDetail.GetFocusedRowCellValue(colKDITEM), grvDetail.GetFocusedRowCellValue(colKDUOM))

                    If ds IsNot Nothing Then
                        grvDetail.SetFocusedRowCellValue(colPRICE, ds.PRICEPURCHASESTANDARD + (ds.PRICEPURCHASESTANDARD * (ds.MARGIN / 100)))
                    Else
                        MsgBox(Statement.ErrorUOM, MsgBoxStyle.Exclamation, Me.Text)

                        Dim sItem = grvDetail.GetFocusedRowCellValue(colKDITEM)
                        grvDetail.CancelUpdateCurrentRow()

                        grvDetail.AddNewRow()
                        grvDetail.SetFocusedRowCellValue(colKDITEM, sItem)
                    End If
                End If
            Catch oErr As Exception
                MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            End Try
        ElseIf e.Column.Name = colHARI.Name Or e.Column.Name = colKDSIGNA.Name Then
            If grvDetail.GetFocusedRowCellValue(colKDSIGNA) IsNot Nothing Then
                Dim oSigna As New Reference.clsSigna
                Dim oItem_L2 As New Reference.clsItem_L2
                Dim QTY As Decimal = 0

                Dim dsSigna = oSigna.GetData(grvDetail.GetFocusedRowCellValue(colKDSIGNA))
                If dsSigna IsNot Nothing Then
                    QTY = (oSigna.GetData(grvDetail.GetFocusedRowCellValue(colKDSIGNA)).SIGNA_1 * oSigna.GetData(grvDetail.GetFocusedRowCellValue(colKDSIGNA)).SIGNA_2) * grvDetail.GetFocusedRowCellValue(colHARI)
                    grvDetail.SetFocusedRowCellValue(colQTY, QTY)
                End If
                Dim dsItem_L2 = oItem_L2.GetData(grvDetail.GetFocusedRowCellValue(colKDITEM_L2))
                If dsItem_L2 IsNot Nothing Then
                    If dsItem_L2.MEMO = "OBAT KRONIS" Then
                        If QTY = 23 Or QTY = 46 Or QTY = 96 Or QTY = 184 Then
                            grvDetail.SetFocusedRowCellValue(colISPROLANIS, True)
                        Else
                            grvDetail.SetFocusedRowCellValue(colISPROLANIS, False)
                        End If
                    Else
                        grvDetail.SetFocusedRowCellValue(colISPROLANIS, False)
                    End If
                Else
                    grvDetail.SetFocusedRowCellValue(colISPROLANIS, False)
                End If
            End If

        ElseIf e.Column.Name = colQTY.Name Or e.Column.Name = colPRICE.Name Or e.Column.Name = colPPN_PERSEN.Name Then
            Dim Persen As Decimal = grvDetail.GetFocusedRowCellValue(colPRICE) * (grvDetail.GetFocusedRowCellValue(colPPN_PERSEN) / 100)

            grvDetail.SetFocusedRowCellValue(colPPN, Persen)

            Dim sSubTotal As Decimal = CDec(grvDetail.GetFocusedRowCellValue(colQTY)) * (CDec(grvDetail.GetFocusedRowCellValue(colPRICE) + grvDetail.GetFocusedRowCellValue(colPPN)))

            grvDetail.SetFocusedRowCellValue(colSUBTOTAL, sSubTotal)

        ElseIf e.Column.Name = colISTUSLAH.Name Or e.Column.Name = colSUBTOTAL.Name Then
            If grvDetail.GetFocusedRowCellValue(colISTUSLAH) = True Then
                grvDetail.SetFocusedRowCellValue(colTUSLAH, 1000)
            Else
                grvDetail.SetFocusedRowCellValue(colTUSLAH, 0)
            End If
            grvDetail.SetFocusedRowCellValue(colGRANDTOTAL, grvDetail.GetFocusedRowCellValue(colSUBTOTAL) + grvDetail.GetFocusedRowCellValue(colTUSLAH))
        End If
    End Sub
#End Region
#Region "Command Button"
    Private Sub frmBilling_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        Select Case e.KeyCode
            Case Keys.F12
                btnClose_Click()
            Case Keys.F2
                If btnSaveNew.Enabled = True Then
                    btnSaveNew_Click()
                End If
            Case Keys.F3
                If btnSaveClose.Enabled = True Then
                    btnSaveClose_Click()
                End If
            Case Keys.F7
                If btnPending.Enabled = True Then
                    btnPending_Click()
                End If
        End Select
    End Sub
    Private Sub btnPending_Click() Handles btnPending.ItemClick
        Dim frmBilling As New frmBilling
        Try
            frmBilling.LoadMe(FORM_MODE.FORM_MODE_ADD, sCategory)
            frmBilling.MdiParent = Me.MdiParent
            frmBilling.Show()
            frmBilling.WindowState = FormWindowState.Maximized
        Catch ex As Exception
            MsgBox(Statement.SaveSuccess, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub btnSaveNew_Click() Handles btnSaveNew.ItemClick
        If fn_Validate() = False Then Exit Sub
        If MsgBox(Statement.SaveQuestion, MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, Me.Text) = MsgBoxResult.No Then Exit Sub
        If fn_Save() = False Then
            MsgBox(Statement.SaveFail, MsgBoxStyle.Exclamation, Me.Text)
        Else
            MsgBox(Statement.SaveSuccess, MsgBoxStyle.Information, Me.Text)
            sStatusSave = "NEW"
            Me.Close()
        End If
    End Sub
    Private Sub btnSaveClose_Click() Handles btnSaveClose.ItemClick
        If fn_Validate() = False Then Exit Sub
        If MsgBox(Statement.SaveQuestion, MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, Me.Text) = MsgBoxResult.No Then Exit Sub
        If fn_Save() = False Then
            MsgBox(Statement.SaveFail, MsgBoxStyle.Exclamation, Me.Text)
        Else
            MsgBox(Statement.SaveSuccess, MsgBoxStyle.Information, Me.Text)
            Me.Close()
        End If
    End Sub
    Private Sub btnClose_Click() Handles btnClose.ItemClick
        Me.Close()
    End Sub
#End Region
#Region "Lookup / Event"
    Private Sub fn_LoadDokter()
        Dim oDokter As New Reference.clsDoctor
        Try
            grdKDDOCTOR_H.Properties.DataSource = oDokter.GetData.Where(Function(x) x.ISACTIVE = True).ToList()
            grdKDDOCTOR_H.Properties.ValueMember = "KDDOCTOR"
            grdKDDOCTOR_H.Properties.DisplayMember = "NAME_DISPLAY"
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadPenjamin()
        Dim oPenjamin As New Reference.clsPENJAMIN
        Try
            grdKDPENJAMIN.Properties.DataSource = oPenjamin.GetData.Where(Function(x) x.ISACTIVE = True).ToList()
            grdKDPENJAMIN.Properties.ValueMember = "KDPENJAMIN"
            grdKDPENJAMIN.Properties.DisplayMember = "MEMO"
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadSigna()
        Dim oSigna As New Reference.clsSigna
        Try
            grdKDSIGNA.DataSource = oSigna.GetData.Where(Function(x) x.ISACTIVE = True).ToList()
            grdKDSIGNA.ValueMember = "KDSIGNA"
            grdKDSIGNA.DisplayMember = "MEMO"
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadCaraPakai()
        Dim oCaraPakai As New Reference.clsCaraPakai
        Try
            grdKDCARAPAKAI.DataSource = oCaraPakai.GetData.Where(Function(x) x.ISACTIVE = True).ToList()
            grdKDCARAPAKAI.ValueMember = "KDCARAPAKAI"
            grdKDCARAPAKAI.DisplayMember = "MEMO"
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadItem()
        If grdKDWAREHOUSE.Text = String.Empty Then Exit Sub
        Try
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
            SQL &= "A.KDITEM "
            SQL &= ",A.NMITEM1 "
            SQL &= ",A.NMITEM2 "
            SQL &= ",A.NMITEM3 "
            SQL &= ",STOK = B.AMOUNT "
            SQL &= "FROM "
            SQL &= "M_ITEM A "
            SQL &= "INNER JOIN M_ITEM_WAREHOUSE B "
            SQL &= "ON A.KDITEM = B.KDITEM "
            SQL &= "INNER JOIN M_ITEM_UOM C "
            SQL &= "ON A.KDITEM = C.KDITEM AND B.KDITEM = C.KDITEM "
            SQL &= "INNER JOIN M_ITEM_L1 D "
            SQL &= "ON A.KDITEM_L1 = D.KDITEM_L1 "
            SQL &= "WHERE C.RATE = 1 "
            SQL &= "AND A.ISACTIVE = 1 "
            SQL &= "AND B.KDWAREHOUSE = '" & grdKDWAREHOUSE.EditValue & "' "
            SQL &= "AND D.MEMO = 'OBAT' "

            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "ITEM")

            grdKDITEM.DataSource = ds.Tables("ITEM")
            grdKDITEM.ValueMember = "KDITEM"
            grdKDITEM.DisplayMember = "NMITEM2"

            If oConn.State = ConnectionState.Open Then
                oConn.Close()
            End If

        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadSatuan()
        Dim oUom As New Reference.clsUOM
        Try
            grdKDUOM.DataSource = oUom.GetData.Where(Function(x) x.ISACTIVE = True).ToList()
            grdKDUOM.ValueMember = "KDUOM"
            grdKDUOM.DisplayMember = "MEMO"
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadWarehouse()
        'Dim oWarehouse As New Reference.clsWarehouse
        'Try
        '    grdKDWAREHOUSE.Properties.DataSource = oWarehouse.GetData.Where(Function(x) x.ISACTIVE = True).ToList()
        '    grdKDWAREHOUSE.Properties.ValueMember = "KDWAREHOUSE"
        '    grdKDWAREHOUSE.Properties.DisplayMember = "NAME_DISPLAY"
        'Catch oErr As Exception
        '    MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        'End Try

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
    Private Sub fn_LoadItem_L2()
        Dim oItem_L2 As New Reference.clsItem_L2
        Try
            grdKDITEM_L2.DataSource = oItem_L2.GetData.Where(Function(x) x.ISACTIVE = True).ToList()
            grdKDITEM_L2.ValueMember = "KDITEM_L2"
            grdKDITEM_L2.DisplayMember = "MEMO"
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub txtCARI_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtCARI.KeyPress
        If Asc(e.KeyChar) = 13 Then
            fn_LoadPendaftaranRekamMedisList(txtCARI.Text)
        End If
    End Sub
    Private Sub fn_LoadPendaftaranRekamMedisList(ByVal sParameter As String)
        Try
            If sParameter = String.Empty Then Exit Sub
            If cboCARI.SelectedIndex = 0 Then
                Dim oPendaftaran_KunjunganPoli As New Admission.clsPendaftaran_KunjunganPoli
                Dim oPendaftaran_KunjunganRuangan As New Admission.clsPendaftaran_KunjunganRuangan

                If sCategory = 0 Then
                    Dim dsCustomerRJList = From x In oPendaftaran_KunjunganPoli.GetDataByRekamMedis(sParameter)
                                           Select Kode = x.KDKUNJUNGAN_POLI, TanggalDatang = x.S_PENDAFTARAN_H.DATE.ToString("dd-MM-yyyy"), Tujuan = x.M_DEPARTMENT.NAME_DISPLAY, Penjamin = x.M_PENJAMIN.MEMO, NoSEP = x.S_PENDAFTARAN_H.NOMORSEP, NoKartuBPJS = x.S_PENDAFTARAN_H.KARTUBPJS, Pasien = x.S_PENDAFTARAN_H.M_CUSTOMER.NAME_DISPLAY, TanggalLahir = x.S_PENDAFTARAN_H.M_CUSTOMER.TANGGALLAHIR.ToString("dd-MM-yyyy")

                    grdCARIKDREGAWAL.Properties.DataSource = dsCustomerRJList.ToList()
                    grdCARIKDREGAWAL.Properties.ValueMember = "Kode"
                    grdCARIKDREGAWAL.Properties.DisplayMember = "Pasien"

                ElseIf sCategory = 1 Then
                    Dim dsCustomerRIList = From x In oPendaftaran_KunjunganRuangan.GetDataByRekamMedis(sParameter)
                                           Select Kode = x.KDKUNJUNGAN_RUANGAN, TanggalDatang = x.S_PENDAFTARAN_H.DATE.ToString("dd-MM-yyyy"), Tujuan = x.M_RUANGRAWAT.NAME_DISPLAY, Penjamin = x.M_PENJAMIN.MEMO, NoSEP = x.S_PENDAFTARAN_H.NOMORSEP, NoKartuBPJS = x.S_PENDAFTARAN_H.KARTUBPJS, Pasien = x.S_PENDAFTARAN_H.M_CUSTOMER.NAME_DISPLAY, TanggalLahir = x.S_PENDAFTARAN_H.M_CUSTOMER.TANGGALLAHIR.ToString("dd-MM-yyyy")

                    grdCARIKDREGAWAL.Properties.DataSource = dsCustomerRIList.ToList()
                    grdCARIKDREGAWAL.Properties.ValueMember = "Kode"
                    grdCARIKDREGAWAL.Properties.DisplayMember = "Pasien"

                Else
                    Dim dsCustomerRJList = From x In oPendaftaran_KunjunganPoli.GetDataByRekamMedis(sParameter)
                                           Select Kode = x.KDKUNJUNGAN_POLI, TanggalDatang = x.S_PENDAFTARAN_H.DATE.ToString("dd-MM-yyyy"), Tujuan = x.M_DEPARTMENT.NAME_DISPLAY, Penjamin = x.M_PENJAMIN.MEMO, NoSEP = x.S_PENDAFTARAN_H.NOMORSEP, NoKartuBPJS = x.S_PENDAFTARAN_H.KARTUBPJS, Pasien = x.S_PENDAFTARAN_H.M_CUSTOMER.NAME_DISPLAY, TanggalLahir = x.S_PENDAFTARAN_H.M_CUSTOMER.TANGGALLAHIR.ToString("dd-MM-yyyy")

                    Dim dsCustomerRIList = From x In oPendaftaran_KunjunganRuangan.GetDataByRekamMedis(sParameter)
                                           Select Kode = x.KDKUNJUNGAN_RUANGAN, TanggalDatang = x.S_PENDAFTARAN_H.DATE.ToString("dd-MM-yyyy"), Tujuan = x.M_RUANGRAWAT.NAME_DISPLAY, Penjamin = x.M_PENJAMIN.MEMO, NoSEP = x.S_PENDAFTARAN_H.NOMORSEP, NoKartuBPJS = x.S_PENDAFTARAN_H.KARTUBPJS, Pasien = x.S_PENDAFTARAN_H.M_CUSTOMER.NAME_DISPLAY, TanggalLahir = x.S_PENDAFTARAN_H.M_CUSTOMER.TANGGALLAHIR.ToString("dd-MM-yyyy")

                    Dim dsCustomerList = dsCustomerRJList.Union(dsCustomerRIList)

                    grdCARIKDREGAWAL.Properties.DataSource = dsCustomerList.ToList()
                    grdCARIKDREGAWAL.Properties.ValueMember = "Kode"
                    grdCARIKDREGAWAL.Properties.DisplayMember = "Pasien"
                End If

            Else
                Dim oPendaftaran_KunjunganPoli As New Admission.clsPendaftaran_KunjunganPoli
                Dim oPendaftaran_KunjunganRuangan As New Admission.clsPendaftaran_KunjunganRuangan

                If sCategory = 0 Then
                    Dim dsCustomerRJList = From x In oPendaftaran_KunjunganPoli.GetDataByRekamNama(sParameter)
                                           Select Kode = x.KDKUNJUNGAN_POLI, Tujuan = x.M_DEPARTMENT.NAME_DISPLAY, Penjamin = x.M_PENJAMIN.MEMO, NoSEP = x.S_PENDAFTARAN_H.NOMORSEP, NoKartuBPJS = x.S_PENDAFTARAN_H.KARTUBPJS, Pasien = x.S_PENDAFTARAN_H.M_CUSTOMER.NAME_DISPLAY, TanggalLahir = x.S_PENDAFTARAN_H.M_CUSTOMER.TANGGALLAHIR.ToString("dd-MM-yyyy")

                    grdCARIKDREGAWAL.Properties.DataSource = dsCustomerRJList.ToList()
                    grdCARIKDREGAWAL.Properties.ValueMember = "Kode"
                    grdCARIKDREGAWAL.Properties.DisplayMember = "Pasien"

                ElseIf sCategory = 1 Then
                    Dim dsCustomerRIList = From x In oPendaftaran_KunjunganRuangan.GetDataByRekamNama(sParameter)
                                           Select Kode = x.KDKUNJUNGAN_RUANGAN, Tujuan = x.M_RUANGRAWAT.NAME_DISPLAY, Penjamin = x.M_PENJAMIN.MEMO, NoSEP = x.S_PENDAFTARAN_H.NOMORSEP, NoKartuBPJS = x.S_PENDAFTARAN_H.KARTUBPJS, Pasien = x.S_PENDAFTARAN_H.M_CUSTOMER.NAME_DISPLAY, TanggalLahir = x.S_PENDAFTARAN_H.M_CUSTOMER.TANGGALLAHIR.ToString("dd-MM-yyyy")

                    grdCARIKDREGAWAL.Properties.DataSource = dsCustomerRIList.ToList()
                    grdCARIKDREGAWAL.Properties.ValueMember = "Kode"
                    grdCARIKDREGAWAL.Properties.DisplayMember = "Pasien"

                Else
                    Dim dsCustomerRJList = From x In oPendaftaran_KunjunganPoli.GetDataByRekamNama(sParameter)
                                           Select Kode = x.KDKUNJUNGAN_POLI, Tujuan = x.M_DEPARTMENT.NAME_DISPLAY, Penjamin = x.M_PENJAMIN.MEMO, NoSEP = x.S_PENDAFTARAN_H.NOMORSEP, NoKartuBPJS = x.S_PENDAFTARAN_H.KARTUBPJS, Pasien = x.S_PENDAFTARAN_H.M_CUSTOMER.NAME_DISPLAY, TanggalLahir = x.S_PENDAFTARAN_H.M_CUSTOMER.TANGGALLAHIR.ToString("dd-MM-yyyy")

                    Dim dsCustomerRIList = From x In oPendaftaran_KunjunganRuangan.GetDataByRekamNama(sParameter)
                                           Select Kode = x.KDKUNJUNGAN_RUANGAN, Tujuan = x.M_RUANGRAWAT.NAME_DISPLAY, Penjamin = x.M_PENJAMIN.MEMO, NoSEP = x.S_PENDAFTARAN_H.NOMORSEP, NoKartuBPJS = x.S_PENDAFTARAN_H.KARTUBPJS, Pasien = x.S_PENDAFTARAN_H.M_CUSTOMER.NAME_DISPLAY, TanggalLahir = x.S_PENDAFTARAN_H.M_CUSTOMER.TANGGALLAHIR.ToString("dd-MM-yyyy")

                    Dim dsCustomerList = dsCustomerRJList.Union(dsCustomerRIList)

                    grdCARIKDREGAWAL.Properties.DataSource = dsCustomerList.ToList()
                    grdCARIKDREGAWAL.Properties.ValueMember = "Kode"
                    grdCARIKDREGAWAL.Properties.DisplayMember = "Pasien"
                End If

            End If

            grdCARIKDREGAWAL.ShowPopup()
            grvCARIKDPENDAFTARAN_AWAL.BestFitColumns()
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub grdCARIKDREGAWAL_KeyPress(sender As Object, e As KeyPressEventArgs) Handles grdCARIKDREGAWAL.KeyPress
        Dim oPendafataran_KunjunganPoli As New Admission.clsPendaftaran_KunjunganPoli
        Dim dsRJ = oPendafataran_KunjunganPoli.GetData(grdCARIKDREGAWAL.EditValue)
        If dsRJ IsNot Nothing Then
            txtKDKUNJUNGAN.Text = dsRJ.KDKUNJUNGAN_POLI
            txtKDPENDAFTARAN.Text = dsRJ.KDPENDAFTARAN
            grdKDPENJAMIN.Text = dsRJ.KDPENJAMIN
            grdKDDOCTOR_H.Text = dsRJ.KDDOCTOR
        Else
            Dim oPendafataran_KunjunganRuangan As New Admission.clsPendaftaran_KunjunganRuangan
            Dim dsRI = oPendafataran_KunjunganRuangan.GetData(grdCARIKDREGAWAL.EditValue)
            If dsRI IsNot Nothing Then
                txtKDKUNJUNGAN.Text = dsRI.KDKUNJUNGAN_RUANGAN
                txtKDPENDAFTARAN.Text = dsRI.KDPENDAFTARAN
                grdKDPENJAMIN.Text = dsRI.KDPENJAMIN
                grdKDDOCTOR_H.Text = dsRI.KDDOCTOR
            Else
                fn_EmptyMe()
            End If
        End If
    End Sub
    Private Sub grdKDWAREHOUSE_EditValueChanged(sender As Object, e As EventArgs) Handles grdKDWAREHOUSE.EditValueChanged
        fn_LoadItem()
    End Sub
#End Region
End Class