Imports DataAccess
Imports UI.WIN.MAIN.My.Resources
Imports System.Linq
Imports System.Data.SqlClient

Public Class frmPurchaseReturn
#Region "Declaration"
    Private oFormMode As FORM_MODE = FORM_MODE.FORM_MODE_VIEW
    Private sNoId As String
    Private isLoad As Boolean = False
    Private oPurchaseReturn As New Purchasing.clsPurchaseReturn
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
            Me.Text = PurchaseReturn.TITLE

            lKDPR.Text = PurchaseReturn.KDPR
            lDATE.Text = PurchaseReturn.TANGGAL
            lKDVENDOR.Text = PurchaseReturn.KDVENDOR & " *"
            lKDWAREHOUSE.Text = PurchaseReturn.KDWAREHOUSE & " *"

            lSUBTOTAL.Text = PurchaseReturn.SUBTOTAL
            lDISCOUNT.Text = PurchaseReturn.DISCOUNT
            lTAX.Text = PurchaseReturn.TAX
            lGRANDTOTAL.Text = PurchaseReturn.GRANDTOTAL

            tab1.Text = PurchaseReturn.TAB_DETAIL
            tab2.Text = PurchaseReturn.TAB_MEMO

            grvDetail.Columns("KDITEM").Caption = PurchaseReturn.DETAIL_KDITEM
            grvDetail.Columns("KDUOM").Caption = PurchaseReturn.DETAIL_KDUOM
            grvDetail.Columns("QTY").Caption = PurchaseReturn.DETAIL_QTY
            grvDetail.Columns("PRICE").Caption = PurchaseInvoice.DETAIL_PRICE
            grvDetail.Columns("SUBTOTAL").Caption = PurchaseInvoice.DETAIL_SUBTOTAL
            grvDetail.Columns("DISCOUNT").Caption = PurchaseInvoice.DETAIL_DISCOUNT
            grvDetail.Columns("GRANDTOTAL").Caption = PurchaseInvoice.DETAIL_GRANDTOTAL
            grvDetail.Columns("REMARKS").Caption = PurchaseReturn.DETAIL_REMARKS

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
        sCode = txtKDPR.Text.Trim.ToUpper
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

        deDATE.Properties.ReadOnly = Status
        grdKDVENDOR.Properties.ReadOnly = Status
        grdKDWAREHOUSE.Properties.ReadOnly = Status
        txtDISCOUNT.Properties.ReadOnly = Status
        txtTAX.Properties.ReadOnly = Status

        txtMEMO.Properties.ReadOnly = Status

        grvDetail.OptionsBehavior.ReadOnly = Status
    End Sub
    Private Sub fn_EmptyMe()
        txtKDPR.Text = "<--- AUTO --->"
        deDATE.DateTime = Now
        grdKDVENDOR.ResetText()
        'grdKDWAREHOUSE.ResetText()
        txtMEMO.ResetText()
        txtSUBTOTAL.ResetText()
        txtDISCOUNT.ResetText()
        txtTAX.ResetText()
        txtGRANDTOTAL.ResetText()
    End Sub
    Private Sub fn_LoadData()
        Try
            ' ***** HEADER *****
            Dim ds = oPurchaseReturn.GetData(sNoId)

            With ds
                txtKDPR.Text = .KDPR
                deDATE.DateTime = .DATE
                grdKDVENDOR.Text = .KDVENDOR
                grdKDWAREHOUSE.Text = .KDWAREHOUSE
                txtMEMO.Text = .MEMO
                txtSUBTOTAL.Text = .SUBTOTAL
                txtDISCOUNT.Text = .DISCOUNT
                txtTAX.Text = .TAX
                txtGRANDTOTAL.Text = .GRANDTOTAL

                bindingSource.DataSource = oPurchaseReturn.GetDataDetail.Where(Function(x) x.KDPR = sNoId).OrderBy(Function(x) x.SEQ).ToList()
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

            grvDetail.UpdateCurrentRow()

            If grvDetail.RowCount < 2 Then
                MsgBox(Statement.ErrorDetail, MsgBoxStyle.Exclamation, Me.Text)
                fn_Validate = False
                Exit Function
            End If

            For i As Integer = 0 To grvDetail.RowCount - 2
                Dim oItem As New Reference.clsItem

                If grvDetail.GetRowCellValue(i, colKDITEM) IsNot Nothing And grvDetail.GetRowCellValue(i, colKDUOM) IsNot Nothing Then
                    Dim ds = oItem.GetData(grvDetail.GetRowCellValue(i, colKDITEM))

                    If ds.M_ITEM_L1.MEMO = "HANDPHONE" Then
                        If grvDetail.GetRowCellValue(i, colREMARKS) = "" Or grvDetail.GetRowCellValue(i, colREMARKS) = "-" Then
                            MsgBox("Catatan harus diisi dengan nomer IMEI!", MsgBoxStyle.Exclamation, Me.Text)

                            grvDetail.FocusedRowHandle = i
                            fn_Validate = False
                            Exit Function
                        Else
                            Dim sCOUNT = grvDetail.GetRowCellValue(i, colREMARKS).ToString().Split(Environment.NewLine).ToList()

                            If CDec(grvDetail.GetRowCellValue(i, colQTY)) <> sCOUNT.Count Then
                                MsgBox("Nomer IMEI harus sesuai dengan jumlah yang dimasukkan!" & vbCrLf & "Anda baru memasukkan : " & sCOUNT.Count & " dari " & FormatNumber(grvDetail.GetFocusedRowCellValue(colQTY), 0) & " nomer IMEI!", MsgBoxStyle.Exclamation, Me.Text)

                                grvDetail.FocusedRowHandle = i
                                fn_Validate = False
                                Exit Function
                            End If
                        End If
                    End If
                End If
            Next
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Function
    Private Function fn_Save() As Boolean
        Try
            ' ***** HEADER *****
            Dim ds = oPurchaseReturn.GetStructureHeader
            With ds
                Try
                    .DATECREATED = oPurchaseReturn.GetData(sNoId).DATECREATED
                Catch oErr As Exception
                    .DATECREATED = Now
                End Try
                .DATEUPDATED = Now

                .KDPR = sNoId
                .DATE = deDATE.DateTime
                .KDVENDOR = IIf(String.IsNullOrEmpty(grdKDVENDOR.EditValue), String.Empty, grdKDVENDOR.EditValue)
                .KDWAREHOUSE = IIf(String.IsNullOrEmpty(grdKDWAREHOUSE.EditValue), String.Empty, grdKDWAREHOUSE.EditValue)
                .MEMO = txtMEMO.Text.Trim.ToUpper
                .SUBTOTAL = CDec(txtSUBTOTAL.Text)
                .DISCOUNT = CDec(txtDISCOUNT.Text)
                .TAX = CDec(txtTAX.Text)
                .GRANDTOTAL = CDec(txtGRANDTOTAL.Text)
                Try
                    .PAYAMOUNT = oPurchaseReturn.GetData(sNoId).PAYAMOUNT
                Catch oErr As Exception
                    .PAYAMOUNT = 0
                End Try
                .KDUSER = sUserID
            End With

            ' ***** DETIL *****
            Dim arrDetail = oPurchaseReturn.GetStructureDetailList
            For i As Integer = 0 To grvDetail.RowCount - 2
                Dim dsDetail = oPurchaseReturn.GetStructureDetail
                With dsDetail
                    Try
                        .DATECREATED = oPurchaseReturn.GetData(sNoId).DATECREATED
                    Catch oErr As Exception
                        .DATECREATED = Now
                    End Try
                    .DATEUPDATED = Now

                    .SEQ = i
                    .KDPR = ds.KDPR
                    .KDITEM = grvDetail.GetRowCellValue(i, colKDITEM)
                    .KDUOM = grvDetail.GetRowCellValue(i, colKDUOM)
                    .QTY = CDec(grvDetail.GetRowCellValue(i, colQTY))
                    .PRICE = CDec(grvDetail.GetRowCellValue(i, colPRICE))
                    .SUBTOTAL = CDec(grvDetail.GetRowCellValue(i, colSUBTOTAL))
                    .DISCOUNT = CDec(grvDetail.GetRowCellValue(i, colDISCOUNT))
                    .GRANDTOTAL = CDec(grvDetail.GetRowCellValue(i, colGRANDTOTAL))
                    .REMARKS = IIf(String.IsNullOrEmpty(grvDetail.GetRowCellValue(i, colREMARKS)), "-", grvDetail.GetRowCellValue(i, colREMARKS))
                End With
                arrDetail.Add(dsDetail)
            Next

            If oFormMode = FORM_MODE.FORM_MODE_ADD Then
                Try
                    fn_Save = oPurchaseReturn.InsertData(ds, arrDetail)
                Catch oErr As Exception
                    MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            ElseIf oFormMode = FORM_MODE.FORM_MODE_EDIT Then
                Try
                    fn_Save = oPurchaseReturn.UpdateData(ds, arrDetail)
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
    Private Sub OnValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtDISCOUNT.EditValueChanged, txtTAX.EditValueChanged, grvDetail.FocusedRowChanged
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

        txtGRANDTOTAL.Text = CDec(txtSUBTOTAL.Text) - CDec(txtDISCOUNT.Text) + CDec(txtTAX.Text)
    End Sub
    Private Sub grvDetail_CellValueChanged(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs) Handles grvDetail.CellValueChanged
        If e.Column.Name = colKDITEM.Name Then
            Dim oItem As New Reference.clsItem
            Try
                If grvDetail.GetFocusedRowCellValue(colKDITEM) IsNot Nothing Then
                    Dim ds = oItem.GetDataDetail_UOM(grvDetail.GetFocusedRowCellValue(colKDITEM))

                    If ds IsNot Nothing Then
                        grvDetail.SetFocusedRowCellValue(colKDUOM, ds.FirstOrDefault(Function(x) x.RATE = 1).KDUOM)
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
        ElseIf e.Column.Name = colQTY.Name Or e.Column.Name = colPRICE.Name Then
            If CDec(grvDetail.GetFocusedRowCellValue(colQTY)) < 1 Then
                grvDetail.SetFocusedRowCellValue(colQTY, 1)
            End If

            Dim sSubTotal As Decimal = CDec(grvDetail.GetFocusedRowCellValue(colQTY)) * CDec(grvDetail.GetFocusedRowCellValue(colPRICE))

            grvDetail.SetFocusedRowCellValue(colSUBTOTAL, sSubTotal)
        ElseIf e.Column.Name = colSUBTOTAL.Name Or e.Column.Name = colDISCOUNT.Name Then
            Dim sGrandTotal As Decimal = CDec(grvDetail.GetFocusedRowCellValue(colSUBTOTAL)) - CDec(grvDetail.GetFocusedRowCellValue(colDISCOUNT))

            grvDetail.SetFocusedRowCellValue(colGRANDTOTAL, sGrandTotal)
        End If
    End Sub
    Private Sub DeleteToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DeleteToolStripMenuItem.Click
        If oFormMode = FORM_MODE.FORM_MODE_VIEW Then Exit Sub
        grvDetail.DeleteSelectedRows()
    End Sub
#End Region
#Region "Command Button"
    Private Sub frmPurchaseReturn_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
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
#End Region
End Class