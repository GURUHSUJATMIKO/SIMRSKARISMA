Namespace Setting
    Public Class clsBPJSKoneksi
        Public oConnection As Setting.clsConnectionUser = Nothing
        Public oError As Setting.clsError = Nothing
        Public sMODUL As String = ""
        Public sREFERENCE As String = ""
        Public sSTATUS As String = ""

        Public Sub New(Optional ByVal sConnection As String = "")
            oConnection = New Setting.clsConnectionUser
            If sConnection = "" Then
                oError = New Setting.clsError
            Else
                oError = New Setting.clsError("TAX")
            End If
            sMODUL = "BPJS_KONEKSI"
        End Sub
        Protected Overrides Sub Finalize()
            MyBase.Finalize()
            oConnection = Nothing
            GC.SuppressFinalize(Me)
        End Sub
        Public Function GetStructureHeader() As SET_BPJS_KONEKSI
            If Not oConnection.GetConnection() Then
                GetStructureHeader = Nothing
            End If
            GetStructureHeader = New SET_BPJS_KONEKSI
        End Function
        Public Function GetData(ByVal sKDKONEKSI As String) As SET_BPJS_KONEKSI
            If Not oConnection.GetConnection() Then
                GetData = Nothing
                Exit Function
            End If
            GetData = oConnection.db.SET_BPJS_KONEKSIs.FirstOrDefault(Function(x) x.KDKONEKSI = sKDKONEKSI And x.ISACTIVE = True)
        End Function
    End Class
End Namespace

