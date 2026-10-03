Imports DataAccess
Imports System.Linq
Imports Newtonsoft.Json.Linq

Namespace SettingAntrian_UI
    Public Class clsSetAntrian_UI
        'Public Function fn_RequestUpdateWaktuAntrean(ByVal KODEBOOKING As String, ByVal ISPANGGIL As Integer, ByVal uTime As Integer) As String
        '    Try
        '        Dim oAntrian As New SettingAntrian.clsSetAntrian
        '        Dim jsonRequest As String = String.Empty

        '        Dim dsAntrian = oAntrian.GetData(KODEBOOKING)
        '        If dsAntrian IsNot Nothing Then
        '            jsonRequest = " { "
        '            jsonRequest &= """kodebooking"": """ & dsAntrian.KODEBOOKING & ""","
        '            jsonRequest &= """taskid"": """ & ISPANGGIL & """, "
        '            jsonRequest &= """waktu"": """ & uTime & """ "
        '            jsonRequest &= "}  "

        '            fn_RequestUpdateWaktuAntrean = jsonRequest

        '        Else
        '            fn_RequestUpdateWaktuAntrean = ""
        '        End If
        '    Catch oErr As Exception
        '        fn_RequestUpdateWaktuAntrean = ""
        '    End Try
        'End Function
        Public Function fn_UpdateWaktuAntrean(ByVal jsonRequest As String, ByVal uTime As Integer) As String
            Try
                Dim oSetKoneksi As New Brigging.clsSetKoneksi
                Dim dsDataSetKoneksi = oSetKoneksi.GetData().FirstOrDefault(Function(x) x.ISACTIVE = True And x.NAME_DISPLAY = "ANTREAN")

                If dsDataSetKoneksi IsNot Nothing Then
                    Dim dsSetKoneksi = oSetKoneksi.UpdateWaktuAntrean(dsDataSetKoneksi.ALAMATWEB, dsDataSetKoneksi.CONSID, dsDataSetKoneksi.SECREATKEY, dsDataSetKoneksi.USER_KEY, uTime, jsonRequest)

                    If dsSetKoneksi <> "" Then
                        Dim allData = JObject.Parse(dsSetKoneksi)

                        Dim CodeResponse As String = String.Empty
                        Dim messageResponse As String = String.Empty

                        CodeResponse = IIf(IsDBNull(allData.Item("metadata").Item("code")) = True, "", allData.Item("metadata").Item("code"))
                        messageResponse = allData("metadata")("message").ToString

                        'MsgBox(CodeResponse & " - " & messageResponse, MsgBoxStyle.Exclamation)

                        If CodeResponse = "200" Then
                            fn_UpdateWaktuAntrean = CodeResponse
                        Else
                            fn_UpdateWaktuAntrean = CodeResponse & "-" & messageResponse
                        End If
                    Else
                        fn_UpdateWaktuAntrean = "Update Waktu Antrean Kosong"
                    End If
                Else
                    fn_UpdateWaktuAntrean = "Koneksi Tidak ditemukan"
                End If
            Catch oErr As Exception
                fn_UpdateWaktuAntrean = oErr.Message
            End Try
        End Function
        'Public Function fn_RequestBatalAntrean(ByVal KODEBOOKING As String, ByVal KETERANGAN As String) As String
        '    Try
        '        Dim oAntrian As New SettingAntrian.clsSetAntrian
        '        Dim jsonRequest As String = String.Empty

        '        jsonRequest = " { "
        '        jsonRequest &= """kodebooking"": """ & KODEBOOKING & ""","
        '        jsonRequest &= """keterangan"": """ & KETERANGAN & """ "
        '        jsonRequest &= "}  "

        '        fn_RequestBatalAntrean = jsonRequest

        '    Catch oErr As Exception
        '        fn_RequestBatalAntrean = ""
        '    End Try
        'End Function
        'Public Function fn_BatalAntrean(ByVal jsonRequest As String, ByVal uTime As Integer) As String
        '    Try
        '        Dim oSetKoneksi As New Brigging.clsSetKoneksi
        '        Dim dsDataSetKoneksi = oSetKoneksi.GetData().FirstOrDefault(Function(x) x.ISACTIVE = True And x.NAME_DISPLAY = "ANTREAN")

        '        If dsDataSetKoneksi IsNot Nothing Then
        '            Dim dsSetKoneksi = oSetKoneksi.BatalAntrean(dsDataSetKoneksi.ALAMATWEB, dsDataSetKoneksi.CONSID, dsDataSetKoneksi.SECREATKEY, dsDataSetKoneksi.USER_KEY, uTime, jsonRequest)

        '            If dsSetKoneksi <> "" Then
        '                Dim allData = JObject.Parse(dsSetKoneksi)

        '                Dim CodeResponse As String = String.Empty
        '                Dim messageResponse As String = String.Empty

        '                CodeResponse = IIf(IsDBNull(allData.Item("metadata").Item("code")) = True, "", allData.Item("metadata").Item("code"))
        '                messageResponse = allData("metadata")("message").ToString

        '                If CodeResponse = "200" Then
        '                    fn_BatalAntrean = CodeResponse
        '                Else
        '                    fn_BatalAntrean = CodeResponse & "-" & messageResponse
        '                End If
        '            Else
        '                fn_BatalAntrean = "Batal Antrean Kosong"
        '            End If
        '        Else
        '            fn_BatalAntrean = "Koneksi Tidak ditemukan"
        '        End If
        '    Catch oErr As Exception
        '        fn_BatalAntrean = oErr.Message
        '    End Try
        'End Function
        'Public Function fn_RequestListWaktuTaskId(ByVal KODEBOOKING As String) As String
        '    Try
        '        Dim oAntrian As New SettingAntrian.clsSetAntrian
        '        Dim jsonRequest As String = String.Empty

        '        jsonRequest = " { "
        '        jsonRequest &= """kodebooking"": """ & KODEBOOKING & """ "
        '        jsonRequest &= "}  "

        '        fn_RequestListWaktuTaskId = jsonRequest

        '    Catch oErr As Exception
        '        fn_RequestListWaktuTaskId = ""
        '    End Try
        'End Function
        'Public Function fn_ListWaktuTaskId(ByVal jsonRequest As String, ByVal uTime As Integer) As String
        '    Try
        '        Dim oSetKoneksi As New Brigging.clsSetKoneksi
        '        Dim dsDataSetKoneksi = oSetKoneksi.GetData().FirstOrDefault(Function(x) x.ISACTIVE = True And x.NAME_DISPLAY = "ANTREAN")

        '        If dsDataSetKoneksi IsNot Nothing Then
        '            Dim dsSetKoneksi = oSetKoneksi.ListWaktuTaskId(dsDataSetKoneksi.ALAMATWEB, dsDataSetKoneksi.CONSID, dsDataSetKoneksi.SECREATKEY, dsDataSetKoneksi.USER_KEY, uTime, jsonRequest)

        '            If dsSetKoneksi <> "" Then
        '                Dim allData = JObject.Parse(dsSetKoneksi)

        '                Dim CodeResponse As String = String.Empty
        '                Dim messageResponse As String = String.Empty

        '                CodeResponse = IIf(IsDBNull(allData.Item("metadata").Item("code")) = True, "", allData.Item("metadata").Item("code"))
        '                messageResponse = allData("metadata")("message").ToString

        '                If CodeResponse = "200" Then
        '                    fn_ListWaktuTaskId = oSetKoneksi.Decrypt(allData("response"), dsDataSetKoneksi.CONSID & dsDataSetKoneksi.SECREATKEY & uTime)
        '                Else
        '                    fn_ListWaktuTaskId = CodeResponse & "-" & messageResponse
        '                End If
        '            Else
        '                fn_ListWaktuTaskId = "Batal Antrean Kosong"
        '            End If
        '        Else
        '            fn_ListWaktuTaskId = "Koneksi Tidak ditemukan"
        '        End If
        '    Catch oErr As Exception
        '        fn_ListWaktuTaskId = oErr.Message
        '    End Try
        'End Function
    End Class
End Namespace