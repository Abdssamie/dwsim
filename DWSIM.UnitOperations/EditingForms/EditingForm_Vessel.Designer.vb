<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class EditingForm_Vessel
    Inherits SharedClasses.ObjectEditorForm

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
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(EditingForm_Vessel))
        Me.GroupBoxConnections = New System.Windows.Forms.GroupBox()
        Me.btnCreateAndConnectEnergy = New System.Windows.Forms.Button()
        Me.btnCreateAndConnectOutlet3 = New System.Windows.Forms.Button()
        Me.btnCreateAndConnectOutlet2 = New System.Windows.Forms.Button()
        Me.btnCreateAndConnectOutlet1 = New System.Windows.Forms.Button()
        Me.btnCreateAndConnectInlet6 = New System.Windows.Forms.Button()
        Me.btnCreateAndConnectInlet5 = New System.Windows.Forms.Button()
        Me.btnCreateAndConnectInlet4 = New System.Windows.Forms.Button()
        Me.btnCreateAndConnectInlet3 = New System.Windows.Forms.Button()
        Me.btnCreateAndConnectInlet2 = New System.Windows.Forms.Button()
        Me.btnCreateAndConnectInlet1 = New System.Windows.Forms.Button()
        Me.btnDisconnectEnergy = New System.Windows.Forms.Button()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.cbEnergy = New System.Windows.Forms.ComboBox()
        Me.btnDisconnectOutlet3 = New System.Windows.Forms.Button()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.cbOutlet3 = New System.Windows.Forms.ComboBox()
        Me.btnDisconnectOutlet2 = New System.Windows.Forms.Button()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.cbOutlet2 = New System.Windows.Forms.ComboBox()
        Me.btnDisconnectOutlet1 = New System.Windows.Forms.Button()
        Me.btnDisconnect6 = New System.Windows.Forms.Button()
        Me.btnDisconnect5 = New System.Windows.Forms.Button()
        Me.btnDisconnect4 = New System.Windows.Forms.Button()
        Me.btnDisconnect3 = New System.Windows.Forms.Button()
        Me.btnDisconnect2 = New System.Windows.Forms.Button()
        Me.btnDisconnect1 = New System.Windows.Forms.Button()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.cbOutlet1 = New System.Windows.Forms.ComboBox()
        Me.cbInlet6 = New System.Windows.Forms.ComboBox()
        Me.cbInlet5 = New System.Windows.Forms.ComboBox()
        Me.cbInlet4 = New System.Windows.Forms.ComboBox()
        Me.cbInlet3 = New System.Windows.Forms.ComboBox()
        Me.cbInlet2 = New System.Windows.Forms.ComboBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.cbInlet1 = New System.Windows.Forms.ComboBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.GroupBoxParameters = New System.Windows.Forms.GroupBox()
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.cbCalcMode = New System.Windows.Forms.ComboBox()
        Me.tbTemperature = New System.Windows.Forms.TextBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.cbTemp = New System.Windows.Forms.ComboBox()
        Me.cbPressureCalcMode = New System.Windows.Forms.ComboBox()
        Me.chkOverrideT = New System.Windows.Forms.CheckBox()
        Me.chkOverrideP = New System.Windows.Forms.CheckBox()
        Me.cbPress = New System.Windows.Forms.ComboBox()
        Me.tbPressure = New System.Windows.Forms.TextBox()
        Me.TabPage3 = New System.Windows.Forms.TabPage()
        Me.cbHeadType = New System.Windows.Forms.ComboBox()
        Me.Label24 = New System.Windows.Forms.Label()
        Me.rbHorizontal = New System.Windows.Forms.RadioButton()
        Me.rbVertical = New System.Windows.Forms.RadioButton()
        Me.Label23 = New System.Windows.Forms.Label()
        Me.lbHeight = New System.Windows.Forms.Label()
        Me.Label22 = New System.Windows.Forms.Label()
        Me.tbHeight = New System.Windows.Forms.TextBox()
        Me.lbDiam = New System.Windows.Forms.Label()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.tbDiam = New System.Windows.Forms.TextBox()
        Me.lblThickness = New System.Windows.Forms.Label()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.cbWallMaterial = New System.Windows.Forms.ComboBox()
        Me.tbThickness = New System.Windows.Forms.TextBox()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        Me.GroupBox5 = New System.Windows.Forms.GroupBox()
        Me.btnUtils = New System.Windows.Forms.Button()
        Me.lblTag = New System.Windows.Forms.TextBox()
        Me.chkActive = New System.Windows.Forms.CheckBox()
        Me.lblConnectedTo = New System.Windows.Forms.Label()
        Me.lblStatus = New System.Windows.Forms.Label()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.GroupBox4 = New System.Windows.Forms.GroupBox()
        Me.rtbAnnotations = New Extended.Windows.Forms.RichTextBoxExtended()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.btnConfigurePP = New System.Windows.Forms.Button()
        Me.cbPropPack = New System.Windows.Forms.ComboBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.UtilitiesCtxMenu = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.AddUtilityTSMI = New System.Windows.Forms.ToolStripMenuItem()
        Me.sizingtsmi = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolTipChangeTag = New System.Windows.Forms.ToolTip(Me.components)
        Me.Button1 = New System.Windows.Forms.Button()
        Me.GroupBoxConnections.SuspendLayout()
        Me.GroupBoxParameters.SuspendLayout()
        Me.TabControl1.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        Me.TabPage3.SuspendLayout()
        Me.GroupBox5.SuspendLayout()
        Me.GroupBox4.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.UtilitiesCtxMenu.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBoxConnections
        '
        resources.ApplyResources(Me.GroupBoxConnections, "GroupBoxConnections")
        Me.GroupBoxConnections.Controls.Add(Me.btnCreateAndConnectEnergy)
        Me.GroupBoxConnections.Controls.Add(Me.btnCreateAndConnectOutlet3)
        Me.GroupBoxConnections.Controls.Add(Me.btnCreateAndConnectOutlet2)
        Me.GroupBoxConnections.Controls.Add(Me.btnCreateAndConnectOutlet1)
        Me.GroupBoxConnections.Controls.Add(Me.btnCreateAndConnectInlet6)
        Me.GroupBoxConnections.Controls.Add(Me.btnCreateAndConnectInlet5)
        Me.GroupBoxConnections.Controls.Add(Me.btnCreateAndConnectInlet4)
        Me.GroupBoxConnections.Controls.Add(Me.btnCreateAndConnectInlet3)
        Me.GroupBoxConnections.Controls.Add(Me.btnCreateAndConnectInlet2)
        Me.GroupBoxConnections.Controls.Add(Me.btnCreateAndConnectInlet1)
        Me.GroupBoxConnections.Controls.Add(Me.btnDisconnectEnergy)
        Me.GroupBoxConnections.Controls.Add(Me.Label16)
        Me.GroupBoxConnections.Controls.Add(Me.cbEnergy)
        Me.GroupBoxConnections.Controls.Add(Me.btnDisconnectOutlet3)
        Me.GroupBoxConnections.Controls.Add(Me.Label15)
        Me.GroupBoxConnections.Controls.Add(Me.cbOutlet3)
        Me.GroupBoxConnections.Controls.Add(Me.btnDisconnectOutlet2)
        Me.GroupBoxConnections.Controls.Add(Me.Label14)
        Me.GroupBoxConnections.Controls.Add(Me.cbOutlet2)
        Me.GroupBoxConnections.Controls.Add(Me.btnDisconnectOutlet1)
        Me.GroupBoxConnections.Controls.Add(Me.btnDisconnect6)
        Me.GroupBoxConnections.Controls.Add(Me.btnDisconnect5)
        Me.GroupBoxConnections.Controls.Add(Me.btnDisconnect4)
        Me.GroupBoxConnections.Controls.Add(Me.btnDisconnect3)
        Me.GroupBoxConnections.Controls.Add(Me.btnDisconnect2)
        Me.GroupBoxConnections.Controls.Add(Me.btnDisconnect1)
        Me.GroupBoxConnections.Controls.Add(Me.Label7)
        Me.GroupBoxConnections.Controls.Add(Me.Label6)
        Me.GroupBoxConnections.Controls.Add(Me.Label5)
        Me.GroupBoxConnections.Controls.Add(Me.Label4)
        Me.GroupBoxConnections.Controls.Add(Me.Label3)
        Me.GroupBoxConnections.Controls.Add(Me.cbOutlet1)
        Me.GroupBoxConnections.Controls.Add(Me.cbInlet6)
        Me.GroupBoxConnections.Controls.Add(Me.cbInlet5)
        Me.GroupBoxConnections.Controls.Add(Me.cbInlet4)
        Me.GroupBoxConnections.Controls.Add(Me.cbInlet3)
        Me.GroupBoxConnections.Controls.Add(Me.cbInlet2)
        Me.GroupBoxConnections.Controls.Add(Me.Label2)
        Me.GroupBoxConnections.Controls.Add(Me.cbInlet1)
        Me.GroupBoxConnections.Controls.Add(Me.Label1)
        Me.GroupBoxConnections.Name = "GroupBoxConnections"
        Me.GroupBoxConnections.TabStop = False
        Me.ToolTipChangeTag.SetToolTip(Me.GroupBoxConnections, resources.GetString("GroupBoxConnections.ToolTip"))
        Me.ToolTipValues.SetToolTip(Me.GroupBoxConnections, resources.GetString("GroupBoxConnections.ToolTip1"))
        Me.ToolTip1.SetToolTip(Me.GroupBoxConnections, resources.GetString("GroupBoxConnections.ToolTip2"))
        '
        'btnCreateAndConnectEnergy
        '
        resources.ApplyResources(Me.btnCreateAndConnectEnergy, "btnCreateAndConnectEnergy")
        Me.btnCreateAndConnectEnergy.BackgroundImage = Global.DWSIM.UnitOperations.My.Resources.Resources.bullet_lightning
        Me.btnCreateAndConnectEnergy.Name = "btnCreateAndConnectEnergy"
        Me.ToolTip1.SetToolTip(Me.btnCreateAndConnectEnergy, resources.GetString("btnCreateAndConnectEnergy.ToolTip"))
        Me.ToolTipChangeTag.SetToolTip(Me.btnCreateAndConnectEnergy, resources.GetString("btnCreateAndConnectEnergy.ToolTip1"))
        Me.ToolTipValues.SetToolTip(Me.btnCreateAndConnectEnergy, resources.GetString("btnCreateAndConnectEnergy.ToolTip2"))
        Me.btnCreateAndConnectEnergy.UseVisualStyleBackColor = True
        '
        'btnCreateAndConnectOutlet3
        '
        resources.ApplyResources(Me.btnCreateAndConnectOutlet3, "btnCreateAndConnectOutlet3")
        Me.btnCreateAndConnectOutlet3.BackgroundImage = Global.DWSIM.UnitOperations.My.Resources.Resources.bullet_lightning
        Me.btnCreateAndConnectOutlet3.Name = "btnCreateAndConnectOutlet3"
        Me.ToolTip1.SetToolTip(Me.btnCreateAndConnectOutlet3, resources.GetString("btnCreateAndConnectOutlet3.ToolTip"))
        Me.ToolTipChangeTag.SetToolTip(Me.btnCreateAndConnectOutlet3, resources.GetString("btnCreateAndConnectOutlet3.ToolTip1"))
        Me.ToolTipValues.SetToolTip(Me.btnCreateAndConnectOutlet3, resources.GetString("btnCreateAndConnectOutlet3.ToolTip2"))
        Me.btnCreateAndConnectOutlet3.UseVisualStyleBackColor = True
        '
        'btnCreateAndConnectOutlet2
        '
        resources.ApplyResources(Me.btnCreateAndConnectOutlet2, "btnCreateAndConnectOutlet2")
        Me.btnCreateAndConnectOutlet2.BackgroundImage = Global.DWSIM.UnitOperations.My.Resources.Resources.bullet_lightning
        Me.btnCreateAndConnectOutlet2.Name = "btnCreateAndConnectOutlet2"
        Me.ToolTip1.SetToolTip(Me.btnCreateAndConnectOutlet2, resources.GetString("btnCreateAndConnectOutlet2.ToolTip"))
        Me.ToolTipChangeTag.SetToolTip(Me.btnCreateAndConnectOutlet2, resources.GetString("btnCreateAndConnectOutlet2.ToolTip1"))
        Me.ToolTipValues.SetToolTip(Me.btnCreateAndConnectOutlet2, resources.GetString("btnCreateAndConnectOutlet2.ToolTip2"))
        Me.btnCreateAndConnectOutlet2.UseVisualStyleBackColor = True
        '
        'btnCreateAndConnectOutlet1
        '
        resources.ApplyResources(Me.btnCreateAndConnectOutlet1, "btnCreateAndConnectOutlet1")
        Me.btnCreateAndConnectOutlet1.BackgroundImage = Global.DWSIM.UnitOperations.My.Resources.Resources.bullet_lightning
        Me.btnCreateAndConnectOutlet1.Name = "btnCreateAndConnectOutlet1"
        Me.ToolTip1.SetToolTip(Me.btnCreateAndConnectOutlet1, resources.GetString("btnCreateAndConnectOutlet1.ToolTip"))
        Me.ToolTipChangeTag.SetToolTip(Me.btnCreateAndConnectOutlet1, resources.GetString("btnCreateAndConnectOutlet1.ToolTip1"))
        Me.ToolTipValues.SetToolTip(Me.btnCreateAndConnectOutlet1, resources.GetString("btnCreateAndConnectOutlet1.ToolTip2"))
        Me.btnCreateAndConnectOutlet1.UseVisualStyleBackColor = True
        '
        'btnCreateAndConnectInlet6
        '
        resources.ApplyResources(Me.btnCreateAndConnectInlet6, "btnCreateAndConnectInlet6")
        Me.btnCreateAndConnectInlet6.BackgroundImage = Global.DWSIM.UnitOperations.My.Resources.Resources.bullet_lightning
        Me.btnCreateAndConnectInlet6.Name = "btnCreateAndConnectInlet6"
        Me.ToolTip1.SetToolTip(Me.btnCreateAndConnectInlet6, resources.GetString("btnCreateAndConnectInlet6.ToolTip"))
        Me.ToolTipChangeTag.SetToolTip(Me.btnCreateAndConnectInlet6, resources.GetString("btnCreateAndConnectInlet6.ToolTip1"))
        Me.ToolTipValues.SetToolTip(Me.btnCreateAndConnectInlet6, resources.GetString("btnCreateAndConnectInlet6.ToolTip2"))
        Me.btnCreateAndConnectInlet6.UseVisualStyleBackColor = True
        '
        'btnCreateAndConnectInlet5
        '
        resources.ApplyResources(Me.btnCreateAndConnectInlet5, "btnCreateAndConnectInlet5")
        Me.btnCreateAndConnectInlet5.BackgroundImage = Global.DWSIM.UnitOperations.My.Resources.Resources.bullet_lightning
        Me.btnCreateAndConnectInlet5.Name = "btnCreateAndConnectInlet5"
        Me.ToolTip1.SetToolTip(Me.btnCreateAndConnectInlet5, resources.GetString("btnCreateAndConnectInlet5.ToolTip"))
        Me.ToolTipChangeTag.SetToolTip(Me.btnCreateAndConnectInlet5, resources.GetString("btnCreateAndConnectInlet5.ToolTip1"))
        Me.ToolTipValues.SetToolTip(Me.btnCreateAndConnectInlet5, resources.GetString("btnCreateAndConnectInlet5.ToolTip2"))
        Me.btnCreateAndConnectInlet5.UseVisualStyleBackColor = True
        '
        'btnCreateAndConnectInlet4
        '
        resources.ApplyResources(Me.btnCreateAndConnectInlet4, "btnCreateAndConnectInlet4")
        Me.btnCreateAndConnectInlet4.BackgroundImage = Global.DWSIM.UnitOperations.My.Resources.Resources.bullet_lightning
        Me.btnCreateAndConnectInlet4.Name = "btnCreateAndConnectInlet4"
        Me.ToolTip1.SetToolTip(Me.btnCreateAndConnectInlet4, resources.GetString("btnCreateAndConnectInlet4.ToolTip"))
        Me.ToolTipChangeTag.SetToolTip(Me.btnCreateAndConnectInlet4, resources.GetString("btnCreateAndConnectInlet4.ToolTip1"))
        Me.ToolTipValues.SetToolTip(Me.btnCreateAndConnectInlet4, resources.GetString("btnCreateAndConnectInlet4.ToolTip2"))
        Me.btnCreateAndConnectInlet4.UseVisualStyleBackColor = True
        '
        'btnCreateAndConnectInlet3
        '
        resources.ApplyResources(Me.btnCreateAndConnectInlet3, "btnCreateAndConnectInlet3")
        Me.btnCreateAndConnectInlet3.BackgroundImage = Global.DWSIM.UnitOperations.My.Resources.Resources.bullet_lightning
        Me.btnCreateAndConnectInlet3.Name = "btnCreateAndConnectInlet3"
        Me.ToolTip1.SetToolTip(Me.btnCreateAndConnectInlet3, resources.GetString("btnCreateAndConnectInlet3.ToolTip"))
        Me.ToolTipChangeTag.SetToolTip(Me.btnCreateAndConnectInlet3, resources.GetString("btnCreateAndConnectInlet3.ToolTip1"))
        Me.ToolTipValues.SetToolTip(Me.btnCreateAndConnectInlet3, resources.GetString("btnCreateAndConnectInlet3.ToolTip2"))
        Me.btnCreateAndConnectInlet3.UseVisualStyleBackColor = True
        '
        'btnCreateAndConnectInlet2
        '
        resources.ApplyResources(Me.btnCreateAndConnectInlet2, "btnCreateAndConnectInlet2")
        Me.btnCreateAndConnectInlet2.BackgroundImage = Global.DWSIM.UnitOperations.My.Resources.Resources.bullet_lightning
        Me.btnCreateAndConnectInlet2.Name = "btnCreateAndConnectInlet2"
        Me.ToolTip1.SetToolTip(Me.btnCreateAndConnectInlet2, resources.GetString("btnCreateAndConnectInlet2.ToolTip"))
        Me.ToolTipChangeTag.SetToolTip(Me.btnCreateAndConnectInlet2, resources.GetString("btnCreateAndConnectInlet2.ToolTip1"))
        Me.ToolTipValues.SetToolTip(Me.btnCreateAndConnectInlet2, resources.GetString("btnCreateAndConnectInlet2.ToolTip2"))
        Me.btnCreateAndConnectInlet2.UseVisualStyleBackColor = True
        '
        'btnCreateAndConnectInlet1
        '
        resources.ApplyResources(Me.btnCreateAndConnectInlet1, "btnCreateAndConnectInlet1")
        Me.btnCreateAndConnectInlet1.BackgroundImage = Global.DWSIM.UnitOperations.My.Resources.Resources.bullet_lightning
        Me.btnCreateAndConnectInlet1.Name = "btnCreateAndConnectInlet1"
        Me.ToolTip1.SetToolTip(Me.btnCreateAndConnectInlet1, resources.GetString("btnCreateAndConnectInlet1.ToolTip"))
        Me.ToolTipChangeTag.SetToolTip(Me.btnCreateAndConnectInlet1, resources.GetString("btnCreateAndConnectInlet1.ToolTip1"))
        Me.ToolTipValues.SetToolTip(Me.btnCreateAndConnectInlet1, resources.GetString("btnCreateAndConnectInlet1.ToolTip2"))
        Me.btnCreateAndConnectInlet1.UseVisualStyleBackColor = True
        '
        'btnDisconnectEnergy
        '
        resources.ApplyResources(Me.btnDisconnectEnergy, "btnDisconnectEnergy")
        Me.btnDisconnectEnergy.Image = Global.DWSIM.UnitOperations.My.Resources.Resources.disconnect
        Me.btnDisconnectEnergy.Name = "btnDisconnectEnergy"
        Me.ToolTip1.SetToolTip(Me.btnDisconnectEnergy, resources.GetString("btnDisconnectEnergy.ToolTip"))
        Me.ToolTipChangeTag.SetToolTip(Me.btnDisconnectEnergy, resources.GetString("btnDisconnectEnergy.ToolTip1"))
        Me.ToolTipValues.SetToolTip(Me.btnDisconnectEnergy, resources.GetString("btnDisconnectEnergy.ToolTip2"))
        Me.btnDisconnectEnergy.UseVisualStyleBackColor = True
        '
        'Label16
        '
        resources.ApplyResources(Me.Label16, "Label16")
        Me.Label16.Name = "Label16"
        Me.ToolTip1.SetToolTip(Me.Label16, resources.GetString("Label16.ToolTip"))
        Me.ToolTipChangeTag.SetToolTip(Me.Label16, resources.GetString("Label16.ToolTip1"))
        Me.ToolTipValues.SetToolTip(Me.Label16, resources.GetString("Label16.ToolTip2"))
        '
        'cbEnergy
        '
        resources.ApplyResources(Me.cbEnergy, "cbEnergy")
        Me.cbEnergy.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbEnergy.FormattingEnabled = True
        Me.cbEnergy.Name = "cbEnergy"
        Me.ToolTip1.SetToolTip(Me.cbEnergy, resources.GetString("cbEnergy.ToolTip"))
        Me.ToolTipValues.SetToolTip(Me.cbEnergy, resources.GetString("cbEnergy.ToolTip1"))
        Me.ToolTipChangeTag.SetToolTip(Me.cbEnergy, resources.GetString("cbEnergy.ToolTip2"))
        '
        'btnDisconnectOutlet3
        '
        resources.ApplyResources(Me.btnDisconnectOutlet3, "btnDisconnectOutlet3")
        Me.btnDisconnectOutlet3.Image = Global.DWSIM.UnitOperations.My.Resources.Resources.disconnect
        Me.btnDisconnectOutlet3.Name = "btnDisconnectOutlet3"
        Me.ToolTip1.SetToolTip(Me.btnDisconnectOutlet3, resources.GetString("btnDisconnectOutlet3.ToolTip"))
        Me.ToolTipChangeTag.SetToolTip(Me.btnDisconnectOutlet3, resources.GetString("btnDisconnectOutlet3.ToolTip1"))
        Me.ToolTipValues.SetToolTip(Me.btnDisconnectOutlet3, resources.GetString("btnDisconnectOutlet3.ToolTip2"))
        Me.btnDisconnectOutlet3.UseVisualStyleBackColor = True
        '
        'Label15
        '
        resources.ApplyResources(Me.Label15, "Label15")
        Me.Label15.Name = "Label15"
        Me.ToolTip1.SetToolTip(Me.Label15, resources.GetString("Label15.ToolTip"))
        Me.ToolTipChangeTag.SetToolTip(Me.Label15, resources.GetString("Label15.ToolTip1"))
        Me.ToolTipValues.SetToolTip(Me.Label15, resources.GetString("Label15.ToolTip2"))
        '
        'cbOutlet3
        '
        resources.ApplyResources(Me.cbOutlet3, "cbOutlet3")
        Me.cbOutlet3.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbOutlet3.FormattingEnabled = True
        Me.cbOutlet3.Name = "cbOutlet3"
        Me.ToolTip1.SetToolTip(Me.cbOutlet3, resources.GetString("cbOutlet3.ToolTip"))
        Me.ToolTipValues.SetToolTip(Me.cbOutlet3, resources.GetString("cbOutlet3.ToolTip1"))
        Me.ToolTipChangeTag.SetToolTip(Me.cbOutlet3, resources.GetString("cbOutlet3.ToolTip2"))
        '
        'btnDisconnectOutlet2
        '
        resources.ApplyResources(Me.btnDisconnectOutlet2, "btnDisconnectOutlet2")
        Me.btnDisconnectOutlet2.Image = Global.DWSIM.UnitOperations.My.Resources.Resources.disconnect
        Me.btnDisconnectOutlet2.Name = "btnDisconnectOutlet2"
        Me.ToolTip1.SetToolTip(Me.btnDisconnectOutlet2, resources.GetString("btnDisconnectOutlet2.ToolTip"))
        Me.ToolTipChangeTag.SetToolTip(Me.btnDisconnectOutlet2, resources.GetString("btnDisconnectOutlet2.ToolTip1"))
        Me.ToolTipValues.SetToolTip(Me.btnDisconnectOutlet2, resources.GetString("btnDisconnectOutlet2.ToolTip2"))
        Me.btnDisconnectOutlet2.UseVisualStyleBackColor = True
        '
        'Label14
        '
        resources.ApplyResources(Me.Label14, "Label14")
        Me.Label14.Name = "Label14"
        Me.ToolTip1.SetToolTip(Me.Label14, resources.GetString("Label14.ToolTip"))
        Me.ToolTipChangeTag.SetToolTip(Me.Label14, resources.GetString("Label14.ToolTip1"))
        Me.ToolTipValues.SetToolTip(Me.Label14, resources.GetString("Label14.ToolTip2"))
        '
        'cbOutlet2
        '
        resources.ApplyResources(Me.cbOutlet2, "cbOutlet2")
        Me.cbOutlet2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbOutlet2.FormattingEnabled = True
        Me.cbOutlet2.Name = "cbOutlet2"
        Me.ToolTip1.SetToolTip(Me.cbOutlet2, resources.GetString("cbOutlet2.ToolTip"))
        Me.ToolTipValues.SetToolTip(Me.cbOutlet2, resources.GetString("cbOutlet2.ToolTip1"))
        Me.ToolTipChangeTag.SetToolTip(Me.cbOutlet2, resources.GetString("cbOutlet2.ToolTip2"))
        '
        'btnDisconnectOutlet1
        '
        resources.ApplyResources(Me.btnDisconnectOutlet1, "btnDisconnectOutlet1")
        Me.btnDisconnectOutlet1.Image = Global.DWSIM.UnitOperations.My.Resources.Resources.disconnect
        Me.btnDisconnectOutlet1.Name = "btnDisconnectOutlet1"
        Me.ToolTip1.SetToolTip(Me.btnDisconnectOutlet1, resources.GetString("btnDisconnectOutlet1.ToolTip"))
        Me.ToolTipChangeTag.SetToolTip(Me.btnDisconnectOutlet1, resources.GetString("btnDisconnectOutlet1.ToolTip1"))
        Me.ToolTipValues.SetToolTip(Me.btnDisconnectOutlet1, resources.GetString("btnDisconnectOutlet1.ToolTip2"))
        Me.btnDisconnectOutlet1.UseVisualStyleBackColor = True
        '
        'btnDisconnect6
        '
        resources.ApplyResources(Me.btnDisconnect6, "btnDisconnect6")
        Me.btnDisconnect6.Image = Global.DWSIM.UnitOperations.My.Resources.Resources.disconnect
        Me.btnDisconnect6.Name = "btnDisconnect6"
        Me.ToolTip1.SetToolTip(Me.btnDisconnect6, resources.GetString("btnDisconnect6.ToolTip"))
        Me.ToolTipChangeTag.SetToolTip(Me.btnDisconnect6, resources.GetString("btnDisconnect6.ToolTip1"))
        Me.ToolTipValues.SetToolTip(Me.btnDisconnect6, resources.GetString("btnDisconnect6.ToolTip2"))
        Me.btnDisconnect6.UseVisualStyleBackColor = True
        '
        'btnDisconnect5
        '
        resources.ApplyResources(Me.btnDisconnect5, "btnDisconnect5")
        Me.btnDisconnect5.Image = Global.DWSIM.UnitOperations.My.Resources.Resources.disconnect
        Me.btnDisconnect5.Name = "btnDisconnect5"
        Me.ToolTip1.SetToolTip(Me.btnDisconnect5, resources.GetString("btnDisconnect5.ToolTip"))
        Me.ToolTipChangeTag.SetToolTip(Me.btnDisconnect5, resources.GetString("btnDisconnect5.ToolTip1"))
        Me.ToolTipValues.SetToolTip(Me.btnDisconnect5, resources.GetString("btnDisconnect5.ToolTip2"))
        Me.btnDisconnect5.UseVisualStyleBackColor = True
        '
        'btnDisconnect4
        '
        resources.ApplyResources(Me.btnDisconnect4, "btnDisconnect4")
        Me.btnDisconnect4.Image = Global.DWSIM.UnitOperations.My.Resources.Resources.disconnect
        Me.btnDisconnect4.Name = "btnDisconnect4"
        Me.ToolTip1.SetToolTip(Me.btnDisconnect4, resources.GetString("btnDisconnect4.ToolTip"))
        Me.ToolTipChangeTag.SetToolTip(Me.btnDisconnect4, resources.GetString("btnDisconnect4.ToolTip1"))
        Me.ToolTipValues.SetToolTip(Me.btnDisconnect4, resources.GetString("btnDisconnect4.ToolTip2"))
        Me.btnDisconnect4.UseVisualStyleBackColor = True
        '
        'btnDisconnect3
        '
        resources.ApplyResources(Me.btnDisconnect3, "btnDisconnect3")
        Me.btnDisconnect3.Image = Global.DWSIM.UnitOperations.My.Resources.Resources.disconnect
        Me.btnDisconnect3.Name = "btnDisconnect3"
        Me.ToolTip1.SetToolTip(Me.btnDisconnect3, resources.GetString("btnDisconnect3.ToolTip"))
        Me.ToolTipChangeTag.SetToolTip(Me.btnDisconnect3, resources.GetString("btnDisconnect3.ToolTip1"))
        Me.ToolTipValues.SetToolTip(Me.btnDisconnect3, resources.GetString("btnDisconnect3.ToolTip2"))
        Me.btnDisconnect3.UseVisualStyleBackColor = True
        '
        'btnDisconnect2
        '
        resources.ApplyResources(Me.btnDisconnect2, "btnDisconnect2")
        Me.btnDisconnect2.Image = Global.DWSIM.UnitOperations.My.Resources.Resources.disconnect
        Me.btnDisconnect2.Name = "btnDisconnect2"
        Me.ToolTip1.SetToolTip(Me.btnDisconnect2, resources.GetString("btnDisconnect2.ToolTip"))
        Me.ToolTipChangeTag.SetToolTip(Me.btnDisconnect2, resources.GetString("btnDisconnect2.ToolTip1"))
        Me.ToolTipValues.SetToolTip(Me.btnDisconnect2, resources.GetString("btnDisconnect2.ToolTip2"))
        Me.btnDisconnect2.UseVisualStyleBackColor = True
        '
        'btnDisconnect1
        '
        resources.ApplyResources(Me.btnDisconnect1, "btnDisconnect1")
        Me.btnDisconnect1.Image = Global.DWSIM.UnitOperations.My.Resources.Resources.disconnect
        Me.btnDisconnect1.Name = "btnDisconnect1"
        Me.ToolTip1.SetToolTip(Me.btnDisconnect1, resources.GetString("btnDisconnect1.ToolTip"))
        Me.ToolTipChangeTag.SetToolTip(Me.btnDisconnect1, resources.GetString("btnDisconnect1.ToolTip1"))
        Me.ToolTipValues.SetToolTip(Me.btnDisconnect1, resources.GetString("btnDisconnect1.ToolTip2"))
        Me.btnDisconnect1.UseVisualStyleBackColor = True
        '
        'Label7
        '
        resources.ApplyResources(Me.Label7, "Label7")
        Me.Label7.Name = "Label7"
        Me.ToolTip1.SetToolTip(Me.Label7, resources.GetString("Label7.ToolTip"))
        Me.ToolTipChangeTag.SetToolTip(Me.Label7, resources.GetString("Label7.ToolTip1"))
        Me.ToolTipValues.SetToolTip(Me.Label7, resources.GetString("Label7.ToolTip2"))
        '
        'Label6
        '
        resources.ApplyResources(Me.Label6, "Label6")
        Me.Label6.Name = "Label6"
        Me.ToolTip1.SetToolTip(Me.Label6, resources.GetString("Label6.ToolTip"))
        Me.ToolTipChangeTag.SetToolTip(Me.Label6, resources.GetString("Label6.ToolTip1"))
        Me.ToolTipValues.SetToolTip(Me.Label6, resources.GetString("Label6.ToolTip2"))
        '
        'Label5
        '
        resources.ApplyResources(Me.Label5, "Label5")
        Me.Label5.Name = "Label5"
        Me.ToolTip1.SetToolTip(Me.Label5, resources.GetString("Label5.ToolTip"))
        Me.ToolTipChangeTag.SetToolTip(Me.Label5, resources.GetString("Label5.ToolTip1"))
        Me.ToolTipValues.SetToolTip(Me.Label5, resources.GetString("Label5.ToolTip2"))
        '
        'Label4
        '
        resources.ApplyResources(Me.Label4, "Label4")
        Me.Label4.Name = "Label4"
        Me.ToolTip1.SetToolTip(Me.Label4, resources.GetString("Label4.ToolTip"))
        Me.ToolTipChangeTag.SetToolTip(Me.Label4, resources.GetString("Label4.ToolTip1"))
        Me.ToolTipValues.SetToolTip(Me.Label4, resources.GetString("Label4.ToolTip2"))
        '
        'Label3
        '
        resources.ApplyResources(Me.Label3, "Label3")
        Me.Label3.Name = "Label3"
        Me.ToolTip1.SetToolTip(Me.Label3, resources.GetString("Label3.ToolTip"))
        Me.ToolTipChangeTag.SetToolTip(Me.Label3, resources.GetString("Label3.ToolTip1"))
        Me.ToolTipValues.SetToolTip(Me.Label3, resources.GetString("Label3.ToolTip2"))
        '
        'cbOutlet1
        '
        resources.ApplyResources(Me.cbOutlet1, "cbOutlet1")
        Me.cbOutlet1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbOutlet1.FormattingEnabled = True
        Me.cbOutlet1.Name = "cbOutlet1"
        Me.ToolTip1.SetToolTip(Me.cbOutlet1, resources.GetString("cbOutlet1.ToolTip"))
        Me.ToolTipValues.SetToolTip(Me.cbOutlet1, resources.GetString("cbOutlet1.ToolTip1"))
        Me.ToolTipChangeTag.SetToolTip(Me.cbOutlet1, resources.GetString("cbOutlet1.ToolTip2"))
        '
        'cbInlet6
        '
        resources.ApplyResources(Me.cbInlet6, "cbInlet6")
        Me.cbInlet6.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbInlet6.FormattingEnabled = True
        Me.cbInlet6.Name = "cbInlet6"
        Me.ToolTip1.SetToolTip(Me.cbInlet6, resources.GetString("cbInlet6.ToolTip"))
        Me.ToolTipValues.SetToolTip(Me.cbInlet6, resources.GetString("cbInlet6.ToolTip1"))
        Me.ToolTipChangeTag.SetToolTip(Me.cbInlet6, resources.GetString("cbInlet6.ToolTip2"))
        '
        'cbInlet5
        '
        resources.ApplyResources(Me.cbInlet5, "cbInlet5")
        Me.cbInlet5.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbInlet5.FormattingEnabled = True
        Me.cbInlet5.Name = "cbInlet5"
        Me.ToolTip1.SetToolTip(Me.cbInlet5, resources.GetString("cbInlet5.ToolTip"))
        Me.ToolTipValues.SetToolTip(Me.cbInlet5, resources.GetString("cbInlet5.ToolTip1"))
        Me.ToolTipChangeTag.SetToolTip(Me.cbInlet5, resources.GetString("cbInlet5.ToolTip2"))
        '
        'cbInlet4
        '
        resources.ApplyResources(Me.cbInlet4, "cbInlet4")
        Me.cbInlet4.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbInlet4.FormattingEnabled = True
        Me.cbInlet4.Name = "cbInlet4"
        Me.ToolTip1.SetToolTip(Me.cbInlet4, resources.GetString("cbInlet4.ToolTip"))
        Me.ToolTipValues.SetToolTip(Me.cbInlet4, resources.GetString("cbInlet4.ToolTip1"))
        Me.ToolTipChangeTag.SetToolTip(Me.cbInlet4, resources.GetString("cbInlet4.ToolTip2"))
        '
        'cbInlet3
        '
        resources.ApplyResources(Me.cbInlet3, "cbInlet3")
        Me.cbInlet3.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbInlet3.FormattingEnabled = True
        Me.cbInlet3.Name = "cbInlet3"
        Me.ToolTip1.SetToolTip(Me.cbInlet3, resources.GetString("cbInlet3.ToolTip"))
        Me.ToolTipValues.SetToolTip(Me.cbInlet3, resources.GetString("cbInlet3.ToolTip1"))
        Me.ToolTipChangeTag.SetToolTip(Me.cbInlet3, resources.GetString("cbInlet3.ToolTip2"))
        '
        'cbInlet2
        '
        resources.ApplyResources(Me.cbInlet2, "cbInlet2")
        Me.cbInlet2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbInlet2.FormattingEnabled = True
        Me.cbInlet2.Name = "cbInlet2"
        Me.ToolTip1.SetToolTip(Me.cbInlet2, resources.GetString("cbInlet2.ToolTip"))
        Me.ToolTipValues.SetToolTip(Me.cbInlet2, resources.GetString("cbInlet2.ToolTip1"))
        Me.ToolTipChangeTag.SetToolTip(Me.cbInlet2, resources.GetString("cbInlet2.ToolTip2"))
        '
        'Label2
        '
        resources.ApplyResources(Me.Label2, "Label2")
        Me.Label2.Name = "Label2"
        Me.ToolTip1.SetToolTip(Me.Label2, resources.GetString("Label2.ToolTip"))
        Me.ToolTipChangeTag.SetToolTip(Me.Label2, resources.GetString("Label2.ToolTip1"))
        Me.ToolTipValues.SetToolTip(Me.Label2, resources.GetString("Label2.ToolTip2"))
        '
        'cbInlet1
        '
        resources.ApplyResources(Me.cbInlet1, "cbInlet1")
        Me.cbInlet1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbInlet1.FormattingEnabled = True
        Me.cbInlet1.Name = "cbInlet1"
        Me.ToolTip1.SetToolTip(Me.cbInlet1, resources.GetString("cbInlet1.ToolTip"))
        Me.ToolTipValues.SetToolTip(Me.cbInlet1, resources.GetString("cbInlet1.ToolTip1"))
        Me.ToolTipChangeTag.SetToolTip(Me.cbInlet1, resources.GetString("cbInlet1.ToolTip2"))
        '
        'Label1
        '
        resources.ApplyResources(Me.Label1, "Label1")
        Me.Label1.Name = "Label1"
        Me.ToolTip1.SetToolTip(Me.Label1, resources.GetString("Label1.ToolTip"))
        Me.ToolTipChangeTag.SetToolTip(Me.Label1, resources.GetString("Label1.ToolTip1"))
        Me.ToolTipValues.SetToolTip(Me.Label1, resources.GetString("Label1.ToolTip2"))
        '
        'GroupBoxParameters
        '
        resources.ApplyResources(Me.GroupBoxParameters, "GroupBoxParameters")
        Me.GroupBoxParameters.Controls.Add(Me.TabControl1)
        Me.GroupBoxParameters.Name = "GroupBoxParameters"
        Me.GroupBoxParameters.TabStop = False
        Me.ToolTipChangeTag.SetToolTip(Me.GroupBoxParameters, resources.GetString("GroupBoxParameters.ToolTip"))
        Me.ToolTipValues.SetToolTip(Me.GroupBoxParameters, resources.GetString("GroupBoxParameters.ToolTip1"))
        Me.ToolTip1.SetToolTip(Me.GroupBoxParameters, resources.GetString("GroupBoxParameters.ToolTip2"))
        '
        'TabControl1
        '
        resources.ApplyResources(Me.TabControl1, "TabControl1")
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Controls.Add(Me.TabPage3)
        Me.TabControl1.Controls.Add(Me.TabPage2)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.ToolTipChangeTag.SetToolTip(Me.TabControl1, resources.GetString("TabControl1.ToolTip"))
        Me.ToolTip1.SetToolTip(Me.TabControl1, resources.GetString("TabControl1.ToolTip1"))
        Me.ToolTipValues.SetToolTip(Me.TabControl1, resources.GetString("TabControl1.ToolTip2"))
        '
        'TabPage1
        '
        resources.ApplyResources(Me.TabPage1, "TabPage1")
        Me.TabPage1.Controls.Add(Me.Label8)
        Me.TabPage1.Controls.Add(Me.cbCalcMode)
        Me.TabPage1.Controls.Add(Me.tbTemperature)
        Me.TabPage1.Controls.Add(Me.Label10)
        Me.TabPage1.Controls.Add(Me.cbTemp)
        Me.TabPage1.Controls.Add(Me.cbPressureCalcMode)
        Me.TabPage1.Controls.Add(Me.chkOverrideT)
        Me.TabPage1.Controls.Add(Me.chkOverrideP)
        Me.TabPage1.Controls.Add(Me.cbPress)
        Me.TabPage1.Controls.Add(Me.tbPressure)
        Me.TabPage1.Name = "TabPage1"
        Me.ToolTip1.SetToolTip(Me.TabPage1, resources.GetString("TabPage1.ToolTip"))
        Me.ToolTipValues.SetToolTip(Me.TabPage1, resources.GetString("TabPage1.ToolTip1"))
        Me.ToolTipChangeTag.SetToolTip(Me.TabPage1, resources.GetString("TabPage1.ToolTip2"))
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'Label8
        '
        resources.ApplyResources(Me.Label8, "Label8")
        Me.Label8.Name = "Label8"
        Me.ToolTip1.SetToolTip(Me.Label8, resources.GetString("Label8.ToolTip"))
        Me.ToolTipChangeTag.SetToolTip(Me.Label8, resources.GetString("Label8.ToolTip1"))
        Me.ToolTipValues.SetToolTip(Me.Label8, resources.GetString("Label8.ToolTip2"))
        '
        'cbCalcMode
        '
        resources.ApplyResources(Me.cbCalcMode, "cbCalcMode")
        Me.cbCalcMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbCalcMode.FormattingEnabled = True
        Me.cbCalcMode.Items.AddRange(New Object() {resources.GetString("cbCalcMode.Items"), resources.GetString("cbCalcMode.Items1"), resources.GetString("cbCalcMode.Items2"), resources.GetString("cbCalcMode.Items3")})
        Me.cbCalcMode.Name = "cbCalcMode"
        Me.ToolTip1.SetToolTip(Me.cbCalcMode, resources.GetString("cbCalcMode.ToolTip"))
        Me.ToolTipValues.SetToolTip(Me.cbCalcMode, resources.GetString("cbCalcMode.ToolTip1"))
        Me.ToolTipChangeTag.SetToolTip(Me.cbCalcMode, resources.GetString("cbCalcMode.ToolTip2"))
        '
        'tbTemperature
        '
        resources.ApplyResources(Me.tbTemperature, "tbTemperature")
        Me.tbTemperature.Name = "tbTemperature"
        Me.ToolTipValues.SetToolTip(Me.tbTemperature, resources.GetString("tbTemperature.ToolTip"))
        Me.ToolTip1.SetToolTip(Me.tbTemperature, resources.GetString("tbTemperature.ToolTip1"))
        Me.ToolTipChangeTag.SetToolTip(Me.tbTemperature, resources.GetString("tbTemperature.ToolTip2"))
        '
        'Label10
        '
        resources.ApplyResources(Me.Label10, "Label10")
        Me.Label10.Name = "Label10"
        Me.ToolTip1.SetToolTip(Me.Label10, resources.GetString("Label10.ToolTip"))
        Me.ToolTipChangeTag.SetToolTip(Me.Label10, resources.GetString("Label10.ToolTip1"))
        Me.ToolTipValues.SetToolTip(Me.Label10, resources.GetString("Label10.ToolTip2"))
        '
        'cbTemp
        '
        resources.ApplyResources(Me.cbTemp, "cbTemp")
        Me.cbTemp.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbTemp.FormattingEnabled = True
        Me.cbTemp.Items.AddRange(New Object() {resources.GetString("cbTemp.Items"), resources.GetString("cbTemp.Items1"), resources.GetString("cbTemp.Items2")})
        Me.cbTemp.Name = "cbTemp"
        Me.ToolTip1.SetToolTip(Me.cbTemp, resources.GetString("cbTemp.ToolTip"))
        Me.ToolTipValues.SetToolTip(Me.cbTemp, resources.GetString("cbTemp.ToolTip1"))
        Me.ToolTipChangeTag.SetToolTip(Me.cbTemp, resources.GetString("cbTemp.ToolTip2"))
        '
        'cbPressureCalcMode
        '
        resources.ApplyResources(Me.cbPressureCalcMode, "cbPressureCalcMode")
        Me.cbPressureCalcMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbPressureCalcMode.FormattingEnabled = True
        Me.cbPressureCalcMode.Items.AddRange(New Object() {resources.GetString("cbPressureCalcMode.Items"), resources.GetString("cbPressureCalcMode.Items1"), resources.GetString("cbPressureCalcMode.Items2")})
        Me.cbPressureCalcMode.Name = "cbPressureCalcMode"
        Me.ToolTip1.SetToolTip(Me.cbPressureCalcMode, resources.GetString("cbPressureCalcMode.ToolTip"))
        Me.ToolTipValues.SetToolTip(Me.cbPressureCalcMode, resources.GetString("cbPressureCalcMode.ToolTip1"))
        Me.ToolTipChangeTag.SetToolTip(Me.cbPressureCalcMode, resources.GetString("cbPressureCalcMode.ToolTip2"))
        '
        'chkOverrideT
        '
        resources.ApplyResources(Me.chkOverrideT, "chkOverrideT")
        Me.chkOverrideT.Name = "chkOverrideT"
        Me.ToolTip1.SetToolTip(Me.chkOverrideT, resources.GetString("chkOverrideT.ToolTip"))
        Me.ToolTipChangeTag.SetToolTip(Me.chkOverrideT, resources.GetString("chkOverrideT.ToolTip1"))
        Me.ToolTipValues.SetToolTip(Me.chkOverrideT, resources.GetString("chkOverrideT.ToolTip2"))
        Me.chkOverrideT.UseVisualStyleBackColor = True
        '
        'chkOverrideP
        '
        resources.ApplyResources(Me.chkOverrideP, "chkOverrideP")
        Me.chkOverrideP.Name = "chkOverrideP"
        Me.ToolTip1.SetToolTip(Me.chkOverrideP, resources.GetString("chkOverrideP.ToolTip"))
        Me.ToolTipChangeTag.SetToolTip(Me.chkOverrideP, resources.GetString("chkOverrideP.ToolTip1"))
        Me.ToolTipValues.SetToolTip(Me.chkOverrideP, resources.GetString("chkOverrideP.ToolTip2"))
        Me.chkOverrideP.UseVisualStyleBackColor = True
        '
        'cbPress
        '
        resources.ApplyResources(Me.cbPress, "cbPress")
        Me.cbPress.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbPress.FormattingEnabled = True
        Me.cbPress.Items.AddRange(New Object() {resources.GetString("cbPress.Items"), resources.GetString("cbPress.Items1"), resources.GetString("cbPress.Items2")})
        Me.cbPress.Name = "cbPress"
        Me.ToolTip1.SetToolTip(Me.cbPress, resources.GetString("cbPress.ToolTip"))
        Me.ToolTipValues.SetToolTip(Me.cbPress, resources.GetString("cbPress.ToolTip1"))
        Me.ToolTipChangeTag.SetToolTip(Me.cbPress, resources.GetString("cbPress.ToolTip2"))
        '
        'tbPressure
        '
        resources.ApplyResources(Me.tbPressure, "tbPressure")
        Me.tbPressure.Name = "tbPressure"
        Me.ToolTipValues.SetToolTip(Me.tbPressure, resources.GetString("tbPressure.ToolTip"))
        Me.ToolTip1.SetToolTip(Me.tbPressure, resources.GetString("tbPressure.ToolTip1"))
        Me.ToolTipChangeTag.SetToolTip(Me.tbPressure, resources.GetString("tbPressure.ToolTip2"))
        '
        'TabPage3
        '
        resources.ApplyResources(Me.TabPage3, "TabPage3")
        Me.TabPage3.Controls.Add(Me.Button1)
        Me.TabPage3.Controls.Add(Me.cbHeadType)
        Me.TabPage3.Controls.Add(Me.Label24)
        Me.TabPage3.Controls.Add(Me.rbHorizontal)
        Me.TabPage3.Controls.Add(Me.rbVertical)
        Me.TabPage3.Controls.Add(Me.Label23)
        Me.TabPage3.Controls.Add(Me.lbHeight)
        Me.TabPage3.Controls.Add(Me.Label22)
        Me.TabPage3.Controls.Add(Me.tbHeight)
        Me.TabPage3.Controls.Add(Me.lbDiam)
        Me.TabPage3.Controls.Add(Me.Label20)
        Me.TabPage3.Controls.Add(Me.tbDiam)
        Me.TabPage3.Controls.Add(Me.lblThickness)
        Me.TabPage3.Controls.Add(Me.Label18)
        Me.TabPage3.Controls.Add(Me.cbWallMaterial)
        Me.TabPage3.Controls.Add(Me.tbThickness)
        Me.TabPage3.Controls.Add(Me.Label17)
        Me.TabPage3.Name = "TabPage3"
        Me.ToolTip1.SetToolTip(Me.TabPage3, resources.GetString("TabPage3.ToolTip"))
        Me.ToolTipValues.SetToolTip(Me.TabPage3, resources.GetString("TabPage3.ToolTip1"))
        Me.ToolTipChangeTag.SetToolTip(Me.TabPage3, resources.GetString("TabPage3.ToolTip2"))
        Me.TabPage3.UseVisualStyleBackColor = True
        '
        'cbHeadType
        '
        resources.ApplyResources(Me.cbHeadType, "cbHeadType")
        Me.cbHeadType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbHeadType.FormattingEnabled = True
        Me.cbHeadType.Items.AddRange(New Object() {resources.GetString("cbHeadType.Items"), resources.GetString("cbHeadType.Items1"), resources.GetString("cbHeadType.Items2"), resources.GetString("cbHeadType.Items3"), resources.GetString("cbHeadType.Items4"), resources.GetString("cbHeadType.Items5")})
        Me.cbHeadType.Name = "cbHeadType"
        Me.ToolTip1.SetToolTip(Me.cbHeadType, resources.GetString("cbHeadType.ToolTip"))
        Me.ToolTipValues.SetToolTip(Me.cbHeadType, resources.GetString("cbHeadType.ToolTip1"))
        Me.ToolTipChangeTag.SetToolTip(Me.cbHeadType, resources.GetString("cbHeadType.ToolTip2"))
        '
        'Label24
        '
        resources.ApplyResources(Me.Label24, "Label24")
        Me.Label24.Name = "Label24"
        Me.ToolTip1.SetToolTip(Me.Label24, resources.GetString("Label24.ToolTip"))
        Me.ToolTipChangeTag.SetToolTip(Me.Label24, resources.GetString("Label24.ToolTip1"))
        Me.ToolTipValues.SetToolTip(Me.Label24, resources.GetString("Label24.ToolTip2"))
        '
        'rbHorizontal
        '
        resources.ApplyResources(Me.rbHorizontal, "rbHorizontal")
        Me.rbHorizontal.Name = "rbHorizontal"
        Me.rbHorizontal.TabStop = True
        Me.ToolTip1.SetToolTip(Me.rbHorizontal, resources.GetString("rbHorizontal.ToolTip"))
        Me.ToolTipChangeTag.SetToolTip(Me.rbHorizontal, resources.GetString("rbHorizontal.ToolTip1"))
        Me.ToolTipValues.SetToolTip(Me.rbHorizontal, resources.GetString("rbHorizontal.ToolTip2"))
        Me.rbHorizontal.UseVisualStyleBackColor = True
        '
        'rbVertical
        '
        resources.ApplyResources(Me.rbVertical, "rbVertical")
        Me.rbVertical.Name = "rbVertical"
        Me.rbVertical.TabStop = True
        Me.ToolTip1.SetToolTip(Me.rbVertical, resources.GetString("rbVertical.ToolTip"))
        Me.ToolTipChangeTag.SetToolTip(Me.rbVertical, resources.GetString("rbVertical.ToolTip1"))
        Me.ToolTipValues.SetToolTip(Me.rbVertical, resources.GetString("rbVertical.ToolTip2"))
        Me.rbVertical.UseVisualStyleBackColor = True
        '
        'Label23
        '
        resources.ApplyResources(Me.Label23, "Label23")
        Me.Label23.Name = "Label23"
        Me.ToolTip1.SetToolTip(Me.Label23, resources.GetString("Label23.ToolTip"))
        Me.ToolTipChangeTag.SetToolTip(Me.Label23, resources.GetString("Label23.ToolTip1"))
        Me.ToolTipValues.SetToolTip(Me.Label23, resources.GetString("Label23.ToolTip2"))
        '
        'lbHeight
        '
        resources.ApplyResources(Me.lbHeight, "lbHeight")
        Me.lbHeight.Name = "lbHeight"
        Me.ToolTip1.SetToolTip(Me.lbHeight, resources.GetString("lbHeight.ToolTip"))
        Me.ToolTipChangeTag.SetToolTip(Me.lbHeight, resources.GetString("lbHeight.ToolTip1"))
        Me.ToolTipValues.SetToolTip(Me.lbHeight, resources.GetString("lbHeight.ToolTip2"))
        '
        'Label22
        '
        resources.ApplyResources(Me.Label22, "Label22")
        Me.Label22.Name = "Label22"
        Me.ToolTip1.SetToolTip(Me.Label22, resources.GetString("Label22.ToolTip"))
        Me.ToolTipChangeTag.SetToolTip(Me.Label22, resources.GetString("Label22.ToolTip1"))
        Me.ToolTipValues.SetToolTip(Me.Label22, resources.GetString("Label22.ToolTip2"))
        '
        'tbHeight
        '
        resources.ApplyResources(Me.tbHeight, "tbHeight")
        Me.tbHeight.Name = "tbHeight"
        Me.ToolTipValues.SetToolTip(Me.tbHeight, resources.GetString("tbHeight.ToolTip"))
        Me.ToolTip1.SetToolTip(Me.tbHeight, resources.GetString("tbHeight.ToolTip1"))
        Me.ToolTipChangeTag.SetToolTip(Me.tbHeight, resources.GetString("tbHeight.ToolTip2"))
        '
        'lbDiam
        '
        resources.ApplyResources(Me.lbDiam, "lbDiam")
        Me.lbDiam.Name = "lbDiam"
        Me.ToolTip1.SetToolTip(Me.lbDiam, resources.GetString("lbDiam.ToolTip"))
        Me.ToolTipChangeTag.SetToolTip(Me.lbDiam, resources.GetString("lbDiam.ToolTip1"))
        Me.ToolTipValues.SetToolTip(Me.lbDiam, resources.GetString("lbDiam.ToolTip2"))
        '
        'Label20
        '
        resources.ApplyResources(Me.Label20, "Label20")
        Me.Label20.Name = "Label20"
        Me.ToolTip1.SetToolTip(Me.Label20, resources.GetString("Label20.ToolTip"))
        Me.ToolTipChangeTag.SetToolTip(Me.Label20, resources.GetString("Label20.ToolTip1"))
        Me.ToolTipValues.SetToolTip(Me.Label20, resources.GetString("Label20.ToolTip2"))
        '
        'tbDiam
        '
        resources.ApplyResources(Me.tbDiam, "tbDiam")
        Me.tbDiam.Name = "tbDiam"
        Me.ToolTipValues.SetToolTip(Me.tbDiam, resources.GetString("tbDiam.ToolTip"))
        Me.ToolTip1.SetToolTip(Me.tbDiam, resources.GetString("tbDiam.ToolTip1"))
        Me.ToolTipChangeTag.SetToolTip(Me.tbDiam, resources.GetString("tbDiam.ToolTip2"))
        '
        'lblThickness
        '
        resources.ApplyResources(Me.lblThickness, "lblThickness")
        Me.lblThickness.Name = "lblThickness"
        Me.ToolTip1.SetToolTip(Me.lblThickness, resources.GetString("lblThickness.ToolTip"))
        Me.ToolTipChangeTag.SetToolTip(Me.lblThickness, resources.GetString("lblThickness.ToolTip1"))
        Me.ToolTipValues.SetToolTip(Me.lblThickness, resources.GetString("lblThickness.ToolTip2"))
        '
        'Label18
        '
        resources.ApplyResources(Me.Label18, "Label18")
        Me.Label18.Name = "Label18"
        Me.ToolTip1.SetToolTip(Me.Label18, resources.GetString("Label18.ToolTip"))
        Me.ToolTipChangeTag.SetToolTip(Me.Label18, resources.GetString("Label18.ToolTip1"))
        Me.ToolTipValues.SetToolTip(Me.Label18, resources.GetString("Label18.ToolTip2"))
        '
        'cbWallMaterial
        '
        resources.ApplyResources(Me.cbWallMaterial, "cbWallMaterial")
        Me.cbWallMaterial.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbWallMaterial.FormattingEnabled = True
        Me.cbWallMaterial.Name = "cbWallMaterial"
        Me.ToolTip1.SetToolTip(Me.cbWallMaterial, resources.GetString("cbWallMaterial.ToolTip"))
        Me.ToolTipValues.SetToolTip(Me.cbWallMaterial, resources.GetString("cbWallMaterial.ToolTip1"))
        Me.ToolTipChangeTag.SetToolTip(Me.cbWallMaterial, resources.GetString("cbWallMaterial.ToolTip2"))
        '
        'tbThickness
        '
        resources.ApplyResources(Me.tbThickness, "tbThickness")
        Me.tbThickness.Name = "tbThickness"
        Me.ToolTipValues.SetToolTip(Me.tbThickness, resources.GetString("tbThickness.ToolTip"))
        Me.ToolTip1.SetToolTip(Me.tbThickness, resources.GetString("tbThickness.ToolTip1"))
        Me.ToolTipChangeTag.SetToolTip(Me.tbThickness, resources.GetString("tbThickness.ToolTip2"))
        '
        'Label17
        '
        resources.ApplyResources(Me.Label17, "Label17")
        Me.Label17.Name = "Label17"
        Me.ToolTip1.SetToolTip(Me.Label17, resources.GetString("Label17.ToolTip"))
        Me.ToolTipChangeTag.SetToolTip(Me.Label17, resources.GetString("Label17.ToolTip1"))
        Me.ToolTipValues.SetToolTip(Me.Label17, resources.GetString("Label17.ToolTip2"))
        '
        'TabPage2
        '
        resources.ApplyResources(Me.TabPage2, "TabPage2")
        Me.TabPage2.Name = "TabPage2"
        Me.ToolTip1.SetToolTip(Me.TabPage2, resources.GetString("TabPage2.ToolTip"))
        Me.ToolTipValues.SetToolTip(Me.TabPage2, resources.GetString("TabPage2.ToolTip1"))
        Me.ToolTipChangeTag.SetToolTip(Me.TabPage2, resources.GetString("TabPage2.ToolTip2"))
        Me.TabPage2.UseVisualStyleBackColor = True
        '
        'GroupBox5
        '
        resources.ApplyResources(Me.GroupBox5, "GroupBox5")
        Me.GroupBox5.Controls.Add(Me.btnUtils)
        Me.GroupBox5.Controls.Add(Me.lblTag)
        Me.GroupBox5.Controls.Add(Me.chkActive)
        Me.GroupBox5.Controls.Add(Me.lblConnectedTo)
        Me.GroupBox5.Controls.Add(Me.lblStatus)
        Me.GroupBox5.Controls.Add(Me.Label13)
        Me.GroupBox5.Controls.Add(Me.Label12)
        Me.GroupBox5.Controls.Add(Me.Label11)
        Me.GroupBox5.Name = "GroupBox5"
        Me.GroupBox5.TabStop = False
        Me.ToolTipChangeTag.SetToolTip(Me.GroupBox5, resources.GetString("GroupBox5.ToolTip"))
        Me.ToolTipValues.SetToolTip(Me.GroupBox5, resources.GetString("GroupBox5.ToolTip1"))
        Me.ToolTip1.SetToolTip(Me.GroupBox5, resources.GetString("GroupBox5.ToolTip2"))
        '
        'btnUtils
        '
        resources.ApplyResources(Me.btnUtils, "btnUtils")
        Me.btnUtils.Image = Global.DWSIM.UnitOperations.My.Resources.Resources.bullet_sparkle
        Me.btnUtils.Name = "btnUtils"
        Me.ToolTip1.SetToolTip(Me.btnUtils, resources.GetString("btnUtils.ToolTip"))
        Me.ToolTipChangeTag.SetToolTip(Me.btnUtils, resources.GetString("btnUtils.ToolTip1"))
        Me.ToolTipValues.SetToolTip(Me.btnUtils, resources.GetString("btnUtils.ToolTip2"))
        Me.btnUtils.UseVisualStyleBackColor = True
        '
        'lblTag
        '
        resources.ApplyResources(Me.lblTag, "lblTag")
        Me.lblTag.Name = "lblTag"
        Me.ToolTipValues.SetToolTip(Me.lblTag, resources.GetString("lblTag.ToolTip"))
        Me.ToolTip1.SetToolTip(Me.lblTag, resources.GetString("lblTag.ToolTip1"))
        Me.ToolTipChangeTag.SetToolTip(Me.lblTag, resources.GetString("lblTag.ToolTip2"))
        '
        'chkActive
        '
        resources.ApplyResources(Me.chkActive, "chkActive")
        Me.chkActive.Image = Global.DWSIM.UnitOperations.My.Resources.Resources.bullet_tick
        Me.chkActive.Name = "chkActive"
        Me.ToolTip1.SetToolTip(Me.chkActive, resources.GetString("chkActive.ToolTip"))
        Me.ToolTipChangeTag.SetToolTip(Me.chkActive, resources.GetString("chkActive.ToolTip1"))
        Me.ToolTipValues.SetToolTip(Me.chkActive, resources.GetString("chkActive.ToolTip2"))
        Me.chkActive.UseVisualStyleBackColor = True
        '
        'lblConnectedTo
        '
        resources.ApplyResources(Me.lblConnectedTo, "lblConnectedTo")
        Me.lblConnectedTo.Name = "lblConnectedTo"
        Me.ToolTip1.SetToolTip(Me.lblConnectedTo, resources.GetString("lblConnectedTo.ToolTip"))
        Me.ToolTipChangeTag.SetToolTip(Me.lblConnectedTo, resources.GetString("lblConnectedTo.ToolTip1"))
        Me.ToolTipValues.SetToolTip(Me.lblConnectedTo, resources.GetString("lblConnectedTo.ToolTip2"))
        '
        'lblStatus
        '
        resources.ApplyResources(Me.lblStatus, "lblStatus")
        Me.lblStatus.Name = "lblStatus"
        Me.ToolTip1.SetToolTip(Me.lblStatus, resources.GetString("lblStatus.ToolTip"))
        Me.ToolTipChangeTag.SetToolTip(Me.lblStatus, resources.GetString("lblStatus.ToolTip1"))
        Me.ToolTipValues.SetToolTip(Me.lblStatus, resources.GetString("lblStatus.ToolTip2"))
        '
        'Label13
        '
        resources.ApplyResources(Me.Label13, "Label13")
        Me.Label13.Name = "Label13"
        Me.ToolTip1.SetToolTip(Me.Label13, resources.GetString("Label13.ToolTip"))
        Me.ToolTipChangeTag.SetToolTip(Me.Label13, resources.GetString("Label13.ToolTip1"))
        Me.ToolTipValues.SetToolTip(Me.Label13, resources.GetString("Label13.ToolTip2"))
        '
        'Label12
        '
        resources.ApplyResources(Me.Label12, "Label12")
        Me.Label12.Name = "Label12"
        Me.ToolTip1.SetToolTip(Me.Label12, resources.GetString("Label12.ToolTip"))
        Me.ToolTipChangeTag.SetToolTip(Me.Label12, resources.GetString("Label12.ToolTip1"))
        Me.ToolTipValues.SetToolTip(Me.Label12, resources.GetString("Label12.ToolTip2"))
        '
        'Label11
        '
        resources.ApplyResources(Me.Label11, "Label11")
        Me.Label11.Name = "Label11"
        Me.ToolTip1.SetToolTip(Me.Label11, resources.GetString("Label11.ToolTip"))
        Me.ToolTipChangeTag.SetToolTip(Me.Label11, resources.GetString("Label11.ToolTip1"))
        Me.ToolTipValues.SetToolTip(Me.Label11, resources.GetString("Label11.ToolTip2"))
        '
        'GroupBox4
        '
        resources.ApplyResources(Me.GroupBox4, "GroupBox4")
        Me.GroupBox4.Controls.Add(Me.rtbAnnotations)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.TabStop = False
        Me.ToolTipChangeTag.SetToolTip(Me.GroupBox4, resources.GetString("GroupBox4.ToolTip"))
        Me.ToolTipValues.SetToolTip(Me.GroupBox4, resources.GetString("GroupBox4.ToolTip1"))
        Me.ToolTip1.SetToolTip(Me.GroupBox4, resources.GetString("GroupBox4.ToolTip2"))
        '
        'rtbAnnotations
        '
        resources.ApplyResources(Me.rtbAnnotations, "rtbAnnotations")
        Me.rtbAnnotations.Name = "rtbAnnotations"
        Me.rtbAnnotations.Rtf = "{\rtf1\ansi\ansicpg1252\deff0\nouicompat\deflang1046{\fonttbl{\f0\fnil Microsoft " &
    "Sans Serif;}}" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "{\*\generator Riched20 10.0.26100}\viewkind4\uc1 " & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "\pard\f0\fs17\" &
    "par" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "}" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10)
        Me.rtbAnnotations.ShowRedo = False
        Me.rtbAnnotations.ShowUndo = False
        Me.ToolTipValues.SetToolTip(Me.rtbAnnotations, resources.GetString("rtbAnnotations.ToolTip"))
        Me.ToolTip1.SetToolTip(Me.rtbAnnotations, resources.GetString("rtbAnnotations.ToolTip1"))
        Me.ToolTipChangeTag.SetToolTip(Me.rtbAnnotations, resources.GetString("rtbAnnotations.ToolTip2"))
        '
        'GroupBox3
        '
        resources.ApplyResources(Me.GroupBox3, "GroupBox3")
        Me.GroupBox3.Controls.Add(Me.btnConfigurePP)
        Me.GroupBox3.Controls.Add(Me.cbPropPack)
        Me.GroupBox3.Controls.Add(Me.Label9)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.TabStop = False
        Me.ToolTipChangeTag.SetToolTip(Me.GroupBox3, resources.GetString("GroupBox3.ToolTip"))
        Me.ToolTipValues.SetToolTip(Me.GroupBox3, resources.GetString("GroupBox3.ToolTip1"))
        Me.ToolTip1.SetToolTip(Me.GroupBox3, resources.GetString("GroupBox3.ToolTip2"))
        '
        'btnConfigurePP
        '
        resources.ApplyResources(Me.btnConfigurePP, "btnConfigurePP")
        Me.btnConfigurePP.BackgroundImage = Global.DWSIM.UnitOperations.My.Resources.Resources.cog
        Me.btnConfigurePP.Name = "btnConfigurePP"
        Me.ToolTip1.SetToolTip(Me.btnConfigurePP, resources.GetString("btnConfigurePP.ToolTip"))
        Me.ToolTipChangeTag.SetToolTip(Me.btnConfigurePP, resources.GetString("btnConfigurePP.ToolTip1"))
        Me.ToolTipValues.SetToolTip(Me.btnConfigurePP, resources.GetString("btnConfigurePP.ToolTip2"))
        Me.btnConfigurePP.UseVisualStyleBackColor = True
        '
        'cbPropPack
        '
        resources.ApplyResources(Me.cbPropPack, "cbPropPack")
        Me.cbPropPack.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbPropPack.FormattingEnabled = True
        Me.cbPropPack.Name = "cbPropPack"
        Me.ToolTip1.SetToolTip(Me.cbPropPack, resources.GetString("cbPropPack.ToolTip"))
        Me.ToolTipValues.SetToolTip(Me.cbPropPack, resources.GetString("cbPropPack.ToolTip1"))
        Me.ToolTipChangeTag.SetToolTip(Me.cbPropPack, resources.GetString("cbPropPack.ToolTip2"))
        '
        'Label9
        '
        resources.ApplyResources(Me.Label9, "Label9")
        Me.Label9.Name = "Label9"
        Me.ToolTip1.SetToolTip(Me.Label9, resources.GetString("Label9.ToolTip"))
        Me.ToolTipChangeTag.SetToolTip(Me.Label9, resources.GetString("Label9.ToolTip1"))
        Me.ToolTipValues.SetToolTip(Me.Label9, resources.GetString("Label9.ToolTip2"))
        '
        'UtilitiesCtxMenu
        '
        resources.ApplyResources(Me.UtilitiesCtxMenu, "UtilitiesCtxMenu")
        Me.UtilitiesCtxMenu.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.AddUtilityTSMI})
        Me.UtilitiesCtxMenu.Name = "ContextMenuStrip1"
        Me.ToolTip1.SetToolTip(Me.UtilitiesCtxMenu, resources.GetString("UtilitiesCtxMenu.ToolTip"))
        Me.ToolTipChangeTag.SetToolTip(Me.UtilitiesCtxMenu, resources.GetString("UtilitiesCtxMenu.ToolTip1"))
        Me.ToolTipValues.SetToolTip(Me.UtilitiesCtxMenu, resources.GetString("UtilitiesCtxMenu.ToolTip2"))
        '
        'AddUtilityTSMI
        '
        resources.ApplyResources(Me.AddUtilityTSMI, "AddUtilityTSMI")
        Me.AddUtilityTSMI.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.sizingtsmi})
        Me.AddUtilityTSMI.Image = Global.DWSIM.UnitOperations.My.Resources.Resources.add
        Me.AddUtilityTSMI.Name = "AddUtilityTSMI"
        '
        'sizingtsmi
        '
        resources.ApplyResources(Me.sizingtsmi, "sizingtsmi")
        Me.sizingtsmi.Image = Global.DWSIM.UnitOperations.My.Resources.Resources.cog
        Me.sizingtsmi.Name = "sizingtsmi"
        '
        'ToolTipChangeTag
        '
        Me.ToolTipChangeTag.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info
        Me.ToolTipChangeTag.ToolTipTitle = "Info"
        '
        'Button1
        '
        resources.ApplyResources(Me.Button1, "Button1")
        Me.Button1.Name = "Button1"
        Me.ToolTip1.SetToolTip(Me.Button1, resources.GetString("Button1.ToolTip"))
        Me.ToolTipChangeTag.SetToolTip(Me.Button1, resources.GetString("Button1.ToolTip1"))
        Me.ToolTipValues.SetToolTip(Me.Button1, resources.GetString("Button1.ToolTip2"))
        Me.Button1.UseVisualStyleBackColor = True
        '
        'EditingForm_Vessel
        '
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.Controls.Add(Me.GroupBox4)
        Me.Controls.Add(Me.GroupBox5)
        Me.Controls.Add(Me.GroupBox3)
        Me.Controls.Add(Me.GroupBoxParameters)
        Me.Controls.Add(Me.GroupBoxConnections)
        Me.Name = "EditingForm_Vessel"
        Me.ShowHint = WeifenLuo.WinFormsUI.Docking.DockState.DockLeft
        Me.ToolTipValues.SetToolTip(Me, resources.GetString("$this.ToolTip"))
        Me.ToolTip1.SetToolTip(Me, resources.GetString("$this.ToolTip1"))
        Me.ToolTipChangeTag.SetToolTip(Me, resources.GetString("$this.ToolTip2"))
        Me.GroupBoxConnections.ResumeLayout(False)
        Me.GroupBoxConnections.PerformLayout()
        Me.GroupBoxParameters.ResumeLayout(False)
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.TabPage1.PerformLayout()
        Me.TabPage3.ResumeLayout(False)
        Me.TabPage3.PerformLayout()
        Me.GroupBox5.ResumeLayout(False)
        Me.GroupBox5.PerformLayout()
        Me.GroupBox4.ResumeLayout(False)
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        Me.UtilitiesCtxMenu.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Public WithEvents GroupBoxConnections As System.Windows.Forms.GroupBox
    Public WithEvents Label7 As System.Windows.Forms.Label
    Public WithEvents Label6 As System.Windows.Forms.Label
    Public WithEvents Label5 As System.Windows.Forms.Label
    Public WithEvents Label4 As System.Windows.Forms.Label
    Public WithEvents Label3 As System.Windows.Forms.Label
    Public WithEvents cbOutlet1 As System.Windows.Forms.ComboBox
    Public WithEvents cbInlet6 As System.Windows.Forms.ComboBox
    Public WithEvents cbInlet5 As System.Windows.Forms.ComboBox
    Public WithEvents cbInlet4 As System.Windows.Forms.ComboBox
    Public WithEvents cbInlet3 As System.Windows.Forms.ComboBox
    Public WithEvents cbInlet2 As System.Windows.Forms.ComboBox
    Public WithEvents Label2 As System.Windows.Forms.Label
    Public WithEvents cbInlet1 As System.Windows.Forms.ComboBox
    Public WithEvents Label1 As System.Windows.Forms.Label
    Public WithEvents GroupBoxParameters As System.Windows.Forms.GroupBox
    Public WithEvents GroupBox4 As System.Windows.Forms.GroupBox
    Public WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Public WithEvents cbPropPack As System.Windows.Forms.ComboBox
    Public WithEvents Label9 As System.Windows.Forms.Label
    Public WithEvents GroupBox5 As System.Windows.Forms.GroupBox
    Public WithEvents lblConnectedTo As System.Windows.Forms.Label
    Public WithEvents lblStatus As System.Windows.Forms.Label
    Public WithEvents Label13 As System.Windows.Forms.Label
    Public WithEvents Label12 As System.Windows.Forms.Label
    Public WithEvents Label11 As System.Windows.Forms.Label
    Public WithEvents rtbAnnotations As Extended.Windows.Forms.RichTextBoxExtended
    Public WithEvents btnDisconnect1 As System.Windows.Forms.Button
    Public WithEvents btnDisconnectOutlet1 As System.Windows.Forms.Button
    Public WithEvents btnDisconnect6 As System.Windows.Forms.Button
    Public WithEvents btnDisconnect5 As System.Windows.Forms.Button
    Public WithEvents btnDisconnect4 As System.Windows.Forms.Button
    Public WithEvents btnDisconnect3 As System.Windows.Forms.Button
    Public WithEvents btnDisconnect2 As System.Windows.Forms.Button
    Public WithEvents chkActive As System.Windows.Forms.CheckBox
    Public WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Public WithEvents btnConfigurePP As System.Windows.Forms.Button
    Public WithEvents btnDisconnectEnergy As System.Windows.Forms.Button
    Public WithEvents Label16 As System.Windows.Forms.Label
    Public WithEvents cbEnergy As System.Windows.Forms.ComboBox
    Public WithEvents btnDisconnectOutlet3 As System.Windows.Forms.Button
    Public WithEvents Label15 As System.Windows.Forms.Label
    Public WithEvents cbOutlet3 As System.Windows.Forms.ComboBox
    Public WithEvents btnDisconnectOutlet2 As System.Windows.Forms.Button
    Public WithEvents Label14 As System.Windows.Forms.Label
    Public WithEvents cbOutlet2 As System.Windows.Forms.ComboBox
    Public WithEvents cbPress As System.Windows.Forms.ComboBox
    Public WithEvents tbPressure As System.Windows.Forms.TextBox
    Public WithEvents chkOverrideP As System.Windows.Forms.CheckBox
    Public WithEvents chkOverrideT As System.Windows.Forms.CheckBox
    Public WithEvents cbTemp As System.Windows.Forms.ComboBox
    Public WithEvents tbTemperature As System.Windows.Forms.TextBox
    Public WithEvents cbPressureCalcMode As System.Windows.Forms.ComboBox
    Public WithEvents Label8 As System.Windows.Forms.Label
    Public WithEvents lblTag As System.Windows.Forms.TextBox
    Public WithEvents btnCreateAndConnectEnergy As System.Windows.Forms.Button
    Public WithEvents btnCreateAndConnectOutlet3 As System.Windows.Forms.Button
    Public WithEvents btnCreateAndConnectOutlet2 As System.Windows.Forms.Button
    Public WithEvents btnCreateAndConnectOutlet1 As System.Windows.Forms.Button
    Public WithEvents btnCreateAndConnectInlet6 As System.Windows.Forms.Button
    Public WithEvents btnCreateAndConnectInlet5 As System.Windows.Forms.Button
    Public WithEvents btnCreateAndConnectInlet4 As System.Windows.Forms.Button
    Public WithEvents btnCreateAndConnectInlet3 As System.Windows.Forms.Button
    Public WithEvents btnCreateAndConnectInlet2 As System.Windows.Forms.Button
    Public WithEvents btnCreateAndConnectInlet1 As System.Windows.Forms.Button
    Public WithEvents btnUtils As System.Windows.Forms.Button
    Public WithEvents UtilitiesCtxMenu As System.Windows.Forms.ContextMenuStrip
    Public WithEvents AddUtilityTSMI As System.Windows.Forms.ToolStripMenuItem
    Public WithEvents sizingtsmi As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolTipChangeTag As ToolTip
    Public WithEvents cbCalcMode As ComboBox
    Public WithEvents Label10 As Label
    Friend WithEvents TabControl1 As TabControl
    Friend WithEvents TabPage1 As TabPage
    Friend WithEvents TabPage2 As TabPage
    Public WithEvents lblThickness As Label
    Public WithEvents tbThickness As TextBox
    Public WithEvents Label18 As Label
    Public WithEvents Label17 As Label
    Public WithEvents cbWallMaterial As ComboBox
    Friend WithEvents TabPage3 As TabPage
    Public WithEvents Label23 As Label
    Public WithEvents lbHeight As Label
    Public WithEvents Label22 As Label
    Public WithEvents tbHeight As TextBox
    Public WithEvents lbDiam As Label
    Public WithEvents Label20 As Label
    Public WithEvents tbDiam As TextBox
    Friend WithEvents rbHorizontal As RadioButton
    Friend WithEvents rbVertical As RadioButton
    Public WithEvents cbHeadType As ComboBox
    Public WithEvents Label24 As Label
    Public WithEvents Button1 As Button
End Class
