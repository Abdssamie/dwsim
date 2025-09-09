Imports DWSIM.Interfaces.Enums.GraphicObjects
Imports unvell.ReoGrid
Imports WeifenLuo.WinFormsUI.Docking

Public Class EditingForm_ReliefValve

    Inherits SharedClasses.ObjectEditorForm

    Public Property SimObject As UnitOperations.ReliefValve

    Public Loaded As Boolean = False
    Public Filling As Boolean = False

    Dim units As SharedClasses.SystemsOfUnits.Units
    Dim nf As String

    Private Sub EditingForm_ReliefValve_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        SetupGrid()

        UpdateInfo()

        ChangeDefaultFont()

    End Sub

    Public Sub SetupGrid()

        With grid1.Worksheets(0)
            .SetScale(Settings.DpiScale)
            .SetRows(100)
            .SetCols(2)
            .SetColumnsWidth(0, 2, 100)
            .SetRangeStyles(0, 0, 100, 2, New WorksheetRangeStyle With {
                .Flag = PlainStyleFlag.HorizontalAlign,
                .HAlign = ReoGridHorAlign.Right
            })
            .SetRangeStyles(0, 0, 100, 2, New WorksheetRangeStyle With {
                .Flag = PlainStyleFlag.VerticalAlign,
                .VAlign = ReoGridVerAlign.Middle
            })
            .SetRangeStyles(0, 0, 100, 2, New WorksheetRangeStyle With {
                .Flag = PlainStyleFlag.FontAll,
                .FontName = System.Drawing.SystemFonts.MessageBoxFont.Name,
                .FontSize = System.Drawing.SystemFonts.MessageBoxFont.SizeInPoints
            })
            .ColumnHeaders(0).Text = "Opening (%)"
            .ColumnHeaders(1).Text = "Kv/Kvmax (%)"
            AddHandler .CellDataChanged,
                Sub(sender, e)
                    If Loaded And Not Filling Then

                        SimObject.FlowSheet.RegisterSnapshot(Interfaces.Enums.SnapshotType.ObjectData, SimObject)

                        SimObject.OpeningKvRelDataTableX.Clear()
                        SimObject.OpeningKvRelDataTableY.Clear()
                        For i = 0 To 99
                            Dim datax = .GetCellData(i, 0)
                            Dim datay = .GetCellData(i, 1)
                            If datax IsNot Nothing And datay IsNot Nothing Then
                                Try
                                    SimObject.OpeningKvRelDataTableX.Add(datax.ToString().ToDoubleFromCurrent())
                                    SimObject.OpeningKvRelDataTableY.Add(datay.ToString().ToDoubleFromCurrent())
                                Catch ex As Exception
                                    MessageBox.Show(String.Format("Error on data table: {0}", ex.Message), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                End Try
                            End If
                        Next
                    End If
                End Sub
        End With

    End Sub

    Sub UpdateInfo()

        units = SimObject.FlowSheet.FlowsheetOptions.SelectedUnitSystem
        nf = SimObject.FlowSheet.FlowsheetOptions.NumberFormat

        Loaded = False

        If Host.Items.Where(Function(x) x.Name.Contains(SimObject.GraphicObject.Tag)).Count > 0 Then
            If InspReportBar Is Nothing Then
                InspReportBar = New SharedClasses.InspectorReportBar
                InspReportBar.Dock = DockStyle.Bottom
                AddHandler InspReportBar.Button1.Click, Sub()
                                                            Dim iwindow As New Inspector.Window2
                                                            iwindow.SelectedObject = SimObject
                                                            iwindow.Show(DockPanel)
                                                        End Sub
                Me.Controls.Add(InspReportBar)
                InspReportBar.BringToFront()
            End If
        Else
            If InspReportBar IsNot Nothing Then
                Me.Controls.Remove(InspReportBar)
                InspReportBar = Nothing
            End If
        End If

        With SimObject

            'first block

            chkActive.Checked = .GraphicObject.Active

            ToolTip1.SetToolTip(chkActive, .FlowSheet.GetTranslatedString("AtivoInativo"))

            Me.Text = .GraphicObject.Tag & " (" & .GetDisplayName() & ")"

            lblTag.Text = .GraphicObject.Tag
            If .Calculated Then
                lblStatus.Text = .FlowSheet.GetTranslatedString("Calculado") & " (" & .LastUpdated.ToString & ")"
                lblStatus.ForeColor = System.Drawing.Color.Blue
            Else
                If Not .GraphicObject.Active Then
                    lblStatus.Text = .FlowSheet.GetTranslatedString("Inativo")
                    lblStatus.ForeColor = System.Drawing.Color.Gray
                ElseIf .ErrorMessage <> "" Then
                    lblStatus.Text = .FlowSheet.GetTranslatedString("Erro")
                    lblStatus.ForeColor = System.Drawing.Color.Red
                Else
                    lblStatus.Text = .FlowSheet.GetTranslatedString("NoCalculado")
                    lblStatus.ForeColor = System.Drawing.Color.Black
                End If
            End If

            lblConnectedTo.Text = ""

            If .IsSpecAttached Then lblConnectedTo.Text = .FlowSheet.SimulationObjects(.AttachedSpecId).GraphicObject.Tag
            If .IsAdjustAttached Then lblConnectedTo.Text = .FlowSheet.SimulationObjects(.AttachedAdjustId).GraphicObject.Tag

            'connections

            Dim mslist As String() = .FlowSheet.GraphicObjects.Values.Where(Function(x) x.ObjectType = ObjectType.MaterialStream).Select(Function(m) m.Tag).OrderBy(Function(m) m).ToArray

            cbInlet1.Items.Clear()
            cbInlet1.Items.AddRange(mslist)

            cbOutlet1.Items.Clear()
            cbOutlet1.Items.AddRange(mslist)

            If .GraphicObject.InputConnectors(0).IsAttached Then cbInlet1.SelectedItem = .GraphicObject.InputConnectors(0).AttachedConnector.AttachedFrom.Tag
            If .GraphicObject.OutputConnectors(0).IsAttached Then cbOutlet1.SelectedItem = .GraphicObject.OutputConnectors(0).AttachedConnector.AttachedTo.Tag

            'property package

            Dim proppacks As String() = .FlowSheet.PropertyPackages.Values.Select(Function(m) m.Tag).ToArray
            cbPropPack.Items.Clear()
            cbPropPack.Items.AddRange(proppacks)
            cbPropPack.SelectedItem = .PropertyPackage?.Tag

            'parameters

            Dim uobj = SimObject

            tbKvOpRel.Text = uobj.PercentOpeningVersusPercentKvExpression

            tbCharParam.Text = .CharacteristicParameter.ToString(nf)

            For i = 0 To .OpeningKvRelDataTableX.Count - 1
                grid1.Worksheets(0).SetCellData(i, 0, .OpeningKvRelDataTableX(i))
            Next
            For i = 0 To .OpeningKvRelDataTableY.Count - 1
                grid1.Worksheets(0).SetCellData(i, 1, .OpeningKvRelDataTableY(i))
            Next

            Panel1.Enabled = uobj.EnableOpeningKvRelationship

            cbOpeningKvRelType.SelectedIndex = .DefinedOpeningKvRelationShipType

        End With

        Loaded = True

    End Sub


    Private Sub btnConfigurePP_Click(sender As Object, e As EventArgs) Handles btnConfigurePP.Click

        SimObject.FlowSheet.PropertyPackages.Values.Where(Function(x) x.Tag = cbPropPack.SelectedItem.ToString).FirstOrDefault()?.DisplayGroupedEditingForm()

    End Sub

    Private Sub lblTag_TextChanged(sender As Object, e As EventArgs) Handles lblTag.TextChanged

        If Loaded Then ToolTipChangeTag.Show("Press ENTER to commit changes.", lblTag, New System.Drawing.Point(0, lblTag.Height + 3), 3000)

    End Sub

    Private Sub btnDisconnect1_Click(sender As Object, e As EventArgs) Handles btnDisconnect1.Click

        If cbInlet1.SelectedItem IsNot Nothing Then

            SimObject.FlowSheet.DisconnectObjects(SimObject.GraphicObject.InputConnectors(0).AttachedConnector.AttachedFrom, SimObject.GraphicObject)
            cbInlet1.SelectedItem = Nothing

        End If

    End Sub

    Private Sub btnDisconnectOutlet1_Click(sender As Object, e As EventArgs) Handles btnDisconnectOutlet1.Click

        If cbOutlet1.SelectedItem IsNot Nothing Then

            SimObject.FlowSheet.DisconnectObjects(SimObject.GraphicObject, SimObject.GraphicObject.OutputConnectors(0).AttachedConnector.AttachedTo)
            cbOutlet1.SelectedItem = Nothing

        End If

    End Sub


    Private Sub tb_TextChanged(sender As Object, e As EventArgs)

        Dim tbox = DirectCast(sender, TextBox)

        If tbox.Text.IsValidDoubleExpression Then
            tbox.ForeColor = System.Drawing.Color.Blue
        Else
            tbox.ForeColor = System.Drawing.Color.Red
        End If

    End Sub

    Private Sub TextBoxKeyDown(sender As Object, e As KeyEventArgs)

        If e.KeyCode = Keys.Enter And Loaded And DirectCast(sender, TextBox).ForeColor = System.Drawing.Color.Blue Then

            SimObject.FlowSheet.RegisterSnapshot(Interfaces.Enums.SnapshotType.ObjectData, SimObject)

            DirectCast(sender, TextBox).SelectAll()

        End If

    End Sub

    Private Sub cbPropPack_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbPropPack.SelectedIndexChanged

        If Loaded Then

            SimObject.FlowSheet.RegisterSnapshot(Interfaces.Enums.SnapshotType.ObjectData, SimObject)
            SimObject.PropertyPackage = SimObject.FlowSheet.PropertyPackages.Values.Where(Function(x) x.Tag = cbPropPack.SelectedItem.ToString).SingleOrDefault

        End If

    End Sub

    Private Sub cbInlet1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbInlet1.SelectedIndexChanged

        If Loaded Then

            Dim text As String = cbInlet1.Text

            If text <> "" Then

                Dim index As Integer = 0

                Dim gobj = SimObject.GraphicObject
                Dim flowsheet = SimObject.FlowSheet

                If flowsheet.GetFlowsheetSimulationObject(text).GraphicObject.OutputConnectors(0).IsAttached Then
                    MessageBox.Show(flowsheet.GetTranslatedString("Todasasconexespossve"), flowsheet.GetTranslatedString("Erro"), MessageBoxButtons.OK, MessageBoxIcon.Error)
                Else
                    If gobj.InputConnectors(index).IsAttached Then flowsheet.DisconnectObjects(gobj.InputConnectors(index).AttachedConnector.AttachedFrom, gobj)
                    Try
                        flowsheet.ConnectObjects(flowsheet.GetFlowsheetSimulationObject(text).GraphicObject, gobj, 0, index)
                    Catch ex As Exception
                        MessageBox.Show(ex.Message, flowsheet.GetTranslatedString("Erro"), MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End Try
                End If
                UpdateInfo()

            End If

        End If

    End Sub

    Private Sub cbOutlet1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbOutlet1.SelectedIndexChanged

        If Loaded Then

            Dim text As String = cbOutlet1.Text

            If text <> "" Then

                Dim index As Integer = 0

                Dim gobj = SimObject.GraphicObject
                Dim flowsheet = SimObject.FlowSheet

                If flowsheet.GetFlowsheetSimulationObject(text).GraphicObject.InputConnectors(0).IsAttached Then
                    MessageBox.Show(flowsheet.GetTranslatedString("Todasasconexespossve"), flowsheet.GetTranslatedString("Erro"), MessageBoxButtons.OK, MessageBoxIcon.Error)
                Else
                    Try
                        If gobj.OutputConnectors(0).IsAttached Then flowsheet.DisconnectObjects(gobj, gobj.OutputConnectors(0).AttachedConnector.AttachedTo)
                        flowsheet.ConnectObjects(gobj, flowsheet.GetFlowsheetSimulationObject(text).GraphicObject, 0, 0)
                    Catch ex As Exception
                        MessageBox.Show(ex.Message, flowsheet.GetTranslatedString("Erro"), MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End Try
                End If
                UpdateInfo()

            End If

        End If

    End Sub

    Private Sub chkActive_CheckedChanged(sender As Object, e As EventArgs) Handles chkActive.CheckedChanged

        If Loaded Then

            SimObject.GraphicObject.Active = chkActive.Checked
            SimObject.FlowSheet.UpdateInterface()
            UpdateInfo()

        End If

    End Sub

    Private Sub btnCreateAndConnectInlet1_Click(sender As Object, e As EventArgs) Handles btnCreateAndConnectInlet1.Click, btnCreateAndConnectOutlet1.Click

        Dim sgobj = SimObject.GraphicObject
        Dim fs = SimObject.FlowSheet

        Dim iidx As Integer = -1
        Dim oidx As Integer = -1

        If sender Is btnCreateAndConnectInlet1 Then

            iidx = 0

        ElseIf sender Is btnCreateAndConnectOutlet1 Then

            oidx = 0

        End If

        If iidx >= 0 Then

            Dim obj = fs.AddObject(ObjectType.MaterialStream, sgobj.InputConnectors(iidx).Position.X - 50, sgobj.InputConnectors(iidx).Position.Y, "")

            If sgobj.InputConnectors(iidx).IsAttached Then fs.DisconnectObjects(sgobj.InputConnectors(iidx).AttachedConnector.AttachedFrom, sgobj)
            fs.ConnectObjects(obj.GraphicObject, sgobj, 0, iidx)

        End If

        If oidx >= 0 Then

            Dim obj = fs.AddObject(ObjectType.MaterialStream, sgobj.OutputConnectors(oidx).Position.X + 30, sgobj.OutputConnectors(oidx).Position.Y, "")

            If sgobj.OutputConnectors(oidx).IsAttached Then fs.DisconnectObjects(sgobj, sgobj.OutputConnectors(oidx).AttachedConnector.AttachedTo)
            fs.ConnectObjects(sgobj, obj.GraphicObject, oidx, 0)

        End If

        UpdateInfo()

    End Sub

    Private Sub btnUtils_Click(sender As Object, e As EventArgs) Handles btnUtils.Click

        UtilitiesCtxMenu.Show(btnUtils, New System.Drawing.Point(20, 0))

    End Sub

    Private Sub sizingtsmi_Click(sender As Object, e As EventArgs) Handles sizingtsmi.Click

        Dim utility As Interfaces.IAttachedUtility = SimObject.FlowSheet.GetUtility(Enums.FlowsheetUtility.PSVSizing)
        utility.Name = "PressureSafetyValveSizing" & (SimObject.AttachedUtilities.Where(Function(x) x.GetUtilityType = Interfaces.Enums.FlowsheetUtility.PSVSizing).Count + 1).ToString

        utility.AttachedTo = SimObject

        With DirectCast(utility, DockContent)
            .ShowHint = WeifenLuo.WinFormsUI.Docking.DockState.Float
        End With

        SimObject.AttachedUtilities.Add(utility)
        SimObject.FlowSheet.DisplayForm(utility)

        AddHandler DirectCast(utility, Form).FormClosed, Sub()
                                                             utility.AttachedTo = Nothing
                                                             SimObject.AttachedUtilities.Remove(utility)
                                                         End Sub
    End Sub

    Private Sub UtilitiesCtxMenu_Opening(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles UtilitiesCtxMenu.Opening

        For Each item In SimObject.AttachedUtilities
            Dim ts As New ToolStripMenuItem(item.Name)
            AddHandler ts.Click, Sub()
                                     Dim f = DirectCast(item, DockContent)
                                     If f.Visible Then
                                         f.Select()
                                     Else
                                         SimObject.FlowSheet.DisplayForm(f)
                                     End If
                                 End Sub
            UtilitiesCtxMenu.Items.Add(ts)
            AddHandler UtilitiesCtxMenu.Closed, Sub() If UtilitiesCtxMenu.Items.Contains(ts) Then UtilitiesCtxMenu.Items.Remove(ts)
            AddHandler DirectCast(item, DockContent).FormClosed, Sub()
                                                                     SimObject.AttachedUtilities.Remove(item)
                                                                     item.AttachedTo = Nothing
                                                                 End Sub
        Next

    End Sub

    Private Sub tbKvOpRel_TextChanged(sender As Object, e As EventArgs) Handles tbKvOpRel.KeyDown

        SimObject.PercentOpeningVersusPercentKvExpression = tbKvOpRel.Text

    End Sub

    Private Sub lblTag_KeyPress(sender As Object, e As KeyEventArgs) Handles lblTag.KeyUp

        If e.KeyCode = Keys.Enter Then

            SimObject.FlowSheet.RegisterSnapshot(Interfaces.Enums.SnapshotType.ObjectLayout)

            If Loaded Then SimObject.GraphicObject.Tag = lblTag.Text
            If Loaded Then SimObject.FlowSheet.UpdateOpenEditForms()
            Me.Text = SimObject.GraphicObject.Tag & " (" & SimObject.GetDisplayName() & ")"
            DirectCast(SimObject.FlowSheet, Interfaces.IFlowsheetGUI).UpdateInterface()

        End If

    End Sub

    Private Sub tbCharParam_TextChanged(sender As Object, e As EventArgs) Handles tbCharParam.TextChanged

        Try
            SimObject.CharacteristicParameter = tbCharParam.Text.ToDoubleFromCurrent()
        Catch ex As Exception
        End Try

    End Sub

    Private Sub cbOpeningKvRelType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbOpeningKvRelType.SelectedIndexChanged

        If Loaded Then SimObject.FlowSheet.RegisterSnapshot(Interfaces.Enums.SnapshotType.ObjectData, SimObject)

        SimObject.DefinedOpeningKvRelationShipType = cbOpeningKvRelType.SelectedIndex

        gbTable.Enabled = False
        tbKvOpRel.Enabled = False
        tbCharParam.Enabled = False

        Select Case SimObject.DefinedOpeningKvRelationShipType
            Case UnitOperations.Valve.OpeningKvRelationshipType.DataTable
                gbTable.Enabled = True
            Case UnitOperations.Valve.OpeningKvRelationshipType.UserDefined
                tbKvOpRel.Enabled = True
            Case UnitOperations.Valve.OpeningKvRelationshipType.QuickOpening
                tbCharParam.Enabled = True
        End Select

    End Sub

End Class