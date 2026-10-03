Imports System.Security.Cryptography
Imports System.Text
Imports System.IO
Imports LZStringVBNet
Imports iTextSharp.text
Imports iTextSharp.text.pdf

Namespace Brigging
    Public Class clsSetKoneksi
        Public oConnection As Setting.clsConnectionMain = Nothing
        Public oError As Setting.clsError = Nothing
        Public sMODUL As String = ""
        Public sREFERENCE As String = ""
        Public sSTATUS As String = ""
        Public sLASTNUMBER As Integer = 0
        Private oBPJSKoneksi As New Setting.clsBPJSKoneksi

        Public Sub New(Optional ByVal sConnection As String = "")
            If sConnection = "" Then
                oConnection = New Setting.clsConnectionMain
                oError = New Setting.clsError
            Else
                oConnection = New Setting.clsConnectionMain("TAX")
                oError = New Setting.clsError("TAX")
            End If

            sMODUL = "SET_KONEKSI"
        End Sub
        Protected Overrides Sub Finalize()
            MyBase.Finalize()
            oConnection = Nothing
            GC.SuppressFinalize(Me)
        End Sub
        Public Function EncryptBPJS2(ByVal consid As String, ByVal secretkey As String, ByVal kodefaskes As String, ByVal data As String) As String

            ' Gabungkan key
            'CONSID & "&" & uTime
            Dim key As String = consid & secretkey & kodefaskes

            Dim encData As String = Nothing

            Try
                ' Dapatkan hash keys
                Dim keys As Byte()() = GetHashKeys(key)

                ' Enkripsi data
                encData = EncryptStringToBytes_Aes2(data, keys(0), keys(1))

            Catch ex As CryptographicException
                ' Log error jika diperlukan
                encData = String.Empty
            Catch ex As ArgumentNullException
                ' Log error jika diperlukan
                encData = String.Empty
            End Try

            Return encData
        End Function
        Private Function EncryptStringToBytes_Aes2(ByVal plainText As String, ByVal Key As Byte(), ByVal IV As Byte()) As String
            If plainText Is Nothing OrElse plainText.Length <= 0 Then
                Throw New ArgumentNullException("plainText")
            End If
            If Key Is Nothing OrElse Key.Length <= 0 Then
                Throw New ArgumentNullException("Key")
            End If
            If IV Is Nothing OrElse IV.Length <= 0 Then
                Throw New ArgumentNullException("IV")
            End If

            Dim encrypted As Byte() = Nothing

            Using aesAlg As Aes = Aes.Create()
                aesAlg.Key = Key
                aesAlg.IV = IV
                aesAlg.Mode = CipherMode.CBC
                aesAlg.Padding = PaddingMode.PKCS7

                Dim encryptor As ICryptoTransform = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV)

                Using msEncrypt As New MemoryStream()
                    Using csEncrypt As New CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write)
                        Using swEncrypt As New StreamWriter(csEncrypt)
                            swEncrypt.Write(plainText)
                        End Using
                        encrypted = msEncrypt.ToArray()
                    End Using
                End Using
            End Using

            Return Convert.ToBase64String(encrypted)
        End Function
        Public Function InsertMedicalRecord(ByVal ALAMATWEB As String, ByVal CONSID As String, ByVal SECREATKEY As String, ByVal USERKEY As String, ByVal uTime As Integer, ByVal Request As String) As String
            Try
                If Not oConnection.GetConnection() Then
                    InsertMedicalRecord = ""
                    Exit Function
                End If

                Dim HasilKey As String = ""

                ' Initialize the keyed hash object using the secret key as the key
                Dim hashObject As New HMACSHA256(Encoding.UTF8.GetBytes(SECREATKEY))
                ' Computes the signature by hashing the salt with the secret key as the key
                Dim signature = hashObject.ComputeHash(Encoding.UTF8.GetBytes(CONSID & "&" & uTime))
                ' Base 64 Encode
                HasilKey = Convert.ToBase64String(signature)

                Dim req As WinHttp.WinHttpRequest

                Dim Url As String = ALAMATWEB & "eclaim/rekammedis/insert"

                req = New WinHttp.WinHttpRequest
                req.Open("POST", Url, False)
                req.SetRequestHeader("X-cons-ID", CONSID)
                req.SetRequestHeader("X-timestamp", uTime)
                req.SetRequestHeader("X-signature", HasilKey)
                req.SetRequestHeader("user_key", USERKEY)
                req.SetRequestHeader("Content-Type", "text/plain")
                'req.SetRequestHeader("Content-Type", "application/json; charset=utf-8")
                'req.SetRequestHeader("Content-Type", "application/json")

                req.Send(Request)

                InsertMedicalRecord = req.ResponseText

                If InsertMedicalRecord = "" Then
                    InsertMedicalRecord = req.Status & " " & req.StatusText
                End If
            Catch ex As Exception
                InsertMedicalRecord = ex.ToString

                Throw ex
            End Try
        End Function

        'Public Function GetStructureHeader() As SET_KONEKSI
        '    If Not oConnection.GetConnection() Then
        '        GetStructureHeader = Nothing
        '    End If
        '    GetStructureHeader = New SET_KONEKSI
        'End Function
        'Public Function GetData() As List(Of SET_KONEKSI)
        '    If Not oConnection.GetConnection() Then
        '        GetData = Nothing
        '        Exit Function
        '    End If
        '    GetData = oConnection.db.SET_KONEKSIs.OrderBy(Function(x) x.KDKONEKSI).ToList()
        'End Function
        'Public Function GetData(ByVal sKDKONEKSI As String) As SET_KONEKSI
        '    If Not oConnection.GetConnection() Then
        '        GetData = Nothing
        '        Exit Function
        '    End If
        '    GetData = oConnection.db.SET_KONEKSIs.FirstOrDefault(Function(x) x.KDKONEKSI = sKDKONEKSI)
        'End Function
        'Public Function GetDataAktive() As SET_KONEKSI
        '    If Not oConnection.GetConnection() Then
        '        GetDataAktive = Nothing
        '        Exit Function
        '    End If
        '    GetDataAktive = oConnection.db.SET_KONEKSIs.FirstOrDefault(Function(x) x.NAME_DISPLAY = "VCLAIM" And x.ISACTIVE = True)
        'End Function
        'Public Function GetDataAktive2() As SET_KONEKSI
        '    If Not oConnection.GetConnection() Then
        '        GetDataAktive2 = Nothing
        '        Exit Function
        '    End If
        '    GetDataAktive2 = oConnection.db.SET_KONEKSIs.FirstOrDefault(Function(x) x.NAME_DISPLAY = "APLICARE" And x.ISACTIVE = True)
        'End Function
        'Public Function IsExist(ByVal sNAME_DISPLAY As String) As Boolean
        '    If Not oConnection.GetConnection() Then
        '        IsExist = False
        '        Exit Function
        '    End If

        '    Dim ds = oConnection.db.SET_KONEKSIs.FirstOrDefault(Function(x) x.NAME_DISPLAY = sNAME_DISPLAY)

        '    If ds IsNot Nothing Then
        '        IsExist = True
        '    Else
        '        IsExist = False
        '    End If
        'End Function
#Region "DECRYPTE"
        Public Function Decrypt(ByVal data As String, ByVal key As String) As String
            Dim decData As String = Nothing
            Dim keys As Byte()() = GetHashKeys(key)

            Try
                decData = LZString.DecompressFromEncodedUriComponent(DecryptStringFromBytes_Aes(data, keys(0), keys(1)))
            Catch oErr As Exception
                'MsgBox("Load Petugas Data : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            End Try
            Return decData
        End Function
        Private Shared Function DecryptStringFromBytes_Aes(ByVal cipherTextString As String, ByVal Key As Byte(), ByVal IV As Byte()) As String
            Dim cipherText As Byte() = Convert.FromBase64String(cipherTextString)
            If cipherText Is Nothing OrElse cipherText.Length <= 0 Then Throw New ArgumentNullException("cipherText")
            If Key Is Nothing OrElse Key.Length <= 0 Then Throw New ArgumentNullException("Key")
            If IV Is Nothing OrElse IV.Length <= 0 Then Throw New ArgumentNullException("IV")
            Dim plaintext As String = Nothing

            Using aesAlg As Aes = Aes.Create()
                aesAlg.Key = Key
                aesAlg.IV = IV
                Dim decryptor As ICryptoTransform = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV)

                Using msDecrypt As MemoryStream = New MemoryStream(cipherText)

                    Using csDecrypt As CryptoStream = New CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read)

                        Using srDecrypt As StreamReader = New StreamReader(csDecrypt)
                            plaintext = srDecrypt.ReadToEnd()
                        End Using
                    End Using
                End Using
            End Using

            Return plaintext
        End Function
        Private Function GetHashKeys(ByVal key As String) As Byte()()
            Dim result As Byte()() = New Byte(1)() {}
            Dim enc As Encoding = Encoding.UTF8
            Dim sha2 As SHA256 = New SHA256CryptoServiceProvider()
            Dim rawKey As Byte() = enc.GetBytes(key)
            Dim rawIV As Byte() = enc.GetBytes(key)
            Dim hashKey As Byte() = sha2.ComputeHash(rawKey)
            Dim hashIV As Byte() = sha2.ComputeHash(rawIV)
            Array.Resize(hashIV, 16)
            result(0) = hashKey
            result(1) = hashIV
            Return result
        End Function
