Imports System.IO
Imports System.Text
Imports DataAccess

Module MainClass
    Public Enum FORM_MODE
        FORM_MODE_VIEW = 0
        FORM_MODE_ADD = 1
        FORM_MODE_EDIT = 2
    End Enum

    Public sUSIADIASESMENIGD As String = String.Empty
    Public sKDDOCTOR_PENDAFTARAN As String = String.Empty
    Public sLoadLaporanOperasi As Boolean = False
    Public sLoadAsesmenAwal As Boolean = False
    Public sTotalPaket As Decimal = 0
    Public sTotalProlanis As Decimal = 0
    Public sGrandtotalRS As Decimal = 0
    Public sCetakUserByMerge As Boolean = False
    Public sUSERCASHIER As String = String.Empty
    Public sUmurPasienDiCPPT As String = String.Empty
    Public sAlamatSuara As String = String.Empty
    Public sAlamatSimpanPDFAsesmenIGD As String = String.Empty
    Public sAlamatSimpanAsesmenIGD As String = String.Empty
    Public sUrlAntrean As String = String.Empty
    Public sConsidAntrean As String = String.Empty
    Public sSecreateKeyAntrean As String = String.Empty
    Public sUserKeyAntrean As String = String.Empty
    Public sUrlVclaim As String = String.Empty
    Public sConsidVclaim As String = String.Empty
    Public sSecreateKeyVclaim As String = String.Empty
    Public sUserKeyVclaim As String = String.Empty
    'Public CopyLocal As String = "C:/TANDATANGAN/"
    Public sCPPT_QRRJ As Boolean = True
    Public sKDCPPT As String = String.Empty
    'Public sDownloadAlamatTandaTanganDokter As String = String.Empty
    Public sKodeTarifEClaim As String = String.Empty
    'Public sUrlEClaim As String = String.Empty
    'Public sGenerateClaim As String = String.Empty
    Public sAlamatSimpanPDFLaboratorium As String = String.Empty
    Public sAlamatBriggingRadiologi As String = String.Empty
    Public sAlamatTandaTanganDokter As String = String.Empty
    Public sAlamatTandaTanganPerawat As String = String.Empty
    Public sKODETEMPLATE_RINGKASAN As String = String.Empty
    Public sKODETEMPLATE As String = String.Empty
    Public sKODECPPTCOPY As String = String.Empty
    Public sKODEASESMENCOPY As String = String.Empty
    Public sHasilLoadRadiologi As String = String.Empty
    Public sSpesialCMG As String = String.Empty
    Public NAMA As String = String.Empty
    Public TANGGALLAHIR As String = String.Empty
    Public USIA As String = String.Empty
    Public JENISKELAMIN As String = String.Empty
    Public sKDUSER_TTD As String = String.Empty
    Public sWAKTU As String = String.Empty
    Public RM As String = String.Empty
    Public sKONSULTASI As String = String.Empty


    Public sFind1 As String = String.Empty
    Public sFind2 As String = String.Empty
    Public sFind3 As String = String.Empty
    Public sFind4 As String = String.Empty
    Public sFind5 As String = String.Empty
    Public sFind6 As String = String.Empty
    Public sFind7 As String = String.Empty
    Public sFind8 As String = String.Empty
    Public sFind9 As String = String.Empty
    Public sFind10 As String = String.Empty
    Public sFind11 As String = String.Empty
    Public sFind12 As String = String.Empty
    Public sFind13 As String = String.Empty
    Public sFind14 As String = String.Empty
    Public sFind15 As String = String.Empty
    Public sFind16 As String = String.Empty
    Public sFind17 As String = String.Empty
    Public sFind18 As String = String.Empty
    Public sFind19 As String = String.Empty
    Public sFind20 As String = String.Empty

    Public sLAYARPOLI As String = ""
    Public sForm0 As Boolean = False
    Public sASESEMEN_IGD As String = String.Empty
    'Public sNO_USG As String = String.Empty
    'Public sNAMA_USG As String = String.Empty
    'Public sUMUR_USG As String = String.Empty
    'Public sDATE_USG As DateTime = Now
    'Public sKDTARIF_USG As String = String.Empty
    'Public sGEJALA_USG As String = String.Empty
    'Public sDOKTER_USG As String = String.Empty
    'Public sDESCRIPTION_USG As String = String.Empty

    Public sNOMORUSG_RAD As String = String.Empty
    Public sPEMERIKSAAN_RAD As String = String.Empty
    Public sUMUR_RAD As String = String.Empty
    Public sKDCUSTOMER_RAD As String = String.Empty
    Public sNAME_DISPLAY_RAD As String = String.Empty
    Public sDATE_RAD As String = String.Empty
    Public sDEPARTMENT_RAD As String = String.Empty
    Public sREPORTDATE_RAD As String = String.Empty
    Public sEXAMPDESC_RAD As String = String.Empty
    Public sDOKTER_RAD As String = String.Empty
    Public sDIAGNOSA_RAD As String = String.Empty
    Public sDESCRIPTION_RAD As String = String.Empty
    Public sDOKTER_RAD2 As String = String.Empty

    Public sRemarks_Ruangan As String = String.Empty
    Public sRemarks_RencanaPembedahan As String = String.Empty
    Public sRemarks_IntruksiDokter As String = String.Empty
    'Public sKODEBPJS As String = String.Empty
    'Public sCategori As Integer = 0
    'Public sNomorSEPKartu As String = String.Empty
    'Public sKDITEM_PILIH As String = String.Empty
    'Public sKDUOM_PILIH As String = String.Empty
    'Public sQTY_PILIH As Decimal = 0
    'Public sKDSIGNA_PILIH As String = String.Empty
    'Public sKDCARAPAKAI_PILIH As String = String.Empty
    'Public sREMARKS_PILIH As String = String.Empty
    Public sPicture As Image
    'Public sPRB As String = String.Empty
    Public sUserIDTandaTangan As String = String.Empty
    Public sDESCRIPTION As String = String.Empty
    Public sSIMPAN As Boolean = False
    'Public sOrder As String = String.Empty
    Public sRemarks As String = String.Empty
    Public sAktiveVersi2 As Boolean = False
    Public isSave As Boolean = False
    Public sNoTransaksi As String = String.Empty
    Public sDatePemeriksaan As DateTime = Now
    Public sListRincianLab As New List(Of String)
    Public sListRincianRad As New List(Of String)
    'Public sDepartmentAuto As String = String.Empty
    Public AlamatDownloadIamge1 As String = "http://203.210.87.29/img/SIGNATURE/"
    Public DownloadIamge1 As String = String.Empty
    Public sPricePembelian As Decimal = 0
    Public sConnOld As String = String.Empty
    Public sPrintRegister As Boolean = False
    Public sHeaderJudulTracking As String = String.Empty
    Public sUmur As String = String.Empty
    Public sDeletePesan As String = String.Empty
    Public sCetakSEP As Boolean = False
    Public sUserID As String = String.Empty
    Public sCode As String = String.Empty
    'Public sTINDAKLANJUT As String = String.Empty
    Public sKDSERVER As String = String.Empty
    Public sKDDATABASE As String = String.Empty
    Public sKDSERVER_TAX As String = String.Empty
    Public sKDDATABASE_TAX As String = String.Empty
    Public sKDCOMPANY As String = String.Empty
    Public sStatusSave As String = String.Empty
    Public sStatusSaveVonekN As Boolean = False
    Public sCompany As String = String.Empty
    Public sAddress As String = String.Empty
    Public sPhone As String = String.Empty
    Public sNPWP As String = String.Empty
    Public sKDWAREHOUSEAUTO As String = String.Empty
    Public sKDUNITKASIR As String = String.Empty

    Public sRMBPJS As String = String.Empty
    Public sDaftar_L1 As String = String.Empty
    Public sDaftar_L2 As String = String.Empty
    Public sDaftar_L2_1 As String = String.Empty
    Public sDaftar_L2_2 As String = String.Empty
    Public sDaftar_L3 As String = String.Empty
    Public sDaftar_L4 As String = String.Empty
    Public sDaftar_L5 As String = String.Empty
    Public sDaftar_L6 As String = String.Empty
    Public sWATERMARK As String = String.Empty

    Public sItem_L1 As String = String.Empty
    Public sItem_L2 As String = String.Empty
    Public sItem_L3 As String = String.Empty
    Public sItem_L4 As String = String.Empty
    Public sItem_L5 As String = String.Empty
    Public sItem_L6 As String = String.Empty
    Public sItem_L7 As String = String.Empty

    Public listCopy As New List(Of DataAccess.S_REQ_RECIPE_D)
    'Public skodeorderlab As String = String.Empty
    'Public skodeorderRad As String = String.Empty

    Public sNAMA_DIKONSUL As String = String.Empty
    Public sRM_DIKONSUL As String = String.Empty
    Public sRUANG_DIKONSUL As String = String.Empty
    Public sDOKTERDARI_DIKONSUL As String = String.Empty
    Public sDOKTERKEPADA_DIKONSUL As String = String.Empty
    Public sISIKONSUL_DIKONSUL As String = String.Empty
    Public sJAWABKONSUL_DIKONSUL As String = String.Empty

    'Public arrDetailOrder As New List(Of DataAccess.S_REQ_ORDER_PENUNJANG)
    'Public listOrderLab As New List(Of String)
    'Public listOrderRad As New List(Of String)
    'Public sIndikasiMedis As String = String.Empty
