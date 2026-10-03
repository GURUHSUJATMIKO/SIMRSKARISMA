Imports DataAccess
Imports UI.WIN.MAIN.My.Resources
Imports System.Linq

Public Class frmItem
#Region "Declaration"
    Private oFormMode As FORM_MODE = FORM_MODE.FORM_MODE_VIEW
    Private sNoId As String
    Private isLoad As Boolean = False
    Private oItem As New Reference.clsItem
    Private sParameter As String = String.Empty

#End Region
#Region "Function"
    Public Sub LoadMe(ByVal FormMode As Integer, ByVal Parameter As String, Optional ByVal NoId As String = "")
        oFormMode = FormMode
        sParameter = Parameter
        sNoId = NoId
    End Sub
    Private Sub Form_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        fn_ChangeFormState()
        fn_LoadLanguage()
        isLoad = True

    End Sub
    Public Sub fn_LoadLanguage()
        Try
            Me.Text = sParameter

            lNMITEM1.Text = IIf(sParameter = "OBAT", "Generik", "-") & "*"
            lNMITEM2.Text = sParameter & "*"
            lNMITEM3.Text = "-" & "*"
            lKDPRODUCER.Text = "Pabrik" & "*"

            lKDITEM_L1.Text = sItem_L1 & " *"
            lKDITEM_L2.Text = sItem_L2 & " *"
            lKDITEM_L3.Text = sItem_L3 & " *"
            lKDITEM_L4.Text = sItem_L4 & " *"
            lKDITEM_L5.Text = sItem_L5 & " *"
            lKDITEM_L6.Text = sItem_L6 & " *"
            lKDITEM_L7.Text = sItem_L7 & " *"

            If sParameter <> "OBAT" Then
                colPRICESALESSTANDARD.Caption = "Tarif"
                colPRICESALESTERMIN.Visible = True
                lITEM_L8.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                lITEM_L9.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            Else
                lITEM_L8.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                lITEM_L9.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                colPRICESALESSTANDARD.Caption = "HJA"
                colPRICESALESTERMIN.Visible = False
            End If

            btnSaveNew.Caption = Caption.FormSaveNew
            btnSaveClose.Caption = Caption.FormSaveClose
            btnClose.Caption = Caption.FormClose
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub Form_Closing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        sCode = txtNMITEM2.Text.Trim.ToUpper
    End Sub
    Private Sub fn_ChangeFormState()
        fn_LoadVENDOR()
        fn_LoadWAREHOUSE()
        fn_LoadITEM_L1()
        fn_LoadITEM_L2()
        fn_LoadITEM_L3()
        fn_LoadITEM_L4()
        fn_LoadITEM_L5()
        fn_LoadITEM_L6()
        fn_LoadITEM_L7()
        fn_LoadITEM_L8()
        fn_LoadITEM_L9()
        fn_LoadUOM()
        fn_LoadJASA()
        fn_LoadItem()
        fn_LoadProducer()

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

        txtNMITEM1.Properties.ReadOnly = Status
        txtNMITEM2.Properties.ReadOnly = Status
        txtNMITEM3.Properties.ReadOnly = Status
        chkISACTIVE.Properties.ReadOnly = Status
        grdITEM_L1.Properties.ReadOnly = Status
        grdITEM_L2.Properties.ReadOnly = Status
        grdITEM_L3.Properties.ReadOnly = Status
        grdITEM_L4.Properties.ReadOnly = Status
        grdITEM_L5.Properties.ReadOnly = Status
        grdITEM_L6.Properties.ReadOnly = Status
        grdKDITEM_L8.Properties.ReadOnly = Status
        grdKDITEM_L9.Properties.ReadOnly = Status
        grvDetail_PBF.OptionsBehavior.ReadOnly = Status
        grvDetail_QTY.OptionsBehavior.ReadOnly = Status
        grvDetail_UOM.OptionsBehavior.ReadOnly = Status
        grvDetail_Jasa.OptionsBehavior.ReadOnly = Status
        grvDetail_BHP.OptionsBehavior.ReadOnly = Status
        grdKDPRODUCER.Properties.ReadOnly = Status

        chkISFORNAS.Properties.ReadOnly = Status

        If sParameter <> "OBAT" Then
            colMARGIN.VisibleIndex = -1
            colPRICEPURCHASESTANDARD.VisibleIndex = -1
            lNMITEM1.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            lNMITEM3.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            lKDITEM_L2.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            lKDITEM_L3.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            lKDITEM_L4.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            lKDITEM_L5.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            lKDITEM_L6.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            lKDPRODUCER.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            lISFORNAS.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never

            tab5.PageVisible = False
            tab6.PageVisible = False
        Else
            tab4.PageVisible = False
            tab2.PageVisible = False

            'tabControl.TabPages(4).PageVisible = False
            lNMITEM3.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        End If
    End Sub
    Private Sub fn_EmptyMe()
        chkISACTIVE.Checked = True
        chkISFORNAS.Checked = False
        grdITEM_L1.ResetText()

        Try
            grdITEM_L2.Text = oItem.DefaultItem_L2
        Catch oErr As Exception

        End Try
        Try
            grdKDPRODUCER.Text = oItem.DefaultProducer
        Catch oErr As Exception

        End Try
        Try
            grdITEM_L3.Text = oItem.DefaultItem_L3
        Catch oErr As Exception

        End Try
        Try
            grdITEM_L4.Text = oItem.DefaultItem_L4
        Catch oErr As Exception

        End Try
        Try
            grdITEM_L5.Text = oItem.DefaultItem_L5
        Catch oErr As Exception

        End Try
        Try
            grdITEM_L6.Text = oItem.DefaultItem_L6
        Catch oErr As Exception

        End Try
        Try
            grdITEM_L7.Text = oItem.DefaultItem_L7
        Catch oErr As Exception

        End Try
        Try
            grdKDITEM_L8.Text = oItem.DefaultItem_L8
        Catch oErr As Exception

        End Try
        Try
            grdKDITEM_L9.Text = oItem.DefaultItem_L9
        Catch oErr As Exception

        End Try

        If sParameter <> "OBAT" Then
            Dim oUom As New Reference.clsUOM
            Dim UOM As String = String.Empty
            Dim dsUOM = oUom.GetDataByMEMO("-")
            If dsUOM IsNot Nothing Then
                UOM = dsUOM.KDUOM
            End If

            grvDetail_UOM.Focus()
            grvDetail_UOM.AddNewRow()
            grvDetail_UOM.SetFocusedRowCellValue(colKDUOM, UOM)
            grvDetail_UOM.SetFocusedRowCellValue(colPRICEPURCHASESTANDARD, 0)
            grvDetail_UOM.SetFocusedRowCellValue(colPRICESALESSTANDARD, 0)
            grvDetail_UOM.SetFocusedRowCellValue(colMARGIN, 0)
            grvDetail_UOM.UpdateCurrentRow()
        End If
    End Sub
    Private Sub fn_LoadData()
        Try
            ' ***** HEADER *****
            Dim ds = oItem.GetData(sNoId)

            With ds
                txtNMITEM1.Text = .NMITEM1
                txtNMITEM2.Text = .NMITEM2
                txtNMITEM3.Text = .NMITEM3
                chkISACTIVE.Checked = .ISACTIVE
                grdITEM_L1.Text = .KDITEM_L1
                grdITEM_L2.Text = .KDITEM_L2
                grdITEM_L3.Text = .KDITEM_L3
                grdITEM_L4.Text = .KDITEM_L4
                grdITEM_L5.Text = .KDITEM_L5
                grdITEM_L6.Text = .KDITEM_L6
                grdITEM_L7.Text = .KDITEM_L7
                grdKDITEM_L8.Text = .KDITEM_L8
                grdKDITEM_L9.Text = .KDITEM_L9
                grdKDPRODUCER.Text = .KDPRODUCER
                chkISFORNAS.Checked = .ISFORNAS

                bindingSource_UOM.DataSource = oItem.GetDataDetail_UOM(sNoId).OrderBy(Function(x) x.M_UOM.MEMO).ToList()
                grdDetail_UOM.DataSource = bindingSource_UOM

                BindingSource_Jasa.DataSource = oItem.GetDataDetail_JASA(sNoId).OrderBy(Function(x) x.M_JASA.MEMO).ToList()
                grdDetail_Jasa.DataSource = BindingSource_Jasa

                BindingSource_BHP.DataSource = oItem.GetDataDetail_ITEM(sNoId).OrderBy(Function(x) x.SEQ).ToList()
                grdDetail_BHP.DataSource = BindingSource_BHP

                BindingSource_QTY.DataSource = oItem.GetDataDetail_QTY(sNoId).OrderBy(Function(x) x.KDWAREHOUSE).ToList()
                grdDetail_QTY.DataSource = BindingSource_QTY

                BindingSource_PBF.DataSource = oItem.GetDataDetail_VENDOR(sNoId).OrderBy(Function(x) x.KDVENDOR).ToList()
                grdDetail_PBF.DataSource = BindingSource_PBF

                tabControl.SelectedTabPage = tab6
                tabControl.SelectedTabPage = tab5
                tabControl.SelectedTabPage = tab4
                tabControl.SelectedTabPage = tab3
                tabControl.SelectedTabPage = tab2
                tabControl.SelectedTabPage = tab1
            End With
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Function ByteArrayToImage(ByVal byteArrayIn() As Byte) As Image
        Using ms As New System.IO.MemoryStream(byteArrayIn)
            Dim returnImage = Image.FromStream(ms)
            Return returnImage
        End Using
    End Function
    Private Function ImageToByteArray(ByVal ImageIn As System.Drawing.Image) As Byte()

        Using ms As New System.IO.MemoryStream
            ImageIn.Save(ms, System.Drawing.Imaging.ImageFormat.Gif)
            Return ms.ToArray
        End Using
    End Function
    Private Function fn_Validate() As Boolean
        Try
            fn_Validate = True

            If txtNMITEM2.Text = String.Empty Then
                txtNMITEM2.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                txtNMITEM2.ErrorText = Statement.ErrorRequired
                txtNMITEM2.Focus()

                txtNMITEM2.Focus()
                fn_Validate = False
                Exit Function
            End If
            If grdITEM_L1.Text = String.Empty Then
                tabControl.SelectedTabPage = tab3

                grdITEM_L1.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                grdITEM_L1.ErrorText = Statement.ErrorRequired
                grdITEM_L1.Focus()
                fn_Validate = False
                Exit Function
            End If
            If grdITEM_L2.Text = String.Empty Then
                tabControl.SelectedTabPage = tab3

                grdITEM_L2.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                grdITEM_L2.ErrorText = Statement.ErrorRequired
                grdITEM_L2.Focus()
                fn_Validate = False
                Exit Function
            End If
            If grdKDPRODUCER.Text = String.Empty Then
                grdKDPRODUCER.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                grdKDPRODUCER.ErrorText = Statement.ErrorRequired
                grdKDPRODUCER.Focus()
                fn_Validate = False
                Exit Function
            End If
            If grdITEM_L3.Text = String.Empty Then
                tabControl.SelectedTabPage = tab3

                grdITEM_L3.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                grdITEM_L3.ErrorText = Statement.ErrorRequired
                grdITEM_L3.Focus()
                fn_Validate = False
                Exit Function
            End If
            If grdITEM_L4.Text = String.Empty Then
                tabControl.SelectedTabPage = tab3

                grdITEM_L4.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                grdITEM_L4.ErrorText = Statement.ErrorRequired
                grdITEM_L4.Focus()
                fn_Validate = False
                Exit Function
            End If
            If grdITEM_L5.Text = String.Empty Then
                tabControl.SelectedTabPage = tab3

                grdITEM_L5.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                grdITEM_L5.ErrorText = Statement.ErrorRequired
                grdITEM_L5.Focus()
                fn_Validate = False
                Exit Function
            End If
            If grdITEM_L6.Text = String.Empty Then
                tabControl.SelectedTabPage = tab3

                grdITEM_L6.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                grdITEM_L6.ErrorText = Statement.ErrorRequired
                grdITEM_L6.Focus()
                fn_Validate = False
                Exit Function
            End If

            If grdITEM_L7.Text = String.Empty Then
                tabControl.SelectedTabPage = tab3

                grdITEM_L7.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                grdITEM_L7.ErrorText = Statement.ErrorRequired
                grdITEM_L7.Focus()
                fn_Validate = False
                Exit Function
            End If
            'If oFormMode = FORM_MODE.FORM_MODE_ADD Then
            '    If oItem.IsExist(txtNMITEM2.Text.ToUpper.Trim) = True Then
            '        txtNMITEM2.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
            '        txtNMITEM2.ErrorText = Statement.ErrorRegistered
            '        txtNMITEM2.Focus()
            '        fn_Validate = False
            '        Exit Function
            '    End If
            'Else
            '    If txtNMITEM2.Text.Trim.ToUpper <> oItem.GetData(sNoId).NMITEM2 Then
            '        If oItem.IsExist(txtNMITEM2.Text.ToUpper.Trim) = True Then
            '            txtNMITEM2.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
            '            txtNMITEM2.ErrorText = Statement.ErrorRegistered
            '            txtNMITEM2.Focus()
            '            fn_Validate = False
            '            Exit Function
            '        End If
            '    End If
            'End If

            grvDetail_UOM.UpdateCurrentRow()
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Function
    Private Function fn_Save() As Boolean
        Try

            ' ***** HEADER *****
            Dim ds = oItem.GetStructureHeader
            With ds
                Try
                    .DATECREATED = oItem.GetData(sNoId).DATECREATED
                Catch oErr As Exception
                    .DATECREATED = Now
                End Try
                .DATEUPDATED = Now

                .KDITEM = sNoId
                .NMITEM1 = txtNMITEM1.Text.Trim.ToUpper
                .NMITEM2 = txtNMITEM2.Text.Trim.ToUpper
                .NMITEM3 = txtNMITEM3.Text.Trim.ToUpper
                .ISACTIVE = chkISACTIVE.Checked
                .KDITEM_L1 = grdITEM_L1.EditValue.ToString.Trim.ToUpper
                .KDITEM_L2 = grdITEM_L2.EditValue.ToString.Trim.ToUpper
                .KDITEM_L3 = grdITEM_L3.EditValue.ToString.Trim.ToUpper
                .KDITEM_L4 = grdITEM_L4.EditValue.ToString.Trim.ToUpper
                .KDITEM_L5 = grdITEM_L5.EditValue.ToString.Trim.ToUpper
                .KDITEM_L6 = grdITEM_L6.EditValue.ToString.Trim.ToUpper
                .KDITEM_L8 = grdKDITEM_L8.EditValue

                If sParameter = "OBAT" Then
                    .KDITEM_L9 = "1"
                Else
                    .KDITEM_L9 = grdKDITEM_L9.EditValue
                End If

                .KDITEM_L10 = ""
                .QTYMAXIMUM = 0
                .QTYMINIMUM = 0
                .ISTAX = False
                Try
                    .DATE_EXPIRE = oItem.GetData(sNoId).DATE_EXPIRE
                Catch oErr As Exception
                    .DATE_EXPIRE = Now
                End Try
                .KDITEM_L7 = grdITEM_L7.EditValue.ToString.Trim.ToUpper
                .KDPRODUCER = grdKDPRODUCER.EditValue
                .ISFORNAS = chkISFORNAS.Checked
            End With

            ' ***** Satuan *****
            Dim arrDetail_UOM = oItem.GetStructureDetail_UOMList
            For i As Integer = 0 To grvDetail_UOM.RowCount - 2
                Dim dsDetail_UOM = oItem.GetStructureDetail_UOM
                With dsDetail_UOM
                    Try
                        .DATECREATED = oItem.GetData(sNoId).DATECREATED
                    Catch oErr As Exception
                        .DATECREATED = Now
                    End Try
                    .DATEUPDATED = Now
                    .KDITEM = ds.KDITEM
                    .KDUOM = grvDetail_UOM.GetRowCellValue(i, colKDUOM)
                    .RATE = 1
                    .PRICEPURCHASESTANDARD = CDec(grvDetail_UOM.GetRowCellValue(i, colPRICEPURCHASESTANDARD))
                    .PRICESALESSTANDARD = CDec(grvDetail_UOM.GetRowCellValue(i, colPRICESALESSTANDARD))
                    .MARGIN = CDec(grvDetail_UOM.GetRowCellValue(i, colMARGIN))
                    .PRICESALESTERMIN = CDec(grvDetail_UOM.GetRowCellValue(i, colPRICESALESTERMIN))
                End With
                arrDetail_UOM.Add(dsDetail_UOM)
            Next

            ' ***** Jasa *****
            Dim arrDetail_JASA = oItem.GetStructureDetail_JASAList
            For i As Integer = 0 To grvDetail_Jasa.RowCount - 2
                Dim dsDetail_JASA = oItem.GetStructureDetail_JASA
                With dsDetail_JASA
                    Try
                        .DATECREATED = oItem.GetData(sNoId).DATECREATED
                    Catch oErr As Exception
                        .DATECREATED = Now
                    End Try
                    .DATEUPDATED = Now
                    .KDITEM = ds.KDITEM
                    .KDJASA = grvDetail_Jasa.GetRowCellValue(i, colKDJASA)
                    .RATE_NONUMUM = CDec(grvDetail_Jasa.GetRowCellValue(i, colRATE_NONUMUM))
                    .RATE_UMUM = CDec(grvDetail_Jasa.GetRowCellValue(i, colRATE_UMUM))
                End With
                arrDetail_JASA.Add(dsDetail_JASA)
            Next

            ' ***** BHP *****
            Dim arrDetail_ITEM = oItem.GetStructureDetail_ITEMList
            For i As Integer = 0 To grvDetail_BHP.RowCount - 2
                Dim dsDetail_ITEM = oItem.GetStructureDetail_ITEM
                With dsDetail_ITEM
                    Try
                        .DATECREATED = oItem.GetData(sNoId).DATECREATED
                    Catch oErr As Exception
                        .DATECREATED = Now
                    End Try
                    .DATEUPDATED = Now
                    .KDITEM_H = ds.KDITEM
                    .KDITEM = grvDetail_BHP.GetRowCellValue(i, colKDITEM)
                    .KDUOM = grvDetail_BHP.GetRowCellValue(i, colKDUOM_)
                    .SEQ = i
                    .QTY = grvDetail_BHP.GetRowCellValue(i, colQTY)
                End With
                arrDetail_ITEM.Add(dsDetail_ITEM)
            Next

            ' ***** QTY *****
            Dim arrDetail_QTY = oItem.GetStructureDetail_QTYList
            For i As Integer = 0 To grvDetail_QTY.RowCount - 2
                Dim dsDetail_QTY = oItem.GetStructureDetail_QTY
                With dsDetail_QTY
                    .KDITEM = ds.KDITEM
                    .KDWAREHOUSE = grvDetail_QTY.GetRowCellValue(i, colKDWAREHOUSE)
                    .QTYMAX = grvDetail_QTY.GetRowCellValue(i, colQTYMAX)
                    .QTYMIN = grvDetail_QTY.GetRowCellValue(i, colQTYMIN)
                    .QTYORDER = 0
                End With
                arrDetail_QTY.Add(dsDetail_QTY)
            Next

            ' ***** VENDOR *****
            Dim arrDetail_VENDOR = oItem.GetStructureDetail_VENDORList
            For i As Integer = 0 To grvDetail_PBF.RowCount - 2
                Dim dsDetail_VENDOR = oItem.GetStructureDetail_VENDOR
                With dsDetail_VENDOR
                    .KDITEM = ds.KDITEM
                    .KDVENDOR = grvDetail_PBF.GetRowCellValue(i, colKDVENDOR)
                End With
                arrDetail_VENDOR.Add(dsDetail_VENDOR)
            Next

            If oFormMode = FORM_MODE.FORM_MODE_ADD Then
                Try
                    fn_Save = oItem.InsertData(sParameter, ds, arrDetail_UOM, arrDetail_JASA, arrDetail_ITEM, arrDetail_QTY, arrDetail_VENDOR)
                Catch oErr As Exception
                    MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            ElseIf oFormMode = FORM_MODE.FORM_MODE_EDIT Then
                Try
                    fn_Save = oItem.UpdateData(ds, arrDetail_UOM, arrDetail_JASA, arrDetail_ITEM, arrDetail_QTY, arrDetail_VENDOR)
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
    Private Sub grvDetail_CellValueChanged(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs) Handles grvDetail_UOM.CellValueChanged
        If e.Column.Name = colKDUOM.Name Then
            Try
                If grvDetail_UOM.GetFocusedRowCellValue(colKDUOM) IsNot Nothing Then
                    For i As Integer = 0 To grvDetail_UOM.RowCount - 2
                        If grvDetail_UOM.GetFocusedRowCellValue(colKDUOM) = grvDetail_UOM.GetRowCellValue(i, colKDUOM) Then
                            MsgBox(Statement.ErrorRegistered, MsgBoxStyle.Exclamation, Me.Text)
                            grvDetail_UOM.CancelUpdateCurrentRow()
                            Exit Sub
                        End If
                    Next
                    grvDetail_UOM.SetFocusedRowCellValue(colPRICESALESTERMIN, 0)
                End If
            Catch oErr As Exception
                MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            End Try
        ElseIf e.Column.Name = colMARGIN.Name Or e.Column.Name = colPRICEPURCHASESTANDARD.Name Then
            If sParameter = "OBAT" Then
                Dim sHargajual As Decimal = (CDec(grvDetail_UOM.GetFocusedRowCellValue(colPRICEPURCHASESTANDARD)) / 100) * (CDec(grvDetail_UOM.GetFocusedRowCellValue(colMARGIN))) + CDec(grvDetail_UOM.GetFocusedRowCellValue(colPRICEPURCHASESTANDARD))
                grvDetail_UOM.SetFocusedRowCellValue(colPRICESALESSTANDARD, sHargajual)
            End If
        End If
    End Sub
    Private Sub grvDetail_BHP_CellValueChanged(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs) Handles grvDetail_BHP.CellValueChanged
        If e.Column.Name = colKDITEM.Name Then
            Dim oItem As New Reference.clsItem
            Try
                If grvDetail_BHP.GetFocusedRowCellValue(colKDITEM) IsNot Nothing Then
                    Dim ds = oItem.GetDataDetail_UOM(grvDetail_BHP.GetFocusedRowCellValue(colKDITEM))

                    If ds IsNot Nothing Then
                        grvDetail_BHP.SetFocusedRowCellValue(colKDUOM_, ds.FirstOrDefault(Function(x) x.RATE = 1).KDUOM)
                    Else
                        MsgBox(Statement.ErrorUOM, MsgBoxStyle.Exclamation, Me.Text)

                        grvDetail_BHP.CancelUpdateCurrentRow()
                    End If
                End If
            Catch oErr As Exception
                MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            End Try
        ElseIf e.Column.Name = colKDUOM_.Name Then
            Dim oItem As New Reference.clsItem
            Try
                If grvDetail_BHP.GetFocusedRowCellValue(colKDITEM) IsNot Nothing And grvDetail_BHP.GetFocusedRowCellValue(colKDUOM_) IsNot Nothing Then
                    Dim ds = oItem.GetDataDetail_UOM(grvDetail_BHP.GetFocusedRowCellValue(colKDITEM), grvDetail_BHP.GetFocusedRowCellValue(colKDUOM_))

                    If ds IsNot Nothing Then

                    Else
                        MsgBox(Statement.ErrorUOM, MsgBoxStyle.Exclamation, Me.Text)

                        Dim sItem = grvDetail_BHP.GetFocusedRowCellValue(colKDITEM)
                        grvDetail_BHP.CancelUpdateCurrentRow()

                        grvDetail_BHP.AddNewRow()
                        grvDetail_BHP.SetFocusedRowCellValue(colKDITEM, sItem)
                    End If
                End If
            Catch oErr As Exception
                MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            End Try
        ElseIf e.Column.Name = colQTY.Name Then
            If CDec(grvDetail_BHP.GetFocusedRowCellValue(colQTY)) = 0 Then
                grvDetail_BHP.SetFocusedRowCellValue(colQTY, 1)
            End If
        End If
    End Sub
    Private Sub DeleteToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DeleteToolStripMenuItem.Click
        If oFormMode = FORM_MODE.FORM_MODE_VIEW Then Exit Sub
        grvDetail_UOM.DeleteSelectedRows()
    End Sub
    Private Sub ToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem1.Click
        If oFormMode = FORM_MODE.FORM_MODE_VIEW Then Exit Sub
        grvDetail_BHP.DeleteSelectedRows()
    End Sub
    Private Sub ToolStripMenuItem2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem2.Click
        If oFormMode = FORM_MODE.FORM_MODE_VIEW Then Exit Sub
        grvDetail_QTY.DeleteSelectedRows()
    End Sub
    Private Sub ToolStripMenuItem3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem3.Click
        If oFormMode = FORM_MODE.FORM_MODE_VIEW Then Exit Sub
        grvDetail_PBF.DeleteSelectedRows()
    End Sub