#End Region
#Region "VCLAIM"
#Region "Referensi"
        Public Function GetDataVClaimReferensiDiagnosa(ByVal ALAMATWEB As String, ByVal CONSID As String, ByVal SECREATKEY As String, ByVal USERKEY As String, ByVal uTime As Integer, ByVal Parameter As String) As String
            Try
                If Not oConnection.GetConnection() Then
                    GetDataVClaimReferensiDiagnosa = ""
                    Exit Function
                End If

                'Dim ds = oConnection.db.SET_KONEKSIs.FirstOrDefault(Function(x) x.NAME_DISPLAY = sNAME_DISPLAY And x.ISACTIVE = True)

                Dim HasilKey As String = ""
                Dim result As String = ""

                ' Initialize the keyed hash object using the secret key as the key
                Dim hashObject As New HMACSHA256(Encoding.UTF8.GetBytes(SECREATKEY))
                ' Computes the signature by hashing the salt with the secret key as the key
                Dim signature = hashObject.ComputeHash(Encoding.UTF8.GetBytes(CONSID & "&" & uTime))
                ' Base 64 Encode
                HasilKey = Convert.ToBase64String(signature)

                Dim req As WinHttp.WinHttpRequest

                Dim Url As String = ALAMATWEB & "referensi/diagnosa/" & Parameter & ""

                req = New WinHttp.WinHttpRequest
                req.Open("GET", Url, False)
                req.SetRequestHeader("X-cons-ID", CONSID)
                req.SetRequestHeader("X-timestamp", uTime)
                req.SetRequestHeader("X-signature", HasilKey)
                req.SetRequestHeader("user_key", USERKEY)
                req.SetRequestHeader("Content-Type", "application/json; charset=utf-8")

                req.Send()

                result = req.ResponseText

                GetDataVClaimReferensiDiagnosa = result

            Catch ex As Exception
                GetDataVClaimReferensiDiagnosa = ex.ToString
                oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                Throw ex
            End Try
        End Function
        Public Function GetDataVClaimReferensiPoli(ByVal ALAMATWEB As String, ByVal CONSID As String, ByVal SECREATKEY As String, ByVal USERKEY As String, ByVal uTime As Integer, ByVal Parameter As String) As String
            Try
                If Not oConnection.GetConnection() Then
                    GetDataVClaimReferensiPoli = ""
                    Exit Function
                End If

                Dim HasilKey As String = ""
                Dim result As String = ""

                ' Initialize the keyed hash object using the secret key as the key
                Dim hashObject As New HMACSHA256(Encoding.UTF8.GetBytes(SECREATKEY))
                ' Computes the signature by hashing the salt with the secret key as the key
                Dim signature = hashObject.ComputeHash(Encoding.UTF8.GetBytes(CONSID & "&" & uTime))
                ' Base 64 Encode
                HasilKey = Convert.ToBase64String(signature)

                Dim req As WinHttp.WinHttpRequest

                Dim Url As String = ALAMATWEB & "referensi/poli/" & Parameter

                req = New WinHttp.WinHttpRequest
                req.Open("GET", Url, False)
                req.SetRequestHeader("X-cons-ID", CONSID)
                req.SetRequestHeader("X-timestamp", uTime)
                req.SetRequestHeader("X-signature", HasilKey)
                req.SetRequestHeader("user_key", USERKEY)
                req.SetRequestHeader("Content-Type", "application/json; charset=utf-8")

                req.Send()

                result = req.ResponseText

                GetDataVClaimReferensiPoli = result

            Catch ex As Exception
                GetDataVClaimReferensiPoli = ex.ToString
                oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                Throw ex
            End Try
        End Function
        Public Function GetDataVClaimReferensiFasilitasKesehatan(ByVal ALAMATWEB As String, ByVal CONSID As String, ByVal SECREATKEY As String, ByVal USERKEY As String, ByVal uTime As Integer, ByVal Parameter1 As String, ByVal Parameter2 As String) As String
            Try
                If Not oConnection.GetConnection() Then
                    GetDataVClaimReferensiFasilitasKesehatan = ""
                    Exit Function
                End If

                Dim HasilKey As String = ""
                Dim result As String = ""

                ' Initialize the keyed hash object using the secret key as the key
                Dim hashObject As New HMACSHA256(Encoding.UTF8.GetBytes(SECREATKEY))
                ' Computes the signature by hashing the salt with the secret key as the key
                Dim signature = hashObject.ComputeHash(Encoding.UTF8.GetBytes(CONSID & "&" & uTime))
                ' Base 64 Encode
                HasilKey = Convert.ToBase64String(signature)

                Dim req As WinHttp.WinHttpRequest

                Dim Url As String = ALAMATWEB & "referensi/faskes/" & Parameter1 & "/" & Parameter2

                req = New WinHttp.WinHttpRequest
                req.Open("GET", Url, False)
                req.SetRequestHeader("X-cons-ID", CONSID)
                req.SetRequestHeader("X-timestamp", uTime)
                req.SetRequestHeader("X-signature", HasilKey)
                req.SetRequestHeader("user_key", USERKEY)
                req.SetRequestHeader("Content-Type", "application/json; charset=utf-8")

                req.Send()

                result = req.ResponseText

                GetDataVClaimReferensiFasilitasKesehatan = result

            Catch ex As Exception
                GetDataVClaimReferensiFasilitasKesehatan = ex.ToString
                oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                Throw ex
            End Try
        End Function
        Public Function GetDataVClaimReferensiDokterDPJP(ByVal ALAMATWEB As String, ByVal CONSID As String, ByVal SECREATKEY As String, ByVal USERKEY As String, ByVal uTime As Integer, ByVal Parameter1 As String, ByVal Parameter2 As String, ByVal Parameter3 As String) As String
            Try
                If Not oConnection.GetConnection() Then
                    GetDataVClaimReferensiDokterDPJP = ""
                    Exit Function
                End If

                Dim HasilKey As String = ""
                Dim result As String = ""

                ' Initialize the keyed hash object using the secret key as the key
                Dim hashObject As New HMACSHA256(Encoding.UTF8.GetBytes(SECREATKEY))
                ' Computes the signature by hashing the salt with the secret key as the key
                Dim signature = hashObject.ComputeHash(Encoding.UTF8.GetBytes(CONSID & "&" & uTime))
                ' Base 64 Encode
                HasilKey = Convert.ToBase64String(signature)

                Dim req As WinHttp.WinHttpRequest

                Dim Url As String = ALAMATWEB & "referensi/dokter/pelayanan/" & Parameter1 & "/tglPelayanan/" & Parameter2 & "/Spesialis/" & Parameter3

                req = New WinHttp.WinHttpRequest
                req.Open("GET", Url, False)
                req.SetRequestHeader("X-cons-ID", CONSID)
                req.SetRequestHeader("X-timestamp", uTime)
                req.SetRequestHeader("X-signature", HasilKey)
                req.SetRequestHeader("user_key", USERKEY)
                req.SetRequestHeader("Content-Type", "application/json; charset=utf-8")

                req.Send()

                result = req.ResponseText

                GetDataVClaimReferensiDokterDPJP = result

            Catch ex As Exception
                GetDataVClaimReferensiDokterDPJP = ex.ToString
                oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                Throw ex
            End Try

        End Function
        Public Function GetDataVClaimReferensiPropinsi(ByVal ALAMATWEB As String, ByVal CONSID As String, ByVal SECREATKEY As String, ByVal USERKEY As String, ByVal uTime As Integer) As String
            Try
                If Not oConnection.GetConnection() Then
                    GetDataVClaimReferensiPropinsi = ""
                    Exit Function
                End If

                Dim HasilKey As String = ""
                Dim result As String = ""

                ' Initialize the keyed hash object using the secret key as the key
                Dim hashObject As New HMACSHA256(Encoding.UTF8.GetBytes(SECREATKEY))
                ' Computes the signature by hashing the salt with the secret key as the key
                Dim signature = hashObject.ComputeHash(Encoding.UTF8.GetBytes(CONSID & "&" & uTime))
                ' Base 64 Encode
                HasilKey = Convert.ToBase64String(signature)

                Dim req As WinHttp.WinHttpRequest

                Dim Url As String = ALAMATWEB & "referensi/propinsi"

                req = New WinHttp.WinHttpRequest
                req.Open("GET", Url, False)
                req.SetRequestHeader("X-cons-ID", CONSID)
                req.SetRequestHeader("X-timestamp", uTime)
                req.SetRequestHeader("X-signature", HasilKey)
                req.SetRequestHeader("user_key", USERKEY)
                req.SetRequestHeader("Content-Type", "application/json; charset=utf-8")

                req.Send()

                result = req.ResponseText

                GetDataVClaimReferensiPropinsi = result

            Catch ex As Exception
                GetDataVClaimReferensiPropinsi = ex.ToString
                oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                Throw ex
            End Try
        End Function
        Public Function GetDataVClaimReferensiKabupaten(ByVal ALAMATWEB As String, ByVal CONSID As String, ByVal SECREATKEY As String, ByVal USERKEY As String, ByVal uTime As Integer, ByVal Parameter As String) As String
            Try
                If Not oConnection.GetConnection() Then
                    GetDataVClaimReferensiKabupaten = ""
                    Exit Function
                End If

                Dim HasilKey As String = ""
                Dim result As String = ""

                ' Initialize the keyed hash object using the secret key as the key
                Dim hashObject As New HMACSHA256(Encoding.UTF8.GetBytes(SECREATKEY))
                ' Computes the signature by hashing the salt with the secret key as the key
                Dim signature = hashObject.ComputeHash(Encoding.UTF8.GetBytes(CONSID & "&" & uTime))
                ' Base 64 Encode
                HasilKey = Convert.ToBase64String(signature)

                Dim req As WinHttp.WinHttpRequest

                Dim Url As String = ALAMATWEB & "referensi/kabupaten/propinsi/" & Parameter

                req = New WinHttp.WinHttpRequest
                req.Open("GET", Url, False)
                req.SetRequestHeader("X-cons-ID", CONSID)
                req.SetRequestHeader("X-timestamp", uTime)
                req.SetRequestHeader("X-signature", HasilKey)
                req.SetRequestHeader("user_key", USERKEY)
                req.SetRequestHeader("Content-Type", "application/json; charset=utf-8")

                req.Send()

                result = req.ResponseText

                GetDataVClaimReferensiKabupaten = result

            Catch ex As Exception
                GetDataVClaimReferensiKabupaten = ex.ToString
                oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                Throw ex
            End Try
        End Function
        Public Function GetDataVClaimReferensiKecamatan(ByVal ALAMATWEB As String, ByVal CONSID As String, ByVal SECREATKEY As String, ByVal USERKEY As String, ByVal uTime As Integer, ByVal Parameter As String) As String
            Try
                If Not oConnection.GetConnection() Then
                    GetDataVClaimReferensiKecamatan = ""
                    Exit Function
                End If

                Dim HasilKey As String = ""
                Dim result As String = ""

                ' Initialize the keyed hash object using the secret key as the key
                Dim hashObject As New HMACSHA256(Encoding.UTF8.GetBytes(SECREATKEY))
                ' Computes the signature by hashing the salt with the secret key as the key
                Dim signature = hashObject.ComputeHash(Encoding.UTF8.GetBytes(CONSID & "&" & uTime))
                ' Base 64 Encode
                HasilKey = Convert.ToBase64String(signature)

                Dim req As WinHttp.WinHttpRequest

                Dim Url As String = ALAMATWEB & "referensi/kecamatan/kabupaten/" & Parameter

                req = New WinHttp.WinHttpRequest
                req.Open("GET", Url, False)
                req.SetRequestHeader("X-cons-ID", CONSID)
                req.SetRequestHeader("X-timestamp", uTime)
                req.SetRequestHeader("X-signature", HasilKey)
                req.SetRequestHeader("user_key", USERKEY)
                req.SetRequestHeader("Content-Type", "application/json; charset=utf-8")

                req.Send()

                result = req.ResponseText

                GetDataVClaimReferensiKecamatan = result

            Catch ex As Exception
                GetDataVClaimReferensiKecamatan = ex.ToString
                oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                Throw ex
            End Try
        End Function
        Public Function GetDataVClaimReferensiDiagnosa_PRB(ByVal ALAMATWEB As String, ByVal CONSID As String, ByVal SECREATKEY As String, ByVal USERKEY As String, ByVal uTime As Integer) As String
            Try
                If Not oConnection.GetConnection() Then
                    GetDataVClaimReferensiDiagnosa_PRB = ""
                    Exit Function
                End If

                'Dim ds = oConnection.db.SET_KONEKSIs.FirstOrDefault(Function(x) x.NAME_DISPLAY = sNAME_DISPLAY And x.ISACTIVE = True)

                Dim HasilKey As String = ""
                Dim result As String = ""

                ' Initialize the keyed hash object using the secret key as the key
                Dim hashObject As New HMACSHA256(Encoding.UTF8.GetBytes(SECREATKEY))
                ' Computes the signature by hashing the salt with the secret key as the key
                Dim signature = hashObject.ComputeHash(Encoding.UTF8.GetBytes(CONSID & "&" & uTime))
                ' Base 64 Encode
                HasilKey = Convert.ToBase64String(signature)

                Dim req As WinHttp.WinHttpRequest

                Dim Url As String = ALAMATWEB & "referensi/diagnosaprb"

                req = New WinHttp.WinHttpRequest
                req.Open("GET", Url, False)
                req.SetRequestHeader("X-cons-ID", CONSID)
                req.SetRequestHeader("X-timestamp", uTime)
                req.SetRequestHeader("X-signature", HasilKey)
                req.SetRequestHeader("user_key", USERKEY)
                req.SetRequestHeader("Content-Type", "application/json; charset=utf-8")

                req.Send()

                result = req.ResponseText

                GetDataVClaimReferensiDiagnosa_PRB = result

            Catch ex As Exception
                GetDataVClaimReferensiDiagnosa_PRB = ex.ToString
                oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                Throw ex
            End Try
        End Function
        Public Function GetDataVClaimReferensiObatGenerikProgramPRB(ByVal ALAMATWEB As String, ByVal CONSID As String, ByVal SECREATKEY As String, ByVal USERKEY As String, ByVal uTime As Integer, ByVal Parameter As String) As String
            Try
                If Not oConnection.GetConnection() Then
                    GetDataVClaimReferensiObatGenerikProgramPRB = ""
                    Exit Function
                End If

                'Dim ds = oConnection.db.SET_KONEKSIs.FirstOrDefault(Function(x) x.NAME_DISPLAY = sNAME_DISPLAY And x.ISACTIVE = True)

                Dim HasilKey As String = ""
                Dim result As String = ""

                ' Initialize the keyed hash object using the secret key as the key
                Dim hashObject As New HMACSHA256(Encoding.UTF8.GetBytes(SECREATKEY))
                ' Computes the signature by hashing the salt with the secret key as the key
                Dim signature = hashObject.ComputeHash(Encoding.UTF8.GetBytes(CONSID & "&" & uTime))
                ' Base 64 Encode
                HasilKey = Convert.ToBase64String(signature)

                Dim req As WinHttp.WinHttpRequest

                Dim Url As String = ALAMATWEB & "referensi/obatprb/" & Parameter

                req = New WinHttp.WinHttpRequest
                req.Open("GET", Url, False)
                req.SetRequestHeader("X-cons-ID", CONSID)
                req.SetRequestHeader("X-timestamp", uTime)
                req.SetRequestHeader("X-signature", HasilKey)
                req.SetRequestHeader("user_key", USERKEY)
                req.SetRequestHeader("Content-Type", "application/json; charset=utf-8")

                req.Send()

                result = req.ResponseText

                GetDataVClaimReferensiObatGenerikProgramPRB = result

            Catch ex As Exception
                GetDataVClaimReferensiObatGenerikProgramPRB = ex.ToString
                oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                Throw ex
            End Try
        End Function
        Public Function GetDataVClaimReferensiProcedure(ByVal ALAMATWEB As String, ByVal CONSID As String, ByVal SECREATKEY As String, ByVal USERKEY As String, ByVal uTime As Integer, ByVal Parameter As String) As String
            Try
                If Not oConnection.GetConnection() Then
                    GetDataVClaimReferensiProcedure = ""
                    Exit Function
                End If

                Dim HasilKey As String = ""
                Dim result As String = ""

                ' Initialize the keyed hash object using the secret key as the key
                Dim hashObject As New HMACSHA256(Encoding.UTF8.GetBytes(SECREATKEY))
                ' Computes the signature by hashing the salt with the secret key as the key
                Dim signature = hashObject.ComputeHash(Encoding.UTF8.GetBytes(CONSID & "&" & uTime))
                ' Base 64 Encode
                HasilKey = Convert.ToBase64String(signature)

                Dim req As WinHttp.WinHttpRequest

                Dim Url As String = ALAMATWEB & "referensi/procedure/" & Parameter

                req = New WinHttp.WinHttpRequest
                req.Open("GET", Url, False)
                req.SetRequestHeader("X-cons-ID", CONSID)
                req.SetRequestHeader("X-timestamp", uTime)
                req.SetRequestHeader("X-signature", HasilKey)
                req.SetRequestHeader("user_key", USERKEY)
                req.SetRequestHeader("Content-Type", "application/json; charset=utf-8")

                req.Send()

                result = req.ResponseText

                GetDataVClaimReferensiProcedure = result

            Catch ex As Exception
                GetDataVClaimReferensiProcedure = ex.ToString
                oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                Throw ex
            End Try
        End Function
        Public Function GetDataVClaimReferensiKelasRawat(ByVal ALAMATWEB As String, ByVal CONSID As String, ByVal SECREATKEY As String, ByVal USERKEY As String, ByVal uTime As Integer) As String
            Try
                If Not oConnection.GetConnection() Then
                    GetDataVClaimReferensiKelasRawat = ""
                    Exit Function
                End If

                Dim HasilKey As String = ""
                Dim result As String = ""

                ' Initialize the keyed hash object using the secret key as the key
                Dim hashObject As New HMACSHA256(Encoding.UTF8.GetBytes(SECREATKEY))
                ' Computes the signature by hashing the salt with the secret key as the key
                Dim signature = hashObject.ComputeHash(Encoding.UTF8.GetBytes(CONSID & "&" & uTime))
                ' Base 64 Encode
                HasilKey = Convert.ToBase64String(signature)

                Dim req As WinHttp.WinHttpRequest

                Dim Url As String = ALAMATWEB & "referensi/kelasrawat"

                req = New WinHttp.WinHttpRequest
                req.Open("GET", Url, False)
                req.SetRequestHeader("X-cons-ID", CONSID)
                req.SetRequestHeader("X-timestamp", uTime)
                req.SetRequestHeader("X-signature", HasilKey)
                req.SetRequestHeader("user_key", USERKEY)
                req.SetRequestHeader("Content-Type", "application/json; charset=utf-8")

                req.Send()

                result = req.ResponseText

                GetDataVClaimReferensiKelasRawat = result

            Catch ex As Exception
                GetDataVClaimReferensiKelasRawat = ex.ToString
                oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                Throw ex
            End Try
        End Function
        Public Function GetDataVClaimReferensiDokter(ByVal ALAMATWEB As String, ByVal CONSID As String, ByVal SECREATKEY As String, ByVal USERKEY As String, ByVal uTime As Integer, ByVal Parameter As String) As String
            Try
                If Not oConnection.GetConnection() Then
                    GetDataVClaimReferensiDokter = ""
                    Exit Function
                End If

                Dim HasilKey As String = ""
                Dim result As String = ""

                ' Initialize the keyed hash object using the secret key as the key
                Dim hashObject As New HMACSHA256(Encoding.UTF8.GetBytes(SECREATKEY))
                ' Computes the signature by hashing the salt with the secret key as the key
                Dim signature = hashObject.ComputeHash(Encoding.UTF8.GetBytes(CONSID & "&" & uTime))
                ' Base 64 Encode
                HasilKey = Convert.ToBase64String(signature)

                Dim req As WinHttp.WinHttpRequest

                Dim Url As String = ALAMATWEB & "referensi/dokter/" & Parameter

                req = New WinHttp.WinHttpRequest
                req.Open("GET", Url, False)
                req.SetRequestHeader("X-cons-ID", CONSID)
                req.SetRequestHeader("X-timestamp", uTime)
                req.SetRequestHeader("X-signature", HasilKey)
                req.SetRequestHeader("user_key", USERKEY)
                req.SetRequestHeader("Content-Type", "application/json; charset=utf-8")

                req.Send()

                result = req.ResponseText

                GetDataVClaimReferensiDokter = result

            Catch ex As Exception
                GetDataVClaimReferensiDokter = ex.ToString
                oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                Throw ex
            End Try
        End Function
        Public Function GetDataVClaimReferensiSpesialistik(ByVal ALAMATWEB As String, ByVal CONSID As String, ByVal SECREATKEY As String, ByVal USERKEY As String, ByVal uTime As Integer) As String
            Try
                If Not oConnection.GetConnection() Then
                    GetDataVClaimReferensiSpesialistik = ""
                    Exit Function
                End If

                Dim HasilKey As String = ""
                Dim result As String = ""

                ' Initialize the keyed hash object using the secret key as the key
                Dim hashObject As New HMACSHA256(Encoding.UTF8.GetBytes(SECREATKEY))
                ' Computes the signature by hashing the salt with the secret key as the key
                Dim signature = hashObject.ComputeHash(Encoding.UTF8.GetBytes(CONSID & "&" & uTime))
                ' Base 64 Encode
                HasilKey = Convert.ToBase64String(signature)

                Dim req As WinHttp.WinHttpRequest

                Dim Url As String = ALAMATWEB & "referensi/spesialistik"

                req = New WinHttp.WinHttpRequest
                req.Open("GET", Url, False)
                req.SetRequestHeader("X-cons-ID", CONSID)
                req.SetRequestHeader("X-timestamp", uTime)
                req.SetRequestHeader("X-signature", HasilKey)
                req.SetRequestHeader("user_key", USERKEY)
                req.SetRequestHeader("Content-Type", "application/json; charset=utf-8")

                req.Send()

                result = req.ResponseText

                GetDataVClaimReferensiSpesialistik = result

            Catch ex As Exception
                GetDataVClaimReferensiSpesialistik = ex.ToString
                oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                Throw ex
            End Try
        End Function
        Public Function GetDataVClaimReferensiRuangRawat(ByVal ALAMATWEB As String, ByVal CONSID As String, ByVal SECREATKEY As String, ByVal USERKEY As String, ByVal uTime As Integer) As String
            Try
                If Not oConnection.GetConnection() Then
                    GetDataVClaimReferensiRuangRawat = ""
                    Exit Function
                End If

                Dim HasilKey As String = ""
                Dim result As String = ""

                ' Initialize the keyed hash object using the secret key as the key
                Dim hashObject As New HMACSHA256(Encoding.UTF8.GetBytes(SECREATKEY))
                ' Computes the signature by hashing the salt with the secret key as the key
                Dim signature = hashObject.ComputeHash(Encoding.UTF8.GetBytes(CONSID & "&" & uTime))
                ' Base 64 Encode
                HasilKey = Convert.ToBase64String(signature)

                Dim req As WinHttp.WinHttpRequest

                Dim Url As String = ALAMATWEB & "referensi/ruangrawat"

                req = New WinHttp.WinHttpRequest
                req.Open("GET", Url, False)
                req.SetRequestHeader("X-cons-ID", CONSID)
                req.SetRequestHeader("X-timestamp", uTime)
                req.SetRequestHeader("X-signature", HasilKey)
                req.SetRequestHeader("user_key", USERKEY)
                req.SetRequestHeader("Content-Type", "application/json; charset=utf-8")

                req.Send()

                result = req.ResponseText

                GetDataVClaimReferensiRuangRawat = result

            Catch ex As Exception
                GetDataVClaimReferensiRuangRawat = ex.ToString
                oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                Throw ex
            End Try
        End Function
        Public Function GetDataVClaimReferensiCaraKeluar(ByVal ALAMATWEB As String, ByVal CONSID As String, ByVal SECREATKEY As String, ByVal USERKEY As String, ByVal uTime As Integer) As String
            Try
                If Not oConnection.GetConnection() Then
                    GetDataVClaimReferensiCaraKeluar = ""
                    Exit Function
                End If

                Dim HasilKey As String = ""
                Dim result As String = ""

                ' Initialize the keyed hash object using the secret key as the key
                Dim hashObject As New HMACSHA256(Encoding.UTF8.GetBytes(SECREATKEY))
                ' Computes the signature by hashing the salt with the secret key as the key
                Dim signature = hashObject.ComputeHash(Encoding.UTF8.GetBytes(CONSID & "&" & uTime))
                ' Base 64 Encode
                HasilKey = Convert.ToBase64String(signature)

                Dim req As WinHttp.WinHttpRequest

                Dim Url As String = ALAMATWEB & "referensi/carakeluar"

                req = New WinHttp.WinHttpRequest
                req.Open("GET", Url, False)
                req.SetRequestHeader("X-cons-ID", CONSID)
                req.SetRequestHeader("X-timestamp", uTime)
                req.SetRequestHeader("X-signature", HasilKey)
                req.SetRequestHeader("user_key", USERKEY)
                req.SetRequestHeader("Content-Type", "application/json; charset=utf-8")

                req.Send()

                result = req.ResponseText

                GetDataVClaimReferensiCaraKeluar = result

            Catch ex As Exception
                GetDataVClaimReferensiCaraKeluar = ex.ToString
                oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                Throw ex
            End Try
        End Function
        Public Function GetDataVClaimReferensipascapulang(ByVal ALAMATWEB As String, ByVal CONSID As String, ByVal SECREATKEY As String, ByVal USERKEY As String, ByVal uTime As Integer) As String
            Try
                If Not oConnection.GetConnection() Then
                    GetDataVClaimReferensipascapulang = ""
                    Exit Function
                End If

                Dim HasilKey As String = ""
                Dim result As String = ""

                ' Initialize the keyed hash object using the secret key as the key
                Dim hashObject As New HMACSHA256(Encoding.UTF8.GetBytes(SECREATKEY))
                ' Computes the signature by hashing the salt with the secret key as the key
                Dim signature = hashObject.ComputeHash(Encoding.UTF8.GetBytes(CONSID & "&" & uTime))
                ' Base 64 Encode
                HasilKey = Convert.ToBase64String(signature)

                Dim req As WinHttp.WinHttpRequest

                Dim Url As String = ALAMATWEB & "referensi/pascapulang"

                req = New WinHttp.WinHttpRequest
                req.Open("GET", Url, False)
                req.SetRequestHeader("X-cons-ID", CONSID)
                req.SetRequestHeader("X-timestamp", uTime)
                req.SetRequestHeader("X-signature", HasilKey)
                req.SetRequestHeader("user_key", USERKEY)
                req.SetRequestHeader("Content-Type", "application/json; charset=utf-8")

                req.Send()

                result = req.ResponseText

                GetDataVClaimReferensipascapulang = result

            Catch ex As Exception
                GetDataVClaimReferensipascapulang = ex.ToString
                oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                Throw ex
            End Try
        End Function
#End Region
#Region "Peserta"
        Public Function GetDataVClaimPesertaNoKartuBPJS(ByVal ALAMATWEB As String, ByVal CONSID As String, ByVal SECREATKEY As String, ByVal USERKEY As String, ByVal uTime As Integer, ByVal Parameter1 As String, ByVal Parameter2 As String) As String
            Try
                If Not oConnection.GetConnection() Then
                    GetDataVClaimPesertaNoKartuBPJS = ""
                    Exit Function
                End If

                Dim HasilKey As String = ""

                ' Initialize the keyed hash object using the secret key as the key
                Dim hashObject As New HMACSHA256(Encoding.UTF8.GetBytes(SECREATKEY))
                ' Computes the signature by hashing the salt with the secret key as the key
                Dim signature = hashObject.ComputeHash(Encoding.UTF8.GetBytes(CONSID & "&" & uTime))
                ' Base 64 Encode
                HasilKey = Convert.ToBase64String(signature)

                Dim req As WinHttp.WinHttpRequest

                Dim Url As String = ALAMATWEB & "Peserta/nokartu/" & Parameter1 & "/tglSEP/" & Parameter2

                req = New WinHttp.WinHttpRequest
                req.Open("GET", Url, False)
                req.SetRequestHeader("X-cons-ID", CONSID)
                req.SetRequestHeader("X-timestamp", uTime)
                req.SetRequestHeader("X-signature", HasilKey)
                req.SetRequestHeader("user_key", USERKEY)
                req.SetRequestHeader("Content-Type", "application/json; charset=utf-8")

                req.Send()

                GetDataVClaimPesertaNoKartuBPJS = req.ResponseText

            Catch ex As Exception
                GetDataVClaimPesertaNoKartuBPJS = ex.ToString
                oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                Throw ex
            End Try
        End Function
        Public Function GetDataVClaimPesertaNIK(ByVal ALAMATWEB As String, ByVal CONSID As String, ByVal SECREATKEY As String, ByVal USERKEY As String, ByVal uTime As Integer, ByVal Parameter1 As String, ByVal Parameter2 As String) As String
            Try
                If Not oConnection.GetConnection() Then
                    GetDataVClaimPesertaNIK = ""
                    Exit Function
                End If

                Dim HasilKey As String = ""

                ' Initialize the keyed hash object using the secret key as the key
                Dim hashObject As New HMACSHA256(Encoding.UTF8.GetBytes(SECREATKEY))
                ' Computes the signature by hashing the salt with the secret key as the key
                Dim signature = hashObject.ComputeHash(Encoding.UTF8.GetBytes(CONSID & "&" & uTime))
                ' Base 64 Encode
                HasilKey = Convert.ToBase64String(signature)

                Dim req As WinHttp.WinHttpRequest

                Dim Url As String = ALAMATWEB & "Peserta/nik/" & Parameter1 & "/tglSEP/" & Parameter2

                req = New WinHttp.WinHttpRequest
                req.Open("GET", Url, False)
                req.SetRequestHeader("X-cons-ID", CONSID)
                req.SetRequestHeader("X-timestamp", uTime)
                req.SetRequestHeader("X-signature", HasilKey)
                req.SetRequestHeader("user_key", USERKEY)
                req.SetRequestHeader("Content-Type", "application/json; charset=utf-8")

                req.Send()

                GetDataVClaimPesertaNIK = req.ResponseText

            Catch ex As Exception
                GetDataVClaimPesertaNIK = ex.ToString
                oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                Throw ex
            End Try
        End Function
