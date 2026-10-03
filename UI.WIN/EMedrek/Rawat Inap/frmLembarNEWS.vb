Imports DataAccess
Imports UI.WIN.MAIN.My.Resources
Imports System.Linq
Imports System.Data.SqlClient
Imports System.Xml

Public Class frmLembarNEWS
#Region "Declaration"
    Private oFormMode As FORM_MODE = FORM_MODE.FORM_MODE_VIEW
    Private isLoad As Boolean = False
    Private oS_DIGITAL_LEMBARNEWS As New Digital.clsS_DIGITAL_LEMBARNEWS
    Private down As Boolean = False
    Private sPERAWAT As String
    Private sKode As String = String.Empty

#End Region
#Region "Function"
    Public Sub LoadMe(ByVal FormMode As Integer, ByVal KDREG As String, ByVal KDCUSTOMER As String, ByVal NAMAPASIEN As String, ByVal JENISKELAMIN As String, ByVal KDUSER_PERAWAT As String, Optional ByVal Kode As String = "")
        oFormMode = FormMode

        txtNoRM.Text = KDCUSTOMER
        txtNamaPasien.Text = NAMAPASIEN
        txtJK.Text = JENISKELAMIN
        txCODE.Text = KDREG
        sKode = Kode
        sPERAWAT = KDUSER_PERAWAT
    End Sub
    Private Sub fn_LoadDigital()

        For iLoop As Integer = 0 To 33
            grvNEWS.AddNewRow()
            grvNEWS.SetRowCellValue(iLoop, SEQ, iLoop + 1)
            grvNEWS.SetRowCellValue(iLoop, GridColumn1, "")
        Next

        'ews
        grvNEWS.SetRowCellValue(0, GridColumn1, "TINGKAT KESADARAN")
        grvNEWS.SetRowCellValue(4, GridColumn1, "TEKANAN DARAH (SISTOLIK)")
        grvNEWS.SetRowCellValue(11, GridColumn1, "NADI")
        grvNEWS.SetRowCellValue(17, GridColumn1, "PERNAPASAN")
        grvNEWS.SetRowCellValue(22, GridColumn1, "SUHU")
        grvNEWS.SetRowCellValue(27, GridColumn1, "SATURASI O2")
        grvNEWS.SetRowCellValue(31, GridColumn1, "KEBUTUHAN PENGGUNAAN O2")

        'GridColumn2
        grvNEWS.SetRowCellValue(0, GridColumn2, "Sopor")
        grvNEWS.SetRowCellValue(1, GridColumn2, "Somnolen")
        grvNEWS.SetRowCellValue(2, GridColumn2, "Compos Mentis")
        grvNEWS.SetRowCellValue(3, GridColumn2, "Coma")

        grvNEWS.SetRowCellValue(4, GridColumn2, ">220")
        grvNEWS.SetRowCellValue(5, GridColumn2, "200 - 220")
        grvNEWS.SetRowCellValue(6, GridColumn2, "160 - 199")
        grvNEWS.SetRowCellValue(7, GridColumn2, "101 - 159")
        grvNEWS.SetRowCellValue(8, GridColumn2, "81 - 100")
        grvNEWS.SetRowCellValue(9, GridColumn2, "71 - 80")
        grvNEWS.SetRowCellValue(10, GridColumn2, "<71")

        grvNEWS.SetRowCellValue(11, GridColumn2, ">131")
        grvNEWS.SetRowCellValue(12, GridColumn2, "111 - 130")
        grvNEWS.SetRowCellValue(13, GridColumn2, "91 - 110")
        grvNEWS.SetRowCellValue(14, GridColumn2, "51 - 90")
        grvNEWS.SetRowCellValue(15, GridColumn2, "41 - 50")
        grvNEWS.SetRowCellValue(16, GridColumn2, "<41")

        grvNEWS.SetRowCellValue(17, GridColumn2, ">24")
        grvNEWS.SetRowCellValue(18, GridColumn2, "21 - 24")
        grvNEWS.SetRowCellValue(19, GridColumn2, "12 - 20")
        grvNEWS.SetRowCellValue(20, GridColumn2, "9 - 11")
        grvNEWS.SetRowCellValue(21, GridColumn2, "<9")

        grvNEWS.SetRowCellValue(22, GridColumn2, ">39,0")
        grvNEWS.SetRowCellValue(23, GridColumn2, "38,1 - 39,0")
        grvNEWS.SetRowCellValue(24, GridColumn2, "36,1 - 38,0")
        grvNEWS.SetRowCellValue(25, GridColumn2, "35,1 - 36,0")
        grvNEWS.SetRowCellValue(26, GridColumn2, "<35,1")

        grvNEWS.SetRowCellValue(27, GridColumn2, ">95")
        grvNEWS.SetRowCellValue(28, GridColumn2, "94 - 95")
        grvNEWS.SetRowCellValue(29, GridColumn2, "92 - 93")
        grvNEWS.SetRowCellValue(30, GridColumn2, "<92")

        grvNEWS.SetRowCellValue(31, GridColumn2, "Pakai")
        grvNEWS.SetRowCellValue(32, GridColumn2, "Tidak Pakai")

        'NILAI
        grvNEWS.SetRowCellValue(0, NILAI, 2)
        grvNEWS.SetRowCellValue(1, NILAI, 1)
        grvNEWS.SetRowCellValue(2, NILAI, 0)
        grvNEWS.SetRowCellValue(3, NILAI, 3)

        grvNEWS.SetRowCellValue(4, NILAI, 3)
        grvNEWS.SetRowCellValue(5, NILAI, 2)
        grvNEWS.SetRowCellValue(6, NILAI, 1)
        grvNEWS.SetRowCellValue(7, NILAI, 0)
        grvNEWS.SetRowCellValue(8, NILAI, 1)
        grvNEWS.SetRowCellValue(9, NILAI, 2)
        grvNEWS.SetRowCellValue(10, NILAI, 3)

        grvNEWS.SetRowCellValue(11, NILAI, 3)
        grvNEWS.SetRowCellValue(12, NILAI, 2)
        grvNEWS.SetRowCellValue(13, NILAI, 1)
        grvNEWS.SetRowCellValue(14, NILAI, 0)
        grvNEWS.SetRowCellValue(15, NILAI, 1)
        grvNEWS.SetRowCellValue(16, NILAI, 2)

        grvNEWS.SetRowCellValue(17, NILAI, 3)
        grvNEWS.SetRowCellValue(18, NILAI, 2)
        grvNEWS.SetRowCellValue(19, NILAI, 0)
        grvNEWS.SetRowCellValue(20, NILAI, 1)
        grvNEWS.SetRowCellValue(21, NILAI, 3)

        grvNEWS.SetRowCellValue(22, NILAI, 2)
        grvNEWS.SetRowCellValue(23, NILAI, 1)
        grvNEWS.SetRowCellValue(24, NILAI, 0)
        grvNEWS.SetRowCellValue(25, NILAI, 1)
        grvNEWS.SetRowCellValue(26, NILAI, 3)

        grvNEWS.SetRowCellValue(27, NILAI, 0)
        grvNEWS.SetRowCellValue(28, NILAI, 1)
        grvNEWS.SetRowCellValue(29, NILAI, 2)
        grvNEWS.SetRowCellValue(30, NILAI, 3)

        grvNEWS.SetRowCellValue(31, NILAI, 3)
        grvNEWS.SetRowCellValue(32, NILAI, 0)

        grvNEWS.Columns("EWS").AppearanceCell.BackColor = Color.White
        grvNEWS.Columns("KETERANGAN").AppearanceCell.BackColor = Color.White

        grvNEWS.DeleteSelectedRows()

        grvNEWS.Columns("SEQ").Visible = False
        grvNEWS.Columns("NILAI").Visible = False
        grvNEWS.Columns("KDKUNJUNGAN").Visible = False

        grvNEWS.Columns(0).Width = layoutlblEWS.Width
        grvNEWS.Columns(1).Width = layoutlblTANGGAL.Width
        grvNEWS.Columns(2).Width = layoutTGL1.Width
        grvNEWS.Columns(3).Width = layoutTGL2.Width
        grvNEWS.Columns(4).Width = layoutTGL3.Width
        grvNEWS.Columns(5).Width = layoutTGL4.Width
        grvNEWS.Columns(6).Width = layoutTGL5.Width
        grvNEWS.Columns(7).Width = layoutTGL6.Width
        grvNEWS.Columns(8).Width = layoutTGL7.Width
        grvNEWS.Columns(9).Width = layoutTGL8.Width
        grvNEWS.Columns(10).Width = layoutTGL9.Width
        grvNEWS.Columns(11).Width = layoutTGL10.Width
        grvNEWS.Columns(12).Width = layoutTGL11.Width
        grvNEWS.Columns(13).Width = layoutTGL12.Width
        grvNEWS.Columns(14).Width = layoutTGL13.Width
        grvNEWS.Columns(15).Width = layoutTGL14.Width
        grvNEWS.Columns(16).Width = layoutTGL15.Width
        grvNEWS.Columns(17).Width = layoutTGL16.Width
        'grvNEWS.Columns(18).Width = layoutTGL17.Width

        layoutTotalSkors.Width = layoutlblEWS.Width + layoutlblTANGGAL.Width

    End Sub
    Private Sub grvNEWS_RowStyle(sender As System.Object, e As DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs) Handles grvNEWS.RowStyle
        If grvNEWS.IsFilterRow(e.RowHandle) Then Exit Sub
        Dim sKet = grvNEWS.GetRowCellValue(e.RowHandle, "KETERANGAN")
        If sKet = "Sopor" Or sKet = "200 - 220" Or sKet = "71 - 80" Or sKet = "111 - 130" Or sKet = "<41" Or sKet = "21 - 24" Or sKet = ">39,0" Or sKet = "92 - 93" Then
            e.Appearance.BackColor = Color.Orange
        ElseIf sKet = "Somnolen" Or sKet = "160 - 199" Or sKet = "81 - 100" Or sKet = "91 - 110" Or sKet = "41 - 50" Or sKet = "9 - 11" Or sKet = "38,1 - 39,0" Or sKet = "35,1 - 36,0" Or sKet = "94 - 95" Then
            e.Appearance.BackColor = Color.Yellow
        ElseIf sKet = "Compos Mentis" Or sKet = "101 - 159" Or sKet = "51 - 90" Or sKet = "12 - 20" Or sKet = "36,1 - 38,0" Or sKet = ">95" Or sKet = "Tidak Pakai" Then
            e.Appearance.BackColor = Color.LimeGreen
        Else
            e.Appearance.BackColor = Color.Red
        End If
    End Sub
    Private Sub Form_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        fn_LoadDigital()
        fn_ChangeFormState()
        isLoad = True
    End Sub
    Private Sub Form_Closing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        Dispose()
        sCode = txCODE.Text.Trim.ToUpper
    End Sub
    Private Overloads Sub Dispose()
        MyBase.Dispose()
        Me.Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
    Private Sub fn_ChangeFormState()
        fn_NOIDUSER()

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

        deTanggal1.Properties.ReadOnly = Status
        deTanggal2.Properties.ReadOnly = Status
        deTanggal3.Properties.ReadOnly = Status
        deTanggal4.Properties.ReadOnly = Status
        deTanggal5.Properties.ReadOnly = Status
        deTanggal6.Properties.ReadOnly = Status
        deTanggal7.Properties.ReadOnly = Status
        deTanggal8.Properties.ReadOnly = Status
        deTanggal9.Properties.ReadOnly = Status
        deTanggal10.Properties.ReadOnly = Status
        deTanggal11.Properties.ReadOnly = Status
        deTanggal12.Properties.ReadOnly = Status
        deTanggal13.Properties.ReadOnly = Status
        deTanggal14.Properties.ReadOnly = Status
        deTanggal15.Properties.ReadOnly = Status
        deTanggal16.Properties.ReadOnly = Status
        deTanggal17.Properties.ReadOnly = Status

        txtJam1.ReadOnly = Status
        txtJam2.ReadOnly = Status
        txtJam3.ReadOnly = Status
        txtJam4.ReadOnly = Status
        txtJam5.ReadOnly = Status
        txtJam6.ReadOnly = Status
        txtJam7.ReadOnly = Status
        txtJam8.ReadOnly = Status
        txtJam9.ReadOnly = Status
        txtJam10.ReadOnly = Status
        txtjam11.ReadOnly = Status
        txtJam12.ReadOnly = Status
        txtJam13.ReadOnly = Status
        txtJam14.ReadOnly = Status
        txtJam15.ReadOnly = Status
        txtJam16.ReadOnly = Status
        txtJam17.ReadOnly = Status

        txtTOTALSKOR1.ReadOnly = Status
        txtTOTALSKOR2.ReadOnly = Status
        txtTOTALSKOR3.ReadOnly = Status
        txtTOTALSKOR4.ReadOnly = Status
        txtTOTALSKOR5.ReadOnly = Status
        txtTOTALSKOR6.ReadOnly = Status
        txtTOTALSKOR7.ReadOnly = Status
        txtTOTALSKOR8.ReadOnly = Status
        txtTOTALSKOR9.ReadOnly = Status
        txtTOTALSKOR10.ReadOnly = Status
        txtTOTALSKOR11.ReadOnly = Status
        txtTOTALSKOR12.ReadOnly = Status
        txtTOTALSKOR13.ReadOnly = Status
        txtTOTALSKOR14.ReadOnly = Status
        txtTOTALSKOR15.ReadOnly = Status
        txtTOTALSKOR16.ReadOnly = Status
        txtTOTALSKOR17.ReadOnly = Status

        grdKDUSER_1.ReadOnly = Status
        grdKDUSER_2.ReadOnly = Status
        grdKDUSER_3.ReadOnly = Status
        grdKDUSER_4.ReadOnly = Status
        grdKDUSER_5.ReadOnly = Status
        grdKDUSER_6.ReadOnly = Status
        grdKDUSER_7.ReadOnly = Status
        grdKDUSER_8.ReadOnly = Status
        grdKDUSER_9.ReadOnly = Status
        grdKDUSER_10.ReadOnly = Status
        grdKDUSER_11.ReadOnly = Status
        grdKDUSER_12.ReadOnly = Status
        grdKDUSER_13.ReadOnly = Status
        grdKDUSER_14.ReadOnly = Status
        grdKDUSER_15.ReadOnly = Status
        grdKDUSER_16.ReadOnly = Status
        grdKDUSER_17.ReadOnly = Status

        txtNAMAPETUGAS1.ReadOnly = True
        txtNAMAPETUGAS2.ReadOnly = True
        txtNAMAPETUGAS3.ReadOnly = True
        txtNAMAPETUGAS4.ReadOnly = True
        txtNAMAPETUGAS5.ReadOnly = True
        txtNAMAPETUGAS6.ReadOnly = True
        txtNAMAPETUGAS7.ReadOnly = True
        txtNAMAPETUGAS8.ReadOnly = True
        txtNAMAPETUGAS9.ReadOnly = True
        txtNAMAPETUGAS10.ReadOnly = True
        txtNAMAPETUGAS11.ReadOnly = True
        txtNAMAPETUGAS12.ReadOnly = True
        txtNAMAPETUGAS13.ReadOnly = True
        txtNAMAPETUGAS14.ReadOnly = True
        txtNAMAPETUGAS15.ReadOnly = True
        txtNAMAPETUGAS16.ReadOnly = True
        txtNAMAPETUGAS17.ReadOnly = True
    End Sub
    Private Sub fn_EmptyMe()
        deTanggal1.ResetText()
        deTanggal2.ResetText()
        deTanggal3.ResetText()
        deTanggal4.ResetText()
        deTanggal5.ResetText()
        deTanggal6.ResetText()
        deTanggal7.ResetText()
        deTanggal8.ResetText()
        deTanggal9.ResetText()
        deTanggal10.ResetText()
        deTanggal11.ResetText()
        deTanggal12.ResetText()
        deTanggal13.ResetText()
        deTanggal14.ResetText()
        deTanggal15.ResetText()
        deTanggal16.ResetText()
        deTanggal17.ResetText()

        txtJam1.ResetText()
        txtJam2.ResetText()
        txtJam3.ResetText()
        txtJam4.ResetText()
        txtJam5.ResetText()
        txtJam6.ResetText()
        txtJam7.ResetText()
        txtJam8.ResetText()
        txtJam9.ResetText()
        txtJam10.ResetText()
        txtjam11.ResetText()
        txtJam12.ResetText()
        txtJam13.ResetText()
        txtJam14.ResetText()
        txtJam15.ResetText()
        txtJam16.ResetText()
        txtJam17.ResetText()

        txtTOTALSKOR1.ResetText()
        txtTOTALSKOR2.ResetText()
        txtTOTALSKOR3.ResetText()
        txtTOTALSKOR4.ResetText()
        txtTOTALSKOR5.ResetText()
        txtTOTALSKOR6.ResetText()
        txtTOTALSKOR7.ResetText()
        txtTOTALSKOR8.ResetText()
        txtTOTALSKOR9.ResetText()
        txtTOTALSKOR10.ResetText()
        txtTOTALSKOR11.ResetText()
        txtTOTALSKOR12.ResetText()
        txtTOTALSKOR13.ResetText()
        txtTOTALSKOR14.ResetText()
        txtTOTALSKOR15.ResetText()
        txtTOTALSKOR16.ResetText()
        txtTOTALSKOR17.ResetText()

        grdKDUSER_1.ResetText()
        grdKDUSER_2.ResetText()
        grdKDUSER_3.ResetText()
        grdKDUSER_4.ResetText()
        grdKDUSER_5.ResetText()
        grdKDUSER_6.ResetText()
        grdKDUSER_7.ResetText()
        grdKDUSER_8.ResetText()
        grdKDUSER_9.ResetText()
        grdKDUSER_10.ResetText()
        grdKDUSER_11.ResetText()
        grdKDUSER_12.ResetText()
        grdKDUSER_13.ResetText()
        grdKDUSER_14.ResetText()
        grdKDUSER_15.ResetText()
        grdKDUSER_16.ResetText()
        grdKDUSER_17.ResetText()

        txtNAMAPETUGAS1.ResetText()
        txtNAMAPETUGAS2.ResetText()
        txtNAMAPETUGAS3.ResetText()
        txtNAMAPETUGAS4.ResetText()
        txtNAMAPETUGAS5.ResetText()
        txtNAMAPETUGAS6.ResetText()
        txtNAMAPETUGAS7.ResetText()
        txtNAMAPETUGAS8.ResetText()
        txtNAMAPETUGAS9.ResetText()
        txtNAMAPETUGAS10.ResetText()
        txtNAMAPETUGAS11.ResetText()
        txtNAMAPETUGAS12.ResetText()
        txtNAMAPETUGAS13.ResetText()
        txtNAMAPETUGAS14.ResetText()
        txtNAMAPETUGAS15.ResetText()
        txtNAMAPETUGAS16.ResetText()
        txtNAMAPETUGAS17.ResetText()

        deDATE.DateTime = Now
    End Sub
    Private Sub fn_LoadData()
        Try
            ' ***** HEADER *****
            Dim ds = oS_DIGITAL_LEMBARNEWS.GetData(txCODE.Text, sKode)

            With ds
                deTanggal1.Text = .DATE1
                deTanggal2.Text = .DATE2
                deTanggal3.Text = .DATE3
                deTanggal4.Text = .DATE4
                deTanggal5.Text = .DATE5
                deTanggal6.Text = .DATE6
                deTanggal7.Text = .DATE7
                deTanggal8.Text = .DATE8
                deTanggal9.Text = .DATE9
                deTanggal10.Text = .DATE10
                deTanggal11.Text = .DATE11
                deTanggal12.Text = .DATE12
                deTanggal13.Text = .DATE13
                deTanggal14.Text = .DATE14
                deTanggal15.Text = .DATE15
                deTanggal16.Text = .DATE16
                deTanggal17.Text = .DATE17

                txtJam1.Text = .JAM1
                txtJam2.Text = .JAM2
                txtJam3.Text = .JAM3
                txtJam4.Text = .JAM4
                txtJam5.Text = .JAM5
                txtJam6.Text = .JAM6
                txtJam7.Text = .JAM7
                txtJam8.Text = .JAM8
                txtJam9.Text = .JAM9
                txtJam10.Text = .JAM10
                txtjam11.Text = .JAM11
                txtJam12.Text = .JAM12
                txtJam13.Text = .JAM13
                txtJam14.Text = .JAM14
                txtJam15.Text = .JAM15
                txtJam16.Text = .JAM16
                txtJam17.Text = .JAM17

                txtTOTALSKOR1.Text = .SKOR1
                txtTOTALSKOR2.Text = .SKOR2
                txtTOTALSKOR3.Text = .SKOR3
                txtTOTALSKOR4.Text = .SKOR4
                txtTOTALSKOR5.Text = .SKOR5
                txtTOTALSKOR6.Text = .SKOR6
                txtTOTALSKOR7.Text = .SKOR7
                txtTOTALSKOR8.Text = .SKOR8
                txtTOTALSKOR9.Text = .SKOR9
                txtTOTALSKOR10.Text = .SKOR10
                txtTOTALSKOR11.Text = .SKOR11
                txtTOTALSKOR12.Text = .SKOR12
                txtTOTALSKOR13.Text = .SKOR13
                txtTOTALSKOR14.Text = .SKOR14
                txtTOTALSKOR15.Text = .SKOR15
                txtTOTALSKOR16.Text = .SKOR16
                txtTOTALSKOR17.Text = .SKOR17

                grdKDUSER_1.Text = .PARAF1
                grdKDUSER_2.Text = .PARAF2
                grdKDUSER_3.Text = .PARAF3
                grdKDUSER_4.Text = .PARAF4
                grdKDUSER_5.Text = .PARAF5
                grdKDUSER_6.Text = .PARAF6
                grdKDUSER_7.Text = .PARAF7
                grdKDUSER_8.Text = .PARAF8
                grdKDUSER_9.Text = .PARAF9
                grdKDUSER_10.Text = .PARAF10
                grdKDUSER_11.Text = .PARAF11
                grdKDUSER_12.Text = .PARAF12
                grdKDUSER_13.Text = .PARAF13
                grdKDUSER_14.Text = .PARAF14
                grdKDUSER_15.Text = .PARAF15
                grdKDUSER_16.Text = .PARAF16
                grdKDUSER_17.Text = .PARAF17

                txtNAMAPETUGAS1.Text = .NAMAPETUGAS1
                txtNAMAPETUGAS2.Text = .NAMAPETUGAS2
                txtNAMAPETUGAS3.Text = .NAMAPETUGAS3
                txtNAMAPETUGAS4.Text = .NAMAPETUGAS4
                txtNAMAPETUGAS5.Text = .NAMAPETUGAS5
                txtNAMAPETUGAS6.Text = .NAMAPETUGAS6
                txtNAMAPETUGAS7.Text = .NAMAPETUGAS7
                txtNAMAPETUGAS8.Text = .NAMAPETUGAS8
                txtNAMAPETUGAS9.Text = .NAMAPETUGAS9
                txtNAMAPETUGAS10.Text = .NAMAPETUGAS10
                txtNAMAPETUGAS11.Text = .NAMAPETUGAS11
                txtNAMAPETUGAS12.Text = .NAMAPETUGAS12
                txtNAMAPETUGAS13.Text = .NAMAPETUGAS13
                txtNAMAPETUGAS14.Text = .NAMAPETUGAS14
                txtNAMAPETUGAS15.Text = .NAMAPETUGAS15
                txtNAMAPETUGAS16.Text = .NAMAPETUGAS16
                txtNAMAPETUGAS17.Text = .NAMAPETUGAS17

                deDATE.DateTime = .DATEUPDATED

                BindingSource1.DataSource = oS_DIGITAL_LEMBARNEWS.GetDataDetail(txCODE.Text, sKode)
                grdNEWS.DataSource = BindingSource1
            End With
        Catch oErr As Exception
            MsgBox("Load List Data : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Function fn_Validate() As Boolean
        Try
            fn_Validate = True
            If txCODE.Text = String.Empty Then
                MsgBox("Dibutuhkan Register", MsgBoxStyle.Exclamation, Me.Text)
                txCODE.Focus()
                fn_Validate = False
                Exit Function
            End If
        Catch oErr As Exception
            MsgBox("Validate Data : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Function
    Private Function fn_Save() As Boolean
        Try
            ' ***** HEADER *****
            Dim ds = oS_DIGITAL_LEMBARNEWS.GetStructureHeader
            With ds
                Try
                    .KDPENDAFTARAN = oS_DIGITAL_LEMBARNEWS.GetData(txCODE.Text, sKode).KDPENDAFTARAN
                Catch ex As Exception
                    .KDPENDAFTARAN = txCODE.Text
                End Try
                Try
                    .DATECREATED = oS_DIGITAL_LEMBARNEWS.GetData(txCODE.Text, sKode).DATECREATED
                Catch ex As Exception
                    .DATECREATED = Now
                End Try
                .DATEUPDATED = deDATE.DateTime
                .DATE1 = deTanggal1.Text
                .DATE2 = deTanggal2.Text
                .DATE3 = deTanggal3.Text
                .DATE4 = deTanggal4.Text
                .DATE5 = deTanggal5.Text
                .DATE6 = deTanggal6.Text
                .DATE7 = deTanggal7.Text
                .DATE8 = deTanggal8.Text
                .DATE9 = deTanggal9.Text
                .DATE10 = deTanggal10.Text
                .DATE11 = deTanggal11.Text
                .DATE12 = deTanggal12.Text
                .DATE13 = deTanggal13.Text
                .DATE14 = deTanggal14.Text
                .DATE15 = deTanggal15.Text
                .DATE16 = deTanggal16.Text
                .DATE17 = deTanggal17.Text

                .JAM1 = txtJam1.Text
                .JAM2 = txtJam2.Text
                .JAM3 = txtJam3.Text
                .JAM4 = txtJam4.Text
                .JAM5 = txtJam5.Text
                .JAM6 = txtJam6.Text
                .JAM7 = txtJam7.Text
                .JAM8 = txtJam8.Text
                .JAM9 = txtJam9.Text
                .JAM10 = txtJam10.Text
                .JAM11 = txtjam11.Text
                .JAM12 = txtJam12.Text
                .JAM13 = txtJam13.Text
                .JAM14 = txtJam14.Text
                .JAM15 = txtJam15.Text
                .JAM16 = txtJam16.Text
                .JAM17 = txtJam17.Text

                .SKOR1 = txtTOTALSKOR1.Text
                .SKOR2 = txtTOTALSKOR2.Text
                .SKOR3 = txtTOTALSKOR3.Text
                .SKOR4 = txtTOTALSKOR4.Text
                .SKOR5 = txtTOTALSKOR5.Text
                .SKOR6 = txtTOTALSKOR6.Text
                .SKOR7 = txtTOTALSKOR7.Text
                .SKOR8 = txtTOTALSKOR8.Text
                .SKOR9 = txtTOTALSKOR9.Text
                .SKOR10 = txtTOTALSKOR10.Text
                .SKOR11 = txtTOTALSKOR11.Text
                .SKOR12 = txtTOTALSKOR12.Text
                .SKOR13 = txtTOTALSKOR13.Text
                .SKOR14 = txtTOTALSKOR14.Text
                .SKOR15 = txtTOTALSKOR15.Text
                .SKOR16 = txtTOTALSKOR16.Text
                .SKOR17 = txtTOTALSKOR17.Text
                .PARAF1 = grdKDUSER_1.EditValue
                .PARAF2 = grdKDUSER_2.EditValue
                .PARAF3 = grdKDUSER_3.EditValue
                .PARAF4 = grdKDUSER_4.EditValue
                .PARAF5 = grdKDUSER_5.EditValue
                .PARAF6 = grdKDUSER_6.EditValue
                .PARAF7 = grdKDUSER_7.EditValue
                .PARAF8 = grdKDUSER_8.EditValue
                .PARAF9 = grdKDUSER_9.EditValue
                .PARAF10 = grdKDUSER_10.EditValue
                .PARAF11 = grdKDUSER_11.EditValue
                .PARAF12 = grdKDUSER_12.EditValue
                .PARAF13 = grdKDUSER_13.EditValue
                .PARAF14 = grdKDUSER_14.EditValue
                .PARAF15 = grdKDUSER_15.EditValue
                .PARAF16 = grdKDUSER_16.EditValue
                .PARAF17 = grdKDUSER_17.EditValue
                .NAMAPETUGAS1 = txtNAMAPETUGAS1.Text
                .NAMAPETUGAS2 = txtNAMAPETUGAS2.Text
                .NAMAPETUGAS3 = txtNAMAPETUGAS3.Text
                .NAMAPETUGAS4 = txtNAMAPETUGAS4.Text
                .NAMAPETUGAS5 = txtNAMAPETUGAS5.Text
                .NAMAPETUGAS6 = txtNAMAPETUGAS6.Text
                .NAMAPETUGAS7 = txtNAMAPETUGAS7.Text
                .NAMAPETUGAS8 = txtNAMAPETUGAS8.Text
                .NAMAPETUGAS9 = txtNAMAPETUGAS9.Text
                .NAMAPETUGAS10 = txtNAMAPETUGAS10.Text
                .NAMAPETUGAS11 = txtNAMAPETUGAS11.Text
                .NAMAPETUGAS12 = txtNAMAPETUGAS12.Text
                .NAMAPETUGAS13 = txtNAMAPETUGAS13.Text
                .NAMAPETUGAS14 = txtNAMAPETUGAS14.Text
                .NAMAPETUGAS15 = txtNAMAPETUGAS15.Text
                .NAMAPETUGAS16 = txtNAMAPETUGAS16.Text
                .NAMAPETUGAS17 = txtNAMAPETUGAS17.Text

                Try
                    .CETAK = oS_DIGITAL_LEMBARNEWS.GetData(txCODE.Text, sKode).CETAK
                Catch ex As Exception
                    .CETAK = 0
                End Try

                Try
                    .KODE = oS_DIGITAL_LEMBARNEWS.GetData(txCODE.Text, sKode).KODE
                Catch ex As Exception
                    .KODE = ""
                End Try

                .KET = ""

                .KDUSER = sPERAWAT
                .KDUSER_SIGNATURE = sUserID
            End With

            ' ***** DETIL *****
            Dim arrDetail = oS_DIGITAL_LEMBARNEWS.GetStructureDetailList ''
            For i As Integer = 0 To grvNEWS.RowCount - 1
                Dim dsDetail = oS_DIGITAL_LEMBARNEWS.GetStructureDetail
                With dsDetail
                    .SEQ = i
                    .KDPENDAFTARAN = ds.KDPENDAFTARAN
                    .EWS = grvNEWS.GetRowCellValue(i, GridColumn1)
                    .KETERANGAN = grvNEWS.GetRowCellValue(i, GridColumn2)
                    .COL1 = grvNEWS.GetRowCellValue(i, GridColumn3)
                    .COL2 = grvNEWS.GetRowCellValue(i, GridColumn4)
                    .COL3 = grvNEWS.GetRowCellValue(i, GridColumn5)
                    .COL4 = grvNEWS.GetRowCellValue(i, GridColumn6)
                    .COL5 = grvNEWS.GetRowCellValue(i, GridColumn7)
                    .COL6 = grvNEWS.GetRowCellValue(i, GridColumn8)
                    .COL7 = grvNEWS.GetRowCellValue(i, GridColumn9)
                    .COL8 = grvNEWS.GetRowCellValue(i, GridColumn10)
                    .COL9 = grvNEWS.GetRowCellValue(i, GridColumn11)
                    .COL10 = grvNEWS.GetRowCellValue(i, GridColumn12)
                    .COL11 = grvNEWS.GetRowCellValue(i, GridColumn13)
                    .COL12 = grvNEWS.GetRowCellValue(i, GridColumn14)
                    .COL13 = grvNEWS.GetRowCellValue(i, GridColumn15)
                    .COL14 = grvNEWS.GetRowCellValue(i, GridColumn16)
                    .COL15 = grvNEWS.GetRowCellValue(i, GridColumn17)
                    .COL16 = grvNEWS.GetRowCellValue(i, GridColumn18)
                    .COL17 = grvNEWS.GetRowCellValue(i, GridColumn19)
                    .NILAI = grvNEWS.GetRowCellValue(i, NILAI)
                    .KODE = ds.KODE
                End With
                arrDetail.Add(dsDetail)
            Next

            If oFormMode = FORM_MODE.FORM_MODE_ADD Then
                Try
                    fn_Save = oS_DIGITAL_LEMBARNEWS.InsertData(ds, arrDetail)
                Catch ex As Exception
                    MsgBox("Simpan Data : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            ElseIf oFormMode = FORM_MODE.FORM_MODE_EDIT Then
                Try
                    fn_Save = oS_DIGITAL_LEMBARNEWS.UpdateData(txCODE.Text,sKode, ds, arrDetail)
                Catch ex As Exception
                    MsgBox("Simpan Data : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            End If
        Catch oErr As Exception
            MsgBox("Simpan Data : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            fn_Save = False
        End Try
    End Function
#End Region
#Region "Command Button"
    Private Sub frmItem_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        Select Case e.KeyCode
            Case Keys.F12
                btnClose_Click()
            Case Keys.F3
                If btnSaveClose.Enabled = True Then
                    btnSaveClose_Click()
                End If
        End Select
    End Sub

    Private Sub btnSaveClose_Click() Handles btnSaveClose.ItemClick
        If fn_Validate() = False Then Exit Sub
        If MsgBox("Save " & txCODE.Text.Trim.ToUpper & "?", MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, Me.Text) = MsgBoxResult.No Then Exit Sub
        If fn_Save() = False Then
            MsgBox("Save gagal! Tolong kontak admin anda!", MsgBoxStyle.Exclamation, Me.Text)
        Else
            MsgBox("Save " & txCODE.Text.Trim.ToUpper & " success!", MsgBoxStyle.Information, Me.Text)
            Me.Close()
        End If
    End Sub
    Private Sub btnClose_Click() Handles btnClose.ItemClick
        Me.Close()
    End Sub

    Private Sub grvNEWS_CellValueChanged(sender As Object, e As DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs) Handles grvNEWS.CellValueChanged
        Try
            Dim cek1 As Integer = 0
            Dim cek2 As Integer = 0
            Dim cek3 As Integer = 0
            Dim cek4 As Integer = 0
            Dim cek5 As Integer = 0
            Dim cek6 As Integer = 0
            Dim cek7 As Integer = 0
            Dim cek8 As Integer = 0
            Dim cek9 As Integer = 0
            Dim cek10 As Integer = 0
            Dim cek11 As Integer = 0
            Dim cek12 As Integer = 0
            Dim cek13 As Integer = 0
            Dim cek14 As Integer = 0
            Dim cek15 As Integer = 0
            Dim cek16 As Integer = 0
            Dim cek17 As Integer = 0

            For i As Integer = 0 To grvNEWS.RowCount - 1
                If grvNEWS.GetRowCellValue(i, GridColumn3) = True Then
                    cek1 = cek1 + grvNEWS.GetRowCellValue(i, NILAI)
                End If
                If grvNEWS.GetRowCellValue(i, GridColumn4) = True Then
                    cek2 = cek2 + grvNEWS.GetRowCellValue(i, NILAI)
                End If
                If grvNEWS.GetRowCellValue(i, GridColumn5) = True Then
                    cek3 = cek3 + grvNEWS.GetRowCellValue(i, NILAI)
                End If
                If grvNEWS.GetRowCellValue(i, GridColumn6) = True Then
                    cek4 = cek4 + grvNEWS.GetRowCellValue(i, NILAI)
                End If
                If grvNEWS.GetRowCellValue(i, GridColumn7) = True Then
                    cek5 = cek5 + grvNEWS.GetRowCellValue(i, NILAI)
                End If
                If grvNEWS.GetRowCellValue(i, GridColumn8) = True Then
                    cek6 = cek6 + grvNEWS.GetRowCellValue(i, NILAI)
                End If
                If grvNEWS.GetRowCellValue(i, GridColumn9) = True Then
                    cek7 = cek7 + grvNEWS.GetRowCellValue(i, NILAI)
                End If
                If grvNEWS.GetRowCellValue(i, GridColumn10) = True Then
                    cek8 = cek8 + grvNEWS.GetRowCellValue(i, NILAI)
                End If
                If grvNEWS.GetRowCellValue(i, GridColumn11) = True Then
                    cek9 = cek9 + grvNEWS.GetRowCellValue(i, NILAI)
                End If
                If grvNEWS.GetRowCellValue(i, GridColumn12) = True Then
                    cek10 = cek10 + grvNEWS.GetRowCellValue(i, NILAI)
                End If
                If grvNEWS.GetRowCellValue(i, GridColumn13) = True Then
                    cek11 = cek11 + grvNEWS.GetRowCellValue(i, NILAI)
                End If
                If grvNEWS.GetRowCellValue(i, GridColumn14) = True Then
                    cek12 = cek12 + grvNEWS.GetRowCellValue(i, NILAI)
                End If
                If grvNEWS.GetRowCellValue(i, GridColumn15) = True Then
                    cek13 = cek13 + grvNEWS.GetRowCellValue(i, NILAI)
                End If
                If grvNEWS.GetRowCellValue(i, GridColumn16) = True Then
                    cek14 = cek14 + grvNEWS.GetRowCellValue(i, NILAI)
                End If
                If grvNEWS.GetRowCellValue(i, GridColumn17) = True Then
                    cek15 = cek15 + grvNEWS.GetRowCellValue(i, NILAI)
                End If
                If grvNEWS.GetRowCellValue(i, GridColumn18) = True Then
                    cek16 = cek16 + grvNEWS.GetRowCellValue(i, NILAI)
                End If
                If grvNEWS.GetRowCellValue(i, GridColumn19) = True Then
                    cek17 = cek17 + grvNEWS.GetRowCellValue(i, NILAI)
                End If
            Next

            txtTOTALSKOR1.Text = cek1
            txtTOTALSKOR2.Text = cek2
            txtTOTALSKOR3.Text = cek3
            txtTOTALSKOR4.Text = cek4
            txtTOTALSKOR5.Text = cek5
            txtTOTALSKOR6.Text = cek6
            txtTOTALSKOR7.Text = cek7
            txtTOTALSKOR8.Text = cek8
            txtTOTALSKOR9.Text = cek9
            txtTOTALSKOR10.Text = cek10
            txtTOTALSKOR11.Text = cek11
            txtTOTALSKOR12.Text = cek12
            txtTOTALSKOR13.Text = cek13
            txtTOTALSKOR14.Text = cek14
            txtTOTALSKOR15.Text = cek15
            txtTOTALSKOR16.Text = cek16
            txtTOTALSKOR17.Text = cek17

            grdKDUSER_1.Focus()
            grvNEWS.Focus()
            
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub



#End Region
#Region "Lookup / Event"
    Private Sub fn_NOIDUSER()
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
            SQL &= "KDSTAFF = A.KDUNIT  "
            SQL &= ",NAME_DISPLAY = A.MEMO "
            SQL &= "FROM "
            SQL &= "M_UNIT A "
            SQL &= "WHERE "
            SQL &= "A.ISACTIVE = 1 "

            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "STAFF")

            grdKDUSER_1.Properties.DataSource = ds.Tables("STAFF")
            grdKDUSER_1.Properties.ValueMember = "KDSTAFF"
            grdKDUSER_1.Properties.DisplayMember = "NAME_DISPLAY"

            grdKDUSER_2.Properties.DataSource = ds.Tables("STAFF")
            grdKDUSER_2.Properties.ValueMember = "KDSTAFF"
            grdKDUSER_2.Properties.DisplayMember = "NAME_DISPLAY"

            grdKDUSER_3.Properties.DataSource = ds.Tables("STAFF")
            grdKDUSER_3.Properties.ValueMember = "KDSTAFF"
            grdKDUSER_3.Properties.DisplayMember = "NAME_DISPLAY"

            grdKDUSER_4.Properties.DataSource = ds.Tables("STAFF")
            grdKDUSER_4.Properties.ValueMember = "KDSTAFF"
            grdKDUSER_4.Properties.DisplayMember = "NAME_DISPLAY"

            grdKDUSER_5.Properties.DataSource = ds.Tables("STAFF")
            grdKDUSER_5.Properties.ValueMember = "KDSTAFF"
            grdKDUSER_5.Properties.DisplayMember = "NAME_DISPLAY"

            grdKDUSER_6.Properties.DataSource = ds.Tables("STAFF")
            grdKDUSER_6.Properties.ValueMember = "KDSTAFF"
            grdKDUSER_6.Properties.DisplayMember = "NAME_DISPLAY"

            grdKDUSER_7.Properties.DataSource = ds.Tables("STAFF")
            grdKDUSER_7.Properties.ValueMember = "KDSTAFF"
            grdKDUSER_7.Properties.DisplayMember = "NAME_DISPLAY"

            grdKDUSER_8.Properties.DataSource = ds.Tables("STAFF")
            grdKDUSER_8.Properties.ValueMember = "KDSTAFF"
            grdKDUSER_8.Properties.DisplayMember = "NAME_DISPLAY"

            grdKDUSER_9.Properties.DataSource = ds.Tables("STAFF")
            grdKDUSER_9.Properties.ValueMember = "KDSTAFF"
            grdKDUSER_9.Properties.DisplayMember = "NAME_DISPLAY"

            grdKDUSER_10.Properties.DataSource = ds.Tables("STAFF")
            grdKDUSER_10.Properties.ValueMember = "KDSTAFF"
            grdKDUSER_10.Properties.DisplayMember = "NAME_DISPLAY"

            grdKDUSER_11.Properties.DataSource = ds.Tables("STAFF")
            grdKDUSER_11.Properties.ValueMember = "KDSTAFF"
            grdKDUSER_11.Properties.DisplayMember = "NAME_DISPLAY"

            grdKDUSER_12.Properties.DataSource = ds.Tables("STAFF")
            grdKDUSER_12.Properties.ValueMember = "KDSTAFF"
            grdKDUSER_12.Properties.DisplayMember = "NAME_DISPLAY"

            grdKDUSER_13.Properties.DataSource = ds.Tables("STAFF")
            grdKDUSER_13.Properties.ValueMember = "KDSTAFF"
            grdKDUSER_13.Properties.DisplayMember = "NAME_DISPLAY"

            grdKDUSER_14.Properties.DataSource = ds.Tables("STAFF")
            grdKDUSER_14.Properties.ValueMember = "KDSTAFF"
            grdKDUSER_14.Properties.DisplayMember = "NAME_DISPLAY"

            grdKDUSER_15.Properties.DataSource = ds.Tables("STAFF")
            grdKDUSER_15.Properties.ValueMember = "KDSTAFF"
            grdKDUSER_15.Properties.DisplayMember = "NAME_DISPLAY"

            grdKDUSER_16.Properties.DataSource = ds.Tables("STAFF")
            grdKDUSER_16.Properties.ValueMember = "KDSTAFF"
            grdKDUSER_16.Properties.DisplayMember = "NAME_DISPLAY"

            grdKDUSER_17.Properties.DataSource = ds.Tables("STAFF")
            grdKDUSER_17.Properties.ValueMember = "KDSTAFF"
            grdKDUSER_17.Properties.DisplayMember = "NAME_DISPLAY"

            If oConn.State = ConnectionState.Open Then
                oConn.Close()
            End If
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub grdKDUSER_1_EditValueChanged(sender As Object, e As EventArgs) Handles grdKDUSER_1.EditValueChanged
        If isLoad = True Then
            txtNAMAPETUGAS1.Text = grdKDUSER_1.Text
        End If
    End Sub
    Private Sub grdKDUSER_2_EditValueChanged(sender As Object, e As EventArgs) Handles grdKDUSER_2.EditValueChanged
        If isLoad = True Then
            txtNAMAPETUGAS2.Text = grdKDUSER_2.Text
        End If
    End Sub
    Private Sub grdKDUSER_3_EditValueChanged(sender As Object, e As EventArgs) Handles grdKDUSER_3.EditValueChanged
        If isLoad = True Then
            txtNAMAPETUGAS3.Text = grdKDUSER_3.Text
        End If
    End Sub
    Private Sub grdKDUSER_4_EditValueChanged(sender As Object, e As EventArgs) Handles grdKDUSER_4.EditValueChanged
        If isLoad = True Then
            txtNAMAPETUGAS4.Text = grdKDUSER_4.Text
        End If
    End Sub
    Private Sub grdKDUSER_5_EditValueChanged(sender As Object, e As EventArgs) Handles grdKDUSER_5.EditValueChanged
        If isLoad = True Then
            txtNAMAPETUGAS5.Text = grdKDUSER_5.Text
        End If
    End Sub
    Private Sub grdKDUSER_6_EditValueChanged(sender As Object, e As EventArgs) Handles grdKDUSER_6.EditValueChanged
        If isLoad = True Then
            txtNAMAPETUGAS6.Text = grdKDUSER_6.Text
        End If
    End Sub
    Private Sub grdKDUSER_7_EditValueChanged(sender As Object, e As EventArgs) Handles grdKDUSER_7.EditValueChanged
        If isLoad = True Then
            txtNAMAPETUGAS7.Text = grdKDUSER_7.Text
        End If
    End Sub
    Private Sub grdKDUSER_8_EditValueChanged(sender As Object, e As EventArgs) Handles grdKDUSER_8.EditValueChanged
        If isLoad = True Then
            txtNAMAPETUGAS8.Text = grdKDUSER_8.Text
        End If
    End Sub
    Private Sub grdKDUSER_9_EditValueChanged(sender As Object, e As EventArgs) Handles grdKDUSER_9.EditValueChanged
        If isLoad = True Then
            txtNAMAPETUGAS9.Text = grdKDUSER_9.Text
        End If
    End Sub
    Private Sub grdKDUSER_10_EditValueChanged(sender As Object, e As EventArgs) Handles grdKDUSER_10.EditValueChanged
        If isLoad = True Then
            txtNAMAPETUGAS10.Text = grdKDUSER_10.Text
        End If
    End Sub
    Private Sub grdKDUSER_11_EditValueChanged(sender As Object, e As EventArgs) Handles grdKDUSER_11.EditValueChanged
        If isLoad = True Then
            txtNAMAPETUGAS11.Text = grdKDUSER_11.Text
        End If
    End Sub
    Private Sub grdKDUSER_12_EditValueChanged(sender As Object, e As EventArgs) Handles grdKDUSER_12.EditValueChanged
        If isLoad = True Then
            txtNAMAPETUGAS12.Text = grdKDUSER_12.Text
        End If
    End Sub
    Private Sub grdKDUSER_13_EditValueChanged(sender As Object, e As EventArgs) Handles grdKDUSER_13.EditValueChanged
        If isLoad = True Then
            txtNAMAPETUGAS13.Text = grdKDUSER_13.Text
        End If
    End Sub
    Private Sub grdKDUSER_14_EditValueChanged(sender As Object, e As EventArgs) Handles grdKDUSER_14.EditValueChanged
        If isLoad = True Then
            txtNAMAPETUGAS14.Text = grdKDUSER_14.Text
        End If
    End Sub
    Private Sub grdKDUSER_15_EditValueChanged(sender As Object, e As EventArgs) Handles grdKDUSER_15.EditValueChanged
        If isLoad = True Then
            txtNAMAPETUGAS15.Text = grdKDUSER_15.Text
        End If
    End Sub
    Private Sub grdKDUSER_16_EditValueChanged(sender As Object, e As EventArgs) Handles grdKDUSER_16.EditValueChanged
        If isLoad = True Then
            txtNAMAPETUGAS16.Text = grdKDUSER_16.Text
        End If
    End Sub
    Private Sub grdKDUSER_17_EditValueChanged(sender As Object, e As EventArgs) Handles grdKDUSER_17.EditValueChanged
        If isLoad = True Then
            txtNAMAPETUGAS17.Text = grdKDUSER_17.Text
        End If
    End Sub
#End Region
End Class