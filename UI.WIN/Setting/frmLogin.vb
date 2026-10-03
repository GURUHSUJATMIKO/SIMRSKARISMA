Imports DataAccess
Imports UI.WIN.MAIN.My.Resources

Public Class frmLogin
    Private ID As String = String.Empty
    Private sPassword As String = String.Empty

    Private Sub frmLogin_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim oConnection As New Setting.clsConnectionUser

        If Not oConnection.GetConnection() Then
            frmDatabaseUser.ShowDialog()
        Else
            If My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Software\SIMRS\USER\", "Database", "") <> Nothing Then
                Dim arrMain() As String = Decrypt(My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Software\SIMRS\USER\", "Database", "").ToString()).Split(";")

                For iLoop As Integer = 0 To arrMain.Length - 1
                    Dim arrMainResult() As String = arrMain(iLoop).Split("=")

                    For xLoop As Integer = 0 To arrMainResult.Length - 1
                        If arrMainResult(xLoop) = "Data Source" Then
                            'txtMainServer.Text = arrMainResult(xLoop + 1)
                        ElseIf arrMainResult(xLoop) = "Initial Catalog" Then
                            'txtMainDatabase.Text = arrMainResult(xLoop + 1)
                        ElseIf arrMainResult(xLoop) = "User ID" Then
                            ID = arrMainResult(xLoop + 1)
                        ElseIf arrMainResult(xLoop) = "Password" Then
                            sPassword = arrMainResult(xLoop + 1)
                        End If
                    Next
                Next
            End If
        End If
    End Sub
#Region "Function"
    Public Function fn_Login() As Boolean
        fn_Login = False

        Try
            Dim oUser As New Setting.clsUser
            If oUser.GetData(txtUsername.Text) IsNot Nothing Then
                Dim ds = oUser.GetData(txtUsername.Text)
                With ds
                    If ds.ISACTIVE = False Then
                        MsgBox("Username " & ds.KDUSER & " Tidak Active", MsgBoxStyle.Information, Me.Text)
                        Exit Function
                    End If

                    If txtUsername.Text = .KDUSER And txtPassword.Text = .PASSWORD Then
                        sUserID = .KDUSER

                        Dim oConnectionMain As New Setting.clsConnectionMain

                        Try
                            oConnectionMain.SaveToRegistry(Encrypt("Data Source=" & ds.KDSERVER & ";Initial Catalog=" & ds.KDDATABASE & ";Persist Security Info=True;User ID=sa;Password=" & sPassword & ""))
                        Catch ex As Exception

                        End Try
                        Try
                            oConnectionMain.SaveToRegistryTax(Encrypt("Data Source=" & ds.KDSERVER_TAX & ";Initial Catalog=" & ds.KDDATABASE_TAX & ";Persist Security Info=True;User ID=sa;Password=" & sPassword & ""))
                        Catch ex As Exception

                        End Try

                        Dim oConnectionAdmisi As New Setting.clsConnectionAdmision


                        Try
                            If sUserID = "RSKC" Then
                                oConnectionAdmisi.SaveToRegistry(Encrypt("Data Source=" & "EMEDREK-RSKC" & ";Initial Catalog=" & "RSKC" & ";Persist Security Info=True;User ID=sa;Password=" & sPassword & ""))

                            Else
                                oConnectionAdmisi.SaveToRegistry(Encrypt("Data Source=" & "192.168.2.212\MSSQLSERVER2016" & ";Initial Catalog=" & "BUDIJAYAGROUP_RSKC" & ";Persist Security Info=True;User ID=sa;Password=" & sPassword & ""))

                            End If
                        Catch ex As Exception

                        End Try

                        Dim oConnectionAdmisi2 As New Setting.clsConnectionMain2

                        Try
                            If sUserID = "RSKC" Then
                                oConnectionAdmisi2.SaveToRegistry(Encrypt("Data Source=" & "EMEDREK-RSKC" & ";Initial Catalog=" & "RSKC" & ";Persist Security Info=True;User ID=sa;Password=" & sPassword & ""))
                            Else
                                oConnectionAdmisi2.SaveToRegistry(Encrypt("Data Source=" & "192.168.2.212\MSSQLSERVER2016" & ";Initial Catalog=" & "BUDIJAYAGROUP_RSKC" & ";Persist Security Info=True;User ID=sa;Password=" & sPassword & ""))
                            End If
                        Catch ex As Exception

                        End Try

                        sCompany = ds.SET_COMPANY.COMPANY
                        sAddress = ds.SET_COMPANY.ADDRESS
                        sPhone = ds.SET_COMPANY.PHONE
                        sNPWP = ds.SET_COMPANY.NPWP

                        sKDSERVER = ds.KDSERVER
                        sKDDATABASE = ds.KDDATABASE
                        sKDSERVER_TAX = ds.KDSERVER_TAX
                        sKDDATABASE_TAX = ds.KDDATABASE_TAX
                        sKDCOMPANY = ds.KDCOMPANY

                        fn_LoadNameModueldanKoneksi()

                        fn_Login = True
                        Exit Function
                    Else
                        MsgBox(Statement.ErrorUserPassword, MsgBoxStyle.Information, Me.Text)
                        txtUsername.Focus()
                        Exit Function
                    End If
                End With
            Else
                MsgBox(Statement.ErrorUserPassword, MsgBoxStyle.Information, Me.Text)
                txtUsername.Focus()
                Exit Function
            End If
        Catch oErr As Exception
            MsgBox(Statement.ErrorUserPassword, MsgBoxStyle.Information, Me.Text)
        End Try
    End Function
    Private Sub fn_LoadNameModueldanKoneksi()
        Try
            Dim oNameModul As New Setting.clsCounter
            Dim ds = oNameModul.GetDataNameModul("DEMO")
            If ds IsNot Nothing Then
                sDaftar_L1 = ds.KDDAFTAR_L1
                sDaftar_L2 = ds.KDDAFTAR_L2
                sDaftar_L2_1 = ds.KDDAFTAR_L2_1
                sDaftar_L2_2 = ds.KDDAFTAR_L2_2
                sDaftar_L3 = ds.KDDAFTAR_L3
                sDaftar_L4 = ds.KDDAFTAR_L4
                sDaftar_L5 = ds.KDDAFTAR_L5
                sDaftar_L6 = ds.KDDAFTAR_L6

                sItem_L1 = ds.KDITEM_L1
                sItem_L2 = ds.KDITEM_L2
                sItem_L3 = ds.KDITEM_L3
                sItem_L4 = ds.KDITEM_L4
                sItem_L5 = ds.KDITEM_L5
                sItem_L6 = ds.KDITEM_L6
                sItem_L7 = ds.KDITEM_L7
            End If

            Dim oSetkoneksi As New Setting.clsBPJSKoneksi
            Dim dsSetKoneksiVClaim = oSetkoneksi.GetData("VCLAIM")

            If dsSetKoneksiVClaim IsNot Nothing Then
                sUrlVclaim = dsSetKoneksiVClaim.ALAMATWEB
                sConsidVclaim = dsSetKoneksiVClaim.CONSID
                sSecreateKeyVclaim = dsSetKoneksiVClaim.SECREATKEY
                sUserKeyVclaim = dsSetKoneksiVClaim.REMARKS
            End If

            Dim dsSetKoneksiAntrean = oSetkoneksi.GetData("ANTREAN")

            If dsSetKoneksiVClaim IsNot Nothing Then
                sUrlAntrean = dsSetKoneksiVClaim.ALAMATWEB
                sConsidAntrean = dsSetKoneksiVClaim.CONSID
                sSecreateKeyAntrean = dsSetKoneksiVClaim.SECREATKEY
                sUserKeyAntrean = dsSetKoneksiVClaim.REMARKS
            End If

            Dim dsSetKoneksiSIMRSOLD = oSetkoneksi.GetData(IIf(sUserID = "RSKC", "SIMRS_OLD_LOCAL", "SIMRS_OLD"))

            If dsSetKoneksiSIMRSOLD IsNot Nothing Then
                sConnOld = dsSetKoneksiSIMRSOLD.ALAMATWEB
                sAlamatSuara = dsSetKoneksiSIMRSOLD.REMARKS
            End If

            Dim dsSetKoneksiTTDDokter = oSetkoneksi.GetData("FOLDER_TTDDOKTER")

            If dsSetKoneksiTTDDokter IsNot Nothing Then
                sAlamatTandaTanganDokter = dsSetKoneksiTTDDokter.ALAMATWEB
            End If

            Dim dsSetKoneksiTTDPerawat = oSetkoneksi.GetData("FOLDER_TTDPERAWAT")

            If dsSetKoneksiTTDPerawat IsNot Nothing Then
                sAlamatTandaTanganPerawat = dsSetKoneksiTTDPerawat.ALAMATWEB
            End If

            Dim dsSetKoneksiPDFLaboratorium = oSetkoneksi.GetData("FOLDER_LABORATORIUM")

            If dsSetKoneksiPDFLaboratorium IsNot Nothing Then
                sAlamatSimpanPDFLaboratorium = dsSetKoneksiPDFLaboratorium.ALAMATWEB
            End If

            Dim dsSetKoneksiEclaim = oSetkoneksi.GetData("ECLAIM")

            If dsSetKoneksiEclaim IsNot Nothing Then
                sKodeTarifEClaim = dsSetKoneksiEclaim.PPKPELAYANAN

            End If

            Dim dsSetKoneksiRadiologi = oSetkoneksi.GetData("BRIGGING_RADIOLOGI")

            If dsSetKoneksiRadiologi IsNot Nothing Then
                sAlamatBriggingRadiologi = dsSetKoneksiRadiologi.ALAMATWEB
            End If

            Dim dsSetKoneksiAsesmenAwalIGD = oSetkoneksi.GetData("FOLDER_ASESMENIGD")

            If dsSetKoneksiAsesmenAwalIGD IsNot Nothing Then
                sAlamatSimpanAsesmenIGD = dsSetKoneksiAsesmenAwalIGD.ALAMATWEB
            End If

            Dim dsSetKoneksiAsesmenAwalIGDSharing = oSetkoneksi.GetData("SHARING_ASESMENMEDISIGD")

            If dsSetKoneksiAsesmenAwalIGDSharing IsNot Nothing Then
                sAlamatSimpanPDFAsesmenIGD = dsSetKoneksiAsesmenAwalIGDSharing.ALAMATWEB
            End If

            'If IO.Directory.Exists(CopyLocal) Then
            '    sAlamatTandaTanganDokter = CopyLocal
            'End If

        Catch ex As Exception

        End Try
    End Sub
#End Region

#Region "Command Button"
    Private Sub btnLogin_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLogin.Click
        If fn_Login() Then
            Me.Close()
        End If
    End Sub
    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Environment.Exit(1)
    End Sub
#End Region
End Class