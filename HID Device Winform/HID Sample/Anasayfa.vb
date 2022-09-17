Imports System.Text
Imports Argeoloji_Usb_HID_Library

Public Class Anasayfa
    Private ReadOnly Usb1 As New Usb_HID_Port(4660, 4660)

    Sub New()

        InitializeComponent()
        Usb1.RegisterHandleNotifications(Me.Handle)
        AddHandler Usb1.Usb_Plug_Event, AddressOf Usb_Takildi_Cikarildi
        If Usb1.CheckDevicePresent() = False Then
            Cihaz_Durumu_Label.Text = "Cihaz Bağlı Değil"
            Cihaz_Durumu_Label.ForeColor = Color.Red
        End If

    End Sub

    Protected Overrides Sub WndProc(ByRef m As Message)
        Usb1.ParseMessages(m)
        MyBase.WndProc(m)
    End Sub

    Private Sub Usb_Takildi_Cikarildi(ByVal o As Object, ByVal e As EventArgs)

        If Usb1.CihazBagli = True Then
            Cihaz_Durumu_Label.Text = "Cihaz Bağlı"
            Cihaz_Durumu_Label.ForeColor = Color.Green
        Else
            Cihaz_Durumu_Label.Text = "Cihaz Bağlı Değil"
            Cihaz_Durumu_Label.ForeColor = Color.Red
        End If
    End Sub




    Private Sub Anasayfa_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub Data_Gonder_Buton_Click(sender As Object, e As EventArgs) Handles Data_Gonder_Buton.Click

        Gelen_Data_Text.ResetText()
        Application.DoEvents()
        Dim SoruBuffer(64) As Byte
        Array.Copy(Encoding.ASCII.GetBytes(Giden_Data_Text.Text), 0, SoruBuffer, 1, Giden_Data_Text.Text.Length)
        Usb1.Send_Data(SoruBuffer) ' Cihaza Soru Soruyoruz ve Ardından Cevap Bekliyoruz.



        Dim GelenBytes(64) As Byte ' Kaç Bytelık Bir data bekliyorsak O Kadarlık bir Dize Oluşturuyoruz. Fazla Olabilir 8 den Az Olamaz.
        If Usb1.Read_Data(GelenBytes, 100) Then ' 100 ms Bekle
            Dim Data As String = Nothing
            For i = 1 To GelenBytes.Length - 1
                Data &= GelenBytes(i) & ","
            Next

            Gelen_Data_Text.Text = Data
        End If


    End Sub
End Class
