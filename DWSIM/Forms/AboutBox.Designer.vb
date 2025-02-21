<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class AboutBox
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(AboutBox))
        Me.Label1 = New System.Windows.Forms.Label()
        Me.LinkLabel1 = New System.Windows.Forms.LinkLabel()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.Version = New System.Windows.Forms.Label()
        Me.Copyright = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.LinkLabel2 = New System.Windows.Forms.LinkLabel()
        Me.LabelLicense = New System.Windows.Forms.Label()
        Me.TextBox1 = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.LblOSInfo = New System.Windows.Forms.Label()
        Me.LblCLRInfo = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Lblcpuinfo = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Lblmem = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        Me.tbAcknowledgements = New System.Windows.Forms.TextBox()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabControl1.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        Me.TabPage2.SuspendLayout()
        Me.SuspendLayout()
        '
        'Label1
        '
        resources.ApplyResources(Me.Label1, "Label1")
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Name = "Label1"
        '
        'LinkLabel1
        '
        resources.ApplyResources(Me.LinkLabel1, "LinkLabel1")
        Me.LinkLabel1.BackColor = System.Drawing.Color.Transparent
        Me.LinkLabel1.Name = "LinkLabel1"
        Me.LinkLabel1.TabStop = True
        '
        'Button1
        '
        resources.ApplyResources(Me.Button1, "Button1")
        Me.Button1.Name = "Button1"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'Version
        '
        resources.ApplyResources(Me.Version, "Version")
        Me.Version.BackColor = System.Drawing.Color.Transparent
        Me.Version.Name = "Version"
        '
        'Copyright
        '
        resources.ApplyResources(Me.Copyright, "Copyright")
        Me.Copyright.BackColor = System.Drawing.Color.Transparent
        Me.Copyright.Name = "Copyright"
        '
        'Label2
        '
        resources.ApplyResources(Me.Label2, "Label2")
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Name = "Label2"
        '
        'LinkLabel2
        '
        resources.ApplyResources(Me.LinkLabel2, "LinkLabel2")
        Me.LinkLabel2.BackColor = System.Drawing.Color.Transparent
        Me.LinkLabel2.Name = "LinkLabel2"
        Me.LinkLabel2.TabStop = True
        '
        'LabelLicense
        '
        resources.ApplyResources(Me.LabelLicense, "LabelLicense")
        Me.LabelLicense.BackColor = System.Drawing.Color.Transparent
        Me.LabelLicense.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.LabelLicense.Name = "LabelLicense"
        '
        'TextBox1
        '
        resources.ApplyResources(Me.TextBox1, "TextBox1")
        Me.TextBox1.BackColor = System.Drawing.Color.White
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.ReadOnly = True
        '
        'Label7
        '
        resources.ApplyResources(Me.Label7, "Label7")
        Me.Label7.BackColor = System.Drawing.Color.Transparent
        Me.Label7.Name = "Label7"
        '
        'LblOSInfo
        '
        resources.ApplyResources(Me.LblOSInfo, "LblOSInfo")
        Me.LblOSInfo.BackColor = System.Drawing.Color.Transparent
        Me.LblOSInfo.Name = "LblOSInfo"
        '
        'LblCLRInfo
        '
        resources.ApplyResources(Me.LblCLRInfo, "LblCLRInfo")
        Me.LblCLRInfo.BackColor = System.Drawing.Color.Transparent
        Me.LblCLRInfo.Name = "LblCLRInfo"
        '
        'Label4
        '
        resources.ApplyResources(Me.Label4, "Label4")
        Me.Label4.BackColor = System.Drawing.Color.Transparent
        Me.Label4.Name = "Label4"
        '
        'Lblcpuinfo
        '
        resources.ApplyResources(Me.Lblcpuinfo, "Lblcpuinfo")
        Me.Lblcpuinfo.BackColor = System.Drawing.Color.Transparent
        Me.Lblcpuinfo.Name = "Lblcpuinfo"
        '
        'Label12
        '
        resources.ApplyResources(Me.Label12, "Label12")
        Me.Label12.BackColor = System.Drawing.Color.Transparent
        Me.Label12.Name = "Label12"
        '
        'Lblmem
        '
        resources.ApplyResources(Me.Lblmem, "Lblmem")
        Me.Lblmem.BackColor = System.Drawing.Color.Transparent
        Me.Lblmem.Name = "Lblmem"
        '
        'Label11
        '
        resources.ApplyResources(Me.Label11, "Label11")
        Me.Label11.BackColor = System.Drawing.Color.Transparent
        Me.Label11.Name = "Label11"
        '
        'PictureBox1
        '
        Me.PictureBox1.Image = Global.DWSIM.My.Resources.Resources.DWSIM_Icon_Vector_2
        resources.ApplyResources(Me.PictureBox1, "PictureBox1")
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.TabStop = False
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Controls.Add(Me.TabPage2)
        resources.ApplyResources(Me.TabControl1, "TabControl1")
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.TextBox1)
        Me.TabPage1.Controls.Add(Me.Label2)
        Me.TabPage1.Controls.Add(Me.Lblcpuinfo)
        Me.TabPage1.Controls.Add(Me.Label7)
        Me.TabPage1.Controls.Add(Me.Label12)
        Me.TabPage1.Controls.Add(Me.LblOSInfo)
        Me.TabPage1.Controls.Add(Me.Lblmem)
        Me.TabPage1.Controls.Add(Me.LabelLicense)
        Me.TabPage1.Controls.Add(Me.Label11)
        Me.TabPage1.Controls.Add(Me.Label4)
        Me.TabPage1.Controls.Add(Me.LinkLabel1)
        Me.TabPage1.Controls.Add(Me.LblCLRInfo)
        Me.TabPage1.Controls.Add(Me.LinkLabel2)
        resources.ApplyResources(Me.TabPage1, "TabPage1")
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'TabPage2
        '
        Me.TabPage2.Controls.Add(Me.tbAcknowledgements)
        resources.ApplyResources(Me.TabPage2, "TabPage2")
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.UseVisualStyleBackColor = True
        '
        'tbAcknowledgements
        '
        Me.tbAcknowledgements.BackColor = System.Drawing.Color.White
        resources.ApplyResources(Me.tbAcknowledgements, "tbAcknowledgements")
        Me.tbAcknowledgements.Name = "tbAcknowledgements"
        Me.tbAcknowledgements.ReadOnly = True
        '
        'AboutBox
        '
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.BackColor = System.Drawing.Color.White
        Me.ControlBox = False
        Me.Controls.Add(Me.TabControl1)
        Me.Controls.Add(Me.PictureBox1)
        Me.Controls.Add(Me.Copyright)
        Me.Controls.Add(Me.Version)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.Label1)
        Me.DoubleBuffered = True
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Name = "AboutBox"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.TabPage1.PerformLayout()
        Me.TabPage2.ResumeLayout(False)
        Me.TabPage2.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Public WithEvents Label1 As System.Windows.Forms.Label
    Public WithEvents LinkLabel1 As System.Windows.Forms.LinkLabel
    Public WithEvents Button1 As System.Windows.Forms.Button
    Public WithEvents Version As System.Windows.Forms.Label
    Public WithEvents Copyright As System.Windows.Forms.Label
    Public WithEvents Label2 As System.Windows.Forms.Label
    Public WithEvents LinkLabel2 As System.Windows.Forms.LinkLabel
    Public WithEvents LabelLicense As System.Windows.Forms.Label
    Public WithEvents TextBox1 As System.Windows.Forms.TextBox
    Public WithEvents Label7 As System.Windows.Forms.Label
    Public WithEvents LblOSInfo As System.Windows.Forms.Label
    Public WithEvents LblCLRInfo As System.Windows.Forms.Label
    Public WithEvents Label4 As System.Windows.Forms.Label
    Public WithEvents Lblmem As System.Windows.Forms.Label
    Public WithEvents Label11 As System.Windows.Forms.Label
    Public WithEvents Lblcpuinfo As System.Windows.Forms.Label
    Public WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents TabControl1 As TabControl
    Friend WithEvents TabPage1 As TabPage
    Friend WithEvents TabPage2 As TabPage
    Public WithEvents tbAcknowledgements As TextBox
End Class