#End Region
#Region "PRB"
        Public Function InsertPRB(ByVal ALAMATWEB As String, ByVal CONSID As String, ByVal SECREATKEY As String, ByVal USERKEY As String, ByVal uTime As Integer, ByVal Request As String) As String
            Try
                If Not oConnection.GetConnection() Then
                    InsertPRB = ""
                    Exit Function
                End If

                Dim HasilKey As String = ""

                ' Initialize the keyed hash object using the secret key as the key
                Dim hashObject As New HMACSHA256(Encoding.UTF8.GetBytes(SECREATKEY))
                ' Computes the signature by hashing the salt with the secret key as the key
                Dim signature = hashObject.ComputeHash(Encoding.UTF8.GetBytes(CONSID & "&" & uTime))
                ' Base 64 Encode
                HasilKey = Convert.ToBase64String(signature)

                Dim req As WinHttp.WinHttpRequest

                Dim Url As String = ALAMATWEB & "PRB/insert"

                req = New WinHttp.WinHttpRequest
                req.Open("POST", Url, False)
                req.SetRequestHeader("X-cons-ID", CONSID)
                req.SetRequestHeader("X-timestamp", uTime)
                req.SetRequestHeader("X-signature", HasilKey)
                req.SetRequestHeader("user_key", USERKEY)
                req.SetRequestHeader("Content-Type", "Application/x-www-form-urlencoded")

                req.Send(Request)

                InsertPRB = req.ResponseText

            Catch ex As Exception
                InsertPRB = ex.ToString
                oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                Throw ex
            End Try
        End Function
        Public Function UpdatePRB(ByVal ALAMATWEB As String, ByVal CONSID As String, ByVal SECREATKEY As String, ByVal USERKEY As String, ByVal uTime As Integer, ByVal Request As String) As String
            Try
                If Not oConnection.GetConnection() Then
                    UpdatePRB = ""
                    Exit Function
                End If

                Dim HasilKey As String = ""

                ' Initialize the keyed hash object using the secret key as the key
                Dim hashObject As New HMACSHA256(Encoding.UTF8.GetBytes(SECREATKEY))
                ' Computes the signature by hashing the salt with the secret key as the key
                Dim signature = hashObject.ComputeHash(Encoding.UTF8.GetBytes(CONSID & "&" & uTime))
                ' Base 64 Encode
                HasilKey = Convert.ToBase64String(signature)

                Dim req As WinHttp.WinHttpRequest

                Dim Url As String = ALAMATWEB & "PRB/Update"

                req = New WinHttp.WinHttpRequest
                req.Open("PUT", Url, False)
                req.SetRequestHeader("X-cons-ID", CONSID)
                req.SetRequestHeader("X-timestamp", uTime)
                req.SetRequestHeader("X-signature", HasilKey)
                req.SetRequestHeader("user_key", USERKEY)
                req.SetRequestHeader("Content-Type", "Application/x-www-form-urlencoded")

                req.Send(Request)

                UpdatePRB = req.ResponseText

            Catch ex As Exception
                UpdatePRB = ex.ToString
                oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                Throw ex
            End Try
        End Function
        Public Function HapusPRB(ByVal ALAMATWEB As String, ByVal CONSID As String, ByVal SECREATKEY As String, ByVal USERKEY As String, ByVal uTime As Integer, ByVal Request As String) As String
            Try
                If Not oConnection.GetConnection() Then
                    HapusPRB = ""
                    Exit Function
                End If

                Dim HasilKey As String = ""

                ' Initialize the keyed hash object using the secret key as the key
                Dim hashObject As New HMACSHA256(Encoding.UTF8.GetBytes(SECREATKEY))
                ' Computes the signature by hashing the salt with the secret key as the key
                Dim signature = hashObject.ComputeHash(Encoding.UTF8.GetBytes(CONSID & "&" & uTime))
                ' Base 64 Encode
                HasilKey = Convert.ToBase64String(signature)

                Dim req As WinHttp.WinHttpRequest

                Dim Url As String = ALAMATWEB & "PRB/Delete"

                req = New WinHttp.WinHttpRequest
                req.Open("DELETE", Url, False)
                req.SetRequestHeader("X-cons-ID", CONSID)
                req.SetRequestHeader("X-timestamp", uTime)
                req.SetRequestHeader("X-signature", HasilKey)
                req.SetRequestHeader("user_key", USERKEY)
                req.SetRequestHeader("Content-Type", "Application/x-www-form-urlencoded")

                req.Send(Request)

                HapusPRB = req.ResponseText

            Catch ex As Exception
                HapusPRB = ex.ToString
                oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                Throw ex
            End Try
        End Function
        Public Function NomorSRB(ByVal ALAMATWEB As String, ByVal CONSID As String, ByVal SECREATKEY As String, ByVal USERKEY As String, ByVal uTime As Integer, ByVal Parameter1 As String, ByVal Parameter2 As String) As String
            Try
                If Not oConnection.GetConnection() Then
                    NomorSRB = ""
                    Exit Function
                End If

                Dim HasilKey As String = ""

                ' Initialize the keyed hash object using the secret key as the key
                Dim hashObject As New HMACSHA256(Encoding.UTF8.GetBytes(SECREATKEY))
                ' Computes the signature by hashing the salt with the secret key as the key
                Dim signature = hashObject.ComputeHash(Encoding.UTF8.GetBytes(CONSID & "&" & uTime))
                ' Base 64 Encode
                HasilKey = Convert.ToBase64String(signature)

                Dim req As WinHttp.WinHttpRequest

                Dim Url As String = ALAMATWEB & "prb/" & Parameter1 & "/nosep/" & Parameter2

                req = New WinHttp.WinHttpRequest
                req.Open("GET", Url, False)
                req.SetRequestHeader("X-cons-ID", CONSID)
                req.SetRequestHeader("X-timestamp", uTime)
                req.SetRequestHeader("X-signature", HasilKey)
                req.SetRequestHeader("user_key", USERKEY)
                req.SetRequestHeader("Content-Type", "application/json; charset=utf-8")

                req.Send()

                NomorSRB = req.ResponseText

            Catch ex As Exception
                NomorSRB = ex.ToString
                oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                Throw ex
            End Try
        End Function
        Public Function TanggalSRB(ByVal ALAMATWEB As String, ByVal CONSID As String, ByVal SECREATKEY As String, ByVal USERKEY As String, ByVal uTime As Integer, ByVal Parameter1 As String, ByVal Parameter2 As String) As String
            Try
                If Not oConnection.GetConnection() Then
                    TanggalSRB = ""
                    Exit Function
                End If

                Dim HasilKey As String = ""

                ' Initialize the keyed hash object using the secret key as the key
                Dim hashObject As New HMACSHA256(Encoding.UTF8.GetBytes(SECREATKEY))
                ' Computes the signature by hashing the salt with the secret key as the key
                Dim signature = hashObject.ComputeHash(Encoding.UTF8.GetBytes(CONSID & "&" & uTime))
                ' Base 64 Encode
                HasilKey = Convert.ToBase64String(signature)

                Dim req As WinHttp.WinHttpRequest

                Dim Url As String = ALAMATWEB & "prb/tglMulai/" & Parameter1 & "/tglAkhir/" & Parameter2

                req = New WinHttp.WinHttpRequest
                req.Open("GET", Url, False)
                req.SetRequestHeader("X-cons-ID", CONSID)
                req.SetRequestHeader("X-timestamp", uTime)
                req.SetRequestHeader("X-signature", HasilKey)
                req.SetRequestHeader("user_key", USERKEY)
                req.SetRequestHeader("Content-Type", "application/json; charset=utf-8")

                req.Send()

                TanggalSRB = req.ResponseText

            Catch ex As Exception
                TanggalSRB = ex.ToString
                oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                Throw ex
            End Try
        End Function
#End Region
#Region "SEP"
#Region "Pembuatan SEP"
        Public Function InsertSEP(ByVal ALAMATWEB As String, ByVal CONSID As String, ByVal SECREATKEY As String, ByVal USERKEY As String, ByVal uTime As Integer, ByVal Request As String) As String
            Try
                If Not oConnection.GetConnection() Then
                    InsertSEP = ""
                    Exit Function
                End If

                Dim HasilKey As String = ""

                ' Initialize the keyed hash object using the secret key as the key
                Dim hashObject As New HMACSHA256(Encoding.UTF8.GetBytes(SECREATKEY))
                ' Computes the signature by hashing the salt with the secret key as the key
                Dim signature = hashObject.ComputeHash(Encoding.UTF8.GetBytes(CONSID & "&" & uTime))
                ' Base 64 Encode
                HasilKey = Convert.ToBase64String(signature)

                Dim req As WinHttp.WinHttpRequest

                Dim Url As String = ALAMATWEB & "SEP/1.1/insert"

                req = New WinHttp.WinHttpRequest
                req.Open("POST", Url, False)
                req.SetRequestHeader("X-cons-ID", CONSID)
                req.SetRequestHeader("X-timestamp", uTime)
                req.SetRequestHeader("X-signature", HasilKey)
                req.SetRequestHeader("user_key", USERKEY)
                req.SetRequestHeader("Content-Type", "Application/x-www-form-urlencoded")

                req.Send(Request)

                InsertSEP = req.ResponseText

            Catch ex As Exception
                InsertSEP = ex.ToString
                oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                Throw ex
            End Try
        End Function
        Public Function UpdateSEP(ByVal ALAMATWEB As String, ByVal CONSID As String, ByVal SECREATKEY As String, ByVal USERKEY As String, ByVal uTime As Integer, ByVal Request As String) As String
            Try
                If Not oConnection.GetConnection() Then
                    UpdateSEP = ""
                    Exit Function
                End If

                Dim HasilKey As String = ""

                ' Initialize the keyed hash object using the secret key as the key
                Dim hashObject As New HMACSHA256(Encoding.UTF8.GetBytes(SECREATKEY))
                ' Computes the signature by hashing the salt with the secret key as the key
                Dim signature = hashObject.ComputeHash(Encoding.UTF8.GetBytes(CONSID & "&" & uTime))
                ' Base 64 Encode
                HasilKey = Convert.ToBase64String(signature)

                Dim req As WinHttp.WinHttpRequest

                Dim Url As String = ALAMATWEB & "SEP/1.1/Update"

                req = New WinHttp.WinHttpRequest
                req.Open("PUT", Url, False)
                req.SetRequestHeader("X-cons-ID", CONSID)
                req.SetRequestHeader("X-timestamp", uTime)
                req.SetRequestHeader("X-signature", HasilKey)
                req.SetRequestHeader("user_key", USERKEY)
                req.SetRequestHeader("Content-Type", "Application/x-www-form-urlencoded")

                req.Send(Request)

                UpdateSEP = req.ResponseText

            Catch ex As Exception
                UpdateSEP = ex.ToString
                oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                Throw ex
            End Try
        End Function
        Public Function HapusSEP(ByVal ALAMATWEB As String, ByVal CONSID As String, ByVal SECREATKEY As String, ByVal USERKEY As String, ByVal uTime As Integer, ByVal Request As String) As String
            Try
                If Not oConnection.GetConnection() Then
                    HapusSEP = ""
                    Exit Function
                End If

                Dim HasilKey As String = ""

                ' Initialize the keyed hash object using the secret key as the key
                Dim hashObject As New HMACSHA256(Encoding.UTF8.GetBytes(SECREATKEY))
                ' Computes the signature by hashing the salt with the secret key as the key
                Dim signature = hashObject.ComputeHash(Encoding.UTF8.GetBytes(CONSID & "&" & uTime))
                ' Base 64 Encode
                HasilKey = Convert.ToBase64String(signature)

                Dim req As WinHttp.WinHttpRequest

                Dim Url As String = ALAMATWEB & "SEP/Delete"

                req = New WinHttp.WinHttpRequest
                req.Open("DELETE", Url, False)
                req.SetRequestHeader("X-cons-ID", CONSID)
                req.SetRequestHeader("X-timestamp", uTime)
                req.SetRequestHeader("X-signature", HasilKey)
                req.SetRequestHeader("user_key", USERKEY)
                req.SetRequestHeader("Content-Type", "Application/x-www-form-urlencoded")

                req.Send(Request)

                HapusSEP = req.ResponseText

            Catch ex As Exception
                HapusSEP = ex.ToString
                oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                Throw ex
            End Try
        End Function
        Public Function CariSEP(ByVal ALAMATWEB As String, ByVal CONSID As String, ByVal SECREATKEY As String, ByVal USERKEY As String, ByVal uTime As Integer, ByVal Parameter As String) As String
            Try
                If Not oConnection.GetConnection() Then
                    CariSEP = ""
                    Exit Function
                End If

                Dim HasilKey As String = ""

                ' Initialize the keyed hash object using the secret key as the key
                Dim hashObject As New HMACSHA256(Encoding.UTF8.GetBytes(SECREATKEY))
                ' Computes the signature by hashing the salt with the secret key as the key
                Dim signature = hashObject.ComputeHash(Encoding.UTF8.GetBytes(CONSID & "&" & uTime))
                ' Base 64 Encode
                HasilKey = Convert.ToBase64String(signature)

                Dim req As WinHttp.WinHttpRequest

                Dim Url As String = ALAMATWEB & "SEP/" & Parameter

                req = New WinHttp.WinHttpRequest
                req.Open("GET", Url, False)
                req.SetRequestHeader("X-cons-ID", CONSID)
                req.SetRequestHeader("X-timestamp", uTime)
                req.SetRequestHeader("X-signature", HasilKey)
                req.SetRequestHeader("user_key", USERKEY)
                req.SetRequestHeader("Content-Type", "Application/x-www-form-urlencoded")

                req.Send()

                CariSEP = req.ResponseText

            Catch ex As Exception
                CariSEP = ex.ToString
                oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                Throw ex
            End Try
        End Function
        Public Function InsertSEPv2(ByVal ALAMATWEB As String, ByVal CONSID As String, ByVal SECREATKEY As String, ByVal USERKEY As String, ByVal uTime As Integer, ByVal Request As String) As String
            Try
                If Not oConnection.GetConnection() Then
                    InsertSEPv2 = ""
                    Exit Function
                End If

                Dim HasilKey As String = ""

                ' Initialize the keyed hash object using the secret key as the key
                Dim hashObject As New HMACSHA256(Encoding.UTF8.GetBytes(SECREATKEY))
                ' Computes the signature by hashing the salt with the secret key as the key
                Dim signature = hashObject.ComputeHash(Encoding.UTF8.GetBytes(CONSID & "&" & uTime))
                ' Base 64 Encode
                HasilKey = Convert.ToBase64String(signature)

                Dim req As WinHttp.WinHttpRequest

                Dim Url As String = ALAMATWEB & "SEP/2.0/insert"

                req = New WinHttp.WinHttpRequest
                req.Open("POST", Url, False)
                req.SetRequestHeader("X-cons-ID", CONSID)
                req.SetRequestHeader("X-timestamp", uTime)
                req.SetRequestHeader("X-signature", HasilKey)
                req.SetRequestHeader("user_key", USERKEY)
                req.SetRequestHeader("Content-Type", "Application/x-www-form-urlencoded")

                req.Send(Request)

                InsertSEPv2 = req.ResponseText

            Catch ex As Exception
                InsertSEPv2 = ex.ToString
                oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                Throw ex
            End Try
        End Function
        Public Function UpdateSEPv2(ByVal ALAMATWEB As String, ByVal CONSID As String, ByVal SECREATKEY As String, ByVal USERKEY As String, ByVal uTime As Integer, ByVal Request As String) As String
            Try
                If Not oConnection.GetConnection() Then
                    UpdateSEPv2 = ""
                    Exit Function
                End If

                Dim HasilKey As String = ""

                ' Initialize the keyed hash object using the secret key as the key
                Dim hashObject As New HMACSHA256(Encoding.UTF8.GetBytes(SECREATKEY))
                ' Computes the signature by hashing the salt with the secret key as the key
                Dim signature = hashObject.ComputeHash(Encoding.UTF8.GetBytes(CONSID & "&" & uTime))
                ' Base 64 Encode
                HasilKey = Convert.ToBase64String(signature)

                Dim req As WinHttp.WinHttpRequest

                Dim Url As String = ALAMATWEB & "SEP/2.0/update"

                req = New WinHttp.WinHttpRequest
                req.Open("PUT", Url, False)
                req.SetRequestHeader("X-cons-ID", CONSID)
                req.SetRequestHeader("X-timestamp", uTime)
                req.SetRequestHeader("X-signature", HasilKey)
                req.SetRequestHeader("user_key", USERKEY)
                req.SetRequestHeader("Content-Type", "Application/x-www-form-urlencoded")

                req.Send(Request)

                UpdateSEPv2 = req.ResponseText

            Catch ex As Exception
                UpdateSEPv2 = ex.ToString
                oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                Throw ex
            End Try
        End Function
        Public Function HapusSEPv2(ByVal ALAMATWEB As String, ByVal CONSID As String, ByVal SECREATKEY As String, ByVal USERKEY As String, ByVal uTime As Integer, ByVal Request As String) As String
            Try
                If Not oConnection.GetConnection() Then
                    HapusSEPv2 = ""
                    Exit Function
                End If

                Dim HasilKey As String = ""

                ' Initialize the keyed hash object using the secret key as the key
                Dim hashObject As New HMACSHA256(Encoding.UTF8.GetBytes(SECREATKEY))
                ' Computes the signature by hashing the salt with the secret key as the key
                Dim signature = hashObject.ComputeHash(Encoding.UTF8.GetBytes(CONSID & "&" & uTime))
                ' Base 64 Encode
                HasilKey = Convert.ToBase64String(signature)

                Dim req As WinHttp.WinHttpRequest

                Dim Url As String = ALAMATWEB & "SEP/2.0/delete"

                req = New WinHttp.WinHttpRequest
                req.Open("DELETE", Url, False)
                req.SetRequestHeader("X-cons-ID", CONSID)
                req.SetRequestHeader("X-timestamp", uTime)
                req.SetRequestHeader("X-signature", HasilKey)
                req.SetRequestHeader("user_key", USERKEY)
                req.SetRequestHeader("Content-Type", "Application/x-www-form-urlencoded")

                req.Send(Request)

                HapusSEPv2 = req.ResponseText

            Catch ex As Exception
                HapusSEPv2 = ex.ToString
                oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                Throw ex
            End Try
        End Function
