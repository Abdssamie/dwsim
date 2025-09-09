Imports DWSIM.UnitOperations.UnitOperations
Imports DWSIM.UnitOperations.UnitOperations.Valve
Imports SkiaSharp.Views.Desktop

Namespace UnitOperations

    Public Class ReliefValve

        Inherits UnitOpBaseClass

        Implements IExternalUnitOperation

        Private UOName As String = "Relief Valve"

        Private UODescription As String = "Safety Relief Valve model"

        Private ReadOnly Property IExternalUnitOperation_Name As String = UOName Implements IExternalUnitOperation.Name

        Public ReadOnly Property Description As String = UODescription Implements IExternalUnitOperation.Description

        Public ReadOnly Property Prefix As String = "PSV-" Implements IExternalUnitOperation.Prefix

        Public Overrides ReadOnly Property MobileCompatible As Boolean = False

        Public Property PercentOpeningVersusPercentKvExpression As String = "1.0*OP"

        Public Property EnableOpeningKvRelationship As Boolean = False

        Public Property CharacteristicParameter As Double = 50

        Public Property DefinedOpeningKvRelationShipType As OpeningKvRelationshipType = OpeningKvRelationshipType.UserDefined

        Public Property OpeningKvRelDataTableX As New List(Of Double)

        Public Property OpeningKvRelDataTableY As New List(Of Double)

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
            Return My.Resources.relief_valve
        End Function

        Private Image As SkiaSharp.SKImage

        'this function draws the object on the flowsheet
        Public Sub Draw(g As Object) Implements Interfaces.IExternalUnitOperation.Draw

            'get the canvas object
            Dim canvas = DirectCast(g, SkiaSharp.SKCanvas)

            'load the icon image on memory
            If Image Is Nothing Then

                Using bitmap = My.Resources.relief_valve.ToSKBitmap()
                    Image = SkiaSharp.SKImage.FromBitmap(bitmap)
                End Using

            End If

            Dim x = Me.GraphicObject.X
            Dim y = Me.GraphicObject.Y
            Dim w = Me.GraphicObject.Width
            Dim h = Me.GraphicObject.Height

            'draw the image into the flowsheet inside the object's reserved rectangle area
            Using p As New SkiaSharp.SKPaint With {.FilterQuality = SkiaSharp.SKFilterQuality.High}
                canvas.DrawImage(Image, New SkiaSharp.SKRect(GraphicObject.X, GraphicObject.Y, GraphicObject.X + GraphicObject.Width, GraphicObject.Y + GraphicObject.Height), p)
            End Using

        End Sub

        'this function creates the connection ports in the flowsheet object
        Public Sub CreateConnectors() Implements Interfaces.IExternalUnitOperation.CreateConnectors

            If GraphicObject.InputConnectors.Count = 0 Then

                Dim port1 As New Drawing.SkiaSharp.GraphicObjects.ConnectionPoint()

                port1.IsEnergyConnector = False
                port1.Type = Interfaces.Enums.GraphicObjects.ConType.ConIn
                port1.Position = New DWSIM.DrawingTools.Point.Point(GraphicObject.X, GraphicObject.Y + 0.55 * GraphicObject.Height)
                port1.ConnectorName = "Inlet Port 1"

                GraphicObject.InputConnectors.Add(port1)

            Else

                GraphicObject.InputConnectors(0).Position = New DWSIM.DrawingTools.Point.Point(GraphicObject.X, GraphicObject.Y + 0.55 * GraphicObject.Height)
                GraphicObject.InputConnectors(0).ConnectorName = "Inlet Port 1"

            End If

            If GraphicObject.OutputConnectors.Count = 0 Then

                Dim port3 As New Drawing.SkiaSharp.GraphicObjects.ConnectionPoint()

                port3.IsEnergyConnector = False
                port3.Type = Interfaces.Enums.GraphicObjects.ConType.ConOut
                port3.Position = New DWSIM.DrawingTools.Point.Point(GraphicObject.X + GraphicObject.Width, GraphicObject.Y + 0.34 * GraphicObject.Height)
                port3.ConnectorName = "Outlet Port 1"

                GraphicObject.OutputConnectors.Add(port3)

                Dim port4 As New Drawing.SkiaSharp.GraphicObjects.ConnectionPoint()

                port4.IsEnergyConnector = False
                port4.Type = Interfaces.Enums.GraphicObjects.ConType.ConOut
                port4.Position = New DWSIM.DrawingTools.Point.Point(GraphicObject.X + GraphicObject.Width, GraphicObject.Y + GraphicObject.Height + 0.78 * GraphicObject.Height)
                port4.ConnectorName = "Outlet Port 2"

                GraphicObject.OutputConnectors.Add(port4)

            Else

                GraphicObject.OutputConnectors(0).Position = New DWSIM.DrawingTools.Point.Point(GraphicObject.X + GraphicObject.Width, GraphicObject.Y + 0.34 * GraphicObject.Height)
                GraphicObject.OutputConnectors(1).Position = New DWSIM.DrawingTools.Point.Point(GraphicObject.X + GraphicObject.Width, GraphicObject.Y + 0.78 * GraphicObject.Height)
                GraphicObject.OutputConnectors(0).ConnectorName = "Outlet Port 1"
                GraphicObject.OutputConnectors(1).ConnectorName = "Outlet Port 2"

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