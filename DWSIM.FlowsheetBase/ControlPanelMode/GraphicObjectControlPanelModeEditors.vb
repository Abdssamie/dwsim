Imports DWSIM.ExtensionMethods
Imports DWSIM.Interfaces
Imports DWSIM.SharedClasses
Imports Eto.Forms
Imports DWSIM.UI.Shared.Common
Imports DWSIM.UnitOperations.SpecialOps

Public Class GraphicObjectControlPanelModeEditors

    Private Shared Function CreateInputForm() As Dialog

        Dim tb As New TextBox With {.Width = 100}
        Dim f As New Dialog
        f.Content = tb
        f.AutoSize = True
        Return f

    End Function

    Public Shared Sub SetInputDelegate(gobj As IGraphicObject, myObj As ISimulationObject)

        gobj.ControlPanelModeEditorDisplayDelegate =
            Sub()
                Dim f = CreateInputForm()
                Dim tb = DirectCast(f.Content, TextBox)
                Dim SelectedObject = myObj?.GetFlowsheet.SimulationObjects.Values.Where(Function(x2) x2.Name = myObj.SelectedObjectID).FirstOrDefault
                If Not SelectedObject Is Nothing Then
                    Dim currentvalue = SystemsOfUnits.Converter.ConvertFromSI(myObj.SelectedPropertyUnits, SelectedObject.GetPropertyValue(myObj.SelectedProperty))
                    tb.Text = currentvalue.ToString(myObj?.GetFlowsheet.FlowsheetOptions.NumberFormat)
                    f.Title = SelectedObject.GraphicObject.Tag + "/" + myObj?.GetFlowsheet.GetTranslatedString(myObj.SelectedProperty)
                    AddHandler tb.KeyDown,
                    Sub(s, e)
                        If e.Key = Keys.Enter Then
                            Try
                                SelectedObject.SetPropertyValue(myObj.SelectedProperty, tb.Text.ToDoubleFromCurrent().ConvertToSI(myObj.SelectedPropertyUnits))
                                f.Close()
                            Catch ex As Exception
                                MessageBox.Show("Error", ex.Message, MessageBoxButtons.OK, MessageBoxType.Error)
                            End Try
                        End If
                    End Sub
                    f.Location = Mouse.Position
                    f.ShowModal()
                End If
            End Sub


    End Sub

    Private Shared Function CreatePIDForm(PIDobj As ISimulationObject) As Dialog

        Dim PID As PIDController = PIDobj

        Dim fs = PID.GetFlowsheet()
        Dim nf = fs.FlowsheetOptions.NumberFormat
        Dim units = fs.FlowsheetOptions.SelectedUnitSystem

        Dim panel = GetDefaultContainer()
        Dim form = CreateDialog(panel, PID.GraphicObject.Tag, 200, 200)

        Dim btn1, btn2 As Button
        Dim tb1, tb2, tb3 As TextBox
        Dim che As New CheckBox

        Dim isActive = PID.Active
        Dim isAuto = Not PID.ManualOverride

        btn1 = panel.CreateAndAddButtonRow(If(isActive, "ON", "OFF"), Nothing,
                                    Sub(btn, e)
                                        If PID Is Nothing Then Exit Sub
                                        isActive = Not isActive
                                        PID.Active = isActive
                                        If isActive Then
                                            btn1.BackgroundColor = Eto.Drawing.Colors.Green
                                            btn1.Text = "ON"
                                            btn2.Enabled = True
                                            tb1.Enabled = True
                                            tb2.Enabled = True
                                            tb3.Enabled = True
                                        Else
                                            btn1.BackgroundColor = Eto.Drawing.Colors.Red
                                            btn1.Text = "OFF"
                                            btn2.Enabled = False
                                            tb1.Enabled = False
                                            tb2.Enabled = False
                                            tb3.Enabled = False
                                        End If
                                    End Sub)

        btn1.BackgroundColor = If(PID.Active, Eto.Drawing.Colors.Green, Eto.Drawing.Colors.Red)
        btn1.TextColor = Eto.Drawing.Colors.White

        btn2 = panel.CreateAndAddButtonRow(If(isAuto, "AUTO", "MANUAL"), Nothing,
                                    Sub(btn, e)
                                        If PID Is Nothing Then Exit Sub
                                        isAuto = Not isAuto
                                        PID.ManualOverride = Not isAuto
                                        tb3.ReadOnly = isAuto
                                        If isAuto Then
                                            btn2.BackgroundColor = Eto.Drawing.Colors.Green
                                            btn2.Text = "AUTO"
                                        Else
                                            btn2.BackgroundColor = Eto.Drawing.Colors.Blue
                                            btn2.Text = "MANUAL"
                                        End If
                                    End Sub)

        btn2.BackgroundColor = If(isAuto, Eto.Drawing.Colors.Green, Eto.Drawing.Colors.Blue)
        btn2.TextColor = Eto.Drawing.Colors.White

        tb1 = panel.CreateAndAddTextBoxRow(nf, "SP", PID.SPValue,
                                               Sub(tb, e)
                                               End Sub)

        tb2 = panel.CreateAndAddTextBoxRow(nf, "PV", PID.PVValue,
                                               Sub(tb, e)
                                               End Sub)

        tb3 = panel.CreateAndAddTextBoxRow(nf, "MV", PID.MVValue,
                                               Sub(tb, e)
                                               End Sub)

        tb2.ReadOnly = True
        tb3.ReadOnly = isAuto

        AddHandler tb1.KeyDown,
            Sub(obj, e)
                If e.Key = Keys.Enter Then
                    Try
                        PID.AdjustValue = tb1.Text.ToDoubleFromCurrent
                        PID.SPValue = PID.AdjustValue
                        form.Close()
                    Catch ex As Exception
                        MessageBox.Show("Error", ex.Message, MessageBoxButtons.OK, MessageBoxType.Error)
                    End Try
                End If
            End Sub

        AddHandler tb3.KeyDown,
            Sub(obj, e)
                If e.Key = Keys.Enter And Not tb3.ReadOnly Then
                    Try
                        PID.MVValue = tb3.Text.ToDoubleFromCurrent
                        form.Close()
                    Catch ex As Exception
                        MessageBox.Show("Error", ex.Message, MessageBoxButtons.OK, MessageBoxType.Error)
                    End Try
                End If
            End Sub

        If isActive Then
            btn2.Enabled = True
            tb1.Enabled = True
            tb2.Enabled = True
            tb3.Enabled = True
        Else
            btn2.Enabled = False
            tb1.Enabled = False
            tb2.Enabled = False
            tb3.Enabled = False
        End If

        Return form

    End Function

    Public Shared Sub SetPIDDelegate(gobj As IGraphicObject, myObj As ISimulationObject)

        gobj.ControlPanelModeEditorDisplayDelegate =
            Sub()
                Dim f = CreatePIDForm(myObj)
                f.Location = Mouse.Position
                f.ShowModal()
            End Sub

    End Sub

End Class