#Region "Printing"
    Public Sub pLine_(ByRef oTxtStream As TextWriter, ByVal nSPACE As Integer, ByVal xcCHAR As String)
        Dim cLINE As String

        cLINE = Space(nSPACE)
        cLINE = Replace(cLINE, " ", xcCHAR)
        oTxtStream.WriteLine(cLINE)
    End Sub
    Public Function pSpace_(ByRef cText As String, ByVal nLength As Long, ByVal Alignment As Single) As String
        Dim cResult As String, nTextLength As Long

        nTextLength = Len(cText)
        cResult = cText
        If (nTextLength < nLength) Then
            cResult = cResult & Space(nLength - Len(cResult))
        ElseIf (nTextLength > nLength) Then
            cResult = Mid(cResult, 1, nLength)
            If Len(cResult) < nLength Then
                cResult = cResult & Space(nLength - Len(cResult))
            End If
        End If
        If (nTextLength < nLength) Then
            Select Case Alignment
                Case 1
                    cResult = (Space(nLength - nTextLength) + cText)
                Case 2
                    cResult = (Space((nLength - nTextLength) / 2) + cText)
                Case 3
                    cResult = (cText + Space(nLength - nTextLength))
            End Select
            cResult = cResult + Space(nLength - Len(cResult))
        End If
        pSpace_ = cResult
        pSpace_ = cResult
    End Function
