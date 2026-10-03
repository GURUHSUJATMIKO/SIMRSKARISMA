Imports DataAccess
Imports UI.WIN.MAIN.My.Resources
Imports System.Linq
Imports System.Data.SqlClient

Public Class frmAsesmenGiziLanjut
#Region "Declaration"
    Private oFormMode As FORM_MODE = FORM_MODE.FORM_MODE_VIEW
    Private isLoad As Boolean = False
    Private oS_DIGITAL_ASESMENGIZILANJUT As New Digital.clsS_DIGITAL_ASESMENGIZILANJUT

#End Region
#Region "Function"
    Public Sub LoadMe(ByVal FormMode As Integer, ByVal KDREG As String, ByVal KDCUSTOMER As String, ByVal NAMAPASIEN As String, ByVal UMUR As String, ByVal KDUSER_PERAWAT As String, ByVal JENISKELAMIN As String, ByVal RUANGAN As String)
        oFormMode = FormMode

        txtNOREG.Text = KDREG
        txtNAMA.Text = NAMAPASIEN
        txtNOMORRM.Text = KDCUSTOMER
        txtRUANG.Text = RUANGAN
        txtUMUR.Text = UMUR
        txtJK.Text = JENISKELAMIN

        txtUSERPERAWAT.Text = KDUSER_PERAWAT
    End Sub
    Private Sub Form_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        fn_ChangeFormState()
        isLoad = True
    End Sub
    Private Sub Form_Closing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        Dispose()
        sCode = txtNOREG.Text.Trim.ToUpper
    End Sub
    Private Overloads Sub Dispose()
        MyBase.Dispose()
        Me.Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
    Private Sub fn_ChangeFormState()
        fn_LoadDoctor()

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
        txtUMUR.Properties.ReadOnly = True
        txtJK.Properties.ReadOnly = True
        txtDIAGNOSAMEDIS.Properties.ReadOnly = Status
        grdKDDOCTOR.Properties.ReadOnly = Status
        txtMOBILITAS.Properties.ReadOnly = Status
        txtRIWAYATPENYAKITTERDAHULU.Properties.ReadOnly = Status
        txtRIWAYATPENYAKITKELUARGA.Properties.ReadOnly = Status
        chkKETERBATASANFISIK_ADA.Properties.ReadOnly = Status
        chkKETERBATASANFISIK_TIDAK.Properties.ReadOnly = Status
        chkPEROKOKYA.Properties.ReadOnly = Status
        chkPEROKOKTIDAK.Properties.ReadOnly = Status
        chkPEROKOKPASIF.Properties.ReadOnly = Status
        'chkASMAK25.Properties.ReadOnly = Status
        'chkASMAK50.Properties.ReadOnly = Status
        'chkASMAK75.Properties.ReadOnly = Status
        'chkASMAK100.Properties.ReadOnly = Status
        'txtALERGIMAK.Properties.ReadOnly = Status
        'txtPANTANGANMAK.Properties.ReadOnly = Status
        'chkKONSUL_PERNAH.Properties.ReadOnly = Status
        'chkKONSUL_TIDAK.Properties.ReadOnly = Status
        txtRIWAYATMAKAN.Properties.ReadOnly = Status
        'txtHASILRECALL.Properties.ReadOnly = Status
        'txtSIMPULAN.Properties.ReadOnly = Status
        txtBB.Properties.ReadOnly = Status
        txtBBBIASANYA.Properties.ReadOnly = Status
        txtPB.Properties.ReadOnly = Status
        txtIMT.Properties.ReadOnly = Status
        txtSTATUSGIZI.Properties.ReadOnly = Status
        txtPENURUNANBB.Properties.ReadOnly = Status
        txtDALAM.Properties.ReadOnly = Status
        txtTL.Properties.ReadOnly = Status
        txtLILA.Properties.ReadOnly = Status
        'txtPEMERIKSAAN1.Properties.ReadOnly = Status
        'txtPEMERIKSAAN2.Properties.ReadOnly = Status
        'txtPEMERIKSAAN3.Properties.ReadOnly = Status
        'txtPEMERIKSAAN4.Properties.ReadOnly = Status
        'txtPEMERIKSAAN5.Properties.ReadOnly = Status
        'txtPEMERIKSAAN6.Properties.ReadOnly = Status
        'txtPEMERIKSAAN7.Properties.ReadOnly = Status
        'txtPEMERIKSAAN8.Properties.ReadOnly = Status
        'txtNILAIHASIL1.Properties.ReadOnly = Status
        'txtNILAIHASIL2.Properties.ReadOnly = Status
        'txtNILAIHASIL3.Properties.ReadOnly = Status
        'txtNILAIHASIL4.Properties.ReadOnly = Status
        'txtNILAIHASIL5.Properties.ReadOnly = Status
        'txtNILAIHASIL6.Properties.ReadOnly = Status
        'txtNILAIHASIL7.Properties.ReadOnly = Status
        'txtNILAIHASIL8.Properties.ReadOnly = Status
        'txtKATEGORI1.Properties.ReadOnly = Status
        'txtKATEGORI2.Properties.ReadOnly = Status
        'txtKATEGORI3.Properties.ReadOnly = Status
        'txtKATEGORI4.Properties.ReadOnly = Status
        'txtKATEGORI5.Properties.ReadOnly = Status
        'txtKATEGORI6.Properties.ReadOnly = Status
        'txtKATEGORI7.Properties.ReadOnly = Status
        'txtKATEGORI8.Properties.ReadOnly = Status
        txtBIOKIMIA.Properties.ReadOnly = Status

        'txtHASILUSGTHORAX.Properties.ReadOnly = Status
        chkKLINIS1.Properties.ReadOnly = Status
        chkKLINIS2.Properties.ReadOnly = Status
        chkKLINIS3.Properties.ReadOnly = Status
        chkKLINIS4.Properties.ReadOnly = Status
        chkKLINIS5.Properties.ReadOnly = Status
        chkKLINIS6.Properties.ReadOnly = Status
        chkKLINIS7.Properties.ReadOnly = Status
        chkKLINIS8.Properties.ReadOnly = Status
        chkKLINIS9.Properties.ReadOnly = Status
        chkKLINIS10.Properties.ReadOnly = Status
        chkKLINIS11.Properties.ReadOnly = Status
        chkKLINIS12.Properties.ReadOnly = Status
        chkKLINIS13.Properties.ReadOnly = Status
        chkKLINIS14.Properties.ReadOnly = Status
        chkKLINIS15.Properties.ReadOnly = Status
        chkKLINIS16.Properties.ReadOnly = Status
        chkKLINIS17.Properties.ReadOnly = Status
        chkKLINIS18.Properties.ReadOnly = Status
        chkKLINIS19.Properties.ReadOnly = Status
        chkKLINIS20.Properties.ReadOnly = Status
        chkKLINIS21.Properties.ReadOnly = Status
        chkKLINIS22.Properties.ReadOnly = Status
        chkKLINIS23.Properties.ReadOnly = Status
        chkKLINIS24.Properties.ReadOnly = Status
        chkKLINIS25.Properties.ReadOnly = Status
        chkKLINIS26.Properties.ReadOnly = Status
        chkKLINIS27.Properties.ReadOnly = Status
        chkKLINIS28.Properties.ReadOnly = Status
        chkKLINIS29.Properties.ReadOnly = Status
        chkKLINIS30.Properties.ReadOnly = Status
        txtTD.Properties.ReadOnly = Status
        txtNADI.Properties.ReadOnly = Status
        txtRESPIRASI.Properties.ReadOnly = Status
        txtSUHU.Properties.ReadOnly = Status
        txtDATALAIN.Properties.ReadOnly = Status
        txtDIAGNOSAGIZI.Properties.ReadOnly = Status
        txtINTERVENSIGIZI.Properties.ReadOnly = Status
        'txtINDIKATOR1.Properties.ReadOnly = Status
        'txtINDIKATOR2.Properties.ReadOnly = Status
        'txtINDIKATOR3.Properties.ReadOnly = Status
        'txtINDIKATOR4.Properties.ReadOnly = Status
        'txtINDIKATOR5.Properties.ReadOnly = Status
        'txtINDIKATOR6.Properties.ReadOnly = Status
        'txtINDIKATOR7.Properties.ReadOnly = Status
        'txtINDIKATOR8.Properties.ReadOnly = Status
        'txtTARGET1.Properties.ReadOnly = Status
        'txtTARGET2.Properties.ReadOnly = Status
        'txtTARGET3.Properties.ReadOnly = Status
        'txtTARGET4.Properties.ReadOnly = Status
        'txtTARGET5.Properties.ReadOnly = Status
        'txtTARGET6.Properties.ReadOnly = Status
        'txtTARGET7.Properties.ReadOnly = Status
        'txtTARGET8.Properties.ReadOnly = Status
        txtRIWAYATPERSONAL.Properties.ReadOnly = Status

    End Sub
    Private Sub fn_EmptyMe()
        deDATE.DateTime = Now
        txtDIAGNOSAMEDIS.ResetText()
        txtMOBILITAS.ResetText()
        txtRIWAYATPENYAKITTERDAHULU.ResetText()
        txtRIWAYATPENYAKITKELUARGA.ResetText()
        chkKETERBATASANFISIK_ADA.Checked = False
        chkKETERBATASANFISIK_TIDAK.Checked = False
        chkPEROKOKYA.Checked = False
        chkPEROKOKTIDAK.Checked = False
        chkPEROKOKPASIF.Checked = False
        'chkASMAK25.Checked = False
        'chkASMAK50.Checked = False
        'chkASMAK75.Checked = False
        'chkASMAK100.Checked = False
        'txtALERGIMAK.ResetText()
        'txtPANTANGANMAK.ResetText()
        'chkKONSUL_PERNAH.Checked = False
        'chkKONSUL_TIDAK.Checked = False
        txtRIWAYATMAKAN.ResetText()
        'txtHASILRECALL.ResetText()
        'txtSIMPULAN.ResetText()
        txtBB.ResetText()
        txtBBBIASANYA.ResetText()
        txtPB.ResetText()
        txtIMT.ResetText()
        txtSTATUSGIZI.ResetText()
        txtPENURUNANBB.ResetText()
        txtDALAM.ResetText()
        txtTL.ResetText()
        txtLILA.ResetText()
        'txtPEMERIKSAAN1.ResetText()
        'txtPEMERIKSAAN2.ResetText()
        'txtPEMERIKSAAN3.ResetText()
        'txtPEMERIKSAAN4.ResetText()
        'txtPEMERIKSAAN5.ResetText()
        'txtPEMERIKSAAN6.ResetText()
        'txtPEMERIKSAAN7.ResetText()
        'txtPEMERIKSAAN8.ResetText()
        'txtNILAIHASIL1.ResetText()
        'txtNILAIHASIL2.ResetText()
        'txtNILAIHASIL3.ResetText()
        'txtNILAIHASIL4.ResetText()
        'txtNILAIHASIL5.ResetText()
        'txtNILAIHASIL6.ResetText()
        'txtNILAIHASIL7.ResetText()
        'txtNILAIHASIL8.ResetText()
        'txtKATEGORI1.ResetText()
        'txtKATEGORI2.ResetText()
        'txtKATEGORI3.ResetText()
        'txtKATEGORI4.ResetText()
        'txtKATEGORI5.ResetText()
        'txtKATEGORI6.ResetText()
        'txtKATEGORI7.ResetText()
        'txtKATEGORI8.ResetText()
        txtBIOKIMIA.ResetText()

        'txtHASILUSGTHORAX.ResetText()
        chkKLINIS1.Checked = False
        chkKLINIS2.Checked = False
        chkKLINIS3.Checked = False
        chkKLINIS4.Checked = False
        chkKLINIS5.Checked = False
        chkKLINIS6.Checked = False
        chkKLINIS7.Checked = False
        chkKLINIS8.Checked = False
        chkKLINIS9.Checked = False
        chkKLINIS10.Checked = False
        chkKLINIS11.Checked = False
        chkKLINIS12.Checked = False
        chkKLINIS13.Checked = False
        chkKLINIS14.Checked = False
        chkKLINIS15.Checked = False
        chkKLINIS16.Checked = False
        chkKLINIS17.Checked = False
        chkKLINIS18.Checked = False
        chkKLINIS19.Checked = False
        chkKLINIS20.Checked = False
        chkKLINIS21.Checked = False
        chkKLINIS22.Checked = False
        chkKLINIS23.Checked = False
        chkKLINIS24.Checked = False
        chkKLINIS25.Checked = False
        chkKLINIS26.Checked = False
        chkKLINIS27.Checked = False
        chkKLINIS28.Checked = False
        chkKLINIS29.Checked = False
        chkKLINIS30.Checked = False
        txtTD.ResetText()
        txtNADI.ResetText()
        txtRESPIRASI.ResetText()
        txtSUHU.ResetText()
        txtDATALAIN.ResetText()
        txtDIAGNOSAGIZI.ResetText()
        txtINTERVENSIGIZI.ResetText()
        'txtINDIKATOR1.ResetText()
        'txtINDIKATOR2.ResetText()
        'txtINDIKATOR3.ResetText()
        'txtINDIKATOR4.ResetText()
        'txtINDIKATOR5.ResetText()
        'txtINDIKATOR6.ResetText()
        'txtINDIKATOR7.ResetText()
        'txtINDIKATOR8.ResetText()
        'txtTARGET1.ResetText()
        'txtTARGET2.ResetText()
        'txtTARGET3.ResetText()
        'txtTARGET4.ResetText()
        'txtTARGET5.ResetText()
        'txtTARGET6.ResetText()
        'txtTARGET7.ResetText()
        'txtTARGET8.ResetText()
        txtRIWAYATPERSONAL.ResetText()

        TextEdit1.ResetText()
        TextEdit2.ResetText()
        TextEdit3.ResetText()
        TextEdit4.ResetText()
    End Sub
    Private Sub fn_LoadData()
        Try
            ' ***** HEADER *****
            Dim ds = oS_DIGITAL_ASESMENGIZILANJUT.GetData(txtNOREG.Text)

            With ds
                deDATE.DateTime = .DATE
                txtDIAGNOSAMEDIS.Text = .DIAGNOSAMEDIS
                grdKDDOCTOR.EditValue = .KDDOCTOR
                txtMOBILITAS.Text = .MOBILITAS
                txtRIWAYATPENYAKITTERDAHULU.Text = .RIWAYATPENYAKITTERDAHULU
                txtRIWAYATPENYAKITKELUARGA.Text = .RIWAYATPENYAKITKELUARGA
                chkKETERBATASANFISIK_ADA.Checked = .KETERBATASANFISIK_ADA
                chkKETERBATASANFISIK_TIDAK.Checked = .KETERBATASANFISIK_TIDAK
                chkPEROKOKYA.Checked = .PEROKOKYA
                chkPEROKOKTIDAK.Checked = .PEROKOKTIDAK
                chkPEROKOKPASIF.Checked = .PEROKOKPASIF
                'chkASMAK25.Checked = .ASMAK25
                'chkASMAK50.Checked = .ASMAK50
                'chkASMAK75.Checked = .ASMAK75
                'chkASMAK100.Checked = .ASMAK100
                'txtALERGIMAK.Text = .ALERGIMAK
                'txtPANTANGANMAK.Text = .PANTANGANMAK
                'chkKONSUL_PERNAH.Checked = .KONSUL_PERNAH
                'chkKONSUL_TIDAK.Checked = .KONSUL_TIDAK
                txtRIWAYATMAKAN.Text = .RIWAYATMAKAN
                'txtHASILRECALL.Text = .HASILRECALL
                'txtSIMPULAN.Text = .SIMPULAN
                txtBB.Text = .BB
                txtBBBIASANYA.Text = .BBBIASANYA
                txtPB.Text = .PB
                txtIMT.Text = .IMT
                txtSTATUSGIZI.Text = .STATUSGIZI
                txtPENURUNANBB.Text = .PENURUNANBB
                txtDALAM.Text = .DALAM
                txtTL.Text = .TL
                txtLILA.Text = .LILA
                'txtPEMERIKSAAN1.Text = .PEMERIKSAAN1
                'txtPEMERIKSAAN2.Text = .PEMERIKSAAN2
                'txtPEMERIKSAAN3.Text = .PEMERIKSAAN3
                'txtPEMERIKSAAN4.Text = .PEMERIKSAAN4
                'txtPEMERIKSAAN5.Text = .PEMERIKSAAN5
                'txtPEMERIKSAAN6.Text = .PEMERIKSAAN6
                'txtPEMERIKSAAN7.Text = .PEMERIKSAAN7
                'txtPEMERIKSAAN8.Text = .PEMERIKSAAN8
                'txtNILAIHASIL1.Text = .NILAIHASIL1
                'txtNILAIHASIL2.Text = .NILAIHASIL2
                'txtNILAIHASIL3.Text = .NILAIHASIL3
                'txtNILAIHASIL4.Text = .NILAIHASIL4
                'txtNILAIHASIL5.Text = .NILAIHASIL5
                'txtNILAIHASIL6.Text = .NILAIHASIL6
                'txtNILAIHASIL7.Text = .NILAIHASIL7
                'txtNILAIHASIL8.Text = .NILAIHASIL8
                'txtKATEGORI1.Text = .KATEGORI1
                'txtKATEGORI2.Text = .KATEGORI2
                'txtKATEGORI3.Text = .KATEGORI3
                'txtKATEGORI4.Text = .KATEGORI4
                'txtKATEGORI5.Text = .KATEGORI5
                'txtKATEGORI6.Text = .KATEGORI6
                'txtKATEGORI7.Text = .KATEGORI7
                'txtKATEGORI8.Text = .KATEGORI8
                txtBIOKIMIA.Text = .HASILUSGTHORAX
                'txtHASILUSGTHORAX.Text = .HASILUSGTHORAX
                chkKLINIS1.Checked = .KLINIS1
                chkKLINIS2.Checked = .KLINIS2
                chkKLINIS3.Checked = .KLINIS3
                chkKLINIS4.Checked = .KLINIS4
                chkKLINIS5.Checked = .KLINIS5
                chkKLINIS6.Checked = .KLINIS6
                chkKLINIS7.Checked = .KLINIS7
                chkKLINIS8.Checked = .KLINIS8
                chkKLINIS9.Checked = .KLINIS9
                chkKLINIS10.Checked = .KLINIS10
                chkKLINIS11.Checked = .KLINIS11
                chkKLINIS12.Checked = .KLINIS12
                chkKLINIS13.Checked = .KLINIS13
                chkKLINIS14.Checked = .KLINIS14
                chkKLINIS15.Checked = .KLINIS15
                chkKLINIS16.Checked = .KLINIS16
                chkKLINIS17.Checked = .KLINIS17
                chkKLINIS18.Checked = .KLINIS18
                chkKLINIS19.Checked = .KLINIS19
                chkKLINIS20.Checked = .KLINIS20
                chkKLINIS21.Checked = .KLINIS21
                chkKLINIS22.Checked = .KLINIS22
                chkKLINIS23.Checked = .KLINIS23
                chkKLINIS24.Checked = .KLINIS24
                chkKLINIS25.Checked = .KLINIS25
                chkKLINIS26.Checked = .KLINIS26
                chkKLINIS27.Checked = .KLINIS27
                chkKLINIS28.Checked = .KLINIS28
                chkKLINIS29.Checked = .KLINIS29
                chkKLINIS30.Checked = .KLINIS30
                txtTD.Text = .TD
                txtNADI.Text = .NADI
                txtRESPIRASI.Text = .RESPIRASI
                txtSUHU.Text = .SUHU
                txtDATALAIN.Text = .DATALAIN
                txtDIAGNOSAGIZI.Text = .DIAGNOSAGIZI
                txtINTERVENSIGIZI.Text = .INTERVENSIGIZI
                'txtINDIKATOR1.Text = .INDIKATOR1
                'txtINDIKATOR2.Text = .INDIKATOR2
                'txtINDIKATOR3.Text = .INDIKATOR3
                'txtINDIKATOR4.Text = .INDIKATOR4
                'txtINDIKATOR5.Text = .INDIKATOR5
                'txtINDIKATOR6.Text = .INDIKATOR6
                'txtINDIKATOR7.Text = .INDIKATOR7
                'txtINDIKATOR8.Text = .INDIKATOR8
                'txtTARGET1.Text = .TARGET1
                'txtTARGET2.Text = .TARGET2
                'txtTARGET3.Text = .TARGET3
                'txtTARGET4.Text = .TARGET4
                'txtTARGET5.Text = .TARGET5
                'txtTARGET6.Text = .TARGET6
                'txtTARGET7.Text = .TARGET7
                'txtTARGET8.Text = .TARGET8
                txtRIWAYATPERSONAL.Text = .RIWAYATPERSONAL

                TextEdit1.Text = .INDIKATOR1
                TextEdit2.Text = .INDIKATOR2
                TextEdit3.Text = .INDIKATOR3
                TextEdit4.Text = .INDIKATOR4
            End With
        Catch oErr As Exception
            MsgBox("Load List Data : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Function fn_Validate() As Boolean
        Try
            fn_Validate = True
            If txtNOREG.Text = String.Empty Then
                MsgBox("Dibutuhkan Register", MsgBoxStyle.Exclamation, Me.Text)
                txtNOREG.Focus()
                fn_Validate = False
                Exit Function
            End If

            If grdKDDOCTOR.Text = String.Empty Then
                MsgBox("Dibutuhkan Dokter", MsgBoxStyle.Exclamation, Me.Text)
                grdKDDOCTOR.Focus()
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
            Dim ds = oS_DIGITAL_ASESMENGIZILANJUT.GetStructureHeader
            With ds
                .KDREG = txtNOREG.Text
                .KDCUSTOMER = txtNOMORRM.Text
                Try
                    .DATECREATED = oS_DIGITAL_ASESMENGIZILANJUT.GetData(txtNOREG.Text).DATECREATED
                Catch ex As Exception
                    .DATECREATED = Now
                End Try
                .DATEUPDATED = Now
                .DATE = deDATE.DateTime
                .DIAGNOSAMEDIS= txtDIAGNOSAMEDIS.Text 
                .KDDOCTOR= grdKDDOCTOR.EditValue
                .NMDOCTOR= grdKDDOCTOR.Text 
                .MOBILITAS= txtMOBILITAS.Text 
                .RIWAYATPENYAKITTERDAHULU= txtRIWAYATPENYAKITTERDAHULU.Text 
                .RIWAYATPENYAKITKELUARGA= txtRIWAYATPENYAKITKELUARGA.Text 
                .KETERBATASANFISIK_ADA= chkKETERBATASANFISIK_ADA.Checked 
                .KETERBATASANFISIK_TIDAK= chkKETERBATASANFISIK_TIDAK.Checked 
                .PEROKOKYA= chkPEROKOKYA.Checked 
                .PEROKOKTIDAK= chkPEROKOKTIDAK.Checked 
                .PEROKOKPASIF= chkPEROKOKPASIF.Checked
                .ASMAK25 = False
                .ASMAK50 = False
                .ASMAK75 = False
                .ASMAK100 = False
                .ALERGIMAK = ""
                .PANTANGANMAK = ""
                .KONSUL_PERNAH = False
                .KONSUL_TIDAK = False
                .RIWAYATMAKAN = txtRIWAYATMAKAN.Text
                .HASILRECALL = ""
                .SIMPULAN = False
                .BB= txtBB.Text 
                .BBBIASANYA= txtBBBIASANYA.Text 
                .PB= txtPB.Text 
                .IMT= txtIMT.Text 
                .STATUSGIZI= txtSTATUSGIZI.Text 
                .PENURUNANBB= txtPENURUNANBB.Text 
                .DALAM= txtDALAM.Text 
                .TL= txtTL.Text 
                .LILA= txtLILA.Text
                .PEMERIKSAAN1 = ""
                .PEMERIKSAAN2 = ""
                .PEMERIKSAAN3 = ""
                .PEMERIKSAAN4 = ""
                .PEMERIKSAAN5 = ""
                .PEMERIKSAAN6 = ""
                .PEMERIKSAAN7 = ""
                .PEMERIKSAAN8 = ""
                .NILAIHASIL1 = ""
                .NILAIHASIL2 = ""
                .NILAIHASIL3 = ""
                .NILAIHASIL4 = ""
                .NILAIHASIL5 = ""
                .NILAIHASIL6 = ""
                .NILAIHASIL7 = ""
                .NILAIHASIL8 = ""
                .KATEGORI1 = ""
                .KATEGORI2 = ""
                .KATEGORI3 = ""
                .KATEGORI4 = ""
                .KATEGORI5 = ""
                .KATEGORI6 = ""
                .KATEGORI7 = ""
                .KATEGORI8 = ""
                .HASILUSGTHORAX = txtBIOKIMIA.Text
                .KLINIS1= chkKLINIS1.Checked 
                .KLINIS2= chkKLINIS2.Checked 
                .KLINIS3= chkKLINIS3.Checked 
                .KLINIS4= chkKLINIS4.Checked 
                .KLINIS5= chkKLINIS5.Checked 
                .KLINIS6= chkKLINIS6.Checked 
                .KLINIS7= chkKLINIS7.Checked 
                .KLINIS8= chkKLINIS8.Checked 
                .KLINIS9= chkKLINIS9.Checked 
                .KLINIS10= chkKLINIS10.Checked 
                .KLINIS11= chkKLINIS11.Checked 
                .KLINIS12= chkKLINIS12.Checked 
                .KLINIS13= chkKLINIS13.Checked 
                .KLINIS14= chkKLINIS14.Checked 
                .KLINIS15= chkKLINIS15.Checked 
                .KLINIS16= chkKLINIS16.Checked 
                .KLINIS17= chkKLINIS17.Checked 
                .KLINIS18= chkKLINIS18.Checked 
                .KLINIS19= chkKLINIS19.Checked 
                .KLINIS20= chkKLINIS20.Checked 
                .KLINIS21= chkKLINIS21.Checked 
                .KLINIS22= chkKLINIS22.Checked 
                .KLINIS23= chkKLINIS23.Checked 
                .KLINIS24= chkKLINIS24.Checked 
                .KLINIS25= chkKLINIS25.Checked 
                .KLINIS26= chkKLINIS26.Checked 
                .KLINIS27= chkKLINIS27.Checked 
                .KLINIS28= chkKLINIS28.Checked 
                .KLINIS29= chkKLINIS29.Checked 
                .KLINIS30= chkKLINIS30.Checked 
                .TD= txtTD.Text 
                .NADI=txtNADI.Text
                .RESPIRASI= txtRESPIRASI.Text 
                .SUHU= txtSUHU.Text 
                .DATALAIN= txtDATALAIN.Text 
                .DIAGNOSAGIZI= txtDIAGNOSAGIZI.Text 
                .INTERVENSIGIZI= txtINTERVENSIGIZI.Text
                .INDIKATOR1 = TextEdit1.Text
                .INDIKATOR2 = TextEdit2.Text
                .INDIKATOR3 = TextEdit3.Text
                .INDIKATOR4 = TextEdit4.Text
                .INDIKATOR5 = ""
                .INDIKATOR6 = ""
                .INDIKATOR7 = ""
                .INDIKATOR8 = ""
                .TARGET1 = ""
                .TARGET2 = ""
                .TARGET3 = ""
                .TARGET4 = ""
                .TARGET5 = ""
                .TARGET6 = ""
                .TARGET7 = ""
                .TARGET8 = ""
                .RIWAYATPERSONAL = txtRIWAYATPERSONAL.Text

                Try
                    .CETAK = oS_DIGITAL_ASESMENGIZILANJUT.GetData(txtNOREG.Text).CETAK
                Catch ex As Exception
                    .CETAK = 0
                End Try

                .KDUSER = sUserID
                .KDUSER_SIGNATURE = txtUSERPERAWAT.Text
            End With

            If oFormMode = FORM_MODE.FORM_MODE_ADD Then
                Try
                    fn_Save = oS_DIGITAL_ASESMENGIZILANJUT.InsertData(ds)
                Catch ex As Exception
                    MsgBox("Simpan Data : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            ElseIf oFormMode = FORM_MODE.FORM_MODE_EDIT Then
                Try
                    fn_Save = oS_DIGITAL_ASESMENGIZILANJUT.UpdateData(ds)
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
            Case Keys.PageUp
                fn_ScrollPage(True)
            Case Keys.PageDown
	            fn_ScrollPage(False)
        End Select
    End Sub
    Private Sub btnSaveClose_Click() Handles btnSaveClose.ItemClick
        If fn_Validate() = False Then Exit Sub
        If MsgBox("Save " & txtNOREG.Text.Trim.ToUpper & "?", MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, Me.Text) = MsgBoxResult.No Then Exit Sub
        If fn_Save() = False Then
            MsgBox("Save gagal! Tolong kontak admin anda!", MsgBoxStyle.Exclamation, Me.Text)
        Else
            MsgBox("Save " & txtNOREG.Text.Trim.ToUpper & " success!", MsgBoxStyle.Information, Me.Text)
            Me.Close()
        End If
    End Sub
    Private Sub btnClose_Click() Handles btnClose.ItemClick
        Me.Close()
    End Sub
#End Region
#Region "Lookup / Event"
    'Private Sub SimpleButton1_Click(sender As Object, e As EventArgs) 
    '    txtTL.Text = "dalam batas normal"
    '    txtPEMERIKSAAN4.Text = "dalam batas normal"
    '    txtPEMERIKSAAN3.Text = "dalam batas normal"
    '    txtPEMERIKSAAN2.Text = "dalam batas normal"
    '    txtPEMERIKSAAN1.Text = "dalam batas normal"
    '    txtLILA.Text = "dalam batas normal"
    '    txtNILAIHASIL1.Text = "dalam batas normal"
    '    txtNILAIHASIL2.Text = "dalam batas normal"
    '    txtNILAIHASIL3.Text = "dalam batas normal"
    'End Sub
    Private Sub fn_LoadDoctor()
        Try
            Dim oConn As New SqlConnection
            Dim oComm As New SqlCommand
            Dim da As SqlDataAdapter
            Dim ds As New DataSet
            Dim SQL As String
            Dim sConn As String = sConnOld
            oConn = New SqlConnection(sConn)

            If oConn.State = ConnectionState.Closed Then
                oConn.Open()
            End If

            SQL = "SELECT "
            SQL &= "* "
            SQL &= "FROM "
            SQL &= "M_DOCTOR A "
            SQL &= "WHERE "
            SQL &= "A.ISACTIVE = 1 "

            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "M_DOCTOR")

            grdKDDOCTOR.Properties.DataSource = ds.Tables("M_DOCTOR")
            grdKDDOCTOR.Properties.ValueMember = "KDDOCTOR"
            grdKDDOCTOR.Properties.DisplayMember = "NAME_DISPLAY"

            If oConn.State = ConnectionState.Open Then
                oConn.Close()
            End If
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub

    Private Sub frmAsesmenGiziLanjut_MouseWheel(sender As Object, e As MouseEventArgs) Handles MyBase.MouseWheel
	    If e.Delta > 0 Then
		    'up
		    fn_ScrollPage(True)
	    Else
		    'down
		    fn_ScrollPage(False)
	    End If
    End Sub

    Private Sub fn_ScrollPage(ByVal isUp As Boolean)
	    Dim myView As Point = Me.Panel3.AutoScrollPosition
	    Dim scrollchange As Integer = 50

	    If isUp Then
		    'up
		    myView.X = -myView.X
		    myView.y = -scrollchange - myView.Y
	    Else
		    'down
		    myView.X = -myView.X
		    myView.y = scrollchange - myView.Y
	    End If

	    Me.Panel3.AutoScrollPosition = myView
    End Sub
#End Region
End Class