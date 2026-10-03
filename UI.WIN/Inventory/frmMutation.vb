Imports DataAccess
Imports UI.WIN.MAIN.My.Resources
Imports System.Linq
Imports System.Data.SqlClient

Public Class frmMutation
#Region "Declaration"
    Private oFormMode As FORM_MODE = FORM_MODE.FORM_MODE_VIEW
    Private sNoId As String
    Private isLoad As Boolean = False
    Private oMutation As New Inventory.clsMutation
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
            Me.Text = Mutation.TITLE

            lKDMUTATION.Text = Mutation.KDMUTATION
            lDATE.Text = Mutation.TANGGAL
            lKDWAREHOUSEFROM.Text = Mutation.KDWAREHOUSEFROM & " *"
            lKDWAREHOUSETO.Text = Mutation.KDWAREHOUSETO & " *"

            tab1.Text = Mutation.TAB_DETAIL
            tab2.Text = Mutation.TAB_MEMO

            grvDetail.Columns("KDITEM").Caption = Mutation.DETAIL_KDITEM
            grvDetail.Columns("KDUOM").Caption = Mutation.DETAIL_KDUOM
            grvDetail.Columns("QTY").Caption = Mutation.DETAIL_QTY
            grvDetail.Columns("REMARKS").Caption = Mutation.DETAIL_REMARKS

            grvKDWAREHOUSEFROM.Columns("NAME_DISPLAY").Caption = Warehouse.NAME_DISPLAY
            grvKDWAREHOUSETO.Columns("NAME_DISPLAY").Caption = Warehouse.NAME_DISPLAY

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
        sCode = txtKDMUTATION.Text.Trim.ToUpper
    End Sub
    Private Sub fn_ChangeFormState()
        fn_LoadKDWAREHOUSEFROM()
        fn_LoadKDWAREHOUSETO()
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
        grdKDWAREHOUSEFROM.Properties.ReadOnly = Status
        grdKDWAREHOUSETO.Properties.ReadOnly = Status

        txtMEMO.Properties.ReadOnly = Status

        grvDetail.OptionsBehavior.ReadOnly = Status
    End Sub
    Private Sub fn_EmptyMe()
        txtKDMUTATION.Text = "<--- AUTO --->"
        deDATE.DateTime = Now
        grdKDWAREHOUSEFROM.ResetText()
        grdKDWAREHOUSETO.ResetText()
        txtMEMO.ResetText()
    End Sub
    Private Sub fn_LoadData()
        Try
            ' ***** HEADER *****
            Dim ds = oMutation.GetData(sNoId)

            With ds
                txtKDMUTATION.Text = .KDMUTATION
                deDATE.DateTime = .DATE
                grdKDWAREHOUSEFROM.Text = .KDWAREHOUSEFROM
                grdKDWAREHOUSETO.Text = .KDWAREHOUSETO
                txtMEMO.Text = .MEMO

                bindingSource.DataSource = oMutation.GetDataDetail.Where(Function(x) x.KDMUTATION = sNoId).OrderBy(Function(x) x.SEQ).ToList()
                grdDetail.DataSource = bindingSource
            End With
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Function fn_Validate() As Boolean
        Try
            fn_Validate = True

            If grdKDWAREHOUSEFROM.Text = String.Empty Then
                grdKDWAREHOUSEFROM.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                grdKDWAREHOUSEFROM.ErrorText = Statement.ErrorRequired

                grdKDWAREHOUSEFROM.Focus()
                fn_Validate = False
                Exit Function
            End If
            If grdKDWAREHOUSETO.Text = String.Empty Then
                grdKDWAREHOUSETO.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                grdKDWAREHOUSETO.ErrorText = Statement.ErrorRequired

                grdKDWAREHOUSETO.Focus()
                fn_Validate = False
                Exit Function
            End If

            If grdKDWAREHOUSEFROM.Text = grdKDWAREHOUSETO.Text Then
                grdKDWAREHOUSEFROM.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                grdKDWAREHOUSETO.ErrorText = Statement.ErrorRequired

                grdKDWAREHOUSEFROM.Focus()
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
            Dim ds = oMutation.GetStructureHeader
            With ds
                Try
                    .DATECREATED = oMutation.GetData(sNoId).DATECREATED
                Catch oErr As Exception
                    .DATECREATED = Now
                End Try
                .DATEUPDATED = Now

                .KDMUTATION = sNoId
                .DATE = deDATE.DateTime
                .KDWAREHOUSEFROM = IIf(String.IsNullOrEmpty(grdKDWAREHOUSEFROM.EditValue), String.Empty, grdKDWAREHOUSEFROM.EditValue)
                .KDWAREHOUSETO = IIf(String.IsNullOrEmpty(grdKDWAREHOUSETO.EditValue), String.Empty, grdKDWAREHOUSETO.EditValue)
                .MEMO = txtMEMO.Text.Trim.ToUpper
                .KDUSER = sUserID
            End With

            ' ***** DETIL *****
            Dim arrDetail = oMutation.GetStructureDetailList
            For i As Integer = 0 To grvDetail.RowCount - 2
                Dim dsDetail = oMutation.GetStructureDetail
                With dsDetail
                    Try
                        .DATECREATED = oMutation.GetData(sNoId).DATECREATED
                    Catch oErr As Exception
                        .DATECREATED = Now
                    End Try
                    .DATEUPDATED = Now

                    .SEQ = i
                    .KDMUTATION = ds.KDMUTATION
                    .KDITEM = grvDetail.GetRowCellValue(i, colKDITEM)
                    .KDUOM = grvDetail.GetRowCellValue(i, colKDUOM)
                    .QTY = CDec(grvDetail.GetRowCellValue(i, colQTY))
                    .REMARKS = IIf(String.IsNullOrEmpty(grvDetail.GetRowCellValue(i, colREMARKS)), "-", grvDetail.GetRowCellValue(i, colREMARKS))
                End With
                arrDetail.Add(dsDetail)
            Next

            If oFormMode = FORM_MODE.FORM_MODE_ADD Then
                Try
                    fn_Save = oMutation.InsertData(ds, arrDetail)
                Catch oErr As Exception
                    MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            ElseIf oFormMode = FORM_MODE.FORM_MODE_EDIT Then
                Try
                    fn_Save = oMutation.UpdateData(ds, arrDetail)
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
            If CDec(grvDetail.GetFocusedRowCellValue(colQTY)) < 1 Then
                grvDetail.SetFocusedRowCellValue(colQTY, 1)
            End If
        End If
    End Sub
    Private Sub DeleteToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DeleteToolStripMenuItem.Click
        If oFormMode = FORM_MODE.FORM_MODE_VIEW Then Exit Sub
        grvDetail.DeleteSelectedRows()
    End Sub
