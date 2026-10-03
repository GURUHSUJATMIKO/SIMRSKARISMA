Imports Newtonsoft.Json.Linq

Public Class frmGrouperStage2New
    Public Sub fn_LoadMe(ByVal jsonDecode As JObject)
        Dim table1 As DataTable
        Dim table2 As DataTable
        Dim table3 As DataTable
        Dim table4 As DataTable

        table1 = New DataTable("M_SPECIAL1")
        table1.Columns.Add("code")
        table1.Columns.Add("description")

        table2 = New DataTable("M_SPECIAL2")
        table2.Columns.Add("code")
        table2.Columns.Add("description")

        table3 = New DataTable("M_SPECIAL3")
        table3.Columns.Add("code")
        table3.Columns.Add("description")

        table4 = New DataTable("M_SPECIAL4")
        table4.Columns.Add("code")
        table4.Columns.Add("description")

        For Each item In jsonDecode("special_cmg_option")

            If item("type") = "Special Procedure" Then
                table1.Rows.Add(New String() {item("code"), item("description")})
            ElseIf item("type") = "Special Prosthesis" Then
                table2.Rows.Add(New String() {item("code"), item("description")})
            ElseIf item("type") = "Special Investigation" Then
                table3.Rows.Add(New String() {item("code"), item("description")})
            ElseIf item("type") = "Special Drug" Then
                table4.Rows.Add(New String() {item("code"), item("description")})
            End If
        Next

        grdKDSPECIAL1.Properties.DataSource = table1
        grdKDSPECIAL1.Properties.ValueMember = "code"
        grdKDSPECIAL1.Properties.DisplayMember = "description"

        grdKDSPECIAL2.Properties.DataSource = table2
        grdKDSPECIAL2.Properties.ValueMember = "code"
        grdKDSPECIAL2.Properties.DisplayMember = "description"

        grdKDSPECIAL3.Properties.DataSource = table3
        grdKDSPECIAL3.Properties.ValueMember = "code"
        grdKDSPECIAL3.Properties.DisplayMember = "description"

        grdKDSPECIAL4.Properties.DataSource = table4
        grdKDSPECIAL4.Properties.ValueMember = "code"
        grdKDSPECIAL4.Properties.DisplayMember = "description"
    End Sub
    Private Sub Form_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        sSpesialCMG = ""
    End Sub
    Private Sub Form_Closing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        Dispose()
    End Sub
    Private Overloads Sub Dispose()
        MyBase.Dispose()
        Me.Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
    Private Sub btnShift_Click(sender As Object, e As EventArgs) Handles btnShift.Click
        Dim list As New List(Of String)
        If grdKDSPECIAL1.Text <> "" Then
            list.Add(grdKDSPECIAL1.EditValue)
        End If
        If grdKDSPECIAL2.Text <> "" Then
            list.Add(grdKDSPECIAL2.EditValue)
        End If
        If grdKDSPECIAL3.Text <> "" Then
            list.Add(grdKDSPECIAL3.EditValue)
        End If
        If grdKDSPECIAL4.Text <> "" Then
            list.Add(grdKDSPECIAL4.EditValue)
        End If
        sSpesialCMG = String.Join("#", List.ToArray)
        Me.Close()
    End Sub
End Class