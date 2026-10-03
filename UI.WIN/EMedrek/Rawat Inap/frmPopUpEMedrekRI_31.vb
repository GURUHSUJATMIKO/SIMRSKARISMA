
Imports System.Linq
Imports System.Data
Imports System.Data.SqlClient

Public Class frmPopUpEMedrekRI_31
    Private oTgl As String = ""
    Private oTENSI As String = ""
    Private oNADI As String = ""
    Private oSUHU As String = ""
    Private oRESPIRASI As String = ""
    Private oKES As String = ""
    Private oSPO2 As String = ""
    Private oORAL As String = ""
    Private oINFUS As String = ""
    Private oDARAH As String = ""
    Private oURINE As String = ""
    Private oDRAIN As String = ""
    Private oNGT As String = ""
    Private oCATATAN As String = ""

    Public Sub fn_LoadMe(ByVal sDATE As String, ByVal sTENSI As String, ByVal sNADI As String, ByVal sSUHU As String, ByVal sRESPIRASI As String, ByVal sKES As String, ByVal sSPO2 As String, ByVal sORAL As String, ByVal sINFUS As String, ByVal sDARAH As String, ByVal sURINE As String, ByVal sDRAIN As String, ByVal sNGT As String, ByVal sCATATAN As String)
        oTgl = sDATE
        oTENSI = sTENSI
        oNADI = sNADI
        oSUHU = sSUHU
        oRESPIRASI = sRESPIRASI
        oKES = sKES
        oSPO2 = sSPO2
        oORAL = sORAL
        oINFUS = sINFUS
        oDARAH = sDARAH
        oURINE = sURINE
        oDRAIN = sDRAIN
        oNGT = sNGT
        oCATATAN = sCATATAN
    End Sub
    Private Sub Form_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        fn_Load()
        fn_LoadLanguage()
    End Sub
    Private Sub cmdSelect_Click()
        If txtTANGGAL.Text = String.Empty Then

            MsgBox("Dibutuhkan Tanggal dan Jam", MsgBoxStyle.Exclamation, Me.Text)
            txtTANGGAL.Focus()
            Exit Sub
        End If

        sFind1 = txtTANGGAL.Text
        sFind2 = txtTENSI.Text
        sFind3 = txtNADI.Text
        sFind4 = txtSUHU.Text
        sFind5 = txtRESPIRASI.Text
        sFind6 = txtKES.Text
        sFind7 = txtSPO2.Text
        sFind8 = txtORAL.Text
        sFind9 = txtINFUS.Text
        sFind10 = txtDARAH.Text
        sFind11 = txtURINE.Text
        sFind12 = txtDRAIN.Text
        sFind13 = txtNGT.Text
        sFind14 = txtCATATAN.Text

        Me.Close()
    End Sub
    Private Sub Form_Closing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        Dispose()
        'sCode = txtDescription.Text.Trim.ToUpper
    End Sub
    Private Overloads Sub Dispose()
        MyBase.Dispose()
        Me.Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
    Public Sub fn_LoadLanguage()

    End Sub
    Private Sub fn_Load()
        txtTANGGAL.Text = oTgl
        txtTENSI.Text = oTENSI
        txtNADI.Text = oNADI
        txtSUHU.Text = oSUHU
        txtRESPIRASI.Text = oRESPIRASI
        txtKES.Text = oKES
        txtSPO2.Text = oSPO2
        txtORAL.Text = oORAL
        txtINFUS.Text = oINFUS
        txtDARAH.Text = oDARAH
        txtURINE.Text = oURINE
        txtDRAIN.Text = oDRAIN
        txtNGT.Text = oNGT
        txtCATATAN.Text = oCATATAN
    End Sub
#Region "Command Button"
    Private Sub frmItem_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        Select Case e.KeyCode
            Case Keys.F12
                btnClose_Click()
            Case Keys.F3
                If btnSaveClose.Enabled = True Then
                    btnSaveClose_Click()
                End If
            Case Keys.Escape
                btnClose_Click()
        End Select
    End Sub
    Private Sub btnClose_Click() Handles btnClose.ItemClick

        sFind1 = String.Empty
        sFind2 = String.Empty
        sFind3 = String.Empty
        sFind4 = String.Empty
        sFind5 = String.Empty
        sFind6 = String.Empty
        sFind7 = String.Empty
        sFind8 = String.Empty
        sFind9 = String.Empty
        sFind10 = String.Empty
        sFind11 = String.Empty
        sFind12 = String.Empty
        sFind13 = String.Empty
        sFind14 = String.Empty

        Me.Close()
    End Sub
    Private Sub btnSaveClose_Click() Handles btnSaveClose.ItemClick
        cmdSelect_Click()
    End Sub
#End Region
End Class