#End Region
#Region "Command Button"
    Private Sub frmMutation_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
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
    Private Sub fn_LoadKDWAREHOUSEFROM()
        Dim oWAREHOUSEFROM As New Reference.clsWarehouse
        Try
            grdKDWAREHOUSEFROM.Properties.DataSource = oWAREHOUSEFROM.GetData.Where(Function(x) x.ISACTIVE = True).ToList()
            grdKDWAREHOUSEFROM.Properties.ValueMember = "KDWAREHOUSE"
            grdKDWAREHOUSEFROM.Properties.DisplayMember = "NAME_DISPLAY"
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub grdKDWAREHOUSEFROM_KeyDown(sender As System.Object, e As System.Windows.Forms.KeyEventArgs) Handles grdKDWAREHOUSEFROM.KeyDown
        If e.KeyCode = Keys.Delete Then
            grdKDWAREHOUSEFROM.ResetText()
        End If
    End Sub
    Private Sub fn_LoadKDWAREHOUSETO()
        Dim oWAREHOUSETO As New Reference.clsWarehouse
        Try
            grdKDWAREHOUSETO.Properties.DataSource = oWAREHOUSETO.GetData.Where(Function(x) x.ISACTIVE = True).ToList()
            grdKDWAREHOUSETO.Properties.ValueMember = "KDWAREHOUSE"
            grdKDWAREHOUSETO.Properties.DisplayMember = "NAME_DISPLAY"
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub grdKDWAREHOUSETO_KeyDown(sender As System.Object, e As System.Windows.Forms.KeyEventArgs) Handles grdKDWAREHOUSETO.KeyDown
        If e.KeyCode = Keys.Delete Then
            grdKDWAREHOUSETO.ResetText()
        End If
    End Sub
    Private Sub fn_LoadKDITEM()
        If grdKDWAREHOUSEFROM.Text = String.Empty Then Exit Sub
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
            SQL &= "AND B.KDWAREHOUSE = '" & grdKDWAREHOUSEFROM.EditValue & "' "
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
    Private Sub grdKDWAREHOUSEFROM_EditValueChanged(sender As Object, e As EventArgs) Handles grdKDWAREHOUSEFROM.EditValueChanged
        fn_LoadKDITEM()
    End Sub
#End Region
End Class