#End Region
#Region "Potensi Suplesi Jasa Raharja"
        Public Function GetDataSuplesiJasaRaharja(ByVal ALAMATWEB As String, ByVal CONSID As String, ByVal SECREATKEY As String, ByVal USERKEY As String, ByVal uTime As Integer, ByVal Parameter1 As String, ByVal Parameter2 As String) As String
            Try
                If Not oConnection.GetConnection() Then
                    GetDataSuplesiJasaRaharja = ""
                    Exit Function
                End If

                Dim HasilKey As String = ""

                ' Initialize the keyed hash object using the secret key as the key
                Dim hashObject As New HMACSHA256(Encoding.UTF8.GetBytes(SECREATKEY))
                ' Computes the signature by hashing the salt with the secret key as the key
                Dim signature = hashObject.ComputeHash(Encoding.UTF8.GetBytes(CONSID & "&" & uTime))
                ' Base 64 Encode
                HasilKey = Convert.ToBase64String(signature)

                Dim req As WinHttp.WinHttpRequest

                Dim Url As String = ALAMATWEB & "sep/JasaRaharja/Suplesi/" & Parameter1 & "/tglPelayanan/" & Parameter2

                req = New WinHttp.WinHttpRequest
                req.Open("GET", Url, False)
                req.SetRequestHeader("X-cons-ID", CONSID)
                req.SetRequestHeader("X-timestamp", uTime)
                req.SetRequestHeader("X-signature", HasilKey)
                req.SetRequestHeader("user_key", USERKEY)
                req.SetRequestHeader("Content-Type", "application/json; charset=utf-8")

                req.Send()

                GetDataSuplesiJasaRaharja = req.ResponseText

            Catch ex As Exception
                GetDataSuplesiJasaRaharja = ex.ToString
                oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                Throw ex
            End Try
        End Function
        Public Function GetDataDataIndukKecelakaan(ByVal ALAMATWEB As String, ByVal CONSID As String, ByVal SECREATKEY As String, ByVal USERKEY As String, ByVal uTime As Integer, ByVal Parameter1 As String) As String
            Try
                If Not oConnection.GetConnection() Then
                    GetDataDataIndukKecelakaan = ""
                    Exit Function
                End If

                Dim HasilKey As String = ""

                ' Initialize the keyed hash object using the secret key as the key
                Dim hashObject As New HMACSHA256(Encoding.UTF8.GetBytes(SECREATKEY))
                ' Computes the signature by hashing the salt with the secret key as the key
                Dim signature = hashObject.ComputeHash(Encoding.UTF8.GetBytes(CONSID & "&" & uTime))
                ' Base 64 Encode
                HasilKey = Convert.ToBase64String(signature)

                Dim req As WinHttp.WinHttpRequest

                Dim Url As String = ALAMATWEB & "sep/KllInduk/List/" & Parameter1

                req = New WinHttp.WinHttpRequest
                req.Open("GET", Url, False)
                req.SetRequestHeader("X-cons-ID", CONSID)
                req.SetRequestHeader("X-timestamp", uTime)
                req.SetRequestHeader("X-signature", HasilKey)
                req.SetRequestHeader("user_key", USERKEY)
                req.SetRequestHeader("Content-Type", "application/json; charset=utf-8")

                req.Send()

                GetDataDataIndukKecelakaan = req.ResponseText

            Catch ex As Exception
                GetDataDataIndukKecelakaan = ex.ToString
                oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                Throw ex
            End Try
        End Function
#End Region
#Region "Approval Penjaminan SEP"
        Public Function Approval_Pengajuan(ByVal ALAMATWEB As String, ByVal CONSID As String, ByVal SECREATKEY As String, ByVal USERKEY As String, ByVal uTime As Integer, ByVal Request As String) As String
            Try
                If Not oConnection.GetConnection() Then
                    Approval_Pengajuan = ""
                    Exit Function
                End If

                Dim HasilKey As String = ""

                ' Initialize the keyed hash object using the secret key as the key
                Dim hashObject As New HMACSHA256(Encoding.UTF8.GetBytes(SECREATKEY))
                ' Computes the signature by hashing the salt with the secret key as the key
                Dim signature = hashObject.ComputeHash(Encoding.UTF8.GetBytes(CONSID & "&" & uTime))
                ' Base 64 Encode
                HasilKey = Convert.ToBase64String(signature)

                Dim req As WinHttp.WinHttpRequest

                Dim Url As String = ALAMATWEB & "Sep/pengajuanSEP"

                req = New WinHttp.WinHttpRequest
                req.Open("POST", Url, False)
                req.SetRequestHeader("X-cons-ID", CONSID)
                req.SetRequestHeader("X-timestamp", uTime)
                req.SetRequestHeader("X-signature", HasilKey)
                req.SetRequestHeader("user_key", USERKEY)
                req.SetRequestHeader("Content-Type", "Application/x-www-form-urlencoded")

                req.Send(Request)

                Approval_Pengajuan = req.ResponseText

            Catch ex As Exception
                Approval_Pengajuan = ex.ToString
                oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                Throw ex
            End Try
        End Function
        Public Function Approval_ApprovalPengajuanSEP(ByVal ALAMATWEB As String, ByVal CONSID As String, ByVal SECREATKEY As String, ByVal USERKEY As String, ByVal uTime As Integer, ByVal Request As String) As String
            Try
                If Not oConnection.GetConnection() Then
                    Approval_ApprovalPengajuanSEP = ""
                    Exit Function
                End If

                Dim HasilKey As String = ""

                ' Initialize the keyed hash object using the secret key as the key
                Dim hashObject As New HMACSHA256(Encoding.UTF8.GetBytes(SECREATKEY))
                ' Computes the signature by hashing the salt with the secret key as the key
                Dim signature = hashObject.ComputeHash(Encoding.UTF8.GetBytes(CONSID & "&" & uTime))
                ' Base 64 Encode
                HasilKey = Convert.ToBase64String(signature)

                Dim req As WinHttp.WinHttpRequest

                Dim Url As String = ALAMATWEB & "Sep/aprovalSEP"

                req = New WinHttp.WinHttpRequest
                req.Open("POST", Url, False)
                req.SetRequestHeader("X-cons-ID", CONSID)
                req.SetRequestHeader("X-timestamp", uTime)
                req.SetRequestHeader("X-signature", HasilKey)
                req.SetRequestHeader("user_key", USERKEY)
                req.SetRequestHeader("Content-Type", "Application/x-www-form-urlencoded")

                req.Send(Request)

                Approval_ApprovalPengajuanSEP = req.ResponseText

            Catch ex As Exception
                Approval_ApprovalPengajuanSEP = ex.ToString
                oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                Throw ex
            End Try
        End Function
#End Region
#Region "Update Tgl Pulang SEP"
        Public Function UpdateTanggalPulang(ByVal ALAMATWEB As String, ByVal CONSID As String, ByVal SECREATKEY As String, ByVal USERKEY As String, ByVal uTime As Integer, ByVal Request As String) As String
            Try
                If Not oConnection.GetConnection() Then
                    UpdateTanggalPulang = ""
                    Exit Function
                End If

                Dim HasilKey As String = ""

                ' Initialize the keyed hash object using the secret key as the key
                Dim hashObject As New HMACSHA256(Encoding.UTF8.GetBytes(SECREATKEY))
                ' Computes the signature by hashing the salt with the secret key as the key
                Dim signature = hashObject.ComputeHash(Encoding.UTF8.GetBytes(CONSID & "&" & uTime))
                ' Base 64 Encode
                HasilKey = Convert.ToBase64String(signature)

                Dim req As WinHttp.WinHttpRequest

                Dim Url As String = ALAMATWEB & "Sep/updtglplg"

                req = New WinHttp.WinHttpRequest
                req.Open("PUT", Url, False)
                req.SetRequestHeader("X-cons-ID", CONSID)
                req.SetRequestHeader("X-timestamp", uTime)
                req.SetRequestHeader("X-signature", HasilKey)
                req.SetRequestHeader("user_key", USERKEY)
                req.SetRequestHeader("Content-Type", "Application/x-www-form-urlencoded")

                req.Send(Request)

                UpdateTanggalPulang = req.ResponseText

            Catch ex As Exception
                UpdateTanggalPulang = ex.ToString
                oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                Throw ex
            End Try
        End Function
        Public Function UpdateTanggalPulangv2(ByVal ALAMATWEB As String, ByVal CONSID As String, ByVal SECREATKEY As String, ByVal USERKEY As String, ByVal uTime As Integer, ByVal Request As String) As String
            Try
                If Not oConnection.GetConnection() Then
                    UpdateTanggalPulangv2 = ""
                    Exit Function
                End If

                Dim HasilKey As String = ""

                ' Initialize the keyed hash object using the secret key as the key
                Dim hashObject As New HMACSHA256(Encoding.UTF8.GetBytes(SECREATKEY))
                ' Computes the signature by hashing the salt with the secret key as the key
                Dim signature = hashObject.ComputeHash(Encoding.UTF8.GetBytes(CONSID & "&" & uTime))
                ' Base 64 Encode
                HasilKey = Convert.ToBase64String(signature)

                Dim req As WinHttp.WinHttpRequest

                Dim Url As String = ALAMATWEB & "SEP/2.0/updtglplgg"

                req = New WinHttp.WinHttpRequest
                req.Open("PUT", Url, False)
                req.SetRequestHeader("X-cons-ID", CONSID)
                req.SetRequestHeader("X-timestamp", uTime)
                req.SetRequestHeader("X-signature", HasilKey)
                req.SetRequestHeader("user_key", USERKEY)
                req.SetRequestHeader("Content-Type", "Application/x-www-form-urlencoded")

                req.Send(Request)

                UpdateTanggalPulangv2 = req.ResponseText

            Catch ex As Exception
                UpdateTanggalPulangv2 = ex.ToString
                oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                Throw ex
            End Try
        End Function
#End Region
#Region "Integrasi SEP dan Inacbg"
        Public Function IntegrasiSEPdenganInacbg(ByVal ALAMATWEB As String, ByVal CONSID As String, ByVal SECREATKEY As String, ByVal USERKEY As String, ByVal uTime As Integer, ByVal Parameter As String) As String
            Try
                If Not oConnection.GetConnection() Then
                    IntegrasiSEPdenganInacbg = ""
                    Exit Function
                End If

                Dim HasilKey As String = ""

                ' Initialize the keyed hash object using the secret key as the key
                Dim hashObject As New HMACSHA256(Encoding.UTF8.GetBytes(SECREATKEY))
                ' Computes the signature by hashing the salt with the secret key as the key
                Dim signature = hashObject.ComputeHash(Encoding.UTF8.GetBytes(CONSID & "&" & uTime))
                ' Base 64 Encode
                HasilKey = Convert.ToBase64String(signature)

                Dim req As WinHttp.WinHttpRequest

                Dim Url As String = ALAMATWEB & "/sep/cbg/" & Parameter

                req = New WinHttp.WinHttpRequest
                req.Open("GET", Url, False)
                req.SetRequestHeader("X-cons-ID", CONSID)
                req.SetRequestHeader("X-timestamp", uTime)
                req.SetRequestHeader("X-signature", HasilKey)
                req.SetRequestHeader("user_key", USERKEY)
                req.SetRequestHeader("Content-Type", "Application/x-www-form-urlencoded")

                req.Send()

                IntegrasiSEPdenganInacbg = req.ResponseText

            Catch ex As Exception
                IntegrasiSEPdenganInacbg = ex.ToString
                oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                Throw ex
            End Try
        End Function
#End Region
#Region "LPK"
        Public Function InsertLPK(ByVal ALAMATWEB As String, ByVal CONSID As String, ByVal SECREATKEY As String, ByVal USERKEY As String, ByVal uTime As Integer, ByVal Request As String) As String
            Try
                If Not oConnection.GetConnection() Then
                    InsertLPK = ""
                    Exit Function
                End If

                Dim HasilKey As String = ""

                ' Initialize the keyed hash object using the secret key as the key
                Dim hashObject As New HMACSHA256(Encoding.UTF8.GetBytes(SECREATKEY))
                ' Computes the signature by hashing the salt with the secret key as the key
                Dim signature = hashObject.ComputeHash(Encoding.UTF8.GetBytes(CONSID & "&" & uTime))
                ' Base 64 Encode
                HasilKey = Convert.ToBase64String(signature)

                Dim req As WinHttp.WinHttpRequest

                Dim Url As String = ALAMATWEB & "LPK/insert"

                req = New WinHttp.WinHttpRequest
                req.Open("POST", Url, False)
                req.SetRequestHeader("X-cons-ID", CONSID)
                req.SetRequestHeader("X-timestamp", uTime)
                req.SetRequestHeader("X-signature", HasilKey)
                req.SetRequestHeader("user_key", USERKEY)
                req.SetRequestHeader("Content-Type", "Application/x-www-form-urlencoded")

                req.Send(Request)

                InsertLPK = req.ResponseText

            Catch ex As Exception
                InsertLPK = ex.ToString
                oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                Throw ex
            End Try
        End Function
        Public Function UpdateLPK(ByVal ALAMATWEB As String, ByVal CONSID As String, ByVal SECREATKEY As String, ByVal USERKEY As String, ByVal uTime As Integer, ByVal Request As String) As String
            Try
                If Not oConnection.GetConnection() Then
                    UpdateLPK = ""
                    Exit Function
                End If

                Dim HasilKey As String = ""

                ' Initialize the keyed hash object using the secret key as the key
                Dim hashObject As New HMACSHA256(Encoding.UTF8.GetBytes(SECREATKEY))
                ' Computes the signature by hashing the salt with the secret key as the key
                Dim signature = hashObject.ComputeHash(Encoding.UTF8.GetBytes(CONSID & "&" & uTime))
                ' Base 64 Encode
                HasilKey = Convert.ToBase64String(signature)

                Dim req As WinHttp.WinHttpRequest

                Dim Url As String = ALAMATWEB & "LPK/update"

                req = New WinHttp.WinHttpRequest
                req.Open("PUT", Url, False)
                req.SetRequestHeader("X-cons-ID", CONSID)
                req.SetRequestHeader("X-timestamp", uTime)
                req.SetRequestHeader("X-signature", HasilKey)
                req.SetRequestHeader("user_key", USERKEY)
                req.SetRequestHeader("Content-Type", "Application/x-www-form-urlencoded")

                req.Send(Request)

                UpdateLPK = req.ResponseText

            Catch ex As Exception
                UpdateLPK = ex.ToString
                oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                Throw ex
            End Try
        End Function
        Public Function DeleteLPK(ByVal ALAMATWEB As String, ByVal CONSID As String, ByVal SECREATKEY As String, ByVal USERKEY As String, ByVal uTime As Integer, ByVal Request As String) As String
            Try
                If Not oConnection.GetConnection() Then
                    DeleteLPK = ""
                    Exit Function
                End If

                Dim HasilKey As String = ""

                ' Initialize the keyed hash object using the secret key as the key
                Dim hashObject As New HMACSHA256(Encoding.UTF8.GetBytes(SECREATKEY))
                ' Computes the signature by hashing the salt with the secret key as the key
                Dim signature = hashObject.ComputeHash(Encoding.UTF8.GetBytes(CONSID & "&" & uTime))
                ' Base 64 Encode
                HasilKey = Convert.ToBase64String(signature)

                Dim req As WinHttp.WinHttpRequest

                Dim Url As String = ALAMATWEB & "LPK/delete"

                req = New WinHttp.WinHttpRequest
                req.Open("DELETE", Url, False)
                req.SetRequestHeader("X-cons-ID", CONSID)
                req.SetRequestHeader("X-timestamp", uTime)
                req.SetRequestHeader("X-signature", HasilKey)
                req.SetRequestHeader("user_key", USERKEY)
                req.SetRequestHeader("Content-Type", "Application/x-www-form-urlencoded")

                req.Send(Request)

                DeleteLPK = req.ResponseText

            Catch ex As Exception
                DeleteLPK = ex.ToString
                oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                Throw ex
            End Try
        End Function
        Public Function DataLembarPengajuanKlaim(ByVal ALAMATWEB As String, ByVal CONSID As String, ByVal SECREATKEY As String, ByVal USERKEY As String, ByVal uTime As Integer, ByVal Parameter1 As String, ByVal Parameter2 As String) As String
            Try
                If Not oConnection.GetConnection() Then
                    DataLembarPengajuanKlaim = ""
                    Exit Function
                End If

                Dim HasilKey As String = ""

                ' Initialize the keyed hash object using the secret key as the key
                Dim hashObject As New HMACSHA256(Encoding.UTF8.GetBytes(SECREATKEY))
                ' Computes the signature by hashing the salt with the secret key as the key
                Dim signature = hashObject.ComputeHash(Encoding.UTF8.GetBytes(CONSID & "&" & uTime))
                ' Base 64 Encode
                HasilKey = Convert.ToBase64String(signature)

                Dim req As WinHttp.WinHttpRequest

                Dim Url As String = ALAMATWEB & "LPK/TglMasuk/" & Parameter1 & "/JnsPelayanan/" & Parameter2

                req = New WinHttp.WinHttpRequest
                req.Open("GET", Url, False)
                req.SetRequestHeader("X-cons-ID", CONSID)
                req.SetRequestHeader("X-timestamp", uTime)
                req.SetRequestHeader("X-signature", HasilKey)
                req.SetRequestHeader("user_key", USERKEY)
                req.SetRequestHeader("Content-Type", "Application/x-www-form-urlencoded")

                req.Send()

                DataLembarPengajuanKlaim = req.ResponseText

            Catch ex As Exception
                DataLembarPengajuanKlaim = ex.ToString
                oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                Throw ex
            End Try
        End Function
#End Region
#Region "Get Finger Print"
        Public Function GetFingerPrint(ByVal ALAMATWEB As String, ByVal CONSID As String, ByVal SECREATKEY As String, ByVal USERKEY As String, ByVal uTime As Integer, ByVal Parameter1 As String, ByVal Parameter2 As String) As String
            Try
                If Not oConnection.GetConnection() Then
                    GetFingerPrint = ""
                    Exit Function
                End If

                Dim HasilKey As String = ""

                ' Initialize the keyed hash object using the secret key as the key
                Dim hashObject As New HMACSHA256(Encoding.UTF8.GetBytes(SECREATKEY))
                ' Computes the signature by hashing the salt with the secret key as the key
                Dim signature = hashObject.ComputeHash(Encoding.UTF8.GetBytes(CONSID & "&" & uTime))
                ' Base 64 Encode
                HasilKey = Convert.ToBase64String(signature)

                Dim req As WinHttp.WinHttpRequest

                Dim Url As String = ALAMATWEB & "SEP/FingerPrint/Peserta/" & Parameter1 & "/TglPelayanan/" & Parameter2

                req = New WinHttp.WinHttpRequest
                req.Open("GET", Url, False)
                req.SetRequestHeader("X-cons-ID", CONSID)
                req.SetRequestHeader("X-timestamp", uTime)
                req.SetRequestHeader("X-signature", HasilKey)
                req.SetRequestHeader("user_key", USERKEY)
                req.SetRequestHeader("Content-Type", "Application/x-www-form-urlencoded")

                req.Send()

                GetFingerPrint = req.ResponseText

            Catch ex As Exception
                GetFingerPrint = ex.ToString
                oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                Throw ex
            End Try
        End Function
        Public Function GetListFingerPrint(ByVal ALAMATWEB As String, ByVal CONSID As String, ByVal SECREATKEY As String, ByVal USERKEY As String, ByVal uTime As Integer, ByVal Parameter As String) As String
            Try
                If Not oConnection.GetConnection() Then
                    GetListFingerPrint = ""
                    Exit Function
                End If

                Dim HasilKey As String = ""

                ' Initialize the keyed hash object using the secret key as the key
                Dim hashObject As New HMACSHA256(Encoding.UTF8.GetBytes(SECREATKEY))
                ' Computes the signature by hashing the salt with the secret key as the key
                Dim signature = hashObject.ComputeHash(Encoding.UTF8.GetBytes(CONSID & "&" & uTime))
                ' Base 64 Encode
                HasilKey = Convert.ToBase64String(signature)

                Dim req As WinHttp.WinHttpRequest

                Dim Url As String = ALAMATWEB & "SEP/FingerPrint/List/Peserta/TglPelayanan/" & Parameter

                req = New WinHttp.WinHttpRequest
                req.Open("GET", Url, False)
                req.SetRequestHeader("X-cons-ID", CONSID)
                req.SetRequestHeader("X-timestamp", uTime)
                req.SetRequestHeader("X-signature", HasilKey)
                req.SetRequestHeader("user_key", USERKEY)
                req.SetRequestHeader("Content-Type", "Application/x-www-form-urlencoded")

                req.Send()

                GetListFingerPrint = req.ResponseText

            Catch ex As Exception
                GetListFingerPrint = ex.ToString
                oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                Throw ex
            End Try
        End Function
