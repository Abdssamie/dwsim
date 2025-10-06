Imports DWSIM.ExtensionMethods
Imports DWSIM.Interfaces.Enums
Imports DWSIM.Interfaces.Enums.GraphicObjects
Imports DWSIM.Thermodynamics.Streams

Public Class EditingForm_SeparatorFiller

    Public SimObject As UnitOperations.UnitOpBaseClass

    Private Sub EditingForm_SeparatorFiller_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Dim units = SimObject.FlowSheet.FlowsheetOptions.SelectedUnitSystem
        Dim nf = SimObject.FlowSheet.FlowsheetOptions.NumberFormat

        lblPressure.Text = units.pressure

        Dim pressure As Double = SimObject.GetDynamicProperty("Operating Pressure")

        tbPressure.Text = pressure.ConvertFromSI(units.pressure).ToString(nf)

        Dim mslist As String() = SimObject.FlowSheet.GraphicObjects.Values.Where(Function(x) x.ObjectType = ObjectType.MaterialStream).Select(Function(m) m.Tag).ToArray

        cbStreams.Items.Clear()
        cbStreams.Items.AddRange(mslist)

        If mslist.Count > 0 Then cbStreams.SelectedIndex = 0

        lblVessel.Text = SimObject.GraphicObject.Tag

        ChangeDefaultFont()

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click

        Close()

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        Dim units = SimObject.FlowSheet.FlowsheetOptions.SelectedUnitSystem
        Dim nf = SimObject.FlowSheet.FlowsheetOptions.NumberFormat

        Dim stream As MaterialStream = SimObject.FlowSheet.GetFlowsheetSimulationObject(cbStreams.SelectedItem.ToString())

        If TypeOf SimObject Is UnitOperations.Vessel Then

            Dim Vessel = DirectCast(SimObject, UnitOperations.Vessel)

            Dim Volume As Double = Vessel.CalculateVolume()

            tbResults.Clear()
            tbResults.Text += "Separator Volume: " + volume.ConvertFromSI(units.volume).ToString(nf) + " " + units.volume + vbCrLf

            Dim pressure As Double = tbPressure.Text.ToDoubleFromCurrent().ConvertToSI(units.pressure)

            tbResults.Text += "Separator Pressure: " + pressure.ConvertFromSI(units.pressure).ToString(nf) + " " + units.pressure + vbCrLf

            SimObject.SetDynamicProperty("Operating Pressure", pressure)

            tbResults.Text += "Updating Separator Pressure... OK" + vbCrLf

            Dim astream As MaterialStream = DirectCast(stream.CloneXML(), MaterialStream)

            tbResults.Text += "Cloning Accumulation Stream... OK" + vbCrLf

            astream.Assign(stream)

            tbResults.Text += String.Format("Copying Specifications from '{0}'... OK", stream.GraphicObject.Tag) + vbCrLf

            Try
                astream.SetPressure(pressure)
                astream.SpecType = StreamSpec.Temperature_and_Pressure
                astream.PropertyPackage = SimObject.PropertyPackage
                astream.PropertyPackage.CurrentMaterialStream = astream
                astream.Calculate()
                tbResults.Text += "Flashing Stream... OK" + vbCrLf
            Catch ex As Exception
                tbResults.Text += "Flashing Stream... Error" + vbCrLf
                tbResults.Text += vbCrLf
                tbResults.Text += ex.ToString() + vbCrLf
                tbResults.Text += "Accumulation Stream NOT updated. Please try again."
                Exit Sub
            End Try

            Dim density = astream.Phases(0).Properties.density.GetValueOrDefault

            astream.SetMassFlow(density * volume)

            SimObject.AccumulationStream = DirectCast(astream.CloneXML(), MaterialStream)

            tbResults.Text += "Setting Accumulation Stream Properties... OK" + vbCrLf

        ElseIf TypeOf SimObject Is UnitOperations.Pipe Then

            tbResults.Clear()

            Dim pressure As Double = tbPressure.Text.ToDoubleFromCurrent().ConvertToSI(units.pressure)

            tbResults.Text += "Defined Pressure: " + pressure.ConvertFromSI(units.pressure).ToString(nf) + " " + units.pressure + vbCrLf

            tbResults.Text += String.Format("Copying Specifications from '{0}'... OK", stream.GraphicObject.Tag) + vbCrLf

            Dim Pipe = DirectCast(SimObject, UnitOperations.Pipe)

            Try
                Dim ims1 = Pipe.GetInletMaterialStream(0)
                Pipe.AccumulationStreams = New List(Of MaterialStream)
                For Each seg In Pipe.Profile.Sections.Values
                    Dim idx As Integer
                    Dim max As Integer = 0
                    If seg.TipoSegmento = "Tubulaosimples" Or seg.TipoSegmento = "" Or
                        seg.TipoSegmento = "Straight Tube Section" Or seg.TipoSegmento = "Straight Tube" Or
                        seg.TipoSegmento = "Tubulação Simples" Then
                        max = seg.Incrementos - 2
                    End If
                    For idx = 0 To max
                        Dim res = seg.Results(idx)
                        Dim as1 As MaterialStream = stream.CloneXML()
                        as1.SetPressure(pressure)
                        Dim D, L, V As Double
                        D = seg.DI * 0.0254
                        L = seg.Comprimento / seg.Incrementos
                        V = Math.PI * D ^ 2 * L / 4 'segment volume
                        as1.SetVolumetricFlow(V)
                        as1.AssignSelfToPP()
                        as1.Calculate(True, True)
                        Pipe.AccumulationStreams.Add(as1)
                    Next
                Next
                tbResults.Text += "Dynamic state initialized successfully." + vbCrLf
            Catch ex As Exception
                tbResults.Text += "Error intializing dynamic state: " + ex.Message + vbCrLf
                tbResults.Text += "Unit Operation contents NOT updated. Please try again."
                Exit Sub
            End Try

        End If

        tbResults.Text += "Finished Successfully!"

        SimObject.UpdateDynamicsEditForm()

    End Sub

End Class