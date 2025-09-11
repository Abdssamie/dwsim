Imports DWSIM.Interfaces.Enums
Imports DWSIM.Thermodynamics.Streams
Imports DWSIM.UnitOperations.UnitOperations
Imports DWSIM.UnitOperations.UnitOperations.Valve
Imports SkiaSharp
Imports SkiaSharp.Views.Desktop

Namespace UnitOperations

    Public Class ReliefValve

        Inherits UnitOpBaseClass

        Implements IExternalUnitOperation

        Private UOName As String = "Relief Valve"

        Private UODescription As String = "Safety Relief Valve model"

        Public Overrides Property ObjectClass As SimulationObjectClass = SimulationObjectClass.PressureChangers

        Private ReadOnly Property IExternalUnitOperation_Name As String = UOName Implements IExternalUnitOperation.Name

        Public ReadOnly Property Description As String = UODescription Implements IExternalUnitOperation.Description

        Public ReadOnly Property Prefix As String = "PSV-" Implements IExternalUnitOperation.Prefix

        Public Overrides ReadOnly Property MobileCompatible As Boolean = False

        Public Property PercentOpeningVersusPercentKvExpression As String = "1.0*OP"

        Public Property EnableOpeningKvRelationship As Boolean = False

        Public Property CharacteristicParameter As Double = 50

        Public Property DefinedOpeningKvRelationShipType As OpeningKvRelationshipType = OpeningKvRelationshipType.Linear

        Public Property OpeningKvRelDataTableX As New List(Of Double)

        Public Property OpeningKvRelDataTableY As New List(Of Double)

        Public Property SetPointPressure As Double = 0.0

        Public Property FullyOpenedPressure As Double = 0.0

        Public Property ViscosityCoefficient As Double = 1.0

        Public Property DischargeCoefficient As Double = 1.0

        Public Property BackPressureCoefficient As Double = 1.0

        Public Property OrificeArea As Double = 0.71 * 0.0001  'D, m2 

        Public Shared Property StandardOrificeAreas = New List(Of String)({
            "D / 0.11 in² / 0.71 cm²",
            "E / 0.20 in² / 1.26 cm²",
            "F / 0.31 in² / 1.98 cm²",
            "G / 0.50 in² / 3.24 cm²",
            "H / 0.79 in² / 5.06 cm²",
            "J / 1.29 in² / 8.30 cm²",
            "K / 1.84 in² / 11.85 cm²",
            "L / 2.85 in² / 18.40 cm²",
            "M / 3.60 in² / 23.23 cm²",
            "N / 4.34 in² / 28.00 cm²",
            "P / 6.38 in² / 41.16 cm²",
            "Q / 11.05 in² / 71.29 cm²",
            "R / 16.00 in² / 103.22 cm²",
            "T / 26.00 in² / 167.74 cm²"
        })


        Public Sub New(ByVal Name As String, ByVal Description As String)

            MyBase.CreateNew()
            Me.ComponentName = Name
            Me.ComponentDescription = Description

        End Sub

        Public Sub New()

            MyBase.New()

        End Sub

        Public Overrides Function GetDisplayName() As String

            Return UOName

        End Function

        Public Overrides Function GetDisplayDescription() As String

            Return UODescription

        End Function

        Public Function ReturnInstance(typename As String) As Object Implements IExternalUnitOperation.ReturnInstance
            Return New ReliefValve()
        End Function

        Public Overrides Function CloneXML() As Object

            Dim objdata = XMLSerializer.XMLSerializer.Serialize(Me)
            Dim newrf = New ReliefValve()
            newrf.LoadData(objdata)

            Return newrf

        End Function

        Public Overrides Function CloneJSON() As Object

            Dim jsonstring = Newtonsoft.Json.JsonConvert.SerializeObject(Me)
            Dim newrf = Newtonsoft.Json.JsonConvert.DeserializeObject(Of ReliefValve)(jsonstring)

            Return newrf

        End Function