#End Region

#Region "Encrypt / Decrypt"
    Private Const INT_lens As Integer = 1
    Public str As StringBuilder
    Public searchStr As String
    Public b As Integer = 6
    Public p() As Integer = {2, 4, 7, 9, 3, INT_lens}
    Public i As Integer
    Public j As Integer
    Public k As Integer
    Public c As Integer
    Public lens As Integer

    Public Function Encrypt(ByVal inputstr As String) As String
        str = New StringBuilder(inputstr)
        lens = str.Length
        While (lens < b) OrElse (lens Mod b)
            str.Append(" ")
            lens += INT_lens
        End While
        For i As Integer = 0 To ((lens / b) - INT_lens)
            For j As Integer = 0 To (b - INT_lens)
                k = p(j) + 100
                c = (6 * i + j)
                str.Replace(str.Chars(c), Chr(Asc(str.Chars(c)) + k), c, INT_lens)
            Next
        Next
        Return str.ToString
        str = Nothing
    End Function
    Public Function Decrypt(ByVal inputstr As String) As String

        str = New StringBuilder(inputstr)
        lens = str.Length
        While (lens < b) OrElse (lens Mod b)
            str.Append(" ")
            lens += INT_lens
        End While

        For i As Integer = 0 To ((lens / b) - INT_lens)
            For j As Integer = 0 To (b - INT_lens)
                k = p(j) + 100
                c = (6 * i + j)
                str.Replace(str.Chars(c), Chr(Asc(str.Chars(c)) - k), c, INT_lens)
            Next
        Next
        Return str.ToString
        str = Nothing
    End Function
#End Region
End Module
