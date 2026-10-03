Imports System.Text

Module clsGlobals
    Public Enum FORM_MODE
        FORM_MODE_VIEW = 0
        FORM_MODE_ADD = 1
        FORM_MODE_EDIT = 2
    End Enum
    Public Function AutoNumberRM(ByVal sLASTNUMBER As Integer) As String
        AutoNumberRM = sLASTNUMBER.ToString().PadLeft(9, "0")
    End Function
    Public Function AutoNumberAntrianFarmasi(ByVal sKDCOUNTER As String, ByVal sLASTNUMBER As Integer) As String
        AutoNumberAntrianFarmasi = sKDCOUNTER & sLASTNUMBER.ToString().PadLeft(3, "0")
    End Function
    Public Function AutoNumberRegister(ByVal sKDCOUNTER As String, ByVal sLASTNUMBER As Integer, ByVal sDATE As DateTime) As String

        If sLASTNUMBER < 10 Then
            AutoNumberRegister = sKDCOUNTER & sDATE.ToString("yyyyMMdd") & "0000" & sLASTNUMBER.ToString
        ElseIf sLASTNUMBER < 100 Then
            AutoNumberRegister = sKDCOUNTER & sDATE.ToString("yyyyMMdd") & "000" & sLASTNUMBER.ToString
        ElseIf sLASTNUMBER < 1000 Then
            AutoNumberRegister = sKDCOUNTER & sDATE.ToString("yyyyMMdd") & "00" & sLASTNUMBER.ToString
        ElseIf sLASTNUMBER < 10000 Then
            AutoNumberRegister = sKDCOUNTER & sDATE.ToString("yyyyMMdd") & "0" & sLASTNUMBER.ToString
        Else
            AutoNumberRegister = sKDCOUNTER & sDATE.ToString("yyyyMMdd") & sLASTNUMBER.ToString
        End If
    End Function
    Public Function AutoNumberSuHat(ByVal sKDCOUNTER As String, ByVal sLASTNUMBER As Integer, ByVal sDATE As DateTime) As String
        Dim sMonth As String = String.Empty

        Select Case Month(sDATE)
            Case 1
                sMonth = "I"
            Case 2
                sMonth = "II"
            Case 3
                sMonth = "III"
            Case 4
                sMonth = "IV"
            Case 5
                sMonth = "V"
            Case 6
                sMonth = "VI"
            Case 7
                sMonth = "VII"
            Case 8
                sMonth = "VIII"
            Case 9
                sMonth = "IX"
            Case 10
                sMonth = "X"
            Case 11
                sMonth = "XI"
            Case 12
                sMonth = "XII"
        End Select

        AutoNumberSuHat = sLASTNUMBER.ToString.PadLeft(3, "0") & "/SKS/RSKC/" & sMonth & "/" & sDATE.ToString("yyyy")
    End Function
    Public Function AutoNumber(ByVal sKDCOUNTER As String, ByVal sLASTNUMBER As Integer, ByVal sDATE As DateTime) As String
        Dim sMonth As String = String.Empty

        Select Case Month(sDATE)
            Case 1
                sMonth = "A"
            Case 2
                sMonth = "B"
            Case 3
                sMonth = "C"
            Case 4
                sMonth = "D"
            Case 5
                sMonth = "E"
            Case 6
                sMonth = "F"
            Case 7
                sMonth = "G"
            Case 8
                sMonth = "H"
            Case 9
                sMonth = "I"
            Case 10
                sMonth = "J"
            Case 11
                sMonth = "K"
            Case 12
                sMonth = "L"
        End Select

        If sLASTNUMBER < 10 Then
            AutoNumber = sKDCOUNTER & Year(sDATE) & sMonth & "00000" & sLASTNUMBER.ToString
        ElseIf sLASTNUMBER < 100 Then
            AutoNumber = sKDCOUNTER & Year(sDATE) & sMonth & "0000" & sLASTNUMBER.ToString
        ElseIf sLASTNUMBER < 1000 Then
            AutoNumber = sKDCOUNTER & Year(sDATE) & sMonth & "000" & sLASTNUMBER.ToString
        ElseIf sLASTNUMBER < 10000 Then
            AutoNumber = sKDCOUNTER & Year(sDATE) & sMonth & "00" & sLASTNUMBER.ToString
        ElseIf sLASTNUMBER < 100000 Then
            AutoNumber = sKDCOUNTER & Year(sDATE) & sMonth & "0" & sLASTNUMBER.ToString
        Else
            AutoNumber = sKDCOUNTER & Year(sDATE) & sMonth & sLASTNUMBER.ToString
        End If
    End Function
    Public Function AutoNumberSIMRS(ByVal sLASTNUMBER As Integer, ByVal sDATE As DateTime) As String
        Dim sMonth As String = String.Empty

        Select Case Month(sDATE)
            Case 1
                sMonth = "01"
            Case 2
                sMonth = "02"
            Case 3
                sMonth = "03"
            Case 4
                sMonth = "04"
            Case 5
                sMonth = "05"
            Case 6
                sMonth = "06"
            Case 7
                sMonth = "07"
            Case 8
                sMonth = "08"
            Case 9
                sMonth = "09"
            Case 10
                sMonth = "10"
            Case 11
                sMonth = "11"
            Case 12
                sMonth = "12"
        End Select

        AutoNumberSIMRS = Microsoft.VisualBasic.Right(Year(sDATE), 2) & sMonth & "-" & sLASTNUMBER.ToString.PadLeft(5, "0")

    End Function
    Public Function AutoNumberCodeBarcode(ByVal LastNumber As Integer) As String
        AutoNumberCodeBarcode = "8" & Year(Now) & Month(Now).ToString().PadLeft(2, "0") & Day(Now).ToString().PadLeft(2, "0") & LastNumber.ToString().PadLeft(5, "0")
    End Function

    Public Function AutoNumberCode(ByVal LastNumber As Integer) As String
        If LastNumber < 10 Then
            AutoNumberCode = "000000000" & LastNumber.ToString
        ElseIf LastNumber < 100 Then
            AutoNumberCode = "00000000" & LastNumber.ToString
        ElseIf LastNumber < 1000 Then
            AutoNumberCode = "0000000" & LastNumber.ToString
        ElseIf LastNumber < 10000 Then
            AutoNumberCode = "000000" & LastNumber.ToString
        ElseIf LastNumber < 100000 Then
            AutoNumberCode = "00000" & LastNumber.ToString
        ElseIf LastNumber < 1000000 Then
            AutoNumberCode = "0000" & LastNumber.ToString
        ElseIf LastNumber < 10000000 Then
            AutoNumberCode = "000" & LastNumber.ToString
        ElseIf LastNumber < 100000000 Then
            AutoNumberCode = "00" & LastNumber.ToString
        ElseIf LastNumber < 100000000 Then
            AutoNumberCode = "0" & LastNumber.ToString
        Else
            AutoNumberCode = LastNumber.ToString
        End If
    End Function

    Public Function AutoNumberVoucher(ByVal sKDCOUNTER As String, ByVal sLASTNUMBER As Integer, ByVal sDATE As DateTime) As String
        Dim sMonth As String = String.Empty

        Select Case Month(sDATE)
            Case 1
                sMonth = "01"
            Case 2
                sMonth = "02"
            Case 3
                sMonth = "03"
            Case 4
                sMonth = "04"
            Case 5
                sMonth = "05"
            Case 6
                sMonth = "06"
            Case 7
                sMonth = "07"
            Case 8
                sMonth = "08"
            Case 9
                sMonth = "09"
            Case 10
                sMonth = "10"
            Case 11
                sMonth = "11"
            Case 12
                sMonth = "12"
        End Select

        If sLASTNUMBER < 10 Then
            AutoNumberVoucher = Year(sDATE) & sMonth & Day(sDATE) & "00000" & sLASTNUMBER.ToString
        ElseIf sLASTNUMBER < 100 Then
            AutoNumberVoucher = Year(sDATE) & sMonth & Day(sDATE) & "0000" & sLASTNUMBER.ToString
        ElseIf sLASTNUMBER < 1000 Then
            AutoNumberVoucher = Year(sDATE) & sMonth & Day(sDATE) & "000" & sLASTNUMBER.ToString
        ElseIf sLASTNUMBER < 10000 Then
            AutoNumberVoucher = Year(sDATE) & sMonth & Day(sDATE) & "00" & sLASTNUMBER.ToString
        ElseIf sLASTNUMBER < 100000 Then
            AutoNumberVoucher = Year(sDATE) & sMonth & Day(sDATE) & "0" & sLASTNUMBER.ToString
        Else
            AutoNumberVoucher = Year(sDATE) & sMonth & Day(sDATE) & sLASTNUMBER.ToString
        End If
    End Function

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