#Region "Automatic Drawing Support"

        Public Overrides Function GetIconBitmap() As Object
            Return My.Resources.Relief_Valve_48px
        End Function

        Private Image As SkiaSharp.SKImage

        'this function draws the object on the flowsheet
        Public Sub Draw(g As Object) Implements Interfaces.IExternalUnitOperation.Draw

            Dim canvas As SKCanvas = DirectCast(g, SKCanvas)

            CreateConnectors()
            GraphicObject.UpdateStatus()

            Dim myPen As New SKPaint()
            With myPen
                .Color = GraphicObject.LineColor
                .StrokeWidth = GraphicObject.LineWidth
                .IsStroke = True
                .IsAntialias = GlobalSettings.Settings.DrawingAntiAlias
            End With

            Dim X = GraphicObject.X
            Dim Y = GraphicObject.Y
            Dim Height = GraphicObject.Height
            Dim Width = GraphicObject.Width

            Dim gp As New SKPath()

            gp.MoveTo(Convert.ToInt32(X + 0.2 * Width), Convert.ToInt32(Y + Height))
            gp.LineTo(Convert.ToInt32(X + 0.5 * Width), Convert.ToInt32(Y + 0.5 * Height))
            gp.LineTo(Convert.ToInt32(X + Width), Convert.ToInt32(Y + 0.2 * Height))
            gp.LineTo(Convert.ToInt32(X + Width), Convert.ToInt32(Y + 0.8 * Height))
            gp.LineTo(Convert.ToInt32(X + 0.5 * Width), Convert.ToInt32(Y + 0.5 * Height))
            gp.LineTo(Convert.ToInt32(X + 0.8 * Width), Convert.ToInt32(Y + Height))
            gp.LineTo(Convert.ToInt32(X + 0.2 * Width), Convert.ToInt32(Y + Height))
            gp.Close()

            Select Case GraphicObject.DrawMode

                Case 0

                    'default

                    Dim gradPen As New SKPaint()
                    With gradPen
                        .Color = GraphicObject.LineColor.WithAlpha(50)
                        .StrokeWidth = GraphicObject.LineWidth
                        .IsStroke = False
                        .IsAntialias = GlobalSettings.Settings.DrawingAntiAlias
                    End With

                    canvas.DrawPath(gp, gradPen)

                    canvas.DrawPath(gp, myPen)

                    canvas.DrawLine(Convert.ToInt32(X + 0.5 * Width), Convert.ToInt32(Y + 0.5 * Height), Convert.ToInt32(X + 0.5 * Width), Convert.ToInt32(Y + 0.2 * Height), myPen)
                    canvas.DrawLine(Convert.ToInt32(X + 0.5 * Width), Convert.ToInt32(Y + 0.2 * Height), Convert.ToInt32(X), Convert.ToInt32(Y + 0.2 * Height), myPen)

                    canvas.DrawLine(Convert.ToInt32(X + 0.1 * Width), Convert.ToInt32(Y + 0.3 * Height), Convert.ToInt32(X + 0.2 * Width), Convert.ToInt32(Y + 0.1 * Height), myPen)
                    canvas.DrawLine(Convert.ToInt32(X + 0.2 * Width), Convert.ToInt32(Y + 0.3 * Height), Convert.ToInt32(X + 0.3 * Width), Convert.ToInt32(Y + 0.1 * Height), myPen)
                    canvas.DrawLine(Convert.ToInt32(X + 0.3 * Width), Convert.ToInt32(Y + 0.3 * Height), Convert.ToInt32(X + 0.4 * Width), Convert.ToInt32(Y + 0.1 * Height), myPen)

                Case 1

                    'b/w

                    With myPen
                        .Color = SKColors.Black
                        .StrokeWidth = GraphicObject.LineWidth
                        .IsStroke = True
                        .IsAntialias = GlobalSettings.Settings.DrawingAntiAlias
                    End With
                    canvas.DrawPath(gp, myPen)

                    canvas.DrawLine(Convert.ToInt32(X + 0.5 * Width), Convert.ToInt32(Y + 0.5 * Height), Convert.ToInt32(X + 0.5 * Width), Convert.ToInt32(Y + 0.2 * Height), myPen)
                    canvas.DrawLine(Convert.ToInt32(X + 0.5 * Width), Convert.ToInt32(Y + 0.2 * Height), Convert.ToInt32(X), Convert.ToInt32(Y + 0.2 * Height), myPen)

                    canvas.DrawLine(Convert.ToInt32(X + 0.1 * Width), Convert.ToInt32(Y + 0.3 * Height), Convert.ToInt32(X + 0.2 * Width), Convert.ToInt32(Y + 0.1 * Height), myPen)
                    canvas.DrawLine(Convert.ToInt32(X + 0.2 * Width), Convert.ToInt32(Y + 0.3 * Height), Convert.ToInt32(X + 0.3 * Width), Convert.ToInt32(Y + 0.1 * Height), myPen)
                    canvas.DrawLine(Convert.ToInt32(X + 0.3 * Width), Convert.ToInt32(Y + 0.3 * Height), Convert.ToInt32(X + 0.4 * Width), Convert.ToInt32(Y + 0.1 * Height), myPen)

                Case 2

                    'load the icon image on memory
                    If Image Is Nothing Then

                        Using bitmap = My.Resources.Relief_Valve_48px.ToSKBitmap()
                            Image = SkiaSharp.SKImage.FromBitmap(bitmap)
                        End Using

                    End If

                    'draw the image into the flowsheet inside the object's reserved rectangle area
                    Using p As New SkiaSharp.SKPaint With {.FilterQuality = SkiaSharp.SKFilterQuality.High}
                        canvas.DrawImage(Image, New SkiaSharp.SKRect(GraphicObject.X, GraphicObject.Y, GraphicObject.X + GraphicObject.Width, GraphicObject.Y + GraphicObject.Height), p)
                    End Using

            End Select

        End Sub

        'this function creates the connection ports in the flowsheet object
        Public Sub CreateConnectors() Implements Interfaces.IExternalUnitOperation.CreateConnectors

            If GraphicObject.InputConnectors.Count = 0 Then

                Dim port1 As New Drawing.SkiaSharp.GraphicObjects.ConnectionPoint()

                port1.IsEnergyConnector = False
                port1.Type = Interfaces.Enums.GraphicObjects.ConType.ConIn
                port1.Position = New DWSIM.DrawingTools.Point.Point(GraphicObject.X + 0.5 * GraphicObject.Width, GraphicObject.Y + GraphicObject.Height)
                port1.ConnectorName = "Inlet Port"
                port1.Direction = Enums.GraphicObjects.ConDir.Up

                GraphicObject.InputConnectors.Add(port1)

            Else

                GraphicObject.InputConnectors(0).Position = New DWSIM.DrawingTools.Point.Point(GraphicObject.X + 0.5 * GraphicObject.Width, GraphicObject.Y + GraphicObject.Height)
                GraphicObject.InputConnectors(0).ConnectorName = "Inlet Port"
                GraphicObject.InputConnectors(0).Direction = Enums.GraphicObjects.ConDir.Up

            End If

            If GraphicObject.OutputConnectors.Count = 0 Then

                Dim port3 As New Drawing.SkiaSharp.GraphicObjects.ConnectionPoint()

                port3.IsEnergyConnector = False
                port3.Type = Interfaces.Enums.GraphicObjects.ConType.ConOut
                port3.Position = New DWSIM.DrawingTools.Point.Point(GraphicObject.X + GraphicObject.Width, GraphicObject.Y + 0.5 * GraphicObject.Height)
                port3.ConnectorName = "Outlet Port"

                GraphicObject.OutputConnectors.Add(port3)

            Else

                GraphicObject.OutputConnectors(0).Position = New DWSIM.DrawingTools.Point.Point(GraphicObject.X + GraphicObject.Width, GraphicObject.Y + 0.5 * GraphicObject.Height)
                GraphicObject.OutputConnectors(0).ConnectorName = "Outlet Port"

            End If

            GraphicObject.EnergyConnector.Active = False

        End Sub

