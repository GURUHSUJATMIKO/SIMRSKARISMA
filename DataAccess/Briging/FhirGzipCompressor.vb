Imports System.IO
Imports System.IO.Compression
Imports System.Text
'Imports System.Web.Script.Serialization

Public Class FhirGzipCompressor
    Public Shared Function CompressFhirJson(ByVal jsonString As String) As Byte()
        If String.IsNullOrEmpty(jsonString) Then
            Return New Byte() {}
        End If

        Dim jsonBytes As Byte() = Encoding.UTF8.GetBytes(jsonString)

        Using outputStream As New MemoryStream()
            ' GANTI: CompressionLevel.Optimal → CompressionMode.Compress
            Using gzipStream As New GZipStream(outputStream, CompressionMode.Compress)
                gzipStream.Write(jsonBytes, 0, jsonBytes.Length)
            End Using
            Return outputStream.ToArray()
        End Using
    End Function
    ' Method utama untuk compress JSON FHIR ke byte array
    'Public Shared Function CompressFhirJson(ByVal jsonString As String) As Byte()
    '    If String.IsNullOrEmpty(jsonString) Then
    '        Return New Byte() {}
    '    End If

    '    Dim jsonBytes As Byte() = Encoding.UTF8.GetBytes(jsonString)

    '    Using outputStream As New MemoryStream()
    '        Using gzipStream As New GZipStream(outputStream, CompressionLevel.Optimal)
    '            gzipStream.Write(jsonBytes, 0, jsonBytes.Length)
    '        End Using
    '        Return outputStream.ToArray()
    '    End Using
    'End Function

    ' Compress ke Base64 string (untuk API/transfer)
    Public Shared Function CompressFhirJsonToBase64(ByVal jsonString As String) As String
        Dim compressedBytes As Byte() = CompressFhirJson(jsonString)
        Return Convert.ToBase64String(compressedBytes)
    End Function

    ' Decompress dari byte array ke JSON string
    Public Shared Function DecompressFhirJson(ByVal compressedBytes As Byte()) As String
        If compressedBytes Is Nothing OrElse compressedBytes.Length = 0 Then
            Return String.Empty
        End If

        Using inputStream As New MemoryStream(compressedBytes)
            Using outputStream As New MemoryStream()
                Using gzipStream As New GZipStream(inputStream, CompressionMode.Decompress)
                    gzipStream.CopyTo(outputStream)
                End Using
                Return Encoding.UTF8.GetString(outputStream.ToArray())
            End Using
        End Using
    End Function

    ' Decompress dari Base64 string ke JSON string
    Public Shared Function DecompressFhirJsonFromBase64(ByVal base64String As String) As String
        Dim compressedBytes As Byte() = Convert.FromBase64String(base64String)
        Return DecompressFhirJson(compressedBytes)
    End Function

    ' Method untuk compress dengan info statistik
    Public Shared Function CompressWithStats(ByVal jsonString As String) As CompressionResult
        Dim result As New CompressionResult()

        result.OriginalSize = Encoding.UTF8.GetByteCount(jsonString)
        result.OriginalLength = jsonString.Length

        Dim compressedBytes As Byte() = CompressFhirJson(jsonString)
        result.CompressedSize = compressedBytes.Length
        result.CompressedBase64 = Convert.ToBase64String(compressedBytes)
        result.CompressionRatio = (1 - (result.CompressedSize / result.OriginalSize)) * 100

        Return result
    End Function

    ' Method untuk compress dan save ke file
    Public Shared Sub CompressAndSaveToFile(ByVal jsonString As String, ByVal filePath As String)
        Dim compressedBytes As Byte() = CompressFhirJson(jsonString)
        File.WriteAllBytes(filePath, compressedBytes)
    End Sub

    ' Method untuk decompress dari file
    Public Shared Function DecompressFromFile(ByVal filePath As String) As String
        Dim compressedBytes As Byte() = File.ReadAllBytes(filePath)
        Return DecompressFhirJson(compressedBytes)
    End Function

End Class

' Class untuk menyimpan hasil kompresi dengan statistik
Public Class CompressionResult
    Public Property OriginalSize As Long
    Public Property OriginalLength As Integer
    Public Property CompressedSize As Long
    Public Property CompressedBase64 As String
    Public Property CompressionRatio As Double

    Public Overrides Function ToString() As String
        Return String.Format("Original Size: {0:N0} bytes | Original Length: {1:N0} chars" & vbCrLf &
                            "Compressed Size: {2:N0} bytes | Compression Ratio: {3:F2}%" & vbCrLf &
                            "Base64 Length: {4:N0} chars",
                            OriginalSize, OriginalLength, CompressedSize, CompressionRatio, CompressedBase64.Length)
    End Function
End Class