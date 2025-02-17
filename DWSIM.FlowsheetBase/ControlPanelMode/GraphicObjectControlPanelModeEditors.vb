Imports DWSIM.ExtensionMethods
Imports DWSIM.Interfaces
Imports DWSIM.SharedClasses
Imports Eto.Forms

Public Class GraphicObjectControlPanelModeEditors

    Private Shared Function CreateForm() As Dialog

        Dim tb As New TextBox With {.Width = 100}
        Dim f As New Dialog
        f.Content = tb
        f.AutoSize = True
        Return f

    End Function

    Public Shared Sub SetInputDelegate(gobj As IGraphicObject, myObj As ISimulationObject)

        gobj.ControlPanelModeEditorDisplayDelegate =
            Sub()
                Dim f = CreateForm()
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

    Public Shared Sub SetPIDDelegate(gobj As IGraphicObject, myObj As ISimulationObject)

        'gobj.ControlPanelModeEditorDisplayDelegate = Sub()
        '                                                 Dim f As New FormPIDCPEditor With {.PID = myObj}
        '                                                 f.StartPosition = FormStartPosition.Manual
        '                                                 f.Location = Cursor.Position
        '                                                 f.ShowDialog()
        '                                             End Sub

    End Sub

End Class
