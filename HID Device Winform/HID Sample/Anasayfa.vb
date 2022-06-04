Imports Argeoloji_Usb_HID_Library

Public Class Anasayfa
    Private ReadOnly Usb1 As New Usb_HID_Port(1590, 1590)

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

    Private Sub Role_Cektir_Click(sender As Object, e As EventArgs) Handles Role_Cektir.Click
        Dim Buffer(8) As Byte
        Buffer(1) = 10 ' Bizim Cihazımızın Röleyi Çektirmek için Kabul Ettiği Kod.
        Usb1.Send_Data(Buffer)
    End Sub

    Private Sub input_Durum_Sorgu_Click(sender As Object, e As EventArgs) Handles input_Durum_Sorgu.Click
        Dim SoruBuffer(8) As Byte
        SoruBuffer(1) = 64 ' Soru Sorma Datası
        Usb1.Send_Data(SoruBuffer) ' Cihaza Soru Soruyoruz ve Ardından Cevap Bekliyoruz.

        Dim GelenBytes(8) As Byte ' Kaç Bytelık Bir data bekliyorsak O Kadarlık bir Dize Oluşturuyoruz. Fazla Olabilir 8 den Az Olamaz.
        Usb1.Read_Data(GelenBytes, 1000) ' 1 Saniyelik Time out Süresi Verilmiştir.

        Dim Data As String = Nothing
        For i = 1 To GelenBytes.Length - 1
            Data &= GelenBytes(i)
        Next

        RichTextBox1.Text = Data
    End Sub

    Private Sub Anasayfa_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub
End Class
