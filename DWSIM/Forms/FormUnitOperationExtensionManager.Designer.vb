<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FormUnitOperationExtensionManager
    Inherits System.Windows.Forms.Form

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

    Private components As System.ComponentModel.IContainer

    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.scMain = New System.Windows.Forms.SplitContainer()
        Me.lblUnitOps = New System.Windows.Forms.Label()
        Me.lvUnitOps = New System.Windows.Forms.ListView()
        Me.colTag = New System.Windows.Forms.ColumnHeader()
        Me.colType = New System.Windows.Forms.ColumnHeader()
        Me.lblExtensions = New System.Windows.Forms.Label()
        Me.clbExtensions = New System.Windows.Forms.CheckedListBox()
        Me.gbDetails = New System.Windows.Forms.GroupBox()
        Me.TableDetails = New System.Windows.Forms.TableLayoutPanel()
        Me.lblExtName = New System.Windows.Forms.Label()
        Me.lblExtNameValue = New System.Windows.Forms.Label()
        Me.lblExtDesc = New System.Windows.Forms.Label()
        Me.lblExtDescValue = New System.Windows.Forms.Label()
        Me.lblExtAuthor = New System.Windows.Forms.Label()
        Me.lblExtAuthorValue = New System.Windows.Forms.Label()
        Me.lblExtWebsite = New System.Windows.Forms.Label()
        Me.lblExtWebsiteValue = New System.Windows.Forms.Label()
        Me.pnlBottom = New System.Windows.Forms.Panel()
        Me.btnClose = New System.Windows.Forms.Button()
        CType(Me.scMain, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.scMain.Panel1.SuspendLayout()
        Me.scMain.Panel2.SuspendLayout()
        Me.scMain.SuspendLayout()
        Me.gbDetails.SuspendLayout()
        Me.TableDetails.SuspendLayout()
        Me.pnlBottom.SuspendLayout()
        Me.SuspendLayout()
        '
        'scMain
        '
        Me.scMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.scMain.Location = New System.Drawing.Point(8, 8)
        Me.scMain.Name = "scMain"
        Me.scMain.Size = New System.Drawing.Size(684, 446)
        Me.scMain.SplitterDistance = 240
        Me.scMain.TabIndex = 0
        '
        'scMain.Panel1 - Unit Operations
        '
        Me.scMain.Panel1.Controls.Add(Me.lvUnitOps)
        Me.scMain.Panel1.Controls.Add(Me.lblUnitOps)
        Me.scMain.Panel1.Padding = New System.Windows.Forms.Padding(0, 0, 4, 0)
        '
        'lblUnitOps
        '
        Me.lblUnitOps.Dock = System.Windows.Forms.DockStyle.Top
        Me.lblUnitOps.Font = New System.Drawing.Font(Me.Font.FontFamily, Me.Font.Size, System.Drawing.FontStyle.Bold)
        Me.lblUnitOps.Location = New System.Drawing.Point(0, 0)
        Me.lblUnitOps.Name = "lblUnitOps"
        Me.lblUnitOps.Size = New System.Drawing.Size(236, 20)
        Me.lblUnitOps.TabIndex = 0
        Me.lblUnitOps.Text = "Unit Operations"
        '
        'lvUnitOps
        '
        Me.lvUnitOps.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.colTag, Me.colType})
        Me.lvUnitOps.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lvUnitOps.FullRowSelect = True
        Me.lvUnitOps.HideSelection = False
        Me.lvUnitOps.Location = New System.Drawing.Point(0, 20)
        Me.lvUnitOps.MultiSelect = False
        Me.lvUnitOps.Name = "lvUnitOps"
        Me.lvUnitOps.Size = New System.Drawing.Size(236, 426)
        Me.lvUnitOps.TabIndex = 1
        Me.lvUnitOps.UseCompatibleStateImageBehavior = False
        Me.lvUnitOps.View = System.Windows.Forms.View.Details
        '
        'colTag
        '
        Me.colTag.Text = "Tag"
        Me.colTag.Width = 130
        '
        'colType
        '
        Me.colType.Text = "Type"
        Me.colType.Width = 100
        '
        'scMain.Panel2 - Extensions
        '
        Me.scMain.Panel2.Controls.Add(Me.gbDetails)
        Me.scMain.Panel2.Controls.Add(Me.clbExtensions)
        Me.scMain.Panel2.Controls.Add(Me.lblExtensions)
        Me.scMain.Panel2.Padding = New System.Windows.Forms.Padding(4, 0, 0, 0)
        '
        'lblExtensions
        '
        Me.lblExtensions.Dock = System.Windows.Forms.DockStyle.Top
        Me.lblExtensions.Font = New System.Drawing.Font(Me.Font.FontFamily, Me.Font.Size, System.Drawing.FontStyle.Bold)
        Me.lblExtensions.Location = New System.Drawing.Point(4, 0)
        Me.lblExtensions.Name = "lblExtensions"
        Me.lblExtensions.Size = New System.Drawing.Size(436, 20)
        Me.lblExtensions.TabIndex = 0
        Me.lblExtensions.Text = "Available Extensions"
        '
        'clbExtensions
        '
        Me.clbExtensions.CheckOnClick = True
        Me.clbExtensions.Dock = System.Windows.Forms.DockStyle.Top
        Me.clbExtensions.FormattingEnabled = True
        Me.clbExtensions.Location = New System.Drawing.Point(4, 20)
        Me.clbExtensions.Name = "clbExtensions"
        Me.clbExtensions.Size = New System.Drawing.Size(436, 180)
        Me.clbExtensions.TabIndex = 1
        '
        'gbDetails
        '
        Me.gbDetails.Anchor = System.Windows.Forms.AnchorStyles.Top Or
                              System.Windows.Forms.AnchorStyles.Left Or
                              System.Windows.Forms.AnchorStyles.Right Or
                              System.Windows.Forms.AnchorStyles.Bottom
        Me.gbDetails.Controls.Add(Me.TableDetails)
        Me.gbDetails.Location = New System.Drawing.Point(4, 208)
        Me.gbDetails.Name = "gbDetails"
        Me.gbDetails.Size = New System.Drawing.Size(436, 238)
        Me.gbDetails.TabIndex = 2
        Me.gbDetails.Text = "Extension Details"
        '
        'TableDetails
        '
        Me.TableDetails.ColumnCount = 2
        Me.TableDetails.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 90))
        Me.TableDetails.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100))
        Me.TableDetails.Controls.Add(Me.lblExtName, 0, 0)
        Me.TableDetails.Controls.Add(Me.lblExtNameValue, 1, 0)
        Me.TableDetails.Controls.Add(Me.lblExtDesc, 0, 1)
        Me.TableDetails.Controls.Add(Me.lblExtDescValue, 1, 1)
        Me.TableDetails.Controls.Add(Me.lblExtAuthor, 0, 2)
        Me.TableDetails.Controls.Add(Me.lblExtAuthorValue, 1, 2)
        Me.TableDetails.Controls.Add(Me.lblExtWebsite, 0, 3)
        Me.TableDetails.Controls.Add(Me.lblExtWebsiteValue, 1, 3)
        Me.TableDetails.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableDetails.Location = New System.Drawing.Point(3, 16)
        Me.TableDetails.Name = "TableDetails"
        Me.TableDetails.RowCount = 4
        Me.TableDetails.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.AutoSize))
        Me.TableDetails.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100))
        Me.TableDetails.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.AutoSize))
        Me.TableDetails.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.AutoSize))
        Me.TableDetails.Size = New System.Drawing.Size(430, 219)
        Me.TableDetails.TabIndex = 0
        '
        'lblExtName
        '
        Me.lblExtName.AutoSize = True
        Me.lblExtName.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblExtName.Font = New System.Drawing.Font(Me.Font.FontFamily, Me.Font.Size, System.Drawing.FontStyle.Bold)
        Me.lblExtName.Name = "lblExtName"
        Me.lblExtName.Text = "Name:"
        '
        'lblExtNameValue
        '
        Me.lblExtNameValue.AutoSize = True
        Me.lblExtNameValue.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblExtNameValue.Name = "lblExtNameValue"
        Me.lblExtNameValue.Text = ""
        '
        'lblExtDesc
        '
        Me.lblExtDesc.AutoSize = True
        Me.lblExtDesc.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblExtDesc.Font = New System.Drawing.Font(Me.Font.FontFamily, Me.Font.Size, System.Drawing.FontStyle.Bold)
        Me.lblExtDesc.Name = "lblExtDesc"
        Me.lblExtDesc.Text = "Description:"
        '
        'lblExtDescValue
        '
        Me.lblExtDescValue.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblExtDescValue.Name = "lblExtDescValue"
        Me.lblExtDescValue.Text = ""
        Me.lblExtDescValue.AutoSize = False
        Me.lblExtDescValue.AutoEllipsis = True
        '
        'lblExtAuthor
        '
        Me.lblExtAuthor.AutoSize = True
        Me.lblExtAuthor.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblExtAuthor.Font = New System.Drawing.Font(Me.Font.FontFamily, Me.Font.Size, System.Drawing.FontStyle.Bold)
        Me.lblExtAuthor.Name = "lblExtAuthor"
        Me.lblExtAuthor.Text = "Author:"
        '
        'lblExtAuthorValue
        '
        Me.lblExtAuthorValue.AutoSize = True
        Me.lblExtAuthorValue.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblExtAuthorValue.Name = "lblExtAuthorValue"
        Me.lblExtAuthorValue.Text = ""
        '
        'lblExtWebsite
        '
        Me.lblExtWebsite.AutoSize = True
        Me.lblExtWebsite.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblExtWebsite.Font = New System.Drawing.Font(Me.Font.FontFamily, Me.Font.Size, System.Drawing.FontStyle.Bold)
        Me.lblExtWebsite.Name = "lblExtWebsite"
        Me.lblExtWebsite.Text = "Website:"
        '
        'lblExtWebsiteValue
        '
        Me.lblExtWebsiteValue.AutoSize = True
        Me.lblExtWebsiteValue.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblExtWebsiteValue.Name = "lblExtWebsiteValue"
        Me.lblExtWebsiteValue.Text = ""
        '
        'pnlBottom
        '
        Me.pnlBottom.Controls.Add(Me.btnClose)
        Me.pnlBottom.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.pnlBottom.Location = New System.Drawing.Point(8, 454)
        Me.pnlBottom.Name = "pnlBottom"
        Me.pnlBottom.Padding = New System.Windows.Forms.Padding(0, 6, 0, 0)
        Me.pnlBottom.Size = New System.Drawing.Size(684, 38)
        Me.pnlBottom.TabIndex = 1
        '
        'btnClose
        '
        Me.btnClose.Anchor = System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right
        Me.btnClose.Location = New System.Drawing.Point(596, 6)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(88, 26)
        Me.btnClose.TabIndex = 0
        Me.btnClose.Text = "Close"
        Me.btnClose.UseVisualStyleBackColor = True
        '
        'FormUnitOperationExtensionManager
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96, 96)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.ClientSize = New System.Drawing.Size(700, 500)
        Me.Controls.Add(Me.scMain)
        Me.Controls.Add(Me.pnlBottom)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow
        Me.MinimumSize = New System.Drawing.Size(500, 400)
        Me.Name = "FormUnitOperationExtensionManager"
        Me.Padding = New System.Windows.Forms.Padding(8, 8, 8, 0)
        Me.ShowIcon = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Unit Operation Extension Manager"
        CType(Me.scMain, System.ComponentModel.ISupportInitialize).EndInit()
        Me.scMain.Panel1.ResumeLayout(False)
        Me.scMain.Panel2.ResumeLayout(False)
        Me.scMain.ResumeLayout(False)
        Me.gbDetails.ResumeLayout(False)
        Me.TableDetails.ResumeLayout(False)
        Me.TableDetails.PerformLayout()
        Me.pnlBottom.ResumeLayout(False)
        Me.ResumeLayout(False)
    End Sub

    Friend WithEvents scMain As System.Windows.Forms.SplitContainer
    Friend WithEvents lblUnitOps As System.Windows.Forms.Label
    Friend WithEvents lvUnitOps As System.Windows.Forms.ListView
    Friend WithEvents colTag As System.Windows.Forms.ColumnHeader
    Friend WithEvents colType As System.Windows.Forms.ColumnHeader
    Friend WithEvents lblExtensions As System.Windows.Forms.Label
    Friend WithEvents clbExtensions As System.Windows.Forms.CheckedListBox
    Friend WithEvents gbDetails As System.Windows.Forms.GroupBox
    Friend WithEvents TableDetails As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents lblExtName As System.Windows.Forms.Label
    Friend WithEvents lblExtNameValue As System.Windows.Forms.Label
    Friend WithEvents lblExtDesc As System.Windows.Forms.Label
    Friend WithEvents lblExtDescValue As System.Windows.Forms.Label
    Friend WithEvents lblExtAuthor As System.Windows.Forms.Label
    Friend WithEvents lblExtAuthorValue As System.Windows.Forms.Label
    Friend WithEvents lblExtWebsite As System.Windows.Forms.Label
    Friend WithEvents lblExtWebsiteValue As System.Windows.Forms.Label
    Friend WithEvents pnlBottom As System.Windows.Forms.Panel
    Friend WithEvents btnClose As System.Windows.Forms.Button
End Class
