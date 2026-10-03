Imports DataAccess
Imports UI.WIN.MAIN.My.Resources
Imports System.Linq
Imports Newtonsoft.Json.Linq
Imports System.Data.SqlClient

Public Class frmBillingRadiologi
#Region "Declaration"
    Private oFormMode As FORM_MODE = FORM_MODE.FORM_MODE_VIEW
    Private sNoId As String
    Private isLoad As Boolean = False
    Private oBillingPoli As New Billing.clsBillingPoli
    Private oBillingRuangan As New Billing.clsBillingRuangan
    Private sKDDOCTOR As String = String.Empty
    Private sKD_DEPARTMENT_RUANGAN As String = String.Empty
    Private sCategory As Integer = 0
    Private sValidasiLab As Boolean = False

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
            Me.Text = BillingRadiologi.TITLE

            btnSaveNew.Caption = Caption.FormSaveNew
            btnSaveClose.Caption = Caption.FormSaveClose
            btnClose.Caption = Caption.FormClose
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub Form_Closing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        sCode = txtKDBILLING_POLI.Text.Trim.ToUpper
    End Sub
    Private Sub fn_ChangeFormState()
        fn_LoadItemH()
        fn_LoadDoctor()
        fn_LoadItem()
        fn_LoadSatuan()
        fn_LoadPenjamin()

        Select Case oFormMode
            Case FORM_MODE.FORM_MODE_VIEW
                fn_ViewMode(True)
                fn_LoadData(sCategory)
            Case FORM_MODE.FORM_MODE_ADD
                fn_ViewMode(False)
                fn_EmptyMe()
            Case FORM_MODE.FORM_MODE_EDIT
                fn_ViewMode(False)
                fn_LoadData(sCategory)
            Case Else
                fn_ViewMode(True)
        End Select
    End Sub
    Private Sub fn_ViewMode(ByVal Status As Boolean)
        btnSaveNew.Enabled = Not Status
        btnSaveClose.Enabled = Not Status

        deDATE.Properties.ReadOnly = Status
        txtKDPENDAFTARAN.Properties.ReadOnly = True
        grdKDPENJAMIN.Properties.ReadOnly = True
        grvDetail.OptionsBehavior.ReadOnly = Status
    End Sub
    Private Sub fn_EmptyMe()
        txtKDBILLING_POLI.Text = "<--- AUTO --->"
        deDATE.DateTime = Now
        txtKDKUNJUNGAN_POLI_RUANGAN.ResetText()
        txtKDPENDAFTARAN.ResetText()
        grdKDPENJAMIN.ResetText()
    End Sub
    Private Sub fn_LoadData(ByVal Parameter As Integer)
        If Parameter = 4 Then
            Try
                ' ***** HEADER *****
                Dim ds = oBillingPoli.GetData(sNoId)

                With ds
                    txtKDBILLING_POLI.Text = .KDBILLING_POLI
                    deDATE.DateTime = .DATE
                    txtKDPENDAFTARAN.Text = .KDPENDAFTARAN
                    grdKDPENJAMIN.Text = .KDPENJAMIN
                    txtKDKUNJUNGAN_POLI_RUANGAN.Text = .KDKUNJUNGAN_POLI
                    txtGRANDTOTAL.Text = .GRANDTOTAL
                    bindingSource.DataSource = oBillingPoli.GetDataDetail.Where(Function(x) x.KDBILLING_POLI = sNoId).OrderBy(Function(x) x.SEQ).ToList()
                    grdDetail.DataSource = bindingSource
                End With
            Catch oErr As Exception
                MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            End Try
        ElseIf Parameter = 5 Then
            Try
                ' ***** HEADER *****
                Dim ds = oBillingRuangan.GetData(sNoId)

                With ds
                    txtKDBILLING_POLI.Text = .KDBILLING_RUANGAN
                    deDATE.DateTime = .DATE
                    txtKDPENDAFTARAN.Text = .KDPENDAFTARAN
                    grdKDPENJAMIN.Text = .KDPENJAMIN
                    txtKDKUNJUNGAN_POLI_RUANGAN.Text = .KDKUNJUNGAN_RUANGAN
                    txtGRANDTOTAL.Text = .GRANDTOTAL
                    bindingSource.DataSource = oBillingRuangan.GetDataDetail.Where(Function(x) x.KDBILLING_RUANGAN = sNoId).OrderBy(Function(x) x.SEQ).ToList()
                    grdDetail.DataSource = bindingSource
                End With
            Catch oErr As Exception
                MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            End Try
        End If
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
            If txtKDKUNJUNGAN_POLI_RUANGAN.Text = String.Empty Then
                txtKDKUNJUNGAN_POLI_RUANGAN.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                txtKDKUNJUNGAN_POLI_RUANGAN.ErrorText = Statement.ErrorRequired

                txtKDKUNJUNGAN_POLI_RUANGAN.Focus()
                fn_Validate = False
                Exit Function
            End If

            If sValidasiLab = False Then
                MsgBox("Kategori Radiologi Salah", MsgBoxStyle.Exclamation, Me.Text)
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
    Private Function fn_Save(ByVal Parameter As Integer) As Boolean
        If Parameter = 4 Then
            Try
                ' ***** HEADER *****
                Dim ds = oBillingPoli.GetStructureHeader
                With ds
                    Try
                        .DATECREATED = oBillingPoli.GetData(sNoId).DATECREATED
                    Catch oErr As Exception
                        .DATECREATED = Now
                    End Try
                    .DATEUPDATED = Now
                    .DATE = deDATE.DateTime
                    .KDPENDAFTARAN = txtKDPENDAFTARAN.Text
                    .KDPENJAMIN = grdKDPENJAMIN.EditValue
                    .CATEGORY = Parameter
                    .KDBILLING_POLI = sNoId
                    .KDKUNJUNGAN_POLI = txtKDKUNJUNGAN_POLI_RUANGAN.Text
                    .SUBTOTAL = CDec(0)
                    .DISCOUNT = CDec(0)
                    .TAX = CDec(0)
                    .GRANDTOTAL = CDec(txtGRANDTOTAL.Text)
                    Try
                        .PAYAMOUNT = oBillingPoli.GetData(sNoId).PAYAMOUNT
                    Catch ex As Exception
                        .PAYAMOUNT = 0
                    End Try
                    .KDUSER = sUserID
                    .MEMO = txtMEMO.Text
                End With

                ' ***** DETIL *****
                Dim arrDetail = oBillingPoli.GetStructureDetailList
                For i As Integer = 0 To grvDetail.RowCount - 2
                    Dim dsDetail = oBillingPoli.GetStructureDetail
                    With dsDetail
                        .KDBILLING_POLI = ds.KDBILLING_POLI
                        .KDDOCTOR = grvDetail.GetRowCellValue(i, colKDDOCTOR)
                        .KDDEPARTMENT = sKD_DEPARTMENT_RUANGAN
                        .KDITEM = grvDetail.GetRowCellValue(i, colKDITEM)
                        .KDUOM = grvDetail.GetRowCellValue(i, colKDUOM)
                        .SEQ = i
                        .QTY = grvDetail.GetRowCellValue(i, colQTY)
                        .PRICE = grvDetail.GetRowCellValue(i, colPRICE)
                        .GRANDTOTAL = grvDetail.GetRowCellValue(i, colGRANDTOTAL)
                        .REMARKS = grvDetail.GetRowCellValue(i, colREMARKS)
                    End With
                    arrDetail.Add(dsDetail)
                Next

                If oFormMode = FORM_MODE.FORM_MODE_ADD Then
                    Try
                        fn_Save = oBillingPoli.InsertData(ds, arrDetail)
                    Catch oErr As Exception
                        MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
                    End Try
                ElseIf oFormMode = FORM_MODE.FORM_MODE_EDIT Then
                    Try
                        fn_Save = oBillingPoli.UpdateData(ds, arrDetail)
                    Catch oErr As Exception
                        MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
                    End Try
                End If
            Catch oErr As Exception
                MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
                fn_Save = False
            End Try
        ElseIf Parameter = 5 Then
            Try
                ' ***** HEADER *****
                Dim ds = oBillingRuangan.GetStructureHeader
                With ds
                    Try
                        .DATECREATED = oBillingRuangan.GetData(sNoId).DATECREATED
                    Catch oErr As Exception
                        .DATECREATED = Now
                    End Try
                    .DATEUPDATED = Now
                    .DATE = deDATE.DateTime
                    .KDPENDAFTARAN = txtKDPENDAFTARAN.Text
                    .KDPENJAMIN = grdKDPENJAMIN.EditValue
                    .CATEGORY = Parameter
                    .KDBILLING_RUANGAN = sNoId
                    .KDKUNJUNGAN_RUANGAN = txtKDKUNJUNGAN_POLI_RUANGAN.Text
                    .SUBTOTAL = CDec(0)
                    .DISCOUNT = CDec(0)
                    .TAX = CDec(0)
                    .GRANDTOTAL = CDec(txtGRANDTOTAL.Text)
                    Try
                        .PAYAMOUNT = oBillingRuangan.GetData(sNoId).PAYAMOUNT
                    Catch ex As Exception
                        .PAYAMOUNT = 0
                    End Try
                    .KDUSER = sUserID
                    .MEMO = txtMEMO.Text
                End With

                ' ***** DETIL *****
                Dim arrDetail = oBillingRuangan.GetStructureDetailList
                For i As Integer = 0 To grvDetail.RowCount - 2
                    Dim dsDetail = oBillingRuangan.GetStructureDetail
                    With dsDetail
                        .KDBILLING_RUANGAN = ds.KDBILLING_RUANGAN
                        .KDDOCTOR = grvDetail.GetRowCellValue(i, colKDDOCTOR)
                        .KDRUANGRAWAT = sKD_DEPARTMENT_RUANGAN
                        .KDITEM = grvDetail.GetRowCellValue(i, colKDITEM)
                        .KDUOM = grvDetail.GetRowCellValue(i, colKDUOM)
                        .SEQ = i
                        .QTY = grvDetail.GetRowCellValue(i, colQTY)
                        .PRICE = grvDetail.GetRowCellValue(i, colPRICE)
                        .GRANDTOTAL = grvDetail.GetRowCellValue(i, colGRANDTOTAL)
                        .REMARKS = grvDetail.GetRowCellValue(i, colREMARKS)
                    End With
                    arrDetail.Add(dsDetail)
                Next

                If oFormMode = FORM_MODE.FORM_MODE_ADD Then
                    Try
                        fn_Save = oBillingRuangan.InsertData(ds, arrDetail)
                    Catch oErr As Exception
                        MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
                    End Try
                ElseIf oFormMode = FORM_MODE.FORM_MODE_EDIT Then
                    Try
                        fn_Save = oBillingRuangan.UpdateData(ds, arrDetail)
                    Catch oErr As Exception
                        MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
                    End Try
                End If
            Catch oErr As Exception
                MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
                fn_Save = False
            End Try
        End If
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
    Private Sub frmBillingRadiologi_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
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
        If fn_Save(sCategory) = False Then
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
        If fn_Save(sCategory) = False Then
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
        grvDetail.SetFocusedRowCellValue(colKDDOCTOR, sKDDOCTOR)
        grvDetail.UpdateCurrentRow()
    End Sub
    Private Sub fn_LoadItemH()
        Dim oItem As New Reference.clsItem_L1
        Try
            Dim ds = From x In oItem.GetData.Where(Function(x) x.ISACTIVE = True And x.MEMO = "RADIOLOGI")
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
            '
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
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadItem()
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
            SQL &= "WHERE A.ISACTIVE = 1 "
            SQL &= "ORDER BY A.NMITEM2 "
            '
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

                Dim dsCustomerRJList = From x In oPendaftaran_KunjunganPoli.GetDataByRekamMedis(sParameter)
                                       Select Kode = x.KDKUNJUNGAN_POLI, Tujuan = x.M_DEPARTMENT.NAME_DISPLAY, Penjamin = x.M_PENJAMIN.MEMO, NoSEP = x.S_PENDAFTARAN_H.NOMORSEP, NoKartuBPJS = x.S_PENDAFTARAN_H.KARTUBPJS, Pasien = x.S_PENDAFTARAN_H.M_CUSTOMER.NAME_DISPLAY, TanggalLahir = x.S_PENDAFTARAN_H.M_CUSTOMER.TANGGALLAHIR.ToString("dd-MM-yyyy")

                Dim dsCustomerRIList = From x In oPendaftaran_KunjunganRuangan.GetDataByRekamMedis(sParameter)
                                       Select Kode = x.KDKUNJUNGAN_RUANGAN, Tujuan = x.M_RUANGRAWAT.NAME_DISPLAY, Penjamin = x.M_PENJAMIN.MEMO, NoSEP = x.S_PENDAFTARAN_H.NOMORSEP, NoKartuBPJS = x.S_PENDAFTARAN_H.KARTUBPJS, Pasien = x.S_PENDAFTARAN_H.M_CUSTOMER.NAME_DISPLAY, TanggalLahir = x.S_PENDAFTARAN_H.M_CUSTOMER.TANGGALLAHIR.ToString("dd-MM-yyyy")

                Dim dsCustomerList = dsCustomerRJList.Union(dsCustomerRIList)

                grdCARIKDREGAWAL.Properties.DataSource = dsCustomerList.ToList()
                grdCARIKDREGAWAL.Properties.ValueMember = "Kode"
                grdCARIKDREGAWAL.Properties.DisplayMember = "Pasien"
            Else
                Dim oPendaftaran_KunjunganPoli As New Admission.clsPendaftaran_KunjunganPoli
                Dim oPendaftaran_KunjunganRuangan As New Admission.clsPendaftaran_KunjunganRuangan

                Dim dsCustomerRJList = From x In oPendaftaran_KunjunganPoli.GetDataByRekamNama(sParameter)
                                       Select Kode = x.KDKUNJUNGAN_POLI, Tujuan = x.M_DEPARTMENT.NAME_DISPLAY, Penjamin = x.M_PENJAMIN.MEMO, NoSEP = x.S_PENDAFTARAN_H.NOMORSEP, NoKartuBPJS = x.S_PENDAFTARAN_H.KARTUBPJS, Pasien = x.S_PENDAFTARAN_H.M_CUSTOMER.NAME_DISPLAY, TanggalLahir = x.S_PENDAFTARAN_H.M_CUSTOMER.TANGGALLAHIR.ToString("dd-MM-yyyy")

                Dim dsCustomerRIList = From x In oPendaftaran_KunjunganRuangan.GetDataByRekamNama(sParameter)
                                       Select Kode = x.KDKUNJUNGAN_RUANGAN, Tujuan = x.M_RUANGRAWAT.NAME_DISPLAY, Penjamin = x.M_PENJAMIN.MEMO, NoSEP = x.S_PENDAFTARAN_H.NOMORSEP, NoKartuBPJS = x.S_PENDAFTARAN_H.KARTUBPJS, Pasien = x.S_PENDAFTARAN_H.M_CUSTOMER.NAME_DISPLAY, TanggalLahir = x.S_PENDAFTARAN_H.M_CUSTOMER.TANGGALLAHIR.ToString("dd-MM-yyyy")

                Dim dsCustomerList = dsCustomerRJList.Union(dsCustomerRIList)

                grdCARIKDREGAWAL.Properties.DataSource = dsCustomerList.ToList()
                grdCARIKDREGAWAL.Properties.ValueMember = "Kode"
                grdCARIKDREGAWAL.Properties.DisplayMember = "Pasien"
            End If

            grdCARIKDREGAWAL.ShowPopup()
            grvCARIKDPENDAFTARAN_AWAL.BestFitColumns()
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub grdCARIKDREGAWAL_KeyPress(sender As Object, e As KeyPressEventArgs) Handles grdCARIKDREGAWAL.KeyPress
        Dim oPendafataran_KunjunganPoli As New Admission.clsPendaftaran_KunjunganPoli
        Dim oPendaftaran_KunjunganRuangan As New Admission.clsPendaftaran_KunjunganRuangan

        Dim dsRJ = oPendafataran_KunjunganPoli.GetData(grdCARIKDREGAWAL.EditValue)

        If dsRJ IsNot Nothing Then
            txtKDKUNJUNGAN_POLI_RUANGAN.Text = dsRJ.KDKUNJUNGAN_POLI
            sKDDOCTOR = dsRJ.KDDOCTOR
            sKD_DEPARTMENT_RUANGAN = dsRJ.KDDEPARTMENT
            txtKDPENDAFTARAN.Text = dsRJ.KDPENDAFTARAN
            grdKDPENJAMIN.Text = dsRJ.KDPENJAMIN
            sValidasiLab = True
            sCategory = 4
        Else
            Dim dsRI = oPendaftaran_KunjunganRuangan.GetData(grdCARIKDREGAWAL.EditValue)

            If dsRI IsNot Nothing Then
                txtKDKUNJUNGAN_POLI_RUANGAN.Text = dsRI.KDKUNJUNGAN_RUANGAN
                sKDDOCTOR = dsRI.KDDOCTOR
                sKD_DEPARTMENT_RUANGAN = dsRI.KDRUANGRAWAT
                txtKDPENDAFTARAN.Text = dsRI.KDPENDAFTARAN
                grdKDPENJAMIN.Text = dsRI.KDPENJAMIN
                sValidasiLab = True
                sCategory = 5
            Else
                sValidasiLab = False
            End If
        End If
    End Sub
#End Region
End Class