Imports System
Imports System.Text
Imports System.Runtime.InteropServices

Public Class SatusehatAuth
#Region "bak"
    'Public Shared Function GetAccessToken(production As Boolean, clientId As String, clientSecret As String) As String
    '    ' URL Endpoint (Ganti dengan Production jika diperlukan)
    '    ' Production: https://api-satusehat.kemkes.go.id/oauth2/v1/accesstoken
    '    'Dim tokenUrl As String = "https://api-satusehat-stg.dto.kemkes.go.id/oauth2/v1/accesstoken?grant_type=client_credentials"

    '    Dim tokenUrl As String = String.Empty

    '    If production = False Then
    '        tokenUrl = "https://api-satusehat-stg.dto.kemkes.go.id/oauth2/v1/accesstoken?grant_type=client_credentials"
    '    Else
    '        tokenUrl = "https://api-satusehat.kemkes.go.id/oauth2/v1/accesstoken?grant_type=client_credentials"
    '    End If

    '    ' Siapkan data POST dalam format application/x-www-form-urlencoded
    '    Dim postData As String = $"client_id={clientId}&client_secret={clientSecret}"

    '    ' Buat objek WinHttpRequest
    '    Dim http As New WinHttp.WinHttpRequest()

    '    Try
    '        ' Buka koneksi (method POST, URL, async=False)
    '        http.Open("POST", tokenUrl, False)

    '        ' Set header Content-Type
    '        http.SetRequestHeader("Content-Type", "application/x-www-form-urlencoded")

    '        ' Kirim request dengan data POST
    '        http.Send(postData)

    '        ' Cek status response
    '        Dim statusCode As Integer = http.Status
    '        Dim responseText As String = http.ResponseText

    '        If statusCode = 200 Then
    '            ' Parse JSON response untuk mengambil access_token
    '            Dim token As String = responseText

    '            If Not String.IsNullOrEmpty(token) Then
    '                Return token
    '            Else
    '                'Console.WriteLine("Access token tidak ditemukan dalam response")
    '                Return token
    '            End If
    '        Else
    '            'Console.WriteLine($"Gagal mendapatkan token. HTTP Status: {statusCode}")
    '            'Console.WriteLine($"Response: {responseText}")

    '            Dim respon As String = $"Error mendapatkan token. HTTP Status: {statusCode}" & $"Response: {responseText}"

    '            Return respon
    '        End If

    '    Catch ex As Exception
    '        'Console.WriteLine($"Error saat request token: {ex.Message}")
    '        Return ($"Error saat request token: {ex.Message}")
    '    Finally
    '        ' Bersihkan resource COM
    '        If http IsNot Nothing Then
    '            Marshal.ReleaseComObject(http)
    '        End If
    '    End Try
    'End Function
    'Public Shared Function SearchPatientByNikAndBirthdate(production As Boolean, accessToken As String, nikIbu As String, birthdate As String) As String
    '    Dim http As New WinHttp.WinHttpRequest()

    '    Try
    '        'https://api-satusehat-stg.dto.kemkes.go.id/fhir-r4/v1/Patient?identifier=https://fhir.kemkes.go.id/id/nik-ibu|9104025209000006&birthdate=2024-12-09

    '        ' Base URL (Staging/Production)
    '        Dim baseUrl As String = ""

    '        ' Format identifier NIK Ibu
    '        Dim identifier As String = $"https://fhir.kemkes.go.id/id/nik-ibu|{nikIbu}"

    '        ' Build URL dengan parameter

    '        If production = False Then
    '            baseUrl = "https://api-satusehat-stg.dto.kemkes.go.id/fhir-r4/v1/Patient?identifier=https://fhir.kemkes.go.id"
    '        Else
    '            baseUrl = "https://api-satusehat.kemkes.go.id/fhir-r4/v1/Patient?identifier=https://fhir.kemkes.go.id"
    '        End If

    '        Dim url As String = $"{baseUrl}/Patient?identifier={identifier}&birthdate={birthdate}"

    '        ' URL Encode parameter
    '        url = Uri.EscapeUriString(url)

    '        ' Buat request
    '        http.Open("GET", url, False)

    '        ' Set Headers
    '        http.SetRequestHeader("Authorization", $"Bearer {accessToken}")
    '        http.SetRequestHeader("Content-Type", "application/json")
    '        http.SetRequestHeader("Cache-Control", "no-cache")
    '        http.SetRequestHeader("Accept", "*/*")

    '        ' Send request
    '        http.Send()

    '        ' Cek response
    '        'If http.Status = 200 Then
    '        '    Return http.ResponseText
    '        'Else
    '        '    'Return http.ResponseText
    '        '    Return $"Error {http.Status}: {http.ResponseText}"
    '        'End If

    '        Return $"{http.Status}: {http.ResponseText}"

    '    Catch ex As Exception
    '        Return $"Exception: {ex.Message}"
    '    Finally
    '        If http IsNot Nothing Then Marshal.ReleaseComObject(http)
    '    End Try
    'End Function
    'Public Shared Function PatientSearchNIK(production As Boolean, accessToken As String, namapasien As String, nik As String) As String
    '    Dim http As New WinHttp.WinHttpRequest()

    '    Try
    '        Dim url As String = ""

    '        If production = False Then
    '            url = "https://api-satusehat-stg.dto.kemkes.go.id/fhir-r4/v1/Patient?name=" & namapasien & "&identifier=https://fhir.kemkes.go.id/id/nik|" & nik & ""
    '        Else
    '            url = "https://api-satusehat.kemkes.go.id/fhir-r4/v1/Patient?name=" & namapasien & "&identifier=https://fhir.kemkes.go.id/id/nik|" & nik & ""
    '        End If

    '        ' URL Encode parameter
    '        url = Uri.EscapeUriString(url)

    '        ' Buat request
    '        http.Open("GET", url, False)

    '        ' Set Headers
    '        http.SetRequestHeader("Authorization", $"Bearer {accessToken}")
    '        http.SetRequestHeader("Content-Type", "application/json")
    '        http.SetRequestHeader("Cache-Control", "no-cache")
    '        http.SetRequestHeader("Accept", "*/*")

    '        ' Send request
    '        http.Send()

    '        ' Cek response
    '        'If http.Status = 200 Then
    '        '    Return http.ResponseText
    '        'Else
    '        '    'Return http.ResponseText
    '        '    Return $"Error {http.Status}: {http.ResponseText}"
    '        'End If

    '        Return $"{http.Status}: {http.ResponseText}"

    '    Catch ex As Exception
    '        Return $"Exception: {ex.Message}"
    '    Finally
    '        If http IsNot Nothing Then Marshal.ReleaseComObject(http)
    '    End Try
    'End Function
    'Public Shared Function CreatePatient(production As Boolean, accessToken As String, jsonPatientData As String) As String
    '    Dim http As New WinHttp.WinHttpRequest()

    '    Try
    '        ' Endpoint untuk create patient
    '        Dim url As String = String.Empty

    '        If production = False Then
    '            url = "https://api-satusehat-stg.dto.kemkes.go.id/fhir-r4/v1/Patient"
    '        Else
    '            url = "https://api-satusehat.kemkes.go.id/fhir-r4/v1/Patient"
    '        End If

    '        ' Setup request
    '        http.Open("POST", url, False)
    '        http.SetRequestHeader("Authorization", $"Bearer {accessToken}")
    '        http.SetRequestHeader("Content-Type", "application/json")

    '        ' Kirim data JSON
    '        http.Send(jsonPatientData)

    '        ' Cek response
    '        'If http.Status = 200 Or http.Status = 201 Then
    '        '    Return http.ResponseText
    '        'Else
    '        '    'Return http.ResponseText
    '        '    Return $"Error {http.Status}: {http.ResponseText}"
    '        'End If

    '        Return $"{http.Status}: {http.ResponseText}"

    '    Catch ex As Exception
    '        Return $"Exception: {ex.Message}"
    '    Finally
    '        If http IsNot Nothing Then Marshal.ReleaseComObject(http)
    '    End Try
    'End Function
    'Public Shared Function PatchPatient(production As Boolean, accessToken As String, idsatusehat As String, ByVal jsonPatientData As String) As String
    '    Dim http As New WinHttp.WinHttpRequest()

    '    Try
    '        ' Endpoint untuk create patient
    '        Dim url As String = String.Empty

    '        If production = False Then
    '            url = "https://api-satusehat-stg.dto.kemkes.go.id/fhir-r4/v1/Patient/" & idsatusehat & ""
    '        Else
    '            url = "https://api-satusehat.kemkes.go.id/fhir-r4/v1/Patient/" & idsatusehat & ""
    '        End If

    '        ' Setup request
    '        http.Open("PATCH", url, False)
    '        http.SetRequestHeader("Authorization", $"Bearer {accessToken}")
    '        http.SetRequestHeader("Content-Type", "application/json")

    '        ' Kirim data JSON
    '        http.Send(jsonPatientData)

    '        ' Cek response
    '        'If http.Status = 200 Or http.Status = 201 Then
    '        '    Return http.ResponseText
    '        'Else
    '        '    'Return http.ResponseText
    '        '    Return $"Error {http.Status}: {http.ResponseText}"
    '        'End If

    '        Return $"{http.Status}: {http.ResponseText}"

    '    Catch ex As Exception
    '        Return $"Exception: {ex.Message}"
    '    Finally
    '        If http IsNot Nothing Then Marshal.ReleaseComObject(http)
    '    End Try
    'End Function
    Public Shared Function OrganizationByID(production As Boolean, accessToken As String, idfaskes As String) As String
        Dim http As New WinHttp.WinHttpRequest()

        Try
            Dim url As String = ""

            If production = False Then
                url = "https://api-satusehat-stg.dto.kemkes.go.id/fhir-r4/v1/Organization/:" & idfaskes
            Else
                url = "https://api-satusehat.kemkes.go.id/fhir-r4/v1/Organization/:" & idfaskes
            End If

            ' URL Encode parameter
            url = Uri.EscapeUriString(url)

            ' Buat request
            http.Open("GET", url, False)

            ' Set Headers
            http.SetRequestHeader("Authorization", $"Bearer {accessToken}")
            http.SetRequestHeader("Content-Type", "application/json")
            http.SetRequestHeader("Cache-Control", "no-cache")
            http.SetRequestHeader("Accept", "*/*")

            ' Send request
            http.Send()

            ' Cek response
            'If http.Status = 200 Then
            '    Return http.ResponseText
            'Else
            '    'Return http.ResponseText
            '    Return $"Error {http.Status}: {http.ResponseText}"
            'End If

            Return $"{http.Status}: {http.ResponseText}"

        Catch ex As Exception
            Return $"Exception: {ex.Message}"
        Finally
            If http IsNot Nothing Then Marshal.ReleaseComObject(http)
        End Try
    End Function
    Public Shared Function OrganizationSearchbyName(production As Boolean, accessToken As String, name As String) As String
        Dim http As New WinHttp.WinHttpRequest()

        Try
            Dim url As String = ""

            If production = False Then
                url = "https://api-satusehat-stg.dto.kemkes.go.id/fhir-r4/v1/Organization?name=" & name
            Else
                url = "https://api-satusehat.kemkes.go.id/fhir-r4/v1/Organization?name=" & name
            End If

            ' URL Encode parameter
            url = Uri.EscapeUriString(url)

            ' Buat request
            http.Open("GET", url, False)

            ' Set Headers
            http.SetRequestHeader("Authorization", $"Bearer {accessToken}")
            http.SetRequestHeader("Content-Type", "application/json")
            http.SetRequestHeader("Cache-Control", "no-cache")
            http.SetRequestHeader("Accept", "*/*")

            ' Send request
            http.Send()

            ' Cek response
            'If http.Status = 200 Then
            '    Return http.ResponseText
            'Else
            '    'Return http.ResponseText
            '    Return $"Error {http.Status}: {http.ResponseText}"
            'End If

            Return $"{http.Status}: {http.ResponseText}"

        Catch ex As Exception
            Return $"Exception: {ex.Message}"
        Finally
            If http IsNot Nothing Then Marshal.ReleaseComObject(http)
        End Try
    End Function
    Public Shared Function LocationSearchbyName(production As Boolean, accessToken As String, name As String) As String
        Dim http As New WinHttp.WinHttpRequest()

        Try
            Dim url As String = ""

            If production = False Then
                url = "https://api-satusehat-stg.dto.kemkes.go.id/fhir-r4/v1/Location?name=" & name
            Else
                url = "https://api-satusehat.kemkes.go.id/fhir-r4/v1/Location?name=" & name
            End If

            ' URL Encode parameter
            url = Uri.EscapeUriString(url)

            ' Buat request
            http.Open("GET", url, False)

            ' Set Headers
            http.SetRequestHeader("Authorization", $"Bearer {accessToken}")
            http.SetRequestHeader("Content-Type", "application/json")
            http.SetRequestHeader("Cache-Control", "no-cache")
            http.SetRequestHeader("Accept", "*/*")

            ' Send request
            http.Send()

            ' Cek response
            'If http.Status = 200 Then
            '    Return http.ResponseText
            'Else
            '    'Return http.ResponseText
            '    Return $"Error {http.Status}: {http.ResponseText}"
            'End If

            Return $"{http.Status}: {http.ResponseText}"

        Catch ex As Exception
            Return $"Exception: {ex.Message}"
        Finally
            If http IsNot Nothing Then Marshal.ReleaseComObject(http)
        End Try
    End Function
    Public Shared Function LocationSearchbyOrgID(production As Boolean, accessToken As String, name As String) As String
        Dim http As New WinHttp.WinHttpRequest()

        Try
            Dim url As String = ""

            If production = False Then
                url = "https://api-satusehat-stg.dto.kemkes.go.id/fhir-r4/v1/Location?organization=" & name
            Else
                url = "https://api-satusehat.kemkes.go.id/fhir-r4/v1/Location?organization=" & name
            End If

            ' URL Encode parameter
            url = Uri.EscapeUriString(url)

            ' Buat request
            http.Open("GET", url, False)

            ' Set Headers
            http.SetRequestHeader("Authorization", $"Bearer {accessToken}")
            http.SetRequestHeader("Content-Type", "application/json")
            http.SetRequestHeader("Cache-Control", "no-cache")
            http.SetRequestHeader("Accept", "*/*")

            ' Send request
            http.Send()

            ' Cek response
            'If http.Status = 200 Then
            '    Return http.ResponseText
            'Else
            '    'Return http.ResponseText
            '    Return $"Error {http.Status}: {http.ResponseText}"
            'End If

            Return $"{http.Status}: {http.ResponseText}"

        Catch ex As Exception
            Return $"Exception: {ex.Message}"
        Finally
            If http IsNot Nothing Then Marshal.ReleaseComObject(http)
        End Try
    End Function
    Public Shared Function CariPasienPACS(ByVal RM As String, ByVal urlData As String) As String
        Dim http As New WinHttp.WinHttpRequest()

        Try
            Dim jsonPatientData As String = "{ 
                ""Level"": ""Patient"", 
                ""Expand"": true, 
                ""Query"": { 
                    ""PatientID"": """ & RM & """ 
                } 
            }"

            'https://dicom.serverapp.my.id/
            ' Endpoint untuk find patient
            'Dim url As String = "http://100.20.10.5:8042/tools/find"
            Dim url As String = urlData & "tools/find"

            ' Kredensial untuk Basic Auth
            Dim username As String = "Admin"
            Dim password As String = "123456"
            Dim credentials As String = Convert.ToBase64String(Encoding.ASCII.GetBytes($"{username}:{password}"))

            ' Setup request
            http.Open("POST", url, False)

            ' Set headers
            http.SetRequestHeader("Content-Type", "application/json")
            http.SetRequestHeader("Authorization", $"Basic {credentials}")

            ' Kirim data JSON
            http.Send(jsonPatientData)

            ' Cek response
            If http.Status = 200 Or http.Status = 201 Then
                Return http.ResponseText
            Else
                Return $"Error {http.Status}: {http.ResponseText}"
            End If

        Catch ex As Exception
            Return $"Exception: {ex.Message}"
        Finally
            If http IsNot Nothing Then Marshal.ReleaseComObject(http)
        End Try
    End Function
    Public Shared Function GetIDPACS(ID As String, ByVal urlData As String) As String
        Dim http As New WinHttp.WinHttpRequest()

        Try
            ' Endpoint untuk find patient
            Dim url As String = urlData & "studies/" & ID

            ' Kredensial untuk Basic Auth
            Dim username As String = "Admin"
            Dim password As String = "123456"
            Dim credentials As String = Convert.ToBase64String(Encoding.ASCII.GetBytes($"{username}:{password}"))

            ' Setup request
            http.Open("GET", url, False)

            ' Set headers
            http.SetRequestHeader("Content-Type", "application/json")
            http.SetRequestHeader("Authorization", $"Basic {credentials}")

            ' Kirim data JSON
            http.Send()

            ' Cek response
            If http.Status = 200 Or http.Status = 201 Then
                Return http.ResponseText
            Else
                Return $"Error {http.Status}: {http.ResponseText}"
            End If

        Catch ex As Exception
            Return $"Exception: {ex.Message}"
        Finally
            If http IsNot Nothing Then Marshal.ReleaseComObject(http)
        End Try
    End Function
    Public Shared Function GetIDSeries(ID As String, ByVal urlData As String) As String
        Dim http As New WinHttp.WinHttpRequest()

        Try
            ' Endpoint untuk find patient
            Dim url As String = urlData & "series/" & ID

            ' Kredensial untuk Basic Auth
            Dim username As String = "Admin"
            Dim password As String = "123456"
            Dim credentials As String = Convert.ToBase64String(Encoding.ASCII.GetBytes($"{username}:{password}"))

            ' Setup request
            http.Open("GET", url, False)

            ' Set headers
            http.SetRequestHeader("Content-Type", "application/json")
            http.SetRequestHeader("Authorization", $"Basic {credentials}")

            ' Kirim data JSON
            http.Send()

            ' Cek response
            If http.Status = 200 Or http.Status = 201 Then
                Return http.ResponseText
            Else
                Return $"Error {http.Status}: {http.ResponseText}"
            End If

        Catch ex As Exception
            Return $"Exception: {ex.Message}"
        Finally
            If http IsNot Nothing Then Marshal.ReleaseComObject(http)
        End Try
    End Function
    Public Shared Function GetIDInstances(ID As String, ByVal urlData As String, ByVal jenis As String) As String
        Dim http As New WinHttp.WinHttpRequest()

        Try
            ' Endpoint untuk find patient
            'Dim url As String = urlData & "instances/" & ID & "/" & jenis

            Dim url As String = urlData & "preview/preview.php?id=" & ID

            ' Kredensial untuk Basic Auth
            Dim username As String = "Admin"
            Dim password As String = "123456"
            Dim credentials As String = Convert.ToBase64String(Encoding.ASCII.GetBytes($"{username}:{password}"))

            ' Setup request
            http.Open("GET", url, False)

            ' Set headers
            http.SetRequestHeader("Content-Type", "application/json")
            http.SetRequestHeader("Authorization", $"Basic {credentials}")

            ' Kirim data JSON
            http.Send()

            ' Cek response
            If http.Status = 200 Or http.Status = 201 Then
                Return http.ResponseText
            Else
                Return $"Error {http.Status}: {http.ResponseText}"
            End If

        Catch ex As Exception
            Return $"Exception: {ex.Message}"
        Finally
            If http IsNot Nothing Then Marshal.ReleaseComObject(http)
        End Try
    End Function
    Public Shared Function GetToken(json As String, ByVal ambil As String) As String
        Dim parts = json.Split(""""c)
        For i = 0 To parts.Length - 1
            If parts(i) = "" & ambil & "" Then
                Return parts(i + 2) ' +2 karena skip : dan spasi
            End If
        Next
        Return ""
    End Function
#End Region
#Region "Format Json"
    Public Shared Function BuildOrganizationJSON(Org_id As String, ByVal display_SubOrg_id As String, namaorganisasi As String, phoners As String, emailrs As String, websiters As String, alamat As String, kota As String, kodepos As String, kodepor As String, kodekota As String, kokdekec As String, kodedesa As String) As String
        '"""code"": ""team""," &
        '       """display"": ""Organizational team""" &

        Dim json As String = "{" &
        """resourceType"": ""Organization""," &
        """active"": true," &
        """identifier"": [{" &
            """use"": ""official""," &
            """system"": """ & "" & """," &
            """value"": """ & "" & """" &
        "}]," &
        """type"": [{" &
            """coding"": [{" &
                """system"": ""http://terminology.hl7.org/CodeSystem/organization-type""," &
                """code"": ""dept""," &
                """display"": ""Hospital Department""" &
            "}]" &
        "}]," &
        """name"": """ & namaorganisasi & """," &
        """telecom"": [" &
            "{""system"": ""phone"", ""value"": """ & phoners & """, ""use"": ""work""}," &
            "{""system"": ""email"", ""value"": """ & emailrs & """, ""use"": ""work""}," &
            "{""system"": ""url"", ""value"": """ & websiters & """, ""use"": ""work""}" &
        "]," &
        """address"": [{" &
            """use"": ""work""," &
            """type"": ""both""," &
            """line"": [""" & alamat & """]," &
            """city"": """ & kota & """," &
            """postalCode"": """ & kodepos & """," &
            """country"": ""ID""," &
            """extension"": [{" &
                """url"": ""https://fhir.kemkes.go.id/r4/StructureDefinition/administrativeCode""," &
                """extension"": [" &
                    "{""url"": ""province"", ""valueCode"": """ & kodepor & """}," &
                    "{""url"": ""city"", ""valueCode"": """ & kodekota & """}," &
                    "{""url"": ""district"", ""valueCode"": """ & kokdekec & """}," &
                    "{""url"": ""village"", ""valueCode"": """ & kodedesa & """}" &
                "]" &
            "}]" &
        "}]," &
        """partOf"": {" &
            """reference"": ""Organization/" & Org_id & """," &
            """display"": """ & display_SubOrg_id & """" &
        "}" &
    "}"

        Return json
    End Function
    Public Shared Function BuildPatientJSON(
        nik As String,
        nama As String,
        gender As String,
        birthDate As String,
        address As String,
        city As String,
        postalCode As String,
        provinceCode As String,
        cityCode As String,
        districtCode As String,
        villageCode As String,
        rw As String,
        rt As String,
        contactName As String,
        contactPhone As String,
        maritalStatusCode As String,
        maritalStatusDisplay As String
    ) As String

        Dim json As String = $"
        {{
            ""resourceType"": ""Patient"",
            ""meta"": {{
                ""profile"": [
                    ""https://fhir.kemkes.go.id/r4/StructureDefinition/Patient""
                ]
            }},
            ""identifier"": [
                {{
                    ""use"": ""official"",
                    ""system"": ""https://fhir.kemkes.go.id/id/nik"",
                    ""value"": ""{nik}""
                }}
            ],
            ""active"": true,
            ""name"": [
                {{
                    ""use"": ""official"",
                    ""text"": ""{nama}""
                }}
            ],
            ""gender"": ""{gender}"",
            ""birthDate"": ""{birthDate}"",
            ""deceasedBoolean"": false,
            ""address"": [
                {{
                    ""use"": ""home"",
                    ""line"": [
                        ""{address}""
                    ],
                    ""city"": ""{city}"",
                    ""postalCode"": ""{postalCode}"",
                    ""country"": ""ID"",
                    ""extension"": [
                        {{
                            ""url"": ""https://fhir.kemkes.go.id/r4/StructureDefinition/administrativeCode"",
                            ""extension"": [
                                {{
                                    ""url"": ""province"",
                                    ""valueCode"": ""{provinceCode}""
                                }},
                                {{
                                    ""url"": ""city"",
                                    ""valueCode"": ""{cityCode}""
                                }},
                                {{
                                    ""url"": ""district"",
                                    ""valueCode"": ""{districtCode}""
                                }},
                                {{
                                    ""url"": ""village"",
                                    ""valueCode"": ""{villageCode}""
                                }},
                                {{
                                    ""url"": ""rw"",
                                    ""valueCode"": ""{rw}""
                                }},
                                {{
                                    ""url"": ""rt"",
                                    ""valueCode"": ""{rt}""
                                }}
                            ]
                        }}
                    ]
                }}
            ],
            ""maritalStatus"": {{
                ""coding"": [
                    {{
                        ""system"": ""http://terminology.hl7.org/CodeSystem/v3-MaritalStatus"",
                        ""code"": ""{maritalStatusCode}"",
                        ""display"": ""{maritalStatusDisplay}""
                    }}
                ],
                ""text"": ""{maritalStatusDisplay}""
            }},
            ""multipleBirthInteger"": 0,
            ""contact"": [
                {{
                    ""relationship"": [
                        {{
                            ""coding"": [
                                {{
                                    ""system"": ""http://terminology.hl7.org/CodeSystem/v2-0131"",
                                    ""code"": ""C""
                                }}
                            ]
                        }}
                    ],
                    ""name"": {{
                        ""use"": ""official"",
                        ""text"": ""{contactName}""
                    }},
                    ""telecom"": [
                        {{
                            ""system"": ""phone"",
                            ""value"": ""{contactPhone}"",
                            ""use"": ""mobile""
                        }}
                    ]
                }}
            ],
            ""communication"": [
                {{
                    ""language"": {{
                        ""coding"": [
                            {{
                                ""system"": ""urn:ietf:bcp:47"",
                                ""code"": ""id-ID"",
                                ""display"": ""Indonesian""
                            }}
                        ],
                        ""text"": ""Indonesian""
                    }},
                    ""preferred"": true
                }}
            ]
        }}"

        ' Bersihkan JSON dari newline dan spasi berlebih
        json = System.Text.RegularExpressions.Regex.Replace(json, "\s+", " ")

        Return json
    End Function
    Public Shared Function PatchBuildPatientJSON(
    nik As String,
    nama As String,
    gender As String,
    birthDate As String,
    address As String,
    city As String,
    postalCode As String,
    provinceCode As String,
    cityCode As String,
    districtCode As String,
    villageCode As String,
    rw As String,
    rt As String,
    contactName As String,
    contactPhone As String,
    maritalStatusCode As String,
    maritalStatusDisplay As String,
    idsatusehat As String
) As String

        ' Format JSON Patch dengan name sebagai ARRAY
        Dim json As String = $"
[
    {{
        ""op"": ""test"",
        ""path"": ""/name"",
        ""value"": [
            {{
                ""use"": ""official"",
                ""text"": ""{nama}""
            }}
        ]
    }},
    {{
        ""op"": ""replace"",
        ""path"": ""/name"",
        ""value"": [
            {{
                ""use"": ""official"",
                ""text"": ""{nama}""
            }}
        ]
    }},
    {{
        ""op"": ""test"",
        ""path"": ""/gender"",
        ""value"": ""{gender}""
    }},
    {{
        ""op"": ""replace"",
        ""path"": ""/gender"",
        ""value"": ""{gender}""
    }},
    {{
        ""op"": ""test"",
        ""path"": ""/birthDate"",
        ""value"": ""{birthDate}""
    }},
    {{
        ""op"": ""replace"",
        ""path"": ""/birthDate"",
        ""value"": ""{birthDate}""
    }},
    {{
        ""op"": ""replace"",
        ""path"": ""/identifier"",
        ""value"": [
            {{
                ""system"": ""https://fhir.kemkes.go.id/id/nik"",
                ""use"": ""official"",
                ""value"": ""{nik}""
            }},
            {{
                ""system"": ""https://fhir.kemkes.go.id/id/ihs-number"",
                ""use"": ""official"",
                ""value"": ""{idsatusehat}""
            }}
        ]
    }},
    {{
        ""op"": ""replace"",
        ""path"": ""/maritalStatus"",
        ""value"": {{
            ""coding"": [
                {{
                    ""system"": ""http://terminology.hl7.org/CodeSystem/v3-MaritalStatus"",
                    ""code"": ""{maritalStatusCode}"",
                    ""display"": ""{maritalStatusDisplay}""
                }}
            ],
            ""text"": ""{maritalStatusDisplay}""
        }}
    }},
    {{
        ""op"": ""replace"",
        ""path"": ""/address"",
        ""value"": [
            {{
                ""use"": ""home"",
                ""line"": [""{address}""],
                ""city"": ""{city}"",
                ""postalCode"": ""{postalCode}"",
                ""country"": ""ID"",
                ""extension"": [
                    {{
                        ""url"": ""https://fhir.kemkes.go.id/r4/StructureDefinition/administrativeCode"",
                        ""extension"": [
                            {{""url"": ""province"", ""valueCode"": ""{provinceCode}""}},
                            {{""url"": ""city"", ""valueCode"": ""{cityCode}""}},
                            {{""url"": ""district"", ""valueCode"": ""{districtCode}""}},
                            {{""url"": ""village"", ""valueCode"": ""{villageCode}""}},
                            {{""url"": ""rw"", ""valueCode"": ""{rw}""}},
                            {{""url"": ""rt"", ""valueCode"": ""{rt}""}}
                        ]
                    }}
                ]
            }}
        ]
    }},
    {{
        ""op"": ""replace"",
        ""path"": ""/contact"",
        ""value"": [
            {{
                ""relationship"": [
                    {{
                        ""coding"": [
                            {{
                                ""system"": ""http://terminology.hl7.org/CodeSystem/v2-0131"",
                                ""code"": ""C""
                            }}
                        ]
                    }}
                ],
                ""name"": {{
                    ""use"": ""official"",
                    ""text"": ""{contactName}""
                }},
                ""telecom"": [
                    {{
                        ""system"": ""phone"",
                        ""value"": ""{contactPhone}"",
                        ""use"": ""mobile""
                    }}
                ]
            }}
        ]
    }}
]"

        ' Bersihkan JSON dari newline dan spasi berlebih
        json = System.Text.RegularExpressions.Regex.Replace(json, "\s+", " ")

        Return json
    End Function
#End Region
#Region "01. Pelayanan - Rawat Jalan"
#Region "O-Auth2"
    Public Shared Function GetAccessToken(production As Boolean, clientId As String, clientSecret As String) As String
        ' URL Endpoint (Ganti dengan Production jika diperlukan)
        ' Production: https://api-satusehat.kemkes.go.id/oauth2/v1/accesstoken
        'Dim tokenUrl As String = "https://api-satusehat-stg.dto.kemkes.go.id/oauth2/v1/accesstoken?grant_type=client_credentials"

        Dim tokenUrl As String = String.Empty

        If production = False Then
            tokenUrl = "https://api-satusehat-stg.dto.kemkes.go.id/oauth2/v1/accesstoken?grant_type=client_credentials"
        Else
            tokenUrl = "https://api-satusehat.kemkes.go.id/oauth2/v1/accesstoken?grant_type=client_credentials"
        End If

        ' Siapkan data POST dalam format application/x-www-form-urlencoded
        Dim postData As String = $"client_id={clientId}&client_secret={clientSecret}"

        ' Buat objek WinHttpRequest
        Dim http As New WinHttp.WinHttpRequest()

        Try
            ' Buka koneksi (method POST, URL, async=False)
            http.Open("POST", tokenUrl, False)

            ' Set header Content-Type
            http.SetRequestHeader("Content-Type", "application/x-www-form-urlencoded")

            ' Kirim request dengan data POST
            http.Send(postData)

            ' Cek status response
            Dim statusCode As Integer = http.Status
            Dim responseText As String = http.ResponseText

            If statusCode = 200 Then
                ' Parse JSON response untuk mengambil access_token
                Dim token As String = responseText

                If Not String.IsNullOrEmpty(token) Then
                    Return token
                Else
                    'Console.WriteLine("Access token tidak ditemukan dalam response")
                    Return token
                End If
            Else
                'Console.WriteLine($"Gagal mendapatkan token. HTTP Status: {statusCode}")
                'Console.WriteLine($"Response: {responseText}")

                Dim respon As String = $"Error mendapatkan token. HTTP Status: {statusCode}" & $"Response: {responseText}"

                Return respon
            End If

        Catch ex As Exception
            'Console.WriteLine($"Error saat request token: {ex.Message}")
            Return ($"Error saat request token: {ex.Message}")
        Finally
            ' Bersihkan resource COM
            If http IsNot Nothing Then
                Marshal.ReleaseComObject(http)
            End If
        End Try
    End Function
#End Region
#Region "00. Membuat Struktur Organisasi dan Lokasi"
    Public Shared Function OrganizationCreatePoliOrg(production As Boolean, accessToken As String, ByVal jsonPatientData As String) As String
        Dim http As New WinHttp.WinHttpRequest()

        Try
            ' Endpoint untuk create patient
            Dim url As String = String.Empty

            If production = False Then
                url = "https://api-satusehat-stg.dto.kemkes.go.id/fhir-r4/v1/Organization"
            Else
                url = "https://api-satusehat.kemkes.go.id/fhir-r4/v1/Organization"
            End If

            ' Setup request
            http.Open("POST", url, False)
            http.SetRequestHeader("Authorization", $"Bearer {accessToken}")
            http.SetRequestHeader("Content-Type", "application/json")

            ' Kirim data JSON
            http.Send(jsonPatientData)

            ' Cek response
            'If http.Status = 200 Or http.Status = 201 Then
            '    Return http.ResponseText
            'Else
            '    'Return http.ResponseText
            '    Return $"Error {http.Status}: {http.ResponseText}"
            'End If

            Return $"{http.Status}: {http.ResponseText}"

        Catch ex As Exception
            Return $"Exception: {ex.Message}"
        Finally
            If http IsNot Nothing Then Marshal.ReleaseComObject(http)
        End Try
    End Function
    Public Shared Function LocationCreatePoliRuang(production As Boolean, accessToken As String, ByVal jsonPatientData As String) As String
        Dim http As New WinHttp.WinHttpRequest()

        Try
            ' Endpoint untuk create patient
            Dim url As String = String.Empty

            If production = False Then
                url = "https://api-satusehat-stg.dto.kemkes.go.id/fhir-r4/v1/Location"
            Else
                url = "https://api-satusehat.kemkes.go.id/fhir-r4/v1/Location"
            End If

            ' Setup request
            http.Open("POST", url, False)
            http.SetRequestHeader("Authorization", $"Bearer {accessToken}")
            http.SetRequestHeader("Content-Type", "application/json")

            ' Kirim data JSON
            http.Send(jsonPatientData)

            ' Cek response
            'If http.Status = 200 Or http.Status = 201 Then
            '    Return http.ResponseText
            'Else
            '    'Return http.ResponseText
            '    Return $"Error {http.Status}: {http.ResponseText}"
            'End If

            Return $"{http.Status}: {http.ResponseText}"

        Catch ex As Exception
            Return $"Exception: {ex.Message}"
        Finally
            If http IsNot Nothing Then Marshal.ReleaseComObject(http)
        End Try
    End Function
#End Region
#Region "01. Mencari Data Pasien dan Nakes"
    Public Shared Function PatientByID(production As Boolean, accessToken As String, parameter As String) As String
        Dim http As New WinHttp.WinHttpRequest()

        Try
            Dim url As String = ""

            If production = False Then
                url = "https://api-satusehat-stg.dto.kemkes.go.id/fhir-r4/v1/Patient/:" & parameter
            Else
                url = "https://api-satusehat.kemkes.go.id/fhir-r4/v1/Patient/:" & parameter
            End If

            ' URL Encode parameter
            url = Uri.EscapeUriString(url)

            ' Buat request
            http.Open("GET", url, False)

            ' Set Headers
            http.SetRequestHeader("Authorization", $"Bearer {accessToken}")
            http.SetRequestHeader("Content-Type", "application/json")
            http.SetRequestHeader("Cache-Control", "no-cache")
            http.SetRequestHeader("Accept", "*/*")

            ' Send request
            http.Send()

            ' Cek response
            'If http.Status = 200 Then
            '    Return http.ResponseText
            'Else
            '    'Return http.ResponseText
            '    Return $"Error {http.Status}: {http.ResponseText}"
            'End If

            Return $"{http.Status}: {http.ResponseText}"

        Catch ex As Exception
            Return $"Exception: {ex.Message}"
        Finally
            If http IsNot Nothing Then Marshal.ReleaseComObject(http)
        End Try
    End Function
    Public Shared Function PatientByNIK(production As Boolean, accessToken As String, parameter As String) As String
        Dim http As New WinHttp.WinHttpRequest()

        Try
            Dim url As String = ""

            If production = False Then
                url = "https://api-satusehat-stg.dto.kemkes.go.id/fhir-r4/v1/Patient?identifier=https://fhir.kemkes.go.id/id/nik|" & parameter
            Else
                url = "https://api-satusehat.kemkes.go.id/fhir-r4/v1/Patient?identifier=https://fhir.kemkes.go.id/id/nik|" & parameter
            End If

            ' URL Encode parameter
            url = Uri.EscapeUriString(url)

            ' Buat request
            http.Open("GET", url, False)

            ' Set Headers
            http.SetRequestHeader("Authorization", $"Bearer {accessToken}")
            http.SetRequestHeader("Content-Type", "application/json")
            http.SetRequestHeader("Cache-Control", "no-cache")
            http.SetRequestHeader("Accept", "*/*")

            ' Send request
            http.Send()

            ' Cek response
            'If http.Status = 200 Then
            '    Return http.ResponseText
            'Else
            '    'Return http.ResponseText
            '    Return $"Error {http.Status}: {http.ResponseText}"
            'End If

            Return $"{http.Status}: {http.ResponseText}"

        Catch ex As Exception
            Return $"Exception: {ex.Message}"
        Finally
            If http IsNot Nothing Then Marshal.ReleaseComObject(http)
        End Try
    End Function
    Public Shared Function PatientSearchNameirthdateGender(production As Boolean, accessToken As String, nama As String, tanggallahir As String, gender As String) As String
        Dim http As New WinHttp.WinHttpRequest()

        Try
            Dim url As String = ""

            If production = False Then
                url = "https://api-satusehat-stg.dto.kemkes.go.id/fhir-r4/v1/Patient?name=" & nama & " 8&birthdate=" & tanggallahir & "&gender=" & gender
            Else
                url = "https://api-satusehat.kemkes.go.id/fhir-r4/v1/Patient?name=" & nama & " 8&birthdate=" & tanggallahir & "&gender=" & gender
            End If

            ' URL Encode parameter
            url = Uri.EscapeUriString(url)

            ' Buat request
            http.Open("GET", url, False)

            ' Set Headers
            http.SetRequestHeader("Authorization", $"Bearer {accessToken}")
            http.SetRequestHeader("Content-Type", "application/json")
            http.SetRequestHeader("Cache-Control", "no-cache")
            http.SetRequestHeader("Accept", "*/*")

            ' Send request
            http.Send()

            ' Cek response
            'If http.Status = 200 Then
            '    Return http.ResponseText
            'Else
            '    'Return http.ResponseText
            '    Return $"Error {http.Status}: {http.ResponseText}"
            'End If

            Return $"{http.Status}: {http.ResponseText}"

        Catch ex As Exception
            Return $"Exception: {ex.Message}"
        Finally
            If http IsNot Nothing Then Marshal.ReleaseComObject(http)
        End Try
    End Function
    Public Shared Function PractitionerByID(production As Boolean, accessToken As String, parameter As String) As String
        Dim http As New WinHttp.WinHttpRequest()

        Try
            Dim url As String = ""

            If production = False Then
                url = "https://api-satusehat-stg.dto.kemkes.go.id/fhir-r4/v1/Practitioner/:id" & parameter
            Else
                url = "https://api-satusehat.kemkes.go.id/fhir-r4/v1/Practitioner/:id" & parameter
            End If

            ' URL Encode parameter
            url = Uri.EscapeUriString(url)

            ' Buat request
            http.Open("GET", url, False)

            ' Set Headers
            http.SetRequestHeader("Authorization", $"Bearer {accessToken}")
            http.SetRequestHeader("Content-Type", "application/json")
            http.SetRequestHeader("Cache-Control", "no-cache")
            http.SetRequestHeader("Accept", "*/*")

            ' Send request
            http.Send()

            ' Cek response
            'If http.Status = 200 Then
            '    Return http.ResponseText
            'Else
            '    'Return http.ResponseText
            '    Return $"Error {http.Status}: {http.ResponseText}"
            'End If

            Return $"{http.Status}: {http.ResponseText}"

        Catch ex As Exception
            Return $"Exception: {ex.Message}"
        Finally
            If http IsNot Nothing Then Marshal.ReleaseComObject(http)
        End Try
    End Function
    Public Shared Function PractitionerByNIK(production As Boolean, accessToken As String, parameter As String) As String
        Dim http As New WinHttp.WinHttpRequest()

        Try
            Dim url As String = ""

            If production = False Then
                url = "https://api-satusehat-stg.dto.kemkes.go.id/fhir-r4/v1/Practitioner?identifier=https://fhir.kemkes.go.id/id/nik|" & parameter
            Else
                url = "https://api-satusehat.kemkes.go.id/fhir-r4/v1/Practitioner?identifier=https://fhir.kemkes.go.id/id/nik|" & parameter
            End If

            ' URL Encode parameter
            url = Uri.EscapeUriString(url)

            ' Buat request
            http.Open("GET", url, False)

            ' Set Headers
            http.SetRequestHeader("Authorization", $"Bearer {accessToken}")
            http.SetRequestHeader("Content-Type", "application/json")
            http.SetRequestHeader("Cache-Control", "no-cache")
            http.SetRequestHeader("Accept", "*/*")

            ' Send request
            http.Send()

            ' Cek response
            'If http.Status = 200 Then
            '    Return http.ResponseText
            'Else
            '    'Return http.ResponseText
            '    Return $"Error {http.Status}: {http.ResponseText}"
            'End If

            Return $"{http.Status}: {http.ResponseText}"

        Catch ex As Exception
            Return $"Exception: {ex.Message}"
        Finally
            If http IsNot Nothing Then Marshal.ReleaseComObject(http)
        End Try
    End Function
    Public Shared Function PractitionerSearchNameirthdateGender(production As Boolean, accessToken As String, nama As String, tanggallahir As String, gender As String) As String
        Dim http As New WinHttp.WinHttpRequest()

        Try
            Dim url As String = ""

            If production = False Then
                url = "https://api-satusehat-stg.dto.kemkes.go.id/fhir-r4/v1/Practitioner?name=" & nama & " 1&birthdate=" & tanggallahir & "&gender=" & gender & ""
            Else
                url = "https://api-satusehat.kemkes.go.id/fhir-r4/v1/Practitioner?name=" & nama & " 1&birthdate=" & tanggallahir & "&gender=" & gender & ""
            End If

            ' URL Encode parameter
            url = Uri.EscapeUriString(url)

            ' Buat request
            http.Open("GET", url, False)

            ' Set Headers
            http.SetRequestHeader("Authorization", $"Bearer {accessToken}")
            http.SetRequestHeader("Content-Type", "application/json")
            http.SetRequestHeader("Cache-Control", "no-cache")
            http.SetRequestHeader("Accept", "*/*")

            ' Send request
            http.Send()

            ' Cek response
            'If http.Status = 200 Then
            '    Return http.ResponseText
            'Else
            '    'Return http.ResponseText
            '    Return $"Error {http.Status}: {http.ResponseText}"
            'End If

            Return $"{http.Status}: {http.ResponseText}"

        Catch ex As Exception
            Return $"Exception: {ex.Message}"
        Finally
            If http IsNot Nothing Then Marshal.ReleaseComObject(http)
        End Try
    End Function
#End Region
#Region "02. Pendaftaran Kunjungan Rawat Jalan"
    Public Shared Function KirimPOST(ByVal url As String, accessToken As String, ByVal jsonPatientData As String) As String
        Dim http As New WinHttp.WinHttpRequest()

        Try
            ' Setup request
            http.Open("POST", url, False)
            http.SetRequestHeader("Authorization", $"Bearer {accessToken}")
            http.SetRequestHeader("Content-Type", "application/json")

            ' Kirim data JSON
            http.Send(jsonPatientData)

            ' Cek response
            'If http.Status = 200 Or http.Status = 201 Then
            '    Return http.ResponseText
            'Else
            '    'Return http.ResponseText
            '    Return $"Error {http.Status}: {http.ResponseText}"
            'End If

            Return $"{http.Status}: {http.ResponseText}"

        Catch ex As Exception
            Return $"Exception: {ex.Message}"
        Finally
            If http IsNot Nothing Then Marshal.ReleaseComObject(http)
        End Try
    End Function
    Public Shared Function EncounterKunjunganBaru(production As Boolean, accessToken As String, ByVal jsonPatientData As String) As String
        Dim http As New WinHttp.WinHttpRequest()

        Try
            ' Endpoint untuk create patient
            Dim url As String = String.Empty

            If production = False Then
                url = "https://api-satusehat-stg.dto.kemkes.go.id/fhir-r4/v1/Encounter"
            Else
                url = "https://api-satusehat.kemkes.go.id/fhir-r4/v1/Encounter"
            End If

            ' Setup request
            http.Open("POST", url, False)
            http.SetRequestHeader("Authorization", $"Bearer {accessToken}")
            http.SetRequestHeader("Content-Type", "application/json")

            ' Kirim data JSON
            http.Send(jsonPatientData)

            ' Cek response
            'If http.Status = 200 Or http.Status = 201 Then
            '    Return http.ResponseText
            'Else
            '    'Return http.ResponseText
            '    Return $"Error {http.Status}: {http.ResponseText}"
            'End If

            Return $"{http.Status}: {http.ResponseText}"

        Catch ex As Exception
            Return $"Exception: {ex.Message}"
        Finally
            If http IsNot Nothing Then Marshal.ReleaseComObject(http)
        End Try
    End Function
    Public Shared Function EncounterKunjunganBaruTaksi4(production As Boolean, accessToken As String, ByVal jsonPatientData As String, ByVal ID As String) As String
        Dim http As New WinHttp.WinHttpRequest()

        Try
            ' Endpoint untuk create patient
            Dim url As String = String.Empty

            If production = False Then
                url = "https://api-satusehat-stg.dto.kemkes.go.id/fhir-r4/v1/Encounter/" & ID
            Else
                url = "https://api-satusehat.kemkes.go.id/fhir-r4/v1/Encounter/" & ID
            End If

            ' Setup request
            http.Open("PUT", url, False)
            http.SetRequestHeader("Authorization", $"Bearer {accessToken}")
            http.SetRequestHeader("Content-Type", "application/json")

            ' Kirim data JSON
            http.Send(jsonPatientData)

            ' Cek response
            'If http.Status = 200 Or http.Status = 201 Then
            '    Return http.ResponseText
            'Else
            '    'Return http.ResponseText
            '    Return $"Error {http.Status}: {http.ResponseText}"
            'End If

            Return $"{http.Status}: {http.ResponseText}"

        Catch ex As Exception
            Return $"Exception: {ex.Message}"
        Finally
            If http IsNot Nothing Then Marshal.ReleaseComObject(http)
        End Try
    End Function
#End Region
#Region "03. Anamnesis"
    Public Shared Function ConditionKeluhanUtama(production As Boolean, accessToken As String, ByVal jsonPatientData As String) As String
        Dim http As New WinHttp.WinHttpRequest()

        Try
            ' Endpoint untuk create patient
            Dim url As String = String.Empty

            If production = False Then
                url = "https://api-satusehat-stg.dto.kemkes.go.id/fhir-r4/v1/Condition"
            Else
                url = "https://api-satusehat.kemkes.go.id/fhir-r4/v1/Condition"
            End If

            ' Setup request
            http.Open("POST", url, False)
            http.SetRequestHeader("Authorization", $"Bearer {accessToken}")
            http.SetRequestHeader("Content-Type", "application/json")

            ' Kirim data JSON
            http.Send(jsonPatientData)

            ' Cek response
            'If http.Status = 200 Or http.Status = 201 Then
            '    Return http.ResponseText
            'Else
            '    'Return http.ResponseText
            '    Return $"Error {http.Status}: {http.ResponseText}"
            'End If

            Return $"{http.Status}: {http.ResponseText}"

        Catch ex As Exception
            Return $"Exception: {ex.Message}"
        Finally
            If http IsNot Nothing Then Marshal.ReleaseComObject(http)
        End Try
    End Function
    Public Shared Function ConditionKeluhanPenyerta(production As Boolean, accessToken As String, ByVal jsonPatientData As String) As String
        Dim http As New WinHttp.WinHttpRequest()

        Try
            ' Endpoint untuk create patient
            Dim url As String = String.Empty

            If production = False Then
                url = "https://api-satusehat-stg.dto.kemkes.go.id/fhir-r4/v1/Condition"
            Else
                url = "https://api-satusehat.kemkes.go.id/fhir-r4/v1/Condition"
            End If

            ' Setup request
            http.Open("POST", url, False)
            http.SetRequestHeader("Authorization", $"Bearer {accessToken}")
            http.SetRequestHeader("Content-Type", "application/json")

            ' Kirim data JSON
            http.Send(jsonPatientData)

            ' Cek response
            'If http.Status = 200 Or http.Status = 201 Then
            '    Return http.ResponseText
            'Else
            '    'Return http.ResponseText
            '    Return $"Error {http.Status}: {http.ResponseText}"
            'End If

            Return $"{http.Status}: {http.ResponseText}"

        Catch ex As Exception
            Return $"Exception: {ex.Message}"
        Finally
            If http IsNot Nothing Then Marshal.ReleaseComObject(http)
        End Try
    End Function
#End Region
#Region "04. Hasil Pemeriksaan Fisik"
    Public Shared Function Obervasi(production As Boolean, accessToken As String, ByVal jsonPatientData As String) As String
        Dim http As New WinHttp.WinHttpRequest()

        Try
            ' Endpoint untuk create patient
            Dim url As String = String.Empty

            If production = False Then
                url = "https://api-satusehat-stg.dto.kemkes.go.id/fhir-r4/v1/Observation"
            Else
                url = "https://api-satusehat.kemkes.go.id/fhir-r4/v1/Observation"
            End If

            ' Setup request
            http.Open("POST", url, False)
            http.SetRequestHeader("Authorization", $"Bearer {accessToken}")
            http.SetRequestHeader("Content-Type", "application/json")

            ' Kirim data JSON
            http.Send(jsonPatientData)

            ' Cek response
            'If http.Status = 200 Or http.Status = 201 Then
            '    Return http.ResponseText
            'Else
            '    'Return http.ResponseText
            '    Return $"Error {http.Status}: {http.ResponseText}"
            'End If

            Return $"{http.Status}: {http.ResponseText}"

        Catch ex As Exception
            Return $"Exception: {ex.Message}"
        Finally
            If http IsNot Nothing Then Marshal.ReleaseComObject(http)
        End Try
    End Function
    Public Shared Function ClinicalImpression(production As Boolean, accessToken As String, ByVal jsonPatientData As String) As String
        Dim http As New WinHttp.WinHttpRequest()

        Try
            ' Endpoint untuk create patient
            Dim url As String = String.Empty

            If production = False Then
                url = "https://api-satusehat-stg.dto.kemkes.go.id/fhir-r4/v1/ClinicalImpression"
            Else
                url = "https://api-satusehat.kemkes.go.id/fhir-r4/v1/ClinicalImpression"
            End If

            ' Setup request
            http.Open("POST", url, False)
            http.SetRequestHeader("Authorization", $"Bearer {accessToken}")
            http.SetRequestHeader("Content-Type", "application/json")

            ' Kirim data JSON
            http.Send(jsonPatientData)

            ' Cek response
            'If http.Status = 200 Or http.Status = 201 Then
            '    Return http.ResponseText
            'Else
            '    'Return http.ResponseText
            '    Return $"Error {http.Status}: {http.ResponseText}"
            'End If

            Return $"{http.Status}: {http.ResponseText}"

        Catch ex As Exception
            Return $"Exception: {ex.Message}"
        Finally
            If http IsNot Nothing Then Marshal.ReleaseComObject(http)
        End Try
    End Function
    Public Shared Function RencanaRawatJalan(production As Boolean, accessToken As String, ByVal jsonPatientData As String) As String
        Dim http As New WinHttp.WinHttpRequest()

        Try
            ' Endpoint untuk create patient
            Dim url As String = String.Empty

            If production = False Then
                url = "https://api-satusehat-stg.dto.kemkes.go.id/fhir-r4/v1/CarePlan"
            Else
                url = "https://api-satusehat.kemkes.go.id/fhir-r4/v1/CarePlan"
            End If

            ' Setup request
            http.Open("POST", url, False)
            http.SetRequestHeader("Authorization", $"Bearer {accessToken}")
            http.SetRequestHeader("Content-Type", "application/json")

            ' Kirim data JSON
            http.Send(jsonPatientData)

            ' Cek response
            'If http.Status = 200 Or http.Status = 201 Then
            '    Return http.ResponseText
            'Else
            '    'Return http.ResponseText
            '    Return $"Error {http.Status}: {http.ResponseText}"
            'End If

            Return $"{http.Status}: {http.ResponseText}"

        Catch ex As Exception
            Return $"Exception: {ex.Message}"
        Finally
            If http IsNot Nothing Then Marshal.ReleaseComObject(http)
        End Try
    End Function
#End Region
#End Region
End Class