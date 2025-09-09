Imports DWSIM.Interfaces.Enums
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

    End Class

End Namespace