#End Region
#Region "SEP Internal"
        Public Function DataSEPInternal(ByVal ALAMATWEB As String, ByVal CONSID As String, ByVal SECREATKEY As String, ByVal USERKEY As String, ByVal uTime As Integer, ByVal Parameter As String) As String
            Try
                If Not oConnection.GetConnection() Then
                    DataSEPInternal = ""
                    Exit Function
                End If

                Dim HasilKey As String = ""

                ' Initialize the keyed hash object using the secret key as the key
                Dim hashObject As New HMACSHA256(Encoding.UTF8.GetBytes(SECREATKEY))
                ' Computes the signature by hashing the salt with the secret key as the key
                Dim signature = hashObject.ComputeHash(Encoding.UTF8.GetBytes(CONSID & "&" & uTime))
                ' Base 64 Encode
                HasilKey = Convert.ToBase64String(signature)

                Dim req As WinHttp.WinHttpRequest

                Dim Url As String = ALAMATWEB & "SEP/Internal/" & Parameter

                req = New WinHttp.WinHttpRequest
                req.Open("GET", Url, False)
                req.SetRequestHeader("X-cons-ID", CONSID)
                req.SetRequestHeader("X-timestamp", uTime)
                req.SetRequestHeader("X-signature", HasilKey)
                req.SetRequestHeader("user_key", USERKEY)
                req.SetRequestHeader("Content-Type", "Application/x-www-form-urlencoded")

                req.Send()

                DataSEPInternal = req.ResponseText

            Catch ex As Exception
                DataSEPInternal = ex.ToString
                oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                Throw ex
            End Try
        End Function
        Public Function HapusSEPInternal(ByVal ALAMATWEB As String, ByVal CONSID As String, ByVal SECREATKEY As String, ByVal USERKEY As String, ByVal uTime As Integer, ByVal Request As String) As String
            Try
                If Not oConnection.GetConnection() Then
                    HapusSEPInternal = ""
                    Exit Function
                End If

                Dim HasilKey As String = ""

                ' Initialize the keyed hash object using the secret key as the key
                Dim hashObject As New HMACSHA256(Encoding.UTF8.GetBytes(SECREATKEY))
                ' Computes the signature by hashing the salt with the secret key as the key
                Dim signature = hashObject.ComputeHash(Encoding.UTF8.GetBytes(CONSID & "&" & uTime))
                ' Base 64 Encode
                HasilKey = Convert.ToBase64String(signature)

                Dim req As WinHttp.WinHttpRequest

                Dim Url As String = ALAMATWEB & "SEP/Internal/delete"

                req = New WinHttp.WinHttpRequest
                req.Open("DELETE", Url, False)
                req.SetRequestHeader("X-cons-ID", CONSID)
                req.SetRequestHeader("X-timestamp", uTime)
                req.SetRequestHeader("X-signature", HasilKey)
                req.SetRequestHeader("user_key", USERKEY)
                req.SetRequestHeader("Content-Type", "Application/x-www-form-urlencoded")

                req.Send(Request)

                HapusSEPInternal = req.ResponseText

            Catch ex As Exception
                HapusSEPInternal = ex.ToString
                oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                Throw ex
            End Try
        End Function
#End Region
#End Region
#Region "Rujukan"
#Region "Cari Rujukan"
        Public Function CariRujukan(ByVal ALAMATWEB As String, ByVal CONSID As String, ByVal SECREATKEY As String, ByVal USERKEY As String, ByVal uTime As Integer, ByVal Parameter As String, ByVal JenisRujukan As Integer) As String
            Try
                If Not oConnection.GetConnection() Then
                    CariRujukan = ""
                    Exit Function
                End If

                Dim HasilKey As String = ""

                ' Initialize the keyed hash object using the secret key as the key
                Dim hashObject As New HMACSHA256(Encoding.UTF8.GetBytes(SECREATKEY))
                ' Computes the signature by hashing the salt with the secret key as the key
                Dim signature = hashObject.ComputeHash(Encoding.UTF8.GetBytes(CONSID & "&" & uTime))
                ' Base 64 Encode
                HasilKey = Convert.ToBase64String(signature)

                Dim req As WinHttp.WinHttpRequest

                Dim Url As String = ALAMATWEB & IIf(JenisRujukan = 0, "Rujukan/", "Rujukan/RS/") & Parameter

                req = New WinHttp.WinHttpRequest
                req.Open("GET", Url, False)
                req.SetRequestHeader("X-cons-ID", CONSID)
                req.SetRequestHeader("X-timestamp", uTime)
                req.SetRequestHeader("X-signature", HasilKey)
                req.SetRequestHeader("user_key", USERKEY)
                req.SetRequestHeader("Content-Type", "application/json; charset=utf-8")

                req.Send()

                CariRujukan = req.ResponseText

            Catch ex As Exception
                CariRujukan = ex.ToString
                oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                Throw ex
            End Try
        End Function
        Public Function CariRujukanKartuSatuRecord(ByVal ALAMATWEB As String, ByVal CONSID As String, ByVal SECREATKEY As String, ByVal USERKEY As String, ByVal uTime As Integer, ByVal Parameter As String, ByVal JenisRujukan As Integer) As String
            Try
                If Not oConnection.GetConnection() Then
                    CariRujukanKartuSatuRecord = ""
                    Exit Function
                End If

                Dim HasilKey As String = ""

                ' Initialize the keyed hash object using the secret key as the key
                Dim hashObject As New HMACSHA256(Encoding.UTF8.GetBytes(SECREATKEY))
                ' Computes the signature by hashing the salt with the secret key as the key
                Dim signature = hashObject.ComputeHash(Encoding.UTF8.GetBytes(CONSID & "&" & uTime))
                ' Base 64 Encode
                HasilKey = Convert.ToBase64String(signature)

                Dim req As WinHttp.WinHttpRequest

                Dim Url As String = ALAMATWEB & IIf(JenisRujukan = 0, "Rujukan/Peserta/", "Rujukan/RS/Peserta/") & Parameter

                req = New WinHttp.WinHttpRequest
                req.Open("GET", Url, False)
                req.SetRequestHeader("X-cons-ID", CONSID)
                req.SetRequestHeader("X-timestamp", uTime)
                req.SetRequestHeader("X-signature", HasilKey)
                req.SetRequestHeader("user_key", USERKEY)
                req.SetRequestHeader("Content-Type", "application/json; charset=utf-8")

                req.Send()

                CariRujukanKartuSatuRecord = req.ResponseText

            Catch ex As Exception
                CariRujukanKartuSatuRecord = ex.ToString
                oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                Throw ex
            End Try
        End Function
        Public Function CariRujukanKartuMultiRecord(ByVal ALAMATWEB As String, ByVal CONSID As String, ByVal SECREATKEY As String, ByVal USERKEY As String, ByVal uTime As Integer, ByVal Parameter As String, ByVal JenisRujukan As Integer) As String
            Try
                If Not oConnection.GetConnection() Then
                    CariRujukanKartuMultiRecord = ""
                    Exit Function
                End If

                Dim HasilKey As String = ""

                ' Initialize the keyed hash object using the secret key as the key
                Dim hashObject As New HMACSHA256(Encoding.UTF8.GetBytes(SECREATKEY))
                ' Computes the signature by hashing the salt with the secret key as the key
                Dim signature = hashObject.ComputeHash(Encoding.UTF8.GetBytes(CONSID & "&" & uTime))
                ' Base 64 Encode
                HasilKey = Convert.ToBase64String(signature)

                Dim req As WinHttp.WinHttpRequest

                Dim Url As String = ALAMATWEB & IIf(JenisRujukan = 0, "Rujukan/List/Peserta/", "Rujukan/RS/List/Peserta/") & Parameter

                req = New WinHttp.WinHttpRequest
                req.Open("GET", Url, False)
                req.SetRequestHeader("X-cons-ID", CONSID)
                req.SetRequestHeader("X-timestamp", uTime)
                req.SetRequestHeader("X-signature", HasilKey)
                req.SetRequestHeader("user_key", USERKEY)
                req.SetRequestHeader("Content-Type", "application/json; charset=utf-8")

                req.Send()

                CariRujukanKartuMultiRecord = req.ResponseText

            Catch ex As Exception
                CariRujukanKartuMultiRecord = ex.ToString
                oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                Throw ex
            End Try
        End Function
#End Region
#Region "Pembuatan Rujukan"
        Public Function InsertRujukan(ByVal ALAMATWEB As String, ByVal CONSID As String, ByVal SECREATKEY As String, ByVal USERKEY As String, ByVal uTime As Integer, ByVal Request As String) As String
            Try
                If Not oConnection.GetConnection() Then
                    InsertRujukan = ""
                    Exit Function
                End If

                Dim HasilKey As String = ""

                ' Initialize the keyed hash object using the secret key as the key
                Dim hashObject As New HMACSHA256(Encoding.UTF8.GetBytes(SECREATKEY))
                ' Computes the signature by hashing the salt with the secret key as the key
                Dim signature = hashObject.ComputeHash(Encoding.UTF8.GetBytes(CONSID & "&" & uTime))
                ' Base 64 Encode
                HasilKey = Convert.ToBase64String(signature)

                Dim req As WinHttp.WinHttpRequest

                Dim Url As String = ALAMATWEB & "Rujukan/insert"

                req = New WinHttp.WinHttpRequest
                req.Open("POST", Url, False)
                req.SetRequestHeader("X-cons-ID", CONSID)
                req.SetRequestHeader("X-timestamp", uTime)
                req.SetRequestHeader("X-signature", HasilKey)
                req.SetRequestHeader("user_key", USERKEY)
                req.SetRequestHeader("Content-Type", "aApplication/x-www-form-urlencoded")

                req.Send(Request)

                InsertRujukan = req.ResponseText

            Catch ex As Exception
                InsertRujukan = ex.ToString
                oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                Throw ex
            End Try
        End Function
        Public Function UpdateRujukan(ByVal ALAMATWEB As String, ByVal CONSID As String, ByVal SECREATKEY As String, ByVal USERKEY As String, ByVal uTime As Integer, ByVal Request As String) As String
            Try
                If Not oConnection.GetConnection() Then
                    UpdateRujukan = ""
                    Exit Function
                End If

                Dim HasilKey As String = ""

                ' Initialize the keyed hash object using the secret key as the key
                Dim hashObject As New HMACSHA256(Encoding.UTF8.GetBytes(SECREATKEY))
                ' Computes the signature by hashing the salt with the secret key as the key
                Dim signature = hashObject.ComputeHash(Encoding.UTF8.GetBytes(CONSID & "&" & uTime))
                ' Base 64 Encode
                HasilKey = Convert.ToBase64String(signature)

                Dim req As WinHttp.WinHttpRequest

                Dim Url As String = ALAMATWEB & "Rujukan/update"

                req = New WinHttp.WinHttpRequest
                req.Open("PUT", Url, False)
                req.SetRequestHeader("X-cons-ID", CONSID)
                req.SetRequestHeader("X-timestamp", uTime)
                req.SetRequestHeader("X-signature", HasilKey)
                req.SetRequestHeader("user_key", USERKEY)
                req.SetRequestHeader("Content-Type", "Application/x-www-form-urlencoded")

                req.Send(Request)

                UpdateRujukan = req.ResponseText

            Catch ex As Exception
                UpdateRujukan = ex.ToString
                oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                Throw ex
            End Try
        End Function
        Public Function DeleteRujukan(ByVal ALAMATWEB As String, ByVal CONSID As String, ByVal SECREATKEY As String, ByVal USERKEY As String, ByVal uTime As Integer, ByVal Request As String) As String
            Try
                If Not oConnection.GetConnection() Then
                    DeleteRujukan = ""
                    Exit Function
                End If

                Dim HasilKey As String = ""

                ' Initialize the keyed hash object using the secret key as the key
                Dim hashObject As New HMACSHA256(Encoding.UTF8.GetBytes(SECREATKEY))
                ' Computes the signature by hashing the salt with the secret key as the key
                Dim signature = hashObject.ComputeHash(Encoding.UTF8.GetBytes(CONSID & "&" & uTime))
                ' Base 64 Encode
                HasilKey = Convert.ToBase64String(signature)

                Dim req As WinHttp.WinHttpRequest

                Dim Url As String = ALAMATWEB & "Rujukan/delete"

                req = New WinHttp.WinHttpRequest
                req.Open("DELETE", Url, False)
                req.SetRequestHeader("X-cons-ID", CONSID)
                req.SetRequestHeader("X-timestamp", uTime)
                req.SetRequestHeader("X-signature", HasilKey)
                req.SetRequestHeader("user_key", USERKEY)
                req.SetRequestHeader("Content-Type", "Application/x-www-form-urlencoded")

                req.Send(Request)

                DeleteRujukan = req.ResponseText

            Catch ex As Exception
                DeleteRujukan = ex.ToString
                oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                Throw ex
            End Try
        End Function
        Public Function InsertRujukanV2(ByVal ALAMATWEB As String, ByVal CONSID As String, ByVal SECREATKEY As String, ByVal USERKEY As String, ByVal uTime As Integer, ByVal Request As String) As String
            Try
                If Not oConnection.GetConnection() Then
                    InsertRujukanV2 = ""
                    Exit Function
                End If

                Dim HasilKey As String = ""

                ' Initialize the keyed hash object using the secret key as the key
                Dim hashObject As New HMACSHA256(Encoding.UTF8.GetBytes(SECREATKEY))
                ' Computes the signature by hashing the salt with the secret key as the key
                Dim signature = hashObject.ComputeHash(Encoding.UTF8.GetBytes(CONSID & "&" & uTime))
                ' Base 64 Encode
                HasilKey = Convert.ToBase64String(signature)

                Dim req As WinHttp.WinHttpRequest

                Dim Url As String = ALAMATWEB & "Rujukan/2.0/insert"

                req = New WinHttp.WinHttpRequest
                req.Open("POST", Url, False)
                req.SetRequestHeader("X-cons-ID", CONSID)
                req.SetRequestHeader("X-timestamp", uTime)
                req.SetRequestHeader("X-signature", HasilKey)
                req.SetRequestHeader("user_key", USERKEY)
                req.SetRequestHeader("Content-Type", "aApplication/x-www-form-urlencoded")

                req.Send(Request)

                InsertRujukanV2 = req.ResponseText

            Catch ex As Exception
                InsertRujukanV2 = ex.ToString
                oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                Throw ex
            End Try
        End Function
        Public Function UpdateRujukanV2(ByVal ALAMATWEB As String, ByVal CONSID As String, ByVal SECREATKEY As String, ByVal USERKEY As String, ByVal uTime As Integer, ByVal Request As String) As String
            Try
                If Not oConnection.GetConnection() Then
                    UpdateRujukanV2 = ""
                    Exit Function
                End If

                Dim HasilKey As String = ""

                ' Initialize the keyed hash object using the secret key as the key
                Dim hashObject As New HMACSHA256(Encoding.UTF8.GetBytes(SECREATKEY))
                ' Computes the signature by hashing the salt with the secret key as the key
                Dim signature = hashObject.ComputeHash(Encoding.UTF8.GetBytes(CONSID & "&" & uTime))
                ' Base 64 Encode
                HasilKey = Convert.ToBase64String(signature)

                Dim req As WinHttp.WinHttpRequest

                Dim Url As String = ALAMATWEB & "Rujukan/2.0/Update"

                req = New WinHttp.WinHttpRequest
                req.Open("PUT", Url, False)
                req.SetRequestHeader("X-cons-ID", CONSID)
                req.SetRequestHeader("X-timestamp", uTime)
                req.SetRequestHeader("X-signature", HasilKey)
                req.SetRequestHeader("user_key", USERKEY)
                req.SetRequestHeader("Content-Type", "Application/x-www-form-urlencoded")

                req.Send(Request)

                UpdateRujukanV2 = req.ResponseText

            Catch ex As Exception
                UpdateRujukanV2 = ex.ToString
                oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                Throw ex
            End Try
        End Function

#End Region
#End Region
#Region "Monitoring"
        Public Function GetDataVClaimMonitoringDataKunjungan(ByVal ALAMATWEB As String, ByVal CONSID As String, ByVal SECREATKEY As String, ByVal USERKEY As String, ByVal uTime As Integer, ByVal Paramater1 As String, ByVal Parameter2 As String) As String
            Try
                If Not oConnection.GetConnection() Then
                    GetDataVClaimMonitoringDataKunjungan = ""
                    Exit Function
                End If

                Dim HasilKey As String = ""

                ' Initialize the keyed hash object using the secret key as the key
                Dim hashObject As New HMACSHA256(Encoding.UTF8.GetBytes(SECREATKEY))
                ' Computes the signature by hashing the salt with the secret key as the key
                Dim signature = hashObject.ComputeHash(Encoding.UTF8.GetBytes(CONSID & "&" & uTime))
                ' Base 64 Encode
                HasilKey = Convert.ToBase64String(signature)

                Dim req As WinHttp.WinHttpRequest

                Dim Url As String = ALAMATWEB & "Monitoring/Kunjungan/Tanggal/" & Paramater1 & "/JnsPelayanan/" & Parameter2

                req = New WinHttp.WinHttpRequest
                req.Open("GET", Url, False)
                req.SetRequestHeader("X-cons-ID", CONSID)
                req.SetRequestHeader("X-timestamp", uTime)
                req.SetRequestHeader("X-signature", HasilKey)
                req.SetRequestHeader("user_key", USERKEY)
                req.SetRequestHeader("Content-Type", "application/json; charset=utf-8")

                req.Send()

                GetDataVClaimMonitoringDataKunjungan = req.ResponseText

            Catch ex As Exception
                GetDataVClaimMonitoringDataKunjungan = ex.ToString
                oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                Throw ex
            End Try
        End Function
        Public Function GetDataVClaimMonitoringDataKlaim(ByVal ALAMATWEB As String, ByVal CONSID As String, ByVal SECREATKEY As String, ByVal USERKEY As String, ByVal uTime As Integer, ByVal Paramater1 As String, ByVal Parameter2 As String, ByVal Parameter3 As String) As String
            Try
                If Not oConnection.GetConnection() Then
                    GetDataVClaimMonitoringDataKlaim = ""
                    Exit Function
                End If

                Dim HasilKey As String = ""

                ' Initialize the keyed hash object using the secret key as the key
                Dim hashObject As New HMACSHA256(Encoding.UTF8.GetBytes(SECREATKEY))
                ' Computes the signature by hashing the salt with the secret key as the key
                Dim signature = hashObject.ComputeHash(Encoding.UTF8.GetBytes(CONSID & "&" & uTime))
                ' Base 64 Encode
                HasilKey = Convert.ToBase64String(signature)

                Dim req As WinHttp.WinHttpRequest

                Dim Url As String = ALAMATWEB & "Monitoring/Klaim/Tanggal/" & Paramater1 & "/JnsPelayanan/" & Parameter2 & "/Status/" & Parameter3

                req = New WinHttp.WinHttpRequest
                req.Open("GET", Url, False)
                req.SetRequestHeader("X-cons-ID", CONSID)
                req.SetRequestHeader("X-timestamp", uTime)
                req.SetRequestHeader("X-signature", HasilKey)
                req.SetRequestHeader("user_key", USERKEY)
                req.SetRequestHeader("Content-Type", "application/json; charset=utf-8")

                req.Send()

                GetDataVClaimMonitoringDataKlaim = req.ResponseText

            Catch ex As Exception
                GetDataVClaimMonitoringDataKlaim = ex.ToString
                oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                Throw ex
            End Try
        End Function
        Public Function GetDataVClaimMonitoringDataHistoriPelayananPeserta(ByVal ALAMATWEB As String, ByVal CONSID As String, ByVal SECREATKEY As String, ByVal USERKEY As String, ByVal uTime As Integer, ByVal Paramater1 As String, ByVal Parameter2 As String, ByVal Parameter3 As String) As String
            Try
                If Not oConnection.GetConnection() Then
                    GetDataVClaimMonitoringDataHistoriPelayananPeserta = ""
                    Exit Function
                End If

                Dim HasilKey As String = ""

                ' Initialize the keyed hash object using the secret key as the key
                Dim hashObject As New HMACSHA256(Encoding.UTF8.GetBytes(SECREATKEY))
                ' Computes the signature by hashing the salt with the secret key as the key
                Dim signature = hashObject.ComputeHash(Encoding.UTF8.GetBytes(CONSID & "&" & uTime))
                ' Base 64 Encode
                HasilKey = Convert.ToBase64String(signature)

                Dim req As WinHttp.WinHttpRequest

                Dim Url As String = ALAMATWEB & "monitoring/HistoriPelayanan/NoKartu/" & Paramater1 & "/tglAwal/" & Parameter2 & "/tglAkhir/" & Parameter3

                req = New WinHttp.WinHttpRequest
                req.Open("GET", Url, False)
                req.SetRequestHeader("X-cons-ID", CONSID)
                req.SetRequestHeader("X-timestamp", uTime)
                req.SetRequestHeader("X-signature", HasilKey)
                req.SetRequestHeader("user_key", USERKEY)
                req.SetRequestHeader("Content-Type", "application/json; charset=utf-8")

                req.Send()

                GetDataVClaimMonitoringDataHistoriPelayananPeserta = req.ResponseText

            Catch ex As Exception
                GetDataVClaimMonitoringDataHistoriPelayananPeserta = ex.ToString
                oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                Throw ex
            End Try
        End Function
        Public Function GetDataVClaimMonitoringDataKlaimJaminanJasaRaharja(ByVal ALAMATWEB As String, ByVal CONSID As String, ByVal SECREATKEY As String, ByVal USERKEY As String, ByVal uTime As Integer, ByVal Parameter1 As String, ByVal Parameter2 As String) As String
            Try
                If Not oConnection.GetConnection() Then
                    GetDataVClaimMonitoringDataKlaimJaminanJasaRaharja = ""
                    Exit Function
                End If

                Dim HasilKey As String = ""

                ' Initialize the keyed hash object using the secret key as the key
                Dim hashObject As New HMACSHA256(Encoding.UTF8.GetBytes(SECREATKEY))
                ' Computes the signature by hashing the salt with the secret key as the key
                Dim signature = hashObject.ComputeHash(Encoding.UTF8.GetBytes(CONSID & "&" & uTime))
                ' Base 64 Encode
                HasilKey = Convert.ToBase64String(signature)

                Dim req As WinHttp.WinHttpRequest

                Dim Url As String = ALAMATWEB & "monitoring/JasaRaharja/tglMulai/" & Parameter1 & "/tglAkhir/" & Parameter2

                req = New WinHttp.WinHttpRequest
                req.Open("GET", Url, False)
                req.SetRequestHeader("X-cons-ID", CONSID)
                req.SetRequestHeader("X-timestamp", uTime)
                req.SetRequestHeader("X-signature", HasilKey)
                req.SetRequestHeader("user_key", USERKEY)
                req.SetRequestHeader("Content-Type", "application/json; charset=utf-8")

                req.Send()

                GetDataVClaimMonitoringDataKlaimJaminanJasaRaharja = req.ResponseText

            Catch ex As Exception
                GetDataVClaimMonitoringDataKlaimJaminanJasaRaharja = ex.ToString
                oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                Throw ex
            End Try
        End Function
