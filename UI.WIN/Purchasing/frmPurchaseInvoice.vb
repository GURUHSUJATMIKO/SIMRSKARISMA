Imports DataAccess
Imports UI.WIN.MAIN.My.Resources
Imports System.Linq
Imports System.Data.SqlClient

Public Class frmPurchaseInvoice
#Region "Declaration"
    Private oFormMode As FORM_MODE = FORM_MODE.FORM_MODE_VIEW
    Private sNoId As String
    Private isLoad As Boolean = False
    Private oPurchaseInvoice As New Purchasing.clsPurchaseInvoice

#End Region
#Region "Function"
    Public Sub LoadMe(ByVal FormMode As Integer, Optional ByVal NoId As String = "")
        oFormMode = FormMode
        sNoId = NoId
    End Sub
    Private Sub Form_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        fn_ChangeFormState()
        fn_LoadLanguage()
        isLoad = True
    End Sub
    Public Sub fn_LoadLanguage()
        Try
            Me.Text = PurchaseInvoice.TITLE

            lKDPI.Text = "Kode"
            lDATE.Text = "Tanggal Faktur"
            lNOFAKTUR.Text = "No Faktur"
            lDATE_CREATED.Text = "Tanggal Entri"
            lDATEDUE.Text = PurchaseInvoice.DATEDUE
            lKDVENDOR.Text = PurchaseInvoice.KDVENDOR & " *"
            lKDWAREHOUSE.Text = PurchaseInvoice.KDWAREHOUSE & " *"

            lSUBTOTAL.Text = PurchaseInvoice.SUBTOTAL
            lDISCOUNT_PERSEN.Text = PurchaseInvoice.DISCOUNT
            lPPN_PERSEN.Text = PurchaseInvoice.TAX
            lMATERAI.Text = "Materai"
            lGRANDTOTAL.Text = PurchaseInvoice.GRANDTOTAL

            tab1.Text = PurchaseInvoice.TAB_DETAIL
            tab2.Text = PurchaseInvoice.TAB_MEMO

            grvDetail.Columns("KDITEM").Caption = PurchaseInvoice.DETAIL_KDITEM
            grvDetail.Columns("KDUOM").Caption = PurchaseInvoice.DETAIL_KDUOM
            grvDetail.Columns("QTY").Caption = PurchaseInvoice.DETAIL_QTY
            grvDetail.Columns("PRICE").Caption = PurchaseInvoice.DETAIL_PRICE
            grvDetail.Columns("SUBTOTAL").Caption = PurchaseInvoice.DETAIL_SUBTOTAL
            grvDetail.Columns("DISCOUNT").Caption = PurchaseInvoice.DETAIL_DISCOUNT
            grvDetail.Columns("GRANDTOTAL").Caption = PurchaseInvoice.DETAIL_GRANDTOTAL
            grvDetail.Columns("REMARKS").Caption = PurchaseInvoice.DETAIL_REMARKS

            grvKDVENDOR.Columns("NAME_DISPLAY").Caption = Vendor.NAME_DISPLAY
            grvKDWAREHOUSE.Columns("NAME_DISPLAY").Caption = Warehouse.NAME_DISPLAY

            'grvKDITEM.Columns("NMITEM1").Caption = Item.NMITEM1
            grvKDITEM.Columns("NMITEM2").Caption = Item.NMITEM2
            'grvKDITEM.Columns("NMITEM3").Caption = Item.NMITEM3

            grvKDUOM.Columns("MEMO").Caption = UOM.MEMO

            btnSaveNew.Caption = Caption.FormSaveNew
            btnSaveClose.Caption = Caption.FormSaveClose
            btnClose.Caption = Caption.FormClose
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub Form_Closing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        sCode = txtKDPI.Text.Trim.ToUpper
    End Sub
    Private Sub fn_ChangeFormState()
        fn_LoadKDVENDOR()
        fn_LoadKDWAREHOUSE()
        fn_LoadKDITEM()
        fn_LoadKDUOM()

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

        deDATECREATED.Properties.ReadOnly = Status
        txtNOFAKTUR.Properties.ReadOnly = Status
        deDATE.Properties.ReadOnly = Status
        deDATEDUE.Properties.ReadOnly = Status
        grdKDVENDOR.Properties.ReadOnly = Status
        grdKDWAREHOUSE.Properties.ReadOnly = Status
        txtDISCOUNT.Properties.ReadOnly = True
        txtPPN.Properties.ReadOnly = True
        txtDISCOUNT_PERSEN.Properties.ReadOnly = Status
        txtPPN_PERSEN.Properties.ReadOnly = Status
        txtMATERAI.Properties.ReadOnly = Status
        txtMEMO.Properties.ReadOnly = Status

        grvDetail.OptionsBehavior.ReadOnly = Status
    End Sub
    Private Sub fn_EmptyMe()
        txtKDPI.Text = "<--- AUTO --->"
        txtNOFAKTUR.ResetText()
        deDATECREATED.DateTime = Now
        deDATE.DateTime = Now
        deDATEDUE.DateTime = Now
        grdKDVENDOR.ResetText()
        txtMEMO.ResetText()
        txtSUBTOTAL.ResetText()
        txtDISCOUNT.ResetText()
        txtPPN.ResetText()
        txtGRANDTOTAL.ResetText()
        txtDISCOUNT_PERSEN.ResetText()
        txtPPN_PERSEN.ResetText()
        txtMATERAI.ResetText()
    End Sub
    Private Sub fn_LoadData()
        Try
            ' ***** HEADER *****
            Dim ds = oPurchaseInvoice.GetData(sNoId)

            With ds
                txtKDPI.Text = .KDPI
                txtNOFAKTUR.Text = .NOFAKTUR
                deDATECREATED.DateTime = .DATECREATED
                deDATE.DateTime = .DATE
                deDATEDUE.DateTime = .DATEDUE
                grdKDVENDOR.Text = .KDVENDOR
                grdKDWAREHOUSE.Text = .KDWAREHOUSE
                txtMEMO.Text = .MEMO
                txtSUBTOTAL.Text = .SUBTOTAL
                txtDISCOUNT_PERSEN.Text = .DISCOUNT_PERSEN
                txtDISCOUNT.Text = .DISCOUNT
                txtPPN_PERSEN.Text = .PPN_PERSEN
                txtPPN.Text = .PPN
                txtMATERAI.Text = .MATERAI
                txtGRANDTOTAL.Text = .GRANDTOTAL

                bindingSource.DataSource = oPurchaseInvoice.GetDataDetail.Where(Function(x) x.KDPI = sNoId).OrderBy(Function(x) x.SEQ).ToList()
                grdDetail.DataSource = bindingSource
            End With
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Function fn_Validate() As Boolean
        Try
            fn_Validate = True

            If grdKDVENDOR.Text = String.Empty Then
                grdKDVENDOR.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                grdKDVENDOR.ErrorText = Statement.ErrorRequired

                grdKDVENDOR.Focus()
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

            If txtNOFAKTUR.Text = String.Empty Then
                txtNOFAKTUR.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                txtNOFAKTUR.ErrorText = Statement.ErrorRequired

                txtNOFAKTUR.Focus()
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
            Dim ds = oPurchaseInvoice.GetStructureHeader
            With ds
                Try
                    .DATECREATED = oPurchaseInvoice.GetData(sNoId).DATECREATED
                Catch oErr As Exception
                    .DATECREATED = deDATECREATED.DateTime
                End Try
                .DATEUPDATED = Now

                .KDPI = txtKDPI.Text.Trim.ToUpper
                .NOFAKTUR = txtNOFAKTUR.Text
                .DATE = deDATE.DateTime
                .DATEDUE = deDATEDUE.DateTime
                .KDVENDOR = IIf(String.IsNullOrEmpty(grdKDVENDOR.EditValue), String.Empty, grdKDVENDOR.EditValue)
                .KDWAREHOUSE = IIf(String.IsNullOrEmpty(grdKDWAREHOUSE.EditValue), String.Empty, grdKDWAREHOUSE.EditValue)
                .MEMO = txtMEMO.Text.Trim.ToUpper
                .SUBTOTAL = CDec(txtSUBTOTAL.Text)
                .DISCOUNT_PERSEN = CDec(txtDISCOUNT_PERSEN.Text)
                .DISCOUNT = CDec(txtDISCOUNT.Text)
                .PPN_PERSEN = CDec(txtPPN_PERSEN.Text)
                .PPN = CDec(txtPPN.Text)
                .MATERAI = CDec(txtMATERAI.Text)
                .GRANDTOTAL = CDec(txtGRANDTOTAL.Text)
                Try
                    .PAYAMOUNT = oPurchaseInvoice.GetData(sNoId).PAYAMOUNT
                Catch oErr As Exception
                    .PAYAMOUNT = 0
                End Try
                Try
                    .ISCHEKED = oPurchaseInvoice.GetData(sNoId).ISCHEKED
                Catch oErr As Exception
                    .ISCHEKED = False
                End Try

                .KDUSER = sUserID
            End With

            ' ***** DETIL *****
            Dim arrDetail = oPurchaseInvoice.GetStructureDetailList
            For i As Integer = 0 To grvDetail.RowCount - 2
                Dim dsDetail = oPurchaseInvoice.GetStructureDetail
                With dsDetail
                    Try
                        .DATECREATED = oPurchaseInvoice.GetData(sNoId).DATECREATED
                    Catch oErr As Exception
                        .DATECREATED = Now
                    End Try
                    .DATEUPDATED = Now

                    .SEQ = i
                    .KDPI = ds.KDPI
                    .KDITEM = grvDetail.GetRowCellValue(i, colKDITEM)
                    .KDUOM = grvDetail.GetRowCellValue(i, colKDUOM)
                    .QTY = CDec(grvDetail.GetRowCellValue(i, colQTY))
                    .PRICE = CDec(grvDetail.GetRowCellValue(i, colPRICE))
                    .SUBPRICE = CDec(grvDetail.GetRowCellValue(i, colSUBPRICE))
                    .SUBTOTAL = CDec(grvDetail.GetRowCellValue(i, colSUBTOTAL))
                    .DISCOUNT_PERSEN = CDec(grvDetail.GetRowCellValue(i, colDISCOUNT_PERSEN))
                    .DISCOUNT = CDec(grvDetail.GetRowCellValue(i, colDISCOUNT))
                    .PPN_PERSEN = CDec(0)
                    .PPN = CDec(0)
                    .GRANDTOTAL = CDec(grvDetail.GetRowCellValue(i, colGRANDTOTAL))
                    .BATCH = IIf(String.IsNullOrEmpty(grvDetail.GetRowCellValue(i, colBATCH)), "", grvDetail.GetRowCellValue(i, colBATCH))
                    .DATE_EXPIRED = CDate(grvDetail.GetRowCellValue(i, colDATE_EXPIRED))
                    .ISUPDATEHARGA = CBool(grvDetail.GetRowCellValue(i, colISUPDATEHARGA))
                    .REMARKS = IIf(String.IsNullOrEmpty(grvDetail.GetRowCellValue(i, colREMARKS)), "-", grvDetail.GetRowCellValue(i, colREMARKS))
                End With

                arrDetail.Add(dsDetail)

            Next

            If oFormMode = FORM_MODE.FORM_MODE_ADD Then
                Try
                    fn_Save = oPurchaseInvoice.InsertData(ds, arrDetail)
                Catch oErr As Exception
                    MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            ElseIf oFormMode = FORM_MODE.FORM_MODE_EDIT Then
                Try
                    fn_Save = oPurchaseInvoice.UpdateData(ds, arrDetail)
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
    Private Sub OnValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtDISCOUNT.EditValueChanged, txtPPN.EditValueChanged, txtMATERAI.EditValueChanged, grvDetail.FocusedRowChanged
        If isLoad Then
            Calculate()
        End If
    End Sub
    Private Sub Calculate()
        Dim sSubTotal = 0
        For i As Integer = 0 To grvDetail.RowCount - 2
            sSubTotal += CDec(grvDetail.GetRowCellValue(i, colGRANDTOTAL))
        Next

        txtSUBTOTAL.Text = sSubTotal

        txtGRANDTOTAL.Text = CDec(txtSUBTOTAL.Text) - CDec(txtDISCOUNT.Text) + CDec(txtPPN.Text) + CDec(txtMATERAI.Text)
    End Sub
    Private Sub grvDetail_CellValueChanged(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs) Handles grvDetail.CellValueChanged
        If e.Column.Name = colKDITEM.Name Then
            Dim oItem As New Reference.clsItem
            Try
                If grvDetail.GetFocusedRowCellValue(colKDITEM) IsNot Nothing Then
                    Dim ds = oItem.GetDataDetail_UOM(grvDetail.GetFocusedRowCellValue(colKDITEM))

                    If ds IsNot Nothing Then
                        grvDetail.SetFocusedRowCellValue(colKDUOM, ds.FirstOrDefault(Function(x) x.RATE = 1).KDUOM)
                        grvDetail.SetFocusedRowCellValue(colDATE_EXPIRED, Now)
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
                        Try
                            grvDetail.SetFocusedRowCellValue(colPRICE, ds.PRICEPURCHASESTANDARD)

                            grvDetail.SetFocusedRowCellValue(colQTY, 1)

                        Catch ex As Exception
                            grvDetail.SetFocusedRowCellValue(colPRICE, ds.PRICEPURCHASESTANDARD)
                        End Try
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
        ElseIf e.Column.Name = colQTY.Name Then
            'If CDec(grvDetail.GetFocusedRowCellValue(colQTY)) < 1 Then
            '    grvDetail.SetFocusedRowCellValue(colQTY, 1)
            'End If
            If CDec(grvDetail.GetFocusedRowCellValue(colPRICE)) > 0 Then
                Dim sSubTotal As Decimal = CDec(grvDetail.GetFocusedRowCellValue(colQTY)) * CDec(grvDetail.GetFocusedRowCellValue(colPRICE))
                grvDetail.SetFocusedRowCellValue(colSUBTOTAL, sSubTotal)
            End If
        ElseIf e.Column.Name = colPRICE.Name Then
            Dim oItem As New Reference.clsItem
            Dim ds = oItem.GetDataDetail_UOM(grvDetail.GetFocusedRowCellValue(colKDITEM), grvDetail.GetFocusedRowCellValue(colKDUOM))

            If grvDetail.GetFocusedRowCellValue(colPRICE) < ds.PRICEPURCHASESTANDARD Then
                If MessageBox.Show("Harga Turun dari Harga Awal : " & FormatNumber(ds.PRICEPURCHASESTANDARD) & vbCrLf & "Apakah akan dilanjutkan?", Me.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Information) = DialogResult.Yes Then
                    grvDetail.SetFocusedRowCellValue(colISUPDATEHARGA, True)
                Else
                    grvDetail.SetFocusedRowCellValue(colISUPDATEHARGA, False)
                End If
            Else
                grvDetail.SetFocusedRowCellValue(colISUPDATEHARGA, True)
            End If

            Dim sSubTotal As Decimal = CDec(grvDetail.GetFocusedRowCellValue(colQTY)) * CDec(grvDetail.GetFocusedRowCellValue(colPRICE))

            grvDetail.SetFocusedRowCellValue(colSUBTOTAL, sSubTotal)
        ElseIf e.Column.Name = colSUBTOTAL.Name Or e.Column.Name = colDISCOUNT_PERSEN.Name Or e.Column.Name = colPPN_PERSEN.Name Then
            
            Dim sDiscountPrice As Decimal = CDec(grvDetail.GetFocusedRowCellValue(colPRICE)) * (CDec(grvDetail.GetFocusedRowCellValue(colDISCOUNT_PERSEN) / 100))

            grvDetail.SetFocusedRowCellValue(colDISCOUNT, sDiscountPrice)

            Dim sTotalPrice As Decimal = CDec(grvDetail.GetFocusedRowCellValue(colPRICE)) - sDiscountPrice

            Dim sPPnPrice As Decimal = sTotalPrice * (CDec(grvDetail.GetFocusedRowCellValue(colPPN_PERSEN) / 100))

            grvDetail.SetFocusedRowCellValue(colPPN, sPPnPrice)

            Dim sSubPrice As Decimal = CDec(grvDetail.GetFocusedRowCellValue(colPRICE)) - sDiscountPrice + sPPnPrice

            grvDetail.SetFocusedRowCellValue(colSUBPRICE, sSubPrice)

            Dim sDiscount As Decimal = CDec(grvDetail.GetFocusedRowCellValue(colSUBTOTAL)) * (CDec(grvDetail.GetFocusedRowCellValue(colDISCOUNT_PERSEN) / 100))

            Dim sTotal As Decimal = CDec(grvDetail.GetFocusedRowCellValue(colSUBTOTAL)) - sDiscount

            Dim sPPn As Decimal = sTotal * (CDec(grvDetail.GetFocusedRowCellValue(colPPN_PERSEN) / 100))

            Dim sGrandTotal As Decimal = CDec(grvDetail.GetFocusedRowCellValue(colSUBTOTAL)) - sDiscount + sPPn

            grvDetail.SetFocusedRowCellValue(colGRANDTOTAL, sGrandTotal)

        End If
    End Sub
    Private Sub txtPPN_PERSEN_EditValueChanged(sender As Object, e As EventArgs) Handles txtPPN_PERSEN.EditValueChanged
        Dim sPPN As Decimal = 0

        sPPN = (CDec(txtSUBTOTAL.Text) - CDec(txtDISCOUNT.Text)) * (CDec(txtPPN_PERSEN.Text) / 100)
        txtPPN.Text = sPPN
    End Sub
    Private Sub txtDISCOUNT_PERSEN_EditValueChanged(sender As Object, e As EventArgs) Handles txtDISCOUNT_PERSEN.EditValueChanged
        Dim sDISKON As Decimal = 0

        sDISKON = CDec(txtSUBTOTAL.Text) * (CDec(txtDISCOUNT_PERSEN.Text) / 100)
        txtDISCOUNT.Text = sDISKON
    End Sub
    Private Sub DeleteToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DeleteToolStripMenuItem.Click
        If oFormMode = FORM_MODE.FORM_MODE_VIEW Then Exit Sub
        grvDetail.DeleteSelectedRows()
    End Sub