#End Region
#Region "Command Button"
    Private Sub frmItem_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
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
    Private Sub fn_LoadItem()
        Dim oItem As New Reference.clsItem
        Try
            grdKDITEM.DataSource = oItem.GetData.Where(Function(x) x.ISACTIVE = True And x.M_ITEM_L1.MEMO = "OBAT").ToList()
            grdKDITEM.ValueMember = "KDITEM"
            grdKDITEM.DisplayMember = "NMITEM2"
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadUOM()
        Dim oUOM As New Reference.clsUOM
        Try
            If sParameter = "OBAT" Then
                grdUOM.DataSource = oUOM.GetData.Where(Function(x) x.ISACTIVE = True).ToList()
                grdUOM.ValueMember = "KDUOM"
                grdUOM.DisplayMember = "MEMO"
            Else
                grdUOM.DataSource = oUOM.GetData.Where(Function(x) x.ISACTIVE = True And x.MEMO = "-").ToList()
                grdUOM.ValueMember = "KDUOM"
                grdUOM.DisplayMember = "MEMO"
            End If

            grdKDUOM.DataSource = oUOM.GetData.Where(Function(x) x.ISACTIVE = True).ToList()
            grdKDUOM.ValueMember = "KDUOM"
            grdKDUOM.DisplayMember = "MEMO"
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadJASA()
        Dim oJASA As New Reference.clsItemJasa
        Try
            grdKDJASA.DataSource = oJASA.GetData.Where(Function(x) x.ISACTIVE = True).ToList()
            grdKDJASA.ValueMember = "KDJASA"
            grdKDJASA.DisplayMember = "MEMO"
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadProducer()
        Dim oProducer As New Reference.clsProducer
        Try
            grdKDPRODUCER.Properties.DataSource = oProducer.GetData.Where(Function(x) x.ISACTIVE = True).ToList()
            grdKDPRODUCER.Properties.ValueMember = "KDPRODUCER"
            grdKDPRODUCER.Properties.DisplayMember = "NAME_DISPLAY"
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadWAREHOUSE()
        Dim oWAREHOUSE As New Reference.clsWarehouse
        Try
            grdKDWAREHOUSE.DataSource = oWAREHOUSE.GetData.Where(Function(x) x.ISACTIVE = True).ToList()
            grdKDWAREHOUSE.ValueMember = "KDWAREHOUSE"
            grdKDWAREHOUSE.DisplayMember = "NAME_DISPLAY"
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadVENDOR()
        Dim oVENDOR As New Reference.clsVendor
        Try
            grdKDVENDOR.DataSource = oVENDOR.GetData.Where(Function(x) x.ISACTIVE = True).ToList()
            grdKDVENDOR.ValueMember = "KDVENDOR"
            grdKDVENDOR.DisplayMember = "NAME_DISPLAY"
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadITEM_L1()
        Dim oItem_L1 As New Reference.clsItem_L1
        Try
            grdITEM_L1.Properties.DataSource = oItem_L1.GetData.Where(Function(x) x.ISACTIVE = True And IIf(sParameter = "OBAT", x.MEMO = "OBAT", x.MEMO <> "OBAT")).ToList()
            grdITEM_L1.Properties.ValueMember = "KDITEM_L1"
            grdITEM_L1.Properties.DisplayMember = "MEMO"
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub grdITEM_L1_KeyDown(sender As System.Object, e As System.Windows.Forms.KeyEventArgs) Handles grdITEM_L1.KeyDown
        If e.KeyCode = Keys.Delete Then
            grdITEM_L1.ResetText()
        End If
    End Sub
    Private Sub fn_LoadITEM_L2()
        Dim oItem_L2 As New Reference.clsItem_L2
        Try
            grdITEM_L2.Properties.DataSource = oItem_L2.GetData.Where(Function(x) x.ISACTIVE = True).ToList()
            grdITEM_L2.Properties.ValueMember = "KDITEM_L2"
            grdITEM_L2.Properties.DisplayMember = "MEMO"
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub grdITEM_L2_KeyDown(sender As System.Object, e As System.Windows.Forms.KeyEventArgs) Handles grdITEM_L2.KeyDown
        If e.KeyCode = Keys.Delete Then
            grdITEM_L2.ResetText()
        End If
    End Sub
    Private Sub fn_LoadITEM_L3()
        Dim oItem_L3 As New Reference.clsItem_L3
        Try
            grdITEM_L3.Properties.DataSource = oItem_L3.GetData.Where(Function(x) x.ISACTIVE = True).ToList()
            grdITEM_L3.Properties.ValueMember = "KDITEM_L3"
            grdITEM_L3.Properties.DisplayMember = "MEMO"
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub grdITEM_L3_KeyDown(sender As System.Object, e As System.Windows.Forms.KeyEventArgs) Handles grdITEM_L3.KeyDown
        If e.KeyCode = Keys.Delete Then
            grdITEM_L3.ResetText()
        End If
    End Sub
    Private Sub fn_LoadITEM_L4()
        Dim oItem_L4 As New Reference.clsItem_L4
        Try
            grdITEM_L4.Properties.DataSource = oItem_L4.GetData.Where(Function(x) x.ISACTIVE = True).ToList()
            grdITEM_L4.Properties.ValueMember = "KDITEM_L4"
            grdITEM_L4.Properties.DisplayMember = "MEMO"
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub grdITEM_L4_KeyDown(sender As System.Object, e As System.Windows.Forms.KeyEventArgs) Handles grdITEM_L4.KeyDown
        If e.KeyCode = Keys.Delete Then
            grdITEM_L4.ResetText()
        End If
    End Sub
    Private Sub fn_LoadITEM_L5()
        Dim oItem_L5 As New Reference.clsItem_L5
        Try
            grdITEM_L5.Properties.DataSource = oItem_L5.GetData.Where(Function(x) x.ISACTIVE = True).ToList()
            grdITEM_L5.Properties.ValueMember = "KDITEM_L5"
            grdITEM_L5.Properties.DisplayMember = "MEMO"
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub grdITEM_L5_KeyDown(sender As System.Object, e As System.Windows.Forms.KeyEventArgs) Handles grdITEM_L5.KeyDown
        If e.KeyCode = Keys.Delete Then
            grdITEM_L5.ResetText()
        End If
    End Sub
    Private Sub fn_LoadITEM_L6()
        Dim oItem_L6 As New Reference.clsItem_L6
        Try
            grdITEM_L6.Properties.DataSource = oItem_L6.GetData.Where(Function(x) x.ISACTIVE = True).ToList()
            grdITEM_L6.Properties.ValueMember = "KDITEM_L6"
            grdITEM_L6.Properties.DisplayMember = "MEMO"
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub grdITEM_L6_KeyDown(sender As System.Object, e As System.Windows.Forms.KeyEventArgs) Handles grdITEM_L6.KeyDown
        If e.KeyCode = Keys.Delete Then
            grdITEM_L6.ResetText()
        End If
    End Sub
    Private Sub fn_LoadITEM_L7()
        Dim oItem_L7 As New Reference.clsItem_L7
        Try
            grdITEM_L7.Properties.DataSource = oItem_L7.GetData.Where(Function(x) x.ISACTIVE = True).ToList()
            grdITEM_L7.Properties.ValueMember = "KDITEM_L7"
            grdITEM_L7.Properties.DisplayMember = "MEMO"
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadITEM_L8()
        Dim oItem_L8 As New Reference.clsItem_L8
        Try
            grdKDITEM_L8.Properties.DataSource = oItem_L8.GetData.Where(Function(x) x.ISACTIVE = True).ToList()
            grdKDITEM_L8.Properties.ValueMember = "KDITEM_L8"
            grdKDITEM_L8.Properties.DisplayMember = "MEMO"
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadITEM_L9()
        Dim oItem_L9 As New Reference.clsItem_L9
        Try
            grdKDITEM_L9.Properties.DataSource = oItem_L9.GetData.Where(Function(x) x.ISACTIVE = True).ToList()
            grdKDITEM_L9.Properties.ValueMember = "KDITEM_L9"
            grdKDITEM_L9.Properties.DisplayMember = "MEMO"
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
#End Region
End Class