#End Region

#Region "Classic UI and Cross-Platform UI Editor Support"

        <Xml.Serialization.XmlIgnore> Public editwindow As EditingForm_ReliefValve

        'display the editor on the classic user interface
        Public Overrides Sub DisplayEditForm()

            If editwindow Is Nothing Then

                editwindow = New EditingForm_ReliefValve() With {.SimObject = Me}

            Else

                If editwindow.IsDisposed Then
                    editwindow = New EditingForm_ReliefValve() With {.SimObject = Me}
                End If

            End If

            FlowSheet.DisplayForm(editwindow)

        End Sub

        'this updates the editor window on classic ui
        Public Overrides Sub UpdateEditForm()

            If editwindow IsNot Nothing Then

                If editwindow.InvokeRequired Then

                    editwindow.Invoke(Sub()
                                          editwindow?.UpdateInfo()
                                      End Sub)

                Else

                    editwindow?.UpdateInfo()

                End If

            End If

        End Sub

        'this closes the editor on classic ui
        Public Overrides Sub CloseEditForm()

            editwindow?.Close()

        End Sub

        'returns the editing form
        Public Overrides Function GetEditingForm() As Form

            Return Nothing

        End Function

        'this function display the properties on the cross-platform user interface
        Public Sub PopulateEditorPanel(container As Object) Implements Interfaces.IExternalUnitOperation.PopulateEditorPanel

            'using extension methods from DWSIM.ExtensionMethods.Eto (DWISM.UI.Shared namespace)

        End Sub

