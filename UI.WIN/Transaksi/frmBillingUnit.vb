Imports DataAccess
Imports UI.WIN.MAIN.My.Resources
Imports System.Linq
Imports Newtonsoft.Json.Linq
Imports System.Data.SqlClient

Public Class frmBillingUnit
#Region "Declaration"
    Private oFormMode As FORM_MODE = FORM_MODE.FORM_MODE_VIEW
    Private sNoId As String
    Private isLoad As Boolean = False
    Private oBillingUnit As New Transaksi.clsBillingUnit

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
            Me.Text = BillingUnit.TITLE

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
        fn_LoadSatuan()
        fn_LoadPenjamin()
        fn_LoadDokter()
        fn_LoadCustomerUnit()
        fn_LoadItemTarif()
        fn_LoadItemH()

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
        grdKDCUSTOMER_UNIT.Properties.ReadOnly = Status
        grdKDDOCTOR_H.Properties.ReadOnly = Status
        grdKDPENJAMIN.Properties.ReadOnly = Status
        grvDetail.OptionsBehavior.ReadOnly = Status
    End Sub
    Private Sub fn_EmptyMe()
        txtKDBILLING.Text = "<--- AUTO --->"
        deDATE.DateTime = Now
        grdKDCUSTOMER_UNIT.ResetText()
        grdKDDOCTOR_H.ResetText()
        grdKDPENJAMIN.ResetText()
    End Sub
    Private Sub fn_LoadData()
        Try
            ' ***** HEADER *****
            Dim ds = oBillingUnit.GetData(sNoId)

            With ds
                txtKDBILLING.Text = .KDBILLING_UNIT
                deDATE.DateTime = .DATE
                grdKDCUSTOMER_UNIT.Text = .KDCUSTOMER_UNIT
                grdKDDOCTOR_H.Text = .KDDOCTOR
                grdKDPENJAMIN.Text = .KDPENJAMIN
                txtGRANDTOTAL.Text = .GRANDTOTAL
                BindingSource.DataSource = oBillingUnit.GetDataDetail.Where(Function(x) x.KDBILLING_UNIT = sNoId).OrderBy(Function(x) x.SEQ).ToList()
                grdDetail.DataSource = BindingSource
            End With
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Function fn_Validate() As Boolean
        Try
            fn_Validate = True

            If grdKDCUSTOMER_UNIT.Text = String.Empty Then
                grdKDCUSTOMER_UNIT.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                grdKDCUSTOMER_UNIT.ErrorText = Statement.ErrorRequired

                grdKDCUSTOMER_UNIT.Focus()
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
            If grdKDDOCTOR_H.Text = String.Empty Then
                grdKDDOCTOR_H.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                grdKDDOCTOR_H.ErrorText = Statement.ErrorRequired

                grdKDDOCTOR_H.Focus()
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
            Dim ds = oBillingUnit.GetStructureHeader
            With ds
                Try
                    .DATECREATED = oBillingUnit.GetData(sNoId).DATECREATED
                Catch oErr As Exception
                    .DATECREATED = Now
                End Try
                .DATEUPDATED = Now
                .DATE = deDATE.DateTime
                .KDPENJAMIN = grdKDPENJAMIN.EditValue
                .KDBILLING_UNIT = sNoId
                .KDCUSTOMER_UNIT = grdKDCUSTOMER_UNIT.EditValue
                .KDDOCTOR = grdKDDOCTOR_H.EditValue
                .SUBTOTAL = CDec(txtGRANDTOTAL.Text)
                .DISCOUNT = CDec(0)
                .TAX = CDec(0)
                .GRANDTOTAL = CDec(txtGRANDTOTAL.Text)
                Try
                    .PAYAMOUNT = oBillingUnit.GetData(sNoId).PAYAMOUNT
                Catch ex As Exception
                    .PAYAMOUNT = 0
                End Try
                .KDUSER = sUserID
                .MEMO = txtMEMO.Text
            End With

            ' ***** DETIL *****
            Dim arrDetail = oBillingUnit.GetStructureDetailList
            For i As Integer = 0 To grvDetail.RowCount - 2
                Dim dsDetail = oBillingUnit.GetStructureDetail
                With dsDetail
                    .KDBILLING_UNIT = ds.KDBILLING_UNIT
                    .KDDOCTOR = grvDetail.GetRowCellValue(i, colKDDOCTOR)
                    .KDITEM = grvDetail.GetRowCellValue(i, colKDITEM)
                    .KDITEM_L1 = grvDetail.GetRowCellValue(i, colKDITEM_L1)
                    .KDITEM_L2 = grvDetail.GetRowCellValue(i, colKDITEM_L2)
                    .KDITEM_L3 = grvDetail.GetRowCellValue(i, colKDITEM_L3)
                    .KDITEM_L4 = grvDetail.GetRowCellValue(i, colKDITEM_L4)
                    .KDITEM_L5 = grvDetail.GetRowCellValue(i, colKDITEM_L5)
                    .KDITEM_L6 = grvDetail.GetRowCellValue(i, colKDITEM_L6)
                    .KDUOM = grvDetail.GetRowCellValue(i, colKDUOM)
                    .SEQ = i
                    .QTY = grvDetail.GetRowCellValue(i, colQTY)
                    .PRICE = grvDetail.GetRowCellValue(i, colPRICE)
                    .SUBTOTAL = grvDetail.GetRowCellValue(i, colGRANDTOTAL)
                    .DISCOUNT = CDec(0)
                    .GRANDTOTAL = grvDetail.GetRowCellValue(i, colGRANDTOTAL)
                    .REMARKS = grvDetail.GetRowCellValue(i, colREMARKS)
                End With
                arrDetail.Add(dsDetail)
            Next

            If oFormMode = FORM_MODE.FORM_MODE_ADD Then
                Try
                    fn_Save = oBillingUnit.InsertData(ds, arrDetail)
                Catch oErr As Exception
                    MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            ElseIf oFormMode = FORM_MODE.FORM_MODE_EDIT Then
                Try
                    fn_Save = oBillingUnit.UpdateData(ds, arrDetail)
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
        For i As Integer = 0 To grvDetail.RowCount - 2
            sSubTotal += CDec(grvDetail.GetRowCellValue(i, colGRANDTOTAL))
        Next

        txtGRANDTOTAL.Text = sSubTotal
    End Sub
    Private Sub grvDetail_CellValueChanged(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs) Handles grvDetail.CellValueChanged
        If e.Column.Name = colKDITEM.Name Then
            Dim oItem As New Reference.clsItem
            Try
                If grvDetail.GetFocusedRowCellValue(colKDITEM) IsNot Nothing Then
                    Dim ds = oItem.GetDataDetail_UOM(grvDetail.GetFocusedRowCellValue(colKDITEM))
                    If ds IsNot Nothing Then
                        grvDetail.SetFocusedRowCellValue(colKDUOM, ds.FirstOrDefault(Function(x) x.RATE = 1).KDUOM)
                        grvDetail.SetFocusedRowCellValue(colQTY, 1)
                        grvDetail.SetFocusedRowCellValue(colREMARKS, "-")
                        grvDetail.SetFocusedRowCellValue(colKDITEM_L1, oItem.GetData(grvDetail.GetFocusedRowCellValue(colKDITEM)).KDITEM_L1)
                        grvDetail.SetFocusedRowCellValue(colKDITEM_L2, oItem.GetData(grvDetail.GetFocusedRowCellValue(colKDITEM)).KDITEM_L2)
                        grvDetail.SetFocusedRowCellValue(colKDITEM_L3, oItem.GetData(grvDetail.GetFocusedRowCellValue(colKDITEM)).KDITEM_L3)
                        grvDetail.SetFocusedRowCellValue(colKDITEM_L4, oItem.GetData(grvDetail.GetFocusedRowCellValue(colKDITEM)).KDITEM_L4)
                        grvDetail.SetFocusedRowCellValue(colKDITEM_L5, oItem.GetData(grvDetail.GetFocusedRowCellValue(colKDITEM)).KDITEM_L5)
                        grvDetail.SetFocusedRowCellValue(colKDITEM_L6, oItem.GetData(grvDetail.GetFocusedRowCellValue(colKDITEM)).KDITEM_L6)
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
                        grvDetail.SetFocusedRowCellValue(colPRICE, ds.PRICESALESSTANDARD)
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
            Dim sSubTotal As Decimal = CDec(grvDetail.GetFocusedRowCellValue(colQTY)) * CDec(grvDetail.GetFocusedRowCellValue(colPRICE))
            grvDetail.SetFocusedRowCellValue(colGRANDTOTAL, sSubTotal)
        End If
    End Sub
