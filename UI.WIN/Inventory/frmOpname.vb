Imports DataAccess
Imports UI.WIN.MAIN.My.Resources
Imports System.Linq
Imports System.Data.SqlClient

Public Class frmOpname
#Region "Declaration"
    Private oFormMode As FORM_MODE = FORM_MODE.FORM_MODE_VIEW
    Private sNoId As String
    Private isLoad As Boolean = False
    Private oOpname As New Inventory.clsOpname
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
            Me.Text = Opname.TITLE

            lKDOPNAME.Text = Opname.KDOPNAME
            lDATE.Text = Opname.TANGGAL
            lKDWAREHOUSE.Text = Opname.KDWAREHOUSE & " *"

            tab1.Text = Opname.TAB_DETAIL
            tab2.Text = Opname.TAB_MEMO

            grvDetail.Columns("KDITEM").Caption = Opname.DETAIL_KDITEM
            grvDetail.Columns("KDUOM").Caption = Opname.DETAIL_KDUOM
            grvDetail.Columns("QTY").Caption = Opname.DETAIL_QTY
            grvDetail.Columns("QTY_KOMPUTER").Caption = Opname.DETAIL_QTY_KOMPUTER
            grvDetail.Columns("QTY_FISIK").Caption = Opname.DETAIL_QTY_FISIK
            grvDetail.Columns("QTY").Caption = Opname.DETAIL_QTY
            grvDetail.Columns("REMARKS").Caption = Opname.DETAIL_REMARKS

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
        sCode = txtKDOPNAME.Text.Trim.ToUpper
    End Sub
    Private Sub fn_ChangeFormState()
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
        grdKDWAREHOUSE.Properties.ReadOnly = Status

        txtMEMO.Properties.ReadOnly = Status

        grvDetail.OptionsBehavior.ReadOnly = Status
    End Sub
    Private Sub fn_EmptyMe()
        txtKDOPNAME.Text = "<--- AUTO --->"
        deDATE.DateTime = Now
        grdKDWAREHOUSE.ResetText()
        txtMEMO.ResetText()
    End Sub
    Private Sub fn_LoadData()
        Try
            ' ***** HEADER *****
            Dim ds = oOpname.GetData(sNoId)

            With ds
                txtKDOPNAME.Text = .KDOPNAME
                deDATE.DateTime = .DATE
                grdKDWAREHOUSE.Text = .KDWAREHOUSE
                txtMEMO.Text = .MEMO

                bindingSource.DataSource = oOpname.GetDataDetail.Where(Function(x) x.KDOPNAME = sNoId).OrderBy(Function(x) x.SEQ).ToList()
                grdDetail.DataSource = bindingSource
            End With
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Function fn_Validate() As Boolean
        Try
            fn_Validate = True

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
            Dim ds = oOpname.GetStructureHeader
            With ds
                Try
                    .DATECREATED = oOpname.GetData(sNoId).DATECREATED
                Catch oErr As Exception
                    .DATECREATED = Now
                End Try
                .DATEUPDATED = Now

                .KDOPNAME = sNoId
                .DATE = deDATE.DateTime
                .KDWAREHOUSE = IIf(String.IsNullOrEmpty(grdKDWAREHOUSE.EditValue), String.Empty, grdKDWAREHOUSE.EditValue)
                .MEMO = txtMEMO.Text.Trim.ToUpper
                .KDUSER = sUserID
            End With

            ' ***** DETIL *****
            Dim arrDetail = oOpname.GetStructureDetailList
            For i As Integer = 0 To grvDetail.RowCount - 2
                Dim dsDetail = oOpname.GetStructureDetail
                With dsDetail
                    Try
                        .DATECREATED = oOpname.GetData(sNoId).DATECREATED
                    Catch oErr As Exception
                        .DATECREATED = Now
                    End Try
                    .DATEUPDATED = Now

                    .SEQ = i
                    .KDOPNAME = ds.KDOPNAME
                    .KDITEM = grvDetail.GetRowCellValue(i, colKDITEM)
                    .KDUOM = grvDetail.GetRowCellValue(i, colKDUOM)
                    .QTY_KOMPUTER = CDec(grvDetail.GetRowCellValue(i, colQTY_KOMPUTER))
                    .QTY_FISIK = CDec(grvDetail.GetRowCellValue(i, colQTY_FISIK))
                    .QTY = CDec(grvDetail.GetRowCellValue(i, colQTY))
                    .REMARKS = IIf(String.IsNullOrEmpty(grvDetail.GetRowCellValue(i, colREMARKS)), "-", grvDetail.GetRowCellValue(i, colREMARKS))
                End With
                arrDetail.Add(dsDetail)
            Next

            If oFormMode = FORM_MODE.FORM_MODE_ADD Then
                Try
                    fn_Save = oOpname.InsertData(ds, arrDetail)
                Catch oErr As Exception
                    MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            ElseIf oFormMode = FORM_MODE.FORM_MODE_EDIT Then
                Try
                    fn_Save = oOpname.UpdateData(ds, arrDetail)
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
    Private Sub grvDetail_CellValueChanged(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs) Handles grvDetail.CellValueChanged
        If e.Column.Name = colKDITEM.Name Then
            Dim oItem As New Reference.clsItem
            Try
                If grvDetail.GetFocusedRowCellValue(colKDITEM) IsNot Nothing Then
                    Dim ds = oItem.GetDataDetail_UOM(grvDetail.GetFocusedRowCellValue(colKDITEM))

                    If ds IsNot Nothing Then
                        grvDetail.SetFocusedRowCellValue(colKDUOM, ds.FirstOrDefault(Function(x) x.RATE = 1).KDUOM)

                        Dim ds2 = oItem.GetDataDetail_WAREHOUSE(grvDetail.GetFocusedRowCellValue(colKDITEM), grdKDWAREHOUSE.EditValue, grvDetail.GetFocusedRowCellValue(colKDUOM))

                        If ds2 IsNot Nothing Then
                            grvDetail.SetFocusedRowCellValue(colQTY_KOMPUTER, ds2.AMOUNT)
                        Else
                            grvDetail.SetFocusedRowCellValue(colQTY_KOMPUTER, 0)
                        End If

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
            If CDec(grvDetail.GetFocusedRowCellValue(colQTY)) = 0 Then
                grvDetail.SetFocusedRowCellValue(colQTY, 1)
            End If
        ElseIf e.Column.Name = colQTY_FISIK.Name Or e.Column.Name = colQTY_KOMPUTER.Name Then
            Try
                Dim sSelisih As Decimal = (CDec(grvDetail.GetFocusedRowCellValue(colQTY_FISIK)) - CDec(grvDetail.GetFocusedRowCellValue(colQTY_KOMPUTER)))

                grvDetail.SetFocusedRowCellValue(colQTY, sSelisih)
            Catch ex As Exception
                grvDetail.SetFocusedRowCellValue(colQTY, 0)
            End Try
        End If
    End Sub
    Private Sub DeleteToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DeleteToolStripMenuItem.Click
        If oFormMode = FORM_MODE.FORM_MODE_VIEW Then Exit Sub
        grvDetail.DeleteSelectedRows()
    End Sub
#End Region
#Region "Command Button"
    Private Sub frmOpname_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
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