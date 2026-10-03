Imports DataAccess
Imports UI.WIN.MAIN.My.Resources
Imports System.Linq
Imports Newtonsoft.Json.Linq
Imports System.Data.SqlClient

Public Class frmBilling
#Region "Declaration"
    Private oFormMode As FORM_MODE = FORM_MODE.FORM_MODE_VIEW
    Private sNoId As String
    Private isLoad As Boolean = False
    Private oBilling As New Transaksi.clsBilling
    Private sCategory As Integer = 0

#End Region
#Region "Function"
    Public Sub LoadMe(ByVal FormMode As Integer, ByVal Category As Integer, Optional ByVal NoId As String = "")
        oFormMode = FormMode
        sCategory = Category
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
        fn_LoadWarehuse()
        fn_LoadItemH()
        fn_LoadDoctor()
        fn_LoadSatuan()
        fn_LoadPenjamin()
        fn_LoadItemTarif()
        fn_LoadItemObat()

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
        txtKDPENDAFTARAN.Properties.ReadOnly = True
        grdKDDOCTOR_H.Properties.ReadOnly = True
        grdKDPENJAMIN.Properties.ReadOnly = True
        grvDetail.OptionsBehavior.ReadOnly = Status
        If sKDWAREHOUSEAUTO <> String.Empty Then
            lUNIT.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        Else
            lUNIT.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        End If
    End Sub
    Private Sub fn_EmptyMe()
        txtKDBILLING.Text = "<--- AUTO --->"
        deDATE.DateTime = Now
        txtKDKUNJUNGAN.ResetText()
        txtKDPENDAFTARAN.ResetText()
        grdKDDOCTOR_H.ResetText()
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
                grdKDDOCTOR_H.Text = .KDDOCTOR
                grdKDPENJAMIN.Text = .KDPENJAMIN
                txtKDKUNJUNGAN.Text = .KDKUNJUNGAN
                txtGRANDTOTAL.Text = .GRANDTOTAL
                grdKDWAREHOUSE.Text = .KDWAREHOUSE

                bindingSource.DataSource = oBilling.GetDataDetail(sNoId)
                grdDetail.DataSource = bindingSource

                BindingSource_BHP.DataSource = oBilling.GetDataDetail_BHP(sNoId)
                grdDetail_BHP.DataSource = BindingSource_BHP

                tabControl.SelectedTabPage = tab3
                tabControl.SelectedTabPage = tab2
                tabControl.SelectedTabPage = tab1

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
                .KDWAREHOUSE = IIf(sKDWAREHOUSEAUTO = String.Empty, oBilling.Daftar_Warehouse_Default, grdKDWAREHOUSE.EditValue)
                .KDBILLING = sNoId
                .KDPENDAFTARAN = txtKDPENDAFTARAN.Text
                .KDDOCTOR = grdKDDOCTOR_H.EditValue
                .KDPENJAMIN = grdKDPENJAMIN.EditValue
                .KDKUNJUNGAN = txtKDKUNJUNGAN.Text
                .SUBTOTAL = CDec(txtGRANDTOTAL.Text)
                .DISCOUNT = CDec(0)
                .TAX = CDec(0)
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
            Dim arrDetail = oBilling.GetStructureDetailList
            For i As Integer = 0 To grvDetail.RowCount - 2
                Dim dsDetail = oBilling.GetStructureDetail
                With dsDetail
                    .KDBILLING = ds.KDBILLING
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

            ' ***** DETIL *****
            Dim arrDetail_BHP = oBilling.GetStructureDetailBHPList
            For i As Integer = 0 To grvDetail_BHP.RowCount - 2
                Dim dsDetail = oBilling.GetStructureDetailBHP
                With dsDetail
                    Try
                        .DATECREATED = oBilling.GetData(sNoId).DATECREATED
                    Catch oErr As Exception
                        .DATECREATED = Now
                    End Try
                    .DATEUPDATED = Now
                    .KDBILLING = ds.KDBILLING
                    .KDITEM = grvDetail_BHP.GetRowCellValue(i, colKDITEM_)
                    .KDUOM = grvDetail_BHP.GetRowCellValue(i, colKDUOM_)
                    .SEQ = i
                    .QTY = grvDetail_BHP.GetRowCellValue(i, colQTY_)
                    .REMARKS = grvDetail_BHP.GetRowCellValue(i, colREMARKS_)
                End With
                arrDetail_BHP.Add(dsDetail)
            Next

            If oFormMode = FORM_MODE.FORM_MODE_ADD Then
                Try
                    fn_Save = oBilling.InsertData(ds, arrDetail, Nothing, arrDetail_BHP)
                Catch oErr As Exception
                    MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            ElseIf oFormMode = FORM_MODE.FORM_MODE_EDIT Then
                Try
                    fn_Save = oBilling.UpdateData(ds, arrDetail, Nothing, arrDetail_BHP)
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
    Private Sub grvDetail_BHP_CellValueChanged(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs) Handles grvDetail_BHP.CellValueChanged
        If e.Column.Name = colKDITEM_.Name Then
            Dim oItem As New Reference.clsItem
            Try
                If grvDetail_BHP.GetFocusedRowCellValue(colKDITEM_) IsNot Nothing Then
                    Dim ds = oItem.GetDataDetail_UOM(grvDetail_BHP.GetFocusedRowCellValue(colKDITEM_))

                    If ds IsNot Nothing Then
                        grvDetail_BHP.SetFocusedRowCellValue(colKDUOM, ds.FirstOrDefault(Function(x) x.RATE = 1).KDUOM)
                        grvDetail_BHP.SetFocusedRowCellValue(colREMARKS_, "")
                    Else
                        MsgBox(Statement.ErrorUOM, MsgBoxStyle.Exclamation, Me.Text)

                        grvDetail_BHP.CancelUpdateCurrentRow()
                    End If
                End If
            Catch oErr As Exception
                MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            End Try
        ElseIf e.Column.Name = colKDUOM.Name Then
            Dim oItem As New Reference.clsItem
            Try
                If grvDetail_BHP.GetFocusedRowCellValue(colKDITEM_) IsNot Nothing And grvDetail_BHP.GetFocusedRowCellValue(colKDUOM) IsNot Nothing Then
                    Dim ds = oItem.GetDataDetail_UOM(grvDetail_BHP.GetFocusedRowCellValue(colKDITEM_), grvDetail_BHP.GetFocusedRowCellValue(colKDUOM))

                    If ds IsNot Nothing Then

                    Else
                        MsgBox(Statement.ErrorUOM, MsgBoxStyle.Exclamation, Me.Text)

                        Dim sItem = grvDetail_BHP.GetFocusedRowCellValue(colKDITEM_)
                        grvDetail_BHP.CancelUpdateCurrentRow()

                        grvDetail_BHP.AddNewRow()
                        grvDetail_BHP.SetFocusedRowCellValue(colKDITEM_, sItem)
                    End If
                End If
            Catch oErr As Exception
                MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            End Try
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
    Private Sub fn_LoadWarehuse()
        Dim oWarehouse As New Reference.clsWarehouse
        Try
            grdKDWAREHOUSE.Properties.DataSource = oWarehouse.GetData.Where(Function(x) x.ISACTIVE = True).ToList()
            grdKDWAREHOUSE.Properties.ValueMember = "KDWAREHOUSE"
            grdKDWAREHOUSE.Properties.DisplayMember = "NAME_DISPLAY"
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
        grvDetail.SetFocusedRowCellValue(colKDDOCTOR, 9)
        grvDetail.UpdateCurrentRow()

        Dim dsDetailBHP = oItem.GetDataDetail_ITEM(grvD.GetFocusedRowCellValue("KDITEM"))

        If dsDetailBHP.Count > 0 Then
            tabControl.SelectedTabPage = tab3
            tabControl.SelectedTabPage = tab2
            tabControl.SelectedTabPage = tab1
        End If
        For Each xloop In dsDetailBHP
            grvDetail_BHP.Focus()
            grvDetail_BHP.AddNewRow()
            grvDetail_BHP.SetFocusedRowCellValue(colKDITEM_, xloop.KDITEM)
            grvDetail_BHP.SetFocusedRowCellValue(colQTY_, xloop.QTY)
            grvDetail_BHP.UpdateCurrentRow()
        Next
    End Sub
    Private Sub fn_LoadItemH()
        Dim oUser As New Setting.clsUser
        Dim oItem As New Reference.clsItem_L1

        Try

            Dim ds = From x In oUser.GetDataDetailT
                     Join y In oItem.GetData
                     On x.KDITEM_L1 Equals y.KDITEM_L1
                     Where x.KDUSER = sUserID
                     Select x.KDITEM_L1, Nama = y.MEMO

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
    Private Sub fn_LoadDoctor()
        Dim oDoctor As New Reference.clsDoctor
        Try
            grdKDDOCTOR.DataSource = oDoctor.GetData.Where(Function(x) x.ISACTIVE = True).ToList()
            grdKDDOCTOR.ValueMember = "KDDOCTOR"
            grdKDDOCTOR.DisplayMember = "NAME_DISPLAY"

            grdKDDOCTOR_H.Properties.DataSource = oDoctor.GetData.Where(Function(x) x.ISACTIVE = True).ToList()
            grdKDDOCTOR_H.Properties.ValueMember = "KDDOCTOR"
            grdKDDOCTOR_H.Properties.DisplayMember = "NAME_DISPLAY"
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadItemObat()
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

            grdKDITEM_.DataSource = ds.Tables("ITEM")
            grdKDITEM_.ValueMember = "KDITEM"
            grdKDITEM_.DisplayMember = "NMITEM2"

            If oConn.State = ConnectionState.Open Then
                oConn.Close()
            End If

        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
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
    Private Sub fn_LoadSatuan()
        Dim oUom As New Reference.clsUOM
        Try
            grdKDUOM.DataSource = oUom.GetData.Where(Function(x) x.ISACTIVE = True).ToList()
            grdKDUOM.ValueMember = "KDUOM"
            grdKDUOM.DisplayMember = "MEMO"

            grdKDUOM_.DataSource = oUom.GetData.Where(Function(x) x.ISACTIVE = True).ToList()
            grdKDUOM_.ValueMember = "KDUOM"
            grdKDUOM_.DisplayMember = "MEMO"
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
            grdKDDOCTOR_H.Text = dsRJ.KDDOCTOR
            txtKDPENDAFTARAN.Text = dsRJ.KDPENDAFTARAN
            grdKDPENJAMIN.Text = dsRJ.KDPENJAMIN
        Else
            Dim oPendafataran_KunjunganRuangan As New Admission.clsPendaftaran_KunjunganRuangan
            Dim dsRI = oPendafataran_KunjunganRuangan.GetData(grdCARIKDREGAWAL.EditValue)
            If dsRI IsNot Nothing Then
                txtKDKUNJUNGAN.Text = dsRI.KDKUNJUNGAN_RUANGAN
                grdKDDOCTOR_H.Text = dsRI.KDDOCTOR
                txtKDPENDAFTARAN.Text = dsRI.KDPENDAFTARAN
                grdKDPENJAMIN.Text = dsRI.KDPENJAMIN
            Else
                fn_EmptyMe()
            End If
        End If
    End Sub
    Private Sub grdKDWAREHOUSE_EditValueChanged(sender As Object, e As EventArgs) Handles grdKDWAREHOUSE.EditValueChanged
        fn_LoadItemObat()
    End Sub
#End Region
End Class