<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Form1
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.connectString = New System.Windows.Forms.TextBox()
        Me.bConnect = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.bDisconnect = New System.Windows.Forms.Button()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.GedcomFile = New System.Windows.Forms.TextBox()
        Me.bSelect = New System.Windows.Forms.Button()
        Me.OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog()
        Me.bImport = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'connectString
        '
        Me.connectString.Location = New System.Drawing.Point(187, 10)
        Me.connectString.Name = "connectString"
        Me.connectString.Size = New System.Drawing.Size(510, 20)
        Me.connectString.TabIndex = 0
        Me.connectString.Text = "Server=(LocalDB)\AncestryToLocalDB;Integrated Security=true;Initial Catalog=Ances" &
    "tryGedCom"
        '
        'bConnect
        '
        Me.bConnect.Location = New System.Drawing.Point(703, 8)
        Me.bConnect.Name = "bConnect"
        Me.bConnect.Size = New System.Drawing.Size(75, 23)
        Me.bConnect.TabIndex = 1
        Me.bConnect.Text = "Connect"
        Me.bConnect.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.ForeColor = System.Drawing.Color.Maroon
        Me.Label1.Location = New System.Drawing.Point(11, 14)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(170, 13)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "Connection String (not Connected)"
        '
        'bDisconnect
        '
        Me.bDisconnect.Enabled = False
        Me.bDisconnect.Location = New System.Drawing.Point(784, 7)
        Me.bDisconnect.Name = "bDisconnect"
        Me.bDisconnect.Size = New System.Drawing.Size(75, 23)
        Me.bDisconnect.TabIndex = 3
        Me.bDisconnect.Text = "Disconnect"
        Me.bDisconnect.UseVisualStyleBackColor = True
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(111, 45)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(70, 13)
        Me.Label2.TabIndex = 4
        Me.Label2.Text = "GedCom File:"
        '
        'GedcomFile
        '
        Me.GedcomFile.Location = New System.Drawing.Point(187, 42)
        Me.GedcomFile.Name = "GedcomFile"
        Me.GedcomFile.Size = New System.Drawing.Size(510, 20)
        Me.GedcomFile.TabIndex = 5
        Me.GedcomFile.Text = "D:\OneDrive\Téléchargements\Eric Gagné.ged"
        '
        'bSelect
        '
        Me.bSelect.Location = New System.Drawing.Point(703, 42)
        Me.bSelect.Name = "bSelect"
        Me.bSelect.Size = New System.Drawing.Size(75, 23)
        Me.bSelect.TabIndex = 6
        Me.bSelect.Text = "Select"
        Me.bSelect.UseVisualStyleBackColor = True
        '
        'OpenFileDialog1
        '
        Me.OpenFileDialog1.DefaultExt = "ged"
        Me.OpenFileDialog1.FileName = "*.ged"
        Me.OpenFileDialog1.Filter = "ged files|*.ged"
        Me.OpenFileDialog1.Title = "GedCom File Selection"
        '
        'bImport
        '
        Me.bImport.Location = New System.Drawing.Point(784, 42)
        Me.bImport.Name = "bImport"
        Me.bImport.Size = New System.Drawing.Size(75, 23)
        Me.bImport.TabIndex = 7
        Me.bImport.Text = "Import"
        Me.bImport.UseVisualStyleBackColor = True
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(875, 116)
        Me.Controls.Add(Me.bImport)
        Me.Controls.Add(Me.bSelect)
        Me.Controls.Add(Me.GedcomFile)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.bDisconnect)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.bConnect)
        Me.Controls.Add(Me.connectString)
        Me.Name = "Form1"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Form1"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents connectString As TextBox
    Friend WithEvents bConnect As Button
    Friend WithEvents Label1 As Label
    Friend WithEvents bDisconnect As Button
    Friend WithEvents Label2 As Label
    Friend WithEvents GedcomFile As TextBox
    Friend WithEvents bSelect As Button
    Friend WithEvents OpenFileDialog1 As OpenFileDialog
    Friend WithEvents bImport As Button
End Class