#End Region
#Region "Rencana Kontrol"
        Public Function InsertRencanaKontrol(ByVal ALAMATWEB As String, ByVal CONSID As String, ByVal SECREATKEY As String, ByVal USERKEY As String, ByVal uTime As Integer, ByVal Request As String) As String
            Try
                If Not oConnection.GetConnection() Then
                    InsertRencanaKontrol = ""
                    Exit Function
                End If

                Dim HasilKey As String = ""

                ' Initialize the keyed hash object using the secret key as the key
                Dim hashObject As New HMACSHA256(Encoding.UTF8.GetBytes(SECREATKEY))
                ' Computes the signature by hashing the salt with the secret key as the key
                Dim signature = hashObject.ComputeHash(Encoding.UTF8.GetBytes(CONSID & "&" & uTime))
                ' Base 64 Encode
                HasilKey = Convert.ToBase64String(signature)

                Dim req As WinHttp.WinHttpRequest

                Dim Url As String = ALAMATWEB & "RencanaKontrol/insert"

                req = New WinHttp.WinHttpRequest
                req.Open("POST", Url, False)
                req.SetRequestHeader("X-cons-ID", CONSID)
                req.SetRequestHeader("X-timestamp", uTime)
                req.SetRequestHeader("X-signature", HasilKey)
                req.SetRequestHeader("user_key", USERKEY)
                req.SetRequestHeader("Content-Type", "Application/x-www-form-urlencoded")

                req.Send(Request)

                InsertRencanaKontrol = req.ResponseText

            Catch ex As Exception
                InsertRencanaKontrol = ex.ToString
                oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                Throw ex
            End Try
        End Function
        Public Function UpdateRencanaKontrol(ByVal ALAMATWEB As String, ByVal CONSID As String, ByVal SECREATKEY As String, ByVal USERKEY As String, ByVal uTime As Integer, ByVal Request As String) As String
            Try
                If Not oConnection.GetConnection() Then
                    UpdateRencanaKontrol = ""
                    Exit Function
                End If

                Dim HasilKey As String = ""

                ' Initialize the keyed hash object using the secret key as the key
                Dim hashObject As New HMACSHA256(Encoding.UTF8.GetBytes(SECREATKEY))
                ' Computes the signature by hashing the salt with the secret key as the key
                Dim signature = hashObject.ComputeHash(Encoding.UTF8.GetBytes(CONSID & "&" & uTime))
                ' Base 64 Encode
                HasilKey = Convert.ToBase64String(signature)

                Dim req As WinHttp.WinHttpRequest

                Dim Url As String = ALAMATWEB & "RencanaKontrol/Update"

                req = New WinHttp.WinHttpRequest
                req.Open("PUT", Url, False)
                req.SetRequestHeader("X-cons-ID", CONSID)
                req.SetRequestHeader("X-timestamp", uTime)
                req.SetRequestHeader("X-signature", HasilKey)
                req.SetRequestHeader("user_key", USERKEY)
                req.SetRequestHeader("Content-Type", "Application/x-www-form-urlencoded")

                req.Send(Request)

                UpdateRencanaKontrol = req.ResponseText

            Catch ex As Exception
                UpdateRencanaKontrol = ex.ToString
                oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                Throw ex
            End Try
        End Function
        Public Function HapusRencanaKontrol(ByVal ALAMATWEB As String, ByVal CONSID As String, ByVal SECREATKEY As String, ByVal USERKEY As String, ByVal uTime As Integer, ByVal Request As String) As String
            Try
                If Not oConnection.GetConnection() Then
                    HapusRencanaKontrol = ""
                    Exit Function
                End If

                Dim HasilKey As String = ""

                ' Initialize the keyed hash object using the secret key as the key
                Dim hashObject As New HMACSHA256(Encoding.UTF8.GetBytes(SECREATKEY))
                ' Computes the signature by hashing the salt with the secret key as the key
                Dim signature = hashObject.ComputeHash(Encoding.UTF8.GetBytes(CONSID & "&" & uTime))
                ' Base 64 Encode
                HasilKey = Convert.ToBase64String(signature)

                Dim req As WinHttp.WinHttpRequest

                Dim Url As String = ALAMATWEB & "RencanaKontrol/Delete"

                req = New WinHttp.WinHttpRequest
                req.Open("DELETE", Url, False)
                req.SetRequestHeader("X-cons-ID", CONSID)
                req.SetRequestHeader("X-timestamp", uTime)
                req.SetRequestHeader("X-signature", HasilKey)
                req.SetRequestHeader("user_key", USERKEY)
                req.SetRequestHeader("Content-Type", "Application/x-www-form-urlencoded")

                req.Send(Request)

                HapusRencanaKontrol = req.ResponseText

            Catch ex As Exception
                HapusRencanaKontrol = ex.ToString
                oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                Throw ex
            End Try
        End Function
        Public Function InsertSPRI(ByVal ALAMATWEB As String, ByVal CONSID As String, ByVal SECREATKEY As String, ByVal USERKEY As String, ByVal uTime As Integer, ByVal Request As String) As String
            Try
                If Not oConnection.GetConnection() Then
                    InsertSPRI = ""
                    Exit Function
                End If

                Dim HasilKey As String = ""

                ' Initialize the keyed hash object using the secret key as the key
                Dim hashObject As New HMACSHA256(Encoding.UTF8.GetBytes(SECREATKEY))
                ' Computes the signature by hashing the salt with the secret key as the key
                Dim signature = hashObject.ComputeHash(Encoding.UTF8.GetBytes(CONSID & "&" & uTime))
                ' Base 64 Encode
                HasilKey = Convert.ToBase64String(signature)

                Dim req As WinHttp.WinHttpRequest

                Dim Url As String = ALAMATWEB & "RencanaKontrol/InsertSPRI"

                req = New WinHttp.WinHttpRequest
                req.Open("POST", Url, False)
                req.SetRequestHeader("X-cons-ID", CONSID)
                req.SetRequestHeader("X-timestamp", uTime)
                req.SetRequestHeader("X-signature", HasilKey)
                req.SetRequestHeader("user_key", USERKEY)
                req.SetRequestHeader("Content-Type", "Application/x-www-form-urlencoded")

                req.Send(Request)

                InsertSPRI = req.ResponseText

            Catch ex As Exception
                InsertSPRI = ex.ToString
                oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                Throw ex
            End Try
        End Function
        Public Function UpdateSPRI(ByVal ALAMATWEB As String, ByVal CONSID As String, ByVal SECREATKEY As String, ByVal USERKEY As String, ByVal uTime As Integer, ByVal Request As String) As String
            Try
                If Not oConnection.GetConnection() Then
                    UpdateSPRI = ""
                    Exit Function
                End If

                Dim HasilKey As String = ""

                ' Initialize the keyed hash object using the secret key as the key
                Dim hashObject As New HMACSHA256(Encoding.UTF8.GetBytes(SECREATKEY))
                ' Computes the signature by hashing the salt with the secret key as the key
                Dim signature = hashObject.ComputeHash(Encoding.UTF8.GetBytes(CONSID & "&" & uTime))
                ' Base 64 Encode
                HasilKey = Convert.ToBase64String(signature)

                Dim req As WinHttp.WinHttpRequest

                Dim Url As String = ALAMATWEB & "RencanaKontrol/UpdateSPRI"

                req = New WinHttp.WinHttpRequest
                req.Open("PUT", Url, False)
                req.SetRequestHeader("X-cons-ID", CONSID)
                req.SetRequestHeader("X-timestamp", uTime)
                req.SetRequestHeader("X-signature", HasilKey)
                req.SetRequestHeader("user_key", USERKEY)
                req.SetRequestHeader("Content-Type", "Application/x-www-form-urlencoded")

                req.Send(Request)

                UpdateSPRI = req.ResponseText

            Catch ex As Exception
                UpdateSPRI = ex.ToString
                oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                Throw ex
            End Try
        End Function
        Public Function CariSEPSuratKontrolSPRI(ByVal ALAMATWEB As String, ByVal CONSID As String, ByVal SECREATKEY As String, ByVal USERKEY As String, ByVal uTime As Integer, ByVal Parameter As String) As String
            Try
                If Not oConnection.GetConnection() Then
                    CariSEPSuratKontrolSPRI = ""
                    Exit Function
                End If

                Dim HasilKey As String = ""

                ' Initialize the keyed hash object using the secret key as the key
                Dim hashObject As New HMACSHA256(Encoding.UTF8.GetBytes(SECREATKEY))
                ' Computes the signature by hashing the salt with the secret key as the key
                Dim signature = hashObject.ComputeHash(Encoding.UTF8.GetBytes(CONSID & "&" & uTime))
                ' Base 64 Encode
                HasilKey = Convert.ToBase64String(signature)

                Dim req As WinHttp.WinHttpRequest

                Dim Url As String = ALAMATWEB & "RencanaKontrol/nosep/" & Parameter

                req = New WinHttp.WinHttpRequest
                req.Open("GET", Url, False)
                req.SetRequestHeader("X-cons-ID", CONSID)
                req.SetRequestHeader("X-timestamp", uTime)
                req.SetRequestHeader("X-signature", HasilKey)
                req.SetRequestHeader("user_key", USERKEY)
                req.SetRequestHeader("Content-Type", "Application/x-www-form-urlencoded")

                req.Send()

                CariSEPSuratKontrolSPRI = req.ResponseText

            Catch ex As Exception
                CariSEPSuratKontrolSPRI = ex.ToString
                oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                Throw ex
            End Try
        End Function
        Public Function CariNomorRencanaKontrol(ByVal ALAMATWEB As String, ByVal CONSID As String, ByVal SECREATKEY As String, ByVal USERKEY As String, ByVal uTime As Integer, ByVal Parameter As String) As String
            Try
                If Not oConnection.GetConnection() Then
                    CariNomorRencanaKontrol = ""
                    Exit Function
                End If

                Dim HasilKey As String = ""

                ' Initialize the keyed hash object using the secret key as the key
                Dim hashObject As New HMACSHA256(Encoding.UTF8.GetBytes(SECREATKEY))
                ' Computes the signature by hashing the salt with the secret key as the key
                Dim signature = hashObject.ComputeHash(Encoding.UTF8.GetBytes(CONSID & "&" & uTime))
                ' Base 64 Encode
                HasilKey = Convert.ToBase64String(signature)

                Dim req As WinHttp.WinHttpRequest

                Dim Url As String = ALAMATWEB & "RencanaKontrol/noSuratKontrol/" & Parameter

                req = New WinHttp.WinHttpRequest
                req.Open("GET", Url, False)
                req.SetRequestHeader("X-cons-ID", CONSID)
                req.SetRequestHeader("X-timestamp", uTime)
                req.SetRequestHeader("X-signature", HasilKey)
                req.SetRequestHeader("user_key", USERKEY)
                req.SetRequestHeader("Content-Type", "Application/x-www-form-urlencoded")

                req.Send()

                CariNomorRencanaKontrol = req.ResponseText

            Catch ex As Exception
                CariNomorRencanaKontrol = ex.ToString
                oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                Throw ex
            End Try
        End Function
        Public Function DataNomorRencanaKontrol(ByVal ALAMATWEB As String, ByVal CONSID As String, ByVal SECREATKEY As String, ByVal USERKEY As String, ByVal uTime As Integer, ByVal Parameter1 As String, ByVal Parameter2 As String, ByVal Parameter3 As String) As String
            Try
                If Not oConnection.GetConnection() Then
                    DataNomorRencanaKontrol = ""
                    Exit Function
                End If

                Dim HasilKey As String = ""

                ' Initialize the keyed hash object using the secret key as the key
                Dim hashObject As New HMACSHA256(Encoding.UTF8.GetBytes(SECREATKEY))
                ' Computes the signature by hashing the salt with the secret key as the key
                Dim signature = hashObject.ComputeHash(Encoding.UTF8.GetBytes(CONSID & "&" & uTime))
                ' Base 64 Encode
                HasilKey = Convert.ToBase64String(signature)

                Dim req As WinHttp.WinHttpRequest

                Dim Url As String = ALAMATWEB & "RencanaKontrol/ListRencanaKontrol/tglAwal/" & Parameter1 & "/tglAkhir/" & Parameter2 & "/filter/" & Parameter3

                req = New WinHttp.WinHttpRequest
                req.Open("GET", Url, False)
                req.SetRequestHeader("X-cons-ID", CONSID)
                req.SetRequestHeader("X-timestamp", uTime)
                req.SetRequestHeader("X-signature", HasilKey)
                req.SetRequestHeader("user_key", USERKEY)
                req.SetRequestHeader("Content-Type", "Application/x-www-form-urlencoded")

                req.Send()

                DataNomorRencanaKontrol = req.ResponseText

            Catch ex As Exception
                DataNomorRencanaKontrol = ex.ToString
                oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                Throw ex
            End Try
        End Function
        Public Function DataPoliSpesialistik(ByVal ALAMATWEB As String, ByVal CONSID As String, ByVal SECREATKEY As String, ByVal USERKEY As String, ByVal uTime As Integer, ByVal Parameter1 As String, ByVal Parameter2 As String, ByVal Parameter3 As String) As String
            Try
                If Not oConnection.GetConnection() Then
                    DataPoliSpesialistik = ""
                    Exit Function
                End If

                Dim HasilKey As String = ""

                ' Initialize the keyed hash object using the secret key as the key
                Dim hashObject As New HMACSHA256(Encoding.UTF8.GetBytes(SECREATKEY))
                ' Computes the signature by hashing the salt with the secret key as the key
                Dim signature = hashObject.ComputeHash(Encoding.UTF8.GetBytes(CONSID & "&" & uTime))
                ' Base 64 Encode
                HasilKey = Convert.ToBase64String(signature)

                Dim req As WinHttp.WinHttpRequest

                Dim Url As String = ALAMATWEB & "RencanaKontrol/ListSpesialistik/JnsKontrol/" & Parameter1 & "/nomor/" & Parameter2 & "/TglRencanaKontrol/" & Parameter3

                req = New WinHttp.WinHttpRequest
                req.Open("GET", Url, False)
                req.SetRequestHeader("X-cons-ID", CONSID)
                req.SetRequestHeader("X-timestamp", uTime)
                req.SetRequestHeader("X-signature", HasilKey)
                req.SetRequestHeader("user_key", USERKEY)
                req.SetRequestHeader("Content-Type", "Application/x-www-form-urlencoded")

                req.Send()

                DataPoliSpesialistik = req.ResponseText

            Catch ex As Exception
                DataPoliSpesialistik = ex.ToString
                oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                Throw ex
            End Try
        End Function
        Public Function DataDokter(ByVal ALAMATWEB As String, ByVal CONSID As String, ByVal SECREATKEY As String, ByVal USERKEY As String, ByVal uTime As Integer, ByVal Parameter1 As String, ByVal Parameter2 As String, ByVal Parameter3 As String) As String
            Try
                If Not oConnection.GetConnection() Then
                    DataDokter = ""
                    Exit Function
                End If

                Dim HasilKey As String = ""

                ' Initialize the keyed hash object using the secret key as the key
                Dim hashObject As New HMACSHA256(Encoding.UTF8.GetBytes(SECREATKEY))
                ' Computes the signature by hashing the salt with the secret key as the key
                Dim signature = hashObject.ComputeHash(Encoding.UTF8.GetBytes(CONSID & "&" & uTime))
                ' Base 64 Encode
                HasilKey = Convert.ToBase64String(signature)

                Dim req As WinHttp.WinHttpRequest

                Dim Url As String = ALAMATWEB & "RencanaKontrol/JadwalPraktekDokter/JnsKontrol/" & Parameter1 & "/KdPoli/" & Parameter2 & "/TglRencanaKontrol/" & Parameter3

                req = New WinHttp.WinHttpRequest
                req.Open("GET", Url, False)
                req.SetRequestHeader("X-cons-ID", CONSID)
                req.SetRequestHeader("X-timestamp", uTime)
                req.SetRequestHeader("X-signature", HasilKey)
                req.SetRequestHeader("user_key", USERKEY)
                req.SetRequestHeader("Content-Type", "Application/x-www-form-urlencoded")

                req.Send()

                DataDokter = req.ResponseText

            Catch ex As Exception
                DataDokter = ex.ToString
                oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                Throw ex
            End Try
        End Function
