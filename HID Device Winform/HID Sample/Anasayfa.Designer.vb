﻿<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Anasayfa
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Gelen_Data_Text = New System.Windows.Forms.RichTextBox()
        Me.Data_Gonder_Buton = New System.Windows.Forms.Button()
        Me.Cihaz_Durumu_Label = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Giden_Data_Text = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'Label3
        '
        Me.Label3.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(162, Byte))
        Me.Label3.Location = New System.Drawing.Point(125, 133)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(91, 20)
        Me.Label3.TabIndex = 16
        Me.Label3.Text = "Gelen Data"
        '
        'Gelen_Data_Text
        '
        Me.Gelen_Data_Text.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Gelen_Data_Text.Location = New System.Drawing.Point(12, 156)
        Me.Gelen_Data_Text.Name = "Gelen_Data_Text"
        Me.Gelen_Data_Text.Size = New System.Drawing.Size(334, 150)
        Me.Gelen_Data_Text.TabIndex = 15
        Me.Gelen_Data_Text.Text = ""
        '
        'Data_Gonder_Buton
        '
        Me.Data_Gonder_Buton.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Data_Gonder_Buton.Location = New System.Drawing.Point(12, 49)
        Me.Data_Gonder_Buton.Name = "Data_Gonder_Buton"
        Me.Data_Gonder_Buton.Size = New System.Drawing.Size(334, 55)
        Me.Data_Gonder_Buton.TabIndex = 14
        Me.Data_Gonder_Buton.Text = "Data Gönder"
        Me.Data_Gonder_Buton.UseVisualStyleBackColor = True
        '
        'Cihaz_Durumu_Label
        '
        Me.Cihaz_Durumu_Label.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Cihaz_Durumu_Label.AutoSize = True
        Me.Cihaz_Durumu_Label.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(162, Byte))
        Me.Cihaz_Durumu_Label.Location = New System.Drawing.Point(125, 323)
        Me.Cihaz_Durumu_Label.Name = "Cihaz_Durumu_Label"
        Me.Cihaz_Durumu_Label.Size = New System.Drawing.Size(128, 20)
        Me.Cihaz_Durumu_Label.TabIndex = 13
        Me.Cihaz_Durumu_Label.Text = "Cihaz Bağlı Değil"
        '
        'Label1
        '
        Me.Label1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(162, Byte))
        Me.Label1.Location = New System.Drawing.Point(11, 323)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(114, 20)
        Me.Label1.TabIndex = 12
        Me.Label1.Text = "Cihaz Durumu:"
        '
        'Giden_Data_Text
        '
        Me.Giden_Data_Text.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(162, Byte))
        Me.Giden_Data_Text.Location = New System.Drawing.Point(105, 14)
        Me.Giden_Data_Text.Name = "Giden_Data_Text"
        Me.Giden_Data_Text.Size = New System.Drawing.Size(241, 29)
        Me.Giden_Data_Text.TabIndex = 17
        Me.Giden_Data_Text.Text = "1234"
        '
        'Label2
        '
        Me.Label2.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(162, Byte))
        Me.Label2.Location = New System.Drawing.Point(8, 20)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(91, 20)
        Me.Label2.TabIndex = 18
        Me.Label2.Text = "Giden Data"
        '
        'Anasayfa
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(365, 362)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Giden_Data_Text)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Gelen_Data_Text)
        Me.Controls.Add(Me.Data_Gonder_Buton)
        Me.Controls.Add(Me.Cihaz_Durumu_Label)
        Me.Controls.Add(Me.Label1)
        Me.Name = "Anasayfa"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Anasayfa"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Label3 As Label
    Friend WithEvents Gelen_Data_Text As RichTextBox
    Friend WithEvents Data_Gonder_Buton As Button
    Friend WithEvents Cihaz_Durumu_Label As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents Giden_Data_Text As TextBox
    Friend WithEvents Label2 As Label
End Class
