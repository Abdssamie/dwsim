<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class EditingForm_ReliefValve

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
        Me.components = New System.ComponentModel.Container()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.gbTable = New System.Windows.Forms.GroupBox()
        Me.grid1 = New unvell.ReoGrid.ReoGridControl()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.tbCharParam = New System.Windows.Forms.TextBox()
        Me.tbKvOpRel = New System.Windows.Forms.TextBox()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.cbOpeningKvRelType = New System.Windows.Forms.ComboBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.GroupBoxConnections = New System.Windows.Forms.GroupBox()
        Me.btnCreateAndConnectOutlet1 = New System.Windows.Forms.Button()
        Me.btnCreateAndConnectInlet1 = New System.Windows.Forms.Button()
        Me.btnDisconnectOutlet1 = New System.Windows.Forms.Button()
        Me.btnDisconnect1 = New System.Windows.Forms.Button()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.cbOutlet1 = New System.Windows.Forms.ComboBox()
        Me.cbInlet1 = New System.Windows.Forms.ComboBox()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.GroupBox5 = New System.Windows.Forms.GroupBox()
        Me.btnUtils = New System.Windows.Forms.Button()
        Me.lblTag = New System.Windows.Forms.TextBox()
        Me.chkActive = New System.Windows.Forms.CheckBox()
        Me.lblConnectedTo = New System.Windows.Forms.Label()
        Me.lblStatus = New System.Windows.Forms.Label()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.btnConfigurePP = New System.Windows.Forms.Button()
        Me.cbPropPack = New System.Windows.Forms.ComboBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.UtilitiesCtxMenu = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.AddUtilityTSMI = New System.Windows.Forms.ToolStripMenuItem()
        Me.sizingtsmi = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolTipChangeTag = New System.Windows.Forms.ToolTip(Me.components)
        Me.Panel1.SuspendLayout()
        Me.gbTable.SuspendLayout()
        Me.GroupBoxConnections.SuspendLayout()
        Me.GroupBox5.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.UtilitiesCtxMenu.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel1.Controls.Add(Me.gbTable)
        Me.Panel1.Controls.Add(Me.Label4)
        Me.Panel1.Controls.Add(Me.tbCharParam)
        Me.Panel1.Controls.Add(Me.tbKvOpRel)
        Me.Panel1.Controls.Add(Me.Label14)
        Me.Panel1.Controls.Add(Me.cbOpeningKvRelType)
        Me.Panel1.Controls.Add(Me.Label10)
        Me.Panel1.Location = New System.Drawing.Point(7, 259)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(462, 557)
        Me.Panel1.TabIndex = 49
        '
        'gbTable
        '
        Me.gbTable.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.gbTable.Controls.Add(Me.grid1)
        Me.gbTable.Location = New System.Drawing.Point(6, 126)
        Me.gbTable.Name = "gbTable"
        Me.gbTable.Size = New System.Drawing.Size(453, 428)
        Me.gbTable.TabIndex = 47
        Me.gbTable.TabStop = False
        Me.gbTable.Text = "Data Table"
        '
        'grid1
        '
        Me.grid1.BackColor = System.Drawing.Color.White
        Me.grid1.ColumnHeaderContextMenuStrip = Nothing
        Me.grid1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grid1.LeadHeaderContextMenuStrip = Nothing
        Me.grid1.Location = New System.Drawing.Point(3, 16)
        Me.grid1.Name = "grid1"
        Me.grid1.RowHeaderContextMenuStrip = Nothing
        Me.grid1.Script = Nothing
        Me.grid1.SheetTabContextMenuStrip = Nothing
        Me.grid1.SheetTabNewButtonVisible = False
        Me.grid1.SheetTabVisible = False
        Me.grid1.SheetTabWidth = 60
        Me.grid1.ShowScrollEndSpacing = True
        Me.grid1.Size = New System.Drawing.Size(447, 409)
        Me.grid1.TabIndex = 0
        Me.grid1.Text = "ReoGridControl1"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.Label4.Location = New System.Drawing.Point(3, 43)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(215, 13)
        Me.Label4.TabIndex = 35
        Me.Label4.Text = "Kv[Cv]/Kv[Cv]max (%) = f(OP(%)) expression"
        '
        'tbCharParam
        '
        Me.tbCharParam.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tbCharParam.Location = New System.Drawing.Point(269, 91)
        Me.tbCharParam.Name = "tbCharParam"
        Me.tbCharParam.Size = New System.Drawing.Size(84, 20)
        Me.tbCharParam.TabIndex = 46
        Me.tbCharParam.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'tbKvOpRel
        '
        Me.tbKvOpRel.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tbKvOpRel.Location = New System.Drawing.Point(4, 64)
        Me.tbKvOpRel.Name = "tbKvOpRel"
        Me.tbKvOpRel.Size = New System.Drawing.Size(455, 20)
        Me.tbKvOpRel.TabIndex = 36
        Me.tbKvOpRel.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.Label14.Location = New System.Drawing.Point(3, 95)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(122, 13)
        Me.Label14.TabIndex = 45
        Me.Label14.Text = "Characteristic Parameter"
        '
        'cbOpeningKvRelType
        '
        Me.cbOpeningKvRelType.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cbOpeningKvRelType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbOpeningKvRelType.FormattingEnabled = True
        Me.cbOpeningKvRelType.Items.AddRange(New Object() {"Linear", "Equal Percentage", "Quick Opening", "User-Defined Expression", "Data Table"})
        Me.cbOpeningKvRelType.Location = New System.Drawing.Point(270, 10)
        Me.cbOpeningKvRelType.Name = "cbOpeningKvRelType"
        Me.cbOpeningKvRelType.Size = New System.Drawing.Size(189, 21)
        Me.cbOpeningKvRelType.TabIndex = 44
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.Label10.Location = New System.Drawing.Point(3, 14)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(124, 13)
        Me.Label10.TabIndex = 43
        Me.Label10.Text = "Opening/Kv[Cv] rel. type"
        '
        'GroupBoxConnections
        '
        Me.GroupBoxConnections.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBoxConnections.Controls.Add(Me.btnCreateAndConnectOutlet1)
        Me.GroupBoxConnections.Controls.Add(Me.btnCreateAndConnectInlet1)
        Me.GroupBoxConnections.Controls.Add(Me.btnDisconnectOutlet1)
        Me.GroupBoxConnections.Controls.Add(Me.btnDisconnect1)
        Me.GroupBoxConnections.Controls.Add(Me.Label7)
        Me.GroupBoxConnections.Controls.Add(Me.cbOutlet1)
        Me.GroupBoxConnections.Controls.Add(Me.cbInlet1)
        Me.GroupBoxConnections.Controls.Add(Me.Label19)
        Me.GroupBoxConnections.Location = New System.Drawing.Point(7, 107)
        Me.GroupBoxConnections.Name = "GroupBoxConnections"
        Me.GroupBoxConnections.Size = New System.Drawing.Size(462, 83)
        Me.GroupBoxConnections.TabIndex = 52
        Me.GroupBoxConnections.TabStop = False
        Me.GroupBoxConnections.Text = "Connections"
        '
        'btnCreateAndConnectOutlet1
        '
        Me.btnCreateAndConnectOutlet1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnCreateAndConnectOutlet1.BackgroundImage = Global.DWSIM.UnitOperations.My.Resources.Resources.bullet_lightning
        Me.btnCreateAndConnectOutlet1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.btnCreateAndConnectOutlet1.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.btnCreateAndConnectOutlet1.Location = New System.Drawing.Point(408, 50)
        Me.btnCreateAndConnectOutlet1.Name = "btnCreateAndConnectOutlet1"
        Me.btnCreateAndConnectOutlet1.Size = New System.Drawing.Size(21, 21)
        Me.btnCreateAndConnectOutlet1.TabIndex = 46
        Me.btnCreateAndConnectOutlet1.UseVisualStyleBackColor = True
        '
        'btnCreateAndConnectInlet1
        '
        Me.btnCreateAndConnectInlet1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnCreateAndConnectInlet1.BackgroundImage = Global.DWSIM.UnitOperations.My.Resources.Resources.bullet_lightning
        Me.btnCreateAndConnectInlet1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.btnCreateAndConnectInlet1.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.btnCreateAndConnectInlet1.Location = New System.Drawing.Point(408, 23)
        Me.btnCreateAndConnectInlet1.Name = "btnCreateAndConnectInlet1"
        Me.btnCreateAndConnectInlet1.Size = New System.Drawing.Size(21, 21)
        Me.btnCreateAndConnectInlet1.TabIndex = 45
        Me.btnCreateAndConnectInlet1.UseVisualStyleBackColor = True
        '
        'btnDisconnectOutlet1
        '
        Me.btnDisconnectOutlet1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnDisconnectOutlet1.Image = Global.DWSIM.UnitOperations.My.Resources.Resources.disconnect
        Me.btnDisconnectOutlet1.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.btnDisconnectOutlet1.Location = New System.Drawing.Point(435, 50)
        Me.btnDisconnectOutlet1.Name = "btnDisconnectOutlet1"
        Me.btnDisconnectOutlet1.Size = New System.Drawing.Size(21, 21)
        Me.btnDisconnectOutlet1.TabIndex = 20
        Me.ToolTipValues.SetToolTip(Me.btnDisconnectOutlet1, "Disconnect")
        Me.btnDisconnectOutlet1.UseVisualStyleBackColor = True
        '
        'btnDisconnect1
        '
        Me.btnDisconnect1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnDisconnect1.Image = Global.DWSIM.UnitOperations.My.Resources.Resources.disconnect
        Me.btnDisconnect1.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.btnDisconnect1.Location = New System.Drawing.Point(435, 23)
        Me.btnDisconnect1.Name = "btnDisconnect1"
        Me.btnDisconnect1.Size = New System.Drawing.Size(21, 21)
        Me.btnDisconnect1.TabIndex = 14
        Me.ToolTipValues.SetToolTip(Me.btnDisconnect1, "Disconnect")
        Me.btnDisconnect1.UseVisualStyleBackColor = True
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.Label7.Location = New System.Drawing.Point(9, 53)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(71, 13)
        Me.Label7.TabIndex = 13
        Me.Label7.Text = "Outlet Stream"
        '
        'cbOutlet1
        '
        Me.cbOutlet1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cbOutlet1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbOutlet1.FormattingEnabled = True
        Me.cbOutlet1.Location = New System.Drawing.Point(262, 50)
        Me.cbOutlet1.Name = "cbOutlet1"
        Me.cbOutlet1.Size = New System.Drawing.Size(140, 21)
        Me.cbOutlet1.TabIndex = 8
        '
        'cbInlet1
        '
        Me.cbInlet1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cbInlet1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbInlet1.FormattingEnabled = True
        Me.cbInlet1.Location = New System.Drawing.Point(262, 23)
        Me.cbInlet1.Name = "cbInlet1"
        Me.cbInlet1.Size = New System.Drawing.Size(140, 21)
        Me.cbInlet1.TabIndex = 1
        '
        'Label19
        '
        Me.Label19.AutoSize = True
        Me.Label19.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.Label19.Location = New System.Drawing.Point(9, 26)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(63, 13)
        Me.Label19.TabIndex = 0
        Me.Label19.Text = "Inlet Stream"
        '
        'GroupBox5
        '
        Me.GroupBox5.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox5.Controls.Add(Me.btnUtils)
        Me.GroupBox5.Controls.Add(Me.lblTag)
        Me.GroupBox5.Controls.Add(Me.chkActive)
        Me.GroupBox5.Controls.Add(Me.lblConnectedTo)
        Me.GroupBox5.Controls.Add(Me.lblStatus)
        Me.GroupBox5.Controls.Add(Me.Label13)
        Me.GroupBox5.Controls.Add(Me.Label12)
        Me.GroupBox5.Controls.Add(Me.Label11)
        Me.GroupBox5.Location = New System.Drawing.Point(7, 7)
        Me.GroupBox5.Name = "GroupBox5"
        Me.GroupBox5.Size = New System.Drawing.Size(462, 98)
        Me.GroupBox5.TabIndex = 51
        Me.GroupBox5.TabStop = False
        Me.GroupBox5.Text = "General Info"
        '
        'btnUtils
        '
        Me.btnUtils.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnUtils.Image = Global.DWSIM.UnitOperations.My.Resources.Resources.bullet_sparkle
        Me.btnUtils.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.btnUtils.Location = New System.Drawing.Point(435, 18)
        Me.btnUtils.Name = "btnUtils"
        Me.btnUtils.Size = New System.Drawing.Size(20, 20)
        Me.btnUtils.TabIndex = 27
        Me.ToolTipValues.SetToolTip(Me.btnUtils, "Add/View Utilities")
        Me.btnUtils.UseVisualStyleBackColor = True
        '
        'lblTag
        '
        Me.lblTag.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblTag.Location = New System.Drawing.Point(133, 19)
        Me.lblTag.Name = "lblTag"
        Me.lblTag.Size = New System.Drawing.Size(296, 20)
        Me.lblTag.TabIndex = 24
        '
        'chkActive
        '
        Me.chkActive.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.chkActive.Appearance = System.Windows.Forms.Appearance.Button
        Me.chkActive.Image = Global.DWSIM.UnitOperations.My.Resources.Resources.bullet_tick
        Me.chkActive.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.chkActive.Location = New System.Drawing.Point(435, 43)
        Me.chkActive.Name = "chkActive"
        Me.chkActive.Size = New System.Drawing.Size(21, 21)
        Me.chkActive.TabIndex = 21
        Me.ToolTipValues.SetToolTip(Me.chkActive, "Active/Inactive")
        Me.chkActive.UseVisualStyleBackColor = True
        '
        'lblConnectedTo
        '
        Me.lblConnectedTo.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblConnectedTo.AutoSize = True
        Me.lblConnectedTo.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblConnectedTo.Location = New System.Drawing.Point(132, 72)
        Me.lblConnectedTo.Name = "lblConnectedTo"
        Me.lblConnectedTo.Size = New System.Drawing.Size(38, 13)
        Me.lblConnectedTo.TabIndex = 20
        Me.lblConnectedTo.Text = "Objeto"
        '
        'lblStatus
        '
        Me.lblStatus.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblStatus.AutoSize = True
        Me.lblStatus.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblStatus.Location = New System.Drawing.Point(132, 47)
        Me.lblStatus.Name = "lblStatus"
        Me.lblStatus.Size = New System.Drawing.Size(38, 13)
        Me.lblStatus.TabIndex = 19
        Me.lblStatus.Text = "Objeto"
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.Label13.Location = New System.Drawing.Point(9, 72)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(51, 13)
        Me.Label13.TabIndex = 17
        Me.Label13.Text = "Linked to"
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.Label12.Location = New System.Drawing.Point(9, 47)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(37, 13)
        Me.Label12.TabIndex = 16
        Me.Label12.Text = "Status"
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.Label11.Location = New System.Drawing.Point(9, 22)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(38, 13)
        Me.Label11.TabIndex = 14
        Me.Label11.Text = "Object"
        '
        'GroupBox3
        '
        Me.GroupBox3.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox3.Controls.Add(Me.btnConfigurePP)
        Me.GroupBox3.Controls.Add(Me.cbPropPack)
        Me.GroupBox3.Controls.Add(Me.Label9)
        Me.GroupBox3.Location = New System.Drawing.Point(7, 193)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(462, 60)
        Me.GroupBox3.TabIndex = 50
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "Property Package Settings"
        '
        'btnConfigurePP
        '
        Me.btnConfigurePP.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnConfigurePP.BackgroundImage = Global.DWSIM.UnitOperations.My.Resources.Resources.cog
        Me.btnConfigurePP.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnConfigurePP.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.btnConfigurePP.Location = New System.Drawing.Point(435, 26)
        Me.btnConfigurePP.Name = "btnConfigurePP"
        Me.btnConfigurePP.Size = New System.Drawing.Size(21, 21)
        Me.btnConfigurePP.TabIndex = 20
        Me.ToolTipValues.SetToolTip(Me.btnConfigurePP, "Configure")
        Me.btnConfigurePP.UseVisualStyleBackColor = True
        '
        'cbPropPack
        '
        Me.cbPropPack.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cbPropPack.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbPropPack.FormattingEnabled = True
        Me.cbPropPack.Location = New System.Drawing.Point(262, 26)
        Me.cbPropPack.Name = "cbPropPack"
        Me.cbPropPack.Size = New System.Drawing.Size(167, 21)
        Me.cbPropPack.TabIndex = 15
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.Label9.Location = New System.Drawing.Point(8, 29)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(92, 13)
        Me.Label9.TabIndex = 14
        Me.Label9.Text = "Property Package"
        '
        'UtilitiesCtxMenu
        '
        Me.UtilitiesCtxMenu.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.AddUtilityTSMI})
        Me.UtilitiesCtxMenu.Name = "ContextMenuStrip1"
        Me.UtilitiesCtxMenu.Size = New System.Drawing.Size(144, 26)
        '
        'AddUtilityTSMI
        '
        Me.AddUtilityTSMI.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.sizingtsmi})
        Me.AddUtilityTSMI.Image = Global.DWSIM.UnitOperations.My.Resources.Resources.add
        Me.AddUtilityTSMI.Name = "AddUtilityTSMI"
        Me.AddUtilityTSMI.Size = New System.Drawing.Size(143, 22)
        Me.AddUtilityTSMI.Text = "Attach Utility"
        '
        'sizingtsmi
        '
        Me.sizingtsmi.Image = Global.DWSIM.UnitOperations.My.Resources.Resources.cog
        Me.sizingtsmi.Name = "sizingtsmi"
        Me.sizingtsmi.Size = New System.Drawing.Size(277, 22)
        Me.sizingtsmi.Text = "Pressure Safety Valve Sizing/Evaluation"
        '
        'ToolTipChangeTag
        '
        Me.ToolTipChangeTag.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info
        Me.ToolTipChangeTag.ToolTipTitle = "Info"
        '
        'EditingForm_ReliefValve
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.AutoScroll = True
        Me.ClientSize = New System.Drawing.Size(481, 828)
        Me.Controls.Add(Me.GroupBoxConnections)
        Me.Controls.Add(Me.GroupBox5)
        Me.Controls.Add(Me.GroupBox3)
        Me.Controls.Add(Me.Panel1)
        Me.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Name = "EditingForm_ReliefValve"
        Me.ShowHint = WeifenLuo.WinFormsUI.Docking.DockState.DockLeft
        Me.Text = "EditingForm_ReliefValve"
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.gbTable.ResumeLayout(False)
        Me.GroupBoxConnections.ResumeLayout(False)
        Me.GroupBoxConnections.PerformLayout()
        Me.GroupBox5.ResumeLayout(False)
        Me.GroupBox5.PerformLayout()
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        Me.UtilitiesCtxMenu.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents Panel1 As Panel
    Friend WithEvents gbTable As GroupBox
    Friend WithEvents grid1 As unvell.ReoGrid.ReoGridControl
    Public WithEvents Label4 As Label
    Public WithEvents tbCharParam As TextBox
    Public WithEvents tbKvOpRel As TextBox
    Public WithEvents Label14 As Label
    Public WithEvents cbOpeningKvRelType As ComboBox
    Public WithEvents Label10 As Label
    Public WithEvents GroupBoxConnections As GroupBox
    Public WithEvents btnCreateAndConnectOutlet1 As Button
    Public WithEvents btnCreateAndConnectInlet1 As Button
    Public WithEvents btnDisconnectOutlet1 As Button
    Public WithEvents btnDisconnect1 As Button
    Public WithEvents Label7 As Label
    Public WithEvents cbOutlet1 As ComboBox
    Public WithEvents cbInlet1 As ComboBox
    Public WithEvents Label19 As Label
    Public WithEvents GroupBox5 As GroupBox
    Public WithEvents btnUtils As Button
    Public WithEvents lblTag As TextBox
    Public WithEvents chkActive As CheckBox
    Public WithEvents lblConnectedTo As Label
    Public WithEvents lblStatus As Label
    Public WithEvents Label13 As Label
    Public WithEvents Label12 As Label
    Public WithEvents Label11 As Label
    Public WithEvents GroupBox3 As GroupBox
    Public WithEvents btnConfigurePP As Button
    Public WithEvents cbPropPack As ComboBox
    Public WithEvents Label9 As Label
    Public WithEvents ToolTip1 As ToolTip
    Public WithEvents UtilitiesCtxMenu As ContextMenuStrip
    Public WithEvents AddUtilityTSMI As ToolStripMenuItem
    Public WithEvents sizingtsmi As ToolStripMenuItem
    Friend WithEvents ToolTipChangeTag As ToolTip
End Class