#End Region
#End Region
        '#Region "Aplicare"
        '        Public Function GetDataAplicareReferensiKelasRawat(ByVal ALAMATWEB As String, ByVal CONSID As String, ByVal SECREATKEY As String, ByVal uTime As Integer) As String
        '            Try
        '                If Not oConnection.GetConnection() Then
        '                    GetDataAplicareReferensiKelasRawat = ""
        '                    Exit Function
        '                End If

        '                'Dim ds = oConnection.db.SET_KONEKSIs.FirstOrDefault(Function(x) x.NAME_DISPLAY = sNAME_DISPLAY And x.ISACTIVE = True)

        '                Dim HasilKey As String = ""
        '                Dim result As String = ""

        '                ' Initialize the keyed hash object using the secret key as the key
        '                Dim hashObject As New HMACSHA256(Encoding.UTF8.GetBytes(SECREATKEY))
        '                ' Computes the signature by hashing the salt with the secret key as the key
        '                Dim signature = hashObject.ComputeHash(Encoding.UTF8.GetBytes(CONSID & "&" & uTime))
        '                ' Base 64 Encode
        '                HasilKey = Convert.ToBase64String(signature)

        '                Dim req As WinHttp.WinHttpRequest

        '                Dim Url As String = ALAMATWEB & "rest/ref/kelas"

        '                req = New WinHttp.WinHttpRequest
        '                req.Open("GET", Url, False)
        '                req.SetRequestHeader("X-Cons-ID", CONSID)
        '                req.SetRequestHeader("X-Timestamp", uTime)
        '                req.SetRequestHeader("X-Signature", HasilKey)
        '                req.SetRequestHeader("Content-Type", "application/json")

        '                req.Send()

        '                result = req.ResponseText

        '                GetDataAplicareReferensiKelasRawat = result

        '            Catch ex As Exception
        '                GetDataAplicareReferensiKelasRawat = ex.ToString
        '                oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
        '                Throw ex
        '            End Try
        '        End Function
        '        Public Function UpdateKetersediaanTempatTidur(ByVal sKDKONEKSI As String, ByVal Request As String) As String
        '            Try
        '                Dim ds = oConnectionUser.db.SET_BPJS_KONEKSIs.FirstOrDefault(Function(x) x.KDKONEKSI = sKDKONEKSI And x.ISACTIVE = True)

        '                Dim uTime As Integer = 0
        '                Dim HasilKey As String = ""
        '                Dim result As String = ""

        '                If ds IsNot Nothing Then
        '                    uTime = (DateTime.UtcNow - New DateTime(1970, 1, 1, 0, 0, 0)).TotalSeconds

        '                    ' Initialize the keyed hash object using the secret key as the key
        '                    Dim hashObject As New HMACSHA256(Encoding.UTF8.GetBytes(ds.SECREATKEY))
        '                    ' Computes the signature by hashing the salt with the secret key as the key
        '                    Dim signature = hashObject.ComputeHash(Encoding.UTF8.GetBytes(ds.CONSID & "&" & uTime))
        '                    ' Base 64 Encode
        '                    HasilKey = Convert.ToBase64String(signature)

        '                    Dim req As WinHttp.WinHttpRequest

        '                    Dim Url As String = ds.ALAMATWEB & "rest/bed/update/" & ds.PPKPELAYANAN

        '                    req = New WinHttp.WinHttpRequest
        '                    req.Open("POST", Url, False)
        '                    req.SetRequestHeader("X-Cons-ID", ds.CONSID)
        '                    req.SetRequestHeader("X-Timestamp", uTime)
        '                    req.SetRequestHeader("X-Signature", HasilKey)
        '                    req.SetRequestHeader("Content-Type", "application/json")
        '                    req.Send(Request)
        '                    result = req.ResponseText

        '                End If

        '                UpdateKetersediaanTempatTidur = result

        '            Catch ex As Exception
        '                UpdateKetersediaanTempatTidur = ex.ToString
        '                oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
        '                Throw ex
        '            End Try
        '        End Function
        '        Public Function RuanganBaru(ByVal sNAME_DISPLAY As String, ByVal Request As String) As String
        '            Try
        '                If Not oConnection.GetConnection() Then
        '                    RuanganBaru = ""
        '                    Exit Function
        '                End If

        '                Dim ds = oConnection.db.SET_KONEKSIs.FirstOrDefault(Function(x) x.NAME_DISPLAY = sNAME_DISPLAY And x.ISACTIVE = True)

        '                Dim uTime As Integer = 0
        '                Dim HasilKey As String = ""
        '                Dim result As String = ""

        '                If ds IsNot Nothing Then
        '                    uTime = (DateTime.UtcNow - New DateTime(1970, 1, 1, 0, 0, 0)).TotalSeconds

        '                    ' Initialize the keyed hash object using the secret key as the key
        '                    Dim hashObject As New HMACSHA256(Encoding.UTF8.GetBytes(ds.SECREATKEY))
        '                    ' Computes the signature by hashing the salt with the secret key as the key
        '                    Dim signature = hashObject.ComputeHash(Encoding.UTF8.GetBytes(ds.CONSID & "&" & uTime))
        '                    ' Base 64 Encode
        '                    HasilKey = Convert.ToBase64String(signature)

        '                    Dim req As WinHttp.WinHttpRequest

        '                    Dim Url As String = ds.ALAMATWEB & "rest/bed/create/" & ds.PPKPELAYANAN

        '                    req = New WinHttp.WinHttpRequest
        '                    req.Open("POST", Url, False)
        '                    req.SetRequestHeader("X-Cons-ID", ds.CONSID)
        '                    req.SetRequestHeader("X-Timestamp", uTime)
        '                    req.SetRequestHeader("X-Signature", HasilKey)
        '                    req.SetRequestHeader("Content-Type", "application/json")
        '                    req.Send(Request)
        '                    result = req.ResponseText

        '                End If

        '                RuanganBaru = result

        '            Catch ex As Exception
        '                RuanganBaru = ex.ToString
        '                oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
        '                Throw ex
        '            End Try
        '        End Function
        '        Public Function RuanganHapus(ByVal sNAME_DISPLAY As String, ByVal Request As String) As String
        '            Try
        '                If Not oConnection.GetConnection() Then
        '                    RuanganHapus = ""
        '                    Exit Function
        '                End If

        '                Dim ds = oConnection.db.SET_KONEKSIs.FirstOrDefault(Function(x) x.NAME_DISPLAY = sNAME_DISPLAY And x.ISACTIVE = True)

        '                Dim uTime As Integer = 0
        '                Dim HasilKey As String = ""
        '                Dim result As String = ""

        '                If ds IsNot Nothing Then
        '                    uTime = (DateTime.UtcNow - New DateTime(1970, 1, 1, 0, 0, 0)).TotalSeconds

        '                    ' Initialize the keyed hash object using the secret key as the key
        '                    Dim hashObject As New HMACSHA256(Encoding.UTF8.GetBytes(ds.SECREATKEY))
        '                    ' Computes the signature by hashing the salt with the secret key as the key
        '                    Dim signature = hashObject.ComputeHash(Encoding.UTF8.GetBytes(ds.CONSID & "&" & uTime))
        '                    ' Base 64 Encode
        '                    HasilKey = Convert.ToBase64String(signature)

        '                    Dim req As WinHttp.WinHttpRequest

        '                    Dim Url As String = ds.ALAMATWEB & "rest/bed/delete/" & ds.PPKPELAYANAN

        '                    req = New WinHttp.WinHttpRequest
        '                    req.Open("POST", Url, False)
        '                    req.SetRequestHeader("X-Cons-ID", ds.CONSID)
        '                    req.SetRequestHeader("X-Timestamp", uTime)
        '                    req.SetRequestHeader("X-Signature", HasilKey)
        '                    req.SetRequestHeader("Content-Type", "application/json")
        '                    req.Send(Request)
        '                    result = req.ResponseText

        '                End If

        '                RuanganHapus = result

        '            Catch ex As Exception
        '                RuanganHapus = ex.ToString
        '                oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
        '                Throw ex
        '            End Try
        '        End Function
        '        Public Function KeterersediaanKamar(ByVal sNAME_DISPLAY As String, ByVal Start As Integer, ByVal Limit As Integer) As String
        '            Try
        '                If Not oConnection.GetConnection() Then
        '                    KeterersediaanKamar = ""
        '                    Exit Function
        '                End If

        '                Dim ds = oConnection.db.SET_KONEKSIs.FirstOrDefault(Function(x) x.NAME_DISPLAY = sNAME_DISPLAY And x.ISACTIVE = True)

        '                Dim uTime As Integer = 0
        '                Dim HasilKey As String = ""
        '                Dim result As String = ""

        '                If ds IsNot Nothing Then
        '                    uTime = (DateTime.UtcNow - New DateTime(1970, 1, 1, 0, 0, 0)).TotalSeconds

        '                    ' Initialize the keyed hash object using the secret key as the key
        '                    Dim hashObject As New HMACSHA256(Encoding.UTF8.GetBytes(ds.SECREATKEY))
        '                    ' Computes the signature by hashing the salt with the secret key as the key
        '                    Dim signature = hashObject.ComputeHash(Encoding.UTF8.GetBytes(ds.CONSID & "&" & uTime))
        '                    ' Base 64 Encode
        '                    HasilKey = Convert.ToBase64String(signature)

        '                    Dim req As WinHttp.WinHttpRequest

        '                    Dim Url As String = ds.ALAMATWEB & "rest/bed/read/" & ds.PPKPELAYANAN & "/" & Start & "/" & Limit

        '                    req = New WinHttp.WinHttpRequest
        '                    req.Open("GET", Url, False)
        '                    req.SetRequestHeader("X-Cons-ID", ds.CONSID)
        '                    req.SetRequestHeader("X-Timestamp", uTime)
        '                    req.SetRequestHeader("X-Signature", HasilKey)
        '                    req.SetRequestHeader("Content-Type", "application/json")

        '                    req.Send()

        '                    result = req.ResponseText

        '                End If

        '                KeterersediaanKamar = result

        '            Catch ex As Exception
        '                KeterersediaanKamar = ex.ToString
        '                oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
        '                Throw ex
        '            End Try
        '        End Function
        '        Public Function Agents(ByVal sNAME_DISPLAY As String, ByVal Request As String, ByVal Start As Integer, ByVal Limit As Integer) As String
        '            Try
        '                If Not oConnection.GetConnection() Then
        '                    Agents = ""
        '                    Exit Function
        '                End If

        '                Dim ds = oConnection.db.SET_KONEKSIs.FirstOrDefault(Function(x) x.NAME_DISPLAY = sNAME_DISPLAY And x.ISACTIVE = True)

        '                Dim uTime As Integer = 0
        '                Dim HasilKey As String = ""
        '                Dim result As String = ""

        '                If ds IsNot Nothing Then
        '                    uTime = (DateTime.UtcNow - New DateTime(1970, 1, 1, 0, 0, 0)).TotalSeconds

        '                    ' Initialize the keyed hash object using the secret key as the key
        '                    Dim hashObject As New HMACSHA256(Encoding.UTF8.GetBytes(ds.SECREATKEY))
        '                    ' Computes the signature by hashing the salt with the secret key as the key
        '                    Dim signature = hashObject.ComputeHash(Encoding.UTF8.GetBytes(ds.CONSID & "&" & uTime))
        '                    ' Base 64 Encode
        '                    HasilKey = Convert.ToBase64String(signature)

        '                    Dim req As WinHttp.WinHttpRequest

        '                    Dim Url As String = ds.ALAMATWEB & "rest/bed/read/" & ds.PPKPELAYANAN & "/" & Start & "/" & Limit

        '                    req = New WinHttp.WinHttpRequest
        '                    req.Open("GET", Url, False)
        '                    req.SetRequestHeader("X-Cons-ID", ds.CONSID)
        '                    req.SetRequestHeader("X-Timestamp", uTime)
        '                    req.SetRequestHeader("X-Signature", HasilKey)
        '                    req.SetRequestHeader("Content-Type", "application/json")
        '                    req.Send(Request)
        '                    result = req.ResponseText

        '                End If

        '                Agents = result

        '            Catch ex As Exception
        '                Agents = ex.ToString
        '                oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
        '                Throw ex
        '            End Try
        '        End Function
        '#End Region
#Region "E-KALIM"
        ' ENCRYPT
        Public Function inacbg_encrypt(text As String, key As String) As String
            Dim keys = Encoding.[Default].GetBytes(hex2bin(key))
            Dim aes As New AesCryptoServiceProvider()
            aes.BlockSize = 128
            aes.KeySize = 256
            aes.GenerateIV()
            Dim iv = aes.IV
            aes.Key = keys
            aes.Mode = CipherMode.CBC
            aes.Padding = PaddingMode.PKCS7
            Dim src As Byte() = Encoding.[Default].GetBytes(text)

            Using enc As ICryptoTransform = aes.CreateEncryptor()
                Dim data As Byte() = enc.TransformFinalBlock(src, 0, src.Length)
                Dim hashObject As New HMACSHA256(keys)
                Dim hash_sign = hashObject.ComputeHash(data)
                Dim signature As Byte() = New Byte(9) {}
                Array.Copy(hash_sign, 0, signature, 0, 10)
                Dim ret As Byte() = New Byte(signature.Length + iv.Length + (data.Length - 1)) {}
                Array.Copy(signature, 0, ret, 0, signature.Length)
                Array.Copy(iv, 0, ret, signature.Length, iv.Length)
                Array.Copy(data, 0, ret, signature.Length + iv.Length, data.Length)
                Return Convert.ToBase64String(ret)
            End Using

        End Function
        ' DECRYPT   
        Public Function inacbg_decrypt(strencrypt As String, key As String) As String
            Dim encoded_str As String = strencrypt
            Dim chiper As Byte() = Convert.FromBase64String(encoded_str)
            Dim length = chiper.Length
            Dim new_byte_iv As Byte() = New Byte(15) {}
            Dim new_byte_msg As Byte() = New Byte(length - 27) {}
            Array.Copy(chiper, 10, new_byte_iv, 0, 16)
            Array.Copy(chiper, 26, new_byte_msg, 0, length - 26)
            Dim byte_key As Byte() = Encoding.[Default].GetBytes(hex2bin(key))
            Dim aes As New RijndaelManaged()
            aes.KeySize = 256
            aes.BlockSize = 128
            aes.Padding = PaddingMode.PKCS7
            aes.Mode = CipherMode.CBC
            aes.Key = byte_key
            aes.IV = new_byte_iv
            Dim AESDecrypt As ICryptoTransform = aes.CreateDecryptor(aes.Key, aes.IV)
            Return Encoding.[Default].GetString(AESDecrypt.TransformFinalBlock(new_byte_msg, 0, new_byte_msg.Length))
        End Function
        Private Shared Function hex2bin(input As String) As String
            input = input.Replace("-", "")
            Dim raw As Byte() = New Byte(input.Length / 2 - 1) {}
            For i As Integer = 0 To raw.Length - 1
                raw(i) = Convert.ToByte(input.Substring(i * 2, 2), 16)
            Next
            Return Encoding.[Default].GetString(raw)
        End Function
        Public Function fn_GetRequsetEklaim(ByVal Request As String) As String
            Try
                Dim ds = oBPJSKoneksi.GetData("ECLAIM")
                Dim result As String = String.Empty

                If ds IsNot Nothing Then
                    Dim req As WinHttp.WinHttpRequest
                    Dim jsonEncode As String = String.Empty
                    Dim JsonEncrypt As String = String.Empty
                    Dim JsonDecrypt As String = String.Empty

                    jsonEncode = Request

                    JsonEncrypt = inacbg_encrypt(jsonEncode, ds.REMARKS)

                    req = New WinHttp.WinHttpRequest
                    req.Open("GET", ds.ALAMATWEB, False)
                    req.Send(JsonEncrypt)

                    If req.Status = "200" Then
                        result = req.ResponseText

                        Dim HasilResult As String

                        HasilResult = result.Replace("----BEGIN ENCRYPTED DATA----", "")

                        HasilResult = HasilResult.Replace("----END ENCRYPTED DATA----", "")

                        fn_GetRequsetEklaim = inacbg_decrypt(HasilResult, ds.REMARKS)
                    Else
                        fn_GetRequsetEklaim = "xx400-" & req.ResponseText
                    End If
                Else
                    fn_GetRequsetEklaim = ""
                End If

            Catch ex As Exception
                fn_GetRequsetEklaim = ex.ToString
                oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                Throw ex
            End Try
        End Function
        Public Function fn_Membuatklaimbaru(ByVal Request As String) As String
            Try
                Dim ds = oBPJSKoneksi.GetData("ECLAIM")
                Dim result As String = String.Empty

                If ds IsNot Nothing Then
                    Dim req As WinHttp.WinHttpRequest
                    Dim jsonEncode As String = String.Empty
                    Dim JsonEncrypt As String = String.Empty
                    Dim JsonDecrypt As String = String.Empty

                    jsonEncode = Request

                    JsonEncrypt = inacbg_encrypt(jsonEncode, ds.REMARKS)

                    req = New WinHttp.WinHttpRequest
                    req.Open("POST", ds.ALAMATWEB, False)
                    req.Send(JsonEncrypt)

                    If req.Status = "200" Then
                        result = req.ResponseText

                        Dim HasilResult As String

                        HasilResult = result.Replace("----BEGIN ENCRYPTED DATA----", "")

                        HasilResult = HasilResult.Replace("----END ENCRYPTED DATA----", "")

                        fn_Membuatklaimbaru = inacbg_decrypt(HasilResult, ds.REMARKS)
                    Else
                        fn_Membuatklaimbaru = "xx400-" & req.ResponseText
                    End If
                Else
                    fn_Membuatklaimbaru = ""
                End If

            Catch ex As Exception
                fn_Membuatklaimbaru = ex.ToString
                oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                Throw ex
            End Try
        End Function
        Public Function fn_UpdateDataPasien(ByVal Request As String) As String
            Try
                Dim ds = oBPJSKoneksi.GetData("ECLAIM")
                Dim result As String = String.Empty

                If ds IsNot Nothing Then
                    Dim req As WinHttp.WinHttpRequest
                    Dim jsonEncode As String = String.Empty
                    Dim JsonEncrypt As String = String.Empty
                    Dim JsonDecrypt As String = String.Empty

                    jsonEncode = Request

                    JsonEncrypt = inacbg_encrypt(jsonEncode, ds.REMARKS)

                    req = New WinHttp.WinHttpRequest
                    req.Open("POST", ds.ALAMATWEB, False)
                    req.Send(JsonEncrypt)

                    If req.Status = "200" Then
                        result = req.ResponseText

                        Dim HasilResult As String

                        HasilResult = result.Replace("----BEGIN ENCRYPTED DATA----", "")

                        HasilResult = HasilResult.Replace("----END ENCRYPTED DATA----", "")

                        fn_UpdateDataPasien = inacbg_decrypt(HasilResult, ds.REMARKS)
                    Else
                        fn_UpdateDataPasien = ""
                    End If
                Else
                    fn_UpdateDataPasien = ""
                End If

            Catch ex As Exception
                fn_UpdateDataPasien = ex.ToString
                oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                Throw ex
            End Try
        End Function
        Public Function fn_MengisiUpdateDataKlaim(ByVal Request As String) As String
            Try
                Dim ds = oBPJSKoneksi.GetData("ECLAIM")
                Dim result As String = String.Empty

                If ds IsNot Nothing Then
                    Dim req As WinHttp.WinHttpRequest
                    Dim jsonEncode As String = String.Empty
                    Dim JsonEncrypt As String = String.Empty
                    Dim JsonDecrypt As String = String.Empty

                    jsonEncode = Request

                    JsonEncrypt = inacbg_encrypt(jsonEncode, ds.REMARKS)

                    req = New WinHttp.WinHttpRequest
                    req.Open("POST", ds.ALAMATWEB, False)
                    req.Send(JsonEncrypt)

                    If req.Status = "200" Then
                        result = req.ResponseText

                        Dim HasilResult As String

                        HasilResult = result.Replace("----BEGIN ENCRYPTED DATA----", "")

                        HasilResult = HasilResult.Replace("----END ENCRYPTED DATA----", "")

                        fn_MengisiUpdateDataKlaim = inacbg_decrypt(HasilResult, ds.REMARKS)
                    Else
                        fn_MengisiUpdateDataKlaim = ""
                    End If
                Else
                    fn_MengisiUpdateDataKlaim = ""
                End If

            Catch ex As Exception
                fn_MengisiUpdateDataKlaim = ex.ToString
                oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                Throw ex
            End Try
        End Function
        Public Function fn_GroupingStage(ByVal Request As String) As String
            Try
                Dim ds = oBPJSKoneksi.GetData("ECLAIM")

                If ds IsNot Nothing Then
                    Dim req As WinHttp.WinHttpRequest
                    Dim jsonEncode As String
                    Dim JsonEncrypt As String
                    Dim JsonDecrypt As String
                    Dim Result As String = String.Empty

                    jsonEncode = Request

                    JsonEncrypt = inacbg_encrypt(jsonEncode, ds.REMARKS)

                    req = New WinHttp.WinHttpRequest
                    req.Open("POST", ds.ALAMATWEB, False)
                    req.Send(JsonEncrypt)

                    If req.Status = "200" Then
                        Result = req.ResponseText

                        Dim HasilResult As String

                        HasilResult = Result.Replace("----BEGIN ENCRYPTED DATA----", "")

                        HasilResult = HasilResult.Replace("----END ENCRYPTED DATA----", "")

                        JsonDecrypt = inacbg_decrypt(HasilResult, ds.REMARKS)

                        fn_GroupingStage = JsonDecrypt

                    Else
                        fn_GroupingStage = "Grouping Statge1, Status Tidak 200" & vbCrLf & req.Status & "-" & req.StatusText
                    End If
                Else
                    fn_GroupingStage = ""
                End If
            Catch oErr As Exception
                fn_GroupingStage = ""
                fn_GroupingStage = "Grouping Stage 1 Gagal : " & vbCrLf & oErr.Message
            End Try
        End Function
        Public Function fn_Pencariandiagnosa(ByVal keyword As String) As String
            Try
                Dim ds = oBPJSKoneksi.GetData("ECLAIM")

                Dim result As String = String.Empty

                If ds IsNot Nothing Then
                    Dim req As WinHttp.WinHttpRequest
                    Dim jsonEncode As String = String.Empty
                    Dim JsonEncrypt As String = String.Empty
                    Dim JsonDecrypt As String = String.Empty

                    Dim Request As String = "{" & """metadata"": {" & """method"": " & """search_diagnosis""}," & """data"": {" & """keyword"": """ & keyword & """ } } "

                    jsonEncode = Request

                    JsonEncrypt = inacbg_encrypt(jsonEncode, ds.REMARKS)

                    req = New WinHttp.WinHttpRequest
                    req.Open("POST", ds.ALAMATWEB, False)
                    req.Send(JsonEncrypt)

                    If req.Status = "200" Then
                        result = req.ResponseText

                        Dim HasilResult As String

                        HasilResult = result.Replace("----BEGIN ENCRYPTED DATA----", "")

                        HasilResult = HasilResult.Replace("----END ENCRYPTED DATA----", "")

                        fn_Pencariandiagnosa = inacbg_decrypt(HasilResult, ds.REMARKS)
                    Else
                        fn_Pencariandiagnosa = ""
                    End If
                Else
                    fn_Pencariandiagnosa = ""
                End If

            Catch ex As Exception
                fn_Pencariandiagnosa = ex.ToString
                oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                Throw ex
            End Try
        End Function
        Public Function fn_PencarianProsedur(ByVal sNAME_DISPLAY As String, ByVal keyword As String) As String
            Try
                Dim ds = oBPJSKoneksi.GetData("ECLAIM")

                Dim result As String = String.Empty

                If ds IsNot Nothing Then
                    Dim req As WinHttp.WinHttpRequest
                    Dim jsonEncode As String = String.Empty
                    Dim JsonEncrypt As String = String.Empty
                    Dim JsonDecrypt As String = String.Empty

                    Dim Request As String = "{" & """metadata"": {" & """method"": " & """search_procedures""}," & """data"": {" & """keyword"": """ & keyword & """ } } "

                    jsonEncode = Request

                    JsonEncrypt = inacbg_encrypt(jsonEncode, ds.REMARKS)

                    req = New WinHttp.WinHttpRequest
                    req.Open("POST", ds.ALAMATWEB, False)
                    req.Send(JsonEncrypt)

                    If req.Status = "200" Then
                        result = req.ResponseText

                        Dim HasilResult As String

                        HasilResult = result.Replace("----BEGIN ENCRYPTED DATA----", "")

                        HasilResult = HasilResult.Replace("----END ENCRYPTED DATA----", "")

                        fn_PencarianProsedur = inacbg_decrypt(HasilResult, ds.REMARKS)
                    Else
                        fn_PencarianProsedur = ""
                    End If
                Else
                    fn_PencarianProsedur = ""
                End If

            Catch ex As Exception
                fn_PencarianProsedur = ex.ToString
                oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                Throw ex
            End Try
        End Function
        Public Function fn_claim_print(ByVal keyword As String) As String
            Try
                Dim result As String = String.Empty

                Dim req As WinHttp.WinHttpRequest
                Dim jsonEncode As String = String.Empty
                Dim JsonEncrypt As String = String.Empty
                Dim JsonDecrypt As String = String.Empty

                jsonEncode = "{   " & """metadata"": {      " & """method"": " & """claim_print""   },   " & """data"": {     " & """nomor_sep"": """ & keyword & """   } } "

                JsonEncrypt = inacbg_encrypt(jsonEncode, "db16cba02244a6bcb769ba2f735cf1d8b2cfdb8daab50e1a51775248cf111a11")

                req = New WinHttp.WinHttpRequest
                req.Open("GET", "http://192.168.2.57/E-Klaim/ws.php/", False)
                req.Send(JsonEncrypt)

                If req.Status = "200" Then
                    result = req.ResponseText

                    Dim HasilResult As String

                    HasilResult = result.Replace("----BEGIN ENCRYPTED DATA----", "")

                    HasilResult = HasilResult.Replace("----END ENCRYPTED DATA----", "")

                    fn_claim_print = inacbg_decrypt(HasilResult, "db16cba02244a6bcb769ba2f735cf1d8b2cfdb8daab50e1a51775248cf111a11")
                Else
                    fn_claim_print = ""
                End If
            Catch ex As Exception
                fn_claim_print = ex.ToString
                oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                Throw ex
            End Try
        End Function