#End Region
#Region "Command Button"
    Private Sub frmBillingUnit_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
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
    Private Sub grvH_DoubleClick(sender As Object, e As EventArgs) Handles grvH.DoubleClick
        If grvH.GetFocusedRowCellValue("KDITEM_L1") Is Nothing Then
            Exit Sub
        End If
        fn_LoadItemD(grvH.GetFocusedRowCellValue("KDITEM_L1"))
    End Sub
    Private Sub grvD_DoubleClick(sender As Object, e As EventArgs) Handles grvD.DoubleClick
        If grvD.GetFocusedRowCellValue("KDITEM") Is Nothing Then
            Exit Sub
        End If

        Dim oItem As New Reference.clsItem
        Dim ds = oItem.GetData(grvD.GetFocusedRowCellValue("KDITEM"))

        grvDetail.Focus()
        grvDetail.AddNewRow()
        grvDetail.SetFocusedRowCellValue(colKDITEM, ds.KDITEM)
        grvDetail.SetFocusedRowCellValue(colQTY, 1)
        grvDetail.SetFocusedRowCellValue(colKDDOCTOR, grdKDDOCTOR_H.EditValue)
        grvDetail.UpdateCurrentRow()

    End Sub
    Private Sub fn_LoadItemH()
        Dim oItem As New Reference.clsItem_L1
        Try
            Dim ds = From x In oItem.GetData.Where(Function(x) x.ISACTIVE = True And x.MEMO <> "OBAT")
                     Select x.KDITEM_L1, Nama = x.MEMO

            grdH.DataSource = ds.ToList()
            grvH.Columns("KDITEM_L1").VisibleIndex = -1

        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadItemD(ByVal sKDITEM_L1 As String)
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
            SQL &= ",NamaTarif = A.NMITEM2 "
            SQL &= "FROM M_ITEM AS A "
            SQL &= "WHERE A.ISACTIVE = 1 "
            SQL &= "AND A.KDITEM_L1 = '" & sKDITEM_L1 & "' "
            SQL &= "ORDER BY A.NMITEM2 "

            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "M_TARIF_")

            grdD.DataSource = ds.Tables("M_TARIF_")
            grvD.BestFitColumns()

            If oConn.State = ConnectionState.Open Then
                oConn.Close()
            End If

            grvD.Columns("KDITEM").VisibleIndex = -1
        Catch oErr As Exception
            MsgBox("Preview Data : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadItemTarif()
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

            SQL = "SELECT * "
            SQL &= "FROM M_ITEM AS A "
            SQL &= "INNER JOIN M_ITEM_L1 B "
            SQL &= "ON A.KDITEM_L1 = B.KDITEM_L1 "
            SQL &= "WHERE A.ISACTIVE = 1 "
            SQL &= "AND B.MEMO <> 'OBAT' "
            SQL &= "ORDER BY A.NMITEM2 "

            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "M_TARIF")

            grdKDITEM.DataSource = ds.Tables("M_TARIF")
            grdKDITEM.ValueMember = "KDITEM"
            grdKDITEM.DisplayMember = "NMITEM2"

            If oConn.State = ConnectionState.Open Then
                oConn.Close()
            End If

        Catch oErr As Exception
            MsgBox("Preview Data : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadDokter()
        Dim oDokter As New Reference.clsDoctor
        Try
            grdKDDOCTOR_H.Properties.DataSource = oDokter.GetData.Where(Function(x) x.ISACTIVE = True).ToList()
            grdKDDOCTOR_H.Properties.ValueMember = "KDDOCTOR"
            grdKDDOCTOR_H.Properties.DisplayMember = "NAME_DISPLAY"

            grdKDDOCTOR.DataSource = oDokter.GetData.Where(Function(x) x.ISACTIVE = True).ToList()
            grdKDDOCTOR.ValueMember = "KDDOCTOR"
            grdKDDOCTOR.DisplayMember = "NAME_DISPLAY"
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
    Private Sub fn_LoadCustomerUnit()
        Dim oCustomer_Unit As New Reference.clsCustomerUnit
        Try
            grdKDCUSTOMER_UNIT.Properties.DataSource = oCustomer_Unit.GetData.Where(Function(x) x.ISACTIVE = True).ToList()
            grdKDCUSTOMER_UNIT.Properties.ValueMember = "KDCUSTOMER_UNIT"
            grdKDCUSTOMER_UNIT.Properties.DisplayMember = "NAME_DISPLAY"
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub SimpleButton1_Click(sender As Object, e As EventArgs) Handles SimpleButton1.Click
        Dim frmCustomerUnit As New frmCustomerUnit
        Try
            frmCustomerUnit.LoadMe(FORM_MODE.FORM_MODE_ADD)
            frmCustomerUnit.ShowDialog(Me)

            fn_LoadCustomerUnit()
            grdKDCUSTOMER_UNIT.Text = sCode
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
#End Region
End Class