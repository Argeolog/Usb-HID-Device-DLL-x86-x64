<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
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
        Me.RichTextBox1 = New System.Windows.Forms.RichTextBox()
        Me.input_Durum_Sorgu = New System.Windows.Forms.Button()
        Me.Cihaz_Durumu_Label = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Role_Cektir = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'Label3
        '
        Me.Label3.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(162, Byte))
        Me.Label3.Location = New System.Drawing.Point(131, 137)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(91, 20)
        Me.Label3.TabIndex = 16
        Me.Label3.Text = "Gelen Data"
        '
        'RichTextBox1
        '
        Me.RichTextBox1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.RichTextBox1.Location = New System.Drawing.Point(12, 160)
        Me.RichTextBox1.Name = "RichTextBox1"
        Me.RichTextBox1.Size = New System.Drawing.Size(334, 80)
        Me.RichTextBox1.TabIndex = 15
        Me.RichTextBox1.Text = ""
        '
        'input_Durum_Sorgu
        '
        Me.input_Durum_Sorgu.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.input_Durum_Sorgu.Location = New System.Drawing.Point(12, 79)
        Me.input_Durum_Sorgu.Name = "input_Durum_Sorgu"
        Me.input_Durum_Sorgu.Size = New System.Drawing.Size(334, 55)
        Me.input_Durum_Sorgu.TabIndex = 14
        Me.input_Durum_Sorgu.Text = "İnput Durum Sorgula"
        Me.input_Durum_Sorgu.UseVisualStyleBackColor = True
        '
        'Cihaz_Durumu_Label
        '
        Me.Cihaz_Durumu_Label.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Cihaz_Durumu_Label.AutoSize = True
        Me.Cihaz_Durumu_Label.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(162, Byte))
        Me.Cihaz_Durumu_Label.Location = New System.Drawing.Point(125, 257)
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
        Me.Label1.Location = New System.Drawing.Point(11, 257)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(114, 20)
        Me.Label1.TabIndex = 12
        Me.Label1.Text = "Cihaz Durumu:"
        '
        'Role_Cektir
        '
        Me.Role_Cektir.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Role_Cektir.Location = New System.Drawing.Point(12, 12)
        Me.Role_Cektir.Name = "Role_Cektir"
        Me.Role_Cektir.Size = New System.Drawing.Size(334, 61)
        Me.Role_Cektir.TabIndex = 11
        Me.Role_Cektir.Text = "Röle Çektir"
        Me.Role_Cektir.UseVisualStyleBackColor = True
        '
        'Anasayfa
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(365, 296)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.RichTextBox1)
        Me.Controls.Add(Me.input_Durum_Sorgu)
        Me.Controls.Add(Me.Cihaz_Durumu_Label)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Role_Cektir)
        Me.Name = "Anasayfa"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Anasayfa"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Label3 As Label
    Friend WithEvents RichTextBox1 As RichTextBox
    Friend WithEvents input_Durum_Sorgu As Button
    Friend WithEvents Cihaz_Durumu_Label As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents Role_Cektir As Button
End Class