#Region "idRG"
        Public Function fn_BriggingEKlaim(ByVal ALAMATWEB As String, ByVal REMARKS As String, ByVal Request As String) As String
            Try
                Dim result As String = String.Empty
                Dim req As WinHttp.WinHttpRequest
                Dim jsonEncode As String = String.Empty
                Dim JsonEncrypt As String = String.Empty
                Dim JsonDecrypt As String = String.Empty

                jsonEncode = Request

                JsonEncrypt = inacbg_encrypt(jsonEncode, REMARKS)

                req = New WinHttp.WinHttpRequest
                req.Open("POST", ALAMATWEB, False)
                req.Send(JsonEncrypt)

                If req.Status = "200" Then
                    result = req.ResponseText

                    Dim HasilResult As String

                    HasilResult = result.Replace("----BEGIN ENCRYPTED DATA----", "")

                    HasilResult = HasilResult.Replace("----END ENCRYPTED DATA----", "")

                    fn_BriggingEKlaim = inacbg_decrypt(HasilResult, REMARKS)
                Else
                    fn_BriggingEKlaim = ""
                End If
            Catch ex As Exception
                fn_BriggingEKlaim = "ERORSIMRS" & ex.ToString
                Throw ex
            End Try
        End Function
#End Region
#End Region
#Region "Web Service Antrean - BPJS"
        Public Function ReferensiJadwalDokter(ByVal ALAMATWEB As String, ByVal CONSID As String, ByVal SECREATKEY As String, ByVal uTime As Integer, ByVal Parameter1 As String, ByVal Parameter2 As String) As String
            Try
                If Not oConnection.GetConnection() Then
                    ReferensiJadwalDokter = ""
                    Exit Function
                End If

                Dim HasilKey As String = ""

                ' Initialize the keyed hash object using the secret key as the key
                Dim hashObject As New HMACSHA256(Encoding.UTF8.GetBytes(SECREATKEY))
                ' Computes the signature by hashing the salt with the secret key as the key
                Dim signature = hashObject.ComputeHash(Encoding.UTF8.GetBytes(CONSID & "&" & uTime))
                ' Base 64 Encode
                HasilKey = Convert.ToBase64String(signature)

                Dim req As WinHttp.WinHttpRequest

                Dim Url As String = ALAMATWEB & "jadwaldokter/kodepoli/" & Parameter1 & "/tanggal/" & Parameter2

                req = New WinHttp.WinHttpRequest
                req.Open("GET", Url, False)
                req.SetRequestHeader("X-Cons-ID", CONSID)
                req.SetRequestHeader("X-Timestamp", uTime)
                req.SetRequestHeader("X-Signature", HasilKey)
                req.SetRequestHeader("Content-Type", "Application/x-www-form-urlencoded")

                req.Send()

                ReferensiJadwalDokter = req.ResponseText

            Catch ex As Exception
                ReferensiJadwalDokter = ex.ToString
                oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                Throw ex
            End Try
        End Function
#End Region
        'Public Function InsertData(ByVal entity As SET_KONEKSI) As Boolean
        '    Try
        '        If Not oConnection.GetConnection() Then
        '            InsertData = False
        '            Exit Function
        '        End If

        '        sREFERENCE = entity.KDKONEKSI
        '        sSTATUS = "INSERT"

        '        'Generate Auto Number
        '        Try
        '            sLASTNUMBER = CInt(oConnection.db.SET_KONEKSIs.OrderByDescending(Function(x) x.KDKONEKSI).FirstOrDefault().KDKONEKSI.Remove(0, (sMODUL & " _ ").Length)) + 1
        '        Catch ex As Exception
        '            sLASTNUMBER = 1
        '        End Try
        '        'End Generate

        '        Try
        '            entity.KDKONEKSI = sMODUL & "_" & AutoNumberCode(sLASTNUMBER)
        '            oConnection.db.SET_KONEKSIs.InsertOnSubmit(entity)
        '        Catch ex As Exception
        '            oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
        '            Throw ex
        '        End Try
        '        Try
        '            oConnection.db.SubmitChanges()
        '        Catch ex As Exception
        '            oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
        '            Throw ex
        '        End Try

        '        InsertData = True
        '    Catch ex As Exception
        '        InsertData = False
        '        oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
        '        Throw ex
        '    End Try
        'End Function
        'Public Function UpdateData(ByVal entity As SET_KONEKSI) As Boolean
        '    Try
        '        If Not oConnection.GetConnection() Then
        '            UpdateData = False
        '            Exit Function
        '        End If

        '        sREFERENCE = entity.KDKONEKSI
        '        sSTATUS = "UPDATE"

        '        Dim ds = oConnection.db.SET_KONEKSIs.FirstOrDefault(Function(x) x.KDKONEKSI = entity.KDKONEKSI)

        '        Try
        '            oConnection.db.SET_KONEKSIs.DeleteOnSubmit(ds)
        '            oConnection.db.SET_KONEKSIs.InsertOnSubmit(entity)
        '        Catch ex As Exception
        '            oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
        '            Throw ex
        '        End Try
        '        Try
        '            oConnection.db.SubmitChanges()
        '        Catch ex As Exception
        '            oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
        '            Throw ex
        '        End Try

        '        UpdateData = True
        '    Catch ex As Exception
        '        UpdateData = False
        '        oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
        '        Throw ex
        '    End Try
        'End Function
        'Public Function DeleteData(ByVal sKDKONEKSI As String) As Boolean
        '    Try
        '        If Not oConnection.GetConnection() Then
        '            DeleteData = False
        '            Exit Function
        '        End If

        '        sREFERENCE = sKDKONEKSI
        '        sSTATUS = "DELETE"

        '        Dim ds = oConnection.db.SET_KONEKSIs.FirstOrDefault(Function(x) x.KDKONEKSI = sKDKONEKSI)

        '        Try
        '            oConnection.db.SET_KONEKSIs.DeleteOnSubmit(ds)
        '        Catch ex As Exception
        '            oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
        '            Throw ex
        '        End Try
        '        Try
        '            oConnection.db.SubmitChanges()
        '        Catch ex As Exception
        '            oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
        '            Throw ex
        '        End Try

        '        DeleteData = True
        '    Catch ex As Exception
        '        DeleteData = False
        '        oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
        '        Throw ex
        '    End Try
        'End Function
        Public Function UpdateWaktuAntrean(ByVal ALAMATWEB As String, ByVal CONSID As String, ByVal SECREATKEY As String, ByVal USERKEY As String, ByVal uTime As Integer, ByVal Request As String) As String
            Try
                If Not oConnection.GetConnection() Then
                    UpdateWaktuAntrean = ""
                    Exit Function
                End If

                Dim HasilKey As String = ""

                ' Initialize the keyed hash object using the secret key as the key
                Dim hashObject As New HMACSHA256(Encoding.UTF8.GetBytes(SECREATKEY))
                ' Computes the signature by hashing the salt with the secret key as the key
                Dim signature = hashObject.ComputeHash(Encoding.UTF8.GetBytes(CONSID & "&" & uTime))
                ' Base 64 Encode
                HasilKey = Convert.ToBase64String(signature)

                Dim req As WinHttp.WinHttpRequest

                Dim Url As String = ALAMATWEB & "antrean/updatewaktu"

                req = New WinHttp.WinHttpRequest
                req.Open("POST", Url, False)
                req.SetRequestHeader("x-cons-id", CONSID)
                req.SetRequestHeader("x-timestamp", uTime)
                req.SetRequestHeader("x-signature", HasilKey)
                req.SetRequestHeader("user_key", USERKEY)

                req.Send(Request)

                UpdateWaktuAntrean = req.ResponseText

            Catch ex As Exception
                UpdateWaktuAntrean = ex.ToString
                oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                Throw ex
            End Try
        End Function

#Region "API RADIOLOGI"
        Public Function GetDataRadiologi(ByVal Parameter As String, ByVal Url As String) As String
            Try
                If Not oConnection.GetConnection() Then
                    GetDataRadiologi = ""
                    Exit Function
                End If

                'Dim ds = oConnection.db.SET_KONEKSIs.FirstOrDefault(Function(x) x.NAME_DISPLAY = sNAME_DISPLAY And x.ISACTIVE = True)

                Dim HasilKey As String = ""
                Dim result As String = ""

                '' Initialize the keyed hash object using the secret key as the key
                'Dim hashObject As New HMACSHA256(Encoding.UTF8.GetBytes(SECREATKEY))
                '' Computes the signature by hashing the salt with the secret key as the key
                'Dim signature = hashObject.ComputeHash(Encoding.UTF8.GetBytes(CONSID & "&" & uTime))
                '' Base 64 Encode
                'HasilKey = Convert.ToBase64String(signature)

                Dim req As WinHttp.WinHttpRequest

                'Dim Url As String = "http://203.210.87.29:4000/tele.indotelemed/api/" & Parameter

                req = New WinHttp.WinHttpRequest
                req.Open("GET", Url & Parameter, False)
                req.SetRequestHeader("x-token", "0d25b85a572f63723a795d0dr87a78df879c8086d090807d46d567")

                req.Send()

                result = req.ResponseText

                GetDataRadiologi = result


                '                {
                '    "response": {
                '        "StudyDate": "13-09-2023 17:14",
                '        "OrderNumber": "R23.07694",
                '        "MedrekNumber": "377390",
                '        "StudyInstanceUID": "1.2.276.0.7230010.3.0.3.5.1.13986826.3024988339",
                '        "LinkSatuSehat": "http://192.168.2.63/tele.indotelemed/satusehat/send/1.2.276.0.7230010.3.0.3.5.1.13986826.3024988339",
                '        "Dicom": [
                '            {
                '                "SOPInstanceUID": "1.2.276.0.7230010.3.0.3.5.1.13986826.1324056798"
                '            }
                '        ],
                '        "PatientName": "JEJEN JAENUDIN TN",
                '        "Age": "51 Year",
                '        "Gender": "M",
                '        "BodyPart": "CHEST",
                '        "Modality": "DX",
                '        "Status": "Final",
                '        "ReferringDoctor": "dr. Yovita",
                '        "Radiologist": "dr. Elgie Aulia Syawala, Sp.Rad",
                '        "Diagnose": "GERD",
                '        "Expertise": "<p>TS YTH</p><p>Cor, sinuses dan diaphragmae dan sinuses normal.<br></p><p>Pulmo</p><p>Hilus dalam batas normal</p><p>Tampak perselubungan opak inhomogen di lapang tengah paru bilateral</p><p>corakan bronchovaskuler bertambah</p><p>Kesan:&nbsp;<br></p><p>Pneumonia bilateral</p><p>Tidak tampak kardiomegali</p>",
                '        "Normal": "",
                '        "LinkViewer": "http://192.168.2.63:5001/viewer?StudyInstanceUIDs=1.2.276.0.7230010.3.0.3.5.1.13986826.3024988339",
                '        "image": [
                '            "http://192.168.2.63/tele.indotelemed/img/1.2.276.0.7230010.3.0.3.5.1.13986826.3024988339/1.2.276.0.7230010.3.0.3.5.1.13986826.1324056798.png"
                '        ]
                '    },
                '    "metaData": {
                '        "message": "OK",
                '        "code": "200"
                '    }
                '}
            Catch ex As Exception
                GetDataRadiologi = ""
                MsgBox("Eror" & ex.Message, MsgBoxStyle.Exclamation)
            End Try
        End Function
#End Region
#Region "ICARE"
        Public Function GetDataIcare(ByVal secretKey As String, ByVal NoKartuBPJS As String, ByVal kodedokter As Integer, ByVal uTime As Integer) As String
            Try
                If Not oConnection.GetConnection() Then
                    GetDataIcare = ""
                    Exit Function
                End If

                Dim HasilKey As String = ""
                Dim data = "16694" & "&" & uTime
                'Dim secretKey = "9kODD4D793"

                ' Initialize the keyed hash object using the secret key as the key
                Dim hashObject As New HMACSHA256(Encoding.UTF8.GetBytes(secretKey))
                ' Computes the signature by hashing the salt with the secret key as the key
                Dim signature = hashObject.ComputeHash(Encoding.UTF8.GetBytes(data))
                ' Base 64 Encode
                HasilKey = Convert.ToBase64String(signature)

                Dim req As WinHttp.WinHttpRequest

                Dim Url As String = "https://apijkn.bpjs-kesehatan.go.id/wsihs/api/rs/validate"

                req = New WinHttp.WinHttpRequest
                req.Open("POST", Url, False)
                req.SetRequestHeader("X-cons-id", "16694")
                req.SetRequestHeader("X-timestamp", uTime)
                req.SetRequestHeader("X-signature", HasilKey)
                req.SetRequestHeader("user_key", "fa4413a9b1bce2453cfced9011733778")
                req.SetRequestHeader("Content-Type", "application/json")

                Dim jsonRequest As String = String.Empty

                jsonRequest = " { "
                jsonRequest &= """param"": """ & NoKartuBPJS & ""","
                jsonRequest &= """kodedokter"": " & kodedokter & " "
                jsonRequest &= "}  "

                req.Send(jsonRequest)

                GetDataIcare = req.ResponseText

            Catch ex As Exception
                GetDataIcare = ex.ToString
                oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                Throw ex
            End Try
        End Function
        Public Function MergeFilesByte(ByVal sourceFiles As List(Of Byte())) As Byte()
            Try
                Dim mergedPdf As Byte() = Nothing
                Using ms As New MemoryStream()
                    Using document As New Document()
                        Using copy As New PdfCopy(document, ms)
                            document.Open()
                            For i As Integer = 0 To sourceFiles.Count - 1
                                Dim reader As New PdfReader(sourceFiles(i))
                                ' loop over the pages in that document
                                Dim n As Integer = reader.NumberOfPages
                                Dim page As Integer = 0
                                While page < n
                                    page = page + 1
                                    copy.AddPage(copy.GetImportedPage(reader, page))
                                End While
                            Next
                        End Using
                    End Using

                    mergedPdf = ms.ToArray()

                    Return mergedPdf

                End Using
            Catch ex As Exception
                MergeFilesByte = Nothing
                MsgBox("Load Merge Data : " & vbCrLf & ex.Message, MsgBoxStyle.Information)
            End Try
        End Function
#End Region
    End Class
End Namespace