#End Region

        Public Overrides Sub Calculate(Optional args As Object = Nothing)


        End Sub

        Public Overrides Sub RunDynamicModel()

            Dim integratorID = FlowSheet.DynamicsManager.ScheduleList(FlowSheet.DynamicsManager.CurrentSchedule).CurrentIntegrator
            Dim integrator = FlowSheet.DynamicsManager.IntegratorList(integratorID)

            If Not integrator.ShouldCalculatePressureFlow Then Exit Sub

            If Not Me.GraphicObject.OutputConnectors(0).IsAttached Then
                Throw New Exception(FlowSheet.GetTranslatedString("Verifiqueasconexesdo"))
            ElseIf Not Me.GraphicObject.InputConnectors(0).IsAttached Then
                Throw New Exception(FlowSheet.GetTranslatedString("Verifiqueasconexesdo"))
            End If

            Dim T1, P1, H1, W, P2, rho, CpCv, V1, xv As Double

            Dim ims, oms As MaterialStream

            ims = Me.GetInletMaterialStream(0)
            oms = Me.GetOutletMaterialStream(0)

            If ims.DynamicsSpec <> Dynamics.DynamicsSpecType.Pressure OrElse
                        oms.DynamicsSpec <> Dynamics.DynamicsSpecType.Pressure Then

                Throw New Exception("Both onlet and outlet streams must be pressure-specified in dynamic mode.")

            End If

            Dim Kvc As Double = 1.0

            Dim OpeningPct = (P1 - SetPointPressure) / (FullyOpenedPressure - SetPointPressure)

            If OpeningPct < 0.0 Then OpeningPct = 0.0
            If OpeningPct > 1.0 Then OpeningPct = 1.0

            If Double.IsInfinity(OpeningPct) Then OpeningPct = 1.0

            If EnableOpeningKvRelationship Then
                Select Case DefinedOpeningKvRelationShipType
                    Case OpeningKvRelationshipType.UserDefined
                        Try
                            Dim ExpContext As New Ciloci.Flee.ExpressionContext()
                            ExpContext.Imports.AddType(GetType(System.Math))
                            ExpContext.Variables.Clear()
                            ExpContext.Options.ParseCulture = Globalization.CultureInfo.InvariantCulture
                            ExpContext.Variables.Add("OP", OpeningPct)
                            Dim Expr = ExpContext.CompileGeneric(Of Double)(PercentOpeningVersusPercentKvExpression)
                            Kvc = Expr.Evaluate() / 100
                        Catch ex As Exception
                            Throw New Exception("Invalid expression for Kv[Cv]/Opening relationship.")
                        End Try
                    Case OpeningKvRelationshipType.QuickOpening
                        Kvc = (OpeningPct / 100.0) ^ 0.5
                    Case OpeningKvRelationshipType.Linear
                        Kvc = OpeningPct / 100.0
                    Case OpeningKvRelationshipType.EqualPercentage
                        Kvc = CharacteristicParameter ^ (OpeningPct / 100.0 - 1.0)
                    Case OpeningKvRelationshipType.DataTable
                        Try
                            Dim factor = MathNet.Numerics.Interpolate.RationalWithoutPoles(OpeningKvRelDataTableX, OpeningKvRelDataTableX).Interpolate(OpeningPct) / 100.0
                            Kvc = factor
                        Catch ex As Exception
                            Throw New Exception("Error calculating Kv from tabulated data: " + ex.Message)
                        End Try
                End Select
            End If

            T1 = ims.GetTemperature()
            P1 = ims.GetPressure()
            H1 = ims.GetMassEnthalpy()

            xv = ims.Phases(2).Properties.massfraction.GetValueOrDefault

            rho = ims.Phases(0).Properties.density.GetValueOrDefault

            V1 = 1.0 / rho

            P2 = oms.GetPressure()

            CpCv = ims.Phases(2).Properties.idealGasHeatCapacityRatio.GetValueOrDefault()

            Dim choked_factor = (2.0 / (CpCv + 1)) ^ (CpCv / (CpCv - 1))

            Dim A = OrificeArea

            Dim Kv = ViscosityCoefficient

            Dim Kd = DischargeCoefficient

            Dim Kb = BackPressureCoefficient

            If xv > 0.99 Then

                'vapor flow

                If (P2 / P1) >= choked_factor Then

                    'choked flow

                    W = A * Kvc * Kd * Kb * (P1 * CpCv / V1 * (2 / (CpCv + 1)) ^ ((CpCv - 1) / (CpCv + 1))) ^ 0.5

                Else

                    'non-choked flow

                    W = A * Kvc * Kd * (P1 / V1 * (2 * CpCv / (CpCv + 1)) * ((P2 / P1) ^ (2.0 / CpCv) - (P2 / P1) ^ ((CpCv + 1) / CpCv))) ^ 0.5

                End If

            ElseIf xv < 0.01 Then

                'liquid flow

                W = A * Kvc * Kd * Kv * (2 * (P1 - P2) * rho) ^ 0.5

            Else

                Throw New Exception("Two-phase flow is not supported yet.")

            End If

            ims.SetMassFlow(W)
            oms.SetMassFlow(W)

            With oms
                .Phases(0).Properties.pressure = P2
                .Phases(0).Properties.enthalpy = H1
                Dim i As Integer = 0
                For Each comp In .Phases(0).Compounds.Values
                    comp.MoleFraction = ims.Phases(0).Compounds(comp.Name).MoleFraction
                    comp.MassFraction = ims.Phases(0).Compounds(comp.Name).MassFraction
                    comp.MassFlow = comp.MassFraction * W
                    comp.MolarFlow = comp.MassFlow / comp.ConstantProperties.Molar_Weight * 1000
                    i += 1
                Next
            End With

            With ims
                Dim i As Integer = 0
                For Each comp In .Phases(0).Compounds.Values
                    comp.MassFlow = comp.MassFraction * W
                    comp.MolarFlow = comp.MassFlow / comp.ConstantProperties.Molar_Weight * 1000
                    i += 1
                Next
            End With

        End Sub

    End Class

End Namespace