#End Region
#Region "Command Button"
    Private Sub frmPurchaseInvoice_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
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
        End Select
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
    Private Sub fn_LoadKDVENDOR()
        Dim oVENDOR As New Reference.clsVendor
        Try
            grdKDVENDOR.Properties.DataSource = oVENDOR.GetData.Where(Function(x) x.ISACTIVE = True).ToList()
            grdKDVENDOR.Properties.ValueMember = "KDVENDOR"
            grdKDVENDOR.Properties.DisplayMember = "NAME_DISPLAY"
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub grdKDVENDOR_KeyDown(sender As System.Object, e As System.Windows.Forms.KeyEventArgs) Handles grdKDVENDOR.KeyDown
        If e.KeyCode = Keys.Delete Then
            grdKDVENDOR.ResetText()
        End If
    End Sub
    Private Sub grdKDVENDOR_Validated() Handles grdKDVENDOR.Validated
        Try
            Dim oVendor As New Reference.clsVendor
            Dim ds = oVendor.GetData(grdKDVENDOR.EditValue)

            deDATEDUE.DateTime = deDATE.DateTime.AddDays(ds.DUE)
        Catch ex As Exception

        End Try
    End Sub
    Private Sub fn_LoadKDWAREHOUSE()
        Dim oWAREHOUSE As New Reference.clsWarehouse
        Try
            grdKDWAREHOUSE.Properties.DataSource = oWAREHOUSE.GetData.Where(Function(x) x.ISACTIVE = True).ToList()
            grdKDWAREHOUSE.Properties.ValueMember = "KDWAREHOUSE"
            grdKDWAREHOUSE.Properties.DisplayMember = "NAME_DISPLAY"
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub grdKDWAREHOUSE_KeyDown(sender As System.Object, e As System.Windows.Forms.KeyEventArgs) Handles grdKDWAREHOUSE.KeyDown
        If e.KeyCode = Keys.Delete Then
            grdKDWAREHOUSE.ResetText()
        End If
    End Sub
    Private Sub fn_LoadKDITEM()
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
            SQL &= ",STOK = ISNULL((SELECT AA.AMOUNT FROM M_ITEM_WAREHOUSE AA INNER JOIN M_ITEM_UOM BB ON AA.KDITEM = BB.KDITEM WHERE A.KDITEM = AA.KDITEM AND A.KDITEM = BB.KDITEM AND BB.RATE = 1 AND AA.KDWAREHOUSE = '" & grdKDWAREHOUSE.EditValue & "'), 0) "
            SQL &= "FROM "
            SQL &= "M_ITEM A "
            SQL &= "INNER JOIN M_ITEM_L1 B "
            SQL &= "ON A.KDITEM_L1 = B.KDITEM_L1 "
            SQL &= "WHERE A.ISACTIVE = 1 "
            SQL &= "AND B.MEMO = 'OBAT' "

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
    Private Sub fn_LoadKDUOM()
        Dim oUOM As New Reference.clsUOM
        Try
            grdKDUOM.DataSource = oUOM.GetData.Where(Function(x) x.ISACTIVE = True).ToList()
            grdKDUOM.ValueMember = "KDUOM"
            grdKDUOM.DisplayMember = "MEMO"
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub grdKDWAREHOUSE_EditValueChanged(sender As Object, e As EventArgs) Handles grdKDWAREHOUSE.EditValueChanged
        fn_LoadKDITEM()
    End Sub
    Private Sub Price_ButtonClick() Handles betxtPrice.ButtonClick
        If oFormMode = FORM_MODE.FORM_MODE_VIEW Then Exit Sub
        If grvDetail.GetFocusedRowCellValue(colKDITEM) Is Nothing Then Exit Sub

        frmBrowsePurchaseInvoice.fn_LoadMe(CInt(grvDetail.GetFocusedRowCellValue(colKDITEM)))
        frmBrowsePurchaseInvoice.ShowDialog(Me)

        If sPricePembelian <> 0 Then
            grvDetail.SetFocusedRowCellValue(colPRICE, sPricePembelian)
        End If

        sPricePembelian = 0
    End Sub
#End Region
End Class